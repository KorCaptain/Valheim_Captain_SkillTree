using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 아처 직업 멀티샷 시스템 - Y키 액티브 스킬
    /// Phase 1 (Y키): 화살을 머리 위 부채꼴 호 형태로 소환·정지
    /// Phase 2 (활 공격 후 적 적중): 소환 발사체가 적의 머리로 자동 호밍 발사
    /// 30초 내 적 미적중 시 발사체 소멸
    /// </summary>
    public static partial class SkillEffect
    {
        private static float lastArcherMultiShotTime = 0f;

        // 소환 상태 (이중시전 방식 - 로컬 오프셋 기반)
        private static Dictionary<Player, List<(GameObject obj, Vector3 localOffset)>> _archerSuspended
            = new Dictionary<Player, List<(GameObject, Vector3)>>();
        private static Dictionary<Player, Coroutine> _archerSummonCoroutines
            = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, Coroutine> _archerLaunchCoroutines
            = new Dictionary<Player, Coroutine>();

        // VFX
        private static GameObject cachedArcherBuffEffectPrefab = null;
        private static GameObject cachedArcherStatusEffectPrefab = null;
        private static Dictionary<Player, GameObject> archerMultiShotBuffEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, GameObject> archerMultiShotStatusEffects = new Dictionary<Player, GameObject>();

        private static int GetArcherLevel() => SkillTreeManager.Instance?.GetSkillLevel("Archer") ?? 0;

        public static int ARCHER_MULTISHOT_ARROWS
        {
            get
            {
                var baseCount = Archer_Config.ArcherMultiShotArrowCountValue;
                var lv = GetArcherLevel();
                return lv switch {
                    2 => baseCount + Archer_Config.ArcherLv2BonusArrowsValue,
                    3 => baseCount + Archer_Config.ArcherLv3BonusArrowsValue,
                    4 => baseCount + Archer_Config.ArcherLv4BonusArrowsValue,
                    5 => baseCount + Archer_Config.ArcherLv5BonusArrowsValue,
                    _ => baseCount
                };
            }
        }

        /// <summary>소환된 발사체가 대기 중인지 확인</summary>
        public static bool IsArcherMultiShotReady(Player player)
        {
            return _archerSuspended.ContainsKey(player);
        }

        /// <summary>Y키: 화살을 머리 위 부채꼴로 소환</summary>
        public static bool ExecuteArcherMultiShot(Player player)
        {
            try
            {
                // Phase 2: 이미 소환 중이면 무시 (공격으로 발사)
                if (_archerSuspended.ContainsKey(player)) return false;

                if (!CanUseArcherMultiShot(player)) return false;

                var weapon = player.GetCurrentWeapon();
                if (weapon?.m_shared?.m_skillType != Skills.SkillType.Bows)
                {
                    ShowSkillEffectText(player, "❌ " + L.Get("bow_equip_required"), Color.red, SkillEffectTextType.Combat);
                    return false;
                }

                var ammo = GetArcherArrow(player);
                if (ammo == null || ammo.m_stack <= 0)
                {
                    ShowSkillEffectText(player, "❌ " + L.Get("no_arrows"), Color.red, SkillEffectTextType.Combat);
                    return false;
                }

                var staminaCost = Archer_Config.ArcherMultiShotStaminaCostValue;
                if (player.GetStamina() < staminaCost)
                {
                    ShowSkillEffectText(player, "❌ " + L.Get("stamina_insufficient"), Color.red, SkillEffectTextType.Combat);
                    return false;
                }

                lastArcherMultiShotTime = Time.time;
                ActiveSkillCooldownRegistry.SetCooldown("Y", Archer_Config.ArcherMultiShotCooldownValue);
                player.UseStamina(staminaCost);

                // Phase 1 시작
                SummonArcherProjectiles(player, ammo);
                PlayArcherMultiShotBuffActivationEffects(player);
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 활성화 오류: {ex.Message}");
                return false;
            }
        }

        private static bool CanUseArcherMultiShot(Player player)
        {
            if (!HasSkill("Archer"))
            {
                ShowSkillEffectText(player, "❌ " + L.Get("archer_job_required"), Color.red, SkillEffectTextType.Combat);
                return false;
            }
            var remaining = Archer_Config.ArcherMultiShotCooldownValue - (Time.time - lastArcherMultiShotTime);
            if (remaining > 0f)
            {
                ShowSkillEffectText(player, "⏳ " + L.Get("cooldown_format", $"{remaining:F1}"), Color.yellow, SkillEffectTextType.Combat);
                return false;
            }
            return true;
        }

        /// <summary>Phase 1: 소환 코루틴 시작</summary>
        private static void SummonArcherProjectiles(Player player, ItemDrop.ItemData ammo)
        {
            // 리스트 미리 등록 → 적중 판정이 즉시 작동
            _archerSuspended[player] = new List<(GameObject, Vector3)>();

            if (_archerSummonCoroutines.TryGetValue(player, out var prev) && prev != null)
                SkillTreeInputListener.Instance.StopCoroutine(prev);

            var co = SkillTreeInputListener.Instance.StartCoroutine(ArcherSummonCoroutine(player, ammo));
            _archerSummonCoroutines[player] = co;
        }

        /// <summary>순차 소환 → 호버 타임아웃 대기 (이중시전과 동일 구조)</summary>
        private static IEnumerator ArcherSummonCoroutine(Player player, ItemDrop.ItemData ammo)
        {
            var localOffsets = CalculateArcherFanOffsets();

            // 화살 프리팹 가져오기
            var prefab = ammo?.m_shared?.m_attack?.m_attackProjectile;
            if (prefab == null)
            {
                Plugin.Log.LogWarning("[아처 멀티샷] 화살 프리팹 없음");
                CancelArcherSummon(player);
                yield break;
            }

            if (!_archerSuspended.TryGetValue(player, out var suspended)) yield break;

            int count = ARCHER_MULTISHOT_ARROWS;
            int actualCount = Mathf.Min(count, localOffsets.Count);

            for (int i = 0; i < actualCount; i++)
            {
                // 소환 취소되거나 플레이어 사망 시 중단
                if (!_archerSuspended.ContainsKey(player) || player == null || player.IsDead())
                    yield break;

                var localOffset = localOffsets[i];
                Vector3 worldPos = player.transform.TransformPoint(localOffset);

                // ① 프리팹 비활성화 → Awake() 차단 (ZNetView 미등록)
                bool wasActive = prefab.activeSelf;
                prefab.SetActive(false);
                var obj = UnityEngine.Object.Instantiate(prefab, worldPos, Quaternion.identity);
                prefab.SetActive(wasActive); // ② 원상복구

                // 위험한 네트워크·물리 컴포넌트 제거
                var znv = obj.GetComponent<ZNetView>();
                if (znv != null) UnityEngine.Object.DestroyImmediate(znv);
                var zsync = obj.GetComponent<ZSyncTransform>();
                if (zsync != null) UnityEngine.Object.DestroyImmediate(zsync);
                var projComp = obj.GetComponent<Projectile>();
                if (projComp != null) UnityEngine.Object.DestroyImmediate(projComp);

                foreach (var col in obj.GetComponentsInChildren<Collider>(true))
                    col.enabled = false;

                var rb = obj.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                // 위치 추적 컴포넌트 부착 (이중시전의 StaffFanCastSuspendedBall 방식)
                var anchor = obj.AddComponent<ArcherSummonedArrow>();
                anchor.Initialize(player.transform, localOffset);

                // ③ 활성화 (ZNetView 없으므로 ZDO 미등록 상태로 비주얼만)
                obj.SetActive(true);

                // VFX 밝기 감소
                SimpleVFX.ApplyVFXDim(obj, SkillTreeConfig.VFXOpacityValue);

                suspended.Add((obj, localOffset));

                // 0.1초 간격 순차 소환 (이중시전 0.4초보다 빠르게)
                yield return new WaitForSeconds(0.1f);
            }

            // 소환 완료 메시지
            if (_archerSuspended.ContainsKey(player))
            {
                int summoned = suspended.Count;
                ShowSkillEffectText(player, "🏹 " + L.Get("multishot_summoned", summoned.ToString()),
                    new Color(0.2f, 0.8f, 0.2f), SkillEffectTextType.Combat);
            }

            // 호버 타임아웃 대기 (기본 30초)
            yield return new WaitForSeconds(Archer_Config.ArcherMultiShotHoverTimeValue);

            if (_archerSuspended.ContainsKey(player))
            {
                ShowSkillEffectText(player, "⏰ " + L.Get("multishot_expired"), Color.gray, SkillEffectTextType.Combat);
                CancelArcherSummon(player);
            }
        }

        /// <summary>부채꼴 호 로컬 오프셋 계산 (이중시전의 수직 반원 호 방식 참조)</summary>
        private static List<Vector3> CalculateArcherFanOffsets()
        {
            float height = Archer_Config.ArcherMultiShotSummonHeightValue;   // 기본 2.5m
            float radius = Archer_Config.ArcherMultiShotSummonRadiusValue;   // 기본 1.5m

            // 로컬 중심: 플레이어 머리 위
            var center = new Vector3(0f, height, 0.2f);

            // 수평 부채꼴 배치: 왼(-60°) → 중앙(0°) → 오른(+60°) 최대 9발
            float[] angles = { -60f, -45f, -30f, -15f, 0f, 15f, 30f, 45f, 60f };
            var offsets = new List<Vector3>();
            foreach (float deg in angles)
            {
                float rad = deg * Mathf.Deg2Rad;
                // X: 좌우(sin), Z: 앞쪽 약간(cos 감쇠)
                var offset = new Vector3(Mathf.Sin(rad) * radius, 0f, Mathf.Cos(rad) * radius * 0.15f);
                offsets.Add(center + offset);
            }
            return offsets;
        }

        /// <summary>원본 화살이 적에 적중 → 소환 발사체 순차 호밍 발사</summary>
        public static void OnOriginalArrowHitEnemy(Player player, Character enemy)
        {
            try
            {
                if (!_archerSuspended.TryGetValue(player, out var suspended) || suspended.Count == 0) return;

                // 소환 코루틴 중단
                if (_archerSummonCoroutines.TryGetValue(player, out var sumCo) && sumCo != null)
                    SkillTreeInputListener.Instance.StopCoroutine(sumCo);
                _archerSummonCoroutines.Remove(player);

                // 현재 무기·화살 정보 캡처
                var weapon = player.GetCurrentWeapon();
                if (weapon == null) { CancelArcherSummon(player); return; }
                var ammo = GetArcherArrow(player);
                if (ammo == null) { CancelArcherSummon(player); return; }

                // suspended 목록에서 제거 (발사 전)
                _archerSuspended.Remove(player);

                // 이전 발사 코루틴 중단
                if (_archerLaunchCoroutines.TryGetValue(player, out var prevLaunch) && prevLaunch != null)
                    SkillTreeInputListener.Instance.StopCoroutine(prevLaunch);

                var launchCo = SkillTreeInputListener.Instance.StartCoroutine(
                    ArcherLaunchCoroutine(player, weapon, ammo, suspended, enemy));
                _archerLaunchCoroutines[player] = launchCo;

                RemoveArcherMultiShotVFX(player);
                ShowSkillEffectText(player, "🏹 " + L.Get("multishot_launched"),
                    new Color(1f, 0.6f, 0f), SkillEffectTextType.Combat);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 소환 발사체 발사 오류: {ex.Message}");
            }
        }

        /// <summary>순차 발사 코루틴 0.05초 간격 (이중시전 FanCastLaunchCoroutine 방식)</summary>
        private static IEnumerator ArcherLaunchCoroutine(Player player, ItemDrop.ItemData weapon,
            ItemDrop.ItemData ammo, List<(GameObject obj, Vector3 localOffset)> suspended, Character target)
        {
            float gap = 0.05f;

            for (int i = 0; i < suspended.Count; i++)
            {
                if (player == null || player.IsDead()) break;

                var (obj, localOff) = suspended[i];

                // 현재 world 위치 캡처
                Vector3 firePos = obj != null
                    ? obj.transform.position
                    : player.transform.TransformPoint(localOff);

                // 소환 비주얼 제거
                if (obj != null) { obj.SetActive(false); UnityEngine.Object.Destroy(obj); }

                // 실제 화살 발사 → 타겟 머리 호밍
                FireArcherHomingArrow(player, weapon, ammo, firePos, target);

                if (i < suspended.Count - 1)
                    yield return new WaitForSeconds(gap);
            }

            // 잔여 오브젝트 정리 (중도 중단 시)
            foreach (var (obj, _) in suspended)
            {
                if (obj != null) { obj.SetActive(false); UnityEngine.Object.Destroy(obj); }
            }

            _archerLaunchCoroutines.Remove(player);
        }

        /// <summary>소환 위치에서 실제 화살 발사 + 호밍 컴포넌트 부착 (FireFanCastProjectile 방식)</summary>
        private static void FireArcherHomingArrow(Player player, ItemDrop.ItemData weapon,
            ItemDrop.ItemData ammo, Vector3 spawnPos, Character target)
        {
            try
            {
                var ammoAttack = ammo.m_shared.m_attack;
                if (ammoAttack?.m_attackProjectile == null) return;
                var bowAttack = weapon.m_shared.m_attack;

                // 타겟 머리 조준 방향 계산
                Vector3 aimDir;
                if (target != null && !target.IsDead())
                {
                    float headOff = GetHeadOffset(target.name);
                    Vector3 headPos = target.transform.position + Vector3.up * headOff;
                    aimDir = (headPos - spawnPos).normalized;
                }
                else
                {
                    aimDir = player.GetLookDir();
                }

                var projectileObj = UnityEngine.Object.Instantiate(
                    ammoAttack.m_attackProjectile,
                    spawnPos,
                    Quaternion.LookRotation(aimDir));

                var projectile = projectileObj.GetComponent<Projectile>();
                if (projectile == null) { UnityEngine.Object.Destroy(projectileObj); return; }

                // HitData 구성
                var lv = GetArcherLevel();
                float dmgPct = lv switch {
                    2 => Archer_Config.ArcherLv2DamagePercentValue,
                    3 => Archer_Config.ArcherLv3DamagePercentValue,
                    4 => Archer_Config.ArcherLv4DamagePercentValue,
                    5 => Archer_Config.ArcherLv5DamagePercentValue,
                    _ => Archer_Config.ArcherMultiShotDamagePercentValue
                };
                var hitData = new HitData();
                hitData.m_damage = weapon.GetDamage();
                hitData.m_damage.Add(ammo.GetDamage());
                hitData.m_damage.Modify(dmgPct / 100f);
                hitData.m_point = spawnPos;
                hitData.m_dir = aimDir;
                hitData.m_attacker = player.GetZDOID();
                hitData.m_skill = Skills.SkillType.Bows;
                hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;

                float vel = bowAttack.m_projectileVel > 0f ? bowAttack.m_projectileVel : 50f;
                projectile.Setup(player, aimDir * vel, bowAttack.m_projectileAccuracy, hitData, ammo, weapon);

                // VFX 밝기 감소
                SimpleVFX.ApplyVFXDim(projectileObj, SkillTreeConfig.VFXOpacityValue);

                // 호밍 컴포넌트 부착 (StaffDualCastHomingProjectile 방식)
                if (target != null && !target.IsDead())
                {
                    var homing = projectileObj.AddComponent<ArcherMultiShotHomingArrow>();
                    homing.Initialize(target);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 화살 발사 오류: {ex.Message}");
            }
        }

        private static float GetHeadOffset(string charName)
        {
            if (charName.Contains("Troll")) return 4.0f;
            if (charName.Contains("Dragon")) return 8.0f;
            if (charName.Contains("Deer") || charName.Contains("Boar")) return 1.0f;
            return 1.8f;
        }

        /// <summary>소환 취소 (만료 or 사망)</summary>
        private static void CancelArcherSummon(Player player)
        {
            try
            {
                if (_archerSuspended.TryGetValue(player, out var suspended))
                {
                    foreach (var (obj, _) in suspended)
                    {
                        if (obj != null) { obj.SetActive(false); UnityEngine.Object.Destroy(obj); }
                    }
                    _archerSuspended.Remove(player);
                }
                _archerSummonCoroutines.Remove(player);
                _archerLaunchCoroutines.Remove(player);
                RemoveArcherMultiShotVFX(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 소환 취소 오류: {ex.Message}");
            }
        }

        private static void RemoveArcherMultiShotVFX(Player player)
        {
            if (archerMultiShotBuffEffects.TryGetValue(player, out var buff) && buff != null)
            { UnityEngine.Object.Destroy(buff); archerMultiShotBuffEffects.Remove(player); }
            if (archerMultiShotStatusEffects.TryGetValue(player, out var status) && status != null)
            { UnityEngine.Object.Destroy(status); archerMultiShotStatusEffects.Remove(player); }
        }

        // ===== 기존 유틸리티 유지 =====

        public static ItemDrop.ItemData GetArcherArrow(Player player)
        {
            var inventory = player.GetInventory();
            if (inventory == null) return null;
            try
            {
                var arrows = inventory.GetAmmoItem("bow", "");
                if (arrows != null && arrows.m_stack > 0 && ValidateArrowProjectile(arrows)) return arrows;
                foreach (var item in inventory.GetAllItems())
                {
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo
                        && item.m_stack > 0 && ValidateArrowProjectile(item))
                        return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 화살 검색 실패: {ex.Message}");
                return null;
            }
        }

        private static bool ValidateArrowProjectile(ItemDrop.ItemData arrow)
        {
            try { return arrow?.m_shared?.m_attack?.m_attackProjectile != null; }
            catch { return false; }
        }

        public static void PlayArcherMultiShotBuffActivationEffects(Player player)
        {
            try
            {
                PlayArcherMultiShotBuffEffect(player);
                PlayArcherMultiShotStatusEffect(player);
                PlayArcherMultiShotActivationSound(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 버프 활성화 효과 오류: {ex.Message}");
            }
        }

        private static void PlayArcherMultiShotBuffEffect(Player player)
        {
            try
            {
                if (cachedArcherBuffEffectPrefab == null)
                {
                    cachedArcherBuffEffectPrefab = VFXManager.GetVFXPrefab("buff_02a");
                    if (cachedArcherBuffEffectPrefab == null) return;
                }
                if (archerMultiShotBuffEffects.TryGetValue(player, out var old) && old != null)
                    UnityEngine.Object.Destroy(old);

                var inst = UnityEngine.Object.Instantiate(cachedArcherBuffEffectPrefab,
                    player.transform.position + Vector3.down * 0.1f, Quaternion.identity);
                inst.transform.SetParent(player.transform, false);
                inst.transform.localPosition = Vector3.down * 0.1f;
                inst.transform.localScale = Vector3.one * 0.4f;
                SetArcherBuffTransparency(inst, 0.15f);
                archerMultiShotBuffEffects[player] = inst;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] buff VFX 오류: {ex.Message}");
            }
        }

        private static void PlayArcherMultiShotStatusEffect(Player player)
        {
            try
            {
                if (cachedArcherStatusEffectPrefab == null)
                {
                    cachedArcherStatusEffectPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedArcherStatusEffectPrefab == null) return;
                }
                if (archerMultiShotStatusEffects.TryGetValue(player, out var old) && old != null)
                    UnityEngine.Object.Destroy(old);

                float duration = Archer_Config.ArcherMultiShotHoverTimeValue + 1f;
                var vfx = SimpleVFX.PlayFollowing("statusailment_01_aura", player.transform,
                    new Vector3(0f, 2.0f, 0f), duration);
                if (vfx != null) archerMultiShotStatusEffects[player] = vfx;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] status VFX 오류: {ex.Message}");
            }
        }

        private static void PlayArcherMultiShotActivationSound(Player player)
        {
            try
            {
                var sfx = ZNetScene.instance?.GetPrefab("sfx_StaffLightning_charge");
                if (sfx != null)
                    UnityEngine.Object.Instantiate(sfx, player.transform.position, Quaternion.identity);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 사운드 오류: {ex.Message}");
            }
        }

        public static void PlayArcherMultiShotHitEffect(Vector3 hitPosition, Character target)
        {
            try
            {
                if (target == null) return;
                float headOff = GetHeadOffset(target.name);
                SimpleVFX.PlayWithSound("hit_01", "arrow_hit",
                    target.transform.position + Vector3.up * headOff, 1.5f);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] hit VFX 오류: {ex.Message}");
            }
        }

        public static float GetArcherMultiShotCooldownRemaining()
        {
            return Mathf.Max(0f, Archer_Config.ArcherMultiShotCooldownValue - (Time.time - lastArcherMultiShotTime));
        }

        public static bool IsArcherMultiShotReady()
        {
            return GetArcherMultiShotCooldownRemaining() <= 0f && HasSkill("Archer");
        }

        public static void CleanupArcherMultiShotOnDeath(Player player)
        {
            try
            {
                if (_archerSummonCoroutines.TryGetValue(player, out var sumCo) && sumCo != null)
                    SkillTreeInputListener.Instance.StopCoroutine(sumCo);
                if (_archerLaunchCoroutines.TryGetValue(player, out var launchCo) && launchCo != null)
                    SkillTreeInputListener.Instance.StopCoroutine(launchCo);
                CancelArcherSummon(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[아처 멀티샷] 사망 정리 실패: {ex.Message}");
            }
        }

        private static void SetArcherBuffTransparency(GameObject buffEffect, float alpha)
        {
            if (buffEffect == null) return;
            try
            {
                foreach (var r in buffEffect.GetComponentsInChildren<Renderer>(true))
                {
                    if (r?.material == null) continue;
                    foreach (var mat in r.materials)
                    {
                        if (mat == null || !mat.HasProperty("_Color")) continue;
                        var c = mat.color; c.a = alpha; mat.color = c;
                        if (mat.HasProperty("_Mode")) { mat.SetFloat("_Mode", 2); mat.renderQueue = 3000; }
                    }
                }
                foreach (var ps in buffEffect.GetComponentsInChildren<ParticleSystem>(true))
                {
                    if (ps == null) continue;
                    var main = ps.main;
                    var sc = main.startColor.color; sc.a = alpha; main.startColor = sc;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 투명도 설정 실패: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 소환된 화살 위치 추적 컴포넌트 (이중시전의 StaffFanCastSuspendedBall 방식)
    /// 플레이어가 이동·회전해도 로컬 오프셋 위치를 LateUpdate에서 유지
    /// </summary>
    public class ArcherSummonedArrow : MonoBehaviour
    {
        private Transform _playerTransform;
        private Vector3 _localOffset;

        public void Initialize(Transform playerTransform, Vector3 localOffset)
        {
            _playerTransform = playerTransform;
            _localOffset = localOffset;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }

        private void LateUpdate()
        {
            if (_playerTransform == null) return;
            transform.position = _playerTransform.TransformPoint(_localOffset);
            // 화살 끝이 앞을 향하도록 (플레이어 정면 방향)
            if (_playerTransform.forward != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(_playerTransform.forward);
        }
    }

    /// <summary>
    /// 아처 멀티샷 화살 호밍 컴포넌트 (이중시전의 StaffDualCastHomingProjectile 방식)
    /// 타겟 머리 위치로 방향 보정
    /// </summary>
    public class ArcherMultiShotHomingArrow : MonoBehaviour
    {
        private Character _target;
        private float _homingStrength;
        private Rigidbody _rb;

        public void Initialize(Character target, float strength = 6f)
        {
            _target = target;
            _homingStrength = strength;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_target == null || _target.IsDead()) { Destroy(this); return; }
            if (_rb == null || _rb.velocity.magnitude < 0.1f) return;

            // 타겟 머리(눈) 위치로 방향 보정
            Vector3 headPos = _target.GetEyePoint();
            Vector3 toTarget = (headPos - transform.position).normalized;
            Vector3 newDir = Vector3.Lerp(_rb.velocity.normalized, toTarget,
                Time.deltaTime * _homingStrength).normalized;
            _rb.velocity = newDir * _rb.velocity.magnitude;
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
        }
    }

    /// <summary>원본 화살 적 적중 감지 → 소환 발사체 자동 호밍 발사</summary>
    [HarmonyPatch(typeof(Projectile), nameof(Projectile.OnHit))]
    [HarmonyPriority(Priority.Low)]
    public static class ArcherMultiShot_ProjectileHit_Patch
    {
        [HarmonyPrefix]
        private static void Prefix(Projectile __instance, Collider collider, bool water)
        {
            if (water || collider == null) return;

            var player = Player.m_localPlayer;
            if (player == null || !SkillEffect.IsArcherMultiShotReady(player)) return;

            // 현재 무기가 활인지 확인
            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared?.m_skillType != Skills.SkillType.Bows) return;

            // 적중한 Character 확인 (몬스터/야생동물만)
            var character = collider.GetComponentInParent<Character>();
            if (character == null || character == player || character.IsDead()) return;
            if (character.GetComponent<BaseAI>() == null) return;

            SkillEffect.OnOriginalArrowHitEnemy(player, character);
        }
    }
}

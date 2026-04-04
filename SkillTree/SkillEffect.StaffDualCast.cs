using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 지팡이 팬캐스트 액티브 스킬 시스템 (2단계)
    /// Phase 1 (R키): 7개 파이어볼을 수직 반원 호 형태로 머리 위 소환·정지
    /// Phase 2 (R키 재입력): 왼→오 순으로 0.08초 간격 연속 발사
    /// </summary>
    public static partial class SkillEffect
    {
        // === 쿨타임 관리 ===
        private static Dictionary<Player, float> staffDualExplosionCooldowns = new Dictionary<Player, float>();

        // === Phase 1: 소환된 발사체 상태 ===
        private static Dictionary<Player, List<(GameObject obj, Vector3 localOffset)>> _fanCastSuspended
            = new Dictionary<Player, List<(GameObject, Vector3)>>();
        private static Dictionary<Player, Coroutine> _fanCastSummonCoroutines
            = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, Coroutine> _fanCastLaunchCoroutines
            = new Dictionary<Player, Coroutine>();

        // === VFX 인스턴스 관리 ===
        private static Dictionary<Player, GameObject> staffDualCastBuffEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, GameObject> staffDualCastStatusEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, GameObject> staffDualCastBuffVFXInstances = new Dictionary<Player, GameObject>();
        private static GameObject cachedStaffDualCastBuffPrefab = null;

        /// <summary>
        /// R키 진입점: Phase 1(소환) 또는 Phase 2(발사) 분기
        /// </summary>
        public static void ActivateStaffDualCast(Player player)
        {
            try
            {
                // Phase 2: 소환된 구슬이 있으면 → 발사
                if (_fanCastSuspended.ContainsKey(player))
                {
                    LaunchFanCast(player);
                    return;
                }

                // Phase 1: 쿨타임 확인
                if (staffDualExplosionCooldowns.TryGetValue(player, out float cdExpiry)
                    && Time.time < cdExpiry)
                {
                    float remaining = cdExpiry - Time.time;
                    DrawFloatingText(player, L.Get("staff_dual_cast_cooldown", Mathf.CeilToInt(remaining)), Color.red);
                    return;
                }

                // Eitr 확인
                float eitrCost = Staff_Config.StaffDoubleCastEitrCostValue;
                if (player.GetEitr() < eitrCost)
                {
                    DrawFloatingText(player, L.Get("staff_eitr_insufficient", eitrCost), Color.red);
                    return;
                }

                // 지팡이 착용 확인
                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    DrawFloatingText(player, L.Get("staff_equip_required"), Color.yellow);
                    return;
                }

                // Eitr 소모 + 쿨타임 설정
                player.UseEitr(eitrCost);
                staffDualExplosionCooldowns[player] = Time.time + Staff_Config.StaffDoubleCastCooldownValue;
                ActiveSkillCooldownRegistry.SetCooldown("R", Staff_Config.StaffDoubleCastCooldownValue);

                // Phase 1 실행
                SummonFanCast(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 활성화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// Phase 1: 소환 코루틴 시작 (0.3초 간격 순차 소환 → 호버 타임아웃)
        /// </summary>
        private static void SummonFanCast(Player player)
        {
            try
            {
                // 리스트 미리 등록 → Phase 2 분기가 즉시 작동
                _fanCastSuspended[player] = new List<(GameObject obj, Vector3 localOffset)>();

                // 시전 직후 즉시 사운드/VFX
                PlayStaffDualCastBuffActivationEffects(player);

                // 이전 소환 코루틴 중단
                if (_fanCastSummonCoroutines.TryGetValue(player, out var prevCo) && prevCo != null)
                    SkillTreeInputListener.Instance.StopCoroutine(prevCo);

                var co = SkillTreeInputListener.Instance.StartCoroutine(SummonAndHoverCoroutine(player));
                _fanCastSummonCoroutines[player] = co;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 소환 시작 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 0.3초 간격 순차 소환 → 전체 완료 후 호버 타임아웃 대기
        /// </summary>
        private static IEnumerator SummonAndHoverCoroutine(Player player)
        {
            var localOffsets = CalculateFanLocalOffsets();
            var prefab = ZNetScene.instance?.GetPrefab("staff_fireball_projectile");
            if (prefab == null)
                Plugin.Log.LogWarning("[팬캐스트] staff_fireball_projectile 프리팹 없음 - 위치만 저장");

            // pre-assigned 리스트 참조
            if (!_fanCastSuspended.TryGetValue(player, out var suspended)) yield break;

            foreach (var localOffset in localOffsets)
            {
                // Phase 2가 이미 발동되었거나 플레이어 사망 시 중단
                if (!_fanCastSuspended.ContainsKey(player) || player == null || player.IsDead())
                    yield break;

                Vector3 worldPos = player.transform.TransformPoint(localOffset);
                GameObject obj = null;
                if (prefab != null)
                {
                    // ① 프리팹 비활성화 → Awake() 차단 (ZNetView.Awake가 실행되지 않으므로 ZDO 미등록)
                    bool wasActive = prefab.activeSelf;
                    prefab.SetActive(false);
                    obj = UnityEngine.Object.Instantiate(prefab, worldPos, Quaternion.identity);
                    prefab.SetActive(wasActive); // ② 프리팹 원상복구

                    // ZNet 관련 컴포넌트 전부 DestroyImmediate (SetActive 전에 제거 필수)
                    // Awake()는 enabled=false여도 실행되므로 반드시 DestroyImmediate 사용
                    var znv = obj.GetComponent<ZNetView>();
                    if (znv != null) UnityEngine.Object.DestroyImmediate(znv);
                    var zsync = obj.GetComponent<ZSyncTransform>();
                    if (zsync != null) UnityEngine.Object.DestroyImmediate(zsync);
                    var projComp = obj.GetComponent<Projectile>();
                    if (projComp != null) UnityEngine.Object.DestroyImmediate(projComp);

                    // 콜라이더 비활성화 (AutoPickup NullReferenceException 방지)
                    foreach (var col in obj.GetComponentsInChildren<Collider>())
                        col.enabled = false;

                    var rb = obj.GetComponent<Rigidbody>();
                    if (rb != null) rb.isKinematic = true;

                    var anchor = obj.AddComponent<StaffFanCastSuspendedBall>();
                    anchor.Initialize(player.transform, localOffset);

                    // ③ 활성화 (ZNetView 없으므로 ZDO 등록 없이 비주얼만 활성화)
                    obj.SetActive(true);
                }
                suspended.Add((obj, localOffset));

                // 0.3초 간격 순차 소환
                yield return new WaitForSeconds(0.3f);
            }

            // 전체 소환 완료 메시지
            if (_fanCastSuspended.ContainsKey(player))
            {
                DrawFloatingText(player, L.Get("staff_fan_cast_summoned"), new Color(0.8f, 0.3f, 1f));
                Plugin.Log.LogDebug($"[팬캐스트] Phase 1 완료 - {suspended.Count}개 소환");
            }

            // 호버 타임아웃 대기
            yield return new WaitForSeconds(Staff_Config.StaffFanCastHoverTimeValue);

            if (_fanCastSuspended.ContainsKey(player))
            {
                DrawFloatingText(player, L.Get("staff_fan_cast_expired"), Color.yellow);
                CancelFanCast(player);
            }
        }

        /// <summary>
        /// 수직 반원 호 7개 로컬 오프셋 계산 (플레이어 로컬 좌표계 기준)
        /// X=right, Y=up, Z=forward
        /// </summary>
        private static List<Vector3> CalculateFanLocalOffsets()
        {
            var offsets = new List<Vector3>();
            float height = Staff_Config.StaffFanCastHeightValue;
            float radius = Staff_Config.StaffFanCastRadiusValue;

            // 로컬 중심: 위로 height, 앞으로 0.3
            Vector3 center = new Vector3(0f, height, 0.3f);

            // 7발: 수직 반원 호, 왼(-75°) → 중앙(0°) → 오른(+75°)
            float[] angles = { -75f, -50f, -25f, 0f, 25f, 50f, 75f };
            foreach (float deg in angles)
            {
                float rad = deg * Mathf.Deg2Rad;
                // sin: X(right), cos: Y(up)
                var offset = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0f);
                offsets.Add(center + offset);
            }
            return offsets;
        }

        /// <summary>
        /// Phase 2: 소환된 7발 일제 발사 (왼→오 0.08초 간격)
        /// </summary>
        private static void LaunchFanCast(Player player)
        {
            try
            {
                if (!_fanCastSuspended.TryGetValue(player, out var suspended)) return;

                // 소환 코루틴 중단 (아직 진행 중일 수 있음)
                if (_fanCastSummonCoroutines.TryGetValue(player, out var summonCo) && summonCo != null)
                    SkillTreeInputListener.Instance.StopCoroutine(summonCo);
                _fanCastSummonCoroutines.Remove(player);

                // 무기 확인
                var weapon = player.GetCurrentWeapon();
                if (weapon == null || weapon.m_shared.m_attack.m_attackProjectile == null)
                {
                    DrawFloatingText(player, L.Get("staff_equip_required"), Color.yellow);
                    CancelFanCast(player);
                    return;
                }

                // 타겟 탐색
                Character target = FindDualCastTarget(player, Vector3.zero);

                // suspended 리스트에서 제거 (코루틴에서 처리)
                _fanCastSuspended.Remove(player);

                // 이전 발사 코루틴 중단 후 새로 시작
                if (_fanCastLaunchCoroutines.TryGetValue(player, out var prevLaunch) && prevLaunch != null)
                    SkillTreeInputListener.Instance.StopCoroutine(prevLaunch);
                var launchCo = SkillTreeInputListener.Instance.StartCoroutine(
                    FanCastLaunchCoroutine(player, weapon, suspended, target));
                _fanCastLaunchCoroutines[player] = launchCo;

                DrawFloatingText(player, L.Get("staff_fan_cast_launched"), new Color(1f, 0.4f, 0.1f));
                Plugin.Log.LogDebug($"[팬캐스트] Phase 2 발사 - {suspended.Count}발");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 발사 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 순차 발사 코루틴 (왼쪽 → 오른쪽, 0.08초 간격)
        /// </summary>
        private static IEnumerator FanCastLaunchCoroutine(Player player, ItemDrop.ItemData weapon,
            List<(GameObject obj, Vector3 localOffset)> suspended, Character target)
        {
            float damagePercent = Staff_Config.StaffDoubleCastDamagePercentValue;
            float gap = Staff_Config.StaffFanCastLaunchGapValue;

            for (int i = 0; i < suspended.Count; i++)
            {
                if (player == null || player.IsDead()) break;

                var (obj, _) = suspended[i];

                // 현재 world 위치 캡처 (플레이어 이동 반영)
                Vector3 firePos = obj != null
                    ? obj.transform.position
                    : player.transform.TransformPoint(suspended[i].localOffset);

                // 소환 VFX 제거 (SetActive false로 즉시 AutoPickup 대상에서 제외)
                if (obj != null) { obj.SetActive(false); UnityEngine.Object.Destroy(obj); }

                // 실제 발사체 발사
                FireFanCastProjectile(player, weapon, firePos, damagePercent, target);

                if (i < suspended.Count - 1)
                    yield return new WaitForSeconds(gap);
            }

            // 잔여 VFX 정리 (중도 중단 시)
            foreach (var (obj, _) in suspended)
            {
                if (obj != null) { obj.SetActive(false); UnityEngine.Object.Destroy(obj); }
            }

            _fanCastLaunchCoroutines.Remove(player);
            ClearStaffDualCastBuff(player);
        }

        /// <summary>
        /// 단일 발사체 발사 (소환 위치 → 타겟 or 정면)
        /// </summary>
        private static void FireFanCastProjectile(Player player, ItemDrop.ItemData weapon,
            Vector3 spawnPos, float damagePercent, Character target)
        {
            try
            {
                var weaponAttack = weapon.m_shared.m_attack;
                if (weaponAttack.m_attackProjectile == null) return;

                // 발사 방향: 6m 미만 → 발 조준, 6m 이상 → 머리 조준
                Vector3 actualDir;
                if (target != null && !target.IsDead())
                {
                    float distToTarget = Vector3.Distance(spawnPos, target.transform.position);
                    Vector3 aimPoint   = distToTarget < 6f
                        ? target.transform.position  // 가까운 몬스터 → 발
                        : target.GetEyePoint();      // 먼 몬스터 → 머리
                    actualDir = (aimPoint - spawnPos).normalized;
                }
                else
                {
                    actualDir = player.GetLookDir();
                }

                VFXManager.PlaySound("sfx_dverger_fireball_rain_shot", spawnPos, 2f);

                var projectileObj = UnityEngine.Object.Instantiate(
                    weaponAttack.m_attackProjectile,
                    spawnPos,
                    Quaternion.LookRotation(actualDir));

                var projectile = projectileObj.GetComponent<Projectile>();
                if (projectile == null) return;

                var hitData = new HitData();
                hitData.m_damage = weapon.GetDamage();
                hitData.m_damage.Modify(damagePercent / 100f);
                hitData.m_point = spawnPos;
                hitData.m_dir = actualDir;
                hitData.m_attacker = player.GetZDOID();
                hitData.m_skill = Skills.SkillType.ElementalMagic;
                hitData.SetAttacker(player);

                float vel = weaponAttack.m_projectileVel > 0 ? weaponAttack.m_projectileVel : 60f;
                projectile.Setup(player, actualDir * vel, weaponAttack.m_projectileAccuracy, hitData, null, weapon);

                if (target != null)
                {
                    var homing = projectileObj.AddComponent<StaffDualCastHomingProjectile>();
                    homing.Initialize(target);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 발사체 생성 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 소환 취소 (만료 or 사망 시)
        /// </summary>
        private static void CancelFanCast(Player player)
        {
            try
            {
                if (_fanCastSuspended.TryGetValue(player, out var suspended))
                {
                    foreach (var (obj, _) in suspended)
                    {
                        if (obj != null) { obj.SetActive(false); UnityEngine.Object.Destroy(obj); }
                    }
                    _fanCastSuspended.Remove(player);
                }
                _fanCastSummonCoroutines.Remove(player);
                _fanCastLaunchCoroutines.Remove(player);
                ClearStaffDualCastBuff(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 취소 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 타겟 탐색 (정면 60도 콘, 40m)
        /// </summary>
        private static Character FindDualCastTarget(Player player, Vector3 aimSpot)
        {
            Character nearest = null;
            float minDist = float.MaxValue;

            if (aimSpot != Vector3.zero)
            {
                foreach (var ch in Character.GetAllCharacters())
                {
                    if (ch == null || ch.IsPlayer() || ch.IsDead()) continue;
                    float dist = Vector3.Distance(ch.transform.position, aimSpot);
                    if (dist < 3f && dist < minDist) { minDist = dist; nearest = ch; }
                }
                if (nearest != null) return nearest;
            }

            // 각도 기준: 눈 위치 기반 (작고 가까운 몬스터의 발-벡터 각도 오류 수정)
            Vector3 playerEye = player.GetEyePoint();
            Vector3 lookDir   = player.GetLookDir();
            minDist = float.MaxValue;

            foreach (var ch in Character.GetAllCharacters())
            {
                if (ch == null || ch.IsPlayer() || ch.IsDead()) continue;
                Vector3 toTarget = ch.GetEyePoint() - playerEye;
                float dist = toTarget.magnitude;
                if (dist > 40f) continue;

                // 5m 이내: 콘 체크 생략 (코앞 몬스터 무조건 후보)
                // 10m 이내: 90° / 10m 이상: 60°
                if (dist > 5f)
                {
                    float cone = dist <= 10f ? 90f : 60f;
                    if (Vector3.Angle(lookDir, toTarget.normalized) > cone) continue;
                }

                if (dist < minDist) { minDist = dist; nearest = ch; }
            }
            return nearest;
        }

        // ========== VFX/SFX 시스템 ==========

        public static void PlayStaffDualCastBuffActivationEffects(Player player)
        {
            try
            {
                PlayStaffDualCastBuffEffect(player);
                PlayStaffDualCastStatusEffect(player);
                PlayStaffDualCastActivationSound(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 버프 효과 재생 실패: {ex.Message}");
            }
        }

        private static void PlayStaffDualCastBuffEffect(Player player)
        {
            try
            {
                if (cachedStaffDualCastBuffPrefab == null)
                {
                    cachedStaffDualCastBuffPrefab = VFXManager.GetVFXPrefab("buff_02a");
                    if (cachedStaffDualCastBuffPrefab == null) return;
                }

                if (staffDualCastBuffEffects.TryGetValue(player, out var prev) && prev != null)
                {
                    UnityEngine.Object.Destroy(prev);
                    staffDualCastBuffEffects.Remove(player);
                }

                var footPos = player.transform.position + Vector3.down * 0.1f;
                var instance = UnityEngine.Object.Instantiate(cachedStaffDualCastBuffPrefab, footPos, Quaternion.identity);
                instance.transform.SetParent(player.transform, false);
                instance.transform.localPosition = Vector3.down * 0.1f;
                instance.transform.localScale = Vector3.one * 0.4f;
                SetStaffDualCastBuffTransparency(instance, 0.2f);
                staffDualCastBuffEffects[player] = instance;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 버프 VFX 실패: {ex.Message}");
            }
        }

        private static void PlayStaffDualCastStatusEffect(Player player)
        {
            try
            {
                if (staffDualCastStatusEffects.TryGetValue(player, out var prev) && prev != null)
                {
                    UnityEngine.Object.Destroy(prev);
                    staffDualCastStatusEffects.Remove(player);
                }

                float duration = Staff_Config.StaffFanCastHoverTimeValue + 1f;
                var vfx = SimpleVFX.PlayFollowing("statusailment_01_aura", player.transform,
                    new Vector3(0f, 2.0f, 0f), duration);
                if (vfx != null)
                    staffDualCastStatusEffects[player] = vfx;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 상태 오라 실패: {ex.Message}");
            }
        }

        private static void PlayStaffDualCastActivationSound(Player player)
        {
            try
            {
                var sfx = ZNetScene.instance?.GetPrefab("sfx_StaffLightning_charge");
                if (sfx != null)
                    UnityEngine.Object.Instantiate(sfx, player.transform.position, Quaternion.identity);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 사운드 재생 오류: {ex.Message}");
            }
        }

        private static void SetStaffDualCastBuffTransparency(GameObject effect, float alpha)
        {
            if (effect == null) return;
            foreach (var r in effect.GetComponentsInChildren<Renderer>(true))
            {
                if (r?.material != null && r.material.HasProperty("_Color"))
                {
                    Color c = r.material.color;
                    c.a = alpha;
                    r.material.color = c;
                }
            }
            foreach (var ps in effect.GetComponentsInChildren<ParticleSystem>(true))
            {
                if (ps != null)
                {
                    var main = ps.main;
                    Color sc = main.startColor.color;
                    sc.a = alpha;
                    main.startColor = sc;
                }
            }
        }

        /// <summary>
        /// VFX/버프 정리
        /// </summary>
        public static void ClearStaffDualCastBuff(Player player)
        {
            try
            {
                if (player == null) return;

                if (staffDualCastBuffEffects.TryGetValue(player, out var buffFx) && buffFx != null)
                {
                    UnityEngine.Object.Destroy(buffFx);
                    staffDualCastBuffEffects.Remove(player);
                }
                if (staffDualCastStatusEffects.TryGetValue(player, out var statusFx) && statusFx != null)
                {
                    UnityEngine.Object.Destroy(statusFx);
                    staffDualCastStatusEffects.Remove(player);
                }
                RemoveStaffDualCastBuffVFX(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[팬캐스트] 버프 정리 오류: {ex.Message}");
            }
        }

        private static void RemoveStaffDualCastBuffVFX(Player player)
        {
            try
            {
                if (staffDualCastBuffVFXInstances.TryGetValue(player, out var vfx) && vfx != null)
                    UnityEngine.Object.Destroy(vfx);
                staffDualCastBuffVFXInstances.Remove(player);
            }
            catch { }
        }

        /// <summary>
        /// 플레이어 사망 시 정리
        /// </summary>
        public static void CleanupStaffDualCastOnDeath(Player player)
        {
            try
            {
                if (_fanCastSummonCoroutines.TryGetValue(player, out var summonCo) && summonCo != null)
                    SkillTreeInputListener.Instance.StopCoroutine(summonCo);
                if (_fanCastLaunchCoroutines.TryGetValue(player, out var launchCo) && launchCo != null)
                    SkillTreeInputListener.Instance.StopCoroutine(launchCo);
                staffDualExplosionCooldowns.Remove(player); // 쿨타임 키 누수 방지
                CancelFanCast(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[팬캐스트] 사망 정리 실패: {ex.Message}");
            }
        }

        // ========== 하위 호환 스텁 (MiscActiveSkills.cs 참조용) ==========

        /// <summary>새 시스템에서는 Z키 연동 없음 → 항상 false</summary>
        public static bool IsStaffDualCastReady(Player player) => false;

        /// <summary>새 시스템에서는 Z키 연동 없음 → no-op</summary>
        public static void PerformStaffDualCastAttack(Player player, ItemDrop.ItemData weapon,
            Vector3 baseDirection, Vector3 aimSpot) { }
    }

    /// <summary>
    /// 소환된 파이어볼 플레이어 추종 컴포넌트 (Phase 1)
    /// 플레이어가 이동·회전해도 로컬 오프셋 위치를 유지
    /// </summary>
    public class StaffFanCastSuspendedBall : MonoBehaviour
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
        }
    }

    /// <summary>
    /// 이중시전 발사체 호밍 컴포넌트
    /// </summary>
    public class StaffDualCastHomingProjectile : MonoBehaviour
    {
        private Character target;
        private float homingStrength;
        private Rigidbody _rb; // Awake에서 1회 캐싱 (매 프레임 GetComponent 방지)

        public void Initialize(Character t, float strength = 5f)
        {
            target = t;
            homingStrength = strength;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // 타겟 사망/소멸 시 컴포넌트 즉시 제거 (Update 무한 실행 방지)
            if (target == null || target.IsDead())
            {
                Destroy(this);
                return;
            }
            if (_rb == null || _rb.velocity.magnitude < 0.1f) return;

            Vector3 headPos = target.GetEyePoint();
            Vector3 toTarget = (headPos - transform.position).normalized;
            Vector3 newDir = Vector3.Lerp(_rb.velocity.normalized, toTarget, Time.deltaTime * homingStrength).normalized;
            _rb.velocity = newDir * _rb.velocity.magnitude;
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
        }
    }
}

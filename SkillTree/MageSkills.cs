using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 직업 전용 액티브 스킬 및 패시브 스킬 시스템
    /// 불의 비 (Fire Rain): 전방 타겟 지정 → 좌측 상공에서 파이어볼 N개×2회 낙하 (1초 간격)
    /// </summary>
    public static class MageSkills
    {
        // 메이지 액티브 스킬 상태 관리
        private static readonly Dictionary<string, float> lastActivationTime = new Dictionary<string, float>();
        private static readonly HashSet<string> pendingExplosions = new HashSet<string>();
        // Lv2+ 연속 발사 충전 (30초 이내 추가 시전 가능)
        private static readonly Dictionary<string, float> extraChargeExpiry = new Dictionary<string, float>();

        /// <summary>메이지 스킬 등록</summary>
        public static void RegisterMageSkills()
        {
            Plugin.Log.LogInfo("[메이지 스킬] 패시브 및 액티브 스킬 등록 완료");
        }

        // 메이지 상태 캐시 (직업 변경 후 갱신 필요)
        private static readonly Dictionary<string, bool> mageStatusCache = new Dictionary<string, bool>();
        private static float lastCacheUpdate = 0f;
        private static readonly float CACHE_UPDATE_INTERVAL = 1f;

        /// <summary>메이지인지 확인 (캐시 기반)</summary>
        public static bool IsMage(Player player)
        {
            try
            {
                if (player == null) return false;
                string playerId = player.GetPlayerID().ToString();
                float currentTime = Time.time;
                if (!mageStatusCache.ContainsKey(playerId) ||
                    (currentTime - lastCacheUpdate) > CACHE_UPDATE_INTERVAL)
                {
                    var manager = SkillTreeManager.Instance;
                    bool isMageResult = manager != null && manager.GetSkillLevel("Mage") > 0;
                    mageStatusCache[playerId] = isMageResult;
                    lastCacheUpdate = currentTime;
                    Plugin.Log.LogDebug($"[메이지 스킬] 메이지 상태 캐시 갱신: {player.GetPlayerName()} = {isMageResult}");
                }
                return mageStatusCache[playerId];
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 스킬] 메이지 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>메이지 상태 캐시 강제 갱신 (직업 변경 후 호출)</summary>
        public static void RefreshMageStatusCache(Player player = null)
        {
            try
            {
                if (player != null)
                    mageStatusCache.Remove(player.GetPlayerID().ToString());
                else
                    mageStatusCache.Clear();
                lastCacheUpdate = 0f;
                Plugin.Log.LogInfo("[메이지 스킬] 메이지 상태 캐시 강제 갱신 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 스킬] 캐시 갱신 실패: {ex.Message}");
            }
        }

        /// <summary>현재 메이지 레벨 가져오기</summary>
        public static int GetMageLevel(Player player)
        {
            var manager = SkillTreeManager.Instance;
            return manager?.GetSkillLevel("Mage") ?? 0;
        }

        /// <summary>메이지 속성 저항 보너스 가져오기 (레벨 기반)</summary>
        public static float GetMageElementalResistance(Player player)
        {
            if (!IsMage(player)) return 0f;
            int level = GetMageLevel(player);
            return Mage_Config.GetElementalResistance(level) / 100f;
        }

        /// <summary>메이지 액티브 스킬 실행 (Y키) - 불의 비</summary>
        public static bool TryExecuteMageActiveSkill(Player player)
        {
            if (!IsMage(player)) return false;

            try
            {
                string playerKey = player.GetPlayerID().ToString();
                float currentTime = Time.time;
                int mageLevel = GetMageLevel(player);

                // Lv2+ 연속 발사 충전 체크
                bool hasExtraCharge = mageLevel >= 2 &&
                    extraChargeExpiry.TryGetValue(playerKey, out float expiry) &&
                    currentTime < expiry;

                if (hasExtraCharge)
                {
                    if (!IsWieldingStaff(player))
                    {
                        player.Message(MessageHud.MessageType.Center, L.Get("staff_required"));
                        return false;
                    }
                    int eitrCost2 = Mage_Config.MageEitrCostValue;
                    if (player.GetEitr() < eitrCost2)
                    {
                        player.Message(MessageHud.MessageType.Center, L.Get("eitr_insufficient", eitrCost2.ToString()));
                        return false;
                    }
                    bool fired2 = ExecuteFireRainSkill(player, mageLevel);
                    if (!fired2) return false; // 사거리 실패 → 충전 유지, 쿨타임/Eitr 소모 없음
                    extraChargeExpiry.Remove(playerKey);
                    lastActivationTime[playerKey] = currentTime;
                    player.AddEitr(-eitrCost2);
                    ActiveSkillCooldownRegistry.SetCooldown("Y", Mage_Config.GetCooldown(mageLevel));
                    Plugin.Log.LogInfo($"[불의 비] {player.GetPlayerName()} 연속 발사 충전 사용");
                    return true;
                }

                // 쿨타임 체크
                if (lastActivationTime.TryGetValue(playerKey, out float lastTime))
                {
                    float cooldownRemaining = Mage_Config.GetCooldown(mageLevel) - (currentTime - lastTime);
                    if (cooldownRemaining > 0)
                    {
                        player.Message(MessageHud.MessageType.Center, L.Get("mage_cooldown", cooldownRemaining.ToString("F1")));
                        return false;
                    }
                }

                // Eitr 체크
                int eitrCost = Mage_Config.MageEitrCostValue;
                if (player.GetEitr() < eitrCost)
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("eitr_insufficient", eitrCost.ToString()));
                    return false;
                }

                // 지팡이 착용 체크
                if (!IsWieldingStaff(player))
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("staff_required"));
                    return false;
                }

                // 스킬 실행
                bool fired = ExecuteFireRainSkill(player, mageLevel);
                if (!fired) return false; // 사거리 실패 → 쿨타임/Eitr 소모 없음

                // Lv2+이면 30초 추가 충전 부여
                if (mageLevel >= 2)
                    extraChargeExpiry[playerKey] = currentTime + 30f;

                lastActivationTime[playerKey] = currentTime;
                ActiveSkillCooldownRegistry.SetCooldown("Y", Mage_Config.GetCooldown(mageLevel));
                player.AddEitr(-eitrCost);

                Plugin.Log.LogInfo($"[불의 비] {player.GetPlayerName()} 스킬 사용 (Lv{mageLevel})");
                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[불의 비] 스킬 실행 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>지팡이 착용 여부 확인</summary>
        private static bool IsWieldingStaff(Player player)
        {
            try
            {
                var rightItem = player.GetCurrentWeapon();
                if (rightItem?.m_shared != null)
                {
                    string itemName = rightItem.m_shared.m_name?.ToLower() ?? "";
                    if (itemName.Contains("staff") || itemName.Contains("wand") ||
                        itemName.Contains("$item_staff"))
                        return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 스킬] 지팡이 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>카메라 전방 range 이내에서 가장 정렬된 적 반환</summary>
        private static Character FindTargetInFront(Player player, float range)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 camForward = GameCamera.instance != null
                ? GameCamera.instance.transform.forward
                : player.transform.forward;

            Collider[] cols = Physics.OverlapSphere(playerPos, range);
            Character bestTarget = null;
            float bestScore = float.MaxValue;

            foreach (var col in cols)
            {
                var ch = col.GetComponent<Character>() ?? col.GetComponentInParent<Character>();
                if (ch == null || ch.IsDead() || ch.IsPlayer()) continue;

                Vector3 toTarget = ch.transform.position - playerPos;
                float dot = Vector3.Dot(camForward.normalized, toTarget.normalized);
                if (dot < 0.3f) continue; // 전방 약 72도 이내만

                float score = toTarget.magnitude * (1f - dot); // 가깝고 정렬된 적 우선
                if (score < bestScore)
                {
                    bestScore = score;
                    bestTarget = ch;
                }
            }
            return bestTarget;
        }

        /// <summary>
        /// 불의 비(Fire Rain) 스킬 실행
        /// 타겟 발밑 VFX + SFX + 상공에서 파이어볼 30개 낙하
        /// </summary>
        private static bool ExecuteFireRainSkill(Player player, int mageLevel)
        {
            try
            {
                float range = Mage_Config.MageAOERangeValue;

                // 타겟 탐색
                Character target = FindTargetInFront(player, range);
                if (target == null)
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("no_targets_in_range"));
                    return false;
                }

                Vector3 targetPos = target.transform.position;

                // 캐릭터를 카메라 방향으로 회전
                if (GameCamera.instance != null)
                {
                    Vector3 camFlat = Vector3.ProjectOnPlane(
                        GameCamera.instance.transform.forward, Vector3.up);
                    if (camFlat.sqrMagnitude > 0.01f)
                        player.transform.rotation = Quaternion.LookRotation(camFlat.normalized);
                }

                // 환호 모션
                player.StartEmote("cheer", false);

                // 발밑 debuff_03 VFX (2배 크기, 2초)
                var debuff03 = VFXManager.GetVFXPrefab("debuff_03");
                if (debuff03 != null)
                {
                    var vfxGo = UnityEngine.Object.Instantiate(
                        debuff03, player.transform.position, Quaternion.identity);
                    vfxGo.transform.localScale = Vector3.one * 2f;
                    UnityEngine.Object.Destroy(vfxGo, 2f);
                }

                // 타겟 VFX + SFX 코루틴
                Plugin.Instance?.StartCoroutine(FireRainVFXAndSFXCoroutine(targetPos));

                // 파이어볼 낙하 코루틴
                Plugin.Instance?.StartCoroutine(FireRainProjectileCoroutine(player, target, mageLevel));

                // 시전 피드백
                player.Message(MessageHud.MessageType.Center,
                    L.Get("mage_firerain_cast", target.GetHoverName()));

                Plugin.Log.LogInfo($"[불의 비] {player.GetPlayerName()} → {target.GetHoverName()} 시전 (Lv{mageLevel})");
                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[불의 비] 스킬 실행 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>타겟 발밑 area_fire_red 2초 + sfx 8회 (0.25초 간격)</summary>
        private static IEnumerator FireRainVFXAndSFXCoroutine(Vector3 targetPos)
        {
            SimpleVFX.Play("area_fire_red", targetPos, 2f);

            for (int i = 0; i < 8; i++)
            {
                VFXManager.PlaySound("sfx_dverger_fireball_rain_shot", targetPos, 15f);
                yield return new WaitForSeconds(0.25f);
            }
        }

        /// <summary>타겟 좌측에서 버스트당 N개 × 2회 낙하 (0.05초 간격, 버스트 사이 1초 대기)</summary>
        private static IEnumerator FireRainProjectileCoroutine(Player player, Character target, int mageLevel)
        {
            if (player == null || target == null) yield break;

            Vector3 targetPos = target.transform.position;
            float radius = Mage_Config.MageFireRainRadiusValue;
            int countPerBurst = Mage_Config.MageFireRainProjectileCountValue;

            // 스폰 원점: 카메라 기준 타겟 좌측 + 상공 (11시 위치)
            Vector3 camRight = GameCamera.instance != null
                ? GameCamera.instance.transform.right
                : player.transform.right;
            Vector3 spawnOrigin = targetPos - camRight * 40f + Vector3.up * 40f;

            // 11시→5시 고정 방향 벡터 (모든 파이어볼 평행 낙하)
            Vector3 mainDir = (targetPos - spawnOrigin).normalized;

            // mainDir 수직 평면의 기저 벡터 (스폰 위치 분산용)
            Vector3 scatterRight = Vector3.Cross(mainDir, Vector3.up).normalized;
            if (scatterRight.magnitude < 0.1f)
                scatterRight = Vector3.Cross(mainDir, Vector3.forward).normalized;
            Vector3 scatterUp = Vector3.Cross(scatterRight, mainDir).normalized;

            // 1차 버스트
            for (int i = 0; i < countPerBurst; i++)
            {
                if (player == null || player.IsDead()) yield break;
                Vector2 rnd = UnityEngine.Random.insideUnitCircle * radius;
                Vector3 spawnPos = spawnOrigin + scatterRight * rnd.x + scatterUp * rnd.y;
                SpawnFireRainProjectile(player, spawnPos, mainDir, mageLevel);
                yield return new WaitForSeconds(0.05f);
            }

            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 2차 버스트
            for (int i = 0; i < countPerBurst; i++)
            {
                if (player == null || player.IsDead()) yield break;
                Vector2 rnd = UnityEngine.Random.insideUnitCircle * radius;
                Vector3 spawnPos = spawnOrigin + scatterRight * rnd.x + scatterUp * rnd.y;
                SpawnFireRainProjectile(player, spawnPos, mainDir, mageLevel);
                yield return new WaitForSeconds(0.05f);
            }
        }

        /// <summary>staff_fireball_projectile 단발 생성 (상공 낙하)</summary>
        private static void SpawnFireRainProjectile(Player player, Vector3 spawnPos, Vector3 dir, int mageLevel)
        {
            try
            {
                var prefab = ZNetScene.instance?.GetPrefab("staff_fireball_projectile");
                if (prefab == null)
                {
                    Plugin.Log.LogWarning("[불의 비] staff_fireball_projectile 프리팹 없음");
                    return;
                }

                var go = UnityEngine.Object.Instantiate(prefab, spawnPos, Quaternion.LookRotation(dir));

                // 데미지 처리는 OnHit 패치에서 담당
                var tag = go.AddComponent<FireRainProjectileTag>();
                tag.Owner = player;
                tag.MageLevel = mageLevel;
                tag.Processed = false;

                // Projectile: 0 데미지 설정 (패치에서 AOE 데미지 처리)
                var proj = go.GetComponent<Projectile>();
                if (proj != null)
                {
                    var hitData = new HitData();
                    hitData.m_damage = new HitData.DamageTypes();
                    hitData.m_dir = dir;
                    hitData.m_skill = Skills.SkillType.ElementalMagic;
                    hitData.m_attacker = player.GetZDOID();
                    var weapon = player.GetCurrentWeapon();
                    proj.Setup(player, dir * 60f, 0f, hitData, null, weapon);
                }

                // 물리: 중력 없이 직선 낙하
                var rb = go.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = dir * 60f;
                    rb.useGravity = false;
                    rb.isKinematic = false;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[불의 비] 발사체 스폰 실패: {ex.Message}");
            }
        }

        /// <summary>파이어볼 착지 반경 3m AOE 데미지 (패치에서 호출)</summary>
        public static void ApplyFireRainAOEDamage(Player owner, Vector3 point, int mageLevel)
        {
            try
            {
                if (owner == null) return;
                var weapon = owner.GetCurrentWeapon();
                var dmg = weapon != null ? weapon.GetDamage() : new HitData.DamageTypes();
                float pct = Mage_Config.GetDamageMultiplier(mageLevel) / 100f;
                dmg.Modify(pct);

                float impactRadius = Mage_Config.MageFireRainImpactRadiusValue;
                var cols = Physics.OverlapSphere(point, impactRadius);
                foreach (var col in cols)
                {
                    var ch = col.GetComponent<Character>() ?? col.GetComponentInParent<Character>();
                    if (ch == null || ch.IsDead() || ch.IsPlayer()) continue;

                    var hit = new HitData();
                    hit.m_damage = dmg;
                    hit.m_point = point;
                    hit.m_dir = (ch.transform.position - point).normalized;
                    hit.m_skill = Skills.SkillType.ElementalMagic;
                    hit.m_attacker = owner.GetZDOID();
                    ch.Damage(hit);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[불의 비] AOE 데미지 실패: {ex.Message}");
            }
        }

        /// <summary>특정 플레이어 상태 정리 (로그아웃/사망 시)</summary>
        public static void CleanupPlayerData(Player player)
        {
            if (player == null) return;
            string playerKey = player.GetPlayerID().ToString();
            lastActivationTime.Remove(playerKey);
            extraChargeExpiry.Remove(playerKey);
            mageStatusCache.Remove(playerKey);
        }

        /// <summary>모든 메이지 스킬 상태 정리 (플러그인 종료시)</summary>
        public static void ClearAllStates()
        {
            lastActivationTime.Clear();
            pendingExplosions.Clear();
            extraChargeExpiry.Clear();
            mageStatusCache.Clear();
            Plugin.Log.LogInfo("[메이지 스킬] 모든 상태 정리 완료 (캐시 포함)");
        }
    }

    // ================================================================
    // 불의 비 낙하 파이어볼 태그 컴포넌트
    // ================================================================
    public class FireRainProjectileTag : MonoBehaviour
    {
        public bool Processed = false;
        public Player Owner;
        public int MageLevel;
    }

    // ================================================================
    // Projectile.OnHit 패치 - 불의 비 발사체 착지 시 AOE 데미지
    // ================================================================
    [HarmonyPatch(typeof(Projectile), "OnHit",
        new System.Type[] { typeof(Collider), typeof(Vector3), typeof(bool), typeof(Vector3) })]
    [HarmonyPriority(Priority.Low)]
    public static class FireRain_ProjectileHit_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Projectile __instance,
            Collider collider, Vector3 hitPoint, bool water, Vector3 normal)
        {
            try
            {
                if (water) return;
                var tag = __instance.GetComponent<FireRainProjectileTag>();
                if (tag == null || tag.Processed) return;
                tag.Processed = true;
                MageSkills.ApplyFireRainAOEDamage(tag.Owner, hitPoint, tag.MageLevel);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[불의 비] OnHit 패치 오류: {ex.Message}");
            }
        }
    }

    // ================================================================
    // 메이지 속성 저항 패시브 스킬 패치 (기존 유지)
    // ================================================================
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class MageElementalResistancePatch
    {
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (!(__instance is Player player)) return;
                if (!MageSkills.IsMage(player)) return;

                float resistance = MageSkills.GetMageElementalResistance(player);
                if (resistance <= 0f) return;

                var originalElementalDamage = GetElementalDamage(hit);
                if (originalElementalDamage > 0f)
                {
                    float reductionMultiplier = 1f - resistance;
                    hit.m_damage.m_fire *= reductionMultiplier;
                    hit.m_damage.m_frost *= reductionMultiplier;
                    hit.m_damage.m_lightning *= reductionMultiplier;
                    hit.m_damage.m_poison *= reductionMultiplier;
                    hit.m_damage.m_spirit *= reductionMultiplier;

                    var reducedElementalDamage = GetElementalDamage(hit);
                    float damageReduced = originalElementalDamage - reducedElementalDamage;

                    if (damageReduced > 0.1f)
                    {
                        Plugin.Log.LogInfo($"[메이지 속성 저항] {player.GetPlayerName()} 속성 데미지 감소 - 원래: {originalElementalDamage:F1} → 감소후: {reducedElementalDamage:F1} (감소량: {damageReduced:F1})");
                        player.Message(MessageHud.MessageType.TopLeft, L.Get("mage_elemental_resist", damageReduced.ToString("F0")));
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 속성 저항] Damage 패치 오류: {ex.Message}");
            }
        }

        private static float GetElementalDamage(HitData hit)
        {
            return hit.m_damage.m_fire + hit.m_damage.m_frost + hit.m_damage.m_lightning +
                   hit.m_damage.m_poison + hit.m_damage.m_spirit;
        }
    }

    [HarmonyPatch(typeof(Player), "OnDestroy")]
    public static class MageSkills_Player_OnDestroy_Patch
    {
        static void Postfix(Player __instance)
        {
            if (__instance == null) return;
            try { MageSkills.CleanupPlayerData(__instance); } catch { }
        }
    }
}

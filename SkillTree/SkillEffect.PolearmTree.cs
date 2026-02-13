using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폴암 전문가 트리 전용 효과 시스템
    /// 공격 범위 보너스, 2연속 공격, G키 액티브 스킬 구현
    /// </summary>
    public static partial class SkillEffect
    {
        // === 폴암 트리 상태 추적 변수들 ===

        // 광역 강타 2연속 공격 추적
        public static Dictionary<Player, int> polearmAreaComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> polearmAreaLastHitTime = new Dictionary<Player, float>();

        // 관통 돌격 액티브 스킬 추적
        public static Dictionary<Player, float> polearmPierceChargeLastUseTime = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> polearmPierceChargeActive = new Dictionary<Player, bool>();
        public static Dictionary<Player, Coroutine> polearmPierceChargeCoroutines = new Dictionary<Player, Coroutine>();

        /// <summary>
        /// 폴암 공격 범위 보너스 계산
        /// polearm_expert (15%), polearm_step4_moon (15%)
        /// </summary>
        public static float GetTotalPolearmRangeBonus(Player player)
        {
            if (player == null) return 0f;

            var weapon = player.GetCurrentWeapon();
            if (weapon == null || !IsUsingPolearm(player)) return 0f;

            float bonus = 0f;

            // 폴암 전문가 (polearm_expert) - 공격 범위 +15%
            if (HasSkill("polearm_expert"))
            {
                bonus += SkillTreeConfig.PolearmExpertRangeBonusValue;
                Plugin.Log.LogDebug($"[폴암 전문가] 공격 범위 +{SkillTreeConfig.PolearmExpertRangeBonusValue}%");
            }

            // 반달 베기 (polearm_step4_moon) - 공격 범위 +15%
            if (HasSkill("polearm_step4_moon"))
            {
                bonus += SkillTreeConfig.PolearmStep4MoonRangeBonusValue;
                Plugin.Log.LogDebug($"[반달 베기] 공격 범위 +{SkillTreeConfig.PolearmStep4MoonRangeBonusValue}%");
            }

            return bonus;
        }

        /// <summary>
        /// 광역 강타 2연속 공격 체크 (polearm_step3_area)
        /// 2연속 공격 시 공격력 +25% (5초 지속)
        /// </summary>
        public static void CheckPolearmAreaCombo(Player player)
        {
            if (!HasSkill("polearm_step3_area")) return;

            float now = Time.time;
            if (!polearmAreaComboCount.ContainsKey(player))
                polearmAreaComboCount[player] = 0;

            // 3초 내 연속 공격 체크
            if (polearmAreaLastHitTime.ContainsKey(player) && now - polearmAreaLastHitTime[player] < 3f)
            {
                polearmAreaComboCount[player]++;
            }
            else
            {
                polearmAreaComboCount[player] = 1;
            }
            polearmAreaLastHitTime[player] = now;

            // 2연속 공격 달성 시
            if (polearmAreaComboCount[player] >= 2)
            {
                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                DrawFloatingText(player, $"⚔️ 광역 강타! 2연속 공격력 +{SkillTreeConfig.PolearmStep3AreaComboBonusValue}%");

                Plugin.Log.LogInfo($"[광역 강타] 2연속 공격 달성 - 공격력 +{SkillTreeConfig.PolearmStep3AreaComboBonusValue}% 보너스 적용");

                // 다음 공격에 보너스 적용 설정
                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1f + (SkillTreeConfig.PolearmStep3AreaComboBonusValue / 100f);
                nextAttackExpiry[player] = now + SkillTreeConfig.PolearmStep3AreaComboDurationValue;

                // 콤보 카운트 리셋
                polearmAreaComboCount[player] = 0;
            }
        }

        /// <summary>
        /// 관통 돌격 액티브 스킬 사용 (G키)
        /// 전방 5m 돌진 → 첫 몬스터 관통 타격 (+200%) → 뒤쪽 40도 AOE 넉백 (+150%)
        /// </summary>
        public static bool UsePolearmPierceChargeSkill(Player player)
        {
            if (player == null || !HasSkill("polearm_step5_king")) return false;

            float now = Time.time;

            // 쿨타임 체크
            if (polearmPierceChargeLastUseTime.ContainsKey(player))
            {
                float timeSinceLastUse = now - polearmPierceChargeLastUseTime[player];
                float cooldown = Polearm_Config.PolearmPierceChargeCooldownValue;

                if (timeSinceLastUse < cooldown)
                {
                    float remainingCooldown = cooldown - timeSinceLastUse;
                    DrawFloatingText(player, $"⏳ 쿨타임: {remainingCooldown:F1}초 남음");
                    return false;
                }
            }

            // 스태미나 체크 (고정값 20)
            float staminaCost = Polearm_Config.PolearmPierceChargeStaminaCostValue;
            if (player.GetStamina() < staminaCost)
            {
                DrawFloatingText(player, "❌ 스태미나 부족!");
                return false;
            }

            // 폴암 착용 확인
            if (!IsUsingPolearm(player))
            {
                DrawFloatingText(player, "❌ 폴암을 착용해야 합니다!");
                return false;
            }

            // 이미 스킬 실행 중인지 확인
            if (polearmPierceChargeActive.ContainsKey(player) && polearmPierceChargeActive[player])
            {
                DrawFloatingText(player, "⚠️ 관통 돌격 실행 중");
                return false;
            }

            // 스태미나 소모
            player.UseStamina(staminaCost);

            // 스킬 활성화
            polearmPierceChargeActive[player] = true;
            polearmPierceChargeLastUseTime[player] = now;

            // 코루틴 시작
            if (polearmPierceChargeCoroutines.ContainsKey(player) && polearmPierceChargeCoroutines[player] != null)
            {
                player.StopCoroutine(polearmPierceChargeCoroutines[player]);
            }

            var coroutine = ExecutePierceChargeSequence(player);
            polearmPierceChargeCoroutines[player] = player.StartCoroutine(coroutine);

            DrawFloatingText(player, "🔱 관통 돌격!");
            Plugin.Log.LogInfo($"[관통 돌격] 스킬 사용 - 돌진 거리: {Polearm_Config.PolearmPierceChargeDashDistanceValue}m");

            return true;
        }

        /// <summary>
        /// 관통 돌격 시퀀스 실행 코루틴
        /// </summary>
        private static IEnumerator ExecutePierceChargeSequence(Player player)
        {
            if (player == null || player.IsDead())
            {
                CleanupPierceCharge(player);
                yield break;
            }

            // 무기 확인
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                CleanupPierceCharge(player);
                yield break;
            }

            var weaponDamage = weapon.GetDamage();
            float dashDistance = Polearm_Config.PolearmPierceChargeDashDistanceValue;
            float moveSpeed = 35f; // 돌진 속도 (m/s) - 빠른 돌격

            // === Phase 0: 전방 몬스터 감지 및 타겟팅 ===
            Vector3 startPos = player.transform.position;
            Vector3 cameraForward = GetCameraForward(player);

            // 전방 원뿔 범위(45도) 내 몬스터 탐색
            Character targetMonster = FindMonsterInCone(player, cameraForward, dashDistance, 45f);

            Vector3 dashDir;
            float actualDashDistance = dashDistance;

            if (targetMonster != null)
            {
                // 몬스터가 있으면 그 방향으로 돌진
                Vector3 toMonster = targetMonster.transform.position - startPos;
                toMonster.y = 0;
                dashDir = toMonster.normalized;
                actualDashDistance = Mathf.Min(toMonster.magnitude + 1f, dashDistance); // 몬스터까지 거리 + 1m

                // 플레이어를 몬스터 방향으로 회전
                player.transform.rotation = Quaternion.LookRotation(dashDir);

                Plugin.Log.LogDebug($"[관통 돌격] 타겟 감지: {targetMonster.m_name}, 거리: {toMonster.magnitude:F1}m");
            }
            else
            {
                // 몬스터가 없으면 카메라 전방으로 돌진
                dashDir = cameraForward;
            }

            // === Phase 1: 전방 돌진 (몬스터 충돌 시 중단) ===
            Character hitMonster = null;
            float traveled = 0f;
            float duration = actualDashDistance / moveSpeed;
            float elapsed = 0f;

            while (elapsed < duration && player != null && !player.IsDead())
            {
                elapsed += Time.deltaTime;
                float step = moveSpeed * Time.deltaTime;
                Vector3 newPos = player.transform.position + dashDir * step;
                newPos = GetGroundPosition(newPos);
                player.transform.position = newPos;
                traveled += step;

                // 충돌 감지 (1.5m 반경)
                hitMonster = FindNearestMonsterInRadius(player, 1.5f);
                if (hitMonster != null)
                {
                    Plugin.Log.LogDebug($"[관통 돌격] 몬스터 충돌 감지: {hitMonster.m_name}");
                    break; // 돌진 중단
                }

                yield return null;
            }

            if (player == null || player.IsDead())
            {
                CleanupPierceCharge(player);
                yield break;
            }

            // === Phase 2: 첫 관통 타격 ===
            if (hitMonster != null && !hitMonster.IsDead())
            {
                // 타겟 방향으로 회전
                Vector3 lookDir = (hitMonster.transform.position - player.transform.position);
                lookDir.y = 0;
                if (lookDir.sqrMagnitude > 0.001f)
                    player.transform.rotation = Quaternion.LookRotation(lookDir.normalized);

                // 일반 공격 실행 - Attack 직접 트리거
                TriggerMeleeAttack(player, weapon);

                // 첫 관통 데미지 (+200%)
                float primaryDamageMultiplier = 1f + (Polearm_Config.PolearmPierceChargePrimaryDamageValue / 100f);

                var hit = new HitData();
                hit.m_damage.m_slash = weaponDamage.m_slash * primaryDamageMultiplier;
                hit.m_damage.m_blunt = weaponDamage.m_blunt * primaryDamageMultiplier;
                hit.m_damage.m_pierce = weaponDamage.m_pierce * primaryDamageMultiplier;

                hit.m_point = hitMonster.GetCenterPoint();
                hit.m_dir = dashDir;
                hit.m_attacker = player.GetZDOID();
                hit.SetAttacker(player);
                hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                // 첫 몬스터 넉백 적용
                float knockbackForce = Polearm_Config.PolearmPierceChargeKnockbackDistanceValue;
                Vector3 primaryKnockbackDir = (hitMonster.transform.position - player.transform.position).normalized;
                hit.m_pushForce = knockbackForce * 3f; // HitData의 pushForce로 넉백

                hitMonster.Damage(hit);

                // 첫 몬스터 스태거 + 물리적 넉백
                hitMonster.Stagger(primaryKnockbackDir);
                var monsterBody = hitMonster.GetComponent<Rigidbody>();
                if (monsterBody != null)
                {
                    monsterBody.AddForce(primaryKnockbackDir * knockbackForce * 10f, ForceMode.Impulse);
                    Plugin.Log.LogDebug($"[관통 돌격] 첫 몬스터 Rigidbody 넉백 적용: {knockbackForce * 10f}");
                }

                // VFX: 첫 관통 (커스텀 VFX - SimpleVFX)
                SimpleVFX.Play("confetti_blast_multicolor", hitMonster.GetCenterPoint(), 2f);

                DrawFloatingText(player, $"💥 관통! (+{Polearm_Config.PolearmPierceChargePrimaryDamageValue}%)");
                Plugin.Log.LogInfo($"[관통 돌격] 첫 타격 성공 - 데미지 +{Polearm_Config.PolearmPierceChargePrimaryDamageValue}%");

                yield return new WaitForSeconds(0.2f);

                // === Phase 3: 후방 AOE 넉백 ===
                if (player != null && !player.IsDead() && hitMonster != null && !hitMonster.IsDead())
                {
                    Vector3 monsterPos = hitMonster.transform.position;
                    Vector3 knockbackDir = dashDir; // 플레이어 → 몬스터 방향 = 넉백 방향
                    float aoeRadius = Polearm_Config.PolearmPierceChargeAoeRadiusValue;
                    float halfAngle = Polearm_Config.PolearmPierceChargeAoeAngleValue / 2f;

                    // AOE 범위 내 적 탐색 (첫 타격 몬스터 뒤쪽)
                    var aoeTargets = GetEnemiesInConeArea(monsterPos, knockbackDir, aoeRadius, halfAngle, player, hitMonster);

                    if (aoeTargets.Count > 0)
                    {
                        float aoeDamageMultiplier = 1f + (Polearm_Config.PolearmPierceChargeAoeDamageValue / 100f);
                        float knockbackDistance = Polearm_Config.PolearmPierceChargeKnockbackDistanceValue;

                        foreach (var enemy in aoeTargets)
                        {
                            if (enemy == null || enemy.IsDead()) continue;

                            // AOE 데미지 (+150%)
                            var aoeHit = new HitData();
                            aoeHit.m_damage.m_slash = weaponDamage.m_slash * aoeDamageMultiplier;
                            aoeHit.m_damage.m_blunt = weaponDamage.m_blunt * aoeDamageMultiplier;
                            aoeHit.m_damage.m_pierce = weaponDamage.m_pierce * aoeDamageMultiplier;

                            // 넉백 방향 계산 (몬스터 위치 기준)
                            Vector3 enemyKnockDir = (enemy.transform.position - monsterPos).normalized;
                            if (enemyKnockDir.sqrMagnitude < 0.001f)
                                enemyKnockDir = knockbackDir; // 같은 위치면 기본 방향 사용

                            aoeHit.m_point = enemy.GetCenterPoint();
                            aoeHit.m_dir = enemyKnockDir;
                            aoeHit.m_pushForce = knockbackDistance * 3f; // 넉백 거리를 힘으로 변환
                            aoeHit.m_attacker = player.GetZDOID();
                            aoeHit.SetAttacker(player);
                            aoeHit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                            enemy.Damage(aoeHit);

                            // 스태거 + 물리적 넉백 적용
                            enemy.Stagger(enemyKnockDir);
                            var rb = enemy.GetComponent<Rigidbody>();
                            if (rb != null)
                            {
                                rb.AddForce(enemyKnockDir * knockbackDistance * 10f, ForceMode.Impulse);
                            }

                            // VFX: AOE 넉백 (발헤임 기본 VFX - VFXManager) - fx_crit 사용
                            VFXManager.PlayVFXMultiplayer("fx_crit", "", enemy.GetCenterPoint(), Quaternion.identity, 2f);
                        }

                        DrawFloatingText(player, $"⚡ AOE 넉백! ({aoeTargets.Count}명, +{Polearm_Config.PolearmPierceChargeAoeDamageValue}%)");
                        Plugin.Log.LogInfo($"[관통 돌격] AOE 넉백 - {aoeTargets.Count}명 타격");
                    }
                }
            }
            else
            {
                // 몬스터 충돌 없이 돌진 완료
                DrawFloatingText(player, "🔱 돌진 완료 (타격 없음)");
            }

            // 상태 정리
            CleanupPierceCharge(player);
            yield return null;
        }

        /// <summary>
        /// 카메라 기준 전방 방향 (Y축 무시)
        /// </summary>
        private static Vector3 GetCameraForward(Player player)
        {
            if (Camera.main != null)
            {
                Vector3 forward = Camera.main.transform.forward;
                forward.y = 0;
                return forward.normalized;
            }
            return player.transform.forward;
        }

        /// <summary>
        /// 지면 높이 보정 (Raycast)
        /// </summary>
        private static Vector3 GetGroundPosition(Vector3 pos)
        {
            if (Physics.Raycast(pos + Vector3.up * 5f, Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("terrain", "Default")))
            {
                return new Vector3(pos.x, hit.point.y + 0.1f, pos.z);
            }
            return pos;
        }

        /// <summary>
        /// 반경 내 가장 가까운 몬스터 탐색
        /// </summary>
        private static Character FindNearestMonsterInRadius(Player player, float radius)
        {
            if (player == null) return null;

            Character nearest = null;
            float minDist = radius;

            foreach (var c in Character.GetAllCharacters())
            {
                if (c == null || c.IsDead() || !c.IsMonsterFaction(Time.time)) continue;

                float dist = Vector3.Distance(c.transform.position, player.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = c;
                }
            }

            return nearest;
        }

        /// <summary>
        /// 전방 원뿔 범위 내 가장 가까운 몬스터 탐색 (돌진 타겟팅용)
        /// </summary>
        private static Character FindMonsterInCone(Player player, Vector3 direction, float maxDistance, float coneAngle)
        {
            if (player == null) return null;

            Character nearest = null;
            float minDist = maxDistance;
            float halfAngle = coneAngle / 2f;

            foreach (var c in Character.GetAllCharacters())
            {
                if (c == null || c.IsDead() || !c.IsMonsterFaction(Time.time)) continue;

                Vector3 toMonster = c.transform.position - player.transform.position;
                toMonster.y = 0;
                float dist = toMonster.magnitude;

                if (dist > maxDistance || dist < 0.5f) continue; // 너무 멀거나 너무 가까우면 제외

                // 각도 체크
                float angle = Vector3.Angle(direction, toMonster.normalized);
                if (angle > halfAngle) continue;

                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = c;
                }
            }

            return nearest;
        }

        /// <summary>
        /// 근접 공격 트리거 (일반 공격 애니메이션 실행)
        /// </summary>
        private static void TriggerMeleeAttack(Player player, ItemDrop.ItemData weapon)
        {
            try
            {
                if (player == null || weapon == null) return;

                // 방법 1: Humanoid.StartAttack 사용 (가장 안정적)
                // secondaryAttack = false (일반 공격)
                bool attackStarted = player.StartAttack(null, false);
                if (attackStarted)
                {
                    Plugin.Log.LogDebug("[관통 돌격] Humanoid.StartAttack() 성공");
                    return;
                }

                // 방법 2: m_attack 필드를 리플렉션으로 접근해서 트리거
                var attackField = typeof(Humanoid).GetField("m_attack",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (attackField != null)
                {
                    var currentAttack = attackField.GetValue(player) as Attack;
                    if (currentAttack != null)
                    {
                        // 이미 진행 중인 공격이 있으면 성공으로 간주
                        Plugin.Log.LogDebug("[관통 돌격] 기존 Attack 진행 중");
                        return;
                    }
                }

                // 방법 3: Animator로 직접 공격 애니메이션 트리거
                var animator = player.GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    // Valheim의 공격 애니메이션 파라미터
                    animator.SetTrigger("swing_longsword");
                    Plugin.Log.LogDebug("[관통 돌격] Animator swing_longsword 트리거 실행");
                    return;
                }

                // 방법 4: ZSyncAnimation 사용
                var zsync = player.GetComponentInChildren<ZSyncAnimation>();
                if (zsync != null)
                {
                    zsync.SetTrigger("swing_longsword");
                    Plugin.Log.LogDebug("[관통 돌격] ZSyncAnimation swing_longsword 트리거 실행");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[관통 돌격] 근접 공격 트리거 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 원뿔 범위 내 적 탐색 (첫 타격 몬스터 뒤쪽)
        /// </summary>
        private static List<Character> GetEnemiesInConeArea(Vector3 origin, Vector3 direction, float radius, float halfAngle, Player player, Character excludeTarget)
        {
            var enemies = new List<Character>();

            foreach (var c in Character.GetAllCharacters())
            {
                if (c == null || c.IsDead() || c == player || c == excludeTarget) continue;
                if (!c.IsMonsterFaction(Time.time)) continue;

                float dist = Vector3.Distance(c.transform.position, origin);
                if (dist > radius) continue;

                // 각도 체크 (방향 기준)
                Vector3 toEnemy = (c.transform.position - origin).normalized;
                float angle = Vector3.Angle(direction, toEnemy);

                if (angle <= halfAngle)
                {
                    enemies.Add(c);
                }
            }

            return enemies;
        }

        /// <summary>
        /// 관통 돌격 상태 정리
        /// </summary>
        private static void CleanupPierceCharge(Player player)
        {
            if (player != null && polearmPierceChargeActive.ContainsKey(player))
            {
                polearmPierceChargeActive[player] = false;
            }
        }

        /// <summary>
        /// 관통 돌격 활성 상태 확인
        /// </summary>
        public static bool IsPolearmPierceChargeActive(Player player)
        {
            return polearmPierceChargeActive.TryGetValue(player, out bool active) && active;
        }

        /// <summary>
        /// 반달 베기 스태미나 감소 (polearm_step4_moon)
        /// 공격 스태미나 소모 -15%
        /// </summary>
        public static float GetPolearmStaminaReduction()
        {
            if (HasSkill("polearm_step4_moon"))
            {
                return SkillTreeConfig.PolearmStep4MoonStaminaReductionValue;
            }

            return 0f;
        }

        /// <summary>
        /// 휠 마우스(특수공격) 데미지 보너스 계산
        /// 회전베기 (polearm_step1_spin) +60%
        /// 지면 강타 (polearm_step3_ground) +80%
        /// </summary>
        public static float GetPolearmWheelDamageBonus()
        {
            float bonus = 0f;

            // 회전베기 - 휠 마우스 공격력 +60%
            if (HasSkill("polearm_step1_spin"))
            {
                bonus += SkillTreeConfig.PolearmStep1SpinWheelDamageValue;
                Plugin.Log.LogDebug($"[회전베기] 휠 공격력 +{SkillTreeConfig.PolearmStep1SpinWheelDamageValue}%");
            }

            // 지면 강타 - 휠 마우스 공격력 +80%
            if (HasSkill("polearm_step3_ground"))
            {
                bonus += SkillTreeConfig.PolearmStep3GroundWheelDamageValue;
                Plugin.Log.LogDebug($"[지면 강타] 휠 공격력 +{SkillTreeConfig.PolearmStep3GroundWheelDamageValue}%");
            }

            return bonus;
        }

        /// <summary>
        /// 영웅 타격 넉백/스태거 확률 (polearm_step2_hero)
        /// 27% 확률로 적을 스태거
        /// </summary>
        public static float GetPolearmHeroKnockbackChance()
        {
            if (HasSkill("polearm_step2_hero"))
            {
                return SkillTreeConfig.PolearmStep2HeroKnockbackChanceValue;
            }
            return 0f;
        }
    }

    /// <summary>
    /// 폴암 공격 범위 보너스 패치
    /// Attack.Start() Postfix - m_attackRange 필드 직접 수정
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class Attack_Start_PolearmRange_Patch
    {
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 폴암 공격 범위 보너스 계산
                float rangeBonus = SkillEffect.GetTotalPolearmRangeBonus(player);

                if (rangeBonus > 0f)
                {
                    float originalRange = __instance.m_attackRange;
                    __instance.m_attackRange *= (1f + (rangeBonus / 100f));

                    Plugin.Log.LogDebug($"[폴암 공격 범위] {originalRange:F2} → {__instance.m_attackRange:F2} (+{rangeBonus}%)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmRange_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 폴암 스태미나 감소 패치
    /// Attack.Start() Postfix - m_attackStamina 필드 직접 수정
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class Attack_Start_PolearmStamina_Patch
    {
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 반달 베기 스태미나 감소
                float staminaReduction = SkillEffect.GetPolearmStaminaReduction();

                if (staminaReduction > 0f)
                {
                    float originalStamina = __instance.m_attackStamina;
                    __instance.m_attackStamina *= (1f - (staminaReduction / 100f));

                    Plugin.Log.LogDebug($"[반달 베기] 스태미나 소모 {originalStamina:F1} → {__instance.m_attackStamina:F1} (-{staminaReduction}%)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmStamina_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 영웅 타격 스태거/넉백 효과 패치
    /// polearm_step2_hero: 27% 확률로 적을 스태거
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_PolearmHeroKnockback_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.Low)]
        static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null || __instance.IsDead()) return;
                if (__instance.IsPlayer()) return; // 플레이어는 제외

                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 영웅 타격 스킬 확인
                float knockbackChance = SkillEffect.GetPolearmHeroKnockbackChance();
                if (knockbackChance <= 0f) return;

                // 확률 체크
                if (UnityEngine.Random.value * 100f > knockbackChance) return;

                // 스태거 적용
                Vector3 knockbackDir = (__instance.transform.position - player.transform.position).normalized;
                __instance.Stagger(knockbackDir);

                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                SkillEffect.DrawFloatingText(player, $"⚔️ 영웅 타격! (스태거)");
                Plugin.Log.LogInfo($"[영웅 타격] 스태거 발동 - {__instance.name}");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_PolearmHeroKnockback_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 폴암 휠 마우스(특수 공격) 데미지 보너스 패치
    /// Attack.Start에서 특수 공격 감지 후 보너스 준비
    /// 회전베기 (polearm_step1_spin) +60%
    /// 지면 강타 (polearm_step3_ground) +80%
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class Attack_Start_PolearmWheelDetect_Patch
    {
        // 마지막 특수 공격 시간 추적
        private static Dictionary<Player, float> lastSecondaryAttackTime = new Dictionary<Player, float>();

        [HarmonyPriority(HarmonyLib.Priority.High)]
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 휠 마우스 보너스 계산
                float wheelBonus = SkillEffect.GetPolearmWheelDamageBonus();
                if (wheelBonus <= 0f) return;

                // 특수 공격(휠 마우스/가운데 버튼) 체크
                // Valheim에서 Secondary Attack은 마우스 가운데 버튼 또는 특정 키
                bool isSecondaryAttack = ZInput.GetButton("SecondaryAttack") || Input.GetMouseButton(2);

                if (isSecondaryAttack)
                {
                    // 특수 공격 시간 기록 (Character.Damage에서 확인용)
                    lastSecondaryAttackTime[player] = Time.time;

                    Plugin.Log.LogDebug($"[폴암 휠 공격] 특수 공격 감지 - 보너스 +{wheelBonus}% 준비");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmWheelDetect_Patch] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 최근 특수 공격 여부 확인 (0.5초 이내)
        /// </summary>
        public static bool IsRecentSecondaryAttack(Player player)
        {
            if (player == null) return false;
            if (!lastSecondaryAttackTime.ContainsKey(player)) return false;
            return Time.time - lastSecondaryAttackTime[player] < 0.5f;
        }

        /// <summary>
        /// 정리
        /// </summary>
        public static void Cleanup(Player player)
        {
            if (player != null)
            {
                lastSecondaryAttackTime.Remove(player);
            }
        }
    }

    /// <summary>
    /// 폴암 휠 마우스 데미지 적용 패치
    /// Character.Damage에서 최종 데미지에 휠 보너스 적용
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_PolearmWheelDamage_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.High)]
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null) return;
                if (__instance.IsPlayer()) return; // 플레이어는 제외

                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 최근 특수 공격인지 확인
                if (!Attack_Start_PolearmWheelDetect_Patch.IsRecentSecondaryAttack(player)) return;

                // 휠 마우스 보너스 계산
                float wheelBonus = SkillEffect.GetPolearmWheelDamageBonus();
                if (wheelBonus <= 0f) return;

                // 물리 데미지에 보너스 적용
                float multiplier = 1f + (wheelBonus / 100f);
                hit.m_damage.m_slash *= multiplier;
                hit.m_damage.m_pierce *= multiplier;
                hit.m_damage.m_blunt *= multiplier;

                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                SkillEffect.DrawFloatingText(player, $"🌀 휠 공격 +{wheelBonus}%!");
                Plugin.Log.LogInfo($"[폴암 휠 공격] 데미지 보너스 +{wheelBonus}% 적용");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_PolearmWheelDamage_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 폴암 스킬 정리 (Player 사망/로그아웃 시)
    /// </summary>
    public static partial class SkillEffect
    {
        public static void CleanupPolearmSkillsOnDeath(Player player)
        {
            try
            {
                polearmAreaComboCount.Remove(player);
                polearmAreaLastHitTime.Remove(player);

                // 관통 돌격 상태 정리
                polearmPierceChargeLastUseTime.Remove(player);
                polearmPierceChargeActive.Remove(player);

                if (polearmPierceChargeCoroutines.ContainsKey(player) && polearmPierceChargeCoroutines[player] != null)
                {
                    try
                    {
                        player.StopCoroutine(polearmPierceChargeCoroutines[player]);
                    }
                    catch { }
                    polearmPierceChargeCoroutines.Remove(player);
                }

                Attack_Start_PolearmWheelDetect_Patch.Cleanup(player);

                Plugin.Log.LogDebug("[폴암 스킬] 플레이어 상태 정리 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[폴암 스킬] 정리 실패: {ex.Message}");
            }
        }
    }
}

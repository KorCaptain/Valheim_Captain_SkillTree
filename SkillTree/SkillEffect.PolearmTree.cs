using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

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

        // Berserker Lv2 관통 돌격 추가 사용 창
        private static Dictionary<Player, float> _pierceChargePendingWindow = new Dictionary<Player, float>();
        private const float PierceChargeExtraWindow = 30f;

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

            // 제작 축복: 폴암 사거리 마법부여
            if (ProducerCrafting.GetEnchantType(weapon) == ProducerCrafting.EnchantType.PolearmRange)
                bonus += ProducerCrafting.GetEnchantValue(weapon);

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
            if (polearmAreaLastHitTime.TryGetValue(player, out float paLastHit) && now - paLastHit < 3f)
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
                DrawFloatingText(player, "⚔️ " + L.Get("polearm_area_combo", SkillTreeConfig.PolearmStep3AreaComboBonusValue));


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
            bool hasBerserkerLv2_pierce = (SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 0) >= 2;
            bool hasTankerLv2_pierce = (SkillTreeManager.Instance?.GetSkillLevel("Tanker") ?? 0) >= 2;
            bool hasExtraPierce = hasBerserkerLv2_pierce || hasTankerLv2_pierce;
            bool inPierceWindow = hasExtraPierce
                && _pierceChargePendingWindow.TryGetValue(player, out float pierceWinExpiry)
                && now <= pierceWinExpiry;

            // 쿨타임 체크 (추가 사용 창 내에서는 쿨타임 무시)
            if (!inPierceWindow && polearmPierceChargeLastUseTime.TryGetValue(player, out float ppLastUse))
            {
                float timeSinceLastUse = now - ppLastUse;
                float cooldown = Polearm_Config.PolearmPierceChargeCooldownValue;

                if (timeSinceLastUse < cooldown)
                {
                    float remainingCooldown = cooldown - timeSinceLastUse;
                    DrawFloatingText(player, "⏳ " + L.Get("polearm_cooldown_remaining", $"{remainingCooldown:F1}"));
                    return false;
                }
            }

            // 스태미나 체크 (고정값 20)
            float staminaCost = Polearm_Config.PolearmPierceChargeStaminaCostValue;
            if (player.GetStamina() < staminaCost)
            {
                DrawFloatingText(player, "❌ " + L.Get("polearm_stamina_insufficient"));
                return false;
            }

            // 폴암 착용 확인
            if (!IsUsingPolearm(player))
            {
                DrawFloatingText(player, "❌ " + L.Get("polearm_required"));
                return false;
            }

            // 타겟 탐색 (쿨타임·스태미나 소모 전)
            Character pierceTarget = FindPierceChargeTarget(player);
            if (pierceTarget == null)
            {
                DrawFloatingText(player, "⚠️ " + L.Get("shield_no_target"));
                return false;
            }

            // 경로 장애물 확인 (쿨타임·스태미나 소모 전)
            if (IsPierceChargePathBlocked(player, pierceTarget))
            {
                DrawFloatingText(player, "🚫 " + L.Get("shield_path_blocked"));
                return false;
            }

            // 이미 스킬 실행 중인지 확인
            if (polearmPierceChargeActive.TryGetValue(player, out bool ppActive) && ppActive)
            {
                DrawFloatingText(player, "⚠️ " + L.Get("pierce_charge_in_progress"));
                return false;
            }

            // 스태미나 소모
            player.UseStamina(staminaCost);

            // 스킬 활성화
            polearmPierceChargeActive[player] = true;

            TankerPrereqLastUsedTime = Time.time;

            // Berserker/Tanker Lv2 추가 사용 창 분기
            if (hasExtraPierce && !inPierceWindow)
            {
                // 1번째 사용: 쿨타임 보류 + 30초 창 오픈
                _pierceChargePendingWindow[player] = now + PierceChargeExtraWindow;
                player.StartCoroutine(ExpirePierceChargeWindow(player));
            }
            else
            {
                // 2번째 사용(창 내) or 비버서커: 쿨타임 즉시 시작
                _pierceChargePendingWindow.Remove(player);
                polearmPierceChargeLastUseTime[player] = now;
                ActiveSkillCooldownRegistry.SetCooldownForSkill("G", "polearm_step5_king", Polearm_Config.PolearmPierceChargeCooldownValue);
            }

            // 코루틴 시작
            if (polearmPierceChargeCoroutines.TryGetValue(player, out var ppPrevCoroutine) && ppPrevCoroutine != null)
            {
                player.StopCoroutine(ppPrevCoroutine);
            }

            var coroutine = ExecutePierceChargeSequence(player, pierceTarget);
            polearmPierceChargeCoroutines[player] = player.StartCoroutine(coroutine);

            DrawFloatingText(player, "🔱 " + L.Get("pierce_charge"));
            Plugin.Log.LogInfo($"[관통 돌격] 스킬 사용 - 돌진 거리: {Polearm_Config.PolearmPierceChargeDashDistanceValue}m");

            return true;
        }

        private static IEnumerator ExecutePierceChargeSequence(Player player, Character target)
        {
            try
            {
                if (player == null || player.IsDead())
                {
                    yield break;
                }

                var weapon = player.GetCurrentWeapon();
                if (weapon == null)
                {
                    yield break;
                }

                float dashDistance = Polearm_Config.PolearmPierceChargeDashDistanceValue;
                float dashDuration = 0.35f;

                // === Phase 0: 타겟 기준 3D 방향 설정 (공중 직선 이동) ===
                Vector3 startPos = player.transform.position;
                Vector3 dashDir3D = (target.GetCenterPoint() - player.GetCenterPoint()).normalized;
                Vector3 dashDirH = new Vector3(dashDir3D.x, 0f, dashDir3D.z);
                if (dashDirH.sqrMagnitude > 0.01f) dashDirH.Normalize();
                else dashDirH = player.GetLookDir();

                Vector3 endPos = startPos + dashDir3D * dashDistance;

                // 오브젝트/던전 벽 충돌 체크 (통과 방지) — 휠윈드와 동일 로직 재사용
                endPos = ClampWhirlwindEndToObstacle(startPos, endPos);
                // 던전 출구 트리거(TeleportWorld) 통과 방지 — 휠윈드와 동일 로직 재사용
                endPos = ClampAwayFromTeleportTrigger(startPos, endPos);

                Plugin.Log.LogDebug($"[관통 돌격] 공중 직선 돌진 시작 - 거리: {dashDistance}m, 타겟: {target.name}");

                SetPlayerAttackSpeedBoost(player, 8.0f);

                var rigidbody = player.GetComponent<Rigidbody>();

                // === Phase 1: 공중 직선 돌진 + 거리 판정 적중 ===
                float elapsed = 0f;
                Character hitMonster = null;
                float knockbackDistance = Polearm_Config.PolearmPierceChargeKnockbackDistanceValue;
                Vector3 finalPos = startPos;

                TriggerMeleeAttack(player, weapon);

                while (elapsed < dashDuration && player != null && !player.IsDead())
                {
                    elapsed += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsed / dashDuration);
                    float easedT = 1f - Mathf.Pow(1f - t, 2f);

                    Vector3 newPos = Vector3.Lerp(startPos, endPos, easedT);

                    // 지형 아래 꺼짐 방지 (공중 이동이지만 땅 밑으로는 안 꺼짐)
                    if (Physics.Raycast(newPos + Vector3.up * 5f, Vector3.down, out RaycastHit groundHit, 10f,
                        LayerMask.GetMask("terrain", "static_solid")))
                    {
                        float terrainY = groundHit.point.y + 0.3f;
                        if (newPos.y < terrainY) newPos.y = terrainY;
                    }

                    // 캐릭터 회전 동기화
                    if (dashDirH != Vector3.zero)
                    {
                        player.transform.rotation = Quaternion.LookRotation(dashDirH);
                        HarmonyLib.Traverse.Create(player).Field("m_lookDir").SetValue(dashDirH);
                    }

                    if (rigidbody != null)
                    {
                        rigidbody.MovePosition(newPos);
                    }
                    player.transform.position = newPos;
                    finalPos = newPos;

                    // 적중 판정: 타겟과 3m 이내
                    if (target != null && !target.IsDead())
                    {
                        float distNow = Vector3.Distance(player.transform.position, target.transform.position);
                        if (distNow <= 3f)
                            hitMonster = target;
                    }

                    if (hitMonster != null)
                    {
                        Plugin.Log.LogDebug($"[관통 돌격] 첫 몬스터 적중! - 돌진 멈춤");
                        finalPos = player.transform.position;

                        int pierceLevel = SkillTreeManager.Instance?.GetSkillLevel("polearm_step5_king") ?? 1;
                        float pierceBonus = (pierceLevel - 1) * Polearm_Config.PolearmPierceChargeLevelBonusValue;
                        float damageMultiplier = 1f + ((Polearm_Config.PolearmPierceChargePrimaryDamageValue + pierceBonus) / 100f);
                        float pierceSkillFactor = player.GetSkillFactor(Skills.SkillType.Polearms);
                        var weaponDamage = weapon.GetDamage(0, pierceSkillFactor);

                        var hit = new HitData();
                        hit.m_damage.m_slash = weaponDamage.m_slash * damageMultiplier;
                        hit.m_damage.m_blunt = weaponDamage.m_blunt * damageMultiplier;
                        hit.m_damage.m_pierce = weaponDamage.m_pierce * damageMultiplier;

                        Vector3 knockDir = (hitMonster.transform.position - player.transform.position).normalized;

                        hit.m_point = hitMonster.GetCenterPoint();
                        hit.m_dir = knockDir;
                        hit.m_pushForce = knockbackDistance * 2f;
                        hit.m_attacker = player.GetZDOID();
                        hit.SetAttacker(player);
                        hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                        hitMonster.Damage(hit);
                        hitMonster.Stagger(knockDir);
                        hitMonster.transform.position = ResolveKnockbackDestination(hitMonster.transform.position, knockDir, knockbackDistance);

                        VFXManager.PlayVFXMultiplayer("fx_crit", "", hitMonster.GetCenterPoint(), Quaternion.identity, 2f);
                        SimpleVFX.Play("confetti_blast_multicolor", hitMonster.GetCenterPoint(), 2f);

                        ApplyAreaKnockback(player, hitMonster, weapon, knockbackDistance);

                        DrawFloatingText(player, "💥 " + L.Get("pierce_charge_damage", Polearm_Config.PolearmPierceChargePrimaryDamageValue + pierceBonus));

                        break;
                    }

                    yield return null;
                }

                if (player == null || player.IsDead())
                {
                    yield break;
                }

                if (rigidbody != null)
                {
                    if (!rigidbody.isKinematic) rigidbody.velocity = Vector3.zero;
                    rigidbody.MovePosition(finalPos);
                }
                player.transform.position = finalPos;

                if (hitMonster == null)
                {
                    DrawFloatingText(player, "🔱 " + L.Get("charge_complete"));
                }

                yield return new WaitForSeconds(0.1f);

                if (player != null && rigidbody != null)
                {
                    rigidbody.MovePosition(finalPos);
                    player.transform.position = finalPos;
                }
            }
            finally
            {
                // 예외/중단 여부와 무관하게 공격속도 부스트 원복 및 "실행 중" 플래그 리셋을 보장
                SetPlayerAttackSpeedBoost(player, 1.0f);
                CleanupPierceCharge(player);
            }
        }

        /// <summary>
        /// 반경 내 모든 적 탐색 (각도 제한 없음)
        /// </summary>
        private static List<Character> GetAllEnemiesInRadius(Vector3 center, float radius, Player excludePlayer)
        {
            var enemies = new List<Character>();

            foreach (var c in Character.GetAllCharacters())
            {
                if (c == null || c.IsDead() || c == excludePlayer) continue;
                if (!c.IsMonsterFaction(Time.time) && !c.IsBoss()) continue;

                float dist = Vector3.Distance(c.transform.position, center);
                if (dist <= radius)
                {
                    enemies.Add(c);
                }
            }

            return enemies;
        }

        /// <summary>
        /// 플레이어 위치 중심 반경 5m 내 모든 몬스터 넉백 (첫 몬스터 제외)
        /// </summary>
        private static void ApplyAreaKnockback(Player player, Character firstMonster, ItemDrop.ItemData weapon, float knockbackForce)
        {
            if (player == null) return;

            // 플레이어 위치 기준 (더 직관적)
            Vector3 playerPos = player.transform.position;
            float aoeRadius = Polearm_Config.PolearmPierceChargeAoeRadiusValue; // Config에서 AOE 반경 가져오기
            float aoeAngle = Polearm_Config.PolearmPierceChargeAoeAngleValue; // Config에서 AOE 각도 가져오기 (280도)
            float includeHalfAngle = aoeAngle / 2f; // 포함할 전방 반각 (140도) - 앞쪽 280도 범위
            int pierceLevelAoe = SkillTreeManager.Instance?.GetSkillLevel("polearm_step5_king") ?? 1;
            float aoePercent = pierceLevelAoe switch {
                2 => 175f, 3 => 200f, 4 => 225f,
                5 => 250f, 6 => 275f, 7 => 300f,
                _ => 150f
            };
            float aoeDamageMultiplier = 1f + (aoePercent / 100f);
            float aoeSkillFactor = player.GetSkillFactor(Skills.SkillType.Polearms);
            var weaponDamage = weapon.GetDamage(0, aoeSkillFactor);

            // 플레이어 전방 방향
            Vector3 playerForward = player.transform.forward;
            playerForward.y = 0;
            playerForward.Normalize();

            int knockbackCount = 0;
            int totalMonsters = 0;

            Plugin.Log.LogDebug($"[관통 돌격 AOE] 플레이어 위치: {playerPos}, 반경: {aoeRadius}m, 전방 {aoeAngle}도 범위");

            foreach (var enemy in Character.GetAllCharacters())
            {
                if (enemy == null || enemy.IsDead() || enemy == player) continue;
                if (!enemy.IsMonsterFaction(Time.time) && !enemy.IsBoss())
                {
                    var tp = enemy as Player;
                    if (tp == null || !tp.IsPVPEnabled() || !player.IsPVPEnabled()) continue;
                }

                // 첫 몬스터는 이미 처리했으므로 제외
                if (firstMonster != null && enemy == firstMonster) continue;

                totalMonsters++;
                float dist = Vector3.Distance(enemy.transform.position, playerPos);

                if (dist > aoeRadius) continue;

                // 플레이어 → 몬스터 방향
                Vector3 toEnemy = (enemy.transform.position - playerPos);
                toEnemy.y = 0;
                toEnemy.Normalize();

                // 전방 방향과의 각도 계산
                float angleToEnemy = Vector3.Angle(playerForward, toEnemy);

                // 280도 범위: 전방 280도(양쪽 140도) 포함 = 각도가 140도 이하인 적만 타격
                if (angleToEnemy > includeHalfAngle)
                {
                    Plugin.Log.LogDebug($"[관통 돌격 AOE] {enemy.name} 제외 - 후방 {angleToEnemy:F1}도 (범위 밖)");
                    continue;
                }

                Plugin.Log.LogDebug($"[관통 돌격 AOE] 몬스터: {enemy.name}, 거리: {dist:F2}m, 각도: {angleToEnemy:F1}도 (전방 범위)");

                // 넉백 방향 (플레이어 → 몬스터 = 바깥으로 밀림)
                Vector3 knockDir = toEnemy;
                if (knockDir.sqrMagnitude < 0.001f)
                    knockDir = player.transform.forward;

                // AOE 데미지 적용
                var aoeHit = new HitData();
                aoeHit.m_damage.m_slash = weaponDamage.m_slash * aoeDamageMultiplier;
                aoeHit.m_damage.m_blunt = weaponDamage.m_blunt * aoeDamageMultiplier;
                aoeHit.m_damage.m_pierce = weaponDamage.m_pierce * aoeDamageMultiplier;

                aoeHit.m_point = enemy.GetCenterPoint();
                aoeHit.m_dir = knockDir;
                aoeHit.m_pushForce = 100f;
                aoeHit.m_attacker = player.GetZDOID();
                aoeHit.SetAttacker(player);
                aoeHit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                enemy.Damage(aoeHit);
                enemy.Stagger(knockDir);

                // 강제 위치 이동 (10m 넉백)
                Vector3 oldPos = enemy.transform.position;
                enemy.transform.position = ResolveKnockbackDestination(enemy.transform.position, knockDir, knockbackForce);

                Plugin.Log.LogDebug($"[관통 돌격 AOE] {enemy.name} 넉백: {oldPos} → {enemy.transform.position}");

                // VFX
                VFXManager.PlayVFXMultiplayer("fx_crit", "", enemy.GetCenterPoint(), Quaternion.identity, 1.5f);

                knockbackCount++;
            }

            Plugin.Log.LogInfo($"[관통 돌격 AOE] 총 몬스터: {totalMonsters}, 범위 내 넉백: {knockbackCount}명");
        }

        /// <summary>
        /// 플레이어 공격속도 가져오기
        /// </summary>
        private static float GetPlayerAttackSpeed(Player player)
        {
            if (player == null) return 1f;
            // SE_Stats를 통한 공격속도 확인 또는 기본값 반환
            return 1f;
        }

        /// <summary>
        /// 플레이어 공격속도 부스트 설정 (임시)
        /// </summary>
        private static void SetPlayerAttackSpeedBoost(Player player, float multiplier)
        {
            if (player == null) return;

            try
            {
                // 공격속도 부스트를 위한 임시 상태 효과 적용
                // Animator 속도 조절로 즉각적인 효과
                var animator = player.GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    animator.speed = multiplier;
                    Plugin.Log.LogDebug($"[관통 돌격] Animator 속도 설정: {multiplier}x");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[관통 돌격] 공격속도 부스트 설정 실패: {ex.Message}");
            }
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
        /// 몬스터 넉백 목적지 계산: 경로상 장애물(바위/벽) 충돌 시 그 앞에서 정지, 최종 위치는 지면 높이로 보정
        /// </summary>
        private static Vector3 ResolveKnockbackDestination(Vector3 startPos, Vector3 knockDir, float distance)
        {
            if (distance <= 0f || knockDir.sqrMagnitude < 0.0001f) return startPos;
            knockDir.Normalize();

            // 장애물 충돌 체크 (바위·벽 등) — 휠윈드/돌진방패와 동일 레이어 마스크
            int blockMask = LayerMask.GetMask("piece", "Default", "static_solid");
            if (Physics.SphereCast(startPos + Vector3.up * 0.8f, 0.4f, knockDir, out RaycastHit hit, distance, blockMask))
            {
                if (hit.collider.GetComponentInParent<Character>() == null)
                {
                    distance = Mathf.Max(0f, hit.distance - 0.3f);
                }
            }

            Vector3 endPos = startPos + knockDir * distance;
            return GetGroundPosition(endPos); // 기존 지면 보정 헬퍼 재사용 (땅속 매몰 방지)
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
                if (c == null || c.IsDead() || (!c.IsMonsterFaction(Time.time) && !c.IsBoss())) continue;

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
                if (c == null || c.IsDead() || (!c.IsMonsterFaction(Time.time) && !c.IsBoss())) continue;

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
                if (!c.IsMonsterFaction(Time.time) && !c.IsBoss()) continue;

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

        private static Character FindPierceChargeTarget(Player player)
        {
            Vector3 lookDir = player.GetLookDir();
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude < 0.01f) return null;
            lookDir.Normalize();

            Vector3 origin = player.GetCenterPoint();
            float range = Polearm_Config.PolearmPierceChargeDashDistanceValue;
            Character nearest = null;
            float nearestDist = float.MaxValue;

            foreach (var enemy in Character.GetAllCharacters())
            {
                if (enemy == null || enemy == player || enemy.IsDead()) continue;
                if (enemy.GetFaction() == Character.Faction.Players)
                {
                    var tp = enemy as Player;
                    if (tp == null || !tp.IsPVPEnabled() || !player.IsPVPEnabled()) continue;
                }

                float dist = Vector3.Distance(origin, enemy.GetCenterPoint());
                if (dist > range) continue;

                Vector3 toEnemyFlat = enemy.GetCenterPoint() - origin;
                toEnemyFlat.y = 0f;
                if (toEnemyFlat.sqrMagnitude < 0.01f)
                {
                    if (dist < nearestDist) { nearestDist = dist; nearest = enemy; }
                    continue;
                }
                toEnemyFlat.Normalize();
                if (Vector3.Angle(lookDir, toEnemyFlat) > 75f) continue;

                if (dist < nearestDist) { nearestDist = dist; nearest = enemy; }
            }
            return nearest;
        }

        // "piece" = 건축물, "Default" = 나무/던전 벽. radius 0.35f로 발 정도 걸린 건 통과 허용
        private static bool IsPierceChargePathBlocked(Player player, Character target)
        {
            Vector3 from = player.GetCenterPoint();
            Vector3 to   = target.GetCenterPoint();
            float dist   = Vector3.Distance(from, to);
            if (dist < 0.5f) return false;

            Vector3 dir = (to - from).normalized;
            LayerMask blockMask = LayerMask.GetMask("piece", "Default", "static_solid");

            if (Physics.SphereCast(from, 0.35f, dir, out RaycastHit hit, dist - 0.5f, blockMask))
            {
                if (hit.collider.GetComponentInParent<Character>() == null)
                {
                    Plugin.Log.LogDebug($"[관통 돌격] 경로 차단: {hit.collider.name}");
                    return true;
                }
            }
            return false;
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
        /// Berserker Lv2 관통 돌격 추가 사용 창 만료 처리
        /// </summary>
        private static IEnumerator ExpirePierceChargeWindow(Player player)
        {
            yield return new WaitForSeconds(PierceChargeExtraWindow);
            if (_pierceChargePendingWindow.ContainsKey(player))
            {
                _pierceChargePendingWindow.Remove(player);
                float cd = Polearm_Config.PolearmPierceChargeCooldownValue;
                polearmPierceChargeLastUseTime[player] = Time.time;
                ActiveSkillCooldownRegistry.SetCooldownForSkill("G", "polearm_step5_king", cd);
                Plugin.Log.LogDebug("[관통 돌격] Berserker 추가 사용 창 만료 - 쿨타임 시작");
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
        /// 폭풍베기 (polearm_step3_ground)는 별도 패치(PolearmStormSlash)에서 처리
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

            return bonus;
        }

        /// <summary>
        /// 영웅 타격 스태거 확률 (polearm_step2_hero)
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
}

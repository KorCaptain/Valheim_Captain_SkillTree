using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 검 스킬 전용 로직 시스템
    /// 돌진 연속 베기 (Rush Slash) 액티브 스킬 및 검 관련 모든 스킬 구현
    /// </summary>
    public static partial class Sword_Skill
    {
        // === 돌진 연속 베기 (Rush Slash) 액티브 스킬 관련 변수 ===
        private static Dictionary<Player, float> rushSlashCooldowns = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> rushSlashActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> rushSlashEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> rushSlashCoroutines = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, int> rushSlashAttackCount = new Dictionary<Player, int>();
        private static Dictionary<Player, float> rushSlashInvincibleUntil = new Dictionary<Player, float>();

        // === 기존 호환용 별칭 ===
        private static Dictionary<Player, float> swordSlashCooldowns => rushSlashCooldowns;
        private static Dictionary<Player, bool> swordSlashActive => rushSlashActive;
        private static Dictionary<Player, float> swordSlashEndTime => rushSlashEndTime;
        private static Dictionary<Player, Coroutine> swordSlashCoroutines => rushSlashCoroutines;
        private static Dictionary<Player, int> swordSlashAttackCount => rushSlashAttackCount;

        // === Paladin Lv2 추가 사용 창 ===
        private static Dictionary<Player, float> _swordSlashPendingWindow = new Dictionary<Player, float>();
        private const float SwordSlashExtraWindow = 30f;

        /// <summary>
        /// 플레이어가 방패를 착용 중인지 확인
        /// </summary>
        public static bool HasShield(Player player)
        {
            if (player == null) return false;

            try
            {
                var inventory = player.GetInventory();
                if (inventory == null) return false;

                foreach (var item in inventory.GetEquippedItems())
                {
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 장비 목록에서 검 무기 아이템 반환 (전투 중 GetCurrentWeapon 문제 회피)
        /// </summary>
        public static ItemDrop.ItemData GetEquippedSword(Player player)
        {
            if (player == null) return null;
            try
            {
                var inventory = player.GetInventory();
                if (inventory == null) return null;

                foreach (var item in inventory.GetEquippedItems())
                {
                    if (item.m_shared.m_skillType == Skills.SkillType.Swords)
                        return item;
                }
                return null;
            }
            catch { return null; }
        }

        /// <summary>
        /// 플레이어가 검을 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 Swords 스킬 타입
        /// 2순위: 프리팹 이름에 "Sword", "sword", "Blade", "blade" 포함
        /// 3순위: 무기 이름에 "검", "sword", "blade" 포함
        /// </summary>
        public static bool IsUsingSword(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 Swords 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
            {
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Sword") || prefabName.Contains("sword") || 
                prefabName.Contains("Blade") || prefabName.Contains("blade"))
            {
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("검") || weaponName.Contains("sword") || weaponName.Contains("blade"))
            {
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// 돌진 연속 베기 G키 액티브 스킬 활성화
        /// - 전방 5m 돌진 후 몬스터 주변으로 빠르게 이동하며 3회 연속 베기
        /// - 1차 70%, 2차 80%, 3차 90% 공격력
        /// - 소모: 스태미나 30 | 쿨타임: 25초
        /// </summary>
        public static void ActivateSwordSlash(Player player) => ActivateRushSlash(player);

        /// <summary>
        /// 돌진 연속 베기 (Rush Slash) 액티브 스킬 발동 (G키)
        /// </summary>
        public static void ActivateRushSlash(Player player)
        {
            try
            {
                if (player == null || player.IsDead())
                {
                    return;
                }

                // 1. 스킬 보유 확인
                bool hasSkill = SkillEffect.HasSkill("sword_step5_finalcut") || SkillEffect.HasSkill("sword_slash");
                if (!hasSkill)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("rush_slash_skill_required"), Color.red);
                    return;
                }

                // 2. 검 착용 확인
                if (!WeaponHelper.IsUsingSwordOrAxe(player))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("sword_or_axe_required"), Color.red);
                    return;
                }

                // 3. 쿨타임 확인
                float now = Time.time;
                bool hasPaladinLv2_rush = (SkillTreeManager.Instance?.GetSkillLevel("Paladin") ?? 0) >= 2;
                bool hasBerserkerLv2_rush = (SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 0) >= 2;
                bool hasTankerLv2_rush = (SkillTreeManager.Instance?.GetSkillLevel("Tanker") ?? 0) >= 2;
                bool hasExtraUse_rush = hasPaladinLv2_rush || hasBerserkerLv2_rush || hasTankerLv2_rush;
                bool inRushSlashWindow = hasExtraUse_rush
                    && _swordSlashPendingWindow.TryGetValue(player, out float rushWinExpiry)
                    && now <= rushWinExpiry;

                if (!inRushSlashWindow && rushSlashCooldowns.TryGetValue(player, out float cdEnd) && now < cdEnd)
                {
                    float remaining = cdEnd - now;
                    SkillEffect.DrawFloatingText(player, L.Get("cooldown_remaining", Mathf.CeilToInt(remaining)), Color.yellow);
                    return;
                }

                // 4. 스태미나 소모 확인
                float requiredStamina = Sword_Config.RushSlashStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // 5. 이미 스킬 실행 중인지 확인
                if (rushSlashActive.TryGetValue(player, out bool alreadyActive) && alreadyActive)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("rush_slash_in_progress"), Color.yellow);
                    return;
                }

                // 6. 스킬 활성화
                float duration = Sword_Config.CalculateTotalSkillDuration();
                float cooldown = Sword_Config.RushSlashCooldownValue;

                rushSlashActive[player] = true;
                rushSlashEndTime[player] = now + duration;
                rushSlashAttackCount[player] = 0;

                // Paladin Lv2 / Berserker Lv2 분기
                if (hasExtraUse_rush && !inRushSlashWindow)
                {
                    // 1번째 사용: 쿨타임 보류 + 30초 추가 사용 창 오픈
                    _swordSlashPendingWindow[player] = now + SwordSlashExtraWindow;
                    player.StartCoroutine(ExpireSwordSlashWindow(player));
                }
                else
                {
                    // 2번째 사용(창 만료) 또는 미해당: 쿨타임 즉시 시작
                    _swordSlashPendingWindow.Remove(player);
                    rushSlashCooldowns[player] = now + cooldown;
                    ActiveSkillCooldownRegistry.SetCooldownForSkills("G", new[] { "sword_step5_finalcut", "sword_slash" }, cooldown);
                }

                // 7. 스태미나 소모
                player.UseStamina(requiredStamina);
                SkillEffect.TankerPrereqLastUsedTime = Time.time;

                // 8. 발동 메시지
                SkillEffect.DrawFloatingText(player, L.Get("rush_slash_activate"), Color.red);

                // 9. 코루틴 시작
                if (rushSlashCoroutines.TryGetValue(player, out var existingCoroutine) && existingCoroutine != null)
                {
                    player.StopCoroutine(existingCoroutine);
                }

                var coroutine = ExecuteRushSlashSequence(player);
                rushSlashCoroutines[player] = player.StartCoroutine(coroutine);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Rush Slash] 스킬 활성화 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 돌진 연속 베기 순차 실행 코루틴
        /// 전방 돌진 후 목표를 주변으로 빠르게 이동하며 3회 연속 베기
        /// 스킬 종료 시 원래 위치로 복귀
        /// </summary>
        private static IEnumerator ExecuteRushSlashSequence(Player player)
        {
            try
            {
            if (player == null || player.IsDead())
            {
                yield break;
            }

            // 원래 위치 저장 (스킬 종료 시 복귀용)
            Vector3 originalPosition = player.transform.position;

            var skillData = Sword_Config.GetRushSlashData();
            float moveSpeed = skillData.moveSpeed;
            float initialDist = skillData.initialDistance;
            float sideDist = skillData.sideDistance;

            // 무기 확인
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                yield break;
            }

            // 무기 기본 데미지
            var weaponDamage = weapon.GetDamage();
            int totalHits = 0;
            var alreadyHitInPath = new HashSet<int>();

            // === 근접 몬스터 탐지 (10m 내 가장 가까운 몬스터) ===
            Character target = FindNearestMonster(player, 10f);
            Vector3 targetPos = target?.transform.position ??
                               (player.transform.position + GetCameraForward(player) * initialDist);

            // === 1차: 전방 돌진 + 베기 ===
            Vector3 dashTarget = player.transform.position + GetCameraForward(player) * initialDist;
            yield return MoveToPositionWithPathHit(player, dashTarget, initialDist, moveSpeed,
                weapon, weaponDamage, skillData.damage1stRatio, Sword_Config.RushSlashPathWidthValue, alreadyHitInPath);

            if (player == null || player.IsDead() || !rushSlashActive.TryGetValue(player, out bool rsActive1) || !rsActive1)
            {
                yield break;
            }

            // 1李?踰좉린 ?ㅽ뻾
            int hits1 = ExecuteSlashAttack(player, weapon, weaponDamage, 1, skillData.damage1stRatio, target);
            totalHits += hits1;
            rushSlashAttackCount[player] = 1;
            yield return new WaitForSeconds(0.35f);

            // === 2차: 몬스터 오른쪽 이동 + 베기 ===
            if (player == null || player.IsDead() || !WeaponHelper.IsUsingSwordOrAxe(player))
            {
                yield break;
            }

            // 목표 위치 갱신 (몬스터가 이동했을 수 있음)
            if (target != null && !target.IsDead())
            {
                targetPos = target.transform.position;
            }

            Vector3 rightPos = CalculateSidePosition(player, targetPos, sideDist, true);
            yield return MoveToPositionWithPathHit(player, rightPos, sideDist, moveSpeed,
                weapon, weaponDamage, skillData.damage2ndRatio, Sword_Config.RushSlashPathWidthValue, alreadyHitInPath);

            if (player == null || player.IsDead() || !rushSlashActive.TryGetValue(player, out bool rsActive2) || !rsActive2)
            {
                yield break;
            }

            // 2李?踰좉린 ?ㅽ뻾
            int hits2 = ExecuteSlashAttack(player, weapon, weaponDamage, 2, skillData.damage2ndRatio, target);
            totalHits += hits2;
            rushSlashAttackCount[player] = 2;
            yield return new WaitForSeconds(0.35f);

            // === 3차: 몬스터 왼쪽 이동 + 베기 ===
            if (player == null || player.IsDead() || !WeaponHelper.IsUsingSwordOrAxe(player))
            {
                yield break;
            }

            // 목표 위치 갱신
            if (target != null && !target.IsDead())
            {
                targetPos = target.transform.position;
            }

            Vector3 leftPos = CalculateSidePosition(player, targetPos, sideDist, false);
            yield return MoveToPositionWithPathHit(player, leftPos, sideDist, moveSpeed,
                weapon, weaponDamage, skillData.damage3rdRatio, Sword_Config.RushSlashPathWidthValue, alreadyHitInPath);

            if (player == null || player.IsDead() || !rushSlashActive.TryGetValue(player, out bool rsActive3) || !rsActive3)
            {
                yield break;
            }

            // 3차 베기 실행 (마무리 피니시)
            int hits3 = ExecuteSlashAttack(player, weapon, weaponDamage, 3, skillData.damage3rdRatio, target);
            totalHits += hits3;
            rushSlashAttackCount[player] = 3;
            yield return new WaitForSeconds(0.35f);

            // === 마무리: 원래 위치로 복귀 ===
            if (player != null && !player.IsDead())
            {
                if (originalPosition.y > 4000f)
                {
                    // 던전 이내: 즉시 복귀 (이동 중 출구 트리거 겹칠 방지)
                    player.transform.position = originalPosition;
                }
                else
                {
                    float returnDistance = Vector3.Distance(player.transform.position, originalPosition);
                    yield return MoveToPosition(player, originalPosition, returnDistance, moveSpeed);
                }

                SkillEffect.DrawFloatingText(player, L.Get("rush_slash_return"), Color.cyan);
            }

            // 정상 종료 시에만 무적 시간 부여 (기존 동작 유지)
            rushSlashInvincibleUntil[player] = Time.time + 1f;
            SkillEffect.DrawFloatingText(player, L.Get("rush_slash_complete", totalHits), Color.green);

            yield return null;
            }
            finally
            {
                // 예외/중단 여부와 무관하게 "실행 중" 플래그 리셋을 보장
                CleanupRushSlash(player);
            }
        }

        /// <summary>
        /// 기존 호환용 코루틴 (deprecated)
        /// </summary>
        private static IEnumerator ExecuteSwordSlashCombo(Player player)
        {
            yield return ExecuteRushSlashSequence(player);
        }

        /// <summary>
        /// 가장 가까운 몬스터 탐지
        /// </summary>
        private static Character FindNearestMonster(Player player, float range)
        {
            if (player == null) return null;

            Character nearest = null;
            float minDist = range;

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
        /// 플레이어를 목표 위치로 빠르게 이동 (Lerp 보간)
        /// </summary>
        private static IEnumerator MoveToPosition(Player player, Vector3 targetPos, float distance, float speed)
        {
            if (player == null) yield break;

            float duration = distance / speed;
            float elapsed = 0f;
            Vector3 startPos = player.transform.position;

            // 지면 높이 보정 (Raycast)
            targetPos = GetGroundPosition(targetPos);

            while (elapsed < duration)
            {
                if (player == null || player.IsDead())
                {
                    yield break;
                }

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                // 부드러운 이동 (EaseOut)
                float smoothT = 1f - Mathf.Pow(1f - t, 2f);
                Vector3 newPos = Vector3.Lerp(startPos, targetPos, smoothT);

                // 지면 높이 보정
                newPos = GetGroundPosition(newPos);
                player.transform.position = newPos;

                yield return null;
            }

            // 최종 위치 설정
            player.transform.position = GetGroundPosition(targetPos);
        }

        /// <summary>
        /// 지면 높이 보정 (Raycast)
        /// </summary>
        private static Vector3 GetGroundPosition(Vector3 pos)
        {
            if (pos.y > 4000f)
            {
                // 던전 내(Y>4000): 던전 바닥은 terrain 레이어가 아니므로 레이어 제한 없이 Raycast
                if (Physics.Raycast(pos + Vector3.up * 3f, Vector3.down, out RaycastHit dungeonHit, 8f))
                {
                    return new Vector3(pos.x, dungeonHit.point.y + 0.1f, pos.z);
                }
                return pos;
            }
            // 일반 지면
            if (Physics.Raycast(pos + Vector3.up * 5f, Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("terrain", "Default")))
            {
                return new Vector3(pos.x, hit.point.y + 0.1f, pos.z);
            }
            return pos;
        }

        /// <summary>
        /// 베기 공격 실행 + VFX + 데미지
        /// </summary>
        private static int ExecuteSlashAttack(Player player, ItemDrop.ItemData weapon, HitData.DamageTypes weaponDamage,
            int attackNumber, float damageRatio, Character target)
        {
            if (player == null) return 0;

            // 1. 목표 방향으로 회전 (공격 모션이 자연스럽게 보이도록)
            if (target != null && !target.IsDead())
            {
                Vector3 lookDir = (target.transform.position - player.transform.position);
                lookDir.y = 0;
                if (lookDir.sqrMagnitude > 0.001f)
                    player.transform.rotation = Quaternion.LookRotation(lookDir.normalized);
            }

            // 2. 검 연속 공격 모션 트리거 (마우스 연속 클릭과 동일한 3연속 베기)
            try
            {
                player.StartAttack(null, false);
            }
            catch { }

            // 3. VFX 선택 (공격 차수별)
            string vfxName = attackNumber switch
            {
                1 => "fx_lightningstaffprojectile_hit",
                2 => "fx_lightningstaffprojectile_hit",
                3 => "fx_crit", // 피니시 효과
                _ => "fx_lightningstaffprojectile_hit"
            };

            // 3. VFX 재생 위치 (플레이어 전방 or 목표 위치)
            Vector3 vfxPos = target != null && !target.IsDead()
                ? target.GetCenterPoint()
                : player.transform.position + player.transform.forward * 2f + Vector3.up * 1f;

            SimpleVFX.Play(vfxName, vfxPos, 1.5f);

            // 4. 직접 데미지 적용 (범위 4m 내 몬스터)
            float ratio = damageRatio / 100f;
            var monsters = Character.GetAllCharacters()
                .Where(c => c != null && !c.IsDead() && (c.IsMonsterFaction(Time.time) || c.IsBoss()) && Vector3.Distance(c.transform.position, player.transform.position) < 4f)
                .Take(5);

            int hitCount = 0;
            foreach (var monster in monsters)
            {
                float bossBonus = monster.IsBoss() ? 1.2f : 1f;
                var hit = new HitData();
                hit.m_damage.m_slash = weaponDamage.m_slash * ratio * bossBonus;
                hit.m_damage.m_blunt = weaponDamage.m_blunt * ratio * bossBonus;
                hit.m_damage.m_pierce = weaponDamage.m_pierce * ratio * bossBonus;

                hit.m_point = monster.GetCenterPoint();
                hit.m_dir = (monster.transform.position - player.transform.position).normalized;
                hit.m_attacker = player.GetZDOID();
                hit.SetAttacker(player);
                hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                monster.Damage(hit);
                hitCount++;

                // 개별 타격 VFX
                SimpleVFX.Play("flash_ellow_pink", monster.GetCenterPoint(), 1f);
            }

            // 5. 플로팅 텍스트
            string attackText = attackNumber switch
            {
                1 => L.Get("rush_slash_1st_attack", (int)damageRatio),
                2 => L.Get("rush_slash_2nd_attack", (int)damageRatio),
                3 => L.Get("rush_slash_finisher", (int)damageRatio),
                _ => L.Get("rush_slash_default", (int)damageRatio)
            };
            SkillEffect.DrawFloatingText(player, attackText, attackNumber == 3 ? Color.yellow : Color.red);

            return hitCount;
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
        /// 카메라 기준 측면 위치 계산 (오른쪽/왼쪽)
        /// </summary>
        private static Vector3 CalculateSidePosition(Player player, Vector3 targetPos, float distance, bool right)
        {
            Vector3 sideDir;
            if (Camera.main != null)
            {
                sideDir = right ? Camera.main.transform.right : -Camera.main.transform.right;
                sideDir.y = 0;
                sideDir.Normalize();
            }
            else
            {
                sideDir = right ? player.transform.right : -player.transform.right;
            }

            return targetPos + sideDir * distance;
        }

        /// <summary>
        /// 카메라 기준 뒤쪽 위치 계산 (몬스터 기준)
        /// </summary>
        private static Vector3 CalculateBackPosition(Player player, Vector3 targetPos, float distance)
        {
            Vector3 backDir;
            if (Camera.main != null)
            {
                backDir = Camera.main.transform.forward; // 카메라 전방 = 몬스터 뒤쪽
                backDir.y = 0;
                backDir.Normalize();
            }
            else
            {
                backDir = player.transform.forward;
            }

            return targetPos + backDir * distance;
        }

        /// <summary>
        /// 돌진 연속 베기 상태 정리
        /// </summary>
        private static void CleanupRushSlash(Player player)
        {
            if (player != null && rushSlashActive.ContainsKey(player))
            {
                rushSlashActive[player] = false;
            }
        }

        /// <summary>
        /// 돌진 연속 베기 액티브 상태 확인
        /// </summary>
        public static bool IsSwordSlashActive(Player player)
        {
            return rushSlashActive.TryGetValue(player, out bool active) && active &&
                   rushSlashEndTime.TryGetValue(player, out float endTime) && Time.time < endTime;
        }
        /// <summary>
        /// 돌진 연속 베기 무적 여부 (시전 중 + 종료 후 1초)
        /// </summary>
        public static bool IsRushSlashInvincible(Player player)
        {
            if (player == null) return false;
            if (rushSlashActive.TryGetValue(player, out bool active) && active) return true;
            return rushSlashInvincibleUntil.TryGetValue(player, out float t) && Time.time < t;
        }

        /// <summary>
        /// 현재 공격 횟수 확인
        /// </summary>
        public static int GetSwordSlashAttackCount(Player player)
        {
            return rushSlashAttackCount.TryGetValue(player, out int count) ? count : 0;
        }

        /// <summary>
        /// 돌진 연속 베기 스킬 강제 중단
        /// </summary>
        public static void StopSwordSlash(Player player)
        {
            try
            {
                if (rushSlashActive.ContainsKey(player))
                {
                    rushSlashActive[player] = false;
                }

                if (rushSlashCoroutines.TryGetValue(player, out var stopCoroutine1) && stopCoroutine1 != null)
                {
                    player.StopCoroutine(stopCoroutine1);
                    rushSlashCoroutines[player] = null;
                }

                SkillEffect.DrawFloatingText(player, L.Get("rush_slash_canceled"), Color.yellow);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Rush Slash] 스킬 중단 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 검 스킬 쿨타임 정보 조회
        /// </summary>
        public static float GetSwordSlashCooldownRemaining(Player player)
        {
            if (rushSlashCooldowns.TryGetValue(player, out float cooldownEnd))
            {
                return Mathf.Max(0f, cooldownEnd - Time.time);
            }
            return 0f;
        }

        /// <summary>
        /// 모든 검 스킬 상태 초기화 (플레이어 로그아웃 시 등)
        /// </summary>
        public static void ClearSwordSkillStates(Player player)
        {
            try
            {
                StopSwordSlash(player);

                rushSlashCooldowns.Remove(player);
                rushSlashActive.Remove(player);
                rushSlashEndTime.Remove(player);
                rushSlashCoroutines.Remove(player);
                rushSlashAttackCount.Remove(player);
                rushSlashInvincibleUntil.Remove(player);

                Plugin.Log.LogDebug($"[Sword Skill] {player.GetPlayerName()} 모든 검 스킬 상태 초기화 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Skill] 상태 초기화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 슬래시 스킬 활성화 상태 확인 (호환용)
        /// </summary>
        public static bool IsSlashActive(Player player)
        {
            if (player == null) return false;
            return rushSlashActive.TryGetValue(player, out bool active) && active;
        }

        /// <summary>
        /// 검 베기 액티브 스킬 슬롯 창 정리 시스템
        /// </summary>
        /// <summary>Paladin Lv2 돌진베기 추가 사용 창 만료</summary>
        private static IEnumerator ExpireSwordSlashWindow(Player player)
        {
            yield return new WaitForSeconds(SwordSlashExtraWindow);
            if (_swordSlashPendingWindow.ContainsKey(player))
            {
                _swordSlashPendingWindow.Remove(player);
                float cd = Sword_Config.RushSlashCooldownValue;
                rushSlashCooldowns[player] = Time.time + cd;
                ActiveSkillCooldownRegistry.SetCooldownForSkills("G", new[] { "sword_step5_finalcut", "sword_slash" }, cd);
                Plugin.Log.LogDebug("[돌진베기] Paladin 추가 사용 창 만료 - 쿨타임 시작");
            }
        }

        public static void CleanupSwordSkillOnDeath(Player player)
        {
            try
            {
                rushSlashCooldowns.Remove(player);
                rushSlashActive.Remove(player);
                rushSlashEndTime.Remove(player);
                _swordSlashPendingWindow.Remove(player);

                if (rushSlashCoroutines.TryGetValue(player, out var stopCoroutine2) && stopCoroutine2 != null)
                {
                    try
                    {
                        Plugin.Instance?.StopCoroutine(stopCoroutine2);
                    }
                    catch { }
                    rushSlashCoroutines.Remove(player);
                }

                rushSlashAttackCount.Remove(player);
                rushSlashInvincibleUntil.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Sword Skill] 정리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 검 전문가 - 공격력 보너스 (비율)
        /// 실제 효과는 ItemData.GetDamage 함수에서 적용됨
        /// </summary>
        public static float GetSwordExpertDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_expert") || !WeaponHelper.IsUsingSwordOrAxe(player))
                return 0f;

            try
            {
                float damageBonus = Sword_Config.SwordExpertDamageValue;
                Plugin.Log.LogDebug($"[검 전문가] 공격력 +{damageBonus}%");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 전문가] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 칼날 되치기 - 공격력 고정 보너스
        /// 실제 효과는 ItemData.GetDamage 함수에서 적용됨
        /// </summary>
        public static float GetSwordRiposteDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_step3_riposte") || !WeaponHelper.IsUsingSwordOrAxe(player)) return 0f;

            try
            {
                float damageBonus = Sword_Config.SwordRiposteDamageBonusValue;
                Plugin.Log.LogDebug($"[칼날 되치기] 공격력 +{damageBonus}");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[칼날 되치기] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }


        // === 패링 돌격 → 회오리베기로 교체됨 (후방호환 스텁) ===

        /// <summary>H키 핸들러에서 더 이상 호출되지 않음 — 후방호환 스텁</summary>
        public static void ActivateParryRush(Player player) { }

        /// <summary>Plugin.Patches.cs / DefenseTree.cs 에서 참조 — 항상 false 반환</summary>
        public static bool IsParryRushActive(Player player) => false;

        /// <summary>Plugin.Patches.cs / DefenseTree.cs 에서 참조 — 회오리베기에선 무시</summary>
        public static void OnParryRushTrigger(Player player, Character attacker) { }

        /// <summary>JobSkills.cs 사망 처리에서 참조 — 회오리베기 정리로 위임</summary>
        public static void CleanupParryRushOnDeath(Player player) { CleanupWhirlwindOnDeath(player); }

    }

}

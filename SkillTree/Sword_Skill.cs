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
    public static class Sword_Skill
    {
        // === 돌진 연속 베기 (Rush Slash) 액티브 스킬 관련 변수 ===
        private static Dictionary<Player, float> rushSlashCooldowns = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> rushSlashActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> rushSlashEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> rushSlashCoroutines = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, int> rushSlashAttackCount = new Dictionary<Player, int>();

        // === 기존 호환성용 별칭 ===
        private static Dictionary<Player, float> swordSlashCooldowns => rushSlashCooldowns;
        private static Dictionary<Player, bool> swordSlashActive => rushSlashActive;
        private static Dictionary<Player, float> swordSlashEndTime => rushSlashEndTime;
        private static Dictionary<Player, Coroutine> swordSlashCoroutines => rushSlashCoroutines;
        private static Dictionary<Player, int> swordSlashAttackCount => rushSlashAttackCount;

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
        /// 장비 목록에서 검 무기 아이템 반환 (패링 중 GetCurrentWeapon 문제 회피)
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
        /// - 전방 5m 돌진 후 몬스터 주변을 빠르게 이동하며 3회 연속 베기
        /// - 1차: 70%, 2차: 80%, 3차: 90% 공격력
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
                if (!IsUsingSword(player))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("sword_required"), Color.red);
                    return;
                }

                // 3. 쿨타임 확인
                float now = Time.time;
                if (rushSlashCooldowns.ContainsKey(player) && now < rushSlashCooldowns[player])
                {
                    float remaining = rushSlashCooldowns[player] - now;
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
                if (rushSlashActive.ContainsKey(player) && rushSlashActive[player])
                {
                    SkillEffect.DrawFloatingText(player, L.Get("rush_slash_in_progress"), Color.yellow);
                    return;
                }

                // 6. 스킬 활성화
                float duration = Sword_Config.CalculateTotalSkillDuration();
                float cooldown = Sword_Config.RushSlashCooldownValue;

                rushSlashActive[player] = true;
                rushSlashEndTime[player] = now + duration;
                rushSlashCooldowns[player] = now + cooldown;
                rushSlashAttackCount[player] = 0;

                // 7. 스태미나 소모
                player.UseStamina(requiredStamina);

                // 8. 발동 메시지
                SkillEffect.DrawFloatingText(player, "⚔️ " + L.Get("rush_slash_activate"), Color.red);

                // 9. 코루틴 시작
                if (rushSlashCoroutines.ContainsKey(player) && rushSlashCoroutines[player] != null)
                {
                    player.StopCoroutine(rushSlashCoroutines[player]);
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
        /// 돌진 연속 베기 시퀀스 실행 코루틴
        /// 전방 돌진 후 몬스터 주변을 빠르게 이동하며 3회 연속 베기
        /// 스킬 종료 후 원래 위치로 복귀
        /// </summary>
        private static IEnumerator ExecuteRushSlashSequence(Player player)
        {
            if (player == null || player.IsDead())
            {
                yield break;
            }

            // 원래 위치 저장 (스킬 종료 후 복귀용)
            Vector3 originalPosition = player.transform.position;

            var skillData = Sword_Config.GetRushSlashData();
            float moveSpeed = skillData.moveSpeed;
            float initialDist = skillData.initialDistance;
            float sideDist = skillData.sideDistance;

            // 무기 확인
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                if (rushSlashActive.ContainsKey(player))
                    rushSlashActive[player] = false;
                yield break;
            }

            // 무기 기본 데미지
            var weaponDamage = weapon.GetDamage();
            int totalHits = 0;

            // === 타겟 몬스터 탐지 (10m 내 가장 가까운 몬스터) ===
            Character target = FindNearestMonster(player, 10f);
            Vector3 targetPos = target?.transform.position ??
                               (player.transform.position + GetCameraForward(player) * initialDist);

            // === 1차: 전방 돌진 + 베기 ===
            Vector3 dashTarget = player.transform.position + GetCameraForward(player) * initialDist;
            yield return MoveToPosition(player, dashTarget, initialDist, moveSpeed);

            if (player == null || player.IsDead() || !rushSlashActive.ContainsKey(player) || !rushSlashActive[player])
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 1차 베기 실행
            int hits1 = ExecuteSlashAttack(player, weapon, weaponDamage, 1, skillData.damage1stRatio, target);
            totalHits += hits1;
            rushSlashAttackCount[player] = 1;
            yield return new WaitForSeconds(0.35f);

            // === 2차: 몬스터 오른쪽 이동 + 베기 ===
            if (player == null || player.IsDead() || !IsUsingSword(player))
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 타겟 위치 갱신 (몬스터가 이동했을 수 있음)
            if (target != null && !target.IsDead())
            {
                targetPos = target.transform.position;
            }

            Vector3 rightPos = CalculateSidePosition(player, targetPos, sideDist, true);
            yield return MoveToPosition(player, rightPos, sideDist, moveSpeed);

            if (player == null || player.IsDead() || !rushSlashActive.ContainsKey(player) || !rushSlashActive[player])
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 2차 베기 실행
            int hits2 = ExecuteSlashAttack(player, weapon, weaponDamage, 2, skillData.damage2ndRatio, target);
            totalHits += hits2;
            rushSlashAttackCount[player] = 2;
            yield return new WaitForSeconds(0.35f);

            // === 3차: 몬스터 왼쪽 이동 + 베기 ===
            if (player == null || player.IsDead() || !IsUsingSword(player))
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 타겟 위치 갱신
            if (target != null && !target.IsDead())
            {
                targetPos = target.transform.position;
            }

            Vector3 leftPos = CalculateSidePosition(player, targetPos, sideDist, false);
            yield return MoveToPosition(player, leftPos, sideDist, moveSpeed);

            if (player == null || player.IsDead() || !rushSlashActive.ContainsKey(player) || !rushSlashActive[player])
            {
                CleanupRushSlash(player);
                yield break;
            }

            // 3차 베기 실행 (마무리 피니셔)
            int hits3 = ExecuteSlashAttack(player, weapon, weaponDamage, 3, skillData.damage3rdRatio, target);
            totalHits += hits3;
            rushSlashAttackCount[player] = 3;
            yield return new WaitForSeconds(0.35f);

            // === 마무리: 원래 위치로 복귀 ===
            if (player != null && !player.IsDead())
            {
                float returnDistance = Vector3.Distance(player.transform.position, originalPosition);
                yield return MoveToPosition(player, originalPosition, returnDistance, moveSpeed);

                SkillEffect.DrawFloatingText(player, "⚔️ " + L.Get("rush_slash_return"), Color.cyan);
            }

            // 상태 정리
            CleanupRushSlash(player);
            SkillEffect.DrawFloatingText(player, "⚔️ " + L.Get("rush_slash_complete", totalHits), Color.green);

            yield return null;
        }

        /// <summary>
        /// 기존 호환성용 코루틴 (deprecated)
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
            // 위에서 아래로 레이캐스트
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

            // 1. 타겟 방향으로 회전 (공격 모션이 자연스럽게 보이도록)
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
                3 => "fx_crit", // 피니셔 효과
                _ => "fx_lightningstaffprojectile_hit"
            };

            // 3. VFX 재생 위치 (플레이어 전방 or 타겟 위치)
            Vector3 vfxPos = target != null && !target.IsDead()
                ? target.GetCenterPoint()
                : player.transform.position + player.transform.forward * 2f + Vector3.up * 1f;

            SimpleVFX.Play(vfxName, vfxPos, 1.5f);

            // 4. 직접 데미지 적용 (범위 4m 내 몬스터)
            float ratio = damageRatio / 100f;
            var monsters = Character.GetAllCharacters()
                .Where(c => c != null && !c.IsDead() && c.IsMonsterFaction(Time.time) &&
                           Vector3.Distance(c.transform.position, player.transform.position) < 4f)
                .Take(5);

            int hitCount = 0;
            foreach (var monster in monsters)
            {
                var hit = new HitData();
                hit.m_damage.m_slash = weaponDamage.m_slash * ratio;
                hit.m_damage.m_blunt = weaponDamage.m_blunt * ratio;
                hit.m_damage.m_pierce = weaponDamage.m_pierce * ratio;

                hit.m_point = monster.GetCenterPoint();
                hit.m_dir = (monster.transform.position - player.transform.position).normalized;
                hit.m_attacker = player.GetZDOID();
                hit.SetAttacker(player);
                hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                monster.Damage(hit);
                hitCount++;

                // 개별 타격 VFX
                SimpleVFX.Play("fx_sword_hit", monster.GetCenterPoint(), 1f);
            }

            // 5. 플로팅 텍스트
            string attackText = attackNumber switch
            {
                1 => $"⚔️ 1차 베기! ({(int)damageRatio}%)",
                2 => $"⚔️ 2차 베기! ({(int)damageRatio}%)",
                3 => $"💥 피니셔! ({(int)damageRatio}%)",
                _ => $"⚔️ 베기! ({(int)damageRatio}%)"
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
        /// 카메라 기준 뒤쪽 위치 계산 (몬스터 뒤)
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

                if (rushSlashCoroutines.ContainsKey(player) && rushSlashCoroutines[player] != null)
                {
                    player.StopCoroutine(rushSlashCoroutines[player]);
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

                Plugin.Log.LogDebug($"[Sword Skill] {player.GetPlayerName()} 모든 검 스킬 상태 초기화 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Skill] 상태 초기화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 슬래쉬 스킬 활성화 상태 확인 (호환성용)
        /// </summary>
        public static bool IsSlashActive(Player player)
        {
            if (player == null) return false;
            return rushSlashActive.TryGetValue(player, out bool active) && active;
        }

        /// <summary>
        /// 검 베기 액티브 스킬 사망 시 정리 시스템
        /// </summary>
        public static void CleanupSwordSkillOnDeath(Player player)
        {
            try
            {
                rushSlashCooldowns.Remove(player);
                rushSlashActive.Remove(player);
                rushSlashEndTime.Remove(player);

                if (rushSlashCoroutines.ContainsKey(player) && rushSlashCoroutines[player] != null)
                {
                    try
                    {
                        Plugin.Instance?.StopCoroutine(rushSlashCoroutines[player]);
                    }
                    catch { }
                    rushSlashCoroutines.Remove(player);
                }

                rushSlashAttackCount.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Sword Skill] 정리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 검 전문가 - 공격력 보너스 (비율)
        /// 실제 효과는 ItemData.GetDamage 패치에서 적용됨
        /// </summary>
        public static float GetSwordExpertDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_expert") || !Sword_Skill.IsUsingSword(player))
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
        /// 실제 효과는 ItemData.GetDamage 패치에서 적용됨
        /// </summary>
        public static float GetSwordRiposteDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_step3_riposte") || !Sword_Skill.IsUsingSword(player)) return 0f;

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

        // === 패링 돌격 (Parry Rush) 액티브 스킬 관련 변수 ===
        private static Dictionary<Player, float> parryRushCooldowns = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> parryRushActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> parryRushExpiry = new Dictionary<Player, float>();

        /// <summary>
        /// G키로 패링 돌격 30초 버프 활성화
        /// - 스킬 보유 확인 (sword_step5_defswitch)
        /// - 방패 착용 확인
        /// - 쿨타임/스태미나 확인
        /// - 버프 활성화 + VFX 재생
        /// </summary>
        public static void ActivateParryRush(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                // 1. 스킬 보유 확인
                if (!SkillEffect.HasSkill("sword_step5_defswitch"))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("parry_rush_skill_required"), Color.red);
                    return;
                }

                // 2. 방패 착용 확인
                if (!HasShield(player))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("shield_required"), Color.red);
                    return;
                }

                // 3. 쿨타임 확인
                float now = Time.time;
                if (parryRushCooldowns.TryGetValue(player, out float cdEnd) && now < cdEnd)
                {
                    float remaining = cdEnd - now;
                    SkillEffect.DrawFloatingText(player, L.Get("cooldown_remaining", Mathf.CeilToInt(remaining)), Color.yellow);
                    return;
                }

                // 4. 스태미나 확인
                float staminaCost = Sword_Config.ParryRushStaminaCostValue;
                if (player.GetStamina() < staminaCost)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // 5. 이미 버프 활성 중인지 확인
                if (IsParryRushActive(player))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("parry_rush_already_active"), Color.yellow);
                    return;
                }

                // 6. 버프 활성화
                float duration = Sword_Config.ParryRushDurationValue;
                float cooldown = Sword_Config.ParryRushCooldownValue;

                parryRushActive[player] = true;
                parryRushExpiry[player] = now + duration;
                parryRushCooldowns[player] = now + cooldown;

                // 7. 스태미나 소모
                player.UseStamina(staminaCost);

                // 8. 버프 표시
                if (Gui.SkillBuffDisplay.Instance != null)
                {
                    Gui.SkillBuffDisplay.Instance.ShowBuff(
                        "parry_rush_buff",
                        "패링 돌격",
                        duration,
                        Color.cyan
                    );
                }

                // 9. 발동 메시지 + VFX
                SkillEffect.DrawFloatingText(player, "🛡️ " + L.Get("parry_rush_activate", duration), Color.cyan);
                VFXManager.PlayVFXMultiplayer("vfx_blocked", "", player.transform.position, player.transform.rotation, duration);

                Plugin.Log.LogInfo($"[패링 돌격] 버프 활성화 - {duration}초, 쿨타임 {cooldown}초");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[패링 돌격] 버프 활성화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 패링 성공 시 돌격 실행 - 방패 들고 몬스터에게 돌진, 데미지 + 밀어내기
        /// </summary>
        public static void OnParryRushTrigger(Player player, Character attacker)
        {
            try
            {
                if (player == null || player.IsDead() || attacker == null || attacker.IsDead()) return;
                if (!IsParryRushActive(player)) return;

                // 무기 확인 (방패 패링 중 GetCurrentWeapon이 방패를 반환할 수 있으므로 장비 목록에서 검 확인)
                var weapon = GetEquippedSword(player);
                if (weapon == null) return;

                // 돌격 코루틴 시작
                var coroutine = ExecuteParryRushCharge(player, attacker, weapon);
                player.StartCoroutine(coroutine);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[패링 돌격] 돌격 트리거 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 패링 돌격 차지 코루틴 - blocking 모션으로 몬스터까지 돌진 후 데미지 + 밀어내기
        /// </summary>
        private static IEnumerator ExecuteParryRushCharge(Player player, Character target, ItemDrop.ItemData weapon)
        {
            if (player == null || target == null) yield break;

            // 1. blocking 모션 시작
            var zanim = player.GetComponentInChildren<ZSyncAnimation>();
            if (zanim != null)
            {
                zanim.SetBool("blocking", true);
            }

            // 2. 몬스터 방향으로 회전
            Vector3 targetPos = target.transform.position;
            Vector3 direction = (targetPos - player.transform.position);
            direction.y = 0;
            if (direction.sqrMagnitude > 0.001f)
            {
                player.transform.rotation = Quaternion.LookRotation(direction.normalized);
            }

            // 3. 몬스터까지 빠르게 이동 (20m/s)
            float distance = Vector3.Distance(player.transform.position, targetPos);
            float moveSpeed = 20f;
            float moveDuration = Mathf.Max(distance / moveSpeed, 0.05f);
            float elapsed = 0f;
            Vector3 startPos = player.transform.position;

            while (elapsed < moveDuration)
            {
                if (player == null || player.IsDead()) yield break;

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / moveDuration);
                float smoothT = 1f - Mathf.Pow(1f - t, 2f);

                // 타겟 위치 갱신 (이동했을 수 있음)
                if (target != null && !target.IsDead())
                {
                    targetPos = target.transform.position;
                }

                Vector3 newPos = Vector3.Lerp(startPos, targetPos, smoothT);
                // 지면 높이 보정
                if (Physics.Raycast(newPos + Vector3.up * 5f, Vector3.down, out RaycastHit groundHit, 10f,
                    LayerMask.GetMask("terrain", "Default")))
                {
                    newPos.y = groundHit.point.y + 0.1f;
                }
                // transform.position 직접 설정 시 Rigidbody가 다음 FixedUpdate에서 덮어씌워 제자리 복귀 발생
                // Traverse로 protected m_body 접근 후 MovePosition으로 물리 엔진에 위치 변경 전달
                var body = HarmonyLib.Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();
                if (body != null)
                {
                    body.velocity = Vector3.zero;
                    body.MovePosition(newPos);
                }
                else
                {
                    player.transform.position = newPos;
                }

                yield return new WaitForFixedUpdate();
            }

            // 4. 도착 - 데미지 적용
            if (target != null && !target.IsDead() && player != null && !player.IsDead())
            {
                var weaponDamage = weapon.GetDamage();
                float damageMultiplier = 1f + (Sword_Config.ParryRushDamageBonusValue / 100f);

                var hit = new HitData();
                hit.m_damage.m_slash = weaponDamage.m_slash * damageMultiplier;
                hit.m_damage.m_blunt = weaponDamage.m_blunt * damageMultiplier;
                hit.m_damage.m_pierce = weaponDamage.m_pierce * damageMultiplier;

                hit.m_point = target.GetCenterPoint();
                hit.m_dir = (target.transform.position - player.transform.position).normalized;
                hit.m_pushForce = Sword_Config.ParryRushPushDistanceValue;
                hit.m_attacker = player.GetZDOID();
                hit.SetAttacker(player);
                hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                target.Damage(hit);

                // VFX: 적중 효과
                VFXManager.PlayVFXMultiplayer("vfx_sledge_hit", "", target.GetCenterPoint(), Quaternion.identity, 2f);

                SkillEffect.DrawFloatingText(player, "🛡️ " + L.Get("parry_rush_damage", Sword_Config.ParryRushDamageBonusValue), Color.cyan);
                Plugin.Log.LogInfo($"[패링 돌격] 돌격 성공! 공격력 +{Sword_Config.ParryRushDamageBonusValue}%, 밀어내기 {Sword_Config.ParryRushPushDistanceValue}m");
            }

            // 5. blocking 해제
            yield return new WaitForSeconds(0.2f);
            if (zanim != null && player != null && !player.IsDead())
            {
                zanim.SetBool("blocking", false);
            }
        }

        /// <summary>
        /// 패링 돌격 버프 활성 상태 확인
        /// </summary>
        public static bool IsParryRushActive(Player player)
        {
            if (player == null) return false;
            return parryRushActive.TryGetValue(player, out bool active) && active &&
                   parryRushExpiry.TryGetValue(player, out float expiry) && Time.time < expiry;
        }

        /// <summary>
        /// 패링 돌격 상태 정리
        /// </summary>
        public static void CleanupParryRush(Player player)
        {
            if (player == null) return;
            parryRushActive.Remove(player);
            parryRushExpiry.Remove(player);

            if (Gui.SkillBuffDisplay.Instance != null)
            {
                Gui.SkillBuffDisplay.Instance.RemoveBuff("parry_rush_buff");
            }
        }

        /// <summary>
        /// 패링 돌격 사망 시 정리
        /// </summary>
        public static void CleanupParryRushOnDeath(Player player)
        {
            try
            {
                parryRushCooldowns.Remove(player);
                parryRushActive.Remove(player);
                parryRushExpiry.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[패링 돌격] 사망 정리 실패: {ex.Message}");
            }
        }

    }

    /// <summary>
    /// 검 공격력 증가 패치 (전문가, 단계별 스킬)
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class SwordDamageBonus_Patch
    {
        /// <summary>
        /// Sword Slash 액티브 스킬 데미지 배율만 처리
        /// 일반 공격력 보너스는 GetDamage 패치로 이동 (Rule 13)
        /// </summary>
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (hit.GetAttacker() is Player player && Sword_Skill.IsUsingSword(player))
                {
                    // Rush Slash 액티브 데미지 배율 적용
                    if (Sword_Skill.IsSwordSlashActive(player))
                    {
                        float damageRatio = Sword_Config.RushSlash1stDamageRatioValue / 100f;
                        hit.m_damage.m_slash *= damageRatio;

                        Plugin.Log.LogDebug($"[Rush Slash] 액티브 배율 {damageRatio:F2}x (slash)");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 데미지 패치] 오류: {ex.Message}");
            }
        }

        // GetSwordDefenseSwitchDamageReduction 제거됨 - 패링 돌격 액티브 스킬로 전환
    }
}
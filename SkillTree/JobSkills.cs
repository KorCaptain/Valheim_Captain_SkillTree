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
    /// 직업별 액티브 스킬 시스템
    /// Y키로 발동되는 6개 직업의 고유 스킬들을 관리
    /// </summary>
    public static class JobSkills
    {
        /// <summary>
        /// 모든 직업 스킬들을 SkillTreeManager에 등록
        /// </summary>
        public static void RegisterJobSkills()
        {
            var manager = SkillTreeManager.Instance;
            
            // 버서커 (동적 툴팁 시스템)
            manager.AddSkill(new SkillNode {
                Id = "Berserker",
                Name = "버서커",
                Description = Berserker_Tooltip.GetBerserkerTooltip(),
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(465, -90),
                Category = "직업",
                IconName = "Berserker_unlock",
                IconNameLocked = "Berserker_lock",
                IconNameUnlocked = "Berserker_unlock",
                NextNodes = new List<string>(),
                RequiredPlayerLevel = 10,
                ApplyEffect = (lv) => { }
            });
            
            // 탱커 (별도 파일에서 관리)
            TankerSkills.RegisterTankerSkill();
            
            // 로그 (별도 파일에서 관리)
            RogueSkills.RegisterRogueSkill();
            
            // 아처 (멀티샷 버프 시스템)
            manager.AddSkill(new SkillNode {
                Id = "Archer",
                Name = "아처",
                Description = Archer_Tooltip.GetArcherTooltip(),
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(-375, 395),
                Category = "직업",
                IconName = "Archer_unlock",
                IconNameLocked = "Archer_lock",
                IconNameUnlocked = "Archer_unlock",
                NextNodes = new List<string>(),
                RequiredPlayerLevel = 10,
                ApplyEffect = (lv) => { }
            });
            
            // 메이지 (동적 툴팁 시스템)
            manager.AddSkill(new SkillNode {
                Id = "Mage",
                Name = "메이지",
                Description = Mage_Tooltip.GetMageTooltip(),
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(-490, 150),
                Category = "직업",
                IconName = "Mage_unlock",
                IconNameLocked = "Mage_lock",
                IconNameUnlocked = "Mage_unlock",
                NextNodes = new List<string>(),
                RequiredPlayerLevel = 10,
                ApplyEffect = (lv) => { }
            });
            
            // 성기사 (컨피그 기반 동적 툴팁)
            manager.AddSkill(new SkillNode {
                Id = "성기사",
                Name = "성기사",
                Description = Paladin_Config.GetPaladinTooltip(),
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(-490, -100),
                Category = "직업",
                IconName = "paladin_unlock",
                IconNameLocked = "paladin_lock",
                IconNameUnlocked = "paladin_unlock",
                NextNodes = new List<string>(),
                RequiredPlayerLevel = 10,
                ApplyEffect = (lv) => { }
            });
            
            // 스킬 등록 완료 후 동적 툴팁 강제 업데이트 (동적 툴팁 적용 보장)
            try
            {
                Archer_Tooltip.UpdateArcherTooltip();
                Plugin.Log.LogDebug("[아처] 아처 툴팁 완료");
                
                Tanker_Tooltip.UpdateTankerTooltip();
                Plugin.Log.LogDebug("[탱커] 탱커 툴팁 완료");
                
                Rogue_Tooltip.UpdateRogueTooltip();
                Plugin.Log.LogDebug("[로그] 로그 툴팁 완료");
                
                Berserker_Tooltip.UpdateBerserkerTooltip();
                Plugin.Log.LogDebug("[버서커] 버서커 툴팁 완료");
                
                Mage_Tooltip.UpdateMageTooltip();
                Plugin.Log.LogDebug("[메이지] 메이지 툴팁 완료");
                
                UpdatePaladinTooltip();
                Plugin.Log.LogDebug("[성기사] 성기사 툴팁 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[직업 스킬 등록] 동적 툴팁 강제 업데이트 실패: {ex.Message}");
            }
        }
        
        // === 헬퍼 함수들 ===
        
        
        /// <summary>
        /// 아처 툴팁 업데이트 (컨피그 변경 시 호출)
        /// </summary>
        public static void UpdateArcherTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[아처 툴팁] UpdateArcherTooltip 호출됨");
                
                // 1. Job_Tooltip 시스템을 통한 업데이트
                Archer_Tooltip.UpdateArcherTooltip();
                
                // 2. 직접 SkillTreeManager에서 업데이트
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Archer"))
                {
                    var newTooltip = Archer_Tooltip.GetArcherTooltip();
                    manager.SkillNodes["Archer"].Description = newTooltip;
                    Plugin.Log.LogDebug($"[아처 툴팁] 직접 업데이트 완료 - 새 툴팁 길이: {newTooltip?.Length ?? 0}");
                }
                else
                {
                    Plugin.Log.LogWarning("[아처 툴팁] SkillTreeManager 또는 Archer 노드가 없음");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 버서커 툴팁 업데이트 (컨피그 변경 시 호출)
        /// </summary>
        public static void UpdateBerserkerTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[버서커 툴팁] UpdateBerserkerTooltip 호출됨");
                
                // 1. Berserker_Tooltip 시스템을 통한 업데이트
                Berserker_Tooltip.UpdateBerserkerTooltip();
                
                // 2. 직접 SkillTreeManager에서 업데이트
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Berserker"))
                {
                    var newTooltip = Berserker_Tooltip.GetBerserkerTooltip();
                    manager.SkillNodes["Berserker"].Description = newTooltip;
                    Plugin.Log.LogDebug($"[버서커 툴팁] 직접 업데이트 완료 - 새 툴팁 길이: {newTooltip?.Length ?? 0}");
                }
                else
                {
                    Plugin.Log.LogWarning("[버서커 툴팁] SkillTreeManager 또는 Berserker 노드가 없음");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 성기사 툴팁 업데이트 (컨피그 변경 시 호출)
        /// </summary>
        public static void UpdatePaladinTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[성기사 툴팁] UpdatePaladinTooltip 호출됨");
                
                // 직접 SkillTreeManager에서 업데이트
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("성기사"))
                {
                    var newTooltip = Paladin_Config.GetPaladinTooltip();
                    manager.SkillNodes["성기사"].Description = newTooltip;
                    Plugin.Log.LogDebug($"[성기사 툴팁] 직접 업데이트 완료 - 새 툴팁 길이: {newTooltip?.Length ?? 0}");
                }
                else
                {
                    Plugin.Log.LogWarning("[성기사 툴팁] SkillTreeManager 또는 성기사 노드가 없음");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[성기사 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 한손 근접무기 착용 여부 확인 (성기사 조건)
        /// </summary>
        private static bool IsUsingOneHandedMeleeWeapon(Player player)
        {
            var weapon = player.GetCurrentWeapon();
            if (weapon == null) return false;
            
            // 한손 근접무기 스킬 타입들
            var oneHandedMeleeTypes = new[]
            {
                Skills.SkillType.Swords,     // 검
                Skills.SkillType.Knives,     // 단검
                Skills.SkillType.Clubs,      // 둔기 (한손)
                Skills.SkillType.Axes        // 도끼 (한손)
            };
            
            var skillType = weapon.m_shared?.m_skillType;
            bool isOneHandedMelee = oneHandedMeleeTypes.Contains(skillType ?? Skills.SkillType.None);
            
            Plugin.Log.LogInfo($"[성기사 무기 체크] 무기: {weapon.m_shared?.m_name}, 스킬타입: {skillType}, 한손근접: {isOneHandedMelee}");
            
            return isOneHandedMelee;
        }
        
        // === 직업별 쿨타임 및 상태 변수 ===
        
        // 성기사 (Paladin -> Holy Knight) - 신성한 치유 (컨피그 시스템 연동)
        private static float holyKnightHealCooldownEnd = 0f;
        
        // 성기사 지속 힐링 관리
        private static Dictionary<Player, Coroutine> activeHealCoroutines = new Dictionary<Player, Coroutine>();
        
        // 탱커 관련 변수들은 TankerSkills.cs로 이동됨
        
        // Berserker - 버서커의 분노
        private static bool berserkerRageActive = false;
        private static float berserkerRageEndTime = 0f;
        private static float berserkerRageDamageBonus = 0f;
        private const float BerserkerRageStaminaCost = 0.15f;
        
        // Rogue - 그림자 일격
        private static float rogueShadowCooldownEnd = 0f;
        private static float rogueShadowEndTime = 0f;
        private static bool rogueShadowActive = false;
        private const float RogueShadowCooldown = 30f;
        private const float RogueShadowDuration = 5f;
        private const float RogueShadowDamageBonus = 0.25f;
        private const float RogueShadowStaminaCost = 0.15f;
        
        // Mage - 마법 폭발
        private static float mageBurstCooldownEnd = 0f;
        private static float mageBurstEndTime = 0f;
        private static bool mageBurstActive = false;
        private const float MageBurstCooldown = 30f;
        private const float MageBurstDuration = 10f;
        private const float MageBurstDamageBonus = 0.1f;
        private const float MageBurstRangeIncrease = 7f;
        private const float MageBurstEitrCost = 0.1f;
        
        // Archer - 멀티샷 (사용하지 않음 - MULTISHOT.cs에서 관리)
        // private static float archerMultiShotCooldownEnd = 0f;

        // === 직업 스킬 실행 메인 함수 ===
        
        /// <summary>
        /// Y키 눌렀을 때 직업 스킬 실행 (성능 최적화)
        /// </summary>
        public static void ExecuteJobSkill(Player player)
        {
            if (player == null || player.IsDead())
            {
                return;
            }

            float currentTime = Time.time;
            var manager = SkillTreeManager.Instance;

            if (manager == null)
            {
                return;
            }

            // 디버그: 모든 직업 레벨 확인
            var holyKnightLevel = manager.GetSkillLevel("성기사");
            var tankerLevel = manager.GetSkillLevel("Tanker");
            var berserkerLevel = manager.GetSkillLevel("Berserker");
            var rogueLevel = manager.GetSkillLevel("Rogue");
            var mageLevel = manager.GetSkillLevel("Mage");
            var archerLevel = manager.GetSkillLevel("Archer");

            // 성능 최적화: 첫 번째로 찾은 직업만 실행
            if (holyKnightLevel > 0)
            {
                ExecuteHolyKnightHeal(player, currentTime);
                return;
            }

            if (tankerLevel > 0)
            {
                TankerSkills.ExecuteTankerTaunt(player);
                return;
            }

            if (manager.GetSkillLevel("Berserker") > 0)
            {
                BerserkerSkills.CastBerserkerRage(player);
                return;
            }

            if (manager.GetSkillLevel("Rogue") > 0)
            {
                RogueSkills.ExecuteRogueShadowStrike(player);
                return;
            }

            if (manager.GetSkillLevel("Mage") > 0)
            {
                MageSkills.TryExecuteMageActiveSkill(player);
                return;
            }

            // 아처 멀티샷 실행
            if (manager.GetSkillLevel("Archer") > 0)
            {
                SkillEffect.ExecuteArcherMultiShot(player);
                return;
            }

            // 직업 미선택 시에만 메시지 (한 번만)
            ShowMessage(player, "직업을 선택하지 않았습니다.");
        }

        // === 개별 직업 스킬 구현 ===

        /// <summary>
        /// 성기사 - 신성한 치유 (컨피그 시스템 연동)
        /// 컨피그에서 설정된 값에 따라 아군을 지속 힐링하고 시전자는 즉시 회복
        /// 필요조건: 한손 근접무기 착용, 성기사
        /// </summary>
        private static void ExecuteHolyKnightHeal(Player player, float currentTime)
        {
            // 한손 근접무기 착용 확인
            if (!IsUsingOneHandedMeleeWeapon(player))
            {
                ShowMessage(player, "한손 근접무기를 착용해야 합니다!");
                return;
            }
            
            // 쿨타임 확인 (컨피그에서 설정)
            if (currentTime < holyKnightHealCooldownEnd)
            {
                float remaining = holyKnightHealCooldownEnd - currentTime;
                ShowMessage(player, $"신성한 치유 쿨타임: {remaining:F1}초");
                return;
            }

            // 자원 확인 (컨피그에서 설정)
            float requiredStamina = Paladin_Config.StaminaCostValue;
            float requiredEitr = Paladin_Config.EitrCostValue;
            
            if (player.GetStamina() < requiredStamina)
            {
                ShowMessage(player, $"스태미나가 부족합니다 ({requiredStamina} 필요)");
                return;
            }

            if (player.GetEitr() < requiredEitr)
            {
                ShowMessage(player, $"에이트르가 부족합니다 ({requiredEitr} 필요)");
                return;
            }

            // 자원 소모
            player.UseStamina(requiredStamina);
            player.UseEitr(requiredEitr);

            // 1. 스킬 발동 시 이펙트 및 사운드 (하이브리드 VFX 방식으로 모든 유저가 볼 수 있도록)
            try
            {
                Vector3 playerPos = player.transform.position;
                
                // ZRoutedRpc를 통한 멀티플레이어 VFX/사운드
                SimpleVFX.PlayWithSound("shaman_heal_aoe", "sfx_dverger_heal_finish", playerPos, 3f);
                Plugin.Log.LogInfo("[성기사] shaman_heal_aoe VFX/사운드 재생");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[성기사] 하이브리드 이펙트/사운드 재생 실패: {ex.Message}");
            }

            // 2. 시전자 발밑에 힐링 효과 1회 표시 (Valheim 내장 VFX 사용)
            PlayPaladinActivationEffect(player);

            // 3. 시전자 즉시 회복 (컨피그 비율)
            float selfHealAmount = player.GetMaxHealth() * Paladin_Config.SelfHealPercentValue;
            player.Heal(selfHealAmount, true);
            
            // 시전자 본인에게도 힐링 오라 효과 추가 (도트 힐 지속시간과 동일)
            CreatePaladinAuraEffect(player, Paladin_Config.HealDurationValue);
            
            if (Paladin_Config.ShowHealNumbersValue)
            {
                ShowMessage(player, $"✨ 자가 치유: +{selfHealAmount:F0} HP");
            }

            // 4. 범위 내 다른 플레이어들에게 지속 힐링 적용 (컨피그 설정)
            var nearbyPlayers = Player.GetAllPlayers()
                .Where(p => p != player && Vector3.Distance(p.transform.position, player.transform.position) <= Paladin_Config.RangeValue && !p.IsDead())
                .ToList();

            int healedCount = nearbyPlayers.Count;
            
            foreach (var targetPlayer in nearbyPlayers)
            {
                // 기존 힐링 코루틴이 있으면 정지
                if (activeHealCoroutines.ContainsKey(targetPlayer))
                {
                    if (activeHealCoroutines[targetPlayer] != null)
                        player.StopCoroutine(activeHealCoroutines[targetPlayer]);
                    activeHealCoroutines.Remove(targetPlayer);
                }

                // 새로운 지속 힐링 시작
                var healCoroutine = player.StartCoroutine(ContinuousHealCoroutine(targetPlayer));
                activeHealCoroutines[targetPlayer] = healCoroutine;
            }

            // 쿨타임 설정 (컨피그에서 설정)
            holyKnightHealCooldownEnd = currentTime + Paladin_Config.CooldownValue;
            
            ShowMessage(player, $"⭐ 성기사 신성한 치유! (자가치유 + {healedCount}명 지속힐)");
            
            // 기존 샤먼 이팩트 유지
            PlayJobEffect(player, "fx_greydwarf_shaman_heal", player.transform.position);
        }


        /// <summary>
        /// 지속 힐링 코루틴 (컨피그 설정에 따른 지속 회복)
        /// </summary>
        private static IEnumerator ContinuousHealCoroutine(Player target)
        {
            if (target == null || target.IsDead())
            {
                yield break;
            }

            float elapsed = 0f;
            int healTicks = 0;
            float healDuration = Paladin_Config.HealDurationValue;
            float healInterval = Paladin_Config.HealIntervalValue;
            float healPercentPerTick = Paladin_Config.AllyHealPercentValue;
            int totalTicks = Paladin_Config.TotalHealTicks;
            bool showNumbers = Paladin_Config.ShowHealNumbersValue;
            bool showProgress = Paladin_Config.ShowHealProgressValue;
            
            // 성기사 전용 힐링 오라 효과 추가 (도트 힐 동안 지속)
            GameObject personalAuraEffect = null;
            try
            {
                personalAuraEffect = CreatePaladinAuraEffect(target, healDuration);

                // ZRoutedRpc를 통한 멀티플레이어 사운드
                SimpleVFX.Play("sfx_dverger_heal_finish", target.transform.position, 2f);

                Plugin.Log.LogInfo($"[성기사] {target.GetPlayerName()}에게 힐링 오라 및 사운드 생성 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[성기사] aura 생성 실패: {ex.Message}");
            }
            
            if (showNumbers)
            {
                ShowMessage(target, "🌟 지속 치유 시작!");
            }
            
            while (elapsed < healDuration && target != null && !target.IsDead())
            {
                // 매 간격마다 회복
                try
                {
                    float healAmount = target.GetMaxHealth() * healPercentPerTick;
                    target.Heal(healAmount, true);
                    healTicks++;
                    
                    // 진행상황 및 수치 표시 (컨피그 설정에 따라)
                    if (showNumbers)
                    {
                        string message = $"💚 지속 치유: +{healAmount:F0} HP";
                        if (showProgress)
                        {
                            message += $" ({healTicks}/{totalTicks})";
                        }
                        ShowMessage(target, message);
                    }
                    
                    // ZRoutedRpc를 통한 힐링 사운드
                    try
                    {
                        SimpleVFX.Play("sfx_dverger_heal_finish", target.transform.position, 1f);
                    }
                    catch (Exception soundEx)
                    {
                        Plugin.Log.LogError($"[성기사] 힐링 사운드 재생 실패: {soundEx.Message}");
                    }
                    
                    Plugin.Log.LogInfo($"[성기사] 지속 힐링 적용 - {target.GetPlayerName()}: +{healAmount:F0} HP ({healTicks}/{totalTicks})");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[성기사] 지속 힐링 적용 실패: {ex.Message}");
                    break;
                }
                
                yield return new WaitForSeconds(healInterval);
                elapsed += healInterval;
            }
            
            // 지속 힐링 완료
            if (target != null && !target.IsDead() && showNumbers)
            {
                ShowMessage(target, "✨ 지속 치유 완료!");
                PlayJobEffect(target, "fx_greydwarf_shaman_heal", target.transform.position); // 치유 효과로 변경
            }
            
            // 완료된 코루틴 제거
            if (activeHealCoroutines.ContainsKey(target))
            {
                activeHealCoroutines.Remove(target);
            }
            
            Plugin.Log.LogInfo($"[성기사] 지속 힐링 완료 - {target?.GetPlayerName() ?? "Unknown"} (총 {healTicks}회 힐링)");
        }




        /// <summary>
        /// 성기사 스킬 활성화 시 힐링 효과 1회 표시 (캐릭터 발밑)
        /// Valheim 내장 VFX 사용 (buff_03a 대체)
        /// </summary>
        private static void PlayPaladinActivationEffect(Player player)
        {
            try
            {
                // 캐릭터 발밑 위치 계산
                var footPosition = player.transform.position + Vector3.down * 0.1f;

                // Valheim 내장 VFX 사용 (ZNetView 충돌 방지)
                SimpleVFX.Play("vfx_Potion_health_medium", footPosition, 2f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[성기사] 힐링 활성화 효과 생성 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 성기사 도트 힐 받는 캐릭터에게 힐링 오라 효과 생성 (도트 힐 동안 지속)
        /// Valheim 내장 VFX 사용 (buff_03a_aura 대체)
        /// </summary>
        private static GameObject CreatePaladinAuraEffect(Player target, float duration)
        {
            try
            {
                // 캐릭터 발밑 위치 계산
                var footPosition = target.transform.position + Vector3.down * 0.1f;

                // Valheim 내장 VFX 사용 (ZNetView 충돌 방지)
                SimpleVFX.Play("vfx_Potion_health_medium", footPosition, duration);

                return null;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[성기사] 힐링 오라 효과 생성 실패: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 성기사 aura 효과 지속시간 후 제거하는 코루틴
        /// </summary>
        private static IEnumerator DestroyPaladinAuraAfterDelay(GameObject auraEffect, Player target, float duration)
        {
            if (auraEffect == null || target == null)
                yield break;

            Plugin.Log.LogInfo($"[성기사] {target.GetPlayerName()}의 힐링 오라 시작 - {duration}초간 지속");

            // 지정된 시간 동안 대기
            yield return new WaitForSeconds(duration);

            // ✅ 플레이어 사망 체크 추가 (대기 후)
            if (target == null || target.IsDead())
            {
                if (auraEffect != null)
                {
                    UnityEngine.Object.Destroy(auraEffect);
                }
                Plugin.Log.LogInfo($"[성기사] 플레이어 사망으로 힐링 오라 조기 제거");
                yield break;
            }

            // 지속시간 종료 시 제거
            if (auraEffect != null)
            {
                UnityEngine.Object.Destroy(auraEffect);
                Plugin.Log.LogInfo($"[성기사] {target?.GetPlayerName() ?? "Unknown"}의 힐링 오라 제거 완료 ({duration}초 지속)");
            }
        }

        // 탱커 관련 함수들은 TankerSkills.cs로 완전 이동됨

        /// <summary>
        /// Berserker - 버서커의 분노 (새로운 BerserkerSkills 시스템 연동)
        /// 20초 동안 체력 비례 데미지 보너스 제공
        /// </summary>
        private static void ExecuteBerserkerRage(Player player, float currentTime)
        {
            try
            {
                // 새로운 BerserkerSkills 시스템으로 위임
                BerserkerSkills.CastBerserkerRage(player);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[버서커] ExecuteBerserkerRage 실행 중 오류: {ex.Message}");
                ShowMessage(player, "스킬 시전 중 오류가 발생했습니다.");
            }
        }

        /// <summary>
        /// Rogue - 그림자 일격
        /// 연막과 함께 몬스터 어그로 완전 제거, 5초간 공격력 25% 증가
        /// </summary>
        private static void ExecuteRogueShadow(Player player, float currentTime)
        {
            // 쿨타임 확인
            if (currentTime < rogueShadowCooldownEnd)
            {
                float remaining = rogueShadowCooldownEnd - currentTime;
                ShowMessage(player, $"그림자 일격 쿨타임: {remaining:F1}초");
                return;
            }

            // 자원 확인
            if (player.GetStamina() < player.GetMaxStamina() * RogueShadowStaminaCost)
            {
                ShowMessage(player, "스태미나가 부족합니다.");
                return;
            }

            // 자원 소모
            player.UseStamina(player.GetMaxStamina() * RogueShadowStaminaCost);

            // 모든 적의 어그로 제거
            var enemies = Character.GetAllCharacters()
                .Where(c => c.IsMonsterFaction(0f))
                .ToList();

            foreach (var enemy in enemies)
            {
                var ai = enemy.GetComponent<BaseAI>();
                if (ai != null)
                {
                    try
                    {
                        // 리플렉션으로 타겟 확인 및 제거
                        var getTargetMethod = ai.GetType().GetMethod("GetTarget");
                        var setTargetMethod = ai.GetType().GetMethod("SetTarget");
                        var setAggravatedMethod = ai.GetType().GetMethod("SetAggravated");

                        if (getTargetMethod != null && setTargetMethod != null)
                        {
                            var currentTarget = getTargetMethod.Invoke(ai, null);
                            if (currentTarget != null && currentTarget.Equals(player))
                            {
                                setTargetMethod.Invoke(ai, new object?[] { null });
                                
                                if (setAggravatedMethod != null)
                                {
                                    setAggravatedMethod.Invoke(ai, new object[] { false, BaseAI.AggravatedReason.Damage });
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Plugin.Log.LogWarning($"[Rogue 어그로] {enemy.name} 어그로 제거 실패: {ex.Message}");
                    }
                }
            }

            // 그림자 상태 활성화
            rogueShadowActive = true;
            rogueShadowEndTime = currentTime + RogueShadowDuration;

            // 쿨타임 설정
            rogueShadowCooldownEnd = currentTime + RogueShadowCooldown;
            
            ShowMessage(player, "🗡️ 그림자 일격! (어그로 제거 + 5초간 데미지 +25%)");
            PlayJobEffect(player, "fx_Lightning", player.transform.position);
        }

        /// <summary>
        /// Mage - 마법 폭발
        /// 10초간 마법 데미지 10% 증가, 광역범위 7m 증가
        /// </summary>
        private static void ExecuteMageBurst(Player player, float currentTime)
        {
            // 쿨타임 확인
            if (currentTime < mageBurstCooldownEnd)
            {
                float remaining = mageBurstCooldownEnd - currentTime;
                ShowMessage(player, $"마법 폭발 쿨타임: {remaining:F1}초");
                return;
            }

            // 자원 확인
            if (player.GetEitr() < player.GetMaxEitr() * MageBurstEitrCost)
            {
                ShowMessage(player, "에이트르가 부족합니다.");
                return;
            }

            // 자원 소모
            player.UseEitr(player.GetMaxEitr() * MageBurstEitrCost);

            // 마법 폭발 상태 활성화
            mageBurstActive = true;
            mageBurstEndTime = currentTime + MageBurstDuration;

            // 쿨타임 설정
            mageBurstCooldownEnd = currentTime + MageBurstCooldown;
            
            ShowMessage(player, "🔮 마법 폭발! (10초간 마법 데미지 +10%, 범위 +7m)");
            PlayJobEffect(player, "fx_fader_meteor_hit", player.transform.position);
        }

        // 아처 멀티샷 실행 함수 주석 처리 (아이콘 문제 해결 테스트용)
        /*
        /// <summary>
        /// Archer - 멀티샷
        /// 4발의 추가 화살 발사 (각 화살 70% 데미지)
        /// </summary>
        private static void ExecuteArcherMultiShot(Player player, float currentTime)
        {
            // 쿨타임 확인
            if (currentTime < archerMultiShotCooldownEnd)
            {
                float remaining = archerMultiShotCooldownEnd - currentTime;
                ShowMessage(player, $"멀티샷 쿨타임: {remaining:F1}초");
                return;
            }

            // 스태미나 확인
            float maxStamina = player.GetMaxStamina();
            float requiredStamina = maxStamina * (Archer_Config.ArcherMultiShotStaminaCostValue / 100f);
            if (player.GetStamina() < requiredStamina)
            {
                ShowMessage(player, "스태미나 부족");
                return;
            }

            // 멀티샷 실행
            SkillEffect.ExecuteArcherMultiShot(player);

            // 쿨타임 설정
            archerMultiShotCooldownEnd = currentTime + Archer_Config.ArcherMultiShotCooldownValue;
            
            ShowMessage(player, $"🏹 멀틴샷! ({Archer_Config.ArcherMultiShotArrowCountValue + 1}발 발사)");
            PlayJobEffect(player, "fx_bow_hit", player.transform.position);
        }
        */

        // === 상태 확인 메서드들 ===

        public static bool IsBerserkerRageActive() => berserkerRageActive && Time.time < berserkerRageEndTime;
        public static bool IsRogueShadowActive() => rogueShadowActive && Time.time < rogueShadowEndTime;
        public static bool IsMageBurstActive() => mageBurstActive && Time.time < mageBurstEndTime;
        // 아처 멀티샷은 일회성 스킬이므로 상태 확인 메서드 불필요
        public static float GetBerserkerRageDamageBonus() => berserkerRageDamageBonus;
        
        /// <summary>
        /// 직업 스킬 상태 업데이트 (SkillTreeManager에서 호출)
        /// </summary>
        public static void UpdateJobSkillStates(Player player)
        {
            float currentTime = Time.time;
            
            // Berserker 분노 종료 체크
            if (berserkerRageActive && currentTime > berserkerRageEndTime)
            {
                berserkerRageActive = false;
                berserkerRageDamageBonus = 0f;
                if (player != null)
                    ShowMessage(player, "버서커의 분노 종료");
            }
            
            // Rogue 그림자 일격 종료 체크
            if (rogueShadowActive && currentTime > rogueShadowEndTime)
            {
                rogueShadowActive = false;
                if (player != null)
                    ShowMessage(player, "그림자 일격 종료");
            }
            
            // Mage 마법 폭발 종료 체크
            if (mageBurstActive && currentTime > mageBurstEndTime)
            {
                mageBurstActive = false;
                if (player != null)
                    ShowMessage(player, "마법 폭발 종료");
            }
            
            // Archer 멀티샷은 일회성 스킬이므로 상태 업데이트 불필요
        }

        // === 헬퍼 메서드들 ===

        private static bool HasJob(Player player, string jobName)
        {
            return SkillTreeManager.Instance.GetSkillLevel(jobName) > 0;
        }

        private static void ShowMessage(Player player, string message)
        {
            // 성능 최적화: 더 빠른 메시지 표시
            try
            {
                SkillEffect.DrawFloatingText(player, message);
            }
            catch
            {
                // 대체 방법: HUD 메시지
                try
                {
                    if (MessageHud.instance != null)
                    {
                        MessageHud.instance.ShowMessage(MessageHud.MessageType.TopLeft, message);
                    }
                }
                catch
                {
                    Plugin.Log.LogInfo($"[직업 스킬] {message}");
                }
            }
        }

        /// <summary>
        /// 직업 스킬 이펙트 재생 (하이브리드 VFX 방식으로 모든 유저 동기화)
        /// </summary>
        private static void PlayJobEffect(Player player, string effectName, Vector3 position)
        {
            try
            {
                // SimpleVFX 방식으로 VFX 재생
                SimpleVFX.Play(effectName, position, 3f);
                Plugin.Log.LogInfo($"[직업 이펙트] {effectName} VFX 재생");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[직업 이펙트] {effectName} 재생 실패: {ex.Message}");
            }
        }

        private static bool IsBossMonster(Character monster)
        {
            return monster?.m_name?.Contains("보스") == true || monster?.GetMaxHealth() > 1000f;
        }

        // =================== 공통 유틸리티 함수들 ===================
        
        /// <summary>
        /// 쿨다운 체크 (JobSkillsUtility 사용)
        /// </summary>
        private static bool IsOnCooldown(Player player, string skillName)
        {
            return JobSkillsUtility.IsOnCooldown(player, skillName);
        }

        /// <summary>
        /// 쿨다운 설정 (JobSkillsUtility 사용)
        /// </summary>
        private static void SetCooldown(Player player, string skillName, float cooldown)
        {
            JobSkillsUtility.SetCooldown(player, skillName, cooldown);
        }


        /// <summary>
        /// 직업 스킬 보유 여부 확인
        /// </summary>
        private static bool HasJobSkill(string jobName)
        {
            return JobSkillsUtility.HasJobSkill(jobName);
        }

        /// <summary>
        /// 활 사용 중인지 확인
        /// </summary>
        private static bool IsUsingBow(Player player)
        {
            return JobSkillsUtility.IsUsingBow(player);
        }

        /// <summary>
        /// 플레이어 사망 시 모든 직업 스킬 상태 정리
        /// </summary>
        public static void CleanupAllJobSkillsOnDeath(Player player)
        {
            try
            {

                // 0. 분노의 망치 코루틴 중단 (액티브 스킬)
                try
                {
                    FuryHammerSkill.CleanupFuryHammerOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[분노의 망치] 정리 실패 (무시): {ex.Message}");
                }

                // 0-1. 이중 시전 코루틴 중단 (액티브 스킬)
                try
                {
                    SkillEffect.CleanupStaffDualCastOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[이중 시전] 정리 실패 (무시): {ex.Message}");
                }

                // 0-2. Speed Tree 정리 (속도 전문가)
                try
                {
                    SkillEffect.CleanupSpeedTreeOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[Speed Tree] 정리 실패 (무시): {ex.Message}");
                }

                // 0-3. Melee Skills 정리 (단검, 검, 창 패시브)
                try
                {
                    SkillEffect.CleanupMeleeSkillsOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[Melee Skills] 정리 실패 (무시): {ex.Message}");
                }

                // 0-4. Sword Skill 정리 (검 베기 액티브 스킬)
                try
                {
                    Sword_Skill.CleanupSwordSkillOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[Sword Skill] 정리 실패 (무시): {ex.Message}");
                }

                // 0-5. Staff Heal 정리 (지팡이 힐 모드)
                try
                {
                    StaffHealingFireball.CleanupStaffHealOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[Staff Heal] 정리 실패 (무시): {ex.Message}");
                }

                // 0-6. Rogue Skills 정리 (로그 그림자 일격)
                try
                {
                    RogueSkills.CleanupRogueSkillsOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[Rogue Skills] 정리 실패 (무시): {ex.Message}");
                }

                // 0-7. Archer MultiShot 정리 (아처 멀티샷)
                try
                {
                    SkillEffect.CleanupArcherMultiShotOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[Archer MultiShot] 정리 실패 (무시): {ex.Message}");
                }

                // 0-8. 수호자의 진심 정리 (둔기 G키 액티브)
                try
                {
                    SkillEffect.CleanupGuardianHeartOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[수호자의 진심] 정리 실패 (무시): {ex.Message}");
                }

                // 0-9. 석궁 단 한 발 정리 (석궁 T키 액티브)
                try
                {
                    SkillEffect.CleanupCrossbowOneShotOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[석궁 단 한 발] 정리 실패 (무시): {ex.Message}");
                }

                // 0-10. 도발 정리 (탱커 Y키 액티브)
                try
                {
                    SkillEffect.CleanupTauntOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[도발] 정리 실패 (무시): {ex.Message}");
                }

                // 0-11. 폭발 화살 정리 (활 T키 액티브)
                try
                {
                    SkillEffect.CleanupExplosiveArrowOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[폭발 화살] 정리 실패 (무시): {ex.Message}");
                }

                // 0-12. 창 강화 투척 정리 (창 H키 액티브)
                try
                {
                    SkillEffect.CleanupSpearEnhancedThrowOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[창 강화 투척] 정리 실패 (무시): {ex.Message}");
                }

                // 1. 성기사 힐링 코루틴 중단
                if (activeHealCoroutines.ContainsKey(player) && activeHealCoroutines[player] != null)
                {
                    try
                    {
                        Plugin.Instance?.StopCoroutine(activeHealCoroutines[player]);
                    }
                    catch { }
                    try { activeHealCoroutines.Remove(player); } catch { }
                }

                // 2. 버서커 정리 (Berserker_Skill.cs 호출)
                try
                {
                    BerserkerSkills.SafeCleanup(player);
                }
                catch { }

                // 3. 탱커 정리 (TankerSkills.cs 호출) - ✅ 코루틴/VFX/도발 정리
                try
                {
                    TankerSkills.CleanupTankerOnDeath(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[탱커 스킬] 정리 실패 (무시): {ex.Message}");
                }

                // 4. 로그 정리 (RogueSkills.cs 호출)
                // TODO: 로그 코루틴 추적 시스템 추가 후 활성화

                // 5. 메이지 정리 (MageSkills.cs 호출)
                // TODO: 메이지 코루틴 추적 시스템 추가 후 활성화
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[직업 스킬 정리] 오류 (무시): {ex.Message}");
            }
        }

        /// <summary>
        /// Player.OnDestroy 패치 - 모든 직업 스킬 통합 정리
        /// </summary>
        [HarmonyPatch(typeof(Player), "OnDestroy")]
        public static class JobSkills_Player_OnDestroy_Patch
        {
            static void Postfix(Player __instance)
            {
                try
                {
                    if (__instance != null)
                    {
                        CleanupAllJobSkillsOnDeath(__instance);
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[직업 스킬 OnDestroy 패치] 오류: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        // ⚠️ Character.ApplyDamage 패치 비활성화 - 사망 시 너무 이른 시점에 정리 시작하여 무한 로딩 유발
        // OnDeath 패치에서 정리하므로 여기서는 불필요
        // [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        // public static class JobSkills_Character_ApplyDamage_Patch
        // {
        //     static void Postfix(Character __instance, HitData hit)
        //     {
        //         try
        //         {
        //             if (__instance is Player player && player.IsDead())
        //             {
        //                 CleanupAllJobSkillsOnDeath(player);
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             Plugin.Log.LogError($"[직업 스킬 ApplyDamage 패치] 오류: {ex.Message}");
        //         }
        //     }
        // }
    }

    /// <summary>
    /// 성기사 패시브 스킬: 물리 및 속성 저항 감소
    /// Character.Damage에서 성기사가 공격할 때 모든 데미지 증가
    /// </summary>
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class PaladinResistanceReductionPatch
    {
        /// <summary>
        /// Character.Damage 실행 전 호출 - 성기사 저항 감소 적용
        /// </summary>
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                // 공격자가 플레이어가 아니면 무시
                if (hit.GetAttacker() == null || !(hit.GetAttacker() is Player attacker))
                    return;

                // 성기사가 아니면 무시
                var manager = SkillTreeManager.Instance;
                if (manager == null || manager.GetSkillLevel("성기사") <= 0)
                    return;

                // 피격자가 몬스터인지 확인 (플레이어는 제외)
                if (__instance is Player)
                    return;

                // 저항 감소 비율 가져오기
                float resistanceReduction = Paladin_Config.ElementalResistanceReductionValue / 100f; // 8% = 0.08
                if (resistanceReduction <= 0f)
                    return;

                // 원래 데미지 저장 (로그용)
                var originalPhysicalDamage = GetPhysicalDamage(hit);
                var originalElementalDamage = GetElementalDamage(hit);

                if (originalPhysicalDamage > 0f || originalElementalDamage > 0f)
                {
                    // 저항 감소로 인한 데미지 증가 배수 계산
                    float damageMultiplier = 1f + resistanceReduction;

                    // 물리 데미지 증가 (관통, 블런트, 베기, 도끼질)
                    hit.m_damage.m_pierce *= damageMultiplier;
                    hit.m_damage.m_blunt *= damageMultiplier;
                    hit.m_damage.m_slash *= damageMultiplier;
                    hit.m_damage.m_chop *= damageMultiplier;

                    // 속성 데미지 증가 (불, 냉기, 번개, 독, 영혼)
                    hit.m_damage.m_fire *= damageMultiplier;
                    hit.m_damage.m_frost *= damageMultiplier;
                    hit.m_damage.m_lightning *= damageMultiplier;
                    hit.m_damage.m_poison *= damageMultiplier;
                    hit.m_damage.m_spirit *= damageMultiplier;

                    var increasedPhysicalDamage = GetPhysicalDamage(hit);
                    var increasedElementalDamage = GetElementalDamage(hit);
                    float totalIncrease = (increasedPhysicalDamage + increasedElementalDamage) - (originalPhysicalDamage + originalElementalDamage);

                    if (totalIncrease > 0.1f) // 의미있는 증가량일 때만 로그
                    {
                        Plugin.Log.LogInfo($"[성기사 패시브] 저항 감소 적용 - 물리: {originalPhysicalDamage:F1}→{increasedPhysicalDamage:F1}, 속성: {originalElementalDamage:F1}→{increasedElementalDamage:F1} (증가량: +{totalIncrease:F1})");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[성기사 패시브] Damage 패치 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 총 물리 데미지 계산 (관통, 블런트, 베기, 도끼질)
        /// </summary>
        private static float GetPhysicalDamage(HitData hit)
        {
            return hit.m_damage.m_pierce + hit.m_damage.m_blunt + hit.m_damage.m_slash + hit.m_damage.m_chop;
        }

        /// <summary>
        /// 총 속성 데미지 계산 (불, 냉기, 번개, 독, 영혼)
        /// </summary>
        private static float GetElementalDamage(HitData hit)
        {
            return hit.m_damage.m_fire + hit.m_damage.m_frost + hit.m_damage.m_lightning +
                   hit.m_damage.m_poison + hit.m_damage.m_spirit;
        }
    }
}

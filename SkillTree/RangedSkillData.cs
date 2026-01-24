using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 원거리 스킬 데이터 (활, 석궁, 지팡이)
    /// SkillTreeData.cs에서 분리된 원거리 무기 전용 스킬 트리
    /// </summary>
    public static class RangedSkillData
    {
        /// <summary>
        /// 원거리 스킬 트리 등록 (활, 석궁, 지팡이)
        /// </summary>
        public static void RegisterRangedSkills()
        {
            var manager = SkillTreeManager.Instance;

            // ==================== 원거리 전문가 루트 노드 ====================
            manager.AddSkill(new SkillNode {
                Id = "ranged_root",
                Name = "원거리 전문가",
                Description = "활/석궁 관통 +2, 지팡이/완드 화염 공격 +2\n필요조건: Crossbow, Bow, Staff, Wand",
                RequiredPoints = 2,
                MaxLevel = 1,
                Position = new Vector2(-90, 60),
                Category = "원거리",
                IconNameLocked = "ranged_lock",
                IconNameUnlocked = "ranged_unlock",
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "활/석궁 관통 +2, 지팡이/완드 화염 공격 +2");
                    }
                }
            });

            // ==================== 석궁 스킬 트리 ====================
            
            // Step 1: 석궁 전문가
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step1_damage",
                Name = "석궁 전문가",
                Description = $"석궁 공격력 +{Crossbow_Config.CrossbowExpertDamageBonusValue}%\n<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-160, 90),
                Category = "원거리",
                IconNameLocked = "crossbow_lock",
                IconNameUnlocked = "crossbow_unlock",
                Prerequisites = new List<string> { "ranged_root" },
                NextNodes = new List<string> { "crossbow_Step2_rapid_fire" },
                ApplyEffect = (lv) => { }
            });

            // Step 2: 연속 발사 Lv1
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step2_rapid_fire",
                Name = "연속 발사 Lv1",
                Description = $"{Crossbow_Config.CrossbowRapidFireChanceValue}% 확률로 {Crossbow_Config.CrossbowRapidFireShotCountValue}발 연속 발사\n" +
                              $"(각 {Crossbow_Config.CrossbowRapidFireDamagePercentValue}% 데미지, 볼트 {Crossbow_Config.CrossbowRapidFireBoltConsumptionValue}발 소모)\n" +
                              $"<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-230, 130),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step1_damage" },
                NextNodes = new List<string> { "crossbow_Step2_balance", "crossbow_Step3_rapid", "crossbow_Step3_mark" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-1: 균형 조준
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step2_balance",
                Name = "균형 조준",
                Description = $"명중 시 {Crossbow_Config.CrossbowBalanceKnockbackChanceValue}% 확률로 넉백 ({Crossbow_Config.CrossbowBalanceKnockbackDistanceValue}m)\n<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-310, 140),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_rapid_fire" },
                NextNodes = new List<string> { "crossbow_Step4_re" },
                ApplyEffect = (lv) => { }
            });

            // Step 3-1: 고속 장전
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step3_rapid",
                Name = "고속 장전",
                Description = $"장전속도 +{Crossbow_Config.CrossbowRapidReloadSpeedValue}%\n<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-300, 190),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_rapid_fire" },
                NextNodes = new List<string> { "crossbow_Step4_re" },
                ApplyEffect = (lv) => { }
            });

            // Step 3-2: 정직한 한 발
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step3_mark",
                Name = "정직한 한 발",
                Description = $"치명타 확률 0% 고정, 대신 석궁 공격력 +{Crossbow_Config.CrossbowMarkDamageBonusValue}%\n<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-290, 240),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_rapid_fire" },
                NextNodes = new List<string> { "crossbow_Step4_re" },
                ApplyEffect = (lv) => { }
            });

            // Step 4: 자동 장전
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step4_re",
                Name = "자동 장전",
                Description = $"명중 시 {Crossbow_Config.CrossbowAutoReloadChanceValue}% 확률로 다음 1회 장전 속도 200%\n<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-370, 240),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_balance", "crossbow_Step3_rapid", "crossbow_Step3_mark" },
                NextNodes = new List<string> { "crossbow_Step4_rapid_fire_lv2", "crossbow_Step5_final" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-1: 연속 발사 Lv2
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step4_rapid_fire_lv2",
                Name = "연속 발사 Lv2",
                Description = $"{Crossbow_Config.CrossbowRapidFireLv2ChanceValue}% 확률로 {Crossbow_Config.CrossbowRapidFireLv2ShotCountValue}발 연속 발사\n" +
                              $"(각 {Crossbow_Config.CrossbowRapidFireLv2DamagePercentValue}% 데미지, 볼트 {Crossbow_Config.CrossbowRapidFireLv2BoltConsumptionValue}발 소모)\n" +
                              $"<color=#FFD700><size=16>※ Lv1과 확률 합산</size></color>\n" +
                              $"<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-450, 250),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step4_re" },
                NextNodes = new List<string> { "crossbow_Step6_expert" },
                ApplyEffect = (lv) => { }
            });

            // Step 5: 결전의 일격
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step5_final",
                Name = "결전의 일격",
                Description = $"체력 {Crossbow_Config.CrossbowFinalStrikeHpThresholdValue}% 이상 적에게 추가 {Crossbow_Config.CrossbowFinalStrikeDamageBonusValue}% 피해\n<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-430, 330),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step4_re" },
                NextNodes = new List<string> { "crossbow_Step6_expert" },
                ApplyEffect = (lv) => { }
            });

            // Step 6: 단 한 발 (액티브 스킬)
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step6_expert",
                Name = "단 한 발",
                Description = $"T키: {Crossbow_Config.CrossbowOneShotDurationValue}초 이내 석궁 발사 시 공격력 +{Crossbow_Config.CrossbowOneShotDamageBonusValue}%, 넉백 {Crossbow_Config.CrossbowOneShotKnockbackValue}m (쿨타임 {Crossbow_Config.CrossbowOneShotCooldownValue}초)\n<color=#A020F0><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = 4,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-550, 350),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step4_rapid_fire_lv2", "crossbow_Step5_final" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // ==================== 활 스킬 트리 ====================
            
            // Step 1: 활 전문가
            manager.AddSkill(new SkillNode {
                Id = "bow_Step1_damage",
                Name = "활 전문가",
                Description = $"활 공격력 +{SkillTreeConfig.BowStep1ExpertDamageBonusValue}%\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-135, 140),
                Category = "원거리",
                IconNameLocked = "bow_lock",
                IconNameUnlocked = "bow_unlock",
                Prerequisites = new List<string> { "ranged_root" },
                NextNodes = new List<string> { "bow_Step2_focus", "bow_Step2_multishot" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-1: 집중 사격
            manager.AddSkill(new SkillNode {
                Id = "bow_Step2_focus",
                Name = "집중 사격",
                Description = $"치명타 확률 +{SkillTreeConfig.BowStep2FocusCritBonusValue}%\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-205, 170),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step1_damage" },
                NextNodes = new List<string> { "bow_Step3_speedshot" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-2: 멀티샷 Lv1
            manager.AddSkill(new SkillNode {
                Id = "bow_Step2_multishot",
                Name = "멀티샷 Lv1",
                Description = $"{SkillTreeConfig.BowMultishotLv1ChanceValue}% 확률로 화살 2개 발사\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-135, 220),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step1_damage" },
                NextNodes = new List<string> { "bow_Step3_speedshot" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🏹 멀티샷 Lv1 습득!", 
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // Step 3: 활 숙련
            manager.AddSkill(new SkillNode {
                Id = "bow_Step3_speedshot",
                Name = "활 숙련",
                Description = $"활 기술(숙련도) +{SkillTreeConfig.BowStep3SpeedShotSkillBonusValue}\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-205, 240),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step2_focus", "bow_Step2_multishot" },
                NextNodes = new List<string> { "bow_Step3_silentshot", "bow_Step4_multishot2", "bow_Step5_instinct" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player == null) {
                        Plugin.Log.LogError("[활 숙련] Player.m_localPlayer가 null입니다!");
                        return;
                    }
                    
                    var skills = player.GetSkills();
                    if (skills == null) {
                        Plugin.Log.LogError("[활 숙련] player.GetSkills()가 null입니다!");
                        return;
                    }
                    
                    // 발하임 스킬 레벨 직접 증가 (CheatRaiseSkill 방식)
                    float skillLevelBonus = SkillTreeConfig.BowStep3SpeedShotSkillBonusValue; // 10
                    float previousLevel = skills.GetSkillLevel(Skills.SkillType.Bows);
                    
                    Plugin.Log.LogWarning($"[활 숙련] 현재 활 기술 레벨: {previousLevel}, 상승할 레벨: {skillLevelBonus}");
                    
                    try {
                        // 발하임 공식 CheatRaiseSkill 메서드 사용 (Terminal.cs에서 사용하는 방식)
                        // 이 방법이 레벨을 직접 올리는 가장 확실한 방법
                        skills.CheatRaiseSkill("Bows", skillLevelBonus, true);
                        
                        float newLevel = skills.GetSkillLevel(Skills.SkillType.Bows);
                        
                        SkillEffect.ShowSkillEffectText(player, $"🏹 활 숙련 습득! 활 기술 +{skillLevelBonus} 레벨 (레벨: {previousLevel:F1} → {newLevel:F1})", 
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        
                        Plugin.Log.LogWarning($"[활 숙련] CheatRaiseSkill로 활 기술 레벨 상승: {previousLevel:F1} → {newLevel:F1} (+{skillLevelBonus} 레벨)");
                    } catch (System.Exception ex) {
                        Plugin.Log.LogError($"[활 숙련] CheatRaiseSkill 실패: {ex.Message}");
                    }
                }
            });

            // Step 3-1: 기본 활공격
            manager.AddSkill(new SkillNode {
                Id = "bow_Step3_silentshot",
                Name = "기본 활공격",
                Description = $"활 공격력 +{SkillTreeConfig.BowStep3SilentShotDamageBonusValue}\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-305, 285),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_speedshot" },
                NextNodes = new List<string> { "bow_Step5_master" },
                ApplyEffect = (lv) => { }
            });

            // Step 4: 멀티샷 Lv2
            manager.AddSkill(new SkillNode {
                Id = "bow_Step4_multishot2",
                Name = "멀티샷 Lv2",
                Description = $"{SkillTreeConfig.BowMultishotLv2ChanceValue}% 확률로 추가 화살 2발 발사 (+3도 각도)\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-255, 315),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_speedshot" },
                NextNodes = new List<string> { "bow_Step5_master" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🏹🏹 멀티샷 Lv2 습득!", 
                            new Color(0.3f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // Step 5-1: 사냥 본능
            manager.AddSkill(new SkillNode {
                Id = "bow_Step5_instinct",
                Name = "사냥 본능",
                Description = $"치명타 확률 +{SkillTreeConfig.BowStep5InstinctCritBonusValue}%\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-205, 335),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_speedshot" },
                NextNodes = new List<string> { "bow_Step5_master" },
                ApplyEffect = (lv) => { }
            });

            // Step 5: 정조준
            manager.AddSkill(new SkillNode {
                Id = "bow_Step5_master",
                Name = "정조준",
                Description = $"크리티컬 데미지 +{SkillTreeConfig.BowStep5MasterCritDamageValue}%\n<color=#A020F0><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-305, 395),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_silentshot", "bow_Step4_multishot2", "bow_Step5_instinct" },
                NextNodes = new List<string> { "bow_Step6_critboost" },
                ApplyEffect = (lv) => { }
            });

            // Step 6: 폭발 화살 (액티브 스킬)
            manager.AddSkill(new SkillNode {
                Id = "bow_Step6_critboost",
                Name = "폭발 화살",
                Description = $"T키: 폭발 화살 발사 (공격력의 {SkillTreeConfig.BowExplosiveArrowDamageValue}% 폭발 데미지, 쿨타임 {SkillTreeConfig.BowExplosiveArrowCooldownValue}초)\n소모: 스태미나 {SkillTreeConfig.BowExplosiveArrowStaminaCostValue}%\n스킬유형: 액티브 스킬, T키\n무기타입: 활\n<color=#FF4500><size=16>※ 폭발 범위 피해</size></color>",
                RequiredPoints = 4,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-365, 475),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step5_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // ==================== 지팡이 스킬 트리 ====================
            
            // Step 1: 지팡이 전문가
            manager.AddSkill(new SkillNode {
                Id = "staff_Step1_damage",
                Name = "지팡이 전문가",
                Description = $"지팡이 속성 공격력 +{Staff_Config.StaffExpertDamageValue}% 증가\n지팡이와 완드 사용법을 익혀 마법 공격력을 높입니다\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-160, 30),
                Category = "지팡이",
                IconNameLocked = "staff_lock",
                IconNameUnlocked = "staff_unlock",
                Prerequisites = new List<string> { "ranged_root" },
                NextNodes = new List<string> { "staff_Step2_focus", "staff_Step2_stream" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-1: 정신 집중
            manager.AddSkill(new SkillNode {
                Id = "staff_Step2_focus",
                Name = "정신 집중",
                Description = $"지팡이 사용 시 에이트르 소모량 -{Staff_Config.StaffFocusEitrReductionValue}% 감소\n정신력 집중으로 마나 효율성을 높입니다\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffFocusRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-230, 70),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step1_damage" },
                NextNodes = new List<string> { "staff_Step3_amp" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-2: 마법 흐름
            manager.AddSkill(new SkillNode {
                Id = "staff_Step2_stream",
                Name = "마법 흐름",
                Description = $"최대 에이트르 +{Staff_Config.StaffStreamEitrBonusValue} 증가\n마법의 흐름을 익혀 더 많은 마나를 보유할 수 있습니다\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffStreamRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-230, 0),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step1_damage" },
                NextNodes = new List<string> { "staff_Step3_amp" },
                ApplyEffect = (lv) => { }
            });

            // Step 3: 마법 증폭
            manager.AddSkill(new SkillNode {
                Id = "staff_Step3_amp",
                Name = "마법 증폭",
                Description = $"지팡이 마법 공격력 +{Staff_Config.StaffAmpDamageValue}% 증가\n마법력을 증폭하여 더 강력한 주문을 구사합니다\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffAmpRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-310, 35),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step2_focus", "staff_Step2_stream" },
                NextNodes = new List<string> { "staff_Step4_reduction", "staff_Step4_range", "staff_Step4_surge" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-1: 냉기 속성
            manager.AddSkill(new SkillNode {
                Id = "staff_Step4_reduction",
                Name = "냉기 속성",
                Description = $"냉기 공격 +{Staff_Config.StaffFrostDamageBonusValue}\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffCritDamageRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-400, 115),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step3_amp" },
                NextNodes = new List<string> { "staff_Step5_archmage" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-2: 화염 속성
            manager.AddSkill(new SkillNode {
                Id = "staff_Step4_range",
                Name = "화염 속성",
                Description = $"화염 공격 +{Staff_Config.StaffFireDamageBonusValue}\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffMagicAmpRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-400, 35),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step3_amp" },
                NextNodes = new List<string> { "staff_Step5_archmage" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-3: 번개 속성
            manager.AddSkill(new SkillNode {
                Id = "staff_Step4_surge",
                Name = "번개 속성",
                Description = $"번개 공격 +{Staff_Config.StaffLightningDamageBonusValue}\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffLuckManaRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-400, -45),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step3_amp" },
                NextNodes = new List<string> { "staff_Step5_archmage" },
                ApplyEffect = (lv) => { }
            });

            // Step 5: 행운 마력 (기존 대마법사를 행운 마력으로 변경)
            manager.AddSkill(new SkillNode {
                Id = "staff_Step5_archmage",
                Name = "행운 마력",
                Description = $"{Staff_Config.StaffLuckManaChanceValue}% 확률로 에이트르 소모 없음\n행운의 마력이 깃들어 공짜로 마법을 시전합니다\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffCritRateRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-490, 35),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step4_reduction", "staff_Step4_range", "staff_Step4_surge" },
                NextNodes = new List<string> { "staff_Step6_dual_cast", "staff_Step6_heal" },
                ApplyEffect = (lv) => { }
            });

            // Step 6-1: 이중 시전 (액티브 스킬)
            manager.AddSkill(new SkillNode {
                Id = "staff_Step6_dual_cast",
                Name = "이중시전",
                Description = $"T키로 추가 마법 발사체 {Staff_Config.StaffDoubleCastProjectileCountValue}발 발사 (좌 -{Staff_Config.StaffDoubleCastAngleOffsetValue:F0}°, 우 +{Staff_Config.StaffDoubleCastAngleOffsetValue:F0}°)\n발사체 데미지: 지팡이/완드 공격력의 {Staff_Config.StaffDoubleCastDamagePercentValue:F0}%\n소모: Eitr {Staff_Config.StaffDoubleCastEitrCostValue:F0}\n스킬유형: 액티브 T키\n무기타입: 지팡이\n쿨타임: {Staff_Config.StaffDoubleCastCooldownValue:F0}초\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffDoubleCastRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-580, 115),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step5_archmage" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // Step 6-2: 힐 (액티브 스킬)
            manager.AddSkill(new SkillNode {
                Id = "staff_Step6_heal",
                Name = "힐",
                Description = $"G키로 {HealerMode_Config.HealerModeDurationValue:F0}초 동안 힐러모드 활성화\n아군 최대체력의 {HealerMode_Config.HealPercentageValue}% 즉시 회복\n소모: Eitr {HealerMode_Config.HealerModeEitrCostValue:F0}\n스킬유형: 액티브 G키\n무기타입: 지팡이\n쿨타임: {HealerMode_Config.HealerModeCooldownValue:F0}초\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffHealerRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-580, -45),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step5_archmage" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });
        }

        /// <summary>
        /// 원거리 스킬 아이콘 통일 설정
        /// </summary>
        public static void SetupRangedSkillIcons()
        {
            var manager = SkillTreeManager.Instance;
            if (manager?.SkillNodes == null) return;

            // 활 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "원거리" && node.Id.StartsWith("bow_"))
                {
                    if (node.Id != "bow_Step1_damage") // 마스터리는 제외
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 석궁 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "원거리" && node.Id.StartsWith("crossbow_"))
                {
                    if (node.Id != "crossbow_Step1_damage") // 마스터리는 제외
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 지팡이 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "지팡이" && node.Id.StartsWith("staff_"))
                {
                    if (node.Id != "staff_Step1_damage") // 마스터리는 제외
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 원거리 루트 노드 위치 설정
            if (manager.SkillNodes.ContainsKey("ranged_root"))
            {
                manager.SkillNodes["ranged_root"].Position = new Vector2(-90, 60);
            }
        }
    }
}
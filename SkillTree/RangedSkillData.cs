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
                Description = $"석궁 공격력 +{Crossbow_Config.CrossbowExpertDamageBonusValue}%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowExpertRequiredPointsValue,
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
                              $"<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowStep2RequiredPointsValue,
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
                Description = $"명중 시 {Crossbow_Config.CrossbowBalanceKnockbackChanceValue}% 확률로 넉백 ({Crossbow_Config.CrossbowBalanceKnockbackDistanceValue}m)\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowStep3RequiredPointsValue,
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
                Description = $"장전속도 +{Crossbow_Config.CrossbowRapidReloadSpeedValue}%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowStep3RequiredPointsValue,
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
                Description = $"치명타 확률 0% 고정, 대신 석궁 공격력 +{Crossbow_Config.CrossbowMarkDamageBonusValue}%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowStep3RequiredPointsValue,
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
                Description = $"명중 시 {Crossbow_Config.CrossbowAutoReloadChanceValue}% 확률로 다음 1회 장전 속도 200%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowStep4RequiredPointsValue,
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
                              $"<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowStep5RequiredPointsValue,
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
                Description = $"체력 {Crossbow_Config.CrossbowFinalStrikeHpThresholdValue}% 이상 적에게 추가 {Crossbow_Config.CrossbowFinalStrikeDamageBonusValue}% 피해\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>",
                RequiredPoints = Crossbow_Config.CrossbowStep5RequiredPointsValue,
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

            // Step 6: 단 한 발 (액티브 스킬) - 표준 툴팁 형식
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step6_expert",
                Name = "단 한 발",
                Description = GetOneShotTooltip(),
                RequiredPoints = Crossbow_Config.CrossbowOneShotRequiredPointsValue,
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
                Description = $"활 공격력 +{Bow_Config.BowStep1ExpertDamageBonusValue}%\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowExpertRequiredPointsValue,
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
                Description = $"치명타 확률 +{Bow_Config.BowStep2FocusCritBonusValue}%\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowStep2RequiredPointsValue,
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
                Description = $"{Bow_Config.BowMultishotLv1ChanceValue}% 확률로 화살 2개 발사\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowMultishotRequiredPointsValue,
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
                Description = $"활 기술(숙련도) +{Bow_Config.BowStep3SpeedShotSkillBonusValue}\n<color=#FFD700><size=14>※ 사망해도 보너스 유지</size></color>\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-205, 240),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step2_focus", "bow_Step2_multishot" },
                NextNodes = new List<string> { "bow_Step3_silentshot", "bow_Step4_multishot2", "bow_Step5_instinct" },
                ApplyEffect = (lv) => {
                    // 숙련도 보너스는 Skills.GetSkillLevel 패치에서 자동 적용됨
                    // 사망해도 유지되며, 스킬트리 해제 시 자동 초기화
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        float skillLevelBonus = Bow_Config.BowStep3SpeedShotSkillBonusValue;
                        SkillEffect.ShowSkillEffectText(player,
                            $"🏹 활 숙련 습득! 활 기술 +{skillLevelBonus} (사망해도 유지)",
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo($"[활 숙련] 활 숙련도 +{skillLevelBonus} 보너스 활성화");
                    }
                }
            });

            // Step 3-1: 기본 활공격
            manager.AddSkill(new SkillNode {
                Id = "bow_Step3_silentshot",
                Name = "기본 활공격",
                Description = $"활 공격력 +{Bow_Config.BowStep3SilentShotDamageBonusValue}\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowStep3RequiredPointsValue,
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
                Description = $"{Bow_Config.BowMultishotLv2ChanceValue}% 확률로 추가 화살 2발 발사 (+3도 각도)\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowStep4RequiredPointsValue,
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
                Description = $"치명타 확률 +{Bow_Config.BowStep5InstinctCritBonusValue}%\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowStep4RequiredPointsValue,
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
                Description = $"크리티컬 데미지 +{Bow_Config.BowStep5MasterCritDamageValue}%\n<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>",
                RequiredPoints = Bow_Config.BowStep5RequiredPointsValue,
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

            // Step 6: 폭발 화살 (액티브 스킬) - 표준 툴팁 형식
            manager.AddSkill(new SkillNode {
                Id = "bow_Step6_critboost",
                Name = "폭발 화살",
                Description = GetExplosiveArrowTooltip(),
                RequiredPoints = Bow_Config.BowExplosiveArrowRequiredPointsValue,
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
                RequiredPoints = Staff_Config.StaffFrostRequiredPointsValue,
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
                RequiredPoints = Staff_Config.StaffFireRequiredPointsValue,
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
                RequiredPoints = Staff_Config.StaffLightningRequiredPointsValue,
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
                RequiredPoints = Staff_Config.StaffLuckManaRequiredPointsValue,
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
                Description = $"R키로 추가 마법 발사체 {Staff_Config.StaffDoubleCastProjectileCountValue}발 발사 (좌 -{Staff_Config.StaffDoubleCastAngleOffsetValue:F0}°, 우 +{Staff_Config.StaffDoubleCastAngleOffsetValue:F0}°)\n발사체 데미지: 지팡이/완드 공격력의 {Staff_Config.StaffDoubleCastDamagePercentValue:F0}%\n소모: Eitr {Staff_Config.StaffDoubleCastEitrCostValue:F0}\n스킬유형: 액티브 R키\n무기타입: 지팡이\n쿨타임: {Staff_Config.StaffDoubleCastCooldownValue:F0}초\n필요조건: 지팡이 또는 완드 착용",
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
                Description = $"H키로 시전자 중심 {Staff_Config.StaffHealRangeValue:F0}m 범위 즉시 힐링\n아군 최대체력의 {Staff_Config.StaffHealPercentageValue}% 즉시 회복\n소모: Eitr {Staff_Config.StaffHealEitrCostValue:F0}\n스킬유형: 액티브 H키\n무기타입: 지팡이\n쿨타임: {Staff_Config.StaffHealCooldownValue:F0}초\n필요조건: 지팡이 또는 완드 착용",
                RequiredPoints = Staff_Config.StaffHealRequiredPointsValue,
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

        /// <summary>
        /// 폭발 화살 동적 툴팁 생성 (R키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형 → 쿨타임 → 필요조건 → 필요포인트
        /// </summary>
        public static string GetExplosiveArrowTooltip()
        {
            var tooltip = "";

            // 1. 스킬명
            tooltip += $"<color=#FFD700><size=22>폭발 화살</size></color>\n\n";

            // 2. 설명
            tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>폭발 화살 발사, 적중 시 주변 적에게 폭발 피해</size></color>\n";

            // 3. 데미지
            tooltip += $"<color=#FF6B6B><size=16>데미지: </size></color><color=#FFB6C1><size=16>공격력의 {Bow_Config.BowExplosiveArrowDamageValue}% 폭발 피해</size></color>\n";

            // 4. 범위
            tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>폭발 범위 5m</size></color>\n";

            // 5. 소모
            tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {Bow_Config.BowExplosiveArrowStaminaCostValue}%</size></color>\n";

            // 6. 스킬유형 (R키 강조)
            tooltip += $"<color=#9400D3><size=16>스킬유형: </size></color><color=#FFD700><size=16>액티브 스킬 - R키</size></color>\n";

            // 7. 쿨타임
            tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{Bow_Config.BowExplosiveArrowCooldownValue}초</size></color>\n";

            // 8. 필요조건
            tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>활 착용</size></color>\n";

            // 9. 필요포인트
            tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>4</size></color>";

            return tooltip.TrimEnd('\n');
        }

        /// <summary>
        /// 단 한 발 동적 툴팁 생성 (R키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형 → 쿨타임 → 필요조건 → 필요포인트
        /// </summary>
        public static string GetOneShotTooltip()
        {
            var tooltip = "";

            // 1. 스킬명
            tooltip += $"<color=#FFD700><size=22>단 한 발</size></color>\n\n";

            // 2. 설명
            tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>{Crossbow_Config.CrossbowOneShotDurationValue}초간 버프 활성화, 다음 석궁 발사 시 강력한 일격</size></color>\n";

            // 3. 데미지
            tooltip += $"<color=#FF6B6B><size=16>데미지: </size></color><color=#FFB6C1><size=16>공격력 +{Crossbow_Config.CrossbowOneShotDamageBonusValue}%</size></color>\n";

            // 4. 범위 (넉백)
            tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>넉백 {Crossbow_Config.CrossbowOneShotKnockbackValue}m</size></color>\n";

            // 5. 소모
            tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 20</size></color>\n";

            // 6. 스킬유형 (R키 강조)
            tooltip += $"<color=#9400D3><size=16>스킬유형: </size></color><color=#FFD700><size=16>액티브 스킬 - R키</size></color>\n";

            // 7. 쿨타임
            tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{Crossbow_Config.CrossbowOneShotCooldownValue}초</size></color>\n";

            // 8. 필요조건
            tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>석궁 착용</size></color>\n";

            // 9. 필요포인트
            tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>4</size></color>";

            return tooltip.TrimEnd('\n');
        }
    }
}
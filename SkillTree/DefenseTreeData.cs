using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class DefenseTreeData
    {
        public static void RegisterDefenseSkills()
        {
            var manager = SkillTreeManager.Instance;
            
            Plugin.Log.LogDebug($"[DefenseTreeData] RegisterDefenseSkills 시작 - 방어 전문가 노드 등록 중");

            // ===== 방어 전문가 스킬 트리 =====
            
            // Tier 0: 방어 전문가 (루트)
            manager.AddSkill(new SkillNode {
                Id = "defense_root",
                NameKey = "defense_root_name",
                DescriptionKey = "defense_root_desc",
                DescriptionArgs = new object[] { Defense_Config.DefenseRootHealthBonusValue, Defense_Config.DefenseRootArmorBonusValue },
                RequiredPoints = Defense_Config.DefenseRootRequiredPointsValue,
                MaxLevel = 1,
                Tier = 0,
                Position = new Vector2(90, -60),
                Category = "방어",
                IconNameLocked = "defense_lock",
                IconNameUnlocked = "defense_unlock",
                NextNodes = new List<string> { "defense_Step1_survival" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, L.Get("defense_root_effect", Defense_Config.DefenseRootHealthBonusValue, Defense_Config.DefenseRootArmorBonusValue));
                    }
                }
            });
            
            Plugin.Log.LogDebug($"[DefenseTreeData] defense_root 노드 등록 완료 - 방어 전문가");

            // Tier 1: 피부경화
            manager.AddSkill(new SkillNode {
                Id = "defense_Step1_survival",
                NameKey = "defense_survival_name",
                DescriptionKey = "defense_survival_desc",
                DescriptionArgs = new object[] { Defense_Config.SurvivalHealthBonusValue, Defense_Config.SurvivalArmorBonusValue },
                RequiredPoints = Defense_Config.DefenseStep1RequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(160, -100),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_root" },
                NextNodes = new List<string> { "defense_Step2_dodge", "defense_Step2_health" },
                ApplyEffect = (lv) => { }
            });

            // Tier 2: 심신단련
            manager.AddSkill(new SkillNode {
                Id = "defense_Step2_dodge",
                NameKey = "defense_dodge_name",
                DescriptionKey = "defense_dodge_desc",
                DescriptionArgs = new object[] { Defense_Config.DodgeStaminaBonusValue, Defense_Config.DodgeEitrBonusValue },
                RequiredPoints = Defense_Config.DefenseStep2DodgeRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(210, -190),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step1_survival" },
                NextNodes = new List<string> { "defense_Step3_breath", "defense_Step3_agile" },
                ApplyEffect = (lv) => { }
            });

            // Tier 2: 체력단련
            manager.AddSkill(new SkillNode {
                Id = "defense_Step2_health",
                NameKey = "defense_health_name",
                DescriptionKey = "defense_health_desc",
                DescriptionArgs = new object[] { Defense_Config.HealthBonusValue, Defense_Config.HealthArmorBonusValue },
                RequiredPoints = Defense_Config.DefenseStep2HealthRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(265, -110),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step1_survival" },
                NextNodes = new List<string> { "defense_Step3_boost", "defense_Step3_shield" },
                ApplyEffect = (lv) => { }
            });

            // Tier 3: 단전호흡
            manager.AddSkill(new SkillNode {
                Id = "defense_Step3_breath",
                NameKey = "defense_breath_name",
                DescriptionKey = "defense_breath_desc",
                DescriptionArgs = new object[] { Defense_Config.BreathEitrBonusValue },
                RequiredPoints = Defense_Config.DefenseStep3BreathRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(230, -270),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step2_dodge" },
                NextNodes = new List<string> { "defense_Step4_mental" },
                ApplyEffect = (lv) => { }
            });

            // Tier 3: 회피단련
            manager.AddSkill(new SkillNode {
                Id = "defense_Step3_agile",
                NameKey = "defense_agile_name",
                DescriptionKey = "defense_agile_desc",
                DescriptionArgs = new object[] { Defense_Config.AgileDodgeBonusValue, Defense_Config.AgileInvincibilityBonusValue },
                RequiredPoints = Defense_Config.DefenseStep3AgileRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(275, -230),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step2_dodge" },
                NextNodes = new List<string> { "defense_Step4_instant", "defense_Step4_mental" },
                ApplyEffect = (lv) => { }
            });

            // Tier 3: 체력증강
            manager.AddSkill(new SkillNode {
                Id = "defense_Step3_boost",
                NameKey = "defense_boost_name",
                DescriptionKey = "defense_boost_desc",
                DescriptionArgs = new object[] { Defense_Config.BoostHealthBonusValue },
                RequiredPoints = Defense_Config.DefenseStep3BoostRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(315, -165),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step2_health" },
                NextNodes = new List<string> { "defense_Step4_tanker", "defense_Step4_instant" },
                ApplyEffect = (lv) => { }
            });

            // Tier 3: 방패훈련
            manager.AddSkill(new SkillNode {
                Id = "defense_Step3_shield",
                NameKey = "defense_shield_name",
                DescriptionKey = "defense_shield_desc",
                DescriptionArgs = new object[] { Defense_Config.ShieldTrainingBlockPowerBonusValue },
                RequiredPoints = Defense_Config.DefenseStep3ShieldRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(345, -110),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step2_health" },
                NextNodes = new List<string> { "defense_Step4_tanker" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, L.Get("defense_shield_effect", Defense_Config.ShieldTrainingBlockPowerBonusValue));
                    }
                }
            });

            // Tier 4: 충격파방출 (액티브 스킬) - 발구르기와 상호 배타
            manager.AddSkill(new SkillNode {
                Id = "defense_Step4_mental",
                NameKey = "defense_mental_name",
                DescriptionKey = "defense_mental_desc",
                DescriptionArgs = new object[] {
                    Defense_Config.ShockwaveRadiusValue,
                    Defense_Config.ShockwaveStunDurationValue,
                    Defense_Config.ShockwaveCooldownValue
                },
                RequiredPoints = Defense_Config.DefenseStep4MentalRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(310, -310),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step3_agile", "defense_Step3_breath" },
                NextNodes = new List<string> { "defense_Step5_focus", "defense_Step5_stamina" },
                MutuallyExclusive = new List<string> { "defense_Step4_instant" },
                ApplyEffect = (lv) => { }
            });

            // Tier 4: 발구르기 (자동 발동 패시브 스킬) - 충격파방출과 상호 배타
            manager.AddSkill(new SkillNode {
                Id = "defense_Step4_instant",
                NameKey = "defense_instant_name",
                DescriptionKey = "defense_instant_desc",
                DescriptionArgs = new object[] { 3, 120 },
                RequiredPoints = Defense_Config.DefenseStep4InstantRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(350, -230),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step3_agile", "defense_Step3_boost" },
                NextNodes = new List<string> { "defense_Step5_stamina", "defense_Step5_heal" },
                MutuallyExclusive = new List<string> { "defense_Step4_mental" },
                ApplyEffect = (lv) => { }
            });

            // Tier 4: 바위피부
            manager.AddSkill(new SkillNode {
                Id = "defense_Step4_tanker",
                NameKey = "defense_tanker_name",
                DescriptionKey = "defense_tanker_desc",
                DescriptionArgs = new object[] { Defense_Config.TankerArmorBonusValue },
                RequiredPoints = Defense_Config.DefenseStep4TankerRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(390, -160),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step3_boost", "defense_Step3_shield" },
                NextNodes = new List<string> { "defense_Step5_heal", "defense_Step5_parry" },
                ApplyEffect = (lv) => { }
            });

            // Tier 5: 지구력
            manager.AddSkill(new SkillNode {
                Id = "defense_Step5_focus",
                NameKey = "defense_focus_name",
                DescriptionKey = "defense_focus_desc",
                DescriptionArgs = new object[] { Defense_Config.FocusRunStaminaReductionValue, Defense_Config.FocusJumpStaminaReductionValue },
                RequiredPoints = Defense_Config.DefenseStep5FocusRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(380, -400),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step4_mental" },
                NextNodes = new List<string> { "defense_Step6_mind", "defense_Step6_attack" },
                ApplyEffect = (lv) => { }
            });

            // Tier 5: 기민함
            manager.AddSkill(new SkillNode {
                Id = "defense_Step5_stamina",
                NameKey = "defense_stamina_name",
                DescriptionKey = "defense_stamina_desc",
                DescriptionArgs = new object[] { Defense_Config.StaminaDodgeBonusValue, Defense_Config.StaminaRollStaminaReductionValue },
                RequiredPoints = Defense_Config.DefenseStep5StaminaRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(420, -310),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step4_mental", "defense_Step4_instant" },
                NextNodes = new List<string> { "defense_Step6_attack", "defense_step6_double_jump" },
                ApplyEffect = (lv) => { }
            });

            // Tier 5: 트롤의 재생력
            manager.AddSkill(new SkillNode {
                Id = "defense_Step5_heal",
                NameKey = "defense_heal_name",
                DescriptionKey = "defense_heal_desc",
                DescriptionArgs = new object[] { Defense_Config.TrollRegenIntervalValue, Defense_Config.TrollRegenBonusValue },
                RequiredPoints = Defense_Config.DefenseStep5HealRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(465, -230),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step4_instant", "defense_Step4_tanker" },
                NextNodes = new List<string> { "defense_step6_double_jump", "defense_Step6_body" },
                ApplyEffect = (lv) => { }
            });

            // Tier 5: 막기달인
            manager.AddSkill(new SkillNode {
                Id = "defense_Step5_parry",
                NameKey = "defense_parry_name",
                DescriptionKey = "defense_parry_desc",
                DescriptionArgs = new object[] { Defense_Config.ParryMasterParryDurationBonusValue, Defense_Config.ParryMasterBlockPowerBonusValue },
                RequiredPoints = Defense_Config.DefenseStep5ParryRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(500, -165),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step4_tanker" },
                NextNodes = new List<string> { "defense_Step6_body", "defense_Step6_true" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, L.Get("defense_parry_effect", Defense_Config.ParryMasterParryDurationBonusValue, Defense_Config.ParryMasterBlockPowerBonusValue));
                    }
                }
            });

            // Tier 6: 마인드쉴드
            manager.AddSkill(new SkillNode {
                Id = "defense_Step6_mind",
                NameKey = "defense_mind_name",
                DescriptionKey = "defense_mind_desc",
                DescriptionArgs = new object[] { 60 },
                RequiredPoints = Defense_Config.DefenseStep6MindRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(415, -475),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step5_focus" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // Tier 6: 신경강화
            manager.AddSkill(new SkillNode {
                Id = "defense_Step6_attack",
                NameKey = "defense_attack_name",
                DescriptionKey = "defense_attack_desc",
                DescriptionArgs = new object[] { Defense_Config.AttackDodgeBonusValue },
                RequiredPoints = Defense_Config.DefenseStep6AttackRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(465, -380),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step5_focus", "defense_Step5_stamina" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // Tier 6: 이단점프 (액티브 스킬)
            manager.AddSkill(new SkillNode {
                Id = "defense_step6_double_jump",
                NameKey = "defense_double_jump_name",
                DescriptionKey = "defense_double_jump_desc",
                DescriptionArgs = new object[] { 1 },
                RequiredPoints = Defense_Config.DefenseStep6DoubleJumpRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(525, -305),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step5_stamina", "defense_Step5_heal" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // Tier 6: 요툰의 생명력
            manager.AddSkill(new SkillNode {
                Id = "defense_Step6_body",
                NameKey = "defense_body_name",
                DescriptionKey = "defense_body_desc",
                DescriptionArgs = new object[] { Defense_Config.BodyHealthBonusValue, Defense_Config.BodyArmorBonusValue },
                RequiredPoints = Defense_Config.DefenseStep6BodyRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(555, -235),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step5_heal", "defense_Step5_parry" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, L.Get("defense_body_effect", Defense_Config.BodyHealthBonusValue, Defense_Config.BodyArmorBonusValue));
                    }
                }
            });

            // Tier 6: 요툰의 방패
            manager.AddSkill(new SkillNode {
                Id = "defense_Step6_true",
                NameKey = "defense_true_name",
                DescriptionKey = "defense_true_desc",
                DescriptionArgs = new object[] { Defense_Config.JotunnShieldBlockStaminaReductionValue, Defense_Config.JotunnShieldNormalSpeedBonusValue, Defense_Config.JotunnShieldTowerSpeedBonusValue },
                RequiredPoints = Defense_Config.DefenseStep6TrueRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(600, -160),
                Category = "방어",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "defense_Step5_parry" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.SkillTree;

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
                Name = "방어 전문가",
                Description = $"체력 +{Defense_Config.DefenseRootHealthBonusValue}, 방어 +{Defense_Config.DefenseRootArmorBonusValue}",
                RequiredPoints = 2,
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
                        SkillEffect.DrawFloatingText(player, $"체력 +{Defense_Config.DefenseRootHealthBonusValue}, 방어 +{Defense_Config.DefenseRootArmorBonusValue}");
                    }
                }
            });
            
            Plugin.Log.LogDebug($"[DefenseTreeData] defense_root 노드 등록 완료 - 방어 전문가");

            // Tier 1: 피부경화
            manager.AddSkill(new SkillNode {
                Id = "defense_Step1_survival",
                Name = "피부경화",
                Description = $"체력 +{Defense_Config.SurvivalHealthBonusValue}, 방어 +{Defense_Config.SurvivalArmorBonusValue}",
                RequiredPoints = 2,
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
                Name = "심신단련",
                Description = $"스태미나 최대치 +{Defense_Config.DodgeStaminaBonusValue}, 에이트르 최대치 +{Defense_Config.DodgeEitrBonusValue}",
                RequiredPoints = 2,
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
                Name = "체력단련",
                Description = $"체력 +{Defense_Config.HealthBonusValue}, 방어 +{Defense_Config.HealthArmorBonusValue}",
                RequiredPoints = 2,
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
                Name = "단전호흡",
                Description = $"에이트르 최대치 +{Defense_Config.BreathEitrBonusValue}",
                RequiredPoints = 3,
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
                Name = "회피단련",
                Description = $"회피 +{Defense_Config.AgileDodgeBonusValue}%, 구르기 무적시간 +{Defense_Config.AgileInvincibilityBonusValue}%",
                RequiredPoints = 3,
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
                Name = "체력증강",
                Description = $"체력 +{Defense_Config.BoostHealthBonusValue}",
                RequiredPoints = 3,
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
                Name = "방패훈련",
                Description = $"방패 방어력 +{Defense_Config.ShieldTrainingBlockPowerBonusValue}",
                RequiredPoints = 3,
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
                        SkillEffect.DrawFloatingText(player, $"방패 방어력 +{Defense_Config.ShieldTrainingBlockPowerBonusValue}");
                    }
                }
            });

            // Tier 4: 충격파방출 (액티브 스킬) - 발구르기와 상호 배타
            manager.AddSkill(new SkillNode {
                Id = "defense_Step4_mental",
                Name = "충격파방출",
                Description = "생명력 45%이하 일시 3미터 이내의 적을 1초간 기절시킴(120초 쿨타임)",
                RequiredPoints = 3,
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
                Name = "발구르기",
                Description = "생명력 35%이하 일시 자동으로 주변 적을 3미터 밀어냄 (120초 쿨타임)",
                RequiredPoints = 3,
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
                Name = "바위피부",
                Description = "방어력 +12%",
                RequiredPoints = 3,
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
                Name = "지구력",
                Description = $"달리기 스태미나 -{Defense_Config.FocusRunStaminaReductionValue}%, 점프 스태미나 -{Defense_Config.FocusJumpStaminaReductionValue}%",
                RequiredPoints = 3,
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
                Name = "기민함",
                Description = $"회피 +{Defense_Config.StaminaDodgeBonusValue}%, 구르기 스태미나 -{Defense_Config.StaminaRollStaminaReductionValue}%",
                RequiredPoints = 3,
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
                Name = "트롤의 재생력",
                Description = $"{Defense_Config.TrollRegenIntervalValue}초마다 체력 +{Defense_Config.TrollRegenBonusValue}",
                RequiredPoints = 3,
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
                Name = "막기달인",
                Description = $"패링 +{Defense_Config.ParryMasterParryDurationBonusValue}초, 방패 방어력 +{Defense_Config.ParryMasterBlockPowerBonusValue}",
                RequiredPoints = 3,
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
                        SkillEffect.DrawFloatingText(player, $"패링 +{Defense_Config.ParryMasterParryDurationBonusValue}초, 방패 방어력 +{Defense_Config.ParryMasterBlockPowerBonusValue}");
                    }
                }
            });

            // Tier 6: 마인드쉴드
            manager.AddSkill(new SkillNode {
                Id = "defense_Step6_mind",
                Name = "마인드쉴드",
                Description = "방어막 유지 +60초",
                RequiredPoints = 4,
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
                Name = "신경강화",
                Description = $"회피 +{Defense_Config.AttackDodgeBonusValue}%",
                RequiredPoints = 4,
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
                Name = "이단점프",
                Description = "공중에서 추가로 1회 점프",
                RequiredPoints = 4,
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
                Name = "요툰의 생명력",
                Description = $"체력 최대치 +{Defense_Config.BodyHealthBonusValue}%, 물리/마법 방어력 +{Defense_Config.BodyArmorBonusValue}%",
                RequiredPoints = 4,
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
                        SkillEffect.DrawFloatingText(player, $"체력 +{Defense_Config.BodyHealthBonusValue}%, 방어 +{Defense_Config.BodyArmorBonusValue}%");
                    }
                }
            });

            // Tier 6: 요툰의 방패
            manager.AddSkill(new SkillNode {
                Id = "defense_Step6_true",
                Name = "요툰의 방패",
                Description = $"방패 블럭 스태미나 -{Defense_Config.JotunnShieldBlockStaminaReductionValue}%, 일반 방패 이동속도 +{Defense_Config.JotunnShieldNormalSpeedBonusValue}%, 대형 방패 이동속도 +{Defense_Config.JotunnShieldTowerSpeedBonusValue}%",
                RequiredPoints = 4,
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
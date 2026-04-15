using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 속도 전문가 스킬트리 노드 등록
    /// </summary>
    public static class SpeedSkillData
    {
        public static void RegisterSpeedSkills()
        {
            var manager = SkillTreeManager.Instance;

            // === 속도 전문가 루트 ===
            manager.AddSkill(new SkillNode {
                Id = "speed_root",
                NameKey = "speed_root_name",
                DescriptionKey = "speed_root_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedRootMoveSpeedValue },
                RequiredPoints = Speed_Config.SpeedRootRequiredPointsValue,
                MaxLevel = 1,
                Tier = 0,
                Position = new Vector2(-90, -60),
                Category = "속도",
                IconNameLocked = "speed_lock",
                IconNameUnlocked = "speed_unlock",
                NextNodes = new List<string> { "speed_base" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player,
                            L.Get("speed_root_effect", Speed_Config.SpeedRootMoveSpeedValue),
                            new Color(0.2f, 0.9f, 1f), SkillEffect.SkillEffectTextType.Combat);
                        Plugin.Log.LogInfo($"[속도 전문가] 스킬 투자 완료: +{Speed_Config.SpeedRootMoveSpeedValue}% 이동속도");
                    }
                }
            });

            // 티어1: 민첩함의 기초
            manager.AddSkill(new SkillNode {
                Id = "speed_base",
                NameKey = "speed_base_name",
                DescriptionKey = "speed_base_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedBaseAttackSpeedValue, Speed_Config.SpeedBaseDodgeDurationValue, Speed_Config.SpeedBaseDodgeMoveSpeedValue },
                RequiredPoints = Speed_Config.SpeedStep1RequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-180, -100),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_root" },
                NextNodes = new List<string> { "melee_combo", "crossbow_reload2", "bow_speed2", "moving_cast" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player,
                            L.Get("speed_base_effect", Speed_Config.SpeedBaseAttackSpeedValue, Speed_Config.SpeedBaseDodgeMoveSpeedValue),
                            new Color(0.2f, 0.9f, 1f), SkillEffect.SkillEffectTextType.Combat);
                    }
                }
            });

            // 티어2: 무기별 특화 분기 (4개)
            manager.AddSkill(new SkillNode {
                Id = "melee_combo",
                NameKey = "melee_combo_name",
                DescriptionKey = "melee_combo_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedMeleeComboDurationValue, Speed_Config.SpeedMeleeComboAttackSpeedValue, Speed_Config.SpeedMeleeComboStaminaValue },
                RequiredPoints = Speed_Config.SpeedStep2_MeleeFlowRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-310, -80),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex1" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "crossbow_reload2",
                NameKey = "crossbow_reload2_name",
                DescriptionKey = "crossbow_reload2_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedCrossbowExpertSpeedValue, Speed_Config.SpeedCrossbowExpertDurationValue, Speed_Config.SpeedCrossbowExpertReloadValue },
                RequiredPoints = Speed_Config.SpeedStep2_CrossbowExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-270, -130),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex1" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "bow_speed2",
                NameKey = "bow_speed2_name",
                DescriptionKey = "bow_speed2_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedBowExpertStaminaValue, Speed_Config.SpeedBowExpertDrawSpeedValue },
                RequiredPoints = Speed_Config.SpeedStep2_BowExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-240, -160),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex2" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "moving_cast",
                NameKey = "moving_cast_name",
                DescriptionKey = "moving_cast_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedStaffCastMoveSpeedValue, Speed_Config.SpeedStaffCastEitrReductionValue },
                RequiredPoints = Speed_Config.SpeedStep2_MobileCastRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-200, -210),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex2" },
                ApplyEffect = (lv) => { }
            });

            // 티어3: 수련자 (2개)
            manager.AddSkill(new SkillNode {
                Id = "speed_ex1",
                NameKey = "speed_ex1_name",
                DescriptionKey = "speed_ex1_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedEx1MeleeSkillValue, Speed_Config.SpeedEx1CrossbowSkillValue },
                RequiredPoints = Speed_Config.SpeedStep3_Practitioner1RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-330, -160),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "melee_combo", "crossbow_reload2" },
                NextNodes = new List<string> { "speed_master", "ship_master" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        float skillLevelBonus = Speed_Config.SpeedEx1MeleeSkillValue;
                        SkillEffect.ShowSkillEffectText(player,
                            L.Get("speed_ex1_effect", skillLevelBonus),
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo($"[수련자1] 근접/석궁 숙련도 +{skillLevelBonus} 보너스 활성화");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "speed_ex2",
                NameKey = "speed_ex2_name",
                DescriptionKey = "speed_ex2_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedEx2StaffSkillValue, Speed_Config.SpeedEx2BowSkillValue },
                RequiredPoints = Speed_Config.SpeedStep3_Practitioner2RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-300, -190),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_speed2", "moving_cast" },
                NextNodes = new List<string> { "speed_master", "ship_master" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        float skillLevelBonus = Speed_Config.SpeedEx2StaffSkillValue;
                        SkillEffect.ShowSkillEffectText(player,
                            L.Get("speed_ex2_effect", skillLevelBonus),
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo($"[수련자2] 지팡이/활 숙련도 +{skillLevelBonus} 보너스 활성화");
                    }
                }
            });

            // 티어4: 마스터 (2개)
            manager.AddSkill(new SkillNode {
                Id = "speed_master",
                NameKey = "speed_master_name",
                DescriptionKey = "speed_master_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedFoodEfficiencyValue },
                RequiredPoints = Speed_Config.SpeedStep4_EnergizerRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-390, -190),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_ex1", "speed_ex2" },
                NextNodes = new List<string> { "agility_peak" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "ship_master",
                NameKey = "ship_master_name",
                DescriptionKey = "ship_master_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedShipBonusValue },
                RequiredPoints = Speed_Config.SpeedStep4_CaptainRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-360, -220),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_ex1", "speed_ex2" },
                NextNodes = new List<string> { "agility_peak" },
                ApplyEffect = (lv) => { }
            });

            // 티어5: 점프 숙련자
            manager.AddSkill(new SkillNode {
                Id = "agility_peak",
                NameKey = "agility_peak_name",
                DescriptionKey = "agility_peak_desc",
                DescriptionArgs = new object[] { Speed_Config.JumpSkillLevelBonusValue, Speed_Config.JumpStaminaReductionValue },
                RequiredPoints = Speed_Config.SpeedStep5RequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-425, -230),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_master", "ship_master" },
                NextNodes = new List<string> { "speed_1", "speed_2", "speed_3" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        float skillLevelBonus = Speed_Config.JumpSkillLevelBonusValue;
                        SkillEffect.ShowSkillEffectText(player,
                            L.Get("agility_peak_effect", skillLevelBonus),
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo($"[점프 숙련자] 점프 숙련도 +{skillLevelBonus} 보너스 활성화");
                    }
                }
            });

            // 티어6: 스텟 증가 (3개)
            manager.AddSkill(new SkillNode {
                Id = "speed_1",
                NameKey = "speed_1_name",
                DescriptionKey = "speed_1_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedDexterityAttackSpeedBonusValue, Speed_Config.SpeedDexterityMoveSpeedBonusValue },
                RequiredPoints = Speed_Config.SpeedStep6_DexterityRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-510, -210),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "agility_peak" },
                NextNodes = new List<string> { "all_master" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "speed_2",
                NameKey = "speed_2_name",
                DescriptionKey = "speed_2_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedEnduranceStaminaBonusValue },
                RequiredPoints = Speed_Config.SpeedStep6_EnduranceRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-475, -250),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "agility_peak" },
                NextNodes = new List<string> { "all_master" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "speed_3",
                NameKey = "speed_3_name",
                DescriptionKey = "speed_3_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedIntellectEitrBonusValue },
                RequiredPoints = Speed_Config.SpeedStep6_IntellectRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-430, -290),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "agility_peak" },
                NextNodes = new List<string> { "all_master" },
                ApplyEffect = (lv) => { }
            });

            // 티어7: 숙련자
            manager.AddSkill(new SkillNode {
                Id = "all_master",
                NameKey = "all_master_name",
                DescriptionKey = "all_master_desc",
                DescriptionArgs = new object[] { Speed_Config.AllMasterRunSkillValue, Speed_Config.AllMasterJumpSkillValue },
                RequiredPoints = Speed_Config.SpeedStep7RequiredPointsValue,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(-565, -280),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_1", "speed_2", "speed_3" },
                NextNodes = new List<string> { "melee_speed1", "crossbow_draw1", "bow_draw1", "staff_speed1" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        float skillLevelBonus = Speed_Config.AllMasterRunSkillValue;
                        SkillEffect.ShowSkillEffectText(player,
                            L.Get("all_master_effect", skillLevelBonus),
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo($"[숙련자] 달리기/점프 숙련도 +{skillLevelBonus} 보너스 활성화");
                    }
                }
            });

            // 티어8: 최종 가속 스킬들 (4개)
            manager.AddSkill(new SkillNode {
                Id = "melee_speed1",
                NameKey = "melee_speed1_name",
                DescriptionKey = "melee_speed1_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedMeleeAttackSpeedValue, Speed_Config.SpeedMeleeComboTripleBonusValue },
                RequiredPoints = Speed_Config.SpeedStep8_MeleeAccelRequiredPointsValue,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-645, -250),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "crossbow_draw1",
                NameKey = "crossbow_draw1_name",
                DescriptionKey = "crossbow_draw1_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedCrossbowDrawSpeedValue, Speed_Config.SpeedCrossbowReloadMoveSpeedValue },
                RequiredPoints = Speed_Config.SpeedStep8_CrossbowAccelRequiredPointsValue,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-665, -300),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "bow_draw1",
                NameKey = "bow_draw1_name",
                DescriptionKey = "bow_draw1_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedBowDrawSpeedValue, Speed_Config.SpeedBowDrawMoveSpeedValue },
                RequiredPoints = Speed_Config.SpeedStep8_BowAccelRequiredPointsValue,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-635, -350),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "staff_speed1",
                NameKey = "staff_speed1_name",
                DescriptionKey = "staff_speed1_desc",
                DescriptionArgs = new object[] { Speed_Config.SpeedStaffCastSpeedFinalValue, Speed_Config.SpeedStaffTripleEitrRecoveryValue },
                RequiredPoints = Speed_Config.SpeedStep8_CastAccelRequiredPointsValue,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-565, -350),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });
        }
    }
}

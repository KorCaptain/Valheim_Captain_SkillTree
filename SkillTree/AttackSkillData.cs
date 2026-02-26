using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 공격 전문가 스킬트리 노드 등록
    /// </summary>
    public static class AttackSkillData
    {
        public static void RegisterAttackSkills()
        {
            var manager = SkillTreeManager.Instance;

            // === 공격 전문가 루트 ===
            manager.AddSkill(new SkillNode {
                Id = "attack_root",
                NameKey = "attack_root_name",
                DescriptionKey = "attack_root_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackRootDamageBonusValue },
                RequiredPoints = Attack_Config.AttackRootRequiredPointsValue,
                MaxLevel = 1,
                Position = new Vector2(0, 95),
                Category = "공격",
                IconNameLocked = "attack_lock",
                IconNameUnlocked = "attack_unlock",
                NextNodes = new List<string> { "atk_base" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, $"⚔️ {L.Get("attack_expert_acquired")}",
                            new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo("[공격 전문가] 효과 적용 완료 - 모든 데미지 +5% (Harmony 패치를 통해 자동 적용)");
                    }
                }
            });

            // 1단계: 기본 공격
            manager.AddSkill(new SkillNode {
                Id = "atk_base",
                NameKey = "atk_base_name",
                DescriptionKey = "atk_base_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackBasePhysicalDamageValue, SkillTreeConfig.AttackBaseElementalDamageValue },
                RequiredPoints = Attack_Config.AttackStep1RequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(0, 155),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "attack_root" },
                NextNodes = new List<string> { "atk_melee_bonus", "atk_bow_bonus", "atk_crossbow_bonus", "atk_staff_bonus" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_base_effect"),
                            new Color(0.8f, 0.6f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // 2단계: 무기별 특화
            manager.AddSkill(new SkillNode {
                Id = "atk_melee_bonus",
                NameKey = "atk_melee_bonus_name",
                DescriptionKey = "atk_melee_bonus_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackMeleeBonusChanceValue, SkillTreeConfig.AttackMeleeBonusDamageValue },
                RequiredPoints = Attack_Config.AttackStep2MeleeRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-90, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, L.Get("atk_melee_bonus_effect"));
                        Plugin.Log.LogInfo("[근접 특화] 효과 적용 완료");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_bow_bonus",
                NameKey = "atk_bow_bonus_name",
                DescriptionKey = "atk_bow_bonus_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackBowBonusChanceValue, SkillTreeConfig.AttackBowBonusDamageValue },
                RequiredPoints = Attack_Config.AttackStep2BowRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-45, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_crossbow_bonus",
                NameKey = "atk_crossbow_bonus_name",
                DescriptionKey = "atk_crossbow_bonus_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackCrossbowBonusChanceValue },
                RequiredPoints = Attack_Config.AttackStep2CrossbowRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(45, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_staff_bonus",
                NameKey = "atk_staff_bonus_name",
                DescriptionKey = "atk_staff_bonus_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackStaffBonusChanceValue, SkillTreeConfig.AttackStaffBonusDamageValue },
                RequiredPoints = Attack_Config.AttackStep2StaffRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(90, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => { }
            });

            // 3단계: 공격 증가
            manager.AddSkill(new SkillNode {
                Id = "atk_twohand_drain",
                NameKey = "atk_twohand_drain_name",
                DescriptionKey = "atk_twohand_drain_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackTwoHandDrainPhysicalDamageValue, SkillTreeConfig.AttackTwoHandDrainElementalDamageValue },
                RequiredPoints = Attack_Config.AttackStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(0, 275),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_melee_bonus", "atk_bow_bonus", "atk_crossbow_bonus", "atk_staff_bonus" },
                NextNodes = new List<string> { "atk_melee_crit", "atk_crit_chance", "atk_ranged_enhance" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_twohand_drain_effect"),
                            new Color(0.9f, 0.7f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[공격 증가] 효과 적용 완료 - 힘+{SkillTreeConfig.AttackStatBonusValue}, 지능+{SkillTreeConfig.AttackStatBonusValue}");
                    }
                }
            });

            // 4단계: 세부 강화
            manager.AddSkill(new SkillNode {
                Id = "atk_melee_crit",
                NameKey = "atk_melee_crit_name",
                DescriptionKey = "atk_melee_crit_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackMeleeEnhancementValue },
                RequiredPoints = Attack_Config.AttackStep4MeleeRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-60, 325),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_twohand_drain" },
                NextNodes = new List<string> { "atk_special" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_crit_chance",
                NameKey = "atk_crit_chance_name",
                DescriptionKey = "atk_crit_chance_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackCritChanceValue },
                RequiredPoints = Attack_Config.AttackStep4CritRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(0, 325),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_twohand_drain" },
                NextNodes = new List<string> { "atk_special" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_ranged_enhance",
                NameKey = "atk_ranged_enhance_name",
                DescriptionKey = "atk_ranged_enhance_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackRangedEnhancementValue },
                RequiredPoints = Attack_Config.AttackStep4RangedRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(60, 325),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_twohand_drain" },
                NextNodes = new List<string> { "atk_special" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_ranged_enhance_effect"),
                            new Color(0.2f, 0.8f, 0.8f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[원거리 강화] 효과 적용 완료 - 원거리 무기 공격력 +{SkillTreeConfig.AttackRangedEnhancementValue}%");
                    }
                }
            });

            // 5단계: 특수화 스탯
            manager.AddSkill(new SkillNode {
                Id = "atk_special",
                NameKey = "atk_special_name",
                DescriptionKey = "atk_special_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackSpecialStatValue },
                RequiredPoints = Attack_Config.AttackStep5RequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(0, 375),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_melee_crit", "atk_crit_chance", "atk_ranged_enhance" },
                NextNodes = new List<string> { "atk_crit_dmg", "atk_finisher_melee", "atk_twohand_crush", "atk_staff_mage" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_special_effect", SkillTreeConfig.AttackSpecialStatValue),
                            new Color(1f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[특수화 스탯] 효과 적용 완료 - 치명타 확률 +{SkillTreeConfig.AttackSpecialStatValue}%");
                    }
                }
            });

            // 6단계: 최종 특화
            manager.AddSkill(new SkillNode {
                Id = "atk_crit_dmg",
                NameKey = "atk_crit_dmg_name",
                DescriptionKey = "atk_crit_dmg_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackCritDamageBonusValue },
                RequiredPoints = Attack_Config.AttackStep6CritDmgRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-90, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_finisher_melee",
                NameKey = "atk_finisher_melee_name",
                DescriptionKey = "atk_finisher_melee_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackFinisherMeleeBonusValue },
                RequiredPoints = Attack_Config.AttackStep6FinisherRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-45, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_twohand_crush",
                NameKey = "atk_twohand_crush_name",
                DescriptionKey = "atk_twohand_crush_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackTwoHandedBonusValue },
                RequiredPoints = Attack_Config.AttackStep6TwoHandRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(45, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_staff_mage",
                NameKey = "atk_staff_mage_name",
                DescriptionKey = "atk_staff_mage_desc",
                DescriptionArgs = new object[] { SkillTreeConfig.AttackStaffElementalValue },
                RequiredPoints = Attack_Config.AttackStep6StaffRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(90, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_staff_mage_effect"),
                            new Color(0.8f, 0.2f, 0.8f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[속성 공격] 효과 적용 완료 - 속성 공격 +{SkillTreeConfig.AttackStaffElementalValue}% (활, 지팡이)");
                    }
                }
            });
        }
    }
}

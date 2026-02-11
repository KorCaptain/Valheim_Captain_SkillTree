using System.Collections.Generic;
using UnityEngine;

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
                Name = "공격 전문가",
                Description = $"모든 데미지 +{SkillTreeConfig.AttackRootDamageBonusValue}%",
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
                        SkillEffect.ShowSkillEffectText(player, "⚔️ 공격 전문가 습득!",
                            new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo("[공격 전문가] 효과 적용 완료 - 모든 데미지 +5% (Harmony 패치를 통해 자동 적용)");
                    }
                }
            });

            // 1단계: 기본 공격
            manager.AddSkill(new SkillNode {
                Id = "atk_base",
                Name = "기본 공격",
                Description = $"물리 공격력 +{SkillTreeConfig.AttackBasePhysicalDamageValue}, 속성 공격력 +{SkillTreeConfig.AttackBaseElementalDamageValue}",
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
                        SkillEffect.ShowSkillEffectText(player, "💪 기본 공격 습득!",
                            new Color(0.8f, 0.6f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // 2단계: 무기별 특화
            manager.AddSkill(new SkillNode {
                Id = "atk_melee_bonus",
                Name = "근접 특화",
                Description = $"근접 무기 사용 시 {SkillTreeConfig.AttackMeleeBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackMeleeBonusDamageValue}% 근접 공격력 증가",
                RequiredPoints = Attack_Config.AttackStep2RequiredPointsValue,
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
                        SkillEffect.DrawFloatingText(player, "⚔️ 근접 특화 습득!");
                        Plugin.Log.LogInfo("[근접 특화] 효과 적용 완료");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_bow_bonus",
                Name = "활 특화",
                Description = $"활 사용 시 {SkillTreeConfig.AttackBowBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackBowBonusDamageValue}% 활 공격력 증가",
                RequiredPoints = Attack_Config.AttackStep2RequiredPointsValue,
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
                Name = "석궁 특화",
                Description = $"석궁 사용 시 {SkillTreeConfig.AttackCrossbowBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackCrossbowBonusDamageValue}% 석궁 공격력 증가",
                RequiredPoints = Attack_Config.AttackStep2RequiredPointsValue,
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
                Name = "지팡이 특화",
                Description = $"지팡이 사용 시 {SkillTreeConfig.AttackStaffBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackStaffBonusDamageValue}% 지팡이 공격력 증가 + 주변 적에게 속성 피해",
                RequiredPoints = Attack_Config.AttackStep2RequiredPointsValue,
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
                Name = "공격 증가",
                Description = $"물리 공격력 +{SkillTreeConfig.AttackTwoHandDrainPhysicalDamageValue}, 속성 공격력 +{SkillTreeConfig.AttackTwoHandDrainElementalDamageValue}",
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
                        SkillEffect.ShowSkillEffectText(player, $"💪 공격 증가!",
                            new Color(0.9f, 0.7f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[공격 증가] 효과 적용 완료 - 힘+{SkillTreeConfig.AttackStatBonusValue}, 지능+{SkillTreeConfig.AttackStatBonusValue}");
                    }
                }
            });

            // 4단계: 세부 강화
            manager.AddSkill(new SkillNode {
                Id = "atk_melee_crit",
                Name = "근접 강화",
                Description = $"근접무기 2연속 공격 시 +{SkillTreeConfig.AttackMeleeEnhancementValue}% 추가 피해",
                RequiredPoints = Attack_Config.AttackStep4RequiredPointsValue,
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
                Name = "정밀 공격",
                Description = $"치명타 확률 +{SkillTreeConfig.AttackCritChanceValue}%",
                RequiredPoints = Attack_Config.AttackStep4RequiredPointsValue,
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
                Name = "원거리 강화",
                Description = $"원거리 무기 공격력 +{SkillTreeConfig.AttackRangedEnhancementValue}% (석궁, 활, 지팡이)",
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
                        SkillEffect.ShowSkillEffectText(player, $"🏹 원거리 강화!",
                            new Color(0.2f, 0.8f, 0.8f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[원거리 강화] 효과 적용 완료 - 원거리 무기 공격력 +{SkillTreeConfig.AttackRangedEnhancementValue}%");
                    }
                }
            });

            // 5단계: 특수화 스탯
            manager.AddSkill(new SkillNode {
                Id = "atk_special",
                Name = "특수화 스탯",
                Description = $"치명타 확률 +{SkillTreeConfig.AttackSpecialStatValue}%",
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
                        SkillEffect.ShowSkillEffectText(player, $"⭐ 특수화 스탯! 치명타 확률 +{SkillTreeConfig.AttackSpecialStatValue}%",
                            new Color(1f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[특수화 스탯] 효과 적용 완료 - 치명타 확률 +{SkillTreeConfig.AttackSpecialStatValue}%");
                    }
                }
            });

            // 6단계: 최종 특화
            manager.AddSkill(new SkillNode {
                Id = "atk_crit_dmg",
                Name = "약점 공격",
                Description = $"치명타 피해 +{SkillTreeConfig.AttackCritDamageBonusValue}%",
                RequiredPoints = Attack_Config.AttackStep6RequiredPointsValue,
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
                Name = "연속 근접의 대가",
                Description = $"근접 3연속 공격 시 +{SkillTreeConfig.AttackFinisherMeleeBonusValue}% 추가 피해",
                RequiredPoints = Attack_Config.AttackStep6RequiredPointsValue,
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
                Name = "양손 분쇄",
                Description = $"양손 무기 공격력 +{SkillTreeConfig.AttackTwoHandedBonusValue}%",
                RequiredPoints = Attack_Config.AttackStep6RequiredPointsValue,
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
                Name = "속성 공격",
                Description = $"속성 공격 +{SkillTreeConfig.AttackStaffElementalValue}% (활, 지팡이)",
                RequiredPoints = Attack_Config.AttackStep6RequiredPointsValue,
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
                        SkillEffect.ShowSkillEffectText(player, $"🔥 속성 공격!",
                            new Color(0.8f, 0.2f, 0.8f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[속성 공격] 효과 적용 완료 - 속성 공격 +{SkillTreeConfig.AttackStaffElementalValue}% (활, 지팡이)");
                    }
                }
            });
        }
    }
}

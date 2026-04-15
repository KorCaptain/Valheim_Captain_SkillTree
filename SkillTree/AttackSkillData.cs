using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 공격 전문가 스킬트리 노드 등록
    /// 선빵(T1) → 무기특화(T2) → 추격전(T3) → 전환(T4) → 난전(T5) → 마무리(T6)
    /// </summary>
    public static class AttackSkillData
    {
        public static void RegisterAttackSkills()
        {
            var manager = SkillTreeManager.Instance;

            // ========================================================
            // Tier 0: 공격 전문가 루트
            // ========================================================
            manager.AddSkill(new SkillNode {
                Id = "attack_root",
                NameKey = "attack_root_name",
                DescriptionKey = "attack_root_desc",
                DescriptionArgs = new object[] { Attack_Config.AttackRootDamageBonusValue },
                RequiredPoints = Attack_Config.AttackRootRequiredPointsValue,
                MaxLevel = 1,
                Position = new Vector2(0, 95),
                Category = "공격",
                IconNameLocked = "attack_lock",
                IconNameUnlocked = "attack_unlock",
                NextNodes = new List<string> { "atk_opener" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("attack_root_effect"),
                            new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                }
            });

            // ========================================================
            // Tier 1: 선빵
            // ========================================================
            manager.AddSkill(new SkillNode {
                Id = "atk_opener",
                NameKey = "atk_opener_name",
                DescriptionKey = "atk_opener_desc",
                DescriptionArgs = new object[] {
                    Attack_Config.AtkOpenerDamageBonusValue,
                    Attack_Config.AtkOpenerStaminaReductionValue,
                    Attack_Config.AtkOpenerDurationValue,
                    Attack_Config.AtkOpenerCooldownValue
                },
                RequiredPoints = Attack_Config.AtkOpenerRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(0, 145),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "attack_root" },
                NextNodes = new List<string> { "atk_opener_melee", "atk_opener_bow", "atk_opener_crossbow", "atk_opener_magic" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_effect"),
                            new Color(1f, 0.6f, 0.1f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            // ========================================================
            // Tier 2: 무기별 선빵 특화 (복수 선택 가능)
            // ========================================================
            manager.AddSkill(new SkillNode {
                Id = "atk_opener_melee",
                NameKey = "atk_opener_melee_name",
                DescriptionKey = "atk_opener_melee_desc",
                DescriptionArgs = new object[] { Attack_Config.AtkOpenerMeleeFinisherBonusValue },
                RequiredPoints = Attack_Config.AtkOpenerMeleeRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-90, 205),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_opener" },
                NextNodes = new List<string> { "atk_pursuit" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_melee_effect"),
                            new Color(0.9f, 0.4f, 0.1f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_opener_bow",
                NameKey = "atk_opener_bow_name",
                DescriptionKey = "atk_opener_bow_desc",
                DescriptionArgs = new object[] { Attack_Config.AtkOpenerBowCritChanceValue },
                RequiredPoints = Attack_Config.AtkOpenerBowRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-30, 205),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_opener" },
                NextNodes = new List<string> { "atk_pursuit" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_bow_effect"),
                            new Color(0.2f, 0.9f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_opener_crossbow",
                NameKey = "atk_opener_crossbow_name",
                DescriptionKey = "atk_opener_crossbow_desc",
                DescriptionArgs = new object[] { Attack_Config.AtkOpenerCrossbowFirstShotBonusValue },
                RequiredPoints = Attack_Config.AtkOpenerCrossbowRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(30, 205),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_opener" },
                NextNodes = new List<string> { "atk_pursuit" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_crossbow_effect"),
                            new Color(0.2f, 0.6f, 1f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_opener_magic",
                NameKey = "atk_opener_magic_name",
                DescriptionKey = "atk_opener_magic_desc",
                DescriptionArgs = new object[] { },
                RequiredPoints = Attack_Config.AtkOpenerMagicRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(90, 205),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_opener" },
                NextNodes = new List<string> { "atk_pursuit" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_magic_effect"),
                            new Color(0.8f, 0.2f, 0.9f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            // ========================================================
            // Tier 3: 추격전
            // ========================================================
            manager.AddSkill(new SkillNode {
                Id = "atk_pursuit",
                NameKey = "atk_pursuit_name",
                DescriptionKey = "atk_pursuit_desc",
                DescriptionArgs = new object[] {
                    Attack_Config.AtkPursuitDamageBonusValue,
                    Attack_Config.AtkPursuitChainDamageBonusValue,
                    Attack_Config.AtkPursuitChainWindowValue
                },
                RequiredPoints = Attack_Config.AtkPursuitRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(0, 275),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_opener_melee", "atk_opener_bow", "atk_opener_crossbow", "atk_opener_magic" },
                NextNodes = new List<string> { "atk_pursuit_speed", "atk_frenzy_trigger" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_pursuit_effect"),
                            new Color(0.3f, 0.8f, 1f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            // ========================================================
            // Tier 4: 전환 분기
            // ========================================================
            manager.AddSkill(new SkillNode {
                Id = "atk_pursuit_speed",
                NameKey = "atk_pursuit_speed_name",
                DescriptionKey = "atk_pursuit_speed_desc",
                DescriptionArgs = new object[] { Attack_Config.AtkPursuitSpeedBonusValue },
                RequiredPoints = Attack_Config.AtkPursuitSpeedRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-45, 335),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_pursuit" },
                NextNodes = new List<string> { "atk_frenzy" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_pursuit_speed_effect"),
                            new Color(0.4f, 1f, 0.6f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_frenzy_trigger",
                NameKey = "atk_frenzy_trigger_name",
                DescriptionKey = "atk_frenzy_trigger_desc",
                DescriptionArgs = new object[] { Attack_Config.AtkFrenzyTriggerStaminaReductionValue },
                RequiredPoints = Attack_Config.AtkFrenzyTriggerRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(45, 335),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_pursuit" },
                NextNodes = new List<string> { "atk_frenzy" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_frenzy_trigger_effect"),
                            new Color(1f, 0.4f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                }
            });

            // ========================================================
            // Tier 5: 난전
            // ========================================================
            manager.AddSkill(new SkillNode {
                Id = "atk_frenzy",
                NameKey = "atk_frenzy_name",
                DescriptionKey = "atk_frenzy_desc",
                DescriptionArgs = new object[] {
                    Attack_Config.AtkFrenzyHitsPerStackValue,
                    Attack_Config.AtkFrenzyStackBonusBaseValue,
                    Attack_Config.AtkFrenzyMaxStacksValue,
                    Attack_Config.AtkFrenzyStackBonusChainValue,
                    Attack_Config.AtkFrenzyTier6AmplifierValue
                },
                RequiredPoints = Attack_Config.AtkFrenzyRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(0, 395),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_pursuit_speed", "atk_frenzy_trigger" },
                NextNodes = new List<string> { "atk_crit_dmg", "atk_finisher_melee", "atk_twohand_crush", "atk_staff_mage" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_frenzy_effect"),
                            new Color(1f, 0.2f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                }
            });

            // ========================================================
            // Tier 6: 마무리 최종 (기존 유지, Prerequisites만 변경)
            // ========================================================
            manager.AddSkill(new SkillNode {
                Id = "atk_crit_dmg",
                NameKey = "atk_crit_dmg_name",
                DescriptionKey = "atk_crit_dmg_desc",
                DescriptionArgs = new object[] { Attack_Config.AttackCritDamageBonusValue },
                RequiredPoints = Attack_Config.AttackStep6CritDmgRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-90, 445),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_frenzy" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_finisher_melee",
                NameKey = "atk_finisher_melee_name",
                DescriptionKey = "atk_finisher_melee_desc",
                DescriptionArgs = new object[] { Attack_Config.AttackFinisherMeleeBonusValue },
                RequiredPoints = Attack_Config.AttackStep6FinisherRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-30, 445),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_frenzy" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_twohand_crush",
                NameKey = "atk_twohand_crush_name",
                DescriptionKey = "atk_twohand_crush_desc",
                DescriptionArgs = new object[] { Attack_Config.AttackTwoHandedBonusValue },
                RequiredPoints = Attack_Config.AttackStep6TwoHandRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(30, 445),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_frenzy" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_staff_mage",
                NameKey = "atk_staff_mage_name",
                DescriptionKey = "atk_staff_mage_desc",
                DescriptionArgs = new object[] { Attack_Config.AttackStaffElementalValue },
                RequiredPoints = Attack_Config.AttackStep6StaffRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(90, 445),
                Category = "공격",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_frenzy" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_staff_mage_effect"),
                            new Color(0.8f, 0.2f, 0.8f), SkillEffect.SkillEffectTextType.Standard);
                }
            });
        }
    }
}

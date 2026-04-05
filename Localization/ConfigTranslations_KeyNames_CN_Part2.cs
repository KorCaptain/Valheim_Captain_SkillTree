using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetChineseKeyNames_Part2()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // 长矛树 - 35个键
                // ============================================

                // === Tier 0: 长矛专家 (6) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: [长矛专家] 所需点数",
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: [长矛专家] 2连击攻击速度加成 (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: [长矛专家] 2连击伤害加成 (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: [长矛专家] 效果持续时间 (秒)",
                ["Tier0_SpearExpert_ProcChance"] = "Tier 0: [长矛专家] 触发概率 (%)",
                ["Tier0_SpearExpert_SpeedBoost"] = "Tier 0: [长矛专家] 速度强化 (%)",

                // === Tier 1: 要害打击 (2) ===
                ["Tier1_QuickStrike_RequiredPoints"] = "Tier 1: [要害打击] 所需点数",
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: [要害打击] 暴击伤害加成 (%)",

                // === Tier 2: 投掷长矛 (3) ===
                ["Tier2_Throw_RequiredPoints"] = "Tier 2: [投掷长矛] 所需点数",
                ["Tier2_Throw_Cooldown"] = "Tier 2: [投掷长矛] 冷却时间 (秒)",
                ["Tier2_Throw_DamageMultiplier"] = "Tier 2: [投掷长矛] 伤害倍率 (%)",

                // === Tier 3-1: 快速穿刺 (2) ===
                ["Tier3_Pierce_RequiredPoints"] = "Tier 3-1: [快速穿刺] 所需点数",
                ["Tier3_Rapid_DamageBonus"] = "Tier 3-1: [快速穿刺] 穿刺伤害加成",

                // === Tier 3-2: 快速长矛 (1) ===
                ["Tier3_QuickSpear_AttackSpeed"] = "Tier 3-2: [快速长矛] 攻击速度 (%)",

                // === Tier 4-1: 闪避突刺 (3) ===
                ["Tier4_Evasion_RequiredPoints"] = "Tier 4-1: [闪避突刺] 所需点数",
                ["Tier4_Evasion_EvasionBonus"] = "Tier 4-1: [闪避突刺] 攻击时闪避加成 (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4-1: [闪避突刺] 体力消耗减少 (%)",

                // === Tier 4-2: 双重打击 (2) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4-2: [双重打击] 2连击伤害加成 (%)",
                ["Tier4_Dual_Duration"] = "Tier 4-2: [双重打击] 效果持续时间 (秒)",

                // === Tier 5-1: 穿透长矛 (6) ===
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5-1: [穿透长矛] 所需点数",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5-1: [穿透长矛] 效果持续时间 (秒)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5-1: [穿透长矛] 雷电伤害倍率 (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5-1: [穿透长矛] 雷电触发所需命中数",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5-1: [穿透长矛] G键冷却时间 (秒)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5-1: [穿透长矛] G键体力消耗",

                // === Tier 5-2: 连击长矛 (8) ===
                ["Tier5_Combo_RequiredPoints"] = "Tier 5-2: [连击长矛] 所需点数",
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5-2: [连击长矛] H键冷却时间 (秒)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5-2: [连击长矛] H键伤害倍率 (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5-2: [连击长矛] H键体力消耗",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5-2: [连击长矛] H键击退范围 (米)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5-2: [连击长矛] 主动效果范围 (米)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5-2: [连击长矛] 效果持续时间 (秒)",
                ["Tier5_Combo_MaxUses"] = "Tier 5-2: [连击长矛] 最大强化投掷次数",

                // ============================================
                // 法杖树 - 30个键
                // ============================================

                // === Tier 0: 法杖专家 (2) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] = "Tier 0: [法杖专家] 元素伤害加成 (%)",
                ["Tier0_StaffExpert_RequiredPoints"] = "Tier 0: [法杖专家] 所需点数",

                // === Tier 1-1: 精神专注 (2) ===
                ["Tier1_MindFocus_EitrReduction"] = "Tier 1-1: [精神专注] 以特尔消耗减少 (%)",
                ["Tier1_MindFocus_RequiredPoints"] = "Tier 1-1: [精神专注] 所需点数",

                // === Tier 1-2: 魔法流 (2) ===
                ["Tier1_MagicFlow_EitrBonus"] = "Tier 1-2: [魔法流] 最大以特尔加成",
                ["Tier1_MagicFlow_RequiredPoints"] = "Tier 1-2: [魔法流] 所需点数",

                // === Tier 2: 魔法强化 (4) ===
                ["Tier2_MagicAmplify_Chance"] = "Tier 2: [魔法强化] 触发概率 (%)",
                ["Tier2_MagicAmplify_DamageBonus"] = "Tier 2: [魔法强化] 元素伤害加成 (%)",
                ["Tier2_MagicAmplify_EitrCostIncrease"] = "Tier 2: [魔法强化] 以特尔消耗增加 (%)",
                ["Tier2_MagicAmplify_RequiredPoints"] = "Tier 2: [魔法强化] 所需点数",

                // === Tier 3-1: 冰霜元素 (2) ===
                ["Tier3_FrostElement_DamageBonus"] = "Tier 3-1: [冰霜元素] 伤害加成",
                ["Tier3_FrostElement_RequiredPoints"] = "Tier 3-1: [冰霜元素] 所需点数",

                // === Tier 3-2: 火焰元素 (2) ===
                ["Tier3_FireElement_DamageBonus"] = "Tier 3-2: [火焰元素] 伤害加成",
                ["Tier3_FireElement_RequiredPoints"] = "Tier 3-2: [火焰元素] 所需点数",

                // === Tier 3-3: 雷电元素 (2) ===
                ["Tier3_LightningElement_DamageBonus"] = "Tier 3-3: [雷电元素] 伤害加成",
                ["Tier3_LightningElement_RequiredPoints"] = "Tier 3-3: [雷电元素] 所需点数",

                // === Tier 4: 幸运法力 (2) ===
                ["Tier4_LuckyMana_Chance"] = "Tier 4: [幸运法力] 免费施法触发概率 (%)",
                ["Tier4_LuckyMana_RequiredPoints"] = "Tier 4: [幸运法力] 所需点数",

                // === Tier 5-1: 双重施法 - R键主动 (9) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] = "Tier 5-1: [双重施法] 额外弹射物数量",
                ["Tier5_DoubleCast_ProjectileDamagePercent"] = "Tier 5-1: [双重施法] 弹射物伤害百分比 (%)",
                ["Tier5_DoubleCast_EitrCost"] = "Tier 5-1: [双重施法] 以特尔消耗",
                ["Tier5_DoubleCast_Cooldown"] = "Tier 5-1: [双重施法] 冷却时间 (秒)",
                ["Tier5_DoubleCast_RequiredPoints"] = "Tier 5-1: [双重施法] 所需点数",
                ["Tier5_FanCast_SummonRadius"] = "Tier 5-1: [双重施法] 弧形半径 (m)",
                ["Tier5_FanCast_SummonHeight"] = "Tier 5-1: [双重施法] 召唤高度 (m)",
                ["Tier5_FanCast_HoverTime"] = "Tier 5-1: [双重施法] 悬浮时间 (秒)",
                ["Tier5_FanCast_LaunchGap"] = "Tier 5-1: [双重施法] 发射间隔 (秒)",

                // === Tier 5-2: 即时范围治疗 - H键主动 (5) ===
                ["Tier5_InstantAreaHeal_Cooldown"] = "Tier 5-2: [治疗] 冷却时间 (秒)",
                ["Tier5_InstantAreaHeal_EitrCost"] = "Tier 5-2: [治疗] 以特尔消耗",
                ["Tier5_InstantAreaHeal_HealPercent"] = "Tier 5-2: [治疗] 治疗量 (最大HP的%)",
                ["Tier5_InstantAreaHeal_Range"] = "Tier 5-2: [治疗] 治疗范围 (米)",
                ["Tier5_InstantAreaHeal_RequiredPoints"] = "Tier 5-2: [治疗] 所需点数",

                // ============================================
                // 弩树 - 34个键
                // ============================================

                // === Tier 0: 弩专家 (2) ===
                ["Tier0_CrossbowExpert_DamageBonus"] = "Tier 0: [弩专家] 弩伤害加成 (%)",
                ["Tier0_CrossbowExpert_RequiredPoints"] = "Tier 0: [弩专家] 所需点数",

                // === Tier 1: 快速射击 (6) ===
                ["Tier1_RapidFire_Chance"] = "Tier 1: [快速射击] 触发概率 (%)",
                ["Tier1_RapidFire_ShotCount"] = "Tier 1: [快速射击] 射击次数",
                ["Tier1_RapidFire_DamagePercent"] = "Tier 1: [快速射击] 伤害百分比 (%)",
                ["Tier1_RapidFire_Delay"] = "Tier 1: [快速射击] 射击延迟 (秒)",
                ["Tier1_RapidFire_BoltConsumption"] = "Tier 1: [快速射击] 弩箭消耗",
                ["Tier1_RapidFire_RequiredPoints"] = "Tier 1: [快速射击] 所需点数",

                // === Tier 2: 平衡瞄准 / 快速装填 / 真实射击 (7) ===
                ["Tier2_BalancedAim_KnockbackChance"] = "Tier 2-1: [平衡瞄准] 击退触发概率 (%)",
                ["Tier2_BalancedAim_KnockbackDistance"] = "Tier 2-1: [平衡瞄准] 击退距离 (米)",
                ["Tier2_BalancedAim_RequiredPoints"] = "Tier 2-1: [平衡瞄准] 所需点数",
                ["Tier2_RapidReload_SpeedIncrease"] = "Tier 2-2: [快速装填] 装填速度提升 (%)",
                ["Tier2_RapidReload_RequiredPoints"] = "Tier 2-2: [快速装填] 所需点数",
                ["Tier2_HonestShot_DamageBonus"] = "Tier 2-3: [真实射击] 伤害加成 (%)",
                ["Tier2_HonestShot_RequiredPoints"] = "Tier 2-3: [真实射击] 所需点数",

                // === Tier 3: 自动装填 (2) ===
                ["Tier3_AutoReload_Chance"] = "Tier 3: [自动装填] 触发概率 (%)",
                ["Tier3_AutoReload_RequiredPoints"] = "Tier 3: [自动装填] 所需点数",

                // === Tier 4: 快速射击Lv2 / 首击 (9) ===
                ["Tier4_RapidFireLv2_Chance"] = "Tier 4-1: [快速射击Lv2] 触发概率 (%)",
                ["Tier4_RapidFireLv2_ShotCount"] = "Tier 4-1: [快速射击Lv2] 射击次数",
                ["Tier4_RapidFireLv2_DamagePercent"] = "Tier 4-1: [快速射击Lv2] 伤害百分比 (%)",
                ["Tier4_RapidFireLv2_Delay"] = "Tier 4-1: [快速射击Lv2] 射击延迟 (秒)",
                ["Tier4_RapidFireLv2_BoltConsumption"] = "Tier 4-1: [快速射击Lv2] 弩箭消耗",
                ["Tier4_RapidFireLv2_RequiredPoints"] = "Tier 4-1: [快速射击Lv2] 所需点数",
                ["Tier4_FinalStrike_HpThreshold"] = "Tier 4-2: [首击] 敌人生命阈值 (%)",
                ["Tier4_FinalStrike_DamageBonus"] = "Tier 4-2: [首击] 额外伤害 (%)",
                ["Tier4_FinalStrike_RequiredPoints"] = "Tier 4-2: [首击] 所需点数",

                // === Tier 5: 一击必杀 - R键主动 (5) ===
                ["Tier5_OneShot_Duration"] = "Tier 5: [一击必杀] 效果持续时间 (秒)",
                ["Tier5_OneShot_DamageBonus"] = "Tier 5: [一击必杀] 伤害加成 (%)",
                ["Tier5_OneShot_KnockbackDistance"] = "Tier 5: [一击必杀] 击退距离 (米)",
                ["Tier5_OneShot_Cooldown"] = "Tier 5: [一击必杀] 冷却时间 (秒)",
                ["Tier5_OneShot_RequiredPoints"] = "Tier 5: [一击必杀] 所需点数",

                // === Tier 6: 火山之冰 (H键 主动技能) ===
                ["Tier6_IceBreath_Cooldown"] = "Tier 6: [火山之冰] 冷却时间 (秒)",
                ["Tier6_IceBreath_StaminaCost"] = "Tier 6: [火山之冰] 体力消耗",
                ["Tier6_IceBreath_FirstHitPercent"] = "Tier 6: [火山之冰] 首次打击伤害 (%)",
                ["Tier6_IceBreath_DotPercent"] = "Tier 6: [火山之冰] 持续伤害 (%)",
                ["Tier6_IceBreath_DotCount"] = "Tier 6: [火山之冰] 持续伤害次数",
                ["Tier6_IceBreath_RequiredPoints"] = "Tier 6: [火山之冰] 所需点数",

                // ============================================
                // 匕首树 - 32个键
                // ============================================

                // === Tier 0: 匕首专家 (2) ===
                ["Tier0_KnifeExpert_BackstabBonus"] = "Tier 0: [匕首专家] 背刺加成 (%)",
                ["Tier0_KnifeExpert_RequiredPoints"] = "Tier 0: [匕首专家] 所需点数",

                // === Tier 1: 闪避精通 (3) ===
                ["Tier1_Evasion_Chance"] = "Tier 1: [闪避精通] 触发概率 (%)",
                ["Tier1_Evasion_Duration"] = "Tier 1: [闪避精通] 持续时间 (秒)",
                ["Tier1_Evasion_RequiredPoints"] = "Tier 1: [闪避精通] 所需点数",

                // === Tier 2: 快速移动 (2) ===
                ["Tier2_FastMove_MoveSpeedBonus"] = "Tier 2: [快速移动] 移动速度加成 (%)",
                ["Tier2_FastMove_RequiredPoints"] = "Tier 2: [快速移动] 所需点数",

                // === Tier 3: 快速攻击 (3) ===
                ["Tier3_CombatMastery_DamageBonus"] = "Tier 3: [快速攻击] 斩击/穿刺伤害加成",
                ["Tier3_CombatMastery_BuffDuration"] = "Tier 3: [快速攻击] 效果持续时间 (秒)",
                ["Tier3_CombatMastery_RequiredPoints"] = "Tier 3: [快速攻击] 所需点数",

                // === Tier 4: 暴击精通 (4) ===
                ["Tier4_AttackEvasion_EvasionBonus"] = "Tier 4: [暴击精通] 闪避加成 (%)",
                ["Tier4_AttackEvasion_BuffDuration"] = "Tier 4: [暴击精通] 效果持续时间 (秒)",
                ["Tier4_AttackEvasion_Cooldown"] = "Tier 4: [暴击精通] 冷却时间 (秒)",
                ["Tier4_AttackEvasion_RequiredPoints"] = "Tier 4: [暴击精通] 所需点数",

                // === Tier 5: 致命伤害 (2) ===
                ["Tier5_CriticalDamage_DamageBonus"] = "Tier 5: [致命伤害] 伤害加成 (%)",
                ["Tier5_CriticalDamage_RequiredPoints"] = "Tier 5: [致命伤害] 所需点数",

                // === Tier 6: 刺客 (3) ===
                ["Tier6_Assassin_CritDamageBonus"] = "Tier 6: [刺客] 暴击伤害加成 (%)",
                ["Tier6_Assassin_CritChanceBonus"] = "Tier 6: [刺客] 暴击率加成 (%)",
                ["Tier6_Assassin_RequiredPoints"] = "Tier 6: [刺客] 所需点数",

                // === Tier 7: 暗杀 (3) ===
                ["Tier7_Assassination_StaggerChance"] = "Tier 7: [暗杀] 硬直触发概率 (%)",
                ["Tier7_Assassination_RequiredComboHits"] = "Tier 7: [暗杀] 所需连击次数",
                ["Tier7_Assassination_RequiredPoints"] = "Tier 7: [暗杀] 所需点数",

                // === Tier 8: 刺客之心 - G键主动 (10) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] = "Tier 8: [刺客之心] 暴击伤害倍率",
                ["Tier8_AssassinHeart_Duration"] = "Tier 8: [刺客之心] 效果持续时间 (秒)",
                ["Tier8_AssassinHeart_StaminaCost"] = "Tier 8: [刺客之心] 体力消耗",
                ["Tier8_AssassinHeart_Cooldown"] = "Tier 8: [刺客之心] 冷却时间 (秒)",
                ["Tier8_AssassinHeart_TeleportRange"] = "Tier 8: [刺客之心] 传送范围 (米)",
                ["Tier8_AssassinHeart_TeleportBackDistance"] = "Tier 8: [刺客之心] 目标后方距离 (米)",
                ["Tier8_AssassinHeart_StunDuration"] = "Tier 8: [刺客之心] 眩晕持续时间 (秒)",
                ["Tier8_AssassinHeart_ComboAttackCount"] = "Tier 8: [刺客之心] 连击攻击次数",
                ["Tier8_AssassinHeart_AttackInterval"] = "Tier 8: [刺客之心] 攻击间隔 (秒)",
                ["Tier8_AssassinHeart_RequiredPoints"] = "Tier 8: [刺客之心] 所需点数",

                // ============================================
                // 剑树 (新格式) - 33个键
                // ============================================

                // === Tier 0: 剑专家 (2) ===
                ["Tier0_SwordExpert_DamageBonus"] = "Tier 0: [剑专家] 剑伤害加成 (%)",
                ["Tier0_SwordExpert_RequiredPoints"] = "Tier 0: [剑专家] 所需点数",

                // === Tier 1-1: 快速斩 (2) ===
                ["Tier1_FastSlash_RequiredPoints"] = "Tier 1-1: [快速斩] 所需点数",
                ["Tier1_FastSlash_AttackSpeedBonus"] = "Tier 1-1: [快速斩] 攻击速度加成 (%)",

                // === Tier 1-2: 反击姿态 (3) ===
                ["Tier1_CounterStance_RequiredPoints"] = "Tier 1-2: [反击姿态] 所需点数",
                ["Tier1_CounterStance_Duration"] = "Tier 1-2: [反击姿态] 效果持续时间 (秒)",
                ["Tier1_CounterStance_DefenseBonus"] = "Tier 1-2: [反击姿态] 防御加成 (%)",

                // === Tier 2: 连击斩 (3) ===
                ["Tier2_ComboSlash_RequiredPoints"] = "Tier 2: [连击斩] 所需点数",
                ["Tier2_ComboSlash_DamageBonus"] = "Tier 2: [连击斩] 伤害加成 (%)",
                ["Tier2_ComboSlash_BuffDuration"] = "Tier 2: [连击斩] 效果持续时间 (秒)",

                // === Tier 3: 还击 (2) ===
                ["Tier3_Riposte_RequiredPoints"] = "Tier 3: [还击] 所需点数",
                ["Tier3_Riposte_DamageBonus"] = "Tier 3: [还击] 斩击伤害加成",

                // === Tier 4-1: 全力以赴 (3) ===
                ["Tier4_AllInOne_RequiredPoints"] = "Tier 4-1: [全力以赴] 所需点数",
                ["Tier4_AllInOne_AttackBonus"] = "Tier 4-1: [全力以赴] 攻击加成 (%)",
                ["Tier4_AllInOne_DefenseBonus"] = "Tier 4-1: [全力以赴] 防御加成",

                // === Tier 4-2: 真实决斗 (2) ===
                ["Tier4_TrueDuel_RequiredPoints"] = "Tier 4-2: [真实决斗] 所需点数",
                ["Tier4_TrueDuel_AttackSpeedBonus"] = "Tier 4-2: [真实决斗] 攻击速度加成 (%)",

                // === Tier 5: 招架冲锋 - G键主动 (6) ===
                ["Tier5_ParryRush_RequiredPoints"] = "Tier 5: [招架冲锋] 所需点数",
                ["Tier5_ParryRush_BuffDuration"] = "Tier 5: [招架冲锋] 效果持续时间 (秒)",
                ["Tier5_ParryRush_BlockPowerRatio"] = "Tier 5: [招架冲锋] 格挡力",
                ["Tier5_ParryRush_PushDistance"] = "Tier 5: [招架冲锋] 推击距离 (米)",
                ["Tier5_ParryRush_StaminaCost"] = "Tier 5: [招架冲锋] 体力消耗",
                ["Tier5_ParryRush_Cooldown"] = "Tier 5: [招架冲锋] 冷却时间 (秒)",

                // === Tier 6: 冲锋斩 - G键主动 (10) ===
                ["Tier6_RushSlash_RequiredPoints"] = "Tier 6: [冲锋斩] 所需点数",
                ["Tier6_RushSlash_Hit1DamageRatio"] = "Tier 6: [冲锋斩] 第1击伤害比率 (%)",
                ["Tier6_RushSlash_Hit2DamageRatio"] = "Tier 6: [冲锋斩] 第2击伤害比率 (%)",
                ["Tier6_RushSlash_Hit3DamageRatio"] = "Tier 6: [冲锋斩] 第3击伤害比率 (%)",
                ["Tier6_RushSlash_InitialDistance"] = "Tier 6: [冲锋斩] 初始冲刺距离 (米)",
                ["Tier6_RushSlash_SideDistance"] = "Tier 6: [冲锋斩] 侧移距离 (米)",
                ["Tier6_RushSlash_StaminaCost"] = "Tier 6: [冲锋斩] 体力消耗",
                ["Tier6_RushSlash_Cooldown"] = "Tier 6: [冲锋斩] 冷却时间 (秒)",
                ["Tier6_RushSlash_MoveSpeed"] = "Tier 6: [冲锋斩] 移动速度 (米/秒)",
                ["Tier6_RushSlash_AttackSpeedBonus"] = "Tier 6: [冲锋斩] 攻击速度加成 (%)",

                // ============================================
                // 锤树 - 34个键
                // ============================================

                // === Tier 0: 锤专家 (4) ===
                ["Tier0_MaceExpert_DamageBonus"] = "Tier 0: [锤专家] 伤害加成 (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: [锤专家] 眩晕概率 (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: [锤专家] 眩晕持续时间 (秒)",
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: [锤专家] 所需点数",

                // === Tier 1: 锤伤害强化 (2) ===
                ["Tier1_MaceExpert_DamageBonus"] = "Tier 1: [锤伤害强化] 伤害加成 (%)",
                ["Tier1_MaceExpert_RequiredPoints"] = "Tier 1: [锤伤害强化] 所需点数",

                // === Tier 2: 眩晕强化 (3) ===
                ["Tier2_StunBoost_StunChanceBonus"] = "Tier 2: [眩晕强化] 眩晕概率加成 (%)",
                ["Tier2_StunBoost_StunDurationBonus"] = "Tier 2: [眩晕强化] 眩晕持续时间加成 (秒)",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: [眩晕强化] 所需点数",

                // === Tier 3-1: 旋转打击 (3) ===
                ["Tier3_SpinStrike_DamageBonus"] = "Tier 3-1: [旋转打击] 伤害加成 (%)",
                ["Tier3_SpinStrike_Range"] = "Tier 3-1: [旋转打击] AOE范围 (米)",
                ["Tier3_Guard_RequiredPoints"] = "Tier 3-1: [旋转打击] 所需点数",

                // === Tier 3-2: 重击 (2) ===
                ["Tier3_HeavyStrike_DamageBonus"] = "Tier 3-2: [重击] 钝击加成",
                ["Tier3_HeavyStrike_RequiredPoints"] = "Tier 3-2: [重击] 所需点数",

                // === Tier 4: 推击 (2) ===
                ["Tier4_Push_KnockbackChance"] = "Tier 4: [推击] 击退概率 (%)",
                ["Tier4_Push_RequiredPoints"] = "Tier 4: [推击] 所需点数",

                // === Tier 5-1: 坦克 (3) ===
                ["Tier5_Tank_HealthBonus"] = "Tier 5-1: [坦克] 生命值加成 (%)",
                ["Tier5_Tank_DamageReduction"] = "Tier 5-1: [坦克] 受到伤害减少 (%)",
                ["Tier5_Tank_RequiredPoints"] = "Tier 5-1: [坦克] 所需点数",

                // === Tier 5-2: DPS强化 (2) ===
                ["Tier5_DPS_DamageBonus"] = "Tier 5-2: [DPS强化] 伤害加成 (%)",
                ["Tier5_DPS_RequiredPoints"] = "Tier 5-2: [DPS强化] 所需点数",

                // === Tier 6: 迅捷攻击 (2) ===
                ["Tier6_Sokgong_AttackSpeedBonus"] = "Tier 6: [迅捷攻击] 攻击速度加成 (%)",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: [迅捷攻击] 所需点数",

                // === Tier 7-1: 狂怒之锤 - H键主动 (6) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] = "Tier 7-1: [狂怒之锤] 第1-4击伤害倍率 (%)",
                ["Tier7_FuryHammer_FinalHitMultiplier"] = "Tier 7-1: [狂怒之锤] 第5击最终伤害倍率 (%)",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7-1: [狂怒之锤] 体力消耗",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7-1: [狂怒之锤] 冷却时间 (秒)",
                ["Tier7_FuryHammer_AoeRadius"] = "Tier 7-1: [狂怒之锤] AOE半径 (米)",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7-1: [狂怒之锤] 所需点数",

                // === Tier 7-2: 盾击冲锋 - G键主动 (4) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7-2: [盾击冲锋] 冷却时间 (秒)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7-2: [盾击冲锋] 体力消耗",
                ["Tier7_ShieldCharge_DamagePercent"] = "Tier 7-2: [盾击冲锋] 格挡力伤害比例 (%)",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7-2: [盾击冲锋] 所需点数",

                // ============================================
                // 长柄武器树 - 37个键
                // ============================================

                // === Tier 0: 长柄武器专家 (2) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] = "Tier 0: [长柄武器专家] 攻击范围加成 (%)",
                ["Tier0_PolearmExpert_RequiredPoints"] = "Tier 0: [长柄武器专家] 所需点数",

                // === Tier 1: 旋转斩 (2) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] = "Tier 1: [旋转斩] 旋转攻击伤害加成 (%)",
                ["Tier1_SpinWheel_RequiredPoints"] = "Tier 1: [旋转斩] 所需点数",

                // === Tier 2-1: 长柄武器强化 (2) ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] = "Tier 2-1: [长柄武器强化] 穿刺伤害加成 (固定值)",
                ["Tier2-1_PolearmBoost_RequiredPoints"] = "Tier 2-1: [长柄武器强化] 所需点数",

                // === Tier 2-2: 英雄打击 (2) ===
                ["Tier2-2_HeroStrike_KnockbackChance"] = "Tier 2-2: [英雄打击] 硬直概率 (%)",
                ["Tier2-2_HeroStrike_RequiredPoints"] = "Tier 2-2: [英雄打击] 所需点数",

                // === Tier 3: 宽幅斩 (3) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] = "Tier 3: [宽幅斩] 双重打击伤害加成 (%)",
                ["Tier3_AreaCombo_DoubleHitDuration"] = "Tier 3: [宽幅斩] 双重打击效果持续时间 (秒)",
                ["Tier3_AreaCombo_RequiredPoints"] = "Tier 3: [宽幅斩] 所需点数",

                // === Tier 4-1: 风暴斩 (2) ===
                ["Tier4-1_StormSlash_ExplosionBonus"] = "Tier 4-1: [风暴斩] 雷电伤害加成",
                ["Tier4-1_GroundWheel_RequiredPoints"] = "Tier 4-1: [风暴斩] 所需点数",

                // === Tier 4-2: 新月斩 (3) ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] = "Tier 4-2: [新月斩] 攻击范围加成 (%)",
                ["Tier4-2_MoonSlash_StaminaReduction"] = "Tier 4-2: [新月斩] 体力消耗减少 (%)",
                ["Tier4-2_MoonSlash_RequiredPoints"] = "Tier 4-2: [新月斩] 所需点数",

                // === Tier 4-3: 压制攻击 (2) ===
                ["Tier4-3_Suppress_DamageBonus"] = "Tier 4-3: [压制攻击] 伤害加成 (%)",
                ["Tier4-3_Suppress_RequiredPoints"] = "Tier 4-3: [压制攻击] 所需点数",

                // === Tier 5: 穿透冲锋 (9) ===
                ["Tier5_PierceCharge_DashDistance"] = "Tier 5: [穿透冲锋] 冲刺距离 (米)",
                ["Tier5_PierceCharge_FirstHitDamageBonus"] = "Tier 5: [穿透冲锋] 第1击伤害加成 (%)",
                ["Tier5_PierceCharge_AoeDamageBonus"] = "Tier 5: [穿透冲锋] AOE击退伤害加成 (%)",
                ["Tier5_PierceCharge_AoeAngle"] = "Tier 5: [穿透冲锋] AOE角度 (度)",
                ["Tier5_PierceCharge_AoeRadius"] = "Tier 5: [穿透冲锋] AOE半径 (米)",
                ["Tier5_PierceCharge_KnockbackDistance"] = "Tier 5: [穿透冲锋] 击退距离 (米)",
                ["Tier5_PierceCharge_StaminaCost"] = "Tier 5: [穿透冲锋] 体力消耗",
                ["Tier5_PierceCharge_Cooldown"] = "Tier 5: [穿透冲锋] 冷却时间 (秒)",
                ["Tier5_PierceCharge_RequiredPoints"] = "Tier 5: [穿透冲锋] 所需点数",

                // === Polearm Tree: 旋风 (7 keys) ===
                ["Tier6_Whirlwind_DamagePercent"]   = "Tier 6: [旋风] 伤害比例 (%)",
                ["Tier6_Whirlwind_StaminaPerSec"]   = "Tier 6: [旋风] 每次体力消耗",
                ["Tier6_Whirlwind_MoveSpeed"]       = "Tier 6: [旋风] 移动速度 (m/s)",
                ["Tier6_Whirlwind_AttackInterval"]  = "Tier 6: [旋风] 攻击间隔 (秒)",
                ["Tier6_Whirlwind_VfxInterval"]     = "Tier 6: [旋风] VFX间隔 (秒)",
                ["Tier6_Whirlwind_Cooldown"]        = "Tier 6: [旋风] 冷却时间 (秒)",
                ["Tier6_Whirlwind_RequiredPoints"]  = "Tier 6: [旋风] 所需点数",

                // ============================================
                // 弓手职业技能 - 25个键
                // ============================================
                ["Archer_MultiShot_ArrowCount"] = "Lv1 多重射击: 箭矢数量",
                ["Archer_MultiShot_ArrowConsumption"] = "Lv1 多重射击: 箭矢消耗",
                ["Archer_MultiShot_DamagePercent"] = "Lv1 多重射击: 每箭伤害 (%)",
                ["Archer_MultiShot_Cooldown"] = "Lv1 多重射击: 冷却时间 (秒)",
                ["Archer_MultiShot_Charges"] = "Lv1 多重射击: 额外射击次数",
                ["Archer_MultiShot_StaminaCost"] = "Lv1 多重射击: 体力消耗",
                ["Archer_JumpHeightBonus"] = "Lv1 被动: 跳跃高度加成 (%)",
                ["Archer_FallDamageReduction"] = "Lv1 被动: 坠落伤害减少 (%)",
                ["Archer_Lv2_BonusArrows"]   = "Lv2 多重射击: 额外箭矢数",
                ["Archer_Lv2_DamagePercent"] = "Lv2 多重射击: 每箭伤害 (%)",
                ["Archer_Lv3_BonusArrows"]   = "Lv3 多重射击: 额外箭矢数",
                ["Archer_Lv3_DamagePercent"] = "Lv3 多重射击: 每箭伤害 (%)",
                ["Archer_Lv4_BonusArrows"]   = "Lv4 多重射击: 额外箭矢数",
                ["Archer_Lv4_DamagePercent"] = "Lv4 多重射击: 每箭伤害 (%)",
                ["Archer_Lv5_BonusArrows"]   = "Lv5 多重射击: 额外箭矢数",
                ["Archer_Lv5_DamagePercent"] = "Lv5 多重射击: 每箭伤害 (%)",
                ["Archer_Lv5_BonusCharges"]  = "Lv5 多重射击: 额外射击次数",
                ["Archer_Lv2_JumpHeightBonus"]     = "Lv2 被动: 跳跃高度加成 (%)",
                ["Archer_Lv3_JumpHeightBonus"]     = "Lv3 被动: 跳跃高度加成 (%)",
                ["Archer_Lv4_JumpHeightBonus"]     = "Lv4 被动: 跳跃高度加成 (%)",
                ["Archer_Lv5_JumpHeightBonus"]     = "Lv5 被动: 跳跃高度加成 (%)",
                ["Archer_Lv3_FallDamageReduction"] = "Lv3 被动: 坠落伤害减少 (%)",
                ["Archer_Lv4_FallDamageReduction"] = "Lv4 被动: 坠落伤害减少 (%)",
                ["Archer_Lv5_FallDamageReduction"] = "Lv5 被动: 坠落伤害减少 (%)",
                ["Archer_ElementalResistPerLevel"] = "被动: 每级元素抗性 (%)",

                // ============================================
                // 法师职业技能 - 6个键
                // ============================================
                ["Mage_AOE_Range"] = "主动: 范围 (米)",
                ["Mage_AOE_Max_Targets"] = "主动: 最大目标数",
                ["Mage_Eitr_Cost"] = "主动: 以特尔消耗",
                ["Mage_Fire_Rain_Radius"] = "降落半径 (米)",
                ["Mage_Fire_Rain_Impact_Radius"] = "落地伤害范围 (米)",
                ["Mage_Fire_Rain_Projectile_Count"] = "每次爆发弹数 (个)",
                ["Mage_Damage_Multiplier"] = "主动: 伤害倍率 (%)",
                ["Mage_Cooldown"] = "主动: 冷却时间 (秒)",
                ["Mage_Elemental_Resistance"] = "被动: 元素抗性 (%)",
                ["Mage_Lv1_Damage_Multiplier"] = "Lv1 AOE伤害倍率 (%)",
                ["Mage_Lv1_Cooldown"] = "Lv1 冷却时间 (秒)",
                ["Mage_Lv1_Elemental_Resistance"] = "Lv1 元素抗性 (%)",
                ["Mage_Lv2_Damage_Multiplier"] = "Lv2 AOE伤害倍率 (%)",
                ["Mage_Lv2_Cooldown"] = "Lv2 冷却时间 (秒)",
                ["Mage_Lv2_Elemental_Resistance"] = "Lv2 元素抗性 (%)",
                ["Mage_Lv3_Damage_Multiplier"] = "Lv3 AOE伤害倍率 (%)",
                ["Mage_Lv3_Cooldown"] = "Lv3 冷却时间 (秒)",
                ["Mage_Lv3_Elemental_Resistance"] = "Lv3 元素抗性 (%)",
                ["Mage_Lv4_Damage_Multiplier"] = "Lv4 AOE伤害倍率 (%)",
                ["Mage_Lv4_Cooldown"] = "Lv4 冷却时间 (秒)",
                ["Mage_Lv4_Elemental_Resistance"] = "Lv4 元素抗性 (%)",
                ["Mage_Lv5_Damage_Multiplier"] = "Lv5 AOE伤害倍率 (%)",
                ["Mage_Lv5_Cooldown"] = "Lv5 冷却时间 (秒)",
                ["Mage_Lv5_Elemental_Resistance"] = "Lv5 元素抗性 (%)",
                ["Mage_Lv1_AOE_Max_Targets"] = "主动: 最大目标数 Lv1",
                ["Mage_Lv2_AOE_Max_Targets"] = "主动: 最大目标数 Lv2",
                ["Mage_Lv3_AOE_Max_Targets"] = "主动: 最大目标数 Lv3",
                ["Mage_Lv4_AOE_Max_Targets"] = "主动: 最大目标数 Lv4",
                ["Mage_Lv5_AOE_Max_Targets"] = "主动: 最大目标数 Lv5",

                // ============================================
                // 坦克职业技能 - 10个键
                // ============================================
                ["Tanker_Taunt_Cooldown"] = "战吼: 冷却时间 (秒)",
                ["Tanker_Taunt_StaminaCost"] = "战吼: 体力消耗",
                ["Tanker_Taunt_Range"] = "战吼: 嘲讽范围 (米)",
                ["Tanker_Taunt_Duration"] = "战吼: 嘲讽持续时间 (秒)",
                ["Tanker_Taunt_BossDuration"] = "战吼: Boss嘲讽持续时间 (秒)",
                ["Tanker_Taunt_DamageReduction"] = "战吼: 伤害减少 (%)",
                ["Tanker_Taunt_BuffDuration"] = "战吼: 效果持续时间 (秒)",
                ["Tanker_Taunt_EffectHeight"] = "战吼: 效果高度 (米)",
                ["Tanker_Taunt_EffectScale"] = "战吼: 效果比例",
                ["Tanker_Taunt_ReflectPercent"] = "战吼: 反射伤害比例 (%)",
                ["Tanker_Passive_DamageReduction"] = "被动: 伤害减少 (%)",

                // ============================================
                // 盗贼职业技能 - 10个键
                // ============================================
                ["Rogue_ShadowStrike_Cooldown"] = "暗影打击: 冷却时间 (秒)",
                ["Rogue_ShadowStrike_StaminaCost"] = "暗影打击: 体力消耗",
                ["Rogue_ShadowStrike_AttackBonus"] = "暗影打击: 攻击加成 (%)",
                ["Rogue_ShadowStrike_BuffDuration"] = "暗影打击: 效果持续时间 (秒)",
                ["Rogue_ShadowStrike_SmokeScale"] = "暗影打击: 烟雾比例",
                ["Rogue_ShadowStrike_AggroRange"] = "暗影打击: 仇恨范围 (米)",
                ["Rogue_ShadowStrike_StealthDuration"] = "暗影打击: 隐身持续时间 (秒)",
                ["Rogue_AttackSpeed_Bonus"] = "被动: 攻击速度加成 (%)",
                ["Rogue_Stamina_Reduction"] = "被动: 体力消耗减少 (%)",
                ["Rogue_Lv1_DodgeChance"] = "Lv1 被动: 回避率 (%)",
                ["Rogue_Lv2_DodgeChance"] = "Lv2 被动: 回避率 (%)",
                ["Rogue_Lv3_DodgeChance"] = "Lv3 被动: 回避率 (%)",
                ["Rogue_Lv4_DodgeChance"] = "Lv4 被动: 回避率 (%)",
                ["Rogue_Lv5_DodgeChance"] = "Lv5 被动: 回避率 (%)",

                // ============================================
                // 圣骑士职业技能 - 9个键
                // ============================================
                ["Paladin_Active_Cooldown"] = "神圣治愈: 冷却时间 (秒)",
                ["Paladin_Active_Range"] = "神圣治愈: 范围 (米)",
                ["Paladin_Active_EitrCost"] = "神圣治愈: 以特尔消耗",
                ["Paladin_Active_StaminaCost"] = "神圣治愈: 体力消耗",
                ["Paladin_Active_SelfHealPercent"] = "神圣治愈: 自身治疗比率 (%)",
                ["Paladin_Active_AllyHealPercentOverTime"] = "神圣治愈: 友方持续治疗比率 (%)",
                ["Paladin_Active_Duration"] = "神圣治愈: 持续时间 (秒)",
                ["Paladin_Active_Interval"] = "神圣治愈: 间隔时间 (秒)",
                ["Paladin_Passive_ElementalResistanceReduction"] = "被动: 抗性加成 (%)",
                ["Paladin_Lv2_SelfHealPercent"] = "Lv2: 自身治疗率 (%)",
                ["Paladin_Lv2_AllyHealPercent"] = "Lv2: 盟友治疗率 (%/次)",
                ["Paladin_Lv3_SelfHealPercent"] = "Lv3: 自身治疗率 (%)",
                ["Paladin_Lv3_AllyHealPercent"] = "Lv3: 盟友治疗率 (%/次)",
                ["Paladin_Lv3_HealRange"] = "Lv3: 治疗范围 (m)",
                ["Paladin_Lv4_SelfHealPercent"] = "Lv4: 自身治疗率 (%)",
                ["Paladin_Lv4_AllyHealPercent"] = "Lv4: 盟友治疗率 (%/次)",
                ["Paladin_Lv4_HealRange"] = "Lv4: 治疗范围 (m)",
                ["Paladin_Lv5_SelfHealPercent"] = "Lv5: 自身治疗率 (%)",
                ["Paladin_Lv5_AllyHealPercent"] = "Lv5: 盟友治疗率 (%/次)",
                ["Paladin_Lv5_HealRange"] = "Lv5: 治疗范围 (m)",
                ["Paladin_Lv5_Cooldown"] = "Lv5: 冷却时间 (秒)",
                ["Paladin_Lv2_ResistanceReduction"] = "Lv2: 抗性减少 (%)",
                ["Paladin_Lv3_ResistanceReduction"] = "Lv3: 抗性减少 (%)",
                ["Paladin_Lv4_ResistanceReduction"] = "Lv4: 抗性减少 (%)",
                ["Paladin_Lv5_ResistanceReduction"] = "Lv5: 抗性减少 (%)",
                ["Paladin_Lv2_StaminaBonus"] = "Lv2: 体力加成",
                ["Paladin_Lv3_StaminaBonus"] = "Lv3: 体力加成",
                ["Paladin_Lv4_StaminaBonus"] = "Lv4: 体力加成",
                ["Paladin_Lv5_StaminaBonus"] = "Lv5: 体力加成",

                // ============================================
                // 狂战士职业技能 - 10个键
                // ============================================
                ["Beserker_Active_Cooldown"] = "狂战士之怒: 冷却时间 (秒)",
                ["Beserker_Active_StaminaCost"] = "狂战士之怒: 体力消耗",
                ["Beserker_Active_Duration"] = "狂战士之怒: 持续时间 (秒)",
                ["Beserker_Active_DamagePerHealthPercent"] = "狂战士之怒: 每1%HP的伤害加成 (%)",
                ["Beserker_Active_MaxDamageBonus"] = "狂战士之怒: 最大伤害加成 (%)",
                ["Beserker_Active_HealthThreshold"] = "狂战士之怒: HP阈值 (%)",
                ["Berserker_Passive_HealthThreshold"] = "死亡蔑视: HP阈值 (%)",
                ["Berserker_Passive_InvincibilityDuration"] = "死亡蔑视: 无敌持续时间 (秒)",
                ["Berserker_Passive_Cooldown"] = "死亡蔑视: 冷却时间 (秒)",
                ["Berserker_Passive_HealthBonus"] = "被动: 最大HP加成 (%)",
                ["Berserker_Lv2_CooldownReduction"] = "Lv2: 狂怒冷却减少 (秒)",
                ["Berserker_Lv3_RageDamageReduction"] = "Lv3: 狂怒中减伤 (%)",
                ["Berserker_Lv4_LowHpAttackBonus"] = "Lv4: 低血量攻击加成 (%)",
                ["Berserker_Lv4_LowHpAttackThreshold"] = "Lv4: 低血量阈值 (%)",
                ["Berserker_Lv5_PassiveCooldownReduction"] = "Lv5: 被动冷却减少 (秒)",
                ["Berserker_Lv5_InvincibilityBonus"] = "Lv5: 额外无敌时间 (秒)",

                // ============================================
                // 剑树 - 路径命中补充
                // ============================================
                ["Tier6_RushSlash_PathWidth"] = "Tier 6: [冲锋斩] 路径命中宽度 (米)",

                // ============================================
                // 制作专家 (Producer) 职业技能
                // ============================================
                ["Producer_Buff_Cooldown"] = "制作专家: 工匠祝福冷却时间 (秒)",
                ["Producer_Buff_Duration"] = "制作专家: 工匠祝福持续时间 (秒)",
                ["Producer_Buff_Range"] = "制作专家: 工匠祝福范围 (米)",
                ["Producer_Buff_AttackBonus"] = "制作专家: 祝福攻击力加成 (%)",
                ["Producer_Buff_MaxHealthBonus"] = "制作专家: 祝福最大HP加成 (%)",
                ["Producer_Buff_StaminaCost"] = "制作专家: 祝福体力消耗",
                ["Producer_FarmGrid_Lv1"] = "制作专家: 农耕格数量 Lv1",
                ["Producer_FarmGrid_Lv2"] = "制作专家: 农耕格数量 Lv2",
                ["Producer_FarmGrid_Lv3"] = "制作专家: 农耕格数量 Lv3",
                ["Producer_FarmGrid_Lv4"] = "制作专家: 农耕格数量 Lv4",
                ["Producer_FarmGrid_Lv5"] = "制作专家: 农耕格数量 Lv5",
                ["Producer_Durability_Lv2"] = "制作专家: 耐久度加成 Lv2 (%)",
                ["Producer_Durability_Lv3"] = "制作专家: 耐久度加成 Lv3 (%)",
                ["Producer_Durability_Lv4"] = "制作专家: 耐久度加成 Lv4 (%)",
                ["Producer_Durability_Lv5"] = "制作专家: 耐久度加成 Lv5 (%)",
                ["Producer_MaterialReduction_Lv2"] = "制作专家: 材料减少 Lv2 (%)",
                ["Producer_MaterialReduction_Lv3"] = "制作专家: 材料减少 Lv3 (%)",
                ["Producer_MaterialReduction_Lv4"] = "制作专家: 材料减少 Lv4 (%)",
                ["Producer_MaterialReduction_Lv5"] = "制作专家: 材料减少 Lv5 (%)",
                ["Producer_EnchantChance_Lv1"] = "制作专家: 附魔概率 Lv1 (%)",
                ["Producer_EnchantChance_Lv2"] = "制作专家: 附魔概率 Lv2 (%)",
                ["Producer_EnchantChance_Lv3"] = "制作专家: 附魔概率 Lv3 (%)",
                ["Producer_EnchantChance_Lv4"] = "制作专家: 附魔概率 Lv4 (%)",
                ["Producer_EnchantChance_Lv5"] = "制作专家: 附魔概率 Lv5 (%)",
            };
        }
    }
}

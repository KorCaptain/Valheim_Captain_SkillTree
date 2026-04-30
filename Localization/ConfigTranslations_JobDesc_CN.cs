using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJobDescriptions_CN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // 弓手职业技能 (Archer Job)
                // ========================================

                // === 弓手：主动技能《多重射击》（6个键）===
                ["Archer_MultiShot_ArrowCount"] =
                "【箭矢数量】\n" +
                "每次多重射击发射的箭矢数量。\n" +
                "箭矢越多 = 范围伤害越高。\n" +
                "推荐：4-7",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【箭矢消耗】\n" +
                "每次多重射击消耗的箭矢数量。\n" +
                "低消耗可实现高效攻击。\n" +
                "推荐：1-2",

                ["Archer_MultiShot_DamagePercent"] =
                "【每箭伤害(%)】\n" +
                "每支箭矢的伤害百分比。\n" +
                "弓基础攻击的百分比。\n" +
                "推荐：40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "多重射击再次使用前的等待时间。\n" +
                "数值越低 = 可越频繁使用。\n" +
                "推荐：25-40秒",

                ["Archer_MultiShot_Charges"] =
                "【充能数】\n" +
                "连续使用多重射击的次数。\n" +
                "多次射击以集中伤害。\n" +
                "推荐：2-4",

                ["Archer_MultiShot_StaminaCost"] =
                "【体力消耗】\n" +
                "使用多重射击消耗的体力。\n" +
                "体力管理至关重要。\n" +
                "推荐：20-35",

                ["Archer_MultiShot_SummonRadius"] =
                "【召唤扇形半径 (m)】\n" +
                "召唤箭矢在玩家头顶排列的弧形半径。\n" +
                "推荐：1.0~2.0m",

                ["Archer_MultiShot_SummonHeight"] =
                "【召唤高度 (m)】\n" +
                "召唤箭矢悬浮在玩家头顶的高度。\n" +
                "推荐：2.0~3.0m",

                ["Archer_MultiShot_HomingSpeed"] =
                "【追踪速度】\n" +
                "召唤箭矢飞向敌人头部的速度。\n" +
                "推荐：20~35",

                ["Archer_MultiShot_HoverTime"] =
                "【悬停时间 (秒)】\n" +
                "召唤箭矢在消失前的最大等待时间。\n" +
                "推荐：25~40秒",

                // === 弓手：被动技能（2个键）===
                ["Archer_JumpHeightBonus"] =
                "【跳跃高度加成(%)】\n" +
                "提升基础跳跃高度。\n" +
                "更易到达高处位置。\n" +
                "推荐：15-25%",

                ["Archer_FallDamageReduction"] =
                "【坠落伤害减少(%)】\n" +
                "减少从高处坠落时受到的伤害。\n" +
                "提升弓手的机动性。\n" +
                "推荐：40-60%",

                // === 弓手：等级奖励（9个键）===
                ["Archer_Lv2_BonusArrows"] =
                "【Lv.2：额外箭矢】\n" +
                "升至Lv.2时获得的额外箭矢。\n" +
                "累加于基础箭矢数量。\n" +
                "推荐：1",

                ["Archer_Lv2_DamagePercent"] =
                "【Lv.2：每箭伤害(%)】\n" +
                "Lv.2时每箭的伤害倍率。\n" +
                "以弓+箭总伤害的%计算。\n" +
                "推荐：50-60%",

                ["Archer_Lv3_BonusArrows"] =
                "【Lv.3：额外箭矢】\n" +
                "升至Lv.3时获得的额外箭矢。\n" +
                "累加于基础箭矢数量。\n" +
                "推荐：2",

                ["Archer_Lv3_DamagePercent"] =
                "【Lv.3：每箭伤害(%)】\n" +
                "Lv.3时每箭的伤害倍率。\n" +
                "以弓+箭总伤害的%计算。\n" +
                "推荐：55-65%",

                ["Archer_Lv4_BonusArrows"] =
                "【Lv.4：额外箭矢】\n" +
                "升至Lv.4时获得的额外箭矢。\n" +
                "累加于基础箭矢数量。\n" +
                "推荐：3",

                ["Archer_Lv4_DamagePercent"] =
                "【Lv.4：每箭伤害(%)】\n" +
                "Lv.4时每箭的伤害倍率。\n" +
                "以弓+箭总伤害的%计算。\n" +
                "推荐：60-70%",

                ["Archer_Lv5_BonusArrows"] =
                "【Lv.5：额外箭矢】\n" +
                "升至Lv.5时获得的额外箭矢。\n" +
                "累加于基础箭矢数量。\n" +
                "推荐：3",

                ["Archer_Lv5_DamagePercent"] =
                "【Lv.5：每箭伤害(%)】\n" +
                "Lv.5时每箭的伤害倍率。\n" +
                "以弓+箭总伤害的%计算。\n" +
                "推荐：60-70%",

                ["Archer_Lv5_BonusCharges"] =
                "【Lv.5：额外充能数】\n" +
                "Lv.5时的额外多重射击充能数。\n" +
                "累加于基础充能数量。\n" +
                "推荐：1",

                // === 弓手：被动等级奖励（8个键）===
                ["Archer_Lv2_JumpHeightBonus"] =
                "【Lv.2 被动：跳跃高度加成(%)】\n" +
                "Lv.2时的额外跳跃高度加成。\n" +
                "累加于Lv.1基础值。\n" +
                "推荐：10%",

                ["Archer_Lv3_JumpHeightBonus"] =
                "【Lv.3 被动：跳跃高度加成(%)】\n" +
                "Lv.3时的额外跳跃高度加成。\n" +
                "推荐：20%",

                ["Archer_Lv4_JumpHeightBonus"] =
                "【Lv.4 被动：跳跃高度加成(%)】\n" +
                "Lv.4时的额外跳跃高度加成。\n" +
                "推荐：20%",

                ["Archer_Lv5_JumpHeightBonus"] =
                "【Lv.5 被动：跳跃高度加成(%)】\n" +
                "Lv.5时的额外跳跃高度加成。\n" +
                "推荐：20%",

                ["Archer_Lv3_FallDamageReduction"] =
                "【Lv.3 被动：坠落伤害减少(%)】\n" +
                "Lv.3时的额外坠落伤害减少。\n" +
                "累加于Lv.1基础值。\n" +
                "推荐：10%",

                ["Archer_Lv4_FallDamageReduction"] =
                "【Lv.4 被动：坠落伤害减少(%)】\n" +
                "Lv.4时的额外坠落伤害减少。\n" +
                "推荐：20%",

                ["Archer_Lv5_FallDamageReduction"] =
                "【Lv.5 被动：坠落伤害减少(%)】\n" +
                "Lv.5时的额外坠落伤害减少。\n" +
                "推荐：35%",

                ["Archer_ElementalResistPerLevel"] =
                "【被动：每级元素抗性(%)】\n" +
                "每个弓手等级获得的元素抗性。\n" +
                "毒素(Lv2+)、冰寒(Lv3+)、火焰(Lv4+)、闪电(Lv5)。\n" +
                "推荐：10%",

                // ========================================
                // 法师职业技能 (Mage Job)
                // ========================================

                // === 法师：主动技能《AOE》（5个键）===
                ["Mage_AOE_Range"] =
                "【AOE范围（米）】\n" +
                "魔法范围攻击的半径。\n" +
                "范围越大可命中更多敌人。\n" +
                "推荐：10-15米",

                ["Mage_Eitr_Cost"] =
                "【以特尔消耗】\n" +
                "使用技能消耗的以特尔量。\n" +
                "魔法资源管理至关重要。\n" +
                "推荐：30-45",

                ["Mage_Damage_Multiplier"] =
                "【伤害倍率(%)】\n" +
                "魔法范围攻击的伤害倍率。\n" +
                "强力爆炸魔法消灭敌人。\n" +
                "推荐：250-350%",

                ["Mage_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前的等待时间。\n" +
                "强力效果导致冷却时间较长。\n" +
                "推荐：150-200秒",

                // === 法师：被动技能（1个键）===
                ["Mage_Elemental_Resistance"] =
                "【元素抗性(%)】\n" +
                "提升对火焰、冰寒、闪电、毒素和灵魂的抗性。\n" +
                "不含物理伤害 — 仅减少魔法伤害。\n" +
                "推荐：12-20%",

                // === 狂战士：被动生命值加成 ===
                ["berserker_passive_health_bonus"] =
                "【最大生命值加成(%)】\n" +
                "狂战士被动：提升最大生命值。\n" +
                "以总生命值（基础+MMO+所有加成）的%计算。\n" +
                "治疗正常生效（包含在m_baseHP中）。\n" +
                "推荐：100%",

                // === 狂战士 Lv2~5 被动 Config ===
                ["Berserker_Lv2_CooldownReduction"] =
                "【狂战士 Lv2：狂怒冷却缩减(秒)】\n" +
                "Lv2时，狂怒技能冷却时间减少此数值。\n" +
                "推荐：5秒",

                ["Berserker_Lv3_RageDamageReduction"] =
                "【狂战士 Lv3：狂怒中受到伤害减免(%)】\n" +
                "Lv3时，狂怒状态下受到的伤害减少此比例。\n" +
                "推荐：15%",

                ["Berserker_Lv4_LowHpAttackBonus"] =
                "【狂战士 Lv4：低生命值攻击加成(%)】\n" +
                "Lv4时，生命值低于阈值时攻击力增加。\n" +
                "推荐：15%",

                ["Berserker_Lv4_LowHpAttackThreshold"] =
                "【狂战士 Lv4：低生命值触发阈值(%)】\n" +
                "低于此生命值%时，Lv4攻击加成激活。\n" +
                "推荐：50%",

                ["Berserker_Lv5_PassiveCooldownReduction"] =
                "【狂战士 Lv5：不屈之死冷却缩减(秒)】\n" +
                "Lv5时，被动技能冷却时间减少此数值。\n" +
                "推荐：120秒",

                ["Berserker_Lv5_InvincibilityBonus"] =
                "【狂战士 Lv5：额外无敌时间(秒)】\n" +
                "Lv5时，不屈之死触发时无敌时间延长。\n" +
                "推荐：2秒",

                // ========================================
                // 坦克职业技能 (Tanker Job)
                // ========================================

                // === 坦克：主动技能《战吼》（9个键）===
                ["Tanker_Taunt_Cooldown"] =
                "【战吼冷却时间（秒）】\n" +
                "技能再次使用前的等待时间。\n" +
                "推荐：45-90秒",

                ["Tanker_Taunt_StaminaCost"] =
                "【战吼体力消耗】\n" +
                "激活战吼消耗的体力。\n" +
                "推荐：20-30",

                ["Tanker_Taunt_Range"] =
                "【战吼范围（米）】\n" +
                "嘲讽敌人的半径范围。\n" +
                "推荐：10-15米",

                ["Tanker_Taunt_Duration"] =
                "【普通怪物嘲讽持续时间（秒）】\n" +
                "对普通怪物的嘲讽生效时间。\n" +
                "推荐：4-8秒",

                ["Tanker_Taunt_BossDuration"] =
                "【Boss嘲讽持续时间（秒）】\n" +
                "对Boss的嘲讽生效时间。\n" +
                "Boss抗性更强 — 效果持续更短。\n" +
                "推荐：1-3秒",

                ["Tanker_Taunt_DamageReduction"] =
                "【受到伤害减少(%)】\n" +
                "战吼增益激活期间的伤害减少量。\n" +
                "推荐：15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【伤害减少增益持续时间（秒）】\n" +
                "激活后伤害减少增益的生效时间。\n" +
                "推荐：4-8秒",

                ["Tanker_Taunt_ReflectPercent"] =
                "【嘲讽反射伤害 (%)】\n" +
                "战吼增益期间将受到的伤害反射给攻击者。\n" +
                "在增益持续时间内有效。\n" +
                "推荐：5-20%",

                ["Tanker_Taunt_EffectHeight"] =
                "【嘲讽标志高度（米）】\n" +
                "嘲讽标志显示在怪物上方的高度。\n" +
                "推荐：1.5-2.5米",

                ["Tanker_Taunt_EffectScale"] =
                "【嘲讽标志缩放】\n" +
                "嘲讽视觉效果的大小倍率。\n" +
                "推荐：0.2-0.5",

                // === 坦克：被动技能（1个键）===
                ["Tanker_Passive_DamageReduction"] =
                "【坦克被动伤害减少(%)】\n" +
                "坦克被动：持续减少受到的伤害。\n" +
                "推荐：10-20%",

                // === Lv1 ===
                ["Tanker_ReflectDuration_Lv1"] =
                "【坦克反射持续时间Lv1 (秒)】\n" +
                "默认：10秒",

                ["Tanker_Hp_Bonus_Lv1"] =
                "【坦克 Lv1 HP加成 (%)】\n" +
                "达到坦克Lv1时最大HP按百分比增加。\n" +
                "默认：25",

                // === Lv2 ===
                ["Tanker_Hp_Bonus_Lv2"] =
                "【坦克 Lv2 HP加成 (%)】\n" +
                "达到坦克Lv2时最大HP按百分比增加。\n" +
                "默认：30",

                ["Tanker_Lv2_BlockPower"] =
                "【坦克 Lv2 格挡防御力】\n" +
                "坦克Lv2被动格挡防御力。\n" +
                "默认：5",

                ["Tanker_ReflectDuration_Lv2"] =
                "【坦克反射持续时间Lv2 (秒)】\n" +
                "默认：12秒",

                // === Lv3 ===
                ["Tanker_Hp_Bonus_Lv3"] =
                "【坦克 Lv3 HP加成 (%)】\n" +
                "达到坦克Lv3时最大HP按百分比增加。\n" +
                "默认：35",

                ["Tanker_Lv3_BlockPower"] =
                "【坦克 Lv3 格挡防御力】\n" +
                "坦克Lv3被动格挡防御力。\n" +
                "默认：10",

                ["Tanker_ReflectDuration_Lv3"] =
                "【坦克反射持续时间Lv3 (秒)】\n" +
                "默认：14秒",

                // === Lv4 ===
                ["Tanker_Hp_Bonus_Lv4"] =
                "【坦克 Lv4 HP加成 (%)】\n" +
                "达到坦克Lv4时最大HP按百分比增加。\n" +
                "默认：40",

                ["Tanker_Lv4_BlockPower"] =
                "【坦克 Lv4 格挡防御力】\n" +
                "坦克Lv4被动格挡防御力。\n" +
                "默认：15",

                ["Tanker_ReflectDuration_Lv4"] =
                "【坦克反射持续时间Lv4 (秒)】\n" +
                "默认：16秒",

                // === Lv5 ===
                ["Tanker_Hp_Bonus_Lv5"] =
                "【坦克 Lv5 HP加成 (%)】\n" +
                "达到坦克Lv5时最大HP按百分比增加。\n" +
                "默认：50",

                ["Tanker_Lv5_BlockPower"] =
                "【坦克 Lv5 格挡防御力】\n" +
                "坦克Lv5被动格挡防御力。\n" +
                "默认：20",

                ["Tanker_ReflectDuration_Lv5"] =
                "【坦克反射持续时间Lv5 (秒)】\n" +
                "默认：20秒",

                // ========================================
                // 盗贼职业技能 (Rogue Job)
                // ========================================

                // === 盗贼：主动技能《暗影打击》（7个键）===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【暗影打击冷却时间（秒）】\n" +
                "暗影打击再次使用前的等待时间。\n" +
                "推荐：20-40秒",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【暗影打击体力消耗】\n" +
                "激活暗影打击消耗的体力。\n" +
                "推荐：20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【暗影打击攻击加成(%)】\n" +
                "激活后增益持续期间的攻击提升量。\n" +
                "推荐：25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【攻击增益持续时间（秒）】\n" +
                "攻击提升增益的生效时间。\n" +
                "推荐：6-12秒",

                ["Rogue_ShadowStrike_SmokeScale"] =
                "【烟雾效果缩放】\n" +
                "烟雾VFX的大小倍率。\n" +
                "推荐：1.5-3.0",

                ["Rogue_ShadowStrike_AggroRange"] =
                "【仇恨清除范围（米）】\n" +
                "清除此半径内所有敌人的仇恨。\n" +
                "推荐：10-20米",

                ["Rogue_ShadowStrike_StealthDuration"] =
                "【盗贼潜行持续时间（秒）】\n" +
                "潜行模式的生效时间。\n" +
                "推荐：5-10秒",

                // === 盗贼：被动技能（3个键）===
                ["Rogue_AttackSpeed_Bonus"] =
                "【攻击速度加成(%)】\n" +
                "盗贼被动：持续提升攻击速度。\n" +
                "推荐：8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【攻击体力消耗减少(%)】\n" +
                "盗贼被动：减少攻击消耗的体力。\n" +
                "推荐：10-20%",

                ["Rogue_Lv1_DodgeChance"] =
                "【Lv1 回避率(%)】\n" +
                "盗贼被动：提升命中回避率，与技能树合计叠加。\n" +
                "推荐：3-6%",
                ["Rogue_Lv2_DodgeChance"] = "【Lv2 回避率(%)】\n推荐：5-8%",
                ["Rogue_Lv3_DodgeChance"] = "【Lv3 回避率(%)】\n推荐：7-10%",
                ["Rogue_Lv4_DodgeChance"] = "【Lv4 回避率(%)】\n推荐：9-12%",
                ["Rogue_Lv5_DodgeChance"] = "【Lv5 回避率(%)】\n推荐：11-15%",

                // ========================================
                // 圣骑士职业技能 (Paladin Job)
                // ========================================

                // === 圣骑士：主动技能《神圣之光》（8个键）===
                ["Paladin_Active_Cooldown"] =
                "【神圣之光冷却时间（秒）】\n" +
                "技能再次使用前的等待时间。\n" +
                "推荐：20-45秒",

                ["Paladin_Active_Range"] =
                "【神圣之光范围（米）】\n" +
                "治疗队友的半径范围。\n" +
                "推荐：4-8米",

                ["Paladin_Active_EitrCost"] =
                "【神圣之光以特尔消耗】\n" +
                "激活神圣之光消耗的以特尔。\n" +
                "推荐：8-15",

                ["Paladin_Active_StaminaCost"] =
                "【神圣之光体力消耗】\n" +
                "激活神圣之光消耗的体力。\n" +
                "推荐：8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【自身治疗百分比（最大生命值%）】\n" +
                "激活时恢复的自身生命值百分比。\n" +
                "推荐：10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【队友持续治疗（最大生命值%/秒）】\n" +
                "每秒为每位队友恢复的生命值百分比。\n" +
                "推荐：1-3%",

                ["Paladin_Active_Duration"] =
                "【治疗持续时间（秒）】\n" +
                "队友持续治疗效果的总持续时间。\n" +
                "推荐：8-15秒",

                ["Paladin_Active_Interval"] =
                "【治疗间隔（秒）】\n" +
                "治疗效果的触发间隔时间。\n" +
                "推荐：1秒",

                // === 圣骑士：被动技能（1个键）===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【物理与元素抗性加成(%)】\n" +
                "圣骑士被动：提升对物理和元素伤害的抗性。\n" +
                "推荐：5-12%",

                // === 圣骑士 Lv2-5 ===
                ["Paladin_Lv2_SelfHealPercent"] = "【Lv2 自身治疗率(%)】\n推荐：15-20%",
                ["Paladin_Lv2_AllyHealPercent"] = "【Lv2 盟友治疗率(%/次)】\n推荐：2-3%",
                ["Paladin_Lv3_SelfHealPercent"] = "【Lv3 自身治疗率(%)】\n推荐：17-22%",
                ["Paladin_Lv3_AllyHealPercent"] = "【Lv3 盟友治疗率(%/次)】\n推荐：2.5-3.5%",
                ["Paladin_Lv3_HealRange"] = "【Lv3 治疗范围(m)】\n推荐：5-7m",
                ["Paladin_Lv4_SelfHealPercent"] = "【Lv4 自身治疗率(%)】\n推荐：19-24%",
                ["Paladin_Lv4_AllyHealPercent"] = "【Lv4 盟友治疗率(%/次)】\n推荐：3-4%",
                ["Paladin_Lv4_HealRange"] = "【Lv4 治疗范围(m)】\n推荐：6-8m",
                ["Paladin_Lv5_SelfHealPercent"] = "【Lv5 自身治疗率(%)】\n推荐：22-28%",
                ["Paladin_Lv5_AllyHealPercent"] = "【Lv5 盟友治疗率(%/次)】\n推荐：3.5-5%",
                ["Paladin_Lv5_HealRange"] = "【Lv5 治疗范围(m)】\n推荐：7-10m",
                ["Paladin_Lv5_Cooldown"] = "【Lv5 冷却时间(秒)】\n推荐：20-30秒",
                ["Paladin_Lv2_ResistanceReduction"] = "【Lv2 抵抗减少(%)】\n推荐：6-10%",
                ["Paladin_Lv3_ResistanceReduction"] = "【Lv3 抵抗减少(%)】\n推荐：8-12%",
                ["Paladin_Lv4_ResistanceReduction"] = "【Lv4 抵抗减少(%)】\n推荐：10-14%",
                ["Paladin_Lv5_ResistanceReduction"] = "【Lv5 抵抗减少(%)】\n推荐：12-18%",
                ["Paladin_Lv2_StaminaBonus"] = "【Lv2 最大耐力加成】\n推荐：8-15",
                ["Paladin_Lv3_StaminaBonus"] = "【Lv3 最大耐力加成】\n推荐：12-20",
                ["Paladin_Lv4_StaminaBonus"] = "【Lv4 最大耐力加成】\n推荐：15-25",
                ["Paladin_Lv5_StaminaBonus"] = "【Lv5 最大耐力加成】\n推荐：20-30",

                // ========================================
                // 狂战士职业技能 (Berserker Job)
                // ========================================

                // === 狂战士：主动技能《狂战怒气》（6个键，保留Beserker拼写）===
                ["Beserker_Active_Cooldown"] =
                "【狂战怒气冷却时间（秒）】\n" +
                "狂战怒气再次使用前的等待时间。\n" +
                "推荐：30-60秒",

                ["Beserker_Active_StaminaCost"] =
                "【狂战怒气体力消耗】\n" +
                "激活狂战怒气消耗的体力。\n" +
                "推荐：15-25",

                ["Beserker_Active_Duration"] =
                "【狂战怒气持续时间（秒）】\n" +
                "狂战怒气增益的生效时间。\n" +
                "推荐：15-25秒",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【每损失1%生命值的伤害加成(%)】\n" +
                "生命值越低，伤害越高。\n" +
                "损失生命值% × 此值 = 伤害加成\n" +
                "推荐：1.5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【最大伤害加成(%)】\n" +
                "生命值关联伤害加成的最大上限。\n" +
                "推荐：150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【激活生命值阈值(%)】\n" +
                "生命值低于此百分比时激活生命值关联伤害加成。\n" +
                "设为100%可持续激活。\n" +
                "推荐：50-100%",

                // === 狂战士：被动技能《死亡挑战》（3个键，保留Beserker拼写）===
                ["Berserker_Passive_HealthThreshold"] =
                "【被动激活生命值阈值(%)】\n" +
                "生命值低于此百分比时激活无敌状态。\n" +
                "推荐：8-15%",

                ["Berserker_Passive_InvincibilityDuration"] =
                "【无敌持续时间（秒）】\n" +
                "被动激活时无敌状态的持续时间。\n" +
                "推荐：5-10秒",

                ["Berserker_Passive_Cooldown"] =
                "【被动冷却时间（秒）】\n" +
                "下次被动无敌激活前的等待时间。\n" +
                "默认：540秒（9分钟）\n" +
                "推荐：120-300秒",

                // === 狂战士：被动生命值加成 ===
                ["Berserker_Passive_HealthBonus"] =
                "【最大生命值加成(%)】\n" +
                "狂战士被动：提升最大生命值。\n" +
                "推荐：100%",

                // === 制作专家 (Producer) 职业技能 ===
                // ========================================
                ["Producer_Buff_Cooldown"] =
                "【工匠祝福：冷却时间（秒）】\n" +
                "制作专家Buff激活之间的冷却时间。\n" +
                "默认：180秒",

                ["Producer_Buff_Duration"] =
                "【工匠祝福：持续时间（秒）】\n" +
                "队友攻击力/HP增益的持续时间。\n" +
                "默认：120秒",

                ["Producer_Buff_Range"] =
                "【工匠祝福：范围（米）】\n" +
                "队友在此范围内获得Buff。\n" +
                "默认：15米",

                ["Producer_Buff_AttackBonus"] =
                "【Buff攻击力加成(%)】\n" +
                "给予Buff队友的攻击力加成。\n" +
                "默认：15%",

                ["Producer_Buff_MaxHealthBonus"] =
                "【Buff最大HP加成(%)】\n" +
                "给予Buff队友的最大HP加成。\n" +
                "默认：15%",

                ["Producer_Buff_StaminaCost"] =
                "【Buff体力消耗】\n" +
                "激活Buff时消耗的体力值。\n" +
                "默认：20",

                // === Producer Lv1 ===
                ["Producer_FarmGrid_Lv1"] = "【农耕格大小 Lv1】\nLv1时农耕格中的额外格子数。\n默认：2",
                ["Producer_EnchantChance_Lv1"] = "【附魔概率 Lv1 (%)】\nLv1时对制作物品附魔的概率。\n默认：0%",

                // === Producer Lv2 ===
                ["Producer_FarmGrid_Lv2"] = "【农耕格大小 Lv2】\nLv2时农耕格中的额外格子数。\n默认：2",
                ["Producer_Durability_Lv2"] = "【制作品耐久度加成 Lv2 (%)】\nLv2时制作物品的耐久度加成。\n默认：10%",
                ["Producer_MaterialReduction_Lv2"] = "【材料消耗减少 Lv2 (%)】\nLv2时每次制作节省的材料。\n默认：10%",
                ["Producer_EnchantChance_Lv2"] = "【附魔概率 Lv2 (%)】\nLv2时对制作物品附魔的概率。\n默认：0%",

                // === Producer Lv3 ===
                ["Producer_FarmGrid_Lv3"] = "【农耕格大小 Lv3】\nLv3时农耕格中的额外格子数。\n默认：4",
                ["Producer_Durability_Lv3"] = "【制作品耐久度加成 Lv3 (%)】\nLv3时制作物品的耐久度加成。\n默认：15%",
                ["Producer_MaterialReduction_Lv3"] = "【材料消耗减少 Lv3 (%)】\nLv3时每次制作节省的材料。\n默认：15%",
                ["Producer_EnchantChance_Lv3"] = "【附魔概率 Lv3 (%)】\nLv3时对制作物品附魔的概率。\n默认：25%",

                // === Producer Lv4 ===
                ["Producer_FarmGrid_Lv4"] = "【农耕格大小 Lv4】\nLv4时农耕格中的额外格子数。\n默认：6",
                ["Producer_Durability_Lv4"] = "【制作品耐久度加成 Lv4 (%)】\nLv4时制作物品的耐久度加成。\n默认：20%",
                ["Producer_MaterialReduction_Lv4"] = "【材料消耗减少 Lv4 (%)】\nLv4时每次制作节省的材料。\n默认：20%",
                ["Producer_EnchantChance_Lv4"] = "【附魔概率 Lv4 (%)】\nLv4时对制作物品附魔的概率。\n默认：30%",

                // === Producer Lv5 ===
                ["Producer_FarmGrid_Lv5"] = "【农耕格大小 Lv5】\nLv5时农耕格中的额外格子数。\n默认：8",
                ["Producer_Durability_Lv5"] = "【制作品耐久度加成 Lv5 (%)】\nLv5时制作物品的耐久度加成。\n默认：30%",
                ["Producer_MaterialReduction_Lv5"] = "【材料消耗减少 Lv5 (%)】\nLv5时每次制作节省的材料。\n默认：30%",
                ["Producer_EnchantChance_Lv5"] = "【附魔概率 Lv5 (%)】\nLv5时对制作物品附魔的概率。\n默认：35%",

                ["Job_Lv1_Cost"] = "【职业Lv1金币消耗】\n所有职业升级到Lv1时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：1000",
                ["Job_Lv2_Cost"] = "【职业Lv2金币消耗】\n所有职业升级到Lv2时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：2000",
                ["Job_Lv3_Cost"] = "【职业Lv3金币消耗】\n所有职业升级到Lv3时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：3000",
                ["Job_Lv4_Cost"] = "【职业Lv4金币消耗】\n所有职业升级到Lv4时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：4000",
                ["Job_Lv5_Cost"] = "【职业Lv5金币消耗】\n所有职业升级到Lv5时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：5000",

                ["Job_Reset_Cost"]    = "【职业技能重置费用】\n重置职业技能点时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：1000",
                ["Active_Reset_Cost"] = "【主动技能重置费用】\n重置主动技能点时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：500",
                ["Passive_Reset_Cost"]= "【被动技能重置费用】\n重置被动技能点时消耗的金币数量。\n仅服务器管理员可修改，自动同步至客户端。\n默认：100",
            };
        }
    }
}

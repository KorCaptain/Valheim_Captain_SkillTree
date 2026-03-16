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

                ["Rogue_ElementalResistance_Debuff"] =
                "【元素抗性提升(%)】\n" +
                "盗贼被动：提升对元素伤害的抗性。\n" +
                "推荐：8-15%",

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
                ["Beserker_Passive_HealthThreshold"] =
                "【被动激活生命值阈值(%)】\n" +
                "生命值低于此百分比时激活无敌状态。\n" +
                "推荐：8-15%",

                ["Beserker_Passive_InvincibilityDuration"] =
                "【无敌持续时间（秒）】\n" +
                "被动激活时无敌状态的持续时间。\n" +
                "推荐：5-10秒",

                ["Beserker_Passive_Cooldown"] =
                "【被动冷却时间（秒）】\n" +
                "下次被动无敌激活前的等待时间。\n" +
                "默认：180秒（3分钟）\n" +
                "推荐：120-300秒",

                // === 狂战士：被动生命值加成 ===
                ["Berserker_Passive_HealthBonus"] =
                "【最大生命值加成(%)】\n" +
                "狂战士被动：提升最大生命值。\n" +
                "推荐：100%",
            };
        }
    }
}

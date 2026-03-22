using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetRangedDescriptions_CN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // 法杖树 (Staff Tree)
                // ========================================

                // === Tier 0: 法杖专家 ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【元素伤害加成(%)】\n" +
                "提升法杖的元素伤害（火焰、冰霜、闪电）。\n" +
                "魔法攻击的基础属性。\n" +
                "推荐：3-7%",

                ["Tier0_StaffExpert_RequiredPoints"] =
                "【所需点数】\n解锁法杖专家所需的点数。",

                // === Tier 1: 心神集中 & 魔法流动 ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【以特尔费用减少(%)】\n" +
                "心神集中降低施法时的以特尔消耗。\n" +
                "使魔法可以更频繁地使用。\n" +
                "推荐：12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【最大以特尔加成（固定）】\n" +
                "魔法流动提升最大以特尔上限。\n" +
                "推荐：25-35",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【所需点数】\n解锁心神集中所需的点数。",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【所需点数】\n解锁魔法流动所需的点数。",

                // === Tier 2: 魔法增幅 ===
                ["Tier2_MagicAmplify_Chance"] =
                "【增幅概率(%)】\n" +
                "触发元素伤害增幅的概率。\n" +
                "推荐：30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【增幅时元素伤害加成(%)】\n" +
                "触发增幅时的伤害提升量。\n" +
                "推荐：30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【以特尔费用增加(%)】\n" +
                "施法时增加的以特尔消耗。\n" +
                "这是强大魔法所付出的代价。\n" +
                "推荐：15-25%",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【所需点数】\n解锁魔法增幅所需的点数。",

                // === Tier 3: 元素 ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【冰霜伤害加成（固定）】\n" +
                "提升冰霜魔法的伤害。\n" +
                "推荐：2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【火焰伤害加成（固定）】\n" +
                "提升火焰魔法的伤害。\n" +
                "推荐：2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【闪电伤害加成（固定）】\n" +
                "提升闪电魔法的伤害。\n" +
                "推荐：2-5",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【所需点数】\n解锁冰霜元素所需的点数。",

                ["Tier3_FireElement_RequiredPoints"] =
                "【所需点数】\n解锁火焰元素所需的点数。",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【所需点数】\n解锁闪电元素所需的点数。",

                // === Tier 4: 魔力之幸 ===
                ["Tier4_LuckyMana_Chance"] =
                "【免费施法概率(%)】\n" +
                "有概率施放法术时不消耗以特尔。\n" +
                "魔力之幸开启无限魔法之路。\n" +
                "推荐：30-40%",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【所需点数】\n解锁魔力之幸所需的点数。",

                // === Tier 5-1: 双重施法（主动 T）===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【额外弹射物数量】\n" +
                "额外魔法弹射物的数量。\n" +
                "推荐：5-10",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【弹射物伤害(%)】\n" +
                "额外弹射物的伤害百分比。\n" +
                "推荐：10-20%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【散射角度（未使用）】\n" +
                "当前版本未使用。方向固定。",

                ["Tier5_DoubleCast_EitrCost"] =
                "【以特尔消耗】\n" +
                "激活技能时消耗的以特尔量。\n" +
                "推荐：15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前需要等待的时间。\n" +
                "推荐：25-40秒",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【所需点数】\n解锁双重施法所需的点数。",

                // === Tier 5-2: 即时范围治疗（主动 H）===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前需要等待的时间。\n" +
                "推荐：25-40秒",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【以特尔消耗】\n" +
                "使用技能时消耗的以特尔量。\n" +
                "推荐：25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【治疗量（%最大生命值）】\n" +
                "恢复的最大生命值百分比。\n" +
                "推荐：20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【治疗范围（米）】\n" +
                "治疗效果覆盖的区域半径。\n" +
                "推荐：10-15米",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【所需点数】\n解锁范围治疗所需的点数。",

                // ========================================
                // 弩树 (Crossbow Tree)
                // ========================================

                // === Tier 0: 弩专家 ===
                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【所需点数】\n解锁弩专家所需的点数。",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【弩伤害加成(%)】\n" +
                "提升弩和弩箭的基础伤害。\n" +
                "推荐：8-12%",

                // === Tier 1: 速射 ===
                ["Tier1_RapidFire_Chance"] =
                "【速射触发概率(%)】\n" +
                "射击时触发速射的概率。\n" +
                "触发时快速连续发射多支弩箭。\n" +
                "推荐：15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【速射弩箭数量】\n" +
                "速射时额外发射的弩箭数量。\n" +
                "推荐：2-4发",

                ["Tier1_RapidFire_DamagePercent"] =
                "【速射伤害(%)】\n" +
                "速射弩箭的伤害百分比。\n" +
                "相对于基础攻击力计算。\n" +
                "推荐：60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【速射间隔（秒）】\n" +
                "速射时每发之间的时间间隔。\n" +
                "越小越快。\n" +
                "推荐：0.1-0.3秒",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【速射弩箭消耗】\n" +
                "每次速射消耗的弩箭数量。\n" +
                "推荐：1-2",

                ["Tier1_RapidFire_RequiredPoints"] =
                "【所需点数】\n解锁速射所需的点数。",

                // === Tier 2: 平衡瞄准 ===
                ["Tier2_BalancedAim_KnockbackChance"] =
                "【击退概率(%)】\n" +
                "弩命中时触发击退的概率。\n" +
                "稳定的姿势确保可靠的命中。\n" +
                "推荐：20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【击退距离（米）】\n" +
                "触发时敌人被击退的距离。\n" +
                "推荐：2-4米",

                ["Tier2_BalancedAim_RequiredPoints"] =
                "【所需点数】\n解锁平衡瞄准所需的点数。",

                // === Tier 2: 快速装填 ===
                ["Tier2_RapidReload_SpeedIncrease"] =
                "【装填速度加成(%)】\n" +
                "提升弩的装填速度。\n" +
                "更快地装载下一支弩箭。\n" +
                "推荐：10-20%",

                ["Tier2_RapidReload_RequiredPoints"] =
                "【所需点数】\n解锁快速装填所需的点数。",

                // === Tier 2: 正直射击 ===
                ["Tier2_HonestShot_DamageBonus"] =
                "【基础伤害加成(%)】\n" +
                "额外提升弩的基础攻击力。\n" +
                "推荐：10-18%",

                ["Tier2_HonestShot_RequiredPoints"] =
                "【所需点数】\n解锁正直射击所需的点数。",

                // === Tier 3: 自动装填 ===
                ["Tier3_AutoReload_Chance"] =
                "【自动装填概率(%)】\n" +
                "命中时有概率以200%速度完成下次装填。\n" +
                "有助于维持持续的攻击节奏。\n" +
                "推荐：20-35%",

                ["Tier3_AutoReload_RequiredPoints"] =
                "【所需点数】\n解锁自动装填所需的点数。",

                // === Tier 4: 速射 Lv.2 ===
                ["Tier4_RapidFireLv2_Chance"] =
                "【速射 Lv.2 概率(%)】\n" +
                "触发强化速射的概率。\n" +
                "与速射 Lv.1 的概率叠加计算。\n" +
                "推荐：20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【Lv.2 弩箭数量】\n" +
                "强化速射时额外发射的弩箭数量。\n" +
                "多于速射 Lv.1。\n" +
                "推荐：4-6发",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【速射 Lv.2 伤害(%)】\n" +
                "强化速射的伤害百分比。\n" +
                "伤害高于 Lv.1。\n" +
                "推荐：75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【速射 Lv.2 间隔（秒）】\n" +
                "强化速射每发之间的时间间隔。\n" +
                "推荐：0.1-0.3秒",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【Lv.2 弩箭消耗】\n" +
                "强化速射的弩箭消耗数量。\n" +
                "推荐：1-2",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【所需点数】\n解锁速射 Lv.2 所需的点数。",

                // === Tier 4: 终结一击 ===
                ["Tier4_FinalStrike_HpThreshold"] =
                "【敌人生命值阈值(%)】\n" +
                "对生命值高于此阈值的敌人造成额外伤害。\n" +
                "对高生命值目标有效。\n" +
                "推荐：40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【额外伤害(%)】\n" +
                "对生命值高于阈值的敌人造成的额外伤害。\n" +
                "推荐：20-40%",

                ["Tier4_FinalStrike_RequiredPoints"] =
                "【所需点数】\n解锁终结一击所需的点数。",

                // === Tier 5: 一击必杀（主动 T）===
                ["Tier5_OneShot_Duration"] =
                "【效果持续时间（秒）】\n" +
                "「一击必杀」效果的持续时长。\n" +
                "在此期间可以发射一次强力射击。\n" +
                "推荐：8-12秒",

                ["Tier5_OneShot_DamageBonus"] =
                "【一击必杀伤害加成(%)】\n" +
                "触发「一击必杀」时的额外伤害。\n" +
                "毁灭性一击的力量。\n" +
                "推荐：150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【击退距离（米）】\n" +
                "命中时敌人被击退的距离。\n" +
                "强大的冲击力将敌人击飞。\n" +
                "推荐：5-10米",

                ["Tier5_OneShot_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前需要等待的时间。\n" +
                "推荐：25-40秒",

                ["Tier5_OneShot_RequiredPoints"] =
                "【所需点数】\n解锁一击必杀所需的点数。",

                // ========================================
                // 弓树 (Bow Tree)
                // ========================================

                // === Tier 0: 弓专家 ===
                ["Tier0_BowExpert_DamageBonus"] =
                "【弓伤害加成(%)】\n" +
                "提升弓和箭矢的基础伤害。\n" +
                "推荐：8-12%",

                ["Tier0_BowExpert_RequiredPoints"] =
                "【所需点数】\n解锁弓专家所需的点数。",

                // === Tier 1-1: 专注射击 ===
                ["Tier1_FocusedShot_CritBonus"] =
                "【暴击率加成(%)】\n" +
                "专注射击提升暴击概率。\n" +
                "专注度越高，暴击率越高。\n" +
                "推荐：5-10%",

                ["Tier1_FocusedShot_RequiredPoints"] =
                "【所需点数】\n解锁专注射击所需的点数。",

                // === Tier 1-2: 多重射击 Lv.1 ===
                ["Tier1_MultishotLv1_ActivationChance"] =
                "【多重射击 Lv.1 概率(%)】\n" +
                "使用弓射击时触发多重射击的概率。\n" +
                "触发时同时发射多支箭矢。\n" +
                "推荐：15-25%",

                ["Tier1_MultishotLv1_AdditionalArrows"] =
                "【额外箭矢数量】\n" +
                "多重射击时额外发射的箭矢数量。\n" +
                "推荐：2-4",

                ["Tier1_MultishotLv1_ArrowConsumption"] =
                "【箭矢消耗】\n" +
                "多重射击时消耗的箭矢数量。\n" +
                "推荐：1-2",

                ["Tier1_MultishotLv1_DamagePerArrow"] =
                "【每支箭矢伤害(%)】\n" +
                "多重射击中每支箭矢的伤害百分比。\n" +
                "推荐：50-70%",

                ["Tier1_MultishotLv1_RequiredPoints"] =
                "【所需点数】\n解锁多重射击 Lv.1 所需的点数。",

                // === Tier 2: 弓术精通 ===
                ["Tier2_BowMastery_SkillBonus"] =
                "【弓技能加成（固定）】\n" +
                "提升弓技能等级。\n" +
                "精通度越高，攻击越强。\n" +
                "推荐：5-10",

                ["Tier2_BowMastery_SpecialArrowChance"] =
                "【特殊箭矢概率(%)】\n" +
                "触发具有特殊效果箭矢的概率。\n" +
                "可附加毒素、火焰、冰霜等状态效果。\n" +
                "推荐：25-35%",

                ["Tier2_BowMastery_RequiredPoints"] =
                "【所需点数】\n解锁弓术精通所需的点数。",

                // === Tier 3-1: 无声打击 ===
                ["Tier3_SilentStrike_DamageBonus"] =
                "【无声打击伤害加成（固定）】\n" +
                "以固定数值提升弓的伤害。\n" +
                "箭矢贯穿敌人造成更多伤害。\n" +
                "推荐：3-8",

                ["Tier3_SilentStrike_RequiredPoints"] =
                "【所需点数】\n解锁无声打击所需的点数。",

                // === Tier 3-2: 多重射击 Lv.2 ===
                ["Tier3_MultishotLv2_ActivationChance"] =
                "【多重射击 Lv.2 概率(%)】\n" +
                "强化版多重射击触发概率。\n" +
                "比 Lv.1 发射更多箭矢。\n" +
                "推荐：20-30%",

                ["Tier3_MultishotLv2_RequiredPoints"] =
                "【所需点数】\n解锁多重射击 Lv.2 所需的点数。",

                // === Tier 3-3: 猎人本能 ===
                ["Tier3_HuntingInstinct_CritBonus"] =
                "【猎人本能暴击率加成(%)】\n" +
                "猎人本能提升暴击概率。\n" +
                "推荐：8-15%",

                ["Tier3_HuntingInstinct_RequiredPoints"] =
                "【所需点数】\n解锁猎人本能所需的点数。",

                // === Tier 4: 精确瞄准 ===
                ["Tier4_PrecisionAim_CritDamage"] =
                "【暴击伤害加成(%)】\n" +
                "精确瞄准提升暴击时的伤害倍率。\n" +
                "命中弱点造成巨大伤害。\n" +
                "推荐：25-40%",

                ["Tier4_PrecisionAim_RequiredPoints"] =
                "【所需点数】\n解锁精确瞄准所需的点数。",

                // === Tier 5: 爆裂箭（主动 T）===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] =
                "【爆裂箭伤害(%)】\n" +
                "主动 T — 爆裂箭的伤害倍率。\n" +
                "具有范围伤害的强力箭矢。\n" +
                "推荐：100-150%",

                ["Tier5_ExplosiveArrow_Radius"] =
                "【爆炸范围（米）】\n" +
                "爆裂箭的伤害半径。\n" +
                "推荐：3-6米",

                ["Tier5_ExplosiveArrow_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前需要等待的时间。\n" +
                "推荐：15-25秒",

                ["Tier5_ExplosiveArrow_StaminaCost"] =
                "【体力消耗(%)】\n" +
                "使用技能时消耗的体力百分比。\n" +
                "推荐：10-20%",

                ["Tier5_ExplosiveArrow_RequiredPoints"] =
                "【所需点数】\n解锁爆裂箭所需的点数。",

                // === Tier 6: 箭雨 ===
                ["Tier6_ArrowRain_DamagePercent"] =
                "【每箭伤害比例(%)】\n每箭造成(弓+箭)伤害的该比例。\n推荐：10-20%",

                ["Tier6_ArrowRain_ArrowCount"] =
                "【箭数量】\n箭雨中落下的箭矢数量。\n推荐：20-40",

                ["Tier6_ArrowRain_Radius"] =
                "【落箭半径(米)】\n箭矢落下的范围半径。\n推荐：10-20m",

                ["Tier6_ArrowRain_Cooldown"] =
                "【冷却时间(秒)】\n技能再次使用的等待时间。\n推荐：35-60秒",

                ["Tier6_ArrowRain_StaminaCost"] =
                "【体力消耗(%)】\n使用技能时消耗的最大体力百分比。\n推荐：20-30%",

                ["Tier6_ArrowRain_RequiredPoints"] =
                "【所需点数】\n解锁箭雨节点所需的技能点数。",
            };
        }
    }
}

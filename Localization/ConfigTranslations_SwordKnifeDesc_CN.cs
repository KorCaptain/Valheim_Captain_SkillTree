using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetSwordKnifeDescriptions_CN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // 剑树 (Sword Tree)
                // ========================================

                // === Tier 0: 剑专家 ===
                ["Sword_Expert_DamageIncrease"] =
                "【剑伤害加成(%)】\n" +
                "提升剑的基础攻击力。\n" +
                "适用于所有剑类型。\n" +
                "推荐：10-20%",

                ["Tier0_SwordExpert_DamageBonus"] =
                "【剑伤害加成(%)】\n" +
                "提升剑的基础攻击力。\n" +
                "适用于所有剑类型。\n" +
                "推荐：10-20%",

                ["Tier0_SwordExpert_RequiredPoints"] =
                "【所需点数】\n解锁剑专家所需的点数。",

                // === Tier 1: 快速斩击 ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【攻击速度加成(%)】\n" +
                "提升剑的攻击速度。\n" +
                "可实现快速连续攻击。\n" +
                "推荐：10-20%",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【攻击速度加成(%)】\n" +
                "提升剑的攻击速度。\n" +
                "可实现快速连续攻击。\n" +
                "推荐：10-20%",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【所需点数】\n解锁快速斩击所需的点数。",

                // === Tier 1: 反击姿态 ===
                ["Tier1_CounterStance_Duration"] =
                "【持续时间（秒）】\n" +
                "反击姿态的生效持续时间。\n" +
                "此期间防御加成提升。\n" +
                "推荐：3-6秒",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【防御加成(%)】\n" +
                "反击姿态期间的防御提升量。\n" +
                "坚守阵地，等待反击时机。\n" +
                "推荐：20-40%",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【所需点数】\n解锁反击姿态所需的点数。",

                // === Tier 2: 连击斩 ===
                ["Sword_ComboSlash_Bonus"] =
                "【连击加成(%)】\n" +
                "连击时造成额外伤害。\n" +
                "保持高连击数可保证高DPS。\n" +
                "推荐：15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【增益持续时间（秒）】\n" +
                "连击加成增益的生效时间。\n" +
                "此期间攻击可延长增益时长。\n" +
                "推荐：3-5秒",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【连击加成(%)】\n" +
                "连击时造成额外伤害。\n" +
                "保持高连击数可保证高DPS。\n" +
                "推荐：15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【增益持续时间（秒）】\n" +
                "连击加成增益的生效时间。\n" +
                "此期间攻击可延长增益时长。\n" +
                "推荐：3-5秒",

                ["Tier2_ComboSlash_RequiredPoints"] =
                "【所需点数】\n解锁连击斩所需的点数。",

                // === Tier 3: 刃光反射 / 反制 ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【攻击加成（固定值）】\n" +
                "以固定值提升刃光反射的攻击力。\n" +
                "招架后发动强力反击。\n" +
                "推荐：20-40",

                ["Tier3_Riposte_DamageBonus"] =
                "【攻击加成（固定值）】\n" +
                "以固定值提升刃光反射的攻击力。\n" +
                "招架后发动强力反击。\n" +
                "推荐：5-15",

                ["Tier3_Riposte_RequiredPoints"] =
                "【所需点数】\n解锁反制所需的点数。",

                // === Tier 4: 攻守兼备 ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【攻击加成(%)】\n" +
                "同时强化攻击与防御。\n" +
                "适合均衡战斗风格。\n" +
                "推荐：10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【防御加成（固定值）】\n" +
                "攻守兼备姿态的防御加成。\n" +
                "进攻时也能保持稳固防御。\n" +
                "推荐：15-30",

                ["Tier4_AllInOne_RequiredPoints"] =
                "【所需点数】\n解锁攻守兼备所需的点数。",

                // === Tier 4: 真实决斗 ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【攻击速度加成(%)】\n" +
                "单挑时的攻击速度加成。\n" +
                "迅速打击压制敌人。\n" +
                "推荐：15-30%",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【攻击速度加成(%)】\n" +
                "单挑时的攻击速度加成。\n" +
                "迅速打击压制敌人。\n" +
                "推荐：15-30%",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【所需点数】\n解锁真实决斗所需的点数。",

                // === Tier 5: 招架冲锋（主动技能 H）===
                ["Sword_ParryCharge_BuffDuration"] =
                "【增益持续时间（秒）】\n" +
                "成功招架后增益的生效时间。\n" +
                "此期间可使用强化攻击。\n" +
                "推荐：5-10秒",

                ["Sword_ParryCharge_DamageBonus"] =
                "【冲锋伤害加成(%)】\n" +
                "招架后冲锋的伤害提升量。\n" +
                "完美时机 = 强力反击。\n" +
                "推荐：50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【击退距离（米）】\n" +
                "冲锋时将敌人击退的距离。\n" +
                "用于调整距离和控制战场。\n" +
                "推荐：3-7米",

                ["Sword_ParryCharge_StaminaCost"] =
                "【体力消耗】\n" +
                "激活增益（H键）消耗的体力。\n" +
                "体力管理至关重要。\n" +
                "推荐：20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前的等待时间。\n" +
                "越少 = 可越频繁使用。\n" +
                "推荐：10-20秒",

                ["Tier5_ParryRush_BuffDuration"] =
                "【增益持续时间（秒）】\n" +
                "成功招架后增益的生效时间。\n" +
                "推荐：20-40秒",

                ["Tier5_ParryRush_DamageBonus"] =
                "【冲锋伤害加成(%)】\n" +
                "招架后冲锋的伤害提升量。\n" +
                "推荐：50-100%",

                ["Tier5_ParryRush_PushDistance"] =
                "【击退距离（米）】\n" +
                "冲锋时将敌人击退的距离。\n" +
                "推荐：3-7米",

                ["Tier5_ParryRush_StaminaCost"] =
                "【体力消耗】\n" +
                "激活技能消耗的体力。\n" +
                "推荐：10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前的等待时间。\n" +
                "推荐：30-60秒",

                ["Tier5_ParryRush_RequiredPoints"] =
                "【所需点数】\n解锁招架冲锋所需的点数。",

                // === Tier 6: 冲锋连斩（主动技能 G）===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【第1击伤害(%)】\n" +
                "冲锋连斩第一击的伤害。\n" +
                "相对于基础攻击的倍率。\n" +
                "推荐：80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【第2击伤害(%)】\n" +
                "冲锋连斩第二击的伤害。\n" +
                "连击升级，伤害递增。\n" +
                "推荐：100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【第3击伤害(%)】\n" +
                "冲锋连斩终结击的伤害。\n" +
                "最强力的终结一击。\n" +
                "推荐：150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【冲锋距离（米）】\n" +
                "技能开始时的冲锋距离。\n" +
                "快速接近敌人。\n" +
                "推荐：5-10米",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【侧向移动距离（米）】\n" +
                "攻击过程中左右移动的距离。\n" +
                "同时闪避与攻击。\n" +
                "推荐：2-5米",

                ["Sword_RushSlash_StaminaCost"] =
                "【体力消耗】\n" +
                "使用技能（G键）消耗的体力。\n" +
                "强力技能需要大量体力。\n" +
                "推荐：40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前的等待时间。\n" +
                "越少 = 可越频繁使用。\n" +
                "推荐：15-30秒",

                ["Sword_RushSlash_MovementSpeed"] =
                "【移动速度（米/秒）】\n" +
                "冲锋过程中的移动速度。\n" +
                "越快 = 战斗越动态。\n" +
                "推荐：8-15米/秒",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【攻击速度加成(%)】\n" +
                "技能期间的攻击速度加成。\n" +
                "忽略其他树的速度，仅此值生效。\n" +
                "推荐：20-40%",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【第1击伤害(%)】\n" +
                "冲锋连斩第一击的伤害。\n" +
                "推荐：60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【第2击伤害(%)】\n" +
                "冲锋连斩第二击的伤害。\n" +
                "推荐：70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【第3击伤害(%)】\n" +
                "冲锋连斩终结击的伤害。\n" +
                "推荐：80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【初始冲锋距离（米）】\n" +
                "技能开始时的冲锋距离。\n" +
                "推荐：3-8米",

                ["Tier6_RushSlash_SideDistance"] =
                "【侧向移动距离（米）】\n" +
                "攻击过程中左右移动的距离。\n" +
                "推荐：2-5米",

                ["Tier6_RushSlash_StaminaCost"] =
                "【体力消耗】\n" +
                "使用技能（G键）消耗的体力。\n" +
                "推荐：20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前的等待时间。\n" +
                "推荐：15-30秒",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【移动速度（米/秒）】\n" +
                "冲锋过程中的移动速度。\n" +
                "推荐：10-25米/秒",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【攻击速度加成(%)】\n" +
                "技能期间的攻击速度加成。\n" +
                "推荐：150-250%",

                ["Tier6_RushSlash_RequiredPoints"] =
                "【所需点数】\n解锁冲锋连斩所需的点数。",

                // ========================================
                // 匕首树 (Knife Tree)
                // ========================================

                // === Tier 0: 匕首专家 ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【背刺伤害加成(%)】\n" +
                "从背后攻击时造成的额外伤害。\n" +
                "刺客的基础能力。\n" +
                "推荐：30-50%",

                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【所需点数】\n解锁匕首专家所需的点数。",

                // === Tier 1: 闪避精通 ===
                ["Tier1_Evasion_Chance"] =
                "【闪避率(%)】\n" +
                "躲避敌人攻击的概率。\n" +
                "越高 = 受到的攻击越少。\n" +
                "推荐：15-25%",

                ["Tier1_Evasion_Duration"] =
                "【无敌持续时间（秒）】\n" +
                "成功闪避后的无敌时间。\n" +
                "推荐：2-4秒",

                ["Tier1_Evasion_RequiredPoints"] =
                "【所需点数】\n解锁闪避精通所需的点数。",

                // === Tier 2: 快速移动 ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【移动速度加成(%)】\n" +
                "提升基础移动速度。\n" +
                "高机动性可迷惑敌人。\n" +
                "推荐：10-20%",

                ["Tier2_FastMove_RequiredPoints"] =
                "【所需点数】\n解锁快速移动所需的点数。",

                // === Tier 3: 战斗精通 ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【伤害加成（固定值）】\n" +
                "每次攻击附加固定伤害。\n" +
                "推荐：1-4",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【增益持续时间（秒）】\n" +
                "战斗精通增益的生效时间。\n" +
                "推荐：8-12秒",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【所需点数】\n解锁战斗精通所需的点数。",

                // === Tier 4: 攻击与闪避 ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【闪避率加成(%)】\n" +
                "同时攻击时提升闪避率。\n" +
                "积极防御。\n" +
                "推荐：20-30%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【增益持续时间（秒）】\n" +
                "闪避效果提升增益的生效时间。\n" +
                "推荐：8-12秒",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "增益再次激活前的等待时间。\n" +
                "推荐：25-35秒",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【所需点数】\n解锁攻击与闪避所需的点数。",

                // === Tier 5: 暴击伤害 ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【伤害加成(%)】\n" +
                "提升暴击时的伤害。\n" +
                "与匕首高暴击率有良好协同。\n" +
                "推荐：20-35%",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【所需点数】\n解锁暴击伤害所需的点数。",

                // === Tier 6: 刺客 ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【暴击伤害加成(%)】\n" +
                "进一步提升暴击时的伤害。\n" +
                "推荐：20-30%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【暴击率加成(%)】\n" +
                "提升暴击的触发概率。\n" +
                "推荐：10-18%",

                ["Tier6_Assassin_RequiredPoints"] =
                "【所需点数】\n解锁刺客所需的点数。",

                // === Tier 7: 暗杀 ===
                ["Tier7_Assassination_StaggerChance"] =
                "【晕眩概率(%)】\n" +
                "连击时使敌人晕眩的概率。\n" +
                "中断敌人的攻击。\n" +
                "推荐：30-45%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【所需连击数】\n" +
                "触发晕眩所需的连续命中次数。\n" +
                "推荐：2-4次",

                ["Tier7_Assassination_RequiredPoints"] =
                "【所需点数】\n解锁暗杀所需的点数。",

                // === Tier 8: 刺客之心（主动技能 G）===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【暴击伤害倍率】\n" +
                "主动G — 刺客之心的暴击伤害倍率。\n" +
                "瞬移至敌人背后并发动致命连击。\n" +
                "推荐：1.2-1.5倍",

                ["Tier8_AssassinHeart_Duration"] =
                "【增益持续时间（秒）】\n" +
                "刺客之心增益的生效时间。\n" +
                "推荐：5-10秒",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【体力消耗】\n" +
                "使用技能消耗的体力。\n" +
                "推荐：15-25",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前的等待时间。\n" +
                "推荐：35-50秒",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【传送范围（米）】\n" +
                "搜索敌人的最大范围。\n" +
                "传送至此半径内敌人背后。\n" +
                "推荐：6-10米",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【敌人背后定位距离（米）】\n" +
                "在敌人背后定位的距离。\n" +
                "背刺攻击的定位距离。\n" +
                "推荐：0.8-1.5米",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【晕眩持续时间（秒）】\n" +
                "敌人处于晕眩状态的时间。\n" +
                "推荐：0.5-2秒",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【连击次数】\n" +
                "传送后自动命中的次数。\n" +
                "推荐：2-4次",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【命中间隔（秒）】\n" +
                "连击各次命中之间的时间间隔。\n" +
                "越少 = 命中越即时。\n" +
                "推荐：0.2-0.5秒",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【所需点数】\n解锁刺客之心所需的点数。",
            };
        }
    }
}

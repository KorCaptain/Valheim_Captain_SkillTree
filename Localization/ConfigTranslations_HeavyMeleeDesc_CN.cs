using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetHeavyMeleeDescriptions_CN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // 长矛树 (Spear Tree)
                // ========================================

                // === Tier 0: 长矛专家 ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【所需点数】\n解锁长矛专家所需的点数。",

                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【2连击后攻击速度加成(%)】\n" +
                "连续2次长矛攻击后，攻击速度提升。\n" +
                "以快速连招压制敌人。\n" +
                "推荐：10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【2连击后伤害加成(%)】\n" +
                "连续2次长矛攻击后，伤害提升。\n" +
                "最大化连招伤害输出。\n" +
                "推荐：7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【效果持续时间（秒）】\n" +
                "2连击后效果的持续时间。\n" +
                "持续时间越长，战斗越稳定。\n" +
                "推荐：4-8秒",

                // === Tier 1: 精准打击 ===
                ["Tier1_QuickStrike_RequiredPoints"] =
                "【所需点数】\n解锁精准打击所需的点数。",

                ["Tier1_VitalStrike_DamageBonus"] =
                "【暴击伤害加成(%)】\n" +
                "提升长矛暴击的伤害。\n" +
                "专精于精准打击弱点。\n" +
                "推荐：20-40%",

                // === Tier 2: 投矛 ===
                ["Tier2_Throw_RequiredPoints"] =
                "【所需点数】\n解锁投矛所需的点数。",

                ["Tier2_Throw_Cooldown"] =
                "【投矛冷却时间（秒）】\n" +
                "再次使用投矛的等待时间。\n" +
                "冷却越短，使用频率越高。\n" +
                "推荐：20-40秒",

                ["Tier2_Throw_DamageMultiplier"] =
                "【投矛伤害倍率(%)】\n" +
                "投出的长矛的伤害倍率。\n" +
                "决定远程攻击的强度。\n" +
                "推荐：100-150%",

                ["Legacy_Throw_BuffDuration"] =
                "【未使用】\n" +
                "此参数当前未使用。\n" +
                "已更改为被动技能。",

                // === Tier 3: 快速长矛 ===
                ["Tier3_Pierce_RequiredPoints"] =
                "【所需点数】\n解锁快速长矛所需的点数。",

                ["Tier3_Rapid_DamageBonus"] =
                "【武器攻击加成（固定值）】\n" +
                "将长矛的基础攻击提升固定数值。\n" +
                "有利于快速连续攻击。\n" +
                "推荐：3-6",

                ["Tier3_QuickSpear_AttackSpeed"] =
                "【攻击速度加成(%)】\n" +
                "提升长矛或长柄武器的攻击速度。\n" +
                "推荐：15-25%",

                // === Tier 4: 闪避打击 ===
                ["Tier4_Evasion_RequiredPoints"] =
                "【所需点数】\n解锁闪避打击所需的点数。",

                ["Tier4_Evasion_EvasionBonus"] =
                "【攻击时闪避加成(%)】\n" +
                "刺出长矛后5秒内，闪避率提升。\n" +
                "提升积极战斗风格的生存能力。\n" +
                "推荐：15-25%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【攻击时体力消耗减少(%)】\n" +
                "闪避打击攻击的体力消耗降低。\n" +
                "可以持续战斗更长时间。\n" +
                "推荐：5-15%",

                // === Tier 4: 双重打击 ===
                ["Tier4_Dual_DamageBonus"] =
                "【双重打击伤害加成(%)】\n" +
                "两次连续攻击时附加额外伤害。\n" +
                "专精于连招伤害攻击。\n" +
                "推荐：18-30%",

                ["Tier4_Dual_Duration"] =
                "【效果持续时间（秒）】\n" +
                "双重打击增益的持续时间。\n" +
                "持续时间越长，伤害越稳定。\n" +
                "推荐：8-15秒",

                // === Tier 5: 穿刺长矛（主动G键）===
                ["Tier5_Penetrate_RequiredPoints"] =
                "【所需点数】\n解锁穿刺长矛所需的点数。",

                ["Legacy_Penetrate_CritChance"] =
                "【未使用】\n" +
                "此参数当前未使用。\n" +
                "已更改为闪电打击效果。",

                ["Tier5_Penetrate_BuffDuration"] =
                "【效果持续时间（秒）】\n" +
                "穿刺长矛增益的持续时间。\n" +
                "决定技能效果的持续时间。\n" +
                "推荐：25-40秒",

                ["Tier5_Penetrate_LightningDamage"] =
                "【闪电伤害倍率(%)】\n" +
                "连招攻击时的闪电伤害倍率。\n" +
                "造成强力的附加伤害。\n" +
                "推荐：200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【触发闪电所需命中次数】\n" +
                "触发闪电效果所需的连续命中次数。\n" +
                "次数越少，触发越频繁。\n" +
                "推荐：3-5次",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【G键技能冷却时间（秒）】\n" +
                "再次使用G键技能的等待时间。\n" +
                "冷却越短，使用频率越高。\n" +
                "推荐：50-80秒",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【G键技能体力消耗】\n" +
                "使用G键技能时消耗的体力。\n" +
                "体力管理至关重要。\n" +
                "推荐：20-35",

                // === Tier 5: 长矛连招（主动H键）===
                ["Tier5_Combo_RequiredPoints"] =
                "【所需点数】\n解锁长矛连招所需的点数。",

                ["Tier5_Combo_HKey_Cooldown"] =
                "【H键技能冷却时间（秒）】\n" +
                "再次使用H键技能的等待时间。\n" +
                "冷却越短，使用频率越高。\n" +
                "推荐：20-35秒",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【H键技能伤害倍率(%)】\n" +
                "长矛连招攻击（H键）的伤害倍率。\n" +
                "强力的单次命中技能。\n" +
                "推荐：250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【H键技能体力消耗】\n" +
                "使用H键技能时消耗的体力。\n" +
                "体力管理必不可少。\n" +
                "推荐：15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【H键击退距离（米）】\n" +
                "H键技能命中时，敌人被击退的距离。\n" +
                "有助于战斗中的位置调整。\n" +
                "推荐：2-5米",

                ["Tier5_Combo_ActiveRange"] =
                "【主动效果半径（米）】\n" +
                "激活长矛连招增益的范围半径。\n" +
                "范围越大，激活情况越多。\n" +
                "推荐：2-5米",

                ["Tier5_Combo_BuffDuration"] =
                "【效果持续时间（秒）】\n" +
                "长矛连招增益的持续时间。\n" +
                "持续时间越长，投矛强化越稳定。\n" +
                "推荐：25-40秒",

                ["Tier5_Combo_MaxUses"] =
                "【最大强化投矛次数】\n" +
                "增益期间最大强化投矛次数。\n" +
                "次数越多，强化持续越长。\n" +
                "推荐：2-5次",

                // ========================================
                // 锤树 (Mace Tree)
                // ========================================

                // === Tier 0: 锤专家 ===
                ["Tier0_MaceExpert_RequiredPoints"] =
                "【所需点数】\n解锁锤专家所需的点数。",

                ["Tier0_MaceExpert_DamageBonus"] =
                "【锤伤害加成(%)】\n" +
                "提升打击武器的基础攻击。\n" +
                "适用于所有锤类武器。\n" +
                "推荐：5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【晕眩概率(%)】\n" +
                "锤击时使敌人晕眩的概率。\n" +
                "晕眩的敌人无法行动。\n" +
                "推荐：15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【晕眩持续时间（秒）】\n" +
                "晕眩效果的持续时间。\n" +
                "持续时间越长，造成伤害的时间越多。\n" +
                "推荐：0.3-1秒",

                // === Tier 1: 锤强化 ===
                ["Tier1_MaceExpert_DamageBonus"] =
                "【锤攻击加成(%)】\n" +
                "打击武器的额外攻击加成。\n" +
                "推荐：8-15%",

                ["Tier1_MaceExpert_RequiredPoints"] =
                "【所需点数】\n解锁锤强化所需的点数。",

                // === Tier 2: 晕眩强化 ===
                ["Tier2_StunBoost_StunChanceBonus"] =
                "【晕眩概率加成(%)】\n" +
                "额外提升晕眩概率。\n" +
                "与锤专家加成叠加。\n" +
                "推荐：10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【晕眩持续时间加成（秒）】\n" +
                "额外延长晕眩时间。\n" +
                "有更多时间造成伤害。\n" +
                "推荐：0.3-0.8秒",

                ["Tier2_StunBoost_RequiredPoints"] =
                "【所需点数】\n解锁晕眩强化所需的点数。",

                // === Tier 3: 防守 ===
                ["Tier3_Guard_ArmorBonus"] =
                "【护甲加成（固定值）】\n" +
                "将基础护甲提升固定数值。\n" +
                "对坦克流派有利。\n" +
                "推荐：2-5",

                ["Tier3_Guard_RequiredPoints"] =
                "【所需点数】\n解锁防守所需的点数。",

                // === Tier 3: 重击 ===
                ["Tier3_HeavyStrike_DamageBonus"] =
                "【冲击伤害加成（固定值）】\n" +
                "将锤的冲击伤害提升固定数值。\n" +
                "与百分比加成叠加。\n" +
                "推荐：2-5",

                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【所需点数】\n解锁重击所需的点数。",

                // === Tier 4: 击退 ===
                ["Tier4_Push_KnockbackChance"] =
                "【击退概率(%)】\n" +
                "攻击时使敌人击退的概率。\n" +
                "有助于保持距离和战场控制。\n" +
                "推荐：25-35%",

                ["Tier4_Push_RequiredPoints"] =
                "【所需点数】\n解锁击退所需的点数。",

                // === Tier 5: 坦克 ===
                ["Tier5_Tank_HealthBonus"] =
                "【生命值加成(%)】\n" +
                "提升最大生命值。\n" +
                "对强化生存能力至关重要。\n" +
                "推荐：20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【受到伤害减少(%)】\n" +
                "减少所有受到的伤害。\n" +
                "与护甲配合，是坦克角色的理想选择。\n" +
                "推荐：8-15%",

                ["Tier5_Tank_RequiredPoints"] =
                "【所需点数】\n解锁坦克所需的点数。",

                // === Tier 5: 伤害（DPS）===
                ["Tier5_DPS_DamageBonus"] =
                "【攻击加成(%)】\n" +
                "额外提升锤的攻击力。\n" +
                "对DPS流派有利。\n" +
                "推荐：15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【攻击速度加成(%)】\n" +
                "提升锤的攻击速度。\n" +
                "弥补重型武器的迟缓。\n" +
                "推荐：8-15%",

                ["Tier5_DPS_RequiredPoints"] =
                "【所需点数】\n解锁DPS所需的点数。",

                // === Tier 6: 大师 ===
                ["Tier6_Grandmaster_ArmorBonus"] =
                "【护甲加成(%)】\n" +
                "百分比护甲加成。\n" +
                "与高级护甲有良好协同。\n" +
                "推荐：15-25%",

                ["Tier6_Grandmaster_RequiredPoints"] =
                "【所需点数】\n解锁大师所需的点数。",

                // === Tier 7: 愤怒之锤（主动H键）===
                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【第1-4击伤害倍率(%)】\n" +
                "「愤怒之锤」技能（H键）第1-4击的伤害倍率。\n" +
                "应用于当前攻击。\n" +
                "推荐：70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【最终一击伤害倍率(%)】\n" +
                "「愤怒之锤」技能（H键）最终一击的伤害倍率。\n" +
                "最后一击是最强的打击。\n" +
                "推荐：130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【体力消耗】\n" +
                "使用H键技能时消耗的体力。\n" +
                "体力管理至关重要。\n" +
                "推荐：35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用的等待时间。\n" +
                "冷却越短，使用频率越高。\n" +
                "推荐：25-35秒",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【AOE半径（米）】\n" +
                "技能的范围伤害半径。\n" +
                "半径越大，命中的敌人越多。\n" +
                "推荐：4-7米",

                ["Tier7_FuryHammer_RequiredPoints"] =
                "【所需点数】\n解锁愤怒之锤所需的点数。",

                // === Tier 7: 守护之心（主动G键）===
                ["Tier7_GuardianHeart_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用G键技能的等待时间。\n" +
                "冷却越短，防御姿态使用越频繁。\n" +
                "推荐：100-140秒",

                ["Tier7_GuardianHeart_StaminaCost"] =
                "【体力消耗】\n" +
                "使用技能时消耗的体力。\n" +
                "作为坦克，体力管理至关重要。\n" +
                "推荐：20-30",

                ["Tier7_GuardianHeart_Duration"] =
                "【效果持续时间（秒）】\n" +
                "防御姿态的持续时间。\n" +
                "在此期间可以反弹伤害并拥有高防御力。\n" +
                "推荐：40-50秒",

                ["Tier7_GuardianHeart_ReflectPercent"] =
                "【伤害反弹百分比(%)】\n" +
                "将受到的伤害反射给攻击者的百分比。\n" +
                "作为坦克，将伤害反还给敌人。\n" +
                "推荐：5-8%",

                ["Tier7_GuardianHeart_RequiredPoints"] =
                "【所需点数】\n解锁守护之心所需的点数。",

                // ========================================
                // 长柄武器树 (Polearm Tree)
                // ========================================

                // === Tier 0: 长柄武器专家 ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【攻击距离加成(%)】\n" +
                "提升长柄武器的攻击距离。\n" +
                "宽广的攻击距离可以从安全距离发动攻击。\n" +
                "推荐：10-20%",

                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【所需点数】\n解锁长柄武器专家所需的点数。",

                // === Tier 1: 旋转攻击 ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【旋转攻击伤害加成(%)】\n" +
                "旋转攻击时的额外伤害。\n" +
                "对多个敌人非常有效。\n" +
                "推荐：50-80%",

                ["Tier1_SpinWheel_RequiredPoints"] =
                "【所需点数】\n解锁旋转攻击所需的点数。",

                // === Tier 2-1: 长柄武器强化 ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] =
                "【武器攻击加成（固定值）】\n" +
                "将长柄武器的基础攻击提升固定数值。\n" +
                "适用于所有长柄武器攻击。\n" +
                "推荐：4-7",

                ["Tier2-1_PolearmBoost_RequiredPoints"] =
                "【所需点数】\n解锁长柄武器强化所需的点数。",

                // === Tier 2-2: 英雄打击 ===
                ["Tier2-2_HeroStrike_KnockbackChance"] =
                "【失稳概率(%)】\n" +
                "攻击时使敌人失去平衡的概率。\n" +
                "有助于战场控制。\n" +
                "推荐：20-35%",

                ["Tier2-2_HeroStrike_RequiredPoints"] =
                "【所需点数】\n解锁英雄打击所需的点数。",

                // === Tier 3: 范围连招 ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【双重打击伤害加成(%)】\n" +
                "两次连续攻击时的额外伤害。\n" +
                "专精于范围连招。\n" +
                "推荐：20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【双重打击增益持续时间（秒）】\n" +
                "双重打击增益的持续时间。\n" +
                "持续时间越长，连招越稳定。\n" +
                "推荐：4-8秒",

                ["Tier3_AreaCombo_RequiredPoints"] =
                "【所需点数】\n解锁范围连招所需的点数。",

                // === Tier 4-1: 砸地 ===
                ["Tier4-1_GroundWheel_RequiredPoints"] =
                "【所需点数】\n解锁砸地所需的点数。",

                // === Tier 4-2: 月牙斩 ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] =
                "【攻击距离加成(%)】\n" +
                "提升月牙斩的攻击范围。\n" +
                "可以在更宽广的半径内发动攻击。\n" +
                "推荐：12-20%",

                ["Tier4-2_MoonSlash_StaminaReduction"] =
                "【体力消耗减少(%)】\n" +
                "减少月牙斩的体力消耗。\n" +
                "可以持续战斗更长时间。\n" +
                "推荐：12-20%",

                ["Tier4-2_MoonSlash_RequiredPoints"] =
                "【所需点数】\n解锁月牙斩所需的点数。",

                // === Tier 4-3: 压制 ===
                ["Tier4-3_Suppress_DamageBonus"] =
                "【压制伤害加成(%)】\n" +
                "压制攻击时的额外伤害。\n" +
                "压制敌人并在战斗中取得主动权。\n" +
                "推荐：25-40%",

                ["Tier4-3_Suppress_RequiredPoints"] =
                "【所需点数】\n解锁压制所需的点数。",

                // === Tier 5: 穿刺冲锋（主动G键）===
                ["Tier5_PierceCharge_DashDistance"] =
                "【冲锋距离（米）】\n" +
                "穿刺技能的冲锋距离。\n" +
                "可以冲破远处的敌人阵列。\n" +
                "推荐：8-12米",

                ["Tier5_PierceCharge_FirstHitDamageBonus"] =
                "【第一击伤害加成(%)】\n" +
                "冲锋中第一击的伤害倍率。\n" +
                "强力的第一击可以压制敌人。\n" +
                "推荐：180-250%",

                ["Tier5_PierceCharge_AoeDamageBonus"] =
                "【击退AOE伤害加成(%)】\n" +
                "冲锋后范围击退的伤害倍率。\n" +
                "击退周围的敌人并造成伤害。\n" +
                "推荐：130-180%",

                ["Tier5_PierceCharge_AoeAngle"] =
                "【AOE角度（度）】\n" +
                "范围击退效果的角度。\n" +
                "280° = 后方/侧面区域，不含80°正面。\n" +
                "推荐：250-300°",

                ["Tier5_PierceCharge_AoeRadius"] =
                "【AOE半径（米）】\n" +
                "范围击退效果的半径。\n" +
                "半径越大，被击退的敌人越多。\n" +
                "推荐：4-7米",

                ["Tier5_PierceCharge_KnockbackDistance"] =
                "【击退距离（米）】\n" +
                "敌人被击退的距离。\n" +
                "有助于战场控制。\n" +
                "推荐：6-10米",

                ["Tier5_PierceCharge_StaminaCost"] =
                "【体力消耗】\n" +
                "使用G键技能时消耗的体力。\n" +
                "体力管理至关重要。\n" +
                "推荐：18-25",

                ["Tier5_PierceCharge_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用G键技能的等待时间。\n" +
                "冷却越短，使用频率越高。\n" +
                "推荐：25-40秒",

                ["Tier5_PierceCharge_RequiredPoints"] =
                "【所需点数】\n解锁穿刺冲锋所需的点数。",
            };
        }
    }
}

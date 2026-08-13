using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetExpertDescriptions_CN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Skill_Tree_Base
                // ========================================
                ["PassiveMessageDisplay"] =
                "【被动消息显示】\n" +
                "控制被动技能触发时的消息显示方式。\n" +
                "  居中（默认） = 屏幕中央\n" +
                "  左上小字 = 左上角小字显示\n" +
                "  关闭 = 禁用显示\n" +
                "※ 学习及生产消息始终显示在屏幕中央。",

                ["GameDifficulty"] =
                "【游戏难度】\n" +
                "选择技能树模组的整体平衡预设。\n" +
                "  Vanilla      = 接近原版的温和数值（默认）\n" +
                "  VeryHard     = CLLC极难 + 怪物HP×2（强化数值）\n" +
                "  UserSettings = 恢复之前保存的用户设置\n" +
                "⚠️ 更改后立即应用所选预设（替换所有技能数值）。",

                ["ShowResetButtons"] =
                "【显示重置按钮】\n" +
                "控制技能树UI中是否显示积分/职业/生产重置按钮（共3个）。\n" +
                "  true  = 显示重置按钮（默认）\n" +
                "  false = 隐藏重置按钮（服务器防止技能积分重置时使用）",

                // ========================================
                // 攻击树 (Attack Tree)
                // ========================================
                ["Tier0_AttackExpert_AllDamageBonus"] =
                "【总伤害加成 (%)】\n" +
                "提升物理伤害和元素伤害。\n" +
                "适用于所有武器的基础攻击力提升。\n" +
                "推荐：8-12%",

                ["Tier2_MeleeSpec_BonusTriggerChance"] =
                "【近战必定额外伤害 (%)】\n" +
                "近战攻击时必定附加额外伤害。\n" +
                "每次攻击固定生效。\n" +
                "推荐：15-25%",

                ["Tier2_MeleeSpec_MeleeDamage"] =
                "【近战额外伤害（固定）】\n" +
                "触发额外伤害时的固定附加伤害值。\n" +
                "推荐：8-15",

                ["Tier2_BowSpec_BonusTriggerChance"] =
                "【弓必定额外伤害 (%)】\n" +
                "弓攻击时必定附加额外伤害。\n" +
                "每次攻击固定生效。\n" +
                "推荐：15-25%",

                ["Tier2_BowSpec_BowDamage"] =
                "【弓额外伤害（固定）】\n" +
                "触发额外伤害时的固定附加伤害值。\n" +
                "推荐：6-12",

                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] =
                "【弩必定额外伤害 (%)】\n" +
                "弩攻击时必定附加额外伤害。\n" +
                "每次攻击固定生效。\n" +
                "推荐：12-20%",

                ["Tier2_CrossbowSpec_CrossbowDamage"] =
                "【弩额外伤害（固定）】\n" +
                "触发额外伤害时的固定附加伤害值。\n" +
                "推荐：7-13",

                ["Tier2_StaffSpec_ElementalTriggerChance"] =
                "【法杖必定额外伤害 (%)】\n" +
                "法杖攻击时必定附加额外伤害。\n" +
                "每次攻击固定生效。\n" +
                "推荐：15-25%",

                ["Tier2_StaffSpec_StaffDamage"] =
                "【法杖额外伤害（固定）】\n" +
                "触发额外伤害时的固定附加伤害值。\n" +
                "推荐：6-12",

                ["Tier1_BaseAttack_PhysicalDamageBonus"] =
                "【物理伤害加成（固定）】\n" +
                "为所有武器增加固定物理伤害值。\n" +
                "推荐：1-3",

                ["Tier1_BaseAttack_ElementalDamageBonus"] =
                "【元素伤害加成（固定）】\n" +
                "增加固定元素伤害（火焰、冰霜、闪电）。\n" +
                "推荐：1-3",

                ["Tier3_AttackBoost_PhysicalDamageBonus"] =
                "【双手武器物理伤害加成 (%)】\n" +
                "提升双手武器的物理伤害。\n" +
                "推荐：8-15%",

                ["Tier3_AttackBoost_ElementalDamageBonus"] =
                "【双手武器元素伤害加成 (%)】\n" +
                "提升双手武器的元素伤害。\n" +
                "推荐：8-15%",

                ["Tier4_PrecisionAttack_CritChance"] =
                "【暴击率加成 (%)】\n" +
                "提升所有攻击的暴击率。\n" +
                "推荐：3-8%",

                ["Tier4_MeleeEnhance_2HitComboBonus"] =
                "【2连击加成 (%)】\n" +
                "连续两次近战攻击时提升伤害。\n" +
                "推荐：8-15%",

                ["Tier4_RangedEnhance_RangedDamageBonus"] =
                "【远程伤害加成（固定）】\n" +
                "增加远程武器（弓、弩）的固定伤害值。\n" +
                "推荐：3-8",

                ["Tier5_SpecialStat_SpecBonus"] =
                "【体力恢复】\n" +
                "命中时恢复的体力百分比。\n" +
                "推荐：3-10",

                ["Tier5_Charge_TriggerChance"] =
                "【触发概率】\n" +
                "命中时触发体力恢复的概率。\n" +
                "推荐：20-50",

                ["Tier6_WeakPointAttack_CritDamageBonus_Lv1"] = "【弱点攻击 Lv1 暴击伤害加成 (%)】\n达到1级时生效的暴击伤害增幅。\n推荐：5%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv2"] = "【弱点攻击 Lv2 暴击伤害加成 (%)】\n达到2级时生效的暴击伤害增幅。\n推荐：9%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv3"] = "【弱点攻击 Lv3 暴击伤害加成 (%)】\n达到3级时生效的暴击伤害增幅。\n推荐：13%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv4"] = "【弱点攻击 Lv4 暴击伤害加成 (%)】\n达到4级时生效的暴击伤害增幅。\n推荐：17%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv5"] = "【弱点攻击 Lv5 暴击伤害加成 (%)】\n达到5级时生效的暴击伤害增幅。\n推荐：21%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv6"] = "【弱点攻击 Lv6 暴击伤害加成 (%)】\n达到6级时生效的暴击伤害增幅。\n推荐：25%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv7"] = "【弱点攻击 Lv7（最高）暴击伤害加成 (%)】\n达到7级（最高）时生效的暴击伤害增幅。\n推荐：29%",

                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] =
                "【双手武器伤害加成 (%)】\n" +
                "提升双手武器的总伤害。\n" +
                "推荐：8-15%",

                ["Tier6_ElementalAttack_ElementalBonus"] =
                "【法杖元素伤害加成 (%)】\n" +
                "提升法杖的元素伤害（火焰、冰霜、闪电）。\n" +
                "推荐：8-15%",

                ["Tier6_ComboFinisher_3HitComboBonus"] =
                "【3连击组合终结加成 (%)】\n" +
                "提升3连击中最后一击的伤害。\n" +
                "推荐：12-20%",

                // ======================================== [新: 攻击系统 4阶段]
                ["Tier1_Opener_DamageBonus"] =
                "【先手攻击伤害加成 (%)】\n" +
                "战斗开始后数秒内伤害提升。\n" +
                "推荐：15-25%",

                ["Tier1_Opener_StaminaReduction"] =
                "【体力消耗削减 (%)】\n" +
                "先手攻击阶段期间减少体力消耗。\n" +
                "推荐：20-30%",

                ["Tier1_Opener_Duration"] =
                "【先手攻击持续时间 (秒)】\n" +
                "战斗开始后先手攻击效果的持续时间。\n" +
                "推荐：4-6 秒",

                ["Tier1_Opener_Cooldown"] =
                "【冷却时间 (秒)】\n" +
                "先手攻击效果再次激活前的等待时间。\n" +
                "推荐：25-35 秒",

                ["Tier2_OpenerMelee_FinisherBonus"] =
                "【近战终结加成 (%)】\n" +
                "战斗开始后首次攻击命中后提升终结倍率。\n" +
                "推荐：15-25%",

                ["Tier2_OpenerBow_CritChance"] =
                "【弓 猎人之眼 暴击伤害 (%)】\n" +
                "先攻窗口内第一支箭必定暴击时额外增加的暴击伤害。\n" +
                "推荐：6-10%",

                ["Tier2_OpenerCrossbow_FirstShotBonus"] =
                "【弩首发加成 (%)】\n" +
                "战斗开始后第一发弩矢伤害提升。\n" +
                "推荐：40-60%",

                ["Tier2_OpenerMagic_StaggerProc"] =
                "【魔法硬直触发 (0/1)】\n" +
                "战斗开始后首次魔法攻击必定触发硬直。\n" +
                "0 = 禁用, 1 = 启用",

                ["Tier3_Pursuit_DamageBonus"] =
                "【追击伤害加成 (%)】\n" +
                "对移动中或逃跑中的敌人造成更多伤害。\n" +
                "推荐：12-18%",

                ["Tier3_Pursuit_ChainDamageBonus"] =
                "【追击连锁加成 (%)】\n" +
                "先手攻击连锁触发后追击伤害进一步提升。\n" +
                "推荐：20-30%",

                ["Tier3_Pursuit_ChainWindow"] =
                "【连锁时间窗口 (秒)】\n" +
                "先手攻击后追击连锁的有效时间。\n" +
                "推荐：4-6 秒",

                ["Tier4_PursuitSpeed_SpeedBonus"] =
                "【移动速度加成 (%)】\n" +
                "战斗中提升移动速度。\n" +
                "推荐：10-15%",

                ["Tier4_FrenzyTrigger_CritChancePerLevel"] =
                "【致命一击暴击率增量（每级%）】\n" +
                "每升一级增加的暴击率。（等级 × 增量）\n" +
                "推荐：1-3%",

                ["Tier5_Frenzy_StackBonusBase"] =
                "【混战层数基础加成 (%)】\n" +
                "无连锁时每层的伤害加成。\n" +
                "推荐：4-6%",

                ["Tier5_Frenzy_StackBonusChain"] =
                "【混战连锁层数加成 (%)】\n" +
                "追击连锁激活时每层的强化伤害加成。\n" +
                "推荐：7-10%",

                ["Tier5_Frenzy_MaxStacks"] =
                "【最大层数】\n" +
                "混战层数的最大值。\n" +
                "推荐：4-6",

                ["Tier5_Frenzy_HitsPerStack"] =
                "【每层所需命中数】\n" +
                "积累一层所需的命中次数。\n" +
                "推荐：2-4",

                ["Tier5_Frenzy_Tier6Amplifier"] =
                "【满层时Tier6增幅倍率 (×)】\n" +
                "达到最大层数时所有Tier6效果的倍率。\n" +
                "推荐：1.2-1.4",

                // === 新4阶段系统所需点数 ===
                ["Tier1_Opener_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
                ["Tier2_OpenerMelee_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
                ["Tier2_OpenerBow_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
                ["Tier2_OpenerCrossbow_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
                ["Tier2_OpenerMagic_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
                ["Tier3_Pursuit_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
                ["Tier4_PursuitSpeed_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
                ["Tier4_FrenzyTrigger_PointsPerLevel"] = "【每级所需点数】\n每次升级消耗的技能点数。\n推荐：2",
                ["Tier5_Frenzy_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                // ========================================
                // 防御树 (Defense Tree)
                // ========================================
                ["Tier0_DefenseExpert_HPBonus"] =
                "【生命值加成（固定）】\n" +
                "增加固定最大生命值。\n" +
                "推荐：3-8",

                ["Tier0_DefenseExpert_ArmorBonus"] =
                "【护甲加成（固定）】\n" +
                "增加固定护甲值。\n" +
                "推荐：1-4",

                ["Tier0_DefenseExpert_AtkPenalty"] =
                "【攻击力降低（%）】\n" +
                "学习防御专家会小幅降低攻击力。\n" +
                "这是防御与攻击之间的权衡。\n" +
                "推荐：1-3%",

                ["Tier1_SkinHardening_HPBonus"] =
                "【生命值加成（固定）】\n" +
                "额外增加最大生命值。\n" +
                "推荐：3-8",

                ["Tier1_SkinHardening_ArmorBonus"] =
                "【护甲加成（固定）】\n" +
                "额外增加护甲值。\n" +
                "推荐：3-8",

                ["Tier2_MindBodyTraining_StaminaBonus"] =
                "【最大体力加成（固定）】\n" +
                "增加最大体力值。\n" +
                "推荐：20-30",

                ["Tier2_MindBodyTraining_EitrBonus"] =
                "【最大以特尔加成（固定）】\n" +
                "增加用于魔法攻击的最大以特尔值。\n" +
                "推荐：20-30",

                ["Tier2_HealthTraining_HPBonus"] =
                "【生命值加成（固定）】\n" +
                "大幅增加最大生命值。\n" +
                "推荐：15-25",

                ["Tier2_HealthTraining_ArmorBonus"] =
                "【护甲加成（固定）】\n" +
                "额外增加护甲值。\n" +
                "推荐：3-8",

                ["Tier3_CoreBreathing_EitrBonus"] =
                "【以特尔加成（固定）】\n" +
                "通过冥想增加以特尔值。\n" +
                "推荐：8-15",

                ["Tier3_EvasionTraining_DodgeBonus"] =
                "【闪避加成 (%)】\n" +
                "提升闪避敌方攻击的概率。\n" +
                "推荐：3-8%",

                ["Tier3_EvasionTraining_InvincibilityBonus"] =
                "【无敌时间（翻滚）(%)】\n" +
                "延长翻滚时的无敌持续时间。\n" +
                "推荐：15-25%",

                ["Tier3_HealthBoost_HPBonus"] =
                "【生命值加成（固定）】\n" +
                "额外增加生命值。\n" +
                "推荐：12-20",

                ["Tier3_ShieldTraining_BlockPowerBonus"] =
                "【格挡力加成（固定）】\n" +
                "增加盾牌的格挡力。\n" +
                "推荐：80-120",

                ["Tier3_BlockTraining_ParryBlockPowerRatio"] =
                "【弹反反击格挡力比率 (%)】\n" +
                "弹反成功时反击伤害 = 格挡力 × 比率 / 100。\n" +
                "推荐：80-150%",

                ["Tier3_BlockTraining_PushDistance"] =
                "【弹反反击击退距离 (m)】\n" +
                "弹反反击成功时敌人被击退的距离。\n" +
                "推荐：3-6m",

                ["Tier4_GroundStomp_Radius"] =
                "【效果范围 (m)】\n" +
                "震地踏冲击波的效果半径。\n" +
                "推荐：2.5-4 m",

                ["Tier4_GroundStomp_KnockbackForce"] =
                "【击退力】\n" +
                "击退敌人的力度。\n" +
                "推荐：15-25",

                ["Tier4_GroundStomp_Cooldown"] =
                "【冷却时间（秒）】\n" +
                "再次使用前的等待时间。\n" +
                "推荐：100-150 秒",

                ["Tier4_GroundStomp_HPThreshold"] =
                "【生命值阈值（自动触发）】\n" +
                "生命值低于此值时自动激活。\n" +
                "0.35 = 生命值的35%\n" +
                "推荐：0.30-0.40",

                ["Tier4_GroundStomp_VFXDuration"] =
                "【视觉效果持续时间（秒）】\n" +
                "视觉效果的显示持续时间。\n" +
                "推荐：0.8-1.5 秒",

                ["Tier4_RockSkin_ArmorBonus"] =
                "【护甲强化 (%)】\n" +
                "对头盔、胸甲、腿甲和盾牌施加百分比护甲加成。\n" +
                "推荐：10-15%",

                ["Tier5_Endurance_RunStaminaReduction"] =
                "【跑步体力消耗 (%)】\n" +
                "减少跑步时的体力消耗。\n" +
                "推荐：8-15%",

                ["Tier5_Endurance_JumpStaminaReduction"] =
                "【跳跃体力消耗 (%)】\n" +
                "减少跳跃时的体力消耗。\n" +
                "推荐：8-15%",

                ["Tier5_Agility_DodgeBonus"] =
                "【闪避加成 (%)】\n" +
                "额外提升闪避概率。\n" +
                "推荐：3-8%",

                ["Tier5_Agility_RollStaminaReduction"] =
                "【翻滚体力消耗 (%)】\n" +
                "减少翻滚时的体力消耗。\n" +
                "推荐：10-18%",

                ["Tier5_TrollRegen_HPRegenBonus"] =
                "【生命值恢复加成（每秒）】\n" +
                "像巨魔一样自动恢复生命值。\n" +
                "推荐：3-8",

                ["Tier5_TrollRegen_RegenInterval"] =
                "【恢复间隔（秒）】\n" +
                "生命值恢复的时间间隔。\n" +
                "推荐：1.5-3 秒",

                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] =
                "【格挡力加成（固定）】\n" +
                "大幅增加盾牌的格挡力。\n" +
                "推荐：80-120",

                ["Tier5_BlockMaster_ParryDurationBonus"] =
                "【招架持续时间加成（秒）】\n" +
                "延长成功招架后的效果持续时间。\n" +
                "推荐：0.8-1.5 秒",

                ["Tier6_NerveEnhancement_DodgeBonus"] =
                "【条件闪避加成（30秒，%)】\n" +
                "连续30秒未闪避时激活。\n" +
                "推荐：85%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【生命值加成 (%)】\n" +
                "按百分比提升最大生命值。\n" +
                "推荐：25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【物理/元素抗性 (%)】\n" +
                "减少所有物理和元素伤害。\n" +
                "推荐：8-15%",

                // ========================================
                // 生产树 (Production Tree)
                // ========================================
                ["Tier0_ProductionExpert_WoodBonusChance"] =
                "【木材+1概率 (%)】\n" +
                "砍伐时额外获得木材的概率。\n" +
                "推荐：40-60%",

                ["Tier0_ProductionExpert_RequiredPoints"] =
                "【所需点数 - 生产专家】\n" +
                "解锁生产专家所需的技能点数。\n" +
                "推荐：2",

                ["Tier1_NoviceWorker_WoodBonusChance"] =
                "【木材+1概率 (%)】\n" +
                "提升砍伐时额外获得木材的概率。\n" +
                "推荐：20-30%",

                ["Tier1_NoviceWorker_RequiredPoints"] =
                "【所需点数 - 新手工人】\n" +
                "解锁新手工人所需的技能点数。\n" +
                "推荐：2",

                ["Tier2_WoodcuttingLv2_BonusChance"] =
                "【木材+1概率 (%)】\n" +
                "砍伐 Lv.2 - 额外获得木材的概率。\n" +
                "推荐：20-30%",

                ["Tier2_WoodcuttingLv2_RequiredPoints"] =
                "【所需点数 - 砍伐 Lv.2】\n" +
                "推荐：2",

                ["Tier2_GatheringLv2_BonusChance"] =
                "【物品+1概率 (%)】\n" +
                "采集 Lv.2 - 额外获得物品的概率。\n" +
                "推荐：20-30%",

                ["Tier2_GatheringLv2_RequiredPoints"] =
                "【所需点数 - 采集 Lv.2】\n" +
                "推荐：2",

                ["Tier2_MiningLv2_BonusChance"] =
                "【矿石+1概率 (%)】\n" +
                "采矿 Lv.2 - 额外获得矿石的概率。\n" +
                "推荐：20-30%",

                ["Tier2_MiningLv2_RequiredPoints"] =
                "【所需点数 - 采矿 Lv.2】\n" +
                "推荐：2",

                ["Tier2_CraftingLv2_UpgradeChance"] =
                "【强化+1概率 (%)】\n" +
                "制作 Lv.2 - 额外提升一级强化等级的概率。\n" +
                "推荐：20-30%",

                ["Tier2_CraftingLv2_RequiredPoints"] =
                "【所需点数 - 制作 Lv.2】\n" +
                "推荐：2",

                ["Tier2_CraftingLv2_DurabilityBonus"] =
                "【最大耐久度 (%)】\n" +
                "制作 Lv.2 - 提升制作物品的最大耐久度。\n" +
                "推荐：20-30%",

                ["Tier3_WoodcuttingLv3_BonusChance"] =
                "【木材+2概率 (%)】\n" +
                "砍伐 Lv.3 - 额外获得2个木材的概率。\n" +
                "推荐：30-40%",

                ["Tier3_WoodcuttingLv3_RequiredPoints"] =
                "【所需点数 - 砍伐 Lv.3】\n" +
                "推荐：2",

                ["Tier3_GatheringLv3_BonusChance"] =
                "【物品+1概率 (%)】\n" +
                "采集 Lv.3 - 提升额外获得物品的概率。\n" +
                "推荐：20-30%",

                ["Tier3_GatheringLv3_RequiredPoints"] =
                "【所需点数 - 采集 Lv.3】\n" +
                "推荐：2",

                ["Tier3_MiningLv3_BonusChance"] =
                "【矿石+1概率 (%)】\n" +
                "采矿 Lv.3 - 提升额外获得矿石的概率。\n" +
                "推荐：20-30%",

                ["Tier3_MiningLv3_RequiredPoints"] =
                "【所需点数 - 采矿 Lv.3】\n" +
                "推荐：2",

                ["Tier3_CraftingLv3_UpgradeChance"] =
                "【强化+1概率 (%)】\n" +
                "制作 Lv.3 - 提升强化概率。\n" +
                "推荐：20-30%",

                ["Tier3_CraftingLv3_RequiredPoints"] =
                "【所需点数 - 制作 Lv.3】\n" +
                "推荐：2",

                ["Tier3_CraftingLv3_DurabilityBonus"] =
                "【最大耐久度 (%)】\n" +
                "制作 Lv.3 - 额外提升耐久度。\n" +
                "推荐：20-30%",

                ["Tier4_WoodcuttingLv4_BonusChance"] =
                "【木材+2概率 (%)】\n" +
                "砍伐 Lv.4 - 额外获得木材的最大概率。\n" +
                "推荐：40-50%",

                ["Tier4_WoodcuttingLv4_RequiredPoints"] =
                "【所需点数 - 砍伐 Lv.4】\n" +
                "推荐：2",

                ["Tier4_GatheringLv4_BonusChance"] =
                "【物品+1概率 (%)】\n" +
                "采集 Lv.4 - 额外获得物品的最大概率。\n" +
                "推荐：20-30%",

                ["Tier4_GatheringLv4_RequiredPoints"] =
                "【所需点数 - 采集 Lv.4】\n" +
                "推荐：2",

                ["Tier4_MiningLv4_BonusChance"] =
                "【矿石+1概率 (%)】\n" +
                "采矿 Lv.4 - 额外获得矿石的最大概率。\n" +
                "推荐：20-30%",

                ["Tier4_MiningLv4_RequiredPoints"] =
                "【所需点数 - 采矿 Lv.4】\n" +
                "推荐：2",

                ["Tier4_CraftingLv4_UpgradeChance"] =
                "【强化+1概率 (%)】\n" +
                "制作 Lv.4 - 最大强化概率。\n" +
                "推荐：20-30%",

                ["Tier4_CraftingLv4_RequiredPoints"] =
                "【所需点数 - 制作 Lv.4】\n" +
                "推荐：2",

                ["Tier4_CraftingLv4_DurabilityBonus"] =
                "【最大耐久度 (%)】\n" +
                "制作 Lv.4 - 最大耐久度提升。\n" +
                "推荐：20-30%",

                // ========================================
                // 速度树 (Speed Tree)
                // ========================================
                ["Tier0_SpeedExpert_MoveSpeedBonus"] =
                "【移动速度加成 (%)】\n" +
                "永久提升移动速度。\n" +
                "推荐：5-10%",

                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] =
                "【闪避后速度加成 (%)】\n" +
                "翻滚后短暂提升移动速度。\n" +
                "推荐：10-20%",

                ["Tier1_AgilityBase_BuffDuration"] =
                "【效果持续时间（秒）】\n" +
                "翻滚后速度加成的持续时间。\n" +
                "推荐：2-3 秒",

                ["Tier1_AgilityBase_AttackSpeedBonus"] =
                "【攻击速度加成 (%)】\n" +
                "提升所有武器的整体攻击速度。\n" +
                "推荐：3-8%",

                ["Tier1_AgilityBase_DodgeSpeedBonus"] =
                "【闪避速度加成 (%)】\n" +
                "提升翻滚动作的动画速度。\n" +
                "推荐：5-15%",

                ["Tier3_BlockTraining_MaxChargeDistance"] =
                "【反击最大有效距离 (m)】\n" +
                "仅当被击晕的怪物在此距离内时反击才会触发。\n" +
                "推荐：6-10m",

                ["Tier0_DefenseExpert_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier1_SkinHardening_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier2_MindTraining_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier2_HealthTraining_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier3_CoreBreathing_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier3_EvasionTraining_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier3_HealthBoost_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier3_ShieldTraining_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier4_Shockwave_Radius"] =
                "【冲击波范围】\n" +
                "震地踏冲击波技能的效果半径（米）。\n" +
                "推荐：3",

                ["Tier4_Shockwave_StunDuration"] =
                "【冲击波晕眩时间】\n" +
                "晕眩效果的持续时间（秒）。\n" +
                "推荐：1",

                ["Tier4_Shockwave_Cooldown"] =
                "【冲击波冷却时间】\n" +
                "技能再使用的等待时间（秒）。\n" +
                "推荐：120",

                ["Tier4_Shockwave_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier4_Shockwave_KnockbackForce"] =
                "【击退力】\n" +
                "冲击波触发时施加给敌人的力度。\n" +
                "推荐：15-25",

                ["Tier4_GroundStomp_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier4_RockSkin_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier5_Endurance_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier5_Agility_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier5_TrollRegen_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier5_BlockMaster_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier6_MindShield_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier6_MindShield_Cooldown"] =
                "【精神护盾冷却时间】\n" +
                "G键精神护盾技能的再使用等待时间（秒）。\n" +
                "默认：210（3分30秒）",

                ["Tier6_MindShield_EitrCost"] =
                "【精神护盾以特尔消耗】\n" +
                "施放精神护盾时消耗的以特尔数量。\n" +
                "默认：30",

                ["Tier6_MindShield_Duration"] =
                "【精神护盾持续时间】\n" +
                "护盾维持的时间（秒）。期间最多吸收相当于以特尔上限的生命值伤害。\n" +
                "默认：180（3分钟）",

                ["Tier6_NerveEnhancement_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier6_DoubleJump_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier6_JotunnVitality_RequiredPoints"] =
                "【所需点数】\n" +
                "解锁此节点所需的技能点数。\n" +
                "推荐：2",

                ["Tier0_SpeedExpert_MoveSpeedPerLevel"] = "【移动速度加成/级 (%)】\n每级提升的移动速度增加量。（Lv1~7 成长系统）\n推荐：1-5%",

                ["Tier0_SpeedExpert_PointsPerLevel"] = "【每级所需点数】\n每次升级消耗的技能点数。\n推荐：1-2",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_2"] = "【Lv2所需玩家等级】\n升级到Lv2所需的EpicMMO玩家等级。（0=无）\n默认：15",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_3"] = "【Lv3所需玩家等级】\n升级到Lv3所需的EpicMMO玩家等级。\n默认：30",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_4"] = "【Lv4所需玩家等级】\n升级到Lv4所需的EpicMMO玩家等级。\n默认：45",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_5"] = "【Lv5所需玩家等级】\n升级到Lv5所需的EpicMMO玩家等级。\n默认：50",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_6"] = "【Lv6所需玩家等级】\n升级到Lv6所需的EpicMMO玩家等级。\n默认：65",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_7"] = "【Lv7所需玩家等级】\n升级到Lv7所需的EpicMMO玩家等级。\n默认：80",

                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "【2连击攻击速度加成 (%)】\n近战连续命中2次后攻击速度提升。\n推荐：8-15%",

                ["Tier2_MeleeFlow_StaminaReduction"] = "【体力消耗减少 (%)】\n流畅打击增益期间体力消耗减少。\n推荐：10-20%",

                ["Tier2_MeleeFlow_Duration"] = "【增益持续时间（秒）】\n近战流畅打击增益的持续时间。\n推荐：3-5秒",

                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "【连击速度加成 (%)】\n连招链中额外的攻击速度加成。\n推荐：5-10%",

                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "【命中后移动速度加成 (%)】\n弩矢命中敌人时移动速度提升。\n推荐：10-15%",

                ["Tier2_CrossbowExpert_BuffDuration"] = "【增益持续时间（秒）】\n命中后速度增益的持续时间。\n推荐：3-5秒",

                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "【增益期间装填速度加成 (%)】\n命中增益生效期间装填速度提升。\n推荐：10-15%",

                ["Tier2_BowExpert_StaminaReduction"] = "【2连击体力消耗减少 (%)】\n连续命中2箭后体力消耗减少。\n推荐：10-15%",

                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "【下一箭拉弦速度加成 (%)】\n连击成功后下一箭的拉弦速度提升。\n推荐：10-20%",

                ["Tier2_BowExpert_BuffDuration"] = "【增益持续时间（秒）】\n连击增益的持续时间。\n推荐：4-6秒",

                ["Tier2_MobileCast_MoveSpeedBonus"] = "【施法中移动速度加成 (%)】\n施放法杖法术时的移动速度加成。\n推荐：8-12%",

                ["Tier2_MobileCast_EitrReduction"] = "【以特尔消耗减少 (%)】\n法杖法术的以特尔消耗减少。\n推荐：8-15%",

                ["Tier2_MobileCast_CastMoveSpeed"] = "【法杖施法中移动速度 (%)】\n引导法杖攻击时的基础移动速度。\n推荐：3-6%",

                ["Tier3_Practitioner1_MeleeSkillBonus"] = "【近战武器技能加成】\n提升所有近战武器技能等级。\n推荐：5-10",

                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "【弩技能加成】\n提升弩技能等级。\n推荐：5-10",

                ["Tier3_Practitioner2_StaffSkillBonus"] = "【法杖技能加成】\n提升法杖技能等级（元素法术）。\n推荐：5-10",

                ["Tier3_Practitioner2_BowSkillBonus"] = "【弓技能加成】\n提升弓技能等级。\n推荐：5-10",

                ["Tier4_Energizer_FoodConsumptionReduction"] = "【食物消耗速率减少 (%)】\n降低食物消耗速度，使增益持续更久。\n推荐：10-20%",

                ["Tier4_Captain_ShipSpeedBonus"] = "【船速加成 (%)】\n提升航行速度。\n推荐：10-20%",

                ["Tier5_JumpMaster_JumpSkillBonus"] = "【跳跃技能加成】\n提升跳跃技能等级。\n推荐：5-15",

                ["Tier5_JumpMaster_JumpStaminaReduction"] = "【跳跃体力消耗减少 (%)】\n减少跳跃时的体力消耗。\n推荐：10-20%",

                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "【近战攻击速度加成 (%)】\n提升近战武器攻击速度。\n推荐：5-8%",

                ["Tier6_Dexterity_MoveSpeedBonus"] = "【移动速度加成 (%)】\n提升整体移动速度。\n推荐：3-8%",

                ["Tier6_Endurance_StaminaMaxBonus"] = "【最大体力加成】\n增加最大体力上限。\n推荐：20-40",

                ["Tier6_Intellect_EitrMaxBonus"] = "【最大以特尔加成】\n增加用于魔法的最大以特尔上限。\n推荐：30-50",

                ["Tier7_Master_RunSkillBonus"] = "【跑步技能加成】\n提升跑步技能等级。\n推荐：5-15",

                ["Tier7_Master_JumpSkillBonus"] = "【跳跃技能加成】\n提升跳跃技能等级。\n推荐：5-15",

                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "【近战攻击速度加成 (%)】\n近战攻击速度的最终提升。\n推荐：5-10%",

                ["Tier8_MeleeAccel_TripleComboBonus"] = "【3连击后下一击速度加成 (%)】\n3连击后下一次攻击的速度大幅提升。\n推荐：20-30%",

                ["Tier8_CrossbowAccel_ReloadSpeed"] = "【装填速度加成 (%)】\n弩装填速度的最终提升。\n推荐：25-35%",

                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "【装填中移动速度 (%)】\n装填弩时的移动速度。\n推荐：20-30%",

                ["Tier8_BowAccel_DrawSpeed"] = "【拉弦速度加成 (%)】\n弓拉弦速度的最终提升。\n推荐：15-20%",

                ["Tier8_BowAccel_DrawMoveSpeed"] = "【拉弦中移动速度 (%)】\n拉弓弦时的移动速度。\n推荐：10-20%",

                ["Tier8_CastAccel_MagicAttackSpeed"] = "【魔法攻击速度加成 (%)】\n魔法攻击速度的最终提升。\n推荐：5-10%",

                ["Tier8_CastAccel_TripleEitrRecovery"] = "【3连击以特尔最大恢复率 (%)】\n3次法术连击后以特尔恢复速度提升。\n推荐：10-15%",

                ["Tier1_AgilityBase_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_MeleeFlow_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_CrossbowExpert_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_BowExpert_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_MobileCast_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier3_Practitioner1_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier3_Practitioner2_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier4_Energizer_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier4_Captain_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier5_JumpMaster_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier6_Dexterity_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier6_Endurance_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier6_Intellect_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier7_Master_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier8_MeleeAccel_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier8_CrossbowAccel_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier8_BowAccel_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier8_CastAccel_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier0_AttackExpert_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier1_BaseAttack_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_MeleeSpec_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_BowSpec_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_CrossbowSpec_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier2_StaffSpec_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier3_AttackBoost_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier4_MeleeEnhance_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier4_PrecisionAttack_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier4_RangedEnhance_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier5_SpecialStat_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier6_WeakPointAttack_PointsPerLevel"] = "【每级所需点数】\n每次升级消耗的技能点数。\n推荐：2",

                ["Tier6_ComboFinisher_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier6_TwoHandCrush_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

                ["Tier6_ElementalAttack_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",

            };
        }
    }
}

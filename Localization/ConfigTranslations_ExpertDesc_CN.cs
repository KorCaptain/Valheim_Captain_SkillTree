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
                "  HardMode     = CLLC极难 + 怪物HP×2（强化数值）\n" +
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

                ["Tier6_WeakPointAttack_CritDamageBonus"] =
                "【暴击伤害加成 (%)】\n" +
                "提升暴击时造成的额外伤害。\n" +
                "推荐：10-20%",

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
                "【弓暴击率 (%)】\n" +
                "战斗开始后第一支箭的暴击率提升。\n" +
                "推荐：10-20%",

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

                ["Tier4_FrenzyTrigger_StaminaReduction"] =
                "【混战体力削减 (%)】\n" +
                "附近有多个敌人时减少体力消耗。\n" +
                "推荐：15-25%",

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
                ["Tier4_FrenzyTrigger_RequiredPoints"] = "【所需点数】\n解锁此节点所需的技能点数。",
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
                "推荐：30-50%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【生命值加成 (%)】\n" +
                "按百分比提升最大生命值。\n" +
                "推荐：25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【物理/元素抗性 (%)】\n" +
                "减少所有物理和元素伤害。\n" +
                "推荐：8-15%",

                ["Tier6_JotunnShield_BlockStaminaReduction"] =
                "【格挡体力消耗 (%)】\n" +
                "减少使用盾牌格挡时的体力消耗。\n" +
                "推荐：20-30%",

                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] =
                "【持普通盾移动速度 (%)】\n" +
                "提升持普通盾时的移动速度。\n" +
                "推荐：3-8%",

                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] =
                "【持塔盾移动速度 (%)】\n" +
                "提升持塔盾时的移动速度。\n" +
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
            };
        }
    }
}

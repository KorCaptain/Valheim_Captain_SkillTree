using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetChineseKeyNames()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetChineseKeyNames_Part1()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetChineseKeyNames_Part2()) dict[kv.Key] = kv.Value;
            return dict;
        }

        private static Dictionary<string, string> GetChineseKeyNames_Part1()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Skill_Tree_Base - 按键绑定
                // ============================================
                ["HotKey_Y"] = "职业技能键",
                ["HotKey_R"] = "远程技能键",
                ["HotKey_G"] = "近战主技能键",
                ["HotKey_H"] = "副技能键",
                ["HUD_IconSize"] = "技能图标大小",
                ["HUD_PosX"] = "技能图标 HUD X位置",
                ["HUD_PosY"] = "技能图标 HUD Y位置",
                ["PassiveMessageDisplay"] = "被动消息显示",
                ["GameDifficulty"] = "游戏难度",

                // ============================================
                // 攻击树 - 33个键
                // ============================================

                // === Tier 0: 攻击专家 (2) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: [攻击专家] 总伤害加成 (%)",
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: [攻击专家] 所需点数",

                // === 新4阶段连锁系统 (Tier1~5) ===
                ["Tier1_Opener_DamageBonus"] = "Tier 1: [先发制人] 伤害加成 (%)",
                ["Tier1_Opener_StaminaReduction"] = "Tier 1: [先发制人] 体力消耗减少 (%)",
                ["Tier1_Opener_Duration"] = "Tier 1: [先发制人] 持续时间 (秒)",
                ["Tier1_Opener_Cooldown"] = "Tier 1: [先发制人] 冷却时间 (秒)",
                ["Tier1_Opener_RequiredPoints"] = "Tier 1: [先发制人] 所需点数",
                ["Tier2_OpenerMelee_FinisherBonus"] = "Tier 2-1: [近战] 终结加成 (%)",
                ["Tier2_OpenerMelee_RequiredPoints"] = "Tier 2-1: [近战] 所需点数",
                ["Tier2_OpenerBow_CritChance"] = "Tier 2-2: [弓] 暴击率 (%)",
                ["Tier2_OpenerBow_RequiredPoints"] = "Tier 2-2: [弓] 所需点数",
                ["Tier2_OpenerCrossbow_FirstShotBonus"] = "Tier 2-3: [弩] 首发加成 (%)",
                ["Tier2_OpenerCrossbow_RequiredPoints"] = "Tier 2-3: [弩] 所需点数",
                ["Tier2_OpenerMagic_StaggerProc"] = "Tier 2-4: [魔法] 击晕触发 (1=启用)",
                ["Tier2_OpenerMagic_RequiredPoints"] = "Tier 2-4: [魔法] 所需点数",
                ["Tier3_Pursuit_DamageBonus"] = "Tier 3: [追击] 基础伤害加成 (%)",
                ["Tier3_Pursuit_ChainDamageBonus"] = "Tier 3: [追击] 连锁伤害加成 (%)",
                ["Tier3_Pursuit_ChainWindow"] = "Tier 3: [追击] 连锁窗口 (秒)",
                ["Tier3_Pursuit_RequiredPoints"] = "Tier 3: [追击] 所需点数",
                ["Tier4_PursuitSpeed_SpeedBonus"] = "Tier 4-1: [疾风追击] 移动速度加成 (%)",
                ["Tier4_PursuitSpeed_RequiredPoints"] = "Tier 4-1: [疾风追击] 所需点数",
                ["Tier4_FrenzyTrigger_StaminaReduction"] = "Tier 4-2: [混战冲入] 体力减少 (%)",
                ["Tier4_FrenzyTrigger_RequiredPoints"] = "Tier 4-2: [混战冲入] 所需点数",
                ["Tier5_Frenzy_StackBonusBase"] = "Tier 5: [混战] 基础叠加加成 (%)",
                ["Tier5_Frenzy_StackBonusChain"] = "Tier 5: [混战] 连锁叠加加成 (%)",
                ["Tier5_Frenzy_MaxStacks"] = "Tier 5: [混战] 最大叠加层数",
                ["Tier5_Frenzy_HitsPerStack"] = "Tier 5: [混战] 每层所需命中数",
                ["Tier5_Frenzy_Tier6Amplifier"] = "Tier 5: [混战] 满层时Tier6增幅倍率",
                ["Tier5_Frenzy_RequiredPoints"] = "Tier 5: [混战] 所需点数",

                // === Tier 1: 基础攻击 (3) ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] = "Tier 1: [基础攻击] 物理伤害加成 (%)",
                ["Tier1_BaseAttack_ElementalDamageBonus"] = "Tier 1: [基础攻击] 元素伤害加成 (%)",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: [基础攻击] 所需点数",

                // === Tier 2: 武器专精 (12) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2-1: [近战专精] 必定加成 (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2-1: [近战专精] 额外伤害",
                ["Tier2_MeleeSpec_RequiredPoints"] = "Tier 2-1: [近战专精] 所需点数",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2-2: [弓专精] 必定加成 (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2-2: [弓专精] 额外伤害",
                ["Tier2_BowSpec_RequiredPoints"] = "Tier 2-2: [弓专精] 所需点数",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2-3: [弩专精] 必定加成 (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2-3: [弩专精] 额外伤害",
                ["Tier2_CrossbowSpec_RequiredPoints"] = "Tier 2-3: [弩专精] 所需点数",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2-4: [法杖专精] 必定加成 (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2-4: [法杖专精] 额外伤害",
                ["Tier2_StaffSpec_RequiredPoints"] = "Tier 2-4: [法杖专精] 所需点数",

                // === Tier 3: 攻击强化 (3) ===
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: [攻击强化] 物理伤害加成 (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: [攻击强化] 元素伤害加成 (%)",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: [攻击强化] 所需点数",

                // === Tier 4: 战斗强化 (6) ===
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4-1: [近战强化] 2连击加成 (%)",
                ["Tier4_MeleeEnhance_RequiredPoints"] = "Tier 4-1: [近战强化] 所需点数",
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4-2: [精准攻击] 暴击率 (%)",
                ["Tier4_PrecisionAttack_RequiredPoints"] = "Tier 4-2: [精准攻击] 所需点数",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4-3: [远程强化] 远程伤害加成 (%)",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4-3: [远程强化] 所需点数",

                // === Tier 5: 充能 (3) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: [充能] 体力恢复 (%)",
                ["Tier5_Charge_TriggerChance"] = "Tier 5: [充能] 触发概率 (%)",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: [充能] 所需点数",

                // === Tier 6: 终极强化 (8) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6-1: [弱点] 暴击伤害加成 (%)",
                ["Tier6_WeakPointAttack_RequiredPoints"] = "Tier 6-1: [弱点] 所需点数",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6-2: [连击终结] 必定加成 (%)",
                ["Tier6_ComboFinisher_RequiredPoints"] = "Tier 6-2: [连击终结] 所需点数",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6-3: [双手重击] 双手武器伤害加成 (%)",
                ["Tier6_TwoHandCrush_RequiredPoints"] = "Tier 6-3: [双手重击] 所需点数",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6-4: [元素攻击] 元素加成 (%)",
                ["Tier6_ElementalAttack_RequiredPoints"] = "Tier 6-4: [元素攻击] 所需点数",

                // ============================================
                // 速度树 - 49个键
                // ============================================

                // === Tier 0: 速度专家 (2) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: [速度专家] 移动速度加成 (%)",
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: [速度专家] 所需点数",

                // === Tier 1: 基础敏捷 (5) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: [基础敏捷] 闪避后移动速度加成 (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: [基础敏捷] 效果持续时间 (秒)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: [基础敏捷] 攻击速度加成 (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: [基础敏捷] 闪避速度加成 (%)",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: [基础敏捷] 所需点数",

                // === Tier 2-1: 近战流 (5) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2-1: [近战流] 2连击攻击速度加成 (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2-1: [近战流] 体力消耗减少 (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2-1: [近战流] 效果持续时间 (秒)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2-1: [近战流] 连击速度加成 (%)",
                ["Tier2_MeleeFlow_RequiredPoints"] = "Tier 2-1: [近战流] 所需点数",

                // === Tier 2-2: 弩专家 (4) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2-2: [弩专家] 命中后移动速度加成 (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2-2: [弩专家] 效果持续时间 (秒)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2-2: [弩专家] 效果期间装填速度加成 (%)",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "Tier 2-2: [弩专家] 所需点数",

                // === Tier 2-3: 弓专家 (4) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2-3: [弓专家] 2连击体力消耗减少 (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2-3: [弓专家] 下次拉弓速度加成 (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2-3: [弓专家] 效果持续时间 (秒)",
                ["Tier2_BowExpert_RequiredPoints"] = "Tier 2-3: [弓专家] 所需点数",

                // === Tier 2-4: 移动施法 (4) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2-4: [移动施法] 施法时移动速度加成 (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2-4: [移动施法] 以特尔消耗减少 (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2-4: [移动施法] 法杖施法时移动速度 (%)",
                ["Tier2_MobileCast_RequiredPoints"] = "Tier 2-4: [移动施法] 所需点数",

                // === Tier 3-1: 实习者1 (3) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3-1: [实习者 1] 近战熟练度加成",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3-1: [实习者 1] 弩熟练度加成",
                ["Tier3_Practitioner1_RequiredPoints"] = "Tier 3-1: [实习者 1] 所需点数",

                // === Tier 3-2: 实习者2 (3) ===
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3-2: [实习者 2] 法杖熟练度加成",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3-2: [实习者 2] 弓熟练度加成",
                ["Tier3_Practitioner2_RequiredPoints"] = "Tier 3-2: [实习者 2] 所需点数",

                // === Tier 4-1: 能量补给 (2) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4-1: [能量补给] 食物消耗率减少 (%)",
                ["Tier4_Energizer_RequiredPoints"] = "Tier 4-1: [能量补给] 所需点数",

                // === Tier 4-2: 船长 (2) ===
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4-2: [船长] 船速加成 (%)",
                ["Tier4_Captain_RequiredPoints"] = "Tier 4-2: [船长] 所需点数",

                // === Tier 5: 跳跃大师 (3) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: [跳跃大师] 跳跃熟练度加成",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: [跳跃大师] 跳跃体力消耗减少 (%)",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: [跳跃大师] 所需点数",

                // === Tier 6-1: 灵巧 (3) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6-1: [灵巧] 近战攻击速度加成 (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6-1: [灵巧] 移动速度加成 (%)",
                ["Tier6_Dexterity_RequiredPoints"] = "Tier 6-1: [灵巧] 所需点数",

                // === Tier 6-2: 耐力 (2) ===
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6-2: [耐力] 最大体力加成",
                ["Tier6_Endurance_RequiredPoints"] = "Tier 6-2: [耐力] 所需点数",

                // === Tier 6-3: 智慧 (2) ===
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6-3: [智慧] 最大以特尔加成",
                ["Tier6_Intellect_RequiredPoints"] = "Tier 6-3: [智慧] 所需点数",

                // === Tier 7: 大师 (3) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: [大师] 奔跑熟练度加成",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: [大师] 跳跃熟练度加成",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: [大师] 所需点数",

                // === Tier 8-1: 近战加速 (3) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8-1: [近战加速] 近战攻击速度加成 (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8-1: [近战加速] 3连击后下次攻击速度加成 (%)",
                ["Tier8_MeleeAccel_RequiredPoints"] = "Tier 8-1: [近战加速] 所需点数",

                // === Tier 8-2: 弩加速 (3) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8-2: [弩加速] 装填速度加成 (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8-2: [弩加速] 装填时移动速度 (%)",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "Tier 8-2: [弩加速] 所需点数",

                // === Tier 8-3: 弓加速 (3) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8-3: [弓加速] 拉弓速度加成 (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8-3: [弓加速] 拉弓时移动速度 (%)",
                ["Tier8_BowAccel_RequiredPoints"] = "Tier 8-3: [弓加速] 所需点数",

                // === Tier 8-4: 施法加速 (3) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8-4: [施法加速] 魔法攻击速度加成 (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8-4: [施法加速] 3连击时最大以特尔恢复率 (%)",
                ["Tier8_CastAccel_RequiredPoints"] = "Tier 8-4: [施法加速] 所需点数",

                // ============================================
                // 防御树 - 59个键
                // ============================================

                // === Tier 0: 防御专家 (3) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: [防御专家] 生命值加成",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: [防御专家] 护甲加成",
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: [防御专家] 所需点数",

                // === Tier 1: 皮肤硬化 (3) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: [皮肤硬化] 生命值加成",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: [皮肤硬化] 护甲加成",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: [皮肤硬化] 所需点数",

                // === Tier 2-1: 身心训练 (3) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2-1: [身心训练] 最大体力加成",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2-1: [身心训练] 最大以特尔加成",
                ["Tier2_MindTraining_RequiredPoints"] = "Tier 2-1: [身心训练] 所需点数",

                // === Tier 2-2: 健康训练 (3) ===
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2-2: [健康训练] 生命值加成",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2-2: [健康训练] 护甲加成",
                ["Tier2_HealthTraining_RequiredPoints"] = "Tier 2-2: [健康训练] 所需点数",

                // === Tier 3-1: 核心呼吸 (2) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3-1: [核心呼吸] 以特尔加成",
                ["Tier3_CoreBreathing_RequiredPoints"] = "Tier 3-1: [核心呼吸] 所需点数",

                // === Tier 3-2: 闪避训练 (3) ===
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3-2: [闪避训练] 闪避加成 (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3-2: [闪避训练] 翻滚无敌时间加成 (%)",
                ["Tier3_EvasionTraining_RequiredPoints"] = "Tier 3-2: [闪避训练] 所需点数",

                // === Tier 3-3: 生命强化 (2) ===
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3-3: [生命强化] 生命值加成",
                ["Tier3_HealthBoost_RequiredPoints"] = "Tier 3-3: [生命强化] 所需点数",

                // === Tier 3-4: 格挡训练 (4) ===
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3-4: [格挡训练] 盾牌格挡力加成",
                ["Tier3_ShieldTraining_RequiredPoints"] = "Tier 3-4: [格挡训练] 所需点数",
                ["Tier3_BlockTraining_ParryBlockPowerRatio"] = "Tier 3-4: [格挡训练] 弹反反击格挡力比率 (%)",
                ["Tier3_BlockTraining_PushDistance"] = "Tier 3-4: [格挡训练] 弹反反击击退距离 (m)",

                // === Tier 4-1: 冲击波 (4) ===
                ["Tier4_Shockwave_Radius"] = "Tier 4-1: [冲击波] 效果范围",
                ["Tier4_Shockwave_StunDuration"] = "Tier 4-1: [冲击波] 眩晕持续时间",
                ["Tier4_Shockwave_Cooldown"] = "Tier 4-1: [冲击波] 冷却时间",
                ["Tier4_Shockwave_RequiredPoints"] = "Tier 4-1: [冲击波] 所需点数",

                // === Tier 4-2: 震地踏 (6) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4-2: [震地踏] 效果范围 (米)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4-2: [震地踏] 击退力",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4-2: [震地踏] 冷却时间 (秒)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4-2: [震地踏] 自动触发生命阈值",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4-2: [震地踏] 视觉效果持续时间 (秒)",
                ["Tier4_GroundStomp_RequiredPoints"] = "Tier 4-2: [震地踏] 所需点数",

                // === Tier 4-3: 岩石皮肤 (2) ===
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4-3: [岩石皮肤] 护甲强化 (%)",
                ["Tier4_RockSkin_RequiredPoints"] = "Tier 4-3: [岩石皮肤] 所需点数",

                // === Tier 5-1: 耐力 (3) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5-1: [耐力] 奔跑体力消耗减少 (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5-1: [耐力] 跳跃体力消耗减少 (%)",
                ["Tier5_Endurance_RequiredPoints"] = "Tier 5-1: [耐力] 所需点数",

                // === Tier 5-2: 敏捷 (3) ===
                ["Tier5_Agility_DodgeBonus"] = "Tier 5-2: [敏捷] 闪避加成 (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5-2: [敏捷] 翻滚体力消耗减少 (%)",
                ["Tier5_Agility_RequiredPoints"] = "Tier 5-2: [敏捷] 所需点数",

                // === Tier 5-3: 巨魔再生 (3) ===
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5-3: [巨魔再生] 生命恢复加成 (每秒)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5-3: [巨魔再生] 恢复间隔 (秒)",
                ["Tier5_TrollRegen_RequiredPoints"] = "Tier 5-3: [巨魔再生] 所需点数",

                // === Tier 5-4: 格挡大师 (3) ===
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5-4: [格挡大师] 盾牌格挡力加成",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5-4: [格挡大师] 招架持续时间加成 (秒)",
                ["Tier5_BlockMaster_RequiredPoints"] = "Tier 5-4: [格挡大师] 所需点数",

                // === Tier 6-1: 心灵盾 (4) ===
                ["Tier6_MindShield_RequiredPoints"] = "Tier 6-1: [心灵盾] 所需点数",
                ["Tier6_MindShield_Cooldown"] = "Tier 6-1: [心灵盾] 冷却时间 (秒)",
                ["Tier6_MindShield_EitrCost"] = "Tier 6-1: [心灵盾] 以太消耗",
                ["Tier6_MindShield_Duration"] = "Tier 6-1: [心灵盾] 护盾持续时间 (秒)",

                // === Tier 6-2: 神经强化 (2) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6-2: [神经强化] 条件性闪避加成 (30秒无闪避, %)",
                ["Tier6_NerveEnhancement_RequiredPoints"] = "Tier 6-2: [神经强化] 所需点数",

                // === Tier 6-3: 二段跳 (1) ===
                ["Tier6_DoubleJump_RequiredPoints"] = "Tier 6-3: [二段跳] 所需点数",

                // === Tier 6-4: 巨人生命 (3) ===
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6-4: [巨人生命] 生命值加成 (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6-4: [巨人生命] 物理/元素抗性 (%)",
                ["Tier6_JotunnVitality_RequiredPoints"] = "Tier 6-4: [巨人生命] 所需点数",

                // === Tier 6-5: 巨人盾 (4) ===
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6-5: [巨人盾] 格挡体力消耗减少 (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6-5: [巨人盾] 普通盾移动速度加成 (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6-5: [巨人盾] 塔盾移动速度加成 (%)",
                ["Tier6_JotunnShield_RequiredPoints"] = "Tier 6-5: [巨人盾] 所需点数",

                // ============================================
                // 生产树 - 22个键
                // ============================================

                // === Tier 0: 生产专家 (1) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] = "Tier 0: 木材+1概率 (%)",

                // === Tier 1: 新手工人 (1) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] = "Tier 1: 木材+1概率 (%)",

                // === Tier 2: 专业化 (5) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] = "Tier 2: 伐木Lv2 - 木材+1概率 (%)",
                ["Tier2_GatheringLv2_BonusChance"] = "Tier 2: 采集Lv2 - 物品+1概率 (%)",
                ["Tier2_MiningLv2_BonusChance"] = "Tier 2: 采矿Lv2 - 矿石+1概率 (%)",
                ["Tier2_CraftingLv2_UpgradeChance"] = "Tier 2: 制作Lv2 - 强化+1概率 (%)",
                ["Tier2_CraftingLv2_DurabilityBonus"] = "Tier 2: 制作Lv2 - 最大耐久度提升 (%)",

                // === Tier 3: 中级技能 (5) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] = "Tier 3: 伐木Lv3 - 木材+2概率 (%)",
                ["Tier3_GatheringLv3_BonusChance"] = "Tier 3: 采集Lv3 - 物品+1概率 (%)",
                ["Tier3_MiningLv3_BonusChance"] = "Tier 3: 采矿Lv3 - 矿石+1概率 (%)",
                ["Tier3_CraftingLv3_UpgradeChance"] = "Tier 3: 制作Lv3 - 强化+1概率 (%)",
                ["Tier3_CraftingLv3_DurabilityBonus"] = "Tier 3: 制作Lv3 - 最大耐久度提升 (%)",

                // === Tier 4: 高级技能 (5) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] = "Tier 4: 伐木Lv4 - 木材+2概率 (%)",
                ["Tier4_GatheringLv4_BonusChance"] = "Tier 4: 采集Lv4 - 物品+1概率 (%)",
                ["Tier4_MiningLv4_BonusChance"] = "Tier 4: 采矿Lv4 - 矿石+1概率 (%)",
                ["Tier4_CraftingLv4_UpgradeChance"] = "Tier 4: 制作Lv4 - 强化+1概率 (%)",
                ["Tier4_CraftingLv4_DurabilityBonus"] = "Tier 4: 制作Lv4 - 最大耐久度提升 (%)",

                // === 生产树: 所需点数 (14) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: [生产专家] 所需点数",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: [新手工人] 所需点数",
                ["Tier2_WoodcuttingLv2_RequiredPoints"] = "Tier 2: [伐木Lv2] 所需点数",
                ["Tier2_GatheringLv2_RequiredPoints"] = "Tier 2: [采集Lv2] 所需点数",
                ["Tier2_MiningLv2_RequiredPoints"] = "Tier 2: [采矿Lv2] 所需点数",
                ["Tier2_CraftingLv2_RequiredPoints"] = "Tier 2: [制作Lv2] 所需点数",
                ["Tier3_WoodcuttingLv3_RequiredPoints"] = "Tier 3: [伐木Lv3] 所需点数",
                ["Tier3_GatheringLv3_RequiredPoints"] = "Tier 3: [采集Lv3] 所需点数",
                ["Tier3_MiningLv3_RequiredPoints"] = "Tier 3: [采矿Lv3] 所需点数",
                ["Tier3_CraftingLv3_RequiredPoints"] = "Tier 3: [制作Lv3] 所需点数",
                ["Tier4_WoodcuttingLv4_RequiredPoints"] = "Tier 4: [伐木Lv4] 所需点数",
                ["Tier4_GatheringLv4_RequiredPoints"] = "Tier 4: [采集Lv4] 所需点数",
                ["Tier4_MiningLv4_RequiredPoints"] = "Tier 4: [采矿Lv4] 所需点数",
                ["Tier4_CraftingLv4_RequiredPoints"] = "Tier 4: [制作Lv4] 所需点数",

                // ============================================
                // 弓树 - 34个键
                // ============================================

                // === Tier 0: 弓专家 (2) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: [弓专家] 弓伤害加成 (%)",
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: [弓专家] 所需点数",

                // === Tier 1-1: 爆头 (2) ===
                ["Tier1_Headshot_HeadZoneRatio"] = "Tier 1-1: [爆头] 头部区域比例",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1-1: [爆头] 所需点数",

                // === Tier 1-2: 多重射击Lv1 (5) ===
                ["Tier1_MultishotLv1_ActivationChance"] = "Tier 1-2: [多重射击Lv1] 触发概率 (%)",
                ["Tier1_MultishotLv1_AdditionalArrows"] = "Tier 1-2: [多重射击Lv1] 额外箭矢数",
                ["Tier1_MultishotLv1_ArrowConsumption"] = "Tier 1-2: [多重射击Lv1] 箭矢消耗",
                ["Tier1_MultishotLv1_DamagePerArrow"] = "Tier 1-2: [多重射击Lv1] 每箭伤害 (%)",
                ["Tier1_MultishotLv1_RequiredPoints"] = "Tier 1-2: [多重射击Lv1] 所需点数",

                // === Tier 2: 弓熟练度 (3) ===
                ["Tier2_BowMastery_SkillBonus"] = "Tier 2: [弓熟练度] 弓技能加成",
                ["Tier2_BowMastery_SpecialArrowChance"] = "Tier 2: [弓熟练度] 特殊箭矢概率 (%)",
                ["Tier2_BowMastery_RequiredPoints"] = "Tier 2: [弓熟练度] 所需点数",

                // === Tier 3-1: 无声射击 (2) ===
                ["Tier3_SilentStrike_DamageBonus"] = "Tier 3-1: [无声射击] 伤害提升",
                ["Tier3_SilentStrike_RequiredPoints"] = "Tier 3-1: [无声射击] 所需点数",

                // === Tier 3-2: 多重射击Lv2 (2) ===
                ["Tier3_MultishotLv2_ActivationChance"] = "Tier 3-2: [多重射击Lv2] 触发概率 (%)",
                ["Tier3_MultishotLv2_RequiredPoints"] = "Tier 3-2: [多重射击Lv2] 所需点数",

                // === Tier 3-3: 猎人本能 (2) ===
                ["Tier3_HuntingInstinct_CritBonus"] = "Tier 3-3: [猎人本能] 暴击率 (%)",
                ["Tier3_HuntingInstinct_RequiredPoints"] = "Tier 3-3: [猎人本能] 所需点数",

                // === Tier 4: 精准瞄准 (2) ===
                ["Tier4_PrecisionAim_CritDamage"] = "Tier 4: [精准瞄准] 暴击伤害加成 (%)",
                ["Tier4_PrecisionAim_RequiredPoints"] = "Tier 4: [精准瞄准] 所需点数",

                // === Tier 5: 爆炸箭 (5) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] = "Tier 5: [爆炸箭] 伤害倍率 (%)",
                ["Tier5_ExplosiveArrow_Radius"] = "Tier 5: [爆炸箭] 爆炸半径 (米)",
                ["Tier5_ExplosiveArrow_Cooldown"] = "Tier 5: [爆炸箭] 冷却时间 (秒)",
                ["Tier5_ExplosiveArrow_StaminaCost"] = "Tier 5: [爆炸箭] 体力消耗 (%)",
                ["Tier5_ExplosiveArrow_RequiredPoints"] = "Tier 5: [爆炸箭] 所需点数",

                // === Tier 6: 箭雨 (6) ===
                ["Tier6_ArrowRain_DamagePercent"] = "Tier 6: [箭雨] 每箭伤害 (%)",
                ["Tier6_ArrowRain_ArrowCount"] = "Tier 6: [箭雨] 箭数量",
                ["Tier6_ArrowRain_Radius"] = "Tier 6: [箭雨] 落箭半径 (m)",
                ["Tier6_ArrowRain_Cooldown"] = "Tier 6: [箭雨] 冷却时间 (秒)",
                ["Tier6_ArrowRain_StaminaCost"] = "Tier 6: [箭雨] 体力消耗 (%)",
                ["Tier6_ArrowRain_RequiredPoints"] = "Tier 6: [箭雨] 所需点数",

                // ============================================
                // 剑树 (旧版) - 30个键
                // ============================================

                // === 剑树: 剑专家 (1) ===
                ["Sword_Expert_DamageIncrease"] = "Tier 0: 剑伤害提升 (%)",

                // === 剑树: 快速斩击 (1) ===
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: 攻击速度加成 (%)",

                // === 剑树: 连击斩 (2) ===
                ["Sword_ComboSlash_Bonus"] = "Tier 2: 连击攻击加成 (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: 效果持续时间 (秒)",

                // === 剑树: 剑刃反射 (1) ===
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: 伤害加成",

                // === 剑树: 真实决斗 (1) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: 攻击速度加成 (%)",

                // === 剑树: 招架冲锋 (5) ===
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: 效果持续时间 (秒)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: 冲锋攻击加成 (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: 推击距离 (米)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: 体力消耗",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: 冷却时间 (秒)",

                // === 剑树: 冲锋斩 (8) ===
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: 第1击伤害比率 (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: 第2击伤害比率 (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: 第3击伤害比率 (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: 初始冲刺距离 (米)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: 侧移距离 (米)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: 体力消耗",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: 冷却时间 (秒)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: 移动速度 (米/秒)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: 攻击速度加成 (%)",
            };
        }
    }
}

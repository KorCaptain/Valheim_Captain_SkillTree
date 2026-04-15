using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJapaneseKeyNames_Part1()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Skill_Tree_Base - キーバインド
                // ============================================
                ["HotKey_Y"] = "職業スキルキー",
                ["HotKey_R"] = "遠距離スキルキー",
                ["HotKey_G"] = "近接メインスキルキー",
                ["HotKey_H"] = "補助スキルキー",
                ["HUD_PosX"] = "HUD X座標",
                ["HUD_PosY"] = "HUD Y座標",
                ["PassiveMessageDisplay"] = "パッシブメッセージ表示",
                ["GameDifficulty"] = "ゲーム難易度",

                // ============================================
                // 攻撃ツリー - 33キー
                // ============================================

                // === Tier 0: 攻撃エキスパート (2) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: [攻撃エキスパート] 全ダメージボーナス (%)",
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: [攻撃エキスパート] 必要ポイント",

                // === 新4フェーズシステム (Tier1~5) ===
                ["Tier1_Opener_DamageBonus"] = "Tier 1: [先制攻撃] ダメージボーナス (%)",
                ["Tier1_Opener_StaminaReduction"] = "Tier 1: [先制攻撃] スタミナ消費減少 (%)",
                ["Tier1_Opener_Duration"] = "Tier 1: [先制攻撃] 持続時間 (秒)",
                ["Tier1_Opener_Cooldown"] = "Tier 1: [先制攻撃] クールダウン (秒)",
                ["Tier1_Opener_RequiredPoints"] = "Tier 1: [先制攻撃] 必要ポイント",
                ["Tier2_OpenerMelee_FinisherBonus"] = "Tier 2-1: [近接] フィニッシャーボーナス (%)",
                ["Tier2_OpenerMelee_RequiredPoints"] = "Tier 2-1: [近接] 必要ポイント",
                ["Tier2_OpenerBow_CritChance"] = "Tier 2-2: [弓] クリティカル率 (%)",
                ["Tier2_OpenerBow_RequiredPoints"] = "Tier 2-2: [弓] 必要ポイント",
                ["Tier2_OpenerCrossbow_FirstShotBonus"] = "Tier 2-3: [クロスボウ] 初弾ボーナス (%)",
                ["Tier2_OpenerCrossbow_RequiredPoints"] = "Tier 2-3: [クロスボウ] 必要ポイント",
                ["Tier2_OpenerMagic_StaggerProc"] = "Tier 2-4: [魔法] スタガー発動 (1=有効)",
                ["Tier2_OpenerMagic_RequiredPoints"] = "Tier 2-4: [魔法] 必要ポイント",
                ["Tier3_Pursuit_DamageBonus"] = "Tier 3: [追撃] 基本ダメージボーナス (%)",
                ["Tier3_Pursuit_ChainDamageBonus"] = "Tier 3: [追撃] 連鎖ダメージボーナス (%)",
                ["Tier3_Pursuit_ChainWindow"] = "Tier 3: [追撃] 連鎖ウィンドウ (秒)",
                ["Tier3_Pursuit_RequiredPoints"] = "Tier 3: [追撃] 必要ポイント",
                ["Tier4_PursuitSpeed_SpeedBonus"] = "Tier 4-1: [疾風追撃] 移動速度ボーナス (%)",
                ["Tier4_PursuitSpeed_RequiredPoints"] = "Tier 4-1: [疾風追撃] 必要ポイント",
                ["Tier4_FrenzyTrigger_StaminaReduction"] = "Tier 4-2: [乱戦突入] スタミナ減少 (%)",
                ["Tier4_FrenzyTrigger_RequiredPoints"] = "Tier 4-2: [乱戦突入] 必要ポイント",
                ["Tier5_Frenzy_StackBonusBase"] = "Tier 5: [乱戦] 基本スタックボーナス (%)",
                ["Tier5_Frenzy_StackBonusChain"] = "Tier 5: [乱戦] 連鎖スタックボーナス (%)",
                ["Tier5_Frenzy_MaxStacks"] = "Tier 5: [乱戦] 最大スタック数",
                ["Tier5_Frenzy_HitsPerStack"] = "Tier 5: [乱戦] スタック毎ヒット数",
                ["Tier5_Frenzy_Tier6Amplifier"] = "Tier 5: [乱戦] 最大スタック時Tier6増幅倍率",
                ["Tier5_Frenzy_RequiredPoints"] = "Tier 5: [乱戦] 必要ポイント",

                // === Tier 1: 基礎攻撃 (3) ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] = "Tier 1: [基礎攻撃] 物理ダメージボーナス (%)",
                ["Tier1_BaseAttack_ElementalDamageBonus"] = "Tier 1: [基礎攻撃] 属性ダメージボーナス (%)",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: [基礎攻撃] 必要ポイント",

                // === Tier 2: 武器特化 (12) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2-1: [近接特化] 常時ボーナス (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2-1: [近接特化] 追加ダメージ",
                ["Tier2_MeleeSpec_RequiredPoints"] = "Tier 2-1: [近接特化] 必要ポイント",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2-2: [弓特化] 常時ボーナス (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2-2: [弓特化] 追加ダメージ",
                ["Tier2_BowSpec_RequiredPoints"] = "Tier 2-2: [弓特化] 必要ポイント",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2-3: [クロスボウ特化] 常時ボーナス (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2-3: [クロスボウ特化] 追加ダメージ",
                ["Tier2_CrossbowSpec_RequiredPoints"] = "Tier 2-3: [クロスボウ特化] 必要ポイント",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2-4: [杖特化] 常時ボーナス (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2-4: [杖特化] 追加ダメージ",
                ["Tier2_StaffSpec_RequiredPoints"] = "Tier 2-4: [杖特化] 必要ポイント",

                // === Tier 3: 攻撃強化 (3) ===
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: [攻撃強化] 物理ダメージボーナス (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: [攻撃強化] 属性ダメージボーナス (%)",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: [攻撃強化] 必要ポイント",

                // === Tier 4: 戦闘強化 (6) ===
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4-1: [近接強化] 2連撃コンボボーナス (%)",
                ["Tier4_MeleeEnhance_RequiredPoints"] = "Tier 4-1: [近接強化] 必要ポイント",
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4-2: [精密攻撃] クリティカル確率 (%)",
                ["Tier4_PrecisionAttack_RequiredPoints"] = "Tier 4-2: [精密攻撃] 必要ポイント",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4-3: [遠距離強化] ダメージボーナス (%)",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4-3: [遠距離強化] 必要ポイント",

                // === Tier 5: チャージ (3) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: [チャージ] スタミナ回復 (%)",
                ["Tier5_Charge_TriggerChance"] = "Tier 5: [チャージ] 発動確率 (%)",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: [チャージ] 必要ポイント",

                // === Tier 6: 最終強化 (8) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6-1: [弱点攻撃] クリティカルダメージボーナス (%)",
                ["Tier6_WeakPointAttack_RequiredPoints"] = "Tier 6-1: [弱点攻撃] 必要ポイント",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6-2: [コンボフィニッシャー] 常時ボーナス (%)",
                ["Tier6_ComboFinisher_RequiredPoints"] = "Tier 6-2: [コンボフィニッシャー] 必要ポイント",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6-3: [両手粉砕] ダメージボーナス (%)",
                ["Tier6_TwoHandCrush_RequiredPoints"] = "Tier 6-3: [両手粉砕] 必要ポイント",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6-4: [属性攻撃] 属性ボーナス (%)",
                ["Tier6_ElementalAttack_RequiredPoints"] = "Tier 6-4: [属性攻撃] 必要ポイント",

                // ============================================
                // 速度ツリー - 49キー
                // ============================================

                // === Tier 0: 速度エキスパート (2) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: [速度エキスパート] 移動速度ボーナス (%)",
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: [速度エキスパート] 必要ポイント",

                // === Tier 1: 機敏基礎 (5) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: [機敏基礎] 回避後移動速度ボーナス (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: [機敏基礎] バフ持続時間 (秒)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: [機敏基礎] 攻撃速度ボーナス (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: [機敏基礎] 回避速度ボーナス (%)",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: [機敏基礎] 必要ポイント",

                // === Tier 2-1: 連続流 (5) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2-1: [連続流] 2連撃攻撃速度ボーナス (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2-1: [連続流] スタミナ消費軽減 (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2-1: [連続流] バフ持続時間 (秒)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2-1: [連続流] コンボ速度ボーナス (%)",
                ["Tier2_MeleeFlow_RequiredPoints"] = "Tier 2-1: [連続流] 必要ポイント",

                // === Tier 2-2: クロスボウエキスパート (4) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2-2: [クロスボウエキスパート] 命中後移動速度ボーナス (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2-2: [クロスボウエキスパート] バフ持続時間 (秒)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2-2: [クロスボウエキスパート] バフ中リロード速度ボーナス (%)",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "Tier 2-2: [クロスボウエキスパート] 必要ポイント",

                // === Tier 2-3: 弓エキスパート (4) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2-3: [弓エキスパート] 2連撃スタミナ軽減 (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2-3: [弓エキスパート] 次の矢引き速度ボーナス (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2-3: [弓エキスパート] バフ持続時間 (秒)",
                ["Tier2_BowExpert_RequiredPoints"] = "Tier 2-3: [弓エキスパート] 必要ポイント",

                // === Tier 2-4: 移動詠唱 (4) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2-4: [移動詠唱] 詠唱中移動速度ボーナス (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2-4: [移動詠唱] Eitrコスト軽減 (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2-4: [移動詠唱] 杖詠唱中移動速度 (%)",
                ["Tier2_MobileCast_RequiredPoints"] = "Tier 2-4: [移動詠唱] 必要ポイント",

                // === Tier 3-1: 熟練者1 (3) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3-1: [熟練者1] 近接熟練度ボーナス",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3-1: [熟練者1] クロスボウ熟練度ボーナス",
                ["Tier3_Practitioner1_RequiredPoints"] = "Tier 3-1: [熟練者1] 必要ポイント",

                // === Tier 3-2: 熟練者2 (3) ===
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3-2: [熟練者2] 杖熟練度ボーナス",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3-2: [熟練者2] 弓熟練度ボーナス",
                ["Tier3_Practitioner2_RequiredPoints"] = "Tier 3-2: [熟練者2] 必要ポイント",

                // === Tier 4-1: エナジャイザー (2) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4-1: [エナジャイザー] 食料消費率軽減 (%)",
                ["Tier4_Energizer_RequiredPoints"] = "Tier 4-1: [エナジャイザー] 必要ポイント",

                // === Tier 4-2: キャプテン (2) ===
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4-2: [キャプテン] 船速度ボーナス (%)",
                ["Tier4_Captain_RequiredPoints"] = "Tier 4-2: [キャプテン] 必要ポイント",

                // === Tier 5: ジャンプマスター (3) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: [ジャンプマスター] ジャンプ熟練度ボーナス",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: [ジャンプマスター] ジャンプスタミナ軽減 (%)",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: [ジャンプマスター] 必要ポイント",

                // === Tier 6-1: 器用さ (3) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6-1: [器用さ] 近接攻撃速度ボーナス (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6-1: [器用さ] 移動速度ボーナス (%)",
                ["Tier6_Dexterity_RequiredPoints"] = "Tier 6-1: [器用さ] 必要ポイント",

                // === Tier 6-2: 持久力 (2) ===
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6-2: [持久力] 最大スタミナボーナス",
                ["Tier6_Endurance_RequiredPoints"] = "Tier 6-2: [持久力] 必要ポイント",

                // === Tier 6-3: 知性 (2) ===
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6-3: [知性] 最大Eitrボーナス",
                ["Tier6_Intellect_RequiredPoints"] = "Tier 6-3: [知性] 必要ポイント",

                // === Tier 7: マスター (3) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: [マスター] 走行熟練度ボーナス",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: [マスター] ジャンプ熟練度ボーナス",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: [マスター] 必要ポイント",

                // === Tier 8-1: 近接加速 (3) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8-1: [近接加速] 近接攻撃速度ボーナス (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8-1: [近接加速] 3連撃後次攻撃速度ボーナス (%)",
                ["Tier8_MeleeAccel_RequiredPoints"] = "Tier 8-1: [近接加速] 必要ポイント",

                // === Tier 8-2: クロスボウ加速 (3) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8-2: [クロスボウ加速] リロード速度ボーナス (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8-2: [クロスボウ加速] リロード中移動速度 (%)",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "Tier 8-2: [クロスボウ加速] 必要ポイント",

                // === Tier 8-3: 弓加速 (3) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8-3: [弓加速] 引き速度ボーナス (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8-3: [弓加速] 引き中移動速度 (%)",
                ["Tier8_BowAccel_RequiredPoints"] = "Tier 8-3: [弓加速] 必要ポイント",

                // === Tier 8-4: 詠唱加速 (3) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8-4: [詠唱加速] 魔法攻撃速度ボーナス (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8-4: [詠唱加速] 3連撃Eitr最大回復率 (%)",
                ["Tier8_CastAccel_RequiredPoints"] = "Tier 8-4: [詠唱加速] 必要ポイント",

                // ============================================
                // 防御ツリー - 59キー
                // ============================================

                // === Tier 0: 防御エキスパート (3) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: [防御エキスパート] HPボーナス",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: [防御エキスパート] 防御力ボーナス",
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: [防御エキスパート] 必要ポイント",

                // === Tier 1: 皮膚強化 (3) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: [皮膚強化] HPボーナス",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: [皮膚強化] 防御力ボーナス",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: [皮膚強化] 必要ポイント",

                // === Tier 2-1: 心身鍛錬 (3) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2-1: [心身鍛錬] 最大スタミナボーナス",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2-1: [心身鍛錬] 最大Eitrボーナス",
                ["Tier2_MindTraining_RequiredPoints"] = "Tier 2-1: [心身鍛錬] 必要ポイント",

                // === Tier 2-2: 体力鍛錬 (3) ===
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2-2: [体力鍛錬] HPボーナス",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2-2: [体力鍛錬] 防御力ボーナス",
                ["Tier2_HealthTraining_RequiredPoints"] = "Tier 2-2: [体力鍛錬] 必要ポイント",

                // === Tier 3-1: 核心呼吸 (2) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3-1: [核心呼吸] Eitrボーナス",
                ["Tier3_CoreBreathing_RequiredPoints"] = "Tier 3-1: [核心呼吸] 必要ポイント",

                // === Tier 3-2: 回避鍛錬 (3) ===
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3-2: [回避鍛錬] 回避ボーナス (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3-2: [回避鍛錬] ロール無敵時間増加 (%)",
                ["Tier3_EvasionTraining_RequiredPoints"] = "Tier 3-2: [回避鍛錬] 必要ポイント",

                // === Tier 3-3: 体力強化 (2) ===
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3-3: [体力強化] HPボーナス",
                ["Tier3_HealthBoost_RequiredPoints"] = "Tier 3-3: [体力強化] 必要ポイント",

                // === Tier 3-4: シールド鍛錬 (2) ===
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3-4: [シールド鍛錬] 盾ブロック力ボーナス",
                ["Tier3_ShieldTraining_RequiredPoints"] = "Tier 3-4: [シールド鍛錬] 必要ポイント",

                // === Tier 4-1: 衝撃波 (4) ===
                ["Tier4_Shockwave_Radius"] = "Tier 4-1: [衝撃波] 範囲",
                ["Tier4_Shockwave_StunDuration"] = "Tier 4-1: [衝撃波] スタン持続時間",
                ["Tier4_Shockwave_Cooldown"] = "Tier 4-1: [衝撃波] クールダウン",
                ["Tier4_Shockwave_RequiredPoints"] = "Tier 4-1: [衝撃波] 必要ポイント",

                // === Tier 4-2: 地面踏みつけ (6) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4-2: [地面踏みつけ] 効果範囲 (m)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4-2: [地面踏みつけ] ノックバック力",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4-2: [地面踏みつけ] クールダウン (秒)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4-2: [地面踏みつけ] 自動発動HP閾値",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4-2: [地面踏みつけ] VFX持続時間 (秒)",
                ["Tier4_GroundStomp_RequiredPoints"] = "Tier 4-2: [地面踏みつけ] 必要ポイント",

                // === Tier 4-3: 岩肌 (2) ===
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4-3: [岩肌] 防御力増幅 (%)",
                ["Tier4_RockSkin_RequiredPoints"] = "Tier 4-3: [岩肌] 必要ポイント",

                // === Tier 5-1: 持久力 (3) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5-1: [持久力] 走行スタミナ軽減 (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5-1: [持久力] ジャンプスタミナ軽減 (%)",
                ["Tier5_Endurance_RequiredPoints"] = "Tier 5-1: [持久力] 必要ポイント",

                // === Tier 5-2: 機敏さ (3) ===
                ["Tier5_Agility_DodgeBonus"] = "Tier 5-2: [機敏さ] 回避ボーナス (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5-2: [機敏さ] ロールスタミナ軽減 (%)",
                ["Tier5_Agility_RequiredPoints"] = "Tier 5-2: [機敏さ] 必要ポイント",

                // === Tier 5-3: トロル再生 (3) ===
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5-3: [トロル再生] HP再生ボーナス (毎秒)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5-3: [トロル再生] 再生間隔 (秒)",
                ["Tier5_TrollRegen_RequiredPoints"] = "Tier 5-3: [トロル再生] 必要ポイント",

                // === Tier 5-4: ブロックマスター (3) ===
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5-4: [ブロックマスター] 盾ブロック力ボーナス",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5-4: [ブロックマスター] パリィ持続時間ボーナス (秒)",
                ["Tier5_BlockMaster_RequiredPoints"] = "Tier 5-4: [ブロックマスター] 必要ポイント",

                // === Tier 6-1: マインドシールド (1) ===
                ["Tier6_MindShield_RequiredPoints"] = "Tier 6-1: [マインドシールド] 必要ポイント",

                // === Tier 6-2: 神経強化 (2) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6-2: [神経強化] 条件付き回避ボーナス (30秒無回避, %)",
                ["Tier6_NerveEnhancement_RequiredPoints"] = "Tier 6-2: [神経強化] 必要ポイント",

                // === Tier 6-3: 二段ジャンプ (1) ===
                ["Tier6_DoubleJump_RequiredPoints"] = "Tier 6-3: [二段ジャンプ] 必要ポイント",

                // === Tier 6-4: ヨトゥンの活力 (3) ===
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6-4: [ヨトゥンの活力] HPボーナス (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6-4: [ヨトゥンの活力] 物理/属性耐性 (%)",
                ["Tier6_JotunnVitality_RequiredPoints"] = "Tier 6-4: [ヨトゥンの活力] 必要ポイント",

                // === Tier 6-5: ヨトゥンの盾 (4) ===
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6-5: [ヨトゥンの盾] ブロックスタミナ軽減 (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6-5: [ヨトゥンの盾] 通常盾移動速度ボーナス (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6-5: [ヨトゥンの盾] タワーシールド移動速度ボーナス (%)",
                ["Tier6_JotunnShield_RequiredPoints"] = "Tier 6-5: [ヨトゥンの盾] 必要ポイント",

                // ============================================
                // 生産ツリー - 22キー
                // ============================================

                // === Tier 0: 生産エキスパート (1) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] = "Tier 0: 木材+1ボーナス確率 (%)",

                // === Tier 1: 見習い工 (1) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] = "Tier 1: 木材+1ボーナス確率 (%)",

                // === Tier 2: 特化 (5) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] = "Tier 2: 木こりLv2 - 木材+1ボーナス確率 (%)",
                ["Tier2_GatheringLv2_BonusChance"] = "Tier 2: 採集Lv2 - アイテム+1ボーナス確率 (%)",
                ["Tier2_MiningLv2_BonusChance"] = "Tier 2: 採掘Lv2 - 鉱石+1ボーナス確率 (%)",
                ["Tier2_CraftingLv2_UpgradeChance"] = "Tier 2: 製作Lv2 - アップグレード+1ボーナス確率 (%)",
                ["Tier2_CraftingLv2_DurabilityBonus"] = "Tier 2: 製作Lv2 - 最大耐久度増加 (%)",

                // === Tier 3: 中級 (5) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] = "Tier 3: 木こりLv3 - 木材+2ボーナス確率 (%)",
                ["Tier3_GatheringLv3_BonusChance"] = "Tier 3: 採集Lv3 - アイテム+1ボーナス確率 (%)",
                ["Tier3_MiningLv3_BonusChance"] = "Tier 3: 採掘Lv3 - 鉱石+1ボーナス確率 (%)",
                ["Tier3_CraftingLv3_UpgradeChance"] = "Tier 3: 製作Lv3 - アップグレード+1ボーナス確率 (%)",
                ["Tier3_CraftingLv3_DurabilityBonus"] = "Tier 3: 製作Lv3 - 最大耐久度増加 (%)",

                // === Tier 4: 上級 (5) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] = "Tier 4: 木こりLv4 - 木材+2ボーナス確率 (%)",
                ["Tier4_GatheringLv4_BonusChance"] = "Tier 4: 採集Lv4 - アイテム+1ボーナス確率 (%)",
                ["Tier4_MiningLv4_BonusChance"] = "Tier 4: 採掘Lv4 - 鉱石+1ボーナス確率 (%)",
                ["Tier4_CraftingLv4_UpgradeChance"] = "Tier 4: 製作Lv4 - アップグレード+1ボーナス確率 (%)",
                ["Tier4_CraftingLv4_DurabilityBonus"] = "Tier 4: 製作Lv4 - 最大耐久度増加 (%)",

                // === 生産ツリー: 必要ポイント (14) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: [生産エキスパート] 必要ポイント",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: [見習い工] 必要ポイント",
                ["Tier2_WoodcuttingLv2_RequiredPoints"] = "Tier 2: [木こりLv2] 必要ポイント",
                ["Tier2_GatheringLv2_RequiredPoints"] = "Tier 2: [採集Lv2] 必要ポイント",
                ["Tier2_MiningLv2_RequiredPoints"] = "Tier 2: [採掘Lv2] 必要ポイント",
                ["Tier2_CraftingLv2_RequiredPoints"] = "Tier 2: [製作Lv2] 必要ポイント",
                ["Tier3_WoodcuttingLv3_RequiredPoints"] = "Tier 3: [木こりLv3] 必要ポイント",
                ["Tier3_GatheringLv3_RequiredPoints"] = "Tier 3: [採集Lv3] 必要ポイント",
                ["Tier3_MiningLv3_RequiredPoints"] = "Tier 3: [採掘Lv3] 必要ポイント",
                ["Tier3_CraftingLv3_RequiredPoints"] = "Tier 3: [製作Lv3] 必要ポイント",
                ["Tier4_WoodcuttingLv4_RequiredPoints"] = "Tier 4: [木こりLv4] 必要ポイント",
                ["Tier4_GatheringLv4_RequiredPoints"] = "Tier 4: [採集Lv4] 必要ポイント",
                ["Tier4_MiningLv4_RequiredPoints"] = "Tier 4: [採掘Lv4] 必要ポイント",
                ["Tier4_CraftingLv4_RequiredPoints"] = "Tier 4: [製作Lv4] 必要ポイント",

                // ============================================
                // 弓ツリー - 24キー
                // ============================================

                // === Tier 0: 弓エキスパート (2) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: [弓エキスパート] 弓ダメージボーナス (%)",
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: [弓エキスパート] 必要ポイント",

                // === Tier 1-1: ヘッドショット (2) ===
                ["Tier1_Headshot_HeadZoneRatio"] = "Tier 1-1: [ヘッドショット] 頭部ゾーン比率",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1-1: [ヘッドショット] 必要ポイント",

                // === Tier 1-2: 多重射撃Lv1 (5) ===
                ["Tier1_MultishotLv1_ActivationChance"] = "Tier 1-2: [多重射撃Lv1] 発動確率 (%)",
                ["Tier1_MultishotLv1_AdditionalArrows"] = "Tier 1-2: [多重射撃Lv1] 追加矢数",
                ["Tier1_MultishotLv1_ArrowConsumption"] = "Tier 1-2: [多重射撃Lv1] 矢消費数",
                ["Tier1_MultishotLv1_DamagePerArrow"] = "Tier 1-2: [多重射撃Lv1] 1本あたりダメージ (%)",
                ["Tier1_MultishotLv1_RequiredPoints"] = "Tier 1-2: [多重射撃Lv1] 必要ポイント",

                // === Tier 2: 弓熟練 (3) ===
                ["Tier2_BowMastery_SkillBonus"] = "Tier 2: [弓熟練] 弓スキルボーナス",
                ["Tier2_BowMastery_SpecialArrowChance"] = "Tier 2: [弓熟練] 特殊矢確率 (%)",
                ["Tier2_BowMastery_RequiredPoints"] = "Tier 2: [弓熟練] 必要ポイント",

                // === Tier 3-1: 静音射撃 (2) ===
                ["Tier3_SilentStrike_DamageBonus"] = "Tier 3-1: [静音射撃] ダメージ増加",
                ["Tier3_SilentStrike_RequiredPoints"] = "Tier 3-1: [静音射撃] 必要ポイント",

                // === Tier 3-2: 多重射撃Lv2 (2) ===
                ["Tier3_MultishotLv2_ActivationChance"] = "Tier 3-2: [多重射撃Lv2] 発動確率 (%)",
                ["Tier3_MultishotLv2_RequiredPoints"] = "Tier 3-2: [多重射撃Lv2] 必要ポイント",

                // === Tier 3-3: 狩人の本能 (2) ===
                ["Tier3_HuntingInstinct_CritBonus"] = "Tier 3-3: [狩人の本能] クリティカル確率 (%)",
                ["Tier3_HuntingInstinct_RequiredPoints"] = "Tier 3-3: [狩人の本能] 必要ポイント",

                // === Tier 4: 精密照準 (2) ===
                ["Tier4_PrecisionAim_CritDamage"] = "Tier 4: [精密照準] クリティカルダメージボーナス (%)",
                ["Tier4_PrecisionAim_RequiredPoints"] = "Tier 4: [精密照準] 必要ポイント",

                // === Tier 5: 爆発矢 (5) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] = "Tier 5: [爆発矢] ダメージ倍率 (%)",
                ["Tier5_ExplosiveArrow_Radius"] = "Tier 5: [爆発矢] 爆発範囲 (m)",
                ["Tier5_ExplosiveArrow_Cooldown"] = "Tier 5: [爆発矢] クールダウン (秒)",
                ["Tier5_ExplosiveArrow_StaminaCost"] = "Tier 5: [爆発矢] スタミナコスト (%)",
                ["Tier5_ExplosiveArrow_RequiredPoints"] = "Tier 5: [爆発矢] 必要ポイント",

                // === Tier 6: 矢の雨 (6) ===
                ["Tier6_ArrowRain_DamagePercent"] = "Tier 6: [矢の雨] 矢1本のダメージ (%)",
                ["Tier6_ArrowRain_ArrowCount"] = "Tier 6: [矢の雨] 矢の数",
                ["Tier6_ArrowRain_Radius"] = "Tier 6: [矢の雨] 降下範囲 (m)",
                ["Tier6_ArrowRain_Cooldown"] = "Tier 6: [矢の雨] クールダウン (秒)",
                ["Tier6_ArrowRain_StaminaCost"] = "Tier 6: [矢の雨] スタミナコスト (%)",
                ["Tier6_ArrowRain_RequiredPoints"] = "Tier 6: [矢の雨] 必要ポイント",
            };
        }
    }
}

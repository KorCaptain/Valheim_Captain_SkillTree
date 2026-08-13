using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetExpertDescriptions_JP()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Skill_Tree_Base
                // ========================================
                ["PassiveMessageDisplay"] =
                "【パッシブメッセージ表示】\n" +
                "パッシブスキル発動時のメッセージ表示方式を設定します。\n" +
                "  中央（デフォルト）= 画面中央\n" +
                "  左上小文字 = 左上に小さく表示\n" +
                "  オフ = 表示しない\n" +
                "※ スキル習得・生産メッセージは常に中央に表示されます。",

                ["GameDifficulty"] =
                "【ゲーム難易度】\n" +
                "スキルツリーMOD全体のバランスプリセットを選択します。\n" +
                "  Vanilla      = バニラに近い穏やかな数値（デフォルト）\n" +
                "  VeryHard     = CLLC超難 + モンスターHP×2（強化数値）\n" +
                "  UserSettings = 以前保存したユーザー設定を復元\n" +
                "⚠️ 変更するとすぐに選択したプリセットが適用されます（全スキル数値が置き換えられます）。",

                // ========================================
                // 攻撃ツリー (Attack Tree)
                // ========================================
                ["Tier0_AttackExpert_AllDamageBonus"] =
                "【総ダメージボーナス (%)】\n" +
                "物理ダメージと属性ダメージを増加させます。\n" +
                "全武器に適用される基本攻撃力強化です。\n" +
                "推奨: 8-12%",

                ["Tier2_MeleeSpec_BonusTriggerChance"] =
                "【近接武器常時ダメージボーナス (%)】\n" +
                "近接攻撃時に常に追加ダメージを与えます。\n" +
                "攻撃するたびに固定で適用されます。\n" +
                "推奨: 15-25%",

                ["Tier2_MeleeSpec_MeleeDamage"] =
                "【近接追加ダメージ（固定値）】\n" +
                "ボーナス発動時に追加される固定ダメージです。\n" +
                "推奨: 8-15",

                ["Tier2_BowSpec_BonusTriggerChance"] =
                "【弓常時ダメージボーナス (%)】\n" +
                "弓攻撃時に常に追加ダメージを与えます。\n" +
                "攻撃するたびに固定で適用されます。\n" +
                "推奨: 15-25%",

                ["Tier2_BowSpec_BowDamage"] =
                "【弓追加ダメージ（固定値）】\n" +
                "ボーナス発動時に追加される固定ダメージです。\n" +
                "推奨: 6-12",

                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] =
                "【クロスボウ常時ダメージボーナス (%)】\n" +
                "クロスボウ攻撃時に常に追加ダメージを与えます。\n" +
                "攻撃するたびに固定で適用されます。\n" +
                "推奨: 12-20%",

                ["Tier2_CrossbowSpec_CrossbowDamage"] =
                "【クロスボウ追加ダメージ（固定値）】\n" +
                "ボーナス発動時に追加される固定ダメージです。\n" +
                "推奨: 7-13",

                ["Tier2_StaffSpec_ElementalTriggerChance"] =
                "【杖常時ダメージボーナス (%)】\n" +
                "杖攻撃時に常に追加ダメージを与えます。\n" +
                "攻撃するたびに固定で適用されます。\n" +
                "推奨: 15-25%",

                ["Tier2_StaffSpec_StaffDamage"] =
                "【杖追加ダメージ（固定値）】\n" +
                "ボーナス発動時に追加される固定ダメージです。\n" +
                "推奨: 6-12",

                ["Tier1_BaseAttack_PhysicalDamageBonus"] =
                "【物理ダメージボーナス（固定値）】\n" +
                "全武器の物理ダメージを固定値で増加させます。\n" +
                "推奨: 1-3",

                ["Tier1_BaseAttack_ElementalDamageBonus"] =
                "【属性ダメージボーナス（固定値）】\n" +
                "全武器の属性ダメージ（炎、氷、雷）を固定値で増加させます。\n" +
                "推奨: 1-3",

                ["Tier3_AttackBoost_PhysicalDamageBonus"] =
                "【両手武器物理ダメージボーナス (%)】\n" +
                "両手武器の物理ダメージを増加させます。\n" +
                "推奨: 8-15%",

                ["Tier3_AttackBoost_ElementalDamageBonus"] =
                "【両手武器属性ダメージボーナス (%)】\n" +
                "両手武器の属性ダメージを増加させます。\n" +
                "推奨: 8-15%",

                ["Tier4_PrecisionAttack_CritChance"] =
                "【クリティカル率ボーナス (%)】\n" +
                "全攻撃のクリティカル率を増加させます。\n" +
                "推奨: 3-8%",

                ["Tier4_MeleeEnhance_2HitComboBonus"] =
                "【2連撃ボーナス (%)】\n" +
                "近接攻撃を2回連続で行うとダメージが増加します。\n" +
                "推奨: 8-15%",

                ["Tier4_RangedEnhance_RangedDamageBonus"] =
                "【遠距離ダメージボーナス（固定値）】\n" +
                "遠距離武器（弓、クロスボウ）の固定ダメージを増加させます。\n" +
                "推奨: 3-8",

                ["Tier5_SpecialStat_SpecBonus"] =
                "【スタミナ回復】\n" +
                "命中時に回復するスタミナの割合です。\n" +
                "推奨: 3-10",

                ["Tier5_Charge_TriggerChance"] =
                "【発動確率】\n" +
                "命中時にスタミナ回復が発動する確率です。\n" +
                "推奨: 20-50",

                ["Tier6_WeakPointAttack_CritDamageBonus_Lv1"] = "【弱点攻撃 Lv1 クリティカルダメージボーナス (%)】\nレベル1到達時に適用されるクリティカルダメージ増加量です。\n推奨: 5%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv2"] = "【弱点攻撃 Lv2 クリティカルダメージボーナス (%)】\nレベル2到達時に適用されるクリティカルダメージ増加量です。\n推奨: 9%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv3"] = "【弱点攻撃 Lv3 クリティカルダメージボーナス (%)】\nレベル3到達時に適用されるクリティカルダメージ増加量です。\n推奨: 13%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv4"] = "【弱点攻撃 Lv4 クリティカルダメージボーナス (%)】\nレベル4到達時に適用されるクリティカルダメージ増加量です。\n推奨: 17%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv5"] = "【弱点攻撃 Lv5 クリティカルダメージボーナス (%)】\nレベル5到達時に適用されるクリティカルダメージ増加量です。\n推奨: 21%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv6"] = "【弱点攻撃 Lv6 クリティカルダメージボーナス (%)】\nレベル6到達時に適用されるクリティカルダメージ増加量です。\n推奨: 25%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv7"] = "【弱点攻撃 Lv7（最大）クリティカルダメージボーナス (%)】\nレベル7（最大）到達時に適用されるクリティカルダメージ増加量です。\n推奨: 29%",

                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] =
                "【両手武器ダメージボーナス (%)】\n" +
                "両手武器の総ダメージを増加させます。\n" +
                "推奨: 8-15%",

                ["Tier6_ElementalAttack_ElementalBonus"] =
                "【杖属性ダメージボーナス (%)】\n" +
                "杖の属性ダメージ（炎、氷、雷）を増加させます。\n" +
                "推奨: 8-15%",

                ["Tier6_ComboFinisher_3HitComboBonus"] =
                "【3連撃フィニッシャーボーナス (%)】\n" +
                "3連撃の最後の一撃のダメージを増加させます。\n" +
                "推奨: 12-20%",

                // ======================================== [新: 攻撃システム 4フェーズ]
                ["Tier1_Opener_DamageBonus"] =
                "【先制攻撃ダメージボーナス (%)】\n" +
                "戦闘開始後数秒間、ダメージが増加します。\n" +
                "推奨: 15-25%",

                ["Tier1_Opener_StaminaReduction"] =
                "【スタミナ消費削減 (%)】\n" +
                "先制攻撃フェーズ中のスタミナ消費を削減します。\n" +
                "推奨: 20-30%",

                ["Tier1_Opener_Duration"] =
                "【先制攻撃効果時間 (秒)】\n" +
                "戦闘開始後の先制攻撃効果の持続時間です。\n" +
                "推奨: 4-6 秒",

                ["Tier1_Opener_Cooldown"] =
                "【クールダウン (秒)】\n" +
                "先制攻撃効果が再度発動するまでの待機時間です。\n" +
                "推奨: 25-35 秒",

                ["Tier2_OpenerMelee_FinisherBonus"] =
                "【近接フィニッシャーボーナス (%)】\n" +
                "戦闘開始後最初の一撃後にフィニッシャー倍率が増加します。\n" +
                "推奨: 15-25%",

                ["Tier2_OpenerBow_CritChance"] =
                "【弓：ハンターの瞳 クリティカルダメージ (%)】\n" +
                "先制窓内で最初の矢が確定クリティカルになる際、追加されるクリティカルダメージです。\n" +
                "推奨: 6-10%",

                ["Tier2_OpenerCrossbow_FirstShotBonus"] =
                "【クロスボウ初弾ボーナス (%)】\n" +
                "戦闘開始後の最初のボルトのダメージが増加します。\n" +
                "推奨: 40-60%",

                ["Tier2_OpenerMagic_StaggerProc"] =
                "【魔法よろめき発動 (0/1)】\n" +
                "戦闘開始後の最初の魔法攻撃でよろめきが確定発動します。\n" +
                "0 = 無効, 1 = 有効",

                ["Tier3_Pursuit_DamageBonus"] =
                "【追撃ダメージボーナス (%)】\n" +
                "移動中・逃走中の敵に対してダメージが増加します。\n" +
                "推奨: 12-18%",

                ["Tier3_Pursuit_ChainDamageBonus"] =
                "【追撃チェーンボーナス (%)】\n" +
                "先制攻撃チェーン発動後に追撃が強化されます。\n" +
                "推奨: 20-30%",

                ["Tier3_Pursuit_ChainWindow"] =
                "【チェーンウィンドウ (秒)】\n" +
                "先制攻撃後の追撃チェーン有効時間です。\n" +
                "推奨: 4-6 秒",

                ["Tier4_PursuitSpeed_SpeedBonus"] =
                "【移動速度ボーナス (%)】\n" +
                "戦闘中の移動速度が増加します。\n" +
                "推奨: 10-15%",

                ["Tier4_FrenzyTrigger_CritChancePerLevel"] =
                "【致命の一撃 クリティカル率増加量 (レベルごと%)】\n" +
                "レベルアップごとに増加するクリティカル率です。(Lv × 増加量)\n" +
                "推奨: 1-3%",

                ["Tier5_Frenzy_StackBonusBase"] =
                "【乱戦スタック基本ボーナス (%)】\n" +
                "チェーンなしの1スタックあたりのダメージボーナスです。\n" +
                "推奨: 4-6%",

                ["Tier5_Frenzy_StackBonusChain"] =
                "【乱戦チェーンスタックボーナス (%)】\n" +
                "追撃チェーン発動時の1スタックあたりの強化ボーナスです。\n" +
                "推奨: 7-10%",

                ["Tier5_Frenzy_MaxStacks"] =
                "【最大スタック数】\n" +
                "乱戦スタックの最大数です。\n" +
                "推奨: 4-6",

                ["Tier5_Frenzy_HitsPerStack"] =
                "【スタックあたりのヒット数】\n" +
                "スタックを1つ積み上げるのに必要なヒット数です。\n" +
                "推奨: 2-4",

                ["Tier5_Frenzy_Tier6Amplifier"] =
                "【最大スタック時Tier6増幅率 (×)】\n" +
                "最大スタック達成時のTier6効果全体の倍率です。\n" +
                "推奨: 1.2-1.4",

                // === 新4フェーズシステム必要ポイント ===
                ["Tier1_Opener_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",
                ["Tier2_OpenerMelee_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",
                ["Tier2_OpenerBow_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",
                ["Tier2_OpenerCrossbow_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",
                ["Tier2_OpenerMagic_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",
                ["Tier3_Pursuit_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",
                ["Tier4_PursuitSpeed_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",
                ["Tier4_FrenzyTrigger_PointsPerLevel"] = "【レベルごとの必要ポイント】\nレベルアップ1回につき消費するスキルポイントです。\n推奨: 2",
                ["Tier5_Frenzy_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイント数です。",

                // ========================================
                // 防御ツリー (Defense Tree)
                // ========================================
                ["Tier0_DefenseExpert_HPBonus"] =
                "【HP ボーナス（固定値）】\n" +
                "最大HPを固定値で増加させます。\n" +
                "推奨: 3-8",

                ["Tier0_DefenseExpert_ArmorBonus"] =
                "【防御力ボーナス（固定値）】\n" +
                "防御力を固定値で増加させます。\n" +
                "推奨: 1-4",

                ["Tier0_DefenseExpert_AtkPenalty"] =
                "【攻撃力低下（%）】\n" +
                "防御専家を習得すると攻撃力が少し低下します。\n" +
                "防御と攻撃のトレードオフです。\n" +
                "推奨: 1-3%",

                ["Tier1_SkinHardening_HPBonus"] =
                "【HP ボーナス（固定値）】\n" +
                "最大HPをさらに増加させます。\n" +
                "推奨: 3-8",

                ["Tier1_SkinHardening_ArmorBonus"] =
                "【防御力ボーナス（固定値）】\n" +
                "防御力をさらに増加させます。\n" +
                "推奨: 3-8",

                ["Tier2_MindBodyTraining_StaminaBonus"] =
                "【最大スタミナボーナス（固定値）】\n" +
                "最大スタミナを増加させます。\n" +
                "推奨: 20-30",

                ["Tier2_MindBodyTraining_EitrBonus"] =
                "【最大エイトルボーナス（固定値）】\n" +
                "魔法攻撃に使用する最大エイトルを増加させます。\n" +
                "推奨: 20-30",

                ["Tier2_HealthTraining_HPBonus"] =
                "【HP ボーナス（固定値）】\n" +
                "最大HPを大幅に増加させます。\n" +
                "推奨: 15-25",

                ["Tier2_HealthTraining_ArmorBonus"] =
                "【防御力ボーナス（固定値）】\n" +
                "防御力をさらに増加させます。\n" +
                "推奨: 3-8",

                ["Tier3_CoreBreathing_EitrBonus"] =
                "【エイトルボーナス（固定値）】\n" +
                "瞑想によりエイトルを増加させます。\n" +
                "推奨: 8-15",

                ["Tier3_EvasionTraining_DodgeBonus"] =
                "【回避率ボーナス (%)】\n" +
                "敵の攻撃を回避する確率を増加させます。\n" +
                "推奨: 3-8%",

                ["Tier3_EvasionTraining_InvincibilityBonus"] =
                "【無敵時間（ローリング）(%)】\n" +
                "ローリング中の無敵持続時間を延長します。\n" +
                "推奨: 15-25%",

                ["Tier3_HealthBoost_HPBonus"] =
                "【HP ボーナス（固定値）】\n" +
                "HPをさらに増加させます。\n" +
                "推奨: 12-20",

                ["Tier3_ShieldTraining_BlockPowerBonus"] =
                "【ブロック力ボーナス（固定値）】\n" +
                "シールドのブロック力を増加させます。\n" +
                "推奨: 80-120",

                ["Tier3_BlockTraining_ParryBlockPowerRatio"] =
                "【パリィ反撃ブロック力比率 (%)】\n" +
                "パリィ成功時の反撃ダメージ = ブロック力 × 比率 / 100。\n" +
                "推奨: 80-150%",

                ["Tier3_BlockTraining_PushDistance"] =
                "【パリィ反撃ノックバック距離 (m)】\n" +
                "パリィ反撃成功時に敵を吹き飛ばす距離です。\n" +
                "推奨: 3-6m",

                ["Tier4_GroundStomp_Radius"] =
                "【効果範囲 (m)】\n" +
                "グラウンドストンプの衝撃波範囲です。\n" +
                "推奨: 2.5-4 m",

                ["Tier4_GroundStomp_KnockbackForce"] =
                "【ノックバック力】\n" +
                "敵を吹き飛ばす力です。\n" +
                "推奨: 15-25",

                ["Tier4_GroundStomp_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 100-150 秒",

                ["Tier4_GroundStomp_HPThreshold"] =
                "【HP閾値（自動発動）】\n" +
                "HP がこの値を下回ると自動発動します。\n" +
                "0.35 = HP の35%\n" +
                "推奨: 0.30-0.40",

                ["Tier4_GroundStomp_VFXDuration"] =
                "【エフェクト持続時間（秒）】\n" +
                "視覚エフェクトの表示持続時間です。\n" +
                "推奨: 0.8-1.5 秒",

                ["Tier4_RockSkin_ArmorBonus"] =
                "【防御力強化 (%)】\n" +
                "ヘルメット、胸装備、脚装備、シールドに防御力ボーナスを付与します。\n" +
                "推奨: 10-15%",

                ["Tier5_Endurance_RunStaminaReduction"] =
                "【走行スタミナ消費 (%)】\n" +
                "走行時のスタミナ消費を減少させます。\n" +
                "推奨: 8-15%",

                ["Tier5_Endurance_JumpStaminaReduction"] =
                "【ジャンプスタミナ消費 (%)】\n" +
                "ジャンプ時のスタミナ消費を減少させます。\n" +
                "推奨: 8-15%",

                ["Tier5_Agility_DodgeBonus"] =
                "【回避率ボーナス (%)】\n" +
                "回避確率をさらに増加させます。\n" +
                "推奨: 3-8%",

                ["Tier5_Agility_RollStaminaReduction"] =
                "【ローリングスタミナ消費 (%)】\n" +
                "ローリング時のスタミナ消費を減少させます。\n" +
                "推奨: 10-18%",

                ["Tier5_TrollRegen_HPRegenBonus"] =
                "【HP 自然回復ボーナス（毎秒）】\n" +
                "トロルのように自動でHP を回復します。\n" +
                "推奨: 3-8",

                ["Tier5_TrollRegen_RegenInterval"] =
                "【回復間隔（秒）】\n" +
                "HP 回復の時間間隔です。\n" +
                "推奨: 1.5-3 秒",

                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] =
                "【ブロック力ボーナス（固定値）】\n" +
                "シールドのブロック力を大幅に増加させます。\n" +
                "推奨: 80-120",

                ["Tier5_BlockMaster_ParryDurationBonus"] =
                "【パリィ持続時間ボーナス（秒）】\n" +
                "パリィ成功後の効果持続時間を延長します。\n" +
                "推奨: 0.8-1.5 秒",

                ["Tier6_NerveEnhancement_DodgeBonus"] =
                "【条件付き回避ボーナス（30秒, %)】\n" +
                "30秒間回避しないと発動します。\n" +
                "推奨: 85%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【HP ボーナス (%)】\n" +
                "最大HPを割合で増加させます。\n" +
                "推奨: 25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【物理/属性耐性 (%)】\n" +
                "全物理・属性ダメージを減少させます。\n" +
                "推奨: 8-15%",

                // ========================================
                // 生産ツリー (Production Tree)
                // ========================================
                ["Tier0_ProductionExpert_WoodBonusChance"] =
                "【木材+1確率 (%)】\n" +
                "伐採時に追加で木材を獲得する確率です。\n" +
                "推奨: 40-60%",

                ["Tier0_ProductionExpert_RequiredPoints"] =
                "【必要ポイント - 生産専家】\n" +
                "生産専家を解放するための必要スキルポイントです。\n" +
                "推奨: 2",

                ["Tier1_NoviceWorker_WoodBonusChance"] =
                "【木材+1確率 (%)】\n" +
                "伐採時の追加木材獲得確率を増加させます。\n" +
                "推奨: 20-30%",

                ["Tier1_NoviceWorker_RequiredPoints"] =
                "【必要ポイント - 見習い労働者】\n" +
                "見習い労働者を解放するための必要スキルポイントです。\n" +
                "推奨: 2",

                ["Tier2_WoodcuttingLv2_BonusChance"] =
                "【木材+1確率 (%)】\n" +
                "伐採 Lv.2 - 追加木材獲得確率です。\n" +
                "推奨: 20-30%",

                ["Tier2_WoodcuttingLv2_RequiredPoints"] =
                "【必要ポイント - 伐採 Lv.2】\n" +
                "推奨: 2",

                ["Tier2_GatheringLv2_BonusChance"] =
                "【アイテム+1確率 (%)】\n" +
                "採集 Lv.2 - 追加アイテム獲得確率です。\n" +
                "推奨: 20-30%",

                ["Tier2_GatheringLv2_RequiredPoints"] =
                "【必要ポイント - 採集 Lv.2】\n" +
                "推奨: 2",

                ["Tier2_MiningLv2_BonusChance"] =
                "【鉱石+1確率 (%)】\n" +
                "採掘 Lv.2 - 追加鉱石獲得確率です。\n" +
                "推奨: 20-30%",

                ["Tier2_MiningLv2_RequiredPoints"] =
                "【必要ポイント - 採掘 Lv.2】\n" +
                "推奨: 2",

                ["Tier2_CraftingLv2_UpgradeChance"] =
                "【強化+1確率 (%)】\n" +
                "製作 Lv.2 - 追加強化レベルを得る確率です。\n" +
                "推奨: 20-30%",

                ["Tier2_CraftingLv2_RequiredPoints"] =
                "【必要ポイント - 製作 Lv.2】\n" +
                "推奨: 2",

                ["Tier2_CraftingLv2_DurabilityBonus"] =
                "【最大耐久度 (%)】\n" +
                "製作 Lv.2 - 製作アイテムの最大耐久度を増加させます。\n" +
                "推奨: 20-30%",

                ["Tier3_WoodcuttingLv3_BonusChance"] =
                "【木材+2確率 (%)】\n" +
                "伐採 Lv.3 - 木材を2個追加で獲得する確率です。\n" +
                "推奨: 30-40%",

                ["Tier3_WoodcuttingLv3_RequiredPoints"] =
                "【必要ポイント - 伐採 Lv.3】\n" +
                "推奨: 2",

                ["Tier3_GatheringLv3_BonusChance"] =
                "【アイテム+1確率 (%)】\n" +
                "採集 Lv.3 - 追加アイテム獲得確率を増加させます。\n" +
                "推奨: 20-30%",

                ["Tier3_GatheringLv3_RequiredPoints"] =
                "【必要ポイント - 採集 Lv.3】\n" +
                "推奨: 2",

                ["Tier3_MiningLv3_BonusChance"] =
                "【鉱石+1確率 (%)】\n" +
                "採掘 Lv.3 - 追加鉱石獲得確率を増加させます。\n" +
                "推奨: 20-30%",

                ["Tier3_MiningLv3_RequiredPoints"] =
                "【必要ポイント - 採掘 Lv.3】\n" +
                "推奨: 2",

                ["Tier3_CraftingLv3_UpgradeChance"] =
                "【強化+1確率 (%)】\n" +
                "製作 Lv.3 - 強化確率を増加させます。\n" +
                "推奨: 20-30%",

                ["Tier3_CraftingLv3_RequiredPoints"] =
                "【必要ポイント - 製作 Lv.3】\n" +
                "推奨: 2",

                ["Tier3_CraftingLv3_DurabilityBonus"] =
                "【最大耐久度 (%)】\n" +
                "製作 Lv.3 - 耐久度をさらに増加させます。\n" +
                "推奨: 20-30%",

                ["Tier4_WoodcuttingLv4_BonusChance"] =
                "【木材+2確率 (%)】\n" +
                "伐採 Lv.4 - 追加木材獲得の最大確率です。\n" +
                "推奨: 40-50%",

                ["Tier4_WoodcuttingLv4_RequiredPoints"] =
                "【必要ポイント - 伐採 Lv.4】\n" +
                "推奨: 2",

                ["Tier4_GatheringLv4_BonusChance"] =
                "【アイテム+1確率 (%)】\n" +
                "採集 Lv.4 - 追加アイテム獲得の最大確率です。\n" +
                "推奨: 20-30%",

                ["Tier4_GatheringLv4_RequiredPoints"] =
                "【必要ポイント - 採集 Lv.4】\n" +
                "推奨: 2",

                ["Tier4_MiningLv4_BonusChance"] =
                "【鉱石+1確率 (%)】\n" +
                "採掘 Lv.4 - 追加鉱石獲得の最大確率です。\n" +
                "推奨: 20-30%",

                ["Tier4_MiningLv4_RequiredPoints"] =
                "【必要ポイント - 採掘 Lv.4】\n" +
                "推奨: 2",

                ["Tier4_CraftingLv4_UpgradeChance"] =
                "【強化+1確率 (%)】\n" +
                "製作 Lv.4 - 最大強化確率です。\n" +
                "推奨: 20-30%",

                ["Tier4_CraftingLv4_RequiredPoints"] =
                "【必要ポイント - 製作 Lv.4】\n" +
                "推奨: 2",

                ["Tier4_CraftingLv4_DurabilityBonus"] =
                "【最大耐久度 (%)】\n" +
                "製作 Lv.4 - 最大耐久度強化です。\n" +
                "推奨: 20-30%",

                // ========================================
                // 速度ツリー (Speed Tree)
                // ========================================
                ["Tier0_SpeedExpert_MoveSpeedBonus"] =
                "【移動速度ボーナス (%)】\n" +
                "移動速度を永続的に増加させます。\n" +
                "推奨: 5-10%",

                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] =
                "【回避後移動速度ボーナス (%)】\n" +
                "ローリング後に一時的に移動速度が増加します。\n" +
                "推奨: 10-20%",

                ["Tier1_AgilityBase_BuffDuration"] =
                "【効果持続時間（秒）】\n" +
                "ローリング後の速度ボーナスの持続時間です。\n" +
                "推奨: 2-3 秒",

                ["Tier1_AgilityBase_AttackSpeedBonus"] =
                "【攻撃速度ボーナス (%)】\n" +
                "全武器の攻撃速度を増加させます。\n" +
                "推奨: 3-8%",

                ["Tier1_AgilityBase_DodgeSpeedBonus"] =
                "【回避速度ボーナス (%)】\n" +
                "ローリングアニメーションの速度を増加させます。\n" +
                "推奨: 5-15%",

                ["ShowResetButtons"] =
                "【リセットボタン表示】\n" +
                "スキルツリーUIでポイント/職業/生産リセットボタンの表示を設定します。\n" +
                "  true  = リセットボタンを表示（デフォルト）\n" +
                "  false = リセットボタンを非表示（サーバーでスキルポイントリセットを防止する場合に使用）",

                ["Tier3_BlockTraining_MaxChargeDistance"] =
                "【カウンター最大有効距離 (m)】\n" +
                "よろめいたモンスターがこの距離内にいる場合のみカウンターが発動します。\n" +
                "推奨: 6-10m",

                ["Tier0_DefenseExpert_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier1_SkinHardening_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier2_MindTraining_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier2_HealthTraining_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier3_CoreBreathing_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier3_EvasionTraining_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier3_HealthBoost_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier3_ShieldTraining_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier4_Shockwave_Radius"] =
                "【衝撃波範囲】\n" +
                "衝撃波放出スキルの効果範囲（メートル）です。\n" +
                "推奨: 3",

                ["Tier4_Shockwave_StunDuration"] =
                "【衝撃波スタン時間】\n" +
                "スタン効果の持続時間（秒）です。\n" +
                "推奨: 1",

                ["Tier4_Shockwave_Cooldown"] =
                "【衝撃波クールダウン】\n" +
                "スキル再使用までの待機時間（秒）です。\n" +
                "推奨: 120",

                ["Tier4_Shockwave_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier4_Shockwave_KnockbackForce"] =
                "【ノックバック力】\n" +
                "衝撃波発動時に敵を吹き飛ばす力です。\n" +
                "推奨: 15-25",

                ["Tier4_GroundStomp_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier4_RockSkin_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier5_Endurance_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier5_Agility_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier5_TrollRegen_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier5_BlockMaster_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier6_MindShield_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier6_MindShield_Cooldown"] =
                "【マインドシールドクールダウン】\n" +
                "Hキー マインドシールドスキルの再使用待機時間です（秒）。\n" +
                "デフォルト: 210（3分30秒）",

                ["Tier6_MindShield_EitrCost"] =
                "【マインドシールドエイトル消費】\n" +
                "マインドシールド発動時に消費するエイトル量です。\n" +
                "デフォルト: 30",

                ["Tier6_MindShield_Duration"] =
                "【マインドシールド持続時間】\n" +
                "シールドの持続時間です（秒）。この間、最大エイトルHPまでダメージを吸収します。\n" +
                "デフォルト: 180（3分）",

                ["Tier6_NerveEnhancement_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier6_DoubleJump_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier6_JotunnVitality_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このノードを解放するために必要なスキルポイント数です。\n" +
                "推奨: 2",

                ["Tier0_SpeedExpert_MoveSpeedPerLevel"] = "【移動速度ボーナス/レベル (%)】\nレベルごとの移動速度増加量です。（Lv1~7成長式）\n推奨: 1-5%",

                ["Tier0_SpeedExpert_PointsPerLevel"] = "【レベルごとの必要ポイント】\n強化1回あたりに消費するスキルポイントです。\n推奨: 1-2",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_2"] = "【Lv2必要プレイヤーレベル】\nLv2強化に必要なEpicMMOプレイヤーレベルです。（0=なし）\nデフォルト: 15",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_3"] = "【Lv3必要プレイヤーレベル】\nLv3強化に必要なEpicMMOプレイヤーレベルです。\nデフォルト: 30",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_4"] = "【Lv4必要プレイヤーレベル】\nLv4強化に必要なEpicMMOプレイヤーレベルです。\nデフォルト: 45",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_5"] = "【Lv5必要プレイヤーレベル】\nLv5強化に必要なEpicMMOプレイヤーレベルです。\nデフォルト: 50",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_6"] = "【Lv6必要プレイヤーレベル】\nLv6強化に必要なEpicMMOプレイヤーレベルです。\nデフォルト: 65",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_7"] = "【Lv7必要プレイヤーレベル】\nLv7強化に必要なEpicMMOプレイヤーレベルです。\nデフォルト: 80",

                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "【2連撃時攻撃速度ボーナス (%)】\n近接武器で2回連続ヒット後に攻撃速度が増加します。\n推奨: 8-15%",

                ["Tier2_MeleeFlow_StaminaReduction"] = "【スタミナ消費削減 (%)】\nフローバフ中のスタミナ消費を削減します。\n推奨: 10-20%",

                ["Tier2_MeleeFlow_Duration"] = "【バフ持続時間（秒）】\n近接フローバフの持続時間です。\n推奨: 3-5秒",

                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "【コンボ速度ボーナス (%)】\nコンボ連携時の追加攻撃速度ボーナスです。\n推奨: 5-10%",

                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "【命中時移動速度ボーナス (%)】\nクロスボウのボルトが敵に命中すると移動速度が増加します。\n推奨: 10-15%",

                ["Tier2_CrossbowExpert_BuffDuration"] = "【バフ持続時間（秒）】\n命中成功後の速度バフの持続時間です。\n推奨: 3-5秒",

                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "【バフ中リロード速度ボーナス (%)】\n命中バフが有効な間リロード速度が増加します。\n推奨: 10-15%",

                ["Tier2_BowExpert_StaminaReduction"] = "【2連撃コンボ時スタミナ削減 (%)】\n弓で2回連続命中後のスタミナ消費が削減されます。\n推奨: 10-15%",

                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "【次の矢の引き速度ボーナス (%)】\nコンボ成功後、次の矢の引き速度が増加します。\n推奨: 10-20%",

                ["Tier2_BowExpert_BuffDuration"] = "【バフ持続時間（秒）】\nコンボバフの持続時間です。\n推奨: 4-6秒",

                ["Tier2_MobileCast_MoveSpeedBonus"] = "【詠唱中移動速度ボーナス (%)】\n杖の魔法詠唱中の移動速度ボーナスです。\n推奨: 8-12%",

                ["Tier2_MobileCast_EitrReduction"] = "【エイトル消費削減 (%)】\n杖の魔法のエイトル消費を削減します。\n推奨: 8-15%",

                ["Tier2_MobileCast_CastMoveSpeed"] = "【杖詠唱中移動速度 (%)】\n杖攻撃をチャネリング中の基本移動速度です。\n推奨: 3-6%",

                ["Tier3_Practitioner1_MeleeSkillBonus"] = "【近接武器スキルボーナス】\n全ての近接武器スキルレベルを増加させます。\n推奨: 5-10",

                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "【クロスボウスキルボーナス】\nクロスボウスキルレベルを増加させます。\n推奨: 5-10",

                ["Tier3_Practitioner2_StaffSkillBonus"] = "【杖スキルボーナス】\n杖スキルレベル（エレメンタルマギ）を増加させます。\n推奨: 5-10",

                ["Tier3_Practitioner2_BowSkillBonus"] = "【弓スキルボーナス】\n弓スキルレベルを増加させます。\n推奨: 5-10",

                ["Tier4_Energizer_FoodConsumptionReduction"] = "【食料消費速度低下 (%)】\n食料の消費速度を遅くし、バフがより長く持続します。\n推奨: 10-20%",

                ["Tier4_Captain_ShipSpeedBonus"] = "【船速度ボーナス (%)】\n航海速度を増加させます。\n推奨: 10-20%",

                ["Tier5_JumpMaster_JumpSkillBonus"] = "【ジャンプスキルボーナス】\nジャンプスキルレベルを増加させます。\n推奨: 5-15",

                ["Tier5_JumpMaster_JumpStaminaReduction"] = "【ジャンプスタミナ削減 (%)】\nジャンプ時のスタミナ消費を削減します。\n推奨: 10-20%",

                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "【近接攻撃速度ボーナス (%)】\n近接武器の攻撃速度を増加させます。\n推奨: 5-8%",

                ["Tier6_Dexterity_MoveSpeedBonus"] = "【移動速度ボーナス (%)】\n全体的な移動速度を増加させます。\n推奨: 3-8%",

                ["Tier6_Endurance_StaminaMaxBonus"] = "【最大スタミナボーナス】\n最大スタミナ量を増加させます。\n推奨: 20-40",

                ["Tier6_Intellect_EitrMaxBonus"] = "【最大エイトルボーナス】\n魔法用の最大エイトル量を増加させます。\n推奨: 30-50",

                ["Tier7_Master_RunSkillBonus"] = "【走行スキルボーナス】\n走行スキルレベルを増加させます。\n推奨: 5-15",

                ["Tier7_Master_JumpSkillBonus"] = "【ジャンプスキルボーナス】\nジャンプスキルレベルを増加させます。\n推奨: 5-15",

                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "【近接攻撃速度ボーナス (%)】\n近接攻撃速度の最終強化です。\n推奨: 5-10%",

                ["Tier8_MeleeAccel_TripleComboBonus"] = "【3連撃時次攻撃速度ボーナス (%)】\n3連撃コンボ後、次の攻撃速度が大幅に増加します。\n推奨: 20-30%",

                ["Tier8_CrossbowAccel_ReloadSpeed"] = "【リロード速度ボーナス (%)】\nクロスボウのリロード速度の最終強化です。\n推奨: 25-35%",

                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "【リロード中移動速度 (%)】\nクロスボウのリロード中の移動速度です。\n推奨: 20-30%",

                ["Tier8_BowAccel_DrawSpeed"] = "【引き速度ボーナス (%)】\n弓の引き速度の最終強化です。\n推奨: 15-20%",

                ["Tier8_BowAccel_DrawMoveSpeed"] = "【引き中移動速度 (%)】\n弓の弦を引いている間の移動速度です。\n推奨: 10-20%",

                ["Tier8_CastAccel_MagicAttackSpeed"] = "【魔法攻撃速度ボーナス (%)】\n魔法攻撃速度の最終強化です。\n推奨: 5-10%",

                ["Tier8_CastAccel_TripleEitrRecovery"] = "【3連撃時エイトル最大回復率 (%)】\n3回の魔法コンボ後、エイトル回復速度が増加します。\n推奨: 10-15%",

                ["Tier1_AgilityBase_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_MeleeFlow_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_CrossbowExpert_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_BowExpert_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_MobileCast_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier3_Practitioner1_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier3_Practitioner2_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier4_Energizer_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier4_Captain_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier5_JumpMaster_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier6_Dexterity_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier6_Endurance_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier6_Intellect_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier7_Master_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier8_MeleeAccel_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier8_CrossbowAccel_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier8_BowAccel_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier8_CastAccel_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier0_AttackExpert_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier1_BaseAttack_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_MeleeSpec_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_BowSpec_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_CrossbowSpec_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier2_StaffSpec_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier3_AttackBoost_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier4_MeleeEnhance_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier4_PrecisionAttack_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier4_RangedEnhance_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier5_SpecialStat_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier6_WeakPointAttack_PointsPerLevel"] = "【レベルごとの必要ポイント】\nレベルアップ1回につき消費するスキルポイントです。\n推奨: 2",

                ["Tier6_ComboFinisher_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier6_TwoHandCrush_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

                ["Tier6_ElementalAttack_RequiredPoints"] = "【必要ポイント】\nこのノードを解放するために必要なスキルポイントです。",

            };
        }
    }
}

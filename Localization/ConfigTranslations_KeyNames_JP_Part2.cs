using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJapaneseKeyNames_Part2()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // 剣ツリー (旧形式) - 20キー
                // ============================================
                ["Sword_Expert_DamageIncrease"] = "Tier 0: 剣ダメージ増加 (%)",
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: 攻撃速度ボーナス (%)",
                ["Sword_ComboSlash_Bonus"] = "Tier 2: コンボ攻撃ボーナス (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: バフ持続時間 (秒)",
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: ダメージボーナス",
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: 攻撃速度ボーナス (%)",
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: バフ持続時間 (秒)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: チャージ攻撃ボーナス (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: 押しつけ距離 (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: スタミナコスト",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: クールダウン (秒)",
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: 1撃目ダメージ率 (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: 2撃目ダメージ率 (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: 3撃目ダメージ率 (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: 初期ダッシュ距離 (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: 側面移動距離 (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: スタミナコスト",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: クールダウン (秒)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: 移動速度 (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: 攻撃速度ボーナス (%)",

                // ============================================
                // 槍ツリー - 35キー
                // ============================================

                // === Tier 0: 槍エキスパート (6) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: [槍エキスパート] 必要ポイント",
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: [槍エキスパート] 2連撃攻撃速度ボーナス (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: [槍エキスパート] 2連撃ダメージボーナス (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: [槍エキスパート] 効果持続時間 (秒)",
                ["Tier0_SpearExpert_ProcChance"] = "Tier 0: [槍エキスパート] 発動確率 (%)",
                ["Tier0_SpearExpert_SpeedBoost"] = "Tier 0: [槍エキスパート] 速度強化 (%)",

                // === Tier 1: 急所突き (2) ===
                ["Tier1_QuickStrike_RequiredPoints"] = "Tier 1: [急所突き] 必要ポイント",
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: [急所突き] クリティカルダメージボーナス (%)",

                // === Tier 2: 投槍 (3) ===
                ["Tier2_Throw_RequiredPoints"] = "Tier 2: [投槍] 必要ポイント",
                ["Tier2_Throw_Cooldown"] = "Tier 2: [投槍] クールダウン (秒)",
                ["Tier2_Throw_DamageMultiplier"] = "Tier 2: [投槍] ダメージ倍率 (%)",

                // === Tier 3-1: 連続刺突 (2) ===
                ["Tier3_Pierce_RequiredPoints"] = "Tier 3-1: [連続刺突] 必要ポイント",
                ["Tier3_Rapid_DamageBonus"] = "Tier 3-1: [連続刺突] 刺突ダメージボーナス",

                // === Tier 3-2: 速槍 (1) ===
                ["Tier3_QuickSpear_AttackSpeed"] = "Tier 3-2: [速槍] 攻撃速度 (%)",

                // === Tier 4-1: 回避突き (3) ===
                ["Tier4_Evasion_RequiredPoints"] = "Tier 4-1: [回避突き] 必要ポイント",
                ["Tier4_Evasion_EvasionBonus"] = "Tier 4-1: [回避突き] 攻撃時回避ボーナス (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4-1: [回避突き] スタミナコスト軽減 (%)",

                // === Tier 4-2: 二重突き (2) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4-2: [二重突き] 2連撃ダメージボーナス (%)",
                ["Tier4_Dual_Duration"] = "Tier 4-2: [二重突き] バフ持続時間 (秒)",

                // === Tier 5-1: 貫通槍 (6) ===
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5-1: [貫通槍] 必要ポイント",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5-1: [貫通槍] バフ持続時間 (秒)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5-1: [貫通槍] 雷ダメージ倍率 (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5-1: [貫通槍] 雷発動ヒット数",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5-1: [貫通槍] Gキークールダウン (秒)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5-1: [貫通槍] Gキースタミナコスト",

                // === Tier 5-2: コンボ槍 (8) ===
                ["Tier5_Combo_RequiredPoints"] = "Tier 5-2: [コンボ槍] 必要ポイント",
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5-2: [コンボ槍] Hキークールダウン (秒)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5-2: [コンボ槍] Hキーダメージ倍率 (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5-2: [コンボ槍] Hキースタミナコスト",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5-2: [コンボ槍] Hキーノックバック範囲 (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5-2: [コンボ槍] アクティブ効果範囲 (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5-2: [コンボ槍] バフ持続時間 (秒)",
                ["Tier5_Combo_MaxUses"] = "Tier 5-2: [コンボ槍] 最大強化投槍回数",

                // ============================================
                // 杖ツリー - 30キー
                // ============================================

                // === Tier 0: 杖エキスパート (2) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] = "Tier 0: [杖エキスパート] 属性ダメージボーナス (%)",
                ["Tier0_StaffExpert_RequiredPoints"] = "Tier 0: [杖エキスパート] 必要ポイント",

                // === Tier 1-1: 精神集中 (2) ===
                ["Tier1_MindFocus_EitrReduction"] = "Tier 1-1: [精神集中] Eitrコスト軽減 (%)",
                ["Tier1_MindFocus_RequiredPoints"] = "Tier 1-1: [精神集中] 必要ポイント",

                // === Tier 1-2: 魔力の流れ (2) ===
                ["Tier1_MagicFlow_EitrBonus"] = "Tier 1-2: [魔力の流れ] 最大Eitrボーナス",
                ["Tier1_MagicFlow_RequiredPoints"] = "Tier 1-2: [魔力の流れ] 必要ポイント",

                // === Tier 2: 魔法増幅 (4) ===
                ["Tier2_MagicAmplify_Chance"] = "Tier 2: [魔法増幅] 発動確率 (%)",
                ["Tier2_MagicAmplify_DamageBonus"] = "Tier 2: [魔法増幅] 属性ダメージボーナス (%)",
                ["Tier2_MagicAmplify_EitrCostIncrease"] = "Tier 2: [魔法増幅] Eitrコスト増加 (%)",
                ["Tier2_MagicAmplify_RequiredPoints"] = "Tier 2: [魔法増幅] 必要ポイント",

                // === Tier 3: 属性 (6) ===
                ["Tier3_FrostElement_DamageBonus"] = "Tier 3-1: [氷属性] ダメージボーナス",
                ["Tier3_FrostElement_RequiredPoints"] = "Tier 3-1: [氷属性] 必要ポイント",
                ["Tier3_FireElement_DamageBonus"] = "Tier 3-2: [火属性] ダメージボーナス",
                ["Tier3_FireElement_RequiredPoints"] = "Tier 3-2: [火属性] 必要ポイント",
                ["Tier3_LightningElement_DamageBonus"] = "Tier 3-3: [雷属性] ダメージボーナス",
                ["Tier3_LightningElement_RequiredPoints"] = "Tier 3-3: [雷属性] 必要ポイント",

                // === Tier 4: ラッキーマナ (2) ===
                ["Tier4_LuckyMana_Chance"] = "Tier 4: [ラッキーマナ] 無消費詠唱発動確率 (%)",
                ["Tier4_LuckyMana_RequiredPoints"] = "Tier 4: [ラッキーマナ] 必要ポイント",

                // === Tier 5-1: 二重詠唱 (6) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] = "Tier 5-1: [二重詠唱] 追加射体数",
                ["Tier5_DoubleCast_ProjectileDamagePercent"] = "Tier 5-1: [二重詠唱] 射体ダメージ率 (%)",
                ["Tier5_DoubleCast_AngleOffset"] = "Tier 5-1: [二重詠唱] 角度オフセット (未使用)",
                ["Tier5_DoubleCast_EitrCost"] = "Tier 5-1: [二重詠唱] Eitrコスト",
                ["Tier5_DoubleCast_Cooldown"] = "Tier 5-1: [二重詠唱] クールダウン (秒)",
                ["Tier5_DoubleCast_RequiredPoints"] = "Tier 5-1: [二重詠唱] 必要ポイント",

                // === Tier 5-2: 回復 (5) ===
                ["Tier5_InstantAreaHeal_Cooldown"] = "Tier 5-2: [回復] クールダウン (秒)",
                ["Tier5_InstantAreaHeal_EitrCost"] = "Tier 5-2: [回復] Eitrコスト",
                ["Tier5_InstantAreaHeal_HealPercent"] = "Tier 5-2: [回復] 回復量 (最大HPの%)",
                ["Tier5_InstantAreaHeal_Range"] = "Tier 5-2: [回復] 回復範囲 (m)",
                ["Tier5_InstantAreaHeal_RequiredPoints"] = "Tier 5-2: [回復] 必要ポイント",

                // ============================================
                // クロスボウツリー - 34キー
                // ============================================

                // === Tier 0: クロスボウエキスパート (2) ===
                ["Tier0_CrossbowExpert_DamageBonus"] = "Tier 0: [クロスボウエキスパート] クロスボウダメージボーナス (%)",
                ["Tier0_CrossbowExpert_RequiredPoints"] = "Tier 0: [クロスボウエキスパート] 必要ポイント",

                // === Tier 1: 速射 (6) ===
                ["Tier1_RapidFire_Chance"] = "Tier 1: [速射] 発動確率 (%)",
                ["Tier1_RapidFire_ShotCount"] = "Tier 1: [速射] 射撃数",
                ["Tier1_RapidFire_DamagePercent"] = "Tier 1: [速射] ダメージ率 (%)",
                ["Tier1_RapidFire_Delay"] = "Tier 1: [速射] 射撃間隔 (秒)",
                ["Tier1_RapidFire_BoltConsumption"] = "Tier 1: [速射] ボルト消費数",
                ["Tier1_RapidFire_RequiredPoints"] = "Tier 1: [速射] 必要ポイント",

                // === Tier 2 (7) ===
                ["Tier2_BalancedAim_KnockbackChance"] = "Tier 2-1: [バランス照準] ノックバック発動確率 (%)",
                ["Tier2_BalancedAim_KnockbackDistance"] = "Tier 2-1: [バランス照準] ノックバック距離 (m)",
                ["Tier2_BalancedAim_RequiredPoints"] = "Tier 2-1: [バランス照準] 必要ポイント",
                ["Tier2_RapidReload_SpeedIncrease"] = "Tier 2-2: [速リロード] リロード速度増加 (%)",
                ["Tier2_RapidReload_RequiredPoints"] = "Tier 2-2: [速リロード] 必要ポイント",
                ["Tier2_HonestShot_DamageBonus"] = "Tier 2-3: [正直な一撃] ダメージボーナス (%)",
                ["Tier2_HonestShot_RequiredPoints"] = "Tier 2-3: [正直な一撃] 必要ポイント",

                // === Tier 3: 自動リロード (2) ===
                ["Tier3_AutoReload_Chance"] = "Tier 3: [自動リロード] 発動確率 (%)",
                ["Tier3_AutoReload_RequiredPoints"] = "Tier 3: [自動リロード] 必要ポイント",

                // === Tier 4 (9) ===
                ["Tier4_RapidFireLv2_Chance"] = "Tier 4-1: [速射Lv2] 発動確率 (%)",
                ["Tier4_RapidFireLv2_ShotCount"] = "Tier 4-1: [速射Lv2] 射撃数",
                ["Tier4_RapidFireLv2_DamagePercent"] = "Tier 4-1: [速射Lv2] ダメージ率 (%)",
                ["Tier4_RapidFireLv2_Delay"] = "Tier 4-1: [速射Lv2] 射撃間隔 (秒)",
                ["Tier4_RapidFireLv2_BoltConsumption"] = "Tier 4-1: [速射Lv2] ボルト消費数",
                ["Tier4_RapidFireLv2_RequiredPoints"] = "Tier 4-1: [速射Lv2] 必要ポイント",
                ["Tier4_FinalStrike_HpThreshold"] = "Tier 4-2: [先制攻撃] 敵HP閾値 (%)",
                ["Tier4_FinalStrike_DamageBonus"] = "Tier 4-2: [先制攻撃] ボーナスダメージ (%)",
                ["Tier4_FinalStrike_RequiredPoints"] = "Tier 4-2: [先制攻撃] 必要ポイント",

                // === Tier 5: 一撃 (5) ===
                ["Tier5_OneShot_Duration"] = "Tier 5: [一撃] バフ持続時間 (秒)",
                ["Tier5_OneShot_DamageBonus"] = "Tier 5: [一撃] ダメージボーナス (%)",
                ["Tier5_OneShot_KnockbackDistance"] = "Tier 5: [一撃] ノックバック距離 (m)",
                ["Tier5_OneShot_Cooldown"] = "Tier 5: [一撃] クールダウン (秒)",
                ["Tier5_OneShot_RequiredPoints"] = "Tier 5: [一撃] 必要ポイント",

                // ============================================
                // ナイフツリー - 32キー
                // ============================================

                // === Tier 0: ナイフエキスパート (2) ===
                ["Tier0_KnifeExpert_BackstabBonus"] = "Tier 0: [ナイフエキスパート] 背後攻撃ボーナス (%)",
                ["Tier0_KnifeExpert_RequiredPoints"] = "Tier 0: [ナイフエキスパート] 必要ポイント",

                // === Tier 1〜8 ===
                ["Tier1_Evasion_Chance"] = "Tier 1: [回避習得] 発動確率 (%)",
                ["Tier1_Evasion_Duration"] = "Tier 1: [回避習得] 持続時間 (秒)",
                ["Tier1_Evasion_RequiredPoints"] = "Tier 1: [回避習得] 必要ポイント",
                ["Tier2_FastMove_MoveSpeedBonus"] = "Tier 2: [素早い移動] 移動速度ボーナス (%)",
                ["Tier2_FastMove_RequiredPoints"] = "Tier 2: [素早い移動] 必要ポイント",
                ["Tier3_CombatMastery_DamageBonus"] = "Tier 3: [素早い攻撃] 斬撃/刺突ダメージボーナス",
                ["Tier3_CombatMastery_BuffDuration"] = "Tier 3: [素早い攻撃] バフ持続時間 (秒)",
                ["Tier3_CombatMastery_RequiredPoints"] = "Tier 3: [素早い攻撃] 必要ポイント",
                ["Tier4_AttackEvasion_EvasionBonus"] = "Tier 4: [クリティカル習得] 回避ボーナス (%)",
                ["Tier4_AttackEvasion_BuffDuration"] = "Tier 4: [クリティカル習得] バフ持続時間 (秒)",
                ["Tier4_AttackEvasion_Cooldown"] = "Tier 4: [クリティカル習得] クールダウン (秒)",
                ["Tier4_AttackEvasion_RequiredPoints"] = "Tier 4: [クリティカル習得] 必要ポイント",
                ["Tier5_CriticalDamage_DamageBonus"] = "Tier 5: [致命ダメージ] ダメージボーナス (%)",
                ["Tier5_CriticalDamage_RequiredPoints"] = "Tier 5: [致命ダメージ] 必要ポイント",
                ["Tier6_Assassin_CritDamageBonus"] = "Tier 6: [アサシン] クリティカルダメージボーナス (%)",
                ["Tier6_Assassin_CritChanceBonus"] = "Tier 6: [アサシン] クリティカル確率ボーナス (%)",
                ["Tier6_Assassin_RequiredPoints"] = "Tier 6: [アサシン] 必要ポイント",
                ["Tier7_Assassination_StaggerChance"] = "Tier 7: [暗殺] スタガー発動確率 (%)",
                ["Tier7_Assassination_RequiredComboHits"] = "Tier 7: [暗殺] 必要コンボヒット数",
                ["Tier7_Assassination_RequiredPoints"] = "Tier 7: [暗殺] 必要ポイント",
                ["Tier8_AssassinHeart_CritDamageMultiplier"] = "Tier 8: [暗殺者の心臓] クリティカルダメージ倍率",
                ["Tier8_AssassinHeart_Duration"] = "Tier 8: [暗殺者の心臓] バフ持続時間 (秒)",
                ["Tier8_AssassinHeart_StaminaCost"] = "Tier 8: [暗殺者の心臓] スタミナコスト",
                ["Tier8_AssassinHeart_Cooldown"] = "Tier 8: [暗殺者の心臓] クールダウン (秒)",
                ["Tier8_AssassinHeart_TeleportRange"] = "Tier 8: [暗殺者の心臓] テレポート範囲 (m)",
                ["Tier8_AssassinHeart_TeleportBackDistance"] = "Tier 8: [暗殺者の心臓] 背後距離 (m)",
                ["Tier8_AssassinHeart_StunDuration"] = "Tier 8: [暗殺者の心臓] スタン持続時間 (秒)",
                ["Tier8_AssassinHeart_ComboAttackCount"] = "Tier 8: [暗殺者の心臓] コンボ攻撃回数",
                ["Tier8_AssassinHeart_AttackInterval"] = "Tier 8: [暗殺者の心臓] 攻撃間隔 (秒)",
                ["Tier8_AssassinHeart_RequiredPoints"] = "Tier 8: [暗殺者の心臓] 必要ポイント",

                // ============================================
                // 剣ツリー (新形式) - 33キー
                // ============================================

                // === Tier 0: 剣エキスパート (2) ===
                ["Tier0_SwordExpert_DamageBonus"] = "Tier 0: [剣エキスパート] 剣ダメージボーナス (%)",
                ["Tier0_SwordExpert_RequiredPoints"] = "Tier 0: [剣エキスパート] 必要ポイント",

                ["Tier1_FastSlash_RequiredPoints"] = "Tier 1-1: [速斬り] 必要ポイント",
                ["Tier1_FastSlash_AttackSpeedBonus"] = "Tier 1-1: [速斬り] 攻撃速度ボーナス (%)",
                ["Tier1_CounterStance_RequiredPoints"] = "Tier 1-2: [反撃構え] 必要ポイント",
                ["Tier1_CounterStance_Duration"] = "Tier 1-2: [反撃構え] バフ持続時間 (秒)",
                ["Tier1_CounterStance_DefenseBonus"] = "Tier 1-2: [反撃構え] 防御ボーナス (%)",
                ["Tier2_ComboSlash_RequiredPoints"] = "Tier 2: [コンボ斬り] 必要ポイント",
                ["Tier2_ComboSlash_DamageBonus"] = "Tier 2: [コンボ斬り] ダメージボーナス (%)",
                ["Tier2_ComboSlash_BuffDuration"] = "Tier 2: [コンボ斬り] バフ持続時間 (秒)",
                ["Tier3_Riposte_RequiredPoints"] = "Tier 3: [リポスト] 必要ポイント",
                ["Tier3_Riposte_DamageBonus"] = "Tier 3: [リポスト] 斬撃ダメージボーナス",
                ["Tier4_AllInOne_RequiredPoints"] = "Tier 4-1: [万能] 必要ポイント",
                ["Tier4_AllInOne_AttackBonus"] = "Tier 4-1: [万能] 攻撃ボーナス (%)",
                ["Tier4_AllInOne_DefenseBonus"] = "Tier 4-1: [万能] 防御ボーナス",
                ["Tier4_TrueDuel_RequiredPoints"] = "Tier 4-2: [真の決闘] 必要ポイント",
                ["Tier4_TrueDuel_AttackSpeedBonus"] = "Tier 4-2: [真の決闘] 攻撃速度ボーナス (%)",
                ["Tier5_ParryRush_RequiredPoints"] = "Tier 5: [パリィラッシュ] 必要ポイント",
                ["Tier5_ParryRush_BuffDuration"] = "Tier 5: [パリィラッシュ] バフ持続時間 (秒)",
                ["Tier5_ParryRush_DamageBonus"] = "Tier 5: [パリィラッシュ] ダメージボーナス (%)",
                ["Tier5_ParryRush_PushDistance"] = "Tier 5: [パリィラッシュ] 押しつけ距離 (m)",
                ["Tier5_ParryRush_StaminaCost"] = "Tier 5: [パリィラッシュ] スタミナコスト",
                ["Tier5_ParryRush_Cooldown"] = "Tier 5: [パリィラッシュ] クールダウン (秒)",
                ["Tier6_RushSlash_RequiredPoints"] = "Tier 6: [ラッシュ斬り] 必要ポイント",
                ["Tier6_RushSlash_Hit1DamageRatio"] = "Tier 6: [ラッシュ斬り] 1撃目ダメージ率 (%)",
                ["Tier6_RushSlash_Hit2DamageRatio"] = "Tier 6: [ラッシュ斬り] 2撃目ダメージ率 (%)",
                ["Tier6_RushSlash_Hit3DamageRatio"] = "Tier 6: [ラッシュ斬り] 3撃目ダメージ率 (%)",
                ["Tier6_RushSlash_InitialDistance"] = "Tier 6: [ラッシュ斬り] 初期ダッシュ距離 (m)",
                ["Tier6_RushSlash_SideDistance"] = "Tier 6: [ラッシュ斬り] 側面移動距離 (m)",
                ["Tier6_RushSlash_StaminaCost"] = "Tier 6: [ラッシュ斬り] スタミナコスト",
                ["Tier6_RushSlash_Cooldown"] = "Tier 6: [ラッシュ斬り] クールダウン (秒)",
                ["Tier6_RushSlash_MoveSpeed"] = "Tier 6: [ラッシュ斬り] 移動速度 (m/s)",
                ["Tier6_RushSlash_AttackSpeedBonus"] = "Tier 6: [ラッシュ斬り] 攻撃速度ボーナス (%)",

                // ============================================
                // メイスツリー - 34キー
                // ============================================

                ["Tier0_MaceExpert_DamageBonus"] = "Tier 0: [メイスエキスパート] ダメージボーナス (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: [メイスエキスパート] スタン確率 (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: [メイスエキスパート] スタン持続時間 (秒)",
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: [メイスエキスパート] 必要ポイント",
                ["Tier1_MaceExpert_DamageBonus"] = "Tier 1: [メイスダメージ強化] ダメージボーナス (%)",
                ["Tier1_MaceExpert_RequiredPoints"] = "Tier 1: [メイスダメージ強化] 必要ポイント",
                ["Tier2_StunBoost_StunChanceBonus"] = "Tier 2: [スタン強化] スタン確率ボーナス (%)",
                ["Tier2_StunBoost_StunDurationBonus"] = "Tier 2: [スタン強化] スタン持続時間ボーナス (秒)",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: [スタン強化] 必要ポイント",
                ["Tier3_SpinStrike_DamageBonus"] = "Tier 3-1: [スピン斬り] ダメージボーナス (%)",
                ["Tier3_SpinStrike_Range"] = "Tier 3-1: [スピン斬り] AoE範囲 (m)",
                ["Tier3_Guard_RequiredPoints"] = "Tier 3-1: [スピン斬り] 必要ポイント",
                ["Tier3_HeavyStrike_DamageBonus"] = "Tier 3-2: [ヘビーストライク] 打撃ボーナス",
                ["Tier3_HeavyStrike_RequiredPoints"] = "Tier 3-2: [ヘビーストライク] 必要ポイント",
                ["Tier4_Push_KnockbackChance"] = "Tier 4: [プッシュ] ノックバック確率 (%)",
                ["Tier4_Push_RequiredPoints"] = "Tier 4: [プッシュ] 必要ポイント",
                ["Tier5_Tank_HealthBonus"] = "Tier 5-1: [タンク] 体力ボーナス (%)",
                ["Tier5_Tank_DamageReduction"] = "Tier 5-1: [タンク] 被ダメージ軽減 (%)",
                ["Tier5_Tank_RequiredPoints"] = "Tier 5-1: [タンク] 必要ポイント",
                ["Tier5_DPS_DamageBonus"] = "Tier 5-2: [DPS強化] ダメージボーナス (%)",
                ["Tier5_DPS_RequiredPoints"] = "Tier 5-2: [DPS強化] 必要ポイント",
                ["Tier6_Sokgong_AttackSpeedBonus"] = "Tier 6: [速攻] 攻撃速度ボーナス (%)",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: [速攻] 必要ポイント",
                ["Tier7_FuryHammer_NormalHitMultiplier"] = "Tier 7-1: [怒りの鎚] 1-4撃目ダメージ倍率 (%)",
                ["Tier7_FuryHammer_FinalHitMultiplier"] = "Tier 7-1: [怒りの鎚] 5撃目最終ダメージ倍率 (%)",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7-1: [怒りの鎚] スタミナコスト",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7-1: [怒りの鎚] クールダウン (秒)",
                ["Tier7_FuryHammer_AoeRadius"] = "Tier 7-1: [怒りの鎚] AOE範囲 (m)",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7-1: [怒りの鎚] 必要ポイント",
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7-2: [シールドチャージ] クールダウン (秒)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7-2: [シールドチャージ] スタミナコスト",
                ["Tier7_ShieldCharge_DamagePercent"] = "Tier 7-2: [シールドチャージ] ブロック力ダメージ率 (%)",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7-2: [シールドチャージ] 必要ポイント",

                // ============================================
                // ポールアームツリー - 37キー
                // ============================================

                ["Tier0_PolearmExpert_AttackRangeBonus"] = "Tier 0: [ポールアームエキスパート] 攻撃範囲ボーナス (%)",
                ["Tier0_PolearmExpert_RequiredPoints"] = "Tier 0: [ポールアームエキスパート] 必要ポイント",
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] = "Tier 1: [回転斬り] ホイール攻撃ダメージボーナス (%)",
                ["Tier1_SpinWheel_RequiredPoints"] = "Tier 1: [回転斬り] 必要ポイント",
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] = "Tier 2-1: [ポールアーム強化] 刺突ダメージボーナス (固定)",
                ["Tier2-1_PolearmBoost_RequiredPoints"] = "Tier 2-1: [ポールアーム強化] 必要ポイント",
                ["Tier2-2_HeroStrike_KnockbackChance"] = "Tier 2-2: [英雄打撃] スタガー確率 (%)",
                ["Tier2-2_HeroStrike_RequiredPoints"] = "Tier 2-2: [英雄打撃] 必要ポイント",
                ["Tier3_AreaCombo_DoubleHitBonus"] = "Tier 3: [広範囲コンボ] 二連撃ダメージボーナス (%)",
                ["Tier3_AreaCombo_DoubleHitDuration"] = "Tier 3: [広範囲コンボ] 二連撃バフ持続時間 (秒)",
                ["Tier3_AreaCombo_RequiredPoints"] = "Tier 3: [広範囲コンボ] 必要ポイント",
                ["Tier4-1_StormSlash_ExplosionBonus"] = "Tier 4-1: [嵐の斬り] 雷ダメージボーナス",
                ["Tier4-1_GroundWheel_RequiredPoints"] = "Tier 4-1: [嵐の斬り] 必要ポイント",
                ["Tier4-2_MoonSlash_AttackRangeBonus"] = "Tier 4-2: [三日月斬り] 攻撃範囲ボーナス (%)",
                ["Tier4-2_MoonSlash_StaminaReduction"] = "Tier 4-2: [三日月斬り] スタミナ軽減 (%)",
                ["Tier4-2_MoonSlash_RequiredPoints"] = "Tier 4-2: [三日月斬り] 必要ポイント",
                ["Tier4-3_Suppress_DamageBonus"] = "Tier 4-3: [制圧攻撃] ダメージボーナス (%)",
                ["Tier4-3_Suppress_RequiredPoints"] = "Tier 4-3: [制圧攻撃] 必要ポイント",
                ["Tier5_PierceCharge_DashDistance"] = "Tier 5: [貫通突撃] ダッシュ距離 (m)",
                ["Tier5_PierceCharge_FirstHitDamageBonus"] = "Tier 5: [貫通突撃] 初撃ダメージボーナス (%)",
                ["Tier5_PierceCharge_AoeDamageBonus"] = "Tier 5: [貫通突撃] AOEノックバックダメージボーナス (%)",
                ["Tier5_PierceCharge_AoeAngle"] = "Tier 5: [貫通突撃] AOE角度 (度)",
                ["Tier5_PierceCharge_AoeRadius"] = "Tier 5: [貫通突撃] AOE範囲 (m)",
                ["Tier5_PierceCharge_KnockbackDistance"] = "Tier 5: [貫通突撃] ノックバック距離 (m)",
                ["Tier5_PierceCharge_StaminaCost"] = "Tier 5: [貫通突撃] スタミナコスト",
                ["Tier5_PierceCharge_Cooldown"] = "Tier 5: [貫通突撃] クールダウン (秒)",
                ["Tier5_PierceCharge_RequiredPoints"] = "Tier 5: [貫通突撃] 必要ポイント",

                // ============================================
                // アーチャー職業スキル
                // ============================================
                ["Archer_MultiShot_ArrowCount"] = "Lv1 マルチショット: 矢数",
                ["Archer_MultiShot_ArrowConsumption"] = "Lv1 マルチショット: 矢消費数",
                ["Archer_MultiShot_DamagePercent"] = "Lv1 マルチショット: 矢あたりダメージ (%)",
                ["Archer_MultiShot_Cooldown"] = "Lv1 マルチショット: クールダウン (秒)",
                ["Archer_MultiShot_Charges"] = "Lv1 マルチショット: ボーナス射撃数",
                ["Archer_MultiShot_StaminaCost"] = "Lv1 マルチショット: スタミナコスト",
                ["Archer_JumpHeightBonus"] = "Lv1 パッシブ: ジャンプ高度ボーナス (%)",
                ["Archer_FallDamageReduction"] = "Lv1 パッシブ: 落下ダメージ軽減 (%)",
                ["Archer_Lv2_BonusArrows"] = "Lv2 マルチショット: ボーナス矢数",
                ["Archer_Lv2_DamagePercent"] = "Lv2 マルチショット: 矢あたりダメージ (%)",
                ["Archer_Lv3_BonusArrows"] = "Lv3 マルチショット: ボーナス矢数",
                ["Archer_Lv3_DamagePercent"] = "Lv3 マルチショット: 矢あたりダメージ (%)",
                ["Archer_Lv4_BonusArrows"] = "Lv4 マルチショット: ボーナス矢数",
                ["Archer_Lv4_DamagePercent"] = "Lv4 マルチショット: 矢あたりダメージ (%)",
                ["Archer_Lv5_BonusArrows"] = "Lv5 マルチショット: ボーナス矢数",
                ["Archer_Lv5_DamagePercent"] = "Lv5 マルチショット: 矢あたりダメージ (%)",
                ["Archer_Lv5_BonusCharges"] = "Lv5 マルチショット: ボーナス射撃数",
                ["Archer_Lv2_JumpHeightBonus"] = "Lv2 パッシブ: ジャンプ高度ボーナス (%)",
                ["Archer_Lv3_JumpHeightBonus"] = "Lv3 パッシブ: ジャンプ高度ボーナス (%)",
                ["Archer_Lv4_JumpHeightBonus"] = "Lv4 パッシブ: ジャンプ高度ボーナス (%)",
                ["Archer_Lv5_JumpHeightBonus"] = "Lv5 パッシブ: ジャンプ高度ボーナス (%)",
                ["Archer_Lv3_FallDamageReduction"] = "Lv3 パッシブ: 落下ダメージ軽減 (%)",
                ["Archer_Lv4_FallDamageReduction"] = "Lv4 パッシブ: 落下ダメージ軽減 (%)",
                ["Archer_Lv5_FallDamageReduction"] = "Lv5 パッシブ: 落下ダメージ軽減 (%)",
                ["Archer_ElementalResistPerLevel"] = "パッシブ: レベルごとの属性耐性 (%)",

                // ============================================
                // メイジ職業スキル
                // ============================================
                ["Mage_AOE_Range"] = "アクティブ: 範囲 (m)",
                ["Mage_AOE_Max_Targets"] = "アクティブ: 最大ターゲット数",
                ["Mage_Eitr_Cost"] = "アクティブ: Eitrコスト",
                ["Mage_Damage_Multiplier"] = "アクティブ: ダメージ倍率 (%)",
                ["Mage_Cooldown"] = "アクティブ: クールダウン (秒)",
                ["Mage_Elemental_Resistance"] = "パッシブ: 属性耐性 (%)",

                // ============================================
                // タンカー職業スキル
                // ============================================
                ["Tanker_Taunt_Cooldown"] = "ウォークライ: クールダウン (秒)",
                ["Tanker_Taunt_StaminaCost"] = "ウォークライ: スタミナコスト",
                ["Tanker_Taunt_Range"] = "ウォークライ: 挑発範囲 (m)",
                ["Tanker_Taunt_Duration"] = "ウォークライ: 挑発持続時間 (秒)",
                ["Tanker_Taunt_BossDuration"] = "ウォークライ: ボス挑発持続時間 (秒)",
                ["Tanker_Taunt_DamageReduction"] = "ウォークライ: ダメージ軽減 (%)",
                ["Tanker_Taunt_BuffDuration"] = "ウォークライ: バフ持続時間 (秒)",
                ["Tanker_Taunt_EffectHeight"] = "ウォークライ: 効果高度 (m)",
                ["Tanker_Taunt_EffectScale"] = "ウォークライ: 効果スケール",
                ["Tanker_Taunt_ReflectPercent"] = "戦吼: 反射ダメージ割合 (%)",
                ["Tanker_Passive_DamageReduction"] = "パッシブ: ダメージ軽減 (%)",

                // ============================================
                // ローグ職業スキル
                // ============================================
                ["Rogue_ShadowStrike_Cooldown"] = "シャドーストライク: クールダウン (秒)",
                ["Rogue_ShadowStrike_StaminaCost"] = "シャドーストライク: スタミナコスト",
                ["Rogue_ShadowStrike_AttackBonus"] = "シャドーストライク: 攻撃ボーナス (%)",
                ["Rogue_ShadowStrike_BuffDuration"] = "シャドーストライク: バフ持続時間 (秒)",
                ["Rogue_Poison_Range"] = "毒爆発: 範囲 (m)",
                ["Rogue_Poison_InstantDamage"] = "毒爆発: 即時ダメージ",
                ["Rogue_Poison_DotDamage"] = "毒爆発: DoTダメージ/秒",
                ["Rogue_Poison_DotDuration"] = "毒爆発: DoT持続時間 (秒)",
                ["Rogue_Poison_VFXCount"] = "毒爆発: VFX数",
                ["Rogue_Poison_VFXInterval"] = "毒爆発: 間隔 (秒)",
                ["Rogue_Lv2_Cooldown"] = "Lv2: クールダウン (秒)",
                ["Rogue_Lv3_Cooldown"] = "Lv3: クールダウン (秒)",
                ["Rogue_Lv4_Cooldown"] = "Lv4: クールダウン (秒)",
                ["Rogue_Lv5_Cooldown"] = "Lv5: クールダウン (秒)",
                ["Rogue_Lv2_AttackBonus"] = "Lv2: 攻撃バフ (%)",
                ["Rogue_Lv3_AttackBonus"] = "Lv3: 攻撃バフ (%)",
                ["Rogue_Lv4_AttackBonus"] = "Lv4: 攻撃バフ (%)",
                ["Rogue_Lv5_AttackBonus"] = "Lv5: 攻撃バフ (%)",
                ["Rogue_Lv2_BuffDuration"] = "Lv2: バフ持続時間 (秒)",
                ["Rogue_Lv3_BuffDuration"] = "Lv3: バフ持続時間 (秒)",
                ["Rogue_Lv4_BuffDuration"] = "Lv4: バフ持続時間 (秒)",
                ["Rogue_Lv5_BuffDuration"] = "Lv5: バフ持続時間 (秒)",
                ["Rogue_Lv2_PoisonBlasts"] = "Lv2: 毒爆発数",
                ["Rogue_Lv3_PoisonBlasts"] = "Lv3: 毒爆発数",
                ["Rogue_Lv4_PoisonBlasts"] = "Lv4: 毒爆発数",
                ["Rogue_Lv5_PoisonBlasts"] = "Lv5: 毒爆発数",
                ["Rogue_Lv2_PoisonInstant"] = "Lv2: 毒即時ダメージ",
                ["Rogue_Lv3_PoisonInstant"] = "Lv3: 毒即時ダメージ",
                ["Rogue_Lv4_PoisonInstant"] = "Lv4: 毒即時ダメージ",
                ["Rogue_Lv5_PoisonInstant"] = "Lv5: 毒即時ダメージ",
                ["Rogue_Lv2_PoisonDot"] = "Lv2: 毒DoT/秒",
                ["Rogue_Lv3_PoisonDot"] = "Lv3: 毒DoT/秒",
                ["Rogue_Lv4_PoisonDot"] = "Lv4: 毒DoT/秒",
                ["Rogue_Lv5_PoisonDot"] = "Lv5: 毒DoT/秒",
                ["Rogue_ShadowStrike_Charges"] = "シャドーストライク: 基本チャージ数",
                ["Rogue_Lv5_BonusCharges"] = "Lv5: ボーナスチャージ数",
                ["Rogue_AttackSpeed_Bonus"] = "パッシブ: 攻撃速度ボーナス (%)",
                ["Rogue_Stamina_Reduction"] = "パッシブ: スタミナ使用軽減 (%)",
                ["Rogue_Lv1_DodgeChance"] = "Lv1 パッシブ: 回避率 (%)",
                ["Rogue_Lv2_AttackSpeed"] = "Lv2 パッシブ: 攻撃速度 (%)",
                ["Rogue_Lv3_AttackSpeed"] = "Lv3 パッシブ: 攻撃速度 (%)",
                ["Rogue_Lv4_AttackSpeed"] = "Lv4 パッシブ: 攻撃速度 (%)",
                ["Rogue_Lv5_AttackSpeed"] = "Lv5 パッシブ: 攻撃速度 (%)",
                ["Rogue_Lv2_StaminaReduction"] = "Lv2 パッシブ: スタミナ軽減 (%)",
                ["Rogue_Lv3_StaminaReduction"] = "Lv3 パッシブ: スタミナ軽減 (%)",
                ["Rogue_Lv4_StaminaReduction"] = "Lv4 パッシブ: スタミナ軽減 (%)",
                ["Rogue_Lv5_StaminaReduction"] = "Lv5 パッシブ: スタミナ軽減 (%)",
                ["Rogue_Lv2_DodgeChance"] = "Lv2 パッシブ: 回避率 (%)",
                ["Rogue_Lv3_DodgeChance"] = "Lv3 パッシブ: 回避率 (%)",
                ["Rogue_Lv4_DodgeChance"] = "Lv4 パッシブ: 回避率 (%)",
                ["Rogue_Lv5_DodgeChance"] = "Lv5 パッシブ: 回避率 (%)",
                ["Rogue_Lv1_MoveSpeed"] = "Lv1 パッシブ: 移動速度 (%)",
                ["Rogue_Lv2_MoveSpeed"] = "Lv2 パッシブ: 移動速度 (%)",
                ["Rogue_Lv3_MoveSpeed"] = "Lv3 パッシブ: 移動速度 (%)",
                ["Rogue_Lv4_MoveSpeed"] = "Lv4 パッシブ: 移動速度 (%)",
                ["Rogue_Lv5_MoveSpeed"] = "Lv5 パッシブ: 移動速度 (%)",

                // ============================================
                // パラディン職業スキル
                // ============================================
                ["Paladin_Active_Cooldown"] = "神聖回復: クールダウン (秒)",
                ["Paladin_Active_Range"] = "神聖回復: 範囲 (m)",
                ["Paladin_Active_EitrCost"] = "神聖回復: Eitrコスト",
                ["Paladin_Active_StaminaCost"] = "神聖回復: スタミナコスト",
                ["Paladin_Active_SelfHealPercent"] = "神聖回復: 自己回復率 (%)",
                ["Paladin_Active_AllyHealPercentOverTime"] = "神聖回復: 味方HoT率 (%)",
                ["Paladin_Active_Duration"] = "神聖回復: 持続時間 (秒)",
                ["Paladin_Active_Interval"] = "神聖回復: 間隔 (秒)",
                ["Paladin_Passive_ElementalResistanceReduction"] = "パッシブ: 耐性ボーナス (%)",
                ["Paladin_Lv2_SelfHealPercent"] = "Lv2: 自己回復率 (%)",
                ["Paladin_Lv2_AllyHealPercent"] = "Lv2: 味方回復率 (%/tick)",
                ["Paladin_Lv3_SelfHealPercent"] = "Lv3: 自己回復率 (%)",
                ["Paladin_Lv3_AllyHealPercent"] = "Lv3: 味方回復率 (%/tick)",
                ["Paladin_Lv3_HealRange"] = "Lv3: 回復範囲 (m)",
                ["Paladin_Lv4_SelfHealPercent"] = "Lv4: 自己回復率 (%)",
                ["Paladin_Lv4_AllyHealPercent"] = "Lv4: 味方回復率 (%/tick)",
                ["Paladin_Lv4_HealRange"] = "Lv4: 回復範囲 (m)",
                ["Paladin_Lv5_SelfHealPercent"] = "Lv5: 自己回復率 (%)",
                ["Paladin_Lv5_AllyHealPercent"] = "Lv5: 味方回復率 (%/tick)",
                ["Paladin_Lv5_HealRange"] = "Lv5: 回復範囲 (m)",
                ["Paladin_Lv5_Cooldown"] = "Lv5: クールダウン (秒)",
                ["Paladin_Lv2_ResistanceReduction"] = "Lv2: 耐性減少 (%)",
                ["Paladin_Lv3_ResistanceReduction"] = "Lv3: 耐性減少 (%)",
                ["Paladin_Lv4_ResistanceReduction"] = "Lv4: 耐性減少 (%)",
                ["Paladin_Lv5_ResistanceReduction"] = "Lv5: 耐性減少 (%)",
                ["Paladin_Lv2_StaminaBonus"] = "Lv2: スタミナボーナス",
                ["Paladin_Lv3_StaminaBonus"] = "Lv3: スタミナボーナス",
                ["Paladin_Lv4_StaminaBonus"] = "Lv4: スタミナボーナス",
                ["Paladin_Lv5_StaminaBonus"] = "Lv5: スタミナボーナス",

                // ============================================
                // バーサーカー職業スキル
                // ============================================
                ["Beserker_Active_Cooldown"] = "バーサーカーの怒り: クールダウン (秒)",
                ["Beserker_Active_StaminaCost"] = "バーサーカーの怒り: スタミナコスト",
                ["Beserker_Active_Duration"] = "バーサーカーの怒り: 持続時間 (秒)",
                ["Beserker_Active_DamagePerHealthPercent"] = "バーサーカーの怒り: HP%あたりダメージ (%)",
                ["Beserker_Active_MaxDamageBonus"] = "バーサーカーの怒り: 最大ダメージボーナス (%)",
                ["Beserker_Active_HealthThreshold"] = "バーサーカーの怒り: HP閾値 (%)",
                ["Berserker_Passive_HealthThreshold"] = "死の抵抗: HP閾値 (%)",
                ["Berserker_Passive_InvincibilityDuration"] = "死の抵抗: 無敵持続時間 (秒)",
                ["Berserker_Passive_Cooldown"] = "死の抵抗: クールダウン (秒)",
                ["Berserker_Passive_HealthBonus"] = "パッシブ: 最大HPボーナス (%)",
                ["Berserker_Lv2_CooldownReduction"] = "Lv2: 怒り CD短縮 (秒)",
                ["Berserker_Lv3_RageDamageReduction"] = "Lv3: 怒り中ダメージ減少 (%)",
                ["Berserker_Lv4_LowHpAttackBonus"] = "Lv4: 低HP攻撃ボーナス (%)",
                ["Berserker_Lv4_LowHpAttackThreshold"] = "Lv4: 低HPしきい値 (%)",
                ["Berserker_Lv5_PassiveCooldownReduction"] = "Lv5: パッシブ CD短縮 (秒)",
                ["Berserker_Lv5_InvincibilityBonus"] = "Lv5: 追加無敵時間 (秒)",

                // ============================================
                // 生産者職業スキル
                // ============================================
                ["Producer_Buff_Cooldown"] = "Lv1 祝福: クールダウン (秒)",
                ["Producer_Buff_Duration"] = "Lv1 祝福: 持続時間 (秒)",
                ["Producer_Buff_Range"] = "Lv1 祝福: 範囲 (m)",
                ["Producer_Buff_AttackBonus"] = "Lv1 祝福: 攻撃ボーナス (%)",
                ["Producer_Buff_MaxHealthBonus"] = "Lv1 祝福: 最大HPボーナス (%)",
                ["Producer_Buff_StaminaCost"] = "Lv1 祝福: スタミナコスト",
                ["Producer_FarmGrid_Lv1"] = "Lv1 パッシブ: 農場グリッド数",
                ["Producer_FarmGrid_Lv2"] = "Lv2 パッシブ: 農場グリッド数",
                ["Producer_Durability_Lv2"] = "Lv2 パッシブ: 耐久度ボーナス (%)",
                ["Producer_MaterialReduction_Lv2"] = "Lv2 パッシブ: 素材軽減 (%)",
                ["Producer_FarmGrid_Lv3"] = "Lv3 パッシブ: 農場グリッド数",
                ["Producer_Durability_Lv3"] = "Lv3 パッシブ: 耐久度ボーナス (%)",
                ["Producer_MaterialReduction_Lv3"] = "Lv3 パッシブ: 素材軽減 (%)",
                ["Producer_EnchantChance_Lv3"] = "Lv3 エンチャント: 確率 (%)",
                ["Producer_EnchantWeaponDmgMin_Lv3"] = "Lv3 エンチャント: 武器ダメージ最小 (%)",
                ["Producer_EnchantWeaponDmgMax_Lv3"] = "Lv3 エンチャント: 武器ダメージ最大 (%)",
                ["Producer_EnchantArmorMin_Lv3"] = "Lv3 エンチャント: 防御力最小 (%)",
                ["Producer_EnchantArmorMax_Lv3"] = "Lv3 エンチャント: 防御力最大 (%)",
                ["Producer_EnchantHpMin_Lv3"] = "Lv3 エンチャント: HP最小 (%)",
                ["Producer_EnchantHpMax_Lv3"] = "Lv3 エンチャント: HP最大 (%)",
                ["Producer_FarmGrid_Lv4"] = "Lv4 パッシブ: 農場グリッド数",
                ["Producer_Durability_Lv4"] = "Lv4 パッシブ: 耐久度ボーナス (%)",
                ["Producer_MaterialReduction_Lv4"] = "Lv4 パッシブ: 素材軽減 (%)",
                ["Producer_EnchantChance_Lv4"] = "Lv4 エンチャント: 確率 (%)",
                ["Producer_EnchantWeaponDmgMin_Lv4"] = "Lv4 エンチャント: 武器ダメージ最小 (%)",
                ["Producer_EnchantWeaponDmgMax_Lv4"] = "Lv4 エンチャント: 武器ダメージ最大 (%)",
                ["Producer_EnchantArmorMin_Lv4"] = "Lv4 エンチャント: 防御力最小 (%)",
                ["Producer_EnchantArmorMax_Lv4"] = "Lv4 エンチャント: 防御力最大 (%)",
                ["Producer_EnchantHpMin_Lv4"] = "Lv4 エンチャント: HP最小 (%)",
                ["Producer_EnchantHpMax_Lv4"] = "Lv4 エンチャント: HP最大 (%)",
                ["Producer_FarmGrid_Lv5"] = "Lv5 パッシブ: 農場グリッド数",
                ["Producer_Durability_Lv5"] = "Lv5 パッシブ: 耐久度ボーナス (%)",
                ["Producer_MaterialReduction_Lv5"] = "Lv5 パッシブ: 素材軽減 (%)",
                ["Producer_EnchantChance_Lv5"] = "Lv5 エンチャント: 確率 (%)",
                ["Producer_EnchantWeaponDmgMin_Lv5"] = "Lv5 エンチャント: 武器ダメージ最小 (%)",
                ["Producer_EnchantWeaponDmgMax_Lv5"] = "Lv5 エンチャント: 武器ダメージ最大 (%)",
                ["Producer_EnchantArmorMin_Lv5"] = "Lv5 エンチャント: 防御力最小 (%)",
                ["Producer_EnchantArmorMax_Lv5"] = "Lv5 エンチャント: 防御力最大 (%)",
                ["Producer_EnchantHpMin_Lv5"] = "Lv5 エンチャント: HP最小 (%)",
                ["Producer_EnchantHpMax_Lv5"] = "Lv5 エンチャント: HP最大 (%)",

                // ============================================
                // 剣ツリー - パスヒット追加
                // ============================================
                ["Tier6_RushSlash_PathWidth"] = "Tier 6: [ラッシュ斬り] パスヒット幅 (m)",

                // ============================================
                // メイジ職業 - レベル別追加キー
                // ============================================
                ["Mage_Fire_Rain_Radius"] = "降下半径 (m)",
                ["Mage_Fire_Rain_Impact_Radius"] = "着弾ダメージ範囲 (m)",
                ["Mage_Fire_Rain_Projectile_Count"] = "バーストあたり発射体数 (個)",
                ["Mage_Lv1_Damage_Multiplier"] = "Lv1 AOEダメージ倍率 (%)",
                ["Mage_Lv1_Cooldown"] = "Lv1 クールダウン (秒)",
                ["Mage_Lv1_Elemental_Resistance"] = "Lv1 属性耐性 (%)",
                ["Mage_Lv2_Damage_Multiplier"] = "Lv2 AOEダメージ倍率 (%)",
                ["Mage_Lv2_Cooldown"] = "Lv2 クールダウン (秒)",
                ["Mage_Lv2_Elemental_Resistance"] = "Lv2 属性耐性 (%)",
                ["Mage_Lv3_Damage_Multiplier"] = "Lv3 AOEダメージ倍率 (%)",
                ["Mage_Lv3_Cooldown"] = "Lv3 クールダウン (秒)",
                ["Mage_Lv3_Elemental_Resistance"] = "Lv3 属性耐性 (%)",
                ["Mage_Lv4_Damage_Multiplier"] = "Lv4 AOEダメージ倍率 (%)",
                ["Mage_Lv4_Cooldown"] = "Lv4 クールダウン (秒)",
                ["Mage_Lv4_Elemental_Resistance"] = "Lv4 属性耐性 (%)",
                ["Mage_Lv5_Damage_Multiplier"] = "Lv5 AOEダメージ倍率 (%)",
                ["Mage_Lv5_Cooldown"] = "Lv5 クールダウン (秒)",
                ["Mage_Lv5_Elemental_Resistance"] = "Lv5 属性耐性 (%)",
                ["Mage_Lv1_AOE_Max_Targets"] = "アクティブ: 最大ターゲット数 Lv1",
                ["Mage_Lv2_AOE_Max_Targets"] = "アクティブ: 最大ターゲット数 Lv2",
                ["Mage_Lv3_AOE_Max_Targets"] = "アクティブ: 最大ターゲット数 Lv3",
                ["Mage_Lv4_AOE_Max_Targets"] = "アクティブ: 最大ターゲット数 Lv4",
                ["Mage_Lv5_AOE_Max_Targets"] = "アクティブ: 最大ターゲット数 Lv5",
            };
        }
    }
}

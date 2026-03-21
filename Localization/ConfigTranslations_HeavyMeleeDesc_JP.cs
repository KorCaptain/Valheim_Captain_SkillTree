using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetHeavyMeleeDescriptions_JP()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // 槍ツリー (Spear Tree)
                // ========================================

                // === Tier 0: 槍の達人 ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【必要ポイント】\n槍の達人を解放するために必要なポイント数。",

                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【2連撃後の攻撃速度ボーナス(%)】\n" +
                "槍で2回連続攻撃後、攻撃速度が上昇。\n" +
                "素早い連続攻撃で敵を圧倒。\n" +
                "推奨：10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【2連撃後のダメージボーナス(%)】\n" +
                "槍で2回連続攻撃後、ダメージが上昇。\n" +
                "コンボ攻撃のダメージを最大化。\n" +
                "推奨：7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【効果持続時間（秒）】\n" +
                "2連撃後の効果が持続する時間。\n" +
                "長い持続時間で安定した戦闘が可能。\n" +
                "推奨：4-8秒",

                // === Tier 1: 急所突き ===
                ["Tier1_QuickStrike_RequiredPoints"] =
                "【必要ポイント】\n急所突きを解放するために必要なポイント数。",

                ["Tier1_VitalStrike_DamageBonus"] =
                "【クリティカルダメージボーナス(%)】\n" +
                "槍攻撃のクリティカルダメージを増加。\n" +
                "急所を狙う精密攻撃に特化。\n" +
                "推奨：20-40%",

                // === Tier 2: 投槍 ===
                ["Tier2_Throw_RequiredPoints"] =
                "【必要ポイント】\n投槍を解放するために必要なポイント数。",

                ["Tier2_Throw_Cooldown"] =
                "【投槍クールダウン（秒）】\n" +
                "再び投槍を使用するまでの待機時間。\n" +
                "短いほど頻繁に使用可能。\n" +
                "推奨：20-40秒",

                ["Tier2_Throw_DamageMultiplier"] =
                "【投槍ダメージ倍率(%)】\n" +
                "投げた槍のダメージ倍率。\n" +
                "遠距離攻撃の威力を決定。\n" +
                "推奨：100-150%",

                ["Legacy_Throw_BuffDuration"] =
                "【未使用】\n" +
                "このパラメータは現在未使用。\n" +
                "パッシブスキルに変更されました。",

                // === Tier 3: 速突き槍 ===
                ["Tier3_Pierce_RequiredPoints"] =
                "【必要ポイント】\n速突き槍を解放するために必要なポイント数。",

                ["Tier3_Rapid_DamageBonus"] =
                "【武器攻撃力ボーナス（固定値）】\n" +
                "槍の基礎攻撃力を固定値で上昇。\n" +
                "素早い連続攻撃に有利。\n" +
                "推奨：3-6",

                ["Tier3_QuickSpear_AttackSpeed"] =
                "【攻撃速度ボーナス(%)】\n" +
                "槍またはポールアームの攻撃速度を向上。\n" +
                "推奨：15-25%",

                // === Tier 4: 回避打ち ===
                ["Tier4_Evasion_RequiredPoints"] =
                "【必要ポイント】\n回避打ちを解放するために必要なポイント数。",

                ["Tier4_Evasion_EvasionBonus"] =
                "【攻撃時の回避ボーナス(%)】\n" +
                "槍を突いた後5秒間、回避率が上昇。\n" +
                "積極的な戦闘スタイルの生存率を向上。\n" +
                "推奨：15-25%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【攻撃時のスタミナ消費減少(%)】\n" +
                "回避打ちのスタミナ消費が低下。\n" +
                "より長く継続して戦闘可能。\n" +
                "推奨：5-15%",

                // === Tier 4: デュアルストライク ===
                ["Tier4_Dual_DamageBonus"] =
                "【デュアルストライクダメージボーナス(%)】\n" +
                "2回連続攻撃時に追加ダメージを付与。\n" +
                "コンボダメージ攻撃に特化。\n" +
                "推奨：18-30%",

                ["Tier4_Dual_Duration"] =
                "【効果持続時間（秒）】\n" +
                "デュアルストライクバフの持続時間。\n" +
                "長い持続時間で安定したダメージ。\n" +
                "推奨：8-15秒",

                // === Tier 5: 貫通突き（アクティブGキー）===
                ["Tier5_Penetrate_RequiredPoints"] =
                "【必要ポイント】\n貫通突きを解放するために必要なポイント数。",

                ["Legacy_Penetrate_CritChance"] =
                "【未使用】\n" +
                "このパラメータは現在未使用。\n" +
                "ライトニングストライク効果に変更されました。",

                ["Tier5_Penetrate_BuffDuration"] =
                "【効果持続時間（秒）】\n" +
                "貫通突きバフの持続時間。\n" +
                "スキル効果の継続時間を決定。\n" +
                "推奨：25-40秒",

                ["Tier5_Penetrate_LightningDamage"] =
                "【雷ダメージ倍率(%)】\n" +
                "コンボ攻撃時の雷ダメージ倍率。\n" +
                "強力な追加ダメージを与える。\n" +
                "推奨：200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【雷発動に必要なヒット数】\n" +
                "雷効果を発動するために必要な連続ヒット数。\n" +
                "少ないほど頻繁に発動。\n" +
                "推奨：3-5回",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【Gキースキルクールダウン（秒）】\n" +
                "再びGキースキルを使用するまでの待機時間。\n" +
                "短いほど頻繁に使用可能。\n" +
                "推奨：50-80秒",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【Gキースキルスタミナ消費】\n" +
                "Gキースキル使用時に消費するスタミナ。\n" +
                "スタミナ管理が重要。\n" +
                "推奨：20-35",

                // === Tier 5: 槍連撃（アクティブHキー）===
                ["Tier5_Combo_RequiredPoints"] =
                "【必要ポイント】\n槍連撃を解放するために必要なポイント数。",

                ["Tier5_Combo_HKey_Cooldown"] =
                "【Hキースキルクールダウン（秒）】\n" +
                "再びHキースキルを使用するまでの待機時間。\n" +
                "短いほど頻繁に使用可能。\n" +
                "推奨：20-35秒",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【Hキースキルダメージ倍率(%)】\n" +
                "槍連撃攻撃（Hキー）のダメージ倍率。\n" +
                "強力な単発ヒットスキル。\n" +
                "推奨：250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【Hキースキルスタミナ消費】\n" +
                "Hキースキル使用時に消費するスタミナ。\n" +
                "スタミナ管理は欠かせない。\n" +
                "推奨：15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【Hキーノックバック距離（メートル）】\n" +
                "Hキースキルがヒットした際、敵がノックバックする距離。\n" +
                "戦闘中の位置取りに役立つ。\n" +
                "推奨：2-5m",

                ["Tier5_Combo_ActiveRange"] =
                "【アクティブ効果半径（メートル）】\n" +
                "槍連撃バフを発動する範囲半径。\n" +
                "広いほど発動しやすい。\n" +
                "推奨：2-5m",

                ["Tier5_Combo_BuffDuration"] =
                "【効果持続時間（秒）】\n" +
                "槍連撃バフの持続時間。\n" +
                "長い持続時間で投槍強化が安定。\n" +
                "推奨：25-40秒",

                ["Tier5_Combo_MaxUses"] =
                "【最大強化投槍回数】\n" +
                "バフ中の最大強化投槍回数。\n" +
                "多いほど強化が長続き。\n" +
                "推奨：2-5回",

                // ========================================
                // メイスツリー (Mace Tree)
                // ========================================

                // === Tier 0: メイスの達人 ===
                ["Tier0_MaceExpert_RequiredPoints"] =
                "【必要ポイント】\nメイスの達人を解放するために必要なポイント数。",

                ["Tier0_MaceExpert_DamageBonus"] =
                "【メイスダメージボーナス(%)】\n" +
                "打撃武器の基礎攻撃力を向上。\n" +
                "全てのメイス系武器に適用。\n" +
                "推奨：5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【スタン確率(%)】\n" +
                "メイス攻撃時に敵をスタンさせる確率。\n" +
                "スタンした敵は行動不能になる。\n" +
                "推奨：15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【スタン持続時間（秒）】\n" +
                "スタン効果の持続時間。\n" +
                "長いほどダメージを与える時間が増える。\n" +
                "推奨：0.3-1秒",

                // === Tier 1: メイス強化 ===
                ["Tier1_MaceExpert_DamageBonus"] =
                "【メイス攻撃ボーナス(%)】\n" +
                "打撃武器の追加攻撃ボーナス。\n" +
                "推奨：8-15%",

                ["Tier1_MaceExpert_RequiredPoints"] =
                "【必要ポイント】\nメイス強化を解放するために必要なポイント数。",

                // === Tier 2: スタン強化 ===
                ["Tier2_StunBoost_StunChanceBonus"] =
                "【スタン確率ボーナス(%)】\n" +
                "スタン確率をさらに向上。\n" +
                "メイスの達人ボーナスと累積。\n" +
                "推奨：10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【スタン持続時間ボーナス（秒）】\n" +
                "スタン時間をさらに延長。\n" +
                "ダメージを与える時間が増える。\n" +
                "推奨：0.3-0.8秒",

                ["Tier2_StunBoost_RequiredPoints"] =
                "【必要ポイント】\nスタン強化を解放するために必要なポイント数。",

                // === Tier 3: ガード ===
                ["Tier3_Guard_ArmorBonus"] =
                "【防御力ボーナス（固定値）】\n" +
                "基礎防御力を固定値で上昇。\n" +
                "タンクビルドに有利。\n" +
                "推奨：2-5",

                ["Tier3_Guard_RequiredPoints"] =
                "【必要ポイント】\nガードを解放するために必要なポイント数。",

                // === Tier 3: ヘビーストライク ===
                ["Tier3_HeavyStrike_DamageBonus"] =
                "【衝撃ダメージボーナス（固定値）】\n" +
                "メイスの衝撃ダメージを固定値で上昇。\n" +
                "パーセントボーナスと累積。\n" +
                "推奨：2-5",

                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【必要ポイント】\nヘビーストライクを解放するために必要なポイント数。",

                // === Tier 4: ノックバック ===
                ["Tier4_Push_KnockbackChance"] =
                "【ノックバック確率(%)】\n" +
                "攻撃時に敵をノックバックさせる確率。\n" +
                "距離の維持と戦場制御に役立つ。\n" +
                "推奨：25-35%",

                ["Tier4_Push_RequiredPoints"] =
                "【必要ポイント】\nノックバックを解放するために必要なポイント数。",

                // === Tier 5: タンク ===
                ["Tier5_Tank_HealthBonus"] =
                "【最大HP増加(%)】\n" +
                "最大HPを向上。\n" +
                "生存率の強化に重要。\n" +
                "推奨：20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【被ダメージ減少(%)】\n" +
                "受けるダメージを全て減少。\n" +
                "防御力と組み合わせてタンクに最適。\n" +
                "推奨：8-15%",

                ["Tier5_Tank_RequiredPoints"] =
                "【必要ポイント】\nタンクを解放するために必要なポイント数。",

                // === Tier 5: DPS ===
                ["Tier5_DPS_DamageBonus"] =
                "【攻撃ボーナス(%)】\n" +
                "メイスの攻撃力をさらに向上。\n" +
                "DPSビルドに有利。\n" +
                "推奨：15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【攻撃速度ボーナス(%)】\n" +
                "メイスの攻撃速度を向上。\n" +
                "重武器の遅さを補う。\n" +
                "推奨：8-15%",

                ["Tier5_DPS_RequiredPoints"] =
                "【必要ポイント】\nDPSを解放するために必要なポイント数。",

                // === Tier 6: グランドマスター ===
                ["Tier6_Grandmaster_ArmorBonus"] =
                "【防御力ボーナス(%)】\n" +
                "パーセント防御力ボーナス。\n" +
                "高級防御具と好相性。\n" +
                "推奨：15-25%",

                ["Tier6_Grandmaster_RequiredPoints"] =
                "【必要ポイント】\nグランドマスターを解放するために必要なポイント数。",

                // === Tier 7: フューリーハンマー（アクティブHキー）===
                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【第1-4撃ダメージ倍率(%)】\n" +
                "「フューリーハンマー」スキル（Hキー）第1-4撃のダメージ倍率。\n" +
                "現在の攻撃に適用。\n" +
                "推奨：70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【最終撃ダメージ倍率(%)】\n" +
                "「フューリーハンマー」スキル（Hキー）最終撃のダメージ倍率。\n" +
                "最後の一撃が最も強力。\n" +
                "推奨：130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【スタミナ消費】\n" +
                "Hキースキル使用時に消費するスタミナ。\n" +
                "スタミナ管理が重要。\n" +
                "推奨：35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用するまでの待機時間。\n" +
                "短いほど頻繁に使用可能。\n" +
                "推奨：25-35秒",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【AOE半径（メートル）】\n" +
                "スキルの範囲ダメージ半径。\n" +
                "広いほど多くの敵にヒット。\n" +
                "推奨：4-7m",

                ["Tier7_FuryHammer_RequiredPoints"] =
                "【必要ポイント】\nフューリーハンマーを解放するために必要なポイント数。",

                // === Tier 7: ガーディアンハート（アクティブGキー）===
                ["Tier7_GuardianHeart_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再びGキースキルを使用するまでの待機時間。\n" +
                "短いほど防御姿勢を頻繁に使用可能。\n" +
                "推奨：100-140秒",

                ["Tier7_GuardianHeart_StaminaCost"] =
                "【スタミナ消費】\n" +
                "スキル使用時に消費するスタミナ。\n" +
                "タンクとしてスタミナ管理が重要。\n" +
                "推奨：20-30",

                ["Tier7_GuardianHeart_Duration"] =
                "【効果持続時間（秒）】\n" +
                "防御姿勢の持続時間。\n" +
                "この間ダメージを反射し高防御力を持つ。\n" +
                "推奨：40-50秒",

                ["Tier7_GuardianHeart_ReflectPercent"] =
                "【ダメージ反射パーセント(%)】\n" +
                "受けたダメージを攻撃者に反射する割合。\n" +
                "タンクとしてダメージを敵に返す。\n" +
                "推奨：5-8%",

                ["Tier7_GuardianHeart_RequiredPoints"] =
                "【必要ポイント】\nガーディアンハートを解放するために必要なポイント数。",

                // ========================================
                // ポールアームツリー (Polearm Tree)
                // ========================================

                // === Tier 0: ポールアームの達人 ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【攻撃距離ボーナス(%)】\n" +
                "ポールアームの攻撃距離を向上。\n" +
                "広い攻撃距離で安全な距離から攻撃可能。\n" +
                "推奨：10-20%",

                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【必要ポイント】\nポールアームの達人を解放するために必要なポイント数。",

                // === Tier 1: スピンアタック ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【スピンアタックダメージボーナス(%)】\n" +
                "スピン攻撃時の追加ダメージ。\n" +
                "複数の敵に非常に効果的。\n" +
                "推奨：50-80%",

                ["Tier1_SpinWheel_RequiredPoints"] =
                "【必要ポイント】\nスピンアタックを解放するために必要なポイント数。",

                // === Tier 2-1: ポールアーム強化 ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] =
                "【武器攻撃力ボーナス（固定値）】\n" +
                "ポールアームの基礎攻撃力を固定値で上昇。\n" +
                "全てのポールアーム攻撃に適用。\n" +
                "推奨：4-7",

                ["Tier2-1_PolearmBoost_RequiredPoints"] =
                "【必要ポイント】\nポールアーム強化を解放するために必要なポイント数。",

                // === Tier 2-2: ヒーローストライク ===
                ["Tier2-2_HeroStrike_KnockbackChance"] =
                "【よろめき確率(%)】\n" +
                "攻撃時に敵のバランスを崩す確率。\n" +
                "戦場制御に役立つ。\n" +
                "推奨：20-35%",

                ["Tier2-2_HeroStrike_RequiredPoints"] =
                "【必要ポイント】\nヒーローストライクを解放するために必要なポイント数。",

                // === Tier 3: エリアコンボ ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【デュアルヒットダメージボーナス(%)】\n" +
                "2回連続攻撃時の追加ダメージ。\n" +
                "範囲コンボに特化。\n" +
                "推奨：20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【デュアルヒットバフ持続時間（秒）】\n" +
                "デュアルヒットバフの持続時間。\n" +
                "長いほどコンボが安定。\n" +
                "推奨：4-8秒",

                ["Tier3_AreaCombo_RequiredPoints"] =
                "【必要ポイント】\nエリアコンボを解放するために必要なポイント数。",

                // === Tier 4-1: グラウンドスラム ===
                ["Tier4-1_GroundWheel_RequiredPoints"] =
                "【必要ポイント】\nグラウンドスラムを解放するために必要なポイント数。",

                // === Tier 4-2: 月牙斬 ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] =
                "【攻撃距離ボーナス(%)】\n" +
                "月牙斬の攻撃範囲を向上。\n" +
                "より広い半径で攻撃可能。\n" +
                "推奨：12-20%",

                ["Tier4-2_MoonSlash_StaminaReduction"] =
                "【スタミナ消費減少(%)】\n" +
                "月牙斬のスタミナ消費を減少。\n" +
                "より長く継続して戦闘可能。\n" +
                "推奨：12-20%",

                ["Tier4-2_MoonSlash_RequiredPoints"] =
                "【必要ポイント】\n月牙斬を解放するために必要なポイント数。",

                // === Tier 4-3: サプレス ===
                ["Tier4-3_Suppress_DamageBonus"] =
                "【サプレスダメージボーナス(%)】\n" +
                "サプレス攻撃時の追加ダメージ。\n" +
                "敵を圧制して戦闘の主導権を握る。\n" +
                "推奨：25-40%",

                ["Tier4-3_Suppress_RequiredPoints"] =
                "【必要ポイント】\nサプレスを解放するために必要なポイント数。",

                // === Tier 5: ピアースチャージ（アクティブGキー）===
                ["Tier5_PierceCharge_DashDistance"] =
                "【突進距離（メートル）】\n" +
                "貫通スキルの突進距離。\n" +
                "遠くの敵陣を突き破ることが可能。\n" +
                "推奨：8-12m",

                ["Tier5_PierceCharge_FirstHitDamageBonus"] =
                "【初撃ダメージボーナス(%)】\n" +
                "突進中の初撃ダメージ倍率。\n" +
                "強力な初撃で敵を制圧。\n" +
                "推奨：180-250%",

                ["Tier5_PierceCharge_AoeDamageBonus"] =
                "【ノックバックAOEダメージボーナス(%)】\n" +
                "突進後の範囲ノックバックダメージ倍率。\n" +
                "周囲の敵をノックバックしダメージを与える。\n" +
                "推奨：130-180%",

                ["Tier5_PierceCharge_AoeAngle"] =
                "【AOE角度（度）】\n" +
                "範囲ノックバック効果の角度。\n" +
                "280°＝後方/側面エリア、正面80°は除く。\n" +
                "推奨：250-300°",

                ["Tier5_PierceCharge_AoeRadius"] =
                "【AOE半径（メートル）】\n" +
                "範囲ノックバック効果の半径。\n" +
                "広いほど多くの敵をノックバック。\n" +
                "推奨：4-7m",

                ["Tier5_PierceCharge_KnockbackDistance"] =
                "【ノックバック距離（メートル）】\n" +
                "敵がノックバックされる距離。\n" +
                "戦場制御に役立つ。\n" +
                "推奨：6-10m",

                ["Tier5_PierceCharge_StaminaCost"] =
                "【スタミナ消費】\n" +
                "Gキースキル使用時に消費するスタミナ。\n" +
                "スタミナ管理が重要。\n" +
                "推奨：18-25",

                ["Tier5_PierceCharge_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再びGキースキルを使用するまでの待機時間。\n" +
                "短いほど頻繁に使用可能。\n" +
                "推奨：25-40秒",

                ["Tier5_PierceCharge_RequiredPoints"] =
                "【必要ポイント】\nピアースチャージを解放するために必要なポイント数。",
            };
        }
    }
}

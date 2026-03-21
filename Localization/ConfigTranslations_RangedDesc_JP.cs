using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetRangedDescriptions_JP()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // 杖ツリー (Staff Tree)
                // ========================================

                // === Tier 0: 杖専家 ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【属性ダメージボーナス (%)】\n" +
                "杖の属性ダメージ（炎、氷、雷）を増加させます。\n" +
                "魔法攻撃の基本となるスキルです。\n" +
                "推奨: 3-7%",

                ["Tier0_StaffExpert_RequiredPoints"] =
                "【必要ポイント】\n杖専家を解放するための必要ポイントです。",

                // === Tier 1: 精神集中 & 魔力の流れ ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【エイトルコスト減少 (%)】\n" +
                "精神集中により詠唱時のエイトル消費を減らします。\n" +
                "より頻繁に魔法を使用できます。\n" +
                "推奨: 12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【最大エイトルボーナス（固定値）】\n" +
                "魔力の流れにより最大エイトルを増加させます。\n" +
                "推奨: 25-35",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【必要ポイント】\n精神集中を解放するための必要ポイントです。",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【必要ポイント】\n魔力の流れを解放するための必要ポイントです。",

                // === Tier 2: 魔法増幅 ===
                ["Tier2_MagicAmplify_Chance"] =
                "【増幅確率 (%)】\n" +
                "属性ダメージ増幅が発動する確率です。\n" +
                "推奨: 30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【増幅時属性ダメージボーナス (%)】\n" +
                "増幅発動時のダメージ増加量です。\n" +
                "推奨: 30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【エイトルコスト増加 (%)】\n" +
                "詠唱時のエイトル消費が増加します。\n" +
                "強力な魔法のコストとして機能します。\n" +
                "推奨: 15-25%",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【必要ポイント】\n魔法増幅を解放するための必要ポイントです。",

                // === Tier 3: 属性 ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【氷ダメージボーナス（固定値）】\n" +
                "氷属性魔法のダメージを増加させます。\n" +
                "推奨: 2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【炎ダメージボーナス（固定値）】\n" +
                "炎属性魔法のダメージを増加させます。\n" +
                "推奨: 2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【雷ダメージボーナス（固定値）】\n" +
                "雷属性魔法のダメージを増加させます。\n" +
                "推奨: 2-5",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【必要ポイント】\n氷属性を解放するための必要ポイントです。",

                ["Tier3_FireElement_RequiredPoints"] =
                "【必要ポイント】\n炎属性を解放するための必要ポイントです。",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【必要ポイント】\n雷属性を解放するための必要ポイントです。",

                // === Tier 4: 幸運の魔力 ===
                ["Tier4_LuckyMana_Chance"] =
                "【無消費詠唱確率 (%)】\n" +
                "エイトルを消費せずに魔法を発動できる確率です。\n" +
                "幸運の魔力で無限詠唱が可能になります。\n" +
                "推奨: 30-40%",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【必要ポイント】\n幸運の魔力を解放するための必要ポイントです。",

                // === Tier 5-1: 二重詠唱（アクティブ T）===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【追加弾数】\n" +
                "追加で発射される魔法弾の数です。\n" +
                "推奨: 5-10",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【弾ダメージ (%)】\n" +
                "追加弾のダメージ割合です。\n" +
                "推奨: 10-20%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【拡散角度（未使用）】\n" +
                "現バージョンでは未使用です。方向は固定です。",

                ["Tier5_DoubleCast_EitrCost"] =
                "【エイトル消費】\n" +
                "スキル発動時に消費するエイトル量です。\n" +
                "推奨: 15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 25-40秒",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【必要ポイント】\n二重詠唱を解放するための必要ポイントです。",

                // === Tier 5-2: 即時範囲回復（アクティブ H）===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 25-40秒",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【エイトル消費】\n" +
                "スキル使用時に消費するエイトル量です。\n" +
                "推奨: 25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【回復量（%最大HP）】\n" +
                "回復する最大HPの割合です。\n" +
                "推奨: 20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【回復範囲（メートル）】\n" +
                "回復効果が及ぶ範囲の半径です。\n" +
                "推奨: 10-15メートル",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【必要ポイント】\n範囲回復を解放するための必要ポイントです。",

                // ========================================
                // クロスボウツリー (Crossbow Tree)
                // ========================================

                // === Tier 0: クロスボウ専家 ===
                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【必要ポイント】\nクロスボウ専家を解放するための必要ポイントです。",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【クロスボウダメージボーナス (%)】\n" +
                "クロスボウとボルトの基礎ダメージを増加させます。\n" +
                "推奨: 8-12%",

                // === Tier 1: 速射 ===
                ["Tier1_RapidFire_Chance"] =
                "【速射発動確率 (%)】\n" +
                "射撃時に速射が発動する確率です。\n" +
                "発動時に複数のボルトを素早く連射します。\n" +
                "推奨: 15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【速射ボルト数】\n" +
                "速射時に追加で発射されるボルトの数です。\n" +
                "推奨: 2-4発",

                ["Tier1_RapidFire_DamagePercent"] =
                "【速射ダメージ (%)】\n" +
                "速射ボルトのダメージ割合です。\n" +
                "基本攻撃力に対する割合で計算されます。\n" +
                "推奨: 60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【速射間隔（秒）】\n" +
                "速射中の各発射間の時間間隔です。\n" +
                "小さいほど速くなります。\n" +
                "推奨: 0.1-0.3秒",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【速射ボルト消費】\n" +
                "速射1回あたりのボルト消費数です。\n" +
                "推奨: 1-2",

                ["Tier1_RapidFire_RequiredPoints"] =
                "【必要ポイント】\n速射を解放するための必要ポイントです。",

                // === Tier 2: バランスドエイム ===
                ["Tier2_BalancedAim_KnockbackChance"] =
                "【ノックバック確率 (%)】\n" +
                "クロスボウ命中時にノックバックが発動する確率です。\n" +
                "安定した姿勢で確実な命中を実現します。\n" +
                "推奨: 20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【ノックバック距離（メートル）】\n" +
                "発動時に敵が後退する距離です。\n" +
                "推奨: 2-4メートル",

                ["Tier2_BalancedAim_RequiredPoints"] =
                "【必要ポイント】\nバランスドエイムを解放するための必要ポイントです。",

                // === Tier 2: 素早い装填 ===
                ["Tier2_RapidReload_SpeedIncrease"] =
                "【装填速度ボーナス (%)】\n" +
                "クロスボウの装填速度を増加させます。\n" +
                "より素早く次のボルトを装填できます。\n" +
                "推奨: 10-20%",

                ["Tier2_RapidReload_RequiredPoints"] =
                "【必要ポイント】\n素早い装填を解放するための必要ポイントです。",

                // === Tier 2: 正直な射撃 ===
                ["Tier2_HonestShot_DamageBonus"] =
                "【基礎ダメージボーナス (%)】\n" +
                "クロスボウの基礎攻撃力をさらに増加させます。\n" +
                "推奨: 10-18%",

                ["Tier2_HonestShot_RequiredPoints"] =
                "【必要ポイント】\n正直な射撃を解放するための必要ポイントです。",

                // === Tier 3: 自動装填 ===
                ["Tier3_AutoReload_Chance"] =
                "【自動装填確率 (%)】\n" +
                "命中時に200%速度で次の装填が完了する確率です。\n" +
                "継続的な攻撃リズムの維持に役立ちます。\n" +
                "推奨: 20-35%",

                ["Tier3_AutoReload_RequiredPoints"] =
                "【必要ポイント】\n自動装填を解放するための必要ポイントです。",

                // === Tier 4: 速射 Lv.2 ===
                ["Tier4_RapidFireLv2_Chance"] =
                "【速射 Lv.2 確率 (%)】\n" +
                "強化速射が発動する確率です。\n" +
                "速射 Lv.1 の確率と重ねて計算されます。\n" +
                "推奨: 20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【Lv.2 ボルト数】\n" +
                "強化速射時に追加で発射されるボルトの数です。\n" +
                "速射 Lv.1 より多くなります。\n" +
                "推奨: 4-6発",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【速射 Lv.2 ダメージ (%)】\n" +
                "強化速射のダメージ割合です。\n" +
                "Lv.1 より高いダメージです。\n" +
                "推奨: 75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【速射 Lv.2 間隔（秒）】\n" +
                "強化速射の各発射間の時間間隔です。\n" +
                "推奨: 0.1-0.3秒",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【Lv.2 ボルト消費】\n" +
                "強化速射のボルト消費数です。\n" +
                "推奨: 1-2",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【必要ポイント】\n速射 Lv.2 を解放するための必要ポイントです。",

                // === Tier 4: ファイナルストライク ===
                ["Tier4_FinalStrike_HpThreshold"] =
                "【敵HP閾値 (%)】\n" +
                "この閾値以上のHPを持つ敵に追加ダメージを与えます。\n" +
                "高HPの敵に対して有効です。\n" +
                "推奨: 40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【追加ダメージ (%)】\n" +
                "閾値以上のHPを持つ敵への追加ダメージです。\n" +
                "推奨: 20-40%",

                ["Tier4_FinalStrike_RequiredPoints"] =
                "【必要ポイント】\nファイナルストライクを解放するための必要ポイントです。",

                // === Tier 5: ワンショット（アクティブ T）===
                ["Tier5_OneShot_Duration"] =
                "【効果持続時間（秒）】\n" +
                "「ワンショット」効果の持続時間です。\n" +
                "この間に強力な一撃を放てます。\n" +
                "推奨: 8-12秒",

                ["Tier5_OneShot_DamageBonus"] =
                "【ワンショットダメージボーナス (%)】\n" +
                "「ワンショット」発動時の追加ダメージです。\n" +
                "壊滅的な一撃の力です。\n" +
                "推奨: 150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【ノックバック距離（メートル）】\n" +
                "命中時に敵が後退する距離です。\n" +
                "強力な衝撃で敵を吹き飛ばします。\n" +
                "推奨: 5-10メートル",

                ["Tier5_OneShot_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 25-40秒",

                ["Tier5_OneShot_RequiredPoints"] =
                "【必要ポイント】\nワンショットを解放するための必要ポイントです。",

                // ========================================
                // 弓ツリー (Bow Tree)
                // ========================================

                // === Tier 0: 弓専家 ===
                ["Tier0_BowExpert_DamageBonus"] =
                "【弓ダメージボーナス (%)】\n" +
                "弓と矢の基礎ダメージを増加させます。\n" +
                "推奨: 8-12%",

                ["Tier0_BowExpert_RequiredPoints"] =
                "【必要ポイント】\n弓専家を解放するための必要ポイントです。",

                // === Tier 1-1: 集中射撃 ===
                ["Tier1_FocusedShot_CritBonus"] =
                "【クリティカル率ボーナス (%)】\n" +
                "集中射撃でクリティカル確率を増加させます。\n" +
                "集中度が高いほどクリティカル率が上昇します。\n" +
                "推奨: 5-10%",

                ["Tier1_FocusedShot_RequiredPoints"] =
                "【必要ポイント】\n集中射撃を解放するための必要ポイントです。",

                // === Tier 1-2: マルチショット Lv.1 ===
                ["Tier1_MultishotLv1_ActivationChance"] =
                "【マルチショット Lv.1 確率 (%)】\n" +
                "弓射撃時にマルチショットが発動する確率です。\n" +
                "発動時に複数の矢を同時に放ちます。\n" +
                "推奨: 15-25%",

                ["Tier1_MultishotLv1_AdditionalArrows"] =
                "【追加矢数】\n" +
                "マルチショット時に追加で放たれる矢の数です。\n" +
                "推奨: 2-4",

                ["Tier1_MultishotLv1_ArrowConsumption"] =
                "【矢消費数】\n" +
                "マルチショット時に消費する矢の数です。\n" +
                "推奨: 1-2",

                ["Tier1_MultishotLv1_DamagePerArrow"] =
                "【矢1本あたりダメージ (%)】\n" +
                "マルチショットの各矢のダメージ割合です。\n" +
                "推奨: 50-70%",

                ["Tier1_MultishotLv1_RequiredPoints"] =
                "【必要ポイント】\nマルチショット Lv.1 を解放するための必要ポイントです。",

                // === Tier 2: 弓術精通 ===
                ["Tier2_BowMastery_SkillBonus"] =
                "【弓スキルボーナス（固定値）】\n" +
                "弓のスキルレベルを増加させます。\n" +
                "熟練度が高いほど攻撃が強力になります。\n" +
                "推奨: 5-10",

                ["Tier2_BowMastery_SpecialArrowChance"] =
                "【特殊矢確率 (%)】\n" +
                "特殊効果を持つ矢が発動する確率です。\n" +
                "毒、炎、氷などの状態効果を付与できます。\n" +
                "推奨: 25-35%",

                ["Tier2_BowMastery_RequiredPoints"] =
                "【必要ポイント】\n弓術精通を解放するための必要ポイントです。",

                // === Tier 3-1: サイレントストライク ===
                ["Tier3_SilentStrike_DamageBonus"] =
                "【サイレントストライクダメージボーナス（固定値）】\n" +
                "弓のダメージを固定値で増加させます。\n" +
                "矢が敵を貫通してより多くのダメージを与えます。\n" +
                "推奨: 3-8",

                ["Tier3_SilentStrike_RequiredPoints"] =
                "【必要ポイント】\nサイレントストライクを解放するための必要ポイントです。",

                // === Tier 3-2: マルチショット Lv.2 ===
                ["Tier3_MultishotLv2_ActivationChance"] =
                "【マルチショット Lv.2 確率 (%)】\n" +
                "強化版マルチショットの発動確率です。\n" +
                "Lv.1 より多くの矢を放ちます。\n" +
                "推奨: 20-30%",

                ["Tier3_MultishotLv2_RequiredPoints"] =
                "【必要ポイント】\nマルチショット Lv.2 を解放するための必要ポイントです。",

                // === Tier 3-3: ハンティングインスティンクト ===
                ["Tier3_HuntingInstinct_CritBonus"] =
                "【ハンティングインスティンクト クリティカル率 (%)】\n" +
                "ハンティングインスティンクトでクリティカル確率を増加させます。\n" +
                "推奨: 8-15%",

                ["Tier3_HuntingInstinct_RequiredPoints"] =
                "【必要ポイント】\nハンティングインスティンクトを解放するための必要ポイントです。",

                // === Tier 4: プレシジョンエイム ===
                ["Tier4_PrecisionAim_CritDamage"] =
                "【クリティカルダメージボーナス (%)】\n" +
                "プレシジョンエイムでクリティカル時のダメージ倍率を増加させます。\n" +
                "弱点命中で巨大なダメージを与えます。\n" +
                "推奨: 25-40%",

                ["Tier4_PrecisionAim_RequiredPoints"] =
                "【必要ポイント】\nプレシジョンエイムを解放するための必要ポイントです。",

                // === Tier 5: 爆裂矢（アクティブ T）===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] =
                "【爆裂矢ダメージ (%)】\n" +
                "アクティブ T — 爆裂矢のダメージ倍率です。\n" +
                "範囲ダメージを持つ強力な矢です。\n" +
                "推奨: 100-150%",

                ["Tier5_ExplosiveArrow_Radius"] =
                "【爆発範囲（メートル）】\n" +
                "爆裂矢のダメージ半径です。\n" +
                "推奨: 3-6メートル",

                ["Tier5_ExplosiveArrow_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 15-25秒",

                ["Tier5_ExplosiveArrow_StaminaCost"] =
                "【スタミナ消費 (%)】\n" +
                "スキル使用時に消費するスタミナの割合です。\n" +
                "推奨: 10-20%",

                ["Tier5_ExplosiveArrow_RequiredPoints"] =
                "【必要ポイント】\n爆裂矢を解放するための必要ポイントです。",
            };
        }
    }
}

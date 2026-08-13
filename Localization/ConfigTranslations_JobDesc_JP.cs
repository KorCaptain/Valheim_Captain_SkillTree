using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJobDescriptions_JP()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // アーチャー職業スキル (Archer Job)
                // ========================================

                // === アーチャー：アクティブスキル《マルチショット》（6キー）===
                ["Archer_MultiShot_ArrowCount"] =
                "【矢の数】\n" +
                "マルチショットで一度に発射する矢の数。\n" +
                "矢が多い＝範囲ダメージが高い。\n" +
                "推奨：4-7",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【矢の消費数】\n" +
                "マルチショット1回で消費する矢の数。\n" +
                "消費が少ないほど効率的な攻撃が可能。\n" +
                "推奨：1-2",

                ["Archer_MultiShot_DamagePercent"] =
                "【矢1本あたりのダメージ(%)】\n" +
                "各矢が与えるダメージのパーセント。\n" +
                "弓の基礎攻撃力に対する割合。\n" +
                "推奨：40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "マルチショット再使用前の待機時間。\n" +
                "数値が低いほど頻繁に使用可能。\n" +
                "推奨：25-40秒",

                ["Archer_MultiShot_Charges"] =
                "【チャージ数】\n" +
                "マルチショットを連続使用できる回数。\n" +
                "複数回射撃でダメージを集中。\n" +
                "推奨：2-4",

                ["Archer_MultiShot_StaminaCost"] =
                "【スタミナ消費】\n" +
                "マルチショット使用時に消費するスタミナ。\n" +
                "スタミナ管理が重要。\n" +
                "推奨：20-35",

                ["Archer_MultiShot_FireInterval"] =
                "【連続発射間隔（秒）】\n" +
                "斉射矢の発射間隔。\n" +
                "5本の矢がこの間隔で順次発射されます。\n" +
                "推奨：0.15-0.3秒",

                // === アーチャー：パッシブスキル（2キー）===
                ["Archer_JumpHeightBonus"] =
                "【ジャンプ高度ボーナス(%)】\n" +
                "基礎ジャンプ高度を向上。\n" +
                "高い場所に簡単に登れる。\n" +
                "推奨：15-25%",

                ["Archer_FallDamageReduction"] =
                "【落下ダメージ減少(%)】\n" +
                "高い場所から落下した際に受けるダメージを減少。\n" +
                "アーチャーの機動性を強化。\n" +
                "推奨：40-60%",

                // === アーチャー：レベルボーナス（9キー）===
                ["Archer_Lv2_BonusArrows"] =
                "【Lv.2：追加矢数】\n" +
                "Lv.2昇格時に獲得する追加矢数。\n" +
                "基礎矢数に累積加算。\n" +
                "推奨：1",

                ["Archer_Lv2_DamagePercent"] =
                "【Lv.2：矢1本あたりのダメージ(%)】\n" +
                "Lv.2時の矢1本あたりのダメージ倍率。\n" +
                "弓+矢の合計ダメージの%で計算。\n" +
                "推奨：50-60%",

                ["Archer_Lv3_BonusArrows"] =
                "【Lv.3：追加矢数】\n" +
                "Lv.3昇格時に獲得する追加矢数。\n" +
                "基礎矢数に累積加算。\n" +
                "推奨：2",

                ["Archer_Lv3_DamagePercent"] =
                "【Lv.3：矢1本あたりのダメージ(%)】\n" +
                "Lv.3時の矢1本あたりのダメージ倍率。\n" +
                "弓+矢の合計ダメージの%で計算。\n" +
                "推奨：55-65%",

                ["Archer_Lv4_BonusArrows"] =
                "【Lv.4：追加矢数】\n" +
                "Lv.4昇格時に獲得する追加矢数。\n" +
                "基礎矢数に累積加算。\n" +
                "推奨：3",

                ["Archer_Lv4_DamagePercent"] =
                "【Lv.4：矢1本あたりのダメージ(%)】\n" +
                "Lv.4時の矢1本あたりのダメージ倍率。\n" +
                "弓+矢の合計ダメージの%で計算。\n" +
                "推奨：60-70%",

                ["Archer_Lv5_BonusArrows"] =
                "【Lv.5：追加矢数】\n" +
                "Lv.5昇格時に獲得する追加矢数。\n" +
                "基礎矢数に累積加算。\n" +
                "推奨：3",

                ["Archer_Lv5_DamagePercent"] =
                "【Lv.5：矢1本あたりのダメージ(%)】\n" +
                "Lv.5時の矢1本あたりのダメージ倍率。\n" +
                "弓+矢の合計ダメージの%で計算。\n" +
                "推奨：60-70%",

                ["Archer_Lv5_BonusCharges"] =
                "【Lv.5：追加チャージ数】\n" +
                "Lv.5時のマルチショット追加チャージ数。\n" +
                "基礎チャージ数に累積加算。\n" +
                "推奨：1",

                // === アーチャー：パッシブレベルボーナス（8キー）===
                ["Archer_Lv2_JumpHeightBonus"] =
                "【Lv.2 パッシブ：ジャンプ高度ボーナス(%)】\n" +
                "Lv.2時の追加ジャンプ高度ボーナス。\n" +
                "Lv.1基礎値に累積。\n" +
                "推奨：10%",

                ["Archer_Lv3_JumpHeightBonus"] =
                "【Lv.3 パッシブ：ジャンプ高度ボーナス(%)】\n" +
                "Lv.3時の追加ジャンプ高度ボーナス。\n" +
                "推奨：20%",

                ["Archer_Lv4_JumpHeightBonus"] =
                "【Lv.4 パッシブ：ジャンプ高度ボーナス(%)】\n" +
                "Lv.4時の追加ジャンプ高度ボーナス。\n" +
                "推奨：20%",

                ["Archer_Lv5_JumpHeightBonus"] =
                "【Lv.5 パッシブ：ジャンプ高度ボーナス(%)】\n" +
                "Lv.5時の追加ジャンプ高度ボーナス。\n" +
                "推奨：20%",

                ["Archer_Lv3_FallDamageReduction"] =
                "【Lv.3 パッシブ：落下ダメージ減少(%)】\n" +
                "Lv.3時の追加落下ダメージ減少。\n" +
                "Lv.1基礎値に累積。\n" +
                "推奨：10%",

                ["Archer_Lv4_FallDamageReduction"] =
                "【Lv.4 パッシブ：落下ダメージ減少(%)】\n" +
                "Lv.4時の追加落下ダメージ減少。\n" +
                "推奨：20%",

                ["Archer_Lv5_FallDamageReduction"] =
                "【Lv.5 パッシブ：落下ダメージ減少(%)】\n" +
                "Lv.5時の追加落下ダメージ減少。\n" +
                "推奨：35%",

                ["Archer_ElementalResistPerLevel"] =
                "【パッシブ：レベル毎の属性耐性(%)】\n" +
                "アーチャーのレベルごとに獲得する属性耐性。\n" +
                "毒(Lv2+)、氷(Lv3+)、炎(Lv4+)、雷(Lv5)。\n" +
                "推奨：10%",

                // ========================================
                // メイジ職業スキル (Mage Job)
                // ========================================

                // === メイジ：アクティブスキル《AOE》（5キー）===
                ["Mage_AOE_Range"] =
                "【AOE範囲（メートル）】\n" +
                "魔法範囲攻撃の半径。\n" +
                "広いほど多くの敵にヒット。\n" +
                "推奨：10-15m",

                ["Mage_Eitr_Cost"] =
                "【エイトル消費】\n" +
                "スキル使用時に消費するエイトル量。\n" +
                "魔法リソース管理が重要。\n" +
                "推奨：30-45",

                ["Mage_Damage_Multiplier"] =
                "【ダメージ倍率(%)】\n" +
                "魔法範囲攻撃のダメージ倍率。\n" +
                "強力な爆発魔法で敵を殲滅。\n" +
                "推奨：250-350%",

                ["Mage_Fire_Rain_Radius"] =
                "【ファイアレイン落下半径 (m)】\n" +
                "ファイアボール30個が対象周辺に落下する半径。\n" +
                "推奨：6-10m",

                ["Mage_Fire_Rain_Impact_Radius"] =
                "【ファイアボール着弾ダメージ範囲 (m)】\n" +
                "各ファイアボール着弾時にダメージを与える範囲。\n" +
                "推奨：2-4m",

                ["Mage_Fire_Rain_Projectile_Count"] =
                "【バーストごとの発射数】\n" +
                "1回のバーストで落下するファイアボールの数。\n" +
                "合計2回のバーストで発射（1バースト -> 1秒 -> 2バースト）。\n" +
                "推奨：15-25個",

                ["Mage_Dungeon_Buff_Damage_Bonus"] =
                "【ダンジョンバフ攻撃力ボーナス (%)】\n" +
                "ダンジョン内でYキー使用時、火の雨の代わりに発動する自己強化バフの攻撃力上昇量。\n" +
                "推奨：20-30%",

                ["Mage_Dungeon_Buff_Duration"] =
                "【ダンジョンバフ持続時間（秒）】\n" +
                "ダンジョン内の代替バフが持続する時間。\n" +
                "推奨：8-12秒",

                ["Mage_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用前の待機時間。\n" +
                "強力な効果のためクールダウンが長い。\n" +
                "推奨：150-200秒",

                // === メイジ：パッシブスキル（1キー）===
                ["Mage_Elemental_Resistance"] =
                "【属性耐性(%)】\n" +
                "炎、氷、雷、毒、スピリットへの耐性を向上。\n" +
                "物理ダメージは含まない — 魔法ダメージのみ軽減。\n" +
                "推奨：12-20%",

                // === バーサーカー：パッシブHP増加 ===
                ["berserker_passive_health_bonus"] =
                "【最大HP増加(%)】\n" +
                "バーサーカーパッシブ：最大HPを向上。\n" +
                "総HP（基礎+MMO+全ボーナス）の%で計算。\n" +
                "回復は正常に機能（m_baseHPに含まれる）。\n" +
                "推奨：100%",

                // === バーサーカー Lv2~5 パッシブ Config ===
                ["Berserker_Lv2_CooldownReduction"] =
                "【バーサーカー Lv2：怒りCDー秒数】\n" +
                "Lv2達成時、怒りスキルのクールダウンをこの数値分短縮。\n" +
                "推奨：5秒",

                ["Berserker_Lv3_RageDamageReduction"] =
                "【バーサーカー Lv3：怒り中の被ダメ軽減(%)】\n" +
                "Lv3達成時、怒り状態での受けるダメージを軽減。\n" +
                "推奨：15%",

                ["Berserker_Lv4_LowHpAttackBonus"] =
                "【バーサーカー Lv4：低HP攻撃力ボーナス(%)】\n" +
                "Lv4達成時、HP閾値以下で攻撃力が増加。\n" +
                "推奨：15%",

                ["Berserker_Lv4_LowHpAttackThreshold"] =
                "【バーサーカー Lv4：低HP発動閾値(%)】\n" +
                "このHP%以下でLv4攻撃力ボーナスが発動。\n" +
                "推奨：50%",

                ["Berserker_Lv5_PassiveCooldownReduction"] =
                "【バーサーカー Lv5：死への抵抗CDー秒数】\n" +
                "Lv5達成時、パッシブのクールダウンをこの数値分短縮。\n" +
                "推奨：120秒",

                ["Berserker_Lv5_InvincibilityBonus"] =
                "【バーサーカー Lv5：追加無敵時間(秒)】\n" +
                "Lv5達成時、死への抵抗発動時の無敵時間を延長。\n" +
                "推奨：2秒",

                // ========================================
                // タンカー職業スキル (Tanker Job)
                // ========================================

                // === タンカー：アクティブスキル《挑発》（9キー）===
                ["Tanker_Taunt_Cooldown"] =
                "【挑発クールダウン（秒）】\n" +
                "スキル再使用前の待機時間。\n" +
                "推奨：45-90秒",

                ["Tanker_Taunt_StaminaCost"] =
                "【挑発スタミナ消費】\n" +
                "挑発発動時に消費するスタミナ。\n" +
                "推奨：20-30",

                ["Tanker_Taunt_Range"] =
                "【挑発範囲（メートル）】\n" +
                "敵を挑発する半径範囲。\n" +
                "推奨：10-15m",

                ["Tanker_Taunt_Duration"] =
                "【通常モンスター挑発持続時間（秒）】\n" +
                "通常モンスターへの挑発が有効な時間。\n" +
                "推奨：4-8秒",

                ["Tanker_Taunt_BossDuration"] =
                "【ボス挑発持続時間（秒）】\n" +
                "ボスへの挑発が有効な時間。\n" +
                "ボスは耐性が高い — 効果時間が短い。\n" +
                "推奨：1-3秒",

                ["Tanker_Taunt_DamageReduction"] =
                "【被ダメージ減少(%)】\n" +
                "挑発バフ発動中の被ダメージ減少量。\n" +
                "推奨：15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【ダメージ減少バフ持続時間（秒）】\n" +
                "発動後にダメージ減少バフが有効な時間。\n" +
                "推奨：4-8秒",

                ["Tanker_Taunt_ReflectPercent"] =
                "【挑発反射ダメージ (%)】\n" +
                "戦吼バフ中に受けたダメージの一部を攻撃者に反射します。\n" +
                "バフ持続時間中に有効。\n" +
                "推奨：5-20%",

                ["Tanker_Taunt_EffectHeight"] =
                "【挑発マーカー高度（メートル）】\n" +
                "モンスターの上に表示される挑発マーカーの高さ。\n" +
                "推奨：1.5-2.5m",

                ["Tanker_Taunt_EffectScale"] =
                "【挑発マーカースケール】\n" +
                "挑発ビジュアルエフェクトのサイズ倍率。\n" +
                "推奨：0.2-0.5",

                // === タンカー：パッシブスキル（1キー）===
                ["Tanker_Passive_DamageReduction"] =
                "【タンカーパッシブ被ダメージ減少(%)】\n" +
                "タンカーパッシブ：常時被ダメージを減少。\n" +
                "推奨：10-20%",

                ["Tanker_NormalShield_SpeedBonus"] =
                "【タンカー: 通常盾 移動速度ボーナス (%)】\n" +
                "タンカーLv1+：通常盾装備時に移動速度が上昇。\n" +
                "デフォルト：25%",

                ["Tanker_TowerShield_SpeedBonus"] =
                "【タンカー: タワー盾 移動速度ボーナス (%)】\n" +
                "タンカーLv1+：タワー盾装備時に移動速度が上昇。\n" +
                "デフォルト：30%",

                // === Lv1 ===
                ["Tanker_ReflectDuration_Lv1"] =
                "【タンカー反射持続時間Lv1 (秒)】\n" +
                "デフォルト：10秒",

                ["Tanker_Hp_Bonus_Lv1"] =
                "【タンカー Lv1 HPボーナス (%)】\n" +
                "タンカーLv1達成時、最大HPがパーセント増加します。\n" +
                "デフォルト：25",

                // === Lv2 ===
                ["Tanker_Hp_Bonus_Lv2"] =
                "【タンカー Lv2 HPボーナス (%)】\n" +
                "タンカーLv2達成時、最大HPがパーセント増加します。\n" +
                "デフォルト：30",

                ["Tanker_Lv2_BlockPower"] =
                "【タンカー Lv2 ブロック防御力】\n" +
                "タンカーLv2パッシブブロック防御力。\n" +
                "デフォルト：5",

                ["Tanker_ReflectDuration_Lv2"] =
                "【タンカー反射持続時間Lv2 (秒)】\n" +
                "デフォルト：12秒",

                // === Lv3 ===
                ["Tanker_Hp_Bonus_Lv3"] =
                "【タンカー Lv3 HPボーナス (%)】\n" +
                "タンカーLv3達成時、最大HPがパーセント増加します。\n" +
                "デフォルト：35",

                ["Tanker_Lv3_BlockPower"] =
                "【タンカー Lv3 ブロック防御力】\n" +
                "タンカーLv3パッシブブロック防御力。\n" +
                "デフォルト：10",

                ["Tanker_ReflectDuration_Lv3"] =
                "【タンカー反射持続時間Lv3 (秒)】\n" +
                "デフォルト：14秒",

                // === Lv4 ===
                ["Tanker_Hp_Bonus_Lv4"] =
                "【タンカー Lv4 HPボーナス (%)】\n" +
                "タンカーLv4達成時、最大HPがパーセント増加します。\n" +
                "デフォルト：40",

                ["Tanker_Lv4_BlockPower"] =
                "【タンカー Lv4 ブロック防御力】\n" +
                "タンカーLv4パッシブブロック防御力。\n" +
                "デフォルト：15",

                ["Tanker_ReflectDuration_Lv4"] =
                "【タンカー反射持続時間Lv4 (秒)】\n" +
                "デフォルト：16秒",

                // === Lv5 ===
                ["Tanker_Hp_Bonus_Lv5"] =
                "【タンカー Lv5 HPボーナス (%)】\n" +
                "タンカーLv5達成時、最大HPがパーセント増加します。\n" +
                "デフォルト：50",

                ["Tanker_Lv5_BlockPower"] =
                "【タンカー Lv5 ブロック防御力】\n" +
                "タンカーLv5パッシブブロック防御力。\n" +
                "デフォルト：20",

                ["Tanker_ReflectDuration_Lv5"] =
                "【タンカー反射持続時間Lv5 (秒)】\n" +
                "デフォルト：20秒",

                // ========================================
                // ローグ職業スキル (Rogue Job)
                // ========================================

                // === ローグ：アクティブスキル《シャドーストライク》（7キー）===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【シャドーストライククールダウン（秒）】\n" +
                "シャドーストライク再使用前の待機時間。\n" +
                "推奨：20-40秒",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【シャドーストライクスタミナ消費】\n" +
                "シャドーストライク発動時に消費するスタミナ。\n" +
                "推奨：20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【シャドーストライク攻撃ボーナス(%)】\n" +
                "発動後バフ持続中の攻撃力上昇量。\n" +
                "推奨：25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【攻撃バフ持続時間（秒）】\n" +
                "攻撃力上昇バフが有効な時間。\n" +
                "推奨：6-12秒",

                ["Rogue_ShadowStrike_SmokeScale"] =
                "【煙エフェクトスケール】\n" +
                "煙VFXのサイズ倍率。\n" +
                "推奨：1.5-3.0",

                ["Rogue_ShadowStrike_AggroRange"] =
                "【ヘイトクリア範囲（メートル）】\n" +
                "この半径内の全ての敵のヘイトをクリア。\n" +
                "推奨：10-20m",

                ["Rogue_ShadowStrike_StealthDuration"] =
                "【ローグステルス持続時間（秒）】\n" +
                "ステルスモードが有効な時間。\n" +
                "推奨：5-10秒",

                // === ローグ：パッシブスキル（3キー）===
                ["Rogue_AttackSpeed_Bonus"] =
                "【攻撃速度ボーナス(%)】\n" +
                "ローグパッシブ：常時攻撃速度を向上。\n" +
                "推奨：8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【攻撃スタミナ消費減少(%)】\n" +
                "ローグパッシブ：攻撃時のスタミナ消費を減少。\n" +
                "推奨：10-20%",

                ["Rogue_Lv1_DodgeChance"] =
                "【Lv1 回避率(%)】\n" +
                "ローグパッシブ：命中に対する回避率を上げる。スキルツリー合計に加算。\n" +
                "推奨：3-6%",
                ["Rogue_Lv2_DodgeChance"] = "【Lv2 回避率(%)】\n推奨：5-8%",
                ["Rogue_Lv3_DodgeChance"] = "【Lv3 回避率(%)】\n推奨：7-10%",
                ["Rogue_Lv4_DodgeChance"] = "【Lv4 回避率(%)】\n推奨：9-12%",
                ["Rogue_Lv5_DodgeChance"] = "【Lv5 回避率(%)】\n推奨：11-15%",

                // ========================================
                // パラディン職業スキル (Paladin Job)
                // ========================================

                // === パラディン：アクティブスキル《神聖回復》（8キー）===
                ["Paladin_Active_Cooldown"] =
                "【神聖回復クールダウン（秒）】\n" +
                "スキル再使用前の待機時間。\n" +
                "推奨：20-45秒",

                ["Paladin_Active_Range"] =
                "【神聖回復範囲（メートル）】\n" +
                "味方を回復する半径範囲。\n" +
                "推奨：4-8m",

                ["Paladin_Active_EitrCost"] =
                "【神聖回復エイトル消費】\n" +
                "神聖回復発動時に消費するエイトル。\n" +
                "推奨：8-15",

                ["Paladin_Active_StaminaCost"] =
                "【神聖回復スタミナ消費】\n" +
                "神聖回復発動時に消費するスタミナ。\n" +
                "推奨：8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【自己回復パーセント（最大HP%）】\n" +
                "発動時に回復する自身のHP割合。\n" +
                "推奨：10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【味方継続回復（最大HP%/秒）】\n" +
                "毎秒各味方のHPを回復するパーセント。\n" +
                "推奨：1-3%",

                ["Paladin_Active_Duration"] =
                "【回復持続時間（秒）】\n" +
                "味方への継続回復効果の総持続時間。\n" +
                "推奨：8-15秒",

                ["Paladin_Active_Interval"] =
                "【回復間隔（秒）】\n" +
                "回復効果が発動する間隔。\n" +
                "推奨：1秒",

                // === パラディン：パッシブスキル（1キー）===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【物理・属性耐性ボーナス(%)】\n" +
                "パラディンパッシブ：物理・属性ダメージへの耐性を向上。\n" +
                "推奨：5-12%",

                // === パラディン Lv2-5 ===
                ["Paladin_Lv2_SelfHealPercent"] = "【Lv2 自己回復率(%)】\n推奨：15-20%",
                ["Paladin_Lv2_AllyHealPercent"] = "【Lv2 味方回復率(%/ティック)】\n推奨：2-3%",
                ["Paladin_Lv3_SelfHealPercent"] = "【Lv3 自己回復率(%)】\n推奨：17-22%",
                ["Paladin_Lv3_AllyHealPercent"] = "【Lv3 味方回復率(%/ティック)】\n推奨：2.5-3.5%",
                ["Paladin_Lv3_HealRange"] = "【Lv3 回復範囲(m)】\n推奨：5-7m",
                ["Paladin_Lv4_SelfHealPercent"] = "【Lv4 自己回復率(%)】\n推奨：19-24%",
                ["Paladin_Lv4_AllyHealPercent"] = "【Lv4 味方回復率(%/ティック)】\n推奨：3-4%",
                ["Paladin_Lv4_HealRange"] = "【Lv4 回復範囲(m)】\n推奨：6-8m",
                ["Paladin_Lv5_SelfHealPercent"] = "【Lv5 自己回復率(%)】\n推奨：22-28%",
                ["Paladin_Lv5_AllyHealPercent"] = "【Lv5 味方回復率(%/ティック)】\n推奨：3.5-5%",
                ["Paladin_Lv5_HealRange"] = "【Lv5 回復範囲(m)】\n推奨：7-10m",
                ["Paladin_Lv2_Cooldown"] = "【Lv2 クールダウン(秒)】\n推奨：25-35秒",
                ["Paladin_Lv3_Cooldown"] = "【Lv3 クールダウン(秒)】\n推奨：24-34秒",
                ["Paladin_Lv4_Cooldown"] = "【Lv4 クールダウン(秒)】\n推奨：23-33秒",
                ["Paladin_Lv5_Cooldown"] = "【Lv5 クールダウン(秒)】\n推奨：20-30秒",
                ["Paladin_Lv2_ResistanceReduction"] = "【Lv2 耐性減少(%)】\n推奨：6-10%",
                ["Paladin_Lv3_ResistanceReduction"] = "【Lv3 耐性減少(%)】\n推奨：8-12%",
                ["Paladin_Lv4_ResistanceReduction"] = "【Lv4 耐性減少(%)】\n推奨：10-14%",
                ["Paladin_Lv5_ResistanceReduction"] = "【Lv5 耐性減少(%)】\n推奨：12-18%",
                ["Paladin_Lv2_StaminaBonus"] = "【Lv2 最大スタミナボーナス】\n推奨：8-15",
                ["Paladin_Lv3_StaminaBonus"] = "【Lv3 最大スタミナボーナス】\n推奨：12-20",
                ["Paladin_Lv4_StaminaBonus"] = "【Lv4 最大スタミナボーナス】\n推奨：15-25",
                ["Paladin_Lv5_StaminaBonus"] = "【Lv5 最大スタミナボーナス】\n推奨：20-30",

                // ========================================
                // バーサーカー職業スキル (Berserker Job)
                // ========================================

                // === バーサーカー：アクティブスキル《バーサーク》（6キー、Beserkerスペル維持）===
                ["Beserker_Active_Cooldown"] =
                "【バーサーククールダウン（秒）】\n" +
                "バーサーク再使用前の待機時間。\n" +
                "推奨：30-60秒",

                ["Beserker_Active_StaminaCost"] =
                "【バーサークスタミナ消費】\n" +
                "バーサーク発動時に消費するスタミナ。\n" +
                "推奨：15-25",

                ["Beserker_Active_Duration"] =
                "【バーサーク持続時間（秒）】\n" +
                "バーサークバフが有効な時間。\n" +
                "推奨：15-25秒",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【HP1%減少ごとのダメージボーナス(%)】\n" +
                "HPが低いほどダメージが高い。\n" +
                "減少HP% × この値 = ダメージボーナス\n" +
                "推奨：1.5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【最大ダメージボーナス(%)】\n" +
                "HP連動ダメージボーナスの最大上限。\n" +
                "推奨：150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【発動HP閾値(%)】\n" +
                "HPがこの割合を下回るとHP連動ダメージボーナスが発動。\n" +
                "100%に設定すると常時発動。\n" +
                "推奨：50-100%",

                // === バーサーカー：パッシブスキル《死への挑戦》（3キー、Beserkerスペル維持）===
                ["Berserker_Passive_HealthThreshold"] =
                "【パッシブ発動HP閾値(%)】\n" +
                "HPがこの割合を下回ると無敵状態が発動。\n" +
                "推奨：8-15%",

                ["Berserker_Passive_InvincibilityDuration"] =
                "【無敵持続時間（秒）】\n" +
                "パッシブ発動時の無敵状態持続時間。\n" +
                "推奨：5-10秒",

                ["Berserker_Passive_Cooldown"] =
                "【パッシブクールダウン（秒）】\n" +
                "次のパッシブ無敵発動まで待機時間。\n" +
                "デフォルト：540秒（9分）\n" +
                "推奨：120-300秒",

                // === バーサーカー：パッシブHP増加 ===
                ["Berserker_Passive_HealthBonus"] =
                "【最大HP増加(%)】\n" +
                "バーサーカーパッシブ：最大HPを向上。\n" +
                "推奨：100%",

                // === バーサーカー：各レベルの怒りクールタイム ===
                ["Berserker_Lv1_Active_Cooldown"] =
                "【バーサーカー Lv1：怒りクールタイム（秒）】\n" +
                "Lv1の怒りスキルクールタイム。\n" +
                "推奨：45秒",

                ["Berserker_Lv2_Active_Cooldown"] =
                "【バーサーカー Lv2：怒りクールタイム（秒）】\n" +
                "Lv2の怒りスキルクールタイム。\n" +
                "推奨：40秒",

                ["Berserker_Lv3_Active_Cooldown"] =
                "【バーサーカー Lv3：怒りクールタイム（秒）】\n" +
                "Lv3の怒りスキルクールタイム。\n" +
                "推奨：40秒",

                ["Berserker_Lv4_Active_Cooldown"] =
                "【バーサーカー Lv4：怒りクールタイム（秒）】\n" +
                "Lv4の怒りスキルクールタイム。\n" +
                "推奨：40秒",

                ["Berserker_Lv5_Active_Cooldown"] =
                "【バーサーカー Lv5：怒りクールタイム（秒）】\n" +
                "Lv5の怒りスキルクールタイム。\n" +
                "推奨：35秒",

                // === バーサーカー：各レベルの怒り持続時間 ===
                ["Berserker_Lv1_Active_Duration"] =
                "【バーサーカー Lv1：怒り持続時間（秒）】\n" +
                "Lv1の怒り効果持続時間。\n" +
                "推奨：20秒",

                ["Berserker_Lv2_Active_Duration"] =
                "【バーサーカー Lv2：怒り持続時間（秒）】\n" +
                "Lv2の怒り効果持続時間。\n" +
                "推奨：20秒",

                ["Berserker_Lv3_Active_Duration"] =
                "【バーサーカー Lv3：怒り持続時間（秒）】\n" +
                "Lv3の怒り効果持続時間。\n" +
                "推奨：25秒",

                ["Berserker_Lv4_Active_Duration"] =
                "【バーサーカー Lv4：怒り持続時間（秒）】\n" +
                "Lv4の怒り効果持続時間。\n" +
                "推奨：25秒",

                ["Berserker_Lv5_Active_Duration"] =
                "【バーサーカー Lv5：怒り持続時間（秒）】\n" +
                "Lv5の怒り効果持続時間。\n" +
                "推奨：25秒",

                // === バーサーカー：各レベルのパッシブ最大HPボーナス ===
                ["Berserker_Lv1_Passive_HealthBonus"] =
                "【バーサーカー Lv1：最大HPボーナス】\n" +
                "Lv1のフラット最大HPボーナス。\n" +
                "推奨：40",

                ["Berserker_Lv2_Passive_HealthBonus"] =
                "【バーサーカー Lv2：最大HPボーナス】\n" +
                "Lv2のフラット最大HPボーナス。\n" +
                "推奨：60",

                ["Berserker_Lv3_Passive_HealthBonus"] =
                "【バーサーカー Lv3：最大HPボーナス】\n" +
                "Lv3のフラット最大HPボーナス。\n" +
                "推奨：80",

                ["Berserker_Lv4_Passive_HealthBonus"] =
                "【バーサーカー Lv4：最大HPボーナス】\n" +
                "Lv4のフラット最大HPボーナス。\n" +
                "推奨：100",

                ["Berserker_Lv5_Passive_HealthBonus"] =
                "【バーサーカー Lv5：最大HPボーナス】\n" +
                "Lv5のフラット最大HPボーナス。\n" +
                "推奨：120",

                // === バーサーカー：各レベルの失ったHP1%ごとの攻撃力増加 ===
                ["Berserker_Lv1_Active_DamagePerHP"] =
                "【バーサーカー Lv1：失HP1%ごとの攻撃力増加(%)】\n" +
                "怒り中、失ったHP1%ごとの攻撃力ボーナス(Lv1)。\n" +
                "推奨：1.5%",

                ["Berserker_Lv2_Active_DamagePerHP"] =
                "【バーサーカー Lv2：失HP1%ごとの攻撃力増加(%)】\n" +
                "怒り中、失ったHP1%ごとの攻撃力ボーナス(Lv2)。\n" +
                "推奨：1.6%",

                ["Berserker_Lv3_Active_DamagePerHP"] =
                "【バーサーカー Lv3：失HP1%ごとの攻撃力増加(%)】\n" +
                "怒り中、失ったHP1%ごとの攻撃力ボーナス(Lv3)。\n" +
                "推奨：1.7%",

                ["Berserker_Lv4_Active_DamagePerHP"] =
                "【バーサーカー Lv4：失HP1%ごとの攻撃力増加(%)】\n" +
                "怒り中、失ったHP1%ごとの攻撃力ボーナス(Lv4)。\n" +
                "推奨：1.8%",

                ["Berserker_Lv5_Active_DamagePerHP"] =
                "【バーサーカー Lv5：失HP1%ごとの攻撃力増加(%)】\n" +
                "怒り中、失ったHP1%ごとの攻撃力ボーナス(Lv5)。\n" +
                "推奨：2.0%",

                // ========================================
                // 製作専家 (Producer) 職業スキル
                // ========================================
                ["Producer_Buff_Cooldown"] =
                "【職人の祝福：クールダウン（秒）】\n" +
                "製作専家Buffが発動できるようになるまでのクールダウン。\n" +
                "デフォルト：180秒",

                ["Producer_Buff_Duration"] =
                "【職人の祝福：持続時間（秒）】\n" +
                "味方の攻撃力/HP増加バフの持続時間。\n" +
                "デフォルト：120秒",

                ["Producer_Buff_Range"] =
                "【職人の祝福：範囲（メートル）】\n" +
                "この範囲内の味方がバフを受ける。\n" +
                "デフォルト：15m",

                ["Producer_Buff_AttackBonus"] =
                "【バフ攻撃力ボーナス(%)】\n" +
                "バフを受けた味方への攻撃力ボーナス。\n" +
                "デフォルト：15%",

                ["Producer_Buff_MaxHealthBonus"] =
                "【バフ最大HPボーナス(%)】\n" +
                "バフを受けた味方への最大HPボーナス。\n" +
                "デフォルト：15%",

                ["Producer_Buff_StaminaCost"] =
                "【バフスタミナ消費】\n" +
                "バフ発動時に消費するスタミナ値。\n" +
                "デフォルト：20",

                // === Producer Lv1 ===
                ["Producer_EnchantChance_Lv1"] = "【エンチャント確率 Lv1 (%)】\nLv1時に製作アイテムをエンチャントする確率。\nデフォルト：45%",
                ["Producer_ElementalProcChance_Lv1"] = "【属性ダメージ発動確率 Lv1 (%)】\nLv1の属性エンチャント(火/霊/毒/雷/氷)が攻撃ごとに発動する確率。\nデフォルト：25%",

                // === Producer Lv2 ===
                ["Producer_Durability_Lv2"] = "【製作品耐久度ボーナス Lv2 (%)】\nLv2時の製作アイテム耐久度ボーナス。\nデフォルト：10%",
                ["Producer_MaterialReduction_Lv2"] = "【素材消費減少 Lv2 (%)】\nLv2時の製作ごとに節約される素材。\nデフォルト：10%",
                ["Producer_EnchantChance_Lv2"] = "【エンチャント確率 Lv2 (%)】\nLv2時に製作アイテムをエンチャントする確率。\nデフォルト：55%",
                ["Producer_ElementalProcChance_Lv2"] = "【属性ダメージ発動確率 Lv2 (%)】\nLv2の属性エンチャントが攻撃ごとに発動する確率。\nデフォルト：30%",

                // === Producer Lv3 ===
                ["Producer_Durability_Lv3"] = "【製作品耐久度ボーナス Lv3 (%)】\nLv3時の製作アイテム耐久度ボーナス。\nデフォルト：15%",
                ["Producer_MaterialReduction_Lv3"] = "【素材消費減少 Lv3 (%)】\nLv3時の製作ごとに節約される素材。\nデフォルト：15%",
                ["Producer_EnchantChance_Lv3"] = "【エンチャント確率 Lv3 (%)】\nLv3時に製作アイテムをエンチャントする確率。\nデフォルト：25%",
                ["Producer_ElementalProcChance_Lv3"] = "【属性ダメージ発動確率 Lv3 (%)】\nLv3の属性エンチャントが攻撃ごとに発動する確率。\nデフォルト：35%",

                // === Producer Lv4 ===
                ["Producer_Durability_Lv4"] = "【製作品耐久度ボーナス Lv4 (%)】\nLv4時の製作アイテム耐久度ボーナス。\nデフォルト：20%",
                ["Producer_MaterialReduction_Lv4"] = "【素材消費減少 Lv4 (%)】\nLv4時の製作ごとに節約される素材。\nデフォルト：20%",
                ["Producer_EnchantChance_Lv4"] = "【エンチャント確率 Lv4 (%)】\nLv4時に製作アイテムをエンチャントする確率。\nデフォルト：80%",
                ["Producer_ElementalProcChance_Lv4"] = "【属性ダメージ発動確率 Lv4 (%)】\nLv4の属性エンチャントが攻撃ごとに発動する確率。\nデフォルト：40%",

                // === Producer Lv5 ===
                ["Producer_Durability_Lv5"] = "【製作品耐久度ボーナス Lv5 (%)】\nLv5時の製作アイテム耐久度ボーナス。\nデフォルト：30%",
                ["Producer_MaterialReduction_Lv5"] = "【素材消費減少 Lv5 (%)】\nLv5時の製作ごとに節約される素材。\nデフォルト：30%",
                ["Producer_EnchantChance_Lv5"] = "【エンチャント確率 Lv5 (%)】\nLv5時に製作アイテムをエンチャントする確率。\nデフォルト：35%",
                ["Producer_ElementalProcChance_Lv5"] = "【属性ダメージ発動確率 Lv5 (%)】\nLv5の属性エンチャントが攻撃ごとに発動する確率。\nデフォルト：45%",

                ["Job_Lv1_Cost"] = "【職業Lv1コインコスト】\nすべての職業をLv1にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：1000",
                ["Job_Lv2_Cost"] = "【職業Lv2コインコスト】\nすべての職業をLv2にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：2000",
                ["Job_Lv3_Cost"] = "【職業Lv3コインコスト】\nすべての職業をLv3にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：3000",
                ["Job_Lv4_Cost"] = "【職業Lv4コインコスト】\nすべての職業をLv4にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：4000",
                ["Job_Lv5_Cost"] = "【職業Lv5コインコスト】\nすべての職業をLv5にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：5000",

                ["Job_Reset_Cost"]    = "【職業スキルリセットコスト】\n職業スキルポイントをリセットする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：1000",
                ["Active_Reset_Cost"] = "【アクティブスキルリセットコスト】\nアクティブスキルポイントをリセットする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：500",
                ["Passive_Reset_Cost"]= "【パッシブスキルリセットコスト】\nパッシブスキルポイントをリセットする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：100",

                ["HotKey_Y"] =
                "【職業スキルキー】\n" +
                "職業のアクティブスキルを発動するキーです。\n" +
                "デフォルト: Y",

                ["HotKey_R"] =
                "【遠距離スキルキー】\n" +
                "遠距離アクティブスキル（マルチショット、デュアルキャストなど）を発動するキーです。\n" +
                "デフォルト: R",

                ["HotKey_G"] =
                "【近接メインスキルキー】\n" +
                "近接メインアクティブスキル（突進斬りなど）を発動するキーです。\n" +
                "デフォルト: G",

                ["HotKey_H"] =
                "【補助スキルキー】\n" +
                "補助アクティブスキル（連功槍、守護者の真心など）を発動するキーです。\n" +
                "デフォルト: H",

                ["QuestToggleKey"] =
                "【クエストパネルのショートカット】\n" +
                "クエストパネルを開閉するショートカットです。\n" +
                "デフォルト: Ctrl+J",

                ["HUD_IconSize"] =
                "【スキルアイコンサイズ】\n" +
                "アクティブスキルHUDに表示されるアイコンのサイズです。\n" +
                "デフォルト: 62",

                ["HUD_PosX"] =
                "【スキルアイコンHUD X位置】\n" +
                "アクティブスキルHUDの左右の位置です。\n" +
                "デフォルト: 306（画面左基準）",

                ["HUD_PosY"] =
                "【スキルアイコンHUD Y位置】\n" +
                "アクティブスキルHUDの上下の位置です。\n" +
                "デフォルト: 139（画面下基準）",

                ["Archer_Attack_StaminaReduction_Lv1"] =
                "【Lv1パッシブ: 攻撃スタミナ消費削減 (%)】\n" +
                "アーチャーLv1で攻撃時に消費するスタミナを削減します。\n" +
                "弓/クロスボウ/杖の全攻撃に適用されます。\n" +
                "推奨: 10-20%",

                ["Archer_Attack_StaminaReduction_Lv2"] =
                "【Lv2パッシブ: 攻撃スタミナ消費削減 (%)】\n" +
                "アーチャーLv2で攻撃時に消費するスタミナを削減します。\n" +
                "推奨: 20-30%",

                ["Archer_Attack_StaminaReduction_Lv3"] =
                "【Lv3パッシブ: 攻撃スタミナ消費削減 (%)】\n" +
                "アーチャーLv3で攻撃時に消費するスタミナを削減します。\n" +
                "推奨: 30-40%",

                ["Archer_Attack_StaminaReduction_Lv4"] =
                "【Lv4パッシブ: 攻撃スタミナ消費削減 (%)】\n" +
                "アーチャーLv4で攻撃時に消費するスタミナを削減します。\n" +
                "推奨: 40-50%",

                ["Archer_Attack_StaminaReduction_Lv5"] =
                "【Lv5パッシブ: 攻撃スタミナ消費削減 (%)】\n" +
                "アーチャーLv5で攻撃時に消費するスタミナを削減します。\n" +
                "推奨: 50-60%",

                ["Archer_AmmoSaveChance"] =
                "【矢/ボルト消費免除確率 (%)】\n" +
                "攻撃時に矢またはボルトを消費しない確率です。\n" +
                "50に設定すると平均で半分の矢が節約されます。\n" +
                "推奨: 30-60%",

                ["Archer_TameHeal_PerLevel"] =
                "【パッシブ: テイム生物回復 (秒間HP)】\n" +
                "アーチャーレベル × この値の分、周囲のテイムした生物を毎秒回復させます。\n" +
                "Lv1ではこの値、Lv5では5倍回復します。\n" +
                "推奨: 1",

                ["Archer_TameHeal_Range"] =
                "【パッシブ: テイム生物回復範囲 (m)】\n" +
                "アーチャー周囲のこの距離以内のテイム生物に回復効果が適用されます。\n" +
                "推奨: 8-15",

                ["Mage_Lv1_Cooldown"] =
                "【クールダウン Lv1（秒）】\n" +
                "メイジLv1スキルの再使用待機時間です。\n" +
                "推奨: 120秒",

                ["Mage_Lv2_Cooldown"] =
                "【クールダウン Lv2（秒）】\n" +
                "メイジLv2スキルの再使用待機時間です。\n" +
                "推奨: 110秒",

                ["Mage_Lv3_Cooldown"] =
                "【クールダウン Lv3（秒）】\n" +
                "メイジLv3スキルの再使用待機時間です。\n" +
                "推奨: 100秒",

                ["Mage_Lv4_Cooldown"] =
                "【クールダウン Lv4（秒）】\n" +
                "メイジLv4スキルの再使用待機時間です。\n" +
                "推奨: 90秒",

                ["Mage_Lv5_Cooldown"] =
                "【クールダウン Lv5（秒）】\n" +
                "メイジLv5スキルの再使用待機時間です。\n" +
                "推奨: 80秒",

                ["Mage_Lv1_AOE_Max_Targets"] =
                "【最大対象数 Lv1】\n" +
                "メイジLv1が同時にヒットさせる最大モンスター数です。近い順に選択されます。\n" +
                "推奨: 6",

                ["Mage_Lv2_AOE_Max_Targets"] =
                "【最大対象数 Lv2】\n" +
                "メイジLv2が同時にヒットさせる最大モンスター数です。\n" +
                "推奨: 7",

                ["Mage_Lv3_AOE_Max_Targets"] =
                "【最大対象数 Lv3】\n" +
                "メイジLv3が同時にヒットさせる最大モンスター数です。\n" +
                "推奨: 8",

                ["Mage_Lv4_AOE_Max_Targets"] =
                "【最大対象数 Lv4】\n" +
                "メイジLv4が同時にヒットさせる最大モンスター数です。\n" +
                "推奨: 9",

                ["Mage_Lv5_AOE_Max_Targets"] =
                "【最大対象数 Lv5】\n" +
                "メイジLv5が同時にヒットさせる最大モンスター数です。\n" +
                "推奨: 10",

                ["Mage_Lv1_Elemental_Resistance"] =
                "【魔法属性耐性 Lv1 (%)】\n" +
                "Lv1メイジの属性耐性です。火炎/氷/雷/毒/霊魂ダメージを減少させます。\n" +
                "推奨: 5%",

                ["Mage_Lv2_Elemental_Resistance"] =
                "【魔法属性耐性 Lv2 (%)】\n" +
                "Lv2メイジの属性耐性です。追加詠唱+1回（30秒以内）を含みます。\n" +
                "推奨: 7%",

                ["Mage_Lv3_Elemental_Resistance"] =
                "【魔法属性耐性 Lv3 (%)】\n" +
                "Lv3メイジの属性耐性です。\n" +
                "推奨: 9%",

                ["Mage_Lv4_Elemental_Resistance"] =
                "【魔法属性耐性 Lv4 (%)】\n" +
                "Lv4メイジの属性耐性です。\n" +
                "推奨: 12%",

                ["Mage_Lv5_Elemental_Resistance"] =
                "【魔法属性耐性 Lv5 (%)】\n" +
                "Lv5メイジの属性耐性です。\n" +
                "推奨: 15%",

                ["Mage_Lv1_Damage_Multiplier"] =
                "【範囲ダメージ倍率 Lv1 (%)】\n" +
                "メイジLv1の範囲ダメージ倍率です。\n" +
                "推奨: 70%",

                ["Mage_Lv2_Damage_Multiplier"] =
                "【範囲ダメージ倍率 Lv2 (%)】\n" +
                "メイジLv2の範囲ダメージ倍率です。\n" +
                "推奨: 90%",

                ["Mage_Lv3_Damage_Multiplier"] =
                "【範囲ダメージ倍率 Lv3 (%)】\n" +
                "メイジLv3の範囲ダメージ倍率です。\n" +
                "推奨: 110%",

                ["Mage_Lv4_Damage_Multiplier"] =
                "【範囲ダメージ倍率 Lv4 (%)】\n" +
                "メイジLv4の範囲ダメージ倍率です。\n" +
                "推奨: 130%",

                ["Mage_Lv5_Damage_Multiplier"] =
                "【範囲ダメージ倍率 Lv5 (%)】\n" +
                "メイジLv5の範囲ダメージ倍率です。\n" +
                "推奨: 150%",

                ["Tanker_Explosion_Radius"] =
                "【挑発爆発範囲 (m)】\n" +
                "タンカーの挑発スキル発動時の爆発効果の影響範囲です。\n" +
                "推奨: 6-12m",

                ["Tanker_BlockPower_Multiplier"] =
                "【シールドブロック力倍率】\n" +
                "タンカー職業レベルに応じてシールドのブロック力に乗算される倍率です。\n" +
                "推奨: 1.0-2.0",

                ["Rogue_Poison_Range"] =
                "【毒爆発範囲 (m)】\n" +
                "各毒爆発エフェクトの影響範囲です。\n" +
                "推奨: 8-15m",

                ["Rogue_Poison_InstantDamage"] =
                "【即時毒ダメージ】\n" +
                "エフェクト1回ごとに敵に与える即時毒ダメージです。\n" +
                "推奨: 8-20",

                ["Rogue_Poison_DotDamage"] =
                "【毒DoT秒間ダメージ】\n" +
                "毒の継続ダメージ（DoT）の秒間ダメージ量です。\n" +
                "推奨: 3-8",

                ["Rogue_Poison_DotDuration"] =
                "【毒DoT持続時間（秒）】\n" +
                "毒の継続ダメージが維持される時間です。\n" +
                "推奨: 8-15秒",

                ["Rogue_Poison_VFXCount"] =
                "【毒爆発回数】\n" +
                "スキル発動時の毒爆発エフェクトの繰り返し回数です。\n" +
                "推奨: 6-10",

                ["Rogue_Poison_VFXInterval"] =
                "【毒爆発間隔（秒）】\n" +
                "各毒爆発の間の時間間隔です。\n" +
                "推奨: 0.3-1.0秒",

                ["Rogue_Lv2_Cooldown"] = "【Lv2シャドウストライククールダウン（秒）】\n推奨: 25-30秒",

                ["Rogue_Lv3_Cooldown"] = "【Lv3シャドウストライククールダウン（秒）】\n推奨: 22-28秒",

                ["Rogue_Lv4_Cooldown"] = "【Lv4シャドウストライククールダウン（秒）】\n推奨: 20-26秒",

                ["Rogue_Lv5_Cooldown"] = "【Lv5シャドウストライククールダウン（秒）】\n推奨: 18-24秒",

                ["Rogue_Lv2_AttackBonus"] = "【Lv2攻撃力バフ (%)】\n推奨: 35-50%",

                ["Rogue_Lv3_AttackBonus"] = "【Lv3攻撃力バフ (%)】\n推奨: 40-55%",

                ["Rogue_Lv4_AttackBonus"] = "【Lv4攻撃力バフ (%)】\n推奨: 45-60%",

                ["Rogue_Lv5_AttackBonus"] = "【Lv5攻撃力バフ (%)】\n推奨: 50-65%",

                ["Rogue_Lv2_BuffDuration"] = "【Lv2バフ持続時間（秒）】\n推奨: 8-12秒",

                ["Rogue_Lv3_BuffDuration"] = "【Lv3バフ持続時間（秒）】\n推奨: 9-13秒",

                ["Rogue_Lv4_BuffDuration"] = "【Lv4バフ持続時間（秒）】\n推奨: 10-14秒",

                ["Rogue_Lv5_BuffDuration"] = "【Lv5バフ持続時間（秒）】\n推奨: 11-15秒",

                ["Rogue_Lv2_PoisonBlasts"] = "【Lv2毒爆発回数】\n推奨: 8-12",

                ["Rogue_Lv3_PoisonBlasts"] = "【Lv3毒爆発回数】\n推奨: 9-13",

                ["Rogue_Lv4_PoisonBlasts"] = "【Lv4毒爆発回数】\n推奨: 10-14",

                ["Rogue_Lv5_PoisonBlasts"] = "【Lv5毒爆発回数】\n推奨: 11-15",

                ["Rogue_Lv2_PoisonInstant"] = "【Lv2即時毒ダメージ】\n推奨: 10-15",

                ["Rogue_Lv3_PoisonInstant"] = "【Lv3即時毒ダメージ】\n推奨: 12-18",

                ["Rogue_Lv4_PoisonInstant"] = "【Lv4即時毒ダメージ】\n推奨: 14-20",

                ["Rogue_Lv5_PoisonInstant"] = "【Lv5即時毒ダメージ】\n推奨: 16-25",

                ["Rogue_Lv2_PoisonDot"] = "【Lv2毒DoT秒間ダメージ】\n推奨: 5-8",

                ["Rogue_Lv3_PoisonDot"] = "【Lv3毒DoT秒間ダメージ】\n推奨: 6-9",

                ["Rogue_Lv4_PoisonDot"] = "【Lv4毒DoT秒間ダメージ】\n推奨: 7-10",

                ["Rogue_Lv5_PoisonDot"] = "【Lv5毒DoT秒間ダメージ】\n推奨: 8-12",

                ["Rogue_ShadowStrike_Charges"] = "【シャドウストライク基本チャージ数】\n基本使用可能チャージ数です。\n推奨: 1",

                ["Rogue_Lv5_BonusCharges"] = "【Lv5追加チャージ数】\nLv5到達時に追加されるチャージ数です。\n推奨: 1",

                ["Rogue_Lv2_AttackSpeed"] = "【Lv2攻撃速度ボーナス (%)】\n推奨: 10-15%",

                ["Rogue_Lv3_AttackSpeed"] = "【Lv3攻撃速度ボーナス (%)】\n推奨: 12-18%",

                ["Rogue_Lv4_AttackSpeed"] = "【Lv4攻撃速度ボーナス (%)】\n推奨: 14-20%",

                ["Rogue_Lv5_AttackSpeed"] = "【Lv5攻撃速度ボーナス (%)】\n推奨: 16-22%",

                ["Rogue_Lv2_StaminaReduction"] = "【Lv2スタミナ消費削減 (%)】\n推奨: 15-20%",

                ["Rogue_Lv3_StaminaReduction"] = "【Lv3スタミナ消費削減 (%)】\n推奨: 17-22%",

                ["Rogue_Lv4_StaminaReduction"] = "【Lv4スタミナ消費削減 (%)】\n推奨: 19-25%",

                ["Rogue_Lv5_StaminaReduction"] = "【Lv5スタミナ消費削減 (%)】\n推奨: 22-30%",

                ["Rogue_Lv1_MoveSpeed"] = "【Lv1移動速度ボーナス (%)】\n推奨: 3-7%",

                ["Rogue_Lv2_MoveSpeed"] = "【Lv2移動速度ボーナス (%)】\n推奨: 5-10%",

                ["Rogue_Lv3_MoveSpeed"] = "【Lv3移動速度ボーナス (%)】\n推奨: 7-12%",

                ["Rogue_Lv4_MoveSpeed"] = "【Lv4移動速度ボーナス (%)】\n推奨: 10-15%",

                ["Rogue_Lv5_MoveSpeed"] = "【Lv5移動速度ボーナス (%)】\n推奨: 12-18%",

                ["Producer_Durability_Lv1"] = "【製作アイテム耐久度ボーナス Lv1 (%)】\nLv1での製作アイテムの耐久度増加率です。\nデフォルト: 50%",

            };
        }
    }
}

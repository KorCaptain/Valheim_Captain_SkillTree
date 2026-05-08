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
                ["Producer_EnchantChance_Lv1"] = "【エンチャント確率 Lv1 (%)】\nLv1時に製作アイテムをエンチャントする確率。\nデフォルト：0%",

                // === Producer Lv2 ===
                ["Producer_Durability_Lv2"] = "【製作品耐久度ボーナス Lv2 (%)】\nLv2時の製作アイテム耐久度ボーナス。\nデフォルト：10%",
                ["Producer_MaterialReduction_Lv2"] = "【素材消費減少 Lv2 (%)】\nLv2時の製作ごとに節約される素材。\nデフォルト：10%",
                ["Producer_EnchantChance_Lv2"] = "【エンチャント確率 Lv2 (%)】\nLv2時に製作アイテムをエンチャントする確率。\nデフォルト：0%",

                // === Producer Lv3 ===
                ["Producer_Durability_Lv3"] = "【製作品耐久度ボーナス Lv3 (%)】\nLv3時の製作アイテム耐久度ボーナス。\nデフォルト：15%",
                ["Producer_MaterialReduction_Lv3"] = "【素材消費減少 Lv3 (%)】\nLv3時の製作ごとに節約される素材。\nデフォルト：15%",
                ["Producer_EnchantChance_Lv3"] = "【エンチャント確率 Lv3 (%)】\nLv3時に製作アイテムをエンチャントする確率。\nデフォルト：25%",

                // === Producer Lv4 ===
                ["Producer_Durability_Lv4"] = "【製作品耐久度ボーナス Lv4 (%)】\nLv4時の製作アイテム耐久度ボーナス。\nデフォルト：20%",
                ["Producer_MaterialReduction_Lv4"] = "【素材消費減少 Lv4 (%)】\nLv4時の製作ごとに節約される素材。\nデフォルト：20%",
                ["Producer_EnchantChance_Lv4"] = "【エンチャント確率 Lv4 (%)】\nLv4時に製作アイテムをエンチャントする確率。\nデフォルト：30%",

                // === Producer Lv5 ===
                ["Producer_Durability_Lv5"] = "【製作品耐久度ボーナス Lv5 (%)】\nLv5時の製作アイテム耐久度ボーナス。\nデフォルト：30%",
                ["Producer_MaterialReduction_Lv5"] = "【素材消費減少 Lv5 (%)】\nLv5時の製作ごとに節約される素材。\nデフォルト：30%",
                ["Producer_EnchantChance_Lv5"] = "【エンチャント確率 Lv5 (%)】\nLv5時に製作アイテムをエンチャントする確率。\nデフォルト：35%",

                ["Job_Lv1_Cost"] = "【職業Lv1コインコスト】\nすべての職業をLv1にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：1000",
                ["Job_Lv2_Cost"] = "【職業Lv2コインコスト】\nすべての職業をLv2にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：2000",
                ["Job_Lv3_Cost"] = "【職業Lv3コインコスト】\nすべての職業をLv3にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：3000",
                ["Job_Lv4_Cost"] = "【職業Lv4コインコスト】\nすべての職業をLv4にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：4000",
                ["Job_Lv5_Cost"] = "【職業Lv5コインコスト】\nすべての職業をLv5にアップグレードする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：5000",

                ["Job_Reset_Cost"]    = "【職業スキルリセットコスト】\n職業スキルポイントをリセットする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：1000",
                ["Active_Reset_Cost"] = "【アクティブスキルリセットコスト】\nアクティブスキルポイントをリセットする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：500",
                ["Passive_Reset_Cost"]= "【パッシブスキルリセットコスト】\nパッシブスキルポイントをリセットする際に消費するコイン数。\nサーバー管理者のみ変更可、クライアントに自動同期。\nデフォルト：100",
            };
        }
    }
}

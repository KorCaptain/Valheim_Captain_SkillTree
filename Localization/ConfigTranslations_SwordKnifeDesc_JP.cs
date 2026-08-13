using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetSwordKnifeDescriptions_JP()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // 剣ツリー (Sword Tree)
                // ========================================

                // === Tier 0: 剣専家 ===
                ["Sword_Expert_DamageIncrease"] =
                "【剣ダメージボーナス (%)】\n" +
                "剣の基礎攻撃力を増加させます。\n" +
                "全剣種類に適用されます。\n" +
                "推奨: 10-20%",

                ["Tier0_SwordExpert_DamageBonus"] =
                "【剣ダメージボーナス (%)】\n" +
                "剣の基礎攻撃力を増加させます。\n" +
                "全剣種類に適用されます。\n" +
                "推奨: 10-20%",

                ["Tier0_SwordExpert_RequiredPoints"] =
                "【必要ポイント】\n剣専家を解放するための必要ポイントです。",

                // === Tier 1: 素早い斬撃 ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【攻撃速度ボーナス (%)】\n" +
                "剣の攻撃速度を増加させます。\n" +
                "素早い連続攻撃が可能になります。\n" +
                "推奨: 10-20%",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【攻撃速度ボーナス (%)】\n" +
                "剣の攻撃速度を増加させます。\n" +
                "素早い連続攻撃が可能になります。\n" +
                "推奨: 10-20%",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【必要ポイント】\n素早い斬撃を解放するための必要ポイントです。",

                // === Tier 1: カウンタースタンス ===
                ["Tier1_CounterStance_Duration"] =
                "【持続時間（秒）】\n" +
                "カウンタースタンスを維持する時間です。\n" +
                "この間、防御力が増加します。\n" +
                "推奨: 3-6秒",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【防御力ボーナス (%)】\n" +
                "カウンタースタンス中の防御力増加量です。\n" +
                "敵の攻撃を耐えながら反撃機会を狙います。\n" +
                "推奨: 20-40%",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【必要ポイント】\nカウンタースタンスを解放するための必要ポイントです。",

                // === Tier 2: コンボスラッシュ ===
                ["Sword_ComboSlash_Bonus"] =
                "【コンボボーナス (%)】\n" +
                "コンボ攻撃時に追加ダメージを与えます。\n" +
                "コンボを維持することで高いDPSを発揮します。\n" +
                "推奨: 15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【バフ持続時間（秒）】\n" +
                "コンボボーナスが持続する時間です。\n" +
                "この間に攻撃するとバフが延長されます。\n" +
                "推奨: 3-5秒",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【コンボボーナス (%)】\n" +
                "コンボ攻撃時に追加ダメージを与えます。\n" +
                "コンボを維持することで高いDPSを発揮します。\n" +
                "推奨: 15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【バフ持続時間（秒）】\n" +
                "コンボボーナスが持続する時間です。\n" +
                "この間に攻撃するとバフが延長されます。\n" +
                "推奨: 3-5秒",

                ["Tier2_ComboSlash_RequiredPoints"] =
                "【必要ポイント】\nコンボスラッシュを解放するための必要ポイントです。",

                // === Tier 3: ブレードリフレクト / リポスト ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【攻撃力ボーナス（固定値）】\n" +
                "ブレードリフレクトの攻撃力を固定値で増加させます。\n" +
                "パリィ後に強力な反撃を発動します。\n" +
                "推奨: 20-40",

                ["Tier3_Riposte_DamageBonus"] =
                "【攻撃力ボーナス（固定値）】\n" +
                "ブレードリフレクトの攻撃力を固定値で増加させます。\n" +
                "パリィ後に強力な反撃を発動します。\n" +
                "推奨: 5-15",

                ["Tier3_Riposte_RequiredPoints"] =
                "【必要ポイント】\nリポストを解放するための必要ポイントです。",

                // === Tier 4: 攻守一体 ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【攻撃力ボーナス (%)】\n" +
                "攻撃と防御を同時に強化します。\n" +
                "バランスの取れた戦闘スタイルに有効です。\n" +
                "推奨: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【防御力ボーナス（固定値）】\n" +
                "攻守一体スタンスの防御力ボーナスです。\n" +
                "攻撃しながらも堅固な防御が可能です。\n" +
                "推奨: 15-30",

                ["Tier4_AllInOne_RequiredPoints"] =
                "【必要ポイント】\n攻守一体を解放するための必要ポイントです。",

                // === Tier 4: 真剣勝負 ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【攻撃速度ボーナス (%)】\n" +
                "一対一での攻撃速度ボーナスです。\n" +
                "素早い連打で敵を圧倒します。\n" +
                "推奨: 15-30%",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【攻撃速度ボーナス (%)】\n" +
                "一対一での攻撃速度ボーナスです。\n" +
                "素早い連打で敵を圧倒します。\n" +
                "推奨: 15-30%",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【必要ポイント】\n真剣勝負を解放するための必要ポイントです。",

                // === Tier 5: パリィチャージ（アクティブ H）===
                ["Sword_ParryCharge_BuffDuration"] =
                "【バフ持続時間（秒）】\n" +
                "パリィ成功後のバフ持続時間です。\n" +
                "この間、強化攻撃が可能です。\n" +
                "推奨: 5-10秒",

                ["Sword_ParryCharge_DamageBonus"] =
                "【チャージダメージボーナス (%)】\n" +
                "パリィ後のチャージ攻撃のダメージ増加量です。\n" +
                "完璧なタイミングで強力な反撃を放ちます。\n" +
                "推奨: 50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【ノックバック距離（メートル）】\n" +
                "チャージ時に敵を吹き飛ばす距離です。\n" +
                "距離調整と戦場制御に使用します。\n" +
                "推奨: 3-7メートル",

                ["Sword_ParryCharge_StaminaCost"] =
                "【スタミナ消費】\n" +
                "バフ発動（Hキー）時に消費するスタミナです。\n" +
                "スタミナ管理が重要です。\n" +
                "推奨: 20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "小さいほど頻繁に使用できます。\n" +
                "推奨: 10-20秒",

                ["Tier5_ParryRush_StaminaCost"] =
                "【スタミナ消費】\n" +
                "スキル発動時に消費するスタミナです。\n" +
                "推奨: 10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 30-60秒",

                ["Tier5_ParryRush_RequiredPoints"] =
                "【必要ポイント】\nパリィラッシュを解放するための必要ポイントです。",

                // === Tier 6: ラッシュスラッシュ（アクティブ G）===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【第1撃ダメージ (%)】\n" +
                "ラッシュスラッシュ第1撃のダメージです。\n" +
                "基礎攻撃力に対する倍率です。\n" +
                "推奨: 80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【第2撃ダメージ (%)】\n" +
                "ラッシュスラッシュ第2撃のダメージです。\n" +
                "コンボが進むにつれダメージが上昇します。\n" +
                "推奨: 100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【第3撃ダメージ (%)】\n" +
                "ラッシュスラッシュのフィニッシュ撃です。\n" +
                "最も強力な最終一撃です。\n" +
                "推奨: 150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【ダッシュ距離（メートル）】\n" +
                "スキル開始時のダッシュ距離です。\n" +
                "素早く敵に接近します。\n" +
                "推奨: 5-10メートル",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【横移動距離（メートル）】\n" +
                "攻撃中の左右移動距離です。\n" +
                "回避しながら攻撃できます。\n" +
                "推奨: 2-5メートル",

                ["Sword_RushSlash_StaminaCost"] =
                "【スタミナ消費】\n" +
                "スキル使用（Gキー）時に消費するスタミナです。\n" +
                "強力なスキルには大量のスタミナが必要です。\n" +
                "推奨: 40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "小さいほど頻繁に使用できます。\n" +
                "推奨: 15-30秒",

                ["Sword_RushSlash_MovementSpeed"] =
                "【移動速度（メートル/秒）】\n" +
                "ダッシュ中の移動速度です。\n" +
                "速いほど動的な戦闘になります。\n" +
                "推奨: 8-15メートル/秒",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【攻撃速度ボーナス (%)】\n" +
                "スキル中の攻撃速度ボーナスです。\n" +
                "他ツリーの速度は無視され、この値のみが適用されます。\n" +
                "推奨: 20-40%",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【第1撃ダメージ (%)】\n" +
                "ラッシュスラッシュ第1撃のダメージです。\n" +
                "推奨: 60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【第2撃ダメージ (%)】\n" +
                "ラッシュスラッシュ第2撃のダメージです。\n" +
                "推奨: 70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【第3撃ダメージ (%)】\n" +
                "ラッシュスラッシュのフィニッシュ撃です。\n" +
                "推奨: 80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【初期ダッシュ距離（メートル）】\n" +
                "スキル開始時のダッシュ距離です。\n" +
                "推奨: 3-8メートル",

                ["Tier6_RushSlash_SideDistance"] =
                "【横移動距離（メートル）】\n" +
                "攻撃中の左右移動距離です。\n" +
                "推奨: 2-5メートル",

                ["Tier6_RushSlash_StaminaCost"] =
                "【スタミナ消費】\n" +
                "スキル使用（Gキー）時に消費するスタミナです。\n" +
                "推奨: 20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 15-30秒",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【移動速度（メートル/秒）】\n" +
                "ダッシュ中の移動速度です。\n" +
                "推奨: 10-25メートル/秒",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【攻撃速度ボーナス (%)】\n" +
                "スキル中の攻撃速度ボーナスです。\n" +
                "推奨: 150-250%",

                ["Tier6_RushSlash_RequiredPoints"] =
                "【必要ポイント】\nラッシュスラッシュを解放するための必要ポイントです。",

                // ========================================
                // ナイフツリー (Knife Tree)
                // ========================================

                // === Tier 0: ナイフ専家 ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【背後攻撃ダメージボーナス (%)】\n" +
                "背後から攻撃した時の追加ダメージです。\n" +
                "アサシンの基本能力です。\n" +
                "推奨: 30-50%",

                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【必要ポイント】\nナイフ専家を解放するための必要ポイントです。",

                // === Tier 1: 回避精通 ===
                ["Tier1_Evasion_Chance"] =
                "【回避率 (%)】\n" +
                "敵の攻撃を回避する確率です。\n" +
                "高いほど受けるダメージが少なくなります。\n" +
                "推奨: 15-25%",

                ["Tier1_Evasion_Duration"] =
                "【無敵持続時間（秒）】\n" +
                "回避成功後の無敵時間です。\n" +
                "推奨: 2-4秒",

                ["Tier1_Evasion_RequiredPoints"] =
                "【必要ポイント】\n回避精通を解放するための必要ポイントです。",

                // === Tier 2: 素早い動き ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【移動速度ボーナス (%)】\n" +
                "基本移動速度を増加させます。\n" +
                "高い機動性で敵を惑わします。\n" +
                "推奨: 10-20%",

                ["Tier2_FastMove_RequiredPoints"] =
                "【必要ポイント】\n素早い動きを解放するための必要ポイントです。",

                // === Tier 3: 戦闘精通 ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【ダメージボーナス（固定値）】\n" +
                "攻撃ごとに固定ダメージを追加します。\n" +
                "推奨: 1-4",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【バフ持続時間（秒）】\n" +
                "戦闘精通バフの持続時間です。\n" +
                "推奨: 8-12秒",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【必要ポイント】\n戦闘精通を解放するための必要ポイントです。",

                // === Tier 4: アタック＆エバジョン ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【回避率ボーナス (%)】\n" +
                "攻撃中の回避率を増加させます。\n" +
                "積極的な防御スタイルです。\n" +
                "推奨: 20-30%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【バフ持続時間（秒）】\n" +
                "回避効果強化バフの持続時間です。\n" +
                "推奨: 8-12秒",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "バフ再発動までの待機時間です。\n" +
                "推奨: 25-35秒",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【必要ポイント】\nアタック＆エバジョンを解放するための必要ポイントです。",

                // === Tier 5: クリティカルダメージ ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【ダメージボーナス (%)】\n" +
                "クリティカル時のダメージを増加させます。\n" +
                "ナイフの高いクリティカル率と相性が良いです。\n" +
                "推奨: 20-35%",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【必要ポイント】\nクリティカルダメージを解放するための必要ポイントです。",

                // === Tier 6: アサシン ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【クリティカルダメージボーナス (%)】\n" +
                "クリティカル時のダメージをさらに増加させます。\n" +
                "推奨: 20-30%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【クリティカル率ボーナス (%)】\n" +
                "クリティカルの発動確率を増加させます。\n" +
                "推奨: 10-18%",

                ["Tier6_Assassin_RequiredPoints"] =
                "【必要ポイント】\nアサシンを解放するための必要ポイントです。",

                // === Tier 7: 暗殺 ===
                ["Tier7_Assassination_StaggerChance"] =
                "【スタッガー確率 (%)】\n" +
                "コンボ中に敵をスタッガーさせる確率です。\n" +
                "敵の攻撃を中断させます。\n" +
                "推奨: 30-45%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【必要コンボヒット数】\n" +
                "スタッガー発動に必要な連続命中回数です。\n" +
                "推奨: 2-4回",

                ["Tier7_Assassination_RequiredPoints"] =
                "【必要ポイント】\n暗殺を解放するための必要ポイントです。",

                // === Tier 8: アサシンハート（アクティブ G）===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【クリティカルダメージ倍率】\n" +
                "アクティブG — アサシンハートのクリティカルダメージ倍率です。\n" +
                "敵の背後に瞬間移動して致命的なコンボを発動します。\n" +
                "推奨: 1.2-1.5倍",

                ["Tier8_AssassinHeart_Duration"] =
                "【バフ持続時間（秒）】\n" +
                "アサシンハートバフの持続時間です。\n" +
                "推奨: 5-10秒",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【スタミナ消費】\n" +
                "スキル使用時に消費するスタミナです。\n" +
                "推奨: 15-25",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "再使用までの待機時間です。\n" +
                "推奨: 35-50秒",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【テレポート範囲（メートル）】\n" +
                "敵を探索する最大範囲です。\n" +
                "この半径内の敵の背後にテレポートします。\n" +
                "推奨: 6-10メートル",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【敵背後配置距離（メートル）】\n" +
                "敵の背後に配置される距離です。\n" +
                "背後攻撃の配置距離です。\n" +
                "推奨: 0.8-1.5メートル",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【スタン持続時間（秒）】\n" +
                "敵がスタン状態になる時間です。\n" +
                "推奨: 0.5-2秒",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【コンボ攻撃回数】\n" +
                "テレポート後に自動で命中する回数です。\n" +
                "推奨: 2-4回",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【命中間隔（秒）】\n" +
                "コンボ各命中間の時間間隔です。\n" +
                "小さいほど瞬時に命中します。\n" +
                "推奨: 0.2-0.5秒",

                ["Tier8_AssassinHeart_AttackSpeedBonus"] =
                "【攻撃速度ボーナス（%）】\n" +
                "アサシンハート発動中の攻撃速度増加量です。\n" +
                "推奨：400-600%",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【必要ポイント】\nアサシンハートを解放するための必要ポイントです。",

                ["Tier9_StackExplosion_DamagePercent"] =
                "【スタックあたり炎ダメージ（%）】\n" +
                "Hキー爆発時のスタック1個あたりの武器攻撃力倍率です。\n" +
                "推奨：30-45%",

                ["Tier9_StackExplosion_MaxStacks"] =
                "【最大スタック数】\n" +
                "蓄積できる最大スタック数です。\n" +
                "推奨：5-10",

                ["Tier9_StackExplosion_StackDuration"] =
                "【スタック持続時間（秒）】\n" +
                "最後の命中後にスタックが持続する時間です。\n" +
                "推奨：3-6秒",

                ["Tier9_StackExplosion_StaminaCost"] =
                "【スタミナ消費】\n" +
                "Hキー爆発発動時に消費するスタミナです。\n" +
                "推奨：10-20",

                ["Tier9_StackExplosion_Cooldown"] =
                "【クールダウン（秒）】\n" +
                "スキル再使用待機時間です。\n" +
                "推奨：40-55秒",

                ["Tier9_StackExplosion_RequiredPoints"] =
                "【必要ポイント】\n" +
                "このスキルノードを解放するための必要ポイント数です。\n" +
                "推奨：3",

                ["Tier9_StackExplosion_BuffDuration"] =
                "【バフ持続時間（秒）】\n" +
                "Hキー後にスタックを蓄積できる時間です。\n" +
                "推奨：10-15秒",

                ["Tier9_StackExplosion_DamageLevelBonus"] =
                "【レベルあたりダメージボーナス（%）】\n" +
                "スタック爆発の各レベルごとに追加されるスタックあたりのダメージです。\n" +
                "推奨：3-8%",

                ["Tier9_StackExplosion_AoePercent"] =
                "【範囲ダメージ率（%）】\n" +
                "Hキー爆発時に7m以内の敵への範囲ダメージ割合です。\n" +
                "合計ダメージ × (値 / 100) = 範囲ダメージ\n" +
                "推奨: 30-50%",

                ["Tier5_WhirlwindSlash_BaseDamage"] =
                "【基本ダメージ (%)】\n" +
                "旋風斬りスキルの基本ダメージ。\n" +
                "推奨：80-150%",

                ["Tier5_WhirlwindSlash_LevelBonus"] =
                "【レベルボーナス (%)】\n" +
                "旋風斬りスキルのレベル毎ダメージボーナス。\n" +
                "推奨：10-25%",

                ["Tier6_RushSlash_DamageLevelBonus"] =
                "【ダメージレベルボーナス (%)】\n" +
                "ラッシュスラッシュスキルのレベル毎ダメージボーナス。\n" +
                "推奨：5-15%",

                ["Tier6_RushSlash_PathWidth"] =
                "【ラッシュスラッシュ】移動経路ヒット幅（m）。\n" +
                "経路上のこの範囲内の全ての敵にヒットします。\n" +
                "推奨：1-3m",

                ["Tier8_AssassinHeart_LevelBonus"] =
                "【レベルボーナス (%)】\n" +
                "アサシンハートスキルのレベル毎ダメージボーナス。\n" +
                "推奨：10-30%",

            };
        }
    }
}

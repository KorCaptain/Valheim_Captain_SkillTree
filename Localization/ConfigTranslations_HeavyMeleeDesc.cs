using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetHeavyMeleeDescriptions_KO()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Spear Tree (Tier 0~5, 35 keys)
                // ========================================








                // === Spear Tree: Tier 0 - 창 전문가 (3개) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【2연타 후 공격속도 보너스 (%)】\n" +
                "창으로 2회 연속 공격 후 공격속도가 증가합니다.\n" +
                "빠른 연속 공격으로 적을 압도합니다.\n" +
                "권장값: 10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【2연타 후 공격력 보너스 (%)】\n" +
                "창으로 2회 연속 공격 후 공격력이 증가합니다.\n" +
                "콤보 공격의 피해를 극대화합니다.\n" +
                "권장값: 7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【버프 지속시간 (초)】\n" +
                "2연타 효과가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 전투가 가능합니다.\n" +
                "권장값: 4-8초",

                // === Spear Tree: Tier 1 - 급소 찌르기 (1개) ===
                ["Tier1_VitalStrike_DamageBonus"] =
                "【치명타 피해 보너스 (%)】\n" +
                "창 공격의 치명타 피해를 증가시킵니다.\n" +
                "급소를 노리는 정밀 공격에 특화됩니다.\n" +
                "권장값: 20-40%",

                // === Spear Tree: Tier 2 - 투창 (3개) ===
                ["Tier2_Throw_Cooldown"] =
                "【투창 쿨타임 (초)】\n" +
                "투창 스킬 재사용 대기시간입니다.\n" +
                "짧을수록 자주 투창 가능합니다.\n" +
                "권장값: 20-40초",

                ["Tier2_Throw_DamageMultiplier"] =
                "【투창 데미지 배율 (%)】\n" +
                "투창 시 가하는 피해의 배율입니다.\n" +
                "원거리 공격 위력을 결정합니다.\n" +
                "권장값: 100-150%",

                ["Tier2_Throw_BuffDuration_NotUsed"] =
                "【사용 안 함】\n" +
                "이 설정은 현재 사용되지 않습니다.\n" +
                "패시브 스킬로 변경되었습니다.",

                // === Spear Tree: Tier 3 - 연격창 (1개) ===
                ["Tier3_Rapid_DamageBonus"] =
                "【무기 공격력 보너스 (고정값)】\n" +
                "창의 기본 공격력을 고정값으로 증가시킵니다.\n" +
                "빠른 연속 공격에 유리합니다.\n" +
                "권장값: 3-6",

                // === Spear Tree: Tier 3 - 폭발창 (3개) ===
                ["Tier3_Explosion_Chance"] =
                "【폭발 발동 확률 (%)】\n" +
                "창 공격 시 폭발 효과가 발동할 확률입니다.\n" +
                "광역 피해를 가할 수 있습니다.\n" +
                "권장값: 20-40%",

                ["Tier3_Explosion_Radius"] =
                "【폭발 범위 (m)】\n" +
                "폭발 피해가 미치는 반경입니다.\n" +
                "넓을수록 다수의 적에게 피해를 줍니다.\n" +
                "권장값: 4-7m",

                ["Tier3_Explosion_DamageBonus"] =
                "【폭발 피해 보너스 (%)】\n" +
                "폭발 시 추가되는 피해량입니다.\n" +
                "광역 딜링의 핵심입니다.\n" +
                "권장값: 25-40%",

                // === Spear Tree: Tier 4 - 회피 찌르기 (2개) ===
                ["Tier4_Evasion_DamageBonus"] =
                "【회피 후 공격력 보너스 (%)】\n" +
                "구르기(회피) 직후 창 공격의 피해가 증가합니다.\n" +
                "기동성과 공격력을 동시에 강화합니다.\n" +
                "권장값: 20-35%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【스태미나 소모 감소 (%)】\n" +
                "회피 후 공격 시 스태미나 소모가 감소합니다.\n" +
                "지속적인 전투가 가능합니다.\n" +
                "권장값: 5-12%",

                // === Spear Tree: Tier 4 - 이연창 (2개) ===
                ["Tier4_Dual_DamageBonus"] =
                "【2연타 공격력 보너스 (%)】\n" +
                "2회 연속 공격 시 추가 피해를 가합니다.\n" +
                "콤보 딜링에 특화됩니다.\n" +
                "권장값: 18-30%",

                ["Tier4_Dual_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "이연창 버프가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 딜링이 가능합니다.\n" +
                "권장값: 8-15초",

                // === Spear Tree: Tier 5 - 꿰뚫는 창 (G키 액티브, 6개) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] =
                "【사용 안 함】\n" +
                "이 설정은 현재 사용되지 않습니다.\n" +
                "번개 충격 효과로 변경되었습니다.",

                ["Tier5_Penetrate_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "꿰뚫는 창 버프가 유지되는 시간입니다.\n" +
                "스킬 효과 지속 시간을 결정합니다.\n" +
                "권장값: 25-40초",

                ["Tier5_Penetrate_LightningDamage"] =
                "【번개 충격 피해 배율 (%)】\n" +
                "연속 공격 시 발동되는 번개 피해의 배율입니다.\n" +
                "강력한 추가 피해를 가합니다.\n" +
                "권장값: 200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【번개 발동 필요 연타 횟수】\n" +
                "번개 충격이 발동되기 위해 필요한 연속 공격 횟수입니다.\n" +
                "적을수록 자주 발동됩니다.\n" +
                "권장값: 3-5회",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【G키 스킬 쿨타임 (초)】\n" +
                "꿰뚫는 창(G키) 재사용 대기시간입니다.\n" +
                "짧을수록 자주 사용 가능합니다.\n" +
                "권장값: 50-80초",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【G키 스킬 스태미나 소모】\n" +
                "꿰뚫는 창(G키) 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 20-35",

                // === Spear Tree: Tier 5 - 연공창 (H키 액티브, 7개) ===
                ["Tier5_Combo_HKey_Cooldown"] =
                "【H키 스킬 쿨타임 (초)】\n" +
                "연공창(H키) 재사용 대기시간입니다.\n" +
                "짧을수록 자주 사용 가능합니다.\n" +
                "권장값: 20-35초",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【H키 스킬 피해 배율 (%)】\n" +
                "연공창(H키) 공격의 피해 배율입니다.\n" +
                "강력한 단발 딜링 스킬입니다.\n" +
                "권장값: 250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【H키 스킬 스태미나 소모】\n" +
                "연공창(H키) 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 필요합니다.\n" +
                "권장값: 15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【H키 스킬 넉백 범위 (m)】\n" +
                "연공창(H키) 적중 시 적을 밀쳐내는 거리입니다.\n" +
                "전투 위치 조절에 유용합니다.\n" +
                "권장값: 2-5m",

                ["Tier5_Combo_ActiveRange"] =
                "【액티브 효과 범위 (m)】\n" +
                "연공창 버프가 활성화되는 범위입니다.\n" +
                "넓을수록 더 많은 상황에서 효과 발동됩니다.\n" +
                "권장값: 2-5m",

                ["Tier5_Combo_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "연공창 버프가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 강화 투창이 가능합니다.\n" +
                "권장값: 25-40초",

                ["Tier5_Combo_MaxUses"] =
                "【최대 강화 투창 횟수】\n" +
                "버프 중 강화된 투창을 사용할 수 있는 최대 횟수입니다.\n" +
                "많을수록 더 오래 강화 효과를 누릴 수 있습니다.\n" +
                "권장값: 2-5회",

                // ========================================
                // Mace Tree (티어별 정렬)
                // ========================================

                // === Tier 0: 둔기 전문가 (Mace Expert) ===

                ["Tier0_MaceExpert_DamageBonus"] =
                "【둔기 피해 보너스 (%)】\n" +
                "둔기 무기의 기본 공격력을 증가시킵니다.\n" +
                "모든 둔기류(클럽, 메이스 등)에 적용됩니다.\n" +
                "권장값: 5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【기절 확률 (%)】\n" +
                "둔기 공격 시 적을 기절시킬 확률입니다.\n" +
                "기절 상태의 적은 행동 불능이 됩니다.\n" +
                "권장값: 15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【기절 지속시간 (초)】\n" +
                "기절 효과가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안전한 딜 타이밍을 확보할 수 있습니다.\n" +
                "권장값: 0.3-1초",

                // === Tier 1: 둔기 공격력 강화 ===

                ["Tier1_MaceExpert_DamageBonus"] =
                "【둔기 공격력 보너스 (%)】\n" +
                "둔기 무기의 추가 공격력 보너스입니다.\n" +
                "권장값: 8-15%",

                // === Tier 2: 기절 강화 (Stun Boost) ===

                ["Tier2_StunBoost_StunChanceBonus"] =
                "【기절 확률 보너스 (%)】\n" +
                "기절 확률을 추가로 증가시킵니다.\n" +
                "둔기 전문가 스킬과 누적 적용됩니다.\n" +
                "권장값: 10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【기절 지속시간 보너스 (초)】\n" +
                "기절 효과의 지속시간을 추가로 증가시킵니다.\n" +
                "더 긴 딜 타이밍 확보가 가능합니다.\n" +
                "권장값: 0.3-0.8초",

                // === Tier 3: 방어 강화 (Guard) ===

                ["Tier3_Guard_ArmorBonus"] =
                "【방어력 보너스 (고정값)】\n" +
                "기본 방어력을 고정 수치로 증가시킵니다.\n" +
                "탱커 빌드에 유용합니다.\n" +
                "권장값: 2-5",

                // === Tier 3: 무거운 타격 (Heavy Strike) ===

                ["Tier3_HeavyStrike_DamageBonus"] =
                "【공격력 보너스 (고정값)】\n" +
                "둔기 공격력을 고정 수치로 증가시킵니다.\n" +
                "퍼센트 보너스와 함께 적용됩니다.\n" +
                "권장값: 2-5",

                // === Tier 4: 밀어내기 (Push) ===

                ["Tier4_Push_KnockbackChance"] =
                "【넉백 확률 (%)】\n" +
                "공격 시 적을 밀어내는 확률입니다.\n" +
                "거리 유지와 전장 제어에 유용합니다.\n" +
                "권장값: 25-35%",

                // === Tier 5: 탱커 (Tank) ===

                ["Tier5_Tank_HealthBonus"] =
                "【체력 보너스 (%)】\n" +
                "최대 체력을 증가시킵니다.\n" +
                "생존력 강화에 필수적입니다.\n" +
                "권장값: 20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【받는 데미지 감소 (%)】\n" +
                "모든 피해를 감소시킵니다.\n" +
                "방어력과 함께 적용되어 탱커 역할에 최적입니다.\n" +
                "권장값: 8-15%",

                // === Tier 5: 공격력 강화 (DPS) ===

                ["Tier5_DPS_DamageBonus"] =
                "【공격력 보너스 (%)】\n" +
                "둔기 무기의 공격력을 추가로 증가시킵니다.\n" +
                "DPS 빌드에 유용합니다.\n" +
                "권장값: 15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "둔기 공격속도를 증가시킵니다.\n" +
                "느린 둔기의 약점을 보완합니다.\n" +
                "권장값: 8-15%",

                // === Tier 6: 그랜드마스터 (Grandmaster) ===

                ["Tier6_Grandmaster_ArmorBonus"] =
                "【방어력 보너스 (%)】\n" +
                "퍼센트 기반 방어력 보너스입니다.\n" +
                "고급 방어구와 시너지가 좋습니다.\n" +
                "권장값: 15-25%",

                // === Tier 7: 분노의 망치 (Fury Hammer - H키 액티브) ===

                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【1~4타 데미지 배율 (%)】\n" +
                "H키 스킬 '분노의 망치' 1~4타 공격의 데미지 배율입니다.\n" +
                "현재 공격력 기준으로 적용됩니다.\n" +
                "권장값: 70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【5타(최종타) 데미지 배율 (%)】\n" +
                "H키 스킬 '분노의 망치' 최종 공격의 데미지 배율입니다.\n" +
                "현재 공격력 기준으로 적용됩니다.\n" +
                "가장 강력한 피니시 공격입니다.\n" +
                "권장값: 130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【스태미나 소모】\n" +
                "H키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 25-35초",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【AOE 범위 (미터)】\n" +
                "스킬의 광역 피해 범위입니다.\n" +
                "넓을수록 더 많은 적을 공격할 수 있습니다.\n" +
                "권장값: 4-7m",

                // === Tier 7: 수호자의 진심 (Guardian Heart - G키 액티브) ===
                ["Tier7_GuardianHeart_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "G키 스킬 '수호자의 진심' 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 방어 자세를 취할 수 있습니다.\n" +
                "권장값: 100-140초",

                ["Tier7_GuardianHeart_StaminaCost"] =
                "【스태미나 소모】\n" +
                "스킬 사용 시 소모되는 스태미나입니다.\n" +
                "탱커 역할 시 스태미나 관리가 중요합니다.\n" +
                "권장값: 20-30",

                ["Tier7_GuardianHeart_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "방어 자세의 지속 시간입니다.\n" +
                "이 시간 동안 피해를 반사하며 높은 방어력을 유지합니다.\n" +
                "권장값: 40-50초",

                ["Tier7_GuardianHeart_ReflectPercent"] =
                "【반사 데미지 비율 (%)】\n" +
                "받은 피해를 공격자에게 반사하는 비율입니다.\n" +
                "탱커로서 적에게 피해를 돌려줄 수 있습니다.\n" +
                "권장값: 5-8%",


                // ========================================
                // Polearm Tree (Tier 0~6, 25 keys)
                // ========================================








                // === Polearm Tree: Tier 0 - 폴암 전문가 (1개) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【공격 범위 보너스 (%)】\n" +
                "폴암(장창, 창날도끼 등)의 공격 범위를 증가시킵니다.\n" +
                "긴 리치로 안전 거리를 유지하며 공격할 수 있습니다.\n" +
                "권장값: 10-20%",

                // === Polearm Tree: Tier 1 - 회전베기 (1개) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【회전 공격 피해 보너스 (%)】\n" +
                "회전 공격 시 추가 피해를 가합니다.\n" +
                "다수의 적을 상대할 때 유용합니다.\n" +
                "권장값: 50-80%",

                // === Polearm Tree: Tier 2 - 영웅 타격 (1개) ===
                ["Tier2_HeroStrike_KnockbackChance"] =
                "【넉백 확률 (%)】\n" +
                "공격 시 적을 밀쳐낼 확률입니다.\n" +
                "전장 제어에 유용합니다.\n" +
                "권장값: 20-35%",

                // === Polearm Tree: Tier 3 - 광역 강타 (2개) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【2연타 피해 보너스 (%)】\n" +
                "2회 연속 공격 시 추가 피해를 가합니다.\n" +
                "광역 콤보 딜링에 특화됩니다.\n" +
                "권장값: 20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【2연타 버프 지속시간 (초)】\n" +
                "2연타 버프가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 콤보가 가능합니다.\n" +
                "권장값: 4-8초",

                // === Polearm Tree: Tier 3 - 지면 강타 (1개) ===
                ["Tier3_GroundWheel_WheelAttackDamageBonus"] =
                "【회전 공격 피해 보너스 (%)】\n" +
                "지면을 강타하는 회전 공격의 피해를 증가시킵니다.\n" +
                "광역 딜링 핵심 스킬입니다.\n" +
                "권장값: 70-100%",

                // === Polearm Tree: Tier 3 - 폴암 강화 (1개) ===
                ["Tier3_PolearmBoost_WeaponDamageBonus"] =
                "【무기 공격력 보너스 (고정값)】\n" +
                "폴암의 기본 공격력을 고정값으로 증가시킵니다.\n" +
                "모든 폴암 공격에 적용됩니다.\n" +
                "권장값: 4-7",

                // === Polearm Tree: Tier 4 - 반달 베기 (2개) ===
                ["Tier4_MoonSlash_AttackRangeBonus"] =
                "【공격 범위 보너스 (%)】\n" +
                "반달 베기의 공격 범위를 증가시킵니다.\n" +
                "더 넓은 범위의 적을 공격할 수 있습니다.\n" +
                "권장값: 12-20%",

                ["Tier4_MoonSlash_StaminaReduction"] =
                "【스태미나 소모 감소 (%)】\n" +
                "반달 베기 사용 시 스태미나 소모가 감소합니다.\n" +
                "지속적인 전투가 가능합니다.\n" +
                "권장값: 12-20%",

                // === Polearm Tree: Tier 5 - 제압 공격 (1개) ===
                ["Tier5_Suppress_DamageBonus"] =
                "【제압 공격 피해 보너스 (%)】\n" +
                "제압 공격 시 추가 피해를 가합니다.\n" +
                "적을 억압하며 전투 주도권을 장악합니다.\n" +
                "권장값: 25-40%",

                // === Polearm Tree: Tier 6 - 관통 돌격 (G키 액티브, 8개) ===
                ["Tier6_PierceCharge_DashDistance"] =
                "【돌진 거리 (m)】\n" +
                "관통 돌격 시 돌진하는 거리입니다.\n" +
                "긴 거리로 적진을 뚫고 들어갑니다.\n" +
                "권장값: 8-12m",

                ["Tier6_PierceCharge_FirstHitDamageBonus"] =
                "【첫 타격 피해 보너스 (%)】\n" +
                "돌격 중 첫 타격의 피해 배율입니다.\n" +
                "강력한 첫 타격으로 적을 제압합니다.\n" +
                "권장값: 180-250%",

                ["Tier6_PierceCharge_AoeDamageBonus"] =
                "【광역 넉백 피해 보너스 (%)】\n" +
                "돌격 종료 후 광역 넉백 공격의 피해 배율입니다.\n" +
                "주변 적들을 밀쳐내며 피해를 가합니다.\n" +
                "권장값: 130-180%",

                ["Tier6_PierceCharge_AoeAngle"] =
                "【광역 각도 (도)】\n" +
                "광역 넉백 효과의 각도입니다.\n" +
                "280도 = 전방 80도 제외한 후방/측면 범위입니다.\n" +
                "권장값: 250-300도",

                ["Tier6_PierceCharge_AoeRadius"] =
                "【광역 반경 (m)】\n" +
                "광역 넉백 효과의 반경입니다.\n" +
                "넓을수록 더 많은 적을 밀쳐냅니다.\n" +
                "권장값: 4-7m",

                ["Tier6_PierceCharge_KnockbackDistance"] =
                "【넉백 거리 (m)】\n" +
                "적을 밀쳐내는 거리입니다.\n" +
                "전장 제어에 유용합니다.\n" +
                "권장값: 6-10m",

                ["Tier6_PierceCharge_StaminaCost"] =
                "【스태미나 소모】\n" +
                "G키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 18-25",

                ["Tier6_PierceCharge_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "G키 스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 25-40초",

            };
        }

        private static Dictionary<string, string> GetHeavyMeleeDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Spear Tree (Tier 0~5, 35 keys)
                // ========================================








                // === Spear Tree: Tier 0 - Spear Expert (3 keys) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【Attack Speed Bonus After 2-Hit (%)】\n" +
                "Attack speed increases after 2 consecutive spear hits.\n" +
                "Overwhelm enemies with rapid combos.\n" +
                "Recommended: 10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【Damage Bonus After 2-Hit (%)】\n" +
                "Damage increases after 2 consecutive spear hits.\n" +
                "Maximizes combo attack damage.\n" +
                "Recommended: 7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of 2-hit combo effect.\n" +
                "Longer duration enables stable combat.\n" +
                "Recommended: 4-8 sec",

                // === Spear Tree: Tier 1 - Vital Strike (1 key) ===
                ["Tier1_VitalStrike_DamageBonus"] =
                "【Critical Damage Bonus (%)】\n" +
                "Increases spear critical hit damage.\n" +
                "Specialized for precision vital strikes.\n" +
                "Recommended: 20-40%",

                // === Spear Tree: Tier 2 - Throw (3 keys) ===
                ["Tier2_Throw_Cooldown"] =
                "【Throw Cooldown (sec)】\n" +
                "Skill reactivation wait time for spear throw.\n" +
                "Shorter allows more frequent throws.\n" +
                "Recommended: 20-40 sec",

                ["Tier2_Throw_DamageMultiplier"] =
                "【Throw Damage Multiplier (%)】\n" +
                "Damage multiplier for thrown spears.\n" +
                "Determines ranged attack power.\n" +
                "Recommended: 100-150%",

                ["Tier2_Throw_BuffDuration_NotUsed"] =
                "【Not Used】\n" +
                "This setting is currently unused.\n" +
                "Changed to passive skill.",

                // === Spear Tree: Tier 3 - Rapid Pierce (1 key) ===
                ["Tier3_Rapid_DamageBonus"] =
                "【Weapon Damage Bonus (Flat)】\n" +
                "Flat damage increase to base spear damage.\n" +
                "Favors rapid consecutive attacks.\n" +
                "Recommended: 3-6",

                // === Spear Tree: Tier 3 - Explosive Spear (3 keys) ===
                ["Tier3_Explosion_Chance"] =
                "【Explosion Trigger Chance (%)】\n" +
                "Probability of explosion effect on spear hit.\n" +
                "Deals area damage.\n" +
                "Recommended: 20-40%",

                ["Tier3_Explosion_Radius"] =
                "【Explosion Radius (m)】\n" +
                "Radius of explosion damage.\n" +
                "Larger radius damages multiple enemies.\n" +
                "Recommended: 4-7 m",

                ["Tier3_Explosion_DamageBonus"] =
                "【Explosion Damage Bonus (%)】\n" +
                "Additional damage from explosion.\n" +
                "Core of area-of-effect damage.\n" +
                "Recommended: 25-40%",

                // === Spear Tree: Tier 4 - Evasion Strike (2 keys) ===
                ["Tier4_Evasion_DamageBonus"] =
                "【Damage Bonus After Dodge (%)】\n" +
                "Spear damage increases immediately after dodging.\n" +
                "Enhances both mobility and damage.\n" +
                "Recommended: 20-35%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【Stamina Cost Reduction (%)】\n" +
                "Reduces stamina cost for attacks after dodge.\n" +
                "Enables sustained combat.\n" +
                "Recommended: 5-12%",

                // === Spear Tree: Tier 4 - Dual Spear (2 keys) ===
                ["Tier4_Dual_DamageBonus"] =
                "【2-Hit Damage Bonus (%)】\n" +
                "Additional damage on 2 consecutive hits.\n" +
                "Specialized for combo damage.\n" +
                "Recommended: 18-30%",

                ["Tier4_Dual_Duration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of dual spear buff.\n" +
                "Longer duration for stable damage output.\n" +
                "Recommended: 8-15 sec",

                // === Spear Tree: Tier 5 - Penetrating Spear (G-Key Active, 6 keys) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] =
                "【Not Used】\n" +
                "This setting is currently unused.\n" +
                "Changed to lightning strike effect.",

                ["Tier5_Penetrate_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of penetrating spear buff.\n" +
                "Determines skill effect duration.\n" +
                "Recommended: 25-40 sec",

                ["Tier5_Penetrate_LightningDamage"] =
                "【Lightning Strike Damage Multiplier (%)】\n" +
                "Lightning damage multiplier on combo trigger.\n" +
                "Deals powerful additional damage.\n" +
                "Recommended: 200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【Lightning Trigger Combo Hits】\n" +
                "Consecutive hits required to trigger lightning.\n" +
                "Lower values trigger more frequently.\n" +
                "Recommended: 3-5 hits",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【G-Key Skill Cooldown (sec)】\n" +
                "Reactivation time for Penetrating Spear (G-key).\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 50-80 sec",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【G-Key Skill Stamina Cost】\n" +
                "Stamina consumed when using Penetrating Spear.\n" +
                "Stamina management is critical.\n" +
                "Recommended: 20-35",

                // === Spear Tree: Tier 5 - Combo Spear (H-Key Active, 7 keys) ===
                ["Tier5_Combo_HKey_Cooldown"] =
                "【H-Key Skill Cooldown (sec)】\n" +
                "Reactivation time for Combo Spear (H-key).\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 20-35 sec",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【H-Key Skill Damage Multiplier (%)】\n" +
                "Damage multiplier for Combo Spear (H-key).\n" +
                "Powerful burst damage skill.\n" +
                "Recommended: 250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【H-Key Skill Stamina Cost】\n" +
                "Stamina consumed when using Combo Spear.\n" +
                "Stamina management required.\n" +
                "Recommended: 15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【H-Key Skill Knockback Range (m)】\n" +
                "Distance enemies are pushed back on hit.\n" +
                "Useful for combat positioning.\n" +
                "Recommended: 2-5 m",

                ["Tier5_Combo_ActiveRange"] =
                "【Active Effect Range (m)】\n" +
                "Range where combo spear buff activates.\n" +
                "Larger range triggers in more situations.\n" +
                "Recommended: 2-5 m",

                ["Tier5_Combo_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of combo spear buff.\n" +
                "Longer duration for stable enhanced throws.\n" +
                "Recommended: 25-40 sec",

                ["Tier5_Combo_MaxUses"] =
                "【Max Enhanced Throws】\n" +
                "Maximum enhanced throws available during buff.\n" +
                "More uses extend enhanced effect duration.\n" +
                "Recommended: 2-5 uses",

                // Mace Tree (Tier-based Sorting)
                // ========================================

                // === Tier 0: Mace Expert ===

                ["Tier0_MaceExpert_DamageBonus"] =
                "【Mace Damage Bonus (%)】\n" +
                "Increases base damage of mace weapons.\n" +
                "Applies to all blunt weapons (clubs, maces, etc.).\n" +
                "Recommended: 5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【Stun Chance (%)】\n" +
                "Chance to stun enemies on mace attacks.\n" +
                "Stunned enemies cannot act.\n" +
                "Recommended: 15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【Stun Duration (sec)】\n" +
                "Duration of the stun effect.\n" +
                "Longer duration provides safe damage windows.\n" +
                "Recommended: 0.3-1 sec",

                // === Tier 1: Mace Damage Enhancement ===

                ["Tier1_MaceExpert_DamageBonus"] =
                "【Mace Damage Bonus (%)】\n" +
                "Additional damage bonus for mace weapons.\n" +
                "Recommended: 8-15%",

                // === Tier 2: Stun Boost ===

                ["Tier2_StunBoost_StunChanceBonus"] =
                "【Stun Chance Bonus (%)】\n" +
                "Additional increase to stun chance.\n" +
                "Stacks with Mace Expert skill.\n" +
                "Recommended: 10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【Stun Duration Bonus (sec)】\n" +
                "Additional increase to stun duration.\n" +
                "Provides even longer damage windows.\n" +
                "Recommended: 0.3-0.8 sec",

                // === Tier 3: Guard ===

                ["Tier3_Guard_ArmorBonus"] =
                "【Armor Bonus (Fixed Value)】\n" +
                "Increases base armor by a fixed amount.\n" +
                "Useful for tank builds.\n" +
                "Recommended: 2-5",

                // === Tier 3: Heavy Strike ===

                ["Tier3_HeavyStrike_DamageBonus"] =
                "【Damage Bonus (Fixed Value)】\n" +
                "Increases mace damage by a fixed amount.\n" +
                "Applies alongside percentage bonuses.\n" +
                "Recommended: 2-5",

                // === Tier 4: Push ===

                ["Tier4_Push_KnockbackChance"] =
                "【Knockback Chance (%)】\n" +
                "Chance to knock back enemies on attack.\n" +
                "Useful for distance control and battlefield management.\n" +
                "Recommended: 25-35%",

                // === Tier 5: Tank ===

                ["Tier5_Tank_HealthBonus"] =
                "【Health Bonus (%)】\n" +
                "Increases maximum health.\n" +
                "Essential for survivability.\n" +
                "Recommended: 20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【Damage Reduction (%)】\n" +
                "Reduces all incoming damage.\n" +
                "Works with armor for optimal tanking.\n" +
                "Recommended: 8-15%",

                // === Tier 5: DPS ===

                ["Tier5_DPS_DamageBonus"] =
                "【Damage Bonus (%)】\n" +
                "Additional damage increase for mace weapons.\n" +
                "Useful for DPS builds.\n" +
                "Recommended: 15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases mace attack speed.\n" +
                "Compensates for slow mace attacks.\n" +
                "Recommended: 8-15%",

                // === Tier 6: Grandmaster ===

                ["Tier6_Grandmaster_ArmorBonus"] =
                "【Armor Bonus (%)】\n" +
                "Percentage-based armor bonus.\n" +
                "Great synergy with high-tier armor.\n" +
                "Recommended: 15-25%",

                // === Tier 7: Fury Hammer (H-Key Active) ===

                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【Hits 1-4 Damage Multiplier (%)】\n" +
                "Damage multiplier for hits 1-4 of H-key skill 'Fury Hammer'.\n" +
                "Based on current attack power.\n" +
                "Recommended: 70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【Hit 5 (Final) Damage Multiplier (%)】\n" +
                "Damage multiplier for the final hit of H-key skill 'Fury Hammer'.\n" +
                "Based on current attack power.\n" +
                "The most powerful finishing blow.\n" +
                "Recommended: 130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on H-key skill use.\n" +
                "Stamina management is important.\n" +
                "Recommended: 35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse delay.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 25-35 sec",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【AOE Radius (meters)】\n" +
                "Area damage radius of the skill.\n" +
                "Larger radius hits more enemies.\n" +
                "Recommended: 4-7m",

                // === Tier 7: Guardian Heart (G-Key Active) ===
                ["Tier7_GuardianHeart_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Reuse delay for G-key skill 'Guardian Heart'.\n" +
                "Shorter cooldown allows more frequent defensive stance.\n" +
                "Recommended: 100-140 sec",

                ["Tier7_GuardianHeart_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on skill use.\n" +
                "Stamina management is crucial for tanking.\n" +
                "Recommended: 20-30",

                ["Tier7_GuardianHeart_Duration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of the defensive stance.\n" +
                "Reflects damage and maintains high defense during this time.\n" +
                "Recommended: 40-50 sec",

                ["Tier7_GuardianHeart_ReflectPercent"] =
                "【Reflect Damage Ratio (%)】\n" +
                "Percentage of received damage reflected to attacker.\n" +
                "Deals damage back to enemies as a tank.\n" +
                "Recommended: 5-8%",


                // ========================================
                // Polearm Tree (Tier 0~6, 25 keys)
                // ========================================








                // === Polearm Tree: Tier 0 - Polearm Expert (1 key) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【Attack Range Bonus (%)】\n" +
                "Increases attack range of polearms (halberds, glaives, etc.).\n" +
                "Long reach allows safe distance attacks.\n" +
                "Recommended: 10-20%",

                // === Polearm Tree: Tier 1 - Spin Wheel (1 key) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【Spinning Attack Damage Bonus (%)】\n" +
                "Additional damage on spinning attacks.\n" +
                "Useful against multiple enemies.\n" +
                "Recommended: 50-80%",

                // === Polearm Tree: Tier 2 - Hero Strike (1 key) ===
                ["Tier2_HeroStrike_KnockbackChance"] =
                "【Knockback Chance (%)】\n" +
                "Probability of pushing enemies back on hit.\n" +
                "Useful for battlefield control.\n" +
                "Recommended: 20-35%",

                // === Polearm Tree: Tier 3 - Area Combo (2 keys) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【2-Hit Damage Bonus (%)】\n" +
                "Additional damage on 2 consecutive hits.\n" +
                "Specialized for area combo damage.\n" +
                "Recommended: 20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【2-Hit Buff Duration (sec)】\n" +
                "Duration of 2-hit combo buff.\n" +
                "Longer duration for stable combos.\n" +
                "Recommended: 4-8 sec",

                // === Polearm Tree: Tier 3 - Ground Wheel (1 key) ===
                ["Tier3_GroundWheel_WheelAttackDamageBonus"] =
                "【Spinning Attack Damage Bonus (%)】\n" +
                "Increases ground-smashing spinning attack damage.\n" +
                "Core area damage skill.\n" +
                "Recommended: 70-100%",

                // === Polearm Tree: Tier 3 - Polearm Boost (1 key) ===
                ["Tier3_PolearmBoost_WeaponDamageBonus"] =
                "【Weapon Damage Bonus (Flat)】\n" +
                "Flat damage increase to base polearm damage.\n" +
                "Applies to all polearm attacks.\n" +
                "Recommended: 4-7",

                // === Polearm Tree: Tier 4 - Moon Slash (2 keys) ===
                ["Tier4_MoonSlash_AttackRangeBonus"] =
                "【Attack Range Bonus (%)】\n" +
                "Increases Moon Slash attack range.\n" +
                "Hit more enemies in wider arc.\n" +
                "Recommended: 12-20%",

                ["Tier4_MoonSlash_StaminaReduction"] =
                "【Stamina Cost Reduction (%)】\n" +
                "Reduces stamina cost when using Moon Slash.\n" +
                "Enables sustained combat.\n" +
                "Recommended: 12-20%",

                // === Polearm Tree: Tier 5 - Suppress Attack (1 key) ===
                ["Tier5_Suppress_DamageBonus"] =
                "【Suppress Attack Damage Bonus (%)】\n" +
                "Additional damage on suppress attacks.\n" +
                "Dominate enemies and seize combat initiative.\n" +
                "Recommended: 25-40%",

                // === Polearm Tree: Tier 6 - Pierce Charge (G-Key Active, 8 keys) ===
                ["Tier6_PierceCharge_DashDistance"] =
                "【Dash Distance (m)】\n" +
                "Charge distance during Pierce Charge.\n" +
                "Long dash to penetrate enemy lines.\n" +
                "Recommended: 8-12 m",

                ["Tier6_PierceCharge_FirstHitDamageBonus"] =
                "【First Hit Damage Bonus (%)】\n" +
                "Damage multiplier for initial charge hit.\n" +
                "Powerful opening strike to suppress enemies.\n" +
                "Recommended: 180-250%",

                ["Tier6_PierceCharge_AoeDamageBonus"] =
                "【AOE Knockback Damage Bonus (%)】\n" +
                "Damage multiplier for AOE knockback after charge.\n" +
                "Push back and damage surrounding enemies.\n" +
                "Recommended: 130-180%",

                ["Tier6_PierceCharge_AoeAngle"] =
                "【AOE Angle (degrees)】\n" +
                "Angle of AOE knockback effect.\n" +
                "280 degrees = rear/side area excluding front 80 degrees.\n" +
                "Recommended: 250-300 degrees",

                ["Tier6_PierceCharge_AoeRadius"] =
                "【AOE Radius (m)】\n" +
                "Radius of AOE knockback effect.\n" +
                "Larger radius pushes more enemies.\n" +
                "Recommended: 4-7 m",

                ["Tier6_PierceCharge_KnockbackDistance"] =
                "【Knockback Distance (m)】\n" +
                "Distance enemies are pushed back.\n" +
                "Useful for battlefield control.\n" +
                "Recommended: 6-10 m",

                ["Tier6_PierceCharge_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using G-key skill.\n" +
                "Stamina management is important.\n" +
                "Recommended: 18-25",

                ["Tier6_PierceCharge_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "G-key skill reactivation wait time.\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 25-40 sec",

            };
        }
    }
}

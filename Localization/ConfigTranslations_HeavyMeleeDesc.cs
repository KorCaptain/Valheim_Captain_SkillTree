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








                // === Spear Tree: Tier 0 - 창 전문가 (4개) ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "창 전문가 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

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

                ["Tier0_SpearExpert_ProcChance"] =
                "【창 전문가 발동 확률 (%)】\n" +
                "공격 시 번개 일격 proc이 발동될 확률입니다.\n" +
                "발동 시 다음 1회 공격이 고속으로 실행됩니다.\n" +
                "권장값: 20-35%",

                ["Tier0_SpearExpert_SpeedBoost"] =
                "【창 전문가 속도 부스트 (%)】\n" +
                "proc 발동 시 추가되는 공격속도 보너스입니다.\n" +
                "기본 100% + 이 수치 = 총 공격속도 배율.\n" +
                "권장값: 80-120%",

                // === Spear Tree: Tier 1 - 빠른 공격모션 (2개) ===
                ["Tier1_QuickStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "빠른 공격모션 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier1_AttackMotion"] =
                "【공격 모션 선택】\n" +
                "창의 일반 공격(좌클릭) 모션을 변경합니다.\n" +
                "단검: 3단 연속 찌르기 모션 / 검: 검 일반 공격 모션\n" +
                "허용값: 단검, 검",

                // === Spear Tree: Tier 2 - 투창 (3개 + Legacy 1개) ===
                ["Tier2_Throw_RequiredPoints"] =
                "【필요 포인트】\n" +
                "투창 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 5",

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

                // === Spear Tree: Tier 3 - 연격창 (2개) ===
                ["Tier3_Pierce_RequiredPoints"] =
                "【필요 포인트】\n" +
                "연격창 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 8",

                ["Tier3_Rapid_DamageBonus"] =
                "【관통 공격력 보너스 (고정값)】\n" +
                "창의 관통 공격력을 고정값으로 증가시킵니다.\n" +
                "빠른 연속 공격에 유리합니다.\n" +
                "권장값: 3-6",

                // === Spear Tree: Tier 3 - 빠른창 (1개) ===
                ["Tier3_QuickSpear_AttackSpeed"] =
                "【공격 속도 보너스 (%)】\n" +
                "창/폴암 착용 시 공격 속도가 증가합니다.\n" +
                "권장값: 15-25%",

                // === Spear Tree: Tier 4 - 회피 찌르기 (3개) ===
                ["Tier4_Evasion_RequiredPoints"] =
                "【필요 포인트】\n" +
                "회피 찌르기 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 12",

                ["Tier4_Evasion_EvasionBonus"] =
                "【공격 시 회피율 보너스 (%)】\n" +
                "창으로 공격 시 회피율이 증가합니다 (5초간).\n" +
                "공격적인 플레이에서 생존력을 높입니다.\n" +
                "권장값: 15-25%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【공격 스태미나 소모 감소 (%)】\n" +
                "회피 찌르기 공격의 스태미나 소모가 감소합니다.\n" +
                "지속적인 전투가 가능합니다.\n" +
                "권장값: 5-15%",

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

                // === Spear Tree: Tier 5 - 꿰뚫는 창 (G키 액티브, 6개 + Legacy 1개) ===
                ["Tier5_Penetrate_RequiredPoints"] =
                "【필요 포인트】\n" +
                "꿰뚫는 창 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 15",

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

                ["Tier5_Penetrate_BaseDamage"] =
                "【Lv1 기본 단일 피해 (%)】\n" +
                "꿰뚫는 창 돌진 시 경로 적에게 입히는 기본 단일 피해입니다.\n" +
                "창 무기의 관통 공격력 대비 비율로 계산됩니다.\n" +
                "권장값: 80-120%",

                ["Tier5_Penetrate_LevelDamageBonus"] =
                "【레벨당 단일 피해 증가 (%)】\n" +
                "꿰뚫는 창 레벨 1당 추가되는 단일 피해 증가량입니다.\n" +
                "권장값: 3-8%",

                ["Tier5_Penetrate_BaseAreaDamage"] =
                "【Lv1 기본 범위 피해 (%)】\n" +
                "꿰뚫는 창 돌진 시 경로 5m 범위에 입히는 기본 피해입니다.\n" +
                "권장값: 60-100%",

                ["Tier5_Penetrate_AreaLevelBonus"] =
                "【레벨당 범위 피해 증가 (%)】\n" +
                "꿰뚫는 창 레벨 1당 추가되는 범위 피해 증가량입니다.\n" +
                "권장값: 3-8%",

                // === Spear Tree: Tier 5 - 연공창 (H키 액티브, 8개) ===
                ["Tier5_Combo_RequiredPoints"] =
                "【필요 포인트】\n" +
                "연공창 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 15",

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

                ["Tier5_Combo_LevelBonus"] =
                "【레벨 보너스 (%)】\n" +
                "연공창 스킬 레벨당 데미지 보너스입니다.\n" +
                "권장값: 5-15%",

                // ========================================
                // Mace Tree (티어별 정렬)
                // ========================================

                // === Tier 0: 둔기 전문가 (Mace Expert) ===

                ["Tier0_MaceExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "둔기 전문가 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

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

                // === Tier 1: 둔기 강화 ===

                ["Tier1_MaceExpert_DamageBonus"] =
                "【둔기 공격력 보너스 (%)】\n" +
                "둔기 무기의 추가 공격력 보너스입니다.\n" +
                "권장값: 8-15%",

                ["Tier1_MaceExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "둔기 강화 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

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

                ["Tier2_StunBoost_RequiredPoints"] =
                "【필요 포인트】\n" +
                "기절 강화 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

                // === Tier 3: 회전 타격 (Spin Strike) ===

                ["Tier3_SpinStrike_DamageBonus"] =
                "【세컨드 어택 데미지 보너스 (%)】\n" +
                "세컨드 어택 시 공격력을 증가시킵니다.\n" +
                "퍼센트 기반으로 높은 기본 공격력일수록 효과가 커집니다.\n" +
                "권장값: 15-25%",

                ["Tier3_SpinStrike_Range"] =
                "【AOE 범위 (미터)】\n" +
                "세컨드 어택 시 주변 적에게 데미지를 주는 범위입니다.\n" +
                "권장값: 5-10m",

                ["Tier3_SpinStrike_KnockbackForce"] =
                "【회전 타격 넉백 거리 (미터)】\n" +
                "세컨드 어택 시 적을 밀어내는 거리입니다. 한손·양손 둔기 공통 적용.\n" +
                "권장값: 2-5m",

                ["Tier3_Guard_RequiredPoints"] =
                "【필요 포인트】\n" +
                "방어 강화 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

                // === Tier 3: 무거운 일격 (Heavy Strike) ===

                ["Tier3_HeavyStrike_DamageBonus"] =
                "【타격 보너스 (고정값)】\n" +
                "둔기 타격 데미지를 고정 수치로 증가시킵니다.\n" +
                "퍼센트 보너스와 함께 적용됩니다.\n" +
                "권장값: 2-5",

                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "무거운 일격 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

                // === Tier 4: 뇌진탕 (Concussion) ===

                ["Tier4_Push_KnockbackChance"] =
                "【뇌진탕 확률 (%)】\n" +
                "둔기로 공격 시 대상의 이동속도와 공격속도를 1.5초간 30% 감소시키는 확률입니다.\n" +
                "전투 제어와 딜링 우위 확보에 유용합니다.\n" +
                "권장값: 30-40%",

                ["Tier4_Push_RequiredPoints"] =
                "【필요 포인트】\n" +
                "뇌진탕 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

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

                ["Tier5_Tank_RequiredPoints"] =
                "【필요 포인트】\n" +
                "탱커 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

                // === Tier 5: 데미지 강화 (DPS) ===

                ["Tier5_DPS_DamageBonus"] =
                "【공격력 보너스 (%)】\n" +
                "둔기 무기의 공격력을 추가로 증가시킵니다.\n" +
                "DPS 빌드에 유용합니다.\n" +
                "권장값: 15-25%",

                ["Tier5_DPS_RequiredPoints"] =
                "【필요 포인트】\n" +
                "데미지 강화 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

                // === Tier 6: 속공 (Sokgong / Swift Attack) ===

                ["Tier6_Sokgong_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "둔기 공격속도를 증가시킵니다.\n" +
                "느린 둔기의 약점을 보완합니다.\n" +
                "권장값: 8-15%",

                ["Tier6_Grandmaster_RequiredPoints"] =
                "【필요 포인트】\n" +
                "속공 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1",

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

                ["Tier7_FuryHammer_RequiredPoints"] =
                "【필요 포인트】\n" +
                "분노의 망치 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier7_FuryHammer_NormalHitLevelBonus"] =
                "【일반 타격 레벨 보너스 (%)】\n" +
                "분노의 망치 일반 타격 레벨당 데미지 보너스입니다.\n" +
                "권장값: 5-15%",

                ["Tier7_FuryHammer_FinalHitLevelBonus"] =
                "【최종 타격 레벨 보너스 (%)】\n" +
                "분노의 망치 최종(폭발) 타격 레벨당 데미지 보너스입니다.\n" +
                "권장값: 10-25%",

                // === Tier 6-5: 방패돌진 (Shield Charge - 방어 전문가, 둔기 트리에서 이전) ===
                ["Tier6_GuardianHeart_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "G/H키 스킬 '방패돌진' 재사용 대기 시간입니다.\n" +
                "권장값: 30-40초",

                ["Tier6_GuardianHeart_StaminaCost"] =
                "【스태미나 소모】\n" +
                "방패돌진 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 15-25",

                ["Tier6_ShieldCharge_DamagePercent"] =
                "【방패 막기력 데미지 비율 (%)】\n" +
                "방패돌진 충돌 시 방패 막기력 대비 가하는 데미지 비율입니다.\n" +
                "높을수록 방패 방어력이 공격력으로 전환됩니다.\n" +
                "권장값: 60-80%",

                ["Tier6_ShieldCharge_MultiHitDamagePercent"] =
                "【다단히트 데미지 비율 (%)】\n" +
                "돌진 중 0.08초 간격 광역타 및 끌어모은 적 최종타(4회, 0.25초 간격)에\n" +
                "적용되는 방패 막기력 대비 기준 데미지 비율(레벨 1 기준)입니다.\n" +
                "권장값: 20-40%",

                ["Tier6_ShieldCharge_MultiHitLevelBonus"] =
                "【다단히트 레벨 보너스 (%)】\n" +
                "방패돌진 스킬 레벨당 다단히트 데미지 비율 증가량입니다.\n" +
                "권장값: 5-15%",

                ["Tier6_ShieldCharge_LevelBonus"] =
                "【레벨 보너스 (%)】\n" +
                "방패돌진 스킬 레벨당 돌진 데미지 보너스입니다.\n" +
                "권장값: 5-15%",

                ["Tier6_GuardianHeart_RequiredPoints"] =
                "【필요 포인트】\n" +
                "방패돌진 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                // === Tier 7: 충격파 강타 (Shockwave Slam - G키 액티브) ===
                ["Tier7_ShockwaveSlam_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "G키 스킬 '충격파 강타' 재사용 대기 시간입니다.\n" +
                "권장값: 30-50초",

                ["Tier7_ShockwaveSlam_StaminaCost"] =
                "【스태미나 소모】\n" +
                "충격파 강타 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 15-25",

                ["Tier7_ShockwaveSlam_DamagePercent"] =
                "【무기 공격력 데미지 비율 (%)】\n" +
                "충격파 강타 적중 시 무기 공격력 대비 가하는 데미지 비율입니다.\n" +
                "권장값: 200-260%",

                ["Tier7_ShockwaveSlam_LevelBonus"] =
                "【레벨 보너스 (%)】\n" +
                "충격파 강타 스킬 레벨당 데미지 보너스입니다.\n" +
                "권장값: 10-30%",

                ["Tier7_ShockwaveSlam_RequiredPoints"] =
                "【필요 포인트】\n" +
                "충격파 강타 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",


                // ========================================
                // Polearm Tree (Tier 0~5, 37 keys)
                // ========================================








                // === Polearm Tree: Tier 0 - 폴암 전문가 (2개) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【공격 범위 보너스 (%)】\n" +
                "폴암(장창, 창날도끼 등)의 공격 범위를 증가시킵니다.\n" +
                "긴 리치로 안전 거리를 유지하며 공격할 수 있습니다.\n" +
                "권장값: 10-20%",

                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "폴암 전문가 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 2",

                // === Polearm Tree: Tier 1 - 회전베기 (2개) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【회전 공격 피해 보너스 (%)】\n" +
                "회전 공격 시 추가 피해를 가합니다.\n" +
                "다수의 적을 상대할 때 유용합니다.\n" +
                "권장값: 50-80%",

                ["Tier1_SpinWheel_RequiredPoints"] =
                "【필요 포인트】\n" +
                "회전베기 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 2",

                // === Polearm Tree: Tier 2-1 - 폴암강화 (2개) ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] =
                "【관통 공격력 보너스 (고정값)】\n" +
                "폴암의 관통 공격력을 고정값으로 증가시킵니다.\n" +
                "모든 폴암 공격에 적용됩니다.\n" +
                "권장값: 4-7",

                ["Tier2-1_PolearmBoost_RequiredPoints"] =
                "【필요 포인트】\n" +
                "폴암강화 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 2",

                // === Polearm Tree: Tier 2-2 - 영웅 타격 (2개) ===
                ["Tier2-2_HeroStrike_KnockbackChance"] =
                "【넉백 확률 (%)】\n" +
                "공격 시 적을 밀쳐낼 확률입니다.\n" +
                "전장 제어에 유용합니다.\n" +
                "권장값: 20-35%",

                ["Tier2-2_HeroStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "영웅 타격 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 2",

                // === Polearm Tree: Tier 3 - 광역 강타 (3개) ===
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

                ["Tier3_AreaCombo_RequiredPoints"] =
                "【필요 포인트】\n" +
                "광역 강타 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 2",

                // === Polearm Tree: Tier 4-1 - 폭풍베기 (3개) ===
                ["Tier4-1_StormSlash_ExplosionBonus"] =
                "【번개속성 추가 데미지】\n" +
                "1차 공격 후 4초 이내 특수(휠 마우스) 공격 시 추가되는 번개 데미지입니다.\n" +
                "권장값: 10-20",

                ["Tier4-1_GroundWheel_RequiredPoints"] =
                "【필요 포인트】\n" +
                "폭풍베기 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 2",

                // === Polearm Tree: Tier 4-2 - 반달 베기 (3개) ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] =
                "【공격 범위 보너스 (%)】\n" +
                "반달 베기의 공격 범위를 증가시킵니다.\n" +
                "더 넓은 범위의 적을 공격할 수 있습니다.\n" +
                "권장값: 12-20%",

                ["Tier4-2_MoonSlash_StaminaReduction"] =
                "【스태미나 소모 감소 (%)】\n" +
                "반달 베기 사용 시 스태미나 소모가 감소합니다.\n" +
                "지속적인 전투가 가능합니다.\n" +
                "권장값: 12-20%",

                ["Tier4-2_MoonSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "반달 베기 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 2",

                // === Polearm Tree: Tier 4-3 - 제압 공격 (2개) ===
                ["Tier4-3_Suppress_DamageBonus"] =
                "【제압 공격 피해 보너스 (%)】\n" +
                "제압 공격 시 추가 피해를 가합니다.\n" +
                "적을 억압하며 전투 주도권을 장악합니다.\n" +
                "권장값: 25-40%",

                ["Tier4-3_Suppress_RequiredPoints"] =
                "【필요 포인트】\n" +
                "제압 공격 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 3",

                // === Polearm Tree: Tier 5 - 관통 돌격 (G키 액티브, 9개) ===
                ["Tier5_PierceCharge_DashDistance"] =
                "【돌진 거리 (m)】\n" +
                "관통 돌격 시 돌진하는 거리입니다.\n" +
                "긴 거리로 적진을 뚫고 들어갑니다.\n" +
                "권장값: 8-12m",

                ["Tier5_PierceCharge_FirstHitDamageBonus"] =
                "【첫 타격 피해 보너스 (%)】\n" +
                "돌격 중 첫 타격의 피해 배율입니다.\n" +
                "강력한 첫 타격으로 적을 제압합니다.\n" +
                "권장값: 180-250%",

                ["Tier5_PierceCharge_AoeDamageBonus"] =
                "【광역 넉백 피해 보너스 (%)】\n" +
                "돌격 종료 후 광역 넉백 공격의 피해 배율입니다.\n" +
                "주변 적들을 밀쳐내며 피해를 가합니다.\n" +
                "권장값: 130-180%",

                ["Tier5_PierceCharge_AoeAngle"] =
                "【광역 각도 (도)】\n" +
                "광역 넉백 효과의 각도입니다.\n" +
                "280도 = 전방 80도 제외한 후방/측면 범위입니다.\n" +
                "권장값: 250-300도",

                ["Tier5_PierceCharge_AoeRadius"] =
                "【광역 반경 (m)】\n" +
                "광역 넉백 효과의 반경입니다.\n" +
                "넓을수록 더 많은 적을 밀쳐냅니다.\n" +
                "권장값: 4-7m",

                ["Tier5_PierceCharge_KnockbackDistance"] =
                "【넉백 거리 (m)】\n" +
                "적을 밀쳐내는 거리입니다.\n" +
                "전장 제어에 유용합니다.\n" +
                "권장값: 6-10m",

                ["Tier5_PierceCharge_StaminaCost"] =
                "【스태미나 소모】\n" +
                "G키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 18-25",

                ["Tier5_PierceCharge_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "G키 스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 25-40초",

                ["Tier5_PierceCharge_RequiredPoints"] =
                "【필요 포인트】\n" +
                "관통 돌격 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 3",

                ["Tier5_PierceCharge_LevelBonus"] =
                "【레벨당 추가 데미지 보너스 (%)】\n" +
                "관통 돌격 스킬의 레벨당 추가 데미지 보너스입니다.\n" +
                "권장값: 20-40%",

                // === Polearm Tree: 휠윈드 (7개) ===
                ["Tier6_Whirlwind_DamagePercent"] =
                "【휠윈드 공격력 비율 (%)】\n" +
                "마우스 휠 버튼 홀드 시 사이클당 무기 공격력 배율.\n" +
                "권장값: 15-35%",

                ["Tier6_Whirlwind_StaminaPerSec"] =
                "【휠윈드 스태미나 소모/회】\n" +
                "점프-공격 1사이클당 소모되는 스태미나.\n" +
                "권장값: 3-6",

                ["Tier6_Whirlwind_MoveSpeed"] =
                "【휠윈드 이동 속도 (m/s)】\n" +
                "사이클당 이동 거리 기준 속도.\n" +
                "권장값: 3-6",

                ["Tier6_Whirlwind_AttackInterval"] =
                "【휠윈드 공격 간격 (초)】\n" +
                "공격 모션 간 대기 시간.\n" +
                "권장값: 0.2-0.5",

                ["Tier6_Whirlwind_VfxInterval"] =
                "【휠윈드 VFX 간격 (초)】\n" +
                "시각 효과 재생 간격.\n" +
                "권장값: 1-3",

                ["Tier6_Whirlwind_Cooldown"] =
                "【휠윈드 쿨타임 (초)】\n" +
                "스킬 종료 후 재사용까지의 대기 시간.\n" +
                "권장값: 10-30",

                ["Tier6_Whirlwind_RequiredPoints"] =
                "【필요 포인트】\n" +
                "휠윈드 노드를 해제하는 데 필요한 스킬 포인트입니다.\n" +
                "권장값: 3",

                ["Tier6_Whirlwind_LevelBonus"] =
                "【레벨 보너스 (%)】\n" +
                "휠윈드 스킬 레벨당 데미지 보너스입니다.\n" +
                "권장값: 5-15%",

                ["Tier6_Whirlwind_DamageReductionPercent"] =
                "【받는 피해 감소 (%)】\n" +
                "휠윈드 사용 중 받는 피해를 감소시킵니다. (Lv1 기준값)\n" +
                "권장값: 20-40%",

                ["Tier6_Whirlwind_DamageReductionLevelBonus"] =
                "【피해 감소 레벨 보너스 (%)】\n" +
                "휠윈드 스킬 레벨당 추가 피해 감소량입니다.\n" +
                "권장값: 5-10%",

            };
        }

        private static Dictionary<string, string> GetHeavyMeleeDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Spear Tree (Tier 0~5, 35 keys)
                // ========================================








                // === Spear Tree: Tier 0 - Spear Expert (4 keys) ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Spear Expert node.\n" +
                "Recommended: 2",

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

                ["Tier0_SpearExpert_ProcChance"] =
                "【Spear Expert Proc Chance (%)】\n" +
                "Chance to trigger Lightning Strike proc on attack.\n" +
                "Triggers 1 extra fast attack when proc activates.\n" +
                "Recommended: 20-35%",

                ["Tier0_SpearExpert_SpeedBoost"] =
                "【Spear Expert Speed Boost (%)】\n" +
                "Attack speed bonus added when proc activates.\n" +
                "Base 100% + this value = total attack speed multiplier.\n" +
                "Recommended: 80-120%",

                // === Spear Tree: Tier 1 - Vital Strike (2 keys) ===
                ["Tier1_QuickStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Quick Attack Motion node.\n" +
                "Recommended: 3",

                ["Tier1_AttackMotion"] =
                "【Attack Motion Selection】\n" +
                "Changes spear normal attack (LMB) animation.\n" +
                "단검(Knife): 3-hit stab chain / 검(Sword): sword swing\n" +
                "Acceptable values: 단검, 검",

                // === Spear Tree: Tier 2 - Throw (3 keys + Legacy 1) ===
                ["Tier2_Throw_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Throw Spear node.\n" +
                "Recommended: 5",

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

                // === Spear Tree: Tier 3 - Rapid Pierce (2 keys) ===
                ["Tier3_Pierce_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Rapid Pierce node.\n" +
                "Recommended: 8",

                ["Tier3_Rapid_DamageBonus"] =
                "【Pierce Damage Bonus (Flat)】\n" +
                "Flat pierce damage increase to spear attacks.\n" +
                "Favors rapid consecutive attacks.\n" +
                "Recommended: 3-6",

                // === Spear Tree: Tier 3 - Fast Spear (1 key) ===
                ["Tier3_QuickSpear_AttackSpeed"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases attack speed when using spear or polearm.\n" +
                "Recommended: 15-25%",

                // === Spear Tree: Tier 4 - Evasion Strike (3 keys) ===
                ["Tier4_Evasion_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Evasion Strike node.\n" +
                "Recommended: 12",

                ["Tier4_Evasion_EvasionBonus"] =
                "【Evasion Bonus on Attack (%)】\n" +
                "Evasion rate increases when attacking with spear (for 5s).\n" +
                "Improves survivability during aggressive play.\n" +
                "Recommended: 15-25%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【Attack Stamina Cost Reduction (%)】\n" +
                "Reduces stamina cost for Evasion Strike attacks.\n" +
                "Enables sustained combat.\n" +
                "Recommended: 5-15%",

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

                // === Spear Tree: Tier 5 - Penetrating Spear (G-Key Active, 6 keys + Legacy 1) ===
                ["Tier5_Penetrate_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Penetrating Spear node.\n" +
                "Recommended: 15",

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

                ["Tier5_Penetrate_BaseDamage"] =
                "【Lv1 Base Single Hit Damage (%)】\n" +
                "Base single-target damage dealt to enemies in the dash path.\n" +
                "Calculated as a ratio of the spear weapon's pierce damage.\n" +
                "Recommended: 80-120%",

                ["Tier5_Penetrate_LevelDamageBonus"] =
                "【Single Damage Bonus per Level (%)】\n" +
                "Additional single-target damage added per skill level.\n" +
                "Recommended: 3-8%",

                ["Tier5_Penetrate_BaseAreaDamage"] =
                "【Lv1 Base Area Damage (%)】\n" +
                "Base damage dealt to enemies within 5m radius of the dash path.\n" +
                "Recommended: 60-100%",

                ["Tier5_Penetrate_AreaLevelBonus"] =
                "【Area Damage Bonus per Level (%)】\n" +
                "Additional area damage added per skill level.\n" +
                "Recommended: 3-8%",

                // === Spear Tree: Tier 5 - Combo Spear (H-Key Active, 8 keys) ===
                ["Tier5_Combo_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Combo Spear node.\n" +
                "Recommended: 15",

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

                ["Tier5_Combo_LevelBonus"] =
                "【Level Bonus (%)】\n" +
                "Damage bonus per level for the Combo Spear skill.\n" +
                "Recommended: 5-15%",

                // Mace Tree (Tier-based Sorting)
                // ========================================

                // === Tier 0: Mace Expert ===

                ["Tier0_MaceExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Mace Expert node.\n" +
                "Recommended: 2",

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

                // === Tier 1: Mace Damage Boost ===

                ["Tier1_MaceExpert_DamageBonus"] =
                "【Mace Damage Bonus (%)】\n" +
                "Additional damage bonus for mace weapons.\n" +
                "Recommended: 8-15%",

                ["Tier1_MaceExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Mace Damage Boost node.\n" +
                "Recommended: 1",

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

                ["Tier2_StunBoost_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Stun Boost node.\n" +
                "Recommended: 1",

                // === Tier 3: Spin Strike ===

                ["Tier3_SpinStrike_DamageBonus"] =
                "【Secondary Attack Damage Bonus (%)】\n" +
                "Increases damage on secondary attack.\n" +
                "Percentage-based; higher base damage yields greater effect.\n" +
                "Recommended: 15-25%",

                ["Tier3_SpinStrike_Range"] =
                "【AoE Range (meters)】\n" +
                "Range within which nearby enemies take damage on secondary attack.\n" +
                "Recommended: 5-10m",

                ["Tier3_SpinStrike_KnockbackForce"] =
                "【Spin Strike Knockback Distance (meters)】\n" +
                "Distance enemies are pushed on secondary attack. Applies to both one-handed and two-handed maces.\n" +
                "Recommended: 2-5m",

                ["Tier3_Guard_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Guard node.\n" +
                "Recommended: 1",

                // === Tier 3: Heavy Strike ===

                ["Tier3_HeavyStrike_DamageBonus"] =
                "【Blunt Bonus (Fixed Value)】\n" +
                "Increases mace blunt damage by a fixed amount.\n" +
                "Applies alongside percentage bonuses.\n" +
                "Recommended: 2-5",

                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Heavy Strike node.\n" +
                "Recommended: 1",

                // === Tier 4: Concussion ===

                ["Tier4_Push_KnockbackChance"] =
                "【Concussion Chance (%)】\n" +
                "Chance on hit with a mace to slow the target's movement and attack speed by 30% for 1.5s.\n" +
                "Useful for combat control and gaining a damage-race advantage.\n" +
                "Recommended: 30-40%",

                ["Tier4_Push_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Concussion node.\n" +
                "Recommended: 1",

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

                ["Tier5_Tank_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Tank node.\n" +
                "Recommended: 1",

                // === Tier 5: DPS ===

                ["Tier5_DPS_DamageBonus"] =
                "【Damage Bonus (%)】\n" +
                "Additional damage increase for mace weapons.\n" +
                "Useful for DPS builds.\n" +
                "Recommended: 15-25%",

                ["Tier5_DPS_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the DPS Boost node.\n" +
                "Recommended: 1",

                // === Tier 6: Swift Attack (Sokgong) ===

                ["Tier6_Sokgong_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases mace attack speed.\n" +
                "Compensates for the slow mace attack rate.\n" +
                "Recommended: 8-15%",

                ["Tier6_Grandmaster_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Swift Attack node.\n" +
                "Recommended: 1",

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

                ["Tier7_FuryHammer_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Fury Hammer node.\n" +
                "Recommended: 3",

                ["Tier7_FuryHammer_NormalHitLevelBonus"] =
                "【Normal Hit Level Bonus (%)】\n" +
                "Damage bonus per level for Fury Hammer normal hits.\n" +
                "Recommended: 5-15%",

                ["Tier7_FuryHammer_FinalHitLevelBonus"] =
                "【Final Hit Level Bonus (%)】\n" +
                "Damage bonus per level for Fury Hammer final (explosion) hit.\n" +
                "Recommended: 10-25%",

                // === Tier 6-5: Shield Charge (Defense Expert, moved from Mace tree) ===
                ["Tier6_GuardianHeart_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Reuse delay for G/H-key skill 'Shield Charge'.\n" +
                "Recommended: 30-40 sec",

                ["Tier6_GuardianHeart_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using Shield Charge.\n" +
                "Recommended: 15-25",

                ["Tier6_ShieldCharge_DamagePercent"] =
                "【Shield Block Power Damage Ratio (%)】\n" +
                "Damage dealt on Shield Charge collision as a percentage of shield block power.\n" +
                "Higher values convert more of your shield's defense into offensive power.\n" +
                "Recommended: 60-80%",

                ["Tier6_ShieldCharge_MultiHitDamagePercent"] =
                "【Multi-Hit Damage Ratio (%)】\n" +
                "Base damage ratio (at level 1) as a percentage of shield block power, applied to the\n" +
                "0.08s-interval area multi-hit during the dash and the 4-tick/0.25s finish multi-hit on gathered enemies.\n" +
                "Recommended: 20-40%",

                ["Tier6_ShieldCharge_MultiHitLevelBonus"] =
                "【Multi-Hit Level Bonus (%)】\n" +
                "Multi-hit damage ratio increase per Shield Charge skill level.\n" +
                "Recommended: 5-15%",

                ["Tier6_ShieldCharge_LevelBonus"] =
                "【Level Bonus (%)】\n" +
                "Damage bonus per level for the Shield Charge skill.\n" +
                "Recommended: 5-15%",

                ["Tier6_GuardianHeart_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Shield Charge node.\n" +
                "Recommended: 3",

                ["Tier7_ShockwaveSlam_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Cooldown time for the G-key skill 'Shockwave Slam'.\n" +
                "Recommended: 30-50 seconds",

                ["Tier7_ShockwaveSlam_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using Shockwave Slam.\n" +
                "Recommended: 15-25",

                ["Tier7_ShockwaveSlam_DamagePercent"] =
                "【Weapon Damage Ratio (%)】\n" +
                "Damage dealt on Shockwave Slam hit as a percentage of weapon damage.\n" +
                "Recommended: 200-260%",

                ["Tier7_ShockwaveSlam_LevelBonus"] =
                "【Level Bonus (%)】\n" +
                "Damage bonus per level for the Shockwave Slam skill.\n" +
                "Recommended: 10-30%",

                ["Tier7_ShockwaveSlam_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Shockwave Slam node.\n" +
                "Recommended: 3",


                // ========================================
                // Polearm Tree (Tier 0~5, 37 keys)
                // ========================================








                // === Polearm Tree: Tier 0 - Polearm Expert (2 keys) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【Attack Range Bonus (%)】\n" +
                "Increases attack range of polearms (halberds, glaives, etc.).\n" +
                "Long reach allows safe distance attacks.\n" +
                "Recommended: 10-20%",

                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Polearm Expert node.\n" +
                "Recommended: 2",

                // === Polearm Tree: Tier 1 - Spin Slash (2 keys) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【Spinning Attack Damage Bonus (%)】\n" +
                "Additional damage on spinning attacks.\n" +
                "Useful against multiple enemies.\n" +
                "Recommended: 50-80%",

                ["Tier1_SpinWheel_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Spin Slash node.\n" +
                "Recommended: 2",

                // === Polearm Tree: Tier 2-1 - Polearm Enhancement (2 keys) ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] =
                "【Pierce Damage Bonus (Flat)】\n" +
                "Flat pierce damage increase to polearm attacks.\n" +
                "Applies to all polearm attacks.\n" +
                "Recommended: 4-7",

                ["Tier2-1_PolearmBoost_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Polearm Enhancement node.\n" +
                "Recommended: 2",

                // === Polearm Tree: Tier 2-2 - Hero Strike (2 keys) ===
                ["Tier2-2_HeroStrike_KnockbackChance"] =
                "【Stagger Chance (%)】\n" +
                "Probability of staggering enemies on hit.\n" +
                "Useful for battlefield control.\n" +
                "Recommended: 20-35%",

                ["Tier2-2_HeroStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Hero Strike node.\n" +
                "Recommended: 2",

                // === Polearm Tree: Tier 3 - Wide Slash (3 keys) ===
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

                ["Tier3_AreaCombo_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Wide Slash node.\n" +
                "Recommended: 2",

                // === Polearm Tree: Tier 4-1 - Storm Slash (3 keys) ===
                ["Tier4-1_StormSlash_ExplosionBonus"] =
                "【Lightning Damage Bonus】\n" +
                "Lightning damage added when using wheel mouse attack within 4s of a primary attack.\n" +
                "Recommended: 10-20",

                ["Tier4-1_GroundWheel_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Storm Slash node.\n" +
                "Recommended: 2",

                // === Polearm Tree: Tier 4-2 - Crescent Slash (3 keys) ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] =
                "【Attack Range Bonus (%)】\n" +
                "Increases Crescent Slash attack range.\n" +
                "Hit more enemies in wider arc.\n" +
                "Recommended: 12-20%",

                ["Tier4-2_MoonSlash_StaminaReduction"] =
                "【Stamina Cost Reduction (%)】\n" +
                "Reduces stamina cost when using Crescent Slash.\n" +
                "Enables sustained combat.\n" +
                "Recommended: 12-20%",

                ["Tier4-2_MoonSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Crescent Slash node.\n" +
                "Recommended: 2",

                // === Polearm Tree: Tier 4-3 - Suppress Attack (2 keys) ===
                ["Tier4-3_Suppress_DamageBonus"] =
                "【Suppress Attack Damage Bonus (%)】\n" +
                "Additional damage on suppress attacks.\n" +
                "Dominate enemies and seize combat initiative.\n" +
                "Recommended: 25-40%",

                ["Tier4-3_Suppress_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Suppress Attack node.\n" +
                "Recommended: 3",

                // === Polearm Tree: Tier 5 - Pierce Charge (G-Key Active, 9 keys) ===
                ["Tier5_PierceCharge_DashDistance"] =
                "【Dash Distance (m)】\n" +
                "Charge distance during Pierce Charge.\n" +
                "Long dash to penetrate enemy lines.\n" +
                "Recommended: 8-12 m",

                ["Tier5_PierceCharge_FirstHitDamageBonus"] =
                "【First Hit Damage Bonus (%)】\n" +
                "Damage multiplier for initial charge hit.\n" +
                "Powerful opening strike to suppress enemies.\n" +
                "Recommended: 180-250%",

                ["Tier5_PierceCharge_AoeDamageBonus"] =
                "【AOE Knockback Damage Bonus (%)】\n" +
                "Damage multiplier for AOE knockback after charge.\n" +
                "Push back and damage surrounding enemies.\n" +
                "Recommended: 130-180%",

                ["Tier5_PierceCharge_AoeAngle"] =
                "【AOE Angle (degrees)】\n" +
                "Angle of AOE knockback effect.\n" +
                "280 degrees = rear/side area excluding front 80 degrees.\n" +
                "Recommended: 250-300 degrees",

                ["Tier5_PierceCharge_AoeRadius"] =
                "【AOE Radius (m)】\n" +
                "Radius of AOE knockback effect.\n" +
                "Larger radius pushes more enemies.\n" +
                "Recommended: 4-7 m",

                ["Tier5_PierceCharge_KnockbackDistance"] =
                "【Knockback Distance (m)】\n" +
                "Distance enemies are pushed back.\n" +
                "Useful for battlefield control.\n" +
                "Recommended: 6-10 m",

                ["Tier5_PierceCharge_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using G-key skill.\n" +
                "Stamina management is important.\n" +
                "Recommended: 18-25",

                ["Tier5_PierceCharge_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "G-key skill reactivation wait time.\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 25-40 sec",

                ["Tier5_PierceCharge_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Pierce Charge node.\n" +
                "Recommended: 3",

                ["Tier5_PierceCharge_LevelBonus"] =
                "【Level Bonus Damage (%)】\n" +
                "Additional damage bonus per level for the Pierce Charge skill.\n" +
                "Recommended: 20-40%",

                // === Polearm Tree: Whirlwind (7 keys) ===
                ["Tier6_Whirlwind_DamagePercent"] =
                "【Whirlwind Damage Ratio (%)】\n" +
                "Weapon damage multiplier per jump-attack cycle.\n" +
                "Recommended: 15-35%",

                ["Tier6_Whirlwind_StaminaPerSec"] =
                "【Whirlwind Stamina Cost/Cycle】\n" +
                "Stamina consumed per jump-attack cycle.\n" +
                "Recommended: 3-6",

                ["Tier6_Whirlwind_MoveSpeed"] =
                "【Whirlwind Move Speed (m/s)】\n" +
                "Movement distance basis per cycle.\n" +
                "Recommended: 3-6",

                ["Tier6_Whirlwind_AttackInterval"] =
                "【Whirlwind Attack Interval (sec)】\n" +
                "Wait time between attack motions.\n" +
                "Recommended: 0.2-0.5",

                ["Tier6_Whirlwind_VfxInterval"] =
                "【Whirlwind VFX Interval (sec)】\n" +
                "Visual effect playback interval.\n" +
                "Recommended: 1-3",

                ["Tier6_Whirlwind_Cooldown"] =
                "【Whirlwind Cooldown (sec)】\n" +
                "Wait time before reuse after skill ends.\n" +
                "Recommended: 10-30",

                ["Tier6_Whirlwind_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Whirlwind node.\n" +
                "Recommended: 3",

                ["Tier6_Whirlwind_LevelBonus"] =
                "【Level Bonus (%)】\n" +
                "Damage bonus per level for the Whirlwind skill.\n" +
                "Recommended: 5-15%",

                ["Tier6_Whirlwind_DamageReductionPercent"] =
                "【Damage Reduction (%)】\n" +
                "Reduces damage taken while Whirlwind is active. (Lv1 base value)\n" +
                "Recommended: 20-40%",

                ["Tier6_Whirlwind_DamageReductionLevelBonus"] =
                "【Damage Reduction Level Bonus (%)】\n" +
                "Additional damage reduction per Whirlwind skill level.\n" +
                "Recommended: 5-10%",

            };
        }
    }
}

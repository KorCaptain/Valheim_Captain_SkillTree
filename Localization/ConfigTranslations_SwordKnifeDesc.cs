using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetSwordKnifeDescriptions_KO()
        {
            return new Dictionary<string, string>
            {









                // === Sword Tree: 검 전문가 (1개) ===
                ["Sword_Expert_DamageIncrease"] =
                "【검 피해 증가 (%)】\n" +
                "검 무기의 기본 공격력을 증가시킵니다.\n" +
                "모든 검류에 적용됩니다.\n" +
                "권장값: 10-20%",

                // === Sword Tree: 빠른 베기 (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "검 공격속도를 증가시킵니다.\n" +
                "빠른 연속 공격이 가능합니다.\n" +
                "권장값: 10-20%",

                // === Sword Tree: 반격 자세 (2개) ===
                ["Tier1_CounterStance_Duration"] =
                "【지속시간 (초)】\n" +
                "반격 자세를 유지하는 시간입니다.\n" +
                "이 시간 동안 방어력이 증가합니다.\n" +
                "권장값: 3-6초",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【방어력 보너스 (%)】\n" +
                "반격 자세 중 방어력 증가량입니다.\n" +
                "적의 공격을 버티며 반격 기회를 노립니다.\n" +
                "권장값: 20-40%",

                // === Sword Tree: 연속 베기 (2개) ===
                ["Sword_ComboSlash_Bonus"] =
                "【연속 공격 보너스 (%)】\n" +
                "연속 공격 시 추가 피해를 입힙니다.\n" +
                "콤보를 유지하면 높은 DPS를 낼 수 있습니다.\n" +
                "권장값: 15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "연속 공격 보너스가 유지되는 시간입니다.\n" +
                "이 시간 내에 다시 공격하면 버프가 연장됩니다.\n" +
                "권장값: 3-5초",

                // === Sword Tree: 칼날 되치기 (1개) ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【공격력 보너스 (고정값)】\n" +
                "칼날 되치기 공격력을 고정 수치로 증가시킵니다.\n" +
                "패링 후 강력한 반격이 가능합니다.\n" +
                "권장값: 20-40",

                // === Sword Tree: 공방일체 (2개) ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【공격력 보너스 (%)】\n" +
                "공격과 방어를 동시에 강화합니다.\n" +
                "균형잡힌 전투 스타일에 유용합니다.\n" +
                "권장값: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【방어력 보너스 (고정값)】\n" +
                "공방일체 자세의 방어력 보너스입니다.\n" +
                "공격하면서도 튼튼한 방어가 가능합니다.\n" +
                "권장값: 15-30",

                // === Sword Tree: 진검승부 (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "1:1 전투에 특화된 공격속도 보너스입니다.\n" +
                "빠른 연타로 적을 압도합니다.\n" +
                "권장값: 15-30%",

                // === Sword Tree: 패링 돌격 (H키 액티브 - 5개) ===
                ["Sword_ParryCharge_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "패링 성공 후 버프가 유지되는 시간입니다.\n" +
                "이 시간 동안 강화된 공격이 가능합니다.\n" +
                "권장값: 5-10초",

                ["Sword_ParryCharge_DamageBonus"] =
                "【돌격 공격력 보너스 (%)】\n" +
                "패링 성공 시 돌격 공격의 피해 증가량입니다.\n" +
                "완벽한 타이밍에 강력한 반격을 날립니다.\n" +
                "권장값: 50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【밀어내기 거리 (m)】\n" +
                "돌격 시 적을 밀어내는 거리입니다.\n" +
                "거리 조절과 전장 제어에 유용합니다.\n" +
                "권장값: 3-7m",

                ["Sword_ParryCharge_StaminaCost"] =
                "【스태미나 소모】\n" +
                "H키 버프 활성화 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 10-20초",

                // === Sword Tree: 돌진 연속 베기 (G키 액티브 - 9개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【1차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 첫 번째 공격 피해 비율입니다.\n" +
                "기본 공격력 대비 배율입니다.\n" +
                "권장값: 80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【2차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 두 번째 공격 피해 비율입니다.\n" +
                "콤보가 이어지며 피해가 증가합니다.\n" +
                "권장값: 100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【3차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 마지막 공격 피해 비율입니다.\n" +
                "가장 강력한 피니시 공격입니다.\n" +
                "권장값: 150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【초기 돌진 거리 (m)】\n" +
                "스킬 시작 시 돌진하는 거리입니다.\n" +
                "빠르게 적에게 접근합니다.\n" +
                "권장값: 5-10m",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【측면 이동 거리 (m)】\n" +
                "연속 공격 중 좌우로 이동하는 거리입니다.\n" +
                "적의 공격을 회피하며 공격합니다.\n" +
                "권장값: 2-5m",

                ["Sword_RushSlash_StaminaCost"] =
                "【스태미나 소모량】\n" +
                "G키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "강력한 만큼 많은 스태미나가 필요합니다.\n" +
                "권장값: 40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 15-30초",

                ["Sword_RushSlash_MovementSpeed"] =
                "【이동 속도 (m/s)】\n" +
                "돌진 중 이동 속도입니다.\n" +
                "빠를수록 역동적인 전투가 가능합니다.\n" +
                "권장값: 8-15 m/s",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【공격 속도 보너스 (%)】\n" +
                "스킬 중 공격속도 보너스 (기본 대비)입니다.\n" +
                "다른 트리의 공격속도는 무시되고 이 값만 적용됩니다.\n" +
                "권장값: 20-40%",

                // === Sword Tree: 새 Tier* 형식 키 (현재 Sword_Config.cs와 일치) ===

                ["Tier0_SwordExpert_DamageBonus"] =
                "【검 피해 증가 (%)】\n" +
                "검 무기의 기본 공격력을 증가시킵니다.\n" +
                "모든 검류에 적용됩니다.\n" +
                "권장값: 10-20%",

                ["Tier0_SwordExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "검 공격속도를 증가시킵니다.\n" +
                "빠른 연속 공격이 가능합니다.\n" +
                "권장값: 10-20%",


                ["Tier2_ComboSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【연속 공격 보너스 (%)】\n" +
                "연속 공격 시 추가 피해를 입힙니다.\n" +
                "콤보를 유지하면 높은 DPS를 낼 수 있습니다.\n" +
                "권장값: 15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "연속 공격 보너스가 유지되는 시간입니다.\n" +
                "이 시간 내에 다시 공격하면 버프가 연장됩니다.\n" +
                "권장값: 3-5초",


                ["Tier3_Riposte_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier3_Riposte_DamageBonus"] =
                "【베기 공격력 보너스 (고정값)】\n" +
                "칼날 되치기 베기 공격력을 고정 수치로 증가시킵니다.\n" +
                "패링 후 강력한 반격이 가능합니다.\n" +
                "권장값: 5-15",



                ["Tier4_AllInOne_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier4_AllInOne_AttackBonus"] =
                "【공격력 보너스 (%)】\n" +
                "공격과 방어를 동시에 강화합니다.\n" +
                "균형 잡힌 전투 스타일에 유용합니다.\n" +
                "권장값: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【막기 방어력 보너스 (고정값)】\n" +
                "공방일체 자세의 막기 방어력 보너스입니다.\n" +
                "검 또는 방패 착용 시 막기 방어력에 추가됩니다.\n" +
                "권장값: 20-30",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "1:1 전투에 특화된 공격속도 보너스입니다.\n" +
                "빠른 연타로 적을 압도합니다.\n" +
                "권장값: 15-30%",


                ["Tier5_ParryRush_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier5_ParryRush_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "패링 성공 후 버프가 유지되는 시간입니다.\n" +
                "이 시간 동안 강화된 공격이 가능합니다.\n" +
                "권장값: 20-40초",

                ["Tier5_ParryRush_BlockPowerRatio"] =
                "【막기 방어력 비율 (%)】\n" +
                "패링 돌격 시 막기 방어력의 비율로 타격 데미지를 줍니다.\n" +
                "방패 또는 검의 막기 방어력 기준입니다.\n" +
                "권장값: 30-70%",

                ["Tier5_ParryRush_PushDistance"] =
                "【밀어내기 거리 (m)】\n" +
                "돌격 시 적을 밀어내는 거리입니다.\n" +
                "거리 조절과 전장 제어에 유용합니다.\n" +
                "권장값: 3-7m",

                ["Tier5_ParryRush_StaminaCost"] =
                "【스태미나 소모】\n" +
                "스킬 활성화 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 30-60초",


                ["Tier6_RushSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【1차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 첫 번째 공격 피해 비율입니다.\n" +
                "기본 공격력 대비 배율입니다.\n" +
                "권장값: 60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【2차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 두 번째 공격 피해 비율입니다.\n" +
                "콤보가 이어지며 피해가 증가합니다.\n" +
                "권장값: 70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【3차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 마지막 공격 피해 비율입니다.\n" +
                "가장 강력한 피니시 공격입니다.\n" +
                "권장값: 80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【초기 돌진 거리 (m)】\n" +
                "스킬 시작 시 돌진하는 거리입니다.\n" +
                "빠르게 적에게 접근합니다.\n" +
                "권장값: 3-8m",

                ["Tier6_RushSlash_SideDistance"] =
                "【측면 이동 거리 (m)】\n" +
                "연속 공격 중 좌우로 이동하는 거리입니다.\n" +
                "적의 공격을 회피하며 공격합니다.\n" +
                "권장값: 2-5m",

                ["Tier6_RushSlash_StaminaCost"] =
                "【스태미나 소모량】\n" +
                "G키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "강력한 만큼 많은 스태미나가 필요합니다.\n" +
                "권장값: 20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 15-30초",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【이동 속도 (m/s)】\n" +
                "돌진 중 이동 속도입니다.\n" +
                "빠를수록 역동적인 전투가 가능합니다.\n" +
                "권장값: 10-25 m/s",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【공격 속도 보너스 (%)】\n" +
                "스킬 중 공격속도 보너스 (기본 대비)입니다.\n" +
                "다른 트리의 공격속도는 무시되고 이 값만 적용됩니다.\n" +
                "권장값: 150-250%",

                ["Tier6_RushSlash_PathWidth"] =
                "【돌진 연속 베기】이동 경로 히트 너비(m).\n" +
                "경로 중 이 범위 내 몬스터를 모두 적중합니다.\n" +
                "권장값: 1-3m",

                // === Knife Tree ===
                // === Tier 0: 단검 전문가 (Knife Expert) ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【백스탭 데미지 보너스 (%)】\n" +
                "적의 뒤에서 공격 시 추가 데미지를 입힙니다.\n" +
                "암살자의 기본 기술입니다.\n" +
                "권장값: 30-50%",


                // === Tier 1: 회피 숙련 (Evasion Mastery) ===
                ["Tier1_Evasion_Chance"] =
                "【회피 확률 (%)】\n" +
                "적의 공격을 회피할 확률입니다.\n" +
                "높을수록 피격을 피할 수 있습니다.\n" +
                "권장값: 15-25%",

                ["Tier1_Evasion_Duration"] =
                "【무적 지속시간 (초)】\n" +
                "회피 성공 시 무적 상태가 유지되는 시간입니다.\n" +
                "권장값: 2-4초",


                // === Tier 2: 빠른 움직임 (Fast Movement) ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【이동 속도 보너스 (%)】\n" +
                "기본 이동 속도를 증가시킵니다.\n" +
                "빠른 기동력으로 적을 농락합니다.\n" +
                "권장값: 10-20%",


                // === Tier 3: 전투 숙련 (Combat Mastery) ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【베기/관통 공격력 보너스 (고정값)】\n" +
                "공격 시 베기와 관통 공격력에 각각 고정 데미지를 추가합니다.\n" +
                "권장값: 1-2",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "전투 숙련 버프가 유지되는 시간입니다.\n" +
                "권장값: 8-12초",


                // === Tier 4: 공격과 회피 (Attack & Evasion) ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【회피율 보너스 (%)】\n" +
                "공격과 동시에 회피율을 증가시킵니다.\n" +
                "공격적인 방어가 가능합니다.\n" +
                "권장값: 20-30%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "회피율 증가 효과가 유지되는 시간입니다.\n" +
                "권장값: 8-12초",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "버프 효과의 재사용 대기 시간입니다.\n" +
                "권장값: 25-35초",


                // === Tier 5: 치명적 피해 (Critical Damage) ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【데미지 보너스 (%)】\n" +
                "치명타 데미지를 증가시킵니다.\n" +
                "단검의 높은 크리티컬과 시너지가 좋습니다.\n" +
                "권장값: 20-35%",


                // === Tier 6: 암살자 (Assassin) ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【크리티컬 데미지 보너스 (%)】\n" +
                "치명타 공격의 피해를 더욱 증가시킵니다.\n" +
                "권장값: 20-30%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【크리티컬 확률 보너스 (%)】\n" +
                "치명타 발동 확률을 증가시킵니다.\n" +
                "권장값: 10-18%",


                // === Tier 7: 암살술 (Assassination) ===
                ["Tier7_Assassination_StaggerChance"] =
                "【스태거 확률 (%)】\n" +
                "연속 공격 시 적을 비틀거리게 할 확률입니다.\n" +
                "적의 공격을 중단시킵니다.\n" +
                "권장값: 30-45%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【필요 콤보 횟수】\n" +
                "스태거 효과 발동에 필요한 연속 공격 횟수입니다.\n" +
                "권장값: 2-4회",


                // === Tier 8: 암살자의 심장 (G키 액티브) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【크리티컬 데미지 배율】\n" +
                "G키 액티브 - 암살자의 심장 발동 시 크리티컬 데미지 배율입니다.\n" +
                "적의 뒤로 순간이동하여 치명적인 연속 공격을 합니다.\n" +
                "권장값: 1.2-1.5배",

                ["Tier8_AssassinHeart_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "암살자의 심장 버프가 유지되는 시간입니다.\n" +
                "권장값: 5-10초",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【스태미나 소모】\n" +
                "스킬 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 15-25",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 35-50초",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【순간이동 탐색 범위 (미터)】\n" +
                "적을 탐색하는 최대 거리입니다.\n" +
                "이 범위 내의 적 뒤로 순간이동합니다.\n" +
                "권장값: 6-10m",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【적 뒤 이동 거리 (미터)】\n" +
                "적의 뒤로 이동하는 거리입니다.\n" +
                "백스탭 각도를 잡기 위한 거리입니다.\n" +
                "권장값: 0.8-1.5m",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【스턴 지속시간 (초)】\n" +
                "적을 기절시키는 시간입니다.\n" +
                "권장값: 0.5-2초",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【연속 공격 횟수】\n" +
                "순간이동 후 자동으로 공격하는 횟수입니다.\n" +
                "권장값: 2-4회",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【공격 간격 (초)】\n" +
                "연속 공격 사이의 시간 간격입니다.\n" +
                "빠를수록 순식간에 공격합니다.\n" +
                "권장값: 0.2-0.5초",



                // === Knife Tree: RequiredPoints Descriptions (KO) ===
                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier1_Evasion_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier2_FastMove_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 1-3",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2-4",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2-4",

                ["Tier6_Assassin_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2-4",

                ["Tier7_Assassination_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2-4",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 스킬 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3-5",


            };
        }

        private static Dictionary<string, string> GetSwordKnifeDescriptions_EN()
        {
            return new Dictionary<string, string>
            {









                // === Sword Tree: Sword Expert (1개) ===
                ["Sword_Expert_DamageIncrease"] =
                "【Sword Damage Increase (%)】\n" +
                "Increases base damage of sword weapons.\n" +
                "Applies to all sword types.\n" +
                "Recommended: 10-20%",

                // === Sword Tree: Fast Slash (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases sword attack speed.\n" +
                "Enables rapid consecutive attacks.\n" +
                "Recommended: 10-20%",

                // === Sword Tree: Counter Stance (2개) ===
                ["Tier1_CounterStance_Duration"] =
                "【Duration (sec)】\n" +
                "Duration of the counter stance.\n" +
                "Defense increases during this time.\n" +
                "Recommended: 3-6 sec",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【Defense Bonus (%)】\n" +
                "Defense increase during counter stance.\n" +
                "Withstand enemy attacks and wait for counter opportunity.\n" +
                "Recommended: 20-40%",

                // === Sword Tree: Combo Slash (2개) ===
                ["Sword_ComboSlash_Bonus"] =
                "【Combo Attack Bonus (%)】\n" +
                "Additional damage during consecutive attacks.\n" +
                "Maintaining combos results in high DPS.\n" +
                "Recommended: 15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of the combo attack bonus.\n" +
                "Attacking again within this time extends the buff.\n" +
                "Recommended: 3-5 sec",

                // === Sword Tree: Riposte (1개) ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【Damage Bonus (Fixed Value)】\n" +
                "Increases riposte damage by a fixed amount.\n" +
                "Enables powerful counter after successful parry.\n" +
                "Recommended: 20-40",

                // === Sword Tree: Attack-Defense Unity (2개) ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【Attack Bonus (%)】\n" +
                "Enhances both attack and defense simultaneously.\n" +
                "Useful for balanced combat style.\n" +
                "Recommended: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【Block Power Bonus (Fixed Value)】\n" +
                "Block power bonus of the Attack & Defense stance.\n" +
                "Added to block power of equipped sword or shield.\n" +
                "Recommended: 20-30",

                // === Sword Tree: True Duel (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus specialized for 1v1 combat.\n" +
                "Overwhelm enemies with rapid strikes.\n" +
                "Recommended: 15-30%",

                // === Sword Tree: Parry Rush (H-Key Active - 5개) ===
                ["Sword_ParryCharge_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of the buff after successful parry.\n" +
                "Enhanced attacks are possible during this time.\n" +
                "Recommended: 5-10 sec",

                ["Sword_ParryCharge_DamageBonus"] =
                "【Rush Damage Bonus (%)】\n" +
                "Rush attack damage increase on successful parry.\n" +
                "Deliver powerful counter with perfect timing.\n" +
                "Recommended: 50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【Push Distance (m)】\n" +
                "Distance enemies are pushed back during rush.\n" +
                "Useful for distance control and battlefield management.\n" +
                "Recommended: 3-7m",

                ["Sword_ParryCharge_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on H-key buff activation.\n" +
                "Stamina management is important.\n" +
                "Recommended: 20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse delay.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 10-20 sec",

                // === Sword Tree: Rush Slash (G-Key Active - 9개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【1st Hit Damage Ratio (%)】\n" +
                "Damage ratio of the first attack in rush slash.\n" +
                "Multiplier based on base attack power.\n" +
                "Recommended: 80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【2nd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the second attack in rush slash.\n" +
                "Damage increases as the combo continues.\n" +
                "Recommended: 100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【3rd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the final attack in rush slash.\n" +
                "The most powerful finishing strike.\n" +
                "Recommended: 150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【Initial Rush Distance (m)】\n" +
                "Rush distance at skill activation.\n" +
                "Quickly close in on enemies.\n" +
                "Recommended: 5-10m",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【Side Move Distance (m)】\n" +
                "Lateral movement distance during combo.\n" +
                "Evade enemy attacks while attacking.\n" +
                "Recommended: 2-5m",

                ["Sword_RushSlash_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on G-key skill use.\n" +
                "Powerful skill requires significant stamina.\n" +
                "Recommended: 40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse delay.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 15-30 sec",

                ["Sword_RushSlash_MovementSpeed"] =
                "【Movement Speed (m/s)】\n" +
                "Movement speed during rush.\n" +
                "Faster speed enables more dynamic combat.\n" +
                "Recommended: 8-15 m/s",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus during skill (vs base).\n" +
                "Other tree bonuses are ignored, only this value applies.\n" +
                "Recommended: 20-40%",

                // === Sword Tree: New Tier* format keys (matches current Sword_Config.cs) ===

                ["Tier0_SwordExpert_DamageBonus"] =
                "【Sword Damage Increase (%)】\n" +
                "Increases base attack power of sword weapons.\n" +
                "Applies to all sword types.\n" +
                "Recommended: 10-20%",

                ["Tier0_SwordExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases sword attack speed.\n" +
                "Enables rapid consecutive attacks.\n" +
                "Recommended: 10-20%",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier2_ComboSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【Combo Attack Bonus (%)】\n" +
                "Deals additional damage on consecutive attacks.\n" +
                "Maintaining combos achieves high DPS.\n" +
                "Recommended: 15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration the combo attack bonus remains active.\n" +
                "Attacking again within this time extends the buff.\n" +
                "Recommended: 3-5 sec",


                ["Tier3_Riposte_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier3_Riposte_DamageBonus"] =
                "【Slash Damage Bonus (flat)】\n" +
                "Increases riposte slash damage by a flat amount.\n" +
                "Enables powerful counterattacks after parrying.\n" +
                "Recommended: 5-15",



                ["Tier4_AllInOne_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus specialized for 1v1 combat.\n" +
                "Overwhelm opponents with rapid strikes.\n" +
                "Recommended: 15-30%",


                ["Tier5_ParryRush_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier5_ParryRush_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration the buff remains after successful parry.\n" +
                "Enhanced attacks are possible during this time.\n" +
                "Recommended: 20-40 sec",

                ["Tier5_ParryRush_BlockPowerRatio"] =
                "【Block Power Ratio (%)】\n" +
                "Applies blunt damage equal to this ratio of block power on parry rush.\n" +
                "Based on equipped shield or sword block power.\n" +
                "Recommended: 30-70%",

                ["Tier5_ParryRush_PushDistance"] =
                "【Knockback Distance (m)】\n" +
                "Distance enemies are pushed during rush.\n" +
                "Useful for range control and battlefield positioning.\n" +
                "Recommended: 3-7m",

                ["Tier5_ParryRush_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when activating the skill.\n" +
                "Stamina management is important.\n" +
                "Recommended: 10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse wait time.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 30-60 sec",


                ["Tier6_RushSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【1st Hit Damage Ratio (%)】\n" +
                "Damage ratio of the first hit in Rush Slash.\n" +
                "Multiplier relative to base attack power.\n" +
                "Recommended: 60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【2nd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the second hit in Rush Slash.\n" +
                "Damage increases as the combo continues.\n" +
                "Recommended: 70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【3rd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the final hit in Rush Slash.\n" +
                "The most powerful finishing strike.\n" +
                "Recommended: 80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【Initial Dash Distance (m)】\n" +
                "Distance dashed when skill starts.\n" +
                "Rapidly closes in on the target.\n" +
                "Recommended: 3-8m",

                ["Tier6_RushSlash_SideDistance"] =
                "【Side Movement Distance (m)】\n" +
                "Left/right movement distance during consecutive attacks.\n" +
                "Evade enemy attacks while striking.\n" +
                "Recommended: 2-5m",

                ["Tier6_RushSlash_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using G key skill.\n" +
                "Powerful skill requires significant stamina.\n" +
                "Recommended: 20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse wait time.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 15-30 sec",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【Movement Speed (m/s)】\n" +
                "Movement speed during the rush.\n" +
                "Faster speed enables more dynamic combat.\n" +
                "Recommended: 10-25 m/s",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus during skill (vs base).\n" +
                "Other tree bonuses are ignored, only this value applies.\n" +
                "Recommended: 150-250%",

                ["Tier6_RushSlash_PathWidth"] =
                "【Rush Slash】Path hit width (m).\n" +
                "Hits all monsters within this range along the movement path.\n" +
                "Recommended: 1-3m",

                // ========================================
                // Knife Tree (Tier 0~8, 32 keys)
                // ========================================










                // === Knife Tree: Tier 0 - Knife Expert (1 key) ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【Backstab Damage Bonus (%)】\n" +
                "Additional damage when attacking from behind.\n" +
                "Fundamental technique for assassins.\n" +
                "Recommended: 30-50%",

                // === Knife Tree: Tier 1 - Evasion (2 keys) ===
                ["Tier1_Evasion_Chance"] =
                "【Evasion Chance (%)】\n" +
                "Probability of evading incoming attacks.\n" +
                "Activates automatically when attacked.\n" +
                "Recommended: 5-15%",

                ["Tier1_Evasion_Duration"] =
                "【Evasion Buff Duration (sec)】\n" +
                "Duration of evasion buff effect.\n" +
                "Provides temporary invulnerability window.\n" +
                "Recommended: 0.5-1.5 sec",

                // === Knife Tree: Tier 2 - Fast Move (1 key) ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【Movement Speed Bonus (%)】\n" +
                "Increases base movement speed.\n" +
                "Critical for assassin mobility.\n" +
                "Recommended: 10-20%",

                // === Knife Tree: Tier 3 - Combat Mastery (2 keys) ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【Slash/Pierce Damage Bonus (flat)】\n" +
                "Adds flat damage to both slash and pierce on each attack.\n" +
                "Recommended: 1-2",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of combat mastery buff.\n" +
                "Determines sustained damage window.\n" +
                "Recommended: 5-10 sec",

                // === Knife Tree: Tier 4 - Attack Evasion (3 keys) ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【Evasion Bonus (%)】\n" +
                "Additional evasion chance during attacks.\n" +
                "Stacks with base evasion.\n" +
                "Recommended: 5-10%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of attack evasion buff.\n" +
                "Active during aggressive combat.\n" +
                "Recommended: 3-8 sec",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reactivation wait time.\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 10-20 sec",

                // === Knife Tree: Tier 5 - Critical Damage (1 key) ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【Critical Damage Bonus (%)】\n" +
                "Increases critical hit damage multiplier.\n" +
                "Synergizes with critical chance.\n" +
                "Recommended: 20-40%",

                // === Knife Tree: Tier 6 - Assassin (2 keys) ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【Critical Damage Bonus (%)】\n" +
                "Additional critical damage multiplier.\n" +
                "Further enhances critical strikes.\n" +
                "Recommended: 30-50%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【Critical Chance Bonus (%)】\n" +
                "Increases critical hit probability.\n" +
                "Pairs with critical damage bonuses.\n" +
                "Recommended: 10-20%",

                // === Knife Tree: Tier 7 - Assassination (2 keys) ===
                ["Tier7_Assassination_StaggerChance"] =
                "【Stagger Chance (%)】\n" +
                "Probability of staggering enemies.\n" +
                "Interrupts enemy actions on successful combo.\n" +
                "Recommended: 30-60%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【Required Combo Hits】\n" +
                "Number of hits needed to trigger stagger.\n" +
                "Lower values mean easier activation.\n" +
                "Recommended: 3-5 hits",

                // === Knife Tree: Tier 8 - Assassin Heart (G-Key Active Skill, 10 keys) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【Critical Damage Multiplier (x)】\n" +
                "Critical damage multiplier during skill.\n" +
                "Massively boosts assassination damage.\n" +
                "Recommended: 2.0-4.0x",

                ["Tier8_AssassinHeart_Duration"] =
                "【Skill Duration (sec)】\n" +
                "Active duration of Assassin Heart.\n" +
                "Determines time window for enhanced attacks.\n" +
                "Recommended: 5-10 sec",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on activation.\n" +
                "Manage stamina carefully.\n" +
                "Recommended: 20-40",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reactivation wait time.\n" +
                "Balance power with frequency.\n" +
                "Recommended: 30-60 sec",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【Teleport Range (m)】\n" +
                "Maximum teleport distance to enemy.\n" +
                "Gap-closer for assassination.\n" +
                "Recommended: 10-20 m",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【Teleport Back Distance (m)】\n" +
                "Retreat distance after combo.\n" +
                "Safety disengagement range.\n" +
                "Recommended: 5-10 m",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【Stun Duration (sec)】\n" +
                "Enemy stun duration on teleport.\n" +
                "Allows safe combo execution.\n" +
                "Recommended: 1-3 sec",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【Combo Attack Count】\n" +
                "Number of rapid attacks in combo.\n" +
                "More hits = more damage.\n" +
                "Recommended: 5-10 hits",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【Attack Interval (sec)】\n" +
                "Time between combo attacks.\n" +
                "Shorter = faster DPS.\n" +
                "Recommended: 0.1-0.3 sec",

                // === Knife Tree: RequiredPoints Descriptions (EN) ===
                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier1_Evasion_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier2_FastMove_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 1-3",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 2-4",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 2-4",

                ["Tier6_Assassin_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 2-4",

                ["Tier7_Assassination_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 2-4",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.\n" +
                "Recommended: 3-5",


            };
        }
    }
}

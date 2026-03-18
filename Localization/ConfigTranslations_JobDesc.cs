using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJobDescriptions_KO()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Skill_Tree_Base - 키 바인딩 설명
                // ========================================
                ["HotKey_Y"] =
                "【직업 스킬 키】\n" +
                "직업 액티브 스킬을 발동하는 키입니다.\n" +
                "기본값: Y",

                ["HotKey_R"] =
                "【원거리 스킬 키】\n" +
                "원거리 액티브 스킬(멀티샷, 이중시전 등)을 발동하는 키입니다.\n" +
                "기본값: R",

                ["HotKey_G"] =
                "【근접 메인 스킬 키】\n" +
                "근접 메인 액티브 스킬(돌진 베기 등)을 발동하는 키입니다.\n" +
                "기본값: G",

                ["HotKey_H"] =
                "【보조 스킬 키】\n" +
                "보조 액티브 스킬(연공창, 수호자의 진심 등)을 발동하는 키입니다.\n" +
                "기본값: H",

                ["HUD_PosX"] =
                "【HUD X 위치】\n" +
                "액티브 스킬 HUD의 좌우 위치입니다.\n" +
                "기본값: 200 (화면 왼쪽 기준)",

                ["HUD_PosY"] =
                "【HUD Y 위치】\n" +
                "액티브 스킬 HUD의 상하 위치입니다.\n" +
                "기본값: 80 (화면 아래 기준)",

                // ========================================
                // Archer Job (Y-Key Active, 8 keys)
                // ========================================

                // === Archer Job: 멀티샷 액티브 스킬 (6개) ===
                ["Archer_MultiShot_ArrowCount"] =
                "【발사 화살 수】\n" +
                "멀티샷 시 한 번에 발사되는 화살의 개수입니다.\n" +
                "더 많은 화살로 광역 피해를 가합니다.\n" +
                "권장값: 4-7개",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【화살 소모량】\n" +
                "멀티샷 사용 시 소모되는 화살의 개수입니다.\n" +
                "적은 소모로 효율적인 공격이 가능합니다.\n" +
                "권장값: 1-2개",

                ["Archer_MultiShot_DamagePercent"] =
                "【화살당 피해 비율 (%)】\n" +
                "각 화살이 가하는 피해의 비율입니다.\n" +
                "기본 활 공격력 대비 비율입니다.\n" +
                "권장값: 40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "멀티샷 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 25-40초",

                ["Archer_MultiShot_Charges"] =
                "【발사 회수】\n" +
                "멀티샷을 연속으로 사용할 수 있는 횟수입니다.\n" +
                "여러 번 발사하여 화력을 집중할 수 있습니다.\n" +
                "권장값: 2-4회",

                ["Archer_MultiShot_StaminaCost"] =
                "【스태미나 소모】\n" +
                "멀티샷 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 20-35",

                // === Archer Job: 패시브 스킬 (2개) ===
                ["Archer_JumpHeightBonus"] =
                "【점프 높이 보너스 (%)】\n" +
                "기본 점프 높이를 증가시킵니다.\n" +
                "높은 곳에 쉽게 올라갈 수 있습니다.\n" +
                "권장값: 15-25%",

                ["Archer_FallDamageReduction"] =
                "【낙하 피해 감소 (%)】\n" +
                "높은 곳에서 떨어질 때 받는 피해를 감소시킵니다.\n" +
                "아처의 기동성을 강화합니다.\n" +
                "권장값: 40-60%",

                // === Archer Job: 레벨업 스탯 변화 (9개) ===
                ["Archer_Lv2_BonusArrows"] =
                "【Lv2: 추가 발사체 수】\n" +
                "아처 Lv2 업그레이드 시 추가되는 화살 수입니다.\n" +
                "기본 화살 수에 더해집니다.\n" +
                "권장값: 1",

                ["Archer_Lv2_DamagePercent"] =
                "【Lv2: 1발당 공격력 (%)】\n" +
                "아처 Lv2에서 각 화살의 데미지 비율입니다.\n" +
                "활+화살 총 공격력의 해당 %로 적용됩니다.\n" +
                "권장값: 50-60%",

                ["Archer_Lv3_BonusArrows"] =
                "【Lv3: 추가 발사체 수】\n" +
                "아처 Lv3 업그레이드 시 추가되는 화살 수입니다.\n" +
                "기본 화살 수에 더해집니다.\n" +
                "권장값: 2",

                ["Archer_Lv3_DamagePercent"] =
                "【Lv3: 1발당 공격력 (%)】\n" +
                "아처 Lv3에서 각 화살의 데미지 비율입니다.\n" +
                "활+화살 총 공격력의 해당 %로 적용됩니다.\n" +
                "권장값: 55-65%",

                ["Archer_Lv4_BonusArrows"] =
                "【Lv4: 추가 발사체 수】\n" +
                "아처 Lv4 업그레이드 시 추가되는 화살 수입니다.\n" +
                "기본 화살 수에 더해집니다.\n" +
                "권장값: 3",

                ["Archer_Lv4_DamagePercent"] =
                "【Lv4: 1발당 공격력 (%)】\n" +
                "아처 Lv4에서 각 화살의 데미지 비율입니다.\n" +
                "활+화살 총 공격력의 해당 %로 적용됩니다.\n" +
                "권장값: 60-70%",

                ["Archer_Lv5_BonusArrows"] =
                "【Lv5: 추가 발사체 수】\n" +
                "아처 Lv5 업그레이드 시 추가되는 화살 수입니다.\n" +
                "기본 화살 수에 더해집니다.\n" +
                "권장값: 3",

                ["Archer_Lv5_DamagePercent"] =
                "【Lv5: 1발당 공격력 (%)】\n" +
                "아처 Lv5에서 각 화살의 데미지 비율입니다.\n" +
                "활+화살 총 공격력의 해당 %로 적용됩니다.\n" +
                "권장값: 60-70%",

                ["Archer_Lv5_BonusCharges"] =
                "【Lv5: 추가 충전 횟수】\n" +
                "아처 Lv5에서 추가되는 멀티샷 충전 횟수입니다.\n" +
                "기본 충전 횟수에 더해집니다.\n" +
                "권장값: 1",

                // === Archer Job: 레벨별 패시브 추가분 (8개) ===
                ["Archer_Lv2_JumpHeightBonus"] =
                "【Lv2 패시브: 점프 높이 추가 (%)】\n" +
                "아처 Lv2 업그레이드 시 추가되는 점프 높이 보너스입니다.\n" +
                "Lv1 기본값에 더해집니다.\n" +
                "권장값: 10%",

                ["Archer_Lv3_JumpHeightBonus"] =
                "【Lv3 패시브: 점프 높이 추가 (%)】\n" +
                "아처 Lv3 업그레이드 시 추가되는 점프 높이 보너스입니다.\n" +
                "권장값: 20%",

                ["Archer_Lv4_JumpHeightBonus"] =
                "【Lv4 패시브: 점프 높이 추가 (%)】\n" +
                "아처 Lv4 업그레이드 시 추가되는 점프 높이 보너스입니다.\n" +
                "권장값: 20%",

                ["Archer_Lv5_JumpHeightBonus"] =
                "【Lv5 패시브: 점프 높이 추가 (%)】\n" +
                "아처 Lv5 업그레이드 시 추가되는 점프 높이 보너스입니다.\n" +
                "권장값: 20%",

                ["Archer_Lv3_FallDamageReduction"] =
                "【Lv3 패시브: 낙하 데미지 감소 추가 (%)】\n" +
                "아처 Lv3 업그레이드 시 추가되는 낙하 데미지 감소량입니다.\n" +
                "Lv1 기본값에 더해집니다.\n" +
                "권장값: 10%",

                ["Archer_Lv4_FallDamageReduction"] =
                "【Lv4 패시브: 낙하 데미지 감소 추가 (%)】\n" +
                "아처 Lv4 업그레이드 시 추가되는 낙하 데미지 감소량입니다.\n" +
                "권장값: 20%",

                ["Archer_Lv5_FallDamageReduction"] =
                "【Lv5 패시브: 낙하 데미지 감소 추가 (%)】\n" +
                "아처 Lv5 업그레이드 시 추가되는 낙하 데미지 감소량입니다.\n" +
                "권장값: 35%",

                ["Archer_ElementalResistPerLevel"] =
                "【패시브: 레벨당 속성 저항 (%)】\n" +
                "아처 레벨업마다 추가되는 속성 저항 기본값입니다.\n" +
                "독(Lv2+), 냉기(Lv3+), 화염(Lv4+), 번개(Lv5) 저항에 적용.\n" +
                "권장값: 10%",

                // ========================================
                // Mage Job (Y-Key Active, 6 keys)
                // ========================================

                // === Mage Job: AOE 액티브 스킬 (5개) ===
                ["Mage_AOE_Range"] =
                "【AOE 범위 (m)】\n" +
                "광역 마법 공격의 범위입니다.\n" +
                "넓은 범위로 다수의 적을 공격합니다.\n" +
                "권장값: 10-15m",

                ["Mage_Eitr_Cost"] =
                "【Eitr 소모량】\n" +
                "스킬 사용 시 소모되는 Eitr입니다.\n" +
                "마법 자원 관리가 중요합니다.\n" +
                "권장값: 30-45",

                ["Mage_Damage_Multiplier"] =
                "【공격 피해 배율 (%)】\n" +
                "광역 마법 공격의 피해 배율입니다.\n" +
                "강력한 폭발 마법으로 적을 섬멸합니다.\n" +
                "권장값: 250-350%",

                ["Mage_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "강력한 스킬이므로 긴 쿨타임을 가집니다.\n" +
                "권장값: 150-200초",

                ["Mage_AOE_Max_Targets"] =
                "【최대 타겟 수】\n" +
                "AOE 스킬이 동시에 공격할 수 있는 최대 몬스터 수입니다.\n" +
                "가까운 순서로 선택됩니다. 높을수록 랙이 발생할 수 있습니다.\n" +
                "권장값: 4-8마리",

                // === Mage Job: 패시브 스킬 (1개) ===
                ["Mage_Elemental_Resistance"] =
                "【마법 속성 저항 (%)】\n" +
                "화염, 냉기, 번개, 독, 영혼 속성 저항을 증가시킵니다.\n" +
                "물리 피해는 제외되며 마법 피해만 감소합니다.\n" +
                "권장값: 12-20%",

                // === Berserker Job: 패시브 스킬 체력 보너스 ===
                ["berserker_passive_health_bonus"] =
                "【최대 체력 보너스 (%)】\n" +
                "버서커 패시브: 최대 체력을 증가시킵니다.\n" +
                "발헤임 기본 체력 + MMO 스탯 효과 + 모든 체력 증감의 총합 기준으로 비율 적용.\n" +
                "힐링 정상 작동 (m_baseHP에 포함).\n" +
                "권장값: 100%",

                // ========================================
                // Tanker Job Skills (탱커 직업 스킬)
                // ========================================

                // === Tanker Job: 전장의 함성 액티브 (9개) ===
                ["Tanker_Taunt_Cooldown"] =
                "【전장의 함성 쿨타임 (초)】\n" +
                "전장의 함성 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 45-90초",

                ["Tanker_Taunt_StaminaCost"] =
                "【전장의 함성 스태미나 소모】\n" +
                "전장의 함성 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 20-30",

                ["Tanker_Taunt_Range"] =
                "【전장의 함성 도발 범위 (m)】\n" +
                "도발 효과가 적용되는 주변 범위입니다.\n" +
                "권장값: 10-15m",

                ["Tanker_Taunt_Duration"] =
                "【일반 몬스터 도발 지속시간 (초)】\n" +
                "일반 몬스터에게 도발 효과가 지속되는 시간입니다.\n" +
                "권장값: 4-8초",

                ["Tanker_Taunt_BossDuration"] =
                "【보스 도발 지속시간 (초)】\n" +
                "보스 몬스터에게 도발 효과가 지속되는 시간입니다.\n" +
                "보스는 일반보다 짧게 적용됩니다.\n" +
                "권장값: 1-3초",

                ["Tanker_Taunt_DamageReduction"] =
                "【자신 피해 감소 (%)】\n" +
                "전장의 함성 발동 시 자신이 받는 피해 감소량입니다.\n" +
                "권장값: 15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【피해 감소 버프 지속시간 (초)】\n" +
                "피해 감소 버프가 지속되는 시간입니다.\n" +
                "권장값: 4-8초",

                ["Tanker_Taunt_EffectHeight"] =
                "【도발 효과 표시 높이 (m)】\n" +
                "몬스터 머리 위에 표시되는 도발 아이콘의 높이입니다.\n" +
                "권장값: 1.5-2.5m",

                ["Tanker_Taunt_EffectScale"] =
                "【도발 효과 크기 배율】\n" +
                "도발 아이콘의 크기 배율입니다.\n" +
                "권장값: 0.2-0.5",

                // === Tanker Job: 패시브 (1개) ===
                ["Tanker_Passive_DamageReduction"] =
                "【탱커 패시브 피해 감소 (%)】\n" +
                "탱커 직업 패시브: 항상 받는 피해를 감소시킵니다.\n" +
                "권장값: 10-20%",

                // ========================================
                // Rogue Job Skills (로그 직업 스킬)
                // ========================================

                // === Rogue Job: 그림자 일격 액티브 (7개) ===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【그림자 일격 쿨타임 (초)】\n" +
                "그림자 일격 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 20-40초",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【그림자 일격 스태미나 소모】\n" +
                "그림자 일격 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【그림자 일격 공격력 증가 (%)】\n" +
                "그림자 일격 발동 후 버프 지속시간 동안 증가하는 공격력입니다.\n" +
                "권장값: 25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【공격력 버프 지속시간 (초)】\n" +
                "공격력 증가 버프가 지속되는 시간입니다.\n" +
                "권장값: 6-12초",

                ["Rogue_Poison_Range"] =
                "【독 폭발 범위 (m)】\n" +
                "각 독 폭발의 영향 범위입니다.\n" +
                "권장값: 8-15m",

                ["Rogue_Poison_InstantDamage"] =
                "【즉시 독데미지】\n" +
                "VFX 1회마다 적에게 가하는 즉시 독데미지입니다.\n" +
                "권장값: 8-20",

                ["Rogue_Poison_DotDamage"] =
                "【독 DoT 초당 데미지】\n" +
                "독 지속 데미지(DoT)의 초당 피해량입니다.\n" +
                "권장값: 3-8",

                ["Rogue_Poison_DotDuration"] =
                "【독 DoT 지속시간 (초)】\n" +
                "독 지속 데미지가 유지되는 시간입니다.\n" +
                "권장값: 8-15초",

                ["Rogue_Poison_VFXCount"] =
                "【독 폭발 횟수】\n" +
                "스킬 시전 시 독 폭발 VFX 반복 횟수입니다.\n" +
                "권장값: 6-10",

                ["Rogue_Poison_VFXInterval"] =
                "【독 폭발 간격 (초)】\n" +
                "각 독 폭발 사이의 시간 간격입니다.\n" +
                "권장값: 0.3-1.0초",

                // === Rogue Job: Lv2~5 쿨다운 ===
                ["Rogue_Lv2_Cooldown"] = "【Lv2 그림자 일격 쿨다운 (초)】\n권장값: 25-30초",
                ["Rogue_Lv3_Cooldown"] = "【Lv3 그림자 일격 쿨다운 (초)】\n권장값: 22-28초",
                ["Rogue_Lv4_Cooldown"] = "【Lv4 그림자 일격 쿨다운 (초)】\n권장값: 20-26초",
                ["Rogue_Lv5_Cooldown"] = "【Lv5 그림자 일격 쿨다운 (초)】\n권장값: 18-24초",

                // === Rogue Job: Lv2~5 공격력 버프 ===
                ["Rogue_Lv2_AttackBonus"] = "【Lv2 공격력 버프 (%)】\n권장값: 35-50%",
                ["Rogue_Lv3_AttackBonus"] = "【Lv3 공격력 버프 (%)】\n권장값: 40-55%",
                ["Rogue_Lv4_AttackBonus"] = "【Lv4 공격력 버프 (%)】\n권장값: 45-60%",
                ["Rogue_Lv5_AttackBonus"] = "【Lv5 공격력 버프 (%)】\n권장값: 50-65%",

                // === Rogue Job: Lv2~5 버프 지속시간 ===
                ["Rogue_Lv2_BuffDuration"] = "【Lv2 버프 지속시간 (초)】\n권장값: 8-12초",
                ["Rogue_Lv3_BuffDuration"] = "【Lv3 버프 지속시간 (초)】\n권장값: 9-13초",
                ["Rogue_Lv4_BuffDuration"] = "【Lv4 버프 지속시간 (초)】\n권장값: 10-14초",
                ["Rogue_Lv5_BuffDuration"] = "【Lv5 버프 지속시간 (초)】\n권장값: 11-15초",

                // === Rogue Job: Lv2~5 독 폭발 횟수 ===
                ["Rogue_Lv2_PoisonBlasts"] = "【Lv2 독 폭발 횟수】\n권장값: 8-12",
                ["Rogue_Lv3_PoisonBlasts"] = "【Lv3 독 폭발 횟수】\n권장값: 9-13",
                ["Rogue_Lv4_PoisonBlasts"] = "【Lv4 독 폭발 횟수】\n권장값: 10-14",
                ["Rogue_Lv5_PoisonBlasts"] = "【Lv5 독 폭발 횟수】\n권장값: 11-15",

                // === Rogue Job: Lv2~5 독 즉시 데미지 ===
                ["Rogue_Lv2_PoisonInstant"] = "【Lv2 즉시 독데미지】\n권장값: 10-15",
                ["Rogue_Lv3_PoisonInstant"] = "【Lv3 즉시 독데미지】\n권장값: 12-18",
                ["Rogue_Lv4_PoisonInstant"] = "【Lv4 즉시 독데미지】\n권장값: 14-20",
                ["Rogue_Lv5_PoisonInstant"] = "【Lv5 즉시 독데미지】\n권장값: 16-25",

                // === Rogue Job: Lv2~5 독 DoT ===
                ["Rogue_Lv2_PoisonDot"] = "【Lv2 독 DoT 초당 데미지】\n권장값: 5-8",
                ["Rogue_Lv3_PoisonDot"] = "【Lv3 독 DoT 초당 데미지】\n권장값: 6-9",
                ["Rogue_Lv4_PoisonDot"] = "【Lv4 독 DoT 초당 데미지】\n권장값: 7-10",
                ["Rogue_Lv5_PoisonDot"] = "【Lv5 독 DoT 초당 데미지】\n권장값: 8-12",

                // === Rogue Job: 충전 시스템 ===
                ["Rogue_ShadowStrike_Charges"] = "【그림자 일격 기본 충전 횟수】\n기본 충전 가능 횟수입니다.\n권장값: 1",
                ["Rogue_Lv5_BonusCharges"] = "【Lv5 추가 충전 횟수】\nLv5 달성 시 추가되는 충전 횟수입니다.\n권장값: 1",

                // === Rogue Job: 패시브 (3개) ===
                ["Rogue_AttackSpeed_Bonus"] =
                "【공격 속도 보너스 (%)】\n" +
                "로그 직업 패시브: 공격 속도를 증가시킵니다.\n" +
                "권장값: 8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【공격 스태미나 사용 감소 (%)】\n" +
                "로그 직업 패시브: 공격 시 스태미나 소모를 감소시킵니다.\n" +
                "권장값: 10-20%",

                ["Rogue_ElementalResistance_Debuff"] =
                "【속성 저항 증가 (%)】\n" +
                "로그 직업 패시브: 속성 피해에 대한 저항을 증가시킵니다.\n" +
                "권장값: 8-15%",

                // === Rogue Job: 패시브 Lv2~5 성장 ===
                ["Rogue_Lv2_AttackSpeed"] = "【Lv2 공격속도 보너스 (%)】\n권장값: 10-15%",
                ["Rogue_Lv3_AttackSpeed"] = "【Lv3 공격속도 보너스 (%)】\n권장값: 12-18%",
                ["Rogue_Lv4_AttackSpeed"] = "【Lv4 공격속도 보너스 (%)】\n권장값: 14-20%",
                ["Rogue_Lv5_AttackSpeed"] = "【Lv5 공격속도 보너스 (%)】\n권장값: 16-22%",

                ["Rogue_Lv2_StaminaReduction"] = "【Lv2 스태미나 감소 (%)】\n권장값: 15-20%",
                ["Rogue_Lv3_StaminaReduction"] = "【Lv3 스태미나 감소 (%)】\n권장값: 17-22%",
                ["Rogue_Lv4_StaminaReduction"] = "【Lv4 스태미나 감소 (%)】\n권장값: 19-25%",
                ["Rogue_Lv5_StaminaReduction"] = "【Lv5 스태미나 감소 (%)】\n권장값: 22-30%",

                ["Rogue_Lv2_ElementalResist"] = "【Lv2 속성 저항 (%)】\n권장값: 10-15%",
                ["Rogue_Lv3_ElementalResist"] = "【Lv3 속성 저항 (%)】\n권장값: 12-18%",
                ["Rogue_Lv4_ElementalResist"] = "【Lv4 속성 저항 (%)】\n권장값: 14-20%",
                ["Rogue_Lv5_ElementalResist"] = "【Lv5 속성 저항 (%)】\n권장값: 16-25%",

                ["Rogue_Lv2_MoveSpeed"] = "【Lv2 이동속도 보너스 (%)】\n권장값: 2-5%",
                ["Rogue_Lv3_MoveSpeed"] = "【Lv3 이동속도 보너스 (%)】\n권장값: 4-7%",
                ["Rogue_Lv4_MoveSpeed"] = "【Lv4 이동속도 보너스 (%)】\n권장값: 6-10%",
                ["Rogue_Lv5_MoveSpeed"] = "【Lv5 이동속도 보너스 (%)】\n권장값: 8-12%",

                // ========================================
                // Paladin Job Skills (성기사 직업 스킬)
                // ========================================

                // === Paladin Job: 신성한 치유 액티브 (8개) ===
                ["Paladin_Active_Cooldown"] =
                "【신성한 치유 쿨타임 (초)】\n" +
                "신성한 치유 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 20-45초",

                ["Paladin_Active_Range"] =
                "【신성한 치유 범위 (m)】\n" +
                "아군 힐링이 적용되는 주변 범위입니다.\n" +
                "권장값: 4-8m",

                ["Paladin_Active_EitrCost"] =
                "【신성한 치유 에이트르 소모량】\n" +
                "신성한 치유 사용 시 소모되는 에이트르입니다.\n" +
                "권장값: 8-15",

                ["Paladin_Active_StaminaCost"] =
                "【신성한 치유 스태미나 소모량】\n" +
                "신성한 치유 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【자가 치유 비율 (최대 체력의 %)】\n" +
                "스킬 발동 시 자신이 회복하는 체력 비율입니다.\n" +
                "권장값: 10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【아군 지속 치유 비율 (최대 체력의 %, 매초)】\n" +
                "범위 내 아군에게 매초 적용되는 지속 힐 비율입니다.\n" +
                "권장값: 1-3%",

                ["Paladin_Active_Duration"] =
                "【지속 치유 시간 (초)】\n" +
                "아군 지속 힐링이 적용되는 총 시간입니다.\n" +
                "권장값: 8-15초",

                ["Paladin_Active_Interval"] =
                "【지속 치유 간격 (초)】\n" +
                "지속 힐링이 적용되는 주기입니다.\n" +
                "권장값: 1초",

                // === Paladin Job: 패시브 (1개) ===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【물리 및 속성 저항 감소 (%)】\n" +
                "성기사 직업 패시브: 물리 및 속성 피해에 대한 저항을 증가시킵니다.\n" +
                "권장값: 5-12%",

                // ========================================
                // Berserker Job Skills (버서커 직업 스킬)
                // ========================================

                // === Berserker Job: 버서커의 분노 액티브 (6개, Beserker 오타 유지) ===
                ["Beserker_Active_Cooldown"] =
                "【버서커의 분노 쿨타임 (초)】\n" +
                "버서커의 분노 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 30-60초",

                ["Beserker_Active_StaminaCost"] =
                "【버서커의 분노 스태미나 소모】\n" +
                "버서커의 분노 발동 시 소모되는 스태미나입니다.\n" +
                "권장값: 15-25",

                ["Beserker_Active_Duration"] =
                "【버서커의 분노 지속시간 (초)】\n" +
                "버서커의 분노 버프가 지속되는 시간입니다.\n" +
                "권장값: 15-25초",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【HP 1%당 피해 증가 (%)】\n" +
                "현재 HP가 낮을수록 더 높은 피해 보너스를 얻습니다.\n" +
                "잃은 HP % × 이 값 = 피해 보너스\n" +
                "권장값: 1.5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【최대 피해 보너스 상한선 (%)】\n" +
                "HP 연동 피해 보너스의 최대 한도입니다.\n" +
                "권장값: 150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【효과 발동 HP 임계값 (%)】\n" +
                "이 HP% 이하일 때 HP 연동 피해 보너스가 활성화됩니다.\n" +
                "100%로 설정하면 항상 활성화됩니다.\n" +
                "권장값: 50-100%",

                // === Berserker Job: 죽음의 도전 패시브 (3개, Beserker 오타 유지) ===
                ["Berserker_Passive_HealthThreshold"] =
                "【패시브 발동 HP 임계값 (%)】\n" +
                "이 HP% 이하로 떨어지면 무적 효과가 발동됩니다.\n" +
                "권장값: 8-15%",

                ["Berserker_Passive_InvincibilityDuration"] =
                "【무적 지속시간 (초)】\n" +
                "패시브 발동 시 무적 상태가 지속되는 시간입니다.\n" +
                "권장값: 5-10초",

                ["Berserker_Passive_Cooldown"] =
                "【패시브 스킬 쿨타임 (초)】\n" +
                "패시브 무적 효과의 재발동 대기시간입니다.\n" +
                "기본값: 180초 (3분)\n" +
                "권장값: 120-300초",

                // === Berserker Job: 패시브 HP 보너스 (대소문자 수정 키) ===
                ["Berserker_Passive_HealthBonus"] =
                "【최대 체력 보너스 (%)】\n" +
                "버서커 패시브: 최대 체력을 증가시킵니다.\n" +
                "권장값: 100%",
            };
        }

        private static Dictionary<string, string> GetJobDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Skill_Tree_Base - Key Binding Descriptions
                // ========================================
                ["HotKey_Y"] =
                "【Job Skill Key】\n" +
                "Key to activate your job's active skill.\n" +
                "Default: Y",

                ["HotKey_R"] =
                "【Ranged Skill Key】\n" +
                "Key to activate ranged active skills (Multishot, Dual Cast, etc.).\n" +
                "Default: R",

                ["HotKey_G"] =
                "【Melee Main Skill Key】\n" +
                "Key to activate melee main active skills (Rush Slash, etc.).\n" +
                "Default: G",

                ["HotKey_H"] =
                "【Secondary Skill Key】\n" +
                "Key to activate secondary active skills (Combo Spear, Guardian Heart, etc.).\n" +
                "Default: H",

                ["HUD_PosX"] =
                "【HUD X Position】\n" +
                "Horizontal position of the active skill HUD.\n" +
                "Default: 200 (from screen left)",

                ["HUD_PosY"] =
                "【HUD Y Position】\n" +
                "Vertical position of the active skill HUD.\n" +
                "Default: 80 (from screen bottom)",

                // ========================================
                // Archer Job (Y-Key Active, 8 keys)
                // ========================================

                // === Archer Job: Multishot Active Skill (6 keys) ===
                ["Archer_MultiShot_ArrowCount"] =
                "【Arrows to Fire】\n" +
                "Number of arrows fired in a single multishot.\n" +
                "More arrows for area damage.\n" +
                "Recommended: 4-7",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【Arrow Consumption】\n" +
                "Number of arrows consumed per multishot.\n" +
                "Low consumption for efficient attacks.\n" +
                "Recommended: 1-2",

                ["Archer_MultiShot_DamagePercent"] =
                "【Damage Per Arrow (%)】\n" +
                "Damage ratio for each arrow.\n" +
                "Percentage of base bow damage.\n" +
                "Recommended: 40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Multishot reactivation wait time.\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 25-40 sec",

                ["Archer_MultiShot_Charges"] =
                "【Shot Charges】\n" +
                "Number of consecutive multishot uses.\n" +
                "Multiple shots to concentrate firepower.\n" +
                "Recommended: 2-4",

                ["Archer_MultiShot_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using multishot.\n" +
                "Stamina management is important.\n" +
                "Recommended: 20-35",

                // === Archer Job: Passive Skills (2 keys) ===
                ["Archer_JumpHeightBonus"] =
                "【Jump Height Bonus (%)】\n" +
                "Increases base jump height.\n" +
                "Easily reach higher positions.\n" +
                "Recommended: 15-25%",

                ["Archer_FallDamageReduction"] =
                "【Fall Damage Reduction (%)】\n" +
                "Reduces damage from falling.\n" +
                "Enhances archer mobility.\n" +
                "Recommended: 40-60%",

                // === Archer Job: Level-up Stat Changes (9 keys) ===
                ["Archer_Lv2_BonusArrows"] =
                "【Lv2: Bonus Arrow Count】\n" +
                "Additional arrows added at Archer Lv2.\n" +
                "Added on top of the base arrow count.\n" +
                "Recommended: 1",

                ["Archer_Lv2_DamagePercent"] =
                "【Lv2: Damage Per Arrow (%)】\n" +
                "Damage ratio per arrow at Archer Lv2.\n" +
                "Applied as % of total bow+arrow damage.\n" +
                "Recommended: 50-60%",

                ["Archer_Lv3_BonusArrows"] =
                "【Lv3: Bonus Arrow Count】\n" +
                "Additional arrows added at Archer Lv3.\n" +
                "Added on top of the base arrow count.\n" +
                "Recommended: 2",

                ["Archer_Lv3_DamagePercent"] =
                "【Lv3: Damage Per Arrow (%)】\n" +
                "Damage ratio per arrow at Archer Lv3.\n" +
                "Applied as % of total bow+arrow damage.\n" +
                "Recommended: 55-65%",

                ["Archer_Lv4_BonusArrows"] =
                "【Lv4: Bonus Arrow Count】\n" +
                "Additional arrows added at Archer Lv4.\n" +
                "Added on top of the base arrow count.\n" +
                "Recommended: 3",

                ["Archer_Lv4_DamagePercent"] =
                "【Lv4: Damage Per Arrow (%)】\n" +
                "Damage ratio per arrow at Archer Lv4.\n" +
                "Applied as % of total bow+arrow damage.\n" +
                "Recommended: 60-70%",

                ["Archer_Lv5_BonusArrows"] =
                "【Lv5: Bonus Arrow Count】\n" +
                "Additional arrows added at Archer Lv5.\n" +
                "Added on top of the base arrow count.\n" +
                "Recommended: 3",

                ["Archer_Lv5_DamagePercent"] =
                "【Lv5: Damage Per Arrow (%)】\n" +
                "Damage ratio per arrow at Archer Lv5.\n" +
                "Applied as % of total bow+arrow damage.\n" +
                "Recommended: 60-70%",

                ["Archer_Lv5_BonusCharges"] =
                "【Lv5: Bonus Charges】\n" +
                "Additional multishot charges added at Archer Lv5.\n" +
                "Added on top of the base charge count.\n" +
                "Recommended: 1",

                // === Archer Job: Level-based Passive Additions (8 keys) ===
                ["Archer_Lv2_JumpHeightBonus"] =
                "【Lv2 Passive: Jump Height Bonus (%)】\n" +
                "Additional jump height bonus at Archer Lv2.\n" +
                "Added on top of the Lv1 base value.\n" +
                "Recommended: 10%",

                ["Archer_Lv3_JumpHeightBonus"] =
                "【Lv3 Passive: Jump Height Bonus (%)】\n" +
                "Additional jump height bonus at Archer Lv3.\n" +
                "Recommended: 20%",

                ["Archer_Lv4_JumpHeightBonus"] =
                "【Lv4 Passive: Jump Height Bonus (%)】\n" +
                "Additional jump height bonus at Archer Lv4.\n" +
                "Recommended: 20%",

                ["Archer_Lv5_JumpHeightBonus"] =
                "【Lv5 Passive: Jump Height Bonus (%)】\n" +
                "Additional jump height bonus at Archer Lv5.\n" +
                "Recommended: 20%",

                ["Archer_Lv3_FallDamageReduction"] =
                "【Lv3 Passive: Fall Damage Reduction (%)】\n" +
                "Additional fall damage reduction at Archer Lv3.\n" +
                "Added on top of the Lv1 base value.\n" +
                "Recommended: 10%",

                ["Archer_Lv4_FallDamageReduction"] =
                "【Lv4 Passive: Fall Damage Reduction (%)】\n" +
                "Additional fall damage reduction at Archer Lv4.\n" +
                "Recommended: 20%",

                ["Archer_Lv5_FallDamageReduction"] =
                "【Lv5 Passive: Fall Damage Reduction (%)】\n" +
                "Additional fall damage reduction at Archer Lv5.\n" +
                "Recommended: 35%",

                ["Archer_ElementalResistPerLevel"] =
                "【Passive: Elemental Resist Per Level (%)】\n" +
                "Base elemental resistance gained per archer level.\n" +
                "Poison(Lv2+), Cold(Lv3+), Fire(Lv4+), Lightning(Lv5).\n" +
                "Recommended: 10%",

                // ========================================
                // Mage Job (Y-Key Active, 6 keys)
                // ========================================

                // === Mage Job: AOE Active Skill (5 keys) ===
                ["Mage_AOE_Range"] =
                "【AOE Range (m)】\n" +
                "Range of area-of-effect magic attack.\n" +
                "Wide range to hit multiple enemies.\n" +
                "Recommended: 10-15 m",

                ["Mage_Eitr_Cost"] =
                "【Eitr Cost】\n" +
                "Eitr consumed when using skill.\n" +
                "Magic resource management is important.\n" +
                "Recommended: 30-45",

                ["Mage_Damage_Multiplier"] =
                "【Damage Multiplier (%)】\n" +
                "Damage multiplier for AOE magic attack.\n" +
                "Powerful explosive magic to annihilate enemies.\n" +
                "Recommended: 250-350%",

                ["Mage_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reactivation wait time.\n" +
                "Long cooldown due to powerful effect.\n" +
                "Recommended: 150-200 sec",

                ["Mage_AOE_Max_Targets"] =
                "[Max Target Count]\n" +
                "Maximum number of monsters the AOE skill can hit simultaneously.\n" +
                "Targets are selected by proximity. Higher values may cause lag.\n" +
                "Recommended: 4-8 targets",

                // === Mage Job: Passive Skill (1 key) ===
                ["Mage_Elemental_Resistance"] =
                "【Elemental Resistance (%)】\n" +
                "Increases resistance to Fire, Frost, Lightning, Poison, and Spirit.\n" +
                "Physical damage excluded, only reduces magic damage.\n" +
                "Recommended: 12-20%",

                // === Berserker Job: Passive HP Bonus ===
                ["berserker_passive_health_bonus"] =
                "【Max HP Bonus (%)】\n" +
                "Berserker Passive: Increases maximum health.\n" +
                "Applied as a percentage of total HP (base + MMO stats + all bonuses).\n" +
                "Healing works correctly (included in m_baseHP).\n" +
                "Recommended: 100%",

                // ========================================
                // Tanker Job Skills
                // ========================================

                // === Tanker Job: War Cry Active (9 keys) ===
                ["Tanker_Taunt_Cooldown"] =
                "【War Cry Cooldown (sec)】\n" +
                "Cooldown time before War Cry can be used again.\n" +
                "Recommended: 45-90 sec",

                ["Tanker_Taunt_StaminaCost"] =
                "【War Cry Stamina Cost】\n" +
                "Stamina consumed when activating War Cry.\n" +
                "Recommended: 20-30",

                ["Tanker_Taunt_Range"] =
                "【War Cry Taunt Range (m)】\n" +
                "Radius in which enemies are taunted.\n" +
                "Recommended: 10-15m",

                ["Tanker_Taunt_Duration"] =
                "【Normal Monster Taunt Duration (sec)】\n" +
                "Duration of taunt effect on regular monsters.\n" +
                "Recommended: 4-8 sec",

                ["Tanker_Taunt_BossDuration"] =
                "【Boss Taunt Duration (sec)】\n" +
                "Duration of taunt effect on boss monsters.\n" +
                "Shorter than normal monsters due to boss resistance.\n" +
                "Recommended: 1-3 sec",

                ["Tanker_Taunt_DamageReduction"] =
                "【Self Damage Reduction (%)】\n" +
                "Incoming damage reduction while War Cry buff is active.\n" +
                "Recommended: 15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【Damage Reduction Buff Duration (sec)】\n" +
                "Duration of the damage reduction buff after activation.\n" +
                "Recommended: 4-8 sec",

                ["Tanker_Taunt_EffectHeight"] =
                "【Taunt Effect Height (m)】\n" +
                "Height above monster where the taunt icon appears.\n" +
                "Recommended: 1.5-2.5m",

                ["Tanker_Taunt_EffectScale"] =
                "【Taunt Effect Scale】\n" +
                "Size multiplier for the taunt icon visual effect.\n" +
                "Recommended: 0.2-0.5",

                // === Tanker Job: Passive (1 key) ===
                ["Tanker_Passive_DamageReduction"] =
                "【Tanker Passive Damage Reduction (%)】\n" +
                "Tanker passive: Permanently reduces incoming damage.\n" +
                "Recommended: 10-20%",

                // ========================================
                // Rogue Job Skills
                // ========================================

                // === Rogue Job: Shadow Strike Active (7 keys) ===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【Shadow Strike Cooldown (sec)】\n" +
                "Cooldown time before Shadow Strike can be used again.\n" +
                "Recommended: 20-40 sec",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【Shadow Strike Stamina Cost】\n" +
                "Stamina consumed when activating Shadow Strike.\n" +
                "Recommended: 20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【Shadow Strike Attack Bonus (%)】\n" +
                "Attack power increase during the buff duration after activation.\n" +
                "Recommended: 25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【Attack Buff Duration (sec)】\n" +
                "Duration of the attack power increase buff.\n" +
                "Recommended: 6-12 sec",

                ["Rogue_Poison_Range"] =
                "【Poison Blast Range (m)】\n" +
                "Radius of each poison blast VFX.\n" +
                "Recommended: 8-15m",

                ["Rogue_Poison_InstantDamage"] =
                "【Instant Poison Damage】\n" +
                "Immediate poison damage dealt per VFX trigger.\n" +
                "Recommended: 8-20",

                ["Rogue_Poison_DotDamage"] =
                "【Poison DoT Damage per Second】\n" +
                "Damage per second from the poison DoT effect.\n" +
                "Recommended: 3-8",

                ["Rogue_Poison_DotDuration"] =
                "【Poison DoT Duration (sec)】\n" +
                "Duration of the poison damage over time effect.\n" +
                "Recommended: 8-15 sec",

                ["Rogue_Poison_VFXCount"] =
                "【Poison Blast Count】\n" +
                "Number of times the poison blast VFX repeats.\n" +
                "Recommended: 6-10",

                ["Rogue_Poison_VFXInterval"] =
                "【Poison Blast Interval (sec)】\n" +
                "Time between each poison blast.\n" +
                "Recommended: 0.3-1.0 sec",

                // === Rogue Job: Lv2~5 Cooldown ===
                ["Rogue_Lv2_Cooldown"] = "【Lv2 Shadow Strike Cooldown (sec)】\nRecommended: 25-30s",
                ["Rogue_Lv3_Cooldown"] = "【Lv3 Shadow Strike Cooldown (sec)】\nRecommended: 22-28s",
                ["Rogue_Lv4_Cooldown"] = "【Lv4 Shadow Strike Cooldown (sec)】\nRecommended: 20-26s",
                ["Rogue_Lv5_Cooldown"] = "【Lv5 Shadow Strike Cooldown (sec)】\nRecommended: 18-24s",

                // === Rogue Job: Lv2~5 Attack Bonus ===
                ["Rogue_Lv2_AttackBonus"] = "【Lv2 Attack Buff (%)】\nRecommended: 35-50%",
                ["Rogue_Lv3_AttackBonus"] = "【Lv3 Attack Buff (%)】\nRecommended: 40-55%",
                ["Rogue_Lv4_AttackBonus"] = "【Lv4 Attack Buff (%)】\nRecommended: 45-60%",
                ["Rogue_Lv5_AttackBonus"] = "【Lv5 Attack Buff (%)】\nRecommended: 50-65%",

                // === Rogue Job: Lv2~5 Buff Duration ===
                ["Rogue_Lv2_BuffDuration"] = "【Lv2 Buff Duration (sec)】\nRecommended: 8-12s",
                ["Rogue_Lv3_BuffDuration"] = "【Lv3 Buff Duration (sec)】\nRecommended: 9-13s",
                ["Rogue_Lv4_BuffDuration"] = "【Lv4 Buff Duration (sec)】\nRecommended: 10-14s",
                ["Rogue_Lv5_BuffDuration"] = "【Lv5 Buff Duration (sec)】\nRecommended: 11-15s",

                // === Rogue Job: Lv2~5 Poison Blasts ===
                ["Rogue_Lv2_PoisonBlasts"] = "【Lv2 Poison Blast Count】\nRecommended: 8-12",
                ["Rogue_Lv3_PoisonBlasts"] = "【Lv3 Poison Blast Count】\nRecommended: 9-13",
                ["Rogue_Lv4_PoisonBlasts"] = "【Lv4 Poison Blast Count】\nRecommended: 10-14",
                ["Rogue_Lv5_PoisonBlasts"] = "【Lv5 Poison Blast Count】\nRecommended: 11-15",

                // === Rogue Job: Lv2~5 Instant Poison ===
                ["Rogue_Lv2_PoisonInstant"] = "【Lv2 Instant Poison Damage】\nRecommended: 10-15",
                ["Rogue_Lv3_PoisonInstant"] = "【Lv3 Instant Poison Damage】\nRecommended: 12-18",
                ["Rogue_Lv4_PoisonInstant"] = "【Lv4 Instant Poison Damage】\nRecommended: 14-20",
                ["Rogue_Lv5_PoisonInstant"] = "【Lv5 Instant Poison Damage】\nRecommended: 16-25",

                // === Rogue Job: Lv2~5 Poison DoT ===
                ["Rogue_Lv2_PoisonDot"] = "【Lv2 Poison DoT per Second】\nRecommended: 5-8",
                ["Rogue_Lv3_PoisonDot"] = "【Lv3 Poison DoT per Second】\nRecommended: 6-9",
                ["Rogue_Lv4_PoisonDot"] = "【Lv4 Poison DoT per Second】\nRecommended: 7-10",
                ["Rogue_Lv5_PoisonDot"] = "【Lv5 Poison DoT per Second】\nRecommended: 8-12",

                // === Rogue Job: Charge System ===
                ["Rogue_ShadowStrike_Charges"] = "【Shadow Strike Base Charges】\nBase number of charges available.\nRecommended: 1",
                ["Rogue_Lv5_BonusCharges"] = "【Lv5 Bonus Charges】\nExtra charges unlocked at Lv5.\nRecommended: 1",

                // === Rogue Job: Passive (3 keys) ===
                ["Rogue_AttackSpeed_Bonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Rogue passive: Permanently increases attack speed.\n" +
                "Recommended: 8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【Attack Stamina Usage Reduction (%)】\n" +
                "Rogue passive: Reduces stamina cost on attacks.\n" +
                "Recommended: 10-20%",

                ["Rogue_ElementalResistance_Debuff"] =
                "【Elemental Resistance Increase (%)】\n" +
                "Rogue passive: Increases resistance to elemental damage.\n" +
                "Recommended: 8-15%",

                // === Rogue Job: Passive Lv2~5 Growth ===
                ["Rogue_Lv2_AttackSpeed"] = "【Lv2 Attack Speed Bonus (%)】\nRecommended: 10-15%",
                ["Rogue_Lv3_AttackSpeed"] = "【Lv3 Attack Speed Bonus (%)】\nRecommended: 12-18%",
                ["Rogue_Lv4_AttackSpeed"] = "【Lv4 Attack Speed Bonus (%)】\nRecommended: 14-20%",
                ["Rogue_Lv5_AttackSpeed"] = "【Lv5 Attack Speed Bonus (%)】\nRecommended: 16-22%",

                ["Rogue_Lv2_StaminaReduction"] = "【Lv2 Stamina Reduction (%)】\nRecommended: 15-20%",
                ["Rogue_Lv3_StaminaReduction"] = "【Lv3 Stamina Reduction (%)】\nRecommended: 17-22%",
                ["Rogue_Lv4_StaminaReduction"] = "【Lv4 Stamina Reduction (%)】\nRecommended: 19-25%",
                ["Rogue_Lv5_StaminaReduction"] = "【Lv5 Stamina Reduction (%)】\nRecommended: 22-30%",

                ["Rogue_Lv2_ElementalResist"] = "【Lv2 Elemental Resistance (%)】\nRecommended: 10-15%",
                ["Rogue_Lv3_ElementalResist"] = "【Lv3 Elemental Resistance (%)】\nRecommended: 12-18%",
                ["Rogue_Lv4_ElementalResist"] = "【Lv4 Elemental Resistance (%)】\nRecommended: 14-20%",
                ["Rogue_Lv5_ElementalResist"] = "【Lv5 Elemental Resistance (%)】\nRecommended: 16-25%",

                ["Rogue_Lv2_MoveSpeed"] = "【Lv2 Move Speed Bonus (%)】\nRecommended: 2-5%",
                ["Rogue_Lv3_MoveSpeed"] = "【Lv3 Move Speed Bonus (%)】\nRecommended: 4-7%",
                ["Rogue_Lv4_MoveSpeed"] = "【Lv4 Move Speed Bonus (%)】\nRecommended: 6-10%",
                ["Rogue_Lv5_MoveSpeed"] = "【Lv5 Move Speed Bonus (%)】\nRecommended: 8-12%",

                // ========================================
                // Paladin Job Skills
                // ========================================

                // === Paladin Job: Holy Healing Active (8 keys) ===
                ["Paladin_Active_Cooldown"] =
                "【Holy Healing Cooldown (sec)】\n" +
                "Cooldown time before Holy Healing can be used again.\n" +
                "Recommended: 20-45 sec",

                ["Paladin_Active_Range"] =
                "【Holy Healing Range (m)】\n" +
                "Radius in which allies receive healing.\n" +
                "Recommended: 4-8m",

                ["Paladin_Active_EitrCost"] =
                "【Holy Healing Eitr Cost】\n" +
                "Eitr consumed when activating Holy Healing.\n" +
                "Recommended: 8-15",

                ["Paladin_Active_StaminaCost"] =
                "【Holy Healing Stamina Cost】\n" +
                "Stamina consumed when activating Holy Healing.\n" +
                "Recommended: 8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【Self Heal Ratio (% of Max HP)】\n" +
                "Percentage of max HP restored to self on activation.\n" +
                "Recommended: 10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【Ally HoT Ratio (% of Max HP, per sec)】\n" +
                "Percentage of max HP restored to each ally per second.\n" +
                "Recommended: 1-3%",

                ["Paladin_Active_Duration"] =
                "【HoT Duration (sec)】\n" +
                "Total duration of the ally heal-over-time effect.\n" +
                "Recommended: 8-15 sec",

                ["Paladin_Active_Interval"] =
                "【HoT Interval (sec)】\n" +
                "Interval between each heal tick.\n" +
                "Recommended: 1 sec",

                // === Paladin Job: Passive (1 key) ===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【Physical & Elemental Resistance Bonus (%)】\n" +
                "Paladin passive: Increases resistance to physical and elemental damage.\n" +
                "Recommended: 5-12%",

                // ========================================
                // Berserker Job Skills
                // ========================================

                // === Berserker Job: Berserker Rage Active (6 keys, Beserker typo preserved) ===
                ["Beserker_Active_Cooldown"] =
                "【Berserker Rage Cooldown (sec)】\n" +
                "Cooldown time before Berserker Rage can be used again.\n" +
                "Recommended: 30-60 sec",

                ["Beserker_Active_StaminaCost"] =
                "【Berserker Rage Stamina Cost】\n" +
                "Stamina consumed when activating Berserker Rage.\n" +
                "Recommended: 15-25",

                ["Beserker_Active_Duration"] =
                "【Berserker Rage Duration (sec)】\n" +
                "Duration of the Berserker Rage buff.\n" +
                "Recommended: 15-25 sec",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【Damage Bonus per 1% HP Lost (%)】\n" +
                "Damage increases as health decreases.\n" +
                "Lost HP % × this value = damage bonus\n" +
                "Recommended: 1.5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【Max Damage Bonus Cap (%)】\n" +
                "Maximum limit for the HP-linked damage bonus.\n" +
                "Recommended: 150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【HP Threshold for Activation (%)】\n" +
                "HP-linked damage bonus activates below this HP%.\n" +
                "Set to 100% to always activate.\n" +
                "Recommended: 50-100%",

                // === Berserker Job: Death Defiance Passive (3 keys, Beserker typo preserved) ===
                ["Berserker_Passive_HealthThreshold"] =
                "【Passive Trigger HP Threshold (%)】\n" +
                "Invincibility triggers when HP falls below this percentage.\n" +
                "Recommended: 8-15%",

                ["Berserker_Passive_InvincibilityDuration"] =
                "【Invincibility Duration (sec)】\n" +
                "Duration of the invincibility effect when passive triggers.\n" +
                "Recommended: 5-10 sec",

                ["Berserker_Passive_Cooldown"] =
                "【Passive Cooldown (sec)】\n" +
                "Cooldown before the passive invincibility can trigger again.\n" +
                "Default: 180 sec (3 minutes)\n" +
                "Recommended: 120-300 sec",

                // === Berserker Job: Passive HP Bonus (case-corrected key) ===
                ["Berserker_Passive_HealthBonus"] =
                "【Max Health Bonus (%)】\n" +
                "Berserker passive: increases maximum health.\n" +
                "Recommended: 100%",

                // ========================================
                // Producer Job Skills (제작 전문가 직업)
                // ========================================
                ["Producer_Buff_Cooldown"] =
                "【장인의 축복 쿨타임 (초)】\n" +
                "제작 전문가 버프 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 120-180초",

                ["Producer_Buff_Duration"] =
                "【장인의 축복 지속시간 (초)】\n" +
                "공격력/체력 버프가 유지되는 시간입니다.\n" +
                "길수록 전투 지속력이 높아집니다.\n" +
                "권장값: 90-120초",

                ["Producer_Buff_Range"] =
                "【장인의 축복 범위 (m)】\n" +
                "파티원에게 버프가 적용되는 범위입니다.\n" +
                "넓을수록 더 멀리 있는 동료에게 적용됩니다.\n" +
                "권장값: 12-20m",

                ["Producer_Buff_AttackBonus"] =
                "【버프 공격력 보너스 (%)】\n" +
                "버프 적용 중 증가하는 공격력 비율입니다.\n" +
                "높을수록 전투 화력이 강화됩니다.\n" +
                "권장값: 10-20%",

                ["Producer_Buff_MaxHealthBonus"] =
                "【버프 최대 체력 보너스 (%)】\n" +
                "버프 적용 중 증가하는 최대 체력 비율입니다.\n" +
                "높을수록 생존력이 향상됩니다.\n" +
                "권장값: 10-20%",

                ["Producer_Buff_StaminaCost"] =
                "【버프 스태미나 소모】\n" +
                "장인의 축복 발동 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 15-25",

                // === 레벨별 패시브 ===
                ["Producer_FarmGrid_Lv1"] = "【농사 그리드 크기 Lv1】\nLv1에서 농사 그리드에 추가되는 칸 수입니다.\n기본값: 2",
                ["Producer_FarmGrid_Lv2"] = "【농사 그리드 크기 Lv2】\nLv2에서 농사 그리드에 추가되는 칸 수입니다.\n기본값: 2",
                ["Producer_FarmGrid_Lv3"] = "【농사 그리드 크기 Lv3】\nLv3에서 농사 그리드에 추가되는 칸 수입니다.\n기본값: 4",
                ["Producer_FarmGrid_Lv4"] = "【농사 그리드 크기 Lv4】\nLv4에서 농사 그리드에 추가되는 칸 수입니다.\n기본값: 6",
                ["Producer_FarmGrid_Lv5"] = "【농사 그리드 크기 Lv5】\nLv5에서 농사 그리드에 추가되는 칸 수입니다.\n기본값: 8",

                ["Producer_Durability_Lv2"] = "【제작 아이템 내구도 보너스 Lv2 (%)】\nLv2에서 제작 아이템의 내구도 증가율입니다.\n기본값: 10%",
                ["Producer_Durability_Lv3"] = "【제작 아이템 내구도 보너스 Lv3 (%)】\nLv3에서 제작 아이템의 내구도 증가율입니다.\n기본값: 15%",
                ["Producer_Durability_Lv4"] = "【제작 아이템 내구도 보너스 Lv4 (%)】\nLv4에서 제작 아이템의 내구도 증가율입니다.\n기본값: 20%",
                ["Producer_Durability_Lv5"] = "【제작 아이템 내구도 보너스 Lv5 (%)】\nLv5에서 제작 아이템의 내구도 증가율입니다.\n기본값: 30%",

                ["Producer_MaterialReduction_Lv2"] = "【제작 재료 감소 Lv2 (%)】\nLv2에서 제작 시 절약되는 재료 비율입니다.\n기본값: 10%",
                ["Producer_MaterialReduction_Lv3"] = "【제작 재료 감소 Lv3 (%)】\nLv3에서 제작 시 절약되는 재료 비율입니다.\n기본값: 15%",
                ["Producer_MaterialReduction_Lv4"] = "【제작 재료 감소 Lv4 (%)】\nLv4에서 제작 시 절약되는 재료 비율입니다.\n기본값: 20%",
                ["Producer_MaterialReduction_Lv5"] = "【제작 재료 감소 Lv5 (%)】\nLv5에서 제작 시 절약되는 재료 비율입니다.\n기본값: 30%",

                ["Producer_EnchantChance_Lv3"] = "【마법부여 확률 Lv3 (%)】\nLv3에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 25%",
                ["Producer_EnchantChance_Lv4"] = "【마법부여 확률 Lv4 (%)】\nLv4에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 30%",
                ["Producer_EnchantChance_Lv5"] = "【마법부여 확률 Lv5 (%)】\nLv5에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 35%",

                ["Producer_EnchantWeaponDmgMin_Lv3"] = "【무기 데미지 마법부여 최솟값 Lv3 (%)】\nLv3 마법부여 무기의 데미지 보너스 최솟값입니다.\n기본값: 5%",
                ["Producer_EnchantWeaponDmgMax_Lv3"] = "【무기 데미지 마법부여 최댓값 Lv3 (%)】\nLv3 마법부여 무기의 데미지 보너스 최댓값입니다.\n기본값: 5%",
                ["Producer_EnchantWeaponDmgMin_Lv4"] = "【무기 데미지 마법부여 최솟값 Lv4 (%)】\nLv4 마법부여 무기의 데미지 보너스 최솟값입니다.\n기본값: 7%",
                ["Producer_EnchantWeaponDmgMax_Lv4"] = "【무기 데미지 마법부여 최댓값 Lv4 (%)】\nLv4 마법부여 무기의 데미지 보너스 최댓값입니다.\n기본값: 9%",
                ["Producer_EnchantWeaponDmgMin_Lv5"] = "【무기 데미지 마법부여 최솟값 Lv5 (%)】\nLv5 마법부여 무기의 데미지 보너스 최솟값입니다.\n기본값: 10%",
                ["Producer_EnchantWeaponDmgMax_Lv5"] = "【무기 데미지 마법부여 최댓값 Lv5 (%)】\nLv5 마법부여 무기의 데미지 보너스 최댓값입니다.\n기본값: 12%",

                ["Producer_EnchantArmorMin_Lv3"] = "【방어구 마법부여 최솟값 Lv3 (%)】\nLv3 마법부여 방어구의 방어력 보너스 최솟값입니다.\n기본값: 5%",
                ["Producer_EnchantArmorMax_Lv3"] = "【방어구 마법부여 최댓값 Lv3 (%)】\nLv3 마법부여 방어구의 방어력 보너스 최댓값입니다.\n기본값: 5%",
                ["Producer_EnchantArmorMin_Lv4"] = "【방어구 마법부여 최솟값 Lv4 (%)】\nLv4 마법부여 방어구의 방어력 보너스 최솟값입니다.\n기본값: 7%",
                ["Producer_EnchantArmorMax_Lv4"] = "【방어구 마법부여 최댓값 Lv4 (%)】\nLv4 마법부여 방어구의 방어력 보너스 최댓값입니다.\n기본값: 9%",
                ["Producer_EnchantArmorMin_Lv5"] = "【방어구 마법부여 최솟값 Lv5 (%)】\nLv5 마법부여 방어구의 방어력 보너스 최솟값입니다.\n기본값: 10%",
                ["Producer_EnchantArmorMax_Lv5"] = "【방어구 마법부여 최댓값 Lv5 (%)】\nLv5 마법부여 방어구의 방어력 보너스 최댓값입니다.\n기본값: 12%",

                ["Producer_EnchantHpMin_Lv3"] = "【HP 마법부여 최솟값 Lv3 (%)】\nLv3 마법부여 아이템의 최대 HP 보너스 최솟값입니다.\n기본값: 2%",
                ["Producer_EnchantHpMax_Lv3"] = "【HP 마법부여 최댓값 Lv3 (%)】\nLv3 마법부여 아이템의 최대 HP 보너스 최댓값입니다.\n기본값: 2%",
                ["Producer_EnchantHpMin_Lv4"] = "【HP 마법부여 최솟값 Lv4 (%)】\nLv4 마법부여 아이템의 최대 HP 보너스 최솟값입니다.\n기본값: 4%",
                ["Producer_EnchantHpMax_Lv4"] = "【HP 마법부여 최댓값 Lv4 (%)】\nLv4 마법부여 아이템의 최대 HP 보너스 최댓값입니다.\n기본값: 5%",
                ["Producer_EnchantHpMin_Lv5"] = "【HP 마법부여 최솟값 Lv5 (%)】\nLv5 마법부여 아이템의 최대 HP 보너스 최솟값입니다.\n기본값: 6%",
                ["Producer_EnchantHpMax_Lv5"] = "【HP 마법부여 최댓값 Lv5 (%)】\nLv5 마법부여 아이템의 최대 HP 보너스 최댓값입니다.\n기본값: 8%",
            };
        }
    }
}

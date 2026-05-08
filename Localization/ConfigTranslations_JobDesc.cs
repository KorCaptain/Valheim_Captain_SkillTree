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

                ["HUD_IconSize"] =
                "【스킬 아이콘 크기】\n" +
                "액티브 스킬 HUD에 표시되는 아이콘의 크기입니다.\n" +
                "기본값: 62",

                ["HUD_PosX"] =
                "【Skill Icon HUD X 위치】\n" +
                "액티브 스킬 HUD의 좌우 위치입니다.\n" +
                "기본값: 306 (화면 왼쪽 기준)",

                ["HUD_PosY"] =
                "【Skill Icon HUD Y 위치】\n" +
                "액티브 스킬 HUD의 상하 위치입니다.\n" +
                "기본값: 139 (화면 아래 기준)",

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
                "권장값: 20-30초",

                ["Archer_MultiShot_Charges"] =
                "【발사 회수】\n" +
                "멀티샷을 연속으로 사용할 수 있는 횟수입니다.\n" +
                "여러 번 발사하여 화력을 집중할 수 있습니다.\n" +
                "권장값: 2-4회",

                ["Archer_MultiShot_StaminaCost"] =
                "【스태미나 소모】\n" +
                "멀티샷 사용 시 소모되는 스태미나입니다.\n" +
                "0으로 설정 시 스태미나 소모 없음.\n" +
                "권장값: 0-15",

                ["Archer_MultiShot_FireInterval"] =
                "【순차 발사 간격 (초)】\n" +
                "볼리샷 추가 화살 사이의 발사 간격입니다.\n" +
                "5발이 이 간격으로 순차 발사됩니다.\n" +
                "권장값: 0.15-0.3초",

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

                // === Archer Job: 신규 패시브 - 공격 스테미나 감소 (5개) ===
                ["Archer_Attack_StaminaReduction_Lv1"] =
                "【Lv1 패시브: 공격 스테미나 감소 (%)】\n" +
                "아처 Lv1에서 공격 시 소모되는 스테미나를 감소시킵니다.\n" +
                "활/석궁/지팡이 모든 공격에 적용됩니다.\n" +
                "권장값: 10-20%",

                ["Archer_Attack_StaminaReduction_Lv2"] =
                "【Lv2 패시브: 공격 스테미나 감소 (%)】\n" +
                "아처 Lv2에서 공격 시 소모되는 스테미나를 감소시킵니다.\n" +
                "권장값: 20-30%",

                ["Archer_Attack_StaminaReduction_Lv3"] =
                "【Lv3 패시브: 공격 스테미나 감소 (%)】\n" +
                "아처 Lv3에서 공격 시 소모되는 스테미나를 감소시킵니다.\n" +
                "권장값: 30-40%",

                ["Archer_Attack_StaminaReduction_Lv4"] =
                "【Lv4 패시브: 공격 스테미나 감소 (%)】\n" +
                "아처 Lv4에서 공격 시 소모되는 스테미나를 감소시킵니다.\n" +
                "권장값: 40-50%",

                ["Archer_Attack_StaminaReduction_Lv5"] =
                "【Lv5 패시브: 공격 스테미나 감소 (%)】\n" +
                "아처 Lv5에서 공격 시 소모되는 스테미나를 감소시킵니다.\n" +
                "권장값: 50-60%",

                // === Archer Job: 신규 패시브 - 화살/볼트 소모 면제 확률 ===
                ["Archer_AmmoSaveChance"] =
                "【화살/볼트 소모 면제 확률 (%)】\n" +
                "공격 시 화살 또는 볼트를 소모하지 않을 확률입니다.\n" +
                "50으로 설정 시 평균 절반의 화살만 소모됩니다.\n" +
                "권장값: 30-60%",

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
                // Mage Job - 불의 비 (Fire Rain) Y-Key Active
                // ========================================

                // === Mage Job: 액티브 스킬 고정값 ===
                ["Mage_AOE_Range"] =
                "【타겟팅 범위 (m)】\n" +
                "불의 비 타겟 탐색 범위입니다.\n" +
                "카메라 전방 이 범위 내 적을 타겟팅합니다.\n" +
                "권장값: 10-15m",

                ["Mage_Eitr_Cost"] =
                "【Eitr 소모량】\n" +
                "스킬 사용 시 소모되는 Eitr입니다.\n" +
                "권장값: 30-45",

                ["Mage_Fire_Rain_Radius"] =
                "【불의 비 낙하 반경 (m)】\n" +
                "파이어볼 30개가 타겟 주변에 낙하하는 반경입니다.\n" +
                "권장값: 6-10m",

                ["Mage_Fire_Rain_Impact_Radius"] =
                "【파이어볼 착지 데미지 범위 (m)】\n" +
                "각 파이어볼 착지 시 데미지를 입히는 범위입니다.\n" +
                "권장값: 2-4m",

                ["Mage_Fire_Rain_Projectile_Count"] =
                "【버스트당 발사체 수 (개)】\n" +
                "1회 버스트당 낙하하는 파이어볼 수입니다.\n" +
                "총 2회 버스트로 발사됩니다 (1버스트 → 1초 → 2버스트).\n" +
                "권장값: 15-25개",

                // === Mage Job: 쿨타임 (레벨별 - 기본 45초) ===
                ["Mage_Lv1_Cooldown"] =
                "【쿨타임 Lv1 (초)】\n" +
                "Lv1 불의 비 재사용 대기 시간입니다.\n" +
                "권장값: 45초",

                ["Mage_Lv2_Cooldown"] =
                "【쿨타임 Lv2 (초)】\n" +
                "Lv2 불의 비 재사용 대기 시간입니다.\n" +
                "권장값: 45초",

                ["Mage_Lv3_Cooldown"] =
                "【쿨타임 Lv3 (초)】\n" +
                "Lv3 불의 비 재사용 대기 시간입니다.\n" +
                "권장값: 45초",

                ["Mage_Lv4_Cooldown"] =
                "【쿨타임 Lv4 (초)】\n" +
                "Lv4 불의 비 재사용 대기 시간입니다.\n" +
                "권장값: 45초",

                ["Mage_Lv5_Cooldown"] =
                "【쿨타임 Lv5 (초)】\n" +
                "Lv5 불의 비 재사용 대기 시간입니다.\n" +
                "권장값: 45초",

                // === Mage Job: 패시브 속성 저항 (레벨별, 기존 유지) ===
                ["Mage_Lv1_Elemental_Resistance"] =
                "【마법 속성 저항 Lv1 (%)】\n" +
                "Lv1 메이지 속성 저항입니다. 화염/냉기/번개/독/영혼 감소.\n" +
                "권장값: 5%",

                ["Mage_Lv2_Elemental_Resistance"] =
                "【마법 속성 저항 Lv2 (%)】\n" +
                "Lv2 메이지 속성 저항입니다. 연속 발사 +1회 시전 포함.\n" +
                "권장값: 7%",

                ["Mage_Lv3_Elemental_Resistance"] =
                "【마법 속성 저항 Lv3 (%)】\n" +
                "Lv3 메이지 속성 저항입니다.\n" +
                "권장값: 9%",

                ["Mage_Lv4_Elemental_Resistance"] =
                "【마법 속성 저항 Lv4 (%)】\n" +
                "Lv4 메이지 속성 저항입니다.\n" +
                "권장값: 12%",

                ["Mage_Lv5_Elemental_Resistance"] =
                "【마법 속성 저항 Lv5 (%)】\n" +
                "Lv5 메이지 속성 저항입니다.\n" +
                "권장값: 15%",

                // === Mage Job: 공격력 배수 (무기 공격력 %) ===
                ["Mage_Lv1_Damage_Multiplier"] =
                "【공격력 배수 Lv1 (%)】\n" +
                "Lv1 파이어볼 1개당 무기 공격력 대비 데미지 비율입니다.\n" +
                "권장값: 22%",

                ["Mage_Lv2_Damage_Multiplier"] =
                "【공격력 배수 Lv2 (%)】\n" +
                "Lv2 파이어볼 1개당 무기 공격력 대비 데미지 비율입니다.\n" +
                "권장값: 24%",

                ["Mage_Lv3_Damage_Multiplier"] =
                "【공격력 배수 Lv3 (%)】\n" +
                "Lv3 파이어볼 1개당 무기 공격력 대비 데미지 비율입니다.\n" +
                "권장값: 26%",

                ["Mage_Lv4_Damage_Multiplier"] =
                "【공격력 배수 Lv4 (%)】\n" +
                "Lv4 파이어볼 1개당 무기 공격력 대비 데미지 비율입니다.\n" +
                "권장값: 28%",

                ["Mage_Lv5_Damage_Multiplier"] =
                "【공격력 배수 Lv5 (%)】\n" +
                "Lv5 파이어볼 1개당 무기 공격력 대비 데미지 비율입니다.\n" +
                "권장값: 30%",

                // === Berserker Job: 패시브 스킬 체력 보너스 ===
                ["berserker_passive_health_bonus"] =
                "【최대 체력 보너스 (%)】\n" +
                "버서커 패시브: 최대 체력을 증가시킵니다.\n" +
                "발헤임 기본 체력 + MMO 스탯 효과 + 모든 체력 증감의 총합 기준으로 비율 적용.\n" +
                "힐링 정상 작동 (m_baseHP에 포함).\n" +
                "권장값: 100%",

                // === Berserker Lv2~5 패시브 Config ===
                ["Berserker_Lv2_CooldownReduction"] =
                "【버서커 Lv2: 분노 쿨타임 감소 (초)】\n" +
                "Lv2 달성 시 분노 스킬 쿨타임이 이 수치만큼 줄어듭니다.\n" +
                "권장값: 5초",

                ["Berserker_Lv3_RageDamageReduction"] =
                "【버서커 Lv3: 분노 중 피해 감소 (%)】\n" +
                "Lv3 달성 시 분노 상태에서 받는 피해가 이 비율만큼 감소합니다.\n" +
                "권장값: 15%",

                ["Berserker_Lv4_LowHpAttackBonus"] =
                "【버서커 Lv4: 저체력 공격력 보너스 (%)】\n" +
                "Lv4 달성 시 체력이 임계값 이하일 때 공격력이 증가합니다.\n" +
                "권장값: 15%",

                ["Berserker_Lv4_LowHpAttackThreshold"] =
                "【버서커 Lv4: 저체력 공격력 발동 임계값 (%)】\n" +
                "체력이 이 비율 이하일 때 Lv4 공격력 보너스가 활성화됩니다.\n" +
                "권장값: 50%",

                ["Berserker_Lv5_PassiveCooldownReduction"] =
                "【버서커 Lv5: 죽음의 무시 쿨타임 단축 (초)】\n" +
                "Lv5 달성 시 죽음의 무시 패시브 쿨타임이 이 수치만큼 줄어듭니다.\n" +
                "권장값: 120초 (2분)",

                ["Berserker_Lv5_InvincibilityBonus"] =
                "【버서커 Lv5: 죽음의 무시 무적 시간 추가 (초)】\n" +
                "Lv5 달성 시 죽음의 무시 발동 시 무적 지속시간이 이 수치만큼 증가합니다.\n" +
                "권장값: 2초",

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

                ["Tanker_Taunt_ReflectPercent"] =
                "【도발 반사 데미지 비율 (%)】\n" +
                "전장의 함성 발동 시 피격 데미지의 일부를 공격자에게 반사합니다.\n" +
                "버프 지속시간 동안 활성화됩니다.\n" +
                "권장값: 5-20%",

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

                ["Tanker_NormalShield_SpeedBonus"] =
                "【탱커 일반 방패 이동속도 보너스 (%)】\n" +
                "탱커 Lv1+: 일반 방패 장착 시 이동속도가 증가합니다.\n" +
                "기본값: 25%",

                ["Tanker_TowerShield_SpeedBonus"] =
                "【탱커 타워 방패 이동속도 보너스 (%)】\n" +
                "탱커 Lv1+: 타워(대형) 방패 장착 시 이동속도가 증가합니다.\n" +
                "기본값: 30%",

                // === Tanker Job: 레벨별 패시브 (Lv 묶음) ===
                // --- Lv1 ---
                ["Tanker_ReflectDuration_Lv1"] =
                "【탱커 반사 지속시간 Lv1 (초)】\n" +
                "도발 발동 시 Lv1에서 반사 효과가 지속되는 시간입니다.\n" +
                "기본값: 10초",

                ["Tanker_Hp_Bonus_Lv1"] =
                "【탱커 Lv1 체력 보너스 (%)】\n" +
                "탱커 Lv1 달성 시 최대 체력이 % 비율로 증가합니다.\n" +
                "기본값: 25",

                // --- Lv2 ---
                ["Tanker_Hp_Bonus_Lv2"] =
                "【탱커 Lv2 체력 보너스 (%)】\n" +
                "탱커 Lv2 달성 시 최대 체력이 % 비율로 증가합니다.\n" +
                "기본값: 30",

                ["Tanker_Lv2_BlockPower"] =
                "【탱커 Lv2 방패 막기 방어력】\n" +
                "탱커 Lv2 달성 시 방패 막기 방어력이 증가합니다.\n" +
                "기본값: 5",

                ["Tanker_ReflectDuration_Lv2"] =
                "【탱커 반사 지속시간 Lv2 (초)】\n" +
                "도발 발동 시 Lv2에서 반사 효과가 지속되는 시간입니다.\n" +
                "기본값: 12초",

                // --- Lv3 ---
                ["Tanker_Hp_Bonus_Lv3"] =
                "【탱커 Lv3 체력 보너스 (%)】\n" +
                "탱커 Lv3 달성 시 최대 체력이 % 비율로 증가합니다.\n" +
                "기본값: 35",

                ["Tanker_Lv3_BlockPower"] =
                "【탱커 Lv3 방패 막기 방어력】\n" +
                "탱커 Lv3 달성 시 방패 막기 방어력이 증가합니다.\n" +
                "기본값: 10",

                ["Tanker_ReflectDuration_Lv3"] =
                "【탱커 반사 지속시간 Lv3 (초)】\n" +
                "도발 발동 시 Lv3에서 반사 효과가 지속되는 시간입니다.\n" +
                "기본값: 14초",

                // --- Lv4 ---
                ["Tanker_Hp_Bonus_Lv4"] =
                "【탱커 Lv4 체력 보너스 (%)】\n" +
                "탱커 Lv4 달성 시 최대 체력이 % 비율로 증가합니다.\n" +
                "기본값: 40",

                ["Tanker_Lv4_BlockPower"] =
                "【탱커 Lv4 방패 막기 방어력】\n" +
                "탱커 Lv4 달성 시 방패 막기 방어력이 증가합니다.\n" +
                "기본값: 15",

                ["Tanker_ReflectDuration_Lv4"] =
                "【탱커 반사 지속시간 Lv4 (초)】\n" +
                "도발 발동 시 Lv4에서 반사 효과가 지속되는 시간입니다.\n" +
                "기본값: 16초",

                // --- Lv5 ---
                ["Tanker_Hp_Bonus_Lv5"] =
                "【탱커 Lv5 체력 보너스 (%)】\n" +
                "탱커 Lv5 달성 시 최대 체력이 % 비율로 증가합니다.\n" +
                "기본값: 50",

                ["Tanker_Lv5_BlockPower"] =
                "【탱커 Lv5 방패 막기 방어력】\n" +
                "탱커 Lv5 달성 시 방패 막기 방어력이 증가합니다.\n" +
                "기본값: 20",

                ["Tanker_ReflectDuration_Lv5"] =
                "【탱커 반사 지속시간 Lv5 (초)】\n" +
                "도발 발동 시 Lv5에서 반사 효과가 지속되는 시간입니다.\n" +
                "기본값: 20초",

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

                ["Rogue_Lv1_DodgeChance"] =
                "【Lv1 회피율 (%)】\n" +
                "로그 직업 패시브: 적중에 대한 회피율을 증가시킵니다. 스킬트리 전체 합산 적용.\n" +
                "권장값: 3-6%",

                // === Rogue Job: 패시브 Lv2~5 성장 ===
                ["Rogue_Lv2_AttackSpeed"] = "【Lv2 공격속도 보너스 (%)】\n권장값: 10-15%",
                ["Rogue_Lv3_AttackSpeed"] = "【Lv3 공격속도 보너스 (%)】\n권장값: 12-18%",
                ["Rogue_Lv4_AttackSpeed"] = "【Lv4 공격속도 보너스 (%)】\n권장값: 14-20%",
                ["Rogue_Lv5_AttackSpeed"] = "【Lv5 공격속도 보너스 (%)】\n권장값: 16-22%",

                ["Rogue_Lv2_StaminaReduction"] = "【Lv2 스태미나 감소 (%)】\n권장값: 15-20%",
                ["Rogue_Lv3_StaminaReduction"] = "【Lv3 스태미나 감소 (%)】\n권장값: 17-22%",
                ["Rogue_Lv4_StaminaReduction"] = "【Lv4 스태미나 감소 (%)】\n권장값: 19-25%",
                ["Rogue_Lv5_StaminaReduction"] = "【Lv5 스태미나 감소 (%)】\n권장값: 22-30%",

                ["Rogue_Lv2_DodgeChance"] = "【Lv2 회피율 (%)】\n권장값: 5-8%",
                ["Rogue_Lv3_DodgeChance"] = "【Lv3 회피율 (%)】\n권장값: 7-10%",
                ["Rogue_Lv4_DodgeChance"] = "【Lv4 회피율 (%)】\n권장값: 9-12%",
                ["Rogue_Lv5_DodgeChance"] = "【Lv5 회피율 (%)】\n권장값: 11-15%",

                ["Rogue_Lv1_MoveSpeed"] = "【Lv1 이동속도 보너스 (%)】\n권장값: 3-7%",
                ["Rogue_Lv2_MoveSpeed"] = "【Lv2 이동속도 보너스 (%)】\n권장값: 5-10%",
                ["Rogue_Lv3_MoveSpeed"] = "【Lv3 이동속도 보너스 (%)】\n권장값: 7-12%",
                ["Rogue_Lv4_MoveSpeed"] = "【Lv4 이동속도 보너스 (%)】\n권장값: 10-15%",
                ["Rogue_Lv5_MoveSpeed"] = "【Lv5 이동속도 보너스 (%)】\n권장값: 12-18%",

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

                // === Paladin Job: Lv2 액티브 ===
                ["Paladin_Lv2_SelfHealPercent"] =
                "【Lv2 자가 치유 비율 (%)】\n" +
                "성기사 Lv2에서 자신이 회복하는 최대 체력 비율입니다.\n" +
                "권장값: 15-20%",

                ["Paladin_Lv2_AllyHealPercent"] =
                "【Lv2 아군 치유 비율 (%/틱)】\n" +
                "성기사 Lv2에서 아군에게 적용되는 매 틱 힐 비율입니다.\n" +
                "권장값: 2-3%",

                // === Paladin Job: Lv3 액티브 ===
                ["Paladin_Lv3_SelfHealPercent"] =
                "【Lv3 자가 치유 비율 (%)】\n" +
                "성기사 Lv3에서 자신이 회복하는 최대 체력 비율입니다.\n" +
                "권장값: 17-22%",

                ["Paladin_Lv3_AllyHealPercent"] =
                "【Lv3 아군 치유 비율 (%/틱)】\n" +
                "성기사 Lv3에서 아군에게 적용되는 매 틱 힐 비율입니다.\n" +
                "권장값: 2.5-3.5%",

                ["Paladin_Lv3_HealRange"] =
                "【Lv3 치유 범위 (m)】\n" +
                "성기사 Lv3에서 적용되는 아군 힐링 범위입니다.\n" +
                "권장값: 5-7m",

                // === Paladin Job: Lv4 액티브 ===
                ["Paladin_Lv4_SelfHealPercent"] =
                "【Lv4 자가 치유 비율 (%)】\n" +
                "성기사 Lv4에서 자신이 회복하는 최대 체력 비율입니다.\n" +
                "권장값: 19-24%",

                ["Paladin_Lv4_AllyHealPercent"] =
                "【Lv4 아군 치유 비율 (%/틱)】\n" +
                "성기사 Lv4에서 아군에게 적용되는 매 틱 힐 비율입니다.\n" +
                "권장값: 3-4%",

                ["Paladin_Lv4_HealRange"] =
                "【Lv4 치유 범위 (m)】\n" +
                "성기사 Lv4에서 적용되는 아군 힐링 범위입니다.\n" +
                "권장값: 6-8m",

                // === Paladin Job: Lv5 액티브 ===
                ["Paladin_Lv5_SelfHealPercent"] =
                "【Lv5 자가 치유 비율 (%)】\n" +
                "성기사 Lv5에서 자신이 회복하는 최대 체력 비율입니다.\n" +
                "권장값: 22-28%",

                ["Paladin_Lv5_AllyHealPercent"] =
                "【Lv5 아군 치유 비율 (%/틱)】\n" +
                "성기사 Lv5에서 아군에게 적용되는 매 틱 힐 비율입니다.\n" +
                "권장값: 3.5-5%",

                ["Paladin_Lv5_HealRange"] =
                "【Lv5 치유 범위 (m)】\n" +
                "성기사 Lv5에서 적용되는 아군 힐링 범위입니다.\n" +
                "권장값: 7-10m",

                ["Paladin_Lv5_Cooldown"] =
                "【Lv5 쿨타임 (초)】\n" +
                "성기사 Lv5에서 적용되는 단축된 쿨타임입니다.\n" +
                "권장값: 20-30초",

                // === Paladin Job: 패시브 Lv2~5 저항 감소 ===
                ["Paladin_Lv2_ResistanceReduction"] =
                "【Lv2 저항 감소 (%)】\n" +
                "성기사 Lv2 패시브: 적의 모든 저항을 감소시킵니다.\n" +
                "권장값: 6-10%",

                ["Paladin_Lv3_ResistanceReduction"] =
                "【Lv3 저항 감소 (%)】\n" +
                "성기사 Lv3 패시브: 적의 모든 저항을 감소시킵니다.\n" +
                "권장값: 8-12%",

                ["Paladin_Lv4_ResistanceReduction"] =
                "【Lv4 저항 감소 (%)】\n" +
                "성기사 Lv4 패시브: 적의 모든 저항을 감소시킵니다.\n" +
                "권장값: 10-14%",

                ["Paladin_Lv5_ResistanceReduction"] =
                "【Lv5 저항 감소 (%)】\n" +
                "성기사 Lv5 패시브: 적의 모든 저항을 감소시킵니다.\n" +
                "권장값: 12-18%",

                // === Paladin Job: 패시브 Lv2~5 스태미나 보너스 ===
                ["Paladin_Lv2_StaminaBonus"] =
                "【Lv2 최대 스태미나 보너스】\n" +
                "성기사 Lv2 패시브: 최대 스태미나를 고정값만큼 증가시킵니다.\n" +
                "권장값: 8-15",

                ["Paladin_Lv3_StaminaBonus"] =
                "【Lv3 최대 스태미나 보너스】\n" +
                "성기사 Lv3 패시브: 최대 스태미나를 고정값만큼 증가시킵니다.\n" +
                "권장값: 12-20",

                ["Paladin_Lv4_StaminaBonus"] =
                "【Lv4 최대 스태미나 보너스】\n" +
                "성기사 Lv4 패시브: 최대 스태미나를 고정값만큼 증가시킵니다.\n" +
                "권장값: 15-25",

                ["Paladin_Lv5_StaminaBonus"] =
                "【Lv5 최대 스태미나 보너스】\n" +
                "성기사 Lv5 패시브: 최대 스태미나를 고정값만큼 증가시킵니다.\n" +
                "권장값: 20-30",

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
                "기본값: 540초 (9분)\n" +
                "권장값: 120-300초",

                // === Berserker Job: 패시브 HP 보너스 (대소문자 수정 키) ===
                ["Berserker_Passive_HealthBonus"] =
                "【최대 체력 보너스 (%)】\n" +
                "버서커 패시브: 최대 체력을 증가시킵니다.\n" +
                "권장값: 100%",

                // ========================================
                // Producer Job Skills (제작 전문가 직업)
                // ========================================
                ["Producer_Buff_Cooldown"] =
                "【장인의 축복 쿨타임 (초)】\n" +
                "제작 전문가 버프 재사용 대기 시간입니다.\n" +
                "권장값: 120-180초",

                ["Producer_Buff_Duration"] =
                "【장인의 축복 지속시간 (초)】\n" +
                "공격력/체력 버프가 유지되는 시간입니다.\n" +
                "권장값: 90-120초",

                ["Producer_Buff_Range"] =
                "【장인의 축복 범위 (m)】\n" +
                "파티원에게 버프가 적용되는 범위입니다.\n" +
                "권장값: 12-20m",

                ["Producer_Buff_AttackBonus"] =
                "【버프 공격력 보너스 (%)】\n" +
                "버프 적용 중 증가하는 공격력 비율입니다.\n" +
                "권장값: 10-20%",

                ["Producer_Buff_MaxHealthBonus"] =
                "【버프 최대 체력 보너스 (%)】\n" +
                "버프 적용 중 증가하는 최대 체력 비율입니다.\n" +
                "권장값: 10-20%",

                ["Producer_Buff_StaminaCost"] =
                "【버프 스태미나 소모】\n" +
                "장인의 축복 발동 시 소모되는 스태미나입니다.\n" +
                "권장값: 15-25",

                // --- Lv1 ---
                ["Producer_Durability_Lv1"] = "【제작 아이템 내구도 보너스 Lv1 (%)】\nLv1에서 제작 아이템의 내구도 증가율입니다.\n기본값: 50%",
                ["Producer_CraftingSuccessRate_Lv1"] = "【제작 성공 확률 Lv1 (%)】\nLv1에서 제작 성공 확률 보너스입니다.\n기본값: 25%",
                ["Producer_EnchantChance_Lv1"] = "【마법부여 확률 Lv1 (%)】\nLv1에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 0%",

                // --- Lv2 ---
                ["Producer_Durability_Lv2"] = "【제작 아이템 내구도 보너스 Lv2 (%)】\nLv2에서 제작 아이템의 내구도 증가율입니다.\n기본값: 10%",
                ["Producer_CraftingSuccessRate_Lv2"] = "【제작 성공 확률 Lv2 (%)】\nLv2에서 제작 성공 확률 보너스입니다.\n기본값: 45%",
                ["Producer_MaterialReduction_Lv2"] = "【제작 재료 감소 Lv2 (%)】\nLv2에서 제작 시 절약되는 재료 비율입니다.\n기본값: 10%",
                ["Producer_EnchantChance_Lv2"] = "【마법부여 확률 Lv2 (%)】\nLv2에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 0%",

                // --- Lv3 ---
                ["Producer_Durability_Lv3"] = "【제작 아이템 내구도 보너스 Lv3 (%)】\nLv3에서 제작 아이템의 내구도 증가율입니다.\n기본값: 15%",
                ["Producer_CraftingSuccessRate_Lv3"] = "【제작 성공 확률 Lv3 (%)】\nLv3에서 제작 성공 확률 보너스입니다.\n기본값: 65%",
                ["Producer_MaterialReduction_Lv3"] = "【제작 재료 감소 Lv3 (%)】\nLv3에서 제작 시 절약되는 재료 비율입니다.\n기본값: 15%",
                ["Producer_EnchantChance_Lv3"] = "【마법부여 확률 Lv3 (%)】\nLv3에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 25%",

                // --- Lv4 ---
                ["Producer_Durability_Lv4"] = "【제작 아이템 내구도 보너스 Lv4 (%)】\nLv4에서 제작 아이템의 내구도 증가율입니다.\n기본값: 20%",
                ["Producer_CraftingSuccessRate_Lv4"] = "【제작 성공 확률 Lv4 (%)】\nLv4에서 제작 성공 확률 보너스입니다.\n기본값: 75%",
                ["Producer_MaterialReduction_Lv4"] = "【제작 재료 감소 Lv4 (%)】\nLv4에서 제작 시 절약되는 재료 비율입니다.\n기본값: 20%",
                ["Producer_EnchantChance_Lv4"] = "【마법부여 확률 Lv4 (%)】\nLv4에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 30%",

                // --- Lv5 ---
                ["Producer_Durability_Lv5"] = "【제작 아이템 내구도 보너스 Lv5 (%)】\nLv5에서 제작 아이템의 내구도 증가율입니다.\n기본값: 30%",
                ["Producer_CraftingSuccessRate_Lv5"] = "【제작 성공 확률 Lv5 (%)】\nLv5에서 제작 성공 확률 보너스입니다.\n기본값: 100%",
                ["Producer_MaterialReduction_Lv5"] = "【제작 재료 감소 Lv5 (%)】\nLv5에서 제작 시 절약되는 재료 비율입니다.\n기본값: 30%",
                ["Producer_EnchantChance_Lv5"] = "【마법부여 확률 Lv5 (%)】\nLv5에서 제작 아이템에 마법부여가 붙을 확률입니다.\n기본값: 35%",
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

                ["HUD_IconSize"] =
                "【Skill Icon Size】\n" +
                "Size of icons displayed in the active skill HUD.\n" +
                "Default: 62",

                ["HUD_PosX"] =
                "【Skill Icon HUD X Position】\n" +
                "Horizontal position of the active skill HUD.\n" +
                "Default: 306 (from screen left)",

                ["HUD_PosY"] =
                "【Skill Icon HUD Y Position】\n" +
                "Vertical position of the active skill HUD.\n" +
                "Default: 139 (from screen bottom)",

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

                ["Archer_MultiShot_FireInterval"] =
                "【Sequential Fire Interval (sec)】\n" +
                "Interval between each volley arrow.\n" +
                "5 arrows fire sequentially at this interval.\n" +
                "Recommended: 0.15-0.3 sec",

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

                // === Mage Job: Cooldown (level-based) ===
                ["Mage_Lv1_Cooldown"] =
                "【Cooldown Lv1 (sec)】\n" +
                "Mage Lv1 skill reactivation wait time.\n" +
                "Recommended: 120 sec",

                ["Mage_Lv2_Cooldown"] =
                "【Cooldown Lv2 (sec)】\n" +
                "Mage Lv2 skill reactivation wait time.\n" +
                "Recommended: 110 sec",

                ["Mage_Lv3_Cooldown"] =
                "【Cooldown Lv3 (sec)】\n" +
                "Mage Lv3 skill reactivation wait time.\n" +
                "Recommended: 100 sec",

                ["Mage_Lv4_Cooldown"] =
                "【Cooldown Lv4 (sec)】\n" +
                "Mage Lv4 skill reactivation wait time.\n" +
                "Recommended: 90 sec",

                ["Mage_Lv5_Cooldown"] =
                "【Cooldown Lv5 (sec)】\n" +
                "Mage Lv5 skill reactivation wait time.\n" +
                "Recommended: 80 sec",

                // === Mage Job: Max Targets (level-based) ===
                ["Mage_Lv1_AOE_Max_Targets"] =
                "【Max Target Count Lv1】\n" +
                "Mage Lv1 max monsters hit simultaneously. Selected by proximity.\n" +
                "Recommended: 6",

                ["Mage_Lv2_AOE_Max_Targets"] =
                "【Max Target Count Lv2】\n" +
                "Mage Lv2 max monsters hit simultaneously.\n" +
                "Recommended: 7",

                ["Mage_Lv3_AOE_Max_Targets"] =
                "【Max Target Count Lv3】\n" +
                "Mage Lv3 max monsters hit simultaneously.\n" +
                "Recommended: 8",

                ["Mage_Lv4_AOE_Max_Targets"] =
                "【Max Target Count Lv4】\n" +
                "Mage Lv4 max monsters hit simultaneously.\n" +
                "Recommended: 9",

                ["Mage_Lv5_AOE_Max_Targets"] =
                "【Max Target Count Lv5】\n" +
                "Mage Lv5 max monsters hit simultaneously.\n" +
                "Recommended: 10",

                // === Mage Job: Passive Elemental Resistance (level-based) ===
                ["Mage_Lv1_Elemental_Resistance"] =
                "【Elemental Resistance Lv1 (%)】\n" +
                "Mage Lv1 elemental resistance. Reduces Fire/Frost/Lightning/Poison/Spirit.\n" +
                "Recommended: 5%",

                ["Mage_Lv2_Elemental_Resistance"] =
                "【Elemental Resistance Lv2 (%)】\n" +
                "Mage Lv2 elemental resistance. Includes extra cast +1 (within 30s).\n" +
                "Recommended: 7%",

                ["Mage_Lv3_Elemental_Resistance"] =
                "【Elemental Resistance Lv3 (%)】\n" +
                "Mage Lv3 elemental resistance.\n" +
                "Recommended: 9%",

                ["Mage_Lv4_Elemental_Resistance"] =
                "【Elemental Resistance Lv4 (%)】\n" +
                "Mage Lv4 elemental resistance.\n" +
                "Recommended: 12%",

                ["Mage_Lv5_Elemental_Resistance"] =
                "【Elemental Resistance Lv5 (%)】\n" +
                "Mage Lv5 elemental resistance.\n" +
                "Recommended: 15%",

                // === Mage Job: AOE Damage Multiplier (level-based) ===
                ["Mage_Lv1_Damage_Multiplier"] =
                "【AOE Damage Multiplier Lv1 (%)】\n" +
                "Mage Lv1 AOE damage multiplier.\n" +
                "Recommended: 70%",

                ["Mage_Lv2_Damage_Multiplier"] =
                "【AOE Damage Multiplier Lv2 (%)】\n" +
                "Mage Lv2 AOE damage multiplier.\n" +
                "Recommended: 90%",

                ["Mage_Lv3_Damage_Multiplier"] =
                "【AOE Damage Multiplier Lv3 (%)】\n" +
                "Mage Lv3 AOE damage multiplier.\n" +
                "Recommended: 110%",

                ["Mage_Lv4_Damage_Multiplier"] =
                "【AOE Damage Multiplier Lv4 (%)】\n" +
                "Mage Lv4 AOE damage multiplier.\n" +
                "Recommended: 130%",

                ["Mage_Lv5_Damage_Multiplier"] =
                "【AOE Damage Multiplier Lv5 (%)】\n" +
                "Mage Lv5 AOE damage multiplier.\n" +
                "Recommended: 150%",

                // === Berserker Job: Passive HP Bonus ===
                ["berserker_passive_health_bonus"] =
                "【Max HP Bonus (%)】\n" +
                "Berserker Passive: Increases maximum health.\n" +
                "Applied as a percentage of total HP (base + MMO stats + all bonuses).\n" +
                "Healing works correctly (included in m_baseHP).\n" +
                "Recommended: 100%",

                // === Berserker Lv2~5 Passive Config ===
                ["Berserker_Lv2_CooldownReduction"] =
                "【Berserker Lv2: Rage Cooldown Reduction (s)】\n" +
                "At Lv2, reduces the Rage skill cooldown by this amount.\n" +
                "Recommended: 5 seconds",

                ["Berserker_Lv3_RageDamageReduction"] =
                "【Berserker Lv3: Damage Reduction While Raging (%)】\n" +
                "At Lv3, reduces incoming damage by this percentage while in rage.\n" +
                "Recommended: 15%",

                ["Berserker_Lv4_LowHpAttackBonus"] =
                "【Berserker Lv4: Low HP Attack Bonus (%)】\n" +
                "At Lv4, increases attack power when HP falls below the threshold.\n" +
                "Recommended: 15%",

                ["Berserker_Lv4_LowHpAttackThreshold"] =
                "【Berserker Lv4: Low HP Attack Threshold (%)】\n" +
                "The HP percentage below which the Lv4 attack bonus activates.\n" +
                "Recommended: 50%",

                ["Berserker_Lv5_PassiveCooldownReduction"] =
                "【Berserker Lv5: Death Defiance Cooldown Reduction (s)】\n" +
                "At Lv5, reduces the Death Defiance passive cooldown by this amount.\n" +
                "Recommended: 120 seconds (2 minutes)",

                ["Berserker_Lv5_InvincibilityBonus"] =
                "【Berserker Lv5: Death Defiance Invincibility Bonus (s)】\n" +
                "At Lv5, extends invincibility duration when Death Defiance triggers.\n" +
                "Recommended: 2 seconds",

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

                ["Tanker_Taunt_ReflectPercent"] =
                "【Taunt Reflect Damage (%)】\n" +
                "Reflects incoming damage to attackers during War Cry buff.\n" +
                "Active for the buff duration.\n" +
                "Recommended: 5-20%",

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

                ["Tanker_NormalShield_SpeedBonus"] =
                "【Tanker Normal Shield Move Speed Bonus (%)】\n" +
                "Tanker Lv1+: Move speed bonus when equipping a normal shield.\n" +
                "Default: 25%",

                ["Tanker_TowerShield_SpeedBonus"] =
                "【Tanker Tower Shield Move Speed Bonus (%)】\n" +
                "Tanker Lv1+: Move speed bonus when equipping a tower (large) shield.\n" +
                "Default: 30%",

                // === Tanker Job: Level-Up Passives (by level) ===
                // --- Lv1 ---
                ["Tanker_ReflectDuration_Lv1"] =
                "【Tanker Reflect Duration Lv1 (sec)】\n" +
                "Duration of reflect effect when taunting at Lv1.\n" +
                "Default: 10 sec",

                ["Tanker_Hp_Bonus_Lv1"] =
                "【Tanker Lv1 HP Bonus (%)】\n" +
                "Max HP increases by this percentage when reaching Tanker Lv1.\n" +
                "Default: 25",

                // --- Lv2 ---
                ["Tanker_Hp_Bonus_Lv2"] =
                "【Tanker Lv2 HP Bonus (%)】\n" +
                "Max HP increases by this percentage when reaching Tanker Lv2.\n" +
                "Default: 30",

                ["Tanker_Lv2_BlockPower"] =
                "【Tanker Lv2 Block Power】\n" +
                "Shield block armor increase at Tanker Lv2.\n" +
                "Default: 5",

                ["Tanker_ReflectDuration_Lv2"] =
                "【Tanker Reflect Duration Lv2 (sec)】\n" +
                "Duration of reflect effect when taunting at Lv2.\n" +
                "Default: 12 sec",

                // --- Lv3 ---
                ["Tanker_Hp_Bonus_Lv3"] =
                "【Tanker Lv3 HP Bonus (%)】\n" +
                "Max HP increases by this percentage when reaching Tanker Lv3.\n" +
                "Default: 35",

                ["Tanker_Lv3_BlockPower"] =
                "【Tanker Lv3 Block Power】\n" +
                "Shield block armor increase at Tanker Lv3.\n" +
                "Default: 10",

                ["Tanker_ReflectDuration_Lv3"] =
                "【Tanker Reflect Duration Lv3 (sec)】\n" +
                "Duration of reflect effect when taunting at Lv3.\n" +
                "Default: 14 sec",

                // --- Lv4 ---
                ["Tanker_Hp_Bonus_Lv4"] =
                "【Tanker Lv4 HP Bonus (%)】\n" +
                "Max HP increases by this percentage when reaching Tanker Lv4.\n" +
                "Default: 40",

                ["Tanker_Lv4_BlockPower"] =
                "【Tanker Lv4 Block Power】\n" +
                "Shield block armor increase at Tanker Lv4.\n" +
                "Default: 15",

                ["Tanker_ReflectDuration_Lv4"] =
                "【Tanker Reflect Duration Lv4 (sec)】\n" +
                "Duration of reflect effect when taunting at Lv4.\n" +
                "Default: 16 sec",

                // --- Lv5 ---
                ["Tanker_Hp_Bonus_Lv5"] =
                "【Tanker Lv5 HP Bonus (%)】\n" +
                "Max HP increases by this percentage when reaching Tanker Lv5.\n" +
                "Default: 50",

                ["Tanker_Lv5_BlockPower"] =
                "【Tanker Lv5 Block Power】\n" +
                "Shield block armor increase at Tanker Lv5.\n" +
                "Default: 20",

                ["Tanker_ReflectDuration_Lv5"] =
                "【Tanker Reflect Duration Lv5 (sec)】\n" +
                "Duration of reflect effect when taunting at Lv5.\n" +
                "Default: 20 sec",

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

                ["Rogue_Lv1_DodgeChance"] =
                "【Lv1 Evasion (%)】\n" +
                "Rogue passive: Increases hit evasion rate (not dodge roll). Stacks with skill tree total.\n" +
                "Recommended: 3-6%",

                // === Rogue Job: Passive Lv2~5 Growth ===
                ["Rogue_Lv2_AttackSpeed"] = "【Lv2 Attack Speed Bonus (%)】\nRecommended: 10-15%",
                ["Rogue_Lv3_AttackSpeed"] = "【Lv3 Attack Speed Bonus (%)】\nRecommended: 12-18%",
                ["Rogue_Lv4_AttackSpeed"] = "【Lv4 Attack Speed Bonus (%)】\nRecommended: 14-20%",
                ["Rogue_Lv5_AttackSpeed"] = "【Lv5 Attack Speed Bonus (%)】\nRecommended: 16-22%",

                ["Rogue_Lv2_StaminaReduction"] = "【Lv2 Stamina Reduction (%)】\nRecommended: 15-20%",
                ["Rogue_Lv3_StaminaReduction"] = "【Lv3 Stamina Reduction (%)】\nRecommended: 17-22%",
                ["Rogue_Lv4_StaminaReduction"] = "【Lv4 Stamina Reduction (%)】\nRecommended: 19-25%",
                ["Rogue_Lv5_StaminaReduction"] = "【Lv5 Stamina Reduction (%)】\nRecommended: 22-30%",

                ["Rogue_Lv2_DodgeChance"] = "【Lv2 Evasion (%)】\nRecommended: 5-8%",
                ["Rogue_Lv3_DodgeChance"] = "【Lv3 Evasion (%)】\nRecommended: 7-10%",
                ["Rogue_Lv4_DodgeChance"] = "【Lv4 Evasion (%)】\nRecommended: 9-12%",
                ["Rogue_Lv5_DodgeChance"] = "【Lv5 Evasion (%)】\nRecommended: 11-15%",

                ["Rogue_Lv1_MoveSpeed"] = "【Lv1 Move Speed Bonus (%)】\nRecommended: 3-7%",
                ["Rogue_Lv2_MoveSpeed"] = "【Lv2 Move Speed Bonus (%)】\nRecommended: 5-10%",
                ["Rogue_Lv3_MoveSpeed"] = "【Lv3 Move Speed Bonus (%)】\nRecommended: 7-12%",
                ["Rogue_Lv4_MoveSpeed"] = "【Lv4 Move Speed Bonus (%)】\nRecommended: 10-15%",
                ["Rogue_Lv5_MoveSpeed"] = "【Lv5 Move Speed Bonus (%)】\nRecommended: 12-18%",

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

                // === Paladin Job: Lv2 Active ===
                ["Paladin_Lv2_SelfHealPercent"] =
                "【Lv2 Self Heal Ratio (%)】\n" +
                "Percentage of max HP restored to self at Paladin Lv2.\n" +
                "Recommended: 15-20%",

                ["Paladin_Lv2_AllyHealPercent"] =
                "【Lv2 Ally Heal Ratio (%/tick)】\n" +
                "Percentage of max HP restored per tick to allies at Paladin Lv2.\n" +
                "Recommended: 2-3%",

                // === Paladin Job: Lv3 Active ===
                ["Paladin_Lv3_SelfHealPercent"] =
                "【Lv3 Self Heal Ratio (%)】\n" +
                "Percentage of max HP restored to self at Paladin Lv3.\n" +
                "Recommended: 17-22%",

                ["Paladin_Lv3_AllyHealPercent"] =
                "【Lv3 Ally Heal Ratio (%/tick)】\n" +
                "Percentage of max HP restored per tick to allies at Paladin Lv3.\n" +
                "Recommended: 2.5-3.5%",

                ["Paladin_Lv3_HealRange"] =
                "【Lv3 Heal Range (m)】\n" +
                "Ally healing radius at Paladin Lv3.\n" +
                "Recommended: 5-7m",

                // === Paladin Job: Lv4 Active ===
                ["Paladin_Lv4_SelfHealPercent"] =
                "【Lv4 Self Heal Ratio (%)】\n" +
                "Percentage of max HP restored to self at Paladin Lv4.\n" +
                "Recommended: 19-24%",

                ["Paladin_Lv4_AllyHealPercent"] =
                "【Lv4 Ally Heal Ratio (%/tick)】\n" +
                "Percentage of max HP restored per tick to allies at Paladin Lv4.\n" +
                "Recommended: 3-4%",

                ["Paladin_Lv4_HealRange"] =
                "【Lv4 Heal Range (m)】\n" +
                "Ally healing radius at Paladin Lv4.\n" +
                "Recommended: 6-8m",

                // === Paladin Job: Lv5 Active ===
                ["Paladin_Lv5_SelfHealPercent"] =
                "【Lv5 Self Heal Ratio (%)】\n" +
                "Percentage of max HP restored to self at Paladin Lv5.\n" +
                "Recommended: 22-28%",

                ["Paladin_Lv5_AllyHealPercent"] =
                "【Lv5 Ally Heal Ratio (%/tick)】\n" +
                "Percentage of max HP restored per tick to allies at Paladin Lv5.\n" +
                "Recommended: 3.5-5%",

                ["Paladin_Lv5_HealRange"] =
                "【Lv5 Heal Range (m)】\n" +
                "Ally healing radius at Paladin Lv5.\n" +
                "Recommended: 7-10m",

                ["Paladin_Lv5_Cooldown"] =
                "【Lv5 Cooldown (sec)】\n" +
                "Reduced cooldown applied at Paladin Lv5.\n" +
                "Recommended: 20-30 sec",

                // === Paladin Job: Passive Lv2~5 Resistance Reduction ===
                ["Paladin_Lv2_ResistanceReduction"] =
                "【Lv2 Resistance Reduction (%)】\n" +
                "Paladin Lv2 passive: Reduces all enemy resistances.\n" +
                "Recommended: 6-10%",

                ["Paladin_Lv3_ResistanceReduction"] =
                "【Lv3 Resistance Reduction (%)】\n" +
                "Paladin Lv3 passive: Reduces all enemy resistances.\n" +
                "Recommended: 8-12%",

                ["Paladin_Lv4_ResistanceReduction"] =
                "【Lv4 Resistance Reduction (%)】\n" +
                "Paladin Lv4 passive: Reduces all enemy resistances.\n" +
                "Recommended: 10-14%",

                ["Paladin_Lv5_ResistanceReduction"] =
                "【Lv5 Resistance Reduction (%)】\n" +
                "Paladin Lv5 passive: Reduces all enemy resistances.\n" +
                "Recommended: 12-18%",

                // === Paladin Job: Passive Lv2~5 Stamina Bonus ===
                ["Paladin_Lv2_StaminaBonus"] =
                "【Lv2 Max Stamina Bonus】\n" +
                "Paladin Lv2 passive: Increases max stamina by a flat amount.\n" +
                "Recommended: 8-15",

                ["Paladin_Lv3_StaminaBonus"] =
                "【Lv3 Max Stamina Bonus】\n" +
                "Paladin Lv3 passive: Increases max stamina by a flat amount.\n" +
                "Recommended: 12-20",

                ["Paladin_Lv4_StaminaBonus"] =
                "【Lv4 Max Stamina Bonus】\n" +
                "Paladin Lv4 passive: Increases max stamina by a flat amount.\n" +
                "Recommended: 15-25",

                ["Paladin_Lv5_StaminaBonus"] =
                "【Lv5 Max Stamina Bonus】\n" +
                "Paladin Lv5 passive: Increases max stamina by a flat amount.\n" +
                "Recommended: 20-30",

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
                "Default: 540 sec (9 minutes)\n" +
                "Recommended: 120-300 sec",

                // === Berserker Job: Passive HP Bonus (case-corrected key) ===
                ["Berserker_Passive_HealthBonus"] =
                "【Max Health Bonus (%)】\n" +
                "Berserker passive: increases maximum health.\n" +
                "Recommended: 100%",

                // ========================================
                // Producer Job Skills
                // ========================================
                ["Producer_Buff_Cooldown"] =
                "【Artisan's Blessing Cooldown (sec)】\n" +
                "Cooldown time before the Producer buff can be used again.\n" +
                "Recommended: 120-180 sec",

                ["Producer_Buff_Duration"] =
                "【Artisan's Blessing Duration (sec)】\n" +
                "Duration of the attack/health buff.\n" +
                "Recommended: 90-120 sec",

                ["Producer_Buff_Range"] =
                "【Artisan's Blessing Range (m)】\n" +
                "Range in which party members receive the buff.\n" +
                "Recommended: 12-20 m",

                ["Producer_Buff_AttackBonus"] =
                "【Buff Attack Bonus (%)】\n" +
                "Attack power increase while the buff is active.\n" +
                "Recommended: 10-20%",

                ["Producer_Buff_MaxHealthBonus"] =
                "【Buff Max Health Bonus (%)】\n" +
                "Max health increase while the buff is active.\n" +
                "Recommended: 10-20%",

                ["Producer_Buff_StaminaCost"] =
                "【Buff Stamina Cost】\n" +
                "Stamina consumed when activating Artisan's Blessing.\n" +
                "Recommended: 15-25",

                // --- Lv1 ---
                ["Producer_Durability_Lv1"] = "【Crafted Item Durability Bonus Lv1 (%)】\nDurability bonus of crafted items at Lv1.\nDefault: 50%",
                ["Producer_CraftingSuccessRate_Lv1"] = "【Crafting Success Rate Lv1 (%)】\nBonus crafting success rate at Lv1.\nDefault: 25%",
                ["Producer_EnchantChance_Lv1"] = "【Enchant Chance Lv1 (%)】\nChance for crafted items to receive an enchantment at Lv1.\nDefault: 0%",

                // --- Lv2 ---
                ["Producer_Durability_Lv2"] = "【Crafted Item Durability Bonus Lv2 (%)】\nDurability bonus of crafted items at Lv2.\nDefault: 10%",
                ["Producer_CraftingSuccessRate_Lv2"] = "【Crafting Success Rate Lv2 (%)】\nBonus crafting success rate at Lv2.\nDefault: 45%",
                ["Producer_MaterialReduction_Lv2"] = "【Material Reduction Lv2 (%)】\nMaterials saved per craft at Lv2.\nDefault: 10%",
                ["Producer_EnchantChance_Lv2"] = "【Enchant Chance Lv2 (%)】\nChance for crafted items to receive an enchantment at Lv2.\nDefault: 0%",

                // --- Lv3 ---
                ["Producer_Durability_Lv3"] = "【Crafted Item Durability Bonus Lv3 (%)】\nDurability bonus of crafted items at Lv3.\nDefault: 15%",
                ["Producer_CraftingSuccessRate_Lv3"] = "【Crafting Success Rate Lv3 (%)】\nBonus crafting success rate at Lv3.\nDefault: 65%",
                ["Producer_MaterialReduction_Lv3"] = "【Material Reduction Lv3 (%)】\nMaterials saved per craft at Lv3.\nDefault: 15%",
                ["Producer_EnchantChance_Lv3"] = "【Enchant Chance Lv3 (%)】\nChance for crafted items to receive an enchantment at Lv3.\nDefault: 25%",

                // --- Lv4 ---
                ["Producer_Durability_Lv4"] = "【Crafted Item Durability Bonus Lv4 (%)】\nDurability bonus of crafted items at Lv4.\nDefault: 20%",
                ["Producer_CraftingSuccessRate_Lv4"] = "【Crafting Success Rate Lv4 (%)】\nBonus crafting success rate at Lv4.\nDefault: 75%",
                ["Producer_MaterialReduction_Lv4"] = "【Material Reduction Lv4 (%)】\nMaterials saved per craft at Lv4.\nDefault: 20%",
                ["Producer_EnchantChance_Lv4"] = "【Enchant Chance Lv4 (%)】\nChance for crafted items to receive an enchantment at Lv4.\nDefault: 30%",

                // --- Lv5 ---
                ["Producer_Durability_Lv5"] = "【Crafted Item Durability Bonus Lv5 (%)】\nDurability bonus of crafted items at Lv5.\nDefault: 30%",
                ["Producer_CraftingSuccessRate_Lv5"] = "【Crafting Success Rate Lv5 (%)】\nBonus crafting success rate at Lv5.\nDefault: 100%",
                ["Producer_MaterialReduction_Lv5"] = "【Material Reduction Lv5 (%)】\nMaterials saved per craft at Lv5.\nDefault: 30%",
                ["Producer_EnchantChance_Lv5"] = "【Enchant Chance Lv5 (%)】\nChance for crafted items to receive an enchantment at Lv5.\nDefault: 35%",

                // ============================================
                // Job Level Coin Cost (직업 레벨업 코인 비용)
                // ============================================
                ["Job_Lv1_Cost"] = "【직업 Lv1 코인 비용】\n모든 직업을 Lv1로 업그레이드할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 1000",
                ["Job_Lv2_Cost"] = "【직업 Lv2 코인 비용】\n모든 직업을 Lv2로 업그레이드할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 2000",
                ["Job_Lv3_Cost"] = "【직업 Lv3 코인 비용】\n모든 직업을 Lv3로 업그레이드할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 3000",
                ["Job_Lv4_Cost"] = "【직업 Lv4 코인 비용】\n모든 직업을 Lv4로 업그레이드할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 4000",
                ["Job_Lv5_Cost"] = "【직업 Lv5 코인 비용】\n모든 직업을 Lv5로 업그레이드할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 5000",

                // ============================================
                // Skill Reset Cost (스킬 포인트 초기화 비용)
                // ============================================
                ["Job_Reset_Cost"]    = "【직업스킬 포인트 초기화 비용】\n직업 스킬 포인트를 초기화할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 1000",
                ["Active_Reset_Cost"] = "【액티브스킬 포인트 초기화 비용】\n액티브 스킬 포인트를 초기화할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 500",
                ["Passive_Reset_Cost"]= "【패시브스킬 포인트 초기화 비용】\n패시브 스킬 포인트를 초기화할 때 소모되는 코인 수입니다.\n서버 관리자만 수정 가능, 모든 클라이언트에 자동 동기화.\n기본값: 100",
            };
        }
    }
}

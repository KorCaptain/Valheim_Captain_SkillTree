using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetRangedDescriptions_KO()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Staff Tree (지팡이 트리)
                // ========================================











                // === Tier 0: 지팡이 전문가 (Staff Expert) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【속성 데미지 보너스 (%)】\n" +
                "지팡이의 속성 데미지(불, 얼음, 번개)를 증가시킵니다.\n" +
                "마법 공격의 기본이 되는 핵심 스킬입니다.\n" +
                "권장값: 10-15%",

                // === Tier 1: 정신 집중 & 마력 흐름 ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【Eitr 소모 감소 (%)】\n" +
                "정신 집중으로 지팡이 주문의 Eitr 소모를 줄입니다.\n" +
                "더 많은 마법을 사용할 수 있습니다.\n" +
                "권장값: 12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【최대 Eitr 보너스 (고정값)】\n" +
                "마력 흐름으로 최대 Eitr를 증가시킵니다.\n" +
                "권장값: 25-35",

                // === Tier 2: 마법 증폭 (Magic Amplify) ===
                ["Tier2_MagicAmplify_Chance"] =
                "【마법 증폭 발동 확률 (%)】\n" +
                "속성 공격이 증폭될 확률입니다.\n" +
                "권장값: 30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【마법 증폭 속성 공격 보너스 (%)】\n" +
                "발동 시 속성 데미지 증가량입니다.\n" +
                "권장값: 30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【마법 증폭 에이트르 소모 증가 (%)】\n" +
                "마법 시전 시 에이트르 소모량이 증가합니다.\n" +
                "강력한 마법의 대가로 작용합니다.\n" +
                "권장값: 15-25%",

                // === Tier 3: 속성 강화 (Elemental Enhancement) ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【얼음 공격력 보너스 (고정값)】\n" +
                "얼음 속성 마법의 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【불 공격력 보너스 (고정값)】\n" +
                "불 속성 마법의 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【번개 공격력 보너스 (고정값)】\n" +
                "번개 속성 마법의 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 2-5",

                // === Tier 4: 행운의 마력 (Lucky Mana) ===
                ["Tier4_LuckyMana_Chance"] =
                "【Eitr 무소모 발동 확률 (%)】\n" +
                "주문 사용 시 Eitr를 소모하지 않을 확률입니다.\n" +
                "행운의 마력으로 무한 시전이 가능합니다.\n" +
                "권장값: 30-40%",

                // === Tier 5-1: 이중 시전 (Double Cast - R키 액티브) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【추가 발사체 개수】\n" +
                "연속 발사 시 추가로 발사되는 마법 발사체 수입니다.\n" +
                "권장값: 5~10발",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【투사체 데미지 비율 (%)】\n" +
                "추가 발사체의 데미지 비율입니다.\n" +
                "권장값: 10-20%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【각도 오프셋 (미사용)】\n" +
                "현재 버전에서 사용되지 않습니다. 동일 방향으로 고정됨.",

                ["Tier5_DoubleCast_EitrCost"] =
                "【Eitr 소모량】\n" +
                "스킬 활성화 시 소모되는 Eitr입니다.\n" +
                "권장값: 15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 25-40초",

                // === Tier 5-2: 즉시 범위 힐 (Instant Area Heal - H키 액티브) ===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 25-40초",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【Eitr 소모량】\n" +
                "스킬 사용 시 소모되는 Eitr입니다.\n" +
                "권장값: 25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【회복량 (최대 HP 대비 %)】\n" +
                "최대 체력 대비 회복되는 비율입니다.\n" +
                "권장값: 20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【치유 범위 (미터)】\n" +
                "힐이 적용되는 범위입니다.\n" +
                "권장값: 10-15m",

                // === Staff Tree RequiredPoints ===
                ["Tier0_StaffExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "지팡이 전문가 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【필요 포인트】\n" +
                "정신 집중 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【필요 포인트】\n" +
                "마법 흐름 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【필요 포인트】\n" +
                "마법 증폭 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【필요 포인트】\n" +
                "냉기 속성 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier3_FireElement_RequiredPoints"] =
                "【필요 포인트】\n" +
                "화염 속성 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【필요 포인트】\n" +
                "번개 속성 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【필요 포인트】\n" +
                "행운 마력 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【필요 포인트】\n" +
                "연속 발사 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【필요 포인트】\n" +
                "즉시 범위 힐 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                // === Crossbow Tree ===
                // === Tier 0: 석궁 전문가 (Crossbow Expert) ===

                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "석궁 전문가 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【석궁 공격력 보너스 (%)】\n" +
                "석궁 및 볼트 무기의 기본 공격력을 증가시킵니다.\n" +
                "권장값: 8-12%",

                // === Tier 1: 연발 (Rapid Fire) ===

                ["Tier1_RapidFire_Chance"] =
                "【연발 발동 확률 (%)】\n" +
                "석궁 발사 시 연발이 발동될 확률입니다.\n" +
                "연발 발동 시 여러 발을 빠르게 발사합니다.\n" +
                "권장값: 15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【연발 발사 횟수】\n" +
                "연발 발동 시 추가로 발사되는 볼트 개수입니다.\n" +
                "권장값: 2-4발",

                ["Tier1_RapidFire_DamagePercent"] =
                "【연발 데미지 비율 (%)】\n" +
                "연발로 발사되는 볼트의 데미지 비율입니다.\n" +
                "원래 공격력 대비 비율입니다.\n" +
                "권장값: 60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【연발 간격 (초)】\n" +
                "연발 시 각 볼트 발사 사이의 시간 간격입니다.\n" +
                "짧을수록 빠르게 발사됩니다.\n" +
                "권장값: 0.1-0.3초",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【연발 볼트 소모량】\n" +
                "연발 시 소모되는 볼트 개수입니다.\n" +
                "권장값: 1-2개",

                ["Tier1_RapidFire_RequiredPoints"] =
                "【필요 포인트】\n" +
                "연발 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                // === Tier 2: 균형잡힌 조준 (Balanced Aim) ===

                ["Tier2_BalancedAim_KnockbackChance"] =
                "【넉백 발동 확률 (%)】\n" +
                "석궁 명중 시 적을 밀어낼 확률입니다.\n" +
                "균형잡힌 자세에서 안정적인 충격을 줍니다.\n" +
                "권장값: 20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【넉백 거리 (미터)】\n" +
                "넉백 발동 시 적을 밀어내는 거리입니다.\n" +
                "권장값: 2-4m",

                // === Tier 2: 신속 장전 (Rapid Reload) ===

                ["Tier2_RapidReload_SpeedIncrease"] =
                "【장전 속도 증가 (%)】\n" +
                "석궁 재장전 속도를 증가시킵니다.\n" +
                "더 빠르게 다음 볼트를 장전할 수 있습니다.\n" +
                "권장값: 10-20%",

                // === Tier 2: 정직한 사격 (Honest Shot) ===

                ["Tier2_HonestShot_DamageBonus"] =
                "【기본 데미지 보너스 (%)】\n" +
                "석궁의 기본 공격력을 추가로 증가시킵니다.\n" +
                "권장값: 10-18%",

                ["Tier2_BalancedAim_RequiredPoints"] =
                "【필요 포인트】\n" +
                "균형 조준 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier2_RapidReload_RequiredPoints"] =
                "【필요 포인트】\n" +
                "고속 장전 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier2_HonestShot_RequiredPoints"] =
                "【필요 포인트】\n" +
                "정직한 한 발 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                // === Tier 3: 자동 재장전 (Auto Reload) ===

                ["Tier3_AutoReload_Chance"] =
                "【자동 장전 발동 확률 (%)】\n" +
                "석궁 명중 시 다음 재장전 속도가 200%로 증가할 확률입니다.\n" +
                "연속 공격 흐름을 유지하는 데 도움을 줍니다.\n" +
                "권장값: 20-35%",

                ["Tier3_AutoReload_RequiredPoints"] =
                "【필요 포인트】\n" +
                "자동 장전 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                // === Tier 4: 연발 Lv2 (Rapid Fire Lv2) ===


                ["Tier4_RapidFireLv2_Chance"] =
                "【연발 Lv2 발동 확률 (%)】\n" +
                "강화된 연발이 발동될 확률입니다.\n" +
                "Tier 1 연발과 확률이 합산됩니다.\n" +
                "권장값: 20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【연발 Lv2 발사 횟수】\n" +
                "강화된 연발의 추가 볼트 개수입니다.\n" +
                "Tier 1 연발보다 더 많이 발사됩니다.\n" +
                "권장값: 4-6발",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【연발 Lv2 데미지 비율 (%)】\n" +
                "강화된 연발 볼트의 데미지 비율입니다.\n" +
                "Tier 1보다 데미지가 증가합니다.\n" +
                "권장값: 75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【연발 Lv2 발사 간격 (초)】\n" +
                "강화된 연발 시 각 볼트 발사 사이의 시간 간격입니다.\n" +
                "권장값: 0.1-0.3초",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【연발 Lv2 볼트 소모량】\n" +
                "강화된 연발 시 소모되는 볼트 개수입니다.\n" +
                "권장값: 1-2개",

                // === Tier 4: 최후의 일격 (First Strike) ===

                ["Tier4_FinalStrike_HpThreshold"] =
                "【적 체력 임계값 (%)】\n" +
                "이 체력 이상인 적에게 추가 데미지를 줍니다.\n" +
                "높은 체력의 적을 상대할 때 효과적입니다.\n" +
                "권장값: 40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【추가 데미지 보너스 (%)】\n" +
                "임계값 이상 체력의 적에게 주는 추가 데미지입니다.\n" +
                "권장값: 20-40%",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【필요 포인트】\n" +
                "연속 발사 Lv2 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier4_FinalStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "결전의 일격 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                // === Tier 5: 원샷 (One Shot - R키 액티브) ===

                ["Tier5_OneShot_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "원샷 버프가 유지되는 시간입니다.\n" +
                "이 시간 동안 강화된 한 발을 발사할 수 있습니다.\n" +
                "권장값: 8-12초",

                ["Tier5_OneShot_DamageBonus"] =
                "【원샷 데미지 보너스 (%)】\n" +
                "원샷 발사 시 추가되는 데미지 보너스입니다.\n" +
                "치명적인 한 발의 위력입니다.\n" +
                "권장값: 150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【넉백 거리 (미터)】\n" +
                "원샷 명중 시 적을 밀어내는 거리입니다.\n" +
                "강력한 충격으로 적을 밀어냅니다.\n" +
                "권장값: 5-10m",

                ["Tier5_OneShot_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 25-40초",

                ["Tier5_OneShot_RequiredPoints"] =
                "【필요 포인트】\n" +
                "단 한 발 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 4",

                // === Bow Tree ===
                // === Tier 0: 활 전문가 (Bow Expert) ===

                ["Tier0_BowExpert_DamageBonus"] =
                "【활 공격력 보너스 (%)】\n" +
                "활과 화살 무기의 기본 공격력을 증가시킵니다.\n" +
                "권장값: 8-12%",

                // === Tier 1-1: 집중 사격 (Focused Shot) ===

                ["Tier1_FocusedShot_CritBonus"] =
                "【치명타 확률 보너스 (%)】\n" +
                "집중 사격으로 크리티컬 확률을 증가시킵니다.\n" +
                "조준에 집중할수록 치명타 기회가 높아집니다.\n" +
                "권장값: 5-10%",

                // === Tier 1-2: 멀티샷 Lv1 ===

                ["Tier1_MultishotLv1_ActivationChance"] =
                "【멀티샷 Lv1 발동 확률 (%)】\n" +
                "활 공격 시 멀티샷이 발동될 확률입니다.\n" +
                "발동 시 여러 화살을 동시에 발사합니다.\n" +
                "권장값: 15-25%",

                ["Tier1_MultishotLv1_AdditionalArrows"] =
                "【추가 화살 개수】\n" +
                "멀티샷 발동 시 추가로 발사되는 화살 개수입니다.\n" +
                "권장값: 2-4개",

                ["Tier1_MultishotLv1_ArrowConsumption"] =
                "【화살 소모량】\n" +
                "멀티샷 발동 시 소모되는 화살 개수입니다.\n" +
                "권장값: 1-2개",

                ["Tier1_MultishotLv1_DamagePerArrow"] =
                "【화살당 데미지 비율 (%)】\n" +
                "멀티샷으로 발사되는 각 화살의 데미지 비율입니다.\n" +
                "권장값: 50-70%",

                // === Tier 2: 활 숙련 (Bow Mastery) ===

                ["Tier2_BowMastery_SkillBonus"] =
                "【활 숙련도 보너스 (고정값)】\n" +
                "활 기술 레벨을 고정값으로 증가시킵니다.\n" +
                "숙련도가 높아질수록 더 강력해집니다.\n" +
                "권장값: 5-10",

                ["Tier2_BowMastery_SpecialArrowChance"] =
                "【특수 화살 발동 확률 (%)】\n" +
                "특수 효과를 가진 화살이 발사될 확률입니다.\n" +
                "독, 불, 얼음 등의 상태이상 화살이 발사됩니다.\n" +
                "권장값: 25-35%",

                // === Tier 3-1: 침묵의 일격 (Silent Strike) ===

                ["Tier3_SilentStrike_DamageBonus"] =
                "【침묵의 일격 데미지 보너스 (고정값)】\n" +
                "활 공격력을 고정 수치로 증가시킵니다.\n" +
                "화살이 적을 관통하여 더 큰 피해를 입힙니다.\n" +
                "권장값: 3-8",

                // === Tier 3-2: 멀티샷 Lv2 ===

                ["Tier3_MultishotLv2_ActivationChance"] =
                "【멀티샷 Lv2 발동 확률 (%)】\n" +
                "강화된 멀티샷의 발동 확률입니다.\n" +
                "Lv1보다 많은 화살을 발사합니다.\n" +
                "권장값: 20-30%",

                // === Tier 3-3: 사냥 본능 (Hunting Instinct) ===

                ["Tier3_HuntingInstinct_CritBonus"] =
                "【사냥 본능 치명타 보너스 (%)】\n" +
                "사냥꾼의 본능으로 크리티컬 확률이 증가합니다.\n" +
                "권장값: 8-15%",

                // === Tier 4: 정조준 및 고급 스킬 ===

                ["Tier4_PrecisionAim_CritDamage"] =
                "【크리티컬 데미지 보너스 (%)】\n" +
                "정조준으로 치명타 피해를 증가시킵니다.\n" +
                "약점을 노려 더 큰 피해를 입힙니다.\n" +
                "권장값: 25-40%",

                // === Tier 5: 폭발 화살 (R키 액티브) ===

                ["Tier5_ExplosiveArrow_DamageMultiplier"] =
                "【폭발 화살 데미지 배율 (%)】\n" +
                "Z키 액티브 스킬 - 폭발 화살의 데미지 배율입니다.\n" +
                "범위 피해를 입히는 강력한 화살입니다.\n" +
                "권장값: 100-150%",

                ["Tier5_ExplosiveArrow_Radius"] =
                "【폭발 범위 (미터)】\n" +
                "폭발 화살의 피해 범위입니다.\n" +
                "권장값: 3-6m",

                ["Tier5_ExplosiveArrow_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 15-25초",

                ["Tier5_ExplosiveArrow_StaminaCost"] =
                "【스태미나 소모 (%)】\n" +
                "스킬 사용 시 소모되는 스태미나 비율입니다.\n" +
                "권장값: 10-20%",

                // === Bow Tree RequiredPoints ===
                ["Tier0_BowExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "활 전문가 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier1_FocusedShot_RequiredPoints"] =
                "【필요 포인트】\n" +
                "집중 사격 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier1_MultishotLv1_RequiredPoints"] =
                "【필요 포인트】\n" +
                "멀티샷 Lv1 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier2_BowMastery_RequiredPoints"] =
                "【필요 포인트】\n" +
                "활 마스터리 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier3_SilentStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "은밀한 일격 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier3_MultishotLv2_RequiredPoints"] =
                "【필요 포인트】\n" +
                "멀티샷 Lv2 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier3_HuntingInstinct_RequiredPoints"] =
                "【필요 포인트】\n" +
                "사냥 본능 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 2",

                ["Tier4_PrecisionAim_RequiredPoints"] =
                "【필요 포인트】\n" +
                "정조준 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 3",

                ["Tier5_ExplosiveArrow_RequiredPoints"] =
                "【필요 포인트】\n" +
                "폭발 화살 노드 해금에 필요한 스킬 포인트 수입니다.\n" +
                "권장값: 4",


            };
        }

        private static Dictionary<string, string> GetRangedDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Staff Tree
                // ========================================











                // === Tier 0: Staff Expert ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【Elemental Damage Bonus (%)】\n" +
                "Increases elemental damage (fire, frost, lightning) of staves.\n" +
                "Foundation of magical attacks.\n" +
                "Recommended: 10-15%",

                // === Tier 1: Mind Focus & Magic Flow ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【Eitr Cost Reduction (%)】\n" +
                "Mind focus reduces Eitr cost of staff spells.\n" +
                "Allows more magic usage.\n" +
                "Recommended: 12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【Max Eitr Bonus (Flat)】\n" +
                "Magic flow increases maximum Eitr.\n" +
                "Recommended: 25-35",

                // === Tier 2: Magic Amplify ===
                ["Tier2_MagicAmplify_Chance"] =
                "【Magic Amplification Activation Chance (%)】\n" +
                "Chance to amplify elemental damage.\n" +
                "Recommended: 30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【Magic Amplification Elemental Damage Bonus (%)】\n" +
                "Elemental damage increase on activation.\n" +
                "Recommended: 30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【Magic Amplification Eitr Cost Increase (%)】\n" +
                "Increases Eitr consumption when casting spells.\n" +
                "Price of powerful magic.\n" +
                "Recommended: 15-25%",

                // === Tier 3: Elemental Enhancement ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【Frost Attack Bonus (Flat)】\n" +
                "Increases frost elemental magic damage by a flat amount.\n" +
                "Recommended: 2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【Fire Attack Bonus (Flat)】\n" +
                "Increases fire elemental magic damage by a flat amount.\n" +
                "Recommended: 2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【Lightning Attack Bonus (Flat)】\n" +
                "Increases lightning elemental magic damage by a flat amount.\n" +
                "Recommended: 2-5",

                // === Tier 4: Lucky Mana ===
                ["Tier4_LuckyMana_Chance"] =
                "【Zero Eitr Cost Chance (%)】\n" +
                "Chance to cast spells without consuming Eitr.\n" +
                "Lucky mana enables infinite casting.\n" +
                "Recommended: 30-40%",

                // === Tier 5-1: Double Cast (R-key Active) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【Extra Projectile Count】\n" +
                "Additional magic projectiles fired per rapid barrage.\n" +
                "Recommended: 5~10",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【Projectile Damage Percent (%)】\n" +
                "Damage percentage of additional projectiles.\n" +
                "Recommended: 10-20%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【Angle Offset (Unused)】\n" +
                "Not used in current version. Fixed to same direction.",

                ["Tier5_DoubleCast_EitrCost"] =
                "【Eitr Cost】\n" +
                "Eitr consumed when activating the skill.\n" +
                "Recommended: 15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 25-40s",

                // === Tier 5-2: Instant Area Heal (H-key Active) ===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 25-40s",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【Eitr Cost】\n" +
                "Eitr consumed when using the skill.\n" +
                "Recommended: 25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【Heal Amount (% of Max HP)】\n" +
                "Healing percentage relative to maximum health.\n" +
                "Recommended: 20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【Heal Range (meters)】\n" +
                "Range where healing is applied.\n" +
                "Recommended: 10-15m",

                // === Staff Tree RequiredPoints ===
                ["Tier0_StaffExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Staff Expert node.\n" +
                "Recommended: 2",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Mental Focus node.\n" +
                "Recommended: 2",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Magic Flow node.\n" +
                "Recommended: 2",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Magic Amplification node.\n" +
                "Recommended: 3",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Frost Element node.\n" +
                "Recommended: 2",

                ["Tier3_FireElement_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Fire Element node.\n" +
                "Recommended: 2",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Lightning Element node.\n" +
                "Recommended: 2",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Lucky Mana node.\n" +
                "Recommended: 3",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Rapid Barrage node.\n" +
                "Recommended: 3",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Instant Area Heal node.\n" +
                "Recommended: 3",

                // === Crossbow Tree ===
                // === Tier 0: Crossbow Expert ===

                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Crossbow Expert node.\n" +
                "Recommended: 2",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【Crossbow Damage Bonus (%)】\n" +
                "Increases base damage of crossbows and bolts.\n" +
                "Recommended: 8-12%",

                // === Tier 1: Rapid Fire ===

                ["Tier1_RapidFire_Chance"] =
                "【Rapid Fire Trigger Chance (%)】\n" +
                "Chance to trigger rapid fire when shooting crossbow.\n" +
                "Fires multiple bolts rapidly when triggered.\n" +
                "Recommended: 15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【Rapid Fire Shot Count】\n" +
                "Number of additional bolts fired during rapid fire.\n" +
                "Recommended: 2-4 shots",

                ["Tier1_RapidFire_DamagePercent"] =
                "【Rapid Fire Damage Percent (%)】\n" +
                "Damage percentage of rapid fire bolts.\n" +
                "Relative to original attack power.\n" +
                "Recommended: 60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【Rapid Fire Interval (seconds)】\n" +
                "Time interval between each bolt during rapid fire.\n" +
                "Shorter means faster firing.\n" +
                "Recommended: 0.1-0.3s",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【Rapid Fire Bolt Consumption】\n" +
                "Number of bolts consumed during rapid fire.\n" +
                "Recommended: 1-2",

                ["Tier1_RapidFire_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Rapid Fire node.\n" +
                "Recommended: 2",

                // === Tier 2: Balanced Aim ===

                ["Tier2_BalancedAim_KnockbackChance"] =
                "【Knockback Trigger Chance (%)】\n" +
                "Chance to knock back enemies on crossbow hit.\n" +
                "Stable stance delivers reliable impact.\n" +
                "Recommended: 20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【Knockback Distance (meters)】\n" +
                "Distance enemies are pushed back when knockback triggers.\n" +
                "Recommended: 2-4m",

                // === Tier 2: Rapid Reload ===

                ["Tier2_RapidReload_SpeedIncrease"] =
                "【Reload Speed Increase (%)】\n" +
                "Increases crossbow reload speed.\n" +
                "Load the next bolt faster.\n" +
                "Recommended: 10-20%",

                // === Tier 2: Honest Shot ===

                ["Tier2_HonestShot_DamageBonus"] =
                "【Base Damage Bonus (%)】\n" +
                "Further increases crossbow base attack power.\n" +
                "Recommended: 10-18%",

                ["Tier2_BalancedAim_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Balanced Aim node.\n" +
                "Recommended: 2",

                ["Tier2_RapidReload_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Quick Reload node.\n" +
                "Recommended: 2",

                ["Tier2_HonestShot_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the True Shot node.\n" +
                "Recommended: 2",

                // === Tier 3: Auto Reload ===

                ["Tier3_AutoReload_Chance"] =
                "【Auto Reload Trigger Chance (%)】\n" +
                "Chance for next reload to be performed at 200% speed on hit.\n" +
                "Helps maintain continuous attack momentum.\n" +
                "Recommended: 20-35%",

                ["Tier3_AutoReload_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Auto Reload node.\n" +
                "Recommended: 2",

                // === Tier 4: Rapid Fire Lv2 ===


                ["Tier4_RapidFireLv2_Chance"] =
                "【Rapid Fire Lv2 Trigger Chance (%)】\n" +
                "Chance to trigger enhanced rapid fire.\n" +
                "Stacks with Tier 1 rapid fire chance.\n" +
                "Recommended: 20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【Rapid Fire Lv2 Shot Count】\n" +
                "Number of additional bolts in enhanced rapid fire.\n" +
                "Fires more than Tier 1 rapid fire.\n" +
                "Recommended: 4-6 shots",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【Rapid Fire Lv2 Damage Percent (%)】\n" +
                "Damage percentage of enhanced rapid fire bolts.\n" +
                "Higher damage than Tier 1.\n" +
                "Recommended: 75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【Rapid Fire Lv2 Interval (seconds)】\n" +
                "Time between each bolt in enhanced rapid fire.\n" +
                "Recommended: 0.1-0.3s",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【Rapid Fire Lv2 Bolt Consumption】\n" +
                "Number of bolts consumed during enhanced rapid fire.\n" +
                "Recommended: 1-2",

                // === Tier 4: First Strike ===

                ["Tier4_FinalStrike_HpThreshold"] =
                "【Enemy HP Threshold (%)】\n" +
                "Deals bonus damage to enemies at or above this HP percentage.\n" +
                "Effective against high-HP targets.\n" +
                "Recommended: 40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【Bonus Damage (%)】\n" +
                "Extra damage dealt to enemies above the HP threshold.\n" +
                "Recommended: 20-40%",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Rapid Fire Lv2 node.\n" +
                "Recommended: 3",

                ["Tier4_FinalStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the First Strike node.\n" +
                "Recommended: 3",

                // === Tier 5: One Shot (R-key Active) ===

                ["Tier5_OneShot_Duration"] =
                "【Buff Duration (seconds)】\n" +
                "Duration the One Shot buff lasts.\n" +
                "Can fire one enhanced shot during this time.\n" +
                "Recommended: 8-12s",

                ["Tier5_OneShot_DamageBonus"] =
                "【One Shot Damage Bonus (%)】\n" +
                "Additional damage bonus when firing One Shot.\n" +
                "Power of the devastating shot.\n" +
                "Recommended: 150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【Knockback Distance (meters)】\n" +
                "Distance enemies are knocked back on hit.\n" +
                "Powerful impact pushes enemies away.\n" +
                "Recommended: 5-10m",

                ["Tier5_OneShot_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 25-40s",

                ["Tier5_OneShot_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the One Shot node.\n" +
                "Recommended: 4",

                // === Bow Tree ===
                // === Tier 0: Bow Expert ===

                ["Tier0_BowExpert_DamageBonus"] =
                "【Bow Damage Bonus (%)】\n" +
                "Increases base damage of bows and arrows.\n" +
                "Recommended: 8-12%",

                // === Tier 1-1: Focused Shot ===

                ["Tier1_FocusedShot_CritBonus"] =
                "【Critical Chance Bonus (%)】\n" +
                "Focused shot increases critical hit chance.\n" +
                "Higher focus leads to more critical opportunities.\n" +
                "Recommended: 5-10%",

                // === Tier 1-2: Multi-Shot Lv1 ===

                ["Tier1_MultishotLv1_ActivationChance"] =
                "【Multi-Shot Lv1 Trigger Chance (%)】\n" +
                "Chance to trigger multishot when attacking with bow.\n" +
                "Fires multiple arrows simultaneously when triggered.\n" +
                "Recommended: 15-25%",

                ["Tier1_MultishotLv1_AdditionalArrows"] =
                "【Additional Arrow Count】\n" +
                "Number of extra arrows fired during multishot.\n" +
                "Recommended: 2-4",

                ["Tier1_MultishotLv1_ArrowConsumption"] =
                "【Arrow Consumption】\n" +
                "Number of arrows consumed during multishot.\n" +
                "Recommended: 1-2",

                ["Tier1_MultishotLv1_DamagePerArrow"] =
                "【Damage Per Arrow (%)】\n" +
                "Damage percentage for each multishot arrow.\n" +
                "Recommended: 50-70%",

                // === Tier 2: Bow Proficiency ===

                ["Tier2_BowMastery_SkillBonus"] =
                "【Bow Proficiency Bonus (Flat)】\n" +
                "Increases bow skill level by a flat amount.\n" +
                "Higher proficiency means stronger attacks.\n" +
                "Recommended: 5-10",

                ["Tier2_BowMastery_SpecialArrowChance"] =
                "【Special Arrow Trigger Chance (%)】\n" +
                "Chance to fire special effect arrows.\n" +
                "Can apply poison, fire, frost, or other status effects.\n" +
                "Recommended: 25-35%",

                // === Tier 3-1: Silent Shot ===

                ["Tier3_SilentStrike_DamageBonus"] =
                "【Silent Shot Damage Bonus (Flat)】\n" +
                "Increases bow damage by a flat amount.\n" +
                "Arrows penetrate enemies dealing more damage.\n" +
                "Recommended: 3-8",

                // === Tier 3-2: Multi-Shot Lv2 ===

                ["Tier3_MultishotLv2_ActivationChance"] =
                "【Multi-Shot Lv2 Trigger Chance (%)】\n" +
                "Enhanced multishot trigger chance.\n" +
                "Fires more arrows than Lv1.\n" +
                "Recommended: 20-30%",

                // === Tier 3-3: Hunter's Instinct ===

                ["Tier3_HuntingInstinct_CritBonus"] =
                "【Hunter's Instinct Crit Bonus (%)】\n" +
                "Hunter's instinct increases critical chance.\n" +
                "Recommended: 8-15%",

                // === Tier 4: Precision Aim & Advanced Skills ===

                ["Tier4_PrecisionAim_CritDamage"] =
                "【Critical Damage Bonus (%)】\n" +
                "Precision aim increases critical hit damage.\n" +
                "Target weak points for massive damage.\n" +
                "Recommended: 25-40%",

                // === Tier 5: Explosive Arrow (R-key Active) ===

                ["Tier5_ExplosiveArrow_DamageMultiplier"] =
                "【Explosive Arrow Damage (%)】\n" +
                "R-key active skill - Explosive arrow damage multiplier.\n" +
                "Powerful arrow that deals area damage.\n" +
                "Recommended: 100-150%",

                ["Tier5_ExplosiveArrow_Radius"] =
                "【Explosion Radius (meters)】\n" +
                "Damage radius of explosive arrow.\n" +
                "Recommended: 3-6m",

                ["Tier5_ExplosiveArrow_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 15-25s",

                ["Tier5_ExplosiveArrow_StaminaCost"] =
                "【Stamina Cost (%)】\n" +
                "Stamina percentage consumed when using skill.\n" +
                "Recommended: 10-20%",

                // === Bow Tree RequiredPoints ===
                ["Tier0_BowExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Bow Expert node.\n" +
                "Recommended: 2",

                ["Tier1_FocusedShot_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Focused Shot node.\n" +
                "Recommended: 2",

                ["Tier1_MultishotLv1_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Multi-Shot Lv1 node.\n" +
                "Recommended: 2",

                ["Tier2_BowMastery_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Bow Mastery node.\n" +
                "Recommended: 3",

                ["Tier3_SilentStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Silent Strike node.\n" +
                "Recommended: 2",

                ["Tier3_MultishotLv2_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Multi-Shot Lv2 node.\n" +
                "Recommended: 2",

                ["Tier3_HuntingInstinct_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Hunting Instinct node.\n" +
                "Recommended: 2",

                ["Tier4_PrecisionAim_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Precision Aim node.\n" +
                "Recommended: 3",

                ["Tier5_ExplosiveArrow_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points required to unlock the Explosive Arrow node.\n" +
                "Recommended: 4",


            };
        }
    }
}

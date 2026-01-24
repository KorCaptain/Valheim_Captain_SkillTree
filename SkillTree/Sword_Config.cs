using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 검 스킬 트리 Config 설정
    /// Mace_Config 패턴 기반 전면 재작성
    /// </summary>
    public static class Sword_Config
    {
        // ===== Tier 0: 검 전문가 (Sword Expert) =====

        /// <summary>
        /// 검 전문가 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordExpertRequiredPoints;

        /// <summary>
        /// 검 전문가 - 피해 증가 (%)
        /// </summary>
        public static ConfigEntry<float> SwordExpertDamageBonus;

        // ===== Tier 1: 빠른 베기 =====

        /// <summary>
        /// Tier 1 빠른 베기 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep1FastSlashRequiredPoints;

        /// <summary>
        /// Tier 1 빠른 베기 - 공격속도 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep1FastSlashSpeed;

        // ===== Tier 2: 연속 베기 =====

        /// <summary>
        /// Tier 2 연속 베기 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep2ComboSlashRequiredPoints;

        /// <summary>
        /// Tier 2 연속 베기 - 연속 공격 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep2ComboSlashBonus;

        /// <summary>
        /// Tier 2 연속 베기 - 버프 지속시간 (초)
        /// </summary>
        public static ConfigEntry<float> SwordStep2ComboSlashDuration;

        // ===== Tier 3: 칼날 되치기 =====

        /// <summary>
        /// Tier 3 칼날 되치기 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep3RiposteRequiredPoints;

        /// <summary>
        /// Tier 3 칼날 되치기 - 공격력 보너스 (고정값)
        /// </summary>
        public static ConfigEntry<float> SwordStep3RiposteDamageBonus;

        // ===== Tier 4: 진검승부 =====

        /// <summary>
        /// Tier 4 진검승부 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep4TrueDuelRequiredPoints;

        /// <summary>
        /// Tier 4 진검승부 - 공격속도 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep4TrueDuelSpeed;

        // ===== Tier 5: 방어 전환 =====

        /// <summary>
        /// Tier 5 방어 전환 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep5DefenseSwitchRequiredPoints;

        /// <summary>
        /// Tier 5 방어 전환 - 방패 착용 시 피해 감소 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep5DefenseSwitchShieldReduction;

        /// <summary>
        /// Tier 5 방어 전환 - 방패 미착용 시 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep5DefenseSwitchNoShieldBonus;

        // ===== Tier 6: Sword Slash 액티브 스킬 =====

        /// <summary>
        /// Tier 6 Sword Slash - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordSlashRequiredPoints;

        /// <summary>
        /// Sword Slash - 연속 공격 횟수
        /// </summary>
        public static ConfigEntry<int> SwordSlashAttackCount;

        /// <summary>
        /// Sword Slash - 공격 간격 (초)
        /// </summary>
        public static ConfigEntry<float> SwordSlashAttackInterval;

        /// <summary>
        /// Sword Slash - 1회 공격력 비율 (%)
        /// </summary>
        public static ConfigEntry<float> SwordSlashDamageRatio;

        /// <summary>
        /// Sword Slash - 스태미나 소모량
        /// </summary>
        public static ConfigEntry<float> SwordSlashStaminaCost;

        /// <summary>
        /// Sword Slash - 쿨타임 (초)
        /// </summary>
        public static ConfigEntry<float> SwordSlashCooldown;

        // ===== 패링 스택 시스템 (방어 전환 연계) =====

        /// <summary>
        /// 패링 스택 - 버프 지속시간 (초)
        /// 패링 성공 시 타이머 리셋
        /// </summary>
        public static ConfigEntry<float> ParryStackBuffDuration;

        /// <summary>
        /// 패링 스택 - 최대 스택 수 (3스택)
        /// </summary>
        public static ConfigEntry<int> MaxParryStacks;

        /// <summary>
        /// 패링 스택 - 1스택 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> ParryStack1DamageBonus;

        /// <summary>
        /// 패링 스택 - 2스택 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> ParryStack2DamageBonus;

        /// <summary>
        /// 패링 스택 - 3스택 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> ParryStack3DamageBonus;

        // ===== 동적 값 프로퍼티 (서버 동기화 지원) =====

        // === Tier 0: 검 전문가 ===
        public static int SwordExpertRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Expert_RequiredPoints", SwordExpertRequiredPoints?.Value ?? 2);
        public static float SwordExpertDamageValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Expert_DamageBonus", SwordExpertDamageBonus?.Value ?? 10f);

        // === Tier 1: 빠른 베기 ===
        public static int SwordStep1FastSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step1_RequiredPoints", SwordStep1FastSlashRequiredPoints?.Value ?? 1);
        public static float SwordStep1FastSlashSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step1_FastSlash_Speed", SwordStep1FastSlashSpeed?.Value ?? 10f);

        // === Tier 2: 연속 베기 ===
        public static int SwordStep2ComboSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step2_RequiredPoints", SwordStep2ComboSlashRequiredPoints?.Value ?? 1);
        public static float SwordStep2ComboSlashBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step2_ComboSlash_Bonus", SwordStep2ComboSlashBonus?.Value ?? 15f);
        public static float SwordStep2ComboSlashDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step2_ComboSlash_Duration", SwordStep2ComboSlashDuration?.Value ?? 5f);

        // === Tier 3: 칼날 되치기 ===
        public static int SwordStep3RiposteRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step3_RequiredPoints", SwordStep3RiposteRequiredPoints?.Value ?? 1);
        public static float SwordRiposteDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step3_Riposte_DamageBonus", SwordStep3RiposteDamageBonus?.Value ?? 5f);

        // === Tier 4: 진검승부 ===
        public static int SwordStep4TrueDuelRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step4_RequiredPoints", SwordStep4TrueDuelRequiredPoints?.Value ?? 1);
        public static float SwordStep4TrueDuelSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step4_TrueDuel_Speed", SwordStep4TrueDuelSpeed?.Value ?? 15f);

        // === Tier 5: 방어 전환 ===
        public static int SwordStep5DefenseSwitchRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step5_RequiredPoints", SwordStep5DefenseSwitchRequiredPoints?.Value ?? 1);
        public static float SwordDefenseSwitchDamageReductionValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step5_DefenseSwitch_ShieldReduction", SwordStep5DefenseSwitchShieldReduction?.Value ?? 10f);
        public static float SwordDefenseSwitchDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step5_DefenseSwitch_NoShieldBonus", SwordStep5DefenseSwitchNoShieldBonus?.Value ?? 15f);

        // === Tier 6: Sword Slash ===
        public static int SwordSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step6_RequiredPoints", SwordSlashRequiredPoints?.Value ?? 3);
        public static int SwordSlashAttackCountValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Slash_AttackCount", SwordSlashAttackCount?.Value ?? 3);
        public static float SwordSlashAttackIntervalValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Slash_AttackInterval", SwordSlashAttackInterval?.Value ?? 0.3f);
        public static float SwordSlashDamageRatioValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Slash_DamageRatio", SwordSlashDamageRatio?.Value ?? 80f);
        public static float SwordSlashStaminaCostValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Slash_StaminaCost", SwordSlashStaminaCost?.Value ?? 25f);
        public static float SwordSlashCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Slash_Cooldown", SwordSlashCooldown?.Value ?? 35f);
        public static float SwordSlashDurationValue => 1f; // 고정값

        // === 패링 스택 시스템 (방어 전환 연계) ===
        public static float ParryStackBuffDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_ParryStack_BuffDuration", ParryStackBuffDuration?.Value ?? 15f);
        public static int MaxParryStacksValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_ParryStack_MaxStacks", MaxParryStacks?.Value ?? 3);
        public static float ParryStack1DamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_ParryStack_1_DamageBonus", ParryStack1DamageBonus?.Value ?? 55f);
        public static float ParryStack2DamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_ParryStack_2_DamageBonus", ParryStack2DamageBonus?.Value ?? 120f);
        public static float ParryStack3DamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_ParryStack_3_DamageBonus", ParryStack3DamageBonus?.Value ?? 200f);

        // ===== 호환성 래퍼 (기존 코드 지원) =====

        /// <summary>
        /// Sword Slash 스킬 상세 정보 구조체
        /// </summary>
        public struct SwordSlashSkillData
        {
            public int attackCount;
            public float attackInterval;
            public float damageRatio;
            public float staminaCost;
            public float cooldown;
            public float duration;
            public string skillKey;
            public string requirement;
        }

        /// <summary>
        /// Sword Slash 스킬 데이터 가져오기
        /// </summary>
        public static SwordSlashSkillData GetSwordSlashData()
        {
            return new SwordSlashSkillData
            {
                attackCount = SwordSlashAttackCountValue,
                attackInterval = SwordSlashAttackIntervalValue,
                damageRatio = SwordSlashDamageRatioValue,
                staminaCost = SwordSlashStaminaCostValue,
                cooldown = SwordSlashCooldownValue,
                duration = SwordSlashDurationValue,
                skillKey = "G키",
                requirement = "검 착용"
            };
        }

        // ===== 기존 호환성 프로퍼티 (SkillTreeConfig 참조 -> 새 Config 참조) =====

        // 기존 코드에서 사용하던 프로퍼티들 (호환성 유지)
        public static float SwordExpertCritValue => 15f; // 기본값 유지
        public static float SwordStep1DamageValue => 10f;
        public static float SwordStep2CritValue => 8f;
        public static float SwordStep3ArmorValue => 12f;
        public static float SwordStep4BlockValue => 20f;
        public static float SwordStep1ExpertDurationValue => SwordStep2ComboSlashDurationValue;
        public static float SwordStep2ComboSlashDurationValueLegacy => SwordStep2ComboSlashDurationValue;
        public static bool IsConfigLoaded => true;

        // ===== 초기화 메서드 =====

        /// <summary>
        /// Sword Config 초기화
        /// </summary>
        /// <param name="config">BepInEx ConfigFile</param>
        public static void Initialize(ConfigFile config)
        {
            // Tier 0: 검 전문가 (Sword Expert)
            SwordExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier0_검전문가_필요포인트",
                2,
                "Tier 0: 검 전문가 - 필요 포인트"
            );

            SwordExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier0_검전문가_피해증가",
                10f,
                "Tier 0: 검 전문가 - 피해 증가 (%)"
            );

            // Tier 1: 빠른 베기
            SwordStep1FastSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_빠른베기_필요포인트",
                1,
                "Tier 1: 빠른 베기(sword_step1_fast_slash) - 필요 포인트"
            );

            SwordStep1FastSlashSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_빠른베기_공격속도보너스",
                10f,
                "Tier 1: 빠른 베기(sword_step1_fast_slash) - 공격속도 보너스 (%)"
            );

            // Tier 2: 연속 베기
            SwordStep2ComboSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_필요포인트",
                1,
                "Tier 2: 연속 베기(sword_step2_combo_slash) - 필요 포인트"
            );

            SwordStep2ComboSlashBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_보너스",
                15f,
                "Tier 2: 연속 베기(sword_step2_combo_slash) - 연속 공격 보너스 (%)"
            );

            SwordStep2ComboSlashDuration = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_지속시간",
                5f,
                "Tier 2: 연속 베기(sword_step2_combo_slash) - 버프 지속시간 (초)"
            );

            // Tier 3: 칼날 되치기
            SwordStep3RiposteRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier3_칼날되치기_필요포인트",
                1,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 필요 포인트"
            );

            SwordStep3RiposteDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier3_칼날되치기_공격력보너스",
                5f,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 공격력 보너스 (고정값)"
            );

            // Tier 4: 진검승부
            SwordStep4TrueDuelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_진검승부_필요포인트",
                1,
                "Tier 4: 진검승부(sword_step4_true_duel) - 필요 포인트"
            );

            SwordStep4TrueDuelSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_진검승부_공격속도보너스",
                15f,
                "Tier 4: 진검승부(sword_step4_true_duel) - 공격속도 보너스 (%)"
            );

            // Tier 5: 방어 전환
            SwordStep5DefenseSwitchRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_방어전환_필요포인트",
                1,
                "Tier 5: 방어 전환(sword_step5_defswitch) - 필요 포인트"
            );

            SwordStep5DefenseSwitchShieldReduction = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_방어전환_방패착용시피해감소",
                10f,
                "Tier 5: 방어 전환(sword_step5_defswitch) - 방패 착용 시 받는 피해 감소 (%)"
            );

            SwordStep5DefenseSwitchNoShieldBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_방어전환_방패미착용시공격력",
                15f,
                "Tier 5: 방어 전환(sword_step5_defswitch) - 방패 미착용 시 공격력 보너스 (%)"
            );

            // Tier 6: Sword Slash 액티브 스킬
            SwordSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_SwordSlash_필요포인트",
                3,
                "Tier 6: Sword Slash(sword_step6_slash) - 필요 포인트"
            );

            SwordSlashAttackCount = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_SwordSlash_연속공격횟수",
                3,
                "Tier 6: Sword Slash(sword_step6_slash) - 연속 공격 횟수"
            );

            SwordSlashAttackInterval = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_SwordSlash_공격간격",
                0.3f,
                "Tier 6: Sword Slash(sword_step6_slash) - 공격 간격 (초)"
            );

            SwordSlashDamageRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_SwordSlash_1회공격력비율",
                80f,
                "Tier 6: Sword Slash(sword_step6_slash) - 1회 공격력 비율 (%)"
            );

            SwordSlashStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_SwordSlash_스태미나소모",
                25f,
                "Tier 6: Sword Slash(sword_step6_slash) - 스태미나 소모량"
            );

            SwordSlashCooldown = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_SwordSlash_쿨타임",
                35f,
                "Tier 6: Sword Slash(sword_step6_slash) - 쿨타임 (초)"
            );

            // 패링 스택 시스템 (방어 전환 스킬 연계)
            ParryStackBuffDuration = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "패링스택_버프지속시간",
                15f,
                "패링 스택 - 버프 지속시간 (초), 패링 성공 시 타이머 리셋"
            );

            MaxParryStacks = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "패링스택_최대스택",
                3,
                "패링 스택 - 최대 스택 수 (3스택)"
            );

            ParryStack1DamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "패링스택_1스택공격력",
                55f,
                "패링 스택 - 1스택 공격력 보너스 (%)"
            );

            ParryStack2DamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "패링스택_2스택공격력",
                120f,
                "패링 스택 - 2스택 공격력 보너스 (%)"
            );

            ParryStack3DamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "패링스택_3스택공격력",
                200f,
                "패링 스택 - 3스택 공격력 보너스 (%)"
            );

            Plugin.Log.LogInfo("[Sword Config] 검 스킬 Config 초기화 완료");
        }

        /// <summary>
        /// 실시간 스킬 데미지 계산
        /// </summary>
        public static float CalculateSwordSlashDamage(float baseDamage)
        {
            return baseDamage * (SwordSlashDamageRatioValue / 100f);
        }

        /// <summary>
        /// 총 스킬 지속시간 계산
        /// </summary>
        public static float CalculateTotalSkillDuration()
        {
            return (SwordSlashAttackCountValue - 1) * SwordSlashAttackIntervalValue + 0.4f;
        }

        /// <summary>
        /// 패링 스택에 따른 공격력 보너스 계산
        /// 1스택: 55%, 2스택: 120%, 3스택: 200%
        /// </summary>
        public static float CalculateParryDamageBonus(int stacks)
        {
            return stacks switch
            {
                1 => ParryStack1DamageBonusValue,
                2 => ParryStack2DamageBonusValue,
                >= 3 => ParryStack3DamageBonusValue,
                _ => 0f
            };
        }
    }
}

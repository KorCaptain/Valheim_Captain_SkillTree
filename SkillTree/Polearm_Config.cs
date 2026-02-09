using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폴암 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Polearm_Config
    {
        // === 폴암 전문가 스킬 설정 ===
        public static ConfigEntry<float> PolearmExpertRangeBonus;
        public static ConfigEntry<float> PolearmStep1SpinWheelDamage;
        public static ConfigEntry<float> PolearmStep1SuppressDamage;
        public static ConfigEntry<float> PolearmStep2HeroKnockbackChance;
        public static ConfigEntry<float> PolearmStep3AreaComboBonus;
        public static ConfigEntry<float> PolearmStep3AreaComboDuration;
        public static ConfigEntry<float> PolearmStep3GroundWheelDamage;
        public static ConfigEntry<float> PolearmStep4MoonRangeBonus;
        public static ConfigEntry<float> PolearmStep4MoonStaminaReduction;
        public static ConfigEntry<float> PolearmStep4ChargeDamageBonus;
        public static ConfigEntry<float> PolearmStep5KingHealthThreshold;
        public static ConfigEntry<float> PolearmStep5KingDamageBonus;
        public static ConfigEntry<float> PolearmStep5KingStaminaCost;
        public static ConfigEntry<float> PolearmStep5KingCooldown;
        public static ConfigEntry<float> PolearmStep5KingDuration;

        // === 폴암 전문가 접근 프로퍼티들 ===
        public static float PolearmExpertRangeBonusValue => SkillTreeConfig.GetEffectiveValue("polearm_expert_range_bonus", PolearmExpertRangeBonus.Value);
        public static float PolearmStep1SpinWheelDamageValue => SkillTreeConfig.GetEffectiveValue("polearm_step1_spin_wheel_damage", PolearmStep1SpinWheelDamage.Value);
        public static float PolearmStep1SuppressDamageValue => SkillTreeConfig.GetEffectiveValue("polearm_step1_suppress_damage", PolearmStep1SuppressDamage.Value);
        public static float PolearmStep2HeroKnockbackChanceValue => SkillTreeConfig.GetEffectiveValue("polearm_step2_hero_knockback_chance", PolearmStep2HeroKnockbackChance.Value);
        public static float PolearmStep3AreaComboBonusValue => SkillTreeConfig.GetEffectiveValue("polearm_step3_area_combo_bonus", PolearmStep3AreaComboBonus.Value);
        public static float PolearmStep3AreaComboDurationValue => SkillTreeConfig.GetEffectiveValue("polearm_step3_area_combo_duration", PolearmStep3AreaComboDuration.Value);
        public static float PolearmStep3GroundWheelDamageValue => SkillTreeConfig.GetEffectiveValue("polearm_step3_ground_wheel_damage", PolearmStep3GroundWheelDamage.Value);
        public static float PolearmStep4MoonRangeBonusValue => SkillTreeConfig.GetEffectiveValue("polearm_step4_moon_range_bonus", PolearmStep4MoonRangeBonus.Value);
        public static float PolearmStep4MoonStaminaReductionValue => SkillTreeConfig.GetEffectiveValue("polearm_step4_moon_stamina_reduction", PolearmStep4MoonStaminaReduction.Value);
        public static float PolearmStep4ChargeDamageBonusValue => SkillTreeConfig.GetEffectiveValue("polearm_step4_charge_damage", PolearmStep4ChargeDamageBonus.Value);
        public static float PolearmStep5KingHealthThresholdValue => SkillTreeConfig.GetEffectiveValue("polearm_step5_king_health_threshold", PolearmStep5KingHealthThreshold.Value);
        public static float PolearmStep5KingDamageBonusValue => SkillTreeConfig.GetEffectiveValue("polearm_step5_king_damage_bonus", PolearmStep5KingDamageBonus.Value);
        public static float PolearmStep5KingStaminaCostValue => SkillTreeConfig.GetEffectiveValue("polearm_step5_king_stamina_cost", PolearmStep5KingStaminaCost.Value);
        public static float PolearmStep5KingCooldownValue => SkillTreeConfig.GetEffectiveValue("polearm_step5_king_cooldown", PolearmStep5KingCooldown.Value);
        public static float PolearmStep5KingDurationValue => SkillTreeConfig.GetEffectiveValue("polearm_step5_king_duration", PolearmStep5KingDuration.Value);

        public static void Initialize(ConfigFile config)
        {
            // Tier 0: 폴암 전문가
            PolearmExpertRangeBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier0_폴암전문가_공격범위보너스", 15f,
                "Tier 0: 폴암 전문가(polearm_expert) - 공격 범위 보너스 (%)");

            // Tier 1: 회전베기
            PolearmStep1SpinWheelDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier1_회전베기_휠마우스공격력보너스", 60f,
                "Tier 1: 회전베기(polearm_step1_spin_wheel) - 휠 마우스 공격력 보너스 (%)");

            // Tier 5: 제압 공격 (티어 순서 변경: 폴암강화와 교환)
            PolearmStep1SuppressDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_제압공격_공격력보너스", 30f,
                "Tier 5: 제압 공격(polearm_step1_suppress) - 공격력 보너스 (%)");

            // Tier 2: 영웅 타격
            PolearmStep2HeroKnockbackChance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2_영웅타격_넉백발생확률", 27f,
                "Tier 2: 영웅 타격(polearm_step2_hero) - 넉백 발생 확률 (%)");

            // Tier 3: 광역 강타
            PolearmStep3AreaComboBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_광역강타_2연속공격보너스", 25f,
                "Tier 3: 광역 강타(polearm_step3_area_combo) - 2연속 공격 보너스 (%)");

            PolearmStep3AreaComboDuration = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_광역강타_2연속공격지속시간", 5f,
                "Tier 3: 광역 강타(polearm_step3_area_combo) - 2연속 공격 지속시간 (초)");

            // Tier 3: 지면 강타
            PolearmStep3GroundWheelDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_지면강타_휠마우스공격력보너스", 80f,
                "Tier 3: 지면 강타(polearm_step3_ground_wheel) - 휠 마우스 공격력 보너스 (%)");

            // Tier 4: 반달 베기
            PolearmStep4MoonRangeBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4_반달베기_공격범위보너스", 15f,
                "Tier 4: 반달 베기(polearm_step4_moon) - 공격 범위 보너스 (%)");

            PolearmStep4MoonStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4_반달베기_공격스태미나감소", 15f,
                "Tier 4: 반달 베기(polearm_step4_moon) - 공격 스태미나 감소 (%)");

            // Tier 3: 폴암강화 (티어 순서 변경: 제압 공격과 교환)
            PolearmStep4ChargeDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_폴암강화_무기공격력보너스", 5f,
                "Tier 3: 폴암강화(polearm_step4_charge) - 무기 공격력 보너스 (고정 수치)");

            // Tier 5: 장창의 제왕
            PolearmStep5KingHealthThreshold = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_장창의제왕_추가피해적용체력임계값", 50f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 추가 피해 적용 체력 임계값 (%)");

            PolearmStep5KingDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_장창의제왕_추가피해보너스", 50f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 추가 피해 보너스 (%)");

            PolearmStep5KingStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_장창의제왕_스태미나소모량", 15f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 스태미나 소모량 (%)");

            PolearmStep5KingCooldown = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_장창의제왕_쿨다운시간", 15f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 쿨다운 시간 (초)");

            PolearmStep5KingDuration = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_장창의제왕_지속시간", 10f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 지속시간 (초)");

            Plugin.Log.LogDebug("[Polearm_Config] 폴암 전문가 트리 설정 초기화 완료");
        }
    }
}

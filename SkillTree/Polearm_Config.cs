using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폴암 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Polearm_Config
    {
        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> PolearmExpertRequiredPoints;
        public static ConfigEntry<int> PolearmStep1RequiredPoints;
        public static ConfigEntry<int> PolearmStep2RequiredPoints;
        public static ConfigEntry<int> PolearmStep3RequiredPoints;
        public static ConfigEntry<int> PolearmStep4RequiredPoints;
        public static ConfigEntry<int> PolearmSuppressRequiredPoints;
        public static ConfigEntry<int> PolearmKingRequiredPoints;

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

        // === 관통 돌격 (Pierce Charge) 액티브 스킬 설정 ===
        public static ConfigEntry<float> PolearmPierceChargeDashDistance;
        public static ConfigEntry<float> PolearmPierceChargePrimaryDamage;
        public static ConfigEntry<float> PolearmPierceChargeAoeDamage;
        public static ConfigEntry<float> PolearmPierceChargeAoeAngle;
        public static ConfigEntry<float> PolearmPierceChargeAoeRadius;
        public static ConfigEntry<float> PolearmPierceChargeKnockbackDistance;
        public static ConfigEntry<float> PolearmPierceChargeStaminaCost;
        public static ConfigEntry<float> PolearmPierceChargeCooldown;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int PolearmExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_expert_required_points", PolearmExpertRequiredPoints?.Value ?? 2);
        public static int PolearmStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step1_required_points", PolearmStep1RequiredPoints?.Value ?? 2);
        public static int PolearmStep2RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step2_required_points", PolearmStep2RequiredPoints?.Value ?? 2);
        public static int PolearmStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step3_required_points", PolearmStep3RequiredPoints?.Value ?? 2);
        public static int PolearmStep4RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step4_required_points", PolearmStep4RequiredPoints?.Value ?? 2);
        public static int PolearmSuppressRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_suppress_required_points", PolearmSuppressRequiredPoints?.Value ?? 3);
        public static int PolearmKingRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_king_required_points", PolearmKingRequiredPoints?.Value ?? 3);

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

        // === 관통 돌격 접근 프로퍼티들 ===
        public static float PolearmPierceChargeDashDistanceValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_dash_distance", PolearmPierceChargeDashDistance.Value);
        public static float PolearmPierceChargePrimaryDamageValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_primary_damage", PolearmPierceChargePrimaryDamage.Value);
        public static float PolearmPierceChargeAoeDamageValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_aoe_damage", PolearmPierceChargeAoeDamage.Value);
        public static float PolearmPierceChargeAoeAngleValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_aoe_angle", PolearmPierceChargeAoeAngle.Value);
        public static float PolearmPierceChargeAoeRadiusValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_aoe_radius", PolearmPierceChargeAoeRadius.Value);
        public static float PolearmPierceChargeKnockbackDistanceValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_knockback_distance", PolearmPierceChargeKnockbackDistance.Value);
        public static float PolearmPierceChargeStaminaCostValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_stamina_cost", PolearmPierceChargeStaminaCost.Value);
        public static float PolearmPierceChargeCooldownValue => SkillTreeConfig.GetEffectiveValue("polearm_pierce_charge_cooldown", PolearmPierceChargeCooldown.Value);

        public static void Initialize(ConfigFile config)
        {
            // === 필요 포인트 설정 ===
            PolearmExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier0_폴암전문가_필요포인트", 2,
                "Tier 0: 폴암 전문가(polearm_expert) - 필요 포인트");

            PolearmStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier1_폴암스킬_필요포인트", 2,
                "Tier 1: 폴암 스킬 - 필요 포인트");

            PolearmStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2_폴암스킬_필요포인트", 2,
                "Tier 2: 폴암 스킬 - 필요 포인트");

            PolearmStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_폴암스킬_필요포인트", 2,
                "Tier 3: 폴암 스킬 - 필요 포인트");

            PolearmStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4_폴암스킬_필요포인트", 2,
                "Tier 4: 폴암 스킬 - 필요 포인트");

            PolearmSuppressRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_제압공격_필요포인트", 3,
                "Tier 5: 제압 공격(polearm_step1_suppress) - 필요 포인트");

            PolearmKingRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_필요포인트", 3,
                "Tier 6: 관통 돌격(G키 액티브) - 필요 포인트");

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

            // Tier 6: 관통 돌격 (Pierce Charge) - G키 액티브 스킬
            PolearmPierceChargeDashDistance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_돌진거리", 8f,
                "Tier 6: 관통 돌격(polearm_step5_king) - 전방 돌진 거리 (m)");

            PolearmPierceChargePrimaryDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_첫타격공격력보너스", 200f,
                "Tier 6: 관통 돌격(polearm_step5_king) - 첫 관통 타격 공격력 보너스 (%)");

            PolearmPierceChargeAoeDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_AOE공격력보너스", 150f,
                "Tier 6: 관통 돌격(polearm_step5_king) - AOE 넉백 공격력 보너스 (%)");

            PolearmPierceChargeAoeAngle = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_AOE각도", 40f,
                "Tier 6: 관통 돌격(polearm_step5_king) - AOE 넉백 각도 (도)");

            PolearmPierceChargeAoeRadius = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_AOE반경", 3f,
                "Tier 6: 관통 돌격(polearm_step5_king) - AOE 넉백 반경 (m)");

            PolearmPierceChargeKnockbackDistance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_넉백거리", 5f,
                "Tier 6: 관통 돌격(polearm_step5_king) - 넉백 거리 (m)");

            PolearmPierceChargeStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_스태미나소모량", 20f,
                "Tier 6: 관통 돌격(polearm_step5_king) - 스태미나 소모량 (고정값)");

            PolearmPierceChargeCooldown = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_관통돌격_쿨다운시간", 30f,
                "Tier 6: 관통 돌격(polearm_step5_king) - 쿨다운 시간 (초)");

            Plugin.Log.LogDebug("[Polearm_Config] 폴암 전문가 트리 설정 초기화 완료");
        }
    }
}

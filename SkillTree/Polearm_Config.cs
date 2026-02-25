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
                "Polearm Tree", "Tier0_PolearmExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier0_PolearmExpert_RequiredPoints"));

            PolearmStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier1_PolearmSkill_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_PolearmSkill_RequiredPoints"));

            PolearmStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2_PolearmSkill_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_PolearmSkill_RequiredPoints"));

            PolearmStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_PolearmSkill_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier3_PolearmSkill_RequiredPoints"));

            PolearmStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4_PolearmSkill_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4_PolearmSkill_RequiredPoints"));

            PolearmSuppressRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_Suppress_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_Suppress_RequiredPoints"));

            PolearmKingRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_RequiredPoints"));

            // Tier 0: 폴암 전문가
            PolearmExpertRangeBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier0_PolearmExpert_AttackRangeBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier0_PolearmExpert_AttackRangeBonus"));

            // Tier 1: 회전베기
            PolearmStep1SpinWheelDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier1_SpinWheel_WheelAttackDamageBonus", 60f,
                SkillTreeConfig.GetConfigDescription("Tier1_SpinWheel_WheelAttackDamageBonus"));

            // Tier 5: 제압 공격 (티어 순서 변경: 폴암강화와 교환)
            PolearmStep1SuppressDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_Suppress_DamageBonus", 30f,
                SkillTreeConfig.GetConfigDescription("Tier5_Suppress_DamageBonus"));

            // Tier 2: 영웅 타격
            PolearmStep2HeroKnockbackChance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2_HeroStrike_KnockbackChance", 27f,
                SkillTreeConfig.GetConfigDescription("Tier2_HeroStrike_KnockbackChance"));

            // Tier 3: 광역 강타
            PolearmStep3AreaComboBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_AreaCombo_DoubleHitBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Tier3_AreaCombo_DoubleHitBonus"));

            PolearmStep3AreaComboDuration = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_AreaCombo_DoubleHitDuration", 5f,
                SkillTreeConfig.GetConfigDescription("Tier3_AreaCombo_DoubleHitDuration"));

            // Tier 3: 지면 강타
            PolearmStep3GroundWheelDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_GroundWheel_WheelAttackDamageBonus", 80f,
                SkillTreeConfig.GetConfigDescription("Tier3_GroundWheel_WheelAttackDamageBonus"));

            // Tier 4: 반달 베기
            PolearmStep4MoonRangeBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4_MoonSlash_AttackRangeBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier4_MoonSlash_AttackRangeBonus"));

            PolearmStep4MoonStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4_MoonSlash_StaminaReduction", 15f,
                SkillTreeConfig.GetConfigDescription("Tier4_MoonSlash_StaminaReduction"));

            // Tier 3: 폴암강화 (티어 순서 변경: 제압 공격과 교환)
            PolearmStep4ChargeDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_PolearmBoost_WeaponDamageBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier3_PolearmBoost_WeaponDamageBonus"));

            // Tier 6: 관통 돌격 (Pierce Charge) - G키 액티브 스킬
            PolearmPierceChargeDashDistance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_DashDistance", 10f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_DashDistance"));

            PolearmPierceChargePrimaryDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_FirstHitDamageBonus", 200f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_FirstHitDamageBonus"));

            PolearmPierceChargeAoeDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_AoeDamageBonus", 150f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_AoeDamageBonus"));

            PolearmPierceChargeAoeAngle = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_AoeAngle", 280f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_AoeAngle"));

            PolearmPierceChargeAoeRadius = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_AoeRadius", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_AoeRadius"));

            PolearmPierceChargeKnockbackDistance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_KnockbackDistance", 8f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_KnockbackDistance"));

            PolearmPierceChargeStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_StaminaCost", 20f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_StaminaCost"));

            PolearmPierceChargeCooldown = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier6_PierceCharge_Cooldown", 30f,
                SkillTreeConfig.GetConfigDescription("Tier6_PierceCharge_Cooldown"));

            Plugin.Log.LogDebug("[Polearm_Config] Polearm Expert tree config initialized");
        }
    }
}

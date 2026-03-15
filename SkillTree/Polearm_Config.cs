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
        public static ConfigEntry<int> PolearmSpinWheelRequiredPoints;
        public static ConfigEntry<int> PolearmPolearmBoostRequiredPoints;
        public static ConfigEntry<int> PolearmHeroStrikeRequiredPoints;
        public static ConfigEntry<int> PolearmAreaComboRequiredPoints;
        public static ConfigEntry<int> PolearmGroundWheelRequiredPoints;
        public static ConfigEntry<int> PolearmMoonSlashRequiredPoints;
        public static ConfigEntry<int> PolearmSuppressRequiredPoints;
        public static ConfigEntry<int> PolearmKingRequiredPoints;

        // === 폴암 전문가 스킬 설정 ===
        public static ConfigEntry<float> PolearmExpertRangeBonus;
        public static ConfigEntry<float> PolearmStep1SpinWheelDamage;
        public static ConfigEntry<float> PolearmStep1SuppressDamage;
        public static ConfigEntry<float> PolearmStep2HeroKnockbackChance;
        public static ConfigEntry<float> PolearmStep3AreaComboBonus;
        public static ConfigEntry<float> PolearmStep3AreaComboDuration;
        public static ConfigEntry<float> PolearmStep3StormSlashExplosion;
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
        public static int PolearmSpinWheelRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step1_required_points", PolearmSpinWheelRequiredPoints?.Value ?? 2);
        public static int PolearmPolearmBoostRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step2a_required_points", PolearmPolearmBoostRequiredPoints?.Value ?? 2);
        public static int PolearmHeroStrikeRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step2b_required_points", PolearmHeroStrikeRequiredPoints?.Value ?? 2);
        public static int PolearmAreaComboRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step3_required_points", PolearmAreaComboRequiredPoints?.Value ?? 2);
        public static int PolearmGroundWheelRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step4a_required_points", PolearmGroundWheelRequiredPoints?.Value ?? 2);
        public static int PolearmMoonSlashRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_step4b_required_points", PolearmMoonSlashRequiredPoints?.Value ?? 2);
        public static int PolearmSuppressRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_suppress_required_points", PolearmSuppressRequiredPoints?.Value ?? 3);
        public static int PolearmKingRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("polearm_king_required_points", PolearmKingRequiredPoints?.Value ?? 3);

        // === 폴암 전문가 접근 프로퍼티들 ===
        public static float PolearmExpertRangeBonusValue => SkillTreeConfig.GetEffectiveValue("polearm_expert_range_bonus", PolearmExpertRangeBonus.Value);
        public static float PolearmStep1SpinWheelDamageValue => SkillTreeConfig.GetEffectiveValue("polearm_step1_spin_wheel_damage", PolearmStep1SpinWheelDamage.Value);
        public static float PolearmStep1SuppressDamageValue => SkillTreeConfig.GetEffectiveValue("polearm_step1_suppress_damage", PolearmStep1SuppressDamage.Value);
        public static float PolearmStep2HeroKnockbackChanceValue => SkillTreeConfig.GetEffectiveValue("polearm_step2_hero_knockback_chance", PolearmStep2HeroKnockbackChance.Value);
        public static float PolearmStep3AreaComboBonusValue => SkillTreeConfig.GetEffectiveValue("polearm_step3_area_combo_bonus", PolearmStep3AreaComboBonus.Value);
        public static float PolearmStep3AreaComboDurationValue => SkillTreeConfig.GetEffectiveValue("polearm_step3_area_combo_duration", PolearmStep3AreaComboDuration.Value);
        public static float PolearmStep3StormSlashExplosionValue => SkillTreeConfig.GetEffectiveValue("polearm_step3_stormslash_explosion", PolearmStep3StormSlashExplosion.Value);
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
            // Tier 0: 폴암 전문가
            PolearmExpertRangeBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier0_PolearmExpert_AttackRangeBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier0_PolearmExpert_AttackRangeBonus"));
            PolearmExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier0_PolearmExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier0_PolearmExpert_RequiredPoints"));

            // Tier 1: 회전베기
            PolearmStep1SpinWheelDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier1_SpinWheel_WheelAttackDamageBonus", 60f,
                SkillTreeConfig.GetConfigDescription("Tier1_SpinWheel_WheelAttackDamageBonus"));
            PolearmSpinWheelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier1_SpinWheel_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_SpinWheel_RequiredPoints"));

            // Tier 2-1: 폴암강화
            PolearmStep4ChargeDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2-1_PolearmBoost_WeaponDamageBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2-1_PolearmBoost_WeaponDamageBonus"));
            PolearmPolearmBoostRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2-1_PolearmBoost_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2-1_PolearmBoost_RequiredPoints"));

            // Tier 2-2: 영웅 타격
            PolearmStep2HeroKnockbackChance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2-2_HeroStrike_KnockbackChance", 27f,
                SkillTreeConfig.GetConfigDescription("Tier2-2_HeroStrike_KnockbackChance"));
            PolearmHeroStrikeRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier2-2_HeroStrike_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2-2_HeroStrike_RequiredPoints"));

            // Tier 3: 광역 강타
            PolearmStep3AreaComboBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_AreaCombo_DoubleHitBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Tier3_AreaCombo_DoubleHitBonus"));
            PolearmStep3AreaComboDuration = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_AreaCombo_DoubleHitDuration", 5f,
                SkillTreeConfig.GetConfigDescription("Tier3_AreaCombo_DoubleHitDuration"));
            PolearmAreaComboRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier3_AreaCombo_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier3_AreaCombo_RequiredPoints"));

            // Tier 4-1: 폭풍베기
            PolearmStep3StormSlashExplosion = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4-1_StormSlash_ExplosionBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier4-1_StormSlash_ExplosionBonus"));
            PolearmGroundWheelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4-1_GroundWheel_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4-1_GroundWheel_RequiredPoints"));

            // Tier 4-2: 반달 베기
            PolearmStep4MoonRangeBonus = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4-2_MoonSlash_AttackRangeBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier4-2_MoonSlash_AttackRangeBonus"));
            PolearmStep4MoonStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4-2_MoonSlash_StaminaReduction", 15f,
                SkillTreeConfig.GetConfigDescription("Tier4-2_MoonSlash_StaminaReduction"));
            PolearmMoonSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4-2_MoonSlash_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4-2_MoonSlash_RequiredPoints"));

            // Tier 4-3: 제압 공격
            PolearmStep1SuppressDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4-3_Suppress_DamageBonus", 30f,
                SkillTreeConfig.GetConfigDescription("Tier4-3_Suppress_DamageBonus"));
            PolearmSuppressRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier4-3_Suppress_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier4-3_Suppress_RequiredPoints"));

            // Tier 5: 관통 돌격 (Pierce Charge) - G키 액티브 스킬
            PolearmPierceChargeDashDistance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_DashDistance", 10f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_DashDistance"));
            PolearmPierceChargePrimaryDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_FirstHitDamageBonus", 200f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_FirstHitDamageBonus"));
            PolearmPierceChargeAoeDamage = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_AoeDamageBonus", 150f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_AoeDamageBonus"));
            PolearmPierceChargeAoeAngle = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_AoeAngle", 280f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_AoeAngle"));
            PolearmPierceChargeAoeRadius = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_AoeRadius", 5f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_AoeRadius"));
            PolearmPierceChargeKnockbackDistance = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_KnockbackDistance", 8f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_KnockbackDistance"));
            PolearmPierceChargeStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_StaminaCost", 20f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_StaminaCost"));
            PolearmPierceChargeCooldown = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_Cooldown", 30f,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_Cooldown"));
            PolearmKingRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Polearm Tree", "Tier5_PierceCharge_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_PierceCharge_RequiredPoints"));

            Plugin.Log.LogDebug("[Polearm_Config] Polearm Expert tree config initialized");
        }
    }
}

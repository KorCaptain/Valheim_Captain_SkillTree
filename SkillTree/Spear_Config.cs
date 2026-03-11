using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 창 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Spear_Config
    {
        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> SpearExpertRequiredPoints;
        public static ConfigEntry<int> SpearStep1RequiredPoints;
        public static ConfigEntry<int> SpearStep2RequiredPoints;
        public static ConfigEntry<int> SpearStep3RequiredPoints;
        public static ConfigEntry<int> SpearStep4RequiredPoints;
        public static ConfigEntry<int> SpearPenetrateRequiredPoints;
        public static ConfigEntry<int> SpearComboRequiredPoints;

        // === 창 전문가 스킬 설정 ===
        public static ConfigEntry<float> SpearStep1AttackSpeed;
        public static ConfigEntry<float> SpearStep1DamageBonus;
        public static ConfigEntry<float> SpearStep1Duration;
        public static ConfigEntry<float> SpearStep1ThrowCooldown;
        public static ConfigEntry<float> SpearStep1ThrowDamage;
        public static ConfigEntry<float> SpearStep1CritDamageBonus;
        public static ConfigEntry<float> SpearStep2EvasionBonus;
        public static ConfigEntry<float> SpearStep2EvasionStaminaReduction;
        public static ConfigEntry<float> SpearStep3PierceDamageBonus;
        // 빠른창 설정
        public static ConfigEntry<float> SpearQuickAttackSpeed;
        // 이연창 설정
        public static ConfigEntry<float> SpearDualDamageBonus;
        public static ConfigEntry<float> SpearDualDuration;

        // === 꿰뚫는 창 (번개 충격) 설정 ===
        public static ConfigEntry<float> SpearStep5PenetrateBuffDuration;
        public static ConfigEntry<float> SpearStep5PenetrateLightningDamage;
        public static ConfigEntry<int> SpearStep5PenetrateComboCount;
        public static ConfigEntry<float> SpearStep5PenetrateCooldown;
        public static ConfigEntry<float> SpearStep5PenetrateStaminaCost;

        public static ConfigEntry<float> SpearStep5ComboCooldown;
        public static ConfigEntry<float> SpearStep5ComboDamage;
        public static ConfigEntry<float> SpearStep5ComboStaminaCost;
        public static ConfigEntry<float> SpearStep5ComboKnockbackRadius;
        public static ConfigEntry<float> SpearStep5ComboRange;
        public static ConfigEntry<float> SpearStep5ComboBuffDuration;
        public static ConfigEntry<int> SpearStep5ComboMaxUses;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int SpearExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_expert_required_points", SpearExpertRequiredPoints?.Value ?? 2);
        public static int SpearStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step1_required_points", SpearStep1RequiredPoints?.Value ?? 2);
        public static int SpearStep2RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step2_required_points", SpearStep2RequiredPoints?.Value ?? 2);
        public static int SpearStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step3_required_points", SpearStep3RequiredPoints?.Value ?? 2);
        public static int SpearStep4RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step4_required_points", SpearStep4RequiredPoints?.Value ?? 2);
        public static int SpearPenetrateRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_penetrate_required_points", SpearPenetrateRequiredPoints?.Value ?? 3);
        public static int SpearComboRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_combo_required_points", SpearComboRequiredPoints?.Value ?? 3);

        // === 창 전문가 접근 프로퍼티들 ===
        public static float SpearStep1AttackSpeedValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_attack_speed", SpearStep1AttackSpeed.Value);
        public static float SpearStep1DamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_damage_bonus", SpearStep1DamageBonus.Value);
        public static float SpearStep1DurationValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_duration", SpearStep1Duration.Value);
        public static float SpearStep2ThrowCooldownValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_throw_cooldown", SpearStep1ThrowCooldown.Value);
        public static float SpearStep2ThrowDamageValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_throw_damage", SpearStep1ThrowDamage.Value);
        public static float SpearStep2CritDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_crit_damage_bonus", SpearStep1CritDamageBonus.Value);
        public static float SpearStep3EvasionBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step2_evasion_bonus", SpearStep2EvasionBonus.Value);
        public static float SpearStep3EvasionStaminaReductionValue => SkillTreeConfig.GetEffectiveValue("spear_Step2_evasion_stamina_reduction", SpearStep2EvasionStaminaReduction.Value);
        public static float SpearStep3PierceDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step3_pierce_damage", SpearStep3PierceDamageBonus.Value);
        // 빠른창 접근 프로퍼티
        public static float SpearQuickAttackSpeedValue => SkillTreeConfig.GetEffectiveValue("spear_Step3_quick_attack_speed", SpearQuickAttackSpeed.Value);
        // 이연창 접근 프로퍼티
        public static float SpearDualDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_dual_damage_bonus", SpearDualDamageBonus.Value);
        public static float SpearDualDurationValue => SkillTreeConfig.GetEffectiveValue("spear_dual_duration", SpearDualDuration.Value);

        // === 꿰뚫는 창 (번개 충격) 접근 프로퍼티 ===
        public static float SpearStep6PenetrateBuffDurationValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_buff_duration", SpearStep5PenetrateBuffDuration.Value);
        public static float SpearStep6PenetrateLightningDamageValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_lightning_damage", SpearStep5PenetrateLightningDamage.Value);
        public static int SpearStep6PenetrateComboCountValue => (int)SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_combo_count", SpearStep5PenetrateComboCount.Value);
        public static float SpearStep6PenetrateCooldownValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_cooldown", SpearStep5PenetrateCooldown.Value);
        public static float SpearStep6PenetrateStaminaCostValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_stamina_cost", SpearStep5PenetrateStaminaCost.Value);

        public static float SpearStep6ComboCooldownValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_cooldown", SpearStep5ComboCooldown.Value);
        public static float SpearStep6ComboDamageValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_damage", SpearStep5ComboDamage.Value);
        public static float SpearStep6ComboStaminaCostValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_stamina_cost", SpearStep5ComboStaminaCost.Value);
        public static float SpearStep6ComboKnockbackRadiusValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_knockback_radius", SpearStep5ComboKnockbackRadius.Value);
        public static float SpearStep2ThrowRangeValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_range", SpearStep5ComboRange.Value);
        public static float SpearStep2ThrowStaminaCostValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_stamina_cost", SpearStep5ComboStaminaCost.Value);
        public static float SpearStep6ComboBuffDurationValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_buff_duration", SpearStep5ComboBuffDuration.Value);
        public static int SpearStep6ComboMaxUsesValue => (int)SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_max_uses", SpearStep5ComboMaxUses.Value);

        public static void Initialize(ConfigFile config)
        {
            // === 필요 포인트 설정 ===
            SpearExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier0_SpearExpert_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier0_SpearExpert_RequiredPoints"));

            SpearStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier1_QuickStrike_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier1_QuickStrike_RequiredPoints"));

            SpearStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier2_Throw_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier2_Throw_RequiredPoints"));

            SpearStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier3_Pierce_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier3_Pierce_RequiredPoints"));

            SpearStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier4_Evasion_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier4_Evasion_RequiredPoints"));

            SpearPenetrateRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Penetrate_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier5_Penetrate_RequiredPoints"));

            SpearComboRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_RequiredPoints"));

            SpearStep1AttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier0_SpearExpert_2HitAttackSpeed",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier0_SpearExpert_2HitAttackSpeed"));

            SpearStep1DamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier0_SpearExpert_2HitDamageBonus",
                7f,
                SkillTreeConfig.GetConfigDescription("Tier0_SpearExpert_2HitDamageBonus"));

            SpearStep1Duration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier0_SpearExpert_EffectDuration",
                4f,
                SkillTreeConfig.GetConfigDescription("Tier0_SpearExpert_EffectDuration"));

            SpearStep1ThrowCooldown = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier2_Throw_Cooldown",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier2_Throw_Cooldown"));

            SpearStep1ThrowDamage = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier2_Throw_DamageMultiplier",
                120f,
                SkillTreeConfig.GetConfigDescription("Tier2_Throw_DamageMultiplier"));

            SpearStep1CritDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier1_VitalStrike_DamageBonus",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier1_VitalStrike_DamageBonus"));

            SpearStep3PierceDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier3_Rapid_DamageBonus",
                4f,
                SkillTreeConfig.GetConfigDescription("Tier3_Rapid_DamageBonus"));

            SpearStep2EvasionBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier4_Evasion_EvasionBonus",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier4_Evasion_EvasionBonus"));

            SpearStep2EvasionStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier4_Evasion_StaminaReduction",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier4_Evasion_StaminaReduction"));

            // === 빠른창 설정 ===
            SpearQuickAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier3_QuickSpear_AttackSpeed",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier3_QuickSpear_AttackSpeed"));

            // === 이연창 설정 ===
            SpearDualDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier4_Dual_DamageBonus",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier4_Dual_DamageBonus"));

            SpearDualDuration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier4_Dual_Duration",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier4_Dual_Duration"));

            SpearStep5PenetrateBuffDuration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Penetrate_BuffDuration",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier5_Penetrate_BuffDuration"));

            SpearStep5PenetrateLightningDamage = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Penetrate_LightningDamage",
                260f,
                SkillTreeConfig.GetConfigDescription("Tier5_Penetrate_LightningDamage"));

            SpearStep5PenetrateComboCount = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Penetrate_HitCount",
                3,
                SkillTreeConfig.GetConfigDescription("Tier5_Penetrate_HitCount"));

            SpearStep5PenetrateCooldown = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Penetrate_GKey_Cooldown",
                60f,
                SkillTreeConfig.GetConfigDescription("Tier5_Penetrate_GKey_Cooldown"));

            SpearStep5PenetrateStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Penetrate_GKey_StaminaCost",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier5_Penetrate_GKey_StaminaCost"));

            SpearStep5ComboCooldown = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_HKey_Cooldown",
                25f,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_HKey_Cooldown"));

            SpearStep5ComboDamage = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_HKey_DamageMultiplier",
                280f,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_HKey_DamageMultiplier"));

            SpearStep5ComboStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_HKey_StaminaCost",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_HKey_StaminaCost"));

            SpearStep5ComboKnockbackRadius = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_HKey_KnockbackRange",
                3f,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_HKey_KnockbackRange"));

            SpearStep5ComboRange = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_ActiveRange",
                3f,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_ActiveRange"));

            SpearStep5ComboBuffDuration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_BuffDuration",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_BuffDuration"));

            SpearStep5ComboMaxUses = SkillTreeConfig.BindServerSync(config,
                "Spear Tree",
                "Tier5_Combo_MaxUses",
                3,
                SkillTreeConfig.GetConfigDescription("Tier5_Combo_MaxUses"));

            Plugin.Log.LogDebug("[Spear_Config] 창 전문가 트리 설정 초기화 완료");
        }
    }
}

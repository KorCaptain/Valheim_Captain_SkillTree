using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 활 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Bow_Config
    {
        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> BowExpertRequiredPoints;
        public static ConfigEntry<int> BowStep2RequiredPoints;
        public static ConfigEntry<int> BowMultishotRequiredPoints;
        public static ConfigEntry<int> BowStep3RequiredPoints;
        public static ConfigEntry<int> BowStep4RequiredPoints;
        public static ConfigEntry<int> BowStep5RequiredPoints;
        public static ConfigEntry<int> BowExplosiveArrowRequiredPoints;

        // === 활 전문가 멀티샷 패시브 스킬 설정 ===
        public static ConfigEntry<float> BowMultishotLv1Chance;
        public static ConfigEntry<float> BowMultishotLv2Chance;
        public static ConfigEntry<int> BowMultishotArrowCount;
        public static ConfigEntry<int> BowMultishotArrowConsumption;
        public static ConfigEntry<float> BowMultishotDamagePercent;

        // === 활 전문가 스킬 설정 ===
        public static ConfigEntry<float> BowStep1ExpertDamageBonus;
        public static ConfigEntry<float> BowStep2FocusCritBonus;
        public static ConfigEntry<float> BowStep3SpeedShotSkillBonus;
        public static ConfigEntry<float> BowStep3SilentShotDamageBonus;
        public static ConfigEntry<float> BowStep3SpecialArrowChance;
        public static ConfigEntry<float> BowStep4PowerShotKnockbackChance;
        public static ConfigEntry<float> BowStep4PowerShotKnockbackPower;
        public static ConfigEntry<float> BowStep5InstinctCritBonus;
        public static ConfigEntry<float> BowStep5MasterCritDamage;
        public static ConfigEntry<float> BowStep5ArrowRainChance;
        public static ConfigEntry<int> BowStep5ArrowRainCount;
        public static ConfigEntry<float> BowStep5BackstepShotCritBonus;
        public static ConfigEntry<float> BowStep5BackstepShotWindow;
        public static ConfigEntry<float> BowStep6CritBoostDamageBonus;
        public static ConfigEntry<float> BowStep6CritBoostCritChance;
        public static ConfigEntry<int> BowStep6CritBoostArrowCount;
        public static ConfigEntry<float> BowStep6CritBoostCooldown;
        public static ConfigEntry<float> BowStep6CritBoostStaminaCost;
        public static ConfigEntry<float> BowExplosiveArrowDamage;
        public static ConfigEntry<float> BowExplosiveArrowCooldown;
        public static ConfigEntry<float> BowExplosiveArrowStaminaCost;
        public static ConfigEntry<float> BowExplosiveArrowRadius;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int BowExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_expert_required_points", BowExpertRequiredPoints?.Value ?? 2);
        public static int BowStep2RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_step2_required_points", BowStep2RequiredPoints?.Value ?? 2);
        public static int BowMultishotRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_multishot_required_points", BowMultishotRequiredPoints?.Value ?? 2);
        public static int BowStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_step3_required_points", BowStep3RequiredPoints?.Value ?? 2);
        public static int BowStep4RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_step4_required_points", BowStep4RequiredPoints?.Value ?? 3);
        public static int BowStep5RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_step5_required_points", BowStep5RequiredPoints?.Value ?? 3);
        public static int BowExplosiveArrowRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_explosive_required_points", BowExplosiveArrowRequiredPoints?.Value ?? 4);

        // === 활 전문가 접근 프로퍼티들 ===
        public static float BowMultishotLv1ChanceValue => SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_Lv1_Chance", BowMultishotLv1Chance.Value);
        public static float BowMultishotLv2ChanceValue => SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_Lv2_Chance", BowMultishotLv2Chance.Value);
        public static int BowMultishotArrowCountValue => (int)SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_ArrowCount", BowMultishotArrowCount.Value);
        public static int BowMultishotArrowConsumptionValue => (int)SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_ArrowConsumption", BowMultishotArrowConsumption.Value);
        public static float BowMultishotDamagePercentValue => SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_DamagePercent", BowMultishotDamagePercent.Value);
        public static float BowStep1ExpertDamageBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step1_expert_damage_bonus", BowStep1ExpertDamageBonus.Value);
        public static float BowStep2FocusCritBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step2_focus_crit_bonus", BowStep2FocusCritBonus.Value);
        public static float BowStep3SpeedShotSkillBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step3_speedshot_skill_bonus", BowStep3SpeedShotSkillBonus.Value);
        public static float BowStep3SilentShotDamageBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step3_silentshot_damage_bonus", BowStep3SilentShotDamageBonus.Value);
        public static float BowStep3SpecialArrowChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step3_special_arrow_chance", BowStep3SpecialArrowChance.Value);
        public static float BowStep4PowerShotKnockbackChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step4_powershot_knockback_chance", BowStep4PowerShotKnockbackChance.Value);
        public static float BowStep4PowerShotKnockbackPowerValue => SkillTreeConfig.GetEffectiveValue("bow_Step4_powershot_knockback_power", BowStep4PowerShotKnockbackPower.Value);
        public static float BowStep5ArrowRainChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_arrow_rain_chance", BowStep5ArrowRainChance.Value);
        public static int BowStep5ArrowRainCountValue => (int)SkillTreeConfig.GetEffectiveValue("bow_Step5_arrow_rain_count", (float)BowStep5ArrowRainCount.Value);
        public static float BowStep5BackstepShotCritBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_backstep_shot_crit_bonus", BowStep5BackstepShotCritBonus.Value);
        public static float BowStep5BackstepShotWindowValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_backstep_shot_window", BowStep5BackstepShotWindow.Value);
        public static float BowStep5InstinctCritBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_instinct_crit_bonus", BowStep5InstinctCritBonus.Value);
        public static float BowStep5MasterCritDamageValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_master_crit_damage", BowStep5MasterCritDamage.Value);
        public static float BowStep6CritBoostDamageBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_damage_bonus", BowStep6CritBoostDamageBonus.Value);
        public static float BowStep6CritBoostCritChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_crit_chance", BowStep6CritBoostCritChance.Value);
        public static int BowStep6CritBoostArrowCountValue => (int)SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_arrow_count", (float)BowStep6CritBoostArrowCount.Value);
        public static float BowStep6CritBoostCooldownValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_cooldown", BowStep6CritBoostCooldown.Value);
        public static float BowStep6CritBoostStaminaCostValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_stamina_cost", BowStep6CritBoostStaminaCost.Value);
        public static float BowExplosiveArrowDamageValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_damage", BowExplosiveArrowDamage.Value);
        public static float BowExplosiveArrowCooldownValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_cooldown", BowExplosiveArrowCooldown.Value);
        public static float BowExplosiveArrowStaminaCostValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_stamina_cost", BowExplosiveArrowStaminaCost.Value);
        public static float BowExplosiveArrowRadiusValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_radius", BowExplosiveArrowRadius.Value);

        public static void Initialize(ConfigFile config)
        {
            // === 필요 포인트 설정 ===
            BowExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier0_BowExpert_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier0_BowExpert_RequiredPoints"));

            BowStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier1_FocusedShot_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier1_FocusedShot_RequiredPoints"));

            BowMultishotRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier2_MultishotLv1_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier2_MultishotLv1_RequiredPoints"));

            BowStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier3_BowMastery_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier3_BowMastery_RequiredPoints"));

            BowStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier4_MultishotLv2_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier4_MultishotLv2_RequiredPoints"));

            BowStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier5_PrecisionAim_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier5_PrecisionAim_RequiredPoints"));

            BowExplosiveArrowRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_ExplosiveArrow_RequiredPoints",
                4,
                SkillTreeConfig.GetConfigDescription("Tier6_ExplosiveArrow_RequiredPoints"));

            // === Bow Tree: 멀티샷 패시브 ===
            BowMultishotLv1Chance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier2_MultishotLv1_ActivationChance",
                15f,
                SkillTreeConfig.GetConfigDescription("Tier2_MultishotLv1_ActivationChance"));

            BowMultishotLv2Chance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier4_MultishotLv2_ActivationChance",
                36f,
                SkillTreeConfig.GetConfigDescription("Tier4_MultishotLv2_ActivationChance"));

            BowMultishotArrowCount = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier2_Multishot_AdditionalArrows",
                2,
                SkillTreeConfig.GetConfigDescription("Tier2_Multishot_AdditionalArrows"));

            BowMultishotArrowConsumption = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier2_Multishot_ArrowConsumption",
                0,
                SkillTreeConfig.GetConfigDescription("Tier2_Multishot_ArrowConsumption"));

            BowMultishotDamagePercent = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier2_Multishot_DamagePerArrow",
                70f,
                SkillTreeConfig.GetConfigDescription("Tier2_Multishot_DamagePerArrow"));

            // === Bow Tree: 공격 스킬 ===
            BowStep1ExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier0_BowExpert_DamageBonus",
                5f,
                SkillTreeConfig.GetConfigDescription("Tier0_BowExpert_DamageBonus"));

            BowStep2FocusCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier1_FocusedShot_CritBonus",
                7f,
                SkillTreeConfig.GetConfigDescription("Tier1_FocusedShot_CritBonus"));

            BowStep3SpeedShotSkillBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier3_SpeedShot_SkillBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier3_SpeedShot_SkillBonus"));

            BowStep3SilentShotDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier3_SilentShot_DamageBonus",
                3f,
                SkillTreeConfig.GetConfigDescription("Tier3_SilentShot_DamageBonus"));

            BowStep3SpecialArrowChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier3_SpecialArrow_Chance",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier3_SpecialArrow_Chance"));

            BowStep4PowerShotKnockbackChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier4_PowerShot_KnockbackChance",
                35f,
                SkillTreeConfig.GetConfigDescription("Tier4_PowerShot_KnockbackChance"));

            BowStep4PowerShotKnockbackPower = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier4_PowerShot_KnockbackDistance",
                5f,
                SkillTreeConfig.GetConfigDescription("Tier4_PowerShot_KnockbackDistance"));

            BowStep5ArrowRainChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier5_ArrowRain_Chance",
                29f,
                SkillTreeConfig.GetConfigDescription("Tier5_ArrowRain_Chance"));

            BowStep5ArrowRainCount = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier5_ArrowRain_ArrowCount",
                3,
                SkillTreeConfig.GetConfigDescription("Tier5_ArrowRain_ArrowCount"));

            BowStep5BackstepShotCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier5_BackstepShot_CritBonus",
                25f,
                SkillTreeConfig.GetConfigDescription("Tier5_BackstepShot_CritBonus"));

            BowStep5BackstepShotWindow = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier5_BackstepShot_Duration",
                3f,
                SkillTreeConfig.GetConfigDescription("Tier5_BackstepShot_Duration"));

            BowStep5InstinctCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier5_HuntingInstinct_CritBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier5_HuntingInstinct_CritBonus"));

            BowStep5MasterCritDamage = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier5_PrecisionAim_CritDamage",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier5_PrecisionAim_CritDamage"));

            BowStep6CritBoostDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_CritBoost_DamageBonus",
                50f,
                SkillTreeConfig.GetConfigDescription("Tier6_CritBoost_DamageBonus"));

            BowStep6CritBoostCritChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_CritBoost_CritChance",
                100f,
                SkillTreeConfig.GetConfigDescription("Tier6_CritBoost_CritChance"));

            BowStep6CritBoostArrowCount = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_CritBoost_ArrowCount",
                5,
                SkillTreeConfig.GetConfigDescription("Tier6_CritBoost_ArrowCount"));

            BowStep6CritBoostCooldown = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_CritBoost_Cooldown",
                45f,
                SkillTreeConfig.GetConfigDescription("Tier6_CritBoost_Cooldown"));

            BowStep6CritBoostStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_CritBoost_StaminaCost",
                25f,
                SkillTreeConfig.GetConfigDescription("Tier6_CritBoost_StaminaCost"));

            BowExplosiveArrowDamage = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_ExplosiveArrow_DamageMultiplier",
                120f,
                SkillTreeConfig.GetConfigDescription("Tier6_ExplosiveArrow_DamageMultiplier"));

            BowExplosiveArrowCooldown = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_ExplosiveArrow_Cooldown",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier6_ExplosiveArrow_Cooldown"));

            BowExplosiveArrowStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_ExplosiveArrow_StaminaCost",
                15f,
                SkillTreeConfig.GetConfigDescription("Tier6_ExplosiveArrow_StaminaCost"));

            BowExplosiveArrowRadius = SkillTreeConfig.BindServerSync(config,
                "Bow Tree",
                "Tier6_ExplosiveArrow_Radius",
                4f,
                SkillTreeConfig.GetConfigDescription("Tier6_ExplosiveArrow_Radius"));

            Plugin.Log.LogDebug("[Bow_Config] 활 전문가 트리 설정 초기화 완료");
        }
    }
}

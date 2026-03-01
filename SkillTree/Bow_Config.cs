using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 활 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Bow_Config
    {
        // === 필요 포인트 설정 ===
        // Tier 0: 활 전문가
        public static ConfigEntry<int> BowExpertRequiredPoints;
        // Tier 1-1: 집중 사격
        public static ConfigEntry<int> BowFocusShotRequiredPoints;
        // Tier 1-2: 멀티샷 Lv1
        public static ConfigEntry<int> BowMultishotRequiredPoints;
        // Tier 2: 활 숙련
        public static ConfigEntry<int> BowStep3RequiredPoints;
        // Tier 3-1: 침묵의 일격
        public static ConfigEntry<int> BowSilentStrikeRequiredPoints;
        // Tier 3-2: 멀티샷 Lv2
        public static ConfigEntry<int> BowStep4RequiredPoints;
        // Tier 3-3: 사냥 본능
        public static ConfigEntry<int> BowHuntingInstinctRequiredPoints;
        // Tier 4: 정조준
        public static ConfigEntry<int> BowStep5RequiredPoints;
        // Tier 5: 폭발 화살
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
        public static ConfigEntry<float> BowStep5InstinctCritBonus;
        public static ConfigEntry<float> BowStep5MasterCritDamage;
        public static ConfigEntry<float> BowExplosiveArrowDamage;
        public static ConfigEntry<float> BowExplosiveArrowCooldown;
        public static ConfigEntry<float> BowExplosiveArrowStaminaCost;
        public static ConfigEntry<float> BowExplosiveArrowRadius;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int BowExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_expert_required_points", BowExpertRequiredPoints?.Value ?? 2);
        public static int BowFocusShotRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_focusshot_required_points", BowFocusShotRequiredPoints?.Value ?? 2);
        public static int BowMultishotRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_multishot_required_points", BowMultishotRequiredPoints?.Value ?? 2);
        public static int BowStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_step3_required_points", BowStep3RequiredPoints?.Value ?? 2);
        public static int BowSilentStrikeRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_silentstrike_required_points", BowSilentStrikeRequiredPoints?.Value ?? 2);
        public static int BowStep4RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_step4_required_points", BowStep4RequiredPoints?.Value ?? 3);
        public static int BowHuntingInstinctRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("bow_huntinginstinct_required_points", BowHuntingInstinctRequiredPoints?.Value ?? 3);
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
        public static float BowStep5InstinctCritBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_instinct_crit_bonus", BowStep5InstinctCritBonus.Value);
        public static float BowStep5MasterCritDamageValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_master_crit_damage", BowStep5MasterCritDamage.Value);
        public static float BowExplosiveArrowDamageValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_damage", BowExplosiveArrowDamage.Value);
        public static float BowExplosiveArrowCooldownValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_cooldown", BowExplosiveArrowCooldown.Value);
        public static float BowExplosiveArrowStaminaCostValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_stamina_cost", BowExplosiveArrowStaminaCost.Value);
        public static float BowExplosiveArrowRadiusValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_radius", BowExplosiveArrowRadius.Value);

        public static void Initialize(ConfigFile config)
        {
            // === Tier 0: 활 전문가 ===
            BowStep1ExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier0_BowExpert_DamageBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier0_BowExpert_DamageBonus"), order: 60);

            BowExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier0_BowExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier0_BowExpert_RequiredPoints"), order: 59);

            // === Tier 1-1: [집중 사격] ===
            BowStep2FocusCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_FocusedShot_CritBonus", 7f,
                SkillTreeConfig.GetConfigDescription("Tier1_FocusedShot_CritBonus"), order: 50);

            BowFocusShotRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_FocusedShot_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_FocusedShot_RequiredPoints"), order: 49);

            // === Tier 1-2: [멀티샷 Lv1] ===
            BowMultishotLv1Chance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_MultishotLv1_ActivationChance", 15f,
                SkillTreeConfig.GetConfigDescription("Tier1_MultishotLv1_ActivationChance"), order: 48);

            BowMultishotArrowCount = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_MultishotLv1_AdditionalArrows", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_MultishotLv1_AdditionalArrows"), order: 48);

            BowMultishotArrowConsumption = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_MultishotLv1_ArrowConsumption", 0,
                SkillTreeConfig.GetConfigDescription("Tier1_MultishotLv1_ArrowConsumption"), order: 48);

            BowMultishotDamagePercent = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_MultishotLv1_DamagePerArrow", 70f,
                SkillTreeConfig.GetConfigDescription("Tier1_MultishotLv1_DamagePerArrow"), order: 48);

            BowMultishotRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_MultishotLv1_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_MultishotLv1_RequiredPoints"), order: 47);

            // === Tier 2: [활 숙련] ===
            BowStep3SpeedShotSkillBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier2_BowMastery_SkillBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier2_BowMastery_SkillBonus"), order: 40);

            BowStep3SpecialArrowChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier2_BowMastery_SpecialArrowChance", 30f,
                SkillTreeConfig.GetConfigDescription("Tier2_BowMastery_SpecialArrowChance"), order: 40);

            BowStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier2_BowMastery_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_BowMastery_RequiredPoints"), order: 39);

            // === Tier 3-1: [침묵의 일격] ===
            BowStep3SilentShotDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_SilentStrike_DamageBonus", 3f,
                SkillTreeConfig.GetConfigDescription("Tier3_SilentStrike_DamageBonus"), order: 32);

            BowSilentStrikeRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_SilentStrike_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier3_SilentStrike_RequiredPoints"), order: 31);

            // === Tier 3-2: [멀티샷 Lv2] ===
            BowMultishotLv2Chance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_MultishotLv2_ActivationChance", 36f,
                SkillTreeConfig.GetConfigDescription("Tier3_MultishotLv2_ActivationChance"), order: 30);

            BowStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_MultishotLv2_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier3_MultishotLv2_RequiredPoints"), order: 29);

            // === Tier 3-3: [사냥 본능] ===
            BowStep5InstinctCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_HuntingInstinct_CritBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier3_HuntingInstinct_CritBonus"), order: 28);

            BowHuntingInstinctRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_HuntingInstinct_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier3_HuntingInstinct_RequiredPoints"), order: 27);

            // === Tier 4: [정조준] ===
            BowStep5MasterCritDamage = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier4_PrecisionAim_CritDamage", 30f,
                SkillTreeConfig.GetConfigDescription("Tier4_PrecisionAim_CritDamage"), order: 20);

            BowStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier4_PrecisionAim_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier4_PrecisionAim_RequiredPoints"), order: 19);

            // === Tier 5: [폭발 화살] (R키 액티브) ===
            BowExplosiveArrowDamage = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_ExplosiveArrow_DamageMultiplier", 120f,
                SkillTreeConfig.GetConfigDescription("Tier5_ExplosiveArrow_DamageMultiplier"), order: 10);

            BowExplosiveArrowRadius = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_ExplosiveArrow_Radius", 4f,
                SkillTreeConfig.GetConfigDescription("Tier5_ExplosiveArrow_Radius"), order: 10);

            BowExplosiveArrowCooldown = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_ExplosiveArrow_Cooldown", 20f,
                SkillTreeConfig.GetConfigDescription("Tier5_ExplosiveArrow_Cooldown"), order: 10);

            BowExplosiveArrowStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_ExplosiveArrow_StaminaCost", 15f,
                SkillTreeConfig.GetConfigDescription("Tier5_ExplosiveArrow_StaminaCost"), order: 10);

            BowExplosiveArrowRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_ExplosiveArrow_RequiredPoints", 4,
                SkillTreeConfig.GetConfigDescription("Tier5_ExplosiveArrow_RequiredPoints"), order: 9);

            Plugin.Log.LogDebug("[Bow_Config] 활 전문가 트리 설정 초기화 완료");
        }
    }
}

using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 속도 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Speed_Config
    {
        // === 필요 포인트 설정 (단일 스킬 4개) ===
        public static ConfigEntry<int> SpeedRootRequiredPoints;
        public static ConfigEntry<int> SpeedStep1RequiredPoints;
        public static ConfigEntry<int> SpeedStep5RequiredPoints;
        public static ConfigEntry<int> SpeedStep7RequiredPoints;

        // === 필요 포인트 설정 (독립 스킬 15개) ===
        public static ConfigEntry<int> SpeedStep2_MeleeFlowRequiredPoints;
        public static ConfigEntry<int> SpeedStep2_CrossbowExpertRequiredPoints;
        public static ConfigEntry<int> SpeedStep2_BowExpertRequiredPoints;
        public static ConfigEntry<int> SpeedStep2_MobileCastRequiredPoints;
        public static ConfigEntry<int> SpeedStep3_Practitioner1RequiredPoints;
        public static ConfigEntry<int> SpeedStep3_Practitioner2RequiredPoints;
        public static ConfigEntry<int> SpeedStep4_EnergizerRequiredPoints;
        public static ConfigEntry<int> SpeedStep4_CaptainRequiredPoints;
        public static ConfigEntry<int> SpeedStep6_DexterityRequiredPoints;
        public static ConfigEntry<int> SpeedStep6_EnduranceRequiredPoints;
        public static ConfigEntry<int> SpeedStep6_IntellectRequiredPoints;
        public static ConfigEntry<int> SpeedStep8_MeleeAccelRequiredPoints;
        public static ConfigEntry<int> SpeedStep8_CrossbowAccelRequiredPoints;
        public static ConfigEntry<int> SpeedStep8_BowAccelRequiredPoints;
        public static ConfigEntry<int> SpeedStep8_CastAccelRequiredPoints;

        // === 티어0: 속도 전문가 ===
        public static ConfigEntry<float> SpeedRootMoveSpeed;

        // === 티어1: 민첩함의 기초 ===
        public static ConfigEntry<float> SpeedBaseDodgeMoveSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeDuration;
        public static ConfigEntry<float> SpeedBaseAttackSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeSpeed;

        // === 티어2: 무기별 특화 ===
        // 연속의 흐름 (근접)
        public static ConfigEntry<float> SpeedMeleeComboAttackSpeed;
        public static ConfigEntry<float> SpeedMeleeComboStamina;
        public static ConfigEntry<float> SpeedMeleeComboDuration;
        public static ConfigEntry<float> SpeedMeleeComboSpeed;
        // 석궁 숙련자
        public static ConfigEntry<float> SpeedCrossbowExpertSpeed;
        public static ConfigEntry<float> SpeedCrossbowExpertDuration;
        public static ConfigEntry<float> SpeedCrossbowExpertReload;
        // 활 숙련자
        public static ConfigEntry<float> SpeedBowExpertStamina;
        public static ConfigEntry<float> SpeedBowExpertDrawSpeed;
        public static ConfigEntry<float> SpeedBowExpertDuration;
        // 이동 시전 (지팡이)
        public static ConfigEntry<float> SpeedStaffCastMoveSpeed;
        public static ConfigEntry<float> SpeedStaffCastEitrReduction;
        public static ConfigEntry<float> SpeedStaffCastSpeed;

        // === 티어3: 수련자 ===
        public static ConfigEntry<float> SpeedEx1MeleeSkill;
        public static ConfigEntry<float> SpeedEx1CrossbowSkill;
        public static ConfigEntry<float> SpeedEx2StaffSkill;
        public static ConfigEntry<float> SpeedEx2BowSkill;

        // === 티어4: 마스터 ===
        public static ConfigEntry<float> SpeedFoodEfficiency;
        public static ConfigEntry<float> SpeedShipBonus;

        // === 티어5: 점프 숙련자 ===
        public static ConfigEntry<float> JumpSkillLevelBonus;
        public static ConfigEntry<float> JumpStaminaReduction;

        // === 티어6: 스탯 ===
        public static ConfigEntry<float> SpeedDexterityAttackSpeedBonus;
        public static ConfigEntry<float> SpeedDexterityMoveSpeedBonus;
        public static ConfigEntry<float> SpeedEnduranceStaminaBonus;
        public static ConfigEntry<float> SpeedIntellectEitrBonus;

        // === 티어7: 숙련자 ===
        public static ConfigEntry<float> AllMasterRunSkill;
        public static ConfigEntry<float> AllMasterJumpSkill;

        // === 티어8: 최종 가속 ===
        public static ConfigEntry<float> SpeedMeleeAttackSpeed;
        public static ConfigEntry<float> SpeedMeleeComboTripleBonus;
        public static ConfigEntry<float> SpeedCrossbowDrawSpeed;
        public static ConfigEntry<float> SpeedCrossbowReloadMoveSpeed;
        public static ConfigEntry<float> SpeedBowDrawSpeed;
        public static ConfigEntry<float> SpeedBowDrawMoveSpeed;
        public static ConfigEntry<float> SpeedStaffCastSpeedFinal;
        public static ConfigEntry<float> SpeedStaffTripleEitrRecovery;

        // === 필요 포인트 접근 프로퍼티 (단일 스킬) ===
        public static int SpeedRootRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_root_required_points", SpeedRootRequiredPoints?.Value ?? 2);
        public static int SpeedStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step1_required_points", SpeedStep1RequiredPoints?.Value ?? 2);
        public static int SpeedStep5RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step5_required_points", SpeedStep5RequiredPoints?.Value ?? 3);
        public static int SpeedStep7RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step7_required_points", SpeedStep7RequiredPoints?.Value ?? 3);

        // === 필요 포인트 접근 프로퍼티 (독립 스킬) ===
        public static int SpeedStep2_MeleeFlowRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step2_meleeflow_rp", SpeedStep2_MeleeFlowRequiredPoints?.Value ?? 2);
        public static int SpeedStep2_CrossbowExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step2_crossbowexpert_rp", SpeedStep2_CrossbowExpertRequiredPoints?.Value ?? 2);
        public static int SpeedStep2_BowExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step2_bowexpert_rp", SpeedStep2_BowExpertRequiredPoints?.Value ?? 2);
        public static int SpeedStep2_MobileCastRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step2_mobilecast_rp", SpeedStep2_MobileCastRequiredPoints?.Value ?? 2);
        public static int SpeedStep3_Practitioner1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step3_practitioner1_rp", SpeedStep3_Practitioner1RequiredPoints?.Value ?? 2);
        public static int SpeedStep3_Practitioner2RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step3_practitioner2_rp", SpeedStep3_Practitioner2RequiredPoints?.Value ?? 2);
        public static int SpeedStep4_EnergizerRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step4_energizer_rp", SpeedStep4_EnergizerRequiredPoints?.Value ?? 2);
        public static int SpeedStep4_CaptainRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step4_captain_rp", SpeedStep4_CaptainRequiredPoints?.Value ?? 2);
        public static int SpeedStep6_DexterityRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step6_dexterity_rp", SpeedStep6_DexterityRequiredPoints?.Value ?? 2);
        public static int SpeedStep6_EnduranceRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step6_endurance_rp", SpeedStep6_EnduranceRequiredPoints?.Value ?? 2);
        public static int SpeedStep6_IntellectRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step6_intellect_rp", SpeedStep6_IntellectRequiredPoints?.Value ?? 2);
        public static int SpeedStep8_MeleeAccelRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step8_meleeaccel_rp", SpeedStep8_MeleeAccelRequiredPoints?.Value ?? 2);
        public static int SpeedStep8_CrossbowAccelRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step8_crossbowaccel_rp", SpeedStep8_CrossbowAccelRequiredPoints?.Value ?? 2);
        public static int SpeedStep8_BowAccelRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step8_bowaccel_rp", SpeedStep8_BowAccelRequiredPoints?.Value ?? 2);
        public static int SpeedStep8_CastAccelRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step8_castaccel_rp", SpeedStep8_CastAccelRequiredPoints?.Value ?? 2);

        // === 속도 전문가 접근 프로퍼티들 ===
        public static float SpeedRootMoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Expert_MoveSpeed", SpeedRootMoveSpeed.Value);
        public static float SpeedBaseDodgeMoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step1_DodgeMoveSpeed", SpeedBaseDodgeMoveSpeed.Value);
        public static float SpeedBaseDodgeDurationValue => SkillTreeConfig.GetEffectiveValue("Speed_Step1_DodgeDuration", SpeedBaseDodgeDuration.Value);
        public static float SpeedMeleeComboAttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_MeleeComboAttackSpeed", SpeedMeleeComboAttackSpeed.Value);
        public static float SpeedMeleeComboStaminaValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_MeleeComboStamina", SpeedMeleeComboStamina.Value);
        public static float SpeedMeleeComboDurationValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_MeleeComboDuration", SpeedMeleeComboDuration.Value);
        public static float SpeedCrossbowExpertSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_CrossbowExpertSpeed", SpeedCrossbowExpertSpeed.Value);
        public static float SpeedCrossbowExpertDurationValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_CrossbowExpertDuration", SpeedCrossbowExpertDuration.Value);
        public static float SpeedCrossbowExpertReloadValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_CrossbowExpertReload", SpeedCrossbowExpertReload.Value);
        public static float SpeedBowExpertStaminaValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_BowExpertStamina", SpeedBowExpertStamina.Value);
        public static float SpeedBowExpertDrawSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_BowExpertDrawSpeed", SpeedBowExpertDrawSpeed.Value);
        public static float SpeedStaffCastMoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_StaffCastMoveSpeed", SpeedStaffCastMoveSpeed.Value);
        public static float SpeedStaffCastEitrReductionValue => SkillTreeConfig.GetEffectiveValue("Speed_Step2_StaffCastEitrReduction", SpeedStaffCastEitrReduction.Value);
        public static float SpeedEx1MeleeSkillValue => SkillTreeConfig.GetEffectiveValue("Speed_Step3_Ex1MeleeSkill", SpeedEx1MeleeSkill.Value);
        public static float SpeedEx1CrossbowSkillValue => SkillTreeConfig.GetEffectiveValue("Speed_Step3_Ex1CrossbowSkill", SpeedEx1CrossbowSkill.Value);
        public static float SpeedEx2StaffSkillValue => SkillTreeConfig.GetEffectiveValue("Speed_Step3_Ex2StaffSkill", SpeedEx2StaffSkill.Value);
        public static float SpeedEx2BowSkillValue => SkillTreeConfig.GetEffectiveValue("Speed_Step3_Ex2BowSkill", SpeedEx2BowSkill.Value);
        public static float SpeedFoodEfficiencyValue => SkillTreeConfig.GetEffectiveValue("Speed_Step4_FoodEfficiency", SpeedFoodEfficiency.Value);
        public static float SpeedShipBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Step4_ShipBonus", SpeedShipBonus.Value);
        public static float JumpSkillLevelBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Step5_JumpSkillLevel", JumpSkillLevelBonus.Value);
        public static float JumpStaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Speed_Step5_JumpStamina", JumpStaminaReduction.Value);
        public static float SpeedDexterityAttackSpeedBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Step6_DexAttackSpeed", SpeedDexterityAttackSpeedBonus.Value);
        public static float SpeedDexterityMoveSpeedBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Step6_DexMoveSpeed", SpeedDexterityMoveSpeedBonus.Value);
        public static float SpeedEnduranceStaminaBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Step6_EnduranceStamina", SpeedEnduranceStaminaBonus.Value);
        public static float SpeedIntellectEitrBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Step6_IntellectEitr", SpeedIntellectEitrBonus.Value);
        public static float AllMasterRunSkillValue => SkillTreeConfig.GetEffectiveValue("Speed_Step7_AllMasterRun", AllMasterRunSkill.Value);
        public static float AllMasterJumpSkillValue => SkillTreeConfig.GetEffectiveValue("Speed_Step7_AllMasterJump", AllMasterJumpSkill.Value);
        public static float SpeedMeleeAttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_MeleeAttackSpeed", SpeedMeleeAttackSpeed.Value);
        public static float SpeedMeleeComboTripleBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_MeleeTripleBonus", SpeedMeleeComboTripleBonus.Value);
        public static float SpeedCrossbowDrawSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_CrossbowDrawSpeed", SpeedCrossbowDrawSpeed.Value);
        public static float SpeedCrossbowReloadMoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_CrossbowReloadMove", SpeedCrossbowReloadMoveSpeed.Value);
        public static float SpeedBowDrawSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_BowDrawSpeed", SpeedBowDrawSpeed.Value);
        public static float SpeedBowDrawMoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_BowDrawMove", SpeedBowDrawMoveSpeed.Value);
        public static float SpeedStaffCastSpeedFinalValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_StaffCastSpeed", SpeedStaffCastSpeedFinal.Value);
        public static float SpeedStaffTripleEitrRecoveryValue => SkillTreeConfig.GetEffectiveValue("Speed_Step8_StaffTripleEitr", SpeedStaffTripleEitrRecovery.Value);

        public static float SpeedBaseAttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier1_BaseAttackSpeed", SpeedBaseAttackSpeed.Value);
        public static float SpeedBaseDodgeSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier1_BaseDodgeSpeed", SpeedBaseDodgeSpeed.Value);
        public static float SpeedMeleeComboSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_MeleeComboSpeed", SpeedMeleeComboSpeed.Value);
        public static float SpeedBowExpertDurationValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_BowExpertDuration", SpeedBowExpertDuration.Value);
        public static float SpeedStaffCastSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_StaffCastSpeed", SpeedStaffCastSpeed.Value);

        // 호환 프로퍼티 (Plugin.Systems.cs에서 참조)
        public static float SpeedMeleeComboBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_MeleeComboBonus", SpeedMeleeComboStamina.Value);
        public static float SpeedCrossbowReloadSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_CrossbowReloadSpeed", SpeedCrossbowDrawSpeed.Value);
        public static float SpeedBowHitBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_BowHitBonus", SpeedBowExpertStamina.Value);
        public static float SpeedBowHitDurationValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_BowHitDuration", SpeedBowExpertDuration.Value);

        public static void Initialize(ConfigFile config)
        {
            // === 티어0: 속도 전문가 ===
            SpeedRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_SpeedExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier0_SpeedExpert_RequiredPoints"), 79);

            SpeedRootMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_SpeedExpert_MoveSpeedBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier0_SpeedExpert_MoveSpeedBonus"), 80);

            // === 티어1: 민첩함의 기초 ===
            SpeedStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_AgilityBase_RequiredPoints"), 69);

            SpeedBaseDodgeMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_DodgeMoveSpeedBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier1_AgilityBase_DodgeMoveSpeedBonus"), 70);

            SpeedBaseDodgeDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_BuffDuration", 2f,
                SkillTreeConfig.GetConfigDescription("Tier1_AgilityBase_BuffDuration"), 70);

            SpeedBaseAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_AttackSpeedBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier1_AgilityBase_AttackSpeedBonus"), 70);

            SpeedBaseDodgeSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_DodgeSpeedBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier1_AgilityBase_DodgeSpeedBonus"), 70);

            // === 티어2-1: 연속의 흐름 (근접) ===
            SpeedStep2_MeleeFlowRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeFlow_RequiredPoints"), 59);

            SpeedMeleeComboAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_AttackSpeedBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeFlow_AttackSpeedBonus"), 60);

            SpeedMeleeComboStamina = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_StaminaReduction", 12f,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeFlow_StaminaReduction"), 60);

            SpeedMeleeComboDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_Duration", 3f,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeFlow_Duration"), 60);

            SpeedMeleeComboSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_ComboSpeedBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeFlow_ComboSpeedBonus"), 60);

            // === 티어2-2: 석궁 숙련자 ===
            SpeedStep2_CrossbowExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_CrossbowExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_CrossbowExpert_RequiredPoints"), 57);

            SpeedCrossbowExpertSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_CrossbowExpert_MoveSpeedBonus", 12f,
                SkillTreeConfig.GetConfigDescription("Tier2_CrossbowExpert_MoveSpeedBonus"), 58);

            SpeedCrossbowExpertDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_CrossbowExpert_BuffDuration", 4f,
                SkillTreeConfig.GetConfigDescription("Tier2_CrossbowExpert_BuffDuration"), 58);

            SpeedCrossbowExpertReload = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_CrossbowExpert_ReloadSpeedBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier2_CrossbowExpert_ReloadSpeedBonus"), 58);

            // === 티어2-3: 활 숙련자 ===
            SpeedStep2_BowExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_BowExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_BowExpert_RequiredPoints"), 55);

            SpeedBowExpertStamina = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_BowExpert_StaminaReduction", 10f,
                SkillTreeConfig.GetConfigDescription("Tier2_BowExpert_StaminaReduction"), 56);

            SpeedBowExpertDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_BowExpert_NextDrawSpeedBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier2_BowExpert_NextDrawSpeedBonus"), 56);

            SpeedBowExpertDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_BowExpert_BuffDuration", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2_BowExpert_BuffDuration"), 56);

            // === 티어2-4: 이동 시전 (지팡이) ===
            SpeedStep2_MobileCastRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MobileCast_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_MobileCast_RequiredPoints"), 53);

            SpeedStaffCastMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MobileCast_MoveSpeedBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier2_MobileCast_MoveSpeedBonus"), 54);

            SpeedStaffCastEitrReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MobileCast_EitrReduction", 10f,
                SkillTreeConfig.GetConfigDescription("Tier2_MobileCast_EitrReduction"), 54);

            SpeedStaffCastSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MobileCast_CastMoveSpeed", 4f,
                SkillTreeConfig.GetConfigDescription("Tier2_MobileCast_CastMoveSpeed"), 54);

            // === 티어3-1: 수련자1 ===
            SpeedStep3_Practitioner1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner1_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier3_Practitioner1_RequiredPoints"), 47);

            SpeedEx1MeleeSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner1_MeleeSkillBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier3_Practitioner1_MeleeSkillBonus"), 48);

            SpeedEx1CrossbowSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner1_CrossbowSkillBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier3_Practitioner1_CrossbowSkillBonus"), 48);

            // === 티어3-2: 수련자2 ===
            SpeedStep3_Practitioner2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner2_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier3_Practitioner2_RequiredPoints"), 45);

            SpeedEx2StaffSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner2_StaffSkillBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier3_Practitioner2_StaffSkillBonus"), 46);

            SpeedEx2BowSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner2_BowSkillBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier3_Practitioner2_BowSkillBonus"), 46);

            // === 티어4-1: 에너자이저 ===
            SpeedStep4_EnergizerRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_Energizer_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4_Energizer_RequiredPoints"), 37);

            SpeedFoodEfficiency = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_Energizer_FoodConsumptionReduction", 15f,
                SkillTreeConfig.GetConfigDescription("Tier4_Energizer_FoodConsumptionReduction"), 38);

            // === 티어4-2: 선장 ===
            SpeedStep4_CaptainRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_Captain_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4_Captain_RequiredPoints"), 35);

            SpeedShipBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_Captain_ShipSpeedBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier4_Captain_ShipSpeedBonus"), 36);

            // === 티어5: 점프 숙련자 ===
            SpeedStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_JumpMaster_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_JumpMaster_RequiredPoints"), 29);

            JumpSkillLevelBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_JumpMaster_JumpSkillBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier5_JumpMaster_JumpSkillBonus"), 30);

            JumpStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_JumpMaster_JumpStaminaReduction", 10f,
                SkillTreeConfig.GetConfigDescription("Tier5_JumpMaster_JumpStaminaReduction"), 30);

            // === 티어6-1: 민첩 ===
            SpeedStep6_DexterityRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Dexterity_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier6_Dexterity_RequiredPoints"), 21);

            SpeedDexterityAttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Dexterity_MeleeAttackSpeedBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_Dexterity_MeleeAttackSpeedBonus"), 22);

            SpeedDexterityMoveSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Dexterity_MoveSpeedBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_Dexterity_MoveSpeedBonus"), 22);

            // === 티어6-2: 지구력 ===
            SpeedStep6_EnduranceRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Endurance_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier6_Endurance_RequiredPoints"), 19);

            SpeedEnduranceStaminaBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Endurance_StaminaMaxBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Tier6_Endurance_StaminaMaxBonus"), 20);

            // === 티어6-3: 지능 ===
            SpeedStep6_IntellectRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Intellect_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier6_Intellect_RequiredPoints"), 17);

            SpeedIntellectEitrBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Intellect_EitrMaxBonus", 35f,
                SkillTreeConfig.GetConfigDescription("Tier6_Intellect_EitrMaxBonus"), 18);

            // === 티어7: 숙련자 ===
            SpeedStep7RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_Master_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier7_Master_RequiredPoints"), 11);

            AllMasterRunSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_Master_RunSkillBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier7_Master_RunSkillBonus"), 12);

            AllMasterJumpSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_Master_JumpSkillBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier7_Master_JumpSkillBonus"), 12);

            // === 티어8-1: 근접 가속 ===
            SpeedStep8_MeleeAccelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_MeleeAccel_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier8_MeleeAccel_RequiredPoints"), 7);

            SpeedMeleeAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_MeleeAccel_AttackSpeedBonus", 7f,
                SkillTreeConfig.GetConfigDescription("Tier8_MeleeAccel_AttackSpeedBonus"), 8);

            SpeedMeleeComboTripleBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_MeleeAccel_TripleComboBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Tier8_MeleeAccel_TripleComboBonus"), 8);

            // === 티어8-2: 석궁 가속 ===
            SpeedStep8_CrossbowAccelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CrossbowAccel_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier8_CrossbowAccel_RequiredPoints"), 5);

            SpeedCrossbowDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CrossbowAccel_ReloadSpeed", 30f,
                SkillTreeConfig.GetConfigDescription("Tier8_CrossbowAccel_ReloadSpeed"), 6);

            SpeedCrossbowReloadMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CrossbowAccel_ReloadMoveSpeed", 25f,
                SkillTreeConfig.GetConfigDescription("Tier8_CrossbowAccel_ReloadMoveSpeed"), 6);

            // === 티어8-3: 활 가속 ===
            SpeedStep8_BowAccelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_BowAccel_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier8_BowAccel_RequiredPoints"), 3);

            SpeedBowDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_BowAccel_DrawSpeed", 15f,
                SkillTreeConfig.GetConfigDescription("Tier8_BowAccel_DrawSpeed"), 4);

            SpeedBowDrawMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_BowAccel_DrawMoveSpeed", 15f,
                SkillTreeConfig.GetConfigDescription("Tier8_BowAccel_DrawMoveSpeed"), 4);

            // === 티어8-4: 시전 가속 ===
            SpeedStep8_CastAccelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CastAccel_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier8_CastAccel_RequiredPoints"), 1);

            SpeedStaffCastSpeedFinal = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CastAccel_MagicAttackSpeed", 6f,
                SkillTreeConfig.GetConfigDescription("Tier8_CastAccel_MagicAttackSpeed"), 2);

            SpeedStaffTripleEitrRecovery = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CastAccel_TripleEitrRecovery", 12f,
                SkillTreeConfig.GetConfigDescription("Tier8_CastAccel_TripleEitrRecovery"), 2);

            Plugin.Log.LogDebug("[Speed_Config] Speed Expert tree config initialized");
        }
    }
}

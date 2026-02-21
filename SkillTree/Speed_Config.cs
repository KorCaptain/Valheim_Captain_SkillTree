using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 속도 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Speed_Config
    {
        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> SpeedRootRequiredPoints;
        public static ConfigEntry<int> SpeedStep1RequiredPoints;
        public static ConfigEntry<int> SpeedStep2RequiredPoints;
        public static ConfigEntry<int> SpeedStep3RequiredPoints;
        public static ConfigEntry<int> SpeedStep4RequiredPoints;
        public static ConfigEntry<int> SpeedStep5RequiredPoints;
        public static ConfigEntry<int> SpeedStep6RequiredPoints;
        public static ConfigEntry<int> SpeedStep7RequiredPoints;
        public static ConfigEntry<int> SpeedStep8RequiredPoints;

        // === 티어0: 속도 전문가 ===
        public static ConfigEntry<float> SpeedRootMoveSpeed;

        // === 티어1: 민첩함의 기초 ===
        public static ConfigEntry<float> SpeedBaseDodgeMoveSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeDuration;

        // === 티어2: 무기별 특화 ===
        // 연속의 흐름 (근접)
        public static ConfigEntry<float> SpeedMeleeComboAttackSpeed;
        public static ConfigEntry<float> SpeedMeleeComboStamina;
        public static ConfigEntry<float> SpeedMeleeComboDuration;
        // 석궁 숙련자
        public static ConfigEntry<float> SpeedCrossbowExpertSpeed;
        public static ConfigEntry<float> SpeedCrossbowExpertDuration;
        public static ConfigEntry<float> SpeedCrossbowExpertReload;
        // 활 숙련자
        public static ConfigEntry<float> SpeedBowExpertStamina;
        public static ConfigEntry<float> SpeedBowExpertDrawSpeed;
        // 이동 시전 (지팡이)
        public static ConfigEntry<float> SpeedStaffCastMoveSpeed;
        public static ConfigEntry<float> SpeedStaffCastEitrReduction;

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

        // === 티어0~2 추가 설정 ===
        public static ConfigEntry<float> SpeedBaseMoveSpeed;
        public static ConfigEntry<float> SpeedBaseAttackSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeSpeed;
        public static ConfigEntry<float> SpeedMeleeComboSpeed;
        public static ConfigEntry<float> SpeedBowExpertDuration;
        public static ConfigEntry<float> SpeedStaffCastSpeed;
        public static ConfigEntry<float> SpeedCooldownReduction;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int SpeedRootRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_root_required_points", SpeedRootRequiredPoints?.Value ?? 2);
        public static int SpeedStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step1_required_points", SpeedStep1RequiredPoints?.Value ?? 2);
        public static int SpeedStep2RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step2_required_points", SpeedStep2RequiredPoints?.Value ?? 2);
        public static int SpeedStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step3_required_points", SpeedStep3RequiredPoints?.Value ?? 2);
        public static int SpeedStep4RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step4_required_points", SpeedStep4RequiredPoints?.Value ?? 2);
        public static int SpeedStep5RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step5_required_points", SpeedStep5RequiredPoints?.Value ?? 3);
        public static int SpeedStep6RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step6_required_points", SpeedStep6RequiredPoints?.Value ?? 2);
        public static int SpeedStep7RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step7_required_points", SpeedStep7RequiredPoints?.Value ?? 3);
        public static int SpeedStep8RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("speed_step8_required_points", SpeedStep8RequiredPoints?.Value ?? 2);

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

        // 티어0~2 추가 프로퍼티
        public static float SpeedBaseMoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier0_BaseMoveSpeed", SpeedBaseMoveSpeed.Value);
        public static float SpeedBaseAttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier1_BaseAttackSpeed", SpeedBaseAttackSpeed.Value);
        public static float SpeedBaseDodgeSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier1_BaseDodgeSpeed", SpeedBaseDodgeSpeed.Value);
        public static float SpeedMeleeComboSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_MeleeComboSpeed", SpeedMeleeComboSpeed.Value);
        public static float SpeedBowExpertDurationValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_BowExpertDuration", SpeedBowExpertDuration.Value);
        public static float SpeedStaffCastSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_StaffCastSpeed", SpeedStaffCastSpeed.Value);
        public static float SpeedCooldownReductionValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier0_CooldownReduction", SpeedCooldownReduction.Value);

        // 티어2 호환 프로퍼티
        public static float SpeedMeleeComboBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_MeleeComboBonus", SpeedMeleeComboStamina.Value);
        public static float SpeedCrossbowReloadSpeedValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_CrossbowReloadSpeed", SpeedCrossbowDrawSpeed.Value);
        public static float SpeedBowHitBonusValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_BowHitBonus", SpeedBowExpertStamina.Value);
        public static float SpeedBowHitDurationValue => SkillTreeConfig.GetEffectiveValue("Speed_Tier2_BowHitDuration", SpeedBowExpertDuration.Value);

        public static void Initialize(ConfigFile config)
        {
            // === 필요 포인트 설정 ===
            SpeedRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_SpeedExpert_RequiredPoints", 2,
                "Tier 0: Speed Expert (speed_root) - Required Points");

            SpeedStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_RequiredPoints", 2,
                "Tier 1: Agility Base (speed_base) - Required Points");

            SpeedStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_WeaponSpec_RequiredPoints", 2,
                "Tier 2: Weapon Specialization Skills - Required Points");

            SpeedStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner_RequiredPoints", 2,
                "Tier 3: Practitioner Skills - Required Points");

            SpeedStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_Master_RequiredPoints", 2,
                "Tier 4: Master Skills - Required Points");

            SpeedStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_JumpMaster_RequiredPoints", 3,
                "Tier 5: Jump Master (agility_peak) - Required Points");

            SpeedStep6RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Stats_RequiredPoints", 2,
                "Tier 6: Stats Skills - Required Points");

            SpeedStep7RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_Master_RequiredPoints", 3,
                "Tier 7: Master (all_master) - Required Points");

            SpeedStep8RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_FinalAcceleration_RequiredPoints", 2,
                "Tier 8: Final Acceleration Skills - Required Points");

            // === 티어0: 속도 전문가 ===
            SpeedRootMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_SpeedExpert_MoveSpeedBonus", 5f,
                "Tier 0: Speed Expert - Move Speed Bonus (%)");

            // === 티어1: 민첩함의 기초 ===
            SpeedBaseDodgeMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_DodgeMoveSpeedBonus", 15f,
                "Tier 1: Agility Base - Move Speed Bonus After Dodge (%)");

            SpeedBaseDodgeDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_BuffDuration", 2f,
                "Tier 1: Agility Base - Dodge Buff Duration (sec)");

            // === 티어2: 무기별 특화 ===
            SpeedMeleeComboAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_AttackSpeedBonus", 8f,
                "Tier 2: Melee Flow - Attack Speed Bonus on 2-Hit Combo (%)");

            SpeedMeleeComboStamina = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_StaminaReduction", 12f,
                "Tier 2: Melee Flow - Stamina Reduction on 2-Hit Combo (%)");

            SpeedMeleeComboDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_Duration", 3f,
                "Tier 2: Melee Flow - Buff Duration (sec)");

            SpeedCrossbowExpertSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_CrossbowExpert_MoveSpeedBonus", 12f,
                "Tier 2: Crossbow Expert - Move Speed Bonus on Hit (%)");

            SpeedCrossbowExpertDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_CrossbowExpert_BuffDuration", 4f,
                "Tier 2: Crossbow Expert - Buff Duration (sec)");

            SpeedCrossbowExpertReload = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_CrossbowExpert_ReloadSpeedBonus", 10f,
                "Tier 2: Crossbow Expert - Reload Speed Bonus During Buff (%)");

            SpeedBowExpertStamina = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_BowExpert_StaminaReduction", 10f,
                "Tier 2: Bow Expert - Stamina Reduction on 2-Hit Combo (%)");

            SpeedBowExpertDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_BowExpert_NextDrawSpeedBonus", 15f,
                "Tier 2: Bow Expert - Next Arrow Draw Speed Bonus (%)");

            SpeedStaffCastMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MobileCast_MoveSpeedBonus", 8f,
                "Tier 2: Mobile Cast - Move Speed Bonus While Casting (%)");

            SpeedStaffCastEitrReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MobileCast_EitrReduction", 10f,
                "Tier 2: Mobile Cast - Eitr Cost Reduction (%)");

            // === 티어3: 수련자 ===
            SpeedEx1MeleeSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner1_MeleeSkillBonus", 8f,
                "Tier 3: Practitioner 1 - Melee Weapon Skill Bonus");

            SpeedEx1CrossbowSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner1_CrossbowSkillBonus", 8f,
                "Tier 3: Practitioner 1 - Crossbow Skill Bonus");

            SpeedEx2StaffSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner2_StaffSkillBonus", 8f,
                "Tier 3: Practitioner 2 - Staff Skill Bonus");

            SpeedEx2BowSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_Practitioner2_BowSkillBonus", 8f,
                "Tier 3: Practitioner 2 - Bow Skill Bonus");

            // === 티어4: 마스터 ===
            SpeedFoodEfficiency = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_Energizer_FoodConsumptionReduction", 15f,
                "Tier 4: Energizer - Food Consumption Rate Reduction (%)");

            SpeedShipBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_Captain_ShipSpeedBonus", 15f,
                "Tier 4: Captain - Ship Speed Bonus (%)");

            // === 티어5: 점프 숙련자 ===
            JumpSkillLevelBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_JumpMaster_JumpSkillBonus", 10f,
                "Tier 5: Jump Master - Jump Skill Bonus");

            JumpStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_JumpMaster_JumpStaminaReduction", 10f,
                "Tier 5: Jump Master - Jump Stamina Reduction (%)");

            // === 티어6: 스탯 ===
            SpeedDexterityAttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Dexterity_MeleeAttackSpeedBonus", 5f,
                "Tier 6: Dexterity - Melee Attack Speed Bonus (%)");

            SpeedDexterityMoveSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Dexterity_MoveSpeedBonus", 5f,
                "Tier 6: Dexterity - Move Speed Bonus (%)");

            SpeedEnduranceStaminaBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Endurance_StaminaMaxBonus", 25f,
                "Tier 6: Endurance - Max Stamina Bonus");

            SpeedIntellectEitrBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_Intellect_EitrMaxBonus", 35f,
                "Tier 6: Intellect - Max Eitr Bonus");

            // === 티어7: 숙련자 ===
            AllMasterRunSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_Master_RunSkillBonus", 8f,
                "Tier 7: Master - Run Skill Bonus");

            AllMasterJumpSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_Master_JumpSkillBonus", 8f,
                "Tier 7: Master - Jump Skill Bonus");

            // === 티어8: 최종 가속 ===
            SpeedMeleeAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_MeleeAccel_AttackSpeedBonus", 7f,
                "Tier 8: Melee Acceleration - Melee Attack Speed Bonus (%)");

            SpeedMeleeComboTripleBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_MeleeAccel_TripleComboBonus", 25f,
                "Tier 8: Melee Acceleration - Next Attack Speed Bonus on 3-Hit Combo (%)");

            SpeedCrossbowDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CrossbowAccel_ReloadSpeed", 30f,
                "Tier 8: Crossbow Acceleration - Reload Speed Bonus (%)");

            SpeedCrossbowReloadMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CrossbowAccel_ReloadMoveSpeed", 25f,
                "Tier 8: Crossbow Acceleration - Move Speed During Reload (%)");

            SpeedBowDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_BowAccel_DrawSpeed", 15f,
                "Tier 8: Bow Acceleration - Draw Speed Bonus (%)");

            SpeedBowDrawMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_BowAccel_DrawMoveSpeed", 15f,
                "Tier 8: Bow Acceleration - Move Speed While Drawing (%)");

            SpeedStaffCastSpeedFinal = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CastAccel_MagicAttackSpeed", 6f,
                "Tier 8: Cast Acceleration - Magic Attack Speed Bonus (%)");

            SpeedStaffTripleEitrRecovery = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_CastAccel_TripleEitrRecovery", 12f,
                "Tier 8: Cast Acceleration - Eitr Max Recovery Rate on 3-Hit Combo (%)");

            // === 티어0~2 추가 설정 ===
            SpeedBaseAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_AttackSpeedBonus", 5f,
                "Tier 1: Agility Base - Attack Speed Bonus (%)");

            SpeedBaseDodgeSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_AgilityBase_DodgeSpeedBonus", 10f,
                "Tier 1: Agility Base - Dodge Speed Bonus (%)");

            SpeedBaseMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_SpeedExpert_BaseMoveSpeedBonus", 3f,
                "Tier 0: Speed Expert - Base Move Speed Bonus (%)");

            SpeedMeleeComboSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MeleeFlow_ComboSpeedBonus", 5f,
                "Tier 2: Melee Flow - Melee Combo Speed Bonus (%)");

            SpeedBowExpertDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_BowExpert_BuffDuration", 5f,
                "Tier 2: Bow Expert - Buff Duration (sec)");

            SpeedStaffCastSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_MobileCast_CastMoveSpeed", 4f,
                "Tier 2: Mobile Cast - Move Speed While Staff Casting (%)");

            SpeedCooldownReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_SpeedExpert_CooldownReduction", 1f,
                "Tier 0: Speed Expert - Cooldown Reduction (sec)");

            Plugin.Log.LogDebug("[Speed_Config] Speed Expert tree config initialized");
        }
    }
}

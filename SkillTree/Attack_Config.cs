using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 공격 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Attack_Config
    {
        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> AttackRootRequiredPoints;
        public static ConfigEntry<int> AttackStep1RequiredPoints;
        // Tier 2: 무기별 독립 필요 포인트
        public static ConfigEntry<int> AttackStep2MeleeRequiredPoints;
        public static ConfigEntry<int> AttackStep2BowRequiredPoints;
        public static ConfigEntry<int> AttackStep2CrossbowRequiredPoints;
        public static ConfigEntry<int> AttackStep2StaffRequiredPoints;
        public static ConfigEntry<int> AttackStep3RequiredPoints;
        // Tier 4: 스킬별 독립 필요 포인트
        public static ConfigEntry<int> AttackStep4MeleeRequiredPoints;
        public static ConfigEntry<int> AttackStep4CritRequiredPoints;
        public static ConfigEntry<int> AttackStep4RangedRequiredPoints;
        public static ConfigEntry<int> AttackStep5RequiredPoints;
        // Tier 6: 스킬별 독립 필요 포인트
        public static ConfigEntry<int> AttackStep6CritDmgRequiredPoints;
        public static ConfigEntry<int> AttackStep6FinisherRequiredPoints;
        public static ConfigEntry<int> AttackStep6TwoHandRequiredPoints;
        public static ConfigEntry<int> AttackStep6StaffRequiredPoints;

        // === 공격 전문가 노드 설정 ===
        // 루트 (공격 전문가)
        public static ConfigEntry<float> AttackRootDamageBonus;

        // 2단계 (4가지)
        public static ConfigEntry<float> AttackMeleeBonusChance;
        public static ConfigEntry<float> AttackMeleeBonusDamage;
        public static ConfigEntry<float> AttackBowBonusChance;
        public static ConfigEntry<float> AttackBowBonusDamage;
        public static ConfigEntry<float> AttackCrossbowBonusChance;
        public static ConfigEntry<float> AttackCrossbowBonusDamage;
        public static ConfigEntry<float> AttackStaffBonusChance;
        public static ConfigEntry<float> AttackStaffBonusDamage;

        // 3단계 (기본 공격)
        public static ConfigEntry<float> AttackBasePhysicalDamage;
        public static ConfigEntry<float> AttackBaseElementalDamage;
        public static ConfigEntry<float> AttackTwoHandDrainPhysicalDamage;
        public static ConfigEntry<float> AttackTwoHandDrainElementalDamage;

        // 4단계 (3가지)
        public static ConfigEntry<float> AttackCritChance;
        public static ConfigEntry<float> AttackMeleeEnhancement;
        public static ConfigEntry<float> AttackRangedEnhancement;

        // 5단계 (특수화 스탯)
        public static ConfigEntry<float> AttackSpecialStat;

        public static ConfigEntry<float> AttackSpecialChance;

        // 6단계 (4가지)
        public static ConfigEntry<float> AttackCritDamageBonus;
        public static ConfigEntry<float> AttackTwoHandedBonus;
        public static ConfigEntry<float> AttackStaffElemental;
        public static ConfigEntry<float> AttackFinisherMeleeBonus;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int AttackRootRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_root_required_points", AttackRootRequiredPoints?.Value ?? 2);
        public static int AttackStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step1_required_points", AttackStep1RequiredPoints?.Value ?? 2);
        // Tier 2: 무기별 독립
        public static int AttackStep2MeleeRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step2_melee_required_points", AttackStep2MeleeRequiredPoints?.Value ?? 2);
        public static int AttackStep2BowRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step2_bow_required_points", AttackStep2BowRequiredPoints?.Value ?? 2);
        public static int AttackStep2CrossbowRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step2_crossbow_required_points", AttackStep2CrossbowRequiredPoints?.Value ?? 2);
        public static int AttackStep2StaffRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step2_staff_required_points", AttackStep2StaffRequiredPoints?.Value ?? 2);
        public static int AttackStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step3_required_points", AttackStep3RequiredPoints?.Value ?? 2);
        // Tier 4: 스킬별 독립
        public static int AttackStep4MeleeRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step4_melee_required_points", AttackStep4MeleeRequiredPoints?.Value ?? 2);
        public static int AttackStep4CritRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step4_crit_required_points", AttackStep4CritRequiredPoints?.Value ?? 2);
        public static int AttackStep4RangedRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step4_ranged_required_points", AttackStep4RangedRequiredPoints?.Value ?? 3);
        public static int AttackStep5RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step5_required_points", AttackStep5RequiredPoints?.Value ?? 2);
        // Tier 6: 스킬별 독립
        public static int AttackStep6CritDmgRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_critdmg_required_points", AttackStep6CritDmgRequiredPoints?.Value ?? 3);
        public static int AttackStep6FinisherRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_finisher_required_points", AttackStep6FinisherRequiredPoints?.Value ?? 3);
        public static int AttackStep6TwoHandRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_twohand_required_points", AttackStep6TwoHandRequiredPoints?.Value ?? 3);
        public static int AttackStep6StaffRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_staff_required_points", AttackStep6StaffRequiredPoints?.Value ?? 3);

        // === 공격 전문가 접근 프로퍼티들 ===
        public static float AttackRootDamageBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Expert_Damage", AttackRootDamageBonus.Value);
        public static float AttackMeleeBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_MeleeBonusChance", AttackMeleeBonusChance.Value);
        public static float AttackMeleeBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_MeleeBonusDamage", AttackMeleeBonusDamage.Value);
        public static float AttackBowBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_BowBonusChance", AttackBowBonusChance.Value);
        public static float AttackBowBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_BowBonusDamage", AttackBowBonusDamage.Value);
        public static float AttackCrossbowBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_CrossbowBonusChance", AttackCrossbowBonusChance.Value);
        public static float AttackCrossbowBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_CrossbowBonusDamage", AttackCrossbowBonusDamage.Value);
        public static float AttackStaffBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_StaffBonusChance", AttackStaffBonusChance.Value);
        public static float AttackStaffBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_StaffBonusDamage", AttackStaffBonusDamage.Value);
        public static float AttackBasePhysicalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_PhysicalDamage", AttackBasePhysicalDamage.Value);
        public static float AttackBaseElementalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_ElementalDamage", AttackBaseElementalDamage.Value);
        public static float AttackTwoHandDrainPhysicalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_TwoHandPhysical", AttackTwoHandDrainPhysicalDamage.Value);
        public static float AttackTwoHandDrainElementalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_TwoHandElemental", AttackTwoHandDrainElementalDamage.Value);
        public static float AttackCritChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_CritChance", AttackCritChance.Value);
        public static float AttackMeleeEnhancementValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_MeleeEnhancement", AttackMeleeEnhancement.Value);
        public static float AttackRangedEnhancementValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_RangedEnhancement", AttackRangedEnhancement.Value);
        public static float AttackSpecialStatValue => SkillTreeConfig.GetEffectiveValue("Attack_Step5_SpecialStat", AttackSpecialStat.Value);
        public static float AttackSpecialChanceValue => SkillTreeConfig.GetEffectiveValue("Tier5_Charge_TriggerChance", AttackSpecialChance?.Value ?? 33f);
        public static float AttackCritDamageBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_CritDamageBonus", AttackCritDamageBonus.Value);
        public static float AttackTwoHandedBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_TwoHandedBonus", AttackTwoHandedBonus.Value);
        public static float AttackStaffElementalValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_StaffElemental", AttackStaffElemental.Value);
        public static float AttackFinisherMeleeBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_FinisherMeleeBonus", AttackFinisherMeleeBonus.Value);

        // 레거시 호환 프로퍼티
        public static float AttackOneHandedBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_OneHandedBonus", 5f);

        public static void Initialize(ConfigFile config)
        {
            // === Tier 0: Attack Expert ===
            AttackRootDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier0_AttackExpert_AllDamageBonus", 1f,
                SkillTreeConfig.GetConfigDescription("Tier0_AttackExpert_AllDamageBonus"), order: 60);

            AttackRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier0_AttackExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier0_AttackExpert_RequiredPoints"), order: 59);

            // === Tier 1: Base Attack ===
            AttackBasePhysicalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_BaseAttack_PhysicalDamageBonus", 1f,
                SkillTreeConfig.GetConfigDescription("Tier1_BaseAttack_PhysicalDamageBonus"), order: 50);

            AttackBaseElementalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_BaseAttack_ElementalDamageBonus", 1f,
                SkillTreeConfig.GetConfigDescription("Tier1_BaseAttack_ElementalDamageBonus"), order: 50);

            AttackStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_BaseAttack_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_BaseAttack_RequiredPoints"), order: 49);

            // === Tier 2-1: Melee Specialization ===
            AttackMeleeBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_MeleeSpec_BonusTriggerChance", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeSpec_BonusTriggerChance"), order: 48);

            AttackMeleeBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_MeleeSpec_MeleeDamage", 2f,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeSpec_MeleeDamage"), order: 48);

            AttackStep2MeleeRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_MeleeSpec_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_MeleeSpec_RequiredPoints"), order: 47);

            // === Tier 2-2: Bow Specialization ===
            AttackBowBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_BowSpec_BonusTriggerChance", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2_BowSpec_BonusTriggerChance"), order: 46);

            AttackBowBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_BowSpec_BowDamage", 1f,
                SkillTreeConfig.GetConfigDescription("Tier2_BowSpec_BowDamage"), order: 46);

            AttackStep2BowRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_BowSpec_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_BowSpec_RequiredPoints"), order: 45);

            // === Tier 2-3: Crossbow Specialization ===
            AttackCrossbowBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_CrossbowSpec_EnhanceTriggerChance", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2_CrossbowSpec_EnhanceTriggerChance"), order: 44);

            AttackCrossbowBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_CrossbowSpec_CrossbowDamage", 1f,
                SkillTreeConfig.GetConfigDescription("Tier2_CrossbowSpec_CrossbowDamage"), order: 44);

            AttackStep2CrossbowRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_CrossbowSpec_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_CrossbowSpec_RequiredPoints"), order: 43);

            // === Tier 2-4: Staff Specialization ===
            AttackStaffBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_StaffSpec_ElementalTriggerChance", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2_StaffSpec_ElementalTriggerChance"), order: 42);

            AttackStaffBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_StaffSpec_StaffDamage", 2f,
                SkillTreeConfig.GetConfigDescription("Tier2_StaffSpec_StaffDamage"), order: 42);

            AttackStep2StaffRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_StaffSpec_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_StaffSpec_RequiredPoints"), order: 41);

            // === Tier 3: Attack Boost ===
            AttackTwoHandDrainPhysicalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_AttackBoost_PhysicalDamageBonus", 1f,
                SkillTreeConfig.GetConfigDescription("Tier3_AttackBoost_PhysicalDamageBonus"), order: 30);

            AttackTwoHandDrainElementalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_AttackBoost_ElementalDamageBonus", 1f,
                SkillTreeConfig.GetConfigDescription("Tier3_AttackBoost_ElementalDamageBonus"), order: 30);

            AttackStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_AttackBoost_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier3_AttackBoost_RequiredPoints"), order: 29);

            // === Tier 4-1: Melee Enhancement ===
            AttackMeleeEnhancement = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_MeleeEnhance_2HitComboBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier4_MeleeEnhance_2HitComboBonus"), order: 28);

            AttackStep4MeleeRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_MeleeEnhance_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4_MeleeEnhance_RequiredPoints"), order: 27);

            // === Tier 4-2: Precision Attack ===
            AttackCritChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_PrecisionAttack_CritChance", 5f,
                SkillTreeConfig.GetConfigDescription("Tier4_PrecisionAttack_CritChance"), order: 26);

            AttackStep4CritRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_PrecisionAttack_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4_PrecisionAttack_RequiredPoints"), order: 25);

            // === Tier 4-3: Ranged Enhancement ===
            AttackRangedEnhancement = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_RangedEnhance_RangedDamageBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier4_RangedEnhance_RangedDamageBonus"), order: 24);

            AttackStep4RangedRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_RangedEnhance_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier4_RangedEnhance_RequiredPoints"), order: 23);

            // === Tier 5: Specialized Stats ===
            AttackSpecialStat = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_SpecialStat_SpecBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier5_SpecialStat_SpecBonus"), order: 10);

            AttackSpecialChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_Charge_TriggerChance", 33f,
                SkillTreeConfig.GetConfigDescription("Tier5_Charge_TriggerChance"), order: 10);

            AttackStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_SpecialStat_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier5_SpecialStat_RequiredPoints"), order: 9);

            // === Tier 6-1: Weak Point Attack ===
            AttackCritDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_WeakPointAttack_CritDamageBonus", 12f,
                SkillTreeConfig.GetConfigDescription("Tier6_WeakPointAttack_CritDamageBonus"), order: 8);

            AttackStep6CritDmgRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_WeakPointAttack_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_WeakPointAttack_RequiredPoints"), order: 7);

            // === Tier 6-2: Combo Finisher ===
            AttackFinisherMeleeBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ComboFinisher_3HitComboBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_ComboFinisher_3HitComboBonus"), order: 6);

            AttackStep6FinisherRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ComboFinisher_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_ComboFinisher_RequiredPoints"), order: 5);

            // === Tier 6-3: Two-Hand Crush ===
            AttackTwoHandedBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_TwoHandCrush_TwoHandDamageBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier6_TwoHandCrush_TwoHandDamageBonus"), order: 4);

            AttackStep6TwoHandRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_TwoHandCrush_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_TwoHandCrush_RequiredPoints"), order: 3);

            // === Tier 6-4: Elemental Attack ===
            AttackStaffElemental = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ElementalAttack_ElementalBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_ElementalAttack_ElementalBonus"), order: 2);

            AttackStep6StaffRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ElementalAttack_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_ElementalAttack_RequiredPoints"), order: 1);

            Plugin.Log.LogDebug("[Attack_Config] Attack Expert tree config initialized");
        }
    }
}

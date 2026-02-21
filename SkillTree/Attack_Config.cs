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
        public static ConfigEntry<int> AttackStep2RequiredPoints;
        public static ConfigEntry<int> AttackStep3RequiredPoints;
        public static ConfigEntry<int> AttackStep4RequiredPoints;
        public static ConfigEntry<int> AttackStep4RangedRequiredPoints;
        public static ConfigEntry<int> AttackStep5RequiredPoints;
        public static ConfigEntry<int> AttackStep6RequiredPoints;

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
        public static ConfigEntry<float> AttackStatBonus;
        public static ConfigEntry<float> AttackTwoHandDrainPhysicalDamage;
        public static ConfigEntry<float> AttackTwoHandDrainElementalDamage;

        // 4단계 (3가지)
        public static ConfigEntry<float> AttackCritChance;
        public static ConfigEntry<float> AttackMeleeEnhancement;
        public static ConfigEntry<float> AttackRangedEnhancement;

        // 5단계 (특수화 스탯)
        public static ConfigEntry<float> AttackSpecialStat;

        // 6단계 (4가지)
        public static ConfigEntry<float> AttackCritDamageBonus;
        public static ConfigEntry<float> AttackTwoHandedBonus;
        public static ConfigEntry<float> AttackStaffElemental;
        public static ConfigEntry<float> AttackFinisherMeleeBonus;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int AttackRootRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_root_required_points", AttackRootRequiredPoints?.Value ?? 2);
        public static int AttackStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step1_required_points", AttackStep1RequiredPoints?.Value ?? 2);
        public static int AttackStep2RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step2_required_points", AttackStep2RequiredPoints?.Value ?? 2);
        public static int AttackStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step3_required_points", AttackStep3RequiredPoints?.Value ?? 2);
        public static int AttackStep4RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step4_required_points", AttackStep4RequiredPoints?.Value ?? 2);
        public static int AttackStep4RangedRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step4_ranged_required_points", AttackStep4RangedRequiredPoints?.Value ?? 3);
        public static int AttackStep5RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step5_required_points", AttackStep5RequiredPoints?.Value ?? 2);
        public static int AttackStep6RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_required_points", AttackStep6RequiredPoints?.Value ?? 3);

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
        public static float AttackStatBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_StatBonus", AttackStatBonus.Value);
        public static float AttackTwoHandDrainPhysicalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_TwoHandPhysical", AttackTwoHandDrainPhysicalDamage.Value);
        public static float AttackTwoHandDrainElementalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_TwoHandElemental", AttackTwoHandDrainElementalDamage.Value);
        public static float AttackCritChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_CritChance", AttackCritChance.Value);
        public static float AttackMeleeEnhancementValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_MeleeEnhancement", AttackMeleeEnhancement.Value);
        public static float AttackRangedEnhancementValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_RangedEnhancement", AttackRangedEnhancement.Value);
        public static float AttackSpecialStatValue => SkillTreeConfig.GetEffectiveValue("Attack_Step5_SpecialStat", AttackSpecialStat.Value);
        public static float AttackCritDamageBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_CritDamageBonus", AttackCritDamageBonus.Value);
        public static float AttackTwoHandedBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_TwoHandedBonus", AttackTwoHandedBonus.Value);
        public static float AttackStaffElementalValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_StaffElemental", AttackStaffElemental.Value);
        public static float AttackFinisherMeleeBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_FinisherMeleeBonus", AttackFinisherMeleeBonus.Value);

        // 레거시 호환 프로퍼티
        public static float AttackOneHandedBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_OneHandedBonus", 5f);

        public static void Initialize(ConfigFile config)
        {
            // === 필요 포인트 설정 ===
            AttackRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier0_AttackExpert_RequiredPoints", 2,
                "Tier 0: Attack Expert (attack_root) - Required Points");

            AttackStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_BaseAttack_RequiredPoints", 2,
                "Tier 1: Base Attack (atk_base) - Required Points");

            AttackStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_WeaponSpec_RequiredPoints", 2,
                "Tier 2: Weapon Specialization Skills - Required Points");

            AttackStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_AttackBoost_RequiredPoints", 2,
                "Tier 3: Attack Boost (atk_twohand_drain) - Required Points");

            AttackStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_DetailEnhance_RequiredPoints", 2,
                "Tier 4: Detail Enhancement (Melee/Precision) - Required Points");

            AttackStep4RangedRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_RangedEnhance_RequiredPoints", 3,
                "Tier 4: Ranged Enhancement (atk_ranged_enhance) - Required Points");

            AttackStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_SpecialStat_RequiredPoints", 2,
                "Tier 5: Specialized Stats (atk_special) - Required Points");

            AttackStep6RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_FinalSpec_RequiredPoints", 3,
                "Tier 6: Final Specialization Skills - Required Points");

            // === Tier 0: Attack Expert ===
            AttackRootDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier0_AttackExpert_AllDamageBonus", 10f,
                "[Server Sync] Tier 0: Attack Expert (attack_root) - All Damage Bonus (%)");

            // === Tier 2: Weapon Specialization ===
            AttackMeleeBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_MeleeSpec_BonusTriggerChance", 20f,
                "Tier 2: Melee Spec (attack_step2_melee) - Bonus Trigger Chance (%)");

            AttackMeleeBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_MeleeSpec_MeleeDamage", 10f,
                "Tier 2: Melee Spec (attack_step2_melee) - Melee Damage (%)");

            AttackBowBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_BowSpec_BonusTriggerChance", 20f,
                "Tier 2: Bow Spec (attack_step2_bow) - Bonus Trigger Chance (%)");

            AttackBowBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_BowSpec_BowDamage", 8f,
                "Tier 2: Bow Spec (attack_step2_bow) - Bow Damage (%)");

            AttackCrossbowBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_CrossbowSpec_EnhanceTriggerChance", 15f,
                "Tier 2: Crossbow Spec (attack_step2_crossbow) - Enhance Trigger Chance (%)");

            AttackCrossbowBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_CrossbowSpec_CrossbowDamage", 9f,
                "Tier 2: Crossbow Spec (attack_step2_crossbow) - Crossbow Damage (%)");

            AttackStaffBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_StaffSpec_ElementalTriggerChance", 20f,
                "Tier 2: Staff Spec (attack_step2_staff) - Elemental Trigger Chance (%)");

            AttackStaffBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_StaffSpec_StaffDamage", 8f,
                "Tier 2: Staff Spec (attack_step2_staff) - Staff Damage (%)");

            // === Tier 3: Base Attack ===
            AttackBasePhysicalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_BaseAttack_PhysicalDamageBonus", 2f,
                "Tier 3: Base Attack (attack_step3_base) - Physical Damage Bonus");

            AttackBaseElementalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_BaseAttack_ElementalDamageBonus", 2f,
                "Tier 3: Base Attack (attack_step3_base) - Elemental Damage Bonus");

            AttackStatBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_AttackBoost_StrIntBonus", 5f,
                "Tier 3: Attack Boost (atk_twohand_drain) - Strength & Intelligence Bonus");

            AttackTwoHandDrainPhysicalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_AttackBoost_PhysicalDamageBonus", 10f,
                "Tier 3: Attack Boost (atk_twohand_drain) - Physical Damage Bonus");

            AttackTwoHandDrainElementalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_AttackBoost_ElementalDamageBonus", 10f,
                "Tier 3: Attack Boost (atk_twohand_drain) - Elemental Damage Bonus");

            // === Tier 4: Combat Enhancement ===
            AttackCritChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_PrecisionAttack_CritChance", 5f,
                "Tier 4: Precision Attack (attack_step4_crit) - Crit Chance (%)");

            AttackMeleeEnhancement = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_MeleeEnhance_2HitComboBonus", 10f,
                "Tier 4: Melee Enhancement (attack_step4_melee_enhance) - 2-Hit Combo Bonus (%)");

            AttackRangedEnhancement = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_RangedEnhance_RangedDamageBonus", 5f,
                "Tier 4: Ranged Enhancement (attack_step4_ranged_enhance) - Ranged Damage Bonus (%)");

            // === Tier 5: Specialized Stats ===
            AttackSpecialStat = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_SpecialStat_SpecBonus", 5f,
                "Tier 5: Specialized Stats (attack_step5_special) - Specialization Bonus");

            // === Tier 6: Final Enhancement ===
            AttackCritDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_WeakPointAttack_CritDamageBonus", 12f,
                "Tier 6: Weak Point Attack (attack_step6_crit_damage) - Crit Damage Bonus (%)");

            AttackTwoHandedBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_TwoHandCrush_TwoHandDamageBonus", 10f,
                "Tier 6: Two-Hand Crush (attack_step6_twohanded) - Two-Handed Weapon Damage Bonus (%)");

            AttackStaffElemental = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ElementalAttack_ElementalBonus", 10f,
                "Tier 6: Elemental Attack (attack_step6_elemental) - Elemental Bonus (Bow, Staff) (%)");

            AttackFinisherMeleeBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ComboFinisher_3HitComboBonus", 15f,
                "Tier 6: Combo Finisher (attack_step6_finisher) - 3-Hit Combo Bonus (%)");

            Plugin.Log.LogDebug("[Attack_Config] Attack Expert tree config initialized");
        }
    }
}

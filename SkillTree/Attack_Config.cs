using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 공격 전문가 스킬트리 Config 설정
    /// 선빵 → 추격전 → 난전 → 마무리 4국면 연쇄 증폭 구조
    /// </summary>
    public static class Attack_Config
    {
        // === 필요 포인트 ConfigEntry ===
        public static ConfigEntry<int> AttackRootRequiredPoints;
        public static ConfigEntry<int> AtkOpenerRequiredPoints;
        public static ConfigEntry<int> AtkOpenerMeleeRequiredPoints;
        public static ConfigEntry<int> AtkOpenerBowRequiredPoints;
        public static ConfigEntry<int> AtkOpenerCrossbowRequiredPoints;
        public static ConfigEntry<int> AtkOpenerMagicRequiredPoints;
        public static ConfigEntry<int> AtkPursuitRequiredPoints;
        public static ConfigEntry<int> AtkPursuitSpeedRequiredPoints;
        public static ConfigEntry<int> AtkFrenzyTriggerRequiredPoints;
        public static ConfigEntry<int> AtkFrenzyRequiredPoints;
        // Tier 6 (기존 유지)
        public static ConfigEntry<int> AttackStep6CritDmgRequiredPoints;
        public static ConfigEntry<int> AttackStep6FinisherRequiredPoints;
        public static ConfigEntry<int> AttackStep6TwoHandRequiredPoints;
        public static ConfigEntry<int> AttackStep6StaffRequiredPoints;

        // === Tier 0: 공격 전문가 루트 ===
        public static ConfigEntry<float> AttackRootDamageBonus;

        // === Tier 1: 선빵 ===
        public static ConfigEntry<float> AtkOpenerDamageBonus;
        public static ConfigEntry<float> AtkOpenerStaminaReduction;
        public static ConfigEntry<float> AtkOpenerDuration;
        public static ConfigEntry<float> AtkOpenerCooldown;

        // === Tier 2: 무기별 선빵 특화 ===
        public static ConfigEntry<float> AtkOpenerMeleeFinisherBonus;
        public static ConfigEntry<float> AtkOpenerBowCritChance;
        public static ConfigEntry<float> AtkOpenerCrossbowFirstShotBonus;
        public static ConfigEntry<float> AtkOpenerMagicStaggerProc;

        // === Tier 3: 추격전 ===
        public static ConfigEntry<float> AtkPursuitDamageBonus;
        public static ConfigEntry<float> AtkPursuitChainDamageBonus;
        public static ConfigEntry<float> AtkPursuitChainWindow;

        // === Tier 4: 전환 분기 ===
        public static ConfigEntry<float> AtkPursuitSpeedBonus;
        public static ConfigEntry<float> AtkFrenzyTriggerStaminaReduction;

        // === Tier 5: 난전 ===
        public static ConfigEntry<float> AtkFrenzyStackBonusBase;
        public static ConfigEntry<float> AtkFrenzyStackBonusChain;
        public static ConfigEntry<int>   AtkFrenzyMaxStacks;
        public static ConfigEntry<int>   AtkFrenzyHitsPerStack;
        public static ConfigEntry<float> AtkFrenzyTier6Amplifier;

        // === Tier 6: 마무리 최종 (기존 유지) ===
        public static ConfigEntry<float> AttackCritDamageBonus;
        public static ConfigEntry<float> AttackTwoHandedBonus;
        public static ConfigEntry<float> AttackStaffElemental;
        public static ConfigEntry<float> AttackFinisherMeleeBonus;

        // ============================================================
        // === 필요 포인트 접근 프로퍼티 ===
        // ============================================================
        public static int AttackRootRequiredPointsValue       => (int)SkillTreeConfig.GetEffectiveValue("attack_root_required_points",         AttackRootRequiredPoints?.Value ?? 2);
        public static int AtkOpenerRequiredPointsValue        => (int)SkillTreeConfig.GetEffectiveValue("atk_opener_required_points",           AtkOpenerRequiredPoints?.Value ?? 3);
        public static int AtkOpenerMeleeRequiredPointsValue   => (int)SkillTreeConfig.GetEffectiveValue("atk_opener_melee_required_points",     AtkOpenerMeleeRequiredPoints?.Value ?? 2);
        public static int AtkOpenerBowRequiredPointsValue     => (int)SkillTreeConfig.GetEffectiveValue("atk_opener_bow_required_points",       AtkOpenerBowRequiredPoints?.Value ?? 2);
        public static int AtkOpenerCrossbowRequiredPointsValue=> (int)SkillTreeConfig.GetEffectiveValue("atk_opener_crossbow_required_points",  AtkOpenerCrossbowRequiredPoints?.Value ?? 2);
        public static int AtkOpenerMagicRequiredPointsValue   => (int)SkillTreeConfig.GetEffectiveValue("atk_opener_magic_required_points",     AtkOpenerMagicRequiredPoints?.Value ?? 2);
        public static int AtkPursuitRequiredPointsValue       => (int)SkillTreeConfig.GetEffectiveValue("atk_pursuit_required_points",          AtkPursuitRequiredPoints?.Value ?? 2);
        public static int AtkPursuitSpeedRequiredPointsValue  => (int)SkillTreeConfig.GetEffectiveValue("atk_pursuit_speed_required_points",    AtkPursuitSpeedRequiredPoints?.Value ?? 2);
        public static int AtkFrenzyTriggerRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("atk_frenzy_trigger_required_points",   AtkFrenzyTriggerRequiredPoints?.Value ?? 2);
        public static int AtkFrenzyRequiredPointsValue        => (int)SkillTreeConfig.GetEffectiveValue("atk_frenzy_required_points",           AtkFrenzyRequiredPoints?.Value ?? 3);
        public static int AttackStep6CritDmgRequiredPointsValue   => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_critdmg_required_points",  AttackStep6CritDmgRequiredPoints?.Value ?? 3);
        public static int AttackStep6FinisherRequiredPointsValue  => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_finisher_required_points", AttackStep6FinisherRequiredPoints?.Value ?? 3);
        public static int AttackStep6TwoHandRequiredPointsValue   => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_twohand_required_points",  AttackStep6TwoHandRequiredPoints?.Value ?? 3);
        public static int AttackStep6StaffRequiredPointsValue     => (int)SkillTreeConfig.GetEffectiveValue("attack_step6_staff_required_points",    AttackStep6StaffRequiredPoints?.Value ?? 3);

        // ============================================================
        // === 효과 값 접근 프로퍼티 ===
        // ============================================================
        public static float AttackRootDamageBonusValue        => SkillTreeConfig.GetEffectiveValue("Attack_Expert_Damage",               AttackRootDamageBonus?.Value ?? 3f);

        // Tier 1
        public static float AtkOpenerDamageBonusValue         => SkillTreeConfig.GetEffectiveValue("Tier1_Opener_DamageBonus",           AtkOpenerDamageBonus?.Value ?? 5f);
        public static float AtkOpenerStaminaReductionValue    => SkillTreeConfig.GetEffectiveValue("Tier1_Opener_StaminaReduction",      AtkOpenerStaminaReduction?.Value ?? 15f);
        public static float AtkOpenerDurationValue            => SkillTreeConfig.GetEffectiveValue("Tier1_Opener_Duration",              AtkOpenerDuration?.Value ?? 5f);
        public static float AtkOpenerCooldownValue            => SkillTreeConfig.GetEffectiveValue("Tier1_Opener_Cooldown",              AtkOpenerCooldown?.Value ?? 35f);

        // Tier 2
        public static float AtkOpenerMeleeFinisherBonusValue  => SkillTreeConfig.GetEffectiveValue("Tier2_OpenerMelee_FinisherBonus",    AtkOpenerMeleeFinisherBonus?.Value ?? 10f);
        public static float AtkOpenerBowCritChanceValue       => SkillTreeConfig.GetEffectiveValue("Tier2_OpenerBow_CritChance",         AtkOpenerBowCritChance?.Value ?? 8f);
        public static float AtkOpenerCrossbowFirstShotBonusValue => SkillTreeConfig.GetEffectiveValue("Tier2_OpenerCrossbow_FirstShotBonus", AtkOpenerCrossbowFirstShotBonus?.Value ?? 10f);
        public static float AtkOpenerMagicStaggerProcValue    => SkillTreeConfig.GetEffectiveValue("Tier2_OpenerMagic_StaggerProc",      AtkOpenerMagicStaggerProc?.Value ?? 1f);

        // Tier 3
        public static float AtkPursuitDamageBonusValue        => SkillTreeConfig.GetEffectiveValue("Tier3_Pursuit_DamageBonus",          AtkPursuitDamageBonus?.Value ?? 5f);
        public static float AtkPursuitChainDamageBonusValue   => SkillTreeConfig.GetEffectiveValue("Tier3_Pursuit_ChainDamageBonus",     AtkPursuitChainDamageBonus?.Value ?? 5f);
        public static float AtkPursuitChainWindowValue        => SkillTreeConfig.GetEffectiveValue("Tier3_Pursuit_ChainWindow",          AtkPursuitChainWindow?.Value ?? 5f);

        // Tier 4
        public static float AtkPursuitSpeedBonusValue         => SkillTreeConfig.GetEffectiveValue("Tier4_PursuitSpeed_SpeedBonus",      AtkPursuitSpeedBonus?.Value ?? 12f);
        public static float AtkFrenzyTriggerStaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Tier4_FrenzyTrigger_StaminaReduction", AtkFrenzyTriggerStaminaReduction?.Value ?? 15f);

        // Tier 5
        public static float AtkFrenzyStackBonusBaseValue      => SkillTreeConfig.GetEffectiveValue("Tier5_Frenzy_StackBonusBase",        AtkFrenzyStackBonusBase?.Value ?? 5f);
        public static float AtkFrenzyStackBonusChainValue     => SkillTreeConfig.GetEffectiveValue("Tier5_Frenzy_StackBonusChain",       AtkFrenzyStackBonusChain?.Value ?? 8f);
        public static int   AtkFrenzyMaxStacksValue           => (int)SkillTreeConfig.GetEffectiveValue("Tier5_Frenzy_MaxStacks",        AtkFrenzyMaxStacks?.Value ?? 5);
        public static int   AtkFrenzyHitsPerStackValue        => (int)SkillTreeConfig.GetEffectiveValue("Tier5_Frenzy_HitsPerStack",     AtkFrenzyHitsPerStack?.Value ?? 3);
        public static float AtkFrenzyTier6AmplifierValue      => SkillTreeConfig.GetEffectiveValue("Tier5_Frenzy_Tier6Amplifier",        AtkFrenzyTier6Amplifier?.Value ?? 1.3f);

        // Tier 6
        public static float AttackCritDamageBonusValue        => SkillTreeConfig.GetEffectiveValue("Attack_Step6_CritDamageBonus",       AttackCritDamageBonus?.Value ?? 5f);
        public static float AttackTwoHandedBonusValue         => SkillTreeConfig.GetEffectiveValue("Attack_Step6_TwoHandedBonus",        AttackTwoHandedBonus?.Value ?? 5f);
        public static float AttackStaffElementalValue         => SkillTreeConfig.GetEffectiveValue("Attack_Step6_StaffElemental",        AttackStaffElemental?.Value ?? 5f);
        public static float AttackFinisherMeleeBonusValue     => SkillTreeConfig.GetEffectiveValue("Attack_Step6_FinisherMeleeBonus",    AttackFinisherMeleeBonus?.Value ?? 5f);

        // ============================================================
        // === 레거시 호환 스텁 (기존 파일 참조 컴파일 오류 방지) ===
        // ============================================================
        public static float AttackMeleeBonusChanceValue       => 0f;
        public static float AttackMeleeBonusDamageValue       => 0f;
        public static float AttackBowBonusChanceValue         => 0f;
        public static float AttackBowBonusDamageValue         => 0f;
        public static float AttackCrossbowBonusChanceValue    => 0f;
        public static float AttackCrossbowBonusDamageValue    => 0f;
        public static float AttackStaffBonusChanceValue       => 0f;
        public static float AttackStaffBonusDamageValue       => 0f;
        public static float AttackBasePhysicalDamageValue     => 0f;
        public static float AttackBaseElementalDamageValue    => 0f;
        public static float AttackTwoHandDrainPhysicalDamageValue  => 0f;
        public static float AttackTwoHandDrainElementalDamageValue => 0f;
        public static float AttackCritChanceValue             => 0f;
        public static float AttackMeleeEnhancementValue       => 0f;
        public static float AttackRangedEnhancementValue      => 0f;
        public static float AttackSpecialStatValue            => 0f;
        public static float AttackSpecialChanceValue          => 0f;
        public static float AttackOneHandedBonusValue         => 0f;

        // ============================================================
        // === 초기화 ===
        // ============================================================
        public static void Initialize(ConfigFile config)
        {
            // === Tier 0: 공격 전문가 루트 ===
            AttackRootDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier0_AttackExpert_AllDamageBonus", 3f,
                SkillTreeConfig.GetConfigDescription("Tier0_AttackExpert_AllDamageBonus"), order: 60);

            AttackRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier0_AttackExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier0_AttackExpert_RequiredPoints"), order: 59);

            // === Tier 1: 선빵 ===
            AtkOpenerDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_Opener_DamageBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier1_Opener_DamageBonus"), order: 55);

            AtkOpenerStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_Opener_StaminaReduction", 15f,
                SkillTreeConfig.GetConfigDescription("Tier1_Opener_StaminaReduction"), order: 54);

            AtkOpenerDuration = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_Opener_Duration", 5f,
                SkillTreeConfig.GetConfigDescription("Tier1_Opener_Duration"), order: 53);

            AtkOpenerCooldown = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_Opener_Cooldown", 30f,
                SkillTreeConfig.GetConfigDescription("Tier1_Opener_Cooldown"), order: 52);

            AtkOpenerRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier1_Opener_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier1_Opener_RequiredPoints"), order: 51);

            // === Tier 2: 무기별 선빵 특화 ===
            AtkOpenerMeleeFinisherBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerMelee_FinisherBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerMelee_FinisherBonus"), order: 48);

            AtkOpenerMeleeRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerMelee_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerMelee_RequiredPoints"), order: 47);

            AtkOpenerBowCritChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerBow_CritChance", 8f,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerBow_CritChance"), order: 46);

            AtkOpenerBowRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerBow_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerBow_RequiredPoints"), order: 45);

            AtkOpenerCrossbowFirstShotBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerCrossbow_FirstShotBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerCrossbow_FirstShotBonus"), order: 44);

            AtkOpenerCrossbowRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerCrossbow_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerCrossbow_RequiredPoints"), order: 43);

            AtkOpenerMagicStaggerProc = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerMagic_StaggerProc", 1f,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerMagic_StaggerProc"), order: 42);

            AtkOpenerMagicRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_OpenerMagic_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_OpenerMagic_RequiredPoints"), order: 41);

            // === Tier 3: 추격전 ===
            AtkPursuitDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_Pursuit_DamageBonus", 8f,
                SkillTreeConfig.GetConfigDescription("Tier3_Pursuit_DamageBonus"), order: 38);

            AtkPursuitChainDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_Pursuit_ChainDamageBonus", 12f,
                SkillTreeConfig.GetConfigDescription("Tier3_Pursuit_ChainDamageBonus"), order: 37);

            AtkPursuitChainWindow = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_Pursuit_ChainWindow", 5f,
                SkillTreeConfig.GetConfigDescription("Tier3_Pursuit_ChainWindow"), order: 36);

            AtkPursuitRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_Pursuit_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier3_Pursuit_RequiredPoints"), order: 35);

            // === Tier 4: 전환 분기 ===
            AtkPursuitSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_PursuitSpeed_SpeedBonus", 12f,
                SkillTreeConfig.GetConfigDescription("Tier4_PursuitSpeed_SpeedBonus"), order: 30);

            AtkPursuitSpeedRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_PursuitSpeed_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4_PursuitSpeed_RequiredPoints"), order: 29);

            AtkFrenzyTriggerStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_FrenzyTrigger_StaminaReduction", 20f,
                SkillTreeConfig.GetConfigDescription("Tier4_FrenzyTrigger_StaminaReduction"), order: 28);

            AtkFrenzyTriggerRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_FrenzyTrigger_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier4_FrenzyTrigger_RequiredPoints"), order: 27);

            // === Tier 5: 난전 ===
            AtkFrenzyStackBonusBase = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_Frenzy_StackBonusBase", 5f,
                SkillTreeConfig.GetConfigDescription("Tier5_Frenzy_StackBonusBase"), order: 20);

            AtkFrenzyStackBonusChain = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_Frenzy_StackBonusChain", 8f,
                SkillTreeConfig.GetConfigDescription("Tier5_Frenzy_StackBonusChain"), order: 19);

            AtkFrenzyMaxStacks = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_Frenzy_MaxStacks", 5,
                SkillTreeConfig.GetConfigDescription("Tier5_Frenzy_MaxStacks"), order: 18);

            AtkFrenzyHitsPerStack = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_Frenzy_HitsPerStack", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_Frenzy_HitsPerStack"), order: 17);

            AtkFrenzyTier6Amplifier = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_Frenzy_Tier6Amplifier", 1.3f,
                SkillTreeConfig.GetConfigDescription("Tier5_Frenzy_Tier6Amplifier"), order: 16);

            AtkFrenzyRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_Frenzy_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_Frenzy_RequiredPoints"), order: 15);

            // === Tier 6: 마무리 최종 (기존 유지) ===
            AttackCritDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_WeakPointAttack_CritDamageBonus", 12f,
                SkillTreeConfig.GetConfigDescription("Tier6_WeakPointAttack_CritDamageBonus"), order: 8);

            AttackStep6CritDmgRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_WeakPointAttack_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_WeakPointAttack_RequiredPoints"), order: 7);

            AttackFinisherMeleeBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ComboFinisher_3HitComboBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_ComboFinisher_3HitComboBonus"), order: 6);

            AttackStep6FinisherRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ComboFinisher_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_ComboFinisher_RequiredPoints"), order: 5);

            AttackTwoHandedBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_TwoHandCrush_TwoHandDamageBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier6_TwoHandCrush_TwoHandDamageBonus"), order: 4);

            AttackStep6TwoHandRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_TwoHandCrush_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_TwoHandCrush_RequiredPoints"), order: 3);

            AttackStaffElemental = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ElementalAttack_ElementalBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_ElementalAttack_ElementalBonus"), order: 2);

            AttackStep6StaffRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_ElementalAttack_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier6_ElementalAttack_RequiredPoints"), order: 1);

            Plugin.Log.LogDebug("[Attack_Config] Attack Expert tree config initialized (4-phase chain system)");
        }
    }
}

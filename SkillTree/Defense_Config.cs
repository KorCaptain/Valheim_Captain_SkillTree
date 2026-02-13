using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 방어 스킬 트리 Config 설정
    /// Defense Tree 모든 스킬 통합 관리
    /// </summary>
    public static class Defense_Config
    {
        // =====================================================
        // 필요 포인트 설정
        // =====================================================
        public static ConfigEntry<int> DefenseRootRequiredPoints;
        public static ConfigEntry<int> DefenseStep1RequiredPoints;
        public static ConfigEntry<int> DefenseStep2RequiredPoints;
        public static ConfigEntry<int> DefenseStep3RequiredPoints;
        public static ConfigEntry<int> DefenseStep4RequiredPoints;
        public static ConfigEntry<int> DefenseStep5RequiredPoints;
        public static ConfigEntry<int> DefenseStep6RequiredPoints;

        // =====================================================
        // Tier 0: 방어 전문가 (defense_root)
        // =====================================================

        /// <summary>
        /// defense_root: 방어 전문가 - 체력 보너스
        /// </summary>
        public static ConfigEntry<float> DefenseRootHealthBonus;

        /// <summary>
        /// defense_root: 방어 전문가 - 방어력 보너스
        /// </summary>
        public static ConfigEntry<float> DefenseRootArmorBonus;

        // =====================================================
        // Tier 1: 피부경화 (defense_Step1_survival)
        // =====================================================

        /// <summary>
        /// defense_Step1_survival: 피부경화 - 체력 보너스
        /// </summary>
        public static ConfigEntry<float> SurvivalHealthBonus;

        /// <summary>
        /// defense_Step1_survival: 피부경화 - 방어력 보너스
        /// </summary>
        public static ConfigEntry<float> SurvivalArmorBonus;

        // =====================================================
        // Tier 2: 심신단련 (defense_Step2_dodge)
        // =====================================================

        /// <summary>
        /// defense_Step2_dodge: 심신단련 - 스태미나 보너스
        /// </summary>
        public static ConfigEntry<float> DodgeStaminaBonus;

        /// <summary>
        /// defense_Step2_dodge: 심신단련 - 에이트르 보너스
        /// </summary>
        public static ConfigEntry<float> DodgeEitrBonus;

        // =====================================================
        // Tier 2: 체력단련 (defense_Step2_health)
        // =====================================================

        /// <summary>
        /// defense_Step2_health: 체력단련 - 체력 보너스
        /// </summary>
        public static ConfigEntry<float> HealthBonus;

        /// <summary>
        /// defense_Step2_health: 체력단련 - 방어력 보너스
        /// </summary>
        public static ConfigEntry<float> HealthArmorBonus;

        // =====================================================
        // Tier 3: 단전호흡 (defense_Step3_breath)
        // =====================================================

        /// <summary>
        /// defense_Step3_breath: 단전호흡 - 에이트르 보너스
        /// </summary>
        public static ConfigEntry<float> BreathEitrBonus;

        // =====================================================
        // Tier 3: 회피단련 (defense_Step3_agile)
        // =====================================================

        /// <summary>
        /// defense_Step3_agile: 회피단련 - 회피율 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> AgileDodgeBonus;

        /// <summary>
        /// defense_Step3_agile: 회피단련 - 구르기 무적시간 증가 (%)
        /// </summary>
        public static ConfigEntry<float> AgileInvincibilityBonus;

        // =====================================================
        // Tier 3: 체력증강 (defense_Step3_boost)
        // =====================================================

        /// <summary>
        /// defense_Step3_boost: 체력증강 - 체력 보너스
        /// </summary>
        public static ConfigEntry<float> BoostHealthBonus;

        // =====================================================
        // Tier 3: 방패훈련 (defense_Step3_shield)
        // =====================================================

        /// <summary>
        /// defense_Step3_shield: 방패훈련 - 방패 방어력 보너스
        /// </summary>
        public static ConfigEntry<float> ShieldTrainingBlockPowerBonus;

        // =====================================================
        // Tier 4: 충격파방출 (defense_Step4_mental)
        // =====================================================
        // 액티브 스킬 - Config 없음 (고정 스탯)

        // =====================================================
        // Tier 4: 발구르기 (defense_Step4_instant)
        // =====================================================

        /// <summary>
        /// defense_Step4_instant: 발구르기 - 효과 범위 (미터)
        /// </summary>
        public static ConfigEntry<float> StompRadius;

        /// <summary>
        /// defense_Step4_instant: 발구르기 - 넉백 힘
        /// </summary>
        public static ConfigEntry<float> StompKnockback;

        /// <summary>
        /// defense_Step4_instant: 발구르기 - 쿨타임 (초)
        /// </summary>
        public static ConfigEntry<float> StompCooldown;

        /// <summary>
        /// defense_Step4_instant: 발구르기 - 자동 발동 체력 임계값 (0.0 ~ 1.0)
        /// </summary>
        public static ConfigEntry<float> StompHealthThreshold;

        /// <summary>
        /// defense_Step4_instant: 발구르기 - VFX 지속시간 (초)
        /// </summary>
        public static ConfigEntry<float> StompVFXDuration;

        // =====================================================
        // Tier 4: 바위피부 (defense_Step4_tanker)
        // =====================================================
        // Config 추가 필요 (현재 12% 고정) - 추후 추가 가능

        // =====================================================
        // Tier 5: 지구력 (defense_Step5_focus)
        // =====================================================

        /// <summary>
        /// defense_Step5_focus: 지구력 - 달리기 스태미나 감소 (%)
        /// </summary>
        public static ConfigEntry<float> FocusRunStaminaReduction;

        /// <summary>
        /// defense_Step5_focus: 지구력 - 점프 스태미나 감소 (%)
        /// </summary>
        public static ConfigEntry<float> FocusJumpStaminaReduction;

        // =====================================================
        // Tier 5: 기민함 (defense_Step5_stamina)
        // =====================================================

        /// <summary>
        /// defense_Step5_stamina: 기민함 - 회피율 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> StaminaDodgeBonus;

        /// <summary>
        /// defense_Step5_stamina: 기민함 - 구르기 스태미나 감소 (%)
        /// </summary>
        public static ConfigEntry<float> StaminaRollStaminaReduction;

        // =====================================================
        // Tier 5: 트롤의 재생력 (defense_Step5_heal)
        // =====================================================

        /// <summary>
        /// defense_Step5_heal: 트롤의 재생력 - 체력 재생 보너스 (초당)
        /// </summary>
        public static ConfigEntry<float> TrollRegenBonus;

        /// <summary>
        /// defense_Step5_heal: 트롤의 재생력 - 재생 간격 (초)
        /// </summary>
        public static ConfigEntry<float> TrollRegenInterval;

        // =====================================================
        // Tier 5: 막기달인 (defense_Step5_parry)
        // =====================================================

        /// <summary>
        /// defense_Step5_parry: 막기달인 - 방패 방어력 보너스
        /// </summary>
        public static ConfigEntry<float> ParryMasterBlockPowerBonus;

        /// <summary>
        /// defense_Step5_parry: 막기달인 - 패링 지속시간 보너스 (초)
        /// </summary>
        public static ConfigEntry<float> ParryMasterParryDurationBonus;

        // =====================================================
        // Tier 6: 마인드쉴드 (defense_Step6_mind)
        // =====================================================
        // Config 추가 필요 (현재 +60초 고정) - 추후 추가 가능

        // =====================================================
        // Tier 6: 신경강화 (defense_Step6_attack)
        // =====================================================

        /// <summary>
        /// defense_Step6_attack: 신경강화 - 회피율 보너스 (영구, %)
        /// </summary>
        public static ConfigEntry<float> AttackDodgeBonus;

        // =====================================================
        // Tier 6: 이단점프 (defense_step6_double_jump)
        // =====================================================
        // 액티브 스킬 - Config 없음 (기능형)

        // =====================================================
        // Tier 6: 요툰의 생명력 (defense_Step6_body)
        // =====================================================

        /// <summary>
        /// defense_Step6_body: 요툰의 생명력 - 체력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> BodyHealthBonus;

        /// <summary>
        /// defense_Step6_body: 요툰의 생명력 - 방어력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> BodyArmorBonus;

        // =====================================================
        // Tier 6: 요툰의 방패 (defense_Step6_true)
        // =====================================================

        /// <summary>
        /// defense_Step6_true: 요툰의 방패 - 방패 블럭 스태미나 감소 (%)
        /// </summary>
        public static ConfigEntry<float> JotunnShieldBlockStaminaReduction;

        /// <summary>
        /// defense_Step6_true: 요툰의 방패 - 일반 방패 이동속도 보상 (%)
        /// </summary>
        public static ConfigEntry<float> JotunnShieldNormalSpeedBonus;

        /// <summary>
        /// defense_Step6_true: 요툰의 방패 - Tower/대형 방패 이동속도 보상 (%)
        /// </summary>
        public static ConfigEntry<float> JotunnShieldTowerSpeedBonus;

        // =====================================================
        // 동적 값 프로퍼티 (서버 동기화 지원)
        // =====================================================

        // === 필요 포인트 동적 값 ===
        public static int DefenseRootRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("defense_root_required_points", DefenseRootRequiredPoints?.Value ?? 2);
        public static int DefenseStep1RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("defense_step1_required_points", DefenseStep1RequiredPoints?.Value ?? 2);
        public static int DefenseStep2RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("defense_step2_required_points", DefenseStep2RequiredPoints?.Value ?? 2);
        public static int DefenseStep3RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("defense_step3_required_points", DefenseStep3RequiredPoints?.Value ?? 3);
        public static int DefenseStep4RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("defense_step4_required_points", DefenseStep4RequiredPoints?.Value ?? 3);
        public static int DefenseStep5RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("defense_step5_required_points", DefenseStep5RequiredPoints?.Value ?? 3);
        public static int DefenseStep6RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("defense_step6_required_points", DefenseStep6RequiredPoints?.Value ?? 4);

        // === Tier 0: 방어 전문가 ===
        public static float DefenseRootHealthBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Root_HealthBonus", DefenseRootHealthBonus?.Value ?? 5f);

        public static float DefenseRootArmorBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Root_ArmorBonus", DefenseRootArmorBonus?.Value ?? 2f);

        // === Tier 1: 피부경화 ===
        public static float SurvivalHealthBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Survival_HealthBonus", SurvivalHealthBonus?.Value ?? 5f);

        public static float SurvivalArmorBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Survival_ArmorBonus", SurvivalArmorBonus?.Value ?? 5f);

        // === Tier 2: 심신단련 ===
        public static float DodgeStaminaBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Dodge_StaminaBonus", DodgeStaminaBonus?.Value ?? 10f);

        public static float DodgeEitrBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Dodge_EitrBonus", DodgeEitrBonus?.Value ?? 10f);

        // === Tier 2: 체력단련 ===
        public static float HealthBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Health_HealthBonus", HealthBonus?.Value ?? 20f);

        public static float HealthArmorBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Health_ArmorBonus", HealthArmorBonus?.Value ?? 5f);

        // === Tier 3: 단전호흡 ===
        public static float BreathEitrBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Breath_EitrBonus", BreathEitrBonus?.Value ?? 10f);

        // === Tier 3: 회피단련 ===
        public static float AgileDodgeBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Agile_DodgeBonus", AgileDodgeBonus?.Value ?? 5f);

        public static float AgileInvincibilityBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Agile_InvincibilityBonus", AgileInvincibilityBonus?.Value ?? 20f);

        // === Tier 3: 체력증강 ===
        public static float BoostHealthBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Boost_HealthBonus", BoostHealthBonus?.Value ?? 15f);

        // === Tier 3: 방패훈련 ===
        public static float ShieldTrainingBlockPowerBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_ShieldTraining_BlockPower", ShieldTrainingBlockPowerBonus?.Value ?? 100f);

        // === Tier 4: 발구르기 ===
        public static float StompRadiusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Stomp_Radius", StompRadius?.Value ?? 3.0f);

        public static float StompKnockbackValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Stomp_Knockback", StompKnockback?.Value ?? 20f);

        public static float StompCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Stomp_Cooldown", StompCooldown?.Value ?? 120f);

        public static float StompHealthThresholdValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Stomp_HealthThreshold", StompHealthThreshold?.Value ?? 0.35f);

        public static float StompVFXDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Stomp_VFXDuration", StompVFXDuration?.Value ?? 1.0f);

        // === Tier 5: 지구력 ===
        public static float FocusRunStaminaReductionValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Focus_RunStaminaReduction", FocusRunStaminaReduction?.Value ?? 10f);

        public static float FocusJumpStaminaReductionValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Focus_JumpStaminaReduction", FocusJumpStaminaReduction?.Value ?? 10f);

        // === Tier 5: 기민함 ===
        public static float StaminaDodgeBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Stamina_DodgeBonus", StaminaDodgeBonus?.Value ?? 5f);

        public static float StaminaRollStaminaReductionValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Stamina_RollStaminaReduction", StaminaRollStaminaReduction?.Value ?? 12f);

        // === Tier 5: 트롤의 재생력 ===
        public static float TrollRegenBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_TrollRegen_Bonus", TrollRegenBonus?.Value ?? 5f);

        public static float TrollRegenIntervalValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_TrollRegen_Interval", TrollRegenInterval?.Value ?? 2f);

        // === Tier 5: 막기달인 ===
        public static float ParryMasterBlockPowerBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_ParryMaster_BlockPower", ParryMasterBlockPowerBonus?.Value ?? 100f);

        public static float ParryMasterParryDurationBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_ParryMaster_ParryDuration", ParryMasterParryDurationBonus?.Value ?? 1f);

        // === Tier 6: 신경강화 ===
        public static float AttackDodgeBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Attack_DodgeBonus", AttackDodgeBonus?.Value ?? 5f);

        // === Tier 6: 요툰의 생명력 ===
        public static float BodyHealthBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Body_HealthBonus", BodyHealthBonus?.Value ?? 30f);

        public static float BodyArmorBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Body_ArmorBonus", BodyArmorBonus?.Value ?? 10f);

        // === Tier 6: 요툰의 방패 ===
        public static float JotunnShieldBlockStaminaReductionValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_JotunnShield_BlockStaminaReduction", JotunnShieldBlockStaminaReduction?.Value ?? 25f);

        public static float JotunnShieldNormalSpeedBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_JotunnShield_NormalSpeedBonus", JotunnShieldNormalSpeedBonus?.Value ?? 5f);

        public static float JotunnShieldTowerSpeedBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_JotunnShield_TowerSpeedBonus", JotunnShieldTowerSpeedBonus?.Value ?? 10f);

        // =====================================================
        // 초기화 메서드
        // =====================================================

        /// <summary>
        /// Defense Config 초기화
        /// </summary>
        /// <param name="config">BepInEx ConfigFile</param>
        public static void Initialize(ConfigFile config)
        {
            // ===========================================
            // 필요 포인트 설정
            // ===========================================

            DefenseRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier0_방어전문가_필요포인트", 2,
                "Tier 0: 방어 전문가(defense_root) - 필요 포인트");

            DefenseStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier1_피부경화_필요포인트", 2,
                "Tier 1: 피부경화(defense_Step1_survival) - 필요 포인트");

            DefenseStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier2_심신체력_필요포인트", 2,
                "Tier 2: 심신단련/체력단련 - 필요 포인트");

            DefenseStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_단전회피체력방패_필요포인트", 3,
                "Tier 3: 단전호흡/회피단련/체력증강/방패훈련 - 필요 포인트");

            DefenseStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_충격발구바위_필요포인트", 3,
                "Tier 4: 충격파방출/발구르기/바위피부 - 필요 포인트");

            DefenseStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_지구기민트롤막기_필요포인트", 3,
                "Tier 5: 지구력/기민함/트롤의재생력/막기달인 - 필요 포인트");

            DefenseStep6RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_최종스킬_필요포인트", 4,
                "Tier 6: 최종 스킬들 - 필요 포인트");

            // ===========================================
            // Tier 0: 방어 전문가
            // ===========================================

            DefenseRootHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier0_방어전문가_체력보너스",
                5f,
                "Tier 0: 방어 전문가(defense_root) - 체력 보너스"
            );

            DefenseRootArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier0_방어전문가_방어력보너스",
                2f,
                "Tier 0: 방어 전문가(defense_root) - 방어력 보너스"
            );

            // ===========================================
            // Tier 1: 피부경화
            // ===========================================

            SurvivalHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier1_피부경화_체력보너스",
                5f,
                "Tier 1: 피부경화(defense_Step1_survival) - 체력 보너스"
            );

            SurvivalArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier1_피부경화_방어력보너스",
                5f,
                "Tier 1: 피부경화(defense_Step1_survival) - 방어력 보너스"
            );

            // ===========================================
            // Tier 2: 심신단련, 체력단련
            // ===========================================

            DodgeStaminaBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier2_심신단련_스태미나보너스",
                25f,
                "Tier 2: 심신단련(defense_Step2_dodge) - 스태미나 보너스"
            );

            DodgeEitrBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier2_심신단련_에이트르보너스",
                25f,
                "Tier 2: 심신단련(defense_Step2_dodge) - 에이트르 보너스"
            );

            HealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier2_체력단련_체력보너스",
                20f,
                "Tier 2: 체력단련(defense_Step2_health) - 체력 보너스"
            );

            HealthArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier2_체력단련_방어력보너스",
                5f,
                "Tier 2: 체력단련(defense_Step2_health) - 방어력 보너스"
            );

            // ===========================================
            // Tier 3: 단전호흡, 회피단련, 체력증강, 방패훈련
            // ===========================================

            BreathEitrBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier3_단전호흡_에이트르보너스",
                10f,
                "Tier 3: 단전호흡(defense_Step3_breath) - 에이트르 보너스"
            );

            AgileDodgeBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier3_회피단련_회피율보너스",
                5f,
                "Tier 3: 회피단련(defense_Step3_agile) - 회피율 보너스 (%)"
            );

            AgileInvincibilityBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier3_회피단련_무적시간증가",
                20f,
                "Tier 3: 회피단련(defense_Step3_agile) - 구르기 무적시간 증가 (%)"
            );

            BoostHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier3_체력증강_체력보너스",
                15f,
                "Tier 3: 체력증강(defense_Step3_boost) - 체력 보너스"
            );

            ShieldTrainingBlockPowerBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier3_방패훈련_방패방어력보너스",
                100f,
                "Tier 3: 방패훈련(defense_Step3_shield) - 방패 방어력 보너스"
            );

            // ===========================================
            // Tier 4: 발구르기 (자동 발동 패시브)
            // ===========================================

            StompRadius = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier4_발구르기_효과범위",
                3.0f,
                "Tier 4: 발구르기(defense_Step4_instant) - 효과 범위 (미터)"
            );

            StompKnockback = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier4_발구르기_넉백힘",
                20f,
                "Tier 4: 발구르기(defense_Step4_instant) - 넉백 힘"
            );

            StompCooldown = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier4_발구르기_쿨타임",
                120f,
                "Tier 4: 발구르기(defense_Step4_instant) - 쿨타임 (초)"
            );

            StompHealthThreshold = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier4_발구르기_체력임계값",
                0.35f,
                "Tier 4: 발구르기(defense_Step4_instant) - 자동 발동 체력 임계값 (0.35 = 35%)"
            );

            StompVFXDuration = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier4_발구르기_VFX지속시간",
                1.0f,
                "Tier 4: 발구르기(defense_Step4_instant) - VFX 지속시간 (초)"
            );

            // ===========================================
            // Tier 5: 지구력, 기민함, 트롤의 재생력, 막기달인
            // ===========================================

            FocusRunStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_지구력_달리기스태미나감소",
                10f,
                "Tier 5: 지구력(defense_Step5_focus) - 달리기 스태미나 감소 (%)"
            );

            FocusJumpStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_지구력_점프스태미나감소",
                10f,
                "Tier 5: 지구력(defense_Step5_focus) - 점프 스태미나 감소 (%)"
            );

            StaminaDodgeBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_기민함_회피율보너스",
                5f,
                "Tier 5: 기민함(defense_Step5_stamina) - 회피율 보너스 (%)"
            );

            StaminaRollStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_기민함_구르기스태미나감소",
                12f,
                "Tier 5: 기민함(defense_Step5_stamina) - 구르기 스태미나 감소 (%)"
            );

            TrollRegenBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_트롤의재생력_체력재생보너스",
                5f,
                "Tier 5: 트롤의 재생력(defense_Step5_heal) - 체력 재생 보너스 (초당)"
            );

            TrollRegenInterval = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_트롤의재생력_재생간격",
                2f,
                "Tier 5: 트롤의 재생력(defense_Step5_heal) - 재생 간격 (초)"
            );

            ParryMasterBlockPowerBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_막기달인_방패방어력보너스",
                100f,
                "Tier 5: 막기달인(defense_Step5_parry) - 방패 방어력 보너스"
            );

            ParryMasterParryDurationBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier5_막기달인_패링지속시간보너스",
                1f,
                "Tier 5: 막기달인(defense_Step5_parry) - 패링 지속시간 보너스 (초)"
            );

            // ===========================================
            // Tier 6: 신경강화, 요툰의 생명력, 요툰의 방패
            // ===========================================

            AttackDodgeBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier6_신경강화_회피율보너스",
                5f,
                "Tier 6: 신경강화(defense_Step6_attack) - 회피율 보너스 (영구, %)"
            );

            BodyHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier6_요툰의생명력_체력보너스",
                30f,
                "Tier 6: 요툰의 생명력(defense_Step6_body) - 체력 보너스 (%)"
            );

            BodyArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier6_요툰의생명력_방어력보너스",
                10f,
                "Tier 6: 요툰의 생명력(defense_Step6_body) - 방어력 보너스 (%)"
            );

            JotunnShieldBlockStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier6_요툰의방패_블럭스태미나감소",
                25f,
                "Tier 6: 요툰의 방패(defense_Step6_true) - 방패 블럭 스태미나 감소 (%)"
            );

            JotunnShieldNormalSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier6_요툰의방패_일반방패이동속도보상",
                5f,
                "Tier 6: 요툰의 방패(defense_Step6_true) - 일반 방패 이동속도 보상 (%)"
            );

            JotunnShieldTowerSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree",
                "Tier6_요툰의방패_대형방패이동속도보상",
                10f,
                "Tier 6: 요툰의 방패(defense_Step6_true) - Tower/대형 방패 이동속도 보상 (%)"
            );
        }
    }
}

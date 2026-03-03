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
        // 필요 포인트 설정 (각 스킬 독립)
        // =====================================================
        public static ConfigEntry<int> DefenseRootRequiredPoints;            // Tier 0: 방어 전문가
        public static ConfigEntry<int> DefenseStep1RequiredPoints;           // Tier 1: 피부경화
        public static ConfigEntry<int> DefenseStep2DodgeRequiredPoints;      // Tier 2-1: 심신단련
        public static ConfigEntry<int> DefenseStep2HealthRequiredPoints;     // Tier 2-2: 체력단련
        public static ConfigEntry<int> DefenseStep3BreathRequiredPoints;     // Tier 3-1: 단전호흡
        public static ConfigEntry<int> DefenseStep3AgileRequiredPoints;      // Tier 3-2: 회피단련
        public static ConfigEntry<int> DefenseStep3BoostRequiredPoints;      // Tier 3-3: 체력증강
        public static ConfigEntry<int> DefenseStep3ShieldRequiredPoints;     // Tier 3-4: 방패훈련
        public static ConfigEntry<int> DefenseStep4MentalRequiredPoints;     // Tier 4-1: 충격파방출
        public static ConfigEntry<int> DefenseStep4InstantRequiredPoints;    // Tier 4-2: 발구르기
        public static ConfigEntry<int> DefenseStep4TankerRequiredPoints;     // Tier 4-3: 바위피부
        public static ConfigEntry<int> DefenseStep5FocusRequiredPoints;      // Tier 5-1: 지구력
        public static ConfigEntry<int> DefenseStep5StaminaRequiredPoints;    // Tier 5-2: 기민함
        public static ConfigEntry<int> DefenseStep5HealRequiredPoints;       // Tier 5-3: 트롤의 재생력
        public static ConfigEntry<int> DefenseStep5ParryRequiredPoints;      // Tier 5-4: 막기달인
        public static ConfigEntry<int> DefenseStep6MindRequiredPoints;       // Tier 6-1: 마인드쉴드
        public static ConfigEntry<int> DefenseStep6AttackRequiredPoints;     // Tier 6-2: 신경강화
        public static ConfigEntry<int> DefenseStep6DoubleJumpRequiredPoints; // Tier 6-3: 이단점프
        public static ConfigEntry<int> DefenseStep6BodyRequiredPoints;       // Tier 6-4: 요툰의 생명력
        public static ConfigEntry<int> DefenseStep6TrueRequiredPoints;       // Tier 6-5: 요툰의 방패

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

        /// <summary>충격파방출 - 효과 범위 (미터)</summary>
        public static ConfigEntry<float> ShockwaveRadius;

        /// <summary>충격파방출 - 기절 지속시간 (초)</summary>
        public static ConfigEntry<float> ShockwaveStunDuration;

        /// <summary>충격파방출 - 쿨타임 (초)</summary>
        public static ConfigEntry<float> ShockwaveCooldown;

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

        /// <summary>
        /// defense_Step4_tanker: 바위피부 - 방어력 증폭 (%)
        /// 투구/흉갑/각반/방패 방어력에 각각 적용
        /// </summary>
        public static ConfigEntry<float> TankerArmorBonus;

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
        /// defense_Step6_body: 요툰의 생명력 - 물리/속성 저항 (%)
        /// </summary>
        public static ConfigEntry<float> BodyArmorBonus;  // 변수명 유지 (config key 호환)

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

        // === 필요 포인트 동적 값 (각 스킬 독립) ===
        public static int DefenseRootRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_root_required_points", DefenseRootRequiredPoints?.Value ?? 2);
        public static int DefenseStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step1_required_points", DefenseStep1RequiredPoints?.Value ?? 2);
        public static int DefenseStep2DodgeRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step2_dodge_required_points", DefenseStep2DodgeRequiredPoints?.Value ?? 2);
        public static int DefenseStep2HealthRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step2_health_required_points", DefenseStep2HealthRequiredPoints?.Value ?? 2);
        public static int DefenseStep3BreathRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step3_breath_required_points", DefenseStep3BreathRequiredPoints?.Value ?? 3);
        public static int DefenseStep3AgileRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step3_agile_required_points", DefenseStep3AgileRequiredPoints?.Value ?? 3);
        public static int DefenseStep3BoostRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step3_boost_required_points", DefenseStep3BoostRequiredPoints?.Value ?? 3);
        public static int DefenseStep3ShieldRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step3_shield_required_points", DefenseStep3ShieldRequiredPoints?.Value ?? 3);
        public static int DefenseStep4MentalRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step4_mental_required_points", DefenseStep4MentalRequiredPoints?.Value ?? 3);
        public static int DefenseStep4InstantRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step4_instant_required_points", DefenseStep4InstantRequiredPoints?.Value ?? 3);
        public static int DefenseStep4TankerRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step4_tanker_required_points", DefenseStep4TankerRequiredPoints?.Value ?? 3);
        public static int DefenseStep5FocusRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step5_focus_required_points", DefenseStep5FocusRequiredPoints?.Value ?? 3);
        public static int DefenseStep5StaminaRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step5_stamina_required_points", DefenseStep5StaminaRequiredPoints?.Value ?? 3);
        public static int DefenseStep5HealRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step5_heal_required_points", DefenseStep5HealRequiredPoints?.Value ?? 3);
        public static int DefenseStep5ParryRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step5_parry_required_points", DefenseStep5ParryRequiredPoints?.Value ?? 3);
        public static int DefenseStep6MindRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step6_mind_required_points", DefenseStep6MindRequiredPoints?.Value ?? 4);
        public static int DefenseStep6AttackRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step6_attack_required_points", DefenseStep6AttackRequiredPoints?.Value ?? 4);
        public static int DefenseStep6DoubleJumpRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step6_doublejump_required_points", DefenseStep6DoubleJumpRequiredPoints?.Value ?? 4);
        public static int DefenseStep6BodyRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step6_body_required_points", DefenseStep6BodyRequiredPoints?.Value ?? 4);
        public static int DefenseStep6TrueRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("defense_step6_true_required_points", DefenseStep6TrueRequiredPoints?.Value ?? 4);

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

        // === Tier 4: 충격파방출 ===
        public static float ShockwaveRadiusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Shockwave_Radius", ShockwaveRadius?.Value ?? 3.0f);

        public static float ShockwaveStunDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Shockwave_StunDuration", ShockwaveStunDuration?.Value ?? 1.0f);

        public static float ShockwaveCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Shockwave_Cooldown", ShockwaveCooldown?.Value ?? 120f);

        // === Tier 4: 바위피부 ===
        public static float TankerArmorBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Defense_Tanker_ArmorBonus", TankerArmorBonus?.Value ?? 12f);

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
            // Tier 0: Defense Expert (방어 전문가)
            // ===========================================

            DefenseRootHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier0_DefenseExpert_HPBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier0_DefenseExpert_HPBonus"), order: 80);

            DefenseRootArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier0_DefenseExpert_ArmorBonus", 2f,
                SkillTreeConfig.GetConfigDescription("Tier0_DefenseExpert_ArmorBonus"), order: 79);

            DefenseRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier0_DefenseExpert_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier0_DefenseExpert_RequiredPoints"), order: 78);

            // ===========================================
            // Tier 1: Skin Hardening (피부경화)
            // ===========================================

            SurvivalHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier1_SkinHardening_HPBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier1_SkinHardening_HPBonus"), order: 76);

            SurvivalArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier1_SkinHardening_ArmorBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier1_SkinHardening_ArmorBonus"), order: 75);

            DefenseStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier1_SkinHardening_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier1_SkinHardening_RequiredPoints"), order: 74);

            // ===========================================
            // Tier 2: Mind-Body Training (심신단련)
            // ===========================================

            DodgeStaminaBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier2_MindBodyTraining_StaminaBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Tier2_MindBodyTraining_StaminaBonus"), order: 72);

            DodgeEitrBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier2_MindBodyTraining_EitrBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Tier2_MindBodyTraining_EitrBonus"), order: 71);

            DefenseStep2DodgeRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier2_MindTraining_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_MindTraining_RequiredPoints"), order: 70);

            // ===========================================
            // Tier 2: Health Training (체력단련)
            // ===========================================

            HealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier2_HealthTraining_HPBonus", 20f,
                SkillTreeConfig.GetConfigDescription("Tier2_HealthTraining_HPBonus"), order: 68);

            HealthArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier2_HealthTraining_ArmorBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier2_HealthTraining_ArmorBonus"), order: 67);

            DefenseStep2HealthRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier2_HealthTraining_RequiredPoints", 2,
                SkillTreeConfig.GetConfigDescription("Tier2_HealthTraining_RequiredPoints"), order: 66);

            // ===========================================
            // Tier 3: Core Breathing (단전호흡)
            // ===========================================

            BreathEitrBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_CoreBreathing_EitrBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier3_CoreBreathing_EitrBonus"), order: 64);

            DefenseStep3BreathRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_CoreBreathing_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier3_CoreBreathing_RequiredPoints"), order: 63);

            // ===========================================
            // Tier 3: Evasion Training (회피단련)
            // ===========================================

            AgileDodgeBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_EvasionTraining_DodgeBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier3_EvasionTraining_DodgeBonus"), order: 61);

            AgileInvincibilityBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_EvasionTraining_InvincibilityBonus", 20f,
                SkillTreeConfig.GetConfigDescription("Tier3_EvasionTraining_InvincibilityBonus"), order: 60);

            DefenseStep3AgileRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_EvasionTraining_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier3_EvasionTraining_RequiredPoints"), order: 59);

            // ===========================================
            // Tier 3: Health Boost (체력증강)
            // ===========================================

            BoostHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_HealthBoost_HPBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Tier3_HealthBoost_HPBonus"), order: 57);

            DefenseStep3BoostRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_HealthBoost_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier3_HealthBoost_RequiredPoints"), order: 56);

            // ===========================================
            // Tier 3: Shield Training (방패훈련)
            // ===========================================

            ShieldTrainingBlockPowerBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_ShieldTraining_BlockPowerBonus", 100f,
                SkillTreeConfig.GetConfigDescription("Tier3_ShieldTraining_BlockPowerBonus"), order: 54);

            DefenseStep3ShieldRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier3_ShieldTraining_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier3_ShieldTraining_RequiredPoints"), order: 53);

            // ===========================================
            // Tier 4: Shockwave (충격파방출)
            // ===========================================

            ShockwaveRadius = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_Shockwave_Radius", 3.0f,
                SkillTreeConfig.GetConfigDescription("Tier4_Shockwave_Radius"), order: 51);

            ShockwaveStunDuration = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_Shockwave_StunDuration", 1.0f,
                SkillTreeConfig.GetConfigDescription("Tier4_Shockwave_StunDuration"), order: 50);

            ShockwaveCooldown = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_Shockwave_Cooldown", 120f,
                SkillTreeConfig.GetConfigDescription("Tier4_Shockwave_Cooldown"), order: 49);

            DefenseStep4MentalRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_Shockwave_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier4_Shockwave_RequiredPoints"), order: 48);

            // ===========================================
            // Tier 4: Ground Stomp (발구르기)
            // ===========================================

            StompRadius = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_GroundStomp_Radius", 3.0f,
                SkillTreeConfig.GetConfigDescription("Tier4_GroundStomp_Radius"), order: 46);

            StompKnockback = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_GroundStomp_KnockbackForce", 20f,
                SkillTreeConfig.GetConfigDescription("Tier4_GroundStomp_KnockbackForce"), order: 45);

            StompCooldown = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_GroundStomp_Cooldown", 120f,
                SkillTreeConfig.GetConfigDescription("Tier4_GroundStomp_Cooldown"), order: 44);

            StompHealthThreshold = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_GroundStomp_HPThreshold", 0.35f,
                SkillTreeConfig.GetConfigDescription("Tier4_GroundStomp_HPThreshold"), order: 43);

            StompVFXDuration = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_GroundStomp_VFXDuration", 1.0f,
                SkillTreeConfig.GetConfigDescription("Tier4_GroundStomp_VFXDuration"), order: 42);

            DefenseStep4InstantRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_GroundStomp_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier4_GroundStomp_RequiredPoints"), order: 41);

            // ===========================================
            // Tier 4: Rock Skin (바위피부)
            // ===========================================

            TankerArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_RockSkin_ArmorBonus", 12f,
                SkillTreeConfig.GetConfigDescription("Tier4_RockSkin_ArmorBonus"), order: 39);

            DefenseStep4TankerRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier4_RockSkin_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier4_RockSkin_RequiredPoints"), order: 38);

            // ===========================================
            // Tier 5: Endurance (지구력)
            // ===========================================

            FocusRunStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_Endurance_RunStaminaReduction", 10f,
                SkillTreeConfig.GetConfigDescription("Tier5_Endurance_RunStaminaReduction"), order: 36);

            FocusJumpStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_Endurance_JumpStaminaReduction", 10f,
                SkillTreeConfig.GetConfigDescription("Tier5_Endurance_JumpStaminaReduction"), order: 35);

            DefenseStep5FocusRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_Endurance_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_Endurance_RequiredPoints"), order: 34);

            // ===========================================
            // Tier 5: Agility (기민함)
            // ===========================================

            StaminaDodgeBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_Agility_DodgeBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier5_Agility_DodgeBonus"), order: 32);

            StaminaRollStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_Agility_RollStaminaReduction", 12f,
                SkillTreeConfig.GetConfigDescription("Tier5_Agility_RollStaminaReduction"), order: 31);

            DefenseStep5StaminaRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_Agility_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_Agility_RequiredPoints"), order: 30);

            // ===========================================
            // Tier 5: Troll Regen (트롤의 재생력)
            // ===========================================

            TrollRegenBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_TrollRegen_HPRegenBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier5_TrollRegen_HPRegenBonus"), order: 28);

            TrollRegenInterval = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_TrollRegen_RegenInterval", 2f,
                SkillTreeConfig.GetConfigDescription("Tier5_TrollRegen_RegenInterval"), order: 27);

            DefenseStep5HealRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_TrollRegen_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_TrollRegen_RequiredPoints"), order: 26);

            // ===========================================
            // Tier 5: Block Master (막기달인)
            // ===========================================

            ParryMasterBlockPowerBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_BlockMaster_ShieldBlockPowerBonus", 100f,
                SkillTreeConfig.GetConfigDescription("Tier5_BlockMaster_ShieldBlockPowerBonus"), order: 24);

            ParryMasterParryDurationBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_BlockMaster_ParryDurationBonus", 1f,
                SkillTreeConfig.GetConfigDescription("Tier5_BlockMaster_ParryDurationBonus"), order: 23);

            DefenseStep5ParryRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier5_BlockMaster_RequiredPoints", 3,
                SkillTreeConfig.GetConfigDescription("Tier5_BlockMaster_RequiredPoints"), order: 22);

            // ===========================================
            // Tier 6: Mind Shield (마인드쉴드)
            // ===========================================

            DefenseStep6MindRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_MindShield_RequiredPoints", 4,
                SkillTreeConfig.GetConfigDescription("Tier6_MindShield_RequiredPoints"), order: 20);

            // ===========================================
            // Tier 6: Nerve Enhancement (신경강화)
            // ===========================================

            AttackDodgeBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_NerveEnhancement_DodgeBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_NerveEnhancement_DodgeBonus"), order: 18);

            DefenseStep6AttackRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_NerveEnhancement_RequiredPoints", 4,
                SkillTreeConfig.GetConfigDescription("Tier6_NerveEnhancement_RequiredPoints"), order: 17);

            // ===========================================
            // Tier 6: Double Jump (이단점프)
            // ===========================================

            DefenseStep6DoubleJumpRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_DoubleJump_RequiredPoints", 4,
                SkillTreeConfig.GetConfigDescription("Tier6_DoubleJump_RequiredPoints"), order: 15);

            // ===========================================
            // Tier 6: Jotunn's Vitality (요툰의 생명력)
            // ===========================================

            BodyHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_JotunnVitality_HPBonus", 30f,
                SkillTreeConfig.GetConfigDescription("Tier6_JotunnVitality_HPBonus"), order: 13);

            BodyArmorBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_JotunnVitality_ArmorBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier6_JotunnVitality_ArmorBonus"), order: 12);

            DefenseStep6BodyRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_JotunnVitality_RequiredPoints", 4,
                SkillTreeConfig.GetConfigDescription("Tier6_JotunnVitality_RequiredPoints"), order: 11);

            // ===========================================
            // Tier 6: Jotunn's Shield (요툰의 방패)
            // ===========================================

            JotunnShieldBlockStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_JotunnShield_BlockStaminaReduction", 25f,
                SkillTreeConfig.GetConfigDescription("Tier6_JotunnShield_BlockStaminaReduction"), order: 9);

            JotunnShieldNormalSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_JotunnShield_NormalShieldMoveSpeedBonus", 5f,
                SkillTreeConfig.GetConfigDescription("Tier6_JotunnShield_NormalShieldMoveSpeedBonus"), order: 8);

            JotunnShieldTowerSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_JotunnShield_TowerShieldMoveSpeedBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Tier6_JotunnShield_TowerShieldMoveSpeedBonus"), order: 7);

            DefenseStep6TrueRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Defense Tree", "Tier6_JotunnShield_RequiredPoints", 4,
                SkillTreeConfig.GetConfigDescription("Tier6_JotunnShield_RequiredPoints"), order: 6);
        }
    }
}

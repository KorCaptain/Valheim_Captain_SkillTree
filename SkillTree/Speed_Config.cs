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
                "Speed Tree", "Tier0_속도전문가_필요포인트", 2,
                "Tier 0: 속도 전문가(speed_root) - 필요 포인트");

            SpeedStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_민첩함의기초_필요포인트", 2,
                "Tier 1: 민첩함의 기초(speed_base) - 필요 포인트");

            SpeedStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_무기특화_필요포인트", 2,
                "Tier 2: 무기별 특화 스킬 - 필요 포인트");

            SpeedStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_수련자_필요포인트", 2,
                "Tier 3: 수련자 스킬 - 필요 포인트");

            SpeedStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_마스터_필요포인트", 2,
                "Tier 4: 마스터 스킬 - 필요 포인트");

            SpeedStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_점프숙련자_필요포인트", 3,
                "Tier 5: 점프 숙련자(agility_peak) - 필요 포인트");

            SpeedStep6RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_스탯_필요포인트", 2,
                "Tier 6: 스탯 스킬 - 필요 포인트");

            SpeedStep7RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_숙련자_필요포인트", 3,
                "Tier 7: 숙련자(all_master) - 필요 포인트");

            SpeedStep8RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_최종가속_필요포인트", 2,
                "Tier 8: 최종 가속 스킬 - 필요 포인트");

            // === 티어0: 속도 전문가 ===
            SpeedRootMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_속도전문가_이동속도", 5f,
                "티어0: 속도 전문가 - 이동속도 보너스 (%)");

            // === 티어1: 민첩함의 기초 ===
            SpeedBaseDodgeMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_민첩함의기초_구르기후이동속도", 15f,
                "티어1: 민첩함의 기초 - 구르기 후 이동속도 보너스 (%)");

            SpeedBaseDodgeDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_민첩함의기초_버프지속시간", 2f,
                "티어1: 민첩함의 기초 - 구르기 버프 지속시간 (초)");

            // === 티어2: 무기별 특화 ===
            SpeedMeleeComboAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_연속의흐름_공격속도보너스", 8f,
                "티어2: 연속의 흐름 - 근접 2연속 적중 시 공격속도 보너스 (%)");

            SpeedMeleeComboStamina = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_연속의흐름_스태미나감소", 12f,
                "티어2: 연속의 흐름 - 근접 2연속 적중 시 스태미나 감소 (%)");

            SpeedMeleeComboDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_연속의흐름_지속시간", 3f,
                "티어2: 연속의 흐름 - 버프 지속시간 (초)");

            SpeedCrossbowExpertSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_석궁숙련자_이동속도보너스", 12f,
                "티어2: 석궁 숙련자 - 석궁 적중 시 이동속도 보너스 (%)");

            SpeedCrossbowExpertDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_석궁숙련자_버프지속시간", 4f,
                "티어2: 석궁 숙련자 - 버프 지속시간 (초)");

            SpeedCrossbowExpertReload = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_석궁숙련자_재장전속도보너스", 10f,
                "티어2: 석궁 숙련자 - 버프 중 재장전 속도 보너스 (%)");

            SpeedBowExpertStamina = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_활숙련자_스태미나감소", 10f,
                "티어2: 활 숙련자 - 활 2연속 적중 시 스태미나 감소 (%)");

            SpeedBowExpertDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_활숙련자_다음장전속도보너스", 15f,
                "티어2: 활 숙련자 - 다음 화살 장전 속도 보너스 (%)");

            SpeedStaffCastMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_이동시전_이동속도보너스", 8f,
                "티어2: 이동 시전 - 마법 시전 중 이동속도 보너스 (%)");

            SpeedStaffCastEitrReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_이동시전_에이트르감소", 10f,
                "티어2: 이동 시전 - 에이트르 소모 감소 (%)");

            // === 티어3: 수련자 ===
            SpeedEx1MeleeSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_수련자1_근접숙련보너스", 8f,
                "티어3: 수련자1 - 근접무기 숙련 보너스");

            SpeedEx1CrossbowSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_수련자1_석궁숙련보너스", 8f,
                "티어3: 수련자1 - 석궁 숙련 보너스");

            SpeedEx2StaffSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_수련자2_지팡이숙련보너스", 8f,
                "티어3: 수련자2 - 지팡이 숙련 보너스");

            SpeedEx2BowSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier3_수련자2_활숙련보너스", 8f,
                "티어3: 수련자2 - 활 숙련 보너스");

            // === 티어4: 마스터 ===
            SpeedFoodEfficiency = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_에너자이져_음식소모감소", 15f,
                "티어4: 에너자이져 - 음식 소모 속도 감소 (%)");

            SpeedShipBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier4_선장_배속도증가", 15f,
                "티어4: 선장 - 배 운전 속도 증가 (%)");

            // === 티어5: 점프 숙련자 ===
            JumpSkillLevelBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_점프숙련자_점프숙련보너스", 10f,
                "티어5: 점프 숙련자 - 점프 숙련 보너스");

            JumpStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier5_점프숙련자_점프스태미나감소", 10f,
                "티어5: 점프 숙련자 - 점프 스태미나 감소 (%)");

            // === 티어6: 스탯 ===
            SpeedDexterityAttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_민첩_근접공격속도보너스", 5f,
                "티어6: 민첩 - 근접 공격속도 보너스 (%)");

            SpeedDexterityMoveSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_민첩_이동속도보너스", 5f,
                "티어6: 민첩 - 이동속도 보너스 (%)");

            SpeedEnduranceStaminaBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_지구력_스태미나최대치", 25f,
                "티어6: 지구력 - 스태미나 최대치 보너스");

            SpeedIntellectEitrBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier6_지능_에이트르최대치", 35f,
                "티어6: 지능 - 에이트르 최대치 보너스");

            // === 티어7: 숙련자 ===
            AllMasterRunSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_숙련자_이동숙련보너스", 8f,
                "티어7: 숙련자 - 이동 숙련 보너스");

            AllMasterJumpSkill = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier7_숙련자_점프숙련보너스", 8f,
                "티어7: 숙련자 - 점프 숙련 보너스");

            // === 티어8: 최종 가속 ===
            SpeedMeleeAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_근접가속_공격속도보너스", 7f,
                "티어8: 근접 가속 - 근접 공격속도 보너스 (%)");

            SpeedMeleeComboTripleBonus = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_근접가속_3연속보너스", 25f,
                "티어8: 근접 가속 - 3연속 적중 시 다음 공격속도 보너스 (%)");

            SpeedCrossbowDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_석궁가속_재장전속도", 30f,
                "티어8: 석궁 가속 - 석궁 재장전 속도 보너스 (%)");

            SpeedCrossbowReloadMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_석궁가속_재장전중이동속도", 25f,
                "티어8: 석궁 가속 - 재장전 중 이동속도 보너스 (%)");

            SpeedBowDrawSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_활가속_장전속도", 15f,
                "티어8: 활 가속 - 활 장전 속도 보너스 (%)");

            SpeedBowDrawMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_활가속_장전중이동속도", 15f,
                "티어8: 활 가속 - 장전 중 이동속도 보너스 (%)");

            SpeedStaffCastSpeedFinal = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_시전가속_마법공격속도", 6f,
                "티어8: 시전 가속 - 마법 공격속도 보너스 (%)");

            SpeedStaffTripleEitrRecovery = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier8_시전가속_3연속에이트르회복", 12f,
                "티어8: 시전 가속 - 3연속 적중 시 에이트르 최대치의 회복률 (%)");

            // === 티어0~2 추가 설정 ===
            SpeedBaseAttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_민첩함의기초_공격속도", 5f,
                "티어1: 민첩함의 기초 - 공격속도 보너스 (%)");

            SpeedBaseDodgeSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier1_민첩함의기초_구르기속도", 10f,
                "티어1: 민첩함의 기초 - 구르기 속도 보너스 (%)");

            SpeedBaseMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_속도전문가_기본이동속도", 3f,
                "티어0: 속도 전문가 - 기본 이동속도 보너스 (%)");

            SpeedMeleeComboSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_연속의흐름_콤보속도", 5f,
                "티어2: 연속의 흐름 - 근접 콤보 속도 보너스 (%)");

            SpeedBowExpertDuration = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_활숙련자_버프지속시간", 5f,
                "티어2: 활 숙련자 버프 지속시간 (초)");

            SpeedStaffCastSpeed = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier2_이동시전_시전속도", 4f,
                "티어2: 이동 시전 - 지팡이 시전 중 이동속도 (%)");

            SpeedCooldownReduction = SkillTreeConfig.BindServerSync(config,
                "Speed Tree", "Tier0_속도전문가_쿨타임감소", 1f,
                "티어0: 속도 전문가 - 쿨타임 감소 (초)");

            Plugin.Log.LogDebug("[Speed_Config] 속도 전문가 트리 설정 초기화 완료");
        }
    }
}

using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기 스킬 트리 Config 설정
    /// </summary>
    public static class Mace_Config
    {
        // ===== Tier 0: 둔기 전문가 (Mace Expert) =====

        /// <summary>
        /// 둔기 전문가 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceExpertRequiredPoints;

        /// <summary>
        /// 둔기 전문가 - 피해 증가 (%)
        /// </summary>
        public static ConfigEntry<float> MaceExpertDamageBonus;

        /// <summary>
        /// 둔기 전문가 - 기절 확률 (%)
        /// </summary>
        public static ConfigEntry<float> MaceExpertStunChance;

        /// <summary>
        /// 둔기 전문가 - 기절 지속시간 (초)
        /// </summary>
        public static ConfigEntry<float> MaceExpertStunDuration;

        // ===== Tier 1: 둔기 공격력 강화 =====

        /// <summary>
        /// Tier 1 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep1RequiredPoints;

        /// <summary>
        /// Tier 1 - 둔기 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep1DamageBonus;

        // ===== Tier 2: 기절 강화 =====

        /// <summary>
        /// Tier 2 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep2RequiredPoints;

        /// <summary>
        /// Tier 2 - 기절 확률 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep2StunChanceBonus;

        /// <summary>
        /// Tier 2 - 기절 지속시간 보너스 (초)
        /// </summary>
        public static ConfigEntry<float> MaceStep2StunDurationBonus;

        // ===== Tier 3: 회전 타격 (Spin Strike) =====

        /// <summary>
        /// Tier 3 방어 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep3GuardRequiredPoints;

        /// <summary>
        /// Tier 3 회전 타격 - 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep3SpinDamageBonus;

        /// <summary>
        /// Tier 3 회전 타격 - AOE 범위 (미터)
        /// </summary>
        public static ConfigEntry<float> MaceStep3SpinRange;

        // ===== Tier 3: 무거운 타격 =====

        /// <summary>
        /// Tier 3 무거운 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep3HeavyRequiredPoints;

        /// <summary>
        /// Tier 3 무거운 - 공격력 보너스 (고정값)
        /// </summary>
        public static ConfigEntry<float> MaceStep3HeavyDamageBonus;

        // ===== Tier 4: 밀어내기 =====

        /// <summary>
        /// Tier 4 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep4RequiredPoints;

        /// <summary>
        /// Tier 4 - 넉백 확률 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep4KnockbackChance;

        // ===== Tier 5: 탱커 =====

        /// <summary>
        /// Tier 5 탱커 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep5TankRequiredPoints;

        /// <summary>
        /// Tier 5 탱커 - 체력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep5TankHealthBonus;

        /// <summary>
        /// Tier 5 탱커 - 받는 데미지 감소 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep5TankDamageReduction;

        // ===== Tier 5: 공격력 강화 =====

        /// <summary>
        /// Tier 5 DPS - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep5DpsRequiredPoints;

        /// <summary>
        /// Tier 5 DPS - 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep5DpsDamageBonus;

        // ===== Tier 6: 속공 (Swift Attack) =====

        /// <summary>
        /// Tier 6 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> MaceStep6RequiredPoints;

        /// <summary>
        /// Tier 6 속공 - 공격속도 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> MaceStep6AttackSpeedBonus;

        // ===== 분노의 망치 (Fury Hammer) - H키 액티브 스킬 =====

        /// <summary>
        /// 분노의 망치 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> FuryHammerRequiredPoints;

        /// <summary>
        /// 분노의 망치 - 1~4타 데미지 배율 (%) - 현재 공격력 기준
        /// </summary>
        public static ConfigEntry<float> FuryHammerNormalHitMultiplier;

        /// <summary>
        /// 분노의 망치 - 5타(최종타) 데미지 배율 (%) - 현재 공격력 기준
        /// </summary>
        public static ConfigEntry<float> FuryHammerFinalHitMultiplier;

        // 연속공격횟수와 공격간딜레이는 하드코딩 (5타, 0.5초 고정) - MaceSkills.FuryHammer.cs 참조

        /// <summary>
        /// 분노의 망치 - 스태미나 소모
        /// </summary>
        public static ConfigEntry<float> FuryHammerStaminaCost;

        /// <summary>
        /// 분노의 망치 - 쿨타임 (초)
        /// </summary>
        public static ConfigEntry<float> FuryHammerCooldown;

        /// <summary>
        /// 분노의 망치 - AOE 범위 (미터)
        /// </summary>
        public static ConfigEntry<float> FuryHammerAoeRadius;

        // ===== 수호자의 진심 (Guardian Heart) - G키 액티브 스킬 =====

        /// <summary>
        /// 수호자의 진심 - 쿨타임 (초)
        /// </summary>
        public static ConfigEntry<float> GuardianHeartCooldown;

        /// <summary>
        /// 수호자의 진심 - 스태미나 소모
        /// </summary>
        public static ConfigEntry<float> GuardianHeartStaminaCost;

        /// <summary>
        /// 수호자의 진심 - 버프 지속시간 (초)
        /// </summary>
        public static ConfigEntry<float> GuardianHeartDuration;

        /// <summary>
        /// 수호자의 진심 - 반사 데미지 비율 (%)
        /// </summary>
        public static ConfigEntry<float> GuardianHeartReflectPercent;

        /// <summary>
        /// 수호자의 진심 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> GuardianHeartRequiredPoints;

        // ===== 동적 값 프로퍼티 (서버 동기화 지원) =====

        // === Tier 0: 둔기 전문가 ===
        public static int MaceExpertRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Expert_RequiredPoints", MaceExpertRequiredPoints?.Value ?? 2);
        public static float MaceExpertDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Expert_DamageBonus", MaceExpertDamageBonus?.Value ?? 5f);
        public static float MaceExpertStunChanceValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Expert_StunChance", MaceExpertStunChance?.Value ?? 20f);
        public static float MaceExpertStunDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Expert_StunDuration", MaceExpertStunDuration?.Value ?? 0.5f);

        // === Tier 1: 둔기 공격력 강화 ===
        public static int MaceStep1RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step1_RequiredPoints", MaceStep1RequiredPoints?.Value ?? 1);
        public static float MaceStep1DamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step1_DamageBonus", MaceStep1DamageBonus?.Value ?? 10f);

        // === Tier 2: 기절 강화 ===
        public static int MaceStep2RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step2_RequiredPoints", MaceStep2RequiredPoints?.Value ?? 2);
        public static float MaceStep2StunChanceBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step2_StunChanceBonus", MaceStep2StunChanceBonus?.Value ?? 15f);
        public static float MaceStep2StunDurationBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step2_StunDurationBonus", MaceStep2StunDurationBonus?.Value ?? 0.5f);

        // === Tier 3: 회전 타격 ===
        public static int MaceStep3GuardRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step3_Guard_RequiredPoints", MaceStep3GuardRequiredPoints?.Value ?? 2);
        public static float MaceStep3SpinDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step3_SpinStrike_DamageBonus", MaceStep3SpinDamageBonus?.Value ?? 20f);
        public static float MaceStep3SpinRangeValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step3_SpinStrike_Range", MaceStep3SpinRange?.Value ?? 7f);

        // === Tier 3: 무거운 타격 ===
        public static int MaceStep3HeavyRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step3_Heavy_RequiredPoints", MaceStep3HeavyRequiredPoints?.Value ?? 3);
        public static float MaceStep3HeavyDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step3_Heavy_DamageBonus", MaceStep3HeavyDamageBonus?.Value ?? 20f);

        // === Tier 4: 밀어내기 ===
        public static int MaceStep4RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step4_RequiredPoints", MaceStep4RequiredPoints?.Value ?? 3);
        public static float MaceStep4KnockbackChanceValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step4_KnockbackChance", MaceStep4KnockbackChance?.Value ?? 30f);

        // === Tier 5: 탱커 ===
        public static int MaceStep5TankRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step5_Tank_RequiredPoints", MaceStep5TankRequiredPoints?.Value ?? 2);
        public static float MaceStep5TankHealthBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step5_Tank_HealthBonus", MaceStep5TankHealthBonus?.Value ?? 25f);
        public static float MaceStep5TankDamageReductionValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step5_Tank_DamageReduction", MaceStep5TankDamageReduction?.Value ?? 10f);

        // === Tier 5: 공격력 강화 ===
        public static int MaceStep5DpsRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step5_Dps_RequiredPoints", MaceStep5DpsRequiredPoints?.Value ?? 3);
        public static float MaceStep5DpsDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step5_Dps_DamageBonus", MaceStep5DpsDamageBonus?.Value ?? 20f);

        // === Tier 6: 속공 ===
        public static int MaceStep6RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_Step6_RequiredPoints", MaceStep6RequiredPoints?.Value ?? 2);
        public static float MaceStep6AttackSpeedBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_Step6_Sokgong_AttackSpeedBonus", MaceStep6AttackSpeedBonus?.Value ?? 10f);

        // === Tier 7: 분노의 망치 ===
        public static int FuryHammerRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_FuryHammer_RequiredPoints", FuryHammerRequiredPoints?.Value ?? 3);

        /// <summary>
        /// 분노의 망치 1~4타 데미지 배율 (서버 우선) - 현재 공격력 기준
        /// </summary>
        public static float FuryHammerNormalHitMultiplierValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_FuryHammer_NormalHitMultiplier", FuryHammerNormalHitMultiplier?.Value ?? 80f);

        /// <summary>
        /// 분노의 망치 5타(최종타) 데미지 배율 (서버 우선) - 현재 공격력 기준
        /// </summary>
        public static float FuryHammerFinalHitMultiplierValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_FuryHammer_FinalHitMultiplier", FuryHammerFinalHitMultiplier?.Value ?? 150f);

        // 연속공격횟수와 공격간딜레이는 하드코딩 (5타, 0.5초 고정) - MaceSkills.FuryHammer.cs 참조

        /// <summary>
        /// 분노의 망치 스태미나 소모 값 (서버 우선)
        /// </summary>
        public static float FuryHammerStaminaCostValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_FuryHammer_StaminaCost", FuryHammerStaminaCost?.Value ?? 40f);

        /// <summary>
        /// 분노의 망치 쿨타임 값 (서버 우선)
        /// </summary>
        public static float FuryHammerCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_FuryHammer_Cooldown", FuryHammerCooldown?.Value ?? 30f);

        /// <summary>
        /// 분노의 망치 AOE 범위 값 (서버 우선)
        /// </summary>
        public static float FuryHammerAoeRadiusValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_FuryHammer_AoeRadius", FuryHammerAoeRadius?.Value ?? 5f);

        /// <summary>
        /// 수호자의 진심 쿨타임 값 (서버 우선)
        /// </summary>
        public static float GuardianHeartCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_GuardianHeart_Cooldown", GuardianHeartCooldown?.Value ?? 45f);

        /// <summary>
        /// 수호자의 진심 스태미나 소모 값 (서버 우선)
        /// </summary>
        public static float GuardianHeartStaminaCostValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_GuardianHeart_StaminaCost", GuardianHeartStaminaCost?.Value ?? 20f);

        /// <summary>
        /// 수호자의 진심 버프 지속시간 값 (서버 우선)
        /// </summary>
        public static float GuardianHeartDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_GuardianHeart_Duration", GuardianHeartDuration?.Value ?? 30f);

        /// <summary>
        /// 수호자의 진심 반사 데미지 비율 값 (서버 우선)
        /// </summary>
        public static float GuardianHeartReflectPercentValue =>
            SkillTreeConfig.GetEffectiveValue("Mace_GuardianHeart_ReflectPercent", GuardianHeartReflectPercent?.Value ?? 6f);

        /// <summary>
        /// 수호자의 진심 필요 포인트 값 (서버 우선)
        /// </summary>
        public static int GuardianHeartRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Mace_GuardianHeart_RequiredPoints", GuardianHeartRequiredPoints?.Value ?? 3);

        // ===== 초기화 메서드 =====

        /// <summary>
        /// Mace Config 초기화
        /// </summary>
        /// <param name="config">BepInEx ConfigFile</param>
        public static void Initialize(ConfigFile config)
        {
            // Tier 0: 둔기 전문가 (Mace Expert)
            MaceExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier0_MaceExpert_DamageBonus",
                5f,
                SkillTreeConfig.GetConfigDescription("Tier0_MaceExpert_DamageBonus")
            );

            MaceExpertStunChance = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier0_MaceExpert_StunChance",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier0_MaceExpert_StunChance")
            );

            MaceExpertStunDuration = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier0_MaceExpert_StunDuration",
                0.5f,
                SkillTreeConfig.GetConfigDescription("Tier0_MaceExpert_StunDuration")
            );

            MaceExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier0_MaceExpert_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier0_MaceExpert_RequiredPoints")
            );

            // Tier 1: 둔기 강화
            MaceStep1DamageBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier1_MaceExpert_DamageBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier1_MaceExpert_DamageBonus")
            );

            MaceStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier1_MaceExpert_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier1_MaceExpert_RequiredPoints")
            );

            // Tier 2: 기절 강화
            MaceStep2StunChanceBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier2_StunBoost_StunChanceBonus",
                15f,
                SkillTreeConfig.GetConfigDescription("Tier2_StunBoost_StunChanceBonus")
            );

            MaceStep2StunDurationBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier2_StunBoost_StunDurationBonus",
                0.5f,
                SkillTreeConfig.GetConfigDescription("Tier2_StunBoost_StunDurationBonus")
            );

            MaceStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier2_StunBoost_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier2_StunBoost_RequiredPoints")
            );

            // Tier 3: 회전 타격
            MaceStep3SpinDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier3_SpinStrike_DamageBonus",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier3_SpinStrike_DamageBonus")
            );

            MaceStep3SpinRange = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier3_SpinStrike_Range",
                7f,
                SkillTreeConfig.GetConfigDescription("Tier3_SpinStrike_Range")
            );

            MaceStep3GuardRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier3_Guard_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier3_Guard_RequiredPoints")
            );

            // Tier 3: 무거운 일격
            MaceStep3HeavyDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier3_HeavyStrike_DamageBonus",
                3f,
                SkillTreeConfig.GetConfigDescription("Tier3_HeavyStrike_DamageBonus")
            );

            MaceStep3HeavyRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier3_HeavyStrike_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier3_HeavyStrike_RequiredPoints")
            );

            // Tier 4: 밀어내기
            MaceStep4KnockbackChance = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier4_Push_KnockbackChance",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier4_Push_KnockbackChance")
            );

            MaceStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier4_Push_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier4_Push_RequiredPoints")
            );

            // Tier 5: 탱커
            MaceStep5TankHealthBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier5_Tank_HealthBonus",
                25f,
                SkillTreeConfig.GetConfigDescription("Tier5_Tank_HealthBonus")
            );

            MaceStep5TankDamageReduction = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier5_Tank_DamageReduction",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier5_Tank_DamageReduction")
            );

            MaceStep5TankRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier5_Tank_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier5_Tank_RequiredPoints")
            );

            // Tier 5: 데미지 강화
            MaceStep5DpsDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier5_DPS_DamageBonus",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier5_DPS_DamageBonus")
            );

            MaceStep5DpsRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier5_DPS_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier5_DPS_RequiredPoints")
            );

            // Tier 6: 속공
            MaceStep6AttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier6_Sokgong_AttackSpeedBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier6_Sokgong_AttackSpeedBonus")
            );

            MaceStep6RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier6_Grandmaster_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier6_Grandmaster_RequiredPoints")
            );

            // Tier 7: 분노의 망치 (Fury Hammer)
            FuryHammerNormalHitMultiplier = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_FuryHammer_NormalHitMultiplier",
                80f,
                SkillTreeConfig.GetConfigDescription("Tier7_FuryHammer_NormalHitMultiplier")
            );

            FuryHammerFinalHitMultiplier = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_FuryHammer_FinalHitMultiplier",
                150f,
                SkillTreeConfig.GetConfigDescription("Tier7_FuryHammer_FinalHitMultiplier")
            );

            // 연속공격횟수와 공격간딜레이는 하드코딩 (5타, 0.5초 고정) - MaceSkills.FuryHammer.cs 참조

            FuryHammerStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_FuryHammer_StaminaCost",
                40f,
                SkillTreeConfig.GetConfigDescription("Tier7_FuryHammer_StaminaCost")
            );

            FuryHammerCooldown = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_FuryHammer_Cooldown",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier7_FuryHammer_Cooldown")
            );

            FuryHammerAoeRadius = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_FuryHammer_AoeRadius",
                5f,
                SkillTreeConfig.GetConfigDescription("Tier7_FuryHammer_AoeRadius")
            );

            FuryHammerRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_FuryHammer_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier7_FuryHammer_RequiredPoints")
            );

            // Tier 7: 수호자의 진심 (Guardian Heart)
            GuardianHeartCooldown = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_GuardianHeart_Cooldown",
                75f,
                SkillTreeConfig.GetConfigDescription("Tier7_GuardianHeart_Cooldown")
            );

            GuardianHeartStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_GuardianHeart_StaminaCost",
                25f,
                SkillTreeConfig.GetConfigDescription("Tier7_GuardianHeart_StaminaCost")
            );

            GuardianHeartDuration = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_GuardianHeart_Duration",
                45f,
                SkillTreeConfig.GetConfigDescription("Tier7_GuardianHeart_Duration")
            );

            GuardianHeartReflectPercent = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_GuardianHeart_ReflectPercent",
                6f,
                SkillTreeConfig.GetConfigDescription("Tier7_GuardianHeart_ReflectPercent")
            );

            GuardianHeartRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Mace Tree",
                "Tier7_GuardianHeart_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier7_GuardianHeart_RequiredPoints")
            );
        }
    }
}

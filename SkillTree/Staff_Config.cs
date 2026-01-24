using BepInEx.Configuration;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 지팡이 스킬 설정 시스템 (동적 컨피그)
    /// 지팡이 전문가 스킬 트리의 모든 설정값 관리
    /// </summary>
    public static class Staff_Config
    {
        // === 지팡이 전문가 설정 ===
        private static ConfigEntry<float> StaffExpertDamage;
        private static ConfigEntry<int> StaffExpertRequiredPoints;

        // === Step 2 설정 ===
        private static ConfigEntry<float> StaffFocusEitrReduction;
        private static ConfigEntry<int> StaffFocusRequiredPoints;
        private static ConfigEntry<float> StaffStreamCastSpeed;
        private static ConfigEntry<int> StaffStreamRequiredPoints;

        // === Step 3 설정 ===
        private static ConfigEntry<float> StaffAmpDamage;
        private static ConfigEntry<int> StaffAmpRequiredPoints;

        // === Step 4 설정 ===
        private static ConfigEntry<float> StaffRangeBonus;
        private static ConfigEntry<int> StaffRangeRequiredPoints;
        private static ConfigEntry<float> StaffLuckManaChance; // 행운 마력 확률
        private static ConfigEntry<int> StaffLuckManaRequiredPoints;
        private static ConfigEntry<float> StaffCritDamage;
        private static ConfigEntry<int> StaffCritDamageRequiredPoints;

        // Step 4: 속성 강화 스킬
        private static ConfigEntry<float> StaffFrostDamageBonus; // 냉기 속성
        private static ConfigEntry<float> StaffFireDamageBonus;  // 화염 속성
        private static ConfigEntry<float> StaffLightningDamageBonus; // 번개 속성

        // === Step 5 설정 ===
        private static ConfigEntry<float> StaffCritRate;
        private static ConfigEntry<int> StaffCritRateRequiredPoints;

        // === Step 6-1: 이중시전 (T키 액티브) 설정 ===
        private static ConfigEntry<int> StaffDoubleCastProjectileCount;
        private static ConfigEntry<float> StaffDoubleCastDamagePercent;
        private static ConfigEntry<float> StaffDoubleCastAngleOffset;
        private static ConfigEntry<float> StaffDoubleCastEitrCost;
        private static ConfigEntry<float> StaffDoubleCastCooldown;
        private static ConfigEntry<int> StaffDoubleCastRequiredPoints;

        // === Step 6-2: 힐러모드 (G키 액티브) 설정 ===
        private static ConfigEntry<int> StaffHealerModeDuration;
        private static ConfigEntry<float> StaffHealerModeEitrCost;
        private static ConfigEntry<float> StaffHealerModeCooldown;
        private static ConfigEntry<float> StaffHealerHealPercentage;
        private static ConfigEntry<float> StaffHealerHealRange;
        private static ConfigEntry<int> StaffHealerProjectileCount;
        private static ConfigEntry<float> StaffHealerProjectileInterval;
        private static ConfigEntry<int> StaffHealerRequiredPoints;

        // === 힐러모드 이펙트는 하드코딩으로 처리 (동적 연동 시스템) ===
        // 이펙트 관련 설정은 컨피그가 아닌 상수로 관리

        /// <summary>
        /// 컨피그 초기화
        /// </summary>
        public static void InitConfig(ConfigFile config)
        {
            try
            {
                // === Tier 0: 지팡이 전문가 ===
                StaffExpertDamage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier0_지팡이전문가_속성공격력보너스", 12f,
                    "Tier 0: 지팡이 전문가(staff_expert_damage) - 속성 공격력 증가 (%)");

                StaffExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier0_지팡이전문가_필요포인트", 2,
                    "Tier 0: 지팡이 전문가(staff_expert_damage) - 스킬 습득에 필요한 포인트");

                // === Tier 2 ===
                StaffFocusEitrReduction = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_정신집중_에이트르절약", 15f,
                    "Tier 2: 정신 집중(staff_step2_focus) - 에이트르 소모량 감소 (%)");

                StaffFocusRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_정신집중_필요포인트", 2,
                    "Tier 2: 정신 집중(staff_step2_focus) - 스킬 습득에 필요한 포인트");

                StaffStreamCastSpeed = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_마법흐름_에이트르보너스", 30f,
                    "Tier 2: 마법 흐름(staff_step2_stream) - 최대 에이트르 증가량");

                StaffStreamRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_마법흐름_필요포인트", 2,
                    "Tier 2: 마법 흐름(staff_step2_stream) - 스킬 습득에 필요한 포인트");

                // === Tier 3 ===
                StaffAmpDamage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_마법증폭_공격력", 10f,
                    "Tier 3: 마법 증폭(staff_step3_amp) - 추가 공격력 (%)");

                StaffAmpRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_마법증폭_필요포인트", 3,
                    "Tier 3: 마법 증폭(staff_step3_amp) - 스킬 습득에 필요한 포인트");

                // === Tier 4 ===
                StaffRangeBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_마법증폭_데미지보너스", 30f,
                    "Tier 4: 마법 증폭(staff_step4_magic_amp) - 지팡이 공격력 증가 (%)");

                StaffRangeRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_마법증폭_필요포인트", 2,
                    "Tier 4: 마법 증폭(staff_step4_magic_amp) - 스킬 습득에 필요한 포인트");

                StaffLuckManaChance = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_행운마력_확률", 35f,
                    "Tier 4: 행운 마력(staff_step4_surge) - 에이트르 무소모 확률 (%)");

                StaffLuckManaRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_행운마력_필요포인트", 3,
                    "Tier 4: 행운 마력(staff_step4_surge) - 스킬 습득에 필요한 포인트");

                StaffCritDamage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_치명타피해_증가율", 30f,
                    "Tier 4: 치명타 피해(staff_step4_crit_damage) - 치명타 피해 증가 (%)");

                StaffCritDamageRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_치명타피해_필요포인트", 3,
                    "Tier 4: 치명타 피해(staff_step4_crit_damage) - 스킬 습득에 필요한 포인트");

                // Tier 4: 속성 강화 스킬
                StaffFrostDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_냉기속성_공격력보너스", 3f,
                    "Tier 4: 냉기 속성(staff_Step4_reduction) - 냉기 공격 보너스 (고정값)");

                StaffFireDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_화염속성_공격력보너스", 3f,
                    "Tier 4: 화염 속성(staff_Step4_range) - 화염 공격 보너스 (고정값)");

                StaffLightningDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_번개속성_공격력보너스", 3f,
                    "Tier 4: 번개 속성(staff_Step4_surge) - 번개 공격 보너스 (고정값)");

                // === Tier 5 ===
                StaffCritRate = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_치명타확률_증가율", 15f,
                    "Tier 5: 치명타 확률(staff_step5_crit_rate) - 치명타 확률 증가 (%)");

                StaffCritRateRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_치명타확률_필요포인트", 3,
                    "Tier 5: 치명타 확률(staff_step5_crit_rate) - 스킬 습득에 필요한 포인트");

                // === Tier 6-1: 이중시전 (T키 액티브) ===
                StaffDoubleCastProjectileCount = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_이중시전_추가발사체수", 2,
                    "Tier 6: 이중시전(staff_step6_double_cast) - 추가 발사체 개수");

                StaffDoubleCastDamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_이중시전_발사체데미지", 70f,
                    "Tier 6: 이중시전(staff_step6_double_cast) - 추가 발사체 데미지 비율 (%)");

                StaffDoubleCastAngleOffset = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_이중시전_각도오프셋", 5f,
                    "Tier 6: 이중시전(staff_step6_double_cast) - 발사체 각도 편차 (도)");

                StaffDoubleCastEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_이중시전_에이트르소모", 20f,
                    "Tier 6: 이중시전(staff_step6_double_cast) - 활성화 에이트르 소모량");

                StaffDoubleCastCooldown = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_이중시전_쿨타임", 30f,
                    "Tier 6: 이중시전(staff_step6_double_cast) - 재사용 대기시간 (초)");

                StaffDoubleCastRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_이중시전_필요포인트", 3,
                    "Tier 6: 이중시전(staff_step6_double_cast) - 스킬 습득에 필요한 포인트");

                // === Tier 6-2: 힐러모드 (G키 액티브) ===
                StaffHealerModeDuration = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_지속시간", 180,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 지속 시간 (초)");

                StaffHealerModeEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_에이트르소모", 30f,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 활성화 에이트르 소모량");

                StaffHealerModeCooldown = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_범위힐쿨타임", 30f,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 범위 힐 재사용 대기시간 (초)");

                StaffHealerHealPercentage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_범위힐치료량", 20f,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 범위 힐 체력 회복 비율 (%)");

                StaffHealerHealRange = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_범위힐범위", 12f,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 범위 힐 치료 범위 (미터)");

                StaffHealerProjectileCount = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_발사체수", 2,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 힐링 발사체 수");

                StaffHealerProjectileInterval = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_발사체간격", 0.2f,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 발사체 발사 간격 (초)");

                StaffHealerRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier6_힐러모드_필요포인트", 3,
                    "Tier 6: 힐러모드(staff_step6_healer_mode) - 스킬 습득에 필요한 포인트");

                // === 힐러모드 이펙트는 하드코딩으로 처리 (동적 연동 시스템이므로 컨피그 불필요) ===
                // 이펙트 설정은 HealerMode_Config.cs와 Mage_HealerMode.cs에서 하드코딩으로 관리
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff 컨피그] 설정 초기화 실패: {ex.Message}");
            }
        }

        // === 설정값 접근자들 ===
        
        // 지팡이 전문가
        public static float StaffExpertDamageValue => StaffExpertDamage?.Value ?? 12f;
        public static int StaffExpertRequiredPointsValue => StaffExpertRequiredPoints?.Value ?? 2;

        // Step 2
        public static float StaffFocusEitrReductionValue => StaffFocusEitrReduction?.Value ?? 15f;
        public static int StaffFocusRequiredPointsValue => StaffFocusRequiredPoints?.Value ?? 2;
        public static float StaffStreamEitrBonusValue => StaffStreamCastSpeed?.Value ?? 30f;
        public static int StaffStreamRequiredPointsValue => StaffStreamRequiredPoints?.Value ?? 2;

        // Step 3
        public static float StaffAmpDamageValue => StaffAmpDamage?.Value ?? 10f;
        public static int StaffAmpRequiredPointsValue => StaffAmpRequiredPoints?.Value ?? 3;

        // Step 4
        public static float StaffMagicAmpDamageValue => StaffRangeBonus?.Value ?? 30f;
        public static int StaffMagicAmpRequiredPointsValue => StaffRangeRequiredPoints?.Value ?? 2;
        public static float StaffLuckManaChanceValue => StaffLuckManaChance?.Value ?? 35f;
        public static int StaffLuckManaRequiredPointsValue => StaffLuckManaRequiredPoints?.Value ?? 3;
        public static float StaffCritDamageValue => StaffCritDamage?.Value ?? 30f;
        public static int StaffCritDamageRequiredPointsValue => StaffCritDamageRequiredPoints?.Value ?? 3;

        // Step 4: 속성 강화
        public static float StaffFrostDamageBonusValue => StaffFrostDamageBonus?.Value ?? 3f;
        public static float StaffFireDamageBonusValue => StaffFireDamageBonus?.Value ?? 3f;
        public static float StaffLightningDamageBonusValue => StaffLightningDamageBonus?.Value ?? 3f;

        // Step 5
        public static float StaffCritRateValue => StaffCritRate?.Value ?? 15f;
        public static int StaffCritRateRequiredPointsValue => StaffCritRateRequiredPoints?.Value ?? 3;

        // Step 6-1: 이중시전
        public static int StaffDoubleCastProjectileCountValue => StaffDoubleCastProjectileCount?.Value ?? 2;
        public static float StaffDoubleCastDamagePercentValue => StaffDoubleCastDamagePercent?.Value ?? 70f;
        public static float StaffDoubleCastAngleOffsetValue => StaffDoubleCastAngleOffset?.Value ?? 5f;
        public static float StaffDoubleCastEitrCostValue => StaffDoubleCastEitrCost?.Value ?? 20f;
        public static float StaffDoubleCastCooldownValue => StaffDoubleCastCooldown?.Value ?? 30f;
        public static int StaffDoubleCastRequiredPointsValue => StaffDoubleCastRequiredPoints?.Value ?? 3;

        // Step 6-2: 힐러모드
        public static int StaffHealerModeDurationValue => StaffHealerModeDuration?.Value ?? 180;
        public static float StaffHealerModeEitrCostValue => StaffHealerModeEitrCost?.Value ?? 30f;
        public static float StaffHealerModeCooldownValue => StaffHealerModeCooldown?.Value ?? 30f;
        public static float StaffHealerHealPercentageValue => StaffHealerHealPercentage?.Value ?? 25f;
        public static float StaffHealerHealRangeValue => StaffHealerHealRange?.Value ?? 10f;
        public static int StaffHealerProjectileCountValue => StaffHealerProjectileCount?.Value ?? 2;
        public static float StaffHealerProjectileIntervalValue => StaffHealerProjectileInterval?.Value ?? 0.2f;
        public static int StaffHealerRequiredPointsValue => StaffHealerRequiredPoints?.Value ?? 3;

        // 힐러모드 이펙트 하드코딩 상수 (동적 연동 시스템)
        public static string StaffHealerBuffVFXValue => "buff_03a";
        public static string StaffHealerStatusVFXValue => "statusailment_01_aura";
        public static string StaffHealerHealingVFXValue => "vfx_HealthUpgrade";
        public static string StaffHealerActivationSoundValue => "sfx_dverger_heal_start";

        /// <summary>
        /// 지팡이 스킬 설정값 유효성 검증
        /// </summary>
        public static bool ValidateConfig()
        {
            try
            {
                bool isValid = true;

                // 기본값 범위 검증
                if (StaffHealerModeDurationValue < 60 || StaffHealerModeDurationValue > 600)
                {
                    Plugin.Log.LogWarning($"[Staff 컨피그] 힐러모드 지속시간이 범위를 벗어남: {StaffHealerModeDurationValue}초 (권장: 60-600초)");
                    isValid = false;
                }

                if (StaffHealerHealPercentageValue < 1f || StaffHealerHealPercentageValue > 100f)
                {
                    Plugin.Log.LogWarning($"[Staff 컨피그] 힐링 비율이 범위를 벗어남: {StaffHealerHealPercentageValue}% (권장: 1-100%)");
                    isValid = false;
                }

                if (StaffHealerHealRangeValue < 1f || StaffHealerHealRangeValue > 50f)
                {
                    Plugin.Log.LogWarning($"[Staff 컨피그] 힐링 범위가 범위를 벗어남: {StaffHealerHealRangeValue}m (권장: 1-50m)");
                    isValid = false;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff 컨피그] 설정값 검증 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 현재 설정값 디버그 출력
        /// </summary>
        public static void LogCurrentConfig()
        {
            try
            {
                Plugin.Log.LogInfo("[Staff 컨피그] 현재 설정값:");
                Plugin.Log.LogInfo($"  - 지팡이 전문가 공격력: +{StaffExpertDamageValue}%");
                Plugin.Log.LogInfo($"  - 힐러모드 지속시간: {StaffHealerModeDurationValue}초");
                Plugin.Log.LogInfo($"  - 힐러모드 쿨타임: {StaffHealerModeCooldownValue}초");
                Plugin.Log.LogInfo($"  - 힐링 비율: {StaffHealerHealPercentageValue}%");
                Plugin.Log.LogInfo($"  - 힐링 범위: {StaffHealerHealRangeValue}m");
                Plugin.Log.LogInfo($"  - 발사체 수: {StaffHealerProjectileCountValue}개");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff 컨피그] 설정값 로그 출력 실패: {ex.Message}");
            }
        }
    }
}
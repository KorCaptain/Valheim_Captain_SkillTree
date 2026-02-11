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

        // === Step 4 설정: 속성 강화 스킬 ===
        private static ConfigEntry<float> StaffFrostDamageBonus;
        private static ConfigEntry<int> StaffFrostRequiredPoints;
        private static ConfigEntry<float> StaffFireDamageBonus;
        private static ConfigEntry<int> StaffFireRequiredPoints;
        private static ConfigEntry<float> StaffLightningDamageBonus;
        private static ConfigEntry<int> StaffLightningRequiredPoints;

        // === Step 5 설정: 행운 마력 ===
        private static ConfigEntry<float> StaffLuckManaChance;
        private static ConfigEntry<int> StaffLuckManaRequiredPoints;

        // === Step 6-1: 이중시전 (R키 액티브) 설정 ===
        private static ConfigEntry<int> StaffDoubleCastProjectileCount;
        private static ConfigEntry<float> StaffDoubleCastDamagePercent;
        private static ConfigEntry<float> StaffDoubleCastAngleOffset;
        private static ConfigEntry<float> StaffDoubleCastEitrCost;
        private static ConfigEntry<float> StaffDoubleCastCooldown;
        private static ConfigEntry<int> StaffDoubleCastRequiredPoints;

        // === Step 6-2: 즉시 범위 힐 (H키 액티브) 설정 ===
        private static ConfigEntry<float> StaffHealCooldown;
        private static ConfigEntry<float> StaffHealEitrCost;
        private static ConfigEntry<float> StaffHealPercentage;
        private static ConfigEntry<float> StaffHealRange;
        private static ConfigEntry<bool> StaffHealSelf;
        private static ConfigEntry<int> StaffHealRequiredPoints;

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

                // === Tier 1 ===
                StaffFocusEitrReduction = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_정신집중_에이트르절약", 15f,
                    "Tier 1: 정신 집중(staff_step2_focus) - 에이트르 소모량 감소 (%)");

                StaffFocusRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_정신집중_필요포인트", 2,
                    "Tier 1: 정신 집중(staff_step2_focus) - 스킬 습득에 필요한 포인트");

                StaffStreamCastSpeed = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_마법흐름_에이트르보너스", 30f,
                    "Tier 1: 마법 흐름(staff_step2_stream) - 최대 에이트르 증가량");

                StaffStreamRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_마법흐름_필요포인트", 2,
                    "Tier 1: 마법 흐름(staff_step2_stream) - 스킬 습득에 필요한 포인트");

                // === Tier 2 ===
                StaffAmpDamage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_마법증폭_공격력", 10f,
                    "Tier 2: 마법 증폭(staff_step3_amp) - 추가 공격력 (%)");

                StaffAmpRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_마법증폭_필요포인트", 3,
                    "Tier 2: 마법 증폭(staff_step3_amp) - 스킬 습득에 필요한 포인트");

                // === Tier 3: 속성 강화 스킬 ===
                StaffFrostDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_냉기속성_공격력보너스", 3f,
                    "Tier 3: 냉기 속성(staff_Step4_reduction) - 냉기 공격 보너스 (고정값)");

                StaffFrostRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_냉기속성_필요포인트", 2,
                    "Tier 3: 냉기 속성(staff_Step4_reduction) - 스킬 습득에 필요한 포인트");

                StaffFireDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_화염속성_공격력보너스", 3f,
                    "Tier 3: 화염 속성(staff_Step4_range) - 화염 공격 보너스 (고정값)");

                StaffFireRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_화염속성_필요포인트", 2,
                    "Tier 3: 화염 속성(staff_Step4_range) - 스킬 습득에 필요한 포인트");

                StaffLightningDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_번개속성_공격력보너스", 3f,
                    "Tier 3: 번개 속성(staff_Step4_surge) - 번개 공격 보너스 (고정값)");

                StaffLightningRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_번개속성_필요포인트", 2,
                    "Tier 3: 번개 속성(staff_Step4_surge) - 스킬 습득에 필요한 포인트");

                // === Tier 4: 행운 마력 ===
                StaffLuckManaChance = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_행운마력_확률", 35f,
                    "Tier 4: 행운 마력(staff_step5_archmage) - 에이트르 무소모 확률 (%)");

                StaffLuckManaRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_행운마력_필요포인트", 3,
                    "Tier 4: 행운 마력(staff_step5_archmage) - 스킬 습득에 필요한 포인트");

                // === Tier 5-1: 이중시전 (R키 액티브) ===
                StaffDoubleCastProjectileCount = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_이중시전_추가발사체수", 2,
                    "Tier 5: 이중시전(staff_step6_double_cast) - 추가 발사체 개수");

                StaffDoubleCastDamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_이중시전_발사체데미지", 70f,
                    "Tier 5: 이중시전(staff_step6_double_cast) - 추가 발사체 데미지 비율 (%)");

                StaffDoubleCastAngleOffset = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_이중시전_각도오프셋", 5f,
                    "Tier 5: 이중시전(staff_step6_double_cast) - 발사체 각도 편차 (도)");

                StaffDoubleCastEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_이중시전_에이트르소모", 20f,
                    "Tier 5: 이중시전(staff_step6_double_cast) - 활성화 에이트르 소모량");

                StaffDoubleCastCooldown = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_이중시전_쿨타임", 30f,
                    "Tier 5: 이중시전(staff_step6_double_cast) - 재사용 대기시간 (초)");

                StaffDoubleCastRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_이중시전_필요포인트", 3,
                    "Tier 5: 이중시전(staff_step6_double_cast) - 스킬 습득에 필요한 포인트");

                // === Tier 5-2: 즉시 범위 힐 (H키 액티브) ===
                StaffHealCooldown = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_즉시범위힐_쿨타임", 30f,
                    "Tier 5: 즉시 범위 힐(staff_step6_heal) - 재사용 대기시간 (초)");

                StaffHealEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_즉시범위힐_에이트르소모", 30f,
                    "Tier 5: 즉시 범위 힐(staff_step6_heal) - 시전 시 소모되는 Eitr");

                StaffHealPercentage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_즉시범위힐_힐량", 25f,
                    "Tier 5: 즉시 범위 힐(staff_step6_heal) - 대상 최대 체력 대비 힐량 (%)");

                StaffHealRange = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_즉시범위힐_범위", 12f,
                    "Tier 5: 즉시 범위 힐(staff_step6_heal) - 시전자 중심 힐 범위 (미터)");

                StaffHealSelf = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_즉시범위힐_자기치료", false,
                    "Tier 5: 즉시 범위 힐(staff_step6_heal) - 자기 치료 허용 여부");

                StaffHealRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_즉시범위힐_필요포인트", 3,
                    "Tier 5: 즉시 범위 힐(staff_step6_heal) - 스킬 습득에 필요한 포인트");

                // === 이벤트 핸들러 등록 ===
                RegisterStaffEventHandlers();

                Plugin.Log.LogDebug("[Staff 컨피그] 설정 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff 컨피그] 설정 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 지팡이 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterStaffEventHandlers()
        {
            try
            {
                // Step 6-1: 이중시전 이벤트
                StaffDoubleCastProjectileCount.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffDoubleCastDamagePercent.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffDoubleCastAngleOffset.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffDoubleCastEitrCost.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffDoubleCastCooldown.SettingChanged += (sender, args) => OnStaffConfigChanged();

                // Step 6-2: 즉시 범위 힐 이벤트
                StaffHealCooldown.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffHealEitrCost.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffHealPercentage.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffHealRange.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffHealSelf.SettingChanged += (sender, args) => OnStaffConfigChanged();

                // 기타 스킬 이벤트
                StaffExpertDamage.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffFocusEitrReduction.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffStreamCastSpeed.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffAmpDamage.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffFrostDamageBonus.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffFireDamageBonus.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffLightningDamageBonus.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffLuckManaChance.SettingChanged += (sender, args) => OnStaffConfigChanged();

                Plugin.Log.LogDebug("[Staff 컨피그] 이벤트 핸들러 등록 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        private static void OnStaffConfigChanged()
        {
            try
            {
                Plugin.Log.LogInfo("[Staff 컨피그] 설정값 변경됨");
                LogCurrentConfig();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff 컨피그] 설정 변경 처리 실패: {ex.Message}");
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

        // Step 4: 속성 강화
        public static float StaffFrostDamageBonusValue => StaffFrostDamageBonus?.Value ?? 3f;
        public static int StaffFrostRequiredPointsValue => StaffFrostRequiredPoints?.Value ?? 2;
        public static float StaffFireDamageBonusValue => StaffFireDamageBonus?.Value ?? 3f;
        public static int StaffFireRequiredPointsValue => StaffFireRequiredPoints?.Value ?? 2;
        public static float StaffLightningDamageBonusValue => StaffLightningDamageBonus?.Value ?? 3f;
        public static int StaffLightningRequiredPointsValue => StaffLightningRequiredPoints?.Value ?? 2;

        // Step 5: 행운 마력
        public static float StaffLuckManaChanceValue => StaffLuckManaChance?.Value ?? 35f;
        public static int StaffLuckManaRequiredPointsValue => StaffLuckManaRequiredPoints?.Value ?? 3;

        // Step 6-1: 이중시전
        public static int StaffDoubleCastProjectileCountValue => StaffDoubleCastProjectileCount?.Value ?? 2;
        public static float StaffDoubleCastDamagePercentValue => StaffDoubleCastDamagePercent?.Value ?? 70f;
        public static float StaffDoubleCastAngleOffsetValue => StaffDoubleCastAngleOffset?.Value ?? 5f;
        public static float StaffDoubleCastEitrCostValue => StaffDoubleCastEitrCost?.Value ?? 20f;
        public static float StaffDoubleCastCooldownValue => StaffDoubleCastCooldown?.Value ?? 30f;
        public static int StaffDoubleCastRequiredPointsValue => StaffDoubleCastRequiredPoints?.Value ?? 3;

        // Step 6-2: 즉시 범위 힐
        public static float StaffHealCooldownValue => StaffHealCooldown?.Value ?? 30f;
        public static float StaffHealEitrCostValue => StaffHealEitrCost?.Value ?? 30f;
        public static float StaffHealPercentageValue => StaffHealPercentage?.Value ?? 25f;
        public static float StaffHealRangeValue => StaffHealRange?.Value ?? 12f;
        public static bool StaffHealSelfValue => StaffHealSelf?.Value ?? false;
        public static int StaffHealRequiredPointsValue => StaffHealRequiredPoints?.Value ?? 3;

        /// <summary>
        /// 현재 설정값 디버그 출력
        /// </summary>
        public static void LogCurrentConfig()
        {
            try
            {
                Plugin.Log.LogInfo("[Staff 컨피그] 현재 설정값:");
                Plugin.Log.LogInfo($"  - 지팡이 전문가 공격력: +{StaffExpertDamageValue}%");
                Plugin.Log.LogInfo($"  - 이중시전: 발사체 {StaffDoubleCastProjectileCountValue}개, 쿨타임 {StaffDoubleCastCooldownValue}초");
                Plugin.Log.LogInfo($"  - 즉시 범위 힐: 힐량 {StaffHealPercentageValue}%, 범위 {StaffHealRangeValue}m, 쿨타임 {StaffHealCooldownValue}초, 자기치료 {StaffHealSelfValue}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff 컨피그] 설정값 로그 출력 실패: {ex.Message}");
            }
        }
    }
}

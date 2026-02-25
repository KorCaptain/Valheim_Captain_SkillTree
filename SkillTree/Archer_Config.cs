using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 아처 직업 전용 컨피그 시스템
    /// 아처 멀티샷 스킬의 모든 설정값들을 관리
    /// </summary>
    public static class Archer_Config
    {
        // === 아처 멀티샷 컨피그 엔트리들 ===
        public static ConfigEntry<int> ArcherMultiShotArrowCount;          // 발사할 화살 수 (기본: 5)
        public static ConfigEntry<int> ArcherMultiShotArrowConsumption;    // 화살 소모량 (기본: 1)
        public static ConfigEntry<float> ArcherMultiShotDamagePercent;     // 화살당 데미지 비율 (기본: 50%)
        public static ConfigEntry<float> ArcherMultiShotCooldown;          // 쿨타임 (초)
        public static ConfigEntry<int> ArcherMultiShotCharges;             // 발사 회수 (기본 2회)
        public static ConfigEntry<float> ArcherMultiShotStaminaCost;       // 소모 스태미나 (기본 25)

        // === 아처 패시브 스킬 컨피그 엔트리들 ===
        public static ConfigEntry<float> ArcherJumpHeightBonus;            // 점프 높이 보너스 (기본: 20%)
        public static ConfigEntry<float> ArcherFallDamageReduction;        // 낙사 데미지 감소 (기본: 50%)

        // === 동적 값 접근자 (MMO 시스템 연동) ===
        public static int ArcherMultiShotArrowCountValue => (int)SkillTreeConfig.GetEffectiveValue("Archer_MultiShot_ArrowCount", ArcherMultiShotArrowCount.Value);
        public static int ArcherMultiShotArrowConsumptionValue => (int)SkillTreeConfig.GetEffectiveValue("Archer_MultiShot_ArrowConsumption", ArcherMultiShotArrowConsumption.Value);
        public static float ArcherMultiShotDamagePercentValue => SkillTreeConfig.GetEffectiveValue("Archer_MultiShot_DamagePercent", ArcherMultiShotDamagePercent.Value);
        public static float ArcherMultiShotCooldownValue => SkillTreeConfig.GetEffectiveValue("Archer_MultiShot_Cooldown", ArcherMultiShotCooldown.Value);
        public static int ArcherMultiShotChargesValue => (int)SkillTreeConfig.GetEffectiveValue("Archer_MultiShot_Charges", (float)ArcherMultiShotCharges.Value);
        public static float ArcherMultiShotStaminaCostValue => SkillTreeConfig.GetEffectiveValue("Archer_MultiShot_StaminaCost", ArcherMultiShotStaminaCost.Value);

        // === 패시브 스킬 동적 값 접근자 ===
        public static float ArcherJumpHeightBonusValue => SkillTreeConfig.GetEffectiveValue("Archer_JumpHeightBonus", ArcherJumpHeightBonus.Value);
        public static float ArcherFallDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Archer_FallDamageReduction", ArcherFallDamageReduction.Value);

        /// <summary>
        /// 아처 컨피그 초기화 (SkillTreeConfig에서 호출)
        /// </summary>
        /// <param name="config">BepInEx ConfigFile 인스턴스</param>
        public static void InitializeArcherConfig(ConfigFile config)
        {
            try
            {
                // Plugin.Log.LogDebug("[아처 컨피그] 초기화 시작"); // 제거: 과도한 로그

                // === 아처 직업 스킬 설정 (액티브 + 패시브) ===
                ArcherMultiShotArrowCount = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_MultiShot_ArrowCount",
                    5,
                    SkillTreeConfig.GetConfigDescription("Archer_MultiShot_ArrowCount")
                );

                ArcherMultiShotArrowConsumption = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_MultiShot_ArrowConsumption",
                    1,
                    SkillTreeConfig.GetConfigDescription("Archer_MultiShot_ArrowConsumption")
                );

                ArcherMultiShotDamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_MultiShot_DamagePercent",
                    50.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_MultiShot_DamagePercent")
                );

                ArcherMultiShotCooldown = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_MultiShot_Cooldown",
                    30.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_MultiShot_Cooldown")
                );

                ArcherMultiShotCharges = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_MultiShot_Charges",
                    2,
                    SkillTreeConfig.GetConfigDescription("Archer_MultiShot_Charges")
                );

                ArcherMultiShotStaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_MultiShot_StaminaCost",
                    25.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_MultiShot_StaminaCost")
                );

                // === 아처 패시브 스킬 설정 ===
                ArcherJumpHeightBonus = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_JumpHeightBonus",
                    20.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_JumpHeightBonus")
                );

                ArcherFallDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_FallDamageReduction",
                    50.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_FallDamageReduction")
                );

                Plugin.Log.LogDebug("[아처 컨피그] 설정 항목 생성 완료 (액티브 + 패시브)");
                
                // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
                RegisterArcherEventHandlers();
                
                Plugin.Log.LogDebug("[아처 컨피그] 초기화 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 아처 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterArcherEventHandlers()
        {
            try
            {
                // 액티브 스킬 이벤트 핸들러
                ArcherMultiShotArrowCount.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherMultiShotCharges.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherMultiShotDamagePercent.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherMultiShotStaminaCost.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherMultiShotArrowConsumption.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherMultiShotCooldown.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();

                // 패시브 스킬 이벤트 핸들러
                ArcherJumpHeightBonus.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherFallDamageReduction.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();

                Plugin.Log.LogDebug("[아처 컨피그] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화 (액티브 + 패시브)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 아처 설정 값들을 디버그용으로 출력
        /// </summary>
        public static void LogArcherConfigValues()
        {
            try
            {
                Plugin.Log.LogInfo($"[아처 컨피그] === 현재 설정값 ===");
                Plugin.Log.LogInfo($"[아처 컨피그] 화살 수: {ArcherMultiShotArrowCountValue}");
                Plugin.Log.LogInfo($"[아처 컨피그] 발사 회수: {ArcherMultiShotChargesValue}");
                Plugin.Log.LogInfo($"[아처 컨피그] 데미지 비율: {ArcherMultiShotDamagePercentValue}%");
                Plugin.Log.LogInfo($"[아처 컨피그] 스태미나 소모: {ArcherMultiShotStaminaCostValue}");
                Plugin.Log.LogInfo($"[아처 컨피그] 화살 소모: {ArcherMultiShotArrowConsumptionValue}");
                Plugin.Log.LogInfo($"[아처 컨피그] 쿨타임: {ArcherMultiShotCooldownValue}초");
                Plugin.Log.LogInfo($"[아처 컨피그] === 패시브 스킬 ===");
                Plugin.Log.LogInfo($"[아처 컨피그] 점프 높이 보너스: {ArcherJumpHeightBonusValue}%");
                Plugin.Log.LogInfo($"[아처 컨피그] 낙사 데미지 감소: {ArcherFallDamageReductionValue}%");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 컨피그] 값 출력 실패: {ex.Message}");
            }
        }
    }
}
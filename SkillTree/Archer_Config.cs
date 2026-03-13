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

        // === 아처 레벨별 패시브 점프/낙사 추가 컨피그 (Lv2~5) ===
        public static ConfigEntry<float> ArcherLv2JumpHeightBonus;         // Lv2 추가 점프 (기본: 10%)
        public static ConfigEntry<float> ArcherLv3JumpHeightBonus;         // Lv3 추가 점프 (기본: 20%)
        public static ConfigEntry<float> ArcherLv4JumpHeightBonus;         // Lv4 추가 점프 (기본: 20%)
        public static ConfigEntry<float> ArcherLv5JumpHeightBonus;         // Lv5 추가 점프 (기본: 20%)
        public static ConfigEntry<float> ArcherLv3FallDamageReduction;     // Lv3 추가 낙사 감소 (기본: 10%)
        public static ConfigEntry<float> ArcherLv4FallDamageReduction;     // Lv4 추가 낙사 감소 (기본: 20%)
        public static ConfigEntry<float> ArcherLv5FallDamageReduction;     // Lv5 추가 낙사 감소 (기본: 35%)
        public static ConfigEntry<float> ArcherElementalResistPerLevel;    // 레벨당 속성 저항 (기본: 10%)

        // === 아처 레벨업 스탯 변화 컨피그 (Lv2~5, 레벨 순 정렬) ===
        public static ConfigEntry<int>   ArcherLv2BonusArrows;
        public static ConfigEntry<float> ArcherLv2DamagePercent;
        public static ConfigEntry<int>   ArcherLv3BonusArrows;
        public static ConfigEntry<float> ArcherLv3DamagePercent;
        public static ConfigEntry<int>   ArcherLv4BonusArrows;
        public static ConfigEntry<float> ArcherLv4DamagePercent;
        public static ConfigEntry<int>   ArcherLv5BonusArrows;
        public static ConfigEntry<float> ArcherLv5DamagePercent;
        public static ConfigEntry<int>   ArcherLv5BonusCharges;

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

        // === 레벨별 패시브 추가분 동적 값 접근자 ===
        public static float ArcherLv2JumpHeightBonusValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv2_JumpHeightBonus", ArcherLv2JumpHeightBonus.Value);
        public static float ArcherLv3JumpHeightBonusValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv3_JumpHeightBonus", ArcherLv3JumpHeightBonus.Value);
        public static float ArcherLv4JumpHeightBonusValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv4_JumpHeightBonus", ArcherLv4JumpHeightBonus.Value);
        public static float ArcherLv5JumpHeightBonusValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv5_JumpHeightBonus", ArcherLv5JumpHeightBonus.Value);
        public static float ArcherLv3FallDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv3_FallDamageReduction", ArcherLv3FallDamageReduction.Value);
        public static float ArcherLv4FallDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv4_FallDamageReduction", ArcherLv4FallDamageReduction.Value);
        public static float ArcherLv5FallDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv5_FallDamageReduction", ArcherLv5FallDamageReduction.Value);
        public static float ArcherElementalResistPerLevelValue => SkillTreeConfig.GetEffectiveValue("Archer_ElementalResistPerLevel", ArcherElementalResistPerLevel.Value);

        // === 레벨업 스탯 변화 동적 값 접근자 (레벨 순) ===
        public static int   ArcherLv2BonusArrowsValue   => (int)SkillTreeConfig.GetEffectiveValue("Archer_Lv2_BonusArrows",   ArcherLv2BonusArrows.Value);
        public static float ArcherLv2DamagePercentValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv2_DamagePercent", ArcherLv2DamagePercent.Value);
        public static int   ArcherLv3BonusArrowsValue   => (int)SkillTreeConfig.GetEffectiveValue("Archer_Lv3_BonusArrows",   ArcherLv3BonusArrows.Value);
        public static float ArcherLv3DamagePercentValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv3_DamagePercent", ArcherLv3DamagePercent.Value);
        public static int   ArcherLv4BonusArrowsValue   => (int)SkillTreeConfig.GetEffectiveValue("Archer_Lv4_BonusArrows",   ArcherLv4BonusArrows.Value);
        public static float ArcherLv4DamagePercentValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv4_DamagePercent", ArcherLv4DamagePercent.Value);
        public static int   ArcherLv5BonusArrowsValue   => (int)SkillTreeConfig.GetEffectiveValue("Archer_Lv5_BonusArrows",   ArcherLv5BonusArrows.Value);
        public static float ArcherLv5DamagePercentValue => SkillTreeConfig.GetEffectiveValue("Archer_Lv5_DamagePercent", ArcherLv5DamagePercent.Value);
        public static int   ArcherLv5BonusChargesValue  => (int)SkillTreeConfig.GetEffectiveValue("Archer_Lv5_BonusCharges",  (float)ArcherLv5BonusCharges.Value);

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
                    0.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_JumpHeightBonus")
                );

                ArcherFallDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_FallDamageReduction",
                    0.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_FallDamageReduction")
                );

                // === 아처 레벨별 패시브 추가 설정 (Lv2~5) ===
                ArcherLv2JumpHeightBonus = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv2_JumpHeightBonus",
                    10.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv2_JumpHeightBonus")
                );

                ArcherLv3JumpHeightBonus = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv3_JumpHeightBonus",
                    20.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv3_JumpHeightBonus")
                );

                ArcherLv4JumpHeightBonus = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv4_JumpHeightBonus",
                    20.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv4_JumpHeightBonus")
                );

                ArcherLv5JumpHeightBonus = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv5_JumpHeightBonus",
                    20.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv5_JumpHeightBonus")
                );

                ArcherLv3FallDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv3_FallDamageReduction",
                    10.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv3_FallDamageReduction")
                );

                ArcherLv4FallDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv4_FallDamageReduction",
                    20.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv4_FallDamageReduction")
                );

                ArcherLv5FallDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv5_FallDamageReduction",
                    35.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv5_FallDamageReduction")
                );

                ArcherElementalResistPerLevel = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_ElementalResistPerLevel",
                    10.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_ElementalResistPerLevel")
                );

                // === 아처 레벨업 스탯 변화 설정 (Lv2~5) ===
                ArcherLv2BonusArrows = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv2_BonusArrows",
                    1,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv2_BonusArrows")
                );

                ArcherLv2DamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv2_DamagePercent",
                    55.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv2_DamagePercent")
                );

                ArcherLv3BonusArrows = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv3_BonusArrows",
                    2,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv3_BonusArrows")
                );

                ArcherLv3DamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv3_DamagePercent",
                    60.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv3_DamagePercent")
                );

                ArcherLv4BonusArrows = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv4_BonusArrows",
                    3,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv4_BonusArrows")
                );

                ArcherLv4DamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv4_DamagePercent",
                    65.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv4_DamagePercent")
                );

                ArcherLv5BonusArrows = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv5_BonusArrows",
                    3,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv5_BonusArrows")
                );

                ArcherLv5DamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv5_DamagePercent",
                    65.0f,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv5_DamagePercent")
                );

                ArcherLv5BonusCharges = SkillTreeConfig.BindServerSync(config,
                    "Archer Job Skills",
                    "Archer_Lv5_BonusCharges",
                    1,
                    SkillTreeConfig.GetConfigDescription("Archer_Lv5_BonusCharges")
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

                // 레벨별 패시브 추가분 이벤트 핸들러
                ArcherLv2JumpHeightBonus.SettingChanged   += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv3JumpHeightBonus.SettingChanged   += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv4JumpHeightBonus.SettingChanged   += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv5JumpHeightBonus.SettingChanged   += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv3FallDamageReduction.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv4FallDamageReduction.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv5FallDamageReduction.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherElementalResistPerLevel.SettingChanged += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();

                // 레벨업 스탯 변화 이벤트 핸들러
                ArcherLv2BonusArrows.SettingChanged    += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv2DamagePercent.SettingChanged  += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv3BonusArrows.SettingChanged    += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv3DamagePercent.SettingChanged  += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv4BonusArrows.SettingChanged    += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv4DamagePercent.SettingChanged  += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv5BonusArrows.SettingChanged    += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv5DamagePercent.SettingChanged  += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();
                ArcherLv5BonusCharges.SettingChanged   += (sender, args) => Archer_Tooltip.UpdateArcherTooltip();

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
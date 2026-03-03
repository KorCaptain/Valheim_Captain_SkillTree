using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 직업 전용 컨피그 시스템
    /// 메이지 액티브 스킬과 패시브 스킬의 모든 설정값들을 관리
    /// </summary>
    public static class Mage_Config
    {
        // === 메이지 액티브 스킬 컨피그 엔트리들 ===
        public static ConfigEntry<float> MageAOERange;                   // 범위 (기본: 12m)
        public static ConfigEntry<int> MageEitrCost;                     // Eitr 소모량 (기본: 35)
        public static ConfigEntry<float> MageDamageMultiplier;           // 공격 데미지 배수 (기본: 150%)
        public static ConfigEntry<float> MageCooldown;                   // 쿨타임 (기본: 90초)
        public static ConfigEntry<string> MageVFXName;                   // VFX 이름 (기본: "vfx_HealthUpgrade")

        // === 메이지 패시브 스킬 컨피그 엔트리들 ===
        public static ConfigEntry<float> MageElementalResistance;        // 마법 속성 저항 (기본: 15%) - 화염/냉기/번개/독/영혼

        // === 동적 값 접근자 (MMO 시스템 연동) ===
        public static float MageAOERangeValue => SkillTreeConfig.GetEffectiveValue("Mage_AOE_Range", MageAOERange.Value);
        public static int MageEitrCostValue => (int)SkillTreeConfig.GetEffectiveValue("Mage_Eitr_Cost", (float)MageEitrCost.Value);
        public static float MageDamageMultiplierValue => SkillTreeConfig.GetEffectiveValue("Mage_Damage_Multiplier", MageDamageMultiplier.Value);
        public static float MageCooldownValue => SkillTreeConfig.GetEffectiveValue("Mage_Cooldown", MageCooldown.Value);
        public static string MageVFXNameValue => MageVFXName.Value;

        // === 패시브 스킬 동적 값 접근자 ===
        public static float MageElementalResistanceValue => SkillTreeConfig.GetEffectiveValue("Mage_Elemental_Resistance", MageElementalResistance.Value);

        /// <summary>
        /// 메이지 컨피그 초기화 (SkillTreeConfig에서 호출)
        /// </summary>
        /// <param name="config">BepInEx ConfigFile 인스턴스</param>
        public static void InitializeMageConfig(ConfigFile config)
        {
            try
            {
                Plugin.Log.LogDebug("[메이지 컨피그] 초기화 시작");

                // === 메이지 직업 스킬 설정 (액티브 + 패시브) ===
                MageAOERange = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills",
                    "Mage_AOE_Range",
                    12.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_AOE_Range")
                );

                MageEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills",
                    "Mage_Eitr_Cost",
                    35,
                    SkillTreeConfig.GetConfigDescription("Mage_Eitr_Cost")
                );

                MageDamageMultiplier = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills",
                    "Mage_Damage_Multiplier",
                    150.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Damage_Multiplier")
                );

                MageCooldown = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills",
                    "Mage_Cooldown",
                    90.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Cooldown")
                );

                MageVFXName = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills",
                    "Mage_VFX_Name",
                    "",
                    SkillTreeConfig.GetConfigDescription("Mage_VFX_Name")
                );

                // === 메이지 패시브 스킬 설정 ===
                MageElementalResistance = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills",
                    "Mage_Elemental_Resistance",
                    15.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Elemental_Resistance")
                );

                Plugin.Log.LogDebug("[메이지 컨피그] 설정 항목 생성 완료 (액티브 + 패시브)");
                
                // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
                RegisterMageEventHandlers();
                
                Plugin.Log.LogDebug("[메이지 컨피그] 초기화 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 메이지 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterMageEventHandlers()
        {
            try
            {
                // 액티브 스킬 이벤트 핸들러
                MageAOERange.SettingChanged += (sender, args) => Mage_Tooltip.UpdateMageTooltip();
                MageEitrCost.SettingChanged += (sender, args) => Mage_Tooltip.UpdateMageTooltip();
                MageDamageMultiplier.SettingChanged += (sender, args) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown.SettingChanged += (sender, args) => Mage_Tooltip.UpdateMageTooltip();
                MageVFXName.SettingChanged += (sender, args) => Mage_Tooltip.UpdateMageTooltip();

                // 패시브 스킬 이벤트 핸들러
                MageElementalResistance.SettingChanged += (sender, args) => Mage_Tooltip.UpdateMageTooltip();

                Plugin.Log.LogDebug("[메이지 컨피그] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화 (액티브 + 패시브)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 메이지 설정 값들을 디버그용으로 출력
        /// </summary>
        public static void LogMageConfigValues()
        {
            try
            {
                Plugin.Log.LogInfo($"[메이지 컨피그] === 현재 설정값 ===");
                Plugin.Log.LogInfo($"[메이지 컨피그] === 액티브 스킬 ===");
                Plugin.Log.LogInfo($"[메이지 컨피그] 범위: {MageAOERangeValue}m");
                Plugin.Log.LogInfo($"[메이지 컨피그] Eitr 소모: {MageEitrCostValue}");
                Plugin.Log.LogInfo($"[메이지 컨피그] 데미지 배수: {MageDamageMultiplierValue}%");
                Plugin.Log.LogInfo($"[메이지 컨피그] 쿨타임: {MageCooldownValue}초");
                Plugin.Log.LogInfo($"[메이지 컨피그] VFX: {MageVFXNameValue}");
                Plugin.Log.LogInfo($"[메이지 컨피그] === 패시브 스킬 ===");
                Plugin.Log.LogInfo($"[메이지 컨피그] 속성 저항: {MageElementalResistanceValue}%");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 컨피그] 값 출력 실패: {ex.Message}");
            }
        }
    }
}
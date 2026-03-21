using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 직업 전용 컨피그 시스템
    /// 메이지 액티브 스킬과 패시브 스킬의 모든 설정값들을 관리
    /// </summary>
    public static class Mage_Config
    {
        // === 메이지 액티브 스킬 고정 컨피그 ===
        public static ConfigEntry<float> MageAOERange;                   // 범위 (기본: 12m)
        public static ConfigEntry<int>   MageEitrCost;                   // Eitr 소모량 (기본: 35)

        // === 메이지 쿨타임 (레벨별) ===
        public static ConfigEntry<float> MageCooldown_Lv1;   // Lv1: 120s
        public static ConfigEntry<float> MageCooldown_Lv2;   // Lv2: 110s
        public static ConfigEntry<float> MageCooldown_Lv3;   // Lv3: 100s
        public static ConfigEntry<float> MageCooldown_Lv4;   // Lv4: 90s
        public static ConfigEntry<float> MageCooldown_Lv5;   // Lv5: 80s

        // === 메이지 최대 타겟 수 (레벨별) ===
        public static ConfigEntry<int> MageAOEMaxTargets_Lv1;   // Lv1: 6
        public static ConfigEntry<int> MageAOEMaxTargets_Lv2;   // Lv2: 7
        public static ConfigEntry<int> MageAOEMaxTargets_Lv3;   // Lv3: 8
        public static ConfigEntry<int> MageAOEMaxTargets_Lv4;   // Lv4: 9
        public static ConfigEntry<int> MageAOEMaxTargets_Lv5;   // Lv5: 10

        // === 메이지 패시브 속성 저항 (레벨별) ===
        public static ConfigEntry<float> MageElementalResistance_Lv1;   // Lv1: 5%
        public static ConfigEntry<float> MageElementalResistance_Lv2;   // Lv2: 7%
        public static ConfigEntry<float> MageElementalResistance_Lv3;   // Lv3: 9%
        public static ConfigEntry<float> MageElementalResistance_Lv4;   // Lv4: 12%
        public static ConfigEntry<float> MageElementalResistance_Lv5;   // Lv5: 15%

        // === 메이지 AOE 데미지 배수 (레벨별) ===
        public static ConfigEntry<float> MageDamageMultiplier_Lv1;      // Lv1: 70%
        public static ConfigEntry<float> MageDamageMultiplier_Lv2;      // Lv2: 90%
        public static ConfigEntry<float> MageDamageMultiplier_Lv3;      // Lv3: 110%
        public static ConfigEntry<float> MageDamageMultiplier_Lv4;      // Lv4: 130%
        public static ConfigEntry<float> MageDamageMultiplier_Lv5;      // Lv5: 150%

        // === 동적 값 접근자 ===
        public static float MageAOERangeValue => SkillTreeConfig.GetEffectiveValue("Mage_AOE_Range", MageAOERange.Value);
        public static int   MageEitrCostValue => (int)SkillTreeConfig.GetEffectiveValue("Mage_Eitr_Cost", (float)MageEitrCost.Value);

        // === 호환성 접근자 (기존 코드 호환 - Lv1 기본값 반환) ===
        public static float MageCooldownValue       => GetCooldown(1);
        public static int   MageAOEMaxTargetsValue  => GetMaxTargets(1);
        public static float MageDamageMultiplierValue => GetDamageMultiplier(1);
        public static float MageElementalResistanceValue => GetElementalResistance(1);

        // === 레벨별 헬퍼 메서드 ===
        public static float GetCooldown(int level)
        {
            switch (level)
            {
                case 1: return SkillTreeConfig.GetEffectiveValue("Mage_Lv1_Cooldown", MageCooldown_Lv1?.Value ?? 120f);
                case 2: return SkillTreeConfig.GetEffectiveValue("Mage_Lv2_Cooldown", MageCooldown_Lv2?.Value ?? 110f);
                case 3: return SkillTreeConfig.GetEffectiveValue("Mage_Lv3_Cooldown", MageCooldown_Lv3?.Value ?? 100f);
                case 4: return SkillTreeConfig.GetEffectiveValue("Mage_Lv4_Cooldown", MageCooldown_Lv4?.Value ?? 90f);
                case 5: return SkillTreeConfig.GetEffectiveValue("Mage_Lv5_Cooldown", MageCooldown_Lv5?.Value ?? 80f);
                default: return 120f;
            }
        }

        public static int GetMaxTargets(int level)
        {
            switch (level)
            {
                case 1: return (int)SkillTreeConfig.GetEffectiveValue("Mage_Lv1_AOE_Max_Targets", (float)(MageAOEMaxTargets_Lv1?.Value ?? 6));
                case 2: return (int)SkillTreeConfig.GetEffectiveValue("Mage_Lv2_AOE_Max_Targets", (float)(MageAOEMaxTargets_Lv2?.Value ?? 7));
                case 3: return (int)SkillTreeConfig.GetEffectiveValue("Mage_Lv3_AOE_Max_Targets", (float)(MageAOEMaxTargets_Lv3?.Value ?? 8));
                case 4: return (int)SkillTreeConfig.GetEffectiveValue("Mage_Lv4_AOE_Max_Targets", (float)(MageAOEMaxTargets_Lv4?.Value ?? 9));
                case 5: return (int)SkillTreeConfig.GetEffectiveValue("Mage_Lv5_AOE_Max_Targets", (float)(MageAOEMaxTargets_Lv5?.Value ?? 10));
                default: return 6;
            }
        }

        public static float GetElementalResistance(int level)
        {
            switch (level)
            {
                case 1: return SkillTreeConfig.GetEffectiveValue("Mage_Lv1_Elemental_Resistance", MageElementalResistance_Lv1?.Value ?? 5f);
                case 2: return SkillTreeConfig.GetEffectiveValue("Mage_Lv2_Elemental_Resistance", MageElementalResistance_Lv2?.Value ?? 7f);
                case 3: return SkillTreeConfig.GetEffectiveValue("Mage_Lv3_Elemental_Resistance", MageElementalResistance_Lv3?.Value ?? 9f);
                case 4: return SkillTreeConfig.GetEffectiveValue("Mage_Lv4_Elemental_Resistance", MageElementalResistance_Lv4?.Value ?? 12f);
                case 5: return SkillTreeConfig.GetEffectiveValue("Mage_Lv5_Elemental_Resistance", MageElementalResistance_Lv5?.Value ?? 15f);
                default: return 5f;
            }
        }

        public static float GetDamageMultiplier(int level)
        {
            switch (level)
            {
                case 1: return SkillTreeConfig.GetEffectiveValue("Mage_Lv1_Damage_Multiplier", MageDamageMultiplier_Lv1?.Value ?? 70f);
                case 2: return SkillTreeConfig.GetEffectiveValue("Mage_Lv2_Damage_Multiplier", MageDamageMultiplier_Lv2?.Value ?? 90f);
                case 3: return SkillTreeConfig.GetEffectiveValue("Mage_Lv3_Damage_Multiplier", MageDamageMultiplier_Lv3?.Value ?? 110f);
                case 4: return SkillTreeConfig.GetEffectiveValue("Mage_Lv4_Damage_Multiplier", MageDamageMultiplier_Lv4?.Value ?? 130f);
                case 5: return SkillTreeConfig.GetEffectiveValue("Mage_Lv5_Damage_Multiplier", MageDamageMultiplier_Lv5?.Value ?? 150f);
                default: return 70f;
            }
        }

        /// <summary>
        /// 메이지 컨피그 초기화 (SkillTreeConfig에서 호출)
        /// </summary>
        public static void InitializeMageConfig(ConfigFile config)
        {
            try
            {
                Plugin.Log.LogDebug("[메이지 컨피그] 초기화 시작");

                // === 고정값 ===
                MageAOERange = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_AOE_Range", 12.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_AOE_Range"));

                MageEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Eitr_Cost", 35,
                    SkillTreeConfig.GetConfigDescription("Mage_Eitr_Cost"));

                // === Lv1 ===
                MageCooldown_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv1_Cooldown", 120.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv1_Cooldown"));
                MageAOEMaxTargets_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv1_AOE_Max_Targets", 6,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv1_AOE_Max_Targets"));
                MageElementalResistance_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv1_Elemental_Resistance", 5.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv1_Elemental_Resistance"));
                MageDamageMultiplier_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv1_Damage_Multiplier", 70.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv1_Damage_Multiplier"));

                // === Lv2 ===
                MageCooldown_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv2_Cooldown", 110.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv2_Cooldown"));
                MageAOEMaxTargets_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv2_AOE_Max_Targets", 7,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv2_AOE_Max_Targets"));
                MageElementalResistance_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv2_Elemental_Resistance", 7.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv2_Elemental_Resistance"));
                MageDamageMultiplier_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv2_Damage_Multiplier", 90.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv2_Damage_Multiplier"));

                // === Lv3 ===
                MageCooldown_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv3_Cooldown", 100.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv3_Cooldown"));
                MageAOEMaxTargets_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv3_AOE_Max_Targets", 8,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv3_AOE_Max_Targets"));
                MageElementalResistance_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv3_Elemental_Resistance", 9.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv3_Elemental_Resistance"));
                MageDamageMultiplier_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv3_Damage_Multiplier", 110.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv3_Damage_Multiplier"));

                // === Lv4 ===
                MageCooldown_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv4_Cooldown", 90.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv4_Cooldown"));
                MageAOEMaxTargets_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv4_AOE_Max_Targets", 9,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv4_AOE_Max_Targets"));
                MageElementalResistance_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv4_Elemental_Resistance", 12.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv4_Elemental_Resistance"));
                MageDamageMultiplier_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv4_Damage_Multiplier", 130.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv4_Damage_Multiplier"));

                // === Lv5 ===
                MageCooldown_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv5_Cooldown", 80.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv5_Cooldown"));
                MageAOEMaxTargets_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv5_AOE_Max_Targets", 10,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv5_AOE_Max_Targets"));
                MageElementalResistance_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv5_Elemental_Resistance", 15.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv5_Elemental_Resistance"));
                MageDamageMultiplier_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv5_Damage_Multiplier", 150.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv5_Damage_Multiplier"));

                Plugin.Log.LogDebug("[메이지 컨피그] 설정 항목 생성 완료");

                RegisterMageEventHandlers();

                Plugin.Log.LogDebug("[메이지 컨피그] 초기화 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        private static void RegisterMageEventHandlers()
        {
            try
            {
                MageAOERange.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageEitrCost.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();

                MageCooldown_Lv1.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv2.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv3.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv4.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv5.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();

                MageAOEMaxTargets_Lv1.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageAOEMaxTargets_Lv2.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageAOEMaxTargets_Lv3.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageAOEMaxTargets_Lv4.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageAOEMaxTargets_Lv5.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();

                MageElementalResistance_Lv1.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageElementalResistance_Lv2.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageElementalResistance_Lv3.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageElementalResistance_Lv4.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageElementalResistance_Lv5.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();

                MageDamageMultiplier_Lv1.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageDamageMultiplier_Lv2.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageDamageMultiplier_Lv3.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageDamageMultiplier_Lv4.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageDamageMultiplier_Lv5.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();

                Plugin.Log.LogDebug("[메이지 컨피그] 이벤트 핸들러 등록 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        public static void LogMageConfigValues()
        {
            try
            {
                Plugin.Log.LogInfo($"[메이지 컨피그] === 현재 설정값 ===");
                Plugin.Log.LogInfo($"[메이지 컨피그] 범위: {MageAOERangeValue}m / Eitr 소모: {MageEitrCostValue}");
                Plugin.Log.LogInfo($"[메이지 컨피그] 쿨타임: Lv1={GetCooldown(1)}s / Lv2={GetCooldown(2)}s / Lv3={GetCooldown(3)}s / Lv4={GetCooldown(4)}s / Lv5={GetCooldown(5)}s");
                Plugin.Log.LogInfo($"[메이지 컨피그] 최대 타겟: Lv1={GetMaxTargets(1)} / Lv2={GetMaxTargets(2)} / Lv3={GetMaxTargets(3)} / Lv4={GetMaxTargets(4)} / Lv5={GetMaxTargets(5)}");
                Plugin.Log.LogInfo($"[메이지 컨피그] 속성 저항: Lv1={GetElementalResistance(1)}% / Lv2={GetElementalResistance(2)}% / Lv3={GetElementalResistance(3)}% / Lv4={GetElementalResistance(4)}% / Lv5={GetElementalResistance(5)}%");
                Plugin.Log.LogInfo($"[메이지 컨피그] AOE 데미지: Lv1={GetDamageMultiplier(1)}% / Lv2={GetDamageMultiplier(2)}% / Lv3={GetDamageMultiplier(3)}% / Lv4={GetDamageMultiplier(4)}% / Lv5={GetDamageMultiplier(5)}%");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 컨피그] 값 출력 실패: {ex.Message}");
            }
        }
    }
}

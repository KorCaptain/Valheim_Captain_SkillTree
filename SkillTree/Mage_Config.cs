using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 직업 전용 컨피그 시스템
    /// 불의 비 (Fire Rain) 액티브 스킬 및 패시브 스킬의 설정값 관리
    /// </summary>
    public static class Mage_Config
    {
        // === 메이지 액티브 스킬 고정 컨피그 ===
        public static ConfigEntry<float> MageAOERange;                     // 타겟팅 범위 (기본: 12m)
        public static ConfigEntry<int>   MageEitrCost;                     // Eitr 소모량 (기본: 35)
        public static ConfigEntry<float> MageFireRainRadius;               // 낙하 반경 (기본: 8m)
        public static ConfigEntry<float> MageFireRainImpactRadius;         // 착지 데미지 범위 (기본: 3m)
        public static ConfigEntry<int>   MageFireRainProjectileCount;     // 버스트당 발사체 수 (기본: 20개)

        // === 메이지 쿨타임 (레벨별 - 모두 45초) ===
        public static ConfigEntry<float> MageCooldown_Lv1;
        public static ConfigEntry<float> MageCooldown_Lv2;
        public static ConfigEntry<float> MageCooldown_Lv3;
        public static ConfigEntry<float> MageCooldown_Lv4;
        public static ConfigEntry<float> MageCooldown_Lv5;

        // === 메이지 패시브 속성 저항 (레벨별, 기존 유지) ===
        public static ConfigEntry<float> MageElementalResistance_Lv1;   // Lv1: 5%
        public static ConfigEntry<float> MageElementalResistance_Lv2;   // Lv2: 7%
        public static ConfigEntry<float> MageElementalResistance_Lv3;   // Lv3: 9%
        public static ConfigEntry<float> MageElementalResistance_Lv4;   // Lv4: 12%
        public static ConfigEntry<float> MageElementalResistance_Lv5;   // Lv5: 15%

        // === 메이지 공격력 배수 (레벨별 - 무기 공격력 %) ===
        public static ConfigEntry<float> MageDamageMultiplier_Lv1;      // Lv1: 22%
        public static ConfigEntry<float> MageDamageMultiplier_Lv2;      // Lv2: 24%
        public static ConfigEntry<float> MageDamageMultiplier_Lv3;      // Lv3: 26%
        public static ConfigEntry<float> MageDamageMultiplier_Lv4;      // Lv4: 28%
        public static ConfigEntry<float> MageDamageMultiplier_Lv5;      // Lv5: 30%

        // === 동적 값 접근자 ===
        public static float MageAOERangeValue => SkillTreeConfig.GetEffectiveValue("Mage_AOE_Range", MageAOERange.Value);
        public static int   MageEitrCostValue => (int)SkillTreeConfig.GetEffectiveValue("Mage_Eitr_Cost", (float)MageEitrCost.Value);
        public static float MageFireRainRadiusValue => SkillTreeConfig.GetEffectiveValue("Mage_Fire_Rain_Radius", MageFireRainRadius.Value);
        public static float MageFireRainImpactRadiusValue => SkillTreeConfig.GetEffectiveValue("Mage_Fire_Rain_Impact_Radius", MageFireRainImpactRadius.Value);
        public static int   MageFireRainProjectileCountValue => (int)SkillTreeConfig.GetEffectiveValue("Mage_Fire_Rain_Projectile_Count", (float)(MageFireRainProjectileCount?.Value ?? 20));

        // === 호환성 접근자 (기존 코드 호환) ===
        public static float MageCooldownValue            => GetCooldown(1);
        public static float MageDamageMultiplierValue    => GetDamageMultiplier(1);
        public static float MageElementalResistanceValue => GetElementalResistance(1);

        // === 레벨별 헬퍼 메서드 ===
        public static float GetCooldown(int level)
        {
            switch (level)
            {
                case 1: return SkillTreeConfig.GetEffectiveValue("Mage_Lv1_Cooldown", MageCooldown_Lv1?.Value ?? 45f);
                case 2: return SkillTreeConfig.GetEffectiveValue("Mage_Lv2_Cooldown", MageCooldown_Lv2?.Value ?? 45f);
                case 3: return SkillTreeConfig.GetEffectiveValue("Mage_Lv3_Cooldown", MageCooldown_Lv3?.Value ?? 45f);
                case 4: return SkillTreeConfig.GetEffectiveValue("Mage_Lv4_Cooldown", MageCooldown_Lv4?.Value ?? 45f);
                case 5: return SkillTreeConfig.GetEffectiveValue("Mage_Lv5_Cooldown", MageCooldown_Lv5?.Value ?? 45f);
                default: return 45f;
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
                case 1: return SkillTreeConfig.GetEffectiveValue("Mage_Lv1_Damage_Multiplier", MageDamageMultiplier_Lv1?.Value ?? 22f);
                case 2: return SkillTreeConfig.GetEffectiveValue("Mage_Lv2_Damage_Multiplier", MageDamageMultiplier_Lv2?.Value ?? 24f);
                case 3: return SkillTreeConfig.GetEffectiveValue("Mage_Lv3_Damage_Multiplier", MageDamageMultiplier_Lv3?.Value ?? 26f);
                case 4: return SkillTreeConfig.GetEffectiveValue("Mage_Lv4_Damage_Multiplier", MageDamageMultiplier_Lv4?.Value ?? 28f);
                case 5: return SkillTreeConfig.GetEffectiveValue("Mage_Lv5_Damage_Multiplier", MageDamageMultiplier_Lv5?.Value ?? 30f);
                default: return 22f;
            }
        }

        /// <summary>메이지 컨피그 초기화 (SkillTreeConfig에서 호출)</summary>
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

                MageFireRainRadius = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Fire_Rain_Radius", 8.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Fire_Rain_Radius"));

                MageFireRainImpactRadius = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Fire_Rain_Impact_Radius", 3.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Fire_Rain_Impact_Radius"));

                MageFireRainProjectileCount = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Fire_Rain_Projectile_Count", 20,
                    SkillTreeConfig.GetConfigDescription("Mage_Fire_Rain_Projectile_Count"));

                // === 레벨별 그룹 (AOE 데미지 배수 → 쿨타임 → 속성 저항) ===
                // --- Lv1 ---
                MageDamageMultiplier_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv1_Damage_Multiplier", 22.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv1_Damage_Multiplier"));
                MageCooldown_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv1_Cooldown", 45.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv1_Cooldown"));
                MageElementalResistance_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv1_Elemental_Resistance", 5.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv1_Elemental_Resistance"));

                // --- Lv2 ---
                MageDamageMultiplier_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv2_Damage_Multiplier", 24.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv2_Damage_Multiplier"));
                MageCooldown_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv2_Cooldown", 45.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv2_Cooldown"));
                MageElementalResistance_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv2_Elemental_Resistance", 7.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv2_Elemental_Resistance"));

                // --- Lv3 ---
                MageDamageMultiplier_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv3_Damage_Multiplier", 26.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv3_Damage_Multiplier"));
                MageCooldown_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv3_Cooldown", 45.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv3_Cooldown"));
                MageElementalResistance_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv3_Elemental_Resistance", 9.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv3_Elemental_Resistance"));

                // --- Lv4 ---
                MageDamageMultiplier_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv4_Damage_Multiplier", 28.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv4_Damage_Multiplier"));
                MageCooldown_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv4_Cooldown", 45.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv4_Cooldown"));
                MageElementalResistance_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv4_Elemental_Resistance", 12.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv4_Elemental_Resistance"));

                // --- Lv5 ---
                MageDamageMultiplier_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv5_Damage_Multiplier", 30.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv5_Damage_Multiplier"));
                MageCooldown_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv5_Cooldown", 45.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv5_Cooldown"));
                MageElementalResistance_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Mage Job Skills", "Mage_Lv5_Elemental_Resistance", 15.0f,
                    SkillTreeConfig.GetConfigDescription("Mage_Lv5_Elemental_Resistance"));

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
                MageAOERange.SettingChanged              += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageEitrCost.SettingChanged              += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageFireRainRadius.SettingChanged        += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageFireRainImpactRadius.SettingChanged  += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageFireRainProjectileCount.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();

                MageCooldown_Lv1.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv2.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv3.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv4.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();
                MageCooldown_Lv5.SettingChanged += (s, a) => Mage_Tooltip.UpdateMageTooltip();

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
                Plugin.Log.LogInfo($"[메이지 컨피그] 타겟팅 범위: {MageAOERangeValue}m / Eitr 소모: {MageEitrCostValue}");
                Plugin.Log.LogInfo($"[메이지 컨피그] 낙하 반경: {MageFireRainRadiusValue}m / 착지 범위: {MageFireRainImpactRadiusValue}m");
                Plugin.Log.LogInfo($"[메이지 컨피그] 쿨타임: {GetCooldown(1)}s (모든 레벨 동일)");
                Plugin.Log.LogInfo($"[메이지 컨피그] 속성 저항: Lv1={GetElementalResistance(1)}% / Lv2={GetElementalResistance(2)}% / Lv3={GetElementalResistance(3)}% / Lv4={GetElementalResistance(4)}% / Lv5={GetElementalResistance(5)}%");
                Plugin.Log.LogInfo($"[메이지 컨피그] 공격력 배수: Lv1={GetDamageMultiplier(1)}% / Lv2={GetDamageMultiplier(2)}% / Lv3={GetDamageMultiplier(3)}% / Lv4={GetDamageMultiplier(4)}% / Lv5={GetDamageMultiplier(5)}%");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 컨피그] 값 출력 실패: {ex.Message}");
            }
        }
    }
}

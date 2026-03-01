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
        private static ConfigEntry<float> StaffAmpChance;
        private static ConfigEntry<float> StaffAmpDamage;
        private static ConfigEntry<float> StaffAmpEitrCostIncrease;
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
                    "Staff Tree", "Tier0_StaffExpert_ElementalDamageBonus", 12f,
                    SkillTreeConfig.GetConfigDescription("Tier0_StaffExpert_ElementalDamageBonus"));

                StaffExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier0_StaffExpert_RequiredPoints", 2,
                    SkillTreeConfig.GetConfigDescription("Tier0_StaffExpert_RequiredPoints"));

                // === Tier 1 ===
                StaffFocusEitrReduction = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_MindFocus_EitrReduction", 15f,
                    SkillTreeConfig.GetConfigDescription("Tier1_MindFocus_EitrReduction"));

                StaffFocusRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_MindFocus_RequiredPoints", 2,
                    SkillTreeConfig.GetConfigDescription("Tier1_MindFocus_RequiredPoints"));

                StaffStreamCastSpeed = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_MagicFlow_EitrBonus", 30f,
                    SkillTreeConfig.GetConfigDescription("Tier1_MagicFlow_EitrBonus"));

                StaffStreamRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier1_MagicFlow_RequiredPoints", 2,
                    SkillTreeConfig.GetConfigDescription("Tier1_MagicFlow_RequiredPoints"));

                // === Tier 2 ===
                StaffAmpChance = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_MagicAmplify_Chance", 38f,
                    SkillTreeConfig.GetConfigDescription("Tier2_MagicAmplify_Chance"));

                StaffAmpDamage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_MagicAmplify_DamageBonus", 35f,
                    SkillTreeConfig.GetConfigDescription("Tier2_MagicAmplify_DamageBonus"));

                StaffAmpEitrCostIncrease = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_MagicAmplify_EitrCostIncrease", 20f,
                    SkillTreeConfig.GetConfigDescription("Tier2_MagicAmplify_EitrCostIncrease"));

                StaffAmpRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier2_MagicAmplify_RequiredPoints", 3,
                    SkillTreeConfig.GetConfigDescription("Tier2_MagicAmplify_RequiredPoints"));

                // === Tier 3: 속성 강화 스킬 ===
                StaffFrostDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_FrostElement_DamageBonus", 3f,
                    SkillTreeConfig.GetConfigDescription("Tier3_FrostElement_DamageBonus"));

                StaffFrostRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_FrostElement_RequiredPoints", 2,
                    SkillTreeConfig.GetConfigDescription("Tier3_FrostElement_RequiredPoints"));

                StaffFireDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_FireElement_DamageBonus", 3f,
                    SkillTreeConfig.GetConfigDescription("Tier3_FireElement_DamageBonus"));

                StaffFireRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_FireElement_RequiredPoints", 2,
                    SkillTreeConfig.GetConfigDescription("Tier3_FireElement_RequiredPoints"));

                StaffLightningDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_LightningElement_DamageBonus", 3f,
                    SkillTreeConfig.GetConfigDescription("Tier3_LightningElement_DamageBonus"));

                StaffLightningRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier3_LightningElement_RequiredPoints", 2,
                    SkillTreeConfig.GetConfigDescription("Tier3_LightningElement_RequiredPoints"));

                // === Tier 4: 행운 마력 ===
                StaffLuckManaChance = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_LuckyMana_Chance", 35f,
                    SkillTreeConfig.GetConfigDescription("Tier4_LuckyMana_Chance"));

                StaffLuckManaRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier4_LuckyMana_RequiredPoints", 3,
                    SkillTreeConfig.GetConfigDescription("Tier4_LuckyMana_RequiredPoints"));

                // === Tier 5-1: 이중시전 (R키 액티브) ===
                StaffDoubleCastProjectileCount = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_DoubleCast_AdditionalProjectileCount", 2,
                    SkillTreeConfig.GetConfigDescription("Tier5_DoubleCast_AdditionalProjectileCount"));

                StaffDoubleCastDamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_DoubleCast_ProjectileDamagePercent", 70f,
                    SkillTreeConfig.GetConfigDescription("Tier5_DoubleCast_ProjectileDamagePercent"));

                StaffDoubleCastAngleOffset = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_DoubleCast_AngleOffset", 5f,
                    SkillTreeConfig.GetConfigDescription("Tier5_DoubleCast_AngleOffset"));

                StaffDoubleCastEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_DoubleCast_EitrCost", 20f,
                    SkillTreeConfig.GetConfigDescription("Tier5_DoubleCast_EitrCost"));

                StaffDoubleCastCooldown = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_DoubleCast_Cooldown", 30f,
                    SkillTreeConfig.GetConfigDescription("Tier5_DoubleCast_Cooldown"));

                StaffDoubleCastRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_DoubleCast_RequiredPoints", 3,
                    SkillTreeConfig.GetConfigDescription("Tier5_DoubleCast_RequiredPoints"));

                // === Tier 5-2: 즉시 범위 힐 (H키 액티브) ===
                StaffHealCooldown = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_InstantAreaHeal_Cooldown", 30f,
                    SkillTreeConfig.GetConfigDescription("Tier5_InstantAreaHeal_Cooldown"));

                StaffHealEitrCost = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_InstantAreaHeal_EitrCost", 30f,
                    SkillTreeConfig.GetConfigDescription("Tier5_InstantAreaHeal_EitrCost"));

                StaffHealPercentage = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_InstantAreaHeal_HealPercent", 25f,
                    SkillTreeConfig.GetConfigDescription("Tier5_InstantAreaHeal_HealPercent"));

                StaffHealRange = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_InstantAreaHeal_Range", 12f,
                    SkillTreeConfig.GetConfigDescription("Tier5_InstantAreaHeal_Range"));

                StaffHealSelf = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_InstantAreaHeal_SelfHeal", false,
                    SkillTreeConfig.GetConfigDescription("Tier5_InstantAreaHeal_SelfHeal"));

                StaffHealRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Staff Tree", "Tier5_InstantAreaHeal_RequiredPoints", 3,
                    SkillTreeConfig.GetConfigDescription("Tier5_InstantAreaHeal_RequiredPoints"));

                // === 이벤트 핸들러 등록 ===
                RegisterStaffEventHandlers();

                Plugin.Log.LogDebug("[Staff_Config] Config initialized");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff_Config] 설정 초기화 실패: {ex.Message}");
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
                StaffAmpChance.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffAmpDamage.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffAmpEitrCostIncrease.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffFrostDamageBonus.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffFireDamageBonus.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffLightningDamageBonus.SettingChanged += (sender, args) => OnStaffConfigChanged();
                StaffLuckManaChance.SettingChanged += (sender, args) => OnStaffConfigChanged();

                Plugin.Log.LogDebug("[Staff_Config] Event handlers registered");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff_Config] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        private static void OnStaffConfigChanged()
        {
            try
            {
                Plugin.Log.LogInfo("[Staff_Config] 설정값 변경됨");
                LogCurrentConfig();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff_Config] 설정 변경 처리 실패: {ex.Message}");
            }
        }

        // === 설정값 접근자들 ===

        // 지팡이 전문가
        public static float StaffExpertDamageValue => SkillTreeConfig.GetEffectiveValue("staff_tier0_expert_damage", StaffExpertDamage?.Value ?? 12f);
        public static int StaffExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier0_expert_rp", StaffExpertRequiredPoints?.Value ?? 2);

        // Step 2
        public static float StaffFocusEitrReductionValue => SkillTreeConfig.GetEffectiveValue("staff_tier1_focus_eitr", StaffFocusEitrReduction?.Value ?? 15f);
        public static int StaffFocusRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier1_focus_rp", StaffFocusRequiredPoints?.Value ?? 2);
        public static float StaffStreamEitrBonusValue => SkillTreeConfig.GetEffectiveValue("staff_tier1_flow_eitr", StaffStreamCastSpeed?.Value ?? 30f);
        public static int StaffStreamRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier1_flow_rp", StaffStreamRequiredPoints?.Value ?? 2);

        // Step 3
        public static float StaffAmpChanceValue => SkillTreeConfig.GetEffectiveValue("staff_tier2_amp_chance", StaffAmpChance?.Value ?? 38f);
        public static float StaffAmpDamageValue => SkillTreeConfig.GetEffectiveValue("staff_tier2_amp_damage", StaffAmpDamage?.Value ?? 35f);
        public static float StaffAmpEitrCostIncreaseValue => SkillTreeConfig.GetEffectiveValue("staff_tier2_amp_eitr", StaffAmpEitrCostIncrease?.Value ?? 20f);
        public static int StaffAmpRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier2_amp_rp", StaffAmpRequiredPoints?.Value ?? 3);

        // Step 4: 속성 강화
        public static float StaffFrostDamageBonusValue => SkillTreeConfig.GetEffectiveValue("staff_tier3_frost_damage", StaffFrostDamageBonus?.Value ?? 3f);
        public static int StaffFrostRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier3_frost_rp", StaffFrostRequiredPoints?.Value ?? 2);
        public static float StaffFireDamageBonusValue => SkillTreeConfig.GetEffectiveValue("staff_tier3_fire_damage", StaffFireDamageBonus?.Value ?? 3f);
        public static int StaffFireRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier3_fire_rp", StaffFireRequiredPoints?.Value ?? 2);
        public static float StaffLightningDamageBonusValue => SkillTreeConfig.GetEffectiveValue("staff_tier3_lightning_damage", StaffLightningDamageBonus?.Value ?? 3f);
        public static int StaffLightningRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier3_lightning_rp", StaffLightningRequiredPoints?.Value ?? 2);

        // Step 5: 행운 마력
        public static float StaffLuckManaChanceValue => SkillTreeConfig.GetEffectiveValue("staff_tier4_luck_chance", StaffLuckManaChance?.Value ?? 35f);
        public static int StaffLuckManaRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier4_luck_rp", StaffLuckManaRequiredPoints?.Value ?? 3);

        // Step 6-1: 이중시전
        public static int StaffDoubleCastProjectileCountValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier5_dualcast_count", StaffDoubleCastProjectileCount?.Value ?? 2);
        public static float StaffDoubleCastDamagePercentValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_dualcast_damage", StaffDoubleCastDamagePercent?.Value ?? 70f);
        public static float StaffDoubleCastAngleOffsetValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_dualcast_angle", StaffDoubleCastAngleOffset?.Value ?? 5f);
        public static float StaffDoubleCastEitrCostValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_dualcast_eitr", StaffDoubleCastEitrCost?.Value ?? 20f);
        public static float StaffDoubleCastCooldownValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_dualcast_cd", StaffDoubleCastCooldown?.Value ?? 30f);
        public static int StaffDoubleCastRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier5_dualcast_rp", StaffDoubleCastRequiredPoints?.Value ?? 3);

        // Step 6-2: 즉시 범위 힐
        public static float StaffHealCooldownValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_heal_cd", StaffHealCooldown?.Value ?? 30f);
        public static float StaffHealEitrCostValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_heal_eitr", StaffHealEitrCost?.Value ?? 30f);
        public static float StaffHealPercentageValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_heal_percent", StaffHealPercentage?.Value ?? 25f);
        public static float StaffHealRangeValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_heal_range", StaffHealRange?.Value ?? 12f);
        public static bool StaffHealSelfValue => SkillTreeConfig.GetEffectiveValue("staff_tier5_heal_self", StaffHealSelf?.Value ?? false ? 1f : 0f) > 0.5f;
        public static int StaffHealRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("staff_tier5_heal_rp", StaffHealRequiredPoints?.Value ?? 3);

        /// <summary>
        /// 현재 설정값 디버그 출력
        /// </summary>
        public static void LogCurrentConfig()
        {
            try
            {
                Plugin.Log.LogInfo("[Staff_Config] 현재 설정값:");
                Plugin.Log.LogInfo($"  - 지팡이 전문가 공격력: +{StaffExpertDamageValue}%");
                Plugin.Log.LogInfo($"  - 이중시전: 발사체 {StaffDoubleCastProjectileCountValue}개, 쿨타임 {StaffDoubleCastCooldownValue}초");
                Plugin.Log.LogInfo($"  - 즉시 범위 힐: 힐량 {StaffHealPercentageValue}%, 범위 {StaffHealRangeValue}m, 쿨타임 {StaffHealCooldownValue}초, 자기치료 {StaffHealSelfValue}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff_Config] 설정값 로그 출력 실패: {ex.Message}");
            }
        }
    }
}

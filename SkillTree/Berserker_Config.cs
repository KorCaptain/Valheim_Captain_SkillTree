using BepInEx.Configuration;
using CaptainSkillTree;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 버서커 직업 전용 컨피그 시스템
    /// MMO 시스템과 연동하여 동적 값 변경 지원
    /// </summary>
    public static class Berserker_Config
    {
        #region Berserker Skills Configuration

        // === Active Skill: Berserker Rage ===
        public static ConfigEntry<float> BerserkerRageCooldown; // Cooldown (seconds)
        public static ConfigEntry<float> BerserkerRageStaminaCost; // Stamina cost
        public static ConfigEntry<float> BerserkerRageDuration; // Buff duration (seconds)
        public static ConfigEntry<float> BerserkerRageDamagePerHealthPercent; // Damage increase per 1% health (%)

        // === Active Skill: Damage Bonus Settings ===
        public static ConfigEntry<float> BerserkerRageMaxDamageBonus; // Max damage bonus cap (%)
        public static ConfigEntry<float> BerserkerRageHealthThreshold; // Health threshold for effect activation (%)

        // === Passive Skill: Death Defiance ===
        public static ConfigEntry<float> BerserkerPassiveHealthThreshold; // Passive activation health threshold (%)
        public static ConfigEntry<float> BerserkerPassiveInvincibilityDuration; // Invincibility duration (seconds)
        public static ConfigEntry<float> BerserkerPassiveCooldown; // Passive cooldown (seconds)
        public static ConfigEntry<float> BerserkerPassiveHealthBonus; // Max HP bonus (% of total HP)

        #endregion

        #region Dynamic Value Properties (MMO Integration)

        // === Active Skill Dynamic Values (MMO getParameter integration) ===
        public static float BerserkerRageCooldownValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_Cooldown", BerserkerRageCooldown?.Value ?? 45f);
        public static float BerserkerRageStaminaCostValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_StaminaCost", BerserkerRageStaminaCost?.Value ?? 20f);
        public static float BerserkerRageDurationValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_Duration", BerserkerRageDuration?.Value ?? 20f);
        public static float BerserkerRageDamagePerHealthPercentValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_DamagePerHealthPercent", BerserkerRageDamagePerHealthPercent?.Value ?? 2f);

        public static float BerserkerRageMaxDamageBonusValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_MaxDamageBonus", BerserkerRageMaxDamageBonus?.Value ?? 200f);
        public static float BerserkerRageHealthThresholdValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_HealthThreshold", BerserkerRageHealthThreshold?.Value ?? 100f);

        // === Passive Skill Dynamic Values (MMO getParameter integration) ===
        public static float BerserkerPassiveHealthThresholdValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_HealthThreshold", BerserkerPassiveHealthThreshold?.Value ?? 10f);
        public static float BerserkerPassiveInvincibilityDurationValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_InvincibilityDuration", BerserkerPassiveInvincibilityDuration?.Value ?? 8f);
        public static float BerserkerPassiveCooldownValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_Cooldown", BerserkerPassiveCooldown?.Value ?? 180f); // 3 minutes = 180 seconds
        public static float BerserkerPassiveHealthBonusValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_HealthBonus", BerserkerPassiveHealthBonus?.Value ?? 100f);

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize Berserker configuration
        /// </summary>
        public static void InitializeBerserkerConfig()
        {
            try
            {
                // === Active Skill: Berserker Rage ===
                BerserkerRageCooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_Cooldown", 45f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_Cooldown"));

                BerserkerRageStaminaCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_StaminaCost", 20f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_StaminaCost"));

                BerserkerRageDuration = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_Duration", 20f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_Duration"));

                BerserkerRageDamagePerHealthPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_DamagePerHealthPercent", 2f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_DamagePerHealthPercent"));

                BerserkerRageMaxDamageBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_MaxDamageBonus", 200f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_MaxDamageBonus"));

                BerserkerRageHealthThreshold = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_HealthThreshold", 100f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_HealthThreshold"));

                // === Passive Skill: Death Defiance ===
                BerserkerPassiveHealthThreshold = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Passive_HealthThreshold", 10f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Passive_HealthThreshold"));

                BerserkerPassiveInvincibilityDuration = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Passive_InvincibilityDuration", 8f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Passive_InvincibilityDuration"));

                BerserkerPassiveCooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Passive_Cooldown", 180f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Passive_Cooldown"));

                BerserkerPassiveHealthBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Passive_HealthBonus", 100f,
                    SkillTreeConfig.GetConfigDescription("berserker_passive_health_bonus"));

                // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
                RegisterBerserkerEventHandlers();

                Plugin.Log.LogDebug("[Berserker Config] All settings loaded (Active + Passive)");
                LogBerserkerConfigValues();
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] Initialization failed: {ex.Message}");
            }
        }

        /// <summary>
        /// 버서커 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterBerserkerEventHandlers()
        {
            try
            {
                BerserkerRageCooldown.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerRageStaminaCost.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerRageDuration.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerRageDamagePerHealthPercent.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerRageMaxDamageBonus.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerRageHealthThreshold.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerPassiveHealthThreshold.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerPassiveInvincibilityDuration.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerPassiveCooldown.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerPassiveHealthBonus.SettingChanged += (sender, args) => OnBerserkerConfigChanged();

                Plugin.Log.LogDebug("[Berserker Config] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        #endregion

        #region Debug Methods

        /// <summary>
        /// Log Berserker config values
        /// </summary>
        public static void LogBerserkerConfigValues()
        {
            try
            {
                Plugin.Log.LogDebug("=== [Berserker Config] Current Settings ===");

                // Active Skill Settings
                Plugin.Log.LogDebug($"[Active] Cooldown: {BerserkerRageCooldownValue}s");
                Plugin.Log.LogDebug($"[Active] Stamina Cost: {BerserkerRageStaminaCostValue}");
                Plugin.Log.LogDebug($"[Active] Duration: {BerserkerRageDurationValue}s");
                Plugin.Log.LogDebug($"[Active] Damage per 1% HP: {BerserkerRageDamagePerHealthPercentValue}%");
                Plugin.Log.LogDebug($"[Active] Max Damage Bonus: {BerserkerRageMaxDamageBonusValue}%");

                // Passive Skill Settings
                Plugin.Log.LogDebug($"[Passive] Health Threshold: {BerserkerPassiveHealthThresholdValue}%");
                Plugin.Log.LogDebug($"[Passive] Invincibility Duration: {BerserkerPassiveInvincibilityDurationValue}s");
                Plugin.Log.LogDebug($"[Passive] Cooldown: {BerserkerPassiveCooldownValue}s ({BerserkerPassiveCooldownValue / 60f:F1}min)");

                Plugin.Log.LogDebug("=== [Berserker Config] Settings Logged ===");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] Failed to log settings: {ex.Message}");
            }
        }

        /// <summary>
        /// Notify config value changes
        /// </summary>
        public static void OnBerserkerConfigChanged()
        {
            try
            {
                Plugin.Log.LogInfo("[Berserker Config] Settings changed - Updating values");
                LogBerserkerConfigValues();

                // 툴팁 업데이트
                JobSkills.UpdateBerserkerTooltip();
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] Failed to notify config change: {ex.Message}");
            }
        }

        #endregion
    }
}
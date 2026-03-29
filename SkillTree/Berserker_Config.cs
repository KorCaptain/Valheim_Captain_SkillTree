using BepInEx.Configuration;
using CaptainSkillTree;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 버서커 직업 전용 컨피그 시스템 (Lv1~5 레벨업 지원)
    /// MMO 시스템과 연동하여 동적 값 변경 지원
    /// </summary>
    public static class Berserker_Config
    {
        #region Berserker Skills Configuration

        // === Active Skill: Berserker Rage ===
        public static ConfigEntry<float> BerserkerRageCooldown;
        public static ConfigEntry<float> BerserkerRageStaminaCost;
        public static ConfigEntry<float> BerserkerRageDuration;
        public static ConfigEntry<float> BerserkerRageDamagePerHealthPercent;

        // === Active Skill: Damage Bonus Settings ===
        public static ConfigEntry<float> BerserkerRageMaxDamageBonus;
        public static ConfigEntry<float> BerserkerRageHealthThreshold;

        // === Passive Skill: Death Defiance ===
        public static ConfigEntry<float> BerserkerPassiveHealthThreshold;
        public static ConfigEntry<float> BerserkerPassiveInvincibilityDuration;
        public static ConfigEntry<float> BerserkerPassiveCooldown;
        public static ConfigEntry<float> BerserkerPassiveHealthBonus;

        // === Level 2: Active Cooldown Reduction ===
        public static ConfigEntry<float> BerserkerLv2CooldownReduction;

        // === Level 3: Rage Damage Reduction ===
        public static ConfigEntry<float> BerserkerLv3RageDamageReduction;

        // === Level 4: Low HP Attack Bonus ===
        public static ConfigEntry<float> BerserkerLv4LowHpAttackBonus;
        public static ConfigEntry<float> BerserkerLv4LowHpAttackThreshold;

        // === Level 5: Passive Enhancement ===
        public static ConfigEntry<float> BerserkerLv5PassiveCooldownReduction;
        public static ConfigEntry<float> BerserkerLv5InvincibilityBonus;

        #endregion

        #region Dynamic Value Properties (MMO Integration)

        // === Active Skill Dynamic Values ===
        public static float BerserkerRageCooldownValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_Cooldown", BerserkerRageCooldown?.Value ?? 45f);
        public static float BerserkerRageStaminaCostValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_StaminaCost", BerserkerRageStaminaCost?.Value ?? 20f);
        public static float BerserkerRageDurationValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_Duration", BerserkerRageDuration?.Value ?? 20f);
        public static float BerserkerRageDamagePerHealthPercentValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_DamagePerHealthPercent", BerserkerRageDamagePerHealthPercent?.Value ?? 2f);
        public static float BerserkerRageMaxDamageBonusValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_MaxDamageBonus", BerserkerRageMaxDamageBonus?.Value ?? 200f);
        public static float BerserkerRageHealthThresholdValue => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_HealthThreshold", BerserkerRageHealthThreshold?.Value ?? 100f);

        // === Passive Skill Dynamic Values ===
        public static float BerserkerPassiveHealthThresholdValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_HealthThreshold", BerserkerPassiveHealthThreshold?.Value ?? 10f);
        public static float BerserkerPassiveInvincibilityDurationValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_InvincibilityDuration", BerserkerPassiveInvincibilityDuration?.Value ?? 8f);
        public static float BerserkerPassiveCooldownValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_Cooldown", BerserkerPassiveCooldown?.Value ?? 600f);
        public static float BerserkerPassiveHealthBonusValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_HealthBonus", BerserkerPassiveHealthBonus?.Value ?? 100f);

        // === Level 2~5 Dynamic Values ===
        public static float BerserkerLv2CooldownReductionValue => SkillTreeConfig.GetEffectiveValue("Berserker_Lv2_CooldownReduction", BerserkerLv2CooldownReduction?.Value ?? 5f);
        public static float BerserkerLv3RageDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Berserker_Lv3_RageDamageReduction", BerserkerLv3RageDamageReduction?.Value ?? 15f);
        public static float BerserkerLv4LowHpAttackBonusValue => SkillTreeConfig.GetEffectiveValue("Berserker_Lv4_LowHpAttackBonus", BerserkerLv4LowHpAttackBonus?.Value ?? 15f);
        public static float BerserkerLv4LowHpAttackThresholdValue => SkillTreeConfig.GetEffectiveValue("Berserker_Lv4_LowHpAttackThreshold", BerserkerLv4LowHpAttackThreshold?.Value ?? 50f);
        public static float BerserkerLv5PassiveCooldownReductionValue => SkillTreeConfig.GetEffectiveValue("Berserker_Lv5_PassiveCooldownReduction", BerserkerLv5PassiveCooldownReduction?.Value ?? 120f);
        public static float BerserkerLv5InvincibilityBonusValue => SkillTreeConfig.GetEffectiveValue("Berserker_Lv5_InvincibilityBonus", BerserkerLv5InvincibilityBonus?.Value ?? 2f);

        #endregion

        #region Level-based Dynamic Helpers

        /// <summary>
        /// 레벨별 유효 분노 쿨타임 반환 (Lv2: -5초, Lv5: 추가 -5초)
        /// </summary>
        public static float GetEffectiveRageCooldown(int level)
        {
            float baseCooldown = BerserkerRageCooldownValue;
            if (level >= 2) baseCooldown -= BerserkerLv2CooldownReductionValue;
            if (level >= 5) baseCooldown -= 5f;
            return Mathf.Max(baseCooldown, 15f);
        }

        /// <summary>
        /// 레벨별 유효 분노 지속시간 반환 (Lv3: +5초)
        /// </summary>
        public static float GetEffectiveRageDuration(int level)
        {
            float baseDuration = BerserkerRageDurationValue;
            if (level >= 3) baseDuration += 5f;
            return baseDuration;
        }

        /// <summary>
        /// 레벨별 유효 최대 데미지 보너스 반환 (Lv4: +50%)
        /// </summary>
        public static float GetEffectiveMaxDamageBonus(int level)
        {
            float baseBonus = BerserkerRageMaxDamageBonusValue;
            if (level >= 4) baseBonus += 50f;
            return baseBonus;
        }

        /// <summary>
        /// 레벨별 유효 패시브 쿨타임 반환 (Lv5: -120초)
        /// </summary>
        public static float GetEffectivePassiveCooldown(int level)
        {
            float baseCooldown = BerserkerPassiveCooldownValue;
            if (level >= 5) baseCooldown -= BerserkerLv5PassiveCooldownReductionValue;
            return Mathf.Max(baseCooldown, 60f);
        }

        /// <summary>
        /// 레벨별 유효 HP 플랫 보너스 반환 (Lv1:40, Lv2:60, Lv3:80, Lv4:100, Lv5:120)
        /// Config 기본값 120 기준 → 단계당 +20 (step = maxBonus / 6)
        /// </summary>
        public static float GetEffectiveHealthBonus(int level)
        {
            float step = BerserkerPassiveHealthBonusValue / 6f; // default: 20
            return (level + 1) * step;
        }

        /// <summary>
        /// 레벨별 유효 무적 지속시간 반환 (Lv5: +2초)
        /// </summary>
        public static float GetEffectiveInvincibilityDuration(int level)
        {
            float baseDuration = BerserkerPassiveInvincibilityDurationValue;
            if (level >= 5) baseDuration += BerserkerLv5InvincibilityBonusValue;
            return baseDuration;
        }

        #endregion

        #region Initialization

        public static void InitializeBerserkerConfig()
        {
            try
            {
                // ─── Lv1 액티브: 분노 ───────────────────────────────────────────────
                BerserkerRageCooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_Cooldown", 45f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_Cooldown"));

                BerserkerRageDuration = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_Duration", 20f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_Duration"));

                BerserkerRageMaxDamageBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_MaxDamageBonus", 200f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_MaxDamageBonus"));

                BerserkerRageStaminaCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_StaminaCost", 20f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_StaminaCost"));

                BerserkerRageDamagePerHealthPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_DamagePerHealthPercent", 2f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_DamagePerHealthPercent"));

                BerserkerRageHealthThreshold = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Beserker_Active_HealthThreshold", 100f,
                    SkillTreeConfig.GetConfigDescription("Beserker_Active_HealthThreshold"));

                // ─── Lv1 패시브: 죽음의 무시 ────────────────────────────────────────
                BerserkerPassiveHealthBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Passive_HealthBonus", 120f,
                    SkillTreeConfig.GetConfigDescription("berserker_passive_health_bonus"));

                BerserkerPassiveHealthThreshold = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Passive_HealthThreshold", 10f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Passive_HealthThreshold"));

                BerserkerPassiveInvincibilityDuration = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Passive_InvincibilityDuration", 8f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Passive_InvincibilityDuration"));

                BerserkerPassiveCooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Passive_Cooldown", 540f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Passive_Cooldown"));

                // ─── Lv2: 분노 쿨타임 -5초 ──────────────────────────────────────────
                BerserkerLv2CooldownReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Lv2_CooldownReduction", 5f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Lv2_CooldownReduction"));

                // ─── Lv3: 분노 중 피해 감소 ──────────────────────────────────────────
                BerserkerLv3RageDamageReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Lv3_RageDamageReduction", 15f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Lv3_RageDamageReduction"));

                // ─── Lv4: 저체력 공격력 보너스 ────────────────────────────────────────
                BerserkerLv4LowHpAttackBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Lv4_LowHpAttackBonus", 15f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Lv4_LowHpAttackBonus"));

                BerserkerLv4LowHpAttackThreshold = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Lv4_LowHpAttackThreshold", 50f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Lv4_LowHpAttackThreshold"));

                // ─── Lv5: 죽음의 무시 강화 ────────────────────────────────────────────
                BerserkerLv5PassiveCooldownReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Lv5_PassiveCooldownReduction", 120f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Lv5_PassiveCooldownReduction"));

                BerserkerLv5InvincibilityBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                    "Berserker Job Skills", "Berserker_Lv5_InvincibilityBonus", 2f,
                    SkillTreeConfig.GetConfigDescription("Berserker_Lv5_InvincibilityBonus"));

                RegisterBerserkerEventHandlers();

                Plugin.Log.LogDebug("[Berserker Config] All settings loaded (Active + Passive + Lv2~5)");
                LogBerserkerConfigValues();
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] Initialization failed: {ex.Message}");
            }
        }

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
                BerserkerLv2CooldownReduction.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerLv3RageDamageReduction.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerLv4LowHpAttackBonus.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerLv4LowHpAttackThreshold.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerLv5PassiveCooldownReduction.SettingChanged += (sender, args) => OnBerserkerConfigChanged();
                BerserkerLv5InvincibilityBonus.SettingChanged += (sender, args) => OnBerserkerConfigChanged();

                Plugin.Log.LogDebug("[Berserker Config] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        #endregion

        #region Debug Methods

        public static void LogBerserkerConfigValues()
        {
            try
            {
                Plugin.Log.LogDebug("=== [Berserker Config] Current Settings ===");
                Plugin.Log.LogDebug($"[Active] Cooldown: {BerserkerRageCooldownValue}s");
                Plugin.Log.LogDebug($"[Active] Stamina Cost: {BerserkerRageStaminaCostValue}");
                Plugin.Log.LogDebug($"[Active] Duration: {BerserkerRageDurationValue}s");
                Plugin.Log.LogDebug($"[Active] Damage per 1% HP: {BerserkerRageDamagePerHealthPercentValue}%");
                Plugin.Log.LogDebug($"[Active] Max Damage Bonus: {BerserkerRageMaxDamageBonusValue}%");
                Plugin.Log.LogDebug($"[Passive] Health Threshold: {BerserkerPassiveHealthThresholdValue}%");
                Plugin.Log.LogDebug($"[Passive] Invincibility Duration: {BerserkerPassiveInvincibilityDurationValue}s");
                Plugin.Log.LogDebug($"[Passive] Cooldown: {BerserkerPassiveCooldownValue}s");
                Plugin.Log.LogDebug($"[Lv2] CooldownReduction: {BerserkerLv2CooldownReductionValue}s");
                Plugin.Log.LogDebug($"[Lv3] RageDamageReduction: {BerserkerLv3RageDamageReductionValue}%");
                Plugin.Log.LogDebug($"[Lv4] LowHpAttackBonus: {BerserkerLv4LowHpAttackBonusValue}%");
                Plugin.Log.LogDebug($"[Lv5] PassiveCooldownReduction: {BerserkerLv5PassiveCooldownReductionValue}s");
                Plugin.Log.LogDebug($"[Lv5] InvincibilityBonus: {BerserkerLv5InvincibilityBonusValue}s");
                Plugin.Log.LogDebug("=== [Berserker Config] Settings Logged ===");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] Failed to log settings: {ex.Message}");
            }
        }

        public static void OnBerserkerConfigChanged()
        {
            try
            {
                Plugin.Log.LogInfo("[Berserker Config] Settings changed - Updating values");
                LogBerserkerConfigValues();
                JobSkills.UpdateBerserkerTooltip();

                int level = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 0;
                float newCooldown = Mathf.Max(GetEffectivePassiveCooldown(level), 60f);
                ActiveSkillCooldownRegistry.RecalculateCooldown("passive_berserker", newCooldown);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Berserker Config] Failed to notify config change: {ex.Message}");
            }
        }

        #endregion
    }
}

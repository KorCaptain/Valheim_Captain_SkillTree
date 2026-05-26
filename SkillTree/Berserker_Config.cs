using BepInEx.Configuration;
using CaptainSkillTree;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 버서커 직업 전용 컨피그 시스템 (Lv1~5 레벨별 개별 Config)
    /// </summary>
    public static class Berserker_Config
    {
        #region ConfigEntry Declarations

        // === Active: 공통 (모든 레벨 공유) ===
        public static ConfigEntry<float> BerserkerRageStaminaCost;
        public static ConfigEntry<float> BerserkerRageMaxDamageBonus;
        public static ConfigEntry<float> BerserkerRageHealthThreshold;

        // === Passive: 공통 ===
        public static ConfigEntry<float> BerserkerPassiveHealthThreshold;
        public static ConfigEntry<float> BerserkerPassiveInvincibilityDuration;
        public static ConfigEntry<float> BerserkerPassiveCooldown;

        // === Lv1~5: 분노 쿨타임 (초) ===
        public static ConfigEntry<float> BerserkerLv1ActiveCooldown;
        public static ConfigEntry<float> BerserkerLv2ActiveCooldown;
        public static ConfigEntry<float> BerserkerLv3ActiveCooldown;
        public static ConfigEntry<float> BerserkerLv4ActiveCooldown;
        public static ConfigEntry<float> BerserkerLv5ActiveCooldown;

        // === Lv1~5: 분노 지속시간 (초) ===
        public static ConfigEntry<float> BerserkerLv1ActiveDuration;
        public static ConfigEntry<float> BerserkerLv2ActiveDuration;
        public static ConfigEntry<float> BerserkerLv3ActiveDuration;
        public static ConfigEntry<float> BerserkerLv4ActiveDuration;
        public static ConfigEntry<float> BerserkerLv5ActiveDuration;

        // === Lv1~5: 패시브 최대 HP 보너스 (플랫) ===
        public static ConfigEntry<float> BerserkerLv1PassiveHealthBonus;
        public static ConfigEntry<float> BerserkerLv2PassiveHealthBonus;
        public static ConfigEntry<float> BerserkerLv3PassiveHealthBonus;
        public static ConfigEntry<float> BerserkerLv4PassiveHealthBonus;
        public static ConfigEntry<float> BerserkerLv5PassiveHealthBonus;

        // === Lv1~5: HP 1%당 공격력 증가 (%) ===
        public static ConfigEntry<float> BerserkerLv1ActiveDamagePerHP;
        public static ConfigEntry<float> BerserkerLv2ActiveDamagePerHP;
        public static ConfigEntry<float> BerserkerLv3ActiveDamagePerHP;
        public static ConfigEntry<float> BerserkerLv4ActiveDamagePerHP;
        public static ConfigEntry<float> BerserkerLv5ActiveDamagePerHP;

        #endregion

        #region Dynamic Value Properties (MMO Integration)

        // === Active: 공통 ===
        public static float BerserkerRageStaminaCostValue        => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_StaminaCost",   BerserkerRageStaminaCost?.Value   ?? 20f);
        public static float BerserkerRageMaxDamageBonusValue     => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_MaxDamageBonus", BerserkerRageMaxDamageBonus?.Value ?? 200f);
        public static float BerserkerRageHealthThresholdValue     => SkillTreeConfig.GetEffectiveValue("Berserker_Rage_HealthThreshold",BerserkerRageHealthThreshold?.Value ?? 100f);

        // === Passive: 공통 ===
        public static float BerserkerPassiveHealthThresholdValue     => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_HealthThreshold",      BerserkerPassiveHealthThreshold?.Value      ?? 10f);
        public static float BerserkerPassiveInvincibilityDurationValue => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_InvincibilityDuration", BerserkerPassiveInvincibilityDuration?.Value ?? 8f);
        public static float BerserkerPassiveCooldownValue            => SkillTreeConfig.GetEffectiveValue("Berserker_Passive_Cooldown",             BerserkerPassiveCooldown?.Value             ?? 540f);

        #endregion

        #region Level-based Dynamic Helpers

        /// <summary>레벨별 분노 쿨타임 (각 레벨 독립 Config)</summary>
        public static float GetEffectiveRageCooldown(int level) => level switch
        {
            1 => BerserkerLv1ActiveCooldown?.Value ?? 45f,
            2 => BerserkerLv2ActiveCooldown?.Value ?? 40f,
            3 => BerserkerLv3ActiveCooldown?.Value ?? 40f,
            4 => BerserkerLv4ActiveCooldown?.Value ?? 40f,
            _ => BerserkerLv5ActiveCooldown?.Value ?? 35f,
        };

        /// <summary>레벨별 분노 지속시간 (각 레벨 독립 Config)</summary>
        public static float GetEffectiveRageDuration(int level) => level switch
        {
            1 => BerserkerLv1ActiveDuration?.Value ?? 20f,
            2 => BerserkerLv2ActiveDuration?.Value ?? 20f,
            3 => BerserkerLv3ActiveDuration?.Value ?? 25f,
            4 => BerserkerLv4ActiveDuration?.Value ?? 25f,
            _ => BerserkerLv5ActiveDuration?.Value ?? 25f,
        };

        /// <summary>레벨별 최대 데미지 보너스 (Lv4: +50%)</summary>
        public static float GetEffectiveMaxDamageBonus(int level)
        {
            float baseBonus = BerserkerRageMaxDamageBonusValue;
            if (level >= 4) baseBonus += 50f;
            return baseBonus;
        }

        /// <summary>레벨별 HP 1%당 공격력 증가 비율 (각 레벨 독립 Config)</summary>
        public static float GetEffectiveDamagePerHealthPercent(int level) => level switch
        {
            1 => BerserkerLv1ActiveDamagePerHP?.Value ?? 1.5f,
            2 => BerserkerLv2ActiveDamagePerHP?.Value ?? 1.6f,
            3 => BerserkerLv3ActiveDamagePerHP?.Value ?? 1.7f,
            4 => BerserkerLv4ActiveDamagePerHP?.Value ?? 1.8f,
            _ => BerserkerLv5ActiveDamagePerHP?.Value ?? 2.0f,
        };

        /// <summary>레벨별 패시브 HP 보너스 (각 레벨 독립 Config)</summary>
        public static float GetEffectiveHealthBonus(int level) => level switch
        {
            1 => BerserkerLv1PassiveHealthBonus?.Value ?? 40f,
            2 => BerserkerLv2PassiveHealthBonus?.Value ?? 60f,
            3 => BerserkerLv3PassiveHealthBonus?.Value ?? 80f,
            4 => BerserkerLv4PassiveHealthBonus?.Value ?? 100f,
            _ => BerserkerLv5PassiveHealthBonus?.Value ?? 120f,
        };

        #endregion

        #region Initialization

        public static void InitializeBerserkerConfig()
        {
            try
            {
                var cfg = Plugin.Instance.Config;
                const string section = "Berserker Job Skills";

                // ─── 액티브 공통 ──────────────────────────────────────────────
                BerserkerRageStaminaCost    = SkillTreeConfig.BindServerSync(cfg, section, "Beserker_Active_StaminaCost",    20f,  SkillTreeConfig.GetConfigDescription("Beserker_Active_StaminaCost"));
                BerserkerRageMaxDamageBonus = SkillTreeConfig.BindServerSync(cfg, section, "Beserker_Active_MaxDamageBonus", 200f, SkillTreeConfig.GetConfigDescription("Beserker_Active_MaxDamageBonus"));
                BerserkerRageHealthThreshold = SkillTreeConfig.BindServerSync(cfg, section, "Beserker_Active_HealthThreshold", 100f, SkillTreeConfig.GetConfigDescription("Beserker_Active_HealthThreshold"));

                // ─── 패시브 공통 ──────────────────────────────────────────────
                BerserkerPassiveHealthThreshold      = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Passive_HealthThreshold",      10f,  SkillTreeConfig.GetConfigDescription("Berserker_Passive_HealthThreshold"));
                BerserkerPassiveInvincibilityDuration = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Passive_InvincibilityDuration", 8f,   SkillTreeConfig.GetConfigDescription("Berserker_Passive_InvincibilityDuration"));
                BerserkerPassiveCooldown             = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Passive_Cooldown",             540f, SkillTreeConfig.GetConfigDescription("Berserker_Passive_Cooldown"));

                // ─── Lv1~5: 분노 쿨타임 ─────────────────────────────────────
                BerserkerLv1ActiveCooldown = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv1_Active_Cooldown", 45f, SkillTreeConfig.GetConfigDescription("Berserker_Lv1_Active_Cooldown"));
                BerserkerLv2ActiveCooldown = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv2_Active_Cooldown", 40f, SkillTreeConfig.GetConfigDescription("Berserker_Lv2_Active_Cooldown"));
                BerserkerLv3ActiveCooldown = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv3_Active_Cooldown", 40f, SkillTreeConfig.GetConfigDescription("Berserker_Lv3_Active_Cooldown"));
                BerserkerLv4ActiveCooldown = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv4_Active_Cooldown", 40f, SkillTreeConfig.GetConfigDescription("Berserker_Lv4_Active_Cooldown"));
                BerserkerLv5ActiveCooldown = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv5_Active_Cooldown", 35f, SkillTreeConfig.GetConfigDescription("Berserker_Lv5_Active_Cooldown"));

                // ─── Lv1~5: 분노 지속시간 ───────────────────────────────────
                BerserkerLv1ActiveDuration = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv1_Active_Duration", 20f, SkillTreeConfig.GetConfigDescription("Berserker_Lv1_Active_Duration"));
                BerserkerLv2ActiveDuration = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv2_Active_Duration", 20f, SkillTreeConfig.GetConfigDescription("Berserker_Lv2_Active_Duration"));
                BerserkerLv3ActiveDuration = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv3_Active_Duration", 25f, SkillTreeConfig.GetConfigDescription("Berserker_Lv3_Active_Duration"));
                BerserkerLv4ActiveDuration = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv4_Active_Duration", 25f, SkillTreeConfig.GetConfigDescription("Berserker_Lv4_Active_Duration"));
                BerserkerLv5ActiveDuration = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv5_Active_Duration", 25f, SkillTreeConfig.GetConfigDescription("Berserker_Lv5_Active_Duration"));

                // ─── Lv1~5: 패시브 HP 보너스 ────────────────────────────────
                BerserkerLv1PassiveHealthBonus = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv1_Passive_HealthBonus",  40f, SkillTreeConfig.GetConfigDescription("Berserker_Lv1_Passive_HealthBonus"));
                BerserkerLv2PassiveHealthBonus = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv2_Passive_HealthBonus",  60f, SkillTreeConfig.GetConfigDescription("Berserker_Lv2_Passive_HealthBonus"));
                BerserkerLv3PassiveHealthBonus = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv3_Passive_HealthBonus",  80f, SkillTreeConfig.GetConfigDescription("Berserker_Lv3_Passive_HealthBonus"));
                BerserkerLv4PassiveHealthBonus = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv4_Passive_HealthBonus", 100f, SkillTreeConfig.GetConfigDescription("Berserker_Lv4_Passive_HealthBonus"));
                BerserkerLv5PassiveHealthBonus = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv5_Passive_HealthBonus", 120f, SkillTreeConfig.GetConfigDescription("Berserker_Lv5_Passive_HealthBonus"));

                // ─── Lv1~5: HP 1%당 공격력 증가 ──────────────────────────────
                BerserkerLv1ActiveDamagePerHP = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv1_Active_DamagePerHP", 1.5f, SkillTreeConfig.GetConfigDescription("Berserker_Lv1_Active_DamagePerHP"));
                BerserkerLv2ActiveDamagePerHP = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv2_Active_DamagePerHP", 1.6f, SkillTreeConfig.GetConfigDescription("Berserker_Lv2_Active_DamagePerHP"));
                BerserkerLv3ActiveDamagePerHP = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv3_Active_DamagePerHP", 1.7f, SkillTreeConfig.GetConfigDescription("Berserker_Lv3_Active_DamagePerHP"));
                BerserkerLv4ActiveDamagePerHP = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv4_Active_DamagePerHP", 1.8f, SkillTreeConfig.GetConfigDescription("Berserker_Lv4_Active_DamagePerHP"));
                BerserkerLv5ActiveDamagePerHP = SkillTreeConfig.BindServerSync(cfg, section, "Berserker_Lv5_Active_DamagePerHP", 2.0f, SkillTreeConfig.GetConfigDescription("Berserker_Lv5_Active_DamagePerHP"));

                RegisterBerserkerEventHandlers();

                Plugin.Log.LogDebug("[Berserker Config] All settings loaded (Lv1~5 per-level)");
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
                BerserkerRageStaminaCost.SettingChanged              += (s, a) => OnBerserkerConfigChanged();
                BerserkerRageMaxDamageBonus.SettingChanged           += (s, a) => OnBerserkerConfigChanged();
                BerserkerRageHealthThreshold.SettingChanged          += (s, a) => OnBerserkerConfigChanged();
                BerserkerPassiveHealthThreshold.SettingChanged       += (s, a) => OnBerserkerConfigChanged();
                BerserkerPassiveInvincibilityDuration.SettingChanged += (s, a) => OnBerserkerConfigChanged();
                BerserkerPassiveCooldown.SettingChanged              += (s, a) => OnBerserkerConfigChanged();

                BerserkerLv1ActiveCooldown.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv2ActiveCooldown.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv3ActiveCooldown.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv4ActiveCooldown.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv5ActiveCooldown.SettingChanged            += (s, a) => OnBerserkerConfigChanged();

                BerserkerLv1ActiveDuration.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv2ActiveDuration.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv3ActiveDuration.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv4ActiveDuration.SettingChanged            += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv5ActiveDuration.SettingChanged            += (s, a) => OnBerserkerConfigChanged();

                BerserkerLv1PassiveHealthBonus.SettingChanged        += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv2PassiveHealthBonus.SettingChanged        += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv3PassiveHealthBonus.SettingChanged        += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv4PassiveHealthBonus.SettingChanged        += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv5PassiveHealthBonus.SettingChanged        += (s, a) => OnBerserkerConfigChanged();

                BerserkerLv1ActiveDamagePerHP.SettingChanged         += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv2ActiveDamagePerHP.SettingChanged         += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv3ActiveDamagePerHP.SettingChanged         += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv4ActiveDamagePerHP.SettingChanged         += (s, a) => OnBerserkerConfigChanged();
                BerserkerLv5ActiveDamagePerHP.SettingChanged         += (s, a) => OnBerserkerConfigChanged();

                Plugin.Log.LogDebug("[Berserker Config] 이벤트 핸들러 등록 완료");
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
                Plugin.Log.LogDebug($"[Active] Stamina:{BerserkerRageStaminaCostValue}  MaxDmgBonus:{BerserkerRageMaxDamageBonusValue}%");
                Plugin.Log.LogDebug($"[Cooldown] Lv1:{BerserkerLv1ActiveCooldown?.Value}s Lv2:{BerserkerLv2ActiveCooldown?.Value}s Lv3:{BerserkerLv3ActiveCooldown?.Value}s Lv4:{BerserkerLv4ActiveCooldown?.Value}s Lv5:{BerserkerLv5ActiveCooldown?.Value}s");
                Plugin.Log.LogDebug($"[Duration] Lv1:{BerserkerLv1ActiveDuration?.Value}s Lv2:{BerserkerLv2ActiveDuration?.Value}s Lv3:{BerserkerLv3ActiveDuration?.Value}s Lv4:{BerserkerLv4ActiveDuration?.Value}s Lv5:{BerserkerLv5ActiveDuration?.Value}s");
                Plugin.Log.LogDebug($"[HP Bonus] Lv1:{BerserkerLv1PassiveHealthBonus?.Value} Lv2:{BerserkerLv2PassiveHealthBonus?.Value} Lv3:{BerserkerLv3PassiveHealthBonus?.Value} Lv4:{BerserkerLv4PassiveHealthBonus?.Value} Lv5:{BerserkerLv5PassiveHealthBonus?.Value}");
                Plugin.Log.LogDebug($"[DmgPerHP] Lv1:{BerserkerLv1ActiveDamagePerHP?.Value}% Lv2:{BerserkerLv2ActiveDamagePerHP?.Value}% Lv3:{BerserkerLv3ActiveDamagePerHP?.Value}% Lv4:{BerserkerLv4ActiveDamagePerHP?.Value}% Lv5:{BerserkerLv5ActiveDamagePerHP?.Value}%");
                Plugin.Log.LogDebug($"[Passive] Threshold:{BerserkerPassiveHealthThresholdValue}%  Invincibility:{BerserkerPassiveInvincibilityDurationValue}s  Cooldown:{BerserkerPassiveCooldownValue}s");
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

                float newCooldown = Mathf.Max(BerserkerPassiveCooldownValue, 60f);
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

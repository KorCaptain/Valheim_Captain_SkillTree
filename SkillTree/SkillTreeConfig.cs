using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using System.IO;
using System.Text;
using Jotunn.Managers;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Jotunn Config 동기화용 속성 클래스
    /// IsAdminOnly = true로 설정된 Config는 서버에서 모든 클라이언트로 자동 동기화됨
    /// </summary>
    public class ConfigurationManagerAttributes
    {
        public bool? IsAdminOnly;
        public bool? Browsable;
        public string Category;
        public int? Order;
        public string DispName;  // F1 메뉴 2차 항목 표시명 (키 이름 번역)
    }

    /// <summary>
    /// 스킬트리 Config 오케스트레이터
    /// 각 무기/트리별 Config는 개별 파일로 분리됨
    /// </summary>
    public static class SkillTreeConfig
    {
        // 서버/클라이언트 동기화용 데이터
        private static Dictionary<string, float> _serverConfigValues = new Dictionary<string, float>();
        private static bool _isServer = false;
        private static bool _hasReceivedServerConfig = false;

        // Config 파일 변경 감지
        private static FileSystemWatcher _configWatcher = null;
        private static ConfigFile _configFile = null;

        // === Language Detection for Config Manager (BepInEx F1 Menu) ===
        private static string _detectedConfigLanguage = "ko";

        /// <summary>
        /// Configuration Manager 표시 언어 감지
        /// BepInEx ConfigDescription은 초기화 시점에 고정되므로 게임 시작 시에만 감지
        /// </summary>
        private static string DetectConfigLanguage()
        {
            try
            {
                // 우선순위 1: SkillTreeConfig.Language 설정 (사용자가 직접 설정한 경우)
                if (Language != null && Language.Value != "Auto")
                {
                    string configLang = Language.Value.ToLower();
                    string result = (configLang == "ko" || configLang == "kr") ? "ko"
                                  : (configLang == "ru") ? "ru"
                                  : "en";
                    Plugin.Log.LogDebug($"[SkillTreeConfig] Using config language: {Language.Value} -> {result}");
                    return result;
                }

                // 우선순위 2: PlayerPrefs 직접 읽기 (Valheim 게임 설정)
                string valheimLang = UnityEngine.PlayerPrefs.GetString("language", "");
                if (!string.IsNullOrEmpty(valheimLang))
                {
                    string langLow = valheimLang.ToLower();
                    string result = (langLow == "korean") ? "ko"
                                  : (langLow == "russian") ? "ru"
                                  : "en";
                    Plugin.Log.LogDebug($"[SkillTreeConfig] Using Valheim language: {valheimLang} -> {result}");
                    return result;
                }

                // 우선순위 3: LocalizationManager (fallback, 이미 초기화된 경우)
                string currentLang = Localization.LocalizationManager.GetCurrentLanguage();
                if (!string.IsNullOrEmpty(currentLang) && currentLang != "ko")
                {
                    Plugin.Log.LogDebug($"[SkillTreeConfig] Using LocalizationManager: {currentLang}");
                    return (currentLang == "ko") ? "ko"
                         : (currentLang == "ru") ? "ru"
                         : "en";
                }

                // 기본값: 한국어
                Plugin.Log.LogDebug("[SkillTreeConfig] Using default language: ko");
                return "ko";
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeConfig] DetectConfigLanguage failed: {ex.Message}");
                return "ko"; // 실패 시 한국어 기본값
            }
        }

        /// <summary>
        /// 카테고리 로컬라이제이션 (Config Manager F1 메뉴)
        /// </summary>
        internal static string GetLocalizedCategory(string categoryKey)
        {
            var translations = Localization.ConfigTranslations.GetCategoryTranslations(_detectedConfigLanguage);
            return translations.ContainsKey(categoryKey) ? translations[categoryKey] : categoryKey;
        }

        /// <summary>
        /// 설명 로컬라이제이션 (Config Manager F1 메뉴)
        /// </summary>
        internal static string GetLocalizedDescription(string descriptionKey)
        {
            // _RequiredPoints 키는 런타임에 처리 (ConfigTranslations에서 제거됨)
            if (descriptionKey.EndsWith("_RequiredPoints"))
                return _detectedConfigLanguage == "ru"
                    ? "【Необходимые очки】\nОчки навыков для разблокировки этого узла."
                    : _detectedConfigLanguage == "en"
                        ? "【Required Points】\nPoints required to unlock this node."
                        : "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.";
            var translations = Localization.ConfigTranslations.GetDescriptionTranslations(_detectedConfigLanguage);
            return translations.ContainsKey(descriptionKey) ? translations[descriptionKey] : descriptionKey;
        }

        /// <summary>
        /// 키 이름 로컬라이제이션 (Config Manager F1 메뉴 2차 항목)
        /// </summary>
        internal static string GetLocalizedKeyName(string keyName)
        {
            var translations = Localization.ConfigTranslations.GetKeyNameTranslations(_detectedConfigLanguage);
            if (translations.ContainsKey(keyName)) return translations[keyName];
            // _RequiredPoints 키는 dict에 없을 경우 런타임 자동 생성 (fallback)
            if (keyName.EndsWith("_RequiredPoints"))
            {
                var tierPart = keyName.Split('_')[0]; // "TierX" or "Knife" 등
                return _detectedConfigLanguage == "ru"
                    ? $"{tierPart}: Необходимые очки"
                    : _detectedConfigLanguage == "en"
                        ? $"{tierPart}: Required Points"
                        : $"{tierPart}: 필요 포인트";
            }
            return keyName;
        }

        #region === Config 바인드 헬퍼 메서드 ===

        public static ConfigEntry<float> BindServerSync(ConfigFile config, string section, string key, float defaultValue, string description, int order = 0)
        {
            // 카테고리명 자동 번역 (Config Manager 표시용)
            string localizedSection = GetLocalizedCategory(section);
            // 키 이름 자동 번역 (Config Manager F1 메뉴 2차 항목)
            string localizedKeyName = GetLocalizedKeyName(key);
            return config.Bind(localizedSection, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes {
                        IsAdminOnly = true,
                        DispName = localizedKeyName,
                        Order = order
                    }));
        }

        public static ConfigEntry<int> BindServerSync(ConfigFile config, string section, string key, int defaultValue, string description, int order = 0)
        {
            // 카테고리명 자동 번역 (Config Manager 표시용)
            string localizedSection = GetLocalizedCategory(section);
            // 키 이름 자동 번역 (Config Manager F1 메뉴 2차 항목)
            string localizedKeyName = GetLocalizedKeyName(key);
            return config.Bind(localizedSection, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes {
                        IsAdminOnly = true,
                        DispName = localizedKeyName,
                        Order = order
                    }));
        }

        public static ConfigEntry<bool> BindServerSync(ConfigFile config, string section, string key, bool defaultValue, string description, int order = 0)
        {
            // 카테고리명 자동 번역 (Config Manager 표시용)
            string localizedSection = GetLocalizedCategory(section);
            // 키 이름 자동 번역 (Config Manager F1 메뉴 2차 항목)
            string localizedKeyName = GetLocalizedKeyName(key);
            return config.Bind(localizedSection, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes {
                        IsAdminOnly = true,
                        DispName = localizedKeyName,
                        Order = order
                    }));
        }

        public static ConfigEntry<string> BindServerSync(ConfigFile config, string section, string key, string defaultValue, string description, int order = 0)
        {
            // 카테고리명 자동 번역 (Config Manager 표시용)
            string localizedSection = GetLocalizedCategory(section);
            // 키 이름 자동 번역 (Config Manager F1 메뉴 2차 항목)
            string localizedKeyName = GetLocalizedKeyName(key);
            return config.Bind(localizedSection, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes {
                        IsAdminOnly = true,
                        DispName = localizedKeyName,
                        Order = order
                    }));
        }

        #endregion

        #region === Skill_Tree_Base 핵심 설정 ===

        // 언어 설정
        public static ConfigEntry<string> Language;

        // 이동속도/공격속도 최대치 제한
        public static ConfigEntry<float> MoveSpeedMaxBonus;
        public static ConfigEntry<float> AttackSpeedMaxBonus;

        // 액티브 스킬 키 바인딩 (클라이언트 로컬)
        public static ConfigEntry<string> HotKeyY;
        public static ConfigEntry<string> HotKeyR;
        public static ConfigEntry<string> HotKeyG;
        public static ConfigEntry<string> HotKeyH;

        // HUD 위치 Config
        public static ConfigEntry<int> HudPosX;
        public static ConfigEntry<int> HudPosY;

        // 동적 값 접근 프로퍼티
        public static string LanguageValue => Language?.Value ?? "Korean";
        public static float MoveSpeedMaxBonusValue => GetEffectiveValue("move_speed_max_bonus", MoveSpeedMaxBonus?.Value ?? 70f);
        public static float AttackSpeedMaxBonusValue => GetEffectiveValue("attack_speed_max_bonus", AttackSpeedMaxBonus?.Value ?? 70f);

        /// <summary>
        /// ConfigEntry<string> 키 이름을 KeyCode로 변환합니다.
        /// 실패 시 fallback 반환.
        /// </summary>
        public static KeyCode GetHotKeyCode(ConfigEntry<string> entry, KeyCode fallback)
        {
            if (entry == null) return fallback;
            if (System.Enum.TryParse<KeyCode>(entry.Value, true, out KeyCode result))
                return result;
            return fallback;
        }

        #endregion

        #region === 검 전문가 스킬 설정 ===

        public static ConfigEntry<float> SwordExpertDamage;
        public static ConfigEntry<float> SwordStep1ExpertComboBonus;
        public static ConfigEntry<float> SwordStep1ExpertDuration;
        public static ConfigEntry<float> SwordStep1FastSlashSpeed;
        public static ConfigEntry<float> SwordStep1CounterDefenseBonus;
        public static ConfigEntry<float> SwordStep1CounterDuration;
        public static ConfigEntry<float> SwordStep2ComboSlashBonus;
        public static ConfigEntry<float> SwordStep2ComboSlashDuration;
        public static ConfigEntry<float> SwordStep3BladeCounterBonus;
        public static ConfigEntry<float> SwordStep3BladeCounterDuration;
        public static ConfigEntry<float> SwordStep3OffenseDefenseAttackBonus;
        public static ConfigEntry<float> SwordStep3OffenseDefenseDefenseBonus;
        public static ConfigEntry<float> SwordStep4TrueDuelSpeed;
        public static ConfigEntry<float> SwordStep5DefenseSwitchShieldReduction;
        public static ConfigEntry<float> SwordStep5DefenseSwitchNoShieldBonus;
        public static ConfigEntry<float> SwordStep6UltimateSlashMultiplier;

        // 검 전문가 접근 프로퍼티들 (Sword_Config 참조로 변경)
        public static float SwordExpertDamageValue => Sword_Config.SwordExpertDamageValue;
        public static float SwordStep1ExpertComboBonusValue => Sword_Config.SwordStep2ComboSlashBonusValue;
        public static float SwordStep1ExpertDurationValue => Sword_Config.SwordStep1ExpertDurationValue;
        public static float SwordStep1FastSlashSpeedValue => Sword_Config.SwordStep1FastSlashSpeedValue;
        public static float SwordStep1CounterDefenseBonusValue => Sword_Config.SwordStep1CounterDefenseBonusValue;
        public static float SwordStep1CounterDurationValue => Sword_Config.SwordStep1CounterDurationValue;
        public static float SwordStep2ComboSlashBonusValue => Sword_Config.SwordStep2ComboSlashBonusValue;
        public static float SwordStep2ComboSlashDurationValue => Sword_Config.SwordStep2ComboSlashDurationValue;
        public static float SwordStep3BladeCounterBonusValue => Sword_Config.SwordRiposteDamageBonusValue;
        public static float SwordStep3BladeCounterDurationValue => 0f; // Deprecated - no longer used
        public static float SwordStep3OffenseDefenseAttackBonusValue => Sword_Config.SwordStep3AllInOneAttackBonusValue;
        public static float SwordStep3OffenseDefenseDefenseBonusValue => Sword_Config.SwordStep3AllInOneDefenseBonusValue;
        public static float SwordStep4TrueDuelSpeedValue => Sword_Config.SwordStep4TrueDuelSpeedValue;
        public static float SwordStep5DefenseSwitchShieldReductionValue => 0f; // Deprecated - no longer used
        public static float SwordStep5DefenseSwitchNoShieldBonusValue => 0f; // Deprecated - no longer used
        public static float SwordStep6UltimateSlashMultiplierValue => 0f; // Deprecated - no longer used

        #endregion

        #region === Proxy: 공격 전문가 (Attack_Config) ===

        public static ConfigEntry<float> AttackRootDamageBonus => Attack_Config.AttackRootDamageBonus;
        public static ConfigEntry<float> AttackMeleeBonusChance => Attack_Config.AttackMeleeBonusChance;
        public static ConfigEntry<float> AttackMeleeBonusDamage => Attack_Config.AttackMeleeBonusDamage;
        public static ConfigEntry<float> AttackBowBonusChance => Attack_Config.AttackBowBonusChance;
        public static ConfigEntry<float> AttackBowBonusDamage => Attack_Config.AttackBowBonusDamage;
        public static ConfigEntry<float> AttackCrossbowBonusChance => Attack_Config.AttackCrossbowBonusChance;
        public static ConfigEntry<float> AttackCrossbowBonusDamage => Attack_Config.AttackCrossbowBonusDamage;
        public static ConfigEntry<float> AttackStaffBonusChance => Attack_Config.AttackStaffBonusChance;
        public static ConfigEntry<float> AttackStaffBonusDamage => Attack_Config.AttackStaffBonusDamage;
        public static ConfigEntry<float> AttackBasePhysicalDamage => Attack_Config.AttackBasePhysicalDamage;
        public static ConfigEntry<float> AttackBaseElementalDamage => Attack_Config.AttackBaseElementalDamage;
        public static ConfigEntry<float> AttackTwoHandDrainPhysicalDamage => Attack_Config.AttackTwoHandDrainPhysicalDamage;
        public static ConfigEntry<float> AttackTwoHandDrainElementalDamage => Attack_Config.AttackTwoHandDrainElementalDamage;
        public static ConfigEntry<float> AttackCritChance => Attack_Config.AttackCritChance;
        public static ConfigEntry<float> AttackMeleeEnhancement => Attack_Config.AttackMeleeEnhancement;
        public static ConfigEntry<float> AttackRangedEnhancement => Attack_Config.AttackRangedEnhancement;
        public static ConfigEntry<float> AttackSpecialStat => Attack_Config.AttackSpecialStat;
        public static ConfigEntry<float> AttackCritDamageBonus => Attack_Config.AttackCritDamageBonus;
        public static ConfigEntry<float> AttackTwoHandedBonus => Attack_Config.AttackTwoHandedBonus;
        public static ConfigEntry<float> AttackStaffElemental => Attack_Config.AttackStaffElemental;
        public static ConfigEntry<float> AttackFinisherMeleeBonus => Attack_Config.AttackFinisherMeleeBonus;

        public static float AttackRootDamageBonusValue => Attack_Config.AttackRootDamageBonusValue;
        public static float AttackMeleeBonusChanceValue => Attack_Config.AttackMeleeBonusChanceValue;
        public static float AttackMeleeBonusDamageValue => Attack_Config.AttackMeleeBonusDamageValue;
        public static float AttackBowBonusChanceValue => Attack_Config.AttackBowBonusChanceValue;
        public static float AttackBowBonusDamageValue => Attack_Config.AttackBowBonusDamageValue;
        public static float AttackCrossbowBonusChanceValue => Attack_Config.AttackCrossbowBonusChanceValue;
        public static float AttackCrossbowBonusDamageValue => Attack_Config.AttackCrossbowBonusDamageValue;
        public static float AttackStaffBonusChanceValue => Attack_Config.AttackStaffBonusChanceValue;
        public static float AttackStaffBonusDamageValue => Attack_Config.AttackStaffBonusDamageValue;
        public static float AttackBasePhysicalDamageValue => Attack_Config.AttackBasePhysicalDamageValue;
        public static float AttackBaseElementalDamageValue => Attack_Config.AttackBaseElementalDamageValue;
        public static float AttackTwoHandDrainPhysicalDamageValue => Attack_Config.AttackTwoHandDrainPhysicalDamageValue;
        public static float AttackTwoHandDrainElementalDamageValue => Attack_Config.AttackTwoHandDrainElementalDamageValue;
        public static float AttackCritChanceValue => Attack_Config.AttackCritChanceValue;
        public static float AttackMeleeEnhancementValue => Attack_Config.AttackMeleeEnhancementValue;
        public static float AttackRangedEnhancementValue => Attack_Config.AttackRangedEnhancementValue;
        public static float AttackSpecialStatValue => Attack_Config.AttackSpecialStatValue;
        public static float AttackCritDamageBonusValue => Attack_Config.AttackCritDamageBonusValue;
        public static float AttackTwoHandedBonusValue => Attack_Config.AttackTwoHandedBonusValue;
        public static float AttackStaffElementalValue => Attack_Config.AttackStaffElementalValue;
        public static float AttackFinisherMeleeBonusValue => Attack_Config.AttackFinisherMeleeBonusValue;
        public static float AttackOneHandedBonusValue => Attack_Config.AttackOneHandedBonusValue;

        #endregion

        #region === Proxy: 속도 전문가 (Speed_Config) ===

        public static ConfigEntry<float> SpeedRootMoveSpeed => Speed_Config.SpeedRootMoveSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeMoveSpeed => Speed_Config.SpeedBaseDodgeMoveSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeDuration => Speed_Config.SpeedBaseDodgeDuration;
        public static ConfigEntry<float> SpeedMeleeComboAttackSpeed => Speed_Config.SpeedMeleeComboAttackSpeed;
        public static ConfigEntry<float> SpeedMeleeComboStamina => Speed_Config.SpeedMeleeComboStamina;
        public static ConfigEntry<float> SpeedMeleeComboDuration => Speed_Config.SpeedMeleeComboDuration;
        public static ConfigEntry<float> SpeedCrossbowExpertSpeed => Speed_Config.SpeedCrossbowExpertSpeed;
        public static ConfigEntry<float> SpeedCrossbowExpertDuration => Speed_Config.SpeedCrossbowExpertDuration;
        public static ConfigEntry<float> SpeedCrossbowExpertReload => Speed_Config.SpeedCrossbowExpertReload;
        public static ConfigEntry<float> SpeedBowExpertStamina => Speed_Config.SpeedBowExpertStamina;
        public static ConfigEntry<float> SpeedBowExpertDrawSpeed => Speed_Config.SpeedBowExpertDrawSpeed;
        public static ConfigEntry<float> SpeedStaffCastMoveSpeed => Speed_Config.SpeedStaffCastMoveSpeed;
        public static ConfigEntry<float> SpeedStaffCastEitrReduction => Speed_Config.SpeedStaffCastEitrReduction;
        public static ConfigEntry<float> SpeedEx1MeleeSkill => Speed_Config.SpeedEx1MeleeSkill;
        public static ConfigEntry<float> SpeedEx1CrossbowSkill => Speed_Config.SpeedEx1CrossbowSkill;
        public static ConfigEntry<float> SpeedEx2StaffSkill => Speed_Config.SpeedEx2StaffSkill;
        public static ConfigEntry<float> SpeedEx2BowSkill => Speed_Config.SpeedEx2BowSkill;
        public static ConfigEntry<float> SpeedFoodEfficiency => Speed_Config.SpeedFoodEfficiency;
        public static ConfigEntry<float> SpeedShipBonus => Speed_Config.SpeedShipBonus;
        public static ConfigEntry<float> JumpSkillLevelBonus => Speed_Config.JumpSkillLevelBonus;
        public static ConfigEntry<float> JumpStaminaReduction => Speed_Config.JumpStaminaReduction;
        public static ConfigEntry<float> SpeedDexterityAttackSpeedBonus => Speed_Config.SpeedDexterityAttackSpeedBonus;
        public static ConfigEntry<float> SpeedDexterityMoveSpeedBonus => Speed_Config.SpeedDexterityMoveSpeedBonus;
        public static ConfigEntry<float> SpeedEnduranceStaminaBonus => Speed_Config.SpeedEnduranceStaminaBonus;
        public static ConfigEntry<float> SpeedIntellectEitrBonus => Speed_Config.SpeedIntellectEitrBonus;
        public static ConfigEntry<float> AllMasterRunSkill => Speed_Config.AllMasterRunSkill;
        public static ConfigEntry<float> AllMasterJumpSkill => Speed_Config.AllMasterJumpSkill;
        public static ConfigEntry<float> SpeedMeleeAttackSpeed => Speed_Config.SpeedMeleeAttackSpeed;
        public static ConfigEntry<float> SpeedMeleeComboTripleBonus => Speed_Config.SpeedMeleeComboTripleBonus;
        public static ConfigEntry<float> SpeedCrossbowDrawSpeed => Speed_Config.SpeedCrossbowDrawSpeed;
        public static ConfigEntry<float> SpeedCrossbowReloadMoveSpeed => Speed_Config.SpeedCrossbowReloadMoveSpeed;
        public static ConfigEntry<float> SpeedBowDrawSpeed => Speed_Config.SpeedBowDrawSpeed;
        public static ConfigEntry<float> SpeedBowDrawMoveSpeed => Speed_Config.SpeedBowDrawMoveSpeed;
        public static ConfigEntry<float> SpeedStaffCastSpeedFinal => Speed_Config.SpeedStaffCastSpeedFinal;
        public static ConfigEntry<float> SpeedStaffTripleEitrRecovery => Speed_Config.SpeedStaffTripleEitrRecovery;
        public static ConfigEntry<float> SpeedBaseAttackSpeed => Speed_Config.SpeedBaseAttackSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeSpeed => Speed_Config.SpeedBaseDodgeSpeed;
        public static ConfigEntry<float> SpeedMeleeComboSpeed => Speed_Config.SpeedMeleeComboSpeed;
        public static ConfigEntry<float> SpeedBowExpertDuration => Speed_Config.SpeedBowExpertDuration;
        public static ConfigEntry<float> SpeedStaffCastSpeed => Speed_Config.SpeedStaffCastSpeed;

        public static float SpeedRootMoveSpeedValue => Speed_Config.SpeedRootMoveSpeedValue;
        public static float SpeedBaseDodgeMoveSpeedValue => Speed_Config.SpeedBaseDodgeMoveSpeedValue;
        public static float SpeedBaseDodgeDurationValue => Speed_Config.SpeedBaseDodgeDurationValue;
        public static float SpeedMeleeComboAttackSpeedValue => Speed_Config.SpeedMeleeComboAttackSpeedValue;
        public static float SpeedMeleeComboStaminaValue => Speed_Config.SpeedMeleeComboStaminaValue;
        public static float SpeedMeleeComboDurationValue => Speed_Config.SpeedMeleeComboDurationValue;
        public static float SpeedCrossbowExpertSpeedValue => Speed_Config.SpeedCrossbowExpertSpeedValue;
        public static float SpeedCrossbowExpertDurationValue => Speed_Config.SpeedCrossbowExpertDurationValue;
        public static float SpeedCrossbowExpertReloadValue => Speed_Config.SpeedCrossbowExpertReloadValue;
        public static float SpeedBowExpertStaminaValue => Speed_Config.SpeedBowExpertStaminaValue;
        public static float SpeedBowExpertDrawSpeedValue => Speed_Config.SpeedBowExpertDrawSpeedValue;
        public static float SpeedStaffCastMoveSpeedValue => Speed_Config.SpeedStaffCastMoveSpeedValue;
        public static float SpeedStaffCastEitrReductionValue => Speed_Config.SpeedStaffCastEitrReductionValue;
        public static float SpeedEx1MeleeSkillValue => Speed_Config.SpeedEx1MeleeSkillValue;
        public static float SpeedEx1CrossbowSkillValue => Speed_Config.SpeedEx1CrossbowSkillValue;
        public static float SpeedEx2StaffSkillValue => Speed_Config.SpeedEx2StaffSkillValue;
        public static float SpeedEx2BowSkillValue => Speed_Config.SpeedEx2BowSkillValue;
        public static float SpeedFoodEfficiencyValue => Speed_Config.SpeedFoodEfficiencyValue;
        public static float SpeedShipBonusValue => Speed_Config.SpeedShipBonusValue;
        public static float JumpSkillLevelBonusValue => Speed_Config.JumpSkillLevelBonusValue;
        public static float JumpStaminaReductionValue => Speed_Config.JumpStaminaReductionValue;
        public static float SpeedDexterityAttackSpeedBonusValue => Speed_Config.SpeedDexterityAttackSpeedBonusValue;
        public static float SpeedDexterityMoveSpeedBonusValue => Speed_Config.SpeedDexterityMoveSpeedBonusValue;
        public static float SpeedEnduranceStaminaBonusValue => Speed_Config.SpeedEnduranceStaminaBonusValue;
        public static float SpeedIntellectEitrBonusValue => Speed_Config.SpeedIntellectEitrBonusValue;
        public static float AllMasterRunSkillValue => Speed_Config.AllMasterRunSkillValue;
        public static float AllMasterJumpSkillValue => Speed_Config.AllMasterJumpSkillValue;
        public static float SpeedMeleeAttackSpeedValue => Speed_Config.SpeedMeleeAttackSpeedValue;
        public static float SpeedMeleeComboTripleBonusValue => Speed_Config.SpeedMeleeComboTripleBonusValue;
        public static float SpeedCrossbowDrawSpeedValue => Speed_Config.SpeedCrossbowDrawSpeedValue;
        public static float SpeedCrossbowReloadMoveSpeedValue => Speed_Config.SpeedCrossbowReloadMoveSpeedValue;
        public static float SpeedBowDrawSpeedValue => Speed_Config.SpeedBowDrawSpeedValue;
        public static float SpeedBowDrawMoveSpeedValue => Speed_Config.SpeedBowDrawMoveSpeedValue;
        public static float SpeedStaffCastSpeedFinalValue => Speed_Config.SpeedStaffCastSpeedFinalValue;
        public static float SpeedStaffTripleEitrRecoveryValue => Speed_Config.SpeedStaffTripleEitrRecoveryValue;
        public static float SpeedBaseAttackSpeedValue => Speed_Config.SpeedBaseAttackSpeedValue;
        public static float SpeedBaseDodgeSpeedValue => Speed_Config.SpeedBaseDodgeSpeedValue;
        public static float SpeedMeleeComboSpeedValue => Speed_Config.SpeedMeleeComboSpeedValue;
        public static float SpeedBowExpertDurationValue => Speed_Config.SpeedBowExpertDurationValue;
        public static float SpeedStaffCastSpeedValue => Speed_Config.SpeedStaffCastSpeedValue;
        public static float SpeedMeleeComboBonusValue => Speed_Config.SpeedMeleeComboBonusValue;
        public static float SpeedCrossbowReloadSpeedValue => Speed_Config.SpeedCrossbowReloadSpeedValue;
        public static float SpeedBowHitBonusValue => Speed_Config.SpeedBowHitBonusValue;
        public static float SpeedBowHitDurationValue => Speed_Config.SpeedBowHitDurationValue;

        #endregion

        #region === Proxy: 활 전문가 (Bow_Config) ===

        public static ConfigEntry<float> BowMultishotLv1Chance => Bow_Config.BowMultishotLv1Chance;
        public static ConfigEntry<float> BowMultishotLv2Chance => Bow_Config.BowMultishotLv2Chance;
        public static ConfigEntry<int> BowMultishotArrowCount => Bow_Config.BowMultishotArrowCount;
        public static ConfigEntry<int> BowMultishotArrowConsumption => Bow_Config.BowMultishotArrowConsumption;
        public static ConfigEntry<float> BowMultishotDamagePercent => Bow_Config.BowMultishotDamagePercent;
        public static ConfigEntry<float> BowStep1ExpertDamageBonus => Bow_Config.BowStep1ExpertDamageBonus;
        public static ConfigEntry<float> BowStep2FocusCritBonus => Bow_Config.BowStep2FocusCritBonus;
        public static ConfigEntry<float> BowStep3SpeedShotSkillBonus => Bow_Config.BowStep3SpeedShotSkillBonus;
        public static ConfigEntry<float> BowStep3SilentShotDamageBonus => Bow_Config.BowStep3SilentShotDamageBonus;
        public static ConfigEntry<float> BowStep3SpecialArrowChance => Bow_Config.BowStep3SpecialArrowChance;
        public static ConfigEntry<float> BowStep5InstinctCritBonus => Bow_Config.BowStep5InstinctCritBonus;
        public static ConfigEntry<float> BowStep5MasterCritDamage => Bow_Config.BowStep5MasterCritDamage;
        public static ConfigEntry<float> BowExplosiveArrowDamage => Bow_Config.BowExplosiveArrowDamage;
        public static ConfigEntry<float> BowExplosiveArrowCooldown => Bow_Config.BowExplosiveArrowCooldown;
        public static ConfigEntry<float> BowExplosiveArrowStaminaCost => Bow_Config.BowExplosiveArrowStaminaCost;
        public static ConfigEntry<float> BowExplosiveArrowRadius => Bow_Config.BowExplosiveArrowRadius;

        public static float BowMultishotLv1ChanceValue => Bow_Config.BowMultishotLv1ChanceValue;
        public static float BowMultishotLv2ChanceValue => Bow_Config.BowMultishotLv2ChanceValue;
        public static int BowMultishotArrowCountValue => Bow_Config.BowMultishotArrowCountValue;
        public static int BowMultishotArrowConsumptionValue => Bow_Config.BowMultishotArrowConsumptionValue;
        public static float BowMultishotDamagePercentValue => Bow_Config.BowMultishotDamagePercentValue;
        public static float BowStep1ExpertDamageBonusValue => Bow_Config.BowStep1ExpertDamageBonusValue;
        public static float BowStep2FocusCritBonusValue => Bow_Config.BowStep2FocusCritBonusValue;
        public static float BowStep3SpeedShotSkillBonusValue => Bow_Config.BowStep3SpeedShotSkillBonusValue;
        public static float BowStep3SilentShotDamageBonusValue => Bow_Config.BowStep3SilentShotDamageBonusValue;
        public static float BowStep3SpecialArrowChanceValue => Bow_Config.BowStep3SpecialArrowChanceValue;
        public static float BowStep5InstinctCritBonusValue => Bow_Config.BowStep5InstinctCritBonusValue;
        public static float BowStep5MasterCritDamageValue => Bow_Config.BowStep5MasterCritDamageValue;
        public static float BowExplosiveArrowDamageValue => Bow_Config.BowExplosiveArrowDamageValue;
        public static float BowExplosiveArrowCooldownValue => Bow_Config.BowExplosiveArrowCooldownValue;
        public static float BowExplosiveArrowStaminaCostValue => Bow_Config.BowExplosiveArrowStaminaCostValue;
        public static float BowExplosiveArrowRadiusValue => Bow_Config.BowExplosiveArrowRadiusValue;

        #endregion

        #region === Proxy: 창 전문가 (Spear_Config) ===

        public static ConfigEntry<float> SpearStep1AttackSpeed => Spear_Config.SpearStep1AttackSpeed;
        public static ConfigEntry<float> SpearStep1DamageBonus => Spear_Config.SpearStep1DamageBonus;
        public static ConfigEntry<float> SpearStep1Duration => Spear_Config.SpearStep1Duration;
        public static ConfigEntry<float> SpearStep1ThrowCooldown => Spear_Config.SpearStep1ThrowCooldown;
        public static ConfigEntry<float> SpearStep1ThrowDamage => Spear_Config.SpearStep1ThrowDamage;
        public static ConfigEntry<float> SpearStep1ThrowBuffDuration => Spear_Config.SpearStep1ThrowBuffDuration;
        public static ConfigEntry<float> SpearStep1CritDamageBonus => Spear_Config.SpearStep1CritDamageBonus;
        public static ConfigEntry<float> SpearStep2EvasionDamageBonus => Spear_Config.SpearStep2EvasionDamageBonus;
        public static ConfigEntry<float> SpearStep3PierceDamageBonus => Spear_Config.SpearStep3PierceDamageBonus;
        // 폭발창
        public static ConfigEntry<float> SpearExplosionChance => Spear_Config.SpearExplosionChance;
        public static ConfigEntry<float> SpearExplosionRadius => Spear_Config.SpearExplosionRadius;
        public static ConfigEntry<float> SpearExplosionDamageBonus => Spear_Config.SpearExplosionDamageBonus;
        // 이연창
        public static ConfigEntry<float> SpearDualDamageBonus => Spear_Config.SpearDualDamageBonus;
        public static ConfigEntry<float> SpearDualDuration => Spear_Config.SpearDualDuration;
        public static ConfigEntry<float> SpearStep5PenetrateCritChance => Spear_Config.SpearStep5PenetrateCritChance;
        public static ConfigEntry<float> SpearStep5ComboCooldown => Spear_Config.SpearStep5ComboCooldown;
        public static ConfigEntry<float> SpearStep5ComboDamage => Spear_Config.SpearStep5ComboDamage;
        public static ConfigEntry<float> SpearStep5ComboStaminaCost => Spear_Config.SpearStep5ComboStaminaCost;
        public static ConfigEntry<float> SpearStep5ComboKnockbackRadius => Spear_Config.SpearStep5ComboKnockbackRadius;
        public static ConfigEntry<float> SpearStep5ComboRange => Spear_Config.SpearStep5ComboRange;

        public static float SpearStep1AttackSpeedValue => Spear_Config.SpearStep1AttackSpeedValue;
        public static float SpearStep1DamageBonusValue => Spear_Config.SpearStep1DamageBonusValue;
        public static float SpearStep1DurationValue => Spear_Config.SpearStep1DurationValue;
        public static float SpearStep2ThrowCooldownValue => Spear_Config.SpearStep2ThrowCooldownValue;
        public static float SpearStep2ThrowDamageValue => Spear_Config.SpearStep2ThrowDamageValue;
        public static float SpearStep2ThrowBuffDurationValue => Spear_Config.SpearStep2ThrowBuffDurationValue;
        public static float SpearStep2CritDamageBonusValue => Spear_Config.SpearStep2CritDamageBonusValue;
        public static float SpearStep3EvasionDamageBonusValue => Spear_Config.SpearStep3EvasionDamageBonusValue;
        public static float SpearStep3PierceDamageBonusValue => Spear_Config.SpearStep3PierceDamageBonusValue;
        // 폭발창 Value
        public static float SpearExplosionChanceValue => Spear_Config.SpearExplosionChanceValue;
        public static float SpearExplosionRadiusValue => Spear_Config.SpearExplosionRadiusValue;
        public static float SpearExplosionDamageBonusValue => Spear_Config.SpearExplosionDamageBonusValue;
        // 이연창 Value
        public static float SpearDualDamageBonusValue => Spear_Config.SpearDualDamageBonusValue;
        public static float SpearDualDurationValue => Spear_Config.SpearDualDurationValue;
        public static float SpearStep6PenetrateCritChanceValue => Spear_Config.SpearStep6PenetrateCritChanceValue;
        public static float SpearStep6ComboCooldownValue => Spear_Config.SpearStep6ComboCooldownValue;
        public static float SpearStep6ComboDamageValue => Spear_Config.SpearStep6ComboDamageValue;
        public static float SpearStep6ComboStaminaCostValue => Spear_Config.SpearStep6ComboStaminaCostValue;
        public static float SpearStep6ComboKnockbackRadiusValue => Spear_Config.SpearStep6ComboKnockbackRadiusValue;
        public static float SpearStep2ThrowRangeValue => Spear_Config.SpearStep2ThrowRangeValue;
        public static float SpearStep2ThrowStaminaCostValue => Spear_Config.SpearStep2ThrowStaminaCostValue;

        #endregion

        #region === Proxy: 폴암 전문가 (Polearm_Config) ===

        public static ConfigEntry<float> PolearmExpertRangeBonus => Polearm_Config.PolearmExpertRangeBonus;
        public static ConfigEntry<float> PolearmStep1SpinWheelDamage => Polearm_Config.PolearmStep1SpinWheelDamage;
        public static ConfigEntry<float> PolearmStep1SuppressDamage => Polearm_Config.PolearmStep1SuppressDamage;
        public static ConfigEntry<float> PolearmStep2HeroKnockbackChance => Polearm_Config.PolearmStep2HeroKnockbackChance;
        public static ConfigEntry<float> PolearmStep3AreaComboBonus => Polearm_Config.PolearmStep3AreaComboBonus;
        public static ConfigEntry<float> PolearmStep3AreaComboDuration => Polearm_Config.PolearmStep3AreaComboDuration;
        public static ConfigEntry<float> PolearmStep3GroundWheelDamage => Polearm_Config.PolearmStep3GroundWheelDamage;
        public static ConfigEntry<float> PolearmStep4MoonRangeBonus => Polearm_Config.PolearmStep4MoonRangeBonus;
        public static ConfigEntry<float> PolearmStep4MoonStaminaReduction => Polearm_Config.PolearmStep4MoonStaminaReduction;
        public static ConfigEntry<float> PolearmStep4ChargeDamageBonus => Polearm_Config.PolearmStep4ChargeDamageBonus;
        // 관통 돌격 Config 프록시
        public static ConfigEntry<float> PolearmPierceChargeDashDistance => Polearm_Config.PolearmPierceChargeDashDistance;
        public static ConfigEntry<float> PolearmPierceChargePrimaryDamage => Polearm_Config.PolearmPierceChargePrimaryDamage;
        public static ConfigEntry<float> PolearmPierceChargeAoeDamage => Polearm_Config.PolearmPierceChargeAoeDamage;
        public static ConfigEntry<float> PolearmPierceChargeAoeAngle => Polearm_Config.PolearmPierceChargeAoeAngle;
        public static ConfigEntry<float> PolearmPierceChargeAoeRadius => Polearm_Config.PolearmPierceChargeAoeRadius;
        public static ConfigEntry<float> PolearmPierceChargeKnockbackDistance => Polearm_Config.PolearmPierceChargeKnockbackDistance;
        public static ConfigEntry<float> PolearmPierceChargeStaminaCost => Polearm_Config.PolearmPierceChargeStaminaCost;
        public static ConfigEntry<float> PolearmPierceChargeCooldown => Polearm_Config.PolearmPierceChargeCooldown;

        public static float PolearmExpertRangeBonusValue => Polearm_Config.PolearmExpertRangeBonusValue;
        public static float PolearmStep1SpinWheelDamageValue => Polearm_Config.PolearmStep1SpinWheelDamageValue;
        public static float PolearmStep1SuppressDamageValue => Polearm_Config.PolearmStep1SuppressDamageValue;
        public static float PolearmStep2HeroKnockbackChanceValue => Polearm_Config.PolearmStep2HeroKnockbackChanceValue;
        public static float PolearmStep3AreaComboBonusValue => Polearm_Config.PolearmStep3AreaComboBonusValue;
        public static float PolearmStep3AreaComboDurationValue => Polearm_Config.PolearmStep3AreaComboDurationValue;
        public static float PolearmStep3GroundWheelDamageValue => Polearm_Config.PolearmStep3GroundWheelDamageValue;
        public static float PolearmStep4MoonRangeBonusValue => Polearm_Config.PolearmStep4MoonRangeBonusValue;
        public static float PolearmStep4MoonStaminaReductionValue => Polearm_Config.PolearmStep4MoonStaminaReductionValue;
        public static float PolearmStep4ChargeDamageBonusValue => Polearm_Config.PolearmStep4ChargeDamageBonusValue;
        // 관통 돌격 Value 프록시
        public static float PolearmPierceChargeDashDistanceValue => Polearm_Config.PolearmPierceChargeDashDistanceValue;
        public static float PolearmPierceChargePrimaryDamageValue => Polearm_Config.PolearmPierceChargePrimaryDamageValue;
        public static float PolearmPierceChargeAoeDamageValue => Polearm_Config.PolearmPierceChargeAoeDamageValue;
        public static float PolearmPierceChargeAoeAngleValue => Polearm_Config.PolearmPierceChargeAoeAngleValue;
        public static float PolearmPierceChargeAoeRadiusValue => Polearm_Config.PolearmPierceChargeAoeRadiusValue;
        public static float PolearmPierceChargeKnockbackDistanceValue => Polearm_Config.PolearmPierceChargeKnockbackDistanceValue;
        public static float PolearmPierceChargeStaminaCostValue => Polearm_Config.PolearmPierceChargeStaminaCostValue;
        public static float PolearmPierceChargeCooldownValue => Polearm_Config.PolearmPierceChargeCooldownValue;

        #endregion

        #region === Initialize ===

        public static void Initialize(ConfigFile config)
        {
            // === STEP 1: 언어 감지 (Config Manager 로컬라이제이션용) ===
            _detectedConfigLanguage = DetectConfigLanguage();
            Plugin.Log.LogInfo($"[SkillTreeConfig] Config Manager language detected: {_detectedConfigLanguage}");

            DetectServerClientMode();

            // === Skill_Tree_Base: 핵심 설정 ===
            Language = config.Bind(
                "Skill_Tree_Base",
                "Language",
                "Auto",
                new ConfigDescription(
                    "Language setting:\n" +
                    "  - 'Auto' = Auto-detect from Valheim settings (Recommended)\n" +
                    "  - 'KR' = Korean\n" +
                    "  - 'EN' = English\n" +
                    "  - 'RU' = Russian\n\n" +
                    "⚠️ IMPORTANT: Game restart required after changing this setting!\n" +
                    "   Config Manager (F1) descriptions are set at game startup.\n\n" +
                    "⚠️ 중요: 이 설정 변경 후 게임 재시작이 필요합니다!\n" +
                    "   Config Manager (F1) 설명은 게임 시작 시 설정됩니다.",
                    new AcceptableValueList<string>("Auto", "KR", "EN", "RU")
                )
            );

            // Language 변경 감지 - 재시작 안내
            Language.SettingChanged += (sender, args) =>
            {
                string newLang = Language.Value;
                Plugin.Log.LogWarning("========================================");
                Plugin.Log.LogWarning("[SkillTreeConfig] Language changed to: " + newLang);
                Plugin.Log.LogWarning("⚠️ GAME RESTART REQUIRED for Config Manager (F1) to update!");
                Plugin.Log.LogWarning("⚠️ 게임 재시작이 필요합니다 (F1 메뉴 업데이트)!");
                Plugin.Log.LogWarning("========================================");

                // UI 언어는 즉시 변경 (스킬트리 UI)
                Localization.LocalizationManager.ReloadLanguage();
            };

            MoveSpeedMaxBonus = config.Bind(
                "Skill_Tree_Base",
                "MoveSpeed_MaxBonus",
                70f,
                new ConfigDescription(
                    "Maximum move speed bonus from skill tree (%) - 스킬트리 이동속도 최대 보너스 (%)",
                    new AcceptableValueRange<float>(0f, 200f),
                    new ConfigurationManagerAttributes { IsAdminOnly = true }
                )
            );

            AttackSpeedMaxBonus = config.Bind(
                "Skill_Tree_Base",
                "AttackSpeed_MaxBonus",
                70f,
                new ConfigDescription(
                    "Maximum attack speed bonus from skill tree (%) - 스킬트리 공격속도 최대 보너스 (%)",
                    new AcceptableValueRange<float>(0f, 200f),
                    new ConfigurationManagerAttributes { IsAdminOnly = true }
                )
            );

            var keyAcceptable = new AcceptableValueList<string>("Y", "R", "G", "H", "Z", "X", "C", "V", "F", "Q", "E", "T", "U", "I", "O", "P");

            HotKeyY = config.Bind(
                "Skill_Tree_Base",
                "HotKey_Y",
                "Y",
                new ConfigDescription(
                    GetConfigDescription("HotKey_Y"),
                    keyAcceptable,
                    new ConfigurationManagerAttributes { IsAdminOnly = false, DispName = GetLocalizedKeyName("HotKey_Y"), Order = -10 }
                )
            );

            HotKeyR = config.Bind(
                "Skill_Tree_Base",
                "HotKey_R",
                "R",
                new ConfigDescription(
                    GetConfigDescription("HotKey_R"),
                    keyAcceptable,
                    new ConfigurationManagerAttributes { IsAdminOnly = false, DispName = GetLocalizedKeyName("HotKey_R"), Order = -11 }
                )
            );

            HotKeyG = config.Bind(
                "Skill_Tree_Base",
                "HotKey_G",
                "G",
                new ConfigDescription(
                    GetConfigDescription("HotKey_G"),
                    keyAcceptable,
                    new ConfigurationManagerAttributes { IsAdminOnly = false, DispName = GetLocalizedKeyName("HotKey_G"), Order = -12 }
                )
            );

            HotKeyH = config.Bind(
                "Skill_Tree_Base",
                "HotKey_H",
                "H",
                new ConfigDescription(
                    GetConfigDescription("HotKey_H"),
                    keyAcceptable,
                    new ConfigurationManagerAttributes { IsAdminOnly = false, DispName = GetLocalizedKeyName("HotKey_H"), Order = -13 }
                )
            );

            HudPosX = config.Bind(
                "Skill_Tree_Base",
                "HUD_PosX",
                315,
                new ConfigDescription(
                    GetConfigDescription("HUD_PosX"),
                    new AcceptableValueRange<int>(0, 1920),
                    new ConfigurationManagerAttributes { IsAdminOnly = false, DispName = GetLocalizedKeyName("HUD_PosX"), Order = -14 }
                )
            );

            HudPosY = config.Bind(
                "Skill_Tree_Base",
                "HUD_PosY",
                110,
                new ConfigDescription(
                    GetConfigDescription("HUD_PosY"),
                    new AcceptableValueRange<int>(0, 1080),
                    new ConfigurationManagerAttributes { IsAdminOnly = false, DispName = GetLocalizedKeyName("HUD_PosY"), Order = -15 }
                )
            );

            Plugin.Log.LogDebug("[SkillTreeConfig] Skill_Tree_Base 설정 초기화 완료");

            // 1. 전문가 트리 (Attack → Speed → Defense → Product 순)
            BindServerSync(config, "─────── Atk, Spd, Prod, Def ───────", "End", "", "");
            Attack_Config.Initialize(config);   // Attack Tree (공격 전문가)
            Speed_Config.Initialize(config);    // Speed Tree (속도 전문가)
            Defense_Config.Initialize(config);  // Defense Tree (방어 전문가)
            Production_Config.Initialize(config);  // Product Tree (생산 전문가)

            // 2. 원거리 무기 트리 (Bow → Staff → Crossbow 순)
            BindServerSync(config, "─────── Ranged Expert Trees ───────", "End", "", "");
            Bow_Config.Initialize(config);                      // Bow Tree (활)
            Staff_Config.InitConfig(config);                    // Staff Tree (지팡이)
            Crossbow_Config.InitializeCrossbowConfig(config);   // Crossbow Tree (석궁)

            // 3. 근접 무기 트리 (Knife → Sword → Mace → Spear → Polearm 순)
            BindServerSync(config, "─────── Melee Expert Trees ────────", "End", "", "");
            Knife_Config.InitializeKnifeConfig(config); // Knife Tree (단검)
            Sword_Config.Initialize(config);            // Sword Tree (검)
            Mace_Config.Initialize(config);             // Mace Tree (둔기)
            Spear_Config.Initialize(config);            // Spear Tree (창)
            Polearm_Config.Initialize(config);          // Polearm Tree (폴암)

            // 4. 직업 트리 (최하단 배치)
            BindServerSync(config, "───────── Job Skill Trees ─────────", "End", "", "");
            Archer_Config.InitializeArcherConfig(config);       // Archer (궁수)
            Mage_Config.InitializeMageConfig(config);           // Mage (마법사)
            Tanker_Config.InitializeTankerConfig(config);       // Tanker (탱커)
            Rogue_Config.InitializeRogueConfig(config);         // Rogue (로그)
            Paladin_Config.InitializePaladinConfig();           // Paladin (성기사)
            Berserker_Config.InitializeBerserkerConfig();       // Berserker (광전사)

            _configFile = config;

            // === Config 변경 이벤트 등록 ===
            RegisterConfigChangeEvents();

            if (_isServer)
            {
                // ❌ 초기화 단계에서는 BroadcastConfigToClients() 호출 제거
                // ZRoutedRpc.instance가 아직 null일 수 있음
                // Plugin.Patches.cs의 DelayedConfigBroadcast()에서 2초 후 안전하게 호출됨
                StartConfigFileWatcher(config);
            }

            InitializeJotunnSyncEvents();
        }

        /// <summary>
        /// Config 변경 시 경고 상태 초기화 및 효과 갱신
        /// </summary>
        private static void RegisterConfigChangeEvents()
        {
            try
            {
                // 이동속도 최대치 변경 시
                MoveSpeedMaxBonus.SettingChanged += (sender, args) =>
                {
                    Plugin.Log.LogInfo($"[Config] 이동속도 최대치 변경: {MoveSpeedMaxBonus.Value}%");

                    // 모든 플레이어의 경고 상태 초기화
                    if (Player.m_localPlayer != null)
                    {
                        ImprovedMoveSpeedPatch.ClearWarningState(Player.m_localPlayer);
                    }
                };

                // 공격속도 최대치 변경 시
                AttackSpeedMaxBonus.SettingChanged += (sender, args) =>
                {
                    Plugin.Log.LogInfo($"[Config] 공격속도 최대치 변경: {AttackSpeedMaxBonus.Value}%");

                    // 모든 플레이어의 경고 상태 초기화
                    if (Player.m_localPlayer != null)
                    {
                        CaptainSkillTree.AttackSpeedHandler_Game_Awake_Patch.ClearAttackSpeedWarningState(Player.m_localPlayer);
                    }
                };

                Plugin.Log.LogDebug("[SkillTreeConfig] Config 변경 이벤트 등록 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeConfig] Config 변경 이벤트 등록 실패: {ex.Message}");
            }
        }


        #endregion

        #region === Server Sync Methods ===

        private static void InitializeJotunnSyncEvents()
        {
            try
            {
                SynchronizationManager.OnConfigurationSynchronized += (sender, args) =>
                {
                    if (args.InitialSynchronization)
                    {
                        Plugin.Log.LogDebug("[SkillTreeConfig] Jotunn 서버 설정 초기 동기화 완료");
                        _hasReceivedServerConfig = true;
                        try { RefreshAllSkillEffects(); }
                        catch (Exception ex) { Plugin.Log.LogWarning($"[SkillTreeConfig] 스킬 효과 재계산 중 오류: {ex.Message}"); }
                    }
                };

                SynchronizationManager.OnAdminStatusChanged += () =>
                {
                    Plugin.Log.LogInfo($"[SkillTreeConfig] Admin 상태 변경: {(SynchronizationManager.Instance.PlayerIsAdmin ? "관리자" : "일반 사용자")}");
                };
            }
            catch (Exception ex) { Plugin.Log.LogWarning($"[SkillTreeConfig] Jotunn 동기화 이벤트 등록 실패: {ex.Message}"); }
        }

        private static void RefreshAllSkillEffects()
        {
            if (Player.m_localPlayer == null) return;
        }

        private static void DetectServerClientMode()
        {
            _isServer = ZNet.instance == null || ZNet.instance.IsServer();
            Plugin.Log.LogDebug($"[SkillTreeConfig] 모드 감지: {(_isServer ? "서버" : "클라이언트")}");
        }

        public static float GetEffectiveValue(string key, float localValue)
        {
            if (!_isServer && _hasReceivedServerConfig && _serverConfigValues.ContainsKey(key))
                return _serverConfigValues[key];
            return localValue;
        }

        /// <summary>
        /// Get localized config description for Config Manager (F1 menu)
        /// Uses ConfigTranslations.cs instead of DefaultLanguages.cs
        /// </summary>
        /// <param name="configKey">Config key (e.g., "Tier0_SwordExpert_RequiredPoints")</param>
        /// <returns>Localized description string</returns>
        public static string GetConfigDescription(string configKey)
        {
            if (string.IsNullOrEmpty(configKey))
                return "";

            try
            {
                // Use GetLocalizedDescription which queries ConfigTranslations.cs
                // This is for Config Manager (F1 menu) display, not in-game UI
                string result = GetLocalizedDescription(configKey);

                // If not found in ConfigTranslations, return readable fallback
                if (result == configKey)
                {
                    // Convert key to readable format: "Tier0_SwordExpert_RequiredPoints" -> "Tier 0: Sword Expert - Required Points"
                    return configKey.Replace("_", " ");
                }

                return result;
            }
            catch
            {
                // If any error occurs, return the config key as fallback
                return configKey.Replace("_", " ");
            }
        }

        public static void BroadcastConfigToClients()
        {
            if (!_isServer) return;

            try
            {
                var configData = new Dictionary<string, float>
                {
                    // Attack Tree
                    ["Attack_Expert_Damage"] = Attack_Config.AttackRootDamageBonus.Value,
                    ["Attack_Step2_MeleeBonusChance"] = Attack_Config.AttackMeleeBonusChance.Value,
                    ["Attack_Step2_MeleeBonusDamage"] = Attack_Config.AttackMeleeBonusDamage.Value,
                    ["Attack_Step4_CritChance"] = Attack_Config.AttackCritChance.Value,

                    // Speed Tree
                    ["Speed_Expert_MoveSpeed"] = Speed_Config.SpeedRootMoveSpeed.Value,
                    ["Speed_Step8_MeleeAttackSpeed"] = Speed_Config.SpeedMeleeAttackSpeed.Value,
                    ["Speed_Step8_BowDrawSpeed"] = Speed_Config.SpeedBowDrawSpeed.Value,

                    // Bow Tree
                    ["Bow_MultiShot_Lv1_Chance"] = Bow_Config.BowMultishotLv1Chance.Value,
                    ["Bow_MultiShot_Lv2_Chance"] = Bow_Config.BowMultishotLv2Chance.Value,

                    // Spear Tree
                    ["spear_Step1_attack_speed"] = Spear_Config.SpearStep1AttackSpeed.Value,
                    ["spear_Step1_damage_bonus"] = Spear_Config.SpearStep1DamageBonus.Value,

                    // Polearm Tree
                    ["polearm_step4_charge_damage"] = Polearm_Config.PolearmStep4ChargeDamageBonus.Value,

                    // Defense Tree
                    ["Defense_Stomp_Radius"] = Defense_Config.StompRadius.Value,
                    ["Defense_Stomp_Cooldown"] = Defense_Config.StompCooldown.Value,

                    // Sword Tree
                    ["sword_expert_damage"] = Sword_Config.SwordExpertDamageBonus.Value,
                };

                var configString = SerializeConfigData(configData);
                if (ZNet.instance != null && ZRoutedRpc.instance != null)
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, "CaptainSkillTree.SkillTreeMod_ConfigSync", configString);
                    Plugin.Log.LogInfo("[SkillTreeConfig] 서버 설정을 모든 클라이언트에게 전송");
                }
                else
                {
                    Plugin.Log.LogWarning("[SkillTreeConfig] ZNet 또는 ZRoutedRpc가 아직 초기화되지 않아 Config 전송을 건너뜁니다.");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeConfig] BroadcastConfigToClients 실패: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private static string SerializeConfigData(Dictionary<string, float> configData)
        {
            var sb = new StringBuilder();
            foreach (var kvp in configData)
                sb.AppendLine($"{kvp.Key}={kvp.Value}");
            return sb.ToString();
        }

        private static Dictionary<string, float> DeserializeConfigData(string configString)
        {
            var result = new Dictionary<string, float>();
            if (string.IsNullOrEmpty(configString)) return result;
            var lines = configString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && float.TryParse(parts[1], out float value))
                    result[parts[0]] = value;
            }
            return result;
        }

        public static void ReceiveServerConfig(string configString)
        {
            try
            {
                var serverConfig = DeserializeConfigData(configString);
                if (serverConfig != null && serverConfig.Count > 0)
                {
                    _serverConfigValues = serverConfig;
                    _hasReceivedServerConfig = true;
                    var player = Player.m_localPlayer;
                    if (player != null) SkillEffect.UpdateDefenseDodgeRate(player);
                }
            }
            catch (Exception ex) { Plugin.Log.LogError($"[SkillTreeConfig] 서버 설정 수신 실패: {ex.Message}"); }
        }

        public static void ReloadAndBroadcast()
        {
            if (!_isServer) return;
            Plugin.Log.LogInfo("[SkillTreeConfig] 설정 리로드 및 재전송");
            BroadcastConfigToClients();
        }

        public static bool SetConfigValue(string key, float value)
        {
            if (!_isServer) return false;
            try
            {
                switch (key)
                {
                    case "Attack_Expert_Damage": Attack_Config.AttackRootDamageBonus.Value = value; break;
                    case "Speed_Expert_MoveSpeed": Speed_Config.SpeedRootMoveSpeed.Value = value; break;
                    case "sword_expert_damage": SwordExpertDamage.Value = value; break;
                    case "Defense_Stomp_Radius": Defense_Config.StompRadius.Value = value; break;
                    default: return false;
                }
                Plugin.Log.LogInfo($"[SkillTreeConfig] 설정 변경: {key} = {value}");
                BroadcastConfigToClients();
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeConfig] 설정 변경 실패: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region === Config File Watcher ===

        private static void StartConfigFileWatcher(ConfigFile config)
        {
            try
            {
                string configPath = config.ConfigFilePath;
                string configDirectory = Path.GetDirectoryName(configPath);
                string configFileName = Path.GetFileName(configPath);
                _configWatcher = new FileSystemWatcher(configDirectory, configFileName);
                _configWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                _configWatcher.EnableRaisingEvents = true;
                _configWatcher.Changed += OnConfigFileChanged;
            }
            catch (Exception ex) { Plugin.Log.LogError($"[SkillTreeConfig] Config 파일 감시 시작 실패: {ex.Message}"); }
        }

        private static void OnConfigFileChanged(object sender, FileSystemEventArgs e)
        {
            try { if (_isServer) BroadcastConfigToClients(); }
            catch (Exception ex) { Plugin.Log.LogError($"[SkillTreeConfig] Config 파일 변경 처리 실패: {ex.Message}"); }
        }

        public static void StopConfigFileWatcher()
        {
            if (_configWatcher != null)
            {
                _configWatcher.EnableRaisingEvents = false;
                _configWatcher.Changed -= OnConfigFileChanged;
                _configWatcher.Dispose();
                _configWatcher = null;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using System.IO;
using System.Text;
using Jotunn.Managers;

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

        #region === Config 바인드 헬퍼 메서드 ===

        public static ConfigEntry<float> BindServerSync(ConfigFile config, string section, string key, float defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
        }

        public static ConfigEntry<int> BindServerSync(ConfigFile config, string section, string key, int defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
        }

        public static ConfigEntry<bool> BindServerSync(ConfigFile config, string section, string key, bool defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
        }

        public static ConfigEntry<string> BindServerSync(ConfigFile config, string section, string key, string defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
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
        public static ConfigEntry<int> SwordSlashAttackCount;
        public static ConfigEntry<float> SwordSlashAttackInterval;
        public static ConfigEntry<float> SwordSlashDamageRatio;
        public static ConfigEntry<float> SwordSlashStaminaCost;
        public static ConfigEntry<float> SwordSlashCooldown;
        public static ConfigEntry<float> SwordStep5DefenseSwitchShieldReduction;
        public static ConfigEntry<float> SwordStep5DefenseSwitchNoShieldBonus;
        public static ConfigEntry<float> SwordStep6UltimateSlashMultiplier;

        // 검 전문가 접근 프로퍼티들
        public static float SwordExpertDamageValue => GetEffectiveValue("sword_expert_damage", SwordExpertDamage.Value);
        public static float SwordStep1ExpertComboBonusValue => GetEffectiveValue("sword_expert_combo_bonus", SwordStep1ExpertComboBonus.Value);
        public static float SwordStep1ExpertDurationValue => GetEffectiveValue("sword_expert_duration", SwordStep1ExpertDuration.Value);
        public static float SwordStep1FastSlashSpeedValue => GetEffectiveValue("sword_Step1_fast_slash_speed", SwordStep1FastSlashSpeed.Value);
        public static float SwordStep1CounterDefenseBonusValue => GetEffectiveValue("sword_Step1_counter_defense_bonus", SwordStep1CounterDefenseBonus.Value);
        public static float SwordStep1CounterDurationValue => GetEffectiveValue("sword_Step1_counter_duration", SwordStep1CounterDuration.Value);
        public static float SwordStep2ComboSlashBonusValue => GetEffectiveValue("sword_Step2_combo_slash_bonus", SwordStep2ComboSlashBonus.Value);
        public static float SwordStep2ComboSlashDurationValue => GetEffectiveValue("sword_Step2_combo_slash_duration", SwordStep2ComboSlashDuration.Value);
        public static float SwordStep3BladeCounterBonusValue => GetEffectiveValue("sword_Step3_blade_counter_bonus", SwordStep3BladeCounterBonus.Value);
        public static float SwordStep3BladeCounterDurationValue => GetEffectiveValue("sword_Step3_blade_counter_duration", SwordStep3BladeCounterDuration.Value);
        public static float SwordStep3OffenseDefenseAttackBonusValue => GetEffectiveValue("sword_Step3_offense_defense_attack_bonus", SwordStep3OffenseDefenseAttackBonus.Value);
        public static float SwordStep3OffenseDefenseDefenseBonusValue => GetEffectiveValue("sword_Step3_offense_defense_defense_bonus", SwordStep3OffenseDefenseDefenseBonus.Value);
        public static float SwordStep4TrueDuelSpeedValue => GetEffectiveValue("sword_Step4_true_duel_speed", SwordStep4TrueDuelSpeed.Value);
        public static int SwordSlashAttackCountValue => (int)GetEffectiveValue("sword_slash_attack_count", SwordSlashAttackCount.Value);
        public static float SwordSlashAttackIntervalValue => GetEffectiveValue("sword_slash_attack_interval", SwordSlashAttackInterval.Value);
        public static float SwordSlashDamageRatioValue => GetEffectiveValue("sword_slash_damage_ratio", SwordSlashDamageRatio.Value);
        public static float SwordSlashStaminaCostValue => GetEffectiveValue("sword_slash_stamina_cost", SwordSlashStaminaCost.Value);
        public static float SwordSlashCooldownValue => GetEffectiveValue("sword_slash_cooldown", SwordSlashCooldown.Value);
        public static float SwordStep5DefenseSwitchShieldReductionValue => GetEffectiveValue("sword_Step5_defense_switch_shield_reduction", SwordStep5DefenseSwitchShieldReduction.Value);
        public static float SwordStep5DefenseSwitchNoShieldBonusValue => GetEffectiveValue("sword_Step5_defense_switch_no_shield_bonus", SwordStep5DefenseSwitchNoShieldBonus.Value);
        public static float SwordStep6UltimateSlashMultiplierValue => GetEffectiveValue("sword_Step6_ultimate_slash_multiplier", SwordStep6UltimateSlashMultiplier.Value);

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
        public static ConfigEntry<float> AttackStatBonus => Attack_Config.AttackStatBonus;
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
        public static float AttackStatBonusValue => Attack_Config.AttackStatBonusValue;
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
        public static ConfigEntry<float> SpeedBaseMoveSpeed => Speed_Config.SpeedBaseMoveSpeed;
        public static ConfigEntry<float> SpeedBaseAttackSpeed => Speed_Config.SpeedBaseAttackSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeSpeed => Speed_Config.SpeedBaseDodgeSpeed;
        public static ConfigEntry<float> SpeedMeleeComboSpeed => Speed_Config.SpeedMeleeComboSpeed;
        public static ConfigEntry<float> SpeedBowExpertDuration => Speed_Config.SpeedBowExpertDuration;
        public static ConfigEntry<float> SpeedStaffCastSpeed => Speed_Config.SpeedStaffCastSpeed;
        public static ConfigEntry<float> SpeedCooldownReduction => Speed_Config.SpeedCooldownReduction;

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
        public static float SpeedBaseMoveSpeedValue => Speed_Config.SpeedBaseMoveSpeedValue;
        public static float SpeedBaseAttackSpeedValue => Speed_Config.SpeedBaseAttackSpeedValue;
        public static float SpeedBaseDodgeSpeedValue => Speed_Config.SpeedBaseDodgeSpeedValue;
        public static float SpeedMeleeComboSpeedValue => Speed_Config.SpeedMeleeComboSpeedValue;
        public static float SpeedBowExpertDurationValue => Speed_Config.SpeedBowExpertDurationValue;
        public static float SpeedStaffCastSpeedValue => Speed_Config.SpeedStaffCastSpeedValue;
        public static float SpeedCooldownReductionValue => Speed_Config.SpeedCooldownReductionValue;
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
        public static ConfigEntry<float> BowStep4PowerShotKnockbackChance => Bow_Config.BowStep4PowerShotKnockbackChance;
        public static ConfigEntry<float> BowStep4PowerShotKnockbackPower => Bow_Config.BowStep4PowerShotKnockbackPower;
        public static ConfigEntry<float> BowStep5InstinctCritBonus => Bow_Config.BowStep5InstinctCritBonus;
        public static ConfigEntry<float> BowStep5MasterCritDamage => Bow_Config.BowStep5MasterCritDamage;
        public static ConfigEntry<float> BowStep5ArrowRainChance => Bow_Config.BowStep5ArrowRainChance;
        public static ConfigEntry<int> BowStep5ArrowRainCount => Bow_Config.BowStep5ArrowRainCount;
        public static ConfigEntry<float> BowStep5BackstepShotCritBonus => Bow_Config.BowStep5BackstepShotCritBonus;
        public static ConfigEntry<float> BowStep5BackstepShotWindow => Bow_Config.BowStep5BackstepShotWindow;
        public static ConfigEntry<float> BowStep6CritBoostDamageBonus => Bow_Config.BowStep6CritBoostDamageBonus;
        public static ConfigEntry<float> BowStep6CritBoostCritChance => Bow_Config.BowStep6CritBoostCritChance;
        public static ConfigEntry<int> BowStep6CritBoostArrowCount => Bow_Config.BowStep6CritBoostArrowCount;
        public static ConfigEntry<float> BowStep6CritBoostCooldown => Bow_Config.BowStep6CritBoostCooldown;
        public static ConfigEntry<float> BowStep6CritBoostStaminaCost => Bow_Config.BowStep6CritBoostStaminaCost;
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
        public static float BowStep4PowerShotKnockbackChanceValue => Bow_Config.BowStep4PowerShotKnockbackChanceValue;
        public static float BowStep4PowerShotKnockbackPowerValue => Bow_Config.BowStep4PowerShotKnockbackPowerValue;
        public static float BowStep5ArrowRainChanceValue => Bow_Config.BowStep5ArrowRainChanceValue;
        public static int BowStep5ArrowRainCountValue => Bow_Config.BowStep5ArrowRainCountValue;
        public static float BowStep5BackstepShotCritBonusValue => Bow_Config.BowStep5BackstepShotCritBonusValue;
        public static float BowStep5BackstepShotWindowValue => Bow_Config.BowStep5BackstepShotWindowValue;
        public static float BowStep5InstinctCritBonusValue => Bow_Config.BowStep5InstinctCritBonusValue;
        public static float BowStep5MasterCritDamageValue => Bow_Config.BowStep5MasterCritDamageValue;
        public static float BowStep6CritBoostDamageBonusValue => Bow_Config.BowStep6CritBoostDamageBonusValue;
        public static float BowStep6CritBoostCritChanceValue => Bow_Config.BowStep6CritBoostCritChanceValue;
        public static int BowStep6CritBoostArrowCountValue => Bow_Config.BowStep6CritBoostArrowCountValue;
        public static float BowStep6CritBoostCooldownValue => Bow_Config.BowStep6CritBoostCooldownValue;
        public static float BowStep6CritBoostStaminaCostValue => Bow_Config.BowStep6CritBoostStaminaCostValue;
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
        public static ConfigEntry<float> SpearStep3QuickDamageBonus => Spear_Config.SpearStep3QuickDamageBonus;
        public static ConfigEntry<float> SpearStep4TripleDamageBonus => Spear_Config.SpearStep4TripleDamageBonus;
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
        public static float SpearStep4QuickDamageBonusValue => Spear_Config.SpearStep4QuickDamageBonusValue;
        public static float SpearStep5TripleDamageBonusValue => Spear_Config.SpearStep5TripleDamageBonusValue;
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
        public static ConfigEntry<float> PolearmStep5KingHealthThreshold => Polearm_Config.PolearmStep5KingHealthThreshold;
        public static ConfigEntry<float> PolearmStep5KingDamageBonus => Polearm_Config.PolearmStep5KingDamageBonus;
        public static ConfigEntry<float> PolearmStep5KingStaminaCost => Polearm_Config.PolearmStep5KingStaminaCost;
        public static ConfigEntry<float> PolearmStep5KingCooldown => Polearm_Config.PolearmStep5KingCooldown;
        public static ConfigEntry<float> PolearmStep5KingDuration => Polearm_Config.PolearmStep5KingDuration;

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
        public static float PolearmStep5KingHealthThresholdValue => Polearm_Config.PolearmStep5KingHealthThresholdValue;
        public static float PolearmStep5KingDamageBonusValue => Polearm_Config.PolearmStep5KingDamageBonusValue;
        public static float PolearmStep5KingStaminaCostValue => Polearm_Config.PolearmStep5KingStaminaCostValue;
        public static float PolearmStep5KingCooldownValue => Polearm_Config.PolearmStep5KingCooldownValue;
        public static float PolearmStep5KingDurationValue => Polearm_Config.PolearmStep5KingDurationValue;

        #endregion

        #region === Initialize ===

        public static void Initialize(ConfigFile config)
        {
            DetectServerClientMode();

            // === Config 초기화 (Speed → Bow → Staff → Crossbow 순) ===
            Speed_Config.Initialize(config);
            Bow_Config.Initialize(config);
            Staff_Config.InitConfig(config);
            Crossbow_Config.InitializeCrossbowConfig(config);

            // === 전문가 트리 Config 초기화 ===
            Attack_Config.Initialize(config);
            Defense_Config.Initialize(config);

            // === 근접 무기 Config 초기화 ===
            Knife_Config.InitializeKnifeConfig(config);
            Sword_Config.Initialize(config);
            InitializeSwordConfig(config);
            Mace_Config.Initialize(config);
            Spear_Config.Initialize(config);
            Polearm_Config.Initialize(config);

            // === 직업 스킬 Config 초기화 ===
            Archer_Config.InitializeArcherConfig(config);
            Mage_Config.InitializeMageConfig(config);
            Tanker_Config.InitializeTankerConfig(config);
            Rogue_Config.InitializeRogueConfig(config);
            Paladin_Config.InitializePaladinConfig();
            Berserker_Config.InitializeBerserkerConfig();

            _configFile = config;

            if (_isServer)
            {
                BroadcastConfigToClients();
                StartConfigFileWatcher(config);
            }

            InitializeJotunnSyncEvents();
        }

        private static void InitializeSwordConfig(ConfigFile config)
        {
            SwordExpertDamage = BindServerSync(config, "Sword Tree", "Tier0_검전문가_공격력보너스", 10f,
                "Tier 0: 검 전문가(sword_expert) - 기본 공격력 보너스 (%)");

            SwordStep1ExpertComboBonus = BindServerSync(config, "Sword Tree", "Tier1_검전문가_2연속공격력보너스", 7f,
                "Tier 1: 검 전문가(sword_step1_expert) - 2연속 공격력 보너스 (%)");

            SwordStep1ExpertDuration = BindServerSync(config, "Sword Tree", "Tier1_검전문가_2연속공격버프지속시간", 4f,
                "Tier 1: 검 전문가(sword_step1_expert) - 2연속 공격 버프 지속시간 (초)");

            SwordStep1FastSlashSpeed = BindServerSync(config, "Sword Tree", "Tier1_빠른베기_공격속도보너스", 5f,
                "Tier 1: 빠른 베기(sword_step1_fast_slash) - 공격속도 보너스 (%)");

            SwordStep1CounterDefenseBonus = BindServerSync(config, "Sword Tree", "Tier1_반격자세_패링성공후방어력보너스", 20f,
                "Tier 1: 반격 자세(sword_step1_counter) - 패링 성공 후 방어력 보너스 (%)");

            SwordStep1CounterDuration = BindServerSync(config, "Sword Tree", "Tier1_반격자세_패링성공후버프지속시간", 5f,
                "Tier 1: 반격 자세(sword_step1_counter) - 패링 성공 후 버프 지속시간 (초)");

            SwordStep2ComboSlashBonus = BindServerSync(config, "Sword Tree", "Tier2_연속베기_3연속공격력보너스", 13f,
                "Tier 2: 연속베기(sword_step2_combo_slash) - 3연속 공격력 보너스 (%)");

            SwordStep2ComboSlashDuration = BindServerSync(config, "Sword Tree", "Tier2_연속베기_3연속공격버프지속시간", 4f,
                "Tier 2: 연속베기(sword_step2_combo_slash) - 3연속 공격 버프 지속시간 (초)");

            SwordStep3BladeCounterBonus = BindServerSync(config, "Sword Tree", "Tier3_칼날되치기_공격력보너스", 3f,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 공격력 보너스 (고정값)");

            SwordStep3BladeCounterDuration = BindServerSync(config, "Sword Tree", "Tier3_칼날되치기_사용안함", 5f,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 사용 안 함 (패시브 스킬)");

            SwordStep3OffenseDefenseAttackBonus = BindServerSync(config, "Sword Tree", "Tier3_공방일체_양손무기착용시공격력보너스", 20f,
                "Tier 3: 공방일체(sword_step3_offense_defense) - 양손 무기 착용시 공격력 보너스 (%)");

            SwordStep3OffenseDefenseDefenseBonus = BindServerSync(config, "Sword Tree", "Tier3_공방일체_양손무기착용시방어력보너스", 10f,
                "Tier 3: 공방일체(sword_step3_offense_defense) - 양손 무기 착용시 방어력 보너스 (%)");

            SwordStep4TrueDuelSpeed = BindServerSync(config, "Sword Tree", "Tier4_진검승부_공격속도보너스", 7f,
                "Tier 4: 진검승부(sword_step4_true_duel) - 공격 속도 보너스 (%)");

            SwordSlashAttackCount = BindServerSync(config, "Sword Tree", "Tier_소드슬래시_연속공격횟수", 3,
                "검 전문가: Sword Slash(sword_slash) - 연속 공격 횟수");

            SwordSlashAttackInterval = BindServerSync(config, "Sword Tree", "Tier_소드슬래시_공격간격", 0.2f,
                "검 전문가: Sword Slash(sword_slash) - 공격 간격 (초)");

            SwordSlashDamageRatio = BindServerSync(config, "Sword Tree", "Tier_소드슬래시_1회공격력비율", 80f,
                "검 전문가: Sword Slash(sword_slash) - 1회 공격력 비율 (%)");

            SwordSlashStaminaCost = BindServerSync(config, "Sword Tree", "Tier_소드슬래시_스태미나소모", 15f,
                "검 전문가: Sword Slash(sword_slash) - 스태미나 소모량");

            SwordSlashCooldown = BindServerSync(config, "Sword Tree", "Tier_소드슬래시_쿨타임", 35f,
                "검 전문가: Sword Slash(sword_slash) - 쿨타임 (초)");

            SwordStep5DefenseSwitchShieldReduction = BindServerSync(config, "Sword Tree", "Tier5_방어전환_방패착용시피해감소", 8f,
                "Tier 5: 방어 전환(sword_step5_defswitch) - 방패 착용 시 받는 피해 감소 (%)");

            SwordStep5DefenseSwitchNoShieldBonus = BindServerSync(config, "Sword Tree", "Tier5_방어전환_방패미착용시공격력보너스", 30f,
                "Tier 5: 방어 전환(sword_step5_defswitch) - 방패 미착용 시 공격력 보너스 (%)");

            SwordStep6UltimateSlashMultiplier = BindServerSync(config, "Sword Tree", "Tier6_일도양단_액티브스킬공격력보너스", 100f,
                "Tier 6: 일도양단(sword_step6_ultimate_slash) - 액티브 스킬 공격력 보너스 (%)");

            Plugin.Log.LogDebug("[SkillTreeConfig] 검 전문가 트리 설정 초기화 완료");
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
            try
            {
                var statusEffect = Player.m_localPlayer.GetSEMan()?.GetStatusEffect("SE_StatTreeSpeed".GetHashCode());
                statusEffect?.ResetTime();
            }
            catch (Exception ex) { Plugin.Log.LogWarning($"[SkillTreeConfig] StatusEffect 업데이트 중 오류: {ex.Message}"); }
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

        public static void BroadcastConfigToClients()
        {
            if (!_isServer) return;

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
                ["sword_expert_damage"] = SwordExpertDamage.Value,
            };

            var configString = SerializeConfigData(configData);
            if (ZNet.instance != null)
            {
                ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, "CaptainSkillTree.SkillTreeMod_ConfigSync", configString);
                Plugin.Log.LogInfo("[SkillTreeConfig] 서버 설정을 모든 클라이언트에게 전송");
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

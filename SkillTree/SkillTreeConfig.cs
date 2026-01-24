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

    public static class SkillTreeConfig
    {
        // 서버/클라이언트 동기화용 데이터
        private static Dictionary<string, float> _serverConfigValues = new Dictionary<string, float>();
        private static bool _isServer = false;
        private static bool _hasReceivedServerConfig = false;

        // Config 파일 변경 감지
        private static FileSystemWatcher _configWatcher = null;
        private static ConfigFile _configFile = null;

        /// <summary>
        /// 서버 동기화용 Config 바인드 헬퍼 (float)
        /// 멀티플레이어에서 서버 Config가 모든 클라이언트에 자동 동기화됨
        /// </summary>
        public static ConfigEntry<float> BindServerSync(ConfigFile config, string section, string key, float defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
        }

        /// <summary>
        /// 서버 동기화용 Config 바인드 헬퍼 (int)
        /// </summary>
        public static ConfigEntry<int> BindServerSync(ConfigFile config, string section, string key, int defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
        }

        /// <summary>
        /// 서버 동기화용 Config 바인드 헬퍼 (bool)
        /// </summary>
        public static ConfigEntry<bool> BindServerSync(ConfigFile config, string section, string key, bool defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
        }

        /// <summary>
        /// 서버 동기화용 Config 바인드 헬퍼 (string)
        /// </summary>
        public static ConfigEntry<string> BindServerSync(ConfigFile config, string section, string key, string defaultValue, string description)
        {
            return config.Bind(section, key, defaultValue,
                new ConfigDescription(description, null,
                    new ConfigurationManagerAttributes { IsAdminOnly = true }));
        }

        // === 공격 전문가 노드 설정 ===
        // BepInEx Config 시스템 사용
        
        // 루트 (공격 전문가)
        public static ConfigEntry<float> AttackRootDamageBonus;
        
        // 2단계 (4가지)
        public static ConfigEntry<float> AttackMeleeBonusChance;
        public static ConfigEntry<float> AttackMeleeBonusDamage;
        public static ConfigEntry<float> AttackBowBonusChance;
        public static ConfigEntry<float> AttackBowBonusDamage;
        public static ConfigEntry<float> AttackCrossbowBonusChance;
        public static ConfigEntry<float> AttackCrossbowBonusDamage;
        public static ConfigEntry<float> AttackStaffBonusChance;
        public static ConfigEntry<float> AttackStaffBonusDamage;
        
        // 3단계 (기본 공격)
        public static ConfigEntry<float> AttackBasePhysicalDamage;  // 기본 공격 - 물리 공격력 보너스
        public static ConfigEntry<float> AttackBaseElementalDamage;  // 기본 공격 - 속성 공격력 보너스
        public static ConfigEntry<float> AttackStatBonus;           // 공격 증가 - 힘+지능 보너스 (atk_twohand_drain용)
        public static ConfigEntry<float> AttackTwoHandDrainPhysicalDamage;  // atk_twohand_drain - 물리 공격력 보너스
        public static ConfigEntry<float> AttackTwoHandDrainElementalDamage; // atk_twohand_drain - 속성 공격력 보너스

        // === 방어 전문가 노드 설정 ===
        // 모든 Defense Config는 Defense_Config.cs 파일로 이동됨

        // 4단계 (3가지)
        public static ConfigEntry<float> AttackCritChance;        // 정밀 공격 - 치명타 확률
        public static ConfigEntry<float> AttackMeleeEnhancement;  // 근접 강화 - 2연속 공격 보너스
        public static ConfigEntry<float> AttackRangedEnhancement; // 원거리 강화 - 원거리 무기 공격력 보너스
        
        // 5단계 (특수화 스탯)
        public static ConfigEntry<float> AttackSpecialStat;       // 특수화 스탯 - 특수화 보너스
        
        // 6단계 (4가지)
        public static ConfigEntry<float> AttackCritDamageBonus;   // 약점 공격 - 치명타 피해 보너스
        public static ConfigEntry<float> AttackTwoHandedBonus;    // 양손 분쇄 - 양손 무기 보너스
        public static ConfigEntry<float> AttackStaffElemental;    // 속성 공격 - 지팡이 속성 보너스
        public static ConfigEntry<float> AttackFinisherMeleeBonus; // 연속 근접의 대가 - 3연속 공격 보너스

        // === 속도 전문가 노드 설정 ===
        
        // 루트 (속도 전문가)
        public static ConfigEntry<float> SpeedRootMoveSpeed;
        
        // 1단계 (기초 속도)
        public static ConfigEntry<float> SpeedBaseMoveSpeed;        // 민첩함의 기초 - 이동속도
        public static ConfigEntry<float> SpeedBaseAttackSpeed;
        public static ConfigEntry<float> SpeedBaseDodgeSpeed;
        
        // 2단계 추가 설정
        public static ConfigEntry<float> SpeedMeleeComboSpeed;      // 근접 연속의 흐름 - 이동속도 보너스
        
        // 2단계 (무기별 특화 4개)
        public static ConfigEntry<float> SpeedMeleeComboStamina;    // 근접 2연타 스태미나 감소
        public static ConfigEntry<float> SpeedMeleeComboDuration;   // 근접 2연타 지속시간
        public static ConfigEntry<float> SpeedCrossbowExpertSpeed;  // 석궁 숙련자 이동속도 보너스
        public static ConfigEntry<float> SpeedCrossbowExpertDuration; // 석궁 숙련자 지속시간
        public static ConfigEntry<float> SpeedBowExpertStamina;     // 활 숙련자 스태미나 감소
        public static ConfigEntry<float> SpeedBowExpertDuration;    // 활 숙련자 지속시간
        public static ConfigEntry<float> SpeedStaffCastSpeed;       // 지팡이 시전 중 이동속도
        
        // 4단계 (마스터)
        public static ConfigEntry<float> SpeedFoodEfficiency;       // 음식 소모 속도 감소
        public static ConfigEntry<float> SpeedShipBonus;            // 배 운전 속도 증가
        
        // 5단계 (특수 능력)
        public static ConfigEntry<float> SpeedCooldownReduction;    // 쿨타임 감소
        
        // 8단계 (최종 가속 4개)
        public static ConfigEntry<float> SpeedMeleeAttackSpeed;     // 근접 공격속도
        public static ConfigEntry<float> SpeedCrossbowDrawSpeed;    // 석궁 장전속도
        public static ConfigEntry<float> SpeedBowDrawSpeed;         // 활 장전속도
        public static ConfigEntry<float> SpeedStaffCastSpeedFinal;  // 지팡이 시전속도

        // === 속도 트리 스탯 변환 설정 ===
        // speed_1: 공격속도/이동속도 보너스
        public static ConfigEntry<float> SpeedDexterityAttackSpeedBonus;  // 공격속도 보너스
        public static ConfigEntry<float> SpeedDexterityMoveSpeedBonus;    // 이동속도 보너스

        // speed_2: 지구력 +3 → 스태미나 최대치 증가
        public static ConfigEntry<float> SpeedEnduranceStaminaBonus;      // 지구력당 스태미나 최대치 보너스

        // speed_3: 지능 +3 → 에이트르 최대치 증가
        public static ConfigEntry<float> SpeedIntellectEitrBonus;         // 지능당 에이트르 최대치 보너스

        // === 점프 숙련자 설정 ===
        public static ConfigEntry<float> JumpSkillLevelBonus;       // 점프 기술 레벨 보너스
        public static ConfigEntry<float> JumpStaminaReduction;      // 점프 스태미나 감소 (%)

        // === 활 전문가 멀티샷 패시브 스킬 설정 ===
        public static ConfigEntry<float> BowMultishotLv1Chance;            // Lv1 확률 (기본: 15%)
        public static ConfigEntry<float> BowMultishotLv2Chance;            // Lv2 확률 (기본: 36%)
        public static ConfigEntry<int> BowMultishotArrowCount;             // 추가 발사할 화살 수 (기본: 2발)
        public static ConfigEntry<int> BowMultishotArrowConsumption;       // 화살 소모량 (기본: 0)
        public static ConfigEntry<float> BowMultishotDamagePercent;        // 화살당 데미지 비율 (기본: 70%)

        // 내부 액세스용 프로퍼티 (새로운 키 네이밍 적용)
        public static float AttackRootDamageBonusValue => GetEffectiveValue("Attack_Expert_Damage", AttackRootDamageBonus.Value);
        public static float AttackMeleeBonusChanceValue => GetEffectiveValue("Attack_Step2_MeleeBonusChance", AttackMeleeBonusChance.Value);
        public static float AttackMeleeBonusDamageValue => GetEffectiveValue("Attack_Step2_MeleeBonusDamage", AttackMeleeBonusDamage.Value);
        public static float AttackBowBonusChanceValue => GetEffectiveValue("Attack_Step2_BowBonusChance", AttackBowBonusChance.Value);
        public static float AttackBowBonusDamageValue => GetEffectiveValue("Attack_Step2_BowBonusDamage", AttackBowBonusDamage.Value);
        public static float AttackCrossbowBonusChanceValue => GetEffectiveValue("Attack_Step2_CrossbowBonusChance", AttackCrossbowBonusChance.Value);
        public static float AttackCrossbowBonusDamageValue => GetEffectiveValue("Attack_Step2_CrossbowBonusDamage", AttackCrossbowBonusDamage.Value);
        public static float AttackStaffBonusChanceValue => GetEffectiveValue("Attack_Step2_StaffBonusChance", AttackStaffBonusChance.Value);
        public static float AttackStaffBonusDamageValue => GetEffectiveValue("Attack_Step2_StaffBonusDamage", AttackStaffBonusDamage.Value);
        
        public static float AttackBasePhysicalDamageValue => GetEffectiveValue("Attack_Step3_PhysicalDamage", AttackBasePhysicalDamage.Value);
        public static float AttackBaseElementalDamageValue => GetEffectiveValue("Attack_Step3_ElementalDamage", AttackBaseElementalDamage.Value);
        public static float AttackStatBonusValue => GetEffectiveValue("Attack_Step3_StatBonus", AttackStatBonus.Value);
        public static float AttackTwoHandDrainPhysicalDamageValue => GetEffectiveValue("Attack_Step3_TwoHandDrain_PhysicalDamage", AttackTwoHandDrainPhysicalDamage.Value);
        public static float AttackTwoHandDrainElementalDamageValue => GetEffectiveValue("Attack_Step3_TwoHandDrain_ElementalDamage", AttackTwoHandDrainElementalDamage.Value);

        // 방어 전문가 Value 프로퍼티는 Defense_Config.cs 파일로 이동됨

        public static float AttackCritChanceValue => GetEffectiveValue("Attack_Step4_CritChance", AttackCritChance.Value);
        public static float AttackMeleeEnhancementValue => GetEffectiveValue("Attack_Step4_MeleeEnhancement", AttackMeleeEnhancement.Value);
        public static float AttackRangedEnhancementValue => GetEffectiveValue("Attack_Step4_RangedEnhancement", AttackRangedEnhancement.Value);
        
        public static float AttackSpecialStatValue => GetEffectiveValue("Attack_Step5_SpecialStat", AttackSpecialStat.Value);
        
        public static float AttackCritDamageBonusValue => GetEffectiveValue("Attack_Step6_CritDamageBonus", AttackCritDamageBonus.Value);
        public static float AttackTwoHandedBonusValue => GetEffectiveValue("Attack_Step6_TwoHandedBonus", AttackTwoHandedBonus.Value);
        public static float AttackStaffElementalValue => GetEffectiveValue("Attack_Step6_StaffElemental", AttackStaffElemental.Value);
        public static float AttackFinisherMeleeBonusValue => GetEffectiveValue("Attack_Step6_FinisherMeleeBonus", AttackFinisherMeleeBonus.Value);

        // 속도 전문가 접근 프로퍼티들
        public static float SpeedRootMoveSpeedValue => GetEffectiveValue("Speed_Expert_MoveSpeed", SpeedRootMoveSpeed.Value);
        public static float SpeedBaseAttackSpeedValue => GetEffectiveValue("Speed_Step1_AttackSpeed", SpeedBaseAttackSpeed.Value);
        public static float SpeedBaseDodgeSpeedValue => GetEffectiveValue("Speed_Step1_DodgeSpeed", SpeedBaseDodgeSpeed.Value);
        public static float SpeedMeleeComboStaminaValue => GetEffectiveValue("Speed_Step2_MeleeComboStamina", SpeedMeleeComboStamina.Value);
        public static float SpeedMeleeComboDurationValue => GetEffectiveValue("Speed_Step2_MeleeComboDuration", SpeedMeleeComboDuration.Value);
        public static float SpeedCrossbowExpertSpeedValue => GetEffectiveValue("Speed_Step2_CrossbowExpertSpeed", SpeedCrossbowExpertSpeed.Value);
        public static float SpeedCrossbowExpertDurationValue => GetEffectiveValue("Speed_Step2_CrossbowExpertDuration", SpeedCrossbowExpertDuration.Value);
        public static float SpeedBowExpertStaminaValue => GetEffectiveValue("Speed_Step2_BowExpertStamina", SpeedBowExpertStamina.Value);
        public static float SpeedBowExpertDurationValue => GetEffectiveValue("Speed_Step2_BowExpertDuration", SpeedBowExpertDuration.Value);
        public static float SpeedStaffCastSpeedValue => GetEffectiveValue("Speed_Step2_StaffCastSpeed", SpeedStaffCastSpeed.Value);
        public static float SpeedFoodEfficiencyValue => GetEffectiveValue("Speed_Step4_FoodEfficiency", SpeedFoodEfficiency.Value);
        public static float SpeedShipBonusValue => GetEffectiveValue("Speed_Step4_ShipBonus", SpeedShipBonus.Value);
        public static float SpeedCooldownReductionValue => GetEffectiveValue("Speed_Step5_CooldownReduction", SpeedCooldownReduction.Value);
        public static float SpeedMeleeAttackSpeedValue => GetEffectiveValue("Speed_Step8_MeleeAttackSpeed", SpeedMeleeAttackSpeed.Value);
        public static float SpeedCrossbowDrawSpeedValue => GetEffectiveValue("Speed_Step8_CrossbowDrawSpeed", SpeedCrossbowDrawSpeed.Value);
        public static float SpeedBowDrawSpeedValue => GetEffectiveValue("Speed_Step8_BowDrawSpeed", SpeedBowDrawSpeed.Value);
        public static float SpeedStaffCastSpeedFinalValue => GetEffectiveValue("Speed_Step8_StaffCastSpeedFinal", SpeedStaffCastSpeedFinal.Value);

        // 속도 트리 스탯 변환 Value 프로퍼티
        public static float SpeedDexterityAttackSpeedBonusValue => GetEffectiveValue("Speed_Dexterity_AttackSpeedBonus", SpeedDexterityAttackSpeedBonus.Value);
        public static float SpeedDexterityMoveSpeedBonusValue => GetEffectiveValue("Speed_Dexterity_MoveSpeedBonus", SpeedDexterityMoveSpeedBonus.Value);
        public static float SpeedEnduranceStaminaBonusValue => GetEffectiveValue("Speed_Endurance_StaminaBonus", SpeedEnduranceStaminaBonus.Value);
        public static float SpeedIntellectEitrBonusValue => GetEffectiveValue("Speed_Intellect_EitrBonus", SpeedIntellectEitrBonus.Value);

        // === 점프 숙련자 동적 값 접근자 ===
        public static float JumpSkillLevelBonusValue => GetEffectiveValue("Jump_Expert_SkillLevelBonus", JumpSkillLevelBonus.Value);
        public static float JumpStaminaReductionValue => GetEffectiveValue("Jump_Expert_StaminaReduction", JumpStaminaReduction.Value);

        // === 활 전문가 멀티샷 패시브 스킬 접근자 ===
        public static float BowMultishotLv1ChanceValue => GetEffectiveValue("Bow_MultiShot_Lv1_Chance", BowMultishotLv1Chance.Value);
        public static float BowMultishotLv2ChanceValue => GetEffectiveValue("Bow_MultiShot_Lv2_Chance", BowMultishotLv2Chance.Value);
        public static int BowMultishotArrowCountValue => (int)GetEffectiveValue("Bow_MultiShot_ArrowCount", BowMultishotArrowCount.Value);
        public static int BowMultishotArrowConsumptionValue => (int)GetEffectiveValue("Bow_MultiShot_ArrowConsumption", BowMultishotArrowConsumption.Value);
        public static float BowMultishotDamagePercentValue => GetEffectiveValue("Bow_MultiShot_DamagePercent", BowMultishotDamagePercent.Value);

        // === 창 전문가 스킬 설정 ===
        // 1단계: 창 전문가 - 2연속 공격 시 공격 속도 +10%, 공격력 +7%(4초 동안)
        public static ConfigEntry<float> SpearStep1AttackSpeed;
        public static ConfigEntry<float> SpearStep1DamageBonus;
        public static ConfigEntry<float> SpearStep1Duration;

        // 2단계: 투창 전문가 - G키 창을 던지고 자동 회수 (쿨타임, 데미지, 버프 지속시간)
        public static ConfigEntry<float> SpearStep1ThrowCooldown;
        public static ConfigEntry<float> SpearStep1ThrowDamage;
        public static ConfigEntry<float> SpearStep1ThrowBuffDuration;
        
        // 2단계: 급소 찌르기 - 창 공격력 +20%
        public static ConfigEntry<float> SpearStep1CritDamageBonus;
        
        // 3단계: 회피 찌르기 - 구르기 직후 공격 시 피해 +25%
        public static ConfigEntry<float> SpearStep2EvasionDamageBonus;
        
        // 3단계: 연격창 - 무기 공격력 +4
        public static ConfigEntry<float> SpearStep3PierceDamageBonus;
        
        // 4단계: 쾌속 창 - 투창 공격력 +40%
        public static ConfigEntry<float> SpearStep3QuickDamageBonus;
        
        // 5단계: 삼연창 - 3연속 공격 시 공격력 +20%
        public static ConfigEntry<float> SpearStep4TripleDamageBonus;
        
        // 6단계: 꿰뚫는 창 - 치명타 확률 12%
        public static ConfigEntry<float> SpearStep5PenetrateCritChance;
        
        // 6단계: 연공창 - G키 액티브 스킬 (넉백, 강화 투창)
        public static ConfigEntry<float> SpearStep5ComboCooldown;
        public static ConfigEntry<float> SpearStep5ComboDamage;
        public static ConfigEntry<float> SpearStep5ComboStaminaCost;
        public static ConfigEntry<float> SpearStep5ComboKnockbackRadius;
        public static ConfigEntry<float> SpearStep5ComboRange;

        // 창 전문가 접근 프로퍼티들
        public static float SpearStep1AttackSpeedValue => GetEffectiveValue("spear_Step1_attack_speed", SpearStep1AttackSpeed.Value);
        public static float SpearStep1DamageBonusValue => GetEffectiveValue("spear_Step1_damage_bonus", SpearStep1DamageBonus.Value);
        public static float SpearStep1DurationValue => GetEffectiveValue("spear_Step1_duration", SpearStep1Duration.Value);
        public static float SpearStep2ThrowCooldownValue => GetEffectiveValue("spear_Step1_throw_cooldown", SpearStep1ThrowCooldown.Value);
        public static float SpearStep2ThrowDamageValue => GetEffectiveValue("spear_Step1_throw_damage", SpearStep1ThrowDamage.Value);
        public static float SpearStep2ThrowBuffDurationValue => GetEffectiveValue("spear_Step1_throw_buff_duration", SpearStep1ThrowBuffDuration.Value);
        public static float SpearStep2CritDamageBonusValue => GetEffectiveValue("spear_Step1_crit_damage_bonus", SpearStep1CritDamageBonus.Value);
        public static float SpearStep3EvasionDamageBonusValue => GetEffectiveValue("spear_Step2_evasion_damage_bonus", SpearStep2EvasionDamageBonus.Value);
        public static float SpearStep3PierceDamageBonusValue => GetEffectiveValue("spear_Step3_pierce_damage", SpearStep3PierceDamageBonus.Value);
        public static float SpearStep4QuickDamageBonusValue => GetEffectiveValue("spear_Step3_quick_damage_bonus", SpearStep3QuickDamageBonus.Value);
        public static float SpearStep5TripleDamageBonusValue => GetEffectiveValue("spear_Step4_triple_damage_bonus", SpearStep4TripleDamageBonus.Value);
        public static float SpearStep6PenetrateCritChanceValue => GetEffectiveValue("spear_Step5_penetrate_crit_chance", SpearStep5PenetrateCritChance.Value);
        public static float SpearStep6ComboCooldownValue => GetEffectiveValue("spear_Step5_combo_cooldown", SpearStep5ComboCooldown.Value);
        public static float SpearStep6ComboDamageValue => GetEffectiveValue("spear_Step5_combo_damage", SpearStep5ComboDamage.Value);
        public static float SpearStep6ComboStaminaCostValue => GetEffectiveValue("spear_Step5_combo_stamina_cost", SpearStep5ComboStaminaCost.Value);
        public static float SpearStep6ComboKnockbackRadiusValue => GetEffectiveValue("spear_Step5_combo_knockback_radius", SpearStep5ComboKnockbackRadius.Value);
        public static float SpearStep2ThrowRangeValue => GetEffectiveValue("spear_Step5_combo_range", SpearStep5ComboRange.Value);
        public static float SpearStep2ThrowStaminaCostValue => GetEffectiveValue("spear_Step5_combo_stamina_cost", SpearStep5ComboStaminaCost.Value);

        // === 단검 전문가 스킬 설정 - Knife_Config.cs로 이동됨 ===
        // 모든 단검 스킬 설정은 Knife_Config.cs에서 관리됨
        
        // === 검 전문가 스킬 설정 ===  
        // Tier 0: 검 전문가 - 기본 공격력 보너스 +25%
        public static ConfigEntry<float> SwordExpertDamage;

        // Step1: 검 전문가 - 2연속 공격력 +7%
        public static ConfigEntry<float> SwordStep1ExpertComboBonus;
        public static ConfigEntry<float> SwordStep1ExpertDuration;
        
        // Step2: 빠른 베기 - 공격속도 +5%
        public static ConfigEntry<float> SwordStep1FastSlashSpeed;
        
        // Step2: 반격 자세 - 패링 성공 후 5초동안 방어력 +20%
        public static ConfigEntry<float> SwordStep1CounterDefenseBonus;
        public static ConfigEntry<float> SwordStep1CounterDuration;
        
        // Step3: 연속베기 - 3연속 공격력 +13%
        public static ConfigEntry<float> SwordStep2ComboSlashBonus;
        public static ConfigEntry<float> SwordStep2ComboSlashDuration;
        
        // Step4: 칼날 되치기 - 패링 성공 시 5초동안 공격력 +20%
        public static ConfigEntry<float> SwordStep3BladeCounterBonus;
        public static ConfigEntry<float> SwordStep3BladeCounterDuration;
        
        // Step4: 공방일체 - 양손 무기 착용시 공격력 +20%, 방어력 +10%
        public static ConfigEntry<float> SwordStep3OffenseDefenseAttackBonus;
        public static ConfigEntry<float> SwordStep3OffenseDefenseDefenseBonus;
        
        // Step5: 진검승부 - 공격 속도 +7%
        public static ConfigEntry<float> SwordStep4TrueDuelSpeed;
        
        // Step6: Sword Slash 액티브 스킬 - 3연속 공격 (80% 데미지 x 3회)
        public static ConfigEntry<int> SwordSlashAttackCount;
        public static ConfigEntry<float> SwordSlashAttackInterval;
        public static ConfigEntry<float> SwordSlashDamageRatio;
        public static ConfigEntry<float> SwordSlashStaminaCost;
        public static ConfigEntry<float> SwordSlashCooldown;
        
        // Step6: 방어 전환 - 방패 착용 시 피해 -8%, 방패 없을 시 피해 +30%
        public static ConfigEntry<float> SwordStep5DefenseSwitchShieldReduction;
        public static ConfigEntry<float> SwordStep5DefenseSwitchNoShieldBonus;
        
        // Step7: 궁극 베기 - 모든 검 스킬 효과 +50% 강화
        public static ConfigEntry<float> SwordStep6UltimateSlashMultiplier;

        // === 활 전문가 스킬 설정 ===
        // Step1: 활 전문가 - 활 공격 데미지 +5%
        public static ConfigEntry<float> BowStep1ExpertDamageBonus;
        
        // Step2: 집중 사격 - 치명타 확률 +7%
        public static ConfigEntry<float> BowStep2FocusCritBonus;
        
        // Step3: 활 숙련 - 활 기술(숙련도) +10
        public static ConfigEntry<float> BowStep3SpeedShotSkillBonus;
        
        // Step3: 침묵의 화살 - 공격력 +15%
        public static ConfigEntry<float> BowStep3SilentShotDamageBonus;
        
        // Step3: 특수 화살 - 30% 확률로 특수 화살 발사
        public static ConfigEntry<float> BowStep3SpecialArrowChance;
        
        // Step4: 강력한 한 발 - 명중 시 35% 확률로 강한 넉백
        public static ConfigEntry<float> BowStep4PowerShotKnockbackChance;
        public static ConfigEntry<float> BowStep4PowerShotKnockbackPower;
        
        // Step5: 사냥 본능 - 치명타 확률 +10%
        public static ConfigEntry<float> BowStep5InstinctCritBonus;

        // Step5: 정조준 - 크리티컬 데미지 +30%
        public static ConfigEntry<float> BowStep5MasterCritDamage;

        // Step5: 화살비 - 29% 확률로 화살 3개 발사
        public static ConfigEntry<float> BowStep5ArrowRainChance;
        public static ConfigEntry<int> BowStep5ArrowRainCount;

        // Step5: 백스텝 샷 - 구르기 직후 공격 시 치명타 확률 +25%
        public static ConfigEntry<float> BowStep5BackstepShotCritBonus;
        public static ConfigEntry<float> BowStep5BackstepShotWindow;
        
        // Step6: 크리티컬 부스트 - T키 액티브 스킬 (활 마스터)
        public static ConfigEntry<float> BowStep6CritBoostDamageBonus;
        public static ConfigEntry<float> BowStep6CritBoostCritChance;
        public static ConfigEntry<int> BowStep6CritBoostArrowCount;
        public static ConfigEntry<float> BowStep6CritBoostCooldown;
        public static ConfigEntry<float> BowStep6CritBoostStaminaCost;
        
        // Step6: 폭발 화살 - T키 액티브 스킬
        public static ConfigEntry<float> BowExplosiveArrowDamage;
        public static ConfigEntry<float> BowExplosiveArrowCooldown;
        public static ConfigEntry<float> BowExplosiveArrowStaminaCost;
        public static ConfigEntry<float> BowExplosiveArrowRadius;


        // 활 전문가 접근 프로퍼티들
        public static float BowStep1ExpertDamageBonusValue => GetEffectiveValue("bow_Step1_expert_damage_bonus", BowStep1ExpertDamageBonus.Value);
        public static float BowStep2FocusCritBonusValue => GetEffectiveValue("bow_Step2_focus_crit_bonus", BowStep2FocusCritBonus.Value);
        public static float BowStep3SpeedShotSkillBonusValue => GetEffectiveValue("bow_Step3_speedshot_skill_bonus", BowStep3SpeedShotSkillBonus.Value);
        public static float BowStep3SilentShotDamageBonusValue => GetEffectiveValue("bow_Step3_silentshot_damage_bonus", BowStep3SilentShotDamageBonus.Value);
        public static float BowStep3SpecialArrowChanceValue => GetEffectiveValue("bow_Step3_special_arrow_chance", BowStep3SpecialArrowChance.Value);
        public static float BowStep4PowerShotKnockbackChanceValue => GetEffectiveValue("bow_Step4_powershot_knockback_chance", BowStep4PowerShotKnockbackChance.Value);
        public static float BowStep4PowerShotKnockbackPowerValue => GetEffectiveValue("bow_Step4_powershot_knockback_power", BowStep4PowerShotKnockbackPower.Value);
        public static float BowStep5ArrowRainChanceValue => GetEffectiveValue("bow_Step5_arrow_rain_chance", BowStep5ArrowRainChance.Value);
        public static int BowStep5ArrowRainCountValue => (int)GetEffectiveValue("bow_Step5_arrow_rain_count", (float)BowStep5ArrowRainCount.Value);
        public static float BowStep5BackstepShotCritBonusValue => GetEffectiveValue("bow_Step5_backstep_shot_crit_bonus", BowStep5BackstepShotCritBonus.Value);
        public static float BowStep5BackstepShotWindowValue => GetEffectiveValue("bow_Step5_backstep_shot_window", BowStep5BackstepShotWindow.Value);
        public static float BowStep5InstinctCritBonusValue => GetEffectiveValue("bow_Step5_instinct_crit_bonus", BowStep5InstinctCritBonus.Value);
        public static float BowStep5MasterCritDamageValue => GetEffectiveValue("bow_Step5_master_crit_damage", BowStep5MasterCritDamage.Value);
        public static float BowStep6CritBoostDamageBonusValue => GetEffectiveValue("bow_Step6_critboost_damage_bonus", BowStep6CritBoostDamageBonus.Value);
        public static float BowStep6CritBoostCritChanceValue => GetEffectiveValue("bow_Step6_critboost_crit_chance", BowStep6CritBoostCritChance.Value);
        public static int BowStep6CritBoostArrowCountValue => (int)GetEffectiveValue("bow_Step6_critboost_arrow_count", (float)BowStep6CritBoostArrowCount.Value);
        public static float BowStep6CritBoostCooldownValue => GetEffectiveValue("bow_Step6_critboost_cooldown", BowStep6CritBoostCooldown.Value);
        public static float BowStep6CritBoostStaminaCostValue => GetEffectiveValue("bow_Step6_critboost_stamina_cost", BowStep6CritBoostStaminaCost.Value);
        public static float BowExplosiveArrowDamageValue => GetEffectiveValue("bow_Step6_explosive_damage", BowExplosiveArrowDamage.Value);
        public static float BowExplosiveArrowCooldownValue => GetEffectiveValue("bow_Step6_explosive_cooldown", BowExplosiveArrowCooldown.Value);
        public static float BowExplosiveArrowStaminaCostValue => GetEffectiveValue("bow_Step6_explosive_stamina_cost", BowExplosiveArrowStaminaCost.Value);
        public static float BowExplosiveArrowRadiusValue => GetEffectiveValue("bow_Step6_explosive_radius", BowExplosiveArrowRadius.Value);


        // === 폴암 전문가 스킬 설정 ===
        // Step0: 폴암 전문가 - 공격 범위 +15%
        public static ConfigEntry<float> PolearmExpertRangeBonus;
        
        // Step1: 회전베기 - 휠 마우스 공격력 +60%
        public static ConfigEntry<float> PolearmStep1SpinWheelDamage;
        
        // Step1: 제압 공격 - 공격력 +30%
        public static ConfigEntry<float> PolearmStep1SuppressDamage;
        
        // Step2: 영웅 타격 - 27% 확률로 넉백
        public static ConfigEntry<float> PolearmStep2HeroKnockbackChance;
        
        // Step3: 광역 강타 - 2연속 공격 시 공격력 +25%(5초동안)
        public static ConfigEntry<float> PolearmStep3AreaComboBonus;
        public static ConfigEntry<float> PolearmStep3AreaComboDuration;
        
        // Step3: 지면 강타 - 휠 마우스 공격력 +80%
        public static ConfigEntry<float> PolearmStep3GroundWheelDamage;
        
        // Step4: 반달 베기 - 공격 범위 +15%, 공격 스태미나 -15%
        public static ConfigEntry<float> PolearmStep4MoonRangeBonus;
        public static ConfigEntry<float> PolearmStep4MoonStaminaReduction;
        
        // Step4: 폴암강화 - 무기 공격력 +5
        public static ConfigEntry<float> PolearmStep4ChargeDamageBonus;
        
        // Step5: 장창의 제왕 (G키 액티브) - 체력 50%이상인 적에게 추가 피해 +50%
        public static ConfigEntry<float> PolearmStep5KingHealthThreshold;
        public static ConfigEntry<float> PolearmStep5KingDamageBonus;
        public static ConfigEntry<float> PolearmStep5KingStaminaCost;
        public static ConfigEntry<float> PolearmStep5KingCooldown;
        public static ConfigEntry<float> PolearmStep5KingDuration;

        // === 방어 전문가 스킬 설정 ===
        // === 둥기 전문가 스킬 ===
        // 모든 Mace Config는 Mace_Config.cs 파일로 이동됨

        // === Core Config 설정 ===
        public static ConfigEntry<int> LevelPerSkillPoint;

        // 둥기 전문가 접근 프로퍼티는 Mace_Config.cs 파일로 이동됨

        // Core Config 접근 프로퍼티
        public static int LevelPerSkillPointValue => (int)GetEffectiveValue("level_per_skill_point", (float)LevelPerSkillPoint.Value);

        // 단검 전문가 접근 프로퍼티 - Knife_Config.cs로 이동됨
        // 모든 단검 관련 값들은 Knife_Config.cs에서 관리됨
        
        
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

        // 폴암 전문가 접근 프로퍼티들
        public static float PolearmExpertRangeBonusValue => GetEffectiveValue("polearm_expert_range_bonus", PolearmExpertRangeBonus.Value);
        public static float PolearmStep1SpinWheelDamageValue => GetEffectiveValue("polearm_step1_spin_wheel_damage", PolearmStep1SpinWheelDamage.Value);
        public static float PolearmStep1SuppressDamageValue => GetEffectiveValue("polearm_step1_suppress_damage", PolearmStep1SuppressDamage.Value);
        public static float PolearmStep2HeroKnockbackChanceValue => GetEffectiveValue("polearm_step2_hero_knockback_chance", PolearmStep2HeroKnockbackChance.Value);
        public static float PolearmStep3AreaComboBonusValue => GetEffectiveValue("polearm_step3_area_combo_bonus", PolearmStep3AreaComboBonus.Value);
        public static float PolearmStep3AreaComboDurationValue => GetEffectiveValue("polearm_step3_area_combo_duration", PolearmStep3AreaComboDuration.Value);
        public static float PolearmStep3GroundWheelDamageValue => GetEffectiveValue("polearm_step3_ground_wheel_damage", PolearmStep3GroundWheelDamage.Value);
        public static float PolearmStep4MoonRangeBonusValue => GetEffectiveValue("polearm_step4_moon_range_bonus", PolearmStep4MoonRangeBonus.Value);
        public static float PolearmStep4MoonStaminaReductionValue => GetEffectiveValue("polearm_step4_moon_stamina_reduction", PolearmStep4MoonStaminaReduction.Value);
        public static float PolearmStep4ChargeDamageBonusValue => GetEffectiveValue("polearm_step4_charge_damage", PolearmStep4ChargeDamageBonus.Value);
        public static float PolearmStep5KingHealthThresholdValue => GetEffectiveValue("polearm_step5_king_health_threshold", PolearmStep5KingHealthThreshold.Value);
        public static float PolearmStep5KingDamageBonusValue => GetEffectiveValue("polearm_step5_king_damage_bonus", PolearmStep5KingDamageBonus.Value);
        public static float PolearmStep5KingStaminaCostValue => GetEffectiveValue("polearm_step5_king_stamina_cost", PolearmStep5KingStaminaCost.Value);
        public static float PolearmStep5KingCooldownValue => GetEffectiveValue("polearm_step5_king_cooldown", PolearmStep5KingCooldown.Value);
        public static float PolearmStep5KingDurationValue => GetEffectiveValue("polearm_step5_king_duration", PolearmStep5KingDuration.Value);

        // 누락된 속성들 추가
        public static float AttackOneHandedBonusValue => GetEffectiveValue("Attack_OneHandedBonus", 5f); // 기본값 5%
        public static float SpeedBaseMoveSpeedValue => GetEffectiveValue("Speed_BaseMoveSpeed", SpeedBaseMoveSpeed.Value); // 컨피그에서 설정 가능
        public static float SpeedMeleeComboSpeedValue => GetEffectiveValue("Speed_MeleeComboSpeed", SpeedMeleeComboSpeed.Value); // 컨피그에서 설정 가능
        public static float SpeedMeleeComboBonusValue => GetEffectiveValue("Speed_MeleeComboBonus", SpeedMeleeComboStamina.Value);
        public static float SpeedCrossbowReloadSpeedValue => GetEffectiveValue("Speed_CrossbowReloadSpeed", SpeedCrossbowDrawSpeed.Value);
        public static float SpeedBowHitBonusValue => GetEffectiveValue("Speed_BowHitBonus", SpeedBowExpertStamina.Value);
        public static float SpeedBowHitDurationValue => GetEffectiveValue("Speed_BowHitDuration", SpeedBowExpertDuration.Value);

        public static void Initialize(ConfigFile config)
        {
            // 서버/클라이언트 감지
            DetectServerClientMode();

            // === Core Config 설정 (서버 동기화 적용) ===
            LevelPerSkillPoint = BindServerSync(config,
                "Core_Config",
                "Level_per_SkillPoint",
                1,
                "[서버동기화] 레벨당 스킬 포인트 획득량 [기본: 1, 범위: 1-10]"
            );

            // === Tier 0: 공격 전문가 (서버 동기화 적용) ===
            AttackRootDamageBonus = BindServerSync(config,
                "Attack Tree",
                "Tier0_공격전문가_모든데미지보너스",
                10f,
                "[서버동기화] Tier 0: 공격 전문가(attack_root) - 모든 데미지 보너스 (%)"
            );

            // === Tier 2: 무기 특화 ===
            AttackMeleeBonusChance = BindServerSync(config,
                "Attack Tree",
                "Tier2_근접특화_추가피해발동확률",
                20f,
                "Tier 2: 근접 특화(attack_step2_melee) - 추가 피해 발동 확률 (%)"
            );

            AttackMeleeBonusDamage = BindServerSync(config,
                "Attack Tree",
                "Tier2_근접특화_근접공격력",
                10f,
                "Tier 2: 근접 특화(attack_step2_melee) - 근접 공격력 (%)"
            );

            AttackBowBonusChance = BindServerSync(config,
                "Attack Tree",
                "Tier2_활특화_추가피해발동확률",
                20f,
                "Tier 2: 활 특화(attack_step2_bow) - 추가 피해 발동 확률 (%)"
            );

            AttackBowBonusDamage = BindServerSync(config,
                "Attack Tree",
                "Tier2_활특화_활공격력",
                8f,
                "Tier 2: 활 특화(attack_step2_bow) - 활 공격력 (%)"
            );

            AttackCrossbowBonusChance = BindServerSync(config,
                "Attack Tree",
                "Tier2_석궁특화_강화발동확률",
                15f,
                "Tier 2: 석궁 특화(attack_step2_crossbow) - 강화 발동 확률 (%)"
            );

            AttackCrossbowBonusDamage = BindServerSync(config,
                "Attack Tree",
                "Tier2_석궁특화_석궁공격력",
                9f,
                "Tier 2: 석궁 특화(attack_step2_crossbow) - 석궁 공격력 (%)"
            );

            AttackStaffBonusChance = BindServerSync(config,
                "Attack Tree",
                "Tier2_지팡이특화_속성피해발동확률",
                20f,
                "Tier 2: 지팡이 특화(attack_step2_staff) - 속성 피해 발동 확률 (%)"
            );

            AttackStaffBonusDamage = BindServerSync(config,
                "Attack Tree",
                "Tier2_지팡이특화_지팡이공격력",
                8f,
                "Tier 2: 지팡이 특화(attack_step2_staff) - 지팡이 공격력 (%)"
            );

            // === Tier 3: 기본 공격 ===
            AttackBasePhysicalDamage = BindServerSync(config,
                "Attack Tree",
                "Tier3_기본공격_물리공격력보너스",
                2f,
                "Tier 3: 기본 공격(attack_step3_base) - 물리 공격력 보너스"
            );

            AttackBaseElementalDamage = BindServerSync(config,
                "Attack Tree",
                "Tier3_기본공격_속성공격력보너스",
                2f,
                "Tier 3: 기본 공격(attack_step3_base) - 속성 공격력 보너스"
            );

            AttackStatBonus = BindServerSync(config,
                "Attack Tree",
                "Tier3_공격증가_힘지능보너스",
                5f,
                "Tier 3: 공격 증가(atk_twohand_drain) - 힘과 지능 보너스"
            );

            AttackTwoHandDrainPhysicalDamage = BindServerSync(config,
                "Attack Tree",
                "Tier3_공격증가_물리공격력보너스",
                10f,
                "Tier 3: 공격 증가(atk_twohand_drain) - 물리 공격력 보너스"
            );

            AttackTwoHandDrainElementalDamage = BindServerSync(config,
                "Attack Tree",
                "Tier3_공격증가_속성공격력보너스",
                10f,
                "Tier 3: 공격 증가(atk_twohand_drain) - 속성 공격력 보너스"
            );

            // === 방어 전문가 트리 설정 ===
            // 모든 Defense Config는 Defense_Config.cs 파일로 이동됨

            // === Tier 4: 전투 강화 ===
            AttackCritChance = BindServerSync(config,
                "Attack Tree",
                "Tier4_정밀공격_치명타확률",
                5f,
                "Tier 4: 정밀 공격(attack_step4_crit) - 치명타 확률 (%)"
            );

            AttackMeleeEnhancement = BindServerSync(config,
                "Attack Tree",
                "Tier4_근접강화_2연속공격추가피해",
                10f,
                "Tier 4: 근접 강화(attack_step4_melee_enhance) - 2연속 공격 추가 피해 (%)"
            );

            AttackRangedEnhancement = BindServerSync(config,
                "Attack Tree",
                "Tier4_원거리강화_원거리무기공격력",
                5f,
                "Tier 4: 원거리 강화(attack_step4_ranged_enhance) - 원거리 무기 공격력 (%)"
            );

            // === Tier 5: 특수화 스탯 ===
            AttackSpecialStat = BindServerSync(config,
                "Attack Tree",
                "Tier5_특수화스탯_특수화보너스",
                5f,
                "Tier 5: 특수화 스탯(attack_step5_special) - 특수화 보너스"
            );

            // === Tier 6: 최종 강화 ===
            AttackCritDamageBonus = BindServerSync(config,
                "Attack Tree",
                "Tier6_약점공격_치명타피해보너스",
                12f,
                "Tier 6: 약점 공격(attack_step6_crit_damage) - 치명타 피해 보너스 (%)"
            );

            AttackTwoHandedBonus = BindServerSync(config,
                "Attack Tree",
                "Tier6_양손분쇄_양손무기공격력보너스",
                10f,
                "Tier 6: 양손 분쇄(attack_step6_twohanded) - 양손 무기 공격력 보너스 (%)"
            );

            AttackStaffElemental = BindServerSync(config,
                "Attack Tree",
                "Tier6_속성공격_속성공격보너스",
                10f,
                "Tier 6: 속성 공격(attack_step6_elemental) - 속성 공격 보너스 (활, 지팡이) (%)"
            );

            AttackFinisherMeleeBonus = BindServerSync(config,
                "Attack Tree",
                "Tier6_연속근접의대가_3연속공격보너스",
                15f,
                "Tier 6: 연속 근접의 대가(attack_step6_finisher) - 3연속 공격 보너스 (%)"
            );

            // Plugin.Log.LogInfo("[SkillTreeConfig] 공격 전문가 트리 설정 초기화 완료"); // 제거: 과도한 로그

            // === 방어 전문가 스킬 설정 (분리된 컨피그 시스템) ===
            Defense_Config.Initialize(config);

            // === Tier 0: 속도 전문가 ===
            SpeedRootMoveSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier0_속도전문가_이동속도보너스",
                5f,
                "Tier 0: 속도 전문가(speed_root) - 이동속도 보너스 (%)"
            );

            // === Tier 1: 기초 속도 ===
            SpeedBaseAttackSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier1_공격속도_공격속도보너스",
                5f,
                "Tier 1: 공격속도(speed_step1_attack) - 공격속도 보너스 (%)"
            );

            SpeedBaseDodgeSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier1_공격속도_구르기속도보너스",
                10f,
                "Tier 1: 공격속도(speed_step1_attack) - 구르기 속도 보너스 (%)"
            );

            SpeedBaseMoveSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier1_민첩함의기초_이동속도보너스",
                3f,
                "Tier 1: 민첩함의 기초(speed_step1_base) - 이동속도 보너스 (%)"
            );

            // === Tier 2: 무기별 특화 ===
            SpeedMeleeComboSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier2_연속의흐름_근접2연타이동속도보너스",
                5f,
                "Tier 2: 연속의 흐름(speed_step2_melee_combo) - 근접 2연타 이동속도 보너스 (%)"
            );

            SpeedMeleeComboStamina = BindServerSync(config,
                "Speed Tree",
                "Tier2_연속의흐름_근접2연타스태미나감소",
                10f,
                "Tier 2: 연속의 흐름(speed_step2_melee_combo) - 근접 2연타 스태미나 감소 (%)"
            );

            SpeedMeleeComboDuration = BindServerSync(config,
                "Speed Tree",
                "Tier2_연속의흐름_근접2연타지속시간",
                5f,
                "Tier 2: 연속의 흐름(speed_step2_melee_combo) - 근접 2연타 지속시간 (초)"
            );

            SpeedCrossbowExpertSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier2_석궁숙련자_이동속도보너스",
                5f,
                "Tier 2: 석궁 숙련자(speed_step2_crossbow_expert) - 석궁 공격 시 이동속도 보너스 (%)"
            );

            SpeedCrossbowExpertDuration = BindServerSync(config,
                "Speed Tree",
                "Tier2_석궁숙련자_이동속도지속시간",
                5f,
                "Tier 2: 석궁 숙련자(speed_step2_crossbow_expert) - 이동속도 지속시간 (초)"
            );

            SpeedBowExpertStamina = BindServerSync(config,
                "Speed Tree",
                "Tier2_활숙련자_스태미나감소",
                10f,
                "Tier 2: 활 숙련자(speed_step2_bow_expert) - 활 2연속 공격 스태미나 감소 (%)"
            );

            SpeedBowExpertDuration = BindServerSync(config,
                "Speed Tree",
                "Tier2_활숙련자_스태미나감소지속시간",
                5f,
                "Tier 2: 활 숙련자(speed_step2_bow_expert) - 스태미나 감소 지속시간 (초)"
            );

            SpeedStaffCastSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier2_이동시전_지팡이시전중이동속도보너스",
                4f,
                "Tier 2: 이동 시전(speed_step2_staff_cast) - 지팡이 시전 중 이동속도 보너스 (%)"
            );

            // === Tier 4: 마스터 ===
            SpeedFoodEfficiency = BindServerSync(config,
                "Speed Tree",
                "Tier4_에너자이져_음식소모속도감소",
                15f,
                "Tier 4: 에너자이져(speed_step4_food) - 음식 소모 속도 감소 (%)"
            );

            SpeedShipBonus = BindServerSync(config,
                "Speed Tree",
                "Tier4_선장_배운전속도증가",
                15f,
                "Tier 4: 선장(speed_step4_ship) - 배 운전 속도 증가 (%)"
            );

            // === Tier 5: 특수 능력 ===
            SpeedCooldownReduction = BindServerSync(config,
                "Speed Tree",
                "Tier5_모래시계_쿨타임감소",
                1f,
                "Tier 5: 모래시계(speed_step5_cooldown) - 쿨타임 감소 (초)"
            );

            // === Tier 8: 최종 가속 ===
            SpeedMeleeAttackSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier8_근접가속_근접공격속도증가",
                7f,
                "Tier 8: 근접 가속(speed_step8_melee_attack) - 근접 공격속도 증가 (%)"
            );

            SpeedCrossbowDrawSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier8_석궁가속_석궁장전속도증가",
                5f,
                "Tier 8: 석궁 가속(speed_step8_crossbow_draw) - 석궁 장전속도 증가 (%)"
            );

            SpeedBowDrawSpeed = BindServerSync(config,
                "Speed Tree",
                "Tier8_활가속_활장전속도증가",
                7f,
                "Tier 8: 활 가속(speed_step8_bow_draw) - 활 장전속도 증가 (%)"
            );

            SpeedStaffCastSpeedFinal = BindServerSync(config,
                "Speed Tree",
                "Tier8_시전가속_지팡이시전속도증가",
                6f,
                "Tier 8: 시전 가속(speed_step8_staff_cast) - 지팡이 시전속도 증가 (%)"
            );

            // === 스탯 변환 스킬 ===

            // 민첩: 공격/이동속도 보너스
            SpeedDexterityAttackSpeedBonus = BindServerSync(config,
                "Speed Tree",
                "Tier_민첩_공격속도보너스",
                2f,
                "스탯: 민첩(speed_1) - 공격속도 보너스 (%)"
            );

            SpeedDexterityMoveSpeedBonus = BindServerSync(config,
                "Speed Tree",
                "Tier_민첩_이동속도보너스",
                2f,
                "스탯: 민첩(speed_1) - 이동속도 보너스 (%)"
            );

            // 지구력 +3 → 스태미나 최대치 증가
            SpeedEnduranceStaminaBonus = BindServerSync(config,
                "Speed Tree",
                "Tier_지구력_스태미나최대치보너스",
                15f,
                "스탯: 지구력(speed_2) - 스태미나 최대치 보너스 (지구력+3 투자 시)"
            );

            // 지능 +3 → 에이트르 최대치 증가
            SpeedIntellectEitrBonus = BindServerSync(config,
                "Speed Tree",
                "Tier_지능_에이트르최대치보너스",
                10f,
                "스탯: 지능(speed_3) - 에이트르 최대치 보너스 (지능+3 투자 시)"
            );

            // === 점프 숙련자 ===
            JumpSkillLevelBonus = BindServerSync(config,
                "Speed Tree",
                "Tier_점프숙련자_점프기술레벨보너스",
                10f,
                "점프 숙련자(speed_step3_jump) - 점프 기술 레벨 보너스"
            );

            JumpStaminaReduction = BindServerSync(config,
                "Speed Tree",
                "Tier_점프숙련자_점프스태미나감소",
                10f,
                "점프 숙련자(speed_step3_jump) - 점프 스태미나 감소 (%)"
            );

            // === Bow Tree: 활 전문가 멀티샷 패시브 ===
            BowMultishotLv1Chance = BindServerSync(config,
                "Bow Tree",
                "Tier_멀티샷Lv1_발동확률",
                15f,
                "활 전문가: 멀티샷 Lv1(bow_multishot_lv1) - 발동 확률 (%)"
            );

            BowMultishotLv2Chance = BindServerSync(config,
                "Bow Tree",
                "Tier_멀티샷Lv2_발동확률",
                36f,
                "활 전문가: 멀티샷 Lv2(bow_multishot_lv2) - 발동 확률 (%)"
            );

            BowMultishotArrowCount = BindServerSync(config,
                "Bow Tree",
                "Tier_멀티샷_추가화살수",
                2,
                "활 전문가: 멀티샷 - 추가 발사 화살 수"
            );

            BowMultishotArrowConsumption = BindServerSync(config,
                "Bow Tree",
                "Tier_멀티샷_화살소모량",
                0,
                "활 전문가: 멀티샷 - 화살 소모량"
            );

            BowMultishotDamagePercent = BindServerSync(config,
                "Bow Tree",
                "Tier_멀티샷_화살데미지비율",
                70f,
                "활 전문가: 멀티샷 - 화살당 데미지 비율 (%)"
            );

            // === Bow Tree: 활 전문가 공격 스킬 ===
            BowStep2FocusCritBonus = BindServerSync(config,
                "Bow Tree",
                "Tier_집중사격_치명타확률",
                15f,
                "활 집중 사격(bow_Step2_focus) - 치명타 확률 (%)"
            );

            // Plugin.Log.LogInfo("[SkillTreeConfig] 속도 전문가 트리 설정 초기화 완료"); // 제거: 과도한 로그

            // === 단검 전문가 설정은 Knife_Config.cs로 이동됨 ===
            // 중복 방지를 위해 기존 설정 제거됨

            // === Sword Tree: Sword Slash 액티브 스킬 ===
            SwordSlashAttackCount = BindServerSync(config,
                "Sword Tree",
                "Tier_소드슬래시_연속공격횟수",
                3,
                "검 전문가: Sword Slash(sword_slash) - 연속 공격 횟수"
            );

            SwordSlashAttackInterval = BindServerSync(config,
                "Sword Tree",
                "Tier_소드슬래시_공격간격",
                0.2f,
                "검 전문가: Sword Slash(sword_slash) - 공격 간격 (초)"
            );

            SwordSlashDamageRatio = BindServerSync(config,
                "Sword Tree",
                "Tier_소드슬래시_1회공격력비율",
                80f,
                "검 전문가: Sword Slash(sword_slash) - 1회 공격력 비율 (%)"
            );

            SwordSlashStaminaCost = BindServerSync(config,
                "Sword Tree",
                "Tier_소드슬래시_스태미나소모",
                15f,
                "검 전문가: Sword Slash(sword_slash) - 스태미나 소모량"
            );

            SwordSlashCooldown = BindServerSync(config,
                "Sword Tree",
                "Tier_소드슬래시_쿨타임",
                35f,
                "검 전문가: Sword Slash(sword_slash) - 쿨타임 (초)"
            );

            // === 활 전문가 스킬 설정 ===
            // Tier 1: 활 전문가
            BowStep1ExpertDamageBonus = BindServerSync(config,
                "Bow Tree",
                "Tier1_활전문가_활공격력보너스",
                5f,
                "Tier 1: 활 전문가(bow_Step1_damage) - 활 공격력 보너스 (%)"
            );

            // Tier 2: 집중 사격
            BowStep2FocusCritBonus = BindServerSync(config,
                "Bow Tree",
                "Tier2_집중사격_치명타확률보너스",
                7f,
                "Tier 2: 집중 사격(bow_step2_focus) - 치명타 확률 보너스 (%)"
            );

            // Tier 2: 멀티샷 Lv1 - 15% 확률로 화살 2개 발사 (이미 위에 정의됨)

            // Tier 3: 활 숙련
            BowStep3SpeedShotSkillBonus = BindServerSync(config,
                "Bow Tree",
                "Tier3_활숙련_활기술숙련도보너스",
                10f,
                "Tier 3: 활 숙련(bow_step3_speedshot) - 활 기술(숙련도) 보너스"
            );
            BowStep3SilentShotDamageBonus = BindServerSync(config,
                "Bow Tree",
                "Tier3_기본활공격_공격력증가",
                3f,
                "Tier 3: 기본 활공격(bow_step3_silentshot) - 활 공격력 증가 (고정값)"
            );

            // Tier 3: 특수 화살
            BowStep3SpecialArrowChance = BindServerSync(config,
                "Bow Tree",
                "Tier3_특수화살_발사확률",
                30f,
                "Tier 3: 특수 화살(bow_step3_special_arrow) - 특수 화살 발사 확률 (%)"
            );

            // Tier 4: 연속사격 - 멀티샷 Lv2 (36% 확률로 화살 2개 발사) (이미 위에 정의됨)

            // Tier 4: 강력한 한 발
            BowStep4PowerShotKnockbackChance = BindServerSync(config,
                "Bow Tree",
                "Tier4_강력한한발_강한넉백확률",
                35f,
                "Tier 4: 강력한 한 발(bow_step4_powershot) - 강한 넉백 확률 (%)"
            );

            BowStep4PowerShotKnockbackPower = BindServerSync(config,
                "Bow Tree",
                "Tier4_강력한한발_넉백거리",
                5f,
                "Tier 4: 강력한 한 발(bow_step4_powershot) - 넉백 거리 (m)"
            );

            // Tier 5: 화살비
            BowStep5ArrowRainChance = BindServerSync(config,
                "Bow Tree",
                "Tier5_화살비_화살3개발사확률",
                29f,
                "Tier 5: 화살비(bow_step5_arrow_rain) - 화살 3개 발사 확률 (%)"
            );

            BowStep5ArrowRainCount = BindServerSync(config,
                "Bow Tree",
                "Tier5_화살비_발사할화살개수",
                3,
                "Tier 5: 화살비(bow_step5_arrow_rain) - 발사할 화살 개수"
            );

            // Tier 5: 백스텝 샷
            BowStep5BackstepShotCritBonus = BindServerSync(config,
                "Bow Tree",
                "Tier5_백스텝샷_구르기후치명타확률보너스",
                25f,
                "Tier 5: 백스텝 샷(bow_step5_backstep_shot) - 구르기 후 치명타 확률 보너스 (%)"
            );

            BowStep5BackstepShotWindow = BindServerSync(config,
                "Bow Tree",
                "Tier5_백스텝샷_구르기후효과지속시간",
                3f,
                "Tier 5: 백스텝 샷(bow_step5_backstep_shot) - 구르기 후 효과 지속시간 (초)"
            );

            // Tier 5: 사냥 본능
            BowStep5InstinctCritBonus = BindServerSync(config,
                "Bow Tree",
                "Tier5_사냥본능_치명타확률보너스",
                10f,
                "Tier 5: 사냥 본능(bow_step5_instinct) - 치명타 확률 보너스 (%)"
            );

            // Tier 5: 정조준
            BowStep5MasterCritDamage = BindServerSync(config,
                "Bow Tree",
                "Tier5_정조준_크리티컬데미지보너스",
                30f,
                "Tier 5: 정조준(bow_step5_master) - 크리티컬 데미지 보너스 (%)"
            );

            // Tier 6: 크리티컬 부스트 - T키 액티브 스킬
            BowStep6CritBoostDamageBonus = BindServerSync(config,
                "Bow Tree",
                "Tier6_크리티컬부스트_T키액티브데미지보너스",
                50f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 데미지 보너스 (%)"
            );

            BowStep6CritBoostCritChance = BindServerSync(config,
                "Bow Tree",
                "Tier6_크리티컬부스트_T키액티브치명타확률",
                100f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 치명타 확률 (%)"
            );

            BowStep6CritBoostArrowCount = BindServerSync(config,
                "Bow Tree",
                "Tier6_크리티컬부스트_T키액티브화살개수",
                5,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 화살 개수"
            );

            BowStep6CritBoostCooldown = BindServerSync(config,
                "Bow Tree",
                "Tier6_크리티컬부스트_T키액티브쿨타임",
                45f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 쿨타임 (초)"
            );

            BowStep6CritBoostStaminaCost = BindServerSync(config,
                "Bow Tree",
                "Tier6_크리티컬부스트_T키액티브스태미나소모",
                25f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 스태미나 소모 (%)"
            );

            // Tier 6: 폭발 화살 - T키 액티브 스킬 (기존)
            BowExplosiveArrowDamage = BindServerSync(config,
                "Bow Tree",
                "Tier6_폭발화살_T키액티브데미지배율",
                120f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - T키 액티브 스킬 데미지 배율 (%)"
            );

            BowExplosiveArrowCooldown = BindServerSync(config,
                "Bow Tree",
                "Tier6_폭발화살_T키액티브쿨타임",
                20f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - T키 액티브 스킬 쿨타임 (초)"
            );

            BowExplosiveArrowStaminaCost = BindServerSync(config,
                "Bow Tree",
                "Tier6_폭발화살_T키액티브스태미나소모",
                15f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - T키 액티브 스킬 스태미나 소모 (%)"
            );

            BowExplosiveArrowRadius = BindServerSync(config,
                "Bow Tree",
                "Tier6_폭발화살_폭발범위",
                4f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - 폭발 범위 (m)"
            );

            Plugin.Log.LogDebug("[SkillTreeConfig] 활 전문가 트리 설정 초기화 완료");

            // === 지팡이 전문가 스킬 설정 (분리된 컨피그 시스템) ===
            Staff_Config.InitConfig(config);

            // === 단검 전문가 스킬 설정 (분리된 컨피그 시스템) ===
            Knife_Config.InitializeKnifeConfig(config);

            // === 석궁 전문가 스킬 설정 (분리된 컨피그 시스템) ===
            Crossbow_Config.InitializeCrossbowConfig(config);

            // === 검 전문가 스킬 설정 ===
            // Tier 0: 검 전문가 기본 데미지
            SwordExpertDamage = BindServerSync(config,
                "Sword Tree",
                "Tier0_검전문가_공격력보너스",
                10f,
                "Tier 0: 검 전문가(sword_expert) - 기본 공격력 보너스 (%)"
            );

            // Tier 1: 검 전문가
            SwordStep1ExpertComboBonus = BindServerSync(config,
                "Sword Tree",
                "Tier1_검전문가_2연속공격력보너스",
                7f,
                "Tier 1: 검 전문가(sword_step1_expert) - 2연속 공격력 보너스 (%)"
            );

            SwordStep1ExpertDuration = BindServerSync(config,
                "Sword Tree",
                "Tier1_검전문가_2연속공격버프지속시간",
                4f,
                "Tier 1: 검 전문가(sword_step1_expert) - 2연속 공격 버프 지속시간 (초)"
            );

            // Tier 1: 빠른 베기
            SwordStep1FastSlashSpeed = BindServerSync(config,
                "Sword Tree",
                "Tier1_빠른베기_공격속도보너스",
                5f,
                "Tier 1: 빠른 베기(sword_step1_fast_slash) - 공격속도 보너스 (%)"
            );

            // Tier 1: 반격 자세
            SwordStep1CounterDefenseBonus = BindServerSync(config,
                "Sword Tree",
                "Tier1_반격자세_패링성공후방어력보너스",
                20f,
                "Tier 1: 반격 자세(sword_step1_counter) - 패링 성공 후 방어력 보너스 (%)"
            );

            SwordStep1CounterDuration = BindServerSync(config,
                "Sword Tree",
                "Tier1_반격자세_패링성공후버프지속시간",
                5f,
                "Tier 1: 반격 자세(sword_step1_counter) - 패링 성공 후 버프 지속시간 (초)"
            );

            // Tier 2: 연속베기
            SwordStep2ComboSlashBonus = BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_3연속공격력보너스",
                13f,
                "Tier 2: 연속베기(sword_step2_combo_slash) - 3연속 공격력 보너스 (%)"
            );

            SwordStep2ComboSlashDuration = BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_3연속공격버프지속시간",
                4f,
                "Tier 2: 연속베기(sword_step2_combo_slash) - 3연속 공격 버프 지속시간 (초)"
            );

            // Tier 3: 칼날 되치기
            SwordStep3BladeCounterBonus = BindServerSync(config,
                "Sword Tree",
                "Tier3_칼날되치기_공격력보너스",
                3f,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 공격력 보너스 (고정값)"
            );

            SwordStep3BladeCounterDuration = BindServerSync(config,
                "Sword Tree",
                "Tier3_칼날되치기_사용안함",
                5f,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 사용 안 함 (패시브 스킬)"
            );

            // Tier 3: 공방일체
            SwordStep3OffenseDefenseAttackBonus = BindServerSync(config,
                "Sword Tree",
                "Tier3_공방일체_양손무기착용시공격력보너스",
                20f,
                "Tier 3: 공방일체(sword_step3_offense_defense) - 양손 무기 착용시 공격력 보너스 (%)"
            );

            SwordStep3OffenseDefenseDefenseBonus = BindServerSync(config,
                "Sword Tree",
                "Tier3_공방일체_양손무기착용시방어력보너스",
                10f,
                "Tier 3: 공방일체(sword_step3_offense_defense) - 양손 무기 착용시 방어력 보너스 (%)"
            );

            // Tier 4: 진검승부
            SwordStep4TrueDuelSpeed = BindServerSync(config,
                "Sword Tree",
                "Tier4_진검승부_공격속도보너스",
                7f,
                "Tier 4: 진검승부(sword_step4_true_duel) - 공격 속도 보너스 (%)"
            );

            // Tier 5: 방어 전환
            SwordStep5DefenseSwitchShieldReduction = BindServerSync(config,
                "Sword Tree",
                "Tier5_방어전환_방패착용시피해감소",
                8f,
                "Tier 5: 방어 전환(sword_step5_defswitch) - 방패 착용 시 받는 피해 감소 (%)"
            );

            SwordStep5DefenseSwitchNoShieldBonus = BindServerSync(config,
                "Sword Tree",
                "Tier5_방어전환_방패미착용시공격력보너스",
                30f,
                "Tier 5: 방어 전환(sword_step5_defswitch) - 방패 미착용 시 공격력 보너스 (%)"
            );

            // Tier 6: 일도양단
            SwordStep6UltimateSlashMultiplier = BindServerSync(config,
                "Sword Tree",
                "Tier6_일도양단_액티브스킬공격력보너스",
                100f,
                "Tier 6: 일도양단(sword_step6_ultimate_slash) - 액티브 스킬 공격력 보너스 (%)"
            );

            Plugin.Log.LogDebug("[SkillTreeConfig] 검 전문가 트리 설정 초기화 완료");

            // === 검(Sword) 전문가 스킬 설정 (분리된 컨피그 시스템) ===
            Sword_Config.Initialize(config);

            // === 둔기(Mace) 전문가 스킬 설정 (분리된 컨피그 시스템) ===
            Mace_Config.Initialize(config);

            // === 창(Spear) 전문가 스킬 설정 ===
            SpearStep1AttackSpeed = BindServerSync(config,
                "Spear Tree",
                "Tier1_창전문가_2연속공격속도보너스",
                10f,
                "Tier 1: 창 전문가(spear_step1_expert) - 2연속 공격 속도 보너스 (%)"
            );

            SpearStep1DamageBonus = BindServerSync(config,
                "Spear Tree",
                "Tier1_창전문가_2연속공격력보너스",
                7f,
                "Tier 1: 창 전문가(spear_step1_expert) - 2연속 공격력 보너스 (%)"
            );

            SpearStep1Duration = BindServerSync(config,
                "Spear Tree",
                "Tier1_창전문가_효과지속시간",
                4f,
                "Tier 1: 창 전문가(spear_step1_expert) - 효과 지속시간 (초)"
            );

            SpearStep1ThrowCooldown = BindServerSync(config,
                "Spear Tree",
                "Tier1_투창전문가_투창쿨타임",
                30f,
                "Tier 1: 투창 전문가(spear_step1_throw) - 투창 쿨타임 (초)"
            );

            SpearStep1ThrowDamage = BindServerSync(config,
                "Spear Tree",
                "Tier1_투창전문가_투창데미지배율",
                120f,
                "Tier 1: 투창 전문가(spear_step1_throw) - 투창 데미지 배율 (%)"
            );

            SpearStep1ThrowBuffDuration = BindServerSync(config,
                "Spear Tree",
                "Tier1_투창전문가_버프지속시간_사용안함",
                15f,
                "Tier 1: 투창 전문가(spear_step1_throw) - 사용 안 함 (패시브 스킬로 변경됨)"
            );

            SpearStep1CritDamageBonus = BindServerSync(config,
                "Spear Tree",
                "Tier1_급소찌르기_창공격력보너스",
                20f,
                "Tier 1: 급소 찌르기(spear_step1_crit) - 창 공격력 보너스 (%)"
            );

            SpearStep2EvasionDamageBonus = BindServerSync(config,
                "Spear Tree",
                "Tier2_회피찌르기_구르기직후피해보너스",
                25f,
                "Tier 2: 회피 찌르기(spear_step2_evasion) - 구르기 직후 공격 피해 보너스 (%)"
            );

            SpearStep3PierceDamageBonus = BindServerSync(config,
                "Spear Tree",
                "Tier3_연격창_무기공격력보너스",
                4f,
                "Tier 3: 연격창(spear_Step3_pierce) - 무기 공격력 보너스 (고정 수치)"
            );

            SpearStep3QuickDamageBonus = BindServerSync(config,
                "Spear Tree",
                "Tier3_쾌속창_투창공격력보너스",
                40f,
                "Tier 3: 쾌속 창(spear_step3_quick) - 투창 공격력 보너스 (%)"
            );

            SpearStep4TripleDamageBonus = BindServerSync(config,
                "Spear Tree",
                "Tier4_삼연창_3연속공격공격력보너스",
                20f,
                "Tier 4: 삼연창(spear_step4_triple) - 3연속 공격 공격력 보너스 (%)"
            );

            SpearStep5PenetrateCritChance = BindServerSync(config,
                "Spear Tree",
                "Tier5_꿰뚫는창_치명타확률",
                12f,
                "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 치명타 확률 (%)"
            );

            SpearStep5ComboCooldown = BindServerSync(config,
                "Spear Tree",
                "Tier5_연공창_G키쿨타임",
                25f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 쿨타임 (초)"
            );

            SpearStep5ComboDamage = BindServerSync(config,
                "Spear Tree",
                "Tier5_연공창_G키데미지배율",
                280f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 데미지 배율 (%)"
            );

            SpearStep5ComboStaminaCost = BindServerSync(config,
                "Spear Tree",
                "Tier5_연공창_G키스태미나소모",
                20f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 스태미나 소모 (%)"
            );

            SpearStep5ComboKnockbackRadius = BindServerSync(config,
                "Spear Tree",
                "Tier5_연공창_G키넉백범위",
                3f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 넉백 범위 (m)"
            );

            SpearStep5ComboRange = BindServerSync(config,
                "Spear Tree",
                "Tier5_연공창_액티브효과범위",
                3f,
                "Tier 5: 연공창(spear_step5_combo_active) - 액티브 효과 범위 (m)"
            );

            Plugin.Log.LogDebug("[SkillTreeConfig] 창 전문가 트리 설정 초기화 완료");

            // === 폴암 전문가 트리 설정 ===
            // Tier 0: 폴암 전문가
            PolearmExpertRangeBonus = BindServerSync(config,
                "Polearm Tree",
                "Tier0_폴암전문가_공격범위보너스",
                15f,
                "Tier 0: 폴암 전문가(polearm_expert) - 공격 범위 보너스 (%)"
            );

            // Tier 1: 회전베기
            PolearmStep1SpinWheelDamage = BindServerSync(config,
                "Polearm Tree",
                "Tier1_회전베기_휠마우스공격력보너스",
                60f,
                "Tier 1: 회전베기(polearm_step1_spin_wheel) - 휠 마우스 공격력 보너스 (%)"
            );

            // Tier 1: 제압 공격
            PolearmStep1SuppressDamage = BindServerSync(config,
                "Polearm Tree",
                "Tier1_제압공격_공격력보너스",
                30f,
                "Tier 1: 제압 공격(polearm_step1_suppress) - 공격력 보너스 (%)"
            );

            // Tier 2: 영웅 타격
            PolearmStep2HeroKnockbackChance = BindServerSync(config,
                "Polearm Tree",
                "Tier2_영웅타격_넉백발생확률",
                27f,
                "Tier 2: 영웅 타격(polearm_step2_hero) - 넉백 발생 확률 (%)"
            );

            // Tier 3: 광역 강타
            PolearmStep3AreaComboBonus = BindServerSync(config,
                "Polearm Tree",
                "Tier3_광역강타_2연속공격보너스",
                25f,
                "Tier 3: 광역 강타(polearm_step3_area_combo) - 2연속 공격 보너스 (%)"
            );

            PolearmStep3AreaComboDuration = BindServerSync(config,
                "Polearm Tree",
                "Tier3_광역강타_2연속공격지속시간",
                5f,
                "Tier 3: 광역 강타(polearm_step3_area_combo) - 2연속 공격 지속시간 (초)"
            );

            // Tier 3: 지면 강타
            PolearmStep3GroundWheelDamage = BindServerSync(config,
                "Polearm Tree",
                "Tier3_지면강타_휠마우스공격력보너스",
                80f,
                "Tier 3: 지면 강타(polearm_step3_ground_wheel) - 휠 마우스 공격력 보너스 (%)"
            );

            // Tier 4: 반달 베기
            PolearmStep4MoonRangeBonus = BindServerSync(config,
                "Polearm Tree",
                "Tier4_반달베기_공격범위보너스",
                15f,
                "Tier 4: 반달 베기(polearm_step4_moon) - 공격 범위 보너스 (%)"
            );

            PolearmStep4MoonStaminaReduction = BindServerSync(config,
                "Polearm Tree",
                "Tier4_반달베기_공격스태미나감소",
                15f,
                "Tier 4: 반달 베기(polearm_step4_moon) - 공격 스태미나 감소 (%)"
            );

            // Tier 4: 폴암강화
            PolearmStep4ChargeDamageBonus = BindServerSync(config,
                "Polearm Tree",
                "Tier4_폴암강화_무기공격력보너스",
                5f,
                "Tier 4: 폴암강화(polearm_step4_charge) - 무기 공격력 보너스 (고정 수치)"
            );

            // Tier 5: 장창의 제왕
            PolearmStep5KingHealthThreshold = BindServerSync(config,
                "Polearm Tree",
                "Tier5_장창의제왕_추가피해적용체력임계값",
                50f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 추가 피해 적용 체력 임계값 (%)"
            );

            PolearmStep5KingDamageBonus = BindServerSync(config,
                "Polearm Tree",
                "Tier5_장창의제왕_추가피해보너스",
                50f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 추가 피해 보너스 (%)"
            );

            PolearmStep5KingStaminaCost = BindServerSync(config,
                "Polearm Tree",
                "Tier5_장창의제왕_스태미나소모량",
                15f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 스태미나 소모량 (%)"
            );

            PolearmStep5KingCooldown = BindServerSync(config,
                "Polearm Tree",
                "Tier5_장창의제왕_쿨다운시간",
                15f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 쿨다운 시간 (초)"
            );

            PolearmStep5KingDuration = BindServerSync(config,
                "Polearm Tree",
                "Tier5_장창의제왕_지속시간",
                10f,
                "Tier 5: 장창의 제왕(polearm_step5_king) - 지속시간 (초)"
            );

            Plugin.Log.LogDebug("[SkillTreeConfig] 폴암 전문가 트리 설정 초기화 완료");

            // === 직업 스킬들 초기화 (컨피그 리스트 최하단에 배치) ===

            // === 아처 직업 스킬 설정 (분리된 컨피그 시스템) ===
            Archer_Config.InitializeArcherConfig(config);

            // === 메이지 직업 스킬 설정 (분리된 컨피그 시스템) ===
            Mage_Config.InitializeMageConfig(config);

            // === 탱커 직업 스킬 설정 (분리된 컨피그 시스템) ===
            Tanker_Config.InitializeTankerConfig(config);

            // === 로그 직업 스킬 설정 (분리된 컨피그 시스템) ===
            Rogue_Config.InitializeRogueConfig(config);

            // === 성기사 직업 스킬 설정 (분리된 컨피그 시스템) ===
            Paladin_Config.InitializePaladinConfig();

            // === 버서커 직업 스킬 설정 (분리된 컨피그 시스템) ===
            Berserker_Config.InitializeBerserkerConfig();

            // ConfigFile 저장
            _configFile = config;

            // 서버라면 모든 클라이언트에게 설정 전송 + Config 파일 감시 시작
            if (_isServer)
            {
                BroadcastConfigToClients();
                StartConfigFileWatcher(config);
            }

            // Jotunn SynchronizationManager 이벤트 등록
            InitializeJotunnSyncEvents();
        }

        /// <summary>
        /// Jotunn SynchronizationManager 동기화 이벤트 초기화
        /// 서버 Config 동기화 완료 시점을 정확히 감지하여 효과 재계산
        /// </summary>
        private static void InitializeJotunnSyncEvents()
        {
            try
            {
                // 서버 Config 동기화 완료 이벤트
                SynchronizationManager.OnConfigurationSynchronized += (sender, args) =>
                {
                    if (args.InitialSynchronization)
                    {
                        Plugin.Log.LogInfo("[SkillTreeConfig] Jotunn 서버 설정 초기 동기화 완료");
                        _hasReceivedServerConfig = true;

                        // 모든 스킬 효과 재계산 요청
                        try
                        {
                            RefreshAllSkillEffects();
                        }
                        catch (Exception ex)
                        {
                            Plugin.Log.LogWarning($"[SkillTreeConfig] 스킬 효과 재계산 중 오류: {ex.Message}");
                        }
                    }
                    else
                    {
                        Plugin.Log.LogDebug("[SkillTreeConfig] Jotunn 서버 설정 재동기화 완료");
                    }
                };

                // Admin 권한 변경 감지 이벤트
                SynchronizationManager.OnAdminStatusChanged += () =>
                {
                    Plugin.Log.LogInfo($"[SkillTreeConfig] Admin 상태 변경: {(SynchronizationManager.Instance.PlayerIsAdmin ? "관리자" : "일반 사용자")}");
                };

                Plugin.Log.LogDebug("[SkillTreeConfig] Jotunn 동기화 이벤트 등록 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[SkillTreeConfig] Jotunn 동기화 이벤트 등록 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 모든 스킬 효과 재계산 (Config 동기화 후 호출)
        /// </summary>
        private static void RefreshAllSkillEffects()
        {
            // 로컬 플레이어가 있을 때만 실행
            if (Player.m_localPlayer == null) return;

            Plugin.Log.LogDebug("[SkillTreeConfig] 서버 Config 동기화 후 스킬 효과 재계산 시작");

            // StatusEffect 업데이트 트리거
            try
            {
                var statusEffect = Player.m_localPlayer.GetSEMan()?.GetStatusEffect("SE_StatTreeSpeed".GetHashCode());
                if (statusEffect != null)
                {
                    statusEffect.ResetTime();
                    Plugin.Log.LogDebug("[SkillTreeConfig] SE_StatTreeSpeed 효과 재설정 완료");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[SkillTreeConfig] StatusEffect 업데이트 중 오류: {ex.Message}");
            }
        }

        // 서버/클라이언트 모드 감지
        private static void DetectServerClientMode()
        {
            // 싱글플레이어이거나 전용 서버인 경우 서버로 취급
            _isServer = ZNet.instance == null || ZNet.instance.IsServer();
            
            Plugin.Log.LogDebug($"[SkillTreeConfig] 모드 감지: {(_isServer ? "서버" : "클라이언트")}");
        }

        // 효과적인 값 반환 (서버 값 우선)
        public static float GetEffectiveValue(string key, float localValue)
        {
            // 클라이언트이고 서버 설정을 받았다면 서버 값 사용
            if (!_isServer && _hasReceivedServerConfig && _serverConfigValues.ContainsKey(key))
            {
                return _serverConfigValues[key];
            }
            // 서버이거나 서버 설정을 받지 못했다면 로컬 값 사용
            return localValue;
        }

        // 서버 → 클라이언트 설정 전송
        public static void BroadcastConfigToClients()
        {
            if (!_isServer) return;

            var configData = new Dictionary<string, float>
            {
                // 루트
                ["Attack_Expert_Damage"] = AttackRootDamageBonus.Value,
                
                // 2단계 (4가지)
                ["Attack_Step2_MeleeBonusChance"] = AttackMeleeBonusChance.Value,
                ["Attack_Step2_MeleeBonusDamage"] = AttackMeleeBonusDamage.Value,
                ["Attack_Step2_BowBonusChance"] = AttackBowBonusChance.Value,
                ["Attack_Step2_BowBonusDamage"] = AttackBowBonusDamage.Value,
                ["Attack_Step2_CrossbowBonusChance"] = AttackCrossbowBonusChance.Value,
                ["Attack_Step2_CrossbowBonusDamage"] = AttackCrossbowBonusDamage.Value,
                ["Attack_Step2_StaffBonusChance"] = AttackStaffBonusChance.Value,
                ["Attack_Step2_StaffBonusDamage"] = AttackStaffBonusDamage.Value,
                
                // 3단계 (1가지)
                ["Attack_Step3_StatBonus"] = AttackStatBonus.Value,
                
                // 4단계 (3가지)
                ["Attack_Step4_CritChance"] = AttackCritChance.Value,
                ["Attack_Step4_MeleeEnhancement"] = AttackMeleeEnhancement.Value,
                ["Attack_Step4_RangedEnhancement"] = AttackRangedEnhancement.Value,
                
                // 5단계 (1가지)
                ["Attack_Step5_SpecialStat"] = AttackSpecialStat.Value,
                
                // 6단계 (4가지)
                ["Attack_Step6_CritDamageBonus"] = AttackCritDamageBonus.Value,
                ["Attack_Step6_TwoHandedBonus"] = AttackTwoHandedBonus.Value,
                ["Attack_Step6_StaffElemental"] = AttackStaffElemental.Value,
                ["Attack_Step6_FinisherMeleeBonus"] = AttackFinisherMeleeBonus.Value,

                // === 속도 전문가 설정들 ===
                // 루트
                ["Speed_Expert_MoveSpeed"] = SpeedRootMoveSpeed.Value,
                
                // 1단계
                ["Speed_Step1_AttackSpeed"] = SpeedBaseAttackSpeed.Value,
                ["Speed_Step1_DodgeSpeed"] = SpeedBaseDodgeSpeed.Value,
                
                // 2단계 (4가지)
                ["Speed_Step2_MeleeComboStamina"] = SpeedMeleeComboStamina.Value,
                ["Speed_Step2_MeleeComboDuration"] = SpeedMeleeComboDuration.Value,
                ["Speed_Step2_CrossbowExpertSpeed"] = SpeedCrossbowExpertSpeed.Value,
                ["Speed_Step2_CrossbowExpertDuration"] = SpeedCrossbowExpertDuration.Value,
                ["Speed_Step2_BowExpertStamina"] = SpeedBowExpertStamina.Value,
                ["Speed_Step2_BowExpertDuration"] = SpeedBowExpertDuration.Value,
                ["Speed_Step2_StaffCastSpeed"] = SpeedStaffCastSpeed.Value,
                
                // 4단계
                ["Speed_Step4_FoodEfficiency"] = SpeedFoodEfficiency.Value,
                ["Speed_Step4_ShipBonus"] = SpeedShipBonus.Value,
                
                // 5단계
                ["Speed_Step5_CooldownReduction"] = SpeedCooldownReduction.Value,
                
                // 8단계 (4가지)
                ["Speed_Step8_MeleeAttackSpeed"] = SpeedMeleeAttackSpeed.Value,
                ["Speed_Step8_CrossbowDrawSpeed"] = SpeedCrossbowDrawSpeed.Value,
                ["Speed_Step8_BowDrawSpeed"] = SpeedBowDrawSpeed.Value,
                ["Speed_Step8_StaffCastSpeedFinal"] = SpeedStaffCastSpeedFinal.Value,

                // === 방어 전문가 설정들 ===
                ["Defense_Stomp_Radius"] = Defense_Config.StompRadius.Value,
                ["Defense_Stomp_Knockback"] = Defense_Config.StompKnockback.Value,
                ["Defense_Stomp_Cooldown"] = Defense_Config.StompCooldown.Value,
                ["Defense_Stomp_HealthThreshold"] = Defense_Config.StompHealthThreshold.Value,
                ["Defense_Stomp_VFXDuration"] = Defense_Config.StompVFXDuration.Value,

                // === 검 전문가 설정들 ===
                ["sword_expert_damage"] = SwordExpertDamage.Value,

                // === 단검 전문가 설정들은 Knife_Config.cs에서 처리됨 ===

                // === 방어 전문가 보너스 설정들 ===
                ["Defense_Root_HealthBonus"] = Defense_Config.DefenseRootHealthBonus?.Value ?? 5f,
                ["Defense_Survival_HealthBonus"] = Defense_Config.SurvivalHealthBonus?.Value ?? 5f,
                ["Defense_Dodge_StaminaBonus"] = Defense_Config.DodgeStaminaBonus?.Value ?? 10f,
                ["Defense_Dodge_EitrBonus"] = Defense_Config.DodgeEitrBonus?.Value ?? 10f,
                ["Defense_Health_HealthBonus"] = Defense_Config.HealthBonus?.Value ?? 20f,
                ["Defense_Breath_EitrBonus"] = Defense_Config.BreathEitrBonus?.Value ?? 10f,
                ["Defense_Boost_HealthBonus"] = Defense_Config.BoostHealthBonus?.Value ?? 15f,
                ["Defense_TrollRegen_Bonus"] = Defense_Config.TrollRegenBonus?.Value ?? 5f,
                ["Defense_TrollRegen_Interval"] = Defense_Config.TrollRegenInterval?.Value ?? 2f,
                ["Defense_Body_HealthBonus"] = Defense_Config.BodyHealthBonus?.Value ?? 30f,
                ["Defense_Body_ArmorBonus"] = Defense_Config.BodyArmorBonus?.Value ?? 10f,
                ["Defense_ShieldTraining_BlockPower"] = Defense_Config.ShieldTrainingBlockPowerBonus?.Value ?? 100f,
                ["Defense_ParryMaster_BlockPower"] = Defense_Config.ParryMasterBlockPowerBonus?.Value ?? 100f,
                ["Defense_ParryMaster_ParryDuration"] = Defense_Config.ParryMasterParryDurationBonus?.Value ?? 1f,
                ["Defense_JotunnShield_BlockStaminaReduction"] = Defense_Config.JotunnShieldBlockStaminaReduction?.Value ?? 25f,
                ["Defense_JotunnShield_NormalSpeedBonus"] = Defense_Config.JotunnShieldNormalSpeedBonus?.Value ?? 5f,
                ["Defense_JotunnShield_TowerSpeedBonus"] = Defense_Config.JotunnShieldTowerSpeedBonus?.Value ?? 10f,

                // === 창 전문가 설정들 ===
                ["spear_Step1_attack_speed"] = SpearStep1AttackSpeed.Value,
                ["spear_Step1_damage_bonus"] = SpearStep1DamageBonus.Value,
                ["spear_Step1_duration"] = SpearStep1Duration.Value,
                ["spear_Step1_throw_cooldown"] = SpearStep1ThrowCooldown.Value,
                ["spear_Step1_throw_damage"] = SpearStep1ThrowDamage.Value,
                ["spear_Step1_throw_buff_duration"] = SpearStep1ThrowBuffDuration.Value,
                ["spear_Step1_crit_damage_bonus"] = SpearStep1CritDamageBonus.Value,
                ["spear_Step2_evasion_damage_bonus"] = SpearStep2EvasionDamageBonus.Value,
                ["spear_Step3_pierce_damage"] = SpearStep3PierceDamageBonus.Value,

                // === 폴암 전문가 설정들 ===
                ["polearm_step4_charge_damage"] = PolearmStep4ChargeDamageBonus.Value,
            };

            // 간단한 문자열 직렬화 (BepInEx 환경용)
            var configString = SerializeConfigData(configData);
            
            // ZNet RPC를 통해 모든 클라이언트에게 전송
            if (ZNet.instance != null)
            {
                ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, "CaptainSkillTree.SkillTreeMod_ConfigSync", configString);
                Plugin.Log.LogInfo("[SkillTreeConfig] 서버 설정을 모든 클라이언트에게 전송");
            }
        }

        // 간단한 문자열 직렬화 메서드 (BepInEx 환경용)
        private static string SerializeConfigData(Dictionary<string, float> configData)
        {
            var sb = new StringBuilder();
            foreach (var kvp in configData)
            {
                sb.AppendLine($"{kvp.Key}={kvp.Value}");
            }
            return sb.ToString();
        }

        // 간단한 문자열 역직렬화 메서드 (BepInEx 환경용)
        private static Dictionary<string, float> DeserializeConfigData(string configString)
        {
            var result = new Dictionary<string, float>();
            if (string.IsNullOrEmpty(configString)) return result;

            var lines = configString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && float.TryParse(parts[1], out float value))
                {
                    result[parts[0]] = value;
                }
            }
            return result;
        }

        // 클라이언트에서 서버 설정 수신
        public static void ReceiveServerConfig(string configString)
        {
            try
            {
                var serverConfig = DeserializeConfigData(configString);
                if (serverConfig != null && serverConfig.Count > 0)
                {
                    _serverConfigValues = serverConfig;
                    _hasReceivedServerConfig = true;

                    // 클라이언트의 회피율을 새 Config 값으로 업데이트
                    var player = Player.m_localPlayer;
                    if (player != null)
                    {
                        SkillEffect.UpdateDefenseDodgeRate(player);
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeConfig] 서버 설정 수신 실패: {ex.Message}");
            }
        }

        // 설정 리로드 (서버 전용)
        public static void ReloadAndBroadcast()
        {
            if (!_isServer) return;
            
            Plugin.Log.LogInfo("[SkillTreeConfig] 설정 리로드 및 재전송");
            BroadcastConfigToClients();
        }

        // 개별 설정 변경 (서버 전용)
        public static bool SetConfigValue(string key, float value)
        {
            if (!_isServer) return false;

            try
            {
                switch (key)
                {
                    // 루트
                    case "Attack_Expert_Damage":
                        AttackRootDamageBonus.Value = value;
                        break;
                    
                    // 2단계 (4가지)
                    case "Attack_Step2_MeleeBonusChance":
                        AttackMeleeBonusChance.Value = value;
                        break;
                    case "Attack_Step2_MeleeBonusDamage":
                        AttackMeleeBonusDamage.Value = value;
                        break;
                    case "Attack_Step2_BowBonusChance":
                        AttackBowBonusChance.Value = value;
                        break;
                    case "Attack_Step2_BowBonusDamage":
                        AttackBowBonusDamage.Value = value;
                        break;
                    case "bow_Step2_focus_crit_bonus":
                        BowStep2FocusCritBonus.Value = value;
                        break;
                    case "bow_Step3_speedshot_skill_bonus":
                        BowStep3SpeedShotSkillBonus.Value = value;
                        break;
                    case "bow_Step3_silentshot_damage_bonus":
                        BowStep3SilentShotDamageBonus.Value = value;
                        break;
                    case "Attack_Step2_CrossbowBonusChance":
                        AttackCrossbowBonusChance.Value = value;
                        break;
                    case "Attack_Step2_CrossbowBonusDamage":
                        AttackCrossbowBonusDamage.Value = value;
                        break;
                    case "Attack_Step2_StaffBonusChance":
                        AttackStaffBonusChance.Value = value;
                        break;
                    case "Attack_Step2_StaffBonusDamage":
                        AttackStaffBonusDamage.Value = value;
                        break;
                    
                    // 3단계 (1가지)
                    case "Attack_Step3_StatBonus":
                        AttackStatBonus.Value = value;
                        break;
                    
                    // 4단계 (2가지)
                    case "Attack_Step4_CritChance":
                        AttackCritChance.Value = value;
                        break;
                    case "Attack_Step4_MeleeEnhancement":
                        AttackMeleeEnhancement.Value = value;
                        break;
                    case "Attack_Step4_RangedEnhancement":
                        AttackRangedEnhancement.Value = value;
                        break;
                    
                    // 5단계 (1가지)
                    case "Attack_Step5_SpecialStat":
                        AttackSpecialStat.Value = value;
                        break;
                    
                    // 6단계 (4가지)
                    case "Attack_Step6_CritDamageBonus":
                        AttackCritDamageBonus.Value = value;
                        break;
                    case "Attack_Step6_TwoHandedBonus":
                        AttackTwoHandedBonus.Value = value;
                        break;
                    case "Attack_Step6_StaffElemental":
                        AttackStaffElemental.Value = value;
                        break;
                    case "Attack_Step6_FinisherMeleeBonus":
                        AttackFinisherMeleeBonus.Value = value;
                        break;

                    // === 속도 전문가 케이스들 ===
                    // 루트
                    case "Speed_Expert_MoveSpeed":
                        SpeedRootMoveSpeed.Value = value;
                        break;
                    
                    // 1단계
                    case "Speed_Step1_AttackSpeed":
                        SpeedBaseAttackSpeed.Value = value;
                        break;
                    case "Speed_Step1_DodgeSpeed":
                        SpeedBaseDodgeSpeed.Value = value;
                        break;
                    
                    // 2단계 (6가지)
                    case "Speed_Step2_MeleeComboStamina":
                        SpeedMeleeComboStamina.Value = value;
                        break;
                    case "Speed_Step2_MeleeComboDuration":
                        SpeedMeleeComboDuration.Value = value;
                        break;
                    case "Speed_Step2_CrossbowExpertSpeed":
                        SpeedCrossbowExpertSpeed.Value = value;
                        break;
                    case "Speed_Step2_CrossbowExpertDuration":
                        SpeedCrossbowExpertDuration.Value = value;
                        break;
                    case "Speed_Step2_BowExpertStamina":
                        SpeedBowExpertStamina.Value = value;
                        break;
                    case "Speed_Step2_BowExpertDuration":
                        SpeedBowExpertDuration.Value = value;
                        break;
                    case "Speed_Step2_StaffCastSpeed":
                        SpeedStaffCastSpeed.Value = value;
                        break;
                    
                    // 4단계 (2가지)
                    case "Speed_Step4_FoodEfficiency":
                        SpeedFoodEfficiency.Value = value;
                        break;
                    case "Speed_Step4_ShipBonus":
                        SpeedShipBonus.Value = value;
                        break;
                    
                    // 5단계
                    case "Speed_Step5_CooldownReduction":
                        SpeedCooldownReduction.Value = value;
                        break;
                    
                    // 8단계 (4가지)
                    case "Speed_Step8_MeleeAttackSpeed":
                        SpeedMeleeAttackSpeed.Value = value;
                        break;
                    case "Speed_Step8_CrossbowDrawSpeed":
                        SpeedCrossbowDrawSpeed.Value = value;
                        break;
                    case "Speed_Step8_BowDrawSpeed":
                        SpeedBowDrawSpeed.Value = value;
                        break;
                    case "Speed_Step8_StaffCastSpeedFinal":
                        SpeedStaffCastSpeedFinal.Value = value;
                        break;
                    
                    // === 창 전문가 케이스들 ===
                    case "spear_Step1_attack_speed":
                        SpearStep1AttackSpeed.Value = value;
                        break;
                    case "spear_Step1_damage_bonus":
                        SpearStep1DamageBonus.Value = value;
                        break;
                    case "spear_Step1_duration":
                        SpearStep1Duration.Value = value;
                        break;
                    case "spear_Step1_throw_cooldown":
                        SpearStep1ThrowCooldown.Value = value;
                        break;
                    case "spear_Step1_throw_damage":
                        SpearStep1ThrowDamage.Value = value;
                        break;
                    case "spear_Step1_throw_buff_duration":
                        SpearStep1ThrowBuffDuration.Value = value;
                        break;
                    case "spear_Step1_crit_damage_bonus":
                        SpearStep1CritDamageBonus.Value = value;
                        break;
                    case "spear_Step2_evasion_damage_bonus":
                        SpearStep2EvasionDamageBonus.Value = value;
                        break;
                    case "spear_Step3_pierce_damage":
                        SpearStep3PierceDamageBonus.Value = value;
                        break;
                    case "spear_Step3_quick_damage_bonus":
                        SpearStep3QuickDamageBonus.Value = value;
                        break;
                    case "spear_Step4_triple_damage_bonus":
                        SpearStep4TripleDamageBonus.Value = value;
                        break;
                    case "spear_Step5_penetrate_crit_chance":
                        SpearStep5PenetrateCritChance.Value = value;
                        break;

                    // === 폴암 전문가 케이스들 ===
                    case "polearm_step4_charge_damage":
                        PolearmStep4ChargeDamageBonus.Value = value;
                        break;

                    // === 방어 전문가 케이스들 ===
                    case "Defense_Stomp_Radius":
                        Defense_Config.StompRadius.Value = value;
                        break;
                    case "Defense_Stomp_Knockback":
                        Defense_Config.StompKnockback.Value = value;
                        break;
                    case "Defense_Stomp_Cooldown":
                        Defense_Config.StompCooldown.Value = value;
                        break;
                    case "Defense_Stomp_HealthThreshold":
                        Defense_Config.StompHealthThreshold.Value = value;
                        break;
                    case "Defense_Stomp_VFXDuration":
                        Defense_Config.StompVFXDuration.Value = value;
                        break;

                    // === 단검 전문가 케이스들은 Knife_Config.cs에서 처리됨 ===

                    // === Sword Slash 액티브 스킬 케이스들 ===
                    case "sword_slash_attack_count":
                        SwordSlashAttackCount.Value = (int)value;
                        break;
                    case "sword_slash_attack_interval":
                        SwordSlashAttackInterval.Value = value;
                        break;
                    case "sword_slash_damage_ratio":
                        SwordSlashDamageRatio.Value = value;
                        break;
                    case "sword_slash_stamina_cost":
                        SwordSlashStaminaCost.Value = value;
                        break;
                    case "sword_slash_cooldown":
                        SwordSlashCooldown.Value = value;
                        break;
                    
                    default:
                        return false;
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

        /// <summary>
        /// Config 파일 변경 감지 시작 (서버 전용)
        /// </summary>
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

                // 변경 감지 시 호출
                _configWatcher.Changed += OnConfigFileChanged;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeConfig] Config 파일 감시 시작 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// Config 파일 변경 감지 이벤트 핸들러
        /// </summary>
        private static void OnConfigFileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                // 모든 클라이언트에게 새 설정 전송 (서버만)
                if (_isServer)
                {
                    BroadcastConfigToClients();
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeConfig] Config 파일 변경 처리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// Config 파일 감시 중지
        /// </summary>
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
    }
}
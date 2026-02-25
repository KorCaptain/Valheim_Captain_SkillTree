using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 단검 전문가 전용 컨피그 시스템
    /// 모든 단검 스킬의 설정값들을 관리
    /// MMO 시스템과 연동하여 동적 값 변경 지원
    /// </summary>
    public static class Knife_Config
    {
        #region 단검 전문가 스킬 기본 설정

        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> KnifeExpertRequiredPoints;
        public static ConfigEntry<int> KnifeEvasionRequiredPoints;
        public static ConfigEntry<int> KnifeMoveSpeedRequiredPoints;
        public static ConfigEntry<int> KnifeAttackSpeedRequiredPoints;
        public static ConfigEntry<int> KnifeCritRateRequiredPoints;
        public static ConfigEntry<int> KnifeCombatDamageRequiredPoints;
        public static ConfigEntry<int> KnifeExecutionRequiredPoints;
        public static ConfigEntry<int> KnifeAssassinationRequiredPoints;
        public static ConfigEntry<int> KnifeAssassinHeartRequiredPoints;

        // === 단검 전문가 효과 설정 ===
        public static ConfigEntry<float> KnifeExpertBackstabBonus;
        public static ConfigEntry<float> KnifeEvasionBonus;
        public static ConfigEntry<float> KnifeEvasionDuration;
        public static ConfigEntry<float> KnifeMoveSpeedBonus;
        public static ConfigEntry<float> KnifeAttackDamageBonus;
        public static ConfigEntry<float> KnifeAttackDamageDuration;
        public static ConfigEntry<float> KnifeAttackEvasionBonus;
        public static ConfigEntry<float> KnifeAttackEvasionDuration;
        public static ConfigEntry<float> KnifeAttackEvasionCooldown;
        public static ConfigEntry<float> KnifeCombatDamageBonus;
        public static ConfigEntry<float> KnifeExecutionCritDamage;
        public static ConfigEntry<float> KnifeExecutionCritChance;
        public static ConfigEntry<float> KnifeAssassinationStaggerChance;
        public static ConfigEntry<int> KnifeAssassinationRequiredHits;

        // === 암살자의 심장 (G키 액티브 스킬) 설정 ===
        public static ConfigEntry<float> KnifeAssassinHeartCritDamage;
        public static ConfigEntry<float> KnifeAssassinHeartDuration;
        public static ConfigEntry<float> KnifeAssassinHeartStaminaCost;
        public static ConfigEntry<float> KnifeAssassinHeartCooldown;
        public static ConfigEntry<float> KnifeAssassinHeartTeleportRange;
        public static ConfigEntry<float> KnifeAssassinHeartTeleportBehind;
        public static ConfigEntry<float> KnifeAssassinHeartStunDuration;
        public static ConfigEntry<int> KnifeAssassinHeartAttackCount;
        public static ConfigEntry<float> KnifeAssassinHeartAttackInterval;

        #endregion

        #region Dynamic Value Properties (MMO 연동)

        // === 필요 포인트 동적 값 ===
        public static int KnifeExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_expert_required_points", (float)(KnifeExpertRequiredPoints?.Value ?? 1));
        public static int KnifeEvasionRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step2_required_points", (float)(KnifeEvasionRequiredPoints?.Value ?? 1));
        public static int KnifeMoveSpeedRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step3_required_points", (float)(KnifeMoveSpeedRequiredPoints?.Value ?? 1));
        public static int KnifeAttackSpeedRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step4_required_points", (float)(KnifeAttackSpeedRequiredPoints?.Value ?? 1));
        public static int KnifeCritRateRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step5_required_points", (float)(KnifeCritRateRequiredPoints?.Value ?? 1));
        public static int KnifeCombatDamageRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step6_required_points", (float)(KnifeCombatDamageRequiredPoints?.Value ?? 1));
        public static int KnifeExecutionRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step7_required_points", (float)(KnifeExecutionRequiredPoints?.Value ?? 1));
        public static int KnifeAssassinationRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step8_required_points", (float)(KnifeAssassinationRequiredPoints?.Value ?? 1));
        public static int KnifeAssassinHeartRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step9_required_points", (float)(KnifeAssassinHeartRequiredPoints?.Value ?? 1));

        // === 단검 전문가 동적 값 ===
        public static float KnifeExpertBackstabBonusValue => SkillTreeConfig.GetEffectiveValue("knife_expert_backstab_bonus", KnifeExpertBackstabBonus?.Value ?? 200f);
        public static float KnifeEvasionBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step2_evasion_bonus", KnifeEvasionBonus?.Value ?? 30f);
        public static float KnifeEvasionDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step2_evasion_duration", KnifeEvasionDuration?.Value ?? 3f);
        public static float KnifeMoveSpeedBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step3_move_speed_bonus", KnifeMoveSpeedBonus?.Value ?? 20f);
        public static float KnifeAttackDamageBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step4_attack_damage_bonus", KnifeAttackDamageBonus?.Value ?? 2f);
        public static float KnifeAttackDamageDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step4_attack_damage_duration", KnifeAttackDamageDuration?.Value ?? 10f);
        public static float KnifeAttackEvasionBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step5_attack_evasion_bonus", KnifeAttackEvasionBonus?.Value ?? 25f);
        public static float KnifeAttackEvasionDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step5_attack_evasion_duration", KnifeAttackEvasionDuration?.Value ?? 10f);
        public static float KnifeAttackEvasionCooldownValue => SkillTreeConfig.GetEffectiveValue("knife_step5_attack_evasion_cooldown", KnifeAttackEvasionCooldown?.Value ?? 30f);
        public static float KnifeCombatDamageBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step6_combat_damage_bonus", KnifeCombatDamageBonus?.Value ?? 25f);
        public static float KnifeExecutionCritDamageValue => SkillTreeConfig.GetEffectiveValue("knife_step7_execution_crit_damage", KnifeExecutionCritDamage?.Value ?? 25f);
        public static float KnifeExecutionCritChanceValue => SkillTreeConfig.GetEffectiveValue("knife_step7_execution_crit_chance", KnifeExecutionCritChance?.Value ?? 12f);
        public static float KnifeAssassinationStaggerChanceValue => SkillTreeConfig.GetEffectiveValue("knife_step8_assassination_stagger_chance", KnifeAssassinationStaggerChance?.Value ?? 38f);
        public static int KnifeAssassinationRequiredHitsValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step8_assassination_required_hits", (float)(KnifeAssassinationRequiredHits?.Value ?? 3));

        // === 암살자의 심장 동적 값 ===
        public static float KnifeAssassinHeartCritDamageValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_crit_damage", KnifeAssassinHeartCritDamage?.Value ?? 2f);
        public static float KnifeAssassinHeartDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_duration", KnifeAssassinHeartDuration?.Value ?? 15f);
        public static float KnifeAssassinHeartStaminaCostValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_stamina_cost", KnifeAssassinHeartStaminaCost?.Value ?? 30f);
        public static float KnifeAssassinHeartCooldownValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_cooldown", KnifeAssassinHeartCooldown?.Value ?? 60f);
        public static float KnifeAssassinHeartTeleportRangeValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_teleport_range", KnifeAssassinHeartTeleportRange?.Value ?? 7f);
        public static float KnifeAssassinHeartTeleportBehindValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_teleport_behind", KnifeAssassinHeartTeleportBehind?.Value ?? 1f);
        public static float KnifeAssassinHeartStunDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_stun_duration", KnifeAssassinHeartStunDuration?.Value ?? 1f);
        public static int KnifeAssassinHeartAttackCountValue => (int)SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_attack_count", (float)(KnifeAssassinHeartAttackCount?.Value ?? 3));
        public static float KnifeAssassinHeartAttackIntervalValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_attack_interval", KnifeAssassinHeartAttackInterval?.Value ?? 0.3f);

        #endregion

        #region Initialization

        /// <summary>
        /// 단검 컨피그 초기화
        /// </summary>
        public static void InitializeKnifeConfig(ConfigFile config)
        {
            try
            {
                Plugin.Log.LogDebug("=== [단검 컨피그] 초기화 시작 ===");

                // === Tier 0: 단검 전문가 ===
                KnifeExpertBackstabBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier0_KnifeExpert_BackstabBonus",
                    35f,
                    SkillTreeConfig.GetConfigDescription("Tier0_KnifeExpert_BackstabBonus"));

                KnifeExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier0_KnifeExpert_RequiredPoints",
                    2,
                    SkillTreeConfig.GetConfigDescription("Tier0_KnifeExpert_RequiredPoints"));

                // === Tier 1: 회피 숙련 ===
                KnifeEvasionBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier1_Evasion_Chance",
                    20f,
                    SkillTreeConfig.GetConfigDescription("Tier1_Evasion_Chance"));

                KnifeEvasionDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier1_Evasion_Duration",
                    3f,
                    SkillTreeConfig.GetConfigDescription("Tier1_Evasion_Duration"));

                KnifeEvasionRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier1_Evasion_RequiredPoints",
                    2,
                    SkillTreeConfig.GetConfigDescription("Tier1_Evasion_RequiredPoints"));

                // === Tier 2: 빠른 움직임 ===
                KnifeMoveSpeedBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier2_FastMove_MoveSpeedBonus",
                    12f,
                    SkillTreeConfig.GetConfigDescription("Tier2_FastMove_MoveSpeedBonus"));

                KnifeMoveSpeedRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier2_FastMove_RequiredPoints",
                    2,
                    SkillTreeConfig.GetConfigDescription("Tier2_FastMove_RequiredPoints"));

                // === Tier 3: 전투 숙련 ===
                KnifeAttackDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier3_CombatMastery_DamageBonus",
                    2f,
                    SkillTreeConfig.GetConfigDescription("Tier3_CombatMastery_DamageBonus"));

                KnifeAttackDamageDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier3_CombatMastery_BuffDuration",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Tier3_CombatMastery_BuffDuration"));

                KnifeAttackSpeedRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier3_CombatMastery_RequiredPoints",
                    2,
                    SkillTreeConfig.GetConfigDescription("Tier3_CombatMastery_RequiredPoints"));

                // === Tier 4: 공격과 회피 ===
                KnifeAttackEvasionBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier4_AttackEvasion_EvasionBonus",
                    25f,
                    SkillTreeConfig.GetConfigDescription("Tier4_AttackEvasion_EvasionBonus"));

                KnifeAttackEvasionDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier4_AttackEvasion_BuffDuration",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Tier4_AttackEvasion_BuffDuration"));

                KnifeAttackEvasionCooldown = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier4_AttackEvasion_Cooldown",
                    30f,
                    SkillTreeConfig.GetConfigDescription("Tier4_AttackEvasion_Cooldown"));

                KnifeCritRateRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier4_AttackEvasion_RequiredPoints",
                    3,
                    SkillTreeConfig.GetConfigDescription("Tier4_AttackEvasion_RequiredPoints"));

                // === Tier 5: 치명적 피해 ===
                KnifeCombatDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier5_CriticalDamage_DamageBonus",
                    25f,
                    SkillTreeConfig.GetConfigDescription("Tier5_CriticalDamage_DamageBonus"));

                KnifeCombatDamageRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier5_CriticalDamage_RequiredPoints",
                    2,
                    SkillTreeConfig.GetConfigDescription("Tier5_CriticalDamage_RequiredPoints"));

                // === Tier 6: 암살자 ===
                KnifeExecutionCritDamage = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier6_Assassin_CritDamageBonus",
                    25f,
                    SkillTreeConfig.GetConfigDescription("Tier6_Assassin_CritDamageBonus"));

                KnifeExecutionCritChance = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier6_Assassin_CritChanceBonus",
                    12f,
                    SkillTreeConfig.GetConfigDescription("Tier6_Assassin_CritChanceBonus"));

                KnifeExecutionRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier6_Assassin_RequiredPoints",
                    2,
                    SkillTreeConfig.GetConfigDescription("Tier6_Assassin_RequiredPoints"));

                // === Tier 7: 암살술 ===
                KnifeAssassinationStaggerChance = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier7_Assassination_StaggerChance",
                    38f,
                    SkillTreeConfig.GetConfigDescription("Tier7_Assassination_StaggerChance"));

                KnifeAssassinationRequiredHits = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier7_Assassination_RequiredComboHits",
                    3,
                    SkillTreeConfig.GetConfigDescription("Tier7_Assassination_RequiredComboHits"));

                KnifeAssassinationRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier7_Assassination_RequiredPoints",
                    2,
                    SkillTreeConfig.GetConfigDescription("Tier7_Assassination_RequiredPoints"));

                // === Tier 8: 암살자의 심장 (G키 액티브) ===
                KnifeAssassinHeartCritDamage = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_CritDamageMultiplier",
                    1.3f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_CritDamageMultiplier"));

                KnifeAssassinHeartDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_Duration",
                    7f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_Duration"));

                KnifeAssassinHeartStaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_StaminaCost",
                    20f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_StaminaCost"));

                KnifeAssassinHeartCooldown = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_Cooldown",
                    40f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_Cooldown"));

                KnifeAssassinHeartTeleportRange = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_TeleportRange",
                    8f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_TeleportRange"));

                KnifeAssassinHeartTeleportBehind = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_TeleportBackDistance",
                    1f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_TeleportBackDistance"));

                KnifeAssassinHeartStunDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_StunDuration",
                    1f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_StunDuration"));

                KnifeAssassinHeartAttackCount = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_ComboAttackCount",
                    3,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_ComboAttackCount"));

                KnifeAssassinHeartAttackInterval = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_AttackInterval",
                    0.3f,
                    SkillTreeConfig.GetConfigDescription("Tier8_AssassinHeart_AttackInterval"));

                KnifeAssassinHeartRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree",
                    "Tier8_AssassinHeart_RequiredPoints",
                    3,
                    "Tier 8: Assassin's Heart (knife_step9_assassin_heart) - Required Points");

                Plugin.Log.LogDebug("[단검 컨피그] 모든 설정 로드 완료");

                // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
                RegisterKnifeEventHandlers();
                
                LogKnifeConfigValues();
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// 단검 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterKnifeEventHandlers()
        {
            try
            {
                // 필요 포인트 이벤트 핸들러
                KnifeExpertRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeEvasionRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeMoveSpeedRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackSpeedRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeCritRateRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeCombatDamageRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeExecutionRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinationRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartRequiredPoints.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();

                // 효과 설정 이벤트 핸들러
                KnifeExpertBackstabBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeEvasionBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeEvasionDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeMoveSpeedBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackDamageBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackDamageDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackEvasionBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackEvasionDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackEvasionCooldown.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeCombatDamageBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeExecutionCritDamage.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeExecutionCritChance.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinationStaggerChance.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinationRequiredHits.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                
                // 암살자의 심장 이벤트 핸들러
                KnifeAssassinHeartCritDamage.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartStaminaCost.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartCooldown.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartTeleportRange.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartTeleportBehind.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartStunDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartAttackCount.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartAttackInterval.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();

                Plugin.Log.LogDebug("[단검 컨피그] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        #endregion

        #region Debug Methods

        /// <summary>
        /// 단검 컨피그 값들 로그 출력
        /// </summary>
        public static void LogKnifeConfigValues()
        {
            try
            {
                Plugin.Log.LogDebug("=== [단검 컨피그] 현재 설정값 ===");
                Plugin.Log.LogDebug("=== 필요 포인트 ===");
                Plugin.Log.LogDebug($"단검 전문가 필요 포인트: {KnifeExpertRequiredPointsValue}");
                Plugin.Log.LogDebug($"회피 숙련 필요 포인트: {KnifeEvasionRequiredPointsValue}");
                Plugin.Log.LogDebug($"빠른 움직임 필요 포인트: {KnifeMoveSpeedRequiredPointsValue}");
                Plugin.Log.LogDebug($"빠른 공격 필요 포인트: {KnifeAttackSpeedRequiredPointsValue}");
                Plugin.Log.LogDebug($"치명타 숙련 필요 포인트: {KnifeCritRateRequiredPointsValue}");
                Plugin.Log.LogDebug($"피해로 필요 포인트: {KnifeCombatDamageRequiredPointsValue}");
                Plugin.Log.LogDebug($"처형술 필요 포인트: {KnifeExecutionRequiredPointsValue}");
                Plugin.Log.LogDebug($"암살자 필요 포인트: {KnifeAssassinationRequiredPointsValue}");
                Plugin.Log.LogDebug($"암살자의 심장 필요 포인트: {KnifeAssassinHeartRequiredPointsValue}");

                Plugin.Log.LogDebug("=== 스킬 효과 ===");
                Plugin.Log.LogDebug($"단검 전문가 백스탭 보너스: {KnifeExpertBackstabBonusValue}%");
                Plugin.Log.LogDebug($"회피 숙련 확률: {KnifeEvasionBonusValue}%, 지속시간: {KnifeEvasionDurationValue}초");
                Plugin.Log.LogDebug($"빠른 움직임: {KnifeMoveSpeedBonusValue}% (단검 착용 시 항상)");
                Plugin.Log.LogDebug($"빠른 공격 단검 데미지: +{KnifeAttackDamageBonusValue}, 지속시간: {KnifeAttackDamageDurationValue}초");
                Plugin.Log.LogDebug($"공격과 회피: 회피 +{KnifeAttackEvasionBonusValue}%, 지속시간: {KnifeAttackEvasionDurationValue}초, 쿨타임: {KnifeAttackEvasionCooldownValue}초");
                Plugin.Log.LogDebug($"피해로 보너스: {KnifeCombatDamageBonusValue}%");
                Plugin.Log.LogDebug($"암살자 치명타 피해: +{KnifeExecutionCritDamageValue}%, 치명타 확률: +{KnifeExecutionCritChanceValue}%");
                Plugin.Log.LogDebug($"암살술 스태거 확률: {KnifeAssassinationStaggerChanceValue}%, 필요 연속 공격: {KnifeAssassinationRequiredHitsValue}회");

                Plugin.Log.LogDebug("=== 암살자의 심장 (G키 액티브) ===");
                Plugin.Log.LogDebug($"치명타 데미지: {KnifeAssassinHeartCritDamageValue}배");
                Plugin.Log.LogDebug($"지속시간: {KnifeAssassinHeartDurationValue}초");
                Plugin.Log.LogDebug($"스태미나 소모: {KnifeAssassinHeartStaminaCostValue}");
                Plugin.Log.LogDebug($"쿨타임: {KnifeAssassinHeartCooldownValue}초");
                
                Plugin.Log.LogDebug("=== [단검 컨피그] 설정값 출력 완료 ===");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 컨피그] 설정값 출력 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 단검 설정값 업데이트 알림
        /// </summary>
        public static void OnKnifeConfigChanged()
        {
            try
            {
                Plugin.Log.LogInfo("[단검 컨피그] 설정값 변경됨 - 새로운 값으로 업데이트");
                LogKnifeConfigValues();
                
                // 툴팁 업데이트
                Knife_Tooltip.UpdateKnifeTooltips();
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 컨피그] 설정 변경 알림 실패: {ex.Message}");
            }
        }

        #endregion
    }
}
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
        public static ConfigEntry<float> KnifeMoveSpeedDuration;
        public static ConfigEntry<float> KnifeAttackDamageBonus;
        public static ConfigEntry<float> KnifeAttackDamageDuration;
        public static ConfigEntry<float> KnifeCritRateBonus;
        public static ConfigEntry<float> KnifeCritRateDuration;
        public static ConfigEntry<float> KnifeCombatDamageBonus;
        public static ConfigEntry<float> KnifeExecutionCritDamage;
        public static ConfigEntry<float> KnifeExecutionStaggerBonus;
        public static ConfigEntry<float> KnifeAssassinationCritMultiplier;

        // === 암살자의 심장 (G키 액티브 스킬) 설정 ===
        public static ConfigEntry<float> KnifeAssassinHeartDamageBonus;
        public static ConfigEntry<float> KnifeAssassinHeartCritChance;
        public static ConfigEntry<float> KnifeAssassinHeartCritDamage;
        public static ConfigEntry<float> KnifeAssassinHeartDuration;
        public static ConfigEntry<float> KnifeAssassinHeartStaminaCost;
        public static ConfigEntry<float> KnifeAssassinHeartCooldown;
        public static ConfigEntry<float> KnifeAssassinHeartTeleportRange;
        public static ConfigEntry<float> KnifeAssassinHeartTeleportBehind;

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
        public static float KnifeMoveSpeedDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step3_move_speed_duration", KnifeMoveSpeedDuration?.Value ?? 8f);
        public static float KnifeAttackDamageBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step4_attack_damage_bonus", KnifeAttackDamageBonus?.Value ?? 2f);
        public static float KnifeAttackDamageDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step4_attack_damage_duration", KnifeAttackDamageDuration?.Value ?? 10f);
        public static float KnifeCritRateBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step5_crit_rate_bonus", KnifeCritRateBonus?.Value ?? 12f);
        public static float KnifeCritRateDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step5_crit_rate_duration", KnifeCritRateDuration?.Value ?? 15f);
        public static float KnifeCombatDamageBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step6_combat_damage_bonus", KnifeCombatDamageBonus?.Value ?? 25f);
        public static float KnifeExecutionCritDamageValue => SkillTreeConfig.GetEffectiveValue("knife_step7_execution_crit_damage", KnifeExecutionCritDamage?.Value ?? 25f);
        public static float KnifeExecutionStaggerBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step7_execution_stagger_bonus", KnifeExecutionStaggerBonus?.Value ?? 10f);
        public static float KnifeAssassinationCritMultiplierValue => SkillTreeConfig.GetEffectiveValue("knife_step8_assassination_crit_multiplier", KnifeAssassinationCritMultiplier?.Value ?? 1.5f);

        // === 암살자의 심장 동적 값 ===
        public static float KnifeAssassinHeartDamageBonusValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_damage_bonus", KnifeAssassinHeartDamageBonus?.Value ?? 50f);
        public static float KnifeAssassinHeartCritChanceValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_crit_chance", KnifeAssassinHeartCritChance?.Value ?? 35f);
        public static float KnifeAssassinHeartCritDamageValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_crit_damage", KnifeAssassinHeartCritDamage?.Value ?? 2f);
        public static float KnifeAssassinHeartDurationValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_duration", KnifeAssassinHeartDuration?.Value ?? 15f);
        public static float KnifeAssassinHeartStaminaCostValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_stamina_cost", KnifeAssassinHeartStaminaCost?.Value ?? 30f);
        public static float KnifeAssassinHeartCooldownValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_cooldown", KnifeAssassinHeartCooldown?.Value ?? 60f);
        public static float KnifeAssassinHeartTeleportRangeValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_teleport_range", KnifeAssassinHeartTeleportRange?.Value ?? 7f);
        public static float KnifeAssassinHeartTeleportBehindValue => SkillTreeConfig.GetEffectiveValue("knife_step9_assassin_heart_teleport_behind", KnifeAssassinHeartTeleportBehind?.Value ?? 1f);

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
                    "Knife Tree", "Tier0_단검전문가_백스탭데미지보너스", 35f,
                    "Tier 0: 단검 전문가(knife_expert_damage) - 백스탭 데미지 보너스 (%)");

                KnifeExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier0_단검전문가_필요포인트", 2,
                    "Tier 0: 단검 전문가(knife_expert_damage) - 필요 포인트");

                // === Tier 2: 회피 숙련 ===
                KnifeEvasionBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier2_회피숙련_회피확률", 20f,
                    "Tier 2: 회피 숙련(knife_step2_evasion) - 회피 확률 (%)");

                KnifeEvasionDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier2_회피숙련_무적시간", 3f,
                    "Tier 2: 회피 숙련(knife_step2_evasion) - 회피 후 무적 시간 (초)");

                KnifeEvasionRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier2_회피숙련_필요포인트", 2,
                    "Tier 2: 회피 숙련(knife_step2_evasion) - 필요 포인트");

                // === Tier 3: 빠른 움직임 ===
                KnifeMoveSpeedBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier3_빠른움직임_이동속도증가", 10f,
                    "Tier 3: 빠른 움직임(knife_step3_move_speed) - 이동속도 증가 (%)");

                KnifeMoveSpeedDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier3_빠른움직임_효과지속시간", 15f,
                    "Tier 3: 빠른 움직임(knife_step3_move_speed) - 효과 지속시간 (초)");

                KnifeMoveSpeedRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier3_빠른움직임_필요포인트", 2,
                    "Tier 3: 빠른 움직임(knife_step3_move_speed) - 필요 포인트");

                // === Tier 4: 전투 숙련 ===
                KnifeAttackDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier4_전투숙련_단검데미지증가", 2f,
                    "Tier 4: 전투 숙련(knife_step4_attack_damage) - 적 처치 시 단검 데미지 증가 (고정값)");

                KnifeAttackDamageDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier4_전투숙련_효과지속시간", 10f,
                    "Tier 4: 전투 숙련(knife_step4_attack_damage) - 효과 지속시간 (초)");

                KnifeAttackSpeedRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier4_전투숙련_필요포인트", 2,
                    "Tier 4: 전투 숙련(knife_step4_attack_damage) - 필요 포인트");

                // === Tier 5: 치명타 숙련 ===
                KnifeCritRateBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier5_치명타숙련_치명타확률증가", 12f,
                    "Tier 5: 치명타 숙련(knife_step5_crit_rate) - 치명타 확률 증가 (%)");

                KnifeCritRateDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier5_치명타숙련_효과지속시간", 15f,
                    "Tier 5: 치명타 숙련(knife_step5_crit_rate) - 효과 지속시간 (초)");

                KnifeCritRateRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier5_치명타숙련_필요포인트", 3,
                    "Tier 5: 치명타 숙련(knife_step5_crit_rate) - 필요 포인트");

                // === Tier 6: 치명적 피해 ===
                KnifeCombatDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier6_치명적피해_공격력증가", 25f,
                    "Tier 6: 치명적 피해(knife_step6_combat_damage) - 공격력 증가 (%)");

                KnifeCombatDamageRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier6_치명적피해_필요포인트", 2,
                    "Tier 6: 치명적 피해(knife_step6_combat_damage) - 필요 포인트");

                // === Tier 7: 암살자 ===
                KnifeExecutionCritDamage = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier7_암살자_치명타피해증가", 25f,
                    "Tier 7: 암살자(knife_step7_execution) - 치명타 피해 증가 (%)");

                KnifeExecutionStaggerBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier7_암살자_비틀거림공격력", 10f,
                    "Tier 7: 암살자(knife_step7_execution) - 비틀거림 공격력 증가 (%)");

                KnifeExecutionRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier7_암살자_필요포인트", 2,
                    "Tier 7: 암살자(knife_step7_execution) - 필요 포인트");

                // === Tier 8: 암살술 ===
                KnifeAssassinationCritMultiplier = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier8_암살술_백스탭공격력증가", 35f,
                    "Tier 8: 암살술(knife_step8_assassination) - 백스탭 공격력 증가 (%)");

                KnifeAssassinationRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier8_암살술_필요포인트", 2,
                    "Tier 8: 암살술(knife_step8_assassination) - 필요 포인트");

                // === Tier 9: 암살자의 심장 (G키 액티브) ===
                KnifeAssassinHeartDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_데미지보너스", 50f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 데미지 보너스 (%)");

                KnifeAssassinHeartCritChance = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_치명타확률증가", 60f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 치명타 확률 증가 (%)");

                KnifeAssassinHeartCritDamage = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_치명타데미지배수", 1.3f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 치명타 데미지 배수");

                KnifeAssassinHeartDuration = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_효과지속시간", 7f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 효과 지속시간 (초)");

                KnifeAssassinHeartStaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_스태미나소모", 20f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 스태미나 소모량");

                KnifeAssassinHeartCooldown = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_쿨타임", 40f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 쿨타임 (초)");

                KnifeAssassinHeartTeleportRange = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_순간이동탐색범위", 7f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 순간이동 대상 탐색 범위 (m)");

                KnifeAssassinHeartTeleportBehind = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_순간이동뒤쪽거리", 1f,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 대상 뒤쪽으로 이동할 거리 (m)");

                KnifeAssassinHeartRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Knife Tree", "Tier9_암살자의심장_필요포인트", 3,
                    "Tier 9: 암살자의 심장(knife_step9_assassin_heart) - 필요 포인트");

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
                KnifeMoveSpeedDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackDamageBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAttackDamageDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeCritRateBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeCritRateDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeCombatDamageBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeExecutionCritDamage.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeExecutionStaggerBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinationCritMultiplier.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                
                // 암살자의 심장 이벤트 핸들러
                KnifeAssassinHeartDamageBonus.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartCritChance.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartCritDamage.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartDuration.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartStaminaCost.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartCooldown.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartTeleportRange.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();
                KnifeAssassinHeartTeleportBehind.SettingChanged += (sender, args) => Knife_Tooltip.UpdateKnifeTooltips();

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
                Plugin.Log.LogDebug($"빠른 움직임: {KnifeMoveSpeedBonusValue}%, 지속시간: {KnifeMoveSpeedDurationValue}초");
                Plugin.Log.LogDebug($"빠른 공격 단검 데미지: +{KnifeAttackDamageBonusValue}, 지속시간: {KnifeAttackDamageDurationValue}초");
                Plugin.Log.LogDebug($"치명타 숙련: {KnifeCritRateBonusValue}%, 지속시간: {KnifeCritRateDurationValue}초");
                Plugin.Log.LogDebug($"피해로 보너스: {KnifeCombatDamageBonusValue}%");
                Plugin.Log.LogDebug($"암살자 치명타 피해: +{KnifeExecutionCritDamageValue}%, 비틀거림 공격력: +{KnifeExecutionStaggerBonusValue}%");
                Plugin.Log.LogDebug($"암살술 치명타 배수: {KnifeAssassinationCritMultiplierValue}");

                Plugin.Log.LogDebug("=== 암살자의 심장 (G키 액티브) ===");
                Plugin.Log.LogDebug($"데미지 보너스: {KnifeAssassinHeartDamageBonusValue}%");
                Plugin.Log.LogDebug($"치명타 확률: {KnifeAssassinHeartCritChanceValue}%");
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
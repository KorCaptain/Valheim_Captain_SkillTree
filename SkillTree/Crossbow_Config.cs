using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 석궁 전문가 전용 컨피그 시스템
    /// 모든 석궁 스킬의 설정값들을 관리
    /// MMO 시스템과 연동하여 동적 값 변경 지원
    /// </summary>
    public static class Crossbow_Config
    {
        #region 필요 포인트 설정

        public static ConfigEntry<int> CrossbowExpertRequiredPoints;
        public static ConfigEntry<int> CrossbowStep2RequiredPoints;
        public static ConfigEntry<int> CrossbowStep3RequiredPoints;
        public static ConfigEntry<int> CrossbowStep4RequiredPoints;
        public static ConfigEntry<int> CrossbowStep5RequiredPoints;
        public static ConfigEntry<int> CrossbowOneShotRequiredPoints;

        #endregion

        #region 석궁 전문가 스킬 기본 설정

        // Tier 0: 석궁 전문가
        public static ConfigEntry<float> CrossbowExpertDamageBonus;

        // Tier 1: 연속 발사
        public static ConfigEntry<float> CrossbowRapidFireChance;
        public static ConfigEntry<int> CrossbowRapidFireShotCount;
        public static ConfigEntry<float> CrossbowRapidFireDamagePercent;
        public static ConfigEntry<float> CrossbowRapidFireDelay;
        public static ConfigEntry<int> CrossbowRapidFireBoltConsumption;

        // Tier 2: 균형 조준
        public static ConfigEntry<float> CrossbowBalanceKnockbackChance;
        public static ConfigEntry<float> CrossbowBalanceKnockbackDistance;

        // Tier 2: 고속 장전
        public static ConfigEntry<float> CrossbowRapidReloadSpeed;

        // Tier 2: 정직한 한 발
        public static ConfigEntry<float> CrossbowMarkDamageBonus;

        // Tier 3: 자동 장전
        public static ConfigEntry<float> CrossbowAutoReloadChance;

        // Tier 4: 연속 발사 Lv2
        public static ConfigEntry<float> CrossbowRapidFireLv2Chance;
        public static ConfigEntry<int> CrossbowRapidFireLv2ShotCount;
        public static ConfigEntry<float> CrossbowRapidFireLv2DamagePercent;
        public static ConfigEntry<float> CrossbowRapidFireLv2Delay;
        public static ConfigEntry<int> CrossbowRapidFireLv2BoltConsumption;

        // Tier 4: 결전의 일격
        public static ConfigEntry<float> CrossbowFinalStrikeHpThreshold;
        public static ConfigEntry<float> CrossbowFinalStrikeDamageBonus;

        // Tier 5: 단 한 발 (액티브)
        public static ConfigEntry<float> CrossbowOneShotDuration;
        public static ConfigEntry<float> CrossbowOneShotDamageBonus;
        public static ConfigEntry<float> CrossbowOneShotKnockback;
        public static ConfigEntry<float> CrossbowOneShotCooldown;

        #endregion

        #region Dynamic Value Properties (MMO 연동)

        // === 필요 포인트 동적 값 ===
        public static int CrossbowExpertRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_expert_required_points",
            CrossbowExpertRequiredPoints?.Value ?? 2);

        public static int CrossbowStep2RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_step2_required_points",
            CrossbowStep2RequiredPoints?.Value ?? 2);

        public static int CrossbowStep3RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_step3_required_points",
            CrossbowStep3RequiredPoints?.Value ?? 2);

        public static int CrossbowStep4RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_step4_required_points",
            CrossbowStep4RequiredPoints?.Value ?? 2);

        public static int CrossbowStep5RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_step5_required_points",
            CrossbowStep5RequiredPoints?.Value ?? 3);

        public static int CrossbowOneShotRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_oneshot_required_points",
            CrossbowOneShotRequiredPoints?.Value ?? 4);

        // === 석궁 전문가 동적 값 ===
        public static float CrossbowExpertDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_expert_damage_bonus",
            CrossbowExpertDamageBonus?.Value ?? 5f);

        public static float CrossbowRapidFireChanceValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_rapid_fire_chance",
            CrossbowRapidFireChance?.Value ?? 15f);

        public static int CrossbowRapidFireShotCountValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_rapid_fire_shots",
            CrossbowRapidFireShotCount?.Value ?? 3);

        public static float CrossbowRapidFireDamagePercentValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_rapid_fire_damage",
            CrossbowRapidFireDamagePercent?.Value ?? 75f);

        public static float CrossbowRapidFireDelayValue =>
            CrossbowRapidFireDelay?.Value ?? 0.33f;

        public static int CrossbowRapidFireBoltConsumptionValue =>
            CrossbowRapidFireBoltConsumption?.Value ?? 1;

        public static float CrossbowBalanceKnockbackChanceValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_balance_knockback_chance",
            CrossbowBalanceKnockbackChance?.Value ?? 30f);

        public static float CrossbowBalanceKnockbackDistanceValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_balance_knockback_distance",
            CrossbowBalanceKnockbackDistance?.Value ?? 3f);

        public static float CrossbowRapidReloadSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_rapid_reload_speed",
            CrossbowRapidReloadSpeed?.Value ?? 10f);

        public static float CrossbowMarkDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_mark_damage_bonus",
            CrossbowMarkDamageBonus?.Value ?? 35f);

        public static float CrossbowAutoReloadChanceValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_auto_reload_chance",
            CrossbowAutoReloadChance?.Value ?? 30f);

        public static float CrossbowRapidFireLv2ChanceValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_rapid_fire_lv2_chance",
            CrossbowRapidFireLv2Chance?.Value ?? 30f);

        public static int CrossbowRapidFireLv2ShotCountValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crossbow_rapid_fire_lv2_shots",
            CrossbowRapidFireLv2ShotCount?.Value ?? 3);

        public static float CrossbowRapidFireLv2DamagePercentValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_rapid_fire_lv2_damage",
            CrossbowRapidFireLv2DamagePercent?.Value ?? 75f);

        public static float CrossbowRapidFireLv2DelayValue =>
            CrossbowRapidFireLv2Delay?.Value ?? 0.33f;

        public static int CrossbowRapidFireLv2BoltConsumptionValue =>
            CrossbowRapidFireLv2BoltConsumption?.Value ?? 1;

        public static float CrossbowFinalStrikeHpThresholdValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_final_strike_hp_threshold",
            CrossbowFinalStrikeHpThreshold?.Value ?? 50f);

        public static float CrossbowFinalStrikeDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_final_strike_damage_bonus",
            CrossbowFinalStrikeDamageBonus?.Value ?? 30f);

        public static float CrossbowOneShotDurationValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_one_shot_duration",
            CrossbowOneShotDuration?.Value ?? 30f);

        public static float CrossbowOneShotDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_one_shot_damage_bonus",
            CrossbowOneShotDamageBonus?.Value ?? 120f);

        public static float CrossbowOneShotKnockbackValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_one_shot_knockback",
            CrossbowOneShotKnockback?.Value ?? 5f);

        public static float CrossbowOneShotCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("crossbow_one_shot_cooldown",
            CrossbowOneShotCooldown?.Value ?? 60f);

        #endregion

        #region Initialization

        /// <summary>
        /// 석궁 컨피그 초기화
        /// </summary>
        public static void InitializeCrossbowConfig(ConfigFile config)
        {
            try
            {
                Plugin.Log.LogDebug("[Crossbow_Config] Config initialization started");

                // === 필요 포인트 설정 ===
                CrossbowExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier0_CrossbowExpert_RequiredPoints", 2,
                    "Tier 0: Crossbow Expert (crossbow_expert_damage) - Required Points");

                CrossbowStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier1_RapidFire_RequiredPoints", 2,
                    "Tier 1: Rapid Fire (crossbow_Step1_rapid_fire) - Required Points");

                CrossbowStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_CrossbowSkills_RequiredPoints", 2,
                    "Tier 2: Crossbow Skills - Required Points");

                CrossbowStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier3_AutoReload_RequiredPoints", 2,
                    "Tier 3: Auto Reload (crossbow_Step3_re) - Required Points");

                CrossbowStep5RequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_CrossbowSkills_RequiredPoints", 3,
                    "Tier 4: Crossbow Skills - Required Points");

                CrossbowOneShotRequiredPoints = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_OneShot_RequiredPoints", 4,
                    "Tier 5: One Shot (R-Key Active) - Required Points");

                // === Tier 0: 석궁 전문가 ===
                CrossbowExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier0_CrossbowExpert_DamageBonus", 5f,
                    "Tier 0: Crossbow Expert (crossbow_expert_damage) - Crossbow Damage Bonus (%)");

                // === Tier 1: 연속 발사 ===
                CrossbowRapidFireChance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier1_RapidFire_Chance", 15f,
                    "Tier 1: Rapid Fire (crossbow_Step1_rapid_fire) - Trigger Chance (%)");

                CrossbowRapidFireShotCount = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier1_RapidFire_ShotCount", 3,
                    "Tier 1: Rapid Fire (crossbow_Step1_rapid_fire) - Total Shot Count");

                CrossbowRapidFireDamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier1_RapidFire_DamagePercent", 75f,
                    "Tier 1: Rapid Fire (crossbow_Step1_rapid_fire) - Each Bolt Damage Percent (%)");

                CrossbowRapidFireDelay = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier1_RapidFire_Delay", 0.33f,
                    "Tier 1: Rapid Fire (crossbow_Step1_rapid_fire) - Bolt Fire Interval (sec)");

                CrossbowRapidFireBoltConsumption = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier1_RapidFire_BoltConsumption", 1,
                    "Tier 1: Rapid Fire (crossbow_Step1_rapid_fire) - Bolt Consumption");

                // === Tier 2: 균형 조준 ===
                CrossbowBalanceKnockbackChance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_BalancedAim_KnockbackChance", 30f,
                    "Tier 2: Balanced Aim (crossbow_Step2_balance) - Knockback Chance (%)");

                CrossbowBalanceKnockbackDistance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_BalancedAim_KnockbackDistance", 3f,
                    "Tier 2: Balanced Aim (crossbow_Step2_balance) - Knockback Distance (m)");

                // === Tier 2: 고속 장전 ===
                CrossbowRapidReloadSpeed = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_RapidReload_SpeedIncrease", 10f,
                    "Tier 2: Rapid Reload (crossbow_Step2_rapid) - Reload Speed Increase (%)");

                // === Tier 2: 정직한 한 발 ===
                CrossbowMarkDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_HonestShot_DamageBonus", 35f,
                    "Tier 2: Honest Shot (crossbow_Step2_mark) - Damage Bonus (%)");

                // === Tier 3: 자동 장전 ===
                CrossbowAutoReloadChance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier3_AutoReload_Chance", 30f,
                    "Tier 3: Auto Reload (crossbow_Step3_re) - Auto Reload Chance (%)");

                // === Tier 4: 연속 발사 Lv2 ===
                CrossbowRapidFireLv2Chance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_RapidFireLv2_Chance", 30f,
                    "Tier 4: Rapid Fire Lv2 (crossbow_Step4_rapid_fire_lv2) - Trigger Chance (%)");

                CrossbowRapidFireLv2ShotCount = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_RapidFireLv2_ShotCount", 3,
                    "Tier 4: Rapid Fire Lv2 (crossbow_Step4_rapid_fire_lv2) - Total Shot Count");

                CrossbowRapidFireLv2DamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_RapidFireLv2_DamagePercent", 75f,
                    "Tier 4: Rapid Fire Lv2 (crossbow_Step4_rapid_fire_lv2) - Each Bolt Damage Percent (%)");

                CrossbowRapidFireLv2Delay = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_RapidFireLv2_Delay", 0.33f,
                    "Tier 4: Rapid Fire Lv2 (crossbow_Step4_rapid_fire_lv2) - Bolt Fire Interval (sec)");

                CrossbowRapidFireLv2BoltConsumption = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_RapidFireLv2_BoltConsumption", 1,
                    "Tier 4: Rapid Fire Lv2 (crossbow_Step4_rapid_fire_lv2) - Bolt Consumption");

                // === Tier 4: 결전의 일격 ===
                CrossbowFinalStrikeHpThreshold = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_FinalStrike_HpThreshold", 50f,
                    "Tier 4: Final Strike (crossbow_Step4_final) - HP Threshold (%)");

                CrossbowFinalStrikeDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_FinalStrike_DamageBonus", 30f,
                    "Tier 4: Final Strike (crossbow_Step4_final) - Additional Damage (%)");

                // === Tier 5: 단 한 발 (액티브 스킬) ===
                CrossbowOneShotDuration = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_OneShot_Duration", 30f,
                    "Tier 5: One Shot (crossbow_Step5_expert) - Buff Duration (sec)");

                CrossbowOneShotDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_OneShot_DamageBonus", 120f,
                    "Tier 5: One Shot (crossbow_Step5_expert) - Damage Bonus (%)");

                CrossbowOneShotKnockback = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_OneShot_KnockbackDistance", 5f,
                    "Tier 5: One Shot (crossbow_Step5_expert) - Knockback Distance (m)");

                CrossbowOneShotCooldown = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_OneShot_Cooldown", 60f,
                    "Tier 5: One Shot (crossbow_Step5_expert) - Cooldown (sec)");

                // === 이벤트 핸들러 등록 (툴팁 실시간 업데이트) ===
                RegisterCrossbowEventHandlers();

                Plugin.Log.LogDebug("[Crossbow_Config] All settings loaded");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Crossbow_Config] Initialization failed: {ex.Message}");
            }
        }

        #endregion

        #region Tooltip Real-Time Update System

        /// <summary>
        /// Config 변경 시 툴팁 실시간 업데이트를 위한 이벤트 핸들러 등록
        /// </summary>
        private static void RegisterCrossbowEventHandlers()
        {
            CrossbowExpertDamageBonus.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireChance.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireShotCount.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireDamagePercent.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireDelay.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireBoltConsumption.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowBalanceKnockbackChance.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowBalanceKnockbackDistance.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidReloadSpeed.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowMarkDamageBonus.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowAutoReloadChance.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireLv2Chance.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireLv2ShotCount.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireLv2DamagePercent.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireLv2Delay.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowRapidFireLv2BoltConsumption.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowFinalStrikeHpThreshold.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowFinalStrikeDamageBonus.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowOneShotDuration.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowOneShotDamageBonus.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowOneShotKnockback.SettingChanged += (sender, args) => UpdateCrossbowTooltips();
            CrossbowOneShotCooldown.SettingChanged += (sender, args) => UpdateCrossbowTooltips();

            Plugin.Log.LogDebug("[석궁 컨피그] 툴팁 실시간 업데이트 이벤트 핸들러 등록 완료");
        }

        /// <summary>
        /// 모든 석궁 스킬 툴팁 업데이트
        /// Config 파일 수정 시 자동으로 게임 내 툴팁이 변경됨
        /// </summary>
        public static void UpdateCrossbowTooltips()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes == null)
                {
                    Plugin.Log.LogWarning("[석궁 툴팁] SkillTreeManager가 초기화되지 않음");
                    return;
                }

                // Tier 1: Crossbow Expert
                UpdateSkillTooltip("crossbow_Step1_damage",
                    $"Crossbow damage +{CrossbowExpertDamageBonusValue}%\n<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 2: Rapid Fire
                UpdateSkillTooltip("crossbow_Step2_rapid_fire",
                    $"{CrossbowRapidFireChanceValue}% chance to fire {CrossbowRapidFireShotCountValue} rapid shots\n" +
                    $"(each {CrossbowRapidFireDamagePercentValue}% damage, {CrossbowRapidFireBoltConsumptionValue} bolt consumed)\n" +
                    $"<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 3-1: Balanced Aim
                UpdateSkillTooltip("crossbow_Step2_balance",
                    $"On hit: {CrossbowBalanceKnockbackChanceValue}% chance to knockback ({CrossbowBalanceKnockbackDistanceValue}m)\n<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 3-2: Rapid Reload
                UpdateSkillTooltip("crossbow_Step3_rapid",
                    $"Reload speed +{CrossbowRapidReloadSpeedValue}%\n<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 3-3: Honest Shot
                UpdateSkillTooltip("crossbow_Step3_mark",
                    $"Crit chance fixed at 0%, but Crossbow damage +{CrossbowMarkDamageBonusValue}%\n<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 4: Auto Reload
                UpdateSkillTooltip("crossbow_Step4_re",
                    $"On hit: {CrossbowAutoReloadChanceValue}% chance for next reload speed 200%\n<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 5-1: Rapid Fire Lv2
                UpdateSkillTooltip("crossbow_Step4_rapid_fire_lv2",
                    $"{CrossbowRapidFireLv2ChanceValue}% chance to fire {CrossbowRapidFireLv2ShotCountValue} rapid shots\n" +
                    $"(each {CrossbowRapidFireLv2DamagePercentValue}% damage, {CrossbowRapidFireLv2BoltConsumptionValue} bolt consumed)\n" +
                    $"<color=#FFD700><size=16>※ Stacks with Lv1 chance</size></color>\n" +
                    $"<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 5-2: Final Strike
                UpdateSkillTooltip("crossbow_Step5_final",
                    $"Deals extra {CrossbowFinalStrikeDamageBonusValue}% damage to enemies with {CrossbowFinalStrikeHpThresholdValue}%+ HP\n<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                // Tier 6: One Shot (Active Skill)
                UpdateSkillTooltip("crossbow_Step6_expert",
                    $"R Key: Crossbow shot within {CrossbowOneShotDurationValue}s grants +{CrossbowOneShotDamageBonusValue}% damage, knockback {CrossbowOneShotKnockbackValue}m (Cooldown: {CrossbowOneShotCooldownValue}s)\n<color=#DDA0DD><size=16>※ Active when Crossbow equipped</size></color>");

                Plugin.Log.LogInfo("[석궁 툴팁] 모든 석궁 스킬 툴팁 업데이트 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[석궁 툴팁] 툴팁 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 개별 스킬 툴팁 업데이트 헬퍼 함수
        /// </summary>
        private static void UpdateSkillTooltip(string skillId, string newDescription)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey(skillId))
                {
                    var skillNode = manager.SkillNodes[skillId];
                    skillNode.Description = newDescription;
                    Plugin.Log.LogDebug($"[석궁 툴팁] {skillId} 업데이트 완료");
                }
                else
                {
                    Plugin.Log.LogWarning($"[석궁 툴팁] {skillId} 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[석궁 툴팁] {skillId} 업데이트 실패: {ex.Message}");
            }
        }

        #endregion
    }
}

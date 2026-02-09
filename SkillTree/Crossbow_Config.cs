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
        #region 석궁 전문가 스킬 기본 설정

        // Tier 1: 석궁 전문가
        public static ConfigEntry<float> CrossbowExpertDamageBonus;

        // Tier 2: 연속 발사
        public static ConfigEntry<float> CrossbowRapidFireChance;
        public static ConfigEntry<int> CrossbowRapidFireShotCount;
        public static ConfigEntry<float> CrossbowRapidFireDamagePercent;
        public static ConfigEntry<float> CrossbowRapidFireDelay;
        public static ConfigEntry<int> CrossbowRapidFireBoltConsumption;

        // Tier 3: 균형 조준
        public static ConfigEntry<float> CrossbowBalanceKnockbackChance;
        public static ConfigEntry<float> CrossbowBalanceKnockbackDistance;

        // Tier 3: 고속 장전
        public static ConfigEntry<float> CrossbowRapidReloadSpeed;

        // Tier 3: 정직한 한 발
        public static ConfigEntry<float> CrossbowMarkDamageBonus;

        // Tier 4: 자동 장전
        public static ConfigEntry<float> CrossbowAutoReloadChance;

        // Tier 5: 연속 발사 Lv2
        public static ConfigEntry<float> CrossbowRapidFireLv2Chance;
        public static ConfigEntry<int> CrossbowRapidFireLv2ShotCount;
        public static ConfigEntry<float> CrossbowRapidFireLv2DamagePercent;
        public static ConfigEntry<float> CrossbowRapidFireLv2Delay;
        public static ConfigEntry<int> CrossbowRapidFireLv2BoltConsumption;

        // Tier 5: 결전의 일격
        public static ConfigEntry<float> CrossbowFinalStrikeHpThreshold;
        public static ConfigEntry<float> CrossbowFinalStrikeDamageBonus;

        // Tier 6: 단 한 발 (액티브)
        public static ConfigEntry<float> CrossbowOneShotDuration;
        public static ConfigEntry<float> CrossbowOneShotDamageBonus;
        public static ConfigEntry<float> CrossbowOneShotKnockback;
        public static ConfigEntry<float> CrossbowOneShotCooldown;

        #endregion

        #region Dynamic Value Properties (MMO 연동)

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
                Plugin.Log.LogDebug("=== [석궁 컨피그] 초기화 시작 ===");

                // === Tier 1: 석궁 전문가 ===
                CrossbowExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier1_석궁전문가_데미지보너스", 5f,
                    "Tier 1: 석궁 전문가(crossbow_Step1_damage) - 석궁 공격력 보너스 (%)");

                // === Tier 2: 연속 발사 ===
                CrossbowRapidFireChance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_연속발사_확률", 15f,
                    "Tier 2: 연속 발사(crossbow_Step2_rapid_fire) - 연속 발사 발동 확률 (%)");

                CrossbowRapidFireShotCount = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_연속발사_발사횟수", 3,
                    "Tier 2: 연속 발사(crossbow_Step2_rapid_fire) - 총 발사 횟수");

                CrossbowRapidFireDamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_연속발사_데미지비율", 75f,
                    "Tier 2: 연속 발사(crossbow_Step2_rapid_fire) - 각 볼트 데미지 비율 (%)");

                CrossbowRapidFireDelay = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_연속발사_발사간격", 0.33f,
                    "Tier 2: 연속 발사(crossbow_Step2_rapid_fire) - 볼트 발사 간격 (초)");

                CrossbowRapidFireBoltConsumption = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier2_연속발사_볼트소모", 1,
                    "Tier 2: 연속 발사(crossbow_Step2_rapid_fire) - 소모할 볼트 수량");

                // === Tier 3: 균형 조준 ===
                CrossbowBalanceKnockbackChance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier3_균형조준_넉백확률", 30f,
                    "Tier 3: 균형 조준(crossbow_Step2_balance) - 넉백 확률 (%)");

                CrossbowBalanceKnockbackDistance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier3_균형조준_넉백거리", 3f,
                    "Tier 3: 균형 조준(crossbow_Step2_balance) - 넉백 거리 (m)");

                // === Tier 3: 고속 장전 ===
                CrossbowRapidReloadSpeed = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier3_고속장전_속도증가", 10f,
                    "Tier 3: 고속 장전(crossbow_Step3_rapid) - 장전 속도 증가 (%)");

                // === Tier 3: 정직한 한 발 ===
                CrossbowMarkDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier3_정직한한발_데미지보너스", 35f,
                    "Tier 3: 정직한 한 발(crossbow_Step3_mark) - 공격력 보너스 (%)");

                // === Tier 4: 자동 장전 ===
                CrossbowAutoReloadChance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier4_자동장전_확률", 30f,
                    "Tier 4: 자동 장전(crossbow_Step4_re) - 자동 장전 확률 (%)");

                // === Tier 5: 연속 발사 Lv2 ===
                CrossbowRapidFireLv2Chance = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_연속발사Lv2_확률", 30f,
                    "Tier 5: 연속 발사 Lv2(crossbow_Step4_rapid_fire_lv2) - 연속 발사 발동 확률 (%)");

                CrossbowRapidFireLv2ShotCount = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_연속발사Lv2_발사횟수", 3,
                    "Tier 5: 연속 발사 Lv2(crossbow_Step4_rapid_fire_lv2) - 총 발사 횟수");

                CrossbowRapidFireLv2DamagePercent = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_연속발사Lv2_데미지비율", 75f,
                    "Tier 5: 연속 발사 Lv2(crossbow_Step4_rapid_fire_lv2) - 각 볼트 데미지 비율 (%)");

                CrossbowRapidFireLv2Delay = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_연속발사Lv2_발사간격", 0.33f,
                    "Tier 5: 연속 발사 Lv2(crossbow_Step4_rapid_fire_lv2) - 볼트 발사 간격 (초)");

                CrossbowRapidFireLv2BoltConsumption = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_연속발사Lv2_볼트소모", 1,
                    "Tier 5: 연속 발사 Lv2(crossbow_Step4_rapid_fire_lv2) - 소모할 볼트 수량");

                // === Tier 5: 결전의 일격 ===
                CrossbowFinalStrikeHpThreshold = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_결전의일격_체력임계값", 50f,
                    "Tier 5: 결전의 일격(crossbow_Step5_final) - 체력 임계값 (%)");

                CrossbowFinalStrikeDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier5_결전의일격_데미지보너스", 30f,
                    "Tier 5: 결전의 일격(crossbow_Step5_final) - 추가 피해 (%)");

                // === Tier 6: 단 한 발 (액티브 스킬) ===
                CrossbowOneShotDuration = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier6_단한발_지속시간", 30f,
                    "Tier 6: 단 한 발(crossbow_Step6_expert) - 버프 지속 시간 (초)");

                CrossbowOneShotDamageBonus = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier6_단한발_공격력보너스", 120f,
                    "Tier 6: 단 한 발(crossbow_Step6_expert) - 공격력 보너스 (%)");

                CrossbowOneShotKnockback = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier6_단한발_넉백거리", 5f,
                    "Tier 6: 단 한 발(crossbow_Step6_expert) - 넉백 거리 (m)");

                CrossbowOneShotCooldown = SkillTreeConfig.BindServerSync(config,
                    "Crossbow Tree", "Tier6_단한발_쿨타임", 60f,
                    "Tier 6: 단 한 발(crossbow_Step6_expert) - 쿨타임 (초)");

                // === 이벤트 핸들러 등록 (툴팁 실시간 업데이트) ===
                RegisterCrossbowEventHandlers();

                Plugin.Log.LogDebug("[석궁 컨피그] 모든 설정 로드 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[석궁 컨피그] 초기화 실패: {ex.Message}");
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

                // Tier 1: 석궁 전문가
                UpdateSkillTooltip("crossbow_Step1_damage",
                    $"석궁 데미지 +{CrossbowExpertDamageBonusValue}%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 2: 연속 발사
                UpdateSkillTooltip("crossbow_Step2_rapid_fire",
                    $"{CrossbowRapidFireChanceValue}% 확률로 {CrossbowRapidFireShotCountValue}발 연속 발사\n" +
                    $"(각 {CrossbowRapidFireDamagePercentValue}% 데미지, 볼트 {CrossbowRapidFireBoltConsumptionValue}발 소모)\n" +
                    $"<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 3-1: 균형 조준
                UpdateSkillTooltip("crossbow_Step2_balance",
                    $"명중 시 {CrossbowBalanceKnockbackChanceValue}% 확률로 넉백 ({CrossbowBalanceKnockbackDistanceValue}m)\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 3-2: 고속 장전
                UpdateSkillTooltip("crossbow_Step3_rapid",
                    $"장전속도 +{CrossbowRapidReloadSpeedValue}%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 3-3: 정직한 한 발
                UpdateSkillTooltip("crossbow_Step3_mark",
                    $"치명타 확률 0% 고정, 대신 석궁 데미지 +{CrossbowMarkDamageBonusValue}%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 4: 자동 장전
                UpdateSkillTooltip("crossbow_Step4_re",
                    $"명중 시 {CrossbowAutoReloadChanceValue}% 확률로 다음 1회 장전 속도 200%\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 5-1: 연속 발사 Lv2
                UpdateSkillTooltip("crossbow_Step4_rapid_fire_lv2",
                    $"{CrossbowRapidFireLv2ChanceValue}% 확률로 {CrossbowRapidFireLv2ShotCountValue}발 연속 발사\n" +
                    $"(각 {CrossbowRapidFireLv2DamagePercentValue}% 데미지, 볼트 {CrossbowRapidFireLv2BoltConsumptionValue}발 소모)\n" +
                    $"<color=#FFD700><size=16>※ Lv1과 확률 합산</size></color>\n" +
                    $"<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 5-2: 결전의 일격
                UpdateSkillTooltip("crossbow_Step5_final",
                    $"체력 {CrossbowFinalStrikeHpThresholdValue}% 이상 적에게 추가 {CrossbowFinalStrikeDamageBonusValue}% 피해\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

                // Tier 6: 단 한 발 (액티브 스킬)
                UpdateSkillTooltip("crossbow_Step6_expert",
                    $"R키: {CrossbowOneShotDurationValue}초 이내 석궁 발사 시 공격력 +{CrossbowOneShotDamageBonusValue}%, 넉백 {CrossbowOneShotKnockbackValue}m (쿨타임 {CrossbowOneShotCooldownValue}초)\n<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>");

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

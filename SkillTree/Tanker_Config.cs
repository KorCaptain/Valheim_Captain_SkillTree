using BepInEx.Configuration;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Tanker 직업 전용 컨피그 시스템
    /// 전장의 함성 - 적 도발 및 피해 감소 버프
    /// 아처 시스템과 동일한 구조로 MMO 연동 지원
    /// </summary>
    public static class Tanker_Config
    {
        // === Tanker 전장의 함성 컨피그 엔트리들 ===
        public static ConfigEntry<float> TankerTauntCooldown;       // 쿨타임 (초)
        public static ConfigEntry<float> TankerTauntStaminaCost;    // 스태미나 소모 (고정값)
        public static ConfigEntry<float> TankerTauntRange;          // 도발 범위 (m)
        public static ConfigEntry<float> TankerTauntDuration;       // 일반 몬스터 도발 지속시간 (초)
        public static ConfigEntry<float> TankerTauntBossDuration;   // 보스 도발 지속시간 (초)
        public static ConfigEntry<float> TankerTauntDamageReduction; // 자신이 받는 피해 감소 (%)
        public static ConfigEntry<float> TankerTauntBuffDuration;    // 피해 감소 버프 지속시간 (초)
        public static ConfigEntry<float> TankerTauntEffectHeight;    // 몬스터 머리위 효과 높이 (m)
        public static ConfigEntry<float> TankerTauntEffectScale;     // taunt 효과 크기 배율
        
        public static ConfigEntry<float> TankerTauntReflectPercent;     // 도발 반사 데미지 비율 (%)

        // === 패시브 효과 설정 ===
        public static ConfigEntry<float> TankerPassiveDamageReduction; // 탱커 패시브 피해 감소 (%)

        // === 레벨업 패시브 효과 설정 ===
        public static ConfigEntry<float> TankerHpBonusLv1;      // Lv1 체력 보너스 (%)
        public static ConfigEntry<float> TankerHpBonusLv2;      // Lv2 체력 보너스 (%)
        public static ConfigEntry<float> TankerHpBonusLv3;      // Lv3 체력 보너스 (%)
        public static ConfigEntry<float> TankerHpBonusLv4;      // Lv4 체력 보너스 (%)
        public static ConfigEntry<float> TankerHpBonusLv5;      // Lv5 체력 보너스 (%)
        public static ConfigEntry<float> TankerLv2BlockPower;   // Lv2 방패 막기 방어력
        public static ConfigEntry<float> TankerLv3BlockPower;   // Lv3 방패 막기 방어력
        public static ConfigEntry<float> TankerLv4BlockPower;   // Lv4 방패 막기 방어력
        public static ConfigEntry<float> TankerLv5BlockPower;   // Lv5 방패 막기 방어력

        // === 레벨별 반사 지속시간 설정 ===
        public static ConfigEntry<float> TankerReflectDurationLv1; // Lv1 반사 지속시간 (초)
        public static ConfigEntry<float> TankerReflectDurationLv2; // Lv2 반사 지속시간 (초)
        public static ConfigEntry<float> TankerReflectDurationLv3; // Lv3 반사 지속시간 (초)
        public static ConfigEntry<float> TankerReflectDurationLv4; // Lv4 반사 지속시간 (초)
        public static ConfigEntry<float> TankerReflectDurationLv5; // Lv5 반사 지속시간 (초)

        // === 동적 값 접근자 (MMO 시스템 연동) ===
        public static float TankerTauntCooldownValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_Cooldown", TankerTauntCooldown?.Value ?? 60f);
        public static float TankerTauntStaminaCostValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_StaminaCost", TankerTauntStaminaCost?.Value ?? 25f);
        public static float TankerTauntRangeValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_Range", TankerTauntRange?.Value ?? 12f);
        public static float TankerTauntDurationValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_Duration", TankerTauntDuration?.Value ?? 9f);
        public static float TankerTauntBossDurationValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_BossDuration", TankerTauntBossDuration?.Value ?? 1f);
        public static float TankerTauntDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_DamageReduction", TankerTauntDamageReduction?.Value ?? 20f);
        public static float TankerTauntBuffDurationValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_BuffDuration", TankerTauntBuffDuration?.Value ?? 8f);
        public static float TankerTauntEffectHeightValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_EffectHeight", TankerTauntEffectHeight?.Value ?? 2.0f);
        public static float TankerTauntEffectScaleValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_EffectScale", TankerTauntEffectScale?.Value ?? 0.8f);
        public static float TankerTauntReflectPercentValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_ReflectPercent", TankerTauntReflectPercent?.Value ?? 10f);
        public static float TankerPassiveDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Tanker_Passive_DamageReduction", TankerPassiveDamageReduction?.Value ?? 15f);
        public static float TankerHpBonusLv1Value => SkillTreeConfig.GetEffectiveValue("Tanker_Hp_Bonus_Lv1", TankerHpBonusLv1?.Value ?? 25f);
        public static float TankerHpBonusLv2Value => SkillTreeConfig.GetEffectiveValue("Tanker_Hp_Bonus_Lv2", TankerHpBonusLv2?.Value ?? 30f);
        public static float TankerHpBonusLv3Value => SkillTreeConfig.GetEffectiveValue("Tanker_Hp_Bonus_Lv3", TankerHpBonusLv3?.Value ?? 35f);
        public static float TankerHpBonusLv4Value => SkillTreeConfig.GetEffectiveValue("Tanker_Hp_Bonus_Lv4", TankerHpBonusLv4?.Value ?? 40f);
        public static float TankerHpBonusLv5Value => SkillTreeConfig.GetEffectiveValue("Tanker_Hp_Bonus_Lv5", TankerHpBonusLv5?.Value ?? 50f);
        public static float TankerLv2BlockPowerValue => SkillTreeConfig.GetEffectiveValue("Tanker_Lv2_BlockPower", TankerLv2BlockPower?.Value ?? 5f);
        public static float TankerLv3BlockPowerValue => SkillTreeConfig.GetEffectiveValue("Tanker_Lv3_BlockPower", TankerLv3BlockPower?.Value ?? 10f);
        public static float TankerLv4BlockPowerValue => SkillTreeConfig.GetEffectiveValue("Tanker_Lv4_BlockPower", TankerLv4BlockPower?.Value ?? 15f);
        public static float TankerLv5BlockPowerValue => SkillTreeConfig.GetEffectiveValue("Tanker_Lv5_BlockPower", TankerLv5BlockPower?.Value ?? 20f);
        public static float TankerReflectDurationLv1Value => SkillTreeConfig.GetEffectiveValue("Tanker_ReflectDuration_Lv1", TankerReflectDurationLv1?.Value ?? 10f);
        public static float TankerReflectDurationLv2Value => SkillTreeConfig.GetEffectiveValue("Tanker_ReflectDuration_Lv2", TankerReflectDurationLv2?.Value ?? 12f);
        public static float TankerReflectDurationLv3Value => SkillTreeConfig.GetEffectiveValue("Tanker_ReflectDuration_Lv3", TankerReflectDurationLv3?.Value ?? 14f);
        public static float TankerReflectDurationLv4Value => SkillTreeConfig.GetEffectiveValue("Tanker_ReflectDuration_Lv4", TankerReflectDurationLv4?.Value ?? 16f);
        public static float TankerReflectDurationLv5Value => SkillTreeConfig.GetEffectiveValue("Tanker_ReflectDuration_Lv5", TankerReflectDurationLv5?.Value ?? 20f);

        public static float GetTankerReflectDurationForLevel(int level) => level switch
        {
            1 => TankerReflectDurationLv1Value,
            2 => TankerReflectDurationLv2Value,
            3 => TankerReflectDurationLv3Value,
            4 => TankerReflectDurationLv4Value,
            _ => TankerReflectDurationLv5Value,
        };

        /// <summary>
        /// Tanker 컨피그 초기화 (SkillTreeConfig에서 호출)
        /// </summary>
        /// <param name="config">BepInEx ConfigFile 인스턴스</param>
        public static void InitializeTankerConfig(ConfigFile config)
        {
            try
            {
                Plugin.Log.LogDebug("[탱커 컨피그] 초기화 시작");
                
                // === 전장의 함성 기본 설정 ===
                TankerTauntCooldown = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_Cooldown",
                    60f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_Cooldown")
                );

                TankerTauntStaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_StaminaCost",
                    25f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_StaminaCost")
                );

                TankerTauntRange = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_Range",
                    12f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_Range")
                );

                // === 도발 지속시간 설정 ===
                TankerTauntDuration = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_Duration",
                    9f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_Duration")
                );

                TankerTauntBossDuration = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_BossDuration",
                    1f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_BossDuration")
                );

                // === 피해 감소 효과 설정 ===
                TankerTauntDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_DamageReduction",
                    20f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_DamageReduction")
                );

                TankerTauntBuffDuration = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_BuffDuration",
                    8f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_BuffDuration")
                );

                // === 반사 데미지 설정 ===
                TankerTauntReflectPercent = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_ReflectPercent",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_ReflectPercent")
                );

                // === 시각 효과 설정 ===
                TankerTauntEffectHeight = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_EffectHeight",
                    2.0f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_EffectHeight")
                );

                TankerTauntEffectScale = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_EffectScale",
                    0.3f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Taunt_EffectScale")
                );

                // === 패시브 효과 설정 ===
                TankerPassiveDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Passive_DamageReduction",
                    15f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Passive_DamageReduction"),
                    order: -1
                );

                // === Lv1 패시브 ===
                TankerReflectDurationLv1 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_ReflectDuration_Lv1",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Tanker_ReflectDuration_Lv1"),
                    order: -2
                );

                // === Lv1 체력 보너스 ===
                TankerHpBonusLv1 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Hp_Bonus_Lv1",
                    25f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Hp_Bonus_Lv1"),
                    order: -2
                );

                // === Lv2 패시브 ===
                TankerHpBonusLv2 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Hp_Bonus_Lv2",
                    30f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Hp_Bonus_Lv2"),
                    order: -3
                );

                TankerLv2BlockPower = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Lv2_BlockPower",
                    5f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Lv2_BlockPower"),
                    order: -4
                );

                TankerReflectDurationLv2 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_ReflectDuration_Lv2",
                    12f,
                    SkillTreeConfig.GetConfigDescription("Tanker_ReflectDuration_Lv2"),
                    order: -5
                );

                // === Lv3 패시브 ===
                TankerHpBonusLv3 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Hp_Bonus_Lv3",
                    35f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Hp_Bonus_Lv3"),
                    order: -6
                );

                TankerLv3BlockPower = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Lv3_BlockPower",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Lv3_BlockPower"),
                    order: -7
                );

                TankerReflectDurationLv3 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_ReflectDuration_Lv3",
                    14f,
                    SkillTreeConfig.GetConfigDescription("Tanker_ReflectDuration_Lv3"),
                    order: -8
                );

                // === Lv4 패시브 ===
                TankerHpBonusLv4 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Hp_Bonus_Lv4",
                    40f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Hp_Bonus_Lv4"),
                    order: -9
                );

                TankerLv4BlockPower = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Lv4_BlockPower",
                    15f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Lv4_BlockPower"),
                    order: -10
                );

                TankerReflectDurationLv4 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_ReflectDuration_Lv4",
                    16f,
                    SkillTreeConfig.GetConfigDescription("Tanker_ReflectDuration_Lv4"),
                    order: -11
                );

                // === Lv5 패시브 ===
                TankerHpBonusLv5 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Hp_Bonus_Lv5",
                    50f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Hp_Bonus_Lv5"),
                    order: -12
                );

                TankerLv5BlockPower = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Lv5_BlockPower",
                    20f,
                    SkillTreeConfig.GetConfigDescription("Tanker_Lv5_BlockPower"),
                    order: -13
                );

                TankerReflectDurationLv5 = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_ReflectDuration_Lv5",
                    20f,
                    SkillTreeConfig.GetConfigDescription("Tanker_ReflectDuration_Lv5"),
                    order: -14
                );

                Plugin.Log.LogDebug("[탱커 컨피그] 설정 항목 생성 완료");
                
                // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
                RegisterTankerEventHandlers();
                
                Plugin.Log.LogDebug("[탱커 컨피그] 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[탱커 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 탱커 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterTankerEventHandlers()
        {
            try
            {
                TankerTauntCooldown.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntStaminaCost.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntRange.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntDuration.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntBossDuration.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntDamageReduction.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntBuffDuration.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntEffectHeight.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntEffectScale.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerTauntReflectPercent.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerPassiveDamageReduction.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerHpBonusLv1.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerHpBonusLv2.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerHpBonusLv3.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerHpBonusLv4.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerHpBonusLv5.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerLv2BlockPower.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerLv3BlockPower.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerLv4BlockPower.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerLv5BlockPower.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerReflectDurationLv1.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerReflectDurationLv2.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerReflectDurationLv3.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerReflectDurationLv4.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();
                TankerReflectDurationLv5.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();

                Plugin.Log.LogDebug("[탱커 컨피그] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[탱커 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 탱커 설정 값들을 디버그용으로 출력
        /// </summary>
        public static void LogTankerConfigValues()
        {
            try
            {
                Plugin.Log.LogInfo($"[탱커 컨피그] === 현재 설정값 ===");
                Plugin.Log.LogInfo($"[탱커 컨피그] 쿨타임: {TankerTauntCooldownValue}초");
                Plugin.Log.LogInfo($"[탱커 컨피그] 스태미나 소모: {TankerTauntStaminaCostValue}");
                Plugin.Log.LogInfo($"[탱커 컨피그] 도발 범위: {TankerTauntRangeValue}m");
                Plugin.Log.LogInfo($"[탱커 컨피그] 일반 몬스터 도발 지속시간: {TankerTauntDurationValue}초");
                Plugin.Log.LogInfo($"[탱커 컨피그] 보스 도발 지속시간: {TankerTauntBossDurationValue}초");
                Plugin.Log.LogInfo($"[탱커 컨피그] 피해 감소: {TankerTauntDamageReductionValue}%");
                Plugin.Log.LogInfo($"[탱커 컨피그] 버프 지속시간: {TankerTauntBuffDurationValue}초");
                Plugin.Log.LogInfo($"[탱커 컨피그] 효과 높이: {TankerTauntEffectHeightValue}m");
                Plugin.Log.LogInfo($"[탱커 컨피그] 효과 크기: {TankerTauntEffectScaleValue}배");
                Plugin.Log.LogInfo($"[탱커 컨피그] 패시브 피해 감소: {TankerPassiveDamageReductionValue}%");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[탱커 컨피그] 값 출력 실패: {ex.Message}");
            }
        }
    }
}
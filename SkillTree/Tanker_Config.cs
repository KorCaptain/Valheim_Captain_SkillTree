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
        
        // === 패시브 효과 설정 ===
        public static ConfigEntry<float> TankerPassiveDamageReduction; // 탱커 패시브 피해 감소 (%)

        // === 동적 값 접근자 (MMO 시스템 연동) ===
        public static float TankerTauntCooldownValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_Cooldown", TankerTauntCooldown?.Value ?? 60f);
        public static float TankerTauntStaminaCostValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_StaminaCost", TankerTauntStaminaCost?.Value ?? 25f);
        public static float TankerTauntRangeValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_Range", TankerTauntRange?.Value ?? 12f);
        public static float TankerTauntDurationValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_Duration", TankerTauntDuration?.Value ?? 5f);
        public static float TankerTauntBossDurationValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_BossDuration", TankerTauntBossDuration?.Value ?? 1f);
        public static float TankerTauntDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_DamageReduction", TankerTauntDamageReduction?.Value ?? 20f);
        public static float TankerTauntBuffDurationValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_BuffDuration", TankerTauntBuffDuration?.Value ?? 5f);
        public static float TankerTauntEffectHeightValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_EffectHeight", TankerTauntEffectHeight?.Value ?? 2.0f);
        public static float TankerTauntEffectScaleValue => SkillTreeConfig.GetEffectiveValue("Tanker_Taunt_EffectScale", TankerTauntEffectScale?.Value ?? 0.8f);
        public static float TankerPassiveDamageReductionValue => SkillTreeConfig.GetEffectiveValue("Tanker_Passive_DamageReduction", TankerPassiveDamageReduction?.Value ?? 15f);
        
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
                    "전장의 함성 - 쿨타임 (초) / War Cry - Cooldown (sec)"
                );
                
                TankerTauntStaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_StaminaCost",
                    25f,
                    "전장의 함성 - 스태미나 소모 / War Cry - Stamina Cost"
                );
                
                TankerTauntRange = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_Range",
                    12f,
                    "전장의 함성 - 도발 범위 (m) / War Cry - Taunt Range (m)"
                );
                
                // === 도발 지속시간 설정 ===
                TankerTauntDuration = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_Duration",
                    5f,
                    "전장의 함성 - 일반 몬스터 도발 지속시간 (초) / War Cry - Normal Monster Taunt Duration (sec)"
                );
                
                TankerTauntBossDuration = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_BossDuration",
                    1f,
                    "전장의 함성 - 보스 도발 지속시간 (초) / War Cry - Boss Taunt Duration (sec)"
                );
                
                // === 피해 감소 효과 설정 ===
                TankerTauntDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_DamageReduction",
                    20f,
                    "전장의 함성 - 자신이 받는 피해 감소 (%) / War Cry - Self Damage Reduction (%)"
                );
                
                TankerTauntBuffDuration = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_BuffDuration",
                    5f,
                    "전장의 함성 - 피해 감소 버프 지속시간 (초) / War Cry - Damage Reduction Buff Duration (sec)"
                );
                
                // === 시각 효과 설정 ===
                TankerTauntEffectHeight = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_EffectHeight",
                    2.0f,
                    "전장의 함성 - 몬스터 머리위 taunt 효과 높이 (m) / War Cry - Taunt Effect Height Above Monster (m)"
                );
                
                TankerTauntEffectScale = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Taunt_EffectScale",
                    0.3f,
                    "전장의 함성 - taunt 효과 크기 배율 (30% 크기) / War Cry - Taunt Effect Scale (30% size)"
                );
                
                // === 패시브 효과 설정 ===
                TankerPassiveDamageReduction = SkillTreeConfig.BindServerSync(config,
                    "Tanker Job Skills",
                    "Tanker_Passive_DamageReduction",
                    15f,
                    "탱커 패시브 - 받는 피해량 감소 (%) / Tanker Passive - Damage Taken Reduction (%)"
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
                TankerPassiveDamageReduction.SettingChanged += (sender, args) => Tanker_Tooltip.UpdateTankerTooltip();

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
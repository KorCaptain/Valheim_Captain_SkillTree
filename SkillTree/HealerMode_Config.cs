using BepInEx.Configuration;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 힐러모드 설정 시스템 (동적 컨피그)
    /// 지팡이 전문가 힐 스킬 (staff_Step6_heal) 관련 모든 설정값 관리
    /// </summary>
    public static class HealerMode_Config
    {
        // === 컨피그 항목들 ===
        private static ConfigEntry<float> healerModeDurationConfig;
        private static ConfigEntry<float> healerModeCooldownConfig;
        private static ConfigEntry<float> healerModeEitrCostConfig;
        private static ConfigEntry<float> healPercentageConfig;
        private static ConfigEntry<float> healRangeConfig;
        // 발사체 관련 설정 제거 - 즉시 힐링으로 변경
        private static ConfigEntry<string> healerBuffVFXConfig;
        private static ConfigEntry<string> healerStatusVFXConfig;
        private static ConfigEntry<string> healingVFXConfig;
        private static ConfigEntry<string> activationSoundConfig;

        /// <summary>
        /// 컨피그 초기화
        /// </summary>
        public static void InitConfig(ConfigFile config)
        {
            try
            {
                // === 기본 설정 ===
                healerModeDurationConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 설정", "힐러모드 지속시간", 180f,
                    "힐러모드가 지속되는 시간 (초)");

                healerModeCooldownConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 설정", "힐러모드 쿨타임", 30f,
                    "힐러모드 재사용 대기시간 (초)");

                healerModeEitrCostConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 설정", "힐러모드 Eitr 소모", 30f,
                    "힐러모드 활성화 시 소모되는 Eitr 량");

                // === 힐링 설정 ===
                healPercentageConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 설정", "힐링 비율", 25f,
                    "아군 최대 체력 대비 힐링 비율 (%)");

                healRangeConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 설정", "힐링 범위", 12f,
                    "힐링 발사체 영향 범위 (미터)");

                // === 발사체 설정 제거 - 즉시 힐링으로 변경 ===

                // === 이펙트 설정 ===
                // Valheim 내장 VFX 사용 (buff_03a 대체 - ZNetView 충돌 방지)
                healerBuffVFXConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 이펙트", "힐러모드 버프 VFX", "vfx_Potion_health_medium",
                    "힐러모드 활성화 시 발밑에 표시되는 버프 이펙트");

                healerStatusVFXConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 이펙트", "힐러모드 상태 VFX", "statusailment_01_aura",
                    "힐러모드 활성화 시 머리 위에 표시되는 상태 이펙트");

                healingVFXConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 이펙트", "힐링 VFX", "vfx_HealthUpgrade",
                    "힐링 받는 플레이어에게 표시되는 힐링 이펙트");

                activationSoundConfig = SkillTreeConfig.BindServerSync(config,
                    "힐러모드 이펙트", "활성화 사운드", "sfx_dverger_heal_start",
                    "힐러모드 활성화 시 재생되는 사운드");

                // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
                RegisterHealerModeEventHandlers();

                Plugin.Log.LogDebug("[힐러모드 컨피그] 모든 설정값이 초기화됨");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 컨피그] 설정 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 힐러모드 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterHealerModeEventHandlers()
        {
            try
            {
                healerModeDurationConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                healerModeCooldownConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                healerModeEitrCostConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                healPercentageConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                healRangeConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                healerBuffVFXConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                healerStatusVFXConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                healingVFXConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();
                activationSoundConfig.SettingChanged += (sender, args) => OnHealerModeConfigChanged();

                Plugin.Log.LogDebug("[힐러모드 컨피그] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 힐러모드 컨피그 변경 시 호출
        /// </summary>
        private static void OnHealerModeConfigChanged()
        {
            try
            {
                Plugin.Log.LogInfo("[힐러모드 컨피그] 설정값 변경됨");
                LogCurrentConfig();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 컨피그] 설정 변경 처리 실패: {ex.Message}");
            }
        }

        // === 설정값 접근자들 ===
        public static float HealerModeDurationValue => healerModeDurationConfig?.Value ?? 180f;
        public static float HealerModeCooldownValue => healerModeCooldownConfig?.Value ?? 30f;
        public static float HealerModeEitrCostValue => healerModeEitrCostConfig?.Value ?? 30f;
        public static float HealPercentageValue => healPercentageConfig?.Value ?? 25f;
        public static float HealRangeValue => healRangeConfig?.Value ?? 12f;
        // 발사체 관련 접근자 제거 - 즉시 힐링으로 변경
        public static string HealerBuffVFXValue => healerBuffVFXConfig?.Value ?? "vfx_Potion_health_medium";
        public static string HealerStatusVFXValue => healerStatusVFXConfig?.Value ?? "statusailment_01_aura";
        public static string HealingVFXValue => healingVFXConfig?.Value ?? "vfx_HealthUpgrade";
        public static string ActivationSoundValue => activationSoundConfig?.Value ?? "sfx_dverger_heal_start";

        /// <summary>
        /// 힐러모드 설정값 유효성 검증
        /// </summary>
        public static bool ValidateConfig()
        {
            try
            {
                bool isValid = true;

                // 기본값 범위 검증
                if (HealerModeDurationValue < 60f || HealerModeDurationValue > 600f)
                {
                    Plugin.Log.LogWarning($"[힐러모드 컨피그] 지속시간이 범위를 벗어남: {HealerModeDurationValue}초 (권장: 60-600초)");
                    isValid = false;
                }

                if (HealPercentageValue < 1f || HealPercentageValue > 100f)
                {
                    Plugin.Log.LogWarning($"[힐러모드 컨피그] 힐링 비율이 범위를 벗어남: {HealPercentageValue}% (권장: 1-100%)");
                    isValid = false;
                }

                if (HealRangeValue < 1f || HealRangeValue > 50f)
                {
                    Plugin.Log.LogWarning($"[힐러모드 컨피그] 힐링 범위가 범위를 벗어남: {HealRangeValue}m (권장: 1-50m)");
                    isValid = false;
                }

                // 발사체 관련 검증 제거 - 즉시 힐링으로 변경

                return isValid;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 컨피그] 설정값 검증 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 현재 설정값 디버그 출력
        /// </summary>
        public static void LogCurrentConfig()
        {
            try
            {
                Plugin.Log.LogInfo("[힐러모드 컨피그] 현재 설정값:");
                Plugin.Log.LogInfo($"  - 지속시간: {HealerModeDurationValue}초");
                Plugin.Log.LogInfo($"  - 쿨타임: {HealerModeCooldownValue}초");
                Plugin.Log.LogInfo($"  - Eitr 소모: {HealerModeEitrCostValue}");
                Plugin.Log.LogInfo($"  - 힐링 비율: {HealPercentageValue}%");
                Plugin.Log.LogInfo($"  - 힐링 범위: {HealRangeValue}m");
                // 발사체 관련 로그 제거 - 즉시 힐링으로 변경
                Plugin.Log.LogInfo($"  - 힐러모드 버프 VFX: {HealerBuffVFXValue}");
                Plugin.Log.LogInfo($"  - 힐러모드 상태 VFX: {HealerStatusVFXValue}");
                Plugin.Log.LogInfo($"  - 힐링 VFX: {HealingVFXValue}");
                Plugin.Log.LogInfo($"  - 활성화 사운드: {ActivationSoundValue}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 컨피그] 설정값 로그 출력 실패: {ex.Message}");
            }
        }
    }
}
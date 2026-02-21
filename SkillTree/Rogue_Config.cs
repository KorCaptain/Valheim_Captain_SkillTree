using BepInEx.Configuration;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Rogue 직업 전용 컨피그 시스템
    /// 그림자 일격 - 연막과 함께 어그로 제거 및 공격력 증가 버프
    /// 탱커 시스템과 동일한 구조로 MMO 연동 지원
    /// </summary>
    public static class Rogue_Config
    {
        // === Rogue 그림자 일격 컨피그 엔트리들 ===
        public static ConfigEntry<float> RogueShadowStrikeCooldown;       // 쿨타임 (초)
        public static ConfigEntry<float> RogueShadowStrikeStaminaCost;    // 스태미나 소모 (고정값)
        public static ConfigEntry<float> RogueShadowStrikeAttackBonus;    // 공격력 증가 (%)
        public static ConfigEntry<float> RogueShadowStrikeBuffDuration;   // 공격력 버프 지속시간 (초)
        public static ConfigEntry<float> RogueShadowStrikeSmokeScale;     // 연막 효과 크기 배율
        public static ConfigEntry<float> RogueShadowStrikeAggroRange;     // 어그로 제거 범위 (m)
        public static ConfigEntry<float> RogueShadowStrikeStealthDuration; // 스텔스 지속시간 (초)
        
        // === 로그 패시브 스킬 효과 ===
        public static ConfigEntry<float> RogueAttackSpeedBonus;          // 공격 속도 보너스 (%)
        public static ConfigEntry<float> RogueStaminaReduction;          // 공격 시 스태미나 감소 (%)
        public static ConfigEntry<float> RogueElementalResistanceDebuff; // 속성 저항 감소 (%)

        // === 동적 값 접근자 (MMO 시스템 연동) ===
        public static float RogueShadowStrikeCooldownValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_Cooldown", RogueShadowStrikeCooldown?.Value ?? 30f);
        public static float RogueShadowStrikeStaminaCostValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_StaminaCost", RogueShadowStrikeStaminaCost?.Value ?? 25f);
        public static float RogueShadowStrikeAttackBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_AttackBonus", RogueShadowStrikeAttackBonus?.Value ?? 35f);
        public static float RogueShadowStrikeBuffDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_BuffDuration", RogueShadowStrikeBuffDuration?.Value ?? 8f);
        public static float RogueShadowStrikeSmokeScaleValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_SmokeScale", RogueShadowStrikeSmokeScale?.Value ?? 2.0f);
        public static float RogueShadowStrikeAggroRangeValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_AggroRange", RogueShadowStrikeAggroRange?.Value ?? 15f);
        public static float RogueShadowStrikeStealthDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_StealthDuration", RogueShadowStrikeStealthDuration?.Value ?? 8f);
        
        // === 로그 패시브 스킬 동적 값 접근자 ===
        public static float RogueAttackSpeedBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_AttackSpeed_Bonus", RogueAttackSpeedBonus?.Value ?? 10f);
        public static float RogueStaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Rogue_Stamina_Reduction", RogueStaminaReduction?.Value ?? 15f);
        public static float RogueElementalResistanceDebuffValue => SkillTreeConfig.GetEffectiveValue("Rogue_ElementalResistance_Debuff", RogueElementalResistanceDebuff?.Value ?? 10f);
        
        /// <summary>
        /// Rogue 컨피그 초기화 (SkillTreeConfig에서 호출)
        /// </summary>
        /// <param name="config">BepInEx ConfigFile 인스턴스</param>
        public static void InitializeRogueConfig(ConfigFile config)
        {
            try
            {
                Plugin.Log.LogDebug("[로그 컨피그] 초기화 시작");
                
                // === 그림자 일격 기본 설정 ===
                RogueShadowStrikeCooldown = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_Cooldown",
                    30f,
                    "그림자 일격 - 쿨타임 (초) / Shadow Strike - Cooldown (sec)"
                );
                
                RogueShadowStrikeStaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_StaminaCost",
                    25f,
                    "그림자 일격 - 스태미나 소모 / Shadow Strike - Stamina Cost"
                );
                
                RogueShadowStrikeAttackBonus = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_AttackBonus",
                    35f,
                    "그림자 일격 - 공격력 증가 (%) / Shadow Strike - Attack Power Increase (%)"
                );
                
                RogueShadowStrikeBuffDuration = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_BuffDuration",
                    8f,
                    "그림자 일격 - 공격력 버프 지속시간 (초) / Shadow Strike - Attack Buff Duration (sec)"
                );
                
                RogueShadowStrikeSmokeScale = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_SmokeScale",
                    2.0f,
                    "그림자 일격 - 연막 효과 크기 배율 / Shadow Strike - Smoke Effect Scale"
                );
                
                RogueShadowStrikeAggroRange = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_AggroRange",
                    15f,
                    "그림자 일격 - 어그로 제거 범위 (m) / Shadow Strike - Aggro Clear Range (m)"
                );
                
                RogueShadowStrikeStealthDuration = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_StealthDuration",
                    8f,
                    "그림자 일격 - 스텔스 지속시간 (초) / Shadow Strike - Stealth Duration (sec)"
                );
                
                // === 로그 패시브 스킬 설정 ===
                RogueAttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_AttackSpeed_Bonus",
                    10f,
                    "로그 패시브 - 공격 속도 보너스 (%) / Rogue Passive - Attack Speed Bonus (%)"
                );

                RogueStaminaReduction = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Stamina_Reduction",
                    15f,
                    "로그 패시브 - 공격 시 스태미나 사용량 감소 (%) / Rogue Passive - Attack Stamina Usage Reduction (%)"
                );

                RogueElementalResistanceDebuff = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ElementalResistance_Debuff",
                    10f,
                    "로그 패시브 - 속성 저항 증가 (%) - 속성 피해 감소 / Rogue Passive - Elemental Resistance Increase (%) - Reduce Elemental Damage"
                );
                
                Plugin.Log.LogDebug("[로그 컨피그] 설정 항목 생성 완료 (그림자 일격 + 패시브 효과)");
                
                // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
                RegisterRogueEventHandlers();
                
                Plugin.Log.LogDebug("[로그 컨피그] 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 로그 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterRogueEventHandlers()
        {
            try
            {
                RogueShadowStrikeCooldown.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeStaminaCost.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeAttackBonus.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeBuffDuration.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeSmokeScale.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeAggroRange.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeStealthDuration.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                
                // 로그 패시브 스킬 이벤트 핸들러
                RogueAttackSpeedBonus.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueStaminaReduction.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueElementalResistanceDebuff.SettingChanged += (sender, args) => Rogue_Tooltip.UpdateRogueTooltip();

                Plugin.Log.LogDebug("[로그 컨피그] 이벤트 핸들러 등록 완료 - 툴팁 자동 업데이트 활성화 (그림자 일격 + 패시브 효과)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 로그 설정 값들을 디버그용으로 출력
        /// </summary>
        public static void LogRogueConfigValues()
        {
            try
            {
                Plugin.Log.LogInfo($"[로그 컨피그] === 현재 설정값 ===");
                Plugin.Log.LogInfo($"[로그 컨피그] 쿨타임: {RogueShadowStrikeCooldownValue}초");
                Plugin.Log.LogInfo($"[로그 컨피그] 스태미나 소모: {RogueShadowStrikeStaminaCostValue}");
                Plugin.Log.LogInfo($"[로그 컨피그] 공격력 증가: {RogueShadowStrikeAttackBonusValue}%");
                Plugin.Log.LogInfo($"[로그 컨피그] 버프 지속시간: {RogueShadowStrikeBuffDurationValue}초");
                Plugin.Log.LogInfo($"[로그 컨피그] 연막 크기 배율: {RogueShadowStrikeSmokeScaleValue}배");
                Plugin.Log.LogInfo($"[로그 컨피그] 어그로 제거 범위: {RogueShadowStrikeAggroRangeValue}m");
                Plugin.Log.LogInfo($"[로그 컨피그] 스텔스 지속시간: {RogueShadowStrikeStealthDurationValue}초");
                Plugin.Log.LogInfo($"[로그 컨피그] === 패시브 효과 ===");
                Plugin.Log.LogInfo($"[로그 컨피그] 공격 속도 보너스: +{RogueAttackSpeedBonusValue}%");
                Plugin.Log.LogInfo($"[로그 컨피그] 스태미나 사용량 감소: -{RogueStaminaReductionValue}%");
                Plugin.Log.LogInfo($"[로그 컨피그] 속성 저항 증가: +{RogueElementalResistanceDebuffValue}%");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 컨피그] 값 출력 실패: {ex.Message}");
            }
        }
    }
}
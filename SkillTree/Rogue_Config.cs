using BepInEx.Configuration;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Rogue 직업 전용 컨피그 시스템
    /// 그림자 일격: 8회 연속 독 폭발 (10m 범위, 즉시 +10 독데미지, 10초간 초당 5 독데미지)
    /// </summary>
    public static class Rogue_Config
    {
        // === 그림자 일격 기본 설정 ===
        public static ConfigEntry<float> RogueShadowStrikeCooldown;
        public static ConfigEntry<float> RogueShadowStrikeStaminaCost;
        public static ConfigEntry<float> RogueShadowStrikeAttackBonus;
        public static ConfigEntry<float> RogueShadowStrikeBuffDuration;
        public static ConfigEntry<float> RogueShadowStrikeStealthDuration;

        // === 독 폭발 설정 ===
        public static ConfigEntry<float> RoguePoisonRange;
        public static ConfigEntry<float> RoguePoisonInstantDamage;
        public static ConfigEntry<float> RoguePoisonDotDamage;
        public static ConfigEntry<float> RoguePoisonDotDuration;
        public static ConfigEntry<float> RoguePoisonVFXCount;
        public static ConfigEntry<float> RoguePoisonVFXInterval;

        // === 로그 패시브 스킬 ===
        public static ConfigEntry<float> RogueAttackSpeedBonus;
        public static ConfigEntry<float> RogueStaminaReduction;
        public static ConfigEntry<float> RogueElementalResistanceDebuff;

        // === 동적 값 접근자 ===
        public static float RogueShadowStrikeCooldownValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_Cooldown", RogueShadowStrikeCooldown?.Value ?? 30f);
        public static float RogueShadowStrikeStaminaCostValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_StaminaCost", RogueShadowStrikeStaminaCost?.Value ?? 25f);
        public static float RogueShadowStrikeAttackBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_AttackBonus", RogueShadowStrikeAttackBonus?.Value ?? 35f);
        public static float RogueShadowStrikeBuffDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_BuffDuration", RogueShadowStrikeBuffDuration?.Value ?? 8f);
        public static float RogueShadowStrikeStealthDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_StealthDuration", RogueShadowStrikeStealthDuration?.Value ?? 8f);

        // === 독 폭발 동적 값 접근자 ===
        public static float RoguePoisonRangeValue => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_Range", RoguePoisonRange?.Value ?? 10f);
        public static float RoguePoisonInstantDamageValue => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_InstantDamage", RoguePoisonInstantDamage?.Value ?? 10f);
        public static float RoguePoisonDotDamageValue => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_DotDamage", RoguePoisonDotDamage?.Value ?? 5f);
        public static float RoguePoisonDotDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_DotDuration", RoguePoisonDotDuration?.Value ?? 10f);
        public static int RoguePoisonVFXCountValue => (int)SkillTreeConfig.GetEffectiveValue("Rogue_Poison_VFXCount", RoguePoisonVFXCount?.Value ?? 8f);
        public static float RoguePoisonVFXIntervalValue => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_VFXInterval", RoguePoisonVFXInterval?.Value ?? 0.5f);

        // === 로그 패시브 동적 값 접근자 ===
        public static float RogueAttackSpeedBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_AttackSpeed_Bonus", RogueAttackSpeedBonus?.Value ?? 10f);
        public static float RogueStaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Rogue_Stamina_Reduction", RogueStaminaReduction?.Value ?? 15f);
        public static float RogueElementalResistanceDebuffValue => SkillTreeConfig.GetEffectiveValue("Rogue_ElementalResistance_Debuff", RogueElementalResistanceDebuff?.Value ?? 10f);

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
                    SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_Cooldown")
                );

                RogueShadowStrikeStaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_StaminaCost",
                    25f,
                    SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_StaminaCost")
                );

                RogueShadowStrikeAttackBonus = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_AttackBonus",
                    35f,
                    SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_AttackBonus")
                );

                RogueShadowStrikeBuffDuration = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_BuffDuration",
                    8f,
                    SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_BuffDuration")
                );

                RogueShadowStrikeStealthDuration = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ShadowStrike_StealthDuration",
                    8f,
                    SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_StealthDuration")
                );

                // === 독 폭발 설정 ===
                RoguePoisonRange = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Poison_Range",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Rogue_Poison_Range")
                );

                RoguePoisonInstantDamage = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Poison_InstantDamage",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Rogue_Poison_InstantDamage")
                );

                RoguePoisonDotDamage = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Poison_DotDamage",
                    5f,
                    SkillTreeConfig.GetConfigDescription("Rogue_Poison_DotDamage")
                );

                RoguePoisonDotDuration = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Poison_DotDuration",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Rogue_Poison_DotDuration")
                );

                RoguePoisonVFXCount = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Poison_VFXCount",
                    8f,
                    SkillTreeConfig.GetConfigDescription("Rogue_Poison_VFXCount")
                );

                RoguePoisonVFXInterval = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Poison_VFXInterval",
                    0.5f,
                    SkillTreeConfig.GetConfigDescription("Rogue_Poison_VFXInterval")
                );

                // === 로그 패시브 설정 ===
                RogueAttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_AttackSpeed_Bonus",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Rogue_AttackSpeed_Bonus")
                );

                RogueStaminaReduction = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_Stamina_Reduction",
                    15f,
                    SkillTreeConfig.GetConfigDescription("Rogue_Stamina_Reduction")
                );

                RogueElementalResistanceDebuff = SkillTreeConfig.BindServerSync(config,
                    "Rogue Job Skills",
                    "Rogue_ElementalResistance_Debuff",
                    10f,
                    SkillTreeConfig.GetConfigDescription("Rogue_ElementalResistance_Debuff")
                );

                RegisterRogueEventHandlers();

                Plugin.Log.LogDebug("[로그 컨피그] 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        private static void RegisterRogueEventHandlers()
        {
            try
            {
                RogueShadowStrikeCooldown.SettingChanged       += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeStaminaCost.SettingChanged    += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeAttackBonus.SettingChanged    += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeBuffDuration.SettingChanged   += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeStealthDuration.SettingChanged+= (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RoguePoisonRange.SettingChanged                += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RoguePoisonInstantDamage.SettingChanged        += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RoguePoisonDotDamage.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RoguePoisonDotDuration.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueAttackSpeedBonus.SettingChanged           += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueStaminaReduction.SettingChanged           += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueElementalResistanceDebuff.SettingChanged  += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                Plugin.Log.LogDebug("[로그 컨피그] 이벤트 핸들러 등록 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }
    }
}

using BepInEx.Configuration;
using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 성기사(Paladin) 직업 스킬 설정 - Lv1~5 레벨업 시스템
    /// 신성한 치유 액티브 + 패시브(저항 감소, 스태미나 보너스) 관리
    /// </summary>
    public static class Paladin_Config
    {
        // === 기본 스킬 설정 (공통/소모/간격) ===
        public static ConfigEntry<float> PaladinHealCooldown { get; set; }
        public static ConfigEntry<float> PaladinHealRange { get; set; }
        public static ConfigEntry<float> PaladinHealEitrCost { get; set; }
        public static ConfigEntry<float> PaladinHealStaminaCost { get; set; }
        public static ConfigEntry<float> PaladinHealDuration { get; set; }
        public static ConfigEntry<float> PaladinHealInterval { get; set; }

        // === Lv1: 액티브 + 패시브 ===
        public static ConfigEntry<float> PaladinHealSelfPercent { get; set; }
        public static ConfigEntry<float> PaladinHealAllyPercentOverTime { get; set; }
        public static ConfigEntry<float> PaladinElementalResistanceReduction { get; set; }

        // === Lv2: 액티브 + 패시브 ===
        public static ConfigEntry<float> PaladinLv2Cooldown { get; set; }
        public static ConfigEntry<float> PaladinLv2SelfHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv2AllyHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv2StaminaBonus { get; set; }
        public static ConfigEntry<float> PaladinLv2ResistanceReduction { get; set; }

        // === Lv3: 액티브 + 패시브 ===
        public static ConfigEntry<float> PaladinLv3Cooldown { get; set; }
        public static ConfigEntry<float> PaladinLv3SelfHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv3AllyHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv3HealRange { get; set; }
        public static ConfigEntry<float> PaladinLv3ResistanceReduction { get; set; }
        public static ConfigEntry<float> PaladinLv3StaminaBonus { get; set; }

        // === Lv4: 액티브 + 패시브 ===
        public static ConfigEntry<float> PaladinLv4Cooldown { get; set; }
        public static ConfigEntry<float> PaladinLv4SelfHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv4AllyHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv4HealRange { get; set; }
        public static ConfigEntry<float> PaladinLv4ResistanceReduction { get; set; }
        public static ConfigEntry<float> PaladinLv4StaminaBonus { get; set; }

        // === Lv5: 액티브 + 패시브 ===
        public static ConfigEntry<float> PaladinLv5SelfHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv5AllyHealPercent { get; set; }
        public static ConfigEntry<float> PaladinLv5HealRange { get; set; }
        public static ConfigEntry<float> PaladinLv5Cooldown { get; set; }
        public static ConfigEntry<float> PaladinLv5ResistanceReduction { get; set; }
        public static ConfigEntry<float> PaladinLv5StaminaBonus { get; set; }

        /// <summary>
        /// 성기사 컨피그 초기화
        /// </summary>
        public static void InitializePaladinConfig()
        {
            // === 공통 설정 ===
            PaladinHealCooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_Cooldown", 30f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_Cooldown"));

            PaladinHealRange = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_Range", 5f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_Range"));

            PaladinHealEitrCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_EitrCost", 10f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_EitrCost"));

            PaladinHealStaminaCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_StaminaCost", 10f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_StaminaCost"));

            PaladinHealDuration = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_Duration", 10f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_Duration"));

            PaladinHealInterval = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_Interval", 1f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_Interval"));

            // === Lv1: 액티브 + 패시브 ===
            PaladinHealSelfPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_SelfHealPercent", 15f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_SelfHealPercent"));

            PaladinHealAllyPercentOverTime = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Active_AllyHealPercentOverTime", 2f,
                SkillTreeConfig.GetConfigDescription("Paladin_Active_AllyHealPercentOverTime"));

            PaladinElementalResistanceReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Passive_ElementalResistanceReduction", 5f,
                SkillTreeConfig.GetConfigDescription("Paladin_Passive_ElementalResistanceReduction"));
            if (PaladinElementalResistanceReduction.Value == 8f)
                PaladinElementalResistanceReduction.Value = 5f;

            // === Lv2: 액티브 + 패시브 ===
            PaladinLv2Cooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv2_Cooldown", 29f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv2_Cooldown"));

            PaladinLv2SelfHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv2_SelfHealPercent", 17f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv2_SelfHealPercent"));

            PaladinLv2AllyHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv2_AllyHealPercent", 2.2f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv2_AllyHealPercent"));
            if (PaladinLv2AllyHealPercent.Value == 2.5f) PaladinLv2AllyHealPercent.Value = 2.2f;

            PaladinLv2StaminaBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv2_StaminaBonus", 10f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv2_StaminaBonus"));

            PaladinLv2ResistanceReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv2_ResistanceReduction", 8f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv2_ResistanceReduction"));

            // === Lv3: 액티브 + 패시브 ===
            PaladinLv3Cooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv3_Cooldown", 28f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv3_Cooldown"));

            PaladinLv3SelfHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv3_SelfHealPercent", 19f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv3_SelfHealPercent"));

            PaladinLv3AllyHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv3_AllyHealPercent", 2.5f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv3_AllyHealPercent"));
            if (PaladinLv3AllyHealPercent.Value == 3f) PaladinLv3AllyHealPercent.Value = 2.5f;

            PaladinLv3HealRange = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv3_HealRange", 6f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv3_HealRange"));

            PaladinLv3ResistanceReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv3_ResistanceReduction", 10f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv3_ResistanceReduction"));

            PaladinLv3StaminaBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv3_StaminaBonus", 15f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv3_StaminaBonus"));

            // === Lv4: 액티브 + 패시브 ===
            PaladinLv4Cooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv4_Cooldown", 27f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv4_Cooldown"));

            PaladinLv4SelfHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv4_SelfHealPercent", 21f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv4_SelfHealPercent"));

            PaladinLv4AllyHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv4_AllyHealPercent", 2.7f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv4_AllyHealPercent"));
            if (PaladinLv4AllyHealPercent.Value == 3.5f) PaladinLv4AllyHealPercent.Value = 2.7f;

            PaladinLv4HealRange = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv4_HealRange", 7f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv4_HealRange"));

            PaladinLv4ResistanceReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv4_ResistanceReduction", 13f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv4_ResistanceReduction"));
            if (PaladinLv4ResistanceReduction.Value == 12f)
                PaladinLv4ResistanceReduction.Value = 13f;

            PaladinLv4StaminaBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv4_StaminaBonus", 20f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv4_StaminaBonus"));

            // === Lv5: 액티브 + 패시브 ===
            PaladinLv5SelfHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv5_SelfHealPercent", 24f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv5_SelfHealPercent"));

            PaladinLv5AllyHealPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv5_AllyHealPercent", 3.0f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv5_AllyHealPercent"));
            if (PaladinLv5AllyHealPercent.Value == 4f) PaladinLv5AllyHealPercent.Value = 3.0f;

            PaladinLv5HealRange = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv5_HealRange", 8f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv5_HealRange"));

            PaladinLv5Cooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv5_Cooldown", 26f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv5_Cooldown"));

            PaladinLv5ResistanceReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv5_ResistanceReduction", 15f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv5_ResistanceReduction"));

            PaladinLv5StaminaBonus = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills", "Paladin_Lv5_StaminaBonus", 25f,
                SkillTreeConfig.GetConfigDescription("Paladin_Lv5_StaminaBonus"));

            RegisterPaladinEventHandlers();
            Plugin.Log.LogDebug("[Paladin Config] 성기사 컨피그 초기화 완료 (Lv1~5)");
        }

        private static void RegisterPaladinEventHandlers()
        {
            try
            {
                PaladinHealCooldown.SettingChanged              += (s, a) => UpdatePaladinTooltip();
                PaladinHealRange.SettingChanged                 += (s, a) => UpdatePaladinTooltip();
                PaladinHealEitrCost.SettingChanged              += (s, a) => UpdatePaladinTooltip();
                PaladinHealStaminaCost.SettingChanged           += (s, a) => UpdatePaladinTooltip();
                PaladinHealSelfPercent.SettingChanged           += (s, a) => UpdatePaladinTooltip();
                PaladinHealAllyPercentOverTime.SettingChanged   += (s, a) => UpdatePaladinTooltip();
                PaladinLv2SelfHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv2AllyHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv3SelfHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv3AllyHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv3HealRange.SettingChanged              += (s, a) => UpdatePaladinTooltip();
                PaladinLv4SelfHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv4AllyHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv4HealRange.SettingChanged              += (s, a) => UpdatePaladinTooltip();
                PaladinLv5SelfHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv5AllyHealPercent.SettingChanged        += (s, a) => UpdatePaladinTooltip();
                PaladinLv5HealRange.SettingChanged              += (s, a) => UpdatePaladinTooltip();
                PaladinLv2Cooldown.SettingChanged               += (s, a) => UpdatePaladinTooltip();
                PaladinLv3Cooldown.SettingChanged               += (s, a) => UpdatePaladinTooltip();
                PaladinLv4Cooldown.SettingChanged               += (s, a) => UpdatePaladinTooltip();
                PaladinLv5Cooldown.SettingChanged               += (s, a) => UpdatePaladinTooltip();
                PaladinElementalResistanceReduction.SettingChanged += (s, a) => UpdatePaladinTooltip();
                PaladinLv3ResistanceReduction.SettingChanged    += (s, a) => UpdatePaladinTooltip();
                PaladinLv2ResistanceReduction.SettingChanged    += (s, a) => UpdatePaladinTooltip();
                PaladinLv4ResistanceReduction.SettingChanged    += (s, a) => UpdatePaladinTooltip();
                PaladinLv5ResistanceReduction.SettingChanged    += (s, a) => UpdatePaladinTooltip();
                PaladinLv2StaminaBonus.SettingChanged           += (s, a) => UpdatePaladinTooltip();
                PaladinLv3StaminaBonus.SettingChanged           += (s, a) => UpdatePaladinTooltip();
                PaladinLv4StaminaBonus.SettingChanged           += (s, a) => UpdatePaladinTooltip();
                PaladinLv5StaminaBonus.SettingChanged           += (s, a) => UpdatePaladinTooltip();
                Plugin.Log.LogDebug("[Paladin Config] 이벤트 핸들러 등록 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Paladin Config] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        private static void UpdatePaladinTooltip()
        {
            try { Paladin_Tooltip.UpdatePaladinTooltip(); }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Paladin Config] UpdatePaladinTooltip 실패: {ex.Message}");
            }
        }

        // ===================================================
        // 레벨별 헬퍼 메서드
        // ===================================================

        /// <summary>자가 치유 비율 (%) - 레벨별</summary>
        public static float GetSelfHealPercent(int level)
        {
            return level switch {
                2 => PaladinLv2SelfHealPercent?.Value ?? 17f,
                3 => PaladinLv3SelfHealPercent?.Value ?? 19f,
                4 => PaladinLv4SelfHealPercent?.Value ?? 21f,
                _ => level >= 5 ? (PaladinLv5SelfHealPercent?.Value ?? 24f)
                                 : (PaladinHealSelfPercent?.Value ?? 15f)
            };
        }

        /// <summary>아군 지속 치유 비율 (%/틱) - 레벨별</summary>
        public static float GetAllyHealPercent(int level)
        {
            return level switch {
                2 => PaladinLv2AllyHealPercent?.Value ?? 2.2f,
                3 => PaladinLv3AllyHealPercent?.Value ?? 2.5f,
                4 => PaladinLv4AllyHealPercent?.Value ?? 2.7f,
                _ => level >= 5 ? (PaladinLv5AllyHealPercent?.Value ?? 3.0f)
                                 : (PaladinHealAllyPercentOverTime?.Value ?? 2f)
            };
        }

        /// <summary>치유 범위 (m) - 레벨별</summary>
        public static float GetHealRange(int level)
        {
            return level switch {
                3 => PaladinLv3HealRange?.Value ?? 6f,
                4 => PaladinLv4HealRange?.Value ?? 7f,
                _ => level >= 5 ? (PaladinLv5HealRange?.Value ?? 8f)
                                 : (PaladinHealRange?.Value ?? 5f)
            };
        }

        /// <summary>쿨타임 (초) - 레벨별 (Lv2=29, Lv3=28, Lv4=27, Lv5+=26)</summary>
        public static float GetCooldown(int level)
        {
            return level switch {
                2 => PaladinLv2Cooldown?.Value ?? 29f,
                3 => PaladinLv3Cooldown?.Value ?? 28f,
                4 => PaladinLv4Cooldown?.Value ?? 27f,
                _ => level >= 5 ? (PaladinLv5Cooldown?.Value ?? 26f)
                                : (PaladinHealCooldown?.Value ?? 30f)
            };
        }

        /// <summary>최대 스태미나 보너스 - 레벨별 (Lv2부터 시작)</summary>
        public static float GetStaminaBonus(int level)
        {
            return level switch {
                2 => PaladinLv2StaminaBonus?.Value ?? 10f,
                3 => PaladinLv3StaminaBonus?.Value ?? 15f,
                4 => PaladinLv4StaminaBonus?.Value ?? 20f,
                _ => level >= 5 ? (PaladinLv5StaminaBonus?.Value ?? 25f) : 0f
            };
        }

        /// <summary>적 저항 감소 (%) - 레벨별</summary>
        public static float GetResistanceReduction(int level)
        {
            return level switch {
                2 => PaladinLv2ResistanceReduction?.Value ?? 8f,
                3 => PaladinLv3ResistanceReduction?.Value ?? 10f,
                4 => PaladinLv4ResistanceReduction?.Value ?? 13f,
                _ => level >= 5 ? (PaladinLv5ResistanceReduction?.Value ?? 15f)
                                 : (PaladinElementalResistanceReduction?.Value ?? 5f)
            };
        }

        /// <summary>레벨 업그레이드 필요 트로피 텍스트</summary>
        public static string GetPaladinLevelCostText(int targetLevel)
        {
            try
            {
                return targetLevel switch {
                    2 => $"{L.Get("item_trophy_troll")} x1 + {L.Get("item_trophy_elder")} x1 + {L.Get("item_coins")} x{SkillTreeConfig.GetJobLevelCost(2)}\n+ {L.Get("paladin_lv2_skill_required")}",
                    3 => $"{L.Get("item_trophy_abomination")} x1 + {L.Get("item_trophy_bonemass")} x1 + {L.Get("item_coins")} x{SkillTreeConfig.GetJobLevelCost(3)}",
                    4 => $"{L.Get("item_trophy_bonemass")} x1 + {L.Get("item_trophy_dragonqueen")} x1 + {L.Get("item_coins")} x{SkillTreeConfig.GetJobLevelCost(4)}",
                    5 => $"{L.Get("item_trophy_goblinking")} x1 + {L.Get("item_trophy_seekerqueen")} x1 + {L.Get("item_coins")} x{SkillTreeConfig.GetJobLevelCost(5)}",
                    _ => $"{L.Get("item_trophy_bear")} x1 + {L.Get("item_eikthyr_trophy")} x1 + {L.Get("item_coins")} x{SkillTreeConfig.GetJobLevelCost(1)}"
                };
            }
            catch { return ""; }
        }

        // ===================================================
        // 하위 호환 편의 프로퍼티 (기존 코드 호환)
        // ===================================================
        public static float CooldownValue        => PaladinHealCooldown?.Value ?? 30f;
        public static float RangeValue           => PaladinHealRange?.Value ?? 5f;
        public static float EitrCostValue        => PaladinHealEitrCost?.Value ?? 10f;
        public static float StaminaCostValue     => PaladinHealStaminaCost?.Value ?? 10f;
        public static float SelfHealPercentValue => (PaladinHealSelfPercent?.Value ?? 15f) / 100f;
        public static float AllyHealPercentValue => (PaladinHealAllyPercentOverTime?.Value ?? 2f) / 100f;
        public static float HealDurationValue    => PaladinHealDuration?.Value ?? 10f;
        public static float HealIntervalValue    => PaladinHealInterval?.Value ?? 1f;
        public static float ElementalResistanceReductionValue => PaladinElementalResistanceReduction?.Value ?? 8f;
        public static int   TotalHealTicks       => (int)(HealDurationValue / HealIntervalValue);
        public static float TotalAllyHealPercent => AllyHealPercentValue * TotalHealTicks;
        public static bool  ShowHealNumbersValue => true;
        public static bool  ShowHealProgressValue => true;
    }
}

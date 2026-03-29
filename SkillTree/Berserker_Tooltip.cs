using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 버서커 직업 전용 툴팁 시스템 - 아처/제작 전문가 패턴 기반 다중 레벨
    /// </summary>
    public static class Berserker_Tooltip
    {
        /// <summary>
        /// 버서커 상세 툴팁 생성
        /// </summary>
        public static string GetBerserkerTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("Berserker") ?? 0;
            int displayLevel = Math.Min(currentLevel + 1, 5); // 다음 레벨 (필요 아이템용)
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;

            float cooldown   = Berserker_Config.GetEffectiveRageCooldown(mainLevel);
            float duration   = Berserker_Config.GetEffectiveRageDuration(mainLevel);
            float maxDmg     = Berserker_Config.GetEffectiveMaxDamageBonus(mainLevel);
            float stamina    = Berserker_Config.BerserkerRageStaminaCostValue;

            var tooltip = $"<color=#FFD700><size=22>{L.Get("job_berserker")}</size></color>\n";

            // 메인 블록: 현재 레벨 액티브 스탯
            tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel}: ";
            tooltip += L.Get("berserker_active_desc", (int)cooldown, (int)duration, (int)maxDmg);
            tooltip += $"</size></color>\n";

            // 패시브 라인
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{GetPassiveStr(mainLevel)}</size></color>\n";

            // 소모
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            tooltip += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {(int)stamina}</size></color>\n";

            // 스킬 유형
            tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{L.Get("skill_type_active_key", "Y")}</size></color>\n";

            // 쿨타임
            tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            tooltip += $"<color=#FFDB58><size=16>{(int)cooldown}{L.Get("unit_seconds")}</size></color>\n";

            // 필요조건
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            tooltip += $"<color=#00FF00><size=16>{L.Get("tooltip_none")}</size></color>\n";

            // 확인사항
            tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>";
            tooltip += $"<color=#FFE4B5><size=16>{L.Get("confirmation_job_only")}</size></color>\n";

            // 필요 아이템 or 최대레벨
            if (currentLevel < 5)
            {
                tooltip += $"<color=#FFA500><size=16>{L.Get("berserker_level_req_items", displayLevel)}: </size></color>";
                tooltip += $"<color=#FF6B6B><size=16>{GetBerserkerLevelCostText(displayLevel)}</size></color>\n";
            }
            else
            {
                tooltip += $"<color=#FFD700><size=16>{L.Get("berserker_max_level")}</size></color>\n";
            }

            // 구분선 + 프리뷰 (mainLevel < 5인 경우)
            if (mainLevel < 5)
            {
                tooltip += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 5; lv++)
                {
                    float lvCd  = Berserker_Config.GetEffectiveRageCooldown(lv);
                    float lvDur = Berserker_Config.GetEffectiveRageDuration(lv);
                    float lvDmg = Berserker_Config.GetEffectiveMaxDamageBonus(lv);
                    tooltip += $"<color=#808080><size=14>Lv{lv}: ";
                    tooltip += L.Get("berserker_active_desc", (int)lvCd, (int)lvDur, (int)lvDmg);
                    tooltip += $"\n  {L.Get("tooltip_passive")}: {GetPassiveStr(lv)}</size></color>\n";
                }
            }

            return tooltip.TrimEnd('\n');
        }

        private static string GetPassiveStr(int level)
        {
            float hpBonus   = Berserker_Config.GetEffectiveHealthBonus(level);
            float threshold = Berserker_Config.BerserkerPassiveHealthThresholdValue;
            float invDur    = Berserker_Config.GetEffectiveInvincibilityDuration(level);
            float passCd    = Berserker_Config.GetEffectivePassiveCooldown(level);
            float dmgRed    = Berserker_Config.BerserkerLv3RageDamageReductionValue;
            float lowHpAtk  = Berserker_Config.BerserkerLv4LowHpAttackBonusValue;
            float lowHpThr  = Berserker_Config.BerserkerLv4LowHpAttackThresholdValue;
            float cdRedMin  = Berserker_Config.BerserkerLv5PassiveCooldownReductionValue / 60f;
            float invBonus  = Berserker_Config.BerserkerLv5InvincibilityBonusValue;

            switch (level)
            {
                case 1:
                    return L.Get("berserker_passive_lv1",
                        (int)hpBonus, (int)threshold, (int)invDur, (int)(passCd / 60f));
                case 2:
                    return L.Get("berserker_passive_lv2",
                        (int)hpBonus, (int)threshold, (int)invDur, (int)(passCd / 60f));
                case 3:
                    return L.Get("berserker_passive_lv3",
                        (int)hpBonus, (int)threshold, (int)invDur, (int)(passCd / 60f), (int)dmgRed);
                case 4:
                    return L.Get("berserker_passive_lv4",
                        (int)hpBonus, (int)threshold, (int)invDur, (int)(passCd / 60f),
                        (int)dmgRed, (int)lowHpAtk, (int)lowHpThr);
                default:
                    return L.Get("berserker_passive_lv5",
                        (int)hpBonus, (int)threshold, (int)invDur, (int)(passCd / 60f),
                        (int)dmgRed, (int)lowHpAtk, (int)lowHpThr,
                        (int)cdRedMin, (int)invBonus);
            }
        }

        private static string GetBerserkerLevelCostText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1:  return L.Get("item_trophy_bear") + " x1 + " + L.Get("item_eikthyr_trophy") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(1);
                case 2:  return L.Get("item_trophy_troll") + " x1 + " + L.Get("item_trophy_theelder") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(2);
                case 3:  return L.Get("item_trophy_abomination") + " x1 + " + L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(3);
                case 4:  return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(4);
                case 5:  return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(5);
                default: return "";
            }
        }

        /// <summary>
        /// 버서커 툴팁 강제 업데이트 (레벨업/컨피그 변경 시 호출)
        /// </summary>
        public static void UpdateBerserkerTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Berserker"))
                {
                    manager.SkillNodes["Berserker"].Description = GetBerserkerTooltip();
                }
            }
            catch (Exception)
            {
                // silent fail
            }
        }

        // ─── 하위 호환성 유지 (구버전 호출부 대응) ─────────────────────────────
        /// <summary>구버전 GetBerserkerTooltip() 래퍼 (동일 반환)</summary>
        public static string GenerateBerserkerTooltip(object _) => GetBerserkerTooltip();

        /// <summary>현재 데미지 보너스 (분노 중 실시간) - 이전 API 유지</summary>
        public static string GetCurrentDamageBonusTooltip(Player player)
        {
            try
            {
                if (player == null) return L.Get("tooltip_no_player");
                if (!BerserkerSkills.IsPlayerInRage(player)) return L.Get("berserker_not_in_rage");
                float dmg    = BerserkerSkills.GetRageDamageBonus(player);
                float hpPct  = player.GetHealthPercentage() * 100f;
                return L.Get("berserker_current_status", hpPct, dmg);
            }
            catch (Exception) { return L.Get("tooltip_calculation_error"); }
        }
    }
}

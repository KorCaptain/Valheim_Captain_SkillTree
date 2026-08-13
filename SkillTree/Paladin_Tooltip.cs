using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 성기사(Paladin) 직업 전용 툴팁 시스템 - Producer/Archer 패턴 기반 Lv1~5
    /// </summary>
    public static class Paladin_Tooltip
    {
        /// <summary>
        /// 성기사 상세 툴팁 생성
        /// </summary>
        public static string GetPaladinTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("Paladin") ?? 0;
            int displayLevel = Math.Min(currentLevel + 1, 5); // 다음 레벨 (필요 아이템용)
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;

            float selfHeal = Paladin_Config.GetSelfHealPercent(mainLevel);
            float allyHeal = Paladin_Config.GetAllyHealPercent(mainLevel);
            float range    = Paladin_Config.GetHealRange(mainLevel);
            float cooldown = Paladin_Config.GetCooldown(mainLevel);
            float duration = Paladin_Config.HealDurationValue;
            float stamina  = Paladin_Config.StaminaCostValue;
            float eitr     = Paladin_Config.EitrCostValue;

            var tooltip = $"<color=#FFD700><size=22>{L.Get("paladin_name")}</size></color>\n";

            // 메인 블록: 현재 레벨 액티브 설명
            tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel}: ";
            tooltip += L.Get("paladin_active_desc", selfHeal, allyHeal, (int)duration);
            tooltip += $"</size></color>\n";

            // 패시브 라인
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{GetPassiveStr(mainLevel)}</size></color>\n";

            // 소모
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            tooltip += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {(int)stamina}, {L.Get("stat_eitr")} {(int)eitr}</size></color>\n";

            // 범위
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_range")}: </size></color>";
            tooltip += $"<color=#FFDAB9><size=16>{range}m</size></color>\n";

            // 스킬 유형
            tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{L.Get("skill_type_active_key", "Y")}</size></color>\n";

            // 쿨타임
            tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            tooltip += $"<color=#FFDB58><size=16>{(int)cooldown}{L.Get("unit_seconds")}</size></color>\n";

            // 필요조건
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            tooltip += $"<color=#00FF00><size=16>{L.Get("paladin_requirement")}</size></color>\n";

            // 확인사항
            tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>";
            tooltip += $"<color=#FFE4B5><size=16>{L.Get("confirmation_job_only")}</size></color>\n";

            // 필요 아이템 or 최대레벨
            if (currentLevel < 5)
            {
                tooltip += $"<color=#FFA500><size=16>{L.Get("paladin_level_req_items", displayLevel)}: </size></color>";
                tooltip += $"<color=#FF6B6B><size=16>{Paladin_Config.GetPaladinLevelCostText(displayLevel)}</size></color>\n";
            }
            else
            {
                tooltip += $"<color=#FFD700><size=16>{L.Get("paladin_max_level")}</size></color>\n";
            }

            // 구분선 + 프리뷰
            if (mainLevel < 5)
            {
                tooltip += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 5; lv++)
                {
                    float pSelf = Paladin_Config.GetSelfHealPercent(lv);
                    float pAlly = Paladin_Config.GetAllyHealPercent(lv);
                    float pRange = Paladin_Config.GetHealRange(lv);
                    float pCd   = Paladin_Config.GetCooldown(lv);
                    tooltip += $"<color=#808080><size=14>Lv{lv}: {L.Get("paladin_active_desc", pSelf, pAlly, (int)duration)}\n  {GetPassiveStr(lv)} | {L.Get("tooltip_cooldown")} {(int)pCd}{L.Get("unit_seconds")}</size></color>\n";
                }
            }

            return tooltip.TrimEnd('\n');
        }

        /// <summary>레벨별 패시브 요약 문자열</summary>
        private static string GetPassiveStr(int level)
        {
            float resist = Paladin_Config.GetResistanceReduction(level);
            float stamBonus = Paladin_Config.GetStaminaBonus(level);

            switch (level)
            {
                case 1:
                    return L.Get("paladin_passive_lv1", (int)resist);
                case 2:
                    return L.Get("paladin_passive_lv2", (int)stamBonus, (int)resist);
                case 3:
                    return L.Get("paladin_passive_lv3", (int)stamBonus, (int)resist);
                case 4:
                    return L.Get("paladin_passive_lv4", (int)stamBonus, (int)resist);
                default:
                    return L.Get("paladin_passive_lv5", (int)stamBonus, (int)resist);
            }
        }

        /// <summary>성기사 툴팁 강제 업데이트</summary>
        public static void UpdatePaladinTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Paladin"))
                {
                    manager.SkillNodes["Paladin"].Description = GetPaladinTooltip();
                }
            }
            catch (Exception)
            {
                // silent fail
            }
        }
    }
}

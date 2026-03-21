using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 직업 전용 툴팁 시스템 - Producer 패턴 기반 다중 레벨
    /// </summary>
    public static class Mage_Tooltip
    {
        /// <summary>
        /// 메이지 상세 툴팁 생성
        /// </summary>
        public static string GetMageTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("Mage") ?? 0;
            int displayLevel = Math.Min(currentLevel + 1, 5); // 다음 레벨 (필요 아이템용)
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;

            float range      = Mage_Config.MageAOERangeValue;
            int   maxTargets = Mage_Config.GetMaxTargets(mainLevel);
            int   eitrCost   = Mage_Config.MageEitrCostValue;
            float cooldown   = Mage_Config.GetCooldown(mainLevel);
            float dmgMult    = Mage_Config.GetDamageMultiplier(mainLevel);
            float resistance = Mage_Config.GetElementalResistance(mainLevel);

            var tooltip = $"<color=#FFD700><size=22>{L.Get("job_mage")}</size></color>\n";

            // 메인 블록: 현재 레벨 스탯
            tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel}: ";
            tooltip += L.Get("mage_desc_aoe", (int)dmgMult);
            tooltip += $"</size></color>\n";

            // 패시브 라인
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{GetPassiveStr(mainLevel)}</size></color>\n";

            // 범위 + 최대 타겟
            tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color>";
            tooltip += $"<color=#B0E0E6><size=16>{range}m</size></color>\n";

            // 소모
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            tooltip += $"<color=#FFDAB9><size=16>{L.Get("stat_eitr")} {eitrCost}</size></color>\n";

            // 스킬 유형
            tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{L.Get("skill_type_job_active", "Y")}</size></color>\n";

            // 쿨타임
            tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            tooltip += $"<color=#FFDB58><size=16>{(int)cooldown}{L.Get("unit_seconds")}</size></color>\n";

            // 필요조건
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            tooltip += $"<color=#00FF00><size=16>{L.Get("requirement_mage")}</size></color>\n";

            // 확인사항
            tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>";
            tooltip += $"<color=#FFE4B5><size=16>{L.Get("confirmation_job_only")}</size></color>\n";

            // 필요 아이템 or 최대레벨
            if (currentLevel < 5)
            {
                tooltip += $"<color=#FFA500><size=16>{L.Get("mage_level_req_items", displayLevel)}: </size></color>";
                tooltip += $"<color=#FF6B6B><size=16>{GetMageLevelCostText(displayLevel)}</size></color>\n";
            }
            else
            {
                tooltip += $"<color=#FFD700><size=16>{L.Get("mage_max_level")}</size></color>\n";
            }

            // 구분선 + 프리뷰 (mainLevel < 5인 경우)
            if (mainLevel < 5)
            {
                tooltip += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 5; lv++)
                {
                    tooltip += $"<color=#808080><size=14>Lv{lv}: {GetPassiveStr(lv)}</size></color>\n";
                }
            }

            return tooltip.TrimEnd('\n');
        }

        private static string GetPassiveStr(int level)
        {
            float resist = Mage_Config.GetElementalResistance(level);
            float dmg    = Mage_Config.GetDamageMultiplier(level);

            switch (level)
            {
                case 2:
                    return L.Get("mage_passive_lv2", (int)resist, (int)dmg);
                default:
                    return L.Get("mage_passive_lv1", (int)resist, (int)dmg);
            }
        }

        private static string GetMageLevelCostText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_greydwarfshaman") + " x1 + " + L.Get("item_eikthyr_trophy") + " x1";
                case 2: return L.Get("item_trophy_troll") + " x1 + " + L.Get("item_trophy_theelder") + " x1";
                case 3: return L.Get("item_trophy_wraith") + " x1 + " + L.Get("item_trophy_bonemass") + " x1";
                case 4: return L.Get("item_trophy_hatchling") + " x1 + " + L.Get("item_trophy_dragonqueen") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_seekerqueen") + " x1";
                default: return "";
            }
        }

        /// <summary>
        /// 메이지 툴팁 강제 업데이트
        /// </summary>
        public static void UpdateMageTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Mage"))
                {
                    manager.SkillNodes["Mage"].Description = GetMageTooltip();
                }
            }
            catch (System.Exception)
            {
                // silent fail
            }
        }
    }
}

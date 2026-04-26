using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가(Producer) 직업 전용 툴팁 시스템 - 아처 패턴 기반 다중 레벨
    /// </summary>
    public static class Producer_Tooltip
    {
        /// <summary>
        /// 제작 전문가 상세 툴팁 생성
        /// </summary>
        public static string GetProducerTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("Producer") ?? 0;
            int displayLevel = Math.Min(currentLevel + 1, 5); // 다음 레벨 (필요 아이템용)
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;

            float attack   = Producer_Config.ProducerBuff_AttackBonusValue;
            float health   = Producer_Config.ProducerBuff_MaxHealthBonusValue;
            float duration = Producer_Config.ProducerBuff_DurationValue;
            float cooldown = Producer_Config.ProducerBuff_CooldownValue;
            float stamina  = Producer_Config.ProducerBuff_StaminaCostValue;

            var tooltip = $"<color=#FFD700><size=22>{L.Get("job_producer")}</size></color>\n";

            // 메인 블록: 현재 레벨 스탯
            tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel}: ";
            tooltip += L.Get("producer_buff_desc", (int)duration, (int)attack, (int)health);
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

            // 필요조건 (아처 구조 통일: requirements 줄 추가)
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            tooltip += $"<color=#00FF00><size=16>{L.Get("tooltip_none")}</size></color>\n";

            // 확인사항
            tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>";
            tooltip += $"<color=#FFE4B5><size=16>{L.Get("confirmation_job_only")}</size></color>\n";

            // 필요 아이템 or 최대레벨
            if (currentLevel < 5)
            {
                tooltip += $"<color=#FFA500><size=16>{L.Get("producer_level_req_items", displayLevel)}: </size></color>";
                tooltip += $"<color=#FF6B6B><size=16>{GetProducerLevelCostText(displayLevel)}</size></color>\n";
            }
            else
            {
                tooltip += $"<color=#FFD700><size=16>{L.Get("producer_max_level")}</size></color>\n";
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
            int farmGrid    = Producer_Config.GetFarmGridCount(level);
            float dur       = Producer_Config.GetDurabilityBonus(level);
            float mat       = Producer_Config.GetMaterialReduction(level);
            float ench      = Producer_Config.GetEnchantChance(level);
            float success   = Producer_Config.GetCraftingSuccessRate(level);

            switch (level)
            {
                case 1:
                    return L.Get("producer_passive_lv1", farmGrid, (int)dur, (int)success);
                case 2:
                    return L.Get("producer_passive_lv2", farmGrid, (int)dur, (int)mat, (int)success);
                case 3:
                    return L.Get("producer_passive_lv3", farmGrid, (int)dur, (int)mat, (int)ench, (int)success)
                        + "\n" + L.Get("producer_enchant_detail_lv3");
                case 4:
                    return L.Get("producer_passive_lv4", farmGrid, (int)dur, (int)mat, (int)ench, (int)success)
                        + "\n" + L.Get("producer_enchant_detail_lv4");
                default:
                    return L.Get("producer_passive_lv5", farmGrid, (int)dur, (int)mat, (int)ench, (int)success)
                        + "\n" + L.Get("producer_enchant_detail_lv5");
            }
        }

        private static string GetProducerLevelCostText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1:  return L.Get("item_trophy_bear") + " x1 + " + L.Get("item_eikthyr_trophy") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(1);
                case 2:  return L.Get("item_trophy_troll") + " x1 + " + L.Get("item_trophy_elder") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(2);
                case 3:  return L.Get("item_trophy_abomination") + " x1 + " + L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(3);
                case 4:  return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(4);
                case 5:  return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_coins") + " x" + SkillTreeConfig.GetJobLevelCost(5);
                default: return "";
            }
        }

        /// <summary>
        /// 제작 전문가 툴팁 강제 업데이트
        /// </summary>
        public static void UpdateProducerTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Producer"))
                {
                    manager.SkillNodes["Producer"].Description = GetProducerTooltip();
                }
            }
            catch (System.Exception)
            {
                // silent fail
            }
        }
    }
}

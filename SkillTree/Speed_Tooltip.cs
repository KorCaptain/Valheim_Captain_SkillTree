using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class Speed_Tooltip
    {
        public static string GetSpeedRootTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                int currentLevel = manager?.GetSkillLevel("speed_root") ?? 0;
                int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
                int displayLevel = Math.Min(currentLevel + 1, 7);
                float perLevel   = Speed_Config.SpeedRootMoveSpeedPerLevelValue;
                float currentBonus = mainLevel * perLevel;
                int pending = manager?.pendingInvestments.ContainsKey("speed_root") == true
                    ? manager.pendingInvestments["speed_root"] : 0;
                int pendingTargetLevel = currentLevel + pending;

                var t = $"<color=#FFD700><size=22>{L.Get("speed_root_name")}</size></color>\n";
                bool mainLinePending = pending > 0 && mainLevel > currentLevel && mainLevel <= pendingTargetLevel;
                string mainLineColor = mainLinePending ? "#FFFFFF" : "#E0E0E0";
                t += $"<color={mainLineColor}><size=16>Lv{mainLevel} : {L.Get("speed_root_stat_preview", currentBonus)}</size></color>\n";
                t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{Speed_Config.SpeedRootPointsPerLevelValue}</size></color>\n";

                if (currentLevel < 7)
                {
                    t += $"<color=#FFA500><size=16>{L.Get("speedroot_upgrade_requires", displayLevel)}: </size></color>";
                    t += $"<color=#FF6B6B><size=16>{GetLevelItemText(displayLevel)}</size></color>\n";
                    int reqLv = Speed_Config.GetSpeedRootRequiredPlayerLevel(displayLevel);
                    if (reqLv > 0)
                        t += $"<color=#FFD700><size=16>{L.Get("player_level_required_short", reqLv)}</size></color>\n";
                }
                else
                {
                    t += $"<color=#FFD700><size=16>{L.Get("speedroot_max_level")}</size></color>\n";
                }

                if (mainLevel < 7)
                {
                    t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 7; lv++)
                    {
                        float bonus = lv * perLevel;
                        string lineColor = (pending > 0 && lv <= pendingTargetLevel) ? "#FFFFFF" : "#808080";
                        t += $"<color={lineColor}><size=14>Lv{lv} : {L.Get("speed_root_stat_preview", bonus)}</size></color>\n";
                    }
                }

                return t.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 전문가 툴팁] 생성 실패: {ex.Message}");
                float perLevel = Speed_Config.SpeedRootMoveSpeedPerLevelValue;
                return $"<color=#FFD700><size=22>{L.Get("speed_root_name")}</size></color>\n" +
                       $"<color=#E0E0E0><size=16>Lv1 : {L.Get("speed_root_stat_preview", perLevel)}</size></color>";
            }
        }

        private static string GetLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_boar") + " x5";
                case 2: return L.Get("item_trophy_greydwarfshaman") + " x5";
                case 3: return L.Get("item_trophy_wraith") + " x5";
                case 4: return L.Get("item_trophy_fenring") + " x5";
                case 5: return L.Get("item_trophy_goblinbrute") + " x5";
                case 6: return L.Get("item_trophy_gjall") + " x5";
                case 7: return L.Get("item_trophy_fallenvalkyrie") + " x5";
                default: return "";
            }
        }
    }
}

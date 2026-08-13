using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class Attack_Tooltip
    {
        public static string GetFrenzyTriggerTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                int currentLevel   = manager?.GetSkillLevel("atk_frenzy_trigger") ?? 0;
                int mainLevel      = currentLevel == 0 ? 1 : currentLevel;
                float perLevel     = Attack_Config.AtkFrenzyTriggerCritChancePerLevelValue;
                int pointsPerLevel = Attack_Config.AtkFrenzyTriggerPointsPerLevelValue;
                float currentBonus = mainLevel * perLevel;
                int pending = manager?.pendingInvestments.ContainsKey("atk_frenzy_trigger") == true
                    ? manager.pendingInvestments["atk_frenzy_trigger"] : 0;
                int pendingTargetLevel = currentLevel + pending;

                var t = $"<color=#FFD700><size=22>{L.Get("atk_frenzy_trigger_name")}</size></color>\n";
                bool mainLinePending = pending > 0 && mainLevel > currentLevel && mainLevel <= pendingTargetLevel;
                string mainLineColor = mainLinePending ? "#FFFFFF" : "#E0E0E0";
                t += $"<color={mainLineColor}><size=16>Lv{mainLevel} : {L.Get("atk_frenzy_trigger_stat_preview", currentBonus)}</size></color>\n";

                if (currentLevel < 7)
                {
                    t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
                    t += $"<color=#FF6B6B><size=16>{pointsPerLevel}</size></color>\n";
                }
                else
                {
                    t += $"<color=#FFD700><size=16>{L.Get("atk_frenzy_trigger_max_level")}</size></color>\n";
                }

                if (mainLevel < 7)
                {
                    t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 7; lv++)
                    {
                        float bonus = lv * perLevel;
                        string lineColor = (pending > 0 && lv <= pendingTargetLevel) ? "#FFFFFF" : "#808080";
                        t += $"<color={lineColor}><size=14>Lv{lv} : {L.Get("atk_frenzy_trigger_stat_preview", bonus)} ({L.Get("tooltip_required_points")}: {pointsPerLevel})</size></color>\n";
                    }
                }

                return t.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[치명적인 공격 툴팁] 생성 실패: {ex.Message}");
                float perLevel = Attack_Config.AtkFrenzyTriggerCritChancePerLevelValue;
                return $"<color=#FFD700><size=22>{L.Get("atk_frenzy_trigger_name")}</size></color>\n" +
                       $"<color=#E0E0E0><size=16>Lv1 : {L.Get("atk_frenzy_trigger_stat_preview", perLevel)}</size></color>";
            }
        }

        public static string GetWeakPointAttackTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                int currentLevel   = manager?.GetSkillLevel("atk_crit_dmg") ?? 0;
                int mainLevel      = currentLevel == 0 ? 1 : currentLevel;
                int pointsPerLevel = Attack_Config.AtkCritDmgPointsPerLevelValue;
                float currentBonus = Attack_Config.GetWeakPointAttackBonus(mainLevel);
                int pending = manager?.pendingInvestments.ContainsKey("atk_crit_dmg") == true
                    ? manager.pendingInvestments["atk_crit_dmg"] : 0;
                int pendingTargetLevel = currentLevel + pending;

                var t = $"<color=#FFD700><size=22>{L.Get("atk_crit_dmg_name")}</size></color>\n";
                bool mainLinePending = pending > 0 && mainLevel > currentLevel && mainLevel <= pendingTargetLevel;
                string mainLineColor = mainLinePending ? "#FFFFFF" : "#E0E0E0";
                t += $"<color={mainLineColor}><size=16>Lv{mainLevel} : {L.Get("atk_crit_dmg_stat_preview", currentBonus)}</size></color>\n";

                if (currentLevel < 7)
                {
                    t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
                    t += $"<color=#FF6B6B><size=16>{pointsPerLevel}</size></color>\n";
                }
                else
                {
                    t += $"<color=#FFD700><size=16>{L.Get("atk_crit_dmg_max_level")}</size></color>\n";
                }

                if (mainLevel < 7)
                {
                    t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 7; lv++)
                    {
                        float bonus = Attack_Config.GetWeakPointAttackBonus(lv);
                        string lineColor = (pending > 0 && lv <= pendingTargetLevel) ? "#FFFFFF" : "#808080";
                        t += $"<color={lineColor}><size=14>Lv{lv} : {L.Get("atk_crit_dmg_stat_preview", bonus)} ({L.Get("tooltip_required_points")}: {pointsPerLevel})</size></color>\n";
                    }
                }

                return t.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[약점 공격 툴팁] 생성 실패: {ex.Message}");
                return $"<color=#FFD700><size=22>{L.Get("atk_crit_dmg_name")}</size></color>\n" +
                       $"<color=#E0E0E0><size=16>Lv1 : {L.Get("atk_crit_dmg_stat_preview", Attack_Config.GetWeakPointAttackBonus(1))}</size></color>";
            }
        }
    }
}

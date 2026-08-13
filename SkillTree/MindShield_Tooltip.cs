using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class MindShield_Tooltip
    {
        public struct MindShieldTooltipData
        {
            public string skillName;
            public string description;
            public string duration;
            public string eitrCost;
            public string skillType;
            public string cooldown;
            public string requirement;
            public string specialNote;
        }

        public static string GetMindShieldTooltip()
        {
            try
            {
                float duration = Defense_Config.MindShieldDurationValue;
                float eitrCost = Defense_Config.MindShieldEitrCostValue;
                float cooldown = Defense_Config.MindShieldCooldownValue;
                int requiredPoints = Defense_Config.DefenseStep6MindRequiredPointsValue;

                var data = new MindShieldTooltipData
                {
                    skillName = L.Get("defense_mind_name"),
                    description = L.Get("defense_mind_short_desc"),
                    duration = $"{(int)duration}{L.Get("unit_seconds")}",
                    eitrCost = $"{(int)eitrCost}",
                    skillType = L.Get("skill_type_active_key", SkillTreeConfig.GetHotKeyDisplayName(SkillTreeConfig.HotKeyG, "G")),
                    cooldown = $"{cooldown:F0}{L.Get("unit_seconds")}",
                    requirement = L.Get("requirement_staff_wand"),
                    specialNote = $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color>"
                };

                return GenerateMindShieldTooltip(data);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[마인드쉴드 툴팁] 생성 실패: {ex.Message}");
                return GetMindShieldFallbackTooltip();
            }
        }

        public static string GenerateMindShieldTooltip(MindShieldTooltipData data)
        {
            try
            {
                var tooltip = "";

                if (!string.IsNullOrEmpty(data.skillName))
                    tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";

                if (!string.IsNullOrEmpty(data.description))
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{data.description}</size></color>\n";

                if (!string.IsNullOrEmpty(data.duration))
                    tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_max_duration")}: </size></color><color=#B0E0E6><size=16>{data.duration}</size></color>\n";

                if (!string.IsNullOrEmpty(data.eitrCost))
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_eitr")} {data.eitrCost}</size></color>\n";

                if (!string.IsNullOrEmpty(data.skillType))
                    tooltip += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#FFD700><size=16>{data.skillType}</size></color>\n";

                if (!string.IsNullOrEmpty(data.cooldown))
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";

                if (!string.IsNullOrEmpty(data.requirement))
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";

                if (!string.IsNullOrEmpty(data.specialNote))
                    tooltip += $"\n{data.specialNote}";

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[마인드쉴드 툴팁] 구조화 툴팁 생성 실패: {ex.Message}");
                return GetMindShieldFallbackTooltip();
            }
        }

        public static string GetMindShieldFallbackTooltip()
        {
            var requiredPoints = Defense_Config.DefenseStep6MindRequiredPointsValue;
            return $"<color=#FFD700><size=22>{L.Get("defense_mind_name")}</size></color>\n\n" +
                   $"<color=#E0E0E0><size=16>{L.Get("defense_mind_short_desc")}\n\n" +
                   $"• {L.Get("tooltip_cost")}: {L.Get("stat_eitr")} 30\n" +
                   $"• {L.Get("tooltip_cooldown")}: 210{L.Get("unit_seconds")}\n" +
                   $"• {L.Get("tooltip_requirements")}: {L.Get("requirement_staff_wand")}\n\n" +
                   $"{L.Get("tooltip_skill_type")}: {L.Get("skill_type_active_key", SkillTreeConfig.GetHotKeyDisplayName(SkillTreeConfig.HotKeyG, "G"))}\n\n" +
                   $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color></size></color>";
        }
    }
}

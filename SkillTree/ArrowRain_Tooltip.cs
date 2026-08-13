using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class ArrowRain_Tooltip
    {
        public static string GetTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("bow_Step6_arrow_rain") ?? 0;
            int mainLevel = currentLevel == 0 ? 1 : currentLevel;
            int displayLevel = Math.Min(currentLevel + 1, 7);

            int count      = Bow_Config.ArrowRainArrowCountValue;
            float cooldown = Bow_Config.ArrowRainCooldownValue;
            float stamina  = Bow_Config.ArrowRainStaminaCostValue;
            float radius   = Bow_Config.ArrowRainRadiusValue;

            int mainDmg = GetDamageForLevel(mainLevel);

            var t = $"<color=#FFD700><size=22>{L.Get("bow_arrow_rain_name")}</size></color>\n";

            t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("bow_arrow_rain_damage_format", count, mainDmg)}</size></color>\n";

            t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            t += $"<color=#FFDAB9><size=16>{L.Get("stamina_percent_format", (int)stamina)}</size></color>\n";

            t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_h")}</size></color>\n";

            t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            t += $"<color=#FFDB58><size=16>{L.Get("seconds_format", cooldown)}</size></color>\n";

            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color>";
            t += $"<color=#B0E0E6><size=16>{L.Get("bow_arrow_rain_range_format", radius)}</size></color>\n";

            t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            t += $"<color=#00FF00><size=16>{L.Get("requirement_bow_equip")}</size></color>\n";

            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
            t += $"<color=#FF6B6B><size=16>{Bow_Config.ArrowRainRequiredPointsValue}</size></color>\n";

            t += $"<color=#FF69B4><size=16>{L.Get("tooltip_special")}: </size></color>";
            t += $"<color=#FFB6C1><size=16>{L.Get("bow_arrow_rain_dungeon_buff_desc", (int)Bow_Config.ArrowRainDungeonBuffDamageBonusValue, (int)Bow_Config.ArrowRainDungeonBuffDurationValue)}</size></color>\n";

            if (currentLevel < 7)
            {
                t += $"<color=#FFA500><size=16>{L.Get("arrow_rain_next_level_req", displayLevel)}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{GetLevelItemText(displayLevel)}</size></color>\n";
            }
            else
            {
                t += $"<color=#FFD700><size=16>{L.Get("arrow_rain_max_level")}</size></color>\n";
            }

            if (mainLevel < 7)
            {
                t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 7; lv++)
                {
                    int pvDmg = GetDamageForLevel(lv);
                    t += $"<color=#808080><size=14>Lv{lv} : {L.Get("bow_arrow_rain_damage_format", count, pvDmg)}</size></color>\n";
                }
            }

            return t.TrimEnd('\n');
        }

        private static int GetDamageForLevel(int level)
        {
            float baseD = Bow_Config.ArrowRainDamagePercentValue;
            float bonus = Bow_Config.ArrowRainLevelBonusValue;
            return (int)(baseD + (level - 1) * bonus);
        }

        private static string GetLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_troll") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_draugrelite") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_fenring") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_lox") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_seekerbrute") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_volture") + " x1";
                default: return "";
            }
        }
    }
}

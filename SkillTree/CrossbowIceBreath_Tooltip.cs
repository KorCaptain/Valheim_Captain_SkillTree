using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class CrossbowIceBreath_Tooltip
    {
        public static string GetTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("crossbow_ice_breath") ?? 0;
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
            int displayLevel = System.Math.Min(currentLevel + 1, 7);

            float stamina  = Crossbow_Config.CrossbowIceBreathStaminaCostValue;
            float cooldown = Crossbow_Config.CrossbowIceBreathCooldownValue;

            var t = $"<color=#FFD700><size=22>{L.Get("crossbow_ice_breath_name")}</size></color>\n";

            t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("icebreath_damage_preview", (int)GetFirstHitForLevel(mainLevel), (int)GetDotForLevel(mainLevel))}</size></color>\n";

            t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            t += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {(int)stamina}</size></color>\n";

            t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", SkillTreeConfig.HotKeyH?.Value ?? "H")}</size></color>\n";

            t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            t += $"<color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";

            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color>";
            t += $"<color=#B0E0E6><size=16>10m·±35°</size></color>\n";

            t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            t += $"<color=#00FF00><size=16>{L.Get("requirement_crossbow_bolt")}</size></color>\n";

            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
            t += $"<color=#FF6B6B><size=16>{Crossbow_Config.CrossbowIceBreathRequiredPointsValue}</size></color>\n";

            if (currentLevel < 7)
            {
                t += $"<color=#FFA500><size=16>{L.Get("icebreath_upgrade_requires", displayLevel)}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{GetLevelItemText(displayLevel)}</size></color>\n";
            }
            else
            {
                t += $"<color=#FFD700><size=16>{L.Get("icebreath_max_level")}</size></color>\n";
            }

            if (mainLevel < 7)
            {
                t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 7; lv++)
                {
                    t += $"<color=#808080><size=14>Lv{lv} : {L.Get("icebreath_damage_preview", (int)GetFirstHitForLevel(lv), (int)GetDotForLevel(lv))}</size></color>\n";
                }
            }

            return t.TrimEnd('\n');
        }

        private static float GetFirstHitForLevel(int level)
            => Crossbow_Config.CrossbowIceBreathFirstHitPctValue + (level - 1) * Crossbow_Config.CrossbowIceBreathLevelBonusValue;

        private static float GetDotForLevel(int level)
            => Crossbow_Config.CrossbowIceBreathDotPctValue + (level - 1) * Crossbow_Config.CrossbowIceBreathDotLevelBonusValue;

        private static string GetLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_greydwarfbrute") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_draugr") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_wolf") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_goblinbrute") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_tick") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_charredwarrior") + " x1";
                default: return "";
            }
        }
    }
}

using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class KnifeAssassinHeart_Tooltip
    {
        public static string GetTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("knife_step9_assassin_heart") ?? 0;
            int mainLevel = currentLevel == 0 ? 1 : currentLevel;
            int displayLevel = System.Math.Min(currentLevel + 1, 7);

            float cooldown = Knife_Config.KnifeAssassinHeartCooldownValue;
            float stamina  = Knife_Config.KnifeAssassinHeartStaminaCostValue;
            float range    = Knife_Config.KnifeAssassinHeartTeleportRangeValue;

            var t = $"<color=#FFD700><size=22>{L.Get("knife_skill_assassin")}</size></color>\n";

            t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("assassin_heart_stats_preview", GetAttackCountForLevel(mainLevel), $"{GetCritMultiplierForLevel(mainLevel):F1}")}</size></color>\n";

            t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            t += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {(int)stamina}</size></color>\n";

            t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", SkillTreeConfig.HotKeyG?.Value ?? "G")}</size></color>\n";

            t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            t += $"<color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";

            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color>";
            t += $"<color=#B0E0E6><size=16>{range}m</size></color>\n";

            t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            t += $"<color=#00FF00><size=16>{L.Get("requirement_knife_claw")}</size></color>\n";

            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
            t += $"<color=#FF6B6B><size=16>{Knife_Config.KnifeAssassinHeartRequiredPointsValue}</size></color>\n";

            if (currentLevel < 7)
            {
                t += $"<color=#FFA500><size=16>{L.Get("assassin_heart_upgrade_requires", displayLevel)}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{GetLevelItemText(displayLevel)}</size></color>\n";
            }
            else
            {
                t += $"<color=#FFD700><size=16>{L.Get("assassin_heart_max_level")}</size></color>\n";
            }

            if (mainLevel < 7)
            {
                t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 7; lv++)
                {
                    t += $"<color=#808080><size=14>Lv{lv} : {L.Get("assassin_heart_stats_preview", GetAttackCountForLevel(lv), $"{GetCritMultiplierForLevel(lv):F1}")}</size></color>\n";
                }
            }

            return t.TrimEnd('\n');
        }

        public static int GetAttackCountForLevel(int level) => level switch
        {
            3 => 4, 4 => 4,
            5 => 5, 6 => 5,
            7 => 6,
            _ => 3   // Lv1, Lv2
        };

        public static float GetCritMultiplierForLevel(int level)
            => Knife_Config.KnifeAssassinHeartCritDamageValue + (level - 1) * Knife_Config.KnifeAssassinHeartLevelBonusValue;

        private static string GetLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_troll") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_abomination") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_sgolem") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_goblinshaman") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_gjall") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_fallenvalkyrie") + " x1";
                default: return "";
            }
        }
    }
}

using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class CrossbowOneShot_Tooltip
    {
        public static string GetTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("crossbow_Step6_expert") ?? 0;
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
            int displayLevel = Math.Min(currentLevel + 1, 7);

            float effectiveDamage = Crossbow_Config.CrossbowOneShotDamageBonusValue
                + (currentLevel > 0 ? (currentLevel - 1) * Crossbow_Config.CrossbowOneShotLevelBonusValue : 0);
            float cooldown  = Crossbow_Config.CrossbowOneShotCooldownValue;
            float duration  = Crossbow_Config.CrossbowOneShotDurationValue;
            float aoeRadius = Crossbow_Config.CrossbowOneShotAoeRadiusValue;
            float slowPct   = (Crossbow_Config.CrossbowOneShotSlowReloadMultiplierValue - 1f) * 100f;

            // 1. 스킬명
            var t = $"<color=#FFD700><size=22>{L.Get("crossbow_oneshot_name")}</size></color>\n";

            // 2. Lv + 핵심 효과
            t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : +{(int)effectiveDamage}% {L.Get("tooltip_damage")}, AOE {aoeRadius}m, {(int)duration}{L.Get("unit_seconds")}</size></color>\n";

            // 3. 패시브 (슬로우 리로드)
            t += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color>";
            t += $"<color=#ADFF2F><size=16>{L.Get("tooltip_reload_penalty", (int)slowPct)}</size></color>\n";

            // 4. 소모
            t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            t += $"<color=#FFDAB9><size=16>{L.Get("stamina_format", 20)}</size></color>\n";

            // 5. 스킬유형
            t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", SkillTreeConfig.HotKeyR?.Value ?? "Z")}</size></color>\n";

            // 6. 쿨타임
            t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            t += $"<color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";

            // 7. 필요조건
            t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            t += $"<color=#00FF00><size=16>{L.Get("requirement_crossbow_equip")}</size></color>\n";

            // 8. 필요포인트
            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
            t += $"<color=#FF6B6B><size=16>{Crossbow_Config.CrossbowOneShotRequiredPointsValue}</size></color>\n";

            // 9. 필요 아이템 (다음 레벨) or 최대레벨
            if (currentLevel < 7)
            {
                t += $"<color=#FFA500><size=16>{L.Get("oneshot_next_level_req", displayLevel)}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{GetLevelItemText(displayLevel)}</size></color>\n";
            }
            else
            {
                t += $"<color=#FFD700><size=16>{L.Get("oneshot_max_level")}</size></color>\n";
            }

            // 10. 구분선 + 프리뷰
            if (mainLevel < 7)
            {
                t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 7; lv++)
                {
                    float lvDamage = Crossbow_Config.CrossbowOneShotDamageBonusValue
                        + (lv - 1) * Crossbow_Config.CrossbowOneShotLevelBonusValue;
                    t += $"<color=#808080><size=14>Lv{lv} : +{(int)lvDamage}%, AOE {aoeRadius}m</size></color>\n";
                }
            }

            return t.TrimEnd('\n');
        }

        private static string GetLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_greydwarfshaman") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_surtling") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_hatchling") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_goblin") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_seeker") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_morgen") + " x1";
                default: return "";
            }
        }
    }
}

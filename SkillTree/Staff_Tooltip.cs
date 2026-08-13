using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class Staff_Tooltip
    {
        public static string GetDualCastTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                int currentLevel = manager?.GetSkillLevel("staff_Step6_dual_cast") ?? 0;
                int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
                int displayLevel = Math.Min(currentLevel + 1, 7);

                float eitrCost = Staff_Config.StaffDoubleCastEitrCostValue;
                float cooldown = Staff_Config.StaffDoubleCastCooldownValue;

                int mainDmg = GetDamageForLevel(mainLevel);

                var t = $"<color=#FFD700><size=22>{L.Get("staff_skill_dual_cast")}</size></color>\n";

                // 현재 레벨 스탯
                t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("dual_cast_damage_preview", mainDmg)}</size></color>\n";

                // 소모
                t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
                t += $"<color=#FFDAB9><size=16>{L.Get("stat_eitr")} {(int)eitrCost}</size></color>\n";

                // 스킬 유형
                t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
                t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", SkillTreeConfig.GetHotKeyDisplayName(SkillTreeConfig.HotKeyR, "Z"))}</size></color>\n";

                // 쿨타임
                t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
                t += $"<color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";

                // 필요조건
                t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
                t += $"<color=#00FF00><size=16>{L.Get("requirement_staff_wand")}</size></color>\n";

                t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{Staff_Config.StaffDoubleCastRequiredPointsValue}</size></color>\n";

                // 강화 필요 아이템 or 최대레벨
                if (currentLevel < 7)
                {
                    t += $"<color=#FFA500><size=16>{L.Get("dual_cast_upgrade_requires", displayLevel)}: </size></color>";
                    t += $"<color=#FF6B6B><size=16>{GetLevelItemText(displayLevel)}</size></color>\n";
                }
                else
                {
                    t += $"<color=#FFD700><size=16>{L.Get("dual_cast_max_level")}</size></color>\n";
                }

                // 구분선 + 레벨 프리뷰
                if (mainLevel < 7)
                {
                    t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 7; lv++)
                    {
                        int pvDmg = GetDamageForLevel(lv);
                        t += $"<color=#808080><size=14>Lv{lv} : {L.Get("dual_cast_damage_preview", pvDmg)}</size></color>\n";
                    }
                }

                return t.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중시전 툴팁] 생성 실패: {ex.Message}");
                return GetDualCastFallbackTooltip();
            }
        }

        private static int GetDamageForLevel(int level)
        {
            float baseD = Staff_Config.StaffDoubleCastDamagePercentValue;
            float bonus = Staff_Config.StaffDoubleCastLevelBonusValue;
            return (int)(baseD + (level - 1) * bonus);
        }

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

        public static string GetDualCastFallbackTooltip()
        {
            int lv1Dmg = GetDamageForLevel(1);
            float eitr    = Staff_Config.StaffDoubleCastEitrCostValue;
            float cooldown = Staff_Config.StaffDoubleCastCooldownValue;
            return $"<color=#FFD700><size=22>{L.Get("staff_skill_dual_cast")}</size></color>\n" +
                   $"<color=#E0E0E0><size=16>Lv1 : {L.Get("dual_cast_damage_preview", lv1Dmg)}</size></color>\n" +
                   $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>" +
                   $"<color=#FFDAB9><size=16>{L.Get("stat_eitr")} {(int)eitr}</size></color>\n" +
                   $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>" +
                   $"<color=#FFDB58><size=16>{(int)cooldown}{L.Get("unit_seconds")}</size></color>\n" +
                   $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>" +
                   $"<color=#00FF00><size=16>{L.Get("requirement_staff_wand")}</size></color>";
        }

        public static void LogCurrentTooltipConfig()
        {
            try
            {
                Plugin.Log.LogInfo("[Staff_Tooltip] 현재 설정값:");
                Plugin.Log.LogInfo($"  - 기본 데미지: {Staff_Config.StaffDoubleCastDamagePercentValue}%");
                Plugin.Log.LogInfo($"  - 레벨 보너스: {Staff_Config.StaffDoubleCastLevelBonusValue}%/레벨");
                Plugin.Log.LogInfo($"  - 에이트르 소모: {Staff_Config.StaffDoubleCastEitrCostValue}");
                Plugin.Log.LogInfo($"  - 쿨타임: {Staff_Config.StaffDoubleCastCooldownValue}초");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Staff_Tooltip] 로그 출력 실패: {ex.Message}");
            }
        }
    }
}

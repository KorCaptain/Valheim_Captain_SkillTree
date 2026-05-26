using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 로그 직업 전용 툴팁 시스템 (Lv1~5)
    /// 아처 패턴 적용 - 현재 레벨 메인 블록 + 프리뷰 블록
    /// </summary>
    public static class Rogue_Tooltip
    {
        /// <summary>
        /// 로그 상세 툴팁 생성 (레벨별 동적 표시)
        /// </summary>
        public static string GetRogueTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                int currentLevel = manager?.GetSkillLevel("Rogue") ?? 0;
                int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
                int displayLevel = Math.Min(currentLevel + 1, 5);

                int stamina    = (int)Rogue_Config.RogueShadowStrikeStaminaCostValue;
                float cooldown = RogueSkills.GetCooldownForLevel(mainLevel);

                var tooltip = $"<color=#FFD700><size=22>{L.Get("job_rogue")}</size></color>\n";

                // 메인 블록: 현재 레벨 스탯
                float atk    = RogueSkills.GetAttackBonusForLevel(mainLevel);
                float dur    = RogueSkills.GetBuffDurationForLevel(mainLevel);
                int   blasts = RogueSkills.GetPoisonBlastsForLevel(mainLevel);

                tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("rogue_effect_strike", (int)atk, (int)dur, blasts)}</size></color>\n";

                float instant = RogueSkills.GetPoisonInstantForLevel(mainLevel);
                float dot = RogueSkills.GetPoisonDotForLevel(mainLevel);
                float range = Rogue_Config.RoguePoisonRangeValue;
                tooltip += $"<color=#90EE90><size=16>{L.Get("rogue_poison_info", (int)range, (int)instant, (int)dot)}</size></color>\n";

                // 패시브
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color>";
                tooltip += $"<color=#ADFF2F><size=16>{GetPassiveStr(mainLevel)}</size></color>\n";

                // 소모
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
                tooltip += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {stamina}</size></color>\n";

                // 스킬 유형
                tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
                tooltip += $"<color=#ADFF2F><size=16>{L.Get("skill_type_job_active", "Y")}</size></color>\n";

                // 쿨다운
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
                tooltip += $"<color=#FFDB58><size=16>{cooldown:F0}{L.Get("unit_seconds")}</size></color>\n";

                // 필요조건
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
                tooltip += $"<color=#00FF00><size=16>{L.Get("requirement_rogue")}</size></color>\n";

                // 확인사항
                tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>";
                tooltip += $"<color=#FFE4B5><size=16>{L.Get("confirmation_job_only")}</size></color>\n";

                // 필요아이템 또는 최대레벨
                if (currentLevel < 5)
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("rogue_level_req_items", displayLevel)}: </size></color>";
                    tooltip += $"<color=#FF6B6B><size=16>{RogueSkills.GetRogueLevelCostText(displayLevel)}</size></color>\n";
                }
                else
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("rogue_max_level")}</size></color>\n";
                }

                // 프리뷰 블록 (현재 레벨 < 5)
                if (mainLevel < 5)
                {
                    tooltip += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 5; lv++)
                    {
                        float pvAtk      = RogueSkills.GetAttackBonusForLevel(lv);
                        float pvDur      = RogueSkills.GetBuffDurationForLevel(lv);
                        int   pvBlasts   = RogueSkills.GetPoisonBlastsForLevel(lv);
                        float pvInstant  = RogueSkills.GetPoisonInstantForLevel(lv);
                        float pvDot      = RogueSkills.GetPoisonDotForLevel(lv);
                        float pvCooldown = RogueSkills.GetCooldownForLevel(lv);
                        float prevAtk      = RogueSkills.GetAttackBonusForLevel(lv - 1);
                        int   prevBlasts   = RogueSkills.GetPoisonBlastsForLevel(lv - 1);
                        float prevInstant  = RogueSkills.GetPoisonInstantForLevel(lv - 1);
                        float prevDot      = RogueSkills.GetPoisonDotForLevel(lv - 1);

                        string activePreview = (lv == 5 && RogueSkills.GetChargesForLevel(lv) > RogueSkills.GetChargesForLevel(lv - 1))
                            ? L.Get("rogue_preview_charges", 1)
                            : L.Get("rogue_preview_attack", (int)(pvAtk - prevAtk), pvBlasts - prevBlasts);

                        tooltip += $"<color=#808080><size=14>Lv{lv} : {activePreview}\n";

                        float instantDelta = pvInstant - prevInstant;
                        float dotDelta = pvDot - prevDot;
                        if (instantDelta > 0 || dotDelta > 0)
                            tooltip += $"  {L.Get("rogue_preview_poison", (int)instantDelta, (int)dotDelta)}\n";

                        float lv1Cooldown = RogueSkills.GetCooldownForLevel(1);
                        float cdReductionFromLv1 = lv1Cooldown - pvCooldown;
                        if (cdReductionFromLv1 > 0.01f)
                            tooltip += $"  {L.Get("rogue_preview_cd", (int)Math.Round(cdReductionFromLv1), (int)pvCooldown)}\n";

                        tooltip += $"  {L.Get("tooltip_passive")}: {GetPassiveStr(lv)}</size></color>\n";
                    }
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 툴팁] 생성 실패: {ex.Message}");
                return GetRogueFallbackTooltip();
            }
        }

        /// <summary>
        /// 레벨별 패시브 설명 문자열
        /// </summary>
        private static string GetPassiveStr(int lv)
        {
            float atkSpd  = RogueSkills.GetAttackSpeedForLevel(lv);
            float stamRed = RogueSkills.GetStaminaReductionForLevel(lv);
            float dodge   = RogueSkills.GetRogueDodgeChance(lv);
            float moveSpd = RogueSkills.GetMoveSpeedForLevel(lv);

            return lv switch
            {
                1 => L.Get("rogue_passive_lv1", (int)atkSpd, (int)stamRed, (int)dodge, (int)moveSpd),
                2 => L.Get("rogue_passive_lv2", (int)atkSpd, (int)stamRed, (int)dodge, (int)moveSpd),
                3 => L.Get("rogue_passive_lv3", (int)atkSpd, (int)stamRed, (int)dodge, (int)moveSpd),
                4 => L.Get("rogue_passive_lv4", (int)atkSpd, (int)stamRed, (int)dodge, (int)moveSpd),
                _ => L.Get("rogue_passive_lv5", (int)atkSpd, (int)stamRed, (int)dodge, (int)moveSpd)
            };
        }

        /// <summary>
        /// 폴백 툴팁 (오류 시 사용)
        /// </summary>
        private static string GetRogueFallbackTooltip()
        {
            return $"<color=#FFD700><size=22>{L.Get("job_rogue")}</size></color>\n" +
                   $"<color=#E0E0E0><size=16>Lv1 : {L.Get("rogue_effect_strike", 30, 8, 6)}</size></color>\n" +
                   $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color>" +
                   $"<color=#ADFF2F><size=16>{L.Get("rogue_passive_lv1", 7, 15, 4, 5)}</size></color>";
        }

        /// <summary>
        /// 로그 툴팁 강제 업데이트 (컨피그 변경 시 호출)
        /// </summary>
        public static void UpdateRogueTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Rogue"))
                {
                    string newTooltip = GetRogueTooltip();
                    var skillNode = manager.SkillNodes["Rogue"];
                    skillNode.Description = newTooltip;
                    Plugin.Log.LogDebug("[로그 툴팁] 업데이트 완료");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
    }
}

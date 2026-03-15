using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 아처 직업 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Archer_Tooltip
    {
        /// <summary>
        /// 아처 툴팁 데이터 구조체
        /// </summary>
        public struct ArcherTooltipData
        {
            public string skillName;          // 스킬명 (예: "멀티샷")
            public string description;        // 설명 (예: "5발씩 2회 발사합니다.")
            public string additionalInfo;     // 추가 정보 (예: "화살 1발은 활+화살 공격력의 50%")
            public string range;             // 범위 (아처는 "없음")
            public string consumeStamina;    // 스태미나 소모 (예: "25")
            public string consumeArrow;      // 화살 소모 (예: "1")
            public string skillType;         // 스킬 유형 (예: "액티브 버프 스킬 - Y키")
            public string cooldown;          // 쿨타임 (예: "30초")
            public string requirement;       // 필요조건 (예: "활 착용")
            public string confirmation;      // 확인사항 (예: "직업은 1개만 선택가능, Lv 10 이상")
            public string requiredItem;      // 필요 아이템 (예: "에이크쉬르 트로피")
            public string passiveSkills;     // 패시브 스킬 (예: "점프 높이 +20%, 낙사 데미지 -50%")
        }

        /// <summary>
        /// 아처 상세 툴팁 생성 - 현재 레벨 스탯을 메인 블록으로 표시
        /// </summary>
        public static string GetArcherTooltip()
        {
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("Archer") ?? 0;
            int displayLevel = Math.Min(currentLevel + 1, 5); // 필요 아이템용 (다음 레벨)
            int mainLevel = currentLevel == 0 ? 1 : currentLevel; // 메인 블록 (현재 레벨, 미획득 시 Lv1)

            int baseArrows  = Archer_Config.ArcherMultiShotArrowCountValue;
            int baseCharges = Archer_Config.ArcherMultiShotChargesValue;
            int baseDamage  = (int)Archer_Config.ArcherMultiShotDamagePercentValue;
            int stamina     = (int)Archer_Config.ArcherMultiShotStaminaCostValue;
            float cooldown  = Archer_Config.ArcherMultiShotCooldownValue;
            float resistVal = Archer_Config.ArcherElementalResistPerLevelValue;

            int arrows  = GetArrowsForLevel(mainLevel, baseArrows);
            int charges = GetChargesForLevel(mainLevel, baseCharges);
            int damage  = GetDamageForLevel(mainLevel, baseDamage);

            var tooltip = $"<color=#FFD700><size=22>{L.Get("job_archer")}</size></color>\n";

            // 메인 블록: mainLevel 기준 스탯 (현재 레벨)
            tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("archer_effect_arrows", arrows, charges, damage)}</size></color>\n";
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{GetPassiveStr(mainLevel, resistVal)}</size></color>\n";
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            tooltip += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {stamina}, {L.Get("stat_arrow")} 1{L.Get("unit_pieces")}</size></color>\n";
            tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            tooltip += $"<color=#ADFF2F><size=16>{L.Get("skill_type_active_key", "Y")}</size></color>\n";
            tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            tooltip += $"<color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            tooltip += $"<color=#00FF00><size=16>{L.Get("archer_job_requirement")}</size></color>\n";
            tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>";
            tooltip += $"<color=#FFE4B5><size=16>{L.Get("confirmation_job_only")}</size></color>\n";

            // 필요아이템 or 최대레벨 메시지 (displayLevel 기준 - 다음 레벨 아이템)
            if (currentLevel < 5)
            {
                tooltip += $"<color=#FFA500><size=16>{L.Get("archer_level_req_items", displayLevel)}: </size></color>";
                tooltip += $"<color=#FF6B6B><size=16>{GetArcherLevelCostText(displayLevel)}</size></color>\n";
            }
            else
            {
                tooltip += $"<color=#FFD700><size=16>{L.Get("archer_max_level")}</size></color>\n";
            }

            // 구분선 + 프리뷰 (mainLevel < 5인 경우)
            if (mainLevel < 5)
            {
                tooltip += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 5; lv++)
                {
                    int pvArrows    = GetArrowsForLevel(lv, baseArrows);
                    int pvCharges   = GetChargesForLevel(lv, baseCharges);
                    int pvDamage    = GetDamageForLevel(lv, baseDamage);
                    int prevArrows  = GetArrowsForLevel(lv - 1, baseArrows);
                    int prevCharges = GetChargesForLevel(lv - 1, baseCharges);

                    string activePreview = pvCharges > prevCharges
                        ? L.Get("archer_preview_charges", pvCharges - prevCharges)
                        : L.Get("archer_preview_arrows_damage", pvArrows - prevArrows, pvDamage);

                    tooltip += $"<color=#808080><size=14>Lv{lv} : {activePreview}\n";
                    tooltip += $"  {L.Get("tooltip_passive")}: {GetPassiveStr(lv, resistVal)}</size></color>\n";
                }
            }

            return tooltip.TrimEnd('\n');
        }

        private static int GetArrowsForLevel(int level, int baseArrows)
        {
            return level switch {
                2 => baseArrows + Archer_Config.ArcherLv2BonusArrowsValue,
                3 => baseArrows + Archer_Config.ArcherLv3BonusArrowsValue,
                4 => baseArrows + Archer_Config.ArcherLv4BonusArrowsValue,
                5 => baseArrows + Archer_Config.ArcherLv5BonusArrowsValue,
                _ => baseArrows
            };
        }

        private static int GetChargesForLevel(int level, int baseCharges)
        {
            return level >= 5 ? baseCharges + Archer_Config.ArcherLv5BonusChargesValue : baseCharges;
        }

        private static int GetDamageForLevel(int level, int baseDamage)
        {
            return level switch {
                2 => (int)Archer_Config.ArcherLv2DamagePercentValue,
                3 => (int)Archer_Config.ArcherLv3DamagePercentValue,
                4 => (int)Archer_Config.ArcherLv4DamagePercentValue,
                5 => (int)Archer_Config.ArcherLv5DamagePercentValue,
                _ => baseDamage
            };
        }

        private static string GetPassiveStr(int level, float resistVal)
        {
            switch (level)
            {
                case 1:  return L.Get("archer_passive_lv1", (int)Archer_Config.ArcherJumpHeightBonusValue, (int)Archer_Config.ArcherFallDamageReductionValue);
                case 2:  return L.Get("archer_passive_lv2", (int)Archer_Config.ArcherLv2JumpHeightBonusValue, (int)resistVal);
                case 3:  return L.Get("archer_passive_lv3", (int)Archer_Config.ArcherLv3JumpHeightBonusValue, (int)Archer_Config.ArcherLv3FallDamageReductionValue, (int)resistVal);
                case 4:  return L.Get("archer_passive_lv4", (int)Archer_Config.ArcherLv4JumpHeightBonusValue, (int)Archer_Config.ArcherLv4FallDamageReductionValue, (int)resistVal);
                default: return L.Get("archer_passive_lv5", (int)Archer_Config.ArcherLv5JumpHeightBonusValue, (int)Archer_Config.ArcherLv5FallDamageReductionValue, (int)resistVal);
            }
        }

        private static string GetArcherLevelCostText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_greydwarfbrute") + " x1 + " + L.Get("item_eikthyr_trophy") + " x1";
                case 2: return L.Get("item_eikthyr_trophy") + " x1 + " + L.Get("item_trophy_theelder") + " x1";
                case 3: return L.Get("item_trophy_hatchling") + " x1 + " + L.Get("item_trophy_theelder") + " x1 + " + L.Get("item_trophy_bonemass") + " x1";
                case 4: return L.Get("item_trophy_abomination") + " x1 + " + L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_dragonqueen") + " x1";
                case 5: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_seekerqueen") + " x1";
                default: return "";
            }
        }

        /// <summary>
        /// 아처 툴팁 생성 (새로운 형식 - 구조화된 표시)
        /// </summary>
        public static string GenerateArcherTooltip(ArcherTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 스킬 이름 (황금색, 크기 22 - 기존 툴팁과 동일)
                tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";

                // 설명 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{data.description}";

                    // 추가 정보가 있으면 괄호로 추가
                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $"( {data.additionalInfo} )";
                    }
                    tooltip += "</size></color>\n";
                }

                // 범위 섹션 (기존 툴팁 스타일)
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{data.range}</size></color>\n";

                // 소모 섹션 - 구조화된 표시 (기존 툴팁 스타일)
                var consumeParts = new List<string>();
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    consumeParts.Add($"{L.Get("stat_stamina")} {data.consumeStamina}");
                }
                if (!string.IsNullOrEmpty(data.consumeArrow))
                {
                    consumeParts.Add($"{L.Get("stat_arrow")} {data.consumeArrow} {L.Get("unit_pieces")}");
                }

                if (consumeParts.Count > 0)
                {
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{string.Join(", ", consumeParts)}</size></color>\n";
                }

                // 스킬 유형 섹션 (Y키 강조: #1E90FF / #ADFF2F)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#ADFF2F><size=16>{data.skillType}</size></color>\n";
                }

                // 패시브 스킬 섹션 (새로 추가)
                if (!string.IsNullOrEmpty(data.passiveSkills))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color><color=#ADFF2F><size=16>{data.passiveSkills}</size></color>\n";
                }

                // 쿨타임 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 필요 아이템 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.requiredItem))
                {
                    tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_item")}: </size></color><color=#FF6B6B><size=16>{data.requiredItem}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception)
            {
                return L.Get("tooltip_generation_error");
            }
        }

        /// <summary>
        /// 아처 대체 툴팁 (설정 로드 실패 시)
        /// </summary>
        private static string GetArcherFallbackTooltip()
        {
            var tooltipData = new ArcherTooltipData
            {
                skillName = L.Get("archer_skill_multishot"),
                description = L.Get("archer_desc_multishot_fallback"),
                additionalInfo = L.Get("archer_desc_arrow_damage_fallback"),
                range = L.Get("tooltip_none"),
                consumeStamina = "25",
                consumeArrow = "1",
                skillType = L.Get("skill_type_active_key", "Y"),
                cooldown = $"30{L.Get("unit_seconds")}",
                requirement = L.Get("requirement_bow_equip"),
                confirmation = L.Get("confirmation_job_only"),
                requiredItem = L.Get("item_eikthyr_trophy"),
                passiveSkills = L.Get("archer_passive_skills", 20, 50)
            };

            return GenerateArcherTooltip(tooltipData);
        }

        /// <summary>
        /// 아처 툴팁 강제 업데이트
        /// </summary>
        public static void UpdateArcherTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Archer"))
                {
                    string newTooltip = GetArcherTooltip();
                    
                    // 기존 Description을 완전히 대체
                    var skillNode = manager.SkillNodes["Archer"];
                    var oldDescription = skillNode.Description;
                    skillNode.Description = newTooltip;
                    
                    // Plugin.Log.LogDebug($"[아처 툴팁] 업데이트 완료");
                    // Plugin.Log.LogDebug($"[아처 툴팁] 이전: {oldDescription?.Substring(0, Math.Min(50, oldDescription?.Length ?? 0))}...");
                    // Plugin.Log.LogDebug($"[아처 툴팁] 새로운: {newTooltip?.Substring(0, Math.Min(50, newTooltip?.Length ?? 0))}...");
                }
                else
                {
                    // Plugin.Log.LogWarning($"[아처 툴팁] Archer 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[아처 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
    }
}
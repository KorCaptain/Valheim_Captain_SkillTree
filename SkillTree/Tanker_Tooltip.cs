using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 탱커 직업 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// 아처 시스템과 동일한 구조로 구현 (통합 버전 - 중복 제거)
    /// </summary>
    public static class Tanker_Tooltip
    {
        /// <summary>
        /// 탱커 툴팁 데이터 구조체
        /// </summary>
        public struct TankerTooltipData
        {
            public string skillName;          // 스킬명 (예: "전장의 함성")
            public string description;        // 설명 (예: "주변 몬스터를 도발하고 자신의 피해를 감소시킵니다.")
            public string additionalInfo;     // 추가 정보 (예: "보스는 지속시간 단축")
            public string range;             // 범위 (예: "12m")
            public string consumeStamina;    // 스태미나 소모 (예: "25")
            public string consumeArrow;      // 화살 소모 (탱커는 "없음")
            public string skillType;         // 스킬 유형 (예: "액티브 버프 스킬 - Y키")
            public string cooldown;          // 쿨타임 (예: "60초")
            public string requirement;       // 필요조건 (예: "방패 착용")
            public string confirmation;      // 확인사항 (예: "직업은 1개만 선택가능, Lv 10 이상")
            public string requiredItem;      // 필요 아이템 (예: "옛 골렘 핵")
        }

        /// <summary>
        /// 탱커 상세 툴팁 생성 (액티브 + 패시브 효과 포함)
        /// </summary>
        public static string GetTankerTooltip()
        {
            try
            {
                // 컨피그에서 실제 값 가져오기
                var duration = Tanker_Config.TankerTauntDurationValue;
                var bossDuration = Tanker_Config.TankerTauntBossDurationValue;
                var activeDamageReduction = Tanker_Config.TankerTauntDamageReductionValue;
                var buffDuration = Tanker_Config.TankerTauntBuffDurationValue;
                var passiveDamageReduction = Tanker_Config.TankerPassiveDamageReductionValue;
                var range = Tanker_Config.TankerTauntRangeValue;
                var stamina = Tanker_Config.TankerTauntStaminaCostValue;
                var cooldown = Tanker_Config.TankerTauntCooldownValue;

                // HTML 스타일 구조화된 툴팁 생성
                var tooltip = $"<color=#FFD700><size=22>{L.Get("job_tanker")} - {L.Get("tanker_skill_warcry")}</size></color>\n\n";

                // 액티브 스킬 설명
                tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>";
                tooltip += L.Get("tanker_desc_warcry", range, duration, bossDuration, buffDuration, activeDamageReduction);
                tooltip += "</size></color>\n";

                // 범위 정보
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{range}m</size></color>\n";

                // 소모 자원
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {stamina}</size></color>\n";

                // 스킬 유형 (Y키 강조)
                tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#ADFF2F><size=16>{L.Get("tanker_skill_type_taunt")}</size></color>\n";

                // 쿨타임
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";

                // 패시브 효과 추가
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive_effect")}: </size></color><color=#00FF00><size=16>{L.Get("tanker_passive_damage_reduction", passiveDamageReduction)}</size></color>\n";

                // 필요조건
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{L.Get("requirement_shield_equip")}</size></color>\n";

                // 확인사항
                tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{L.Get("confirmation_job_only")}</size></color>\n";

                // 필요포인트
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{L.Get("item_eikthyr_trophy")}</size></color>";

                return tooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[탱커 툴팁] GetTankerTooltip 실패: {ex.Message}");
                return L.Get("tooltip_load_error");
            }
        }

        /// <summary>
        /// 탱커 툴팁 생성 (아처와 동일한 형식 - 구조화된 표시)
        /// </summary>
        public static string GenerateTankerTooltip(TankerTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 스킬 이름 (황금색, 크기 22)
                tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";

                // 설명 섹션
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{data.description}";

                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $"( {data.additionalInfo} )";
                    }
                    tooltip += "</size></color>\n";
                }

                // 범위 섹션
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{data.range}</size></color>\n";

                // 소모 섹션
                var consumeParts = new List<string>();
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    consumeParts.Add($"{L.Get("stat_stamina")} {data.consumeStamina}");
                }
                if (!string.IsNullOrEmpty(data.consumeArrow) && data.consumeArrow != L.Get("tooltip_none"))
                {
                    consumeParts.Add($"{L.Get("stat_arrow")} {data.consumeArrow} {L.Get("unit_pieces")}");
                }

                if (consumeParts.Count > 0)
                {
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{string.Join(", ", consumeParts)}</size></color>\n";
                }

                // 스킬 유형 섹션
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#ADFF2F><size=16>{data.skillType}</size></color>\n";
                }

                // 쿨타임 섹션
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 필요포인트 섹션
                if (!string.IsNullOrEmpty(data.requiredItem))
                {
                    tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{data.requiredItem}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception)
            {
                return L.Get("tooltip_generation_error");
            }
        }

        /// <summary>
        /// 탱커 대체 툴팁 (설정 로드 실패 시)
        /// </summary>
        private static string GetTankerFallbackTooltip()
        {
            var tooltipData = new TankerTooltipData
            {
                skillName = $"{L.Get("job_tanker")} - {L.Get("tanker_skill_warcry")}",
                description = L.Get("tanker_desc_warcry_fallback"),
                additionalInfo = "",
                range = "12m",
                consumeStamina = "25",
                consumeArrow = L.Get("tooltip_none"),
                skillType = L.Get("skill_type_active_key", "Y"),
                cooldown = $"60{L.Get("unit_seconds")}",
                requirement = L.Get("requirement_shield_equip"),
                confirmation = L.Get("confirmation_job_only"),
                requiredItem = L.Get("item_eikthyr_trophy")
            };

            return GenerateTankerTooltip(tooltipData);
        }

        /// <summary>
        /// 탱커 툴팁 강제 업데이트
        /// </summary>
        public static void UpdateTankerTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Tanker"))
                {
                    string newTooltip = GetTankerTooltip();
                    
                    // 기존 Description을 완전히 대체
                    var skillNode = manager.SkillNodes["Tanker"];
                    var oldDescription = skillNode.Description;
                    skillNode.Description = newTooltip;
                    
                    // Plugin.Log.LogDebug($"[탱커 툴팁] 업데이트 완료");
                    // Plugin.Log.LogDebug($"[탱커 툴팁] 이전: {oldDescription?.Substring(0, Math.Min(50, oldDescription?.Length ?? 0))}...");
                    // Plugin.Log.LogDebug($"[탱커 툴팁] 새로운: {newTooltip?.Substring(0, Math.Min(50, newTooltip?.Length ?? 0))}...");
                }
                else
                {
                    // Plugin.Log.LogWarning($"[탱커 툴팁] Tanker 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[탱커 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
    }
}
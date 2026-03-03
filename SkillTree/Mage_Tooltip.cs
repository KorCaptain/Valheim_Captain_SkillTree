using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 직업 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Mage_Tooltip
    {
        /// <summary>
        /// 메이지 툴팁 데이터 구조체
        /// </summary>
        public struct MageTooltipData
        {
            public string skillName;          // 스킬명 (예: "초월적 마법 폭발")
            public string description;        // 설명 (예: "12m 내의 모든 몬스터 vfx_HealthUpgrade 이팩트를 사용하고 3초뒤 공격 데미지 150%적용")
            public string additionalInfo;     // 추가 정보
            public string range;             // 범위 (예: "12m")
            public string consumeEitr;       // Eitr 소모 (예: "35")
            public string skillType;         // 스킬 유형 (예: "액티브 스킬(Y)")
            public string cooldown;          // 쿨타임 (예: "90초")
            public string requirement;       // 필요조건 (예: "지팡이 착용")
            public string confirmation;      // 확인사항 (예: "직업은 1개만 선택가능, Lv 10 이상")
            public string requiredItem;      // 필요 아이템
            public string passiveSkills;     // 패시브 스킬 (예: "마법 속성 저항 +15%")
        }

        /// <summary>
        /// 메이지 상세 툴팁 생성 (아처와 동일한 형식)
        /// </summary>
        public static string GetMageTooltip()
        {
            Plugin.Log.LogDebug("[메이지 툴팁] GetMageTooltip() 호출됨");
            
            // 컨피그에서 실제 값 가져오기
            var range = Mage_Config.MageAOERangeValue;
            var eitrCost = Mage_Config.MageEitrCostValue;
            var damageMultiplier = Mage_Config.MageDamageMultiplierValue;
            var cooldown = Mage_Config.MageCooldownValue;
            var vfxName = Mage_Config.MageVFXNameValue;
            
            Plugin.Log.LogDebug($"[메이지 툴팁] 컨피그 값들 - 범위: {range}, Eitr: {eitrCost}, 데미지: {damageMultiplier}%, 쿨타임: {cooldown}초");
            
            // 패시브 스킬 값 가져오기
            var elementalResistance = Mage_Config.MageElementalResistanceValue;
            
            // 상세 툴팁 데이터 생성
            var data = new MageTooltipData
            {
                skillName = L.Get("job_mage"),
                description = L.Get("mage_desc_aoe", damageMultiplier),
                range = $"{range}m",
                consumeEitr = $"{eitrCost}",
                skillType = L.Get("skill_type_job_active", "Y"),
                cooldown = $"{cooldown}{L.Get("unit_seconds")}",
                requirement = L.Get("requirement_mage"),
                confirmation = L.Get("confirmation_job_only"),
                passiveSkills = L.Get("mage_passive_resistance", elementalResistance),
                requiredItem = L.Get("item_eikthyr_trophy")
            };
            
            string finalTooltip = GenerateMageTooltip(data);
            Plugin.Log.LogDebug($"[메이지 툴팁] 최종 툴팁 생성 완료 - 길이: {finalTooltip?.Length ?? 0}");
            return finalTooltip;
        }

        /// <summary>
        /// 메이지 툴팁 생성 (새로운 형식 - 구조화된 표시)
        /// </summary>
        public static string GenerateMageTooltip(MageTooltipData data)
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
                if (!string.IsNullOrEmpty(data.consumeEitr))
                {
                    consumeParts.Add($"{L.Get("stat_eitr")} {data.consumeEitr}");
                }

                if (consumeParts.Count > 0)
                {
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{string.Join(", ", consumeParts)}</size></color>\n";
                }

                // 스킬 유형 섹션 (Y키 강조)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#ADFF2F><size=16>{data.skillType}</size></color>\n";
                }

                // 패시브 스킬 섹션
                if (!string.IsNullOrEmpty(data.passiveSkills))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color><color=#ADFF2F><size=16>{data.passiveSkills}</size></color>\n";
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

                // 필요 아이템 섹션
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
        /// 메이지 대체 툴팁 (설정 로드 실패 시)
        /// </summary>
        private static string GetMageFallbackTooltip()
        {
            var tooltipData = new MageTooltipData
            {
                skillName = L.Get("job_mage"),
                description = L.Get("mage_desc_aoe", 150),
                additionalInfo = "",
                range = "12m",
                consumeEitr = "35",
                skillType = L.Get("skill_type_job_active", "Y"),
                cooldown = $"90{L.Get("unit_seconds")}",
                requirement = L.Get("requirement_mage"),
                confirmation = L.Get("confirmation_job_only"),
                requiredItem = L.Get("item_eikthyr_trophy"),
                passiveSkills = L.Get("mage_passive_resistance", 15)
            };

            return GenerateMageTooltip(tooltipData);
        }

        /// <summary>
        /// 메이지 툴팁 강제 업데이트
        /// </summary>
        public static void UpdateMageTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("Mage"))
                {
                    string newTooltip = GetMageTooltip();
                    
                    // 기존 Description을 완전히 대체
                    var skillNode = manager.SkillNodes["Mage"];
                    var oldDescription = skillNode.Description;
                    skillNode.Description = newTooltip;
                    
                    // Plugin.Log.LogDebug($"[메이지 툴팁] 업데이트 완료");
                    // Plugin.Log.LogDebug($"[메이지 툴팁] 이전: {oldDescription?.Substring(0, Math.Min(50, oldDescription?.Length ?? 0))}...");
                    // Plugin.Log.LogDebug($"[메이지 툴팁] 새로운: {newTooltip?.Substring(0, Math.Min(50, newTooltip?.Length ?? 0))}...");
                }
                else
                {
                    // Plugin.Log.LogWarning($"[메이지 툴팁] Mage 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[메이지 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
    }
}
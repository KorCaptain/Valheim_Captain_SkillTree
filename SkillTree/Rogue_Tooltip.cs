using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 로그 직업 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// 탱커 시스템과 동일한 구조로 구현 (통합 버전 - 중복 제거)
    /// </summary>
    public static class Rogue_Tooltip
    {
        /// <summary>
        /// 로그 툴팁 데이터 구조체
        /// </summary>
        public struct RogueTooltipData
        {
            public string skillName;          // 스킬명 (예: "그림자 일격")
            public string description;        // 설명 (예: "Y키로 5초간 은신하며 어그로 제거")
            public string additionalInfo;     // 추가 정보 (예: "10초간 공격력 +30% 증가")
            public string range;             // 범위 (예: "어그로 제거 범위 15m")
            public string consumeStamina;    // 스태미나 소모 (예: "25")
            public string consumeArrow;      // 화살 소모 (로그는 "없음")
            public string skillType;         // 스킬 유형 (예: "액티브 스킬(Y키)")
            public string cooldown;          // 쿨타임 (예: "30초")
            public string requirement;       // 필요조건 (예: "단검 착용")
            public string confirmation;      // 확인사항
            public string requiredItem;      // 필요 아이템
            public string specialNote;       // 특별 안내
        }

        /// <summary>
        /// 로그 상세 툴팁 생성 (컨피그 연동)
        /// </summary>
        public static string GetRogueTooltip()
        {
            Plugin.Log.LogDebug("[로그 툴팁] GetRogueTooltip() 호출됨");

            try
            {
                // 컨피그에서 실제 값 가져오기
                var stealthDuration = Rogue_Config.RogueShadowStrikeStealthDurationValue;
                var attackBonus = Rogue_Config.RogueShadowStrikeAttackBonusValue;
                var buffDuration = Rogue_Config.RogueShadowStrikeBuffDurationValue;
                var cooldown = Rogue_Config.RogueShadowStrikeCooldownValue;
                var staminaCost = Rogue_Config.RogueShadowStrikeStaminaCostValue;
                var aggroRange = Rogue_Config.RogueShadowStrikeAggroRangeValue;

                // 로그 패시브 스킬 컨피그 값 가져오기
                var stealthSkillBonus = Rogue_Config.RogueSneakSkillBonusValue;
                var stealthSpeedBonus = Rogue_Config.RogueSneakSpeedBonusValue;
                var fallDamageReduction = Rogue_Config.RogueFallDamageReductionValue;

                Plugin.Log.LogDebug($"[로그 툴팁] 컨피그 값들 - 은신시간: {stealthDuration}초, 공격력: +{attackBonus}%, 쿨타임: {cooldown}초, 스태미나: {staminaCost}");

                // 메이지 스타일 데이터 구조
                var data = new RogueTooltipData
                {
                    skillName = L.Get("job_rogue"),
                    description = L.Get("rogue_desc_shadow_strike", stealthDuration, aggroRange),
                    additionalInfo = L.Get("rogue_desc_attack_bonus", buffDuration, attackBonus),
                    range = $"{aggroRange}m",
                    consumeStamina = $"{staminaCost:F0}",
                    consumeArrow = "",
                    skillType = L.Get("skill_type_job_active", "Y"),
                    cooldown = $"{cooldown:F0}{L.Get("unit_seconds")}",
                    requirement = L.Get("requirement_rogue"),
                    confirmation = L.Get("confirmation_job_only"),
                    requiredItem = L.Get("item_eikthyr_trophy"),
                    specialNote = ""
                };

                string finalTooltip = GenerateRogueTooltip(data, stealthSkillBonus, stealthSpeedBonus, fallDamageReduction);
                Plugin.Log.LogDebug($"[로그 툴팁] 최종 툴팁 생성 완료 - 길이: {finalTooltip?.Length ?? 0}");
                return finalTooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetRogueFallbackTooltip();
            }
        }

        /// <summary>
        /// 로그 툴팁 생성 (메이지 스타일)
        /// </summary>
        public static string GenerateRogueTooltip(RogueTooltipData data, float stealthSkillBonus, float stealthSpeedBonus, float fallDamageReduction)
        {
            try
            {
                var tooltip = "";

                // 스킬 이름 (황금색, 크기 22)
                tooltip += $"<color=#FFD700><size=22>{L.Get("job_rogue")}</size></color>\n\n";

                // 설명 섹션
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{data.description}";

                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $", {data.additionalInfo}";
                    }
                    tooltip += "</size></color>\n";
                }

                // 소모 섹션
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {data.consumeStamina}</size></color>\n";
                }

                // 스킬유형 섹션 (Y키 강조)
                tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#ADFF2F><size=16>{L.Get("skill_type_job_active", "Y")}</size></color>\n";

                // 패시브 섹션
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color><color=#ADFF2F><size=16>{L.Get("rogue_passive_desc", stealthSkillBonus, stealthSpeedBonus, fallDamageReduction)}</size></color>\n";

                // 쿨타임 섹션
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{L.Get("requirement_rogue")}</size></color>\n";

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
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 툴팁] 생성 실패: {ex.Message}");
                return L.Get("tooltip_generation_error");
            }
        }

        /// <summary>
        /// 포맷된 툴팁 문자열 생성 (표준 형식 - 레거시)
        /// </summary>
        private static string CreateFormattedTooltip(RogueTooltipData data)
        {
            try
            {
                var tooltip = $"{L.Get("tooltip_description")}: {data.description}\n";

                if (!string.IsNullOrEmpty(data.additionalInfo))
                {
                    tooltip += $"{L.Get("tooltip_additional")}: {data.additionalInfo}\n";
                }

                tooltip += $"{L.Get("tooltip_range")}: {data.range}\n";
                tooltip += $"{L.Get("tooltip_cost")}: {L.Get("stat_stamina")} {data.consumeStamina}";

                if (data.consumeArrow != L.Get("tooltip_none"))
                {
                    tooltip += $", {data.consumeArrow}";
                }

                tooltip += $"\n{L.Get("tooltip_skill_type")}: {data.skillType}\n";
                tooltip += $"{L.Get("tooltip_cooldown")}: {data.cooldown}\n";
                tooltip += $"{L.Get("tooltip_requirements")}: {data.requirement}\n";
                tooltip += $"{L.Get("tooltip_notice")}: {data.confirmation}\n";
                tooltip += $"{L.Get("tooltip_required_item")}: {data.requiredItem}";

                return tooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 툴팁] CreateFormattedTooltip 실패: {ex.Message}");
                return GetRogueFallbackTooltip();
            }
        }

        /// <summary>
        /// 로그 대체 툴팁 (오류 시 사용 - 메이지 스타일)
        /// </summary>
        private static string GetRogueFallbackTooltip()
        {
            var data = new RogueTooltipData
            {
                skillName = L.Get("job_rogue"),
                description = L.Get("rogue_desc_shadow_strike", Rogue_Config.RogueShadowStrikeStealthDurationValue, Rogue_Config.RogueShadowStrikeAggroRangeValue),
                additionalInfo = L.Get("rogue_desc_attack_bonus", Rogue_Config.RogueShadowStrikeBuffDurationValue, Rogue_Config.RogueShadowStrikeAttackBonusValue),
                range = $"{Rogue_Config.RogueShadowStrikeAggroRangeValue}m",
                consumeStamina = $"{Rogue_Config.RogueShadowStrikeStaminaCostValue:F0}",
                consumeArrow = "",
                skillType = L.Get("skill_type_job_active", "Y"),
                cooldown = $"{Rogue_Config.RogueShadowStrikeCooldownValue:F0}{L.Get("unit_seconds")}",
                requirement = L.Get("requirement_rogue"),
                confirmation = L.Get("confirmation_job_only"),
                requiredItem = L.Get("item_eikthyr_trophy"),
                specialNote = ""
            };

            return GenerateRogueTooltip(data, Rogue_Config.RogueSneakSkillBonusValue, Rogue_Config.RogueSneakSpeedBonusValue, Rogue_Config.RogueFallDamageReductionValue);
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

                    // 기존 Description을 완전히 대체
                    var skillNode = manager.SkillNodes["Rogue"];
                    skillNode.Description = newTooltip;

                    Plugin.Log.LogDebug($"[로그 툴팁] Rogue 툴팁 업데이트 완료");
                }
                else
                {
                    Plugin.Log.LogWarning($"[로그 툴팁] Rogue 스킬 노드를 찾을 수 없음");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 툴팁] 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 로그 툴팁 디버그 정보 출력
        /// </summary>
        public static void LogRogueTooltipDebugInfo()
        {
            try
            {
                Plugin.Log.LogInfo("=== [로그 툴팁] 디버그 정보 ===");
                
                // 현재 툴팁 내용
                string currentTooltip = GetRogueTooltip();
                Plugin.Log.LogDebug($"[로그 툴팁] 현재 툴팁:\n{currentTooltip}");
                
                // 컨피그 값들
                Rogue_Config.LogRogueConfigValues();
                
                Plugin.Log.LogInfo("=== [로그 툴팁] 디버그 정보 종료 ===");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 툴팁] 디버그 정보 출력 실패: {ex.Message}");
            }
        }
    }
}
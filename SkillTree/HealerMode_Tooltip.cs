using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 힐러모드 전용 동적 툴팁 시스템
    /// 컨피그 시스템과 연동하여 실시간 값을 표시
    /// </summary>
    public static class HealerMode_Tooltip
    {
        /// <summary>
        /// 힐러모드 툴팁 데이터 구조체
        /// </summary>
        public struct HealerModeTooltipData
        {
            public string skillName;          // 스킬명
            public string description;        // 설명
            public string additionalInfo;     // 추가 정보
            public string duration;          // 지속시간
            public string cooldown;          // 쿨타임
            public string consumeEitr;       // Eitr 소모
            public string healPercentage;    // 힐링 비율
            public string healRange;         // 힐링 범위
            // 발사체 수 필드 제거 - 즉시 힐링으로 변경
            public string skillType;         // 스킬 유형
            public string requirement;       // 필요조건
            public string confirmation;      // 확인사항
            public string specialNote;       // 특별 안내
        }

        /// <summary>
        /// 힐러모드 상세 툴팁 생성 (컨피그 연동)
        /// </summary>
        public static string GetHealerModeTooltip()
        {
            Plugin.Log.LogDebug("[힐러모드 툴팁] GetHealerModeTooltip() 호출됨");
            
            try
            {
                // 컨피그에서 실제 값 가져오기 (Staff_Config에서 힐 설정 가져옴)
                var cooldown = Staff_Config.StaffHealCooldownValue;
                var eitrCost = Staff_Config.StaffHealEitrCostValue;
                var healPercentage = Staff_Config.StaffHealPercentageValue;
                var healRange = Staff_Config.StaffHealRangeValue;
                var healSelf = Staff_Config.StaffHealSelfValue;
                // 발사체 관련 변수 제거 - 즉시 힐링으로 변경

                Plugin.Log.LogDebug($"[힐러모드 툴팁] 컨피그 값들 - 쿨타임: {cooldown}초, 힐링: {healPercentage}%, 범위: {healRange}m, 자힐: {healSelf}");
                
                // 상세 툴팁 데이터 생성
                var requiredPoints = Staff_Config.StaffHealRequiredPointsValue;
                string selfHealNote = healSelf ? "시전자 포함 치료" : "시전자는 치료되지 않음 (다른 플레이어만 치료)";

                var data = new HealerModeTooltipData
                {
                    skillName = "힐",
                    description = $"시전자 중심 {healRange:F0}m 범위 즉시 힐링",
                    additionalInfo = $"아군 최대체력의 {healPercentage:F0}% 즉시 회복",
                    duration = "", // 즉시 힐링이므로 지속시간 없음
                    cooldown = $"{cooldown:F0}초",
                    consumeEitr = $"{eitrCost:F0}",
                    healPercentage = $"{healPercentage:F0}%",
                    healRange = $"{healRange:F0}m",
                    // 발사체 수 제거 - 즉시 힐링으로 변경
                    skillType = "액티브 스킬 - H키",
                    requirement = "지팡이 또는 완드 착용",
                    confirmation = "", // 확인사항 제거
                    specialNote = $"{selfHealNote}\n\n<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color>"
                };
                
                string finalTooltip = GenerateHealerModeTooltip(data);
                Plugin.Log.LogDebug($"[힐러모드 툴팁] 최종 툴팁 생성 완료 - 길이: {finalTooltip?.Length ?? 0}");
                return finalTooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetHealerModeFallbackTooltip();
            }
        }

        /// <summary>
        /// 힐러모드 툴팁 생성 (H키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지/효과 → 범위 → 소모 → 스킬유형(H키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        public static string GenerateHealerModeTooltip(HealerModeTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 1. 스킬명 (#FFD700, size=22)
                if (!string.IsNullOrEmpty(data.skillName))
                {
                    tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";
                }

                // 2. 설명 (#FFD700 / #E0E0E0)
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>{data.description}";

                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 3. 효과 - 힐링 효과 (#FF6B6B / #FFB6C1) - 초록 대신 빨강 계열로 통일
                tooltip += $"<color=#98FB98><size=16>힐링 효과: </size></color><color=#00FF00><size=16>아군 최대체력의 {data.healPercentage} 즉시 회복</size></color>\n";

                // 4. 범위 (#87CEEB / #B0E0E6)
                tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>{data.healRange}</size></color>\n";

                // 5. 소모 (#FFB347 / #FFDAB9)
                tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>Eitr {data.consumeEitr}</size></color>\n";

                // 6. 스킬유형 (H키 강조: #FF1493 / #00FFFF)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#FF1493><size=16>스킬유형: </size></color><color=#00FFFF><size=16>{data.skillType}</size></color>\n";
                }

                // 7. 쿨타임 (#FFA500 / #FFDB58)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 8. 필요조건 (#98FB98 / #00FF00)
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 9. 확인사항 (#F0E68C / #FFE4B5)
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 10. 특별안내 + 필요포인트
                if (!string.IsNullOrEmpty(data.specialNote))
                {
                    tooltip += $"<color=#DDA0DD><size=16>특별안내: </size></color><color=#E6E6FA><size=16>{data.specialNote}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 툴팁] 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        /// <summary>
        /// 힐러모드 대체 툴팁 (설정 로드 실패 시)
        /// </summary>
        private static string GetHealerModeFallbackTooltip()
        {
            var requiredPoints = Staff_Config.StaffHealRequiredPointsValue;
            var healSelf = Staff_Config.StaffHealSelfValue;
            string selfHealNote = healSelf ? "시전자 포함 치료" : "시전자는 치료되지 않음 (다른 플레이어만 치료)";

            var tooltipData = new HealerModeTooltipData
            {
                skillName = "힐",
                description = $"시전자 중심 {Staff_Config.StaffHealRangeValue}m 범위 즉시 힐링",
                additionalInfo = $"아군 최대체력의 {Staff_Config.StaffHealPercentageValue}% 즉시 회복",
                duration = "", // 즉시 힐링이므로 지속시간 없음
                cooldown = $"{Staff_Config.StaffHealCooldownValue}초",
                consumeEitr = $"{Staff_Config.StaffHealEitrCostValue}",
                healPercentage = $"{Staff_Config.StaffHealPercentageValue}%",
                healRange = $"{Staff_Config.StaffHealRangeValue}m",
                // 발사체 수 제거 - 즉시 힐링으로 변경
                skillType = "액티브 스킬 - H키",
                requirement = "지팡이 또는 완드 착용",
                confirmation = "", // 확인사항 제거
                specialNote = $"{selfHealNote}\n\n<color=#87CEEB><size=16>💎 필요포인트: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color>"
            };

            return GenerateHealerModeTooltip(tooltipData);
        }

        /// <summary>
        /// 힐러모드 툴팁 강제 업데이트
        /// </summary>
        public static void UpdateHealerModeTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey("staff_Step6_heal"))
                {
                    string newTooltip = GetHealerModeTooltip();
                    
                    // 기존 Description을 완전히 대체
                    var skillNode = manager.SkillNodes["staff_Step6_heal"];
                    skillNode.Description = newTooltip;
                }
                else
                {
                    Plugin.Log.LogWarning($"[힐러모드 툴팁] staff_Step6_heal 스킬 노드를 찾을 수 없음");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐러모드 툴팁] 업데이트 실패: {ex.Message}");
            }
        }
    }
}
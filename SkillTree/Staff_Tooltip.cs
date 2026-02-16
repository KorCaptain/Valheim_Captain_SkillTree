using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 지팡이 스킬 동적 툴팁 시스템 (컨피그 연동)
    /// HealerMode_Tooltip 패턴을 따라 이중 시전 툴팁을 동적으로 생성
    /// </summary>
    public static class Staff_Tooltip
    {
        /// <summary>
        /// 이중 시전 툴팁 데이터 구조체
        /// </summary>
        public class DualCastTooltipData
        {
            public string skillName = "";
            public string description = "";
            public string additionalInfo = "";
            public string projectileCount = "";
            public string damagePercent = "";
            public string angleOffset = "";
            public string eitrCost = "";
            public string cooldown = "";
            public string skillType = "";
            public string requirement = "";
            public string confirmation = "";
            public string specialNote = "";
        }

        /// <summary>
        /// 이중 시전 스킬 동적 툴팁 생성 (컨피그 연동)
        /// </summary>
        public static string GetDualCastTooltip()
        {
            try
            {
                // Staff_Config에서 동적 설정값 가져오기
                int projectileCount = Staff_Config.StaffDoubleCastProjectileCountValue;
                float damagePercent = Staff_Config.StaffDoubleCastDamagePercentValue;
                float angleOffset = Staff_Config.StaffDoubleCastAngleOffsetValue;
                float eitrCost = Staff_Config.StaffDoubleCastEitrCostValue;
                float cooldown = Staff_Config.StaffDoubleCastCooldownValue;

                // 필요포인트 가져오기
                var requiredPoints = Staff_Config.StaffDoubleCastRequiredPointsValue;

                // 상세 툴팁 데이터 생성
                var data = new DualCastTooltipData
                {
                    skillName = L.Get("staff_skill_dual_cast"),
                    description = L.Get("staff_desc_dual_cast", projectileCount),
                    additionalInfo = L.Get("staff_desc_dual_cast_angle", angleOffset),
                    projectileCount = $"{projectileCount}{L.Get("unit_pieces")}",
                    damagePercent = $"{damagePercent:F0}%",
                    angleOffset = L.Get("staff_desc_dual_cast_angle_unit", angleOffset),
                    eitrCost = $"{eitrCost:F0}",
                    cooldown = $"{cooldown:F0}{L.Get("unit_seconds")}",
                    skillType = L.Get("skill_type_active_key", "R"),
                    requirement = L.Get("requirement_staff_wand"),
                    confirmation = "",
                    specialNote = $"{L.Get("staff_desc_dual_cast_note")}\n\n<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color>"
                };

                string finalTooltip = GenerateDualCastTooltip(data);
                return finalTooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetDualCastFallbackTooltip();
            }
        }

        /// <summary>
        /// 이중 시전 툴팁 생성 (R키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형(R키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        public static string GenerateDualCastTooltip(DualCastTooltipData data)
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
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{data.description}";

                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 3. 데미지 (#FF6B6B / #FFB6C1)
                if (!string.IsNullOrEmpty(data.damagePercent))
                {
                    tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color><color=#FFB6C1><size=16>{L.Get("staff_desc_dual_cast_damage", data.damagePercent)}</size></color>\n";
                }

                // 4. 범위 - 분산 각도 (#87CEEB / #B0E0E6)
                if (!string.IsNullOrEmpty(data.angleOffset))
                {
                    tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_dispersion_angle")}: </size></color><color=#B0E0E6><size=16>{data.angleOffset}</size></color>\n";
                }

                // 5. 소모 (#FFB347 / #FFDAB9)
                if (!string.IsNullOrEmpty(data.eitrCost))
                {
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_eitr")} {data.eitrCost}</size></color>\n";
                }

                // 6. 스킬유형 (R키 강조: #9400D3 / #FFD700)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#FFD700><size=16>{data.skillType}</size></color>\n";
                }

                // 7. 쿨타임 (#FFA500 / #FFDB58)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 8. 필요조건 (#98FB98 / #00FF00)
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 9. 확인사항 (#F0E68C / #FFE4B5)
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 10. 특별안내 + 필요포인트
                if (!string.IsNullOrEmpty(data.specialNote))
                {
                    tooltip += $"<color=#DDA0DD><size=16>{L.Get("tooltip_special_note")}: </size></color><color=#E6E6FA><size=16>{data.specialNote}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전 툴팁] 구조화된 툴팁 생성 실패: {ex.Message}");
                return GetDualCastFallbackTooltip();
            }
        }

        /// <summary>
        /// 이중 시전 백업 툴팁 (오류 시 사용)
        /// </summary>
        public static string GetDualCastFallbackTooltip()
        {
            var requiredPoints = Staff_Config.StaffDoubleCastRequiredPointsValue;

            return $"<color=#FFD700><size=22>{L.Get("staff_skill_dual_cast")}</size></color>\n\n" +
                   $"<color=#E0E0E0><size=16>{L.Get("skill_type_active_key", "R")}: {L.Get("staff_desc_dual_cast_fallback")}\n\n" +
                   $"• {L.Get("tooltip_damage")}: {L.Get("staff_desc_dual_cast_damage_fallback")}\n" +
                   $"• {L.Get("tooltip_cost")}: {L.Get("stat_eitr")} 20\n" +
                   $"• {L.Get("tooltip_cooldown")}: 30{L.Get("unit_seconds")}\n" +
                   $"• {L.Get("tooltip_requirements")}: {L.Get("requirement_staff_wand")}\n\n" +
                   $"{L.Get("tooltip_skill_type")}: {L.Get("skill_type_active_key", "R")}\n\n" +
                   $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color></size></color>";
        }

        /// <summary>
        /// 툴팁 데이터 검증
        /// </summary>
        public static bool ValidateTooltipData(DualCastTooltipData data)
        {
            try
            {
                if (data == null)
                {
                    Plugin.Log.LogError("[이중 시전 툴팁] 툴팁 데이터가 null입니다");
                    return false;
                }

                if (string.IsNullOrEmpty(data.skillName))
                {
                    Plugin.Log.LogWarning("[이중 시전 툴팁] 스킬 이름이 비어있습니다");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전 툴팁] 데이터 검증 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 현재 컨피그 값들 디버그 출력
        /// </summary>
        public static void LogCurrentTooltipConfig()
        {
            try
            {
                Plugin.Log.LogInfo("[이중 시전 툴팁] 현재 컨피그 기반 툴팁 설정:");
                Plugin.Log.LogInfo($"  - 추가 발사체 수: {Staff_Config.StaffDoubleCastProjectileCountValue}개");
                Plugin.Log.LogInfo($"  - 발사체 데미지: {Staff_Config.StaffDoubleCastDamagePercentValue}%");
                Plugin.Log.LogInfo($"  - 각도 오프셋: ±{Staff_Config.StaffDoubleCastAngleOffsetValue}°");
                Plugin.Log.LogInfo($"  - 에이트르 소모: {Staff_Config.StaffDoubleCastEitrCostValue}");
                Plugin.Log.LogInfo($"  - 쿨타임: {Staff_Config.StaffDoubleCastCooldownValue}초");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전 툴팁] 컨피그 로그 출력 실패: {ex.Message}");
            }
        }
    }
}
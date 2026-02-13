using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폴암 스킬 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Polearm_Tooltip
    {
        // MeleeTooltipUtils.MeleeTooltipData 사용
        // 기존 PolearmTooltipData 제거

        /// <summary>
        /// 폴암 전문가 툴팁 생성
        /// </summary>
        public static string GetPolearmExpertTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmExpertTooltip() 호출됨");
            
            var requiredPoints = 2; // 폴암 전문가 고정값
            
            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "폴암 전문가",
                $"공격 범위 +{SkillTreeConfig.PolearmExpertRangeBonusValue}%",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requirement = "폴암 착용";
            data.requiredPoints = requiredPoints.ToString();
            
            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 회전베기 툴팁 생성
        /// </summary>
        public static string GetPolearmStep1SpinTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep1SpinTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "회전베기",
                $"특수 공격 공격력 +{SkillTreeConfig.PolearmStep1SpinWheelDamageValue}%",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 제압 공격 툴팁 생성 (Tier 5 - 폴암강화와 티어 교환)
        /// </summary>
        public static string GetPolearmStep1SuppressTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep1SuppressTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "제압 공격",
                $"공격력 +{SkillTreeConfig.PolearmStep1SuppressDamageValue}%",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 영웅 타격 툴팁 생성
        /// </summary>
        public static string GetPolearmStep2HeroTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep2HeroTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "영웅 타격",
                $"{SkillTreeConfig.PolearmStep2HeroKnockbackChanceValue}% 확률로 넉백",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 광역 강타 툴팁 생성
        /// </summary>
        public static string GetPolearmStep3AreaTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep3AreaTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "광역 강타",
                $"2연속 공격 시 공격력 +{SkillTreeConfig.PolearmStep3AreaComboBonusValue}%({SkillTreeConfig.PolearmStep3AreaComboDurationValue}초동안)",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 지면 강타 툴팁 생성
        /// </summary>
        public static string GetPolearmStep3GroundTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep3GroundTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "지면 강타",
                $"특수 공격 공격력 +{SkillTreeConfig.PolearmStep3GroundWheelDamageValue}%",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 반달 베기 툴팁 생성
        /// </summary>
        public static string GetPolearmStep4MoonTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep4MoonTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "반달 베기",
                $"공격 범위 +{SkillTreeConfig.PolearmStep4MoonRangeBonusValue}%, 공격 스태미나 -{SkillTreeConfig.PolearmStep4MoonStaminaReductionValue}%",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 폴암강화 툴팁 생성 (Tier 3 - 제압 공격과 티어 교환)
        /// </summary>
        public static string GetPolearmStep4ChargeTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep4ChargeTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "폴암강화",
                $"무기 공격력 +{SkillTreeConfig.PolearmStep4ChargeDamageBonusValue}",
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 관통 돌격 툴팁 생성 (G키 액티브 스킬)
        /// 전방 돌진 → 첫 관통 타격 → 후방 AOE 넉백
        /// </summary>
        public static string GetPolearmStep5KingTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep5KingTooltip() 호출됨 (관통 돌격)");

                // Config에서 동적 설정값 가져오기
                float dashDistance = Polearm_Config.PolearmPierceChargeDashDistanceValue;
                float primaryDamage = Polearm_Config.PolearmPierceChargePrimaryDamageValue;
                float aoeDamage = Polearm_Config.PolearmPierceChargeAoeDamageValue;
                float aoeAngle = Polearm_Config.PolearmPierceChargeAoeAngleValue;
                float aoeRadius = Polearm_Config.PolearmPierceChargeAoeRadiusValue;
                float knockbackDist = Polearm_Config.PolearmPierceChargeKnockbackDistanceValue;
                float staminaCost = Polearm_Config.PolearmPierceChargeStaminaCostValue;
                float cooldown = Polearm_Config.PolearmPierceChargeCooldownValue;

                var tooltip = "";

                // 1. 스킬명 (#FFD700, size=22)
                tooltip += $"<color=#FFD700><size=22>관통 돌격</size></color>\n\n";

                // 2. 설명 (#FFD700 / #E0E0E0)
                tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>전방 {dashDistance:F0}m 돌진, 적 충돌 시 관통 공격</size></color>\n";

                // 3. 데미지 (#FF6B6B / #FFB6C1)
                tooltip += $"<color=#FF6B6B><size=16>첫 타격: </size></color><color=#FFB6C1><size=16>공격력 +{primaryDamage:F0}%</size></color>\n";

                // 4. AOE 데미지
                tooltip += $"<color=#FF6B6B><size=16>AOE 넉백: </size></color><color=#FFB6C1><size=16>공격력 +{aoeDamage:F0}% (뒤쪽 {aoeAngle:F0}°, {aoeRadius:F0}m)</size></color>\n";

                // 5. 넉백 거리
                tooltip += $"<color=#87CEEB><size=16>넉백 거리: </size></color><color=#ADD8E6><size=16>{knockbackDist:F0}m</size></color>\n";

                // 6. 소모 (#FFB347 / #FFDAB9)
                tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {staminaCost:F0}</size></color>\n";

                // 7. 스킬유형 (G키 강조: #FF4500 / #00FF00)
                tooltip += $"<color=#FF4500><size=16>스킬유형: </size></color><color=#00FF00><size=16>액티브 스킬 - G키</size></color>\n";

                // 8. 쿨타임 (#FFA500 / #FFDB58)
                tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{cooldown:F0}초</size></color>\n";

                // 9. 필요조건 (#98FB98 / #00FF00)
                tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>폴암 착용</size></color>\n";

                // 10. 확인사항 (#F0E68C / #FFE4B5)
                tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>같은 무기 전문가 내에서만 다중 습득 가능</size></color>\n";

                // 11. 필요포인트 (#87CEEB / #FF6B6B)
                tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>3</size></color>";

                Plugin.Log.LogDebug($"[관통 돌격 툴팁] 최종 툴팁 생성 완료 - 길이: {tooltip?.Length ?? 0}");
                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[관통 돌격 툴팁] 생성 실패: {ex.Message}");
                return "<color=#FFD700><size=22>관통 돌격</size></color>\n\n<color=#E0E0E0><size=16>액티브 G키 스킬\n툴팁 생성 오류</size></color>";
            }
        }

        // MeleeTooltipUtils.GenerateTooltip() 사용
        // 기존 GeneratePolearmTooltip() 제거
        // [DEPRECATED] - Use MeleeTooltipUtils.GenerateTooltip() instead
        private static string GeneratePolearmTooltip(MeleeTooltipUtils.MeleeTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 설명 섹션
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#E0E0E0><size=16>{data.description}";
                    
                    // 추가 정보가 있으면 괄호로 추가
                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 소모 섹션 (있을 때만 표시)
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {data.consumeStamina}</size></color>\n";
                }

                // 쿨타임 섹션 (액티브 스킬만)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폴암 툴팁] 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        #region 스킬 매핑

        /// <summary>
        /// 폴암 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> PolearmSkillMappings = new()
        {
            { "polearm_expert", GetPolearmExpertTooltip },
            { "polearm_step1_spin", GetPolearmStep1SpinTooltip },
            { "polearm_step1_suppress", GetPolearmStep1SuppressTooltip },
            { "polearm_step2_hero", GetPolearmStep2HeroTooltip },
            { "polearm_step3_area", GetPolearmStep3AreaTooltip },
            { "polearm_step3_ground", GetPolearmStep3GroundTooltip },
            { "polearm_step4_moon", GetPolearmStep4MoonTooltip },
            { "polearm_step4_charge", GetPolearmStep4ChargeTooltip },
            { "polearm_step5_king", GetPolearmStep5KingTooltip }
        };

        #endregion

        /// <summary>
        /// 모든 폴암 스킬 툴팁 업데이트
        /// </summary>
        public static void UpdatePolearmTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(PolearmSkillMappings, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 개별 폴암 스킬 툴팁 업데이트
        /// </summary>
        private static void UpdateIndividualPolearmTooltip(string skillId, string newTooltip)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey(skillId))
                {
                    var skillNode = manager.SkillNodes[skillId];
                    skillNode.Description = newTooltip;
                    
                    Plugin.Log.LogDebug($"[폴암 툴팁] {skillId} 업데이트 완료");
                }
                else
                {
                    Plugin.Log.LogWarning($"[폴암 툴팁] {skillId} 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폴암 툴팁] {skillId} 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 특정 폴암 스킬 툴팁 가져오기
        /// </summary>
        public static string GetPolearmSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, PolearmSkillMappings, MeleeTooltipUtils.WeaponType.Polearm);
        }
    }
}
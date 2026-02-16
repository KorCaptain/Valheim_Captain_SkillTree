using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

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

            var requiredPoints = 2;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_expert")}</size></color>",
                L.Get("polearm_desc_expert", SkillTreeConfig.PolearmExpertRangeBonusValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requirement = L.Get("requirement_polearm_equip");
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
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_spin")}</size></color>",
                L.Get("polearm_desc_spin", SkillTreeConfig.PolearmStep1SpinWheelDamageValue),
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
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_suppress")}</size></color>",
                L.Get("polearm_desc_suppress", SkillTreeConfig.PolearmStep1SuppressDamageValue),
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
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_hero")}</size></color>",
                L.Get("polearm_desc_hero", SkillTreeConfig.PolearmStep2HeroKnockbackChanceValue),
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
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_area")}</size></color>",
                L.Get("polearm_desc_area", SkillTreeConfig.PolearmStep3AreaComboBonusValue, SkillTreeConfig.PolearmStep3AreaComboDurationValue),
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
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_ground")}</size></color>",
                L.Get("polearm_desc_ground", SkillTreeConfig.PolearmStep3GroundWheelDamageValue),
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
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_moon")}</size></color>",
                L.Get("polearm_desc_moon", SkillTreeConfig.PolearmStep4MoonRangeBonusValue, SkillTreeConfig.PolearmStep4MoonStaminaReductionValue),
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
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_charge")}</size></color>",
                L.Get("polearm_desc_charge", SkillTreeConfig.PolearmStep4ChargeDamageBonusValue),
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
                tooltip += $"<color=#FFD700><size=22>{L.Get("polearm_skill_king")}</size></color>\n\n";

                // 2. 설명 (#FFD700 / #E0E0E0)
                tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{L.Get("polearm_desc_king", dashDistance)}</size></color>\n";

                // 3. 데미지 (#FF6B6B / #FFB6C1)
                tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_first_hit")}: </size></color><color=#FFB6C1><size=16>{L.Get("polearm_desc_king_first", primaryDamage)}</size></color>\n";

                // 4. AOE 데미지
                tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_aoe_knockback")}: </size></color><color=#FFB6C1><size=16>{L.Get("polearm_desc_king_aoe", aoeDamage, aoeAngle, aoeRadius)}</size></color>\n";

                // 5. 넉백 거리
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_knockback_distance")}: </size></color><color=#ADD8E6><size=16>{L.Get("polearm_desc_king_knockback", knockbackDist)}</size></color>\n";

                // 6. 소모 (#FFB347 / #FFDAB9)
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {staminaCost:F0}</size></color>\n";

                // 7. 스킬유형 (G키 강조: #FF4500 / #00FF00)
                tooltip += $"<color=#FF4500><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#00FF00><size=16>{L.Get("skill_type_active_key", "G")}</size></color>\n";

                // 8. 쿨타임 (#FFA500 / #FFDB58)
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{cooldown:F0}{L.Get("unit_seconds")}</size></color>\n";

                // 9. 필요조건 (#98FB98 / #00FF00)
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{L.Get("requirement_polearm_equip")}</size></color>\n";

                // 10. 확인사항 (#F0E68C / #FFE4B5)
                tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{L.Get("tooltip_same_weapon_only")}</size></color>\n";

                // 11. 필요포인트 (#87CEEB / #FF6B6B)
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>3</size></color>";

                Plugin.Log.LogDebug($"[관통 돌격 툴팁] 최종 툴팁 생성 완료 - 길이: {tooltip?.Length ?? 0}");
                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[관통 돌격 툴팁] 생성 실패: {ex.Message}");
                return $"<color=#FFD700><size=22>{L.Get("polearm_skill_king")}</size></color>\n\n<color=#E0E0E0><size=16>{L.Get("skill_type_active_key", "G")}\n{L.Get("tooltip_generation_error")}</size></color>";
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
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {data.consumeStamina}</size></color>\n";
                }

                // 쿨타임 섹션 (액티브 스킬만)
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
                    tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폴암 툴팁] 생성 실패: {ex.Message}");
                return L.Get("tooltip_generation_error");
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
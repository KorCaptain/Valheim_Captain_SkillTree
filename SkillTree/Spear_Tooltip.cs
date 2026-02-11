using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 창 스킬 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Spear_Tooltip
    {
        /// <summary>
        /// 창 툴팁 데이터 구조체
        /// </summary>
        public struct SpearTooltipData
        {
            public string skillName;          // 스킬명
            public string description;        // 설명
            public string additionalInfo;     // 추가 정보
            public string consumeStamina;     // 스태미나 소모
            public string skillType;          // 스킬 유형
            public string cooldown;           // 쿨타임
            public string requirement;        // 필요조건
            public string confirmation;       // 확인사항
        }

        /// <summary>
        /// 창 전문가 툴팁 생성
        /// </summary>
        public static string GetSpearExpertTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearExpertTooltip() 호출됨");
            
            var requiredPoints = 2; // 창 전문가 고정값
            
            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "창 전문가",
                $"2연속 공격 시 공격 속도 +{SkillTreeConfig.SpearStep1AttackSpeedValue}%, 공격력 +{SkillTreeConfig.SpearStep1DamageBonusValue}%({SkillTreeConfig.SpearStep1DurationValue}초 동안)",
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requirement = "창 착용";
            data.requiredPoints = requiredPoints.ToString();
            
            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 투창 전문가 툴팁 생성 (패시브 스킬 - 쿨타임 없음)
        /// </summary>
        public static string GetSpearStep1ThrowTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep1ThrowTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "투창 전문가",
                $"창 던지기 공격력 +{SkillTreeConfig.SpearStep2ThrowDamageValue}%",
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requirement = "한손 창 착용";
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 급소 찌르기 툴팁 생성
        /// </summary>
        public static string GetSpearStep1CritTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep1CritTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "급소 찌르기",
                $"창 공격력 +{Spear_Config.SpearStep2CritDamageBonusValue}%",
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 연격창 툴팁 생성 (Tier 3)
        /// </summary>
        public static string GetSpearStep3PierceTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep3PierceTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "연격창",
                $"무기 공격력 +{SkillTreeConfig.SpearStep3PierceDamageBonusValue}",
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 회피 찌르기 툴팁 생성 (Tier 4)
        /// </summary>
        public static string GetSpearStep2EvasionTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep2EvasionTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "회피 찌르기",
                $"구르기 직후 공격 시 피해 +{Spear_Config.SpearStep3EvasionDamageBonusValue}%, 공격 스태미나 -{Spear_Config.SpearStep3EvasionStaminaReductionValue}%",
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 쾌속 창 툴팁 생성
        /// </summary>
        public static string GetSpearStep3QuickTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep3QuickTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "쾌속 창",
                $"투창 공격력 +{Spear_Config.SpearStep4QuickDamageBonusValue}%",
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 삼연창 툴팁 생성
        /// </summary>
        public static string GetSpearStep4TripleTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep4TripleTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "삼연창",
                $"3연속 공격 시 공격력 +{Spear_Config.SpearStep5TripleDamageBonusValue}%",
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 꿰뚫는 창 액티브 스킬 툴팁 생성 (번개 충격)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형(G키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        public static string GetSpearStep5PenetrateTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep5PenetrateTooltip() 호출됨");

            try
            {
                var tooltip = "";

                // 1. 스킬명 (#FFD700, size=22)
                tooltip += $"<color=#FFD700><size=22>꿰뚫는 창</size></color>\n\n";

                // 2. 설명 (#FFD700 / #E0E0E0)
                tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>{Spear_Config.SpearStep6PenetrateBuffDurationValue}초간 번개 충격 모드 활성화, {Spear_Config.SpearStep6PenetrateComboCountValue}회 연속 적중 시 번개 충격 발동</size></color>\n";

                // 3. 데미지 (#FF6B6B / #FFB6C1)
                tooltip += $"<color=#FF6B6B><size=16>데미지: </size></color><color=#FFB6C1><size=16>무기 공격력 +{Spear_Config.SpearStep6PenetrateLightningDamageValue}%</size></color>\n";

                // 4. 범위 - 생략 (단일 대상)

                // 5. 소모 (#FFB347 / #FFDAB9)
                tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {Spear_Config.SpearStep6PenetrateStaminaCostValue}%</size></color>\n";

                // 6. 스킬유형 (G키 강조: #FF4500 / #00FF00)
                tooltip += $"<color=#FF4500><size=16>스킬유형: </size></color><color=#00FF00><size=16>액티브 스킬 - G키</size></color>\n";

                // 7. 쿨타임 (#FFA500 / #FFDB58)
                tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{Spear_Config.SpearStep6PenetrateCooldownValue}초</size></color>\n";

                // 8. 필요조건 (#98FB98 / #00FF00)
                tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>창 착용</size></color>\n";

                // 9. 확인사항 (#F0E68C / #FFE4B5)
                tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>같은 무기 전문가 내에서만 다중 습득 가능</size></color>\n";

                // 10. 필요포인트 (#87CEEB / #FF6B6B)
                tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>3</size></color>";

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[창 툴팁] GetSpearStep5PenetrateTooltip 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        /// <summary>
        /// 연공창 액티브 스킬 툴팁 생성 (H키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형(H키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        public static string GetSpearEnhancedThrowTooltip()
        {
            try
            {
                var tooltip = "";

                // 1. 스킬명 (#FFD700, size=22)
                tooltip += $"<color=#FFD700><size=22>연공창</size></color>\n\n";

                // 2. 설명 (#FFD700 / #E0E0E0)
                tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>투창을 강화하여 창을 던지고 적과 주변 몬스터를 넉백시킴</size></color>\n";

                // 3. 데미지 (#FF6B6B / #FFB6C1)
                tooltip += $"<color=#FF6B6B><size=16>데미지: </size></color><color=#FFB6C1><size=16>+{SkillTreeConfig.SpearStep6ComboDamageValue}%</size></color>\n";

                // 4. 범위 (#87CEEB / #B0E0E6)
                tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>주변 {SkillTreeConfig.SpearStep2ThrowRangeValue}m</size></color>\n";

                // 5. 소모 (#FFB347 / #FFDAB9)
                tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {SkillTreeConfig.SpearStep2ThrowStaminaCostValue}%</size></color>\n";

                // 6. 스킬유형 (H키 강조: #FF1493 / #00FFFF)
                tooltip += $"<color=#FF1493><size=16>스킬유형: </size></color><color=#00FFFF><size=16>액티브 스킬 - H키</size></color>\n";

                // 7. 쿨타임 (#FFA500 / #FFDB58)
                tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{SkillTreeConfig.SpearStep6ComboCooldownValue}초</size></color>\n";

                // 8. 필요조건 (#98FB98 / #00FF00)
                tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>창 착용</size></color>\n";

                // 9. 확인사항 (#F0E68C / #FFE4B5)
                tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>같은 무기 전문가 내에서만 다중 습득 가능</size></color>\n";

                // 10. 필요포인트 (#87CEEB / #FF6B6B)
                tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>3</size></color>";

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[연공창 툴팁] 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        /// <summary>
        /// 창 툴팁 생성 (구버전 스타일)
        /// </summary>
        public static string GenerateSpearTooltip(SpearTooltipData data)
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
                Plugin.Log.LogError($"[창 툴팁] 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        #region 스킬 매핑

        /// <summary>
        /// 창 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> SpearSkillMappings = new()
        {
            { "spear_expert", GetSpearExpertTooltip },
            { "spear_Step1_throw", GetSpearStep1ThrowTooltip },
            { "spear_Step1_crit", GetSpearStep1CritTooltip },
            { "spear_Step2_evasion", GetSpearStep2EvasionTooltip },
            { "spear_Step3_pierce", GetSpearStep3PierceTooltip },
            { "spear_Step3_quick", GetSpearStep3QuickTooltip },
            { "spear_Step4_triple", GetSpearStep4TripleTooltip },
            { "spear_Step5_penetrate", GetSpearStep5PenetrateTooltip },
            { "spear_Step5_combo", GetSpearEnhancedThrowTooltip }
        };

        #endregion

        /// <summary>
        /// 모든 창 스킬 툴팁 업데이트
        /// </summary>
        public static void UpdateSpearTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(SpearSkillMappings, MeleeTooltipUtils.WeaponType.Spear);
        }

        // MeleeTooltipUtils.UpdateSkillTooltip() 사용
        // 기존 UpdateIndividualSpearTooltip() 제거

        /// <summary>
        /// 특정 창 스킬 툴팁 가져오기
        /// </summary>
        public static string GetSpearSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, SpearSkillMappings, MeleeTooltipUtils.WeaponType.Spear);
        }
    }
}
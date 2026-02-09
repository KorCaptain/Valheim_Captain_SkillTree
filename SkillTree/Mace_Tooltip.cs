using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기 스킬 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Mace_Tooltip
    {
        // MeleeTooltipUtils.MeleeTooltipData 사용
        // 기존 MaceTooltipData 제거

        /// <summary>
        /// 둔기 전문가 툴팁 생성
        /// </summary>
        public static string GetMaceExpertTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceExpertTooltip() 호출됨");

            var requiredPoints = Mace_Config.MaceExpertRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "둔기 전문가",
                $"둔기 피해 +{Mace_Config.MaceExpertDamageBonusValue}%, 공격 시 {Mace_Config.MaceExpertStunChanceValue}% 확률로 {Mace_Config.MaceExpertStunDurationValue}초 기절",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requirement = "둔기 착용";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 공격력 강화 툴팁 생성
        /// </summary>
        public static string GetMaceStep1DamageTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep1DamageTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "공격력 강화",
                $"공격력 +{Mace_Config.MaceStep1DamageBonusValue}%",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 2단계 기절 강화 툴팁 생성
        /// </summary>
        public static string GetMaceStep2StunBoostTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep2StunBoostTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "기절 강화",
                $"기절 확률 +{Mace_Config.MaceStep2StunChanceBonusValue}%, 지속시간 +{Mace_Config.MaceStep2StunDurationBonusValue}초",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 3단계 방어 분기 툴팁 생성
        /// </summary>
        public static string GetMaceStep3BranchGuardTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep3BranchGuardTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "방어 강화",
                $"방어력 +{Mace_Config.MaceStep3GuardArmorBonusValue}",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 3단계 무거운 분기 툴팁 생성
        /// </summary>
        public static string GetMaceStep3BranchHeavyTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep3BranchHeavyTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "무거운 타격",
                $"공격력 +{Mace_Config.MaceStep3HeavyDamageBonusValue}",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 4단계 밀어내기 툴팁 생성
        /// </summary>
        public static string GetMaceStep4PushTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep4PushTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "밀어내기",
                $"공격 시 {Mace_Config.MaceStep4KnockbackChanceValue}% 확률로 넉백",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 5단계 탱커 툴팁 생성
        /// </summary>
        public static string GetMaceStep5TankTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep5TankTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "탱커",
                $"체력 +{Mace_Config.MaceStep5TankHealthBonusValue}%, 받는 데미지 -{Mace_Config.MaceStep5TankDamageReductionValue}%",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 5단계 데미지 툴팁 생성
        /// </summary>
        public static string GetMaceStep5DpsTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep5DpsTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "공격력 강화",
                $"공격력 +{Mace_Config.MaceStep5DpsDamageBonusValue}%, 공격속도 +{Mace_Config.MaceStep5DpsAttackSpeedBonusValue}%",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 6단계 그랜드마스터 툴팁 생성
        /// </summary>
        public static string GetMaceStep6GrandmasterTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep6GrandmasterTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "그랜드마스터",
                $"방어 +{Mace_Config.MaceStep6ArmorBonusValue}%",
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 분노의 망치 툴팁 데이터 구조체
        /// </summary>
        public class FuryHammerTooltipData
        {
            public string skillName = "";
            public string description = "";
            public string additionalInfo = "";
            public string attackCount = "";
            public string baseDamage = "";
            public string damageIncrement = "";
            public string aoeRadius = "";
            public string attackInterval = "";
            public string staminaCost = "";
            public string cooldown = "";
            public string skillType = "";
            public string requirement = "";
            public string confirmation = "";
            public string requiredPoints = "";
        }

        /// <summary>
        /// 둔기 7단계 분노의 망치 툴팁 생성
        /// </summary>
        public static string GetMaceStep7FuryHammerTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep7FuryHammerTooltip() 호출됨");

                // 하드코딩 상수 (수정 불가)
                const int attackCount = 5;           // 5타 고정
                const float attackInterval = 0.5f;   // 0.5초 고정

                // Config에서 동적 설정값 가져오기
                float normalHitMultiplier = Mace_Config.FuryHammerNormalHitMultiplierValue;
                float finalHitMultiplier = Mace_Config.FuryHammerFinalHitMultiplierValue;
                float aoeRadius = Mace_Config.FuryHammerAoeRadiusValue;
                float staminaCost = Mace_Config.FuryHammerStaminaCostValue;
                float cooldown = Mace_Config.FuryHammerCooldownValue;

                Plugin.Log.LogDebug($"[분노의 망치 툴팁] 컨피그 값들 - 공격 횟수: {attackCount}회 (고정), 1~4타 배율: {normalHitMultiplier}%, 5타 배율: {finalHitMultiplier}%, 스태미나: {staminaCost}, 쿨타임: {cooldown}초");

                // 상세 툴팁 데이터 생성 (간소화)
                var data = new FuryHammerTooltipData
                {
                    skillName = "분노의 망치",
                    description = $"{attackCount}회 연속 세컨더리 어택 발동",
                    additionalInfo = $"1~4타: {attackInterval}초 간격, 5타: {attackInterval}초 후 데미지",
                    attackCount = "", // 제거
                    baseDamage = $"현재 공격력 × 1~4타: {normalHitMultiplier:F0}%, 5타: {finalHitMultiplier:F0}%",
                    damageIncrement = "", // 제거
                    aoeRadius = $"{aoeRadius:F0}m",
                    attackInterval = "", // 제거 (additionalInfo에 포함)
                    staminaCost = $"{staminaCost:F0}",
                    cooldown = $"{cooldown:F0}초",
                    skillType = "액티브 스킬 - H키",
                    requirement = "양손둔기 착용",
                    confirmation = "같은 무기 전문가 내에서만 다중 습득 가능",
                    requiredPoints = "3"
                };

                string finalTooltip = GenerateFuryHammerTooltip(data);
                Plugin.Log.LogDebug($"[분노의 망치 툴팁] 최종 툴팁 생성 완료 - 길이: {finalTooltip?.Length ?? 0}");
                return finalTooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[분노의 망치 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetFuryHammerFallbackTooltip();
            }
        }

        /// <summary>
        /// 분노의 망치 툴팁 생성 (H키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형(H키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        private static string GenerateFuryHammerTooltip(FuryHammerTooltipData data)
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

                // 3. 데미지 (#FF6B6B / #FFB6C1)
                if (!string.IsNullOrEmpty(data.baseDamage))
                {
                    tooltip += $"<color=#FF6B6B><size=16>데미지: </size></color><color=#FFB6C1><size=16>{data.baseDamage}</size></color>\n";
                }

                // 4. 범위 (#87CEEB / #B0E0E6) - AOE 범위
                if (!string.IsNullOrEmpty(data.aoeRadius))
                {
                    tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>{data.aoeRadius}</size></color>\n";
                }

                // 5. 소모 (#FFB347 / #FFDAB9)
                if (!string.IsNullOrEmpty(data.staminaCost))
                {
                    tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {data.staminaCost}</size></color>\n";
                }

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

                // 10. 필요포인트 (#87CEEB / #FF6B6B)
                if (!string.IsNullOrEmpty(data.requiredPoints))
                {
                    tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>{data.requiredPoints}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[분노의 망치 툴팁] 구조화된 툴팁 생성 실패: {ex.Message}");
                return GetFuryHammerFallbackTooltip();
            }
        }

        /// <summary>
        /// 분노의 망치 백업 툴팁 (오류 시 사용)
        /// </summary>
        private static string GetFuryHammerFallbackTooltip()
        {
            return "<color=#FFD700><size=22>분노의 망치</size></color>\n\n" +
                   "<color=#E0E0E0><size=16>H키: 5회 연속 세컨더리 어택 발동 (1~4타: 0.8초 간격, 5타: 0.5초 후 데미지)\n\n" +
                   "• 데미지: 현재 공격력 × 1~4타: 80%, 5타: 150%\n" +
                   "• AOE 범위: 5m\n" +
                   "• 소모: 스태미나 40\n" +
                   "• 쿨타임: 30초\n" +
                   "• 필요조건: 둔기 착용\n\n" +
                   "💥 스킬유형: 액티브 스킬 - H키\n\n" +
                   "확인사항: 같은 무기 전문가 내에서만 다중 습득 가능</size></color>";
        }

        /// <summary>
        /// 수호자의 진심 툴팁 데이터 구조체
        /// </summary>
        public class GuardianHeartTooltipData
        {
            public string skillName = "";
            public string description = "";
            public string additionalInfo = "";
            public string duration = "";
            public string reflectPercent = "";
            public string staminaCost = "";
            public string cooldown = "";
            public string skillType = "";
            public string requirement = "";
            public string confirmation = "";
            public string specialNote = "";
        }

        /// <summary>
        /// 수호자의 진심 툴팁 생성
        /// </summary>
        public static string GetMaceStep7GuardianHeartTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep7GuardianHeartTooltip() 호출됨");

                // Config에서 동적 설정값 가져오기
                float duration = Mace_Config.GuardianHeartDurationValue;
                float reflectPercent = Mace_Config.GuardianHeartReflectPercentValue;
                float staminaCost = Mace_Config.GuardianHeartStaminaCostValue;
                float cooldown = Mace_Config.GuardianHeartCooldownValue;
                int requiredPoints = Mace_Config.GuardianHeartRequiredPointsValue;

                Plugin.Log.LogDebug($"[수호자의 진심 툴팁] 컨피그 값들 - 지속시간: {duration}초, 반사: {reflectPercent}%, 스태미나: {staminaCost}, 쿨타임: {cooldown}초");

                // 상세 툴팁 데이터 생성
                var data = new GuardianHeartTooltipData
                {
                    skillName = "수호자의 진심",
                    description = $"{duration:F0}초간 방어 버프 활성화",
                    additionalInfo = $"받는 데미지의 {reflectPercent:F0}%를 공격자에게 반사",
                    duration = $"{duration:F0}초",
                    reflectPercent = $"{reflectPercent:F0}%",
                    staminaCost = $"{staminaCost:F0}",
                    cooldown = $"{cooldown:F0}초",
                    skillType = "액티브 스킬 - G키",
                    requirement = "둔기 + 방패 착용",
                    confirmation = "같은 무기 전문가 내에서만 다중 습득 가능",
                    specialNote = $"버프 지속 중 방어력 증가 및 데미지 감소 효과 적용\n\n<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color>"
                };

                string finalTooltip = GenerateGuardianHeartTooltip(data);
                Plugin.Log.LogDebug($"[수호자의 진심 툴팁] 최종 툴팁 생성 완료 - 길이: {finalTooltip?.Length ?? 0}");
                return finalTooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetGuardianHeartFallbackTooltip();
            }
        }

        /// <summary>
        /// 수호자의 진심 툴팁 생성 (G키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지/효과 → 범위 → 소모 → 스킬유형(G키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        private static string GenerateGuardianHeartTooltip(GuardianHeartTooltipData data)
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

                // 3. 효과 - 버프 지속시간 + 데미지 반사 (#FF6B6B / #FFB6C1)
                if (!string.IsNullOrEmpty(data.duration) || !string.IsNullOrEmpty(data.reflectPercent))
                {
                    string effectText = "";
                    if (!string.IsNullOrEmpty(data.duration))
                        effectText += $"버프 {data.duration}";
                    if (!string.IsNullOrEmpty(data.reflectPercent))
                    {
                        if (!string.IsNullOrEmpty(effectText)) effectText += ", ";
                        effectText += $"데미지 반사 {data.reflectPercent}";
                    }
                    tooltip += $"<color=#FF6B6B><size=16>효과: </size></color><color=#FFB6C1><size=16>{effectText}</size></color>\n";
                }

                // 4. 범위 - 생략 (자기 자신)

                // 5. 소모 (#FFB347 / #FFDAB9)
                if (!string.IsNullOrEmpty(data.staminaCost))
                {
                    tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {data.staminaCost}</size></color>\n";
                }

                // 6. 스킬유형 (G키 강조: #FF4500 / #00FF00)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#FF4500><size=16>스킬유형: </size></color><color=#00FF00><size=16>{data.skillType}</size></color>\n";
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

                // 10. 필요포인트 - specialNote에 포함되어 있음
                if (!string.IsNullOrEmpty(data.specialNote))
                {
                    tooltip += $"<color=#DDA0DD><size=16>특별안내: </size></color><color=#E6E6FA><size=16>{data.specialNote}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심 툴팁] 구조화된 툴팁 생성 실패: {ex.Message}");
                return GetGuardianHeartFallbackTooltip();
            }
        }

        /// <summary>
        /// 수호자의 진심 백업 툴팁 (오류 시 사용)
        /// </summary>
        private static string GetGuardianHeartFallbackTooltip()
        {
            int requiredPoints = Mace_Config.GuardianHeartRequiredPointsValue;

            return "<color=#FFD700><size=22>수호자의 진심</size></color>\n\n" +
                   "<color=#E0E0E0><size=16>G키: 45초간 방어 버프 활성화 (받는 데미지의 6%를 공격자에게 반사)\n\n" +
                   "• 버프 지속시간: 45초\n" +
                   "• 데미지 반사: 6%\n" +
                   "• 소모: 스태미나 25\n" +
                   "• 쿨타임: 120초\n" +
                   "• 필요조건: 둔기 착용\n\n" +
                   "스킬유형: 액티브 스킬 - G키\n\n" +
                   $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color></size></color>";
        }

        // [DEPRECATED] - Use MeleeTooltipUtils.GenerateTooltip() instead
        private static string GenerateMaceTooltip(MeleeTooltipUtils.MeleeTooltipData data)
        {
            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        #region 스킬 매핑

        /// <summary>
        /// 둔기 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> MaceSkillMappings = new()
        {
            { "mace_Step1_damage", GetMaceExpertTooltip },
            { "mace_Step2_stun_boost", GetMaceStep2StunBoostTooltip },
            { "mace_Step3_branch_guard", GetMaceStep3BranchGuardTooltip },
            { "mace_Step3_branch_heavy", GetMaceStep3BranchHeavyTooltip },
            { "mace_Step4_push", GetMaceStep4PushTooltip },
            { "mace_Step5_tank", GetMaceStep5TankTooltip },
            { "mace_Step5_dps", GetMaceStep5DpsTooltip },
            { "mace_Step6_grandmaster", GetMaceStep6GrandmasterTooltip },
            { "mace_Step7_fury_hammer", GetMaceStep7FuryHammerTooltip },
            { "mace_Step7_guardian_heart", GetMaceStep7GuardianHeartTooltip }
        };

        #endregion

        /// <summary>
        /// 모든 둔기 스킬 툴팁 업데이트
        /// </summary>
        public static void UpdateMaceTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(MaceSkillMappings, MeleeTooltipUtils.WeaponType.Mace);
        }

        // MeleeTooltipUtils.UpdateSkillTooltip() 사용
        // 기존 UpdateIndividualMaceTooltip() 제거

        /// <summary>
        /// 특정 둔기 스킬 툴팁 가져오기
        /// </summary>
        public static string GetMaceSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, MaceSkillMappings, MeleeTooltipUtils.WeaponType.Mace);
        }
    }
}
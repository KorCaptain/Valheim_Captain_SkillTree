using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 버서커 직업 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// 로그 시스템과 동일한 구조로 구현
    /// </summary>
    public static class Berserker_Tooltip
    {
        /// <summary>
        /// 버서커 툴팁 데이터 구조체
        /// </summary>
        public struct BerserkerTooltipData
        {
            public string skillName;          // 스킬명 (예: \"버서커 분노\")
            public string description;        // 설명 (예: \"20초 동안 체력의 -1% 비례 데미지 +2%\")
            public string additionalInfo;     // 추가 정보 (예: \"체력이 적을수록 더 강한 데미지\")
            public string range;             // 범위 (예: \"자신\")
            public string consumeStamina;    // 스태미나 소모 (예: \"20\")
            public string consumeArrow;      // 화살 소모 (버서커는 \"없음\")
            public string skillType;         // 스킬 유형 (예: \"액티브 버프 스킬 - Y키\")
            public string cooldown;          // 쿨타임 (예: \"45초\")
            public string requirement;       // 필요조건 (예: \"직업 버서커\")
            public string confirmation;      // 확인사항 (예: \"직업은 1개만 선택가능, Lv 10 이상\")
            public string requiredItem;      // 필요 아이템 (예: \"고대 바크 스피어\")
        }

        /// <summary>
        /// 버서커 구조화된 툴팁 생성 (힐 스킬과 동일한 패턴)
        /// </summary>
        public static string GetBerserkerTooltip()
        {
            Plugin.Log.LogDebug("[버서커 툴팁] GetBerserkerTooltip() 호출됨");

            try
            {
                // 컨피그에서 실제 값 가져오기
                var duration = Berserker_Config.BerserkerRageDurationValue;
                var cooldown = Berserker_Config.BerserkerRageCooldownValue;
                var staminaCost = Berserker_Config.BerserkerRageStaminaCostValue;
                var damagePerPercent = Berserker_Config.BerserkerRageDamagePerHealthPercentValue;
                var maxDamageBonus = Berserker_Config.BerserkerRageMaxDamageBonusValue;

                Plugin.Log.LogDebug($"[버서커 툴팁] 컨피그 값들 - 지속시간: {duration}초, 쿨타임: {cooldown}초, 스태미나: {staminaCost}, 데미지: {damagePerPercent}%");

                var data = new BerserkerTooltipData
                {
                    skillName = "버서커 분노",
                    description = $"Y키로 {duration}초간 분노 상태 발동하여 강력한 공격력 증가",
                    additionalInfo = $"체력 손실 1%당 데미지 +{damagePerPercent:F0}%, 최대 +{maxDamageBonus:F0}% 한계",
                    range = "자신",
                    consumeStamina = $"{staminaCost:F0}",
                    consumeArrow = "없음",
                    skillType = "액티브 스킬(Y키)",
                    cooldown = $"{cooldown:F0}초",
                    requirement = "직업 버서커",
                    confirmation = "직업은 1개만 선택가능",
                    requiredItem = ""
                };

                string finalTooltip = GenerateBerserkerTooltip(data);
                Plugin.Log.LogDebug($"[버서커 툴팁] 최종 툴팁 생성 완료 - 길이: {finalTooltip?.Length ?? 0}");
                return finalTooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetBerserkerFallbackTooltip();
            }
        }

        /// <summary>
        /// 버서커 구조화된 툴팁 생성 (힐러모드 구조 참조)
        /// </summary>
        public static string GenerateBerserkerTooltip(BerserkerTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 스킬 이름 (황금색, 크기 22)
                if (!string.IsNullOrEmpty(data.skillName))
                {
                    tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";
                }

                // 설명 섹션
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>{data.description}";

                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 분노 효과 섹션
                tooltip += $"<color=#FF6B6B><size=16>⚔️ 분노 효과: </size></color><color=#FF8C82><size=16>체력 손실 1%당 데미지 +{Berserker_Config.BerserkerRageDamagePerHealthPercentValue}%</size></color>\n";

                // 패시브 효과 섹션
                var passiveHealthThreshold = Berserker_Config.BerserkerPassiveHealthThresholdValue;
                var passiveInvincibilityDuration = Berserker_Config.BerserkerPassiveInvincibilityDurationValue;
                var passiveCooldown = Berserker_Config.BerserkerPassiveCooldownValue;
                tooltip += $"<color=#98FB98><size=16>🛡️ 패시브 효과: </size></color><color=#00FF00><size=16>스태미나 재생 +20%, 체력 {passiveHealthThreshold:F0}% 이하 시 {passiveInvincibilityDuration:F0}초간 무적 (쿨타임 {passiveCooldown / 60f:F0}분)</size></color>\n";

                // 영향 범위 섹션
                if (!string.IsNullOrEmpty(data.range))
                {
                    tooltip += $"<color=#87CEEB><size=16>📍 영향 범위: </size></color><color=#B0E0E6><size=16>{data.range}</size></color>\n";
                }

                // 소모 섹션
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    tooltip += $"<color=#FFB347><size=16>⚡ 소모: </size></color><color=#FFDAB9><size=16>스태미나 {data.consumeStamina}</size></color>\n";
                }

                // 스킬 유형 섹션
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#87CEEB><size=16>🔮 스킬유형: </size></color><color=#B0E0E6><size=16>{data.skillType}</size></color>\n";
                }

                // 쿨타임 섹션
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>⏳ 쿨타임: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>✅ 필요조건: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>⚠️ 확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 필요포인트 섹션 추가
                tooltip += $"<color=#87CEEB><size=16>💎 필요포인트: </size></color><color=#FF6B6B><size=16>에이크쉬르 트로피</size></color>";

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        /// <summary>
        /// 버서커 대체 툴팁 (오류 시 사용)
        /// </summary>
        private static string GetBerserkerFallbackTooltip()
        {
            var data = new BerserkerTooltipData
            {
                skillName = "버서커 분노",
                description = $"Y키로 {Berserker_Config.BerserkerRageDurationValue}초간 분노 상태 발동하여 강력한 공격력 증가",
                additionalInfo = $"체력 손실 1%당 데미지 +{Berserker_Config.BerserkerRageDamagePerHealthPercentValue:F0}%, 최대 +{Berserker_Config.BerserkerRageMaxDamageBonusValue:F0}% 한계",
                range = "자신",
                consumeStamina = $"{Berserker_Config.BerserkerRageStaminaCostValue}",
                consumeArrow = "없음",
                skillType = "액티브 스킬(Y키)",
                cooldown = $"{Berserker_Config.BerserkerRageCooldownValue}초",
                requirement = "직업 버서커",
                confirmation = "직업은 1개만 선택가능",
                requiredItem = ""
            };

            return GenerateBerserkerTooltip(data);
        }

        /// <summary>
        /// 포맷된 툴팁 문자열 생성 (표준 형식)
        /// </summary>
        private static string CreateFormattedTooltip(BerserkerTooltipData data)
        {
            try
            {
                var tooltip = $"설명: {data.description}\n";
                
                if (!string.IsNullOrEmpty(data.additionalInfo))
                {
                    tooltip += $"추가: {data.additionalInfo}\n";
                }
                
                tooltip += $"범위: {data.range}\n";
                tooltip += $"소모: 스태미나 {data.consumeStamina}";
                
                if (data.consumeArrow != "없음")
                {
                    tooltip += $", {data.consumeArrow}";
                }
                
                tooltip += $"\n스킬유형: {data.skillType}\n";
                tooltip += $"쿨타임: {data.cooldown}\n";
                tooltip += $"필요조건: {data.requirement}\n";
                tooltip += $"⚠️확인사항: {data.confirmation}\n";
                tooltip += $"⚠️필요 아이템: {data.requiredItem}";
                
                return tooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] CreateFormattedTooltip 실패: {ex.Message}");
                return CreateDefaultTooltip();
            }
        }

        /// <summary>
        /// 기본 툴팁 생성 (오류 시 fallback) - 액티브 + 패시브 포함
        /// </summary>
        private static string CreateDefaultTooltip()
        {
            return "설명:\n" +
                   "[액티브 스킬 - Y키]\n" +
                   "20초 동안 체력의 -1% 비례 데미지 +2%, 체력이 적을수록 더 강한 데미지\n\n" +
                   "[패시브 스킬]\n" +
                   "• 스테미나 리젠 +20% (항상 적용)\n" +
                   "• 체력 10% 이하 시 7초간 무적 (쿨타임 300초)\n\n" +
                   "추가: 최대 +200% 데미지 한계, 빨간/황금 오라 효과\n" +
                   "범위: 자신\n" +
                   "소모: 스태미나 20 (액티브만)\n" +
                   "쿨타임: 45초 (액티브), 300초 (패시브)\n" +
                   "필요조건: 직업 버서커\n" +
                   "⚠️확인사항: 직업은 1개만 선택가능, Lv 10 이상\n" +
                   "⚠️필요 아이템: 고대 바크 스피어";
        }

        /// <summary>
        /// 버서커 툴팁 강제 업데이트 (컨피그 변경 시 호출)
        /// </summary>
        public static void UpdateBerserkerTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[버서커 툴팁] UpdateBerserkerTooltip 호출됨");
                
                // 버서커 툴팁 생성하여 로그만 출력
                string newTooltip = GetBerserkerTooltip();
                Plugin.Log.LogDebug($"[버서커 툴팁] 새 툴팁 생성됨 (길이: {newTooltip.Length}자)");
                
                // 현재는 로그만 출력하고, 향후 스킬트리 새로고침 시스템과 연동될 예정
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] UpdateBerserkerTooltip 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 버서커 툴팁 디버그 정보 출력
        /// </summary>
        public static void LogBerserkerTooltipDebugInfo()
        {
            try
            {
                Plugin.Log.LogInfo("=== [버서커 툴팁] 디버그 정보 ===");
                
                // 현재 툴팁 내용
                string currentTooltip = GetBerserkerTooltip();
                Plugin.Log.LogDebug($"[버서커 툴팁] 현재 툴팁:\n{currentTooltip}");
                
                // 컨피그 값들
                Berserker_Config.LogBerserkerConfigValues();
                
                Plugin.Log.LogInfo("=== [버서커 툴팁] 디버그 정보 종료 ===");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] 디버그 정보 출력 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 실시간 데미지 보너스 계산 툴팁 (현재 플레이어 상태 기반)
        /// </summary>
        public static string GetCurrentDamageBonusTooltip(Player player)
        {
            try
            {
                if (player == null) return "플레이어 정보 없음";

                // 현재 분노 상태 확인
                bool inRage = BerserkerSkills.IsPlayerInRage(player);
                if (!inRage)
                {
                    return "분노 상태가 아님";
                }

                // 현재 데미지 보너스 계산
                float damageBonus = BerserkerSkills.GetRageDamageBonus(player);
                float currentHealthPercent = player.GetHealthPercentage() * 100f;
                
                return $"현재 체력: {currentHealthPercent:F1}%\n" +
                       $"현재 데미지 보너스: +{damageBonus:F1}%\n" +
                       $"분노 상태: 활성";
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] GetCurrentDamageBonusTooltip 실패: {ex.Message}");
                return "계산 오류";
            }
        }

        /// <summary>
        /// 예상 데미지 보너스 계산 툴팁 (체력별 시뮬레이션)
        /// </summary>
        public static string GetDamageBonusSimulationTooltip()
        {
            try
            {
                var damagePerPercent = Berserker_Config.BerserkerRageDamagePerHealthPercentValue;
                var maxBonus = Berserker_Config.BerserkerRageMaxDamageBonusValue;

                string simulation = "📊 체력별 데미지 보너스 예상:\n";
                
                // 체력 100%, 75%, 50%, 25%, 1%일 때의 보너스 계산
                int[] healthLevels = { 100, 75, 50, 25, 1 };
                
                foreach (int health in healthLevels)
                {
                    float lostHealth = 100f - health;
                    float bonus = lostHealth * damagePerPercent;
                    bonus = Mathf.Min(bonus, maxBonus);
                    
                    simulation += $"체력 {health}%: +{bonus:F0}% 데미지\n";
                }
                
                simulation += $"최대 한계: +{maxBonus:F0}%";
                
                return simulation;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 툴팁] GetDamageBonusSimulationTooltip 실패: {ex.Message}");
                return "시뮬레이션 오류";
            }
        }
    }
}
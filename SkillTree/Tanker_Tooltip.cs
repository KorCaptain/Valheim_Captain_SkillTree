using System;
using System.Collections.Generic;
using UnityEngine;

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
                
                // HTML 스타일 구조화된 툴팁 생성 (이미지와 동일한 스타일)
                var tooltip = "<color=#FFD700><size=22>탱커 - 전장의 함성</size></color>\n\n";
                
                // 액티브 스킬 설명
                tooltip += "<color=#90EE90><size=16>설명: </size></color><color=#E0E0E0><size=16>";
                tooltip += $"{range}m 범위 적을 도발해 {duration}초 동안 나를 공격하게 만듭니다.";
                tooltip += $"(보스 {bossDuration}초), 시전자는 {buffDuration}초 동안 피해감소 {activeDamageReduction}%";
                tooltip += "</size></color>\n";
                
                // 범위 정보
                tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>{range}m</size></color>\n";
                
                // 소모 자원
                tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스테미나 {stamina}</size></color>\n";
                
                // 스킬 유형
                tooltip += "<color=#DDA0DD><size=16>스킬유형: </size></color><color=#E6E6FA><size=16>액티브 도발 스킬 - Y키</size></color>\n";
                
                // 쿨타임
                tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{cooldown}초</size></color>\n";
                
                // 패시브 효과 추가
                tooltip += $"<color=#98FB98><size=16>패시브 효과: </size></color><color=#00FF00><size=16>받는 피해량 -{passiveDamageReduction}%</size></color>\n";
                
                // 필요조건
                tooltip += "<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>방패 착용</size></color>\n";
                
                // 확인사항
                tooltip += "<color=#F0E68C><size=16>⚠️확인사항: </size></color><color=#FFE4B5><size=16>직업은 1개만 선택가능, Lv 10 이상</size></color>\n";
                
                // 필요포인트
                tooltip += "<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>에이크쉬르 트로피</size></color>";
                
                return tooltip;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[탱커 툴팁] GetTankerTooltip 실패: {ex.Message}");
                return "탱커 스킬 정보를 불러올 수 없음";
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

                // 스킬 이름 (황금색, 크기 22 - 기존 툴팁과 동일)
                tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";

                // 설명 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#90EE90><size=16>설명: </size></color><color=#E0E0E0><size=16>{data.description}";
                    
                    // 추가 정보가 있으면 괄호로 추가
                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $"( {data.additionalInfo} )";
                    }
                    tooltip += "</size></color>\n";
                }

                // 범위 섹션 (기존 툴팁 스타일)
                tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>{data.range}</size></color>\n";

                // 소모 섹션 - 구조화된 표시 (기존 툴팁 스타일)
                var consumeParts = new List<string>();
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    consumeParts.Add($"스테미나 {data.consumeStamina}");
                }
                if (!string.IsNullOrEmpty(data.consumeArrow) && data.consumeArrow != "없음")
                {
                    consumeParts.Add($"화살 {data.consumeArrow} 개");
                }

                if (consumeParts.Count > 0)
                {
                    tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>{string.Join(", ", consumeParts)}</size></color>\n";
                }

                // 스킬 유형 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#DDA0DD><size=16>스킬유형: </size></color><color=#E6E6FA><size=16>{data.skillType}</size></color>\n";
                }

                // 쿨타임 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>⚠️확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 필요포인트 섹션 (기존 툴팁 스타일)
                if (!string.IsNullOrEmpty(data.requiredItem))
                {
                    tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>{data.requiredItem}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[탱커 툴팁] 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        /// <summary>
        /// 탱커 대체 툴팁 (설정 로드 실패 시)
        /// </summary>
        private static string GetTankerFallbackTooltip()
        {
            var tooltipData = new TankerTooltipData
            {
                skillName = "탱커 - 전장의 함성",
                description = "적을 도발해 5초 동안 나를 공격하게 만듭니다.(보스 1초), 시전자는 피해감소 20%가 5초 지속",
                additionalInfo = "",
                range = "12m",
                consumeStamina = "25",
                consumeArrow = "없음",
                skillType = "액티브 버프 스킬 - Y키",
                cooldown = "60초",
                requirement = "방패 착용",
                confirmation = "직업은 1개만 선택가능, Lv 10 이상",
                requiredItem = "에이크쉬르 트로피"
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
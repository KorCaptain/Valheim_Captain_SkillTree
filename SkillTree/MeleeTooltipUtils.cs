using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 근접 무기 툴팁 시스템 공통 유틸리티
    /// 모든 무기 타입의 중복 코드를 통합하여 효율성 향상
    /// </summary>
    public static class MeleeTooltipUtils
    {
        /// <summary>
        /// 공통 근접 무기 툴팁 데이터 구조체
        /// 모든 무기 타입에서 사용 가능
        /// </summary>
        public struct MeleeTooltipData
        {
            public string skillName;          // 스킬명
            public string description;        // 설명
            public string additionalInfo;     // 추가 정보
            public string consumeStamina;     // 스태미나 소모
            public string skillType;          // 스킬 유형
            public string cooldown;           // 쿨타임
            public string requirement;        // 필요조건
            public string requiredPoints;     // 필요 포인트
            public string confirmation;       // 확인사항
        }

        /// <summary>
        /// 무기 타입 열거형
        /// </summary>
        public enum WeaponType
        {
            Knife,    // 단검
            Sword,    // 검
            Polearm,  // 폴암
            Spear,    // 창
            Mace      // 둔기
        }

        /// <summary>
        /// 무기 타입별 한글 이름 매핑
        /// </summary>
        private static readonly Dictionary<WeaponType, string> WeaponNames = new()
        {
            { WeaponType.Knife, "단검" },
            { WeaponType.Sword, "검" },
            { WeaponType.Polearm, "폴암" },
            { WeaponType.Spear, "창" },
            { WeaponType.Mace, "둔기" }
        };

        /// <summary>
        /// 공통 근접 무기 툴팁 생성 함수
        /// 모든 무기 타입에서 사용 가능한 통합 툴팁 생성기
        /// </summary>
        /// <param name="data">툴팁 데이터</param>
        /// <param name="weaponType">무기 타입 (로깅용)</param>
        /// <returns>생성된 HTML 형식 툴팁</returns>
        public static string GenerateTooltip(MeleeTooltipData data, WeaponType weaponType)
        {
            try
            {
                var tooltip = "";

                // 스킬명 섹션
                if (!string.IsNullOrEmpty(data.skillName))
                {
                    tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";
                }

                // 설명 섹션
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>{data.description}</size></color>\n";
                }

                // 소모 섹션 (있을 때만 표시)
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스테미나 {data.consumeStamina}</size></color>\n";
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
                else
                {
                    Plugin.Log.LogWarning($"[MeleeTooltip] 필요조건이 비어있음: skillName='{data.skillName}'");
                }

                // 스킬 유형 섹션 (패시브/액티브)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#87CEEB><size=16>스킬유형: </size></color><color=#B0E0E6><size=16>{data.skillType}</size></color>\n";
                }

                // 필요 포인트 섹션
                if (!string.IsNullOrEmpty(data.requiredPoints))
                {
                    tooltip += $"<color=#DDA0DD><size=16>필요 포인트: </size></color><color=#E6E6FA><size=16>{data.requiredPoints}</size></color>\n";
                }

                // 확인사항 섹션
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>⚠️확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
                }

                LogTooltipGeneration(weaponType, "GenerateTooltip", "성공");
                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                LogTooltipError(weaponType, "GenerateTooltip", ex.Message);
                return "툴팁 생성 오류";
            }
        }

        /// <summary>
        /// 개별 스킬 툴팁 업데이트 공통 함수
        /// </summary>
        /// <param name="skillId">업데이트할 스킬 ID</param>
        /// <param name="newTooltip">새로운 툴팁 텍스트</param>
        /// <param name="weaponType">무기 타입 (로깅용)</param>
        public static void UpdateSkillTooltip(string skillId, string newTooltip, WeaponType weaponType)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey(skillId))
                {
                    var skillNode = manager.SkillNodes[skillId];
                    skillNode.Description = newTooltip;
                    
                    Plugin.Log.LogDebug($"[{WeaponNames[weaponType]} 툴팁] {skillId} 업데이트 완료");
                }
                else
                {
                    Plugin.Log.LogWarning($"[{WeaponNames[weaponType]} 툴팁] {skillId} 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception ex)
            {
                LogTooltipError(weaponType, "UpdateSkillTooltip", $"{skillId} 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 다중 스킬 툴팁 업데이트 공통 함수
        /// </summary>
        /// <param name="tooltipMappings">스킬ID -> 툴팁 매핑</param>
        /// <param name="weaponType">무기 타입</param>
        public static void UpdateMultipleTooltips(Dictionary<string, Func<string>> tooltipMappings, WeaponType weaponType)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes == null)
                {
                    Plugin.Log.LogWarning($"[{WeaponNames[weaponType]} 툴팁] SkillTreeManager가 초기화되지 않음");
                    return;
                }

                foreach (var mapping in tooltipMappings)
                {
                    var skillId = mapping.Key;
                    var tooltipFunc = mapping.Value;
                    var newTooltip = tooltipFunc.Invoke();
                    
                    UpdateSkillTooltip(skillId, newTooltip, weaponType);
                }

            }
            catch (System.Exception ex)
            {
                LogTooltipError(weaponType, "UpdateMultipleTooltips", $"전체 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 스킬 ID 기반 툴팁 조회 공통 함수
        /// </summary>
        /// <param name="skillId">조회할 스킬 ID</param>
        /// <param name="tooltipMappings">스킬ID -> 툴팁 매핑</param>
        /// <param name="weaponType">무기 타입</param>
        /// <returns>해당 스킬의 툴팁</returns>
        public static string GetSkillTooltip(string skillId, Dictionary<string, Func<string>> tooltipMappings, WeaponType weaponType)
        {
            try
            {
                if (tooltipMappings.TryGetValue(skillId, out var tooltipFunc))
                {
                    return tooltipFunc.Invoke();
                }
                else
                {
                    Plugin.Log.LogWarning($"[{WeaponNames[weaponType]} 툴팁] 알 수 없는 스킬 ID: {skillId}");
                    return $"알 수 없는 {WeaponNames[weaponType]} 스킬";
                }
            }
            catch (System.Exception ex)
            {
                LogTooltipError(weaponType, "GetSkillTooltip", $"{skillId} 툴팁 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        /// <summary>
        /// 무기별 기본 필요조건 생성
        /// </summary>
        /// <param name="weaponType">무기 타입</param>
        /// <returns>해당 무기의 필요조건 텍스트</returns>
        public static string GetWeaponRequirement(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.Knife => "단검 착용",
                WeaponType.Sword => "검 착용",
                WeaponType.Polearm => "폴암 착용",
                WeaponType.Spear => "창 착용",
                WeaponType.Mace => "둔기 착용",
                _ => "무기 착용"
            };
        }

        /// <summary>
        /// 툴팁 생성 로깅
        /// </summary>
        private static void LogTooltipGeneration(WeaponType weaponType, string methodName, string status)
        {
            Plugin.Log.LogDebug($"[{WeaponNames[weaponType]} 툴팁] {methodName}() {status}");
        }

        /// <summary>
        /// 툴팁 오류 로깅
        /// </summary>
        private static void LogTooltipError(WeaponType weaponType, string methodName, string errorMessage)
        {
            Plugin.Log.LogError($"[{WeaponNames[weaponType]} 툴팁] {methodName} 오류: {errorMessage}");
        }

        /// <summary>
        /// 패시브 스킬용 기본 툴팁 데이터 생성
        /// </summary>
        /// <param name="skillName">스킬명</param>
        /// <param name="description">설명</param>
        /// <param name="weaponType">무기 타입</param>
        /// <returns>기본 패시브 스킬 툴팁 데이터</returns>
        public static MeleeTooltipData CreatePassiveSkillData(string skillName, string description, WeaponType weaponType)
        {
            return new MeleeTooltipData
            {
                skillName = skillName,
                description = description,
                additionalInfo = "",
                consumeStamina = "",
                skillType = "패시브 스킬",
                cooldown = "",
                requirement = GetWeaponRequirement(weaponType),
                requiredPoints = "",
                confirmation = ""
            };
        }

        /// <summary>
        /// 액티브 스킬용 기본 툴팁 데이터 생성
        /// </summary>
        /// <param name="skillName">스킬명</param>
        /// <param name="description">설명</param>
        /// <param name="consumeStamina">스태미나 소모</param>
        /// <param name="cooldown">쿨타임</param>
        /// <param name="weaponType">무기 타입</param>
        /// <param name="additionalInfo">추가 정보</param>
        /// <param name="confirmation">확인사항</param>
        /// <param name="keyBinding">키 바인딩 (예: "G키", "T키")</param>
        /// <returns>기본 액티브 스킬 툴팁 데이터</returns>
        public static MeleeTooltipData CreateActiveSkillData(string skillName, string description,
            string consumeStamina, string cooldown, WeaponType weaponType,
            string additionalInfo = "", string confirmation = "", string keyBinding = "")
        {
            string skillTypeText = "액티브 스킬";
            if (!string.IsNullOrEmpty(keyBinding))
            {
                skillTypeText = $"액티브 스킬({keyBinding})";
            }

            return new MeleeTooltipData
            {
                skillName = skillName,
                description = description,
                additionalInfo = additionalInfo,
                consumeStamina = consumeStamina,
                skillType = skillTypeText,
                cooldown = cooldown,
                requirement = GetWeaponRequirement(weaponType),
                requiredPoints = "",
                confirmation = confirmation
            };
        }
    }
}
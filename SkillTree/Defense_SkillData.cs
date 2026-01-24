using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 방어 스킬 툴팁 시스템 - Config 연동
    /// 수호자의 진심 등 방어 스킬의 동적 툴팁 생성
    /// 힐러모드 툴팁과 동일한 구조 및 색상 시스템 적용
    /// </summary>
    public static class Defense_SkillData
    {
        /// <summary>
        /// 수호자의 진심 툴팁 데이터 구조체
        /// </summary>
        public struct GuardianHeartTooltipData
        {
            public string skillName;          // 스킬명
            public string description;        // 설명
            public string additionalInfo;     // 추가 정보
            public string duration;          // 지속시간
            public string cooldown;          // 쿨타임
            public string consumeStamina;    // 스태미나 소모
            public string reflectPercentage; // 반사 데미지 비율
            public string skillType;         // 스킬 유형
            public string requirement;       // 필요조건
            public string specialNote;       // 특별 안내
        }




        /// <summary>
        /// 모든 방어 스킬 툴팁 업데이트 (Config 변경 시 호출)
        /// </summary>
        public static void UpdateDefenseTooltips()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes == null)
                {
                    Plugin.Log.LogWarning("[방어 스킬 데이터] 스킬 매니저가 없어 툴팁 업데이트 불가");
                    return;
                }


                Plugin.Log.LogDebug("[방어 스킬 데이터] 모든 방어 스킬 툴팁 업데이트 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[방어 스킬 데이터] 툴팁 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 방어 스킬 시스템 초기화
        /// </summary>
        public static void InitializeDefenseSkills()
        {
            try
            {
                Plugin.Log.LogDebug("[방어 스킬 데이터] 방어 스킬 시스템 초기화 시작");

                // Config 초기화 확인
                if (Mace_Config.GuardianHeartCooldown == null)
                {
                    Plugin.Log.LogWarning("[방어 스킬 데이터] Mace_Config가 초기화되지 않음");
                    return;
                }

                // 툴팁 업데이트
                UpdateDefenseTooltips();

                Plugin.Log.LogDebug("[방어 스킬 데이터] 방어 스킬 시스템 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[방어 스킬 데이터] 방어 스킬 시스템 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 디버그용 방어 스킬 설정값 출력
        /// </summary>
        public static void LogDefenseSkillValues()
        {
            try
            {
                Plugin.Log.LogInfo($"[방어 스킬 데이터] === 수호자의 진심 현재 설정값 ===");
                Plugin.Log.LogInfo($"[방어 스킬 데이터] 지속시간: {Mace_Config.GuardianHeartDurationValue}초");
                Plugin.Log.LogInfo($"[방어 스킬 데이터] 반사 데미지: {Mace_Config.GuardianHeartReflectPercentValue}%");
                Plugin.Log.LogInfo($"[방어 스킬 데이터] 스태미나 소모: {Mace_Config.GuardianHeartStaminaCostValue}");
                Plugin.Log.LogInfo($"[방어 스킬 데이터] 쿨타임: {Mace_Config.GuardianHeartCooldownValue}초");
                Plugin.Log.LogInfo($"[방어 스킬 데이터] 필요 포인트: {Mace_Config.GuardianHeartRequiredPointsValue}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[방어 스킬 데이터] 설정값 출력 실패: {ex.Message}");
            }
        }
    }
}
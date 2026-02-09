using System;
using System.Collections.Generic;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain Exp Table
    /// EpicMMO의 경험치 테이블 계산 공식을 그대로 복제
    /// LevelSystem.cs 536-562줄 참조
    /// </summary>
    public static class CaptainExpTable
    {
        /// <summary>
        /// 레벨별 필요 경험치 테이블
        /// Key: 레벨, Value: 해당 레벨 도달에 필요한 경험치
        /// </summary>
        private static Dictionary<int, long> levelsExp = new Dictionary<int, long>();

        /// <summary>
        /// 테이블 초기화 여부
        /// </summary>
        private static bool isInitialized = false;

        /// <summary>
        /// 경험치 테이블 초기화/재계산
        /// JSON 파일 우선, 없으면 Config 값 사용
        /// </summary>
        public static void FillLevelsExp()
        {
            try
            {
                levelsExp.Clear();

                // JSON 파일 확인
                var jsonData = CaptainExpJsonLoader.LoadLevelExp();

                int levelExp;
                float multiply;
                int maxLevel;
                bool useAccumulative;

                if (jsonData != null)
                {
                    // JSON 데이터 사용
                    levelExp = jsonData.baseExp;
                    multiply = jsonData.multiplier;
                    maxLevel = jsonData.maxLevel;
                    useAccumulative = jsonData.useAccumulative;

                    // 커스텀 레벨이 있으면 직접 사용
                    if (jsonData.customLevels != null && jsonData.customLevels.Count > 0)
                    {
                        levelsExp[1] = 0;
                        foreach (var entry in jsonData.customLevels)
                        {
                            levelsExp[entry.level] = entry.expRequired;
                        }
                        isInitialized = true;
                        Plugin.Log.LogDebug($"[CaptainExpTable] JSON 커스텀 레벨 테이블 로드 ({jsonData.customLevels.Count}개)");
                        LogFirstLevels(5);
                        return;
                    }
                }
                else
                {
                    // Config 값 사용
                    levelExp = CaptainLevelConfig.LevelExp.Value;
                    multiply = CaptainLevelConfig.MultiNextLevel.Value;
                    maxLevel = CaptainLevelConfig.MaxLevel.Value;
                    useAccumulative = CaptainLevelConfig.LevelExpForEachLevel.Value;
                }

                // 레벨 1의 필요 경험치는 기본값
                levelsExp[1] = 0; // 레벨 1은 시작 레벨이므로 0

                if (useAccumulative)
                {
                    // 누적 방식: current = current * multiply + levelExp
                    long current = 0;
                    for (int i = 1; i <= maxLevel; i++)
                    {
                        current = (long)Math.Round(current * multiply + levelExp);
                        levelsExp[i + 1] = current;
                    }
                }
                else
                {
                    // 배수 방식: current = current * multiply
                    long current = levelExp;
                    for (int i = 1; i <= maxLevel; i++)
                    {
                        levelsExp[i + 1] = current;
                        current = (long)Math.Round(current * multiply);
                    }
                }

                isInitialized = true;
                Plugin.Log.LogDebug($"[CaptainExpTable] 경험치 테이블 초기화 완료 (최대 레벨: {maxLevel})");

                // 처음 몇 레벨 로그 출력 (디버깅용)
                LogFirstLevels(5);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainExpTable] 경험치 테이블 초기화 실패: {ex.Message}");
                isInitialized = false;
            }
        }

        /// <summary>
        /// 특정 레벨에 필요한 경험치 반환
        /// </summary>
        /// <param name="level">목표 레벨</param>
        /// <returns>필요 경험치</returns>
        public static long GetExpForLevel(int level)
        {
            if (!isInitialized)
            {
                FillLevelsExp();
            }

            if (levelsExp.TryGetValue(level, out var exp))
            {
                return exp;
            }

            // 테이블에 없는 레벨인 경우 (최대 레벨 초과)
            if (level > CaptainLevelConfig.MaxLevel.Value)
            {
                return long.MaxValue; // 더 이상 레벨업 불가
            }

            // 레벨 1 이하
            if (level <= 1)
            {
                return 0;
            }

            // 예외적인 경우 기본값 계산
            Plugin.Log.LogWarning($"[CaptainExpTable] 경험치 테이블에 없는 레벨: {level}");
            return (long)(CaptainLevelConfig.LevelExp.Value * Math.Pow(CaptainLevelConfig.MultiNextLevel.Value, level - 1));
        }

        /// <summary>
        /// 테이블 재초기화 (Config 변경 시)
        /// </summary>
        public static void Reinitialize()
        {
            isInitialized = false;
            FillLevelsExp();
        }

        /// <summary>
        /// 현재 레벨에서 다음 레벨까지 필요한 경험치 반환
        /// </summary>
        /// <param name="currentLevel">현재 레벨</param>
        /// <returns>다음 레벨까지 필요한 경험치</returns>
        public static long GetExpToNextLevel(int currentLevel)
        {
            return GetExpForLevel(currentLevel + 1);
        }

        /// <summary>
        /// 총 경험치로 레벨 계산
        /// </summary>
        /// <param name="totalExp">총 경험치</param>
        /// <returns>계산된 레벨</returns>
        public static int GetLevelFromTotalExp(long totalExp)
        {
            if (!isInitialized)
            {
                FillLevelsExp();
            }

            int level = 1;
            long accumulatedExp = 0;

            for (int i = 2; i <= CaptainLevelConfig.MaxLevel.Value + 1; i++)
            {
                long expNeeded = GetExpForLevel(i);
                if (accumulatedExp + expNeeded > totalExp)
                {
                    break;
                }
                accumulatedExp += expNeeded;
                level = i - 1;
            }

            return Math.Min(level, CaptainLevelConfig.MaxLevel.Value);
        }

        /// <summary>
        /// 특정 레벨까지 필요한 총 경험치 반환
        /// </summary>
        /// <param name="targetLevel">목표 레벨</param>
        /// <returns>총 필요 경험치</returns>
        public static long GetTotalExpForLevel(int targetLevel)
        {
            if (!isInitialized)
            {
                FillLevelsExp();
            }

            long total = 0;
            for (int i = 2; i <= targetLevel; i++)
            {
                total += GetExpForLevel(i);
            }
            return total;
        }

        /// <summary>
        /// 레벨업 진행률 계산 (0.0 ~ 1.0)
        /// </summary>
        /// <param name="currentLevel">현재 레벨</param>
        /// <param name="currentExp">현재 레벨의 경험치</param>
        /// <returns>진행률 (0.0 ~ 1.0)</returns>
        public static float GetLevelProgress(int currentLevel, long currentExp)
        {
            long expForNextLevel = GetExpForLevel(currentLevel + 1);
            if (expForNextLevel <= 0)
            {
                return 1.0f; // 최대 레벨
            }
            return (float)currentExp / expForNextLevel;
        }

        /// <summary>
        /// 처음 몇 레벨의 경험치 테이블 로그 출력 (디버깅용)
        /// </summary>
        private static void LogFirstLevels(int count)
        {
            Plugin.Log.LogDebug("=== Exp Table (first levels) ===");
            for (int i = 2; i <= Math.Min(count + 1, CaptainLevelConfig.MaxLevel.Value); i++)
            {
                Plugin.Log.LogDebug($"Level {i}: {GetExpForLevel(i):N0} exp");
            }
            Plugin.Log.LogDebug("================================");
        }

        /// <summary>
        /// 전체 경험치 테이블 로그 출력 (디버깅용)
        /// </summary>
        public static void LogFullTable()
        {
            if (!isInitialized)
            {
                FillLevelsExp();
            }

            Plugin.Log.LogInfo("=== Full Exp Table ===");
            foreach (var kvp in levelsExp)
            {
                Plugin.Log.LogInfo($"Level {kvp.Key}: {kvp.Value:N0} exp");
            }
            Plugin.Log.LogInfo("======================");
        }
    }
}

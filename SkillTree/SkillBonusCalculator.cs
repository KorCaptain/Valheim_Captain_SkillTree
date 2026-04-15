using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 스킬 보너스 계산용 공통 헬퍼 클래스
    /// 여러 스킬 파일에서 중복되는 보너스 계산 로직을 공통화
    /// </summary>
    public static class SkillBonusCalculator
    {
        // 성능 최적화: 프레임 단위 캐시
        // GetDamage/GetTooltip 등 15+ 패치 체인이 한 프레임에 실행될 때 중복 계산 방지
        private static int _lastCacheFrame = -1;
        private static readonly Dictionary<string, float> _frameCache = new Dictionary<string, float>();
        // 캐시 키 생성용 StringBuilder 재사용 (Unity 메인스레드 단일 호출 보장)
        private static readonly StringBuilder _keyBuilder = new StringBuilder(128);

        /// <summary>
        /// 여러 스킬의 보너스를 합산하여 총합 반환
        /// </summary>
        /// <param name="bonuses">스킬ID와 해당 값을 반환하는 함수의 튜플 배열</param>
        /// <returns>활성화된 스킬들의 보너스 합계</returns>
        /// <example>
        /// var totalBonus = SkillBonusCalculator.CalculateTotal(
        ///     ("mace_expert_damage", () => Mace_Config.MaceExpertDamageBonusValue),
        ///     ("mace_Step1_damage", () => Mace_Config.MaceStep1DamageBonusValue)
        /// );
        /// </example>
        public static float CalculateTotal(params (string skillId, Func<float> getValue)[] bonuses)
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return 0f;

            // 프레임 캐시: 새 프레임 시작 시 초기화
            int currentFrame = UnityEngine.Time.frameCount;
            if (currentFrame != _lastCacheFrame)
            {
                _lastCacheFrame = currentFrame;
                _frameCache.Clear();
            }

            // 캐시 키: StringBuilder 재사용으로 string 할당 최소화
            _keyBuilder.Clear();
            foreach (var (skillId, _) in bonuses)
            {
                _keyBuilder.Append(skillId);
                _keyBuilder.Append('|');
            }
            string key = _keyBuilder.ToString();
            if (_frameCache.TryGetValue(key, out float cached))
                return cached;

            float total = 0f;
            foreach (var (skillId, getValue) in bonuses)
            {
                if (manager.GetSkillLevel(skillId) > 0)
                {
                    total += getValue();
                }
            }
            _frameCache[key] = total;
            return total;
        }

        /// <summary>
        /// 여러 스킬의 보너스를 합산하여 총합 반환 (리스트 버전)
        /// </summary>
        public static float CalculateTotal(IEnumerable<(string skillId, Func<float> getValue)> bonuses)
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return 0f;

            // 프레임 캐시 적용
            int currentFrame = UnityEngine.Time.frameCount;
            if (currentFrame != _lastCacheFrame)
            {
                _lastCacheFrame = currentFrame;
                _frameCache.Clear();
            }

            _keyBuilder.Clear();
            _keyBuilder.Append("list|");
            foreach (var (skillId, _) in bonuses)
            {
                _keyBuilder.Append(skillId);
                _keyBuilder.Append('|');
            }
            string key = _keyBuilder.ToString();
            if (_frameCache.TryGetValue(key, out float cached))
                return cached;

            float total = 0f;
            foreach (var (skillId, getValue) in bonuses)
            {
                if (manager.GetSkillLevel(skillId) > 0)
                    total += getValue();
            }
            _frameCache[key] = total;
            return total;
        }

        /// <summary>
        /// 단일 스킬의 보너스 반환 (스킬이 활성화되지 않으면 0 반환)
        /// </summary>
        /// <param name="skillId">스킬 ID</param>
        /// <param name="getValue">값을 반환하는 함수</param>
        /// <returns>스킬이 활성화되어 있으면 값, 아니면 0</returns>
        public static float GetIfActive(string skillId, Func<float> getValue)
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return 0f;

            return manager.GetSkillLevel(skillId) > 0 ? getValue() : 0f;
        }

        /// <summary>
        /// 스킬 레벨에 따른 보너스 반환 (레벨당 보너스 방식)
        /// </summary>
        /// <param name="skillId">스킬 ID</param>
        /// <param name="bonusPerLevel">레벨당 보너스 값</param>
        /// <returns>스킬 레벨 * 레벨당 보너스</returns>
        public static float GetLevelBasedBonus(string skillId, float bonusPerLevel)
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return 0f;

            int level = manager.GetSkillLevel(skillId);
            return level * bonusPerLevel;
        }

        /// <summary>
        /// 스킬 활성화 여부 확인
        /// </summary>
        /// <param name="skillId">스킬 ID</param>
        /// <returns>스킬 레벨이 0보다 크면 true</returns>
        public static bool IsSkillActive(string skillId)
        {
            var manager = SkillTreeManager.Instance;
            return manager != null && manager.GetSkillLevel(skillId) > 0;
        }

        /// <summary>
        /// 여러 스킬 중 하나라도 활성화되어 있는지 확인
        /// </summary>
        /// <param name="skillIds">스킬 ID 배열</param>
        /// <returns>하나라도 활성화되어 있으면 true</returns>
        public static bool IsAnySkillActive(params string[] skillIds)
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return false;

            return skillIds.Any(id => manager.GetSkillLevel(id) > 0);
        }

        /// <summary>
        /// 모든 스킬이 활성화되어 있는지 확인
        /// </summary>
        /// <param name="skillIds">스킬 ID 배열</param>
        /// <returns>모든 스킬이 활성화되어 있으면 true</returns>
        public static bool AreAllSkillsActive(params string[] skillIds)
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return false;

            return skillIds.All(id => manager.GetSkillLevel(id) > 0);
        }

        /// <summary>
        /// 직업 스킬 활성화 확인 (Archer, Tanker, Rogue, Mage 등)
        /// </summary>
        /// <param name="jobId">직업 ID</param>
        /// <returns>해당 직업이 활성화되어 있으면 true</returns>
        public static bool IsJobActive(string jobId)
        {
            return IsSkillActive(jobId);
        }

        /// <summary>
        /// 배율 적용 보너스 계산 (1 + 보너스/100 형태)
        /// </summary>
        /// <param name="bonuses">스킬ID와 해당 값을 반환하는 함수의 튜플 배열</param>
        /// <returns>1 + (총 보너스/100) 형태의 배율</returns>
        public static float CalculateMultiplier(params (string skillId, Func<float> getValue)[] bonuses)
        {
            float totalBonus = CalculateTotal(bonuses);
            return 1f + (totalBonus / 100f);
        }
    }
}

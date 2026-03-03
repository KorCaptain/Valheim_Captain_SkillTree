using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 액티브 스킬 쿨다운 중앙 레지스트리
    /// HUD가 읽고, 각 스킬 발동 지점에서 SetCooldown을 호출합니다.
    /// slot 상수: "Y", "R", "G", "H"
    /// </summary>
    public static class ActiveSkillCooldownRegistry
    {
        private struct CooldownEntry
        {
            public float EndTime;
            public float TotalTime;
        }

        private static readonly Dictionary<string, CooldownEntry> _entries = new Dictionary<string, CooldownEntry>();

        /// <summary>
        /// 슬롯의 쿨다운을 설정합니다. 스킬 발동 시 호출하세요.
        /// </summary>
        public static void SetCooldown(string slot, float totalDuration)
        {
            if (totalDuration <= 0f) return;
            _entries[slot] = new CooldownEntry
            {
                EndTime = Time.time + totalDuration,
                TotalTime = totalDuration
            };
        }

        /// <summary>
        /// 쿨다운 비율 반환 (0 = 사용 가능, 1 = 쿨다운 최대)
        /// HUD 오버레이의 fillAmount에 사용합니다.
        /// </summary>
        public static float GetCooldownRatio(string slot)
        {
            if (!_entries.TryGetValue(slot, out var entry)) return 0f;
            if (entry.TotalTime <= 0f) return 0f;
            float remaining = entry.EndTime - Time.time;
            if (remaining <= 0f) return 0f;
            return Mathf.Clamp01(remaining / entry.TotalTime);
        }

        /// <summary>
        /// 남은 쿨다운 초 반환 (0 = 사용 가능)
        /// </summary>
        public static float GetCooldownRemaining(string slot)
        {
            if (!_entries.TryGetValue(slot, out var entry)) return 0f;
            return Mathf.Max(0f, entry.EndTime - Time.time);
        }
    }
}

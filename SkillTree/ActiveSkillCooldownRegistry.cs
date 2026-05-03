using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 액티브 스킬 쿨다운 중앙 레지스트리
    /// HUD가 읽고, 각 스킬 발동 지점에서 SetCooldown을 호출합니다.
    /// slot 상수: "Y", "R", "G", "H", "M2"(휠윈드)
    /// </summary>
    public static class ActiveSkillCooldownRegistry
    {
        private struct CooldownEntry
        {
            public float EndTime;
            public float TotalTime;
        }

        private static readonly Dictionary<string, CooldownEntry> _entries = new Dictionary<string, CooldownEntry>();
        private static readonly Dictionary<string, CooldownEntry> _skillEntries = new Dictionary<string, CooldownEntry>();

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
            CaptainSkillTree.Gui.ActiveSkillHUD.Instance?.OnCooldownStarted();
        }

        /// <summary>
        /// 슬롯 + 스킬 ID 두 곳에 쿨다운을 함께 등록합니다.
        /// R/G/H 다중무기 슬롯에서 무기별 개별 쿨타임 추적에 사용합니다.
        /// </summary>
        public static void SetCooldownForSkill(string slot, string skillId, float totalDuration)
        {
            if (totalDuration <= 0f) return;
            var entry = new CooldownEntry { EndTime = Time.time + totalDuration, TotalTime = totalDuration };
            _entries[slot] = entry;
            if (!string.IsNullOrEmpty(skillId)) _skillEntries[skillId] = entry;
            CaptainSkillTree.Gui.ActiveSkillHUD.Instance?.OnCooldownStarted();
        }

        /// <summary>
        /// 여러 스킬 ID에 동일 쿨다운을 등록합니다 (검 finalcut/slash처럼 동일 액션을 공유하는 경우).
        /// </summary>
        public static void SetCooldownForSkills(string slot, string[] skillIds, float totalDuration)
        {
            if (totalDuration <= 0f) return;
            var entry = new CooldownEntry { EndTime = Time.time + totalDuration, TotalTime = totalDuration };
            _entries[slot] = entry;
            foreach (var id in skillIds) if (!string.IsNullOrEmpty(id)) _skillEntries[id] = entry;
            CaptainSkillTree.Gui.ActiveSkillHUD.Instance?.OnCooldownStarted();
        }

        /// <summary>스킬 ID 기준 쿨다운 비율 (0=사용가능, 1=최대)</summary>
        public static float GetSkillCooldownRatio(string skillId)
        {
            if (!_skillEntries.TryGetValue(skillId, out var entry) || entry.TotalTime <= 0f) return 0f;
            float remaining = entry.EndTime - Time.time;
            if (remaining <= 0f) return 0f;
            return Mathf.Clamp01(remaining / entry.TotalTime);
        }

        /// <summary>스킬 ID 기준 남은 쿨다운 초</summary>
        public static float GetSkillCooldownRemaining(string skillId)
        {
            if (!_skillEntries.TryGetValue(skillId, out var entry)) return 0f;
            return Mathf.Max(0f, entry.EndTime - Time.time);
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

        /// <summary>
        /// Config 변경 시 발동 시점을 기준으로 EndTime과 TotalTime을 모두 재계산.
        /// 이미 만료된 경우(new_EndTime &lt;= Time.time) 엔트리를 삭제하여 즉시 사용 가능 상태로.
        /// </summary>
        public static void RecalculateCooldown(string slot, float newTotalTime)
        {
            if (!_entries.TryGetValue(slot, out var entry)) return;
            if (newTotalTime <= 0f) return;

            float triggerTime = entry.EndTime - entry.TotalTime; // 발동 시점 역산
            float newEndTime = triggerTime + newTotalTime;

            if (newEndTime <= Time.time)
            {
                _entries.Remove(slot); // 새 쿨타임 기준으로 이미 만료 → 즉시 사용 가능
                return;
            }
            _entries[slot] = new CooldownEntry { EndTime = newEndTime, TotalTime = newTotalTime };
            CaptainSkillTree.Gui.ActiveSkillHUD.Instance?.OnCooldownChanged();
        }

        /// <summary>
        /// 모든 활성 쿨타임 중 최소 잔여시간 반환.
        /// 활성 쿨타임 없으면 0f 반환. HUD 폴링 간격 결정에 사용.
        /// </summary>
        public static float GetMinRemaining()
        {
            float min = float.MaxValue;
            foreach (var e in _entries.Values)
            {
                float r = e.EndTime - Time.time;
                if (r > 0f && r < min) min = r;
            }
            return min == float.MaxValue ? 0f : min;
        }
    }
}

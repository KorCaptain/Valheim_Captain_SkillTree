using System.Runtime.CompilerServices;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// 몬스터별 마지막 유효 공격자 캐시.
    /// 독/화상 등 바닐라 도트(SE_Poison, SE_Burning)는 매 틱마다 공격자 정보 없는
    /// HitData로 ApplyDamage를 호출하므로, 도트 틱으로 사망 시 m_lastHit만으로는
    /// 킬러를 판정할 수 없다. 이 캐시는 공격자가 있는 타격이 있을 때마다 갱신되어,
    /// 도트 사망 시 킬 판정의 fallback으로 사용된다.
    /// </summary>
    internal static class CaptainLastAttackerCache
    {
        private class AttackerRecord
        {
            public Character Attacker;
            public float Time;
        }

        // 키(몬스터)가 GC되면 자동으로 정리되므로 별도 cleanup 불필요
        private static readonly ConditionalWeakTable<Character, AttackerRecord> _cache = new ConditionalWeakTable<Character, AttackerRecord>();

        public static void Record(Character target, Character attacker)
        {
            if (target == null || attacker == null) return;

            if (_cache.TryGetValue(target, out var record))
            {
                record.Attacker = attacker;
                record.Time = Time.time;
            }
            else
            {
                _cache.Add(target, new AttackerRecord { Attacker = attacker, Time = Time.time });
            }
        }

        public static bool TryGetRecent(Character target, float maxAge, out Character attacker)
        {
            attacker = null;
            if (target == null) return false;

            if (_cache.TryGetValue(target, out var record) && record.Attacker != null)
            {
                if (Time.time - record.Time <= maxAge)
                {
                    attacker = record.Attacker;
                    return true;
                }
            }

            return false;
        }
    }
}

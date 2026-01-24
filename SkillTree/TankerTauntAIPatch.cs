using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 탱커 도발 시스템 - MonsterAI 타겟 선택 강제 제어
    /// 도발 중인 몬스터는 무조건 탱커를 타겟하도록 매 프레임 강제
    /// </summary>
    [HarmonyPatch(typeof(MonsterAI), "UpdateAI")]
    public static class TankerTauntAIPatch
    {
        /// <summary>
        /// 도발 중인 몬스터 추적 Dictionary
        /// Key: Character (몬스터), Value: (Player tanker, float expiryTime)
        /// </summary>
        private static Dictionary<Character, (Player tanker, float expiry)> tauntedMonsters
            = new Dictionary<Character, (Player tanker, float expiry)>();

        /// <summary>
        /// MonsterAI.UpdateAI 실행 전 호출 - 도발 중인 몬스터 타겟 강제 유지
        /// 매 프레임 실행되지만 Dictionary 조회만 하므로 성능 영향 최소
        /// </summary>
        static void Prefix(MonsterAI __instance)
        {
            try
            {
                // m_character는 protected이므로 리플렉션으로 접근
                var characterField = typeof(BaseAI).GetField("m_character",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (characterField == null) return;

                Character monster = characterField.GetValue(__instance) as Character;
                if (monster == null) return;

                // 도발 중인 몬스터인지 확인 (O(1) Dictionary 조회)
                if (tauntedMonsters.ContainsKey(monster))
                {
                    var (tanker, expiry) = tauntedMonsters[monster];

                    // 도발 시간 만료 체크
                    if (Time.time > expiry)
                    {
                        tauntedMonsters.Remove(monster);
                        return;
                    }

                    // ✅ 안전한 탱커 유효성 검사 (Unity 객체 파괴 상태 고려)
                    // Unity 객체는 파괴 중일 때 == null은 false지만 메서드 호출 시 예외 발생 가능
                    bool tankerInvalid = false;
                    try
                    {
                        // Unity의 implicit bool operator 사용 - 파괴된 객체는 false 반환
                        if (tanker == null || !tanker)
                        {
                            tankerInvalid = true;
                        }
                        else if (tanker.IsDead())
                        {
                            tankerInvalid = true;
                        }
                    }
                    catch
                    {
                        // IsDead() 호출 중 MissingReferenceException 발생 시
                        tankerInvalid = true;
                    }

                    if (tankerInvalid)
                    {
                        tauntedMonsters.Remove(monster);
                        return;
                    }

                    // 강제로 탱커를 타겟으로 설정 (매 프레임)
                    ForceTankerTarget(__instance, tanker, monster.name);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[도발 AI 패치] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 몬스터를 도발 목록에 추가 (TankerSkills.cs에서 호출)
        /// </summary>
        /// <param name="monster">도발할 몬스터</param>
        /// <param name="tanker">탱커 플레이어</param>
        /// <param name="duration">도발 지속시간 (초)</param>
        public static void AddTauntedMonster(Character monster, Player tanker, float duration)
        {
            if (monster == null || tanker == null) return;

            float expiryTime = Time.time + duration;
            tauntedMonsters[monster] = (tanker, expiryTime);

            Plugin.Log.LogDebug($"[도발 AI 패치] {monster.name} 도발 등록 - {duration}초 ({tanker.GetPlayerName()})");
        }

        /// <summary>
        /// 도발 목록에서 제거 (도발 종료 시 호출)
        /// </summary>
        /// <param name="monster">도발 해제할 몬스터</param>
        public static void RemoveTauntedMonster(Character monster)
        {
            if (monster != null && tauntedMonsters.ContainsKey(monster))
            {
                tauntedMonsters.Remove(monster);
                Plugin.Log.LogDebug($"[도발 AI 패치] {monster.name} 도발 제거");
            }
        }

        /// <summary>
        /// 강제로 탱커를 타겟으로 설정 (리플렉션 사용)
        /// 여러 방법을 동시 사용하여 확실하게 타겟 변경
        /// </summary>
        private static void ForceTankerTarget(MonsterAI ai, Player tanker, string monsterName)
        {
            try
            {
                // 1. SetHuntPlayer 호출 (MonsterAI 전용, 가장 효과적)
                var setHuntMethod = typeof(MonsterAI).GetMethod("SetHuntPlayer",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (setHuntMethod != null)
                {
                    setHuntMethod.Invoke(ai, new object[] { tanker });
                }

                // 2. SetTarget 호출 (BaseAI 메서드, 백업)
                var setTargetMethod = ai.GetType().GetMethod("SetTarget",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (setTargetMethod != null)
                {
                    setTargetMethod.Invoke(ai, new object[] { tanker });
                }

                // 3. m_target 필드 직접 설정 (최종 백업)
                var targetField = ai.GetType().GetField("m_target",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (targetField != null)
                {
                    targetField.SetValue(ai, tanker);
                }

                // 4. m_targetCreature 필드도 설정 (추가 백업)
                var targetCreatureField = ai.GetType().GetField("m_targetCreature",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (targetCreatureField != null)
                {
                    targetCreatureField.SetValue(ai, tanker);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[도발 AI 패치] {monsterName} 타겟 강제 설정 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어 사망 시 해당 플레이어의 모든 도발 해제
        /// </summary>
        /// <param name="player">사망한 플레이어</param>
        public static void ClearTauntsByPlayer(Player player)
        {
            try
            {
                var monstersToRemove = new List<Character>();

                foreach (var kvp in tauntedMonsters)
                {
                    // ✅ 안전한 비교: ReferenceEquals 사용 (Unity 오버로드 우회)
                    if (ReferenceEquals(kvp.Value.tanker, player))
                    {
                        monstersToRemove.Add(kvp.Key);
                    }
                }

                foreach (var monster in monstersToRemove)
                {
                    tauntedMonsters.Remove(monster);
                }
            }
            catch
            {
                // 예외 발생 시 전체 딕셔너리 클리어 (안전 우선)
                tauntedMonsters.Clear();
            }
        }

        /// <summary>
        /// 모든 도발 해제 (디버그/리셋용)
        /// </summary>
        public static void ClearAllTaunts()
        {
            int count = tauntedMonsters.Count;
            tauntedMonsters.Clear();
            Plugin.Log.LogDebug($"[도발 AI 패치] 모든 도발 해제 ({count}마리)");
        }

        /// <summary>
        /// 현재 도발 중인 몬스터 수 (디버그용)
        /// </summary>
        public static int GetTauntedMonsterCount()
        {
            return tauntedMonsters.Count;
        }
    }
}

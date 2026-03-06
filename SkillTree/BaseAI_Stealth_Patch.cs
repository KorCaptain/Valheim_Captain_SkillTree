using System;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree;
using System.Collections.Generic;
using System.Linq;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// BaseAI 시스템 스텔스 패치 - 완전한 스텔스 구현
    /// 로그 그림자 일격 스킬의 스텔스 효과를 위해 몬스터가 스텔스 중인 플레이어를 완전히 인식하지 못하도록 차단
    /// </summary>
    public static class BaseAI_Stealth_Patches
    {
        // 반복 리플렉션 비용 방지를 위해 필드 캐시
        private static readonly System.Reflection.FieldInfo s_targetCreatureField =
            typeof(BaseAI).GetField("m_targetCreature",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        /// <summary>
        /// BaseAI.GetTargetCreature 패치 - 스텔스 중인 플레이어는 타겟에서 제외
        /// </summary>
        [HarmonyPatch(typeof(BaseAI), nameof(BaseAI.GetTargetCreature))]
        public static class BaseAI_GetTargetCreature_Stealth_Patch
        {
            [HarmonyPriority(Priority.High)]
            public static void Postfix(BaseAI __instance, ref Character __result)
            {
                try
                {
                    // 결과가 null이면 처리 불필요
                    if (__result == null) return;
                    
                    // 타겟이 플레이어인지 확인
                    if (__result is Player targetPlayer)
                    {
                        // 스텔스 또는 어그로 제거 상태 확인
                        if (RogueSkills.IsPlayerInStealth(targetPlayer) || RogueSkills.IsAggroRemoved(targetPlayer))
                        {
                            // 스텔스 중인 플레이어는 타겟에서 제외
                            __result = null;
                            
                            Plugin.Log.LogDebug($"[BaseAI 스텔스 패치] {targetPlayer.GetPlayerName()} 스텔스 중 - GetTargetCreature에서 제외됨");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[BaseAI 스텔스 패치] GetTargetCreature 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// BaseAI.SetAlerted 패치 - 스텔스 중인 플레이어 근처에서 "!" VFX 생성 차단
        /// </summary>
        [HarmonyPatch(typeof(BaseAI), "SetAlerted")]
        public static class BaseAI_SetAlerted_Stealth_Patch
        {
            public static bool Prefix(BaseAI __instance, bool alert)
            {
                if (!alert) return true; // SetAlerted(false)는 항상 허용

                try
                {
                    foreach (var character in Character.GetAllCharacters())
                    {
                        if (character is Player p && (RogueSkills.IsPlayerInStealth(p) || RogueSkills.IsAggroRemoved(p))
                            && Vector3.Distance(__instance.transform.position, p.transform.position) < 50f)
                        {
                            Plugin.Log.LogDebug($"[BaseAI 스텔스 패치] SetAlerted(true) 차단 - 스텔스 플레이어 {p.GetPlayerName()} 근처");
                            return false; // "!" VFX 생성 차단
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[BaseAI 스텔스 패치] SetAlerted 패치 오류: {ex.Message}");
                }

                return true;
            }
        }

        /// <summary>
        /// MonsterAI.UpdateAI Postfix - 매 AI 업데이트 후 m_targetCreature를 직접 차단
        /// MonsterAI.UpdateTarget이 SetTarget()을 거치지 않고 m_targetCreature에 직접 할당하므로
        /// Postfix로 업데이트 후 강제 클리어
        /// </summary>
        [HarmonyPatch(typeof(MonsterAI), "UpdateAI")]
        public static class MonsterAI_UpdateAI_Stealth_Patch
        {
            [HarmonyPriority(Priority.High)]
            public static void Postfix(MonsterAI __instance)
            {
                try
                {
                    if (s_targetCreatureField == null) return;
                    var target = s_targetCreatureField.GetValue(__instance) as Character;
                    if (target is Player p && (RogueSkills.IsPlayerInStealth(p) || RogueSkills.IsAggroRemoved(p)))
                    {
                        s_targetCreatureField.SetValue(__instance, null);
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[BaseAI 스텔스 패치] UpdateAI Postfix 오류: {ex.Message}");
                }
            }
        }

        // CanSeeTarget 패치는 ambiguous match 오류로 인해 제거
        // 다른 패치들로 충분한 스텔스 효과 구현

        // FindTarget 패치도 ambiguous match 위험이 있어 제거

        /// <summary>
        /// 스텔스 상태 확인 및 강제 타겟 해제를 위한 정기적인 클리너
        /// AI 패치 대신 코루틴 기반으로 안전하게 처리
        /// </summary>
        public static void StartStealthCleaner()
        {
            if (Plugin.Instance != null)
            {
                Plugin.Instance.StartCoroutine(StealthCleanerCoroutine());
            }
        }

        /// <summary>
        /// 스텔스 클리너 코루틴 - 주기적으로 스텔스 플레이어 타겟 해제
        /// </summary>
        private static System.Collections.IEnumerator StealthCleanerCoroutine()
        {
            while (true)
            {
                try
                {
                    CleanStealthTargets();
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[BaseAI 스텔스 패치] 스텔스 클리너 오류: {ex.Message}");
                }
                
                yield return new WaitForSeconds(0.5f); // 0.5초마다 실행
            }
        }

        /// <summary>
        /// 모든 AI에서 스텔스 플레이어 타겟 해제
        /// </summary>
        private static void CleanStealthTargets()
        {
            if (s_targetCreatureField == null) return;

            var allCharacters = Character.GetAllCharacters();
            foreach (var character in allCharacters)
            {
                if (character == null || character.IsPlayer() || character.IsDead()) continue;

                var baseAI = character.GetBaseAI();
                if (baseAI == null) continue;

                // 직접 필드 읽기 - GetTargetCreature()의 우리 Postfix는 스텔스 플레이어를 null로 반환하므로 우회 필요
                var currentTarget = s_targetCreatureField.GetValue(baseAI) as Character;
                if (currentTarget is Player targetPlayer && (RogueSkills.IsPlayerInStealth(targetPlayer) || RogueSkills.IsAggroRemoved(targetPlayer)))
                {
                    // 스텔스 중인 플레이어 타겟 해제
                    ClearAITarget(baseAI);
                    
                    // MonsterAI인 경우 헌팅도 해제
                    if (baseAI is MonsterAI monsterAI)
                    {
                        var huntTarget = GetMonsterHuntTarget(monsterAI);
                        if (huntTarget is Player huntPlayer && (RogueSkills.IsPlayerInStealth(huntPlayer) || RogueSkills.IsAggroRemoved(huntPlayer)))
                        {
                            ClearMonsterHunt(monsterAI);
                        }
                    }
                    
                    Plugin.Log.LogDebug($"[BaseAI 스텔스 패치] {targetPlayer.GetPlayerName()} 스텔스 중 - {character.GetHoverName()} 타겟 해제");
                }
            }
        }

        /// <summary>
        /// AI 타겟을 안전하게 해제하는 헬퍼 메서드
        /// </summary>
        private static void ClearAITarget(BaseAI ai)
        {
            try
            {
                // SetTarget 메서드 호출 시도
                var setTargetMethod = ai.GetType().GetMethod("SetTarget", new[] { typeof(Character) });
                if (setTargetMethod != null)
                {
                    setTargetMethod.Invoke(ai, new object[] { null });
                }
                
                // m_targetCreature 필드 직접 해제
                var targetCreatureField = ai.GetType().GetField("m_targetCreature", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (targetCreatureField != null)
                {
                    targetCreatureField.SetValue(ai, null);
                }

                // SetAlerted(false) 호출 - alerted 상태를 정상 경로로 초기화
                var setAlertedMethod = ai.GetType().GetMethod("SetAlerted",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                    null, new[] { typeof(bool) }, null);
                setAlertedMethod?.Invoke(ai, new object[] { false });
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[BaseAI 스텔스 패치] AI 타겟 해제 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// MonsterAI의 헌팅 타겟 가져오기
        /// </summary>
        private static Character GetMonsterHuntTarget(MonsterAI monsterAI)
        {
            try
            {
                var huntField = typeof(MonsterAI).GetField("m_hunt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (huntField != null)
                {
                    return huntField.GetValue(monsterAI) as Character;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[BaseAI 스텔스 패치] 헌팅 타겟 가져오기 실패: {ex.Message}");
            }
            
            return null;
        }

        /// <summary>
        /// MonsterAI의 헌팅 상태를 안전하게 해제하는 헬퍼 메서드
        /// </summary>
        private static void ClearMonsterHunt(MonsterAI monsterAI)
        {
            try
            {
                // SetHuntPlayer 메서드 호출
                var setHuntMethod = typeof(MonsterAI).GetMethod("SetHuntPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (setHuntMethod != null)
                {
                    setHuntMethod.Invoke(monsterAI, new object[] { null });
                }
                
                // m_hunt 필드 직접 해제
                var huntField = typeof(MonsterAI).GetField("m_hunt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (huntField != null)
                {
                    huntField.SetValue(monsterAI, null);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[BaseAI 스텔스 패치] MonsterAI 헌팅 해제 실패: {ex.Message}");
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using CaptainSkillTree;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 로그 스텔스 + 어그로 제거 시스템 (RogueSkills partial class)
    /// </summary>
    public static partial class RogueSkills
    {
        // === 스텔스 시스템 ===
        private static Dictionary<Player, float> stealthEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> stealthActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, Coroutine> stealthDurationCoroutine = new Dictionary<Player, Coroutine>();
        private static bool stealthCleanerStarted = false;

        // === 어그로 제거 독립 상태 (공격 시 스텔스 해제 후에도 유지) ===
        private static Dictionary<Player, float> aggroRemovalEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> aggroRemovalActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, Coroutine> aggroRemovalLoopCoroutine = new Dictionary<Player, Coroutine>();

        // ==================== 스텔스 시스템 ====================

        /// <summary>
        /// 스텔스 상태 적용 (스텔스 지속시간 동안 몬스터가 플레이어를 타겟팅하지 못함)
        /// </summary>
        private static void ApplyStealthState(Player player)
        {
            try
            {
                float stealthDuration = Rogue_Config.RogueShadowStrikeStealthDurationValue;

                stealthEndTime[player] = Time.time + stealthDuration;
                stealthActive[player] = true;

                player.Message(MessageHud.MessageType.Center, L.Get("rogue_stealth_start", stealthDuration.ToString()));

                EnsureStealthCleanerRunning();

                if (Plugin.Instance != null)
                {
                    if (stealthDurationCoroutine.TryGetValue(player, out var existing) && existing != null)
                        Plugin.Instance.StopCoroutine(existing);
                    var co = Plugin.Instance.StartCoroutine(StealthDurationCoroutine(player, stealthDuration));
                    stealthDurationCoroutine[player] = co;
                }
            }
            catch (System.Exception) { }
        }

        private static void EnsureStealthCleanerRunning()
        {
            if (!stealthCleanerStarted)
            {
                stealthCleanerStarted = true;
                BaseAI_Stealth_Patches.StartStealthCleaner();
            }
        }

        private static IEnumerator StealthDurationCoroutine(Player player, float duration)
        {
            yield return new WaitForSeconds(duration);

            if (player == null || player.IsDead()) yield break;

            if (stealthActive.ContainsKey(player) && stealthActive[player])
                RemoveStealthState(player, "시간 만료");
        }

        /// <summary>
        /// 스텔스 상태 제거
        /// </summary>
        public static void RemoveStealthState(Player player, string reason = "알 수 없음")
        {
            try
            {
                if (!stealthActive.ContainsKey(player) || !stealthActive[player]) return;

                stealthActive[player] = false;
                stealthEndTime.Remove(player);

                player.Message(MessageHud.MessageType.Center, L.Get("rogue_stealth_end", reason));
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// 플레이어가 현재 스텔스 상태인지 확인
        /// </summary>
        public static bool IsPlayerInStealth(Player player)
        {
            if (player == null) return false;
            if (!stealthActive.ContainsKey(player) || !stealthActive[player]) return false;

            if (stealthEndTime.TryGetValue(player, out float endTime) && Time.time >= endTime)
            {
                RemoveStealthState(player, "시간 만료");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 어그로 제거 상태인지 확인 (스텔스와 독립, 공격 시에도 유지)
        /// </summary>
        public static bool IsAggroRemoved(Player player)
        {
            if (player == null) return false;
            if (!aggroRemovalActive.TryGetValue(player, out bool active) || !active) return false;
            if (!aggroRemovalEndTime.TryGetValue(player, out float endTime)) return false;

            if (Time.time >= endTime)
            {
                aggroRemovalActive[player] = false;
                aggroRemovalEndTime.Remove(player);
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_aggro_protection_end"));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 스텔스 시스템 상태 정리 (플레이어 로그아웃 시)
        /// </summary>
        public static void CleanupStealthState(Player player)
        {
            try
            {
                stealthActive.Remove(player);
                stealthEndTime.Remove(player);
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// 스텔스 + 어그로 제거 상태 전체 정리 (CleanupRogueSkillsOnDeath에서 호출, lock 내부)
        /// </summary>
        internal static void CleanupStealthAndAggroState(Player player)
        {
            // 스텔스 코루틴 정리
            if (stealthDurationCoroutine.TryGetValue(player, out var stealthCo) && stealthCo != null)
            {
                try
                {
                    if (Plugin.Instance != null) Plugin.Instance.StopCoroutine(stealthCo);
                    else if (player != null) player.StopCoroutine(stealthCo);
                }
                catch (Exception) { }
            }
            stealthDurationCoroutine.Remove(player);
            stealthEndTime.Remove(player);
            stealthActive.Remove(player);

            // 어그로 제거 루프 코루틴 정리
            if (aggroRemovalLoopCoroutine.TryGetValue(player, out var aggroLoop) && aggroLoop != null)
            {
                try
                {
                    if (Plugin.Instance != null) Plugin.Instance.StopCoroutine(aggroLoop);
                    else if (player != null) player.StopCoroutine(aggroLoop);
                }
                catch (Exception) { }
            }
            aggroRemovalLoopCoroutine.Remove(player);
            aggroRemovalEndTime.Remove(player);
            aggroRemovalActive.Remove(player);
        }

        // ==================== 어그로 제거 시스템 ====================

        /// <summary>
        /// 주변 몬스터 어그로 제거
        /// </summary>
        private static int RemoveNearbyMonsterAggro(Player player)
        {
            int count = 0;
            try
            {
                float aggroRange = Rogue_Config.RogueShadowStrikeAggroRangeValue;
                Vector3 playerPos = player.transform.position;

                var nearbyEnemies = Character.GetAllCharacters()
                    .Where(c => c != null && !c.IsDead() && c != player && !c.IsPlayer())
                    .Where(c => Vector3.Distance(playerPos, c.transform.position) <= aggroRange)
                    .ToList();

                foreach (var enemy in nearbyEnemies)
                {
                    if (enemy == null) continue;
                    try
                    {
                        var baseAI = enemy.GetBaseAI();
                        if (baseAI != null && SafeRemoveAggro(player, enemy, baseAI, enemy.GetHoverName() ?? enemy.name ?? "Unknown"))
                            count++;
                    }
                    catch (Exception) { }
                }
            }
            catch (System.Exception) { }
            return count;
        }

        /// <summary>
        /// 어그로 제거 반복 코루틴 (1초마다 실행, 버프 시간 동안 지속)
        /// </summary>
        private static IEnumerator AggroRemovalLoopCoroutine(Player player, float duration)
        {
            float endTime = Time.time + duration;
            while (Time.time < endTime)
            {
                yield return new WaitForSeconds(1f);

                if (player == null || player.IsDead()) break;

                RemoveNearbyMonsterAggro(player);

                try
                {
                    VFX.VFXManager.PlayVFXMultiplayer("fx_greenroots_projectile_hit", "", player.transform.position, Quaternion.identity, 0.7f);
                }
                catch (Exception) { }
            }

            aggroRemovalLoopCoroutine.Remove(player);
        }

        /// <summary>
        /// 안전한 어그로 제거 - 다단계 해제 시스템
        /// </summary>
        private static bool SafeRemoveAggro(Player player, Character enemy, BaseAI ai, string enemyName)
        {
            bool success = false;
            try
            {
                var currentTarget = ai.GetTargetCreature();
                if (currentTarget == null || currentTarget.gameObject != player.gameObject)
                    return true; // 이미 어그로 없음

                var monsterAI = ai as MonsterAI;
                if (monsterAI != null)
                    success = TryRemoveMonsterAIHuntPlayer(monsterAI, enemyName);

                TrySetMonsterAIToIdle(monsterAI, enemyName);
                success |= TryResetTargetCreature(ai, enemyName);

                return VerifyAggroRemoval(player, ai, enemyName, success);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[❌ 안전한 어그로 제거] {enemyName} 처리 실패: {ex.Message}");
                return false;
            }
        }

        private static bool TryRemoveMonsterAIHuntPlayer(MonsterAI monsterAI, string enemyName)
        {
            try
            {
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                bool success = false;

                var setHuntMethod = typeof(MonsterAI).GetMethod("SetHuntPlayer", flags);
                if (setHuntMethod != null) { setHuntMethod.Invoke(monsterAI, new object[] { null }); success = true; }

                var huntField = typeof(MonsterAI).GetField("m_hunt", flags);
                if (huntField != null) { huntField.SetValue(monsterAI, null); success = true; }

                return success;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} MonsterAI hunt 해제 실패: {ex.Message}");
                return false;
            }
        }

        private static void TrySetMonsterAIToIdle(MonsterAI monsterAI, string enemyName)
        {
            if (monsterAI == null) return;
            try
            {
                var stateField = typeof(MonsterAI).GetField("m_state", BindingFlags.NonPublic | BindingFlags.Instance);
                stateField?.SetValue(monsterAI, 0); // 0 = State.Idle
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} Idle 전환 실패: {ex.Message}");
            }
        }

        private static bool TryResetTargetCreature(BaseAI ai, string enemyName)
        {
            bool success = false;
            try
            {
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;

                var setTargetMethod = ai.GetType().GetMethod("SetTarget", new[] { typeof(Character) });
                if (setTargetMethod != null) { setTargetMethod.Invoke(ai, new object[] { null }); success = true; }

                ai.GetType().GetField("m_target", flags)?.SetValue(ai, null);
                ai.GetType().GetField("m_targetCreature", flags)?.SetValue(ai, null);
                if (success) success = true;

                var setAlerted = ai.GetType().GetMethod("SetAlerted", flags, null, new[] { typeof(bool) }, null);
                if (setAlerted != null) { setAlerted.Invoke(ai, new object[] { false }); success = true; }

                ai.GetType().GetField("m_timeSinceHurt", flags)?.SetValue(ai, 999f);
                ai.GetType().GetField("m_lastDamageTime", flags)?.SetValue(ai, 0f);

                return success;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} 타겟 리셋 실패: {ex.Message}");
                return false;
            }
        }

        private static bool VerifyAggroRemoval(Player player, BaseAI ai, string enemyName, bool successSoFar)
        {
            try
            {
                var finalTarget = ai.GetTargetCreature();
                if (finalTarget == null || finalTarget.gameObject != player.gameObject)
                {
                    Plugin.Log.LogInfo($"[✅ 안전한 어그로 제거] {enemyName} 어그로 해제 확인됨");
                    return true;
                }
                Plugin.Log.LogWarning($"[❌ 안전한 어그로 제거] {enemyName} 어그로 해제 실패");
                return successSoFar;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} 최종 확인 실패: {ex.Message}");
                return successSoFar;
            }
        }
    }
}

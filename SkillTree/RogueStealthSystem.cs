using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 로그 스텔스 시스템 (RogueSkills partial class)
    /// </summary>
    public static partial class RogueSkills
    {
        // === 스텔스 시스템 ===
        private static Dictionary<Player, float> stealthEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> stealthActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, Coroutine> stealthDurationCoroutine = new Dictionary<Player, Coroutine>();
        private static bool stealthCleanerStarted = false;

        // ==================== 스텔스 시스템 ====================

        /// <summary>
        /// 스텔스 상태 적용
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
        /// 스텔스 시스템 상태 정리
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
        /// 스텔스 상태 전체 정리 (CleanupRogueSkillsOnDeath에서 호출, lock 내부)
        /// </summary>
        internal static void CleanupStealthAndAggroState(Player player)
        {
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
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 탈출 스킬 - 시작 지점으로 포탈 텔레포트 (3시간 1회)
    /// </summary>
    public static class EscapeSkill
    {
        private const string PREFS_KEY = "CST_EscapeLastUsed";
        private const float COOLDOWN_SECONDS = 10800f; // 3시간

        /// <summary>사용 가능 여부 (세션 간 유지)</summary>
        public static bool CanEscape()
        {
            try
            {
                string s = PlayerPrefs.GetString(PREFS_KEY, "");
                if (string.IsNullOrEmpty(s) || !long.TryParse(s, out long ticks)) return true;
                return (DateTime.UtcNow - new DateTime(ticks, DateTimeKind.Utc)).TotalSeconds >= COOLDOWN_SECONDS;
            }
            catch { return true; }
        }

        /// <summary>남은 쿨타임(분) 반환</summary>
        public static float GetRemainingMinutes()
        {
            try
            {
                string s = PlayerPrefs.GetString(PREFS_KEY, "");
                if (string.IsNullOrEmpty(s) || !long.TryParse(s, out long ticks)) return 0f;
                float remaining = COOLDOWN_SECONDS - (float)(DateTime.UtcNow - new DateTime(ticks, DateTimeKind.Utc)).TotalSeconds;
                return Mathf.Max(0f, remaining / 60f);
            }
            catch { return 0f; }
        }

        /// <summary>탈출 시도 - 시작 지점으로 Valheim TeleportTo 사용</summary>
        public static void TryEscape(Player player)
        {
            try
            {
                if (!CanEscape())
                {
                    int mins = Mathf.CeilToInt(GetRemainingMinutes());
                    player.Message(MessageHud.MessageType.TopLeft,
                        Localization.L.Get("ui_escape_cooldown", mins.ToString()), 0, null);
                    return;
                }

                Vector3 dest = FindStartPoint();
                Plugin.Log.LogInfo($"[Escape] 시작 지점 → {dest}");

                Plugin.Instance.StartCoroutine(DoTeleport(player, dest));

                PlayerPrefs.SetString(PREFS_KEY, DateTime.UtcNow.Ticks.ToString());
                PlayerPrefs.Save();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Escape] TryEscape 오류: {ex.Message}\n{ex.StackTrace}");
            }
        }

        // 시작 지점 탐색: StartTemple → SpawnPoint → 맵 중앙 순서로 시도
        private static Vector3 FindStartPoint()
        {
            // 1. ZoneSystem에서 StartTemple 위치 탐색
            if (ZoneSystem.instance != null)
            {
                ZoneSystem.LocationInstance loc;
                if (ZoneSystem.instance.FindClosestLocation("StartTemple", Vector3.zero, out loc))
                {
                    Plugin.Log.LogInfo($"[Escape] StartTemple 발견: {loc.m_position}");
                    return loc.m_position;
                }
            }

            // 2. 플레이어 스폰 포인트 (침대 없으면 초기 스폰 위치)
            if (Game.instance != null)
            {
                Vector3 spawnPos = Vector3.zero;
                var profile = Game.instance.GetPlayerProfile();
                if (profile != null)
                {
                    bool hasSpawn = profile.HaveCustomSpawnPoint();
                    if (hasSpawn)
                    {
                        spawnPos = profile.GetCustomSpawnPoint();
                        Plugin.Log.LogInfo($"[Escape] 커스텀 스폰 포인트: {spawnPos}");
                        return spawnPos;
                    }
                }
            }

            // 3. 맵 중앙 (0, 0) 지형 높이
            Plugin.Log.LogInfo("[Escape] 맵 중앙으로 폴백");
            float groundY = ZoneSystem.instance != null
                ? ZoneSystem.instance.GetGroundHeight(Vector3.zero)
                : 2f;
            return new Vector3(0f, groundY + 1f, 0f);
        }

        private static IEnumerator DoTeleport(Player player, Vector3 dest)
        {
            yield return null; // 다음 프레임 대기

            try
            {
                // Valheim 포탈 방식 텔레포트 (distantTeleport=true: 존 로딩 포함)
                player.TeleportTo(dest, player.transform.rotation, true);

                Plugin.Log.LogInfo($"[Escape] TeleportTo 호출 완료 → {dest}");

                // 성공 메시지
                player.Message(MessageHud.MessageType.Center,
                    Localization.L.Get("ui_escape"), 0, null);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Escape] DoTeleport 오류: {ex.Message}");
            }
        }
    }
}

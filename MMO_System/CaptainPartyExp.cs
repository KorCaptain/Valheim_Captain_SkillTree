using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain Party Exp System
    /// EpicMMO 없을 때 파티 경험치 분배 지원
    /// - 킬러가 파티원에게 경험치 RPC 전송 (그룹 범위 70m, 70% 분배)
    /// - Groups 모드 리플렉션으로 소프트 의존 처리
    /// </summary>
    [HarmonyPatch]
    public static class CaptainPartyExp
    {
        // ──────────────────────────────────────────
        // RPC 이름
        // ──────────────────────────────────────────
        private const string RPC_PARTY = "CaptainSkillTree_AddPartyExp";
        private const float  GROUP_RANGE  = 70f;
        private const float  GROUP_FACTOR = 0.70f;

        // ──────────────────────────────────────────
        // Groups API 리플렉션 캐시
        // ──────────────────────────────────────────
        private static bool       _groupsChecked;
        private static MethodInfo _isLoadedMethod;
        private static MethodInfo _groupPlayersMethod;

        // m_lastHit 필드 캐시
        private static FieldInfo _lastHitField;
        private static bool      _lastHitChecked;

        // ──────────────────────────────────────────
        // RPC 등록: Game.SpawnPlayer 이후 (SpawnPlayer는 기존에 사용 확인됨)
        // ──────────────────────────────────────────

        [HarmonyPatch(typeof(Game), "SpawnPlayer")]
        [HarmonyPostfix]
        public static void Game_SpawnPlayer_Postfix_PartyRpc()
        {
            if (CaptainMMOBridge.UseEpicMMO) return;

            // ZRoutedRpc가 준비된 뒤 등록
            if (ZRoutedRpc.instance == null) return;

            // 중복 등록 방지
            try
            {
                ZRoutedRpc.instance.Register(RPC_PARTY,
                    new Action<long, int, Vector3, int>(RPC_ReceivePartyExp));
                Plugin.Log.LogInfo("[CaptainPartyExp] 파티 경험치 RPC 등록 완료");
            }
            catch { /* 이미 등록된 경우 무시 */ }
        }

        // ──────────────────────────────────────────
        // Character.OnDeath 패치 - 로컬 플레이어가 킬러일 때 파티 경험치 전송
        // ──────────────────────────────────────────

        [HarmonyPatch(typeof(Character), "OnDeath")]
        [HarmonyPostfix]
        public static void Character_OnDeath_PartyExp(Character __instance)
        {
            try
            {
                if (CaptainMMOBridge.UseEpicMMO) return;
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;
                if (__instance == null || __instance.IsPlayer() || __instance.IsTamed()) return;

                var localPlayer = Player.m_localPlayer;
                if (localPlayer == null || localPlayer.IsDead()) return;

                // 로컬 플레이어가 킬러인지 확인
                if (!IsKilledByLocalPlayer(__instance, localPlayer)) return;

                // 파티 분배 가능한지 확인
                if (!Groups_IsLoaded()) return;

                // 몬스터 경험치 계산
                string monsterName = GetMonsterNameClone(__instance);
                if (!CaptainMonsterExp.HasMonster(monsterName)) return;

                int baseExp    = CaptainMonsterExp.GetExp(monsterName);
                int maxExp     = CaptainMonsterExp.GetMaxExp(monsterName);
                int monsterLvl = CaptainMonsterExp.GetLevel(monsterName);
                int starLevel  = Mathf.Max(0, __instance.GetLevel() - 1);
                float lvlBonus = CaptainLevelConfig.ExpForLvlMonster?.Value ?? 1.5f;

                int killExp = baseExp + (int)(maxExp * lvlBonus * starLevel);
                if (CaptainLevelConfig.MobLevelPerStar?.Value ?? true)
                    monsterLvl += starLevel;

                var pos = __instance.transform.position;

                // 파티원에게 RPC 전송
                int sent = 0;
                foreach (var member in Groups_GetPlayers())
                {
                    string memberName = GetMemberName(member);
                    if (string.IsNullOrEmpty(memberName)) continue;
                    if (memberName == localPlayer.GetPlayerName()) continue;

                    long peerId = GetMemberPeerId(member);
                    if (peerId == 0) continue;

                    int sendExp = Mathf.RoundToInt(killExp * GROUP_FACTOR);
                    ZRoutedRpc.instance?.InvokeRoutedRPC(peerId, RPC_PARTY,
                        sendExp, pos, monsterLvl);
                    sent++;
                }

                if (sent > 0)
                    Plugin.Log.LogDebug($"[CaptainPartyExp] 파티 경험치 전송: {killExp} × {GROUP_FACTOR:P0} → {sent}명");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainPartyExp] OnDeath 처리 오류: {ex.Message}");
            }
        }

        // ──────────────────────────────────────────
        // RPC 수신 - 파티원으로서 경험치 받기
        // ──────────────────────────────────────────

        public static void RPC_ReceivePartyExp(long sender, int exp, Vector3 monsterPos, int monsterLevel)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || player.IsDead()) return;

                // 거리 체크
                if (Vector3.Distance(monsterPos, player.transform.position) >= GROUP_RANGE) return;

                // 레벨 보정 적용
                int finalExp = ApplyLevelCurve(exp, monsterLevel);
                if (finalExp <= 0) return;

                CaptainMMOBridge.AddExp(finalExp);
                Plugin.Log.LogDebug($"[CaptainPartyExp] 파티 경험치 수신: +{finalExp}");

                if (CaptainLevelConfig.ShowExpPopup?.Value ?? true)
                    CaptainExpHud.ShowExpGainPopup(finalExp);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainPartyExp] RPC_ReceivePartyExp 오류: {ex.Message}");
            }
        }

        // ──────────────────────────────────────────
        // 헬퍼: 로컬 플레이어가 킬러인지 확인
        // ──────────────────────────────────────────

        private static bool IsKilledByLocalPlayer(Character monster, Player localPlayer)
        {
            // m_lastHit 리플렉션으로 공격자 확인
            var lastHit = GetLastHit(monster);
            if (lastHit != null)
            {
                var attacker = lastHit.GetAttacker();
                if (attacker == localPlayer) return true;

                // 길들인 동물이 공격했고 근처 있으면
                if (attacker != null && attacker.IsTamed())
                {
                    if (Vector3.Distance(localPlayer.transform.position, attacker.transform.position) <= 50f)
                        return true;
                }

                // 다른 플레이어가 킬러라면 파티원으로서 처리 안 함 (그 플레이어의 클라이언트가 처리)
                if (attacker != null && attacker.IsPlayer()) return false;
            }

            // Fallback: 로컬 플레이어가 전투 범위(15m) 내에 있고 무기를 들고 있으면
            if (Vector3.Distance(localPlayer.transform.position, monster.transform.position) <= 15f)
            {
                if (localPlayer.GetCurrentWeapon() != null) return true;
            }

            return false;
        }

        private static HitData GetLastHit(Character character)
        {
            if (!_lastHitChecked)
            {
                _lastHitChecked = true;
                _lastHitField = typeof(Character).GetField("m_lastHit",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            }

            if (_lastHitField == null) return null;
            try { return _lastHitField.GetValue(character) as HitData; }
            catch { return null; }
        }

        private static string GetMonsterNameClone(Character character)
        {
            string prefab = Utils.GetPrefabName(character.gameObject);
            if (!string.IsNullOrEmpty(prefab)) return $"{prefab}(Clone)";
            return character.m_name ?? "";
        }

        // ──────────────────────────────────────────
        // 레벨 곡선 보정
        // ──────────────────────────────────────────

        private static int ApplyLevelCurve(int baseExp, int monsterLevel)
        {
            if (monsterLevel == 0) return baseExp;

            int   playerLvl = CaptainMMOBridge.GetLevel();
            int   maxRange  = CaptainLevelConfig.MaxLevelExp?.Value ?? 10;
            int   minRange  = CaptainLevelConfig.MinLevelExp?.Value ?? 10;
            bool  curveExp  = CaptainLevelConfig.CurveExp?.Value ?? false;
            bool  noExp     = CaptainLevelConfig.NoExpPastLevel?.Value ?? false;

            int result = baseExp;
            int maxRangeLvl = playerLvl + maxRange;
            int minRangeLvl = playerLvl - minRange;

            if (monsterLevel > maxRangeLvl)
            {
                if (noExp) return 0;
                if (curveExp) result = baseExp / Math.Max(1, monsterLevel - maxRangeLvl);
            }
            else if (monsterLevel < minRangeLvl)
            {
                if (noExp) return 0;
                if (curveExp) result = baseExp / Math.Max(1, minRangeLvl - monsterLevel);
            }

            return Math.Max(0, result);
        }

        // ──────────────────────────────────────────
        // Groups API 리플렉션
        // ──────────────────────────────────────────

        private static void EnsureGroupsApi()
        {
            if (_groupsChecked) return;
            _groupsChecked = true;

            try
            {
                var apiType = Type.GetType("Groups.API, Groups");
                if (apiType == null) return;

                _isLoadedMethod    = apiType.GetMethod("IsLoaded",
                    BindingFlags.Public | BindingFlags.Static);
                _groupPlayersMethod = apiType.GetMethod("GroupPlayers",
                    BindingFlags.Public | BindingFlags.Static);

                Plugin.Log.LogInfo("[CaptainPartyExp] Groups 모드 API 감지됨");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainPartyExp] Groups API 초기화 실패: {ex.Message}");
            }
        }

        private static bool Groups_IsLoaded()
        {
            EnsureGroupsApi();
            if (_isLoadedMethod == null) return false;
            try { return (bool)_isLoadedMethod.Invoke(null, null); }
            catch { return false; }
        }

        private static System.Collections.IEnumerable Groups_GetPlayers()
        {
            EnsureGroupsApi();
            if (_groupPlayersMethod == null) yield break;
            System.Collections.IEnumerable list = null;
            try { list = _groupPlayersMethod.Invoke(null, null) as System.Collections.IEnumerable; }
            catch { yield break; }
            if (list == null) yield break;
            foreach (var item in list) yield return item;
        }

        private static string GetMemberName(object member)
        {
            try { return member?.GetType().GetField("name")?.GetValue(member) as string; }
            catch { return null; }
        }

        private static long GetMemberPeerId(object member)
        {
            try
            {
                var f = member?.GetType().GetField("peerId");
                return f != null ? Convert.ToInt64(f.GetValue(member)) : 0L;
            }
            catch { return 0L; }
        }
    }
}

using System;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Character.OnDeath는 몬스터 ZDO 소유(owner) 클라이언트에서만 실행되므로, QuestKillPatch가
    /// 소유자 자신의 크레딧을 처리한 뒤 이 클래스를 통해 나머지 접속 중인 클라이언트에게도
    /// 처치 정보를 RPC로 전파한다. 각 수신 클라이언트는 자신의 로컬 플레이어 기준으로 거리를
    /// 확인한 뒤 QuestKillPatch.ApplyKillCredit을 호출해 스스로 진행도를 반영한다.
    /// MMO_System/CaptainPartyExp.cs의 RPC 등록/전송/수신 패턴을 그대로 따른다.
    /// </summary>
    [HarmonyPatch]
    public static class QuestKillPartySync
    {
        public const string RpcName = "CaptainSkillTree.QuestKillCredit";

        // CaptainMMOPatches.IsKilledByPlayerOrParty의 파티 판정 거리(50m)와 동일하게 유지한다.
        private const float PartyRange = 50f;

        [HarmonyPatch(typeof(Game), "SpawnPlayer")]
        [HarmonyPostfix]
        public static void Game_SpawnPlayer_Postfix_RegisterRpc()
        {
            if (ZRoutedRpc.instance == null) return;

            try
            {
                ZRoutedRpc.instance.Register(RpcName, new Action<long, string, Vector3>(RPC_ReceiveKillCredit));
            }
            catch { /* 이미 등록된 경우 무시 */ }
        }

        /// <summary>소유자 클라이언트가 몬스터 처치 후 호출. 접속 중인 다른 모든 피어에게 처치 정보를 전송한다.</summary>
        public static void BroadcastKillCredit(string prefabName, Vector3 pos)
        {
            try
            {
                if (ZNet.instance == null || ZRoutedRpc.instance == null) return;

                foreach (var peer in ZNet.instance.GetPeers())
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(peer.m_uid, RpcName, prefabName, pos);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[QuestKillPartySync] BroadcastKillCredit 오류: {ex.Message}");
            }
        }

        private static void RPC_ReceiveKillCredit(long sender, string prefabName, Vector3 pos)
        {
            try
            {
                if (!Quest_Config.IsEnabled) return;

                var player = Player.m_localPlayer;
                if (player == null || string.IsNullOrEmpty(prefabName)) return;

                if (Vector3.Distance(pos, player.transform.position) > PartyRange) return;

                QuestKillPatch.ApplyKillCredit(player, prefabName);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestKillPartySync] RPC_ReceiveKillCredit 오류: {ex.Message}");
            }
        }
    }
}

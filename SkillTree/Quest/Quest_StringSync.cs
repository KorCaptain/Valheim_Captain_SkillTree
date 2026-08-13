using System;
using System.Collections.Generic;
using System.Text;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Quest_Config의 문자열 필드(Type/TargetPrefab/보상 아이템명/특수보상)를 서버→클라이언트로
    /// 동기화한다. SkillTreeConfig.Broadcast.cs의 float 전용 파이프라인(250여 개 기존 Config가
    /// 이미 사용 중)과는 별도의 독립 RPC 채널을 써서, 기존 동기화에 영향을 주지 않는다.
    /// 동작 방식은 md/SERVER_SYNC_RULES.md의 float 파이프라인과 동일(서버만 전송, 클라이언트는
    /// 수신값을 GetEffectiveString()으로 우선 사용).
    /// </summary>
    public static class Quest_StringSync
    {
        public const string RpcName = "CaptainSkillTree.QuestStringSync";

        private static Dictionary<string, string> _serverStrings = new Dictionary<string, string>();
        private static bool _hasReceivedServerStrings = false;

        public static string GetEffectiveString(string key, string localValue)
        {
            if (!SkillTreeConfig.IsServer && _hasReceivedServerStrings && _serverStrings.TryGetValue(key, out var v))
                return v;
            return localValue;
        }

        /// <summary>서버에서만 실행. targetPeerId = 0이면 전체 클라이언트에게 전송.</summary>
        public static void BroadcastToClients(long targetPeerId = 0)
        {
            if (!SkillTreeConfig.IsServer) return;

            try
            {
                var data = new Dictionary<string, string>();
                foreach (var kvp in Quest_Config.Quests)
                {
                    string prefix = kvp.Key;
                    var q = kvp.Value;
                    data[Quest_Config.SyncKeyType(prefix)] = q.Type?.Value ?? "";
                    data[Quest_Config.SyncKeyTargetPrefab(prefix)] = q.TargetPrefab?.Value ?? "";
                    data[Quest_Config.SyncKeyItem1(prefix)] = q.ItemReward1?.Value ?? "";
                    data[Quest_Config.SyncKeyItem2(prefix)] = q.ItemReward2?.Value ?? "";
                    data[Quest_Config.SyncKeySpecialReward(prefix)] = q.SpecialReward?.Value ?? "";
                }

                var payload = Serialize(data);
                if (ZNet.instance != null && ZRoutedRpc.instance != null)
                {
                    long target = targetPeerId == 0 ? ZRoutedRpc.Everybody : targetPeerId;
                    ZRoutedRpc.instance.InvokeRoutedRPC(target, RpcName, payload);
                    string dest = targetPeerId == 0 ? "모든 클라이언트" : $"peerId={targetPeerId}";
                    Plugin.Log.LogInfo($"[Quest_StringSync] 퀘스트 문자열 설정 전송 → {dest} ({data.Count}개 항목)");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Quest_StringSync] BroadcastToClients 실패: {ex.Message}");
            }
        }

        public static void RPC_Receive(long sender, string payload)
        {
            try
            {
                var parsed = Deserialize(payload);
                if (parsed.Count > 0)
                {
                    _serverStrings = parsed;
                    _hasReceivedServerStrings = true;
                    QuestManager.InvalidateCache();
                    Plugin.Log.LogInfo($"[Quest_StringSync] 서버 퀘스트 문자열 설정 수신 ({parsed.Count}개 항목)");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Quest_StringSync] RPC_Receive 실패: {ex.Message}");
            }
        }

        private static string Serialize(Dictionary<string, string> data)
        {
            var sb = new StringBuilder();
            foreach (var kvp in data)
                sb.Append(kvp.Key).Append('=').Append(kvp.Value ?? "").Append('\n');
            return sb.ToString();
        }

        private static Dictionary<string, string> Deserialize(string payload)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(payload)) return result;
            var lines = payload.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                int idx = line.IndexOf('=');
                if (idx < 0) continue;
                string key = line.Substring(0, idx);
                string value = idx + 1 < line.Length ? line.Substring(idx + 1) : "";
                result[key] = value;
            }
            return result;
        }
    }
}

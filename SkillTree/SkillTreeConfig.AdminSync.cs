using System;
using BepInEx.Configuration;
using Jotunn.Managers;

namespace CaptainSkillTree.SkillTree
{
    public static partial class SkillTreeConfig
    {
        // RPC_AdminConfigUpdate가 직접 SetSerializedValue 하는 동안 서버 SettingChanged 브로드캐스트 중복 방지
        private static bool _isProcessingRpcUpdate = false;

        public static void SubscribeAdminConfigChanges(ConfigFile config)
        {
            config.SettingChanged += OnAdminSettingChanged;
            config.SettingChanged += OnServerSettingChanged;
            Plugin.Log.LogDebug("[AdminSync] 어드민 Config 변경 구독 등록 완료");
        }

        // 서버 측: Shudnal ConfigManager 등 외부 수단으로 config가 바뀔 때 브로드캐스트
        private static void OnServerSettingChanged(object sender, SettingChangedEventArgs e)
        {
            if (!_isServer) return;
            if (_isProcessingRpcUpdate) return; // 우리 RPC 처리 중이면 스킵 (RPC 완료 후 명시 호출)
            BroadcastConfigToClients();
        }

        private static void OnAdminSettingChanged(object sender, SettingChangedEventArgs e)
        {
            if (_isServer) return;
            if (SynchronizationManager.Instance?.PlayerIsAdmin != true) return;

            bool isAdminOnly = false;
            foreach (var tag in e.ChangedSetting.Description.Tags)
            {
                if (tag is ConfigurationManagerAttributes cma && cma.IsAdminOnly == true)
                {
                    isAdminOnly = true;
                    break;
                }
            }
            if (!isAdminOnly) return;

            // Shudnal ConfigManager가 설치된 경우 자체 어드민 동기화를 처리하므로 중복 RPC 방지
            if (IsShudnalConfigManagerPresent())
            {
                Plugin.Log.LogDebug("[AdminSync] Shudnal ConfigManager 감지 - 자체 RPC 스킵");
                return;
            }

            if (ZRoutedRpc.instance == null) return;

            string section = e.ChangedSetting.Definition.Section;
            string key = e.ChangedSetting.Definition.Key;
            string value = e.ChangedSetting.GetSerializedValue();
            string payload = $"{section}\t{key}\t{value}";

            long serverPeerId = ZNet.instance?.GetServerPeer()?.m_uid ?? 0L;
            ZRoutedRpc.instance.InvokeRoutedRPC(
                serverPeerId,
                "CaptainSkillTree.AdminConfigUpdate",
                payload);

            Plugin.Log.LogInfo($"[AdminSync] 서버 전송: [{section}] {key} = {value}");
        }

        private static bool IsShudnalConfigManagerPresent()
        {
            return BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("shudnal.ConfigurationManager");
        }

        public static void RPC_AdminConfigUpdate(long sender, string payload)
        {
            if (!_isServer) return;

            try
            {
                var peer = ZNet.instance?.GetPeer(sender);
                var steamId = peer?.m_rpc?.GetSocket()?.GetHostName();

                if (string.IsNullOrEmpty(steamId) ||
                    !(ZNet.instance.GetAdminList()?.Contains(steamId) ?? false))
                {
                    Plugin.Log.LogWarning($"[AdminSync] 비어드민 Config 변경 차단 (sender: {sender})");
                    return;
                }

                var parts = payload.Split('\t');
                if (parts.Length != 3)
                {
                    Plugin.Log.LogWarning($"[AdminSync] 잘못된 payload 형식: {payload}");
                    return;
                }

                string section = parts[0];
                string key = parts[1];
                string value = parts[2];

                if (_configFile == null)
                {
                    Plugin.Log.LogError("[AdminSync] _configFile이 null입니다.");
                    return;
                }

                var entry = _configFile[new ConfigDefinition(section, key)];
                if (entry != null)
                {
                    // SetSerializedValue가 SettingChanged를 발생시키므로 중복 broadcast 방지
                    _isProcessingRpcUpdate = true;
                    entry.SetSerializedValue(value);
                    _configFile.Save();
                    _isProcessingRpcUpdate = false;
                    BroadcastConfigToClients();
                    Plugin.Log.LogInfo($"[AdminSync] 서버 업데이트 완료: [{section}] {key} = {value}");
                }
                else
                {
                    Plugin.Log.LogWarning($"[AdminSync] Config 항목 없음: [{section}] {key}");
                }
            }
            catch (Exception ex)
            {
                _isProcessingRpcUpdate = false; // 예외 시에도 플래그 해제
                Plugin.Log.LogError($"[AdminSync] RPC_AdminConfigUpdate 실패: {ex.Message}");
            }
        }
    }
}

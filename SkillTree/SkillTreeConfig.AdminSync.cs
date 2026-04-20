using System;
using BepInEx.Configuration;
using Jotunn.Managers;

namespace CaptainSkillTree.SkillTree
{
    public static partial class SkillTreeConfig
    {
        public static void SubscribeAdminConfigChanges(ConfigFile config)
        {
            config.SettingChanged += OnAdminSettingChanged;
            Plugin.Log.LogDebug("[AdminSync] 어드민 Config 변경 구독 등록 완료");
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
                    entry.SetSerializedValue(value);
                    _configFile.Save();
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
                Plugin.Log.LogError($"[AdminSync] RPC_AdminConfigUpdate 실패: {ex.Message}");
            }
        }
    }
}

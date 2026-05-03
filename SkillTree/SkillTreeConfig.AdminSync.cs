using System;
using System.Collections.Generic;
using System.Text;
using BepInEx.Configuration;
using Jotunn.Managers;

namespace CaptainSkillTree.SkillTree
{
    public static partial class SkillTreeConfig
    {
        // RPC_AdminConfigUpdate가 직접 SetSerializedValue 하는 동안 서버 SettingChanged 브로드캐스트 중복 방지
        private static bool _isProcessingRpcUpdate = false;

        // ── 해결책 1: 실시간 싱크 토글 (기본 OFF → skillconfig sync 커맨드로 수동 적용) ──
        private static ConfigEntry<bool> _liveConfigSync;

        // ── 해결책 2: Debounce — 1.5초 이내 중복 브로드캐스트/RPC 차단 ──
        private static DateTime _lastServerBroadcastTime = DateTime.MinValue;
        private static DateTime _lastClientRpcTime = DateTime.MinValue;
        private static readonly TimeSpan _syncCooldown = TimeSpan.FromMilliseconds(1500);

        public static void SubscribeAdminConfigChanges(ConfigFile config)
        {
            // 실시간 싱크 토글 Config 바인딩 (로컬 전용, 서버 싱크 대상 아님)
            _liveConfigSync = config.Bind(
                "Skill_Tree_Base",
                "EnableLiveConfigSync",
                false,
                new ConfigDescription(
                    "F1 메뉴 실시간 Config 싱크 활성화.\n" +
                    "false (기본) = skillconfig sync 커맨드로만 적용 — 크래시 방지 권장\n" +
                    "true = F1 메뉴 변경 즉시 서버 전송 (1.5초 debounce 적용)"));

            config.SettingChanged += OnAdminSettingChanged;
            config.SettingChanged += OnServerSettingChanged;
            Plugin.Log.LogDebug("[AdminSync] 어드민 Config 변경 구독 등록 완료");
        }

        // 서버 측: Shudnal ConfigManager 등 외부 수단으로 config가 바뀔 때 브로드캐스트 (debounce 적용)
        private static void OnServerSettingChanged(object sender, SettingChangedEventArgs e)
        {
            if (!_isServer) return;
            if (_isProcessingRpcUpdate) return;

            // debounce: 1.5초 이내 재브로드캐스트 차단 (FileSystemWatcher 버스트 방어)
            if ((DateTime.UtcNow - _lastServerBroadcastTime) < _syncCooldown) return;
            _lastServerBroadcastTime = DateTime.UtcNow;

            BroadcastConfigToClients();
        }

        private static void OnAdminSettingChanged(object sender, SettingChangedEventArgs e)
        {
            if (_isServer) return;
            if (SynchronizationManager.Instance?.PlayerIsAdmin != true) return;

            // ── 해결책 1: 실시간 싱크 꺼져 있으면 즉시 종료 ──
            if (_liveConfigSync?.Value != true) return;

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

            // ── 해결책 2: 클라이언트→서버 RPC debounce ──
            if ((DateTime.UtcNow - _lastClientRpcTime) < _syncCooldown) return;
            _lastClientRpcTime = DateTime.UtcNow;

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

        /// <summary>
        /// 관리자 로컬 Config 전체를 서버에 일괄 전송 (skillconfig sync 커맨드 전용)
        /// IsAdminOnly 태그가 붙은 항목만 직렬화해서 배치 RPC로 전송
        /// </summary>
        /// <returns>전송된 항목 수</returns>
        public static int SendBatchSyncToServer()
        {
            if (_configFile == null)
            {
                Plugin.Log.LogError("[AdminSync] SendBatchSyncToServer: _configFile이 null입니다.");
                return 0;
            }

            var sb = new StringBuilder();
            int count = 0;

            foreach (var entry in _configFile)
            {
                bool isAdminOnly = false;
                foreach (var tag in entry.Value.Description.Tags)
                {
                    if (tag is ConfigurationManagerAttributes cma && cma.IsAdminOnly == true)
                    {
                        isAdminOnly = true;
                        break;
                    }
                }
                if (!isAdminOnly) continue;

                string section = entry.Key.Section;
                string key = entry.Key.Key;
                string value = entry.Value.GetSerializedValue();
                sb.Append($"{section}\t{key}\t{value}\n");
                count++;
            }

            if (count == 0)
            {
                Plugin.Log.LogWarning("[AdminSync] SendBatchSyncToServer: 전송할 IsAdminOnly 항목이 없습니다.");
                return 0;
            }

            string payload = sb.ToString();

            if (_isServer)
            {
                RPC_AdminBatchSync(0L, payload);
            }
            else
            {
                if (ZRoutedRpc.instance == null || ZNet.instance == null)
                {
                    Plugin.Log.LogError("[AdminSync] SendBatchSyncToServer: ZRoutedRpc 또는 ZNet이 null입니다.");
                    return 0;
                }
                long serverPeerId = ZNet.instance.GetServerPeer()?.m_uid ?? 0L;
                ZRoutedRpc.instance.InvokeRoutedRPC(
                    serverPeerId,
                    "CaptainSkillTree.AdminBatchSync",
                    payload);
            }

            Plugin.Log.LogInfo($"[AdminSync] 배치 싱크 전송 완료: {count}개 항목");
            return count;
        }

        /// <summary>
        /// 서버에서 관리자 일괄 Config 수신 처리 (RPC 핸들러)
        /// </summary>
        public static void RPC_AdminBatchSync(long sender, string payload)
        {
            if (!_isServer) return;

            try
            {
                // sender == 0 이면 서버 자신이 직접 호출한 것
                if (sender != 0L)
                {
                    var peer = ZNet.instance?.GetPeer(sender);
                    var steamId = peer?.m_rpc?.GetSocket()?.GetHostName();
                    if (string.IsNullOrEmpty(steamId) ||
                        !(ZNet.instance.GetAdminList()?.Contains(steamId) ?? false))
                    {
                        Plugin.Log.LogWarning($"[AdminSync] 비관리자 배치 싱크 차단 (sender: {sender})");
                        return;
                    }
                }

                if (_configFile == null)
                {
                    Plugin.Log.LogError("[AdminSync] RPC_AdminBatchSync: _configFile이 null입니다.");
                    return;
                }

                var lines = payload.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int applied = 0;

                _isProcessingRpcUpdate = true;
                foreach (var line in lines)
                {
                    var parts = line.Split('\t');
                    if (parts.Length != 3) continue;

                    string section = parts[0];
                    string key = parts[1];
                    string value = parts[2];

                    var entry = _configFile[new ConfigDefinition(section, key)];
                    if (entry != null)
                    {
                        entry.SetSerializedValue(value);
                        applied++;
                    }
                }
                _configFile.Save();
                _isProcessingRpcUpdate = false;

                BroadcastConfigToClients();
                Plugin.Log.LogInfo($"[AdminSync] 배치 싱크 완료: {applied}개 항목 적용");
            }
            catch (Exception ex)
            {
                _isProcessingRpcUpdate = false;
                Plugin.Log.LogError($"[AdminSync] RPC_AdminBatchSync 실패: {ex.Message}");
            }
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

                    // debounce: 1.5초 이내 재브로드캐스트 차단
                    if ((DateTime.UtcNow - _lastServerBroadcastTime) >= _syncCooldown)
                    {
                        _lastServerBroadcastTime = DateTime.UtcNow;
                        BroadcastConfigToClients();
                    }
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

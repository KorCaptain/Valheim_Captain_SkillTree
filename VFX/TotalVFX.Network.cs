using UnityEngine;

namespace CaptainSkillTree.VFX
{
    public static partial class TotalVFX
    {
        #region 네트워크 동기화 재생 (ZNetScene ZDO 방식)

        /// <summary>
        /// ZNetScene ZDO 동기화 VFX — 모든 클라이언트에 자동 표시.
        /// Patch_ZNetScene_RegisterVFXHost 없으면 TotalVFX.Play()로 fallback.
        /// </summary>
        public static GameObject PlayNetworkSync(string vfxName, Vector3 position, float duration = 5f)
        {
            if (!VFX_ENABLED) return null;
            if (ZNetScene.instance == null) return null;

            var hostPrefab = Patch_ZNetScene_RegisterVFXHost.HostPrefab;
            if (hostPrefab == null)
                return Play(vfxName, position, duration);

            // 비활성 상태로 Instantiate → SetActive(true) → ZDO 생성
            var host = UnityEngine.Object.Instantiate(hostPrefab, position, Quaternion.identity);
            host.SetActive(true);
            host.GetComponent<VFXNetworkController>()?.InitAsOwner(vfxName, duration);
            return host;
        }

        /// <summary>플레이어 위치에 ZNetScene 동기화 VFX (고정 위치, 버프 발동 시점용)</summary>
        public static GameObject PlayNetworkSyncOnPlayer(Player player, string vfxName, float duration = 5f)
        {
            if (!VFX_ENABLED || player == null) return null;
            return PlayNetworkSync(vfxName, player.transform.position + Vector3.up, duration);
        }

        #endregion

        #region RPC 수신 핸들러

        internal static void OnReceiveCustomVFX(long sender, string vfxName, Vector3 pos, float duration)
        {
            _isReceivingRPC = true;
            try   { Play(vfxName, pos, duration); }
            finally { _isReceivingRPC = false; }
        }

        internal static void OnReceivePlayOnPlayer(long sender, string vfxName, long playerID, float duration)
        {
            string key = $"{vfxName}_{playerID}";
            if (_recentLocalCreations.TryGetValue(key, out float t) && Time.time - t < 1f)
            {
                _recentLocalCreations.Remove(key);
                return;
            }
            _isReceivingRPC = true;
            try
            {
                foreach (var p in Player.GetAllPlayers())
                {
                    if (p != null && p.GetPlayerID() == playerID)
                    {
                        PlayOnPlayer(p, vfxName, duration);
                        break;
                    }
                }
            }
            finally { _isReceivingRPC = false; }
        }

        #endregion

        #region 내부 헬퍼

        private static GameObject GetOrFindPrefab(string vfxName)
        {
            if (_cachedPrefabs.TryGetValue(vfxName, out var prefab))
            {
                if (prefab != null) return prefab;
                _cachedPrefabs.Remove(vfxName);
            }
            prefab = FindPrefabInResources(vfxName);
            if (prefab != null) _cachedPrefabs[vfxName] = prefab;
            return prefab;
        }

        private static GameObject FindPrefabInResources(string prefabName)
        {
            try
            {
                foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (obj != null && obj.name == prefabName &&
                        (obj.scene.name == null || obj.scene.rootCount == 0))
                        return obj;
                }
            }
            catch { }
            return null;
        }

        #endregion

        #region 기존 VFXManager API 호환 (no-op 또는 위임)

        public static GameObject GetCachedPrefab(string vfxName) => GetPrefab(vfxName);
        public static GameObject GetVFXPrefab(string prefabName) => GetPrefab(prefabName);

        public static TotalVFXPlaybackMethod GetOptimalPlaybackMethod(string prefabName)
            => TotalVFXPlaybackMethod.Registry;

        public static void CachePrefabs()              { }
        public static void ClearCache()                { }
        public static void InitializeVFXRPC()          { }
        public static void ClearFailureList()          { }
        public static void RetryFailedPrefabs()        { }
        public static void LogStatus()                 { }
        public static void LogPrefabDetails(string n)  { }
        public static void DiagnoseCriticalPrefabs()   { }
        public static void DiagnoseHybridSystem()      { }
        public static void DeepDebugBuff01()           { }

        #endregion
    }
}

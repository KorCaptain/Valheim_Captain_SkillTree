using UnityEngine;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// VFX 매니저 - WackyMMO 방식 (단순화)
    /// 모든 VFX 기능 비활성화 - 무한 로딩 방지
    /// </summary>
    public static class VFXManager
    {
        /// <summary>
        /// VFX 시스템 전역 활성화 플래그 - 항상 false (비활성화)
        /// </summary>
        public static bool VFX_ENABLED = false;

        /// <summary>
        /// VFX 재생 - 모든 VFX 비활성화 상태
        /// </summary>
        public static void PlayVFXMultiplayer(string vfxName, string soundName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f, float scale = 1f)
        {
            // 모든 VFX 비활성화 상태
            return;
        }

        public static GameObject PlayVFXAttachedToPlayer(Player player, string vfxName,
            string soundName, float duration, Vector3 offset = default)
        {
            return null;
        }

        public static void PlayVFX(string prefabName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f) { }

        public static void PlayVFXOnPlayer(string prefabName, Player player,
            float destroyAfter = 5f) { }

        public static void PlayVFXNetworked(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f) { }

        public static void PlayVFXNetworkedOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f) { }

        public static void PlayVFXOptimized(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f, bool isNetworked = false) { }

        public static void PlayVFXHybrid(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f) { }

        public static void PlayVFXHybridOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f) { }

        public static GameObject GetVFXPrefab(string prefabName) => null;

        public static void Initialize() { }

        public static void InitializeVFXRPC() { }

        public static void ClearCache() { }

        public static void ClearFailureList() { }

        public static void RetryFailedPrefabs() { }

        public static void LogStatus() { }

        public static void LogPrefabDetails(string prefabName) { }

        public static void DiagnoseCriticalPrefabs() { }

        public static void DiagnoseHybridSystem() { }

        public static void DeepDebugBuff01() { }

        public static VFXPlaybackMethod GetOptimalPlaybackMethod(string prefabName) => VFXPlaybackMethod.Fallback;
    }

    /// <summary>
    /// VFX 재생 방식 열거형
    /// </summary>
    public enum VFXPlaybackMethod
    {
        Registry,
        RPC,
        Fallback
    }
}

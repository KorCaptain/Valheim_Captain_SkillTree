using UnityEngine;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// VFX 매니저 - SimpleVFX 래퍼
    /// 모든 VFX 호출을 SimpleVFX로 위임 (ZNetScene.GetPrefab 사용 금지)
    /// </summary>
    public static class VFXManager
    {
        /// <summary>
        /// VFX 시스템 전역 활성화 플래그
        /// </summary>
        public static bool VFX_ENABLED = true;

        #region SimpleVFX 래퍼 메서드

        /// <summary>
        /// VFX 프리팹 가져오기 → SimpleVFX.GetPrefab 사용
        /// </summary>
        public static GameObject GetCachedPrefab(string vfxName)
        {
            return SimpleVFX.GetPrefab(vfxName);
        }

        /// <summary>
        /// VFX 프리팹 가져오기 (기존 API 호환)
        /// </summary>
        public static GameObject GetVFXPrefab(string prefabName)
        {
            return SimpleVFX.GetPrefab(prefabName);
        }

        /// <summary>
        /// 플레이어를 따라다니는 VFX → SimpleVFX.PlayOnPlayer
        /// </summary>
        public static GameObject PlayVFXFollowPlayer(Player player, string vfxName, float duration = 5f, Vector3 localScale = default)
        {
            if (!VFX_ENABLED || player == null) return null;
            return SimpleVFX.PlayOnPlayer(player, vfxName, duration);
        }

        /// <summary>
        /// 고정 위치 VFX → SimpleVFX.Play
        /// </summary>
        public static GameObject PlayVFXAtPosition(string vfxName, Vector3 position, float duration = 5f, float scale = 1f)
        {
            if (!VFX_ENABLED) return null;
            return SimpleVFX.Play(vfxName, position, duration);
        }

        /// <summary>
        /// 플레이어 위치에 VFX (고정, 따라다니지 않음)
        /// </summary>
        public static GameObject PlayVFXOnPlayer(string vfxName, Player player, float duration = 5f, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;
            if (offset == default) offset = Vector3.up * 1f;
            return SimpleVFX.Play(vfxName, player.transform.position + offset, duration);
        }

        /// <summary>
        /// 사운드 재생 → SimpleVFX.Play
        /// </summary>
        public static void PlaySound(string soundName, Vector3 position, float duration = 5f)
        {
            if (string.IsNullOrEmpty(soundName)) return;
            SimpleVFX.Play(soundName, position, duration);
        }

        #endregion

        #region 기존 API 호환 메서드

        /// <summary>
        /// VFX + 사운드 동시 재생 → SimpleVFX.PlayWithSound
        /// </summary>
        public static void PlayVFXMultiplayer(string vfxName, string soundName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f, float scale = 1f)
        {
            if (!VFX_ENABLED) return;
            SimpleVFX.PlayWithSound(vfxName, soundName, position, destroyAfter);
        }

        /// <summary>
        /// 플레이어에 부착된 VFX → SimpleVFX.PlayOnPlayer
        /// </summary>
        public static GameObject PlayVFXAttachedToPlayer(Player player, string vfxName,
            string soundName, float duration, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;

            // VFX 재생 (캐릭터 따라다님)
            var vfxObj = SimpleVFX.PlayOnPlayer(player, vfxName, duration, offset == default ? null : (Vector3?)offset);

            // 사운드 재생
            if (!string.IsNullOrEmpty(soundName))
                SimpleVFX.Play(soundName, player.transform.position, duration);

            return vfxObj;
        }

        /// <summary>
        /// VFX 단순 재생
        /// </summary>
        public static void PlayVFX(string prefabName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f)
        {
            if (!VFX_ENABLED) return;
            SimpleVFX.Play(prefabName, position, destroyAfter);
        }

        /// <summary>
        /// 네트워크 VFX 재생
        /// </summary>
        public static void PlayVFXNetworked(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED) return;
            SimpleVFX.PlayWithSound(prefabName, soundName, position, destroyAfter);
        }

        /// <summary>
        /// 플레이어에 네트워크 VFX 재생
        /// </summary>
        public static void PlayVFXNetworkedOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED || player == null) return;
            SimpleVFX.PlayWithSound(prefabName, soundName, player.transform.position + Vector3.up * 1f, destroyAfter);
        }

        /// <summary>
        /// 최적화된 VFX 재생
        /// </summary>
        public static void PlayVFXOptimized(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f, bool isNetworked = false)
        {
            if (!VFX_ENABLED) return;
            SimpleVFX.PlayWithSound(prefabName, soundName, position, destroyAfter);
        }

        /// <summary>
        /// 하이브리드 VFX 재생
        /// </summary>
        public static void PlayVFXHybrid(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED) return;
            SimpleVFX.PlayWithSound(prefabName, soundName, position, destroyAfter);
        }

        /// <summary>
        /// 플레이어에 하이브리드 VFX 재생
        /// </summary>
        public static void PlayVFXHybridOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED || player == null) return;
            SimpleVFX.PlayWithSound(prefabName, soundName, player.transform.position + Vector3.up * 1f, destroyAfter);
        }

        #endregion

        #region 초기화 및 유틸리티 (빈 메서드 - SimpleVFX가 처리)

        public static void Initialize() { }
        public static void CachePrefabs() { }
        public static void InitializeVFXRPC() { }
        public static void ClearCache() { }
        public static void ClearFailureList() { }
        public static void RetryFailedPrefabs() { }
        public static void LogStatus() { }
        public static void LogPrefabDetails(string prefabName) { }
        public static void DiagnoseCriticalPrefabs() { }
        public static void DiagnoseHybridSystem() { }
        public static void DeepDebugBuff01() { }
        public static VFXPlaybackMethod GetOptimalPlaybackMethod(string prefabName) => VFXPlaybackMethod.Registry;

        #endregion
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

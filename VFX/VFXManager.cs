using System;
using HarmonyLib;
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
        /// VFX + 사운드 동시 재생 → rotation이 지정된 경우 SimpleVFX.PlayWithRotation, 아니면 PlayWithSound
        /// </summary>
        public static void PlayVFXMultiplayer(string vfxName, string soundName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f, float scale = 1f)
        {
            if (!VFX_ENABLED) return;
            if (rotation != default && rotation != Quaternion.identity)
                SimpleVFX.PlayWithRotation(vfxName, soundName, position, rotation, destroyAfter);
            else
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
        /// 발헤임 기본 VFX를 플레이어에 부착하여 따라다니게 재생
        /// ZNetView(자식 포함) 전체 제거 → 무한 로딩 방지
        /// </summary>
        public static GameObject PlayVFXFollowPlayer(Player player, string vfxName,
            string soundName, float duration, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;

            try
            {
                var prefab = ZNetScene.instance?.GetPrefab(vfxName);
                if (prefab == null) return null;

                var vfxObj = UnityEngine.Object.Instantiate(prefab, player.transform);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = offset == default ? new Vector3(0f, 0.5f, 0f) : offset;

                    // ZNetView 전체 제거 (자식 포함) → 무한 로딩 방지
                    // physics 콜백 컨텍스트에서 DestroyImmediate 불가 → disabled 후 Destroy
                    foreach (var nv in vfxObj.GetComponentsInChildren<ZNetView>())
                    {
                        nv.enabled = false;
                        UnityEngine.Object.Destroy(nv);
                    }

                    UnityEngine.Object.Destroy(vfxObj, duration);
                }

                if (!string.IsNullOrEmpty(soundName))
                    SimpleVFX.Play(soundName, player.transform.position, duration);

                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[VFXManager] PlayVFXFollowPlayer({vfxName}) 실패: {ex.Message}");
                return null;
            }
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

        #region ZNetScene 동기화 VFX (Orbs.cs 패턴 — 모든 클라이언트에 자동 표시)

        // ZNetScene에 등록된 CaptainVFXHost 프리팹을 통해 VFX를 네트워크 동기화 재생
        // SimpleVFX(RPC) 방식과 달리 Valheim 내장 ZDO 동기화를 활용하여 안정적으로 다른 플레이어에게 보임
        public static GameObject PlayVFXNetworkSync(string vfxName, Vector3 position, float duration = 5f)
        {
            if (!VFX_ENABLED) return null;
            if (ZNetScene.instance == null) return null;

            var hostPrefab = Patch_ZNetScene_RegisterVFXHost.HostPrefab;
            if (hostPrefab == null)
                return SimpleVFX.Play(vfxName, position, duration); // fallback

            // 비활성 상태로 Instantiate → Awake 지연
            var host = UnityEngine.Object.Instantiate(hostPrefab, position, Quaternion.identity);

            // SetActive(true) → ZNetView.Awake() → ZDO 생성
            host.SetActive(true);

            // ZDO에 VFX 정보 기록 (다른 클라이언트가 Start()에서 읽음)
            host.GetComponent<VFXNetworkController>()?.InitAsOwner(vfxName, duration);

            return host;
        }

        // 플레이어 위치에 ZNetScene 동기화 VFX 재생 (고정 위치, 버프 발동 시점용)
        public static GameObject PlayVFXNetworkSyncOnPlayer(Player player, string vfxName, float duration = 5f)
        {
            if (!VFX_ENABLED || player == null) return null;
            return PlayVFXNetworkSync(vfxName, player.transform.position + Vector3.up, duration);
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

    /// <summary>
    /// Valheim 내부 ShieldDomeParticleColor.Start()의 NullReferenceException 억제
    /// fx_shieldgenerator_domehit 프리팹 사용 시 발생하는 게임 내부 버그 우회
    /// </summary>
    [HarmonyPatch(typeof(ShieldDomeParticleColor), "Start")]
    internal static class Patch_ShieldDomeParticleColor_Start
    {
        static Exception Finalizer(Exception __exception) => null;
    }
}

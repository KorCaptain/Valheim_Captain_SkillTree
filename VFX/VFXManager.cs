using UnityEngine;
using System.Collections.Generic;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// VFX 매니저 - WackyEpicMMOSystem 방식 적용
    /// ZNetView 제거 없이 사용, 플레이어 본에 SetParent로 캐릭터 추적
    /// </summary>
    public static class VFXManager
    {
        /// <summary>
        /// VFX 시스템 전역 활성화 플래그
        /// </summary>
        public static bool VFX_ENABLED = true;

        /// <summary>
        /// 캐시된 VFX 프리팹 (ZNetScene.Awake에서 초기화)
        /// </summary>
        private static Dictionary<string, GameObject> _cachedPrefabs = new Dictionary<string, GameObject>();

        /// <summary>
        /// 플레이어 본(Bone) 경로 - 캐릭터 추적 VFX용
        /// </summary>
        private const string PLAYER_BONE_PATH = "Visual/Armature/Hips/Spine/Spine1/Spine2";

        #region 프리팹 캐싱 시스템

        /// <summary>
        /// VFX 프리팹 캐싱 (ZNetScene.Awake Postfix에서 호출)
        /// WackyEpicMMOSystem 방식: Resources.FindObjectsOfTypeAll로 Valheim 내장 프리팹 캐싱
        /// </summary>
        public static void CachePrefabs()
        {
            _cachedPrefabs.Clear();

            // WackyEpicMMO 방식: Resources.FindObjectsOfTypeAll로 Valheim 내장 프리팹 캐싱
            // ZNetScene.GetPrefab() 대신 이미 생성된 프리팹을 직접 찾아 캐싱 → ZNetView 충돌 방지
            string[] targetVfxNames = new string[]
            {
                "fx_guardstone_permitted_add",
                "fx_Float",
                "fx_creature_tamed",
                "sfx_coins_pile_destroyed",
                "vfx_Potion_stamina_medium",
                "vfx_Potion_health_medium",
                "vfx_spawn",
                "fx_eikthyr_forwardshockwave",
                "vfx_corpse_destruction_small",
                "fx_shaman_fireball_expl",
                "sfx_build_hammer_stone",
                "sfx_sword_hit",
                "fx_crit",
                "fx_seeker_hurt",
                "vfx_klubba_hit_ground",
                "vfx_GoblinBrute_Groundslam",
                "vfx_FireBallHit",
                // 성기사/힐러 모드용 VFX 추가
                "fx_greydwarf_shaman_heal",
                "vfx_greydwarf_shaman_heal",
                "sfx_greydwarf_shaman_heal_start",
                "sfx_greydwarf_shaman_heal_loop",
                "sfx_greydwarf_shaman_heal_end",
                "vfx_HealthUpgrade",
                "sfx_dverger_heal_finish"
            };

            // Resources.FindObjectsOfTypeAll로 프리팹 찾기 (WackyEpicMMO 방식 - ZNetView 충돌 방지)
            GameObject[] allPrefabs = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in allPrefabs)
            {
                if (obj == null) continue;

                // 타겟 VFX 이름과 매칭
                foreach (string targetName in targetVfxNames)
                {
                    if (obj.name == targetName && !_cachedPrefabs.ContainsKey(targetName))
                    {
                        _cachedPrefabs[targetName] = obj;
                    }
                }

                // 추가 패턴 매칭 (WackyEpicMMO 방식)
                if (obj.name.Contains("fx_seeker_hurt") && !_cachedPrefabs.ContainsKey("fx_seeker_crit"))
                {
                    _cachedPrefabs["fx_seeker_crit"] = obj;
                }
                if (obj.name.Contains("vfx_Potion") && !_cachedPrefabs.ContainsKey(obj.name))
                {
                    _cachedPrefabs[obj.name] = obj;
                }
                if (obj.name.Contains("greydwarf_shaman") && !_cachedPrefabs.ContainsKey(obj.name))
                {
                    _cachedPrefabs[obj.name] = obj;
                }
            }

            Plugin.Log?.LogInfo($"[VFX] 프리팹 캐싱 완료: {_cachedPrefabs.Count}개");
        }

        /// <summary>
        /// 캐시된 프리팹 가져오기
        /// </summary>
        public static GameObject GetCachedPrefab(string vfxName)
        {
            // 1. 캐시에서 찾기
            if (_cachedPrefabs.TryGetValue(vfxName, out var cached))
                return cached;

            // 2. ZNetScene에서 찾기
            var prefab = ZNetScene.instance?.GetPrefab(vfxName);
            if (prefab != null)
            {
                _cachedPrefabs[vfxName] = prefab;
                return prefab;
            }

            return null;
        }

        #endregion

        #region 캐릭터 추적 VFX (WackyEpicMMO levelUp 방식)

        /// <summary>
        /// 플레이어 본에 부착된 VFX 재생 - 캐릭터를 따라다님
        /// WackyEpicMMOSystem PlayerFVX.levelUp() 방식
        /// </summary>
        public static GameObject PlayVFXFollowPlayer(Player player, string vfxName, float duration = 5f, Vector3 localScale = default)
        {
            if (!VFX_ENABLED || player == null) return null;

            try
            {
                var prefab = GetCachedPrefab(vfxName);
                if (prefab == null)
                {
                    Plugin.Log?.LogWarning($"[VFX] '{vfxName}' 프리팹을 찾을 수 없음");
                    return null;
                }

                // 플레이어 본(Bone) 찾기
                Transform parent = player.transform.Find(PLAYER_BONE_PATH);
                if (parent == null)
                {
                    // 본을 못 찾으면 플레이어 Transform 사용
                    parent = player.transform;
                    Plugin.Log?.LogWarning($"[VFX] 플레이어 본을 찾을 수 없어 플레이어 Transform 사용");
                }

                // WackyEpicMMO 방식: 부모에 부착하여 생성
                var vfxObj = Object.Instantiate(prefab, parent);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = Vector3.zero;

                    if (localScale != default)
                        vfxObj.transform.localScale = localScale;

                    Object.Destroy(vfxObj, duration);
                }

                return vfxObj;
            }
            catch (System.Exception ex)
            {
                Plugin.Log?.LogError($"[VFX] PlayVFXFollowPlayer 오류: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region 고정 위치 VFX (WackyEpicMMO CriticalVFX 방식)

        /// <summary>
        /// 고정 위치에 VFX 재생
        /// WackyEpicMMOSystem CritDmgVFX.CriticalVFX() 방식
        /// </summary>
        public static GameObject PlayVFXAtPosition(string vfxName, Vector3 position, float duration = 5f, float scale = 1f)
        {
            if (!VFX_ENABLED) return null;

            try
            {
                var prefab = GetCachedPrefab(vfxName);
                if (prefab == null)
                {
                    Plugin.Log?.LogWarning($"[VFX] '{vfxName}' 프리팹을 찾을 수 없음");
                    return null;
                }

                // WackyEpicMMO 방식: 위치에 직접 생성 (ZNetView 제거 없음!)
                var vfxObj = Object.Instantiate(prefab, position, Quaternion.identity);
                if (vfxObj != null)
                {
                    if (scale != 1f)
                        vfxObj.transform.localScale = Vector3.one * scale;

                    Object.Destroy(vfxObj, duration);
                }

                return vfxObj;
            }
            catch (System.Exception ex)
            {
                Plugin.Log?.LogError($"[VFX] PlayVFXAtPosition 오류: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 플레이어 위치에 VFX 재생 (고정 위치, 따라다니지 않음)
        /// </summary>
        public static GameObject PlayVFXOnPlayer(string vfxName, Player player, float duration = 5f, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;

            if (offset == default)
                offset = Vector3.up * 1f;

            return PlayVFXAtPosition(vfxName, player.transform.position + offset, duration);
        }

        #endregion

        #region 사운드 재생

        /// <summary>
        /// 사운드 재생
        /// </summary>
        public static void PlaySound(string soundName, Vector3 position, float duration = 5f)
        {
            if (string.IsNullOrEmpty(soundName)) return;

            try
            {
                var soundPrefab = GetCachedPrefab(soundName);
                if (soundPrefab == null)
                    soundPrefab = ZNetScene.instance?.GetPrefab(soundName);

                if (soundPrefab != null)
                {
                    var soundObj = Object.Instantiate(soundPrefab, position, Quaternion.identity);
                    if (soundObj != null)
                        Object.Destroy(soundObj, duration);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log?.LogError($"[VFX] PlaySound 오류: {ex.Message}");
            }
        }

        #endregion

        #region 호환성 유지 메서드 (기존 API)

        /// <summary>
        /// VFX + 사운드 동시 재생 (기존 PlayVFXMultiplayer 대체)
        /// </summary>
        public static void PlayVFXMultiplayer(string vfxName, string soundName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f, float scale = 1f)
        {
            if (!VFX_ENABLED) return;

            // VFX 재생
            if (!string.IsNullOrEmpty(vfxName))
                PlayVFXAtPosition(vfxName, position, destroyAfter, scale);

            // 사운드 재생
            if (!string.IsNullOrEmpty(soundName))
                PlaySound(soundName, position, destroyAfter);
        }

        /// <summary>
        /// 플레이어에 부착된 VFX 재생 (기존 API 호환)
        /// </summary>
        public static GameObject PlayVFXAttachedToPlayer(Player player, string vfxName,
            string soundName, float duration, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;

            // VFX 재생 (캐릭터 따라다님)
            var vfxObj = PlayVFXFollowPlayer(player, vfxName, duration);

            // 사운드 재생
            if (!string.IsNullOrEmpty(soundName))
                PlaySound(soundName, player.transform.position, duration);

            return vfxObj;
        }

        /// <summary>
        /// VFX 단순 재생 (기존 API 호환)
        /// </summary>
        public static void PlayVFX(string prefabName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f)
        {
            PlayVFXAtPosition(prefabName, position, destroyAfter);
        }

        /// <summary>
        /// 네트워크 VFX 재생 (기존 API 호환)
        /// </summary>
        public static void PlayVFXNetworked(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f)
        {
            PlayVFXMultiplayer(prefabName, soundName, position, rotation, destroyAfter, 1f);
        }

        /// <summary>
        /// 플레이어에 네트워크 VFX 재생 (기존 API 호환)
        /// </summary>
        public static void PlayVFXNetworkedOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f)
        {
            if (player == null) return;
            PlayVFXMultiplayer(prefabName, soundName, player.transform.position + Vector3.up * 1f,
                Quaternion.identity, destroyAfter, 1f);
        }

        /// <summary>
        /// 최적화된 VFX 재생 (기존 API 호환)
        /// </summary>
        public static void PlayVFXOptimized(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f, bool isNetworked = false)
        {
            PlayVFXMultiplayer(prefabName, soundName, position, rotation, destroyAfter, 1f);
        }

        /// <summary>
        /// 하이브리드 VFX 재생 (기존 API 호환)
        /// </summary>
        public static void PlayVFXHybrid(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f)
        {
            PlayVFXMultiplayer(prefabName, soundName, position, rotation, destroyAfter, 1f);
        }

        /// <summary>
        /// 플레이어에 하이브리드 VFX 재생 (기존 API 호환)
        /// </summary>
        public static void PlayVFXHybridOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f)
        {
            if (player == null) return;
            PlayVFXMultiplayer(prefabName, soundName, player.transform.position + Vector3.up * 1f,
                Quaternion.identity, destroyAfter, 1f);
        }

        /// <summary>
        /// VFX 프리팹 가져오기 (기존 API 호환)
        /// </summary>
        public static GameObject GetVFXPrefab(string prefabName)
        {
            return GetCachedPrefab(prefabName);
        }

        #endregion

        #region 초기화 및 유틸리티

        /// <summary>
        /// 초기화 (ZNetScene.Awake에서 호출)
        /// </summary>
        public static void Initialize()
        {
            CachePrefabs();
        }

        public static void InitializeVFXRPC() { }
        public static void ClearCache() => _cachedPrefabs.Clear();
        public static void ClearFailureList() { }
        public static void RetryFailedPrefabs() { }

        public static void LogStatus()
        {
            Plugin.Log?.LogInfo($"[VFX] 캐시된 프리팹: {_cachedPrefabs.Count}개, VFX 활성화: {VFX_ENABLED}");
        }

        public static void LogPrefabDetails(string prefabName)
        {
            var prefab = GetCachedPrefab(prefabName);
            Plugin.Log?.LogInfo($"[VFX] '{prefabName}' → {(prefab != null ? "✅ 존재" : "❌ 없음")}");
        }

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

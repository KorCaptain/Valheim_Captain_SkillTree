using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// TotalVFX - 커스텀 VFX와 발헤임 기본 VFX를 하나의 진입점으로 통합.
    /// IsCustomVFX()로 자동 판별하여 적절한 방식 적용.
    /// - 커스텀 VFX: Instantiate + Destroy, RPC 브로드캐스트
    /// - 발헤임 기본 VFX: 순수 Instantiate (Destroy 금지), VFXFollowBehaviour 추적
    /// </summary>
    public static partial class TotalVFX
    {
        #region Fields

        private static readonly Dictionary<string, GameObject> _cachedPrefabs
            = new Dictionary<string, GameObject>();
        private static AssetBundle _debuffBundle = null;
        private static bool _initialized = false;
        private static bool _isReceivingRPC = false;
        private static readonly Dictionary<string, float> _recentLocalCreations
            = new Dictionary<string, float>();
        private static readonly Dictionary<string, float> _vfxDimMapping
            = new Dictionary<string, float>();

        public static bool VFX_ENABLED = true;
        public static GameObject PlayerVFX = null;
        public static GameObject MonsterVFX = null;

        // 커스텀 VFX 전용 RPC 이름 (SimpleVFX의 RPC와 충돌 방지)
        internal const string RPC_PLAY_CUSTOM    = "CaptainTVFX_PlayCustom";
        internal const string RPC_PLAY_ON_PLAYER = "CaptainTVFX_PlayOnPlayer";

        private static readonly HashSet<string> _customVFXNames = new HashSet<string>
        {
            "area_circles_blue", "area_fire_red", "area_heal_green",
            "area_magic_multicolor", "area_star_ellow",
            "buff_01", "buff_02a", "buff_03a", "buff_03a_aura",
            "debuff", "debuff_03", "debuff_03_aura",
            "statusailment_01", "statusailment_01_aura",
            "confetti_blast_multicolor", "confetti_directional_multicolor",
            "dust_permanently_blue",
            "flash_blue_purple", "flash_ellow", "flash_ellow_pink",
            "flash_magic_blue_pink", "flash_magic_ellow_blue", "flash_round_ellow",
            "flash_star_ellow_green", "flash_star_ellow_purple",
            "guard_01", "healing",
            "hit_01", "hit_02", "hit_03", "hit_04",
            "plexus",
            "shine_blue", "shine_ellow", "shine_pink",
            "sparkle_ellow",
            "taunt",
            "water_blast_blue", "water_blast_green"
        };

        #endregion

        #region 초기화

        public static void Initialize()
        {
            if (_initialized) return;
            try
            {
                LoadFromPrefabRegistry();
                LoadDebuffBundle();
                CacheValheimPrefabs();
                _initialized = true;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] 초기화 실패: {ex.Message}");
            }
        }

        private static void LoadFromPrefabRegistry()
        {
            try
            {
                var allPrefabs = CaptainSkillTree.Prefab.PrefabRegistry.GetAllRegisteredPrefabs();
                foreach (var kvp in allPrefabs)
                {
                    string name = kvp.Key;
                    GameObject prefab = kvp.Value;
                    if (prefab == null) continue;

                    if (_customVFXNames.Contains(name) && !_cachedPrefabs.ContainsKey(name))
                        _cachedPrefabs[name] = prefab;

                    string lower = name.ToLowerInvariant();
                    if (_customVFXNames.Contains(lower) && !_cachedPrefabs.ContainsKey(lower))
                        _cachedPrefabs[lower] = prefab;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[TotalVFX] PrefabRegistry 로드 실패: {ex.Message}");
            }
        }

        private static void LoadDebuffBundle()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = null;
                foreach (var n in assembly.GetManifestResourceNames())
                {
                    if (n.EndsWith(".debuff") || n.Contains(".VFX.debuff"))
                    {
                        resourceName = n;
                        break;
                    }
                }
                if (resourceName == null) return;
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null) return;
                    _debuffBundle = AssetBundle.LoadFromStream(stream);
                }
                if (_debuffBundle != null)
                {
                    var assets = _debuffBundle.LoadAllAssets<GameObject>();
                    if (assets.Length > 0)
                    {
                        _cachedPrefabs["debuff"] = assets[0];
                        PlayerVFX = assets[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] debuff 번들 로드 실패: {ex.Message}");
            }
        }

        public static void CacheValheimPrefabs()
        {
            try
            {
                string[] vfxNames = {
                    "fx_seeker_hurt", "fx_backstab", "fx_crit",
                    "vfx_HealthUpgrade", "shaman_heal_aoe", "fx_greydwarf_shaman_heal",
                    "buff_03a", "buff_03a_aura", "buff_02a", "statusailment_01_aura",
                    "vfx_GodExplosion", "fx_siegebomb_explosion", "fx_Fader_Roar",
                    "fx_Lightning", "fx_fader_meteor_hit", "fx_bow_hit",
                    "vfx_spawn_small", "vfx_spawn_large", "flash_blue_purple",
                    "fx_guardstone_activate", "fx_guardstone_permitted_add",
                    "fx_eikthyr_stomp", "fx_Fader_Spin", "debuff_03",
                    "fx_Fader_Roar_Projectile_Hit", "fx_Fader_Fissure_Prespawn",
                    "sfx_morgen_alert", "sfx_dverger_heal_finish", "sfx_oozebomb_explode"
                };

                foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (obj == null) continue;
                    if (obj.scene.name != null && obj.scene.rootCount > 0) continue;

                    if (obj.name.Contains("fx_seeker_hurt") && MonsterVFX == null)
                    {
                        MonsterVFX = obj;
                        _cachedPrefabs["monster"] = obj;
                    }
                    foreach (string n in vfxNames)
                    {
                        if (obj.name == n && !_cachedPrefabs.ContainsKey(n))
                            _cachedPrefabs[n] = obj;
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] Valheim 프리팹 캐싱 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 발헤임 기본 VFX를 커스텀으로 등록 (ZNetView 제거 → Destroy 안전).
        /// 반드시 ZNetScene.Awake Postfix에서 호출할 것.
        /// </summary>
        public static void RegisterAsCustom(string vfxName)
        {
            if (_customVFXNames.Contains(vfxName)) return;
            if (ZNetScene.instance == null) return;

            var original = ZNetScene.instance.GetPrefab(vfxName);
            if (original == null) return;

            bool wasActive = original.activeSelf;
            original.SetActive(false);
            var clone = UnityEngine.Object.Instantiate(original);
            original.SetActive(wasActive);

            clone.name = vfxName;
            UnityEngine.Object.DontDestroyOnLoad(clone);

            foreach (var nv in clone.GetComponentsInChildren<ZNetView>(true))
                UnityEngine.Object.DestroyImmediate(nv);
            foreach (var st in clone.GetComponentsInChildren<ZSyncTransform>(true))
                UnityEngine.Object.DestroyImmediate(st);
            foreach (var sa in clone.GetComponentsInChildren<ZSyncAnimation>(true))
                UnityEngine.Object.DestroyImmediate(sa);
            foreach (var proj in clone.GetComponentsInChildren<Projectile>(true))
                UnityEngine.Object.DestroyImmediate(proj);

            clone.SetActive(false);
            _customVFXNames.Add(vfxName);
            _cachedPrefabs[vfxName] = clone;
        }

        public static void RegisterVFXDim(string vfxName, float factor)
            => _vfxDimMapping[vfxName] = factor;

        #endregion

        #region 상태 / 조회

        public static bool IsInitialized => _initialized && (PlayerVFX != null || MonsterVFX != null);

        public static bool IsCustomVFX(string vfxName)
            => !string.IsNullOrEmpty(vfxName) && _customVFXNames.Contains(vfxName);

        public static GameObject GetPrefab(string type)
        {
            if (type == "player") return PlayerVFX;
            if (type == "monster") return MonsterVFX;
            _cachedPrefabs.TryGetValue(type, out var p);
            return p;
        }

        #endregion

        #region 재생 — 위치 고정

        /// <summary>
        /// 고정 위치 재생. 커스텀이면 Destroy + RPC 브로드캐스트, 발헤임이면 순수 Instantiate.
        /// </summary>
        public static GameObject Play(string vfxName, Vector3 position, float duration = 3f)
        {
            if (!VFX_ENABLED || string.IsNullOrEmpty(vfxName)) return null;

            bool isCustom = IsCustomVFX(vfxName);

            // 커스텀 VFX: 다른 클라이언트에 브로드캐스트
            if (!_isReceivingRPC && isCustom && ZRoutedRpc.instance != null)
            {
                try
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(
                        ZRoutedRpc.Everybody, RPC_PLAY_CUSTOM, vfxName, position, duration);
                }
                catch { }
            }

            try
            {
                if (_cachedPrefabs.TryGetValue(vfxName, out var cached))
                {
                    if (cached != null) return InstantiateVFX(cached, position, duration, vfxName);
                    _cachedPrefabs.Remove(vfxName);
                }

                if (isCustom)
                {
                    var prefab = FindPrefabInResources(vfxName);
                    _cachedPrefabs[vfxName] = prefab;
                    return prefab != null ? InstantiateVFX(prefab, position, duration, vfxName) : null;
                }

                // 발헤임 기본 VFX: Destroy 호출 안 함
                if (ZNetScene.instance != null)
                {
                    var zvPrefab = ZNetScene.instance.GetPrefab(vfxName);
                    if (zvPrefab != null)
                        return UnityEngine.Object.Instantiate(zvPrefab, position, Quaternion.identity);
                }
                var resPrefab = FindPrefabInResources(vfxName);
                if (resPrefab != null)
                {
                    _cachedPrefabs[vfxName] = resPrefab;
                    return UnityEngine.Object.Instantiate(resPrefab, position, Quaternion.identity);
                }
                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] Play({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>고정 위치 VFX (scale 무시 — PlayVFXAtPosition 대응)</summary>
        public static GameObject PlayAtPosition(string vfxName, Vector3 position, float duration = 5f, float scale = 1f)
            => Play(vfxName, position, duration);

        /// <summary>VFX + 사운드 동시 재생</summary>
        public static void PlayWithSound(string vfxName, string sfxName, Vector3 position, float duration = 3f)
        {
            if (!VFX_ENABLED) return;
            if (!string.IsNullOrEmpty(vfxName)) Play(vfxName, position, duration);
            if (!string.IsNullOrEmpty(sfxName)) Play(sfxName, position, duration);
        }

        /// <summary>회전 포함 VFX + 사운드</summary>
        public static GameObject PlayWithRotation(string vfxName, string sfxName, Vector3 position,
            Quaternion rotation, float duration = 3f)
        {
            if (!VFX_ENABLED) return null;
            GameObject result = null;
            if (!string.IsNullOrEmpty(vfxName))
            {
                try
                {
                    bool isCustom = IsCustomVFX(vfxName);
                    if (!_cachedPrefabs.TryGetValue(vfxName, out var prefab) || prefab == null)
                    {
                        if (!isCustom && ZNetScene.instance != null)
                            prefab = ZNetScene.instance.GetPrefab(vfxName);
                        if (prefab == null) prefab = FindPrefabInResources(vfxName);
                        if (prefab != null) _cachedPrefabs[vfxName] = prefab;
                    }
                    if (prefab != null)
                    {
                        result = UnityEngine.Object.Instantiate(prefab, position, rotation);
                        if (result != null && isCustom)
                            UnityEngine.Object.Destroy(result, duration);
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log?.LogWarning($"[TotalVFX] PlayWithRotation({vfxName}) 실패: {ex.Message}");
                }
            }
            if (!string.IsNullOrEmpty(sfxName)) Play(sfxName, position, duration);
            return result;
        }

        /// <summary>VFX + 사운드 네트워크 브로드캐스트 (rotation 지정 가능)</summary>
        public static void PlayMultiplayer(string vfxName, string sfxName, Vector3 position,
            Quaternion rotation = default, float duration = 5f)
        {
            if (!VFX_ENABLED) return;
            if (rotation != default && rotation != Quaternion.identity)
                PlayWithRotation(vfxName, sfxName, position, rotation, duration);
            else
                PlayWithSound(vfxName, sfxName, position, duration);
        }

        #endregion

        #region 재생 — 플레이어 / 타겟

        /// <summary>
        /// 플레이어에 VFX 부착.
        /// 커스텀: 자식 Instantiate + Destroy | 발헤임: TotalVFXFollowBehaviour 추적
        /// </summary>
        public static GameObject PlayOnPlayer(Player player, string vfxName,
            float duration = 5f, Vector3? localOffset = null)
        {
            if (!VFX_ENABLED || player == null || string.IsNullOrEmpty(vfxName)) return null;

            bool isCustom = IsCustomVFX(vfxName);

            // 커스텀 VFX: 브로드캐스트
            if (!_isReceivingRPC && isCustom && ZRoutedRpc.instance != null)
            {
                _recentLocalCreations[$"{vfxName}_{player.GetPlayerID()}"] = Time.time;
                try
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(
                        ZRoutedRpc.Everybody, RPC_PLAY_ON_PLAYER, vfxName, player.GetPlayerID(), duration);
                }
                catch { }
            }

            try
            {
                var prefab = isCustom
                    ? GetOrFindPrefab(vfxName)
                    : (ZNetScene.instance?.GetPrefab(vfxName) ?? GetOrFindPrefab(vfxName));
                if (prefab == null) return null;

                Vector3 offset = localOffset ?? new Vector3(0f, 1f, 0f);
                GameObject vfxObj;

                if (isCustom)
                {
                    vfxObj = UnityEngine.Object.Instantiate(prefab, player.transform);
                    if (vfxObj == null) return null;
                    vfxObj.SetActive(true);
                    vfxObj.transform.localPosition = offset;
                    if (duration > 0f) UnityEngine.Object.Destroy(vfxObj, duration);
                    else SetParticleAutoDestroy(vfxObj);
                }
                else
                {
                    // 발헤임 VFX: 자식 부착 금지 → 위치 추적 컴포넌트
                    vfxObj = UnityEngine.Object.Instantiate(prefab,
                        player.transform.position + offset, Quaternion.identity);
                    if (vfxObj == null) return null;
                    var follow = vfxObj.AddComponent<TotalVFXFollowBehaviour>();
                    follow.Target = player.transform;
                    follow.Offset = offset;
                    if (duration > 0f) UnityEngine.Object.Destroy(vfxObj, duration);
                    else SetParticleAutoDestroy(vfxObj);
                }
                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] PlayOnPlayer({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>플레이어에 VFX + 사운드 부착 (PlayVFXAttachedToPlayer 대응)</summary>
        public static GameObject PlayAttachedToPlayer(Player player, string vfxName,
            string sfxName, float duration, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;
            var vfxObj = PlayOnPlayer(player, vfxName, duration,
                offset == default ? (Vector3?)null : offset);
            if (!string.IsNullOrEmpty(sfxName))
                Play(sfxName, player.transform.position, duration);
            return vfxObj;
        }

        /// <summary>
        /// 발헤임 VFX를 플레이어 자식으로 부착 (ZNetView 제거 — PlayVFXFollowPlayer 대응)
        /// </summary>
        public static GameObject PlayFollowPlayer(Player player, string vfxName,
            string sfxName, float duration, Vector3 offset = default)
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
                    foreach (var nv in vfxObj.GetComponentsInChildren<ZNetView>())
                    {
                        nv.enabled = false;
                        UnityEngine.Object.Destroy(nv);
                    }
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }
                if (!string.IsNullOrEmpty(sfxName))
                    Play(sfxName, player.transform.position, duration);
                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] PlayFollowPlayer({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>타겟 Transform을 따라다니는 커스텀 VFX</summary>
        public static GameObject PlayFollowing(string vfxName, Transform followTarget,
            Vector3 localOffset, float duration = 5f)
        {
            if (!VFX_ENABLED || string.IsNullOrEmpty(vfxName) || followTarget == null) return null;
            try
            {
                var prefab = GetOrFindPrefab(vfxName);
                if (prefab == null) return null;

                var vfxObj = UnityEngine.Object.Instantiate(prefab, followTarget);
                if (vfxObj != null)
                {
                    if (!vfxObj.activeSelf) vfxObj.SetActive(true);
                    vfxObj.transform.localPosition = localOffset;
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }
                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] PlayFollowing({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>타겟 캐릭터에 VFX 부착</summary>
        public static GameObject PlayOnTarget(Character target, string vfxName,
            float duration = 3f, Vector3? localOffset = null)
        {
            if (!VFX_ENABLED || target == null || string.IsNullOrEmpty(vfxName)) return null;
            try
            {
                bool isCustom = IsCustomVFX(vfxName);
                var prefab = isCustom
                    ? GetOrFindPrefab(vfxName)
                    : (ZNetScene.instance?.GetPrefab(vfxName) ?? GetOrFindPrefab(vfxName));
                if (prefab == null) return null;

                var vfxObj = UnityEngine.Object.Instantiate(prefab, target.transform);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = localOffset ?? new Vector3(0f, 1.5f, 0f);
                    if (isCustom) UnityEngine.Object.Destroy(vfxObj, duration);
                }
                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[TotalVFX] PlayOnTarget({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region 유틸

        public static void ApplyVFXDim(GameObject vfx, float factor)
        {
            if (vfx == null) return;
            var dimmer = vfx.AddComponent<TotalVFXDimmerBehaviour>();
            dimmer.factor = factor;
        }

        private static GameObject InstantiateVFX(GameObject prefab, Vector3 position,
            float duration, string vfxName = "")
        {
            if (prefab == null) return null;
            var vfxObj = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
            if (vfxObj != null)
            {
                if (!vfxObj.activeSelf) vfxObj.SetActive(true);
                if (!string.IsNullOrEmpty(vfxName) && IsCustomVFX(vfxName))
                    UnityEngine.Object.Destroy(vfxObj, duration);
            }
            return vfxObj;
        }

        private static void SetParticleAutoDestroy(GameObject vfxObj)
        {
            foreach (var ps in vfxObj.GetComponentsInChildren<ParticleSystem>(true))
            {
                var main = ps.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
            }
        }

        #endregion

        #region Harmony 패치 — ZNetScene 초기화

        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        private static class TotalVFX_ZNetScene_Awake_Patch
        {
            static void Postfix()
            {
                try
                {
                    TotalVFX.Initialize();

                    // 발헤임 기본 VFX를 커스텀으로 등록 (ZNetView 제거 → Destroy 안전)
                    TotalVFX.RegisterAsCustom("vfx_Potion_health_medium");
                    TotalVFX.RegisterAsCustom("vfx_GoblinShield");
                    TotalVFX.RegisterAsCustom("fx_shield_start");
                    TotalVFX.RegisterAsCustom("fx_Lightning");
                    TotalVFX.RegisterAsCustom("fx_batteringram_fire");

                    // RPC 등록 (커스텀 VFX 멀티플레이어 브로드캐스트)
                    if (ZRoutedRpc.instance != null)
                    {
                        try
                        {
                            ZRoutedRpc.instance.Register(RPC_PLAY_CUSTOM,
                                new Action<long, string, Vector3, float>(TotalVFX.OnReceiveCustomVFX));
                        }
                        catch { }
                        try
                        {
                            ZRoutedRpc.instance.Register(RPC_PLAY_ON_PLAYER,
                                new Action<long, string, long, float>(TotalVFX.OnReceivePlayOnPlayer));
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log?.LogError($"[TotalVFX] ZNetScene 패치 실패: {ex.Message}");
                }
            }
        }

        #endregion
    }
}

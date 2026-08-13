using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.SkillTree;
namespace CaptainSkillTree
{
    /// <summary>
    /// SimpleVFX - 커스텀 VFX 하나씩 테스트
    /// </summary>
    public static class SimpleVFX
    {
        #region Static Fields

        /// <summary>
        /// 캐시된 프리팹
        /// </summary>
        private static Dictionary<string, GameObject> _cachedPrefabs = new Dictionary<string, GameObject>();

        /// <summary>
        /// 로드된 AssetBundle (static 유지, 언로드 안 함)
        /// </summary>
        private static AssetBundle _debuffBundle = null;

        /// <summary>
        /// 초기화 완료 여부
        /// </summary>
        private static bool _initialized = false;

        /// <summary>
        /// RPC 수신 중 재방송 방지 플래그 (무한 루프 차단)
        /// </summary>
        private static bool _isReceivingRPC = false;

        /// <summary>
        /// 로컬 직접 생성 기록: RPC 중복 생성 방지용 (vfxName+playerID → 생성 Time.time)
        /// </summary>
        private static readonly Dictionary<string, float> _recentLocalCreations
            = new Dictionary<string, float>();

        /// <summary>
        /// 커스텀 VFX 네트워크 RPC 이름
        /// </summary>
        internal const string RPC_PLAY_CUSTOM_VFX  = "CaptainVFX_PlayCustom";
        internal const string RPC_PLAY_ON_PLAYER   = "CaptainVFX_PlayOnPlayer";

        /// <summary>
        /// 커스텀 VFX 목록 (AssetBundle에서 로드, Destroy 필요)
        /// 발헤임 기본 VFX는 Destroy 호출 시 무한 로딩 발생!
        /// </summary>
        private static readonly HashSet<string> _customVFXNames = new HashSet<string>
        {
            // 영역 효과
            "area_circles_blue", "area_fire_red", "area_heal_green",
            "area_magic_multicolor", "area_star_ellow",

            // 버프/디버프 효과
            "buff_01", "buff_02a", "buff_03a", "buff_03a_aura",
            "debuff", "debuff_03", "debuff_03_aura",
            "statusailment_01", "statusailment_01_aura",

            // 색종이 효과
            "confetti_blast_multicolor", "confetti_directional_multicolor",

            // 먼지/연기 효과
            "dust_permanently_blue",

            // 플래시 효과 (flash_star_ellow_purple 포함!)
            "flash_blue_purple", "flash_ellow", "flash_ellow_pink",
            "flash_magic_blue_pink", "flash_magic_ellow_blue", "flash_round_ellow",
            "flash_star_ellow_green", "flash_star_ellow_purple",

            // 방어/치료 효과
            "guard_01", "healing",

            // 타격 효과
            "hit_01", "hit_02", "hit_03", "hit_04",

            // 플렉서스 효과
            "plexus",

            // 샤인 효과
            "shine_blue", "shine_ellow", "shine_pink",

            // 스파클/스파크 효과
            "sparkle_ellow",

            // 특수 효과
            "taunt",

            // 워터 블라스트 효과
            "water_blast_blue", "water_blast_green"
        };

        /// <summary>
        /// 플레이어용 VFX (debuff 번들)
        /// </summary>
        public static GameObject PlayerVFX = null;

        /// <summary>
        /// 몬스터용 VFX (Valheim 내장)
        /// </summary>
        public static GameObject MonsterVFX = null;

        #endregion

        #region 초기화

        /// <summary>
        /// ZNetScene.Awake에서 호출
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            try
            {

                // 1. PrefabRegistry에서 등록된 VFX 프리팹 가져오기
                LoadFromPrefabRegistry();

                // 2. "debuff" 번들만 로드 (fallback)
                LoadDebuffBundle();

                // 3. Valheim 내장 VFX 캐시 (몬스터용)
                CacheValheimPrefabs();

                _initialized = true;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// PrefabRegistry에서 등록된 VFX 프리팹들을 캐시에 추가
        /// </summary>
        private static void LoadFromPrefabRegistry()
        {
            try
            {
                var allPrefabs = CaptainSkillTree.Prefab.PrefabRegistry.GetAllRegisteredPrefabs();
                int loadedCount = 0;

                foreach (var kvp in allPrefabs)
                {
                    string prefabName = kvp.Key;
                    GameObject prefab = kvp.Value;

                    if (prefab == null) continue;

                    // 커스텀 VFX 이름인 경우만 캐시에 추가
                    if (_customVFXNames.Contains(prefabName) && !_cachedPrefabs.ContainsKey(prefabName))
                    {
                        _cachedPrefabs[prefabName] = prefab;
                        loadedCount++;
                    }

                    // 대소문자 무시 검색 (예: Buff_01 -> buff_01)
                    string lowerName = prefabName.ToLowerInvariant();
                    if (_customVFXNames.Contains(lowerName) && !_cachedPrefabs.ContainsKey(lowerName))
                    {
                        _cachedPrefabs[lowerName] = prefab;
                        loadedCount++;
                    }
                }

            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[SimpleVFX] PrefabRegistry 로드 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// "debuff" 번들만 로드 (테스트용)
        /// </summary>
        private static void LoadDebuffBundle()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = null;

                foreach (var name in assembly.GetManifestResourceNames())
                {
                    if (name.EndsWith(".debuff") || name.Contains(".VFX.debuff"))
                    {
                        resourceName = name;
                        break;
                    }
                }

                if (resourceName == null)
                {
                    Plugin.Log?.LogWarning("[SimpleVFX] 'debuff' 리소스 없음");
                    return;
                }

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
                        PlayerVFX = assets[0];  // 플레이어용 VFX로 할당
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] debuff 번들 로드 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// Valheim 내장 VFX 캐싱 (자주 사용하는 VFX 프리팹들)
        /// </summary>
        public static void CacheValheimPrefabs()
        {
            try
            {
                // 자주 사용하는 VFX 이름 목록
                string[] vfxNames = new string[]
                {
                    // 몬스터/타격 효과
                    "fx_seeker_hurt", "fx_backstab", "fx_crit",
                    // 힐링/버프 효과
                    "vfx_HealthUpgrade", "shaman_heal_aoe", "fx_greydwarf_shaman_heal",
                    "buff_03a", "buff_03a_aura", "buff_02a", "statusailment_01_aura",
                    // 폭발/마법 효과
                    "vfx_GodExplosion", "fx_siegebomb_explosion", "fx_Fader_Roar",
                    "fx_Lightning", "fx_fader_meteor_hit", "fx_bow_hit",
                    // 스폰/일반 효과
                    "vfx_spawn_small", "vfx_spawn_large", "flash_blue_purple",
                    "fx_guardstone_activate", "fx_guardstone_permitted_add",
                    "fx_eikthyr_stomp", "fx_Fader_Spin", "debuff_03",
                    "fx_Fader_Roar_Projectile_Hit", "fx_Fader_Fissure_Prespawn",
                    "sfx_fader_claw_pre", "sfx_fader_claw_swipe",
                    // 사운드
                    "sfx_morgen_alert", "sfx_dverger_heal_finish", "sfx_oozebomb_explode"
                };

                GameObject[] all = Resources.FindObjectsOfTypeAll<GameObject>();

                foreach (GameObject obj in all)
                {
                    if (obj == null) continue;

                    // 프리팹만 필터링 (씬 인스턴스 제외)
                    if (obj.scene.name != null && obj.scene.rootCount > 0) continue;

                    // 몬스터용 VFX - fx_seeker_hurt
                    if (obj.name.Contains("fx_seeker_hurt") && MonsterVFX == null)
                    {
                        MonsterVFX = obj;
                        _cachedPrefabs["monster"] = obj;
                    }

                    // 자주 사용하는 VFX 캐싱
                    foreach (string vfxName in vfxNames)
                    {
                        if (obj.name == vfxName && !_cachedPrefabs.ContainsKey(vfxName))
                        {
                            _cachedPrefabs[vfxName] = obj;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] Valheim 프리팹 캐싱 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// Valheim 기본 VFX를 커스텀 VFX로 등록
        /// ZNetScene에서 클론 → ZNetView 전체 제거 → _customVFXNames + _cachedPrefabs 등록
        /// 이후 SimpleVFX.PlayOnPlayer에서 커스텀 경로로 처리 → Destroy 안전
        /// ※ ZNetScene.Awake 이후(Postfix)에서 호출할 것
        /// </summary>
        public static void RegisterValheimVFXAsCustom(string vfxName)
        {
            if (_customVFXNames.Contains(vfxName)) return;
            if (ZNetScene.instance == null) return;

            var original = ZNetScene.instance.GetPrefab(vfxName);
            if (original == null)
            {
                Plugin.Log?.LogWarning($"[SimpleVFX] RegisterValheimVFXAsCustom: {vfxName} 프리팹 없음");
                return;
            }

            // ★ 핵심: Instantiate 전에 원본을 비활성화
            //   → clone의 ZNetView.Awake()가 실행되지 않아 ZNetScene에 등록 안 됨
            //   → 이후 DestroyImmediate해도 ZNetScene 내부 리스트에 null 참조 안 남음
            bool wasActive = original.activeSelf;
            original.SetActive(false);
            var clone = UnityEngine.Object.Instantiate(original);
            original.SetActive(wasActive); // 즉시 원본 복원

            clone.name = vfxName;
            UnityEngine.Object.DontDestroyOnLoad(clone);

            // 네트워크 컴포넌트 전체 제거 (자식 포함) — Awake 미실행 상태라 ZNetScene 오염 없음
            // ZNetView 제거 후 ZSyncTransform 등 의존 컴포넌트도 반드시 제거해야 SetActive(true) 시 NullRef 방지
            foreach (var nv in clone.GetComponentsInChildren<ZNetView>(true))
                UnityEngine.Object.DestroyImmediate(nv);
            foreach (var st in clone.GetComponentsInChildren<ZSyncTransform>(true))
                UnityEngine.Object.DestroyImmediate(st);
            foreach (var sa in clone.GetComponentsInChildren<ZSyncAnimation>(true))
                UnityEngine.Object.DestroyImmediate(sa);
            foreach (var proj in clone.GetComponentsInChildren<Projectile>(true))
                UnityEngine.Object.DestroyImmediate(proj);

            clone.SetActive(false); // 프리팹 템플릿으로 유지 (비활성)

            _customVFXNames.Add(vfxName);
            _cachedPrefabs[vfxName] = clone;
        }

        #endregion

        #region VFX 재생 (WackyEpicMMO PlayerFVX 방식)

        /// <summary>
        /// 플레이어를 따라다니는 VFX (버서커 발동 시)
        /// </summary>
        public static GameObject PlayOnPlayer(Player player, float duration = 5f)
        {
            if (player == null || PlayerVFX == null)
            {
                Plugin.Log?.LogWarning($"[SimpleVFX] PlayOnPlayer - player: {(player != null)}, PlayerVFX: {(PlayerVFX != null)}");
                return null;
            }

            try
            {
                // 플레이어 Transform에 직접 부착 (캐릭터 따라다님)
                var vfxObj = UnityEngine.Object.Instantiate(PlayerVFX, player.transform);

                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = new Vector3(0f, 1f, 0f);  // 캐릭터 중심 위쪽
                    // 원래 크기 유지 (스케일 조정 안 함)
                    Plugin.Log?.LogInfo($"[SimpleVFX] VFX 생성됨 (캐릭터 부착) - 스케일: {vfxObj.transform.localScale}");
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }

                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayOnPlayer 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 고정 위치에 VFX (몬스터 타격 시)
        /// </summary>
        public static GameObject PlayAtPosition(Vector3 position, float duration = 2f)
        {
            if (MonsterVFX == null) return null;

            try
            {
                // WackyEpicMMO CriticalVFX 방식
                var vfxObj = UnityEngine.Object.Instantiate(MonsterVFX, position, Quaternion.identity);
                if (vfxObj != null)
                {
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }

                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayAtPosition 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 프리팹 직접 가져오기
        /// </summary>
        public static GameObject GetPrefab(string type)
        {
            if (type == "player") return PlayerVFX;
            if (type == "monster") return MonsterVFX;
            _cachedPrefabs.TryGetValue(type, out var prefab);
            return prefab;
        }

        /// <summary>
        /// 초기화 상태
        /// </summary>
        public static bool IsInitialized => _initialized && (PlayerVFX != null || MonsterVFX != null);

        #endregion

        #region 범용 VFX 재생 메서드

        /// <summary>
        /// Valheim 내장 VFX를 이름으로 재생 (고정 위치)
        /// - 커스텀 VFX: Destroy 호출 O
        /// - 발헤임 기본 VFX: Destroy 호출 X (무한 로딩 방지)
        /// </summary>
        public static GameObject Play(string vfxName, Vector3 position, float duration = 3f)
        {
            if (string.IsNullOrEmpty(vfxName)) return null;

            // 커스텀 VFX이고 RPC 수신 중이 아닌 경우 → 다른 클라이언트에 브로드캐스트
            if (!_isReceivingRPC && IsCustomVFX(vfxName) && ZRoutedRpc.instance != null)
            {
                try
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(
                        ZRoutedRpc.Everybody, RPC_PLAY_CUSTOM_VFX, vfxName, position, duration);
                }
                catch { }
            }

            try
            {
                GameObject prefab = null;
                bool isCustom = IsCustomVFX(vfxName);

                // 1. 캐시에서 찾기
                if (_cachedPrefabs.TryGetValue(vfxName, out prefab))
                {
                    if (prefab != null)
                        return InstantiateVFX(prefab, position, duration, vfxName);
                    // null 캐시: 번들이 이후 로드되었을 수 있으므로 제거 후 재탐색
                    _cachedPrefabs.Remove(vfxName);
                }

                // 2. 커스텀 VFX는 Resources에서 찾기
                if (isCustom)
                {
                    prefab = FindPrefabInResources(vfxName);
                    _cachedPrefabs[vfxName] = prefab; // null이어도 저장 (반복 탐색 방지)
                    if (prefab != null)
                    {
                        return InstantiateVFX(prefab, position, duration, vfxName);
                    }
                }
                else
                {
                    // 3. 발헤임 기본 VFX는 ZNetScene에서 찾기 (Destroy 안 함)
                    if (ZNetScene.instance != null)
                    {
                        prefab = ZNetScene.instance.GetPrefab(vfxName);
                        if (prefab != null)
                        {
                            var vfxObj = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
                            // ⚠️ 발헤임 기본 VFX는 Destroy 호출 안 함 (발헤임이 자동 정리)
                            return vfxObj;
                        }
                    }

                    // 4. ZNetScene 실패 시 Resources에서 시도 (fallback)
                    prefab = FindPrefabInResources(vfxName);
                    if (prefab != null)
                    {
                        _cachedPrefabs[vfxName] = prefab;
                        // 발헤임 기본 VFX이므로 Destroy 안 함
                        return UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] Play({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 대상 Transform을 따라다니는 커스텀 VFX 재생 (PlayOnPlayer 방식)
        /// parent로 Instantiate → 몬스터 이동 시 VFX도 자동으로 따라다님
        /// </summary>
        public static GameObject PlayFollowing(string vfxName, Transform followTarget, Vector3 localOffset, float duration = 5f)
        {
            if (string.IsNullOrEmpty(vfxName) || followTarget == null) return null;

            try
            {
                GameObject prefab = null;

                if (_cachedPrefabs.TryGetValue(vfxName, out prefab))
                {
                    if (prefab == null) return null;
                }
                else
                {
                    prefab = FindPrefabInResources(vfxName);
                    _cachedPrefabs[vfxName] = prefab;
                    if (prefab == null) return null;
                }

                // followTarget에 부착 → 자동으로 따라다님
                var vfxObj = UnityEngine.Object.Instantiate(prefab, followTarget);
                if (vfxObj != null)
                {
                    if (!vfxObj.activeSelf)
                        vfxObj.SetActive(true);
                    vfxObj.transform.localPosition = localOffset;
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }

                return vfxObj;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayFollowing({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// VFX 발광·투명도 감소.
        /// VFXDimmerBehaviour 컴포넌트를 부착 → 다음 LateUpdate에서 적용
        /// (파티클 초기화 완료 후 프레임에 처리하여 확실한 적용 보장)
        /// </summary>
        public static void ApplyVFXDim(GameObject vfx, float factor)
        {
            if (vfx == null) return;
            var dimmer = vfx.AddComponent<VFXDimmerBehaviour>();
            dimmer.factor = factor;
        }

        /// <summary>
        /// Resources에서 프리팹 찾기 (ZNetScene 사용 금지)
        /// WackyEpicMMOSystem 방식: 프리팹만 필터링 (씬 인스턴스 제외)
        /// </summary>
        private static GameObject FindPrefabInResources(string prefabName)
        {
            try
            {
                foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (obj != null && obj.name == prefabName)
                    {
                        // 프리팹만 필터링 (씬 인스턴스 제외)
                        // scene.name이 null이거나 rootCount가 0이면 프리팹
                        if (obj.scene.name == null || obj.scene.rootCount == 0)
                        {
                            return obj;
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// 커스텀 VFX인지 확인 (Destroy 필요 여부 결정)
        /// 발헤임 기본 VFX에 Destroy 호출하면 무한 로딩 발생!
        /// </summary>
        private static bool IsCustomVFX(string vfxName)
        {
            return !string.IsNullOrEmpty(vfxName) && _customVFXNames.Contains(vfxName);
        }

        /// <summary>
        /// VFX Instantiate (타입별 분리 처리)
        /// - 커스텀 VFX: Instantiate + Destroy
        /// - 발헤임 기본 VFX: 순수 Instantiate (Destroy 안 함 - 발헤임이 자동 정리)
        /// </summary>
        private static GameObject InstantiateVFX(GameObject prefab, Vector3 position, float duration, string vfxName = "")
        {
            if (prefab == null) return null;

            var vfxObj = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
            if (vfxObj != null)
            {
                // RegisterValheimVFXAsCustom 클론은 SetActive(false)로 저장 → 반드시 활성화
                if (!vfxObj.activeSelf)
                    vfxObj.SetActive(true);

                // 커스텀 VFX만 Destroy 호출
                if (!string.IsNullOrEmpty(vfxName) && IsCustomVFX(vfxName))
                    UnityEngine.Object.Destroy(vfxObj, duration);
                // 발헤임 기본 VFX는 Destroy 호출 안 함 (발헤임이 자동 정리)
            }
            return vfxObj;
        }

        /// <summary>
        /// Valheim 내장 VFX를 플레이어에 부착 (캐릭터 따라다님)
        /// - 커스텀 VFX: Destroy 호출 O
        /// - 발헤임 기본 VFX: Destroy 호출 X (무한 로딩 방지)
        /// </summary>
        public static GameObject PlayOnPlayer(Player player, string vfxName, float duration = 5f, Vector3? localOffset = null)
        {
            if (player == null || string.IsNullOrEmpty(vfxName)) return null;

            // 커스텀 VFX + RPC 수신 중 아닌 경우 → 플레이어 ID와 함께 브로드캐스트
            if (!_isReceivingRPC && IsCustomVFX(vfxName) && ZRoutedRpc.instance != null)
            {
                // RPC 전송 전에 중복 방지 키 등록 (자기 수신 방지)
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
                GameObject prefab = null;
                bool isCustom = IsCustomVFX(vfxName);

                // 프리팹 찾기
                if (isCustom)
                {
                    prefab = GetOrFindPrefab(vfxName);
                }
                else
                {
                    // 발헤임 기본 VFX는 ZNetScene에서 우선 찾기
                    if (ZNetScene.instance != null)
                    {
                        prefab = ZNetScene.instance.GetPrefab(vfxName);
                    }
                    // fallback
                    if (prefab == null)
                    {
                        prefab = GetOrFindPrefab(vfxName);
                    }
                }

                if (prefab == null) return null;

                GameObject vfxObj;
                Vector3 offsetVal = localOffset ?? new Vector3(0f, 1f, 0f);

                if (isCustom)
                {
                    // 커스텀 VFX: 플레이어 자식으로 부착 (ZNetView 없으므로 안전)
                    vfxObj = UnityEngine.Object.Instantiate(prefab, player.transform);
                    if (vfxObj == null) return null;
                    vfxObj.SetActive(true); // 비활성 템플릿(RegisterValheimVFXAsCustom)에서 복제된 경우 활성화
                    vfxObj.transform.localPosition = offsetVal;
                    if (duration > 0f)
                        UnityEngine.Object.Destroy(vfxObj, duration);
                    else
                        SetParticleAutoDestroy(vfxObj);
                }
                else
                {
                    // 발헤임 기본 VFX: 자식 부착 금지 (ZNetView 등 네트워크 컴포넌트 충돌 → 무한 로딩)
                    // → 고정 위치에 생성 후 VFXFollowBehaviour로 플레이어 위치 추적
                    var worldPos = player.transform.position + offsetVal;
                    vfxObj = UnityEngine.Object.Instantiate(prefab, worldPos, Quaternion.identity);
                    if (vfxObj == null) return null;
                    var follow = vfxObj.AddComponent<VFXFollowBehaviour>();
                    follow.Target = player.transform;
                    follow.Offset = offsetVal;
                    if (duration > 0f)
                        UnityEngine.Object.Destroy(vfxObj, duration);
                    else
                        SetParticleAutoDestroy(vfxObj);
                }

                if (vfxObj != null)
                {

                    // 로컬 직접 생성 기록 → RPC 중복 생성 방지
                    if (!_isReceivingRPC && isCustom)
                    {
                        string dedupKey = $"{vfxName}_{player.GetPlayerID()}";
                        _recentLocalCreations[dedupKey] = Time.time;
                    }

                    return vfxObj;
                }

                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayOnPlayer({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// duration <= 0 일 때 파티클 시스템 stopAction = Destroy 설정 → 자동 소멸
        /// </summary>
        private static void SetParticleAutoDestroy(GameObject vfxObj)
        {
            foreach (var ps in vfxObj.GetComponentsInChildren<ParticleSystem>(true))
            {
                var main = ps.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
            }
        }

        /// <summary>
        /// 캐시 또는 Resources에서 프리팹 찾기
        /// </summary>
        private static GameObject GetOrFindPrefab(string vfxName)
        {
            if (_cachedPrefabs.TryGetValue(vfxName, out var prefab))
            {
                if (prefab != null) return prefab;
                // null 캐시 제거 후 재탐색 (번들 로드 타이밍 문제 대비)
                _cachedPrefabs.Remove(vfxName);
            }

            prefab = FindPrefabInResources(vfxName);
            if (prefab != null)
                _cachedPrefabs[vfxName] = prefab; // 성공한 경우만 저장

            return prefab;
        }

        /// <summary>
        /// Valheim 내장 VFX를 타겟에 부착 (타겟 따라다님)
        /// - 커스텀 VFX: Destroy 호출 O
        /// - 발헤임 기본 VFX: Destroy 호출 X (무한 로딩 방지)
        /// </summary>
        public static GameObject PlayOnTarget(Character target, string vfxName, float duration = 3f, Vector3? localOffset = null)
        {
            if (target == null || string.IsNullOrEmpty(vfxName)) return null;

            try
            {
                GameObject prefab = null;
                bool isCustom = IsCustomVFX(vfxName);

                // 프리팹 찾기
                if (isCustom)
                {
                    prefab = GetOrFindPrefab(vfxName);
                }
                else
                {
                    // 발헤임 기본 VFX는 ZNetScene에서 우선 찾기
                    if (ZNetScene.instance != null)
                    {
                        prefab = ZNetScene.instance.GetPrefab(vfxName);
                    }
                    // fallback
                    if (prefab == null)
                    {
                        prefab = GetOrFindPrefab(vfxName);
                    }
                }

                if (prefab == null) return null;

                // 타겟 Transform에 부착
                var vfxObj = UnityEngine.Object.Instantiate(prefab, target.transform);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = localOffset ?? new Vector3(0f, 1.5f, 0f);

                    // 커스텀 VFX만 Destroy 호출
                    if (isCustom)
                    {
                        UnityEngine.Object.Destroy(vfxObj, duration);
                    }
                    // 발헤임 기본 VFX는 Destroy 호출 안 함 (발헤임이 자동 정리)

                    return vfxObj;
                }

                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] PlayOnTarget({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// VFX + 사운드 동시 재생 (고정 위치)
        /// </summary>
        public static void PlayWithSound(string vfxName, string sfxName, Vector3 position, float duration = 3f)
        {
            // VFX 재생
            if (!string.IsNullOrEmpty(vfxName))
                Play(vfxName, position, duration);

            // 사운드 재생
            if (!string.IsNullOrEmpty(sfxName))
                Play(sfxName, position, duration);
        }

        /// <summary>
        /// VFX + 사운드 동시 재생 (회전 지정, 고정 위치)
        /// Valheim 기본 VFX에 방향성이 필요할 때 사용
        /// </summary>
        public static GameObject PlayWithRotation(string vfxName, string sfxName, Vector3 position, Quaternion rotation, float duration = 3f)
        {
            GameObject result = null;

            if (!string.IsNullOrEmpty(vfxName))
            {
                try
                {
                    GameObject prefab = null;
                    bool isCustom = IsCustomVFX(vfxName);

                    // 캐시에서 찾기
                    if (!_cachedPrefabs.TryGetValue(vfxName, out prefab) || prefab == null)
                    {
                        if (!isCustom && ZNetScene.instance != null)
                            prefab = ZNetScene.instance.GetPrefab(vfxName);
                        if (prefab == null)
                            prefab = FindPrefabInResources(vfxName);
                        if (prefab != null)
                            _cachedPrefabs[vfxName] = prefab;
                    }

                    if (prefab != null)
                    {
                        // 지정된 rotation으로 생성 (Quaternion.identity 아님)
                        result = UnityEngine.Object.Instantiate(prefab, position, rotation);
                        if (result != null)
                        {
                            // 발헤임 기본 VFX는 Destroy 호출 안 함
                            if (isCustom)
                                UnityEngine.Object.Destroy(result, duration);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log?.LogWarning($"[SimpleVFX] PlayWithRotation({vfxName}) 실패: {ex.Message}");
                }
            }

            // 사운드 재생
            if (!string.IsNullOrEmpty(sfxName))
                Play(sfxName, position, duration);

            return result;
        }

        #endregion

        #region 네트워크 RPC 핸들러

        /// <summary>
        /// [RPC 수신] 다른 클라이언트가 Play()를 호출 → 로컬에서 재생
        /// _isReceivingRPC = true로 재방송 차단
        /// </summary>
        internal static void OnReceiveCustomVFX(long sender, string vfxName, Vector3 pos, float duration)
        {
            _isReceivingRPC = true;
            try   { Play(vfxName, pos, duration); }
            finally { _isReceivingRPC = false; }
        }

        /// <summary>
        /// [RPC 수신] 다른 클라이언트가 PlayOnPlayer()를 호출 → 플레이어 ID로 찾아 로컬 재생
        /// </summary>
        internal static void OnReceivePlayOnPlayer(long sender, string vfxName, long playerID, float duration)
        {
            // 로컬에서 직접 생성한 지 1초 이내 → RPC 중복 생성 차단
            string dedupKey = $"{vfxName}_{playerID}";
            if (_recentLocalCreations.TryGetValue(dedupKey, out float t) && Time.time - t < 1f)
            {
                _recentLocalCreations.Remove(dedupKey);
                return; // 이미 로컬에서 생성 완료, 스킵
            }

            _isReceivingRPC = true;
            try
            {
                foreach (var p in Player.GetAllPlayers())
                {
                    if (p == null) continue;
                    if (p.GetPlayerID() == playerID)
                    {
                        PlayOnPlayer(p, vfxName, duration);
                        break;
                    }
                }
            }
            finally { _isReceivingRPC = false; }
        }

        #endregion
    }

    #region Harmony Patch

    /// <summary>
    /// ZNetScene.Awake Postfix - Valheim 내장 VFX 캐싱만 (WackyEpicMMO 방식)
    /// </summary>
    [HarmonyPatch(typeof(ZNetScene), "Awake")]
    public static class SimpleVFX_ZNetScene_Awake_Patch
    {
        static void Postfix(ZNetScene __instance)
        {
            try
            {
                // SimpleVFX 전체 초기화 (debuff 번들 로드 + Valheim VFX 캐싱)
                SimpleVFX.Initialize();
                // Valheim 기본 VFX 중 커스텀화할 VFX 등록 (ZNetView 제거 → Destroy 안전)
                SimpleVFX.RegisterValheimVFXAsCustom("vfx_Potion_health_medium");
                SimpleVFX.RegisterValheimVFXAsCustom("vfx_GoblinShield");
                SimpleVFX.RegisterValheimVFXAsCustom("fx_shield_start");
                SimpleVFX.RegisterValheimVFXAsCustom("fx_Lightning");
                SimpleVFX.RegisterValheimVFXAsCustom("fx_chainlightning_hit");
                SimpleVFX.RegisterValheimVFXAsCustom("fx_batteringram_fire");
                // ZNetScene에 커스텀 VFX 프리팹 등록 (spawn 명령어 사용 가능)
                CaptainSkillTree.Prefab.PrefabRegistry.RegisterToZNetScene();

                // 커스텀 VFX 멀티플레이어 브로드캐스트 RPC 등록
                if (ZRoutedRpc.instance != null)
                {
                    try
                    {
                        ZRoutedRpc.instance.Register(SimpleVFX.RPC_PLAY_CUSTOM_VFX,
                            new Action<long, string, Vector3, float>(SimpleVFX.OnReceiveCustomVFX));
                    }
                    catch { /* 이미 등록된 경우 무시 */ }

                    try
                    {
                        ZRoutedRpc.instance.Register(SimpleVFX.RPC_PLAY_ON_PLAYER,
                            new Action<long, string, long, float>(SimpleVFX.OnReceivePlayOnPlayer));
                    }
                    catch { /* 이미 등록된 경우 무시 */ }

                    try
                    {
                        ZRoutedRpc.instance.Register(SkillTree.SkillEffect.RPC_FANCAST_SUMMON,
                            new Action<long, long>(SkillTree.SkillEffect.OnReceiveFanCastSummon));
                    }
                    catch { }

                    try
                    {
                        ZRoutedRpc.instance.Register(SkillTree.SkillEffect.RPC_FANCAST_CANCEL,
                            new Action<long, long>(SkillTree.SkillEffect.OnReceiveFanCastCancel));
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] ZNetScene 패치 실패: {ex.Message}");
            }
        }
    }

    #endregion

    /// <summary>
    /// 발헤임 기본 VFX가 대상 Transform을 따라다니도록 하는 컴포넌트.
    /// 플레이어 자식으로 부착하지 않고 매 프레임 위치를 갱신하여 무한 로딩 방지.
    /// </summary>
    public class VFXFollowBehaviour : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;

        void Update()
        {
            if (Target == null) { UnityEngine.Object.Destroy(gameObject); return; }
            transform.position = Target.position + Offset;
        }
    }

    /// <summary>
    /// VFX 밝기·투명도 감소 컴포넌트.
    /// 파티클 초기화 완료 이후 LateUpdate에서 한 번 적용 후 자동 제거.
    /// - Material: _TintColor / _Color / _BaseColor (RGB + alpha 모두 × factor)
    /// - Additive shader: alpha 무시 → RGB 감소가 핵심
    /// - ParticleSystem: Stop/Clear → startColor + emission 감소 → Play
    /// - Light: intensity 감소
    /// </summary>
    public class VFXDimmerBehaviour : MonoBehaviour
    {
    public float factor = 0.5f;

    private void LateUpdate()
    {
        try { Apply(); }
        catch (Exception ex)
        {
            Plugin.Log?.LogWarning($"[VFXDimmer] 적용 실패: {ex.Message}");
        }
        Destroy(this); // 1회 적용 후 컴포넌트 제거
    }

    private void Apply()
    {
        // 1. Material (multi-material 포함, 전체 슬롯)
        var colorProps = new[] { "_TintColor", "_Color", "_BaseColor" };
        var matList = new List<Material>();
        foreach (var r in GetComponentsInChildren<Renderer>(true))
        {
            if (r == null) continue;
            r.GetMaterials(matList);
            foreach (var mat in matList)
            {
                if (mat == null) continue;
                foreach (var prop in colorProps)
                {
                    if (mat.HasProperty(prop))
                    {
                        Color c = mat.GetColor(prop);
                        c.r *= factor; c.g *= factor; c.b *= factor; c.a *= factor;
                        mat.SetColor(prop, c);
                        break;
                    }
                }
                if (mat.HasProperty("_EmissionColor"))
                    mat.SetColor("_EmissionColor", mat.GetColor("_EmissionColor") * factor);
            }
        }

        // 2. Light
        foreach (var lt in GetComponentsInChildren<Light>(true))
            if (lt != null) lt.intensity *= factor;

        // 3. ParticleSystem
        var allPS = GetComponentsInChildren<ParticleSystem>(true);
        if (allPS.Length == 0) return;

        // 3a. 개별 Stop
        foreach (var ps in allPS)
            if (ps != null) ps.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        foreach (var ps in allPS)
        {
            if (ps == null) continue;
            var main = ps.main;

            // startColor RGB·alpha 감소
            Color sc = main.startColor.color;
            sc.r *= factor; sc.g *= factor; sc.b *= factor; sc.a *= factor;
            main.startColor = new ParticleSystem.MinMaxGradient(sc);

            // 파티클 크기 축소 (확실한 시각 변화)
            main.startSizeMultiplier *= factor;

            // colorOverLifetime — gradient key colors 감소 (startColor를 덮어쓰는 모듈 대응)
            var col = ps.colorOverLifetime;
            if (col.enabled)
            {
                var grad = col.color;
                if (grad.mode == ParticleSystemGradientMode.Gradient ||
                    grad.mode == ParticleSystemGradientMode.TwoGradients)
                {
                    var g = grad.gradient;
                    var keys = g.colorKeys;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        var k = keys[i];
                        k.color = new Color(k.color.r * factor, k.color.g * factor,
                                            k.color.b * factor, k.color.a);
                        keys[i] = k;
                    }
                    g.colorKeys = keys;
                    col.color = new ParticleSystem.MinMaxGradient(g);
                }
            }

            // ※ Emission rate 수정 제거:
            // rateOverTime.mode가 Curve/TwoConstants일 때 .constant=0 을 읽어
            // 파티클 전체 소멸 버그 발생 → 색상·크기 감소만으로 밝기 조절 충분
        }

        // 3b. 개별 Play
        foreach (var ps in allPS)
            if (ps != null) ps.Play(false);

    }
    } // class VFXDimmerBehaviour
} // namespace CaptainSkillTree

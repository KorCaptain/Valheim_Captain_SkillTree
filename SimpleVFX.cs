using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

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
                Plugin.Log?.LogInfo("[SimpleVFX] 초기화 시작...");

                // 1. PrefabRegistry에서 등록된 VFX 프리팹 가져오기
                LoadFromPrefabRegistry();

                // 2. "debuff" 번들만 로드 (fallback)
                LoadDebuffBundle();

                // 3. Valheim 내장 VFX 캐시 (몬스터용)
                CacheValheimPrefabs();

                _initialized = true;
                Plugin.Log?.LogInfo($"[SimpleVFX] 초기화 완료 - 캐시된 프리팹: {_cachedPrefabs.Count}개");
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

                Plugin.Log?.LogInfo($"[SimpleVFX] PrefabRegistry에서 {loadedCount}개 VFX 로드됨");
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
                        Plugin.Log?.LogInfo($"[SimpleVFX] 'debuff' 프리팹 로드 완료: {assets[0].name}");
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

                Plugin.Log?.LogInfo($"[SimpleVFX] Valheim VFX 캐시 완료: {_cachedPrefabs.Count}개");
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] Valheim 프리팹 캐싱 실패: {ex.Message}");
            }
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

            try
            {
                GameObject prefab = null;
                bool isCustom = IsCustomVFX(vfxName);

                // 1. 캐시에서 찾기 (null 캐시 포함 - 반복 탐색 방지)
                if (_cachedPrefabs.TryGetValue(vfxName, out prefab))
                {
                    if (prefab == null) return null; // null 캐시 → 즉시 반환
                    return InstantiateVFX(prefab, position, duration, vfxName);
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
                // 커스텀 VFX만 Destroy 호출
                if (!string.IsNullOrEmpty(vfxName) && IsCustomVFX(vfxName))
                {
                    UnityEngine.Object.Destroy(vfxObj, duration);
                }
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

                // 플레이어 Transform에 부착
                var vfxObj = UnityEngine.Object.Instantiate(prefab, player.transform);
                if (vfxObj != null)
                {
                    vfxObj.transform.localPosition = localOffset ?? new Vector3(0f, 1f, 0f);

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
                Plugin.Log?.LogError($"[SimpleVFX] PlayOnPlayer({vfxName}) 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 캐시 또는 Resources에서 프리팹 찾기
        /// </summary>
        private static GameObject GetOrFindPrefab(string vfxName)
        {
            // null 캐시 포함하여 확인 (반복 탐색 방지)
            if (_cachedPrefabs.TryGetValue(vfxName, out var prefab))
                return prefab; // null이면 null 반환 (재탐색 안 함)

            prefab = FindPrefabInResources(vfxName);
            _cachedPrefabs[vfxName] = prefab; // null이어도 저장

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
                Plugin.Log?.LogInfo($"[SimpleVFX] 초기화 결과 - PlayerVFX: {(SimpleVFX.PlayerVFX != null ? "로드됨" : "null")}, MonsterVFX: {(SimpleVFX.MonsterVFX != null ? "로드됨" : "null")}");
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogError($"[SimpleVFX] ZNetScene 패치 실패: {ex.Message}");
            }
        }
    }

    #endregion
}

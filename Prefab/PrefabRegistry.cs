using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using Jotunn.Utils;

namespace CaptainSkillTree.Prefab
{
    /// <summary>
    /// ZNetScene에 누락된 프리팹들을 자동으로 등록하는 시스템
    /// asset/Resources 폴더의 모든 프리팹을 ZNetScene에 추가
    /// </summary>
    public class PrefabRegistry
    {
        #region Static Fields
        private static readonly Dictionary<string, GameObject> registeredPrefabs = new Dictionary<string, GameObject>();
        private static readonly List<string> failedRegistrations = new List<string>();
        private static readonly HashSet<string> loadedBundles = new HashSet<string>();
        private static bool isInitialized = false;
        
        /// <summary>
        /// VFX 폴더의 모든 프리팹들 - EmbeddedResource로 포함된 모든 VFX 효과
        /// </summary>
        private static readonly HashSet<string> vfxPrefabs = new HashSet<string>
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
            
            // 플래시 효과
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
            
            // 쇼크 효과 (제거됨 - 사용하지 않음)
            
            // 스파클/스파크 효과
            "sparkle_ellow",
            
            // 특수 효과
            "taunt", // star aura 제거됨
            
            // 워터 블라스트 효과
            "water_blast_blue", "water_blast_green"
        };
        
        /// <summary>
        /// VFX 폴더의 모든 프리팹에 대한 타입별 설정
        /// </summary>
        private static readonly Dictionary<string, VFXConfig> vfxConfigurations = new Dictionary<string, VFXConfig>
        {
            // 영역 효과
            ["area_circles_blue"] = new VFXConfig(VFXType.Magic, UnityEngine.Color.blue, 4f),
            ["area_fire_red"] = new VFXConfig(VFXType.Explosion, UnityEngine.Color.red, 3f),
            ["area_heal_green"] = new VFXConfig(VFXType.Heal, UnityEngine.Color.green, 5f),
            ["area_magic_multicolor"] = new VFXConfig(VFXType.Magic, UnityEngine.Color.magenta, 4f),
            ["area_star_ellow"] = new VFXConfig(VFXType.Special, UnityEngine.Color.yellow, 3f),
            
            // 버프/디버프 효과
            ["buff_01"] = new VFXConfig(VFXType.Buff, UnityEngine.Color.blue, 2.5f),
            ["buff_02a"] = new VFXConfig(VFXType.Buff, UnityEngine.Color.green, 3f),
            ["buff_03a"] = new VFXConfig(VFXType.Buff, UnityEngine.Color.blue, 3f),
            ["buff_03a_aura"] = new VFXConfig(VFXType.Buff, UnityEngine.Color.blue, 2.5f),
            ["debuff"] = new VFXConfig(VFXType.Debuff, UnityEngine.Color.red, 2f),
            ["debuff_03"] = new VFXConfig(VFXType.Debuff, UnityEngine.Color.red, 2.5f),
            ["debuff_03_aura"] = new VFXConfig(VFXType.Debuff, UnityEngine.Color.red, 2.5f),
            ["statusailment_01"] = new VFXConfig(VFXType.Debuff, new UnityEngine.Color(0.5f, 0f, 0.5f), 3f),
            ["statusailment_01_aura"] = new VFXConfig(VFXType.Debuff, new UnityEngine.Color(0.5f, 0f, 0.5f), 2.5f),
            
            // 색종이 효과
            ["confetti_blast_multicolor"] = new VFXConfig(VFXType.Special, UnityEngine.Color.white, 2f),
            ["confetti_directional_multicolor"] = new VFXConfig(VFXType.Special, UnityEngine.Color.white, 2.5f),
            
            // 먼지/연기 효과 (삭제된 파일들 제거)
            ["dust_permanently_blue"] = new VFXConfig(VFXType.Special, UnityEngine.Color.blue, 3f),
            
            // 플래시 효과
            ["flash_blue_purple"] = new VFXConfig(VFXType.Special, new UnityEngine.Color(0.5f, 0f, 1f), 1f),
            ["flash_ellow"] = new VFXConfig(VFXType.Special, UnityEngine.Color.yellow, 1f),
            ["flash_ellow_pink"] = new VFXConfig(VFXType.Special, new UnityEngine.Color(1f, 1f, 0.5f), 1f),
            ["flash_magic_blue_pink"] = new VFXConfig(VFXType.Magic, new UnityEngine.Color(0.5f, 0.5f, 1f), 1.5f),
            ["flash_magic_ellow_blue"] = new VFXConfig(VFXType.Magic, new UnityEngine.Color(0.5f, 1f, 1f), 1.5f),
            ["flash_round_ellow"] = new VFXConfig(VFXType.Special, UnityEngine.Color.yellow, 1.2f),
            ["flash_star_ellow_green"] = new VFXConfig(VFXType.Special, new UnityEngine.Color(0.8f, 1f, 0.5f), 1.5f),
            ["flash_star_ellow_purple"] = new VFXConfig(VFXType.Special, new UnityEngine.Color(1f, 0.8f, 1f), 1.5f),
            
            // 방어/치료 효과
            ["guard_01"] = new VFXConfig(VFXType.Special, new UnityEngine.Color(0f, 0.8f, 1f), 2f),
            ["healing"] = new VFXConfig(VFXType.Heal, UnityEngine.Color.green, 3f),
            
            // 타격 효과
            ["hit_01"] = new VFXConfig(VFXType.Hit, UnityEngine.Color.yellow, 1f),
            ["hit_02"] = new VFXConfig(VFXType.Hit, new UnityEngine.Color(1f, 0.8f, 0f), 1.2f),
            ["hit_03"] = new VFXConfig(VFXType.Hit, new UnityEngine.Color(1f, 0.5f, 0f), 1.5f),
            ["hit_04"] = new VFXConfig(VFXType.Hit, UnityEngine.Color.red, 2f),
            
            // ["lightning aura"] 제거 - 사용하지 않음
            // ["star aura"] 제거 - 사용하지 않음
            
            // 플렉서스 효과 제거 - 사용하지 않음
            // ["multishot"] 제거 - 사용하지 않음
            // ["plexus"] 제거 - 사용하지 않음
            // ["plexus aoe"] 제거 - 사용하지 않음
            
            // 폭발 효과 (삭제된 파일들 제거)
            
            // 샤인 효과
            ["shine_blue"] = new VFXConfig(VFXType.Special, UnityEngine.Color.blue, 2f),
            ["shine_ellow"] = new VFXConfig(VFXType.Special, UnityEngine.Color.yellow, 2f),
            ["shine_pink"] = new VFXConfig(VFXType.Special, new UnityEngine.Color(1f, 0.7f, 0.9f), 2f),
            
            // 스파클/스파크 효과 (삭제된 파일들 제거)
            ["sparkle_ellow"] = new VFXConfig(VFXType.Special, UnityEngine.Color.yellow, 1.5f),
            
            // 특수 효과
            ["taunt"] = new VFXConfig(VFXType.Special, UnityEngine.Color.red, 1.8f),
            
            // 워터 블라스트 효과
            ["water_blast_blue"] = new VFXConfig(VFXType.Magic, UnityEngine.Color.blue, 3f),
            ["water_blast_green"] = new VFXConfig(VFXType.Magic, UnityEngine.Color.green, 3f),
            
            // 쇼크 효과 (제거됨 - 사용하지 않음)
            // shock_vfx, shock_vfx_color, shock_vfx_distortion 제거됨
            
            // star aura 제거됨, plexus 효과만 유지
            ["plexus"] = new VFXConfig(VFXType.Magic, new UnityEngine.Color(0.5f, 0.8f, 1f), 3.5f)
        };
        
        /// <summary>
        /// VFX 설정 구조체
        /// </summary>
        private struct VFXConfig
        {
            public VFXType Type;
            public UnityEngine.Color Color;
            public float Duration;
            
            public VFXConfig(VFXType type, UnityEngine.Color color, float duration)
            {
                Type = type;
                Color = color;
                Duration = duration;
            }
        }
        
        /// <summary>
        /// VFX 타입 열거형
        /// </summary>
        private enum VFXType
        {
            Buff,       // 버프 효과
            Debuff,     // 디버프 효과
            Hit,        // 타격 효과
            Explosion,  // 폭발 효과
            Magic,      // 마법 효과
            Heal,       // 힐링 효과
            Special     // 특수 효과
        }
        #endregion

        #region Initialization
        /// <summary>
        /// 프리팹 레지스트리 초기화 - Plugin.cs에서 호출
        /// </summary>
        public static void Initialize()
        {
            if (isInitialized) return;

            try
            {
                // EmbeddedResource에서 프리팹 번들 로드
                LoadPrefabBundles();

                isInitialized = true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[프리팹 등록] 초기화 실패: {ex.Message}\n{ex.StackTrace}");
            }
        }
        #endregion

        #region Bundle Loading
        /// <summary>
        /// EmbeddedResource에서 AssetBundle 로드하여 프리팹 추출
        /// </summary>
        private static void LoadPrefabBundles()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            
            // 스킬트리 UI 전용 프리팹들 (ZNetScene 등록 불필요) - Plugin.cs에서 직접 처리
            var skillTreeUIBundles = new HashSet<string>
            {
                "captainskilltreeui", "skill_start", "skill_node", "job_icon"
            };
            
            // VFX/효과 프리팹들 (ZNetScene 등록 필요) - 게임에서 실제 사용됨
            var vfxEffectBundles = new HashSet<string>
            {
                // 영역 효과
                "area_circles_blue", "area_fire_red", "area_heal_green", 
                "area_magic_multicolor", "area_star_ellow",
                
                // 버프/디버프 효과
                "buff_01", "buff_02a", "buff_03a", "buff_03a_aura",
                "debuff", "debuff_03", "debuff_03_aura", "statusailment_01", "statusailment_01_aura",
                
                // 색종이 효과
                "confetti_blast_multicolor", "confetti_directional_multicolor",
                
                // 먼지/연기 효과
                "dust_permanently_blue",
                
                // 플래시 효과들
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
                
                // 쇼크/전기 관련 효과 (제거됨 - 사용하지 않음)
                
                // 스파클/반짝 효과
                "sparkle_ellow",
                
                // 특수 효과
                "taunt", // star aura 제거됨
                
                // 물폭발 효과들  
                "water_blast_blue", "water_blast_green",
                
            };
            
            foreach (string resourceName in resourceNames)
            {
                // CaptainSkillTree.asset.Resources. 또는 CaptainSkillTree.asset.VFX. 로 시작하는 리소스 처리
                if (resourceName.StartsWith("CaptainSkillTree.asset.Resources.") || 
                    resourceName.StartsWith("CaptainSkillTree.asset.VFX."))
                {
                    // BGM 파일은 제외 (이미 BGMManager에서 처리)
                    if (resourceName.Contains("Skill_Tree_BGM"))
                    {
                        continue;
                    }
                    
                    // 번들 이름 추출 (Resources 또는 VFX 폴더에서)
                    string bundleName;
                    if (resourceName.StartsWith("CaptainSkillTree.asset.Resources."))
                    {
                        bundleName = resourceName.Replace("CaptainSkillTree.asset.Resources.", "");
                    }
                    else
                    {
                        bundleName = resourceName.Replace("CaptainSkillTree.asset.VFX.", "");
                    }
                    
                    // 스킬트리 UI 프리팹들은 제외 (Plugin.cs에서 직접 처리)
                    if (skillTreeUIBundles.Contains(bundleName))
                    {
                        continue;
                    }
                    
                    // VFX/효과 프리팹들만 ZNetScene에 등록 (실제 게임에서 사용)
                    if (!vfxEffectBundles.Contains(bundleName))
                    {
                        continue;
                    }
                    
                    // 이미 로드된 번들인지 확인
                    if (loadedBundles.Contains(resourceName))
                    {
                        continue;
                    }
                    
                    try
                    {
                        LoadSingleBundle(assembly, resourceName);
                        loadedBundles.Add(resourceName);
                    }
                    catch (Exception ex)
                    {
                        Plugin.Log.LogError($"[프리팹 등록] '{bundleName}' 로드 실패: {ex.Message}");
                        failedRegistrations.Add(resourceName);
                    }
                }
            }
            
            // 로드 결과 요약
            int successCount = loadedBundles.Count;
            int totalVfxCount = vfxEffectBundles.Count;
            // Plugin.Log.LogDebug($"=== [프리팹 등록] VFX 로드 완료: {successCount}/{totalVfxCount}개 성공 ==="); // 제거: 과도한 로그
        }

        /// <summary>
        /// 단일 AssetBundle에서 프리팹 로드
        /// Jotunn AssetUtils를 사용하여 간소화된 에셋 로딩
        /// </summary>
        private static void LoadSingleBundle(Assembly assembly, string resourceName)
        {
            // 번들 이름 추출
            string bundleName = ExtractBundleName(resourceName);

            // Jotunn AssetUtils로 간소화된 로드 (스트림 처리 자동화)
            AssetBundle assetBundle = AssetUtils.LoadAssetBundleFromResources(bundleName, assembly);

            if (assetBundle == null)
            {
                throw new InvalidOperationException($"AssetBundle 로드 실패: {resourceName}");
            }

            try
            {
                // GameObject 검색
                var gameObjects = assetBundle.LoadAllAssets<GameObject>();

                // GameObject가 없으면 모든 에셋에서 검색
                if (gameObjects.Length == 0)
                {
                    var allAssets = assetBundle.LoadAllAssets();
                    var foundGameObjects = new List<GameObject>();

                    foreach (var asset in allAssets)
                    {
                        if (asset is GameObject gameObjectAsset)
                        {
                            foundGameObjects.Add(gameObjectAsset);
                        }
                    }

                    gameObjects = foundGameObjects.ToArray();
                }

                if (gameObjects.Length == 0)
                {
                    // 가상 GameObject 생성 시도
                    GameObject virtualGameObject;
                    if (vfxPrefabs.Contains(bundleName))
                    {
                        virtualGameObject = CreateAdvancedVirtualVFX(bundleName);
                    }
                    else
                    {
                        virtualGameObject = CreateBasicVirtualVFX(bundleName);
                    }

                    if (virtualGameObject != null)
                    {
                        RegisterPrefab(virtualGameObject, resourceName);
                    }
                    return;
                }

                // 찾은 GameObject들 등록
                foreach (var gameObject in gameObjects)
                {
                    if (gameObject != null)
                    {
                        RegisterPrefab(gameObject, resourceName);
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[프리팹 등록] 프리팹 로드 중 오류 ({resourceName}): {ex.Message}");
            }
            finally
            {
                // AssetBundle 언로드 (프리팹은 메모리에 유지)
                if (assetBundle != null)
                {
                    assetBundle.Unload(false);
                }
            }
        }

        /// <summary>
        /// 개별 프리팹을 딕셔너리에 등록
        /// </summary>
        private static void RegisterPrefab(GameObject prefab, string sourceName)
        {
            try
            {
                if (prefab == null) return;
                
                string prefabName = prefab.name;
                
                // 이름 정리 (Clone 등 제거)
                if (prefabName.Contains("(Clone)"))
                {
                    prefabName = prefabName.Replace("(Clone)", "").Trim();
                }
                
                // 중복 검사
                if (registeredPrefabs.ContainsKey(prefabName))
                {
                    return;
                }
                
                // DontDestroyOnLoad 적용하여 씬 전환 시에도 유지
                UnityEngine.Object.DontDestroyOnLoad(prefab);
                
                // 딕셔너리에 등록
                registeredPrefabs[prefabName] = prefab;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[프리팹 등록] 프리팹 등록 실패 ({prefab?.name}): {ex.Message}");
            }
        }
        #endregion

        #region ZNetScene Integration
        /// <summary>
        /// ZNetScene.instance에 등록된 프리팹들 추가 - ZNetScene 초기화 후 호출
        /// MMO 방식을 참고한 안정적인 등록 방식 적용
        /// </summary>
        public static void RegisterToZNetScene()
        {
            if (ZNetScene.instance == null)
            {
                Plugin.Log.LogWarning("[프리팹 등록] ZNetScene.instance가 null입니다");
                return;
            }

            int addedCount = 0;
            int skippedCount = 0;

            Plugin.Log.LogDebug("=== [프리팹 등록] ZNetScene 프리팹 등록 시작 ===");
            
            foreach (var kvp in registeredPrefabs)
            {
                try
                {
                    string prefabName = kvp.Key;
                    GameObject prefab = kvp.Value;
                    
                    if (prefab == null) continue;
                    
                    // 프리팹 이름 설정
                    prefab.name = prefabName;
                    
                    // 이미 등록된 프리팹인지 확인
                    if (ZNetScene.instance.GetPrefab(prefabName) != null)
                    {
                        skippedCount++;
                        continue;
                    }
                    
                    // m_prefabs 또는 m_nonNetViewPrefabs 리스트에 추가 (Jotunn 방식)
                    // ✅ ZNetView가 있으면 m_prefabs, 없으면 m_nonNetViewPrefabs
                    if (prefab.GetComponent<ZNetView>() != null)
                    {
                        if (!ZNetScene.instance.m_prefabs.Contains(prefab))
                        {
                            ZNetScene.instance.m_prefabs.Add(prefab);
                        }
                    }
                    else
                    {
                        if (!ZNetScene.instance.m_nonNetViewPrefabs.Contains(prefab))
                        {
                            ZNetScene.instance.m_nonNetViewPrefabs.Add(prefab);
                        }
                    }
                    
                    // m_namedPrefabs 딕셔너리에 안전하게 추가 (덮어쓰기 방지)
                    int hashCode = prefabName.GetStableHashCode();
                    var namedPrefabsField = typeof(ZNetScene).GetField("m_namedPrefabs",
                        BindingFlags.NonPublic | BindingFlags.Instance);

                    if (namedPrefabsField != null)
                    {
                        var namedPrefabs = namedPrefabsField.GetValue(ZNetScene.instance) as Dictionary<int, GameObject>;
                        if (namedPrefabs != null)
                        {
                            // 해시코드 충돌 감지 및 방지
                            if (namedPrefabs.ContainsKey(hashCode))
                            {
                                var existingPrefab = namedPrefabs[hashCode];
                                if (existingPrefab != null && existingPrefab.name != prefabName)
                                {
                                    // 다른 프리팹과 해시 충돌 발생 - 경고 후 건너뛰기
                                    Plugin.Log.LogWarning($"[프리팹 등록] 해시 충돌 감지! '{prefabName}' (hash: {hashCode}) vs 기존: '{existingPrefab.name}' - 기존 프리팹 보호를 위해 등록 건너뜀");
                                    skippedCount++;
                                    continue;
                                }
                                // 같은 이름의 프리팹이면 안전하게 업데이트
                                Plugin.Log.LogDebug($"[프리팹 등록] 동일 프리팹 업데이트: {prefabName}");
                            }

                            // 충돌 없으면 안전하게 등록
                            namedPrefabs[hashCode] = prefab;
                        }
                    }
                    
                    // 등록 검증
                    if (ZNetScene.instance.GetPrefab(prefabName) != null)
                    {
                        addedCount++;
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[프리팹 등록] ZNetScene 등록 실패 ({kvp.Key}): {ex.Message}");
                }
            }

            Plugin.Log.LogDebug($"=== [프리팹 등록] ZNetScene 등록 완료: {addedCount}/{registeredPrefabs.Count}개 성공 ===");
            
            // VFX 매니저에 재시도 요청
            try
            {
                CaptainSkillTree.VFX.VFXManager.RetryFailedPrefabs();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[프리팹 등록] VFX 매니저 재시도 요청 실패: {ex.Message}");
            }
        }
        #endregion

        #region Harmony Patches
        // ⚠️ ZNetScene.Awake 패치 완전 비활성화 (v0.1.118 - 무한 로딩 원인)
        // 외부 VFX 프리팹은 ZNetScene 등록 없이 로컬에서만 재생
        // 멀티플레이어 VFX 동기화는 별도 시스템으로 구현 예정
        /*
        /// <summary>
        /// ZNetScene.Awake 패치 - ZNetScene 초기화 후 자동으로 프리팹 등록
        /// </summary>
        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        public static class ZNetScene_Awake_Patch
        {
            static void Postfix(ZNetScene __instance)
            {
                try
                {
                    __instance.StartCoroutine(RegisterPrefabsCoroutine(__instance));
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[프리팹 등록] ZNetScene.Awake 패치 오류: {ex.Message}");
                }
            }

            private static System.Collections.IEnumerator RegisterPrefabsCoroutine(ZNetScene znetScene)
            {
                // ZNetScene 초기화 대기
                yield return new WaitForSeconds(1f);

                // ✅ 플레이어 사망 체크 추가 (1초 대기 후)
                if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
                {
                    Plugin.Log.LogInfo("[프리팹 등록] 플레이어 사망으로 프리팹 등록 중단");
                    yield break;
                }

                try
                {
                    RegisterToZNetScene();
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[프리팹 등록] VFX 프리팹 등록 실패: {ex.Message}");
                }
            }

        }
        */

        // ⚠️ VFXManager 패치 비활성화 (v0.1.119 - SimpleVFX로 대체)
        // SimpleVFX.cs에서 ZNetScene.Awake 패치로 초기화됨
        /*
        /// <summary>
        /// ZNetScene.Awake 패치 - VFX 프리팹 캐싱 (WackyEpicMMOSystem 방식)
        /// </summary>
        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        public static class ZNetScene_Awake_VFXCache_Patch
        {
            static void Postfix(ZNetScene __instance)
            {
                try
                {
                    // VFXManager 프리팹 캐싱 (WackyEpicMMOSystem 방식)
                    VFX.VFXManager.Initialize();
                    Plugin.Log?.LogInfo("[VFX] ZNetScene.Awake에서 VFXManager 초기화 완료");
                }
                catch (Exception ex)
                {
                    Plugin.Log?.LogError($"[VFX] ZNetScene.Awake 패치 오류: {ex.Message}");
                }
            }
        }
        */
        #endregion

        #region Helper Methods
        /// <summary>
        /// 리소스 이름에서 번들 이름 추출
        /// </summary>
        private static string ExtractBundleName(string resourceName)
        {
            try
            {
                if (resourceName.StartsWith("CaptainSkillTree.asset.Resources."))
                {
                    return resourceName.Replace("CaptainSkillTree.asset.Resources.", "");
                }
                else if (resourceName.StartsWith("CaptainSkillTree.asset.VFX."))
                {
                    return resourceName.Replace("CaptainSkillTree.asset.VFX.", "");
                }
                
                // 마지막 . 이후의 부분을 반환
                int lastDotIndex = resourceName.LastIndexOf('.');
                if (lastDotIndex >= 0 && lastDotIndex < resourceName.Length - 1)
                {
                    return resourceName.Substring(lastDotIndex + 1);
                }
                
                return resourceName;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[프리팹 등록] ExtractBundleName 실패 ({resourceName}): {ex.Message}");
                return resourceName;
            }
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// 등록된 프리팹 수 반환
        /// </summary>
        public static int GetRegisteredPrefabCount() => registeredPrefabs.Count;

        /// <summary>
        /// 특정 프리팹이 등록되었는지 확인
        /// </summary>
        public static bool IsPrefabRegistered(string prefabName) => registeredPrefabs.ContainsKey(prefabName);

        /// <summary>
        /// 등록된 프리팹 이름 목록 반환
        /// </summary>
        public static List<string> GetRegisteredPrefabNames() => new List<string>(registeredPrefabs.Keys);

        /// <summary>
        /// 특정 프리팹 GameObject 반환 (대소문자 무시)
        /// </summary>
        public static GameObject GetRegisteredPrefab(string prefabName)
        {
            // 정확한 이름으로 먼저 시도
            if (registeredPrefabs.TryGetValue(prefabName, out GameObject prefab))
                return prefab;

            // 대소문자 무시하고 검색
            foreach (var kvp in registeredPrefabs)
            {
                if (string.Equals(kvp.Key, prefabName, StringComparison.OrdinalIgnoreCase))
                    return kvp.Value;
            }

            return null;
        }

        /// <summary>
        /// 모든 등록된 프리팹 딕셔너리 반환 (WackyEpicMMOSystem 방식 ZNetScene 등록용)
        /// </summary>
        public static Dictionary<string, GameObject> GetAllRegisteredPrefabs()
        {
            return new Dictionary<string, GameObject>(registeredPrefabs);
        }
        #endregion

        #region Virtual VFX Creation Methods
        /// <summary>
        /// 고급 가상 VFX GameObject 생성 - 알려진 빈 AssetBundle에 대해 타입별 고급 설정
        /// </summary>
        private static GameObject CreateAdvancedVirtualVFX(string bundleName)
        {
            try
            {
                // VFX 설정 확인
                if (!vfxConfigurations.TryGetValue(bundleName, out VFXConfig config))
                {
                    config = new VFXConfig { Type = VFXType.Buff, Color = Color.cyan, Duration = 3.0f };
                }
                
                // 기본 GameObject 생성
                var vfxObject = new GameObject(bundleName);
                vfxObject.layer = LayerMask.NameToLayer("Default");
                
                // VFX 타입별 컴포넌트 추가
                switch (config.Type)
                {
                    case VFXType.Buff:
                    case VFXType.Debuff:
                        CreateBuffVFXComponents(vfxObject, config);
                        break;
                        
                    case VFXType.Hit:
                    case VFXType.Explosion:
                        CreateHitVFXComponents(vfxObject, config);
                        break;
                        
                    case VFXType.Magic:
                    case VFXType.Heal:
                        CreateMagicVFXComponents(vfxObject, config);
                        break;
                        
                    case VFXType.Special:
                        CreateSpecialVFXComponents(vfxObject, config);
                        break;
                        
                    default:
                        CreateBasicVFXComponents(vfxObject, config);
                        break;
                }
                
                // 자동 제거 컴포넌트 추가 (메모리 누수 방지)
                var autoDestroy = vfxObject.AddComponent<TimedDestruction>();
                autoDestroy.m_timeout = config.Duration;
                
                return vfxObject;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[가상 VFX] 고급 가상 VFX 생성 실패 ({bundleName}): {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 기본 가상 VFX GameObject 생성 - 알려지지 않은 빈 AssetBundle에 대한 기본 생성
        /// </summary>
        private static GameObject CreateBasicVirtualVFX(string bundleName)
        {
            try
            {
                // 기본 GameObject 생성
                var vfxObject = new GameObject(bundleName);
                vfxObject.layer = LayerMask.NameToLayer("Default");
                
                // 기본 VFX 구성 (간단한 파티클)
                var config = new VFXConfig { Type = VFXType.Special, Color = Color.white, Duration = 2.0f };
                CreateBasicVFXComponents(vfxObject, config);
                
                // 자동 제거 컴포넌트 추가
                var autoDestroy = vfxObject.AddComponent<TimedDestruction>();
                autoDestroy.m_timeout = config.Duration;
                
                return vfxObject;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[가상 VFX] 기본 가상 VFX 생성 실패 ({bundleName}): {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Buff/Debuff 타입 VFX 컴포넌트 생성
        /// </summary>
        private static void CreateBuffVFXComponents(GameObject vfxObject, VFXConfig config)
        {
            // 파티클 시스템 추가
            var particleSystem = vfxObject.AddComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startLifetime = config.Duration * 0.8f; // VFX 지속시간보다 약간 짧게
            main.startSpeed = 0.5f;
            main.startSize = config.Type == VFXType.Buff ? 1.0f : 0.8f; // Buff가 Debuff보다 크게
            main.startColor = config.Color;
            main.maxParticles = 20;
            
            var emission = particleSystem.emission;
            emission.rateOverTime = 10f;
            
            var shape = particleSystem.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 0.5f;
        }
        
        /// <summary>
        /// Hit/Explosion 타입 VFX 컴포넌트 생성
        /// </summary>
        private static void CreateHitVFXComponents(GameObject vfxObject, VFXConfig config)
        {
            // 파티클 시스템 추가 (폭발 효과)
            var particleSystem = vfxObject.AddComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startLifetime = 0.5f; // 짧고 강렬한 효과
            main.startSpeed = 5.0f;
            main.startSize = config.Type == VFXType.Explosion ? 2.0f : 1.0f;
            main.startColor = config.Color;
            main.maxParticles = config.Type == VFXType.Explosion ? 50 : 30;
            
            var emission = particleSystem.emission;
            emission.SetBursts(new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(0.0f, emission.GetBurst(0).count.constant)
            });
            emission.rateOverTime = 0f; // Burst만 사용
            
            var shape = particleSystem.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = config.Type == VFXType.Explosion ? 1.0f : 0.3f;
        }
        
        /// <summary>
        /// Magic/Heal 타입 VFX 컴포넌트 생성
        /// </summary>
        private static void CreateMagicVFXComponents(GameObject vfxObject, VFXConfig config)
        {
            // 파티클 시스템 추가 (매직 효과)
            var particleSystem = vfxObject.AddComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startLifetime = config.Duration * 0.6f;
            main.startSpeed = 2.0f;
            main.startSize = 0.8f;
            main.startColor = config.Color;
            main.maxParticles = 40;
            
            var emission = particleSystem.emission;
            emission.rateOverTime = 25f;
            
            var shape = particleSystem.shape;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 15f;
            shape.radius = 0.3f;
            
            // 마법 효과에 빛 추가
            var light = vfxObject.AddComponent<Light>();
            light.color = config.Color;
            light.intensity = config.Type == VFXType.Heal ? 1.0f : 0.7f;
            light.range = 3.0f;
            light.type = LightType.Point;
        }
        
        /// <summary>
        /// Special 타입 VFX 컴포넌트 생성
        /// </summary>
        private static void CreateSpecialVFXComponents(GameObject vfxObject, VFXConfig config)
        {
            // 특수 효과용 복합 파티클 시스템
            var particleSystem = vfxObject.AddComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startLifetime = config.Duration;
            main.startSpeed = 1.5f;
            main.startSize = 1.2f;
            main.startColor = config.Color;
            main.maxParticles = 35;
            
            var emission = particleSystem.emission;
            emission.rateOverTime = 15f;
            
            var shape = particleSystem.shape;
            shape.shapeType = ParticleSystemShapeType.Circle;
            shape.radius = 0.8f;
            
            var velocityOverLifetime = particleSystem.velocityOverLifetime;
            velocityOverLifetime.enabled = true;
            velocityOverLifetime.radial = 2.0f;
        }
        
        /// <summary>
        /// 기본 VFX 컴포넌트 생성 (모든 타입에서 공통 사용)
        /// </summary>
        private static void CreateBasicVFXComponents(GameObject vfxObject, VFXConfig config)
        {
            // 간단한 파티클 시스템
            var particleSystem = vfxObject.AddComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startLifetime = config.Duration * 0.7f;
            main.startSpeed = 1.0f;
            main.startSize = 1.0f;
            main.startColor = config.Color;
            main.maxParticles = 25;
            
            var emission = particleSystem.emission;
            emission.rateOverTime = 12f;
            
            var shape = particleSystem.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 0.4f;
        }
        #endregion

        #region Debug Methods
        /// <summary>
        /// 등록된 모든 프리팹 정보를 로그로 출력
        /// </summary>
        public static void LogAllRegisteredPrefabs()
        {
            Plugin.Log.LogDebug("=== [프리팹 등록] 등록된 프리팹 목록 ===");
            
            if (registeredPrefabs.Count == 0)
            {
                Plugin.Log.LogDebug("등록된 프리팹이 없습니다.");
                return;
            }
            
            foreach (var kvp in registeredPrefabs)
            {
                GameObject prefab = kvp.Value;
                string info = $"  - {kvp.Key}";
                
                if (prefab != null)
                {
                    info += $" (GameObject: {prefab.name})";
                    
                    // 컴포넌트 정보 추가
                    var components = prefab.GetComponents<Component>();
                    if (components.Length > 1) // Transform은 기본이므로 제외
                    {
                        info += $" [컴포넌트: {components.Length - 1}개]";
                    }
                }
                else
                {
                    info += " (NULL!)";
                }
                
                Plugin.Log.LogInfo(info);
            }
            
            Plugin.Log.LogDebug($"총 {registeredPrefabs.Count}개 프리팹 등록됨");
        }
        #endregion
    }
}
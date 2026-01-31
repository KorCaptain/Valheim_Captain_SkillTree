using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using HarmonyLib;
using System.Reflection;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.SkillTree.CriticalSystem;
using System.Collections;
using System.Linq;
using CaptainSkillTree.Audio;
using CaptainSkillTree.Prefab;
using CaptainSkillTree.VFX;
using Jotunn.Utils;
using Jotunn.Entities;
using Jotunn.Managers;

namespace CaptainSkillTree
{
    [BepInPlugin("CaptainSkillTree.SkillTreeMod", "Captain SkillTree Mod", "0.1.257")]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    public partial class Plugin : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource Log;
        private static GameObject skillTreeIconObj;
        private static Sprite swordIcon;
        private static AssetBundle iconAssetBundle;
        private static AssetBundle uiAssetBundle;
        private static AssetBundle customIconBundle;
        private static AssetBundle jobIconBundle;
        private static AssetBundle vfxBundle;
        private static AssetBundle jobVfxBundle;
        public static int SkillTreePoint = 0;
        private static CaptainSkillTree.Gui.SkillTreeUI skillTreeUI;
        private static Sprite customSkillTreeIcon;

        /// <summary>
        /// 스킬트리가 현재 열려있는지 확인 (BGM 매니저용)
        /// </summary>
        public static bool IsSkillTreeOpen => skillTreeUI != null && skillTreeUI.panel != null && skillTreeUI.panel.activeSelf;
        private static Plugin? _instance;
        public static Plugin? Instance => _instance;

        // 핵심 기능 보호를 위한 안전 장치 시스템
        private static bool _coreSystemsInitialized = false;
        private static bool _emergencyMode = false;
        private static int _iconCreationAttempts = 0;
        private const int MAX_ICON_ATTEMPTS = 5;

        private void Awake()
        {
            Log = Logger;
            _instance = this;

            // Config 시스템 초기화
            SkillTreeConfig.Initialize(Config);

            // Staff Tree Config 시스템 초기화 (힐러모드 설정 포함)
            Staff_Config.InitConfig(Config);

            // 안전 장치 초기화
            InitializeCoreSafeguards();

            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("CaptainSkillTree.Mod");

            // Harmony 패치 등록
            Log.LogInfo("========== WackyEpicMMOSystem 설치 확인 중... ==========");

            try
            {
                harmony.PatchAll();
                Log.LogInfo("========== WackyEpicMMOSystem 설치 확인 및 연동 성공! ==========");
                Log.LogDebug("[MMO 연동] 제작 강화 시스템 Harmony 패치 등록 완료");
            }
            catch (Exception ex)
            {
                Log.LogError($"=== [CRITICAL] Harmony.PatchAll() 실패: {ex.Message} ===");
                Log.LogError($"=== [CRITICAL] StackTrace: {ex.StackTrace} ===");
            }

            // 입력 리스너 등록 (중복 방지)
            InitializeInputListener();

            SkillTreeData.RegisterAll();

            // 커스텀 스킬트리 아이콘 로드 시도 (안전 장치 포함)
            SafeLoadCustomIcon();

            // BGM 시스템 초기화
            InitializeBGMSystem();

            // 프리팹 레지스트리 초기화
            InitializePrefabSystem();

            // NOTE: AnimationSpeedManager 핸들러는 Game.Awake Postfix에서 등록됨 (Rule 14-3)
            // Plugin.Patches.cs의 AttackSpeedHandler_Game_Awake_Patch 참조

            // Jotunn CommandManager로 콘솔 명령어 등록
            RegisterJotunnCommands();

            // 핵심 시스템 초기화 완료 표시
            _coreSystemsInitialized = true;
            Log.LogWarning("========== Captain SkillTree Mod 로딩 성공 ==========");
        }

        /// <summary>
        /// Jotunn CommandManager를 사용하여 콘솔 명령어 등록
        /// </summary>
        private void RegisterJotunnCommands()
        {
            try
            {
                CommandManager.Instance.AddConsoleCommand(new SkillAddConsoleCommand());
                CommandManager.Instance.AddConsoleCommand(new SkillResetConsoleCommand());
                Log.LogInfo("[Jotunn] 콘솔 명령어 등록 완료: skilladd, skillreset");
            }
            catch (Exception ex)
            {
                Log.LogError($"[Jotunn] 콘솔 명령어 등록 실패: {ex.Message}");
            }
        }

        // NOTE: AnimationSpeedManager 핸들러는 Plugin.Patches.cs의
        // AttackSpeedHandler_Game_Awake_Patch에서 Game.Awake Postfix로 등록됨 (Rule 14-3)

        // 핵심 안전 장치 초기화
        private static void InitializeCoreSafeguards()
        {
            try
            {
                if (_coreSystemsInitialized) return;
                ValidateEssentialResources();
            }
            catch (Exception ex)
            {
                Log.LogError($"[안전 장치] 초기화 실패: {ex.Message}");
                _emergencyMode = true;
            }
        }

        // 입력 리스너 안전 초기화 (Gui.SkillTreeInputListener 사용 - T/G/H/Y 키 처리 포함)
        private static void InitializeInputListener()
        {
            try
            {
                if (CaptainSkillTree.SkillTreeInputListener.Instance == null)
                {
                    var go = new GameObject("SkillTreeInputListener");
                    go.AddComponent<CaptainSkillTree.SkillTreeInputListener>();
                    DontDestroyOnLoad(go);
                }
            }
            catch (Exception ex)
            {
                Log.LogError($"[안전 장치] InputListener 초기화 실패: {ex.Message}");
            }
        }

        // 필수 리소스 검증
        private static void ValidateEssentialResources()
        {
            var requiredBundles = new string[] {
                "CaptainSkillTree.asset.Resources.skill_node",
                "CaptainSkillTree.asset.Resources.captainskilltreeui",
                "CaptainSkillTree.asset.Resources.skill_start",
                "CaptainSkillTree.asset.Resources.job_icon"
            };

            foreach (var bundleName in requiredBundles)
            {
                var assembly = typeof(Plugin).Assembly;
                var stream = assembly.GetManifestResourceStream(bundleName);
                if (stream == null)
                {
                    throw new Exception($"필수 리소스 누락: {bundleName}");
                }
                stream.Close();
            }

            ResetEmergencyMode();
        }

        private static void ResetEmergencyMode()
        {
            _emergencyMode = false;
            _iconCreationAttempts = 0;
        }

        // 커스텀 아이콘 안전 로드
        private static void SafeLoadCustomIcon()
        {
            try
            {
                Log.LogDebug("[아이콘] 커스텀 아이콘 로드 시작");

                var jobIconBundle = GetJobIconBundle();
                if (jobIconBundle != null)
                {
                    Log.LogDebug("[아이콘] job_icon 번들 로드 성공");

                    string[] jobIconNames = {
                        "Paladin_unlock", "Tanker_unlock", "Berserker_unlock",
                        "Archer_unlock", "Mage_unlock", "Rogue_unlock",
                        "Paladin", "Tanker", "Berserker", "Archer", "Mage", "Rogue"
                    };

                    foreach (var iconName in jobIconNames)
                    {
                        var testSprite = jobIconBundle.LoadAsset<Sprite>(iconName);
                        if (testSprite != null)
                        {
                            Log.LogDebug($"[아이콘] job_icon에서 {iconName} 로드 성공: {testSprite.name}");
                        }
                    }
                }

                var customIconBundle = GetCustomIconBundle();
                if (customIconBundle != null)
                {
                    Log.LogDebug("[아이콘] 커스텀 아이콘 번들 로드 성공");

                    var allAssets = customIconBundle.GetAllAssetNames();
                    Log.LogDebug($"[아이콘] 번들 에셋 수: {allAssets.Length}");

                    foreach (var assetName in allAssets)
                    {
                        Log.LogDebug($"[아이콘] 번들 에셋: {assetName}");
                    }

                    customSkillTreeIcon = customIconBundle.LoadAsset<Sprite>("skill_start");
                    if (customSkillTreeIcon != null)
                    {
                        Log.LogDebug("[아이콘] skill_start 스프라이트 로드 성공");
                    }
                    else
                    {
                        TryAlternativeIconNames(customIconBundle);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError($"[아이콘] 커스텀 아이콘 로드 중 오류: {ex.Message}");
                Log.LogError($"[아이콘] StackTrace: {ex.StackTrace}");
            }
        }

        private static void TryAlternativeIconNames(AssetBundle bundle)
        {
            var possibleNames = new string[] {
                "skill_start", "SkillStart", "skill_tree_start",
                "captainskilltreeicon", "CaptainSkillTreeIcon",
                "SkillTreeIcon", "skilltreeicon"
            };

            foreach (var name in possibleNames)
            {
                var sprite = bundle.LoadAsset<Sprite>(name);
                if (sprite != null)
                {
                    customSkillTreeIcon = sprite;
                    Log.LogDebug($"[아이콘] 대체 이름으로 아이콘 로드 성공: {name}");
                    return;
                }
            }

            Log.LogWarning("[아이콘] 모든 아이콘 이름 시도 실패 - 게임 기본 아이콘 사용");
        }

        #region AssetBundle Loaders

        public static AssetBundle GetIconAssetBundle()
        {
            if (iconAssetBundle == null)
            {
                var assembly = typeof(Plugin).Assembly;
                var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.skill_node");
                if (stream != null)
                    iconAssetBundle = AssetBundle.LoadFromStream(stream);
            }
            return iconAssetBundle;
        }

        public static AssetBundle GetUIAssetBundle()
        {
            if (uiAssetBundle == null)
            {
                var assembly = typeof(Plugin).Assembly;
                var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.captainskilltreeui");
                if (stream != null)
                    uiAssetBundle = AssetBundle.LoadFromStream(stream);
            }
            return uiAssetBundle;
        }

        public static AssetBundle GetCustomIconBundle()
        {
            if (customIconBundle == null)
            {
                try
                {
                    var assembly = typeof(Plugin).Assembly;
                    Log.LogDebug($"[번들] Assembly: {assembly.FullName}");

                    var resourceNames = assembly.GetManifestResourceNames();
                    Log.LogDebug($"[번들] 사용 가능한 리소스 수: {resourceNames.Length}");

                    bool foundIconResource = false;
                    foreach (var name in resourceNames)
                    {
                        Log.LogDebug($"[번들] 리소스: {name}");
                        if (name.Contains("skill_start"))
                        {
                            foundIconResource = true;
                            Log.LogDebug($"[번들] 시작 아이콘 리소스 발견: {name}");
                        }
                    }

                    if (!foundIconResource)
                    {
                        Log.LogError("[번들] skill_start 리소스가 DLL에 포함되지 않음!");
                    }

                    var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.skill_start");
                    if (stream != null)
                    {
                        Log.LogDebug($"[번들] 스트림 로드 성공, 크기: {stream.Length} bytes");
                        customIconBundle = AssetBundle.LoadFromStream(stream);
                        if (customIconBundle != null)
                        {
                            Log.LogDebug("[번들] AssetBundle 로드 성공");
                        }
                        else
                        {
                            Log.LogError("[번들] AssetBundle.LoadFromStream 실패");
                        }
                    }
                    else
                    {
                        Log.LogError("[번들] 리소스 스트림이 null");
                    }
                }
                catch (Exception ex)
                {
                    Log.LogError($"[번들] GetCustomIconBundle 오류: {ex.Message}");
                }
            }
            return customIconBundle;
        }

        public static AssetBundle GetJobIconBundle()
        {
            if (jobIconBundle == null)
            {
                var assembly = typeof(Plugin).Assembly;
                var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.job_icon");
                if (stream != null)
                {
                    jobIconBundle = AssetBundle.LoadFromStream(stream);
                    if (jobIconBundle != null)
                    {
                        Log.LogDebug("[아이콘] job_icon 번들 로드 성공");
                        var allAssets = jobIconBundle.GetAllAssetNames();
                        Log.LogDebug($"[아이콘] job_icon 번들 에셋 수: {allAssets.Length}");
                        foreach (var assetName in allAssets)
                        {
                            Log.LogDebug($"[아이콘] job_icon 에셋: {assetName}");
                        }
                    }
                    else
                    {
                        Log.LogError("[아이콘] job_icon AssetBundle.LoadFromStream 실패");
                    }
                }
                else
                {
                    Log.LogWarning("[아이콘] job_icon 리소스 스트림이 null");
                }
            }
            return jobIconBundle;
        }

        public static AssetBundle GetVfxBundle()
        {
            if (vfxBundle == null)
            {
                var assembly = typeof(Plugin).Assembly;
                var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.VFX.vfxbundle");
                if (stream != null)
                {
                    vfxBundle = AssetBundle.LoadFromStream(stream);
                    if (vfxBundle != null)
                    {
                        Log.LogDebug("[VFX] VFX 폴더에서 vfxbundle 번들 로드 성공");
                    }
                    else
                    {
                        Log.LogError("[VFX] VFX 폴더에서 vfxbundle 번들 로드 실패");
                    }
                }
                else
                {
                    Log.LogError("[VFX] VFX 폴더에서 vfxbundle 리소스 스트림을 찾을 수 없음");
                }
            }
            return vfxBundle;
        }

        public static AssetBundle GetJobVfxBundle()
        {
            if (jobVfxBundle == null)
            {
                var assembly = typeof(Plugin).Assembly;
                var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.job_vfx");
                if (stream != null)
                {
                    jobVfxBundle = AssetBundle.LoadFromStream(stream);
                    if (jobVfxBundle != null)
                    {
                        Log.LogDebug("[VFX] job_vfx 번들 로드 성공");
                    }
                    else
                    {
                        Log.LogError("[VFX] job_vfx 번들 로드 실패");
                    }
                }
                else
                {
                    Log.LogError("[VFX] job_vfx 리소스 스트림을 찾을 수 없음");
                }
            }
            return jobVfxBundle;
        }

        public static T LoadEmbeddedAsset<T>(string resourcePath) where T : UnityEngine.Object
        {
            try
            {
                var assembly = typeof(Plugin).Assembly;
                var stream = assembly.GetManifestResourceStream(resourcePath);
                if (stream == null)
                {
                    Log.LogWarning($"[LoadEmbeddedAsset] 리소스를 찾을 수 없음: {resourcePath}");
                    return null;
                }

                var bundle = AssetBundle.LoadFromStream(stream);
                if (bundle == null)
                {
                    Log.LogWarning($"[LoadEmbeddedAsset] AssetBundle 로드 실패: {resourcePath}");
                    stream.Close();
                    return null;
                }

                var allAssets = bundle.LoadAllAssets<T>();
                if (allAssets.Length > 0)
                {
                    var asset = allAssets[0];
                    Log.LogInfo($"[LoadEmbeddedAsset] 에셋 로드 성공: {resourcePath} -> {asset.name}");
                    return asset;
                }

                Log.LogWarning($"[LoadEmbeddedAsset] {typeof(T).Name} 타입의 에셋을 찾을 수 없음: {resourcePath}");
                bundle.Unload(false);
                stream.Close();
                return null;
            }
            catch (Exception ex)
            {
                Log.LogError($"[LoadEmbeddedAsset] 에셋 로드 오류: {resourcePath} - {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Inventory Icon System

        // 인벤토리 UI가 열릴 때마다 버튼 생성
        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Show))]
        public static class InventoryShowPatch
        {
            static bool Prepare() => true;

            public static void Postfix()
            {
                try
                {
                    ShowSkillTreeIcon();
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[스킬트리] InventoryShowPatch 오류: {ex.Message}");
                    Plugin.Log.LogError($"[스킬트리] StackTrace: {ex.StackTrace}");
                }
            }
        }

        private static void ShowSkillTreeIcon()
        {
            if (_emergencyMode || _iconCreationAttempts >= MAX_ICON_ATTEMPTS)
            {
                Log.LogWarning("[안전 장치] 비상 모드 또는 최대 시도 횟수 초과 - 아이콘 생성 건너뛰기");
                return;
            }

            try
            {
                if (!ValidatePrerequisites())
                {
                    Log.LogWarning("[안전 장치] 전제 조건 미충족 - 아이콘 생성 연기");
                    return;
                }

                var mmoType = Type.GetType("EpicMMOSystem.MyUI, EpicMMOSystem");

                if (mmoType != null)
                {
                    if (TryCreateMMOStyleIcon())
                    {
                        ResetIconAttempts();
                        return;
                    }
                }

                CreateFallbackSkillTreeIcon();
            }
            catch (Exception ex)
            {
                _iconCreationAttempts++;
                Log.LogError($"[안전 장치] SkillTreeIcon 생성 중 예외 (시도 {_iconCreationAttempts}/{MAX_ICON_ATTEMPTS}): {ex.Message}");
                Log.LogError($"[안전 장치] StackTrace: {ex.StackTrace}");

                if (_iconCreationAttempts < MAX_ICON_ATTEMPTS)
                {
                    try
                    {
                        CreateFallbackSkillTreeIcon();
                    }
                    catch (Exception fallbackEx)
                    {
                        Log.LogError($"[안전 장치] 대체 아이콘 생성도 실패: {fallbackEx.Message}");
                    }
                }
                else
                {
                    Log.LogError("[안전 장치] 최대 시도 횟수 도달 - 비상 모드 활성화");
                    _emergencyMode = true;
                }
            }
        }

        private static bool ValidatePrerequisites()
        {
            try
            {
                if (Player.m_localPlayer == null)
                {
                    Log.LogDebug("[안전 장치] Player가 아직 로드되지 않음");
                    return false;
                }

                if (InventoryGui.instance == null)
                {
                    Log.LogDebug("[안전 장치] InventoryGui가 아직 준비되지 않음");
                    return false;
                }

                if (GetCustomIconBundle() == null && swordIcon == null)
                {
                    Log.LogWarning("[안전 장치] 아이콘 리소스가 준비되지 않음 - 로드 시도");
                    SafeLoadCustomIcon();
                    LoadGameDefaultIcon();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogError($"[안전 장치] 전제 조건 검증 실패: {ex.Message}");
                return false;
            }
        }

        private static void LoadGameDefaultIcon()
        {
            try
            {
                if (swordIcon == null && ObjectDB.instance != null)
                {
                    var prefab = ObjectDB.instance.GetItemPrefab("SwordIron");
                    if (prefab != null)
                    {
                        var itemDrop = prefab.GetComponent<ItemDrop>();
                        if (itemDrop != null)
                        {
                            swordIcon = itemDrop.m_itemData.GetIcon();
                            Log.LogDebug("[안전 장치] 게임 기본 아이콘 로드 성공");
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 게임 기본 아이콘 로드 실패 (정상적 - 대체 아이콘 사용)
            }
        }

        private static void ResetIconAttempts()
        {
            _iconCreationAttempts = 0;
        }

        private static bool TryCreateMMOStyleIcon()
        {
            try
            {
                var mmoType = Type.GetType("EpicMMOSystem.MyUI, EpicMMOSystem");
                if (mmoType == null) { Plugin.Log.LogWarning("EpicMMOSystem.MyUI 타입을 찾을 수 없음"); return false; }
                var navigationPanelField = mmoType.GetField("navigationPanel", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (navigationPanelField == null) { Plugin.Log.LogWarning("navigationPanel 필드를 찾을 수 없음"); return false; }
                var navigationPanel = navigationPanelField.GetValue(null) as GameObject;
                if (navigationPanel == null) { Plugin.Log.LogWarning("navigationPanel이 null"); return false; }
                var buttons = navigationPanel.transform.Find("Buttons");
                if (buttons == null) { Plugin.Log.LogWarning("Buttons 오브젝트를 찾을 수 없음"); return false; }

                var exist = buttons.Find("ButtonSkillTree");
                if (exist != null)
                {
                    skillTreeIconObj = exist.gameObject;
                    skillTreeIconObj.SetActive(true);
                    return true;
                }

                var buttonLevelSystem = buttons.Find("ButtonLevelSystem");
                if (buttonLevelSystem == null) { Plugin.Log.LogWarning("ButtonLevelSystem 오브젝트를 찾을 수 없음"); return false; }
                skillTreeIconObj = GameObject.Instantiate(buttonLevelSystem.gameObject, buttons);
                skillTreeIconObj.name = "ButtonSkillTree";

                var image = skillTreeIconObj.GetComponent<Image>();
                if (image == null) image = skillTreeIconObj.GetComponentInChildren<Image>();
                if (customSkillTreeIcon != null)
                {
                    image.sprite = customSkillTreeIcon;
                }
                else
                {
                    if (swordIcon == null)
                    {
                        var objdb = ObjectDB.instance;
                        if (objdb == null) { Plugin.Log.LogWarning("ObjectDB.instance가 null"); return false; }
                        var prefab = objdb.GetItemPrefab("SwordIron");
                        if (prefab == null) { Plugin.Log.LogWarning("SwordIron 프리팹을 찾을 수 없음"); return false; }
                        var itemDrop = prefab.GetComponent<ItemDrop>();
                        if (itemDrop == null) { Plugin.Log.LogWarning("SwordIron에 ItemDrop 컴포넌트가 없음"); return false; }
                        swordIcon = itemDrop.m_itemData.GetIcon();
                        if (swordIcon == null) { Plugin.Log.LogWarning("SwordIron 아이콘(Sprite)을 찾을 수 없음"); return false; }
                    }
                    image.sprite = swordIcon;
                }

                var rect = skillTreeIconObj.GetComponent<RectTransform>();
                var origRect = buttonLevelSystem.GetComponent<RectTransform>();
                rect.anchoredPosition = origRect.anchoredPosition + new Vector2(80, 0);
                rect.sizeDelta = origRect.sizeDelta;
                rect.anchorMin = origRect.anchorMin;
                rect.anchorMax = origRect.anchorMax;
                rect.pivot = origRect.pivot;
                rect.localScale = origRect.localScale;

                foreach (Transform child in skillTreeIconObj.transform)
                {
                    if (child.GetComponent<Image>() == null)
                        child.gameObject.SetActive(false);
                }

                SetupIconClickEvent();

                var canvas = skillTreeIconObj.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    Log.LogDebug($"[시작 아이콘] 부모 Canvas sortingOrder: {canvas.sortingOrder}");
                }

                skillTreeIconObj.SetActive(true);
                Log.LogDebug($"[시작 아이콘] 아이콘 생성 및 활성화 완료");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"아이콘 생성 실패: {ex.Message}");
                return false;
            }
        }

        private static void CreateFallbackSkillTreeIcon()
        {
            var inv = InventoryGui.instance;
            if (inv == null)
            {
                Plugin.Log.LogWarning("[디버그] InventoryGui.instance가 null, 아이콘 생성 불가");
                return;
            }

            Plugin.Log.LogInfo("[디버그] InventoryGui 인스턴스 확인됨");

            if (skillTreeIconObj != null)
            {
                skillTreeIconObj.SetActive(true);
                Plugin.Log.LogDebug("기존 대체 아이콘 활성화");
                return;
            }

            try
            {
                var canvas = inv.GetComponentInChildren<Canvas>();
                if (canvas == null)
                {
                    Plugin.Log.LogWarning("InventoryGui에서 Canvas를 찾을 수 없음");
                    return;
                }

                var inventoryPanel = canvas.transform.Find("Player");
                if (inventoryPanel == null)
                {
                    Plugin.Log.LogWarning("Player 패널을 찾을 수 없음, Canvas에 직접 배치");
                    inventoryPanel = canvas.transform;
                }

                var buttonGO = new GameObject("ButtonSkillTree_Fallback");
                buttonGO.transform.SetParent(inventoryPanel, false);

                var rect = buttonGO.AddComponent<RectTransform>();
                rect.anchorMin = new Vector2(0.5f, 0f);
                rect.anchorMax = new Vector2(0.5f, 0f);
                rect.pivot = new Vector2(0.5f, 0f);
                rect.anchoredPosition = new Vector2(100f, 10f);
                rect.sizeDelta = new Vector2(60f, 60f);

                var image = buttonGO.AddComponent<Image>();

                if (customSkillTreeIcon != null)
                {
                    image.sprite = customSkillTreeIcon;
                }
                else
                {
                    if (swordIcon == null)
                    {
                        var objdb = ObjectDB.instance;
                        if (objdb != null)
                        {
                            var prefab = objdb.GetItemPrefab("SwordIron");
                            if (prefab != null)
                            {
                                var itemDrop = prefab.GetComponent<ItemDrop>();
                                if (itemDrop != null)
                                {
                                    swordIcon = itemDrop.m_itemData.GetIcon();
                                }
                            }
                        }
                    }
                    image.sprite = swordIcon;
                }

                if (image.sprite == null)
                {
                    image.color = new Color(0.2f, 0.6f, 1f, 0.8f);
                    Plugin.Log.LogInfo("스프라이트가 없어 단색으로 표시");
                }

                var button = buttonGO.AddComponent<Button>();
                button.targetGraphic = image;

                skillTreeIconObj = buttonGO;
                SetupIconClickEvent();

                var parentCanvas = skillTreeIconObj.GetComponentInParent<Canvas>();
                if (parentCanvas != null)
                {
                    Log.LogDebug($"[대체 아이콘] 부모 Canvas sortingOrder: {parentCanvas.sortingOrder}");
                }

                skillTreeIconObj.SetActive(true);
                Plugin.Log.LogDebug("아이콘 생성 성공");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"대체 아이콘 생성 실패: {ex.Message}");
            }
        }

        private static void SetupIconSprite(Image image)
        {
            bool iconLoaded = false;

            try
            {
                SafeLoadCustomIcon();
                if (customSkillTreeIcon != null)
                {
                    image.sprite = customSkillTreeIcon;
                    image.color = Color.white;
                    iconLoaded = true;
                    Log.LogDebug("[아이콘] 커스텀 아이콘 적용 성공");
                }
            }
            catch (Exception ex)
            {
                Log.LogDebug($"[아이콘] 커스텀 아이콘 로드 실패: {ex.Message}");
            }

            if (!iconLoaded)
            {
                try
                {
                    if (ObjectDB.instance != null)
                    {
                        var prefab = ObjectDB.instance.GetItemPrefab("SwordIron");
                        if (prefab != null)
                        {
                            var itemDrop = prefab.GetComponent<ItemDrop>();
                            if (itemDrop != null)
                            {
                                var gameIcon = itemDrop.m_itemData.GetIcon();
                                if (gameIcon != null)
                                {
                                    image.sprite = gameIcon;
                                    image.color = Color.white;
                                    iconLoaded = true;
                                    Log.LogDebug("[아이콘] 게임 기본 아이콘(검) 적용 성공");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.LogDebug($"[아이콘] 기본 아이콘 로드 실패: {ex.Message}");
                }
            }

            if (!iconLoaded && image.transform.parent != null)
            {
                var textObj = new GameObject("IconText");
                textObj.transform.SetParent(image.transform.parent, false);
                var text = textObj.AddComponent<UnityEngine.UI.Text>();
                text.text = "스킬\n트리";
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                text.fontSize = 14;
                text.color = Color.white;
                text.alignment = TextAnchor.MiddleCenter;

                var textRect = text.GetComponent<RectTransform>();
                textRect.anchorMin = Vector2.zero;
                textRect.anchorMax = Vector2.one;
                textRect.offsetMin = Vector2.zero;
                textRect.offsetMax = Vector2.zero;
            }
        }

        private static void SetupIconClickEvent()
        {
            var button = skillTreeIconObj.GetComponent<Button>() ?? skillTreeIconObj.AddComponent<Button>();
            button.interactable = true;
            var image = skillTreeIconObj.GetComponent<Image>();
            if (image != null)
            {
                image.raycastTarget = true;
            }
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                Log.LogDebug("[시작 아이콘] 클릭 이벤트 감지됨!");

                if (SkillTreeBGMManager.Instance != null && SkillTreeBGMManager.Instance.IsBGMEnabled)
                {
                    SkillTreeBGMManager.Instance.PlaySkillTreeBGM();
                    Log.LogInfo("[BGM] 스킬트리 BGM 재생 시작 (BGM 활성화 상태)");
                }
                else if (SkillTreeBGMManager.Instance != null)
                {
                    Log.LogInfo("[BGM] BGM이 비활성화되어 있어 재생하지 않음");
                }

                var inv = InventoryGui.instance;
                if (inv == null) { Debug.LogWarning("[스킬트리] InventoryGui.instance를 찾을 수 없음"); return; }
                var canvas = inv.GetComponentInChildren<Canvas>();

                if (skillTreeUI == null || skillTreeUI.panel == null || skillTreeUI.panel.transform.parent != canvas.transform)
                {
                    if (skillTreeUI != null && skillTreeUI.panel != null)
                    {
                        Destroy(skillTreeUI.panel);
                    }
                    var go = new GameObject("SkillTreeUI");
                    go.transform.SetParent(canvas.transform, false);
                    skillTreeUI = go.AddComponent<CaptainSkillTree.Gui.SkillTreeUI>();
                    skillTreeUI.CreateUI(canvas);
                    Plugin.Log.LogInfo("[스킬트리] skillTreeUI 새로 생성 및 Canvas 하위에 배치");
                }

                if (!skillTreeUI.panel.activeSelf)
                {
                    skillTreeUI.RefreshUI();
                    skillTreeUI.panel.SetActive(true);
                }
                else
                {
                    skillTreeUI.panel.SetActive(false);

                    if (SkillTreeBGMManager.Instance != null)
                    {
                        SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
                    }
                }
            });
        }

        #endregion

        #region OnGUI

        private void OnGUI()
        {
            try
            {
                SkillTreeManager.Instance.OnGUIShowMessage();
            }
            catch (Exception ex)
            {
                Log.LogWarning($"OnGUI 메시지 표시 중 오류: {ex.Message}");
            }
        }

        #endregion

        #region BGM System Initialization

        private static void InitializeBGMSystem()
        {
            try
            {
                var bgmManagerGO = new GameObject("SkillTreeBGMManager");
                bgmManagerGO.AddComponent<SkillTreeBGMManager>();
                DontDestroyOnLoad(bgmManagerGO);

                if (SkillTreeBGMManager.Instance != null)
                {
                    SkillTreeBGMManager.Instance.Initialize();
                    Log.LogDebug("[BGM] 스킬트리 BGM 시스템 초기화 완료");
                }
                else
                {
                    Log.LogWarning("[BGM] SkillTreeBGMManager.Instance가 null");
                }
            }
            catch (Exception ex)
            {
                Log.LogError($"[BGM] 초기화 실패: {ex.Message}");
            }
        }

        #endregion

        #region Prefab System Initialization

        private static void InitializePrefabSystem()
        {
            try
            {
                // PrefabRegistry 초기화 (AssetBundle에서 VFX 프리팹 로드)
                Prefab.PrefabRegistry.Initialize();
            }
            catch (System.Exception ex)
            {
                Log.LogError($"[프리팹] 초기화 실패: {ex.Message}");
            }
        }

        #endregion
    }
}

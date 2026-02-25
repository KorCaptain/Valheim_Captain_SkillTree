# 🚫 **핵심 기능 보호 - 수정 금지 구역**

## ⚠️ **중요 경고**
**이 파일에 명시된 코드들은 절대로 수정하면 안 됩니다!**
**커서(AI)는 이 파일을 확인하고 해당 코드들을 건드리지 말아야 합니다.**

---

## 🛡️ **절대 수정 금지 - 핵심 기능들**

### 📱 **1. 탭키 → 인벤토리 아이콘 시스템**

#### **A. Plugin.cs - InventoryGui.Show 패치**
```csharp
[HarmonyPatch(typeof(InventoryGui), "Show")]
public static class InventoryShowPatch
{
    static void Postfix()
    {
        ShowSkillTreeIcon();
    }
}
```
**🚫 절대 수정 금지 사유**: 탭키로 인벤토리 열 때 아이콘 표시하는 핵심 패치

#### **B. Plugin.cs - ShowSkillTreeIcon() 함수**
```csharp
private static void ShowSkillTreeIcon()
{
    // mmo 시스템이 있는지 먼저 확인
    var mmoType = Type.GetType("EpicMMOSystem.MyUI, EpicMMOSystem");
    if (mmoType != null)
    {
        // mmo 방식으로 아이콘 생성 시도
        if (TryCreateMMOStyleIcon())
        {
            Log.LogInfo("[안전 장치] MMO 방식으로 SkillTreeIcon 생성 성공");
            ResetIconAttempts();
            return;
        }
    }
    
    // mmo 방식 실패 시 대체 방법 사용
    Log.LogWarning("[안전 장치] MMO 방식 실패, 독립적 아이콘 생성 방식 사용");
    CreateFallbackSkillTreeIcon();
}
```
**🚫 절대 수정 금지 사유**: 아이콘 생성하는 핵심 로직

---

### 🎯 **2. MMO 스타일 아이콘 생성 시스템**

#### **A. Plugin.cs - TryCreateMMOStyleIcon() 함수**
```csharp
private static bool TryCreateMMOStyleIcon()
{
    try
    {
        // EpicMMOSystem의 navigationPanel 찾기
        var mmoCanvas = GameObject.Find("EpicMMO(Clone)");
        if (mmoCanvas == null) return false;

        var navigationPanel = mmoCanvas.transform.Find("Canvas/MyUI/navigationPanel");
        if (navigationPanel == null) return false;

        var buttonsContainer = navigationPanel.Find("Buttons");
        if (buttonsContainer == null) return false;

        // 기존 SkillTreeButton이 있으면 제거
        var existingButton = buttonsContainer.Find("ButtonSkillTree");
        if (existingButton != null)
        {
            UnityEngine.Object.Destroy(existingButton.gameObject);
        }

        // 기존 ButtonLevelSystem 복제하여 SkillTree 버튼 생성
        var levelSystemButton = buttonsContainer.Find("ButtonLevelSystem");
        if (levelSystemButton == null) return false;

        // ButtonLevelSystem 복제
        var skillTreeButton = UnityEngine.Object.Instantiate(levelSystemButton.gameObject, buttonsContainer);
        skillTreeButton.name = "ButtonSkillTree";

        // 위치 조정 (기존 버튼 오른쪽에 배치)
        var rect = skillTreeButton.GetComponent<RectTransform>();
        var origRect = levelSystemButton.GetComponent<RectTransform>();
        rect.anchoredPosition = origRect.anchoredPosition + new Vector2(80, 0);

        // 아이콘 교체
        var iconImg = skillTreeButton.transform.Find("Icon")?.GetComponent<Image>();
        if (iconImg != null && customSkillTreeIcon != null)
        {
            iconImg.sprite = customSkillTreeIcon;
        }
        else if (iconImg != null && swordIcon != null)
        {
            iconImg.sprite = swordIcon;
        }

        // 버튼 클릭 이벤트 설정
        var button = skillTreeButton.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            var ui = SkillTreeInputListener.Instance?.skillTreeUI;
            if (ui != null)
            {
                ui.ToggleSkillTreeUI();
            }
        });

        skillTreeIconObj = skillTreeButton;
        return true;
    }
    catch (Exception ex)
    {
        Plugin.Log.LogError($"MMO 방식 아이콘 생성 실패: {ex.Message}");
        return false;
    }
}
```
**🚫 절대 수정 금지 사유**: MMO 시스템과 연동하는 핵심 기능

---

### 🎮 **3. 스킬트리 UI 토글 시스템**

#### **A. Gui/SkillTreeInputListener.cs - 전체 파일**
```csharp
using UnityEngine;
using CaptainSkillTree.Gui;

namespace CaptainSkillTree
{
    public class SkillTreeInputListener : MonoBehaviour
    {
        public static SkillTreeInputListener? Instance { get; private set; }
        public SkillTreeUI? skillTreeUI;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        void Update()
        {
            // ESC 키로 스킬트리 닫기
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (skillTreeUI != null && skillTreeUI.panel != null && skillTreeUI.panel.activeInHierarchy)
                {
                    skillTreeUI.panel.SetActive(false);
                }
            }
        }

        public void ToggleSkillTreeUI()
        {
            try
            {
                if (skillTreeUI == null)
                {
                    InitializeSkillTreeUI();
                }

                if (skillTreeUI?.panel != null)
                {
                    bool isActive = skillTreeUI.panel.activeInHierarchy;
                    skillTreeUI.panel.SetActive(!isActive);
                    
                    if (!isActive)
                    {
                        skillTreeUI.RefreshUI();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[SkillTreeInputListener] ToggleSkillTreeUI 오류: {ex.Message}");
            }
        }

        private void InitializeSkillTreeUI()
        {
            try
            {
                var mainCanvas = FindMainCanvas();
                if (mainCanvas != null)
                {
                    skillTreeUI = new SkillTreeUI();
                    skillTreeUI.CreateUI(mainCanvas);
                }
                else
                {
                    Debug.LogError("[SkillTreeInputListener] Main Canvas를 찾을 수 없습니다.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[SkillTreeInputListener] InitializeSkillTreeUI 오류: {ex.Message}");
            }
        }

        private Canvas FindMainCanvas()
        {
            var inventoryGui = InventoryGui.instance;
            if (inventoryGui != null)
            {
                var canvas = inventoryGui.GetComponentInParent<Canvas>();
                if (canvas != null) return canvas;
            }

            var canvases = FindObjectsOfType<Canvas>();
            foreach (var canvas in canvases)
            {
                if (canvas.gameObject.name.Contains("GUI") || canvas.gameObject.name.Contains("Canvas"))
                {
                    return canvas;
                }
            }

            return canvases.Length > 0 ? canvases[0] : null;
        }
    }
}
```
**🚫 절대 수정 금지 사유**: 스킬트리 UI 토글과 ESC키 처리하는 핵심 컴포넌트

---

### 💾 **4. AssetBundle 로딩 시스템**

#### **A. Plugin.cs - GetIconAssetBundle, GetUIAssetBundle, GetCustomIconBundle 함수들**
```csharp
public static AssetBundle GetIconAssetBundle()
{
    if (iconAssetBundle == null)
    {
        var assembly = typeof(Plugin).Assembly;
        var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.skill_node");
        if (stream != null)
        {
            iconAssetBundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
        }
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
        {
            uiAssetBundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
        }
    }
    return uiAssetBundle;
}

public static AssetBundle GetCustomIconBundle()
{
    if (customIconBundle == null)
    {
        var assembly = typeof(Plugin).Assembly;
        var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.captainskilltreeicon");
        if (stream != null)
        {
            customIconBundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
        }
    }
    return customIconBundle;
}
```
**🚫 절대 수정 금지 사유**: AssetBundle에서 리소스 로딩하는 핵심 함수들

---

### 🎯 **5. 스킬 데이터 관리 시스템**

#### **A. SkillTree/SkillTreeData.cs - RegisterAll() 함수**
```csharp
public static void RegisterAll()
{
    var manager = SkillTreeManager.Instance;
    
    // 6개 루트 노드 등록 (절대 수정 금지)
    manager.AddSkill(new SkillNode { Id = "attack_root", Name = "공격 전문가", ... });
    manager.AddSkill(new SkillNode { Id = "ranged_root", Name = "원거리 전문가", ... });
    manager.AddSkill(new SkillNode { Id = "melee_root", Name = "근접 전문가", ... });
    manager.AddSkill(new SkillNode { Id = "speed_root", Name = "속도 전문가", ... });
    manager.AddSkill(new SkillNode { Id = "production_root", Name = "생산 전문가", ... });
    manager.AddSkill(new SkillNode { Id = "defense_root", Name = "방어 전문가", ... });
    
    // 하위 노드들...
}
```
**🚫 절대 수정 금지 사유**: 모든 스킬 노드를 등록하는 핵심 데이터 시스템

---

## 🔐 **추가 보호 규칙**

### **파일별 수정 금지 영역**

1. **Plugin.cs**
   - `InventoryShowPatch` 클래스 전체
   - `ShowSkillTreeIcon()` 함수
   - `TryCreateMMOStyleIcon()` 함수  
   - `CreateFallbackSkillTreeIcon()` 함수
   - AssetBundle 관련 모든 함수들

2. **SkillTreeInputListener.cs**
   - 전체 파일 수정 금지

3. **SkillTreeData.cs**
   - `RegisterAll()` 함수의 6개 루트 노드 등록 부분

4. **SkillTreeManager.cs**
   - `GetSkillLevel()`, `SetSkillLevel()` 함수
   - `InvestSkillPoint()` 함수

---

## ✅ **안전하게 수정 가능한 영역**

1. **새로운 스킬 노드 추가** (기존 것은 건드리지 말고)
2. **UI 스타일 변경** (기본 구조는 유지하면서)
3. **툴팁 내용 수정**
4. **이펙트 추가**
5. **새로운 기능 추가** (기존 기능에 영향 주지 않는 선에서)

---

## 🚨 **위반 시 대응 방법**

만약 실수로 핵심 코드를 수정했다면:

1. **즉시 되돌리기**: Git에서 마지막 정상 버전으로 복구
2. **백업 확인**: `asset/Resources` 폴더의 번들 파일들 온전한지 확인
3. **테스트 실행**: 탭키 → 인벤토리 → 아이콘 표시 정상 작동 확인

---

**⚠️ 이 보호 시스템을 무시하고 핵심 코드를 수정하면 전체 시스템이 작동하지 않을 수 있습니다!**

---

## 📚 **관련 문서**
- **🏠 메인 규칙**: [../CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 구조
- **⚙️ MMO 연동**: [MMO_INTEGRATION_GUIDE.md](MMO_INTEGRATION_GUIDE.md) - 안전한 패치 방식
- **🚨 빌드 오류**: [BUILD_ERRORS_GUIDE.md](BUILD_ERRORS_GUIDE.md) - 문제 해결 방법
- **⚡ 빠른 참조**: [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - 핵심 규칙 요약 
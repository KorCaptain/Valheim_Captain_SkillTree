# 인벤토리 패치 수정 시 필수 체크리스트

> 인벤토리 관련 Harmony 패치를 **추가하거나 수정할 때** 반드시 이 체크리스트를 확인할 것.
> 멈춤 현상의 근본 원인을 정리하고 재발 방지를 위해 작성됨 (2026-03-28).

---

## 🚨 현재 활성 인벤토리 패치 현황 (추가 시 반드시 업데이트)

| 패치 메서드 | 파일 | 패치 클래스 | 발동 시점 |
|------------|------|------------|---------|
| `InventoryGui.DoCrafting` Prefix+Postfix | `CraftingEnhancement.cs` | `CraftingDurability_DoCrafting_Patch` | 제작, 수리 |
| `InventoryGui.DoCrafting` Postfix | `CraftingEnhancement.cs` | `CraftingEnhancement_DoCrafting_Patch` | 제작, 수리 |
| `InventoryGui.DoCrafting` Prefix | `AccurateCraftingDetector.cs` | `InventoryGui_DoCrafting_DirectDetector` | 제작, 수리 |
| `InventoryGui.DoCrafting` Prefix+Postfix | `ProducerCrafting.cs` | `Producer_InventoryGui_DoCrafting_Patch` | 제작, 수리 |
| `InventoryGrid.UpdateGui` Postfix | `ProducerEnchantUI.cs` | `ProducerEnchantUI` | 드래그 중, 상자 열기, 아이템 이동 |
| `InventoryGui.Show` Postfix | `Plugin.Patches.cs` | `InventoryShowIconPositionPatch` | 인벤토리 열기, 상자 열기 |
| `InventoryGui.Show` Postfix | `MMO_System/EpicMMOPanelExtension.cs` | (EpicMMOUIPatch 통해 등록) | UIReload 시 |
| `InventoryGui.Hide` Postfix | `Plugin.Patches.cs` | `InventoryHidePatch` | 인벤토리 닫기 |
| `InventoryGui.Hide` Postfix | `AccurateCraftingDetector.cs` | `InventoryGui_Hide_SafetyReset` | 인벤토리 닫기 |
| `InventoryGui.Awake` Postfix | `AccurateCraftingDetector.cs` | `InventoryGui_CraftButton_Patch` | InventoryGui 초기화 |

---

## ✅ 체크리스트

### 1. 발동 조건 확인

- [ ] **`InventoryGui.DoCrafting` 패치인가?**
  - Valheim에서 **수리(Repair)도 DoCrafting을 호출**함
  - 수리 시 불필요한 인벤토리 순회를 막으려면 Prefix 최상단에 `IsRepairAction()` 추가 필수
  - 예시: `CraftingDurability_DoCrafting_Patch`, `Producer_InventoryGui_DoCrafting_Patch` 참조

- [ ] **`InventoryGui.Show()` Postfix 패치인가?**
  - 상자(Container) 열기에도 발동됨
  - 씬 탐색(`FindObjectOfType`, `Transform.Find()` 재귀) 금지. 결과 캐싱 필수

- [ ] **`InventoryGrid.UpdateGui()` Postfix 패치인가?**
  - 드래그 중 **매 프레임** 호출됨 (0.25s throttle 필수)
  - throttle 없이 `GetAllItems()` 호출 시 드래그 중 반복 freeze 발생

### 2. 성능 확인

- [ ] **`GetAllItems()` 호출이 있는가?**
  - throttle 또는 `IsRepairAction()` early return 으로 제한
  - 수리/드래그 경로에서 호출되지 않도록 주의

- [ ] **`Reflection(GetField/GetMethod)`이 있는가?**
  - `static FieldInfo _xxxField` 캐시 패턴 반드시 사용
  - 패치 메서드 내부에서 `typeof(...).GetField()` 직접 호출 금지

- [ ] **`Transform.Find()` 또는 `GetComponent()`가 반복 호출되는가?**
  - Dictionary 캐시 (`_borderCache` 등) 패턴 사용
  - 루프 내 반복 호출 시 frame drop 원인

- [ ] **`tex.Apply()` 등 GPU 동기화 호출이 있는가?**
  - 패치 메서드 내 직접 호출 금지
  - `Hud.Awake()` Postfix 또는 별도 PreloadXxx() 메서드로 분리
  - 현재 구현: `ProducerEnchantUI.PreloadSprite()` → `HudAwakeHideIconPatch.Postfix()`에서 호출

- [ ] **`Plugin.Log.LogInfo()`가 수리/드래그 경로에 있는가?**
  - 수리 클릭마다 LogInfo가 10번 이상 호출되면 I/O 지연 발생
  - 해당 경로는 `LogDebug`로 변경하거나 `#if DEBUG` 조건 처리

### 3. 패치 중복 누적 확인

- [ ] **동일 메서드에 몇 개의 패치가 있는가? (위 현황 표 참고)**
  - `DoCrafting`: 현재 4개 활성 → 추가 시 신중 검토
  - `UpdateGui`: 현재 1개 → throttle 필수
  - `Show`: 현재 2개 → 각 패치가 중복 작업 하지 않는지 확인

- [ ] **새 패치가 기존 패치와 동일한 작업을 중복 수행하는가?**
  - `GetAllItems()` 중복 호출, 동일 스냅샷 중복 생성 등 피할 것

### 4. 씬 탐색 안전 규칙

- [ ] **`FindObjectOfType()`, `transform.root.GetComponentInChildren()` 등 씬 전체 탐색이 있는가?**
  - 패치 메서드 내 직접 호출 금지
  - 코루틴 + 지연 실행 (`yield return null` x3 이상) 후 캐시에 저장
  - 현재 구현: `EpicMMOPanelExtension.InjectDelayed()` 참조

---

## 🧪 수정 후 필수 테스트 시나리오

마법부여 아이템(`cspt_enchant_type > 0`)을 **5개 이상** 인벤토리에 보유한 상태로 진행:

- [ ] 수리 망치 클릭으로 아이템 수리 → 멈춤 없음
- [ ] 인벤토리에서 아이템 **좌클릭 드래그 2초 이상 유지** → 멈춤 없음
- [ ] 드래그 후 **아이템 드롭(놓기)** → 멈춤 없음
- [ ] **Ctrl+클릭**으로 아이템 이동 → 멈춤 없음
- [ ] **상자(Chest) 열기** → 멈춤 없음
- [ ] 첫 번째 게임 세션 인벤토리 최초 열기 → 멈춤 없음 (GPU 선행 로드 확인)

---

## 🔧 수리 감지 패턴 (재사용 가능)

```csharp
// DoCrafting 패치의 Prefix 최상단에 추가 (Reflection 캐시 포함)
private static FieldInfo _craftTimerField;

private static bool IsRepairAction(InventoryGui gui)
{
    try
    {
        if (_craftTimerField == null)
            _craftTimerField = typeof(InventoryGui).GetField("m_craftTimer",
                BindingFlags.NonPublic | BindingFlags.Instance);
        if (_craftTimerField != null)
            return (float)_craftTimerField.GetValue(gui) < 0f;
    }
    catch { }
    return false;
}

// Prefix에서:
public static void Prefix(InventoryGui __instance, Player player)
{
    if (IsRepairAction(__instance)) return; // ← 이 라인 추가
    // ... 기존 코드
}
```

---

## 📌 알려진 freeze 원인 기록 (해결됨)

| 증상 | 원인 | 해결 방법 | 파일 |
|------|------|---------|------|
| 드래그 중 0.25초마다 freeze | `SetBorder()` → `RefreshBorder()` 매 호출 (Transform.Find + GetComponent) | `IsBorderUpToDate()` 조건으로 구버전만 갱신 | `ProducerEnchantUI.cs` |
| 첫 인벤토리/상자 열기 freeze | `EnsureFields()` 내 `tex.Apply()` GPU 동기화 | `PreloadSprite()`로 분리 → `HudAwake` 시 선행 실행 | `ProducerEnchantUI.cs`, `Plugin.Patches.cs` |
| 수리 클릭 freeze | `DoCrafting` Prefix에서 풀 인벤토리 순회 (수리도 DoCrafting 호출) | `IsRepairAction()` early return | `CraftingEnhancement.cs`, `ProducerCrafting.cs` |

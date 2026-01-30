# 빌드 오류 및 해결 가이드

## Common Build Errors and Solutions

## 🚨 **시작 아이콘 미표시 문제 (Start Icon Not Showing)**

### **문제 증상**
- 모드는 정상 로드되지만 Tab키 → 인벤토리 열 때 스킬트리 아이콘이 나타나지 않음
- MMO 시스템과 다른 기능들은 정상 작동
- 로그에 InventoryShowPatch 관련 메시지가 전혀 없음

### **핵심 원인**
**MMO 패치의 TargetMethod() 오류로 인한 Harmony.PatchAll() 실패**

```
Error: Patching exception in method static System.Reflection.MethodBase 
CaptainSkillTree.SkillTree.SpearExpert_MMO_Agility_Patch::TargetMethod()
```

### **문제 분석**
1. **TargetMethod()가 null 반환** - EpicMMOSystem.LevelSystem.getParameter 메서드를 찾을 수 없음
2. **Harmony.PatchAll() 전체 실패** - 하나의 패치 오류가 전체 패치 등록을 중단
3. **InventoryShowPatch 등록 실패** - 결과적으로 시작 아이콘이 생성되지 않음

### **해결 방법**

#### **1단계: 문제 패치 임시 비활성화**
```csharp
// [HarmonyPatch] // 임시 비활성화 - TargetMethod 오류 방지
public static class SpearExpert_MMO_Agility_Patch
{
    static bool Prepare() 
    {
        var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
        if (levelSystemType == null) 
        {
            Plugin.Log.LogWarning("[MMO 패치] EpicMMOSystem을 찾을 수 없어 창 전문가 MMO 연동 패치를 건너뜁니다");
            return false;
        }
        return true;
    }
    
    static System.Reflection.MethodBase TargetMethod()
    {
        var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
        return levelSystemType?.GetMethod("getParameter", new[] { typeof(object) });
    }
}
```

#### **2단계: Harmony 패치 등록 상태 확인**
```csharp
try
{
    harmony.PatchAll();
    Log.LogError("=== [CRITICAL] Harmony.PatchAll() 성공! ===");
}
catch (System.Exception ex)
{
    Log.LogError($"=== [CRITICAL] Harmony.PatchAll() 실패: {ex.Message} ===");
}
```

#### **3단계: InventoryShowPatch 작동 확인**
```csharp
[HarmonyLib.HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Show))]
public static class InventoryShowPatch
{
    static bool Prepare()
    {
        Plugin.Log.LogError("=== [CRITICAL] InventoryShowPatch Prepare 호출됨! ===");
        return true;
    }
    
    public static void Postfix()
    {
        Log.LogError("=== [CRITICAL] 인벤토리 열림 패치 호출됨! ===");
        ShowSkillTreeIcon();
    }
}
```

### **예방 방법**
1. **MMO 패치에 안전 장치 추가**: TargetMethod()에서 null 체크
2. **Prepare 메서드 활용**: 패치 가능 여부를 사전에 확인
3. **핵심 패치 우선순위**: InventoryShowPatch 같은 핵심 기능을 먼저 등록
4. **단계별 패치 등록**: 문제 발생 시 원인을 쉽게 추적할 수 있도록 분리

### **진단 순서**
1. `Harmony.PatchAll()` 성공/실패 확인
2. `InventoryShowPatch Prepare` 호출 여부 확인  
3. Tab키 → 인벤토리 열 때 `인벤토리 열림 패치 호출됨` 메시지 확인
4. MMO 시스템 관련 패치들의 TargetMethod 오류 점검

---

### Namespace Reference Errors
**Error:** `'CaptainSkillTree.SkillTree.CaptainSkillTree.SkillTree.CaptainSkillTree.SkillTree' 네임스페이스에 'SkillTreeManager' 형식이 없습니다`

**Solution:** Replace with simple reference:
```csharp
// Wrong
CaptainSkillTree.SkillTree.CaptainSkillTree.SkillTree.CaptainSkillTree.SkillTree.SkillTreeManager

// Correct
SkillTreeManager.Instance
```

### ItemData Access Errors
**Error:** `'ItemDrop.ItemData'에는 'm_itemData'에 대한 정의가 포함되어 있지 않습니다`

**Solution:** Use m_shared instead:
```csharp
// Wrong
weapon.m_itemData.m_name
weapon.m_itemData.m_itemType

// Correct
weapon.m_shared.m_name
weapon.m_shared.m_itemType
```

### Protected Member Access Errors
**Error:** `'보호 수준 때문에 'Character.AddStaggerDamage(float, Vector3)'에 액세스할 수 없습니다`

**Solution:** Use public fields and methods:
```csharp
// Wrong
character.AddStaggerDamage(staggerDamage, Vector3.zero);

// Correct
character.m_staggerDamage += staggerDamage;
character.CheckStagger();
```

### Missing Field Errors
**Error:** `'HitData'에는 'm_crit'에 대한 정의가 포함되어 있지 않습니다`

**Solution:** Use alternative logic:
```csharp
// Wrong
if (hit.m_crit)

// Correct
if (hit.m_damage.GetTotalDamage() > 50f) // Critical threshold
```

### Color Constants
**Error:** `'Color'에는 'orange'에 대한 정의가 포함되어 있지 않습니다`

**Solution:** Use Color constructor:
```csharp
// Wrong
Color.orange

// Correct
new Color(1f, 0.5f, 0f)
```

### Unused Variable Warnings
**Warning:** `'변수' 할당되었지만 사용되지 않았습니다`

**Solution:** Add usage or remove:
```csharp
// For variables that should be used
bool showEffect = false;
if (showEffect) Debug.Log("Effect processing");

// For exception variables
catch (Exception) // Remove variable name
```

## Critical System Issues and Solutions

### Harmony Patch Parameter Errors (하모니 패치 매개변수 오류)
**문제 증상:**
```
[Error  :  HarmonyX] Failed to patch virtual void Player::UseEitr(float v): 
System.Exception: Parameter "eitr" not found in method virtual void Player::UseEitr(float v)
```

**근본 원인:**
- 스킬 효과 수정 중 Harmony 패치의 **매개변수 이름을 잘못 사용**
- Valheim 메서드의 실제 매개변수 이름과 패치 코드의 매개변수 이름 불일치
- 이는 **전체 Harmony 패치 시스템**을 불안정하게 만들어 **아이콘 생성 실패** 등 연쇄 문제 발생

**해결 방법:**
```csharp
// ❌ 잘못된 방식 (매개변수 이름 오류)
[HarmonyPatch(typeof(Player), nameof(Player.UseEitr))]
public static void Postfix(Player __instance, float eitr)  // ← 잘못된 이름

// ✅ 올바른 방식 (Valheim 메서드 시그니처에 맞춤)
[HarmonyPatch(typeof(Player), nameof(Player.UseEitr))]
public static void Postfix(Player __instance, float v)     // ← 올바른 이름
{
    if (v < 5f) return;  // 함수 내에서도 v 사용
}
```

**검증 방법:**
1. **로그 확인**: `Parameter "xxx" not found in method` 에러 메시지 확인
2. **Valheim 디컴파일**: 실제 메서드 시그니처 확인
3. **연쇄 영향 확인**: 다른 패치들 (InventoryGui.Show 등)이 정상 작동하는지 확인

**예방 규칙:**
- Harmony 패치 작성 시 항상 **Valheim 소스의 실제 매개변수 이름** 사용
- 임의로 매개변수 이름을 변경하지 말고 **v, __instance** 등 원본 이름 유지
- 패치 에러 발생 시 즉시 수정하여 **전체 시스템 안정성** 확보

**이 문제의 심각성:**
- 하나의 잘못된 패치가 **전체 모드 기능을 마비**시킬 수 있음
- 특히 **아이콘 생성**, **UI 시스템**, **스킬 효과** 등 핵심 기능에 영향
- MMO 시스템과의 연동에도 문제를 일으켜 **호환성 문제** 발생 가능

## Development Environment and Tools
- Visual Studio 2022
- Unity 2022.3.50f1 (provide step-by-step Unity Editor guidance when needed)
- C# based development with MMO folder patterns as reference

## Reference Implementation
The `mmo` folder contains the EpicMMOSystem reference implementation demonstrating proper patterns for:
- Server synchronization
- Data persistence
- GUI architecture
- Level progression systems
- Experience gain mechanisms

Use these patterns as templates when extending or modifying the skill tree functionality.
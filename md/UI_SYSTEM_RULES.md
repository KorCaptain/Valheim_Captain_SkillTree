# UI_SYSTEM_RULES.md - 스킬트리 UI 아이콘 크기 규칙

CaptainSkillTree 모드의 스킬 아이콘 크기 일관성 유지 및 계층 구조 명확화 규칙입니다.

---

## 📚 규칙 목록

- **Rule 10**: 스킬 아이콘 크기 규칙 (Skill Icon Size Rules)

---

## Rule 10: 스킬 아이콘 크기 규칙

### 📋 목적
스킬트리 UI에서 **아이콘 크기 일관성 유지** 및 **계층 구조 명확화**

### 🎯 핵심 원리
**`RectTransform.localScale`을 사용하여 스프라이트 원본 크기 완전 무시**

### 📊 우선순위 판별 순서
```
직업 아이콘 → 무기 전문가 노드 → 일반 스킬 노드 (루트 노드 포함)
```

---

## ⚠️ 기술적 제약 사항

### 문제: Unity Image 컴포넌트는 스프라이트 원본 크기를 존중함

**기술적 이유**:
- AssetBundle 스프라이트는 다양한 원본 해상도를 가짐 (128x128, 256x256, 512x512 등)
- 실제 렌더링 크기 = `(Sprite.rect.size / Sprite.pixelsPerUnit) × CanvasScaler`
- `sizeDelta`만으로는 원본 크기 영향을 완전히 제거 불가능
- `Image.Type.Sliced`는 9-slice Border 설정이 있는 스프라이트에서만 작동

### 해결책: localScale 방식 사용

1. **모든 노드의 sizeDelta를 고정 크기 `100x100`으로 설정**
2. **localScale로 실제 렌더링 크기 제어** (스프라이트 원본 크기 완전 무시)
3. **pixelsPerUnitMultiplier = 1.0f** 설정으로 픽셀 밀도 정규화

---

## 아이콘 크기 사양

### 잠김 상태 (Locked State)

| 타입 | 크기 | sizeDelta | localScale |
|------|------|-----------|------------|
| **직업 아이콘** | 85x85 | (100, 100) | (0.85, 0.85, 1) |
| **무기 전문가 노드** | 42x42 | (100, 100) | (0.42, 0.42, 1) |
| **일반 스킬 노드 + 루트 노드** | 40x40 | (100, 100) | (0.40, 0.40, 1) |

**직업 아이콘**:
- Berserker, Tanker, Rogue, Archer, Mage, Paladin

**무기 전문가 노드**:
- sword, bow, mace, dagger, spear, polearm, crossbow, staff, knife

**일반 스킬 노드 + 루트 노드**:
- all_skill_unlock 스프라이트 사용
- 루트 노드: attack_root, defense_root, production_root, speed_root, melee_root, ranged_root

### 해제 상태 (Unlocked State)

| 타입 | 크기 | sizeDelta | localScale |
|------|------|-----------|------------|
| **직업 아이콘** | 105x105 | (100, 100) | (1.05, 1.05, 1) |
| **무기 전문가 노드** | 50x50 | (100, 100) | (0.50, 0.50, 1) |
| **일반 스킬 노드 + 루트 노드** | 52x52 | (100, 100) | (0.52, 0.52, 1) |

---

## 구현 위치

### SkillTreeNodeUI.cs

#### CreateNodeObjects() - 초기 노드 생성 시
**위치**: Lines 165-182

```csharp
// 노드 아이콘 생성 시 크기 설정
var rect = iconObj.GetComponent<RectTransform>();
rect.sizeDelta = new Vector2(100, 100);  // 고정 기준 크기

// 잠김 상태 크기
if (isJobIcon)
    rect.localScale = new Vector3(0.85f, 0.85f, 1f);  // 직업: 85x85
else if (isWeapon)
    rect.localScale = new Vector3(0.42f, 0.42f, 1f);  // 무기: 42x42
else
    rect.localScale = new Vector3(0.40f, 0.40f, 1f);  // 일반+루트: 40x40

img.pixelsPerUnitMultiplier = 1.0f;  // 픽셀 밀도 무시
```

#### RefreshNodeStates() - 해제 상태 업데이트
**위치**: Lines 283-300

```csharp
// 스킬 해제 시 크기 증가
if (node.IsUnlocked)
{
    if (isJobIcon)
        rect.localScale = new Vector3(1.05f, 1.05f, 1f);  // 직업: 105x105
    else if (isWeapon)
        rect.localScale = new Vector3(0.50f, 0.50f, 1f);  // 무기: 50x50
    else
        rect.localScale = new Vector3(0.52f, 0.52f, 1f);  // 일반+루트: 52x52
}
```

#### RefreshNodeStates() - 잠김 상태 업데이트
**위치**: Lines 323-340

```csharp
// 스킬 잠김 시 크기 복원
if (!node.IsUnlocked)
{
    if (isJobIcon)
        rect.localScale = new Vector3(0.85f, 0.85f, 1f);  // 직업: 85x85
    else if (isWeapon)
        rect.localScale = new Vector3(0.42f, 0.42f, 1f);  // 무기: 42x42
    else
        rect.localScale = new Vector3(0.40f, 0.40f, 1f);  // 일반+루트: 40x40
}
```

### SkillTreeUI.cs

#### RefreshNodeStates() - 포인트 투자 시 전체 노드 갱신
**위치**: Lines 710-734

```csharp
// 스킬 포인트 투자 후 모든 노드 상태 갱신
public void RefreshNodeStates()
{
    foreach (var node in allNodes)
    {
        var rect = nodeObjects[node.Id].GetComponent<RectTransform>();
        bool isJobIcon = IsJobIcon(node);
        bool isWeapon = IsWeaponExpert(node);

        if (node.IsUnlocked)
        {
            // 해제 상태 크기
            if (isJobIcon)
                rect.localScale = new Vector3(1.05f, 1.05f, 1f);
            else if (isWeapon)
                rect.localScale = new Vector3(0.50f, 0.50f, 1f);
            else
                rect.localScale = new Vector3(0.52f, 0.52f, 1f);
        }
        else
        {
            // 잠김 상태 크기
            if (isJobIcon)
                rect.localScale = new Vector3(0.85f, 0.85f, 1f);
            else if (isWeapon)
                rect.localScale = new Vector3(0.42f, 0.42f, 1f);
            else
                rect.localScale = new Vector3(0.40f, 0.40f, 1f);
        }
    }
}
```

---

## 노드 타입 판별 로직

### 무기 전문가 판별

```csharp
string[] weaponNodeIds = { "dagger", "sword", "mace", "spear", "polearm", "crossbow", "bow", "staff" };
bool isWeapon = weaponNodeIds.Any(w => node.Id.Contains(w)) || node.Id.Contains("knife");
```

**판별 대상**:
- sword, bow, mace, dagger, spear, polearm, crossbow, staff, knife

### 직업 아이콘 판별

```csharp
bool isJobIcon = IsJobIcon(node) || node.Id == "Mage" || node.Id == "성기사";

private bool IsJobIcon(SkillNode node)
{
    string[] jobNodeIds = { "Berserker", "Tanker", "Rogue", "Archer", "Paladin" };
    return jobNodeIds.Any(j => node.Id.Contains(j));
}
```

**판별 대상**:
- Berserker, Tanker, Rogue, Archer, Mage, Paladin (성기사)

### 루트 노드는 일반 스킬 노드와 동일

**중요**: 과거에는 `isWeapon || isRoot` 조건으로 루트를 전문가 크기로 처리했으나 **잘못된 분류**였습니다.

**올바른 분류**:
- 루트 노드는 **일반 스킬 노드와 동일한 크기** (40/52)
- attack_root, defense_root, production_root, speed_root, melee_root, ranged_root

---

## 코드 예시

### 기본 패턴

```csharp
// localScale 방식으로 크기 강제 적용 (스프라이트 원본 크기 완전 무시)
rect.sizeDelta = new Vector2(100, 100);  // 모든 노드 고정 기준 크기

// 잠김 상태 크기 설정
if (isJobIconOrForced)
    rect.localScale = new Vector3(0.85f, 0.85f, 1f);  // 직업: 85x85
else if (isWeapon)
    rect.localScale = new Vector3(0.42f, 0.42f, 1f);  // 무기 전문가: 42x42
else
    rect.localScale = new Vector3(0.40f, 0.40f, 1f);  // 일반+루트: 40x40

// 해제 상태 크기 설정
if (isJobIconOrForced)
    rect.localScale = new Vector3(1.05f, 1.05f, 1f);  // 직업: 105x105
else if (isWeapon)
    rect.localScale = new Vector3(0.50f, 0.50f, 1f);  // 무기 전문가: 50x50
else
    rect.localScale = new Vector3(0.52f, 0.52f, 1f);  // 일반+루트: 52x52

// 스프라이트 픽셀 밀도 무시
img.pixelsPerUnitMultiplier = 1.0f;
```

---

## 금지 사항

### ❌ sizeDelta만으로 크기 제어 금지

```csharp
// ❌ 잘못된 예시: sizeDelta만 변경
rect.sizeDelta = new Vector2(42, 42);  // 원본 크기 영향 제거 불가
```

**문제**: 스프라이트 원본 크기의 영향을 완전히 제거할 수 없음

### ❌ SetNativeSize() 호출 금지

```csharp
// ❌ 잘못된 예시: SetNativeSize 호출
img.SetNativeSize();  // 원본 픽셀 크기 강제
rect.sizeDelta = new Vector2(42, 42);  // SetNativeSize가 이를 무시
```

**문제**: 스프라이트 원본 픽셀 크기를 강제하여 크기 불일치 유발

### ❌ Image.Type.Sliced 사용 금지

```csharp
// ❌ 잘못된 예시: Sliced 타입 사용
img.type = Image.Type.Sliced;
```

**문제**: AssetBundle 스프라이트는 9-slice Border 설정이 없어 작동 안 함

### ❌ 루트 노드를 전문가 크기로 처리 금지

```csharp
// ❌ 잘못된 예시: 루트를 전문가 크기로 분류
bool isWeaponOrRoot = isWeapon || isRoot;  // 잘못된 분류
if (isWeaponOrRoot)
    rect.localScale = new Vector3(0.42f, 0.42f, 1f);  // 루트가 42x42이 됨
```

**문제**: 루트 노드는 일반 스킬과 동일한 크기 (40/52)여야 함

### ❌ 직업 아이콘 크기 변경 금지

**필수 크기**:
- 잠김: 85x85
- 해제: 105x105

**금지**: 직업 아이콘 크기를 다른 값으로 변경 금지

### ❌ 전문가와 일반 노드 혼동 금지

**올바른 크기**:
- 무기 전문가: 42x42 (잠김) / 50x50 (해제)
- 일반 + 루트: 40x40 (잠김) / 52x52 (해제)

### ❌ 우선순위 무시 금지

**필수 판별 순서**:
1. 직업 아이콘 판별
2. 무기 전문가 노드 판별
3. 일반 스킬 노드 + 루트 노드 판별

**잘못된 순서는 잘못된 크기 적용을 유발합니다.**

---

## 트러블슈팅 이력

### 시도 1: Image.Type.Sliced 변경
- **결과**: ❌ **실패**
- **이유**: Border 설정 없는 스프라이트에는 효과 없음

### 시도 2: SetNativeSize() 후 sizeDelta 재설정
- **결과**: ❌ **실패**
- **이유**: SetNativeSize()가 원본 크기를 강제하여 sizeDelta 무시

### 시도 3: RefreshNodeStates에 무기/일반 노드 크기 추가
- **결과**: ❌ **실패**
- **이유**: sizeDelta로는 원본 크기 영향 제거 불가

### 시도 4: 루트 노드를 전문가 크기로 분류
- **결과**: ❌ **실패**
- **이유**: 루트는 일반 노드와 동일해야 함

### 시도 5 (최종): localScale 방식
- **결과**: ✅ **성공**
- **이유**: 스프라이트 원본 크기 완전 무시

---

## 체크리스트

UI 아이콘 크기 구현 시 다음을 확인하세요:

### 기본 설정
- [ ] **sizeDelta 고정**: 모든 노드 `100x100` 설정
- [ ] **localScale 사용**: 실제 크기 제어에 localScale 사용
- [ ] **픽셀 밀도 무시**: `pixelsPerUnitMultiplier = 1.0f` 설정

### 노드 타입 판별
- [ ] **직업 아이콘**: 85x85 (잠김) / 105x105 (해제)
- [ ] **무기 전문가**: 42x42 (잠김) / 50x50 (해제)
- [ ] **일반 + 루트**: 40x40 (잠김) / 52x52 (해제)
- [ ] **우선순위 순서**: 직업 → 무기 전문가 → 일반+루트

### 금지 사항 확인
- [ ] **sizeDelta만 사용 금지**: localScale 필수
- [ ] **SetNativeSize 금지**: 원본 크기 강제하지 않음
- [ ] **Sliced 타입 금지**: AssetBundle 스프라이트는 Border 없음
- [ ] **루트 크기 오분류 금지**: 루트는 일반 노드와 동일

### 구현 위치 확인
- [ ] **SkillTreeNodeUI.cs**: CreateNodeObjects, RefreshNodeStates (2곳)
- [ ] **SkillTreeUI.cs**: RefreshNodeStates
- [ ] **일관성**: 모든 위치에서 동일한 크기 적용

---

## 🔗 관련 문서

- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - UI 렌더링 순서 빠른 참조
- [CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 목차
- [CORE_PROTECTION_README.md](CORE_PROTECTION_README.md) - UI 보호 규칙

---

**작성일**: 2025-01-29
**버전**: 1.0
**적용 범위**: Rule 10

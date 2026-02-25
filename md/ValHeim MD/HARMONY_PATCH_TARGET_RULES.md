# Harmony 패치 대상 클래스 규칙

## 핵심 원칙

Harmony 패치의 `typeof()` 대상은 **메서드가 실제 정의된 클래스**를 지정해야 한다.
부모 클래스를 지정하면 패치가 **조용히 실패**한다 (에러 없이 미적용).

---

## Valheim 클래스 계층 구조

```
Character (최상위)
  └── Humanoid (중간)
        └── Player (최하위)
```

### 메서드별 정의 위치

| 메서드 | 정의 클래스 | 올바른 typeof | 잘못된 typeof |
|--------|------------|---------------|---------------|
| `BlockAttack()` | **Humanoid** | `typeof(Humanoid)` | `typeof(Character)` |
| `Stagger()` | **Character** | `typeof(Character)` | - |
| `Damage()` | **Character** | `typeof(Character)` | - |
| `GetBodyArmor()` | **Character** | `typeof(Character)` | - |
| `IsBlocking()` | **Character** | `typeof(Character)` | - |
| `IsStaggering()` | **Character** | `typeof(Character)` | - |
| `SetHealth()` | **Character** | `typeof(Character)` | - |
| `RPC_Damage()` | **Character** | `typeof(Character)` | - |
| `GetCurrentWeapon()` | **Humanoid** | `typeof(Humanoid)` | `typeof(Character)` |
| `StartAttack()` | **Humanoid** | `typeof(Humanoid)` | `typeof(Character)` |
| `EquipItem()` | **Humanoid** | `typeof(Humanoid)` | `typeof(Character)` |

---

## 버그 사례: BlockAttack 패치 미적용

### 증상
- 패링 관련 모든 스킬이 작동하지 않음
- **에러 로그 없음** (조용히 실패)
- 빌드 성공, 로딩 성공, 하지만 패치 미적용

### 원인
```csharp
// BlockAttack()은 Humanoid에 정의됨
// Character에는 BlockAttack() 메서드가 없음

// 잘못된 코드 - 패치 미적용 (에러 없이!)
[HarmonyPatch(typeof(Character), "BlockAttack")]

// 올바른 코드 - 패치 정상 적용
[HarmonyPatch(typeof(Humanoid), "BlockAttack")]
```

### 영향 범위 (실제 발생한 문제)
- **Plugin.Patches.cs**: `SwordSkillTreeParryPatch` 미작동
- **SkillEffect.DefenseTree.cs**: 방어 트리 패링 효과 미작동

### 수정 파일
| 파일 | 라인 | 변경 내용 |
|------|------|----------|
| `Plugin.Patches.cs` | 201 | `typeof(Character)` -> `typeof(Humanoid)` |
| `SkillEffect.DefenseTree.cs` | 76 | `typeof(Character)` -> `typeof(Humanoid)` |

---

## 확인 방법

### 1. 빌드 시 확인 불가
- `typeof(Character)`를 써도 빌드 에러 없음
- Harmony가 런타임에 메서드를 못 찾으면 조용히 스킵

### 2. 런타임 확인
```csharp
// 디버그 로그로 패치 적용 확인
Plugin.Log.LogInfo("[패치 확인] BlockAttack Postfix 진입");
```

### 3. Valheim API 확인
```
valheim_all_api.md 에서 메서드의 정의 클래스 검색
또는 dnSpy로 assembly_valheim.dll 디컴파일
```

---

## 체크리스트

새 Harmony 패치 작성 시:

- [ ] 대상 메서드가 **어느 클래스에 정의**되었는지 확인
- [ ] `typeof()`에 **정의 클래스** 지정 (부모 클래스 아님)
- [ ] 런타임에 패치 진입 로그 확인
- [ ] 특히 `Humanoid` 메서드에 `typeof(Character)` 쓰지 않았는지 확인

---

**작성일**: 2026-02-06
**관련 파일**: Plugin.Patches.cs, SkillEffect.DefenseTree.cs

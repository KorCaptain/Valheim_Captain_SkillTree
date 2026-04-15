# Job Lv2 — 30초 추가 사용 창 시스템 참조 문서

> 작성일: 2026-03-20
> 대상: 직업 Lv2 "30초 추가 사용 창" 패턴을 구현한 아처·메이지 + 향후 추가 직업

---

## 1. 개요

직업 레벨 2 달성 시 특정 스킬이 **두 번 연속 사용 가능**해지는 시스템.

- **기본 동작**: 스킬 1회 사용 → 쿨타임 즉시 시작
- **Lv2 동작**: 스킬 1회 사용 → 쿨타임 **보류** + 30초 추가 사용 창 오픈 → 창 안에서 2번째 사용 시 쿨타임 시작 (또는 30초 경과 시 자동 쿨타임 시작)

### 적용 직업 현황

| 직업 | 스킬 | 상태 |
|------|------|------|
| 아처 (Archer) | 폭발화살 (ExplosiveArrow) | ✅ 완료 |
| 메이지 (Mage) | 연속발사 (StaffDualCast) | ✅ 완료 |
| 기타 직업 | — | 미구현 (향후 추가 가능) |

---

## 2. 시스템 동작 원리

### 공통 플로우

```
[R키 입력]
    │
    ├─ Lv2 미보유: 스킬 실행 → 쿨타임 즉시 시작 (기존 동작)
    │
    └─ Lv2 보유:
         │
         ├─ 창이 열려 있음 (1번째 사용 후 30초 이내)?
         │    ├─ YES: 2번째 사용 → 창 종료 → 쿨타임 시작
         │    └─ NO : 1번째 사용 → 쿨타임 보류 → 30초 창 시작 → 만료 코루틴 시작
         │
         └─ [만료 코루틴]
              └─ 30초 경과 후 창이 아직 열려 있음? → 쿨타임 시작
```

### 쿨타임 체크 우선순위

```
R키 입력
  → 창이 열려 있으면(inPendingWindow == true) → 쿨타임 체크 건너뜀
  → 창이 닫혀 있으면 → 일반 쿨타임 체크 진행
```

---

## 3. 직업별 구현 현황

| 직업 | 스킬 | 키 | 구현 파일 | 상태 |
|------|------|-----|-----------|------|
| 아처 | 폭발화살 | R키 | `SkillTree/SkillEffect.ExplosiveArrow.cs` | ✅ 완료 |
| 메이지 | 연속발사 | R키 | `SkillTree/SkillEffect.StaffDualCast.cs` | ✅ 완료 |

---

## 4. 코드 패턴 (구현 템플릿)

아래 패턴을 복사·수정하여 새 직업 Lv2에 적용한다.

### 4-1. 정적 필드 선언

```csharp
// 직업 Lv2: 30초 추가 사용 창 관리
private static Dictionary<Player, float> _[스킬명]PendingWindow = new Dictionary<Player, float>();
private const float [스킬명]ExtraWindow = 30f;
```

### 4-2. 쿨타임 체크 수정

기존 쿨타임 체크 **앞에** 창 여부 확인 로직 삽입:

```csharp
// [직업명] Lv2 추가 사용 창이 열려 있으면 쿨타임 체크 건너뜀
bool inPendingWindow = _[스킬명]PendingWindow.TryGetValue(player, out float windowEnd)
    && Time.time <= windowEnd;

if (!inPendingWindow)
{
    // 기존 쿨타임 체크 로직
    if (/* 쿨타임 중 */)
    {
        // 쿨타임 메시지 표시
        return;
    }
}
```

### 4-3. 쿨타임 시작 분기

스킬 실행 성공 후 Lv2 여부에 따라 분기:

```csharp
bool has[직업명]Lv2 = SkillTreeManager.Instance != null
    && SkillTreeManager.Instance.GetSkillLevel("[직업명]") >= 2;

if (has[직업명]Lv2)
{
    if (_[스킬명]PendingWindow.TryGetValue(player, out float we) && Time.time <= we)
    {
        // 2번째 사용: 창 종료 → 쿨타임 시작
        _[스킬명]PendingWindow.Remove(player);
        // 쿨타임 설정 (※ 방식은 직업마다 다름 — 5절 참고)
        ActiveSkillCooldownRegistry.SetCooldown("R", Config.[쿨타임값]);
        Plugin.Log.LogDebug("[스킬명] [직업명] Lv2 2번째 사용 → 쿨타임 시작");
    }
    else
    {
        // 1번째 사용: 쿨타임 보류, 30초 창 시작
        _[스킬명]PendingWindow[player] = Time.time + [스킬명]ExtraWindow;
        [Instance].StartCoroutine(Expire[스킬명]Window(player));
        Plugin.Log.LogDebug("[스킬명] [직업명] Lv2 1번째 사용 → 30초 창 시작");
    }
}
else
{
    // Lv2 미보유: 즉시 쿨타임 시작
    // 쿨타임 설정 (※ 방식은 직업마다 다름 — 5절 참고)
    ActiveSkillCooldownRegistry.SetCooldown("R", Config.[쿨타임값]);
}
```

### 4-4. 만료 코루틴

```csharp
private static IEnumerator Expire[스킬명]Window(Player player)
{
    yield return new WaitForSeconds([스킬명]ExtraWindow);
    if (_[스킬명]PendingWindow.ContainsKey(player))
    {
        // 30초 내 2번째 사용 없음 → 쿨타임 시작
        _[스킬명]PendingWindow.Remove(player);
        // 쿨타임 설정 (※ 방식은 직업마다 다름 — 5절 참고)
        ActiveSkillCooldownRegistry.SetCooldown("R", Config.[쿨타임값]);
        Plugin.Log.LogDebug("[스킬명] [직업명] Lv2 창 만료 → 쿨타임 시작");
    }
}
```

### 4-5. CleanupOnDeath 패턴

```csharp
public static void Cleanup[스킬명]OnDeath(Player player)
{
    try
    {
        // 기존 정리 코드...
        _[스킬명]PendingWindow.Remove(player);
        // 추가 정리 코드...
    }
    catch (Exception ex)
    {
        Plugin.Log.LogWarning($"[스킬명] 정리 실패: {ex.Message}");
    }
}
```

---

## 5. 직업별 핵심 차이점 비교

| 항목 | 아처 (폭발화살) | 메이지 (연속발사) |
|------|----------------|-----------------|
| **구현 파일** | `SkillEffect.ExplosiveArrow.cs` | `SkillEffect.StaffDualCast.cs` |
| **StartCoroutine 인스턴스** | `Plugin.Instance` | `SkillTreeInputListener.Instance` |
| **쿨타임 저장 방식** | 기준시간 (`= Time.time`) | 만료시간 (`= Time.time + CooldownValue`) |
| **쿨타임 체크 방식** | `Time.time - cooldown[player] < CooldownValue` | `Time.time < cooldowns[player]` |
| **창 필드명** | `_explosiveArrowPendingWindow` | `_staffDualCastPendingWindow` |
| **상수명** | `ExplosiveArrowExtraWindow` | `StaffDualCastExtraWindow` |
| **using 추가 필요** | `using System.Collections;` (기존 포함) | `using System.Collections;` (기존 포함) |

### StartCoroutine 선택 기준

- `Plugin.Instance`: 스킬 효과 파일에서 직접 코루틴 실행 시 사용 (SkillEffect.*)
- `SkillTreeInputListener.Instance`: 입력 리스너에서 코루틴 실행 시 사용

> **주의**: `SkillTreeInputListener.cs`는 수정 금지 파일이지만,
> 해당 인스턴스를 통한 `StartCoroutine` 호출은 허용됨.

### 쿨타임 저장 방식 비교

```csharp
// 아처 방식 (기준시간 저장)
cooldown[player] = Time.time;  // "언제 시작했나"
// 체크: Time.time - cooldown[player] < CooldownValue

// 메이지 방식 (만료시간 저장)
cooldowns[player] = Time.time + CooldownValue;  // "언제 끝나나"
// 체크: Time.time < cooldowns[player]
```

두 방식 모두 정상 동작하지만, **같은 스킬 파일 내에서는 일관성 유지** 필수.

---

## 6. 실제 구현 코드 발췌

### 아처 — 1번째/2번째 분기 (SkillEffect.ExplosiveArrow.cs:97)

```csharp
if (hasArcherLv2)
{
    if (_explosiveArrowPendingWindow.TryGetValue(player, out float we) && Time.time <= we)
    {
        // 2번째 사용: 창 종료 → 쿨타임 시작
        _explosiveArrowPendingWindow.Remove(player);
        explosiveArrowCooldown[player] = Time.time;
        ActiveSkillCooldownRegistry.SetCooldown("R", SkillTreeConfig.BowExplosiveArrowCooldownValue);
        Plugin.Log.LogDebug("[폭발화살] Archer Lv2 2번째 사용 → 쿨타임 시작");
    }
    else
    {
        // 1번째 사용: 쿨타임 보류, 30초 창 시작
        _explosiveArrowPendingWindow[player] = Time.time + ExplosiveArrowExtraWindow;
        Plugin.Instance.StartCoroutine(ExpireExplosiveArrowWindow(player));
        Plugin.Log.LogDebug("[폭발화살] Archer Lv2 1번째 사용 → 30초 창 시작");
    }
}
```

### 메이지 — 1번째/2번째 분기 (SkillEffect.StaffDualCast.cs:77)

```csharp
if (hasMageLv2)
{
    if (_staffDualCastPendingWindow.TryGetValue(player, out float we) && Time.time <= we)
    {
        // 2번째 사용: 창 종료 → 쿨타임 시작
        _staffDualCastPendingWindow.Remove(player);
        staffDualExplosionCooldowns[player] = Time.time + Staff_Config.StaffDoubleCastCooldownValue;
        ActiveSkillCooldownRegistry.SetCooldown("R", Staff_Config.StaffDoubleCastCooldownValue);
        Plugin.Log.LogDebug("[연속발사] Mage Lv2 2번째 사용 → 쿨타임 시작");
    }
    else
    {
        // 1번째 사용: 쿨타임 보류, 30초 창 시작
        _staffDualCastPendingWindow[player] = Time.time + StaffDualCastExtraWindow;
        SkillTreeInputListener.Instance.StartCoroutine(ExpireStaffDualCastWindow(player));
        Plugin.Log.LogDebug("[연속발사] Mage Lv2 1번째 사용 → 30초 창 시작");
    }
}
```

---

## 7. 향후 추가 직업 구현 가이드

### 구현 체크리스트

- [ ] 정적 필드 2개 추가: `_[스킬명]PendingWindow`, `[스킬명]ExtraWindow = 30f`
- [ ] 쿨타임 체크 앞에 `inPendingWindow` 로직 삽입
- [ ] 스킬 실행부에 Lv2 분기 (1번째/2번째 사용 처리)
- [ ] 만료 코루틴 `Expire[스킬명]Window` 구현
- [ ] `CleanupOnDeath`에 `_[스킬명]PendingWindow.Remove(player)` 추가
- [ ] 직업 레벨 체크: `SkillTreeManager.Instance.GetSkillLevel("[직업명]") >= 2`

### 주의사항

1. **창 열림 여부 체크가 쿨타임 체크보다 먼저 와야 한다.**
   순서 바꾸면 창이 열려 있어도 쿨타임에 막힘.

2. **만료 코루틴은 `_PendingWindow.ContainsKey(player)` 를 재확인 후 쿨타임 시작.**
   2번째 사용으로 이미 창이 닫혔다면 코루틴이 중복으로 쿨타임 시작하는 것을 방지.

3. **`CleanupOnDeath`에 반드시 추가.**
   사망 후 창 상태가 남아 있으면 재접속 시 쿨타임 없이 바로 사용 가능해지는 버그 발생.

4. **쿨타임 저장 방식은 기존 파일의 방식을 따른다.**
   아처 방식(기준시간)을 쓰는 파일에는 아처 방식으로, 메이지 방식(만료시간)을 쓰는 파일에는 메이지 방식으로.

---

## 참고 파일

| 파일 | 용도 |
|------|------|
| `SkillTree/SkillEffect.ExplosiveArrow.cs` | 아처 Lv2 구현 원본 |
| `SkillTree/SkillEffect.StaffDualCast.cs` | 메이지 Lv2 구현 원본 |
| `md/Job_Skill_LevelUP.md` | 직업 레벨업 구조 (레벨업 흐름, 재료, 애니메이션) |
| `md/ACTIVE_SKILL_SYSTEM.md` | 액티브 스킬 키 바인딩 전체 규칙 |

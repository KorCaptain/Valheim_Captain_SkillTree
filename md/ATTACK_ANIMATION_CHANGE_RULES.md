# 공격 모션(Animation) 변경 규칙

발헤임에서 공격 애니메이션을 다른 무기 모션으로 교체하는 방법과 주의사항.

---

## 핵심 사실: Valheim은 Attack을 Clone()한다

```csharp
// Humanoid.StartAttack() 내부 (발헤임 소스)
Attack attack = currentWeapon.m_shared.m_attack.Clone(); // ← 새 객체 생성!
attack.Start(this, body, zanim, ...);
```

**`Attack.Start()`에 전달되는 `__instance`는 원본이 아닌 Clone 객체다.**  
따라서 아래 비교는 항상 실패한다:

```csharp
// ❌ 절대 사용 금지 — 항상 true(불일치)로 조건 통과 안 됨
if (weapon.m_shared.m_attack != __instance) return;
```

---

## 무기별 모션 유형 — 단일 vs 체인(연속)

Valheim 공격 모션은 `m_attackChainLevels`와 `m_attackRandomAnimations` 값에 따라 세 가지로 구분된다.

| 유형 | 조건 | 예시 무기 | 트리거 이름 |
|------|------|-----------|------------|
| **단일 모션** | `m_attackChainLevels ≤ 1` & `m_attackRandomAnimations < 2` | 도끼, 창 2차(투척), 방패 | `"axe_secondary"` |
| **체인(연속) 모션** | `m_attackChainLevels > 1` | **단검(3단), 검(2~3단)** | `"knife_stab0"`, `"knife_stab1"`, `"knife_stab2"` |
| **랜덤 모션** | `m_attackRandomAnimations ≥ 2` | 일부 망치/도끼 | `"swing0"`, `"swing1"` (랜덤 선택) |

### Attack.Start 내부 트리거 결정 로직

```csharp
if (m_attackChainLevels > 1)
{
    // previousAttack.m_attackAnimation == this.m_attackAnimation 이면 체인 이어서 진행
    // timeSinceLastAttack 초과 또는 마지막 단계이면 0으로 리셋
    zanim.SetTrigger(m_attackAnimation + currentChainLevel); // "knife_stab0", "knife_stab1", ...
}
else if (m_attackRandomAnimations >= 2)
{
    zanim.SetTrigger(m_attackAnimation + Random.Range(0, m_attackRandomAnimations));
}
else
{
    zanim.SetTrigger(m_attackAnimation); // suffix 없음
}
```

### 모션 교체 시 유형 확인 필수

다른 무기의 모션으로 교체할 때, **대상 무기의 유형을 먼저 확인**해야 한다:

```csharp
var knifeAttack = GetCachedKnifePrimaryAttack();

// 단검: m_attackChainLevels = 3 (체인 모션)
// → 교체 시 단순히 m_attackAnimation만 바꾸면 체인 전체가 재생되지 않는다
```

---

## 체인 모션 교체 시 핵심 문제: Postfix 복원이 체인 추적을 깨뜨린다

체인 모션을 교체할 때 `m_attackChainLevels = 3`으로 단순히 설정하면 **첫 번째 모션만 반복**된다.

### 왜 m_attackChainLevels만 바꾸면 안 되는가

Attack.Start 내부에서 체인 이어가기 조건:
```csharp
if (previousAttack.m_attackAnimation == this.m_attackAnimation)
    currentChainLevel = previousAttack.m_nextAttackChainLevel; // 이어서 증가
```

**Postfix에서 `m_attackAnimation`을 원복하면:**
- 이번 공격: `this.m_attackAnimation = "knife_stab"` (Prefix에서 교체)
- 다음 공격의 `previousAttack.m_attackAnimation = "spear"` (Postfix에서 복원됨)
- 비교: `"spear" != "knife_stab"` → 체인 매번 0으로 리셋 → 첫 번째 모션만 반복

### 해결: 수동 체인 카운터 패턴

Postfix 복원 문제를 우회하기 위해 패치 내부에서 체인 레벨을 직접 추적한다.  
suffix를 미리 계산하여 `m_attackAnimation`에 포함시키고, `m_attackChainLevels = 0`으로 유지해 Valheim이 중복 suffix를 붙이지 않도록 한다.

```
공격 1회차: trigger = "knife_stab0", m_attackChainLevels = 0
공격 2회차: trigger = "knife_stab1", m_attackChainLevels = 0   (2초 이내 연속)
공격 3회차: trigger = "knife_stab2", m_attackChainLevels = 0
공격 4회차: trigger = "knife_stab0", m_attackChainLevels = 0   (순환)
```

---

## 모션 변경 방법 비교

| 방법 | 사용 시점 | 장점 | 단점 |
|------|----------|------|------|
| **Flag + 수동 체인 카운터** (권장) | 체인 모션 교체 | 모든 체인 모션 순환, 정확한 1/2차 구분 | 패치 2개 + 카운터 관리 필요 |
| **Flag 패턴 (단일 모션)** | 단일 모션 교체 | 간단 | 체인 모션 무기엔 첫 번째만 재생됨 |
| **zanim.SetTrigger 직접 호출** | 코루틴 내 특수 동작(도약, 대시 등) | 즉시 적용, 간단 | 일반 공격 흐름과 분리된 경우만 가능 |

---

## 방법 1-A: Flag + 수동 체인 카운터 (권장 — 체인 모션 교체)

### 구조

```
[Humanoid.StartAttack Prefix]
  !secondaryAttack && 조건 충족 → s_pending.Add(player)
         ↓ (내부에서 attack.Clone().Start() 호출)
[Attack.Start Prefix]
  s_pending.Remove(player) 성공
  → s_chainState에서 현재 레벨 읽기
  → trigger = baseName + level (예: "knife_stab1")
  → s_chainState 업데이트 (level+1, Time.time)
  → m_attackAnimation = trigger, m_attackChainLevels = 0
[Attack.Start Postfix]
  m_attackAnimation 원복
```

### 구현 예시 (꿰뚫는 창 패시브 — 단검 3단 체인)

**① 정적 필드 (MeleeSkillPatches.cs — SpearPenetrate_KnifeAnim_Patch 내부)**
```csharp
// 체인 공격 레벨 추적: (현재 레벨, 마지막 공격 시각)
static readonly Dictionary<Player, (int level, float lastTime)> s_knifeChainState =
    new Dictionary<Player, (int level, float lastTime)>();
private const float KnifeChainResetSeconds = 2.0f; // 2초 무공격 시 체인 리셋
```

**② Humanoid.StartAttack Prefix — 플래그 설정**
```csharp
public static void Prefix(Humanoid __instance, bool secondaryAttack)
{
    if (secondaryAttack) return;                          // 2차 공격(투창 등) 제외
    if (__instance is not Player player) return;
    if (!SkillEffect.IsUsingSpear(player)) return;
    if (!SkillEffect.HasSkill("spear_Step5_penetrate")) return;
    SkillEffect.s_spearKnifeAnimPending.Add(player);
}
```

**③ Attack.Start Prefix/Postfix — 체인 모션 교체**
```csharp
static void Prefix(Attack __instance, Humanoid character, out State __state)
{
    __state = null;
    if (character is not Player player) return;
    if (!SkillEffect.s_spearKnifeAnimPending.Remove(player)) return;

    var knifeAttack = SkillEffect.GetCachedKnifePrimaryAttack();
    if (knifeAttack == null) return;

    string baseName = knifeAttack.m_attackAnimation;
    int chainMax = knifeAttack.m_attackChainLevels > 1
        ? knifeAttack.m_attackChainLevels
        : (knifeAttack.m_attackRandomAnimations >= 2 ? knifeAttack.m_attackRandomAnimations : 1);

    string trigger;
    if (chainMax > 1)
    {
        s_knifeChainState.TryGetValue(player, out var prev);
        // 2초 이상 경과 시 체인 리셋
        int level = (UnityEngine.Time.time - prev.lastTime > KnifeChainResetSeconds) ? 0 : prev.level;
        trigger = baseName + level;
        s_knifeChainState[player] = ((level + 1) % chainMax, UnityEngine.Time.time);
    }
    else
    {
        trigger = baseName; // 단일 모션
    }

    __state = new State
    {
        anim        = __instance.m_attackAnimation,
        chainLevels = __instance.m_attackChainLevels,
        randomAnims = __instance.m_attackRandomAnimations
    };
    __instance.m_attackAnimation        = trigger; // suffix 포함된 완성 이름
    __instance.m_attackChainLevels      = 0;       // Valheim이 suffix 추가로 붙이지 않도록
    __instance.m_attackRandomAnimations = 0;
}

static void Postfix(Attack __instance, State __state)
{
    if (__state == null) return;
    __instance.m_attackAnimation        = __state.anim;
    __instance.m_attackChainLevels      = __state.chainLevels;
    __instance.m_attackRandomAnimations = __state.randomAnims;
}
```

---

## 방법 1-B: Flag 패턴 단순형 (단일 모션 교체)

대상 무기가 단일 모션(`m_attackChainLevels ≤ 1`)인 경우만 사용 가능.

```csharp
// Prefix에서 트리거 이름 직접 계산
var trigger = SkillEffect.GetKnifePrimaryAnimTrigger();
// GetKnifePrimaryAnimTrigger()는 단일 모션이면 그대로, 체인이면 "0" suffix 고정
```

> **주의**: 이 방법으로 체인 모션 무기를 교체하면 항상 첫 번째 모션만 재생된다.

---

## 방법 2: zanim.SetTrigger 직접 호출 (코루틴 내 특수 동작)

일반 공격 흐름 밖(코루틴, 대시, 도약 등)에서 시각 모션만 교체할 때 사용.

```csharp
// 분노의 망치(FuryHammer) — 도약 구간 단검 모션
var knifeSecondary = GetCachedKnifeSecondaryAttack();
var zanim = player.GetComponentInChildren<ZSyncAnimation>();
zanim.SetTrigger(knifeSecondary.m_attackAnimation);
```

> **주의**: 코루틴 내에서만 사용. 일반 공격 중 SetTrigger를 두 번 호출하면  
> 애니메이터에 트리거가 중복 설정되어 먼저 설정된 트리거(기존 무기)가 우선 실행된다.

---

## 애니메이션 트리거 이름 계산

`Attack.m_attackAnimation`은 **기본 이름**이고, 실제 트리거는 suffix가 붙을 수 있다:

```csharp
// 단일 모션: suffix 없음
zanim.SetTrigger("spear_attack");

// 체인 모션: 0, 1, 2 ...
zanim.SetTrigger("knife_stab0");
zanim.SetTrigger("knife_stab1");
zanim.SetTrigger("knife_stab2");

// 랜덤 모션: 랜덤 번호
zanim.SetTrigger("swing" + Random.Range(0, m_attackRandomAnimations));
```

캐시에서 단일 트리거 이름 계산 시 (단일/첫 번째 모션만 필요한 경우):
```csharp
string GetAnimTrigger(Attack attack)
{
    if (attack.m_attackChainLevels > 1 || attack.m_attackRandomAnimations >= 2)
        return attack.m_attackAnimation + "0"; // 첫 번째 트리거만
    return attack.m_attackAnimation;
}
```

---

## 무기별 캐시 메서드 패턴

```csharp
// Primary Attack (좌클릭): m_attack
var attack = prefab.GetComponent<ItemDrop>()?.m_itemData?.m_shared?.m_attack;

// Secondary Attack (우클릭): m_secondaryAttack
var attack = prefab.GetComponent<ItemDrop>()?.m_itemData?.m_shared?.m_secondaryAttack;
```

| 캐시 메서드 | 위치 | 무기 유형 | 용도 |
|------------|------|-----------|------|
| `GetCachedKnifePrimaryAttack()` | `SkillEffect.SwordSpearSkillEffects.cs` | 단검 (체인 3단) | 창 패시브 단검 모션 |
| `GetCachedKnifeSecondaryAttack()` | `MaceSkills.FuryHammer.cs` | 단검 2차 (단일) | 분노의 망치 도약 모션 |
| `GetCachedSledgeIronAttack()` | `MaceSkills.FuryHammer.cs` | 철 망치 (단일) | 분노의 망치 착지 모션 |

---

## 절대 금지

```csharp
// ❌ Clone 때문에 항상 실패
if (weapon.m_shared.m_attack != __instance) return;
if (weapon.m_shared.m_attack == __instance) { ... }

// ❌ 체인 모션 무기를 단순 Flag 패턴으로 교체 (첫 번째 모션만 반복됨)
// → 수동 체인 카운터(s_knifeChainState) 패턴 사용할 것

// ❌ m_attackChainLevels를 대상 무기 값으로 설정 (Postfix 복원으로 체인 추적 불가)
__instance.m_attackChainLevels = knifeAttack.m_attackChainLevels; // 작동 안 함

// ❌ 일반 공격 중 이중 SetTrigger (기존 무기 모션이 우선 실행됨)
// Humanoid.StartAttack Postfix에서 zanim.SetTrigger() 호출

// ❌ 코루틴 도약 중 StartAttack() 호출 (현재 무기 모션으로 오염)
player.StartAttack(null, false);
```

---

## 관련 파일

| 파일 | 역할 |
|------|------|
| `SkillTree/SkillEffect.SwordSpearSkillEffects.cs` | 창 패시브 캐시 + 플래그 필드 |
| `SkillTree/SkillEffect.MeleeSkillPatches.cs` | `SpearPenetrate_KnifeAnim_Patch` (수동 체인 카운터 패턴 구현) |
| `SkillTree/MaceSkills.FuryHammer.cs` | `GetCachedKnifeSecondaryAttack()` (직접 SetTrigger 패턴) |
| `rules/FURY_HAMMER_MOTION_RULES.md` | 분노의 망치 모션 규칙 (직접 SetTrigger 금지 사항) |
| `valheim_dll_api/assembly_valheim/Attack.cs` | Attack.Start 구현 (zanim.SetTrigger 호출 위치, 체인 로직) |
| `valheim_dll_api/assembly_valheim/Humanoid.cs` | StartAttack → attack.Clone().Start() 확인 |

---

**작성일**: 2026-05-16  
**최종 수정**: 2026-05-16 — 체인 모션 유형 분류, Postfix 복원 문제, 수동 체인 카운터 패턴 추가  
**트리거**: 꿰뚫는 창 단검 모션 패시브 구현 — 체인 3단 모션 중 첫 번째만 재생되던 버그 수정

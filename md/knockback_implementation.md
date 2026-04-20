# Knockback 구현 노트

## 핵심: Valheim 이동 시스템 구조

### CustomFixedUpdate 흐름
```
Character.CustomFixedUpdate(float dt)
  → m_currentVel = Lerp(m_currentVel, desired_movement, acceleration)
  → AddPushbackForce(ref m_currentVel)   ← m_pushForce 적용 지점
  → vector2 = m_currentVel - m_body.velocity
  → m_body.AddForce(vector2, VelocityChange)
```

- Valheim 캐릭터는 `FixedUpdate`가 아닌 **`CustomFixedUpdate(float dt)`** 사용
- `m_body.velocity`를 직접 set하면 다음 `CustomFixedUpdate`가 덮어씀 → 코루틴 방식 무효

---

## m_pushForce 메커니즘 (핵심 API)

### 관련 필드/메서드 (assembly_valheim/Character.cs)

```csharp
public Vector3 m_pushForce = Vector3.zero;          // public 필드, 직접 접근 가능
public const float m_pushForcedissipation = 100f;   // 초당 감소량

public void AddPushbackForce(ref Vector3 velocity)
{
    if (m_pushForce != Vector3.zero)
    {
        Vector3 normalized = m_pushForce.normalized;
        float num = Vector3.Dot(normalized, velocity);
        if (num < 20f)
            velocity += normalized * (20f - num); // push 방향 속도를 20m/s로 보장
    }
}

public void UpdatePushback(float dt)
{
    m_pushForce = Vector3.MoveTowards(m_pushForce, Vector3.zero, 100f * dt); // 100/s 감소
}
```

### 동작 원리
| 항목 | 값 |
|------|-----|
| 적용 시점 | `CustomFixedUpdate` 내 `AddPushbackForce` 호출 시 |
| velocity 보장 | push 방향으로 최소 20 m/s |
| 감소 속도 | 100f/s (MoveTowards) |
| 지속 시간 | `magnitude / 100f` 초 |
| 이동 거리 | ≈ `20 × duration` m (마찰 감안 약 70~80%) |

### 거리 계산 예시
- `m_pushForce.magnitude = 45` → 0.45초 → 20 m/s × 0.45s ≈ **9m** (실제 약 7m)
- `m_pushForce.magnitude = 35` → 0.35초 → 20 m/s × 0.35s ≈ **7m** (실제 약 5m)

---

## ApplyPushback vs 직접 설정

### ApplyPushback 공식 경로 (권장 안함)
```csharp
public void ApplyPushback(Vector3 dir, float pushForce)
{
    float num = pushForce * Clamp01(1 + GetEquipmentMovementModifier()) / m_body.mass * 2.5f;
    dir.y = 0f;      // ← Y 강제 0 (수직 성분 없음)
    dir.Normalize();
    Vector3 vector = dir * num;
    if (m_pushForce.magnitude < vector.magnitude)
        m_pushForce = vector;
}
```
- **문제**: Y 성분 강제 0, mass 변수에 따라 크기 달라짐

### 직접 설정 (사용 중인 방식)
```csharp
// knockDir에 Y 성분 포함 가능, 크기 직접 제어
if (__instance.m_pushForce.magnitude < forceMagnitude)
    __instance.m_pushForce = knockDir * forceMagnitude;
```
- Y 성분 포함 가능 (입체 넉백)
- 크기 예측 가능

---

## 현재 구현 (Knockback.cs)

```csharp
private const float KnockbackStaggerForce = 500f;
private const float KnockbackPushForce = 45f; // 0.45s × 20m/s ≈ 7~9m

// RPC_Damage Prefix에서:
hit.m_pushForce = KnockbackStaggerForce * attackRatio; // 스태거 애니메이션 트리거
knockDir.y = 0.3f;  // 약간의 상향 성분
knockDir.Normalize();
if (__instance.m_pushForce.magnitude < forceMagnitude)
    __instance.m_pushForce = knockDir * forceMagnitude;
```

### hit.m_pushForce vs character.m_pushForce
- `hit.m_pushForce` → 게임이 `ApplyPushback(hit)` 호출 → `character.m_pushForce` 설정 시도
- 우리가 먼저 더 큰 값으로 설정했으면 게임의 설정이 무시됨 (magnitude 비교)
- 500f는 mass 고려 시 ~17 magnitude → 우리 45f보다 작아 덮어쓰지 않음

---

## 실패했던 방법들

### 1. body.velocity 직접 설정 (WaitForFixedUpdate 코루틴)
```csharp
yield return new WaitForFixedUpdate();
body.velocity += force;  // ❌ 다음 CustomFixedUpdate가 덮어씀
```
**실패 이유**: WaitForFixedUpdate는 물리 시뮬레이션 이후 실행, 다음 프레임 CustomFixedUpdate에서 velocity 초기화

### 2. Character.FixedUpdate Postfix
```csharp
[HarmonyPatch(typeof(Character), "FixedUpdate")]  // ❌ 메서드 없음
```
**실패 이유**: Valheim은 `FixedUpdate` 대신 `CustomFixedUpdate` 사용, Harmony 패치 무시됨

### 3. 25프레임 루프 코루틴
```csharp
for (int i = 0; i < 25; i++) {
    yield return new WaitForFixedUpdate();
    vel.x = force.x * progress;   // ❌ 효과 없음
    body.velocity = vel;
}
```
**실패 이유**: 동일한 타이밍 문제, 매 프레임 CustomFixedUpdate가 덮어씀

---

## 튜닝 가이드

| KnockbackPushForce | 지속 시간 | 예상 거리 |
|--------------------|-----------|-----------|
| 30f | 0.30s | ~4m |
| 45f | 0.45s | ~7m |
| 60f | 0.60s | ~9m |
| 80f | 0.80s | ~12m |

- `attackRatio` = `Configurations_MonsterAttackEffectPercent / 100f` 곱해짐
- `knockDir.y` 값: 0.0 (수평만) ~ 0.5 (강한 상향)

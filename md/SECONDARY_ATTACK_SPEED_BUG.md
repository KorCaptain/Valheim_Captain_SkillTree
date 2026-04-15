# 세컨더리 어택 속도 버그 (휠마우스 공격)

## 버그 요약

| 항목 | 내용 |
|------|------|
| **증상** | 휠마우스(세컨더리 어택) 속도가 비정상적으로 빠름 |
| **일반 공격** | 정상 |
| **원인** | AnimationSpeedManager의 `speed < 0.99 → 1.0` 보정 코드 |
| **해결** | 보정 코드 제거, 원본 speed에 보너스 적용 |

---

## 근본 원인

### Valheim의 세컨더리 어택 구조

Valheim에서 마우스 휠 공격(Secondary Attack)은 **의도적으로 낮은 animation speed**를 가진다.
예를 들어 `speed = 0.7` 등의 값으로 느린 강공격 애니메이션을 구현한다.

### 문제의 코드

```csharp
// Attack_Speed_bug.md에도 기록된 "올바른 구현"이었으나, 실제로는 버그 유발
AnimationSpeedManager.Add((character, speed) =>
{
    if (character is Player player && player.InAttack())
    {
        // 이 보정이 문제!
        double finalSpeed = speed < 0.99 ? 1.0 : speed;
        //     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        // 세컨더리 어택의 의도적 느린 speed(0.7 등)를 강제로 1.0으로 올림
        // 결과: 강공격이 일반 공격 속도로 재생 → 비정상적으로 빠름

        float attackSpeedBonus = SkillEffect.GetTotalAttackSpeedBonus(player);
        if (attackSpeedBonus > 0f)
        {
            double bonusMultiplier = 1.0 + (attackSpeedBonus / 100.0);
            finalSpeed = finalSpeed * bonusMultiplier;
        }
        return finalSpeed;
    }
    return speed;
});
```

### 작동 메커니즘

```
일반 공격 (speed = 1.0):
  1.0 >= 0.99 → 보정 안 됨 → 1.0 * 1.05 = 1.05 (정상)

세컨더리 어택 (speed = 0.7):
  0.7 < 0.99 → 강제 1.0으로 보정! → 1.0 * 1.05 = 1.05
  원래 의도: 0.7 * 1.05 = 0.735 (느린 강공격)
  실제 결과: 1.05 (일반 공격과 동일 속도!)
```

---

## 수정된 코드

```csharp
AnimationSpeedManager.Add((character, speed) =>
{
    if (character is Player player && player.InAttack())
    {
        // 돌진베기 등 특수 스킬은 고정 속도 사용
        if (Sword_Skill.IsSlashActive(player))
        {
            return 1.0 + (Sword_Config.RushSlashAttackSpeedBonusValue / 100.0);
        }

        // 일반 보너스는 원본 speed에 곱하기 (보정 없음)
        float attackSpeedBonus = SkillEffect.GetTotalAttackSpeedBonus(player);
        if (attackSpeedBonus > 0f)
        {
            double bonusMultiplier = 1.0 + (attackSpeedBonus / 100.0);
            return speed * bonusMultiplier;  // 원본 speed 유지!
        }

        return speed;  // 보너스 없으면 원본 그대로
    }
    return speed;
});
```

---

## 핵심 규칙

### AnimationSpeedManager에서 speed 값을 절대 보정하지 말 것

```csharp
// 금지 패턴들
double finalSpeed = speed < 0.99 ? 1.0 : speed;    // 하한 보정 금지
double finalSpeed = Math.Max(speed, 1.0);            // 최소값 강제 금지
double finalSpeed = Math.Clamp(speed, 0.5, 2.0);     // 범위 제한 금지

// 올바른 패턴
return speed * bonusMultiplier;  // 원본 speed에 배율만 적용
```

### 이유
- Valheim은 다양한 공격 타입별로 서로 다른 animation speed를 사용
- 일반 공격: ~1.0
- 세컨더리 어택: 0.5~0.9 (무기별 다름)
- 점프 공격: 다양
- 이 값을 보정하면 Valheim의 의도된 공격 밸런스가 깨짐

---

## 기존 문서와의 관계

| 문서 | 내용 | 관계 |
|------|------|------|
| `Attack_Speed_bug.md` | animator.speed 직접 조작 충돌 버그 | **다른 버그** (이전 발생) |
| `ATTACK_SPEED_SYSTEM_RULES.md` | AnimationSpeedManager 일반 규칙 | 상위 규칙 문서 |
| 이 문서 | speed 하한 보정으로 인한 세컨더리 어택 버그 | **이 버그** (2026-02-06 수정) |

> `Attack_Speed_bug.md`의 "올바른 구현" 예제에 `speed < 0.99 ? 1.0 : speed` 코드가 있으나,
> 이 코드가 세컨더리 어택 버그의 원인이었다. 해당 문서의 예제는 더 이상 유효하지 않다.

---

## 테스트 방법

```
1. 검(또는 다른 무기) 장착
2. 좌클릭 일반 공격 → 속도 정상인지 확인
3. 마우스 휠 세컨더리 어택 → 느린 강공격 애니메이션 유지되는지 확인
4. 공격속도 버프 활성화 후 3번 반복 → 살짝 빨라지되 비정상적으로 빠르지 않은지 확인
```

---

**작성일**: 2026-02-06
**수정 파일**: Plugin.Patches.cs (AnimationSpeedManager 핸들러)

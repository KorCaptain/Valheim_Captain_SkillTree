# 공격 속도 버그 해결 기록

## 버그 요약
**증상**: CaptainSkillTree 모드 활성화 시 모든 무기의 기본 공격 속도가 느려짐
**원인**: `animator.speed` 직접 조작과 `AnimationSpeedManager` 충돌
**해결**: animator.speed 직접 조작 패치 비활성화, AnimationSpeedManager로 통합

---

## 문제 상세

### 증상
- CaptainSkillTree 비활성화 시 공격 속도 정상
- CaptainSkillTree 활성화 시 모든 무기 공격이 느림
- 스킬 보너스 로그는 정상 출력되지만 실제 게임에서 적용 안됨

### 근본 원인

`Plugin.Patches.cs`의 `KnifeAttackSpeedAnimatorPatch`가 문제였음:

```csharp
// ⛔ 문제의 코드
[HarmonyPatch(typeof(CharacterAnimEvent), nameof(CharacterAnimEvent.CustomFixedUpdate))]
public static class KnifeAttackSpeedAnimatorPatch
{
    public static void Postfix(CharacterAnimEvent __instance)
    {
        // 단검이 아닌 무기 사용 시
        if (currentWeapon?.m_shared?.m_skillType != Skills.SkillType.Knives)
        {
            ResetAnimatorSpeed(player);  // ← animator.speed = 1f로 리셋!
            return;
        }
    }

    private static void ResetAnimatorSpeed(Player player)
    {
        animator.speed = 1f;  // ← AnimationSpeedManager 효과를 덮어씀!
    }
}
```

### 충돌 메커니즘

1. `AnimationSpeedManager.Add()` 핸들러가 공격 속도 증가 계산
2. `CharacterAnimEvent.CustomFixedUpdate`가 **매 프레임** 호출됨
3. 단검이 아닌 무기 사용 시 `animator.speed = 1f`로 강제 리셋
4. AnimationSpeedManager의 속도 증가가 다음 프레임에 덮어씌워짐
5. 결과: 공격 속도가 항상 1.0으로 고정됨

---

## 해결 방법

### 원칙
**모든 공격 속도는 AnimationSpeedManager에서 통합 처리**

### 올바른 구현 (Game.Awake에서 등록)

```csharp
[HarmonyPatch(typeof(Game), "Awake")]
public static class AttackSpeedHandler_Game_Awake_Patch
{
    [HarmonyPostfix]
    public static void Postfix()
    {
        AnimationSpeedManager.Add((character, speed) =>
        {
            if (character is Player player && player.InAttack())
            {
                // 입력 속도가 1.0 미만이면 보정
                double finalSpeed = speed < 0.99 ? 1.0 : speed;

                // 스킬 보너스 계산
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
    }
}
```

### 금지 사항

| 금지 | 이유 |
|------|------|
| `animator.speed = 값` 직접 조작 | AnimationSpeedManager와 충돌 |
| `CharacterAnimEvent.CustomFixedUpdate` 패치 | 매 프레임 호출로 덮어쓰기 |
| `Plugin.Awake()`에서 핸들러 등록 | 타이밍 오류 (Game.Awake 사용) |

---

## 진단 방법

### 1. 모드 비활성화 테스트
```
CaptainSkillTree 비활성화 → 공격 속도 정상?
→ YES: 우리 모드가 원인
→ NO: 다른 모드 또는 게임 문제
```

### 2. 로그 확인
```
[공속 진단] ⚠️ 입력 속도가 1.0 미만! speed=0.xxx
→ 다른 핸들러가 속도를 줄이고 있음

[공격 속도] ✅ +10%: 입력=1.000 → 출력=1.100
→ 정상 작동
```

### 3. animator.speed 조작 검색
```bash
grep -r "animator.speed" --include="*.cs"
grep -r "ResetAnimatorSpeed" --include="*.cs"
```

---

## 체크리스트

새로운 공격 속도 관련 기능 추가 시:

- [ ] `AnimationSpeedManager.Add()` 사용하는가?
- [ ] `Game.Awake` Postfix에서 등록하는가?
- [ ] `player.InAttack()` 조건 확인하는가?
- [ ] `animator.speed` 직접 조작 없는가?
- [ ] `CharacterAnimEvent` 패치 없는가?

---

## 관련 파일

| 파일 | 역할 |
|------|------|
| `Plugin.Patches.cs` | AnimationSpeedManager 핸들러 등록 (L517~) |
| `SkillEffect.SpeedTree.cs` | `GetTotalAttackSpeedBonus()` 구현 |
| `SkillTreeConfig.cs` | 공격 속도 Config 값들 |

---

## 버전 히스토리

| 버전 | 변경 |
|------|------|
| v0.1.224 | KnifeAttackSpeedAnimatorPatch 조건 추가 (부분 해결) |
| v0.1.225 | KnifeAttackSpeedAnimatorPatch 완전 비활성화 (완전 해결) |

---

## 참고: AnimationSpeedManager 작동 원리

WackyEpicMMOSystem의 AnimationSpeedManager:
1. 게임의 애니메이션 속도를 조절하는 핸들러 시스템
2. 여러 핸들러가 순차적으로 실행되며 speed 값을 전달
3. 최종 speed 값이 실제 애니메이션 속도에 반영됨

**중요**:
- `animator.speed` = Unity Animator 컴포넌트의 속도 (직접 조작 금지)
- `AnimationSpeedManager` = 게임 레벨의 공격 애니메이션 속도 (이것 사용)

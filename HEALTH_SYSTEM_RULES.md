# HEALTH_SYSTEM_RULES.md - Valheim 체력 시스템 및 힐링 규칙

CaptainSkillTree 모드의 체력 보너스 구현 시 힐링 깜빡임 방지 및 정상 작동 보장 규칙입니다.

---

## 📚 규칙 목록

- **Rule 9**: Valheim 체력 시스템 및 힐링 메커니즘 (Health System & Healing Rules)

---

## Rule 9: Valheim 체력 시스템 및 힐링 메커니즘

### 📋 목적
스킬 효과로 체력 증가 시 **힐링 깜빡임 방지** 및 **정상 작동 보장**

### 🎯 핵심 원리
**모든 체력 보너스는 반드시 `m_baseHP`에 포함되어야 힐링이 정상 작동합니다.**

---

## Valheim 체력 시스템 구조

### 체력 시스템 3요소

Valheim의 체력 시스템은 3가지 요소로 구성됩니다:

#### 1. m_baseHP (내부 체력)
- **역할**: 음식으로 얻은 기본 체력
- **특징**: **힐링이 작동하는 범위**
- **중요성**: 이 값에 포함되지 않은 보너스는 힐링되지 않음

#### 2. GetMaxHealth() (최대 체력)
- **역할**: UI에 표시되는 총 최대 체력
- **계산**: `m_baseHP + 추가 보너스`
- **용도**: 화면 표시 전용

#### 3. Player.Heal() (힐링 시스템)
- **역할**: 체력 회복 메커니즘
- **제한**: **m_baseHP 범위 내에서만 작동**
- **문제**: m_baseHP를 초과하는 구간은 힐링되지 않음

---

## Valheim 힐링 메커니즘

### 힐링 작동 원리

```
Player.Heal()은 m_baseHP를 초과하는 체력은 회복하지 못함
```

**문제 상황**:
- 스킬 보너스가 GetMaxHealth에만 있고 m_baseHP에 없으면
- → 보너스 구간에서 힐링 깜빡임 발생

### 예시: 힐링 깜빡임 문제

**시나리오**: 기본 체력 100, 스킬 보너스 +30 → 최대 130

#### ❌ 잘못된 구현 (힐링 깜빡임)
```csharp
// GetMaxHealth에만 보너스 추가
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix(Player __instance, ref float __result)
{
    __result += 30f;  // ❌ m_baseHP에 포함 안 됨
}
```

**결과**:
- m_baseHP: 100 (음식만)
- GetMaxHealth: 130 (100 + 30)
- **힐링 범위**: 0~100 (m_baseHP 범위만)
- **문제**: 100~130 구간에서 힐링 실패 → 깜빡임 발생

#### ✅ 올바른 구현 (힐링 정상)
```csharp
// GetTotalFoodValue에서 m_baseHP에 포함
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix(Player __instance, ref float hp, ref float stamina)
{
    hp += 30f;  // ✅ m_baseHP에 포함됨
}
```

**결과**:
- m_baseHP: 130 (음식 + 보너스)
- GetMaxHealth: 130 (m_baseHP 그대로)
- **힐링 범위**: 0~130 (전체 범위)
- **효과**: 전체 0~130 구간에서 힐링 정상 작동

---

## 체력 보너스 구현 규칙

### Rule 1: 고정 체력 보너스 → GetTotalFoodValue 패치

**패턴**: 고정된 수치(+20 HP)를 추가하는 스킬

```csharp
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static class SkillTree_Player_GetTotalFoodValue_HealthBonus_Patch
{
    public static void Postfix(Player __instance, ref float hp, ref float stamina)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // 고정 체력 보너스는 hpBonus에 추가 (m_baseHP에 포함됨)
        float hpBonus = 0f;

        // 예시 1: 피부경화 스킬로 체력 +20
        if (manager.GetSkillLevel("defense_Step1_survival") > 0)
        {
            float survivalBonus = Defense_Config.SurvivalHealthBonusValue;
            hpBonus += survivalBonus;
            Plugin.Log.LogDebug($"[피부경화] 고정 체력 보너스: +{survivalBonus}");
        }

        // 예시 2: 방어 전문가 루트로 체력 +20
        if (manager.GetSkillLevel("defense_root") > 0)
        {
            float rootBonus = Defense_Config.DefenseRootHealthBonusValue;
            hpBonus += rootBonus;
            Plugin.Log.LogDebug($"[방어 전문가] 고정 체력 보너스: +{rootBonus}");
        }

        // 기본 체력에 추가 → m_baseHP에 포함
        hp += hpBonus;

        if (hpBonus > 0f)
        {
            Plugin.Log.LogInfo($"[체력 시스템] 총 고정 보너스: +{hpBonus}, 최종 m_baseHP: {hp:F0}");
        }
    }
}
```

**장점**:
- m_baseHP에 직접 포함되어 힐링 정상 작동
- 간단하고 직관적인 구현
- 음식 버프와 동일한 방식으로 처리

---

### Rule 2: 비율 체력 보너스 → GetTotalFoodValue에서 고정값으로 변환

**패턴**: 비율(+30%)로 체력을 증가시키는 스킬

**핵심**: 비율 보너스를 **고정 수치로 변환**하여 m_baseHP에 포함

```csharp
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static class SkillTree_Player_GetTotalFoodValue_HealthPercent_Patch
{
    public static void Postfix(Player __instance, ref float hp, ref float stamina)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float hpBonus = 0f;

        // === 고정 보너스 먼저 적용 ===
        if (manager.GetSkillLevel("defense_Step1_survival") > 0)
        {
            float survivalBonus = Defense_Config.SurvivalHealthBonusValue;
            hpBonus += survivalBonus;
        }

        // === 비율 보너스를 "고정 수치"로 변환 ===
        if (manager.GetSkillLevel("defense_Step6_body") > 0)
        {
            // 현재까지의 총 기본 체력 (음식 + 고정 보너스)
            float baseHealthBeforeBonus = hp + hpBonus;

            // 비율 보너스 계산 (30% = 0.3)
            float bonusPercent = Defense_Config.BodyHealthBonusValue / 100f;

            // 고정 체력으로 변환
            float bonusHealth = baseHealthBeforeBonus * bonusPercent;

            // m_baseHP에 포함
            hpBonus += bonusHealth;

            Plugin.Log.LogDebug($"[요툰의 생명력] 기본 체력 +{bonusPercent * 100f}%: {baseHealthBeforeBonus:F0} * {bonusPercent:F2} = +{bonusHealth:F0}");
        }

        // 전체 보너스를 기본 체력에 추가
        hp += hpBonus;

        if (hpBonus > 0f)
        {
            Plugin.Log.LogInfo($"[체력 시스템] 총 보너스: +{hpBonus:F0}, 최종 m_baseHP: {hp:F0}");
        }
    }
}
```

**변환 로직**:
1. **기본 체력 계산**: 음식 + 고정 보너스 = `baseHealthBeforeBonus`
2. **비율 적용**: `baseHealthBeforeBonus × 0.3` = 고정 수치
3. **m_baseHP에 포함**: `hp += 고정 수치`

**예시**:
- 음식 체력: 100
- 고정 보너스: +20 (피부경화)
- 비율 보너스: +30% (요툰의 생명력)
- 계산: (100 + 20) × 0.3 = **+36**
- 최종 m_baseHP: 100 + 20 + 36 = **156**

---

### Rule 3: GetMaxHealth 패치는 표시 전용

**원칙**: GetMaxHealth는 **UI 표시 전용**으로만 사용

```csharp
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static class SkillTree_Player_GetMaxHealth_Display_Patch
{
    public static void Postfix(Player __instance, ref float __result)
    {
        // ⚠️ GetMaxHealth는 UI 표시용
        // 힐링에 영향을 주는 보너스는 반드시 GetTotalFoodValue에서 처리
        // 여기서는 m_baseHP에 이미 포함되지 않은 표시 전용 보너스만 추가

        // 예시: 이미 GetTotalFoodValue에서 처리된 보너스는 여기서 추가하지 않음
        // === 요툰의 생명력: GetTotalFoodValue에서 이미 처리됨 ===
        // ✅ 힐링 깜빡임 방지: 비율 보너스를 고정 수치로 변환하여 m_baseHP에 포함

        // ❌ 여기서 추가하면 힐링 깜빡임 발생
        // __result += someBonus;  // 절대 금지!

        Plugin.Log.LogDebug($"[체력 시스템] 최종 GetMaxHealth: {__result:F0}");
    }
}
```

**용도**:
- UI에 표시할 최대 체력만 조정
- m_baseHP에 포함되지 않은 표시 전용 보너스만 추가
- **대부분의 경우 GetTotalFoodValue만 사용하면 충분**

---

## 체력 보너스 분류 가이드

| 보너스 타입 | 구현 위치 | 이유 | 힐링 작동 |
|------------|---------|------|---------|
| **고정 체력 보너스** (+20 HP) | GetTotalFoodValue | m_baseHP에 포함 | ✅ 정상 |
| **비율 체력 보너스** (+30%) | GetTotalFoodValue (고정값 변환) | 비율을 고정값으로 변환하여 m_baseHP에 포함 | ✅ 정상 |
| **표시 전용 보너스** | GetMaxHealth | 힐링에 영향 없는 UI 표시만 | ⚠️ 힐링 안 됨 |

---

## 금지 사항

### ❌ 비율 보너스를 GetMaxHealth에만 추가 금지

```csharp
// ❌ 잘못된 예시: 힐링 깜빡임 유발
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix(Player __instance, ref float __result)
{
    float bonusPercent = 0.3f;  // 30%
    __result *= (1f + bonusPercent);  // m_baseHP에 포함 안 됨!
}
```

**문제**:
- GetMaxHealth만 증가 → m_baseHP는 그대로
- 보너스 구간에서 힐링 작동 안 함
- 힐링 아이템 사용 시 깜빡임 발생

### ❌ 고정 보너스를 GetMaxHealth에만 추가 금지

```csharp
// ❌ 잘못된 예시: 힐링 작동 안 함
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix(Player __instance, ref float __result)
{
    __result += 20f;  // m_baseHP에 포함 안 됨!
}
```

**문제**:
- 고정 보너스가 m_baseHP에 없음
- 보너스 구간에서 힐링 실패

### ❌ 중복 적용 금지

```csharp
// ❌ 잘못된 예시: 중복 적용
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix1(Player __instance, ref float hp, ref float stamina)
{
    hp += 20f;  // 첫 번째 적용
}

[HarmonyPatch(typeof(Player), nameof(Player.GetMaxHealth))]
public static void Postfix2(Player __instance, ref float __result)
{
    __result += 20f;  // 중복 적용! (총 +40)
}
```

**문제**:
- GetTotalFoodValue와 GetMaxHealth에서 같은 보너스 중복
- 의도하지 않은 과도한 체력 증가

---

## 디버깅 패턴

### 체력 시스템 디버깅 로그

```csharp
// 패턴 1: 고정 보너스
Plugin.Log.LogDebug($"[피부경화] 고정 체력 보너스: +{survivalBonus}");

// 패턴 2: 비율 보너스 변환
Plugin.Log.LogDebug($"[요툰의 생명력→음식] 기본 체력 +{bonusPercent * 100f}%: {baseHealthBeforeBonus:F0} * {bonusPercent:F2} = +{bonusHealth:F0}");

// 패턴 3: 최종 m_baseHP
Plugin.Log.LogDebug($"[체력 시스템] 최종 m_baseHP: {hp:F0}, 최대 체력: {__result:F0}");

// 패턴 4: 총 보너스 요약
Plugin.Log.LogInfo($"[체력 시스템] 총 보너스: +{hpBonus:F0}, 최종 m_baseHP: {hp:F0}");
```

### 힐링 테스트 패턴

```csharp
// 테스트 시나리오
// 1. 스킬 활성화 후 체력 확인
Plugin.Log.LogInfo($"[테스트] 스킬 활성화: m_baseHP={player.GetHealth():F0}, Max={player.GetMaxHealth():F0}");

// 2. 데미지 받기
player.Damage(new HitData { m_damage = new HitData.DamageTypes { m_blunt = 50f } });
Plugin.Log.LogInfo($"[테스트] 데미지 후: m_baseHP={player.GetHealth():F0}");

// 3. 힐링 아이템 사용
player.Heal(50f);
Plugin.Log.LogInfo($"[테스트] 힐링 후: m_baseHP={player.GetHealth():F0}");

// 4. 보너스 구간에서 힐링 확인
// ✅ 정상: 체력이 증가함
// ❌ 깜빡임: 체력이 증가하지 않음
```

---

## 참고 구현 예시

### 성공 사례 1: 요툰의 생명력 (비율 보너스)

**위치**: `SkillEffect.cs` Lines 706-715

```csharp
// 비율 보너스 → 고정값 변환 성공 사례
if (manager.GetSkillLevel("defense_Step6_body") > 0)
{
    float baseHealthBeforeBonus = hp + hpBonus;
    float bonusPercent = Defense_Config.BodyHealthBonusValue / 100f;
    float bonusHealth = baseHealthBeforeBonus * bonusPercent;
    hpBonus += bonusHealth;

    Plugin.Log.LogDebug($"[요툰의 생명력→음식] 기본 체력 +{bonusPercent * 100f}%: {baseHealthBeforeBonus:F0} * {bonusPercent:F2} = +{bonusHealth:F0}");
}

hp += hpBonus;  // m_baseHP에 포함
```

**결과**: 힐링 정상 작동, 깜빡임 없음

### 성공 사례 2: 피부경화 (고정 보너스)

**위치**: `SkillEffect.cs`

```csharp
// 고정 체력 보너스 구현
if (manager.GetSkillLevel("defense_Step1_survival") > 0)
{
    float survivalBonus = Defense_Config.SurvivalHealthBonusValue;
    hpBonus += survivalBonus;
    Plugin.Log.LogDebug($"[피부경화] 고정 체력 보너스: +{survivalBonus}");
}

hp += hpBonus;  // m_baseHP에 포함
```

**결과**: 힐링 정상 작동

---

## 체크리스트

체력 보너스 구현 시 다음을 확인하세요:

### 구현 단계
- [ ] **GetTotalFoodValue 패치**: 체력 보너스는 반드시 여기서 구현
- [ ] **고정 보너스**: `hp += bonusValue` 형태로 직접 추가
- [ ] **비율 보너스**: 고정값으로 변환 후 추가 (`baseHealth × percent`)
- [ ] **m_baseHP 포함 확인**: `hp += hpBonus` 반드시 실행

### 디버깅 단계
- [ ] **로그 추가**: 각 스킬별 보너스 계산 과정 로그
- [ ] **최종 m_baseHP 확인**: 로그로 최종 m_baseHP 값 확인
- [ ] **힐링 테스트**: 게임에서 실제로 힐링 작동 확인
- [ ] **보너스 구간 테스트**: 보너스 구간에서도 힐링 정상 작동 확인

### 금지 사항 확인
- [ ] **GetMaxHealth 금지**: 힐링 영향 보너스는 GetMaxHealth에 추가 금지
- [ ] **중복 적용 금지**: GetTotalFoodValue와 GetMaxHealth에서 중복 적용 금지
- [ ] **비율 직접 적용 금지**: 비율은 반드시 고정값으로 변환 후 적용

---

## 문제 해결 가이드

### 증상: 힐링 깜빡임 (체력이 회복되지 않음)

**원인**:
- 보너스가 GetMaxHealth에만 적용됨
- m_baseHP에 포함되지 않음

**해결**:
1. GetMaxHealth 패치 제거
2. GetTotalFoodValue에서 `hp += bonus` 추가
3. 게임 재시작 후 테스트

### 증상: 체력이 과도하게 증가

**원인**:
- GetTotalFoodValue와 GetMaxHealth에서 중복 적용

**해결**:
1. 중복된 패치 하나 제거
2. GetTotalFoodValue만 사용 권장

### 증상: 비율 보너스가 작동 안 함

**원인**:
- 비율을 고정값으로 변환하지 않음

**해결**:
1. `baseHealth × percent` 계산
2. 계산 결과를 `hp += bonusHealth`로 추가

---

## 🔗 관련 문서

- [CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 목차
- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - 체력 시스템 빠른 참조
- [CONFIG_MANAGEMENT_RULES.md](CONFIG_MANAGEMENT_RULES.md) - Config 연동 (Rule 7)
- [DEVELOPMENT_PATTERNS.md](DEVELOPMENT_PATTERNS.md) - 체력 시스템 성공/실패 사례

---

**작성일**: 2025-01-29
**버전**: 1.0
**적용 범위**: Rule 9

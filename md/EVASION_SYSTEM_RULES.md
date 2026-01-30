# Rule 15: 회피 시스템 규칙

## 📋 개요

CaptainSkillTree의 회피 시스템은 **3가지 핵심 요소**로 구성됩니다:
1. **회피 확률 (Dodge Chance)** - 공격을 완전히 회피하는 확률
2. **구르기 무적시간 (Dodge Invincibility)** - 구르기 중 피해를 받지 않는 시간
3. **구르기 스태미나 (Dodge Stamina)** - 구르기 시 소모되는 스태미나

---

## 🎯 핵심 규칙

### Rule 15-1: 회피 확률 시스템 (SetCustomDodgeChance)

**구현 위치**: `SkillEffect.StatTree.cs` - `UpdateDefenseDodgeRate()`

#### **방어 트리 회피 스킬**

```csharp
/// <summary>
/// 방어 트리 회피율 계산 및 적용
/// Player.SetCustomDodgeChance()로 Valheim 회피 시스템에 통합
/// </summary>
public static void UpdateDefenseDodgeRate(Player player)
{
    float totalDodge = 0f;

    // defense_Step3_agile: 회피단련 - 회피 +15%
    if (manager.GetSkillLevel("defense_Step3_agile") > 0)
    {
        totalDodge += Defense_Config.AgileDodgeBonusValue / 100f;
    }

    // defense_Step5_stamina: 기민함 - 회피 +10%
    if (manager.GetSkillLevel("defense_Step5_stamina") > 0)
    {
        totalDodge += Defense_Config.StaminaDodgeBonusValue / 100f;
    }

    // defense_Step6_attack: 신경강화 - 회피 +8%
    if (manager.GetSkillLevel("defense_Step6_attack") > 0)
    {
        totalDodge += Defense_Config.AttackDodgeBonusValue / 100f;
    }

    // knife_step2_evasion: 회피 숙련 - 회피 +15% (단검 전문가, 무조건 적용)
    if (manager.GetSkillLevel("knife_step2_evasion") > 0)
    {
        totalDodge += Knife_Config.KnifeEvasionBonusValue / 100f;
    }

    // Valheim 회피 시스템에 적용
    player.SetCustomDodgeChance(totalDodge);
}
```

**적용 시점**:
- `Player.Awake` - 플레이어 생성 시 초기화
- 스킬 활성화/해제 시 - `UpdateDefenseDodgeRate()` 호출

**핵심 포인트**:
- ✅ `SetCustomDodgeChance()` 사용 - Valheim 표준 API
- ✅ 모든 회피 보너스는 **가산 방식** (0.15 + 0.10 = 0.25 = 25% 회피)
- ✅ 단검 회피는 **무조건 적용** (단검 착용 여부 무관)
- ❌ 곱셈 방식 사용 금지
- ❌ 직접 m_dodgeSkill 필드 수정 금지

---

### Rule 15-2: 구르기 무적시간 시스템 (Dodge Invincibility)

**구현 위치**: `SkillEffect.DefenseTree.cs` - `Player_Dodge_DefenseTree_Patch.Postfix`

#### **방어 트리 + 단검 트리 무적시간 스킬**

```csharp
/// <summary>
/// 구르기 무적시간 증가
/// Player.Dodge Postfix - Harmony Traverse로 m_dodgeInvincibilityTimer 수정
/// </summary>
[HarmonyPatch(typeof(Player), "Dodge")]
public static class Player_Dodge_DefenseTree_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(Player __instance)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // defense_Step3_agile: 회피단련 - 무적시간 +20%
        if (manager.GetSkillLevel("defense_Step3_agile") > 0)
        {
            float bonus = Defense_Config.AgileInvincibilityBonusValue / 100f;
            var traverse = Traverse.Create(__instance);
            float currentTimer = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
            float newTimer = currentTimer * (1f + bonus); // 배율 방식
            traverse.Field("m_dodgeInvincibilityTimer").SetValue(newTimer);

            Plugin.Log.LogDebug($"[회피단련] 무적시간 증가: {currentTimer:F2}초 → {newTimer:F2}초 (+{bonus * 100f}%)");
        }

        // knife_step2_evasion: 회피 숙련 - 무적시간 +15% (단검 착용 시만)
        if (manager.GetSkillLevel("knife_expert") > 0 &&
            manager.GetSkillLevel("knife_step2_evasion") > 0 &&
            SkillEffect.IsUsingDagger(__instance))
        {
            float bonus = Knife_Config.KnifeEvasionBonusValue / 100f;
            var traverse = Traverse.Create(__instance);
            float currentTimer = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
            float newTimer = currentTimer * (1f + bonus); // 배율 방식
            traverse.Field("m_dodgeInvincibilityTimer").SetValue(newTimer);

            Plugin.Log.LogDebug($"[회피 숙련] 단검 무적시간 증가: {currentTimer:F2}초 → {newTimer:F2}초 (+{bonus * 100f}%)");
        }
    }
}
```

**적용 방식**:
- **배율 적용**: `newTimer = currentTimer * (1 + bonus%)`
- **중첩 가능**: 회피단련(+20%) + 단검 회피(+15%) = 기본 무적시간 * 1.20 * 1.15 = **38% 증가**

**핵심 포인트**:
- ✅ `Player.Dodge` Postfix 사용 - 구르기 후 무적시간 수정
- ✅ Harmony Traverse로 protected 필드 접근
- ✅ 배율 방식으로 중첩 적용
- ✅ 단검 회피는 **조건부 적용** (단검 착용 시만)
- ❌ Prefix에서 수정 금지 (타이밍 오류)
- ❌ 가산 방식 사용 금지 (밸런스 문제)

---

### Rule 15-3: 구르기 스태미나 감소 시스템 (Dodge Stamina)

**구현 위치**: `SkillEffect.DefenseTree.cs` - `Player_Dodge_DefenseTree_Patch.Prefix`

#### **방어 트리 스태미나 스킬**

```csharp
/// <summary>
/// 구르기 스태미나 감소
/// Player.Dodge Prefix - Harmony Traverse로 m_dodgeStaminaUsage 수정
/// </summary>
[HarmonyPatch(typeof(Player), "Dodge")]
public static class Player_Dodge_DefenseTree_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Prefix(Player __instance, ref Vector3 dodgeDir)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // defense_Step5_stamina: 기민함 - 구르기 스태미나 -12%
        if (manager.GetSkillLevel("defense_Step5_stamina") > 0)
        {
            float reduction = Defense_Config.StaminaRollStaminaReductionValue / 100f;
            var traverse = Traverse.Create(__instance);
            float originalStamina = traverse.Field("m_dodgeStaminaUsage").GetValue<float>();
            float reducedStamina = originalStamina * (1f - reduction); // 감소 배율
            traverse.Field("m_dodgeStaminaUsage").SetValue(reducedStamina);

            Plugin.Log.LogDebug($"[기민함] 구르기 스태미나 감소: {originalStamina:F1} → {reducedStamina:F1} (-{reduction * 100f}%)");
        }
    }
}
```

**적용 방식**:
- **감소 배율**: `reducedStamina = originalStamina * (1 - reduction%)`
- **12% 감소**: 기본 스태미나 10 → 8.8 소모

**핵심 포인트**:
- ✅ `Player.Dodge` Prefix 사용 - 구르기 전 스태미나 수정
- ✅ Harmony Traverse로 protected 필드 접근
- ✅ 감소 배율 방식 사용
- ❌ Postfix에서 수정 금지 (타이밍 늦음)
- ❌ 가산 방식 사용 금지

---

## 📊 회피 스킬 통합 비교표

### 회피 확률 (Dodge Chance)

| 스킬 ID | 스킬명 | 회피 확률 | 조건 | 적용 방식 |
|---------|--------|----------|------|-----------|
| defense_Step3_agile | 회피단련 | +15% | 방어 루트 | 가산 |
| defense_Step5_stamina | 기민함 | +10% | 방어 루트 | 가산 |
| defense_Step6_attack | 신경강화 | +8% | 방어 루트 | 가산 |
| knife_step2_evasion | 회피 숙련 | +15% | 단검 전문가 | 가산 |

**최대 회피 확률**: 15% + 10% + 8% + 15% = **48%**

### 구르기 무적시간 (Dodge Invincibility)

| 스킬 ID | 스킬명 | 무적시간 | 조건 | 적용 방식 |
|---------|--------|----------|------|-----------|
| defense_Step3_agile | 회피단련 | +20% | 방어 루트 | 배율 |
| knife_step2_evasion | 회피 숙련 | +15% | 단검 착용 | 배율 |

**최대 무적시간 증가**: 기본 * 1.20 * 1.15 = **38% 증가**

### 구르기 스태미나 (Dodge Stamina)

| 스킬 ID | 스킬명 | 스태미나 감소 | 조건 | 적용 방식 |
|---------|--------|--------------|------|-----------|
| defense_Step5_stamina | 기민함 | -12% | 방어 루트 | 감소 배율 |

**최대 스태미나 감소**: 기본 * 0.88 = **12% 감소**

---

## 🔍 Valheim 회피 시스템 통합

### SetCustomDodgeChance() - Valheim API

**Valheim의 공식 회피 시스템**:
```csharp
// Player.cs 내부 구현
public void SetCustomDodgeChance(float chance)
{
    m_customDodgeChance = Mathf.Clamp01(chance); // 0~1 범위 제한
}

// 회피 판정 시 사용
private bool Dodge(Vector3 dodgeDir)
{
    float totalDodgeChance = m_dodgeSkill.m_level * 0.01f + m_customDodgeChance;

    if (UnityEngine.Random.value < totalDodgeChance)
    {
        // 회피 성공!
        StartDodge(dodgeDir);
        return true;
    }
    return false;
}
```

**CaptainSkillTree 연동**:
- ✅ `SetCustomDodgeChance()` 사용 - Valheim 표준 방식
- ✅ Dodge 스킬 레벨 보너스와 **독립적으로 중첩**
- ✅ 최대 100% 제한 (Valheim 자체 Clamp01)

---

## 🛠️ 구현 패턴

### Pattern 1: 회피 확률 업데이트 패턴

```csharp
// 스킬 변경 시 호출
public static void UpdateDefenseDodgeRate(Player player)
{
    float totalDodge = 0f;

    // 모든 회피 스킬 합산
    totalDodge += GetDefenseTreeDodgeBonus();
    totalDodge += GetKnifeDodgeBonus();

    // Valheim 시스템에 적용
    player.SetCustomDodgeChance(totalDodge);
}

// Player.Awake에서 초기화
[HarmonyPatch(typeof(Player), "Awake")]
public static void Postfix(Player __instance)
{
    UpdateDefenseDodgeRate(__instance);
}
```

### Pattern 2: 무적시간 증가 패턴

```csharp
// Player.Dodge Postfix - 구르기 후 즉시 적용
[HarmonyPatch(typeof(Player), "Dodge")]
public static void Postfix(Player __instance)
{
    float bonus = GetInvincibilityBonus();

    var traverse = Traverse.Create(__instance);
    float current = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
    float modified = current * (1f + bonus); // 배율 방식
    traverse.Field("m_dodgeInvincibilityTimer").SetValue(modified);
}
```

### Pattern 3: 스태미나 감소 패턴

```csharp
// Player.Dodge Prefix - 구르기 전 스태미나 수정
[HarmonyPatch(typeof(Player), "Dodge")]
public static void Prefix(Player __instance)
{
    float reduction = GetStaminaReduction();

    var traverse = Traverse.Create(__instance);
    float original = traverse.Field("m_dodgeStaminaUsage").GetValue<float>();
    float reduced = original * (1f - reduction); // 감소 배율
    traverse.Field("m_dodgeStaminaUsage").SetValue(reduced);
}
```

---

## 🚨 금지 사항

### 회피 확률 시스템
1. ❌ **직접 m_dodgeSkill 수정 금지**
   - Valheim 내부 스킬 시스템 손상 가능
   - `SetCustomDodgeChance()` 사용 필수

2. ❌ **곱셈 방식 사용 금지**
   - 회피 확률은 가산 방식만 허용
   - 예: 15% + 10% = 25% (O), 15% * 1.1 = 16.5% (X)

3. ❌ **100% 초과 회피 금지**
   - Valheim 자체적으로 Clamp01 적용
   - 밸런스 파괴 방지

### 무적시간 시스템
1. ❌ **Prefix에서 무적시간 수정 금지**
   - 타이밍 오류 발생
   - Postfix에서만 수정

2. ❌ **가산 방식 사용 금지**
   - 무적시간은 배율 방식만 허용
   - 예: 0.4초 * 1.2 = 0.48초 (O), 0.4초 + 0.08초 = 0.48초 (X)

3. ❌ **단검 회피를 무조건 적용 금지**
   - 단검 착용 여부 확인 필수
   - `SkillEffect.IsUsingDagger()` 사용

### 스태미나 시스템
1. ❌ **Postfix에서 스태미나 수정 금지**
   - 구르기 후 수정하면 이미 소모됨
   - Prefix에서만 수정

2. ❌ **가산 방식 사용 금지**
   - 스태미나는 감소 배율 방식만 허용
   - 예: 10 * 0.88 = 8.8 (O), 10 - 1.2 = 8.8 (X)

---

## ✅ 권장 사항

### 1. Valheim API 우선 사용
- ✅ `SetCustomDodgeChance()` - 회피 확률
- ✅ Harmony Traverse - protected 필드 접근
- ✅ `Player.Dodge` 패치 - 무적시간/스태미나

### 2. 조건부 적용 명확화
- ✅ 방어 트리: 루트 노드 확인
- ✅ 단검 트리: knife_expert + 스킬 + 착용 확인

### 3. Config 기반 개발
- ✅ 모든 수치는 Config에서 관리
- ✅ 툴팁과 효과 수치 동일하게 유지
- ✅ 3단계 연동 (Config → 툴팁 → 효과)

### 4. 디버그 로그 활용
- ✅ 회피 확률 적용 시 로그 출력
- ✅ 무적시간 변경 내역 로그
- ✅ 스태미나 감소 내역 로그

### 5. 중복 방지
- ✅ 회피 확률: UpdateDefenseDodgeRate()에서 일괄 처리
- ✅ 무적시간: 한 패치에서 모든 보너스 적용
- ✅ 스태미나: 한 패치에서 모든 감소 적용

---

## 🧪 테스트 체크리스트

### 회피 확률 테스트
- [ ] defense_Step3_agile: 회피 확률 +15% 적용
- [ ] defense_Step5_stamina: 회피 확률 +10% 추가
- [ ] defense_Step6_attack: 회피 확률 +8% 추가
- [ ] knife_step2_evasion: 회피 확률 +15% 추가 (무조건)
- [ ] 모든 스킬 활성화 시: 총 48% 회피 확률
- [ ] F5 Console에서 SetCustomDodgeChance(0.48) 확인

### 무적시간 테스트
- [ ] defense_Step3_agile: 무적시간 +20% 적용
- [ ] knife_step2_evasion + 단검 착용: 무적시간 +15% 추가
- [ ] 단검 미착용: knife 무적시간 적용 안 됨
- [ ] 두 스킬 모두: 기본 * 1.2 * 1.15 = 38% 증가
- [ ] F5 Console에서 무적시간 로그 확인

### 스태미나 테스트
- [ ] defense_Step5_stamina: 구르기 스태미나 -12% 적용
- [ ] 기본 10 스태미나 → 8.8 소모 확인
- [ ] F5 Console에서 스태미나 로그 확인

---

## 🔗 관련 규칙

- **Rule 1**: MMO 시스템 연동 - 회피는 Valheim 자체 시스템 사용
- **Rule 7**: Config 관리 - 회피 수치는 Config로 관리
- **Rule 14**: 공격 속도 시스템 - 유사한 Harmony 패턴

---

**마지막 업데이트**: 2025-01-13
**검증 상태**: ✅ 게임 내 테스트 완료 (방어 트리 + 단검 트리)

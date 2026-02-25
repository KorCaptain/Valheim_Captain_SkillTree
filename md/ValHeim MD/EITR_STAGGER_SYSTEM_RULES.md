# EITR_STAGGER_SYSTEM_RULES.md - 에이트르 및 비틀거림 시스템 규칙

## 📋 개요

**목적**: 에이트르(마법 자원) 및 비틀거림(기절 상태) 시스템의 구현 패턴 정리

**검증 상태**:
- ✅ **에이트르 시스템**: 검증 완료 (방어 트리, 지팡이 트리 등 다수 스킬에서 사용 중)
- ⚠️ **비틀거림 시스템**: 부분 검증 필요 (IsStaggering() API 작동 확인 필요)

**적용 범위**: 에이트르 최대치 증가, 비틀거림 상태 감지 및 추가 효과

---

## Rule 18: 에이트르(Eitr) 시스템

### Rule 18-1: 에이트르 최대치 증가 시스템 ✅ (검증 완료)

#### 개요
- **정의**: 마법 사용을 위한 자원인 에이트르의 최대치 증가
- **Valheim API**: `Player.GetTotalFoodValue()` 패치, `Player.GetMaxEitr()`
- **용도**: 마법사, 지팡이 사용자 등 마법 자원 증가
- **패치 방식**: Postfix로 eitr 파라미터에 보너스 합산
- **검증 상태**: ✅ 다수 스킬에서 정상 작동 확인

#### 현재 구현된 에이트르 스킬 목록

| 스킬 ID | 이름 | 효과 | 트리 | 검증 |
|---------|------|------|------|------|
| defense_Step2_dodge | 심신단련 | 스태미나 +30, 에이트르 +10 | 방어 | ✅ |
| defense_Step3_breath | 단전호흡 | 에이트르 +20 | 방어 | ✅ |
| speed_3 | 신속 전문가 | 에이트르 +10 | 속도 | ✅ |
| staff_Step2_stream | 마법 흐름 | 에이트르 +30 | 지팡이 | ✅ |

#### 구현 패턴 (GetTotalFoodValue 패치)

**고정값 에이트르 보너스**:
```csharp
/// <summary>
/// 에이트르 최대치 증가 (GetTotalFoodValue 패치)
/// mmo 시스템과 동일한 방식으로 음식 보너스에 합산
/// </summary>
[HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
public static class Player_GetTotalFoodValue_Eitr_YourSkill_Patch
{
    [HarmonyPriority(Priority.Low)]  // MMO 이후 실행
    public static void Postfix(ref float eitr)
    {
        try
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return;

            float eitrBonus = 0f;

            // 스킬 체크
            if (manager.GetSkillLevel("your_skill_id") > 0)
            {
                eitrBonus += YourConfig.EitrBonusValue;  // 고정값
            }

            if (eitrBonus > 0)
            {
                eitr += eitrBonus;
                Plugin.Log.LogDebug($"[스킬] 에이트르 보너스: +{eitrBonus}");
            }
        }
        catch (System.Exception ex)
        {
            Plugin.Log.LogError($"[스킬] 에이트르 패치 오류: {ex.Message}");
        }
    }
}
```

**비율 에이트르 보너스** (현재 사용 안 함, 참고용):
```csharp
[HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
public static class Player_GetTotalFoodValue_Eitr_Percent_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(ref float eitr)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float multiplier = 1f;

        // 예: 에이트르 +20%
        if (manager.GetSkillLevel("your_skill_id") > 0)
        {
            float bonus = YourConfig.EitrBonusPercentValue / 100f;
            multiplier += bonus;
        }

        eitr *= multiplier;
    }
}
```

#### 에이트르 소모 (액티브 스킬)

**UseEitr() 방식**:
```csharp
// 고정량 소모
float eitrCost = 50f;
if (player.GetEitr() < eitrCost)
{
    ShowMessage(player, "에이트르가 부족합니다.");
    return;
}
player.UseEitr(eitrCost);
```

**AddEitr() 방식**:
```csharp
// 음수로 소모
float eitrCost = 50f;
player.AddEitr(-eitrCost);
```

**최대치 비율 소모**:
```csharp
// 최대 에이트르의 30% 소모
float eitrCostPercent = 0.3f;
float eitrCost = player.GetMaxEitr() * eitrCostPercent;

if (player.GetEitr() < eitrCost)
{
    ShowMessage(player, "에이트르가 부족합니다.");
    return;
}
player.UseEitr(eitrCost);
```

---

## Rule 19: 비틀거림(Stagger) 시스템

### Rule 19-1: 비틀거림 상태 감지 시스템 ⚠️ (검증 진행 중)

#### 개요
- **정의**: 적이 비틀거림(기절) 상태일 때 추가 효과 적용
- **Valheim API**: `Character.IsStaggering()` (검증 필요)
- **용도**: 비틀거림 중 추가 피해, 특수 효과 발동
- **패치 방식**: Humanoid.Damage 또는 Character.Damage Postfix
- **검증 상태**: ⚠️ IsStaggering() 메서드 존재 및 작동 확인 필요
- **검증 빌드**: v0.1.64 (상세 로그 추가됨)

#### 📋 검증 가이드

**⚠️ 검증 작업 진행 중 - 자세한 절차는 아래 가이드 참조**

👉 **[STAGGER_VERIFICATION_GUIDE.md](./STAGGER_VERIFICATION_GUIDE.md)** - 완전한 검증 절차 및 체크리스트

#### ⚠️ 검증 필요 사항

**IsStaggering() API 확인 필요**:
```yaml
검증_필요_항목:
  Phase_1_API_존재_확인:
    - [ ] Character.IsStaggering() 메서드가 Valheim에 실제로 존재하는가?
    - [ ] 검증 로그가 정상적으로 출력되는가?
    - [ ] API 호출 시 오류가 발생하지 않는가?

  Phase_2_추가_피해_검증:
    - [ ] 비틀거림 상태일 때 true를 반환하는가?
    - [ ] 30% 데미지 증가가 적용되는가?
    - [ ] 일반 상태에서는 false를 반환하는가?

  Phase_3_다양한_상황_테스트:
    - [ ] 기절(stun), 넉백(knockback)과 구분되는가?
    - [ ] 모든 적 유형(인간형, 동물, 보스)에서 작동하는가?
    - [ ] 멀티플레이어 환경에서 동기화되는가?

  Phase_4_성능_검증:
    - [ ] FPS 영향 5% 이내
    - [ ] 로그 스팸 없음
```

**검증 방법 (요약)**:
1. **게임 실행**: Valheim 실행 및 knife_stagger 스킬 활성화
2. **비틀거림 유발**: 둔기로 적을 공격하여 비틀거림 상태 만들기
3. **로그 확인**: BepInEx 로그에서 검증 메시지 확인
   - ✅ `[Stagger 검증 성공] 비틀거림 추가 피해 적용!` 출력 확인
   - ❌ `[Stagger 검증 실패] IsStaggering() API 오류` 발생 확인
4. **결과 보고**: STAGGER_VERIFICATION_GUIDE.md의 보고 양식 작성

**자세한 검증 절차는 STAGGER_VERIFICATION_GUIDE.md를 참조하세요.**

#### 현재 구현된 비틀거림 스킬

| 스킬 ID | 이름 | 효과 | 트리 | 검증 |
|---------|------|------|------|------|
| knife_stagger | 단검 기절 | 비틀거림 중 피해 +30% | 단검 | ⚠️ 검증 필요 |

#### 구현 패턴 (비틀거림 추가 피해)

**비틀거림 중 피해 증가** (현재 구현):
```csharp
/// <summary>
/// 비틀거림 상태의 적에게 추가 피해
/// Plugin.cs의 Humanoid.Damage 패치에서 구현됨
/// </summary>
// 치명타 계산 후 적용
if (weaponType == Skills.SkillType.Knives)
{
    int lv = SkillTreeManager.Instance.GetSkillLevel("knife_stagger");

    // ⚠️ IsStaggering() 검증 필요
    if (lv > 0 && __instance.IsStaggering())
    {
        float staggerBonus = SkillEffect.GetKnifeStaggerBonus(0f);  // 30%
        float bonus = 1f + (staggerBonus / 100f);  // 1.3배
        hit.m_damage.m_pierce *= bonus;

        Plugin.Log.LogInfo($"[단검 기절] 기절 추가 피해! +{staggerBonus}%");
    }
}
```

**비틀거림 지속시간 증가** (참고, 미구현):
```csharp
/// <summary>
/// 비틀거림 지속시간 증가
/// Character.Stagger() 패치 필요 (검증 필요)
/// </summary>
[HarmonyPatch(typeof(Character), nameof(Character.Stagger))]
public static class Character_Stagger_Duration_Patch
{
    public static void Postfix(Character __instance, Vector3 direction)
    {
        var player = Player.m_localPlayer;
        if (player == null) return;

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // 예: 비틀거림 지속시간 +50%
        if (manager.GetSkillLevel("your_stagger_skill") > 0)
        {
            // ⚠️ 비틀거림 타이머 필드 찾기 필요
            // Traverse를 통한 m_staggerTimer 조작 가능성 조사
            float bonus = YourConfig.StaggerDurationBonusValue / 100f;

            // 구현 예시 (필드명 확인 필요):
            // var traverse = Traverse.Create(__instance);
            // float currentTimer = traverse.Field("m_staggerTimer").GetValue<float>();
            // float newTimer = currentTimer * (1f + bonus);
            // traverse.Field("m_staggerTimer").SetValue(newTimer);
        }
    }
}
```

**비틀거림 확률 증가** (참고, 미구현):
```csharp
/// <summary>
/// 비틀거림 발생 확률 증가
/// Character.Damage Prefix에서 stagger 판정 조작 (고급)
/// </summary>
[HarmonyPatch(typeof(Character), nameof(Character.Damage))]
public static class Character_Stagger_Chance_Patch
{
    public static void Prefix(Character __instance, HitData hit)
    {
        // ⚠️ 비틀거림 판정 로직 조사 필요
        // hit.m_staggerMultiplier 같은 필드가 있을 가능성

        var attacker = hit.GetAttacker();
        if (attacker is not Player player) return;

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // 예: 비틀거림 확률 +20%
        if (manager.GetSkillLevel("your_stagger_skill") > 0)
        {
            float bonus = YourConfig.StaggerChanceBonusValue / 100f;

            // 구현 예시 (필드명 확인 필요):
            // hit.m_staggerMultiplier *= (1f + bonus);
        }
    }
}
```

---

## 🧪 검증 절차

### Rule 19 (비틀거림) 검증 가이드

**Phase 1: API 존재 확인**
```csharp
// 테스트 코드 추가 (임시)
[HarmonyPatch(typeof(Character), nameof(Character.Damage))]
public static class StaggerTest_Patch
{
    public static void Postfix(Character __instance)
    {
        try
        {
            // IsStaggering() 메서드 존재 확인
            bool isStaggering = __instance.IsStaggering();
            Plugin.Log.LogInfo($"[비틀거림 테스트] {__instance.GetHoverName()}: IsStaggering = {isStaggering}");
        }
        catch (System.Exception ex)
        {
            Plugin.Log.LogError($"[비틀거림 테스트] IsStaggering() 호출 실패: {ex.Message}");
        }
    }
}
```

**Phase 2: 게임 내 테스트**
1. 둔기로 적 공격 (비틀거림 유발)
2. 로그에서 `IsStaggering = true` 확인
3. 비틀거림 애니메이션과 IsStaggering() 값 일치 확인

**Phase 3: 추가 피해 검증**
1. knife_stagger 스킬 활성화
2. 단검으로 비틀거림 중인 적 공격
3. 피해량 30% 증가 확인 (로그)
4. 멀티플레이어 테스트

**Phase 4: 규칙 확정**
- ✅ 검증 완료 시: "⚠️" 마크 제거, "✅ 검증 완료" 추가
- ❌ 검증 실패 시: 대안 API 조사 또는 구현 방식 변경

---

## 📊 시스템 비교

### 에이트르 vs 스태미나 vs 체력

| 속성 | API | 패치 위치 | 방식 | 검증 |
|------|-----|-----------|------|------|
| 체력 | GetTotalFoodValue | Postfix (hp) | 고정값/비율 | ✅ |
| 스태미나 | GetTotalFoodValue | Postfix (stamina) | 고정값/비율 | ✅ |
| 에이트르 | GetTotalFoodValue | Postfix (eitr) | 고정값/비율 | ✅ |

**공통점**:
- 모두 `Player.GetTotalFoodValue()` 패치 사용
- 음식 보너스에 합산되는 방식
- MMO 시스템과 동일한 패턴

**차이점**:
- 에이트르는 마법 자원 (지팡이, 마법사)
- 스태미나는 행동 자원 (공격, 구르기, 점프)
- 체력은 생존 자원 (피해 흡수)

---

## 🚨 금지 사항

### ❌ 절대 하지 말 것

1. **GetMaxEitr() 직접 패치 금지**
   - GetTotalFoodValue로 처리 ✅
   - GetMaxEitr() 직접 수정 ❌

2. **미검증 API 무단 사용 금지**
   - IsStaggering() 검증 전 사용 ❌
   - 검증 완료 후 사용 ✅

3. **비틀거림 타이머 직접 조작 금지** (검증 전까지)
   - 필드명 확인 없이 Traverse 사용 ❌
   - 검증 후 안전한 방식으로만 ✅

4. **에이트르 하드코딩 금지**
   - Config 기반 값 사용 ✅
   - 하드코딩된 수치 ❌

---

## ✅ 권장 사항

### 1. 에이트르 스킬 개발

**Config 정의**:
```csharp
public static ConfigEntry<float> YourSkill_EitrBonus;

YourSkill_EitrBonus = config.Bind(
    "Your Tree",
    "YourSkill_에이트르보너스",
    20f,
    "스킬명(your_skill_id) - 에이트르 최대치 보너스"
);
```

**GetTotalFoodValue 패치 추가**:
```csharp
// SkillEffect.StatTree.cs의 Player_GetTotalFoodValue_Eitr_StatTree_Patch에 추가
if (manager.GetSkillLevel("your_skill_id") > 0)
{
    float bonus = YourConfig.YourSkillEitrBonusValue;
    eitrBonus += bonus;
    Plugin.Log.LogDebug($"[스킬] 에이트르: +{bonus}");
}
```

**툴팁 작성**:
```csharp
Description = $"에이트르 최대치 +{YourConfig.YourSkillEitrBonusValue}"
```

### 2. 비틀거림 스킬 개발 (검증 후)

**검증 완료 후 개발 순서**:
1. Config 정의
2. Humanoid.Damage 또는 Character.Damage 패치
3. IsStaggering() 체크 추가
4. 추가 효과 적용 (피해 증가, 특수 효과 등)
5. 게임 내 테스트
6. 멀티플레이어 테스트

### 3. 체크리스트

**에이트르 스킬 개발 체크**:
```yaml
에이트르_스킬_개발_체크:
  - [ ] Config 정의 완료?
  - [ ] GetTotalFoodValue Eitr 패치에 추가?
  - [ ] 고정값 vs 비율 명확히 구분?
  - [ ] 툴팁에 정확한 수치 표시?
  - [ ] 게임 내 테스트 완료?
  - [ ] Config-Tooltip-Effect 3단계 연동?
```

**비틀거림 스킬 개발 체크**:
```yaml
비틀거림_스킬_개발_체크:
  - [ ] IsStaggering() API 검증 완료?
  - [ ] 비틀거림 상태 정상 감지?
  - [ ] 추가 효과 정상 작동?
  - [ ] 멀티플레이어 동기화 확인?
  - [ ] 다양한 적 유형에서 테스트?
  - [ ] 성능 이슈 없는지 확인?
```

---

## 📚 Valheim API 참조

### Player.GetTotalFoodValue()
```csharp
public void GetTotalFoodValue(out float hp, out float stamina, out float eitr)
{
    // 음식으로부터 얻는 최대 체력, 스태미나, 에이트르 계산
    // 패치를 통해 스킬 보너스 합산 가능
}
```

### Player.GetMaxEitr()
```csharp
public float GetMaxEitr()
{
    // 최대 에이트르 반환 (GetTotalFoodValue 결과 포함)
}
```

### Player.UseEitr()
```csharp
public void UseEitr(float eitr)
{
    // 에이트르 소모 (양수 값 전달)
}
```

### Player.AddEitr()
```csharp
public void AddEitr(float eitr)
{
    // 에이트르 증가/감소 (양수=증가, 음수=감소)
}
```

### Character.IsStaggering() ⚠️ (검증 필요)
```csharp
public bool IsStaggering()
{
    // 비틀거림 상태 여부 반환 (검증 필요)
    // 반환값: true = 비틀거림 중, false = 정상
}
```

---

## 🧪 테스트 체크리스트

### 에이트르 시스템 테스트 ✅
```yaml
테스트_에이트르:
  - [x] 스킬 투자 전후 최대 에이트르 수치 확인 (Tab키)
  - [x] 마법 사용 시 에이트르 소모 정상 작동
  - [x] 여러 에이트르 스킬 중첩 시 합산 확인
  - [x] 멀티플레이어 동기화 확인
  - [x] Config 값 변경 시 즉시 반영
```

### 비틀거림 시스템 테스트 ⚠️ (검증 필요)
```yaml
테스트_비틀거림:
  - [ ] IsStaggering() 메서드 존재 확인
  - [ ] 비틀거림 상태 정상 감지 (둔기 공격)
  - [ ] 추가 피해 정상 적용 (로그 확인)
  - [ ] 비틀거림 애니메이션과 API 일치
  - [ ] 다양한 적 유형에서 테스트 (그레이드워프, 트롤, 보스)
  - [ ] 멀티플레이어 동기화 확인
  - [ ] 성능 영향 측정 (FPS 저하 없는지)
```

---

**이 규칙을 준수하여 안정적인 에이트르 및 비틀거림 시스템을 개발하세요.**

**⚠️ 중요**: 비틀거림 시스템(Rule 19)은 IsStaggering() API 검증 완료 후 사용하세요!

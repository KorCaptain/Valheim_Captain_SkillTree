# CRITICAL_SYSTEM_RULES.md - 중앙화된 치명타 시스템 규칙

CaptainSkillTree 모드의 치명타 확률 및 피해를 중앙에서 관리하는 시스템 규칙입니다.

---

## 📚 규칙 목록

- **Rule 12**: 중앙화된 치명타 시스템 (Centralized Critical Hit System)

---

## Rule 12: 중앙화된 치명타 시스템

### 📋 목적
모든 무기의 치명타 확률 및 피해를 **중앙에서 관리**하여 일관성 확보

### 🎯 핵심 원칙
```
공통 보너스 + 무기별 보너스 = 최종 치명타 효과
```

---

## 치명타 시스템 아키텍처

### 📂 파일 위치
```
CaptainSkillTree/
└── SkillTree/
    └── CriticalSystem/
        ├── Critical.cs           (치명타 확률 계산)
        └── CriticalDamage.cs     (치명타 피해 계산)
```

### 핵심 파일

#### 1. Critical.cs - 치명타 확률 계산

**주요 함수**:
- `CalculateCritChance(player, weaponType)` - 무기별 치명타 확률 계산
- `RollCritical(critChance)` - 치명타 발생 여부 판정 (0-100 범위)
- `GetCommonCritChanceBonus(player)` - 모든 무기에 적용되는 공통 확률 보너스
- `GetXXXCritChance(player)` - 무기별 치명타 확률 계산 함수
  - `GetKnifeCritChance(player)` - 단검
  - `GetBowCritChance(player)` - 활
  - `GetSpearCritChance(player)` - 창
  - `GetCrossbowCritChance(player)` - 석궁
  - 기타 무기...

#### 2. CriticalDamage.cs - 치명타 피해 계산

**주요 함수**:
- `CalculateCritDamageMultiplier(player, weaponType)` - 무기별 치명타 피해 배수
- `ApplyCriticalDamage(player, hit, multiplier, weaponType)` - HitData에 치명타 피해 적용
- `GetCommonCritDamageBonus(player)` - 모든 무기에 적용되는 공통 피해 보너스
- `GetXXXCritDamage(player)` - 무기별 치명타 피해 계산 함수
  - `GetKnifeCritDamage(player)` - 단검
  - `GetBowCritDamage(player)` - 활
  - 기타 무기...

### 통합 위치

**Plugin.cs** - `WeaponCriticalSystemPatch` 패치가 `Character.ApplyDamage`에서 호출됩니다.

```csharp
[HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
public static class WeaponCriticalSystemPatch
{
    public static void Prefix(Character __instance, HitData hit)
    {
        // 치명타 시스템 통합 호출
        var player = hit.GetAttacker() as Player;
        if (player == null) return;

        // 1. 치명타 확률 계산
        float critChance = Critical.CalculateCritChance(player, weaponType);

        // 2. 치명타 판정
        if (Critical.RollCritical(critChance))
        {
            // 3. 치명타 피해 배수 계산
            float critMultiplier = CriticalDamage.CalculateCritDamageMultiplier(player, weaponType);

            // 4. 치명타 피해 적용
            CriticalDamage.ApplyCriticalDamage(player, hit, critMultiplier, weaponType);
        }
    }
}
```

---

## 공통 보너스 시스템 (모든 무기 자동 적용)

### 공격 전문가 트리 (Attack Tree) 보너스

모든 무기가 기본적으로 받는 치명타 보너스입니다.

#### Tier 4: 정밀 공격 (atk_crit_chance)
- **효과**: 치명타 확률 +5%
- **Config**: `SkillTreeConfig.AttackCritChanceValue`
- **함수**: `Critical.GetCommonCritChanceBonus()`

```csharp
public static float GetCommonCritChanceBonus(Player player)
{
    float bonus = 0f;

    // Tier 4: 정밀 공격 - 치명타 확률 +5%
    if (SkillEffect.HasSkill("atk_crit_chance"))
    {
        float tierBonus = SkillTreeConfig.AttackCritChanceValue;
        bonus += tierBonus;
        Plugin.Log.LogDebug($"[공통 치명타] Tier 4 정밀 공격: +{tierBonus}%");
    }

    return bonus;
}
```

#### Tier 6: 약점 공격 (atk_crit_dmg)
- **효과**: 치명타 피해 +7%
- **Config**: `SkillTreeConfig.AttackCritDamageBonusValue`
- **함수**: `CriticalDamage.GetCommonCritDamageBonus()`

```csharp
public static float GetCommonCritDamageBonus(Player player)
{
    float bonus = 0f;

    // Tier 6: 약점 공격 - 치명타 피해 +7%
    if (SkillEffect.HasSkill("atk_crit_dmg"))
    {
        float tierBonus = SkillTreeConfig.AttackCritDamageBonusValue;
        bonus += tierBonus;
        Plugin.Log.LogDebug($"[공통 치명타] Tier 6 약점 공격: +{tierBonus}%");
    }

    return bonus;
}
```

### 적용 방식

**자동 적용**: 모든 무기의 `GetXXXCritChance()` 및 `GetXXXCritDamage()` 함수가 **자동으로 공통 보너스를 먼저 호출**합니다.

---

## 무기별 구현 패턴

### 패턴 1: 기본 무기 (공통 보너스만 사용)

**용도**: 무기별 치명타 스킬이 아직 없는 경우

```csharp
public static float GetSwordCritChance(Player player)
{
    float bonus = 0f;

    // === 공통 보너스 (공격 전문가 트리) ===
    bonus += GetCommonCritChanceBonus(player);

    // TODO: 검 치명타 스킬 구현 시 추가

    return bonus;
}
```

**결과**:
- 공격 Tier 4 활성화 시: 치명타 확률 +5%
- 공격 Tier 6 활성화 시: 치명타 피해 +7%

---

### 패턴 2: 무기별 패시브 스킬 추가

**용도**: 무기별 고유 치명타 스킬이 있는 경우

```csharp
public static float GetSpearCritChance(Player player)
{
    float bonus = 0f;

    // === 공통 보너스 (공격 전문가 트리) ===
    bonus += GetCommonCritChanceBonus(player);

    // Tier 6: 꿰뚫는 창 - 치명타 확률 +12%
    if (SkillEffect.HasSkill("spear_Step5_penetrate"))
    {
        float tierBonus = 12f;
        bonus += tierBonus;
        Plugin.Log.LogDebug($"[치명타] Tier 6 꿰뚫는 창 (패시브): +{tierBonus}%");
    }

    if (bonus > 0f)
    {
        Plugin.Log.LogInfo($"[창 치명타] 총 확률: {bonus}%");
    }

    return bonus;
}
```

**결과**:
- 공통 보너스: +5% (공격 Tier 4)
- 무기 스킬: +12% (창 Tier 6)
- **총 치명타 확률**: +17%

---

### 패턴 3: 시간 기반 버프 스킬 (액티브 스킬)

**용도**: T/G/H/Y키로 활성화하는 시간 제한 버프

```csharp
public static float GetBowCritChance(Player player)
{
    float bonus = 0f;

    // === 공통 보너스 (공격 전문가 트리) ===
    bonus += GetCommonCritChanceBonus(player);

    // Tier 6: 크리티컬 부스트 - T키 액티브 (100% 치명타)
    if (SkillEffect.HasSkill("bow_Step6_critboost"))
    {
        if (SkillEffect.bowCritBoostEndTime.TryGetValue(player, out float endTime)
            && Time.time < endTime)
        {
            float tierBonus = SkillTreeConfig.BowStep6CritBoostCritChanceValue;
            bonus += tierBonus;
            float remainingTime = endTime - Time.time;
            Plugin.Log.LogDebug($"[치명타] Tier 6 크리티컬 부스트 (T키 액티브): +{tierBonus}% (남은 시간: {remainingTime:F1}초)");
        }
    }

    if (bonus > 0f)
    {
        Plugin.Log.LogInfo($"[활 치명타] 총 확률: {bonus}%");
    }

    return bonus;
}
```

**SkillEffect.cs에 추적 딕셔너리 추가**:
```csharp
// 시간 기반 버프 추적
public static Dictionary<Player, float> bowCritBoostEndTime = new Dictionary<Player, float>();

// 스킬 활성화 시
public static void ActivateBowCritBoost(Player player)
{
    float duration = SkillTreeConfig.BowStep6CritBoostDurationValue;
    bowCritBoostEndTime[player] = Time.time + duration;
    Plugin.Log.LogInfo($"[크리티컬 부스트] 활성화: {duration}초 동안 치명타 확률 +100%");
}
```

**결과**:
- 공통 보너스: +5% (공격 Tier 4)
- 액티브 스킬: +100% (활 Tier 6, 5초 동안)
- **총 치명타 확률**: +105% (사실상 100% 확정)

---

### 패턴 4: 치명타 비활성화 메커니즘 (트레이드오프)

**용도**: 치명타를 포기하고 다른 보너스를 얻는 스킬

```csharp
public static float GetCrossbowCritChance(Player player)
{
    // Tier 3: 정직한 한 발 - 치명타 확률 0% 고정 (치명타 비활성화, 대신 데미지 +35%)
    if (SkillEffect.HasSkill("crossbow_Step3_mark"))
    {
        Plugin.Log.LogDebug("[치명타] Tier 3 정직한 한 발: 치명타 비활성화 (0% 고정, 공통 보너스도 무시)");
        return 0f; // 치명타 완전 차단 (공통 보너스도 적용 안 됨)
    }

    float bonus = 0f;

    // === 공통 보너스 (공격 전문가 트리) ===
    bonus += GetCommonCritChanceBonus(player);

    return bonus;
}
```

**결과**:
- 스킬 활성화 시: 치명타 확률 0% (공통 보너스도 무시)
- 대신: 기본 데미지 +35% 보너스
- **트레이드오프**: 안정적인 데미지 vs. 확률 기반 치명타

---

## 현재 무기별 치명타 스킬 인벤토리

| 무기 | 치명타 확률 스킬 | 치명타 피해 스킬 | 총 보너스 (최대) |
|------|----------------|----------------|----------------|
| **단검** | 4개 (구버전+Tier5+Tier7+Tier9) | 3개 (구버전+Tier7+Tier9) | 확률: 높음, 피해: 매우 높음 |
| **활** | 4개 (Tier2+Tier5×2+Tier6) | 2개 (Tier5+Tier6) | 확률: 최대 100%, 피해: 높음 |
| **창** | 1개 (Tier6 꿰뚫는 창) | 공통 보너스만 | 확률: +12%, 피해: +7% |
| **석궁** | 치명타 비활성화 옵션 | 치명타 비활성화 옵션 | 트레이드오프 (데미지 +35%) |
| **검** | 공통 보너스만 | 공통 보너스만 | 확률: +5%, 피해: +7% |
| **둔기** | 공통 보너스만 | 공통 보너스만 | 확률: +5%, 피해: +7% |
| **폴암** | 공통 보너스만 | 공통 보너스만 | 확률: +5%, 피해: +7% |
| **지팡이** | 공통 보너스만 | 공통 보너스만 | 확률: +5%, 피해: +7% |

### 공통 보너스 (모든 무기 기본 적용)

- **치명타 확률**: +5% (공격 Tier 4)
- **치명타 피해**: +7% (공격 Tier 6)

---

## 새 치명타 스킬 추가 방법

### Step 1: Config 설정 추가 (해당 무기 Config 파일)

```csharp
// 예시: Sword_Config.cs
public static ConfigEntry<float> SwordExecutionCritChanceValue;
public static ConfigEntry<float> SwordExecutionCritDamageValue;

public static void Initialize(ConfigFile config)
{
    SwordExecutionCritChanceValue = config.Bind(
        "Sword Tree",
        "Tier7_처형자_치명타확률",
        15f,
        "Tier 7: 처형자(sword_step7_execution) - 치명타 확률 보너스 (%)"
    );

    SwordExecutionCritDamageValue = config.Bind(
        "Sword Tree",
        "Tier7_처형자_치명타피해",
        25f,
        "Tier 7: 처형자(sword_step7_execution) - 치명타 피해 보너스 (%)"
    );
}
```

---

### Step 2: Critical.cs에 스킬 로직 추가

```csharp
// Critical.cs - 치명타 확률 함수
public static float GetSwordCritChance(Player player)
{
    float bonus = 0f;

    // === 공통 보너스 (공격 전문가 트리) ===
    bonus += GetCommonCritChanceBonus(player);

    // Tier 7: 처형자 - 치명타 확률 +15%
    if (SkillEffect.HasSkill("sword_step7_execution"))
    {
        float tierBonus = Sword_Config.SwordExecutionCritChanceValue;
        bonus += tierBonus;
        Plugin.Log.LogDebug($"[치명타] Tier 7 처형자 (패시브): +{tierBonus}%");
    }

    if (bonus > 0f)
    {
        Plugin.Log.LogInfo($"[검 치명타] 총 확률: {bonus}%");
    }

    return bonus;
}

// CriticalDamage.cs - 치명타 피해 함수
public static float GetSwordCritDamage(Player player)
{
    float bonus = 0f;

    // === 공통 보너스 (공격 전문가 트리) ===
    bonus += GetCommonCritDamageBonus(player);

    // Tier 7: 처형자 - 치명타 피해 +25%
    if (SkillEffect.HasSkill("sword_step7_execution"))
    {
        float tierBonus = Sword_Config.SwordExecutionCritDamageValue;
        bonus += tierBonus;
        Plugin.Log.LogDebug($"[치명타 피해] Tier 7 처형자 (패시브): +{tierBonus}%");
    }

    if (bonus > 0f)
    {
        Plugin.Log.LogInfo($"[검 치명타 피해] 총 배수: {bonus}%");
    }

    return bonus;
}
```

---

### Step 3: 툴팁 업데이트 (해당 무기 SkillData 파일)

```csharp
// 예시: SwordSkillData.cs
new SkillNode
{
    Id = "sword_step7_execution",
    Name = "처형자",
    // Config 참조로 동적 연결
    Description = $"체력 30% 이하 적 처형 확률 증가, 치명타 확률 +{Sword_Config.SwordExecutionCritChanceValue}%, 치명타 피해 +{Sword_Config.SwordExecutionCritDamageValue}%",
    Tier = 7,
    RequiredPoints = 1
}
```

---

### Step 4: 시간 기반 버프인 경우 SkillEffect.cs에 추적 딕셔너리 추가

**액티브 스킬만 해당** (패시브 스킬은 불필요)

```csharp
// SkillEffect.cs - 시간 기반 버프 추적
public static Dictionary<Player, float> swordExecutionEndTime = new Dictionary<Player, float>();

// 스킬 활성화 함수
public static void ActivateSwordExecution(Player player)
{
    float duration = Sword_Config.SwordExecutionDurationValue;
    swordExecutionEndTime[player] = Time.time + duration;
    Plugin.Log.LogInfo($"[처형자] 활성화: {duration}초 동안 치명타 확률 +{Sword_Config.SwordExecutionCritChanceValue}%");
}

// Critical.cs에서 시간 체크
if (SkillEffect.HasSkill("sword_step7_execution"))
{
    if (SkillEffect.swordExecutionEndTime.TryGetValue(player, out float endTime)
        && Time.time < endTime)
    {
        float tierBonus = Sword_Config.SwordExecutionCritChanceValue;
        bonus += tierBonus;
        float remainingTime = endTime - Time.time;
        Plugin.Log.LogDebug($"[치명타] Tier 7 처형자 (액티브): +{tierBonus}% (남은 시간: {remainingTime:F1}초)");
    }
}
```

---

## 치명타 피해 적용 방식 (Rule 11 연동)

치명타 피해는 **물리 데미지 4종**에만 적용됩니다.

```csharp
// CriticalDamage.ApplyCriticalDamage()
public static void ApplyCriticalDamage(Player player, HitData hit, float critMultiplier, string weaponType)
{
    // 치명타 피해 배수 계산 (1 + bonus/100)
    float damageBonus = 1f + (critMultiplier / 100f);

    // === 물리 데미지 타입들 (전투용 - 4종, Rule 11 준수) ===
    if (hit.m_damage.m_pierce > 0) hit.m_damage.m_pierce *= damageBonus;   // 관통
    if (hit.m_damage.m_blunt > 0) hit.m_damage.m_blunt *= damageBonus;    // 블런트
    if (hit.m_damage.m_slash > 0) hit.m_damage.m_slash *= damageBonus;    // 베기
    if (hit.m_damage.m_chop > 0) hit.m_damage.m_chop *= damageBonus;     // 도끼질

    // ⚠️ m_pickaxe는 제외 (전투 스킬)
    // ⚠️ 속성 데미지(fire, frost, lightning, poison, spirit)는 제외

    Plugin.Log.LogInfo($"[치명타 피해] {weaponType} 치명타! 피해 배수: {damageBonus:F2}");
}
```

**Rule 11 준수**:
- 물리 4종 (pierce, blunt, slash, chop)에만 적용
- 속성 데미지는 치명타 피해 적용 제외
- Rule 11 "발헤임 데미지 타입 시스템" 참조

---

## 체크리스트

새 치명타 스킬 구현 시 다음을 확인하세요:

### Config 단계
- [ ] **Config 추가**: 해당 무기 Config 파일에 동적 설정값 추가
- [ ] **카테고리**: `"[Weapon] Tree"` 형식 사용
- [ ] **키**: `"TierX_스킬명_치명타확률/피해"` 형식 사용

### 로직 단계
- [ ] **Critical.cs**: `GetXXXCritChance()` 함수에 스킬 로직 구현
- [ ] **CriticalDamage.cs**: `GetXXXCritDamage()` 함수에 스킬 로직 구현
- [ ] **공통 보너스 호출**: `GetCommonCritChanceBonus()` 또는 `GetCommonCritDamageBonus()` 먼저 호출 확인
- [ ] **로그 추가**: 디버깅 로그 추가 (LogDebug, LogInfo)

### 툴팁 단계
- [ ] **Config 참조**: 툴팁에 `{Config.PropertyName.Value}` 형태로 수치 표시
- [ ] **간결함**: "치명타 확률 +15%, 치명타 피해 +25%" 형식

### 액티브 스킬 추가 단계 (해당 시)
- [ ] **딕셔너리 추가**: SkillEffect.cs에 `Dictionary<Player, float>` 추적 딕셔너리 추가
- [ ] **시간 체크 로직**: `Time.time < endTime` 조건 확인
- [ ] **활성화 함수**: 스킬 활성화 시 `endTime` 설정

### 검증 단계
- [ ] **치명타 피해**: 물리 4종에만 적용 (Rule 11 준수)
- [ ] **일관성**: 기존 무기 치명타 스킬 참고하여 패턴 유지
- [ ] **게임 테스트**: 실제 게임에서 치명타 확률 및 피해 확인

---

## 참고 구현 코드 위치

### 성공 사례

#### 단검 치명타
- **확률**: `Critical.cs` Lines 88-137
- **피해**: `CriticalDamage.cs` Lines 98-135
- **특징**: 4개 확률 스킬 + 3개 피해 스킬

#### 활 치명타
- **확률**: `Critical.cs` Lines 146-201
- **피해**: `CriticalDamage.cs` Lines 144-178
- **특징**: T키 액티브 스킬 (100% 치명타)

#### 창 치명타
- **확률**: `Critical.cs` Lines 258-279
- **특징**: 패시브 스킬 (꿰뚫는 창 +12%)

#### 석궁 치명타 비활성화
- **확률**: `Critical.cs` Lines 206-223
- **피해**: `CriticalDamage.cs` Lines 183-199
- **특징**: 트레이드오프 메커니즘 (치명타 0%, 데미지 +35%)

#### 공통 보너스
- **확률**: `Critical.cs` Lines 66-79
- **피해**: `CriticalDamage.cs` Lines 76-89
- **특징**: 모든 무기 자동 적용

---

## 🔗 관련 문서

- [DAMAGE_SYSTEM_RULES.md](DAMAGE_SYSTEM_RULES.md) - Rule 11 (발헤임 데미지 타입 시스템)
- [CONFIG_MANAGEMENT_RULES.md](CONFIG_MANAGEMENT_RULES.md) - Rule 7 (Config 연동)
- [SKILL_NAMING_RULES.md](SKILL_NAMING_RULES.md) - Rule 4, 5, 6 (스킬 ID 명명 규칙)
- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - 치명타 시스템 빠른 참조
- [CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 목차

---

**작성일**: 2025-01-29
**버전**: 1.0
**적용 범위**: Rule 12

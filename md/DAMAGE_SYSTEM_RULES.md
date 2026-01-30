# 데미지 시스템 규칙 (DAMAGE_SYSTEM_RULES.md)

**규칙 통합**: Rule 11 (Valheim 데미지 타입), Rule 11-1 (무기별 데미지 타입), Rule 13 (GetDamage 패치)
**기존 문서**: DAMAGE_INCREASE_RULES.md 통합

---

## 📋 목차
1. [Valheim 데미지 타입 시스템 (Rule 11)](#rule-11-valheim-데미지-타입-시스템)
2. [무기별 공격력 보너스 규칙 (Rule 11-1)](#rule-11-1-무기별-공격력-보너스-규칙)
3. [GetDamage 패치 시스템 (Rule 13)](#rule-13-getdamage-패치-시스템)
4. [MMO 독립성 원칙](#mmo-독립성-원칙)

---

## Rule 11: Valheim 데미지 타입 시스템

### 🎯 핵심 원칙
- 데미지 증가/감소 스킬은 **모든 관련 타입**에 적용 필수
- 일부 타입만 적용하면 특정 무기가 스킬 혜택을 못 받음
- 물리 5종 + 속성 5종 = 총 10종 데미지 타입

### 물리 데미지 타입 (5종)

| 타입 | C# 필드명 | 설명 | 사용 무기/공격 |
|------|-----------|------|---------------|
| **관통** | `m_pierce` | 뾰족한 무기로 찌르는 데미지 | 창, 화살, 크로스보우 볼트 |
| **블런트** | `m_blunt` | 둔기로 타격하는 데미지 | 둔기, 맨손, 몬스터 타격 |
| **베기** | `m_slash` | 날카로운 무기로 베는 데미지 | 검, 도끼, 나이프 |
| **도끼질** | `m_chop` | 도끼 특수 공격 데미지 | 도끼 (나무 베기) |
| **곡괭이** | `m_pickaxe` | 곡괭이 특수 공격 데미지 | 곡괭이 (광석 채굴) |

**전투 스킬 구현 시**:
- ✅ **권장**: pierce, blunt, slash, chop **4종 필수 적용**
- ⚠️ **선택적**: `m_pickaxe`는 몬스터가 사용하지 않으므로 전투 스킬에서 생략 가능
- ❌ **금지**: 일부 타입만 적용하여 특정 무기가 스킬 혜택을 못 받는 상황 방지

### 속성 데미지 타입 (5종)

| 타입 | C# 필드명 | 설명 | 주요 사용처 |
|------|-----------|------|------------|
| **불** | `m_fire` | 화염 속성 데미지 | 화염 화살, 용 브레스, 불 마법 |
| **냉기** | `m_frost` | 냉기 속성 데미지 | 서리 화살, 드레이크 브레스, 얼음 마법 |
| **번개** | `m_lightning` | 번개 속성 데미지 | 번개 화살, 천둥 마법 |
| **독** | `m_poison` | 독 속성 데미지 | 독 화살, 독 몬스터 공격 |
| **영혼** | `m_spirit` | 영혼 속성 데미지 | 영혼 무기, 언데드 공격 |

**속성 스킬 구현 시**:
- ✅ **필수**: 5종 모두 적용 (일부만 적용 시 불균형 발생)
- ✅ **일관성**: 속성 저항 증가/감소 스킬은 반드시 5종 모두 처리

### 코드 구현 패턴

#### 패턴 1: 물리 데미지 증가/감소 (전투용 - 4종)
```csharp
// ✅ 올바른 예시: 전투 스킬용 물리 데미지 처리
float multiplier = 1f + (bonusPercent / 100f);

hit.m_damage.m_pierce *= multiplier;   // 관통
hit.m_damage.m_blunt *= multiplier;    // 블런트
hit.m_damage.m_slash *= multiplier;    // 베기
hit.m_damage.m_chop *= multiplier;     // 도끼질

// ❌ m_pickaxe는 전투 스킬에서 생략 가능 (몬스터 미사용)
```

#### 패턴 2: 물리 데미지 전체 처리 (방어/회피용 - 5종)
```csharp
// ✅ 올바른 예시: 방어/회피 스킬용 전체 물리 데미지 처리
hit.m_damage.m_pierce *= reductionMultiplier;
hit.m_damage.m_blunt *= reductionMultiplier;
hit.m_damage.m_slash *= reductionMultiplier;
hit.m_damage.m_chop *= reductionMultiplier;
hit.m_damage.m_pickaxe *= reductionMultiplier;  // 회피 시에는 포함
```

#### 패턴 3: 속성 데미지 처리 (5종 필수)
```csharp
// ✅ 올바른 예시: 속성 데미지는 항상 5종 모두 처리
hit.m_damage.m_fire *= multiplier;      // 불
hit.m_damage.m_frost *= multiplier;     // 냉기
hit.m_damage.m_lightning *= multiplier; // 번개
hit.m_damage.m_poison *= multiplier;    // 독
hit.m_damage.m_spirit *= multiplier;    // 영혼
```

### 스킬 타입별 권장 처리 범위

| 스킬 타입 | 물리 데미지 | 속성 데미지 | 비고 |
|----------|-----------|-----------|------|
| **공격 증가** | 4종 (m_pickaxe 제외) | 5종 모두 | 전투용 |
| **방어/저항** | 5종 모두 | 5종 모두 | 방어용 |
| **회피/무적** | 5종 모두 | 5종 모두 | 특수 |
| **속성 특화** | - | 5종 모두 | 마법사 |

---

## Rule 11-1: 무기별 공격력 보너스 규칙

### 🎯 핵심 원칙
각 무기는 **주 데미지 타입에만** 공격력 보너스를 적용해야 함

### ⚠️ 일반적인 오류: 중첩 효과 (Stacking Bug)

**문제**: 공격력 보너스를 모든 데미지 타입에 적용하면 **의도하지 않은 중첩**이 발생합니다.

```csharp
// ❌ 잘못된 구현: 모든 타입에 적용
originalHit.m_damage.m_pierce += 4;
originalHit.m_damage.m_slash += 4;
originalHit.m_damage.m_blunt += 4;
originalHit.m_damage.m_chop += 4;
// 결과: 총 +16 효과! (의도: +4)

// ✅ 올바른 구현: 주 데미지 타입에만 적용
originalHit.m_damage.m_pierce += 4;  // 창은 pierce만
// 결과: 의도한 대로 +4 효과
```

### 무기별 주 데미지 타입 매핑

| 무기 | 주 데미지 타입 | 파일 위치 | 비고 |
|------|--------------|----------|------|
| **창(Spear)** | `m_pierce` | `SkillEffect.MeleeSkills.cs` | 찌르기 무기 |
| **폴암(Polearm)** | `m_pierce` | `SkillEffect.MeleeSkills.cs` | 창과 동일 |
| **검(Sword)** | `m_slash` | `Sword_Skill.cs` | 베기 무기 |
| **둔기(Mace)** | `m_blunt` | `MaceSkills.cs` | 타격 무기 |
| **단검(Knife)** | `m_slash` | `Knife_Skill.cs` | 베기 무기 |
| **활(Bow)** | `m_pierce` | `SkillEffect.RangedSkills.cs` | 화살은 pierce |
| **석궁(Crossbow)** | `m_pierce` | `SkillEffect.RangedSkills.cs` | 볼트는 pierce |

**주의**: 과거에는 단검이 `pierce + slash` 모두 적용했으나, 사용자 요청에 따라 `slash`만 적용으로 변경되었습니다.

### 올바른 구현 패턴

#### 패턴 1: 덧셈 방식 (고정 수치 보너스)
```csharp
// 창 스킬: pierce만 적용
float bonusValue = SkillTreeConfig.SpearStep3PierceDamageBonusValue;
originalHit.m_damage.m_pierce += bonusValue;

// 검 스킬: slash만 적용
float bonusValue = Sword_Config.SwordExpertDamageValue;
hit.m_damage.m_slash += bonusValue;

// 둔기 스킬: blunt만 적용
float bonusValue = Mace_Config.MaceExpertDamageValue;
hit.m_damage.m_blunt += bonusValue;
```

#### 패턴 2: 곱셈 방식 (비율 보너스)
```csharp
// 창 스킬: pierce만 적용
float multiplier = 1f + (bonusPercent / 100f);
hit.m_damage.m_pierce *= multiplier;

// 검 스킬: slash만 적용
float multiplier = 1f + (bonusPercent / 100f);
hit.m_damage.m_slash *= multiplier;

// 단검 스킬: slash만 적용 (과거 pierce + slash에서 변경됨)
float multiplier = critDamageMultiplier;
hit.m_damage.m_slash *= multiplier;
```

---

## Rule 13: GetDamage 패치 시스템

### 🎯 핵심 원칙
`ItemData.GetDamage` 패치로 무기 공격력을 증가시켜 UI에 자동 반영

### GetDamage 패치 장점

**UI 자동 반영**:
- 인벤토리 툴팁에 증가된 공격력 수치가 자동 표시됨
- Valheim 기본 UI 시스템이 자동으로 계산하여 표시
- MMO 스탯 연동 없이 독립적으로 작동
- 플레이어가 시각적으로 스킬 효과를 즉시 확인 가능

**호출 시점**:
- 아이템 툴팁 표시 시 (마우스 오버)
- 실제 데미지 계산 시 (공격 시)
- 성능 최적화를 위해 로그는 Debug 레벨로만 출력

### 패치 위치
**파일**: `SkillEffect.MeleeSkills.cs:1575-1766`

```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage),
              new[] { typeof(int), typeof(float) })]
public static class SkillTree_ItemData_GetDamage_MeleeExpert_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
    {
        // 무기별 스킬 보너스 적용
    }
}
```

### 구현 패턴

#### 패턴 1: 고정값 데미지 보너스
```csharp
// 단검 Tier 3: 빠른 공격 - 베기 데미지 +2 (고정값)
if (__instance.m_shared.m_skillType == Skills.SkillType.Knives)
{
    float knifeDamageBonus = Knife_Skill.GetKnifeAttackDamageBonus(player);
    if (knifeDamageBonus > 0)
    {
        if (__result.m_slash > 0) __result.m_slash += knifeDamageBonus;
        Plugin.Log.LogDebug($"[빠른 공격] 단검 베기 데미지 +{knifeDamageBonus}");
    }
}
```

#### 패턴 2: 비율 데미지 보너스
```csharp
// 단검 Tier 6: 치명적 피해 - 공격력 +25% (비율)
float combatDamageBonus = Knife_Skill.GetKnifeCombatDamageBonus(player);
if (combatDamageBonus > 0)
{
    float multiplier = 1f + (combatDamageBonus / 100f);
    __result.m_slash *= multiplier;
    __result.m_pierce *= multiplier;
    Plugin.Log.LogDebug($"[치명적 피해] 단검 공격력 +{combatDamageBonus}% (배수: {multiplier:F2}x)");
}
```

#### 패턴 3: 액티브 스킬 버프 적용
```csharp
// 단검 Tier 9: 암살자의 심장 (G키 액티브) - 피해 +50%
if (SkillEffect.IsKnifeAssassinHeartActive(player))
{
    float heartDamageBonus = Knife_Config.KnifeAssassinHeartDamageBonusValue / 100f;
    __result.m_slash *= (1f + heartDamageBonus);
    __result.m_pierce *= (1f + heartDamageBonus);
    Plugin.Log.LogDebug($"[암살자의 심장] 단검 데미지 +{Knife_Config.KnifeAssassinHeartDamageBonusValue}% (지속시간 중)");
}
```

#### 패턴 4: 여러 스킬 누적 보너스
```csharp
// 둔기 스킬: 여러 Tier 보너스 합산
float totalMaceBonus = 0f;

// Tier 1: 둔기 전문가 - 공격력 +10%
if (SkillEffect.HasSkill("mace_Step1_damage"))
{
    totalMaceBonus += Mace_Config.MaceStep1DamageBonusValue;
}

// Tier 3: 무거운 타격 - 공격력 +20%
if (SkillEffect.HasSkill("mace_Step3_branch_heavy"))
{
    totalMaceBonus += Mace_Config.MaceStep3HeavyDamageBonusValue;
}

// GetDamageHelper로 물리 데미지 보너스 적용
if (totalMaceBonus > 0)
{
    GetDamageHelper.ApplyPhysicalDamageBonus(ref __result, totalMaceBonus);
    Plugin.Log.LogInfo($"[둔기 스킬] 총 데미지 +{totalMaceBonus}%");
}
```

### GetDamageHelper 유틸리티 함수

**위치**: `SkillEffect.cs:1253-1319`

#### 물리 데미지 보너스 (전투용 4종)
```csharp
public static void ApplyPhysicalDamageBonus(ref HitData.DamageTypes damage, float bonusPercent)
{
    if (bonusPercent <= 0) return;
    float multiplier = 1f + (bonusPercent / 100f);

    if (damage.m_pierce > 0) damage.m_pierce *= multiplier;
    if (damage.m_blunt > 0) damage.m_blunt *= multiplier;
    if (damage.m_slash > 0) damage.m_slash *= multiplier;
    if (damage.m_chop > 0) damage.m_chop *= multiplier;
}
```

#### 고정값 데미지 추가
```csharp
public static void AddFixedDamage(ref HitData.DamageTypes damage, float value, params string[] types)
{
    if (value <= 0) return;

    foreach (var type in types)
    {
        switch (type.ToLower())
        {
            case "slash": if (damage.m_slash > 0) damage.m_slash += value; break;
            case "pierce": if (damage.m_pierce > 0) damage.m_pierce += value; break;
            case "blunt": if (damage.m_blunt > 0) damage.m_blunt += value; break;
            case "chop": if (damage.m_chop > 0) damage.m_chop += value; break;
            // ... 기타 타입
        }
    }
}
```

### 무기별 스킬 보너스 함수 패턴

**보너스 계산 함수** (예시: `Knife_Skill.cs:183-197`)
```csharp
/// <summary>
/// 빠른 공격 - 공격력 패시브 보너스
/// 실제 효과는 ItemData.GetDamage 패치에서 적용됨
/// </summary>
public static float GetKnifeAttackDamageBonus(Player player)
{
    if (!SkillEffect.HasSkill("knife_step4_attack_damage") || !IsUsingDagger(player))
        return 0f;

    try
    {
        float damageBonus = Knife_Config.KnifeAttackDamageBonusValue;
        Plugin.Log.LogDebug($"[빠른 공격] 공격력 +{damageBonus}");
        return damageBonus;
    }
    catch (System.Exception ex)
    {
        Plugin.Log.LogError($"[빠른 공격] 보너스 계산 실패: {ex.Message}");
        return 0f;
    }
}
```

### 구현된 무기별 공격력 보너스

| 무기 | 패치 위치 | 스킬 예시 | 타입 |
|------|---------|---------|------|
| **단검** | Line 1597-1637 | 빠른 공격 (+2), 치명적 피해 (+25%), 암살자의 심장 (+50%) | 고정값 + 비율 + 액티브 |
| **둔기** | Line 1639-1671 | 둔기 전문가 (+10%), 무거운 타격 (+20%), 공격력 강화 (+20%) | 누적 비율 |
| **창** | Line 1673-1712 | 창 전문가, 회피 찌르기, 연격창, 삼연창 | 누적 비율 |
| **폴암** | Line 1714-1747 | 제압 공격 (+30%), 폴암강화 (+5 고정) | 비율 + 고정값 |

### 성능 최적화 주의사항

**로그 레벨 구분**:
```csharp
// GetDamage는 자주 호출되므로 LogDebug 사용
Plugin.Log.LogDebug($"[스킬명] 보너스 +{value}");

// 최종 결과만 LogInfo로 출력
Plugin.Log.LogInfo($"[무기 스킬] 총 데미지 +{totalBonus}%");
```

**조건 체크 순서**:
```csharp
// 빠른 체크부터 먼저 수행
if (__instance?.m_shared == null) return;
if (player == null) return;
if (!SkillEffect.HasSkill("skill_id")) return;
if (!IsUsingWeapon(player)) return;

// 무거운 계산은 마지막
float bonus = CalculateComplexBonus();
```

---

## MMO 독립성 원칙

### 🎯 핵심 원칙
- **절대 금지**: MMO의 `getParameter` 패치를 통한 스탯 주입 (데이터 손실 위험)
- **필수 사용**: 직접 `HitData.m_damage` 조작 또는 `GetDamage` 패치

### ❌ 금지된 패턴

```csharp
// 절대 사용 금지 (PROHIBITED)
[HarmonyPatch(typeof(LevelSystem), nameof(LevelSystem.getParameter))]
public static class MMO_GetParameter_Patch
{
    static void Postfix(string paramName, ref int __result)
    {
        if (paramName == "Strength")
        {
            int atkBaseLv = SkillTreeManager.Instance.GetSkillLevel("atk_base");
            if (atkBaseLv > 0) __result += 2;  // ← MMO가 제어하므로 데이터 손실 위험
        }
    }
}
```

**문제점**:
- MMO 스탯 리셋 시 데이터 손실
- MMO 시스템과 충돌
- 의존성 증가

### ✅ 권장 패턴

#### 패턴 1: GetDamage 패치 (공격력 증가)
```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage))]
public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
{
    // 툴팁에도 반영되고 실제 데미지도 증가
    if (__result.m_slash > 0) __result.m_slash += bonusValue;
}
```

#### 패턴 2: Character.Damage 패치 (전투 데미지 조작)
```csharp
[HarmonyPatch(typeof(Character), nameof(Character.Damage))]
[HarmonyPrefix]
public static void Prefix(Character __instance, ref HitData hit)
{
    // MMO 계산 이후, 최종 데미지 적용 전
    if (hit.m_damage.m_pierce > 0)
    {
        float bonus = GetSkillBonus();
        hit.m_damage.m_pierce *= (1f + bonus);
    }
}
```

---

## 체크리스트

새 데미지 스킬 구현 시 다음을 확인하세요:

### Rule 11 체크리스트
- [ ] 물리 데미지: pierce, blunt, slash, chop 4종 적용 (공격용)
- [ ] 물리 데미지: pickaxe 포함 여부 결정 (방어/회피용은 포함)
- [ ] 속성 데미지: fire, frost, lightning, poison, spirit 5종 모두 적용
- [ ] 헬퍼 함수의 데미지 계산식도 동일한 타입 포함
- [ ] 기존 유사 스킬 코드 참고하여 일관성 유지

### Rule 11-1 체크리스트
- [ ] 무기별 주 데미지 타입 확인 (매핑 테이블 참조)
- [ ] 공격력 보너스는 주 데미지 타입에만 적용
- [ ] `Modify()` 메서드 사용 금지 (모든 타입에 적용됨)
- [ ] 로그 메시지에 적용된 데미지 타입 명시
- [ ] 기존 유사 무기 스킬 참조하여 일관성 유지

### Rule 13 체크리스트
- [ ] Config 파일에 동적 설정값 추가
- [ ] 보너스 계산 함수 작성 (무기 타입 체크 포함)
- [ ] GetDamage 패치에 스킬 로직 추가
- [ ] Rule 11-1 준수: 무기별 주 데미지 타입에만 적용
- [ ] GetDamageHelper 사용하여 일관된 패턴 유지
- [ ] 툴팁에 Config 참조 형태로 수치 표시
- [ ] LogDebug로 상세 로그, LogInfo로 최종 결과만 출력
- [ ] 기존 무기 스킬 참고하여 일관성 유지

### MMO 독립성 체크리스트
- [ ] MMO getParameter 패치 사용하지 않음
- [ ] GetDamage 또는 Character.Damage 패치 사용
- [ ] 조건부 적용: 해당 데미지 타입이 있을 때만 증가
- [ ] Config 기반 동적 관리 사용

---

## 참고 코드 위치

### Rule 11 참고 구현
- **성기사 패시브**: `JobSkills.cs:1065-1075` - 물리 4종 + 속성 5종
- **메이지 속성 저항**: `MageSkills.cs:502-506` - 속성 5종 감소
- **탱커 데미지 감소**: `Plugin.cs:152-161` - 물리 5종 + 속성 5종
- **회피 시스템**: `Plugin.cs:123-132` - 완전 회피 (10종)

### Rule 11-1 참고 구현
- **창 스킬**: `SkillEffect.MeleeSkills.cs:923-1336` - pierce 적용 (7개 스킬)
- **폴암 스킬**: `SkillEffect.MeleeSkills.cs:1356-1364` - pierce 적용
- **검 스킬**: `Sword_Skill.cs:932-938, 892-899` - slash 적용
- **둔기 스킬**: `MaceSkills.cs:183-188` - blunt 적용
- **단검 스킬**: `Knife_Skill.cs:123, 267, 413` - slash 적용 (과거 pierce + slash → slash만)

### Rule 13 참고 구현
- **단검 공격력**: `SkillEffect.MeleeSkills.cs:1597-1637`, `Knife_Skill.cs:183-232`
- **둔기 공격력**: `SkillEffect.MeleeSkills.cs:1639-1671`, `MaceSkills.cs`
- **창 공격력**: `SkillEffect.MeleeSkills.cs:1673-1712`
- **폴암 공격력**: `SkillEffect.MeleeSkills.cs:1714-1747`
- **Helper 함수**: `SkillEffect.cs:1253-1319`
- **생산 스킬 예시**: `SkillEffect.cs:1020-1057` (벌목/채광 효율)

---

## 참고 문서
- [CONFIG_MANAGEMENT_RULES.md](CONFIG_MANAGEMENT_RULES.md) - Config 연동 규칙
- [CRITICAL_SYSTEM_RULES.md](CRITICAL_SYSTEM_RULES.md) - 치명타 시스템
- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - 빠른 참조
- [CLAUDE.md](../CLAUDE.md) - 메인 규칙 파일

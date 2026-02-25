# MMO 시스템 연동 가이드

## MMO System Effect Application Analysis (MMO 시스템 효과 적용 분석)

### MMO getParameter 시스템 구조
```csharp
// 핵심: getParameter 메서드가 모든 스탯의 기준점
public int getParameter(Parameter parameter)
{
    if (!Player.m_localPlayer) return 0;
    if (!Player.m_localPlayer.m_knownTexts.ContainsKey($"{pluginKey}_{midleKey}_{parameter.ToString()}"))
        return 0;
    
    int value = int.Parse(Player.m_localPlayer.m_knownTexts[$"{pluginKey}_{midleKey}_{parameter.ToString()}"]);
    // 최대값 제한 적용
    int max = parameter.ToString() switch 
    {
        "Strength" => EpicMMOSystem.maxValueStrength.Value,
        "Agility" => EpicMMOSystem.maxValueDexterity.Value,
        "Body" => EpicMMOSystem.maxValueEndurance.Value,
        "Intellect" => EpicMMOSystem.maxValueIntellect.Value,
        "Vigour" => EpicMMOSystem.maxValueVigour.Value,
        "Special" => EpicMMOSystem.maxValueSpecial.Value
    };
    return Math.Min(value, max);
}
```

## Two-Tier Effect Application System

### Tier 1 (Preferred): MMO Stat Integration
```csharp
// Patch getParameter to increase MMO stats → MMO auto-applies effects
[HarmonyPatch] // EpicMMOSystem.LevelSystem.getParameter
[HarmonyPriority(Priority.High)]  // Modify MMO base values
public static void Postfix(object parameter, ref int __result)
{
    if (parameter?.ToString() == "Strength")
    {
        __result += skillTreeStrengthBonus; // MMO automatically calculates damage
    }
    if (parameter?.ToString() == "Body") // Endurance → Stamina
    {
        __result += skillTreeEnduranceBonus; // MMO automatically handles stamina
    }
    if (parameter?.ToString() == "Intellect") // Intelligence → Eitr
    {
        __result += skillTreeIntellectBonus; // MMO automatically handles eitr
    }
}
```

### Tier 2 (Special Cases): Direct Effect Patches
```csharp
// Only for effects that MMO doesn't handle or weapon-specific bonuses
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage))]
[HarmonyPriority(Priority.Low)]  // Execute after MMO
public static void Postfix(ref HitData.DamageTypes __result)
{
    // Only for special cases: weapon-specific, conditional bonuses
    float weaponSpecificBonus = GetWeaponSpecificBonus();
    __result.m_slash += weaponSpecificBonus;
}
```

## MMO Parameter 매핑 (명확한 역할 정의)
```csharp
public enum Parameter
{
    Strength = 0,  // 힘 - 물리 데미지, 운반 중량, 치명타 데미지
    Agility = 1,   // 민첩 - 공격 속도, 스태미나 소모량
    Intellect = 2, // 지능 - 마법 데미지, 에이트르 (최대 에이트르)
    Body = 3,      // 체구 - 스태미나 (최대 스태미나), 체력 재생, 물리 데미지 감소
    Vigour = 4,    // 활력 - 체력 (최대 체력), 체력 재생, 마법 데미지 감소
    Special = 5    // 특수 - 채굴 속도, 치명타 확률 등
}
```

**중요 구분:**
- **Vigour**: 체력(Health) 전용 - 최대 체력 증가
- **Body**: 스태미나(Stamina) 전용 - 최대 스태미나 증가
- **Intellect**: 에이트르(Eitr) 전용 - 최대 에이트르 증가

## 스탯별 효과 적용 패턴 분석

### 1. Vigour (체력) 시스템
```csharp
// 체력 증가: Player.GetTotalFoodValue 패치
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix(ref float hp)
{
    var addHp = Instance.getAddHp() + EpicMMOSystem.addDefaultHealth.Value;
    hp += addHp;  // 직접 가산
}

// 체력 재생: SEMan.ModifyHealthRegen 패치  
[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyHealthRegen))]
public static void Postfix(SEMan __instance, ref float regenMultiplier)
{
    if (__instance.m_character.IsPlayer())
    {
        float add = Instance.getAddRegenHp();
        regenMultiplier += add / 100;  // 백분율 보너스
    }
}

// 마법 데미지 감소: Character.RPC_Damage 패치
[HarmonyPatch(typeof(Character), nameof(Character.RPC_Damage))]
public static void Prefix(Character __instance, HitData hit)
{
    if (!__instance.IsPlayer()) return;
    float add = Instance.getAddMagicArmor();
    var value = 1 - add / 100;  // 감소 배율
    
    hit.m_damage.m_fire *= value;
    hit.m_damage.m_frost *= value;
    // ... 모든 속성 데미지 감소
}
```

### 2. Body (스태미나) 시스템
```csharp
// 스태미나 증가: Player.GetTotalFoodValue 패치
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix(ref float stamina)
{
    stamina += Instance.getAddStamina();  // 직접 가산
}

// 스태미나 재생: SEMan.ModifyStaminaRegen 패치
[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyStaminaRegen))]
public static void Postfix(SEMan __instance, ref float staminaMultiplier)
{
    if (__instance.m_character.IsPlayer())
    {
        float add = Instance.getStaminaRegen();
        staminaMultiplier += add / 100;  // 백분율 보너스
    }
}

// 물리 데미지 감소: Character.RPC_Damage 패치
[HarmonyPatch(typeof(Character), nameof(Character.RPC_Damage))]
public static void Prefix(Character __instance, HitData hit)
{
    float add = Instance.getAddPhysicArmor();
    var value = 1 - add / 100;
    
    hit.m_damage.m_blunt *= value;
    hit.m_damage.m_slash *= value;
    // ... 모든 물리 데미지 감소
}
```

### 3. Intellect (에이트르/마법) 시스템
```csharp
// 에이트르 증가: Player.GetTotalFoodValue 패치
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix(ref float eitr)
{
    if (eitr > 2 || EpicMMOSystem.addDefaultEitr.Value > 0f)
    {
        var addeitr = Instance.getAddEitr();
        eitr += addeitr + EpicMMOSystem.addDefaultEitr.Value;
    }
}

// 마법 데미지 증가: ItemDrop.ItemData.GetDamage 패치
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage))]
public static void Postfix(ref ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
{
    if (!Player.m_localPlayer.m_inventory.ContainsItem(__instance)) return;
    
    float add = Instance.getAddMagicDamage();
    var value = add / 100 + 1;  // 배율 계산 (1 + 보너스%)
    
    __result.m_fire *= value;
    __result.m_frost *= value;
    // ... 모든 속성 데미지 증가
}

// 에이트르 재생: SEMan.ModifyEitrRegen 패치
[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyEitrRegen))]
public static void Postfix(SEMan __instance, ref float eitrMultiplier)
{
    if (__instance.m_character.IsPlayer())
    {
        float add = Instance.getEitrRegen();
        eitrMultiplier += add / 100;
    }
}
```

### 4. Strength (힘) 시스템
```csharp
// 물리 데미지 증가: ItemDrop.ItemData.GetDamage 패치
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage))]
public static void Postfix(ref ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
{
    if (!Player.m_localPlayer.m_inventory.ContainsItem(__instance)) return;
    float add = Instance.getAddPhysicDamage();
    var value = add / 100 + 1;  // 배율 계산
    
    __result.m_blunt *= value;
    __result.m_slash *= value;
    __result.m_pierce *= value;
    __result.m_chop *= value;
    __result.m_pickaxe *= value;
}

// 운반 중량 증가: Player.GetMaxCarryWeight 패치
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxCarryWeight))]
public static void Postfix(ref float __result)
{         
    var addWeight = Instance.getAddWeight() + EpicMMOSystem.addDefaultWeight.Value;
    float hold = (float)Math.Round(addWeight);
    __result += hold;  // 직접 가산
}

// 방패 스태미나 소모 감소: SEMan.ModifyBlockStaminaUsage 패치
[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyBlockStaminaUsage))]
public static void Postfix(float baseStaminaUse, ref float staminaUse)
{
    staminaUse = staminaUse - ((Instance.getReducedStaminaBlock() / 100) * staminaUse);
}

// 치명타 데미지: Character.ApplyDamage 패치
[HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
public static void Prefix(Character __instance, ref bool showDamageText, ref HitData hit)
{
    if (hit.GetAttacker() == Player.m_localPlayer && UnityEngine.Random.Range(0f, 100f) < Instance.getAddCriticalChance())
    {
        float num2 = 1f + (Instance.getAddCriticalDmg() / 100f);
        // 모든 데미지 타입에 치명타 배율 적용
        hit.m_damage.m_blunt *= num2;
        // ... 모든 데미지 타입
    }
}
```

### 5. Agility (민첩) 시스템
```csharp
// 공격 스태미나 소모 감소: Attack.GetAttackStamina 패치
[HarmonyPatch(typeof(Attack), nameof(Attack.GetAttackStamina))]
public static void Postfix(ref float __result)
{
    var multi = 1 - Instance.getAttackStamina() / 100;
    __result *= multi;  // 감소 배율
}

// 달리기 스태미나 소모 감소: SEMan.ModifyRunStaminaDrain 패치
[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyRunStaminaDrain))]
public static void Postfix(ref float drain)
{
    var multi = 1 - Instance.getStaminaReduction() / 100;
    drain *= multi;
}

// 점프 스태미나 소모 감소: SEMan.ModifyJumpStaminaUsage 패치
[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyJumpStaminaUsage))]
public static void Postfix(ref float staminaUse)
{
    var multi = 1 - Instance.getStaminaReduction() / 100;
    staminaUse *= multi;
}
```

## MMO 효과 적용 핵심 패턴 정리

### 패턴 1: 최대값 증가 (GetTotalFoodValue 패치)
- **타겟**: `Player.GetTotalFoodValue`
- **용도**: 체력, 스태미나, 에이트르 최대값 증가
- **방식**: `ref float` 파라미터에 직접 가산

### 패턴 2: 재생률 증가 (SEMan.Modify*Regen 패치)
- **타겟**: `SEMan.ModifyHealthRegen`, `ModifyStaminaRegen`, `ModifyEitrRegen`
- **용도**: 재생 속도 증가
- **방식**: `regenMultiplier += bonus / 100` (백분율 보너스)

### 패턴 3: 데미지 증가 (ItemData.GetDamage 패치)
- **타겟**: `ItemDrop.ItemData.GetDamage`
- **용도**: 무기 데미지 증가
- **방식**: `__result.m_damage *= (1 + bonus / 100)` (배율 적용)

### 패턴 4: 데미지 감소 (Character.RPC_Damage 패치)
- **타겟**: `Character.RPC_Damage`
- **용도**: 받는 데미지 감소
- **방식**: `hit.m_damage *= (1 - reduction / 100)` (감소 배율)

### 패턴 5: 소모량 감소 (SEMan.Modify*Usage 패치)
- **타겟**: `SEMan.ModifyBlockStaminaUsage`, `Attack.GetAttackStamina` 등
- **용도**: 스태미나 소모량 감소
- **방식**: `usage *= (1 - reduction / 100)` (감소 배율)

### 패턴 6: 운반 중량 (Player.GetMaxCarryWeight 패치)
- **타겟**: `Player.GetMaxCarryWeight`
- **용도**: 최대 운반 중량 증가
- **방식**: `__result += addWeight` (직접 가산)

## CaptainSkillTree 구현 권장사항

### 1. MMO getParameter 패치 방식 (Tier 1 - 최우선)
```csharp
[HarmonyPatch] // EpicMMOSystem.LevelSystem.getParameter
[HarmonyPriority(Priority.High)]
public static void Postfix(object parameter, ref int __result)
{
    if (parameter?.ToString() == "Vigour")
        __result += SkillTreeManager.Instance.GetVigourBonus();
    if (parameter?.ToString() == "Body") 
        __result += SkillTreeManager.Instance.GetBodyBonus();
    if (parameter?.ToString() == "Intellect")
        __result += SkillTreeManager.Instance.GetIntellectBonus();
    if (parameter?.ToString() == "Strength")
        __result += SkillTreeManager.Instance.GetStrengthBonus();
    if (parameter?.ToString() == "Agility")
        __result += SkillTreeManager.Instance.GetAgilityBonus();
    if (parameter?.ToString() == "Special")
        __result += SkillTreeManager.Instance.GetSpecialBonus();
}
```

### 2. 성능 최적화 패턴 (MMO에서 사용)
```csharp
// 빈번한 호출 메서드의 캐싱 패턴 (getAddWeight 참고)
private static int HoldValueTimes = 0;
private static float HoldValue = 0;

public float GetCachedValue()
{
    if (HoldValueTimes == 0)
    {
        HoldValue = CalculateExpensiveValue();
    }
    else
        return HoldValue;
        
    HoldValueTimes++;
    if (HoldValueTimes == 50) // 50회 호출마다 재계산
        HoldValueTimes = 0;
        
    return HoldValue;
}
```

### 3. 효과 적용 시점 제어
```csharp
// MMO 패턴: Player 체크 + 인벤토리 검증
if (Player.m_localPlayer == null) return;
if (!Player.m_localPlayer.m_inventory.ContainsItem(__instance)) return;

// MMO 패턴: IsPlayer() 체크
if (__instance.m_character.IsPlayer())
{
    // 효과 적용
}
```

### 4. 데미지 계산 공식 (MMO 표준)
```csharp
// 데미지 증가 (곱셈 방식)
var multiplier = bonusPercent / 100 + 1;  // 10% 보너스 = 1.1 배율
damage *= multiplier;

// 데미지 감소 (곱셈 방식)  
var reduction = 1 - reductionPercent / 100;  // 10% 감소 = 0.9 배율
damage *= reduction;

// 절대값 증가 (가산 방식)
value += flatBonus;  // 체력, 스태미나, 에이트르 등
```

## Implementation Priority Guidelines

### MMO 스탯 연동 방식 (Tier 1) 우선 사용
- 모든 기본 스탯 효과는 MMO getParameter 패치로 구현
- MMO의 검증된 계산 시스템을 활용하여 안정성 확보

### 직접 패치 (Tier 2)는 특수한 경우에만 사용
- MMO가 지원하지 않는 효과만 직접 패치로 구현
- 무기별 특수 효과, 조건부 효과 등에 한정

### 체력 시스템 구현 우선순위 (필수 준수)
- **1순위 (권장)**: MMO Vigour 스탯 연동 (getParameter 패치, Priority.High)
- **2순위 (예외적)**: Character.GetMaxHealth 직접 패치 (Priority.Low, MMO 스탯 연동 실패 시만)
- **테스트 필수**: Vigour 스탯 연동이 정상 작동하는지 먼저 검증

### Standard Implementation (우선순위 순):
- **Health**: MMO Vigour stat integration (getParameter, Priority.High) - 필수 권장
- **Stamina**: MMO Body stat integration (getParameter, Priority.High) - Body는 스태미나 전용
- **Eitr**: MMO Intellect stat integration (getParameter, Priority.High)
- **Physical Damage**: MMO Strength stat integration (getParameter, Priority.High)
- **Elemental Damage**: MMO Intellect stat integration (getParameter, Priority.High)
- **Carry Weight**: MMO Strength stat integration (getParameter, Priority.High)

이 분석을 통해 CaptainSkillTree는 MMO 시스템과 완벽히 호환되는 방식으로 효과를 구현할 수 있습니다.

---

## 📚 **관련 문서**
- **🏠 메인 규칙**: [../CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 구조
- **🔐 보호 규칙**: [CORE_PROTECTION_README.md](CORE_PROTECTION_README.md) - 수정 금지 영역
- **🚨 빌드 오류**: [BUILD_ERRORS_GUIDE.md](BUILD_ERRORS_GUIDE.md) - Harmony 패치 오류 해결
- **🎯 개발 패턴**: [DEVELOPMENT_PATTERNS.md](DEVELOPMENT_PATTERNS.md) - MMO 연동 성공/실패 사례
- **⚡ 빠른 참조**: [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - MMO 패치 패턴 요약
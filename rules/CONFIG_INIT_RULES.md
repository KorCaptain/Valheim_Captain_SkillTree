# CONFIG_INIT_RULES.md — Config 초기화 규칙

> 트리거: config, SkillTreeConfig, Initialize, BindServerSync, 초기화 순서
> 상세 → `md/CONFIG_GUIDE.md`

---

## R1. Config 초기화 순서 (순서 변경 금지)

```
0. Captain Level System
   Skill_Tree_Base (Language → MoveSpeed_Max → AttackSpeed_Max)
   (구분선: "─ Attack, Speed, Production, Defense Trees ─")

1. 전문가 트리
   Attack_Config → Speed_Config → Defense_Config → Production_Config
   (구분선: "─── Ranged Expert Trees ────")

2. 원거리 무기 트리
   Bow_Config → Staff_Config → Crossbow_Config
   (구분선: "─── Melee Expert Trees ────")

3. 근접 무기 트리
   Knife_Config → Sword_Config → Mace_Config → Spear_Config → Polearm_Config
   (구분선: "─── Job Skill Trees ────")

4. 직업 트리 (반드시 최하단)
   Archer_Config → Mage_Config → Tanker_Config → Rogue_Config → Paladin_Config → Berserker_Config → Producer_Config
```

---

## R2. 구분선 규칙 (CRITICAL)

```csharp
// ✅ 올바름 - 각각 다른 section 이름
BindServerSync(config, "─ Atk, Spd, Production, Def Trees ─", "End", "", "");
BindServerSync(config, "─ Ranged Expert Trees ──────", "End", "", "");
BindServerSync(config, "─ Melee Expert Trees   ──────", "End", "", "");
BindServerSync(config, "─ Job Skill Trees ─────────", "End", "", "");
```

- 구분선은 `SkillTreeConfig.cs`에서만 추가 (개별 Config 파일 내부 금지)
- 각 구분선마다 고유한 section 이름 사용 (BepInEx는 같은 이름을 하나로 합침)
- ❌ 동일한 section 이름 반복 사용 금지

---

## R3. Config 파일 구조 패턴

```csharp
public static class {Weapon}_Config
{
    public static ConfigEntry<float> TierX_SkillName_Property;

    // Value 접근 시 반드시 GetEffectiveValue 사용
    public static float TierX_SkillName_PropertyValue =>
        (float)SkillTreeConfig.GetEffectiveValue("key", TierX_SkillName_Property?.Value ?? 기본값);

    public static void Initialize(ConfigFile config)
    {
        TierX_SkillName_Property = SkillTreeConfig.BindServerSync(config,
            "Weapon Tree",                           // 카테고리 (영어 고정)
            "TierX_SkillName_Property",              // 키 (영어 Tier 기반)
            기본값,
            SkillTreeConfig.GetConfigDescription("TierX_SkillName_Property")  // 자동 번역
        );
    }
}
```

```csharp
// ✅ 올바름 - Value 프로퍼티 사용
float damage = Sword_Config.RushSlash1stDamageRatioValue;

// ❌ 금지 - ConfigEntry 직접 접근
float damage = Sword_Config.RushSlash1stDamageRatio.Value;
```

---

## R4. 3종 세트 필수 (새 Config 키 추가 시)

| 항목 | 위치 |
|------|------|
| ① DisplayName (키 이름) | `ConfigTranslations.cs` → `GetKoreanKeyNames()` / `GetEnglishKeyNames()` |
| ② Description (마우스오버 설명) | `ConfigTranslations.cs` → `GetDescriptionTranslations()` |
| ③ GetConfigDescription() 호출 | `*_Config.cs` → `BindServerSync()` description 파라미터 |

❌ 하나라도 빠지면 F1 Config Manager에서 번역이 깨짐

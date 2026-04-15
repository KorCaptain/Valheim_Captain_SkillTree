# ARMOR_BLOCK_SYSTEM_RULES.md - 방어력 및 방패 시스템 규칙

## 📋 개요

**목적**: Valheim 방어 시스템의 두 가지 독립적인 요소(갑옷 방어력 vs 방패 방어력) 명확히 구분 및 구현

**중요성**:
- **방어력(Armor)**: 피해 감소 계산에 사용 (투구, 가슴, 다리 등의 갑옷)
- **방패 방어력(Block Power)**: 방패로 막을 때 흡수 가능한 데미지량

**적용 범위**: 모든 방어 관련 스킬, 특히 방어 전문가 트리

---

## Rule 17: 방어력(Armor) vs 방패 방어력(Block Power) 구분

### Rule 17-1: 방어력 (Armor) 시스템

#### 개요
- **정의**: 투구, 가슴갑옷, 다리 방어구 등에서 제공하는 피해 감소 수치
- **Valheim API**: `Character.GetBodyArmor()`
- **용도**: 모든 피해에 대한 기본 방어 계산
- **패치 방식**: Postfix로 최종 방어력에 보너스 합산

#### 방어력 보너스 스킬 목록

| 스킬 ID | 이름 | 효과 | 방식 | 조건 |
|---------|------|------|------|------|
| defense_root | 방어 전문가 | 방어 +5 | 고정값 | 무조건 |
| defense_Step1_survival | 피부경화 | 방어 +15 | 고정값 | 무조건 |
| defense_Step2_health | 체력단련 | 방어 +10 | 고정값 | 무조건 |
| defense_Step4_tanker | 바위피부 | 방어력 +12% | 비율 | 무조건 |
| defense_Step6_body | 요툰의 생명력 | 물리/마법 방어력 +10% | 비율 | 무조건 |
| mace_step3_guard | 둔기 방어 | 방어 +15% | 비율 | 둔기 착용 시 |
| mace_step6_grandmaster | 둔기 대가 | 방어 +20% | 비율 | 둔기 착용 시 |

#### 구현 패턴

**고정값 방어력 보너스**:
```csharp
[HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
public static class YourSkill_ArmorPatch
{
    [HarmonyPriority(Priority.Low)]  // MMO 이후 실행
    public static void Postfix(Character __instance, ref float __result)
    {
        if (__instance is not Player player) return;

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float armorBonus = 0f;

        // 스킬 체크
        if (manager.GetSkillLevel("your_skill_id") > 0)
        {
            armorBonus += YourConfig.ArmorBonusValue;  // 고정값
        }

        __result += armorBonus;
    }
}
```

**비율 방어력 보너스**:
```csharp
[HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
public static class YourSkill_ArmorPercentPatch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(Character __instance, ref float __result)
    {
        if (__instance is not Player player) return;

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float armorMultiplier = 1f;

        // 스킬 체크
        if (manager.GetSkillLevel("your_skill_id") > 0)
        {
            float bonus = YourConfig.ArmorBonusPercentValue / 100f;
            armorMultiplier += bonus;  // 1.12 = 12% 증가
        }

        __result *= armorMultiplier;
    }
}
```

**무기별 조건부 방어력**:
```csharp
// 예: 둔기 착용 시만 방어력 보너스
if (manager.GetSkillLevel("mace_step3_guard") > 0 &&
    SkillEffect.IsUsingMace(player))
{
    float bonus = Mace_Config.GuardArmorBonusValue / 100f;
    armorMultiplier += bonus;
}
```

---

### Rule 17-2: 방패 방어력 (Block Power) 시스템

#### 개요
- **정의**: 방패로 공격을 막을 때 흡수 가능한 데미지량
- **Valheim API**: `ItemDrop.ItemData.GetBlockPower(int quality)`
- **용도**: 방패 막기 시 데미지 흡수 계산
- **패치 방식**: Postfix로 최종 막기력에 보너스 합산

#### ⚠️ 현재 구현 문제점 발견

**잘못 구현된 스킬**:
| 스킬 ID | 이름 | 툴팁 설명 | 실제 구현 | 문제점 |
|---------|------|-----------|----------|--------|
| defense_Step3_shield | 방패훈련 | "방패 방어력 +100" | `GetBodyArmor()` 패치 | ❌ 갑옷 방어력에 추가됨 |
| defense_Step5_parry | 막기달인 | "방패 방어력 +100" | `GetBodyArmor()` 패치 | ❌ 갑옷 방어력에 추가됨 |

**올바른 구현이 필요한 스킬**:
| 스킬 ID | 이름 | 의도된 효과 | 필요한 API |
|---------|------|-------------|-----------|
| defense_Step3_shield | 방패훈련 | 방패 막기력 +100 | `GetBlockPower()` |
| defense_Step5_parry | 막기달인 | 방패 막기력 +100 | `GetBlockPower()` |

#### 올바른 구현 패턴

**방패 방어력 보너스 (고정값)**:
```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetBlockPower))]
public static class ShieldBlockPower_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
    {
        // 방패가 아니면 무시
        if (__instance.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield)
            return;

        var player = Player.m_localPlayer;
        if (player == null) return;

        // 현재 장착한 방패가 맞는지 확인
        if (player.GetLeftItem() != __instance)
            return;

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float blockPowerBonus = 0f;

        // defense_Step3_shield: 방패훈련 (방패 방어력 +100)
        if (manager.GetSkillLevel("defense_Step3_shield") > 0)
        {
            blockPowerBonus += Defense_Config.ShieldTrainingBlockPowerBonusValue;
        }

        // defense_Step5_parry: 막기달인 (방패 방어력 +100)
        if (manager.GetSkillLevel("defense_Step5_parry") > 0)
        {
            blockPowerBonus += Defense_Config.ParryMasterBlockPowerBonusValue;
        }

        __result += blockPowerBonus;
    }
}
```

**방패 방어력 보너스 (비율)**:
```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetBlockPower))]
public static class ShieldBlockPowerPercent_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
    {
        if (__instance.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield)
            return;

        var player = Player.m_localPlayer;
        if (player == null) return;

        if (player.GetLeftItem() != __instance)
            return;

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float multiplier = 1f;

        // 예: 방패 방어력 +15%
        if (manager.GetSkillLevel("your_shield_skill") > 0)
        {
            float bonus = YourConfig.ShieldBonusPercentValue / 100f;
            multiplier += bonus;
        }

        __result *= multiplier;
    }
}
```

---

### Rule 17-3: 방패 관련 추가 시스템

#### 패링(Parry) 지속시간
- **정의**: 방패로 막기를 성공한 후 반격 가능한 시간
- **Valheim API**: `Humanoid.BlockAttack()` 또는 `m_blockTimer` 필드
- **구현 위치**: Postfix에서 타이머 연장

**현재 구현된 스킬**:
| 스킬 ID | 이름 | 효과 |
|---------|------|------|
| defense_Step5_parry | 막기달인 | 패링 +1초 |

**구현 패턴** (참고, 아직 미구현):
```csharp
[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
public static class ParryDuration_Patch
{
    public static void Postfix(Humanoid __instance, HitData hit, Character attacker)
    {
        if (__instance is not Player player) return;
        if (!hit.m_statusEffect.StartsWith("Parry")) return;  // 패링 성공 시

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // defense_Step5_parry: 막기달인 (패링 +1초)
        if (manager.GetSkillLevel("defense_Step5_parry") > 0)
        {
            float bonus = Defense_Config.ParryMasterParryDurationBonusValue;
            // 패링 버프 지속시간 연장 로직 추가 필요
        }
    }
}
```

#### 방패 블럭 스태미나 감소
- **정의**: 방패로 막을 때 소모되는 스태미나 감소
- **Valheim API**: `Player.BlockAttack()` 내부의 스태미나 계산
- **구현 위치**: Prefix에서 스태미나 소모량 조정

**현재 구현된 스킬**:
| 스킬 ID | 이름 | 효과 |
|---------|------|------|
| defense_Step6_true | 요툰의 방패 | 방패 블럭 스태미나 -25% |

**구현 패턴** (참고, 아직 미구현):
```csharp
[HarmonyPatch(typeof(Player), nameof(Player.BlockAttack))]
public static class BlockStamina_Patch
{
    public static void Prefix(Player __instance, HitData hit)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        // defense_Step6_true: 요툰의 방패 (블럭 스태미나 -25%)
        if (manager.GetSkillLevel("defense_Step6_true") > 0)
        {
            float reduction = Defense_Config.JotunnShieldBlockStaminaReductionValue / 100f;
            // hit.m_blockable 또는 스태미나 계산 조정
            // 구현 세부사항 조사 필요
        }
    }
}
```

#### 방패 장착 시 이동속도
- **정의**: 방패를 들고 있을 때 이동속도 보너스
- **Valheim API**: `SE_Stats.m_speedModifier` (StatusEffect 방식)
- **구현 위치**: SE_StatTreeSpeed 또는 별도 StatusEffect

**현재 구현된 스킬**:
| 스킬 ID | 이름 | 효과 |
|---------|------|------|
| defense_Step6_true | 요툰의 방패 | 일반 방패 이동속도 +5%, 대형 방패 +10% |

**구현 패턴** (SE_Stats 사용):
```csharp
// SE_StatTreeSpeed.UpdateStatusEffect() 내부에 추가
public override void UpdateStatusEffect(float dt)
{
    float totalSpeedBonus = 0f;

    // defense_Step6_true: 요툰의 방패
    if (manager.GetSkillLevel("defense_Step6_true") > 0)
    {
        var leftItem = player.GetLeftItem();
        if (leftItem != null && leftItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
        {
            string shieldName = leftItem.m_shared.m_name;

            // 대형 방패 판별 (예: "Tower" 포함)
            if (shieldName.Contains("Tower") || shieldName.Contains("타워"))
            {
                totalSpeedBonus += Defense_Config.JotunnShieldTowerSpeedBonusValue / 100f;
            }
            else  // 일반 방패
            {
                totalSpeedBonus += Defense_Config.JotunnShieldNormalSpeedBonusValue / 100f;
            }
        }
    }

    m_speedModifier = totalSpeedBonus;  // SE_Stats에 적용
}
```

---

## 📊 현재 구현 상태 비교

### 올바르게 구현된 스킬

| 카테고리 | 스킬 ID | API | 상태 |
|---------|---------|-----|------|
| 갑옷 방어력 | defense_root ~ defense_Step2_health | GetBodyArmor() | ✅ 정상 |
| 갑옷 방어력 | defense_Step4_tanker | GetBodyArmor() | ✅ 정상 |
| 갑옷 방어력 | defense_Step6_body | GetBodyArmor() | ✅ 정상 |
| 갑옷 방어력 | mace_step3_guard, mace_step6_grandmaster | GetBodyArmor() | ✅ 정상 |

### 잘못 구현된 스킬

| 스킬 ID | 이름 | 툴팁 | 현재 API | 올바른 API | 수정 필요 |
|---------|------|------|----------|-----------|----------|
| defense_Step3_shield | 방패훈련 | "방패 방어력 +100" | GetBodyArmor() | GetBlockPower() | ⚠️ 수정 필요 |
| defense_Step5_parry | 막기달인 | "방패 방어력 +100" | GetBodyArmor() | GetBlockPower() | ⚠️ 수정 필요 |

### 미구현 스킬

| 스킬 ID | 이름 | 효과 | 필요한 구현 | 상태 |
|---------|------|------|------------|------|
| defense_Step5_parry | 막기달인 | 패링 +1초 | BlockAttack 패치 | ❌ 미구현 |
| defense_Step6_true | 요툰의 방패 | 블럭 스태미나 -25% | BlockAttack 패치 | ❌ 미구현 |
| defense_Step6_true | 요툰의 방패 | 방패 이동속도 보너스 | SE_Stats 또는 Speed.cs | ❌ 미구현 |

---

## 🚨 금지 사항

### ❌ 절대 하지 말 것

1. **API 혼동 금지**
   - 방패 관련 효과를 GetBodyArmor()에 추가 ❌
   - 갑옷 관련 효과를 GetBlockPower()에 추가 ❌

2. **툴팁과 실제 구현 불일치 금지**
   - 툴팁에 "방패 방어력"이라고 쓰면 반드시 GetBlockPower() 사용 ✅
   - 툴팁에 "방어력"이라고 쓰면 반드시 GetBodyArmor() 사용 ✅

3. **방패 체크 누락 금지**
   - GetBlockPower() 패치 시 반드시 ItemType.Shield 체크 ✅
   - 현재 장착한 방패인지 확인 (GetLeftItem()) ✅

4. **패링 시스템 직접 수정 금지**
   - Valheim 내부 패링 로직은 복잡하므로 충분한 조사 후 구현 ✅

---

## ✅ 권장 사항

### 1. 명확한 용어 사용
- **"방어력" 또는 "갑옷 방어력"**: Character.GetBodyArmor() 패치
- **"방패 방어력" 또는 "막기력"**: ItemDrop.ItemData.GetBlockPower() 패치
- **"패링 지속시간"**: Humanoid.BlockAttack() 패치
- **"블럭 스태미나"**: Player.BlockAttack() 스태미나 계산 조정

### 2. 툴팁 일관성
```csharp
// 좋은 예
Description = $"방어력 +{value}";  // GetBodyArmor() 사용
Description = $"방패 방어력 +{value}";  // GetBlockPower() 사용

// 나쁜 예
Description = $"방패 방어력 +{value}";  // 실제로는 GetBodyArmor() 사용 ❌
```

### 3. Config 명명 규칙
```csharp
// 갑옷 방어력
public static ConfigEntry<float> YourSkill_ArmorBonus;

// 방패 방어력
public static ConfigEntry<float> YourSkill_BlockPowerBonus;

// 패링
public static ConfigEntry<float> YourSkill_ParryDurationBonus;

// 블럭 스태미나
public static ConfigEntry<float> YourSkill_BlockStaminaReduction;
```

### 4. 구현 전 체크리스트
```yaml
방어_관련_스킬_구현_체크:
  - [ ] 효과가 갑옷 방어력인가? → GetBodyArmor()
  - [ ] 효과가 방패 방어력인가? → GetBlockPower()
  - [ ] 효과가 패링인가? → BlockAttack() 조사
  - [ ] 효과가 블럭 스태미나인가? → BlockAttack() 조사
  - [ ] 툴팁과 실제 구현이 일치하는가?
  - [ ] Config-Tooltip-Effect 3단계 연동 완료?
  - [ ] 무기/방패 장착 조건 체크 추가?
```

### 5. 기존 잘못된 구현 수정 순서
```yaml
defense_Step3_shield_수정:
  1. SkillEffect.DefenseTree.cs의 GetBodyArmor 패치에서 제거
  2. 새로운 GetBlockPower 패치 생성
  3. 방패 장착 체크 추가
  4. 게임 내 테스트

defense_Step5_parry_수정:
  1. SkillEffect.DefenseTree.cs의 GetBodyArmor 패치에서 제거
  2. 새로운 GetBlockPower 패치 생성 (방패 방어력)
  3. BlockAttack 패치 추가 (패링 지속시간)
  4. 게임 내 테스트
```

---

## 📚 Valheim API 참조

### Character.GetBodyArmor()
```csharp
public float GetBodyArmor()
{
    // 투구, 가슴, 다리 등 모든 갑옷의 방어력 합산
    // 반환값: 총 방어력 (float)
}
```

### ItemDrop.ItemData.GetBlockPower(int quality)
```csharp
public float GetBlockPower(int quality)
{
    // 방패의 막기력 계산 (품질 고려)
    // 반환값: 방패 막기력 (float)
}
```

### Player.GetLeftItem()
```csharp
public ItemDrop.ItemData GetLeftItem()
{
    // 왼손에 든 아이템 (방패) 반환
    // 반환값: ItemDrop.ItemData (null 가능)
}
```

---

## 🧪 테스트 체크리스트

### 갑옷 방어력 테스트
```yaml
테스트_갑옷_방어력:
  - [ ] 스킬 투자 전후 캐릭터 방어력 수치 확인 (Tab키)
  - [ ] 피해를 받았을 때 감소량 비교
  - [ ] 다른 갑옷으로 교체 시 보너스 유지
  - [ ] 멀티플레이어 동기화 확인
```

### 방패 방어력 테스트
```yaml
테스트_방패_방어력:
  - [ ] 스킬 투자 전후 방패 막기력 수치 확인
  - [ ] 방패로 막았을 때 데미지 흡수량 비교
  - [ ] 방패를 벗으면 보너스 사라지는지 확인
  - [ ] 다른 방패로 교체 시 보너스 적용 확인
  - [ ] 방패 없이 맨손일 때 보너스 없는지 확인
```

### 패링 시스템 테스트
```yaml
테스트_패링:
  - [ ] 패링 성공 시 반격 가능 시간 측정
  - [ ] 스킬 투자 전후 시간 차이 확인
  - [ ] 패링 실패 시 효과 없는지 확인
```

### 블럭 스태미나 테스트
```yaml
테스트_블럭_스태미나:
  - [ ] 방패로 막을 때 스태미나 소모량 비교
  - [ ] 스킬 투자 전후 소모량 차이 확인
  - [ ] 강한 공격 막을 때 감소 효과 확인
```

---

**이 규칙을 준수하여 방어력과 방패 방어력을 명확히 구분하고 정확하게 구현하세요.**

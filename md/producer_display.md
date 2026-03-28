# producer_display.md - 제작 전문가 툴팁 표시 규칙

## 📋 개요

제작 전문가(Producer) 인챈트/버프 효과를 무기·방어구 아이템 툴팁에 표시하는 규칙.
중복 표시 없이 **제작 축복 라인 1개 + 수치 변환**으로 통합 표시.

---

## 🗂️ 구현 파일 4종 역할 분담

| 파일 | 역할 |
|------|------|
| `SkillTree/ProducerCrafting.cs` | 인챈트 적용(실제 효과) + ✨ 아이콘 추가만. **출처 라인 추가 금지** |
| `SkillTree/SkillEffect.WeaponTooltip.cs` | 무기 인챈트 출처 라인 + 버프 출처 라인 |
| `SkillTree/SkillEffect.Attack_Tooltip_Display.cs` | 무기 데미지/공격속도 수치 변환 |
| `SkillTree/SkillEffect.ArmorTooltip.cs` | 방어구 방어력 수치 변환 + 인챈트 출처 라인 |

---

## ⚙️ 인챈트 타입별 처리 규칙

### EnchantType 정의 (ProducerCrafting.cs)
```csharp
public enum EnchantType { None=0, WeaponDmg=1, Armor=2, MaxHP=3, WeaponSpd=4, MaxStamina=5 }
```

### 무기 인챈트

| 타입 | 수치 변환 파일 | 수치 변환 방식 | 출처 라인 파일 |
|------|-------------|-------------|-------------|
| WeaponDmg | `Attack_Tooltip_Display.cs` | `physPct += enchantVal; elemPct += enchantVal;` (CollectBonuses) | `WeaponTooltip.cs` |
| WeaponSpd | `Attack_Tooltip_Display.cs` | `displayAtkSpd = b.AttackSpeed + b.ProducerEnchantSpd;` (AppendExtraStats) | `WeaponTooltip.cs` |

**WeaponDmg 수치 변환 위치** (SkillEffect.Attack_Tooltip_Display.cs):
```csharp
// CollectBonuses 함수 하단 (약 370번대)
var pEnchantType = ProducerCrafting.GetEnchantType(item);
if (pEnchantType == ProducerCrafting.EnchantType.WeaponDmg)
{
    float pEnchantVal = ProducerCrafting.GetEnchantValue(item);
    physPct += pEnchantVal;
    elemPct += pEnchantVal;
    b.ProducerEnchantPct = pEnchantVal;  // AppendExtraStats 중복표시 방지용
}
```

**WeaponSpd 수치 변환 위치** (SkillEffect.Attack_Tooltip_Display.cs AppendExtraStats):
```csharp
// 스킬 속도 + WeaponSpd 인챈트 합산 표시
float displayAtkSpd = b.AttackSpeed + b.ProducerEnchantSpd;
if (displayAtkSpd > 0.01f)
    result += $"\n<color={COL_ATK_SPD}>⚡ {L.Get("weapon_effect_atk_spd")}: +{displayAtkSpd:F0}%</color>";
```

### 방어구 인챈트

| 타입 | 수치 변환 파일 | 수치 변환 방식 | 출처 라인 파일 |
|------|-------------|-------------|-------------|
| Armor | `ArmorTooltip.cs` | `totalPct = rockSkinPct + enchantPct;` → BuildLine에 전달 | `ArmorTooltip.cs` |
| MaxHP | 수치 변환 없음 | - | `ArmorTooltip.cs` |
| MaxStamina | 수치 변환 없음 | - | `ArmorTooltip.cs` |

---

## 📝 출처 라인 포맷

### 무기 (SkillEffect.WeaponTooltip.cs)

```csharp
// 버프 출처 (장인의 축복 활성 시에만)
if (ProducerSkills.IsProducerBuffActive(player))
{
    float atkBonus = Producer_Config.ProducerBuff_AttackBonusValue;
    if (atkBonus > 0f)
        __result += $"\n<color=#FF8C00>⚒️</color><color=#4FC3F7>{L.Get("weapon_effect_producer_buff")}</color> : {L.Get("weapon_stat_atk_power")} <color=orange>+{atkBonus:F0}%</color>";
}

// 인챈트 출처 (마법부여 아이템에 상시)
var enchantType = ProducerCrafting.GetEnchantType(item);
float enchantVal = ProducerCrafting.GetEnchantValue(item);
if (enchantVal > 0f)
{
    if (enchantType == ProducerCrafting.EnchantType.WeaponDmg)
        __result += $"\n<color=#FFD700>{L.Get("producer_enchant_weapon_dmg", $"{enchantVal:F1}")}</color>";
    else if (enchantType == ProducerCrafting.EnchantType.WeaponSpd)
        __result += $"\n<color=#FFD700>{L.Get("producer_enchant_weapon_spd", $"{enchantVal:F1}")}</color>";
}
```

### 방어구 (SkillEffect.ArmorTooltip.cs - Helmet/Chest/Legs 각 case)

```csharp
// 인챈트 출처 (마법부여 아이템에 상시) - 3종 타입 각각
if (enchantPct > 0f)
    bonusText += $"\n<color=#FFD700>{L.Get("producer_enchant_armor", $"{enchantPct:F1}")}</color>";
if (enchantHp > 0f)
    bonusText += $"\n<color=#FFD700>{L.Get("producer_enchant_hp", $"{enchantHp:F1}")}</color>";
if (enchantStaminaPct > 0f)
    bonusText += $"\n<color=#FFD700>{L.Get("producer_enchant_stamina", $"{enchantStaminaPct:F1}")}</color>";
```

---

## 🌐 로컬라이제이션 키 (DefaultLanguages_JobExpert.cs)

| 키 | 한국어 형식 | 영어 형식 |
|----|-----------|---------|
| `producer_enchant_weapon_dmg` | `✨ 제작 축복: 공격력 +{0}%` | `✨ Crafting Blessing: Attack +{0}%` |
| `producer_enchant_weapon_spd` | `✨ 제작 축복: 공격속도 +{0}%` | `✨ Crafting Blessing: ATK SPD +{0}%` |
| `producer_enchant_armor` | `✨ 제작 축복: 방어력 +{0}%` | `✨ Crafting Blessing: Armor +{0}%` |
| `producer_enchant_hp` | `✨ 제작 축복: 체력 +{0}%` | `✨ Crafting Blessing: Max HP +{0}%` |
| `producer_enchant_stamina` | `✨ 제작 축복: 스태미나 +{0}` | `✨ Crafting Blessing: Stamina +{0}` |
| `weapon_effect_producer_buff` | 장인의 축복 | Artisan's Blessing |

---

## 📊 표시 포맷 예시

### WeaponDmg 인챈트 무기 (스킬 없음)
```
관통: 131 (120 × +9%)        ← 인챈트 9% 포함 수치 변환
✨ 제작 축복: 공격력 +9.0%   ← 출처 라인 (WeaponTooltip.cs)
```

### WeaponDmg + 버프 활성
```
관통: 143 (120 × +19%)       ← 스킬 트리 보너스 + 인챈트 9% + 버프 10%
⚒️ 장인의 축복: 공격력 +10%  ← 버프 출처
✨ 제작 축복: 공격력 +9.0%   ← 인챈트 출처
```

### WeaponSpd 인챈트 무기
```
⚡ 공격속도: +8%              ← 스킬 속도 5% + 인챈트 3% 합산
✨ 제작 축복: 공격속도 +3.0%  ← 인챈트 출처
```

### Armor 인챈트 방어구
```
$item_armor: 27 (22 × +9%)   ← 인챈트 9% 포함 수치 변환 (BuildLine)
✨ 제작 축복: 방어력 +9.0%   ← 출처 라인 (ArmorTooltip.cs)
```

### MaxHP 인챈트 방어구
```
$item_armor: 22              ← 방어력 수치 변환 없음
✨ 제작 축복: 체력 +4.0%     ← 출처 라인
```

---

## ⚠️ 수정 시 핵심 규칙

### 1. 출처 라인 중복 금지
`ProducerCrafting.cs`의 `Producer_ItemData_GetTooltip_Patch`에서는 **출처 라인 추가 절대 금지**.
✨ 아이콘 추가(`lines[0] = $"<color=#FFD700>✨</color> {lines[0]}";`)만 허용.

### 2. 새 인챈트 타입 추가 시 체크리스트

```yaml
새_인챈트_추가_체크리스트:
  ProducerCrafting.cs:
    - [ ] EnchantType enum에 새 타입 추가
    - [ ] 인챈트 적용 패치(Postfix) 추가
    - [ ] GetEnchantDescription() switch 추가

  무기_인챈트_경우:
    - [ ] Attack_Tooltip_Display.cs CollectBonuses에 수치 반영 추가
    - [ ] WeaponBonuses struct에 필드 추가 (필요 시)
    - [ ] AppendExtraStats에 합산 표시 추가 (WeaponSpd 패턴 참고)
    - [ ] SkillEffect.WeaponTooltip.cs에 출처 라인 추가

  방어구_인챈트_경우:
    - [ ] ArmorTooltip.cs의 enchantPct/enchantHp/enchantStaminaPct 변수 읽기 추가
    - [ ] BuildLine 합산 추가 (Armor 타입이면 totalPct에 포함)
    - [ ] Helmet/Chest/Legs case 각각에 출처 라인 추가

  공통:
    - [ ] DefaultLanguages_JobExpert.cs에 7개 언어 키 추가
    - [ ] ru.json 동기화
    - [ ] Producer_Config.cs에 수치 Config 추가
    - [ ] ConfigTranslations_JobDesc.cs 번역 추가
```

### 3. 기존 인챈트 수치 변경 시

| 변경 대상 | 수정 파일 |
|----------|---------|
| 버프 공격력 % | `Producer_Config.cs` → `ProducerBuff_AttackBonus` |
| 인챈트 % 범위 | `Producer_Config.cs` → `GetEnchantWeaponDmgMin/Max_Lv*` |
| 출처 라인 텍스트 | `DefaultLanguages_JobExpert.cs` + `ru.json` |

### 4. 수치 변환 원칙
- 인챈트 % 값은 **스킬트리 보너스와 합산**하여 데미지/방어력 라인에 표시
- `producerDisplayPct`(버프 %)와 `b.ProducerEnchantPct`(인챈트 %)는 `AppendExtraStats`의 `displayPhysPct`에서 제외 (각각 별도 라인으로 표시되므로)

---

## 🔍 디버그 방법

```csharp
// ProducerCrafting.cs - 인챈트 확인
var t = ProducerCrafting.GetEnchantType(item);
var v = ProducerCrafting.GetEnchantValue(item);
Plugin.Log.LogInfo($"[제작 축복] 타입={t}, 값={v}");
```

---

## 🔗 관련 문서

| 문서 | 내용 |
|------|------|
| `md/Aattack_Tolltip_Display.md` | 무기 툴팁 전체 규칙 |
| `md/ARMOR_TOOLTIP_DISPLAY_RULES.md` | 방어구 툴팁 전체 규칙 |
| `md/DAMAGE_SYSTEM_RULES.md` | 데미지 계산 시스템 |
| `md/CONFIG_GUIDE.md` | Config 키 규칙 |

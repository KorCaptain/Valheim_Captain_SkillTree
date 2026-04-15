---
name: cst-producer-display
description: 제작 전문가(Producer) 인챈트/버프 툴팁 표시 규칙. 제작 축복 수치 변환·출처 라인 추가·수정 시 필수 참조. 트리거: producer enchant, 제작 축복, 제작 전문가 툴팁, crafting blessing, producer tooltip, weapon enchant display, armor enchant display
---

## 역할 분담 (절대 준수)

| 파일 | 역할 |
|------|------|
| `ProducerCrafting.cs` | 인챈트 실제 효과 패치 + ✨ 아이콘 추가만. **출처 라인 추가 금지** |
| `SkillEffect.WeaponTooltip.cs` | 무기 인챈트 출처 라인 + 버프(⚒️장인의 축복) 출처 라인 |
| `SkillEffect.Attack_Tooltip_Display.cs` | 무기 데미지/공격속도 수치 변환 |
| `SkillEffect.ArmorTooltip.cs` | 방어구 방어력 수치 변환 + 인챈트 출처 라인 |

---

## 인챈트 타입별 처리

```
EnchantType: None=0, WeaponDmg=1, Armor=2, MaxHP=3, WeaponSpd=4, MaxStamina=5
```

### 무기 WeaponDmg
- **수치 변환**: `Attack_Tooltip_Display.cs` CollectBonuses → `physPct += enchantVal; elemPct += enchantVal; b.ProducerEnchantPct = enchantVal;`
- **출처 라인**: `WeaponTooltip.cs` → `L.Get("producer_enchant_weapon_dmg", $"{enchantVal:F1}")`

### 무기 WeaponSpd
- **수치 변환**: `Attack_Tooltip_Display.cs` AppendExtraStats → `float displayAtkSpd = b.AttackSpeed + b.ProducerEnchantSpd;`
- **출처 라인**: `WeaponTooltip.cs` → `L.Get("producer_enchant_weapon_spd", $"{enchantVal:F1}")`

### 방어구 Armor
- **수치 변환**: `ArmorTooltip.cs` → `float totalPct = (rockSkinActive ? rockSkinPct : 0f) + enchantPct;` → BuildLine에 전달
- **출처 라인**: `ArmorTooltip.cs` → `L.Get("producer_enchant_armor", $"{enchantPct:F1}")`

### 방어구 MaxHP / MaxStamina
- **수치 변환 없음** (방어구 방어력 라인과 무관)
- **출처 라인**: `ArmorTooltip.cs` → `L.Get("producer_enchant_hp", ...)` / `L.Get("producer_enchant_stamina", ...)`

---

## 표시 포맷

```
[WeaponDmg 무기 + 버프 활성]
관통: 143 (120 × +19%)       ← 스킬 + 인챈트 + 버프 합산
⚒️ 장인의 축복: 공격력 +10%
✨ 제작 축복: 공격력 +9.0%

[WeaponSpd 무기]
⚡ 공격속도: +8%              ← 스킬 5% + 인챈트 3% 합산
✨ 제작 축복: 공격속도 +3.0%

[Armor 방어구]
$item_armor: 27 (22 × +9%)   ← 인챈트 9% 포함 수치 변환
✨ 제작 축복: 방어력 +9.0%

[MaxHP 방어구]
$item_armor: 22               ← 수치 변환 없음
✨ 제작 축복: 체력 +4.0%
```

---

## 새 인챈트 추가 체크리스트

### 무기 인챈트 추가 시
1. `ProducerCrafting.cs` - EnchantType enum 추가, 효과 패치 추가
2. `WeaponBonuses` struct - 추적용 float 필드 추가
3. `Attack_Tooltip_Display.cs` CollectBonuses - 수치 반영 (physPct/elemPct 또는 별도 필드)
4. `Attack_Tooltip_Display.cs` AppendExtraStats - 합산 표시 추가
5. `SkillEffect.WeaponTooltip.cs` - 출처 라인 추가 (enchantType 분기)

### 방어구 인챈트 추가 시
1. `ProducerCrafting.cs` - EnchantType enum 추가, 효과 패치 추가
2. `ArmorTooltip.cs` - enchant 변수 읽기 추가 (GetEnchantType 분기)
3. `ArmorTooltip.cs` - BuildLine에 수치 포함 (Armor 타입) 또는 별도 라인
4. `ArmorTooltip.cs` - Helmet/Chest/Legs 각 case에 출처 라인 추가

### 공통
5. `DefaultLanguages_JobExpert.cs` - 7개 언어 키 추가
6. `Localization/ru.json` - 동기화
7. `Producer_Config.cs` - 수치 범위 Config 추가
8. `ConfigTranslations_JobDesc.cs` - Config 번역 추가

---

## 핵심 주의사항

- `ProducerCrafting.cs`의 `Producer_ItemData_GetTooltip_Patch`에서 **출처 라인 추가 절대 금지** (중복 발생)
- `AppendExtraStats`의 `displayPhysPct`에서 `producerDisplayPct`와 `b.ProducerEnchantPct` 제외 필수 (각각 별도 라인으로 표시)
- 로컬라이제이션 키 형식: `L.Get("producer_enchant_XXX", $"{val:F1}")`
- `{0}` 자리에 `$"{val:F1}"` 전달 (소수점 1자리)

**전체 문서**: `md/producer_display.md`

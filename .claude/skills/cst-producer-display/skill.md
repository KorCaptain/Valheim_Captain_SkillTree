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
EnchantType: None=0, WeaponDmg=1, Armor=2, MaxHP=3, WeaponSpd=4, MaxStamina=5,
             BowCrit=6, CrossbowReload=7, CooldownReduce=8, DodgeRoll=9, MoveSpeed=10,
             Eitr=11, InvWeight=12, EitrRegen=13, JumpForce=14, BlockPower=15,
             FireProc=16, SpiritProc=17, PoisonProc=18, LightningProc=19,
             FrostProc=20, PolearmRange=21
```

### 무기군별 차별화 슬롯 (Sword/Axe/Mace/Spear/Polearm/Knife/Staff)

`GetSlotKey()`가 `item.m_shared.m_skillType` 기준으로 근접무기를 세분화(검=Sword, 도끼=Axe, 둔기=Mace, 창=Spear, 폴암=Polearm, 단검=Knife). 지팡이/완드(ElementalMagic·BloodMagic)는 `Staff` 슬롯으로 별도 분리(itemType 체크보다 우선 판정).

| 슬롯 | 신규 배정 | 비고 |
|------|----------|------|
| Sword/Axe | FireProc(16) | 검·도끼 공유 |
| Mace | SpiritProc(17) | |
| Spear | FrostProc(20) | 석궁과 공유 |
| Polearm | PolearmRange(21) + LightningProc(19) + FrostProc(20) | 비속성+속성 2종 혼합 |
| Knife | PoisonProc(18) | |
| Bow / Crossbow | + LightningProc(19) / + FrostProc(20) | 기존 BowCrit/CrossbowReload에 추가 |
| Staff | FireProc/PoisonProc/LightningProc/FrostProc 중 무작위 1개 | WeaponDmg/WeaponSpd 미배정(물리 데미지 필드 안 씀) |

**확률형(Proc) 메커니즘**: 피해량은 아이템 롤 값(표준 % 곱선) 그대로 사용하되, **발동 확률은 별도로 `cspt_enchant_level`(제작 당시 레벨)에 연동된 `Producer_Config.GetElementalProcChance(level)` 고정 테이블**(Lv1 25%→Lv5 45%, +5%p/레벨)에서 조회한다. 적용 파일: `ProducerCrafting_WeaponElement.cs`(신규, `Character.Damage` Prefix 1개, switch 분기). PolearmRange는 예외적으로 `SkillEffect.PolearmTree.cs`의 `GetTotalPolearmRangeBonus()`에 1줄만 추가하는 방식.

### 6~15번 처리 매핑 (ProducerCrafting_Effects.cs)

| ID | 이름 | 슬롯 | 출처 라인 |
|----|------|------|----------|
| 6 | BowCrit | Bow | `WeaponTooltip.cs` |
| 7 | CrossbowReload | Crossbow | `WeaponTooltip.cs` |
| 8 | CooldownReduce | Helmet | `ArmorTooltip.cs` Helmet case |
| 9 | DodgeRoll | Legs | `ArmorTooltip.cs` Legs case |
| 10 | MoveSpeed | Legs/Shield | `ArmorTooltip.cs` Legs case + Shield case |
| 11 | Eitr | Shoulder | `ArmorTooltip.cs` Shoulder case |
| 12 | InvWeight | Accessory | `ArmorTooltip.cs` Utility case |
| 13 | EitrRegen | Accessory | `ArmorTooltip.cs` Utility case |
| 14 | JumpForce | Accessory | `ArmorTooltip.cs` Utility case |
| 15 | BlockPower | Shield | `ArmorTooltip.cs` Shield case |

`ArmorTooltip.cs` 하나의 파일이 Helmet/Chest/Legs/Shoulder/Utility/Shield 6개 case를 모두 처리한다 (방어구 파일 = Helmet/Chest/Legs만이 아님).

### 슬롯 풀 배정 (Producer_Enchant.json slot_pools) — 의도적 배제 있음

| 슬롯 | 배정 | 배제 |
|------|------|------|
| Helmet | MaxHP, CooldownReduce, Armor | MaxStamina |
| Chest | MaxHP, Armor | MaxStamina |
| Legs | DodgeRoll, MoveSpeed, MaxStamina | Armor, MaxHP |
| Shoulder | MaxStamina, Eitr | Armor, MaxHP |
| Shield | BlockPower, MoveSpeed | Armor (BlockPower가 Armor와 수치 1:1 동일한 대체 스탯) |

새 슬롯에 Armor/MaxStamina를 배정하는 것은 `GetEquippedArmorEnchantTotal()`(4슬롯 전체 스캔)과 `ArmorTooltip.cs`의 Helmet/Chest/Legs case가 이미 지원하므로 JSON만 수정하면 되지만, 위 "배제" 열에 있는 조합은 의도적 설계이므로 사용자 승인 없이 추가하지 않는다. MaxHP는 적용 로직이 Helmet/Chest로 하드코딩돼 있어 확장 시 `ProducerCrafting.cs` 코드 수정이 선행되어야 한다.

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

## 인벤토리 마우스오버 표시 — 이미 자동 동작

출처 라인 패치는 `GetTooltip(item, ...)` 파라미터의 `item`(호버 중인 아이템 자체)에서 직접 읽으므로 **장착 여부와 무관**하게 표시된다. `ArmorTooltip.cs`의 `hasBonus` 게이트도 모든 enchant 플래그를 OR 조건에 포함하므로 스킬 미투자 캐릭터도 정상 표시. 기존 case(Helmet/Legs 등)가 해당 타입의 변수를 이미 읽고 있다면, 슬롯 풀에 타입만 추가해도 툴팁은 자동으로 뜬다 — 별도 코드 불필요.
악세사리/망토는 방어구와 달리 라인 교체가 아닌 "끝에 추가" 방식(`md/ACCESSORY_CAPE_TOOLTIP_DISPLAY_RULES.md` 참고).

**전체 문서**: `md/producer_display.md`

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

### EnchantType 정의 (ProducerCrafting.cs:23-41) — 전체 15종

```csharp
public enum EnchantType
{
    None           = 0,
    WeaponDmg      = 1,   // 무기: 공격력 +%
    Armor          = 2,   // 방어구: 방어력 +%
    MaxHP          = 3,   // 방어구: 체력 +%
    WeaponSpd      = 4,   // 일반무기: 공격속도 +%
    MaxStamina     = 5,   // 방어구: 스태미나 +%
    BowCrit        = 6,   // 활: 치명타 +%
    CrossbowReload = 7,   // 석궁: 재장전 속도 단축
    CooldownReduce = 8,   // 투구: 액티브 스킬 쿨타임 -%
    DodgeRoll      = 9,   // 각반: 회피 스태미나 소모 -%
    MoveSpeed      = 10,  // 각반/방패: 이동속도 +%
    Eitr           = 11,  // 망토: 에이트르 최대치 +flat
    InvWeight      = 12,  // 악세사리: 인벤 최대 무게 +flat
    EitrRegen      = 13,  // 악세사리: 에이트르 회복속도 +%
    JumpForce      = 14,  // 악세사리: 점프력 +%
    BlockPower     = 15,  // 방패: 가드 방어력 +%
    FireProc       = 16,  // 검/도끼/지팡이: 화염 피해 확률 +%
    SpiritProc     = 17,  // 둔기: 영혼 피해 확률 +%
    PoisonProc     = 18,  // 단검/지팡이: 독 피해 확률 +%
    LightningProc  = 19,  // 활/폴암/지팡이: 번개 피해 확률 +%
    FrostProc      = 20,  // 석궁/창/폴암/지팡이: 냉기 피해 확률 +%
    PolearmRange   = 21,  // 폴암: 공격 사거리 +%
}
```

### 6~15번 타입 적용/출처 라인 매핑 (ProducerCrafting_Effects.cs 및 개별 시스템 파일)

| ID | 이름 | 적용 슬롯 | 적용 파일(패치 대상) | 출처 라인 파일 (case) | 로컬라이제이션 키 |
|----|------|----------|---------------------|----------------------|------------------|
| 6 | BowCrit | Bow | `CriticalSystem/Critical.cs:196-197` | `WeaponTooltip.cs` | `producer_enchant_bow_crit` |
| 7 | CrossbowReload | Crossbow | `SkillEffect.SpeedTree.cs:391-399` (customData 직접 읽기) | `WeaponTooltip.cs` | `producer_enchant_crossbow_reload` |
| 8 | CooldownReduce | Helmet | `ProducerCrafting_Effects.cs:30-48` `ActiveSkillCooldownRegistry.SetCooldown` Prefix | `ArmorTooltip.cs` Helmet case | `producer_enchant_cooldown_reduce` |
| 9 | DodgeRoll | Legs | `ProducerCrafting_Effects.cs:57-76` `Character.GetEquipmentDodgeStaminaModifier` Postfix | `ArmorTooltip.cs` Legs case | `producer_enchant_dodge_roll` |
| 10 | MoveSpeed | Legs/Shield | `ProducerCrafting_Effects.cs:82-122` `GetJogSpeedFactor`/`GetRunSpeedFactor` Postfix | `ArmorTooltip.cs` Legs case + Shield case | `producer_enchant_move_speed` |
| 11 | Eitr | Shoulder | `ProducerCrafting_Effects.cs:156-172` `Player.GetTotalFoodValue` Postfix | `ArmorTooltip.cs` Shoulder case | `producer_enchant_eitr` |
| 12 | InvWeight | Accessory | `ProducerCrafting_Effects.cs:179-193` `Player.GetMaxCarryWeight` Postfix | `ArmorTooltip.cs` **Utility case** | `producer_enchant_inv_weight` |
| 13 | EitrRegen | Accessory | `ProducerCrafting_Effects.cs:201-215` `Player.GetEquipmentEitrRegenModifier` Postfix | `ArmorTooltip.cs` **Utility case** | `producer_enchant_eitr_regen` |
| 14 | JumpForce | Accessory | `ProducerCrafting_Effects.cs:223-259` `Character.Jump` Prefix+Postfix | `ArmorTooltip.cs` **Utility case** | `producer_enchant_jump_force` |
| 15 | BlockPower | Shield | `ProducerCrafting_Effects.cs:129-149` `ItemDrop.ItemData.GetBlockPower` Postfix | `ArmorTooltip.cs` **Shield case** | `producer_enchant_block_power` |

> 6~7번(무기 전용)만 `WeaponTooltip.cs`에서 처리되고, 8~15번은 전부 `ArmorTooltip.cs` 내부의 슬롯별 case(Helmet/Legs/Shoulder/Utility/Shield)에서 처리된다 — "방어구 파일 = Helmet/Chest/Legs만" 이라는 기존 통념이 아니라 Shoulder/Utility/Shield case까지 전부 이 한 파일에 있다는 점에 유의.

---

## ⚔️ 무기군별 차별화 마법부여 (16~21) — 속성 확률 데미지

기존에는 모든 근접무기(검/도끼/둔기/창/폴암/단검)가 `GetSlotKey()`에서 `"Weapon"` 하나로 뭉뚱그려져 동일한 풀(WeaponDmg/WeaponSpd)만 공유했다. 무기군별 정체성을 살리기 위해 `SkillTree/ProducerCrafting.cs:323-355` `GetSlotKey()`를 무기 스킬타입(`item.m_shared.m_skillType`) 기준으로 세분화했다.

### 무기군 → 슬롯 키 → 배정 속성

| 무기군 | `Skills.SkillType` | 슬롯 키 | 배정된 신규 효과 |
|--------|---------------------|---------|-------------------|
| 검 | Swords | `Sword` | FireProc(16) |
| 도끼 | Axes | `Axe` | FireProc(16) |
| 둔기 | Clubs | `Mace` | SpiritProc(17) |
| 창 | Spears | `Spear` | FrostProc(20) |
| 폴암 | Polearms | `Polearm` | PolearmRange(21) + LightningProc(19) + FrostProc(20) |
| 단검 | Knives (또는 프리팹/이름에 Dagger/Claw/Fist 포함) | `Knife` | PoisonProc(18) |
| 활 | Bows | `Bow` (기존) | + LightningProc(19) |
| 석궁 | Crossbows | `Crossbow` (기존) | + FrostProc(20) |
| 지팡이/완드 | ElementalMagic 또는 BloodMagic | `Staff` (신규) | FireProc/PoisonProc/LightningProc/FrostProc 4종 중 제작 시 무작위 1개 (가중치 동일 → 기존 `PickRandom()` 그대로 재사용) |

각 근접무기 슬롯은 기존 WeaponDmg(1)+WeaponSpd(4)도 그대로 유지한 채 신규 효과를 **추가** 배정한다 (교체 아님). `GetSlotKey()`는 스킬타입 미매칭 시 프리팹/아이템명에 `Dagger`/`Claw`/`Fist` 포함 여부로 단검 폴백 판정 후, 그래도 안 되면 기존처럼 `"Weapon"`(범용)으로 폴백한다. **Staff는 ElementalMagic/BloodMagic 체크가 itemType 체크보다 먼저 실행**되므로 지팡이의 실제 `m_itemType`(주로 `TwoHandedWeaponLeft`)과 무관하게 정확히 판정된다.

### 핵심 메커니즘: 발동 확률과 피해량의 분리

일반적인 %형 인챈트와 달리, FireProc/SpiritProc/PoisonProc/LightningProc/FrostProc 5종은 **"매 타격마다 확률적으로 발동"** 하는 방식이며, 발동 확률과 피해량이 서로 다른 소스에서 결정된다:

- **피해량**: 기존 아키텍처 그대로 — 아이템에 롤된 값(`cspt_enchant_value`, 표준 % 곱선 Lv1 3-4%~Lv5 11-15%)을 그 타격의 `hit.m_damage.GetTotalDamage()`에 곱해 해당 속성 필드에 가산.
- **발동 확률**: 아이템이 몇 레벨에서 롤됐는지에 연동된 **고정값**(`Producer_Config.GetElementalProcChance(level)`) — Lv1 25% → Lv2 30% → Lv3 35% → Lv4 40% → Lv5 45%(레벨당 +5%p), BepInEx Config로 조정 가능.
- 이를 위해 `m_customData`에 신규 키 **`cspt_enchant_level`**(제작 당시 제작 전문가 레벨 1~5)을 추가 저장한다. `ProducerCrafting.GetEnchantLevel(item)`으로 조회.

**적용 파일**: `SkillTree/ProducerCrafting_WeaponElement.cs` (신규) — `Character.Damage` Prefix 1개가 5종 전부 처리 (switch 분기). `Priority.Low`, 기존 `Producer_Enchant_WeaponDmg_Patch`와 동일 위치.

**PolearmRange(21)**: 속성이 아닌 예외 — `SkillEffect.PolearmTree.cs:38-66` `GetTotalPolearmRangeBonus()`(기존 `polearm_expert`/`polearm_step4_moon` 스킬 보너스 합산 함수)에 인챈트 항 1줄만 추가하는 방식으로 통합, 별도 Harmony 패치 불필요.

**툴팁**: `SkillEffect.WeaponTooltip.cs`의 `isWeapon` 판정에 `ItemType.TwoHandedWeaponLeft`(지팡이) 추가 필요 — 기존엔 OneHandedWeapon/TwoHandedWeapon/Bow만 허용해 지팡이 툴팁에 출처 라인이 전혀 표시되지 않았음(수정됨). 6종 신규 타입 모두 `WeaponTooltip.cs`의 기존 `else if (enchantType == ...)` 체인에 분기 추가.

**로컬라이제이션 완료 현황**:
- 인벤토리 마우스오버 출처 라인 6개 키(`producer_enchant_fire_proc` 등) — 7개 언어(KO/EN/DE/JA/PT_BR/ZH-CN/RU) 전부 반영 완료 (`DefaultLanguages_JobExpert.cs`, `_EN.cs`, `de/ja/pt_BR/zh-cn/ru.json`)
- `ElementalProcChance_Lv1~5` Config manager(F1) ② 설명(Description) + ① 표시명(KeyName) — 7개 언어 전부 반영 완료: `ConfigTranslations_JobDesc.cs`(KO+EN)/`_DE.cs`/`_CN.cs`/`_JP.cs`/`_RU.cs`/`_PTBR.cs` + `ConfigTranslations_KeyNames_KO.cs`/`_EN.cs`/`_DE_Part2.cs`/`_CN_Part2.cs`/`_JP_Part2.cs`/`_RU.cs`/`_PTBR_2.cs`. Config 2차 항목(KeyName)은 `MULTILANGUAGE_GUIDE.md` 13절의 "① DispName / ② Description / ③ GetConfigDescription() 호출" 3종 세트 규칙을 따름.

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

## 🎯 슬롯별 인챈트 풀 (Producer_Enchant.json `slot_pools`)

| 슬롯 | 배정된 타입 | 의도적으로 배제한 타입 |
|------|-----------|----------------------|
| Weapon (미분류 근접무기 폴백) | WeaponDmg(1), WeaponSpd(4) | - |
| Bow | WeaponDmg(1), BowCrit(6), **LightningProc(19)** | - |
| Crossbow | WeaponDmg(1), CrossbowReload(7), **FrostProc(20)** | - |
| Sword | WeaponDmg(1), WeaponSpd(4), **FireProc(16)** | - |
| Axe | WeaponDmg(1), WeaponSpd(4), **FireProc(16)** | - |
| Mace | WeaponDmg(1), WeaponSpd(4), **SpiritProc(17)** | - |
| Spear | WeaponDmg(1), WeaponSpd(4), **FrostProc(20)** | - |
| Polearm | WeaponDmg(1), WeaponSpd(4), **PolearmRange(21), LightningProc(19), FrostProc(20)** | - |
| Knife | WeaponDmg(1), WeaponSpd(4), **PoisonProc(18)** | - |
| Staff (지팡이/완드, ElementalMagic+BloodMagic) | **FireProc(16)/PoisonProc(18)/LightningProc(19)/FrostProc(20) 중 무작위 1개** | WeaponDmg/WeaponSpd — 엘리멘탈 무기는 물리 데미지 필드(blunt/slash/pierce)를 쓰지 않아 두 효과가 사실상 무효하므로 배정하지 않음 |
| Helmet | MaxHP(3), CooldownReduce(8), **Armor(2)** | MaxStamina — 투구는 스태미나 배제 |
| Chest | MaxHP(3), Armor(2) | MaxStamina — 흉갑은 스태미나 배제 |
| Legs | DodgeRoll(9), MoveSpeed(10), **MaxStamina(5)** | Armor, MaxHP — 각반은 방어력/체력 배제 |
| Shoulder | MaxStamina(5), Eitr(11) | Armor, MaxHP — 망토는 방어력/체력 배제 |
| Accessory | InvWeight(12), EitrRegen(13), JumpForce(14) | - |
| Shield | BlockPower(15), MoveSpeed(10) | Armor — `BlockPower`가 방패의 방어 스탯 역할(Armor와 레벨별 수치 1:1 동일: Lv1 3-4%, Lv2 5-6%, Lv3 7-8%, Lv4 9-10%, Lv5 11-15%)을 대체하므로 별도로 Armor를 배정하지 않음 |

**설계 원칙**: 모든 방어구 슬롯이 { Armor, MaxHP, MaxStamina } 3종을 전부 갖는 대칭 구조가 아니라, **슬롯별로 의도적으로 배제한 스탯이 있다.** 새로 슬롯 풀을 확장할 때는 반드시 이 표를 먼저 확인하고, 배제 의도가 있는 조합(Helmet·Chest+MaxStamina, Legs+Armor/MaxHP, Shoulder+Armor)은 사용자 승인 없이 추가하지 않는다.

**JSON만으로 확장 가능한 경우**: Armor(2)/MaxStamina(5)의 적용 함수는 `GetEquippedArmorEnchantTotal()`(Helmet/Chest/Legs/Shoulder 4슬롯 전체 스캔)이고, `ArmorTooltip.cs`도 Helmet/Chest/Legs case가 이미 `enchantPct`/`enchantStaminaPct` 라인을 지원하므로, 이 두 타입을 위 4슬롯 중 아직 없는 곳에 추가하는 것은 `.cs` 수정 없이 `Producer_Enchant.json`의 `slot_pools`만 변경하면 된다. 단, **MaxHP(3)**는 적용 로직이 Helmet/Chest로만 하드코딩되어 있어(`GetEquippedSlotEnchantTotal(..., Helmet, Chest)`) Legs/Shoulder로 확장하려면 `ProducerCrafting.cs`의 해당 패치 수정이 선행되어야 한다.

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
    - [ ] 대상 슬롯 case에 출처 라인 추가 — ArmorTooltip.cs는 Helmet/Chest/Legs/Shoulder/Utility/Shield 6개 case를 모두 다룸(6~15번 타입 매핑표 참고), 실제 배정될 슬롯의 case만 수정

  공통:
    - [ ] DefaultLanguages_JobExpert.cs에 7개 언어 키 추가
    - [ ] ru.json 동기화
    - [ ] Producer_Config.cs에 수치 Config 추가
    - [ ] ConfigTranslations_JobDesc.cs 번역 추가

  확률형(Proc) 인챈트인 경우 (FireProc 등 참고):
    - [ ] 발동 확률은 피해량과 별도 소스(Producer_Config.GetElementalProcChance(level) 같은 레벨 고정 테이블)로 분리할지 결정
    - [ ] 분리한다면 GetEnchantLevel(item)로 롤 레벨 조회 필요 (cspt_enchant_level 저장 확인)
    - [ ] ProducerCrafting_WeaponElement.cs 패턴(Character.Damage Prefix, switch 분기) 재사용 검토
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
| `md/ARMOR_TOOLTIP_DISPLAY_RULES.md` | 방어구/방패 툴팁 전체 규칙 (Helmet/Chest/Legs/Shield case, `hasBonus` 게이트에 enchant 플래그 포함 근거) |
| `md/ACCESSORY_CAPE_TOOLTIP_DISPLAY_RULES.md` | 악세사리(Utility/Ring/Necklace)·망토(Shoulder) 마법부여 툴팁 규칙 — 방어구와 달리 라인 교체가 아닌 "끝에 추가" 방식 |
| `md/DAMAGE_SYSTEM_RULES.md` | 데미지 계산 시스템 |
| `md/CONFIG_GUIDE.md` | Config 키 규칙 |

## 📌 인벤토리 마우스오버 표시 동작 원리

마법부여 아이템은 **장착 여부와 무관하게** 인벤토리에서 마우스 오버만 해도 효과가 표시된다. 근거:

1. 출처 라인 패치(`ItemData_GetTooltip_ArmorBonus_Patch` 등)는 모두 `ItemDrop.ItemData.GetTooltip` Postfix이며, 첫 인자로 받는 `item`(호버 중인 아이템 그 자체)에서 `ProducerCrafting.GetEnchantType(item)`/`GetEnchantValue(item)`을 직접 읽는다 — 장착 중인 아이템 목록을 조회하는 것이 아니다.
2. `SkillEffect.ArmorTooltip.cs`의 `hasBonus` 조기 return 게이트(스킬 미투자 시 툴팁 수정을 건너뛰는 조건)에는 `enchantPct/enchantHp/enchantStaminaPct/enchantCooldown/...` 등 모든 인챈트 플래그가 OR 조건으로 이미 포함되어 있다. 즉 스킬트리에 아무것도 투자하지 않은 캐릭터라도, 마법부여된 아이템이면 툴팁 라인이 정상 표시된다.
3. 따라서 새 슬롯 풀 조합(Helmet+Armor, Legs+MaxStamina 등)을 추가할 때 **툴팁 표시 자체를 위한 추가 코드는 필요 없다** — 이미 있는 case(Helmet/Legs)가 해당 EnchantType의 변수(enchantPct/enchantStaminaPct)를 읽고 있다면 자동으로 작동한다. 새 EnchantType을 아예 새로 만들 때만 위 "새 인챈트 타입 추가 체크리스트"를 따른다.

# 어택.디스플레이.md - 무기 아이템 툴팁 스킬 효과 표시 규칙

## 📋 1. 개요

### 배경
무기 아이템에 마우스 오버 시 스킬트리 효과 + 제작 전문가 버프를 통합 표시.

**구현 파일 2종**:
- `SkillTree/SkillEffect.Attack_Tooltip_Display.cs` — 스킬트리 보너스 + 제작 전문가 버프 수치 적용
- `SkillTree/SkillEffect.WeaponTooltip.cs` — 제작 전문가 버프 출처 라인 추가

**참조 파일**: `md/ARMOR_TOOLTIP_DISPLAY_RULES.md` (동일 패턴 적용)

---

## 🗡️ 2. 지원 무기 10종 및 데미지 타입

| 무기 | WeaponType | 물리 데미지 타입 | 속성 데미지 타입 | 스킬 타입 |
|------|-----------|----------------|----------------|----------|
| 단검 | Knife | `m_pierce` + `m_slash` | - | `SkillType.Knives` |
| 주먹/클로 | Fist | `m_blunt` (주먹) / `m_pierce`+`m_slash` (클로) | - | `SkillType.Unarmed` |
| 검 | Sword | `m_slash` | - | `SkillType.Swords` |
| 둔기 | Mace | `m_blunt` | - | `SkillType.Clubs` |
| 창 | Spear | `m_pierce` | - | `SkillType.Spears` |
| 폴암 | Polearm | `m_pierce` + `m_slash` | - | `SkillType.Polearms` |
| 활 | Bow | `m_pierce` | - | `SkillType.Bows` |
| 석궁 | Crossbow | `m_pierce` | - | `SkillType.Crossbows` |
| 지팡이 | Staff | - | `m_fire` / `m_frost` / `m_fire` / `m_lightning` /`m_poison` / `m_spirit` | `SkillType.ElementalMagic` |
| 완드 | Wand | - | `m_fire` / `m_frost` / `m_fire` / `m_lightning` /`m_poison` / `m_spirit`  | `SkillType.Elementalmagic` |

### 분류 특이사항

**주먹/클로 (Fist)**: `SkillType.Unarmed`이지만 **단검 트리에서 인식**됨.
- `WeaponHelper.IsUsingKnife()` 내부에서 프리팹명 `"Fist"` / `"fist"` / `"Claw"` / `"claw"` 포함 시 단검으로 처리
- 툴팁 보너스도 단검 트리 스킬(Knife_Config) 적용

**완드 (Wand)**: `SkillType.Elementalmagic`.
- `WeaponHelper.IsUsingStaffOrWand()`로 지팡이(`ElementalMagic`)와 묶여 처리됨
- 별도 Wand_Config 없음 → Staff_Config + Attack_Config 공통 보너스 적용

### 발헤임 GetTooltip 원시 데미지 라인 키

```
$inventory_blunt    → 타격 (둔기)
$inventory_slash    → 베기 (검)
$inventory_pierce   → 관통 (단검, 창, 활)
$inventory_fire     → 불
$inventory_frost    → 냉기
$inventory_lightning → 번개
$inventory_poison   → 독
$inventory_spirit   → 정신
```

> **핵심**: 감지 키는 반드시 `$inventory_*` (원시 텍스트). 한국어/영어 번역문 감지 절대 금지.

---

## 📊 3. 표시 항목 4가지 정의

### 3-1. 데미지 보너스

**표시 대상**: 스킬 + 제작 전문가 버프로 인한 **물리/속성 데미지 % 증가** 합산값
**라인 감지 키**: `$inventory_blunt`, `$inventory_slash`, `$inventory_pierce`, `$inventory_fire` 등
**표시 위치**: 각 데미지 타입 라인을 개별 교체

```
타격: [주황]총합 [회색]([흰색]기본값 [회색]× [파랑]+XX%[회색])
불:   [주황]총합 [회색]([흰색]기본값 [회색]× [파랑]+XX%[회색])
```

### 3-2. 공격속도 보너스

**라인 감지 키**: 신규 라인 추가
**표시 위치**: 데미지 라인 하단

```
공격속도: <color=#4FC3F7>+XX%</color>
```

### 3-3. 치명타 확률/피해 보너스

```
치명타 확률: <color=#4FC3F7>+XX%</color>
치명타 피해: <color=#4FC3F7>+XX%</color>
```

### 3-4. 제작 전문가 버프 출처 라인

**표시 위치**: 모든 스탯 라인 맨 아래
**색상**: 아이콘 주황, 레이블 파랑(`#4FC3F7`), 수치 주황

```
⚒️[파랑]장인의 축복[/파랑] : 공격력 [주황]+15%[/주황]
```

---

## 🎨 4. 색상 규칙

```csharp
private const string COL_TOTAL   = "orange";    // 총합: 발헤임 기본 값 색상
private const string COL_BASE    = "white";     // 기본 수치: 흰색
private const string COL_BONUS   = "#4FC3F7";   // 스킬 보너스: 파란색
private const string COL_GRAY    = "#808080";   // 괄호/연산자: 회색
private const string COL_ATK_PHY = "#FFB347";   // 물리 공격력 라벨: 주황
private const string COL_ATK_ELEM = "#87CEEB";  // 속성 공격력 라벨: 하늘색
```

---

## 📝 5. 표시 포맷 예시

### 스킬 보너스만 (버프 없음)

```
타격: 125 (122 × +3%)
⚔️ 물리 공격력: +3%
```

### 제작 전문가 버프만 활성 (스킬 없음)

```
타격: 140 (122 × +15%)
불:   138 (120 × +15%)
⚒️장인의 축복 : 공격력 +15%
```

### 스킬 + 버프 동시 (지팡이)

```
불:   149 ((120 + 3) × +20%)    ← 속성 스킬 +5% + 버프 +15%
번개: 115 ((100 + 2) × +20%)
⚒️장인의 축복 : 공격력 +15%
🔥 속성 공격력: +5%              ← 스킬트리 전용 수치만 표시
```

### Producer 인챈트만 (+9%)

```
타격: 16 (15 × +9%)             ← crafting flat(+3) 포함 기본값에 인챈트 % 적용
✨ 제작 축복: 공격력 +9.0%       ← WeaponTooltip.cs에서 추가 (중복 표시 안 됨)
```

### crafting_lv2 flat (+3) + Producer 인챈트 (+9%)

```
타격: 16 ((12 + 3) × +9%)       ← flat 먼저 합산, 그 후 % 적용
✨ 제작 축복: 공격력 +9.0%
```

### 속성 무기 + Producer 인챈트 (+9%)

```
불:   131 (120 × +9%)           ← 속성에도 인챈트 % 적용됨
✨ 제작 축복: 공격력 +9.0%
```

---

## 🏗️ 6. 구현 구조

### 6-1. SkillEffect.Attack_Tooltip_Display.cs

**역할**: 스킬트리 보너스 + 제작 전문가 버프를 통합하여 각 데미지 라인에 반영

```
WeaponTooltipHelper
  ├── CollectBonuses(item, player, group) → WeaponBonuses
  │     ├── 스킬트리 물리 보너스 → physPct
  │     ├── 스킬트리 속성 보너스 → elemPct
  │     ├── [버프] IsProducerBuffActive → physPct += N, elemPct += N
  │     ├── [crafting_lv2] csct_weapon_dmg → b.FlatAllPhysical += craftDmg
  │     ├── [crafting_lv2] csct_weapon_spd → b.AttackSpeed += craftSpd
  │     └── [Producer 인챈트] cspt_enchant_type=WeaponDmg
  │           → physPct += pEnchantVal, elemPct += pEnchantVal
  │           → b.ProducerEnchantPct = pEnchantVal  ← 중복표시 방지용
  ├── GetRawBaseDamage(item, qualityLevel) → HitData.DamageTypes (패치 전 순수값)
  ├── ModifyDamageLines(ref result, bonuses, raw)
  │     └── TryReplaceDamageLine(line, raw, b)
  │           ├── $inventory_blunt  → BuildDamageLine(raw.m_blunt,  FlatBlunt,  PctPhysical)
  │           ├── $inventory_slash  → BuildDamageLine(raw.m_slash,  FlatSlash,  PctPhysical)
  │           ├── $inventory_pierce → BuildDamageLine(raw.m_pierce, FlatPierce, PctPhysical)
  │           ├── $inventory_fire   → BuildDamageLine(raw.m_fire,   FlatFire,   PctElemental)
  │           ├── $inventory_frost  → BuildDamageLine(raw.m_frost,  FlatFrost,  PctElemental)
  │           └── $inventory_lightning → BuildDamageLine(...)
  └── AppendExtraStats(ref result, bonuses, raw)
        ├── "⚔️ 물리 공격력: +X%"  (PctPhysical - producerPct - ProducerEnchantPct)
        ├── "🔥 속성 공격력: +X%"  (PctElemental - producerPct - ProducerEnchantPct)
        └── 공격속도 / 치명타 / 특수효과 라인들
```

**주의**: `AppendExtraStats`의 물리/속성 공격력 표시값에서 producer buff + Producer 인챈트 %를 빼야 함 (각각 별도 라인으로 표시되므로 중복 방지):

```csharp
float producerDisplayPct = ProducerSkills.IsProducerBuffActive(Player.m_localPlayer)
    ? Producer_Config.ProducerBuff_AttackBonusValue : 0f;
// b.ProducerEnchantPct = cspt_enchant_type=WeaponDmg 인챈트 %, 0이면 0
float displayPhysPct = b.PctPhysical - (b.HasSuppressAttack ? b.SuppressAttackPct : 0f) - producerDisplayPct - b.ProducerEnchantPct;
float displayElemPct = b.PctElemental - producerDisplayPct - b.ProducerEnchantPct;
```

### 6-2. SkillEffect.WeaponTooltip.cs

**역할**: 제작 전문가 버프 활성 시 버프 출처 라인 1줄 추가 (데미지 라인 교체는 하지 않음)

```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip), ...)]
[HarmonyPostfix, HarmonyPriority(Priority.Low)]
private static void Postfix(...)
{
    if (!isWeapon) return;
    if (!ProducerSkills.IsProducerBuffActive(player)) return;

    float atkBonus = Producer_Config.ProducerBuff_AttackBonusValue;
    // 버프 출처 1줄만 추가. 데미지 수치는 Attack_Tooltip_Display가 처리.
    __result += $"\n<color=#FF8C00>⚒️</color><color=#4FC3F7>{L.Get("weapon_effect_producer_buff")}</color> : {L.Get("weapon_stat_atk_power")} <color=orange>+{atkBonus:F0}%</color>";
}
```

---

## 🔧 7. Harmony Patch 우선순위

| 패치 클래스 | 파일 | 우선순위 | 역할 |
|-------------|------|---------|------|
| `ItemData_GetTooltip_WeaponBonus_Patch` | `Attack_Tooltip_Display.cs` | Priority.Low | 데미지 라인 교체 + 스탯 라인 추가 |
| `ItemData_GetTooltip_WeaponBuff_Patch` | `WeaponTooltip.cs` | Priority.Low | 버프 출처 라인 1줄 추가 |

두 패치 모두 Priority.Low. 순서는 플러그인 등록 순서에 따르나, 역할이 분리되어 있으므로 순서 무관.

---

## 📋 8. 무기별 스킬 ID → Config 매핑 표

### 공통 (전 무기 적용)

| 스킬 ID | 효과 | 타입 |
|---------|------|------|
| `attack_root` | 공격 전문가 전 데미지 +3% | 물리+속성% |
| `atk_pursuit_speed` | 이동속도 +12% (상시 패시브) | MoveSpeed |
| `speed_base` | 공격속도 | 공격속도% |

### 공격 전문가 트리 4국면 시스템

| Tier | 스킬 ID | 효과 유형 | 툴팁 표시 방식 |
|------|---------|----------|--------------|
| T0 | `attack_root` | 전 데미지 +3% (항시) | physPct + elemPct |
| T1 | `atk_opener` | 전투 개시 5초 +20% (조건부) | **미표시** (시간 기반 동적) |
| T2 | `atk_opener_melee` | 마무리 예열 (조건부) | **미표시** (전투 조건부) |
| T2 | `atk_opener_bow` | 크리확률 +15% **(상시 패시브)** | `b.CritChance += 15f` (Bow 전용) |
| T2 | `atk_opener_crossbow` | 첫발 +50% (조건부) | **미표시** (조건부 재충전) |
| T2 | `atk_opener_magic` | 스태거 확정 (조건부) | **미표시** (조건부) |
| T3 | `atk_pursuit` | 이동 적 +15~25% (조건부) | **미표시** (적 상태 조건부) |
| T4 | `atk_pursuit_speed` | 이동속도 +12% **(상시 패시브)** | `b.MoveSpeed += 12f` (공통) |
| T4 | `atk_frenzy_trigger` | 스태미나 -20% (조건부) | **미표시** (거리 조건부) |
| T5 | `atk_frenzy` | 스택 데미지 +5~40% (동적) | **미표시** (동적 스택) |
| T6 | `atk_crit_dmg` | 크리피해 +12% | Critical 시스템 통합 |
| T6 | `atk_finisher_melee` | 근접 +5% | physPct (근접 무기만) |
| T6 | `atk_twohand_crush` | 양손 +10% | physPct (양손 무기만) |
| T6 | `atk_staff_mage` | 속성 +5% | elemPct (지팡이만) |

> **동적/조건부 효과 미표시 원칙**: 전투 상태(`AttackTreeTracker`)에 따라 수치가 변하는 효과는
> 정적 아이템 툴팁에 표시하지 않음. 인게임 화면 텍스트(`ShowSkillEffectText`)로 대신 표시.

### 제작 전문가 버프 및 인챈트

| 출처 | 적용 대상 | 메서드 | 비고 |
|------|----------|--------|------|
| `ProducerBuff_AttackBonus` (버프, 기본 15%) | **물리+속성 전부** | `CollectBonuses`에서 physPct, elemPct 동시 증가 | 버프 활성 시에만 |
| `csct_weapon_dmg` (crafting_lv2 flat) | FlatAllPhysical | `b.FlatAllPhysical += craftDmg` | customData에서 직접 읽기 |
| `csct_weapon_spd` (crafting_lv2 speed) | AttackSpeed | `b.AttackSpeed += craftSpd` | customData에서 직접 읽기 |
| `cspt_enchant_type=WeaponDmg` (Producer 인챈트 %) | **물리+속성 전부** | `physPct += pEnchantVal`, `elemPct += pEnchantVal` | `b.ProducerEnchantPct`로 중복표시 방지 |

### 물리 무기 (Knife/Sword/Mace/Spear/Polearm/Bow/Crossbow)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `knife_step6_combat_damage` | `Knife_Config.KnifeCombatDamageBonusValue` | 전투 데미지 | 물리% |
| `sword_expert` | `Sword_Config.SwordExpertDamageValue` | 전문가 데미지 | 물리% |
| `mace_Step1_damage` | `Mace_Config.MaceExpertDamageBonusValue` | 전문가 데미지 | 물리% |
| `spear_Step1_crit` | `Spear_Config.SpearStep2CritDamageBonusValue` | 급소 찌르기 | 물리% |
| `bow_Step1_damage` | `Bow_Config.BowStep1ExpertDamageBonusValue` | 전문가 데미지 | 물리% |
| `crossbow_Step1_damage` | `Crossbow_Config.CrossbowExpertDamageBonusValue` | 전문가 데미지 | 물리% |
| `atk_opener_bow` | `Attack_Config.AtkOpenerBowCritChanceValue` | 크리확률 +15% (Bow 전용, 상시) | CritChance |
| `atk_finisher_melee` | `Attack_Config.AttackFinisherMeleeBonusValue` | 근접 +5% (T6) | 물리% |
| `atk_twohand_crush` | `Attack_Config.AttackTwoHandedBonusValue` | 양손 +10% (T6) | 물리% |

### 속성 무기 (Staff/Wand)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `staff_Step1_damage` | `Staff_Config.StaffExpertDamageValue` | 전문가 데미지 | 속성% |
| `atk_staff_mage` | `Attack_Config.AttackStaffElementalValue` | 속성 마스터 +5% (T6) | 속성% |

---

## ⚠️ 9. 구현 주의사항

### ① 감지 키는 반드시 원시 텍스트

```csharp
// ✅ 올바름
if (line.Contains("$inventory_blunt")) { ... }

// ❌ 금지 - 번역 후 문자열
if (line.Contains("타격")) { ... }
if (line.Contains("Blunt")) { ... }
```

### ② rawBase는 패치 전 순수 값

```csharp
// ✅ 올바름: m_shared 직접 계산
var raw = item.m_shared.m_damages;
if (qualityLevel > 1) raw += item.m_shared.m_damagesPerLevel * (qualityLevel - 1);

// ❌ 금지: GetDamage() → 이미 스킬 패치 적용된 값
```

### ③ 제작 관련 수치 분리 원칙

| 출처 | 데미지 라인 반영 | 스탯 라인 ("⚔️ 물리/🔥 속성") | 별도 라인 |
|------|----------------|-------------------------------|----------|
| 스킬트리 % 보너스 | ✅ (PctPhysical/PctElemental) | ✅ 표시 | - |
| 제작 전문가 버프 (15%) | ✅ (physPct/elemPct에 포함) | ❌ 제외 (`- producerDisplayPct`) | `WeaponTooltip.cs`에서 ⚒️장인의 축복 라인 |
| Producer 인챈트 % (`cspt_enchant_type=WeaponDmg`) | ✅ (physPct/elemPct에 포함) | ❌ 제외 (`- b.ProducerEnchantPct`) | `WeaponTooltip.cs`에서 ✨제작 축복 라인 |
| crafting_lv2 flat (`csct_weapon_dmg`) | ✅ (FlatAllPhysical) | - | - |

### ④ 효과 누적 합산 규칙 준수

```csharp
// ✅ 올바름 - physPct에 모든 물리 % 누산
float physPct = 0f;
if (HasSkill("attack_root")) physPct += Attack_Config.AttackRootDamageBonusValue;
if (HasSkill("sword_expert")) physPct += Sword_Config.SwordExpertDamageValue;
if (IsProducerBuffActive) physPct += Producer_Config.ProducerBuff_AttackBonusValue;
b.PctPhysical = physPct;
```

---

## ✅ 10. 검증 방법

### 인게임 확인 체크리스트

```yaml
무기_툴팁_검증:
  스킬만_보유_버프없음:
    - [ ] 물리 스킬 보유 시 타격/베기/관통 라인에 × +X% 표시
    - [ ] 속성 스킬 보유 시 불/번개/냉기 라인에 × +X% 표시
    - [ ] ⚔️/🔥 스탯 라인에 해당 % 표시
    - [ ] 장인의 축복 라인 미표시 (버프 없음)

  버프만_활성_스킬없음:
    - [ ] 모든 물리 데미지 라인에 × +15% 표시
    - [ ] 모든 속성 데미지 라인에 × +15% 표시
    - [ ] ⚔️/🔥 스탯 라인 미표시 (스킬 없음)
    - [ ] ⚒️장인의 축복 : 공격력 +15% 표시 (파란색 레이블)

  스킬_AND_버프_동시:
    - [ ] 물리 라인: × +(스킬% + 15%)
    - [ ] 속성 라인: × +(스킬% + 15%)
    - [ ] ⚔️ 물리 공격력: +스킬%  (버프 제외)
    - [ ] 🔥 속성 공격력: +스킬%  (버프 제외)
    - [ ] ⚒️장인의 축복 : 공격력 +15%

  Producer_인챈트_WeaponDmg:
    - [ ] 물리 무기: 타격/베기/관통 라인에 × +인챈트% 표시
    - [ ] 속성 무기: 불/번개/냉기 라인에 × +인챈트% 표시
    - [ ] ⚔️ 물리 공격력 스탯 라인에 인챈트% 미포함 (중복 방지)
    - [ ] 🔥 속성 공격력 스탯 라인에 인챈트% 미포함 (중복 방지)
    - [ ] ✨ 제작 축복 출처 라인은 WeaponTooltip.cs에서 별도 표시

  crafting_lv2_flat:
    - [ ] csct_weapon_dmg 붙은 무기: 데미지 라인에 (기본 + flat) 형태 표시

  비무기_아이템:
    - [ ] 방어구 등에서 패치 미작동 확인
```

---

## 📦 11. 파일 크기 현황

| 파일 | 역할 |
|------|------|
| `SkillEffect.Attack_Tooltip_Display.cs` | 메인 구현 (WeaponTooltipHelper + Patch 클래스) |
| `SkillEffect.WeaponTooltip.cs` | 버프 출처 라인 추가 전용 (소형) |

---

## 🔗 관련 문서

| 문서 | 내용 |
|------|------|
| `md/ARMOR_TOOLTIP_DISPLAY_RULES.md` | 방어구 툴팁 패턴 (이 문서의 기준) |
| `md/DAMAGE_SYSTEM_RULES.md` | 데미지 계산 시스템 |
| `md/CONFIG_GUIDE.md` | Config 키 규칙 |

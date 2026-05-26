# ACCESSORY_CAPE_TOOLTIP_DISPLAY_RULES.md - 악세사리/망토 마법부여 툴팁 표시 규칙

## 📋 개요

**목적**: 악세사리(Utility/Ring/Necklace)와 망토(Shoulder) 아이템에 마법부여(제작 축복) 효과를 올바르게 표시하는 방법

**참조**: [ARMOR_TOOLTIP_DISPLAY_RULES.md](./ARMOR_TOOLTIP_DISPLAY_RULES.md) 동일 패턴 적용

---

## 대상 아이템 타입

| ItemType | 슬롯 | 마법부여 풀 |
|---------|------|----------|
| `ItemType.Shoulder` | 망토 | MaxStamina(5) OR Eitr(11) |
| `ItemType.Utility` | 벨트 | InvWeight(12) OR EitrRegen(13) OR JumpForce(14) |
| `ItemType.Ring` | 반지 | InvWeight(12) OR EitrRegen(13) OR JumpForce(14) |
| `ItemType.Necklace` | 목걸이 | InvWeight(12) OR EitrRegen(13) OR JumpForce(14) |

---

## Rule A1: GetTooltip 원시 텍스트 구조

마법부여 툴팁은 `ItemDrop.ItemData.GetTooltip` Postfix에서 처리.
**중요**: 이 시점의 `__result`는 로컬라이제이션 **전** 원시 텍스트.

### 악세사리/망토 아이템 라인 형식 (예시)

```
[0]  '$item_megingjord_description'    ← 설명
[1]  ''
[2]  '$item_utility'                   ← 아이템 타입 (Utility/Ring/Necklace/Shoulder)
[3]  '$item_crafter: <color=orange>...</color>'
[4]  '$item_weight: <color=orange>4.0</color>'
[5]  '$item_quality: <color=orange>1</color>'
[6]  '$item_durability: <color=orange>100%</color>'
[7]  '$item_maxcarryweight: <color=orange>150</color>'  ← 벨트(Megingjord)
[8]  '$item_movement_modifier: ...'    ← 망토
```

> **주의**: 악세사리/망토는 방어력(`$item_armor`) 라인이 없음.
> 마법부여 효과는 **툴팁 맨 끝에 별도 라인으로 추가** 방식 사용.

---

## Rule A2: 마법부여 효과 표시 위치

방어구(ARMOR_TOOLTIP_DISPLAY_RULES.md)와 달리, 악세사리/망토는 기존 라인을 교체하는 대신
**툴팁 끝에 새 라인 추가** 방식 사용:

```csharp
// ProducerCrafting.cs의 GetTooltip Postfix에서 처리
var type = GetEnchantType(item);
if (type == EnchantType.None) return;

float value = GetEnchantValue(item);
string enchantLine = BuildEnchantLine(type, value);  // "✨ 제작 축복: ..." 형식

__result += "\n" + enchantLine;
```

---

## Rule A3: 표시 형식 (EnchantType별)

### 색상 상수 (기존 ARMOR 규칙 동일)

```csharp
private const string COL_ENCHANT = "#FFD700";   // 금색 - 마법부여 접두사 ✨
private const string COL_BONUS   = "#4FC3F7";   // 파란색 - 수치
private const string COL_UNIT    = "#808080";   // 회색 - 단위
```

### 슬롯별 표시 형식

| EnchantType | 표시 예시 |
|-------------|---------|
| MaxStamina (망토) | `<color=#FFD700>✨</color> 제작 축복: 스태미나 <color=#4FC3F7>+8%</color>` |
| Eitr (망토) | `<color=#FFD700>✨</color> 제작 축복: 에이트르 <color=#4FC3F7>+10</color>` |
| InvWeight (악세사리) | `<color=#FFD700>✨</color> 제작 축복: 인벤 무게 <color=#4FC3F7>+100</color>` |
| EitrRegen (악세사리) | `<color=#FFD700>✨</color> 제작 축복: 에이트르 회복 <color=#4FC3F7>+8%</color>` |
| JumpForce (악세사리) | `<color=#FFD700>✨</color> 제작 축복: 점프력 <color=#4FC3F7>+8%</color>` |

> **Eitr**: flat 수치이므로 `%` 없이 숫자만 표시
> **InvWeight**: kg 단위 없이 수치만 표시 (인벤토리 UI 자동 반영)

---

## Rule A4: InvWeight 인벤토리 표시

InvWeight 마법부여는 `Player.GetMaxCarryWeight` Postfix로 실제 최대 무게에 반영:

```csharp
// ProducerCrafting_Effects.cs
[HarmonyPatch(typeof(Player), nameof(Player.GetMaxCarryWeight))]
public static class Producer_Enchant_GetMaxCarryWeight_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(Player __instance, ref float __result)
    {
        float bonus = GetEquippedAccessoryEnchantTotal(__instance, EnchantType.InvWeight);
        if (bonus > 0f) __result += bonus;
    }
}
```

**결과**: 인벤토리 UI 무게 표시 → `현재무게 / 최대무게`에서 최대무게가 자동 증가
- 기본: `52.3 / 300`
- 악세사리 마법부여 +100 착용 시: `52.3 / 400`

---

## Rule A5: GetEquippedAccessoryEnchantTotal 구현

`ProducerCrafting.cs`에 추가할 악세사리 전용 합산 헬퍼:

```csharp
/// <summary>
/// 착용 중인 악세사리(Utility/Ring/Necklace) 마법부여 합산
/// </summary>
public static float GetEquippedAccessoryEnchantTotal(Player player, EnchantType targetType)
{
    var inv = player.GetInventory();
    if (inv == null) return 0f;
    float total = 0f;
    foreach (var item in inv.GetAllItems())
    {
        if (!item.m_equipped) continue;
        var t = item.m_shared.m_itemType;
        if (t != ItemDrop.ItemData.ItemType.Utility   &&
            t != ItemDrop.ItemData.ItemType.Ring       &&
            t != ItemDrop.ItemData.ItemType.Necklace) continue;
        if (GetEnchantType(item) == targetType)
            total += GetEnchantValue(item);
    }
    return total;
}
```

---

## Rule A6: GetEquippedSlotEnchantTotal 범용 헬퍼

특정 ItemType 슬롯에서만 합산:

```csharp
/// <summary>
/// 특정 ItemType 슬롯(들)에서만 마법부여 합산
/// </summary>
public static float GetEquippedSlotEnchantTotal(Player player, EnchantType targetType,
    params ItemDrop.ItemData.ItemType[] allowedSlots)
{
    var inv = player.GetInventory();
    if (inv == null) return 0f;
    float total = 0f;
    var slotSet = new HashSet<ItemDrop.ItemData.ItemType>(allowedSlots);
    foreach (var item in inv.GetAllItems())
    {
        if (!item.m_equipped) continue;
        if (!slotSet.Contains(item.m_shared.m_itemType)) continue;
        if (GetEnchantType(item) == targetType)
            total += GetEnchantValue(item);
    }
    return total;
}
```

---

## Rule A7: 패치 등록 형식

ARMOR_TOOLTIP_DISPLAY_RULES.md Rule T8과 동일:

```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
    new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
public static class ItemData_GetTooltip_AccessoryCapeEnchant_Patch
{
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Low)]
    private static void Postfix(ItemDrop.ItemData item, ref string __result)
    {
        var t = item.m_shared.m_itemType;
        bool isTarget = t == ItemDrop.ItemData.ItemType.Shoulder
                     || t == ItemDrop.ItemData.ItemType.Utility
                     || t == ItemDrop.ItemData.ItemType.Ring
                     || t == ItemDrop.ItemData.ItemType.Necklace;
        if (!isTarget) return;

        var enchantType = ProducerCrafting.GetEnchantType(item);
        if (enchantType == ProducerCrafting.EnchantType.None) return;

        float value = ProducerCrafting.GetEnchantValue(item);
        __result += "\n" + BuildEnchantLine(enchantType, value);
    }
}
```

> **주의**: 방어구(ARMOR 패치)는 기존 라인 교체; 악세사리/망토는 **줄 추가** 방식

---

## ✅ 구현 체크리스트

```yaml
악세사리_망토_마법부여_툴팁:
  아이템_타입:
    - [ ] Shoulder(망토): MaxStamina, Eitr 표시
    - [ ] Utility(벨트): InvWeight, EitrRegen, JumpForce 표시
    - [ ] Ring(반지): 위와 동일
    - [ ] Necklace(목걸이): 위와 동일
  표시_방식:
    - [ ] 툴팁 끝에 새 라인 추가 (교체 아님)
    - [ ] ✨ 금색 접두사 + 파란 수치
    - [ ] Eitr flat: % 없이 수치만
    - [ ] InvWeight: kg 없이 수치만
  효과_적용:
    - [ ] InvWeight → Player.GetMaxCarryWeight Postfix
    - [ ] MaxStamina → Player.GetMaxStamina Postfix
    - [ ] Eitr flat → Player.GetMaxEitr Postfix
    - [ ] EitrRegen → 에이트르 리젠 메서드 Postfix
    - [ ] JumpForce → Character.Jump Postfix
  헬퍼:
    - [ ] GetEquippedAccessoryEnchantTotal() 추가
    - [ ] GetEquippedSlotEnchantTotal() 추가
  성능:
    - [ ] GetAllItems() 호출: DoCrafting Postfix → 이벤트성 (OK)
    - [ ] GetMaxCarryWeight 패치: 호출 빈도 확인 필요 (캐시 고려)
```

---

## 🐛 디버그 방법

```csharp
// 임시 디버그 (문제 발생 시 추가, 해결 후 제거)
Plugin.Log.LogInfo($"[악세사리 툴팁] 타입={item.m_shared.m_itemType}, EnchantType={enchantType}, 값={value}");
```

---

## 스킬 툴팁 공격력 표기 원칙

### Rule S1: "공격력" 표기 통일

모든 스킬 툴팁에서 "데미지" 대신 "공격력"을 사용한다.

| 언어 | 기존 | 변경 |
|------|------|------|
| 한국어 | 데미지 | **공격력** |
| 영어 | Damage | ATK |

- 퍼센트 형식 유지: `공격력 {0}% · 범위 {1}%`
- 7개 언어 로컬라이제이션 동기화 필수 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- 스킬트리 UI 마우스오버 툴팁 기준

### Rule S2: 무기별 실제 공격력 계산 기준

실제 수치를 표시하거나 내부 계산 시, **스킬트리 보너스가 반영된 스킬팩터**를 반드시 적용한다.

```csharp
// 활 (Bow) — 활 + 장착 화살 합산
float bowSkillFactor = player.GetSkillFactor(Skills.SkillType.Bows);
float bowBase = weapon.GetDamage(0, bowSkillFactor).GetTotalDamage();

// 지팡이 (Staff / ElementalMagic)
float staffSkillFactor = player.GetSkillFactor(Skills.SkillType.ElementalMagic);
float staffBase = weapon.GetDamage(0, staffSkillFactor).GetTotalDamage();

// 완드 (BloodMagic)
float wandSkillFactor = player.GetSkillFactor(Skills.SkillType.BloodMagic);
float wandBase = weapon.GetDamage(0, wandSkillFactor).GetTotalDamage();
```

무기 미착용 또는 타입 불일치 시 → 퍼센트 표시로 폴백 필수.

```csharp
var weapon = player.GetCurrentWeapon();
if (weapon == null || weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
    return 0f; // 폴백: 퍼센트 표시
```

### Rule S3: 퍼센트 vs 실제 수치 표시 정책

| 상황 | 표시 방식 |
|------|----------|
| 기본 정책 (활·지팡이·완드 착용 조건 포함) | **퍼센트 표시** `공격력 80% · 범위 55%` |
| 실제 수치로 표시 시 | 무기 미착용 → 퍼센트 폴백 필수 |
| 실제 수치 계산 | Rule S2 스킬팩터 적용 방식 사용 |

> 폭발화살(R키)·화살비(H키)·이중시전(R키) 툴팁은 **퍼센트 표시** 정책 적용 중.
> 각 스킬에 "활 착용" 조건이 있으므로 퍼센트 표시로 충분함.
> 실제 수치 표시로 전환 시 Rule S2 기준을 따를 것.

---

## 아처 방식 툴팁 구현 패턴 (표준)

참조 파일: `SkillTree/Archer_Tooltip.cs` → `GetArcherTooltip()`

모든 신규 스킬 툴팁은 이 패턴을 표준으로 따른다.

### Rule T1: 파일 구조

```csharp
// 파일: SkillTree/XxxSkillName_Tooltip.cs
public static class XxxSkillName_Tooltip
{
    public static string GetTooltip()         // 진입점 (SkillTreeUI에서 호출)
    private static int GetStatForLevel(int level)   // 레벨별 수치 헬퍼
    private static string GetLevelItemText(int targetLevel)  // 강화 아이템 텍스트
}
```

### Rule T2: 색상 상수 (섹션별)

| 섹션 | 라벨 색상 | 값 색상 |
|------|----------|--------|
| 스킬명 | — | `#FFD700` (금색) |
| 현재 Lv 스탯 | — | `#E0E0E0` (밝은 회색) |
| 패시브 | `#98FB98` (연두) | `#ADFF2F` (라임) |
| 소모 비용 | `#FFB347` (주황) | `#FFDAB9` (살구) |
| 스킬 유형 (Y키) | `#1E90FF` (파랑) | `#ADFF2F` (라임) |
| 스킬 유형 (R/T/H키) | `#9400D3` (보라) | `#FFD700` (금색) |
| 쿨타임 | `#FFA500` (주황) | `#FFDB58` (노랑) |
| 범위 | `#87CEEB` (하늘) | `#B0E0E6` (연하늘) |
| 필요조건 | `#98FB98` (연두) | `#00FF00` (초록) |
| 공지사항 | `#F0E68C` (카키) | `#FFE4B5` (모카) |
| 강화 아이템 | `#FFA500` (주황) | `#FF6B6B` (연빨강) |
| 최대 레벨 | — | `#FFD700` (금색) |
| 레벨 프리뷰 구분선 | — | `#808080` (회색) |
| 레벨 프리뷰 텍스트 | — | `#808080` (회색) |

### Rule T3: GetTooltip() 구조 (순서 준수)

```csharp
public static string GetTooltip()
{
    // 1. 레벨 계산
    int currentLevel = manager?.GetSkillLevel("skill_id") ?? 0;
    int mainLevel    = currentLevel == 0 ? 1 : currentLevel;      // 표시용 (미획득 시 Lv1)
    int displayLevel = Math.Min(currentLevel + 1, MAX_LEVEL);      // 다음 레벨 (아이템 표시용)

    // 2. Config 값 로드
    float cooldown = Xxx_Config.XxxCooldownValue;
    int   stamina  = (int)Xxx_Config.XxxStaminaCostValue;

    // 3. 툴팁 조립 (아래 순서 고정)
    var t = $"<color=#FFD700><size=22>{L.Get("skill_name_key")}</size></color>\n";

    // 현재 Lv 스탯
    t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("stat_preview_key", GetStatForLevel(mainLevel))}</size></color>\n";

    // 패시브 (있는 경우)
    // t += $"<color=#98FB98>...<color=#ADFF2F>...

    // 소모 비용
    t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
    t += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {stamina}</size></color>\n";

    // 스킬 유형
    t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
    t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", "T")}</size></color>\n";

    // 쿨타임
    t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
    t += $"<color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";

    // 필요조건
    t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
    t += $"<color=#00FF00><size=16>{L.Get("requirement_bow_equip")}</size></color>\n";

    // 강화 아이템 또는 최대레벨
    if (currentLevel < MAX_LEVEL)
    {
        t += $"<color=#FFA500><size=16>{L.Get("upgrade_requires_key", displayLevel)}: </size></color>";
        t += $"<color=#FF6B6B><size=16>{GetLevelItemText(displayLevel)}</size></color>\n";
    }
    else
    {
        t += $"<color=#FFD700><size=16>{L.Get("max_level_key")}</size></color>\n";
    }

    // 레벨 프리뷰 (mainLevel < MAX_LEVEL인 경우)
    if (mainLevel < MAX_LEVEL)
    {
        t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
        for (int lv = mainLevel + 1; lv <= MAX_LEVEL; lv++)
        {
            t += $"<color=#808080><size=14>Lv{lv} : {L.Get("stat_preview_key", GetStatForLevel(lv))}</size></color>\n";
        }
    }

    return t.TrimEnd('\n');
}
```

### Rule T4: 다국어 적용 필수 규칙

**모든 텍스트는 `L.Get()` 경유 필수. 하드코딩 금지.**

| 항목 | 키 위치 | 동기화 파일 |
|------|---------|-----------|
| 스킬명, 효과 설명 | `DefaultLanguages_WeaponSkills.cs` KO + EN | 7개 json |
| 스탯 프리뷰 포맷 | `"stat_preview_key"` | 7개 json |
| 강화 아이템 텍스트 | `L.Get("item_trophy_xxx")` 조합 | 이미 등록된 키 재사용 |
| 최대레벨 메시지 | `"xxx_max_level"` | 7개 json |
| 강화 타이틀 | `"xxx_upgrade_requires"` | 7개 json |

**7개 언어 동기화 순서:**
1. `DefaultLanguages_WeaponSkills.cs` — KO (한국어) + EN (영어)
2. `Localization/de.json` — 독일어
3. `Localization/ru.json` — 러시아어
4. `Localization/zh-cn.json` — 중국어 간체
5. `Localization/ja.json` — 일본어
6. `Localization/pt_BR.json` — 포르투갈어(브라질)

**새 키 추가 시 체크리스트:**
```
[ ] DefaultLanguages_WeaponSkills.cs  KO 추가
[ ] DefaultLanguages_WeaponSkills.cs  EN 추가
[ ] de.json      추가
[ ] ru.json      추가
[ ] zh-cn.json   추가
[ ] ja.json      추가
[ ] pt_BR.json   추가
```

### Rule T5: 강화 아이템 텍스트 헬퍼 패턴

```csharp
private static string GetLevelItemText(int targetLevel)
{
    switch (targetLevel)
    {
        case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
        case 2: return L.Get("item_trophy_elder")   + " x1 + " + L.Get("item_trophy_frosttroll") + " x1";
        // ...
        default: return "";
    }
}
// → L.Get()으로 아이템명을 가져오므로 자동 다국어 지원
```

### Rule T6: SkillTreeUI 연동

툴팁 파일 작성 후 `Gui/SkillTreeUI.cs`의 `GetNodeTooltip()` 또는 해당 스킬 조건 분기에 연결:

```csharp
// SkillTreeUI.cs 내 GetNodeTooltip() 또는 노드별 분기
if (node.Id == "skill_id")
    return XxxSkillName_Tooltip.GetTooltip();
```

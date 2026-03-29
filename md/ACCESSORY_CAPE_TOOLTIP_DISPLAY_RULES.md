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

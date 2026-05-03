# 내구도 보너스 시스템 규칙

## 핵심 구조

내구도 보너스는 **두 곳에 동시 적용**해야 "현재/최대" 둘 다 올라간다.

| 위치 | 대상 | 역할 |
|------|------|------|
| `DoCrafting` Postfix | `item.m_durability *= mult` | 현재 내구도 올림 |
| `GetMaxDurability(int)` Postfix | `__result *= mult` | 최대 내구도 올림 (툴팁 분모) |

둘 중 하나라도 빠지면 `250/200` 또는 `200/250` 형태로 깨진다.

---

## m_customData 키 (아이템에 저장)

| 키 | 파일 | 스킬 |
|----|------|------|
| `"CraftingDurabilityBonus"` | `CraftingEnhancement.cs` | crafting_lv2~4 |
| `"cspt_dur_bonus_mult"` | `ProducerCrafting.cs` (`DUR_BONUS_KEY`) | 제작전문가(Producer) |

두 보너스가 **체인** 적용된다. 예: crafting_lv2(×1.45) + Producer(×1.50) → 기본 200 → 290 → 435

---

## Harmony 패치 어트리뷰트 규칙

```csharp
// ✅ 정확 — 툴팁은 int 오버로드를 직접 호출
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetMaxDurability), new[] { typeof(int) })]

// ❌ 잘못됨 — no-params 버전은 내부에서 int 버전을 호출할 뿐
[HarmonyPatch(typeof(ItemDrop.ItemData), "GetMaxDurability", new Type[] { })]
```

Valheim `GetMaxDurability` 오버로드:
- `GetMaxDurability()` → `GetMaxDurability(this.m_quality)` 호출만 함
- `GetMaxDurability(int quality)` → 실제 계산 (`m_maxDurability + quality_bonus`)
- 툴팁(`GetTooltip`)은 `GetMaxDurability(qualityLevel)` (int 버전) 직접 호출

---

## Harmony 등록 필수 조건

```csharp
// ✅ Harmony PatchAll()이 인식함
public static void Postfix(...)

// ❌ 조용히 실패 — 등록 안 됨
private static void Postfix(...)
```

---

## GetMaxDurability 반복 호출 특성

툴팁 렌더링 중 `GetMaxDurability`가 프레임마다 여러 번 호출된다 → **정상 동작**.
Postfix에 `LogInfo` 넣으면 무한 로그 출력됨 — **진단 후 반드시 제거**.

---

## 파일별 패치 위치

### `CraftingEnhancement.cs`

| 클래스 | 역할 |
|--------|------|
| `CraftingDurability_DoCrafting_Patch` | 제작 시 `m_durability *= mult`, `m_customData` 저장 |
| `ItemData_GetMaxDurability_Enhancement_Patch` | 툴팁 최대 내구도 보정 (public Postfix 필수) |

`GetPlayerCraftingBonus()` 내부에 Producer 보너스 **포함 금지** — `ProducerCrafting.cs`가 독립 처리.

### `ProducerCrafting.cs`

| 클래스 | 역할 |
|--------|------|
| `Producer_InventoryGui_DoCrafting_Patch` | 제작 시 `m_durability *= mult`, `m_customData` 저장 |
| `Producer_GetMaxDurability_Patch` | 툴팁 최대 내구도 보정 |

---

## 신규 내구도 보너스 스킬 추가 체크리스트

- [ ] `DoCrafting` Postfix에서 배율 계산 후 `item.m_durability *= mult`
- [ ] `item.m_customData["키"] = mult.ToString(InvariantCulture)` 저장
- [ ] `GetMaxDurability(int)` Postfix 클래스 추가 (`public static void Postfix`)
- [ ] Postfix에서 `m_customData["키"]` 읽어 `__result *= mult`
- [ ] 어트리뷰트: `new[] { typeof(int) }` 사용
- [ ] 다른 스킬과 키 충돌 여부 확인
- [ ] 다른 내구도 패치와 중복 적용 여부 확인

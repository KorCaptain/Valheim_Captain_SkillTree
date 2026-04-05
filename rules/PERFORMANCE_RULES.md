# PERFORMANCE_RULES.md — 성능·안전 규칙

> 트리거: 신규/수정 스킬 완성 후 반드시 자가 점검
> 인벤토리 관련 추가 체크 → `md/INVENTORY_PATCH_CHECKLIST.md`

---

## 자가 점검 체크리스트

```
[ ] 이 Harmony 패치가 초당 몇 번 발동되는가?
    → 이벤트성(제작/피격/사용)이면 OK, Update급이면 캐시/throttle 추가
[ ] 코루틴에 최대 지속시간 또는 종료 플래그가 있는가?
[ ] DoCrafting 패치라면 IsRepairAction() early return이 있는가?
[ ] GetAllItems() 호출에 0.25s throttle이 있는가?
[ ] Reflection이 static 캐시를 사용하는가?
[ ] 플레이어 키 Dictionary가 퇴장 시 정리되는가?
[ ] ZNet RPC / VFX가 루프 내에서 반복 호출되지 않는가?
```

---

## R1. 프레임 단위 패치 금지

| 금지 패턴 | 이유 | 대안 |
|---------|------|------|
| `Player.Update()` Postfix 내 무거운 연산 | 초당 60회 → CPU 과부하 | 이벤트 기반 패치로 대체 |
| `InventoryGrid.UpdateGui()` 내 `GetAllItems()` 무제한 호출 | 드래그 중 매 프레임 실행 | throttle (0.25s 이상) 필수 |
| `Hud.Update()` 내 스킬 계산 | 초당 60회 → 스태터 | `SkillBonusCalculator` 프레임 캐시 사용 |

---

## R2. 코루틴 탈출 조건 필수

```csharp
// ✅ 안전: 탈출 조건 명확
while (elapsed < duration && player != null && !player.IsDead())
{
    yield return new WaitForSeconds(0.1f);
    elapsed += 0.1f;
}

// ❌ 위험: 무한 루프 → 서버 과부하
while (isActive) { DoHeavyWork(); yield return null; }
```

- 모든 코루틴에 최대 지속시간 또는 종료 플래그 필수
- 사망/로그아웃 시 자동 종료 조건 포함
- 중복 실행 방지: `if (coroutine != null) StopCoroutine()` 패턴

---

## R3. Harmony 패치 오발동 방지

| 패치 메서드 | 실제 발동 상황 | 필수 early return |
|------------|--------------|-----------------|
| `InventoryGui.DoCrafting` | 제작 + **수리** 모두 호출 | `IsRepairAction()` 체크 |
| `InventoryGui.Show()` | 인벤토리 + **상자 열기** 모두 호출 | 컨테이너 여부 확인 |
| `InventoryGrid.UpdateGui()` | **드래그 중 매 프레임** | 0.25s throttle 필수 |
| `Player.ConsumeResources()` | 제작 + **일부 스킬 발동** | craftButtonClicked 플래그 |

---

## R4. 메모리 누수 방지

- `Dictionary<Player, Coroutine>`: 로그아웃 시 반드시 `Remove(player)`
- `HashSet<string>` 강화 기록: 인벤토리 닫기/주기적으로 `Clear()`
- `static Texture2D / Sprite`: `HideFlags.HideAndDontSave` 설정
- 이벤트 리스너: `RemoveAllListeners()` 후 재등록 (중복 방지)

---

## R5. Reflection 성능

```csharp
// ✅ 안전: 1회 캐싱
private static FieldInfo _myField;
private static FieldInfo GetMyField() =>
    _myField ??= typeof(TargetClass).GetField("m_field", BindingFlags.NonPublic | BindingFlags.Instance);

// ❌ 위험: 호출마다 실행 → GC + 느림
void Postfix(...) {
    var field = typeof(TargetClass).GetField("m_field", ...);
}
```

---

## R6. ZNet RPC / 멀티플레이어 부하

- `ZNetScene.instance.SpawnObject()` / RPC: 루프 내 반복 금지, 단발성만
- `VFXManager.PlayVFXMultiplayer()`: 패시브 스킬에서 호출 금지
- `ZDO.Set()`: 이벤트 발생 시 1회 원칙. 불가피한 경우 최소 0.5s 간격 유지

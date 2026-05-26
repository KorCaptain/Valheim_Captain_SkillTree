# ACTIVE_SKILL_HUD_RULES.md — ActiveSkillHUD 구현 규칙

**파일**: `Gui/ActiveSkillHUD.cs`  
**대상**: 인게임 HUD 슬롯 (Y/R/G/H/M2/PASS 단축키 아이콘 + 레이블)

---

## Rule 1: ScaleWithScreenSize 캔버스에서 Text는 전용 Dynamic Font 필수

### ❌ 금지
```csharp
keyText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
```

### ✅ 필수
```csharp
keyText.font = GetHudFont(); // 반드시 이 헬퍼 사용
```

`GetHudFont()`는 `Font.CreateDynamicFontFromOSFont("Arial", 14)`를 우선 사용한다.

### 이유
`ActiveSkillHUDCanvas`는 `CanvasScaler.ScaleMode.ScaleWithScreenSize`(1920×1080 기준)를 사용한다.
Valheim 게임 UI는 `ConstantPixelSize` 캔버스를 사용한다.

Unity에서 두 캔버스가 `Resources.GetBuiltinResource<Font>("Arial.ttf")` **동일 font 에셋을 공유**하면
font texture atlas가 한쪽 캔버스의 scale에 맞춰 캐싱된다. 반대 scale 캔버스에서는
**Text 컴포넌트 렌더링이 완전히 실패(불표시)** 된다.

- `Image` 컴포넌트: shader 기반 렌더링 → 영향 없음 (아이콘은 정상)
- `Text` 컴포넌트: font atlas 의존 → **충돌 시 텍스트 완전 불표시**

`Font.CreateDynamicFontFromOSFont`는 HUD 전용 독립 atlas를 생성하므로 충돌 없음.

---

## Rule 2: Text 컴포넌트에 Overflow 설정 필수

ScaleWithScreenSize 모드에서 canvas scale이 변할 때 Text가 rect를 초과해
truncate될 수 있으므로 KeyLabel Text에는 반드시 overflow 설정을 추가한다.

```csharp
keyText.verticalOverflow   = VerticalWrapMode.Overflow;
keyText.horizontalOverflow = HorizontalWrapMode.Overflow;
```

---

## Rule 3: KeyLabel RectTransform 위치 기준

```csharp
keyRt.anchorMin         = new Vector2(0.5f, 0f);   // 슬롯 하단 중앙 앵커
keyRt.anchorMax         = new Vector2(0.5f, 0f);
keyRt.pivot             = new Vector2(0.5f, 0f);
keyRt.anchoredPosition  = new Vector2(0f, -12f);   // 아이콘 아래 7px 간격
keyRt.sizeDelta         = new Vector2(iconSz, 32f);
```

**위치 기하학** (iconSz=62, 슬롯 center-pivot 기준 y좌표):
| 요소 | y 범위 | 비고 |
|------|--------|------|
| 아이콘 (Bg/Border/Icon) | −21 ~ +41 | anchoredPosition=(0,10), size=62 |
| KeyLabel at −12f | −60 ~ −28 | 아이콘 하단(−21)과 7px 간격 ✅ |
| KeyLabel at +8f | −40 ~ −8 | 아이콘과 13px 겹침 ❌ |

### ❌ 금지
```csharp
keyRt.anchoredPosition = new Vector2(0f, 8f);   // 아이콘과 겹쳐 버림
```

---

## Rule 4: 아이콘 크기 Update 루프에 KeyLabelText sizeDelta 반드시 포함

`HudIconSize.Value`가 변경될 때 모든 동적 요소를 일괄 업데이트해야 한다.

```csharp
if (iconSz != _lastIconSize)
{
    // ... IconRt / CooldownOverlay / CountdownText / BgRt / BorderRt / RootRt / LayoutElem ...
    if (slot?.KeyLabelText != null)
        slot.KeyLabelText.rectTransform.sizeDelta = new Vector2(iconSz, 32f); // ← 필수
}
```

KeyLabelText를 누락하면 초기 iconSz=0 상태로 생성된 경우 width=0이 유지되어
텍스트가 표시되지 않는다.

---

## Rule 5: CanvasScaler 설정 변경 시 Text 컴포넌트 재검증 필수

`CanvasScaler.ScaleMode`를 변경하거나 추가할 경우:
1. **모든 Text 컴포넌트**가 `GetHudFont()`(전용 dynamic font)를 사용하는지 확인
2. `verticalOverflow / horizontalOverflow = Overflow` 설정 여부 확인
3. `anchoredPosition` 값이 아이콘 영역과 겹치지 않는지 확인 (Rule 3 참조)

Image 컴포넌트는 영향을 받지 않으므로 "아이콘은 보이는데 텍스트만 안 보인다"는
증상이 나타나면 font atlas 충돌(Rule 1)을 가장 먼저 의심할 것.

---

## 참고

- `GetHudFont()` 구현: `Gui/ActiveSkillHUD.cs` line ~61
- 관련 규칙: `rules/UI_RENDERING_RULES.md` (SetSiblingIndex, 렌더 순서)
- 관련 규칙: `md/UI_SYSTEM_RULES.md` (스킬트리 UI 아이콘 크기)

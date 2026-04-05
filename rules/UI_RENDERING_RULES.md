# UI_RENDERING_RULES.md — UI 렌더링 규칙

> 트리거: UI, panel, tooltip, SetSiblingIndex, SkillTreeUI, 렌더링 순서
> 상세 → `md/UI_SYSTEM_RULES.md`

---

## R1. SetSiblingIndex 렌더링 순서 (필수 준수)

```csharp
bgObj.transform.SetSiblingIndex(0);        // 배경
line.transform.SetSiblingIndex(1);         // 연결선
nodeObj.transform.SetSiblingIndex(2);      // 일반 노드
jobNodeObj.transform.SetSiblingIndex(3);   // 직업 아이콘 (Berserker, Tanker, Rogue, Archer, Mage, Paladin)
tooltipObj.transform.SetAsLastSibling();   // 툴팁 (최상위)
```

**적용 파일**: `Gui/SkillTreeUI.cs`, `Gui/SkillTreeNodeUI.cs`, `Gui/SkillTreeTooltip.cs`

순서 변경 시 노드·툴팁이 배경에 가려지는 렌더링 버그 발생.

---

## R2. 전문가 제한 시스템
- **원거리 전문가**: 액티브 스킬 1개만 (활 OR 석궁)
- **근접 전문가**: 액티브 스킬 1개만 → G키는 무기별 자동 전환
- **지팡이/둔기 전문가**: 같은 무기류 2개 모두 가능
- **직업 슬롯**: 1개만 선택 가능

---

## R3. G키 자동 전환 시스템

```
착용 무기 확인 → 해당 스킬 검사 → 발동 또는 가이드 메시지
```

| 착용 무기 | G키 동작 |
|----------|---------|
| 지팡이 | 힐링 |
| 단검 | 암살자의 심장 |
| 검 | Sword Slash |
| 창 | 연공창 |
| 둔기 | 분노의 망치 |

---

## R4. 수정 금지
- `SkillTreeInputListener.cs` — 전체 파일 (UI 토글 및 ESC키 처리)
- `SkillTreeData.cs` — RegisterAll() 함수의 6개 루트 노드

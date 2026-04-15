---
name: cst-ui-system
description: Use when working on UI panels, node icons, tooltips, or rendering order. Triggers: UI, panel, tooltip, SetSiblingIndex, localScale, 아이콘 크기, 렌더링 순서, UI 패널
---

## 핵심 규칙 요약

- **아이콘 크기**: `RectTransform.localScale` 사용 - 스프라이트 원본 크기 무시
  - 직업 아이콘 > 전문가 노드 > 일반 스킬 노드 순서로 판별
- **렌더링 순서** (`SetSiblingIndex`): 배경(0) → 연결선(1) → 일반노드(2) → 직업아이콘(3) → 툴팁(SetAsLastSibling)
- 적용 파일: `SkillTreeUI.cs`, `SkillTreeNodeUI.cs`, `SkillTreeTooltip.cs`
- 툴팁은 항상 최상위 렌더링 (`SetAsLastSibling()` 필수)

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\UI_SYSTEM_RULES.md`

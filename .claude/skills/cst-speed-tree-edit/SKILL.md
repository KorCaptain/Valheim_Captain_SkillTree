---
name: cst-speed-tree-edit
description: Use when editing the speed expert tree skill layout or tier configuration. Triggers: speed tree edit, 속도 트리 편집, speed tree tier, 속도 전문가 트리 구성
---

## 핵심 규칙 요약

- **티어 구성 (Speed_tree-edit.txt 기준)**:
  - 티어0: 속도 전문가 - 이동속도 +5%
  - 티어1: 민첩함의 기초 - 공격속도 +5%, 구르기 후 2초간 이동속도 +15%
- 스킬 추가 시 `cst-speed-expert` 참조하여 StatusEffect 방식으로 구현
- 각 티어 스킬은 Config + 효과 + 툴팁 + 다국어 5종 세트 동시 수정
- 구현 시 `SPEED_EXPERT_VALHEIM_API_IMPLEMENTATION.md` 패턴 따르기

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\Speed_tree-edit.txt`

---
name: cst-quick
description: Use for quick API reference or when looking up key rules fast. Triggers: quick reference, API, 빠른 참조, 수정 금지, 핵심 규칙, quick ref
---

## 핵심 규칙 요약

- **절대 금지**: Plugin.cs / SkillTreeInputListener.cs / SkillTreeData.cs RegisterAll() 루트 노드
- **5종 세트 원칙**: Config · 효과 · 툴팁 · UI다국어 · Config다국어 동시 수정
- **이벤트 기반 패치만 허용** - 프레임 기반 패치 금지
- **MMO 우선 연동** - 기본 스탯은 getParameter 패치 사용
- **공통 유틸**: WeaponHelper, SkillBonusCalculator, SkillNodeBuilder 반드시 사용

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\QUICK_REFERENCE.md`

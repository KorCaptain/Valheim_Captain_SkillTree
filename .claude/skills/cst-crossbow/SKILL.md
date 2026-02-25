---
name: cst-crossbow
description: Use when working on crossbow skill implementation. Triggers: crossbow, 석궁, crossbow skill, 석궁 스킬, crossbow expert
---

## 핵심 규칙 요약

- **구현 상태** (v0.1.5): Config 연동 완료(10개 스킬, 14개 Config 변수 100%), 효과 구현 부분적
- 석궁 스킬은 원거리 전문가 계열 - R키 액티브 1개 제한 적용
- `WeaponHelper.IsUsingCrossbow()` 사용하여 무기 타입 감지
- 석궁 데미지 타입: `m_pierce` 위주 (화살과 동일한 관통 타입)
- 미구현 스킬 목록 확인 후 단계적 구현 필요

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\CROSSBOW_SKILL_STATUS.md`

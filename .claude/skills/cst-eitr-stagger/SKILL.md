---
name: cst-eitr-stagger
description: Use when implementing eitr (magic resource) or stagger mechanic skills. Triggers: eitr, stagger, IsStaggering, m_eitr, SetMaxEitr, 에이트르, 스태거, 비틀거림
---

## 핵심 규칙 요약

- **에이트르 최대치**: `Player.SetMaxEitr()` Postfix 패치 - 검증 완료 ✅
- 에이트르 보너스: `m_eitr` 관련 패치로 구현, MMO 지팡이 트리와 연동
- **비틀거림 감지**: `Character.IsStaggering()` API - 작동 여부 검증 필요 ⚠️
- 스태거 상태 추가 효과: `IsStaggering()` 확인 후 별도 패치에서 처리
- 에이트르 회복속도: `Player.m_eiterRegen` 수정으로 구현

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\EITR_STAGGER_SYSTEM_RULES.md`

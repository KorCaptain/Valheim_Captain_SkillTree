---
name: cst-stagger-guide
description: Use when verifying stagger detection or implementing stagger-based skill effects. Triggers: stagger verification, IsStaggering, 스태거 검증, 비틀거림 감지, stagger debug
---

## 핵심 규칙 요약

- **검증 목표**: `Character.IsStaggering()` API 정상 작동 여부 확인
- 비틀거림 감지: `Character.IsStaggering()` 반환값으로 상태 확인
- 검증 단계: API 호출 확인 → 상태 정확성 확인 → 추가 효과 적용 테스트
- 스태거 상태 추가 효과(예: 추가 데미지)는 `IsStaggering() == true` 조건에서만 발동
- 에이트르/스태거 통합 규칙: `cst-eitr-stagger` 참조

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\STAGGER_VERIFICATION_GUIDE.md`

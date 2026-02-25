---
name: cst-patterns
description: Use when looking for proven development patterns or avoiding known anti-patterns. Triggers: pattern, 개발 패턴, anti-pattern, 성공 패턴, 실패 사례, best practice
---

## 핵심 규칙 요약

- **성공 패턴**: 체계적 디버깅(단계별 로그) → 기존 작동 코드 분석 → Valheim 내부 명명 준수
- 아이템 감지: `"Wood"` ❌ → `"$item_wood"` ✅ (Valheim 내부 명명 규칙)
- 기존 직업 스킬 패턴 참고: `inventory.HaveItem("$item_trophy_eikthyr")` 방식
- **실패 패턴**: 직접 문자열 비교, 외부 패키지 명 사용, 추측 기반 구현
- 새 기능 구현 전 유사한 작동 코드 먼저 분석 후 패턴 적용

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\DEVELOPMENT_PATTERNS.md`

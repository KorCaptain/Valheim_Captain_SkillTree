---
name: cst-parry
description: Use when implementing parry (perfect block) detection. Triggers: parry, 패링, perfect block, BlockAttack, block detection, 퍼펙트 블록
---

## 핵심 규칙 요약

- **핵심 문제**: Valheim의 `BlockAttack()`은 일반 막기와 패링 모두 `true` 반환
- 패링(퍼펙트 블록)만 구분하려면 별도 감지 로직 필요
- 타이밍 기반 감지: 블록 입력 후 특정 시간 내 공격 받으면 패링으로 판정
- 여러 접근법 시도 결과 기록 참조 (어떤 방법이 작동/실패했는지 확인)
- `WeaponHelper.IsUsingShield()` 확인 후 패링 로직 적용

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\PARRY_DETECTION_SYSTEM.md`

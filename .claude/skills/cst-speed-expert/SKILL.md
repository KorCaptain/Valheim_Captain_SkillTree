---
name: cst-speed-expert
description: Use when implementing speed expert tree skills (move speed, attack speed combos). Triggers: speed tree, 속도 전문가, MoveSpeed, 이동속도, speed expert, SE_SkillTreeMoveSpeed
---

## 핵심 규칙 요약

- **구현 방식 통일**: Valheim API 기반 - `SE_SkillTreeMoveSpeed` StatusEffect 방식 사용
- ❌ `MyStatRegistry.MoveSpeedBonuses` Registry 방식 혼용 금지
- ❌ 직접 `m_speed` 수정 금지
- 속도 전문가 티어 구성: 티어0(이동속도+5%) → 티어1(공속+5%, 구르기 후 이동속도+15%) 등
- StatusEffect 기반으로 서버 동기화 자동 처리

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SPEED_EXPERT_VALHEIM_API_IMPLEMENTATION.md`

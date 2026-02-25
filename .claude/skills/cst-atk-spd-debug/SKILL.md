---
name: cst-atk-spd-debug
description: Use when debugging attack speed issues or investigating speed-related bugs. Triggers: attack speed debug, 공격속도 디버그, animator speed conflict, AnimationSpeedManager 충돌
---

## 핵심 규칙 요약

- **버그**: 모드 활성화 시 모든 무기 기본 공격 속도 저하
- **원인**: `animator.speed` 직접 조작 패치와 AnimationSpeedManager 충돌
- **해결**: `animator.speed` 직접 조작 패치 비활성화 → AnimationSpeedManager로 통합
- 공격속도 관련 모든 패치는 `AnimationSpeedManager`를 통해서만 처리
- 버그 재현 시: 해당 패치 비활성화 후 단계적으로 활성화하며 원인 파악

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\Attack_Speed_bug.md`

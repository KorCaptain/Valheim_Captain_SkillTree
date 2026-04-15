---
name: cst-atk-spd-bug
description: Use when encountering secondary attack speed (mouse wheel) abnormal behavior. Triggers: secondary attack speed bug, 세컨더리 어택 속도, 휠마우스 공격, mouse wheel attack speed
---

## 핵심 규칙 요약

- **증상**: 휠마우스(세컨더리 어택) 속도가 비정상적으로 빠름, 일반 공격은 정상
- **원인**: AnimationSpeedManager의 `speed < 0.99 → 1.0` 보정 코드
- **해결**: 보정 코드 제거 - 원본 speed에 직접 보너스 적용
- 세컨더리 어택은 별도 애니메이션 클립 사용 - 일반 공속과 다른 계산 필요
- 수정 후 일반 공격/세컨더리 어택 모두 테스트 필수

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SECONDARY_ATTACK_SPEED_BUG.md`

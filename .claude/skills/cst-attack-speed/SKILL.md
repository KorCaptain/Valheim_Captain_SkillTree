---
name: cst-attack-speed
description: Use when implementing attack speed skills. Triggers: attack speed, AnimationSpeedManager, SetAnimationSpeed, 공격속도, 공격 속도, animator speed
---

## 핵심 규칙 요약

- **AnimationSpeedManager.dll 참조 필수** - `<Private>True</Private>` 설정으로 배포 시 DLL 포함
- 직접 호출 방식 사용: `AnimationSpeedManager.Instance.SetAnimationSpeed(player, speed)`
- ❌ 리플렉션 방식(`Type.GetType`) 사용 금지
- ❌ `animator.speed` 직접 조작 금지 (AnimationSpeedManager와 충돌)
- 공속 보너스: `SkillBonusCalculator.CalculateTotal()` 사용 - 여러 트리의 보너스 합산 필수
- 세컨더리 어택(휠마우스) 속도 버그: `speed < 0.99 → 1.0` 보정 코드 제거 필요

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ATTACK_SPEED_SYSTEM_RULES.md`

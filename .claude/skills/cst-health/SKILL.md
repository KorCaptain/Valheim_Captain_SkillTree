---
name: cst-health
description: Use when implementing HP/health bonus skills. Triggers: health, HP, heal, m_baseHP, SetMaxHealth, 체력, 최대 HP, 힐링 깜빡임
---

## 핵심 규칙 요약

- **모든 체력 보너스는 반드시 `m_baseHP`에 포함**되어야 힐링이 정상 작동 (힐링 깜빡임 방지)
- Valheim 체력 시스템 3요소: `m_baseHP`(내부), `m_health`(현재), `GetMaxHealth()`(표시용)
- 체력 보너스 적용: `Player.m_baseHP += bonus` 또는 `SetMaxHealth()` Postfix 패치
- 힐링 시스템은 `m_baseHP` 기준으로 회복량 계산 - 별도 체력 추가 시 깜빡임 발생
- MMO Body 파라미터와 연동 시 `getParameter(Parameter.Body)` 우선 사용

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\HEALTH_SYSTEM_RULES.md`

---
name: cst-evasion
description: Use when implementing dodge/evasion skills. Triggers: dodge, evasion, SetCustomDodgeChance, IsBlockedBySkill, 회피, 구르기, 회피율
---

## 핵심 규칙 요약

- **회피 확률**: `Player.SetCustomDodgeChance(chance)` 호출 - `UpdateDefenseDodgeRate()`에서 구현
- **구르기 무적시간**: `Player.m_dodgeInvincibleTime` 직접 수정
- **구르기 스태미나**: `Player.m_dodgeStaminaUsage` 조정
- 회피 스킬 3가지 독립 구현: 확률/무적시간/스태미나 각각 별도 패치
- 복수 트리(방어 + 기타)에서 동일 효과 시 `SkillBonusCalculator.CalculateTotal()`로 합산

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\EVASION_SYSTEM_RULES.md`

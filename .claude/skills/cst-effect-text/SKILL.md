---
name: cst-effect-text
description: Use when displaying skill activation effect text on screen. Triggers: effect text, tooltip format, ShowSkillEffectText, DamageText, 효과 텍스트, 스킬 발동 텍스트
---

## 핵심 규칙 요약

- **표준 API**: `SkillEffect.ShowSkillEffectText(player, text, color, SkillEffectTextType)`
- `SkillEffectTextType`: Damage, Heal, Buff, Debuff, Special 등 구분
- 색상: 툴팁 색상 표준(`cst-tooltip`) 참조하여 일관성 유지
- 로컬 지역 표시 방식 - 해당 클라이언트에만 표시
- 액티브 스킬 발동 시 텍스트 필수, 패시브는 텍스트 금지

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SKILL_EFFECT_TEXT_STANDARD.md`

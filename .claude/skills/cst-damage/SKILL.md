---
name: cst-damage
description: Use when implementing or modifying damage-related skills. Triggers: damage, HitData, DamageMod, GetDamage, m_pierce, m_slash, m_blunt, 데미지, 공격력, 물리 데미지
---

## 핵심 규칙 요약

- **10종 데미지 타입**: 물리 5종(m_pierce, m_blunt, m_slash, m_chop, m_pickaxe) + 속성 5종(m_fire, m_frost, m_lightning, m_poison, m_spirit)
- 데미지 증가 스킬은 **모든 관련 타입**에 적용 필수 (일부만 적용 시 특정 무기 스킬 혜택 누락)
- **GetDamage 패치**: `HitData.GetDamage()`를 Postfix로 패치하여 최종 계산값에 보너스 합산
- MMO 연동 우선: `getParameter(Parameter.Strength)` 등 MMO 시스템을 통해 스탯 연동
- `SkillBonusCalculator.CalculateTotal()` 사용 필수 - 동일 효과 누적 합산

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\DAMAGE_SYSTEM_RULES.md`

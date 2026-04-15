---
name: cst-naming
description: Use when creating new skill IDs or key bindings. Triggers: skill ID, naming, 명명 규칙, skill name, 스킬 ID, 전문가 스킬 명명
---

## 핵심 규칙 요약

- **전문가 스킬**: `{type}_expert_{attr}` (예: `sword_expert_damage`)
- **일반 스킬**: `{weapon}_Step{tier}_{name}` (예: `bow_step6_critboost`)
- **루트 노드**: `{category}_root` (예: `melee_root`)
- **Step 번호**: 티어 레벨 숫자 (Step1 ~ Step7)
- 액티브 스킬 키 바인딩: R(원거리), G(근접 메인), H(보조), Y(직업)
- 전문가 제한: 전문가 스킬 ID에 `_expert_` 포함 필수

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SKILL_NAMING_RULES.md`

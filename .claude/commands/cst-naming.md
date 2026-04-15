# 스킬 명명 규칙 로드

스킬 ID 명명 규칙을 로드합니다.

## 로드할 문서

`C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SKILL_NAMING_RULES.md`

Read 도구로 위 파일을 읽고 스킬 명명에 적용하세요.

## 명명 규칙 요약

### 전문가 스킬
`{타입}_expert_{속성}`
- 타입: weapon, combat, production, speed, job
- 예시: `sword_expert_damage`, `bow_expert_precision`

### 일반 스킬
`{weapon/category}_Step{tier}_{skill_name}`
- 예시: `bow_step6_critboost`, `mace_step7_fury_hammer`

### 루트 노드
`{category}_root`
- 예시: `melee_root`, `ranged_root`, `defense_root`

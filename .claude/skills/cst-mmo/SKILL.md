---
name: cst-mmo
description: Use when integrating skills with EpicMMO system stats. Triggers: MMO, EpicMMO, getParameter, Tier1, EpicMMOSystem, Parameter.Strength, MMO 연동
---

## 핵심 규칙 요약

- **Tier 1 최우선**: `getParameter(Parameter.X)` 패치를 통한 MMO 스탯 연동
- **Tier 2 예외**: MMO가 지원하지 않는 특수 효과만 직접 패치 허용
- `getParameter` 반환값에 스킬 보너스를 더하여 MMO 시스템이 자동 처리
- 주요 파라미터: `Strength`(물리공격), `Agility`(속도/회피), `Body`(체력), `Intelligence`(마법)
- MMO 최대값 제한 존재 - `maxValueStrength` 등 Config 값 확인 필수

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\MMO_INTEGRATION_GUIDE.md`

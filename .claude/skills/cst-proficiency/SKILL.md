---
name: cst-proficiency
description: Use when implementing skill proficiency bonus or Valheim skill level display. Triggers: proficiency, 숙련도, skill level, 기술 레벨, 숙련도 보너스, bonus level
---

## 핵심 규칙 요약

- 스킬트리 노드 습득 시 해당 Valheim 기술에 보너스 레벨 추가
- UI 표시: "기본 레벨 +보너스" 형식
- **사망 시 보너스 레벨 유지** (기본 레벨만 감소)
- 보너스 레벨은 `KnownTexts` 방식으로 저장 (캐릭터 데이터에 영구 보존)
- 숙련도 패치: `Skills.GetSkillLevel()` Postfix에서 보너스 합산

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SKILL_PROFICIENCY_SYSTEM.md`

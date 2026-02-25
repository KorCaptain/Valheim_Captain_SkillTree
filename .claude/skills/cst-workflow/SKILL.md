---
name: cst-workflow
description: Use when starting to develop a new skill or modifying an existing one. Triggers: workflow, 개발 순서, new skill, 스킬 추가, 스킬 수정, 새 스킬, 개발 워크플로우
---

## 핵심 규칙 요약

- **스킬 변경 5종 세트**: Config → 효과(HarmonyPatch) → 툴팁 → DefaultLanguages → ConfigTranslations 동시 수정
- **개발 순서**: 로컬라이제이션 키 먼저 등록 → Config → 효과 구현 → 툴팁 → 검증
- 빌드 전 로컬라이제이션 검증 스크립트 필수 실행:
  ```
  cd CaptainSkillTree/scripts
  powershell -ExecutionPolicy Bypass -File validate_loc_keys.ps1
  ```
- 카테고리별 필수 참조 규칙 문서 확인 (데미지, 체력, 공속 등)

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SKILL_DEVELOPMENT_WORKFLOW.md`

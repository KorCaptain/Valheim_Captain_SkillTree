---
name: cst-localization
description: Use when adding or modifying localization keys for UI text or config descriptions. Triggers: localization, L.Get(), DefaultLanguages, ConfigTranslations, 로컬라이제이션, 다국어, 번역
---

## 핵심 규칙 요약

- **UI 다국어**: `Localization/DefaultLanguages.cs` - KO + EN 키 동시 등록 필수
- **Config 다국어**: `Localization/ConfigTranslations.cs` - 【】형식으로 번역 추가
- 코드 작성 전 반드시 `DefaultLanguages.cs`에 키 먼저 등록
- 빌드 전 검증 스크립트 실행: `scripts/validate_loc_keys.ps1`
- 하드코딩된 한글 텍스트 절대 금지 - 반드시 `L.Get("key")` 사용
- 최신 통합본 (2026-02-25): CLAUDE.md Rule 11, 12 및 Rule 3 통합

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\LOCALIZATION_GUIDE.md`

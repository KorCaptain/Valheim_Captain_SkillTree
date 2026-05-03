---
name: cst-config-guide
description: Use for comprehensive Config setup, ordering, tooltip integration, and multiplayer sync. Triggers: config guide, GetConfigDescription, Config ordering, 설정 가이드, Config 통합
---

## 핵심 규칙 요약

- **최신 통합본** (2026-02-25): CONFIG_RULES.md + CONFIG_MANAGEMENT_RULES.md 통합
- Config 키 명명: `Tier{n}_{한글스킬명}_{설정타입}`
- 초기화 순서: 전문가(Attack→Speed→Defense→Production) → 원거리(Bow→Staff→Crossbow) → 근접(Knife→Sword→Mace→Spear→Polearm) → 직업
- 구분선: `new ConfigDefinition("--- 섹션명 ---", "구분선")`
- 멀티플레이어 동기화: `ServerSync` 사용 - 서버 Config가 클라이언트에 자동 전파
- **업데이트 시 유저 값 보존 (R6)**: `DifficultyManager.ApplyVeryhardWithUserOverlay()` 자동 실행 — 기존 키 유저값 유지, 신규 키 VH 기본값, GameDifficulty = "UserSettings" 자동 설정

**전체 문서**: `rules/CONFIG_INIT_RULES.md` (R1~R6)

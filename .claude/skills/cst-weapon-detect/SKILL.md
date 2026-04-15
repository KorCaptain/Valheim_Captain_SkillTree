---
name: cst-weapon-detect
description: Use when checking weapon types or adding weapon detection logic. Triggers: WeaponHelper, IsUsing, IsUsingDagger, weapon type check, 무기 감지, 무기 타입, WeaponHelper
---

## 핵심 규칙 요약

- **`WeaponHelper` 클래스 반드시 사용** - 직접 `IsUsingXXX` 메서드 중복 작성 금지
- **3단계 우선순위**: Valheim 기본 스킬 타입 → 프리팹 이름 → 무기 이름 순서로 감지
- 모드 호환성: 다른 모드의 커스텀 무기도 프리팹/이름 기반으로 감지 가능
- 주요 메서드: `IsUsingDagger()`, `IsUsingSword()`, `IsUsingBow()`, `IsUsingShield()` 등
- 무기 감지 실패 시 항상 3단계 모두 확인 후 디버그 로그 출력

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\WEAPON_DETECTION_EXTENSIBILITY_GUIDE.md`

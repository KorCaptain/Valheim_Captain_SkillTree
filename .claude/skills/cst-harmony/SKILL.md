---
name: cst-harmony
description: Use when writing Harmony patches or encountering silent patch failures. Triggers: Harmony, HarmonyPatch, prefix, postfix, typeof, TargetMethod, 패치 실패, silent fail
---

## 핵심 규칙 요약

- **`typeof()` 대상**: 메서드가 **실제 정의된 클래스** 지정 (부모 클래스 지정 시 조용히 실패)
- Valheim 클래스 계층 주의: `Player` → `Humanoid` → `Character` 순 상속
- 패치 실패 확인: 에러 없이 미적용되는 경우 클래스 계층 확인
- Prefix: 원본 메서드 전 실행, Postfix: 원본 메서드 후 실행 (`__result` 수정 가능)
- `TargetMethod()` 사용 시 반환값이 `null`이면 Harmony.PatchAll() 전체 실패 주의

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\HARMONY_PATCH_TARGET_RULES.md`

---
name: cst-vfx-fix
description: Use when VFX or sound causes infinite loading or crashes. Triggers: VFX loading, infinite loop, 무한 로딩, VFX 버그, 사운드 크래시, infinite loading fix
---

## 핵심 규칙 요약

- ✅ **사용**: `VFXManager.PlayVFXMultiplayer()` (ZRoutedRpc 기반)
- ❌ **금지**: `VFXManager.PlayVFXHybrid()` (구버전 - 무한 로딩 유발)
- ❌ **금지**: `Instantiate()` + `AudioSource.PlayClipAtPoint()` 조합
- VFX 재생 전 반드시 `ZNetScene.instance.GetPrefab(name) != null` 확인
- 패시브 스킬에 VFX/SFX 적용 절대 금지
- 버프형 VFX(지속형): 별도 버프 VFX 가이드 참조

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\VFX_SOUND_INFINITE_LOADING_FIX.md`

---
name: cst-vfx-rules
description: Use when adding VFX or SFX to skills, or registering VFX prefabs. Triggers: VFX, ZNetScene, PlayVFXMultiplayer, SimpleVFX, prefab, 이펙트, 사운드, VFX 등록
---

## 핵심 규칙 요약

- **커스텀 VFX** (hit_01 등 asset/VFX/): `SimpleVFX` 사용
- **발헤임 기본 VFX** (vfx_blocked 등): `VFXManager.PlayVFXMultiplayer()` 사용
- ❌ `VFXManager.PlayVFXHybrid()` 사용 금지 (무한 로딩 유발)
- ❌ `Instantiate()` + `AudioSource.PlayClipAtPoint()` 직접 조합 금지
- **패시브 스킬**: VFX/SFX 사용 금지, 텍스트 표시만
- ZNetScene 등록: `Plugin.cs`의 `UnifiedVfxPrefabRegisterPatch`에서 자동 처리
- 내부 VFX 목록: `VFX/Valheim_prefab.txt` / 커스텀: `asset/VFX/`

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ZNETSCENE_VFX_RULES.md`

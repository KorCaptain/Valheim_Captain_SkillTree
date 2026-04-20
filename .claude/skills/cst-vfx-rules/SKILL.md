---
name: cst-vfx-rules
description: Use when adding VFX or SFX to skills, or registering VFX prefabs. Triggers: VFX, ZNetScene, PlayVFXMultiplayer, SimpleVFX, RegisterValheimVFXAsCustom, prefab, 이펙트, 사운드, VFX 등록, 멀티플레이어 VFX
---

## VFX 타입 3분류

| 타입 | 메서드 |
|------|--------|
| 커스텀 VFX / 발헤임 고정 위치 | `VFXManager.PlayVFXMultiplayer()` |
| 발헤임 VFX — 플레이어 추적 + 멀티플레이어 | `RegisterValheimVFXAsCustom` 등록 후 `SimpleVFX.PlayOnPlayer()` |
| 발헤임 VFX — 로컬 전용 | 순수 `ZNetScene.GetPrefab` + `Instantiate` |

## 핵심 규칙

- ❌ `VFXManager.PlayVFXHybrid()` 금지 (무한 로딩)
- ❌ 발헤임 기본 VFX를 `player.transform` 자식 직접 부착 금지
- ❌ 발헤임 기본 VFX 1초 이내 연속 호출 금지
- ❌ 패시브 스킬 VFX 금지 (텍스트만)
- ✅ ZNet 발사체 → 비활성 프리팹 트릭 사용
- ✅ 오브젝트 제거 → `SetActive(false)` 후 `Destroy`
- `RegisterValheimVFXAsCustom` 등록 위치: `SimpleVFX_ZNetScene_Awake_Patch.Postfix`

**전체 문서**: `md/ZNETSCENE_VFX_RULES.md`

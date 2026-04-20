---
name: cst-vfx-fix
description: Use when VFX or sound causes infinite loading, duplicate VFX, or crashes. Triggers: VFX loading, infinite loop, 무한 로딩, VFX 버그, 중복 VFX, 두개 생겨, 사운드 크래시
---

## 무한 로딩 원인별 해결

| 원인 | 해결 |
|------|------|
| `PlayVFXHybrid` 사용 | `PlayVFXMultiplayer`로 교체 |
| 발헤임 기본 VFX 연속 호출 (<1초) | 각 타격마다 다른 VFX |
| 발헤임 기본 VFX → player.transform 자식 부착 | `RegisterValheimVFXAsCustom` 후 `SimpleVFX.PlayOnPlayer` |
| ZNet 발사체 즉시 Instantiate | 비활성 프리팹 트릭 |
| Destroy만 호출 | `SetActive(false)` 후 `Destroy` |

## 중복 VFX 원인 (두 개 생기는 경우)

`SimpleVFX.PlayOnPlayer`가 `ZRoutedRpc.Everybody`로 RPC 전송 시 자기 수신으로 중복 생성.  
→ SimpleVFX.cs 내부에서 `_recentLocalCreations` 키를 RPC 전송 **전**에 등록하여 방지.  
→ 코드에 이미 적용됨. 재발 시 SimpleVFX.cs 614-623줄 확인.

## RegisterValheimVFXAsCustom 사용 시기

발헤임 기본 VFX를 **플레이어 추적 + 다른 플레이어에게도 표시** 필요할 때:
```csharp
// SimpleVFX_ZNetScene_Awake_Patch.Postfix 안에서만
SimpleVFX.RegisterValheimVFXAsCustom("vfx_GoblinShield");
// 재생
SimpleVFX.PlayOnPlayer(player, "vfx_GoblinShield", duration, offset);
```

**전체 문서**: `md/ZNETSCENE_VFX_RULES.md`

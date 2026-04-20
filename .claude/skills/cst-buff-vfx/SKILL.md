---
name: cst-buff-vfx
description: Use when implementing persistent buff-type VFX that follows the character. Triggers: 버프 VFX, buff visual, 지속 VFX, follow VFX, 버프 이펙트, buff effect, 플레이어 따라다니는 VFX
---

## 버프형 VFX 2종

| 유형 | 프리팹 예시 | 메서드 | 지속 |
|------|------------|--------|------|
| 커스텀 VFX 추적 | buff_01, statusailment_01_aura | `VFXManager.GetVFXPrefab` + `Instantiate` + `SetParent` | Dictionary로 수동 관리 |
| 발헤임 기본 VFX 추적 + 멀티플레이어 | vfx_GoblinShield, vfx_Potion_health_medium | `RegisterValheimVFXAsCustom` 후 `SimpleVFX.PlayOnPlayer` | duration으로 자동 소멸 |

## 커스텀 VFX 수동 관리 패턴

```csharp
// 활성화
private static Dictionary<Player, GameObject> _statusEffects = new();
var prefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
var instance = UnityEngine.Object.Instantiate(prefab, player.transform.position, Quaternion.identity);
instance.transform.SetParent(player.transform, false);
instance.transform.localPosition = Vector3.up * 2.0f;
_statusEffects[player] = instance;

// 종료 시
if (_statusEffects.TryGetValue(player, out var go) && go != null)
    UnityEngine.Object.Destroy(go);
_statusEffects.Remove(player);
```

## 발헤임 기본 VFX 추적 패턴 (자동 소멸, 멀티플레이어 동기화)

```csharp
// ZNetScene.Awake Postfix에 등록
SimpleVFX.RegisterValheimVFXAsCustom("vfx_GoblinShield");

// 재생 (duration 후 자동 소멸, 모든 클라이언트에 RPC 브로드캐스트)
SimpleVFX.PlayOnPlayer(player, "vfx_GoblinShield", duration, new Vector3(0f, 1.2f, 0f));
```

- ❌ 패시브 스킬 VFX 금지
- ✅ 버프 종료/사망 시 반드시 Dictionary 정리

**전체 문서**: `md/버프형_VFX.md` / `md/ZNETSCENE_VFX_RULES.md`

# VFX/SFX 통합 규칙

> 참조: 내부 VFX 프리팹 목록 → `VFX/Valheim_prefab.txt` / 커스텀 VFX → `asset/VFX/`

---

## 🎯 VFX 타입 4분류 — 사용 메서드 결정표

| 타입 | 예시 | 메서드 | 이유 |
|------|------|--------|------|
| **커스텀 VFX** (asset/VFX/) | hit_01, debuff, buff_02a | `VFXManager.PlayVFXMultiplayer()` | RPC 멀티플레이어 동기화 |
| **발헤임 기본 VFX — 고정 위치** | fx_greenroots_projectile_hit, fx_siegebomb_explosion | `VFXManager.PlayVFXMultiplayer()` | RPC 멀티플레이어 동기화 |
| **발헤임 기본 VFX — 플레이어 추적 + 멀티플레이어** | vfx_GoblinShield, fx_shield_start, vfx_Potion_health_medium | `RegisterValheimVFXAsCustom` 등록 후 `SimpleVFX.PlayOnPlayer()` | ZNetView 제거 + RPC 브로드캐스트 |
| **발헤임 기본 VFX — 코루틴 볼리/버스트 (플레이어 추적)** | fx_batteringram_fire | `RegisterValheimVFXAsCustom` 등록 후 `SimpleVFX.PlayFollowing()` | ZNetView 제거 + 코루틴 내 반복 호출 안전 |

---

## ✅ 사용 패턴

### 1. 커스텀 VFX / 발헤임 기본 VFX (고정 위치)

```csharp
// VFX + 사운드 동시 재생
VFXManager.PlayVFXMultiplayer("debuff", "sfx_morgen_alert", position, Quaternion.identity, 3f);

// VFX만 (사운드 없음)
VFXManager.PlayVFXMultiplayer("flash_round_ellow", "", position, Quaternion.identity, 2f);
```

### 2. 발헤임 기본 VFX — 플레이어 추적 + 멀티플레이어 동기화

**등록** (`SimpleVFX_ZNetScene_Awake_Patch.Postfix` 안에서만 호출):
```csharp
SimpleVFX.RegisterValheimVFXAsCustom("vfx_GoblinShield");
SimpleVFX.RegisterValheimVFXAsCustom("fx_shield_start");
```

**재생**:
```csharp
// 플레이어를 N초간 따라다님 + 모든 클라이언트에 RPC 브로드캐스트
SimpleVFX.PlayOnPlayer(player, "vfx_GoblinShield", duration, new Vector3(0f, 1.2f, 0f));
```

**내부 동작**: `RegisterValheimVFXAsCustom`이 ZNetScene 원본을 비활성 클론으로 복제 → ZNetView/ZSyncTransform/ZSyncAnimation 제거 → `_customVFXNames` 등록 → 이후 `PlayOnPlayer` 호출 시 안전한 `Destroy` + `ZRoutedRpc.Everybody` RPC 자동 브로드캐스트

### 3. 순차 VFX (코루틴 딜레이)

시전 이펙트 → 딜레이 → 유지 이펙트 패턴:
```csharp
// 시전 이펙트 즉시
SimpleVFX.PlayOnPlayer(player, "fx_shield_start", 3f);

// N초 후 유지 이펙트
SkillTreeInputListener.Instance?.StartCoroutine(DelayedVFX(player));

private static IEnumerator DelayedVFX(Player player)
{
    yield return new WaitForSeconds(2f);
    if (!IsActive(player)) yield break;
    float duration = TotalDuration - 2f;
    if (duration <= 0f) yield break;
    SimpleVFX.PlayOnPlayer(player, "vfx_GoblinShield", duration, new Vector3(0f, 1.2f, 0f));
}
```

### 4. 발헤임 기본 VFX — 코루틴 볼리/버스트 (플레이어 추적)

볼리처럼 **코루틴 안에서 반복 재생** + **플레이어를 따라다녀야** 할 때.  
`PlayOnPlayer`와 달리 RPC 브로드캐스트 없이 로컬 전용으로 부착.

**등록** (`SimpleVFX_ZNetScene_Awake_Patch.Postfix` 안에서만):
```csharp
SimpleVFX.RegisterValheimVFXAsCustom("fx_batteringram_fire");
```

**재생** (코루틴 내부):
```csharp
// 발사 직후 즉시
SimpleVFX.PlayFollowing("fx_batteringram_fire", player.transform, new Vector3(0f, 1.5f, 0f), 1.5f);

// 코루틴 루프 중간지점
yield return new WaitForSeconds(interval);
SimpleVFX.PlayFollowing("fx_batteringram_fire", player.transform, new Vector3(0f, 1.5f, 0f), 1.5f);
```

**내부 동작**: `_cachedPrefabs`에서 ZNetView 제거된 클린 클론 찾기 → `Instantiate(clone, player.transform)` → `Destroy(vfxObj, duration)` — ZNetView 없으므로 ZDO 미생성, 던전 입장 시 무한 로딩 없음.

> ⚠️ **치명적 함정 — `_customVFXNames` 사전 등록 금지**
>
> ```csharp
> // ❌ 절대 금지: 정적 HashSet에 직접 추가
> private static readonly HashSet<string> _customVFXNames = new HashSet<string>
> {
>     "fx_batteringram_fire",  // ← RegisterValheimVFXAsCustom이 Contains 체크로 조기 리턴!
>     ...
> };
> ```
> `RegisterValheimVFXAsCustom`은 `_customVFXNames.Contains` 시 즉시 리턴.  
> 클린 클론을 만들지 않아 나중에 `FindPrefabInResources`가 ZNetView 있는 원본을 캐시 → 더 위험.  
> **반드시 `RegisterValheimVFXAsCustom`에게만 `_customVFXNames.Add` 를 맡길 것.**

### 5. 로컬 전용 발헤임 기본 VFX (멀티플레이어 동기화 불필요)

```csharp
// 단순 Instantiate — 발헤임이 알아서 정리
var prefab = ZNetScene.instance?.GetPrefab("smokebomb_explosion");
if (prefab != null)
    UnityEngine.Object.Instantiate(prefab, playerPos, Quaternion.identity);
```

### 6. ZNet 발사체 프리팹을 로컬 VFX로 사용 (비활성 트릭)

ZNetView가 내장된 발사체를 VFX로 소환 시 → ZNetView.Awake 차단 필수:
```csharp
bool wasActive = prefab.activeSelf;
prefab.SetActive(false);                                          // Awake() 차단
var obj = UnityEngine.Object.Instantiate(prefab, pos, rot);
prefab.SetActive(wasActive);                                      // 원본 복구
UnityEngine.Object.DestroyImmediate(obj.GetComponent<ZNetView>());
UnityEngine.Object.DestroyImmediate(obj.GetComponent<ZSyncTransform>());
foreach (var col in obj.GetComponentsInChildren<Collider>()) col.enabled = false;
obj.SetActive(true);                                              // 비주얼만 활성화
// 제거 시
obj.SetActive(false);
UnityEngine.Object.Destroy(obj);
```

---

## 🚨 무한 로딩 방지 규칙

### 금지 사항

| 금지 | 대체 |
|------|------|
| `VFXManager.PlayVFXHybrid()` | `PlayVFXMultiplayer()` |
| `Instantiate()` + `AudioSource.PlayClipAtPoint()` 직접 조합 | `PlayVFXMultiplayer()` |
| 발헤임 기본 VFX를 1초 이내 연속 호출 | 각 타격마다 다른 VFX 사용 |
| 발헤임 기본 VFX를 `player.transform` 자식으로 직접 부착 | `RegisterValheimVFXAsCustom` 후 `PlayFollowing` 또는 `PlayOnPlayer` |
| ZNet 발사체 즉시 Instantiate → DestroyImmediate(znv) | 비활성 트릭 (§6) |
| 오브젝트 Destroy만 호출 | `SetActive(false)` 후 `Destroy` |
| 패시브 스킬에 VFX 사용 | 텍스트 표시만 허용 |
| `_customVFXNames` 정적 HashSet에 발헤임 VFX 직접 추가 | `RegisterValheimVFXAsCustom`만 사용 (§4 주의사항) |
| 코루틴 볼리에서 `PlayVFXFollowPlayer(player, name, "soundName", duration)` OLD 오버로드 | `SimpleVFX.PlayFollowing(name, player.transform, offset, duration)` |

### 발헤임 기본 VFX 연속 호출 금지

ZRoutedRpc 패킷 충돌 → 무한 로딩:
```csharp
// ❌ 같은 발헤임 VFX 연속 호출
for (int i = 0; i < 5; i++)
    VFXManager.PlayVFXMultiplayer("vfx_sledge_iron_hit", "", position, rotation, 1.5f);

// ✅ 타격마다 다른 VFX
string[] vfxList = { "hit_01", "hit_02", "hit_03", "hit_04", "fx_siegebomb_explosion" };
for (int i = 0; i < 5; i++)
    VFXManager.PlayVFXMultiplayer(vfxList[i], "", position, rotation, 1.5f);
```

---

## 🌐 RPC 멀티플레이어 동기화 규칙

`SimpleVFX.PlayOnPlayer`는 커스텀 VFX(`_customVFXNames` 포함)에 한해 `ZRoutedRpc.Everybody`로 자동 브로드캐스트.

**중복 방지**: `_recentLocalCreations` 키는 RPC 전송 **전**에 등록해야 로컬서버 환경의 즉시 루프백 중복을 방지할 수 있음 (SimpleVFX.cs 내부 처리됨).

---

## 📦 VFX 프리팹 목록

### 커스텀 VFX (asset/VFX/ — SimpleVFX 등록됨)

**버프/디버프**
- `buff_01` — 공격력 증가 버프
- `buff_02a` — 아처 멀티샷 활성화
- `buff_03a` — 캐릭터 힐 이펙트
- `buff_03a_aura` — 녹색 방울 힐링 도트
- `debuff` — 붉은 화살표 디버프 (반복, 종료코드 필요)
- `debuff_03` — 하얀 바닥 마법진
- `debuff_03_aura` — 하얀 버프형 (종료코드 필요)
- `statusailment_01` — 상태이상 효과
- `statusailment_01_aura` — 캐릭터 머리 버프 표시 (종료 필요)

**타격/전투**
- `hit_01` ~ `hit_04` — 기본 타격 효과

**특수**
- `healing` — 녹색 안개 힐링 (반복, 종료코드 필요)
- `taunt` — 도발 효과
- `plexus` — 번개 파란 라운드 (반복, 종료코드 필요)
- `LevelUpVFX2` — MMO 레벨업

**미사용 (등록 예정)**
- `area_circles_blue`, `area_fire_red`, `area_heal_green`, `area_magic_multicolor`, `area_star_ellow`
- `shine_blue`, `shine_pink`, `shine_ellow`
- `sparkle_ellow`, `confetti_blast_multicolor`, `confetti_directional_multicolor`
- `flash_blue_purple`, `flash_ellow`, `flash_ellow_pink`, `flash_magic_blue_pink`, `flash_magic_ellow_blue`
- `flash_round_ellow`, `flash_star_ellow_purple`, `water_blast_blue`, `water_blast_green`
- `guard_01`, `dust_permanently_blue`

### 발헤임 기본 VFX (주요 사용 목록)

- `smokebomb_explosion` — 스모크 연막
- `vfx_GodExplosion` — 녹색 폭파
- `fx_Fader_CorpseExplosion` — 푸른 큰 안개
- `fx_siegebomb_explosion` — 히트 폭파
- `charred_fireball_projectile` — 독/힐 이펙트
- `fx_greenroots_projectile_hit` — 힐/독 히트
- `fx_lightningstaffprojectile_hit` — 번개 히트
- `fx_shaman_fireball_expl` — 핑크 히트
- `fx_shaman_protect` — 푸른 회오리
- `fx_shield_start` — 쉴드/버프 시작 (**RegisterValheimVFXAsCustom 등록됨**)
- `fx_shieldgenerator_domehit` — 그린 방어
- `staff_greenroots_projectile` — 그린 볼 뿌리
- `fx_Fader_Spin` — 광역 푸른 원
- `dvergerstaffheal_aoe` — 좁은 범위 힐
- `vfx_GoblinShield` — 고블린 방어 돔 (**RegisterValheimVFXAsCustom 등록됨**)

---

## ✅ 구현 체크리스트

액티브 스킬 VFX 구현 시:

- [ ] 고정 위치 VFX → `PlayVFXMultiplayer()` 사용 (PlayVFXHybrid 금지)
- [ ] 중복 사운드 호출 없음 (한 번의 PlayVFXMultiplayer에 통합)
- [ ] 발헤임 기본 VFX 연속 호출 없음 (각 타격마다 다른 VFX)
- [ ] 발헤임 기본 VFX 로컬 전용 → 순수 `Instantiate` 사용
- [ ] ZNet 발사체를 VFX로 사용 시 → 비활성 프리팹 트릭 적용
- [ ] 발헤임 기본 VFX를 플레이어 추적 + 멀티플레이어 → `RegisterValheimVFXAsCustom` 등록 후 `SimpleVFX.PlayOnPlayer`
- [ ] 오브젝트 제거 시 `SetActive(false)` 후 `Destroy`
- [ ] 패시브 스킬 VFX 없음 (텍스트만)

---

## 📚 관련 문서

- `버프형_VFX.md` — 지속 버프 VFX Dictionary 관리 패턴
- `VFX/Valheim_prefab.txt` — 발헤임 전체 VFX 프리팹 목록
- `ACTIVE_SKILL_SYSTEM.md` — 액티브 스킬 VFX 적용

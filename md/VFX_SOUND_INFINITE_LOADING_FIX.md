# VFX/사운드 무한 로딩 방지 규칙

## 🚨 핵심 원칙
**VFX/사운드 시스템은 무한 로딩을 유발할 수 있으므로 반드시 아래 규칙을 준수해야 합니다.**

---

## 📋 필수 준수 사항

### 1. **VFX/사운드 재생 메서드 통일**
- ✅ **사용**: `VFXManager.PlayVFXMultiplayer()` (ZRoutedRpc 기반)
- ❌ **금지**: `VFXManager.PlayVFXHybrid()` (구버전, 무한 로딩 유발)
- ❌ **금지**: 직접 `Instantiate()` + `AudioSource.PlayClipAtPoint()` 조합

**올바른 사용 예시**:
```csharp
// VFX + 사운드 동시 재생
VFXManager.PlayVFXMultiplayer("debuff", "sfx_morgen_alert", position, rotation, 3f);

// VFX만 재생 (사운드 없음)
VFXManager.PlayVFXMultiplayer("flash_round_ellow", "", position, rotation, 2f);

// 사운드만 재생 (VFX 없음)
VFXManager.PlayVFXMultiplayer("", "sfx_dverger_heal_finish", position, rotation, 1f);
```

**잘못된 예시** (무한 로딩 유발):
```csharp
// ❌ PlayVFXHybrid 사용 금지
VFXManager.PlayVFXHybrid("sfx_dverger_heal_finish", casterPos, Quaternion.identity, 2f, "staff_heal_sound", 0.8f);

// ❌ 중복 사운드 호출
VFXManager.PlayVFXMultiplayer("shaman_heal_aoe", "sfx_dverger_heal_finish", casterPos, caster.transform.rotation, 3f);
VFXManager.PlayVFXHybrid("sfx_dverger_heal_finish", casterPos, Quaternion.identity, 2f, "staff_heal_sound", 0.8f); // 중복!
```

---

### 2. **커스텀 VFX 대소문자 규칙**

#### 문제 상황
- **VFX 파일명**: `debuff`, `flash_round_ellow`, `statusailment_01` (소문자)
- **GameObject명**: `Debuff`, `Flash_round_ellow`, `Statusailment_01` (대문자 시작)
- **등록 시**: 매핑 테이블에서 소문자 → 대문자 변환
- **검색 시**: 대소문자 정확히 일치해야 검색 성공

#### 해결 방법
**VFXPrefabRegistry.cs**의 두 가지 안전장치:

**1) 등록 시 양쪽 이름 모두 등록** (TryRegisterSingleVFX)
```csharp
// 로컬 캐시에 저장 (원본 이름과 프리팹 이름 양쪽 모두 등록)
registeredVFXPrefabs[vfxName] = vfxPrefab;

// 프리팹의 실제 이름도 등록 (대소문자 차이로 인한 검색 실패 방지)
if (vfxPrefab.name != vfxName)
{
    registeredVFXPrefabs[vfxPrefab.name] = vfxPrefab;
    Plugin.Log.LogDebug($"[VFX Registry] 양쪽 이름 등록: '{vfxName}' & '{vfxPrefab.name}'");
}
```

**2) 검색 시 대소문자 무시 폴백** (GetRegisteredVFX)
```csharp
public static GameObject GetRegisteredVFX(string vfxName)
{
    if (string.IsNullOrEmpty(vfxName)) return null;

    // 1. 정확한 이름으로 검색 (빠른 속도)
    if (registeredVFXPrefabs.TryGetValue(vfxName, out GameObject prefab))
    {
        return prefab;
    }

    // 2. 대소문자 무시 검색 (폴백 - 안전망)
    foreach (var kvp in registeredVFXPrefabs)
    {
        if (string.Equals(kvp.Key, vfxName, System.StringComparison.OrdinalIgnoreCase))
        {
            Plugin.Log.LogDebug($"[VFX Registry] 대소문자 무시 검색 성공: '{vfxName}' → '{kvp.Key}'");
            return kvp.Value;
        }
    }

    return null;
}
```

---

### 3. **VFX 검색 우선순위**

**VFXManager.RPC_PlayVFX** 메서드의 2단계 검색:
```csharp
// VFX 재생 (커스텀 VFX 우선, Valheim 기본 폴백)
if (!string.IsNullOrEmpty(vfxName))
{
    // 1. 커스텀 VFX 먼저 확인 (VFXPrefabRegistry)
    var vfxPrefab = VFXPrefabRegistry.GetRegisteredVFX(vfxName);

    // 2. 없으면 Valheim 기본 프리팹 확인 (ZNetScene)
    if (vfxPrefab == null && ZNetScene.instance != null)
    {
        vfxPrefab = ZNetScene.instance.GetPrefab(vfxName);
    }

    // 3. VFX 재생
    if (vfxPrefab != null)
    {
        var vfxObj = UnityEngine.Object.Instantiate(vfxPrefab, position, rotation);
        UnityEngine.Object.Destroy(vfxObj, destroyAfter);
        Plugin.Log.LogInfo($"[VFX RPC] ✅ VFX 재생 성공: {vfxName}");
    }
    else
    {
        Plugin.Log.LogWarning($"[VFX RPC] ⚠️ VFX 프리팹을 찾을 수 없음: '{vfxName}'");
    }
}
```

**검색 순서**:
1. **VFXPrefabRegistry** (커스텀 VFX) - 대소문자 양쪽 등록 + 폴백 검색
2. **ZNetScene** (Valheim 기본 VFX) - 공식 프리팹

---

### 4. **중복 VFX/사운드 호출 방지**

#### 잘못된 패턴 (무한 로딩 유발)
```csharp
// 1. 스킬 발동 시 이펙트 및 사운드
VFXManager.PlayVFXMultiplayer("shaman_heal_aoe", "sfx_dverger_heal_finish", casterPos, caster.transform.rotation, 3f);

// 2. 시전자 효과음 (중복!)
VFXManager.PlayVFXHybrid("sfx_dverger_heal_finish", casterPos, Quaternion.identity, 2f, "staff_heal_sound", 0.8f);
```

#### 올바른 패턴
```csharp
// 1. 스킬 발동 시 이펙트 및 사운드 (하나로 통합)
VFXManager.PlayVFXMultiplayer("shaman_heal_aoe", "sfx_dverger_heal_finish", casterPos, caster.transform.rotation, 3f);
Plugin.Log.LogInfo("[지팡이 힐] shaman_heal_aoe VFX/사운드 재생");
```

---

### 5. **🚨 발헤임 기본 VFX 연속 호출 금지 (무한 로딩 유발)**

#### 문제 상황
**발헤임 기본 VFX**(ZNetScene에서 로드)를 **짧은 시간 내에 연속으로 호출하면 무한 로딩 발생**

#### 원인
- 발헤임 기본 VFX는 내부적으로 네트워크 동기화 및 리소스 관리 시스템을 가짐
- 같은 VFX를 짧은 시간(~1초 이내) 내에 2회 이상 호출 시 ZRoutedRpc 패킷 충돌 발생
- 네트워크 패킷 큐가 손상되어 무한 로딩 상태 진입

#### 잘못된 예시 (무한 로딩 유발)
```csharp
// ❌ 같은 발헤임 기본 VFX를 연속 호출
for (int i = 0; i < 5; i++)
{
    VFXManager.PlayVFXMultiplayer("vfx_sledge_iron_hit", "sfx_sledge_iron_hit", position, rotation, 1.5f);
    yield return new WaitForSeconds(0.8f); // 짧은 간격으로 반복 → 무한 로딩!
}
```

#### 올바른 해결 방법

**방법 1: 각 타격마다 다른 VFX 사용** (권장)
```csharp
// ✅ 각 타격마다 고유한 VFX 사용 (중복 방지)
string[] vfxList = { "hit_01", "hit_02", "hit_03", "hit_04", "fx_siegebomb_explosion" };
for (int i = 0; i < 5; i++)
{
    VFXManager.PlayVFXMultiplayer(vfxList[i], "sfx_sledge_iron_hit", position, rotation, 1.5f);
    yield return new WaitForSeconds(0.8f); // 안전!
}
```

**방법 2: 커스텀 VFX 사용**
```csharp
// ✅ 커스텀 VFX는 연속 호출 가능 (VFXPrefabRegistry 관리)
for (int i = 0; i < 5; i++)
{
    VFXManager.PlayVFXMultiplayer("hit_01", "sfx_sledge_iron_hit", position, rotation, 1.5f);
    yield return new WaitForSeconds(0.8f); // 커스텀 VFX는 안전
}
```

**방법 3: 호출 간격 충분히 확보 (비권장)**
```csharp
// ⚠️ 호출 간격을 2초 이상 확보하면 가능하지만 느림
VFXManager.PlayVFXMultiplayer("vfx_sledge_iron_hit", "", position, rotation, 1.5f);
yield return new WaitForSeconds(2.0f); // 느리지만 안전
```

#### 발헤임 기본 VFX vs 커스텀 VFX 구분

**발헤임 기본 VFX** (연속 호출 시 무한 로딩):
- ZNetScene.instance.GetPrefab()에서 로드되는 모든 VFX
- 예시: `vfx_sledge_iron_hit`, `fx_siegebomb_explosion`, `smokebomb_explosion`
- ZNETSCENE_VFX_RULES.md의 "발헤임 기본 이팩트" 섹션 참조

**커스텀 VFX** (연속 호출 안전):
- VFXPrefabRegistry에 등록된 VFX
- 예시: `hit_01`, `hit_02`, `debuff`, `buff_02a`
- ZNETSCENE_VFX_RULES.md의 "등록된 VFX 프리팹 목록" 섹션 참조

#### 무한 로딩 테스트 성공 사례
**분노의 망치 스킬** (MaceSkills.FuryHammer.cs):
- **문제**: `vfx_sledge_iron_hit`를 5연타에서 중복 사용 → 무한 로딩
- **해결**: 각 타격마다 다른 VFX 사용 (`flash_round_ellow`, `water_blast_blue`, `flash_star_ellow_purple`, `fx_siegebomb_explosion`)
- **결과**: 무한 로딩 해결 ✅

---

### 6. **🚨 발헤임 기본 VFX 로컬 전용 표현 방법 (VFXManager 사용 불가)**

#### 문제 상황
**발헤임 기본 VFX**(ZNetScene에서 로드)를 **VFXManager.PlayVFX()로 호출하면 무한 로딩 발생**

#### 원인
1. **코루틴 실패 시 자동 정리 없음**:
   - `Plugin.Instance?.StartCoroutine(SafeDestroyVFX())` (VFXManager.cs 라인 508)
   - `Plugin.Instance`가 `null`이면 코루틴이 시작되지 않음
   - SafeDestroyVFX의 모든 정리 로직(플레이어 사망 체크, ZNetView 제거, ParticleSystem 정지)이 **절대 실행 안 됨**
   - `vfxObject`가 씬에 영구 저장되어 무한 로딩 발생

2. **ZNetView 이중 제거 로직의 모순**:
   - PlayVFX에서 `Destroy(znetView)` 호출 (큐에 들어감, 즉시 제거 아님)
   - SafeDestroyVFX에서 `DestroyImmediate(znetView)` 호출 (즉시 제거)
   - 코루틴이 시작되지 않으면 ZNetView가 삭제 큐에만 들어가고 실제로는 제거 안 됨

3. **smokebomb_explosion의 복잡한 구조**:
   - 발헤임 기본 프리팹 특성: ZNetView 내재, 복잡한 OnNetworkSpawn 핸들러
   - Destroy 큐에 들어가도 ZNetView가 계속 추적 가능
   - VFXManager의 복잡한 정리 로직이 오히려 방해

#### 잘못된 예시 (무한 로딩 유발)
```csharp
// ❌ 발헤임 기본 VFX를 VFXManager.PlayVFX로 호출
VFXManager.PlayVFX("smokebomb_explosion", playerPos, Quaternion.identity, 5f);

// ❌ 발헤임 기본 VFX를 VFXManager.PlayVFXMultiplayer로 호출
VFXManager.PlayVFXMultiplayer("smokebomb_explosion", "", playerPos, Quaternion.identity, 5f);
```

#### 올바른 해결 방법: 순수 Instantiate 방식 (권장)

**발헤임 기본 VFX는 VFXManager를 거치지 않고 직접 Instantiate**
```csharp
// ✅ 발헤임 기본 프리팹 순수 표현 (간단하고 안전)
private static void CreateSmokeEffect(Player player)
{
    try
    {
        Vector3 playerPos = player.transform.position;

        // 발헤임 기본 smokebomb_explosion 프리팹 순수 표현
        var znetScene = ZNetScene.instance;
        if (znetScene != null)
        {
            var smokePrefab = znetScene.GetPrefab("smokebomb_explosion");
            if (smokePrefab != null)
            {
                // 단순 Instantiate만 (발헤임이 알아서 처리)
                UnityEngine.Object.Instantiate(smokePrefab, playerPos, Quaternion.identity);
                Plugin.Log.LogInfo($"[스킬명] smokebomb_explosion 연막 효과 생성 (순수 표현)");
                return;
            }
        }

        // 프리팹 로드 실패 시 메시지만
        player.Message(MessageHud.MessageType.Center, "💨 연막!");
        Plugin.Log.LogWarning("[스킬명] smokebomb_explosion 프리팹 없음 - 메시지만 표시");
    }
    catch (System.Exception ex)
    {
        Plugin.Log.LogError($"[스킬명] 연막 효과 생성 실패: {ex.Message}");
    }
}
```

#### 발헤임 기본 VFX vs 커스텀 VFX 사용 방법 비교

| VFX 타입 | 사용 메서드 | 이유 |
|---------|-----------|------|
| **발헤임 기본 VFX** | `VFXManager.PlayVFXMultiplayer()` | 멀티플레이어 동기화 + 안전한 정리 |
| **커스텀 VFX** | `SimpleVFX` | 커스텀 AssetBundle VFX 전용 처리 |

**발헤임 기본 VFX 예시**:
- `smokebomb_explosion`, `vfx_sledge_iron_hit`, `fx_siegebomb_explosion`
- `vfx_GodExplosion`, `fx_Fader_CorpseExplosion`, `fx_shaman_fireball_expl`
- ZNETSCENE_VFX_RULES.md의 "발헤임 기본 이팩트" 섹션 참조

**커스텀 VFX 예시**:
- `hit_01`, `hit_02`, `debuff`, `buff_02a`, `flash_blue_purple`
- VFXPrefabRegistry에 등록된 모든 VFX
- ZNETSCENE_VFX_RULES.md의 "등록된 VFX 프리팹 목록" 섹션 참조

#### 무한 로딩 해결 성공 사례
**로그 그림자 일격 스킬** (RogueSkills.cs):
- **문제**: `smokebomb_explosion`을 VFXManager.PlayVFX로 호출 → 무한 로딩
- **해결**: 순수 Instantiate 방식으로 변경 (발헤임 네이티브 정리 활용)
- **결과**: 무한 로딩 해결 ✅

#### 주의 사항
- **로컬 전용 효과에만 사용**: 멀티플레이어 동기화가 필요 없는 경우만
- **발헤임 기본 VFX에만 적용**: 커스텀 VFX는 VFXManager.PlayVFXMultiplayer 사용
- **정리 코드 불필요**: 발헤임이 자동으로 정리하므로 Destroy 호출하지 않음

---

### 9. **🚨 ZNet 발사체 프리팹을 로컬 전용 VFX로 사용하는 방법 (비활성 프리팹 트릭)**

#### 문제 상황
`staff_fireball_projectile`처럼 **ZNetView가 내장된 발사체 프리팹**을 7개, 0.3초 간격으로 연속 소환하면:
1. `Instantiate(prefab)` → `ZNetView.Awake()` 즉시 실행 → ZDO가 ZDOMan에 등록
2. `DestroyImmediate(znv)` → ZNetView 컴포넌트 제거되지만 **ZDO는 ZDOMan에 고아로 잔존**
3. 사망/리스폰 시 → ZDOMan이 고아 ZDO 동기화 대기 → **무한 로딩**

#### 근본 원인
`DestroyImmediate(znv)`로 ZNetView를 제거해도 이미 `Awake()`에서 등록된 ZDO는 ZDOMan에 남아 있음.  
`Destroy(obj)` 시 `ZNetView.OnDestroy()`가 호출되지 않으므로 ZDO가 영구 고아 상태가 됨.

#### 해결 방법: 비활성 프리팹 인스턴스화 트릭
**Unity 규칙**: 비활성 상태(SetActive(false))로 Instantiate된 오브젝트는 **Awake()가 호출되지 않음**  
→ 프리팹을 비활성화한 채로 Instantiate → ZNetView/ZSyncTransform/Projectile 전부 DestroyImmediate → SetActive(true)  
→ ZNetView.Awake()가 실행된 적 없으므로 ZDO 미등록 → 안전하게 Destroy 가능

```csharp
// ✅ ZNet 발사체 프리팹을 로컬 전용 VFX로 소환
var prefab = ZNetScene.instance?.GetPrefab("staff_fireball_projectile");
if (prefab != null)
{
    // ① 프리팹 비활성화 → Awake() 차단 (ZNetView.Awake 실행 안 됨 → ZDO 미등록)
    bool wasActive = prefab.activeSelf;
    prefab.SetActive(false);
    var obj = UnityEngine.Object.Instantiate(prefab, worldPos, Quaternion.identity);
    prefab.SetActive(wasActive); // ② 프리팹 즉시 원상복구

    // ZNet/게임 로직 컴포넌트 전부 제거 (Awake 미실행 상태이므로 안전)
    // ※ enabled=false는 Awake를 막지 못함 → 반드시 DestroyImmediate 사용
    var znv = obj.GetComponent<ZNetView>();
    if (znv != null) UnityEngine.Object.DestroyImmediate(znv);
    var zsync = obj.GetComponent<ZSyncTransform>();
    if (zsync != null) UnityEngine.Object.DestroyImmediate(zsync);
    var proj = obj.GetComponent<Projectile>();
    if (proj != null) UnityEngine.Object.DestroyImmediate(proj);

    // 콜라이더 비활성화 (AutoPickup NullReferenceException 방지)
    foreach (var col in obj.GetComponentsInChildren<Collider>())
        col.enabled = false;

    var rb = obj.GetComponent<Rigidbody>();
    if (rb != null) rb.isKinematic = true;

    // ③ 활성화 → 비주얼(MeshRenderer, ParticleSystem)만 남은 순수 로컬 VFX
    obj.SetActive(true);
}
```

#### Destroy 시 주의사항
오브젝트 제거 시 반드시 `SetActive(false)` 후 `Destroy` 호출.  
`Destroy`는 다음 프레임에 실제 제거되므로, 그 사이 `AutoPickup` 등이 컴포넌트를 접근하면 NullRef 발생.

```csharp
// ✅ 올바른 제거 패턴
if (obj != null)
{
    obj.SetActive(false);            // 즉시 Physics/AutoPickup 대상에서 제외
    UnityEngine.Object.Destroy(obj); // 다음 프레임에 메모리 해제
}

// ❌ 잘못된 제거 패턴 (AutoPickup NullRef 유발)
if (obj != null) UnityEngine.Object.Destroy(obj);
```

#### 적용 대상
- **발사체 프리팹** (`*_projectile`)을 호버/대기 상태 비주얼로 사용할 때
- ZNetView가 있는 프리팹을 **로컬 전용 시각 효과**로만 쓸 때
- 동일 프리팹을 **짧은 간격으로 다수 소환**해야 할 때 (ZDO 부하 없이 안전)

#### 해결 성공 사례
**지팡이 팬캐스트 스킬** (SkillEffect.StaffDualCast.cs):
- **문제**: `staff_fireball_projectile` 7개를 소환 후 사망 → 리스폰 무한 로딩
- **원인**: `DestroyImmediate(znv)`로 ZNetView만 제거, ZDO는 ZDOMan에 고아로 잔존
- **해결**: 비활성 프리팹 트릭으로 ZNetView.Awake() 차단 → ZDO 미등록
- **결과**: 무한 로딩 완전 해결 ✅

---

### 10. **🌀 발헤임 기본 VFX를 커스텀화하여 플레이어 따라다니게 만들기 (RegisterValheimVFXAsCustom)**

#### 문제 상황
발헤임 기본 VFX(`vfx_Potion_health_medium` 등)를 플레이어 자식으로 부착하면:
1. `ZNetView.Awake()`가 ZNetScene에 등록 → `Destroy()` 후 null 참조 잔존 → **무한 로딩**
2. `SetActive(true)` 시 `ZSyncTransform.Awake()`가 `ZNetView` 없음을 접근 → **NullReferenceException**

#### 해결 방법: `SimpleVFX.RegisterValheimVFXAsCustom()`

ZNetScene에서 클론 생성 → 네트워크 컴포넌트 전부 제거 → `_customVFXNames`에 등록  
→ 이후 `SimpleVFX.PlayOnPlayer()`의 커스텀 경로(안전한 `Destroy`)로 재생됨

**등록 방법** (`SimpleVFX_ZNetScene_Awake_Patch.Postfix` 안에서만 호출):
```csharp
SimpleVFX.RegisterValheimVFXAsCustom("vfx_Potion_health_medium");
```

**내부 동작 원리** (`SimpleVFX.RegisterValheimVFXAsCustom`):
```csharp
// ① 원본을 비활성화한 채 Instantiate → clone의 Awake() 전혀 실행 안 됨 → ZNetScene 등록 안 됨
bool wasActive = original.activeSelf;
original.SetActive(false);
var clone = UnityEngine.Object.Instantiate(original);
original.SetActive(wasActive); // 즉시 원상복구

// ② 네트워크 컴포넌트 일괄 제거 (자식 포함)
// ZNetView만 제거하면 ZSyncTransform.Awake()가 SetActive(true) 시 NullRef 유발 → 함께 제거 필수
foreach (var nv in clone.GetComponentsInChildren<ZNetView>(true))
    UnityEngine.Object.DestroyImmediate(nv);
foreach (var st in clone.GetComponentsInChildren<ZSyncTransform>(true))
    UnityEngine.Object.DestroyImmediate(st);
foreach (var sa in clone.GetComponentsInChildren<ZSyncAnimation>(true))
    UnityEngine.Object.DestroyImmediate(sa);

// ③ 비활성 템플릿으로 보관 → _customVFXNames 등록
clone.SetActive(false);
_customVFXNames.Add(vfxName);
_cachedPrefabs[vfxName] = clone;
```

**재생 방법**:
```csharp
// ✅ 플레이어를 N초간 따라다니는 발헤임 기본 VFX
SimpleVFX.PlayOnPlayer(player, "vfx_Potion_health_medium", 5f, new Vector3(0f, 0.5f, 0f));
```

> ⚠️ `PlayOnPlayer` 내부에서 비활성 템플릿을 `Instantiate` 후 `SetActive(true)` 호출  
> → 비활성으로 인스턴스화 → 명시적 활성화 → 파티클 시작 → `Destroy(duration)` 안전

#### 제거해야 할 컴포넌트 (ZNetView 의존 체인)
| 컴포넌트 | 제거 이유 |
|----------|---------|
| `ZNetView` | ZNetScene 등록 주체 — 제거 안 하면 Destroy 시 null 잔존 |
| `ZSyncTransform` | ZNetView에 의존, SetActive(true) 시 Awake에서 NullRef |
| `ZSyncAnimation` | ZNetView에 의존, 애니메이터 동기화 컴포넌트 |

#### 적용 대상
- 발헤임 기본 VFX를 **플레이어/캐릭터에 부착해 따라다니게** 해야 할 때
- 파티클 효과가 **N초 후 안전하게 Destroy** 되어야 할 때
- §9의 비활성 트릭과 유사하지만 **재사용 가능한 템플릿 캐싱**이 필요할 때

#### 해결 성공 사례
**성기사 Y키 힐 스킬** (`JobSkills.PlayPaladinActivationEffect`):
- **문제**: `vfx_Potion_health_medium`을 커스텀 없이 `PlayOnPlayer` 호출 → 무한 로딩
- **해결**: `RegisterValheimVFXAsCustom` 등록 후 커스텀 경로 재생
- **결과**: 5초간 시전자 따라다니다 안전 소멸 ✅

---

### 7. **액티브 스킬 VFX/사운드 체크리스트**

액티브 스킬 개발 시 반드시 확인:
- [ ] `PlayVFXMultiplayer()` 사용 (PlayVFXHybrid 금지)
- [ ] 중복 사운드 호출 없음
- [ ] **발헤임 기본 VFX 연속 호출 없음** (각 타격마다 다른 VFX 사용)
- [ ] **발헤임 기본 VFX 로컬 전용은 순수 Instantiate 사용** (VFXManager 금지)
- [ ] **ZNet 발사체 프리팹을 VFX로 사용 시 비활성 프리팹 트릭 적용** (§9 참조)
- [ ] **발헤임 기본 VFX를 플레이어 부착이 필요하면 `RegisterValheimVFXAsCustom` 사용** (§10 참조)
- [ ] **오브젝트 제거 시 `SetActive(false)` 후 `Destroy` 호출** (AutoPickup NullRef 방지)
- [ ] 커스텀 VFX 이름이 targetVFXList에 등록되어 있음
- [ ] 대소문자 매핑이 vfxMapping에 정의되어 있음
- [ ] 로그로 VFX/사운드 재생 확인

---

### 8. **패시브 스킬 VFX/사운드 규칙**

- ❌ **패시브 스킬**: VFX/사운드 사용 금지 (텍스트 표시만)
- ✅ **액티브 스킬**: VFX/사운드 풍부하게 사용

---

## 🛠️ 무한 로딩 발생 시 디버깅

### 1. 로그 확인
```csharp
Plugin.Log.LogInfo($"[VFX] VFX 재생: {vfxName}");
Plugin.Log.LogInfo($"[VFX] 사운드 재생: {soundName}");
```

### 2. PlayVFXHybrid 호출 검색
```bash
# 프로젝트 전체에서 PlayVFXHybrid 검색
grep -r "PlayVFXHybrid" CaptainSkillTree/
```

### 3. 중복 호출 검색
동일한 VFX/사운드가 짧은 시간 내 여러 번 호출되는지 확인

### 4. VFX 등록 상태 확인
```csharp
VFXPrefabRegistry.DiagnoseRegistrationStatus();
```

---

## 📊 성공 사례

### ✅ 지팡이 힐 스킬 (수정 후)
**파일**: `SkillEffect.ActiveSkills.cs` (라인 991-997)
```csharp
// 1. 스킬 발동 시 이펙트 및 사운드 (ZRoutedRpc 멀티플레이어 동기화)
try
{
    // 시전자 위치에 shaman_heal_aoe 이펙트 + 사운드
    VFXManager.PlayVFXMultiplayer("shaman_heal_aoe", "sfx_dverger_heal_finish", casterPos, caster.transform.rotation, 3f);
    Plugin.Log.LogInfo("[지팡이 힐] shaman_heal_aoe VFX/사운드 재생");
}
```

### ✅ 버서커 스킬
**파일**: `Berserker_Skill.cs`
```csharp
// 분노 버프 VFX (커스텀 VFX)
VFXManager.PlayVFXMultiplayer("debuff", "sfx_morgen_alert", playerPos, Quaternion.identity, 3f);

// 공격 시 플래시 VFX
VFXManager.PlayVFXMultiplayer("flash_round_ellow", "", attackPosition, Quaternion.identity, 2f);

// 피격 시 디버프 VFX
VFXManager.PlayVFXMultiplayer("statusailment_01", "", hitPosition, Quaternion.identity, 1.5f);
```

---

## 🔒 절대 금지 사항

1. ❌ **PlayVFXHybrid 사용** → PlayVFXMultiplayer로 대체
2. ❌ **중복 사운드 호출** → 하나의 PlayVFXMultiplayer에 통합
3. ❌ **대소문자 미확인** → targetVFXList와 vfxMapping 확인 필수
4. ❌ **발헤임 기본 VFX를 VFXManager로 호출** → 순수 Instantiate 사용
5. ❌ **커스텀 VFX를 직접 Instantiate** → VFXManager.PlayVFXMultiplayer 사용
6. ❌ **패시브 스킬 VFX** → 텍스트 표시만 허용
7. ❌ **ZNet 프리팹을 바로 Instantiate 후 DestroyImmediate(znv)** → 비활성 프리팹 트릭 사용 (§9)
8. ❌ **오브젝트 제거 시 Destroy만 호출** → `SetActive(false)` 후 `Destroy` 호출
9. ❌ **발헤임 기본 VFX를 플레이어 자식으로 직접 부착** → `RegisterValheimVFXAsCustom`으로 커스텀화 후 `SimpleVFX.PlayOnPlayer` 사용 (§10)

---

## 📝 개발 워크플로우

### 새로운 VFX 추가 시
1. **VFX 파일 확인**: `asset/VFX/` 폴더에 파일 존재 확인
2. **targetVFXList 등록**: `VFXPrefabRegistry.cs` (라인 29-83)
3. **vfxMapping 추가**: 소문자 → 대문자 매핑 (라인 239-308)
4. **PlayVFXMultiplayer 사용**: 액티브 스킬에서 호출
5. **빌드 후 테스트**: 무한 로딩 발생 여부 확인

### 무한 로딩 발생 시
1. **PlayVFXHybrid 검색**: 프로젝트 전체 검색
2. **중복 호출 확인**: 동일 VFX/사운드 중복 호출
3. **VFX 등록 확인**: DiagnoseRegistrationStatus() 로그
4. **대소문자 확인**: vfxMapping과 실제 GameObject명 일치 여부

---

## 🎯 요약

| 항목 | 필수 사항 |
|------|----------|
| **메서드** | PlayVFXMultiplayer만 사용 (커스텀 VFX) |
| **발헤임 기본 VFX** | 순수 Instantiate 사용 (VFXManager 금지) |
| **ZNet 발사체를 VFX로** | 비활성 프리팹 트릭 사용 (§9 참조) |
| **발헤임 기본 VFX 플레이어 부착** | `RegisterValheimVFXAsCustom` 등록 → `SimpleVFX.PlayOnPlayer` (§10) |
| **오브젝트 제거** | `SetActive(false)` 후 `Destroy` 호출 |
| **중복 방지** | 동일 VFX/사운드 한 번만 호출 |
| **대소문자** | 양쪽 이름 등록 + 폴백 검색 |
| **검색 순서** | VFXPrefabRegistry → ZNetScene |
| **패시브** | VFX/사운드 사용 금지 |
| **액티브** | VFX/사운드 풍부하게 사용 |

**이 규칙을 준수하면 VFX/사운드 무한 로딩을 완벽히 방지할 수 있습니다.**

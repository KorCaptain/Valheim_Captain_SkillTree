# VFX 투명도(Dim) 시스템

## 개요

"My VFX 투명도" Config 값(0~100%)으로 모든 커스텀/발헤임 기본 VFX의 밝기를 조절하는 시스템.  
`Skill_Tree_Base` 섹션에 위치하며, 클라이언트 개별 적용 (서버 동기화 없음).

---

## Config

```csharp
// SkillTreeConfig.cs
public static float VFXOpacityValue => _vfxOpacity.Value / 100f; // 0.0 ~ 1.0
// NOT BindServerSync — 클라이언트 개별 설정
```

---

## VFX 종류별 dim 처리 방식

### 1. 커스텀 VFX (`asset/VFX/` 에셋)

**자동 dim** — `SimpleVFX.Initialize()`에서 `_customVFXNames` 전체에 루프 적용.

```csharp
// SimpleVFX.cs — Initialize()
float vfxDim = SkillTreeConfig.VFXOpacityValue;
var dimExcluded = new HashSet<string> { "statusailment_01_aura", "taunt" };
foreach (var vfxName in _customVFXNames)
{
    if (!dimExcluded.Contains(vfxName))
        RegisterVFXDim(vfxName, vfxDim);
}
```

**dim 제외 VFX** (의도적):
| VFX | 이유 |
|-----|------|
| `statusailment_01_aura` | 버프 활성 상태 표시용 — 희미해지면 인지 불가 |
| `taunt` | 어그로 도발 인디케이터 — 항상 선명해야 함 |

**재생 메서드별 dim 적용 경로**:
| 메서드 | dim 처리 |
|--------|---------|
| `SimpleVFX.Play()` | `_vfxDimMapping` 자동 조회 |
| `SimpleVFX.PlayOnPlayer(string, float)` | `_vfxDimMapping` 자동 조회 |
| `SimpleVFX.PlayOnPlayer(Player, float)` | `_vfxDimMapping` 조회 후 수동 `ApplyVFXDim` |
| `SimpleVFX.PlayOnTarget()` | `_vfxDimMapping` 조회 후 수동 `ApplyVFXDim` |
| `SimpleVFX.PlayWithRotation()` | `_vfxDimMapping` 조회 후 수동 `ApplyVFXDim` |
| `SimpleVFX.PlayWithSound()` | `_vfxDimMapping` 자동 조회 |

---

### 2. 발헤임 기본 VFX (ZNet 등록 프리팹)

**수동 dim** — `VFXManager.PlayVFXMultiplayer` 대신 직접 `Instantiate` 패턴 사용.

```csharp
// 패턴 (복사해서 사용할 것)
{
    var _prefab = ZNetScene.instance?.GetPrefab("fx_이름");
    if (_prefab != null)
    {
        var _go = UnityEngine.Object.Instantiate(_prefab, position, Quaternion.identity);
        SimpleVFX.ApplyVFXDim(_go, SkillTreeConfig.VFXOpacityValue);
        // ⚠️ Destroy 생략 — 발헤임 기본 VFX는 자동 정리 (Destroy 호출 시 무한 로딩 위험)
    }
}
```

**⚠️ 핵심 제약사항**:
- `Destroy()` **절대 호출 금지** → ZNetView destroy RPC 루프 → 무한 로딩 발생
- 직접 Instantiate는 **로컬 클라이언트에만 표시** (다른 플레이어에게 안 보임)
- `VFXManager.PlayVFXHybrid()` 사용 금지 (무한 로딩 유발)

**적용된 발헤임 기본 VFX 목록**:
| VFX 이름 | 스킬 | 파일 |
|---------|------|------|
| `fx_fallenfalkyrie_spin` | 폴암 휠윈드 (×2) | `SkillEffect.PolearmWhirlwind.cs` |
| `fx_blobLava_explosion` | 폭발화살 적중 | `SkillEffect.ExplosiveArrow.cs` |
| `fx_siegebomb_explosion` | 폭발화살 적중 | `SkillEffect.ExplosiveArrow.cs` |
| `fx_siegebomb_explosion` | 분노의 망치 5타 | `MaceSkills.FuryHammer.cs` |
| `fx_shieldgenerator_domehit` | 방패돌진 (×2) | `Mace_Active.cs` |
| `fx_DvergerMage_Ice_hit` | 화살비 착지 | `SkillEffect.ArrowRain.cs` |

---

## ApplyVFXDim 내부 동작

```csharp
// SimpleVFX.cs — ApplyVFXDim()
public static void ApplyVFXDim(GameObject go, float dimFactor)
```

`VFXDimmerBehaviour` 컴포넌트를 부착 → LateUpdate에서 `Apply()` 호출:

1. PS 재생 중단 (`Stop`)
2. **ParticleSystem.main.startColor** alpha × dimFactor
3. **Renderer.material color** alpha × dimFactor
4. **startSize** × dimFactor (크기도 함께 축소)
5. PS 재시작 (`Play`)

**⚠️ emission rate 수정 금지**: Curve 모드 ParticleSystem에서 `em.rateOverTime.constant`가 0을 반환해 전체 파티클이 사라지는 버그 발생 → 제거됨.

---

## 새 VFX에 dim 적용하는 법

### 커스텀 VFX (asset/VFX/ 추가 시)
→ `_customVFXNames`에 등록만 하면 자동 dim 적용. 별도 작업 불필요.

### 발헤임 기본 VFX (PlayVFXMultiplayer 대체 시)
위의 "수동 dim 패턴" 복사 사용. `Destroy` 절대 추가하지 말 것.

### Instantiate 직접 사용하는 커스텀 로직
```csharp
var go = UnityEngine.Object.Instantiate(prefab, pos, rot);
SimpleVFX.ApplyVFXDim(go, SkillTreeConfig.VFXOpacityValue);
```

---

## 멀티플레이 동작

| 방식 | 본인 | 다른 플레이어 |
|------|------|--------------|
| `VFXManager.PlayVFXMultiplayer` | ✅ | ✅ (ZNet RPC) |
| 직접 `Instantiate` + `ApplyVFXDim` | ✅ | ❌ (로컬 전용) |

VFX는 시각 효과이므로 로컬 전용도 UX 충족. 게임플레이 영향 없음.

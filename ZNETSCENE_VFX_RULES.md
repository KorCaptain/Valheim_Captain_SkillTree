# ZNetScene VFX/SFX 통합 관리 규칙

## ⚠️ 중요: 이 문서는 VFX 프리팹 목록 참고용입니다

**VFX 재생 방법은 [VFX_SOUND_INFINITE_LOADING_FIX.md](VFX_SOUND_INFINITE_LOADING_FIX.md)를 따르세요.**

이 문서의 `직접 Instantiate` 패턴은 구버전이며, **무한 로딩을 유발할 수 있습니다**.
반드시 최신 `VFXManager.PlayVFXMultiplayer()` 메서드를 사용하세요.

---

## 발헤임 기본 이팩트로 프리팹 기본 등록 되어 있음
smokebomb_explosion - 발헤임 기본 스모크 효과 - 로그 이팩트
vfx_GodExplosion - 발헤임 기본 녹색 폭파 완전 큼
fx_Fader_CorpseExplosion - 발헤임 기본 푸른 큰 안개 
fx_siegebomb_explosion - 발헤임 기본 - 히트 폭파
charred_fireball_projectile - 화려한 회오리 중심 독, 힐 이팩트
fx_greenroots_projectile_hit - 힐, 독 히트 이팩트
fx_lightningstaffprojectile_hit - 번개 히트 이팩트
fx_shaman_fireball_expl - 둥근 핑크 히트 이팩트
fx_shaman_protect - 푸른 회오리 이팩트
fx_shield_start - 쉴드 및 버프 시작 이팩트
fx_shieldgenerator_domehit - 그린 방어 이팩트
staff_greenroots_projectile - 그린 볼 뿌리 소환
fx_Fader_Spin - 광역 원 푸른 


## 🎯 핵심 원칙

### 1. ZNetScene 필수 등록 규칙
- **모든 VFX/SFX 효과는 ZNetScene에 등록 후 사용**
- **Plugin.cs의 UnifiedVfxPrefabRegisterPatch에서 자동 등록**
- **사용 전 반드시 ZNetScene.instance.GetPrefab()로 존재 확인**

### 2. DLL 리소스 구조
```



CaptainSkillTree.asset.Resources.{리소스명}
```

## 📦 등록된 VFX 프리팹 목록

### 버프/디버프 효과
- `buff_02a` - 아처 멀티샷 활성화 버프
- `buff_03a` - 캐릭터 힐 효과 이팩트
- `buff_03a_aura` - 잔잔한 캐릭터 머리위로 녹생 방울 힐링 도트효과 
- `debuff` - 디버프 화살표 위에서 아래로 붉은색 - 버서커 사용 - 반복됨 종료 코드 필요
- `debuff_03` - ㅣ ㅣ ㅣ ㅣ 하얀색 버프? 비슷 - 종료코드 필요
- `debuff_03_aura` - 오라 타입 디버프
- `statusailment_01` - 상태 이상 효과 1
- `statusailment_01_aura` - 스킬 시전 캐릭터 머리 표시(종료 필요)
- buff_01 - 공격력 증가 버프
- fx_Fader_Spin - 광역 푸른 둥근 보기좋은 굿

### 타격/전투 효과
- `hit_01` - 기본 타격 효과 1
- `hit_02` - 기본 타격 효과 2
- `hit_03` - 기본 타격 효과 3
- `hit_04` - 기본 타격 효과 4


### 특수 효과
- `healing` - 힐링 효과, 녹색 안개와 십차 모양 힐 - 반복됨 종료 코드 필요(종료 코드는 버서커 이팩티 종료 참고)
- `taunt` - 도발 효과 - 몬스터 머리위에 표시 필요

### 오라/지속 효과
 - StatusAilment_01_Aura    스킬 시전동안 (종료 필요)
                                     캐릭터 머리위에 별 버프 모양 - "단 한 발" 스킬 이팩트
- `plexus` - 플렉서스 효과 - 번개 파란색 라운드  - 반복됨 종료 코드 필요(종료 코드는 버서커 이팩티 종료 참고)

### MMO 호환 효과
- `LevelUpVFX2` - MMO 레벨업 효과

## 기타 미사용된 효과 - 사용가능하게 모두 등록은 되어야함 추후 사용예정
area_circles_blue - 원기둥형 동글동글 푸른 빛
area_fire_red - 원기둥형 붉은 빛 줄기 위로 올라감
area_heal_green - 원기둥형 빛 십자모양 위로 올라감 힐?
area_magic_multicolor - 원기둥형 빛 원 모양 위로 올라감
area_star_ellow - 원기둥형 빛 노란 줄 올라감

shine_blue - 빛 흰,블루 광역 - 종료 코드 필요
shine_pink - 
shine_ellow

sparkle_ellow - 흐린 뿌연 노란 빛 - 로그 사용

confetti_blast_multicolor - 히트 효과 - 여러 모양 히트 주로 노란색?
confetti_directional_multicolor - 히트효과? 중앙에서 왼쪽 오른쪽 각 각 히트여러모양 퍼져나감
flash_blue_purple - 히트 블루 퍼플
flash_ellow - 히트 노랑 
flash_ellow_pink - 히트 노랑 핑크
flash_magic_blue_pink - 히트 매직 붐 블루 핑크
flash_magic_ellow_blue
flash_round_ellow - 히트 노랑 크게 히트 - 쓸만함
flash_star_ellow_purple - 히트 폭발 스타 ... - 중하
water_blast_blue - 푸른 물폭파
water_blast_green - 그린 물폭파

guard_01 - 막기 효과 파란색 원 물파장
dust_permanently_blue - 둥근 여러 모양 푸른색으로 뭉개 뭉개 광역


## 🔧 사용 방법

### 1. 표준 VFX 재생 패턴
```csharp
public static void PlayVfxEffect(string effectName, Vector3 position, Quaternion rotation = default)
{
    try
    {
        var znet = ZNetScene.instance;
        if (znet == null)
        {
            Plugin.Log.LogWarning($"[VFX] ZNetScene이 null입니다");
            return;
        }
        
        var effectPrefab = znet.GetPrefab(effectName);
        if (effectPrefab != null)
        {
            UnityEngine.Object.Instantiate(effectPrefab, position, rotation);
            Plugin.Log.LogDebug($"[VFX] {effectName} 효과 재생 완료");
        }
        else
        {
            Plugin.Log.LogWarning($"[VFX] ZNetScene에서 {effectName} 프리팹을 찾을 수 없음");
        }
    }
    catch (Exception ex)
    {
        Plugin.Log.LogError($"[VFX] {effectName} 재생 오류: {ex.Message}");
    }
}
```

### 2. 캐시된 VFX 재생 패턴 (성능 최적화)
```csharp
private static readonly Dictionary<string, GameObject> vfxCache = new Dictionary<string, GameObject>();

public static void PlayCachedVfxEffect(string effectName, Vector3 position, Quaternion rotation = default)
{
    try
    {
        // 캐시에서 먼저 확인
        if (!vfxCache.ContainsKey(effectName))
        {
            var znet = ZNetScene.instance;
            if (znet != null)
            {
                var prefab = znet.GetPrefab(effectName);
                if (prefab != null)
                {
                    vfxCache[effectName] = prefab;
                    Plugin.Log.LogDebug($"[VFX 캐시] {effectName} 프리팹 캐시됨");
                }
                else
                {
                    Plugin.Log.LogWarning($"[VFX 캐시] {effectName} 프리팹을 ZNetScene에서 찾을 수 없음");
                    return;
                }
            }
            else
            {
                Plugin.Log.LogWarning($"[VFX 캐시] ZNetScene이 null입니다");
                return;
            }
        }
        
        // 캐시된 프리팹으로 효과 재생
        var cachedPrefab = vfxCache[effectName];
        if (cachedPrefab != null)
        {
            UnityEngine.Object.Instantiate(cachedPrefab, position, rotation);
            Plugin.Log.LogDebug($"[VFX 캐시] {effectName} 캐시된 효과 재생 완료");
        }
    }
    catch (Exception ex)
    {
        Plugin.Log.LogError($"[VFX 캐시] {effectName} 재생 오류: {ex.Message}");
    }
}
```

### 3. 플레이어 위치 기반 효과 재생
```csharp
public static void PlayPlayerVfxEffect(string effectName, Player player)
{
    if (player == null) return;
    PlayVfxEffect(effectName, player.transform.position, player.transform.rotation);
}
```

### 4. 지속 효과 관리 (버프/디버프)
```csharp
// 플레이어별 활성 효과 추적
private static readonly Dictionary<Player, Dictionary<string, GameObject>> activePlayerEffects = 
    new Dictionary<Player, Dictionary<string, GameObject>>();

public static void ApplyPersistentEffect(string effectName, Player player, float duration = 0f)
{
    try
    {
        // 기존 효과 제거
        RemovePersistentEffect(effectName, player);
        
        // 새 효과 생성
        var effectObj = CreateVfxEffect(effectName, player.transform.position);
        if (effectObj != null)
        {
            // 플레이어를 따라다니도록 설정
            effectObj.transform.SetParent(player.transform);
            
            // 추적 목록에 추가
            if (!activePlayerEffects.ContainsKey(player))
                activePlayerEffects[player] = new Dictionary<string, GameObject>();
            
            activePlayerEffects[player][effectName] = effectObj;
            
            // 지속 시간이 있으면 자동 제거 예약
            if (duration > 0f)
            {
                player.StartCoroutine(RemoveEffectAfterDelay(effectName, player, duration));
            }
        }
    }
    catch (Exception ex)
    {
        Plugin.Log.LogError($"[지속 효과] {effectName} 적용 오류: {ex.Message}");
    }
}

public static void RemovePersistentEffect(string effectName, Player player)
{
    try
    {
        if (activePlayerEffects.ContainsKey(player) && 
            activePlayerEffects[player].ContainsKey(effectName))
        {
            var effectObj = activePlayerEffects[player][effectName];
            if (effectObj != null)
            {
                UnityEngine.Object.Destroy(effectObj);
            }
            activePlayerEffects[player].Remove(effectName);
        }
    }
    catch (Exception ex)
    {
        Plugin.Log.LogError($"[지속 효과] {effectName} 제거 오류: {ex.Message}");
    }
}
```

## 🚨 필수 준수 사항

### 1. 효과 사용 전 검증
```csharp
// ✅ 올바른 방식 - 항상 ZNetScene에서 확인
var effectPrefab = ZNetScene.instance?.GetPrefab("buff_02a");
if (effectPrefab != null)
{
    UnityEngine.Object.Instantiate(effectPrefab, position, rotation);
}
else
{
    Plugin.Log.LogWarning("buff_02a 프리팹을 ZNetScene에서 찾을 수 없음");
}

// ❌ 잘못된 방식 - 검증 없이 바로 사용
UnityEngine.Object.Instantiate(Resources.Load("buff_02a"), position, rotation);
```

### 2. 프리팹 등록 추가 방법
새로운 VFX 리소스 추가 시:

1. **Captain_SkillTree.csproj에 EmbeddedResource 추가**
```xml
<EmbeddedResource Include="asset/Resources/새효과명" />
```

2. **Plugin.cs의 resourceBundles에 등록**
```csharp
["새효과명"] = new[] { "새효과명" },
```

### 3. 오류 처리 패턴
```csharp
// 모든 VFX 관련 코드는 try-catch로 보호
try
{
    // VFX 코드
}
catch (Exception ex)
{
    Plugin.Log.LogError($"[VFX] 오류: {ex.Message}");
}
```

## 📊 성능 최적화 가이드

### 1. 캐싱 시스템 활용
- 자주 사용되는 효과는 캐시해서 성능 향상
- Dictionary<string, GameObject>로 프리팹 캐시 관리

### 2. 메모리 관리
- 지속 효과는 반드시 추적하여 적절한 시점에 제거
- activePlayerEffects로 플레이어별 효과 관리

### 3. 로그 레벨 관리
- 정상 작동: LogDebug 사용
- 경고 상황: LogWarning 사용  
- 오류 상황: LogError 사용

## ✅ 체크리스트

효과 구현 시 반드시 확인:

- [ ] Captain_SkillTree.csproj에 EmbeddedResource로 등록됨
- [ ] Plugin.cs의 resourceBundles에 등록됨  
- [ ] ZNetScene.instance.GetPrefab()로 존재 확인
- [ ] try-catch 블록으로 오류 처리 구현
- [ ] 적절한 로그 레벨 사용
- [ ] 지속 효과는 추적 및 정리 시스템 구현

이 규칙을 준수하여 안정적이고 효율적인 VFX 시스템을 구축하세요.

---

## 📚 **관련 문서**
- **🏠 메인 규칙**: [../CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 구조
- **🔐 보호 규칙**: [CORE_PROTECTION_README.md](CORE_PROTECTION_README.md) - AssetBundle 로딩 시스템
- **🎹 액티브 스킬**: [ACTIVE_SKILL_SYSTEM.md](ACTIVE_SKILL_SYSTEM.md) - 액티브 스킬 VFX 적용
- **🚨 빌드 오류**: [BUILD_ERRORS_GUIDE.md](BUILD_ERRORS_GUIDE.md) - VFX 관련 오류 해결
- **⚡ 빠른 참조**: [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - VFX 사용 패턴 요약
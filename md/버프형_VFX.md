# 버프형 VFX 시스템 가이드

## 개요

버프형 VFX는 **캐릭터를 따라다니며 지속되는 시각 효과**로, 버프 활성화 상태를 시각적으로 표시합니다.

---

## 버프형 VFX 구조

### 1️⃣ 2단계 VFX 시스템

버프 스킬은 **2가지 VFX**를 조합하여 사용합니다:

| VFX 타입 | 프리팹 | 위치 | 지속시간 | 용도 |
|---------|--------|------|---------|------|
| **활성화 효과** | `buff_01`, `buff_02a` | 발밑 (0.5f) | 2초 | 버프 활성화 순간 효과 |
| **상태 표시** | `statusailment_01_aura` | 머리 위 (2.0f) | 버프 종료까지 | 버프 지속 상태 표시 |

---

## 구현 패턴

### 필수 변수

```csharp
public static partial class SkillEffect
{
    // 상태 VFX 인스턴스 관리
    private static Dictionary<Player, GameObject> skillNameStatusEffects = new Dictionary<Player, GameObject>();

    // 프리팹 캐시 (한 번만 로드)
    private static GameObject cachedSkillNameStatusPrefab = null;
}
```

---

### 버프 활성화 시

```csharp
/// <summary>
/// 버프 활성화 시 VFX/SFX 효과
/// </summary>
private static void PlaySkillNameActivationEffects(Player player)
{
    try
    {
        // 1. 활성화 효과 (buff_01) - 발밑, 2초간
        var buff01Prefab = VFXManager.GetVFXPrefab("buff_01");
        if (buff01Prefab != null)
        {
            var buff01Effect = UnityEngine.Object.Instantiate(
                buff01Prefab,
                player.transform.position + Vector3.up * 0.5f,
                player.transform.rotation
            );
            buff01Effect.transform.SetParent(player.transform, false);
            buff01Effect.transform.localPosition = Vector3.up * 0.5f;
            buff01Effect.transform.localScale = Vector3.one * 0.8f;

            // 2초 후 자동 제거
            UnityEngine.Object.Destroy(buff01Effect, 2f);
        }

        // 2. 상태 표시 효과 (statusailment_01_aura) - 머리 위, 버프 종료까지
        PlaySkillNameStatusEffect(player);

        // 3. 활성화 사운드
        VFXManager.PlaySound("sfx_reload_dverger_done", player.transform.position, 5f);
    }
    catch (System.Exception ex)
    {
        Plugin.Log.LogError($"[스킬명] 활성화 효과 재생 실패: {ex.Message}");
    }
}
```

---

### 상태 표시 VFX 생성

```csharp
/// <summary>
/// 버프 상태 효과 VFX (statusailment_01_aura - 머리 위, 버프 종료까지 지속)
/// </summary>
private static void PlaySkillNameStatusEffect(Player player)
{
    try
    {
        // 캐시된 프리팹이 없으면 한 번만 로드
        if (cachedSkillNameStatusPrefab == null)
        {
            cachedSkillNameStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
            if (cachedSkillNameStatusPrefab == null)
            {
                Plugin.Log.LogWarning("[스킬명] statusailment_01_aura 프리팹을 찾을 수 없음");
                return;
            }
        }

        // 기존 상태 효과가 있으면 제거 (중복 방지)
        if (skillNameStatusEffects.ContainsKey(player) && skillNameStatusEffects[player] != null)
        {
            UnityEngine.Object.Destroy(skillNameStatusEffects[player]);
            skillNameStatusEffects.Remove(player);
        }

        // statusailment_01_aura 효과 생성 (머리 위 2미터)
        var headPosition = player.transform.position + Vector3.up * 2.0f;
        var statusInstance = UnityEngine.Object.Instantiate(
            cachedSkillNameStatusPrefab,
            headPosition,
            Quaternion.identity
        );

        // 캐릭터를 따라다니도록 부모 설정
        statusInstance.transform.SetParent(player.transform, false);
        statusInstance.transform.localPosition = Vector3.up * 2.0f; // 머리 위 고정
        statusInstance.transform.localScale = Vector3.one * 0.6f;

        // 상태 효과 인스턴스 저장 (버프 종료 시 제거하기 위해)
        skillNameStatusEffects[player] = statusInstance;
    }
    catch (System.Exception ex)
    {
        Plugin.Log.LogError($"[스킬명] 상태 효과 재생 실패: {ex.Message}");
    }
}
```

---

### 버프 소모 시 (종료)

```csharp
/// <summary>
/// 버프 소모 처리 (발사 후 호출)
/// </summary>
public static void ConsumeSkillName(Player player)
{
    if (skillNameReady.ContainsKey(player) && skillNameReady[player])
    {
        skillNameReady[player] = false;

        // 상태 효과 제거 (statusailment_01_aura)
        if (skillNameStatusEffects.ContainsKey(player) && skillNameStatusEffects[player] != null)
        {
            UnityEngine.Object.Destroy(skillNameStatusEffects[player]);
            skillNameStatusEffects.Remove(player);
        }

        ShowSkillEffectText(player, "💥 " + L.Get("skill_consumed"), Color.red, SkillEffectTextType.Critical);
    }
}
```

---

### 플레이어 사망 시 정리

```csharp
/// <summary>
/// 스킬 정리 메서드 (플레이어 사망 시 호출)
/// </summary>
public static void CleanupSkillNameOnDeath(Player player)
{
    try
    {
        skillNameCooldown.Remove(player);
        skillNameReady.Remove(player);

        // 상태 효과 GameObject 제거 (statusailment_01_aura)
        if (skillNameStatusEffects.ContainsKey(player))
        {
            var statusEffect = skillNameStatusEffects[player];
            if (statusEffect != null)
            {
                try
                {
                    UnityEngine.Object.Destroy(statusEffect);
                }
                catch { }
            }
            skillNameStatusEffects.Remove(player);
        }
    }
    catch (Exception ex)
    {
        Plugin.Log.LogWarning($"[스킬명] 정리 실패: {ex.Message}");
    }
}
```

---

## 실제 구현 예시

### 1. 폭발 화살 (Explosive Arrow)

```csharp
// 변수 선언
private static Dictionary<Player, GameObject> explosiveArrowStatusEffects = new Dictionary<Player, GameObject>();
private static GameObject cachedExplosiveArrowStatusPrefab = null;

// R키 활성화 시
explosiveArrowReady[player] = true;
PlayExplosiveArrowActivationEffects(player); // buff_01 + statusailment_01_aura

// 화살 발사 시
ConsumeExplosiveArrow(player); // statusailment_01_aura 제거

// 사망 시
CleanupExplosiveArrowOnDeath(player); // 모든 VFX 정리
```

### 2. 아처 멀티샷 (Archer MultiShot)

```csharp
// 변수 선언
private static Dictionary<Player, GameObject> archerMultiShotStatusEffects = new Dictionary<Player, GameObject>();
private static GameObject cachedArcherStatusEffectPrefab = null;

// Y키 활성화 시
archerMultiShotCharges[player] = 2; // 2회 충전
PlayArcherMultiShotBuffActivationEffects(player); // buff_02a + statusailment_01_aura

// 2회 모두 사용 시
archerMultiShotCharges[player] = 0;
// statusailment_01_aura 제거

// 사망 시
CleanupArcherMultiShotOnDeath(player); // 모든 VFX 정리
```

---

## VFX 위치 및 스케일 기준

| 효과 | 높이 (Vector3.up) | 스케일 | 용도 |
|------|------------------|--------|------|
| **buff_01** | 0.5f | 0.8f | 발밑 활성화 효과 |
| **buff_02a** | 0.1f | 0.4f | 발밑 지속 효과 (아처 전용) |
| **statusailment_01_aura** | 2.0f | 0.6f | 머리 위 상태 표시 |

---

## 주의 사항

### ✅ 올바른 방법

1. **VFXManager.GetVFXPrefab() 사용**
   ```csharp
   var prefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
   ```

2. **프리팹 캐싱**
   ```csharp
   if (cachedStatusPrefab == null)
   {
       cachedStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
   }
   ```

3. **Dictionary로 인스턴스 관리**
   ```csharp
   statusEffects[player] = statusInstance;
   ```

4. **반드시 정리**
   ```csharp
   // 버프 종료 시
   UnityEngine.Object.Destroy(statusEffects[player]);
   statusEffects.Remove(player);
   ```

### ❌ 금지 사항

1. **ZNetScene.GetPrefab() 직접 사용**
   ```csharp
   // ❌ 금지
   var prefab = ZNetScene.instance.GetPrefab("statusailment_01_aura");
   ```

2. **SimpleVFX.Play() 사용 (상태 VFX에는 부적합)**
   ```csharp
   // ❌ 상태 VFX는 수동 관리 필요
   SimpleVFX.Play("statusailment_01_aura", position, 5f);
   ```

3. **정리 누락**
   ```csharp
   // ❌ 메모리 누수 발생
   // Destroy 및 Dictionary.Remove 필수!
   ```

---

## 체크리스트

버프형 VFX 구현 시 다음 항목을 확인하세요:

- [ ] Dictionary<Player, GameObject> 선언
- [ ] 캐시 변수 (cachedPrefab) 선언
- [ ] VFXManager.GetVFXPrefab() 사용
- [ ] 프리팹 null 체크
- [ ] 기존 효과 제거 (중복 방지)
- [ ] SetParent로 캐릭터 추적
- [ ] localPosition 설정 (머리 위 2.0f)
- [ ] localScale 설정 (0.6f)
- [ ] Dictionary에 저장
- [ ] 버프 종료 시 Destroy + Remove
- [ ] 사망 시 정리 (CleanupOnDeath)

---

## 참고 파일

- **폭발 화살**: `SkillTree/SkillEffect.ExplosiveArrow.cs`
- **아처 멀티샷**: `SkillTree/SkillEffect.ArcherMultiShot.cs`
- **석궁 단 한발**: `SkillTree/SkillEffect.CrossbowOneShot.cs`
- **VFXManager**: `VFX/VFXManager.cs`

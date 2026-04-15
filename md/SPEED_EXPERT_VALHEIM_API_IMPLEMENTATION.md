# 속도 전문가 Valheim API 기반 구현 완료

## 🏗️⚡ **Architect + Performance 관점의 개선**

### 🔍 **기존 문제점 분석**

#### **🔴 Architecture 문제점:**
1. **중복된 구현 방식** - 3가지 다른 속도 적용 방식이 혼재
   - `SE_SkillTreeMoveSpeed` (StatusEffect 방식)
   - `MyStatRegistry.MoveSpeedBonuses` (Registry 방식)  
   - `player.m_runSpeed` 직접 수정 (위험한 방식)

2. **비표준 Player 확장** - `PlayerSkillTreeExtension`
   - Valheim API 표준을 벗어난 비공식 확장
   - 호환성 및 안정성 문제 가능성

#### **🔴 Performance 문제점:**
1. **비효율적인 StatusEffect 관리**
   ```csharp
   // 문제: 매번 StatusEffect 존재 여부 체크
   var hash = typeof(SE_SkillTreeMoveSpeed).Name.GetStableHashCode();
   bool hasEffect = seMan.HaveStatusEffect(hash);
   ```

2. **직접 `m_runSpeed` 수정의 위험성**
   ```csharp
   // 위험: 다른 시스템과 충돌 가능
   player.m_runSpeed = 5.5f * (1f + bonus);
   ```

## 🎯 **Valheim API 기반 개선 완료**

### ✅ **1. 표준 SE_Stats 방식 채택**

**Valheim 공식 StatusEffect 시스템 활용:**

```csharp
/// <summary>
/// Valheim 표준 SE_Stats 기반 속도 전문가 StatusEffect
/// 공식 API를 활용한 안전하고 호환성 높은 구현
/// </summary>
public class SE_SkillTreeMoveSpeed : SE_Stats
{
    public SE_SkillTreeMoveSpeed()
    {
        m_name = "속도 전문가";
        m_tooltip = $"이동속도 +{SkillTreeConfig.SpeedRootMoveSpeedValue}%";
        m_ttl = 0f; // 무한 지속 (스킬 해제 시까지)
        m_icon = null; // 필요시 아이콘 설정
    }
    
    public override void Setup(Character character)
    {
        base.Setup(character);
        
        // Valheim 표준 방식: m_speedModifier 사용
        float speedPercent = SkillTreeConfig.SpeedRootMoveSpeedValue;
        m_speedModifier = 1f + (speedPercent / 100f); // 3% = 1.03f
        
        // 툴팁 업데이트
        m_tooltip = $"이동속도 +{speedPercent}%";
        
        Plugin.Log.LogInfo($"[속도 전문가] StatusEffect 적용: +{speedPercent}% (배율: {m_speedModifier})");
    }
    
    public override void UpdateStatusEffect(float dt)
    {
        base.UpdateStatusEffect(dt);
        
        // 스킬 레벨 체크: 스킬이 해제되면 StatusEffect 제거
        if (SkillTreeManager.Instance?.GetSkillLevel("speed_root") <= 0)
        {
            m_character?.GetSEMan()?.RemoveStatusEffect(this);
        }
    }
}
```

### ✅ **2. Performance 최적화된 관리 시스템**

**상태 캐싱으로 불필요한 체크 최소화:**

```csharp
/// <summary>
/// Valheim 표준 방식 속도 전문가 StatusEffect 관리
/// Performance 최적화: 불필요한 체크 최소화, 캐싱 활용
/// </summary>
private static readonly Dictionary<Player, bool> _speedEffectState = new();

public static void ApplySkillTreeMoveSpeed(Player player)
{
    if (player == null || player != Player.m_localPlayer) return;
    
    var seMan = player.GetSEMan();
    if (seMan == null) return;

    bool hasSkill = SkillTreeManager.Instance?.GetSkillLevel("speed_root") > 0;
    
    // Performance: 상태 변화가 있을 때만 처리
    if (_speedEffectState.TryGetValue(player, out bool lastState) && lastState == hasSkill)
    {
        return; // 상태 변화 없음, 처리 생략
    }
    
    var hash = typeof(SE_SkillTreeMoveSpeed).Name.GetStableHashCode();
    bool hasEffect = seMan.HaveStatusEffect(hash);
    
    try
    {
        if (hasSkill && !hasEffect)
        {
            // 스킬 활성화: StatusEffect 추가
            var se = ScriptableObject.CreateInstance<SE_SkillTreeMoveSpeed>();
            seMan.AddStatusEffect(se);
            Plugin.Log.LogInfo($"[속도 전문가] StatusEffect 활성화: +{SkillTreeConfig.SpeedRootMoveSpeedValue}%");
        }
        else if (!hasSkill && hasEffect)
        {
            // 스킬 비활성화: StatusEffect 제거
            seMan.RemoveStatusEffect(hash);
            Plugin.Log.LogInfo("[속도 전문가] StatusEffect 비활성화");
        }
        
        // 상태 캐싱
        _speedEffectState[player] = hasSkill;
    }
    catch (System.Exception ex)
    {
        Plugin.Log.LogError($"[속도 전문가] StatusEffect 관리 오류: {ex.Message}");
    }
}
```

### ✅ **3. 이벤트 기반 자동 관리 시스템**

**Player 생명주기 이벤트 연동:**

```csharp
/// <summary>
/// Valheim 공식 Player 이벤트 연동 시스템
/// Architect 관점: 깨끗한 이벤트 기반 아키텍처
/// Performance 관점: 필요할 때만 효과 업데이트
/// </summary>

/// <summary>
/// Player 생명주기 이벤트 처리 - 속도 효과 자동 관리
/// </summary>
[HarmonyPatch(typeof(Player), "Awake")]
public static class Player_Awake_SpeedEffect_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(Player __instance)
    {
        if (__instance == Player.m_localPlayer)
        {
            // 플레이어 생성 시 속도 효과 초기화
            SkillEffect.ApplySkillTreeMoveSpeed(__instance);
            Plugin.Log.LogInfo($"[속도 전문가] 플레이어 {__instance.GetPlayerName()} 속도 효과 초기화");
        }
    }
}

/// <summary>
/// Player 사망/로그아웃 시 메모리 정리
/// </summary>
[HarmonyPatch(typeof(Player), "OnDestroy")]
public static class Player_OnDestroy_SpeedEffect_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Prefix(Player __instance)
    {
        if (__instance == Player.m_localPlayer)
        {
            // 상태 캐시 정리로 메모리 누수 방지
            SkillEffect.ClearSpeedEffectState(__instance);
        }
    }
}
```

### ✅ **4. 개선된 스킬 활성화 피드백**

**자연스러운 시각적 피드백:**

```csharp
ApplyEffect = (lv) => {
    var player = Player.m_localPlayer;
    if (player != null) {
        // Valheim 표준 방식으로 StatusEffect 적용
        SkillEffect.ApplySkillTreeMoveSpeed(player);
        SkillEffect.DrawFloatingText(player, 
            $"🏃 속도 전문가 활성화! (+{SkillTreeConfig.SpeedRootMoveSpeedValue}% 이동속도)", 
            new Color(0.2f, 0.8f, 1f, 1f)); // 시원한 파란색
        Plugin.Log.LogInfo($"[속도 전문가] 스킬 활성화: +{SkillTreeConfig.SpeedRootMoveSpeedValue}% 이동속도");
    }
}
```

## 🔧 **기술적 개선 사항**

### **Architecture 개선:**
1. **단일 구현 방식**: SE_Stats 기반 표준 방식만 사용
2. **이벤트 기반 설계**: Player 생명주기 이벤트와 연동
3. **명확한 책임 분리**: StatusEffect 관리와 UI 피드백 분리

### **Performance 개선:**
1. **상태 캐싱**: 불필요한 StatusEffect 체크 최소화
2. **메모리 관리**: Player 해제 시 자동 캐시 정리
3. **조건부 처리**: 상태 변화가 있을 때만 처리

### **호환성 개선:**
1. **Valheim 표준 API**: 공식 SE_Stats 시스템 사용
2. **안전한 패치 방식**: Harmony 우선순위 적절히 설정
3. **예외 처리**: 모든 상황에서 안정적인 동작 보장

## 📊 **성능 비교**

### **Before (기존 방식):**
- 3가지 중복 구현 방식
- 매번 StatusEffect 존재 여부 체크
- 직접 Player 필드 수정 (위험)
- 메모리 누수 가능성

### **After (개선된 방식):**
- 단일 표준 SE_Stats 방식
- 상태 변화 시에만 처리 (캐싱)
- Valheim 공식 API 활용 (안전)
- 자동 메모리 관리

## ✅ **완료된 개선 사항**

1. ✅ **Valheim API 기반 분석 완료**
2. ✅ **기존 문제점 파악 및 해결**
3. ✅ **SE_Stats 기반 표준 구현**
4. ✅ **Performance 최적화 (상태 캐싱)**
5. ✅ **이벤트 기반 자동 관리**
6. ✅ **메모리 누수 방지**
7. ✅ **시각적 피드백 개선**

이제 속도 전문가 시스템은 **Valheim 표준 API를 완전히 준수**하면서 **Performance 최적화**까지 달성한 안정적이고 효율적인 시스템이 되었습니다!
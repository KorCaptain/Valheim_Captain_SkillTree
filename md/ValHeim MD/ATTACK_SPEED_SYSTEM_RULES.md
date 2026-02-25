# Rule 14: 공격 속도 시스템 규칙

## 📋 개요

CaptainSkillTree의 공격 속도 시스템은 **AnimationSpeedManager.dll**을 사용하여 Valheim의 공격 애니메이션 속도를 제어합니다.

---

## 🎯 핵심 규칙

### Rule 14-1: AnimationSpeedManager.dll 참조 필수

**Captain_SkillTree.csproj 설정**:
```xml
<!-- AnimationSpeedManager.dll 참조 (배포 시 포함) -->
<Reference Include="AnimationSpeedManager">
  <HintPath>Lib\AnimationSpeedManager.dll</HintPath>
  <Private>True</Private>  ← 배포 시 DLL 포함!
</Reference>
```

**중요 사항**:
- ✅ `<Private>True</Private>` 필수 - 배포 패키지에 DLL 포함
- ✅ Lib 폴더에 AnimationSpeedManager.dll (8KB) 필요
- ❌ 리플렉션 방식 (Type.GetType) 사용 금지
- ❌ `<Private>False</Private>` 사용 금지 - 배포 시 작동 안 함

---

### Rule 14-2: 직접 호출 방식 사용

**올바른 구현 (WackyEpicMMOSystem 패턴)**:
```csharp
// ✅ 직접 호출 방식
AnimationSpeedManager.Add((character, speed) =>
{
    if (character is Player player && player.InAttack())
    {
        float attackSpeedBonus = GetTotalAttackSpeedBonus(player);

        if (attackSpeedBonus > 0f)
        {
            double bonusMultiplier = 1.0 + (attackSpeedBonus / 100.0);
            return speed * bonusMultiplier;
        }
    }
    return speed;
});
```

**잘못된 구현 (사용 금지)**:
```csharp
// ❌ 리플렉션 방식 - 사용 금지!
var animationSpeedManagerType = Type.GetType(
    "EpicLoot.MagicItemEffects.AnimationSpeedManager, EpicMMOSystem");
```

**이유**:
- 리플렉션은 namespace/assembly 이름 오류로 실패 가능
- 타입 안전성 없음 (런타임 오류)
- 코드 복잡도 증가
- 성능 저하

---

### Rule 14-3: Game.Awake 패치 패턴

**필수 구현 위치**: Plugin.cs

```csharp
/// <summary>
/// Game.Awake 패치 - AnimationSpeedManager에 공격속도 핸들러 등록
/// </summary>
[HarmonyLib.HarmonyPatch(typeof(Game), "Awake")]
public static class CaptainSkillTree_AnimationSpeedManager_Patch
{
    private static bool _attackSpeedHandlerRegistered = false;

    [HarmonyLib.HarmonyPostfix]
    public static void Postfix(Game __instance)
    {
        if (_attackSpeedHandlerRegistered) return;  // 중복 방지

        try
        {
            AnimationSpeedManager.Add((character, speed) =>
            {
                if (character is Player player && player.InAttack())
                {
                    float attackSpeedBonus =
                        CaptainSkillTree.SkillTree.SkillEffect.GetTotalAttackSpeedBonus(player);

                    if (attackSpeedBonus > 0f)
                    {
                        double bonusMultiplier = 1.0 + (attackSpeedBonus / 100.0);
                        double modifiedSpeed = speed * bonusMultiplier;

                        Plugin.Log.LogDebug(
                            $"[공격 속도] {player.GetPlayerName()} 속도 변경: " +
                            $"{speed:F3} → {modifiedSpeed:F3} (+{attackSpeedBonus}%)");

                        return modifiedSpeed;
                    }
                }
                return speed;
            });

            _attackSpeedHandlerRegistered = true;
            Plugin.Log.LogInfo("[공격 속도] AnimationSpeedManager 핸들러 등록 완료 (Game.Awake)");
        }
        catch (System.Exception ex)
        {
            Plugin.Log.LogError($"[공격 속도] AnimationSpeedManager 등록 실패: {ex.Message}");
        }
    }
}
```

**핵심 포인트**:
- ✅ `Game.Awake` Postfix 사용 (AnimationSpeedManager 초기화 이후)
- ✅ `_attackSpeedHandlerRegistered` 중복 방지 플래그
- ✅ `player.InAttack()` 조건 필수
- ❌ `Plugin.Awake()`에서 호출 금지 - 타이밍 오류

---

### Rule 14-4: GetTotalAttackSpeedBonus() 구현

**필수 구현 위치**: SkillEffect.SpeedTree.cs 또는 SkillEffect.cs

```csharp
/// <summary>
/// 플레이어의 총 공격속도 보너스 계산
/// </summary>
public static float GetTotalAttackSpeedBonus(Player player)
{
    if (player == null) return 0f;

    float totalBonus = 0f;
    var weapon = player.GetCurrentWeapon();
    if (weapon == null) return 0f;

    try
    {
        // 검 빠른 베기 - 검 착용 시만
        if (HasSkill("sword_step1_fastslash"))
        {
            if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
            {
                totalBonus += SkillTreeConfig.SwordStep1FastSlashSpeedValue;
                Plugin.Log.LogDebug($"[공격 속도] 검 빠른 베기: +{SkillTreeConfig.SwordStep1FastSlashSpeedValue}%");
            }
        }

        // 추가 공격속도 스킬들...

        return totalBonus;
    }
    catch (System.Exception ex)
    {
        Plugin.Log.LogError($"[공격 속도] 계산 오류: {ex.Message}");
        return 0f;
    }
}
```

**핵심 포인트**:
- ✅ 무기 타입 검증 필수 (Swords, Axes, Spears 등)
- ✅ Config 값 사용 (하드코딩 금지)
- ✅ Debug 로그로 적용 내역 출력
- ✅ 예외 처리

---

### Rule 14-5: InAttack() 조건 필수

**필수 조건**:
```csharp
if (character is Player player && player.InAttack())
{
    // 공격 속도 보너스 적용
}
```

**이유**:
- ✅ 공격 중일 때만 속도 변경 적용
- ✅ 이동/점프/기타 동작에 영향 없음
- ❌ `player.m_currentAttack != null` 사용 금지 - protected 멤버

---

### Rule 14-6: 배포 시 AnimationSpeedManager.dll 포함 필수

**빌드 결과물**:
```
📁 CaptainSkillTree_v1.0.0.zip
├── CaptainSkillTree.dll (22MB)
└── AnimationSpeedManager.dll (8KB)  ← 필수!
```

**설치 위치**:
```
BepInEx/plugins/Captain_Skilltree/
├── CaptainSkillTree.dll
└── AnimationSpeedManager.dll
```

**주의 사항**:
- ❌ AnimationSpeedManager.dll 누락 시 TypeLoadException 발생
- ✅ WackyEpicMMOSystem에도 포함되어 있지만, 독립 배포 위해 포함 필수
- ✅ 중복되어도 문제 없음 (같은 파일, 8KB)

---

## 🔍 디버깅 로그

### 정상 작동 로그

**게임 시작 시**:
```
[Info :CaptainSkillTree] [공격 속도] AnimationSpeedManager 핸들러 등록 완료 (Game.Awake)
```

**공격 시** (F5 Console에서 확인):
```
[Debug:CaptainSkillTree] [공격 속도] 검 빠른 베기: +5%
[Debug:CaptainSkillTree] [공격 속도] PlayerName 속도 변경: 1.000 → 1.050 (+5%)
```

### 오류 로그

**TypeLoadException**:
```
Could not load file or assembly 'AnimationSpeedManager'
```
→ **해결**: AnimationSpeedManager.dll을 plugins 폴더에 복사

**핸들러 등록 실패**:
```
[Error :CaptainSkillTree] [공격 속도] AnimationSpeedManager 등록 실패
```
→ **해결**: .csproj에 DLL 참조 확인, 직접 호출 방식 사용

---

## 📚 참고 구현

### WackyEpicMMOSystem 패턴 (Plugin.cs L436)

```csharp
AnimationSpeedManager.Add((character, speed) => {
    if (character is not Player player ||
        !player.InAttack() ||
        player.m_currentAttack is null ||
        (LevelSystem.Instance.getParameter(Parameter.Agility) == 0) ||
        player.GetCurrentWeapon().m_shared.m_skillType == Skills.SkillType.Bows)
    {
        return speed;
    }
    return speed * (1 + LevelSystem.Instance.getAddAttackSpeed() / 100);
});
```

### EpicLoot 패턴 (ModifyAttackSpeed.cs)

```csharp
[HarmonyPatch(typeof(Game), nameof(Game.Awake))]
public static class ModifyAttackSpeed_ApplyAnimationHandler_Patch
{
    internal static bool appliedAttackSpeed = false;

    private static void Postfix(Game __instance)
    {
        if (appliedAttackSpeed == false)
        {
            AnimationSpeedManager.Add((character, speed) => ModifyAttackSpeed(character, speed));
            appliedAttackSpeed = true;
        }
    }
}
```

---

## ⚠️ 금지 사항

1. ❌ **리플렉션 방식 사용 금지**
   - Type.GetType(), GetMethod(), Invoke() 사용 불가

2. ❌ **Plugin.Awake에서 등록 금지**
   - 반드시 Game.Awake Postfix 사용

3. ❌ **m_currentAttack 직접 접근 금지**
   - protected 멤버, InAttack() 메서드 사용

4. ❌ **<Private>False</Private> 사용 금지**
   - 배포 시 DLL 포함 안 됨

5. ❌ **AnimationSpeedManager.dll 배포 누락 금지**
   - 사용자가 설치 시 TypeLoadException 발생

---

## ✅ 권장 사항

1. ✅ **WackyEpicMMOSystem 패턴 준수**
   - 검증된 구현 방식 따르기

2. ✅ **무기 타입별 속도 보너스 구분**
   - Swords, Axes, Spears 등 무기별 개별 적용

3. ✅ **Config 기반 수치 관리**
   - 하드코딩 대신 SkillTreeConfig 사용

4. ✅ **Debug 로그 활용**
   - 속도 변경 내역 로그로 확인

5. ✅ **예외 처리 철저히**
   - try-catch로 안정성 확보

---

## 🔗 관련 규칙

- **Rule 1**: MMO 시스템 연동 - AnimationSpeedManager는 MMO와 독립적
- **Rule 2**: VFX/사운드 규칙 - 공격속도는 시각/사운드 효과 없음
- **Rule 7**: Config 관리 - 공격속도 수치는 Config로 관리
- **Rule 11**: 데미지 시스템 - 공격속도는 데미지와 독립적

---

**마지막 업데이트**: 2025-12-07
**검증 상태**: ✅ 게임 내 테스트 완료

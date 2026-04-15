# 패링(Perfect Block) 감지 시스템

## 핵심 문제

Valheim의 `BlockAttack()`은 **일반 막기와 패링 모두** `true`를 반환한다.
패링(퍼펙트 블록)만 구분하려면 별도의 감지 방법이 필요하다.

---

## 시도한 접근법과 결과

### 접근법 1: m_blockTimer 체크 (실패)

```csharp
// BlockAttack Prefix에서 m_blockTimer 확인
[HarmonyPrefix]
public static void Prefix(Humanoid __instance, out float __state)
{
    __state = Traverse.Create(__instance).Field("m_blockTimer").GetValue<float>();
}

[HarmonyPostfix]
public static void Postfix(Humanoid __instance, float __state)
{
    // __state != -1 이면 패링?
    if (__state != -1f) { /* 패링으로 판단 */ }
}
```

**실패 원인**: `m_blockTimer`는 방패를 들고 있는 동안 계속 `!= -1`이다.
시간 기반 값(`Time.time`)이 설정되므로, 블로킹 중이면 항상 True가 된다.
**결과**: 일반 막기에서도 작동됨

---

### 접근법 2: attacker.IsStaggering() 체크 (실패)

```csharp
// BlockAttack Postfix에서 공격자 Stagger 상태 확인
[HarmonyPostfix]
public static void Postfix(Character __instance, Character attacker)
{
    if (attacker.IsStaggering()) { /* 패링으로 판단 */ }
}
```

**실패 원인**: `BlockAttack` Postfix 시점에서 `attacker.Stagger()`가 아직 호출되지 않았거나,
같은 프레임에서 상태가 즉시 반영되지 않는다.
**결과**: 패링해도 항상 False, 아예 작동 안 됨

---

### 접근법 3: Character.Stagger() 패치 (성공)

**원리**: Valheim은 패링 성공 시 공격자에게 `Stagger()`를 호출한다.
일반 막기에서는 `Stagger()`가 호출되지 않는다.
따라서 `Character.Stagger()` 자체를 패치하면 패링만 정확히 감지 가능하다.

```csharp
[HarmonyPatch(typeof(Character), nameof(Character.Stagger))]
public static class ParryRush_Stagger_Patch
{
    public static void Postfix(Character __instance)
    {
        try
        {
            // __instance = Stagger 당하는 대상 (몬스터)
            // 플레이어가 Stagger 당하는 경우 무시
            if (__instance == null || __instance.IsPlayer()) return;

            var player = Player.m_localPlayer;
            if (player == null || player.IsDead()) return;

            // 액티브 스킬 버프 확인
            if (!Sword_Skill.IsParryRushActive(player)) return;

            // 플레이어가 현재 블로킹 중인지 확인
            // (몬스터가 Stagger되는 다른 원인 필터링)
            if (player.IsBlocking())
            {
                Sword_Skill.OnParryRushTrigger(player, __instance);
            }
        }
        catch (System.Exception ex)
        {
            Plugin.Log.LogError($"[패링 돌격] Stagger 패치 오류: {ex.Message}");
        }
    }
}
```

---

## 감지 로직 요약

```
패링 발생 시 Valheim 내부 흐름:
1. 플레이어가 방패를 든 상태에서 타이밍 맞춰 막기
2. Humanoid.BlockAttack() 호출 → true 반환
3. 패링 성공 → attacker.Stagger() 호출  <-- 여기를 감지!
4. 일반 막기 → Stagger() 호출 안 됨

감지 조건:
- Character.Stagger() Postfix 진입
- __instance.IsPlayer() == false (몬스터가 Stagger 당함)
- player.IsBlocking() == true (플레이어가 막기 중)
```

---

## 패링 vs 일반 막기 구분 요약

| 상황 | BlockAttack 반환 | Stagger() 호출 | 감지 가능 |
|------|-----------------|----------------|----------|
| 패링 (퍼펙트 블록) | true | O (공격자에게) | O |
| 일반 막기 | true | X | O (역으로) |
| 막기 실패 | false | X | - |

---

## 주의사항

### 1. Stagger가 호출되는 다른 상황
- 높은 데미지로 인한 스태거 (전투 중 누적)
- 보스 특수 공격

**대응**: `player.IsBlocking()` 조건으로 필터링.
플레이어가 막기 중일 때 몬스터가 Stagger되면 패링으로 판단.

### 2. BlockAttack Postfix와 분리
패링 감지(`Character.Stagger`)와 일반 블록 효과(`Humanoid.BlockAttack`)를 분리한다:
- **BlockAttack Postfix**: 칼날 되치기, 반격 자세 등 (블록/패링 공통 효과)
- **Stagger Postfix**: 패링 돌격 등 (패링 전용 효과)

### 3. 멀티플레이어 고려
- `Player.m_localPlayer`를 사용하므로 로컬 플레이어만 처리
- 다른 플레이어의 패링은 각자 클라이언트에서 감지

---

## 적용 스킬

| 스킬 ID | 이름 | 감지 방식 | 위치 |
|---------|------|----------|------|
| sword_step5_defswitch | 패링 돌격 | Stagger Postfix | Plugin.Patches.cs |
| sword_counter | 칼날 되치기 | BlockAttack Postfix | Plugin.Patches.cs |
| sword_riposte | 반격 자세 | BlockAttack Postfix | Plugin.Patches.cs |
| defense_Step5_parry | 막기달인 | BlockAttack Postfix | SkillEffect.DefenseTree.cs |

---

**작성일**: 2026-02-06
**관련 파일**: Plugin.Patches.cs (SwordSkillTreeParryPatch, ParryRush_Stagger_Patch)

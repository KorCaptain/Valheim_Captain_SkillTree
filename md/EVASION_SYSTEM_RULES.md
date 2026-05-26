# Rule 15: 회피 시스템 규칙

## 📋 개요

CaptainSkillTree의 회피 시스템은 **4가지 핵심 요소**로 구성됩니다:
1. **회피 확률 (Dodge Chance)** - 공격을 완전히 회피하는 확률 (누적 합산)
2. **회피 기울기 모션** - 회피 성공 시 상체를 공격자 반대 방향으로 기울였다가 복원 (발 고정)
3. **구르기 무적시간 (Dodge Invincibility)** - 구르기 중 피해를 받지 않는 시간
4. **구르기 스태미나 (Dodge Stamina)** - 구르기 시 소모되는 스태미나

---

## ⚡ 두 가지 회피 시스템 구분

| 항목 | 발헤임 기본 구르기 | CaptainSkillTree 공격 회피 확률 |
|------|-----------------|-------------------------------|
| 발동 방법 | Space/Roll 키 직접 입력 | 공격 피격 시 **자동 확률 판정** |
| 구현 함수 | `Player.Dodge()` | `Damage_Patch.Prefix()` |
| 설정 API | `m_dodgeInvincibilityTimer` | `player.SetCustomDodgeChance()` |
| 결과 | 구르기 애니메이션 + 무적 프레임 | 피해 0 + 기울기 모션 (몸 이동 없음) |
| 스태미나 소모 | ✅ 구르기 스태미나 소모 | ❌ 소모 없음 |
| 적용 스킬 | Rule 15-4, 15-5 (무적시간/스태미나) | Rule 15-1, 15-2, 15-3 (확률/모션/쿨타임) |

> **이 문서 Rule 15-1~15-3의 "회피"는 전부 공격 확률 회피(Attack Evasion)입니다.**  
> 발헤임의 구르기 동작과 **별개**로 작동하며, 구르기 없이 서 있는 상태에서도 발동합니다.

---

## 🎯 회피 스킬 전체 구조

| 스킬 ID | 스킬명 | 트리 | 회피율 | 쿨타임 | 무기 조건 | 회피 타입 | 동작 방식 |
|---------|--------|------|--------|--------|----------|---------|-----------|
| `defense_Step3_agile` | 회피단련 | 방어 | +15% | 없음 | 없음 | **공격 회피** | 항상 누적 |
| `defense_Step5_stamina` | 기민함 | 방어 | +10% | 없음 | 없음 | **공격 회피** | 항상 누적 |
| `defense_Step6_attack` | 신경강화 | 방어 | +8% | **45초** | 없음 | **공격 회피** | 준비 시 누적, 발동 시 쿨타임 |
| `knife_step2_evasion` | 회피 숙련 | 단검 | +15% | 없음 | 없음 | **공격 회피** | 항상 누적 |
| `knife_step5_crit_rate` | 공격과 회피 | 단검 | 임시 % | 자체 타이머 | 단검 | **공격 회피** | `knifeAttackEvasionEndTime`으로 독립 관리 |
| `spear_Step2_evasion` | 회피 찌르기 | 창 | 임시 % | 버프 지속시간 | **창 착용 필수** | **공격 회피** | `IsSpearEvasionBuffActive()`로 독립 관리 |
| `Rogue` | 로그 직업 | 직업 | Lv1:4%~Lv5:12% | 없음 | 없음 | **공격 회피** | 항상 누적, `GetRogueDodgeChance(lv)` |

**최대 기본 회피 확률**: 15% + 10% + 8% + 15% + 12%(Rogue Lv5) = **60%** (신경강화 준비 + 로그 Lv5 기준)

---

## 🎯 핵심 규칙

### Rule 15-1: 회피 확률 시스템 (UpdateDefenseDodgeRate)

**구현 위치**: `SkillEffect.StatTree.cs` - `UpdateDefenseDodgeRate()`

```csharp
public static void UpdateDefenseDodgeRate(Player player)
{
    float totalDodge = 0f;

    // defense_Step3_agile: 회피단련 +15% (항상 누적)
    if (manager.GetSkillLevel("defense_Step3_agile") > 0)
        totalDodge += Defense_Config.AgileDodgeBonusValue / 100f;

    // defense_Step5_stamina: 기민함 +10% (항상 누적)
    if (manager.GetSkillLevel("defense_Step5_stamina") > 0)
        totalDodge += Defense_Config.StaminaDodgeBonusValue / 100f;

    // defense_Step6_attack: 신경강화 +8% (쿨타임 중이면 제외)
    if (manager.GetSkillLevel("defense_Step6_attack") > 0)
    {
        bool isInCooldown = nerveLastEvasionTime.ContainsKey(player) &&
                            Time.time - nerveLastEvasionTime[player] < 45f;
        if (!isInCooldown)
            totalDodge += Defense_Config.AttackDodgeBonusValue / 100f;
    }

    // knife_step2_evasion: 회피 숙련 +15% (항상 누적, 무기 무관)
    if (manager.GetSkillLevel("knife_step2_evasion") > 0)
        totalDodge += Knife_Config.KnifeEvasionBonusValue / 100f;

    // knife_step5_crit_rate: 공격과 회피 임시 % (단검 착용 + 버프 활성 시)
    float attackEvasionBonus = GetKnifeAttackEvasionBonus(player);
    if (attackEvasionBonus > 0f)
        totalDodge += attackEvasionBonus / 100f;

    // spear_Step2_evasion: 회피 찌르기 임시 % (창 착용 + 버프 활성 시)
    if (IsSpearEvasionBuffActive(player))
        totalDodge += Spear_Config.SpearStep3EvasionBonusValue / 100f;

    player.SetCustomDodgeChance(totalDodge);
}
```

**핵심 포인트**:
- ✅ 모든 회피 보너스는 **가산 방식** (`totalDodge +=`)
- ✅ 신경강화는 쿨타임 중 자동 제외, 만료 시 자동 복구
- ✅ 무기 조건부 스킬(창/단검)은 독립 버프 타이머로 관리
- ❌ 곱셈 방식 사용 금지
- ❌ `m_dodgeSkill` 직접 수정 금지

---

### Rule 15-2: 신경강화 쿨타임 관리 규칙 ⚠️

**구현 위치**: `Plugin.Systems.cs` - `NerveEnhancementSystem.Damage_Patch.Prefix()`

#### 쿨타임 시작 조건 (핵심 버그 방지 규칙)

```csharp
// 회피 성공 시 — 신경강화 쿨타임 처리
if (manager?.GetSkillLevel("defense_Step6_attack") > 0)
{
    // ⚠️ 반드시 준비 상태 확인 후 쿨타임 시작
    // 이미 쿨타임 중인 경우 = 다른 스킬(회피단련/기민함/회피 숙련)이 회피한 것
    // → 신경강화 쿨타임 재시작 금지
    bool nerveIsReady = !SkillEffect.nerveLastEvasionTime.ContainsKey(player) ||
                        Time.time - SkillEffect.nerveLastEvasionTime[player] >= 45f;
    if (nerveIsReady)
    {
        SkillEffect.nerveLastEvasionTime[player] = Time.time;
        SkillEffect.UpdateDefenseDodgeRate(player);  // 8% 제외하여 회피율 재계산
        ActiveSkillCooldownRegistry.SetCooldownForSkill("PASS", "defense_Step6_attack", 45f);
        ActiveSkillHUD.Instance?.OnCooldownStarted();
        var nerveTimer = player.GetComponent<NerveEnhancementTimer>();
        if (nerveTimer == null)
            nerveTimer = player.gameObject.AddComponent<NerveEnhancementTimer>();
        nerveTimer.ResetTimer(player);
    }
}
```

#### 올바른 신경강화 동작 흐름

```
[신경강화 준비 상태]
  → 회피 풀에 8% 포함 (총 예: 33%)
  → 회피 성공 시 → 쿨타임 시작
  → UpdateDefenseDodgeRate() → 8% 제외 (총 25%)
  → 45초 후 → NerveEnhancementTimer → nerveLastEvasionTime 제거
  → UpdateDefenseDodgeRate() → 8% 복구 (총 33%)

[신경강화 쿨타임 중]
  → 회피 풀에 8% 미포함 (총 25%)
  → 다른 스킬로 회피 성공 → 신경강화 쿨타임 재시작 안 함
  → 타이머 자연 만료 후 준비 상태로 복귀
```

**❌ 금지 패턴 (이전 버그)**:
```csharp
// 쿨타임 확인 없이 무조건 갱신 → 신경강화가 영구적으로 비활성화되는 버그
if (manager?.GetSkillLevel("defense_Step6_attack") > 0)
{
    SkillEffect.nerveLastEvasionTime[player] = Time.time; // ← 버그: 항상 쿨타임 시작
}
```

---

### Rule 15-3: 회피 기울기 모션 (DodgeLeanComponent)

**구현 위치**: `Plugin.Systems.cs` - `NerveEnhancementSystem.Damage_Patch.Prefix()` (회피 성공 블록 내) + `DodgeLeanComponent` 클래스 (파일 끝)

회피 성공 시 캐릭터 상체가 공격자 반대 방향으로 30° 기울었다가 1초 후 복원. 발은 고정, 몸 이동 없음.

#### 트리거 코드 (Damage_Patch 내)

```csharp
// 회피 모션: 상체 기울기 (발 고정, 1초 복원)
var dodgeAttacker = hit.GetAttacker();
float leanSide;
if (dodgeAttacker != null)
{
    Vector3 away = player.transform.position - dodgeAttacker.transform.position;
    away.y = 0f;
    leanSide = (away.sqrMagnitude > 0.01f && Vector3.Dot(player.transform.right, away.normalized) >= 0f)
        ? 1f : -1f;
}
else
{
    leanSide = 1f;  // fallback: 오른쪽
}
var lean = player.gameObject.GetComponent<DodgeLeanComponent>();
if (lean == null) lean = player.gameObject.AddComponent<DodgeLeanComponent>();
lean.StartLean(leanSide);
```

**leanSide 방향 판정**: `Vector3.Dot(player.right, away)` — 플레이어 오른쪽 방향과 "공격자에서 멀어지는 방향"의 내적으로 좌/우를 결정. Unity에서 Euler Z 양수 = 왼쪽 기울기, 음수 = 오른쪽 기울기.

#### DodgeLeanComponent 클래스

```csharp
internal class DodgeLeanComponent : MonoBehaviour
{
    private float _side;
    private float _angle;
    private float _duration;
    private float _startTime;
    private bool _active;
    private Coroutine _coroutine;

    internal void StartLean(float side, float angle = 30f, float duration = 1f)
    {
        if (_active) return;  // 진행 중인 모션 방해 금지
        _side = side;
        _angle = angle;
        _duration = duration;
        _startTime = Time.time;
        _active = true;
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(LeanCoroutine());
    }

    private IEnumerator LeanCoroutine()
    {
        var player = GetComponent<Player>();
        if (player == null) { _active = false; yield break; }
        var visual = player.transform.Find("Visual");
        if (visual == null) { _active = false; yield break; }
        while (true)
        {
            yield return new WaitForEndOfFrame();
            float t = (Time.time - _startTime) / _duration;
            if (t >= 1f)
            {
                visual.localRotation = Quaternion.identity;
                _active = false;
                yield break;
            }
            float cur = Mathf.Lerp(_angle * _side, 0f, t);
            visual.localRotation = Quaternion.Euler(0f, 0f, cur);
        }
    }
}
```

#### 핵심 설계 결정

**왜 `player.transform.Find("Visual")` 인가?**
- Valheim의 캐릭터 계층: `Player(root)` → `Visual` → `Armature` → 모든 Bone
- `Visual` 은 Animator의 제어 범위 **위**에 있어 Animator가 덮어쓰지 못함
- 뼈(bone) 직접 회전 시 Valheim Animator는 프레임 간 초기화를 하지 않아 매 프레임 누적 발생
  - 예: `chest.rotation = Lean(30°) * R0 = R1` → `Lean(29°) * R1 = Lean(59°)*R0` → 급격히 커져 좌우 흔들림

**왜 `WaitForEndOfFrame` 인가?**
- `Update()` / `LateUpdate()` 보다 나중에 실행 → Valheim 지형 기울기·Animator 모두 완료 후 적용
- Visual에 직접 localRotation을 SET하므로 누적 없음

**`_active` 가드의 역할**
- 피격이 연속으로 발생해도 진행 중인 모션을 방해하지 않음
- 종료 시 `visual.localRotation = Quaternion.identity` 로 반드시 원상복구

**튜닝 파라미터**:
| 파라미터 | 기본값 | 의미 |
|---------|-------|------|
| `angle` | 30f | 최대 기울기 각도 (도) |
| `duration` | 1f | 복원까지 걸리는 시간 (초) |

---

### Rule 15-4: 구르기 무적시간 시스템 (Dodge Invincibility)

**구현 위치**: `SkillEffect.DefenseTree.cs` - `Player_Dodge_DefenseTree_Patch.Postfix`

```csharp
[HarmonyPatch(typeof(Player), "Dodge")]
public static class Player_Dodge_DefenseTree_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(Player __instance)
    {
        // defense_Step3_agile: 회피단련 — 무적시간 +20% (배율)
        if (manager.GetSkillLevel("defense_Step3_agile") > 0)
        {
            float bonus = Defense_Config.AgileInvincibilityBonusValue / 100f;
            var traverse = Traverse.Create(__instance);
            float current = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
            traverse.Field("m_dodgeInvincibilityTimer").SetValue(current * (1f + bonus));
        }

        // knife_step2_evasion: 회피 숙련 — 무적시간 +15% (단검 착용 시만)
        if (manager.GetSkillLevel("knife_step2_evasion") > 0 &&
            SkillEffect.IsUsingDagger(__instance))
        {
            float bonus = Knife_Config.KnifeEvasionBonusValue / 100f;
            var traverse = Traverse.Create(__instance);
            float current = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
            traverse.Field("m_dodgeInvincibilityTimer").SetValue(current * (1f + bonus));
        }
    }
}
```

**핵심 포인트**:
- ✅ `Player.Dodge` **Postfix** — 구르기 후 무적시간 수정
- ✅ **배율 방식**: `current * (1 + bonus%)` — 중첩 적용 가능
- ✅ 단검 회피는 **단검 착용 조건** 필수
- ❌ Prefix에서 수정 금지 (타이밍 오류)

**최대 무적시간**: 기본 × 1.20 × 1.15 = **+38%**

---

### Rule 15-5: 구르기 스태미나 감소 시스템 (Dodge Stamina)

**구현 위치**: `SkillEffect.DefenseTree.cs` - `Player_Dodge_DefenseTree_Patch.Prefix`

```csharp
[HarmonyPatch(typeof(Player), "Dodge")]
public static class Player_Dodge_DefenseTree_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Prefix(Player __instance, ref Vector3 dodgeDir)
    {
        // defense_Step5_stamina: 기민함 — 구르기 스태미나 -12%
        if (manager.GetSkillLevel("defense_Step5_stamina") > 0)
        {
            float reduction = Defense_Config.StaminaRollStaminaReductionValue / 100f;
            var traverse = Traverse.Create(__instance);
            float original = traverse.Field("m_dodgeStaminaUsage").GetValue<float>();
            traverse.Field("m_dodgeStaminaUsage").SetValue(original * (1f - reduction));
        }
    }
}
```

**핵심 포인트**:
- ✅ `Player.Dodge` **Prefix** — 구르기 전 스태미나 수정
- ✅ **감소 배율**: `original * (1 - reduction%)`
- ❌ Postfix에서 수정 금지 (이미 소모됨)

---

## 🔍 회피 발동 흐름 (Damage_Patch)

**구현 위치**: `Plugin.Systems.cs` - `NerveEnhancementSystem.Damage_Patch.Prefix()`

```
Character.Damage() 호출
  ↓
[Prefix 진입]
  ↓
dodgeChance = player.GetCustomDodgeChance()  ← UpdateDefenseDodgeRate()의 합산값
  ↓
roll < dodgeChance ? (회피 성공)
  ├─ 전체 데미지 타입 0으로 설정
  ├─ m_dodgeEffects.Create() + PlayVFXMultiplayer("sfx_dodge")
  ├─ DodgeLeanComponent.StartLean() 호출 (기울기 모션)  ← Rule 15-3
  ├─ 회피 메시지 표시
  ├─ 신경강화 쿨타임 처리 (nerveIsReady 확인)  ← Rule 15-2
  └─ return false  ← 원본 Damage() 실행 차단
```

---

## 📊 무기 조건부 스킬 독립 관리 규칙

무기 착용 조건이 있는 회피 스킬은 **UpdateDefenseDodgeRate() 외부에서 자체 타이머 관리**:

| 스킬 | 버프 부여 함수 | 활성 확인 함수 | 타이머 필드 |
|------|--------------|--------------|------------|
| 공격과 회피 (`knife_step5_crit_rate`) | 자동 (2연속 공격 시) | `GetKnifeAttackEvasionBonus()` | `knifeAttackEvasionEndTime` |
| 회피 찌르기 (`spear_Step2_evasion`) | `ApplySpearEvasionBuff()` | `IsSpearEvasionBuffActive()` | 별도 타이머 |

`UpdateDefenseDodgeRate()`에서는 이 함수들을 호출하여 활성 여부만 확인 후 `totalDodge +=`.

---

## 🚨 금지 사항

### 회피 확률
- ❌ `m_dodgeSkill` 직접 수정 — `SetCustomDodgeChance()` 사용 필수
- ❌ 곱셈 방식 — 가산 방식만 허용
- ❌ 신경강화 쿨타임 중 `nerveLastEvasionTime` 강제 갱신 — 다른 스킬 회피 시 재시작 금지

### 기울기 모션
- ❌ Bone 직접 회전 (`chest.rotation = lean * chest.rotation`) — Valheim Animator가 초기화하지 않아 매 프레임 누적, 좌우 흔들림 버그
- ❌ `m_pushForce` 사용 — 캐릭터가 4m 뒤로 밀려남, 발 이동 발생
- ❌ `_active` 가드 제거 — 연속 피격 시 LeanCoroutine 중복 실행
- ❌ Visual 복구 누락 — 모션 종료 후 `visual.localRotation = Quaternion.identity` 필수

### 무적시간
- ❌ Prefix에서 수정 — Postfix만 허용
- ❌ 가산 방식 — 배율 방식만 허용
- ❌ 단검 회피 무조건 적용 — `IsUsingDagger()` 확인 필수

### 스태미나
- ❌ Postfix에서 수정 — Prefix만 허용
- ❌ 가산 방식 — 감소 배율 방식만 허용

---

## 🧪 공격 회피(Attack Evasion) 인게임 확인 방법

### 확인 절차

1. 스킬 포인트를 회피 스킬에 투자
2. 갓모드 ON, 낮 고정 (`./valheim.sh god` → `./valheim.sh tod 0.5`)
3. 몬스터 소환 후 **구르기 없이 제자리에 서있기** (`./valheim.sh spawn Draugr 3`)
4. 여러 번 공격받으며 아래 현상 확인:

| 확인 항목 | 공격 회피 발동 시 현상 |
|---------|-------------------|
| 피해 숫자 | 뜨지 않음 (0 피해) |
| 상체 모션 | 공격자 반대 방향으로 30° 기울기 후 1초 복원 |
| 캐릭터 이동 | 없음 (발 고정) |
| 스태미나 소모 | 없음 |
| 구르기 애니메이션 | 없음 (구르기와 독립) |

### 코드 연결 경로

```
공격 피격
  → Character.Damage() 호출
  → Damage_Patch.Prefix() 진입
  → player.GetCustomDodgeChance() 값 참조
      ← UpdateDefenseDodgeRate()가 세팅한 값
      ← 방어(agile/stamina/nerve) + 단검 + 창 + 로그 스킬 모두 누적 가산
  → Random.value < dodgeChance? → 공격 회피 발동
  → HitData 타입 전부 0 설정 (피해 차단)
  → DodgeLeanComponent.StartLean() (기울기 모션)
```

### 구르기와 공격 회피 동시 작동

- 구르기(Dodge) 중에도 `Damage_Patch`는 발동함 → 구르기 무적 + 공격 회피 확률이 **동시에** 작동
- Rule 15-4·15-5(구르기 강화)와 Rule 15-1~15-3(공격 회피)은 **완전히 독립**

---

## 🧪 테스트 체크리스트

### 회피 확률
- [ ] 회피단련만: 15% 회피 확인
- [ ] 기민함 추가: 25% 회피 확인
- [ ] 신경강화 추가: 33% 회피 확인
- [ ] 회피 성공 후 신경강화 쿨타임 시작, 8% 제외 (25%로 감소) 확인
- [ ] 45초 후 신경강화 복구 (33%로 증가) 확인
- [ ] 쿨타임 중 다른 스킬로 회피 성공 → 신경강화 쿨타임 재시작 안 함 확인
- [ ] 회피 숙련 추가: 기본 누적 합산 확인

### 기울기 모션
- [ ] 회피 성공 시 발 고정 상태로 상체만 공격자 반대 방향으로 30° 기울기 확인
- [ ] 1초 후 `Quaternion.identity`로 복원 확인
- [ ] 연속 피격 시 진행 중인 모션이 중단되지 않음 (`_active` 가드) 확인
- [ ] 공격자 없을 시 오른쪽(leanSide=1f) fallback 확인

### 무적시간
- [ ] 회피단련: 구르기 무적시간 +20% 확인
- [ ] 회피 숙련 + 단검 착용: 무적시간 추가 +15% 확인
- [ ] 단검 미착용 시 회피 숙련 무적시간 미적용 확인

### 스태미나
- [ ] 기민함: 구르기 스태미나 -12% 확인

---

## 🔗 관련 파일

| 파일 | 역할 |
|------|------|
| `SkillEffect.StatTree.cs` | `UpdateDefenseDodgeRate()`, `nerveLastEvasionTime` |
| `Plugin.Systems.cs` | 회피 발동 판정, 슬라이드 모션, 신경강화 쿨타임 |
| `SkillEffect.DefenseTree.cs` | 구르기 무적시간/스태미나 Harmony 패치 |
| `SkillEffect.KnifeSkillEffects.cs` | `GetKnifeAttackEvasionBonus()` |
| `SkillEffect.SwordSpearSkillEffects.cs` | `ApplySpearEvasionBuff()`, `IsSpearEvasionBuffActive()` |

---

**마지막 업데이트**: 2026-05-13  
**변경 내용**: Rule 15-3 전면 재작성 — m_pushForce 슬라이드 제거 → DodgeLeanComponent + Visual transform Z-rotation 기울기 모션으로 교체 (angle=30°, duration=1s, 뼈 누적 버그 해결)  
**검증 상태**: ✅ 빌드 완료

# ARMOR_TOOLTIP_DISPLAY_RULES.md - 방어구/방패 아이템 툴팁 표시 규칙

## 📋 개요

**목적**: 방어구/방패 아이템에 마우스 오버 시 스킬 효과 및 보너스를 올바르게 표시하는 방법

** 표시 순서 **:
- 방어력
- 가드(막기) 방어력
- 체력
- 저항
- 회피
- 이동속도

** 표시 방법 **:

- 방어력(부위별 별도 표시) 관련 스킬명 : 방어력 +"+숫자"
- 방어력(방어구 공통 표시) 관련 스킬명 : 방어력 +"+숫자%"
- 가드(막기) 방어력(방어구 공통 표시) 관련 스킬명 : 가드 방어력 +"+숫자"
- 체력(방어구 공통 표시) 관련 스킬명 : 체력 +"+숫자"
- 체력(방어구 공통 표시) 관련 스킬명 : 체력 +"+숫자%"
- 저항(방어구 공통 표시) : 물리저항 +"스킬트리의 패시브에서 물리저항 관련효과 총합 + 직업 스킬의 물리저항" / 속성저항 +"스킬트리의 패시브에서 속성저항 관련효과 총합 + 직업 스킬의 속성 저항"
- 회피(방어구 공통 표시) : 회피 +"스킬트리의 패시브에서 회피 관련효과 총합 + 직업 스킬의 회피" (단, 확률성 회피는 표시 하지 않는다.)
- 이동속도(방어구 공통 표시) : 이동속도 +"스킬트리의 패시브에서 이동속도 관련효과 총합 + 직업 스킬의 이동속도" (단, 확률성 이동속도 증가는 표시 하지 않는다.)

** 표시 예시 **:
- 피부강화 : 방어력 +5 
-  : +100 


**구현 파일**: `SkillTree/SkillEffect.ArmorTooltip.cs`

**핵심 발견 사항 (디버그 로그로 확인)**:
- `GetTooltip()` 반환값은 **로컬라이제이션 전** 원시 텍스트
- 라인 감지 시 한국어 ("가드 방어력") **절대 사용 금지**
- 반드시 `$item_*` 원시 키로 감지해야 함

---

## Rule T1: GetTooltip 원시 텍스트 구조

### 실제 라인 형식 (로컬라이제이션 전)

```
[0]  '$item_shield_flametal_description'
[1]  ''
[2]  '$item_onehanded'
[3]  '$item_crafter: <color=orange>Test</color>'
[4]  '$item_weight: <color=orange>5.0</color>'
[5]  '$item_quality: <color=orange>1</color>'
[6]  '$item_durability: <color=orange>100%</color> <color=yellow>(200/200)</color>'
[7]  '$item_repairlevel: <color=orange>3</color>'
[8]  '$item_blockarmor: <color=orange>114</color> <color=yellow>(118)</color>'  ← 방패
[9]  '$item_blockforce: <color=orange>50</color>'
[10] '$item_parrybonus: <color=orange>1.5x</color>'
[11] '$item_parryadrenaline: <color=orange>5</color>'
[12] '$item_movement_modifier: <color=orange>-5%</color> ($item_total:<color=yellow>-15%</color>)'
```

### 방어구 라인 형식

```
$item_armor: <color=orange>22</color>          ← 투구/흉갑/각반
$item_blockarmor: <color=orange>114</color> <color=yellow>(118)</color>   ← 방패
```

### 발헤임 기본 색상 의미

| 색상 | 용도 | 예시 |
|------|------|------|
| `<color=orange>` | 현재 품질 값 | `<color=orange>114</color>` |
| `<color=yellow>` | 최대 품질 값 / 총합 보조 정보 | `<color=yellow>(118)</color>` |

---

## Rule T2: 라인 감지 규칙

### ✅ 올바른 감지 방식

```csharp
// 방패 블록파워 라인
if (!line.Contains("$item_blockarmor")) continue;

// 방어구 방어력 라인
if (!line.Contains("$item_armor")) continue;
```

### ❌ 절대 금지

```csharp
// ❌ 한국어 텍스트로 감지 → 패치 실행 시 미번역 상태
if (!line.Contains("가드 방어력")) continue;   // 항상 False!
if (!line.Contains("방어력")) continue;         // 항상 False!
if (!line.Contains("Block Power")) continue;    // 항상 False!

// ❌ $item_armor는 $item_blockarmor의 부분 문자열
// → isShield 분기 없이 사용 시 방패 라인도 매칭될 수 있음
```

> **이유**: `GetTooltip()`의 `__result`는 UI가 `Localization.Localize()` 호출 전 상태.
> 우리 Postfix가 실행되는 시점에는 `$item_*` 키가 번역되지 않은 원시 텍스트다.

---

## Rule T3: 표시 색상 규칙

### 색상 상수 (SkillEffect.ArmorTooltip.cs)

```csharp
private const string COL_TOTAL = "orange";    // 총합: 발헤임 기본 값 색상 (주황)
private const string COL_BASE  = "white";     // 기본 방어력: 흰색
private const string COL_BONUS = "#4FC3F7";   // 스킬 보너스: 파란색
private const string COL_ROCK  = "#00BFFF";   // 바위피부 %: 하늘색
private const string COL_GRAY  = "#808080";   // 괄호/연산자: 회색
```

### 표시 형식별 색상

**스킬 보너스만 (평타 고정값)**:
```
가드 방어력: [주황]214 [회색]([흰색]114 [회색]+ [파랑]100[회색])
```

**바위피부만 (% 보너스)**:
```
가드 방어력: [주황]128 [회색]([흰색]114[회색] * [하늘]12%[회색])
```

**스킬 + 바위피부 (둘 다)**:
```
가드 방어력: [주황]240 [회색](([흰색]114[회색] + [파랑]100[회색]) * [하늘]12%[회색])
```

**스킬 없음 (방패만 해당)**:
```
가드 방어력: [주황]114    ← yellow (118) 제거됨
```

---

## Rule T4: rawBase 계산 방법

### 방패 블록파워 (rawBase)

```csharp
// ✅ 올바름: m_shared 필드 직접 계산 (언패치된 기본값)
float rawBase = item.m_shared.m_blockPower +
                item.m_shared.m_blockPowerPerLevel * (qualityLevel - 1);
```

**GetBlockPower() 호출 금지 이유**:
- `GetBlockPower()`는 이미 패치되어 있음 (스킬 보너스 포함)
- 장착된 방패라면 스킬 보너스가 이미 더해진 값 반환 → 이중 계산 발생

### 방어구 방어력 (baseArmor)

```csharp
// ✅ 올바름: GetArmor()는 패치 대상이 아님
float baseArmor = item.GetArmor(qualityLevel, worldLevel);
```

---

## Rule T5: 전체 라인 교체 방식

`__result`를 줄 단위로 분리 후, 목표 라인을 새 포맷으로 교체:

```csharp
string[] lines = __result.Split('\n');

for (int i = 0; i < lines.Length; i++)
{
    if (!lines[i].Contains("$item_blockarmor")) continue;

    int colonIdx = lines[i].IndexOf(':');
    if (colonIdx < 0) break;

    string label = lines[i].Substring(0, colonIdx + 1); // "$item_blockarmor:"

    // 새 라인으로 교체 (로컬라이제이션은 UI가 나중에 수행)
    lines[i] = BuildLine(label, rawBase, flatBlockBonus, ...);
    break;
}

__result = string.Join("\n", lines);
```

**교체 후 UI 로컬라이제이션**:
- `$item_blockarmor:` → `가드 방어력:` (UI가 자동 변환)
- 우리가 추가한 색상 태그와 숫자는 그대로 유지됨

---

## Rule T6: 방패 yellow (최대값) 제거

발헤임 기본 방패 툴팁은 `<color=yellow>(118)</color>` 형식으로 최대 품질 값을 표시.
이는 스킬 유무와 무관하게 혼란을 주므로 **항상 제거**한다.

```csharp
// 방패는 스킬 없어도 항상 처리 (yellow 제거 목적)
if (!isShield && flatBonus == 0f && !rockSkinActive)
    return;  // 방어구는 스킬 없으면 스킵

// 스킬 없음 → yellow 제거만
lines[i] = $"{label} <color={COL_TOTAL}>{rawBase:F0}</color>";
```

| 이전 | 이후 |
|------|------|
| `가드 방어력: 114 (118)` | `가드 방어력: 114` |
| `가드 방어력: 114 (118)` (방패훈련 찍음) | `가드 방어력: 214 (114 + 100)` |

---

## Rule T7: 적용 대상 아이템 타입

| ItemType | 감지 키 | 보너스 스킬 |
|----------|---------|------------|
| `ItemType.Helmet` | `$item_armor` | `defense_root` |
| `ItemType.Chest` | `$item_armor` | `defense_Step1_survival` |
| `ItemType.Legs` | `$item_armor` | `defense_Step2_health` |
| `ItemType.Shield` | `$item_blockarmor` | `defense_Step3_shield`, `defense_Step5_parry` |

**공통 보너스** (모든 타입):
- `defense_Step4_tanker` (바위피부): 전 방어구/방패에 % 보너스 추가

---

## Rule T8: 패치 등록 형식

```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
    new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
public static class ItemData_GetTooltip_ArmorBonus_Patch
{
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Low)]
    private static void Postfix(ItemDrop.ItemData item, int qualityLevel, bool crafting,
        float worldLevel, int stackOverride, ref string __result)
    { ... }
}
```

**주의**: `GetTooltip`은 **static 메서드** → Postfix 첫 번째 파라미터가 `item` (인스턴스 아님)

---

## 🐛 디버그 방법

### BepInEx 로그로 툴팁 라인 확인

```csharp
// 임시 디버그 (문제 발생 시 추가, 해결 후 제거)
Plugin.Log.LogInfo($"[방어구 툴팁] 총 {lines.Length}개 라인");
for (int j = 0; j < lines.Length; j++)
    Plugin.Log.LogInfo($"  [{j}] '{lines[j]}'");
```

> **주의**: `LogDebug`는 BepInEx 기본 설정에서 출력 안 됨. 진단 시 `LogInfo` 사용.

### 로그 위치
```
{발헤임 설치 폴더}/BepInEx/LogOutput.log
```

---

## ✅ 구현 체크리스트

```yaml
방어구_툴팁_구현:
  감지:
    - [ ] $item_armor 키로 방어구 라인 감지
    - [ ] $item_blockarmor 키로 방패 라인 감지
    - [ ] "가드 방어력" 등 한국어 감지 절대 사용 금지
  색상:
    - [ ] 총합 값: <color=orange> (발헤임 원본 색상)
    - [ ] 기본 방어력: <color=white>
    - [ ] 스킬 보너스: <color=#4FC3F7> (파랑)
    - [ ] 바위피부 %: <color=#00BFFF> (하늘)
    - [ ] 괄호/연산자: <color=#808080> (회색)
  rawBase:
    - [ ] 방패: m_shared.m_blockPower + m_blockPowerPerLevel * (quality-1)
    - [ ] 방어구: item.GetArmor(qualityLevel, worldLevel)
    - [ ] GetBlockPower() 직접 호출 금지 (이중 계산 위험)
  방패_특이사항:
    - [ ] 스킬 없어도 항상 처리 (yellow 최대값 제거)
    - [ ] 조기 return 조건에 isShield 체크 포함
```

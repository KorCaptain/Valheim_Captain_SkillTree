# 숙련도 보너스 시스템 (Skill Proficiency System)

## 개요
스킬트리에서 습득한 숙련도 보너스를 Valheim의 기술 레벨에 적용하고, UI에 "기본 레벨 +보너스" 형식으로 표시하는 시스템.

## 핵심 기능

### 1. 숙련도 보너스 적용
- 스킬트리 노드 습득 시 해당 기술에 보너스 레벨 추가
- 사망해도 보너스 레벨 유지 (기본 레벨만 감소)
- 스킬트리 리셋 시 보너스 제거

### 2. UI 표시
- 형식: `기본레벨 +보너스` (예: `5 +8`)
- 보너스는 빨간색(`#FF3333`)으로 표시
- 기본 레벨 0이어도 보너스 표시 (예: `0 +10`)

## 파일 구조

| 파일 | 역할 |
|------|------|
| `SkillEffect.SpeedTree2.cs` | `GetSkillLevelBonus()` - 보너스 계산 |
| `SkillEffect.SkillUIDisplay.cs` | UI 패치 - 숙련도 창 표시 |
| `SkillTreeConfig.cs` | Config 값 (보너스 수치) |

## 구현 상세

### GetSkillLevelBonus (SkillEffect.SpeedTree2.cs)

스킬 타입별 총 보너스 계산:

```csharp
public static float GetSkillLevelBonus(Skills.SkillType skillType)
{
    float bonus = 0f;
    var manager = SkillTreeManager.Instance;
    if (manager == null) return bonus;

    switch (skillType)
    {
        case Skills.SkillType.Swords:
        case Skills.SkillType.Knives:
        case Skills.SkillType.Clubs:
        case Skills.SkillType.Polearms:
        case Skills.SkillType.Spears:
        case Skills.SkillType.Axes:
            // speed_ex1: 근접무기 숙련 +8
            if (manager.GetSkillLevel("speed_ex1") > 0)
                bonus += SkillTreeConfig.Tier2_수련자1_근접석궁숙련.Value;
            break;

        case Skills.SkillType.Crossbows:
            // speed_ex1: 석궁 숙련 +8
            if (manager.GetSkillLevel("speed_ex1") > 0)
                bonus += SkillTreeConfig.Tier2_수련자1_근접석궁숙련.Value;
            break;

        case Skills.SkillType.Bows:
            // speed_ex2: 활 숙련 +8
            if (manager.GetSkillLevel("speed_ex2") > 0)
                bonus += SkillTreeConfig.Tier2_수련자2_지팡이활숙련.Value;
            // bow_Step3_speedshot: 활 숙련 +10
            if (manager.GetSkillLevel("bow_Step3_speedshot") > 0)
                bonus += SkillTreeConfig.BowStep3SpeedShotSkillBonusValue;
            break;

        case Skills.SkillType.Jump:
            // all_master: 점프 +8
            if (manager.GetSkillLevel("all_master") > 0)
                bonus += SkillTreeConfig.Tier5_숙련자_스킬보너스.Value;
            // agility_peak: 점프 +10
            if (manager.GetSkillLevel("agility_peak") > 0)
                bonus += SkillTreeConfig.Tier4_점프숙련자_점프레벨.Value;
            break;

        case Skills.SkillType.Run:
            // all_master: 달리기 +8
            if (manager.GetSkillLevel("all_master") > 0)
                bonus += SkillTreeConfig.Tier5_숙련자_스킬보너스.Value;
            break;
    }
    return bonus;
}
```

### Skills.GetSkillLevel 패치

```csharp
[HarmonyPatch(typeof(Skills), nameof(Skills.GetSkillLevel))]
public static class Skills_GetSkillLevel_Patch
{
    public static void Postfix(Skills.SkillType skillType, ref float __result)
    {
        float bonus = SkillEffect.GetSkillLevelBonus(skillType);
        if (bonus > 0f)
        {
            __result += bonus;
        }
    }
}
```

### UI 표시 패치 (SkillEffect.SkillUIDisplay.cs)

```csharp
[HarmonyPatch(typeof(SkillsDialog), "Setup")]
public static class SkillsDialog_Setup_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(SkillsDialog __instance, Player player)
    {
        // m_elements 필드에서 스킬 요소 가져오기
        // 각 요소에서 TMP_Text 찾아서 포맷팅
    }
}
```

**처리 로직**:
1. `m_elements` 필드 리플렉션 접근
2. 각 요소의 TMP_Text 컴포넌트 수집
3. 스킬 이름 → SkillType 매핑 (한글/영어 지원)
4. `GetSkillLevelBonus()`로 보너스 계산
5. 보너스 있으면 `기본 <color=#FF3333>+보너스</color>` 형식으로 표시
6. 다중 텍스트 컴포넌트 처리 (첫 번째만 표시, 나머지 비움)

## 한글 스킬 이름 매핑

```csharp
_skillNameMap["검"] = Skills.SkillType.Swords;
_skillNameMap["칼"] = Skills.SkillType.Knives;
_skillNameMap["둔기"] = Skills.SkillType.Clubs;
_skillNameMap["창"] = Skills.SkillType.Spears;
_skillNameMap["장창"] = Skills.SkillType.Polearms;
_skillNameMap["폴암"] = Skills.SkillType.Polearms;
_skillNameMap["도끼"] = Skills.SkillType.Axes;
_skillNameMap["활"] = Skills.SkillType.Bows;
_skillNameMap["석궁"] = Skills.SkillType.Crossbows;
_skillNameMap["원소마법"] = Skills.SkillType.ElementalMagic;
_skillNameMap["피마법"] = Skills.SkillType.BloodMagic;
_skillNameMap["달리기"] = Skills.SkillType.Run;
_skillNameMap["점프"] = Skills.SkillType.Jump;
_skillNameMap["수영"] = Skills.SkillType.Swim;
_skillNameMap["잠행"] = Skills.SkillType.Sneak;
_skillNameMap["곡괭이"] = Skills.SkillType.Pickaxes;
_skillNameMap["벌목"] = Skills.SkillType.WoodCutting;
```

## 보너스 스킬 노드 목록

| 스킬 ID | 이름 | 대상 기술 | 보너스 |
|---------|------|----------|--------|
| `speed_ex1` | 수련자1 | 근접무기, 석궁 | +8 |
| `speed_ex2` | 수련자2 | 지팡이, 활 | +8 |
| `all_master` | 숙련자 | 달리기, 점프 | +8 |
| `agility_peak` | 점프 숙련자 | 점프 | +10 |
| `bow_Step3_speedshot` | 활 스피드샷 | 활 | +10 |

## 새 숙련도 보너스 추가 방법

### 1. Config 추가 (SkillTreeConfig.cs)
```csharp
public static ConfigEntry<float> NewSkill_Bonus;
NewSkill_Bonus = config.Bind("NewCategory", "NewSkill_Bonus", 5f, "설명");
```

### 2. GetSkillLevelBonus에 case 추가
```csharp
case Skills.SkillType.TargetSkill:
    if (manager.GetSkillLevel("new_skill_id") > 0)
        bonus += SkillTreeConfig.NewSkill_Bonus.Value;
    break;
```

### 3. 스킬 노드 ApplyEffect에서 텍스트 표시 (선택)
```csharp
ApplyEffect = (lv) => {
    var player = Player.m_localPlayer;
    if (player != null) {
        SkillEffect.ShowSkillEffectText(player,
            $"스킬 습득! 대상기술 +{bonusValue}",
            new Color(0.2f, 0.8f, 0.2f),
            SkillEffect.SkillEffectTextType.Critical);
    }
}
```

## 중복 호출 방지

```csharp
private static int _lastSetupFrame = -1;

public static void Postfix(...)
{
    int currentFrame = Time.frameCount;
    if (_lastSetupFrame == currentFrame) return;
    _lastSetupFrame = currentFrame;
    // ...
}
```

## 다중 텍스트 컴포넌트 처리

Valheim UI 요소에 여러 숫자 텍스트가 있을 수 있음:
```csharp
List<TMP_Text> levelTexts = new List<TMP_Text>();
// 모든 숫자 텍스트 수집

bool firstText = true;
foreach (var levelText in levelTexts)
{
    if (firstText)
    {
        levelText.text = formattedText;  // "0 +8"
        firstText = false;
    }
    else
    {
        levelText.text = "";  // 나머지 비움
    }
}
```

## 디버깅

### 로그 확인
```
[숙련도 UI] Swords: 0 +8 (원본총합: 8, 텍스트수: 1)
[숙련도 UI] Bows: 0 +10 (원본총합: 10, 텍스트수: 2)
```

### 일반적인 문제

| 증상 | 원인 | 해결 |
|------|------|------|
| 보너스 표시 안됨 | 한글 매핑 누락 | `_skillNameMap`에 추가 |
| 숫자 중복 표시 | 다중 텍스트 컴포넌트 | 나머지 텍스트 비우기 |
| 색상 안됨 | richText 미활성화 | `levelText.richText = true` |
| 중복 호출 | 같은 프레임 재호출 | `Time.frameCount` 체크 |

## 관련 파일

- `SkillTree/SkillEffect.SpeedTree2.cs` - 보너스 계산, GetSkillLevel 패치
- `SkillTree/SkillEffect.SkillUIDisplay.cs` - UI 표시 패치
- `SkillTree/SkillTreeConfig.cs` - Config 값 정의
- `SkillTree/RangedSkillData.cs` - 원거리 스킬 노드 (bow_Step3_speedshot)

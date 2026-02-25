# 스킬 툴팁 색상 표준화 가이드

## 개요
CaptainSkillTree 모드의 스킬 툴팁(Description) 표시 시 사용되는 색상 표준입니다.
직업, 액티브, 패시브 스킬 모두 동일한 색상 규칙을 적용합니다.

---

## 표준 색상 코드표

### 1. 기본 구성요소

| 구분 | 라벨 색상 | 값 색상 | 용도 |
|------|-----------|---------|------|
| 스킬명 | `#FFD700` | - | 툴팁 상단 스킬 이름 |
| 설명 | `#FFD700` | `#E0E0E0` | 스킬 설명 텍스트 |
| 범위 | `#87CEEB` | `#B0E0E6` | 스킬 영향 범위 |
| 소모 | `#FFB347` | `#FFDAB9` | 스태미나/Eitr 소모량 |
| 쿨타임 | `#FFA500` | `#FFDB58` | 스킬 재사용 대기시간 |
| 스킬유형 | `#DDA0DD` | `#E6E6FA` | 액티브/패시브 구분 |
| 필요조건 | `#98FB98` | `#00FF00` | 착용 무기/선행 스킬 |
| 확인사항 | `#F0E68C` | `#FFE4B5` | 추가 안내사항 |
| 필요포인트 | `#87CEEB` | `#FF6B6B` | 스킬 습득 비용 |

### 2. 특수 조건 표시

| 용도 | 색상 코드 | 예시 |
|------|-----------|------|
| 무기 착용 조건 | `#DDA0DD` | ※ 석궁 착용시 효과발동 |
| 확률 합산 정보 | `#FFD700` | ※ Lv1과 확률 합산 |
| 숙련도 유지 | `#FFD700` | ※ 사망해도 보너스 유지 |
| 폭발/범위 효과 | `#FF6B6B` | ※ 폭발 범위 피해 |

### 3. 직업별 특수 효과

| 직업 | 효과 라벨 | 효과 값 | 용도 |
|------|-----------|---------|------|
| Berserker | `#FF6B6B` | `#FF8C82` | 분노 효과 |
| Healer/Paladin | `#98FB98` | `#00FF00` | 힐링 효과 |
| 공통 | `#98FB98` | `#ADFF2F` | 패시브 효과 |

---

## 스킬 타입별 적용 가이드

### 패시브 스킬

```csharp
// Description 예시
Description = $"석궁 공격력 +{Config.DamageBonus}%\n" +
              $"<color=#DDA0DD><size=16>※ 석궁 착용시 효과발동</size></color>"

// 숙련도 스킬 예시
Description = $"활 기술(숙련도) +{Config.SkillBonus}\n" +
              $"<color=#FFD700><size=14>※ 사망해도 보너스 유지</size></color>\n" +
              $"<color=#DDA0DD><size=16>※ 활 착용시 효과발동</size></color>"

// 확률 합산 스킬 예시
Description = $"{Config.ChanceValue}% 확률로 추가 효과\n" +
              $"<color=#FFD700><size=16>※ Lv1과 확률 합산</size></color>\n" +
              $"<color=#DDA0DD><size=16>※ 무기 착용시 효과발동</size></color>"
```

### 액티브 스킬

```csharp
// 기본 구조
Description = $"T키: 스킬 발동 효과 설명\n" +
              $"소모: 스태미나 {Config.StaminaCost}%\n" +
              $"스킬유형: 액티브 스킬, T키\n" +
              $"무기타입: 활\n" +
              $"<color=#FF6B6B><size=16>※ 폭발 범위 피해</size></color>"

// 힐 스킬 예시
Description = $"G키로 {Config.Duration}초 동안 힐러모드 활성화\n" +
              $"아군 최대체력의 {Config.HealPercent}% 즉시 회복\n" +
              $"소모: Eitr {Config.EitrCost}\n" +
              $"스킬유형: 액티브 G키\n" +
              $"무기타입: 지팡이\n" +
              $"쿨타임: {Config.Cooldown}초"
```

### 직업 스킬 (Tooltip 클래스)

```csharp
// 표준 툴팁 구조
tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";
tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>{data.description}</size></color>\n";
tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>{data.range}</size></color>\n";
tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>{data.consume}</size></color>\n";
tooltip += $"<color=#DDA0DD><size=16>스킬유형: </size></color><color=#E6E6FA><size=16>{data.skillType}</size></color>\n";
tooltip += $"<color=#98FB98><size=16>패시브: </size></color><color=#ADFF2F><size=16>{data.passiveSkills}</size></color>\n";
tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
tooltip += $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>{data.requiredItem}</size></color>";
```

---

## 색상 의미 체계

### 색상 카테고리

| 색상 계열 | 의미 | 대표 색상 |
|-----------|------|-----------|
| 골드/노란색 | 중요 정보, 스킬명, 핵심 수치 | `#FFD700`, `#FFDB58` |
| 라벤더/보라 | 조건, 스킬유형, 특수 효과 | `#DDA0DD`, `#E6E6FA` |
| 하늘색/파란색 | 범위, 필요포인트, 정보 | `#87CEEB`, `#B0E0E6` |
| 주황색 | 소모, 쿨타임 | `#FFB347`, `#FFA500` |
| 초록색 | 필요조건, 패시브 효과, 힐링 | `#98FB98`, `#00FF00` |
| 빨간색 | 위험/중요 효과, 폭발, 비용 | `#FF6B6B`, `#FF0000` |
| 회색 | 일반 설명 텍스트 | `#E0E0E0` |

### 크기 가이드

| 요소 | 크기 | 용도 |
|------|------|------|
| 스킬명 | `<size=22>` | 툴팁 제목 |
| 일반 텍스트 | `<size=16>` | 설명, 수치, 조건 |
| 부가 정보 | `<size=14>` | 보조 설명 |

---

## 금지 색상 (더 이상 사용하지 않음)

| 색상 코드 | 대체 색상 | 사유 |
|-----------|-----------|------|
| `#A020F0` | `#DDA0DD` | 액티브/직업 스킬과 통일 |
| `#FF4500` | `#FF6B6B` | 밝은 빨강으로 가독성 향상 |

---

## ApplyEffect 습득 메시지 (이모지 규칙)

**참고**: 툴팁 Description이 아닌 ApplyEffect 습득 메시지에는 이모지 사용 가능

```csharp
// 습득 메시지 예시 (이모지 사용 가능)
SkillEffect.ShowSkillEffectText(player, "🏹 멀티샷 Lv1 습득!",
    new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);

SkillEffect.ShowSkillEffectText(player, "⚔️ 공격 전문가 습득!",
    new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
```

**이모지 카테고리:**
- 근접 무기: ⚔️, 🗡️, 🔨
- 원거리 무기: 🏹
- 마법: ✨, 💫
- 방어: 🛡️
- 속도: 🏃
- 생산: 🔨, ⛏️

---

## 관련 파일

### Description 색상 적용 파일
- `SkillTree/RangedSkillData.cs` - 원거리 스킬
- `SkillTree/MeleeSkillData.cs` - 근접 스킬
- `SkillTree/DefenseSkillData.cs` - 방어 스킬
- `SkillTree/*_Config.cs` - 각 무기별 설정

### 직업 Tooltip 클래스 파일
- `SkillTree/Archer_Tooltip.cs`
- `SkillTree/Berserker_Tooltip.cs`
- `SkillTree/HealerMode_Tooltip.cs`
- `SkillTree/Knife_Tooltip.cs`
- `SkillTree/Rogue_Tooltip.cs`
- `SkillTree/Tanker_Tooltip.cs`
- `SkillTree/Mage_Tooltip.cs`
- `SkillTree/Paladin_Tooltip.cs`

---

## 버전 이력

| 버전 | 날짜 | 변경사항 |
|------|------|----------|
| 1.0 | 2026-02-06 | 초기 표준 문서 작성 |
| - | - | RangedSkillData.cs 색상 통일 완료 |

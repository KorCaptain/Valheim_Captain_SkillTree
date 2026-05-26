# 액티브 스킬 Lv1~7 업그레이드 시스템

> 작성일: 2026-05-18
> 기반 구현: 폭발화살(bow_Step6_critboost) Lv1~7
> 참고 패턴: 아처(Archer) 직업 Lv1~5 레벨업 시스템 (job-skill-levelup.md)

---

## 개요

일반 액티브 스킬(MaxLevel=1)을 보스 트로피 소모 방식의 **Lv1~7 업그레이드 시스템**으로 확장하는 패턴.
직업(Job) 레벨업과 구조가 동일하나 직업 제한 없이 무기 스킬에 적용된다.

---

## 1. 폭발화살 레벨 시스템 스펙

### 레벨별 조건 및 데미지

| 레벨 | 데미지 (직격) | 범위 피해 (독립 수치) | 소모 트로피 (2종) |
|------|-------------|---------------------|----------------|
| Lv1 | 80% | **55%** | 에이크쉬르 + 멧돼지 |
| Lv2 | 100% | **70%** | 엘더 + 서리트롤 |
| Lv3 | 120% | **85%** | 본메스 + 어보미네이션 |
| Lv4 | 140% | **100%** | 모더 + 돌골렘 |
| Lv5 | 160% | **115%** | 야글루스 + 고블린샤먼 |
| Lv6 | 180% | **130%** | 시커여왕 + 지알 |
| Lv7 | 200% | **150%** | 페이더 + 타락한발키리 |

> ⚠️ 범위 피해는 직격의 고정 비율이 **아님** — 레벨별 독립 절대 수치 (기본 공격력 대비 %)

**직격 데미지 공식**: `기본데미지% + (레벨-1) × LevelBonus(20f)`
**범위 피해**: 레벨별 switch 테이블 (`BowExplosive_Tooltip.GetAreaDamageForLevel`)

### 공통 수치
- 활성화 키: R키
- 쿨타임: 40초
- 스태미나: 15 (플랫 고정값, % 아님)
- 폭발 반경: 7m
- 활성화 지속시간: 6초 이내 공격 시 발동
- 필요 스킬포인트: 3

---

## 2. 파일 구조 (partial class 방식)

```
CaptainSkillTree/
├── SkillTree/
│   ├── Bow_Config.cs                          ← Config 항목 (LevelBonus 포함)
│   ├── SkillEffect.ExplosiveArrow.cs          ← 실행 로직 (레벨별 데미지 계산)
│   ├── RangedSkillData.cs                     ← 노드 등록 (MaxLevel = 7)
│   ├── BowExplosive_Tooltip.cs                ← [신규] 레벨 연동 툴팁
│   └── ActiveSkills/
│       └── SkillTreeManager.BowExplosiveArrow.cs  ← [신규] 트로피 Has/GetMissing/Consume
├── Gui/
│   ├── SkillTreeUI.cs                         ← partial 선언, 분기 호출만
│   └── ActiveSkills/
│       └── SkillTreeUI.BowExplosiveArrow.cs   ← [신규] 다이얼로그 + 핸들러
└── SkillTree/
    └── SkillTreeManager.cs                    ← partial 선언, ConsumeExplosiveArrowLevelItems 호출
```

---

## 3. 코드 흐름

### 3-1. 투자 검증 (CanInvestWithMessage)

```
SkillTreeUI.cs → CanInvestWithMessage(node)
  └─ node.Id == "bow_Step6_critboost"          ← currentLevel >= 1 조건 없음 (Lv0→Lv1도 포함)
       └─ CheckExplosiveArrowInvest(node, currentLevel)  [SkillTreeUI.BowExplosiveArrow.cs]
            ├─ targetLevel = currentLevel + 1
            ├─ targetLevel > 7 → "최대 레벨" 반환
            ├─ isAdmin → 어드민이면 트로피 검사 스킵 (의도된 동작)
            └─ !HasExplosiveArrowLevelItems(targetLevel) → 부족 메시지 반환
```

### 3-2. 클릭 핸들러

```
SkillTreeUI.cs → InvestPoint(node) → 클릭 이벤트
  └─ HandleExplosiveArrowClick(node)  [SkillTreeUI.BowExplosiveArrow.cs]
       ├─ node.Id != "bow_Step6_critboost" → return false  ← ★ 반드시 최상단에 위치
       ├─ targetLevel > 7 → ShowWarning("최대 레벨"), return true
       ├─ 포인트 부족 → ShowWarning, return true
       ├─ 트로피 보유 → ShowExplosiveArrowUpgradeConfirmDialog(targetLevel, nodeRect)
       └─ 트로피 부족 → ShowWarning("트로피 필요"), return true
```

### 3-3. 확인 다이얼로그 → 투자 확정

```
확인 버튼 클릭 [SkillTreeUI.BowExplosiveArrow.cs]
  └─ mgr.AddPendingInvestment("bow_Step6_critboost")
  └─ mgr.ConfirmInvestments()  [SkillTreeManager.cs]
       └─ pending.Key == "bow_Step6_critboost"  ← currentLevel >= 1 조건 없음 (Lv0→Lv1도 포함)
            └─ ConsumeExplosiveArrowLevelItems(targetLevel)  [SkillTreeManager.BowExplosiveArrow.cs]
                 └─ inventory.RemoveItem("$item_trophy_xxx", 1) × 2종
```

---

## 4. 핵심 구현 포인트

### 4-1. MaxLevel 변경

```csharp
// RangedSkillData.cs
manager.AddSkill(new SkillNode {
    Id = "bow_Step6_critboost",
    MaxLevel = 7,   // ← 반드시 7로 설정 (기본 1이면 Lv2 투자 불가)
    ...
});
```

### 4-2. SkillTreeManager partial 선언

```csharp
// SkillTreeManager.cs (line 31)
public partial class SkillTreeManager  // ← partial 필수
```

```csharp
// SkillTreeManager.cs — ConfirmInvestments() 내부
else if (pending.Key == "bow_Step6_critboost")  // ← currentLevel >= 1 조건 없음
{
    int targetLevel = currentLevel + 1;
    ConsumeExplosiveArrowLevelItems(targetLevel);
}
```

### 4-3. SkillTreeUI partial 선언

```csharp
// SkillTreeUI.cs (line 31)
public partial class SkillTreeUI : MonoBehaviour  // ← partial 필수
```

```csharp
// SkillTreeUI.cs — CanInvestWithMessage() 내부
else if (node.Id == "bow_Step6_critboost")  // ← currentLevel >= 1 조건 없음
{
    return CheckExplosiveArrowInvest(node, currentLevel);
}

// SkillTreeUI.cs — InvestPoint() 내부 (node.Id 조건 없이 호출)
if (HandleExplosiveArrowClick(node)) return;
```

### 4-4. HandleXxxClick — node.Id 체크 필수

```csharp
// SkillTreeUI.BowExplosiveArrow.cs
private bool HandleExplosiveArrowClick(SkillTree.SkillNode node)
{
    // ★ 반드시 첫 줄에 node.Id 체크
    if (node.Id != "bow_Step6_critboost") return false;

    var manager = SkillTree.SkillTreeManager.Instance;
    int skillLevel = manager.GetSkillLevel("bow_Step6_critboost");
    int targetLevel = skillLevel + 1;
    ...
    return true;
}
```

> ⚠️ `HandleXxxClick` 함수는 `SkillTreeUI.cs`에서 **node.Id 검증 없이** 모든 노드 클릭에 대해 호출됨.
> 함수 내부에서 반드시 `if (node.Id != "xxx") return false;` 를 첫 줄에 추가해야 한다.
> 이 체크가 없으면 다른 스킬 노드 클릭 시 해당 스킬의 업그레이드 다이얼로그가 뜨는 버그 발생.

### 4-5. 스태미나 플랫 처리

```csharp
// SkillEffect.ExplosiveArrow.cs — 플랫 값으로 처리 (% 아님)
float requiredStamina = SkillTreeConfig.BowExplosiveArrowStaminaCostValue;
// ❌ 잘못된 예: maxStamina * (cost / 100f)
```

### 4-6. 레벨별 데미지 계산

```csharp
// SkillEffect.ExplosiveArrow.cs — 직격 데미지
int explosiveLevel = SkillTreeManager.Instance?.GetSkillLevel("bow_Step6_critboost") ?? 1;
float levelBonus = (explosiveLevel - 1) * Bow_Config.BowExplosiveArrowLevelBonusValue;
float explosiveDamage = totalBaseDamage * ((SkillTreeConfig.BowExplosiveArrowDamageValue + levelBonus) / 100f);

// 범위 피해 — 레벨별 switch 테이블 (직격 비율 아님)
int explosiveLevelArea = SkillTreeManager.Instance?.GetSkillLevel("bow_Step6_critboost") ?? 1;
float areaPercent = explosiveLevelArea switch {
    2 => 70f, 3 => 85f, 4 => 100f,
    5 => 115f, 6 => 130f, 7 => 150f,
    _ => 55f   // Lv1 및 기본값
};
float areaDamage = totalBaseDamage * (areaPercent / 100f);
```

---

## 5. Bow_Config.cs — 필요 Config 항목

```csharp
// ConfigEntry 선언
public static ConfigEntry<float> BowExplosiveArrowDamage;      // 기본 데미지% (80f)
public static ConfigEntry<float> BowExplosiveArrowCooldown;    // 쿨타임 (40f)
public static ConfigEntry<float> BowExplosiveArrowStaminaCost; // 스태미나 (15f, 플랫)
public static ConfigEntry<float> BowExplosiveArrowRadius;      // 반경 (7f)
public static ConfigEntry<float> BowExplosiveArrowLevelBonus;  // 레벨당 보너스% (20f)

// BindServerSync (Initialize 내부)
BowExplosiveArrowLevelBonus = SkillTreeConfig.BindServerSync<float>(
    "bow_Step6_explosive_level_bonus", 20f, GetConfigDescription("key"));

// 프로퍼티
public static float BowExplosiveArrowLevelBonusValue =>
    SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_level_bonus", BowExplosiveArrowLevelBonus?.Value ?? 20f);
```

---

## 6. 툴팁 시스템 — BowExplosive_Tooltip.cs

아처(Archer_Tooltip.cs)와 동일한 패턴:

```
파일: SkillTree/BowExplosive_Tooltip.cs
클래스: public static class BowExplosive_Tooltip

메서드:
  GetTooltip()                         — 메인 진입점 (RangedSkillData에서 호출)
  GetDamageForLevel(int level)         — 80 + (lv-1) × LevelBonus
  GetAreaDamageForLevel(int level)     — 레벨별 switch 테이블 (55/70/85/100/115/130/150)
  GetLevelItemText(int targetLevel)    — 트로피 조합 텍스트
```

```csharp
// BowExplosive_Tooltip.cs — GetAreaDamageForLevel
private static int GetAreaDamageForLevel(int level) => level switch
{
    2 => 70, 3 => 85, 4 => 100,
    5 => 115, 6 => 130, 7 => 150,
    _ => 55   // Lv1 및 기본값
};
```

### 툴팁 출력 구조

```
🏹 폭발 화살                              [황금색 size=22]

Lv{현재} : 데미지 {X}% · 범위 {Y}%        [회색 size=16]
소모: 스태미나 {N}                         [주황색]
스킬 유형: 액티브 - R키                   [보라/황금]
쿨타임: {N}초                             [주황/노랑]
폭발 반경: {N}m                           [하늘색]
필요조건: 활 착용                          [초록]

(currentLevel < 7)
  Lv{next} 강화 필요: {트로피A} x1 + {트로피B} x1  [주황/빨강]
(currentLevel == 7)
  최대 레벨                               [황금색]

────────────────────────────────────       [회색 구분선]
(mainLevel < 7인 경우 레벨 프리뷰)
Lv2 : 데미지 100% · 범위 70%             [회색 size=14]
Lv3 : 데미지 120% · 범위 85%
...
```

### RangedSkillData.cs에서 호출

```csharp
public static string GetExplosiveArrowTooltip()
{
    return BowExplosive_Tooltip.GetTooltip();
}
```

---

## 7. 다국어 처리 — 7개 언어 체크리스트

### 신규 추가된 키 목록

| 키 | 용도 |
|----|------|
| `explosive_arrow_upgrade_title` | 다이얼로그 제목 |
| `explosive_arrow_upgrade_confirm` | 확인 메시지 ({0}=레벨) |
| `explosive_arrow_max_level` | 최대 레벨 알림 |
| `explosive_arrow_level_item_required` | 트로피 부족 메시지 |
| `explosive_arrow_missing_items` | 부족 아이템 목록 |
| `explosive_arrow_upgrade_requires` | 툴팁 강화 필요 라벨 |
| `explosive_arrow_damage_preview` | 툴팁 데미지 표시 ({0}=직격%, {1}=범위%) |
| `item_trophy_eikthyr` | 에이크쉬르 트로피 이름 |
| `item_trophy_boar` | 멧돼지 트로피 이름 |
| `item_trophy_sgolem` | 돌 골렘 트로피 이름 |
| `item_trophy_goblinshaman` | 고블린 샤먼 트로피 이름 |
| `item_trophy_gjall` | 지알 트로피 이름 |
| `item_trophy_fader` | 페이더 트로피 이름 |
| `item_trophy_fallenvalkyrie` | 타락한 발키리 트로피 이름 |

### 언어별 파일 위치

| 언어 | 파일 | 방식 |
|------|------|------|
| KO | `Localization/DefaultLanguages_WeaponSkills.cs` → `GetKorean_WeaponSkills()` | C# 딕셔너리 |
| EN | `Localization/DefaultLanguages_WeaponSkills.cs` → `GetEnglish_WeaponSkills()` | C# 딕셔너리 |
| DE | `Localization/de.json` | 임베디드 JSON |
| RU | `Localization/ru.json` | 임베디드 JSON |
| ZH-CN | `Localization/zh-cn.json` | 임베디드 JSON |
| JA | `Localization/ja.json` | 임베디드 JSON |
| PT-BR | `Localization/pt_BR.json` | 임베디드 JSON |

### JSON 파일 추가 규칙

```json
// 마지막 기존 항목에 콤마 추가 후 새 키 삽입
{
  ...
  "se_ice_breath_slow_tooltip": "Move Speed -50%",  // ← 콤마 추가
  "explosive_arrow_upgrade_title": "🏹 Explosive Arrow Upgrade",
  "explosive_arrow_damage_preview": "Damage {0}% · AoE {1}%"
}
// 마지막 항목에는 콤마 없음
```

### 폭발화살 키 번역 참고

| 키 | KO | EN | DE | RU | ZH-CN | JA | PT-BR |
|----|----|----|----|----|-------|----|-------|
| `explosive_arrow_upgrade_title` | 🏹 폭발 화살 강화 | 🏹 Explosive Arrow Upgrade | 🏹 Explosivpfeil Aufwertung | 🏹 Улучшение взрывной стрелы | 🏹 爆炸箭升级 | 🏹 爆発矢強化 | 🏹 Aprimoramento da Flecha Explosiva |
| `explosive_arrow_damage_preview` | 데미지 {0}% · 범위 {1}% | Damage {0}% · AoE {1}% | Schaden {0}% · Fläche {1}% | Урон {0}% · Область {1}% | 伤害 {0}% · 范围 {1}% | ダメージ {0}% · 範囲 {1}% | Dano {0}% · AoE {1}% |
| `item_trophy_eikthyr` | 에이크쉬르 트로피 | Eikthyr Trophy | Eikthyr-Trophäe | Трофей Эйктюра | 艾克西尔战利品 | エイクシルのトロフィー | Troféu de Eikthyr |
| `item_trophy_boar` | 멧돼지 트로피 | Boar Trophy | Wildschwein-Trophäe | Трофей кабана | 野猪战利品 | イノシシのトロフィー | Troféu de Javali |
| `item_trophy_fader` | 페이더 트로피 | Fader Trophy | Fader-Trophäe | Трофей Фейдера | 费德战利品 | フェイダーのトロフィー | Troféu de Fader |
| `item_trophy_fallenvalkyrie` | 타락한 발키리 트로피 | Fallen Valkyrie Trophy | Gefallene-Walküre-Trophäe | Трофей падшей валькирии | 堕落女武神战利品 | 堕ちたワルキューレのトロフィー | Troféu de Valquíria Caída |

---

## 8. 흔한 버그 패턴 및 방지법

### ❌ Bug A: HandleXxxClick에 node.Id 체크 누락

```csharp
// 잘못된 예 — node.Id 체크 없음
private bool HandleExplosiveArrowClick(SkillTree.SkillNode node)
{
    var manager = ...;
    int skillLevel = manager.GetSkillLevel("bow_Step6_critboost");  // 항상 고정 스킬 조회
    ...
    return true;  // 항상 true → 다른 노드 클릭도 차단
}
```

**증상**: 다른 스킬 클릭 시 폭발화살 업그레이드 다이얼로그가 뜸.

```csharp
// 올바른 예
private bool HandleExplosiveArrowClick(SkillTree.SkillNode node)
{
    if (node.Id != "bow_Step6_critboost") return false;  // ← 첫 줄에 위치
    ...
}
```

---

### ❌ Bug B: CanInvestWithMessage/ConfirmInvestments에 currentLevel >= 1 조건

```csharp
// 잘못된 예 — Lv0→Lv1(첫 학습) 제외
else if (node.Id == "bow_Step6_critboost" && currentLevel >= 1)
```

**증상**: Lv1 첫 학습 시 트로피 없이도 학습 가능.

```csharp
// 올바른 예
else if (node.Id == "bow_Step6_critboost")
```

---

### ❌ Bug C: 범위 피해를 직격 비율로 계산

```csharp
// 잘못된 예 — 직격 × 고정 70%
float areaDamage = totalBaseDamage * ((BowExplosiveArrowDamageValue + levelBonusArea) / 100f) * 0.7f;
```

**증상**: 레벨 올려도 범위 피해 비율이 항상 동일 (직격의 70%).

```csharp
// 올바른 예 — 레벨별 독립 수치
float areaPercent = explosiveLevelArea switch {
    2 => 70f, 3 => 85f, 4 => 100f,
    5 => 115f, 6 => 130f, 7 => 150f,
    _ => 55f
};
float areaDamage = totalBaseDamage * (areaPercent / 100f);
```

---

### ❌ Bug D: CheckXxxInvest에 선행 스킬(Prerequisite) 체크 누락

```csharp
// 잘못된 예 — 선행 스킬 없이도 Lv1 첫 학습 가능
private InvestResult CheckExplosiveArrowInvest(SkillTree.SkillNode node, int currentLevel)
{
    int targetLevel = currentLevel + 1;  // ← 선행 스킬 체크 없이 바로 targetLevel 계산
    ...
}
```

**증상**: `SkillNode.Prerequisites`가 UI 잠금(회색)은 해제하지만 **투자 검증에는 자동 적용되지 않음**.
선행 스킬을 배우지 않아도 Lv1 첫 학습(currentLevel == 0 → 1)이 가능해지는 버그.

```csharp
// 올바른 예 — currentLevel == 0 시 선행 스킬 체크
private InvestResult CheckExplosiveArrowInvest(SkillTree.SkillNode node, int currentLevel)
{
    if (currentLevel == 0 && SkillTree.SkillTreeManager.Instance.GetSkillLevel("PREREQ_SKILL_ID") <= 0)
        return new InvestResult(false, L10n.Get("SKILL_prereq_required"));
    int targetLevel = currentLevel + 1;
    ...
}
```

> ⚠️ `currentLevel == 0` 조건만 체크. Lv2 이상 업그레이드 시에는 이미 Lv1을 획득한 상태이므로 선행 조건은 충족됨.
> `SkillNode.Prerequisites`는 **UI 잠금** 용도일 뿐 — 투자 검증(`CheckXxxInvest`)에서 **별도로 수동 체크** 필요.

---

## 9. 신규 액티브 스킬에 적용 시 체크리스트

새 무기 스킬을 Lv1~7 업그레이드 시스템으로 만들 때 참고:

### 필수 작업 목록

- [ ] `SkillTreeManager.cs` — `partial` 키워드 확인
- [ ] `SkillTreeUI.cs` — `partial` 키워드 확인
- [ ] `SkillTree/ActiveSkills/{스킬}Manager.cs` — 신규 생성 (Has/GetMissing/Consume)
- [ ] `Gui/ActiveSkills/{스킬}UI.cs` — 신규 생성 (CheckInvest/HandleClick/Dialog)
  - [ ] **HandleXxxClick 첫 줄**: `if (node.Id != "xxx") return false;` 추가
  - [ ] **CheckXxxInvest**: `currentLevel == 0` 시 선행 스킬(`PREREQ_ID`) 체크 추가 (Bug D 방지)
- [ ] `SkillTreeManager.cs ConfirmInvestments()` — 스킬 ID 분기 추가 (`currentLevel >= 1` 없이)
- [ ] `SkillTreeUI.cs CanInvestWithMessage()` — 스킬 ID 분기 추가 (`currentLevel >= 1` 없이)
- [ ] `SkillTreeUI.cs InvestPoint()` — HandleXxxClick() 호출 추가
- [ ] `*_Config.cs` — LevelBonus ConfigEntry + BindServerSync + 프로퍼티 추가
- [ ] `*SkillData.cs` — MaxLevel = N, GetXxxTooltip() 수정
- [ ] `Xxx_Tooltip.cs` — 신규 생성 (BowExplosive_Tooltip.cs 참고)
  - [ ] `GetAreaDamageForLevel` — 레벨별 switch 테이블, 직격 비율 아님
- [ ] `SkillEffect.Xxx.cs` — 스태미나 플랫 처리, 레벨별 switch로 범위 피해 계산
- [ ] `DefaultLanguages_WeaponSkills.cs` — KO + EN 키 추가
- [ ] `de.json`, `ru.json`, `zh-cn.json`, `ja.json`, `pt_BR.json` — 5개 JSON 번역 추가
- [ ] `dotnet build` — 빌드 확인

### 레벨업 조건 설계 가이드

- **트로피 소모**: 2종 조합 권장 (보스 1종 + 바이옴 몬스터 1종)
- **데미지 스케일**: 레벨당 균등 % 증가 (예: +20%/레벨)
- **범위 피해**: 레벨별 독립 수치 설계 (직격 비율로 자동 계산 금지)
- **스태미나**: 플랫 수치 권장 (% 방식은 캐릭터 최대 스태미나에 의존하므로 불균형)
- **Lv1 조건**: 첫 진입장벽 (예: 1바이옴 보스 + 평범한 몬스터)
- **Lv7 조건**: 최고 난이도 보스 (Fader 계열)

---

## 10. 관련 파일 참고

| 파일 | 내용 |
|------|------|
| `md/job-skill-levelup.md` | 아처 직업 레벨업 시스템 (원형 패턴) |
| `md/active-skill-system.md` | 액티브 스킬 키 바인딩 규칙 |
| `md/multilanguage-guide.md` | 7개 언어 시스템 전체 구조 |
| `md/config-guide.md` | BindServerSync, GetEffectiveValue 사용법 |
| `md/tooltip-color-standard.md` | 툴팁 색상 코드 규칙 |
| `SkillTree/BowExplosive_Tooltip.cs` | 폭발화살 툴팁 구현 (참고용) |
| `SkillTree/Archer_Tooltip.cs` | 아처 툴팁 구현 (원형 패턴) |

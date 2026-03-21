# MULTILANGUAGE_GUIDE.md - CaptainSkillTree 다국어 시스템 가이드

> 최종 업데이트: 2026-03-20 (JP 완전 지원 추가)

---

## 지원 언어 목록

| 코드 | 언어 | 상태 |
|------|------|------|
| `ko` | 한국어 | ✅ 완전 지원 (기준 언어) |
| `en` | 영어 | ✅ 완전 지원 (fallback 언어) |
| `de` | 독일어 | ✅ 완전 지원 (Phase 2) |
| `ru` | 러시아어 | ✅ 완전 지원 |
| `pt_BR` | 포르투갈어 (브라질) | ✅ 완전 지원 |
| `zh-cn` | 중국어 간체 | ✅ 완전 지원 |
| `ja` | 일본어 | ✅ 완전 지원 |

---

## 1. Config 언어 설정

### AcceptableValueList 순서
`SkillTree/SkillTreeConfig.cs`에서 F1 Config Manager 드롭다운 순서를 정의:
```csharp
new AcceptableValueList<string>("Auto", "KR", "EN", "DE", "RU", "PT_BR", "CN","JA")
```

### DetectConfigLanguage() 우선순위 (3단계)

**우선순위 1: 수동 Config 설정** (F1에서 직접 선택)
```csharp
string result = (configLang == "ko" || configLang == "kr") ? "ko"
              : (configLang == "de") ? "de"
              : (configLang == "ru") ? "ru"
              : (configLang == "pt_br" || configLang == "pt") ? "pt_BR"
              : "en";
```

**우선순위 2: Valheim 게임 언어 자동 감지**
```csharp
string result = (langLow == "korean") ? "ko"
              : (langLow.Contains("german") || langLow == "deutsch") ? "de"
              : (langLow == "russian") ? "ru"
              : (langLow == "portuguese_brazilian") ? "pt_BR"
              : "en";
```

**우선순위 3: LocalizationManager 감지값**
```csharp
return (currentLang == "ko") ? "ko"
     : (currentLang == "de") ? "de"
     : (currentLang == "ru") ? "ru"
     : (currentLang == "pt_BR") ? "pt_BR"
     : "en";
```

---

## 2. 스킬트리 UI 번역 (DefaultLanguages*.cs)

### 파일 구조
| 파일 | 담당 키 |
|------|---------|
| `DefaultLanguages.cs` | 루트 진입점, `GetKorean()` / `GetEnglish()` 통합 |
| `DefaultLanguages_AttackProduction.cs` | 공격/생산 트리 키 |
| `DefaultLanguages_WeaponSkills.cs` | 무기별 스킬 키 |
| `DefaultLanguages_JobExpert.cs` | 직업/전문가 스킬 키 |
| `DefaultLanguages_ItemEffects.cs` | 아이템 효과 툴팁 키 |
| `DefaultLanguages_GameMessages.cs` | 게임 시스템 메시지 키 |

### 키 추가 절차
1. `DefaultLanguages_GameMessages.cs` (또는 해당 분류 파일)에 KO + EN 동시 추가
2. `Localization/ru.json` 에 동일 키 추가 (번역 없으면 EN 원문 사용)
3. `Localization/de.json` 에 동일 키 추가 (번역 없으면 EN 원문 사용)
4. `Localization/pt_BR.json` 에 동일 키 추가 (번역 없으면 EN 원문 사용)

```csharp
// DefaultLanguages_GameMessages.cs 예시
{ "level_decrease_msg", "레벨이 감소했습니다." },  // KO
{ "level_decrease_msg", "Level has decreased." },   // EN
```

---

## 3. 툴팁 번역 + FormatException 주의사항

### {N} 플레이스홀더 규칙 (⚠️ 중요)
**모든 언어 파일에서 `{N}` 번호가 EN과 동일해야 한다.**

```json
// EN (기준)
"spear_desc_expert": "Spear damage +{0}%, speed +{1}%"

// ❌ 잘못된 DE (플레이스홀더 수 불일치 → FormatException)
"spear_desc_expert": "Speer-Schaden +{0}%, Geschwindigkeit +{1}%, Kraft +{2}%"

// ✅ 올바른 DE
"spear_desc_expert": "Speer-Schaden +{0}%, Geschwindigkeit +{1}%"
```

**발생 증상**: 게임 실행 시 `FormatException: Index (zero based) must be greater...` 오류

### 툴팁 색상 포맷
```csharp
// L.Get()으로 번역 후 string.Format으로 값 삽입
var text = string.Format(L.Get("skill_key"), value1, value2);
```

---

## 4. 스킬 사용 메시지 / 게임 접속 메시지

### 하드코딩 금지 (L.Get() 필수)
```csharp
// ❌ 금지
Player.m_localPlayer.Message(MessageHud.MessageType.Center, "레벨이 감소했습니다.");

// ✅ 올바름
Player.m_localPlayer.Message(MessageHud.MessageType.Center, L.Get("level_decrease_msg"));
```

**관련 파일**: `SkillTree/LevelSyncManager.cs`, 액티브 스킬 발동 코드

---

## 5. 아이템 호버 툴팁 (Attack_Tooltip, ArmorTooltip)

**관련 파일**: `SkillTree/Attack_Tooltip_Display.cs`, `SkillTree/ArmorTooltip.cs`

- 아이템 효과 텍스트도 `L.Get()` 사용
- 키는 `DefaultLanguages_ItemEffects.cs`에 등록

---

## 6. Translation 폴더 내보내기 (게임 실행 시 자동 생성)

### 생성 위치
```
BepInEx\config\CaptainSkillTree\Translation\
├── en.json      ← DefaultLanguages.GetEnglish() 기준 최신본 (항상 덮어씀)
├── ru.json      ← EN 키 전체 + RU 번역값 병합
├── pt_BR.json   ← EN 키 전체 + PT_BR 번역값 병합
├── de.json      ← EN 키 전체 + DE 번역값 병합
├── ja.json      ← EN 키 전체 + JA번역값 병합
└── zh-cn.json   ← EN 키 전체 + ZH-CN 번역값 병합
```

### 동작 원칙
- **게임 시작 시마다 자동 덮어씌우기** → 항상 최신 키 목록 유지
- **미번역 신규 키**: EN 원문이 fallback으로 들어감
- **목적**: 유저가 번역본 수정 후 개발자에게 전달 가능

### 관련 코드 (`LocalizationManager.cs`)
```csharp
private static void ExportTranslationTemplates()
{
    var translationPath = Path.Combine(
        BepInEx.Paths.ConfigPath, "CaptainSkillTree", "Translation");

    // en.json (기준)
    var enData = DefaultLanguages.GetEnglish();
    File.WriteAllText(enPath, DictToJson(enData), UTF8);

    // de.json (EN 기준 + DE 번역 병합)
    var deData = new Dictionary<string, string>(enData);
    var deTranslations = LoadFromEmbeddedResource("de") ?? _translations["de"];
    foreach (var kvp in deTranslations) deData[kvp.Key] = kvp.Value;
    File.WriteAllText(dePath, DictToJson(deData), UTF8);
    // ru.json, pt_BR.json도 동일 패턴
}
```

---

## 7. 새 언어 추가 체크리스트 (7개 파일)

새 언어 `xx`를 추가할 때 **반드시 7개 파일 동시 수정**:

| # | 파일 | 작업 |
|---|------|------|
| 1 | `SkillTree/SkillTreeConfig.cs` | `AcceptableValueList`에 "XX" 추가 |
| 2 | `SkillTree/SkillTreeConfig.cs` | `DetectConfigLanguage()` 3곳에 분기 추가 |
| 3 | `SkillTree/SkillTreeConfig.cs` | `GetLocalizedDescription()`, `GetLocalizedKeyName()` XX 분기 추가 |
| 4 | `Localization/LocalizationManager.cs` | `_supportedLanguages`에 "XX" 추가 |
| 5 | `Localization/LocalizationManager.cs` | `ExportTranslationTemplates()`에 xx.json 블록 추가 |
| 6 | `Localization/ConfigTranslations.cs` | 3개 라우팅 메서드에 XX 분기 + GetXXCategories/Descriptions/KeyNames() 추가 |
| 7 | `Captain_SkillTree.csproj` | `xx.json` EmbeddedResource 등록 |
| + | `Localization/xx.json` | 번역 파일 생성 |

---

## 8. 오늘 발견된 버그 (2026-03-16, Phase 2 DE)

### 버그 1: de.json EmbeddedResource 미등록
- **증상**: `LoadFromEmbeddedResource("de")` 항상 null 반환 → 독일어 번역 무효
- **원인**: `Captain_SkillTree.csproj`에 de.json이 EmbeddedResource로 등록 안 됨
- **수정**: csproj에 `<EmbeddedResource Include="Localization\de.json" />` 추가
- **교훈**: 새 json 파일 추가 시 **반드시 csproj에도 등록**

### 버그 2: LoadLanguageFiles merge가 번역을 덮어씌움
- **증상**: LocalizationManager가 파일을 merge할 때 `ko/en` 베이스 번역이 외부 json으로 덮어씌워짐
- **원인**: merge 로직에서 overwrite 조건 미분리
- **수정**: `ko`/`en`만 overwrite 허용, 나머지는 기존값 유지

### 버그 3: {N} 플레이스홀더 수 불일치
- **증상**: `FormatException` 게임 크래시
- **원인**: `spear_desc_expert` 등에서 de.json이 EN보다 `{}` 플레이스홀더 적게/많게 포함
- **수정**: de.json 해당 키의 `{2}` → `{1}` 수정
- **교훈**: 번역 시 `{}` 개수·번호는 EN과 반드시 일치

### 버그 4: ru.json 미번역 키 (영어 fallback)
- **증상**: 러시아어 클라이언트에서 일부 키가 영어로 표시
- **원인**: `mmo_diff_notification` 등 키가 ru.json에 영어 원문으로 들어있었음
- **수정**: 해당 키 러시아어 번역으로 교체

### 버그 5: 하드코딩 한국어 문자열
- **증상**: 독일어/영어 플레이어도 한국어 메시지 표시
- **위치**: `LevelSyncManager.cs` 등 게임 메시지 출력 코드
- **수정**: `"레벨이 감소했습니다."` → `L.Get("level_decrease_msg")` 교체
- **교훈**: 유저 노출 텍스트는 **항상 `L.Get()`** 사용

---

## 9. 번역 파일 분리 원칙

| 파일 | 용도 | 대상 |
|------|------|------|
| `Localization/DefaultLanguages.cs` | 스킬트리 UI 전용 | 노드명, 툴팁, 버프 표시 |
| `Localization/ConfigTranslations.cs` | F1 Config Manager 전용 | 카테고리, 설정 설명 |

> ❌ 혼용 금지: DefaultLanguages.cs에 Config 키 추가하거나 반대로 하면 안 됨

---

## 10. 키 명명 규칙

```
{category}_{subcategory}_{property}
예: mace_desc_fury_attack, rogue_passive_desc
```

### 필수 규칙
1. **모든 언어 블록에 동시 추가**: KO, EN 블록 모두 필수
2. **수치 포맷 동기화**: `{0}`, `{1}` 개수·순서가 모든 언어에서 동일해야 함
3. **키 이름 변경 시**: 기존 키 삭제 후 새 키를 전 언어에 추가

```csharp
// ✅ 올바름 - 모든 언어에 동일 키
// Korean 블록
["rogue_passive_desc"] = "공격 속도 +{0}%, 스태미나 사용 -{1}%",

// English 블록
["rogue_passive_desc"] = "Attack speed +{0}%, Stamina use -{1}%",

// ❌ 금지 - 한국어만 수정, 영어 누락 → 런타임 경고 발생
```

---

## 11. 키 누락 방지 워크플로우

### 문제 증상
```
[Warning] [Localization] ✗ Key not found in any language: 'knife_desc_attack_evasion'
```

### 올바른 순서
```
1. DefaultLanguages.cs에 KO + EN 키 먼저 추가
2. 코드에서 L.Get("key") 사용
3. 빌드 전 검증 스크립트 실행
4. 빌드 (경고 0개 확인)
```

```csharp
// ❌ 잘못된 순서 - 코드 먼저 작성 → 경고 발생
L.Get("new_skill_name")

// ✅ 올바른 순서 - 키 먼저 등록 후 사용
// 1. DefaultLanguages.cs
["new_skill_name"] = "새 스킬"   // KO 블록
["new_skill_name"] = "New Skill" // EN 블록
// 2. 코드에서 사용
L.Get("new_skill_name")
```

---

## 12. 검증 스크립트 상세

```bash
# 빌드 전 필수 실행
cd C:/home/ssunyme/.npm-global/bin/CaptainSkillTree/scripts
powershell -ExecutionPolicy Bypass -File validate_loc_keys.ps1
```

**스크립트 기능:**
- `.cs` 파일에서 `L.Get("key")` 패턴 추출
- DefaultLanguages.cs의 KO/EN 블록 키 추출
- 누락된 키, 언어별 불일치 리포트

**출력 예시:**
```
MISSING KEYS (2):
  - 'knife_desc_attack_evasion'
      Used in: SkillTree\Knife_Tooltip.cs
  - 'bow_penetration_desc'
      Used in: SkillTree\RangedSkillData.cs
```

**실행 시점:**
- 새 스킬/효과 추가 후
- 키 이름 변경 후
- 빌드 전 (필수)
- 커밋 전 (필수)

---

## 13. Config Manager 번역 형식 (ConfigTranslations.cs)

### 추가 순서
1. **ConfigTranslations.cs에 먼저 번역 추가** (KO + EN)
2. **Config 파일에서 GetConfigDescription() 호출**

```csharp
// ConfigTranslations.cs - 한국어
["Tier0_DefenseExpert_HPBonus"] =
    "【체력 보너스】\n" +
    "방어 전문가 스킬의 체력 증가 보너스입니다.\n" +
    "권장값: 5-10";

// ConfigTranslations.cs - 영어
["Tier0_DefenseExpert_HPBonus"] =
    "【Health Bonus】\n" +
    "Health increase bonus from Defense Expert skill.\n" +
    "Recommended: 5-10";
```

### 3종 세트 상세 (Rule 13)

| 항목 | 파일 | 내용 |
|------|------|------|
| **① 2차 항목 표시명 (DispName)** | `ConfigTranslations.cs` → `GetKoreanKeyNames()` + `GetEnglishKeyNames()` | F1 Config Manager에서 키 이름 표시 |
| **② 마우스오버 세부설명 (Description)** | `ConfigTranslations.cs` → `GetDescriptionTranslations()` (KO + EN) | 마우스오버 시 나타나는 상세 설명 |
| **③ GetConfigDescription() 호출** | `*_Config.cs` → `BindServerSync()` description 파라미터 | 하드코딩 문자열 대신 반드시 사용 |

> 상세 내용은 `md/CONFIG_GUIDE.md` 참조

---

## 14. 전체 체크리스트

### 스킬/효과 추가·수정 시
- [ ] DefaultLanguages.cs - KO 블록에 키 추가
- [ ] DefaultLanguages.cs - EN 블록에 키 추가
- [ ] 수치 포맷(`{0}`, `{1}`) 모든 언어 동기화
- [ ] `ru.json` 동기화 (번역 없으면 EN 원문 사용)
- [ ] `validate_loc_keys.ps1` 실행 (누락 키 0개)
- [ ] 빌드 테스트 (경고 0개)

### 새 Config 추가 시
- [ ] ConfigTranslations.cs - KO Description 추가 (【】 형식)
- [ ] ConfigTranslations.cs - EN Description 추가 (【】 형식)
- [ ] ConfigTranslations.cs - KO/EN KeyName 추가
- [ ] Config 파일 - GetConfigDescription() 사용
- [ ] F1 메뉴 번역 표시 확인

---

## 참조

- `md/CONFIG_GUIDE.md` - Config 키 규칙, 번역 체계
- `Localization/LocalizationManager.cs` - 번역 로드/내보내기 핵심 로직
- `Localization/ConfigTranslations.cs` - F1 Config Manager 번역 진입점

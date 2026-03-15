# MULTILANGUAGE_GUIDE.md - CaptainSkillTree 다국어 시스템 가이드

> 최종 업데이트: 2026-03-16 (DE 추가 Phase 2 완료)

---

## 지원 언어 목록

| 코드 | 언어 | 상태 |
|------|------|------|
| `ko` | 한국어 | ✅ 완전 지원 (기준 언어) |
| `en` | 영어 | ✅ 완전 지원 (fallback 언어) |
| `de` | 독일어 | ✅ 완전 지원 (Phase 2) |
| `ru` | 러시아어 | ✅ 완전 지원 |
| `pt_BR` | 포르투갈어 (브라질) | ✅ 완전 지원 |

---

## 1. Config 언어 설정

### AcceptableValueList 순서
`SkillTree/SkillTreeConfig.cs`에서 F1 Config Manager 드롭다운 순서를 정의:
```csharp
new AcceptableValueList<string>("Auto", "KR", "EN", "DE", "RU", "PT_BR")
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
└── de.json      ← EN 키 전체 + DE 번역값 병합
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

## 참조

- `md/LOCALIZATION_GUIDE.md` - DefaultLanguages.cs 키 관리, 검증 스크립트
- `md/CONFIG_GUIDE.md` - Config 키 규칙, 번역 체계
- `Localization/LocalizationManager.cs` - 번역 로드/내보내기 핵심 로직
- `Localization/ConfigTranslations.cs` - F1 Config Manager 번역 진입점

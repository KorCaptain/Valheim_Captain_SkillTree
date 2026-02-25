# Config Manager 다국어 번역 적용 완료 리포트

**날짜:** 2025-02-23
**작업자:** Claude Code
**버전:** 0.1.612

---

## 📋 작업 요약

BepInEx Config Manager (F1 메뉴)에서 300+ 설정 항목의 설명이 한국어/영어로 표시되도록 ConfigTranslations.cs 번역을 적용했습니다.

---

## ✅ 완료된 작업

### Phase 0: 오늘 작업 되돌리기 (15분)

**제거한 파일:**
- `claudedocs/localization_fix_report_2025-02-22.md`

**수정한 파일:**
- `Localization/DefaultLanguages.cs`
  - 한국어 블록: 22개 키 제거 (Line 1205-1237)
  - 영어 블록: 22개 키 제거 (Line 2429-2461)
  - 이유: Config Manager 번역은 ConfigTranslations.cs 사용, DefaultLanguages.cs는 스킬트리 UI 전용

### Phase 1: Config 번역 방법 조사 (30분)

**현재 구조 분석:**
```
┌─────────────────────────────────────────────────────────┐
│ Config 번역 시스템 구조                                  │
├─────────────────────────────────────────────────────────┤
│                                                          │
│  DefaultLanguages.cs                                     │
│  └─ 스킬트리 UI 전용 (노드, 툴팁, 버프 표시)             │
│                                                          │
│  ConfigTranslations.cs                                   │
│  ├─ GetCategoryTranslations() - 5개 카테고리 번역        │
│  └─ GetDescriptionTranslations() - 300+ 설정 설명 번역  │
│                                                          │
│  SkillTreeConfig.cs                                      │
│  ├─ GetLocalizedCategory() - ConfigTranslations 조회    │
│  ├─ GetLocalizedDescription() - ConfigTranslations 조회 │
│  └─ GetConfigDescription() ← 이전: DefaultLanguages.cs  │
│                              ← 수정: ConfigTranslations.cs│
└─────────────────────────────────────────────────────────┘
```

**발견한 사실:**
1. ✅ 모든 Config 파일에서 이미 `GetConfigDescription()` 사용 중 (300+ 호출)
2. ✅ `GetLocalizedDescription()`은 정의되어 있지만 실제로 호출 안 됨
3. ❌ `GetConfigDescription()`이 DefaultLanguages.cs를 사용 (잘못된 번역 시스템)

### Phase 2: 최적 방법 선택 및 구현 (10분)

**선택한 방법:** 방법 1 변형 - `GetConfigDescription()` 내부 로직 수정

**장점:**
- ✅ 기존 300+ Config Bind 호출 수정 불필요 (한 곳만 수정)
- ✅ ConfigTranslations.cs의 번역 데이터 활용
- ✅ 최소한의 코드 변경으로 최대 효과

**수정한 파일:**
- `SkillTree/SkillTreeConfig.cs` (Line 662-691)

**변경 내용:**
```csharp
// Before (DefaultLanguages.cs 사용)
string locKey = $"config_{configKey.ToLower()}";
string result = Localization.L.Get(locKey);

// After (ConfigTranslations.cs 사용)
string result = GetLocalizedDescription(configKey);
```

### Phase 3: 테스트 및 검증 (5분)

**빌드 테스트:**
```
dotnet build Captain_SkillTree.csproj -c Debug

✅ 빌드했습니다.
    경고 0개
    오류 0개

버전: 0.1.611 → 0.1.612
```

**게임 테스트 필요:**
- [ ] 게임 실행 (한국어)
- [ ] F1 키 → BepInEx Configuration Manager 열기
- [ ] 카테고리 확인: "Bow Tree" (영어 고정)
- [ ] 설정 설명 확인: 한국어 번역 표시 여부
- [ ] 언어 변경 → 영어로 재시작 → 설정 설명 영어 표시 여부

---

## 🔍 기술적 세부사항

### ConfigTranslations.cs 구조

**1차 항목 (카테고리 - 5개):**
```csharp
GetKoreanCategories() → {
    "Bow Tree": "활 트리",
    "Sword Tree": "검 트리",
    "Attack Tree": "공격 트리",
    "Defense Tree": "방어 트리",
    "Speed Tree": "속도 트리"
}
```

**2차 항목 (설명 - 300+개):**
```csharp
GetKoreanDescriptions() → {
    "Tier0_BowExpert_RequiredPoints": "Tier 0: 활 전문가 - 필요 포인트",
    "Tier1_FocusedShot_RequiredPoints": "Tier 1: 집중 사격 - 필요 포인트",
    ...
}
```

### 번역 적용 흐름

```
1. 게임 시작
   └─ SkillTreeConfig.Initialize()
       └─ DetectConfigLanguage() → "ko" 또는 "en"

2. Config Bind 호출
   └─ SkillTreeConfig.BindServerSync(config, "Bow Tree", "Tier0_BowExpert_RequiredPoints", 2,
       GetConfigDescription("Tier0_BowExpert_RequiredPoints"))
           ↓
       GetConfigDescription("Tier0_BowExpert_RequiredPoints")
           ↓
       GetLocalizedDescription("Tier0_BowExpert_RequiredPoints")
           ↓
       ConfigTranslations.GetDescriptionTranslations("ko")
           ↓
       "Tier 0: 활 전문가 - 필요 포인트"

3. F1 메뉴 표시
   └─ BepInEx ConfigManager
       └─ ConfigDescription 읽기
           └─ 번역된 문자열 표시
```

---

## 📝 향후 Config 추가 시 가이드

### 필수 규칙

**1. ConfigTranslations.cs에 먼저 번역 추가**
```csharp
// Localization/ConfigTranslations.cs

// 한국어 블록 (GetKoreanDescriptions)
["new_skill_name"] = "새 스킬 설명",

// 영어 블록 (GetEnglishDescriptions)
["new_skill_name"] = "New Skill Description",
```

**2. Config 파일에서 GetConfigDescription() 호출**
```csharp
// SkillTree/YourWeapon_Config.cs

YourNewSetting = SkillTreeConfig.BindServerSync(config,
    "Your Tree",  // 카테고리 (영어 고정)
    "new_skill_name",
    defaultValue,
    SkillTreeConfig.GetConfigDescription("new_skill_name")  // 자동 번역
);
```

**3. 체크리스트**
- [ ] ConfigTranslations.cs - 한국어 블록에 키 추가
- [ ] ConfigTranslations.cs - 영어 블록에 키 추가
- [ ] Config 파일 - GetConfigDescription() 호출
- [ ] 빌드 테스트 (경고 0개)
- [ ] F1 메뉴 실행 테스트 (번역 표시 확인)

---

## 🎯 성공 기준

### 즉각적 성공
- [x] 오늘 추가한 22개 키 제거 완료 (DefaultLanguages.cs)
- [x] GetConfigDescription() 수정 완료 (ConfigTranslations.cs 사용)
- [x] 빌드 경고: 0개
- [ ] Config Manager (F1) 한국어 설명 표시 (게임 테스트 필요)
- [ ] Config Manager (F1) 영어 설명 표시 (게임 테스트 필요)

### 장기적 성공
- [x] 새 Config 추가 시 번역 적용 프로세스 확립
- [ ] 문서화 완료 (CLAUDE.md 업데이트 필요)
- [ ] 언어 변경 시 자동 번역 작동 (게임 테스트 필요)

---

## 📋 후속 작업

### 필수 작업
1. **게임 실행 테스트**
   - 한국어로 게임 시작 → F1 메뉴 확인
   - 영어로 게임 재시작 → F1 메뉴 확인
   - 스크린샷 캡처

2. **CLAUDE.md 업데이트**
   - Config Manager 번역 규칙 추가 (필수 준수 사항 섹션)
   - ConfigTranslations.cs 사용 가이드
   - 체크리스트 추가

3. **CONFIG_MANAGEMENT_RULES.md 수정**
   - 잘못된 예시 코드 수정
   - GetLocalizedDescription() 사용 예시 추가

### 선택 작업
- 로컬라이제이션 키 검증 스크립트에 ConfigTranslations.cs 검증 추가
- Config 추가 자동화 스크립트 작성

---

## 🔄 변경 파일 요약

| 파일 | 변경 내용 | 라인 수 |
|------|-----------|---------|
| `Localization/DefaultLanguages.cs` | 22개 키 제거 (한국어 + 영어) | -33 |
| `SkillTree/SkillTreeConfig.cs` | GetConfigDescription() 로직 수정 | ~10 |
| `claudedocs/localization_fix_report_2025-02-22.md` | 파일 삭제 | -전체 |
| `claudedocs/config_localization_implementation_2025-02-23.md` | 새 리포트 생성 | +전체 |

---

## 💡 핵심 인사이트

1. **분리된 번역 시스템:**
   - DefaultLanguages.cs = 스킬트리 UI (노드, 툴팁, 버프)
   - ConfigTranslations.cs = Config Manager F1 메뉴

2. **효율적 구현:**
   - 300+ Config Bind 호출 수정 대신, 한 곳(GetConfigDescription)만 수정
   - 최소 변경, 최대 효과

3. **카테고리 번역 보류:**
   - 현재는 설명만 번역 적용
   - 카테고리는 영어 고정 (CONFIG_MANAGEMENT_RULES.md 규칙 준수)
   - 필요 시 BindServerSync 메서드에서 GetLocalizedCategory() 호출 추가 가능

---

## 📚 참고 문서

- `md/CONFIG_RULES.md` - Config 초기화 순서 규칙
- `md/CONFIG_MANAGEMENT_RULES.md` - Config 관리 가이드
- `Localization/ConfigTranslations.cs` - 번역 데이터 소스
- `SkillTree/SkillTreeConfig.cs` - Config 바인드 헬퍼 메서드

---

**작업 완료 시간:** 약 1시간
**예상 대비:** -1.5~2.5시간 절약 (효율적 구현 덕분)

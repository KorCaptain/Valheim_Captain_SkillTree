# 로컬라이제이션 가이드

> 최종 업데이트: 2026-02-25
> CLAUDE.md 규칙 11, 12 및 Rule 3 (DefaultLanguages 규칙) 통합본

---

## 1. 번역 파일 구조

| 파일 | 용도 | 대상 |
|------|------|------|
| `Localization/DefaultLanguages.cs` | 스킬트리 UI 전용 | 노드명, 툴팁, 버프 표시 |
| `Localization/ConfigTranslations.cs` | F1 Config Manager 전용 | 카테고리, 설정 설명 |

> ❌ 혼용 금지: DefaultLanguages.cs에 Config 키 추가하거나 반대로 하면 안 됨

---

## 2. 스킬트리 UI 로컬라이제이션 (DefaultLanguages.cs)

### 키 명명 규칙
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

## 3. 키 누락 방지 워크플로우

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

## 4. 검증 스크립트

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

## 5. Config Manager 번역 (ConfigTranslations.cs)

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

> 상세 내용은 `md/CONFIG_GUIDE.md` 섹션 5 참조

---

## 6. 전체 체크리스트

스킬/효과 추가·수정 시:
- [ ] DefaultLanguages.cs - KO 블록에 키 추가
- [ ] DefaultLanguages.cs - EN 블록에 키 추가
- [ ] 수치 포맷(`{0}`, `{1}`) 모든 언어 동기화
- [ ] `validate_loc_keys.ps1` 실행 (누락 키 0개)
- [ ] 빌드 테스트 (경고 0개)

새 Config 추가 시:
- [ ] ConfigTranslations.cs - KO Description 추가 (【】 형식)
- [ ] ConfigTranslations.cs - EN Description 추가 (【】 형식)
- [ ] Config 파일 - GetConfigDescription() 사용
- [ ] F1 메뉴 번역 표시 확인

---

## 관련 문서
- `md/CONFIG_GUIDE.md` - Config 다국어 번역 상세 규칙
- `Localization/DefaultLanguages.cs` - 스킬트리 UI 번역 데이터
- `Localization/ConfigTranslations.cs` - Config Manager 번역 데이터
- `scripts/validate_loc_keys.ps1` - 키 누락 검증 스크립트

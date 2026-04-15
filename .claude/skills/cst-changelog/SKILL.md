---
name: cst-changelog
description: 코드 수정 완료 후 CHANGELOG.md 기록 및 manifest.json 버전 자동 업데이트. changelog, 변경로그, 버전업, version up, 배포 준비 키워드로 트리거.
---

# CaptainSkillTree CHANGELOG 자동 기록

코드 수정이 완료된 후 호출하는 스킬. manifest.json 버전 증가 → 변경 내용 요약 → 사용자 확인 → CHANGELOG.md 기록 순서로 진행한다.

---

## Step 1: 현재 체인지로그 버전 확인 (즉시 실행, 확인 불필요)

1. `Thunderstore/manifest.json` 읽기
2. `version_number` 파싱 → 이 값이 현재 체인지로그 버전 (형식: `MAJOR.MINOR.XX`)

> 버전은 빌드 시 `IncrementVersion.ps1`이 자동 설정한다.
> 빌드 버전(3자리, 예: `1.2.031`)을 10개씩 묶어 체인지로그 버전(2자리, 예: `1.2.04`)으로 표기.
> 매핑: 001~010 → 01, 011~020 → 02, 031~040 → 04 등

```json
// 예시: manifest.json의 "version_number": "1.2.04"
//       → 이번 CHANGELOG 항목도 [1.2.04] 로 기록
```

---

## Step 2: 이번 세션 변경 내용 요약

대화 기록과 수정된 파일을 바탕으로 변경 내용을 분류:

| 기호 | 의미 |
|------|------|
| ✅fix | 버그 수정 |
| ✅new | 신기능 추가 |
| ✅improve | 기존 기능 개선 |

**요약 규칙:**
- 핵심 내용만, 한 줄로 충분히 표현
- 내부 리팩터링·파일명 변경 등 사용자에게 보이지 않는 것은 제외
- 플레이어가 체감하는 변화 위주 (스킬 효과, UI, 밸런스, 버그 등)
- 번호는 `fix1`, `fix2` ... 순서대로 (영어와 한국어 동일 번호)

---

## Step 3: 사용자에게 요약 제시 및 확인

아래 형식으로 요약을 출력하고 질문:

```
[체인지로그 버전: X.X.XX] CHANGELOG에 기록할 내용:
(빌드 버전 X.X.XXX → 공개 버전 X.X.XX로 그룹 표기)

- ✅fix1 : [English]
- ✅fix2 : [English]

- ✅fix1 : [한국어]
- ✅fix2 : [한국어]

CHANGELOG.md에 위 내용을 기록할까요? (OK / Skip)
```

---

## Step 3.5: 날짜 중복 체크

CHANGELOG.md 첫 번째 항목의 날짜와 오늘 날짜를 비교한다:

- **같은 날짜**: 새 항목 생성 X → 기존 항목 끝에 새 변경사항을 **추가(append)**
  - 버전 번호: 새 버전이 더 높으면 첫 줄 `# [X.X.X]` 업데이트
  - fix 번호: 기존 마지막 fix 번호 이어서 증가 (예: 기존 fix3 → 새 항목은 fix4부터)
  - new 번호: 기존 마지막 new 번호 이어서 증가
  - 한국어 항목도 동일하게 `-` 구분선 아래에 이어서 추가
- **다른 날짜**: 기존대로 최상단에 새 항목 생성 (Step 4로 이동)

---

## Step 4: CHANGELOG.md 작성 (OK 응답 시)

### 새 날짜인 경우 (Step 3.5에서 다른 날짜 판정)
`Thunderstore/CHANGELOG.md` 파일 **최상단** (첫 번째 `#` 항목 바로 앞)에 삽입.

**삽입 형식:**
```markdown
# [X.X.X] - YYYY-MM-DD
- ✅fix1 : [English description]
- ✅fix2 : [English description]
-
- ✅fix1 : [한국어 설명]
- ✅fix2 : [한국어 설명]

```

### 같은 날짜인 경우 (Step 3.5에서 동일 날짜 판정)
기존 항목 끝에 이어서 추가. 영어 구분선(`-`) 앞에 영어 항목, 한국어 구분선 아래에 한국어 항목 추가.

**주의:**
- 영어 항목 먼저, `-` 단독 구분선, 한국어 항목 순서
- 날짜는 오늘 날짜 (YYYY-MM-DD)
- 기존 내용은 건드리지 않음 (상단 삽입 또는 끝에 append만)
- Skip 선택 시 → manifest.json 버전만 올라간 상태 유지, CHANGELOG 미기록

---

## 사용 예시

사용자: "changelog 기록해줘" 또는 "버전업 해줘" 또는 "배포 준비해줘"
→ 이 스킬 즉시 실행

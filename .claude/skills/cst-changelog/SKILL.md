---
name: cst-changelog
description: 코드 수정 완료 후 CHANGELOG.md 기록 및 manifest.json 버전 자동 업데이트. changelog, 변경로그, 버전업, version up, 배포 준비 키워드로 트리거.
---

# CaptainSkillTree CHANGELOG 자동 기록

코드 수정이 완료된 후 호출하는 스킬. **확인을 묻지 않고 항상 자동으로 실행한다** (사용자 지시: "이건 항상 자동으로 하게 해줘").

## 파일 구조 (중요)

- **`Thunderstore/CHANGELOG.md`** — 이 스킬이 유일하게 직접 쓰는 파일. 파일명을 포함한 상세 버전.
- **`Thunderstore/Captain_Skilltree/CHANGELOG.md`** — 배포판(플레이어 노출용). **이 스킬은 이 파일을 직접 건드리지 않는다.** `build/SyncThunderstorePackage.ps1`이 `dotnet build`(PostBuild)마다 루트 파일에서 `[File.cs, ...]` 대괄호 파일명 표기와 `## Files Modified` 블록을 자동으로 제거해 이 파일을 다시 생성한다.

---

## Step 1: 현재 버전 확인 (즉시 실행)

`Thunderstore/manifest.json`의 `version_number`를 읽는다. 날짜 헤더 옆에 `(vX.X.XX)` 형태로만 참고용으로 표기하고, 그 외에는 버전 번호를 본문에서 신경 쓰지 않는다(과거처럼 버전별 헤더/번호매기 안 함).

---

## Step 2: 변경 내용 요약 (핵심)

대화 기록과 실제로 수정한 파일을 바탕으로, **항목당 한 줄**로 간결하게 요약한다.

**규칙:**
- 번호 매기기(`fix1`, `improve2` 등) 사용하지 않는다 — 그냥 `-` bullet.
- ✅fix/new/improve 같은 카테고리 태그도 붙이지 않는다. 설명 자체로 무엇이 바뀌었는지 드러나면 충분하다.
- 한 줄에 핵심만: 무엇이 바뀌었는지 + (필요하면) 수치/이유를 짧게. 여러 문장으로 풀어쓰지 않는다.
- 내부 리팩터링·파일명 변경 등 플레이어가 체감할 수 없는 것은 제외. 스킬 효과/UI/밸런스/버그 등 실제로 게임에서 느껴지는 변화 위주.
- 각 항목 맨 앞에 `[File1.cs, File2.cs]` 형태로 이번에 실제로 수정한 파일을 전부 나열(신규 파일은 `(new)`/`(신규)`, 삭제 파일은 `(removed)`/`(삭제)` 표기). 이 대괄호 부분은 루트 파일에만 쓰고, 배포판은 빌드 시 자동으로 제거되므로 신경 쓰지 않는다.
- 영어 항목과 한국어 항목은 내용이 1:1로 대응해야 한다(개수·순서 동일).

---

## Step 3: 날짜 중복 체크

`Thunderstore/CHANGELOG.md`에서 가장 위에 있는 `## YYYY-MM-DD` 헤더의 날짜와 오늘 날짜를 비교한다.

- **같은 날짜**: 새 헤더를 만들지 않고, 기존 `### English` 목록 끝에 새 bullet을 추가하고 `### 한국어` 목록 끝에도 대응하는 bullet을 추가한다. 헤더의 `(vX.X.XX)`는 버전이 더 올라갔으면 최신 버전으로 갱신.
- **다른 날짜**: 파일 맨 위(`# Changelog / 변경 로그` 헤더 바로 다음, 기존 첫 항목 바로 앞)에 새 `## YYYY-MM-DD (vX.X.XX)` 블록을 삽입.

---

## Step 4: CHANGELOG.md 작성 (바로 실행, 확인 불필요)

### 새 날짜 삽입 형식
```markdown
## YYYY-MM-DD (vX.X.XX)

### English
- [File1.cs, File2.cs] One-line description of the change.
- [File3.cs] Another change.

### 한국어
- [File1.cs, File2.cs] 변경 내용 한 줄 설명.
- [File3.cs] 다른 변경 내용.

```
(끝에 빈 줄 하나를 두고 바로 다음 기존 항목이 이어지게 한다.)

### 같은 날짜에 이어 쓰는 경우
기존 `### English` 목록의 마지막 bullet 바로 다음 줄에 새 bullet을 추가하고, `### 한국어` 목록의 마지막 bullet 바로 다음 줄에도 대응하는 bullet을 추가한다. 그 외 기존 내용은 건드리지 않는다.

**공통 주의사항:**
- 과거(오늘 이전) 날짜의 항목은 옛 포맷(`# [X.X.X] - YYYY-MM-DD` + `## Files Modified` + 번호매기 bullet)이어도 **절대 새 포맷으로 소급 변환하지 않는다.** 그대로 둔다.
- `Thunderstore/Captain_Skilltree/CHANGELOG.md`는 이 스킬이 직접 쓰지 않는다 — 다음 `dotnet build` 시 자동 생성된다.

---

## Step 4.5: 배포판 즉시 갱신 (중요)

Step 4에서 루트에 기록한 직후, 다음 실제 빌드까지 기다리지 않고 배포판을 바로 최신 상태로 맞춘다:

```
powershell -ExecutionPolicy Bypass -File build\SyncThunderstorePackage.ps1 -ProjectDir "C:\home\ssunyme\.npm-global\bin\CaptainSkillTree" -DllPath "C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\bin\CaptainSkillTree.dll"
```

(DLL이 이미 최신이 아니어도 체인지로그 파생 로직 자체는 항상 실행되므로 안전하다. `-DllPath`가 존재하지 않으면 DLL 복사 단계만 건너뛴다.)

---

## Step 5: 파일 크기 자동 정리 (매번 기록 후 실행, 확인 불필요)

`Thunderstore/CHANGELOG.md` 기준 100,000자 초과 시, 파일 하단(가장 오래된 항목)부터 최상위 헤더(`## YYYY-MM-DD` 또는 옛 포맷 `# [X.X.X]`) 단위로 삭제해 **95,000자 이하**가 될 때까지 자른다. 항목 중간을 자르지 않는다. 결과(이전 크기 → 이후 크기, 삭제된 날짜 범위)만 알린다.

---

## 사용 예시

사용자: "changelog 기록해줘" 또는 "버전업 해줘" 또는 "배포 준비해줘"
→ 이 스킬 즉시 실행, 확인 없이 `Thunderstore/CHANGELOG.md`에 기록하고 결과만 짧게 보고

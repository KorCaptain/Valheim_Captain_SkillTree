# CaptainSkillTree 문서

CaptainSkillTree Valheim 모드의 개발 및 유지보수 문서입니다.

## 디렉토리 구조

```
docs/
├── 01-plan/              # 기획 단계 문서
│   └── features/
├── 02-design/            # 설계 단계 문서
│   └── features/
├── 03-analysis/          # 검증 단계 문서 (Gap Analysis)
├── 04-report/            # 완료 보고서
│   ├── features/
│   ├── changelog.md      # 변경로그
│   └── SkillTree-PerformanceFix.report.md
├── archive/              # 아카이브 (완료된 피처)
└── README.md             # 이 파일
```

## PDCA 사이클

각 피처의 개발은 PDCA(Plan-Design-Do-Check-Act) 사이클을 따릅니다.

### 1. Plan (기획)
- 목표 및 요구사항 정의
- 위험도 및 영향도 분석
- 성공 기준 수립

**문서**: `01-plan/features/{feature}.plan.md`

### 2. Design (설계)
- 기술 설계 및 아키텍처
- API/데이터 모델 정의
- 구현 순서 및 테스트 계획

**문서**: `02-design/features/{feature}.design.md`

### 3. Do (구현)
- 코드 개발 및 구현
- 단위 테스트 작성
- 빌드 및 기본 검증

**산출물**: 소스 코드, 테스트 코드

### 4. Check (검증)
- 설계와 구현의 일치도 분석
- Gap 분석 및 이슈 리포트
- Match Rate 산출

**문서**: `03-analysis/{feature}.analysis.md`

### 5. Act (완료)
- Gap 보완 및 개선
- 최종 보고서 작성
- 프로젝트 종료 및 기록

**문서**: `04-report/features/{feature}.report.md`

---

## 최근 완료 피처

### SkillTree-PerformanceFix (v1.2.08)
- **상태**: 완료 ✅
- **버전**: 1.2.08
- **완료일**: 2026-03-26
- **설명**: SkillBuffDisplay.cs 성능 최적화 (GC 스파이크 80% 감소)

**주요 개선**:
1. RectTransform 캐싱 - 메모리 할당 제거
2. WaitForSeconds 정적화 - 코루틴 메모리 91% 감소
3. 조건부 텍스트 갱신 - 렌더링 비용 83% 감소

**문서**:
- 완료 보고서: [SkillTree-PerformanceFix.report.md](04-report/SkillTree-PerformanceFix.report.md)
- 변경로그: [changelog.md](04-report/changelog.md)
- 상태 파일: [.pdca-status.json](.pdca-status.json)

---

## 문서 작성 규칙

### 파일 명명
- 영문 소문자 + 하이픈 + `.md` 확장자
- 예: `skill-tree-ui.plan.md`, `performance-fix.design.md`

### 문서 헤더 (필수)
```markdown
# {제목}

> **요약**: {한 줄 설명}
>
> **작성일**: {YYYY-MM-DD}
> **상태**: Draft | Review | Approved
> **버전**: 1.0

---
```

### 상태 표시
- ✅ 완료 (Completed)
- 🔄 진행 중 (In Progress)
- ⏸️ 보류 (On Hold)
- ❌ 폐기 (Deprecated)

---

## 변경로그 관리

모든 주요 변경사항은 `04-report/changelog.md`에 기록합니다.

### 포맷 (Semantic Versioning)
```markdown
## [v{MAJOR}.{MINOR}.{PATCH}] - {YYYY-MM-DD}

### Added
- 새 기능

### Changed
- 변경 사항

### Fixed
- 버그 수정

### Performance
- 성능 개선
```

---

## 상태 추적 (.pdca-status.json)

PDCA 사이클 진행 상황을 JSON 형식으로 추적합니다.

**주요 필드**:
- `status`: 현재 상태 (completed, in-progress, on-hold)
- `phase`: PDCA 단계 (plan, design, do, check, act, completed)
- `matchRate`: 설계와 구현의 일치도 (0-100%)
- `iterationCount`: 개선 반복 횟수
- `documents`: 각 단계별 문서 경로

---

## 아카이빙

완료된 피처는 `archive/YYYY-MM/` 폴더로 이동됩니다.

### 아카이브 구조
```
archive/
├── 2026-03/
│   ├── SkillTree-PerformanceFix/
│   │   ├── plan.md
│   │   ├── design.md
│   │   ├── analysis.md
│   │   └── report.md
│   └── _INDEX.md
```

---

## 참고 문서

프로젝트 루트의 `md/` 디렉토리에 있는 개발 가이드 문서들:

- `CLAUDE.md` - Claude AI 개발 규칙 및 체크리스트
- `SKILL_DEVELOPMENT_WORKFLOW.md` - 스킬 개발 워크플로우
- `CONFIG_GUIDE.md` - 설정 시스템 가이드
- `MULTILANGUAGE_GUIDE.md` - 다국어 시스템 가이드
- `UI_SYSTEM_RULES.md` - UI 시스템 규칙
- `ZNETSCENE_VFX_RULES.md` - VFX 시스템 규칙
- `MMO_INTEGRATION_GUIDE.md` - MMO 연동 가이드

---

## 빠른 참조

### 새 피처 추가
```
1. Plan 문서 작성: /pdca plan {feature-name}
2. Design 문서 작성: /pdca design {feature-name}
3. 구현: /pdca do {feature-name}
4. 검증 분석: /pdca analyze {feature-name}
5. 개선 반복: /pdca iterate {feature-name} (필요시)
6. 완료 보고서: /pdca report {feature-name}
7. 아카이빙: /pdca archive {feature-name}
```

### 현재 상태 확인
```
/pdca status
```

### 다음 단계 가이드
```
/pdca next
```

---

**최종 업데이트**: 2026-03-26
**문서 버전**: 1.0

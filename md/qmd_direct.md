# QMD 컬렉션 디렉토리 — CaptainSkillTree

> QMD Collection: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree`  
> 총 69개 MD 파일 인덱싱 · 업데이트: `qmd update` · 임베딩: `qmd embed`

---

## 빠른 검색 패턴

```json
// 규칙 찾기
{ "searches": [{"type":"lex","query":"VFX rules"},{"type":"vec","query":"패시브 VFX 금지 규칙"}],
  "collections": ["C:\\home\\ssunyme\\.npm-global\\bin\\CaptainSkillTree"] }

// 구현 파일 찾기
{ "searches": [{"type":"lex","query":"SkillBonusCalculator CalculateTotal"}],
  "collections": ["C:\\home\\ssunyme\\.npm-global\\bin\\CaptainSkillTree"] }

// 개념으로 찾기
{ "searches": [{"type":"vec","query":"공격속도 누적 계산 방식"}],
  "collections": ["C:\\home\\ssunyme\\.npm-global\\bin\\CaptainSkillTree"] }
```

---

## 파일 카탈로그

### 루트 핵심 문서

| 파일 | 크기 | 내용 |
|------|------|------|
| `readme.md` | 14.6KB | 프로젝트 전체 개요, 스킬 목록, 설치 가이드 |
| `claude.md` | 4.2KB | Claude 행동 규칙, QMD 검색 원칙, 모델 사용 기준 |
| `claude-3man.md` | 11.1KB | **CRITICAL RULES** — 3-man 팀 규칙, MMO 연동, VFX, 스킬 ID |
| `architect.md` | 7.7KB | 전체 아키텍처 가이드, 시스템 설계 원칙 |
| `builder.md` | 3.2KB | 빌더 역할 가이드, 구현 패턴 |
| `reviewer.md` | 3.5KB | 리뷰어 체크리스트 |
| `active-skill.md` | 3.8KB | 액티브 스킬 전체 목록 (T/G/H/Y 키 매핑) |
| `job-skill.md` | 5.8KB | 직업 스킬 목록 (6직업: 궁수/마법사/탱커/로그/성기사/광전사) |
| `new-setup.md` | 3.8KB | 신규 세션 설정 가이드 |

---

### rules/ — 필수 규칙 파일

| 파일 | 트리거 조건 |
|------|-----------|
| `rules/localization-rules.md` | L.Get(), 언어 키, Config 번역, json 동기화 |
| `rules/config-init-rules.md` | SkillTreeConfig, Initialize, BindServerSync 순서 |
| `rules/performance-rules.md` | Update 패치, 코루틴, 신규/수정 스킬 완성 후 |
| `rules/ui-rendering-rules.md` | SetSiblingIndex, UI, panel, tooltip 순서 |

---

### md/ — 시스템 규칙 & 가이드 (40개)

#### 데미지·전투 시스템
| 파일 | 내용 |
|------|------|
| `md/damage-system-rules.md` | 데미지 계산 규칙, HitData 패치 |
| `md/critical-system-rules.md` | 크리티컬 타격 시스템 |
| `md/attack-speed-system-rules.md` | 공격속도 누적 계산, AnimationSpeedManager |
| `md/attack-speed-bug.md` | 공격속도 버그 케이스 모음 |
| `md/secondary-attack-speed-bug.md` | 2차 공격 속도 버그 |
| `md/evasion-system-rules.md` | 회피 시스템 규칙 |
| `md/armor-block-system-rules.md` | 방어력·블록 파워 시스템 |
| `md/eitr-stagger-system-rules.md` | 에이트르·스태거 시스템 |
| `md/stagger-verification-guide.md` | 스태거 검증 가이드 |
| `md/parry-detection-system.md` | 패링 감지 시스템 |
| `md/health-system-rules.md` | 체력·힐 시스템 |

#### UI·툴팁 시스템
| 파일 | 내용 |
|------|------|
| `md/ui-system-rules.md` | UI 렌더링 규칙, 패널 구조 |
| `md/aattack-tolltip-display.md` | 공격 툴팁 표시 규칙 |
| `md/armor-tooltip-display-rules.md` | 방어구 툴팁 표시 규칙 |
| `md/accessory-cape-tooltip-display-rules.md` | 악세서리·망토 툴팁 규칙 |
| `md/tooltip-color-standard.md` | 툴팁 색상 표준 |
| `md/skill-effect-text-standard.md` | 스킬 효과 텍스트 표준 |
| `md/producer-display.md` | 제작 전문가 인챈트 표시 규칙 |
| `md/까마귀패널.md` | 까마귀 패널 UI |

#### VFX·사운드
| 파일 | 내용 |
|------|------|
| `md/znetscene-vfx-rules.md` | ZNetScene VFX 규칙, PlayVFXMultiplayer |
| `md/버프형-vfx.md` | 버프형 VFX 패턴 |
| `md/vfx-sound-infinite-loading-fix.md` | VFX/사운드 무한 로딩 버그 픽스 |

#### 스킬 개발
| 파일 | 내용 |
|------|------|
| `md/active-skill-system.md` | 액티브 스킬 시스템 전체 구조 |
| `md/skill-development-workflow.md` | 스킬 개발 워크플로우 |
| `md/skill-naming-rules.md` | 스킬 명명 규칙 |
| `md/skill-proficiency-system.md` | 스킬 숙련도 시스템 |
| `md/job-lv2.md` | 직업 Lv2 스킬 설계 |
| `md/job-skill-levelup.md` | 직업 스킬 레벨업 규칙 |
| `md/crossbow-skill-status.md` | 석궁 스킬 현황 |

#### 시스템·통합
| 파일 | 내용 |
|------|------|
| `md/mmo-integration-guide.md` | EpicMMO 연동 가이드, getParameter |
| `md/multilanguage-guide.md` | 다국어 시스템 가이드 (7개 언어) |
| `md/config-guide.md` | Config 시스템 가이드 (23KB, 가장 큰 규칙 파일) |
| `md/inventory-patch-checklist.md` | 인벤토리 패치 체크리스트 |
| `md/harmony-patch-target-rules.md` | Harmony 패치 대상 규칙 |
| `md/valheim-all-api.md` | Valheim 전체 API 레퍼런스 |
| `md/speed-expert-valheim-api-implementation.md` | 속도 관련 API 구현 |
| `md/weapon-detection-extensibility-guide.md` | 무기 감지 확장성 가이드 |
| `md/weaponloaded.md` | 무기 로드 상태 |

#### 빌드·개발 환경
| 파일 | 내용 |
|------|------|
| `md/build-errors-guide.md` | 빌드 에러 가이드 |
| `md/development-patterns.md` | 개발 패턴 모음 |
| `md/core-protection-readme.md` | 코어 파일 보호 규칙 |
| `md/mcp-project-setup.md` | MCP 프로젝트 설정 |
| `md/quick-reference.md` | 빠른 참조 (19.9KB) |
| `md/production-damage-text-implementation.md` | 제작 데미지 텍스트 구현 |

---

### handoff/ — 세션 인계 문서

| 파일 | 내용 |
|------|------|
| `handoff/session-checkpoint.md` | 현재 세션 체크포인트 |
| `handoff/architect-brief.md` | 아키텍트 브리프 |
| `handoff/build-log.md` | 빌드 로그 |
| `handoff/review-feedback.md` | 리뷰 피드백 |
| `handoff/review-request.md` | 리뷰 요청 |

---

### docs/ — 문서화

| 파일 | 내용 |
|------|------|
| `docs/readme.md` | 문서 구조 안내 |
| `docs/04-report/changelog.md` | 개발 보고서 |
| `docs/archive/2026-03/` | 2026-03 아카이브 (Paladin v1.2.12, 성능 픽스) |

---

### thunderstore/ — 배포 문서

| 파일 | 크기 | 내용 |
|------|------|------|
| `thunderstore/captain-skilltree/changelog.md` | **31.6KB** | 전체 버전 히스토리 |
| `thunderstore/captain-skilltree/readme.md` | 22.5KB | Thunderstore 공개 README |

---

## 주제별 검색 치트시트

| 작업 | 검색어 |
|------|--------|
| 새 스킬 추가 절차 | `lex: "스킬 변경 5종 세트"` |
| VFX 규칙 확인 | `lex: PlayVFXMultiplayer` |
| 데미지 계산 | `vec: "HitData 데미지 누적 계산"` |
| Config 번역 키 | `lex: GetConfigDescription` |
| 공격속도 패치 | `lex: AnimationSpeedManager` |
| 툴팁 색상 | `lex: tooltip color` |
| MMO 연동 | `lex: getParameter EpicMMO` |
| 버전 업 절차 | `lex: changelog PATCH MINOR` |
| 빌드 오류 | `lex: CS0 build error` |
| 다국어 키 추가 | `lex: DefaultLanguages json` |

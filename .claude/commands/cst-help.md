# CaptainSkillTree Skill 도움말

사용 가능한 CaptainSkillTree skill 목록입니다.

## 카테고리별 Skill (여러 문서 로드)

| Skill | 설명 | 문서 수 |
|-------|------|---------|
| `/cst-core` | 핵심 시스템 규칙 전체 | 10개 |
| `/cst-impl` | 구현 가이드 전체 | 6개 |
| `/cst-workflow` | 개발 워크플로우 | 3개 |
| `/cst-ref` | 참고 문서 | 6개 |
| `/cst-all` | 전체 문서 목록 | 26개 |

## 개별 Skill (단일 문서 로드)

| Skill | 설명 |
|-------|------|
| `/cst-damage` | 데미지 계산 규칙 |
| `/cst-health` | 체력 시스템 규칙 |
| `/cst-critical` | 크리티컬 시스템 규칙 |
| `/cst-mmo` | MMO 통합 가이드 |
| `/cst-vfx` | VFX/사운드 시스템 |
| `/cst-active` | 액티브 스킬 시스템 |
| `/cst-naming` | 스킬 명명 규칙 |
| `/cst-config` | 설정 관리 규칙 |
| `/cst-ui` | UI 시스템 규칙 |
| `/cst-proficiency` | 숙련도 보너스 시스템 |
| `/cst-quick` | 빠른 참조 |
| `/cst-build` | 빌드 오류 가이드 |

## 사용 예시

```
/cst-core       # 핵심 시스템 10개 문서 로드
/cst-damage     # 데미지 시스템만 로드
/cst-help       # 이 도움말 표시
```

## 권장 사용법

1. **새 스킬 개발 시**: `/cst-workflow` + `/cst-naming`
2. **데미지 관련 작업**: `/cst-damage` + `/cst-critical`
3. **숙련도 보너스 작업**: `/cst-proficiency`
4. **UI 작업**: `/cst-ui`
5. **VFX 작업**: `/cst-vfx`
6. **빌드 오류**: `/cst-build`
7. **전체 컨텍스트 필요**: `/cst-core` → `/cst-impl`

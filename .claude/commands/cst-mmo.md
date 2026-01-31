# MMO 통합 가이드 로드

EpicMMOSystem과의 통합 패턴을 로드합니다.

## 로드할 문서

`C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\MMO_INTEGRATION_GUIDE.md`

Read 도구로 위 파일을 읽고 MMO 연동 개발에 적용하세요.

## 핵심 원칙

- **Tier 1 (최우선)**: MMO getParameter 패치를 통한 스탯 연동
- **Tier 2 (예외적)**: 직접 패치는 MMO가 지원하지 않는 특수 효과만
- 모든 기본 스탯 효과는 MMO 시스템을 통해 구현

## 주요 패턴

- getParameter 패치 방식
- 스탯 보너스 적용 방법
- MMO 시스템과의 충돌 방지

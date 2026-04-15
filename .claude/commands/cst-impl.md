# CaptainSkillTree 구현 가이드 로드

스킬 구현에 필요한 가이드 문서들을 로드합니다.

## 로드할 문서 (6개)

다음 MD 파일들을 읽고 컨텍스트에 적용하세요:

1. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\MMO_INTEGRATION_GUIDE.md` - MMO 시스템 통합 패턴
2. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ACTIVE_SKILL_SYSTEM.md` - 키 바인딩 및 제한 시스템
3. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\VFX_SOUND_INFINITE_LOADING_FIX.md` - VFX/사운드 무한로딩 수정
4. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\WEAPON_DETECTION_EXTENSIBILITY_GUIDE.md` - 무기 감지 확장 가이드
5. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SKILL_NAMING_RULES.md` - 스킬 명명 규칙
6. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\CORE_PROTECTION_README.md` - 코어 보호 규칙

## 적용 지침

1. 각 파일을 Read 도구로 읽습니다
2. 구현 패턴과 가이드라인을 현재 작업에 적용합니다
3. 새 스킬 개발 시 이 가이드를 따릅니다

## 핵심 내용

- **MMO 통합**: getParameter 패치를 통한 스탯 연동 우선
- **액티브 스킬**: T/G/H/Y 키 바인딩 규칙
- **VFX 수정**: 무한 로딩 방지 패턴
- **스킬 명명**: ID 명명 규칙 준수
- **코어 보호**: Plugin.cs, InputListener.cs 수정 금지

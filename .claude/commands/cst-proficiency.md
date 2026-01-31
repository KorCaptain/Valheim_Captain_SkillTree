# 숙련도 보너스 시스템 로드

CaptainSkillTree 숙련도 보너스 시스템 규칙을 로드합니다.

## 로드할 문서

`C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\SKILL_PROFICIENCY_SYSTEM.md`

Read 도구로 위 파일을 읽고 숙련도 관련 개발에 적용하세요.

## 핵심 내용

- 숙련도 보너스 계산 (`GetSkillLevelBonus`)
- UI 표시 패치 (`SkillsDialog.Setup`)
- 한글 스킬 이름 매핑
- 새 보너스 추가 방법
- 다중 텍스트 컴포넌트 처리
- 중복 호출 방지

## 관련 파일

- `SkillTree/SkillEffect.SpeedTree2.cs` - 보너스 계산
- `SkillTree/SkillEffect.SkillUIDisplay.cs` - UI 패치
- `SkillTree/SkillTreeConfig.cs` - Config 값

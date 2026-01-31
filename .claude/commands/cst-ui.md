# UI 시스템 규칙 로드

스킬트리 UI 시스템 규칙을 로드합니다.

## 로드할 문서

`C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\UI_SYSTEM_RULES.md`

Read 도구로 위 파일을 읽고 UI 개발에 적용하세요.

## 핵심 규칙

### UI 렌더링 순서 (SetSiblingIndex)
1. **스킬트리 배경** (index 0)
2. **노드 연결선** (index 1)
3. **일반 노드 아이콘** (index 2)
4. **직업 아이콘** (index 3)
5. **툴팁** (최상위)

### 직업 아이콘 판별
Berserker, Tanker, Rogue, Archer, Mage, Paladin

### 적용 파일
- SkillTreeUI.cs
- SkillTreeNodeUI.cs
- SkillTreeTooltip.cs

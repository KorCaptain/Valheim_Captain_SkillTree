# CaptainSkillTree 핵심 시스템 로드

CaptainSkillTree 개발에 필요한 핵심 시스템 규칙들을 로드합니다.

## 로드할 문서 (10개)

다음 MD 파일들을 순서대로 읽고 컨텍스트에 적용하세요:

1. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\DAMAGE_SYSTEM_RULES.md` - 데미지 계산 규칙
2. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\HEALTH_SYSTEM_RULES.md` - 체력 시스템 규칙
3. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\CRITICAL_SYSTEM_RULES.md` - 크리티컬 시스템 규칙
4. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ATTACK_SPEED_SYSTEM_RULES.md` - 공격속도 시스템 규칙
5. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\EVASION_SYSTEM_RULES.md` - 회피 시스템 규칙
6. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ARMOR_BLOCK_SYSTEM_RULES.md` - 방어/블록 시스템 규칙
7. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\CONFIG_MANAGEMENT_RULES.md` - 설정 관리 규칙
8. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\UI_SYSTEM_RULES.md` - UI 시스템 규칙
9. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\EITR_STAGGER_SYSTEM_RULES.md` - Eitr/스태거 시스템 규칙
10. `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ZNETSCENE_VFX_RULES.md` - ZNetScene VFX 규칙

## 적용 지침

1. 각 파일을 Read 도구로 읽습니다
2. 규칙들을 현재 작업 컨텍스트에 적용합니다
3. 스킬 개발 시 이 규칙들을 준수합니다
4. 로드 완료 후 간단히 요약을 출력합니다

## 사용 예시

```
/cst-core
```

핵심 시스템 규칙이 로드되면 데미지, 체력, 크리티컬, 공격속도, 회피, 방어, 설정, UI, Eitr, VFX 관련 개발 시 올바른 패턴을 적용할 수 있습니다.

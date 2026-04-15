---
name: cst-valheim-api
description: Use when looking up Valheim API methods, classes, or game systems. Triggers: Valheim API, ZDO, Humanoid, Player API, Character, 발헤임 API, 클래스 참조
---

## 핵심 규칙 요약

- **541개 클래스** 주요 API 목록 (dnSpy 추출, assembly_valheim.dll)
- 주요 시스템: Player, Character, Humanoid, ZDO, ZNetScene, ItemDrop, Skills
- `Player.GetLocalPlayer()` - 로컬 플레이어 획득
- 코드 분석용 DLL 디렉토리: `C:\home\ssunyme\.npm-global\bin\valheim_dll_api`
- Harmony 패치 시 클래스 계층 확인 필수 (Player → Humanoid → Character)

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\valheim_all_api.md`

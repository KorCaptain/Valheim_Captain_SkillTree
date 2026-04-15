---
name: cst-core-protect
description: Use when about to modify Plugin.cs or SkillTreeInputListener.cs. Triggers: Plugin.cs, SkillTreeInputListener, 수정 금지, core protection, InventoryShowPatch
---

## 핵심 규칙 요약

- **절대 수정 금지**: `Plugin.cs`와 `SkillTreeInputListener.cs` 전체
- Plugin.cs 보호 영역: `InventoryShowPatch`, `ShowSkillTreeIcon`, `TryCreateMMOStyleIcon`, AssetBundle 함수들
- SkillTreeInputListener.cs 보호 영역: 전체 파일 (UI 토글 및 ESC키 처리)
- SkillTreeData.cs: `RegisterAll()`의 6개 루트 노드 수정 금지
- 새 기능은 별도 파일/클래스로 분리하여 구현

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\CORE_PROTECTION_README.md`

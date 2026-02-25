---
name: cst-prod-text
description: Use when implementing production/farming skills with damage text display. Triggers: production, farming, DamageText, WorldTextInstance, 생산 스킬, 채집, 생산 텍스트
---

## 핵심 규칙 요약

- 생산 스킬 효과 발동 시 `DamageText.WorldTextInstance`로 캐릭터 머리 위 텍스트 표시
- MMO 방식 활용: `EpicMMOSystem`의 DamageText 시스템 직접 사용
- ❌ 단순 `MessageHud` 방식 금지 (불안정)
- ❌ `DamageText.ShowText()` 직접 호출 금지
- 생산 스킬 효과: 채집량 증가, 자원 획득 확률 등

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\PRODUCTION_DAMAGE_TEXT_IMPLEMENTATION.md`

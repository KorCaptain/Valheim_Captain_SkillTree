---
name: cst-critical
description: Use when implementing critical hit probability or damage skills. Triggers: critical, crit, CriticalHit, 크리티컬, 치명타, 치명타 확률
---

## 핵심 규칙 요약

- **공통 보너스 + 무기별 보너스 = 최종 치명타 효과** (중앙화된 시스템)
- 치명타 확률: `CriticalHitManager.GetCritChance()` 통해 계산 (직접 패치 금지)
- 치명타 피해: `CriticalHitManager.GetCritDamageMultiplier()` 활용
- 무기별 크리티컬 스킬도 반드시 `CriticalHitManager`를 통해 합산 등록
- `SkillBonusCalculator.CalculateTotal()` 사용 - 서로 다른 트리의 동일 효과 누적 합산 필수

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\CRITICAL_SYSTEM_RULES.md`

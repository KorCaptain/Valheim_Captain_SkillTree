---
name: cst-armor-block
description: Use when implementing armor or shield block power skills. Triggers: armor, block power, GetBodyArmor, BlockAttack, 방어력, 갑옷, 블록, 방패
---

## 핵심 규칙 요약

- **방어력(Armor) vs 방패 방어력(Block Power) 명확히 구분**
  - 방어력: `Character.GetBodyArmor()` Postfix 패치 - 갑옷에서 제공하는 피해 감소
  - 방패 방어력: `ItemDrop.ItemData.GetBlockPower()` 패치 - 방패로 막을 때 흡수량
- 방어력 보너스: 최종값에 Postfix로 합산 (`__result += bonus`)
- 블록 파워 보너스: 방패 타입 무기에만 적용 (`WeaponHelper.IsUsingShield()` 확인 필수)
- 두 시스템은 독립적 - 방어력 보너스가 블록 파워에 영향 없음

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ARMOR_BLOCK_SYSTEM_RULES.md`

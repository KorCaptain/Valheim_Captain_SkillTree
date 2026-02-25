---
name: cst-armor-block
description: Use when implementing armor or shield block power skills, or armor tooltip display. Triggers: armor, block power, GetBodyArmor, BlockAttack, 방어력, 갑옷, 블록, 방패, tooltip, 툴팁, $item_armor, $item_blockarmor
---

## 핵심 규칙 요약

- **방어력(Armor) vs 방패 방어력(Block Power) 명확히 구분**
  - 방어력: `Character.GetBodyArmor()` Postfix 패치
  - 방패 방어력: `ItemDrop.ItemData.GetBlockPower()` 패치
- **툴팁 표시**: `GetTooltip()` Postfix → 반환값은 **미번역 원시 텍스트**
  - 방패: `$item_blockarmor` 키로 감지 ("가드 방어력" 사용 금지!)
  - 방어구: `$item_armor` 키로 감지
  - 색상: 총합=`<color=orange>`, 기본=`<color=white>`, 보너스=`<color=#4FC3F7>`

**전체 문서**:
- 시스템 규칙: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ARMOR_BLOCK_SYSTEM_RULES.md`
- 툴팁 표시: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\ARMOR_TOOLTIP_DISPLAY_RULES.md`

# active_skill_ui.md — 액티브 스킬 노드 UI 색상 규칙

**파일**: `Gui/SkillTreeNodeUI.cs`  
**적용 대상**: 스킬트리 패널 내 액티브 스킬 노드 아이콘  
**최초 적용**: v1.25.06 (2026-06-23)

---

## 개요

스킬트리 UI에서 액티브 스킬 노드(키 바인딩 스킬)는 패시브 노드와 구별되도록
**색상으로 습득 상태를 표시**한다.

| 상태 | 색상 | Color 값 |
|------|------|----------|
| 미습득 (락) | 흐린 파란색 | `new Color(0.5f, 0.65f, 1f, 0.5f)` |
| 습득 (언락) | 황금색 | `new Color(1f, 0.84f, 0f, 1f)` |

패시브/무기전문가/직업 아이콘 노드는 기존과 동일하게 흰색(alpha 차이)으로 유지.

---

## 액티브 스킬 노드 ID 목록

### R키 (원거리 액티브)
| 노드 ID | 스킬명 |
|---------|--------|
| `crossbow_Step6_expert` | 석궁 단 한 발 |
| `bow_Step6_critboost` | 활 폭발화살 |
| `staff_Step6_dual_cast` | 지팡이 이중시전 |

### G키 (근접 액티브)
| 노드 ID | 스킬명 |
|---------|--------|
| `sword_step5_finalcut` | 검 돌진 연속 베기 (Lv2 업그레이드) |
| `sword_slash` | 검 돌진 베기 |
| `knife_step9_assassin_heart` | 단검 암살자의 심장 |
| `spear_Step5_penetrate` | 창 꿰뚫는 창 |
| `polearm_step5_king` | 폴암 관통돌격 |
| `mace_Step7_guardian_heart` | 둔기 방패돌진 |
| `defense_Step6_mind` | 방어 마인드쉴드 |

### H키 (보조 액티브)
| 노드 ID | 스킬명 |
|---------|--------|
| `crossbow_ice_breath` | 석궁 발칸 아이스 |
| `bow_Step6_arrow_rain` | 활 화살비 |
| `sword_step5_defswitch` | 검 회오리베기 |
| `spear_Step5_combo` | 창 연공창 |
| `mace_Step7_fury_hammer` | 둔기 분노의 망치 |
| `staff_Step6_heal` | 지팡이 범위 힐 |
| `knife_step10_stack_explosion` | 단검 약점폭발 |

### Mouse2 (홀드 액티브)
| 노드 ID | 스킬명 |
|---------|--------|
| `polearm_step6_whirlwind` | 폴암 휠윈드 |

---

## 구현 위치

### `SkillTreeNodeUI.cs` — 클래스 필드

```csharp
private static readonly HashSet<string> ActiveSkillNodeIds = new HashSet<string>
{
    // R키
    "crossbow_Step6_expert", "bow_Step6_critboost", "staff_Step6_dual_cast",
    // G키
    "sword_step5_finalcut", "sword_slash", "knife_step9_assassin_heart",
    "spear_Step5_penetrate", "polearm_step5_king", "mace_Step7_guardian_heart",
    "defense_Step6_mind",
    // H키
    "crossbow_ice_breath", "bow_Step6_arrow_rain", "sword_step5_defswitch",
    "spear_Step5_combo", "mace_Step7_fury_hammer", "staff_Step6_heal",
    "knife_step10_stack_explosion",
    // Mouse2
    "polearm_step6_whirlwind"
};
```

### `GenerateSkillTreeNodesAndLines()` — 초기 생성 시

```csharp
bool isActiveSkill = ActiveSkillNodeIds.Contains(node.Id);
// 초기 락 색상: 액티브 스킬=흐린 블루, 그 외=50% 흰색
img.color = isActiveSkill ? new Color(0.5f, 0.65f, 1f, 0.5f) : new Color(1f, 1f, 1f, 0.5f);
```

### `RefreshNodeStates()` — 상태 갱신 시

```csharp
bool isActiveSkill = ActiveSkillNodeIds.Contains(node.Id);

// 언락 상태
img.color = isActiveSkill ? new Color(1f, 0.84f, 0f, 1f) : new Color(1f, 1f, 1f, 1f);

// 락 상태
img.color = isActiveSkill ? new Color(0.5f, 0.65f, 1f, 0.5f) : new Color(1f, 1f, 1f, 0.5f);
```

---

## 색상 제외 대상

아래 노드 타입은 이 규칙에서 제외되며 기존 흰색 처리 유지:

- **직업 아이콘**: Berserker, Tanker, Rogue, Archer, Mage, Paladin  
  → 이미 고유 아트워크 보유, `isJobIconOrForced` 분기에서 처리
- **무기 전문가 루트**: sword, bow, mace, dagger, spear, polearm, crossbow, staff  
  → `isWeapon` 분기에서 처리
- **패시브 스킬 노드**: 전체 (위 두 그룹 제외 일반 노드)  
  → 흰색 알파 차이로 구분 유지

---

## 신규 액티브 스킬 추가 시

1. `SkillEffect.ActiveSkills.cs`에서 해당 키 핸들러(`HandleRKeySkills` 등)에 노드 ID 추가
2. `ActiveSkillNodeIds` HashSet에 동일 노드 ID 추가
3. 이 문서의 노드 ID 목록 업데이트

---

## 관련 문서

- [UI_SYSTEM_RULES.md](UI_SYSTEM_RULES.md) — 노드 크기 규칙 (Rule 10)
- [ACTIVE_SKILL_SYSTEM.md](ACTIVE_SKILL_SYSTEM.md) — 액티브 스킬 제한 시스템
- [ACTIVE_SKILL_HUD_RULES.md](ACTIVE_SKILL_HUD_RULES.md) — HUD 슬롯 UI 규칙

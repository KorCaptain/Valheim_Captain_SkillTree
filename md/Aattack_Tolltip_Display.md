# 어택.디스플레이.md - 무기 아이템 툴팁 스킬 효과 표시 규칙

## 📋 1. 개요

### 배경
방어구 툴팁 시스템(`SkillEffect.ArmorTooltip.cs`)이 성공적으로 구현됨.
무기 아이템에도 동일한 방식으로 스킬 보너스를 표시해야 함.

### 목표
무기에 마우스 오버 시 스킬트리 효과를 아이템 툴팁에 통합 표시.

**표시 순서 (발헤임 기본 툴팁 라인 순서)**:
1. 데미지 (물리/속성)
2. 공격속도
3. 치명타 확률
4. 치명타 피해 배율

**구현 파일**: `SkillTree/SkillEffect.WeaponTooltip.cs`
**참조 파일**: `md/ARMOR_TOOLTIP_DISPLAY_RULES.md` (동일 패턴 적용)

---

## 🗡️ 2. 지원 무기 10종 및 데미지 타입

| 무기 | WeaponType | 물리 데미지 타입 | 속성 데미지 타입 | 스킬 타입 |
|------|-----------|----------------|----------------|----------|
| 단검 | Knife | `m_pierce` + `m_slash` | - | `SkillType.Knives` |
| 주먹/클로 | Fist | `m_blunt` (주먹) / `m_pierce`+`m_slash` (클로) | - | `SkillType.Unarmed` |
| 검 | Sword | `m_slash` | - | `SkillType.Swords` |
| 둔기 | Mace | `m_blunt` | - | `SkillType.Clubs` |
| 창 | Spear | `m_pierce` | - | `SkillType.Spears` |
| 폴암 | Polearm | `m_pierce` + `m_slash` | - | `SkillType.Polearms` |
| 활 | Bow | `m_pierce` | - | `SkillType.Bows` |
| 석궁 | Crossbow | `m_pierce` | - | `SkillType.Crossbows` |
| 지팡이 | Staff | - | `m_fire` / `m_frost` / `m_lightning` | `SkillType.ElementalMagic` |
| 완드 | Wand | - | `m_blunt` / `m_pierce` / `m_spirit` / `m_poison` | `SkillType.Elementalmagic` |

### 분류 특이사항

**주먹/클로 (Fist)**: `SkillType.Unarmed`이지만 **단검 트리에서 인식**됨.
- `WeaponHelper.IsUsingKnife()` 내부에서 프리팹명 `"Fist"` / `"fist"` / `"Claw"` / `"claw"` 포함 시 단검으로 처리
- 툴팁 보너스도 단검 트리 스킬(Knife_Config) 적용
- 순수 맨손(`WeaponHelper.IsUsingUnarmed()`)은 별도 처리 가능

**완드 (Wand)**: `SkillType.Elementalmagic`.
- `WeaponHelper.IsUsingStaffOrWand()`로 지팡이(`ElementalMagic`)와 묶여 처리됨
- 별도 Wand_Config 없음 → Staff_Config + Attack_Config 공통 보너스 적용
- 데미지 타입은 완드 종류에 따라 다름 (주로 관통/블런트/영혼/독)

### 발헤임 GetTooltip 원시 데미지 라인

```
$item_damage: <color=orange>45</color> <color=yellow>(72)</color>   ← 물리 단일
$item_damages: <color=orange>45 slash, 12 pierce</color>            ← 복합 물리
$item_damage: <color=orange>30 fire</color>                          ← 속성
```

> **핵심**: 감지 키는 반드시 `$item_damage` (원시 텍스트). 한국어/영어 번역문 감지 절대 금지.

---

## 📊 3. 표시 항목 4가지 정의

### 3-1. 데미지 보너스

**표시 대상**: 스킬로 인한 **물리/속성 데미지 % 증가** 합산값
**라인 감지 키**: `$item_damage`
**표시 위치**: 기존 데미지 라인 교체

```
데미지: [주황]총합값 [회색]([흰색]기본값 [회색]* [파랑]+XX%[회색])
```

### 3-2. 공격속도 보너스

**표시 대상**: 스킬로 인한 **공격속도 % 증가** 합산값
**라인 감지 키**: `$item_attackforce` 또는 신규 라인 추가
**표시 위치**: 데미지 라인 하단에 **신규 라인 삽입**

```
공격속도: <color=#4FC3F7>+XX%</color>    (스킬 보너스 있을 때만 표시)
```

### 3-3. 치명타 확률 보너스

**표시 대상**: 스킬로 인한 **치명타 확률 % 증가** 합산값
**라인 감지 키**: `$item_staggerdamage` 또는 신규 라인 추가
**표시 위치**: 공격속도 하단에 **신규 라인 삽입**

```
치명타 확률: <color=#4FC3F7>+XX%</color>    (스킬 보너스 있을 때만 표시)
```

### 3-4. 치명타 피해 배율 보너스

**표시 대상**: 스킬로 인한 **치명타 피해 배율 % 증가** 합산값
**표시 위치**: 치명타 확률 하단에 **신규 라인 삽입**

```
치명타 피해: <color=#4FC3F7>+XX%</color>    (스킬 보너스 있을 때만 표시)
```

---

## 🎨 4. 색상 규칙

ARMOR_TOOLTIP과 동일한 색상 상수 사용:

```csharp
private const string COL_TOTAL = "orange";    // 총합: 발헤임 기본 값 색상
private const string COL_BASE  = "white";     // 기본 수치: 흰색
private const string COL_BONUS = "#4FC3F7";   // 스킬 보너스: 파란색
private const string COL_GRAY  = "#808080";   // 괄호/연산자: 회색
```

### 색상 적용 원칙

| 값 유형 | 색상 | 예시 |
|---------|------|------|
| 최종 총합 | `orange` | 최종 데미지 수치 |
| 기본 수치 | `white` | 무기 원본 데미지 |
| 스킬 보너스 | `#4FC3F7` (파랑) | +15% 등 스킬 기여분 |
| 연산자/괄호 | `#808080` (회색) | `(`, `)`, `*`, `+` |

---

## 📝 5. 표시 포맷 예시 (BuildDamageLine 4케이스)

### Case 1: 스킬 보너스 없음
```
데미지: <color=orange>45</color>
```
→ 기본 발헤임 표시 유지, 교체 불필요.

### Case 2: 고정값 데미지 보너스만
```
데미지: <color=orange>58</color> <color=#808080>(<color=white>45</color> <color=#808080>+</color> <color=#4FC3F7>13</color><color=#808080>)</color>
```

### Case 3: % 데미지 보너스만
```
데미지: <color=orange>51</color> <color=#808080>(<color=white>45</color> <color=#808080>*</color> <color=#4FC3F7>+15%</color><color=#808080>)</color>
```

### Case 4: 고정값 + % 보너스 (둘 다)
```
데미지: <color=orange>65</color> <color=#808080>((<color=white>45</color> <color=#808080>+</color> <color=#4FC3F7>12</color><color=#808080>) *</color> <color=#4FC3F7>+15%</color><color=#808080>)</color>
```

### 공격속도/치명타 신규 라인 (보너스 있을 때만)
```
공격속도: <color=#4FC3F7>+12%</color>
치명타 확률: <color=#4FC3F7>+8%</color>
치명타 피해: <color=#4FC3F7>+25%</color>
```

---

## 📋 6. 무기별 스킬 ID → Config 매핑 표

### 공통 (전 무기 적용)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `attack_root` | `Attack_Config.AttackRootDamageBonus` | 공격 전문가 데미지 | 데미지% |
| `atk_base_physical` | `Attack_Config.AttackBasePhysicalDamage` | 기본 물리 강화 | 데미지% |
| `atk_crit_chance` | `Attack_Config.AttackCritChance` | 치명타 확률 | 치명타확률% |
| `atk_critdmg_bonus` | `Attack_Config.AttackCritDamageBonus` | 치명타 피해 | 치명타피해% |

### 속성 무기 공통 (지팡이 + 완드)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `atk_base_elemental` | `Attack_Config.AttackBaseElementalDamage` | 속성 강화 | 데미지% |
| `atk_staff_elemental` | `Attack_Config.AttackStaffElemental` | 지팡이/완드 속성 마스터 | 데미지% |

### 단검 (Knife)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `knife_expert_backstab` | `Knife_Config.KnifeExpertBackstabBonus` | 전문가 기습 | 데미지% |
| `knife_combat_damage` | `Knife_Config.KnifeCombatDamageBonus` | 전투 데미지 | 데미지% |
| `knife_execution_crit` | `Knife_Config.KnifeExecutionCritChance` | 처형 치명타 | 치명타확률% |
| `knife_execution_critdmg` | `Knife_Config.KnifeExecutionCritDamage` | 처형 치명타 피해 | 치명타피해% |
| `knife_attack_speed` | `Knife_Config.KnifeAttackSpeedRequiredPoints` | 공격속도 | 공격속도% |

### 주먹/클로 (Fist)

> **단검 트리에 통합**: `WeaponHelper.IsUsingKnife()`가 프리팹명 `"Fist"/"fist"/"Claw"/"claw"` 포함 시 단검으로 인식.
> 따라서 Fist 무기는 **Knife_Config 스킬 보너스를 그대로 적용**.

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 | 비고 |
|---------|---------------|------|------|------|
| `knife_expert_backstab` | `Knife_Config.KnifeExpertBackstabBonus` | 기습 데미지 | 데미지% | 단검 공유 |
| `knife_combat_damage` | `Knife_Config.KnifeCombatDamageBonus` | 전투 데미지 | 데미지% | 단검 공유 |
| `knife_execution_crit` | `Knife_Config.KnifeExecutionCritChance` | 처형 치명타 | 치명타확률% | 단검 공유 |
| `knife_execution_critdmg` | `Knife_Config.KnifeExecutionCritDamage` | 처형 치명타 피해 | 치명타피해% | 단검 공유 |
| `atk_melee_enhance` | `Attack_Config.AttackMeleeEnhancement` | 근접 강화 | 데미지% | 공통 |

**무기 타입 감지 코드**:
```csharp
// Fist 무기 판별 (WeaponHelper 활용)
bool isFist = item.m_shared.m_skillType == Skills.SkillType.Unarmed
           || (item.m_dropPrefab?.name ?? "").ToLower().Contains("fist")
           || (item.m_dropPrefab?.name ?? "").ToLower().Contains("claw");
```

### 검 (Sword)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `sword_expert_damage` | `Sword_Config.SwordExpertDamageBonus` | 전문가 데미지 | 데미지% |
| `sword_Step1_fastslash` | `Sword_Config.SwordStep1FastSlashSpeed` | 빠른 베기 속도 | 공격속도% |
| `sword_Step2_comboslash` | `Sword_Config.SwordStep2ComboSlashBonus` | 연속 베기 | 데미지% |
| `atk_melee_enhance` | `Attack_Config.AttackMeleeEnhancement` | 근접 강화 | 데미지% |

### 둔기 (Mace)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `mace_expert_damage` | `Mace_Config` 참조 | 전문가 데미지 | 데미지% |
| `atk_twohand_crush` | `Attack_Config.AttackTwoHandedBonus` | 양손 분쇄 | 데미지% |

### 창 (Spear)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `spear_expert_damage` | `Spear_Config` 참조 | 전문가 데미지 | 데미지% |
| `atk_melee_enhance` | `Attack_Config.AttackMeleeEnhancement` | 근접 강화 | 데미지% |

### 폴암 (Polearm)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `polearm_expert_damage` | `Polearm_Config` 참조 | 전문가 데미지 | 데미지% |
| `atk_twohand_crush` | `Attack_Config.AttackTwoHandedBonus` | 양손 분쇄 | 데미지% |

### 활 (Bow)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `bow_expert_damage` | `Bow_Config.BowStep1ExpertDamageBonus` | 전문가 데미지 | 데미지% |
| `bow_Step2_focuscrit` | `Bow_Config.BowStep2FocusCritBonus` | 집중사격 치명타 | 치명타확률% |
| `bow_Step5_mastercrit` | `Bow_Config.BowStep5MasterCritDamage` | 마스터 치명타 피해 | 치명타피해% |
| `atk_ranged_enhance` | `Attack_Config.AttackRangedEnhancement` | 원거리 강화 | 데미지% |

### 석궁 (Crossbow)

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 |
|---------|---------------|------|------|
| `crossbow_expert_damage` | `Crossbow_Config` 참조 | 전문가 데미지 | 데미지% |
| `atk_ranged_enhance` | `Attack_Config.AttackRangedEnhancement` | 원거리 강화 | 데미지% |

### 완드 (Wand)

> **지팡이 트리에 통합**: `WeaponHelper.IsUsingStaffOrWand()`가 `Elementalmagic` 타입을 포함하여 처리.
> 별도 Wand_Config 없음 → **Staff_Config + Attack_Config 공통 보너스** 적용.

| 스킬 ID | Config 프로퍼티 | 효과 | 타입 | 비고 |
|---------|---------------|------|------|------|
| `staff_expert_damage` | `Staff_Config.StaffExpertDamage` | 전문가 데미지 | 데미지% | 지팡이 공유 |
| `atk_base_elemental` | `Attack_Config.AttackBaseElementalDamage` | 속성 강화 | 데미지% | 공통 |
| `atk_staff_elemental` | `Attack_Config.AttackStaffElemental` | 속성 마스터 | 데미지% | 지팡이 공유 |
| `atk_crit_chance` | `Attack_Config.AttackCritChance` | 치명타 확률 | 치명타확률% | 공통 |
| `atk_critdmg_bonus` | `Attack_Config.AttackCritDamageBonus` | 치명타 피해 | 치명타피해% | 공통 |

**완드 데미지 타입 특이사항**:
```
완드 종류별 데미지 타입:
- 피의 마법봉: m_blunt (기본) + m_spirit (특수)
- 독 완드:     m_pierce + m_poison
→ rawBase 계산 시 m_shared.m_damages 전체 합산 필요
```

**무기 타입 감지 코드**:
```csharp
// 완드 판별 (Elementalmagic 타입)
bool isWand = item.m_shared.m_skillType == Skills.SkillType.Elementalmagic;
// 지팡이+완드 통합 판별
bool isStaffOrWand = WeaponHelper.IsUsingStaffOrWand(player);
```

---

## 🏗️ 7. 구현 파일 구조 (WeaponTooltip.cs 클래스 설계)

**파일 경로**: `CaptainSkillTree/SkillTree/SkillEffect.WeaponTooltip.cs`
**예상 라인 수**: 350~450줄 (800줄 제한 이내)

```csharp
namespace CaptainSkillTree.SkillTree
{
    public static partial class SkillEffect
    {
        // ─────────────────────────────────────────
        // 색상 상수 (ARMOR와 동일)
        // ─────────────────────────────────────────
        // (중복 선언 주의 - partial class이므로 ArmorTooltip에 이미 선언된 경우 제거)

        // ─────────────────────────────────────────
        // 내부 헬퍼: 무기 타입 분류
        // ─────────────────────────────────────────
        private static bool IsMeleeWeapon(ItemDrop.ItemData item) { ... }
        private static bool IsRangedWeapon(ItemDrop.ItemData item) { ... }
        private static bool IsStaffWeapon(ItemDrop.ItemData item) { ... }
        // Fist: Unarmed 타입 OR 프리팹명에 "fist"/"claw" 포함
        private static bool IsFistWeapon(ItemDrop.ItemData item) { ... }
        // Wand: Elementalmagic 타입 (IsUsingStaffOrWand와 동일 로직 적용)
        private static bool IsWandWeapon(ItemDrop.ItemData item) { ... }

        // ─────────────────────────────────────────
        // 내부 헬퍼: 스킬 보너스 집계
        // ─────────────────────────────────────────
        private static float GetDamageBonus(ItemDrop.ItemData item) { ... }
        private static float GetAttackSpeedBonus(ItemDrop.ItemData item) { ... }
        private static float GetCritChanceBonus(ItemDrop.ItemData item) { ... }
        private static float GetCritDamageBonus(ItemDrop.ItemData item) { ... }

        // ─────────────────────────────────────────
        // 내부 헬퍼: 라인 포맷 빌더
        // ─────────────────────────────────────────
        private static string BuildDamageLine(string label, float rawBase, float bonusPct) { ... }
        private static string BuildBonusLine(string label, float bonusPct) { ... }

        // ─────────────────────────────────────────
        // Harmony Patch 클래스 (아래 섹션 8 참조)
        // ─────────────────────────────────────────
    }
}
```

---

## 🔧 8. Harmony Patch 코드 골격

```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
    new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
public static class ItemData_GetTooltip_WeaponBonus_Patch
{
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Low)]
    private static void Postfix(ItemDrop.ItemData item, int qualityLevel, bool crafting,
        float worldLevel, int stackOverride, ref string __result)
    {
        try
        {
            // 1. 무기 타입 확인 (비무기 아이템 조기 종료)
            if (!IsWeaponItem(item)) return;

            // 2. 스킬 보너스 집계
            float dmgBonus  = GetDamageBonus(item);
            float spdBonus  = GetAttackSpeedBonus(item);
            float critChance = GetCritChanceBonus(item);
            float critDmg   = GetCritDamageBonus(item);

            // 3. 모든 보너스가 0이면 조기 종료
            if (dmgBonus == 0f && spdBonus == 0f && critChance == 0f && critDmg == 0f)
                return;

            // 4. 라인 분리 후 교체/삽입
            string[] lines = __result.Split('\n');
            var result = new System.Text.StringBuilder();
            bool insertedExtras = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("$item_damage") && dmgBonus > 0f)
                {
                    // 데미지 라인 교체
                    float rawBase = GetRawDamage(item, qualityLevel, worldLevel);
                    result.AppendLine(BuildDamageLine(lines[i], rawBase, dmgBonus));
                }
                else
                {
                    result.AppendLine(lines[i]);
                }

                // 데미지 라인 직후 공속/치명타 삽입 (1회만)
                if (!insertedExtras && lines[i].Contains("$item_damage"))
                {
                    if (spdBonus > 0f)   result.AppendLine(BuildBonusLine("$cst_attack_speed",  spdBonus));
                    if (critChance > 0f) result.AppendLine(BuildBonusLine("$cst_crit_chance",   critChance));
                    if (critDmg > 0f)    result.AppendLine(BuildBonusLine("$cst_crit_damage",   critDmg));
                    insertedExtras = true;
                }
            }

            __result = result.ToString().TrimEnd();
        }
        catch (System.Exception ex)
        {
            Plugin.Log.LogError($"[무기 툴팁] 패치 오류: {ex.Message}");
        }
    }
}
```

> **주의**: `GetTooltip`은 **static 메서드** → Postfix 첫 파라미터가 `item` (인스턴스 아님). ARMOR_TOOLTIP과 동일.

---

## 🗑️ 9. 수정/삭제 대상 파일

구현 전 기존 인라인 툴팁 코드를 제거하거나 이관해야 함.

### SkillEffect.AttackTree.cs (Line 457~끝)

```
// 근접 전문가 효과 패치는 SkillEffect.MeleeSkills.cs로 이동됨
// MMO 방식에 맞게 공격 상태 추적 시스템 제거
// 스킬 효과는 MMO getParameter 패치를 통해 구현
```
→ 현재는 주석만 존재. WeaponTooltip.cs 구현 후 이 파일에 패치 클래스 추가 불필요.

### SkillEffect.RangedSkills.cs (파일 끝 확인)

현재 848줄. 원거리 툴팁 관련 인라인 코드가 있다면 WeaponTooltip.cs로 이관.
**확인 방법**: `$item_damage` 문자열 grep 후 인라인 툴팁 여부 검사.

```bash
grep -n "item_damage\|GetTooltip\|tooltip" CaptainSkillTree/SkillTree/SkillEffect.RangedSkills.cs
```

---

## ⚠️ 10. 구현 주의사항 5개

### ① 감지 키는 반드시 원시 텍스트

```csharp
// ✅ 올바름
if (lines[i].Contains("$item_damage")) { ... }

// ❌ 금지 - 번역 후 문자열
if (lines[i].Contains("데미지")) { ... }
if (lines[i].Contains("Damage")) { ... }
```

### ② rawBase는 패치 전 순수 값으로 계산

```csharp
// ✅ 올바름: m_shared 직접 계산 (패치 미적용 상태)
float rawDamage = item.m_shared.m_damages.m_slash
                + item.m_shared.m_damages.m_pierce
                + item.m_shared.m_damagePerLevel.m_slash * (qualityLevel - 1);

// ❌ 금지: GetDamage() 호출 → 이미 스킬 패치 적용된 값 반환 → 이중 계산
```

### ③ partial class 중복 선언 주의

`SkillEffect.ArmorTooltip.cs`에 이미 색상 상수(`COL_TOTAL` 등)가 선언되어 있으면 WeaponTooltip.cs에서 **재선언하지 않음**.
→ 동일 `partial class SkillEffect` 내에서 공유됨.

### ④ 신규 라인 로컬라이제이션

`$cst_attack_speed`, `$cst_crit_chance`, `$cst_crit_damage` 등 신규 라인 키를 사용할 경우:
- `DefaultLanguages.cs`에 KO + EN 동시 등록 필수
- 또는 로컬라이제이션 없이 한국어 하드코딩 대신 **영어 키를 그대로 표시**하는 방식도 허용 (ARMOR_TOOLTIP 방식)

### ⑤ 효과 누적 합산 규칙 준수

```csharp
// ✅ 올바름 - SkillBonusCalculator 사용
float dmgBonus = SkillBonusCalculator.CalculateTotal(
    ("attack_root",       () => Attack_Config.AttackRootDamageBonusValue),
    ("sword_expert_damage", () => Sword_Config.SwordExpertDamageBonusValue),
    ("atk_base_physical", () => Attack_Config.AttackBasePhysicalDamageValue)
);

// ❌ 금지 - 첫 번째 스킬만 반환
if (HasSkill("attack_root")) return Attack_Config.AttackRootDamageBonusValue;
```

---

## ✅ 11. 검증 방법

### 디버그 로그로 라인 확인

```csharp
// 임시 디버그 (문제 발생 시, 해결 후 반드시 제거)
Plugin.Log.LogInfo($"[무기 툴팁] 총 {lines.Length}개 라인, 무기: {item?.m_shared?.m_name}");
for (int j = 0; j < lines.Length; j++)
    Plugin.Log.LogInfo($"  [{j}] '{lines[j]}'");
```

> **주의**: `LogDebug`는 기본 설정에서 출력 안 됨. 진단 시 `LogInfo` 사용.

### 로그 위치
```
{발헤임 설치 폴더}/BepInEx/LogOutput.log
```

### 인게임 확인 체크리스트

```yaml
무기_툴팁_검증:
  감지:
    - [ ] $item_damage 키로 데미지 라인 감지 확인
    - [ ] 비무기 아이템(방어구 등)에서 패치 미작동 확인
    - [ ] 스킬 미보유 시 툴팁 변화 없음 확인
  데미지:
    - [ ] 스킬 보유 시 총합 수치 증가 반영
    - [ ] 괄호 내 기본값(흰색) + 보너스(파란색) 표시
  추가 라인:
    - [ ] 공격속도 보너스 있을 때만 라인 추가
    - [ ] 치명타 확률/피해 보너스 있을 때만 라인 추가
  누적:
    - [ ] 여러 트리 동시 보너스 합산 표시 확인
    - [ ] 이중 계산 없음 확인 (rawBase vs GetDamage)
```

---

## 📦 12. 파일 크기 예상표

| 파일 | 예상 줄 수 | 역할 |
|------|-----------|------|
| `SkillEffect.WeaponTooltip.cs` | 350~450줄 | 메인 구현 (800줄 제한 준수) |
| `SkillEffect.AttackTree.cs` | 변동 없음 | 기존 파일, 별도 수정 없음 |
| `SkillEffect.RangedSkills.cs` | 변동 없음 | 인라인 툴팁 코드 있으면 이관 |

> 450줄 초과 시 무기 타입별로 분리:
> - `SkillEffect.WeaponTooltip.Melee.cs` (단검/주먹클로/검/둔기/창/폴암)
> - `SkillEffect.WeaponTooltip.Ranged.cs` (활/석궁/지팡이/완드)

---

## 🔗 관련 문서

| 문서 | 내용 |
|------|------|
| `md/ARMOR_TOOLTIP_DISPLAY_RULES.md` | 방어구 툴팁 패턴 (이 문서의 기준) |
| `md/DAMAGE_SYSTEM_RULES.md` | 데미지 계산 시스템 |
| `md/CONFIG_GUIDE.md` | Config 키 규칙 |

# Job Skill Level Up — 직업 스킬 레벨업 참조 문서

> 작성일: 2026-03-16
> 대상: 아처(Archer) 직업 레벨업 구조 + 반복 혼동 패턴 정리

---

## 1. 직업 스킬 레벨업 방식

### 레벨업 흐름 (코드 경로)

```
확인 버튼 클릭 (SkillTreeUI.cs:1192)
  → mgr.AddPendingInvestment("Archer")
  → mgr.ConfirmInvestments()          ← SkillTreeManager.cs:1073
      → ConsumeArcherLevelItems(targetLevel)  ← 아처 전용 아이템 소모
      → SetSkillLevel(skillId, newLevel)
      → node.ApplyEffect?.Invoke(newLevel)
  → UpdateArcherTooltip()
  → StartCoroutine(PlayArcherLevelUpAnimation(nodeRect, targetLevel))
```

### 직업별 레벨업 차이

| 직업 | 최대 레벨 | 재료 소모 | 애니메이션 |
|------|-----------|-----------|------------|
| Archer | 5 | 레벨마다 다른 트로피 조합 | 특수 5초 4단계 |
| Berserker/Tanker/Rogue/Mage/Paladin | 1 | 초기 전직 1회만 | 없음 |

### 아처 레벨별 재료 (SkillTreeManager.cs:ConsumeArcherLevelItems)

| 목표 레벨 | 소모 재료 |
|-----------|----------|
| Lv1 | TrophyGreydwarfBrute x1 + TrophyEikthyr x1 |
| Lv2 | TrophyEikthyr x1 + TrophyTheElder x1 |
| Lv3 | TrophyHatchling x1 + TrophyTheElder x1 + TrophyBonemass x1 |
| Lv4 | TrophyAbomination x1 + TrophyBonemass x1 + TrophyDragonQueen x1 |
| Lv5 | TrophyDragonQueen x1 + TrophyGoblinKing x1 + TrophySeekerQueen x1 |

---

## 2. 이미지 시각 효과 구조

### PlayArcherLevelUpAnimation (SkillTreeUI.cs:1217)

- Lv2~5 업그레이드 시 호출
- `PlayFloatingLevelText` + `PlayJobIconSpecialEffect` **동시 재생**

```csharp
StartCoroutine(PlayFloatingLevelText(nodeRect, level));      // 비동기 시작 (동시)
yield return StartCoroutine(PlayJobIconSpecialEffect(nodeRect)); // 완료 대기
```

### PlayFloatingLevelText (SkillTreeUI.cs:1228)

- "LvN" 텍스트 (빨간색, 36pt Bold)
- 노드 위에서 시작 → 3초간 위로 150px 이동 + 페이드아웃

### PlayJobIconSpecialEffect 4단계 (SkillTreeUI.cs:2327)

| 단계 | 시간 | 내용 |
|------|------|------|
| 1. 준비 | 0.5초 | Sin 펄스 스케일, 오라 등장 |
| 2. 상승+회전 | 2.0초 | 원위치→화면중앙(EaseOutQuart), 1x→2.5x, 720° 회전 |
| 3. 절정 | 1.0초 | 파티클 폭발, 원본색→은빛→흰색→원본색 |
| 4. 복귀 | 1.5초 | EaseOutBounce 복귀, 최종 1.1x 크기 유지 |
| **합계** | **5초** | |

---

## 3. 툴팁 표시 방법

### GetArcherTooltip() 표시 블록 구조 (Archer_Tooltip.cs:36)

```
[제목]        황금색 #FFD700, 22pt
[메인블록]    mainLevel = currentLevel == 0 ? 1 : currentLevel
              Lv{mainLevel} : 효과 스탯
              패시브 스킬
              소모 / 스킬타입 / 쿨타임 / 필요조건 / 공지사항
[필요아이템]  displayLevel = currentLevel + 1  ← 다음 레벨 재료
[프리뷰]      mainLevel+1 ~ 5  ← 회색 #808080 14pt
```

> ⚠️ 메인블록은 **현재 레벨(mainLevel)** 기준, 필요아이템은 **다음 레벨(displayLevel)** 기준
> 이 둘을 혼동하면 "한 단계 앞서 표시" 버그가 발생함

### 색상 규칙

| 색상 | 코드 | 용도 |
|------|------|------|
| 황금색 | #FFD700 | 스킬명, 최대레벨 |
| 밝은회색 | #E0E0E0 | 현재 레벨 스탯 |
| 연초록 | #98FB98 | 레이블 (패시브, 필요조건) |
| 황금초록 | #ADFF2F | 패시브값, 스킬타입 |
| 주황색 | #FFB347 | 소모 레이블 |
| 파란색 | #1E90FF | 스킬타입 레이블 |
| 빨간색 | #FF6B6B | 필요아이템값 |
| 회색 | #808080 | 구분선, 프리뷰 |

---

## 4. Config 항목 리스트 (Archer_Config.cs)

총 25개 BindServerSync 키:

| 키 이름 | 기본값 | 용도 |
|---------|--------|------|
| Archer_MultiShot_ArrowCount | 5 | 화살 발사 수 |
| Archer_MultiShot_DamagePercent | 50.0 | 화살당 데미지% |
| Archer_MultiShot_Cooldown | 30.0 | 쿨타임(초) |
| Archer_MultiShot_Charges | 2 | 사용 횟수 |
| Archer_MultiShot_StaminaCost | 25.0 | 스태미나 소모 |
| Archer_JumpHeightBonus | 0.0 | Lv1 점프 높이% |
| Archer_FallDamageReduction | 0.0 | Lv1~2 낙사 감소% |
| Archer_Lv2_JumpHeightBonus | 10.0 | Lv2 점프 높이% |
| Archer_Lv3_JumpHeightBonus | 20.0 | Lv3 점프 높이% |
| Archer_Lv4_JumpHeightBonus | 20.0 | Lv4 점프 높이% |
| Archer_Lv5_JumpHeightBonus | 20.0 | Lv5 점프 높이% |
| Archer_Lv3_FallDamageReduction | 10.0 | Lv3 낙사 감소% |
| Archer_Lv4_FallDamageReduction | 20.0 | Lv4 낙사 감소% |
| Archer_Lv5_FallDamageReduction | 35.0 | Lv5 낙사 감소% |
| Archer_ElementalResistPerLevel | 10.0 | 레벨당 속성저항% |
| Archer_Lv2_BonusArrows | 1 | Lv2 추가 화살수 |
| Archer_Lv2_DamagePercent | 55.0 | Lv2 데미지% |
| Archer_Lv3_BonusArrows | 2 | Lv3 추가 화살수 |
| Archer_Lv3_DamagePercent | 60.0 | Lv3 데미지% |
| Archer_Lv4_BonusArrows | 3 | Lv4 추가 화살수 |
| Archer_Lv4_DamagePercent | 65.0 | Lv4 데미지% |
| Archer_Lv5_BonusArrows | 3 | Lv5 추가 화살수 |
| Archer_Lv5_DamagePercent | 65.0 | Lv5 데미지% |
| Archer_Lv5_BonusCharges | 1 | Lv5 추가 사용횟수 |
| Archer_Lv2_Cooldown | (값 확인 필요) | Lv2 쿨타임 |

### Config 규칙

- 모든 키: `GetConfigDescription("키이름")` 필수 (하드코딩 금지)
- 값 접근: `SkillTreeConfig.GetEffectiveValue()` 래퍼 사용 (MMO 연동)

---

## 5. 트로피 이름 불일치 문제

**3가지 컨텍스트에서 이름이 다름**:

| 트로피 | 커맨드용 프리팹명 | RemoveItem 호출 | L.Get 키 | 툴팁 표시 |
|--------|-----------------|----------------|---------|----------|
| 에이크쉬르 | TrophyEikthyr | `$item_trophy_eikthyr` | `item_eikthyr_trophy` ⚠️ | Eikthyr Trophy |
| 장로 | TrophyTheElder | `$item_trophy_elder` | `item_trophy_theelder` | The Elder Trophy |
| 부화 드레이크 | TrophyHatchling | `$item_trophy_hatchling` | `item_trophy_hatchling` | Drake Trophy |
| 본매스 | TrophyBonemass | `$item_trophy_bonemass` | `item_trophy_bonemass` | Bonemass Trophy |
| 어보미네이션 | TrophyAbomination | `$item_trophy_abomination` | `item_trophy_abomination` | Abomination Trophy |
| 모데르 | TrophyDragonQueen | `$item_trophy_dragonqueen` | `item_trophy_dragonqueen` | Moder Trophy |
| 야글루스 | TrophyGoblinKing | `$item_trophy_goblinking` | `item_trophy_goblinking` | Yagluth Trophy |
| 시커 퀸 | TrophySeekerQueen | `$item_trophy_seekerqueen` | `item_trophy_seekerqueen` | Seeker Queen Trophy |

### ⚠️ 알려진 불일치

1. **`item_eikthyr_trophy`** (Archer_Tooltip.cs:149,151) — DefaultLanguages에 **미정의**
   올바른 키: `item_trophy_eikthyr` (패턴: `item_trophy_{prefab소문자}`)

2. **`$item_trophy_elder`** vs `item_trophy_theelder` — 발헤임 실제 프리팹명은 `TrophyTheElder`
   RemoveItem은 `$item_trophy_theElder` 이어야 할 수 있음 (인게임 확인 필요)

### 프리팹명 확인 방법

```csharp
// 인게임에서 직접 확인 (BepInEx 콘솔)
foreach (var item in Player.m_localPlayer.GetInventory().GetAllItems())
    Debug.Log(item.m_shared.m_name);  // $item_trophy_xxx 형식 출력

// 또는 ZNetScene에서 프리팹 목록 확인
foreach (var prefab in ZNetScene.instance.m_prefabs)
    if (prefab.name.ToLower().Contains("trophy"))
        Debug.Log(prefab.name);
```

> 커맨드: `/give TrophyEikthyr` (프리팹명 그대로)
> RemoveItem: `inventory.RemoveItem("$item_trophy_eikthyr", 1)` (소문자 + $ 접두사)
> L.Get 키: `item_trophy_eikthyr` ($ 없음, DefaultLanguages.cs에 반드시 정의 필요)

---

## 6. 오늘 작업 내용 (2026-03-16)

### 버그 수정 1: 아처 패시브 누적 버그 (ArcherSkills.cs)

`GetArcherJumpHeightBonus()` / `GetArcherFallDamageReduction()` 함수:
- **이전**: `if (level >= N)` 조건을 중첩 → 모든 하위 레벨 보너스 누적 합산
- **수정**: `switch` 문으로 레벨별 단일값 반환

| | Lv5 수정 전 | Lv5 수정 후 |
|--|------------|------------|
| 점프 높이 | +70% (누적) | +20% (단일) |
| 낙사 감소 | -65% (누적) | -35% (단일) |

```csharp
// ✅ 수정 후 — switch 단일값 패턴
private float GetArcherJumpHeightBonus(int level) => level switch
{
    1 => 0f,
    2 => Config.Archer_Lv2_JumpHeightBonus,
    3 => Config.Archer_Lv3_JumpHeightBonus,
    4 => Config.Archer_Lv4_JumpHeightBonus,
    5 => Config.Archer_Lv5_JumpHeightBonus,
    _ => 0f
};
```

### 버그 수정 2: 툴팁 "한 단계 앞서 표시" 버그 (Archer_Tooltip.cs)

- `mainLevel = currentLevel == 0 ? 1 : currentLevel` 변수 추가
- 메인 블록 기준: `displayLevel(다음레벨)` → `mainLevel(현재레벨)` 으로 변경
- 필요아이템 기준: `displayLevel` 유지 (다음 레벨 재료 표시)
- 프리뷰 범위: `displayLevel+1~5` → `mainLevel+1~5`

### 기능 개선: 레벨업 시각 효과 동시 재생 (SkillTreeUI.cs)

`PlayArcherLevelUpAnimation()` 내부:

```csharp
// ❌ 이전 — 순차 재생 (텍스트 끝난 후 아이콘 효과 시작)
yield return StartCoroutine(PlayFloatingLevelText(nodeRect, level));
yield return StartCoroutine(PlayJobIconSpecialEffect(nodeRect));

// ✅ 수정 후 — 동시 재생
StartCoroutine(PlayFloatingLevelText(nodeRect, level));          // 비동기 시작
yield return StartCoroutine(PlayJobIconSpecialEffect(nodeRect)); // 완료 대기
```

결과: "LvN" 텍스트 플로팅 + 아이콘 중앙이동/720°회전이 동시에 재생됨

---

## 관련 문서

| 문서 | 내용 |
|------|------|
| `LOCALIZATION_GUIDE.md` | L.Get() 키 관리, 검증 스크립트 |
| `CONFIG_GUIDE.md` | Config 키 규칙, 3종 세트 |
| `ACTIVE_SKILL_SYSTEM.md` | 액티브 스킬 키 바인딩 |
| `QUICK_REFERENCE.md` | 빠른 참조 |

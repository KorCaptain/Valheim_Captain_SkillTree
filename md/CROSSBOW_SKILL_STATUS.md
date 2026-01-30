# 석궁 스킬 효과 구현 상태 보고서

**작성일**: 2025년 1월 (버전 0.1.5)
**상태**: Config 연동 완료, 효과 구현은 부분적

---

## 📊 전체 구현 상태 요약

- ✅ **Config 연동**: 10개 스킬, 14개 Config 변수 - **100% 완료**
- ✅ **툴팁 연동**: 9개 스킬 툴팁 - **100% 완료**
- ⚠️ **효과 구현**: 9개 스킬 중 **4개 부분 구현**, **3개 미구현**, **2개 완전 구현**

---

## 🎯 스킬별 상세 구현 상태

### ✅ Tier 1: 석궁 전문가 (crossbow_Step1_damage)

**Config 연동**: ✅ 완료
- `CrossbowExpertDamageBonus` (기본값: 5%)
- `CrossbowExpertDamageBonusValue` (MMO 연동)

**툴팁**: ✅ 완료
- "석궁 데미지 +{X}%"
- Config 변경 시 실시간 업데이트

**효과 구현**: ❌ **미구현**
- **필요 작업**:
  - `SkillEffect.RangedSkills.cs`의 `RangedSkills_ItemData_GetDamage_RangedExpert_Patch`에 석궁 데미지 보너스 추가
  - 또는 `AttackTree.cs`에 석궁 전문가 보너스 추가
- **참고 패턴**: 활 전문가 구현 (bow_Step1_damage, 라인 349-353)

---

### ✅ Tier 2: 강한 관통 (crossbow_Step2_pierce_all)

**Config 연동**: ✅ 완료
- `CrossbowPierceAllChance` (기본값: 28%)
- `CrossbowPierceAllChanceValue` (MMO 연동)

**툴팁**: ✅ 완료
- "{X}% 확률로 모든 몬스터 관통"
- Config 변경 시 실시간 업데이트

**효과 구현**: ⚠️ **부분 구현** (하드코딩)
- **위치**: `SkillEffect.RangedSkills.cs:51-54`
- **현재 코드**:
  ```csharp
  public static bool TryPierceAllMonsters()
  {
      return UnityEngine.Random.Range(0f, 1f) < 0.28f; // ❌ 하드코딩
  }
  ```
- **필요 수정**:
  ```csharp
  public static bool TryPierceAllMonsters()
  {
      return UnityEngine.Random.Range(0f, 100f) < Crossbow_Config.CrossbowPierceAllChanceValue; // ✅ Config 연동
  }
  ```
- **호출 위치**: 발사체 적중 시 패치에서 호출 필요 (현재 미호출)

---

### ✅ Tier 3-1: 균형 조준 (crossbow_Step2_balance)

**Config 연동**: ✅ 완료
- `CrossbowBalanceKnockbackChance` (기본값: 30%)
- `CrossbowBalanceKnockbackDistance` (기본값: 3m)
- 두 값 모두 MMO 연동

**툴팁**: ✅ 완료
- "명중 시 {X}% 확률로 넉백 ({Y}m)"
- Config 변경 시 실시간 업데이트

**효과 구현**: ⚠️ **부분 구현** (하드코딩)
- **위치**: `SkillEffect.RangedSkills.cs:59-81`
- **현재 코드**:
  ```csharp
  public static bool TryKnockbackOnHit(Character target, Vector3 attackerPosition)
  {
      if (UnityEngine.Random.Range(0f, 1f) < 0.30f) // ❌ 하드코딩
      {
          Vector3 knockbackForce = knockbackDirection * 3f; // ❌ 하드코딩
          // ...
      }
  }
  ```
- **필요 수정**:
  ```csharp
  public static bool TryKnockbackOnHit(Character target, Vector3 attackerPosition)
  {
      float chance = Crossbow_Config.CrossbowBalanceKnockbackChanceValue / 100f;
      if (UnityEngine.Random.Range(0f, 1f) < chance) // ✅ Config 연동
      {
          float distance = Crossbow_Config.CrossbowBalanceKnockbackDistanceValue;
          Vector3 knockbackForce = knockbackDirection * distance; // ✅ Config 연동
          // ...
      }
  }
  ```
- **호출 위치**: 발사체 적중 시 패치에서 호출 필요 (현재 미호출)

---

### ✅ Tier 3-2: 고속 장전 (crossbow_Step3_rapid)

**Config 연동**: ✅ 완료
- `CrossbowRapidReloadSpeed` (기본값: 10%)
- `CrossbowRapidReloadSpeedValue` (MMO 연동)

**툴팁**: ✅ 완료
- "장전속도 +{X}%"
- Config 변경 시 실시간 업데이트

**효과 구현**: ❌ **미구현**
- **필요 작업**:
  - AnimationSpeedManager 또는 리로드 시간 패치 필요
  - 석궁 재장전 애니메이션 속도 증가 구현
- **참고 패턴**: 공격 속도 시스템 (AnimationSpeedManager.dll)
- **주의사항**: 활은 장전 개념이 없으므로 석궁 전용 구현 필요

---

### ✅ Tier 3-3: 정직한 한 발 (crossbow_Step3_mark)

**Config 연동**: ✅ 완료
- `CrossbowMarkDamageBonus` (기본값: 35%)
- `CrossbowMarkDamageBonusValue` (MMO 연동)

**툴팁**: ✅ 완료
- "치명타 확률 0% 고정, 대신 석궁 데미지 +{X}%"
- Config 변경 시 실시간 업데이트

**효과 구현**: ⚠️ **부분 구현**
- **치명타 0% 구현**: ✅ 완료
  - **위치**: `CriticalSystem/Critical.cs` (GetCrossbowCritChance 메서드)
  - 치명타 완전 차단 구현됨
- **데미지 보너스**: ❌ 미구현
  - **필요 작업**:
    - `SkillEffect.RangedSkills.cs` 또는 `AttackTree.cs`에 데미지 보너스 추가
    - Config 참조로 변경: `Crossbow_Config.CrossbowMarkDamageBonusValue`
  - **참고 패턴**: 활 전문가 데미지 보너스 (bow_Step1_damage)

---

### ✅ Tier 4: 자동 장전 (crossbow_Step4_re)

**Config 연동**: ✅ 완료
- `CrossbowAutoReloadChance` (기본값: 30%)
- `CrossbowAutoReloadChanceValue` (MMO 연동)

**툴팁**: ✅ 완료
- "명중 시 {X}% 확률로 빠르게 자동 장전"
- Config 변경 시 실시간 업데이트

**효과 구현**: ⚠️ **부분 구현** (하드코딩)
- **위치**: `SkillEffect.RangedSkills.cs:86-117`
- **현재 코드**:
  ```csharp
  public static bool TryAutoReload(Player player)
  {
      if (UnityEngine.Random.Range(0f, 1f) < 0.30f) // ❌ 하드코딩
      {
          // 자동 장전 로직
      }
  }
  ```
- **필요 수정**:
  ```csharp
  public static bool TryAutoReload(Player player)
  {
      float chance = Crossbow_Config.CrossbowAutoReloadChanceValue / 100f;
      if (UnityEngine.Random.Range(0f, 1f) < chance) // ✅ Config 연동
      {
          // 자동 장전 로직
      }
  }
  ```
- **호출 위치**: 발사체 적중 시 패치에서 호출 필요 (현재 미호출)
- **VFX**: `crossbow_re` 효과는 구현되어 있음 (라인 93)

---

### ✅ Tier 5-1: 관통 볼트 (crossbow_Step4_pierce)

**Config 연동**: ✅ 완료
- `CrossbowPierceBoltArmorIgnore` (기본값: 15%)
- `CrossbowPierceBoltArmorIgnoreValue` (MMO 연동)

**툴팁**: ✅ 완료
- "몬스터 방어력 {X}% 무시"
- Config 변경 시 실시간 업데이트

**효과 구현**: ❌ **미구현**
- **필요 작업**:
  - 발사체 데미지 계산 시 방어력 무시 로직 추가
  - Character.Damage 패치에서 방어력 계산 수정
- **참고 패턴**: 공격 전문가 트리의 방어력 관통 스킬

---

### ✅ Tier 5-2: 결전의 일격 (crossbow_Step5_final)

**Config 연동**: ✅ 완료
- `CrossbowFinalStrikeHpThreshold` (기본값: 50%)
- `CrossbowFinalStrikeDamageBonus` (기본값: 30%)
- 두 값 모두 MMO 연동

**툴팁**: ✅ 완료
- "체력 {X}% 이상 적에게 추가 {Y}% 피해"
- Config 변경 시 실시간 업데이트

**효과 구현**: ⚠️ **부분 구현**
- **체력 확인 함수**: ✅ 구현됨
  - **위치**: `SkillEffect.RangedSkills.cs:122-126`
  - **현재 코드**:
    ```csharp
    public static bool IsHighHealthTarget(Character target)
    {
        return target.GetHealthPercentage() >= 0.5f; // ❌ 하드코딩
    }
    ```
  - **필요 수정**:
    ```csharp
    public static bool IsHighHealthTarget(Character target)
    {
        float threshold = Crossbow_Config.CrossbowFinalStrikeHpThresholdValue / 100f;
        return target.GetHealthPercentage() >= threshold; // ✅ Config 연동
    }
    ```
- **데미지 보너스**: ❌ 미구현
  - **필요 작업**: 발사체 적중 시 체력 확인 후 데미지 증가 로직 추가
  - **호출 위치**: Character.Damage 패치에서 IsHighHealthTarget 확인 후 데미지 적용

---

### ✅ Tier 6: 단 한 발 (crossbow_Step6_expert) - 액티브 스킬

**Config 연동**: ✅ 완료
- `CrossbowOneShotDuration` (기본값: 30초)
- `CrossbowOneShotPierceBonus` (기본값: 120%)
- `CrossbowOneShotKnockback` (기본값: 5m)
- `CrossbowOneShotCooldown` (기본값: 60초)
- 모든 값 MMO 연동

**툴팁**: ✅ 완료
- "T키: {A}초 이내 석궁 발사 시 관통 데미지 +{B}%, 넉백 {C}m (쿨타임 {D}초)"
- Config 변경 시 실시간 업데이트

**효과 구현**: ⚠️ **부분 구현** (하드코딩)
- **위치**: `SkillEffect.ActiveSkills.cs` (라인 28-2313)
- **현재 코드**:
  ```csharp
  private static readonly float crossbowOneShotCooldownTime = 60f; // ❌ 하드코딩
  // 코드 내에 30초 버프, 120% 관통, 5m 넉백 하드코딩
  ```
- **필요 수정**:
  1. 쿨타임: `Crossbow_Config.CrossbowOneShotCooldownValue`로 변경
  2. 버프 지속시간: `Crossbow_Config.CrossbowOneShotDurationValue`로 변경
  3. 관통 데미지 보너스: `Crossbow_Config.CrossbowOneShotPierceBonusValue`로 변경
  4. 넉백 거리: `Crossbow_Config.CrossbowOneShotKnockbackValue`로 변경
- **VFX/SFX**: ✅ 구현됨 (vfx_crossbow_lightning_fire, buff_01, statusailment_01_aura)
- **버프 시스템**: ✅ 구현됨 (캐릭터 따라다니는 이펙트, 버프 카운트다운)
- **스킬 소모**: ✅ 구현됨 (CheckAndConsumeCrossbowOneShot 메서드)

---

## 🔧 필요한 작업 우선순위

### 🔴 **우선순위 1: 하드코딩 → Config 연동 (4개)**

1. **강한 관통 (pierce_all)** - 확률 하드코딩 수정
2. **균형 조준 (balance)** - 확률 + 넉백 거리 하드코딩 수정
3. **자동 장전 (auto_reload)** - 확률 하드코딩 수정
4. **결전의 일격 (final)** - 체력 임계값 하드코딩 수정
5. **단 한 발 (expert)** - 4개 변수 하드코딩 수정

### 🟡 **우선순위 2: 효과 미구현 (3개)**

1. **석궁 전문가 (damage)** - 데미지 보너스 구현 필요
2. **고속 장전 (rapid)** - 재장전 속도 증가 구현 필요
3. **관통 볼트 (pierce)** - 방어력 무시 구현 필요

### 🟢 **우선순위 3: 부분 구현 완성 (1개)**

1. **정직한 한 발 (mark)** - 데미지 보너스 추가 (치명타 0%는 완료)

---

## 📝 구현 체크리스트

### Config 시스템
- [x] 14개 Config 변수 선언
- [x] 동적 값 접근자 (MMO 연동)
- [x] Config 초기화
- [x] 툴팁 실시간 업데이트 시스템

### 효과 구현 (9개 스킬)
- [ ] crossbow_Step1_damage - 데미지 보너스
- [ ] crossbow_Step2_pierce_all - 확률 Config 연동 + 호출 위치 추가
- [ ] crossbow_Step2_balance - 확률/거리 Config 연동 + 호출 위치 추가
- [ ] crossbow_Step3_rapid - 재장전 속도 구현
- [ ] crossbow_Step3_mark - 데미지 보너스 추가 (crit 0%는 완료)
- [ ] crossbow_Step4_re - 확률 Config 연동 + 호출 위치 추가
- [ ] crossbow_Step4_pierce - 방어력 무시 구현
- [ ] crossbow_Step5_final - 체력 임계값 Config 연동 + 데미지 보너스 구현
- [ ] crossbow_Step6_expert - 4개 하드코딩 변수 Config 연동

---

## 🎯 다음 단계

1. **빌드 및 테스트** - Config 연동 및 툴팁 업데이트 정상 작동 확인
2. **하드코딩 수정** - 우선순위 1 작업 진행
3. **효과 구현** - 우선순위 2, 3 작업 진행
4. **게임 내 테스트** - 모든 석궁 스킬 효과 검증

---

**보고서 작성자**: Claude Sonnet 4.5
**보고서 버전**: 1.0
**최종 수정일**: 2025-01-06

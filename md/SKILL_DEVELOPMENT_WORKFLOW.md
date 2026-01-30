# SKILL_DEVELOPMENT_WORKFLOW.md - 스킬 개발 필수 워크플로우

## 📋 개요

**목적**: 스킬 개발/수정 시 관련 규칙을 자동으로 참조하고, 규칙이 없는 경우 경고 및 문서화

**적용 범위**: 모든 스킬 관련 코드 작성, 수정, 리팩토링

---

## 🔍 스킬 카테고리별 필수 참조 규칙

### 1. 공격 관련 스킬
**트리거 키워드**: `damage`, `attack`, `physical`, `pierce`, `blunt`, `slash`, `fire`, `frost`, `lightning`, `poison`, `spirit`

**필수 참조 규칙**:
- ✅ **[DAMAGE_SYSTEM_RULES.md](DAMAGE_SYSTEM_RULES.md)**
  - Rule 11: 발헤임 10가지 데미지 타입
  - Rule 11-1: 무기별 주 데미지 타입
  - Rule 13: GetDamage 패치 시스템

**체크리스트**:
```yaml
공격_스킬_개발_체크:
  - [ ] 데미지 타입이 10가지 중 하나인가? (물리 5종 + 속성 5종)
  - [ ] 무기별 주 데미지 타입에만 보너스 적용하는가?
  - [ ] GetDamage 패치 패턴을 따르는가? (고정값/비율)
  - [ ] Config-Tooltip-Effect 3단계 연동 완료?
```

### 2. 공격속도 관련 스킬
**트리거 키워드**: `attack speed`, `animation`, `공격속도`, `애니메이션`

**필수 참조 규칙**:
- ✅ **[ATTACK_SPEED_SYSTEM_RULES.md](ATTACK_SPEED_SYSTEM_RULES.md)**
  - Rule 14: AnimationSpeedManager 직접 호출 방식
  - Game.Awake Postfix 패치
  - InAttack() 조건 필수

**체크리스트**:
```yaml
공격속도_스킬_개발_체크:
  - [ ] AnimationSpeedManager.dll 직접 호출 방식 사용?
  - [ ] Game.Awake Postfix 패치 구현?
  - [ ] InAttack() 조건 체크 추가?
  - [ ] animator.speed 직접 제어 금지 확인?
  - [ ] Config 기반 값 사용?
```

### 3. 회피 관련 스킬
**트리거 키워드**: `dodge`, `evasion`, `invincibility`, `회피`, `구르기`, `무적`

**필수 참조 규칙**:
- ✅ **[EVASION_SYSTEM_RULES.md](EVASION_SYSTEM_RULES.md)**
  - Rule 15-1: 회피 확률 시스템 (SetCustomDodgeChance)
  - Rule 15-2: 구르기 무적시간 시스템 (m_dodgeInvincibilityTimer)
  - Rule 15-3: 구르기 스태미나 감소 시스템 (m_dodgeStaminaUsage)

**체크리스트**:
```yaml
회피_스킬_개발_체크:
  - [ ] 3가지 하위 시스템 중 어느 것에 해당?
    - [ ] 회피 확률: SetCustomDodgeChance() 사용?
    - [ ] 무적시간: Postfix + Traverse + 배율 방식?
    - [ ] 스태미나: Prefix + Traverse + 감소 배율?
  - [ ] Harmony Traverse 패턴 올바르게 사용?
  - [ ] 가산/배율/감소 방식 올바르게 구분?
  - [ ] UpdateDefenseDodgeRate() 통합 완료?
```

### 4. 방어력/방패 관련 스킬
**트리거 키워드**: `armor`, `defense`, `block`, `shield`, `방어력`, `갑옷`, `방패`, `막기`

**필수 참조 규칙**:
- ✅ **[ARMOR_BLOCK_SYSTEM_RULES.md](ARMOR_BLOCK_SYSTEM_RULES.md)**
  - Rule 17-1: 방어력(Armor) 시스템 - Character.GetBodyArmor()
  - Rule 17-2: 방패 방어력(Block Power) 시스템 - ItemDrop.ItemData.GetBlockPower()
  - Rule 17-3: 방패 관련 추가 시스템 (패링, 블럭 스태미나, 이동속도)

**체크리스트**:
```yaml
방어력_방패_스킬_개발_체크:
  - [ ] 효과가 갑옷 방어력인가? → GetBodyArmor() 사용
  - [ ] 효과가 방패 방어력인가? → GetBlockPower() 사용
  - [ ] 방패 체크 추가? (ItemType.Shield, GetLeftItem())
  - [ ] 툴팁과 실제 구현 일치?
    - [ ] "방어력" → GetBodyArmor()
    - [ ] "방패 방어력" → GetBlockPower()
  - [ ] Config-Tooltip-Effect 3단계 연동 완료?
  - [ ] 고정값 vs 비율 보너스 명확히 구분?
```

### 5. 체력 관련 스킬
**트리거 키워드**: `health`, `hp`, `체력`, `생명력`, `최대 체력`

**필수 참조 규칙**:
- ✅ **[HEALTH_SYSTEM_RULES.md](HEALTH_SYSTEM_RULES.md)**
  - Rule 9: 발헤임 체력 시스템 구조
  - m_baseHP 포함 필수
  - GetTotalFoodValue 패치 구현

**체크리스트**:
```yaml
체력_스킬_개발_체크:
  - [ ] 체력 보너스는 m_baseHP에 포함?
  - [ ] GetTotalFoodValue 패치에서 구현?
  - [ ] 비율 보너스를 고정값으로 변환?
  - [ ] GetMaxHealth 직접 수정 금지 확인?
```

### 6. 치명타 관련 스킬
**트리거 키워드**: `critical`, `crit`, `치명타`

**필수 참조 규칙**:
- ✅ **[CRITICAL_SYSTEM_RULES.md](CRITICAL_SYSTEM_RULES.md)**
  - Rule 12: 중앙화된 치명타 시스템
  - Critical.cs, CriticalDamage.cs
  - 4가지 구현 패턴

**체크리스트**:
```yaml
치명타_스킬_개발_체크:
  - [ ] Critical.cs / CriticalDamage.cs 사용?
  - [ ] 공통 보너스 + 무기별 보너스 구조?
  - [ ] 4가지 패턴 중 어느 것 사용?
    - [ ] 기본 패턴
    - [ ] 패시브 스킬 패턴
    - [ ] 시간 기반 버프 패턴
    - [ ] 비활성화 패턴
```

### 7. 이동속도 관련 스킬
**트리거 키워드**: `movement speed`, `move speed`, `이동속도`

**필수 참조 규칙**:
- ✅ **[SPEED_EXPERT_VALHEIM_API_IMPLEMENTATION.md](SPEED_EXPERT_VALHEIM_API_IMPLEMENTATION.md)**
  - SE_Stats 기반 구현
  - m_speedModifier 사용

**체크리스트**:
```yaml
이동속도_스킬_개발_체크:
  - [ ] SE_StatTreeSpeed 사용? (권장)
  - [ ] SE_Stats m_speedModifier 사용?
  - [ ] 중복 적용 방지 확인?
  - [ ] 무기별 조건부 적용 시 IsUsing{Weapon}() 체크?
```

### 8. 에이트르 관련 스킬
**트리거 키워드**: `eitr`, `Eitr`, `에이트르`, `마법`, `마나`

**필수 참조 규칙**:
- ✅ **[EITR_STAGGER_SYSTEM_RULES.md](EITR_STAGGER_SYSTEM_RULES.md)** - Rule 18
  - GetTotalFoodValue 패치 사용
  - 고정값 보너스 방식
  - UseEitr(), AddEitr() 소모 방식

**체크리스트**:
```yaml
에이트르_스킬_개발_체크:
  - [ ] GetTotalFoodValue Postfix 패치 사용?
  - [ ] eitr 파라미터에 보너스 합산?
  - [ ] 고정값 vs 비율 명확히 구분?
  - [ ] Config-Tooltip-Effect 3단계 연동?
  - [ ] UseEitr() 또는 AddEitr() 소모 방식?
  - [ ] GetMaxEitr() 직접 패치 금지 확인?
```

### 9. 비틀거림 관련 스킬
**트리거 키워드**: `stagger`, `Stagger`, `비틀거림`, `기절`, `스태거`

**필수 참조 규칙**:
- ⚠️ **[EITR_STAGGER_SYSTEM_RULES.md](EITR_STAGGER_SYSTEM_RULES.md)** - Rule 19
  - IsStaggering() API 사용 (검증 필요)
  - 비틀거림 중 추가 효과 적용
  - Humanoid.Damage 또는 Character.Damage 패치

**체크리스트**:
```yaml
비틀거림_스킬_개발_체크:
  - [ ] ⚠️ IsStaggering() API 검증 완료?
  - [ ] 비틀거림 상태 정상 감지 테스트?
  - [ ] 추가 효과 정상 작동 확인?
  - [ ] 다양한 적 유형에서 테스트? (인간형/동물/보스)
  - [ ] 멀티플레이어 동기화 확인?
  - [ ] 성능 이슈 없는지 확인? (FPS 저하)
```

**⚠️ 비틀거림 시스템 검증 절차**:
1. **API 존재 확인**: IsStaggering() 메서드 호출 테스트
2. **게임 내 테스트**: 둔기로 적 비틀거림 유발 후 로그 확인
3. **추가 피해 검증**: 비틀거림 중 피해 증가 정상 작동 확인
4. **멀티플레이어 테스트**: 호스트/클라이언트 모두 확인
5. **규칙 확정**: 검증 완료 후 "⚠️" 마크 제거

---

## ⚠️ 규칙 부재 경고 시스템

### 경고 조건
다음 상황에서 **즉시 경고**:

1. **새로운 효과 타입 발견**
   - 기존 9개 카테고리에 속하지 않는 스킬 효과
   - 예: 스태미나 회복, 채집 속도, 낙하 피해 감소 등

2. **기존 규칙과 다른 구현 패턴**
   - 문서화된 패턴을 따르지 않는 새로운 접근 방식
   - 예: GetDamage 대신 다른 메서드 패치

3. **Valheim API 직접 사용**
   - 문서화되지 않은 Valheim 내부 API 호출
   - 예: 새로운 protected 필드 접근

### 경고 메시지 형식
```
⚠️ [규칙 부재 경고]
카테고리: {스킬_효과_타입}
발견 위치: {파일명}:{줄번호}
상황: {구체적_설명}

→ 조치 필요:
1. 기존 규칙 재확인 (해당 규칙이 실제로 없는지)
2. 규칙화 가능성 검토 (재사용 가능한 패턴인가?)
3. 규칙 문서화 진행 (새로운 .md 파일 생성 또는 기존 파일 업데이트)
```

---

## 📝 규칙 문서화 절차

### 1단계: 규칙 부재 확인
```bash
# 관련 키워드로 기존 규칙 검색
grep -r "스태미나" CaptainSkillTree/*.md
grep -r "stamina" CaptainSkillTree/*.md
```

### 2단계: 구현 패턴 분석
```yaml
분석_항목:
  - 어떤 Valheim API 사용?
  - Harmony 패치 위치 (Prefix/Postfix/Transpiler)
  - Config-Tooltip-Effect 연동 방식
  - 무기별/조건부 적용 여부
  - 재사용 가능한 공통 패턴 존재?
```

### 3단계: 규칙 문서 생성/업데이트
**새로운 규칙 파일 생성 시**:
```markdown
# {CATEGORY}_SYSTEM_RULES.md

## Rule {번호}: {규칙_제목}

### 개요
- **목적**: {무엇을_위한_규칙}
- **적용 범위**: {어떤_스킬들}
- **Valheim API**: {사용하는_API}

### 구현 패턴
{코드_예제}

### 금지 사항
- ❌ {하면_안되는_것}

### 권장 사항
- ✅ {권장_패턴}

### 테스트 체크리스트
- [ ] {확인_항목}
```

**기존 파일 업데이트 시**:
- 새로운 Rule 섹션 추가
- 관련 스킬 목록 업데이트
- 패턴 비교 테이블 추가

### 4단계: QUICK_REFERENCE.md 업데이트
```markdown
## {새로운_카테고리} 관련 스킬 개발 패턴

### 핵심 API
{API_설명}

### 코드 예제
{간단한_예제}

### 주의사항
{핵심_포인트}
```

### 5단계: CLAUDE.md 통합
```markdown
## 📚 규칙 체계 (Rule Hierarchy)

### 🎯 필수 규칙 (Core Rules)
- **[새로운 규칙](경로.md)** - Rule {번호}
  - {설명}
```

---

## 🔄 스킬 개발 워크플로우 (단계별)

### Phase 1: 기획 및 규칙 확인
```yaml
1. 스킬 효과 정의:
   - 효과 타입 분류 (공격/공격속도/회피/방어력/방패/체력/치명타/이동속도/에이트르/비틀거림/기타)
   - 수치 설계 (고정값/비율)
   - 조건부 적용 여부 (무기/상황)

2. 관련 규칙 참조:
   - 위 카테고리별 필수 참조 규칙 확인
   - 체크리스트 작성
   - 규칙 부재 시 경고 발생

3. 구현 패턴 선택:
   - 기존 패턴 재사용 (권장)
   - 새로운 패턴 필요 시 문서화 계획
```

### Phase 2: 구현
```yaml
1. Config 정의:
   - {Category}_Config.cs에 추가
   - 기본값, 범위, 설명 포함
   - 툴팁용 설명 작성

2. 효과 구현:
   - SkillEffect.{Category}.cs 또는 별도 파일
   - Harmony 패치 작성
   - 관련 규칙의 패턴 준수

3. 3단계 연동:
   - Config: 설정값
   - Tooltip: 플레이어에게 보이는 설명
   - Effect: 실제 효과 구현
```

### Phase 3: 검증
```yaml
1. 규칙 준수 확인:
   - 카테고리별 체크리스트 완료
   - 금지 사항 위반 없는지 확인
   - 권장 패턴 따르는지 확인

2. 빌드 테스트:
   - 컴파일 오류 없는지
   - 경고 메시지 확인
   - DLL 크기 확인

3. 게임 내 테스트:
   - 효과 정상 작동
   - Config 값 변경 반영
   - 멀티플레이어 동기화
```

### Phase 4: 문서화
```yaml
1. 규칙 부재 시:
   - 새로운 규칙 문서 생성
   - QUICK_REFERENCE.md 업데이트
   - CLAUDE.md 통합

2. 기존 규칙 확장 시:
   - 해당 규칙 파일 업데이트
   - 스킬 목록 추가
   - 패턴 비교 테이블 업데이트

3. 개발 로그 기록:
   - 어떤 패턴 사용했는지
   - 왜 그 패턴을 선택했는지
   - 주의사항 기록
```

---

## 🎯 자동 규칙 참조 트리거

### AI 어시스턴트 동작 규칙
다음 키워드가 감지되면 **자동으로 관련 규칙 참조**:

```yaml
자동_트리거:
  공격_관련:
    키워드: [damage, attack, pierce, blunt, slash, fire, frost, lightning, poison, spirit, 데미지, 공격]
    참조: DAMAGE_SYSTEM_RULES.md

  공격속도_관련:
    키워드: [attack speed, animation, 공격속도, 애니메이션]
    참조: ATTACK_SPEED_SYSTEM_RULES.md

  회피_관련:
    키워드: [dodge, evasion, invincibility, roll, 회피, 구르기, 무적]
    참조: EVASION_SYSTEM_RULES.md

  방어력_방패_관련:
    키워드: [armor, defense, block, shield, parry, 방어력, 갑옷, 방패, 막기, 패링]
    참조: ARMOR_BLOCK_SYSTEM_RULES.md

  체력_관련:
    키워드: [health, hp, 체력, 생명력, 최대 체력]
    참조: HEALTH_SYSTEM_RULES.md

  치명타_관련:
    키워드: [critical, crit, 치명타]
    참조: CRITICAL_SYSTEM_RULES.md

  이동속도_관련:
    키워드: [movement speed, move speed, 이동속도]
    참조: SPEED_EXPERT_VALHEIM_API_IMPLEMENTATION.md

  에이트르_관련:
    키워드: [eitr, Eitr, 에이트르, 마법, 마나]
    참조: EITR_STAGGER_SYSTEM_RULES.md (Rule 18)
    상태: ✅ 검증 완료

  비틀거림_관련:
    키워드: [stagger, Stagger, 비틀거림, 기절, 스태거]
    참조: EITR_STAGGER_SYSTEM_RULES.md (Rule 19)
    상태: ⚠️ 검증 필요
    동작: 검증 완료 확인 후 사용

  규칙_부재:
    키워드: [stamina regen, gathering, fall damage, 스태미나 회복, 채집, 낙하]
    동작: 경고 메시지 + 규칙화 제안
```

---

## 🚨 금지 사항 (워크플로우 위반)

### ❌ 절대 하지 말 것
1. **규칙 확인 없이 스킬 구현** - 반드시 관련 규칙 먼저 읽기
2. **패턴 무시하고 새로운 방식** - 기존 패턴 재사용 우선
3. **규칙 부재 시 무시하고 진행** - 경고 발생 시 즉시 문서화
4. **문서화 없이 복잡한 패턴 사용** - 재사용 가능하면 규칙화 필수
5. **체크리스트 건너뛰기** - 모든 항목 확인 필수

### ⚠️ 주의 사항
1. **새로운 Valheim API 사용 시** - 반드시 테스트 후 문서화
2. **기존 스킬 수정 시** - 같은 카테고리 다른 스킬에 영향 없는지 확인
3. **Config 값 변경 시** - 게임 밸런스 고려
4. **멀티플레이어 환경** - 동기화 필수

---

## ✅ 권장 사항 (Best Practices)

### 1. 규칙 우선 접근
```
스킬 아이디어 → 관련 규칙 확인 → 패턴 선택 → 구현 → 검증 → 문서화
```

### 2. 점진적 개발
- 한 번에 하나의 스킬만 개발
- 빌드 → 테스트 → 다음 스킬

### 3. 패턴 재사용
- 같은 효과 타입은 동일한 패턴 사용
- 코드 중복 최소화

### 4. 적극적 문서화
- 새로운 패턴 발견 시 즉시 규칙화
- 다른 개발자가 참조할 수 있게

### 5. 체계적 테스트
- 단위 기능별 테스트
- 통합 테스트 (여러 스킬 조합)
- 멀티플레이어 테스트

---

## 📚 참조 문서 링크

### 필수 규칙
- [DAMAGE_SYSTEM_RULES.md](DAMAGE_SYSTEM_RULES.md) - 공격 관련
- [ATTACK_SPEED_SYSTEM_RULES.md](ATTACK_SPEED_SYSTEM_RULES.md) - 공격속도 관련
- [EVASION_SYSTEM_RULES.md](EVASION_SYSTEM_RULES.md) - 회피 관련
- [HEALTH_SYSTEM_RULES.md](HEALTH_SYSTEM_RULES.md) - 방어/체력 관련
- [CRITICAL_SYSTEM_RULES.md](CRITICAL_SYSTEM_RULES.md) - 치명타 관련
- [SPEED_EXPERT_VALHEIM_API_IMPLEMENTATION.md](SPEED_EXPERT_VALHEIM_API_IMPLEMENTATION.md) - 이동속도 관련

### 시스템 가이드
- [MMO_INTEGRATION_GUIDE.md](MMO_INTEGRATION_GUIDE.md) - MMO 연동
- [CONFIG_MANAGEMENT_RULES.md](CONFIG_MANAGEMENT_RULES.md) - 설정 관리
- [SKILL_NAMING_RULES.md](SKILL_NAMING_RULES.md) - 명명 규칙

### 빠른 참조
- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - 핵심 패턴 요약

---

**이 워크플로우를 준수하여 일관성 있고 안정적인 스킬 시스템을 개발하세요.**

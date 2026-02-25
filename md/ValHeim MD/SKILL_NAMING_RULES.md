# 스킬 명명 및 키바인딩 규칙 (SKILL_NAMING_RULES.md)

**규칙 통합**: Rule 4 (스킬 ID 명명), Rule 5 (액티브 스킬 키 바인딩), Rule 6 (전문가 제한 시스템)

---

## 📋 목차
1. [스킬 ID 명명 규칙 (Rule 4)](#rule-4-스킬-id-명명-규칙)
2. [액티브 스킬 키 바인딩 (Rule 5)](#rule-5-액티브-스킬-키-바인딩)
3. [전문가 기반 제한 시스템 (Rule 6)](#rule-6-전문가-기반-제한-시스템)
4. [통합 예시](#통합-예시)

---

## Rule 4: 스킬 ID 명명 규칙

### 🎯 핵심 원칙
- **일관성**: 모든 스킬 ID는 정해진 패턴을 따라야 함
- **가독성**: ID만으로 스킬 타입과 계층을 파악 가능
- **충돌 방지**: 고유한 ID로 스킬 간 충돌 방지

### 명명 패턴

#### 1. 전문가 스킬 (Expert Skills)
**패턴**: `{타입}_expert_{속성}`

**타입 분류**:
- `weapon`: 무기 전문가 (sword, bow, mace, dagger, spear, polearm, crossbow, staff)
- `combat`: 전투 전문가 (melee, ranged, attack, defense)
- `production`: 생산 전문가 (mining, woodcutting, fishing)
- `speed`: 속도 전문가 (run, swim, sneak)
- `job`: 직업 전문가 (archer, tanker, berserker, rogue, mage, paladin)

**예시**:
```
sword_expert_damage         // 검 전문가 - 데미지 특화
bow_expert_precision        // 활 전문가 - 정확도 특화
mining_expert_efficiency    // 채광 전문가 - 효율 특화
knife_expert_backstab       // 단검 전문가 - 백스탭 특화
```

#### 2. 일반 스킬 (Regular Skills)
**패턴**: `{weapon/category}_Step{tier}_{skill_name}`

**step 규칙**:
- `step`: 단계 번호 (step1, step2, ..., step9)
- Tier와 step은 동일한 번호 사용

**예시**:
```
bow_step6_critboost         // 활 Tier 6 - 크리티컬 부스트
mace_step7_fury_hammer      // 둔기 Tier 7 - 분노의 망치
knife_step4_attack_damage   // 단검 Tier 4 - 빠른 공격
spear_step3_pierce          // 창 Tier 3 - 연격창
```

#### 3. 루트 노드 (Root Nodes)
**패턴**: `{category}_root`

**카테고리 분류**:
- `melee_root`: 근접 전문가 루트
- `ranged_root`: 원거리 전문가 루트
- `attack_root`: 공격 전문가 루트
- `defense_root`: 방어 전문가 루트
- `production_root`: 생산 전문가 루트
- `speed_root`: 속도 전문가 루트

**예시**:
```
melee_root              // 근접 무기 트리 루트
ranged_root             // 원거리 무기 트리 루트
defense_root            // 방어 트리 루트
```

### 금지 사항
- ❌ 공백 사용: `bow step6 critboost` (스네이크 케이스 사용 필수)
- ❌ 대문자 사용: `Bow_Step6_CritBoost` (소문자만 사용)
- ❌ 특수문자: `bow-step6-critboost` (언더스코어만 허용)
- ❌ 축약어 남용: `bw_s6_cb` (명확한 단어 사용)

### 체크리스트
- [ ] 스킬 ID가 정해진 패턴 준수
- [ ] 소문자 + 언더스코어만 사용
- [ ] Tier 번호와 step 번호 일치
- [ ] 고유한 ID로 중복 없음

---

## Rule 5: 액티브 스킬 키 바인딩

### 🎯 핵심 원칙
- **직관성**: 스킬 타입에 따라 일관된 키 배치
- **충돌 방지**: 같은 키에 여러 스킬 배정 불가
- **확장성**: 향후 추가 스킬을 고려한 키 배치

### 키 바인딩 구조

#### T키: 원거리 액티브 스킬
**대상 무기**: 활, 석궁, 지팡이 (원거리 공격형)

**등록된 스킬**:
- `bow_step6_critboost` - 활: 크리티컬 부스트 (치명타 확률 100%)
- `crossbow_step5_rapidfire` - 석궁: 연발 사격
- `staff_step7_double_cast` - 지팡이: 이중 시전 (2배 투사체)

**특징**:
- 공격력 증폭 또는 속도 증가
- 짧은 쿨타임 (10-30초)
- 스태미나 소모

#### G키: 보조형 액티브 스킬
**대상 무기**: 지팡이 (힐링), 단검 (암살), 검 (강타)

**등록된 스킬**:
- `staff_step5_healing` - 지팡이: 힐링 (자신/주변 회복)
- `knife_step9_assassin_heart` - 단검: 암살자의 심장 (데미지+치명타 버프)
- `sword_step6_slash` - 검: Sword Slash (강력한 베기 공격)
- `mace_step7_fury_hammer` - 둔기: 분노의 망치 (광역 경직)
- `spear_step8_combo` - 창: 연공창 (3연속 빠른 공격)

**무기별 자동 전환**:
- 착용 무기 확인 → 해당 스킬 자동 발동
- 지팡이 착용 시 → 힐링
- 단검 착용 시 → 암살자의 심장
- 검 착용 시 → Sword Slash

**특징**:
- 생존 또는 유틸리티 효과
- 중간 쿨타임 (20-60초)
- 스태미나 또는 에이트르 소모

#### H키: 방어형/특수 액티브 스킬
**대상 무기**: 둔기, 창, 방패

**등록된 스킬**:
- `mace_step9_reflect` - 둔기: 분노의 반사 (피해 반사)
- `spear_step6_throw` - 창: 강화 투척 (원거리 관통 공격)

**특징**:
- 방어 또는 특수 메커니즘
- 긴 쿨타임 (60-120초)
- 높은 스태미나 소모

#### Y키: 직업 액티브 스킬
**대상**: 6개 직업 (Archer, Tanker, Berserker, Rogue, Mage, Paladin)

**등록된 스킬**:
- `Archer` - 궁수: 화살비 (광역 화살 공격)
- `Tanker` - 탱커: 도발 (적 어그로 집중)
- `Berserker` - 광전사: 광폭화 (공격력↑ 방어력↓)
- `Rogue` - 로그: 그림자 일격 (백스탭 강화)
- `Mage` - 마법사: 메테오 (광역 화염 마법)
- `Paladin` - 성기사: 신성한 일격 (언데드 추가 피해)

**특징**:
- 직업 고유 능력
- 매우 긴 쿨타임 (120-300초)
- 높은 리소스 소모 (에이트르 우선)

### 키 바인딩 구현 위치
- **SkillTreeInputListener.cs**: 키 입력 감지 (수정 금지)
- **Knife_Skill.cs**: G키 단검 스킬 처리
- **MaceSkills.cs**: G키/H키 둔기 스킬 처리
- **JobSkills.cs**: Y키 직업 스킬 처리

### 금지 사항
- ❌ **같은 키에 여러 스킬 중복 배정** - 무기별 자동 전환 사용
- ❌ **기본 게임 키와 충돌** (E, R, 숫자키 등)
- ❌ **키 바인딩 임의 변경** - SkillTreeInputListener.cs 수정 금지

### 체크리스트
- [ ] 스킬이 올바른 키에 배정됨
- [ ] 무기 타입과 키가 일치
- [ ] 중복 배정 없음
- [ ] SkillTreeInputListener.cs 미수정

---

## Rule 6: 전문가 기반 제한 시스템

### 🎯 핵심 원칙
- **전문화**: 특정 분야에 집중하도록 액티브 스킬 제한
- **균형**: 모든 전문가가 동일한 제약 조건
- **유연성**: 특정 무기군(지팡이/둔기)은 예외 허용

### 전문가별 제한 규칙

#### 원거리 전문가 (Ranged Expert)
**제한**: 액티브 스킬 **1개만** 선택 가능

**대상 스킬**:
- 활 (T키)
- 석궁 (T키)

**선택 시나리오**:
- ✅ 활 OR 석궁 중 1개 선택
- ❌ 활 AND 석궁 동시 선택 불가

**구현**:
```csharp
// ActiveSkillSlots.cs - 원거리 전문가 슬롯 체크
if (HasSkill("ranged_root"))
{
    int rangedActives = CountActives(new[] {"bow_step6_critboost", "crossbow_step5_rapidfire"});
    if (rangedActives >= 1) return false; // 이미 1개 선택됨
}
```

#### 근접 전문가 (Melee Expert)
**제한**: 액티브 스킬 **1개만** 선택 가능

**대상 스킬**:
- 단검 (G키)
- 검 (G키)
- 둔기 (G키/H키)
- 창 (G키/H키)

**선택 시나리오**:
- ✅ 단검 OR 검 OR 둔기 OR 창 중 1개 선택
- ❌ 여러 근접 무기 액티브 동시 선택 불가

**G키 무기별 자동 전환**:
- 근접 전문가가 여러 무기 액티브 보유 가능
- G키 입력 시 **현재 착용한 무기**에 맞는 스킬 자동 발동
- 예: 힐링+암살자의 심장 보유 → 지팡이 착용 시 힐링, 단검 착용 시 암살자의 심장

**구현**:
```csharp
// ActiveSkillSlots.cs - 근접 전문가 슬롯 체크
if (HasSkill("melee_root"))
{
    int meleeActives = CountActives(new[] {"knife_step9_assassin_heart", "sword_step6_slash", ...});
    if (meleeActives >= 1) return false;
}
```

#### 지팡이/둔기 전문가 (Staff/Mace Expert)
**제한**: 같은 무기류 **2개 모두** 선택 가능

**대상 스킬**:
- 지팡이: T키 (이중 시전) + G키 (힐링)
- 둔기: G키 (분노의 망치) + H키 (분노의 반사)

**선택 시나리오**:
- ✅ 지팡이 2개 스킬 모두 선택 가능
- ✅ 둔기 2개 스킬 모두 선택 가능
- ❌ 지팡이 + 둔기 혼합 선택 불가

**구현**:
```csharp
// 지팡이/둔기는 같은 무기 내에서 2개 허용
if (HasSkill("staff_expert") && newSkillId.Contains("staff")) return true;
if (HasSkill("mace_expert") && newSkillId.Contains("mace")) return true;
```

#### 직업 슬롯 (Job Slot)
**제한**: **1개만** 선택 가능

**대상 스킬**:
- Archer, Tanker, Berserker, Rogue, Mage, Paladin (Y키)

**선택 시나리오**:
- ✅ 6개 직업 중 1개만 선택
- ❌ 여러 직업 동시 선택 불가

**구현**:
```csharp
// ActiveSkillSlots.cs - 직업 슬롯 체크
string[] jobs = {"Archer", "Tanker", "Berserker", "Rogue", "Mage", "Paladin"};
int jobCount = CountActives(jobs);
if (jobCount >= 1) return false; // 이미 직업 선택됨
```

### 전문가 제한 검증 위치
- **SkillTreeManager.cs**: `CanUnlockActiveSkill()` - 해제 가능 여부 검증
- **ActiveSkillSlots.cs**: 슬롯 제한 로직 (존재 시)
- **SkillTreeUI.cs**: UI에서 시각적 제한 표시

### 금지 사항
- ❌ **제한 우회** - 임의로 여러 액티브 스킬 해제
- ❌ **직업 슬롯 예외** - 직업은 반드시 1개만
- ❌ **전문가 없이 액티브 스킬 해제** - 해당 전문가 먼저 해제 필수

### 체크리스트
- [ ] 전문가 해제 후 액티브 스킬 해제
- [ ] 원거리 전문가: 액티브 1개만
- [ ] 근접 전문가: 액티브 1개만 (G키 자동 전환)
- [ ] 지팡이/둔기: 같은 무기 내 2개 허용
- [ ] 직업: 1개만 선택

---

## 통합 예시

### 예시 1: 활 전문가 빌드
```
✅ ranged_root (원거리 전문가) 해제
✅ bow_expert_precision (활 전문가) 해제
✅ bow_step6_critboost (T키 액티브) 해제
❌ crossbow_step5_rapidfire (T키 액티브) 해제 불가 (이미 활 액티브 선택)
✅ Archer (Y키 직업) 해제
```

### 예시 2: 단검 전문가 빌드
```
✅ melee_root (근접 전문가) 해제
✅ knife_expert_backstab (단검 전문가) 해제
✅ knife_step9_assassin_heart (G키 액티브) 해제
❌ sword_step6_slash (G키 액티브) 해제 불가 (이미 단검 액티브 선택)
✅ Rogue (Y키 직업) 해제
```

### 예시 3: 지팡이 전문가 빌드 (2개 액티브)
```
✅ ranged_root (원거리 전문가) 해제
✅ staff_expert (지팡이 전문가) 해제
✅ staff_step7_double_cast (T키 액티브) 해제
✅ staff_step5_healing (G키 액티브) 해제 (지팡이는 2개 허용)
✅ Mage (Y키 직업) 해제
```

### 예시 4: 둔기 전문가 빌드 (2개 액티브)
```
✅ melee_root (근접 전문가) 해제
✅ mace_expert (둔기 전문가) 해제
✅ mace_step7_fury_hammer (G키 액티브) 해제
✅ mace_step9_reflect (H키 액티브) 해제 (둔기는 2개 허용)
✅ Tanker (Y키 직업) 해제
```

---

## 참고 문서
- [ACTIVE_SKILL_SYSTEM.md](ACTIVE_SKILL_SYSTEM.md) - Rule 5 상세 가이드
- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - 빠른 참조
- [CLAUDE.md](../CLAUDE.md) - 메인 규칙 파일

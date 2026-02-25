# 액티브 스킬 시스템 (Active Skill Rules System)

## Expert-Based Active Skill Limitation System (전문가 기반 액티브 스킬 제한 시스템)

### 핵심 규칙:
- **원거리 전문가**: 원거리 액티브 스킬 1개만 선택 가능 (bow_Step6_critboost, crossbow_Step6_expert)
- **근접 전문가**: 근접 액티브 스킬 1개만 선택 가능
- **지팡이 전문가**: staff_Step6_dual_cast + staff_Step6_heal 2개 모두 선택 가능 (같은 무기류)
- **둔기 전문가**: mace_Step7_fury_hammer 1개 선택 가능
- **창 전문가**: spear_Step6_enhanced_throw 액티브 스킬 (H키)
- **직업 슬롯**: 1개만 선택 가능 (Archer, Tanker, Berserker, Rogue, Mage, Paladin)
- **공격/속도/생산/방어 전문가**: 액티브 스킬 없음, 제한 없음

### 전문가별 액티브 스킬 제한:
- **원거리 전문가**: 활 마스터 습득 시 → 석궁 전문가 스킬 차단 (메시지: "원거리 전문가 액티브 스킬은 더 이상 배울 수 없습니다.")
- **근접 전문가**: 검/도끼 등 하나 습득 시 → 다른 근접 무기 액티브 스킬 차단 (메시지: "근접 전문가 액티브 스킬은 더 이상 배울 수 없습니다.")
- **지팡이 전문가**: 2개 모두 습득 가능 (같은 무기류 예외)
- **둔기 전문가**: 1개 습득 가능
- **창 전문가**: 창 액티브 스킬 습득 가능

## Validation System (검증 시스템)

### 차별화된 메시지 시스템:
- **경고 (Warning)**: melee_root ↔ ranged_root 교차 투자 시 - 투자 허용, 노란색 경고만 표시
- **차단 (Blocking)**: 전문가별 액티브 스킬 제한 위반 시 - 투자 차단, 빨간색 오류 메시지

### 전문가별 차단 메시지:
- **원거리 전문가**: "원거리 전문가 액티브 스킬은 더 이상 배울 수 없습니다."
- **근접 전문가**: "근접 전문가 액티브 스킬은 더 이상 배울 수 없습니다."
- **창 전문가**: "창 전문가 액티브 스킬은 더 이상 배울 수 없습니다."
- **같은 무기류 예외**: 지팡이/둔기 전문가는 해당 무기의 모든 액티브 스킬 습득 가능

## Active Skill Key Binding System (액티브 스킬 키 바인딩 시스템)

### T키 시스템 (원거리 액티브 스킬):
1. 석궁 착용 + crossbow_Step6_expert → 단 한 발
2. 활 착용 + bow_Step6_critboost → 활 마스터 멀티샷
3. 지팡이 착용 + staff_Step6_dual_cast → 이중 시전

### G키 시스템 (보조형 액티브 스킬):
1. 지팡이 착용 + staff_Step6_heal → 힐
2. 둔기 착용 + mace_Step7_fury_hammer → 분노의 망치
3. 창 착용 + spear_Step5_combo → 연공창 (강화 투척)
4. 검 착용 + sword_step5_finalcut → Sword Slash
5. 단검 착용 + knife_step9_assassin_heart → 암살자의 심장

### H키 시스템 (방어형/특수 액티브 스킬):
1. 창 착용 + spear_Step6_enhanced_throw → 강화 투척

### ~~휠마우스 버튼 시스템~~ (더 이상 사용하지 않음 - 연공창이 G키로 이동됨)

### Y키 시스템 (직업 액티브 스킬):
1. Archer 직업 스킬
2. Tanker 직업 스킬
3. Berserker 직업 스킬
4. Rogue 직업 스킬
5. Mage 직업 스킬
6. Paladin 직업 스킬

## Step-Based Skill ID Convention (Step 기반 스킬 ID 규칙)

### 명명 규칙: `{weapon}_{Step번호}_{skill_name}`
- Step 번호는 각 무기 트리의 단계별 레벨을 의미
- 예시: bow_critboost → bow_Step6_critboost
- 모든 액티브 스킬은 이 규칙을 따라 명명

### 무기별 액티브 스킬 ID:
```csharp
// 원거리 액티브 스킬 (T키)
"bow_Step6_critboost"           // 활 마스터
"crossbow_Step6_expert"         // 단 한 발
"staff_Step6_dual_cast"         // 이중 시전

// 보조형 액티브 스킬 (G키)
"staff_Step6_heal"              // 힐
"mace_Step7_fury_hammer"        // 분노의 망치

// 방어형/특수 액티브 스킬 (H키)
"mace_Step7_guardian_soul"      // 반사

// 창 액티브 스킬 (휠마우스)
"spear_Step8_enhanced_throw"    // 연공창

// 직업 액티브 스킬 (Y키)
"job_archer_active"             // Archer 스킬
"job_tanker_active"             // Tanker 스킬
"job_berserker_active"          // Berserker 스킬
"job_rogue_active"              // Rogue 스킬
"job_mage_active"               // Mage 스킬
"job_paladin_active"            // Paladin 스킬
```

## UI Enhancement (UI 개선사항)

### 툴팁 전문가 정보:
- 원거리 전문가: "🎯 원거리 전문가 액티브 스킬 (1개만 선택 가능)"
- 근접 전문가: "⚔️ 근접 전문가 액티브 스킬 (1개만 선택 가능)"
- 지팡이 전문가: "🪄 지팡이 전문가 (2개 모두 선택 가능)"
- 둔기 전문가: "🔨 둔기 전문가 (2개 모두 선택 가능)"
- 창 전문가: "🗡️ 창 전문가 (액티브 스킬 사용 가능)"
- 직업: "👤 직업 슬롯 (1개만 선택 가능)"
- 전문가 노드: "⚠️ 전문가 노드 (교차 투자 시 경고)"

### 빌드 상태 UI:
- 현재 선택된 원거리/근접/직업 스킬 실시간 표시
- CreateBuildStatusUI() 및 UpdateBuildStatusText() 메서드로 구현

## Implementation Files (구현 파일들)
- **SkillTreeManager.cs**: 슬롯 상태 관리, 검증 로직
- **SkillEffect.ActiveSkills.cs**: T키/G키/H키 통합 핸들러
- **ActiveSkillInput.cs**: T키/G키/H키/Y키 우선순위 시스템
- **SkillTreeTooltip.cs**: 슬롯 정보 표시
- **SkillTreeUI.cs**: 빌드 상태 UI
- **SkillTreeData.cs**: Step 기반 스킬 ID

## Active Skill Categories (액티브 스킬 카테고리)

### 1. 원거리 액티브 스킬 (T키)
- **활 마스터**: 멀티샷 (화살 3발 동시 발사)
- **석궁 전문가**: 단 한 발 (강력한 단일 발사)
- **지팡이 이중시전**: 마법 2번 시전

### 2. 보조형 액티브 스킬 (G키)
- **지팡이 힐**: 체력 회복 마법
- **둔기 분노의 망치**: 강력한 범위 공격
- **창 연공창**: 강화된 투창으로 적과 주변 몬스터 넉백 (+200% 피해)
- **검 Sword Slash**: 빠른 3연속 베기 공격
- **단검 암살자의 심장**: 강력한 단일 타겟 공격

### 3. 방어형/특수 액티브 스킬 (H키)
- **둔기 반사**: 받은 데미지 반사

### ~~4. 창 액티브 스킬 (휠마우스 버튼)~~ (더 이상 사용하지 않음)

### 5. 직업 액티브 스킬 (Y키)
- **Archer**: 원거리 특화 스킬
- **Tanker**: 방어 특화 스킬  
- **Berserker**: 공격 특화 스킬
- **Rogue**: 은신/크리티컬 특화 스킬
- **Mage**: 마법 특화 스킬
- **Paladin**: 지원/회복 특화 스킬

## Skill Conflict Resolution (스킬 충돌 해결)

### 같은 키 바인딩 우선순위:
1. **T키**: 착용 무기에 따른 자동 선택
   - 석궁 > 활 > 지팡이 순서로 우선순위
2. **G키**: 착용 무기에 따른 자동 전환 시스템 (🆕 개선됨)
   - **무기별 우선순위**: 지팡이 → 단검 → 검 → 창 → 둔기
   - **스마트 전환**: 현재 착용한 무기에 맞는 스킬만 활성화
   - **다중 스킬 지원**: 근접 전문가가 여러 G키 스킬 보유 시 자동 전환
3. **H키**: 착용 무기에 따른 자동 선택
   - 둔기만 지원
4. **휠마우스**: 착용 무기에 따른 자동 선택
   - 창만 지원
5. **Y키**: 선택한 직업에 따른 고정 스킬

### 스킬 활성화 조건:
```csharp
// T키 스킬 활성화 조건
if (HasWeapon("Crossbow") && HasSkill("crossbow_Step6_expert"))
    return "crossbow_expert";
else if (HasWeapon("Bow") && HasSkill("bow_Step6_critboost"))
    return "bow_master";
else if (HasWeapon("Staff") && HasSkill("staff_Step6_dual_cast"))
    return "staff_dual_cast";

// G키 스킬 활성화 조건 (🆕 무기별 자동 전환)
// 착용한 무기에 따라 해당 스킬만 검사 - 다른 스킬 보유 여부와 무관하게 동작
if (IsUsingStaff() && HasSkill("staff_Step6_heal"))
    return "staff_heal";
else if (IsUsingDagger() && HasSkill("knife_step9_assassin_heart"))
    return "assassin_heart";
else if (IsUsingSword() && HasSkill("sword_step5_finalcut"))
    return "sword_slash";
else if (IsUsingSpear() && HasSkill("spear_Step5_combo"))
    return "spear_combo";
else if (IsUsingMace() && HasSkill("mace_Step7_fury_hammer"))
    return "mace_fury";

// H키 스킬 활성화 조건
if (HasWeapon("Mace") && HasSkill("mace_Step7_guardian_soul"))
    return "mace_guardian";

// 휠마우스 스킬 활성화 조건
if (HasWeapon("Spear") && HasSkill("spear_Step8_enhanced_throw"))
    return "spear_enhanced_throw";
```

## G키 무기별 자동 전환 시스템 (🆕 신규 기능)

### 핵심 개선사항:
- **문제점 해결**: 이전에는 힐 스킬 보유 시 다른 무기 착용해도 "지팡이 장착 필요" 메시지
- **스마트 로직**: 현재 착용한 무기 타입을 우선 확인 후 해당 스킬만 검사
- **다중 스킬 지원**: 근접 전문가가 여러 G키 스킬을 익혀도 착용 무기에 따라 자동 전환

### 동작 원리:
```csharp
// 기존 방식 (문제점): 스킬 보유 여부 우선 확인
if (hasStaffHeal && !isUsingStaff) {
    DrawFloatingText("지팡이를 장착해야 합니다!");
    return; // 다른 무기 스킬 검사 안함!
}

// 개선된 방식 (해결책): 착용 무기 우선 확인
if (isUsingDagger) {
    if (hasAssassinHeart) {
        ActivateAssassinHeart();
    } else {
        DrawFloatingText("암살자의 심장 스킬이 필요합니다!");
    }
}
```

### 사용자 경험 개선:
- **단검 + 암살자의 심장 + 힐 스킬 보유**: G키 → 암살자의 심장 발동 ✅
- **지팡이 + 힐 스킬 + 암살자의 심장 보유**: G키 → 힐링 발동 ✅
- **검 + 모든 G키 스킬 보유**: G키 → Sword Slash 발동 ✅

### 구체적인 메시지 시스템:
- **지팡이 착용**: "지팡이 힐 스킬이 필요합니다!"
- **단검 착용**: "암살자의 심장 스킬이 필요합니다!"
- **검 착용**: "검 액티브 스킬이 필요합니다!"
- **무기 미착용**: "G키를 지원하는 무기를 착용하세요!"

## Performance Considerations (성능 고려사항)

### 스킬 검증 최적화:
- 스킬 상태를 캐시하여 매번 계산하지 않음
- 무기 변경 시에만 액티브 스킬 목록 갱신
- 키 입력 시 최소한의 검증만 수행
- G키 시스템: 착용 무기 확인 후 해당 스킬만 검사하여 불필요한 연산 제거

### 메모리 관리:
- 액티브 스킬 상태를 static 변수로 관리
- 불필요한 객체 생성 방지
- 스킬 쿨타임을 효율적으로 추적

이 시스템을 통해 다양한 빌드 조합과 전략적 선택을 제공하면서도 밸런스를 유지할 수 있습니다.

---

## 📚 **관련 문서**
- **🏠 메인 규칙**: [../CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 구조
- **🎨 VFX 규칙**: [ZNETSCENE_VFX_RULES.md](ZNETSCENE_VFX_RULES.md) - 액티브 스킬 시각 효과
- **⚙️ MMO 연동**: [MMO_INTEGRATION_GUIDE.md](MMO_INTEGRATION_GUIDE.md) - 스탯 기반 스킬 제한
- **🎯 개발 패턴**: [DEVELOPMENT_PATTERNS.md](DEVELOPMENT_PATTERNS.md) - 액티브 스킬 구현 패턴
- **⚡ 빠른 참조**: [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - 키바인딩 및 제한 요약
# Config 통합 가이드

> 최종 업데이트: 2026-02-28 (중복/충돌 내용 정리 — Section 1/4/5/7 일관성 확보, 파일 분리 구조 명시)
> **CONFIG_RULES.md**, **CONFIG_MANAGEMENT_RULES.md** 통합본 (단일 소스). CLAUDE.md 설정이 최신 기준.

---

## 1. Config 키 명명 규칙

### 기본 형식
```
Tier N_[SkillName]_PropertyName
```
** 실제 효과 구현 되지 않은건 컨피그에 항목에 추가하지 말고 개발자에게 효과 구현되지 않은 항목이 있다고 안내한다.

| 항목 | 규칙 | 예시 |
|------|------|------|
| **카테고리(1차)** | 영어 고정, `"X Tree"` 형식 | `"Bow Tree"`, `"Defense Tree"` |
| **키(2차)** | 영어, Tier 번호 필수 | `Tier 0_[스킬명]_효과`, `Tier 0_[스킬명]_쿨타임`, `Tier 0_[스킬명]_RequiredPoints` |
| **설명** | `GetConfigDescription()` 사용 **필수** | 하드코딩된 영어 문자열 금지 |

> 키(2차) 정렬 순서: 효과 → 수량 → 범위 → 쿨타임 → 필요조건 (RequiredPoints는 같은 Tier 내 최하단)

### 카테고리 목록
```
Attack Tree, Speed Tree, Defense Tree, Production Tree
Bow Tree, Staff Tree, Crossbow Tree
Knife Tree, Sword Tree, Mace Tree, Spear Tree, Polearm Tree
Archer Job Skills, Mage Job Skills, Tanker Job Skills
Rogue Job Skills, Paladin Job Skills, Berserker Job Skills
```
### 티어 구분
1. 구분1
- 다음 티어 배울수 있는 조건 같으면 같은티어 
티어 표시 예시 : Tier 2-1, Tier 2-2, Tier 2-3  같은티어 다른 스킬 구분함
2. 구분2
- 다음 티어 배울수 있는 조건이 다르지만 현재 티어를 배워야 다음 티어를 배울 수 있는 경우 
티어 표시 예시 :  Tier 2-1, Tier 2-2, Tier 2-3  같은티어 다른 스킬 구분함

### 티어 구분이 특별한 스킬트리
1. 생산 전문가 트리구조 및 순서
- Tier 0 : 생산 전문가
- Tier 1 : 기초 일꾼
- Tier 2-1 : 벌목 Lv2
- Tier 2-2 : 채집 Lv2
- Tier 2-3 : 채광 Lv2
- Tier 2-4 : 제작 Lv2
- Tier 3-1 : 벌목 Lv3
- Tier 3-2 : 채집 Lv3
- Tier 3-3 : 채광 Lv3
- Tier 3-4 : 제작 Lv3
- Tier 4-1 : 벌목 Lv4
- Tier 4-2 : 채집 Lv4
- Tier 4-3 : 채광 Lv4
- Tier 4-4 : 제작 Lv4 

2. 방어 전문가 트리구조 및 순서
- Tier 0 : 방어 전문가
- Tier 1 : 피부 경화
- Tier 2-1 : 심신단련
- Tier 2-2 : 체력단련
- Tier 3-1 : 단전호흡
- Tier 3-2 : 회피단련
- Tier 3-3 : 체력증강
- Tier 3-4 : 방패훈련
- Tier 4-1 : 충격파방출
- Tier 4-2 : 발구르기
- Tier 4-3 : 바위피부
- Tier 5-1 : 지구력
- Tier 5-2 : 기민함
- Tier 5-3 : 트롤의 재생력
- Tier 5-4 : 막기달인
- Tier 6-1 : 마인드쉴드
- Tier 6-2 : 신경강화
- Tier 6-3 : 이단점프
- Tier 6-4 : 요툰의 생명력
- Tier 6-5 : 요툰의 방패

### Tier 번호 규칙
| Tier | 용도 | 예시 |
|------|------|------|
| Tier 0 | 전문가 루트 스킬 | `Tier 0_[BowExpert]_AllDamageBonus` |
| Tier 1~9 | 일반 스킬 단계 | `Tier 3_[MultiShot]_TriggerChance` |
| Tier 1~9 | 일반 스킬 단계 | `Tier 4-1_[집중 공격]_공격력(%)` |
| Tier 1~9 | 일반 스킬 단계 | `Tier 4-2_[치명타]_TriggerChance` |
| Tier 1~9 | 일반 스킬 단계 | `Tier 5_[정조준]_크리티컬 데미지(%)` |
| Active_ | 액티브 스킬 속성 | `Active_MultiShot_ArrowConsumption` |
| Passive_ | 패시브 특수 속성 | `Passive_FallDamageReduction` |
| Legacy_ | 더 이상 사용하지 않는 설정 | `Legacy_구르기속도` |

---

## 2. Config 초기화 순서 (SkillTreeConfig.cs Initialize)

**순서 변경 금지**. 다음 순서를 반드시 준수:

```
0. Captain Level System
   Skill_Tree_Base (Language → MoveSpeed_Max → AttackSpeed_Max)
   (구분선: "─ Attack, Speed, Production, Defense Trees ─")
1. 전문가 트리
   Attack_Config → Speed_Config → Defense_Config → Production_Config
   (구분선: "─── Ranged Expert Trees ────")
2. 원거리 무기 트리
   Bow_Config → Staff_Config → Crossbow_Config
   (구분선: "─── Melee Expert Trees ────")

3. 근접 무기 트리
   Knife_Config → Sword_Config → Mace_Config → Spear_Config → Polearm_Config
   (구분선: "─── Job Skill Trees ────")

4. 직업 트리 (반드시 최하단)
   Archer_Config → Mage_Config → Tanker_Config → Rogue_Config → Paladin_Config → Berserker_Config
```

### 구분선 규칙 (CRITICAL)
- ✅ 구분선은 SkillTreeConfig.cs에서만 추가 (개별 Config 파일 내부 금지)
- ✅ 각 구분선마다 **고유한 section 이름** 사용 (BepInEx는 같은 이름을 하나로 합침)
- ❌ 동일한 section 이름 반복 사용 금지

```csharp
// ✅ 올바른 구분선 (각각 다른 section)
BindServerSync(config, "─ Atk, Spd, Production, Def Trees ─", "End", "", "");
BindServerSync(config, "─ Ranged Expert Trees ──────", "End", "", "");
BindServerSync(config, "─ Melee Expert Trees   ──────", "End", "", "");
BindServerSync(config, "─ Job Skill Trees ─────────", "End", "", "");
```

---

## 3. Config 파일 구조

```csharp
public static class {Weapon}_Config
{
    // 1. ConfigEntry 선언
    public static ConfigEntry<float> TierX_SkillName_Property;

    // 2. Value 접근 프로퍼티 (항상 GetEffectiveValue 사용)
    public static float TierX_SkillName_PropertyValue =>
        (float)SkillTreeConfig.GetEffectiveValue("key", TierX_SkillName_Property?.Value ?? 기본값);

    // 3. 초기화 메서드
    public static void Initialize(ConfigFile config)
    {
        TierX_SkillName_Property = SkillTreeConfig.BindServerSync(config,
            "Weapon Tree",                           // 카테고리 (영어 고정)
            "TierX_SkillName_Property",              // 키 (영어 Tier 기반)
            기본값,
            SkillTreeConfig.GetConfigDescription("TierX_SkillName_Property")  // 자동 번역!
        );
    }
}
```

### Config 값 참조 방법
```csharp
// ✅ 올바름 - Value 프로퍼티 사용 (서버 동기화 포함)
float damage = Sword_Config.RushSlash1stDamageRatioValue;

// ❌ 금지 - ConfigEntry 직접 접근
float damage = Sword_Config.RushSlash1stDamageRatio.Value;
```

---

## 4. 스킬 변경 5종 세트 워크플로우

스킬 추가/수정 시 반드시 다음 순서로 진행:

### 1단계: Config 파일 작성
```csharp
// {Weapon}_Config.cs
TierXSkillProperty = SkillTreeConfig.BindServerSync(config,
    "Weapon Tree",
    "TierX_SkillName_Property",
    기본값,
    SkillTreeConfig.GetConfigDescription("TierX_SkillName_Property")
);
```

### 2단계: 툴팁 연동 (동적 참조 필수)
```csharp
// {Weapon}SkillData.cs
Description = $"효과 +{Config.PropertyValue}%"   // ✅ Config 동적 참조
// Description = "효과 +100%"                    // ❌ 하드코딩 금지
```

### 3단계: 효과 구현
- **방법 A (권장)**: Valheim api 맞게 패치 
- **방법 B (예외)**: 직접 패치 (MMO가 지원하지 않는 특수 효과만)

### 완료 체크리스트 (5종 세트)
- [ ] Config 파일에 설정값 정의
- [ ] 툴팁에 `{Config.Value}` 동적 참조
- [ ] 효과에 `GetEffectiveValue()` 사용
- [ ] UI 설명(툴팁)과 실제 효과가 같은 Config 값 사용
- [ ] LogDebug로 추적 로그 추가하고 개발 완료시 제거
- [ ] `DefaultLanguages.cs`에 KO+EN 키 등록 (스킬 노드 이름/설명)
- [ ] `ConfigTranslations.cs`에 DispName + Description 등록 (F1 Config Manager 표시)

---

## 5. Config 다국어 번역 시스템 (최신 - 2026-02-25)

### 번역 파일 분리 원칙

| 파일 | 용도 |
|------|------|
| `Localization/DefaultLanguages.cs` | 스킬트리 UI 전용 (노드 이름, 툴팁, 버프) |
| `Localization/ConfigTranslations.cs` | F1 Config Manager 전용 — **partial class 디스패처** (아래 분리 파일들을 호출) |
| `Localization/ConfigTranslations_KeyNames_KO.cs` | 한국어 2차 항목 표시명 (DispName) |
| `Localization/ConfigTranslations_KeyNames_EN.cs` | 영어 2차 항목 표시명 (DispName) |
| `Localization/ConfigTranslations_ExpertDesc.cs` | 전문가 트리 마우스오버 설명 (Description) |
| `Localization/ConfigTranslations_RangedDesc.cs` | 원거리(Bow/Staff/Crossbow) 마우스오버 설명 |
| `Localization/ConfigTranslations_SwordKnifeDesc.cs` | Sword/Knife 마우스오버 설명 |
| `Localization/ConfigTranslations_HeavyMeleeDesc.cs` | 근접무기(Mace/Spear/Polearm) 마우스오버 설명 |
| `Localization/ConfigTranslations_JobDesc.cs` | 직업 트리 마우스오버 설명 |

> ❌ DefaultLanguages.cs에 Config 키 추가 금지!
> ℹ️ 새 번역 추가 시: DispName은 `_KeyNames_KO/EN.cs`, Description은 해당 무기/직업 Desc 파일에 추가

---

### ConfigTranslations.cs 3개 번역 레이어 (CRITICAL)

F1 Config Manager에는 **3개의 독립적인 번역 레이어**가 있으며, 새 트리 추가 시 **모두 업데이트 필수**:

| 레이어 | 딕셔너리 메서드 | F1 메뉴 표시 위치 | 작업 필요 여부 |
|--------|----------------|------------------|----------------|
| **1차 카테고리** | `GetCategoryTranslations()` | 섹션 헤더 | **영어 고정 — 추가 수정 불필요** |
| **마우스오버 설명** | `GetDescriptionTranslations()` | 항목 호버 시 툴팁 | ✅ 필수 — 누락 시 영어/키 이름 노출 |
| **2차 항목 표시명** | `GetKoreanKeyNames()` / `GetEnglishKeyNames()` | 항목 이름(DispName) | ✅ 필수 — 누락 시 `Tier0_BowExpert_...` 원시 키 노출 |

> ⚠️ **흔한 실수**: Description만 추가하고 `GetKoreanKeyNames()`/`GetEnglishKeyNames()` 누락 → 2차 항목이 `Tier1_RapidFire_Chance` 같은 원시 키로 표시됨

---

### 레이어 1: 카테고리 (영어 고정 — 추가 작업 불필요)
```csharp
// GetKoreanCategories() / GetEnglishCategories() 내부
// BepInEx 제약으로 카테고리는 영어 고정 — 새 트리 추가 시도 영어만 사용!
["Speed Tree"] = "Speed Tree"   // ✅ 영어 통일
["Speed Tree"] = "속도 트리"    // ❌ 한국어 사용 금지
```

---

### 레이어 2: 마우스오버 설명 (Description)

```csharp
// GetKoreanDescriptions() 내부
["Tier0_DefenseExpert_HPBonus"] =
    "【체력 보너스】\n" +
    "방어 전문가 스킬의 체력 증가 보너스입니다.\n" +
    "권장값: 5-10";

// GetEnglishDescriptions() 내부
["Tier0_DefenseExpert_HPBonus"] =
    "【Health Bonus】\n" +
    "Health increase bonus from Defense Expert skill.\n" +
    "Recommended: 5-10";
```

**Description 작성 패턴:**
```
필요 포인트:     【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.
능력치 보너스:   【보너스명 (단위)】\n효과 설명\n활용 방법\n권장값: X-Y
스킬 보너스:     【스킬명 보너스】\n효과 설명\n권장값: X-Y
```

---

### 레이어 3: 2차 항목 표시명 (DispName) ← 자주 누락됨

`BindServerSync()`가 내부적으로 `GetLocalizedKeyName(key)`를 호출해 `ConfigurationManagerAttributes { DispName }` 설정.
이 값이 없으면 원시 키 코드(`Tier1_RapidFire_Chance`)가 그대로 2차 항목 이름으로 표시됨.

```csharp
// GetKoreanKeyNames() 내부 - 한국어 표시명
["Tier1_RapidFire_Chance"] = "Tier 1: [스킬이름]연발 발동 확률 (%)",
["Tier1_RapidFire_ShotCount"] = "Tier 1: [스킬이름]연발 발사 횟수",
["Tier5_DoubleCast_Cooldown"] = "Tier 5: [스킬이름]쿨타임 (초)",

// GetEnglishKeyNames() 내부 - 영어 표시명
["Tier1_RapidFire_Chance"] = "Tier 1: [SkillName]Rapid Fire Trigger Chance (%)",
["Tier1_RapidFire_ShotCount"] = "Tier 1: [SkillName]Rapid Fire Shot Count",
["Tier5_DoubleCast_Cooldown"] = "Tier 5: [SkillName]Cooldown (sec)",
```

**표시명 작성 패턴:**
```
필요 포인트:  "Tier N: [스킬명]필요 포인트"  /  "Tier N: [SkillName]Required Points"
능력치 (수치): "Tier N: 설명 (단위)" /  "Tier N: Description (unit)"
예) "Tier 1: [스킬명]연발 발동 확률 (%)"   /  "Tier 1: [SkillName]Rapid Fire Trigger Chance (%)"
예) "Tier 5: [스킬명]쿨타임 (초)"          /  "Tier 5: [SkillName]Cooldown (sec)"
```

---

### GetConfigDescription() 사용법 (필수)

```csharp
// ✅ 올바름 - 자동 번역 (한국어/영어 전환)
SkillTreeConfig.BindServerSync(config,
    "Defense Tree",
    "Tier0_DefenseExpert_HPBonus",
    5f,
    SkillTreeConfig.GetConfigDescription("Tier0_DefenseExpert_HPBonus")
);

// ❌ 금지 - 하드코딩된 영어 (F1 메뉴에서 번역 안 됨)
SkillTreeConfig.BindServerSync(config,
    "Defense Tree",
    "Tier0_DefenseExpert_HPBonus",
    5f,
    "Tier 0: Defense Expert - Health Bonus"
);
```

### GetConfigDescription() 작동 흐름
```
BindServerSync(config, section, key, value, GetConfigDescription(key))
    ├─ GetLocalizedKeyName(key)          → GetKoreanKeyNames()/GetEnglishKeyNames()
    │       → ConfigurationManagerAttributes { DispName = "Tier 1: 연발 발동 확률 (%)" }
    └─ GetLocalizedDescription(key)      → GetDescriptionTranslations("ko")
            → description 툴팁 텍스트 반환
```

---

### 새 트리(Config) 추가 체크리스트

- [ ] `GetDescriptionTranslations()` - 한국어 Description 추가 (【】 형식)
- [ ] `GetDescriptionTranslations()` - 영어 Description 추가 (【】 형식)
- [ ] `GetKoreanKeyNames()` - 한국어 2차 항목 표시명 추가
- [ ] `GetEnglishKeyNames()` - 영어 2차 항목 표시명 추가
- [ ] Config 파일 - `GetConfigDescription()` 사용 (하드코딩 금지)
- [ ] 빌드 테스트 (경고 0개)
- [ ] F1 메뉴에서 2차 항목명 + 마우스오버 설명 모두 번역 확인


## 6. 툴팁 작성 규칙

### 핵심 원칙
1. **Config 동적 참조 필수**: `{Config.Value}` 형태 (하드코딩 금지)
2. **효과만 표시**: 스토리/배경 설명 금지
3. **심플한 용어** 사용

### 용어 심플화
| ❌ 금지 | ✅ 권장 |
|--------|--------|
| 방어력, 체력 최대치, 스태미나 최대치 | 방어, 체력, 스태미나 |
| 무적 지속시간, 데미지 보너스 | 무적시간, 데미지 |
| 치명타 확률 보너스, 이동 속도 증가 | 치명타 확률, 이동속도 |

### 툴팁 패턴
```csharp
// 단일 효과
Description = $"체력 +{Config.Value}"

// 2개 효과 (쉼표 구분)
Description = $"체력 +{Config.HealthValue}, 방어 +{Config.ArmorValue}"

// 비율 표기
Description = $"회피 +{Config.DodgeValue}%, 무적시간 +{Config.InvincibilityValue}%"

// 액티브 스킬 (키 포함)
Description = $"G키: {Config.Duration}초 돌진 후 데미지 +{Config.Damage}%"

// 3개 이상 (쉼표 구분)
Description = $"블럭 스태미나 -{Config.A}%, 일반방패 이동속도 +{Config.B}%, 대형방패 이동속도 +{Config.C}%"
```

---

## 7. 멀티플레이어 Config 동기화

### 핵심 원칙
- **서버 Config 우선**: 멀티플레이어에서 서버 값이 모든 클라이언트에 적용
- **BepInEx 자동 추적**: 명시적 `Reload()` 불필요, `.Value`로 최신값 자동 접근
- **새 Config 추가 시**: `BroadcastConfigToClients()` 딕셔너리에 반드시 포함

### 효과 구현 시 GetEffectiveValue 사용
```csharp
// ✅ 올바름 - Value 프로퍼티 사용 (내부적으로 서버 Config 우선 적용)
float bonus = Defense_Config.AgileDodgeBonusValue;

// ❌ 금지 - ConfigEntry 직접 접근 (클라이언트 로컬값만 참조)
float bonus = Defense_Config.AgileDodgeBonus.Value;
```

> ℹ️ `AgileDodgeBonusValue`는 `float` 프로퍼티로, 내부에서 `GetEffectiveValue("key", localValue)`를 호출하여 서버 Config를 자동으로 우선 적용합니다.

### 새 Config 추가 후 동기화 체크리스트
- [ ] `BroadcastConfigToClients()` 딕셔너리에 새 Config 추가
- [ ] `GetEffectiveValue()` 사용으로 서버 Config 우선 적용
- [ ] `ReceiveServerConfig()`에서 영향받는 시스템 갱신 추가
- [ ] 브로드캐스트/수신 로그 추가

### 상세 구현 패턴

#### FileSystemWatcher 패턴 (서버)
```csharp
// Initialize()에서 서버 시작 시 설정
if (_isServer)
{
    BroadcastConfigToClients();
    _configFileWatcher = new FileSystemWatcher(directory)
    {
        Filter = fileName,
        NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
    };
    _configFileWatcher.Changed += OnConfigFileChanged;
    _configFileWatcher.EnableRaisingEvents = true;
}

// 파일 변경 감지 시 즉시 전송 (명시적 Reload() 불필요 - BepInEx가 자동 추적)
private static void OnConfigFileChanged(object sender, FileSystemEventArgs e)
{
    if (_isServer) BroadcastConfigToClients();
}
```

#### BroadcastConfigToClients() 패턴
새 Config 추가 시 딕셔너리에 반드시 포함:
```csharp
var configData = new Dictionary<string, string>();
configData["DefenseRootHealth"] = Defense_Config.DefenseRootHealthBonusValue.ToString();
configData["BowStep6CritChance"] = Bow_Config.BowStep6CritBoostCritChanceValue.ToString();
// ⚠️ 새 Config 추가 시 여기에도 추가 필수!
ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, "SkillTreeConfigSync", SerializeConfigData(configData));
```

#### ReceiveServerConfig() 패턴
Config 수신 시 영향받는 시스템 즉시 갱신:
```csharp
public static void ReceiveServerConfig(string configString)
{
    _serverConfigValues = DeserializeConfigData(configString);
    _hasReceivedServerConfig = true;
    var player = Player.m_localPlayer;
    if (player != null)
    {
        SkillEffect.UpdateDefenseDodgeRate(player);
        SkillEffect.UpdateAttackSpeed(player);
        Critical.RecalculateCritChance(player);
    }
}
```

---

## 8. 레거시 및 주의사항

### 레거시 설정 접두사
더 이상 사용하지 않는 Config는 `Legacy_` 접두사:
```
Legacy_구르기속도, Legacy_기본이동속도, Legacy_근접콤보속도
Legacy_지팡이시전속도, Legacy_쿨타임감소
```

### 금지 패턴
```csharp
// ❌ 여러 카테고리로 분산 (단일 카테고리 사용 필수)
config.Bind("Knife Tree", "Tier4_Speed", 2f, "...");
config.Bind("Knife Skills Points", "Tier4_Points", 4, "...");

// ❌ Tier 번호 누락
config.Bind("Bow Tree", "CritBoost_Cooldown", 30f, "...");

// ❌ 직업 스킬을 무기 트리 중간에 배치
Bow_Config.Initialize(config);
Archer_Config.Initialize(config);  // 반드시 최하단으로
```

### 레거시 코드 변경 이력

#### Sword_Config.cs 제거 항목
| 제거된 항목 | 대체 |
|------------|------|
| `SwordSlashDamageRatioValue` | `RushSlash1stDamageRatioValue` 사용 |
| `SwordSlashDurationValue` | `CalculateTotalSkillDuration()` 사용 |
| `SwordSlashSkillData` 구조체 | - |
| `GetSwordSlashData()` 메서드 | - |
| `CalculateSwordSlashDamage()` 메서드 | - |

#### Speed_Config.cs 네이밍 변경
| 변경 전 | 변경 후 |
|---------|---------|
| `Common_구르기속도` | `Legacy_구르기속도` |
| `Common_기본이동속도` | `Legacy_기본이동속도` |
| `Common_근접콤보속도` | `Legacy_근접콤보속도` |
| `Common_지팡이시전속도` | `Legacy_지팡이시전속도` |
| `Common_쿨타임감소` | `Legacy_쿨타임감소` |

#### Bow_Config.cs 전체 재구성 (2026-02-27) — 서브티어 분리 패턴 적용

**제거된 필드** (실제 스킬에 없던 항목):
| 제거 필드 | 비고 |
|----------|------|
| `BowStep4PowerShotKnockbackChance` | RangedSkillData에 미사용 |
| `BowStep4PowerShotKnockbackPower` | RangedSkillData에 미사용 |

SkillTreeConfig.cs의 프록시 프로퍼티도 동시 제거.

**Config 키 전체 리네이밍** (Tier 번호 조정):
| 변경 전 키 | 변경 후 키 |
|-----------|-----------|
| `Tier2_MultishotLv1_ActivationChance` | `Tier1_MultishotLv1_ActivationChance` |
| `Tier2_Multishot_AdditionalArrows` | `Tier1_MultishotLv1_AdditionalArrows` |
| `Tier2_Multishot_ArrowConsumption` | `Tier1_MultishotLv1_ArrowConsumption` |
| `Tier2_Multishot_DamagePerArrow` | `Tier1_MultishotLv1_DamagePerArrow` |
| `Tier2_MultishotLv1_RequiredPoints` | `Tier1_MultishotLv1_RequiredPoints` |
| `Tier3_SpeedShot_SkillBonus` | `Tier2_BowMastery_SkillBonus` |
| `Tier3_SpecialArrow_Chance` | `Tier2_BowMastery_SpecialArrowChance` |
| `Tier3_BowMastery_RequiredPoints` *(신규)* | `Tier2_BowMastery_RequiredPoints` |
| `Tier3_SilentShot_DamageBonus` | `Tier3_SilentStrike_DamageBonus` |
| `Tier4_MultishotLv2_ActivationChance` | `Tier3_MultishotLv2_ActivationChance` |
| `Tier4_PowerShot_KnockbackChance` | 제거 |
| `Tier4_PowerShot_KnockbackDistance` | 제거 |
| `Tier4_MultishotLv2_RequiredPoints` | `Tier3_MultishotLv2_RequiredPoints` |
| `Tier5_HuntingInstinct_CritBonus` | `Tier3_HuntingInstinct_CritBonus` |
| `Tier5_PrecisionAim_CritDamage` | `Tier4_PrecisionAim_CritDamage` |
| `Tier5_ArrowRain_Chance` | `Tier4_ArrowRain_Chance` |
| `Tier5_ArrowRain_ArrowCount` | `Tier4_ArrowRain_ArrowCount` |
| `Tier5_BackstepShot_CritBonus` | `Tier4_BackstepShot_CritBonus` |
| `Tier5_BackstepShot_Duration` | `Tier4_BackstepShot_Duration` |
| `Tier5_PrecisionAim_RequiredPoints` | `Tier4_PrecisionAim_RequiredPoints` |
| `Tier6_ExplosiveArrow_*` | `Tier5_ExplosiveArrow_*` |
| `Tier6_CritBoost_*` | `Tier5_CritBoost_*` |

**신규 추가된 RequiredPoints 필드**:
| 필드명 | Config 키 | 대응 노드 |
|--------|----------|----------|
| `BowFocusShotRequiredPoints` | `Tier1_FocusedShot_RequiredPoints` | bow_Step2_focus |
| `BowSilentStrikeRequiredPoints` | `Tier3_SilentStrike_RequiredPoints` | bow_Step3_silentshot |
| `BowHuntingInstinctRequiredPoints` | `Tier3_HuntingInstinct_RequiredPoints` | bow_Step5_instinct |

> ⚠️ 기존 사용자 Config 값 리셋됨 (키 이름 변경). 개발 단계에서만 허용.

---

## 9. 서브티어 분리 패턴 (Attack Tree 기준, 2026-02-27)

하나의 Tier에 여러 스킬이 있는 경우, RequiredPoints를 공유 1개로 묶지 않고 **스킬별 독립 Config**로 분리한다.

### 언제 적용하는가
- 같은 Tier 안에 **2개 이상 서로 다른 무기/역할의 스킬**이 존재할 때
- F1 Config Manager에서 "Tier 2" 하나로 뭉쳐 보여 구분이 어려울 때

### F1 표시명 규칙

```
Tier N-X: [스킬명] 속성명
```

**[스킬명]은 반드시 해당 SkillNode의 NameKey 표시명과 동일하게 작성해야 한다.**
DefaultLanguages.cs의 NameKey 값을 기준으로 KO/EN 모두 확인 후 작성.

```
// 확인 경로: RangedSkillData.cs → NameKey = "bow_xxx_name"
//           DefaultLanguages.cs → ["bow_xxx_name"] = "실제 스킬명"
```

| ❌ 틀린 예 | ✅ 올바른 예 | 이유 |
|-----------|------------|------|
| `[Multishot Lv1]` | `[Multi-Shot Lv1]` | 툴팁 표기는 하이픈 포함 |
| `[Bow Mastery]` | `[Bow Proficiency]` | 툴팁 NameKey = "Bow Proficiency" |
| `[Silent Strike]` | `[Silent Shot]` | 툴팁 NameKey = "Silent Shot" |
| `[Hunting Instinct]` | `[Hunter's Instinct]` | 툴팁 NameKey = "Hunter's Instinct" |

> ℹ️ 단일 스킬 노드의 **서브 이펙트** (화살비, 백스텝샷, 크리부스트 등)는 자체 NameKey가 없으므로
> 효과를 설명하는 짧은 이름으로 작성 가능 (예: `[화살비]`, `[Crit Boost]`)

| 예시 | KO | EN |
|------|----|----|
| Tier 0 공격 전문가 필요 포인트 | `"Tier 0: [공격 전문가] 필요 포인트"` | `"Tier 0: [Attack Expert] Required Points"` |
| Tier 2-1 근접 특화 발동 확률 | `"Tier 2-1: [근접 특화] 발동 확률 (%)"` | `"Tier 2-1: [Melee Spec] Trigger Chance (%)"` |
| Tier 2-1 근접 특화 필요 포인트 | `"Tier 2-1: [근접 특화] 필요 포인트"` | `"Tier 2-1: [Melee Spec] Required Points"` |
| Tier 4-2 정밀 공격 치명타 확률 | `"Tier 4-2: [정밀 공격] 치명타 확률 (%)"` | `"Tier 4-2: [Precision Attack] Crit Chance (%)"` |

> ⚠️ **주의**: Tier 0, 1, 3, 5처럼 단일 스킬만 있는 Tier의 RequiredPoints도 `GetLocalizedKeyName` fallback에 의존하면 안 됨.
> fallback은 `"Tier0: Required Points"` (공백 없음, 스킬명 없음) 형식으로 표시되므로
> **반드시 `ConfigTranslations_KeyNames_KO.cs` / `_EN.cs`에 명시적으로 등록해야 한다.**

### Attack_Config.cs 구현 패턴

#### 필드 선언 (공유 1개 → 스킬별 N개)
```csharp
// ❌ 이전 방식 - 모든 Tier 2 스킬이 하나의 RequiredPoints 공유
public static ConfigEntry<int> AttackStep2RequiredPoints;

// ✅ 신규 방식 - 스킬별 독립
public static ConfigEntry<int> AttackStep2MeleeRequiredPoints;
public static ConfigEntry<int> AttackStep2BowRequiredPoints;
public static ConfigEntry<int> AttackStep2CrossbowRequiredPoints;
public static ConfigEntry<int> AttackStep2StaffRequiredPoints;
```

#### Value 프로퍼티
```csharp
public static int AttackStep2MeleeRequiredPointsValue =>
    (int)SkillTreeConfig.GetEffectiveValue("attack_step2_melee_required_points",
        AttackStep2MeleeRequiredPoints?.Value ?? 2);
```

#### Initialize() - order 서브티어별 분리
```csharp
// === Tier 2-1: Melee Specialization ===
AttackMeleeBonusChance = SkillTreeConfig.BindServerSync(config,
    "Attack Tree", "Tier2_MeleeSpec_BonusTriggerChance", 20f,
    SkillTreeConfig.GetConfigDescription("Tier2_MeleeSpec_BonusTriggerChance"), order: 48);
AttackMeleeBonusDamage = SkillTreeConfig.BindServerSync(config,
    "Attack Tree", "Tier2_MeleeSpec_MeleeDamage", 10f,
    SkillTreeConfig.GetConfigDescription("Tier2_MeleeSpec_MeleeDamage"), order: 48);
AttackStep2MeleeRequiredPoints = SkillTreeConfig.BindServerSync(config,
    "Attack Tree", "Tier2_MeleeSpec_RequiredPoints", 2,
    SkillTreeConfig.GetConfigDescription("Tier2_MeleeSpec_RequiredPoints"), order: 47);

// === Tier 2-2: Bow Specialization ===
// ... order: 46 / 46 / 45 순으로
```

**order 배치 규칙 (서브티어 내림차순)**:

| 서브티어 | 값 order | RequiredPoints order |
|---------|----------|----------------------|
| N-1 (첫 번째) | 최상위 | 최상위 - 1 |
| N-2 | N-1 - 2 | N-1 - 3 |
| N-3 | N-2 - 2 | N-2 - 3 |
| N-4 | N-3 - 2 | N-3 - 3 |

### Bow Tree 서브티어 order 목록 (2026-02-27 재구성)

| 티어 | 서브티어 | 스킬 키 접두사 | 값 order | RP order |
|------|---------|-------------|----------|----------|
| Tier 0 | 단일 | `Tier0_BowExpert_*` | 60 | 59 |
| Tier 1 | 1-1 집중 사격 | `Tier1_FocusedShot_*` | 50 | 49 |
| Tier 1 | 1-2 멀티샷 Lv1 | `Tier1_MultishotLv1_*` | 48 | 47 |
| Tier 2 | 단일 | `Tier2_BowMastery_*` | 40 | 39 |
| Tier 3 | 3-1 침묵의 일격 | `Tier3_SilentStrike_*` | 32 | 31 |
| Tier 3 | 3-2 멀티샷 Lv2 | `Tier3_MultishotLv2_*` | 30 | 29 |
| Tier 3 | 3-3 사냥 본능 | `Tier3_HuntingInstinct_*` | 28 | 27 |
| Tier 4 | 단일 | `Tier4_PrecisionAim_*`, `Tier4_ArrowRain_*`, `Tier4_BackstepShot_*` | 20 | 19 |
| Tier 5 | 단일 | `Tier5_ExplosiveArrow_*`, `Tier5_CritBoost_*` | 10 | 9 |

### Attack Tree 서브티어 order 목록 (현재 적용 값)

| 티어 | 서브티어 | 스킬 키 | 값 order | RP order |
|------|---------|--------|----------|----------|
| Tier 2 | 2-1 근접 특화 | `Tier2_MeleeSpec_*` | 48 | 47 |
| Tier 2 | 2-2 활 특화 | `Tier2_BowSpec_*` | 46 | 45 |
| Tier 2 | 2-3 석궁 특화 | `Tier2_CrossbowSpec_*` | 44 | 43 |
| Tier 2 | 2-4 지팡이 특화 | `Tier2_StaffSpec_*` | 42 | 41 |
| Tier 4 | 4-1 근접 강화 | `Tier4_MeleeEnhance_*` | 28 | 27 |
| Tier 4 | 4-2 정밀 공격 | `Tier4_PrecisionAttack_*` | 26 | 25 |
| Tier 4 | 4-3 원거리 강화 | `Tier4_RangedEnhance_*` | 24 | 23 |
| Tier 6 | 6-1 약점 공격 | `Tier6_WeakPointAttack_*` | 8 | 7 |
| Tier 6 | 6-2 연속 근접 | `Tier6_ComboFinisher_*` | 6 | 5 |
| Tier 6 | 6-3 양손 분쇄 | `Tier6_TwoHandCrush_*` | 4 | 3 |
| Tier 6 | 6-4 속성 공격 | `Tier6_ElementalAttack_*` | 2 | 1 |

### SkillData.cs 참조 변경

```csharp
// ❌ 이전 - 공유 RequiredPoints
RequiredPoints = Attack_Config.AttackStep2RequiredPointsValue,

// ✅ 신규 - 스킬별 독립
// atk_melee_bonus
RequiredPoints = Attack_Config.AttackStep2MeleeRequiredPointsValue,
// atk_bow_bonus
RequiredPoints = Attack_Config.AttackStep2BowRequiredPointsValue,
// atk_crossbow_bonus
RequiredPoints = Attack_Config.AttackStep2CrossbowRequiredPointsValue,
// atk_staff_bonus
RequiredPoints = Attack_Config.AttackStep2StaffRequiredPointsValue,
```

### 서브티어 분리 체크리스트

- [ ] `*_Config.cs` 필드 선언: 공유 필드 제거 → 스킬별 독립 필드 추가
- [ ] `*_Config.cs` Value 프로퍼티: 동일하게 분리
- [ ] `*_Config.cs` Initialize(): 서브티어별 코멘트 + order 분리
- [ ] `SkillTreeConfig.cs`: 제거된 필드의 프록시 프로퍼티 동시 제거
- [ ] `*SkillData.cs`: RequiredPoints 참조를 독립 프로퍼티로 교체
- [ ] `ConfigTranslations_KeyNames_KO.cs`: 기존 표시명 수정 + RequiredPoints 키 추가
- [ ] `ConfigTranslations_KeyNames_EN.cs`: 동일 (영문, **[스킬명]은 EN 툴팁명 기준**)
- [ ] `ConfigTranslations_RangedDesc.cs` (또는 해당 Desc 파일): 키 이름 변경 반영 + 신규 RequiredPoints 설명 추가
- [ ] [스킬명] 툴팁 일치 검증: DefaultLanguages.cs NameKey와 Config 표시명 대조
- [ ] 빌드 성공 확인 (경고 0, 오류 0)

---

## 관련 문서
- `Localization/ConfigTranslations.cs` - 번역 데이터 소스
- `SkillTree/SkillTreeConfig.cs` - GetConfigDescription(), GetEffectiveValue()
- `md/LOCALIZATION_GUIDE.md` - 스킬트리 UI 로컬라이제이션
- `claudedocs/config_localization_implementation_2025-02-23.md` - 구현 리포트

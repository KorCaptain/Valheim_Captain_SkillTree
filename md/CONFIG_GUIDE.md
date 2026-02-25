# Config 통합 가이드

> 최종 업데이트: 2026-02-25 (CONFIG_RULES.md + CONFIG_MANAGEMENT_RULES.md 완전 통합)
> **CONFIG_RULES.md**, **CONFIG_MANAGEMENT_RULES.md** 통합본 (단일 소스). CLAUDE.md 설정이 최신 기준.

---

## 1. Config 키 명명 규칙

### 기본 형식
```
TierN_SkillName_PropertyName
```

| 항목 | 규칙 | 예시 |
|------|------|------|
| **카테고리(1차)** | 영어 고정, `"X Tree"` 형식 | `"Bow Tree"`, `"Defense Tree"` |
| **키(2차)** | 영어, Tier 번호 필수 | `Tier0_BowExpert_RequiredPoints` |
| **설명** | `GetConfigDescription()` 사용 **필수** | 하드코딩된 영어 문자열 금지 |

### 카테고리 목록
```
Attack Tree, Speed Tree, Defense Tree, Production Tree
Bow Tree, Staff Tree, Crossbow Tree
Knife Tree, Sword Tree, Mace Tree, Spear Tree, Polearm Tree
Archer Job Skills, Mage Job Skills, Tanker Job Skills
Rogue Job Skills, Paladin Job Skills, Berserker Job Skills
```

### Tier 번호 규칙
| Tier | 용도 | 예시 |
|------|------|------|
| Tier0 | 전문가 루트 스킬 | `Tier0_AttackExpert_AllDamageBonus` |
| Tier1~9 | 일반 스킬 단계 | `Tier5_MultiShot_TriggerChance` |
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
BindServerSync(config, "─ Attack, Speed, Production, Defense Trees ─", "End", "", "");
BindServerSync(config, "─ Ranged Expert Trees ────", "End", "", "");
BindServerSync(config, "─ Melee Expert Trees ────", "End", "", "");
BindServerSync(config, "─ Job Skill Trees ", "End", "", "");
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

## 4. 스킬 변경 3단계 워크플로우

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
- **방법 A (권장)**: MMO getParameter 패치 (기본 스탯은 반드시 이 방법)
- **방법 B (예외)**: 직접 패치 (MMO가 지원하지 않는 특수 효과만)

### 3단계 체크리스트
- [ ] Config 파일에 설정값 정의
- [ ] 툴팁에 `{Config.Value}` 동적 참조
- [ ] 효과에 `GetEffectiveValue()` 사용
- [ ] UI 설명과 실제 효과가 같은 Config 값 사용
- [ ] LogDebug로 추적 로그 추가

---

## 5. Config 다국어 번역 시스템 (최신 - 2026-02-25)

### 번역 파일 분리 원칙
| 파일 | 용도 |
|------|------|
| `Localization/DefaultLanguages.cs` | 스킬트리 UI 전용 (노드, 툴팁, 버프) |
| `Localization/ConfigTranslations.cs` | F1 Config Manager 전용 (카테고리 + 설명 + 2차 항목명) |

> ❌ DefaultLanguages.cs에 Config 키 추가 금지!

---

### ConfigTranslations.cs 3개 번역 레이어 (CRITICAL)

F1 Config Manager에는 **3개의 독립적인 번역 레이어**가 있으며, 새 트리 추가 시 **모두 업데이트 필수**:

| 레이어 | 딕셔너리 메서드 | F1 메뉴 표시 위치 | 누락 시 증상 |
|--------|----------------|------------------|-------------|
| **1차 카테고리** | `GetCategoryTranslations()` | 섹션 헤더 | 영어로 표시 |
| **마우스오버 설명** | `GetDescriptionTranslations()` | 항목 호버 시 툴팁 | 영어/키 이름으로 표시 |
| **2차 항목 표시명** | `GetKoreanKeyNames()` / `GetEnglishKeyNames()` | 항목 이름(DispName) | 내부 키 코드(`Tier0_BowExpert_...`)가 그대로 노출 |

> ⚠️ **흔한 실수**: Description만 추가하고 `GetKoreanKeyNames()`/`GetEnglishKeyNames()` 누락 → 2차 항목이 `Tier1_RapidFire_Chance` 같은 원시 키로 표시됨

---

### 레이어 1: 카테고리 번역
```csharp
// GetKoreanCategories() / GetEnglishCategories() 내부
// BepInEx 제약으로 카테고리는 영어 고정!
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
["Tier1_RapidFire_Chance"] = "Tier 1: 연발 발동 확률 (%)",
["Tier1_RapidFire_ShotCount"] = "Tier 1: 연발 발사 횟수",
["Tier5_DoubleCast_Cooldown"] = "Tier 5: 쿨타임 (초)",

// GetEnglishKeyNames() 내부 - 영어 표시명
["Tier1_RapidFire_Chance"] = "Tier 1: Rapid Fire Trigger Chance (%)",
["Tier1_RapidFire_ShotCount"] = "Tier 1: Rapid Fire Shot Count",
["Tier5_DoubleCast_Cooldown"] = "Tier 5: Cooldown (sec)",
```

**표시명 작성 패턴:**
```
필요 포인트:  "Tier N: 필요 포인트"  /  "Tier N: Required Points"
능력치 (수치): "Tier N: 설명 (단위)" /  "Tier N: Description (unit)"
예) "Tier 1: 연발 발동 확률 (%)"   /  "Tier 1: Rapid Fire Trigger Chance (%)"
예) "Tier 5: 쿨타임 (초)"          /  "Tier 5: Cooldown (sec)"
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
// ✅ 멀티플레이어 호환 - 서버 Config 우선 적용
float bonus = SkillTreeConfig.GetEffectiveValue(
    Defense_Config.AgileDodgeBonusValue,
    "DefenseAgileDodge"
);

// ❌ 금지 - 클라이언트 로컬값만 참조
float bonus = Defense_Config.AgileDodgeBonusValue.Value;
```

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

#### Bow_Config.cs 네이밍 수정
| 변경 전 | 변경 후 |
|---------|---------|
| `Tier_멀티샷Lv1_발동확률` | `Tier2_멀티샷Lv1_발동확률` |
| `Tier_멀티샷Lv2_발동확률` | `Tier4_멀티샷Lv2_발동확률` |
| `Tier_멀티샷_추가화살수` | `Tier2_멀티샷_추가화살수` |
| `Tier_멀티샷_화살소모량` | `Tier2_멀티샷_화살소모량` |
| `Tier_멀티샷_화살데미지비율` | `Tier2_멀티샷_화살데미지비율` |

---

## 관련 문서
- `Localization/ConfigTranslations.cs` - 번역 데이터 소스
- `SkillTree/SkillTreeConfig.cs` - GetConfigDescription(), GetEffectiveValue()
- `md/LOCALIZATION_GUIDE.md` - 스킬트리 UI 로컬라이제이션
- `claudedocs/config_localization_implementation_2025-02-23.md` - 구현 리포트

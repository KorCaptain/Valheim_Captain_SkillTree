# CONFIG_MANAGEMENT_RULES.md - 설정 관리 및 동기화 규칙

CaptainSkillTree 모드의 Config 관리, 툴팁 작성, 멀티플레이어 동기화 규칙입니다.

---

## 📚 규칙 목록

- **Rule 7**: 스킬 효과 수정 규칙 (Dynamic Config Integration)
- **Rule 7-1**: Config 통합 및 정렬 순서 규칙 (Config Organization & Ordering)
- **Rule 7-2**: 스킬 툴팁 작성 규칙 (Skill Tooltip Writing Rules)
- **Rule 8**: 멀티플레이어 Config 동기화 시스템 (Server-Client Config Sync)

---

## Rule 7: 스킬 효과 수정 규칙 (Dynamic Config Integration)

### 📋 목적
모든 스킬 수정 시 **Config-툴팁-효과 3단계 연동**을 통해 일관성 있는 개발 보장

### 🎯 핵심 원칙
모든 스킬 수정 시 반드시 다음 3단계를 순서대로 진행해야 합니다:

#### 1단계: Config 파일 작성
**위치**: `{WeaponType}_Config.cs`

```csharp
// 예시: Bow_Config.cs
public static ConfigEntry<float> BowStep6CritBoostCritChanceValue;
public static ConfigEntry<float> BowStep6CritBoostDurationValue;

public static void Initialize(ConfigFile config)
{
    BowStep6CritBoostCritChanceValue = config.Bind(
        "Bow Tree",
        "Tier6_크리티컬부스트_치명타확률",
        100f,
        "Tier 6: 크리티컬 부스트(bow_Step6_critboost) - 치명타 확률 보너스 (%)"
    );

    BowStep6CritBoostDurationValue = config.Bind(
        "Bow Tree",
        "Tier6_크리티컬부스트_지속시간",
        5f,
        "Tier 6: 크리티컬 부스트(bow_Step6_critboost) - 효과 지속 시간 (초)"
    );
}
```

**Config 작성 규칙**:
- 카테고리: `"[Tree Name] Tree"` 형식 (예: "Bow Tree", "Mace Tree")
- 키: `"TierX_스킬명_속성명"` 형식 (Tier 번호 포함 필수)
- 설명: `"Tier X: 스킬명(skill_id) - 속성 설명"` 형식

#### 2단계: 툴팁 연동
**위치**: `{WeaponType}SkillData.cs`

```csharp
// 예시: BowSkillData.cs
new SkillNode
{
    Id = "bow_Step6_critboost",
    Name = "크리티컬 부스트",
    // ✅ Config 참조 형태로 동적 연결
    Description = $"T키: {SkillTreeConfig.BowStep6CritBoostDurationValue}초 동안 치명타 확률 +{SkillTreeConfig.BowStep6CritBoostCritChanceValue}%",
    Tier = 6,
    RequiredPoints = 1
}
```

**툴팁 작성 규칙**:
- ✅ **동적 참조**: `{Config.PropertyName.Value}` 형태 필수
- ❌ **하드코딩 금지**: `"5초 동안 치명타 확률 +100%"` (고정값 사용 금지)

#### 3단계: 효과 구현
**위치**: `SkillEffect.cs` 또는 해당 스킬 파일

**방법 A: MMO getParameter 패치 (Tier 1 - 권장)**
```csharp
[HarmonyPatch(typeof(EpicMMOSystem.LevelSystem), nameof(EpicMMOSystem.LevelSystem.getParameter))]
[HarmonyPriority(Priority.High)]
public static void Postfix(object parameter, ref int __result)
{
    if (parameter?.ToString() == "Strength")
    {
        int bonus = (int)SkillTreeConfig.AttackExpertStrengthValue;
        __result += bonus;
        Plugin.Log.LogDebug($"[MMO 스탯 연동] Strength +{bonus}");
    }
}
```

**방법 B: 직접 패치 (Tier 2 - 특수 효과만)**
```csharp
// 예시: 활 치명타 부스트 (시간 기반 버프)
if (SkillEffect.HasSkill("bow_Step6_critboost"))
{
    if (SkillEffect.bowCritBoostEndTime.TryGetValue(player, out float endTime)
        && Time.time < endTime)
    {
        float critChance = SkillTreeConfig.BowStep6CritBoostCritChanceValue;
        bonus += critChance;
        Plugin.Log.LogDebug($"[크리티컬 부스트] 치명타 확률 +{critChance}%");
    }
}
```

### ✅ 3단계 연동 체크리스트

스킬 추가/수정 시 다음을 확인하세요:
- [ ] **Config 파일**: 동적 설정값 정의 (`{WeaponType}_Config.cs`)
- [ ] **툴팁 연동**: `{Config.PropertyName.Value}` 형태로 참조 (`{WeaponType}SkillData.cs`)
- [ ] **효과 구현**: MMO 패치 또는 직접 패치로 효과 적용 (`SkillEffect.cs`)
- [ ] **일관성 검증**: UI 설명과 실제 효과가 동일한 Config 값 사용
- [ ] **로그 추가**: 효과 적용 시 LogDebug로 추적 가능하도록 설정

### 🚫 금지 사항
- ❌ **하드코딩된 수치**: 툴팁이나 효과에서 고정값 사용 금지
- ❌ **Config 미연동**: Config 없이 직접 수치 입력 금지
- ❌ **분리된 검증 로직**: UI와 백엔드가 다른 값 사용 금지

### 📊 장점
1. **일관성 보장**: UI 설명과 실제 효과가 항상 일치
2. **유지보수 편의**: 한 곳(Config)만 수정하면 전체 반영
3. **서버 동기화**: 멀티플레이어 환경에서 자동 동기화 (Rule 8 참조)
4. **디버깅 용이**: 로그로 Config 값 추적 가능

---

## Rule 7-1: Config 통합 및 정렬 순서 규칙 (Config Organization & Ordering)

### 📋 목적
Config 파일의 일관성과 가독성 향상, Defense Tree 패턴 통일

### 🎯 통합 패턴 (단일 카테고리 사용)

모든 Config는 **단일 Tree 카테고리**를 사용하여 관련 설정을 한 곳에 모읍니다.

```csharp
// ✅ 올바른 예시: 단일 카테고리 사용
PropertyName = config.Bind(
    "[Tree Name] Tree",               // 단일 카테고리명 (예: "Bow Tree", "Mace Tree")
    "TierX_스킬명_속성명",             // Tier 포함 키 (예: "Tier6_크리티컬부스트_쿨타임")
    defaultValue,
    "Tier X: 스킬명(skill_id) - 속성 설명"  // Tier 포함 설명
);
```

**카테고리 명명 규칙**:
- 형식: `"[Tree Name] Tree"` (Tree 접미사 필수)
- 예시: `"Bow Tree"`, `"Mace Tree"`, `"Defense Tree"`, `"Berserker Job Skills"`

**키 명명 규칙**:
- 형식: `"TierX_스킬명_속성명"` (Tier 번호 포함 필수)
- 예시: `"Tier6_크리티컬부스트_쿨타임"`, `"Tier0_단검전문가_백스탭데미지보너스"`

### 📊 Config 초기화 순서 (SkillTreeConfig.cs Initialize 메서드)

**필수 준수 순서**:
```csharp
public static void Initialize(ConfigFile config)
{
    // 1. 전문가 트리 (Attack → Speed → Defense -> Product 순)
    Attack_Config.Initialize(config);   // Attack Tree (공격 전문가)
    Speed_Config.Initialize(config);    // Speed Tree (속도 전문가)
    Defense_Config.Initialize(config);  // Defense Tree (방어 전문가)
    Product_Config.Initialize(config);  // Product Tree (생산 전문가)

    // === 구분선: 전문가 트리 끝 ===
    BindServerSync(config, "──────────────────────────", "전문가 트리 구분선", "", "...");

    // 2. 원거리 무기 트리 (Bow → Staff → Crossbow 순)
    Bow_Config.Initialize(config);                      // Bow Tree (활)
    Staff_Config.InitConfig(config);                    // Staff Tree (지팡이)
    Crossbow_Config.InitializeCrossbowConfig(config);   // Crossbow Tree (석궁)

    // === 구분선: 원거리 무기 트리 끝 ===
    BindServerSync(config, "──────────────────────────", "원거리 무기 트리 구분선", "", "...");

    // 3. 근접 무기 트리 (Knife → Sword → Mace → Spear → Polearm 순)
    Knife_Config.InitializeKnifeConfig(config); // Knife Tree (단검)
    Sword_Config.Initialize(config);            // Sword Tree (검)
    InitializeSwordConfig(config);
    Mace_Config.Initialize(config);             // Mace Tree (둔기)
    Spear_Config.Initialize(config);            // Spear Tree (창)
    Polearm_Config.Initialize(config);          // Polearm Tree (폴암)

    // === 구분선: 근접 무기 트리 끝 ===
    BindServerSync(config, "──────────────────────────", "근접 무기 트리 구분선", "", "...");

    // 4. 직업 트리 (최하단 배치)
    Archer_Config.InitializeArcherConfig(config);       // Archer (궁수)
    Mage_Config.InitializeMageConfig(config);           // Mage (마법사)
    Tanker_Config.InitializeTankerConfig(config);       // Tanker (탱커)
    Rogue_Config.InitializeRogueConfig(config);         // Rogue (로그)
    Paladin_Config.InitializePaladinConfig();           // Paladin (성기사)
    Berserker_Config.InitializeBerserkerConfig();       // Berserker (광전사)
}
```

**순서 변경 금지**: 위 순서를 준수하여 Config 파일에서 일관된 정렬 유지

### 🔲 구분선 규칙

**구분선 위치 규칙**:
- ✅ 구분선은 **트리 그룹 사이**에 배치 (SkillTreeConfig.cs에서만 추가)
- ❌ 개별 Config 파일(Crossbow_Config, Defense_Config 등) 안에 구분선 넣지 않음
- 구분선 카테고리: `"──────────────────────────"` (별도 카테고리로 분리)

### 🔢 Tier 명명 규칙

| Tier | 용도 | 예시 |
|------|------|------|
| **Tier 0** | 전문가 스킬 | `Tier0_단검전문가_백스탭데미지보너스` |
| **Tier 1-9** | 일반 스킬 | `Tier5_화살비_화살3개발사확률`, `Tier6_크리티컬부스트_쿨타임` |
| **특수** | 멀티샷, 스탯 변환 등 | `Tier_스킬명_속성명` 형식 |

### 🚫 금지 사항

#### ❌ 여러 카테고리로 분산
```csharp
// ❌ 잘못된 예시: 여러 카테고리 사용
config.Bind("Knife Tree", "Tier4_빠른공격_데미지", 2f, "...");
config.Bind("Knife Skills Required Points", "Tier4_요구포인트", 4, "...");
// → "Knife Tree" 하나로 통일해야 함
```

#### ❌ 카테고리명에 서브키 포함
```csharp
// ❌ 잘못된 예시: 서브키 포함
config.Bind("Mace.GuardianHeart", "Duration", 10f, "...");
// → "Mace Tree" 카테고리 사용 필수
```

#### ❌ 키에서 Tier 생략
```csharp
// ❌ 잘못된 예시: Tier 번호 누락
config.Bind("Bow Tree", "크리티컬부스트_쿨타임", 30f, "...");
// → "Tier6_크리티컬부스트_쿨타임" 형식 필수
```

#### ❌ 직업 스킬을 무기 트리 중간에 배치
```csharp
// ❌ 잘못된 예시: 직업 스킬이 무기 트리 중간에 위치
Bow_Config.Initialize(config);
Archer_Config.Initialize(config);  // ← 직업 스킬이 중간에 삽입됨
Mace_Config.Initialize(config);
// → 직업 스킬은 반드시 최하단에 배치
```

### ✅ 올바른 Config 예시

```csharp
// Defense_Config.cs - 모범 사례
public static class Defense_Config
{
    public static ConfigEntry<float> DefenseRootHealthBonusValue;
    public static ConfigEntry<float> DefenseRootArmorBonusValue;
    public static ConfigEntry<float> AgileDodgeBonusValue;
    public static ConfigEntry<float> AgileInvincibilityBonusValue;

    public static void Initialize(ConfigFile config)
    {
        // === 단일 카테고리 "Defense Tree" 사용 ===
        DefenseRootHealthBonusValue = config.Bind(
            "Defense Tree",  // 단일 카테고리
            "Tier0_방어전문가_체력보너스",  // Tier 포함 키
            20f,
            "Tier 0: 방어 전문가(defense_root) - 체력 보너스"  // Tier 포함 설명
        );

        DefenseRootArmorBonusValue = config.Bind(
            "Defense Tree",
            "Tier0_방어전문가_방어력보너스",
            5f,
            "Tier 0: 방어 전문가(defense_root) - 방어력 보너스"
        );

        AgileDodgeBonusValue = config.Bind(
            "Defense Tree",
            "Tier3_회피단련_회피율",
            10f,
            "Tier 3: 회피단련(defense_Step3_agile) - 회피율 보너스 (%)"
        );

        AgileInvincibilityBonusValue = config.Bind(
            "Defense Tree",
            "Tier3_회피단련_무적시간",
            20f,
            "Tier 3: 회피단련(defense_Step3_agile) - 구르기 무적시간 보너스 (%)"
        );
    }
}
```

### 📊 체크리스트

새 Config 추가 시 다음을 확인하세요:
- [ ] **단일 카테고리 사용**: `"[Tree Name] Tree"` 형식
- [ ] **Tier 포함 키**: `"TierX_스킬명_속성명"` 형식
- [ ] **초기화 순서 준수**: SkillTreeConfig.cs에서 올바른 위치에 호출
- [ ] **직업 스킬 최하단**: 직업 Config는 반드시 마지막에 초기화
- [ ] **설명 일관성**: `"Tier X: 스킬명(skill_id) - 속성 설명"` 형식

---

## Rule 7-2: 스킬 툴팁 작성 규칙 (Skill Tooltip Writing Rules)

### 📋 목적
게임 내 스킬 설명의 일관성과 가독성 향상

### 🎯 기본 원칙
**회피단련 스타일의 심플한 효과 표시**

### ✅ 올바른 툴팁 작성 패턴

```csharp
// ✅ 올바른 예시 1: 회피단련 (2개 효과, 쉼표 구분)
Description = $"회피 +{Defense_Config.AgileDodgeBonusValue}%, 구르기 무적시간 +{Defense_Config.AgileInvincibilityBonusValue}%"

// ✅ 올바른 예시 2: 방어 전문가 (2개 효과, 쉼표 구분)
Description = $"체력 +{Defense_Config.DefenseRootHealthBonusValue}, 방어 +{Defense_Config.DefenseRootArmorBonusValue}"

// ✅ 올바른 예시 3: 단일 효과
Description = $"체력 +{Config.Value}"

// ✅ 올바른 예시 4: 3개 이상 효과 (쉼표로 가독성 유지)
Description = $"블럭 스태미나 -{Config.BlockStaminaValue}%, 일반 방패 이동속도 +{Config.NormalSpeedValue}%, 대형 방패 이동속도 +{Config.TowerSpeedValue}%"
```

### ❌ 잘못된 툴팁 작성 패턴

```csharp
// ❌ 잘못된 예시 1: 긴 설명/스토리 텍스트
Description = "전체 방어 상승을 위한 첫번째 조건은 바로 '살아남는 것'이다. 전투에서 더 많이 버티기 위해 체력과 방어를 늘린다."
// → 효과만 간결하게: "체력 +20, 방어 +5"

// ❌ 잘못된 예시 2: 하드코딩된 수치
Description = "치명타 확률 +100%, 지속시간 5초"
// → Config 참조: $"치명타 확률 +{Config.CritChance}%, 지속시간 {Config.Duration}초"

// ❌ 잘못된 예시 3: 장황한 용어
Description = $"체력 최대치 +{Config.Value}, 방어력 증가 +{Config.Armor}"
// → 심플하게: $"체력 +{Config.Value}, 방어 +{Config.Armor}"

// ❌ 잘못된 예시 4: 불필요한 설명
Description = "방어력을 증가시킨다."
// → 효과만 표시: "방어 +5"
```

### 📋 필수 규칙

1. **Config 연동 필수**: 모든 수치는 `{Config.PropertyName.Value}` 형태로 동적 참조
2. **효과만 표시**: 스토리/배경 설명 금지, 수치 효과만 간결하게
3. **심플한 용어**: "방어력" → "방어", "체력 최대치" → "체력"
4. **단일 필드**: `Description` 필드만 사용 (LongDescription 등 사용 금지)
5. **% 표기 일관성**: 비율은 항상 `+{value}%` 형태로 표기

### 📖 용어 심플화 가이드

| ❌ 금지 (장황함) | ✅ 권장 (심플) |
|----------------|--------------|
| 방어력 | 방어 |
| 체력 최대치 | 체력 |
| 스태미나 최대치 | 스태미나 |
| 에이트르 최대치 | 에이트르 |
| 무적 지속시간 | 무적시간 |
| 데미지 보너스 | 데미지 |
| 공격력 증가 | 공격력 |
| 치명타 확률 보너스 | 치명타 확률 |
| 이동 속도 증가 | 이동속도 |

### 📊 툴팁 구조 예시

```csharp
// 패턴 1: 단일 효과
Description = $"체력 +{Config.Value}"

// 패턴 2: 2개 효과 (쉼표 구분)
Description = $"체력 +{Config.HealthValue}, 방어 +{Config.ArmorValue}"

// 패턴 3: 비율 + 고정값
Description = $"회피 +{Config.DodgeValue}%, 구르기 무적시간 +{Config.InvincibilityValue}%"

// 패턴 4: 액티브 스킬 (키 바인딩 포함)
Description = $"T키: {Config.Duration}초 동안 치명타 확률 +{Config.CritChance}%"

// 패턴 5: 3개 이상 효과 (가독성 위해 쉼표 구분)
Description = $"블럭 스태미나 -{Config.BlockStaminaValue}%, 일반 방패 이동속도 +{Config.NormalSpeedValue}%, 대형 방패 이동속도 +{Config.TowerSpeedValue}%"

// 패턴 6: 조건부 효과
Description = $"백스탭 공격 데미지 +{Config.BackstabDamage}%"
```

### 🚫 금지 패턴

- ❌ 하드코딩된 수치: `"체력 +20"` → ✅ `$"체력 +{Config.Value}"`
- ❌ 긴 설명: `"전투에서 더 많이 버티기 위해..."` → ✅ 효과만 표시
- ❌ 장황한 용어: `"체력 최대치 +20"` → ✅ `"체력 +20"`
- ❌ 불필요한 설명: `"방어력을 증가시킨다"` → ✅ `"방어 +5"`
- ❌ LongDescription 사용: `Description` 필드만 사용

### ✅ 체크리스트

툴팁 작성 시 다음을 확인하세요:
- [ ] **Config 참조**: `{Config.PropertyName.Value}` 형태 사용
- [ ] **간결함**: 효과만 표시, 스토리/설명 제거
- [ ] **심플한 용어**: "방어력" → "방어", "체력 최대치" → "체력"
- [ ] **% 표기**: 비율은 `+{value}%` 형태
- [ ] **쉼표 구분**: 2개 이상 효과는 쉼표로 구분

---

## Rule 8: 멀티플레이어 Config 동기화 시스템 (Server-Client Config Sync)

### 📋 목적
멀티플레이어 환경에서 서버 관리자의 Config를 모든 클라이언트에 자동 동기화

### 🎯 핵심 원칙

**서버 Config 우선 원칙**: 멀티플레이어 환경에서는 서버 관리자의 Config가 모든 클라이언트에 적용됩니다.

### 🔄 FileSystemWatcher 패턴 (서버)

서버에서 Config 파일 변경을 자동 감지하고 실시간으로 클라이언트에 브로드캐스트합니다.

```csharp
// SkillTreeConfig.cs - 서버 초기화 시
public static void Initialize(ConfigFile config)
{
    // ... Config 초기화 ...

    if (_isServer)
    {
        // 초기 Config 브로드캐스트
        BroadcastConfigToClients();

        // Config 파일 감시 시작
        StartConfigFileWatcher(config);
    }
}

// Config 파일 감시자 시작
private static void StartConfigFileWatcher(ConfigFile config)
{
    string configFilePath = config.ConfigFilePath;
    string directory = Path.GetDirectoryName(configFilePath);
    string fileName = Path.GetFileName(configFilePath);

    _configFileWatcher = new FileSystemWatcher(directory)
    {
        Filter = fileName,
        NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
    };

    _configFileWatcher.Changed += OnConfigFileChanged;
    _configFileWatcher.EnableRaisingEvents = true;

    Plugin.Log.LogInfo("[Config 동기화] 파일 감시 시작");
}

// 파일 변경 감지 시 즉시 전송 (Reload 불필요)
private static void OnConfigFileChanged(object sender, FileSystemEventArgs e)
{
    if (_isServer)
    {
        // BepInEx ConfigFile이 자동으로 최신값 추적
        BroadcastConfigToClients();
        Plugin.Log.LogInfo("[Config 동기화] 파일 변경 감지, 클라이언트에 브로드캐스트");
    }
}
```

**핵심 원리**:
- BepInEx ConfigFile은 **자동으로 파일 변경을 추적** → 명시적 `Reload()` 불필요
- `BroadcastConfigToClients()`는 `.Value`로 현재값을 읽어서 전송
- 파일이 변경되면 즉시 클라이언트에 업데이트 전파

### 📤 서버 Config 브로드캐스트

```csharp
private static void BroadcastConfigToClients()
{
    if (!_isServer) return;

    var configData = new Dictionary<string, string>();

    // === 모든 Config 값을 딕셔너리에 추가 ===
    // Attack Tree
    configData["AttackExpertStrength"] = SkillTreeConfig.AttackExpertStrengthValue.ToString();
    configData["AttackCritChance"] = SkillTreeConfig.AttackCritChanceValue.ToString();

    // Defense Tree
    configData["DefenseRootHealth"] = Defense_Config.DefenseRootHealthBonusValue.ToString();
    configData["DefenseRootArmor"] = Defense_Config.DefenseRootArmorBonusValue.ToString();

    // Bow Tree
    configData["BowStep6CritBoostCritChance"] = SkillTreeConfig.BowStep6CritBoostCritChanceValue.ToString();
    configData["BowStep6CritBoostDuration"] = SkillTreeConfig.BowStep6CritBoostDurationValue.ToString();

    // ... 모든 Config 추가 ...

    // 딕셔너리를 문자열로 직렬화
    string configString = SerializeConfigData(configData);

    // 모든 클라이언트에 RPC 전송
    ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, "SkillTreeConfigSync", configString);

    Plugin.Log.LogInfo($"[Config 동기화] 서버 Config 브로드캐스트 완료 ({configData.Count}개 항목)");
}
```

**중요**: 새 Config 추가 시 반드시 `BroadcastConfigToClients()` 딕셔너리에 포함시켜야 합니다.

### 📥 클라이언트 Config 수신 및 적용

```csharp
// SkillTreeConfig.cs - 클라이언트 Config 수신 시
public static void ReceiveServerConfig(string configString)
{
    _serverConfigValues = DeserializeConfigData(configString);
    _hasReceivedServerConfig = true;

    Plugin.Log.LogInfo($"[Config 동기화] 서버 Config 수신 완료 ({_serverConfigValues.Count}개 항목)");

    // === 영향받는 시스템 즉시 업데이트 ===
    var player = Player.m_localPlayer;
    if (player != null)
    {
        // 예시 1: 회피율 재계산
        SkillEffect.UpdateDefenseDodgeRate(player);

        // 예시 2: 공격속도 재계산
        SkillEffect.UpdateAttackSpeed(player);

        // 예시 3: 치명타 시스템 갱신
        Critical.RecalculateCritChance(player);

        Plugin.Log.LogDebug("[Config 동기화] 플레이어 스탯 업데이트 완료");
    }
}

// Config 값 가져오기 (서버 우선)
public static float GetEffectiveValue(ConfigEntry<float> config, string key)
{
    // 멀티플레이어 클라이언트인 경우 서버 Config 우선
    if (_hasReceivedServerConfig && _serverConfigValues.TryGetValue(key, out string serverValue))
    {
        if (float.TryParse(serverValue, out float value))
        {
            return value;
        }
    }

    // 싱글플레이어 또는 서버인 경우 로컬 Config 사용
    return config.Value;
}
```

### 🔄 Config 적용 예시

```csharp
// 스킬 효과 구현 시 GetEffectiveValue 사용
public static float GetDefenseDodgeBonus(Player player)
{
    if (!HasSkill("defense_Step3_agile")) return 0f;

    // 멀티플레이어에서는 서버 Config 우선 적용
    float dodgeBonus = SkillTreeConfig.GetEffectiveValue(
        Defense_Config.AgileDodgeBonusValue,
        "DefenseAgileDodge"
    );

    return dodgeBonus;
}
```

### 📋 핵심 원칙

1. **BepInEx ConfigFile 자동 추적**: 명시적 `Reload()` 불필요, `.Value`로 최신값 자동 접근
2. **서버 우선 적용**: 모든 스킬 효과는 `GetEffectiveValue()`를 통해 서버 Config 우선 적용
3. **즉시 업데이트**: Config 수신 시 영향받는 시스템 즉시 업데이트
4. **완전한 동기화**: 새 Config 추가 시 `BroadcastConfigToClients()` 딕셔너리에 포함 필수

### ✅ 체크리스트

멀티플레이어 Config 동기화 구현 시 다음을 확인하세요:
- [ ] **서버 브로드캐스트**: `BroadcastConfigToClients()`에 새 Config 추가
- [ ] **GetEffectiveValue 사용**: 모든 스킬 효과에서 서버 Config 우선 적용
- [ ] **시스템 업데이트**: `ReceiveServerConfig()`에서 영향받는 시스템 갱신
- [ ] **RPC 등록**: ZRoutedRpc에 "SkillTreeConfigSync" RPC 등록
- [ ] **로그 추가**: Config 브로드캐스트 및 수신 시 로그 기록

### 🚫 금지 사항

- ❌ **명시적 Reload() 호출**: BepInEx가 자동 추적하므로 불필요
- ❌ **로컬 Config 직접 사용**: 멀티플레이어에서는 `GetEffectiveValue()` 필수
- ❌ **브로드캐스트 딕셔너리 누락**: 새 Config 추가 시 반드시 포함

### 📊 참고 코드 위치

- **서버 브로드캐스트**: `SkillTreeConfig.cs` - `BroadcastConfigToClients()`
- **클라이언트 수신**: `SkillTreeConfig.cs` - `ReceiveServerConfig()`
- **FileSystemWatcher**: `SkillTreeConfig.cs` - `StartConfigFileWatcher()`, `OnConfigFileChanged()`
- **GetEffectiveValue**: `SkillTreeConfig.cs` - `GetEffectiveValue()`

---

## 🔗 관련 문서

- [QUICK_REFERENCE.md](QUICK_REFERENCE.md) - Config 통합 패턴 빠른 참조
- [CLAUDE.md](../CLAUDE.md) - 전체 개발 규칙 목차
- [MMO_INTEGRATION_GUIDE.md](MMO_INTEGRATION_GUIDE.md) - MMO getParameter 패치 (Rule 7 1단계 방법 A)
- [BUILD_ERRORS_GUIDE.md](BUILD_ERRORS_GUIDE.md) - Config 관련 빌드 오류 해결

---

**작성일**: 2025-01-29
**버전**: 1.0
**적용 범위**: Rules 7, 7-1, 7-2, 8

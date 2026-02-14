# Config 시스템 규칙

> 최종 업데이트: 2026-02-13 (v0.1.445)

## 1. Config 키 네이밍 규칙

### 기본 형식
```
Tier{n}_{한글스킬명}_{설정타입}
```

### 예시
| 키 이름 | 설명 |
|---------|------|
| `Tier0_검전문가_공격력보너스` | Tier 0 검 전문가 스킬의 공격력 보너스 |
| `Tier2_집중사격_치명타확률보너스` | Tier 2 집중 사격 스킬의 치명타 확률 |
| `Tier6_폭발화살_R키액티브쿨타임` | Tier 6 폭발화살 액티브 스킬 쿨타임 |

### 설정타입 종류
- `필요포인트` - 스킬 습득에 필요한 포인트
- `공격력보너스` / `데미지보너스` - 공격력 증가 (%)
- `치명타확률보너스` - 크리티컬 확률 증가 (%)
- `쿨타임` - 재사용 대기시간 (초)
- `스태미나소모` - 스태미나 소모량 (%)
- `지속시간` - 효과 지속시간 (초)
- `발동확률` - 효과 발동 확률 (%)

### 레거시 설정
더 이상 사용하지 않는 설정은 `Legacy_` 접두사 사용:
```
Legacy_구르기속도
Legacy_기본이동속도
```

---

## 2. Config 파일 초기화 순서

`SkillTreeConfig.cs`의 `Initialize()` 메서드에서 정의된 순서:

```csharp
// 1. 전문가 트리 (Attack → Speed → Defense → Product 순)
Attack_Config.Initialize(config);   // Attack Tree (공격 전문가)
Speed_Config.Initialize(config);    // Speed Tree (속도 전문가)
Defense_Config.Initialize(config);  // Defense Tree (방어 전문가)
Production_Config.Initialize(config);  // Product Tree (생산 전문가)

// === 구분선: 전문가 트리 끝 ===
BindServerSync(config, "─────────── 공격,속도,생산,방어 트리───────────", "전문가 트리 끝", "", "...");

// 2. 원거리 무기 트리 (Bow → Staff → Crossbow 순)
Bow_Config.Initialize(config);                      // Bow Tree (활)
Staff_Config.InitConfig(config);                    // Staff Tree (지팡이)
Crossbow_Config.InitializeCrossbowConfig(config);   // Crossbow Tree (석궁)

// === 구분선: 원거리 무기 트리 끝 ===
BindServerSync(config, "─────────── 원거리 전문가 트리───────────", "원거리 무기 끝", "", "...");

// 3. 근접 무기 트리 (Knife → Sword → Mace → Spear → Polearm 순)
Knife_Config.InitializeKnifeConfig(config); // Knife Tree (단검)
Sword_Config.Initialize(config);            // Sword Tree (검)
InitializeSwordConfig(config);
Mace_Config.Initialize(config);             // Mace Tree (둔기)
Spear_Config.Initialize(config);            // Spear Tree (창)
Polearm_Config.Initialize(config);          // Polearm Tree (폴암)

// === 구분선: 근접 무기 트리 끝 ===
BindServerSync(config, "─────────── 근접 전문가 트리 ───────────", "근접 무기 끝", "", "...");

// 4. 직업 트리 (최하단 배치)
Archer_Config.InitializeArcherConfig(config);       // Archer (궁수)
Mage_Config.InitializeMageConfig(config);           // Mage (마법사)
Tanker_Config.InitializeTankerConfig(config);       // Tanker (탱커)
Rogue_Config.InitializeRogueConfig(config);         // Rogue (로그)
Paladin_Config.InitializePaladinConfig();           // Paladin (성기사)
Berserker_Config.InitializeBerserkerConfig();       // Berserker (광전사)
```

### 순서 요약
| 순서 | 카테고리 | Config 파일 |
|------|----------|-------------|
| 1 | 전문가 트리 | Attack → Speed → Defense → Product |
| 2 | (구분선) | ─────────── 공격,속도,생산,방어 트리─────────── |
| 3 | 원거리 무기 | Bow → Staff → Crossbow |
| 4 | (구분선) | ─────────── 원거리 전문가 트리─────────── |
| 5 | 근접 무기 | Knife → Sword → Mace → Spear → Polearm |
| 6 | (구분선) | ─────────── 근접 전문가 트리 ─────────── |
| 7 | 직업 | Archer → Mage → Tanker → Rogue → Paladin → Berserker |

### 구분선 규칙 (CRITICAL)
- **각 구분선은 고유한 section 이름 사용**: BepInEx Config는 같은 section을 하나로 합침
- **구분선은 트리 밖에 배치**: 각 트리 그룹 사이에 구분선 추가
- **개별 Config 파일에 구분선 넣지 않음**: SkillTreeConfig.cs에서만 구분선 추가

#### 구분선 형식
| Section (카테고리) | Key |
|-------------------|-----|
| `─────────── 공격,속도,생산,방어 트리───────────` | 전문가 트리 끝 |
| `─────────── 원거리 전문가 트리───────────` | 원거리 무기 끝 |
| `─────────── 근접 전문가 트리 ───────────` | 근접 무기 끝 |

> ⚠️ **주의**: 같은 section 이름을 사용하면 BepInEx가 하나의 카테고리로 합치므로, 반드시 각 구분선마다 **다른 section 이름**을 사용해야 합니다.

---

## 3. Config 파일별 규칙

### 필수 구조
각 Config 파일은 다음 구조를 따름:

```csharp
public static class {Weapon}_Config
{
    // === ConfigEntry 선언 ===
    public static ConfigEntry<int> {Skill}RequiredPoints;
    public static ConfigEntry<float> {Skill}DamageBonus;

    // === Value 접근 프로퍼티 ===
    public static int {Skill}RequiredPointsValue =>
        (int)SkillTreeConfig.GetEffectiveValue("key", {Skill}RequiredPoints?.Value ?? 기본값);

    // === 초기화 메서드 ===
    public static void Initialize(ConfigFile config)
    {
        {Skill}RequiredPoints = SkillTreeConfig.BindServerSync(config,
            "{Category} Tree", "Tier{n}_{스킬명}_필요포인트", 기본값,
            "Tier {n}: {스킬명}({skill_id}) - 필요 포인트");
    }
}
```

### Config 카테고리명
| 카테고리 | 사용 파일 |
|----------|-----------|
| `Speed Tree` | Speed_Config.cs |
| `Attack Tree` | Attack_Config.cs |
| `Defense Tree` | Defense_Config.cs |
| `Bow Tree` | Bow_Config.cs |
| `Staff Tree` | Staff_Config.cs |
| `Crossbow Tree` | Crossbow_Config.cs |
| `Knife Tree` | Knife_Config.cs |
| `Sword Tree` | Sword_Config.cs |
| `Mace Tree` | Mace_Config.cs |
| `Spear Tree` | Spear_Config.cs |
| `Polearm Tree` | Polearm_Config.cs |

---

## 4. 삭제된 레거시 코드

### Sword_Config.cs에서 제거된 항목
- `SwordSlashDamageRatioValue` → `RushSlash1stDamageRatioValue` 사용
- `SwordSlashDurationValue` → `CalculateTotalSkillDuration()` 사용
- `SwordSlashSkillData` 구조체
- `GetSwordSlashData()` 메서드
- `CalculateSwordSlashDamage()` 메서드

### Speed_Config.cs 네이밍 변경
| 변경 전 | 변경 후 |
|---------|---------|
| `Common_구르기속도` | `Legacy_구르기속도` |
| `Common_기본이동속도` | `Legacy_기본이동속도` |
| `Common_근접콤보속도` | `Legacy_근접콤보속도` |
| `Common_지팡이시전속도` | `Legacy_지팡이시전속도` |
| `Common_쿨타임감소` | `Legacy_쿨타임감소` |

### Bow_Config.cs 네이밍 수정
| 변경 전 | 변경 후 |
|---------|---------|
| `Tier_멀티샷Lv1_발동확률` | `Tier2_멀티샷Lv1_발동확률` |
| `Tier_멀티샷Lv2_발동확률` | `Tier4_멀티샷Lv2_발동확률` |
| `Tier_멀티샷_추가화살수` | `Tier2_멀티샷_추가화살수` |
| `Tier_멀티샷_화살소모량` | `Tier2_멀티샷_화살소모량` |
| `Tier_멀티샷_화살데미지비율` | `Tier2_멀티샷_화살데미지비율` |

---

## 5. 주의사항

### 새 Config 추가 시
1. 반드시 `Tier{n}_{한글스킬명}_{설정타입}` 형식 사용
2. `SkillTreeConfig.BindServerSync()` 메서드로 바인딩
3. Value 프로퍼티에서 `SkillTreeConfig.GetEffectiveValue()` 사용
4. 설명(description)에 스킬 ID 명시: `"Tier {n}: {스킬명}({skill_id}) - {설명}"`

### Config 값 참조 시
```csharp
// ✅ 올바른 방법 - Value 프로퍼티 사용
float damage = Sword_Config.RushSlash1stDamageRatioValue;

// ❌ 잘못된 방법 - ConfigEntry 직접 접근
float damage = Sword_Config.RushSlash1stDamageRatio.Value;
```

### 서버 동기화
- 모든 Config는 `BindServerSync`로 바인딩
- 서버 값이 클라이언트에 자동 동기화됨
- 클라이언트에서 변경해도 서버 값으로 덮어씌워짐

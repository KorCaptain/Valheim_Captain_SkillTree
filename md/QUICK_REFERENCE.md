# QUICK_REFERENCE.md - 핵심 규칙 요약

CaptainSkillTree 개발 시 즉시 참조할 수 있는 핵심 규칙 요약입니다.

## 🚫 **절대 금지 (DO NOT)**

### 🔐 **수정 금지 영역**
- **Plugin.cs**: InventoryShowPatch, ShowSkillTreeIcon, TryCreateMMOStyleIcon, AssetBundle 함수들
- **SkillTreeInputListener.cs**: 전체 파일 (UI 토글 및 ESC키 처리)
- **SkillTreeData.cs**: RegisterAll() 함수의 6개 루트 노드

### 🎨 **VFX/SFX 규칙**
- **패시브 스킬**: VFX/SFX 사용 **금지**, 텍스트 표시만
- **커스텀 효과명**: 작동 안 함, **실제 Valheim 효과명만** 사용
- **PlayVFXHybrid() 사용 금지**: 무한 로딩 유발, VFXManager.PlayVFXMultiplayer() 필수

### 💻 **개발 안티패턴**
- **존재하지 않는 메서드 호출**: API 확인 없이 가정 금지
- **Valheim 비표준 명명**: "Wood" → "$item_wood" 사용 필수
- **분리된 검증 로직**: UI와 백엔드 다른 로직 사용 금지
- **체력 보너스를 GetMaxHealth에만 추가 금지**: 힐링 깜빡임 유발
- **비율 체력 보너스 직접 적용 금지**: 고정값 변환 후 m_baseHP에 포함 필수
- **AnimationSpeedManager 리플렉션 사용 금지**: 직접 호출 방식 필수
- **Plugin.Awake에서 AnimationSpeedManager 등록 금지**: Game.Awake Postfix 사용
- **수동 DLL 복사 금지**: 빌드 명령어에 자동 복사 포함 필수
- **빌드 성공 후 게임 폴더 미확인 금지**: "✅ DLL 복사 완료!" 메시지 확인
- **정상 작동 확인 후 디버그 로그 방치 금지**: LogDebug 정리 필수

---

## ✅ **필수 패턴 (MUST DO)**

### ⚙️ **MMO 시스템 연동**
```csharp
// Tier 1 (최우선) - MMO getParameter 패치
[HarmonyPatch] // EpicMMOSystem.LevelSystem.getParameter
[HarmonyPriority(Priority.High)]
public static void Postfix(object parameter, ref int __result)
{
    if (parameter?.ToString() == "Strength")
        __result += SkillTreeManager.Instance.GetStrengthBonus();
}
```

### 🎮 **액티브 스킬 키바인딩**
- **T키**: 원거리 (석궁, 활, 지팡이 이중시전)
- **G키**: 보조형 - 🆕 **무기별 자동 전환**
  - 지팡이 → 힐링, 단검 → 암살자의 심장, 검 → Sword Slash
  - 창 → 연공창, 둔기 → 분노의 망치
- **H키**: 방어형 (둔기 반사, 창 강화 투척)
- **Y키**: 직업 (6개 직업별 고유 스킬)

### 🎯 **스킬 ID 명명 규칙**
```
{weapon}_Step{tier}_{skill_name}
예시: bow_Step6_critboost, mace_Step7_fury_hammer
```

### 📦 **EmbeddedResource 시스템 (Rule 3)**

**목적**: 모든 asset 파일을 DLL에 포함하여 안정적인 배포

**리소스 명명 규칙**:
```
CaptainSkillTree.asset.Resources.{bundle_name}
```

**필수 AssetBundle**:
- `skill_start` - 시작 아이콘
- `skill_node` - 노드 아이콘
- `job_icon` - 직업 아이콘
- `captainskilltreeui` - UI 시스템

**구현 패턴**:
```csharp
// Plugin.cs에서 AssetBundle 로드
using (Stream stream = Assembly.GetExecutingAssembly()
    .GetManifestResourceStream("CaptainSkillTree.asset.Resources.skill_start"))
{
    AssetBundle bundle = AssetBundle.LoadFromStream(stream);
    // 사용...
}
```

**프로젝트 설정**:
- Visual Studio에서 asset 파일 속성 → Build Action: `EmbeddedResource`
- 파일이 DLL에 포함되어 별도 배포 불필요

### ⚡ **공격 속도 시스템 (Rule 14)**

**목적**: AnimationSpeedManager로 공격 애니메이션 속도 제어

**필수 설정** (.csproj):
```xml
<Reference Include="AnimationSpeedManager">
  <HintPath>Lib\AnimationSpeedManager.dll</HintPath>
  <Private>True</Private>  ← 배포 시 DLL 포함 필수!
</Reference>
```

**구현 패턴** (Plugin.cs):
```csharp
[HarmonyPatch(typeof(Game), "Awake")]
public static class CaptainSkillTree_AnimationSpeedManager_Patch
{
    private static bool _attackSpeedHandlerRegistered = false;

    [HarmonyPostfix]
    public static void Postfix(Game __instance)
    {
        if (_attackSpeedHandlerRegistered) return;

        AnimationSpeedManager.Add((character, speed) =>
        {
            if (character is Player player && player.InAttack())
            {
                float bonus = GetTotalAttackSpeedBonus(player);
                if (bonus > 0f)
                    return speed * (1.0 + bonus / 100.0);
            }
            return speed;
        });

        _attackSpeedHandlerRegistered = true;
    }
}
```

**배포 필수 파일**:
- ✅ CaptainSkillTree.dll
- ✅ AnimationSpeedManager.dll (8KB)

### ⚔️ **공격력 증가 패턴 (GetDamage 패치)**
```csharp
// 아이템 툴팁에 공격력 자동 반영
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage))]
public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
{
    // 고정값 보너스 (예: 단검 +2)
    if (__result.m_slash > 0) __result.m_slash += bonusValue;

    // 비율 보너스 (예: 둔기 +25%)
    float multiplier = 1f + (bonusPercent / 100f);
    GetDamageHelper.ApplyPhysicalDamageBonus(ref __result, bonusPercent);

    // ⚠️ 무기별 주 데미지 타입에만 적용 (Rule 11-1)
    // 단검: m_slash, 창/폴암: m_pierce, 둔기: m_blunt
}
```

**장점**:
- 인벤토리 마우스 오버 시 증가된 공격력 표시
- Valheim UI가 자동으로 계산
- LogDebug로 로그, LogInfo로 최종 결과만

### ⚙️ **Config 통합 패턴**
```csharp
// Defense Tree 패턴 준수
PropertyName = config.Bind(
    "[Tree Name] Tree",               // 단일 카테고리명
    "TierX_스킬명_속성명",             // Tier 포함 키
    defaultValue,
    "Tier X: 스킬명(skill_id) - 속성 설명"
);
```

**초기화 순서**:
1. 공통 트리 (Attack)
2. 방어 트리 (Defense) - Attack Tree 바로 아래
3. 속도/생산 트리 (Speed, Production)
4. 무기 트리 (Bow → Staff → Knife → Sword → Mace → Spear → Polearm)
5. 직업 트리 (Archer → Mage → Tanker → Rogue → Paladin → Berserker, 최하단)
   - Berserker 카테고리: "Berserker Job Skills"

**금지 사항**:
- ❌ 여러 카테고리 분산
- ❌ 카테고리명에 서브키 (예: "Mace.GuardianHeart")
- ❌ 직업 스킬을 무기 트리 중간에 배치

### 💊 **체력 보너스 구현 패턴**
```csharp
// 고정 체력 보너스 → GetTotalFoodValue
[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
public static void Postfix(Player __instance, ref float hp, ref float stamina)
{
    float hpBonus = 0f;

    // 고정값 보너스 (예: +20 HP)
    if (HasSkill("defense_Step1_survival"))
        hpBonus += Defense_Config.SurvivalHealthBonusValue;

    // 비율 보너스는 고정값으로 변환
    if (HasSkill("defense_Step6_body"))
    {
        float baseHealth = hp + hpBonus;  // 현재까지의 총 기본 체력
        float bonusPercent = Defense_Config.BodyHealthBonusValue / 100f;
        float bonusHealth = baseHealth * bonusPercent;  // 고정 수치 계산
        hpBonus += bonusHealth;  // m_baseHP에 포함
    }

    hp += hpBonus;  // 전체 보너스를 기본 체력에 추가
}
```

**핵심**: 모든 체력 보너스는 `m_baseHP`에 포함되어야 힐링 정상 작동

### 🎯 **치명타 구현 패턴**
```csharp
// Critical.cs - 확률 계산
public static float GetBowCritChance(Player player)
{
    float bonus = 0f;

    // 공통 보너스 (모든 무기 +5%)
    bonus += GetCommonCritChanceBonus(player);

    // 무기별 패시브 스킬
    if (HasSkill("bow_Step5_precision"))
        bonus += Bow_Config.PrecisionCritChanceValue;

    return bonus;
}

// CriticalDamage.cs - 피해 계산
public static float GetBowCritDamage(Player player)
{
    float bonus = 0f;

    // 공통 보너스 (모든 무기 +7%)
    bonus += GetCommonCritDamageBonus(player);

    // 무기별 패시브 스킬
    if (HasSkill("bow_Step6_critboost"))
        bonus += Bow_Config.CritBoostDamageValue;

    return bonus;
}
```

**핵심**: 공통 보너스 + 무기별 보너스 = 최종 효과

### 🎯 **회피 시스템 구현 패턴 (Rule 15)**

**3가지 핵심 요소**: 회피 확률, 구르기 무적시간, 구르기 스태미나

#### **회피 확률 (SetCustomDodgeChance)**
```csharp
// SkillEffect.StatTree.cs - UpdateDefenseDodgeRate()
public static void UpdateDefenseDodgeRate(Player player)
{
    float totalDodge = 0f;

    // 방어 트리 회피 스킬 (가산 방식)
    if (HasSkill("defense_Step3_agile")) // 회피단련 +15%
        totalDodge += Defense_Config.AgileDodgeBonusValue / 100f;

    if (HasSkill("defense_Step5_stamina")) // 기민함 +10%
        totalDodge += Defense_Config.StaminaDodgeBonusValue / 100f;

    if (HasSkill("defense_Step6_attack")) // 신경강화 +8%
        totalDodge += Defense_Config.AttackDodgeBonusValue / 100f;

    // 단검 트리 회피 스킬 (무조건 적용)
    if (HasSkill("knife_step2_evasion")) // 회피 숙련 +15%
        totalDodge += Knife_Config.KnifeEvasionBonusValue / 100f;

    // Valheim 표준 API 사용
    player.SetCustomDodgeChance(totalDodge);
}
```

#### **구르기 무적시간 (Dodge Invincibility)**
```csharp
// SkillEffect.DefenseTree.cs - Player_Dodge_DefenseTree_Patch.Postfix
[HarmonyPatch(typeof(Player), "Dodge")]
public static void Postfix(Player __instance)
{
    // 회피단련: 무적시간 +20% (배율 방식)
    if (HasSkill("defense_Step3_agile"))
    {
        float bonus = Defense_Config.AgileInvincibilityBonusValue / 100f;
        var traverse = Traverse.Create(__instance);
        float current = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
        traverse.Field("m_dodgeInvincibilityTimer").SetValue(current * (1f + bonus));
    }

    // 회피 숙련: 무적시간 +15% (단검 착용 시만, 배율 방식)
    if (HasSkill("knife_expert") && HasSkill("knife_step2_evasion") &&
        SkillEffect.IsUsingDagger(__instance))
    {
        float bonus = Knife_Config.KnifeEvasionBonusValue / 100f;
        var traverse = Traverse.Create(__instance);
        float current = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
        traverse.Field("m_dodgeInvincibilityTimer").SetValue(current * (1f + bonus));
    }
}
```

#### **구르기 스태미나 (Dodge Stamina)**
```csharp
// SkillEffect.DefenseTree.cs - Player_Dodge_DefenseTree_Patch.Prefix
[HarmonyPatch(typeof(Player), "Dodge")]
public static void Prefix(Player __instance)
{
    // 기민함: 구르기 스태미나 -12% (감소 배율)
    if (HasSkill("defense_Step5_stamina"))
    {
        float reduction = Defense_Config.StaminaRollStaminaReductionValue / 100f;
        var traverse = Traverse.Create(__instance);
        float original = traverse.Field("m_dodgeStaminaUsage").GetValue<float>();
        traverse.Field("m_dodgeStaminaUsage").SetValue(original * (1f - reduction));
    }
}
```

**핵심**:
- ✅ 회피 확률: **가산 방식** (0.15 + 0.10 = 0.25 = 25%)
- ✅ 무적시간: **배율 방식** (기본 * 1.2 * 1.15 = 38% 증가)
- ✅ 스태미나: **감소 배율** (기본 * 0.88 = 12% 감소)
- ✅ `SetCustomDodgeChance()` 사용 - Valheim 표준 API
- ✅ 단검 회피는 **조건부 적용** (착용 시만)

---

## 🛡️ **방어력 / 방패 시스템 개발 패턴** (Rule 17)

### 핵심 개념
- **방어력(Armor)**: 투구, 가슴, 다리 등 갑옷의 피해 감소 수치
- **방패 방어력(Block Power)**: 방패로 막을 때 흡수 가능한 데미지량
- **절대 혼동 금지**: 두 시스템은 **완전히 독립적**

### 1️⃣ 방어력 (Armor) 패턴

#### API
```csharp
Character.GetBodyArmor()  // 갑옷 방어력 반환
```

#### 고정값 방어력 보너스
```csharp
[HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
public static class YourSkill_ArmorPatch
{
    [HarmonyPriority(Priority.Low)]  // MMO 이후
    public static void Postfix(Character __instance, ref float __result)
    {
        if (__instance is not Player player) return;

        var manager = SkillTreeManager.Instance;
        if (manager?.GetSkillLevel("your_skill_id") > 0)
        {
            __result += YourConfig.ArmorBonusValue;  // 고정값 추가
        }
    }
}
```

#### 비율 방어력 보너스
```csharp
// defense_Step4_tanker: 방어력 +12%
if (manager.GetSkillLevel("defense_Step4_tanker") > 0)
{
    float multiplier = 1f + (Defense_Config.TankerArmorBonusValue / 100f);
    __result *= multiplier;  // 1.12 = 12% 증가
}
```

### 2️⃣ 방패 방어력 (Block Power) 패턴

#### API
```csharp
ItemDrop.ItemData.GetBlockPower(int quality)  // 방패 막기력 반환
```

#### 방패 방어력 보너스 (올바른 구현)
```csharp
[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetBlockPower))]
public static class ShieldBlockPower_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
    {
        // 1. 방패가 아니면 무시
        if (__instance.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield)
            return;

        var player = Player.m_localPlayer;
        if (player == null) return;

        // 2. 현재 장착한 방패인지 확인
        if (player.GetLeftItem() != __instance)
            return;

        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float blockPowerBonus = 0f;

        // defense_Step3_shield: 방패훈련 (+100)
        if (manager.GetSkillLevel("defense_Step3_shield") > 0)
        {
            blockPowerBonus += Defense_Config.ShieldTrainingBlockPowerBonusValue;
        }

        // defense_Step5_parry: 막기달인 (+100)
        if (manager.GetSkillLevel("defense_Step5_parry") > 0)
        {
            blockPowerBonus += Defense_Config.ParryMasterBlockPowerBonusValue;
        }

        __result += blockPowerBonus;
    }
}
```

### ⚠️ 현재 구현 문제점

| 스킬 | 툴팁 | 현재 API | 문제 |
|------|------|---------|------|
| defense_Step3_shield | "방패 방어력 +100" | GetBodyArmor() | ❌ 갑옷에 추가됨 |
| defense_Step5_parry | "방패 방어력 +100" | GetBodyArmor() | ❌ 갑옷에 추가됨 |

**수정 필요**: 위 패턴대로 GetBlockPower() 패치로 이전

### 3️⃣ 방패 이동속도 패턴 (SE_Stats)

```csharp
// SE_StatTreeSpeed.UpdateStatusEffect() 내부
if (manager.GetSkillLevel("defense_Step6_true") > 0)
{
    var leftItem = player.GetLeftItem();
    if (leftItem?.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
    {
        string shieldName = leftItem.m_shared.m_name;

        // 대형 방패 (+10%)
        if (shieldName.Contains("Tower"))
            totalSpeedBonus += 0.10f;
        else  // 일반 방패 (+5%)
            totalSpeedBonus += 0.05f;
    }
}

m_speedModifier = totalSpeedBonus;
```

### 핵심 원칙
- ✅ **방어력**: `GetBodyArmor()` 패치
- ✅ **방패 방어력**: `GetBlockPower()` 패치 + 방패 타입 체크
- ✅ **툴팁 일관성**: "방패 방어력"이라고 쓰면 반드시 GetBlockPower() 사용
- ✅ **장착 확인**: `player.GetLeftItem()` 체크 필수
- ❌ 방패 효과를 GetBodyArmor()에 추가 금지

---

## ⚡ **에이트르 / 비틀거림 시스템 개발 패턴** (Rule 18, 19)

### 핵심 개념
- **에이트르(Eitr)**: 마법 자원, GetTotalFoodValue 패치로 증가 ✅ 검증 완료
- **비틀거림(Stagger)**: 기절 상태, IsStaggering() API 사용 ⚠️ 검증 필요

### 1️⃣ 에이트르 최대치 증가 ✅

#### API
```csharp
Player.GetTotalFoodValue(out float hp, out float stamina, out float eitr)
Player.GetMaxEitr()
Player.UseEitr(float eitr)
```

#### 에이트르 증가 패턴
```csharp
[HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
public static class Player_GetTotalFoodValue_Eitr_Patch
{
    [HarmonyPriority(Priority.Low)]
    public static void Postfix(ref float eitr)
    {
        var manager = SkillTreeManager.Instance;
        if (manager == null) return;

        float eitrBonus = 0f;

        // 스킬 체크
        if (manager.GetSkillLevel("your_skill_id") > 0)
        {
            eitrBonus += YourConfig.EitrBonusValue;  // 고정값
        }

        if (eitrBonus > 0)
        {
            eitr += eitrBonus;
        }
    }
}
```

#### 에이트르 소모 (액티브 스킬)
```csharp
// 고정량 소모
float eitrCost = 50f;
if (player.GetEitr() < eitrCost)
{
    ShowMessage(player, "에이트르가 부족합니다.");
    return;
}
player.UseEitr(eitrCost);

// 최대치 비율 소모 (30%)
float eitrCost = player.GetMaxEitr() * 0.3f;
player.UseEitr(eitrCost);
```

### 2️⃣ 비틀거림 추가 피해 ⚠️

#### API (검증 필요)
```csharp
Character.IsStaggering()  // ⚠️ 게임 내 테스트 필요
```

#### 비틀거림 피해 증가 패턴
```csharp
// Humanoid.Damage 패치에서 구현
if (weaponType == Skills.SkillType.Knives)
{
    int lv = SkillTreeManager.Instance.GetSkillLevel("knife_stagger");

    // ⚠️ IsStaggering() 검증 필요
    if (lv > 0 && __instance.IsStaggering())
    {
        float staggerBonus = 30f;  // Config 값
        float bonus = 1f + (staggerBonus / 100f);  // 1.3배
        hit.m_damage.m_pierce *= bonus;
    }
}
```

### ⚠️ 비틀거림 검증 필요 사항
```yaml
검증_필요:
  - [ ] Character.IsStaggering() 메서드 존재?
  - [ ] 비틀거림 상태 정상 감지?
  - [ ] 다양한 적 유형 작동? (인간형/동물/보스)
  - [ ] 멀티플레이어 동기화?
```

**검증 방법**:
1. 둔기로 적 비틀거림 유발
2. 디버그 로그로 IsStaggering() 값 확인
3. 추가 피해 정상 적용 확인

### 현재 구현 상태

| 시스템 | 스킬 예시 | API | 검증 |
|--------|----------|-----|------|
| 에이트르 | defense_Step2_dodge | GetTotalFoodValue | ✅ |
| 에이트르 | defense_Step3_breath | GetTotalFoodValue | ✅ |
| 에이트르 | staff_Step2_stream | GetTotalFoodValue | ✅ |
| 비틀거림 | knife_stagger | IsStaggering() | ⚠️ |

### 핵심 원칙
- ✅ **에이트르**: GetTotalFoodValue Postfix, 고정값 방식
- ⚠️ **비틀거림**: IsStaggering() 검증 완료 후 사용
- ✅ **Config 기반**: 하드코딩 금지
- ❌ GetMaxEitr() 직접 패치 금지

---

## 🔧 **빌드 & 개발**

### 📋 **빌드 명령어 (자동 DLL 복사 포함)**
```bash
# Debug 빌드 + 자동 복사
cd CaptainSkillTree && dotnet build Captain_SkillTree.csproj -c Debug && cp -f "/c/Users/ssuny/Desktop/Cusor_data/bin/CaptainSkillTree.dll" "/c/Users/ssuny/AppData/Roaming/r2modmanPlus-local/Valheim/profiles/Cusor_1/BepInEx/plugins/Captain_Skilltree/" && echo "✅ DLL 복사 완료!"

# Release 빌드 + 자동 복사
cd CaptainSkillTree && dotnet build Captain_SkillTree.csproj -c Release && cp -f "/c/Users/ssuny/Desktop/Cusor_data/bin/CaptainSkillTree.dll" "/c/Users/ssuny/AppData/Roaming/r2modmanPlus-local/Valheim/profiles/Cusor_1/BepInEx/plugins/Captain_Skilltree/" && echo "✅ DLL 복사 완료!"
```

**빌드 결과**: `C:\Users\ssuny\Desktop\Cusor_data\bin\CaptainSkillTree.dll`
**게임 폴더**: `C:\Users\ssuny\AppData\Roaming\r2modmanPlus-local\Valheim\profiles\Cusor_1\BepInEx\plugins\Captain_Skilltree\`

### ⚖️ **개발 제한**
- **800라인 제한**: 새 파일은 800라인 이하
- **한국어 대화**: 항상 한국어로 응답
- **로깅 우선**: 각 메서드마다 단계별 로그

### 🔄 **개발 워크플로우**
1. **코드 수정 → 즉시 빌드 + 자동 복사** (위 명령어 사용)
2. **빌드 성공 확인** (0 오류, 0 경고)
3. **복사 완료 확인** ("✅ DLL 복사 완료!" 메시지)
4. **게임 내 테스트** - Valheim 재시작 후 기능 검증
5. **디버그 로그 정리** - 정상 작동 확인 시 LogDebug 제거
6. **최종 배포** - Release 빌드 + 자동 복사

---

## 🎨 **UI 렌더링 순서**
```csharp
// 필수 준수 순서 (위에서부터)
bgObj.transform.SetSiblingIndex(0);        // 배경
line.transform.SetSiblingIndex(1);         // 연결선
nodeObj.transform.SetSiblingIndex(2);      // 일반 노드
jobNodeObj.transform.SetSiblingIndex(3);   // 직업 아이콘
tooltipObj.transform.SetAsLastSibling();   // 툴팁 (최상위)
```

---

## 🎭 **전문가 제한 시스템**
- **원거리 전문가**: 액티브 스킬 1개만 (활 OR 석궁)
- **근접 전문가**: 액티브 스킬 1개만 → 🆕 **G키는 무기별 자동 전환**
- **지팡이/둔기 전문가**: 같은 무기류 2개 모두 가능
- **직업 슬롯**: 1개만 선택 가능

---

## 🔄 **G키 자동 전환 시스템** (🆕 신규)

### 🎯 **동작 원리**
```
착용 무기 확인 → 해당 스킬 검사 → 발동 또는 에러 메시지
❌ 이전: 스킬 보유 여부 우선 → 잘못된 무기 시 "지팡이 장착 필요"
✅ 현재: 무기 타입 우선 → 정확한 무기별 가이드 메시지
```

### 📋 **실제 동작 예시**
- **근접 전문가가 힐+암살자의 심장 보유**:
  - 단검 착용 + G키 → 암살자의 심장 ✅
  - 지팡이 착용 + G키 → 힐링 ✅

---

## 🔍 **VFX 사용 패턴**
```csharp
// VFXManager.PlayVFXMultiplayer() 필수 (무한 로딩 방지)
VFXManager.PlayVFXMultiplayer("buff_02a", player.transform.position, Quaternion.identity);

// ❌ 금지: PlayVFXHybrid() 또는 직접 Instantiate()
```

### 🎪 **등록된 주요 VFX**
- `buff_02a` - 버프 효과
- `healing` - 힐링 효과 (지속, 종료 코드 필요)
- `debuff` - 디버프 효과 (지속, 종료 코드 필요)
- `hit_01~04` - 타격 효과
- `taunt` - 도발 효과

---

## 📚 **상세 규칙 문서 링크**

### 🎯 **필수 규칙 (Core Rules)**
- **[스킬 명명 및 키바인딩](SKILL_NAMING_RULES.md)** - Rule 4, 5, 6
- **[데미지 시스템](DAMAGE_SYSTEM_RULES.md)** - Rule 11, 11-1, 13
- **[설정 관리](CONFIG_MANAGEMENT_RULES.md)** - Rule 7, 7-1, 7-2, 8
- **[체력 시스템](HEALTH_SYSTEM_RULES.md)** - Rule 9
- **[치명타 시스템](CRITICAL_SYSTEM_RULES.md)** - Rule 12
- **[UI 시스템](UI_SYSTEM_RULES.md)** - Rule 10

### 📖 **시스템별 가이드**
- **[MMO 연동](MMO_INTEGRATION_GUIDE.md)** - Rule 1
- **[VFX/사운드](VFX_SOUND_INFINITE_LOADING_FIX.md)** - Rule 2
- **[액티브 스킬](ACTIVE_SKILL_SYSTEM.md)** - Rule 5 상세
- **[빌드 오류](BUILD_ERRORS_GUIDE.md)** - 일반적인 오류 해결
- **[코어 보호](CORE_PROTECTION_README.md)** - 수정 금지 영역

---

## 🚨 **긴급 상황 체크리스트**

### 아이콘 미표시 문제
1. `Harmony.PatchAll()` 성공 확인
2. `InventoryShowPatch` 등록 여부 확인
3. MMO 패치 `TargetMethod()` 오류 점검
4. EmbeddedResource 등록 확인 (Build Action: EmbeddedResource)
5. AssetBundle 로드 성공 여부 로그 확인

### 빌드 오류 시
1. 매개변수 이름 오류 → Valheim 원본 이름 사용
2. 네임스페이스 중복 → 단순 참조로 변경
3. 보호된 멤버 접근 → public 멤버 사용
4. 컴파일 오류 우선 해결 → 경고는 나중에
5. 작은 단위 수정 후 빌드 → 대량 수정 금지

### 효과 작동 안됨
1. ZNetScene 등록 확인
2. 실제 Valheim 효과명 사용 확인
3. EmbeddedResource 등록 확인
4. MMO getParameter 패치 우선 사용 확인
5. 로그에서 패치 적용 여부 확인

### 힐링 깜빡임 발생
1. 체력 보너스가 `GetTotalFoodValue`에서 구현되었는지 확인
2. `m_baseHP`에 포함되었는지 확인 (GetMaxHealth에만 추가 금지)
3. 비율 보너스가 고정값으로 변환되었는지 확인
4. 로그에서 기본 체력, 보너스 체력, 최대 체력 추적

### 치명타 작동 안됨
1. `Critical.cs`의 `GetXXXCritChance()` 함수 확인
2. `CriticalDamage.cs`의 `GetXXXCritDamage()` 함수 확인
3. 공통 보너스 `GetCommonCritChanceBonus()` 호출 확인
4. WeaponCriticalSystemPatch에서 무기 타입 매칭 확인
5. 로그에서 치명타 확률/피해 계산 결과 확인

### 데미지 타입 문제
1. 물리 데미지: pierce, blunt, slash, chop 4종 적용 확인 (전투용)
2. 속성 데미지: fire, frost, lightning, poison, spirit 5종 적용 확인
3. 무기별 주 데미지 타입만 공격력 보너스 적용 확인
   - 단검/검: m_slash
   - 창/폴암: m_pierce
   - 둔기: m_blunt
4. GetDamage 패치에서 주 데미지 타입만 증가하는지 확인
5. 로그에서 데미지 타입별 보너스 추적

### Config 동기화 문제
1. FileSystemWatcher가 서버에서 시작되었는지 확인
2. `BroadcastConfigToClients()`가 호출되는지 확인
3. 클라이언트에서 `ReceiveServerConfig()` 호출 확인
4. `GetEffectiveValue()`로 서버 Config 우선 적용 확인
5. 새 Config 항목이 브로드캐스트 딕셔너리에 포함되었는지 확인

### VFX 무한 로딩 문제
1. `PlayVFXHybrid()` 사용 금지 확인
2. `VFXManager.PlayVFXMultiplayer()` 사용 확인
3. 직접 `Instantiate()` 호출 금지 확인
4. VFX 프리팹 이름 확인 (실제 Valheim 효과명)
5. 패시브 스킬에서 VFX 사용하지 않는지 확인

---

## 💡 **자주 사용하는 코드 패턴**

### 스킬 보유 확인
```csharp
var manager = SkillTreeManager.Instance;
if (manager == null) return;

if (manager.GetSkillLevel("skill_id") > 0)
{
    // 스킬 효과 적용
}
```

### Config 값 읽기
```csharp
// 정적 Config 사용
float bonusValue = Defense_Config.SurvivalHealthBonusValue;

// 멀티플레이어 환경에서는 GetEffectiveValue() 사용 (서버 Config 우선)
float effectiveValue = SkillTreeConfig.GetEffectiveValue(
    Defense_Config.SurvivalHealthBonusValue,
    "Defense_SurvivalHealthBonus"
);
```

### 무기 타입 확인
```csharp
ItemDrop.ItemData currentWeapon = player.GetCurrentWeapon();
if (currentWeapon == null) return;

bool isBow = currentWeapon.m_shared.m_skillType == Skills.SkillType.Bows;
bool isKnife = currentWeapon.m_shared.m_skillType == Skills.SkillType.Knives;
bool isSword = currentWeapon.m_shared.m_skillType == Skills.SkillType.Swords;
```

### 로그 작성
```csharp
// 디버그 로그 (상세 정보)
Plugin.Log.LogDebug($"[스킬명] 변수명: {value:F2}");

// 정보 로그 (최종 결과만)
Plugin.Log.LogInfo($"[스킬명] 최종 효과: {totalBonus}%");

// 경고 로그 (예외 상황)
Plugin.Log.LogWarning($"[스킬명] 경고: {message}");
```

---

*빠른 참조를 위해 이 문서를 북마크하세요!*

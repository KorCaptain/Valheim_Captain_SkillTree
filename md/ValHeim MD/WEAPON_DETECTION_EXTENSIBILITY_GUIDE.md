# 무기 감지 시스템 확장성 가이드

다른 모드와의 호환성을 위한 무기 감지 시스템 개선 사항

## 개선된 무기 감지 함수들

모든 무기 감지 함수가 3단계 우선순위 시스템으로 개선되어 Valheim 기본 무기뿐만 아니라 다른 모드의 커스텀 무기도 정확히 감지할 수 있습니다.

### 1. 단검 (Dagger/Knife) 감지 - `IsUsingDagger()`

**우선순위:**
1. **Valheim 기본 스킬 타입**: `Skills.SkillType.Knives`
2. **프리팹 이름**: `Dagger`, `dagger`, `Knife`, `knife`, 'knive', 'Knive" 포함
3. **무기 이름**: `단검`, `dagger`, `knife`, 'knive' 포함

**지원하는 모드 무기 예시:**
- `ModDagger_Iron` ✅
- `EpicLoot_ElvenKnife` ✅  
- `CustomWeapons_AssassinDagger` ✅

### 2. 검 (Sword/Blade) 감지 - `IsUsingSword()`

**우선순위:**
1. **Valheim 기본 스킬 타입**: `Skills.SkillType.Swords`
2. **프리팹 이름**: `Sword`, `sword`, `Blade`, `blade` 포함
3. **무기 이름**: `검`, `sword`, `blade` 포함

**지원하는 모드 무기 예시:**
- `EpicLoot_FlameBlade` ✅
- `WeaponPack_KatanaSword` ✅
- `MagicWeapons_CrystalBlade` ✅

### 3. 둔기 (Mace/Club/Hammer) 감지 - `IsUsingMace()`

**우선순위:**
1. **Valheim 기본 스킬 타입**: `Skills.SkillType.Clubs`
2. **프리팹 이름**: `Mace`, `mace`, `Club`, `club`, `Hammer`, `hammer` 포함
3. **무기 이름**: `둔기`, `mace`, `club`, `hammer` 포함

**지원하는 모드 무기 예시:**
- `EpicLoot_IronMace` ✅
- `DwarvenPack_WarHammer` ✅
- `BattleClub_Reinforced` ✅

### 4. 활 (Bow) 감지 - `IsUsingBow()`

**우선순위:**
1. **Valheim 기본 스킬 타입**: `Skills.SkillType.Bows`
2. **프리팹 이름**: `Bow`, `bow`, `Longbow`, `longbow` 포함
3. **무기 이름**: `활`, `bow`, `longbow` 포함

**지원하는 모드 무기 예시:**
- `ElvenWeapons_LongBow` ✅
- `MagicArchery_EnchantedBow` ✅
- `HunterPack_CompositeBow` ✅

### 5. 석궁 (Crossbow) 감지 - `IsUsingCrossbow()`

**우선순위:**
1. **프리팹 이름**: `Crossbow`, `crossbow` 포함 (최우선)
2. **무기 이름**: `석궁`, `crossbow` 포함
3. **특수 케이스**: Bows 스킬 타입 + crossbow 관련 이름

**지원하는 모드 무기 예시:**
- `MedievalWeapons_HeavyCrossbow` ✅
- `Siege_RepeatingCrossbow` ✅
- `CustomRanged_AutoCrossbow` ✅

### 6. 지팡이 (Staff/Wand) 감지 - `IsUsingStaff()`

**우선순위:**
1. **Valheim 기본 스킬 타입**: `Skills.SkillType.ElementalMagic`
2. **프리팹 이름**: `Staff`, `staff`, `Wand`, `wand`, `Rod`, `rod` 포함
3. **무기 이름**: `지팡이`, `staff`, `wand`, `rod` 포함

**지원하는 모드 무기 예시:**
- `MagicOverhaul_IceStaff` ✅
- `WizardPack_FireWand` ✅
- `ElementalWeapons_LightningRod` ✅

### 7. 창 (Spear/Lance) 감지 - `IsUsingSpear()`

**우선순위:**
1. **Valheim 기본 스킬 타입**: `Skills.SkillType.Spears`
2. **프리팹 이름**: `Spear`, `spear`, `Lance`, `lance`, `Pike`, `pike` 포함
3. **무기 이름**: `창`, `spear`, `lance`, `pike` 포함

**지원하는 모드 무기 예시:**
- `KnightWeapons_CavalryLance` ✅
- `MedievalPack_WarPike` ✅
- `TribalWeapons_HuntingSpear` ✅

## 디버깅 시스템

모든 무기 감지 함수에는 상세한 로깅 시스템이 포함되어 있습니다:

### 로그 레벨
- **Debug**: Valheim 기본 무기 감지 (일반적인 경우)
- **Info**: 다른 모드 무기 감지 성공 (중요한 정보)
- **Debug**: 무기가 해당 타입이 아닌 경우

### 로그 예시
```
[단검 감지] 프리팹 이름으로 단검 감지: ModDagger_Obsidian (Epic Obsidian Dagger)
[검 감지] 무기 이름으로 검 감지: Flaming Blade (프리팹: CustomSword_Fire)
[둔기 감지] Valheim 기본 Clubs 스킬 타입: Bronze mace
```

## 호환성 보장

### 하위 호환성
- 기존 Valheim 무기는 100% 호환
- 기존 스킬 시스템 동작 변경 없음
- 성능 영향 최소화

### 확장성
- 새로운 모드 무기 자동 감지
- 다국어 무기 이름 지원
- 커스텀 프리팹 명명 규칙 대응

### 성능 최적화
- 1순위 검사로 대부분 케이스 빠른 처리
- 불필요한 문자열 검사 최소화
- 캐싱을 통한 반복 검사 최적화

## 모드 개발자를 위한 권장사항

### 무기 프리팹 명명 규칙
```
{ModName}_{WeaponType}_{Material/Tier}
예: EpicLoot_Dagger_Mythril
   WeaponPack_Sword_Legendary
   MagicStaves_Staff_Ice
```

### 무기 표시 이름 권장사항
- 영어: 표준 무기 타입 키워드 포함 (Dagger, Sword, Bow 등)
- 한국어: 한국어 무기 타입 포함 (단검, 검, 활 등)
- 다국어: 해당 언어의 무기 타입 키워드 포함

## 테스트 케이스

각 무기 타입별로 다음 케이스들이 정상 감지되는지 확인:

1. **Valheim 기본 무기** - 스킬 타입으로 감지
2. **영어 키워드 모드 무기** - 프리팹/이름으로 감지  
3. **한국어 키워드 무기** - 이름으로 감지
4. **대소문자 혼합** - 대소문자 구분 없이 감지
5. **특수 모드 무기** - 복합 키워드로 감지

이 시스템을 통해 CaptainSkillTree 모드는 다른 무기 모드들과 완벽하게 호환되며, 사용자가 어떤 무기 모드를 사용하더라도 스킬 트리 효과가 정상적으로 적용됩니다.
# WackyEpicMMOSystem 경험치 아이템 분석

> 소스 경로: `C:\home\ssunyme\.npm-global\bin\WackyEpicMMOSystem` (로컬 소스 사본)
> 분석 목적: 퀘스트 보상에 마법 오브 / XP 포션을 실물 지급하기 위해 정확한 프리팹명·수치·매핑을 확정.

## 1. 마법 오브 (Magic Orb) — 8단계

| 프리팹명 | 로컬라이제이션 키 | 기본 지급 경험치 (config 조정 가능) |
|---|---|---|
| `mmo_orb1` | `mmo_xp_orb1` | 300 |
| `mmo_orb2` | `mmo_xp_orb2` | 600 |
| `mmo_orb3` | `mmo_xp_orb3` | 1,000 |
| `mmo_orb4` | `mmo_xp_orb4` | 2,000 |
| `mmo_orb5` | `mmo_xp_orb5` | 4,000 |
| `mmo_orb6` | `mmo_xp_orb6` | 8,000 |
| `mmo_orb7` | `mmo_xp_orb7` | 16,000 |
| `mmo_orb8` | `mmo_xp_orb8` | 32,000 |

- 등록 위치: `LevelSystem/DataMonsters.cs` `InitItems()` — `new Item("mmo_xp", "mmo_orb1", "asset")` 형태로 8개 전부 등록.
- 경험치 수치는 `Plugin.cs`의 `XPforOrb1`~`XPforOrb8` ConfigEntry(섹션 "6.Orbs and Potions")에서 조정 가능.
- 드롭: 일반 몬스터 1%(`OrbDropChance`), 보스 100%(`OrbDropChancefromBoss`) 확률로 1~3개(`OrdDropMaxAmountFromBoss`) 드롭.
- 소비 시 EpicMMOSystem 자체 로직이 즉시 경험치를 지급한다 (우리 쪽은 아이템만 지급하면 됨).
- 바이옴 매핑(Thunderstore 설명 기준, 8단계 수와 일치): Meadows=1, BlackForest=2, Swamp/Ocean=3, Mountain=4, Plains=5, Mistlands=6, Ashlands=7, Deep North=8.
  - CaptainSkillTree 퀘스트 시스템은 Deep North 바이옴 퀘스트가 아직 없어 `mmo_orb8`은 현재 미사용.

## 2. XP 포션 (Drinkable) — 3단계

| 프리팹명 | 효과 (기본값, config 조정 가능) | 지속시간 |
|---|---|---|
| `mmo_xp_drink1` (Minor) | 획득 경험치 +30% (`XPforMinorPotion` = 1.3) | 10분 (`PotionSEtime`) |
| `mmo_xp_drink2` (Medium) | 획득 경험치 +60% (`XPforMediumPotion` = 1.6) | 10분 |
| `mmo_xp_drink3` (Greater) | 획득 경험치 +100% (`XPforGreaterPotion` = 2.0) | 10분 |

- 등록 위치: `Plugin.cs` `itemassets()` — `new Item("mmo_xp", "mmo_xp_drink1", "asset")` 등.
- **주의**: 브루잉 재료(밀주)인 `mmo_mead_minor`/`mmo_mead_med`/`mmo_mead_greater`와는 다른 아이템이다. 이건 마법 발효조(Magic Fermenator)에서 `mmo_mead_*` + Mob Chunk를 발효시켜 만드는 **최종 음용 아이템**이며, 우리가 지급해야 하는 건 이 `mmo_xp_drink*` 쪽이다.
- 실제 발동 효과(획득 경험치 배율 증가)는 상태이상(StatusEffect) `Potion_MMO_Minor` / `Potion_MMO_Medium` / `Potion_MMO_Greater`가 담당하며, 소비 즉시 EpicMMOSystem이 알아서 부여한다.

## 3. CaptainSkillTree 퀘스트 보상 매핑 (적용 완료)

`SkillTree/Quest/QuestManager.cs`의 `BiomeOrbPrefabs` / `BiomePotionPrefabs`:

| 바이옴 | 오브 | 포션 등급 |
|---|---|---|
| Meadows | mmo_orb1 | Minor (mmo_xp_drink1) |
| BlackForest | mmo_orb2 | Minor |
| Swamp | mmo_orb3 | Minor |
| Mountain | mmo_orb4 | Medium (mmo_xp_drink2) |
| Plains | mmo_orb5 | Medium |
| Mistlands | mmo_orb6 | Greater (mmo_xp_drink3) |
| Ashlands | mmo_orb7 | Greater |
| (DeepNorth, 미구현) | mmo_orb8 | Greater |

포션은 등급이 3단계뿐이라 7개 바이옴을 난이도 순으로 3/2/2로 묶었다.

수량은 바이옴 내 퀘스트 표시 순서(`QuestDefinition.DisplayOrder`)에 따라 1~3개로 선형 배분한다(맨 위 퀘스트=1개, 보스=3개) — `QuestManager.GetQuestRewardItemCount()` 참고.

## 4. 이전에 시도했다가 제거한 접근

한때 "다음 레벨업까지 필요한 경험치 × 바이옴/레벨 기반 %"를 직접 계산해 `CaptainMMOBridge.AddExp()`로 지급하는 방식을 구현했으나:
- `CaptainMMOBridge.GetExpToNextLevel()`이 EpicMMO 연동 시 리플렉션(`EpicMMOReflectionHelper.GetNeedExp()`)에 의존했는데 값이 0으로 반환되는 문제가 있었고,
- 이번에 EpicMMOSystem 자체에 이미 검증된 경험치 아이템(오브/포션)이 있다는 게 확인되어, 직접 계산 대신 **아이템 실물 지급**으로 전환했다. 훨씬 단순하고 EpicMMOSystem 자체 로직에 의존하므로 안정적이다.

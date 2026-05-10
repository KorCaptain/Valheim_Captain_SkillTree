# SpawnThat / Drop That 서버 설정 규칙

설정 파일 위치: `C:\Users\ssuny\AppData\Roaming\r2modmanPlus-local\Valheim\profiles\Cusor_1\BepInEx\config\`

---

## SpawnThat — 파일별 역할

| 파일 | 용도 |
|------|------|
| `spawn_that.cfg` | 전체 옵션 제어판 (디버그, 로깅 on/off) |
| `spawn_that.world_spawners_advanced.cfg` | 월드 전역 스포너 추가/수정 |
| `spawn_that.spawnarea_spawners.cfg` | 특정 Location의 SpawnArea 스포너 수정 |
| `spawn_that.local_spawners_advanced.cfg` | 던전/Location 내부 LocalSpawner 수정 |
| `spawn_that.simple.cfg` | 기존 월드 스포너 배수 조정 (간단 수정용) |

---

## SpawnThat — 성/요새 내부 스폰 방법

### 지상형 몬스터 → SpawnArea 방식 (spawnarea_spawners.cfg)

```ini
[Spawner_CharredCross]
IdentifyByName=Spawner_CharredCross

[Spawner_CharredCross.3]          # 기존 .0 .1 .2 이후 인덱스 사용
Enabled=True
TemplateEnabled=True
PrefabName=Charred_Melee_Dyrnwyn
SpawnWeight=30
LevelMin=1
LevelMax=3
```

- `CharredFortress` 내부 기존 스포너: `Spawner_CharredCross` (인덱스 0~2 사용 중)
- SpawnArea 방식은 성 구조물 내부 고정 스폰 포인트에 직접 연결됨

### 공중형 몬스터 → WorldSpawner + ConditionLocation (world_spawners_advanced.cfg)

```ini
[WorldSpawner.100]
Name=CharredFortress_FallenValkyrie
Enabled=True
Biomes=AshLands
PrefabName=FallenValkyrie
ConditionLocation=CharredFortress   # Location 이름으로 구역 제한
GroundOffset=20                     # 공중형은 오프셋 필요
ConditionAltitudeMin=15
MaxSpawned=2
SpawnInterval=800
SpawnChance=75
...
SetFaction=Demon
```

- 인덱스는 기존 마지막 번호 이후 사용 (현재 SA_ 시리즈가 85~99 사용 중 → 100부터)
- `ConditionLocation` 파라미터는 디버그 파일에 기본값으로 안 나타나지만 유효함

---

## SpawnThat — Ashlands 주요 Location 이름

| Location | 설명 |
|----------|------|
| `CharredFortress` | 잿빛 성/요새 |
| `CharredStone_Spawner` | 잿빛 돌 스포너 구역 |
| `CharredTowerRuins3` | 잿빛 탑 유적 |
| `PlaceofMystery1~3` | 신비의 장소 |

전체 Location 목록 확인: `spawn_that.cfg`에서 `WriteLocationsToFile = true` 후 게임 재시작

---

## Drop That — 서플리먼트 파일 규칙

### 파일 이름 규칙
```
drop_that.character_drop.{임의이름}.cfg
```
`drop_that.cfg`의 `LoadSupplementalDropTables = true`로 자동 로드됨

### 섹션 헤더 형식 ⚠️

```ini
# ✅ 올바른 형식
[CreatureName.0]
PrefabName = ItemName

# ❌ 틀린 형식 (에러 발생)
[CharacterDrop.CreatureName.0]
PrefabName = ItemName
```

### 파라미터

```ini
[CreatureName.0]
PrefabName = ItemPrefabName    # 아이템 프리팹 이름
SetAmountMin = 1               # 최소 드랍 수량
SetAmountMax = 3               # 최대 드랍 수량
SetChanceToDrop = 100          # 드랍 확률 (0~100 정수) ⚠️ 0.0~1.0 아님
```

### ChanceToDrop 스케일 ⚠️ 중요

| 원하는 확률 | SetChanceToDrop 값 |
|------------|-------------------|
| 100% | 100 |
| 50% | 50 |
| 25% | 25 |
| 10% | 10 |

`1.0`으로 쓰면 **1%**로 적용됨 (0.0~1.0 스케일이 아님)

### 여러 아이템 드랍 — 인덱스로 구분

```ini
[Morgen.0]
PrefabName = IronOre
SetAmountMin = 4
SetAmountMax = 4
SetChanceToDrop = 25

[Morgen.1]
PrefabName = FlametalOre
SetAmountMin = 4
SetAmountMax = 4
SetChanceToDrop = 25

[Morgen.2]
PrefabName = SurtlingCore
SetAmountMin = 3
SetAmountMax = 3
SetChanceToDrop = 50
```

- 각 인덱스는 **독립적으로 판정**됨 → "둘 중 하나만" 드랍은 직접 지원 안 됨
- 기존 바닐라 드랍은 `ClearAllExistingWhenModified = false`(기본값)면 유지됨

---

## Drop That — 디버그 방법

`drop_that.cfg`에서 활성화:
```ini
WriteCharacterDropsToFile = true         # 기본 드랍 테이블 출력
WriteLoadedConfigsToFile = true          # 로드된 설정 출력
WriteDropTablesAfterChangesToFile = true # 변경 후 최종 드랍 테이블 출력
```

게임 재시작 후 `BepInEx\Debug\` 폴더 확인:
- `drop_that.character_drop.loaded.cfg` — 우리 설정 로드 여부 확인
- `drop_that.character_drop.after_changes.cfg` — 최종 적용 결과 확인

---

## 현재 적용된 드랍 설정 (drop_that.character_drop.ashlands_bell.cfg)

| 몬스터 | 드랍 아이템 | 수량 | 확률 |
|--------|------------|------|------|
| Charred_Melee_Dyrnwyn | BellFragment | 1 | 25% |
| FallenValkyrie | BellFragment | 1 | 25% |
| Gjall | Softtissue | 3~5 | 100% |
| DvergerMageFire | Softtissue | 1 | 100% |
| DvergerMageIce | Softtissue | 1 | 100% |
| DvergerMageSupport | Softtissue | 1 | 100% |
| Bjorn | TinOre | 5~8 | 100% |
| BlobLava | SurtlingCore | 1~3 | 100% |
| Morgen | IronOre | 4 | 25% |
| Morgen | FlametalOre | 4 | 25% |
| Morgen | SurtlingCore | 3 | 50% |
| Morgen_NonSleeping | IronOre | 4 | 25% |
| Morgen_NonSleeping | FlametalOre | 4 | 25% |
| Morgen_NonSleeping | SurtlingCore | 3 | 50% |

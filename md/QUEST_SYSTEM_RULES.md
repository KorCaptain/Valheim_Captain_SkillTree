# 퀘스트 시스템 규칙

## 개요

바이옴별 자원 채집 / 몬스터 처치 / 보스 처치 퀘스트 + 코인·아이템·특수 보상 시스템.
Config로 전체 목록·수량·보상 조정 가능, 서버 동기화 지원, 7개 언어 지원.

- **진입점**: 스킬트리 UI 내 좌측 상단 Quest 버튼 / 탭 인벤토리 아이콘 / Ctrl+J 단축키(Quest_Config.QuestToggleKey, F1 Config Manager "Quest System"에서 리바인드 가능)
- **닫기**: ESC 또는 Tab

---

## 파일 구조

| 파일 | 역할 |
|------|------|
| `SkillTree/Quest/Quest_Config.cs` | Config 스키마. `QuestSeed[]` 데이터 배열로 바이옴×퀘스트를 정의하고 루프로 BindServerSync 등록(하드코딩 나열 대신 데이터 기반) |
| `SkillTree/Quest/Quest_StringSync.cs` | 문자열 필드(Type/TargetPrefab/보상 아이템명 등) 전용 서버 동기화 채널 (아래 "서버 동기화" 참고) |
| `SkillTree/Quest/QuestDefinition.cs` | Config에서 읽어온 퀘스트 1개의 런타임 스냅샷 (읽기 전용 POCO) |
| `SkillTree/Quest/QuestManager.cs` | 진행도 추적(`Player.m_customData`에 저장) · 완료 판정 · 보상 클레임 · 실시간 UI 갱신 · 채집 효과음 |
| `SkillTree/Quest/QuestRewardSpawner.cs` | 보상 실물 지급 — 코인(인벤토리 우선, 꽉 찼을 때만 드롭) / 아이템(월드 드롭) / 특수 보상(테이밍 소환, 스탯 보너스) |
| `SkillTree/Quest/QuestKillPatch.cs` | 몬스터/보스 처치 후킹 (`Character.OnDeath`) |
| `SkillTree/Quest/QuestGatherPatch.cs` | 채집 후킹 (`Inventory.AddItem(ItemDrop.ItemData)`) — Wood는 제외(아래 참고) |
| `SkillTree/Quest/QuestGatherOriginTracker.cs` | "직접 채집했는가" 판별. 채광/Pickable은 플래그+상관관계 방식, 나무는 벌목 시점 직접 지급 방식 |
| `Gui/QuestPanelUI.cs` | 퀘스트 목록 UI 패널 (SkillTreeBG 배경 재사용, 스크롤, 바이옴 헤더/행) |
| `Gui/SkillTreeUI.Quest.cs` | 스킬트리 UI 내부 Quest 버튼 (partial class로 SkillTreeUI.cs 확장) |
| `MMO_System/QuestIconPatch.cs` | 탭 인벤토리 화면 Quest 아이콘 (씬 전체 탐색 방식) |
| `Localization/DefaultLanguages_Quest.cs`, `_EN.cs` | ko/en UI 문구 + 아이템/몬스터 이름 |
| `Localization/{de,ja,pt_BR,ru,zh-cn}.json` | 나머지 5개 언어 |

---

## 핵심 메커니즘

### Config 스키마 (데이터 기반, 하드코딩 나열 금지)

`Quest_Config.cs`의 `Seeds` 배열에 `QuestSeed` 구조체로 바이옴별 퀘스트를 정의하면, `Initialize()`가 루프를 돌며 필드별 `BindServerSync` ConfigEntry를 자동 생성한다. 250개 가까운 수치 Config를 전부 손으로 나열하는 기존 `SkillTreeConfig.Broadcast.cs` 패턴을 그대로 따라가지 않고, 이 부분만 데이터 기반으로 예외적으로 처리했다(31개 퀘스트 × 8개 필드를 손으로 나열하면 파일이 감당 불가능하게 커짐).

새 퀘스트 추가 = `Seeds` 배열에 한 줄 추가:
```csharp
new QuestSeed{ Biome="Meadows", Id="Quest6", Type="Gather", TargetPrefab="Flint", Amount=50, Item1="Coins", Item1Amt=0, CoinMin=30, CoinMax=50 },
```
`Type`은 `"Gather"` / `"Kill"` / `"KillBoss"`. `Special`은 `"TameLoxCalf"` 또는 `"JumpProficiency:10"` 형태(파싱은 `QuestRewardSpawner.GrantSpecialReward` 참고, 새 특수 보상 추가 시 거기 분기 추가).

### 완료 → 클레임 2단계

목표 달성 시 `Completed=true`만 세팅(자동 지급 X, confetti VFX만). 퀘스트 창에서 `[보상]` 버튼을 눌러야 `ClaimReward()`가 실제 보상을 지급하고 `Claimed=true`로 바뀐다. 상태는 `Player.m_customData`에 `CaptainSkillTree_Quest_{Key}_{Count|Completed|Claimed}` 키로 영속화.

### 서버 동기화 (2채널)

- **수치 필드**(Enabled/Amount/CoinMin/CoinMax/ItemRewardAmount): 기존 `SkillTreeConfig.Broadcast.cs`의 `Dictionary<string,float>` 채널에 편승(`BroadcastConfigToClients()` 안에서 `Quest_Config.Quests`를 루프 돌며 등록). 읽을 땐 `SkillTreeConfig.GetEffectiveValue(key, localValue)`.
- **문자열 필드**(Type/TargetPrefab/ItemReward1-2/SpecialReward): 기존 채널이 float 전용이라 담을 수 없어서, **완전히 별도의 RPC 채널**(`Quest_StringSync.cs`)을 새로 만들었다. 기존 250여 개 Config 동기화에 영향 없이 독립 동작. `SkillTreeConfig.Broadcast.cs`의 `BroadcastConfigToClients()` 끝에서 `Quest_StringSync.BroadcastToClients()`를 같이 호출해, 브로드캐스트가 일어나는 모든 지점(신규 접속/어드민 변경/배치싱크)에서 자동으로 같이 전송되게 연결했다.
- 읽을 땐 `Quest_StringSync.GetEffectiveString(key, localValue)`.
- 새 수치/문자열 Config를 서버 동기화 대상에 추가하려면 `md/SERVER_SYNC_RULES.md` 5번 항목도 참고.

### 코인 보상 — 인벤토리 우선 지급

코인은 캐릭터 앞에 드롭하지 않고 `Inventory.CanAddItem()`으로 전체 수량이 들어갈 자리가 있는지 먼저 확인한 뒤 `Inventory.AddItem(prefab, chunk)`을 최대 스택 크기 단위로 반복 호출해 인벤토리에 바로 지급한다(서버 렉 유발 방지, 월드 오브젝트 생성 최소화). 자리가 없을 때만 기존 방식으로 캐릭터 앞에 드롭. `Inventory.AddItem(GameObject, int)`은 `amount`가 `maxStackSize`를 넘으면 그냥 잘라버리므로(내부적으로 자동 분할 안 함) 직접 청크 단위로 반복 호출해야 한다.

---

## 발견된 버그와 교훈

### 버그 1: Inventory.AddItem 오버로드 착각 — 채집 퀘스트 전체가 작동 안 할 뻔함

**증상**: 채집 퀘스트가 전혀 진행되지 않음(구현 직후 자체 검토 단계에서 발견, 배포 전).

**원인**: `Inventory.AddItem(GameObject, int)`를 후킹했는데, 이건 **낚시 획득에만 쓰이는 경로**였음. 실제 벌목/채광/Pickable 채집은 전부 `Humanoid.Pickup()` → `Inventory.AddItem(ItemDrop.ItemData)` 한 지점으로 모인다(`Inventory.cs` 소스 직접 확인). `AddItem(GameObject,int)`는 내부적으로 `AddItem(ItemData)`에 위임하도록 되어 있어 낚시 등 다른 경로는 커버하지만, 실제 세계 드롭 픽업은 절대 이 오버로드를 거치지 않는다.

**수정**: `Inventory.AddItem(ItemDrop.ItemData)` 단일 오버로드로 재후킹. 이 메서드 내부에서 `item.m_stack`이 처리 도중 변형되므로 Prefix로 원래 수량을 캡처(`__state`)해서 사용.

**교훈**: Valheim API에서 이름이 비슷한 오버로드가 여러 개 있을 때, 실제 게임 코드가 어느 걸 호출하는지(`Humanoid.Pickup()` 등 실제 호출부) 반드시 소스로 확인할 것. 오버로드 이름만 보고 추측 금지.

---

### 버그 2: MineRock/MineRock5/Destructible의 `m_health` 필드는 상수다

**증상**: 채광 퀘스트(구리/주석/철/은/화염강 등)가 직접 캐도 전혀 진행되지 않음.

**원인**: `__instance.m_health`를 "현재 남은 체력"으로 착각해서 `if (__instance.m_health > 0) return;`으로 파괴 여부를 판별했는데, 이 필드는 채광 1회당 기본 데미지 상수(또는 부위별 기본 체력 상수)일 뿐 실제 남은 체력이 아니라 **항상 0보다 큼** → 파괴 조건이 절대 만족되지 않음.

**수정**: 실제 남은 체력은 `ZDO`에서 직접 읽어야 함.
- `MineRock`: `nview.GetZDO().GetFloat("Health" + hitAreaIndex, ...)` (부위별 키)
- `MineRock5`: `__instance.GetHitArea(hitAreaIndex).m_health`
- `Destructible`: `nview.GetZDO().GetFloat(ZDOVars.s_health, ...)` (TreeBase와 동일 패턴)

**교훈**: `m_health`류 필드가 진짜 "현재 체력"인지, 아니면 "기본값 상수"인지 타입/이름만 보고 판단하지 말고 실제 대입되는 곳을 추적할 것. 같은 이름이라도 클래스마다 의미가 다를 수 있다.

---

### 버그 3: DropTable.m_drops\[0\]이 항상 주 자원이라는 보장 없음

**증상**: 나무를 벌목해도 채집 퀘스트가 거의 진행 안 됨(수십 그루를 베어도 "+1"이 한 번 찍히는 수준).

**원인**: `SkillTree/ProductionEffects.cs`의 보너스 나무 지급 로직을 참고해 "벌목 시점에 드롭테이블을 직접 읽어 즉시 지급"하는 방식으로 설계했는데, 이때 `dropTable.m_drops[0]`(첫 번째 항목)만 읽었다. 드롭테이블 항목 순서가 "주 자원이 항상 0번"이라는 보장이 없어서, 낮은 확률/수량의 다른 항목을 잘못 집는 경우가 있었음.

**수정**: `m_drops` 전체를 순회하며, 활성 Gather 퀘스트 대상과 프리팹명이 일치하는 항목을 찾아 그 항목의 `(m_stackMin+m_stackMax)/2`를 지급하도록 변경.

**교훈**: 참고 코드(ProductionEffects.cs)가 `m_drops[0]`를 쓴다고 해서 그게 항상 안전한 가정은 아니다 — 원본 코드는 "아이템 이름 표시용"으로만 썼지 "수량 계산의 근거"로 쓴 게 아니었으므로, 용도가 다르면 같은 패턴을 그대로 재사용하면 안 된다.

---

### 버그 4: 나무 자동습득은 타이밍이 불안정해서 "플래그+상관관계" 방식이 부적합했음

**증상**: 벌목 직후 잠시 후 나무를 자동으로 주워도(2초, 이후 6초로 늘려도) 퀘스트 진행이 안 됨.

**원인**: 원래 설계는 "방금 채집 행위를 했다"는 시간 제한 플래그를 세우고, 나중에 `Inventory.AddItem`이 호출되면 그 플래그가 살아있는지 확인하는 2단계 상관관계 방식이었다. Pickable(E키로 즉시 습득)에는 잘 맞았지만, 나무는 쓰러뜨린 뒤 흩어진 조각을 걸어가서 자동으로 줍기까지의 시간이 들쭉날쭉해서 상관관계가 계속 어긋났다.

**수정**: 나무만 예외적으로 **1단계 직접 지급 방식**으로 전환 — `Inventory.AddItem`을 기다리지 않고, 벌목/통나무 파괴 이벤트(`TreeBase.RPC_Damage`, `TreeLog.Destroy`) 발생 시점에 드롭테이블을 읽어 그 자리에서 바로 진행도 지급. `QuestGatherPatch.cs`에서는 `prefabName == "Wood"`일 때 명시적으로 스킵해서 중복 지급 방지.

**교훈**: "행위 시점"과 "결과가 인벤토리에 들어오는 시점"이 시간적으로 크게 벌어질 수 있는 케이스는 상관관계(플래그+나중에 확인) 방식보다, 가능하면 **행위 시점에 직접 결과를 확정**하는 방식이 훨씬 견고하다.

---

### 버그 5: 화면에 보이는 스킬트리 아이콘 ≠ 내가 가정한 EpicMMO 경로

**증상**: 탭 인벤토리 화면에 Quest 아이콘이 계속 안 뜸. 스킬트리 아이콘은 정상적으로 보임.

**원인**: `md/CORE_PROTECTION_README.md`에 문서화된 `"EpicMMO(Clone)/Canvas/MyUI/navigationPanel/Buttons"` 경로를 가정하고 `GameObject.Find()` + `Transform.Find()` 체이닝으로 찾았는데, 실제 이 서버 설정에서는 `LevelHud(Clone)/Canvas/NavigatePanel/Buttons`였다(BepInEx 로그로 확인). 문서/과거 코드에 적힌 경로가 항상 현재 실제 계층 구조와 일치한다고 가정하면 안 된다.

**수정**: 고정 경로 탐색 대신, `Resources.FindObjectsOfTypeAll<RectTransform>()`으로 씬 전체에서 이름이 `"ButtonSkillTree"`인 오브젝트를 직접 재귀 탐색(단, `gameObject.scene.IsValid()`로 프리팹 에셋 참조는 제외).

**교훈**: 하드코딩된 UI 경로는 버전/설정에 따라 깨지기 쉽다. **경로를 몰라도 이름으로 씬 전체를 탐색**하는 방식이 훨씬 견고하다. 의심스러우면 바로 로그를 남겨서 실측할 것 — 추측으로 몇 번씩 고치는 것보다 로그 한 번 보는 게 빠르다.

---

### 버그 6: 복제된 버튼의 "첫 번째 자식 Image"가 항상 아이콘은 아니다

**증상**: Quest 아이콘을 스킬트리 버튼에서 복제해 생산전문가 아이콘으로 바꿨는데, 계속 원본(검 아이콘) 그대로 보임.

**원인**: 복제된 버튼의 자식 Image 중 **첫 번째로 발견된 것**을 아이콘으로 가정하고 스프라이트를 교체했는데, 그게 실제 아이콘이 아니라 테두리/배경 장식 Image였음.

**수정**: 자식 Image들을 전부 로그로 남기고(`GetComponentsInChildren<Image>`), 그중 **RectTransform 면적이 가장 작은 것**을 아이콘으로 판단해서 교체(아이콘은 보통 버튼 배경/테두리보다 작은 인셋 이미지라는 특성 이용).

**교훈**: 계층 구조 안에 같은 타입의 컴포넌트가 여러 개 있을 때 "첫 번째"를 아이콘으로 가정하지 말 것. 이름/크기/부모-자식 관계 등 추가 단서로 식별하거나, 안 되면 전부 로그로 남겨서 실측 확인.

---

### 버그 7: HorizontalLayoutGroup에서 LayoutElement.preferredWidth가 가용 폭에 거의 맞먹으면 계산이 깨짐

**증상**: 퀘스트 창의 바이옴 헤더 텍스트("목초지", "검은숲" 등)가 세 번의 구조 변경(스트레치 anchor → 래퍼 제거 → HorizontalLayoutGroup 통일)에도 계속 글자 일부만 보임. 반면 같은 구조를 쓰는 퀘스트 행 라벨은 처음부터 정상 표시됨.

**원인**: 헤더 텍스트의 `LayoutElement.preferredWidth`를 `BoxWidth - 60`(≈540, 패널 전체 폭에 거의 맞먹는 값)로 설정했음. 반면 정상 동작하던 행 라벨은 `preferredWidth = 320`으로 여유 있게 낮은 값을 썼다. 요청 폭이 패딩·스크롤바 여백까지 합친 실제 가용 폭에 근접하거나 초과하면 `HorizontalLayoutGroup`의 폭 계산이 깨지는 것으로 추정됨.

**수정**: 헤더 텍스트 `preferredWidth`를 220으로 낮춤(바이옴 이름은 최대 4글자라 이 정도로 충분).

**교훈**: 구조(레이아웃 컴포넌트 조합)가 똑같아도 **수치 값 자체**가 문제가 될 수 있다. "구조를 똑같이 맞췄는데도 안 된다"면 컴포넌트 종류가 아니라 각 필드에 넣은 **숫자 값**을 의심할 것 — 특히 가용 공간에 거의 맞먹거나 초과하는 크기 요청.

---

## 알려진 제약사항 / TODO

- 잿빛땅 보스(Fader) 처치 퀘스트 보상 미지정 — `Quest_Config.cs` Seeds의 `Ashlands Quest4` 항목에 값 채우기
- 퀘스트 진행/완료 상태는 `Player.m_localPlayer` 기준 클라이언트 로컬 판정 — 기존 EXP 시스템(`CaptainMMOPatches.cs`)과 동일한 아키텍처라 헤드리스 데디케이트 서버에서는 동작 안 함(설계상 한계, 신규 결함 아님)
- 파티 플레이 시 몬스터/보스 처치 퀘스트 인정 범위(솔로만 vs 파티 공유) 미검증
- `Lox_Calf` 즉시 테이밍(`Character.SetTamed(true)`)의 `ZNetView` 등록 타이밍 미검증(이론상 `Instantiate` 시 동기 처리되어야 하나 실측 필요)
- 문자열 Config 필드(Type/TargetPrefab 등)는 `IsAdminOnly`로 F1 UI에서는 잠겨있지만, 수치 필드와 달리 클라이언트가 로컬 cfg 파일을 직접 편집하면 여전히 로컬 값이 서버 값으로 덮어써지지 않을 가능성 낮음(서버 동기화 채널 존재) — 다만 관련 테스트는 미실시

---

**작성일**: 2026-07-30
**관련 파일**: `SkillTree/Quest/*`, `Gui/QuestPanelUI.cs`, `Gui/SkillTreeUI.Quest.cs`, `MMO_System/QuestIconPatch.cs`, `Localization/*Quest*`

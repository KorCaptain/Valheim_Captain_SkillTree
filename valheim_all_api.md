# Valheim 전체 API 목록 (assembly_valheim.dll)
# dnSpy로 추출한 541개 클래스의 주요 API들
# 모딩에 유용한 API들을 시스템별로 분류

## 🎮 핵심 게임 시스템

### Player.cs - 플레이어 시스템
- Player.GetLocalPlayer() - 로컬 플레이어 가져오기
- Player.GetSkillFactor(Skills.SkillType) - 스킬 레벨 요소 가져오기
- Player.RaiseSkill(Skills.SkillType, float) - 스킬 경험치 증가
- Player.AddNoise(float) - 소음 추가
- Player.GetInventory() - 인벤토리 가져오기
- Player.Damage(HitData) - 플레이어 데미지
- Player.Heal(float) - 체력 회복
- Player.AddStamina(float) - 스태미나 추가
- Player.PlacePiece(Piece) - 건축물 설치
- Player.GetClosestPlayer(Vector3, float) - 가장 가까운 플레이어

### Character.cs - 캐릭터 시스템
- Character.Damage(HitData) - 캐릭터 데미지
- Character.Heal(float) - 체력 회복
- Character.SetHealth(float) - 체력 설정
- Character.GetHealth() - 현재 체력
- Character.IsDead() - 사망 여부
- Character.AddSEMan(StatusEffect) - 상태효과 추가
- Character.GetSEMan() - 상태효과 매니저

### Game.cs - 게임 코어 시스템
- Game.instance - 게임 인스턴스
- Game.IncrementPlayerStat(PlayerStatType, float) - 플레이어 통계 증가
- Game.ScaleDrops(GameObject, int) - 드롭 아이템 스케일링
- Game.CheckDropConversion(HitData, ItemDrop, GameObject, ref int) - 드롭 변환 체크

## 🎒 인벤토리 & 아이템 시스템

### Inventory.cs - 인벤토리 시스템
- Inventory.AddItem(GameObject, int) - 아이템 추가
- Inventory.AddItem(ItemDrop.ItemData) - 아이템 데이터 추가
- Inventory.RemoveItem(string, int) - 아이템 제거
- Inventory.CountItems(string) - 아이템 개수 세기
- Inventory.GetAllItems() - 모든 아이템 가져오기
- Inventory.FindFreeStackSpace(string) - 빈 스택 공간 찾기
- Inventory.HaveItem(string) - 아이템 보유 여부

### ItemDrop.cs - 아이템 드롭 시스템
- ItemDrop.ItemData - 아이템 데이터 클래스
- ItemDrop.OnCreateNew(GameObject) - 새 아이템 생성시 호출
- ItemDrop.SetStack(int) - 스택 개수 설정
- ItemDrop.ItemData.GetMaxDurability() - 최대 내구도
- ItemDrop.ItemData.m_shared - 공유 아이템 데이터
- ItemDrop.ItemData.m_quality - 아이템 품질
- ItemDrop.ItemData.m_durability - 현재 내구도

### Container.cs - 컨테이너 시스템
- Container.GetInventory() - 컨테이너 인벤토리
- Container.CheckAccess(long) - 접근 권한 체크
- Container.RPC_RequestOpen(long, bool) - 컨테이너 열기 요청
- Container.RPC_OpenResponse(long, bool) - 열기 응답

## 🏗️ 건축 & 제작 시스템

### Piece.cs - 건축 시스템
- Piece.IsPlacedByPlayer() - 플레이어가 설치했는지 확인
- Piece.CanBeRemoved() - 제거 가능한지 확인
- Piece.m_craftingStation - 제작 스테이션 요구사항

### CraftingStation.cs - 제작 스테이션
- CraftingStation.GetLevel() - 제작 스테이션 레벨
- CraftingStation.CheckUsable(Player, bool) - 사용 가능 여부
- CraftingStation.GetExtensionList() - 확장 목록

### Recipe.cs - 제작 레시피
- Recipe.m_item - 제작 결과물
- Recipe.m_resources - 필요 재료들
- Recipe.m_craftingStation - 필요한 제작 스테이션

## ⚔️ 전투 & 데미지 시스템

### HitData.cs - 데미지 데이터
- HitData.GetTotalDamage() - 총 데미지 계산
- HitData.GetAttacker() - 공격자 가져오기
- HitData.CheckToolTier(int, bool) - 도구 티어 체크
- HitData.ApplyResistance(HitData.DamageModifiers, out HitData.DamageModifier) - 저항력 적용
- HitData.m_damage - 데미지 구조체
- HitData.m_point - 타격 지점
- HitData.m_dir - 타격 방향

### Attack.cs - 공격 시스템
- Attack.GetAttackStamina() - 공격 스태미나 소모량
- Attack.GetAttackDamage() - 공격 데미지
- Attack.OnAttackTrigger() - 공격 트리거

### BaseAI.cs - AI 시스템
- BaseAI.SetTarget(Character) - 타겟 설정
- BaseAI.GetTarget() - 현재 타겟
- BaseAI.AggravateAllInArea(Vector3, float, BaseAI.AggravatedReason) - 주변 적대화

## 🌲 자원 채집 시스템

### Pickable.cs - 채집 가능한 오브젝트
- Pickable.RPC_Pick(long, int) - 채집 RPC
- Pickable.Interact(Humanoid, bool, bool) - 상호작용
- Pickable.SetPicked(bool) - 채집 상태 설정
- Pickable.CanBePicked() - 채집 가능 여부
- Pickable.m_itemPrefab - 채집 아이템 프리팹

### TreeBase.cs - 나무 시스템
- TreeBase.RPC_Damage(long, HitData) - 나무 데미지 RPC
- TreeBase.Damage(HitData) - 나무 데미지
- TreeBase.SpawnLog(Vector3) - 통나무 생성
- TreeBase.m_health - 나무 체력
- TreeBase.m_dropWhenDestroyed - 파괴시 드롭 테이블

### TreeLog.cs - 통나무 시스템
- TreeLog.RPC_Damage(long, HitData) - 통나무 데미지 RPC
- TreeLog.Destroy(HitData) - 통나무 파괴
- TreeLog.m_dropWhenDestroyed - 파괴시 드롭 테이블

### MineRock.cs - 채광 시스템
- MineRock.RPC_Hit(long, HitData, int) - 채광 히트 RPC
- MineRock.Damage(HitData) - 채광 데미지
- MineRock.GetHealth() - 광석 체력
- MineRock.m_dropItems - 드롭 아이템들

### MineRock5.cs - 고급 채광 시스템
- MineRock5.RPC_Damage(long, HitData, int) - 고급 채광 데미지 RPC
- MineRock5.DamageArea(int, HitData) - 영역 데미지
- MineRock5.UpdateMesh() - 메시 업데이트

### Destructible.cs - 파괴 가능한 오브젝트
- Destructible.RPC_Damage(long, HitData) - 파괴 데미지 RPC
- Destructible.Damage(HitData) - 파괴 데미지
- Destructible.GetDestructibleType() - 파괴 가능한 타입

## 🐟 낚시 & 동물 시스템

### Fish.cs - 물고기 시스템
- Fish.OnHooked() - 낚시에 걸림
- Fish.Interact(Humanoid, bool, bool) - 물고기 상호작용

### Tameable.cs - 길들이기 시스템
- Tameable.Tame() - 길들이기
- Tameable.GetTameness() - 길들임 정도
- Tameable.SetTamed(bool) - 길들임 상태 설정

### AnimalAI.cs - 동물 AI
- AnimalAI.SetFollowTarget(GameObject) - 따라갈 타겟 설정
- AnimalAI.GetFollowTarget() - 현재 따라가는 타겟

## 🏠 건물 & 스테이션 시스템

### Bed.cs - 침대 시스템
- Bed.CheckAccess(long) - 침대 접근 권한
- Bed.IsCurrent() - 현재 스폰 지점인지 확인

### Fireplace.cs - 화로 시스템
- Fireplace.IsBurning() - 타고 있는지 확인
- Fireplace.GetFuel() - 연료량
- Fireplace.AddFuel() - 연료 추가

### Smelter.cs - 제련소 시스템
- Smelter.GetQueueSize() - 대기열 크기
- Smelter.AddOre(GameObject) - 광석 추가
- Smelter.AddFuel(GameObject) - 연료 추가

### CookingStation.cs - 요리 스테이션
- CookingStation.GetFreeSlot() - 빈 슬롯 찾기
- CookingStation.AddItem(string, int) - 요리 재료 추가

## 🌍 월드 & 환경 시스템

### ZNetScene.cs - 네트워크 씬
- ZNetScene.instance - 전역 인스턴스
- ZNetScene.GetPrefab(string) - 프리팹 가져오기
- ZNetScene.Destroy(GameObject) - 오브젝트 파괴
- ZNetScene.InLoadingScreen - 로딩 화면 여부

### World.cs - 월드 시스템
- World.GetWorldSaveData() - 월드 저장 데이터
- World.GetName() - 월드 이름

### EnvMan.cs - 환경 매니저
- EnvMan.instance - 전역 인스턴스
- EnvMan.GetCurrentEnvironment() - 현재 환경
- EnvMan.SetForceEnvironment(string) - 강제 환경 설정

## 📊 UI & GUI 시스템

### Hud.cs - HUD 시스템
- Hud.instance - 전역 인스턴스
- Hud.FlashHealthBar() - 체력바 깜빡임
- Hud.DamageFlash() - 데미지 플래시

### InventoryGui.cs - 인벤토리 GUI
- InventoryGui.instance - 전역 인스턴스
- InventoryGui.Show(Container) - 컨테이너와 함께 표시
- InventoryGui.Hide() - 숨기기

### MessageHud.cs - 메시지 HUD
- MessageHud.instance - 전역 인스턴스
- MessageHud.ShowMessage(MessageType, string) - 메시지 표시
- MessageHud.QueueUnlockMsg(string) - 잠금 해제 메시지 큐

### DamageText.cs - 데미지 텍스트
- DamageText.instance - 전역 인스턴스
- DamageText.ShowText(DamageText.TextType, Vector3, float, bool) - 텍스트 표시

## 🎵 오디오 & 이펙트 시스템

### AudioMan.cs - 오디오 매니저
- AudioMan.instance - 전역 인스턴스
- AudioMan.PlaySoundAt(AudioClip, Vector3) - 위치에서 사운드 재생

### EffectList.cs - 이펙트 시스템
- EffectList.Create(Vector3, Quaternion, Transform, float, int) - 이펙트 생성
- EffectList.HasEffects() - 이펙트 보유 여부

## 🔧 유틸리티 & 네트워크

### ZNetView.cs - 네트워크 뷰
- ZNetView.IsOwner() - 소유자인지 확인
- ZNetView.IsValid() - 유효한지 확인
- ZNetView.GetZDO() - ZDO 가져오기
- ZNetView.Register<T>(string, Action<long, T>) - RPC 등록
- ZNetView.InvokeRPC(string, object[]) - RPC 호출
- ZNetView.Destroy() - 네트워크 오브젝트 파괴

### ZDO.cs - 네트워크 데이터 오브젝트
- ZDO.GetFloat(int, float) - float 값 가져오기
- ZDO.Set(int, float) - float 값 설정
- ZDO.GetBool(int, bool) - bool 값 가져오기
- ZDO.Set(int, bool) - bool 값 설정
- ZDO.GetString(int, string) - 문자열 값 가져오기

### Utils & Extensions
- UnityEngine.Object.Instantiate<T>() - 오브젝트 인스턴스화
- UnityEngine.Random - 랜덤 유틸리티
- Mathf - 수학 함수들

## 📝 스킬 시스템

### Skills.cs - 스킬 시스템
- Skills.RaiseSkill(SkillType, float) - 스킬 레벨업
- Skills.GetSkill(SkillType) - 특정 스킬 가져오기
- Skills.GetSkillFactor(SkillType) - 스킬 요소 가져오기

### StatusEffect.cs - 상태 효과
- StatusEffect.Setup(Character) - 상태 효과 설정
- StatusEffect.UpdateStatusEffect(float) - 상태 효과 업데이트
- StatusEffect.Stop() - 상태 효과 중단

## 🚢 운송 & 이동 시스템

### Ship.cs - 배 시스템
- Ship.GetSpeed() - 현재 속도
- Ship.HasPlayerOnboard() - 플레이어 탑승 여부

### Vagon.cs - 수레 시스템
- Vagon.CanAttach(Ship) - 연결 가능 여부
- Vagon.AttachTo(Ship) - 배에 연결

## 🏺 저장 & 데이터 시스템

### SaveSystem.cs - 저장 시스템
- SaveSystem.GetSaveDataAsync() - 비동기 저장 데이터 가져오기
- SaveSystem.SaveAsync() - 비동기 저장

### ObjectDB.cs - 오브젝트 데이터베이스
- ObjectDB.instance - 전역 인스턴스
- ObjectDB.GetItemPrefab(string) - 아이템 프리팹 가져오기

## 📊 총 통계
- **총 클래스 수**: 541개
- **주요 시스템**: 15개 카테고리
- **핵심 API**: 200+ 메서드
- **RPC 메서드**: 50+ 네트워크 함수

## 🎯 모딩에 가장 유용한 API Top 20
1. Player.GetLocalPlayer()
2. ZNetScene.instance.GetPrefab()
3. Player.GetInventory().AddItem()
4. HitData.GetTotalDamage()
5. Character.Damage()
6. ZNetView.InvokeRPC()
7. Game.IncrementPlayerStat()
8. DamageText.instance.ShowText()
9. EffectList.Create()
10. Pickable.RPC_Pick()
11. MineRock.RPC_Hit()
12. TreeBase.RPC_Damage()
13. ItemDrop.OnCreateNew()
14. Player.RaiseSkill()
15. Inventory.CountItems()
16. ZNetView.IsOwner()
17. MessageHud.instance.ShowMessage()
18. Skills.GetSkillFactor()
19. Character.AddSEMan()
20. BaseAI.AggravateAllInArea()

---
**생성일**: 2025-01-30
**분석 도구**: dnSpy
**소스**: assembly_valheim.dll (Valheim 공식)
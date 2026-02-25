using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 생산 전문가 스킬 효과 구현 클래스
    /// 벌목, 채집, 채광 시 확률적으로 추가 자원 획득
    /// </summary>
    public static class ProductionEffects
    {
        // ===== 생산 스킬 발동 확률 =====
        private const float PRODUCTION_ROOT_CHANCE = 0.5f; // 50% 확률
        private const float NOVICE_WORKER_CHANCE = 0.25f;  // 25% 확률
        private const float WOODCUTTING_LV2_CHANCE = 0.25f; // 벌목 Lv2
        private const float GATHERING_LV2_CHANCE = 0.25f;   // 채집 Lv2
        private const float MINING_LV2_CHANCE = 0.25f;      // 채광 Lv2

        // 중복 발동 방지용 트래킹
        private static readonly Dictionary<int, float> processedTrees = new Dictionary<int, float>();
        private const float TREE_COOLDOWN = 1f; // 1초 쿨다운

        /// <summary>
        /// 벌목 시 추가 나무 획득 처리 (TreeLog 파괴 시 - 드롭 시점)
        /// EpicLoot 방식 참고: 실제 아이템 드롭 시점에 보너스 적용
        /// </summary>
        [HarmonyPatch(typeof(TreeLog), nameof(TreeLog.Destroy))]
        public static class TreeLog_Destroy_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Prefix(TreeLog __instance, HitData hitData)
            {
                try
                {
                    // 플레이어가 공격했는지 확인
                    if (hitData?.GetAttacker() != Player.m_localPlayer) return;
                    if (Player.m_localPlayer == null) return;

                    // 생산 전문가 스킬이 없으면 전체 로직 건너뛰기 (최적화)
                    var manager = SkillTreeManager.Instance;
                    if (manager == null || manager.GetSkillLevel("production_root") <= 0) return;

                    // 중복 처리 방지
                    int treeId = __instance.GetInstanceID();
                    float currentTime = Time.time;
                    if (processedTrees.ContainsKey(treeId) &&
                        currentTime - processedTrees[treeId] < TREE_COOLDOWN) return;

                    processedTrees[treeId] = currentTime;

                    // 실제 드롭 위치에서 보너스 아이템 생성 (EpicLoot 방식)
                    TryDropExtraWood(hitData.GetAttacker(), __instance.m_dropWhenDestroyed, __instance.transform.position);
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[생산 효과] TreeLog 파괴 처리 중 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 벌목 시 추가 나무 획득 처리 (TreeBase 파괴 시 - 드롭 시점)
        /// EpicLoot 방식 참고: 실제 아이템 드롭 시점에 보너스 적용
        /// </summary>
        [HarmonyPatch(typeof(TreeBase), nameof(TreeBase.Damage))]
        public static class TreeBase_Damage_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(TreeBase __instance, HitData hit)
            {
                try
                {
                    // 플레이어가 공격했는지 확인
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;
                    if (Player.m_localPlayer == null) return;

                    // 생산 전문가 스킬이 없으면 전체 로직 건너뛰기 (최적화)
                    var manager = SkillTreeManager.Instance;
                    if (manager == null || manager.GetSkillLevel("production_root") <= 0) return;

                    // 나무가 파괴되었는지 확인 (체력이 0 이하)
                    if (__instance.m_health > 0) return; // 아직 파괴되지 않음

                    // 중복 처리 방지
                    int treeId = __instance.GetInstanceID();
                    float currentTime = Time.time;
                    if (processedTrees.ContainsKey(treeId) &&
                        currentTime - processedTrees[treeId] < TREE_COOLDOWN) return;

                    processedTrees[treeId] = currentTime;

                    // 실제 드롭 위치에서 보너스 아이템 생성 (EpicLoot 방식)
                    TryDropExtraWood(hit.GetAttacker(), __instance.m_dropWhenDestroyed, __instance.transform.position);
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[생산 효과] TreeBase 파괴 처리 중 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 채집 시 추가 자원 획득 처리 (Pickable 아이템 채집 시)
        /// Publicized Assembly를 통해 RPC_Pick 접근 가능
        /// </summary>
        [HarmonyPatch(typeof(Pickable), "RPC_Pick")]
        public static class Pickable_Pick_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Pickable __instance)
            {
                try
                {
                    if (Player.m_localPlayer == null) return;

                    // 생산 전문가 스킬이 없으면 전체 로직 건너뛰기 (최적화)
                    var manager = SkillTreeManager.Instance;
                    if (manager == null || manager.GetSkillLevel("production_root") <= 0) return;

                    // 채집한 아이템 이름 확인
                    string resourceName = GetPickableResourceName(__instance);
                    if (string.IsNullOrEmpty(resourceName)) return;

                    // 채집 보너스 시도
                    TryDropExtraResource(resourceName, __instance.transform.position, "gathering");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[생산 효과] 채집 처리 중 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 채광 시 추가 자원 획득 처리 (MineRock 파괴 시 - 드롭 시점)
        /// MMO 시스템 방식과 동일하게 RPC_Hit 패치 사용
        /// </summary>
        [HarmonyPatch(typeof(MineRock), "RPC_Hit")]
        public static class MineRock_Hit_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(MineRock __instance, HitData hit)
            {
                try
                {
                    // 플레이어가 공격했고, 광석이 파괴되었는지 확인
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;
                    if (Player.m_localPlayer == null) return;

                    // 생산 전문가 스킬이 없으면 전체 로직 건너뛰기 (최적화)
                    var manager = SkillTreeManager.Instance;
                    if (manager == null || manager.GetSkillLevel("production_root") <= 0) return;

                    if (__instance.m_health > 0) return; // 아직 파괴되지 않음

                    // 중복 처리 방지
                    int rockId = __instance.GetInstanceID();
                    float currentTime = Time.time;
                    if (processedTrees.ContainsKey(rockId) &&
                        currentTime - processedTrees[rockId] < TREE_COOLDOWN) return;

                    processedTrees[rockId] = currentTime;

                    // 채광 보너스 적용 (광석 종류에 따라 다름)
                    TryDropExtraMiningResource(__instance.m_dropItems, __instance.transform.position);
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[생산 효과] MineRock 파괴 처리 중 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 채광 시 추가 자원 획득 처리 (MineRock5 파괴 시)
        /// Publicized Assembly를 통해 RPC_Damage 접근 가능
        /// </summary>
        [HarmonyPatch(typeof(MineRock5), "RPC_Damage")]
        public static class MineRock5_Damage_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(MineRock5 __instance, HitData hit)
            {
                try
                {
                    // 플레이어가 공격했고, 광석이 파괴되었는지 확인
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;
                    if (Player.m_localPlayer == null) return;

                    // 생산 전문가 스킬이 없으면 전체 로직 건너뛰기 (최적화)
                    var manager = SkillTreeManager.Instance;
                    if (manager == null || manager.GetSkillLevel("production_root") <= 0) return;

                    if (__instance.m_health > 0) return; // 아직 파괴되지 않음

                    // 중복 처리 방지
                    int rockId = __instance.GetInstanceID();
                    float currentTime = Time.time;
                    if (processedTrees.ContainsKey(rockId) &&
                        currentTime - processedTrees[rockId] < TREE_COOLDOWN) return;

                    processedTrees[rockId] = currentTime;

                    // 채광 보너스 적용
                    TryDropExtraMiningResource(__instance.m_dropItems, __instance.transform.position);
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[생산 효과] MineRock5 파괴 처리 중 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 채광 시 추가 자원 획득 처리 (Destructible 파괴 시)
        /// Publicized Assembly를 통해 RPC_Damage 접근 가능
        /// </summary>
        [HarmonyPatch(typeof(Destructible), "RPC_Damage")]
        public static class Destructible_Damage_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Destructible __instance, HitData hit)
            {
                try
                {
                    // 플레이어가 공격했고, 오브젝트가 파괴되었는지 확인
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;
                    if (Player.m_localPlayer == null) return;

                    // 생산 전문가 스킬이 없으면 전체 로직 건너뛰기 (최적화)
                    var manager = SkillTreeManager.Instance;
                    if (manager == null || manager.GetSkillLevel("production_root") <= 0) return;

                    if (__instance.m_health > 0) return; // 아직 파괴되지 않음

                    // 중복 처리 방지
                    int objId = __instance.GetInstanceID();
                    float currentTime = Time.time;
                    if (processedTrees.ContainsKey(objId) &&
                        currentTime - processedTrees[objId] < TREE_COOLDOWN) return;

                    processedTrees[objId] = currentTime;

                    // 파괴 가능한 광석인지 확인 후 보너스 적용
                    if (IsMinableDestructible(__instance))
                    {
                        var dropComponent = __instance.GetComponent<DropOnDestroyed>();
                        if (dropComponent?.m_dropWhenDestroyed != null)
                        {
                            TryDropExtraMiningResource(dropComponent.m_dropWhenDestroyed, __instance.transform.position);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[생산 효과] Destructible 파괴 처리 중 오류: {ex.Message}");
                }
            }
        }

        // 제작 보너스는 기존 CraftingEnhancement.cs 시스템을 통해 처리됨
        // CraftingEnhancement.ApplyCraftingEnhancement에서 제작 완료 시 ProductionEffects.CheckAndApplyCraftingBonus 호출

        /// <summary>
        /// 나무 추가 드롭 시도 (EpicLoot 방식 참고)
        /// 실제 아이템이 월드에 드롭되는 위치에 보너스 아이템 생성
        /// </summary>
        private static void TryDropExtraWood(Character attacker, DropTable dropTable, Vector3 dropPosition)
        {
            if (attacker != Player.m_localPlayer) return;
            if (Player.m_localPlayer == null) return;

            var manager = SkillTreeManager.Instance;
            if (manager == null) return;

            float totalChance = 0f;
            string effectSource = "";

            // 생산 전문가 (production_root) - 50% 확률
            // ※ 패치 메서드에서 이미 체크했으므로 여기서는 무조건 보유
            totalChance += PRODUCTION_ROOT_CHANCE;
            effectSource = "생산 전문가";

            // 초보 일꾼 - 25% 확률 (누적)
            if (manager.GetSkillLevel("novice_worker") > 0)
            {
                totalChance += NOVICE_WORKER_CHANCE;
                effectSource = "초보 일꾼";
            }

            // 벌목 Lv2 - 25% 확률 (누적)
            if (manager.GetSkillLevel("woodcutting_lv2") > 0)
            {
                totalChance += WOODCUTTING_LV2_CHANCE;
                effectSource = "벌목 Lv2";
            }

            // 벌목 Lv3 - 35% 확률 (누적)
            if (manager.GetSkillLevel("woodcutting_lv3") > 0)
            {
                totalChance += 0.35f;
                effectSource = "벌목 Lv3";
            }

            // 벌목 Lv4 - 45% 확률 (누적)
            if (manager.GetSkillLevel("woodcutting_lv4") > 0)
            {
                totalChance += 0.45f;
                effectSource = "벌목 Lv4";
            }

            // 확률 체크 및 월드에 아이템 드롭
            float randomValue = UnityEngine.Random.Range(0f, 1f);
            Plugin.Log.LogInfo($"[생산 효과] 확률 체크: 롤링값 {randomValue:F3}, 필요값 {totalChance:F3}");

            if (totalChance > 0 && randomValue < totalChance)
            {
                Plugin.Log.LogInfo($"[생산 효과] 벌목 보너스 발동! {effectSource}");

                // 드롭 개수 결정: Lv3/Lv4는 +2, 나머지는 +1
                int dropCount = (manager.GetSkillLevel("woodcutting_lv3") > 0 ||
                                manager.GetSkillLevel("woodcutting_lv4") > 0) ? 2 : 1;

                // DropTable에서 실제 드롭되는 나무 종류 확인
                string woodItemName = "Wood"; // 기본값
                GameObject woodPrefab = null;

                if (dropTable?.m_drops != null && dropTable.m_drops.Count > 0)
                {
                    // 첫 번째 드롭 아이템을 확인 (보통 나무 종류)
                    var firstDrop = dropTable.m_drops[0];
                    if (firstDrop.m_item != null)
                    {
                        woodItemName = firstDrop.m_item.name;
                        woodPrefab = firstDrop.m_item.gameObject;
                        Plugin.Log.LogInfo($"[생산 효과] DropTable에서 발견한 나무 종류: {woodItemName}");
                    }
                }

                // DropTable에서 찾지 못한 경우 기본 Wood 사용
                if (woodPrefab == null)
                {
                    woodPrefab = ZNetScene.instance.GetPrefab("Wood");
                    Plugin.Log.LogInfo($"[생산 효과] 기본 Wood 프리팹 사용");
                }

                if (woodPrefab != null)
                {
                    // 나무 종류별 한국어 이름 매핑
                    string displayName = woodItemName switch
                    {
                        "Wood" => "나무",
                        "FineWood" => "질 좋은 나무",
                        "RoundLog" => "둥근 통나무",
                        "ElderBark" => "고대의 나무껍질",
                        "YggdrasilWood" => "위그드라실 나무",
                        "CoreWood" => "코어 우드",
                        _ => woodItemName
                    };

                    for (int i = 0; i < dropCount; i++)
                    {
                        // 드롭 위치를 약간 랜덤하게 조정 (겹치지 않도록)
                        Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 0.5f;
                        randomOffset.y = 0f; // 높이는 고정
                        Vector3 finalDropPosition = dropPosition + randomOffset;

                        // ItemDrop 생성하여 월드에 배치
                        var itemDrop = woodPrefab.GetComponent<ItemDrop>();
                        if (itemDrop != null)
                        {
                            GameObject droppedItem = UnityEngine.Object.Instantiate(woodPrefab, finalDropPosition, Quaternion.identity);
                        }
                    }

                    // 플레이어 머리 위에 메시지 표시 (MMO 방식 DamageText)
                    SkillEffect.DrawFloatingText(Player.m_localPlayer,
                        $"🪓 {displayName} +{dropCount}",
                        new Color(0.4f, 0.8f, 0.2f, 1f)); // 자연스러운 초록색

                    // 벌목 효율 텍스트도 실제 나무 획득 시에만 표시
                    ShowWoodcuttingEfficiencyText();

                    Plugin.Log.LogInfo($"[생산 효과] 벌목 보너스 발동 성공: {effectSource} - {displayName} +{dropCount} 월드에 드롭됨");
                }
            }
            else
            {
                Plugin.Log.LogInfo($"[생산 효과] 벌목 보너스 미발동 - 확률: {totalChance * 100:F1}%, 롤링: {randomValue * 100:F1}%");
            }
        }

        /// <summary>
        /// 채집 자원 추가 드롭 시도
        /// </summary>
        private static void TryDropExtraResource(string resourceName, Vector3 dropPosition, string activityType)
        {
            if (Player.m_localPlayer == null) return;
            if (string.IsNullOrEmpty(resourceName)) return;

            var manager = SkillTreeManager.Instance;
            if (manager == null) return;

            float totalChance = 0f;
            string effectSource = "";

            // 생산 전문가 기본 확률
            if (manager.GetSkillLevel("production_root") > 0)
            {
                totalChance += PRODUCTION_ROOT_CHANCE;
                effectSource = "생산 전문가";
            }
            else
            {
                return; // 생산 전문가 스킬이 없으면 보너스 없음
            }

            // 초보 일꾼 보너스
            if (manager.GetSkillLevel("novice_worker") > 0)
            {
                totalChance += NOVICE_WORKER_CHANCE;
                effectSource = "초보 일꾼";
            }

            // 채집 관련 스킬 보너스
            if (activityType == "gathering")
            {
                if (manager.GetSkillLevel("gathering_lv2") > 0)
                {
                    totalChance += GATHERING_LV2_CHANCE;
                    effectSource = "채집 Lv2";
                }
                if (manager.GetSkillLevel("gathering_lv3") > 0)
                {
                    totalChance += 0.35f;
                    effectSource = "채집 Lv3";
                }
                if (manager.GetSkillLevel("gathering_lv4") > 0)
                {
                    totalChance += 0.40f;
                    effectSource = "채집 Lv4";
                }
            }

            // 확률 체크 및 드롭
            if (UnityEngine.Random.Range(0f, 1f) < totalChance)
            {
                DropResourceItem(resourceName, dropPosition, effectSource);
            }
        }

        /// <summary>
        /// 채광 자원 추가 드롭 시도
        /// </summary>
        private static void TryDropExtraMiningResource(DropTable dropItems, Vector3 dropPosition)
        {
            if (Player.m_localPlayer == null) return;
            if (dropItems?.m_drops == null || dropItems.m_drops.Count == 0) return;

            var manager = SkillTreeManager.Instance;
            if (manager == null) return;

            float totalChance = 0f;
            string effectSource = "";

            // 생산 전문가 기본 확률
            if (manager.GetSkillLevel("production_root") > 0)
            {
                totalChance += PRODUCTION_ROOT_CHANCE;
                effectSource = "생산 전문가";
            }
            else
            {
                return; // 생산 전문가 스킬이 없으면 보너스 없음
            }

            // 초보 일꾼 보너스
            if (manager.GetSkillLevel("novice_worker") > 0)
            {
                totalChance += NOVICE_WORKER_CHANCE;
                effectSource = "초보 일꾼";
            }

            // 채광 관련 스킬 보너스
            if (manager.GetSkillLevel("mining_lv2") > 0)
            {
                totalChance += MINING_LV2_CHANCE;
                effectSource = "채광 Lv2";
            }
            if (manager.GetSkillLevel("mining_lv3") > 0)
            {
                totalChance += 0.3f;
                effectSource = "채광 Lv3";
            }

            // 확률 체크 및 드롭
            if (UnityEngine.Random.Range(0f, 1f) < totalChance)
            {
                // DropTable에서 첫 번째 드롭 아이템 사용
                var firstDrop = dropItems.m_drops[0];
                if (firstDrop.m_item != null)
                {
                    string resourceName = firstDrop.m_item.name;
                    DropResourceItem(resourceName, dropPosition, effectSource);
                }
                else
                {
                    // 기본값으로 Stone 사용
                    DropResourceItem("Stone", dropPosition, effectSource);
                }
            }
        }

        /// <summary>
        /// 실제 자원 아이템을 월드에 드롭
        /// </summary>
        private static void DropResourceItem(string resourceName, Vector3 dropPosition, string effectSource)
        {
            var resourcePrefab = ZNetScene.instance.GetPrefab(resourceName);
            if (resourcePrefab != null)
            {
                // 드롭 위치를 약간 랜덤하게 조정
                Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 0.5f;
                randomOffset.y = 0f;
                Vector3 finalDropPosition = dropPosition + randomOffset;

                // ItemDrop 생성하여 월드에 배치
                var itemDrop = resourcePrefab.GetComponent<ItemDrop>();
                if (itemDrop != null)
                {
                    GameObject droppedItem = UnityEngine.Object.Instantiate(resourcePrefab, finalDropPosition, Quaternion.identity);
                    
                    // 자원별 한국어 이름 매핑
                    string displayName = resourceName switch
                    {
                        "Wood" => "나무",
                        "Stone" => "돌",
                        "Bronze" => "청동",
                        "Iron" => "철",
                        "Copper" => "구리",
                        "Tin" => "주석",
                        "Silver" => "은",
                        "Flint" => "부싯돌",
                        "Raspberry" => "산딸기",
                        "Blueberries" => "블루베리",
                        "Mushroom" => "버섯",
                        "MushroomYellow" => "노란버섯",
                        "MushroomBlue" => "파란버섯",
                        "Coal" => "석탄",
                        "Obsidian" => "흑요석",
                        _ => resourceName
                    };
                    
                    // 플레이어 머리 위에 메시지 표시 (MMO 방식 DamageText)
                    SkillEffect.DrawFloatingText(Player.m_localPlayer, 
                        $"⚒️ {displayName} +1", 
                        new Color(0.2f, 0.8f, 0.9f, 1f)); // 자연스러운 청록색
                    
                    Plugin.Log.LogInfo($"[생산 효과] {effectSource} 보너스 발동: {displayName} +1 월드에 드롭됨");
                }
            }
        }

        /// <summary>
        /// 제작 보너스 확인 및 적용 (CraftingEnhancement.cs에서 호출됨)
        /// </summary>
        public static void CheckAndApplyCraftingBonus()
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return;

            float totalChance = 0f;
            string effectSource = "";

            // 제작 Lv2 - 25% 확률
            if (manager.GetSkillLevel("crafting_lv2") > 0)
            {
                totalChance += 0.25f;
                effectSource = "제작 Lv2";
            }

            // 제작 Lv3 - 30% 확률 (누적)
            if (manager.GetSkillLevel("crafting_lv3") > 0)
            {
                totalChance += 0.3f;
                effectSource = "제작 Lv3";
            }

            // 제작 Lv4 - 35% 확률 (누적)
            if (manager.GetSkillLevel("crafting_lv4") > 0)
            {
                totalChance += 0.35f;
                effectSource = "제작 Lv4";
            }

            // 확률 체크 및 보너스 재료 추가
            if (totalChance > 0 && UnityEngine.Random.Range(0f, 1f) < totalChance)
            {
                var inventory = Player.m_localPlayer.GetInventory();
                if (inventory != null)
                {
                    // 제작 관련 재료들 중 랜덤하게 하나 선택
                    string[] craftingMaterials = { "Wood", "Stone", "Bronze", "Iron" };
                    string selectedMaterial = craftingMaterials[UnityEngine.Random.Range(0, craftingMaterials.Length)];
                    
                    var materialPrefab = ZNetScene.instance.GetPrefab(selectedMaterial);
                    if (materialPrefab != null)
                    {
                        bool added = inventory.AddItem(materialPrefab, 1);
                        if (added)
                        {
                            // 한국어 이름으로 변환
                            string displayName = selectedMaterial switch
                            {
                                "Wood" => "나무",
                                "Stone" => "석재", 
                                "Bronze" => "청동",
                                "Iron" => "철",
                                _ => selectedMaterial
                            };
                            
                            // 플레이어 머리 위에 메시지 표시 (MMO 방식 DamageText)
                            SkillEffect.DrawFloatingText(Player.m_localPlayer, 
                                $"🔨 {displayName} +1", 
                                new Color(1f, 0.9f, 0.3f, 1f)); // 자연스러운 노란색
                            
                            Plugin.Log.LogInfo($"[생산 효과] 제작 보너스 발동: {effectSource} - {displayName} +1");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pickable 오브젝트에서 채집할 수 있는 자원 이름 추출
        /// </summary>
        private static string GetPickableResourceName(Pickable pickable)
        {
            if (pickable?.m_itemPrefab?.name != null)
            {
                return pickable.m_itemPrefab.name;
            }
            
            // 이름으로 추정 (베리류, 버섯류 등)
            string objectName = pickable.name.Replace("(Clone)", "");
            return objectName switch
            {
                "RaspberryBush" => "Raspberry",
                "BlueberryBush" => "Blueberries", 
                "Pickable_Mushroom" => "Mushroom",
                "Pickable_MushroomYellow" => "MushroomYellow",
                "Pickable_MushroomBlue" => "MushroomBlue",
                "Pickable_Branch" => "Wood",
                "Pickable_Stone" => "Stone",
                "Pickable_Flint" => "Flint",
                _ => null
            };
        }

        /// <summary>
        /// Destructible이 채광 가능한 광석/자원인지 확인
        /// </summary>
        private static bool IsMinableDestructible(Destructible destructible)
        {
            if (destructible == null) return false;
            
            string objectName = destructible.name.Replace("(Clone)", "");
            
            // 채광 가능한 오브젝트들 (광석, 은행나무 등)
            return objectName.Contains("rock") || 
                   objectName.Contains("Rock") ||
                   objectName.Contains("copper") ||
                   objectName.Contains("tin") ||
                   objectName.Contains("iron") ||
                   objectName.Contains("silver") ||
                   objectName.Contains("Copper") ||
                   objectName.Contains("Tin") ||
                   objectName.Contains("Iron") ||
                   objectName.Contains("Silver") ||
                   objectName.Contains("Obsidian");
        }

        /// <summary>
        /// 벌목 효율 텍스트를 실제 나무 획득 시에만 표시
        /// </summary>
        private static void ShowWoodcuttingEfficiencyText()
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null || Player.m_localPlayer == null) return;

            // 벌목 효율 스킬을 학습했는지 확인
            int efficiencyBonus = 0;
            
            // 벌목 Lv2: +20% 효율
            if (manager.GetSkillLevel("woodcutting_lv2") > 0)
            {
                efficiencyBonus = 20;
            }
            
            // 벌목 Lv3: +30% 효율
            if (manager.GetSkillLevel("woodcutting_lv3") > 0)
            {
                efficiencyBonus = 30;
            }
            
            // 벌목 Lv4: +40% 효율
            if (manager.GetSkillLevel("woodcutting_lv4") > 0)
            {
                efficiencyBonus = 40;
            }
            
            // 벌목 효율 스킬이 있을 때만 텍스트 표시 (8% 확률)
            if (efficiencyBonus > 0 && UnityEngine.Random.Range(0f, 1f) < 0.08f)
            {
                SkillEffect.DrawFloatingText(Player.m_localPlayer, 
                    $"🪓 벌목 효율 +{efficiencyBonus}%", 
                    new Color(0.8f, 0.6f, 0.2f, 1f)); // 나무 비슷한 갈색
                
                Plugin.Log.LogInfo($"[생산 효과] 벌목 효율 텍스트 표시: +{efficiencyBonus}% (실제 나무 획득 시)");
            }
        }
    }
}
using System;
using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 자원 채집(벌목/채집/채광 등) 시 활성 Gather 퀘스트 진행도를 증가시키는 패치.
    ///
    /// Valheim에서 벌목/채광/Pickable 채집은 전부 월드에 아이템을 "드롭"한 뒤 플레이어가
    /// 걸어서 줍는 구조이며(Pickable.RPC_Pick도 즉시 인벤토리에 넣지 않고 Drop만 함),
    /// 실제 습득은 항상 Humanoid.Pickup() → Inventory.AddItem(ItemDrop.ItemData) 한 지점으로
    /// 모인다. 따라서 TreeBase/Pickable/MineRock 등 개별 소스를 각각 패치하는 대신
    /// 이 지점 하나만 후킹한다. (참고: Inventory.AddItem(GameObject, int)은 내부적으로
    /// 이 오버로드를 호출하도록 위임되어 있어 낚시 등 다른 획득 경로도 함께 커버된다.)
    ///
    /// 단, 이 지점만으로는 "그 아이템이 어디서 왔는지"(내가 직접 채집 vs 남이 떨군 것/남이
    /// 채집한 것을 주움) 구분이 안 되므로, QuestGatherOriginTracker가 관리하는 "방금 직접
    /// 채집 행위를 했다" 플래그(6초 유효)가 살아있을 때만 진행도를 인정한다.
    ///
    /// 나무(Wood)는 걸어서 자동 습득하는 타이밍이 불안정해 QuestGatherOriginTracker가
    /// 벌목/통나무 파괴 시점에 드롭테이블을 직접 읽어 즉시 지급한다 - 여기서 또 세면
    /// 중복 지급되므로 Wood는 이 후킹에서 제외한다.
    /// </summary>
    [HarmonyPatch(typeof(Inventory), nameof(Inventory.AddItem), typeof(ItemDrop.ItemData))]
    public static class QuestGatherPatch
    {
        [HarmonyPrefix]
        public static void Prefix(ItemDrop.ItemData item, out int __state)
        {
            // AddItem 내부에서 item.m_stack이 처리 도중 변형되므로, 시도한 원래 수량을 미리 저장해둔다.
            __state = item?.m_stack ?? 0;
        }

        [HarmonyPostfix]
        public static void Postfix(Inventory __instance, ItemDrop.ItemData item, bool __result, int __state)
        {
            try
            {
                if (!__result || __state <= 0) return;
                if (!Quest_Config.IsEnabled) return;

                var player = Player.m_localPlayer;
                if (player == null || player.GetInventory() != __instance) return;

                var prefab = item?.m_dropPrefab;
                if (prefab == null) return;

                string prefabName = Utils.GetPrefabName(prefab);
                if (string.IsNullOrEmpty(prefabName)) return;
                if (prefabName == "Wood") return; // QuestGatherOriginTracker가 벌목 시점에 직접 지급함(중복 방지)

                var quest = QuestManager.FindByTarget(QuestType.Gather, prefabName);
                if (quest == null) return;

                bool selfHarvested = QuestGatherOriginTracker.RecentlySelfHarvested;
                Plugin.Log.LogInfo($"[QuestGatherPatch] 인벤토리 획득 감지: {prefabName} x{__state} (직접채집플래그={selfHarvested}, 대상퀘스트={quest.Key})");

                if (!selfHarvested) return; // 직접 채집한 게 아니면 무시

                QuestManager.AddProgress(player, quest, __state);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestGatherPatch] 오류: {ex.Message}");
            }
        }
    }
}

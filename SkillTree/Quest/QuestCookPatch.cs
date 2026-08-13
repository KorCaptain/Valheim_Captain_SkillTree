using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 요리대에서 완성된 요리를 수거할 때 활성 Cook 퀘스트 진행도를 증가시키는 패치.
    /// CookingStation.HaveDoneItem/GetSlot/IsItemDone은 실제 게임 어셈블리에서 public이
    /// 아니라(참고용 디컴파일 소스와 다름) AccessTools 리플렉션으로 우회해서 호출한다.
    /// CookingStation.OnInteract()는 완성 슬롯이 있으면 그 자리에서 RPC로 슬롯을 비우고
    /// 아이템을 지급하므로, Prefix에서 원본 실행 전에 완성된 슬롯의 프리팹명을 미리 읽어
    /// __state에 담아뒀다가 Postfix에서 사용한다.
    /// </summary>
    [HarmonyPatch]
    public static class QuestCookPatch
    {
        private static readonly MethodInfo HaveDoneItemMethod = AccessTools.Method(typeof(CookingStation), "HaveDoneItem");
        private static readonly MethodInfo GetSlotMethod = AccessTools.Method(typeof(CookingStation), "GetSlot");
        private static readonly MethodInfo IsItemDoneMethod = AccessTools.Method(typeof(CookingStation), "IsItemDone");
        private static readonly FieldInfo SlotsField = AccessTools.Field(typeof(CookingStation), "m_slots");

        static QuestCookPatch()
        {
            if (HaveDoneItemMethod == null || GetSlotMethod == null || IsItemDoneMethod == null || SlotsField == null)
            {
                Plugin.Log.LogError($"[QuestCookPatch] 리플렉션 초기화 실패 - HaveDoneItem:{HaveDoneItemMethod != null} GetSlot:{GetSlotMethod != null} IsItemDone:{IsItemDoneMethod != null} m_slots:{SlotsField != null} (요리 퀘스트 진행도가 절대 증가하지 않음)");
            }
        }

        [HarmonyPatch(typeof(CookingStation), "OnInteract")]
        [HarmonyPrefix]
        public static void Prefix(CookingStation __instance, out string __state)
        {
            __state = null;
            try
            {
                if (!Quest_Config.IsEnabled || __instance == null) return;
                if (HaveDoneItemMethod == null || GetSlotMethod == null || IsItemDoneMethod == null || SlotsField == null) return;
                if (!(bool)HaveDoneItemMethod.Invoke(__instance, null)) return;

                var slots = (Transform[])SlotsField.GetValue(__instance);
                for (int i = 0; i < slots.Length; i++)
                {
                    object[] args = { i, null, null, null };
                    GetSlotMethod.Invoke(__instance, args);
                    string itemName = args[1] as string;
                    if (!string.IsNullOrEmpty(itemName) && (bool)IsItemDoneMethod.Invoke(__instance, new object[] { itemName }))
                    {
                        __state = itemName;
                        Plugin.Log.LogInfo($"[QuestCookPatch] 완성된 요리 감지: {itemName}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestCookPatch] Prefix 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(CookingStation), "OnInteract")]
        [HarmonyPostfix]
        public static void Postfix(Humanoid user, bool __result, string __state)
        {
            try
            {
                if (!Quest_Config.IsEnabled || !__result || string.IsNullOrEmpty(__state)) return;

                var player = Player.m_localPlayer;
                if (player == null || user != player) return;

                int matched = 0;
                foreach (var quest in QuestManager.GetActiveQuests().Where(q => q.Type == QuestType.Cook))
                {
                    matched++;
                    if (quest.RequireDistinctTargets)
                        QuestManager.AddDistinctProgress(player, quest, __state);
                    else
                        QuestManager.AddProgress(player, quest, 1);
                }
                Plugin.Log.LogInfo($"[QuestCookPatch] {__state} 수거 처리 - 매칭된 Cook 퀘스트 {matched}개에 진행도 반영");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestCookPatch] Postfix 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 가마솥(Cauldron)/제작 메뉴로 만드는 요리(수프 등)를 수거할 때 활성 Cook 퀘스트 진행도를 증가시키는 패치.
    /// 이 요리는 CookingStation.OnInteract가 아니라 InventoryGui.DoCrafting → Inventory.AddItem(string, ...)
    /// (Vector2i position을 받는 7-arg 오버로드) 한 지점으로만 지급된다 - 디컴파일 소스 전체에서 이 오버로드의
    /// 유일한 호출부가 InventoryGui.DoCrafting(1304번째 줄)이므로, 다른 획득 경로(채집/전리품/거래 등)와
    /// 절대 겹치지 않는다. 완성된 아이템이 음식(m_food > 0)인 경우에만 진행도를 반영한다.
    /// </summary>
    [HarmonyPatch(typeof(Inventory), nameof(Inventory.AddItem),
        new[] { typeof(string), typeof(int), typeof(int), typeof(int), typeof(long), typeof(string), typeof(Vector2i), typeof(bool) })]
    public static class QuestCookCraftedPatch
    {
        [HarmonyPostfix]
        public static void Postfix(Inventory __instance, ItemDrop.ItemData __result)
        {
            try
            {
                if (__result?.m_shared == null || __result.m_shared.m_food <= 0f) return;
                if (!Quest_Config.IsEnabled) return;

                var player = Player.m_localPlayer;
                if (player == null || player.GetInventory() != __instance) return;

                string prefabName = __result.m_dropPrefab != null ? __result.m_dropPrefab.name : __result.m_shared.m_name;
                if (string.IsNullOrEmpty(prefabName)) return;

                int matched = 0;
                foreach (var quest in QuestManager.GetActiveQuests().Where(q => q.Type == QuestType.Cook))
                {
                    matched++;
                    if (quest.RequireDistinctTargets)
                        QuestManager.AddDistinctProgress(player, quest, prefabName);
                    else
                        QuestManager.AddProgress(player, quest, 1);
                }
                if (matched > 0)
                    Plugin.Log.LogInfo($"[QuestCookCraftedPatch] {prefabName} 제작 감지 - 매칭된 Cook 퀘스트 {matched}개에 진행도 반영");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestCookCraftedPatch] Postfix 오류: {ex.Message}");
            }
        }
    }
}

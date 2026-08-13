using System;
using System.Linq;
using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>낚시로 물고기를 낚아 올릴 때 활성 Fish 퀘스트 진행도를 증가시키는 패치.</summary>
    [HarmonyPatch]
    public static class QuestFishPatch
    {
        [HarmonyPatch(typeof(FishingFloat), "Catch")]
        [HarmonyPostfix]
        public static void Postfix(Fish fish, Character owner)
        {
            try
            {
                if (!Quest_Config.IsEnabled || fish == null) return;

                var player = Player.m_localPlayer;
                if (player == null || owner != player) return;

                string prefabName = Utils.GetPrefabName(fish.gameObject);
                if (string.IsNullOrEmpty(prefabName)) return;

                foreach (var quest in QuestManager.GetActiveQuests().Where(q => q.Type == QuestType.Fish))
                {
                    if (quest.RequireDistinctTargets)
                        QuestManager.AddDistinctProgress(player, quest, prefabName);
                    else
                        QuestManager.AddProgress(player, quest, 1);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestFishPatch] 오류: {ex.Message}");
            }
        }
    }
}

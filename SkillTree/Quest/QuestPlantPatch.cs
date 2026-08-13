using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 작물(씨앗)을 심을 때 활성 Plant 퀘스트 진행도를 증가시키는 패치.
    /// 과거에는 Plant.Awake() + ZNetView.IsOwner()로 "방금 이 클라이언트가 심었다"를
    /// 추정했으나, Balrond Amazing Nature 같은 모드가 존(zone) 진입 시 야생 식물을
    /// 로컬에서 직접 Instantiate하면 그 클라이언트가 ZDO 소유권을 갖게 되어 실제로
    /// 심지 않았는데도 퀘스트가 진행되는 오탐이 있었다. Player.PlacePiece()는 건축/
    /// 재배 UI로 로컬 플레이어가 배치를 실제로 확정했을 때만 호출되므로 이 지점을
    /// 후킹해 "직접 심음"을 판별한다.
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.PlacePiece), typeof(Piece), typeof(Vector3), typeof(Quaternion), typeof(bool))]
    public static class QuestPlantPatch
    {
        [HarmonyPostfix]
        public static void Postfix(Player __instance, Piece piece)
        {
            try
            {
                if (!Quest_Config.IsEnabled || piece == null) return;
                if (__instance != Player.m_localPlayer) return;
                if (piece.GetComponent<Plant>() == null) return; // 씨앗/작물 피스만 대상

                var player = Player.m_localPlayer;
                if (player == null) return;

                string prefabName = Utils.GetPrefabName(piece.gameObject);
                if (string.IsNullOrEmpty(prefabName)) return;

                foreach (var quest in QuestManager.GetActiveQuests().Where(q => q.Type == QuestType.Plant))
                {
                    if (quest.RequireDistinctTargets)
                        QuestManager.AddDistinctProgress(player, quest, prefabName);
                    else
                        QuestManager.AddProgress(player, quest, 1);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestPlantPatch] 오류: {ex.Message}");
            }
        }
    }
}

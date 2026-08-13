using System;
using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 몬스터/보스 처치 시 활성 Kill/KillBoss 퀘스트 진행도를 증가시키는 패치.
    /// MMO_System/CaptainMMOPatches.cs의 OnDeath 후킹 패턴을 재사용하되,
    /// 경험치 시스템과는 독립적으로 항상 동작한다(EpicMMO 사용 여부 무관).
    /// </summary>
    [HarmonyPatch]
    public static class QuestKillPatch
    {
        [HarmonyPatch(typeof(Character), "OnDeath")]
        [HarmonyPostfix]
        public static void Postfix(Character __instance)
        {
            try
            {
                if (!Quest_Config.IsEnabled) return;
                if (__instance == null || __instance.IsPlayer()) return;
                if (__instance.IsTamed()) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

                string prefabName = Utils.GetPrefabName(__instance.gameObject);
                if (string.IsNullOrEmpty(prefabName)) return;

                // 이 프리팹이 활성 퀘스트와 무관하면(퀘스트와 상관없는 몹) 아래 크레딧 처리와
                // 파티 브로드캐스트 모두 건너뛴다 - 잦은 몹 사망마다 불필요한 RPC를 쏘지 않기 위함.
                bool isQuestRelevant = QuestManager.FindAllByTarget(QuestType.Kill, prefabName).Count > 0
                    || QuestManager.FindAllByTarget(QuestType.KillBoss, prefabName).Count > 0
                    || QuestManager.FindByTarget(QuestType.Gather, "Trophy" + prefabName) != null;
                if (!isQuestRelevant) return;

                // Character.OnDeath는 이 몬스터 ZDO의 소유(owner) 클라이언트에서만 실행되므로,
                // 여기서는 소유자 자신의 크레딧만 처리하고 나머지 파티원에게는 RPC로 전파한다.
                if (CaptainSkillTree.MMO_System.CaptainMMOPatches.IsKilledByPlayerOrParty(__instance, player))
                {
                    ApplyKillCredit(player, prefabName);
                }

                QuestKillPartySync.BroadcastKillCredit(prefabName, __instance.transform.position);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestKillPatch] 오류: {ex.Message}");
            }
        }

        /// <summary>주어진 플레이어 기준으로 몬스터 처치에 따른 퀘스트 진행도를 반영한다.
        /// 소유자 클라이언트의 로컬 처리와 QuestKillPartySync의 RPC 수신 처리가 이 메서드를 공유한다.</summary>
        internal static void ApplyKillCredit(Player player, string prefabName)
        {
            // 같은 몬스터를 목표로 하는 Kill 퀘스트가 둘 이상 있을 수 있으므로(예: 그레이드워프
            // 처치 퀘스트가 목표 수량만 다르게 여러 개 존재) 매칭되는 퀘스트 전부에 진행도를 반영한다.
            foreach (var quest in QuestManager.FindAllByTarget(QuestType.Kill, prefabName))
            {
                QuestManager.AddProgress(player, quest, 1);
            }
            foreach (var quest in QuestManager.FindAllByTarget(QuestType.KillBoss, prefabName))
            {
                // 무피해+무음식 특수 퀘스트는 일반 처치 진행과 달리 조건을 만족했을 때만 진행된다.
                if (quest.Key == "Meadows_Quest6")
                {
                    bool flawless = QuestEikthyrFlawlessTracker.WasFlawless() && player.GetFoods().Count == 0;
                    if (flawless)
                        QuestManager.AddProgress(player, quest, 1);
                    continue;
                }
                // 근접 무기로만 엘더 처치 특수 퀘스트도 조건을 만족했을 때만 진행된다.
                if (quest.Key == "BlackForest_Quest7")
                {
                    if (QuestGdKingMeleeOnlyTracker.WasMeleeOnly())
                        QuestManager.AddProgress(player, quest, 1);
                    continue;
                }
                // 마법 공격으로만 본메스 처치 특수 퀘스트도 조건을 만족했을 때만 진행된다.
                if (quest.Key == "Swamp_Quest7")
                {
                    if (QuestBonemassMagicOnlyTracker.WasMagicOnly())
                        QuestManager.AddProgress(player, quest, 1);
                    continue;
                }
                // 근접 무기로만 모더 처치 특수 퀘스트도 조건을 만족했을 때만 진행된다.
                if (quest.Key == "Mountain_Quest5")
                {
                    if (QuestModerMeleeOnlyTracker.WasMeleeOnly())
                        QuestManager.AddProgress(player, quest, 1);
                    continue;
                }
                // 원거리(활/석궁/마법) 무기로만 모더 처치 특수 퀘스트도 조건을 만족했을 때만 진행된다.
                if (quest.Key == "Mountain_Quest6")
                {
                    if (QuestModerRangedOnlyTracker.WasRangedOnly())
                        QuestManager.AddProgress(player, quest, 1);
                    continue;
                }
                // 일반 보스 처치 퀘스트("N회 처치")도 처치 시점마다 카운터를 1 증가시킨다.
                QuestManager.AddProgress(player, quest, 1);
            }
            if (prefabName == "Eikthyr")
                QuestEikthyrFlawlessTracker.ResetEncounter();
            if (prefabName == "gd_king")
                QuestGdKingMeleeOnlyTracker.ResetEncounter();
            if (prefabName == "Bonemass")
                QuestBonemassMagicOnlyTracker.ResetEncounter();
            if (prefabName == "Dragon")
            {
                QuestModerMeleeOnlyTracker.ResetEncounter();
                QuestModerRangedOnlyTracker.ResetEncounter();
            }

            // 트로피 채집 퀘스트(사슴 트로피, 회색난쟁이 트로피 등)는 전리품을 직접 주울 때까지
            // 기다리지 않고 Wood와 동일하게 처치 시점에 즉시 지급한다. 시체에서 트로피를 줍는
            // 행위는 QuestGatherOriginTracker가 인정하는 "직접 채집"(벌목/채광/Pickable)에
            // 포함되지 않아 일반 Gather 후킹만으로는 진행되지 않기 때문.
            var trophyQuest = QuestManager.FindByTarget(QuestType.Gather, "Trophy" + prefabName);
            if (trophyQuest != null)
            {
                QuestManager.AddProgress(player, trophyQuest, 1);
            }
        }
    }
}

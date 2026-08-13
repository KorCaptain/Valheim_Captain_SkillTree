using System;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// "방금 로컬 플레이어가 직접 채집 행위(벌목/채광/Pickable)를 했다"는 짧은 유효시간 플래그를 관리한다.
    /// QuestGatherPatch.cs의 Inventory.AddItem 후킹이 이 플래그가 살아있을 때만 진행도를 인정하도록 해서,
    /// 남이 떨어뜨린 아이템이나 남이 채집한 걸 줍는 경우는 퀘스트 진행에서 제외한다.
    /// 수량 계산은 하지 않는다(정확한 수량은 QuestGatherPatch의 Inventory.AddItem 후킹이 담당).
    /// </summary>
    public static class QuestGatherOriginTracker
    {
        // 나무를 벌목하고 흩어진 나무 조각까지 걸어가서 자동 습득하는 데 시간이 걸릴 수 있어 여유있게 설정.
        private const float GraceWindowSeconds = 6f;
        private static float _selfHarvestUntil = -999f;

        public static bool RecentlySelfHarvested => Time.time <= _selfHarvestUntil;

        private static void MarkSelfHarvest(string source)
        {
            _selfHarvestUntil = Time.time + GraceWindowSeconds;
            Plugin.Log.LogInfo($"[QuestGatherOriginTracker] 직접 채집 플래그 세팅: {source} (유효시간 {GraceWindowSeconds}초)");
        }

        // ===== 벌목 (SkillTree/ProductionEffects.cs와 동일한 판별 조건 재사용) =====
        //
        // 나무는 인벤토리로 자동 습득(걸어서 줍기)되는 타이밍이 들쭉날쭉해서 플래그+상관관계
        // 방식이 불안정했다. 생산 전문가 "초보 일꾼"/"벌목 Lv" 보너스 나무 지급 로직
        // (ProductionEffects.cs의 TryDropExtraWood)과 동일하게, 나무는 여기서 드롭테이블을
        // 직접 읽어 Inventory.AddItem을 기다리지 않고 그 자리에서 바로 진행도를 지급한다.

        public static void TryGrantWoodProgressDirectly(DropTable dropTable)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || dropTable?.m_drops == null || dropTable.m_drops.Count == 0) return;

                // 드롭테이블 항목 순서가 "Wood가 항상 0번"이라는 보장이 없어(m_drops[0]만 읽으면
                // 실제로는 낮은 확률/수량의 다른 항목을 잘못 집을 수 있음), 전체 항목을 훑어서
                // 활성 Gather 퀘스트 대상과 이름이 일치하는 항목만 찾아 그 수량을 지급한다.
                bool grantedAny = false;
                foreach (var drop in dropTable.m_drops)
                {
                    if (drop.m_item == null) continue;

                    string prefabName = Utils.GetPrefabName(drop.m_item);
                    if (string.IsNullOrEmpty(prefabName)) continue;

                    var quest = QuestManager.FindByTarget(QuestType.Gather, prefabName);
                    if (quest == null) continue;

                    int amount = Mathf.Max(1, Mathf.RoundToInt((drop.m_stackMin + drop.m_stackMax) / 2f));
                    Plugin.Log.LogInfo($"[QuestGatherOriginTracker] 벌목 직접 지급: {prefabName} +{amount} (대상퀘스트={quest.Key})");
                    QuestManager.AddProgress(player, quest, amount);
                    grantedAny = true;
                }

                if (!grantedAny)
                {
                    var sb = new System.Text.StringBuilder();
                    foreach (var d in dropTable.m_drops)
                    {
                        if (sb.Length > 0) sb.Append(", ");
                        sb.Append(d.m_item != null ? Utils.GetPrefabName(d.m_item) : "null");
                    }
                    Plugin.Log.LogInfo($"[QuestGatherOriginTracker] 벌목 드롭테이블에 활성 Gather 퀘스트 대상 없음 (드롭테이블 항목: {sb})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestGatherOriginTracker] TryGrantWoodProgressDirectly 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(TreeBase), "RPC_Damage")]
        public static class TreeBase_Damage_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(TreeBase __instance, HitData hit)
            {
                try
                {
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;

                    var nview = __instance.GetComponent<ZNetView>();
                    if (nview == null || !nview.IsValid()) return;
                    float health = nview.GetZDO().GetFloat(ZDOVars.s_health, __instance.m_health);
                    if (health > 0f) return; // 아직 파괴 안 됨

                    if (!Quest_Config.IsEnabled) return;
                    TryGrantWoodProgressDirectly(__instance.m_dropWhenDestroyed);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[QuestGatherOriginTracker] TreeBase 처리 오류: {ex.Message}");
                }
            }
        }

        [HarmonyPatch(typeof(TreeLog), nameof(TreeLog.Destroy))]
        public static class TreeLog_Destroy_Patch
        {
            [HarmonyPrefix]
            public static void Prefix(TreeLog __instance, HitData hitData)
            {
                try
                {
                    if (hitData?.GetAttacker() != Player.m_localPlayer) return;
                    if (!Quest_Config.IsEnabled) return;
                    TryGrantWoodProgressDirectly(__instance.m_dropWhenDestroyed);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[QuestGatherOriginTracker] TreeLog 처리 오류: {ex.Message}");
                }
            }
        }

        // ===== 채집 (Pickable) =====

        [HarmonyPatch(typeof(Pickable), nameof(Pickable.Interact))]
        public static class Pickable_Interact_Patch
        {
            [HarmonyPrefix]
            public static void Prefix(Pickable __instance, Humanoid character, out bool __state)
            {
                __state = (character == Player.m_localPlayer) && !__instance.GetPicked();
            }

            [HarmonyPostfix]
            public static void Postfix(bool __state)
            {
                if (__state) MarkSelfHarvest("Pickable.Interact");
            }
        }

        // ===== 채광 =====

        [HarmonyPatch(typeof(MineRock), "RPC_Hit")]
        public static class MineRock_Hit_Patch
        {
            // 주의: __instance.m_health는 채광 1회당 기본 데미지 상수일 뿐 실제 남은 체력이 아니라
            // 항상 >0이므로 파괴 여부 판별에 쓸 수 없다. 아이템은 어차피 부위 파괴 시에만 드롭되고
            // 진행도는 QuestGatherPatch(Inventory.AddItem)에서 별도로 세므로, 여기서는 Pickable과
            // 동일하게 "직접 공격했는가"만 확인한다.
            [HarmonyPostfix]
            public static void Postfix(HitData hit)
            {
                try
                {
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;
                    MarkSelfHarvest("MineRock.RPC_Hit");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[QuestGatherOriginTracker] MineRock 처리 오류: {ex.Message}");
                }
            }
        }

        [HarmonyPatch(typeof(MineRock5), "RPC_Damage")]
        public static class MineRock5_Damage_Patch
        {
            // 주의: __instance.m_health는 부위별 기본 체력 상수일 뿐 실제 남은 체력이 아니므로
            // 파괴 여부 판별에 쓸 수 없다. MineRock_Hit_Patch와 동일하게 공격자만 확인한다.
            [HarmonyPostfix]
            public static void Postfix(HitData hit)
            {
                try
                {
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;
                    MarkSelfHarvest("MineRock5.RPC_Damage");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[QuestGatherOriginTracker] MineRock5 처리 오류: {ex.Message}");
                }
            }
        }

        [HarmonyPatch(typeof(Destructible), "RPC_Damage")]
        public static class Destructible_Damage_Patch
        {
            // 주의: __instance.m_health는 기본 체력 상수일 뿐 실제 남은 체력이 아니므로
            // 파괴 여부 판별에 쓸 수 없다. MineRock_Hit_Patch와 동일하게 공격자만 확인한다.
            [HarmonyPostfix]
            public static void Postfix(HitData hit)
            {
                try
                {
                    if (hit?.GetAttacker() != Player.m_localPlayer) return;
                    MarkSelfHarvest("Destructible.RPC_Damage");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[QuestGatherOriginTracker] Destructible 처리 오류: {ex.Message}");
                }
            }
        }
    }
}

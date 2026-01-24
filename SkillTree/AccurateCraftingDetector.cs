using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 완전한 4단계 제작 감지 시스템
    /// 1. 인벤토리 열림 → 2. 제작 버튼 클릭 → 3. 재료 소모 감지 → 4. 새 아이템 인벤토리 진입
    /// </summary>
    public static class CraftButtonDetector
    {
        // 제작 버튼 클릭 상태 추적
        internal static bool craftButtonClicked = false;
        
        // 제작 버튼 클릭 시간 추적 (타임아웃 방지)
        internal static float craftButtonClickTime = 0f;
        
        // 재료 소모 감지 상태
        internal static bool materialConsumed = false;
        
        // 재료 소모 시간 추적
        internal static float materialConsumedTime = 0f;
        
        // 제작 전 인벤토리 아이템 개수 (새 아이템 감지용)
        internal static int inventoryCountBeforeCraft = 0;
        
        // 제작 유효 시간 (5초)
        private const float CRAFT_TIMEOUT_SECONDS = 5f;
        
        /// <summary>
        /// 완전한 4단계 제작 조건 확인
        /// </summary>
        internal static bool IsFullCraftingConditionsMet()
        {
            // 1단계: 인벤토리 열림 확인
            if (!InventoryGui.IsVisible())
            {
                Plugin.Log.LogDebug("[4단계 조건] 1단계 실패: 인벤토리가 열려있지 않음");
                return false;
            }
            
            // 2단계: 제작 버튼 클릭 확인
            if (!craftButtonClicked)
            {
                Plugin.Log.LogDebug("[4단계 조건] 2단계 실패: 제작 버튼이 클릭되지 않음");
                return false;
            }
            
            float timeSinceClick = Time.time - craftButtonClickTime;
            if (timeSinceClick > CRAFT_TIMEOUT_SECONDS)
            {
                // 타임아웃 - 자동 리셋
                ResetAllFlags();
                Plugin.Log.LogDebug($"[4단계 조건] 제작 버튼 클릭 타임아웃 ({timeSinceClick:F1}초) - 자동 리셋");
                return false;
            }
            
            // 3단계: 재료 소모 확인
            if (!materialConsumed)
            {
                Plugin.Log.LogDebug("[4단계 조건] 3단계 실패: 재료가 소모되지 않음");
                return false;
            }
            
            float timeSinceMaterialConsumed = Time.time - materialConsumedTime;
            if (timeSinceMaterialConsumed > CRAFT_TIMEOUT_SECONDS)
            {
                Plugin.Log.LogDebug($"[4단계 조건] 재료 소모 타임아웃 ({timeSinceMaterialConsumed:F1}초)");
                return false;
            }
            
            Plugin.Log.LogInfo("[4단계 조건] 1~3단계 모든 조건 충족됨");
            return true;
        }
        
        /// <summary>
        /// 모든 제작 플래그 리셋
        /// </summary>
        internal static void ResetAllFlags()
        {
            craftButtonClicked = false;
            craftButtonClickTime = 0f;
            materialConsumed = false;
            materialConsumedTime = 0f;
            inventoryCountBeforeCraft = 0;
            Plugin.Log.LogDebug("[플래그 리셋] 모든 제작 감지 플래그 리셋됨");
        }
    }
    
    /// <summary>
    /// 제작 버튼 클릭 감지 패치
    /// </summary>
    [HarmonyPatch(typeof(InventoryGui), "Awake")]
    public static class InventoryGui_CraftButton_Patch
    {
        private static void Postfix(InventoryGui __instance)
        {
            try
            {
                // 제작 버튼에 클릭 리스너 추가
                __instance.m_craftButton.onClick.AddListener(() => {
                    Plugin.Log.LogInfo("제작 버튼 클릭 감지!");
                    CraftButtonDetector.craftButtonClicked = true;
                    CraftButtonDetector.craftButtonClickTime = Time.time; // 클릭 시간 기록
                    
                    // 제작 전 인벤토리 아이템 개수 기록 (4단계 조건용)
                    var player = Player.m_localPlayer;
                    if (player?.GetInventory() != null)
                    {
                        CraftButtonDetector.inventoryCountBeforeCraft = player.GetInventory().GetAllItems().Count;
                        Plugin.Log.LogDebug($"[제작 버튼] 제작 전 인벤토리 아이템 개수: {CraftButtonDetector.inventoryCountBeforeCraft}");
                    }
                });
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"제작 버튼 리스너 등록 오류: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// 인벤토리 닫힘 시 안전장치 (제작 플래그 리셋)
    /// </summary>
    [HarmonyPatch(typeof(InventoryGui), "Hide")]
    public static class InventoryGui_Hide_SafetyReset
    {
        private static void Postfix()
        {
            if (CraftButtonDetector.craftButtonClicked || CraftButtonDetector.materialConsumed)
            {
                CraftButtonDetector.ResetAllFlags();
                Plugin.Log.LogDebug("인벤토리 닫힘으로 모든 제작 플래그 리셋");
            }
        }
    }
    
    /// <summary>
    /// 3단계: 재료 소모 감지 패치 (Player.ConsumeResources) - 정밀 컨텍스트 분석
    /// </summary>
    [HarmonyPatch(typeof(Player), "ConsumeResources")]
    public static class Player_ConsumeResources_MaterialDetector
    {
        private static void Postfix(Player __instance, Piece.Requirement[] requirements)
        {
            try
            {
                if (__instance != Player.m_localPlayer || requirements == null || requirements.Length == 0)
                {
                    return;
                }
                
                // 호출 컨텍스트 정밀 분석
                var callStack = new System.Diagnostics.StackTrace();
                string callerInfo = "";
                for (int i = 1; i < Math.Min(callStack.FrameCount, 5); i++)
                {
                    var frame = callStack.GetFrame(i);
                    if (frame?.GetMethod() != null)
                    {
                        callerInfo += $"{frame.GetMethod().DeclaringType?.Name}.{frame.GetMethod().Name} -> ";
                    }
                }
                
                Plugin.Log.LogInfo($"[제작 감지] ConsumeResources 호출 - 호출 스택: {callerInfo}");
                
                // 인벤토리 GUI 상태 확인
                bool inventoryOpen = InventoryGui.IsVisible();
                var inventoryGui = InventoryGui.instance;
                
                // InventoryGui 필드들 안전하게 확인 (reflection 사용)
                bool craftingPanelActive = false;
                string currentRecipe = "없음";
                
                try
                {
                    var craftingPanelField = typeof(InventoryGui).GetField("m_craftingPanel", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (craftingPanelField != null && inventoryGui != null)
                    {
                        var craftingPanel = craftingPanelField.GetValue(inventoryGui) as GameObject;
                        craftingPanelActive = craftingPanel?.activeSelf ?? false;
                    }
                    
                    var selectedRecipeField = typeof(InventoryGui).GetField("m_selectedRecipe", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (selectedRecipeField != null && inventoryGui != null)
                    {
                        var selectedRecipe = selectedRecipeField.GetValue(inventoryGui);
                        if (selectedRecipe != null)
                        {
                            var itemField = selectedRecipe.GetType().GetField("m_item", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                            if (itemField != null)
                            {
                                var item = itemField.GetValue(selectedRecipe);
                                var itemDataField = item?.GetType().GetField("m_itemData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                                if (itemDataField != null)
                                {
                                    var itemData = itemDataField.GetValue(item) as ItemDrop.ItemData;
                                    currentRecipe = itemData?.m_shared?.m_name ?? "파싱불가";
                                }
                            }
                        }
                    }
                }
                catch (Exception reflectionEx)
                {
                    Plugin.Log.LogDebug($"[제작 상태] Reflection 오류: {reflectionEx.Message}");
                }
                
                Plugin.Log.LogInfo($"[제작 상태] 인벤토리열림:{inventoryOpen}, 제작패널:{craftingPanelActive}, 현재레시피:{currentRecipe}");
                
                // 소모된 재료 로그
                string materialsInfo = "";
                foreach (var req in requirements)
                {
                    materialsInfo += $"{req.m_resItem?.m_itemData?.m_shared?.m_name ?? "알수없음"}({req.m_amount}), ";
                }
                Plugin.Log.LogInfo($"[소모 재료] {materialsInfo}");
                
                // 실제 제작 상황 판단 (DoCrafting 패치와 연동)
                bool isActualCrafting = CraftButtonDetector.materialConsumed && 
                                       CraftButtonDetector.craftButtonClicked &&
                                       callerInfo.Contains("DoCrafting");
                
                if (isActualCrafting)
                {
                    Plugin.Log.LogInfo($"[제작 감지] ✅ 실제 제작 상황 확인됨");
                    
                    // 제작된 아이템 찾기
                    var recentItem = GetRecentlyCraftedItem(__instance);
                    if (recentItem != null)
                    {
                        Plugin.Log.LogInfo($"[제작 완료] 제작된 아이템: {recentItem.m_shared?.m_name}");
                        
                        // 제작 강화 효과 적용
                        CraftingEnhancement.ApplyCraftingEnhancement(__instance, recentItem);
                        
                        Plugin.Log.LogInfo($"[제작 강화] 적용 완료: {recentItem.m_shared?.m_name}");
                        
                        // 제작 완료 후 플래그 리셋
                        CraftButtonDetector.ResetAllFlags();
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[제작 감지] 제작된 아이템을 찾을 수 없음");
                    }
                }
                else
                {
                    Plugin.Log.LogInfo($"[제작 감지] ❌ 제작이 아닌 상황 - materialConsumed:{CraftButtonDetector.materialConsumed}, craftButtonClicked:{CraftButtonDetector.craftButtonClicked}, DoCrafting호출:{callerInfo.Contains("DoCrafting")}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[제작 감지] ConsumeResources 패치 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 최근 제작된 아이템 찾기 (재료 소모 시점에서 추적)
        /// </summary>
        private static ItemDrop.ItemData GetRecentlyCraftedItem(Player player)
        {
            try
            {
                // ConsumeResources는 재료가 실제로 소모되는 시점이므로
                // 제작 버튼 클릭 후 가장 최근에 추가된 아이템을 찾는다
                var inventory = player.GetInventory();
                if (inventory?.GetAllItems() == null) return null;
                
                var allItems = inventory.GetAllItems();
                if (allItems.Count == 0) return null;
                
                // 가장 최근 추가된 아이템 반환 (제작으로 인한 것으로 가정)
                var recentItem = allItems[allItems.Count - 1];
                
                Plugin.Log.LogInfo($"[제작 아이템 찾기] 최근 아이템: {recentItem.m_shared?.m_name}");
                return recentItem;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[제작 아이템 찾기] 오류: {ex.Message}");
                return null;
            }
        }
    }
    
    /// <summary>
    /// 이중 검증: InventoryGui.DoCrafting 패치 (실제 제작 메서드 직접 감지)
    /// </summary>
    [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
    public static class InventoryGui_DoCrafting_DirectDetector
    {
        private static void Prefix(InventoryGui __instance, Player player)
        {
            try
            {
                if (player != Player.m_localPlayer) return;
                
                Plugin.Log.LogInfo("[DoCrafting] ✅ 실제 제작 메서드 직접 호출됨!");
                
                // DoCrafting 호출 플래그 설정 (ConsumeResources 검증용)
                CraftButtonDetector.materialConsumed = true;
                CraftButtonDetector.materialConsumedTime = Time.time;
                
                // 현재 선택된 레시피 로그 (reflection 사용)
                try
                {
                    var selectedRecipeField = typeof(InventoryGui).GetField("m_selectedRecipe", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (selectedRecipeField != null)
                    {
                        var recipe = selectedRecipeField.GetValue(__instance);
                        if (recipe != null)
                        {
                            var itemField = recipe.GetType().GetField("m_item", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                            if (itemField != null)
                            {
                                var item = itemField.GetValue(recipe);
                                var itemDataField = item?.GetType().GetField("m_itemData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                                if (itemDataField != null)
                                {
                                    var itemData = itemDataField.GetValue(item) as ItemDrop.ItemData;
                                    Plugin.Log.LogInfo($"[DoCrafting] 제작 중인 레시피: {itemData?.m_shared?.m_name ?? "알수없음"}");
                                }
                            }
                        }
                    }
                }
                catch (Exception reflectionEx)
                {
                    Plugin.Log.LogDebug($"[DoCrafting] 레시피 정보 추출 오류: {reflectionEx.Message}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[DoCrafting] 패치 오류: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// [비활성화] 기존 Inventory.AddItem 패치 - ConsumeResources 패치로 대체됨
    /// 실제 재료 소모 없이도 제작 효과가 발동되는 문제 해결을 위해 비활성화
    /// </summary>
    /*
    [HarmonyPatch(typeof(Inventory), "AddItem", new System.Type[] { typeof(ItemDrop.ItemData) })]
    public static class Inventory_AddItem_CraftEnhancement_DISABLED
    {
        // 이 패치는 재료 소모 없이도 효과가 발동되는 문제로 인해 비활성화됨
        // Player.ConsumeResources 패치로 대체됨
    }
    */
    
    /// <summary>
    /// [비활성화] 백업 제작 감지: Player.AddKnownItem 패치 
    /// 인벤토리 아이템 이동 시에도 제작 효과가 발동되는 문제로 인해 비활성화
    /// DoCrafting + ConsumeResources 조합만 사용
    /// </summary>
    /*
    [HarmonyPatch(typeof(Player), "AddKnownItem")]
    public static class Player_AddKnownItem_BackupCraftingDetector_DISABLED
    {
    // [비활성화] 백업 감지 시스템 전체 - 인벤토리 아이템 이동 시 오발동 방지
    */
}
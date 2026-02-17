using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 아이템 관리 및 인벤토리 확인/소모 로직
    /// </summary>
    public static class ItemManager
    {
        /// <summary>
        /// 플레이어가 특정 아이템 요구사항을 만족하는지 확인
        /// </summary>
        /// <param name="requirements">아이템 요구사항 목록</param>
        /// <param name="missingItems">부족한 아이템 목록 (출력)</param>
        /// <returns>모든 요구사항을 만족하면 true</returns>
        public static bool CanSatisfyRequirements(List<ItemRequirement> requirements, out List<ItemRequirement> missingItems)
        {
            missingItems = new List<ItemRequirement>();
            
            if (requirements == null || requirements.Count == 0)
                return true;

            var player = Player.m_localPlayer;
            if (player?.GetInventory() == null)
                return false;

            var inventory = player.GetInventory();

            foreach (var requirement in requirements)
            {
                if (!HasSufficientItem(inventory, requirement))
                {
                    missingItems.Add(requirement);
                }
            }

            return missingItems.Count == 0;
        }

        /// <summary>
        /// 인벤토리에서 특정 아이템이 충분한지 확인 (착용한 아이템 제외)
        /// </summary>
        private static bool HasSufficientItem(Inventory inventory, ItemRequirement requirement)
        {
            // 디버그 메시지 제거됨
            
            if (requirement is ItemEquipConsumeRequirement equipConsumeReq)
            {
                // 착용 후 소모 조건: 장착 중인지 확인
                bool result = IsItemEquipped(equipConsumeReq.ItemName);
                return result;
            }
            else if (requirement is ItemEquipRequirement equipReq)
            {
                // 장착 조건: 특정 장비를 착용 중인지 확인
                bool result = IsItemEquipped(equipReq.ItemName);
                // 디버그 메시지 제거됨
                return result;
            }
            else if (requirement is ItemQuantityRequirement quantityReq)
            {
                // 수량 조건: 인벤토리에 특정 수량 이상 보유 (착용한 것 제외)
                int count = CountUnequippedItems(inventory, quantityReq.ItemName);
                bool result = count >= quantityReq.Quantity;
                // 디버그 메시지 제거됨
                return result;
            }
            else
            {
                // 일반 조건: 필요 수량만큼 보유 (착용한 것 제외)
                int count = CountUnequippedItems(inventory, requirement.ItemName);
                bool result = count >= requirement.Quantity;
                // 디버그 메시지 제거됨
                return result;
            }
        }

        /// <summary>
        /// 착용하지 않은 아이템 개수 계산
        /// </summary>
        private static int CountUnequippedItems(Inventory inventory, string itemName)
        {
            if (inventory == null) return 0;

            int count = 0;
            // 디버그 메시지 제거됨
            
            foreach (var item in inventory.GetAllItems())
            {
                if (item != null && !item.m_equipped)
                {
                    string dropPrefabName = item.m_dropPrefab?.name;
                    string sharedName = item.m_shared?.m_name;
                    
                    // 디버그 메시지 제거됨
                    
                    // SkillTreeManager와 동일한 방식으로 매칭 (dropPrefab 우선)
                    if (dropPrefabName == itemName)
                    {
                        count += item.m_stack;
                        // 디버그 메시지 제거됨
                    }
                }
            }

            // 디버그 메시지 제거됨
            return count;
        }

        /// <summary>
        /// 특정 아이템이 착용되어 있는지 확인 (프리팹명 기준, 확장성 지원)
        /// </summary>
        private static bool IsItemEquipped(string itemName)
        {
            var player = Player.m_localPlayer;
            if (player == null) return false;

            var inventory = player.GetInventory();
            if (inventory == null) return false;

            // 인벤토리의 모든 아이템을 확인하여 장착된 것만 찾기
            var allItems = inventory.GetAllItems();
            foreach (var item in allItems)
            {
                if (item != null && item.m_equipped)
                {
                    string dropPrefabName = item.m_dropPrefab?.name;
                    string sharedName = item.m_shared?.m_name;
                    
                    // 1. 정확한 프리팹명 매칭 (기존 방식)
                    if (dropPrefabName == itemName)
                    {
                        // Plugin.Log.LogInfo($"[장비 확인] 착용 중인 아이템 발견 (정확 매칭): {itemName} -> {dropPrefabName}");
                        return true;
                    }
                    
                    // 2. 폴암 전문가 확장성: 프리팹명에 "Spear" 또는 "spear" 포함 시 모든 창 인식
                    if (IsPolearmSkill(itemName) && IsSpearWeapon(dropPrefabName))
                    {
                        Plugin.Log.LogInfo($"[장비 확인] 폴암 확장 매칭 발견: {itemName} -> {dropPrefabName}");
                        return true;
                    }
                    
                    // 3. 백업: 표시명으로 비교
                    if (sharedName == itemName)
                    {
                        Plugin.Log.LogInfo($"[장비 확인] 착용 중인 아이템 발견 (표시명): {itemName} -> {sharedName}");
                        return true;
                    }
                }
            }

            Plugin.Log.LogInfo($"[장비 확인] 착용 중이지 않음: {itemName}");
            return false;
        }

        /// <summary>
        /// 폴암 관련 스킬인지 확인
        /// </summary>
        private static bool IsPolearmSkill(string itemName)
        {
            // 폴암 전문가 스킬들의 요구 아이템인지 확인
            var polearmItems = new HashSet<string>
            {
                "SpearFlint", "SpearBronze", "SpearIron", 
                "SpearElderbark", "SpearWolfFang", "SpearCarapace"
            };
            return polearmItems.Contains(itemName);
        }

        /// <summary>
        /// 프리팹명이 창 무기인지 확인 (확장성 지원)
        /// </summary>
        private static bool IsSpearWeapon(string prefabName)
        {
            if (string.IsNullOrEmpty(prefabName)) return false;
            
            // 대소문자 구분없이 "Spear" 또는 "spear" 포함 확인
            // 다른 모드의 창 프리팹도 인식 (예: ModSpear, CustomSpearType 등)
            return prefabName.ToLower().Contains("spear");
        }

        /// <summary>
        /// 착용한 장비 아이템을 제거 (제작 스킬 언락 완료 시)
        /// </summary>
        /// <param name="requirements">장비 요구사항 목록</param>
        /// <returns>성공적으로 제거했으면 true</returns>
        public static bool RemoveEquippedItems(List<ItemRequirement> requirements)
        {
            if (requirements == null || requirements.Count == 0)
                return true;

            var player = Player.m_localPlayer;
            if (player?.GetInventory() == null)
                return false;

            var inventory = player.GetInventory();
            var removedItems = new List<string>();

            foreach (var requirement in requirements)
            {
                if (requirement is ItemEquipRequirement equipReq)
                {
                    // 착용한 아이템을 찾아서 제거
                    var equippedItem = FindEquippedItem(inventory, equipReq.ItemName);
                    if (equippedItem != null)
                    {
                        // 착용 해제 후 제거
                        player.UnequipItem(equippedItem);
                        inventory.RemoveItem(equippedItem);
                        removedItems.Add(equipReq.DisplayName);
                        
                        Plugin.Log.LogInfo($"[ItemManager] 착용한 장비 제거: {equipReq.DisplayName}");
                    }
                }
            }

            // 제거 완료 알림
            if (removedItems.Count > 0)
            {
                var itemText = string.Join(", ", removedItems);
                SkillEffect.DrawFloatingText(player, $"🔨 {L.Get("equipment_consumed", itemText)}", new Color(0.9f, 0.7f, 0.3f));
                Plugin.Log.LogInfo($"[ItemManager] 장비 제거 완료: {itemText}");
            }

            return true;
        }

        /// <summary>
        /// 착용한 특정 아이템을 찾기 (확장성 지원)
        /// </summary>
        private static ItemDrop.ItemData FindEquippedItem(Inventory inventory, string itemName)
        {
            if (inventory == null) return null;

            foreach (var item in inventory.GetAllItems())
            {
                if (item != null && item.m_equipped)
                {
                    string dropPrefabName = item.m_dropPrefab?.name;
                    
                    // 1. 정확한 프리팹명 매칭
                    if (dropPrefabName == itemName)
                    {
                        return item;
                    }
                    
                    // 2. 폴암 전문가 확장성: 프리팹명에 "Spear" 포함 시 창 인식
                    if (IsPolearmSkill(itemName) && IsSpearWeapon(dropPrefabName))
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 아이템 요구사항에 따라 실제로 아이템을 소모
        /// </summary>
        /// <param name="requirements">소모할 아이템 목록</param>
        /// <returns>성공적으로 소모했으면 true</returns>
        public static bool ConsumeItems(List<ItemRequirement> requirements)
        {
            // 디버그 메시지 제거됨
            
            if (requirements == null || requirements.Count == 0)
                return true;

            var player = Player.m_localPlayer;
            if (player?.GetInventory() == null)
            {
                // 디버그 메시지 제거됨
                return false;
            }

            var inventory = player.GetInventory();

            // 먼저 모든 아이템이 충분한지 다시 한번 확인
            // 디버그 메시지 제거됨
            if (!CanSatisfyRequirements(requirements, out var missingItems))
            {
                Plugin.Log.LogWarning($"[ItemManager] 아이템 소모 실패 - 부족한 아이템: {string.Join(", ", missingItems.Select(x => x.ToString()))}");
                // 디버그 메시지 제거됨
                return false;
            }
            // 디버그 메시지 제거됨

            // 실제 아이템 소모
            foreach (var requirement in requirements)
            {
                if (requirement is ItemEquipConsumeRequirement equipConsumeReq)
                {
                    // 착용한 장비를 찾아서 제거
                    var equippedItem = FindEquippedItem(inventory, equipConsumeReq.ItemName);
                    if (equippedItem != null)
                    {
                        // 착용 해제 후 제거
                        var localPlayer = Player.m_localPlayer;
                        localPlayer?.UnequipItem(equippedItem);
                        inventory.RemoveItem(equippedItem);
                        
                        Plugin.Log.LogInfo($"[ItemManager] 착용 장비 소모: {equipConsumeReq.DisplayName}");
                    }
                }
                else if (requirement.IsConsumed && 
                    !(requirement is ItemQuantityRequirement) && 
                    !(requirement is ItemEquipRequirement))
                {
                    // 소모되는 아이템만 제거 (수량 조건과 장착 조건은 제외)
                    var initialCount = inventory.CountItems(requirement.ItemName);
                    inventory.RemoveItem(requirement.ItemName, requirement.Quantity);
                    var remainingCount = inventory.CountItems(requirement.ItemName);
                    
                    if (initialCount - remainingCount < requirement.Quantity)
                    {
                        Plugin.Log.LogError($"[ItemManager] 아이템 제거 실패: {requirement.ItemName} x{requirement.Quantity}");
                        return false;
                    }
                    
                    Plugin.Log.LogInfo($"[ItemManager] 아이템 소모: {requirement.DisplayName} x{requirement.Quantity}");
                }
            }

            return true;
        }

        /// <summary>
        /// 아이템 요구사항을 문자열로 변환 (UI 표시용)
        /// </summary>
        public static string GetRequirementsText(List<ItemRequirement> requirements)
        {
            if (requirements == null || requirements.Count == 0)
                return "필요 재료 없음";

            var player = Player.m_localPlayer;
            var inventory = player?.GetInventory();
            var requirementTexts = new List<string>();

            foreach (var requirement in requirements)
            {
                var hasEnough = inventory != null && HasSufficientItem(inventory, requirement);
                var colorTag = hasEnough ? "<color=#00FF00>" : "<color=#FF0000>";
                var statusText = hasEnough ? "[O]" : "[X]";
                
                if (requirement is ItemEquipConsumeRequirement)
                {
                    requirementTexts.Add($"{colorTag}{statusText} {requirement}</color>");
                }
                else if (requirement is ItemEquipRequirement)
                {
                    requirementTexts.Add($"{colorTag}{statusText} {requirement}</color>");
                }
                else if (requirement is ItemQuantityRequirement)
                {
                    requirementTexts.Add($"{colorTag}{statusText} {requirement}</color>");
                }
                else
                {
                    // requirement.ToString()이 이미 (소모) 포함하므로 중복 제거
                    requirementTexts.Add($"{colorTag}{statusText} {requirement}</color>");
                }
            }

            return string.Join("\n", requirementTexts);
        }

        /// <summary>
        /// 스킬 언락 가능 여부 확인 (전체)
        /// </summary>
        /// <param name="skillId">스킬 ID</param>
        /// <param name="missingItems">부족한 아이템 목록</param>
        /// <returns>언락 가능하면 true</returns>
        public static bool CanUnlockSkill(string skillId, out List<ItemRequirement> missingItems)
        {
            var requirements = SkillItemRequirements.GetRequirements(skillId);
            return CanSatisfyRequirements(requirements, out missingItems);
        }

        /// <summary>
        /// 스킬 언락 시 아이템 소모
        /// </summary>
        /// <param name="skillId">스킬 ID</param>
        /// <returns>성공적으로 소모했으면 true</returns>
        public static bool UnlockSkillWithItems(string skillId)
        {
            var requirements = SkillItemRequirements.GetRequirements(skillId);
            
            if (ConsumeItems(requirements))
            {
                // 소모 성공 시 플레이어에게 알림
                var player = Player.m_localPlayer;
                if (player != null)
                {
                    var consumedItems = requirements.Where(r => r.IsConsumed && 
                        !(r is ItemQuantityRequirement) && 
                        !(r is ItemEquipRequirement)).ToList();
                    if (consumedItems.Count > 0)
                    {
                        var itemTexts = consumedItems.Select(x => $"{x.DisplayName} x{x.Quantity}").ToList();
                        SkillEffect.DrawFloatingText(player, $"🔨 {L.Get("material_consumed", string.Join(", ", itemTexts))}");
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// 특정 스킬의 아이템 요구사항 텍스트 반환
        /// </summary>
        public static string GetSkillRequirementsText(string skillId)
        {
            var requirements = SkillItemRequirements.GetRequirements(skillId);
            return GetRequirementsText(requirements);
        }
    }
}
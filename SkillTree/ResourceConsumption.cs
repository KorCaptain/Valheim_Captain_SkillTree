using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 자원 소모 시스템 - 생산 전문가 스킬 학습 시 필요한 재료를 실제로 소모
    /// </summary>
    public static class ResourceConsumption
    {
        /// <summary>
        /// 스킬 학습 시 자원 소모 처리
        /// </summary>
        /// <param name="player">플레이어</param>
        /// <param name="skillId">스킬 ID</param>
        /// <returns>소모 성공 시 true</returns>
        public static bool ConsumeResourcesForSkill(Player player, string skillId)
        {
            if (player?.GetInventory() == null) return false;

            try
            {
                var inventory = player.GetInventory();
                var consumedItems = new List<string>();

                // 1. 아이템 매니저를 통한 재료 소모 (기존 시스템)
                if (SkillItemRequirements.HasRequirements(skillId))
                {
                    var requirements = SkillItemRequirements.GetRequirements(skillId);
                    var consumableRequirements = requirements.Where(r => r.IsConsumed).ToList();
                    var equipRequirements = requirements.Where(r => r is ItemEquipRequirement).ToList();
                    
                    // 일반 소모 아이템 처리
                    if (consumableRequirements.Count > 0)
                    {
                        if (!ItemManager.ConsumeItems(consumableRequirements))
                        {
                            Plugin.Log.LogWarning($"[자원 소모] 아이템 소모 실패: {skillId}");
                            return false;
                        }

                        consumedItems.AddRange(consumableRequirements.Select(r => $"{r.DisplayName} x{r.Quantity}"));
                    }
                    
                    // 제작 스킬의 착용 장비 제거 처리
                    if (IsProductionCraftingSkill(skillId) && equipRequirements.Count > 0)
                    {
                        if (!ItemManager.RemoveEquippedItems(equipRequirements))
                        {
                            Plugin.Log.LogWarning($"[자원 소모] 착용 장비 제거 실패: {skillId}");
                            return false;
                        }

                        consumedItems.AddRange(equipRequirements.Select(r => $"{r.DisplayName} (착용 소모)"));
                    }
                }

                // 2. 자원 기반 스킬의 특별 처리 (벌목, 채집, 채광)
                if (ResourceValidator.HasResourceRequirements(skillId))
                {
                    if (!ConsumeResourceRequirements(player, skillId, consumedItems))
                    {
                        return false;
                    }
                }

                // 3. 소모 완료 알림
                if (consumedItems.Count > 0)
                {
                    var itemText = string.Join(", ", consumedItems);
                    SkillEffect.DrawFloatingText(player, $"🔨 {L.Get("material_consumed", itemText)}");
                    Plugin.Log.LogInfo($"[자원 소모] 스킬 학습 완료: {skillId} - 소모된 재료: {itemText}");
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[자원 소모] 오류 ({skillId}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 자원 기반 스킬의 재료 소모 처리
        /// </summary>
        private static bool ConsumeResourceRequirements(Player player, string skillId, List<string> consumedItems)
        {
            var inventory = player.GetInventory();
            var resourceRequirements = ResourceValidator.GetResourceRequirements(skillId);

            // 자원 기반 스킬은 보유 조건만 확인하고 실제로는 소모하지 않음
            // 하지만 특정 스킬은 일부 재료를 소모할 수 있음
            
            switch (skillId)
            {
                case "woodcutting_lv3":
                case "woodcutting_lv4":
                    // 벌목 스킬: 나무는 소모하지 않고 보유 조건만 확인
                    Plugin.Log.LogInfo($"[자원 소모] {skillId}: 나무 보유량 확인 완료 (소모하지 않음)");
                    break;

                case "gathering_lv3":
                    // 채집 스킬: 채집물은 소모하지 않고 보유 조건만 확인
                    Plugin.Log.LogInfo($"[자원 소모] {skillId}: 채집물 보유량 확인 완료 (소모하지 않음)");
                    break;

                case "mining_lv2":
                    // 채광 Lv2: 구리 20개 전부 소모
                    if (ConsumeSpecificResource(inventory, "Copper", "구리", 20, consumedItems))
                    {
                        Plugin.Log.LogInfo($"[자원 소모] {skillId}: 구리 20개 소모");
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case "mining_lv3":
                    // 채광 Lv3: 철 30개 전부 소모
                    if (ConsumeSpecificResource(inventory, "Iron", "철", 30, consumedItems))
                    {
                        Plugin.Log.LogInfo($"[자원 소모] {skillId}: 철 30개 소모");
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case "mining_lv4":
                    // 채광 Lv4: 은 25개 전부 소모
                    if (ConsumeSpecificResource(inventory, "Silver", "은", 25, consumedItems))
                    {
                        Plugin.Log.LogInfo($"[자원 소모] {skillId}: 은 25개 소모");
                    }
                    else
                    {
                        return false;
                    }
                    break;

                default:
                    // 기타 자원 기반 스킬은 보유 조건만 확인
                    Plugin.Log.LogInfo($"[자원 소모] {skillId}: 자원 보유 조건만 확인 (소모하지 않음)");
                    break;
            }

            return true;
        }

        /// <summary>
        /// 특정 자원을 지정된 수량만큼 소모
        /// </summary>
        private static bool ConsumeSpecificResource(Inventory inventory, string itemName, string displayName, int quantity, List<string> consumedItems)
        {
            int currentCount = inventory.CountItems(itemName);
            if (currentCount < quantity)
            {
                Plugin.Log.LogWarning($"[자원 소모] {displayName} 부족: {currentCount}/{quantity}");
                return false;
            }

            // 실제 아이템 제거
            inventory.RemoveItem(itemName, quantity);
            
            // 제거 확인
            int remainingCount = inventory.CountItems(itemName);
            int actualRemoved = currentCount - remainingCount;
            
            if (actualRemoved >= quantity)
            {
                consumedItems.Add($"{displayName} x{actualRemoved}");
                return true;
            }
            else
            {
                Plugin.Log.LogError($"[자원 소모] {displayName} 제거 실패: 요청 {quantity}개, 실제 제거 {actualRemoved}개");
                return false;
            }
        }

        /// <summary>
        /// 스킬의 총 소모 비용을 문자열로 반환 (UI 표시용)
        /// </summary>
        public static string GetConsumptionCostText(string skillId)
        {
            var costTexts = new List<string>();

            // 1. 일반 아이템 소모 비용
            if (SkillItemRequirements.HasRequirements(skillId))
            {
                var requirements = SkillItemRequirements.GetRequirements(skillId);
                var consumableRequirements = requirements.Where(r => r.IsConsumed).ToList();
                
                foreach (var req in consumableRequirements)
                {
                    costTexts.Add($"🔸 {req.DisplayName} x{req.Quantity} (소모)");
                }
            }

            // 2. 자원 기반 특별 소모 비용
            switch (skillId)
            {
                case "mining_lv2":
                    costTexts.Add("🔸 구리 x20 (소모)");
                    break;
                case "mining_lv3":
                    costTexts.Add("🔸 철 x30 (소모)");
                    break;
                case "mining_lv4":
                    costTexts.Add("🔸 은 x25 (소모)");
                    break;
            }

            // 3. 자원 보유 조건
            if (ResourceValidator.HasResourceRequirements(skillId))
            {
                var resourceText = ResourceValidator.GetResourceRequirementsText(skillId);
                if (!string.IsNullOrEmpty(resourceText))
                {
                    var lines = resourceText.Split('\n');
                    foreach (var line in lines)
                    {
                        if (!string.IsNullOrEmpty(line.Trim()))
                        {
                            // 이미 소모 비용에 포함된 것은 제외
                            if ((skillId == "mining_lv2" && line.Contains("구리")) ||
                                (skillId == "mining_lv3" && line.Contains("철")) ||
                                (skillId == "mining_lv4" && line.Contains("은")))
                            {
                                continue;
                            }
                            costTexts.Add($"🔹 {line.Replace("[O]", "").Replace("[X]", "").Trim()} (보유 필요)");
                        }
                    }
                }
            }

            if (costTexts.Count == 0)
            {
                return "추가 비용 없음";
            }

            return string.Join("\n", costTexts);
        }

        /// <summary>
        /// 플레이어가 스킬 학습 비용을 감당할 수 있는지 확인
        /// </summary>
        public static bool CanAffordSkill(Player player, string skillId, out string missingItems)
        {
            missingItems = "";
            var missingList = new List<string>();

            if (player?.GetInventory() == null)
            {
                missingItems = "인벤토리 정보 없음";
                return false;
            }

            // 1. 일반 아이템 요구사항 확인
            if (SkillItemRequirements.HasRequirements(skillId))
            {
                if (!ItemManager.CanUnlockSkill(skillId, out var missingItemReqs))
                {
                    missingList.AddRange(missingItemReqs.Select(r => $"{r.DisplayName} x{r.Quantity}"));
                }
            }

            // 2. 자원 요구사항 확인
            if (ResourceValidator.HasResourceRequirements(skillId))
            {
                var resourceResult = ResourceValidator.ValidateResourceRequirements(player, skillId);
                if (!resourceResult.IsValid)
                {
                    missingList.AddRange(resourceResult.MissingResources.Select(r => $"{r.DisplayName} x{r.RequiredQuantity}"));
                }
            }

            // 3. 특별 소모 비용 확인
            var inventory = player.GetInventory();
            switch (skillId)
            {
                case "mining_lv2":
                    if (inventory.CountItems("Copper") < 20)
                    {
                        missingList.Add($"구리 x{20 - inventory.CountItems("Copper")} (소모용)");
                    }
                    break;
                case "mining_lv3":
                    if (inventory.CountItems("Iron") < 30)
                    {
                        missingList.Add($"철 x{30 - inventory.CountItems("Iron")} (소모용)");
                    }
                    break;
                case "mining_lv4":
                    if (inventory.CountItems("Silver") < 25)
                    {
                        missingList.Add($"은 x{25 - inventory.CountItems("Silver")} (소모용)");
                    }
                    break;
            }

            if (missingList.Count > 0)
            {
                missingItems = string.Join(", ", missingList);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 제작 관련 생산 스킬인지 확인
        /// </summary>
        private static bool IsProductionCraftingSkill(string skillId)
        {
            return skillId == "crafting_lv2" || skillId == "crafting_lv3" || skillId == "crafting_lv4";
        }
    }
}
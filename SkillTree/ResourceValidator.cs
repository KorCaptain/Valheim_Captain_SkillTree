using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 자원 조건 검증 시스템 - 나무, 광석, 채집물 등의 보유량 확인
    /// </summary>
    public static class ResourceValidator
    {
        /// <summary>
        /// 자원 기반 스킬 조건 매핑
        /// </summary>
        private static readonly Dictionary<string, List<ResourceRequirement>> ResourceRequirements = new Dictionary<string, List<ResourceRequirement>>
        {
            // 벌목 Lv3: 나무 200개 보유
            ["woodcutting_lv3"] = new List<ResourceRequirement>
            {
                new ResourceRequirement("Wood", "나무", 200)
            },
            
            // 벌목 Lv4: 나무 400개 보유
            ["woodcutting_lv4"] = new List<ResourceRequirement>
            {
                new ResourceRequirement("Wood", "나무", 400)
            },
            
            // 채집 Lv3: 채집류 100개 보유 (베리 + 버섯 합계)
            ["gathering_lv3"] = new List<ResourceRequirement>
            {
                new ResourceRequirement("Raspberry", "산딸기", 50),
                new ResourceRequirement("Mushroom", "버섯", 50)
            },

            // 채광 Lv2: 구리 20개 소모
            ["mining_lv2"] = new List<ResourceRequirement>
            {
                new ResourceRequirement("Copper", "구리", 20)
            },
            
            // 채광 Lv3: 철 30개 소모
            ["mining_lv3"] = new List<ResourceRequirement>
            {
                new ResourceRequirement("Iron", "철", 30)
            },
            
            // 채광 Lv4: 은 25개 소모
            ["mining_lv4"] = new List<ResourceRequirement>
            {
                new ResourceRequirement("Silver", "은", 25)
            }
        };

        /// <summary>
        /// 자원 요구사항 클래스
        /// </summary>
        public class ResourceRequirement
        {
            public string ItemName { get; set; }
            public string DisplayName { get; set; }
            public int RequiredQuantity { get; set; }

            public ResourceRequirement(string itemName, string displayName, int requiredQuantity)
            {
                ItemName = itemName;
                DisplayName = displayName;
                RequiredQuantity = requiredQuantity;
            }

            public override string ToString()
            {
                return $"{DisplayName} {RequiredQuantity}개";
            }
        }

        /// <summary>
        /// 자원 검증 결과 클래스
        /// </summary>
        public class ResourceValidationResult
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
            public List<ResourceRequirement> MissingResources { get; set; } = new List<ResourceRequirement>();

            public ResourceValidationResult(bool isValid, string message)
            {
                IsValid = isValid;
                Message = message;
            }
        }

        /// <summary>
        /// 스킬에 자원 조건이 있는지 확인
        /// </summary>
        public static bool HasResourceRequirements(string skillId)
        {
            return ResourceRequirements.ContainsKey(skillId);
        }

        /// <summary>
        /// 플레이어가 특정 스킬의 자원 조건을 만족하는지 확인
        /// </summary>
        public static ResourceValidationResult ValidateResourceRequirements(Player player, string skillId)
        {
            if (!HasResourceRequirements(skillId))
            {
                return new ResourceValidationResult(true, "자원 조건 없음");
            }

            if (player?.GetInventory() == null)
            {
                return new ResourceValidationResult(false, "플레이어 인벤토리를 찾을 수 없습니다.");
            }

            var requirements = ResourceRequirements[skillId];
            var inventory = player.GetInventory();
            var missingResources = new List<ResourceRequirement>();

            foreach (var requirement in requirements)
            {
                int currentCount = inventory.CountItems(requirement.ItemName);
                if (currentCount < requirement.RequiredQuantity)
                {
                    missingResources.Add(new ResourceRequirement(
                        requirement.ItemName,
                        requirement.DisplayName,
                        requirement.RequiredQuantity - currentCount
                    ));
                }
            }

            if (missingResources.Count > 0)
            {
                var missingText = string.Join(", ", missingResources.Select(r => $"{r.DisplayName} {r.RequiredQuantity}개"));
                return new ResourceValidationResult(false, $"부족한 자원: {missingText}")
                {
                    MissingResources = missingResources
                };
            }

            return new ResourceValidationResult(true, "자원 조건 만족");
        }

        /// <summary>
        /// 스킬의 자원 요구사항을 UI용 텍스트로 변환
        /// </summary>
        public static string GetResourceRequirementsText(string skillId)
        {
            if (!HasResourceRequirements(skillId))
            {
                return "";
            }

            var player = Player.m_localPlayer;
            if (player?.GetInventory() == null)
            {
                return "인벤토리 정보 없음";
            }

            var requirements = ResourceRequirements[skillId];
            var inventory = player.GetInventory();
            var requirementTexts = new List<string>();

            foreach (var requirement in requirements)
            {
                int currentCount = inventory.CountItems(requirement.ItemName);
                bool hasEnough = currentCount >= requirement.RequiredQuantity;
                
                var colorTag = hasEnough ? "<color=#00FF00>" : "<color=#FF0000>";
                var statusText = hasEnough ? "[O]" : "[X]";
                
                requirementTexts.Add($"{colorTag}{statusText} {requirement.DisplayName} {currentCount}/{requirement.RequiredQuantity}</color>");
            }

            return string.Join("\n", requirementTexts);
        }

        /// <summary>
        /// 특정 스킬의 자원 요구사항 목록 반환
        /// </summary>
        public static List<ResourceRequirement> GetResourceRequirements(string skillId)
        {
            return HasResourceRequirements(skillId) ? ResourceRequirements[skillId] : new List<ResourceRequirement>();
        }

        /// <summary>
        /// 플레이어의 자원 보유량 확인
        /// </summary>
        public static int GetResourceCount(Player player, string itemName)
        {
            if (player?.GetInventory() == null) return 0;
            return player.GetInventory().CountItems(itemName);
        }

        /// <summary>
        /// 자원 기반 스킬 목록 반환
        /// </summary>
        public static IEnumerable<string> GetResourceBasedSkills()
        {
            return ResourceRequirements.Keys;
        }
    }
}
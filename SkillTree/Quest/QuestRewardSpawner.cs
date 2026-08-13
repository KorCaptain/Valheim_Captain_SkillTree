using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>퀘스트 보상 실물 지급(아이템/코인 드롭, 특수 보상)을 담당하는 헬퍼.</summary>
    public static class QuestRewardSpawner
    {
        /// <summary>
        /// prefabName 아이템을 amount만큼 캐릭터 앞에 드롭한다. 아이템 최대 스택 크기를 넘으면
        /// 여러 더미로 나눠 드롭한다(SkillTree/ProductionEffects.cs의 드롭 패턴 재사용).
        /// </summary>
        public static void SpawnStackedItem(string prefabName, int amount, Vector3 basePosition)
        {
            if (string.IsNullOrEmpty(prefabName) || amount <= 0) return;

            var prefab = ZNetScene.instance?.GetPrefab(prefabName);
            if (prefab == null)
            {
                Plugin.Log.LogWarning($"[Quest] 보상 프리팹을 찾을 수 없음: {prefabName}");
                return;
            }

            var template = prefab.GetComponent<ItemDrop>();
            if (template == null)
            {
                Plugin.Log.LogWarning($"[Quest] ItemDrop 컴포넌트가 없는 프리팹: {prefabName}");
                return;
            }

            int maxStack = Mathf.Max(1, template.m_itemData.m_shared.m_maxStackSize);
            int remaining = amount;
            int stackIndex = 0;

            while (remaining > 0)
            {
                int stackAmount = Mathf.Min(remaining, maxStack);
                remaining -= stackAmount;

                Vector3 offset = UnityEngine.Random.insideUnitSphere * (0.6f + stackIndex * 0.2f);
                offset.y = 0f;
                Vector3 finalPos = basePosition + offset;

                var dropped = Object.Instantiate(prefab, finalPos, Quaternion.identity);
                var itemDrop = dropped.GetComponent<ItemDrop>();
                if (itemDrop != null)
                {
                    itemDrop.m_itemData.m_stack = stackAmount;
                }

                stackIndex++;
            }

            Plugin.Log.LogInfo($"[Quest] 보상 지급: {prefabName} x{amount} ({stackIndex}개 더미로 드롭)");
        }

        /// <summary>
        /// 코인 보상 전용 지급. 캐릭터 앞 월드 드롭(ItemDrop+ZNetView Instantiate)은
        /// 오브젝트 수가 많아지면 서버 렉을 유발할 수 있어, 인벤토리에 자리가 있으면
        /// 바로 인벤토리로 지급하고 자리가 없을 때만 캐릭터 앞에 드롭한다.
        /// </summary>
        public static void GrantCoins(Player player, int amount, Vector3 fallbackDropPosition)
        {
            if (player == null || amount <= 0) return;

            var prefab = ZNetScene.instance?.GetPrefab("Coins");
            if (prefab == null)
            {
                Plugin.Log.LogWarning("[Quest] Coins 프리팹을 찾을 수 없음");
                return;
            }

            var inventory = player.GetInventory();
            if (inventory != null && inventory.CanAddItem(prefab, amount))
            {
                var template = prefab.GetComponent<ItemDrop>();
                int maxStack = template != null ? Mathf.Max(1, template.m_itemData.m_shared.m_maxStackSize) : amount;
                int remaining = amount;
                while (remaining > 0)
                {
                    int chunk = Mathf.Min(remaining, maxStack);
                    inventory.AddItem(prefab, chunk);
                    remaining -= chunk;
                }
                Plugin.Log.LogInfo($"[Quest] 코인 보상 {amount}개 인벤토리에 즉시 지급");
            }
            else
            {
                // 인벤토리 공간 부족 - 기존처럼 캐릭터 앞에 드롭
                SpawnStackedItem("Coins", amount, fallbackDropPosition);
                Plugin.Log.LogInfo($"[Quest] 인벤토리 공간 부족으로 코인 {amount}개를 캐릭터 앞에 드롭");
            }
        }

        /// <summary>SpecialReward 문자열("TameLoxCalf" / "JumpProficiency:10")을 파싱해 처리한다.</summary>
        public static void GrantSpecialReward(Player player, string specialReward, Vector3 dropPosition)
        {
            if (string.IsNullOrEmpty(specialReward)) return;

            if (specialReward == "TameLoxCalf")
            {
                SpawnTamedAnimal(player, dropPosition, "Lox_Calf");
                return;
            }

            if (specialReward == "TameWolfCub")
            {
                SpawnTamedAnimal(player, dropPosition, "Wolf_cub");
                return;
            }

            if (specialReward.StartsWith("JumpProficiency"))
            {
                // 실제 보너스 적용은 SkillEffect.SpeedTree2.cs의 GetSkillLevelBonus가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다(Claimed 플래그만으로 충분).
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_jump_proficiency")}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("StaminaBonus"))
            {
                // 실제 보너스 적용은 SkillEffect.StatTree.cs의 스태미나 GetTotalFoodValue 패치가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다.
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_stamina_bonus")}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("ElementalDamage"))
            {
                // 실제 보너스 적용은 SkillEffect.AttackTree.cs의 Character.Damage 패치가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다.
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_elemental_damage")}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("PhysicalDamage"))
            {
                // 실제 보너스 적용은 SkillEffect.AttackTree.cs의 Character.Damage 패치가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다.
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_physical_damage")}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("FarmingSkill"))
            {
                // 실제 보너스 적용은 SkillEffect.SpeedTree2.cs의 GetSkillLevelBonus가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다(Claimed 플래그만으로 충분).
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_farming_skill", ParseSkillAmount(specialReward))}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("CookingSkill"))
            {
                // 실제 보너스 적용은 SkillEffect.SpeedTree2.cs의 GetSkillLevelBonus가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다(Claimed 플래그만으로 충분).
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_cooking_skill", ParseSkillAmount(specialReward))}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("RandomItem:"))
            {
                var options = specialReward.Substring("RandomItem:".Length).Split(',');
                if (options.Length > 0)
                {
                    string chosen = options[UnityEngine.Random.Range(0, options.Length)];
                    SpawnStackedItem(chosen, 1, dropPosition);
                }
                return;
            }

            if (specialReward.StartsWith("FishingSkill"))
            {
                // 실제 보너스 적용은 SkillEffect.SpeedTree2.cs의 GetSkillLevelBonus가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다(Claimed 플래그만으로 충분).
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_fishing_skill", ParseSkillAmount(specialReward))}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("MeleeWeaponSkill"))
            {
                // 실제 보너스 적용은 SkillEffect.SpeedTree2.cs의 GetSkillLevelBonus가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다(Claimed 플래그만으로 충분).
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_melee_weapon_skill", ParseSkillAmount(specialReward))}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            if (specialReward.StartsWith("RangedWeaponSkill"))
            {
                // 실제 보너스 적용은 SkillEffect.SpeedTree2.cs의 GetSkillLevelBonus가
                // QuestManager.IsClaimed(player, questKey)를 조회해 처리한다(Claimed 플래그만으로 충분).
                SkillEffect.DrawFloatingText(player, $"⬆ {L.Get("quest_reward_ranged_weapon_skill", ParseSkillAmount(specialReward))}", new Color(0.6f, 0.9f, 1f, 1f));
                return;
            }

            Plugin.Log.LogWarning($"[Quest] 알 수 없는 SpecialReward: {specialReward}");
        }

        /// <summary>"FarmingSkill:2" 같은 SpecialReward 문자열에서 ':' 뒤의 수치만 파싱한다(표시용).</summary>
        private static float ParseSkillAmount(string specialReward)
        {
            var parts = specialReward.Split(':');
            return parts.Length > 1 && float.TryParse(parts[1], out var v) ? v : 1f;
        }

        private static void SpawnTamedAnimal(Player player, Vector3 dropPosition, string prefabName)
        {
            var prefab = ZNetScene.instance?.GetPrefab(prefabName);
            if (prefab == null)
            {
                Plugin.Log.LogWarning($"[Quest] {prefabName} 프리팹을 찾을 수 없음");
                return;
            }

            var spawned = Object.Instantiate(prefab, dropPosition, Quaternion.identity);
            var character = spawned.GetComponent<Character>();
            if (character != null)
            {
                character.SetTamed(true);
            }
            else
            {
                Plugin.Log.LogWarning($"[Quest] {prefabName}에 Character 컴포넌트가 없음 - 테이밍 실패");
            }
        }
    }
}

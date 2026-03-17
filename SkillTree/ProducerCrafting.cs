using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가(Producer) 제작 관련 패치
    /// - 내구도 보너스 (Lv2+)
    /// - 재료 감소 (Lv2+)
    /// - 제작 마법부여 (Lv3+)
    /// </summary>
    public static class ProducerCrafting
    {
        // 마지막으로 제작된 아이템 ZDO 저장 (인챈트 ZDO 키 프리픽스)
        private const string ENCHANT_TYPE_KEY  = "cspt_enchant_type";
        private const string ENCHANT_VALUE_KEY = "cspt_enchant_value";

        public enum EnchantType { None = 0, WeaponDmg = 1, Armor = 2, MaxHP = 3 }

        /// <summary>
        /// 제작된 아이템 마법부여 타입 조회 (외부 툴팁용)
        /// </summary>
        public static EnchantType GetEnchantType(ItemDrop.ItemData item)
        {
            if (item?.m_customData == null) return EnchantType.None;
            if (item.m_customData.TryGetValue(ENCHANT_TYPE_KEY, out string val) &&
                int.TryParse(val, out int type))
                return (EnchantType)type;
            return EnchantType.None;
        }

        public static float GetEnchantValue(ItemDrop.ItemData item)
        {
            if (item?.m_customData == null) return 0f;
            if (item.m_customData.TryGetValue(ENCHANT_VALUE_KEY, out string val) &&
                float.TryParse(val, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out float v))
                return v;
            return 0f;
        }

        #region Harmony Patches

        // ============================================================
        // 내구도 보너스 + 마법부여: InventoryGui.DoCrafting Postfix
        // ============================================================
        [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
        public static class Producer_InventoryGui_DoCrafting_Patch
        {
            public static void Postfix(InventoryGui __instance, Player player)
            {
                try
                {
                    if (!ProducerSkills.IsProducer(player)) return;
                    int level = ProducerSkills.GetProducerLevel(player);

                    // 방금 제작된 아이템 찾기 (인벤토리에서 가장 최근 추가된 것)
                    ItemDrop.ItemData crafted = FindLastCraftedItem(player);
                    if (crafted == null) return;

                    // --- 내구도 보너스 (Lv2+) ---
                    float durBonus = Producer_Config.GetDurabilityBonus(level);
                    if (durBonus > 0f && crafted.m_durability > 0f)
                    {
                        crafted.m_durability *= (1f + durBonus / 100f);
                        Plugin.Log.LogDebug($"[제작 전문가] 내구도 +{durBonus}%: {crafted.m_shared.m_name}");
                    }

                    // --- 마법부여 (Lv3+) ---
                    float enchChance = Producer_Config.GetEnchantChance(level);
                    if (enchChance > 0f && UnityEngine.Random.Range(0f, 100f) < enchChance)
                    {
                        ApplyEnchantment(player, crafted, level);
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[제작 전문가] DoCrafting 패치 오류: {ex.Message}");
                }
            }

            private static ItemDrop.ItemData FindLastCraftedItem(Player player)
            {
                // 인벤토리의 가장 마지막에 추가된 일반 아이템 검색
                // Valheim에서 제작 후 아이템은 인벤토리에 추가됨
                var inv = player.GetInventory();
                if (inv == null) return null;

                ItemDrop.ItemData newest = null;
                foreach (var item in inv.GetAllItems())
                {
                    // 무기 또는 방어구만 대상
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon ||
                        item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon ||
                        item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow ||
                        item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet ||
                        item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest ||
                        item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs ||
                        item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Hands ||
                        item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shoulder)
                    {
                        newest = item; // 마지막으로 일치한 것이 가장 최근 아이템
                    }
                }
                return newest;
            }

            private static void ApplyEnchantment(Player player, ItemDrop.ItemData item, int level)
            {
                bool isWeapon = (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon ||
                                 item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon ||
                                 item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow);

                EnchantType type;
                float value;

                if (isWeapon)
                {
                    // 무기: 공격력 or 방어구: 무기로 처리 시 공격력 부여
                    type  = EnchantType.WeaponDmg;
                    value = GetEnchantRange(level, Producer_Config.ProducerEnchantWeaponDmgMin_Lv3.Value,
                                                   Producer_Config.ProducerEnchantWeaponDmgMax_Lv5.Value, level);
                }
                else
                {
                    // 방어구: 방어력 or 체력
                    bool useArmor = UnityEngine.Random.Range(0, 2) == 0;
                    if (useArmor)
                    {
                        type  = EnchantType.Armor;
                        value = GetEnchantRange(level, Producer_Config.ProducerEnchantArmorMin_Lv3.Value,
                                                       Producer_Config.ProducerEnchantArmorMax_Lv5.Value, level);
                    }
                    else
                    {
                        type  = EnchantType.MaxHP;
                        value = GetEnchantRange(level, Producer_Config.ProducerEnchantHpMin_Lv3.Value,
                                                       Producer_Config.ProducerEnchantHpMax_Lv5.Value, level);
                    }
                }

                if (item.m_customData == null)
                    item.m_customData = new Dictionary<string, string>();

                item.m_customData[ENCHANT_TYPE_KEY]  = ((int)type).ToString();
                item.m_customData[ENCHANT_VALUE_KEY]  = value.ToString(System.Globalization.CultureInfo.InvariantCulture);

                // 플레이어에게 알림
                string enchantMsg = GetEnchantMessage(type, value);
                player.Message(MessageHud.MessageType.Center, enchantMsg);
                Plugin.Log.LogDebug($"[제작 전문가] 마법부여: {item.m_shared.m_name} - {enchantMsg}");
            }

            private static float GetEnchantRange(int level, float minBase, float maxBase, int lv)
            {
                // 레벨에 따라 min/max 선택
                float minVal, maxVal;
                switch (lv)
                {
                    case 3:
                        minVal = Producer_Config.ProducerEnchantWeaponDmgMin_Lv3.Value;
                        maxVal = Producer_Config.ProducerEnchantWeaponDmgMax_Lv3.Value;
                        break;
                    case 4:
                        minVal = Producer_Config.ProducerEnchantWeaponDmgMin_Lv4.Value;
                        maxVal = Producer_Config.ProducerEnchantWeaponDmgMax_Lv4.Value;
                        break;
                    default:
                        minVal = Producer_Config.ProducerEnchantWeaponDmgMin_Lv5.Value;
                        maxVal = Producer_Config.ProducerEnchantWeaponDmgMax_Lv5.Value;
                        break;
                }
                return UnityEngine.Random.Range(minVal, maxVal);
            }

            private static string GetEnchantMessage(EnchantType type, float value)
            {
                switch (type)
                {
                    case EnchantType.WeaponDmg: return L.Get("producer_enchant_weapon_dmg", $"{value:F1}");
                    case EnchantType.Armor:     return L.Get("producer_enchant_armor", $"{value:F1}");
                    case EnchantType.MaxHP:     return L.Get("producer_enchant_hp", $"{value:F1}");
                    default:                    return L.Get("producer_enchant_notify");
                }
            }
        }

        // ============================================================
        // 재료 감소: Inventory.CountItems / Player 아이템 소모 패치
        // 가장 안전한 방법: 제작 완료 후 재료를 일부 돌려주기
        // ============================================================
        [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
        public static class Producer_InventoryGui_DoCrafting_MaterialReturn_Patch
        {
            // 재료 소모 전에 감소율을 저장
            private static bool _craftPending = false;
            private static Dictionary<string, int> _materialSnapshot = new Dictionary<string, int>();

            public static void Prefix(InventoryGui __instance, Player player)
            {
                try
                {
                    _craftPending = false;
                    if (!ProducerSkills.IsProducer(player)) return;
                    int level = ProducerSkills.GetProducerLevel(player);
                    if (level < 2) return;

                    _craftPending = true;
                    // 스냅샷: 현재 인벤토리 아이템 수량
                    _materialSnapshot.Clear();
                    var inv = player.GetInventory();
                    if (inv == null) return;

                    foreach (var item in inv.GetAllItems())
                    {
                        string key = item.m_shared.m_name;
                        if (!_materialSnapshot.ContainsKey(key))
                            _materialSnapshot[key] = 0;
                        _materialSnapshot[key] += item.m_stack;
                    }
                }
                catch (Exception) { }
            }

            public static void Postfix(InventoryGui __instance, Player player)
            {
                try
                {
                    if (!_craftPending) return;
                    if (!ProducerSkills.IsProducer(player)) return;
                    int level = ProducerSkills.GetProducerLevel(player);
                    float reduction = Producer_Config.GetMaterialReduction(level) / 100f;
                    if (reduction <= 0f) return;

                    // 소모된 재료 파악 후 일부 환불
                    var inv = player.GetInventory();
                    if (inv == null) return;

                    foreach (var kv in _materialSnapshot)
                    {
                        int afterCount = inv.CountItems(kv.Key);
                        int consumed = kv.Value - afterCount;
                        if (consumed <= 0) continue;

                        // 감소율만큼 환불
                        int refund = Mathf.FloorToInt(consumed * reduction);
                        if (refund <= 0) continue;

                        // 아이템 프리팹 찾아서 환불
                        var prefab = ObjectDB.instance?.GetItemPrefab(kv.Key);
                        if (prefab == null) continue;

                        var itemDrop = prefab.GetComponent<ItemDrop>();
                        if (itemDrop == null) continue;

                        inv.AddItem(itemDrop.m_itemData.m_shared.m_name,
                                   refund,
                                   itemDrop.m_itemData.m_quality,
                                   itemDrop.m_itemData.m_variant,
                                   0L, "");

                        Plugin.Log.LogDebug($"[제작 전문가] 재료 환불: {kv.Key} x{refund} ({reduction*100:F0}% 감소)");
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[제작 전문가] 재료 환불 오류: {ex.Message}");
                }
            }
        }

        // ============================================================
        // 마법부여 툴팁 표시: ItemDrop.GetTooltip Postfix
        // ============================================================
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
            new Type[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
        public static class Producer_ItemData_GetTooltip_Patch
        {
            public static void Postfix(ref string __result, ItemDrop.ItemData item)
            {
                try
                {
                    var type = GetEnchantType(item);
                    if (type == EnchantType.None) return;

                    float val = GetEnchantValue(item);
                    string enchantLine = type switch {
                        EnchantType.WeaponDmg => L.Get("producer_enchant_weapon_dmg", $"{val:F1}"),
                        EnchantType.Armor     => L.Get("producer_enchant_armor", $"{val:F1}"),
                        EnchantType.MaxHP     => L.Get("producer_enchant_hp", $"{val:F1}"),
                        _                     => ""
                    };

                    if (!string.IsNullOrEmpty(enchantLine))
                        __result += $"\n<color=#FFD700>{enchantLine}</color>";
                }
                catch (Exception) { }
            }
        }

        #endregion
    }
}

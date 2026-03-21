using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가(Producer) 제작 관련 패치
    /// - 내구도 보너스 (Lv2+)
    /// - 재료 감소 (Lv2+)
    /// - 제작 마법부여 (Lv3+): WeaponDmg/WeaponSpd 1:1, Armor/MaxHP/MaxStamina 1:1:1
    /// - 마법부여 효과 발동 패치
    /// </summary>
    public static class ProducerCrafting
    {
        private const string ENCHANT_TYPE_KEY  = "cspt_enchant_type";
        private const string ENCHANT_VALUE_KEY = "cspt_enchant_value";
        private const string DUR_BONUS_KEY     = "cspt_dur_bonus_mult";

        public enum EnchantType { None = 0, WeaponDmg = 1, Armor = 2, MaxHP = 3, WeaponSpd = 4, MaxStamina = 5 }

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

        /// <summary>
        /// 장착 방어구 중 targetType 마법부여 합산
        /// </summary>
        public static float GetEquippedArmorEnchantTotal(Player player, EnchantType targetType)
        {
            var inv = player.GetInventory();
            if (inv == null) return 0f;
            float total = 0f;
            foreach (var item in inv.GetAllItems())
            {
                if (!item.m_equipped) continue;   // 착용 중인 아이템만
                var t = item.m_shared.m_itemType;
                if (t != ItemDrop.ItemData.ItemType.Helmet   &&
                    t != ItemDrop.ItemData.ItemType.Chest     &&
                    t != ItemDrop.ItemData.ItemType.Legs      &&
                    t != ItemDrop.ItemData.ItemType.Hands     &&
                    t != ItemDrop.ItemData.ItemType.Shoulder) continue;
                if (GetEnchantType(item) == targetType)
                    total += GetEnchantValue(item);
            }
            return total;
        }

        #region Harmony Patches

        // ============================================================
        // 내구도 보너스 + 마법부여: InventoryGui.DoCrafting Postfix
        // ============================================================
        [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
        public static class Producer_InventoryGui_DoCrafting_Patch
        {
            private static readonly HashSet<string> _preItemPositionSnapshot = new HashSet<string>();

            public static void Prefix(InventoryGui __instance, Player player)
            {
                _preItemPositionSnapshot.Clear();
                if (!ProducerSkills.IsProducer(player)) return;
                var inv = player.GetInventory();
                if (inv == null) return;
                foreach (var item in inv.GetAllItems())
                    if (IsApplicableItemType(item))
                        _preItemPositionSnapshot.Add($"{item.m_gridPos.x},{item.m_gridPos.y}");
            }

            public static void Postfix(InventoryGui __instance, Player player)
            {
                try
                {
                    if (!ProducerSkills.IsProducer(player)) return;
                    int level = ProducerSkills.GetProducerLevel(player);

                    ItemDrop.ItemData crafted = FindLastCraftedItem(player);
                    if (crafted == null) return;

                    // --- 내구도 보너스 (Lv2+) ---
                    float durBonus = Producer_Config.GetDurabilityBonus(level);
                    if (durBonus > 0f && crafted.m_durability > 0f)
                    {
                        float mult = 1f + durBonus / 100f;
                        crafted.m_durability *= mult;
                        // m_customData에 배율 저장 → GetMaxDurability() 패치에서 분모도 동일하게 증가
                        if (crafted.m_customData == null)
                            crafted.m_customData = new Dictionary<string, string>();
                        crafted.m_customData[DUR_BONUS_KEY] = mult.ToString(
                            System.Globalization.CultureInfo.InvariantCulture);
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
                var inv = player.GetInventory();
                if (inv == null) return null;

                foreach (var item in inv.GetAllItems())
                {
                    if (!IsApplicableItemType(item)) continue;
                    string posKey = $"{item.m_gridPos.x},{item.m_gridPos.y}";
                    if (!_preItemPositionSnapshot.Contains(posKey))
                        return item;
                }
                return null;
            }

            private static bool IsApplicableItemType(ItemDrop.ItemData item)
            {
                var t = item.m_shared.m_itemType;
                return t == ItemDrop.ItemData.ItemType.OneHandedWeapon
                    || t == ItemDrop.ItemData.ItemType.TwoHandedWeapon
                    || t == ItemDrop.ItemData.ItemType.Bow
                    || t == ItemDrop.ItemData.ItemType.Helmet
                    || t == ItemDrop.ItemData.ItemType.Chest
                    || t == ItemDrop.ItemData.ItemType.Legs
                    || t == ItemDrop.ItemData.ItemType.Hands
                    || t == ItemDrop.ItemData.ItemType.Shoulder;
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
                    // 무기: 공격력 / 공격속도 1:1
                    bool useDmg = UnityEngine.Random.Range(0, 2) == 0;
                    if (useDmg)
                    {
                        type  = EnchantType.WeaponDmg;
                        value = GetWeaponDmgRange(level);
                    }
                    else
                    {
                        type  = EnchantType.WeaponSpd;
                        value = GetWeaponSpdRange(level);
                    }
                }
                else
                {
                    // 방어구: 방어력 / 체력 / 스태미나 1:1:1
                    int roll = UnityEngine.Random.Range(0, 3);
                    if (roll == 0)
                    {
                        type  = EnchantType.Armor;
                        value = GetArmorRange(level);
                    }
                    else if (roll == 1)
                    {
                        type  = EnchantType.MaxHP;
                        value = GetHpRange(level);
                    }
                    else
                    {
                        type  = EnchantType.MaxStamina;
                        value = GetStaminaRange(level);
                    }
                }

                if (item.m_customData == null)
                    item.m_customData = new Dictionary<string, string>();

                item.m_customData[ENCHANT_TYPE_KEY]  = ((int)type).ToString();
                value = (float)Math.Round(value, 1, MidpointRounding.AwayFromZero); // 소수점 둘째 자리에서 반올림
                item.m_customData[ENCHANT_VALUE_KEY] = value.ToString(System.Globalization.CultureInfo.InvariantCulture);

                // 플레이어에게 알림
                string enchantMsg = GetEnchantMessage(type, value);
                player.Message(MessageHud.MessageType.Center, enchantMsg);
                Plugin.Log.LogDebug($"[제작 전문가] 마법부여: {item.m_shared.m_name} - {enchantMsg}");

                // VFX (커스텀) + SFX (발헤임 기본)
                try
                {
                    SimpleVFX.Play("statusailment_01", player.transform.position);
                    VFXManager.PlayVFXAtPosition("sfx_fader_bell", player.transform.position);
                }
                catch (Exception) { }
            }

            private static float GetWeaponDmgRange(int level)
            {
                switch (level)
                {
                    case 3:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantWeaponDmgMin_Lv3.Value, Producer_Config.ProducerEnchantWeaponDmgMax_Lv3.Value);
                    case 4:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantWeaponDmgMin_Lv4.Value, Producer_Config.ProducerEnchantWeaponDmgMax_Lv4.Value);
                    default: return UnityEngine.Random.Range(Producer_Config.ProducerEnchantWeaponDmgMin_Lv5.Value, Producer_Config.ProducerEnchantWeaponDmgMax_Lv5.Value);
                }
            }

            private static float GetWeaponSpdRange(int level)
            {
                switch (level)
                {
                    case 3:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantWeaponSpdMin_Lv3.Value, Producer_Config.ProducerEnchantWeaponSpdMax_Lv3.Value);
                    case 4:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantWeaponSpdMin_Lv4.Value, Producer_Config.ProducerEnchantWeaponSpdMax_Lv4.Value);
                    default: return UnityEngine.Random.Range(Producer_Config.ProducerEnchantWeaponSpdMin_Lv5.Value, Producer_Config.ProducerEnchantWeaponSpdMax_Lv5.Value);
                }
            }

            private static float GetArmorRange(int level)
            {
                switch (level)
                {
                    case 3:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantArmorMin_Lv3.Value, Producer_Config.ProducerEnchantArmorMax_Lv3.Value);
                    case 4:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantArmorMin_Lv4.Value, Producer_Config.ProducerEnchantArmorMax_Lv4.Value);
                    default: return UnityEngine.Random.Range(Producer_Config.ProducerEnchantArmorMin_Lv5.Value, Producer_Config.ProducerEnchantArmorMax_Lv5.Value);
                }
            }

            private static float GetHpRange(int level)
            {
                switch (level)
                {
                    case 3:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantHpMin_Lv3.Value, Producer_Config.ProducerEnchantHpMax_Lv3.Value);
                    case 4:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantHpMin_Lv4.Value, Producer_Config.ProducerEnchantHpMax_Lv4.Value);
                    default: return UnityEngine.Random.Range(Producer_Config.ProducerEnchantHpMin_Lv5.Value, Producer_Config.ProducerEnchantHpMax_Lv5.Value);
                }
            }

            private static float GetStaminaRange(int level)
            {
                switch (level)
                {
                    case 3:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantStaminaMin_Lv3.Value, Producer_Config.ProducerEnchantStaminaMax_Lv3.Value);
                    case 4:  return UnityEngine.Random.Range(Producer_Config.ProducerEnchantStaminaMin_Lv4.Value, Producer_Config.ProducerEnchantStaminaMax_Lv4.Value);
                    default: return UnityEngine.Random.Range(Producer_Config.ProducerEnchantStaminaMin_Lv5.Value, Producer_Config.ProducerEnchantStaminaMax_Lv5.Value);
                }
            }

            private static string GetEnchantMessage(EnchantType type, float value)
            {
                switch (type)
                {
                    case EnchantType.WeaponDmg:   return L.Get("producer_enchant_weapon_dmg", $"{value:F1}");
                    case EnchantType.WeaponSpd:   return L.Get("producer_enchant_weapon_spd", $"{value:F1}");
                    case EnchantType.Armor:        return L.Get("producer_enchant_armor",      $"{value:F1}");
                    case EnchantType.MaxHP:        return L.Get("producer_enchant_hp",         $"{value:F1}");
                    case EnchantType.MaxStamina:   return L.Get("producer_enchant_stamina",    $"{value:F1}");
                    default:                       return L.Get("producer_enchant_notify");
                }
            }
        }

        // ============================================================
        // 재료 감소: 제작 완료 후 재료 일부 환불
        // ============================================================
        [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
        public static class Producer_InventoryGui_DoCrafting_MaterialReturn_Patch
        {
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

                    var inv = player.GetInventory();
                    if (inv == null) return;

                    foreach (var kv in _materialSnapshot)
                    {
                        int afterCount = inv.CountItems(kv.Key);
                        int consumed = kv.Value - afterCount;
                        if (consumed <= 0) continue;

                        int refund = Mathf.FloorToInt(consumed * reduction);
                        if (refund <= 0) continue;

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

                    // 아이템 이름 첫 줄에 인챈트 금색 ✨ 표시 추가
                    var lines = __result.Split('\n');
                    if (lines.Length > 0)
                        lines[0] = $"<color=#FFD700>✨</color> {lines[0]}";
                    __result = string.Join("\n", lines);

                    float val = GetEnchantValue(item);
                    string enchantLine = type switch {
                        EnchantType.WeaponDmg  => L.Get("producer_enchant_weapon_dmg", $"{val:F1}"),
                        EnchantType.WeaponSpd  => L.Get("producer_enchant_weapon_spd", $"{val:F1}"),
                        EnchantType.Armor      => L.Get("producer_enchant_armor",      $"{val:F1}"),
                        EnchantType.MaxHP      => L.Get("producer_enchant_hp",         $"{val:F1}"),
                        EnchantType.MaxStamina => L.Get("producer_enchant_stamina",    $"{val:F1}"),
                        _                      => ""
                    };

                    if (!string.IsNullOrEmpty(enchantLine))
                        __result += $"\n<color=#FFD700>{enchantLine}</color>";
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 마법부여 효과: WeaponDmg - Character.Damage Prefix
        // ============================================================
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Producer_Enchant_WeaponDmg_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Prefix(Character __instance, HitData hit)
            {
                try
                {
                    var attacker = hit.GetAttacker();
                    if (attacker == null || !attacker.IsPlayer()) return;

                    var player = attacker as Player;
                    if (player == null) return;

                    var weapon = player.GetCurrentWeapon();
                    if (weapon == null) return;

                    if (GetEnchantType(weapon) != EnchantType.WeaponDmg) return;
                    float bonus = GetEnchantValue(weapon);
                    if (bonus <= 0f) return;

                    float mult = bonus / 100f;
                    if (hit.m_damage.m_blunt  > 0) hit.m_damage.m_blunt  *= (1f + mult);
                    if (hit.m_damage.m_slash  > 0) hit.m_damage.m_slash  *= (1f + mult);
                    if (hit.m_damage.m_pierce > 0) hit.m_damage.m_pierce *= (1f + mult);
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 마법부여 효과: Armor - Character.GetBodyArmor Postfix
        // ============================================================
        [HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
        public static class Producer_Enchant_GetBodyArmor_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Character __instance, ref float __result)
            {
                try
                {
                    if (!__instance.IsPlayer()) return;
                    var player = __instance as Player;
                    if (player == null) return;

                    // 바위피부 활성 시: StatTree 패치에서 합산 처리 → 여기서 스킵
                    var manager = SkillTreeManager.Instance;
                    if (manager != null && manager.GetSkillLevel("defense_Step4_tanker") > 0) return;

                    float enchantPctTotal = GetEquippedArmorEnchantTotal(player, EnchantType.Armor);
                    if (enchantPctTotal > 0f) __result += __result * (enchantPctTotal / 100f);
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 마법부여 효과: MaxHP - Character.GetMaxHealth Postfix
        // ============================================================
        [HarmonyPatch(typeof(Character), nameof(Character.GetMaxHealth))]
        public static class Producer_Enchant_GetMaxHealth_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Character __instance, ref float __result)
            {
                try
                {
                    if (!__instance.IsPlayer()) return;
                    var player = __instance as Player;
                    if (player == null) return;

                    float bonus = GetEquippedArmorEnchantTotal(player, EnchantType.MaxHP);
                    if (bonus > 0f) __result += bonus;
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 내구도 최대치 보정: GetMaxDurability Postfix
        // m_customData의 배율을 적용해 "12000/12000" 형태로 표시
        // ============================================================
        [HarmonyPatch(typeof(ItemDrop.ItemData), "GetMaxDurability", new[] { typeof(int) })]
        public static class Producer_GetMaxDurability_Patch
        {
            public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
            {
                try
                {
                    if (__instance?.m_customData == null) return;
                    if (!__instance.m_customData.TryGetValue(DUR_BONUS_KEY, out string val)) return;
                    if (float.TryParse(val, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out float mult))
                    {
                        __result *= mult;
                    }
                }
                catch (Exception) { }
            }
        }

        #endregion
    }
}

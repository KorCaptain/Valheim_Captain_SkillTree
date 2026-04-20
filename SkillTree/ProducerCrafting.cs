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

        public enum EnchantType
        {
            None          = 0,
            WeaponDmg     = 1,   // 무기: 공격력 +%
            Armor         = 2,   // 흉갑: 방어력 +%
            MaxHP         = 3,   // 투구/흉갑: 체력 +%
            WeaponSpd     = 4,   // 일반무기: 공격속도 +%
            MaxStamina    = 5,   // 망토: 스태미나 +%
            BowCrit       = 6,   // 활: 치명타 +%
            CrossbowReload= 7,   // 석궁: 재장전 속도 단축 (ms)
            CooldownReduce= 8,   // 투구: 쿨타임 감소 +%
            DodgeRoll     = 9,   // 각반: 회피 거리 +%
            MoveSpeed     = 10,  // 각반: 이동속도 +%
            Eitr          = 11,  // 망토: 에이트르 +수치(flat)
            InvWeight     = 12,  // 악세사리: 인벤 최대 무게 +수치
            EitrRegen     = 13,  // 악세사리: 에이트르 회복속도 +%
            JumpForce     = 14,  // 악세사리: 점프력 +%
            BlockPower    = 15,  // 방패: 가드 방어력 +%
        }

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
        /// 장착 방어구 중 targetType 마법부여 합산 (기존 호환 유지)
        /// </summary>
        public static float GetEquippedArmorEnchantTotal(Player player, EnchantType targetType)
        {
            var inv = player.GetInventory();
            if (inv == null) return 0f;
            float total = 0f;
            foreach (var item in inv.GetAllItems())
            {
                if (!item.m_equipped) continue;
                var t = item.m_shared.m_itemType;
                if (t != ItemDrop.ItemData.ItemType.Helmet   &&
                    t != ItemDrop.ItemData.ItemType.Chest     &&
                    t != ItemDrop.ItemData.ItemType.Legs      &&
                    t != ItemDrop.ItemData.ItemType.Shoulder) continue;
                if (GetEnchantType(item) == targetType)
                    total += GetEnchantValue(item);
            }
            return total;
        }

        /// <summary>
        /// 착용 중인 악세사리(Utility/Ring/Necklace) 마법부여 합산
        /// </summary>
        public static float GetEquippedAccessoryEnchantTotal(Player player, EnchantType targetType)
        {
            var inv = player.GetInventory();
            if (inv == null) return 0f;
            float total = 0f;
            foreach (var item in inv.GetAllItems())
            {
                if (!item.m_equipped) continue;
                if (item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Utility) continue;
                if (GetEnchantType(item) == targetType)
                    total += GetEnchantValue(item);
            }
            return total;
        }

        /// <summary>
        /// 특정 ItemType 슬롯(들)에서 targetType 마법부여 합산
        /// </summary>
        public static float GetEquippedSlotEnchantTotal(Player player, EnchantType targetType,
            params ItemDrop.ItemData.ItemType[] allowedSlots)
        {
            var inv = player.GetInventory();
            if (inv == null) return 0f;
            float total = 0f;
            var slotSet = new HashSet<ItemDrop.ItemData.ItemType>(allowedSlots);
            foreach (var item in inv.GetAllItems())
            {
                if (!item.m_equipped) continue;
                if (!slotSet.Contains(item.m_shared.m_itemType)) continue;
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
            // 수리 감지용 Reflection 캐시 (DoCrafting은 수리에도 호출됨)
            private static System.Reflection.FieldInfo _craftTimerField;

            private static bool IsRepairAction(InventoryGui gui)
            {
                try
                {
                    if (_craftTimerField == null)
                        _craftTimerField = typeof(InventoryGui).GetField("m_craftTimer",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (_craftTimerField != null)
                        return (float)_craftTimerField.GetValue(gui) < 0f;
                }
                catch { }
                return false;
            }

            public static void Prefix(InventoryGui __instance, Player player)
            {
                _preItemPositionSnapshot.Clear();
                if (!ProducerSkills.IsProducer(player)) return;
                if (IsRepairAction(__instance)) return; // 수리 시 인벤토리 순회 스킵
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
                    bool durApplied = false;
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
                        durApplied = true;
                    }

                    // --- 마법부여 (Lv3+) ---
                    bool enchantApplied = false;
                    float enchChance = Producer_Config.GetEnchantChance(level);
                    if (enchChance > 0f && UnityEngine.Random.Range(0f, 100f) < enchChance)
                    {
                        ApplyEnchantment(player, crafted, level); // 내부에서 VFX 처리
                        enchantApplied = true;
                    }

                    // 내구도만 적용된 경우 VFX (마법부여는 ApplyEnchantment 내부에서 처리)
                    if (durApplied && !enchantApplied)
                    {
                        try
                        {
                            SimpleVFX.Play("statusailment_01", player.transform.position);
                            VFXManager.PlayVFXAtPosition("sfx_fader_bell", player.transform.position);
                        }
                        catch (Exception) { }
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
                return !string.IsNullOrEmpty(GetSlotKey(item));
            }

            private static void ApplyEnchantment(Player player, ItemDrop.ItemData item, int level)
            {
                string slotKey = GetSlotKey(item);
                if (string.IsNullOrEmpty(slotKey)) return;

                int enchantId = ProducerEnchantData.PickRandom(slotKey);
                if (enchantId == 0) return;

                var range = ProducerEnchantData.GetRange(enchantId, level);
                float value = UnityEngine.Random.Range(range.Min, range.Max);
                value = (float)Math.Round(value, 1, MidpointRounding.AwayFromZero);

                if (item.m_customData == null)
                    item.m_customData = new Dictionary<string, string>();

                item.m_customData[ENCHANT_TYPE_KEY]  = enchantId.ToString();
                item.m_customData[ENCHANT_VALUE_KEY] = value.ToString(
                    System.Globalization.CultureInfo.InvariantCulture);

                string enchantMsg = GetEnchantMessage((EnchantType)enchantId, value);
                player.Message(MessageHud.MessageType.Center, enchantMsg);
                Plugin.Log.LogDebug($"[제작 전문가] 마법부여: {item.m_shared.m_name} [{slotKey}] id={enchantId} val={value}");

                try
                {
                    SimpleVFX.Play("statusailment_01", player.transform.position);
                    VFXManager.PlayVFXAtPosition("sfx_fader_bell", player.transform.position);
                    CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("vfx_shieldgenerator_refuel", "", player.transform.position, Quaternion.identity, 3f);
                }
                catch (Exception) { }
            }

            /// <summary>
            /// 아이템 타입에 따른 슬롯 키 반환 (ProducerEnchantData.GetPool 호출용)
            /// </summary>
            private static string GetSlotKey(ItemDrop.ItemData item)
            {
                var t = item.m_shared.m_itemType;
                // 석궁: skillType으로 구분
                if (item.m_shared.m_skillType == Skills.SkillType.Crossbows)
                    return "Crossbow";
                // 활
                if (t == ItemDrop.ItemData.ItemType.Bow)
                    return "Bow";
                // 일반 무기
                if (t == ItemDrop.ItemData.ItemType.OneHandedWeapon ||
                    t == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                    return "Weapon";
                // 방어구 슬롯
                if (t == ItemDrop.ItemData.ItemType.Helmet)   return "Helmet";
                if (t == ItemDrop.ItemData.ItemType.Chest)    return "Chest";
                if (t == ItemDrop.ItemData.ItemType.Legs)     return "Legs";
                if (t == ItemDrop.ItemData.ItemType.Shoulder) return "Shoulder";
                // 방패
                if (t == ItemDrop.ItemData.ItemType.Shield) return "Shield";
                // 악세사리
                if (t == ItemDrop.ItemData.ItemType.Utility) return "Accessory";
                return "";
            }

            private static string GetEnchantMessage(EnchantType type, float value)
            {
                string displayKey = ProducerEnchantData.GetDisplayKey((int)type);
                string unit       = ProducerEnchantData.GetUnit((int)type);
                string valStr     = unit == "ms"
                    ? $"{value:F0}"   // 재장전은 정수 표시
                    : $"{value:F1}";
                // unit이 % 또는 ms면 접미사 포함, 빈 문자열이면 수치만
                string suffix = unit == "%" ? "%" : (unit == "ms" ? "ms" : "");
                return L.Get(displayKey, valStr + suffix);
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
                    // 인챈트 출처 라인은 WeaponTooltip.cs / ArmorTooltip.cs에서 처리 (중복 방지)
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
        // 마법부여 효과: MaxHP - Player.GetTotalFoodValue Postfix
        // GetTotalFoodValue 패치로 m_baseHP에 포함 → 힐링 깜빡임 방지
        // ============================================================
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Producer_Enchant_GetMaxHealth_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance, ref float hp)
            {
                try
                {
                    // 비율 보너스를 고정값으로 변환하여 m_baseHP에 포함 (Rule 2 패턴)
                    float bonus = GetEquippedSlotEnchantTotal(__instance, EnchantType.MaxHP,
                        ItemDrop.ItemData.ItemType.Helmet,
                        ItemDrop.ItemData.ItemType.Chest);
                    if (bonus > 0f)
                    {
                        float bonusHp = hp * (bonus / 100f);
                        hp += bonusHp;
                        Plugin.Log.LogDebug($"[제작 마법부여 MaxHP] +{bonus}%: +{bonusHp:F0} (m_baseHP 포함)");
                    }
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

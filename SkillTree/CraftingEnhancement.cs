using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 강화 시스템 - 기존 스킬 시스템 연동
    /// 제작 스킬 (crafting_lv2, crafting_lv3, crafting_lv4) 학습 시 제작 강화 효과
    /// </summary>
    public static class CraftingEnhancement
    {
        // 강화 적용 기록 (중복 적용 방지)
        private static HashSet<string> enhancedItems = new HashSet<string>();
        
        /// <summary>
        /// 제작된 아이템에 강화 효과 적용
        /// </summary>
        public static void ApplyCraftingEnhancement(Player player, ItemDrop.ItemData item)
        {
            try
            {
                Plugin.Log.LogInfo($"[제작 강화] ApplyCraftingEnhancement 호출됨 - 아이템: {item?.m_shared?.m_name}");
                
                if (player == null || item == null) 
                {
                    Plugin.Log.LogDebug("[제작 강화] 플레이어 또는 아이템이 null");
                    return;
                }
                
                // 화살과 볼트는 제작 강화에서 제외
                if (IsArrowOrBolt(item))
                {
                    Plugin.Log.LogInfo($"[제작 강화] 화살/볼트 제외: {item.m_shared?.m_name}");
                    return;
                }
                
                // 제작 보너스 확인
                var craftingBonus = GetPlayerCraftingBonus(player);
                if (craftingBonus == null)
                {
                    Plugin.Log.LogInfo("[제작 강화] 제작 보너스 없음 (제작 스킬 미보유)");
                    return;
                }
                
                Plugin.Log.LogInfo($"[제작 강화] 제작 보너스 확인됨 - 강화확률: {craftingBonus.EnhanceChance * 100:F0}%, 내구도보너스: {craftingBonus.DurabilityBonus * 100:F0}%");

                // 제작 강화는 아이템 품질과 내구도만 향상 (재료 추가 제거)
                
                // 중복 적용 방지 (아이템 고유 ID 기반)
                string itemUID = $"{item.m_dropPrefab?.name}_{Time.time}_{UnityEngine.Random.Range(0, 10000)}";
                if (enhancedItems.Contains(itemUID))
                {
                    Plugin.Log.LogInfo("[제작 강화] 이미 처리된 아이템");
                    return;
                }
                enhancedItems.Add(itemUID);
                
                // 강화 확률 체크
                bool enhanceSuccess = UnityEngine.Random.value <= craftingBonus.EnhanceChance;
                bool durabilityApplied = craftingBonus.DurabilityBonus > 0;
                
                // 품질 강화 적용 (성공 시에만)
                if (enhanceSuccess)
                {
                    ApplyQualityEnhancement(item, craftingBonus.EnhanceLevel);
                    Plugin.Log.LogInfo($"[제작 강화] 강화 성공: {item.m_shared.m_name} +{craftingBonus.EnhanceLevel} (확률: {craftingBonus.EnhanceChance * 100:F0}%)");
                }
                else
                {
                    Plugin.Log.LogInfo($"[제작 강화] 강화 실패: {item.m_shared.m_name} (확률: {craftingBonus.EnhanceChance * 100:F0}%)");
                }
                
                // 내구도 보너스 적용 (항상 적용)
                if (durabilityApplied)
                {
                    ApplyDurabilityBonus(item, craftingBonus.DurabilityBonus);
                    Plugin.Log.LogInfo($"[제작 강화] 내구도 보너스 적용: {item.m_shared.m_name} +{craftingBonus.DurabilityBonus * 100:F0}%");
                }
                
                // 통합 메시지 표시
                ShowCraftingEnhancementMessage(player, enhanceSuccess, durabilityApplied, craftingBonus);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[제작 강화] 오류: {ex.Message}\n{ex.StackTrace}");
            }
        }
        
        /// <summary>
        /// 플레이어의 제작 보너스 가져오기 (기존 스킬 시스템 기반)
        /// </summary>
        public static CraftingBonus GetPlayerCraftingBonus(Player player)
        {
            if (player == null) 
            {
                Plugin.Log.LogInfo("[제작 보너스] 플레이어가 null");
                return null;
            }
            
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager == null)
                {
                    Plugin.Log.LogInfo("[제작 보너스] SkillTreeManager.Instance가 null");
                    return null;
                }
                
                Plugin.Log.LogInfo("[제작 보너스] 제작 스킬 레벨 확인 시작");
                
                // 제작 스킬 누적 계산 (Lv2: 25%, Lv3: +25%, Lv4: +25% = 최대 75%)
                float totalEnhanceChance = 0f;
                float totalDurabilityBonus = 0f;
                
                int lv2 = manager.GetSkillLevel("crafting_lv2");
                int lv3 = manager.GetSkillLevel("crafting_lv3");
                int lv4 = manager.GetSkillLevel("crafting_lv4");
                int lv5 = manager.GetSkillLevel("crafting_lv5");

                Plugin.Log.LogInfo($"[제작 보너스] 스킬 레벨 - Lv2:{lv2}, Lv3:{lv3}, Lv4:{lv4}, Lv5:{lv5}");
                
                if (lv2 > 0)
                {
                    totalEnhanceChance += 0.25f; // 제작 Lv2: 25%
                    totalDurabilityBonus += 0.25f; // 내구도 25%
                    Plugin.Log.LogInfo("[제작 보너스] Lv2 보너스 적용: 강화 25%, 내구도 25%");
                }
                
                if (lv3 > 0)
                {
                    totalEnhanceChance += 0.25f; // 제작 Lv3: +25% (누적 50%)
                    totalDurabilityBonus += 0.25f; // 내구도 +25%
                    Plugin.Log.LogInfo("[제작 보너스] Lv3 보너스 적용: 강화 +25%, 내구도 +25%");
                }
                
                if (lv4 > 0)
                {
                    totalEnhanceChance += 0.25f; // 제작 Lv4: +25% (누적 75%)
                    totalDurabilityBonus += 0.25f; // 내구도 +25%
                    Plugin.Log.LogInfo("[제작 보너스] Lv4 보너스 적용: 강화 +25%, 내구도 +25%");
                }

                if (lv5 > 0)
                {
                    float lv5Durability = Production_Config.CraftingLv5DurabilityBonusValue / 100f;
                    totalDurabilityBonus += lv5Durability; // 내구도 +30%
                    Plugin.Log.LogInfo($"[제작 보너스] Lv5 보너스 적용: 내구도 +{lv5Durability * 100:F0}%");
                }
                
                Plugin.Log.LogInfo($"[제작 보너스] 총 보너스 - 강화확률: {totalEnhanceChance * 100:F0}%, 내구도보너스: {totalDurabilityBonus * 100:F0}%");
                
                if (totalEnhanceChance > 0)
                {
                    var bonus = new CraftingBonus(totalEnhanceChance, totalDurabilityBonus, 1); // 누적 확률로 +1 강화
                    Plugin.Log.LogInfo($"[제작 보너스] CraftingBonus 생성 완료");
                    return bonus;
                }
                
                Plugin.Log.LogInfo("[제작 보너스] 제작 스킬 없음 - null 반환");
                return null; // 제작 스킬 없음
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[제작 강화] 제작 보너스 계산 실패: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 아이템 품질(강화 레벨) 향상
        /// </summary>
        internal static void ApplyQualityEnhancement(ItemDrop.ItemData item, int enhanceLevel)
        {
            try
            {
                if (item?.m_shared == null) return;
                
                // 현재 품질에 강화 레벨 추가
                int newQuality = Mathf.Min(item.m_quality + enhanceLevel, item.m_shared.m_maxQuality);
                
                if (newQuality > item.m_quality)
                {
                    int actualIncrease = newQuality - item.m_quality;
                    item.m_quality = newQuality;
                    
                    Plugin.Log.LogInfo($"[품질 강화] {item.m_shared.m_name}: {item.m_quality - actualIncrease} → {item.m_quality} (+{actualIncrease})");
                }
                else
                {
                    Plugin.Log.LogInfo($"[품질 강화] {item.m_shared.m_name}: 이미 최대 품질 ({item.m_quality}/{item.m_shared.m_maxQuality})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[품질 강화] 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 아이템 내구도 최대치 증가 (올바른 최대치 내구도 증가)
        /// </summary>
        internal static void ApplyDurabilityBonus(ItemDrop.ItemData item, float bonusPercent)
        {
            try
            {
                if (item?.m_shared == null) return;

                // 중복 적용 방지: 이미 보너스가 적용되어 있으면 스킵
                if (item.m_customData != null && item.m_customData.ContainsKey("CraftingDurabilityBonus"))
                {
                    Plugin.Log.LogInfo($"[내구도 강화] {item.m_shared.m_name}: 이미 내구도 보너스 적용됨");
                    return;
                }

                if (item.m_customData == null)
                    item.m_customData = new Dictionary<string, string>();

                // 배율 방식으로 저장 → GetMaxDurability 패치에서 분모도 동일하게 증가 (NNN/NNN 표시)
                float mult = 1f + bonusPercent;
                item.m_customData["CraftingDurabilityBonus"] = mult.ToString(
                    System.Globalization.CultureInfo.InvariantCulture);

                float oldDurability = item.m_durability;
                item.m_durability *= mult;

                Plugin.Log.LogInfo($"[내구도 강화] {item.m_shared.m_name}: 내구도 {oldDurability:F1} → {item.m_durability:F1} (+{bonusPercent * 100:F0}%)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[내구도 강화] 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 아이템의 실제 최대 내구도 계산 (보너스 포함)
        /// </summary>
        public static float GetEnhancedMaxDurability(ItemDrop.ItemData item)
        {
            if (item?.m_shared == null) return 0f;

            float baseDurability = item.GetMaxDurability();

            if (item.m_customData != null &&
                item.m_customData.TryGetValue("CraftingDurabilityBonus", out string bonusStr) &&
                float.TryParse(bonusStr, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out float mult))
            {
                return baseDurability * mult;
            }

            return baseDurability;
        }
        
        /// <summary>
        /// 제작 강화 메시지 표시 (사용자 요청에 따른 개별/통합 메시지)
        /// </summary>
        private static void ShowCraftingEnhancementMessage(Player player, bool enhanceSuccess, bool durabilityApplied, CraftingBonus craftingBonus)
        {
            if (!enhanceSuccess && !durabilityApplied)
            {
                // 둘 다 적용되지 않았으면 메시지 없음
                return;
            }
            
            if (enhanceSuccess && durabilityApplied)
            {
                // 두 효과가 모두 적용된 경우: 통합 메시지
                SkillEffect.DrawFloatingText(player, 
                    "✨ 강화 및 내구도 효과가 적용되었습니다.", 
                    Color.yellow);
            }
            else if (enhanceSuccess)
            {
                // 강화만 성공한 경우: 개별 메시지
                SkillEffect.DrawFloatingText(player, 
                    $"✨ 강화 +{craftingBonus.EnhanceLevel} 효과", 
                    Color.yellow);
            }
            else if (durabilityApplied)
            {
                // 내구도만 적용된 경우: 개별 메시지
                SkillEffect.DrawFloatingText(player, 
                    $"🔧 내구도 +{craftingBonus.DurabilityBonus * 100:F0}% 효과", 
                    Color.cyan);
            }
        }
        
        /// <summary>
        /// 강화 기록 정리 (메모리 관리)
        /// </summary>
        public static void ClearEnhancementHistory()
        {
            enhancedItems.Clear();
            Plugin.Log.LogInfo("[제작 강화] 강화 기록 정리 완료");
        }
        
        /// <summary>
        /// 화살과 볼트 아이템 판별
        /// </summary>
        internal static bool IsArrowOrBolt(ItemDrop.ItemData item)
        {
            try
            {
                if (item?.m_shared == null) return false;
                
                // 아이템 타입이 탄약인지 확인
                if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo)
                {
                    Plugin.Log.LogDebug($"[제작 강화] 탄약 타입 감지: {item.m_shared.m_name}");
                    return true;
                }
                
                // 아이템명으로 화살/볼트 판별 (Valheim 표준 명명 규칙)
                string itemName = item.m_shared?.m_name?.ToLower() ?? "";
                
                // 화살 관련 아이템명들
                if (itemName.Contains("arrow") || itemName.Contains("화살") || 
                    itemName.Contains("wood") && itemName.Contains("arrow") ||
                    itemName.Contains("fire") && itemName.Contains("arrow") ||
                    itemName.Contains("frost") && itemName.Contains("arrow") ||
                    itemName.Contains("poison") && itemName.Contains("arrow") ||
                    itemName.Contains("obsidian") && itemName.Contains("arrow") ||
                    itemName.Contains("needle") && itemName.Contains("arrow") ||
                    itemName.Contains("silver") && itemName.Contains("arrow"))
                {
                    Plugin.Log.LogDebug($"[제작 강화] 화살 감지: {item.m_shared.m_name}");
                    return true;
                }
                
                // 볼트 관련 아이템명들  
                if (itemName.Contains("bolt") || itemName.Contains("볼트") ||
                    itemName.Contains("wood") && itemName.Contains("bolt") ||
                    itemName.Contains("bone") && itemName.Contains("bolt") ||
                    itemName.Contains("iron") && itemName.Contains("bolt") ||
                    itemName.Contains("silver") && itemName.Contains("bolt") ||
                    itemName.Contains("blackmetal") && itemName.Contains("bolt"))
                {
                    Plugin.Log.LogDebug($"[제작 강화] 볼트 감지: {item.m_shared.m_name}");
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[제작 강화] IsArrowOrBolt 오류: {ex.Message}");
                return false;
            }
        }
    }
    
    /// <summary>
    /// 제작 보너스 클래스
    /// </summary>
    [System.Serializable]
    public class CraftingBonus
    {
        public float EnhanceChance { get; set; }     // 강화 확률 (0.25f = 25%)
        public float DurabilityBonus { get; set; }   // 내구도 보너스 (0.25f = 25%)
        public int EnhanceLevel { get; set; }        // 강화 레벨 (+1)
        
        public CraftingBonus(float enhanceChance, float durabilityBonus, int enhanceLevel)
        {
            EnhanceChance = enhanceChance;
            DurabilityBonus = durabilityBonus;
            EnhanceLevel = enhanceLevel;
        }
    }
    
    /// <summary>
    /// 중복된 Inventory.AddItem 패치 제거됨
    /// 제작 강화는 AccurateCraftingDetector에서 직접 호출
    /// </summary>
    
    /// <summary>
    /// ItemDrop.ItemData.GetMaxDurability 패치 - 내구도 보너스 반영
    /// MMO 시스템 연동 우선 - 안전한 매개변수 없는 GetMaxDurability 메서드 타겟
    /// </summary>
    [HarmonyPatch(typeof(ItemDrop.ItemData), "GetMaxDurability", new[] { typeof(int) })]
    public static class ItemData_GetMaxDurability_Enhancement_Patch
    {
        private static void Postfix(ItemDrop.ItemData __instance, ref float __result)
        {
            try
            {
                if (__instance?.m_customData == null) return;

                if (__instance.m_customData.TryGetValue("CraftingDurabilityBonus", out string bonusStr) &&
                    float.TryParse(bonusStr, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out float mult))
                {
                    __result *= mult;
                    #if DEBUG
                    Plugin.Log.LogDebug($"[내구도 패치] {__instance.m_shared?.m_name}: x{mult:F2} 내구도 배율 적용");
                    #endif
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[내구도 패치] 오류: {ex.Message}");
            }
        }
    }

    // ============================================================
    // crafting_lv2~4 내구도 최대치 증가: DoCrafting Prefix/Postfix
    // ConsumeResources 타이밍 버그 수정 - 스냅샷 방식으로 새 아이템 탐지
    // ProducerCrafting.cs의 Producer_InventoryGui_DoCrafting_Patch와 동일 구조
    // ============================================================
    [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
    public static class CraftingDurability_DoCrafting_Patch
    {
        private static readonly HashSet<string> _preSnapshot = new HashSet<string>();

        public static void Prefix(InventoryGui __instance, Player player)
        {
            _preSnapshot.Clear();
            if (player != Player.m_localPlayer) return;
            var manager = SkillTreeManager.Instance;
            if (manager == null || manager.GetSkillLevel("crafting_lv2") <= 0) return;
            var inv = player.GetInventory();
            if (inv == null) return;
            foreach (var item in inv.GetAllItems())
                if (!CraftingEnhancement.IsArrowOrBolt(item))
                    _preSnapshot.Add($"{item.m_gridPos.x},{item.m_gridPos.y}");
        }

        public static void Postfix(InventoryGui __instance, Player player)
        {
            if (_preSnapshot.Count == 0) return;
            if (player != Player.m_localPlayer) return;
            var inv = player.GetInventory();
            if (inv == null) { _preSnapshot.Clear(); return; }

            // 스냅샷에 없는 새 아이템 탐지 (ProducerCrafting과 동일 패턴)
            ItemDrop.ItemData crafted = null;
            foreach (var item in inv.GetAllItems())
            {
                if (CraftingEnhancement.IsArrowOrBolt(item)) continue;
                if (!_preSnapshot.Contains($"{item.m_gridPos.x},{item.m_gridPos.y}"))
                { crafted = item; break; }
            }
            _preSnapshot.Clear();

            if (crafted == null) return;

            var bonus = CraftingEnhancement.GetPlayerCraftingBonus(player);
            if (bonus == null) return;

            // 품질 강화 (확률)
            bool enhanceSuccess = UnityEngine.Random.value <= bonus.EnhanceChance;
            if (enhanceSuccess)
                CraftingEnhancement.ApplyQualityEnhancement(crafted, bonus.EnhanceLevel);

            // 내구도 보너스 (항상 - 중복 방지 내장)
            if (bonus.DurabilityBonus > 0f && crafted.m_durability > 0f)
            {
                bool durApplied = !(crafted.m_customData?.ContainsKey("CraftingDurabilityBonus") ?? false);
                if (durApplied)
                    CraftingEnhancement.ApplyDurabilityBonus(crafted, bonus.DurabilityBonus);
            }

            Plugin.Log.LogDebug($"[제작 보너스] {crafted.m_shared?.m_name}: 강화={enhanceSuccess}, 내구도+{bonus.DurabilityBonus * 100:F0}%");
        }
    }

    // ============================================================
    // crafting_lv2 무기 마법부여: InventoryGui.DoCrafting Postfix
    // 25% 확률로 무기에 공격력+5 또는 공격속도+5% 부여
    // ============================================================
    [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
    public static class CraftingEnhancement_DoCrafting_Patch
    {
        private const string CRAFT_DMG_KEY = "csct_weapon_dmg";
        private const string CRAFT_SPD_KEY = "csct_weapon_spd";

        public static void Postfix(InventoryGui __instance, Player player)
        {
            try
            {
                if (player != Player.m_localPlayer) return;

                var manager = SkillTreeManager.Instance;
                if (manager == null || manager.GetSkillLevel("crafting_lv2") <= 0) return;

                var crafted = FindLastWeapon(player);
                if (crafted == null) return;

                // 중복 방지
                if (crafted.m_customData != null &&
                    (crafted.m_customData.ContainsKey(CRAFT_DMG_KEY) ||
                     crafted.m_customData.ContainsKey(CRAFT_SPD_KEY))) return;

                // 25% 확률
                if (UnityEngine.Random.Range(0f, 1f) > 0.25f) return;

                if (crafted.m_customData == null)
                    crafted.m_customData = new Dictionary<string, string>();

                bool useDmg = UnityEngine.Random.Range(0, 2) == 0;
                if (useDmg)
                {
                    crafted.m_customData[CRAFT_DMG_KEY] = "5";
                    player.Message(MessageHud.MessageType.Center, L.Get("crafting_lv2_enchant_dmg"));
                }
                else
                {
                    crafted.m_customData[CRAFT_SPD_KEY] = "5";
                    player.Message(MessageHud.MessageType.Center, L.Get("crafting_lv2_enchant_spd"));
                }
                Plugin.Log.LogDebug($"[제작 Lv2 마법부여] {crafted.m_shared.m_name}: {(useDmg ? "공격력+5" : "공격속도+5%")}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[제작 Lv2 마법부여] 오류: {ex.Message}");
            }
        }

        private static ItemDrop.ItemData FindLastWeapon(Player player)
        {
            var inv = player.GetInventory();
            if (inv == null) return null;

            // CraftingDurabilityBonus가 있으면 이 세션에 제작된 아이템 (CraftingDurability_DoCrafting_Patch에서 설정)
            foreach (var item in inv.GetAllItems())
            {
                if (item.m_customData == null || !item.m_customData.ContainsKey("CraftingDurabilityBonus")) continue;
                var t = item.m_shared.m_itemType;
                if (t == ItemDrop.ItemData.ItemType.OneHandedWeapon ||
                    t == ItemDrop.ItemData.ItemType.TwoHandedWeapon ||
                    t == ItemDrop.ItemData.ItemType.Bow)
                    return item;
            }
            return null;
        }
    }

    // ============================================================
    // crafting_lv2 무기 공격력 보너스 적용: ItemDrop.ItemData.GetDamage
    // ============================================================
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class ItemData_GetDamage_CraftWeaponBonus_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            try
            {
                if (__instance?.m_customData == null) return;
                if (__instance.m_customData.TryGetValue("csct_weapon_dmg", out string val) &&
                    float.TryParse(val, out float bonus) && bonus > 0f)
                {
                    __result.m_slash  += bonus;
                    __result.m_blunt  += bonus;
                    __result.m_pierce += bonus;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[제작 Lv2 데미지 패치] 오류: {ex.Message}");
            }
        }
    }

    // ============================================================
    // crafting_lv2 무기 마법부여 툴팁 표시
    // ============================================================
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
        new Type[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
    public static class ItemData_GetTooltip_CraftEnchant_Patch
    {
        public static void Postfix(ref string __result, ItemDrop.ItemData item)
        {
            try
            {
                if (item?.m_customData == null) return;

                if (item.m_customData.TryGetValue("csct5_type", out string t5type) &&
                    item.m_customData.TryGetValue("csct5_value", out string t5val))
                    __result += $"\n<color=#FF8C00>{L.Get("crafting_lv5_enchant_tooltip", t5type, t5val)}</color>";
            }
            catch (Exception) { }
        }
    }

    // ============================================================
    // crafting_lv5 마법부여: DoCrafting Postfix (무기/방어구 랜덤 부여)
    // 35% 확률로 무기: dmg 10~12 OR spd 10~12
    //              방어구: armor 10~12 OR hp 6~8 OR stamina 9~12
    // ============================================================
    [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
    public static class CraftingLv5Enchant_DoCrafting_Patch
    {
        private const string T5_TYPE = "csct5_type";
        private const string T5_VAL  = "csct5_value";

        public static void Postfix(Player player) { } // crafting_lv5 제거됨 - ProducerCrafting.cs 담당
    }

    // ============================================================
    // crafting_lv5 공격력 보너스: ItemDrop.ItemData.GetDamage
    // ============================================================
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class ItemData_GetDamage_Lv5WeaponBonus_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            try
            {
                if (__instance?.m_customData == null) return;
                if (__instance.m_customData.TryGetValue("csct5_type", out string t) && t == "weapon_dmg" &&
                    __instance.m_customData.TryGetValue("csct5_value", out string v) &&
                    float.TryParse(v, out float bonus))
                {
                    __result.m_slash  += bonus;
                    __result.m_blunt  += bonus;
                    __result.m_pierce += bonus;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[제작 Lv5 데미지 패치] 오류: {ex.Message}");
            }
        }
    }

    // ============================================================
    // crafting_lv5 재료 절감: DoCrafting Postfix (별도 클래스)
    // 30% 비율로 각 레시피 재료 환불
    // ============================================================
    [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
    public static class CraftingLv5MaterialRefund_DoCrafting_Patch
    {
        public static void Postfix(InventoryGui __instance, Player player) { } // crafting_lv5 제거됨 - ProducerCrafting.cs 담당
    }
}
using System;
using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 무기 아이템 툴팁 패치
    ///
    /// 제작 전문가 장인의 축복 버프 활성화 시:
    ///   - 데미지 라인 수치 반영: Attack_Tooltip_Display.cs의 CollectBonuses에서 처리
    ///   - 버프 출처 표시 라인: 이 파일에서 추가
    /// </summary>
    public static partial class SkillEffect
    {
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
            new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
        public static class ItemData_GetTooltip_WeaponBuff_Patch
        {
            [HarmonyPostfix]
            [HarmonyPriority(Priority.Low)]
            private static void Postfix(ItemDrop.ItemData item, int qualityLevel, bool crafting,
                float worldLevel, int stackOverride, ref string __result)
            {
                try
                {
                    if (item == null) return;
                    var player = Player.m_localPlayer;
                    if (player == null) return;

                    var itemType = item.m_shared.m_itemType;
                    bool isWeapon = itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon
                                 || itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon
                                 || itemType == ItemDrop.ItemData.ItemType.Bow;
                    if (!isWeapon) return;

                    // 버프 출처 표시 (데미지 라인 수치는 Attack_Tooltip_Display.cs CollectBonuses에서 처리)
                    if (ProducerSkills.IsProducerBuffActive(player))
                    {
                        float atkBonus = Producer_Config.ProducerBuff_AttackBonusValue;
                        if (atkBonus > 0f)
                            __result += $"\n<color=#FF8C00>⚒️</color><color=#4FC3F7>{L.Get("weapon_effect_producer_buff")}</color> : {L.Get("weapon_stat_atk_power")} <color=orange>+{atkBonus:F0}%</color>";
                    }

                    // 제작 축복 인챈트 출처 라인 (ProducerCrafting.cs 중복 방지 → 여기서 처리)
                    var enchantType = ProducerCrafting.GetEnchantType(item);
                    float enchantVal = ProducerCrafting.GetEnchantValue(item);
                    if (enchantVal > 0f)
                    {
                        if (enchantType == ProducerCrafting.EnchantType.WeaponDmg)
                            __result += $"\n<color=#FFD700>{L.Get("producer_enchant_weapon_dmg", $"{enchantVal:F1}")}</color>";
                        else if (enchantType == ProducerCrafting.EnchantType.WeaponSpd)
                            __result += $"\n<color=#FFD700>{L.Get("producer_enchant_weapon_spd", $"{enchantVal:F1}")}</color>";
                        else if (enchantType == ProducerCrafting.EnchantType.BowCrit)
                            __result += $"\n<color=#FFD700>{L.Get("producer_enchant_bow_crit", $"{enchantVal:F1}")}</color>";
                        else if (enchantType == ProducerCrafting.EnchantType.CrossbowReload)
                            __result += $"\n<color=#FFD700>{L.Get("producer_enchant_crossbow_reload", $"{enchantVal:F0}")}</color>";
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogDebug($"[무기 버프 툴팁] 패치 오류: {ex.Message}");
                }
            }
        }
    }
}

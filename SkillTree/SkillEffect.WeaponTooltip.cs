using System;
using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 무기 아이템 툴팁 패치
    ///
    /// 현재 지원 효과:
    ///   - 제작 전문가 장인의 축복 버프 활성화 시: 공격력 +X% 표시
    ///
    /// 핵심: GetTooltip 반환값은 로컬라이제이션 전 원시 텍스트
    ///   무기 키: $item_damage
    ///   라인 감지 시 한국어/영어 텍스트 금지, 반드시 $item_* 키 사용
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
                                 || itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon;
                    if (!isWeapon) return;

                    if (!ProducerSkills.IsProducerBuffActive(player)) return;

                    float atkBonus = Producer_Config.ProducerBuff_AttackBonusValue;
                    if (atkBonus <= 0f) return;

                    __result += $"\n<color=#FF8C00>⚒️</color><color=white>{L.Get("weapon_effect_producer_buff")}</color> : {L.Get("weapon_effect_phys_atk")} <color=orange>+{atkBonus:F0}%</color>";
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogDebug($"[무기 버프 툴팁] 패치 오류: {ex.Message}");
                }
            }
        }
    }
}

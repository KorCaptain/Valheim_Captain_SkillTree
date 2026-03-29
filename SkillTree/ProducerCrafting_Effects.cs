using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가 마법부여 신규 효과 패치 (타입 7~14)
    /// - CooldownReduce  (8): 투구 → 액티브 스킬 쿨타임 -% [ActiveSkillCooldownRegistry Prefix]
    /// - DodgeRoll       (9): 각반 → 회피 스태미나 소모 -% [GetEquipmentDodgeStaminaModifier Postfix]
    /// - MoveSpeed      (10): 각반 → 이동속도 +%           [GetJog/RunSpeedFactor Postfix]
    /// - Eitr           (11): 망토 → 에이트르 최대치 +flat  [GetTotalFoodValue Postfix]
    /// - InvWeight      (12): 악세사리 → 최대 무게 +flat    [GetMaxCarryWeight Postfix]
    /// - EitrRegen      (13): 악세사리 → 에이트르 회복속도 +% [GetEquipmentEitrRegenModifier Postfix]
    /// - JumpForce      (14): 악세사리 → 점프력 +%          [Character.Jump Prefix+Postfix]
    ///
    /// BowCrit      (6): Critical.cs GetBowCritChance 에 직접 추가
    /// CrossbowReload(7): SkillEffect.SpeedTree.cs CalculateAttackSpeedBonusInternal 에 직접 추가
    /// MaxStamina   (5): SkillEffect.StatTree.cs 에서 이미 처리
    /// </summary>
    public static class ProducerCrafting_Effects
    {
        // ============================================================
        // 1. CooldownReduce: ActiveSkillCooldownRegistry.SetCooldown Prefix
        //    투구(Helmet) 마법부여 합산 후 쿨타임 비율 감소
        //    - Player.m_localPlayer 기준 (액티브 스킬은 로컬만 발동)
        // ============================================================
        [HarmonyPatch(typeof(ActiveSkillCooldownRegistry), nameof(ActiveSkillCooldownRegistry.SetCooldown))]
        public static class Producer_Enchant_CooldownReduce_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Prefix(ref float totalDuration)
            {
                try
                {
                    var player = Player.m_localPlayer;
                    if (player == null) return;
                    float reducePct = ProducerCrafting.GetEquippedSlotEnchantTotal(
                        player, ProducerCrafting.EnchantType.CooldownReduce,
                        ItemDrop.ItemData.ItemType.Helmet);
                    if (reducePct > 0f)
                        totalDuration *= (1f - Mathf.Clamp(reducePct / 100f, 0f, 0.9f));
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 2. DodgeRoll: Character.GetEquipmentDodgeStaminaModifier Postfix
        //    각반(Legs) 마법부여 → 회피 스태미나 소모 -%
        //    UpdateDodge 내 계산식:
        //      num = usage - usage*MoveMod + usage*DodgeStamMod
        //    DodgeStamMod에 음수 추가 → 소모 감소
        // ============================================================
        [HarmonyPatch(typeof(Character), nameof(Character.GetEquipmentDodgeStaminaModifier))]
        public static class Producer_Enchant_DodgeRoll_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Character __instance, ref float __result)
            {
                try
                {
                    if (!__instance.IsPlayer()) return;
                    var player = __instance as Player;
                    if (player == null) return;
                    float bonus = ProducerCrafting.GetEquippedSlotEnchantTotal(
                        player, ProducerCrafting.EnchantType.DodgeRoll,
                        ItemDrop.ItemData.ItemType.Legs);
                    if (bonus > 0f)
                        __result -= Mathf.Clamp(bonus / 100f, 0f, 0.9f);
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 3a. MoveSpeed: Player.GetJogSpeedFactor Postfix
        //     각반(Legs) 마법부여 → 걷기/조깅 속도 +%
        // ============================================================
        [HarmonyPatch(typeof(Player), "GetJogSpeedFactor")]
        public static class Producer_Enchant_MoveSpeed_Jog_Patch
        {
            [HarmonyPriority(Priority.VeryLow)]
            public static void Postfix(Player __instance, ref float __result)
            {
                try
                {
                    float bonus = ProducerCrafting.GetEquippedSlotEnchantTotal(
                        __instance, ProducerCrafting.EnchantType.MoveSpeed,
                        ItemDrop.ItemData.ItemType.Legs);
                    if (bonus > 0f)
                        __result *= (1f + bonus / 100f);
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 3b. MoveSpeed: Player.GetRunSpeedFactor Postfix
        //     각반(Legs) 마법부여 → 달리기 속도 +%
        // ============================================================
        [HarmonyPatch(typeof(Player), "GetRunSpeedFactor")]
        public static class Producer_Enchant_MoveSpeed_Run_Patch
        {
            [HarmonyPriority(Priority.VeryLow)]
            public static void Postfix(Player __instance, ref float __result)
            {
                try
                {
                    float bonus = ProducerCrafting.GetEquippedSlotEnchantTotal(
                        __instance, ProducerCrafting.EnchantType.MoveSpeed,
                        ItemDrop.ItemData.ItemType.Legs);
                    if (bonus > 0f)
                        __result *= (1f + bonus / 100f);
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 4. Eitr (flat): Player.GetTotalFoodValue Postfix
        //    망토(Shoulder) 마법부여 → 에이트르 최대치 flat 증가
        //    StatTree Eitr 패치와 별도 (Priority.VeryLow 더 낮음)
        // ============================================================
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Producer_Enchant_Eitr_Patch
        {
            [HarmonyPriority(Priority.VeryLow)]
            public static void Postfix(Player __instance, ref float eitr)
            {
                try
                {
                    if (__instance == null) return;
                    float bonus = ProducerCrafting.GetEquippedSlotEnchantTotal(
                        __instance, ProducerCrafting.EnchantType.Eitr,
                        ItemDrop.ItemData.ItemType.Shoulder);
                    if (bonus > 0f) eitr += bonus;
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 5. InvWeight: Player.GetMaxCarryWeight Postfix
        //    악세사리(Utility/Ring/Necklace) 마법부여 → 최대 무게 flat 증가
        //    인벤토리 UI가 GetMaxCarryWeight() 호출 → 자동 반영
        // ============================================================
        [HarmonyPatch(typeof(Player), nameof(Player.GetMaxCarryWeight))]
        public static class Producer_Enchant_InvWeight_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance, ref float __result)
            {
                try
                {
                    float bonus = ProducerCrafting.GetEquippedAccessoryEnchantTotal(
                        __instance, ProducerCrafting.EnchantType.InvWeight);
                    if (bonus > 0f) __result += bonus;
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 6. EitrRegen: Player.GetEquipmentEitrRegenModifier Postfix
        //    악세사리 마법부여 → 에이트르 회복속도 +%
        //    Valheim 계산: num6 = 1f + Equipment_Modifier → eitrRegen *= num6
        //    따라서 bonus/100 추가 → 10% = 0.1 추가 → 10% 더 빠른 회복
        // ============================================================
        [HarmonyPatch(typeof(Player), nameof(Player.GetEquipmentEitrRegenModifier))]
        public static class Producer_Enchant_EitrRegen_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance, ref float __result)
            {
                try
                {
                    float bonus = ProducerCrafting.GetEquippedAccessoryEnchantTotal(
                        __instance, ProducerCrafting.EnchantType.EitrRegen);
                    if (bonus > 0f) __result += bonus / 100f;
                }
                catch (Exception) { }
            }
        }

        // ============================================================
        // 7. JumpForce: Character.Jump Prefix + Postfix
        //    악세사리 마법부여 → 점프력 +% (m_jumpForce 임시 증가)
        //    Prefix: m_jumpForce를 배율 적용 후 캐시
        //    Postfix: m_jumpForce 원복
        // ============================================================
        [HarmonyPatch(typeof(Character), nameof(Character.Jump))]
        public static class Producer_Enchant_JumpForce_Patch
        {
            private static readonly Dictionary<Character, float> _originalJumpForce
                = new Dictionary<Character, float>();

            [HarmonyPriority(Priority.Low)]
            public static void Prefix(Character __instance)
            {
                try
                {
                    if (!__instance.IsPlayer()) return;
                    var player = __instance as Player;
                    if (player == null) return;
                    float bonus = ProducerCrafting.GetEquippedAccessoryEnchantTotal(
                        player, ProducerCrafting.EnchantType.JumpForce);
                    if (bonus <= 0f) return;
                    _originalJumpForce[__instance] = __instance.m_jumpForce;
                    __instance.m_jumpForce *= (1f + bonus / 100f);
                }
                catch (Exception) { }
            }

            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Character __instance)
            {
                try
                {
                    if (_originalJumpForce.TryGetValue(__instance, out float orig))
                    {
                        __instance.m_jumpForce = orig;
                        _originalJumpForce.Remove(__instance);
                    }
                }
                catch (Exception) { }
            }
        }
    }
}

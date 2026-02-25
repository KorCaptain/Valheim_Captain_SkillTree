using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 방어 트리 전용 스킬 효과 (구르기/스태미나/방패)
    ///
    /// 구현 스킬:
    /// - defense_Step3_agile (회피단련): 구르기 무적시간 +20%
    /// - defense_Step5_stamina (기민함): 구르기 스태미나 -12%
    /// - defense_Step3_shield (방패훈련): 방패 방어력 +100
    /// - defense_Step5_parry (막기달인): 방패 방어력 +100, 패링 지속시간 +1초
    /// </summary>
    public static class SkillEffect_DefenseTree
    {
        /// <summary>
        /// 방패 방어력(블럭 파워) 보너스 (defense_Step3_shield, defense_Step5_parry)
        /// Rule 17-2: ItemDrop.ItemData.GetBlockPower 패치 방식
        /// </summary>
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetBlockPower), new Type[] { typeof(float) })]
        public static class ItemData_GetBlockPower_DefenseTree_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(ItemDrop.ItemData __instance, float skillFactor, ref float __result)
            {
                try
                {
                    // 1. 방패가 아니면 무시
                    if (__instance.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield)
                        return;

                    var player = Player.m_localPlayer;
                    if (player == null) return;

                    // 2. 현재 장착한 방패인지 확인 (Traverse 사용)
                    var leftItem = Traverse.Create(player).Field("m_leftItem").GetValue<ItemDrop.ItemData>();
                    if (leftItem != __instance)
                        return;

                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    float blockPowerBonus = 0f;

                    // defense_Step3_shield: 방패훈련 (+100)
                    if (manager.GetSkillLevel("defense_Step3_shield") > 0)
                    {
                        blockPowerBonus += Defense_Config.ShieldTrainingBlockPowerBonusValue;
                    }

                    // defense_Step5_parry: 막기달인 (+100)
                    if (manager.GetSkillLevel("defense_Step5_parry") > 0)
                    {
                        blockPowerBonus += Defense_Config.ParryMasterBlockPowerBonusValue;
                    }

                    if (blockPowerBonus > 0)
                    {
                        __result += blockPowerBonus;
                    }

                    // defense_Step4_tanker: 바위피부 - 방패 방어력 +X%
                    if (manager.GetSkillLevel("defense_Step4_tanker") > 0)
                    {
                        float multiplier = Defense_Config.TankerArmorBonusValue / 100f;
                        __result += __result * multiplier;
                        Plugin.Log.LogDebug($"[방어 트리] 바위피부 방패 방어력 +{Defense_Config.TankerArmorBonusValue}% 적용 (총: {__result})");
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[방어 트리] GetBlockPower 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 패링 지속시간 보너스 (defense_Step5_parry)
        /// Character.BlockAttack 메서드 패치 (막기 성공 시)
        /// </summary>
        [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
        public static class Character_BlockAttack_DefenseTree_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Character __instance, bool __result)
            {
                try
                {
                    // 막기에 실패했으면 무시
                    if (!__result) return;

                    // 로컬 플레이어가 아니면 무시
                    if (__instance != Player.m_localPlayer) return;

                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    // defense_Step5_parry: 막기달인
                    if (manager.GetSkillLevel("defense_Step5_parry") > 0)
                    {
                        float bonusDuration = Defense_Config.ParryMasterParryDurationBonusValue;

                        // m_perfectBlockBonus 필드 접근 (Traverse 사용)
                        var traverse = Traverse.Create(__instance);
                        float currentTimer = traverse.Field("m_perfectBlockBonus").GetValue<float>();

                        // 패링 성공 시에만 적용 (타이머가 0보다 클 때)
                        if (currentTimer > 0)
                        {
                            float newTimer = currentTimer + bonusDuration;
                            traverse.Field("m_perfectBlockBonus").SetValue(newTimer);
                            Plugin.Log.LogDebug($"[막기달인] 패링 지속시간: {currentTimer:F2}초 → {newTimer:F2}초 (+{bonusDuration}초)");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[방어 트리] BlockAttack 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 구르기 스태미나 감소 및 무적시간 증가
        /// </summary>
        [HarmonyPatch(typeof(Player), "Dodge")]
        public static class Player_Dodge_DefenseTree_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Prefix(Player __instance, ref Vector3 dodgeDir)
            {
                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                // 방어 루트 노드가 없으면 무시
                if (manager.GetSkillLevel("defense_root") <= 0) return;

                // 기민함 스킬: 구르기 스태미나 -12%
                if (manager.GetSkillLevel("defense_Step5_stamina") > 0)
                {
                    float reduction = Defense_Config.StaminaRollStaminaReductionValue / 100f;

                    // Character의 m_dodgeStaminaUsage 필드 접근 (Traverse 사용)
                    var traverse = Traverse.Create(__instance);
                    float originalStamina = traverse.Field("m_dodgeStaminaUsage").GetValue<float>();
                    float reducedStamina = originalStamina * (1f - reduction);
                    traverse.Field("m_dodgeStaminaUsage").SetValue(reducedStamina);

                    Plugin.Log.LogDebug($"[기민함] 구르기 스태미나 감소: {originalStamina:F1} → {reducedStamina:F1} (-{reduction * 100f}%)");
                }
            }

            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance)
            {
                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                // 방어 루트 노드가 없으면 무시
                if (manager.GetSkillLevel("defense_root") <= 0) return;

                // 회피단련 스킬: 무적시간 +20%
                if (manager.GetSkillLevel("defense_Step3_agile") > 0)
                {
                    float bonus = Defense_Config.AgileInvincibilityBonusValue / 100f;

                    // Character의 m_dodgeInvincibilityTimer 필드 접근
                    var traverse = Traverse.Create(__instance);
                    float currentTimer = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
                    float newTimer = currentTimer * (1f + bonus);
                    traverse.Field("m_dodgeInvincibilityTimer").SetValue(newTimer);

                    Plugin.Log.LogDebug($"[회피단련] 무적시간 증가: {currentTimer:F2}초 → {newTimer:F2}초 (+{bonus * 100f}%)");
                }

                // === 단검 전문가: 회피 숙련 스킬 ===
                // knife_step2_evasion: 단검 착용 시 구르기 무적시간 보너스
                if (manager.GetSkillLevel("knife_expert") > 0 &&
                    manager.GetSkillLevel("knife_step2_evasion") > 0)
                {
                    // 단검 착용 확인
                    if (__instance is Player player && SkillEffect.IsUsingDagger(player))
                    {
                        float bonus = Knife_Config.KnifeEvasionBonusValue / 100f;

                        // Character의 m_dodgeInvincibilityTimer 필드 접근
                        var traverse = Traverse.Create(__instance);
                        float currentTimer = traverse.Field("m_dodgeInvincibilityTimer").GetValue<float>();
                        float newTimer = currentTimer * (1f + bonus);
                        traverse.Field("m_dodgeInvincibilityTimer").SetValue(newTimer);

                        Plugin.Log.LogDebug($"[회피 숙련] 단검 무적시간 증가: {currentTimer:F2}초 → {newTimer:F2}초 (+{bonus * 100f}%)");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 요툰의 방패: 방패 블럭 스태미나 소모 -25%
    /// SEMan.ModifyBlockStaminaUsage 패치
    /// </summary>
    [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyBlockStaminaUsage))]
    public static class SEMan_ModifyBlockStaminaUsage_JotunnShield_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ref float staminaUse)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;

                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                // defense_Step6_true: 요툰의 방패
                if (manager.GetSkillLevel("defense_Step6_true") > 0)
                {
                    float reduction = Defense_Config.JotunnShieldBlockStaminaReductionValue / 100f;
                    float originalUse = staminaUse;
                    staminaUse = staminaUse - (reduction * staminaUse);

                    Plugin.Log.LogDebug($"[요툰의 방패] 블럭 스태미나 감소: {originalUse:F1} → {staminaUse:F1} (-{reduction * 100f}%)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[방어 트리] ModifyBlockStaminaUsage 패치 오류: {ex.Message}");
            }
        }
    }

}

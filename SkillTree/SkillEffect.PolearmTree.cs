using EpicMMOSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폴암 전문가 트리 전용 효과 시스템
    /// 공격 범위 보너스, 2연속 공격, G키 액티브 스킬 구현
    /// </summary>
    public static partial class SkillEffect
    {
        // === 폴암 트리 상태 추적 변수들 ===

        // 광역 강타 2연속 공격 추적
        public static Dictionary<Player, int> polearmAreaComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> polearmAreaLastHitTime = new Dictionary<Player, float>();

        // 장창의 제왕 액티브 스킬 추적
        public static Dictionary<Player, float> polearmKingLastUseTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> polearmKingBuffEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> polearmKingBuffActive = new Dictionary<Player, bool>();

        /// <summary>
        /// 폴암 공격 범위 보너스 계산
        /// polearm_expert (15%), polearm_step4_moon (15%)
        /// </summary>
        public static float GetTotalPolearmRangeBonus(Player player)
        {
            if (player == null) return 0f;

            var weapon = player.GetCurrentWeapon();
            if (weapon == null || !IsUsingPolearm(player)) return 0f;

            float bonus = 0f;

            // 폴암 전문가 (polearm_expert) - 공격 범위 +15%
            if (HasSkill("polearm_expert"))
            {
                bonus += SkillTreeConfig.PolearmExpertRangeBonusValue;
                Plugin.Log.LogDebug($"[폴암 전문가] 공격 범위 +{SkillTreeConfig.PolearmExpertRangeBonusValue}%");
            }

            // 반달 베기 (polearm_step4_moon) - 공격 범위 +15%
            if (HasSkill("polearm_step4_moon"))
            {
                bonus += SkillTreeConfig.PolearmStep4MoonRangeBonusValue;
                Plugin.Log.LogDebug($"[반달 베기] 공격 범위 +{SkillTreeConfig.PolearmStep4MoonRangeBonusValue}%");
            }

            return bonus;
        }

        /// <summary>
        /// 광역 강타 2연속 공격 체크 (polearm_step3_area)
        /// 2연속 공격 시 공격력 +25% (5초 지속)
        /// </summary>
        public static void CheckPolearmAreaCombo(Player player)
        {
            if (!HasSkill("polearm_step3_area")) return;

            float now = Time.time;
            if (!polearmAreaComboCount.ContainsKey(player))
                polearmAreaComboCount[player] = 0;

            // 3초 내 연속 공격 체크
            if (polearmAreaLastHitTime.ContainsKey(player) && now - polearmAreaLastHitTime[player] < 3f)
            {
                polearmAreaComboCount[player]++;
            }
            else
            {
                polearmAreaComboCount[player] = 1;
            }
            polearmAreaLastHitTime[player] = now;

            // 2연속 공격 달성 시
            if (polearmAreaComboCount[player] >= 2)
            {
                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                DrawFloatingText(player, $"⚔️ 광역 강타! 2연속 공격력 +{SkillTreeConfig.PolearmStep3AreaComboBonusValue}%");

                Plugin.Log.LogInfo($"[광역 강타] 2연속 공격 달성 - 공격력 +{SkillTreeConfig.PolearmStep3AreaComboBonusValue}% 보너스 적용");

                // 다음 공격에 보너스 적용 설정
                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1f + (SkillTreeConfig.PolearmStep3AreaComboBonusValue / 100f);
                nextAttackExpiry[player] = now + SkillTreeConfig.PolearmStep3AreaComboDurationValue;

                // 콤보 카운트 리셋
                polearmAreaComboCount[player] = 0;
            }
        }

        /// <summary>
        /// 장창의 제왕 액티브 스킬 사용 (G키)
        /// 체력 50% 이상인 적에게 추가 피해 +50%
        /// </summary>
        public static bool UsePolearmKingSkill(Player player)
        {
            if (player == null || !HasSkill("polearm_step5_king")) return false;

            float now = Time.time;

            // 쿨타임 체크
            if (polearmKingLastUseTime.ContainsKey(player))
            {
                float timeSinceLastUse = now - polearmKingLastUseTime[player];
                float cooldown = SkillTreeConfig.PolearmStep5KingCooldownValue;

                if (timeSinceLastUse < cooldown)
                {
                    float remainingCooldown = cooldown - timeSinceLastUse;
                    DrawFloatingText(player, $"⏳ 쿨타임: {remainingCooldown:F1}초 남음");
                    return false;
                }
            }

            // 스태미나 체크
            float staminaCost = player.GetMaxStamina() * (SkillTreeConfig.PolearmStep5KingStaminaCostValue / 100f);
            if (player.GetStamina() < staminaCost)
            {
                DrawFloatingText(player, "❌ 스태미나 부족!");
                return false;
            }

            // 폴암 착용 확인
            if (!IsUsingPolearm(player))
            {
                DrawFloatingText(player, "❌ 폴암을 착용해야 합니다!");
                return false;
            }

            // 스태미나 소모
            player.UseStamina(staminaCost);

            // 버프 활성화
            polearmKingBuffActive[player] = true;
            polearmKingBuffEndTime[player] = now + SkillTreeConfig.PolearmStep5KingDurationValue;
            polearmKingLastUseTime[player] = now;

            // VFX/SFX 재생 (액티브 스킬이므로 허용)
            VFXManager.PlayVFXMultiplayer(
                "fx_guardstone_activate",
                "",  // 사운드 없음
                player.transform.position,
                player.transform.rotation
            );

            DrawFloatingText(player, $"👑 장창의 제왕! ({SkillTreeConfig.PolearmStep5KingDurationValue}초)");
            Plugin.Log.LogInfo($"[장창의 제왕] 스킬 사용 - 지속시간: {SkillTreeConfig.PolearmStep5KingDurationValue}초");

            return true;
        }

        /// <summary>
        /// 장창의 제왕 버프가 활성화되어 있는지 확인
        /// </summary>
        public static bool IsPolearmKingBuffActive(Player player)
        {
            if (player == null || !HasSkill("polearm_step5_king")) return false;

            if (!polearmKingBuffActive.ContainsKey(player)) return false;

            if (!polearmKingBuffActive[player]) return false;

            float now = Time.time;
            if (polearmKingBuffEndTime.ContainsKey(player) && now < polearmKingBuffEndTime[player])
            {
                return true;
            }

            // 버프 만료
            polearmKingBuffActive[player] = false;
            return false;
        }

        /// <summary>
        /// 장창의 제왕 데미지 보너스 적용
        /// 체력 50% 이상인 적에게만 적용
        /// </summary>
        public static float GetPolearmKingDamageBonus(Character target)
        {
            if (target == null || target.IsDead()) return 0f;

            float healthPercent = (target.GetHealth() / target.GetMaxHealth()) * 100f;
            float threshold = SkillTreeConfig.PolearmStep5KingHealthThresholdValue;

            if (healthPercent >= threshold)
            {
                float bonus = SkillTreeConfig.PolearmStep5KingDamageBonusValue;
                Plugin.Log.LogDebug($"[장창의 제왕] 대상 체력 {healthPercent:F1}% >= {threshold}% - 추가 피해 +{bonus}%");
                return bonus;
            }

            return 0f;
        }

        /// <summary>
        /// 반달 베기 스태미나 감소 (polearm_step4_moon)
        /// 공격 스태미나 소모 -15%
        /// </summary>
        public static float GetPolearmStaminaReduction()
        {
            if (HasSkill("polearm_step4_moon"))
            {
                return SkillTreeConfig.PolearmStep4MoonStaminaReductionValue;
            }

            return 0f;
        }
    }

    /// <summary>
    /// 폴암 공격 범위 보너스 패치
    /// Attack.Start() Postfix - m_attackRange 필드 직접 수정
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class Attack_Start_PolearmRange_Patch
    {
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 폴암 공격 범위 보너스 계산
                float rangeBonus = SkillEffect.GetTotalPolearmRangeBonus(player);

                if (rangeBonus > 0f)
                {
                    float originalRange = __instance.m_attackRange;
                    __instance.m_attackRange *= (1f + (rangeBonus / 100f));

                    Plugin.Log.LogDebug($"[폴암 공격 범위] {originalRange:F2} → {__instance.m_attackRange:F2} (+{rangeBonus}%)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmRange_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 폴암 스태미나 감소 패치
    /// Attack.Start() Postfix - m_attackStamina 필드 직접 수정
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class Attack_Start_PolearmStamina_Patch
    {
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 반달 베기 스태미나 감소
                float staminaReduction = SkillEffect.GetPolearmStaminaReduction();

                if (staminaReduction > 0f)
                {
                    float originalStamina = __instance.m_attackStamina;
                    __instance.m_attackStamina *= (1f - (staminaReduction / 100f));

                    Plugin.Log.LogDebug($"[반달 베기] 스태미나 소모 {originalStamina:F1} → {__instance.m_attackStamina:F1} (-{staminaReduction}%)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmStamina_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 장창의 제왕 데미지 보너스 패치
    /// Character.Damage() Postfix에서 추가 데미지 적용
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_PolearmKing_Patch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null) return;

                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 장창의 제왕 버프 활성화 확인
                if (!SkillEffect.IsPolearmKingBuffActive(player)) return;

                // 대상 체력 체크 및 보너스 적용
                float damageBonus = SkillEffect.GetPolearmKingDamageBonus(__instance);

                if (damageBonus > 0f)
                {
                    // 모든 물리 데미지에 보너스 적용
                    hit.m_damage.m_blunt *= (1f + (damageBonus / 100f));
                    hit.m_damage.m_slash *= (1f + (damageBonus / 100f));
                    hit.m_damage.m_pierce *= (1f + (damageBonus / 100f));
                    hit.m_damage.m_chop *= (1f + (damageBonus / 100f));
                    hit.m_damage.m_pickaxe *= (1f + (damageBonus / 100f));

                    Plugin.Log.LogInfo($"[장창의 제왕] 추가 피해 +{damageBonus}% 적용");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_PolearmKing_Patch] 오류: {ex.Message}");
            }
        }
    }
}

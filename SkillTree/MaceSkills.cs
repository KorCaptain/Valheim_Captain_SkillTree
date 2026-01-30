using HarmonyLib;
using UnityEngine;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기 스킬 효과 구현 (직접 패치 방식 - Tier 2)
    /// WeaponHelper, SkillBonusCalculator 사용하여 중복 코드 제거
    /// </summary>
    public static class MaceSkills
    {
        /// <summary>
        /// 모든 둔기 데미지 보너스 계산
        /// Tier 0 (전문가 +5%) + Tier 1 (+10%) + Tier 5 DPS (+20%)
        /// </summary>
        public static float GetTotalMaceDamageBonus(string skillId = "")
        {
            return SkillBonusCalculator.CalculateTotal(
                ("mace_expert_damage", () => Mace_Config.MaceExpertDamageBonusValue),
                ("mace_Step1_damage", () => Mace_Config.MaceStep1DamageBonusValue),
                ("mace_Step5_dps", () => Mace_Config.MaceStep5DpsDamageBonusValue)
            );
        }

        /// <summary>
        /// 기절 확률 계산
        /// Tier 0 (전문가 20%) + Tier 2 (+15%)
        /// </summary>
        public static float GetTotalStunChance(string skillId = "")
        {
            return SkillBonusCalculator.CalculateTotal(
                ("mace_expert_damage", () => Mace_Config.MaceExpertStunChanceValue),
                ("mace_Step2_stun_boost", () => Mace_Config.MaceStep2StunChanceBonusValue)
            );
        }

        /// <summary>
        /// 기절 지속시간 계산
        /// Tier 0 (전문가 0.5초) + Tier 2 (+0.5초)
        /// </summary>
        public static float GetTotalStunDuration(string skillId = "")
        {
            return SkillBonusCalculator.CalculateTotal(
                ("mace_expert_damage", () => Mace_Config.MaceExpertStunDurationValue),
                ("mace_Step2_stun_boost", () => Mace_Config.MaceStep2StunDurationBonusValue)
            );
        }

        /// <summary>
        /// 방어력 비율 보너스 계산
        /// Tier 6 Grandmaster (+20%)
        /// </summary>
        public static float GetTotalArmorBonusPercent()
        {
            return SkillBonusCalculator.GetIfActive(
                "mace_Step6_grandmaster",
                () => Mace_Config.MaceStep6ArmorBonusValue
            );
        }

        /// <summary>
        /// 방어력 고정값 보너스 계산
        /// Tier 3 Guard (+3)
        /// </summary>
        public static float GetTotalArmorBonusFixed()
        {
            return SkillBonusCalculator.GetIfActive(
                "mace_Step3_branch_guard",
                () => Mace_Config.MaceStep3GuardArmorBonusValue
            );
        }

        /// <summary>
        /// 공격속도 보너스 반환 (Plugin.cs의 시스템 활용)
        /// Tier 5 DPS: +10%
        /// </summary>
        public static float GetAttackSpeedBonus()
        {
            return SkillBonusCalculator.GetIfActive(
                "mace_Step5_dps",
                () => Mace_Config.MaceStep5DpsAttackSpeedBonusValue
            );
        }
    }

    // ===== Harmony 패치 =====

    /// <summary>
    /// 둔기 데미지 증가 패치 (공격 전문가 패턴 참조)
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_DamagePatch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                // 공격자가 플레이어인지 확인
                if (hit.GetAttacker() is not Player attacker) return;

                // 둔기 무기 사용 여부 확인 (WeaponHelper 사용)
                if (!WeaponHelper.IsUsingMace(attacker)) return;

                // 총 데미지 배율 계산
                float damageBonus = MaceSkills.GetTotalMaceDamageBonus();
                if (damageBonus > 0f)
                {
                    float totalDamageMultiplier = 1f + damageBonus / 100f;

                    // blunt 데미지 타입에만 배율 적용
                    hit.m_damage.m_blunt *= totalDamageMultiplier;

                    Plugin.Log.LogDebug($"[둔기 데미지] 보너스 +{damageBonus}% 적용 (blunt, 배율: {totalDamageMultiplier:F2})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 데미지 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 기절 효과 패치
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_StunPatch
    {
        static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                // 공격자가 플레이어인지 확인
                if (hit.GetAttacker() is not Player attacker) return;

                // 둔기 무기 사용 여부 확인 (WeaponHelper 사용)
                if (!WeaponHelper.IsUsingMace(attacker)) return;

                // 기절 확률 및 지속시간 계산
                float stunChance = MaceSkills.GetTotalStunChance();
                float stunDuration = MaceSkills.GetTotalStunDuration();

                if (stunChance <= 0f || stunDuration <= 0f) return;

                // 확률 체크
                if (UnityEngine.Random.Range(0f, 100f) > stunChance) return;

                // 기절 효과 적용
                if (__instance != null && !__instance.IsDead())
                {
                    __instance.Stagger(Vector3.zero);
                    Plugin.Log.LogDebug($"[둔기 기절] 확률 {stunChance}% 발동! 지속시간: {stunDuration}초");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 기절 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 노크백 효과 패치
    /// Tier 4: 30% 확률로 노크백
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_KnockbackPatch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                // 공격자가 플레이어인지 확인
                if (hit.GetAttacker() is not Player attacker) return;

                // 둔기 무기 사용 여부 확인 (WeaponHelper 사용)
                if (!WeaponHelper.IsUsingMace(attacker)) return;

                // Tier 4: 밀어내기 (SkillBonusCalculator 사용)
                if (SkillBonusCalculator.IsSkillActive("mace_Step4_push"))
                {
                    float knockbackChance = Mace_Config.MaceStep4KnockbackChanceValue;

                    // 확률 체크
                    if (UnityEngine.Random.Range(0f, 100f) <= knockbackChance)
                    {
                        // 노크백 강도 증가
                        hit.m_pushForce *= 2.0f;
                        Plugin.Log.LogDebug($"[둔기 밀어내기] 확률 {knockbackChance}% 발동! 노크백 강도 2배");
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 노크백 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 방어력 증가 패치 (방어 전문가 패턴 참조)
    /// Tier 3 Guard (+15%) + Tier 6 Grandmaster (+20%)
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
    public static class MaceSkills_ArmorPatch
    {
        static void Postfix(Character __instance, ref float __result)
        {
            try
            {
                // 플레이어만 처리
                if (__instance is not Player player) return;

                // 둔기 무기 사용 여부 확인 (WeaponHelper 사용)
                if (!WeaponHelper.IsUsingMace(player)) return;

                // 방어력 비율 보너스 계산
                float armorBonusPercent = MaceSkills.GetTotalArmorBonusPercent();
                if (armorBonusPercent > 0f)
                {
                    float bonusArmor = __result * (armorBonusPercent / 100f);
                    __result += bonusArmor;
                    Plugin.Log.LogDebug($"[둔기 방어력] +{armorBonusPercent}% 적용: {bonusArmor:F1} 추가");
                }

                // 방어력 고정값 보너스 계산
                float armorBonusFixed = MaceSkills.GetTotalArmorBonusFixed();
                if (armorBonusFixed > 0f)
                {
                    __result += armorBonusFixed;
                    Plugin.Log.LogDebug($"[둔기 방어력] +{armorBonusFixed} 고정 추가");
                }

                Plugin.Log.LogDebug($"[둔기 방어력] 최종 방어력: {__result:F1}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 방어력 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 체력 증가 패치 (SkillEffect.cs 패턴 참조)
    /// Tier 5 Tank: 체력 +25%
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.GetMaxHealth))]
    public static class MaceSkills_HealthPatch
    {
        static void Postfix(Character __instance, ref float __result)
        {
            try
            {
                // 플레이어만 처리
                if (__instance is not Player player) return;

                // 둔기 무기 사용 여부 확인 (WeaponHelper 사용)
                if (!WeaponHelper.IsUsingMace(player)) return;

                // Tier 5 Tank: 체력 보너스 (+25%) (SkillBonusCalculator 사용)
                if (SkillBonusCalculator.IsSkillActive("mace_Step5_tank"))
                {
                    float healthBonus = Mace_Config.MaceStep5TankHealthBonusValue;
                    float baseHealth = __result;
                    float bonusHealth = baseHealth * (healthBonus / 100f);
                    __result += bonusHealth;

                    Plugin.Log.LogDebug($"[둔기 탱커] 체력 +{healthBonus}%: {baseHealth:F0} → {__result:F0} (+{bonusHealth:F0})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 체력 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 받는 데미지 감소 패치
    /// Tier 5 Tank: 받는 데미지 -10%
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_DamageReductionPatch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                // 피격자가 플레이어인지 확인
                if (__instance is not Player player) return;

                // 둔기 무기 사용 여부 확인 (WeaponHelper 사용)
                if (!WeaponHelper.IsUsingMace(player)) return;

                // Tier 5 Tank: 받는 데미지 감소 (-10%) (SkillBonusCalculator 사용)
                if (SkillBonusCalculator.IsSkillActive("mace_Step5_tank"))
                {
                    float damageReduction = Mace_Config.MaceStep5TankDamageReductionValue;
                    float reductionMultiplier = 1f - (damageReduction / 100f);

                    // 모든 데미지 타입 감소
                    hit.m_damage.m_blunt *= reductionMultiplier;
                    hit.m_damage.m_pierce *= reductionMultiplier;
                    hit.m_damage.m_slash *= reductionMultiplier;
                    hit.m_damage.m_chop *= reductionMultiplier;
                    hit.m_damage.m_pickaxe *= reductionMultiplier;
                    hit.m_damage.m_fire *= reductionMultiplier;
                    hit.m_damage.m_frost *= reductionMultiplier;
                    hit.m_damage.m_lightning *= reductionMultiplier;
                    hit.m_damage.m_poison *= reductionMultiplier;
                    hit.m_damage.m_spirit *= reductionMultiplier;

                    Plugin.Log.LogDebug($"[둔기 탱커] 받는 데미지 -{damageReduction}% 감소 (배율: {reductionMultiplier:F2})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 데미지 감소 패치] 오류: {ex.Message}");
            }
        }
    }
}

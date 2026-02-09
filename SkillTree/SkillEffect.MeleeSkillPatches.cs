using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    // === 근접 스킬 Harmony 패치들 ===

    /// <summary>
    /// 단검 공격 시 연속 공격 및 백스탭 효과
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MeleeSkills_Dagger_Attack_Patch
    {
        static bool Prepare()
        {
            Plugin.Log.LogDebug("[안전 장치] 단검 공격 패치 준비 완료");
            return true;
        }

        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingDagger(player)) return;

                // 연속 공격 카운트 업데이트
                SkillEffect.UpdateConsecutiveHits(player);

                // 백스탭 확인
                bool isBackstab = SkillEffect.IsBackstab(player, __instance);

                // 단검 전문가 - 백스탭 공격 시 데미지 보너스
                SkillEffect.CheckKnifeExpertBackstab(player, __instance, hit);

                // 전투 숙련 - 전투 중 공격력 증가
                SkillEffect.CheckKnifeCombatDamage(player, hit);

                // 암살자의 심장 - G키 액티브 스킬 치명타 효과
                SkillEffect.ApplyKnifeAssassinHeartCrit(player, hit);

                // 연속 공격 치명타 (3회 연속 공격 시)
                var consecutiveHits = SkillEffect.consecutiveHits.TryGetValue(player, out var hits) ? hits : 0;
                if (consecutiveHits >= 3)
                {
                    if (SkillEffect.HasSkill("knife_step5_crit_rate"))
                    {
                        hit.m_damage.m_slash *= 1.3f;
                        hit.m_damage.m_pierce *= 1.3f;
                        SkillEffect.PlaySkillEffect(player, "knife_crit1", hit.m_point);
                        SkillEffect.DrawFloatingText(player, "⚡ 연속 공격!");
                    }
                }

                // 치명적 일격 (5회 연속 공격 시)
                if (consecutiveHits >= 5 && SkillEffect.HasSkill("knife_step8_assassination"))
                {
                    hit.m_damage.m_slash *= 2.0f;
                    hit.m_damage.m_pierce *= 2.0f;
                    SkillEffect.PlaySkillEffect(player, "knife_crit2", hit.m_point);
                    SkillEffect.DrawFloatingText(player, "💀 치명적 일격!");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 단검 공격 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 검 공격 시 연계 및 방어 효과
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MeleeSkills_Sword_Attack_Patch
    {
        static bool Prepare()
        {
            Plugin.Log.LogDebug("[안전 장치] 검 공격 패치 준비 완료");
            return true;
        }

        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingSword(player)) return;

                // 검술 연계 업데이트
                SkillEffect.UpdateSwordCombo(player);

                // 검 전문가 2연속 공격 체크
                SkillEffect.CheckSwordExpertCombo(player);

                // 연속베기 3연속 공격 체크
                SkillEffect.CheckSwordComboSlash(player);

                // 다음 공격 부스트 적용
                if (SkillEffect.nextAttackBoosted.TryGetValue(player, out var boosted) && boosted)
                {
                    if (Time.time < SkillEffect.nextAttackExpiry[player])
                    {
                        float multiplier = SkillEffect.nextAttackMultiplier[player];
                        hit.m_damage.m_slash *= multiplier;
                        hit.m_damage.m_pierce *= multiplier;
                        SkillEffect.nextAttackBoosted[player] = false;
                        SkillEffect.PlaySkillEffect(player, "sword_power", hit.m_point);
                        SkillEffect.DrawFloatingText(player, $"⚔️ 강화된 일격! (+{(multiplier - 1) * 100:F0}%)");
                    }
                    else
                    {
                        SkillEffect.nextAttackBoosted[player] = false;
                    }
                }

                // 양손검 추가 데미지
                if (SkillEffect.IsUsingTwoHandedSword(player) && SkillEffect.HasSkill("sword_power"))
                {
                    hit.m_damage.m_slash *= 1.2f;
                    hit.m_damage.m_pierce *= 1.2f;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 검 공격 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 창 공격 시 특수 효과들
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MeleeSkills_Spear_Attack_Patch
    {
        static bool Prepare()
        {
            Plugin.Log.LogDebug("[안전 장치] 창 공격 패치 준비 완료");
            return true;
        }

        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null) return;

                // 창 스킬 처리
                if (SkillEffect.IsUsingSpear(player))
                {
                    ProcessSpearAttack(player, __instance, hit);
                }

                // 폴암 스킬 처리
                if (SkillEffect.IsUsingPolearm(player))
                {
                    ProcessPolearmAttack(player, hit);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 창/폴암 공격 패치 오류: {ex.Message}");
            }
        }

        private static void ProcessSpearAttack(Player player, Character target, HitData hit)
        {
            // 번개 충격 처리 중이면 스킵 (재진입 방지 - 무한 루프 방지)
            if (SkillEffect.IsProcessingSpearLightningDamage()) return;

            // 창 전문가 2연속 공격 체크
            SkillEffect.CheckSpearExpertCombo(player);

            // 창 전문가 공격력 보너스
            if (SkillEffect.spearExpertComboCount.TryGetValue(player, out int comboCount) && comboCount >= 2)
            {
                float bonusPercent = SkillTreeConfig.SpearStep1DamageBonusValue / 100f;
                hit.m_damage.m_pierce *= (1f + bonusPercent);
                SkillEffect.DrawFloatingText(player, $"🔥 창 전문가 공격력! (+{SkillTreeConfig.SpearStep1DamageBonusValue}%)");
            }

            // 회피 찌르기 체크
            bool isAfterRoll = SkillEffect.spearAfterRoll.TryGetValue(player, out bool afterRoll) && afterRoll &&
                              Time.time - SkillEffect.spearRollTime[player] < 2f;

            // 급소 찌르기
            if (SkillEffect.HasSkill("spear_Step1_crit"))
            {
                float bonusPercent = SkillTreeConfig.SpearStep2CritDamageBonusValue / 100f;
                hit.m_damage.m_pierce *= (1f + bonusPercent);
                SkillEffect.PlaySkillEffect(player, "spear_Step1_crit", hit.m_point);
            }

            // 회피 찌르기
            if (isAfterRoll && SkillEffect.HasSkill("spear_Step2_evasion"))
            {
                float bonusPercent = SkillTreeConfig.SpearStep3EvasionDamageBonusValue / 100f;
                hit.m_damage.m_pierce *= (1f + bonusPercent);
                SkillEffect.spearAfterRoll[player] = false;
                SkillEffect.PlaySkillEffect(player, "spear_Step2_evasion", hit.m_point);
                SkillEffect.DrawFloatingText(player, $"🎯 회피 찌르기! (+{SkillTreeConfig.SpearStep3EvasionDamageBonusValue}%)");
            }

            // 삼연창 효과
            SkillEffect.CheckSpearTripleCombo(player);
            if (SkillEffect.spearTripleComboActive.TryGetValue(player, out bool tripleActive) && tripleActive)
            {
                float bonusPercent = SkillTreeConfig.SpearStep5TripleDamageBonusValue / 100f;
                hit.m_damage.m_pierce *= (1f + bonusPercent);
                SkillEffect.spearTripleComboActive[player] = false;
                SkillEffect.PlaySkillEffect(player, "spear_Step4_triple", hit.m_point);
                SkillEffect.DrawFloatingText(player, $"🔥 삼연창! (+{SkillTreeConfig.SpearStep5TripleDamageBonusValue}%)");
            }

            // 연격창
            SkillEffect.CheckDoubleAttack(player, target, hit);

            // 투창 전문가
            SkillEffect.ApplySpearThrowExpertDamage(hit);

            // 꿰뚫는 창 (번개 충격)
            SkillEffect.CheckSpearPenetrateCombo(player, target, hit);
        }

        private static void ProcessPolearmAttack(Player player, HitData hit)
        {
            // 광역 강타 2연속 공격 체크
            SkillEffect.CheckPolearmAreaCombo(player);

            // 다음 공격 부스트 적용
            if (SkillEffect.nextAttackBoosted.TryGetValue(player, out var boosted) && boosted)
            {
                if (Time.time < SkillEffect.nextAttackExpiry[player])
                {
                    float multiplier = SkillEffect.nextAttackMultiplier[player];
                    hit.m_damage.m_pierce *= multiplier;
                    hit.m_damage.m_slash *= multiplier;
                    hit.m_damage.m_blunt *= multiplier;
                    SkillEffect.nextAttackBoosted[player] = false;
                    Plugin.Log.LogDebug($"[광역 강타] 강화된 일격 적용 - {(multiplier - 1) * 100:F0}% 보너스");
                }
                else
                {
                    SkillEffect.nextAttackBoosted[player] = false;
                }
            }

            // 폴암강화
            if (SkillEffect.HasSkill("polearm_step4_charge"))
            {
                float bonusValue = SkillTreeConfig.PolearmStep4ChargeDamageBonusValue;
                hit.m_damage.m_pierce += bonusValue;
                Plugin.Log.LogDebug($"[폴암강화] 무기 공격력 보너스 적용 (pierce): +{bonusValue}");
            }
        }
    }

    /// <summary>
    /// 구르기 시 창/단검 스킬 상태 추적
    /// </summary>
    [HarmonyPatch(typeof(Player), "Dodge")]
    public static class MeleeSkill_Dodge_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player __instance, Vector3 dodgeDir)
        {
            try
            {
                // 창 스킬: 회피 찌르기
                if (SkillEffect.IsUsingSpear(__instance))
                {
                    if (SkillEffect.HasSkill("spear_Step2_evasion"))
                    {
                        SkillEffect.spearAfterRoll[__instance] = true;
                        SkillEffect.spearRollTime[__instance] = Time.time;

                        if (SkillEffect.spearRollBuffCoroutine.ContainsKey(__instance))
                        {
                            __instance.StopCoroutine(SkillEffect.spearRollBuffCoroutine[__instance]);
                        }
                        SkillEffect.spearRollBuffCoroutine[__instance] = __instance.StartCoroutine(ClearSpearRollBuff(__instance));

                        Plugin.Log.LogInfo("[회피 찌르기] 구르기 감지 - 2초간 보너스 활성화");
                        SkillEffect.DrawFloatingText(__instance, "🏃 회피 찌르기 준비!", Color.green);
                    }
                }

                // 단검 스킬
                if (SkillEffect.IsUsingDagger(__instance))
                {
                    SkillEffect.knifeLastRollTime[__instance] = Time.time;
                    SkillEffect.knifeAfterRoll[__instance] = true;

                    SkillEffect.CheckKnifeEvasion(__instance);
                    SkillEffect.ActivateKnifeMoveSpeed(__instance);

                    Plugin.Log.LogInfo("[단검 구르기] 모든 단검 버프 스킬 활성화");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 구르기 패치 오류: {ex.Message}");
            }
        }

        private static IEnumerator ClearSpearRollBuff(Player player)
        {
            yield return new WaitForSeconds(2f);

            if (SkillEffect.spearAfterRoll.ContainsKey(player))
            {
                SkillEffect.spearAfterRoll[player] = false;
                Plugin.Log.LogInfo("[회피 찌르기] 버프 자동 해제");
                SkillEffect.DrawFloatingText(player, "회피 찌르기 종료", Color.gray);
            }
        }
    }

    /// <summary>
    /// 근접 전문가 효과 패치
    /// </summary>
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class SkillTree_ItemData_GetDamage_MeleeExpert_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            try
            {
                if (__instance?.m_shared == null) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

                // 근접 전문가: 각 근접무기 타입별 데미지 +2
                if (SkillEffect.HasSkill("melee_root") && IsMeleeWeapon(__instance))
                {
                    if (__result.m_slash > 0) __result.m_slash += 2f;
                    if (__result.m_pierce > 0) __result.m_pierce += 2f;
                    if (__result.m_blunt > 0) __result.m_blunt += 2f;
                }

                // 단검 패시브 스킬 보너스
                ApplyKnifePassiveBonus(__instance, player, ref __result);

                // 검 패시브 스킬 보너스
                ApplySwordPassiveBonus(__instance, player, ref __result);

                // 둔기 패시브 스킬 보너스
                ApplyMacePassiveBonus(__instance, player, ref __result);

                // 창 패시브 스킬 보너스
                ApplySpearPassiveBonus(__instance, player, ref __result);

                // 폴암 패시브 스킬 보너스
                ApplyPolearmPassiveBonus(__instance, player, ref __result);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→발하임] GetDamage 근접전문가 패치 오류: {ex.Message}");
            }
        }

        private static bool IsMeleeWeapon(ItemDrop.ItemData weapon)
        {
            if (weapon?.m_shared == null) return false;

            return weapon.m_shared.m_skillType == Skills.SkillType.Swords ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Axes ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Knives ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Spears ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Polearms;
        }

        private static void ApplyKnifePassiveBonus(ItemDrop.ItemData item, Player player, ref HitData.DamageTypes result)
        {
            if (item.m_shared.m_skillType != Skills.SkillType.Knives) return;

            // 빠른 공격 - 베기 데미지 +2
            float knifeDamageBonus = Knife_Skill.GetKnifeAttackDamageBonus(player);
            if (knifeDamageBonus > 0)
            {
                if (result.m_slash > 0) result.m_slash += knifeDamageBonus;
            }

            // 치명적 피해 - 공격력 +25%
            float combatDamageBonus = Knife_Skill.GetKnifeCombatDamageBonus(player);
            if (combatDamageBonus > 0)
            {
                float multiplier = 1f + (combatDamageBonus / 100f);
                result.m_damage *= multiplier;
                result.m_slash *= multiplier;
                result.m_pierce *= multiplier;
                result.m_blunt *= multiplier;
            }

            // 암살자의 심장 - 피해 +50%
            if (SkillEffect.IsKnifeAssassinHeartActive(player))
            {
                float heartDamageBonus = Knife_Config.KnifeAssassinHeartDamageBonusValue / 100f;
                result.m_slash *= (1f + heartDamageBonus);
                result.m_pierce *= (1f + heartDamageBonus);
            }

            // 로그 직업: 그림자 일격 버프
            if (RogueSkills.IsRogueAttackBuffActive(player))
            {
                float rogueAttackBonus = Rogue_Config.RogueShadowStrikeAttackBonusValue / 100f;
                result.m_slash *= (1f + rogueAttackBonus);
                result.m_pierce *= (1f + rogueAttackBonus);
            }
        }

        private static void ApplySwordPassiveBonus(ItemDrop.ItemData item, Player player, ref HitData.DamageTypes result)
        {
            if (item.m_shared.m_skillType != Skills.SkillType.Swords) return;

            float totalSwordBonusPercent = 0f;
            float totalSwordBonusFixed = 0f;

            // 검 전문가 - 공격력 +10%
            float expertBonus = Sword_Skill.GetSwordExpertDamageBonus(player);
            if (expertBonus > 0) totalSwordBonusPercent += expertBonus;

            // 칼날 되치기 - 고정값
            float riposteBonus = Sword_Skill.GetSwordRiposteDamageBonus(player);
            if (riposteBonus > 0) totalSwordBonusFixed += riposteBonus;

            // 방어 전환 → 패링 돌격으로 전환됨 (액티브 스킬, 패시브 보너스 없음)

            // 비율 보너스 적용
            if (totalSwordBonusPercent > 0 && result.m_slash > 0)
            {
                float multiplier = 1f + (totalSwordBonusPercent / 100f);
                result.m_slash *= multiplier;
            }

            // 고정값 보너스 적용
            if (totalSwordBonusFixed > 0 && result.m_slash > 0)
            {
                result.m_slash += totalSwordBonusFixed;
            }
        }

        private static void ApplyMacePassiveBonus(ItemDrop.ItemData item, Player player, ref HitData.DamageTypes result)
        {
            if (item.m_shared.m_skillType != Skills.SkillType.Clubs) return;

            float totalMaceBonusPercent = 0f;
            float totalMaceBonusFixed = 0f;

            // 둔기 전문가 - 공격력 +10%
            if (SkillEffect.HasSkill("mace_Step1_damage"))
            {
                totalMaceBonusPercent += Mace_Config.MaceStep1DamageBonusValue;
            }

            // 무거운 타격 - 공격력 +3
            if (SkillEffect.HasSkill("mace_Step3_branch_heavy"))
            {
                totalMaceBonusFixed += Mace_Config.MaceStep3HeavyDamageBonusValue;
            }

            // 공격력 강화 - 공격력 +20%
            if (SkillEffect.HasSkill("mace_Step5_dps"))
            {
                totalMaceBonusPercent += Mace_Config.MaceStep5DpsDamageBonusValue;
            }

            // 비율 보너스 적용
            if (totalMaceBonusPercent > 0)
            {
                GetDamageHelper.ApplyPhysicalDamageBonus(ref result, totalMaceBonusPercent);
            }

            // 고정값 보너스 적용
            if (totalMaceBonusFixed > 0 && result.m_blunt > 0)
            {
                result.m_blunt += totalMaceBonusFixed;
            }
        }

        private static void ApplySpearPassiveBonus(ItemDrop.ItemData item, Player player, ref HitData.DamageTypes result)
        {
            if (item.m_shared.m_skillType != Skills.SkillType.Spears) return;

            float totalSpearBonus = 0f;

            // 창 전문가
            if (SkillEffect.HasSkill("spear_expert"))
            {
                totalSpearBonus += SkillTreeConfig.SpearStep1DamageBonusValue;
            }

            // 회피 찌르기
            if (SkillEffect.HasSkill("spear_Step2_evasion"))
            {
                totalSpearBonus += SkillTreeConfig.SpearStep3EvasionDamageBonusValue;
            }

            // 연격창
            if (SkillEffect.HasSkill("spear_Step3_pierce"))
            {
                totalSpearBonus += SkillTreeConfig.SpearStep3PierceDamageBonusValue;
            }

            // 삼연창
            if (SkillEffect.HasSkill("spear_Step4_triple"))
            {
                totalSpearBonus += SkillTreeConfig.SpearStep5TripleDamageBonusValue;
            }

            if (totalSpearBonus > 0)
            {
                GetDamageHelper.ApplyPhysicalDamageBonus(ref result, totalSpearBonus);
            }
        }

        private static void ApplyPolearmPassiveBonus(ItemDrop.ItemData item, Player player, ref HitData.DamageTypes result)
        {
            if (item.m_shared.m_skillType != Skills.SkillType.Polearms) return;

            float totalPolearmBonusPercent = 0f;
            float totalPolearmBonusFixed = 0f;

            // 제압 공격 - 공격력 +30%
            if (SkillEffect.HasSkill("polearm_step1_suppress"))
            {
                totalPolearmBonusPercent += SkillTreeConfig.PolearmStep1SuppressDamageValue;
            }

            // 폴암강화 - 무기 공격력 +5
            if (SkillEffect.HasSkill("polearm_step4_charge"))
            {
                totalPolearmBonusFixed += SkillTreeConfig.PolearmStep4ChargeDamageBonusValue;
            }

            // 비율 보너스 적용
            if (totalPolearmBonusPercent > 0)
            {
                GetDamageHelper.ApplyPhysicalDamageBonus(ref result, totalPolearmBonusPercent);
            }

            // 고정값 보너스 적용
            if (totalPolearmBonusFixed > 0)
            {
                GetDamageHelper.AddFixedDamage(ref result, totalPolearmBonusFixed, "pierce");
            }
        }
    }

    /// <summary>
    /// 근접 무기 스킬 사망 시 정리 시스템
    /// </summary>
    public static partial class SkillEffect
    {
        public static void CleanupMeleeSkillsOnDeath(Player player)
        {
            try
            {
                // 단검 관련 Dictionary 정리
                consecutiveHits.Remove(player);
                lastHitTime.Remove(player);
                if (evasionBuffCoroutine.ContainsKey(player))
                {
                    evasionBuffCoroutine.Remove(player);
                }
                evasionBonus.Remove(player);
                stealthMovementBonus.Remove(player);
                knifeEvasionEndTime.Remove(player);
                knifeMoveSpeedEndTime.Remove(player);
                knifeDamageBonusEndTime.Remove(player);
                knifeCritRateEndTime.Remove(player);
                knifeLastRollTime.Remove(player);
                knifeAfterRoll.Remove(player);
                knifeAssassinHeartEndTime.Remove(player);
                knifeAssassinHeartCooldownEndTime.Remove(player);

                // 검 관련 Dictionary 정리
                swordComboCount.Remove(player);
                swordLastHitTime.Remove(player);
                if (defenseBuffCoroutine.ContainsKey(player))
                {
                    defenseBuffCoroutine.Remove(player);
                }
                defenseBonus.Remove(player);
                nextAttackBoosted.Remove(player);
                nextAttackMultiplier.Remove(player);
                nextAttackExpiry.Remove(player);
                swordCounterDefenseEndTime.Remove(player);
                swordBladeCounterEndTime.Remove(player);

                // 창 관련 Dictionary 정리
                spearComboCount.Remove(player);
                spearLastHitTime.Remove(player);
                spearThrowCooldown.Remove(player);
                spearAfterRoll.Remove(player);
                spearRollTime.Remove(player);
                if (spearRollBuffCoroutine.ContainsKey(player))
                {
                    spearRollBuffCoroutine.Remove(player);
                }
                spearTripleComboActive.Remove(player);
                spearComboSequenceActive.Remove(player);
                spearExpertComboCount.Remove(player);
                spearExpertLastHitTime.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Melee Skills] 정리 실패: {ex.Message}");
            }
        }
    }
}

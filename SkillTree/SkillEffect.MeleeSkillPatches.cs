using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    // === 근접 스킬 Harmony 패치들 ===

    /// <summary>
    /// 단검 전문가 백스탭 보너스 - Prefix로 실제 피해에 반영
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MeleeSkills_Dagger_Backstab_Prefix_Patch
    {
        [HarmonyPriority(Priority.Normal)]
        public static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;
                if (__instance.IsPlayer() && !SkillEffect.IsPvPCombat(__instance, attacker as Character)) return;
                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingDagger(player)) return;
                // 직접 단검 공격(Knives 스킬)만 적용 — 로그 AOE 등 스킬 생성 데미지 제외
                if (hit.m_skill != Skills.SkillType.Knives) return;
                SkillEffect.CheckKnifeExpertBackstab(player, __instance, hit);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[단검 전문가] 백스탭 Prefix 패치 오류: {ex.Message}");
            }
        }
    }

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
                if (attacker == null || !attacker.IsPlayer()) return;
                if (__instance.IsPlayer() && !SkillEffect.IsPvPCombat(__instance, attacker as Character)) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingDagger(player)) return;

                // 연속 공격 카운트 업데이트
                SkillEffect.UpdateConsecutiveHits(player);

                // 전투 숙련 - 전투 중 공격력 증가
                SkillEffect.CheckKnifeCombatDamage(player, hit);

                // 암살자의 심장 - G키 액티브 스킬 치명타 효과
                SkillEffect.ApplyKnifeAssassinHeartCrit(player, hit);

                // 공격과 회피 - 2연속 공격 시 회피율 증가 (쿨타임 30초)
                var consecutiveHits = SkillEffect.consecutiveHits.TryGetValue(player, out var hits) ? hits : 0;
                if (consecutiveHits >= 2)
                {
                    SkillEffect.CheckStep5AttackEvasion(player);
                }

                // 암살술 - 3연속 공격 시 스태거 발동
                Knife_Skill.ApplyKnifeAssassinationBonus(player, __instance);

                // 약점폭발 - 공격 적중 시 스택 누적
                KnifeStackExplosion.AddStack(player, __instance);
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
                if (attacker == null || !attacker.IsPlayer()) return;
                if (__instance.IsPlayer() && !SkillEffect.IsPvPCombat(__instance, attacker as Character)) return;

                var player = attacker as Player;
                if (player == null || !WeaponHelper.IsUsingSwordOrAxe(player)) return;

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
                        if (!SkillEffect.nextAttackShowMessage.TryGetValue(player, out var showMsg) || showMsg)
                            SkillEffect.DrawFloatingText(player, "⚔️ " + L.Get("enhanced_strike", $"{(multiplier - 1) * 100:F0}"));
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

                // 공방일체 효과 표시 (10% 확률)
                if (UnityEngine.Random.Range(0f, 1f) < 0.1f)
                {
                    SkillEffect.ApplySwordOffenseDefense(player, hit);
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
                if (attacker == null || !attacker.IsPlayer()) return;
                if (__instance.IsPlayer() && !SkillEffect.IsPvPCombat(__instance, attacker as Character)) return;

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

            // 창 전문가 proc 소비 (이번 공격으로 버프 차지 감소)
            SkillEffect.ConsumeSpearExpertProc(player);

            // 창 전문가 proc 발동 (다음 공격에 적용될 버프)
            SkillEffect.TriggerSpearExpertProc(player);

            // 회피 찌르기 - 공격 시 5초간 회피 버프
            SkillEffect.ApplySpearEvasionBuff(player);

            // 이연창 콤보 체크 (버프 활성화)
            SkillEffect.CheckSpearDualCombo(player);

            // 실제 데미지 수정은 GetDamage 패치(ApplySpearPassiveBonus)에서 처리
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
            // 폴암강화(polearm_step4_charge)는 영구 패시브 - 데미지는 GetDamage에서 적용, 텍스트 표시 없음
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
                // 단검 스킬
                if (SkillEffect.IsUsingDagger(__instance))
                {
                    SkillEffect.knifeLastRollTime[__instance] = Time.time;
                    SkillEffect.knifeAfterRoll[__instance] = true;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 구르기 패치 오류: {ex.Message}");
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

                // 근접 전문가: 각 근접무기 타입별 데미지 +3
                if (SkillEffect.HasSkill("melee_root") && IsMeleeWeapon(__instance))
                {
                    if (__result.m_slash > 0) __result.m_slash += 3f;
                    if (__result.m_pierce > 0) __result.m_pierce += 3f;
                    if (__result.m_blunt > 0) __result.m_blunt += 3f;
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
                   weapon.m_shared.m_skillType == Skills.SkillType.Polearms ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Unarmed;
        }

        private static void ApplyKnifePassiveBonus(ItemDrop.ItemData item, Player player, ref HitData.DamageTypes result)
        {
            if (item.m_shared.m_skillType != Skills.SkillType.Knives) return;

            // 빠른 공격 - 베기/관통 공격력 각 +1
            float knifeDamageBonus = Knife_Skill.GetKnifeAttackDamageBonus(player);
            if (knifeDamageBonus > 0)
            {
                if (result.m_slash > 0) result.m_slash += knifeDamageBonus;
                if (result.m_pierce > 0) result.m_pierce += knifeDamageBonus;
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
            if (item.m_shared.m_skillType != Skills.SkillType.Swords &&
                item.m_shared.m_skillType != Skills.SkillType.Axes) return;

            float totalSwordBonusPercent = 0f;
            float totalSwordBonusFixed = 0f;

            // 검 전문가 - 공격력 +10%
            float expertBonus = Sword_Skill.GetSwordExpertDamageBonus(player);
            if (expertBonus > 0) totalSwordBonusPercent += expertBonus;

            // 칼날 되치기 - 고정값
            float riposteBonus = Sword_Skill.GetSwordRiposteDamageBonus(player);
            if (riposteBonus > 0) totalSwordBonusFixed += riposteBonus;

            // 방어 전환 → 패링 돌격으로 전환됨 (액티브 스킬, 패시브 보너스 없음)

            // 공방일체 - 검 사용 시 공격력 보너스
            if (SkillEffect.HasSkill("sword_step3_allinone"))
            {
                float allinoneBonus = SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue / 100f;
                if (result.m_slash > 0) result.m_slash *= (1f + allinoneBonus);
                if (result.m_pierce > 0) result.m_pierce *= (1f + allinoneBonus);
            }

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

            // 무거운 일격 - 타격 +3
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

            // 창 전문가 - 신규 메커닉은 공격속도 proc (SpeedTree에서 처리), 데미지 보너스 없음

            // 투창 전문가는 Plugin.Patches.cs WeaponCriticalSystemPatch(ApplyDamage)에서 단일 처리

            // 연격창 - 관통 공격력 전용
            if (SkillEffect.HasSkill("spear_Step3_pierce"))
            {
                result.m_pierce += SkillTreeConfig.SpearStep3PierceDamageBonusValue;
            }

            // 이연창 (버프 활성 시에만 적용)
            if (SkillEffect.HasSkill("spear_Step4_triple") && SkillEffect.IsSpearDualBuffActive(player))
            {
                totalSpearBonus += Spear_Config.SpearDualDamageBonusValue;
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

            // 폴암강화 - 관통 공격력 +5
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
                GetDamageHelper.AddFixedDamage(ref result, totalPolearmBonusFixed, "slash", "pierce");
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
                knifeMoveSpeedEndTime.Remove(player);
                knifeDamageBonusEndTime.Remove(player);
                knifeAttackEvasionEndTime.Remove(player);
                knifeAttackEvasionCooldownEndTime.Remove(player);
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
                nextAttackShowMessage.Remove(player);
                swordCounterDefenseEndTime.Remove(player);

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
                spearEvasionBuffEndTime.Remove(player);
                spearTripleComboActive.Remove(player);
                spearComboSequenceActive.Remove(player);
                spearExpertComboCount.Remove(player);
                spearExpertLastHitTime.Remove(player);
                spearExpertProcEndTime.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Melee Skills] 정리 실패: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 암살자의 심장 - 적중 카운트 증가 패치
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class AssassinHeart_HitCount_Patch
    {
        [HarmonyPostfix]
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                // 플레이어가 공격자인 경우만 처리
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;
                if (__instance.IsPlayer()) return; // 몬스터만 대상

                var player = attacker as Player;
                if (player == null) return;

                // 암살자의 심장 공격 모드인지 확인
                if (!SkillEffect.IsAssassinHeartAttackMode(player)) return;

                // 대상 몬스터 확인
                var target = SkillEffect.GetAssassinHeartTarget(player);
                if (target == null || target != __instance) return;

                // 단검 사용 중인지 확인
                if (!SkillEffect.IsUsingDagger(player)) return;

                // 적중 카운트 증가
                SkillEffect.IncrementAssassinHeartHitCount(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[암살자의 심장] 적중 카운트 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 빠른 공격모션 - Humanoid.StartAttack에서 pending 세트 설정
    /// </summary>
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
    public static class SpearAttackMotion_StartAttack_Patch
    {
        [HarmonyPrefix, HarmonyPriority(Priority.Normal)]
        static void Prefix(Humanoid __instance, bool secondaryAttack)
        {
            var player = __instance as Player;
            if (player == null || player != Player.m_localPlayer) return;
            if (!SkillEffect.HasSkill("spear_Step1_crit")) return;
            if (player.GetCurrentWeapon()?.m_shared?.m_skillType != Skills.SkillType.Spears) return;

            if (secondaryAttack)
            {
                // StartAttack(false)가 내부 실패 시 Attack.Start 미호출로 pending이 잔류할 수 있음
                // 세컨드 공격 진입 시 반드시 클리어해 던지기 모션 교체 방지
                SkillEffect.s_spearKnifeAnimPending.Remove(player);
                SpearAttackMotion_AnimSwap_Patch.s_spearSwordAnimPending.Remove(player);

                // 창 던지기: 플래그 설정 → SetRightHandEquipped 재실행 시 플립 덮어쓰기 방지
                SpearModelFlip_Patch.s_isThrowingSpear = true;
                var visEquip = player.GetComponent<VisEquipment>();
                var rightItem = SpearModelFlip_Patch.s_rightItemField?.GetValue(visEquip) as GameObject;
                if (rightItem != null)
                    rightItem.transform.localRotation = Quaternion.identity;
                return;
            }

            if (Spear_Config.SpearStep1AttackMotionValue == "단검")
                SkillEffect.s_spearKnifeAnimPending.Add(player);
            else
                SpearAttackMotion_AnimSwap_Patch.s_spearSwordAnimPending.Add(player);
        }
    }

    /// <summary>
    /// 빠른 공격모션 - Attack.Start에서 수동 체인 카운터로 모션 교체
    /// ATTACK_ANIMATION_CHANGE_RULES.md 방법 1-A: Flag + 수동 체인 카운터
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class SpearAttackMotion_AnimSwap_Patch
    {
        internal static readonly HashSet<Player> s_spearSwordAnimPending = new HashSet<Player>();

        static readonly Dictionary<Player, (int level, float lastTime)> s_knifeChain =
            new Dictionary<Player, (int level, float lastTime)>();
        static readonly Dictionary<Player, (int level, float lastTime)> s_swordChain =
            new Dictionary<Player, (int level, float lastTime)>();
        const float ChainResetSec = 2.0f;

        class State
        {
            public string anim;
            public int chainLevels;
            public int randomAnims;
        }

        [HarmonyPrefix]
        static void Prefix(Attack __instance, Humanoid character, out State __state)
        {
            __state = null;
            var player = character as Player;
            if (player == null) return;

            // 창 던지기 진행 중이면 애니메이션 교체 건너뜀 (이중 방어)
            if (SpearModelFlip_Patch.s_isThrowingSpear) return;

            bool isKnife = SkillEffect.s_spearKnifeAnimPending.Remove(player);
            bool isSword = !isKnife && s_spearSwordAnimPending.Remove(player);
            if (!isKnife && !isSword) return;

            var src = isKnife
                ? SkillEffect.GetCachedKnifePrimaryAttack()
                : SkillEffect.GetCachedSwordPrimaryAttack();
            if (src == null) return;

            // 빌려온 모션 재생 확정 → 전방 2m 보정 판정 대상으로 등록
            SpearQuickAttackGuaranteedHit_Trigger_Patch.s_guaranteedHitPending.Add(player);

            int chainMax = src.m_attackChainLevels > 1 ? src.m_attackChainLevels
                         : src.m_attackRandomAnimations >= 2 ? src.m_attackRandomAnimations : 1;
            string trigger;
            if (chainMax > 1)
            {
                var dict = isKnife ? s_knifeChain : s_swordChain;
                dict.TryGetValue(player, out var prev);
                int lvl = (Time.time - prev.lastTime > ChainResetSec) ? 0 : prev.level;
                trigger = src.m_attackAnimation + lvl;
                dict[player] = ((lvl + 1) % chainMax, Time.time);
            }
            else
            {
                trigger = src.m_attackAnimation;
            }

            __state = new State
            {
                anim        = __instance.m_attackAnimation,
                chainLevels = __instance.m_attackChainLevels,
                randomAnims = __instance.m_attackRandomAnimations
            };
            __instance.m_attackAnimation        = trigger;
            __instance.m_attackChainLevels      = 0;
            __instance.m_attackRandomAnimations = 0;
        }

        [HarmonyPostfix]
        static void Postfix(Attack __instance, State __state)
        {
            if (__state == null) return;
            __instance.m_attackAnimation        = __state.anim;
            __instance.m_attackChainLevels      = __state.chainLevels;
            __instance.m_attackRandomAnimations = __state.randomAnims;
        }
    }

    [HarmonyPatch(typeof(VisEquipment), "SetRightHandEquipped")]
    public static class SpearModelFlip_Patch
    {
        internal static readonly System.Reflection.FieldInfo s_rightItemField =
            typeof(VisEquipment).GetField("m_rightItemInstance",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

        internal static bool s_isThrowingSpear = false;

        [HarmonyPostfix]
        static void Postfix(VisEquipment __instance)
        {
            var player = __instance.GetComponent<Player>();
            if (player == null || player != Player.m_localPlayer) return;

            var rightItem = s_rightItemField?.GetValue(__instance) as GameObject;

            // 창이 손에서 사라짐 (던짐 완료) → 플래그 클리어
            if (rightItem == null)
            {
                s_isThrowingSpear = false;
                return;
            }

            bool isSpear = player.GetCurrentWeapon()?.m_shared?.m_skillType == Skills.SkillType.Spears
                        || rightItem.name.IndexOf("Spear", StringComparison.OrdinalIgnoreCase) >= 0;
            if (!isSpear) return;

            // 던지기 중: Postfix가 플립 덮어쓰기 방지
            if (s_isThrowingSpear)
            {
                rightItem.transform.localRotation = Quaternion.identity;
                return;
            }

            rightItem.transform.localRotation = SkillEffect.HasSkill("spear_Step1_crit")
                ? Quaternion.Euler(0, 180, 0)
                : Quaternion.identity;
        }
    }

    /// <summary>
    /// 빠른 공격모션 - 전방 2m 보정 판정
    /// 빌려온 단검/검 애니메이션의 히트 트리거 타이밍이 창 고유 판정과 어긋나
    /// 키 작은 몬스터를 놓치는 경우가 있어, 네이티브 판정이 못 맞춘 전방 2m 이내 적을 보정 적중시킨다.
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.OnAttackTrigger))]
    public static class SpearQuickAttackGuaranteedHit_Trigger_Patch
    {
        internal static readonly HashSet<Player> s_guaranteedHitPending = new HashSet<Player>();
        internal static readonly HashSet<Player> s_swingActive = new HashSet<Player>();
        internal static readonly Dictionary<Player, HashSet<Character>> s_nativeHitTargets =
            new Dictionary<Player, HashSet<Character>>();

        private const float HitRadius = 2f;
        private const float ForwardHalfAngle = 80f;

        internal static Player GetAttacker(Attack attack)
        {
            return Traverse.Create(attack).Field("m_character").GetValue<Humanoid>() as Player;
        }

        [HarmonyPrefix]
        static void Prefix(Attack __instance)
        {
            var player = GetAttacker(__instance);
            if (player == null) return;
            if (!s_guaranteedHitPending.Remove(player)) return;

            s_swingActive.Add(player);
            s_nativeHitTargets[player] = new HashSet<Character>();
        }

        [HarmonyPostfix]
        static void Postfix(Attack __instance)
        {
            var player = GetAttacker(__instance);
            if (player == null) return;
            if (!s_swingActive.Remove(player)) return;

            try
            {
                ApplyGuaranteedHit(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[빠른 공격모션] 보정 판정 오류: {ex.Message}");
            }
            finally
            {
                s_nativeHitTargets.Remove(player);
            }
        }

        private static void ApplyGuaranteedHit(Player player)
        {
            var weapon = player.GetCurrentWeapon();
            if (weapon == null) return;

            s_nativeHitTargets.TryGetValue(player, out var alreadyHit);

            Vector3 playerPos = player.transform.position;
            Vector3 forward = player.transform.forward;
            forward.y = 0f;
            forward.Normalize();

            float skillFactor = player.GetSkillFactor(Skills.SkillType.Spears);
            var weaponDamage = weapon.GetDamage(0, skillFactor);

            var colliders = Physics.OverlapSphere(playerPos, HitRadius);
            var processed = new HashSet<Character>();

            foreach (var col in colliders)
            {
                if (col == null) continue;
                var target = col.GetComponent<Character>() ?? col.GetComponentInParent<Character>();
                if (target == null || target == player || target.IsDead()) continue;
                if (!processed.Add(target)) continue;
                if (alreadyHit != null && alreadyHit.Contains(target)) continue;

                bool isMonster = target.IsMonsterFaction(Time.time) || target.IsBoss();
                if (!isMonster)
                {
                    var targetPlayer = target as Player;
                    if (targetPlayer == null || !targetPlayer.IsPVPEnabled() || !player.IsPVPEnabled()) continue;
                }

                Vector3 toTarget = target.transform.position - playerPos;
                toTarget.y = 0f;
                if (toTarget.sqrMagnitude < 0.001f)
                    toTarget = forward;
                else
                    toTarget.Normalize();

                if (Vector3.Angle(forward, toTarget) > ForwardHalfAngle) continue;

                var hitData = new HitData();
                hitData.m_damage = weaponDamage;
                hitData.m_point = target.GetCenterPoint();
                hitData.m_dir = toTarget;
                hitData.m_pushForce = weapon.m_shared.m_attackForce * skillFactor;
                hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;
                hitData.m_skill = Skills.SkillType.Spears;
                hitData.m_blockable = weapon.m_shared.m_blockable;
                hitData.m_dodgeable = weapon.m_shared.m_dodgeable;
                hitData.SetAttacker(player);

                target.Damage(hitData);
            }
        }
    }

    /// <summary>
    /// 빠른 공격모션 - 네이티브 판정이 실제로 맞춘 대상을 기록 (전방 2m 보정 시 중복 데미지 방지)
    /// Attack.AddHitPoint는 실제 게임 어셈블리에서 public이 아니라 접근 불가 → Character.Damage로 대체 추적
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class SpearQuickAttackGuaranteedHit_TrackNativeHit_Patch
    {
        [HarmonyPrefix]
        static void Prefix(Character __instance, HitData hit)
        {
            if (SpearQuickAttackGuaranteedHit_Trigger_Patch.s_swingActive.Count == 0) return;

            var player = hit.GetAttacker() as Player;
            if (player == null) return;
            if (hit.m_skill != Skills.SkillType.Spears) return;
            if (!SpearQuickAttackGuaranteedHit_Trigger_Patch.s_swingActive.Contains(player)) return;

            if (SpearQuickAttackGuaranteedHit_Trigger_Patch.s_nativeHitTargets.TryGetValue(player, out var set))
                set.Add(__instance);
        }
    }

    [HarmonyPatch(typeof(SkillTreeManager), nameof(SkillTreeManager.ResetAllSkillLevelsExceptProduction))]
    public static class SpearModelFlip_SkillReset_Patch
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            var player = Player.m_localPlayer;
            if (player == null) return;
            var visEquip = player.GetComponent<VisEquipment>();
            if (visEquip == null) return;
            var rightItem = SpearModelFlip_Patch.s_rightItemField?.GetValue(visEquip) as GameObject;
            if (rightItem == null) return;

            bool isSpear = player.GetCurrentWeapon()?.m_shared?.m_skillType == Skills.SkillType.Spears
                        || rightItem.name.IndexOf("Spear", StringComparison.OrdinalIgnoreCase) >= 0;
            if (!isSpear) return;

            rightItem.transform.localRotation = Quaternion.identity;
        }
    }

}

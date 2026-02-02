using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 검/창 스킬 효과 시스템
    /// SkillEffect.MeleeSkills.cs에서 분리된 검/창 관련 기능들
    /// 폴암 스킬은 SkillEffect.PolearmTree.cs에 정의됨
    /// </summary>
    public static partial class SkillEffect
    {
        // === 검 연계 시스템 ===

        /// <summary>
        /// 검 연계 공격 카운트 업데이트
        /// </summary>
        public static void UpdateSwordCombo(Player player)
        {
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            float now = Time.time;
            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 3f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;

            // 3연타 달성 시 다음 공격 부스트
            if (swordComboCount[player] >= 3)
            {
                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1.5f;
                nextAttackExpiry[player] = now + 5f;
                PlaySkillEffect(player, "sword_combo");
                DrawFloatingText(player, "⚔️ 검술 연계 3단!");
            }
        }

        /// <summary>
        /// 검 전문가 2연속 공격 체크 (sword_expert)
        /// </summary>
        public static void CheckSwordExpertCombo(Player player)
        {
            if (!HasSkill("sword_expert")) return;

            float now = Time.time;
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 3f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;

            if (swordComboCount[player] >= 2)
            {
                DrawFloatingText(player, $"⚔️ 검 전문가 2연속! (+{SkillTreeConfig.SwordStep1ExpertComboBonusValue}%)");
                Plugin.Log.LogInfo($"[검 전문가] 2연속 공격 달성 - 공격력 +{SkillTreeConfig.SwordStep1ExpertComboBonusValue}% 보너스 적용");

                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1f + (SkillTreeConfig.SwordStep1ExpertComboBonusValue / 100f);
                nextAttackExpiry[player] = now + SkillTreeConfig.SwordStep1ExpertDurationValue;
            }
        }

        /// <summary>
        /// 연속베기 3연속 공격 체크 (sword_step2_combo)
        /// </summary>
        public static void CheckSwordComboSlash(Player player)
        {
            if (!HasSkill("sword_step2_combo")) return;

            float now = Time.time;
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 3f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;

            if (swordComboCount[player] >= 3)
            {
                DrawFloatingText(player, $"⚔️ 연속베기! 3연속 공격력 +{SkillTreeConfig.SwordStep2ComboSlashBonusValue}%");
                Plugin.Log.LogInfo("[연속베기] 3연속 공격 달성 - 공격력 +13% 보너스 적용");

                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1f + (SkillTreeConfig.SwordStep2ComboSlashBonusValue / 100f);
                nextAttackExpiry[player] = now + SkillTreeConfig.SwordStep2ComboSlashDurationValue;
                swordComboCount[player] = 0;
            }
        }

        /// <summary>
        /// 반격 자세 - 패링 성공 후 방어력 증가 효과
        /// </summary>
        public static void ApplySwordCounterDefense(Player player)
        {
            if (!HasSkill("sword_step1_counter")) return;

            float duration = SkillTreeConfig.SwordStep1CounterDurationValue;
            swordCounterDefenseEndTime[player] = Time.time + duration;

            DrawFloatingText(player, $"🛡️ 반격 자세! ({duration}초간 방어력 +{SkillTreeConfig.SwordStep1CounterDefenseBonusValue}%)");
            Plugin.Log.LogInfo($"[반격 자세] 패링 성공 - {duration}초간 방어력 +{SkillTreeConfig.SwordStep1CounterDefenseBonusValue}% 적용");
        }

        /// <summary>
        /// 칼날 되치기 - 패링 성공 시 공격력 증가 효과
        /// </summary>
        public static void ApplySwordBladeCounter(Player player)
        {
            if (!HasSkill("sword_step3_riposte")) return;

            float duration = SkillTreeConfig.SwordStep3BladeCounterDurationValue;
            swordBladeCounterEndTime[player] = Time.time + duration;

            DrawFloatingText(player, $"⚔️ 칼날 되치기! ({duration}초간 공격력 +{SkillTreeConfig.SwordStep3BladeCounterBonusValue}%)");
            Plugin.Log.LogInfo($"[칼날 되치기] 패링 성공 - {duration}초간 공격력 +{SkillTreeConfig.SwordStep3BladeCounterBonusValue}% 적용");
        }

        /// <summary>
        /// 공방일체 - 양손 무기 착용 시 공격력/방어력 보너스
        /// </summary>
        public static void ApplySwordOffenseDefense(Player player, HitData hit)
        {
            if (!HasSkill("sword_step3_allinone")) return;

            var currentWeapon = player.GetCurrentWeapon();
            if (currentWeapon != null && currentWeapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
            {
                DrawFloatingText(player, $"⚔️🛡️ 공방일체! (공격력 +{SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue}%, 방어력 +{SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue})");
                Plugin.Log.LogDebug($"[공방일체] 양손 무기 착용 - 공격력 +{SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue}%, 방어력 +{SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue} 적용");
            }
        }

        /// <summary>
        /// 방어 전환 - 방패 착용/미착용에 따른 스탯 조정
        /// </summary>
        public static void ApplySwordDefenseSwitch(Player player, HitData hit)
        {
            if (!HasSkill("sword_step5_defswitch")) return;

            var inventory = player.GetInventory();
            bool hasShield = false;
            if (inventory != null)
            {
                var items = inventory.GetEquippedItems();
                hasShield = items.Any(item => item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield);
            }

            if (hasShield)
            {
                DrawFloatingText(player, "🛡️ 방어 전환 (방패): 방어력 +15%, 공격력 -5%");
                Plugin.Log.LogDebug("[방어 전환] 방패 착용 모드 - 방어력 +15%, 공격력 -5%");
            }
            else
            {
                DrawFloatingText(player, "⚔️ 방어 전환 (공격): 공격력 +20%, 방어력 -10%");
                Plugin.Log.LogDebug("[방어 전환] 공격 모드 - 공격력 +20%, 방어력 -10%");
            }
        }

        /// <summary>
        /// Sword Slash 액티브 스킬 데미지 보너스 적용
        /// </summary>
        public static void ApplySwordSlashDamageBonus(Player player, Character target, HitData hit)
        {
            if (!HasSkill("sword_step5_finalcut") && !HasSkill("sword_slash")) return;
            if (!Sword_Skill.IsSwordSlashActive(player)) return;

            float damageRatio = Sword_Config.SwordSlashDamageRatioValue / 100f;

            hit.m_damage.m_blunt *= damageRatio;
            hit.m_damage.m_slash *= damageRatio;
            hit.m_damage.m_pierce *= damageRatio;
            hit.m_damage.m_chop *= damageRatio;
            hit.m_damage.m_pickaxe *= damageRatio;
            hit.m_damage.m_fire *= damageRatio;
            hit.m_damage.m_frost *= damageRatio;
            hit.m_damage.m_lightning *= damageRatio;
            hit.m_damage.m_poison *= damageRatio;
            hit.m_damage.m_spirit *= damageRatio;

            PlaySkillEffect(player, "sword_slash", target.transform.position);
            DrawFloatingText(player, $"⚔️ Sword Slash! ({damageRatio * 100:F0}%)", Color.red);

            Plugin.Log.LogInfo($"[Sword Slash] 데미지 보너스 적용 - {damageRatio * 100:F0}%");
        }

        // === 창 스킬 시스템 ===

        /// <summary>
        /// 창 롤 후 공격 보너스 체크
        /// </summary>
        public static void CheckSpearRollAttack(Player player)
        {
            if (!spearAfterRoll.ContainsKey(player)) return;

            float now = Time.time;
            if (spearAfterRoll[player] && now - spearRollTime[player] < 2f)
            {
                spearAfterRoll[player] = false;
                PlaySkillEffect(player, "spear_evasion");
                DrawFloatingText(player, "🎯 회피 후 반격!");
            }
        }

        /// <summary>
        /// 창 전문가 2연속 공격 체크 (MMO 연동 방식)
        /// </summary>
        public static void CheckSpearExpertCombo(Player player)
        {
            if (!HasSkill("spear_expert")) return;

            float now = Time.time;
            if (!spearExpertComboCount.ContainsKey(player))
                spearExpertComboCount[player] = 0;

            if (spearExpertLastHitTime.ContainsKey(player) && now - spearExpertLastHitTime[player] < 3f)
            {
                spearExpertComboCount[player]++;
            }
            else
            {
                spearExpertComboCount[player] = 1;
            }
            spearExpertLastHitTime[player] = now;
        }

        /// <summary>
        /// 삼연창 3연속 공격 체크
        /// </summary>
        public static void CheckSpearTripleCombo(Player player)
        {
            if (!HasSkill("spear_Step4_triple")) return;

            float now = Time.time;
            if (!spearComboCount.ContainsKey(player))
                spearComboCount[player] = 0;

            if (spearLastHitTime.ContainsKey(player) && now - spearLastHitTime[player] < 3f)
            {
                spearComboCount[player]++;
            }
            else
            {
                spearComboCount[player] = 1;
            }
            spearLastHitTime[player] = now;

            if (spearComboCount[player] >= 3)
            {
                spearTripleComboActive[player] = true;
                spearComboCount[player] = 0;

                Plugin.Log.LogInfo("[삼연창] 3연속 공격 달성 - 다음 공격에 보너스 적용");
            }
        }

        /// <summary>
        /// 연격창 무기 공격력 +4 패시브 보너스 적용
        /// </summary>
        public static void CheckDoubleAttack(Player player, Character target, HitData originalHit)
        {
            if (!HasSkill("spear_Step3_pierce")) return;

            float bonusValue = SkillTreeConfig.SpearStep3PierceDamageBonusValue;
            originalHit.m_damage.m_pierce += bonusValue;

            Plugin.Log.LogDebug($"[연격창] 무기 공격력 보너스 적용 (pierce): +{SkillTreeConfig.SpearStep3PierceDamageBonusValue}");
        }

        /// <summary>
        /// 투창 전문가 공격력 보너스 적용
        /// </summary>
        public static void ApplySpearThrowExpertDamage(HitData hit)
        {
            if (!HasSkill("spear_Step1_throw")) return;

            float bonusPercent = 0.3f; // 30%
            hit.m_damage.m_pierce *= (1f + bonusPercent);

            Plugin.Log.LogDebug("[투창 전문가] 투창 공격력 +30% 적용 (pierce)");
        }

        /// <summary>
        /// 꿰뚫는 창 치명타 확률 적용
        /// </summary>
        public static void ApplySpearPenetrateCritical(Player player, HitData hit)
        {
            if (!HasSkill("spear_Step5_penetrate")) return;

            float critChance = 0.12f;
            if (UnityEngine.Random.Range(0f, 1f) <= critChance)
            {
                hit.m_damage.m_pierce *= 2f;

                DrawFloatingText(player, "💥 꿰뚫는 창 치명타!", Color.red);
                Plugin.Log.LogDebug("[꿰뚫는 창] 치명타 발생! (12% 확률)");
            }
        }
    }
}

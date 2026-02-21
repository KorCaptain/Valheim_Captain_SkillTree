using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 단검 전문가 전용 툴팁 시스템
    /// 각 스킬별로 동적 툴팁을 생성하고 관리
    /// 아처/탱커 툴팁 시스템과 동일한 구조로 구현
    /// </summary>
    public static class Knife_Tooltip
    {
        // MeleeTooltipUtils.MeleeTooltipData 사용
        // 기존 KnifeTooltipData 제거

        #region 개별 스킬 툴팁 생성

        /// <summary>
        /// 단검 전문가 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeExpertTooltip()
        {
            var backstabBonus = Knife_Config.KnifeExpertBackstabBonusValue;
            var requiredPoints = Knife_Config.KnifeExpertRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_expert")}</size></color>",
                L.Get("knife_desc_expert", backstabBonus),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 회피 숙련 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeEvasionTooltip()
        {
            var evasionBonus = Knife_Config.KnifeEvasionBonusValue;
            var requiredPoints = Knife_Config.KnifeEvasionRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_evasion")}</size></color>",
                L.Get("knife_desc_evasion", evasionBonus),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 빠른 움직임 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeMoveSpeedTooltip()
        {
            var speedBonus = Knife_Config.KnifeMoveSpeedBonusValue;
            var requiredPoints = Knife_Config.KnifeMoveSpeedRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_move_speed")}</size></color>",
                L.Get("knife_desc_move_speed", speedBonus),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 빠른 공격 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeAttackSpeedTooltip()
        {
            var damageBonus = Knife_Config.KnifeAttackDamageBonusValue;
            var requiredPoints = Knife_Config.KnifeAttackSpeedRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_attack_speed")}</size></color>",
                L.Get("knife_desc_attack_speed", damageBonus),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 공격과 회피 툴팁 생성
        /// </summary>
        public static string GetKnifeCritRateTooltip()
        {
            var evasionBonus = Knife_Config.KnifeAttackEvasionBonusValue;
            var evasionDuration = Knife_Config.KnifeAttackEvasionDurationValue;
            var evasionCooldown = Knife_Config.KnifeAttackEvasionCooldownValue;
            var requiredPoints = Knife_Config.KnifeCritRateRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_crit_rate")}</size></color>",
                L.Get("knife_desc_attack_evasion", evasionBonus, evasionDuration),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 치명적 피해 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeCombatDamageTooltip()
        {
            var damageBonus = Knife_Config.KnifeCombatDamageBonusValue;
            var requiredPoints = Knife_Config.KnifeCombatDamageRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_combat_damage")}</size></color>",
                L.Get("knife_desc_combat_damage", damageBonus),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 암살자 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeExecutionTooltip()
        {
            var critDamage = Knife_Config.KnifeExecutionCritDamageValue;
            var critChance = Knife_Config.KnifeExecutionCritChanceValue;
            var requiredPoints = Knife_Config.KnifeExecutionRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_execution")}</size></color>",
                L.Get("knife_desc_execution", critDamage, critChance),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 암살술 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeAssassinationTooltip()
        {
            var staggerChance = Knife_Config.KnifeAssassinationStaggerChanceValue;
            var requiredHits = Knife_Config.KnifeAssassinationRequiredHitsValue;
            var requiredPoints = Knife_Config.KnifeAssassinationRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_assassination")}</size></color>",
                L.Get("knife_desc_assassination", staggerChance, requiredHits),
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.requirement = L.Get("requirement_knife_claw");

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 암살자의 심장 툴팁 생성 (G키 액티브 스킬) - 개선된 상세 툴팁
        /// 순간이동 + 스턴 + 연속 공격 + 버프 효과 포함
        /// </summary>
        public static string GetKnifeAssassinHeartTooltip()
        {
            var critDamage = Knife_Config.KnifeAssassinHeartCritDamageValue;
            var duration = Knife_Config.KnifeAssassinHeartDurationValue;
            var staminaCost = Knife_Config.KnifeAssassinHeartStaminaCostValue;
            var cooldown = Knife_Config.KnifeAssassinHeartCooldownValue;
            var requiredPoints = Knife_Config.KnifeAssassinHeartRequiredPointsValue;
            var teleportRange = Knife_Config.KnifeAssassinHeartTeleportRangeValue;
            var teleportBehind = Knife_Config.KnifeAssassinHeartTeleportBehindValue;
            var stunDuration = Knife_Config.KnifeAssassinHeartStunDurationValue;
            var attackCount = Knife_Config.KnifeAssassinHeartAttackCountValue;

            var data = MeleeTooltipUtils.CreateActiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("knife_skill_assassin")}</size></color>",
                L.Get("knife_desc_assassin_main", teleportRange, teleportBehind, stunDuration, attackCount, duration, critDamage),
                $"{staminaCost}",
                $"{cooldown}{L.Get("unit_seconds")}",
                MeleeTooltipUtils.WeaponType.Knife,
                L.Get("knife_desc_assassin_note")
            );
            data.requirement = L.Get("requirement_knife_claw");
            data.requiredPoints = requiredPoints.ToString();
            data.skillType = L.Get("skill_type_active_key", "G");
            data.confirmation = $"{L.Get("tooltip_same_weapon_only")}\n{L.Get("knife_desc_assassin_note2", teleportRange)}";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        #endregion

        #region 통합 툴팁 생성

        /// <summary>
        /// 단검 툴팁 생성 (공통 유틸리티 사용)
        /// </summary>
        public static string GenerateKnifeTooltip(MeleeTooltipUtils.MeleeTooltipData data)
        {
            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        #endregion

        #region 스킬 매핑

        /// <summary>
        /// 단검 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> KnifeSkillMappings = new()
        {
            { "knife_expert_backstab", GetKnifeExpertTooltip },
            { "knife_step2_evasion", GetKnifeEvasionTooltip },
            { "knife_step3_move_speed", GetKnifeMoveSpeedTooltip },
            { "knife_step4_attack_damage", GetKnifeAttackSpeedTooltip },
            { "knife_step5_crit_rate", GetKnifeCritRateTooltip },
            { "knife_step6_combat_damage", GetKnifeCombatDamageTooltip },
            { "knife_step7_execution", GetKnifeExecutionTooltip },
            { "knife_step8_assassination", GetKnifeAssassinationTooltip },
            { "knife_step9_assassin_heart", GetKnifeAssassinHeartTooltip }
        };

        #endregion

        #region 툴팁 업데이트 관리

        /// <summary>
        /// 모든 단검 스킬 툴팁 업데이트
        /// </summary>
        public static void UpdateKnifeTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(KnifeSkillMappings, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 개별 단검 스킬 툴팁 업데이트
        /// </summary>
        private static void UpdateIndividualKnifeTooltip(string skillId, string newTooltip)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey(skillId))
                {
                    var skillNode = manager.SkillNodes[skillId];
                    var oldDescription = skillNode.Description;
                    skillNode.Description = newTooltip;
                    
                    Plugin.Log.LogDebug($"[단검 툴팁] {skillId} 업데이트 완료");
                }
                else
                {
                    Plugin.Log.LogWarning($"[단검 툴팁] {skillId} 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 툴팁] {skillId} 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 특정 단검 스킬 툴팁 가져오기
        /// </summary>
        public static string GetKnifeSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, KnifeSkillMappings, MeleeTooltipUtils.WeaponType.Knife);
        }

        #endregion
    }
}
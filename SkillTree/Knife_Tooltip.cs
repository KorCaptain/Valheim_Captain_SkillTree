using System;
using System.Collections.Generic;
using UnityEngine;

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
                "<color=#FFD700><size=22>단검 전문가</size></color>",
                $"적의 뒤에서 공격 시 피해 +{backstabBonus}%",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "단검의 기본 전문 기술";
            data.requirement = "단검 또는 클로 착용";
            
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
                "<color=#FFD700><size=22>회피 숙련</size></color>",
                $"회피 확률 +{evasionBonus}%",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "위험한 상황에서의 생존 기술";
            data.requirement = "단검 또는 클로 착용";

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
                "<color=#FFD700><size=22>빠른 움직임</size></color>",
                $"이동속도 +{speedBonus}%",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "빠른 이동으로 전술적 우위 확보";
            data.requirement = "단검 또는 클로 착용";

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
                "<color=#FFD700><size=22>빠른 공격</size></color>",
                $"공격력 +{damageBonus}",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "단검 공격력 강화";
            data.requirement = "단검 또는 클로 착용";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 치명타 숙련 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeCritRateTooltip()
        {
            var critBonus = Knife_Config.KnifeCritRateBonusValue;
            var requiredPoints = Knife_Config.KnifeCritRateRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>치명타 숙련</size></color>",
                $"치명타 확률 +{critBonus}%",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "치명타 확률 증가";
            data.requirement = "단검 또는 클로 착용";

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
                "<color=#FFD700><size=22>치명적 피해</size></color>",
                $"공격력 +{damageBonus}%",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "단검 공격력 증가";
            data.requirement = "단검 또는 클로 착용";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 암살자 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeExecutionTooltip()
        {
            var critDamage = Knife_Config.KnifeExecutionCritDamageValue;
            var staggerBonus = Knife_Config.KnifeExecutionStaggerBonusValue;
            var requiredPoints = Knife_Config.KnifeExecutionRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>암살자</size></color>",
                $"치명타 피해 +{critDamage}%, 비틀거림 공격력 +{staggerBonus}%",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "암살자의 치명적인 공격 능력 강화";
            data.requirement = "단검 또는 클로 착용";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 암살술 툴팁 생성 - 개선된 상세 툴팁
        /// </summary>
        public static string GetKnifeAssassinationTooltip()
        {
            var backstabBonus = Knife_Config.KnifeAssassinationCritMultiplierValue;
            var requiredPoints = Knife_Config.KnifeAssassinationRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>암살술</size></color>",
                $"백스탭 공격력 +{backstabBonus}%",
                MeleeTooltipUtils.WeaponType.Knife
            );
            data.requiredPoints = requiredPoints.ToString();
            data.additionalInfo = "적의 뒤에서 강력한 타격";
            data.requirement = "단검 또는 클로 착용";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Knife);
        }

        /// <summary>
        /// 암살자의 심장 툴팁 생성 (G키 액티브 스킬) - 개선된 상세 툴팁
        /// 순간이동 + 버프 효과 포함
        /// </summary>
        public static string GetKnifeAssassinHeartTooltip()
        {
            var damageBonus = Knife_Config.KnifeAssassinHeartDamageBonusValue;
            var critChance = Knife_Config.KnifeAssassinHeartCritChanceValue;
            var critDamage = Knife_Config.KnifeAssassinHeartCritDamageValue;
            var duration = Knife_Config.KnifeAssassinHeartDurationValue;
            var staminaCost = Knife_Config.KnifeAssassinHeartStaminaCostValue;
            var cooldown = Knife_Config.KnifeAssassinHeartCooldownValue;
            var requiredPoints = Knife_Config.KnifeAssassinHeartRequiredPointsValue;
            var teleportRange = Knife_Config.KnifeAssassinHeartTeleportRangeValue;
            var teleportBehind = Knife_Config.KnifeAssassinHeartTeleportBehindValue;

            var critDamagePercent = (critDamage - 1) * 100; // 1.3배 → 30% 증가

            var data = MeleeTooltipUtils.CreateActiveSkillData(
                "<color=#FFD700><size=22>암살자의 심장</size></color>",
                $"정면 {teleportRange}m 내 적의 뒤({teleportBehind}m)로 순간이동\n{duration}초간 피해 +{damageBonus}%, 치명타 확률 +{critChance}%, 치명타 피해 +{critDamagePercent}%",
                $"{staminaCost}",
                $"{cooldown}초",
                MeleeTooltipUtils.WeaponType.Knife,
                "암살자의 모든 능력을 극한까지 끌어올리는 궁극기"
            );
            data.requirement = "단검 또는 클로 착용";
            data.requiredPoints = requiredPoints.ToString();
            data.skillType = "액티브 스킬 - G키";
            data.confirmation = "같은 무기 전문가 내에서만 다중 습득 가능\n정면에 적 없으면 스킬 취소";

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
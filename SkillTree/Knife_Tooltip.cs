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
                L.Get("knife_desc_attack_speed", damageBonus, damageBonus),
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
        /// 암살자의 심장 툴팁 (G키 액티브 스킬) — KnifeAssassinHeart_Tooltip.cs로 위임
        /// </summary>
        public static string GetKnifeAssassinHeartTooltip()
            => KnifeAssassinHeart_Tooltip.GetTooltip();

        /// <summary>
        /// 약점폭발 툴팁 생성 (H키 액티브 스킬) — Archer 형식 레벨 업그레이드 표시
        /// </summary>
        public static string GetKnifeStackExplosionTooltip()
        {
            var manager      = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("knife_step10_stack_explosion") ?? 0;
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
            int displayLevel = System.Math.Min(currentLevel + 1, 7);

            var staminaCost   = Knife_Config.KnifeStackExplosionStaminaCostValue;
            var cooldown      = Knife_Config.KnifeStackExplosionCooldownValue;
            var maxStacks     = (int)Knife_Config.KnifeStackExplosionMaxStacksValue;
            var stackDuration = Knife_Config.KnifeStackExplosionStackDurationValue;
            var buffDuration  = Knife_Config.KnifeStackExplosionBuffDurationValue;
            var aoePercent    = (int)Knife_Config.KnifeStackExplosionAoePercentValue;
            var reqPoints     = Knife_Config.KnifeStackExplosionRequiredPointsValue;

            int damage = GetStackDamageForLevel(mainLevel);

            var tooltip = $"<color=#FF6600><size=22>{L.Get("knife_skill_stack_explosion")}</size></color>\n";
            tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("knife_stack_explosion_effect", maxStacks, damage, aoePercent)}</size></color>\n";
            tooltip += $"<color=#E0E0E0><size=16>{L.Get("knife_stack_explosion_timing", stackDuration, buffDuration)}</size></color>\n";
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_stamina")}: </size></color>"
                     + $"<color=#FFDAB9><size=16>{staminaCost}</size></color>\n";
            tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>"
                     + $"<color=#FFDB58><size=16>{cooldown}{L.Get("unit_seconds")}</size></color>\n";
            tooltip += $"<color=#1E90FF><size=16>{L.Get("tooltip_skill_type")}: </size></color>"
                     + $"<color=#87CEEB><size=16>{L.Get("skill_type_active_key", "H")}</size></color>\n";
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirement")}: </size></color>"
                     + $"<color=#00FF00><size=16>{L.Get("requirement_knife_claw")}</size></color>\n";
            tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>"
                     + $"<color=#FF6B6B><size=16>{reqPoints}</size></color>\n";

            if (currentLevel < 7)
            {
                tooltip += $"<color=#FFA500><size=16>{L.Get("stack_explosion_next_level_req", displayLevel)}: </size></color>"
                         + $"<color=#FF6B6B><size=16>{GetStackExplosionLevelCostText(displayLevel)}</size></color>\n";
            }
            else
            {
                tooltip += $"<color=#FFD700><size=16>{L.Get("stack_explosion_max_level")}</size></color>\n";
            }

            if (mainLevel < 7)
            {
                tooltip += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 7; lv++)
                {
                    int pvDamage = GetStackDamageForLevel(lv);
                    tooltip += $"<color=#808080><size=14>Lv{lv} : {L.Get("knife_stack_explosion_preview", pvDamage, aoePercent)}</size></color>\n";
                }
            }

            return tooltip.TrimEnd('\n');
        }

        private static int GetStackDamageForLevel(int level)
        {
            int baseDmg  = (int)Knife_Config.KnifeStackExplosionDamagePercentValue;
            int perLevel = (int)Knife_Config.KnifeStackExplosionDamageLevelBonusValue;
            return baseDmg + (level - 1) * perLevel;
        }

        private static string GetStackExplosionLevelCostText(int level)
        {
            switch (level)
            {
                case 1: return $"{L.Get("item_trophy_eikthyr")} ×1 + {L.Get("item_trophy_troll")} ×1";
                case 2: return $"{L.Get("item_trophy_elder")} ×1 + {L.Get("item_trophy_skeleton")} ×1";
                case 3: return $"{L.Get("item_trophy_bonemass")} ×1 + {L.Get("item_trophy_wraith")} ×1";
                case 4: return $"{L.Get("item_trophy_dragonqueen")} ×1 + {L.Get("item_trophy_fenring")} ×1";
                case 5: return $"{L.Get("item_trophy_goblinking")} ×1 + {L.Get("item_trophy_deathsquito")} ×1";
                case 6: return $"{L.Get("item_trophy_seekerqueen")} ×1 + {L.Get("item_trophy_gjall")} ×1";
                case 7: return $"{L.Get("item_trophy_fader")} ×1 + {L.Get("item_trophy_charredarcher")} ×1";
                default: return "";
            }
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
            { "knife_step9_assassin_heart", GetKnifeAssassinHeartTooltip },
            { "knife_step10_stack_explosion", GetKnifeStackExplosionTooltip }
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
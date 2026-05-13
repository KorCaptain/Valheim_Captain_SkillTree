using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 검 스킬 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Sword_Tooltip
    {
        // MeleeTooltipUtils.MeleeTooltipData 사용
        // 기존 SwordTooltipData 제거

        /// <summary>
        /// 돌진 연속 베기 상세 툴팁 생성
        /// </summary>
        public static string GetSwordSlashTooltip()
        {
            Plugin.Log.LogDebug("[Sword Tooltip] GetSwordSlashTooltip() called");

            int currentLevel = SkillTreeManager.Instance?.GetSkillLevel("sword_step5_finalcut") ?? 0;
            float levelBonus = currentLevel > 0 ? (currentLevel - 1) * Sword_Config.RushSlashDamageLevelBonusValue : 0f;

            var skillData = Sword_Config.GetRushSlashData();
            float eff1 = skillData.damage1stRatio + levelBonus;
            float eff2 = skillData.damage2ndRatio + levelBonus;
            float eff3 = skillData.damage3rdRatio + levelBonus;

            string skillNameDisplay = currentLevel > 0
                ? $"<color=#4FC3F7><size=22>{L.Get("sword_skill_rush_slash")} [Lv{currentLevel}/7]</size></color>"
                : $"<color=#FFD700><size=22>{L.Get("sword_skill_rush_slash")}</size></color>";

            string description = L.Get("sword_desc_rush_slash", skillData.initialDistance) + "\n" +
                                $"<color=#98FB98>{L.Get("sword_desc_rush_slash_1st", eff1)}</color>\n" +
                                $"<color=#FFA500>{L.Get("sword_desc_rush_slash_2nd", eff2)}</color>\n" +
                                $"<color=#FF6B6B>{L.Get("sword_desc_rush_slash_3rd", eff3)}</color>\n" +
                                $"<color=#87CEEB>{L.Get("sword_desc_rush_slash_path")}</color>";

            var data = MeleeTooltipUtils.CreateActiveSkillData(
                skillNameDisplay,
                description,
                $"{skillData.staminaCost}",
                $"{skillData.cooldown}{L.Get("unit_seconds")}",
                MeleeTooltipUtils.WeaponType.Sword,
                $"{L.Get("tooltip_same_weapon_only")}\n{L.Get("tooltip_not_invincible")}",
                "",
                "G"
            );
            data.requiredPoints = "3";

            string finalTooltip = MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
            string levelInfo = BuildRushSlashLevelInfo(currentLevel);
            Plugin.Log.LogDebug($"[Sword Tooltip] Final tooltip generated - length: {finalTooltip?.Length ?? 0}");
            return string.IsNullOrEmpty(levelInfo) ? finalTooltip : finalTooltip + "\n" + levelInfo;
        }

        private static string BuildRushSlashLevelInfo(int currentLevel)
        {
            if (currentLevel <= 0) return "";
            if (currentLevel >= 7)
                return $"<color=#FFD700><size=15>★ {L.Get("rush_slash_max_level")}</size></color>";
            int nextLevel = currentLevel + 1;
            var missing = SkillTreeManager.Instance?.GetMissingRushSlashItems(nextLevel);
            string req = $"<color=#FFD700><size=15>▶ {L.Get("rush_slash_next_level_req", nextLevel)}</size></color>";
            if (missing != null && missing.Count > 0)
                req += $"\n<color=#FF6B6B><size=14>  {L.Get("rush_slash_missing_items", string.Join(", ", missing))}</size></color>";
            else
                req += $"\n<color=#00FF00><size=14>  ✅ {L.Get("rush_slash_upgrade_ready")}</size></color>";
            return req;
        }

        /// <summary>
        /// 검 전문가 툴팁 생성
        /// </summary>
        public static string GetSwordExpertTooltip()
        {
            Plugin.Log.LogDebug("[Sword Tooltip] GetSwordExpertTooltip() called");

            var requiredPoints = 2;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_expert")}</size></color>",
                L.Get("sword_desc_expert", Sword_Config.SwordExpertDamageValue, SkillTreeConfig.SwordStep1ExpertComboBonusValue, Sword_Config.SwordStep1ExpertDurationValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_sword_equip");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 빠른 베기 툴팁 생성 (sword_step1_fastslash)
        /// </summary>
        public static string GetFastSlashTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetFastSlashTooltip() 호출됨");

            var requiredPoints = 2;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_fast_slash")}</size></color>",
                L.Get("sword_desc_fast_slash", SkillTreeConfig.SwordStep1FastSlashSpeedValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_sword_equip");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 반격 자세 툴팁 생성 (sword_step1_counter)
        /// </summary>
        public static string GetCounterTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetCounterTooltip() 호출됨");

            var requiredPoints = 3;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_counter")}</size></color>",
                L.Get("sword_desc_counter", SkillTreeConfig.SwordStep1CounterDurationValue, SkillTreeConfig.SwordStep1CounterDefenseBonusValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_sword_equip");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 연속베기 툴팁 생성 (sword_step2_combo)
        /// </summary>
        public static string GetComboTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetComboTooltip() 호출됨");

            var requiredPoints = 2;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_combo")}</size></color>",
                L.Get("sword_desc_combo", SkillTreeConfig.SwordStep2ComboSlashBonusValue, Sword_Config.SwordStep2ComboSlashDurationValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_sword_equip");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 칼날 되치기 툴팁 생성 (sword_step3_riposte)
        /// </summary>
        public static string GetRiposteTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetRiposteTooltip() 호출됨");

            var requiredPoints = 3;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_riposte")}</size></color>",
                L.Get("sword_desc_riposte", Sword_Config.SwordRiposteDamageBonusValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_sword_equip");
            data.additionalInfo = L.Get("tooltip_sword_damage_boost");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 공방일체 툴팁 생성 (sword_step3_allinone)
        /// </summary>
        public static string GetAllInOneTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetAllInOneTooltip() 호출됨");

            var requiredPoints = 2;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_all_in_one")}</size></color>",
                L.Get("sword_desc_all_in_one", SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue, SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_two_handed_sword");
            data.additionalInfo = L.Get("tooltip_sword_effect_note");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 진검승부 툴팁 생성 (sword_step4_duel)
        /// </summary>
        public static string GetDuelTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetDuelTooltip() 호출됨");

            var requiredPoints = 3;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_duel")}</size></color>",
                L.Get("sword_desc_duel", SkillTreeConfig.SwordStep4TrueDuelSpeedValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_sword_equip");
            data.additionalInfo = L.Get("tooltip_sword_effect_note");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 회오리베기 툴팁 생성 (sword_step5_defswitch)
        /// </summary>
        public static string GetDefSwitchTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetDefSwitchTooltip() 호출됨 (회오리베기)");

            float staminaCost = Sword_Config.ParryRushStaminaCostValue;
            float cooldown    = Sword_Config.ParryRushCooldownValue;
            const float r1 = 5f;
            const float r2 = 8f;
            const float r3 = 12f;
            const float d1 = 140f;
            const float d2 = 180f;
            const float d3 = 220f;

            string description = L.Get("sword_desc_parry_rush", r1, r2, r3) + "\n" +
                $"<color=#98FB98>{L.Get("sword_desc_parry_rush_damage", 1, d1)}</color>\n" +
                $"<color=#98FB98>{L.Get("sword_desc_parry_rush_damage", 2, d2)}</color>\n" +
                $"<color=#FFD700>{L.Get("sword_desc_parry_rush_damage", 3, d3)}</color>";

            var data = MeleeTooltipUtils.CreateActiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_parry_rush")}</size></color>",
                description,
                $"{staminaCost}",
                $"{cooldown}{L.Get("unit_seconds")}",
                MeleeTooltipUtils.WeaponType.Sword,
                L.Get("tooltip_same_weapon_only"),
                L.Get("requirement_sword_or_shield_equip"),
                "H"
            );
            data.requiredPoints = "3";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 궁극 베기 툴팁 생성 (sword_Step6_ultimate_slash)
        /// </summary>
        public static string GetUltimateSlashTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetUltimateSlashTooltip() 호출됨");

            var requiredPoints = 3;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_ultimate")}</size></color>",
                L.Get("sword_desc_ultimate", SkillTreeConfig.SwordStep6UltimateSlashMultiplierValue),
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = L.Get("requirement_sword_equip");
            data.additionalInfo = L.Get("tooltip_sword_effect_note");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }


        /// <summary>
        /// 검 스킬 일반 툴팁 생성 (전문가, 기본 스킬용)
        /// </summary>
        public static string GetSwordGeneralTooltip(string skillId)
        {
            try
            {
                Plugin.Log.LogDebug($"[검 툴팁] 일반 검 스킬 툴팁 요청 - skillId: {skillId}");

                switch (skillId)
                {
                    case "sword_expert":
                        return GetSwordExpertTooltip();

                    case "sword_step1_fastslash":
                        return GetFastSlashTooltip();

                    case "sword_step1_counter":
                        return GetCounterTooltip();

                    case "sword_step2_combo":
                        return GetComboTooltip();

                    case "sword_step3_riposte":
                        return GetRiposteTooltip();

                    case "sword_step3_allinone":
                        return GetAllInOneTooltip();

                    case "sword_step4_duel":
                        return GetDuelTooltip();

                    case "sword_step5_finalcut":
                    case "sword_slash":
                        return GetSwordSlashTooltip();

                    case "sword_step5_defswitch":
                        return GetDefSwitchTooltip();

                    case "sword_step6_ultimate":
                    case "sword_step6_ultimateslash":
                        return GetUltimateSlashTooltip();

                    default:
                        Plugin.Log.LogWarning($"[검 툴팁] 알 수 없는 스킬 ID: {skillId}");
                        return L.Get("tooltip_skill_not_found", L.Get("weapon_sword"));
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 툴팁] 일반 툴팁 생성 오류: {ex.Message}");
                return L.Get("tooltip_generation_error");
            }
        }

        // [DEPRECATED] - Use MeleeTooltipUtils.GenerateTooltip() instead
        private static string GenerateSwordTooltip(MeleeTooltipUtils.MeleeTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 설명 섹션
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#E0E0E0><size=16>{data.description}";
                    
                    // 추가 정보가 있으면 괄호로 추가
                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 소모 섹션 (있을 때만 표시)
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스테미나 {data.consumeStamina}</size></color>\n";
                }

                // 쿨타임 섹션 (액티브 스킬만)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 툴팁] 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }

        // [DEPRECATED] - Use MeleeTooltipUtils.GenerateTooltip() instead
        private static string GenerateSwordSlashTooltip(MeleeTooltipUtils.MeleeTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 설명 섹션
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#E0E0E0><size=16>{data.description}";

                    // 추가 정보가 있으면 괄호로 추가
                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 소모 섹션
                if (!string.IsNullOrEmpty(data.consumeStamina))
                {
                    tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {data.consumeStamina}</size></color>\n";
                }

                // 쿨타임 섹션 (액티브 스킬만)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 툴팁] Sword Slash 툴팁 생성 실패: {ex.Message}");
                return "툴팁 생성 오류";
            }
        }


        /// <summary>
        /// 돌진 연속 베기 대체 툴팁 (설정 로드 실패 시)
        /// </summary>
        private static string GetSwordSlashFallbackTooltip()
        {
            string description = L.Get("sword_desc_rush_slash", 5) + "\n" +
                                $"<color=#98FB98>{L.Get("sword_desc_rush_slash_1st", 70)}</color>\n" +
                                $"<color=#FFA500>{L.Get("sword_desc_rush_slash_2nd", 80)}</color>\n" +
                                $"<color=#FF6B6B>{L.Get("sword_desc_rush_slash_3rd", 90)}</color>\n" +
                                $"<color=#87CEEB>{L.Get("sword_desc_rush_slash_path")}</color>";

            var data = MeleeTooltipUtils.CreateActiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("sword_skill_rush_slash")}</size></color>",
                description,
                "30",
                $"25{L.Get("unit_seconds")}",
                MeleeTooltipUtils.WeaponType.Sword,
                L.Get("tooltip_not_invincible"),
                "",
                "G"
            );

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        #region 스킬 매핑

        /// <summary>
        /// 검 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> SwordSkillMappings = new()
        {
            { "sword_expert", GetSwordExpertTooltip },
            { "sword_step1_fastslash", GetFastSlashTooltip },
            { "sword_step1_counter", GetCounterTooltip },
            { "sword_step2_combo", GetComboTooltip },
            { "sword_step3_riposte", GetRiposteTooltip },
            { "sword_step3_allinone", GetAllInOneTooltip },
            { "sword_step4_duel", GetDuelTooltip },
            { "sword_step5_finalcut", GetSwordSlashTooltip },
            { "sword_step5_defswitch", GetDefSwitchTooltip }
        };

        #endregion

        /// <summary>
        /// 검 툴팁 강제 업데이트
        /// </summary>
        public static void UpdateSwordTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(SwordSkillMappings, MeleeTooltipUtils.WeaponType.Sword);
        }

        // MeleeTooltipUtils.UpdateSkillTooltip() 사용
        // 기존 UpdateIndividualSwordTooltip() 제거

        /// <summary>
        /// 특정 검 스킬 툴팁 가져오기
        /// </summary>
        public static string GetSwordSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, SwordSkillMappings, MeleeTooltipUtils.WeaponType.Sword);
        }
    }
}
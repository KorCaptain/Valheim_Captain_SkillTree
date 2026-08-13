using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폴암 스킬 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Polearm_Tooltip
    {
        // MeleeTooltipUtils.MeleeTooltipData 사용
        // 기존 PolearmTooltipData 제거

        /// <summary>
        /// 폴암 전문가 툴팁 생성
        /// </summary>
        public static string GetPolearmExpertTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmExpertTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_expert")}</size></color>",
                L.Get("polearm_desc_expert", SkillTreeConfig.PolearmExpertRangeBonusValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requirement = L.Get("requirement_polearm_equip");
            data.requiredPoints = Polearm_Config.PolearmExpertRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 회전베기 툴팁 생성
        /// </summary>
        public static string GetPolearmStep1SpinTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep1SpinTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_spin")}</size></color>",
                L.Get("polearm_desc_spin", SkillTreeConfig.PolearmStep1SpinWheelDamageValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = Polearm_Config.PolearmSpinWheelRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 제압 공격 툴팁 생성 (Tier 4-3)
        /// </summary>
        public static string GetPolearmStep1SuppressTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep1SuppressTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_suppress")}</size></color>",
                L.Get("polearm_desc_suppress", SkillTreeConfig.PolearmStep1SuppressDamageValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = Polearm_Config.PolearmSuppressRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 영웅 타격 툴팁 생성
        /// </summary>
        public static string GetPolearmStep2HeroTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep2HeroTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_hero")}</size></color>",
                L.Get("polearm_desc_hero", SkillTreeConfig.PolearmStep2HeroKnockbackChanceValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = Polearm_Config.PolearmHeroStrikeRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 광역 강타 툴팁 생성
        /// </summary>
        public static string GetPolearmStep3AreaTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep3AreaTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_area")}</size></color>",
                L.Get("polearm_desc_area", SkillTreeConfig.PolearmStep3AreaComboBonusValue, SkillTreeConfig.PolearmStep3AreaComboDurationValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = Polearm_Config.PolearmAreaComboRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 폭풍베기 툴팁 생성 (구 지면 강타)
        /// </summary>
        public static string GetPolearmStep3GroundTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep3GroundTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_ground")}</size></color>",
                L.Get("polearm_desc_ground", SkillTreeConfig.PolearmStep3StormSlashExplosionValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = Polearm_Config.PolearmGroundWheelRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 반달 베기 툴팁 생성
        /// </summary>
        public static string GetPolearmStep4MoonTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep4MoonTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_moon")}</size></color>",
                L.Get("polearm_desc_moon", SkillTreeConfig.PolearmStep4MoonRangeBonusValue, SkillTreeConfig.PolearmStep4MoonStaminaReductionValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = Polearm_Config.PolearmMoonSlashRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 폴암강화 툴팁 생성 (Tier 2-1)
        /// </summary>
        public static string GetPolearmStep4ChargeTooltip()
        {
            Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep4ChargeTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("polearm_skill_charge")}</size></color>",
                L.Get("polearm_desc_charge", SkillTreeConfig.PolearmStep4ChargeDamageBonusValue),
                MeleeTooltipUtils.WeaponType.Polearm
            );
            data.requiredPoints = Polearm_Config.PolearmPolearmBoostRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 관통 돌격 툴팁 생성 (G키 액티브 스킬, Lv1~7 레벨 연동)
        /// </summary>
        public static string GetPolearmStep5KingTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep5KingTooltip() 호출됨 (관통 돌격)");

                int currentLevel = SkillTreeManager.Instance?.GetSkillLevel("polearm_step5_king") ?? 0;
                float levelBonus = Polearm_Config.PolearmPierceChargeLevelBonusValue;
                float basePrimary = Polearm_Config.PolearmPierceChargePrimaryDamageValue;
                float dashDistance = Polearm_Config.PolearmPierceChargeDashDistanceValue;
                float aoeAngle = Polearm_Config.PolearmPierceChargeAoeAngleValue;
                float aoeRadius = Polearm_Config.PolearmPierceChargeAoeRadiusValue;
                float knockbackDist = Polearm_Config.PolearmPierceChargeKnockbackDistanceValue;
                float staminaCost = Polearm_Config.PolearmPierceChargeStaminaCostValue;
                float cooldown = Polearm_Config.PolearmPierceChargeCooldownValue;

                float currentPrimary = basePrimary + (currentLevel > 0 ? (currentLevel - 1) * levelBonus : 0f);
                float currentAoe = GetPierceChargeAoeForLevel(currentLevel > 0 ? currentLevel : 1);

                var tooltip = "";

                // 1. 스킬명
                string lvSuffix = currentLevel > 0 ? $" [Lv{currentLevel}/7]" : "";
                tooltip += $"<color=#FFD700><size=22>{L.Get("polearm_skill_king")}{lvSuffix}</size></color>\n\n";

                // 2. 현재 레벨 데미지 수치
                if (currentLevel > 0)
                    tooltip += $"<color=#AAAAAA><size=16>Lv{currentLevel} : {L.Get("pierce_charge_damage_preview", (int)currentPrimary, (int)currentAoe)}</size></color>\n";

                // 3. 설명
                tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{L.Get("polearm_desc_king", dashDistance)}</size></color>\n";

                // 4. 직격 데미지
                tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_first_hit")}: </size></color><color=#FFB6C1><size=16>{L.Get("polearm_desc_king_first", (int)currentPrimary)}</size></color>\n";

                // 5. AOE 데미지
                tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_aoe_knockback")}: </size></color><color=#FFB6C1><size=16>{L.Get("polearm_desc_king_aoe", (int)currentAoe, aoeAngle, aoeRadius)}</size></color>\n";

                // 6. 넉백 거리
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_knockback_distance")}: </size></color><color=#ADD8E6><size=16>{L.Get("polearm_desc_king_knockback", knockbackDist)}</size></color>\n";

                // 7. 소모
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {staminaCost:F0}</size></color>\n";

                // 8. 스킬유형
                tooltip += $"<color=#FF4500><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#00FF00><size=16>{L.Get("skill_type_active_key", "G")}</size></color>\n";

                // 9. 쿨타임
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{cooldown:F0}{L.Get("unit_seconds")}</size></color>\n";

                // 10. 필요조건
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{L.Get("requirement_polearm_equip")}</size></color>\n";

                // 11. 확인사항
                tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{L.Get("tooltip_same_weapon_only")}</size></color>\n";

                // 12. 필요포인트
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{Polearm_Config.PolearmKingRequiredPointsValue}</size></color>\n";

                // 13. 강화 필요 트로피 / 최대 레벨
                if (currentLevel < 7)
                {
                    int nextLevel = currentLevel + 1;
                    tooltip += $"\n<color=#FFB347><size=16>Lv{nextLevel} {L.Get("pierce_charge_upgrade_title")}: </size></color>";
                    tooltip += $"<color=#FF6B6B><size=16>{GetPierceChargeTrophyText(nextLevel)}</size></color>";
                }
                else if (currentLevel >= 7)
                {
                    tooltip += $"\n<color=#FFD700><size=16>★ {L.Get("pierce_charge_max_level")} ★</size></color>";
                }

                // 14. 레벨 프리뷰 (미획득 또는 최대 미만)
                if (currentLevel < 7)
                {
                    tooltip += "\n\n<color=#666666><size=14>────────────────────────────</size></color>";
                    int startLv = currentLevel <= 0 ? 2 : currentLevel + 1;
                    for (int lv = startLv; lv <= 7; lv++)
                    {
                        float p = basePrimary + (lv - 1) * levelBonus;
                        float a = GetPierceChargeAoeForLevel(lv);
                        tooltip += $"\n<color=#888888><size=14>Lv{lv} : {L.Get("pierce_charge_damage_preview", (int)p, (int)a)}</size></color>";
                    }
                }

                Plugin.Log.LogDebug($"[관통 돌격 툴팁] 완료 - 길이: {tooltip?.Length ?? 0}");
                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[관통 돌격 툴팁] 생성 실패: {ex.Message}");
                return $"<color=#FFD700><size=22>{L.Get("polearm_skill_king")}</size></color>\n\n<color=#E0E0E0><size=16>{L.Get("skill_type_active_key", "G")}\n{L.Get("tooltip_generation_error")}</size></color>";
            }
        }

        private static float GetPierceChargeAoeForLevel(int level) => level switch
        {
            2 => 175f, 3 => 200f, 4 => 225f,
            5 => 250f, 6 => 275f, 7 => 300f,
            _ => 150f
        };

        private static string GetPierceChargeTrophyText(int targetLevel) => targetLevel switch
        {
            1 => $"{L.Get("item_trophy_eikthyr")} x1 + {L.Get("item_trophy_boar")} x1",
            2 => $"{L.Get("item_trophy_elder")} x1 + {L.Get("item_trophy_troll")} x1",
            3 => $"{L.Get("item_trophy_bonemass")} x1 + {L.Get("item_trophy_abomination")} x1",
            4 => $"{L.Get("item_trophy_dragonqueen")} x1 + {L.Get("item_trophy_sgolem")} x1",
            5 => $"{L.Get("item_trophy_goblinking")} x1 + {L.Get("item_trophy_goblinshaman")} x1",
            6 => $"{L.Get("item_trophy_seekerqueen")} x1 + {L.Get("item_trophy_gjall")} x1",
            7 => $"{L.Get("item_trophy_fader")} x1 + {L.Get("item_trophy_fallenvalkyrie")} x1",
            _ => ""
        };

        // MeleeTooltipUtils.GenerateTooltip() 사용
        // 기존 GeneratePolearmTooltip() 제거
        // [DEPRECATED] - Use MeleeTooltipUtils.GenerateTooltip() instead
        private static string GeneratePolearmTooltip(MeleeTooltipUtils.MeleeTooltipData data)
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
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {data.consumeStamina}</size></color>\n";
                }

                // 쿨타임 섹션 (액티브 스킬만)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 필요조건 섹션
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 확인사항 섹션
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폴암 툴팁] 생성 실패: {ex.Message}");
                return L.Get("tooltip_generation_error");
            }
        }

        /// <summary>
        /// 휠윈드 툴팁 생성 (Mouse2 홀드 액티브 스킬)
        /// </summary>
        public static string GetPolearmStep6WhirlwindTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[폴암 툴팁] GetPolearmStep6WhirlwindTooltip() 호출됨 (휠윈드)");

                var manager = SkillTreeManager.Instance;
                int currentLevel = manager?.GetSkillLevel("polearm_step6_whirlwind") ?? 0;
                int mainLevel = currentLevel == 0 ? 1 : currentLevel;
                int displayLevel = System.Math.Min(currentLevel + 1, 7);

                float staminaPerCycle = Polearm_Config.PolearmWhirlwindStaminaPerSecValue;
                float cooldown = Polearm_Config.PolearmWhirlwindCooldownValue;
                float maxDuration = Polearm_Config.PolearmWhirlwindMaxDurationValue;

                var t = $"<color=#FFD700><size=22>{L.Get("polearm_skill_whirlwind")}</size></color>\n";

                t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("whirlwind_damage_preview", (int)GetWhirlwindHitPercentForLevel(mainLevel), GetWhirlwindAoePercentForLevel(mainLevel))}</size></color>\n";

                t += $"<color=#87CEFA><size=16>{L.Get("whirlwind_reduction_preview", (int)GetWhirlwindReductionPercentForLevel(mainLevel))}</size></color>\n";

                t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
                t += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {staminaPerCycle:F1}/{L.Get("unit_cycle")}</size></color>\n";

                t += $"<color=#FF4500><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
                t += $"<color=#00FF00><size=16>{L.Get("skill_type_active_key", "Mouse2")}</size></color>\n";

                t += $"<color=#FFA500><size=16>{L.Get("tooltip_max_duration")}: </size></color>";
                t += $"<color=#FFDB58><size=16>{maxDuration:F0}{L.Get("unit_seconds")}</size></color>\n";

                t += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>";
                t += $"<color=#FFE4B5><size=16>{L.Get("polearm_whirlwind_hold_notice", (int)maxDuration)}</size></color>\n";

                t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
                t += $"<color=#FFDB58><size=16>{cooldown:F0}{L.Get("unit_seconds")}</size></color>\n";

                t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
                t += $"<color=#00FF00><size=16>{L.Get("requirement_polearm_equip")}</size></color>\n";

                t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{Polearm_Config.PolearmWhirlwindRequiredPointsValue}</size></color>\n";

                if (currentLevel < 7)
                {
                    t += $"<color=#FFA500><size=16>{L.Get("whirlwind_upgrade_requires", displayLevel)}: </size></color>";
                    t += $"<color=#FF6B6B><size=16>{GetWhirlwindLevelItemText(displayLevel)}</size></color>\n";
                }
                else
                {
                    t += $"<color=#FFD700><size=16>{L.Get("whirlwind_max_level")}</size></color>\n";
                }

                if (mainLevel < 7)
                {
                    t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 7; lv++)
                    {
                        t += $"<color=#808080><size=14>Lv{lv} : {L.Get("whirlwind_damage_preview", (int)GetWhirlwindHitPercentForLevel(lv), GetWhirlwindAoePercentForLevel(lv))}</size></color>\n";
                        t += $"<color=#808080><size=14>{L.Get("whirlwind_reduction_preview", (int)GetWhirlwindReductionPercentForLevel(lv))}</size></color>\n";
                    }
                }

                Plugin.Log.LogDebug($"[휠윈드 툴팁] 최종 툴팁 생성 완료 - 길이: {t?.Length ?? 0}");
                return t.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폴암 툴팁] 휠윈드 생성 실패: {ex.Message}");
                return $"<color=#FFD700><size=22>{L.Get("polearm_skill_whirlwind")}</size></color>\n\n<color=#E0E0E0><size=16>{L.Get("skill_type_active_key", "Mouse2")}\n{L.Get("tooltip_generation_error")}</size></color>";
            }
        }

        private static float GetWhirlwindHitPercentForLevel(int level)
            => Polearm_Config.PolearmWhirlwindDamagePercentValue + (level - 1) * Polearm_Config.PolearmWhirlwindLevelBonusValue;

        private static float GetWhirlwindReductionPercentForLevel(int level)
            => Polearm_Config.PolearmWhirlwindDamageReductionPercentValue + (level - 1) * Polearm_Config.PolearmWhirlwindDamageReductionLevelBonusValue;

        private static int GetWhirlwindAoePercentForLevel(int level) => level switch
        {
            2 => 20, 3 => 25, 4 => 30,
            5 => 35, 6 => 40, 7 => 50,
            _ => 15
        };

        private static string GetWhirlwindLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_troll") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_abomination") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_sgolem") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_goblinshaman") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_gjall") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_fallenvalkyrie") + " x1";
                default: return "";
            }
        }

        #region 스킬 매핑

        /// <summary>
        /// 폴암 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> PolearmSkillMappings = new()
        {
            { "polearm_expert", GetPolearmExpertTooltip },
            { "polearm_step1_spin", GetPolearmStep1SpinTooltip },
            { "polearm_step1_suppress", GetPolearmStep1SuppressTooltip },
            { "polearm_step2_hero", GetPolearmStep2HeroTooltip },
            { "polearm_step3_area", GetPolearmStep3AreaTooltip },
            { "polearm_step3_ground", GetPolearmStep3GroundTooltip },
            { "polearm_step4_moon", GetPolearmStep4MoonTooltip },
            { "polearm_step4_charge", GetPolearmStep4ChargeTooltip },
            { "polearm_step5_king", GetPolearmStep5KingTooltip },
            { "polearm_step6_whirlwind", GetPolearmStep6WhirlwindTooltip }
        };

        #endregion

        /// <summary>
        /// 모든 폴암 스킬 툴팁 업데이트
        /// </summary>
        public static void UpdatePolearmTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(PolearmSkillMappings, MeleeTooltipUtils.WeaponType.Polearm);
        }

        /// <summary>
        /// 개별 폴암 스킬 툴팁 업데이트
        /// </summary>
        private static void UpdateIndividualPolearmTooltip(string skillId, string newTooltip)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager?.SkillNodes != null && manager.SkillNodes.ContainsKey(skillId))
                {
                    var skillNode = manager.SkillNodes[skillId];
                    skillNode.Description = newTooltip;
                    
                    Plugin.Log.LogDebug($"[폴암 툴팁] {skillId} 업데이트 완료");
                }
                else
                {
                    Plugin.Log.LogWarning($"[폴암 툴팁] {skillId} 스킬 노드를 찾을 수 없음");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폴암 툴팁] {skillId} 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 특정 폴암 스킬 툴팁 가져오기
        /// </summary>
        public static string GetPolearmSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, PolearmSkillMappings, MeleeTooltipUtils.WeaponType.Polearm);
        }
    }
}
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
            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("sword_step5_finalcut") ?? 0;
            int mainLevel = currentLevel == 0 ? 1 : currentLevel;
            int displayLevel = System.Math.Min(currentLevel + 1, 7);
            float levelBonus = (mainLevel - 1) * Sword_Config.RushSlashDamageLevelBonusValue;
            var skillData = Sword_Config.GetRushSlashData();
            float eff1 = skillData.damage1stRatio + levelBonus;
            float eff2 = skillData.damage2ndRatio + levelBonus;
            float eff3 = skillData.damage3rdRatio + levelBonus;

            var t = $"<color=#FFD700><size=22>{L.Get("sword_skill_rush_slash")}</size></color>\n";

            t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : " +
                 $"{L.Get("sword_desc_rush_slash_1st", (int)eff1)} / " +
                 $"{L.Get("sword_desc_rush_slash_2nd", (int)eff2)} / " +
                 $"{L.Get("sword_desc_rush_slash_3rd", (int)eff3)}</size></color>\n";

            t += $"<color=#E0E0E0><size=16>{L.Get("sword_desc_rush_slash", skillData.initialDistance)}</size></color>\n";
            t += $"<color=#87CEEB><size=16>{L.Get("sword_desc_rush_slash_path")}</size></color>\n";

            t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            t += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {(int)skillData.staminaCost}</size></color>\n";

            t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", SkillTreeConfig.GetHotKeyDisplayName(SkillTreeConfig.HotKeyG, "G"))}</size></color>\n";

            t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            t += $"<color=#FFDB58><size=16>{skillData.cooldown}{L.Get("unit_seconds")}</size></color>\n";

            t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            t += $"<color=#00FF00><size=16>{L.Get("requirement_sword_equip")}</size></color>\n";

            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
            t += $"<color=#FF6B6B><size=16>{Sword_Config.RushSlashRequiredPointsValue}</size></color>\n";

            if (currentLevel < 7)
            {
                t += $"<color=#FFA500><size=16>{L.Get("rush_slash_next_level_req", displayLevel)}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{GetRushSlashLevelItemText(displayLevel)}</size></color>\n";
            }
            else
            {
                t += $"<color=#FFD700><size=16>{L.Get("rush_slash_max_level")}</size></color>\n";
            }

            if (mainLevel < 7)
            {
                t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 7; lv++)
                {
                    float lvBonus = (lv - 1) * Sword_Config.RushSlashDamageLevelBonusValue;
                    float lvEff1 = skillData.damage1stRatio + lvBonus;
                    float lvEff2 = skillData.damage2ndRatio + lvBonus;
                    float lvEff3 = skillData.damage3rdRatio + lvBonus;
                    t += $"<color=#808080><size=14>Lv{lv} : " +
                         $"{L.Get("sword_desc_rush_slash_1st", (int)lvEff1)} / " +
                         $"{L.Get("sword_desc_rush_slash_2nd", (int)lvEff2)} / " +
                         $"{L.Get("sword_desc_rush_slash_3rd", (int)lvEff3)}</size></color>\n";
                }
            }

            return t.TrimEnd('\n');
        }

        private static string GetRushSlashLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_troll") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_draugrelite") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_sgolem") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_goblinshaman") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_seekerbrute") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_fallenvalkyrie") + " x1";
                default: return "";
            }
        }

        /// <summary>
        /// 검 전문가 툴팁 생성
        /// </summary>
        public static string GetSwordExpertTooltip()
        {
            Plugin.Log.LogDebug("[Sword Tooltip] GetSwordExpertTooltip() called");

            var requiredPoints = Sword_Config.SwordExpertRequiredPointsValue;

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

            var requiredPoints = Sword_Config.SwordStep1FastSlashRequiredPointsValue;

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

            var requiredPoints = Sword_Config.SwordStep1CounterRequiredPointsValue;

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

            var requiredPoints = Sword_Config.SwordStep2ComboSlashRequiredPointsValue;

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

            var requiredPoints = Sword_Config.SwordStep3RiposteRequiredPointsValue;

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

            var requiredPoints = Sword_Config.SwordStep3AllInOneRequiredPointsValue;

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

            var requiredPoints = Sword_Config.SwordStep4TrueDuelRequiredPointsValue;

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
        /// 회오리베기 툴팁 생성 (sword_step5_defswitch) — CrossbowOneShot 형식, Lv1~7
        /// </summary>
        public static string GetDefSwitchTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetDefSwitchTooltip() 호출됨 (회오리베기)");

            var manager = SkillTreeManager.Instance;
            int currentLevel = manager?.GetSkillLevel("sword_step5_defswitch") ?? 0;
            int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
            int displayLevel = Math.Min(currentLevel + 1, 7);

            const float r1 = 5f, r2 = 8f, r3 = 12f;
            const float ratio1 = 140f, ratio2 = 180f, ratio3 = 220f;
            float levelBonus = (mainLevel - 1) * Sword_Config.WhirlwindSlashLevelBonusValue;
            float scaledBase = Sword_Config.WhirlwindSlashBaseDamageValue + levelBonus;
            float d1 = scaledBase * (ratio1 / 100f);
            float d2 = scaledBase * (ratio2 / 100f);
            float d3 = scaledBase * (ratio3 / 100f);

            // 1. 스킬명
            var t = currentLevel > 0
                ? $"<color=#4FC3F7><size=22>{L.Get("sword_skill_parry_rush")} [Lv{currentLevel}/7]</size></color>\n"
                : $"<color=#FFD700><size=22>{L.Get("sword_skill_parry_rush")}</size></color>\n";

            // 2. Lv + 3단계 피해 요약
            t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {(int)d1}/{(int)d2}/{(int)d3} {L.Get("tooltip_damage")}, AOE {r1}m/{r2}m/{r3}m</size></color>\n";

            // 3. 소모 (스태미나)
            t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
            t += $"<color=#FFDAB9><size=16>{L.Get("stamina_format", (int)Sword_Config.ParryRushStaminaCostValue)}</size></color>\n";

            // 5. 스킬유형 (H키)
            t += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
            t += $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", SkillTreeConfig.GetHotKeyDisplayName(SkillTreeConfig.HotKeyH, "H"))}</size></color>\n";

            // 6. 쿨타임
            t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
            t += $"<color=#FFDB58><size=16>{Sword_Config.ParryRushCooldownValue}{L.Get("unit_seconds")}</size></color>\n";

            // 7. 필요조건
            t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
            t += $"<color=#00FF00><size=16>{L.Get("requirement_sword_or_shield_equip")}</size></color>\n";

            // 8. 필요포인트
            t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
            t += $"<color=#FF6B6B><size=16>{Sword_Config.SwordStep5DefenseSwitchRequiredPointsValue}</size></color>\n";

            // 9. 다음 레벨 트로피 조건 or 최대레벨
            if (currentLevel < 7)
            {
                t += $"<color=#FFA500><size=16>{L.Get("whirlwind_next_level_req", displayLevel)}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{GetWhirlwindLevelItemText(displayLevel)}</size></color>\n";
            }
            else
            {
                t += $"<color=#FFD700><size=16>{L.Get("whirlwind_max_level")}</size></color>\n";
            }

            // 10. 구분선 + Lv 프리뷰
            if (mainLevel < 7)
            {
                t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                for (int lv = mainLevel + 1; lv <= 7; lv++)
                {
                    float lvBase = Sword_Config.WhirlwindSlashBaseDamageValue + (lv - 1) * Sword_Config.WhirlwindSlashLevelBonusValue;
                    float lvD1 = lvBase * (ratio1 / 100f);
                    float lvD2 = lvBase * (ratio2 / 100f);
                    float lvD3 = lvBase * (ratio3 / 100f);
                    t += $"<color=#808080><size=14>Lv{lv} : {(int)lvD1}/{(int)lvD2}/{(int)lvD3} {L.Get("tooltip_damage")}, AOE {r1}m/{r2}m/{r3}m</size></color>\n";
                }
            }

            return t.TrimEnd('\n');
        }

        private static string GetWhirlwindLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_troll") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_draugrelite") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_sgolem") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_goblinshaman") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_seekerbrute") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_fallenvalkyrie") + " x1";
                default: return "";
            }
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
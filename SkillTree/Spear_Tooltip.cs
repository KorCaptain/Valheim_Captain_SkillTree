using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 창 스킬 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Spear_Tooltip
    {
        /// <summary>
        /// 창 툴팁 데이터 구조체
        /// </summary>
        public struct SpearTooltipData
        {
            public string skillName;          // 스킬명
            public string description;        // 설명
            public string additionalInfo;     // 추가 정보
            public string consumeStamina;     // 스태미나 소모
            public string skillType;          // 스킬 유형
            public string cooldown;           // 쿨타임
            public string requirement;        // 필요조건
            public string confirmation;       // 확인사항
        }

        /// <summary>
        /// 창 전문가 툴팁 생성
        /// </summary>
        public static string GetSpearExpertTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearExpertTooltip() 호출됨");

            var requiredPoints = 2; // 창 전문가 고정값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("spear_skill_expert")}</size></color>",
                L.Get("spear_desc_expert", Spear_Config.SpearExpertProcChanceValue, 100f + Spear_Config.SpearExpertSpeedBoostPercentValue),
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requirement = L.Get("requirement_spear_equip");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 투창 전문가 툴팁 생성 (패시브 스킬 - 쿨타임 없음)
        /// </summary>
        public static string GetSpearStep1ThrowTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep1ThrowTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("spear_skill_throw")}</size></color>",
                L.Get("spear_desc_throw", SkillTreeConfig.SpearStep2ThrowDamageValue),
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requirement = L.Get("requirement_one_hand_spear");
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 빠른 공격모션 툴팁 생성
        /// </summary>
        public static string GetSpearStep1CritTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep1CritTooltip() (빠른 공격모션) 호출됨");

            string motionVal = Spear_Config.SpearStep1AttackMotionValue;
            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("spear_skill_attack_motion")}</size></color>",
                L.Get("spear_desc_attack_motion", motionVal),
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = Spear_Config.SpearStep1RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 연격창 툴팁 생성 (Tier 3)
        /// </summary>
        public static string GetSpearStep3PierceTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep3PierceTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("spear_skill_pierce")}</size></color>",
                L.Get("spear_desc_pierce", SkillTreeConfig.SpearStep3PierceDamageBonusValue),
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = "2";

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 회피 찌르기 툴팁 생성 (Tier 4)
        /// </summary>
        public static string GetSpearStep2EvasionTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep2EvasionTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("spear_skill_evasion")}</size></color>",
                L.Get("spear_desc_evasion", Spear_Config.SpearStep3EvasionBonusValue, Spear_Config.SpearStep3EvasionStaminaReductionValue),
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = Spear_Config.SpearStep3RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 빠른창 툴팁 생성
        /// </summary>
        public static string GetSpearStep3QuickTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep3QuickTooltip() (빠른창) 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("spear_skill_explosion")}</size></color>",
                L.Get("spear_desc_explosion", Spear_Config.SpearQuickAttackSpeedValue),
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = Spear_Config.SpearStep3RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        /// <summary>
        /// 이연창 툴팁 생성
        /// </summary>
        public static string GetSpearStep4TripleTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep4TripleTooltip() (이연창) 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("spear_skill_dual")}</size></color>",
                L.Get("spear_desc_dual", Spear_Config.SpearDualDurationValue, Spear_Config.SpearDualDamageBonusValue),
                MeleeTooltipUtils.WeaponType.Spear
            );
            data.requiredPoints = Spear_Config.SpearStep4RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Spear);
        }

        public static string GetSpearStep5PenetrateTooltip()
        {
            Plugin.Log.LogDebug("[창 툴팁] GetSpearStep5PenetrateTooltip() 호출됨");

            try
            {
                var manager = SkillTreeManager.Instance;
                int currentLevel = manager?.GetSkillLevel("spear_Step5_penetrate") ?? 0;
                int mainLevel    = currentLevel == 0 ? 1 : currentLevel;
                int displayLevel = System.Math.Min(currentLevel + 1, 7);

                float singleDmg = Spear_Config.SpearPenetrateBaseDamageValue
                    + (currentLevel > 0 ? (currentLevel - 1) * Spear_Config.SpearPenetrateLevelDamageBonusValue : 0);
                float areaDmg   = Spear_Config.SpearPenetrateBaseAreaDamageValue
                    + (currentLevel > 0 ? (currentLevel - 1) * Spear_Config.SpearPenetrateAreaLevelBonusValue : 0);
                int charges     = (mainLevel + 1) / 2;

                // 1. 스킬명
                var t = $"<color=#FFD700><size=22>{L.Get("spear_skill_penetrate")}</size></color>\n\n";

                // 2. Lv + 핵심 효과
                t += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("spear_penetrate_lv1_desc", charges)}</size></color>\n";

                // 3. 소모
                t += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>";
                t += $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {Spear_Config.SpearStep6PenetrateStaminaCostValue}</size></color>\n";

                // 4. 스킬유형 (G키)
                t += $"<color=#FF4500><size=16>{L.Get("tooltip_skill_type")}: </size></color>";
                t += $"<color=#00FF00><size=16>{L.Get("skill_type_active_key", "G")}</size></color>\n";

                // 5. 쿨타임
                t += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>";
                t += $"<color=#FFDB58><size=16>{Spear_Config.SpearStep6PenetrateCooldownValue}{L.Get("unit_seconds")}</size></color>\n";

                // 6. 필요조건
                t += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>";
                t += $"<color=#00FF00><size=16>{L.Get("requirement_spear_equip")}</size></color>\n";

                // 7. 필요포인트
                t += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>";
                t += $"<color=#FF6B6B><size=16>{Spear_Config.SpearPenetrateRequiredPointsValue}</size></color>\n";

                // 8. 다음 레벨 강화 재료 or 최대레벨
                if (currentLevel < 7)
                {
                    t += $"<color=#FFA500><size=16>{L.Get("spear_penetrate_upgrade_requires", displayLevel)}: </size></color>";
                    t += $"<color=#FF6B6B><size=16>{GetSpearPenetrateLevelItemText(displayLevel)}</size></color>\n";
                }
                else
                {
                    t += $"<color=#FFD700><size=16>{L.Get("spear_penetrate_max_level")}</size></color>\n";
                }

                // 9. 구분선 + 레벨 프리뷰
                if (mainLevel < 7)
                {
                    t += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 7; lv++)
                    {
                        float lvSingle = Spear_Config.SpearPenetrateBaseDamageValue + (lv - 1) * Spear_Config.SpearPenetrateLevelDamageBonusValue;
                        float lvArea   = Spear_Config.SpearPenetrateBaseAreaDamageValue + (lv - 1) * Spear_Config.SpearPenetrateAreaLevelBonusValue;
                        int   lvCharge = (lv + 1) / 2;
                        t += $"<color=#808080><size=14>Lv{lv} : {L.Get("spear_penetrate_lv1_desc", lvCharge)}</size></color>\n";
                    }
                }

                return t.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[창 툴팁] GetSpearStep5PenetrateTooltip 생성 실패: {ex.Message}");
                return L.Get("tooltip_generation_error");
            }
        }

        private static string GetSpearPenetrateLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return L.Get("item_trophy_eikthyr") + " x1 + " + L.Get("item_trophy_boar") + " x1";
                case 2: return L.Get("item_trophy_elder") + " x1 + " + L.Get("item_trophy_greydwarfshaman") + " x1";
                case 3: return L.Get("item_trophy_bonemass") + " x1 + " + L.Get("item_trophy_surtling") + " x1";
                case 4: return L.Get("item_trophy_dragonqueen") + " x1 + " + L.Get("item_trophy_hatchling") + " x1";
                case 5: return L.Get("item_trophy_goblinking") + " x1 + " + L.Get("item_trophy_goblin") + " x1";
                case 6: return L.Get("item_trophy_seekerqueen") + " x1 + " + L.Get("item_trophy_seeker") + " x1";
                case 7: return L.Get("item_trophy_fader") + " x1 + " + L.Get("item_trophy_morgen") + " x1";
                default: return "";
            }
        }

        /// <summary>
        /// 연공창 액티브 스킬 툴팁 생성 (H키 액티브 스킬) — Lv1~7 레벨 시스템 반영
        /// </summary>
        public static string GetSpearEnhancedThrowTooltip()
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                int currentLevel = manager?.GetSkillLevel("spear_Step5_combo") ?? 0;
                int mainLevel = currentLevel == 0 ? 1 : currentLevel;
                int displayLevel = System.Math.Min(currentLevel + 1, 7);
                int maxUses = mainLevel + 2;
                float levelBonus = (mainLevel - 1) * Spear_Config.SpearComboLevelBonusValue;
                float damageBonus = Spear_Config.SpearStep6ComboDamageValue + levelBonus;

                var tooltip = "";

                // 1. 스킬명 (#FFD700, size=22)
                tooltip += $"<color=#FFD700><size=22>{L.Get("spear_skill_combo")}</size></color>\n\n";

                // 2. 현재 레벨 + 투창 횟수·데미지 (#E0E0E0, size=16)
                tooltip += $"<color=#E0E0E0><size=16>Lv{mainLevel} : {L.Get("spear_combo_lv1_desc", maxUses, (int)damageBonus)}</size></color>\n";

                // 3. 범위 (#87CEEB / #B0E0E6)
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{L.Get("spear_desc_combo_range", SkillTreeConfig.SpearStep2ThrowRangeValue)}</size></color>\n";

                // 4. 소모 (#FFB347 / #FFDAB9)
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {SkillTreeConfig.SpearStep2ThrowStaminaCostValue}</size></color>\n";

                // 4-1. 패시브: 자동 회수 (#90EE90 / #98FB98)
                tooltip += $"<color=#90EE90><size=16>{L.Get("tooltip_passive")}: </size></color><color=#98FB98><size=16>{L.Get("spear_combo_passive_retrieve")}</size></color>\n";

                // 5. 스킬유형 (H키 강조: #FF1493 / #00FFFF)
                tooltip += $"<color=#FF1493><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#00FFFF><size=16>{L.Get("skill_type_active_key", "H")}</size></color>\n";

                // 6. 쿨타임 (#FFA500 / #FFDB58)
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{SkillTreeConfig.SpearStep6ComboCooldownValue}{L.Get("unit_seconds")}</size></color>\n";

                // 7. 필요조건 (#98FB98 / #00FF00)
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{L.Get("requirement_spear_equip")}</size></color>\n";

                // 8. 확인사항 (#F0E68C / #FFE4B5)
                tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{L.Get("tooltip_same_weapon_only")}</size></color>\n";

                // 9. 필요포인트 (#87CEEB / #FF6B6B)
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{Spear_Config.SpearComboRequiredPointsValue}</size></color>\n";

                // 10. 다음 레벨 강화 재료 or 최대레벨
                if (currentLevel < 7)
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("spear_combo_upgrade_requires", displayLevel)}: </size></color>";
                    tooltip += $"<color=#FF6B6B><size=16>{GetSpearComboLevelItemText(displayLevel)}</size></color>\n";
                }
                else
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("spear_combo_max_level")}</size></color>\n";
                }

                // 11. 구분선 + 레벨 프리뷰
                if (mainLevel < 7)
                {
                    tooltip += $"<color=#808080><size=14>────────────────────────────────────</size></color>\n";
                    for (int lv = mainLevel + 1; lv <= 7; lv++)
                    {
                        int lvUses = lv + 2;
                        float lvBonus = Spear_Config.SpearStep6ComboDamageValue + (lv - 1) * Spear_Config.SpearComboLevelBonusValue;
                        tooltip += $"<color=#808080><size=14>Lv{lv} : {L.Get("spear_combo_lv1_desc", lvUses, (int)lvBonus)}</size></color>\n";
                    }
                }

                return tooltip.TrimEnd('\n');
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[연공창 툴팁] 생성 실패: {ex.Message}");
                return L.Get("tooltip_generation_error");
            }
        }

        private static string GetSpearComboLevelItemText(int targetLevel)
        {
            switch (targetLevel)
            {
                case 1: return $"{L.Get("item_trophy_eikthyr")} x1 + {L.Get("item_trophy_boar")} x1";
                case 2: return $"{L.Get("item_trophy_elder")} x1 + {L.Get("item_trophy_troll")} x1";
                case 3: return $"{L.Get("item_trophy_bonemass")} x1 + {L.Get("item_trophy_abomination")} x1";
                case 4: return $"{L.Get("item_trophy_dragonqueen")} x1 + {L.Get("item_trophy_sgolem")} x1";
                case 5: return $"{L.Get("item_trophy_goblinking")} x1 + {L.Get("item_trophy_goblinshaman")} x1";
                case 6: return $"{L.Get("item_trophy_seekerqueen")} x1 + {L.Get("item_trophy_gjall")} x1";
                case 7: return $"{L.Get("item_trophy_fader")} x1 + {L.Get("item_trophy_fallenvalkyrie")} x1";
                default: return "";
            }
        }

        /// <summary>
        /// 창 툴팁 생성 (구버전 스타일)
        /// </summary>
        public static string GenerateSpearTooltip(SpearTooltipData data)
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
                Plugin.Log.LogError($"[창 툴팁] 생성 실패: {ex.Message}");
                return L.Get("tooltip_generation_error");
            }
        }

        #region 스킬 매핑

        /// <summary>
        /// 창 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> SpearSkillMappings = new()
        {
            { "spear_expert", GetSpearExpertTooltip },
            { "spear_Step1_throw", GetSpearStep1ThrowTooltip },
            { "spear_Step1_crit", GetSpearStep1CritTooltip },
            { "spear_Step2_evasion", GetSpearStep2EvasionTooltip },
            { "spear_Step3_pierce", GetSpearStep3PierceTooltip },
            { "spear_Step3_quick", GetSpearStep3QuickTooltip },
            { "spear_Step4_triple", GetSpearStep4TripleTooltip },
            { "spear_Step5_penetrate", GetSpearStep5PenetrateTooltip },
            { "spear_Step5_combo", GetSpearEnhancedThrowTooltip }
        };

        #endregion

        /// <summary>
        /// 모든 창 스킬 툴팁 업데이트
        /// </summary>
        public static void UpdateSpearTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(SpearSkillMappings, MeleeTooltipUtils.WeaponType.Spear);
        }

        // MeleeTooltipUtils.UpdateSkillTooltip() 사용
        // 기존 UpdateIndividualSpearTooltip() 제거

        /// <summary>
        /// 특정 창 스킬 툴팁 가져오기
        /// </summary>
        public static string GetSpearSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, SpearSkillMappings, MeleeTooltipUtils.WeaponType.Spear);
        }
    }
}
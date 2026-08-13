using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기 스킬 전용 툴팁 시스템
    /// 컨피그 시스템과 연동하여 동적 값을 표시
    /// </summary>
    public static class Mace_Tooltip
    {
        // MeleeTooltipUtils.MeleeTooltipData 사용
        // 기존 MaceTooltipData 제거

        /// <summary>
        /// 둔기 전문가 툴팁 생성
        /// </summary>
        public static string GetMaceExpertTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceExpertTooltip() 호출됨");

            var requiredPoints = Mace_Config.MaceExpertRequiredPointsValue;

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_expert")}</size></color>",
                L.Get("mace_desc_expert", Mace_Config.MaceExpertDamageBonusValue, Mace_Config.MaceExpertStunChanceValue, Mace_Config.MaceExpertStunDurationValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requirement = L.Get("requirement_mace_equip");
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 공격력 강화 툴팁 생성
        /// </summary>
        public static string GetMaceStep1DamageTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep1DamageTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_damage_boost")}</size></color>",
                L.Get("mace_desc_damage_boost", Mace_Config.MaceStep1DamageBonusValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep1RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 2단계 기절 강화 툴팁 생성
        /// </summary>
        public static string GetMaceStep2StunBoostTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep2StunBoostTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_stun_boost")}</size></color>",
                L.Get("mace_desc_stun_boost", Mace_Config.MaceStep2StunChanceBonusValue, Mace_Config.MaceStep2StunDurationBonusValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep2RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 3단계 방어 분기 툴팁 생성
        /// </summary>
        public static string GetMaceStep3BranchGuardTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep3BranchGuardTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_guard_boost")}</size></color>",
                L.Get("mace_desc_guard_boost", Mace_Config.MaceStep3SpinDamageBonusValue, Mace_Config.MaceStep3SpinRangeValue, Mace_Config.MaceStep3SpinKnockbackForceValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep3GuardRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 3단계 무거운 분기 툴팁 생성
        /// </summary>
        public static string GetMaceStep3BranchHeavyTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep3BranchHeavyTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_heavy_strike")}</size></color>",
                L.Get("mace_desc_heavy_strike", Mace_Config.MaceStep3HeavyDamageBonusValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep3HeavyRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 4단계 밀어내기 툴팁 생성
        /// </summary>
        public static string GetMaceStep4PushTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep4PushTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_knockback")}</size></color>",
                L.Get("mace_desc_knockback", Mace_Config.MaceStep4KnockbackChanceValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep4RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 5단계 탱커 툴팁 생성
        /// </summary>
        public static string GetMaceStep5TankTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep5TankTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_tanker")}</size></color>",
                L.Get("mace_desc_tanker", Mace_Config.MaceStep5TankHealthBonusValue, Mace_Config.MaceStep5TankDamageReductionValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep5TankRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 5단계 데미지 툴팁 생성
        /// </summary>
        public static string GetMaceStep5DpsTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep5DpsTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_dps_boost")}</size></color>",
                L.Get("mace_desc_dps_boost", Mace_Config.MaceStep5DpsDamageBonusValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep5DpsRequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 둔기 6단계 그랜드마스터 툴팁 생성
        /// </summary>
        public static string GetMaceStep6GrandmasterTooltip()
        {
            Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep6GrandmasterTooltip() 호출됨");

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                $"<color=#FFD700><size=22>{L.Get("mace_skill_grandmaster")}</size></color>",
                L.Get("mace_desc_grandmaster", Mace_Config.MaceStep6AttackSpeedBonusValue),
                MeleeTooltipUtils.WeaponType.Mace
            );
            data.requiredPoints = Mace_Config.MaceStep6RequiredPointsValue.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        /// <summary>
        /// 분노의 망치 툴팁 데이터 구조체
        /// </summary>
        public class FuryHammerTooltipData
        {
            public string skillName = "";
            public string description = "";
            public string additionalInfo = "";
            public string attackCount = "";
            public string baseDamage = "";
            public string damageIncrement = "";
            public string aoeRadius = "";
            public string attackInterval = "";
            public string staminaCost = "";
            public string cooldown = "";
            public string skillType = "";
            public string requirement = "";
            public string confirmation = "";
            public string requiredPoints = "";
        }

        /// <summary>
        /// 둔기 7단계 분노의 망치 툴팁 생성 (레벨 연동)
        /// </summary>
        public static string GetMaceStep7FuryHammerTooltip()
        {
            try
            {
                Plugin.Log.LogDebug("[둔기 툴팁] GetMaceStep7FuryHammerTooltip() 호출됨");

                int currentLevel = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_fury_hammer") ?? 0;
                float baseNormal = Mace_Config.FuryHammerNormalHitMultiplierValue;
                float baseFinal = Mace_Config.FuryHammerFinalHitMultiplierValue;
                float normalLvBonus = Mace_Config.FuryHammerNormalHitLevelBonusValue;
                float finalLvBonus = Mace_Config.FuryHammerFinalHitLevelBonusValue;
                float aoeRadius = Mace_Config.FuryHammerAoeRadiusValue;
                float staminaCost = Mace_Config.FuryHammerStaminaCostValue;
                float cooldown = Mace_Config.FuryHammerCooldownValue;
                int requiredPoints = Mace_Config.FuryHammerRequiredPointsValue;

                int dispLevel = currentLevel > 0 ? currentLevel : 1;
                float normalHit = baseNormal + (dispLevel - 1) * normalLvBonus;
                float finalHit = baseFinal + (dispLevel - 1) * finalLvBonus;

                var tooltip = $"<color=#FFD700><size=22>{L.Get("mace_skill_fury")}</size></color>\n\n";

                if (currentLevel > 0)
                {
                    tooltip += $"<color=#87CEEB><size=16>Lv{currentLevel}: </size></color>" +
                               $"<color=#E0E0E0><size=16>{L.Get("fury_hammer_damage_preview", (int)normalHit, (int)finalHit)}</size></color>\n";
                }
                else
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color>" +
                               $"<color=#E0E0E0><size=16>{L.Get("mace_desc_fury_action")}</size></color>\n";
                }

                tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color>" +
                           $"<color=#FFB6C1><size=16>{L.Get("mace_desc_fury_damage", (int)normalHit, (int)finalHit)}</size></color>\n";
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color>" +
                           $"<color=#B0E0E6><size=16>{aoeRadius:F0}{L.Get("unit_meter")}</size></color>\n";
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>" +
                           $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {staminaCost:F0}</size></color>\n";
                tooltip += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color>" +
                           $"<color=#FFD700><size=16>{L.Get("skill_type_active_key", "H")}</size></color>\n";
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>" +
                           $"<color=#FFDB58><size=16>{cooldown:F0}{L.Get("unit_seconds")}</size></color>\n";
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>" +
                           $"<color=#00FF00><size=16>{L.Get("requirement_two_hand_mace")}</size></color>\n";
                tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color>" +
                           $"<color=#FFE4B5><size=16>{L.Get("tooltip_same_weapon_only")}</size></color>\n";
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>" +
                           $"<color=#FF6B6B><size=16>{requiredPoints}</size></color>\n";

                if (currentLevel < 7)
                {
                    int nextLevel = (currentLevel > 0 ? currentLevel : 0) + 1;
                    string itemText = GetShieldChargeItemText(nextLevel);
                    tooltip += $"<color=#FFB347><size=16>{L.Get("fury_hammer_upgrade_requires", nextLevel)}: </size></color>" +
                               $"<color=#FF6B6B><size=16>{itemText}</size></color>\n";
                }
                else
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("fury_hammer_max_level")}</size></color>\n";
                }

                if (currentLevel < 7)
                {
                    tooltip += "\n\n<color=#A9A9A9><size=14>────────────────────────</size></color>";
                    int startPreview = currentLevel > 0 ? currentLevel + 1 : 2;
                    for (int lv = startPreview; lv <= 7; lv++)
                    {
                        float n = baseNormal + (lv - 1) * normalLvBonus;
                        float f = baseFinal + (lv - 1) * finalLvBonus;
                        tooltip += $"\n<color=#888888><size=14>Lv{lv}: {L.Get("fury_hammer_damage_preview", (int)n, (int)f)}</size></color>";
                    }
                }

                Plugin.Log.LogDebug($"[분노의 망치 툴팁] 생성 완료 Lv{currentLevel}");
                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[분노의 망치 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetFuryHammerFallbackTooltip();
            }
        }

        /// <summary>
        /// 분노의 망치 툴팁 생성 (H키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형(H키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        private static string GenerateFuryHammerTooltip(FuryHammerTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 1. 스킬명 (#FFD700, size=22)
                if (!string.IsNullOrEmpty(data.skillName))
                {
                    tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";
                }

                // 2. 설명 (#FFD700 / #E0E0E0)
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{data.description}";

                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 3. 데미지 (#FF6B6B / #FFB6C1)
                if (!string.IsNullOrEmpty(data.baseDamage))
                {
                    tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color><color=#FFB6C1><size=16>{data.baseDamage}</size></color>\n";
                }

                // 4. 범위 (#87CEEB / #B0E0E6) - AOE 범위
                if (!string.IsNullOrEmpty(data.aoeRadius))
                {
                    tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{data.aoeRadius}</size></color>\n";
                }

                // 5. 소모 (#FFB347 / #FFDAB9)
                if (!string.IsNullOrEmpty(data.staminaCost))
                {
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {data.staminaCost}</size></color>\n";
                }

                // 6. 스킬유형 (H키 강조: #FF1493 / #00FFFF)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#FF1493><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#00FFFF><size=16>{data.skillType}</size></color>\n";
                }

                // 7. 쿨타임 (#FFA500 / #FFDB58)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 8. 필요조건 (#98FB98 / #00FF00)
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 9. 확인사항 (#F0E68C / #FFE4B5)
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 10. 필요포인트 (#87CEEB / #FF6B6B)
                if (!string.IsNullOrEmpty(data.requiredPoints))
                {
                    tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>{data.requiredPoints}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[분노의 망치 툴팁] 구조화된 툴팁 생성 실패: {ex.Message}");
                return GetFuryHammerFallbackTooltip();
            }
        }

        /// <summary>
        /// 분노의 망치 백업 툴팁 (오류 시 사용)
        /// </summary>
        private static string GetFuryHammerFallbackTooltip()
        {
            return $"<color=#FFD700><size=22>{L.Get("mace_skill_fury")}</size></color>\n\n" +
                   $"<color=#E0E0E0><size=16>H: {L.Get("mace_desc_fury_attack", 5)} ({L.Get("mace_desc_fury_interval", 0.8f, 0.5f)})\n\n" +
                   $"• {L.Get("tooltip_damage")}: {L.Get("mace_desc_fury_damage", 80, 150)}\n" +
                   $"• {L.Get("tooltip_range")}: 5{L.Get("unit_meter")}\n" +
                   $"• {L.Get("tooltip_cost")}: {L.Get("stat_stamina")} 40\n" +
                   $"• {L.Get("tooltip_cooldown")}: 30{L.Get("unit_seconds")}\n" +
                   $"• {L.Get("tooltip_requirements")}: {L.Get("requirement_mace_equip")}\n\n" +
                   $"💥 {L.Get("tooltip_skill_type")}: {L.Get("skill_type_active_key", "H")}\n\n" +
                   $"{L.Get("tooltip_notice")}: {L.Get("tooltip_same_weapon_only")}</size></color>";
        }

        /// <summary>
        /// 수호자의 진심 툴팁 데이터 구조체
        /// </summary>
        public class GuardianHeartTooltipData
        {
            public string skillName = "";
            public string description = "";
            public string additionalInfo = "";
            public string dashDistance = "";
            public string damagePercent = "";
            public string multiHitInfo = "";
            public string staminaCost = "";
            public string cooldown = "";
            public string skillType = "";
            public string requirement = "";
            public string confirmation = "";
            public string specialNote = "";
        }

        /// <summary>
        /// 수호자의 진심 툴팁 생성 (레벨 연동)
        /// </summary>
        public static string GetMaceStep7GuardianHeartTooltip()
        {
            try
            {
                int currentLevel = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_guardian_heart") ?? 0;
                float baseDmg = Defense_Config.ShieldChargeDamagePercentValue;
                float levelBonus = Defense_Config.ShieldChargeLevelBonusValue;
                float staminaCost = Defense_Config.GuardianHeartStaminaCostValue;
                float cooldown = Defense_Config.GuardianHeartCooldownValue;
                int requiredPoints = Defense_Config.GuardianHeartRequiredPointsValue;

                int dispLevel = currentLevel > 0 ? currentLevel : 1;
                float singleDmg = baseDmg + (dispLevel - 1) * levelBonus;
                float multiDmg = GetShieldChargeMultiHitPercent(dispLevel);

                var tooltip = $"<color=#FFD700><size=22>{L.Get("mace_skill_guardian")}</size></color>\n\n";

                tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color>" +
                           $"<color=#E0E0E0><size=16>{L.Get("mace_desc_guardian_buff")}</size></color>\n";
                tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color>" +
                           $"<color=#FFB6C1><size=16>{L.Get("shield_charge_damage_preview", (int)singleDmg, (int)multiDmg)}</size></color>\n";
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color>" +
                           $"<color=#B0E0E6><size=16>{L.Get("mace_effect_buff")} 12m</size></color>\n";
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>" +
                           $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {staminaCost:F0}</size></color>\n";
                tooltip += $"<color=#FF4500><size=16>{L.Get("tooltip_skill_type")}: </size></color>" +
                           $"<color=#00FF00><size=16>{L.Get("skill_type_active_key", "Mouse2")}</size></color>\n";
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>" +
                           $"<color=#FFDB58><size=16>{cooldown:F0}{L.Get("unit_seconds")}</size></color>\n";
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>" +
                           $"<color=#00FF00><size=16>{L.Get("requirement_mace_shield")}</size></color>\n";
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_required_job")}: </size></color>" +
                           $"<color=#00FF00><size=16>{L.Get("job_name_tanker")}</size></color>\n";
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>" +
                           $"<color=#FF6B6B><size=16>{requiredPoints}</size></color>\n";

                if (currentLevel < 7)
                {
                    int nextLevel = (currentLevel > 0 ? currentLevel : 0) + 1;
                    string itemText = GetShieldChargeItemText(nextLevel);
                    tooltip += $"<color=#FFB347><size=16>{L.Get("shield_charge_upgrade_requires", nextLevel)}: </size></color>" +
                               $"<color=#FF6B6B><size=16>{itemText}</size></color>\n";
                }
                else
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("shield_charge_max_level")}</size></color>\n";
                }

                if (currentLevel < 7)
                {
                    tooltip += "\n\n<color=#A9A9A9><size=14>────────────────────────</size></color>";
                    int startPreview = currentLevel > 0 ? currentLevel + 1 : 2;
                    for (int lv = startPreview; lv <= 7; lv++)
                    {
                        float sd = baseDmg + (lv - 1) * levelBonus;
                        float md = GetShieldChargeMultiHitPercent(lv);
                        tooltip += $"\n<color=#888888><size=14>Lv{lv}: {L.Get("shield_charge_damage_preview", (int)sd, (int)md)}</size></color>";
                    }
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심 툴팁] 툴팁 생성 실패: {ex.Message}");
                return GetGuardianHeartFallbackTooltip();
            }
        }

        private static float GetShieldChargeMultiHitPercent(int level) =>
            Defense_Config.ShieldChargeMultiHitDamagePercentValue
            + (level - 1) * Defense_Config.ShieldChargeMultiHitLevelBonusValue;

        private static string GetShieldChargeItemText(int targetLevel) => targetLevel switch
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

        /// <summary>
        /// 충격파 강타 툴팁 생성 (레벨 연동, 방패돌진 툴팁과 동일한 순서/색상 구조)
        /// </summary>
        public static string GetMaceStep7ShockwaveSlamTooltip()
        {
            try
            {
                int currentLevel = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_shockwave_slam") ?? 0;
                float baseDmg = Mace_Config.ShockwaveSlamDamagePercentValue;
                float levelBonus = Mace_Config.ShockwaveSlamLevelBonusValue;
                float staminaCost = Mace_Config.ShockwaveSlamStaminaCostValue;
                float cooldown = Mace_Config.ShockwaveSlamCooldownValue;
                int requiredPoints = Mace_Config.ShockwaveSlamRequiredPointsValue;

                int dispLevel = currentLevel > 0 ? currentLevel : 1;
                float dmg = baseDmg + (dispLevel - 1) * levelBonus;

                var tooltip = $"<color=#FFD700><size=22>{L.Get("mace_skill_shockwave_slam")}</size></color>\n\n";

                tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color>" +
                           $"<color=#E0E0E0><size=16>{L.Get("mace_desc_shockwave_slam")}</size></color>\n";
                tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color>" +
                           $"<color=#FFB6C1><size=16>{L.Get("shockwave_slam_damage_preview", (int)dmg)}</size></color>\n";
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color>" +
                           $"<color=#B0E0E6><size=16>7m</size></color>\n";
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color>" +
                           $"<color=#FFDAB9><size=16>{L.Get("stat_stamina")} {staminaCost:F0}</size></color>\n";
                tooltip += $"<color=#FF4500><size=16>{L.Get("tooltip_skill_type")}: </size></color>" +
                           $"<color=#00FF00><size=16>{L.Get("skill_type_active_key", "G")}</size></color>\n";
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color>" +
                           $"<color=#FFDB58><size=16>{cooldown:F0}{L.Get("unit_seconds")}</size></color>\n";
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color>" +
                           $"<color=#00FF00><size=16>{L.Get("requirement_two_hand_mace")}</size></color>\n";
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color>" +
                           $"<color=#FF6B6B><size=16>{requiredPoints}</size></color>\n";

                if (currentLevel < 7)
                {
                    int nextLevel = (currentLevel > 0 ? currentLevel : 0) + 1;
                    string itemText = GetShieldChargeItemText(nextLevel);
                    tooltip += $"<color=#FFB347><size=16>{L.Get("shield_charge_upgrade_requires", nextLevel)}: </size></color>" +
                               $"<color=#FF6B6B><size=16>{itemText}</size></color>\n";
                }
                else
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("shockwave_slam_max_level")}</size></color>\n";
                }

                if (currentLevel < 7)
                {
                    tooltip += "\n\n<color=#A9A9A9><size=14>────────────────────────</size></color>";
                    int startPreview = currentLevel > 0 ? currentLevel + 1 : 2;
                    for (int lv = startPreview; lv <= 7; lv++)
                    {
                        float sd = baseDmg + (lv - 1) * levelBonus;
                        tooltip += $"\n<color=#888888><size=14>Lv{lv}: {L.Get("shockwave_slam_damage_preview", (int)sd)}</size></color>";
                    }
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[충격파 강타 툴팁] 툴팁 생성 실패: {ex.Message}");
                return $"<color=#FFD700><size=22>{L.Get("mace_skill_shockwave_slam")}</size></color>";
            }
        }

        /// <summary>
        /// 수호자의 진심 툴팁 생성 (G키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지/효과 → 범위 → 소모 → 스킬유형(G키 강조) → 쿨타임 → 필요조건 → 확인사항 → 필요포인트
        /// </summary>
        private static string GenerateGuardianHeartTooltip(GuardianHeartTooltipData data)
        {
            try
            {
                var tooltip = "";

                // 1. 스킬명 (#FFD700, size=22)
                if (!string.IsNullOrEmpty(data.skillName))
                {
                    tooltip += $"<color=#FFD700><size=22>{data.skillName}</size></color>\n\n";
                }

                // 2. 설명 (#FFD700 / #E0E0E0)
                if (!string.IsNullOrEmpty(data.description))
                {
                    tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{data.description}";

                    if (!string.IsNullOrEmpty(data.additionalInfo))
                    {
                        tooltip += $" ({data.additionalInfo})";
                    }
                    tooltip += "</size></color>\n";
                }

                // 3. 다단히트 (#FF6B6B / #FFB6C1)
                if (!string.IsNullOrEmpty(data.multiHitInfo))
                {
                    tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color><color=#FFB6C1><size=16>{data.multiHitInfo}</size></color>\n";
                }

                // 4. 범위 - 돌진 거리 (#87CEEB / #B0E0E6)
                if (!string.IsNullOrEmpty(data.dashDistance))
                {
                    string effectText = $"{L.Get("mace_effect_buff")} {data.dashDistance}";
                    tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{effectText}</size></color>\n";
                }

                // 5. 소모 (#FFB347 / #FFDAB9)
                if (!string.IsNullOrEmpty(data.staminaCost))
                {
                    tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stat_stamina")} {data.staminaCost}</size></color>\n";
                }

                // 6. 스킬유형 (G키 강조: #FF4500 / #00FF00)
                if (!string.IsNullOrEmpty(data.skillType))
                {
                    tooltip += $"<color=#FF4500><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#00FF00><size=16>{data.skillType}</size></color>\n";
                }

                // 7. 쿨타임 (#FFA500 / #FFDB58)
                if (!string.IsNullOrEmpty(data.cooldown))
                {
                    tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{data.cooldown}</size></color>\n";
                }

                // 8. 필요조건 (#98FB98 / #00FF00)
                if (!string.IsNullOrEmpty(data.requirement))
                {
                    tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{data.requirement}</size></color>\n";
                }

                // 9. 확인사항 (#F0E68C / #FFE4B5)
                if (!string.IsNullOrEmpty(data.confirmation))
                {
                    tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_notice")}: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>\n";
                }

                // 10. 필요포인트 - specialNote에 포함되어 있음
                if (!string.IsNullOrEmpty(data.specialNote))
                {
                    tooltip += $"<color=#DDA0DD><size=16>{L.Get("tooltip_special_note")}: </size></color><color=#E6E6FA><size=16>{data.specialNote}</size></color>";
                }

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심 툴팁] 구조화된 툴팁 생성 실패: {ex.Message}");
                return GetGuardianHeartFallbackTooltip();
            }
        }

        /// <summary>
        /// 수호자의 진심 백업 툴팁 (오류 시 사용)
        /// </summary>
        private static string GetGuardianHeartFallbackTooltip()
        {
            int requiredPoints = Defense_Config.GuardianHeartRequiredPointsValue;

            float dmg = Defense_Config.ShieldChargeDamagePercentValue;
            float mhDmg = Defense_Config.ShieldChargeMultiHitDamagePercentValue;
            return "<color=#FFD700><size=22>방패돌진</size></color>\n\n" +
                   $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>방패 돌진하여 방패 막기력의 {dmg:F0}% 충돌 데미지</size></color>\n" +
                   $"<color=#FF6B6B><size=16>데미지: </size></color><color=#FFB6C1><size=16>다단히트: VFX 발동마다 3m 반경 방패 막기력의 {mhDmg:F0}% 추가 타격 + 크리티컬 이펙트</size></color>\n" +
                   "<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>돌진 거리 12m</size></color>\n" +
                   "<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 20</size></color>\n" +
                   "<color=#FF4500><size=16>스킬유형: </size></color><color=#00FF00><size=16>액티브 스킬 - Mouse2</size></color>\n" +
                   "<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>35초</size></color>\n" +
                   "<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>방패 착용</size></color>\n" +
                   "<color=#98FB98><size=16>필요 직업: </size></color><color=#00FF00><size=16>탱커</size></color>\n" +
                   "<color=#DDA0DD><size=16>특별안내: </size></color><color=#E6E6FA><size=16>방패로 적을 가격하고 주변 5m 적을 도발한다.</size></color>\n\n" +
                   $"<color=#87CEEB><size=16>필요포인트: </size></color><color=#FF6B6B><size=16>{requiredPoints}</size></color>";
        }

        // [DEPRECATED] - Use MeleeTooltipUtils.GenerateTooltip() instead
        private static string GenerateMaceTooltip(MeleeTooltipUtils.MeleeTooltipData data)
        {
            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Mace);
        }

        #region 스킬 매핑

        /// <summary>
        /// 둔기 스킬 ID와 툴팁 함수 매핑
        /// </summary>
        private static readonly Dictionary<string, Func<string>> MaceSkillMappings = new()
        {
            { "mace_Step1_damage", GetMaceExpertTooltip },
            { "mace_Step2_stun_boost", GetMaceStep2StunBoostTooltip },
            { "mace_Step3_branch_guard", GetMaceStep3BranchGuardTooltip },
            { "mace_Step3_branch_heavy", GetMaceStep3BranchHeavyTooltip },
            { "mace_Step4_push", GetMaceStep4PushTooltip },
            { "mace_Step5_tank", GetMaceStep5TankTooltip },
            { "mace_Step5_dps", GetMaceStep5DpsTooltip },
            { "mace_Step6_grandmaster", GetMaceStep6GrandmasterTooltip },
            { "mace_Step7_fury_hammer", GetMaceStep7FuryHammerTooltip },
            { "mace_Step7_guardian_heart", GetMaceStep7GuardianHeartTooltip },
            { "mace_Step7_shockwave_slam", GetMaceStep7ShockwaveSlamTooltip }
        };

        #endregion

        /// <summary>
        /// 모든 둔기 스킬 툴팁 업데이트
        /// </summary>
        public static void UpdateMaceTooltips()
        {
            MeleeTooltipUtils.UpdateMultipleTooltips(MaceSkillMappings, MeleeTooltipUtils.WeaponType.Mace);
        }

        // MeleeTooltipUtils.UpdateSkillTooltip() 사용
        // 기존 UpdateIndividualMaceTooltip() 제거

        /// <summary>
        /// 특정 둔기 스킬 툴팁 가져오기
        /// </summary>
        public static string GetMaceSkillTooltip(string skillId)
        {
            return MeleeTooltipUtils.GetSkillTooltip(skillId, MaceSkillMappings, MeleeTooltipUtils.WeaponType.Mace);
        }
    }
}
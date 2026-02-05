using System;
using System.Collections.Generic;
using UnityEngine;

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
            Plugin.Log.LogDebug("[검 툴팁] GetSwordSlashTooltip() 호출됨 (돌진 연속 베기)");

            // 컨피그에서 실제 값 가져오기
            var skillData = Sword_Config.GetRushSlashData();
            var requiredPoints = 3; // MeleeSkillData.cs에서 확인한 값

            Plugin.Log.LogDebug($"[검 툴팁] 컨피그 값들 - 1차: {skillData.damage1stRatio}%, 2차: {skillData.damage2ndRatio}%, 3차: {skillData.damage3rdRatio}%, 스태미나: {skillData.staminaCost}, 쿨타임: {skillData.cooldown}초");

            // 돌진 연속 베기 설명 구성
            string description = $"전방 {skillData.initialDistance}m 돌진 후 몬스터 주변을 빠르게 이동하며 3회 연속 베기\n" +
                                $"<color=#98FB98>1차 베기: 공격력 {skillData.damage1stRatio}%</color>\n" +
                                $"<color=#FFA500>2차 베기: 공격력 {skillData.damage2ndRatio}%</color>\n" +
                                $"<color=#FF6B6B>3차 베기 (피니셔): 공격력 {skillData.damage3rdRatio}%</color>";

            // MeleeTooltipUtils를 사용한 툴팁 데이터 생성
            var data = MeleeTooltipUtils.CreateActiveSkillData(
                "<color=#FFD700><size=22>돌진 연속 베기</size></color>",
                description,
                $"{skillData.staminaCost}",
                $"{skillData.cooldown}초",
                MeleeTooltipUtils.WeaponType.Sword,
                "스킬 사용 중 무적 아님",
                "",
                "G키"
            );
            data.requiredPoints = requiredPoints.ToString();

            string finalTooltip = MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
            Plugin.Log.LogDebug($"[검 툴팁] 최종 툴팁 생성 완료 - 길이: {finalTooltip?.Length ?? 0}");
            return finalTooltip;
        }

        /// <summary>
        /// 검 전문가 툴팁 생성
        /// </summary>
        public static string GetSwordExpertTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetSwordExpertTooltip() 호출됨");

            var requiredPoints = 2; // 검 전문가 고정값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>검 전문가</size></color>",
                $"검 공격력 +{Sword_Config.SwordExpertDamageValue}%\n2연속 공격력 +{SkillTreeConfig.SwordStep1ExpertComboBonusValue}% ({Sword_Config.SwordStep1ExpertDurationValue}초)",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 빠른 베기 툴팁 생성 (sword_step1_fastslash)
        /// </summary>
        public static string GetFastSlashTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetFastSlashTooltip() 호출됨");

            var requiredPoints = 2; // MeleeSkillData.cs에서 확인한 값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>빠른 베기</size></color>",
                $"공격속도 +{SkillTreeConfig.SwordStep1FastSlashSpeedValue}%",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 반격 자세 툴팁 생성 (sword_step1_counter)
        /// </summary>
        public static string GetCounterTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetCounterTooltip() 호출됨");

            var requiredPoints = 3; // MeleeSkillData.cs에서 확인한 값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>반격 자세</size></color>",
                $"패링 성공 후 {SkillTreeConfig.SwordStep1CounterDurationValue}초동안 방어력 +{SkillTreeConfig.SwordStep1CounterDefenseBonusValue}%",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 연속베기 툴팁 생성 (sword_step2_combo)
        /// </summary>
        public static string GetComboTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetComboTooltip() 호출됨");

            var requiredPoints = 2; // MeleeSkillData.cs에서 확인한 값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>연속베기</size></color>",
                $"3연속 공격력 +{SkillTreeConfig.SwordStep2ComboSlashBonusValue}% ({Sword_Config.SwordStep2ComboSlashDurationValue}초)",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 칼날 되치기 툴팁 생성 (sword_step3_riposte)
        /// </summary>
        public static string GetRiposteTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetRiposteTooltip() 호출됨");

            var requiredPoints = 3; // MeleeSkillData.cs에서 확인한 값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "칼날 되치기",
                $"공격력 +{Sword_Config.SwordRiposteDamageBonusValue}",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.additionalInfo = "검 공격력 향상";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 공방일체 툴팁 생성 (sword_step3_allinone)
        /// </summary>
        public static string GetAllInOneTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetAllInOneTooltip() 호출됨");

            var requiredPoints = 2; // MeleeSkillData.cs에서 확인한 값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>공방일체</size></color>",
                $"양손 무기 착용 시 공격력 +{SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue}%, 방어력 +{SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue}%",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.additionalInfo = "※ 검 사용 시 효과 발동";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 진검승부 툴팁 생성 (sword_step4_duel)
        /// </summary>
        public static string GetDuelTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetDuelTooltip() 호출됨");

            var requiredPoints = 3; // MeleeSkillData.cs에서 확인한 값

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>진검승부</size></color>",
                $"공격 속도 +{SkillTreeConfig.SwordStep4TrueDuelSpeedValue}%",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.additionalInfo = "※ 검 사용 시 효과 발동";
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 패링 돌격 툴팁 생성 (sword_step5_defswitch)
        /// </summary>
        public static string GetDefSwitchTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetDefSwitchTooltip() 호출됨 (패링 돌격)");

            var requiredPoints = 3;

            float duration = Sword_Config.ParryRushDurationValue;
            float damageBonus = Sword_Config.ParryRushDamageBonusValue;
            float pushDist = Sword_Config.ParryRushPushDistanceValue;
            float staminaCost = Sword_Config.ParryRushStaminaCostValue;
            float cooldown = Sword_Config.ParryRushCooldownValue;

            string description = $"{duration}초 동안 패링 성공 시 몬스터에게 방패돌격\n" +
                                $"<color=#98FB98>공격력 +{damageBonus}%</color>\n" +
                                $"<color=#FFA500>{pushDist}m 밀어내기</color>";

            var data = MeleeTooltipUtils.CreateActiveSkillData(
                "<color=#FFD700><size=22>패링 돌격</size></color>",
                description,
                $"{staminaCost}",
                $"{cooldown}초",
                MeleeTooltipUtils.WeaponType.Sword,
                "근접 액티브 스킬은 1개만 습득 가능",
                "방패 착용",
                "G키"
            );
            data.requiredPoints = requiredPoints.ToString();

            return MeleeTooltipUtils.GenerateTooltip(data, MeleeTooltipUtils.WeaponType.Sword);
        }

        /// <summary>
        /// 궁극 베기 툴팁 생성 (sword_Step6_ultimate_slash)
        /// </summary>
        public static string GetUltimateSlashTooltip()
        {
            Plugin.Log.LogDebug("[검 툴팁] GetUltimateSlashTooltip() 호출됨");

            var requiredPoints = 3; // Tier 6 스킬

            var data = MeleeTooltipUtils.CreatePassiveSkillData(
                "<color=#FFD700><size=22>궁극 베기</size></color>",
                $"모든 검 스킬 효과 +{SkillTreeConfig.SwordStep6UltimateSlashMultiplierValue}%",
                MeleeTooltipUtils.WeaponType.Sword
            );
            data.requirement = "검 착용";
            data.additionalInfo = "※ 검 사용 시 효과 발동";
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
                        return "검 스킬 정보를 찾을 수 없음";
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 툴팁] 일반 툴팁 생성 오류: {ex.Message}");
                return "툴팁 생성 오류";
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
                    tooltip += $"<color=#F0E68C><size=16>⚠️확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
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
                    tooltip += $"<color=#F0E68C><size=16>⚠️확인사항: </size></color><color=#FFE4B5><size=16>{data.confirmation}</size></color>";
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
            var data = MeleeTooltipUtils.CreateActiveSkillData(
                "<color=#FFD700><size=22>돌진 연속 베기</size></color>",
                "전방 5m 돌진 후 몬스터 주변을 이동하며 3회 연속 베기\n1차: 70%, 2차: 80%, 3차: 90%",
                "30",
                "25초",
                MeleeTooltipUtils.WeaponType.Sword,
                "스킬 사용 중 무적 아님",
                "",
                "G키"
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
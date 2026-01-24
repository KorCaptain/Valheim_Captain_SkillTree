using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 단검 전문가 스킬 로직 모음
    /// SkillEffect.MeleeSkills.cs에서 단검 관련 로직을 분리
    /// </summary>
    public static class Knife_Skill
    {
        #region 단검 무기 감지

        /// <summary>
        /// 플레이어가 단검 또는 맨주먹을 사용 중인지 확인
        /// </summary>
        public static bool IsUsingDagger(Player player)
        {
            var weapon = player?.GetCurrentWeapon();
            if (weapon?.m_shared?.m_skillType == Skills.SkillType.Knives)
            {
                return true; // 단검
            }
            
            // 맨주먹 확인 (무기가 없는 경우)
            if (weapon?.m_shared?.m_skillType == Skills.SkillType.Unarmed)
            {
                return true; // 장갑(맨주먹)
            }
            
            // 프리팹 이름에 단검 관련 키워드가 포함되어 있는지 확인
            if (weapon != null)
            {
                string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
                string prefabName = weapon.m_dropPrefab?.name?.ToLower() ?? "";
                bool isDaggerByName = ContainsDaggerKeyword(weaponName) || ContainsDaggerKeyword(prefabName);
                
                if (isDaggerByName)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        /// <summary>
        /// 프리팹 이름에 단검 관련 키워드가 포함되어 있는지 확인
        /// </summary>
        private static bool ContainsDaggerKeyword(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            
            string[] daggerKeywords = { "knives", "knife", "dagger" };
            string lowerName = name.ToLower();
            
            foreach (string keyword in daggerKeywords)
            {
                if (lowerName.Contains(keyword))
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// 플레이어가 전투 상태인지 확인 (대체 구현)
        /// </summary>
        private static bool IsPlayerInCombat(Player player)
        {
            if (player == null) return false;
            
            try
            {
                // StatusEffect 'InCombat'을 통한 확인 (가장 안정적)
                var seman = player.GetSEMan();
                if (seman != null)
                {
                    var combatStatus = seman.GetStatusEffect("InCombat".GetStableHashCode());
                    if (combatStatus != null) return true;
                }
                
                // 대체 방법: 최근 피해를 받았거나 입혔는지 확인
                // 단순화된 전투 상태 판별: 단검/맨주먹을 들고 있고 최근에 활동했다면 전투 상태로 간주
                var weapon = player.GetCurrentWeapon();
                if (weapon?.m_shared?.m_skillType == Skills.SkillType.Knives || 
                    weapon?.m_shared?.m_skillType == Skills.SkillType.Unarmed)
                {
                    // 단검/맨주먹을 들고 있으면 일단 전투 상태로 간주 (단순화)
                    return true;
                }
                
                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[전투 상태 확인] 오류 발생: {ex.Message}");
                return false; // 오류 시 false 반환
            }
        }

        #endregion

        #region 패시브 스킬 효과

        /// <summary>
        /// 단검 전문가 - 백스탭 데미지 보너스
        /// </summary>
        public static void ApplyKnifeExpertBackstab(Player player, ref HitData hit, Character targetCharacter)
        {
            if (!SkillEffect.HasSkill("knife_expert_backstab") || !IsUsingDagger(player)) return;

            try
            {
                bool isBackstab = SkillEffect.IsBackstab(player, targetCharacter);
                if (isBackstab)
                {
                    float bonus = Knife_Config.KnifeExpertBackstabBonusValue / 100f;
                    hit.m_damage.m_slash *= (1f + bonus);

                    SkillEffect.ShowSkillEffectText(player,
                        $"🗡️ 단검 전문가 - 백스탭! (+{Knife_Config.KnifeExpertBackstabBonusValue}%)",
                        Color.red, SkillEffect.SkillEffectTextType.Combat);

                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 전문가] 백스탭 보너스 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 회피 숙련 - 회피 확률 패시브 보너스
        /// 실제 효과는 Plugin.cs의 Character_Damage_DefenseSkills_Patch에서 적용됨
        /// </summary>
        public static float GetKnifeEvasionBonus(Player player)
        {
            if (!SkillEffect.HasSkill("knife_step2_evasion") || !IsUsingDagger(player)) return 0f;

            try
            {
                float evasionBonus = Knife_Config.KnifeEvasionBonusValue;
                Plugin.Log.LogDebug($"[회피 숙련] 회피 확률 +{evasionBonus}%");
                return evasionBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[회피 숙련] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 빠른 움직임 - 이동속도 패시브 보너스
        /// 실제 효과는 Player.UpdateModifiers 패치에서 적용됨
        /// </summary>
        public static float GetKnifeMoveSpeedBonus(Player player)
        {
            if (!SkillEffect.HasSkill("knife_step3_move_speed") || !IsUsingDagger(player)) return 0f;

            try
            {
                float speedBonus = Knife_Config.KnifeMoveSpeedBonusValue;
                Plugin.Log.LogDebug($"[빠른 움직임] 이동속도 +{speedBonus}%");
                return speedBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[빠른 움직임] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 빠른 공격 - 공격력 패시브 보너스
        /// 실제 효과는 ItemData.GetDamage 패치에서 적용됨
        /// </summary>
        public static float GetKnifeAttackDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("knife_step4_attack_damage") || !IsUsingDagger(player)) return 0f;

            try
            {
                float damageBonus = Knife_Config.KnifeAttackDamageBonusValue;
                Plugin.Log.LogDebug($"[빠른 공격] 공격력 +{damageBonus}");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[빠른 공격] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 치명타 숙련 - 치명타 확률 패시브 보너스
        /// ⚠️ 이 함수는 더 이상 사용되지 않습니다.
        /// 치명타 시스템이 CriticalSystem으로 중앙화되어 자동 처리됩니다.
        /// - 치명타 확률: Critical.GetKnifeCritChance()에서 처리 (Line 103-113)
        /// </summary>
        [Obsolete("치명타 시스템이 CriticalSystem으로 통합되었습니다. Critical.GetKnifeCritChance()에서 자동 처리됩니다.")]
        public static void ApplyKnifeCritRateBoost(Player player)
        {
            // 더 이상 사용되지 않음 - Critical 시스템에서 자동 처리
            // 하위 호환을 위해 함수는 유지하지만 빈 구현
            return;
        }

        /// <summary>
        /// 치명적 피해 - 공격력 패시브 보너스
        /// 실제 효과는 ItemData.GetDamage 패치에서 적용됨
        /// </summary>
        public static float GetKnifeCombatDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("knife_step6_combat_damage") || !IsUsingDagger(player)) return 0f;

            try
            {
                float damageBonus = Knife_Config.KnifeCombatDamageBonusValue;
                Plugin.Log.LogDebug($"[치명적 피해] 공격력 +{damageBonus}%");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[치명적 피해] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 암살자 - 패시브 치명타 확률 및 치명타 피해 증가
        ///
        /// ⚠️ 이 함수는 더 이상 사용되지 않습니다 (하위 호환용으로만 유지)
        /// 치명타 시스템이 CriticalSystem으로 중앙화되어 자동 처리됩니다.
        ///
        /// - 치명타 확률: Critical.GetKnifeCritChance()에서 처리
        /// - 치명타 피해: CriticalDamage.GetKnifeCritDamage()에서 처리
        /// </summary>
        [Obsolete("치명타 시스템이 CriticalSystem으로 통합되었습니다. 이 함수는 자동으로 처리되므로 호출 불필요합니다.")]
        public static void ApplyKnifeExecutionPassive(Player player, ref HitData hit, bool isCritical)
        {
            // 더 이상 사용되지 않음 - Critical 시스템에서 자동 처리
            // 하위 호환을 위해 함수는 유지하지만 빈 구현
            return;
        }

        /// <summary>
        /// 암살술 - 백스탭 공격력 증가
        /// </summary>
        public static void ApplyKnifeAssassinationBonus(Player player, ref HitData hit, Character targetCharacter)
        {
            if (!SkillEffect.HasSkill("knife_step8_assassination") || !IsUsingDagger(player)) return;

            try
            {
                // 백스탭 조건 확인 (적의 뒤에서 공격)
                bool isBackstab = SkillEffect.IsBackstab(player, targetCharacter);

                if (isBackstab)
                {
                    float backstabBonus = Knife_Config.KnifeAssassinationCritMultiplierValue / 100f;
                    hit.m_damage.m_slash *= (1f + backstabBonus);
                    hit.m_damage.m_pierce *= (1f + backstabBonus);

                    SkillEffect.ShowSkillEffectText(player,
                        $"🗡️ 암살술 - 백스탭! (+{Knife_Config.KnifeAssassinationCritMultiplierValue}%)",
                        Color.magenta, SkillEffect.SkillEffectTextType.Combat);

                    Plugin.Log.LogDebug($"[암살술] 백스탭 공격 - 데미지 +{Knife_Config.KnifeAssassinationCritMultiplierValue}%");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[암살술] 백스탭 보너스 적용 실패: {ex.Message}");
            }
        }

        #endregion

        #region 액티브 스킬 (G키)

        /// <summary>
        /// 암살자의 심장 - G키 액티브 스킬 사용 가능 여부 확인
        /// </summary>
        public static bool CanUseAssassinHeart(Player player)
        {
            if (!SkillEffect.HasSkill("knife_step9_assassin_heart") || !IsUsingDagger(player)) return false;

            try
            {
                // 쿨타임 확인 (JobSkillsUtility 사용)
                string skillName = "암살자의 심장";
                if (JobSkillsUtility.IsOnCooldown(player, skillName))
                {
                    float remaining = JobSkillsUtility.GetRemainingCooldown(player, skillName);
                    SkillEffect.ShowSkillEffectText(player, 
                        $"쿨타임 {remaining:F1}초 남음", 
                        Color.red, SkillEffect.SkillEffectTextType.Passive);
                    return false;
                }

                // 스태미나 확인
                float staminaCost = Knife_Config.KnifeAssassinHeartStaminaCostValue;
                if (player.GetStamina() < staminaCost)
                {
                    SkillEffect.ShowSkillEffectText(player, 
                        "스태미나 부족!", 
                        Color.red, SkillEffect.SkillEffectTextType.Passive);
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[암살자의 심장] 사용 가능 여부 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 암살자의 심장 - G키 액티브 스킬 사용
        /// </summary>
        public static bool UseAssassinHeart(Player player)
        {
            if (!CanUseAssassinHeart(player)) return false;

            try
            {
                // 스태미나 소모
                float staminaCost = Knife_Config.KnifeAssassinHeartStaminaCostValue;
                player.UseStamina(staminaCost);

                // 쿨타임 설정 (JobSkillsUtility 사용)
                string skillName = "암살자의 심장";
                float cooldown = Knife_Config.KnifeAssassinHeartCooldownValue;
                JobSkillsUtility.SetCooldown(player, skillName, cooldown);

                // 버프 효과 적용
                ApplyAssassinHeartBuff(player);

                // VFX/SFX 재생
                SkillEffect.PlaySkillEffect(player, "knife_step9_assassin_heart", player.transform.position);

                SkillEffect.ShowSkillEffectText(player, 
                    "💀 암살자의 심장 발동!", 
                    new Color(1f, 0.2f, 0.2f), SkillEffect.SkillEffectTextType.Combat);

                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[암살자의 심장] 스킬 사용 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 암살자의 심장 버프 효과 적용
        /// </summary>
        private static void ApplyAssassinHeartBuff(Player player)
        {
            try
            {
                float duration = Knife_Config.KnifeAssassinHeartDurationValue;
                
                // 기존 버프 제거
                var hash = "KnifeAssassinHeartBuff".GetStableHashCode();
                player.GetSEMan()?.RemoveStatusEffect(hash);
                
                // 새로운 버프 적용
                var assassinHeartSE = ScriptableObject.CreateInstance<SE_Stats>();
                assassinHeartSE.m_name = "암살자의 심장";
                assassinHeartSE.m_tooltip = $"데미지 +{Knife_Config.KnifeAssassinHeartDamageBonusValue}%, " +
                                           $"치명타 확률 +{Knife_Config.KnifeAssassinHeartCritChanceValue}%, " +
                                           $"치명타 데미지 {Knife_Config.KnifeAssassinHeartCritDamageValue}배";
                assassinHeartSE.m_ttl = duration;
                
                // 데미지 보너스 적용 (간단한 구현)
                float damageBonus = Knife_Config.KnifeAssassinHeartDamageBonusValue / 100f;
                assassinHeartSE.m_damageModifier = 1f + damageBonus;
                
                player.GetSEMan()?.AddStatusEffect(assassinHeartSE);
                
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[암살자의 심장] 버프 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 암살자의 심장 활성 상태에서 추가 효과 적용
        /// </summary>
        public static void ApplyAssassinHeartEffects(Player player, ref HitData hit, bool isCritical)
        {
            if (!SkillEffect.HasSkill("knife_step9_assassin_heart") || !IsUsingDagger(player)) return;

            try
            {
                // 암살자의 심장 버프 활성 상태 확인
                var hash = "KnifeAssassinHeartBuff".GetStableHashCode();
                if (player.GetSEMan()?.HaveStatusEffect(hash) == true)
                {
                    if (isCritical)
                    {
                        // 치명타 데미지 배수 적용 (slash만)
                        float critDamageMultiplier = Knife_Config.KnifeAssassinHeartCritDamageValue;
                        hit.m_damage.m_slash *= critDamageMultiplier;

                        SkillEffect.ShowSkillEffectText(player,
                            $"💀 암살자의 심장! (치명타 {critDamageMultiplier}배)",
                            new Color(1f, 0.2f, 0.2f), SkillEffect.SkillEffectTextType.Combat);

                        // 추가 VFX/SFX
                        SkillEffect.PlaySkillEffect(player, "knife_step9_assassin_heart", hit.m_point);

                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[암살자의 심장] 활성 효과 적용 실패: {ex.Message}");
            }
        }

        #endregion

        #region 유틸리티

        /// <summary>
        /// 단검 스킬 전체 초기화
        /// </summary>
        public static void InitializeKnifeSkills()
        {
            try
            {
                // 단검 스킬 시스템 초기화
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 스킬] 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 단검 스킬 상태 정리
        /// </summary>
        public static void CleanupKnifeSkills(Player player)
        {
            try
            {
                if (player == null) return;
                
                // 단검 관련 버프 모두 제거
                var seMan = player.GetSEMan();
                if (seMan != null)
                {
                    seMan.RemoveStatusEffect("KnifeMoveSpeedBoost".GetStableHashCode());
                    seMan.RemoveStatusEffect("KnifeAttackSpeedBoost".GetStableHashCode());
                    seMan.RemoveStatusEffect("KnifeCritRateBoost".GetStableHashCode());
                    seMan.RemoveStatusEffect("KnifeAssassinHeartBuff".GetStableHashCode());
                }
                
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 스킬] 상태 정리 실패: {ex.Message}");
            }
        }

        #endregion

        #region 비틀거림 보너스

        /// <summary>
        /// 암살자 스킬 비틀거림 공격력 보너스 가져오기
        /// </summary>
        public static float GetKnifeStaggerBonus(Player player)
        {
            if (!SkillEffect.HasSkill("knife_step7_execution") || !IsUsingDagger(player)) return 0f;

            return Knife_Config.KnifeExecutionStaggerBonusValue / 100f;
        }

        /// <summary>
        /// HitData에 비틀거림 보너스 적용
        /// </summary>
        public static void ApplyKnifeStaggerBonus(Player player, ref HitData hit)
        {
            if (player == null || hit == null) return;

            float staggerBonus = GetKnifeStaggerBonus(player);
            if (staggerBonus > 0f)
            {
                hit.m_staggerMultiplier *= (1f + staggerBonus);
            }
        }

        #endregion
    }

    /// <summary>
    /// 단검 암살자 스킬 - 비틀거림 공격력 보너스 패치
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Knife_Character_Damage_StaggerPatch
    {
        /// <summary>
        /// Character.Damage 실행 전 - 단검 비틀거림 보너스 적용
        /// </summary>
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (hit == null) return;

                // 공격자가 플레이어인지 확인
                var attacker = hit.GetAttacker();
                if (attacker == null || !(attacker is Player player)) return;

                // 단검 사용 중인지 확인
                if (!Knife_Skill.IsUsingDagger(player)) return;

                // 비틀거림 보너스 적용
                Knife_Skill.ApplyKnifeStaggerBonus(player, ref hit);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[암살자] 비틀거림 보너스 적용 실패: {ex.Message}");
            }
        }
    }
}
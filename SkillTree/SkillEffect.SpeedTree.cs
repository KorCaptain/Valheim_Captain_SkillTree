using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 속도 전문가 트리 전용 효과 시스템
    /// speed_root부터 8단계까지 모든 속도 관련 스킬 구현
    /// </summary>
    public static partial class SkillEffect
    {
        // === 속도 트리 상태 추적 변수들 ===
        
        // 조건부 이동속도 보너스 추적
        public static Dictionary<Player, float> meleeComboSpeedEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> crossbowExpertSpeedEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> bowExpertStaminaEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> staffCasting = new Dictionary<Player, bool>();
        
        // 연속 공격 카운트 (속도용)
        public static Dictionary<Player, int> speedMeleeComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> speedMeleeLastHitTime = new Dictionary<Player, float>();
        public static Dictionary<Player, int> bowComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> bowLastHitTime = new Dictionary<Player, float>();
        
        // 석궁 장전 상태 추적
        public static Dictionary<Player, bool> crossbowReloading = new Dictionary<Player, bool>();

        // === 속도 스킬 이펙트 데이터 ===
        private static readonly Dictionary<string, SkillEffectData> speedSkillEffects = new Dictionary<string, SkillEffectData>
        {
            // 기본 스킬들
            ["speed_base"] = new SkillEffectData("fx_speed", "sfx_creature_tamed", Color.cyan),
            ["melee_combo"] = new SkillEffectData("fx_combo_speed", "sfx_speed_boost", Color.yellow),
            ["crossbow_reload2"] = new SkillEffectData("fx_reload_speed", "sfx_reload_fast", Color.green),
            ["bow_speed2"] = new SkillEffectData("fx_arrow_speed", "sfx_arrow_hit", Color.blue),
            ["moving_cast"] = new SkillEffectData("fx_magic_speed", "sfx_magic_cast", Color.magenta),

            // 고급 스킬들
            ["speed_master"] = new SkillEffectData("fx_energy_boost", "sfx_energy", new Color(1f, 0.8f, 0f)),
            ["agility_peak"] = new SkillEffectData("fx_time_slow", "sfx_time", Color.white),
            ["all_master"] = new SkillEffectData("fx_mastery", "sfx_mastery", new Color(1f, 0.5f, 0f)),

            // 공격속도 스킬들
            ["melee_speed1"] = new SkillEffectData("fx_attack_speed", "sfx_swing_fast", Color.red),
            ["bow_draw1"] = new SkillEffectData("fx_draw_speed", "sfx_bow_draw", Color.green),
            ["crossbow_draw1"] = new SkillEffectData("fx_crossbow_speed", "sfx_crossbow_load", Color.cyan),
            ["staff_speed1"] = new SkillEffectData("fx_cast_speed", "sfx_cast_fast", Color.magenta),
        };

        // === 속도 스킬 이름 매핑 ===
        private static readonly Dictionary<string, string> speedSkillNames = new Dictionary<string, string>
        {
            ["speed_base"] = "민첩함의 기초",
            ["melee_combo"] = "연속의 흐름",
            ["crossbow_reload2"] = "장전 이동",
            ["bow_speed2"] = "연계 속도",
            ["moving_cast"] = "이동 시전",
            ["speed_ex1"] = "수련자1",
            ["speed_ex2"] = "수련자2",
            ["speed_master"] = "에너자이져",
            ["ship_master"] = "선장",
            ["agility_peak"] = "점프 숙련자",
            ["speed_1"] = "민첩",
            ["speed_2"] = "지구력",
            ["speed_3"] = "지능",
            ["all_master"] = "숙련자",
            ["melee_speed1"] = "근접 가속",
            ["crossbow_draw1"] = "석궁 가속",
            ["bow_draw1"] = "활 가속",
            ["staff_speed1"] = "시전 가속",
        };

        // === 헬퍼 함수들 ===
        
        /// <summary>
        /// 근접 무기 2연타 스태미나 감소 효과 체크
        /// </summary>
        public static void CheckMeleeComboSpeed(Player player)
        {
            if (!HasSkill("melee_combo")) return;
            
            float currentTime = Time.time;
            
            if (!speedMeleeComboCount.ContainsKey(player))
                speedMeleeComboCount[player] = 0;
            if (!speedMeleeLastHitTime.ContainsKey(player))
                speedMeleeLastHitTime[player] = 0;

            // 3초 내 연속 공격인지 확인
            if (currentTime - speedMeleeLastHitTime[player] < 3f)
            {
                speedMeleeComboCount[player]++;
            }
            else
            {
                speedMeleeComboCount[player] = 1;
            }

            speedMeleeLastHitTime[player] = currentTime;

            // 2연속 공격 시 스태미나 감소 효과 적용
            if (speedMeleeComboCount[player] >= 2)
            {
                meleeComboSpeedEndTime[player] = currentTime + SkillTreeConfig.SpeedMeleeComboDurationValue;
                PlaySkillEffect(player, "melee_combo");
                ShowSkillEffectText(player, 
                    $"⚔️ 연속의 흐름!", 
                    new Color(0.3f, 0.7f, 1f), SkillEffectTextType.Combat);
                speedMeleeComboCount[player] = 0; // 리셋
            }
        }

        /// <summary>
        /// 활 숙련자: 활 2연속 공격 시 스태미나 감소 효과 체크
        /// </summary>
        public static void CheckBowExpertCombo(Player player)
        {
            if (!HasSkill("bow_speed2")) return;
            
            float currentTime = Time.time;
            
            if (!bowComboCount.ContainsKey(player))
                bowComboCount[player] = 0;
            if (!bowLastHitTime.ContainsKey(player))
                bowLastHitTime[player] = 0;

            // 3초 내 연속 공격인지 확인
            if (currentTime - bowLastHitTime[player] < 3f)
            {
                bowComboCount[player]++;
            }
            else
            {
                bowComboCount[player] = 1;
            }

            bowLastHitTime[player] = currentTime;

            // 2연타 달성 시 스태미나 감소 효과 적용
            if (bowComboCount[player] >= 2)
            {
                bowExpertStaminaEndTime[player] = currentTime + SkillTreeConfig.SpeedBowExpertDurationValue;
                PlaySkillEffect(player, "bow_speed2");
                ShowSkillEffectText(player, 
                    "🏹 활 숙련자!", 
                    new Color(0.4f, 0.8f, 1f), SkillEffectTextType.Combat);
                bowComboCount[player] = 0; // 리셋
            }
        }
        
        /// <summary>
        /// 석궁 숙련자: 석궁 공격 시 이동속도 보너스 적용
        /// </summary>
        public static void ApplyCrossbowExpertSpeed(Player player)
        {
            if (!HasSkill("crossbow_reload2")) return;
            
            float currentTime = Time.time;
            crossbowExpertSpeedEndTime[player] = currentTime + SkillTreeConfig.SpeedCrossbowExpertDurationValue;
            
            PlaySkillEffect(player, "crossbow_reload2");
            ShowSkillEffectText(player, 
                "⚡ 석궁 숙련자!", 
                new Color(0.2f, 0.9f, 1f), SkillEffectTextType.Combat);
        }

        /// <summary>
        /// 현재 플레이어의 조건부 이동속도 보너스 계산
        /// 기본 이동속도는 Speed.cs에서 처리하므로 여기서는 조건부만 처리
        /// </summary>
        public static float GetConditionalMoveSpeedBonus(Player player)
        {
            // Speed.cs의 GetConditionalSpeedBonus 사용
            return Speed.GetConditionalSpeedBonus(player);
        }

        /// <summary>
        /// 속도 스킬 이펙트 데이터 가져오기
        /// </summary>
        public static SkillEffectData? GetSpeedSkillEffect(string skillId)
        {
            return speedSkillEffects.TryGetValue(skillId, out var effect) ? effect : (SkillEffectData?)null;
        }

        /// <summary>
        /// 속도 스킬 이름 가져오기
        /// </summary>
        public static string GetSpeedSkillName(string skillId)
        {
            return speedSkillNames.TryGetValue(skillId, out var name) ? name : skillId;
        }

        /// <summary>
        /// 모든 속도 스킬 이펙트를 메인 딕셔너리에 추가
        /// </summary>
        public static void RegisterSpeedSkillEffects(Dictionary<string, SkillEffectData> mainEffects)
        {
            foreach (var kvp in speedSkillEffects)
            {
                mainEffects[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// 모든 속도 스킬 이름을 메인 딕셔너리에 추가
        /// </summary>
        public static void RegisterSpeedSkillNames(Dictionary<string, string> mainNames)
        {
            foreach (var kvp in speedSkillNames)
            {
                mainNames[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// 점프 숙련자 점프 스태미나 감소 계산 (기존 패치에서 호출)
        /// </summary>
        public static float GetJumpStaminaReduction()
        {
            try
            {
                // 점프 숙련자 스킬 보유 시 점프 스태미나 -10% 감소
                if (HasSkill("agility_peak"))
                {
                    return SkillTreeConfig.JumpStaminaReductionValue;
                }
                return 0f;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 점프 스태미나 감소 계산 오류: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 전체 공격 속도 보너스 계산 (MMO getParameter 패치용)
        /// </summary>
        public static float GetTotalAttackSpeedBonus(Player player)
        {
            if (player == null) return 0f;

            try
            {
                float bonus = 0f;
                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return 0f;

                var skillType = weapon.m_shared.m_skillType;

                // 속도 트리 1단계: 민첩함의 기초 (speed_base) - 모든 무기 공격속도
                int speedBaseLevel = SkillTreeManager.Instance?.GetSkillLevel("speed_base") ?? -1;
                bool hasSpeedBase = speedBaseLevel > 0;
                if (hasSpeedBase)
                {
                    float speedBaseValue = SkillTreeConfig.SpeedBaseAttackSpeedValue;
                    bonus += speedBaseValue;
                }

                // 검 빠른 베기 (sword_step1_fastslash) - 검 착용 시만
                if (HasSkill("sword_step1_fastslash"))
                {
                    if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
                    {
                        bonus += SkillTreeConfig.SwordStep1FastSlashSpeedValue;
                    }
                }

                // 검 진검승부 (sword_step4_duel) - 검 착용 시만
                if (HasSkill("sword_step4_duel"))
                {
                    if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
                    {
                        bonus += SkillTreeConfig.SwordStep4TrueDuelSpeedValue;
                    }
                }

                // 창 전문가 (spear_expert) - 2연속 공격 속도 - 창 착용 시만
                if (HasSkill("spear_expert"))
                {
                    if (weapon.m_shared.m_skillType == Skills.SkillType.Spears ||
                        weapon.m_shared.m_skillType == Skills.SkillType.Polearms)
                    {
                        bonus += SkillTreeConfig.SpearStep1AttackSpeedValue;
                    }
                }

                // 민첩 스탯 (speed_1) - 공격속도 보너스 - 모든 무기
                if (HasSkill("speed_1"))
                {
                    bonus += SkillTreeConfig.SpeedDexterityAttackSpeedBonusValue;
                }

                // 연속의 흐름 (melee_combo) - 2연속 적중 시 공격속도 보너스
                float meleeComboBonus = GetMeleeComboAttackSpeedBonus(player);
                if (meleeComboBonus > 0f)
                {
                    bonus += meleeComboBonus;
                    Plugin.Log.LogDebug($"[공속] 연속의 흐름 보너스: +{meleeComboBonus}%");
                }

                // 근접 가속 (melee_speed1)
                if (HasSkill("melee_speed1"))
                {
                    bool isMelee = weapon.m_shared.m_skillType == Skills.SkillType.Swords ||
                                   weapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                                   weapon.m_shared.m_skillType == Skills.SkillType.Knives ||
                                   weapon.m_shared.m_skillType == Skills.SkillType.Spears ||
                                   weapon.m_shared.m_skillType == Skills.SkillType.Polearms;

                    if (isMelee)
                    {
                        bonus += SkillTreeConfig.SpeedMeleeAttackSpeedValue;

                        // 3연속 공격 보너스
                        if (IsTier8MeleeComboActive(player))
                        {
                            bonus += SkillTreeConfig.SpeedMeleeComboTripleBonusValue;
                            Plugin.Log.LogDebug($"[공속] 근접 3연속 보너스: +{SkillTreeConfig.SpeedMeleeComboTripleBonusValue}%");
                        }
                    }
                }

                // 활 가속 (bow_draw1)
                if (weapon.m_shared.m_skillType == Skills.SkillType.Bows)
                {
                    if (HasSkill("bow_draw1"))
                    {
                        bonus += SkillTreeConfig.SpeedBowDrawSpeedValue;
                    }

                    // 활 숙련자 (bow_speed2) - 2연속 적중 시 다음 장전 속도 보너스
                    float bowExpertBonus = GetBowExpertDrawSpeedBonus(player);
                    if (bowExpertBonus > 0f)
                    {
                        bonus += bowExpertBonus;
                        Plugin.Log.LogDebug($"[공속] 활 숙련자 장전속도: +{bowExpertBonus}%");
                    }
                }

                // 석궁 가속 (crossbow_draw1)
                if (weapon.m_shared.m_skillType == Skills.SkillType.Crossbows)
                {
                    if (HasSkill("crossbow_draw1"))
                    {
                        bonus += SkillTreeConfig.SpeedCrossbowDrawSpeedValue;
                    }

                    // 석궁 숙련자 (crossbow_reload2) - 버프 중 재장전 속도 보너스
                    float crossbowExpertBonus = GetCrossbowExpertReloadBonus(player);
                    if (crossbowExpertBonus > 0f)
                    {
                        bonus += crossbowExpertBonus;
                        Plugin.Log.LogDebug($"[공속] 석궁 숙련자 재장전: +{crossbowExpertBonus}%");
                    }
                }

                // 지팡이 가속 (staff_speed1)
                if (HasSkill("staff_speed1"))
                {
                    if (weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic ||
                        weapon.m_shared.m_skillType == Skills.SkillType.BloodMagic)
                    {
                        bonus += SkillTreeConfig.SpeedStaffCastSpeedFinalValue;
                    }
                }

                // 둔기 공격속도 보너스 (Tier 5 DPS: +10%)
                bool isUsingMace = WeaponHelper.IsUsingMace(player);
                if (isUsingMace)
                {
                    float maceBonus = MaceSkills.GetAttackSpeedBonus();
                    if (maceBonus > 0f)
                    {
                        bonus += maceBonus;
                    }
                }

                return bonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 공격 속도 계산 오류: {ex.Message}");
                return 0f;
            }
        }
    }

    // === 속도 트리 Harmony 패치들 ===


    /// <summary>
    /// 1단계: 구르기 속도 증가 - Character.Damage 패치에서 시각적 효과만 제공
    /// 실제 구르기 속도는 UseStamina 패치에서 스태미나 감소로 구현
    /// </summary>

    /// <summary>
    /// 2단계: 근접 공격 시 연속의 흐름 체크
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class SpeedTree_Character_Damage_MeleeCombo_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null) return;

                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return;

                // 근접 무기 사용 시 연속의 흐름 체크 (근접 속도 전문가 스킬 보유 시에만)
                bool isMeleeWeapon = weapon.m_shared.m_skillType == Skills.SkillType.Swords ||
                                    weapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                                    weapon.m_shared.m_skillType == Skills.SkillType.Knives ||
                                    weapon.m_shared.m_skillType == Skills.SkillType.Spears ||
                                    weapon.m_shared.m_skillType == Skills.SkillType.Polearms;

                if (isMeleeWeapon && SkillEffect.HasSkill("melee_combo"))
                {
                    SkillEffect.CheckMeleeComboSpeed(player);
                }

                // 활 적중 시 활 숙련자 체크 (활 속도 전문가 스킬 보유 시에만)
                if (weapon.m_shared.m_skillType == Skills.SkillType.Bows && SkillEffect.HasSkill("bow_speed2"))
                {
                    SkillEffect.CheckBowExpertCombo(player);
                }

                // 석궁 적중 시 석궁 숙련자 체크 (석궁 속도 전문가 스킬 보유 시에만)
                if (weapon.m_shared.m_skillType == Skills.SkillType.Crossbows && SkillEffect.HasSkill("crossbow_reload2"))
                {
                    SkillEffect.ApplyCrossbowExpertSpeed(player);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 근접 콤보 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 4단계: 음식 소모 속도 감소 (-15%) - EatFood 패치로 대체
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.EatFood))]
    public static class SpeedTree_Player_EatFood_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player __instance, ItemDrop.ItemData item, bool __result)
        {
            try
            {
                if (!__result || __instance != Player.m_localPlayer) return;

                // speed_master: 음식 효과 지속시간 +15% 증가 (소모 속도 감소 효과)
                if (SkillEffect.HasSkill("speed_master"))
                {
                    if (UnityEngine.Random.Range(0f, 1f) < 0.3f)
                    {
                        SkillEffect.ShowSkillEffectText(__instance, "⚡ 에너자이져!", 
                            new Color(0.5f, 0.8f, 1f), SkillEffect.SkillEffectTextType.Combat);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 음식 소모 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 플레이어 사망 시 Speed Tree 모든 상태 정리
    /// </summary>
    public static partial class SkillEffect
    {
        public static void CleanupSpeedTreeOnDeath(Player player)
        {
            try
            {
                // 조건부 이동속도 추적 정리
                if (meleeComboSpeedEndTime.ContainsKey(player))
                {
                    meleeComboSpeedEndTime.Remove(player);
                }
                if (crossbowExpertSpeedEndTime.ContainsKey(player))
                {
                    crossbowExpertSpeedEndTime.Remove(player);
                }
                if (bowExpertStaminaEndTime.ContainsKey(player))
                {
                    bowExpertStaminaEndTime.Remove(player);
                }
                if (staffCasting.ContainsKey(player))
                {
                    staffCasting.Remove(player);
                }

                // 연속 공격 카운트 정리
                if (speedMeleeComboCount.ContainsKey(player))
                {
                    speedMeleeComboCount.Remove(player);
                }
                if (speedMeleeLastHitTime.ContainsKey(player))
                {
                    speedMeleeLastHitTime.Remove(player);
                }
                if (bowComboCount.ContainsKey(player))
                {
                    bowComboCount.Remove(player);
                }
                if (bowLastHitTime.ContainsKey(player))
                {
                    bowLastHitTime.Remove(player);
                }

                // 석궁 장전 상태 정리
                if (crossbowReloading.ContainsKey(player))
                {
                    crossbowReloading.Remove(player);
                }

                // Speed.cs의 캐시 정리 호출
                Speed.ClearPlayerCache(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Speed Tree] 정리 실패: {ex.Message}");
            }
        }
    }

}
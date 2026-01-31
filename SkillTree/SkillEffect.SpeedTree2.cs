using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 속도 전문가 트리 추가 효과 시스템
    /// 미구현 효과들을 구현
    /// </summary>
    public static partial class SkillEffect
    {
        // === 추가 상태 추적 변수들 ===

        // 구르기 후 이동속도 버프
        public static Dictionary<Player, float> dodgeSpeedEndTime = new Dictionary<Player, float>();

        // 3연속 공격 추적 (티어8용)
        public static Dictionary<Player, int> tier8MeleeComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> tier8MeleeLastHitTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> tier8MeleeComboEndTime = new Dictionary<Player, float>();

        // 마법 3연속 추적 (시전 가속용)
        public static Dictionary<Player, int> staffComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> staffLastCastTime = new Dictionary<Player, float>();

        // 장전 중 이동속도 추적
        public static Dictionary<Player, bool> isDrawingBow = new Dictionary<Player, bool>();
        public static Dictionary<Player, bool> isReloadingCrossbow = new Dictionary<Player, bool>();

        /// <summary>
        /// 구르기 후 이동속도 버프 활성화 체크
        /// </summary>
        public static bool IsDodgeSpeedActive(Player player)
        {
            if (player == null) return false;
            if (!HasSkill("speed_base")) return false;

            if (dodgeSpeedEndTime.TryGetValue(player, out float endTime))
            {
                return Time.time < endTime;
            }
            return false;
        }

        /// <summary>
        /// 구르기 후 이동속도 보너스 적용
        /// </summary>
        public static void ApplyDodgeSpeedBuff(Player player)
        {
            if (player == null || !HasSkill("speed_base")) return;

            float duration = SkillTreeConfig.SpeedBaseDodgeDurationValue;
            dodgeSpeedEndTime[player] = Time.time + duration;

            ShowSkillEffectText(player,
                $"🏃 민첩함의 기초! (+{SkillTreeConfig.SpeedBaseDodgeMoveSpeedValue}% 이동속도)",
                new Color(0.2f, 0.9f, 1f), SkillEffectTextType.Combat);
        }

        /// <summary>
        /// 구르기 후 이동속도 보너스 값 가져오기
        /// </summary>
        public static float GetDodgeSpeedBonus(Player player)
        {
            if (IsDodgeSpeedActive(player))
            {
                return SkillTreeConfig.SpeedBaseDodgeMoveSpeedValue / 100f;
            }
            return 0f;
        }

        /// <summary>
        /// 3연속 근접 공격 체크 (melee_speed1용)
        /// </summary>
        public static void CheckTier8MeleeCombo(Player player)
        {
            if (!HasSkill("melee_speed1")) return;

            float currentTime = Time.time;

            if (!tier8MeleeComboCount.ContainsKey(player))
                tier8MeleeComboCount[player] = 0;
            if (!tier8MeleeLastHitTime.ContainsKey(player))
                tier8MeleeLastHitTime[player] = 0;

            // 3초 내 연속 공격인지 확인
            if (currentTime - tier8MeleeLastHitTime[player] < 3f)
            {
                tier8MeleeComboCount[player]++;
            }
            else
            {
                tier8MeleeComboCount[player] = 1;
            }

            tier8MeleeLastHitTime[player] = currentTime;

            // 3연속 공격 시 보너스 적용
            if (tier8MeleeComboCount[player] >= 3)
            {
                tier8MeleeComboEndTime[player] = currentTime + 3f; // 3초간 유지
                ShowSkillEffectText(player,
                    $"⚡ 근접 가속 3연속! (+{SkillTreeConfig.SpeedMeleeComboTripleBonusValue}% 공격속도)",
                    new Color(1f, 0.5f, 0.2f), SkillEffectTextType.Combat);
                tier8MeleeComboCount[player] = 0;
            }
        }

        /// <summary>
        /// 3연속 근접 공격 보너스 활성 체크
        /// </summary>
        public static bool IsTier8MeleeComboActive(Player player)
        {
            if (player == null) return false;
            if (tier8MeleeComboEndTime.TryGetValue(player, out float endTime))
            {
                return Time.time < endTime;
            }
            return false;
        }

        /// <summary>
        /// 3연속 마법 공격 체크 (staff_speed1용)
        /// </summary>
        public static void CheckStaffCombo(Player player)
        {
            if (!HasSkill("staff_speed1")) return;

            float currentTime = Time.time;

            if (!staffComboCount.ContainsKey(player))
                staffComboCount[player] = 0;
            if (!staffLastCastTime.ContainsKey(player))
                staffLastCastTime[player] = 0;

            // 4초 내 연속 시전인지 확인
            if (currentTime - staffLastCastTime[player] < 4f)
            {
                staffComboCount[player]++;
            }
            else
            {
                staffComboCount[player] = 1;
            }

            staffLastCastTime[player] = currentTime;

            // 3연속 시전 시 에이트르 회복
            if (staffComboCount[player] >= 3)
            {
                float maxEitr = player.GetMaxEitr();
                float recoveryAmount = maxEitr * (SkillTreeConfig.SpeedStaffTripleEitrRecoveryValue / 100f);
                player.AddEitr(recoveryAmount);

                ShowSkillEffectText(player,
                    $"✨ 시전 가속 3연속! (+{recoveryAmount:F0} 에이트르)",
                    new Color(0.6f, 0.3f, 1f), SkillEffectTextType.Combat);
                staffComboCount[player] = 0;
            }
        }

        /// <summary>
        /// 연속의 흐름 스태미나 감소 배율 계산
        /// </summary>
        public static float GetMeleeComboStaminaReduction(Player player)
        {
            if (player == null) return 1f;
            if (!HasSkill("melee_combo")) return 1f;

            if (meleeComboSpeedEndTime.TryGetValue(player, out float endTime) && Time.time < endTime)
            {
                return 1f - (SkillTreeConfig.SpeedMeleeComboStaminaValue / 100f);
            }
            return 1f;
        }

        /// <summary>
        /// 연속의 흐름 공격속도 보너스 가져오기
        /// </summary>
        public static float GetMeleeComboAttackSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            if (!HasSkill("melee_combo")) return 0f;

            if (meleeComboSpeedEndTime.TryGetValue(player, out float endTime) && Time.time < endTime)
            {
                return SkillTreeConfig.SpeedMeleeComboAttackSpeedValue;
            }
            return 0f;
        }

        /// <summary>
        /// 활 숙련자 스태미나 감소 배율 계산
        /// </summary>
        public static float GetBowExpertStaminaReduction(Player player)
        {
            if (player == null) return 1f;
            if (!HasSkill("bow_speed2")) return 1f;

            if (bowExpertStaminaEndTime.TryGetValue(player, out float endTime) && Time.time < endTime)
            {
                return 1f - (SkillTreeConfig.SpeedBowExpertStaminaValue / 100f);
            }
            return 1f;
        }

        /// <summary>
        /// 활 숙련자 장전속도 보너스 가져오기
        /// </summary>
        public static float GetBowExpertDrawSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            if (!HasSkill("bow_speed2")) return 0f;

            if (bowExpertStaminaEndTime.TryGetValue(player, out float endTime) && Time.time < endTime)
            {
                return SkillTreeConfig.SpeedBowExpertDrawSpeedValue;
            }
            return 0f;
        }

        /// <summary>
        /// 석궁 숙련자 버프 중 재장전 속도 보너스
        /// </summary>
        public static float GetCrossbowExpertReloadBonus(Player player)
        {
            if (player == null) return 0f;
            if (!HasSkill("crossbow_reload2")) return 0f;

            if (crossbowExpertSpeedEndTime.TryGetValue(player, out float endTime) && Time.time < endTime)
            {
                return SkillTreeConfig.SpeedCrossbowExpertReloadValue;
            }
            return 0f;
        }

        /// <summary>
        /// 활 장전 중 이동속도 보너스 (bow_draw1)
        /// </summary>
        public static float GetBowDrawMoveSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            if (!HasSkill("bow_draw1")) return 0f;

            if (isDrawingBow.TryGetValue(player, out bool drawing) && drawing)
            {
                return SkillTreeConfig.SpeedBowDrawMoveSpeedValue / 100f;
            }
            return 0f;
        }

        /// <summary>
        /// 석궁 재장전 중 이동속도 보너스 (crossbow_draw1)
        /// </summary>
        public static float GetCrossbowReloadMoveSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            if (!HasSkill("crossbow_draw1")) return 0f;

            if (isReloadingCrossbow.TryGetValue(player, out bool reloading) && reloading)
            {
                return SkillTreeConfig.SpeedCrossbowReloadMoveSpeedValue / 100f;
            }
            return 0f;
        }

        /// <summary>
        /// 이동 시전 에이트르 감소 배율 계산
        /// </summary>
        public static float GetMovingCastEitrReduction(Player player)
        {
            if (player == null) return 1f;
            if (!HasSkill("moving_cast")) return 1f;

            var weapon = player.GetCurrentWeapon();
            if (weapon == null) return 1f;

            bool isStaff = weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic ||
                          weapon.m_shared.m_skillType == Skills.SkillType.BloodMagic;

            if (isStaff)
            {
                return 1f - (SkillTreeConfig.SpeedStaffCastEitrReductionValue / 100f);
            }
            return 1f;
        }

        /// <summary>
        /// 이동 시전 이동속도 보너스
        /// </summary>
        public static float GetMovingCastSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            if (!HasSkill("moving_cast")) return 0f;

            var weapon = player.GetCurrentWeapon();
            if (weapon == null) return 0f;

            bool isStaff = weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic ||
                          weapon.m_shared.m_skillType == Skills.SkillType.BloodMagic;

            // 마법 시전 중인지 체크 (공격 중)
            if (isStaff && player.InAttack())
            {
                return SkillTreeConfig.SpeedStaffCastMoveSpeedValue / 100f;
            }
            return 0f;
        }

        /// <summary>
        /// 에너자이저 음식 소모 감소 배율
        /// </summary>
        public static float GetFoodDrainReduction()
        {
            if (!HasSkill("speed_master")) return 1f;
            return 1f - (SkillTreeConfig.SpeedFoodEfficiencyValue / 100f);
        }

        /// <summary>
        /// 선장 배 속도 보너스
        /// </summary>
        public static float GetShipSpeedBonus()
        {
            if (!HasSkill("ship_master")) return 0f;
            return SkillTreeConfig.SpeedShipBonusValue / 100f;
        }

        /// <summary>
        /// 숙련자 숙련 보너스 적용 (all_master)
        /// </summary>
        public static float GetAllMasterSkillBonus()
        {
            if (!HasSkill("all_master")) return 0f;
            return SkillTreeConfig.AllMasterRunSkillValue;
        }

        /// <summary>
        /// 수련자1 숙련 보너스 (근접/석궁)
        /// </summary>
        public static float GetSpeedEx1SkillBonus()
        {
            if (!HasSkill("speed_ex1")) return 0f;
            return SkillTreeConfig.SpeedEx1MeleeSkillValue;
        }

        /// <summary>
        /// 수련자2 숙련 보너스 (지팡이/활)
        /// </summary>
        public static float GetSpeedEx2SkillBonus()
        {
            if (!HasSkill("speed_ex2")) return 0f;
            return SkillTreeConfig.SpeedEx2StaffSkillValue;
        }

        /// <summary>
        /// 스킬 타입별 숙련도 레벨 보너스 계산
        /// 스킬트리로 얻은 숙련도 보너스를 반환 (사망해도 유지됨)
        /// 스킬트리 해제 시 자동으로 0이 되어 숙련도 원복
        /// </summary>
        public static float GetSkillLevelBonus(Skills.SkillType skillType)
        {
            float bonus = 0f;

            try
            {
                // === 속도 전문가 트리 ===

                // 수련자1 (speed_ex1): 근접무기 숙련 +3, 석궁 숙련 +3
                if (HasSkill("speed_ex1"))
                {
                    float ex1Bonus = SkillTreeConfig.SpeedEx1MeleeSkillValue;
                    switch (skillType)
                    {
                        case Skills.SkillType.Swords:
                        case Skills.SkillType.Clubs:
                        case Skills.SkillType.Knives:
                        case Skills.SkillType.Spears:
                        case Skills.SkillType.Polearms:
                        case Skills.SkillType.Crossbows:
                            bonus += ex1Bonus;
                            break;
                    }
                }

                // 수련자2 (speed_ex2): 지팡이 숙련 +3, 활 숙련 +3
                if (HasSkill("speed_ex2"))
                {
                    float ex2Bonus = SkillTreeConfig.SpeedEx2StaffSkillValue;
                    switch (skillType)
                    {
                        case Skills.SkillType.ElementalMagic:
                        case Skills.SkillType.BloodMagic:
                        case Skills.SkillType.Bows:
                            bonus += ex2Bonus;
                            break;
                    }
                }

                // 숙련자 (all_master): 모든 스텟 +2 (이동, 점프)
                if (HasSkill("all_master"))
                {
                    float allMasterBonus = SkillTreeConfig.AllMasterRunSkillValue;
                    switch (skillType)
                    {
                        case Skills.SkillType.Run:
                        case Skills.SkillType.Jump:
                            bonus += allMasterBonus;
                            break;
                    }
                }

                // 점프 숙련자 (agility_peak): 점프 숙련 +10
                if (HasSkill("agility_peak"))
                {
                    if (skillType == Skills.SkillType.Jump)
                    {
                        bonus += SkillTreeConfig.JumpSkillLevelBonusValue;
                    }
                }

                // === 활 전문가 트리 ===

                // 활 숙련 (bow_Step3_speedshot): 활 숙련도 +10
                if (HasSkill("bow_Step3_speedshot"))
                {
                    if (skillType == Skills.SkillType.Bows)
                    {
                        bonus += SkillTreeConfig.BowStep3SpeedShotSkillBonusValue;
                    }
                }

                // === 석궁 전문가 트리 ===
                // (현재 석궁 숙련도 보너스 스킬 없음, 추후 추가 가능)
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리] 숙련도 보너스 계산 오류: {ex.Message}");
            }

            return bonus;
        }

        /// <summary>
        /// 스킬트리 리셋 시 호출 - 보너스 숙련도 제거
        /// SkillResetCommand에서 호출됨
        /// </summary>
        public static void OnSkillTreeReset(Player player)
        {
            if (player == null) return;

            try
            {
                var skills = player.GetSkills();
                if (skills == null) return;

                Plugin.Log.LogInfo("[스킬트리] 숙련도 보너스 초기화 시작");

                // GetSkillLevelBonus가 이제 0을 반환하므로,
                // 별도의 레벨 하락 처리는 필요 없음
                // (Skills.GetSkillLevel 패치에서 보너스가 0이 되어 자동 적용)

                Plugin.Log.LogInfo("[스킬트리] 숙련도 보너스 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리] 숙련도 초기화 오류: {ex.Message}");
            }
        }
    }

    // === 추가 Harmony 패치들 ===

    /// <summary>
    /// 구르기 감지 - 민첩함의 기초 (speed_base)
    /// </summary>
    [HarmonyPatch(typeof(Player), "Dodge")]
    public static class SpeedTree_Player_Dodge_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player __instance)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;

                // speed_base: 구르기 후 이동속도 버프 적용
                if (SkillEffect.HasSkill("speed_base"))
                {
                    SkillEffect.ApplyDodgeSpeedBuff(__instance);
                    Plugin.Log.LogInfo($"[민첩함의 기초] 구르기 감지! 이동속도 버프 적용");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 구르기 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 음식 소모 속도 감소 - 에너자이저 (speed_master)
    /// </summary>
    [HarmonyPatch(typeof(Player), "UpdateFood")]
    public static class SpeedTree_Player_UpdateFood_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Prefix(Player __instance, float dt, ref bool forceUpdate)
        {
            // 이 패치는 음식 소모 속도를 조절하기 위해 dt를 수정할 수 없으므로
            // 대신 음식 타이머를 직접 조작해야 함
        }
    }

    /// <summary>
    /// 음식 아이템 소모 시간 증가 - 에너자이저 (speed_master)
    /// Player.GetFoods에서 음식 잔여 시간 조작
    /// </summary>
    [HarmonyPatch(typeof(Player.Food), "CanEatAgain")]
    public static class SpeedTree_PlayerFood_CanEatAgain_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player.Food __instance, ref bool __result)
        {
            // 에너자이저가 활성화되면 음식 지속시간이 늘어나 더 오래 지속됨
            // CanEatAgain이 false를 반환하는 동안 음식 효과가 유지됨
        }
    }

    /// <summary>
    /// 배 속도 증가 - 선장 (ship_master)
    /// </summary>
    [HarmonyPatch(typeof(Ship), "GetSailForce")]
    public static class SpeedTree_Ship_GetSailForce_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Ship __instance, ref Vector3 __result)
        {
            try
            {
                // 플레이어가 배를 조종 중인지 확인
                var player = Player.m_localPlayer;
                if (player == null) return;

                // ship_master 스킬 확인
                if (!SkillEffect.HasSkill("ship_master")) return;

                // 배 속도 증가
                float bonus = SkillEffect.GetShipSpeedBonus();
                if (bonus > 0f)
                {
                    __result *= (1f + bonus);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 배 속도 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 스태미나 사용 감소 - 연속의 흐름, 활 숙련자
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
    public static class SpeedTree_Player_UseStamina_Patch
    {
        [HarmonyPriority(Priority.High)]
        public static void Prefix(Player __instance, ref float v)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;

                var weapon = __instance.GetCurrentWeapon();
                if (weapon == null) return;

                var skillType = weapon.m_shared.m_skillType;

                // 연속의 흐름: 근접 무기 스태미나 감소
                bool isMelee = skillType == Skills.SkillType.Swords ||
                              skillType == Skills.SkillType.Clubs ||
                              skillType == Skills.SkillType.Knives ||
                              skillType == Skills.SkillType.Spears ||
                              skillType == Skills.SkillType.Polearms;

                if (isMelee)
                {
                    float meleeReduction = SkillEffect.GetMeleeComboStaminaReduction(__instance);
                    if (meleeReduction < 1f)
                    {
                        v *= meleeReduction;
                    }
                }

                // 활 숙련자: 활 스태미나 감소
                if (skillType == Skills.SkillType.Bows)
                {
                    float bowReduction = SkillEffect.GetBowExpertStaminaReduction(__instance);
                    if (bowReduction < 1f)
                    {
                        v *= bowReduction;
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 스태미나 감소 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 에이트르 사용 감소 - 이동 시전 (moving_cast)
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.UseEitr))]
    public static class SpeedTree_Player_UseEitr_Patch
    {
        [HarmonyPriority(Priority.High)]
        public static void Prefix(Player __instance, ref float v)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;

                // 이동 시전: 에이트르 감소
                float reduction = SkillEffect.GetMovingCastEitrReduction(__instance);
                if (reduction < 1f)
                {
                    v *= reduction;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 에이트르 감소 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 활/석궁 장전 상태 추적
    /// </summary>
    [HarmonyPatch(typeof(Player), "UpdateWeaponLoading")]
    public static class SpeedTree_Player_UpdateWeaponLoading_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player __instance, ItemDrop.ItemData weapon, float dt)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;
                if (weapon == null) return;

                var skillType = weapon.m_shared.m_skillType;

                // 활 장전 상태 추적
                if (skillType == Skills.SkillType.Bows)
                {
                    bool isDrawing = __instance.IsDrawingBow();
                    SkillEffect.isDrawingBow[__instance] = isDrawing;
                }

                // 석궁 재장전 상태 추적
                if (skillType == Skills.SkillType.Crossbows)
                {
                    // 석궁은 m_attackDrawTime으로 장전 상태 확인
                    bool isReloading = weapon.m_shared.m_attack.m_attackType == Attack.AttackType.Projectile;
                    SkillEffect.isReloadingCrossbow[__instance] = isReloading && __instance.InAttack();
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 무기 장전 상태 추적 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 숙련도 레벨 보너스 - 수련자1/2, 숙련자 (speed_ex1, speed_ex2, all_master)
    /// 스킬트리 보너스만큼 숙련도 레벨에 직접 추가
    /// </summary>
    [HarmonyPatch(typeof(Skills), nameof(Skills.GetSkillLevel))]
    public static class SpeedTree_Skills_GetSkillLevel_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Skills __instance, Skills.SkillType skillType, ref float __result)
        {
            try
            {
                float bonus = SkillEffect.GetSkillLevelBonus(skillType);
                if (bonus > 0f)
                {
                    __result += bonus;
                    // 최대 100 제한
                    if (__result > 100f) __result = 100f;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 숙련도 레벨 패치 오류: {ex.Message}");
            }
        }
    }

    // LowerAllSkills 패치 제거됨
    // GetSkillLevel 패치에서 보너스를 더해주므로, 실제 레벨이 0이어도 보너스만큼 표시됨
    // 실제 레벨을 건드리지 않아야 중복 보너스 문제가 발생하지 않음

    /// <summary>
    /// 마법 공격 시 3연속 체크 (staff_speed1)
    /// </summary>
    [HarmonyPatch(typeof(Attack), "Start")]
    public static class SpeedTree_Attack_Start_StaffCombo_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Attack __instance, Humanoid character, ref bool __result)
        {
            try
            {
                if (!__result) return;

                var player = character as Player;
                if (player == null || player != Player.m_localPlayer) return;

                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return;

                // 지팡이 사용 시 3연속 체크
                if (weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic ||
                    weapon.m_shared.m_skillType == Skills.SkillType.BloodMagic)
                {
                    SkillEffect.CheckStaffCombo(player);
                }

                // 근접 무기 3연속 체크 (melee_speed1)
                bool isMelee = weapon.m_shared.m_skillType == Skills.SkillType.Swords ||
                              weapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                              weapon.m_shared.m_skillType == Skills.SkillType.Knives ||
                              weapon.m_shared.m_skillType == Skills.SkillType.Spears ||
                              weapon.m_shared.m_skillType == Skills.SkillType.Polearms;

                if (isMelee)
                {
                    SkillEffect.CheckTier8MeleeCombo(player);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[속도 트리] 공격 시작 패치 오류: {ex.Message}");
            }
        }
    }
}

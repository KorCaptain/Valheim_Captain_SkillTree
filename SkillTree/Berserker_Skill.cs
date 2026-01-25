using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 버서커 직업 전용 스킬 시스템 (재작성 버전 - 무한 로딩 문제 해결)
    /// 액티브: 버서커 분노 (Y키) - 20초간 체력 비례 데미지 증가
    /// 패시브: 죽음의 무시 - 스태미나 리젠 +20%, 체력 10% 이하 시 8초 무적
    /// </summary>
    public static class BerserkerSkills
    {
        #region State Classes

        /// <summary>
        /// 버서커 분노 액티브 스킬 상태
        /// </summary>
        private class RageState
        {
            public float EndTime;
            public float CooldownEndTime;
            public int LastDamageTier = -1; // 20% 간격 표시용

            public bool IsActive => Time.time < EndTime;
            public bool OnCooldown => Time.time < CooldownEndTime;

            public void Clear()
            {
                EndTime = 0f;
                CooldownEndTime = 0f;
                LastDamageTier = -1;
            }
        }

        /// <summary>
        /// 버서커 패시브 스킬 상태
        /// </summary>
        private class PassiveState
        {
            public float EndTime;
            public float CooldownEndTime;
            public float LastNotificationTime;

            public bool IsActive => Time.time < EndTime;
            public bool OnCooldown => Time.time < CooldownEndTime;

            public void Clear()
            {
                EndTime = 0f;
                CooldownEndTime = 0f;
                LastNotificationTime = 0f;
            }
        }

        #endregion

        #region Fields

        private static Dictionary<Player, RageState> rageStates = new Dictionary<Player, RageState>();
        private static Dictionary<Player, PassiveState> passiveStates = new Dictionary<Player, PassiveState>();

        #endregion

        #region Active Skill - Berserker Rage

        /// <summary>
        /// 버서커 분노 스킬 시전
        /// </summary>
        public static bool CastBerserkerRage(Player player)
        {
            try
            {
                if (player == null)
                {
                    Plugin.Log.LogWarning("[버서커 분노] 플레이어가 null");
                    return false;
                }

                // 상태 가져오기 (없으면 생성)
                if (!rageStates.ContainsKey(player))
                    rageStates[player] = new RageState();

                var state = rageStates[player];

                // 쿨다운 확인
                if (state.OnCooldown)
                {
                    float remaining = state.CooldownEndTime - Time.time;
                    SkillEffect.ShowSkillEffectText(player, $"쿨다운 중 ({remaining:F0}초)",
                        Color.red, SkillEffect.SkillEffectTextType.Critical);
                    return false;
                }

                // 스태미나 확인
                float staminaCost = Berserker_Config.BerserkerRageStaminaCostValue;
                if (player.GetStamina() < staminaCost)
                {
                    SkillEffect.ShowSkillEffectText(player, "스태미나 부족!",
                        Color.red, SkillEffect.SkillEffectTextType.Critical);
                    return false;
                }

                // 이미 분노 상태인지 확인
                if (state.IsActive)
                {
                    SkillEffect.ShowSkillEffectText(player, "이미 분노 상태!",
                        Color.yellow, SkillEffect.SkillEffectTextType.Critical);
                    return false;
                }

                // 스태미나 소모
                player.UseStamina(staminaCost);

                // 분노 상태 적용
                float duration = Berserker_Config.BerserkerRageDurationValue;
                float cooldown = Berserker_Config.BerserkerRageCooldownValue;

                state.EndTime = Time.time + duration;
                state.CooldownEndTime = Time.time + cooldown;
                state.LastDamageTier = -1;

                // VFX 효과 생성 (자동 정리, 사운드 포함)
                CreateRageEffect(player);

                // 화면 텍스트
                SkillEffect.ShowSkillEffectText(player, "🔥 버서커 분노!",
                    Color.red, SkillEffect.SkillEffectTextType.XLarge);

                // 분노 시전 성공
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 시전 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 플레이어가 분노 상태인지 확인
        /// </summary>
        public static bool IsPlayerInRage(Player player)
        {
            if (player == null) return false;
            if (!rageStates.ContainsKey(player)) return false;
            return rageStates[player].IsActive;
        }

        /// <summary>
        /// 분노 데미지 보너스 계산 (Config 연동)
        /// </summary>
        public static float GetRageDamageBonus(Player player)
        {
            if (!IsPlayerInRage(player)) return 0f;

            try
            {
                // 체력 손실 비율 계산
                float currentHealthPercent = player.GetHealthPercentage();
                float lostHealthPercent = (1f - currentHealthPercent) * 100f;

                // Config 값 사용
                float damagePerPercent = Berserker_Config.BerserkerRageDamagePerHealthPercentValue;
                float maxBonus = Berserker_Config.BerserkerRageMaxDamageBonusValue;

                float damageBonus = lostHealthPercent * damagePerPercent;
                damageBonus = Mathf.Min(damageBonus, maxBonus);

                // 20% 간격 표시
                UpdateDamageDisplay(player, damageBonus);

                return damageBonus;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 데미지 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 20% 간격 데미지 증가 표시
        /// </summary>
        private static void UpdateDamageDisplay(Player player, float currentDamageBonus)
        {
            try
            {
                if (!rageStates.ContainsKey(player)) return;

                var state = rageStates[player];
                int currentTier = Mathf.FloorToInt(currentDamageBonus / 20f);
                currentTier = Mathf.Min(currentTier, 5);

                if (currentTier > state.LastDamageTier && currentTier > 0)
                {
                    int displayPercentage = currentTier * 20;
                    SkillEffect.ShowSkillEffectText(player,
                        $"🔥 분노 {displayPercentage}% 🔥",
                        Color.red,
                        SkillEffect.SkillEffectTextType.XLarge);

                    state.LastDamageTier = currentTier;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 표시 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 분노 VFX 효과 생성 (WackyEpicMMO 방식 - 플레이어를 따라다니는 이펙트)
        /// </summary>
        private static void CreateRageEffect(Player player)
        {
            try
            {
                // SimpleVFX 방식: Valheim 내장 VFX로 플레이어 효과
                SimpleVFX.PlayOnPlayer(player, Berserker_Config.BerserkerRageDurationValue);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] VFX 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 몬스터 적중 시 VFX 효과 (WackyEpicMMO CriticalVFX 방식)
        /// </summary>
        public static void CreateMonsterHitEffect(Character target)
        {
            try
            {
                if (target == null) return;

                // SimpleVFX 방식: Valheim 내장 VFX로 몬스터 타격 효과
                SimpleVFX.PlayAtPosition(target.transform.position + Vector3.up * 1f, 2f);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 적중 효과 실패: {ex.Message}");
            }
        }

        #endregion

        #region Passive Skill - Death Defiance

        /// <summary>
        /// 버서커 패시브 스킬 체크
        /// </summary>
        public static void CheckBerserkerPassiveSkill(Player player)
        {
            try
            {
                if (player == null) return;

                // 상태 가져오기 (없으면 생성)
                if (!passiveStates.ContainsKey(player))
                    passiveStates[player] = new PassiveState();

                var state = passiveStates[player];

                // 이미 패시브 무적 상태면 패스
                if (state.IsActive) return;

                // 쿨다운 중이면 패스
                if (state.OnCooldown) return;

                // 체력 임계값 확인
                float currentHealthPercent = player.GetHealthPercentage();
                float threshold = Berserker_Config.BerserkerPassiveHealthThresholdValue / 100f;

                if (currentHealthPercent > threshold) return;

                // 패시브 무적 발동
                ApplyPassiveInvincibility(player, state);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] 체크 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 패시브 무적 상태인지 확인
        /// </summary>
        public static bool IsPassiveInvincibilityActive(Player player)
        {
            if (player == null) return false;
            if (!passiveStates.ContainsKey(player)) return false;
            return passiveStates[player].IsActive;
        }

        /// <summary>
        /// 패시브 무적 쿨다운 확인
        /// </summary>
        public static bool IsPassiveInvincibilityOnCooldown(Player player)
        {
            if (player == null) return false;
            if (!passiveStates.ContainsKey(player)) return false;
            return passiveStates[player].OnCooldown;
        }

        /// <summary>
        /// 스태미나 리젠 보너스 (+20%)
        /// </summary>
        public static float GetBerserkerStaminaRegenBonus(Player player)
        {
            if (player == null) return 0f;
            return 0.2f; // 20% 보너스
        }

        /// <summary>
        /// 패시브 무적 적용
        /// </summary>
        private static void ApplyPassiveInvincibility(Player player, PassiveState state)
        {
            try
            {
                float duration = Berserker_Config.BerserkerPassiveInvincibilityDurationValue;
                float cooldown = Berserker_Config.BerserkerPassiveCooldownValue;

                state.EndTime = Time.time + duration;
                state.CooldownEndTime = Time.time + cooldown;
                state.LastNotificationTime = 0f;

                // VFX 효과 (자동 정리)
                CreatePassiveEffect(player);

                SkillEffect.ShowSkillEffectText(player, "⚡ 죽음의 무시 ⚡",
                    Color.yellow, SkillEffect.SkillEffectTextType.XLarge);

                // 무적 발동 완료
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] 무적 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 패시브 VFX 효과 생성 (WackyEpicMMO 방식)
        /// </summary>
        private static void CreatePassiveEffect(Player player)
        {
            try
            {
                // SimpleVFX 방식: Valheim 내장 VFX로 패시브 효과
                SimpleVFX.PlayOnPlayer(player, 3f);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] VFX 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 패시브 쿨다운 알림
        /// </summary>
        public static void CheckPassiveCooldownNotifications(Player player)
        {
            try
            {
                if (player == null) return;
                if (!passiveStates.ContainsKey(player)) return;

                var state = passiveStates[player];
                if (!state.OnCooldown) return;

                float remainingTime = state.CooldownEndTime - Time.time;
                if (remainingTime <= 0) return;

                float currentTime = Time.time;
                float timeSinceLastNotification = currentTime - state.LastNotificationTime;

                bool shouldNotify = false;

                // 마지막 1분 이하: 20초 간격
                if (remainingTime <= 60f && timeSinceLastNotification >= 20f)
                {
                    shouldNotify = true;
                }
                // 1분 초과: 60초 간격
                else if (remainingTime > 60f && timeSinceLastNotification >= 60f)
                {
                    shouldNotify = true;
                }

                if (shouldNotify)
                {
                    int minutes = Mathf.FloorToInt(remainingTime / 60f);
                    int seconds = Mathf.FloorToInt(remainingTime % 60f);

                    string message = minutes > 0
                        ? $"🛡️ 패시브 무적 쿨타임: {minutes}분 {seconds:00}초"
                        : $"🛡️ 패시브 무적 쿨타임: {seconds}초";

                    SkillEffect.ShowSkillEffectText(player, message,
                        new Color(0.8f, 0.8f, 1f), SkillEffect.SkillEffectTextType.Standard);

                    state.LastNotificationTime = currentTime;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] 알림 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 버서커 스킬 보유 확인
        /// </summary>
        private static bool HasBerserkerSkill(Player player)
        {
            try
            {
                if (player == null) return false;
                return SkillEffect.HasSkill("Berserker");
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// 안전한 정리 (무한 로딩 방지)
        /// </summary>
        public static void SafeCleanup(Player player)
        {
            try
            {
                if (player == null)
                {
                    // 플레이어가 null이면 전체 초기화
                    try { rageStates?.Clear(); } catch { }
                    try { passiveStates?.Clear(); } catch { }
                    return;
                }

                // 상태 제거 (예외 무시)
                try
                {
                    if (rageStates != null && rageStates.ContainsKey(player))
                    {
                        try { rageStates[player]?.Clear(); } catch { }
                        try { rageStates.Remove(player); } catch { }
                    }
                }
                catch { }

                try
                {
                    if (passiveStates != null && passiveStates.ContainsKey(player))
                    {
                        try { passiveStates[player]?.Clear(); } catch { }
                        try { passiveStates.Remove(player); } catch { }
                    }
                }
                catch { }
            }
            catch
            {
                // 모든 예외 완전히 무시 - 무한 로딩 절대 방지
            }
        }

        /// <summary>
        /// 모든 상태 초기화
        /// </summary>
        public static void ClearAllStates()
        {
            try
            {
                rageStates?.Clear();
                passiveStates?.Clear();
                // 상태 초기화 완료
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 정리] 초기화 실패: {ex.Message}");
            }
        }

        #endregion

        #region Harmony Patches

        /// <summary>
        /// 데미지 패치 - 분노 보너스 및 패시브 무적
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Berserker_Character_Damage_Patch
        {
            public static void Prefix(Character __instance, ref HitData hit)
            {
                try
                {
                    // 플레이어가 아니면 패스
                    if (!(__instance is Player player)) return;

                    // 버서커 스킬이 없으면 패스
                    if (!HasBerserkerSkill(player)) return;

                    // === 분노 데미지 보너스만 적용 ===
                    if (IsPlayerInRage(player))
                    {
                        float damageBonus = GetRageDamageBonus(player);
                        if (damageBonus > 0f)
                        {
                            float multiplier = 1f + (damageBonus / 100f);
                            hit.m_damage.Modify(multiplier);

                            // 몬스터 적중 효과
                            if (__instance != player)
                            {
                                CreateMonsterHitEffect(__instance);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[버서커 패치] Damage 패치 실패: {ex.Message}");
                }
            }
        }

        // Player Update 패치 제거 - 무한 로딩 원인
        // 분노 종료는 Coroutine으로 처리하거나 시간 제한 없이 영구 적용

        #endregion
    }
}

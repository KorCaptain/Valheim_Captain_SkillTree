using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 즉시 힐링 시스템 - G키로 범위 내 즉시 힐링
    /// 시전자 중심 12m 범위, 다른 플레이어만 치료 (자기 치료 불가)
    /// 적중한 아군 체력의 25%를 즉시 회복, 에이트르 30 소모, 30초 쿨타임
    /// </summary>
    public static class Mage_HealerMode
    {
        // === 즉시 힐링 쿨다운 관리 ===
        private static Dictionary<Player, float> instantHealCooldowns = new Dictionary<Player, float>();

        // === 설정값 ===
        // Config 연동으로 변경 - 동적 값 사용
        private static float INSTANT_HEAL_COOLDOWN => HealerMode_Config.HealerModeCooldownValue;           // Config 연동
        private static float INSTANT_HEAL_EITR_COST => HealerMode_Config.HealerModeEitrCostValue;         // Config 연동
        private static float HEAL_PERCENTAGE => HealerMode_Config.HealPercentageValue / 100f;  // Config 연동 (% → 소수)
        private static float HEAL_RANGE => HealerMode_Config.HealRangeValue;                   // Config 연동

        /// <summary>
        /// 메이지 즉시 힐링 시전 (G키 - 범위 내 즉시 힐링)
        /// 쿨타임 중이면 남은 시간 표시
        /// </summary>
        public static bool CastInstantHeal(Player player)
        {
            try
            {
                // 쿨다운 확인
                if (IsInstantHealOnCooldown(player))
                {
                    float remaining = GetInstantHealCooldownRemaining(player);
                    player.Message(MessageHud.MessageType.Center, $"🩺 즉시 힐링 쿨다운 {remaining:F0}초 남음");
                    SkillEffect.DrawFloatingText(player, $"🩺 쿨다운 {remaining:F0}초", Color.red);
                    return false;
                }

                // 지팡이/완드 착용 확인
                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    StaffEquipmentDetector.ShowStaffRequiredMessage(player);
                    return false;
                }

                // Eitr 확인
                if (player.GetEitr() < INSTANT_HEAL_EITR_COST)
                {
                    SkillEffect.DrawFloatingText(player, $"❌ Eitr 부족! (필요: {INSTANT_HEAL_EITR_COST})", Color.red);
                    return false;
                }

                // Eitr 소모
                player.UseEitr(INSTANT_HEAL_EITR_COST);

                // 쿨다운 설정
                instantHealCooldowns[player] = Time.time + INSTANT_HEAL_COOLDOWN;

                // 즉시 힐링 실행
                PerformInstantHeal(player);

                // 성공 메시지
                SkillEffect.DrawFloatingText(player, $"🩺 즉시 힐링 시전!", new Color(0.2f, 1f, 0.2f));

                Plugin.Log.LogInfo($"[메이지 즉시 힐링] {player.GetPlayerName()} 즉시 힐링 시전 - 범위: {HEAL_RANGE}m, 힐링량: {HEAL_PERCENTAGE * 100:F0}%, 쿨타임: {INSTANT_HEAL_COOLDOWN}초");
                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 즉시 힐링] 즉시 힐링 시전 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 즉시 힐링 실행 - 범위 내 모든 다른 플레이어 힐링
        /// </summary>
        private static void PerformInstantHeal(Player caster)
        {
            Vector3 casterPos = caster.transform.position;

            // 1. 활성화 사운드 재생
            PlayInstantHealSound(caster);

            // 2. 범위 내 모든 플레이어에게 즉시 힐링 적용 (시전자 제외)
            var nearbyPlayers = Player.GetAllPlayers()
                .Where(p => p != caster && Vector3.Distance(p.transform.position, casterPos) <= HEAL_RANGE && !p.IsDead())
                .ToList();

            Plugin.Log.LogInfo($"[메이지 즉시 힐링] 범위 내 플레이어 발견: {nearbyPlayers.Count}명");

            foreach (var targetPlayer in nearbyPlayers)
            {
                HealPlayer(targetPlayer, caster);
            }

            // 3. 시전자에게 힐링 VFX 표시 (자기 자신은 치료 안됨을 명시)
            PlayHealingVFX(caster, false); // 자기 자신은 치료 안됨

            if (nearbyPlayers.Count == 0)
            {
                SkillEffect.DrawFloatingText(caster, "❌ 범위 내 치료할 플레이어 없음", Color.yellow);
            }
        }

        /// <summary>
        /// 개별 플레이어 힐링
        /// </summary>
        private static void HealPlayer(Player targetPlayer, Player caster)
        {
            try
            {
                float maxHealth = targetPlayer.GetMaxHealth();
                float healAmount = maxHealth * HEAL_PERCENTAGE;
                float currentHealth = targetPlayer.GetHealth();
                float newHealth = Mathf.Min(currentHealth + healAmount, maxHealth);

                targetPlayer.SetHealth(newHealth);

                // 힐링 VFX 및 메시지
                PlayHealingVFX(targetPlayer, true);
                SkillEffect.DrawFloatingText(targetPlayer, $"💚 +{healAmount:F0} HP", Color.green);
                targetPlayer.Message(MessageHud.MessageType.Center, $"🩺 {caster.GetPlayerName()}이(가) 치료했습니다");

                Plugin.Log.LogInfo($"[메이지 즉시 힐링] {caster.GetPlayerName()} → {targetPlayer.GetPlayerName()} 힐링: {healAmount:F0} HP ({HEAL_PERCENTAGE * 100:F0}%)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[메이지 즉시 힐링] {targetPlayer.GetPlayerName()} 힐링 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 즉시 힐링이 쿨다운 중인지 확인
        /// </summary>
        public static bool IsInstantHealOnCooldown(Player player)
        {
            if (!instantHealCooldowns.ContainsKey(player))
                return false;

            return Time.time < instantHealCooldowns[player];
        }

        /// <summary>
        /// 즉시 힐링 쿨다운 남은 시간
        /// </summary>
        public static float GetInstantHealCooldownRemaining(Player player)
        {
            if (!instantHealCooldowns.ContainsKey(player))
                return 0f;

            float remaining = instantHealCooldowns[player] - Time.time;
            return Mathf.Max(0f, remaining);
        }

        /// <summary>
        /// 즉시 힐링 사운드 재생
        /// </summary>
        private static void PlayInstantHealSound(Player player)
        {
            try
            {
                // VFXManager는 VFX 전용이므로 사운드는 Player.m_localPlayer를 통해 재생
                if (Player.m_localPlayer != null)
                {
                    Player.m_localPlayer.GetComponent<AudioSource>()?.PlayOneShot(null); // 사운드 재생은 별도 구현 필요
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[메이지 즉시 힐링] 사운드 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 힐링 VFX 표시
        /// </summary>
        private static void PlayHealingVFX(Player player, bool isHealed)
        {
            try
            {
                if (isHealed)
                {
                    // VFXManager의 실제 메서드 사용
                    VFXManager.PlayVFX(HealerMode_Config.HealingVFXValue, player.transform.position);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[메이지 즉시 힐링] VFX 표시 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 힐러모드 활성 상태 확인 - 즉시 힐링 시스템에서는 false 반환
        /// </summary>
        public static bool IsHealerModeActive(Player player)
        {
            return false; // 즉시 힐링 시스템에서는 지속 모드 없음
        }

        /// <summary>
        /// 힐러모드 남은 시간 - 즉시 힐링 시스템에서는 0 반환
        /// </summary>
        public static float GetHealerModeTimeRemaining(Player player)
        {
            return 0f; // 즉시 힐링 시스템에서는 지속 시간 없음
        }

        /// <summary>
        /// 힐러모드 쿨다운 남은 시간 - 즉시 힐링 쿨다운으로 변경
        /// </summary>
        public static float GetHealerModeCooldownRemaining(Player player)
        {
            return GetInstantHealCooldownRemaining(player);
        }

        /// <summary>
        /// 모든 즉시 힐링 상태 정리 (플레이어 로그아웃 시 등)
        /// </summary>
        public static void ClearHealerModeStates(Player player)
        {
            try
            {
                // 즉시 힐링 쿨다운 제거
                instantHealCooldowns.Remove(player);

                Plugin.Log.LogInfo($"[메이지 즉시 힐링] {player.GetPlayerName()} 모든 힐링 상태 정리 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 즉시 힐링] 상태 정리 오류: {ex.Message}");
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폴암 공격 범위 보너스 패치
    /// Attack.Start() Postfix - m_attackRange 필드 직접 수정
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class Attack_Start_PolearmRange_Patch
    {
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 폴암 공격 범위 보너스 계산
                float rangeBonus = SkillEffect.GetTotalPolearmRangeBonus(player);

                if (rangeBonus > 0f)
                {
                    float originalRange = __instance.m_attackRange;
                    __instance.m_attackRange *= (1f + (rangeBonus / 100f));

                    Plugin.Log.LogDebug($"[폴암 공격 범위] {originalRange:F2} → {__instance.m_attackRange:F2} (+{rangeBonus}%)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmRange_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 폴암 스태미나 감소 패치
    /// Attack.Start() Postfix - m_attackStamina 필드 직접 수정
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class Attack_Start_PolearmStamina_Patch
    {
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 반달 베기 스태미나 감소
                float staminaReduction = SkillEffect.GetPolearmStaminaReduction();

                if (staminaReduction > 0f)
                {
                    float originalStamina = __instance.m_attackStamina;
                    __instance.m_attackStamina *= (1f - (staminaReduction / 100f));

                    Plugin.Log.LogDebug($"[반달 베기] 스태미나 소모 {originalStamina:F1} → {__instance.m_attackStamina:F1} (-{staminaReduction}%)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmStamina_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 영웅 타격 스태거/넉백 효과 패치
    /// polearm_step2_hero: 27% 확률로 적을 스태거
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_PolearmHeroKnockback_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.Low)]
        static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null) return;
                if (__instance.IsPlayer()) return; // 플레이어는 제외

                // 로컬 플레이어 직접 참조 (DrawFloatingText가 Player.m_localPlayer와 동일성 비교)
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 로컬 플레이어의 공격인지 확인
                var attacker = hit.GetAttacker();
                if (attacker == null || attacker != player) return;

                // 영웅 타격 스킬 확인
                float knockbackChance = SkillEffect.GetPolearmHeroKnockbackChance();
                if (knockbackChance <= 0f) return;

                // 확률 체크
                if (UnityEngine.Random.value * 100f > knockbackChance) return;

                // 텍스트는 IsDead 무관하게 표시 (스태거 확률 발동 알림)
                SkillEffect.DrawFloatingText(player, "⚔️ " + L.Get("hero_strike_stagger"));

                // 스태거는 생존한 대상에만 적용
                if (!__instance.IsDead())
                {
                    Vector3 knockbackDir = (__instance.transform.position - player.transform.position).normalized;
                    __instance.Stagger(knockbackDir);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_PolearmHeroKnockback_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 폴암 휠 마우스(특수 공격) 데미지 보너스 패치
    /// Humanoid.StartAttack(secondaryAttack=true) 파라미터로 확실하게 감지
    /// 회전베기 (polearm_step1_spin) +60%
    /// 폭풍베기 (polearm_step3_ground) → 별도 PolearmStormSlash 패치에서 처리
    /// </summary>
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
    public static class Attack_Start_PolearmWheelDetect_Patch
    {
        // 마지막 특수 공격 시간 추적
        private static Dictionary<Player, float> lastSecondaryAttackTime = new Dictionary<Player, float>();

        [HarmonyPriority(HarmonyLib.Priority.High)]
        static void Prefix(Humanoid __instance, bool secondaryAttack)
        {
            try
            {
                var player = __instance as Player;
                if (player == null || player != Player.m_localPlayer) return;
                if (!SkillEffect.IsUsingPolearm(player)) return;

                // 회전베기 또는 폭풍베기 스킬이 있을 때만 처리
                bool hasWheelSkill = SkillEffect.GetPolearmWheelDamageBonus() > 0f
                                   || SkillEffect.HasSkill("polearm_step3_ground");
                if (!hasWheelSkill) return;

                if (secondaryAttack)
                {
                    // 특수 공격 시간 기록 (Character.Damage에서 확인용)
                    lastSecondaryAttackTime[player] = Time.time;
                    Plugin.Log.LogDebug($"[폴암] 특수 공격 감지");
                }
                else if (SkillEffect.HasSkill("polearm_step3_ground"))
                {
                    // 1차 일반 공격 → 폭풍베기 4초 프라이밍
                    SkillEffect.polearmStormSlashPrimedTime[player] = Time.time;
                    Plugin.Log.LogDebug($"[폭풍베기] 프라이밍 시작 (4초 창)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Attack_Start_PolearmWheelDetect_Patch] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 최근 특수 공격 여부 확인 (1.5초 이내 - 느린 폴암 애니메이션 대응)
        /// </summary>
        public static bool IsRecentSecondaryAttack(Player player)
        {
            if (player == null) return false;
            return lastSecondaryAttackTime.TryGetValue(player, out float ppSecAtk) && Time.time - ppSecAtk < 1.5f;
        }

        /// <summary>
        /// 정리
        /// </summary>
        public static void Cleanup(Player player)
        {
            if (player != null)
            {
                lastSecondaryAttackTime.Remove(player);
            }
        }
    }

    /// <summary>
    /// 폴암 휠 마우스 데미지 적용 패치
    /// Character.Damage에서 최종 데미지에 휠 보너스 적용
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_PolearmWheelDamage_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.High)]
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null) return;
                if (__instance.IsPlayer()) return; // 플레이어는 제외

                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 최근 특수 공격인지 확인
                if (!Attack_Start_PolearmWheelDetect_Patch.IsRecentSecondaryAttack(player)) return;

                // 휠 마우스 보너스 계산
                float wheelBonus = SkillEffect.GetPolearmWheelDamageBonus();
                if (wheelBonus <= 0f) return;

                // 물리 데미지에 보너스 적용
                float multiplier = 1f + (wheelBonus / 100f);
                hit.m_damage.m_slash *= multiplier;
                hit.m_damage.m_pierce *= multiplier;
                hit.m_damage.m_blunt *= multiplier;

                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                SkillEffect.DrawFloatingText(player, "🌀 " + L.Get("wheel_attack", wheelBonus));
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_PolearmWheelDamage_Patch] 오류: {ex.Message}");
            }
        }
    }

    // 관통 돌격 데미지 패치 제거됨 - 코루틴에서 직접 데미지 적용하므로 중복 방지

    /// <summary>
    /// 폴암 스킬 정리 (Player 사망/로그아웃 시)
    /// </summary>
    public static partial class SkillEffect
    {
        public static void CleanupPolearmSkillsOnDeath(Player player)
        {
            try
            {
                polearmAreaComboCount.Remove(player);
                polearmAreaLastHitTime.Remove(player);

                // 관통 돌격 상태 정리
                polearmPierceChargeLastUseTime.Remove(player);
                polearmPierceChargeActive.Remove(player);

                if (polearmPierceChargeCoroutines.TryGetValue(player, out var ppCleanupCoroutine) && ppCleanupCoroutine != null)
                {
                    try
                    {
                        player.StopCoroutine(ppCleanupCoroutine);
                    }
                    catch { }
                    polearmPierceChargeCoroutines.Remove(player);
                }

                Attack_Start_PolearmWheelDetect_Patch.Cleanup(player);
                CleanupStormSlashOnDeath(player);

                Plugin.Log.LogDebug("[폴암 스킬] 플레이어 상태 정리 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[폴암 스킬] 정리 실패: {ex.Message}");
            }
        }
    }
}

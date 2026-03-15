using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폭풍베기 (polearm_step3_ground) 전용 효과
    /// 1차 일반 공격 후 4초 이내 2차 특수(휠 마우스) 공격 시:
    /// - VFX (fx_lightningweapon_hit)
    /// - 번개 속성 데미지 추가 (PolearmStep3StormSlashExplosionValue)
    /// </summary>
    public static partial class SkillEffect
    {
        private const float STORM_SLASH_WINDOW = 4f;

        // 1차 공격 시간 추적 (플레이어별)
        public static Dictionary<Player, float> polearmStormSlashPrimedTime = new Dictionary<Player, float>();

        /// <summary>
        /// 폭풍베기 발동 조건 확인 (4초 이내 프라이밍)
        /// </summary>
        public static bool IsStormSlashPrimed(Player player)
        {
            if (player == null) return false;
            if (!polearmStormSlashPrimedTime.TryGetValue(player, out float primedTime)) return false;
            return Time.time - primedTime < STORM_SLASH_WINDOW;
        }

        /// <summary>
        /// 폭풍베기 상태 정리 (사망/로그아웃 시)
        /// </summary>
        public static void CleanupStormSlashOnDeath(Player player)
        {
            if (player == null) return;
            polearmStormSlashPrimedTime.Remove(player);
        }
    }

    /// <summary>
    /// 폭풍베기 2차 공격(휠) 적중 패치
    /// 4초 이내 프라이밍 상태 + 2차 특수 공격 → 번개속성 추가 + VFX
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_PolearmStormSlash_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.High)]
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null) return;
                if (__instance.IsPlayer()) return;

                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;
                if (!SkillEffect.HasSkill("polearm_step3_ground")) return;
                if (!Attack_Start_PolearmWheelDetect_Patch.IsRecentSecondaryAttack(player)) return;
                if (!SkillEffect.IsStormSlashPrimed(player)) return;

                float lightningBonus = SkillTreeConfig.PolearmStep3StormSlashExplosionValue;

                // 번개 속성 데미지 추가
                hit.m_damage.m_lightning += lightningBonus;

                // VFX - 번개 효과
                VFXManager.PlayVFXMultiplayer("fx_lightningweapon_hit", "", __instance.GetCenterPoint(), Quaternion.identity, 2f);

                // 발동 텍스트
                SkillEffect.DrawFloatingText(player, "⚡ " + L.Get("storm_slash_triggered", lightningBonus));

                Plugin.Log.LogInfo($"[폭풍베기] 번개 +{lightningBonus} 적용");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_PolearmStormSlash_Patch] 오류: {ex.Message}");
            }
        }
    }
}

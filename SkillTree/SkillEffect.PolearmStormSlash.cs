using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폭풍베기 (polearm_step3_ground) 전용 효과
    /// 휠 마우스 특수 공격 적중 시:
    /// - 1차: 베기 +30% 즉시 적용 + vfx_HealthUpgrade
    /// - 2초 후: 폭발 +60% + fx_crit VFX
    /// </summary>
    public static partial class SkillEffect
    {
        // 폭풍베기 코루틴 추적 (플레이어별 다수 코루틴 가능)
        public static Dictionary<Player, List<Coroutine>> polearmStormSlashCoroutines = new Dictionary<Player, List<Coroutine>>();

        /// <summary>
        /// 폭풍베기 폭발 코루틴 - delay초 후 대상에 폭발 데미지 적용
        /// </summary>
        internal static IEnumerator ExecuteStormSlashExplosion(Player player, Character target, ItemDrop.ItemData weapon, float delay = 2f)
        {
            yield return new WaitForSeconds(delay);

            if (player == null || player.IsDead()) yield break;
            if (target == null || weapon == null) yield break;

            float explosionBonus = SkillTreeConfig.PolearmStep3StormSlashExplosionValue;

            // VFX + 텍스트는 대상 생사 무관하게 항상 표시
            try
            {
                VFXManager.PlayVFXMultiplayer("fx_crit", "", target.GetCenterPoint(), Quaternion.identity, 2f);
            }
            catch { }

            DrawFloatingText(player, "💥 " + L.Get("storm_slash_explosion", explosionBonus));

            // 데미지는 생존한 대상에만 적용
            if (!target.IsDead())
            {
                try
                {
                    var weaponDamage = weapon.GetDamage();
                    var hit = new HitData();
                    hit.m_damage.m_slash = weaponDamage.m_slash * (1f + explosionBonus / 100f);
                    hit.m_point = target.GetCenterPoint();
                    hit.m_dir = (target.transform.position - player.transform.position).normalized;
                    hit.m_attacker = player.GetZDOID();
                    hit.SetAttacker(player);
                    hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                    target.Damage(hit);
                    Plugin.Log.LogInfo($"[폭풍베기] 폭발 데미지 +{explosionBonus}% 적용");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[폭풍베기] 폭발 오류: {ex.Message}");
                }
            }
            else
            {
                Plugin.Log.LogDebug($"[폭풍베기] 대상 이미 사망 - VFX/텍스트만 표시");
            }
        }

        /// <summary>
        /// 폭풍베기 코루틴 정리 (사망/로그아웃 시)
        /// </summary>
        public static void CleanupStormSlashOnDeath(Player player)
        {
            if (player == null) return;
            if (!polearmStormSlashCoroutines.TryGetValue(player, out var coroutines)) return;

            foreach (var cr in coroutines)
            {
                if (cr != null)
                {
                    try { player.StopCoroutine(cr); } catch { }
                }
            }
            polearmStormSlashCoroutines.Remove(player);
        }
    }

    /// <summary>
    /// 폭풍베기 1차 타격 패치
    /// Character.Damage Prefix - 휠 공격 시 1차 베기 +30% 적용 후 2초 뒤 폭발 예약
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

                float firstBonus = SkillTreeConfig.PolearmStep3GroundWheelDamageValue;

                // 1차 베기 보너스 즉시 적용
                hit.m_damage.m_slash *= (1f + firstBonus / 100f);

                // Valheim 기본 VFX - 대상에 재생
                VFXManager.PlayVFXMultiplayer("vfx_HealthUpgrade", "", __instance.GetCenterPoint(), Quaternion.identity, 2f);
                SkillEffect.DrawFloatingText(player, "🌀 " + L.Get("storm_slash_first", firstBonus));

                // 2초 후 폭발 코루틴 시작
                var weapon = player.GetCurrentWeapon();
                if (weapon != null)
                {
                    var coroutine = player.StartCoroutine(SkillEffect.ExecuteStormSlashExplosion(player, __instance, weapon, 2f));

                    if (!SkillEffect.polearmStormSlashCoroutines.ContainsKey(player))
                        SkillEffect.polearmStormSlashCoroutines[player] = new List<Coroutine>();
                    SkillEffect.polearmStormSlashCoroutines[player].Add(coroutine);
                }

                Plugin.Log.LogInfo($"[폭풍베기] 1차 베기 +{firstBonus}% 적용, 폭발 예약 중");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_PolearmStormSlash_Patch] 오류: {ex.Message}");
            }
        }
    }
}

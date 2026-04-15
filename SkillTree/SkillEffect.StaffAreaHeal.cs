using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 지팡이 즉시 범위 힐 액티브 스킬 시스템
    /// H키로 시전자 주변 플레이어에게 즉시 힐링 적용
    /// </summary>
    public static partial class SkillEffect
    {
        // === 힐 스킬 쿨타임 관리 ===
        private static Dictionary<Player, float> staffHealCooldowns = new Dictionary<Player, float>();

        /// <summary>
        /// 지팡이 즉시 범위 힐 스킬
        /// </summary>
        public static void ActivateStaffAreaHeal(Player player)
        {
            try
            {
                // 쿨타임 확인
                if (staffHealCooldowns.ContainsKey(player) && Time.time < staffHealCooldowns[player])
                {
                    float remaining = staffHealCooldowns[player] - Time.time;
                    DrawFloatingText(player, L.Get("heal_cooldown", Mathf.CeilToInt(remaining)), Color.red);
                    return;
                }

                // 에이트르 확인
                float eitrCost = Staff_Config.StaffHealEitrCostValue;
                if (player.GetEitr() < eitrCost)
                {
                    DrawFloatingText(player, L.Get("staff_eitr_insufficient", eitrCost), Color.red);
                    return;
                }

                // 에이트르 소모
                player.UseEitr(eitrCost);

                // 쿨타임 적용
                staffHealCooldowns[player] = Time.time + Staff_Config.StaffHealCooldownValue;
                ActiveSkillCooldownRegistry.SetCooldown("H", Staff_Config.StaffHealCooldownValue);

                // 범위 힐 실행
                ExecuteAreaHeal(player);

                Plugin.Log.LogInfo($"[지팡이 범위 힐] 발동 - 범위: {Staff_Config.StaffHealRangeValue}m, 치료량: {Staff_Config.StaffHealPercentageValue}%");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 범위 힐] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 시전자를 중심으로 한 즉시 범위 치료 실행
        /// </summary>
        private static void ExecuteAreaHeal(Player caster)
        {
            try
            {
                Vector3 casterPos = caster.transform.position;
                float healRange = Staff_Config.StaffHealRangeValue;
                float healPercent = Staff_Config.StaffHealPercentageValue / 100f;

                // VFX 재생 (하드코딩)
                try
                {
                    // buff_03a_aura: 시전자 캐릭터에 부착 → 따라다니며 3초 후 자동 종료
                    SimpleVFX.PlayOnPlayer(caster, "buff_03a_aura", 3f);
                    // 사운드는 고정 위치 재생
                    SimpleVFX.Play("sfx_dverger_heal_finish", casterPos, 2f);
                    // 방패 발전기 충전 VFX (발헤임 기본 VFX → VFXManager 사용)
                    CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("vfx_shieldgenerator_refuel", "", casterPos, Quaternion.identity, 3f);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[지팡이 힐] VFX 재생 실패: {ex.Message}");
                }

                // 범위 내 플레이어에게 힐링 적용 (시전자 제외 - 아군만 힐링)
                var allPlayers = Player.GetAllPlayers();
                var nearbyPlayers = allPlayers
                    .Where(p => p != null &&
                               p != caster &&  // 시전자 항상 제외 (객체 비교)
                               Vector3.Distance(p.transform.position, casterPos) <= healRange &&
                               !p.IsDead())
                    .ToList();

                int healedCount = 0;

                foreach (var targetPlayer in nearbyPlayers)
                {
                    try
                    {
                        float maxHealth = targetPlayer.GetMaxHealth();
                        float healAmount = maxHealth * healPercent;

                        targetPlayer.Heal(healAmount, true);

                        // 개별 힐 이펙트 (하드코딩)
                        try
                        {
                            // buff_03a: 힐 대상 발 밑에 부착 → 따라다니며 2초 후 자동 종료
                            SimpleVFX.PlayOnPlayer(targetPlayer, "buff_03a", 2f, new Vector3(0f, 0f, 0f));
                        }
                        catch { }

                        DrawFloatingText(targetPlayer, $"✨ +{healAmount:F0} HP", Color.green);
                        healedCount++;
                    }
                    catch (Exception healEx)
                    {
                        Plugin.Log.LogError($"[지팡이 힐] {targetPlayer?.GetPlayerName() ?? "Unknown"} 힐링 실패: {healEx.Message}");
                    }
                }

                // 시전자 결과 알림
                if (healedCount > 0)
                {
                    DrawFloatingText(caster, "💚 " + L.Get("sacred_heal", healedCount), Color.green);
                }
                else
                {
                    DrawFloatingText(caster, "💚 " + L.Get("no_heal_target"), Color.yellow);
                }

                Plugin.Log.LogInfo($"[지팡이 힐] 총 {healedCount}명 치료 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 힐] 실행 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 힐 이펙트 재생
        /// </summary>
        private static void PlayHealEffect(Vector3 position)
        {
            try
            {
                var healEffect = ZNetScene.instance?.GetPrefab("vfx_HealthUpgrade");
                if (healEffect != null)
                {
                    var effectObj = UnityEngine.Object.Instantiate(healEffect, position + Vector3.up * 1f, Quaternion.identity);
                    var znetView = effectObj?.GetComponent<ZNetView>();
                    if (znetView != null) UnityEngine.Object.DestroyImmediate(znetView);
                    if (effectObj != null) UnityEngine.Object.Destroy(effectObj, 3f);
                }

                var healSound = ZNetScene.instance?.GetPrefab("sfx_dverger_heal_start");
                if (healSound != null)
                {
                    var soundObj = UnityEngine.Object.Instantiate(healSound, position, Quaternion.identity);
                    var znetView2 = soundObj?.GetComponent<ZNetView>();
                    if (znetView2 != null) UnityEngine.Object.DestroyImmediate(znetView2);
                    if (soundObj != null) UnityEngine.Object.Destroy(soundObj, 3f);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 이펙트] 재생 오류: {ex.Message}");
            }
        }
    }
}

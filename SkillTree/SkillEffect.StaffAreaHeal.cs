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

                // 레벨별 쿨타임 (Lv1=30초, 레벨당 -2초)
                int healLvForCd = SkillTreeManager.Instance?.GetSkillLevel("staff_Step6_heal") ?? 1;
                float activeCooldown = GetStaffHealCooldownForLevel(healLvForCd);

                // 쿨타임 적용
                staffHealCooldowns[player] = Time.time + activeCooldown;
                ActiveSkillCooldownRegistry.SetCooldownForSkill("H", "staff_Step6_heal", activeCooldown);

                // 범위 힐 실행
                ExecuteAreaHeal(player);

                Plugin.Log.LogInfo($"[지팡이 범위 힐] 발동 Lv{healLvForCd} - 범위: {Staff_Config.StaffHealRangeValue}m, 치료량: {GetStaffHealPercentForLevel(healLvForCd)}%");
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
                int healLevel = SkillTreeManager.Instance?.GetSkillLevel("staff_Step6_heal") ?? 1;
                float healPercent = GetStaffHealPercentForLevel(healLevel) / 100f;

                // VFX 재생 (하드코딩)
                try
                {
                    // buff_03a_aura: 시전자 캐릭터에 부착 → 따라다니며 3초 후 자동 종료
                    SimpleVFX.PlayOnPlayer(caster, "buff_03a_aura", 3f);
                    // 사운드 (발헤임 기본 SFX → VFXManager)
                    CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("sfx_dverger_heal_finish", "", casterPos, Quaternion.identity, 2f);
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

                        // 개별 힐 이펙트: buff_03a_aura 왼쪽 어깨 / 머리 / 오른쪽 어깨
                        try
                        {
                            SimpleVFX.PlayOnPlayer(targetPlayer, "buff_03a_aura", 2.5f, new Vector3(-0.3f, 1.5f, 0f));
                            SimpleVFX.PlayOnPlayer(targetPlayer, "buff_03a_aura", 2.5f, new Vector3(0f, 1.8f, 0f));
                            SimpleVFX.PlayOnPlayer(targetPlayer, "buff_03a_aura", 2.5f, new Vector3(0.3f, 1.5f, 0f));
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

        internal static float GetStaffHealPercentForLevel(int level)
        {
            float lv1 = Staff_Config.StaffHealPercentageValue;
            return level switch
            {
                2 => lv1 + 2f,
                3 => lv1 + 4f,
                4 => lv1 + 7f,
                5 => lv1 + 10f,
                6 => lv1 + 13f,
                7 => lv1 + 17f,
                _ => lv1
            };
        }

        // 레벨별 쿨타임 (Lv1=30초, 레벨당 -2초)
        internal static float GetStaffHealCooldownForLevel(int level)
        {
            float baseCooldown = Staff_Config.StaffHealCooldownValue;
            float reduction = (level - 1) * 2f;
            return Mathf.Max(baseCooldown - reduction, 1f);
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

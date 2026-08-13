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
                               !p.IsDead() &&
                               IsHealAllowed(caster, p))
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
                            var leftVfx = SimpleVFX.PlayOnPlayer(targetPlayer, "buff_03a_aura", 2.5f, new Vector3(-0.3f, 1.5f, 0f));
                            if (leftVfx != null)
                            {
                                var headVfx = UnityEngine.Object.Instantiate(leftVfx, targetPlayer.transform);
                                headVfx.transform.localPosition = new Vector3(0f, 1.8f, 0f);
                                UnityEngine.Object.Destroy(headVfx, 2.5f);

                                var rightVfx = UnityEngine.Object.Instantiate(leftVfx, targetPlayer.transform);
                                rightVfx.transform.localPosition = new Vector3(0.3f, 1.5f, 0f);
                                UnityEngine.Object.Destroy(rightVfx, 2.5f);
                            }
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

                // 번개 속성 범위 데미지
                ExecuteLightningDamage(caster, casterPos, healRange, healLevel);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 힐] 실행 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 힐 범위 내 적(몬스터/PvP 플레이어)에게 번개 속성 데미지
        /// </summary>
        private static void ExecuteLightningDamage(Player caster, Vector3 pos, float range, int level)
        {
            try
            {
                float pct = GetLightningDamagePercent(level) / 100f;

                var weapon = caster.GetCurrentWeapon();
                var baseDmgTypes = weapon != null ? weapon.GetDamage() : new HitData.DamageTypes();
                float elemBase = baseDmgTypes.m_fire + baseDmgTypes.m_lightning +
                                 baseDmgTypes.m_frost + baseDmgTypes.m_poison + baseDmgTypes.m_spirit;
                if (elemBase <= 0f) elemBase = baseDmgTypes.GetTotalDamage();
                float finalDmg = elemBase * pct;

                if (finalDmg <= 0f) return;

                // 몬스터 타겟
                var targets = Character.GetAllCharacters()
                    .Where(c => c != null && !c.IsDead() && !c.IsTamed() &&
                           (c.IsMonsterFaction(0f) || c.m_faction == Character.Faction.Boss) &&
                           Vector3.Distance(c.transform.position, pos) <= range)
                    .ToList();

                // PvP 플레이어 타겟 (시전자·타겟 모두 PvP 활성화 시)
                if (caster.IsPVPEnabled())
                {
                    targets.AddRange(Player.GetAllPlayers()
                        .Where(p => p != null && p != caster && p.IsPVPEnabled() && !p.IsDead() &&
                               Vector3.Distance(p.transform.position, pos) <= range)
                        .Cast<Character>());
                }

                int hitCount = 0;
                foreach (var enemy in targets)
                {
                    try
                    {
                        var hit = new HitData();
                        hit.m_damage.m_lightning = finalDmg;
                        hit.m_pushForce = 0f;
                        hit.m_dir = (enemy.transform.position - pos).normalized;
                        hit.m_point = enemy.GetCenterPoint();
                        hit.SetAttacker(caster);
                        enemy.Damage(hit);

                        // 3m 넉백 (magnitude 20f → ~3m, Traverse로 private 필드 접근)
                        Vector3 knockDir = (enemy.transform.position - pos);
                        knockDir.y = 0.3f;
                        knockDir.Normalize();
                        const float knockMagnitude = 20f;
                        var traverseEnemy = HarmonyLib.Traverse.Create(enemy);
                        if (traverseEnemy.Field("m_pushForce").FieldExists())
                        {
                            Vector3 cur = (Vector3)traverseEnemy.Field("m_pushForce").GetValue();
                            if (cur.magnitude < knockMagnitude)
                                traverseEnemy.Field("m_pushForce").SetValue(knockDir * knockMagnitude);
                        }

                        // 적중 VFX
                        try
                        {
                            CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer(
                                "fx_chainlightning_hit", "", enemy.GetCenterPoint(), Quaternion.identity, 1.5f);
                        }
                        catch { }

                        hitCount++;
                    }
                    catch (Exception hitEx)
                    {
                        Plugin.Log.LogError($"[번개 힐] 타겟 {enemy?.name ?? "Unknown"} 데미지 실패: {hitEx.Message}");
                    }
                }

                Plugin.Log.LogInfo($"[번개 힐] Lv{level} {(int)(pct * 100)}% → {finalDmg:F1} 번개 데미지, {hitCount}마리 적중");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[번개 힐] 실행 오류: {ex.Message}");
            }
        }

        internal static float GetLightningDamagePercent(int level)
        {
            float basePct = Staff_Config.StaffHealLightningDamageValue;
            return basePct + (level - 1) * 20f;
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

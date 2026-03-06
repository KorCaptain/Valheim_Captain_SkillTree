using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 지팡이 연속 발사 액티브 스킬 시스템
    /// R키로 버프 활성화 후 30초 내 마법 공격 시 0.25초 간격으로 7발 추가 발사
    /// </summary>
    public static partial class SkillEffect
    {
        // === 연속 발사 쿨타임 관리 ===
        private static Dictionary<Player, float> staffDualExplosionCooldowns = new Dictionary<Player, float>();

        // === 연속 발사 버프 상태 관리 ===
        private static Dictionary<Player, bool> staffDualCastReady = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> staffDualCastExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> staffDualCastBuffCoroutines = new Dictionary<Player, Coroutine>();

        // 버프 효과 인스턴스 관리
        private static Dictionary<Player, GameObject> staffDualCastBuffEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, GameObject> staffDualCastStatusEffects = new Dictionary<Player, GameObject>();

        // 버프 VFX 인스턴스 관리 (statusailment_01_aura)
        private static Dictionary<Player, GameObject> staffDualCastBuffVFXInstances = new Dictionary<Player, GameObject>();

        // 프리팹 캐시
        private static GameObject cachedStaffDualCastBuffPrefab = null;
        private static GameObject cachedStaffDualCastStatusPrefab = null;

        /// <summary>
        /// 이중 시전 버프 활성화 (R키)
        /// </summary>
        public static void ActivateStaffDualCast(Player player)
        {
            try
            {
                // 쿨타임 확인
                if (staffDualExplosionCooldowns.ContainsKey(player) && Time.time < staffDualExplosionCooldowns[player])
                {
                    float remaining = staffDualExplosionCooldowns[player] - Time.time;
                    DrawFloatingText(player, L.Get("staff_dual_cast_cooldown", Mathf.CeilToInt(remaining)), Color.red);
                    return;
                }

                // 에이트르 확인
                float eitrCost = Staff_Config.StaffDoubleCastEitrCostValue;
                if (player.GetEitr() < eitrCost)
                {
                    DrawFloatingText(player, L.Get("staff_eitr_insufficient", eitrCost), Color.red);
                    return;
                }

                // 에이트르 소모
                player.UseEitr(eitrCost);

                // 쿨타임 적용
                staffDualExplosionCooldowns[player] = Time.time + Staff_Config.StaffDoubleCastCooldownValue;
                ActiveSkillCooldownRegistry.SetCooldown("R", Staff_Config.StaffDoubleCastCooldownValue);

                // 버프 활성화 (30초간 지속)
                float buffDuration = 30f;
                staffDualCastReady[player] = true;
                staffDualCastExpiry[player] = Time.time + buffDuration;

                // VFX/SFX 효과
                PlayStaffDualCastBuffActivationEffects(player);

                // 기존 코루틴 중단
                if (staffDualCastBuffCoroutines.ContainsKey(player))
                {
                    SkillTreeInputListener.Instance.StopCoroutine(staffDualCastBuffCoroutines[player]);
                    staffDualCastBuffCoroutines.Remove(player);
                }

                // 타이머 코루틴 시작
                var coroutine = SkillTreeInputListener.Instance.StartCoroutine(StaffDualCastBuffTimer(player, buffDuration));
                staffDualCastBuffCoroutines[player] = coroutine;

                DrawFloatingText(player, "✨ " + L.Get("staff_dual_cast_ready", 30), new Color(0.8f, 0.3f, 1f, 1f));
                Plugin.Log.LogInfo($"[연속 발사] R키로 버프 활성화 - 지속시간: {buffDuration}초, 에이트르 소모: {eitrCost}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 버프 활성화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 버프 활성화 효과
        /// </summary>
        public static void PlayStaffDualCastBuffActivationEffects(Player player)
        {
            try
            {
                PlayStaffDualCastBuffEffect(player);
                PlayStaffDualCastStatusEffect(player);
                PlayStaffDualCastActivationSound(player);
                CreateStaffDualCastBuffVFX(player);  // 버프 VFX 추가
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 버프 활성화 효과 재생 실패: {ex.Message}");
            }
        }

        private static void PlayStaffDualCastBuffEffect(Player player)
        {
            try
            {
                if (cachedStaffDualCastBuffPrefab == null)
                {
                    cachedStaffDualCastBuffPrefab = VFXManager.GetVFXPrefab("buff_02a");
                    if (cachedStaffDualCastBuffPrefab == null) return;
                }

                // 기존 효과 제거
                if (staffDualCastBuffEffects.ContainsKey(player) && staffDualCastBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastBuffEffects[player]);
                    staffDualCastBuffEffects.Remove(player);
                }

                // 효과 생성
                var footPosition = player.transform.position + Vector3.down * 0.1f;
                var effectInstance = UnityEngine.Object.Instantiate(cachedStaffDualCastBuffPrefab, footPosition, Quaternion.identity);
                effectInstance.transform.SetParent(player.transform, false);
                effectInstance.transform.localPosition = Vector3.down * 0.1f;
                effectInstance.transform.localScale = Vector3.one * 0.4f;

                SetStaffDualCastBuffTransparency(effectInstance, 0.2f);
                staffDualCastBuffEffects[player] = effectInstance;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 버프 효과 재생 실패: {ex.Message}");
            }
        }

        private static void PlayStaffDualCastStatusEffect(Player player)
        {
            try
            {
                if (cachedStaffDualCastStatusPrefab == null)
                {
                    cachedStaffDualCastStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedStaffDualCastStatusPrefab == null) return;
                }

                // 기존 효과 제거
                if (staffDualCastStatusEffects.ContainsKey(player) && staffDualCastStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastStatusEffects[player]);
                    staffDualCastStatusEffects.Remove(player);
                }

                // 머리 위 효과 생성
                var headPosition = player.transform.position + Vector3.up * 2.0f;
                var statusInstance = UnityEngine.Object.Instantiate(cachedStaffDualCastStatusPrefab, headPosition, Quaternion.identity);
                statusInstance.transform.SetParent(player.transform, false);
                statusInstance.transform.localPosition = Vector3.up * 2.0f;
                statusInstance.transform.localScale = Vector3.one * 0.7f;

                staffDualCastStatusEffects[player] = statusInstance;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 상태 효과 재생 실패: {ex.Message}");
            }
        }

        private static void PlayStaffDualCastActivationSound(Player player)
        {
            try
            {
                var znet = ZNetScene.instance;
                if (znet != null)
                {
                    var soundEffect = znet.GetPrefab("sfx_StaffLightning_charge");
                    if (soundEffect != null)
                    {
                        UnityEngine.Object.Instantiate(soundEffect, player.transform.position, Quaternion.identity);
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 활성화 사운드 재생 오류: {ex.Message}");
            }
        }

        private static void SetStaffDualCastBuffTransparency(GameObject buffEffect, float alpha)
        {
            if (buffEffect == null) return;

            var renderers = buffEffect.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                if (renderer?.material != null && renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.color;
                    color.a = alpha;
                    renderer.material.color = color;
                }
            }

            var particleSystems = buffEffect.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in particleSystems)
            {
                if (ps != null)
                {
                    var main = ps.main;
                    Color startColor = main.startColor.color;
                    startColor.a = alpha;
                    main.startColor = startColor;
                }
            }
        }

        private static IEnumerator StaffDualCastBuffTimer(Player player, float buffDuration)
        {
            float startTime = Time.time;
            float nextRemindTime = startTime + 10f;

            while (staffDualCastReady.ContainsKey(player) && staffDualCastReady[player] &&
                   Time.time < staffDualCastExpiry[player])
            {
                if (player == null || player.IsDead())
                {
                    ClearStaffDualCastBuff(player);
                    yield break;
                }

                if (Time.time >= nextRemindTime)
                {
                    float remainingTime = staffDualCastExpiry[player] - Time.time;
                    if (remainingTime > 0)
                    {
                        DrawFloatingText(player, "✨ " + L.Get("staff_dual_cast_remaining", $"{remainingTime:F0}"), new Color(0.8f, 0.3f, 1f, 1f));
                        nextRemindTime = Time.time + 10f;
                    }
                }

                yield return new WaitForSeconds(1f);
            }

            if (staffDualCastReady.ContainsKey(player) && staffDualCastReady[player])
            {
                DrawFloatingText(player, L.Get("staff_dual_cast_expired"), Color.yellow);
                ClearStaffDualCastBuff(player);
            }
        }

        /// <summary>
        /// 이중 시전 버프 상태 확인
        /// </summary>
        public static bool IsStaffDualCastReady(Player player)
        {
            return staffDualCastReady.ContainsKey(player) &&
                   staffDualCastReady[player] &&
                   Time.time < staffDualCastExpiry[player];
        }

        /// <summary>
        /// 이중 시전 버프 정리
        /// </summary>
        public static void ClearStaffDualCastBuff(Player player)
        {
            try
            {
                if (player == null) return;

                staffDualCastReady.Remove(player);
                staffDualCastExpiry.Remove(player);

                if (staffDualCastBuffEffects.ContainsKey(player) && staffDualCastBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastBuffEffects[player]);
                    staffDualCastBuffEffects.Remove(player);
                }

                if (staffDualCastStatusEffects.ContainsKey(player) && staffDualCastStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastStatusEffects[player]);
                    staffDualCastStatusEffects.Remove(player);
                }

                // 버프 VFX 제거
                RemoveStaffDualCastBuffVFX(player);

                staffDualCastBuffCoroutines.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 버프 정리 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 연속 발사 실제 실행 (0.25초 간격으로 발사체 연속 발사)
        /// </summary>
        public static void PerformStaffDualCastAttack(Player player, ItemDrop.ItemData weapon, Vector3 baseDirection)
        {
            try
            {
                if (!IsStaffDualCastReady(player)) return;

                int shotCount = Staff_Config.StaffDoubleCastProjectileCountValue;
                float damagePercent = Staff_Config.StaffDoubleCastDamagePercentValue;

                ClearStaffDualCastBuff(player);

                SkillTreeInputListener.Instance.StartCoroutine(
                    StaffRapidFireCoroutine(player, weapon, baseDirection, shotCount, damagePercent));
                DrawFloatingText(player, "✨ " + L.Get("staff_dual_cast_activated", shotCount.ToString()), new Color(0.8f, 0.3f, 1f));
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 실행 오류: {ex.Message}");
            }
        }

        private static IEnumerator StaffRapidFireCoroutine(Player player, ItemDrop.ItemData weapon,
            Vector3 fireDirection, int shotCount, float damagePercent)
        {
            for (int i = 0; i < shotCount; i++)
            {
                if (player == null || player.IsDead() || player.GetCurrentWeapon() != weapon)
                    yield break;

                if (i > 0)
                    yield return new WaitForSeconds(0.25f);

                FireSingleStaffProjectile(player, weapon, fireDirection, damagePercent);
            }
        }

        private static void FireSingleStaffProjectile(Player player, ItemDrop.ItemData weapon,
            Vector3 fireDirection, float damagePercent)
        {
            try
            {
                var weaponAttack = weapon.m_shared.m_attack;
                if (weaponAttack.m_attackProjectile == null) return;

                var spawnPoint = player.transform.position + player.transform.forward * 0.5f + Vector3.up * 1.4f;

                var projectileObj = UnityEngine.Object.Instantiate(
                    weaponAttack.m_attackProjectile,
                    spawnPoint,
                    Quaternion.LookRotation(fireDirection));

                var projectile = projectileObj.GetComponent<Projectile>();
                if (projectile == null) return;

                var hitData = new HitData();
                hitData.m_damage = weapon.GetDamage();
                hitData.m_damage.Modify(damagePercent / 100f);
                hitData.m_point = spawnPoint;
                hitData.m_dir = fireDirection;
                hitData.m_attacker = player.GetZDOID();
                hitData.m_skill = Skills.SkillType.ElementalMagic;
                hitData.SetAttacker(player);

                float vel = weaponAttack.m_projectileVel > 0 ? weaponAttack.m_projectileVel : 60f;
                projectile.Setup(player, fireDirection * vel, weaponAttack.m_projectileAccuracy, hitData, null, weapon);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 발사체 생성 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어 사망 시 정리
        /// </summary>
        public static void CleanupStaffDualCastOnDeath(Player player)
        {
            try
            {
                if (staffDualCastBuffCoroutines.ContainsKey(player))
                {
                    SkillTreeInputListener.Instance.StopCoroutine(staffDualCastBuffCoroutines[player]);
                    staffDualCastBuffCoroutines.Remove(player);
                }
                ClearStaffDualCastBuff(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[연속 발사] 정리 실패: {ex.Message}");
            }
        }

        #region === 버프 VFX 시스템 ===

        /// <summary>
        /// 이중시전 버프 VFX 생성 (머리 위 1.2m)
        /// </summary>
        private static void CreateStaffDualCastBuffVFX(Player player)
        {
            try
            {
                // 기존 VFX 제거
                RemoveStaffDualCastBuffVFX(player);

                // 새 VFX 생성 (머리 위 1.2m, 무한 지속)
                var vfx = SimpleVFX.PlayOnPlayer(player, "statusailment_01_aura", 9999f, new Vector3(0f, 1.2f, 0f));
                if (vfx != null)
                {
                    staffDualCastBuffVFXInstances[player] = vfx;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[연속 발사 버프 VFX] 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중시전 버프 VFX 제거
        /// </summary>
        private static void RemoveStaffDualCastBuffVFX(Player player)
        {
            try
            {
                if (staffDualCastBuffVFXInstances.TryGetValue(player, out var vfx) && vfx != null)
                {
                    UnityEngine.Object.Destroy(vfx);
                }
                staffDualCastBuffVFXInstances.Remove(player);
            }
            catch (Exception)
            {
                // 조용히 무시
            }
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 석궁 "단 한 발" 액티브 스킬 시스템
    /// R키로 버프 활성화 후 30초 내 공격 시 강화된 데미지와 넉백 적용
    /// </summary>
    public static partial class SkillEffect
    {
        // === 석궁 단 한 발 상태 변수 ===
        private static Dictionary<Player, float> crossbowOneShotCooldown = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> crossbowOneShotReady = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> crossbowOneShotExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> crossbowOneShotCoroutine = new Dictionary<Player, Coroutine>();
        // 장전 중 사운드 코루틴 추적
        private static Dictionary<Player, Coroutine> _oneShotReloadSoundCoroutines = new Dictionary<Player, Coroutine>();

        // Archer Lv2: 추가 사용 차지 관리
        private static Dictionary<Player, bool> _archerLv2OneShotChargeReady = new Dictionary<Player, bool>();

        // === 버프 이펙트 변수 ===
        private static Dictionary<Player, GameObject> followingBuffEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, Coroutine> followingBuffCoroutines = new Dictionary<Player, Coroutine>();

        // === 단 한 발 슬로우 리로드 패널티 ===
        private static Dictionary<Player, bool> _oneShotSlowReloadPending = new Dictionary<Player, bool>();

        public static bool HasOneShotSlowReloadPending(Player p) =>
            _oneShotSlowReloadPending.TryGetValue(p, out bool v) && v;

        public static void ConsumeOneShotSlowReloadPending(Player p) =>
            _oneShotSlowReloadPending[p] = false;

        /// <summary>
        /// 석궁 단 한 발 버프 활성화
        /// </summary>
        public static void ActivateCrossbowOneShot(Player player)
        {
            if (!crossbowOneShotCooldown.ContainsKey(player))
                crossbowOneShotCooldown[player] = 0f;

            bool hasArcherLv2 = SkillTreeManager.Instance != null
                && SkillTreeManager.Instance.GetSkillLevel("Archer") >= 2;
            bool hasExtraCharge = hasArcherLv2
                && _archerLv2OneShotChargeReady.TryGetValue(player, out bool chargeReady) && chargeReady;

            float cooldownTime = Crossbow_Config.CrossbowOneShotCooldownValue;
            float durationTime = Crossbow_Config.CrossbowOneShotDurationValue;

            // Archer Lv2 추가 차지 있으면 쿨타임 체크 건너뜀
            if (!hasExtraCharge)
            {
                if (Time.time - crossbowOneShotCooldown[player] < cooldownTime)
                {
                    float remainingCooldown = cooldownTime - (Time.time - crossbowOneShotCooldown[player]);
                    DrawFloatingText(player, L.Get("crossbow_oneshot_cooldown", $"{remainingCooldown:F1}"));
                    return;
                }
            }

            if (!WeaponHelper.IsUsingCrossbow(player))
            {
                DrawFloatingText(player, L.Get("crossbow_equip_required"));
                return;
            }

            crossbowOneShotReady[player] = true;
            crossbowOneShotExpiry[player] = Time.time + durationTime;
            _oneShotSlowReloadPending[player] = true;

            // 장전 상태 강제 해제 → 다시 장전 모드로 전환
            try
            {
                HarmonyLib.Traverse.Create(player).Field("m_weaponLoaded").SetValue(null);
                var nview = HarmonyLib.Traverse.Create(player).Field("m_nview").GetValue<ZNetView>();
                nview?.GetZDO()?.Set("WeaponLoaded".GetStableHashCode(), false);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[단 한 발] 장전 해제 실패: {ex.Message}");
            }

            // 패시브 장전 보너스 연동
            // 고속 장전(crossbow_Step3_rapid): 항상 적용 (장전 패치에서 자동 처리)
            // 자동 장전(crossbow_Step4_re): 확률 재굴림 → 버프 부여 시 장전 패치에서 소비
            if (HasSkill("crossbow_Step4_re"))
            {
                float autoChance = Crossbow_Config.CrossbowAutoReloadChanceValue / 100f;
                if (UnityEngine.Random.value < autoChance)
                {
                    SetAutoReloadBuff(player, true);
                    DrawFloatingText(player, L.Get("crossbow_auto_reload_activated", $"{200}"));
                }
            }

            if (hasExtraCharge)
            {
                // 2번째 사용: 차지 소비 → 쿨타임 시작
                _archerLv2OneShotChargeReady[player] = false;
                crossbowOneShotCooldown[player] = Time.time;
                ActiveSkillCooldownRegistry.SetCooldown("R", cooldownTime);
                Plugin.Log.LogDebug("[단 한 발] Archer Lv2 2번째 사용 → 쿨타임 시작");
            }
            else if (hasArcherLv2)
            {
                // 1번째 사용: 차지 부여, 쿨타임 없음
                _archerLv2OneShotChargeReady[player] = true;
                Plugin.Log.LogDebug("[단 한 발] Archer Lv2 1번째 사용 → 추가 사용 준비");
            }
            else
            {
                crossbowOneShotCooldown[player] = Time.time;
                ActiveSkillCooldownRegistry.SetCooldown("R", cooldownTime);
            }

            if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
            {
                player.StopCoroutine(crossbowOneShotCoroutine[player]);
            }
            crossbowOneShotCoroutine[player] = player.StartCoroutine(CrossbowOneShotBuffDisplay(player));

            try
            {
                StartFollowingBuffEffect(player);

                // 장전 중 소리 코루틴 시작
                if (_oneShotReloadSoundCoroutines.ContainsKey(player) && _oneShotReloadSoundCoroutines[player] != null)
                    player.StopCoroutine(_oneShotReloadSoundCoroutines[player]);
                _oneShotReloadSoundCoroutines[player] = player.StartCoroutine(OneShotReloadSoundCoroutine(player));
            }
            catch (Exception buffEffectEx)
            {
                Plugin.Log.LogError($"[단 한 발 버프] VFX 재생 실패: {buffEffectEx.Message}");
            }

            DrawFloatingText(player, L.Get("crossbow_oneshot_ready"));
        }

        private static void StartFollowingBuffEffect(Player player)
        {
            try
            {
                StopFollowingBuffEffect(player);

                if (followingBuffCoroutines.ContainsKey(player) && followingBuffCoroutines[player] != null)
                {
                    player.StopCoroutine(followingBuffCoroutines[player]);
                }

                followingBuffCoroutines[player] = player.StartCoroutine(FollowingBuffEffectCoroutine(player));
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[석궁 단 한 발] 이펙트 시작 실패: {ex.Message}");
            }
        }

        private static void StopFollowingBuffEffect(Player player)
        {
            try
            {
                if (followingBuffCoroutines.ContainsKey(player) && followingBuffCoroutines[player] != null)
                {
                    player.StopCoroutine(followingBuffCoroutines[player]);
                    followingBuffCoroutines[player] = null;
                }

                if (followingBuffEffects.ContainsKey(player) && followingBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(followingBuffEffects[player]);
                    followingBuffEffects[player] = null;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[석궁 단 한 발] 이펙트 정리 오류: {ex.Message}");
            }
        }

        private static IEnumerator FollowingBuffEffectCoroutine(Player player)
        {
            GameObject buff01Effect = null;
            GameObject starAuraEffect = null;

            try
            {
                // buff_02a 이펙트 (2초 짧게 재생)
                var buff02aPrefab = VFXManager.GetVFXPrefab("buff_02a");
                if (buff02aPrefab != null)
                {
                    buff01Effect = UnityEngine.Object.Instantiate(buff02aPrefab, player.transform.position + Vector3.up * 0.5f, player.transform.rotation);
                    buff01Effect.transform.SetParent(player.transform, false);
                    buff01Effect.transform.localPosition = Vector3.up * 0.5f;
                    buff01Effect.transform.localScale = Vector3.one * 0.8f;
                    player.StartCoroutine(DestroyEffectAfterDelay(buff01Effect, 2f, "buff_02a"));
                }

                // statusailment_01_aura 이펙트 (버프 지속 동안 유지)
                var starAuraPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                if (starAuraPrefab != null)
                {
                    starAuraEffect = UnityEngine.Object.Instantiate(starAuraPrefab, player.transform.position + Vector3.up * 2.2f, player.transform.rotation);
                    starAuraEffect.transform.SetParent(player.transform, false);
                    starAuraEffect.transform.localPosition = Vector3.up * 2.2f;
                    starAuraEffect.transform.localScale = Vector3.one * 0.6f;

                    float buffDuration = crossbowOneShotExpiry[player] - Time.time;
                    player.StartCoroutine(DestroyEffectAfterDelay(starAuraEffect, buffDuration, "statusailment_01_aura"));
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[석궁 단 한 발] 이펙트 생성 오류: {ex.Message}");
            }

            while (crossbowOneShotReady.ContainsKey(player) && crossbowOneShotReady[player] && Time.time < crossbowOneShotExpiry[player])
            {
                if (player == null || player.IsDead())
                {
                    if (buff01Effect != null) UnityEngine.Object.Destroy(buff01Effect);
                    if (starAuraEffect != null) UnityEngine.Object.Destroy(starAuraEffect);
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
        }

        /// <summary>
        /// 장전 중 1초 간격으로 소리 반복 재생, 장전 완료 시 자동 종료
        /// </summary>
        private static IEnumerator OneShotReloadSoundCoroutine(Player player)
        {
            // 장전 해제 직후이므로 첫 틱은 바로 소리 재생
            while (player != null && !player.IsDead())
            {
                try
                {
                    CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer(
                        "sfx_oozebomb_explode", "", player.transform.position, Quaternion.identity, 1f);
                }
                catch { }

                yield return new WaitForSeconds(1f);

                // 장전 완료 시 종료
                if (player == null || player.IsWeaponLoaded())
                    break;
            }

            if (_oneShotReloadSoundCoroutines.ContainsKey(player))
                _oneShotReloadSoundCoroutines[player] = null;
        }

        private static IEnumerator DestroyEffectAfterDelay(GameObject effect, float delay, string effectName)
        {
            if (effect == null) yield break;
            yield return new WaitForSeconds(delay);

            if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
            {
                if (effect != null) UnityEngine.Object.Destroy(effect);
                yield break;
            }

            if (effect != null) UnityEngine.Object.Destroy(effect);
        }

        private static IEnumerator CrossbowOneShotBuffDisplay(Player player)
        {
            while (crossbowOneShotReady[player] && Time.time < crossbowOneShotExpiry[player])
            {
                if (player == null || player.IsDead()) break;

                float remainingTime = crossbowOneShotExpiry[player] - Time.time;
                if (remainingTime > 0)
                {
                    yield return new WaitForSeconds(5f);
                    if (player == null || player.IsDead()) break;

                    if (crossbowOneShotReady[player])
                    {
                        DrawFloatingText(player, L.Get("crossbow_oneshot_remaining", $"{remainingTime:F0}"));
                    }
                }
                else break;
            }

            StopFollowingBuffEffect(player);

            if (crossbowOneShotReady[player])
            {
                crossbowOneShotReady[player] = false;
                DrawFloatingText(player, L.Get("crossbow_oneshot_expired"));
            }
        }

        private static IEnumerator ShowCooldownDisplay(Player player, float cooldownTime, string skillName)
        {
            float elapsed = 0f;
            while (elapsed < cooldownTime)
            {
                if (player == null || player.IsDead()) yield break;
                yield return new WaitForSeconds(2f);
                if (player == null || player.IsDead()) yield break;

                elapsed += 2f;
                float remaining = cooldownTime - elapsed;
                if (remaining > 0)
                {
                    DrawFloatingText(player, L.Get("skill_cooldown_format", skillName, $"{remaining:F1}"));
                }
            }
        }

        /// <summary>
        /// 석궁 단 한 발 스킬 사용 확인 및 효과 적용
        /// </summary>
        public static bool CheckAndConsumeCrossbowOneShot(Player player, Character target)
        {
            try
            {
                if (!crossbowOneShotReady.ContainsKey(player) || !crossbowOneShotReady[player])
                    return false;

                if (target == player)
                    return false;

                bool isValidTarget = target.IsMonsterFaction(Time.time) ||
                                   (target.IsPlayer() && target != player) ||
                                   target.name.Contains("Deer") || target.name.Contains("Boar") ||
                                   target.name.Contains("Neck") || target.name.Contains("Greyling");

                if (isValidTarget)
                {
                    // 효과 제거
                    crossbowOneShotReady[player] = false;

                    if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
                    {
                        player.StopCoroutine(crossbowOneShotCoroutine[player]);
                        crossbowOneShotCoroutine[player] = null;
                    }

                    StopFollowingBuffEffect(player);

                    // 직격 대상 VFX
                    try
                    {
                        CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("lightningAOE", "", target.transform.position, Quaternion.identity, 2f);
                    }
                    catch { }

                    // 직격 대상 중심 AOE 넉백 (m_pushForce 직접 설정 방식 - knockback_implementation.md 참조)
                    try
                    {
                        float aoeRadius = Crossbow_Config.CrossbowOneShotAoeRadiusValue;
                        // 7m 거리 = magnitude ~45f (md: 45f → 0.45s × 20m/s ≈ 7m)
                        float pushMagnitude = Crossbow_Config.CrossbowOneShotKnockbackValue;
                        Vector3 aoeCenter = target.transform.position;
                        var hitColliders = Physics.OverlapSphere(aoeCenter, aoeRadius);
                        var processedEnemies = new System.Collections.Generic.HashSet<Character>();

                        foreach (var col in hitColliders)
                        {
                            var enemy = col.GetComponentInParent<Character>();
                            if (enemy == null || processedEnemies.Contains(enemy)) continue;
                            if (enemy == player || enemy.IsDead()) continue;
                            if (!BaseAI.IsEnemy(player, enemy)) continue;

                            processedEnemies.Add(enemy);
                            Vector3 dir = (enemy.transform.position - aoeCenter).normalized;
                            dir.y = 0.3f;
                            dir.Normalize();

                            // character.m_pushForce 직접 설정 (HitData 경유 시 mass 나누기로 약해짐)
                            var tEnemy = HarmonyLib.Traverse.Create(enemy);
                            if (tEnemy.Field("m_pushForce").FieldExists())
                            {
                                var cur = (Vector3)tEnemy.Field("m_pushForce").GetValue();
                                if (cur.magnitude < pushMagnitude)
                                    tEnemy.Field("m_pushForce").SetValue(dir * pushMagnitude);
                            }
                            enemy.Stagger(dir);

                            try
                            {
                                CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("fx_Lightning", "", enemy.transform.position, Quaternion.identity, 2f);
                            }
                            catch { }
                        }
                    }
                    catch (Exception aoeEx)
                    {
                        Plugin.Log.LogWarning($"[석궁 단 한 발] AOE 넉백 오류: {aoeEx.Message}");
                    }

                    DrawFloatingText(player, L.Get("crossbow_oneshot_activated"));
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[석궁 단 한 발] 스킬 소모 처리 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 석궁 단 한 발 정리 (플레이어 사망 시)
        /// </summary>
        public static void CleanupCrossbowOneShotOnDeath(Player player)
        {
            try
            {
                if (crossbowOneShotCoroutine.ContainsKey(player))
                {
                    if (crossbowOneShotCoroutine[player] != null)
                    {
                        try { player.StopCoroutine(crossbowOneShotCoroutine[player]); } catch { }
                    }
                    crossbowOneShotCoroutine.Remove(player);
                }

                crossbowOneShotReady.Remove(player);
                crossbowOneShotCooldown.Remove(player);
                crossbowOneShotExpiry.Remove(player);
                _archerLv2OneShotChargeReady.Remove(player);
                _oneShotSlowReloadPending.Remove(player);

                // 장전 소리 코루틴 정리
                if (_oneShotReloadSoundCoroutines.ContainsKey(player))
                {
                    if (_oneShotReloadSoundCoroutines[player] != null)
                        try { player.StopCoroutine(_oneShotReloadSoundCoroutines[player]); } catch { }
                    _oneShotReloadSoundCoroutines.Remove(player);
                }

                // 버프 이펙트 정리
                if (followingBuffCoroutines.ContainsKey(player))
                {
                    try { player.StopCoroutine(followingBuffCoroutines[player]); } catch { }
                    followingBuffCoroutines.Remove(player);
                }
                if (followingBuffEffects.ContainsKey(player) && followingBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(followingBuffEffects[player]);
                }
                followingBuffEffects.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[석궁 단 한 발] 정리 실패: {ex.Message}");
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 석궁 "단 한 발" 액티브 스킬 시스템
    /// T키로 버프 활성화 후 30초 내 공격 시 강화된 데미지와 넉백 적용
    /// </summary>
    public static partial class SkillEffect
    {
        // === 석궁 단 한 발 상태 변수 ===
        private static Dictionary<Player, float> crossbowOneShotCooldown = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> crossbowOneShotReady = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> crossbowOneShotExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> crossbowOneShotCoroutine = new Dictionary<Player, Coroutine>();
        private static readonly float crossbowOneShotCooldownTime = 60f;
        private static readonly float crossbowOneShotDuration = 30f;

        // === 버프 이펙트 변수 ===
        private static Dictionary<Player, GameObject> followingBuffEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, Coroutine> followingBuffCoroutines = new Dictionary<Player, Coroutine>();

        /// <summary>
        /// 석궁 단 한 발 버프 활성화
        /// </summary>
        public static void ActivateCrossbowOneShot(Player player)
        {
            if (!crossbowOneShotCooldown.ContainsKey(player))
                crossbowOneShotCooldown[player] = 0f;

            if (Time.time - crossbowOneShotCooldown[player] < crossbowOneShotCooldownTime)
            {
                float remainingCooldown = crossbowOneShotCooldownTime - (Time.time - crossbowOneShotCooldown[player]);
                DrawFloatingText(player, $"단 한 발 쿨타임: {remainingCooldown:F1}초");
                if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
                {
                    player.StopCoroutine(crossbowOneShotCoroutine[player]);
                }
                crossbowOneShotCoroutine[player] = player.StartCoroutine(ShowCooldownDisplay(player, remainingCooldown, "단 한 발"));
                return;
            }

            if (!IsUsingCrossbow(player))
            {
                DrawFloatingText(player, "석궁을 착용해야 합니다!");
                return;
            }

            crossbowOneShotCooldown[player] = Time.time;
            crossbowOneShotReady[player] = true;
            crossbowOneShotExpiry[player] = Time.time + crossbowOneShotDuration;

            if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
            {
                player.StopCoroutine(crossbowOneShotCoroutine[player]);
            }
            crossbowOneShotCoroutine[player] = player.StartCoroutine(CrossbowOneShotBuffDisplay(player));

            try
            {
                StartFollowingBuffEffect(player);

                var buffSoundNames = new string[] { "sfx_creature_tamed", "sfx_build_hammer_metal" };
                foreach (var soundName in buffSoundNames)
                {
                    var sound = ZNetScene.instance?.GetPrefab(soundName)?.GetComponent<AudioSource>()?.clip;
                    if (sound != null)
                    {
                        AudioSource.PlayClipAtPoint(sound, player.transform.position);
                        break;
                    }
                }
            }
            catch (Exception buffEffectEx)
            {
                Plugin.Log.LogError($"[단 한 발 버프] VFX 재생 실패: {buffEffectEx.Message}");
            }

            DrawFloatingText(player, "🎯 단 한 발 준비 완료! (30초)");
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
                // buff_01 이펙트
                var buff01Prefab = VFXManager.GetVFXPrefab("buff_01");
                if (buff01Prefab != null)
                {
                    buff01Effect = UnityEngine.Object.Instantiate(buff01Prefab, player.transform.position + Vector3.up * 0.5f, player.transform.rotation);
                    buff01Effect.transform.SetParent(player.transform, false);
                    buff01Effect.transform.localPosition = Vector3.up * 0.5f;
                    buff01Effect.transform.localScale = Vector3.one * 0.8f;
                    player.StartCoroutine(DestroyEffectAfterDelay(buff01Effect, 2f, "buff_01"));
                }

                // statusailment_01_aura 이펙트
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
                        DrawFloatingText(player, $"🎯 단 한 발 준비됨 ({remainingTime:F0}초)");
                    }
                }
                else break;
            }

            StopFollowingBuffEffect(player);

            if (crossbowOneShotReady[player])
            {
                crossbowOneShotReady[player] = false;
                DrawFloatingText(player, "단 한 발 버프 만료");
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
                    DrawFloatingText(player, $"{skillName} 쿨타임: {remaining:F1}초");
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
                    // 넉백 효과 적용
                    try
                    {
                        Vector3 knockbackDirection = (target.transform.position - player.transform.position).normalized;
                        float knockbackDistance = Crossbow_Config.CrossbowOneShotKnockbackValue;
                        target.Stagger(knockbackDirection);

                        var rigidbody = target.GetComponent<Rigidbody>();
                        if (rigidbody != null && !rigidbody.isKinematic)
                        {
                            rigidbody.AddForce(knockbackDirection * knockbackDistance * 2f, ForceMode.Impulse);
                        }
                    }
                    catch (Exception knockbackEx)
                    {
                        Plugin.Log.LogWarning($"[석궁 단 한 발] 넉백 효과 적용 실패: {knockbackEx.Message}");
                    }

                    // 효과 제거
                    crossbowOneShotReady[player] = false;

                    if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
                    {
                        player.StopCoroutine(crossbowOneShotCoroutine[player]);
                        crossbowOneShotCoroutine[player] = null;
                    }

                    StopFollowingBuffEffect(player);

                    // VFX 재생
                    try
                    {
                        SimpleVFX.Play("fx_siegebomb_explosion", target.transform.position, 3f);
                    }
                    catch { }

                    DrawFloatingText(player, "🎯 단 한 발 발동!");
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
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[석궁 단 한 발] 정리 실패: {ex.Message}");
            }
        }
    }
}

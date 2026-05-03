using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using CaptainSkillTree;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 마인드쉴드 스킬 — 방어전문가 Tier 6 G키 액티브
    /// 에이트르 최대치만큼 보호막으로 HP 피해 흡수
    /// </summary>
    public static class MindShieldSkill
    {
        // === 상태 관리 ===
        private static Dictionary<Player, bool> _shieldActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> _shieldHp = new Dictionary<Player, float>();
        private static Dictionary<Player, float> _shieldExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, float> _shieldMaxHp = new Dictionary<Player, float>();
        private static Dictionary<Player, GameObject> _shieldVfx = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, Coroutine> _expiryCoroutines = new Dictionary<Player, Coroutine>();

        private static float _lastActivateTime = 0f;

        // === 공개 API ===

        public static float GetShieldHp(Player player)
            => _shieldHp.TryGetValue(player, out float hp) ? hp : 0f;

        public static float GetShieldMaxHp(Player player)
            => _shieldMaxHp.TryGetValue(player, out float max) ? max : 0f;

        public static bool IsActive(Player player)
        {
            if (player == null) return false;
            return _shieldActive.TryGetValue(player, out bool active) && active;
        }

        public static void Activate(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 스킬 보유 확인
            if ((SkillTreeManager.Instance?.GetSkillLevel("defense_Step6_mind") ?? 0) <= 0) return;

            // 쿨타임 체크
            float cooldown = Defense_Config.MindShieldCooldownValue;
            if (Time.time - _lastActivateTime < cooldown)
            {
                float remaining = cooldown - (Time.time - _lastActivateTime);
                int min = Mathf.FloorToInt(remaining / 60f);
                int sec = Mathf.FloorToInt(remaining % 60f);
                string msg = min > 0
                    ? L.Get("mind_shield_cooldown") + $" ({min}분 {sec}초)"
                    : L.Get("mind_shield_cooldown") + $" ({sec}초)";
                SkillEffect.DrawFloatingText(player, msg, Color.yellow);
                return;
            }

            // 에이트르 체크
            float eitrCost = Defense_Config.MindShieldEitrCostValue;
            if (player.GetEitr() < eitrCost)
            {
                SkillEffect.DrawFloatingText(player, L.Get("mind_shield_eitr_insufficient"), Color.red);
                return;
            }

            // 이미 활성화된 보호막 제거
            if (IsActive(player))
                ForceRemoveShield(player);

            // 에이트르 소모
            player.UseEitr(eitrCost);

            // 보호막 설정
            float maxEitr = player.GetMaxEitr();
            _shieldMaxHp[player] = maxEitr;
            _shieldHp[player] = maxEitr;
            _shieldExpiry[player] = Time.time + Defense_Config.MindShieldDurationValue;
            _shieldActive[player] = true;
            _lastActivateTime = Time.time;

            // HUD 쿨타임 등록
            ActiveSkillCooldownRegistry.SetCooldownForSkill("G", "defense_Step6_mind", cooldown);

            // VFX
            PlayActivateVFX(player);

            // 만료 코루틴
            StartExpiryCoroutine(player);

            SkillEffect.DrawFloatingText(player, L.Get("mind_shield_activate"), Color.cyan);
        }

        // === 보호막 처리 ===

        public static void BreakShield(Player player)
        {
            if (player == null) return;
            _shieldActive[player] = false;
            _shieldHp[player] = 0f;

            StopExpiryCoroutine(player);
            RemoveShieldVFX(player);

            try
            {
                VFXManager.PlayVFXMultiplayer("fx_GoblinShieldBreak", "",
                    player.transform.position, Quaternion.identity, 3f);
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[마인드쉴드] BreakShield VFX 실패: {ex.Message}");
            }

        }

        private static void ExpireShield(Player player)
        {
            if (player == null) return;
            _shieldActive[player] = false;
            _shieldHp[player] = 0f;
            RemoveShieldVFX(player);
        }

        private static void ForceRemoveShield(Player player)
        {
            _shieldActive[player] = false;
            _shieldHp[player] = 0f;
            StopExpiryCoroutine(player);
            RemoveShieldVFX(player);
        }

        private static void RemoveShieldVFX(Player player)
        {
            if (_shieldVfx.TryGetValue(player, out var go) && go != null)
            {
                try { UnityEngine.Object.Destroy(go); }
                catch { }
            }
            _shieldVfx.Remove(player);
        }

        // === VFX ===

        private static void PlayActivateVFX(Player player)
        {
            try
            {
                // 시전 이펙트 (일회성)
                SimpleVFX.PlayOnPlayer(player, "fx_shield_start", 3f);
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[마인드쉴드] VFX 재생 실패: {ex.Message}");
            }

            // 2초 뒤 보호막 유지 VFX 시작
            SkillTreeInputListener.Instance?.StartCoroutine(DelayedShieldVFX(player));
        }

        private static IEnumerator DelayedShieldVFX(Player player)
        {
            yield return new WaitForSeconds(2f);

            if (!IsActive(player)) yield break;

            try
            {
                float duration = Defense_Config.MindShieldDurationValue - 2f;
                if (duration <= 0f) yield break;

                var vfxObj = SimpleVFX.PlayOnPlayer(player, "vfx_GoblinShield", duration,
                    new Vector3(0f, 1.2f, 0f));
                if (vfxObj != null)
                {
                    vfxObj.transform.localScale = Vector3.one * 1.2f;
                    DimVfx(vfxObj, 0.5f);
                    _shieldVfx[player] = vfxObj;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[마인드쉴드] 쉴드 VFX 재생 실패: {ex.Message}");
            }
        }

        private static void DimVfx(GameObject vfxObj, float factor)
        {
            foreach (var ps in vfxObj.GetComponentsInChildren<ParticleSystem>(true))
            {
                var main = ps.main;
                var c = main.startColor.color;
                main.startColor = new Color(c.r * factor, c.g * factor, c.b * factor, c.a * factor);
            }
            foreach (var r in vfxObj.GetComponentsInChildren<Renderer>(true))
            {
                if (r.material != null && r.material.HasProperty("_Color"))
                {
                    var c = r.material.color;
                    r.material.color = new Color(c.r * factor, c.g * factor, c.b * factor, c.a * factor);
                }
            }
        }

        private static void PlayHitVFX(Player player)
        {
            try
            {
                VFXManager.PlayVFXMultiplayer("fx_StaffShield_Hit", "",
                    player.GetCenterPoint(), Quaternion.identity, 2f);
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[마인드쉴드] 타격 VFX 실패: {ex.Message}");
            }
        }

        // === 코루틴 ===

        private static void StartExpiryCoroutine(Player player)
        {
            StopExpiryCoroutine(player);
            var coroutine = SkillTreeInputListener.Instance?.StartCoroutine(ShieldExpiryCoroutine(player));
            if (coroutine != null)
                _expiryCoroutines[player] = coroutine;
        }

        private static void StopExpiryCoroutine(Player player)
        {
            if (_expiryCoroutines.TryGetValue(player, out var c) && c != null)
            {
                SkillTreeInputListener.Instance?.StopCoroutine(c);
                _expiryCoroutines.Remove(player);
            }
        }

        private static IEnumerator ShieldExpiryCoroutine(Player player)
        {
            yield return new WaitForSeconds(Defense_Config.MindShieldDurationValue);
            if (IsActive(player))
                ExpireShield(player);
        }

        // === Harmony Patch — 데미지 인터셉트 ===

        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        [HarmonyPriority(Priority.High)]
        public static class MindShield_Character_ApplyDamage_Patch
        {
            public static void Prefix(Character __instance, HitData hit)
            {
                if (!(__instance is Player player)) return;
                if (!IsActive(player)) return;

                // 만료 시간 체크
                if (_shieldExpiry.TryGetValue(player, out float expiry) && Time.time > expiry)
                {
                    ExpireShield(player);
                    return;
                }

                float totalDmg = hit.m_damage.m_blunt + hit.m_damage.m_slash + hit.m_damage.m_pierce
                               + hit.m_damage.m_chop + hit.m_damage.m_pickaxe + hit.m_damage.m_fire
                               + hit.m_damage.m_frost + hit.m_damage.m_lightning
                               + hit.m_damage.m_poison + hit.m_damage.m_spirit;
                if (totalDmg <= 0f) return;

                float remaining = _shieldHp.TryGetValue(player, out float hp) ? hp : 0f;
                if (remaining <= 0f)
                {
                    BreakShield(player);
                    return;
                }

                float maxHp = _shieldMaxHp.TryGetValue(player, out float m) ? m : 0f;

                if (totalDmg <= remaining)
                {
                    // 보호막이 피해 전부 흡수
                    float newHp = remaining - totalDmg;
                    _shieldHp[player] = newHp;
                    ZeroAllDamage(hit);
                    PlayHitVFX(player);

                    if (newHp <= 0f)
                        BreakShield(player);
                }
                else
                {
                    // 보호막으로 일부 흡수, 나머지는 HP 피해
                    float ratio = (totalDmg - remaining) / totalDmg;
                    ScaleAllDamage(hit, ratio);
                    PlayHitVFX(player);
                    _shieldHp[player] = 0f;
                    BreakShield(player);
                }
            }
        }

        private static void ZeroAllDamage(HitData hit)
        {
            hit.m_damage.m_blunt = 0f;
            hit.m_damage.m_slash = 0f;
            hit.m_damage.m_pierce = 0f;
            hit.m_damage.m_chop = 0f;
            hit.m_damage.m_pickaxe = 0f;
            hit.m_damage.m_fire = 0f;
            hit.m_damage.m_frost = 0f;
            hit.m_damage.m_lightning = 0f;
            hit.m_damage.m_poison = 0f;
            hit.m_damage.m_spirit = 0f;
        }

        private static void ScaleAllDamage(HitData hit, float ratio)
        {
            hit.m_damage.m_blunt *= ratio;
            hit.m_damage.m_slash *= ratio;
            hit.m_damage.m_pierce *= ratio;
            hit.m_damage.m_chop *= ratio;
            hit.m_damage.m_pickaxe *= ratio;
            hit.m_damage.m_fire *= ratio;
            hit.m_damage.m_frost *= ratio;
            hit.m_damage.m_lightning *= ratio;
            hit.m_damage.m_poison *= ratio;
            hit.m_damage.m_spirit *= ratio;
        }
    }
}

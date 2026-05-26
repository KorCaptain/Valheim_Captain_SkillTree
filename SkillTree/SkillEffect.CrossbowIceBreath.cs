using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 석궁 "빙결 폭발탄" 액티브 스킬 (H키)
    /// 전방 8m·±35° 콘 범위 내 적에게 아이스 브레스 발사
    /// 첫 타격: 무기공격력 80% / 이후 초당 35% × 5회 DoT
    /// VFX: 캐릭터에서 각 적중 몬스터 방향으로 발동
    /// </summary>
    public static partial class SkillEffect
    {
        // === 발칸 아이스 상태 변수 ===
        private static Dictionary<Player, float>     _iceBreathCooldown   = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> _iceBreathCoroutine  = new Dictionary<Player, Coroutine>();

        private const float IceBreathRange = 10f;

        // ============================================================
        /// <summary>H키: 발칸 아이스 활성화</summary>
        // ============================================================
        public static void ActivateCrossbowIceBreath(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 쿨타임 체크
            if (!_iceBreathCooldown.ContainsKey(player))
                _iceBreathCooldown[player] = 0f;

            float cooldownTime = Crossbow_Config.CrossbowIceBreathCooldownValue;
            float elapsed = Time.time - _iceBreathCooldown[player];
            if (elapsed < cooldownTime)
            {
                float remaining = cooldownTime - elapsed;
                DrawFloatingText(player, L.Get("crossbow_ice_breath_cooldown", $"{remaining:F1}"), Color.cyan);
                return;
            }

            // 석궁 장착 체크
            if (!WeaponHelper.IsUsingCrossbow(player))
            {
                DrawFloatingText(player, L.Get("crossbow_equip_required"), Color.red);
                return;
            }

            // 볼트 장전 완료 체크 (Valheim IsWeaponLoaded API)
            if (!player.IsWeaponLoaded())
            {
                DrawFloatingText(player, L.Get("crossbow_not_loaded"), Color.yellow);
                return;
            }

            // 스태미나 체크
            float staminaCost = Crossbow_Config.CrossbowIceBreathStaminaCostValue;
            if (player.GetStamina() < staminaCost)
            {
                DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                return;
            }

            // 발사 방향: 캐릭터 회전 전에 실제 카메라 방향 캡처
            Vector3 fireDir = GameCamera.instance != null
                ? GameCamera.instance.transform.forward
                : player.GetLookDir();

            // 쿨타임 시작 + 스태미나 소모
            _iceBreathCooldown[player] = Time.time;
            ActiveSkillCooldownRegistry.SetCooldownForSkill("H", "crossbow_ice_breath", cooldownTime);
            player.UseStamina(staminaCost);

            // 캐릭터를 카메라 수평 방향으로 회전 (Y=0 유지)
            try
            {
                Vector3 camFlat = new Vector3(fireDir.x, 0f, fireDir.z);
                if (camFlat.sqrMagnitude > 0.001f)
                {
                    camFlat = camFlat.normalized;
                    player.transform.rotation = Quaternion.LookRotation(camFlat);
                    HarmonyLib.Traverse.Create(player).Field("m_lookDir").SetValue(camFlat);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[발칸 아이스] 캐릭터 회전 실패: {ex.Message}");
            }

            // 기존 코루틴 정리
            if (_iceBreathCoroutine.ContainsKey(player) && _iceBreathCoroutine[player] != null)
            {
                try { Plugin.Instance.StopCoroutine(_iceBreathCoroutine[player]); } catch { }
            }
            _iceBreathCoroutine[player] = Plugin.Instance.StartCoroutine(IceBreathCoroutine(player, fireDir));

            DrawFloatingText(player, L.Get("crossbow_ice_breath_ready"), new Color(0.4f, 0.8f, 1f));
        }

        // ============================================================
        private static IEnumerator IceBreathCoroutine(Player player, Vector3 fireDir)
        {
            if (player == null) yield break;

            int iceLevel = SkillTreeManager.Instance?.GetSkillLevel("crossbow_ice_breath") ?? 1;
            if (iceLevel < 1) iceLevel = 1;

            float levelBonus  = (iceLevel - 1) * Crossbow_Config.CrossbowIceBreathLevelBonusValue;
            float firstHitPct = (Crossbow_Config.CrossbowIceBreathFirstHitPctValue + levelBonus) / 100f;

            float dotLevelBonus = (iceLevel - 1) * Crossbow_Config.CrossbowIceBreathDotLevelBonusValue;
            float dotPct      = (Crossbow_Config.CrossbowIceBreathDotPctValue + dotLevelBonus) / 100f;
            int   dotCount    = Crossbow_Config.CrossbowIceBreathDotCountValue;

            // 무기 기본 공격력 (m_pierce 우선)
            var weaponItem = player.GetCurrentWeapon();
            if (weaponItem == null) yield break;

            float crossbowSkillFactor = player.GetSkillFactor(Skills.SkillType.Crossbows);
            var dmgTypes = weaponItem.GetDamage(weaponItem.m_quality, crossbowSkillFactor);
            float baseDmg = dmgTypes.m_pierce > 0f ? dmgTypes.m_pierce
                          : dmgTypes.m_blunt  > 0f ? dmgTypes.m_blunt
                          : dmgTypes.m_slash  > 0f ? dmgTypes.m_slash
                          : 20f;

            // 전방 8m·±35° 내 타겟 확정
            var targets = GetIceBreathTargets(player, fireDir);

            // VFX: 타겟별로 캐릭터 → 타겟 방향 재생 (타겟 없으면 fireDir 방향 1회)
            if (targets.Count > 0)
            {
                foreach (var t in targets)
                    PlayIceBreathVFX(player, t, 3f);
            }
            else
            {
                PlayIceBreathVFXDir(player, fireDir, 3f);
            }

            // === 즉시: 첫 타격 (80%) ===
            foreach (var target in targets)
            {
                try
                {
                    Vector3 hitDir = (target.transform.position - player.transform.position).normalized;

                    var hit = new HitData();
                    hit.m_damage.m_frost  = baseDmg * firstHitPct;
                    hit.m_point           = target.GetCenterPoint();
                    hit.m_dir             = hitDir;
                    hit.m_pushForce       = 500f;
                    hit.m_attacker        = player.GetZDOID();
                    hit.SetAttacker(player);
                    target.Damage(hit);

                    // 넉백: Stagger + Rigidbody 물리력 (10m 넉백)
                    try
                    {
                        target.Stagger(hitDir);
                        var rb = target.GetComponent<Rigidbody>();
                        if (rb != null && !rb.isKinematic)
                            rb.AddForce(hitDir * 120f, ForceMode.Impulse);
                    }
                    catch { }

                    ApplyIceBreathSlow(target);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[발칸 아이스] 첫 타격 오류: {ex.Message}");
                }
            }

            // === DoT: 초당 35% × 5회 ===
            for (int i = 0; i < dotCount; i++)
            {
                yield return new WaitForSeconds(1f);
                if (player == null || player.IsDead()) yield break;

                foreach (var target in targets)
                {
                    try
                    {
                        if (target == null || target.IsDead()) continue;

                        var hit = new HitData();
                        hit.m_damage.m_frost  = baseDmg * dotPct;
                        hit.m_point           = target.GetCenterPoint();
                        hit.m_dir             = (target.transform.position - player.transform.position).normalized;
                        hit.m_pushForce       = 0f;
                        hit.m_attacker        = player.GetZDOID();
                        hit.SetAttacker(player);
                        target.Damage(hit);
                    }
                    catch (Exception ex)
                    {
                        Plugin.Log.LogWarning($"[발칸 아이스] DoT 오류: {ex.Message}");
                    }
                }
            }

            _iceBreathCoroutine.Remove(player);
        }

        // ============================================================
        /// <summary>전방 콘 범위 내 유효 대상 반환 (8m, ±35°)</summary>
        // ============================================================
        private static List<Character> GetIceBreathTargets(Player player, Vector3 fireDir)
        {
            var result = new List<Character>();
            var origin = player.transform.position + Vector3.up * 1f;

            foreach (var c in Character.GetAllCharacters())
            {
                try
                {
                    if (c == null || c == player || c.IsDead()) continue;
                    if (c.IsPlayer()) continue;
                    if (!c.IsMonsterFaction(Time.time) && c.GetFaction() != Character.Faction.Boss) continue;

                    float dist = (c.GetCenterPoint() - origin).magnitude;
                    if (dist > IceBreathRange) continue;

                    // 콘 체크: 수평 방향만 사용 (거대몹 높이 차이 보정)
                    Vector3 toCenter = c.GetCenterPoint() - origin;
                    Vector3 flatDir  = new Vector3(toCenter.x, 0f, toCenter.z);
                    Vector3 flatFire = new Vector3(fireDir.x,  0f, fireDir.z);
                    if (flatDir.sqrMagnitude < 0.001f || flatFire.sqrMagnitude < 0.001f) continue;
                    if (Vector3.Angle(flatFire.normalized, flatDir.normalized) > 35f) continue;

                    result.Add(c);
                }
                catch { }
            }
            return result;
        }

        // ============================================================
        /// <summary>이동속도 50% 감소 슬로우 2초 적용</summary>
        // ============================================================
        private static void ApplyIceBreathSlow(Character target)
        {
            try
            {
                var slowSE = ScriptableObject.CreateInstance<SE_Stats>();
                slowSE.m_name          = L.Get("se_ice_breath_slow");
                slowSE.m_tooltip       = L.Get("se_ice_breath_slow_tooltip");
                slowSE.m_ttl           = 2f;
                slowSE.m_speedModifier = 0.5f;
                target.GetSEMan()?.AddStatusEffect(slowSE, true);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[발칸 아이스] 슬로우 적용 실패: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>VFX: 플레이어 → 타겟 방향으로 재생</summary>
        // ============================================================
        private static void PlayIceBreathVFX(Player player, Character target, float duration)
        {
            try
            {
                var origin  = player.transform.position + Vector3.up * 1.2f;
                var flatDir = new Vector3(
                    target.transform.position.x - origin.x,
                    0f,
                    target.transform.position.z - origin.z
                ).normalized;
                var rot = Quaternion.LookRotation(flatDir);
                VFXManager.PlayVFXMultiplayer(
                    "vfx_dragon_coldbreath",
                    "sfx_dragon_coldbreath_trailon",
                    origin, rot, duration);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[발칸 아이스] VFX 재생 실패: {ex.Message}");
            }
        }

        /// <summary>VFX: 타겟 없을 때 fireDir 방향으로 재생</summary>
        private static void PlayIceBreathVFXDir(Player player, Vector3 fireDir, float duration)
        {
            try
            {
                var origin = player.transform.position + Vector3.up * 1.2f;
                var rot    = Quaternion.LookRotation(fireDir);
                VFXManager.PlayVFXMultiplayer(
                    "vfx_dragon_coldbreath",
                    "sfx_dragon_coldbreath_trailon",
                    origin, rot, duration);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[발칸 아이스] VFX 재생 실패: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>플레이어 사망 시 정리</summary>
        // ============================================================
        public static void CleanupIceBreathOnDeath(Player player)
        {
            try
            {
                if (_iceBreathCoroutine.ContainsKey(player))
                {
                    if (_iceBreathCoroutine[player] != null)
                    {
                        try { Plugin.Instance.StopCoroutine(_iceBreathCoroutine[player]); } catch { }
                    }
                    _iceBreathCoroutine.Remove(player);
                }
                _iceBreathCooldown.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[발칸 아이스] 정리 실패: {ex.Message}");
            }
        }
    }
}

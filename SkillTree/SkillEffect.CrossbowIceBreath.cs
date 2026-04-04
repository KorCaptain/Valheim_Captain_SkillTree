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
    /// 석궁 "발칸 아이스" 액티브 스킬 (H키)
    /// 전방 10m·±35° 콘 범위 내 적에게 아이스 브레스 발사
    /// 첫 타격: 무기공격력 80% / 이후 초당 35% × 5회 DoT
    /// </summary>
    public static partial class SkillEffect
    {
        // === 발칸 아이스 상태 변수 ===
        private static Dictionary<Player, float>     _iceBreathCooldown   = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> _iceBreathCoroutine  = new Dictionary<Player, Coroutine>();
        public  static bool                          _iceBreathActivating  = false;

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

            // 장전 체크 (볼트 장착 여부)
            if (player.GetAmmoItem() == null)
            {
                DrawFloatingText(player, L.Get("crossbow_no_ammo"), Color.red);
                return;
            }

            // 스태미나 체크
            float staminaCost = Crossbow_Config.CrossbowIceBreathStaminaCostValue;
            if (player.GetStamina() < staminaCost)
            {
                DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                return;
            }

            // 쿨타임 시작 + 스태미나 소모
            _iceBreathCooldown[player] = Time.time;
            ActiveSkillCooldownRegistry.SetCooldown("H", cooldownTime);
            player.UseStamina(staminaCost);

            // 캐릭터를 카메라 수평 방향으로 회전 (Y=0 유지)
            try
            {
                Vector3 camFlat = player.GetLookDir();
                camFlat.y = 0f;
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

            // 석궁 발사 모션 (연속 발사 차단 플래그 설정)
            _iceBreathActivating = true;
            try { player.StartAttack(null, false); } catch { }
            _iceBreathActivating = false;

            // 기존 코루틴 정리
            if (_iceBreathCoroutine.ContainsKey(player) && _iceBreathCoroutine[player] != null)
            {
                try { Plugin.Instance.StopCoroutine(_iceBreathCoroutine[player]); } catch { }
            }
            _iceBreathCoroutine[player] = Plugin.Instance.StartCoroutine(IceBreathCoroutine(player));

            DrawFloatingText(player, L.Get("crossbow_ice_breath_ready"), new Color(0.4f, 0.8f, 1f));
        }

        // ============================================================
        private static IEnumerator IceBreathCoroutine(Player player)
        {
            if (player == null) yield break;

            float firstHitPct = Crossbow_Config.CrossbowIceBreathFirstHitPctValue / 100f;
            float dotPct      = Crossbow_Config.CrossbowIceBreathDotPctValue       / 100f;
            int   dotCount    = Crossbow_Config.CrossbowIceBreathDotCountValue;

            // 무기 기본 공격력 (m_pierce 우선)
            var weaponItem = player.GetCurrentWeapon();
            if (weaponItem == null) yield break;

            var dmgTypes = weaponItem.GetDamage();
            float baseDmg = dmgTypes.m_pierce > 0f ? dmgTypes.m_pierce
                          : dmgTypes.m_blunt  > 0f ? dmgTypes.m_blunt
                          : dmgTypes.m_slash  > 0f ? dmgTypes.m_slash
                          : 20f;

            // VFX 1회만 재생
            PlayIceBreathVFX(player, 3f);

            // === 즉시: 첫 타격 (80%) + DoT 대상 캡처 ===
            // 발사 시점 카메라 방향으로 대상 확정 → DoT 전체에서 재사용
            var targets = GetIceBreathTargets(player);
            foreach (var target in targets)
            {
                try
                {
                    var hit = new HitData();
                    hit.m_damage.m_frost  = baseDmg * firstHitPct;
                    hit.m_point           = target.GetCenterPoint();
                    hit.m_dir             = (target.transform.position - player.transform.position).normalized;
                    hit.m_pushForce       = 150f;
                    hit.m_attacker        = player.GetZDOID();
                    target.Damage(hit);
                    ApplyIceBreathSlow(target);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[발칸 아이스] 첫 타격 오류: {ex.Message}");
                }
            }

            // === DoT: 초당 35% × 5회 (캡처된 대상 재사용) ===
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
        /// <summary>카메라 전방 방향 (Y 포함 — 수직 조준 지원)</summary>
        // ============================================================
        private static Vector3 GetCameraForward3D(Player player)
        {
            // player.GetLookDir() = Valheim 실제 조준 방향 (화살·석궁 볼트와 동일)
            return player.GetLookDir();
        }

        // ============================================================
        /// <summary>전방 콘 범위 내 유효 대상 반환 (10m, ±35°)</summary>
        // ============================================================
        private static List<Character> GetIceBreathTargets(Player player)
        {
            var result = new List<Character>();
            var origin = player.transform.position + Vector3.up * 1f;

            foreach (var c in Character.GetAllCharacters())
            {
                try
                {
                    if (c == null || c == player || c.IsDead()) continue;
                    if (!c.IsMonsterFaction(Time.time) && !c.IsPlayer()) continue;
                    if (c.IsPlayer() && c == player) continue;

                    float dist = (c.GetCenterPoint() - origin).magnitude;
                    if (dist > IceBreathRange) continue;

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
                slowSE.m_name          = "발칸 아이스";
                slowSE.m_tooltip       = "이동속도 -50%";
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
        private static void PlayIceBreathVFX(Player player, float duration)
        {
            try
            {
                var lookDir = GetCameraForward3D(player);
                var pos     = player.transform.position + lookDir * 2f + Vector3.up * 1.2f;
                var rot     = Quaternion.LookRotation(lookDir);
                VFXManager.PlayVFXMultiplayer(
                    "vfx_dragon_coldbreath",
                    "sfx_dragon_coldbreath_trailon",
                    pos, rot, duration);
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

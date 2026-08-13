using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기(Mace) 액티브 스킬 전용 클래스 - 충격파 강타 (Shockwave Slam)
    /// - G키 액티브 스킬. 방패돌진이 방어 트리로 이전되며 그 자리를 대체.
    ///   반경 7m 이내 몬스터를 스윙 임팩트 순간(공격속도 +100% 적용, 시전 0.8초 후) 1.5초간 스태거시키며 광역 데미지를 동시 적용.
    /// </summary>
    public static partial class SkillEffect
    {
        // ==================== 충격파 강타 (Shockwave Slam) ====================

        private static Dictionary<Player, float> shockwaveSlamCooldowns = new Dictionary<Player, float>();
        private static HashSet<Player> shockwaveSlamActive = new HashSet<Player>();
        private static Dictionary<Player, bool> shockwaveSlam1stHitBuff = new Dictionary<Player, bool>(); // 스윙 공격속도 버프 (FuryHammerSkill.furyHammer1stHitBuff와 동일 패턴)

        private const float ShockwaveSlamRange = 7f;
        private const float ShockwaveSlamStunDuration = 1.5f; // 강제 스태거 지속시간
        private const float ShockwaveSlamSwingImpactDelay = 0.6f; // 스윙 SetTrigger → 실제 내리찍는 임팩트 프레임까지 대기 (공격속도 +100% 버프 반영 후 모션이 끝나는 시점)

        /// <summary>
        /// 충격파 강타 스윙 공격속도 버프 활성 상태 확인 (분노의 망치와 동일 패턴)
        /// </summary>
        public static bool IsShockwaveSlam1stHitBuffActive(Player player)
        {
            if (player == null) return false;
            return shockwaveSlam1stHitBuff.TryGetValue(player, out bool buff) && buff;
        }

        /// <summary>
        /// 충격파 강타 스윙 공격속도 버프 보너스 (%) — AnimationSpeedManager가 GetTotalAttackSpeedBonus()를 통해 자동 적용
        /// </summary>
        public static float GetShockwaveSlam1stHitSpeedBonus(Player player)
        {
            return IsShockwaveSlam1stHitBuffActive(player) ? 100f : 0f;
        }

        /// <summary>
        /// 충격파 강타 액티브 스킬 발동
        /// </summary>
        public static void ActivateShockwaveSlam(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                if (!HasSkill("mace_Step7_shockwave_slam"))
                {
                    DrawFloatingText(player, L.Get("g_key_skill_required"), Color.red);
                    return;
                }

                if (!WeaponHelper.IsUsingMace(player))
                {
                    DrawFloatingText(player, L.Get("requirement_two_hand_mace"), Color.red);
                    return;
                }

                if (shockwaveSlamActive.Contains(player))
                {
                    return;
                }

                float now = Time.time;
                if (shockwaveSlamCooldowns.ContainsKey(player) && now < shockwaveSlamCooldowns[player])
                {
                    float remaining = shockwaveSlamCooldowns[player] - now;
                    DrawFloatingText(player, L.Get("cooldown_seconds", Mathf.CeilToInt(remaining).ToString()), Color.yellow);
                    return;
                }

                float requiredStamina = Mace_Config.ShockwaveSlamStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                shockwaveSlamCooldowns[player] = now + Mace_Config.ShockwaveSlamCooldownValue;
                ActiveSkillCooldownRegistry.SetCooldownForSkill("G", "mace_Step7_shockwave_slam", Mace_Config.ShockwaveSlamCooldownValue);
                player.UseStamina(requiredStamina);

                player.StartCoroutine(ShockwaveSlamCoroutine(player));
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[충격파 강타] 발동 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 충격파 강타 핵심 코루틴
        /// 시전 즉시 슬랫지 스윙 모션 재생(+100% 공격속도) → 모션 종료(임팩트) 시점에 반경 7m 스캔 → 1.5초 강제 스태거 + VFX/SFX + 광역 데미지 동시 적용
        /// </summary>
        private static IEnumerator ShockwaveSlamCoroutine(Player player)
        {
            shockwaveSlamActive.Add(player);

            var animator = player.GetComponentInChildren<Animator>();
            bool origRootMotion = animator != null && animator.applyRootMotion;
            if (animator != null) animator.applyRootMotion = false; // 스윙 SetTrigger와 이후 입력(공격/블록) 모션이 겹칠 때 원치 않는 전방 이동 방지 (FuryHammerSkill과 동일 패턴)

            Vector3 playerForward = player.GetLookDir();
            playerForward.y = 0f;
            if (playerForward.sqrMagnitude < 0.01f) playerForward = player.transform.forward;
            playerForward.Normalize();

            // 시전 즉시 슬랫지 스윙 모션 재생 + 공격속도 +100% 버프 (몬스터 유무와 무관 — 빈 땅에서도 시전감 있도록)
            shockwaveSlam1stHitBuff[player] = true;
            var sledgeAttack = FuryHammerSkill.GetCachedSledgeIronAttack();
            var zanim = player.GetComponentInChildren<ZSyncAnimation>();
            if (sledgeAttack != null && zanim != null && !string.IsNullOrEmpty(sledgeAttack.m_attackAnimation))
            {
                string trigger = (sledgeAttack.m_attackChainLevels > 1 || sledgeAttack.m_attackRandomAnimations >= 2)
                    ? sledgeAttack.m_attackAnimation + "0"
                    : sledgeAttack.m_attackAnimation;
                zanim.SetTrigger(trigger);
            }

            // 스윙 모션이 끝나고 실제로 땅을 내리찍는 임팩트 순간까지 대기 (+100% 공격속도 적용된 시간)
            // 버프는 여기서 바로 끄지 않고 스킬 종료까지 유지 — 애니메이션 재생이 대기 시간보다 살짝 길게 남아 InAttack()이
            // 계속 true인 상태에서 버프만 먼저 꺼지면 공격속도 캡(70%) 경고가 잘못 뜨는 것을 방지 (FuryHammerSkill 패턴과 동일하게 스킬 활성 구간 전체를 커버)
            yield return new WaitForSeconds(ShockwaveSlamSwingImpactDelay);
            if (player == null || player.IsDead())
            {
                shockwaveSlam1stHitBuff.Remove(player);
                if (animator != null) animator.applyRootMotion = origRootMotion;
                shockwaveSlamActive.Remove(player);
                yield break;
            }

            // 임팩트 순간에 스캔 (대기 중 이동/사망한 몬스터를 정확히 반영) — 방향 무관 반경 7m
            var gathered = new List<Character>();

            try
            {
                Vector3 playerPos = player.transform.position;
                foreach (var c in Character.GetAllCharacters())
                {
                    if (c == null || c.IsDead() || c == player) continue;
                    if (!JobSkillsUtility.IsMonsterFaction(c.GetFaction()))
                    {
                        var tp = c as Player;
                        if (tp == null || !tp.IsPVPEnabled() || !player.IsPVPEnabled()) continue;
                    }

                    if (Vector3.Distance(c.transform.position, playerPos) > ShockwaveSlamRange) continue;

                    gathered.Add(c);
                }

                Plugin.Log.LogInfo($"[충격파 강타] 임팩트 — 반경 {ShockwaveSlamRange}m 스캔, 수집 적: {gathered.Count}명");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[충격파 강타] 스캔 오류: {ex.Message}");
                shockwaveSlam1stHitBuff.Remove(player);
                if (animator != null) animator.applyRootMotion = origRootMotion;
                shockwaveSlamActive.Remove(player);
                yield break;
            }

            // 반경 내 몬스터 전원 1.5초 강제 스태거 (HitData + m_staggerTimer 강제 지속시간 패턴, ExecuteShockwaveSkill과 동일)
            foreach (var ge in gathered)
            {
                if (ge == null || ge.IsDead()) continue;

                Vector3 dir = (ge.transform.position - player.transform.position).normalized;

                var staggerHit = new HitData();
                staggerHit.m_damage.m_blunt = 0.1f;
                staggerHit.m_staggerMultiplier = 100f;
                staggerHit.m_pushForce = 0f;
                staggerHit.m_point = ge.transform.position;
                staggerHit.m_dir = dir;
                staggerHit.SetAttacker(player);
                ge.Damage(staggerHit);

                var traverse = HarmonyLib.Traverse.Create(ge);
                if (traverse.Field("m_staggerTimer").FieldExists())
                    traverse.Field("m_staggerTimer").SetValue(ShockwaveSlamStunDuration);

                ge.Stagger(dir);
            }

            VFXManager.PlayVFXMultiplayer("vfx_sledge_hit", "sfx_sledge_hit", player.GetCenterPoint(), Quaternion.identity, 1f);

            try
            {
                ApplyShockwaveSlamDamage(player, gathered, playerForward);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[충격파 강타] 데미지 적용 오류: {ex.Message}");
            }

            shockwaveSlam1stHitBuff.Remove(player);
            if (animator != null) animator.applyRootMotion = origRootMotion;
            shockwaveSlamActive.Remove(player);
            Plugin.Log.LogInfo($"[충격파 강타] 종료 — 타격 대상: {gathered.Count}명");
        }

        /// <summary>
        /// 임팩트 순간 1회성 광역 데미지: 무기 공격력 기준 %(레벨별 230~350%)
        /// </summary>
        private static void ApplyShockwaveSlamDamage(Player player, List<Character> gathered, Vector3 dashDir)
        {
            var weapon = player.GetCurrentWeapon();
            if (weapon == null) return;

            float skillFactor = player.GetSkillFactor(Skills.SkillType.Clubs);
            var weaponDamage = weapon.GetDamage(0, skillFactor);

            int level = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_shockwave_slam") ?? 1;
            float pct = (Mace_Config.ShockwaveSlamDamagePercentValue + (level - 1) * Mace_Config.ShockwaveSlamLevelBonusValue) / 100f;

            bool hitAny = false;
            float totalDmg = 0f;
            foreach (var enemy in gathered)
            {
                if (enemy == null || enemy.IsDead()) continue;

                var hit = new HitData();
                hit.m_damage.m_blunt = weaponDamage.m_blunt * pct;
                hit.m_damage.m_slash = weaponDamage.m_slash * pct;
                hit.m_damage.m_pierce = weaponDamage.m_pierce * pct;
                hit.m_damage.m_chop = weaponDamage.m_chop * pct;
                hit.m_attacker = player.GetZDOID();
                hit.SetAttacker(player);
                hit.m_point = enemy.GetCenterPoint();
                hit.m_dir = dashDir;
                hit.m_skill = Skills.SkillType.Clubs;
                hit.m_blockable = false;
                hit.m_dodgeable = false;

                enemy.Damage(hit);
                VFXManager.PlayVFXMultiplayer("hit_02", "sfx_ice_hit", enemy.GetCenterPoint(), Quaternion.identity, 1.5f);

                if (!enemy.IsPlayer())
                {
                    TankerTauntAIPatch.AddTauntedMonster(enemy, player, 5f);
                }

                totalDmg = hit.GetTotalDamage();
                hitAny = true;
            }

            if (hitAny)
            {
                DrawFloatingText(player, "💥 " + L.Get("mace_skill_shockwave_slam") + $" {Mathf.RoundToInt(totalDmg)}", new Color(1f, 0.5f, 0.1f, 1f));
            }

            Plugin.Log.LogInfo($"[충격파 강타] 최종타 완료 — 대상:{gathered.Count}명, 비율={pct * 100f:F0}%");
        }

        /// <summary>
        /// 충격파 강타 정리 메서드 (플레이어 사망 시 호출)
        /// </summary>
        public static void CleanupShockwaveSlamOnDeath(Player player)
        {
            try
            {
                shockwaveSlamCooldowns.Remove(player);
                shockwaveSlamActive.Remove(player);
                shockwaveSlam1stHitBuff.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[충격파 강타] 정리 실패: {ex.Message}");
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static partial class Sword_Skill
    {
        // === 회오리베기 (Whirlwind Slash) 상태 관리 ===
        private static Dictionary<Player, float> whirlwindCooldowns = new Dictionary<Player, float>();
        private static HashSet<Player> whirlwindCharging = new HashSet<Player>();
        private static Attack s_polearmSecondaryCache = null;

        /// <summary>
        /// H키: 회오리베기 발동
        /// 검 장착 확인 → 쿨타임 확인 → 스태미나 확인 → 3단계 AoE 코루틴 시작
        /// </summary>
        public static void ActivateWhirlwindSlash(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                // 1. 스킬 보유 확인
                if (!SkillEffect.HasSkill("sword_step5_defswitch"))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("parry_rush_skill_required"), Color.red);
                    return;
                }

                // 2. 검 또는 도끼 장착 확인
                if (!WeaponHelper.IsUsingSwordOrAxe(player))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("sword_or_axe_required"), Color.red);
                    return;
                }

                // 3. 시전 중 중복 방지
                if (whirlwindCharging.Contains(player))
                {
                    SkillEffect.DrawFloatingText(player, L.Get("parry_rush_already_active"), Color.yellow);
                    return;
                }

                // 4. 쿨타임 확인
                float now = Time.time;
                if (whirlwindCooldowns.TryGetValue(player, out float cdEnd) && now < cdEnd)
                {
                    float remaining = cdEnd - now;
                    SkillEffect.DrawFloatingText(player, L.Get("cooldown_remaining", Mathf.CeilToInt(remaining)), Color.yellow);
                    return;
                }

                // 5. 스태미나 확인
                float staminaCost = Sword_Config.ParryRushStaminaCostValue;
                if (player.GetStamina() < staminaCost)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // 6. 스태미나 소모 + 쿨타임 등록
                player.UseStamina(staminaCost);
                float cooldown = Sword_Config.ParryRushCooldownValue;
                whirlwindCooldowns[player] = now + cooldown;
                ActiveSkillCooldownRegistry.SetCooldownForSkill("H", "sword_step5_defswitch", cooldown);
                SkillEffect.TankerPrereqLastUsedTime = Time.time;

                // 7. 시전 시작
                whirlwindCharging.Add(player);
                var weapon = player.GetCurrentWeapon();
                player.StartCoroutine(ExecuteWhirlwindSlashCoroutine(player, weapon));

                Plugin.Log.LogInfo($"[회오리베기] 발동 — 쿨타임 {cooldown}초");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[회오리베기] 발동 오류: {ex.Message}");
            }
        }

        public static bool IsWhirlwindCharging(Player player) => whirlwindCharging.Contains(player);

        /// <summary>
        /// 회오리베기 3단계 순차 AoE 코루틴
        /// </summary>
        private static IEnumerator ExecuteWhirlwindSlashCoroutine(Player player, ItemDrop.ItemData weapon)
        {
            const float radius1 = 5f;
            const float radius2 = 8f;
            const float radius3 = 12f;
            const float dmg1    = 140f;
            const float dmg2    = 180f;
            const float dmg3    = 220f;

            // === 1차 공격 ===
            Plugin.Log.LogInfo("[회오리베기] 코루틴: 1차 진입");
            if (player != null && !player.IsDead())
            {
                try { TriggerSpinAnimation(player, weapon); }
                catch (Exception ex) { Plugin.Log.LogError($"[회오리베기] 1차 애니 오류: {ex.Message}"); }
                ApplyWhirlwindAoE(player, weapon, radius1, dmg1,
                    "fx_Fader_Roar_Projectile_Hit", "sfx_fader_claw_swipe", 1);
            }

            yield return new WaitForSeconds(0.9f);

            // === 2차 공격 ===
            Plugin.Log.LogInfo("[회오리베기] 코루틴: 2차 진입");
            if (player != null && !player.IsDead())
            {
                try { TriggerSpinAnimation(player, weapon); }
                catch (Exception ex) { Plugin.Log.LogError($"[회오리베기] 2차 애니 오류: {ex.Message}"); }
                ApplyWhirlwindAoE(player, weapon, radius2, dmg2,
                    "fx_Fader_Fissure_Prespawn", "sfx_fader_claw_swipe", 2);
            }

            yield return new WaitForSeconds(0.9f);

            // === 3차 공격 ===
            Plugin.Log.LogInfo("[회오리베기] 코루틴: 3차 진입");
            if (player != null && !player.IsDead())
            {
                try { TriggerSpinAnimation(player, weapon); }
                catch (Exception ex) { Plugin.Log.LogError($"[회오리베기] 3차 애니 오류: {ex.Message}"); }
                ApplyWhirlwindAoE(player, weapon, radius3, dmg3,
                    "fx_Fader_Spin", "sfx_fader_claw_swipe", 3);
            }

            // whirlwindCharging 제거 → IsWhirlwindCharging=false → AnimationSpeedManager 자동 복원
            whirlwindCharging.Remove(player);
        }

        private static Attack GetCachedPolearmSecondary()
        {
            if (s_polearmSecondaryCache != null) return s_polearmSecondaryCache;
            string[] candidates = { "AtgeirBlackmetal", "AtgeirHimminAfl", "AtgeirKrom", "AtgeirBronze" };
            foreach (var name in candidates)
            {
                var prefab = ObjectDB.instance?.GetItemPrefab(name);
                var atk = prefab?.GetComponent<ItemDrop>()?.m_itemData?.m_shared?.m_secondaryAttack;
                if (atk != null && !string.IsNullOrEmpty(atk.m_attackAnimation))
                {
                    s_polearmSecondaryCache = atk;
                    Plugin.Log.LogInfo($"[회오리베기] 폴암 모션 캐시: {name} → {atk.m_attackAnimation}");
                    return atk;
                }
            }
            return null;
        }

        private static void TriggerSpinAnimation(Player player, ItemDrop.ItemData weapon)
        {
            // 검/도끼 모두 폴암 세컨드 모션으로 통일 (StartAttack 블로킹 우회)
            var polearmAtk = GetCachedPolearmSecondary();
            var zanim = player.GetComponentInChildren<ZSyncAnimation>();

            if (polearmAtk != null && zanim != null && !string.IsNullOrEmpty(polearmAtk.m_attackAnimation))
            {
                string trigger = (polearmAtk.m_attackChainLevels > 1 || polearmAtk.m_attackRandomAnimations >= 2)
                    ? polearmAtk.m_attackAnimation + "0"
                    : polearmAtk.m_attackAnimation;
                zanim.SetTrigger(trigger);
                return;
            }

            // 폴백: ObjectDB 미로드 시 기존 방식
            bool isSword = weapon?.m_shared?.m_skillType == Skills.SkillType.Swords;
            var fallbackAtk = isSword
                ? weapon?.m_shared?.m_secondaryAttack
                : weapon?.m_shared?.m_attack;
            if (fallbackAtk != null)
            {
                float orig = fallbackAtk.m_attackStamina;
                fallbackAtk.m_attackStamina = 0f;
                player.StartAttack(null, isSword);
                fallbackAtk.m_attackStamina = orig;
            }
            else
            {
                player.StartAttack(null, false);
            }
        }

        /// <summary>
        /// 회오리베기 단계별 AoE 데미지 + VFX + 스태거
        /// 반경 내 몬스터/보스/플레이어 전체 적중
        /// </summary>
        private static void ApplyWhirlwindAoE(Player player, ItemDrop.ItemData weapon,
            float radius, float damageRatio, string vfxName, string sfxName, int hitPhase)
        {
            if (player == null || weapon == null) return;

            Vector3 playerPos = player.transform.position;
            int hitCount = 0;

            try
            {
                int whirlwindLevel = SkillTreeManager.Instance?.GetSkillLevel("sword_step5_defswitch") ?? 1;
                float levelBonus = (whirlwindLevel - 1) * Sword_Config.WhirlwindSlashLevelBonusValue;
                float scaledBase = Sword_Config.WhirlwindSlashBaseDamageValue + levelBonus;
                float totalDmg = scaledBase * (damageRatio / 100f);

                foreach (var c in Character.GetAllCharacters())
                {
                    if (c == null || c.IsDead() || c == player) continue;

                    bool isHostile = c.IsMonsterFaction(0f)
                        || c.m_faction == Character.Faction.Boss
                        || (c is Player && c != player);
                    if (!isHostile) continue;

                    if (Vector3.Distance(c.transform.position, playerPos) > radius) continue;

                    Vector3 dir = (c.transform.position - playerPos);
                    dir.y = 0;
                    dir = dir.sqrMagnitude > 0.001f ? dir.normalized : player.transform.forward;

                    var hit = new HitData();
                    hit.m_damage.m_slash = totalDmg;
                    hit.m_point = c.GetCenterPoint();
                    hit.m_dir = dir;
                    hit.m_staggerMultiplier = 10f;
                    hit.m_attacker = player.GetZDOID();
                    hit.SetAttacker(player);
                    hit.m_toolTier = (short)weapon.m_shared.m_toolTier;
                    hit.m_skill = weapon.m_shared.m_skillType;
                    hit.m_blockable = false;
                    hit.m_dodgeable = false;

                    HitData.DamageModifiers dmgMods = c.GetDamageModifiers();
                    HitData.DamageModifier dmgMod;
                    hit.ApplyResistance(dmgMods, out dmgMod);
                    c.ApplyDamage(hit, true, true, dmgMod);
                    c.Stagger(dir);
                    VFXManager.PlayVFXMultiplayer("fx_crit", "", c.GetCenterPoint(), Quaternion.identity, 1.5f);

                    // 3차 공격: 반경 5m 이내 적 넉백
                    if (hitPhase == 3 && Vector3.Distance(c.transform.position, playerPos) <= 5f)
                    {
                        Vector3 knockDir = dir;
                        knockDir.y = 0.2f;
                        knockDir.Normalize();
                        const float kbForce = 35f;
                        var tc = Traverse.Create(c);
                        if (tc.Field("m_pushForce").FieldExists())
                        {
                            var cur = (Vector3)tc.Field("m_pushForce").GetValue();
                            if (cur.magnitude < kbForce)
                                tc.Field("m_pushForce").SetValue(knockDir * kbForce);
                        }
                    }

                    hitCount++;
                }

                // 단계별 VFX + SFX (캐릭터 중심)
                VFXManager.PlayVFXMultiplayer(vfxName, sfxName, playerPos, Quaternion.identity, 2f);
                SkillEffect.DrawFloatingText(player, L.Get("parry_rush_damage", hitPhase, Mathf.RoundToInt(damageRatio)), Color.yellow);
                Plugin.Log.LogInfo($"[회오리베기] {hitPhase}차 공격 — 반경 {radius}m, 데미지 {damageRatio}%, 적중 {hitCount}명");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[회오리베기] {hitPhase}차 AoE 오류: {ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// 사망 시 회오리베기 상태 정리
        /// </summary>
        public static void CleanupWhirlwindOnDeath(Player player)
        {
            try
            {
                whirlwindCooldowns.Remove(player);
                whirlwindCharging.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[회오리베기] 사망 정리 실패: {ex.Message}");
            }
        }
    }
}

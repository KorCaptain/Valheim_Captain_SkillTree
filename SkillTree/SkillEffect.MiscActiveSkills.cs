using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Gui;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 기타 액티브 스킬 시스템 (발구르기, 도발 등)
    /// </summary>
    public static partial class SkillEffect
    {
        // === 도발 관련 변수 ===
        public static float tauntCooldown = 15f;
        public static float lastTauntTime = -999f;
        public static Dictionary<Player, Coroutine> tauntCooldownCoroutine = new Dictionary<Player, Coroutine>();

        /// <summary>
        /// 발구르기 스킬 실행 (자동 발동 패시브)
        /// </summary>
        public static void ExecuteStompSkill(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                if (!HasSkill("defense_Step4_instant")) return;

                float radius = Defense_Config.StompRadiusValue;
                float knockback = Defense_Config.StompKnockbackValue;
                float vfxDuration = Defense_Config.StompVFXDurationValue;

                Vector3 playerPos = player.transform.position;

                Collider[] hitColliders = Physics.OverlapSphere(playerPos, radius);
                List<Character> hitEnemies = new List<Character>();

                foreach (var collider in hitColliders)
                {
                    if (collider == null) continue;

                    Character enemy = collider.GetComponentInParent<Character>();
                    if (enemy != null && enemy != player && !enemy.IsDead() &&
                        BaseAI.IsEnemy(player, enemy) && !hitEnemies.Contains(enemy))
                    {
                        Vector3 direction = (enemy.transform.position - playerPos).normalized;
                        direction.y = 0.5f;

                        HitData pushHit = new HitData();
                        pushHit.m_point = enemy.transform.position;
                        pushHit.m_dir = direction;
                        pushHit.m_pushForce = knockback;
                        pushHit.m_damage.m_damage = 0.01f;
                        pushHit.m_attacker = player.GetZDOID();
                        pushHit.SetAttacker(player);

                        enemy.Damage(pushHit);
                        enemy.Stagger(direction);

                        hitEnemies.Add(enemy);

                        try
                        {
                            Vector3 enemyHeadPos = enemy.GetHeadPoint();
                            SimpleVFX.Play("statusailment_01_aura", enemyHeadPos, 2f);
                        }
                        catch { }
                    }
                }

                PlayStompVFX(playerPos, vfxDuration);
                player.StartCoroutine(PlayStompSFX(playerPos));

                if (hitEnemies.Count > 0)
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("ground_stomp_effect", hitEnemies.Count.ToString()));
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[발구르기] 실행 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 충격파방출 스킬 실행 (자동 발동 패시브 - HP 45% 이하)
        /// </summary>
        public static void ExecuteShockwaveSkill(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;
                if (!HasSkill("defense_Step4_mental")) return;

                float radius = Defense_Config.ShockwaveRadiusValue;
                float stunDuration = Defense_Config.ShockwaveStunDurationValue;
                Vector3 playerPos = player.transform.position;

                Collider[] hitColliders = Physics.OverlapSphere(playerPos, radius);
                List<Character> hitEnemies = new List<Character>();

                foreach (var collider in hitColliders)
                {
                    if (collider == null) continue;
                    Character enemy = collider.GetComponentInParent<Character>();
                    if (enemy == null || enemy == player || enemy.IsDead()) continue;
                    if (!BaseAI.IsEnemy(player, enemy)) continue;
                    if (hitEnemies.Contains(enemy)) continue;

                    Vector3 dir = (enemy.transform.position - playerPos).normalized;

                    // 스태거 적용 (3단계)
                    HitData staggerHit = new HitData();
                    staggerHit.m_damage.m_blunt = 0.1f;
                    staggerHit.m_staggerMultiplier = 100f;
                    staggerHit.m_pushForce = 0f;
                    staggerHit.m_point = enemy.transform.position;
                    staggerHit.m_dir = dir;
                    staggerHit.SetAttacker(player);
                    enemy.Damage(staggerHit);

                    var traverse = Traverse.Create(enemy);
                    if (traverse.Field("m_staggerTimer").FieldExists())
                        traverse.Field("m_staggerTimer").SetValue(stunDuration);

                    enemy.Stagger(dir);

                    hitEnemies.Add(enemy);
                }

                VFXManager.PlayVFXMultiplayer("fx_Fader_CorpseExplosion", "", playerPos);
                player.StartCoroutine(PlayStompSFX(playerPos));

                if (hitEnemies.Count > 0)
                    player.Message(MessageHud.MessageType.Center,
                        L.Get("shockwave_effect", hitEnemies.Count.ToString()));
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[충격파방출] 실행 오류: {ex.Message}");
            }
        }

        private static void PlayStompVFX(Vector3 position, float duration)
        {
            try
            {
                SimpleVFX.Play("shine_blue", position, duration);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[발구르기 VFX] 재생 오류: {ex.Message}");
            }
        }

        private static IEnumerator PlayStompSFX(Vector3 position)
        {
            var sfxPrefab = ZNetScene.instance?.GetPrefab("sfx_metal_blocked");
            if (sfxPrefab != null)
            {
                var sfxObj1 = UnityEngine.Object.Instantiate(sfxPrefab, position, Quaternion.identity);
                var znetView1 = sfxObj1?.GetComponent<ZNetView>();
                if (znetView1 != null) UnityEngine.Object.DestroyImmediate(znetView1);
                if (sfxObj1 != null) UnityEngine.Object.Destroy(sfxObj1, 3f);
            }

            yield return new WaitForSeconds(0.1f);

            if (sfxPrefab != null)
            {
                var sfxObj2 = UnityEngine.Object.Instantiate(sfxPrefab, position, Quaternion.identity);
                var znetView2 = sfxObj2?.GetComponent<ZNetView>();
                if (znetView2 != null) UnityEngine.Object.DestroyImmediate(znetView2);
                if (sfxObj2 != null) UnityEngine.Object.Destroy(sfxObj2, 3f);
            }
        }

        private static IEnumerator ReleaseTauntAfterDelay(BaseAI ai, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (ai != null)
            {
                try
                {
                    var setAggravatedMethod = ai.GetType().GetMethod("SetAggravated");
                    setAggravatedMethod?.Invoke(ai, new object[] { false, BaseAI.AggravatedReason.Damage });

                    var setTargetMethod = ai.GetType().GetMethod("SetTarget");
                    setTargetMethod?.Invoke(ai, new object[] { null });
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[도발] 보스 도발 해제 실패: {ex.Message}");
                }
            }
        }

        public static IEnumerator TauntCooldownDisplay(Player player)
        {
            SkillBuffDisplay.Instance.ShowBuff(
                "taunt_cooldown",
                L.Get("taunt_cooldown_display"),
                tauntCooldown,
                new Color(0.8f, 0.8f, 0.8f, 1f),
                "😤"
            );

            yield return new WaitForSeconds(tauntCooldown);

            if (player == null || player.IsDead())
            {
                tauntCooldownCoroutine.Remove(player);
                yield break;
            }

            DrawFloatingText(player, L.Get("taunt_ready"), Color.green);
            tauntCooldownCoroutine.Remove(player);
        }

        public static void ApplyWarriorShoutEffect(Player player, int level)
        {
            // TODO: 실제 효과 구현
        }

        public static bool IsBossMonster(Character mob)
        {
            return mob != null && mob.m_name != null && mob.m_name.Contains("보스");
        }

        private static void ShowTauntEffectOnMonster(Character monster)
        {
            try
            {
                Vector3 effectPosition = monster.transform.position + Vector3.up * 2f;
                var znet = ZNetScene.instance;
                if (znet != null)
                {
                    var effectNames = new string[] { "fx_creature_tamed", "fx_levelup", "fx_hit_campdamage" };
                    foreach (var effectName in effectNames)
                    {
                        var effectPrefab = znet.GetPrefab(effectName);
                        if (effectPrefab != null)
                        {
                            UnityEngine.Object.Instantiate(effectPrefab, effectPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[도발 이펙트] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 도발 정리 (플레이어 사망 시)
        /// </summary>
        public static void CleanupTauntOnDeath(Player player)
        {
            try
            {
                if (tauntCooldownCoroutine.ContainsKey(player))
                {
                    if (tauntCooldownCoroutine[player] != null)
                    {
                        try { player.StopCoroutine(tauntCooldownCoroutine[player]); } catch { }
                    }
                    tauntCooldownCoroutine.Remove(player);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[도발] 정리 실패: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 이중시전 마법 발사체 충돌 처리 컴포넌트
    /// </summary>
    public class DoubleCastProjectileHandler : MonoBehaviour
    {
        private Player caster;
        private ItemDrop.ItemData weapon;
        private float damagePercent;
        private int projectileIndex;
        private bool hasHit = false;

        public void Initialize(Player player, ItemDrop.ItemData weaponData, float dmgPercent, int index)
        {
            caster = player;
            weapon = weaponData;
            damagePercent = dmgPercent;
            projectileIndex = index;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hasHit || caster == null) return;

            var character = other.GetComponent<Character>();
            if (character != null && character != caster && !character.IsPlayer())
            {
                hasHit = true;
                HandleProjectileImpact(character);
            }
        }

        private void HandleProjectileImpact(Character target)
        {
            try
            {
                PlayMagicImpactEffects(transform.position);
                ApplyMagicDamage(target, damagePercent);
                Destroy(gameObject);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 발사체 충돌 처리 오류: {ex.Message}");
            }
        }

        private void PlayMagicImpactEffects(Vector3 impactPoint)
        {
            try
            {
                var magicEffect = ZNetScene.instance?.GetPrefab("fx_icestaffprojectile_hit");
                if (magicEffect != null)
                {
                    var effectObj = UnityEngine.Object.Instantiate(magicEffect, impactPoint, Quaternion.identity);
                    var znetView = effectObj?.GetComponent<ZNetView>();
                    if (znetView != null) UnityEngine.Object.DestroyImmediate(znetView);
                    if (effectObj != null) UnityEngine.Object.Destroy(effectObj, 3f);
                }

                var magicSound = ZNetScene.instance?.GetPrefab("sfx_staff_fireball_hit");
                if (magicSound != null)
                {
                    var soundObj = UnityEngine.Object.Instantiate(magicSound, impactPoint, Quaternion.identity);
                    var znetView2 = soundObj?.GetComponent<ZNetView>();
                    if (znetView2 != null) UnityEngine.Object.DestroyImmediate(znetView2);
                    if (soundObj != null) UnityEngine.Object.Destroy(soundObj, 3f);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 충돌 효과 재생 오류: {ex.Message}");
            }
        }

        private void ApplyMagicDamage(Character target, float dmgPercent)
        {
            try
            {
                if (weapon == null) return;

                var baseDamage = weapon.GetDamage();

                var projectileDamage = new HitData.DamageTypes();
                projectileDamage.m_fire = baseDamage.m_fire * dmgPercent;
                projectileDamage.m_frost = baseDamage.m_frost * dmgPercent;
                projectileDamage.m_lightning = baseDamage.m_lightning * dmgPercent;
                projectileDamage.m_poison = baseDamage.m_poison * dmgPercent;
                projectileDamage.m_spirit = baseDamage.m_spirit * dmgPercent;

                var hitData = new HitData();
                hitData.m_damage = projectileDamage;
                hitData.m_point = target.transform.position;
                hitData.m_dir = (target.transform.position - caster.transform.position).normalized;
                hitData.m_attacker = caster.GetZDOID();
                hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;

                target.Damage(hitData);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 데미지 적용 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 지팡이 공격 시 이중 시전 버프 자동 발동 Harmony 패치
    /// </summary>
    [HarmonyPatch(typeof(Attack), "FireProjectileBurst")]
    [HarmonyPriority(Priority.High)]
    public static class StaffDualCast_Attack_FireProjectileBurst_Patch
    {
        [HarmonyPrefix]
        private static bool Prefix(Attack __instance)
        {
            try
            {
                // ✅ CRITICAL: 플레이어 공격만 처리 (몬스터/NPC 차단)
                // Attack.m_character로 실제 공격자가 로컬 플레이어인지 검증
                var attacker = Traverse.Create(__instance).Field("m_character").GetValue<Character>();
                if (attacker == null || attacker != Player.m_localPlayer) return true;

                var player = Player.m_localPlayer;
                if (player == null) return true;

                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player)) return true;

                var currentWeapon = player.GetCurrentWeapon();

                if (!SkillEffect.IsStaffDualCastReady(player)) return true;

                Vector3 attackDir = player.GetLookDir();
                SkillTreeInputListener.Instance.StartCoroutine(DelayedDualCastExecution(player, currentWeapon, attackDir));

                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 패치 오류: {ex.Message}");
                return true;
            }
        }

        private static IEnumerator DelayedDualCastExecution(Player player, ItemDrop.ItemData weapon, Vector3 attackDir)
        {
            yield return new WaitForSeconds(0.5f);

            if (player == null || player.IsDead()) yield break;

            if (SkillEffect.IsStaffDualCastReady(player))
            {
                SkillEffect.PerformStaffDualCastAttack(player, weapon, attackDir);
            }
        }
    }
}

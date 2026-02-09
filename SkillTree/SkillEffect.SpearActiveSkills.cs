using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Gui;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 창 투창 전문가 패시브 및 연공창 액티브 스킬 시스템
    /// </summary>
    public static partial class SkillEffect
    {
        // === 투창 전문가 패시브 쿨타임 ===
        private static Dictionary<Player, float> spearThrowPassiveCooldown = new Dictionary<Player, float>();

        // === 연공창 액티브 스킬 상태 ===
        private static Dictionary<Player, float> spearEnhancedThrowCooldowns = new Dictionary<Player, float>();
        public static Dictionary<Player, float> spearEnhancedThrowBuffEndTime = new Dictionary<Player, float>();

        /// <summary>
        /// 투창 전문가 패시브 쿨타임 확인
        /// </summary>
        public static bool CanUseSpearThrowPassive(Player player)
        {
            if (player == null) return false;
            float now = Time.time;
            if (spearThrowPassiveCooldown.ContainsKey(player) && now < spearThrowPassiveCooldown[player])
                return false;
            return true;
        }

        /// <summary>
        /// 투창 전문가 패시브 쿨타임 설정
        /// </summary>
        public static void SetSpearThrowPassiveCooldown(Player player)
        {
            if (player == null) return;
            float cooldown = SkillTreeConfig.SpearStep2ThrowCooldownValue;
            spearThrowPassiveCooldown[player] = Time.time + cooldown;
        }

        /// <summary>
        /// 창 연공창 액티브 스킬 (H키)
        /// </summary>
        public static void HandleSpearActiveSkill(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                // 스킬 보유 확인
                if (!HasSkill("spear_Step5_combo"))
                {
                    DrawFloatingText(player, "연공창 스킬이 필요합니다", Color.red);
                    return;
                }

                // 창 착용 확인
                if (!IsUsingSpear(player))
                {
                    DrawFloatingText(player, "창을 착용해야 합니다", Color.red);
                    return;
                }

                // 쿨타임 확인
                float now = Time.time;
                if (spearEnhancedThrowCooldowns.ContainsKey(player) && now < spearEnhancedThrowCooldowns[player])
                {
                    float remaining = spearEnhancedThrowCooldowns[player] - now;
                    DrawFloatingText(player, $"쿨타임: {Mathf.CeilToInt(remaining)}초", Color.yellow);
                    return;
                }

                // 스태미나 확인
                float maxStamina = player.GetMaxStamina();
                float requiredStamina = maxStamina * (SkillTreeConfig.SpearStep2ThrowStaminaCostValue / 100f);
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, "스태미나 부족", Color.red);
                    return;
                }

                // 스킬 발동
                ExecuteSpearEnhancedThrow(player);

                // 쿨타임 및 스태미나 소모 적용
                spearEnhancedThrowCooldowns[player] = now + SkillTreeConfig.SpearStep2ThrowCooldownValue;
                player.UseStamina(requiredStamina);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창] 오류: {ex.Message}");
            }
        }

        private static void ExecuteSpearEnhancedThrow(Player player)
        {
            try
            {
                // 버프 활성화
                float buffDuration = SkillTreeConfig.SpearStep2ThrowBuffDurationValue;
                float damageBonus = SkillTreeConfig.SpearStep6ComboDamageValue;
                spearEnhancedThrowBuffEndTime[player] = Time.time + buffDuration;

                // 실제 창 던지기 실행
                player.StartAttack(null, true);

                // 버프 표시
                SkillBuffDisplay.Instance.ShowBuff(
                    "spear_enhanced_throw",
                    "연공창",
                    buffDuration,
                    new Color(1f, 0.8f, 0.2f, 1f),
                    "🏹"
                );

                DrawFloatingText(player, $"[연공창] 강화된 투창! +{damageBonus}%", new Color(1f, 0.8f, 0.2f, 1f));
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창 실행] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 강화된 창 투사체 충돌 처리 컴포넌트
        /// </summary>
        public class EnhancedSpearProjectileHandler : MonoBehaviour
        {
            private Player thrower;
            private bool hasHit = false;

            public void Initialize(Player player) => thrower = player;

            private void OnTriggerEnter(Collider other)
            {
                if (hasHit) return;

                var character = other.GetComponent<Character>();
                if (character != null && character != thrower && !character.IsPlayer())
                {
                    hasHit = true;
                    HandleSpearImpact(character);
                }
            }

            private void HandleSpearImpact(Character target)
            {
                try
                {
                    // VFX 효과
                    SimpleVFX.Play("vfx_HitSparks", target.transform.position + Vector3.up * 1f, 1.5f);

                    // 데미지 적용
                    ApplyEnhancedSpearDamage(target, SkillTreeConfig.SpearStep2ThrowDamageValue / 100f);

                    // 범위 넉백
                    ApplyAreaKnockbackAndDamage(transform.position, target);

                    Destroy(gameObject);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[연공창] 충돌 처리 오류: {ex.Message}");
                }
            }

            private void ApplyEnhancedSpearDamage(Character target, float damageMultiplier)
            {
                try
                {
                    if (thrower?.GetCurrentWeapon() == null) return;

                    var weapon = thrower.GetCurrentWeapon();
                    var baseDamage = weapon.GetDamage();

                    var enhancedDamage = new HitData.DamageTypes();
                    enhancedDamage.m_pierce = baseDamage.m_pierce * damageMultiplier;
                    enhancedDamage.m_slash = baseDamage.m_slash * damageMultiplier;
                    enhancedDamage.m_blunt = baseDamage.m_blunt * damageMultiplier;

                    var hitData = new HitData();
                    hitData.m_damage = enhancedDamage;
                    hitData.m_point = target.transform.position;
                    hitData.m_dir = (target.transform.position - thrower.transform.position).normalized;
                    hitData.m_attacker = thrower.GetZDOID();
                    hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;

                    target.Damage(hitData);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[연공창] 피해 적용 오류: {ex.Message}");
                }
            }

            private void ApplyAreaKnockbackAndDamage(Vector3 center, Character mainTarget)
            {
                try
                {
                    float radius = SkillTreeConfig.SpearStep2ThrowRangeValue;
                    var colliders = Physics.OverlapSphere(center, radius);

                    foreach (var collider in colliders)
                    {
                        var character = collider.GetComponent<Character>();
                        if (character != null && character != thrower && !character.IsPlayer() && character != mainTarget)
                        {
                            Vector3 knockbackDir = (character.transform.position - center).normalized;
                            character.SetLookDir(knockbackDir * -1f, 0.2f);

                            var knockbackHit = new HitData();
                            knockbackHit.m_damage.m_blunt = 1f;
                            knockbackHit.m_point = character.transform.position;
                            knockbackHit.m_dir = knockbackDir;
                            knockbackHit.m_pushForce = 100f;
                            knockbackHit.m_attacker = thrower.GetZDOID();
                            character.Damage(knockbackHit);

                            ApplyEnhancedSpearDamage(character, 0.5f);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[연공창] 범위 넉백 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 창 강화 투척 정리 (플레이어 사망 시)
        /// </summary>
        public static void CleanupSpearEnhancedThrowOnDeath(Player player)
        {
            try
            {
                spearEnhancedThrowCooldowns.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[창 강화 투척] 정리 실패: {ex.Message}");
            }
        }
    }
}

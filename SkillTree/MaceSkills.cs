using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기 스킬 효과 구현 (직접 패치 방식 - Tier 2)
    /// WeaponHelper, SkillBonusCalculator 사용하여 중복 코드 제거
    /// </summary>
    public static class MaceSkills
    {
        /// <summary>
        /// 모든 둔기 데미지 보너스 계산
        /// Tier 0 (전문가 +5%) + Tier 1 (+10%) + Tier 5 DPS (+20%)
        /// </summary>
        public static float GetTotalMaceDamageBonus(string skillId = "")
        {
            return SkillBonusCalculator.CalculateTotal(
                ("mace_expert_damage", () => Mace_Config.MaceExpertDamageBonusValue),
                ("mace_Step1_damage", () => Mace_Config.MaceStep1DamageBonusValue),
                ("mace_Step5_dps", () => Mace_Config.MaceStep5DpsDamageBonusValue)
            );
        }

        /// <summary>
        /// 기절 확률 계산
        /// Tier 0 (전문가 20%) + Tier 2 (+15%)
        /// </summary>
        public static float GetTotalStunChance(string skillId = "")
        {
            return SkillBonusCalculator.CalculateTotal(
                ("mace_expert_damage", () => Mace_Config.MaceExpertStunChanceValue),
                ("mace_Step2_stun_boost", () => Mace_Config.MaceStep2StunChanceBonusValue)
            );
        }

        /// <summary>
        /// 기절 지속시간 계산
        /// Tier 0 (전문가 0.5초) + Tier 2 (+0.5초)
        /// </summary>
        public static float GetTotalStunDuration(string skillId = "")
        {
            return SkillBonusCalculator.CalculateTotal(
                ("mace_expert_damage", () => Mace_Config.MaceExpertStunDurationValue),
                ("mace_Step2_stun_boost", () => Mace_Config.MaceStep2StunDurationBonusValue)
            );
        }

        /// <summary>
        /// 회전 타격 데미지 보너스 반환
        /// Tier 3 Guard: 세컨드 어택 시 +20%
        /// </summary>
        public static float GetMaceSpinDamageBonus()
        {
            return SkillBonusCalculator.GetIfActive(
                "mace_Step3_branch_guard",
                () => Mace_Config.MaceStep3SpinDamageBonusValue
            );
        }

        /// <summary>
        /// 속공 공격속도 보너스 반환 (AnimationSpeedManager 통합)
        /// Tier 6 속공: +10%
        /// </summary>
        public static float GetMaceSokgongAttackSpeedBonus()
        {
            return SkillBonusCalculator.GetIfActive(
                "mace_Step6_grandmaster",
                () => Mace_Config.MaceStep6AttackSpeedBonusValue
            );
        }

        // 마지막 세컨드 어택 시간 추적 (회전 타격용)
        internal static Dictionary<Player, float> s_lastSpinTime = new Dictionary<Player, float>();
    }

    // ===== Harmony 패치 =====

    /// <summary>
    /// 둔기 데미지 증가 패치 (공격 전문가 패턴 참조)
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_DamagePatch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (hit.GetAttacker() is not Player attacker) return;
                if (!WeaponHelper.IsUsingMace(attacker)) return;

                float damageBonus = MaceSkills.GetTotalMaceDamageBonus();
                if (damageBonus > 0f)
                {
                    float totalDamageMultiplier = 1f + damageBonus / 100f;
                    hit.m_damage.m_blunt *= totalDamageMultiplier;
                    Plugin.Log.LogDebug($"[둔기 데미지] 보너스 +{damageBonus}% 적용 (blunt, 배율: {totalDamageMultiplier:F2})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 데미지 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 기절 효과 패치
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_StunPatch
    {
        static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                if (hit.GetAttacker() is not Player attacker) return;
                if (!WeaponHelper.IsUsingMace(attacker)) return;

                float stunChance = MaceSkills.GetTotalStunChance();
                float stunDuration = MaceSkills.GetTotalStunDuration();

                if (stunChance <= 0f || stunDuration <= 0f) return;
                if (UnityEngine.Random.Range(0f, 100f) > stunChance) return;

                if (__instance != null && !__instance.IsDead())
                {
                    __instance.Stagger(Vector3.zero);
                    Plugin.Log.LogDebug($"[둔기 기절] 확률 {stunChance}% 발동! 지속시간: {stunDuration}초");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 기절 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 밀어내기 효과 패치
    /// Tier 4: 막기 미사용 상태에서 피격 시 30% 확률로 공격자를 5m 밀어냄
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_KnockbackPatch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                // 피격자가 플레이어인지 확인
                if (__instance is not Player player) return;
                if (!WeaponHelper.IsUsingMace(player)) return;
                if (!SkillBonusCalculator.IsSkillActive("mace_Step4_push")) return;
                if (player.IsBlocking()) return;

                // 공격자가 몬스터인지 확인
                Character attacker = hit.GetAttacker() as Character;
                if (attacker == null || attacker.IsPlayer()) return;

                float knockbackChance = Mace_Config.MaceStep4KnockbackChanceValue;
                if (UnityEngine.Random.Range(0f, 100f) > knockbackChance) return;

                // 공격자를 5m 밀어냄 (pushForce 80f ≈ 5m)
                Vector3 pushDir = (attacker.transform.position - player.transform.position).normalized;
                var pushHit = new HitData();
                pushHit.m_pushForce = 80f;
                pushHit.m_dir = pushDir;
                pushHit.m_point = attacker.GetCenterPoint();
                pushHit.m_blockable = false;
                pushHit.m_dodgeable = false;
                attacker.Damage(pushHit);

                Plugin.Log.LogDebug($"[둔기 밀어내기] 확률 {knockbackChance}% 발동! 공격자를 밀어냄");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 밀어내기 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 체력 증가 패치
    /// Tier 5 Tank: 체력 +25%
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.GetMaxHealth))]
    public static class MaceSkills_HealthPatch
    {
        static void Postfix(Character __instance, ref float __result)
        {
            try
            {
                if (__instance is not Player player) return;
                if (!WeaponHelper.IsUsingMace(player)) return;

                if (SkillBonusCalculator.IsSkillActive("mace_Step5_tank"))
                {
                    float healthBonus = Mace_Config.MaceStep5TankHealthBonusValue;
                    float baseHealth = __result;
                    float bonusHealth = baseHealth * (healthBonus / 100f);
                    __result += bonusHealth;

                    Plugin.Log.LogDebug($"[둔기 탱커] 체력 +{healthBonus}%: {baseHealth:F0} → {__result:F0} (+{bonusHealth:F0})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 체력 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 둔기 받는 데미지 감소 패치
    /// Tier 5 Tank: 받는 데미지 -10%
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_DamageReductionPatch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance is not Player player) return;
                if (!WeaponHelper.IsUsingMace(player)) return;

                if (SkillBonusCalculator.IsSkillActive("mace_Step5_tank"))
                {
                    float damageReduction = Mace_Config.MaceStep5TankDamageReductionValue;
                    float reductionMultiplier = 1f - (damageReduction / 100f);

                    hit.m_damage.m_blunt *= reductionMultiplier;
                    hit.m_damage.m_pierce *= reductionMultiplier;
                    hit.m_damage.m_slash *= reductionMultiplier;
                    hit.m_damage.m_chop *= reductionMultiplier;
                    hit.m_damage.m_pickaxe *= reductionMultiplier;
                    hit.m_damage.m_fire *= reductionMultiplier;
                    hit.m_damage.m_frost *= reductionMultiplier;
                    hit.m_damage.m_lightning *= reductionMultiplier;
                    hit.m_damage.m_poison *= reductionMultiplier;
                    hit.m_damage.m_spirit *= reductionMultiplier;

                    Plugin.Log.LogDebug($"[둔기 탱커] 받는 데미지 -{damageReduction}% 감소 (배율: {reductionMultiplier:F2})");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[둔기 데미지 감소 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 회전 타격 - 폴암 세컨드 모션 적용 패치
    /// Humanoid.StartAttack Prefix에서 둔기의 m_secondaryAttack을 폴암 것으로 임시 교체
    /// → 한손 둔기: 기존 세컨드 모션 → 폴암 회전 모션 교체
    /// → 양손 둔기: 없던 세컨드 모션 → 폴암 회전 모션 강제 적용
    /// </summary>
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
    public static class MaceSkills_SpinAnimation_Patch
    {
        private static Attack s_polearmSecondaryCache = null;
        private static Attack s_savedSecondary = null;
        private static ItemDrop.ItemData.SharedData s_savedSharedData = null;

        static void Prefix(Humanoid __instance, bool secondaryAttack)
        {
            try
            {
                if (!secondaryAttack) return;
                if (__instance is not Player player) return;
                if (!WeaponHelper.IsUsingMace(player)) return;
                if (!SkillBonusCalculator.IsSkillActive("mace_Step3_branch_guard")) return;

                Attack polearmSecondary = GetCachedPolearmSecondary();
                if (polearmSecondary == null) return;

                var weapon = player.GetCurrentWeapon();
                if (weapon?.m_shared == null) return;

                s_savedSharedData = weapon.m_shared;
                s_savedSecondary = weapon.m_shared.m_secondaryAttack;
                weapon.m_shared.m_secondaryAttack = polearmSecondary;
                Plugin.Log.LogDebug("[둔기 회전 모션] 폴암 세컨드 모션으로 임시 교체");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[MaceSkills_SpinAnimation_Patch.Prefix] 오류: {ex.Message}");
            }
        }

        static void Postfix()
        {
            if (s_savedSharedData == null) return;
            s_savedSharedData.m_secondaryAttack = s_savedSecondary;
            s_savedSharedData = null;
            s_savedSecondary = null;
        }

        private static Attack GetCachedPolearmSecondary()
        {
            if (s_polearmSecondaryCache != null) return s_polearmSecondaryCache;
            if (ObjectDB.instance == null) return null;

            string[] candidates = { "AtgeirBronze", "AtgeirBlackmetal", "Atgeir" };
            foreach (var prefabName in candidates)
            {
                var prefab = ObjectDB.instance.GetItemPrefab(prefabName);
                if (prefab == null) continue;
                var item = prefab.GetComponent<ItemDrop>();
                var secondary = item?.m_itemData?.m_shared?.m_secondaryAttack;
                if (secondary == null) continue;
                s_polearmSecondaryCache = secondary;
                Plugin.Log.LogInfo($"[둔기 회전 모션] 폴암 세컨드 공격 캐시 완료: {prefabName}");
                return s_polearmSecondaryCache;
            }
            Plugin.Log.LogWarning("[둔기 회전 모션] 폴암 프리팹을 찾지 못했습니다 (AtgeirBronze/AtgeirBlackmetal/Atgeir)");
            return null;
        }
    }

    /// <summary>
    /// 회전 타격 - 세컨드 어택 감지 패치
    /// Attack.Start에서 특수 공격 감지 후 시간 기록
    /// </summary>
    [HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
    public static class MaceSkills_SpinDetect_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.High)]
        static void Postfix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                if (!WeaponHelper.IsUsingMace(player)) return;
                if (!SkillBonusCalculator.IsSkillActive("mace_Step3_branch_guard")) return;

                // 세컨드 어택(휠 마우스 / SecondaryAttack 버튼) 감지
                bool isSecondaryAttack = ZInput.GetButton("SecondaryAttack") || Input.GetMouseButton(2);
                if (!isSecondaryAttack) return;

                MaceSkills.s_lastSpinTime[player] = Time.time;
                Plugin.Log.LogDebug("[둔기 회전 타격] 세컨드 어택 감지 - 보너스 준비");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[MaceSkills_SpinDetect_Patch] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 회전 타격 - 데미지 적용 패치
    /// 세컨드 어택 후 0.5초 이내 공격 시 +20% 데미지 및 7m AOE 적용
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MaceSkills_SpinStrikePatch
    {
        private static bool s_applyingAoe = false;

        [HarmonyPriority(HarmonyLib.Priority.High)]
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (s_applyingAoe) return;
                if (__instance == null || __instance.IsPlayer()) return;

                if (hit.GetAttacker() is not Player attacker) return;
                if (!WeaponHelper.IsUsingMace(attacker)) return;
                if (!SkillBonusCalculator.IsSkillActive("mace_Step3_branch_guard")) return;

                // 세컨드 어택 시간 체크 (0.5초 이내)
                if (!MaceSkills.s_lastSpinTime.TryGetValue(attacker, out float t)) return;
                if (Time.time - t > 0.5f) return;

                float spinBonus = MaceSkills.GetMaceSpinDamageBonus();
                if (spinBonus <= 0f) return;

                // 데미지 보너스 적용 (+20%)
                float multiplier = 1f + spinBonus / 100f;
                hit.m_damage.m_blunt *= multiplier;

                // 7m AOE 적용 (재귀 방지 플래그 사용)
                float spinRange = Mace_Config.MaceStep3SpinRangeValue;
                s_applyingAoe = true;
                try
                {
                    var mobs = Character.GetAllCharacters().Where(c =>
                        c != __instance &&
                        c.IsMonsterFaction(0f) &&
                        Vector3.Distance(c.transform.position, attacker.transform.position) < spinRange
                    ).ToList();

                    foreach (var mob in mobs)
                    {
                        var aoeHit = new HitData();
                        aoeHit.m_damage.m_blunt = hit.m_damage.m_blunt;
                        aoeHit.m_dir = (mob.transform.position - attacker.transform.position).normalized;
                        aoeHit.m_point = mob.GetCenterPoint();
                        aoeHit.m_attacker = attacker.GetZDOID();
                        aoeHit.SetAttacker(attacker);
                        aoeHit.m_blockable = false;
                        aoeHit.m_dodgeable = false;
                        mob.Damage(aoeHit);
                    }
                }
                finally
                {
                    s_applyingAoe = false;
                }

                // 시간 리셋 (중복 발동 방지)
                MaceSkills.s_lastSpinTime[attacker] = -999f;

                // 텍스트 표시 (패시브: VFX 금지)
                SkillEffect.DrawFloatingText(attacker, "🌀 " + L.Get("spin_attack_hit", spinBonus));
                Plugin.Log.LogDebug($"[둔기 회전 타격] +{spinBonus}% 데미지 적용, AOE {spinRange}m");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[MaceSkills_SpinStrikePatch] 오류: {ex.Message}");
            }
        }
    }
}

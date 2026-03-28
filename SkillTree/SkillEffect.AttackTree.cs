using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.SkillTree.CriticalSystem;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    // ===== 공격 전문가 트리 상태 추적 =====
    public static class AttackTreeTracker
    {
        // === 선빵 상태 ===
        public static Dictionary<Player, float> openerStartTime     = new Dictionary<Player, float>();
        public static Dictionary<Player, float> openerLastUsedTime  = new Dictionary<Player, float>();
        public static Dictionary<Player, bool>  firstMeleeUsed      = new Dictionary<Player, bool>();
        public static Dictionary<Player, bool>  firstBowUsed        = new Dictionary<Player, bool>();
        public static Dictionary<Player, bool>  firstCrossbowUsed   = new Dictionary<Player, bool>();
        public static Dictionary<Player, bool>  firstMagicUsed      = new Dictionary<Player, bool>();

        // === 추격전 상태 ===
        public static Dictionary<Player, bool>  pursuitTriggered    = new Dictionary<Player, bool>();

        // === 난전 스택 상태 ===
        public static Dictionary<Player, int>   frenzyStack         = new Dictionary<Player, int>();
        public static Dictionary<Player, int>   frenzyHitCount      = new Dictionary<Player, int>();
        public static Dictionary<Player, float> frenzyLastHitTime   = new Dictionary<Player, float>();
        public static Dictionary<Player, bool>  frenzyMaxReached    = new Dictionary<Player, bool>();

        // 선빵 버프 활성 여부
        public static bool IsOpenerActive(Player p)
        {
            if (!openerStartTime.TryGetValue(p, out float t)) return false;
            return Time.time - t < Attack_Config.AtkOpenerDurationValue;
        }

        // 선빵→추격 연쇄 윈도우 여부 (5초)
        public static bool IsOpenerChainActive(Player p)
        {
            if (!openerStartTime.TryGetValue(p, out float t)) return false;
            return Time.time - t < Attack_Config.AtkPursuitChainWindowValue;
        }

        // 선빵 쿨다운 완료 여부
        public static bool IsOpenerReady(Player p)
        {
            if (!openerLastUsedTime.TryGetValue(p, out float t)) return true;
            return Time.time - t >= Attack_Config.AtkOpenerCooldownValue;
        }

        // 선빵 발동 (첫 타격 시)
        public static void TriggerOpener(Player p)
        {
            openerStartTime[p]    = Time.time;
            openerLastUsedTime[p] = Time.time;
            firstMeleeUsed[p]     = false;
            firstBowUsed[p]       = false;
            firstCrossbowUsed[p]  = false;
            firstMagicUsed[p]     = false;
            pursuitTriggered[p]   = false;
        }

        // 난전 스택 업데이트 (3히트마다 +1스택)
        public static void UpdateFrenzyStack(Player p)
        {
            float now = Time.time;
            if (!frenzyHitCount.ContainsKey(p))  frenzyHitCount[p]  = 0;
            if (!frenzyStack.ContainsKey(p))      frenzyStack[p]     = 0;
            if (!frenzyLastHitTime.ContainsKey(p)) frenzyLastHitTime[p] = 0f;
            if (!frenzyMaxReached.ContainsKey(p)) frenzyMaxReached[p] = false;

            // 8초 내 연속 공격 아닐 경우 초기화
            if (now - frenzyLastHitTime[p] > 8f)
            {
                frenzyHitCount[p]  = 0;
                frenzyStack[p]     = 0;
                frenzyMaxReached[p] = false;
            }

            frenzyLastHitTime[p] = now;
            frenzyHitCount[p]++;

            int hitsPerStack = Attack_Config.AtkFrenzyHitsPerStackValue;
            int maxStacks    = Attack_Config.AtkFrenzyMaxStacksValue;

            if (frenzyHitCount[p] >= hitsPerStack && frenzyStack[p] < maxStacks)
            {
                frenzyStack[p]++;
                frenzyHitCount[p] = 0;
                if (frenzyStack[p] >= maxStacks)
                    frenzyMaxReached[p] = true;
            }
        }
    }

    // ===== 공격 전문가 종합 데미지 보너스 패치 =====
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_AttackTree_Patch
    {
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (!(hit.GetAttacker() is Player player)) return;
                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                float totalDamageMultiplier = 1f;
                var currentWeapon = player.GetCurrentWeapon();

                bool isMelee     = WeaponHelper.IsUsingMeleeWeapon(player);
                bool isBow       = WeaponHelper.IsUsingBow(player);
                bool isCrossbow  = WeaponHelper.IsUsingCrossbow(player);
                bool isStaff     = WeaponHelper.IsUsingStaffOrWand(player);

                // ──────────────────────────────────────────────
                // 버서커 분노 데미지 보너스 (기존 유지)
                // ──────────────────────────────────────────────
                if (BerserkerSkills.IsPlayerInRage(player))
                {
                    float rageDmg = BerserkerSkills.GetRageDamageBonus(player);
                    if (rageDmg > 0f)
                    {
                        if (currentWeapon != null)
                        {
                            bool isRangedOrSpearCombo =
                                isBow || isCrossbow || isStaff ||
                                (WeaponHelper.IsUsingSpear(player) &&
                                 SkillEffect.spearEnhancedThrowBuffEndTime.TryGetValue(player, out float se) &&
                                 Time.time < se);
                            if (isRangedOrSpearCombo) rageDmg *= 0.3f;
                        }
                        totalDamageMultiplier *= 1f + rageDmg / 100f;
                        BerserkerSkills.CreateMonsterHitEffect(__instance);
                        if (Random.Range(0f, 1f) < 0.15f)
                            SkillEffect.ShowSkillEffectText(player, "🔥 " + L.Get("rage_bonus", $"{rageDmg:F0}"),
                                new Color(1f, 0.2f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                    }
                }

                // ──────────────────────────────────────────────
                // Tier 0: 공격 전문가 루트 (+3%)
                // ──────────────────────────────────────────────
                if (manager.GetSkillLevel("attack_root") > 0)
                    totalDamageMultiplier *= 1f + Attack_Config.AttackRootDamageBonusValue / 100f;

                // ──────────────────────────────────────────────
                // Tier 1: 선빵 발동 감지 + 데미지 보너스 적용
                // ──────────────────────────────────────────────
                if (manager.GetSkillLevel("atk_opener") > 0)
                {
                    // 쿨다운 완료 시 첫 타격에 선빵 발동
                    if (AttackTreeTracker.IsOpenerReady(player))
                    {
                        AttackTreeTracker.TriggerOpener(player);
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_activated"),
                            new Color(1f, 0.7f, 0.1f), SkillEffect.SkillEffectTextType.Critical);
                    }

                    if (AttackTreeTracker.IsOpenerActive(player))
                        totalDamageMultiplier *= 1f + Attack_Config.AtkOpenerDamageBonusValue / 100f;
                }

                // ──────────────────────────────────────────────
                // Tier 2: 무기별 선빵 특화
                // ──────────────────────────────────────────────
                bool openerActive = AttackTreeTracker.IsOpenerActive(player);

                // 근접: 마무리 예열 — 선빵 윈도우 내 첫 근접 타격 +20%
                {
                    bool meleeUsed = AttackTreeTracker.firstMeleeUsed.TryGetValue(player, out var mu) && mu;
                    if (openerActive && isMelee && manager.GetSkillLevel("atk_opener_melee") > 0 && !meleeUsed)
                    {
                        AttackTreeTracker.firstMeleeUsed[player] = true;
                        totalDamageMultiplier *= 1f + Attack_Config.AtkOpenerMeleeFinisherBonusValue / 100f;
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_melee_activated"),
                            new Color(1f, 0.5f, 0.1f), SkillEffect.SkillEffectTextType.Combat);
                    }
                }

                // 활: 선빵 윈도우 내 첫발 크리 확정
                {
                    bool bowUsed = AttackTreeTracker.firstBowUsed.TryGetValue(player, out var bu) && bu;
                    if (openerActive && isBow && manager.GetSkillLevel("atk_opener_bow") > 0 && !bowUsed)
                    {
                        AttackTreeTracker.firstBowUsed[player] = true;
                        float critBonus = CriticalDamage.CalculateCritDamageMultiplier(player, Skills.SkillType.Bows);
                        CriticalDamage.ApplyCriticalDamage(player, ref hit, critBonus, Skills.SkillType.Bows);
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_bow_activated"),
                            new Color(0.2f, 1f, 0.4f), SkillEffect.SkillEffectTextType.Critical);
                    }
                }

                // 석궁: 첫 볼트 +50%
                {
                    bool xbowUsed = AttackTreeTracker.firstCrossbowUsed.TryGetValue(player, out var xu) && xu;
                    if (openerActive && isCrossbow && manager.GetSkillLevel("atk_opener_crossbow") > 0 && !xbowUsed)
                    {
                        AttackTreeTracker.firstCrossbowUsed[player] = true;
                        totalDamageMultiplier *= 1f + Attack_Config.AtkOpenerCrossbowFirstShotBonusValue / 100f;
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_crossbow_activated"),
                            new Color(0.3f, 0.7f, 1f), SkillEffect.SkillEffectTextType.Critical);
                    }
                }

                // 기존 석궁 "단 한 발" 스킬
                if (isCrossbow && SkillEffect.CheckAndConsumeCrossbowOneShot(player, __instance))
                {
                    float oneShotBonus = Crossbow_Config.CrossbowOneShotDamageBonusValue / 100f;
                    totalDamageMultiplier *= 1f + oneShotBonus;
                    SkillEffect.ShowSkillEffectText(player, "🎯 " + L.Get("one_shot", Crossbow_Config.CrossbowOneShotDamageBonusValue),
                        new Color(1f, 0.8f, 0f), SkillEffect.SkillEffectTextType.Critical);
                }

                // 마법: 첫 마법 공격 스태거 확정
                {
                    bool magicUsed = AttackTreeTracker.firstMagicUsed.TryGetValue(player, out var mgu) && mgu;
                    if (openerActive && isStaff && manager.GetSkillLevel("atk_opener_magic") > 0 && !magicUsed)
                    {
                        AttackTreeTracker.firstMagicUsed[player] = true;
                        hit.m_staggerMultiplier = 10f;
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_opener_magic_activated"),
                            new Color(0.8f, 0.2f, 1f), SkillEffect.SkillEffectTextType.Critical);
                    }
                }

                // ──────────────────────────────────────────────
                // Tier 3: 추격전 — 이동/도주 중인 적 데미지 보너스
                // ──────────────────────────────────────────────
                if (manager.GetSkillLevel("atk_pursuit") > 0)
                {
                    bool targetMoving = __instance.GetVelocity().magnitude > 0.5f;
                    if (targetMoving)
                    {
                        bool chainActive = AttackTreeTracker.IsOpenerChainActive(player);
                        float pursuitBonus = chainActive
                            ? Attack_Config.AtkPursuitChainDamageBonusValue
                            : Attack_Config.AtkPursuitDamageBonusValue;
                        totalDamageMultiplier *= 1f + pursuitBonus / 100f;

                        if (!AttackTreeTracker.pursuitTriggered.TryGetValue(player, out bool pt) || !pt)
                        {
                            AttackTreeTracker.pursuitTriggered[player] = true;
                            if (chainActive)
                                SkillEffect.ShowSkillEffectText(player, L.Get("atk_pursuit_chain"),
                                    new Color(0.3f, 0.9f, 1f), SkillEffect.SkillEffectTextType.Combat);
                        }
                    }
                }

                // ──────────────────────────────────────────────
                // Tier 4: atk_frenzy_trigger — 주변 3m 적 2명↑ 시 난전 진입 가속
                // (스태미나 감소는 UseStamina 패치에서 별도 처리)
                // ──────────────────────────────────────────────

                // ──────────────────────────────────────────────
                // Tier 5: 난전 — 연속 히트 스택 데미지
                // ──────────────────────────────────────────────
                if (manager.GetSkillLevel("atk_frenzy") > 0)
                {
                    AttackTreeTracker.UpdateFrenzyStack(player);

                    int stack     = AttackTreeTracker.frenzyStack.TryGetValue(player, out int s) ? s : 0;
                    bool pursuit  = AttackTreeTracker.pursuitTriggered.TryGetValue(player, out bool pt2) && pt2;
                    float bonusPerStack = pursuit
                        ? Attack_Config.AtkFrenzyStackBonusChainValue
                        : Attack_Config.AtkFrenzyStackBonusBaseValue;

                    if (stack > 0)
                    {
                        totalDamageMultiplier *= 1f + (bonusPerStack * stack) / 100f;
                        if (Random.Range(0f, 1f) < 0.12f)
                            SkillEffect.ShowSkillEffectText(player,
                                L.Get("atk_frenzy_stack", stack, (int)(bonusPerStack * stack)),
                                new Color(1f, 0.3f, 0.1f), SkillEffect.SkillEffectTextType.Combat);
                    }
                }

                // ──────────────────────────────────────────────
                // 연속베기: 검 콤보 슬래시 버프 (기존 유지)
                // ──────────────────────────────────────────────
                if (isMelee && SkillEffect.IsUsingSword(player) &&
                    SkillEffect.swordComboSlashBuffEndTime.TryGetValue(player, out float comboEnd) &&
                    Time.time < comboEnd)
                {
                    totalDamageMultiplier *= 1f + SkillTreeConfig.SwordStep2ComboSlashBonusValue / 100f;
                }

                // ──────────────────────────────────────────────
                // Tier 6: 마무리 최종 (난전 Max 스택 시 ×1.3 증폭 포함)
                // ──────────────────────────────────────────────
                bool frenzyMax = AttackTreeTracker.frenzyMaxReached.TryGetValue(player, out bool fm) && fm;
                float tier6Amp = (frenzyMax && manager.GetSkillLevel("atk_frenzy") > 0)
                    ? Attack_Config.AtkFrenzyTier6AmplifierValue
                    : 1f;

                // 연속 근접의 대가: 한손 무기 상시 +5% (난전 Max 시 ×1.3)
                if (manager.GetSkillLevel("atk_finisher_melee") > 0 && currentWeapon != null)
                {
                    bool isOneHanded = (currentWeapon.m_shared.m_skillType == Skills.SkillType.Swords &&
                                        currentWeapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.TwoHandedWeapon) ||
                                       currentWeapon.m_shared.m_skillType == Skills.SkillType.Knives ||
                                       WeaponHelper.IsUsingOneHandedMace(player) ||
                                       (currentWeapon.m_shared.m_skillType == Skills.SkillType.Axes &&
                                        currentWeapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.TwoHandedWeapon);
                    if (isOneHanded)
                        totalDamageMultiplier *= (1f + Attack_Config.AttackFinisherMeleeBonusValue / 100f) * tier6Amp;
                }

                // 양손 분쇄: 양손 무기 +10% (난전 Max 시 ×1.3)
                if (manager.GetSkillLevel("atk_twohand_crush") > 0 && currentWeapon != null)
                {
                    if (IsTwoHandedWeapon(currentWeapon))
                        totalDamageMultiplier *= (1f + Attack_Config.AttackTwoHandedBonusValue / 100f) * tier6Amp;
                }

                // 속성 공격: 활/지팡이 속성 데미지 +5% (난전 Max 시 ×1.3)
                if (manager.GetSkillLevel("atk_staff_mage") > 0 && currentWeapon != null && (isBow || isStaff))
                {
                    float elemBonus = (1f + Attack_Config.AttackStaffElementalValue / 100f) * tier6Amp;
                    hit.m_damage.m_fire      *= elemBonus;
                    hit.m_damage.m_frost     *= elemBonus;
                    hit.m_damage.m_lightning *= elemBonus;
                    hit.m_damage.m_poison    *= elemBonus;
                    hit.m_damage.m_spirit    *= elemBonus;
                    if (frenzyMax && Random.Range(0f, 1f) < 0.15f)
                        SkillEffect.ShowSkillEffectText(player, L.Get("atk_frenzy_max_elemental"),
                            new Color(1f, 0.5f, 0f), SkillEffect.SkillEffectTextType.Critical);
                }

                // 약점 공격: 크리티컬 피해 +12% (Critical.cs에서 처리, tier6Amp는 전달)
                // CriticalDamage.AttackTreeTier6Amp = tier6Amp; // 필요시 전달 가능

                // ──────────────────────────────────────────────
                // 제작 전문가 장인의 축복 버프 (기존 유지)
                // ──────────────────────────────────────────────
                if (ProducerSkills.IsProducerBuffActive(player))
                    totalDamageMultiplier *= 1f + Producer_Config.ProducerBuff_AttackBonusValue / 100f;

                // ──────────────────────────────────────────────
                // 총 데미지 배율 적용
                // ──────────────────────────────────────────────
                if (totalDamageMultiplier > 1f)
                {
                    hit.m_damage.m_damage   *= totalDamageMultiplier;
                    hit.m_damage.m_blunt    *= totalDamageMultiplier;
                    hit.m_damage.m_slash    *= totalDamageMultiplier;
                    hit.m_damage.m_pierce   *= totalDamageMultiplier;
                    hit.m_damage.m_chop     *= totalDamageMultiplier;
                    hit.m_damage.m_pickaxe  *= totalDamageMultiplier;
                    hit.m_damage.m_fire     *= totalDamageMultiplier;
                    hit.m_damage.m_frost    *= totalDamageMultiplier;
                    hit.m_damage.m_lightning *= totalDamageMultiplier;
                    hit.m_damage.m_poison   *= totalDamageMultiplier;
                    hit.m_damage.m_spirit   *= totalDamageMultiplier;

                    if (Random.Range(0f, 1f) < 0.02f)
                    {
                        bool dbgPursuit = AttackTreeTracker.pursuitTriggered.TryGetValue(player, out var pp) && pp;
                        int  dbgStack   = AttackTreeTracker.frenzyStack.TryGetValue(player, out var fs) ? fs : 0;
                        Plugin.Log.LogInfo($"[공격 트리] 총 배율: {totalDamageMultiplier:F2}x " +
                            $"(선빵={openerActive}, 추격={dbgPursuit}, 난전스택={dbgStack}, MaxAmplify={frenzyMax})");
                    }
                }

                // ──────────────────────────────────────────────
                // 커스텀 치명타 시스템 (기존 유지)
                // ──────────────────────────────────────────────
                if (currentWeapon != null)
                {
                    var skillType = currentWeapon.m_shared.m_skillType;
                    float critChance = Critical.CalculateCritChance(player, skillType);
                    if (Critical.RollCritical(critChance))
                    {
                        float critDmg = CriticalDamage.CalculateCritDamageMultiplier(player, skillType);
                        // 난전 Max 시 약점 공격도 ×1.3
                        if (frenzyMax && manager.GetSkillLevel("atk_crit_dmg") > 0)
                            critDmg *= tier6Amp;
                        CriticalDamage.ApplyCriticalDamage(player, ref hit, critDmg, skillType);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[공격 트리] 데미지 패치 오류: {ex.Message}");
            }
        }

        private static bool IsTwoHandedWeapon(ItemDrop.ItemData weapon)
        {
            if (weapon?.m_shared == null) return false;
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                return true;
            return weapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Polearms ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Spears;
        }
    }

    // ===== 양손 분쇄 GetDamage 패치 (기존 유지) =====
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class SkillTree_ItemData_GetDamage_TwoHandedCrush_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            try
            {
                if (__instance?.m_shared == null) return;
                if (SkillEffect.HasSkill("atk_twohand_crush") &&
                    __instance.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                {
                    float m = 1f + Attack_Config.AttackTwoHandedBonusValue / 100f;
                    __result.m_damage *= m;
                    __result.m_blunt  *= m;
                    __result.m_slash  *= m;
                    __result.m_pierce *= m;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[양손 분쇄] GetDamage 패치 오류: {ex.Message}");
            }
        }
    }
}

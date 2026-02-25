using System;
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
    // ===== 공격 전문가 트리 상태 추적 (통합됨) =====
    public static class AttackTreeTracker
    {
        public static Dictionary<Player, int> meleeComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> meleeLastHitTime = new Dictionary<Player, float>();
    }

    // ===== 공격 전문가 종합 데미지 보너스 패치 =====
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_AttackTree_Patch
    {
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                // 공격자가 플레이어인지 확인
                if (hit.GetAttacker() is Player player)
                {
                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    float totalDamageMultiplier = 1f;
                    bool showEffect = false;
                    bool isAttackTreeEffect = false; // 공격 전문가 스킬 효과인지 추적
                    bool showAttackRootEffect = false; // attack_root 스킬 전용 효과 표시 플래그

                    // === 버서커 분노 데미지 보너스 ===
                    if (BerserkerSkills.IsPlayerInRage(player))
                    {
                        float rageDamageBonus = BerserkerSkills.GetRageDamageBonus(player);
                        if (rageDamageBonus > 0f)
                        {
                            float rageMultiplier = 1f + (rageDamageBonus / 100f);
                            totalDamageMultiplier *= rageMultiplier;
                            showEffect = true;

                            // 몬스터 적중시 flash_round_ellow 이팩트 생성
                            BerserkerSkills.CreateMonsterHitEffect(__instance);

                            // 시각적 효과 (가끔씩만 표시)
                            if (UnityEngine.Random.Range(0f, 1f) < 0.15f)
                            {
                                SkillEffect.ShowSkillEffectText(player, "🔥 " + L.Get("rage_bonus", $"{rageDamageBonus:F0}"),
                                    new Color(1f, 0.2f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                            }
                        }
                    }

                    // 공격 전문가 루트: 모든 공격력 +[CONFIG]% (물리, 속성)
                    if (manager.GetSkillLevel("attack_root") > 0)
                    {
                        float bonus = SkillTreeConfig.AttackRootDamageBonusValue / 100f;
                        totalDamageMultiplier *= (1f + bonus);
                        isAttackTreeEffect = true;
                        showAttackRootEffect = true; // attack_root 전용 플래그 설정
                    }

                    // 1단계: 기본 공격 - 물리/속성 데미지 직접 증가 (MMO 독립)
                    if (manager.GetSkillLevel("atk_base") > 0)
                    {
                        float physicalBonus = SkillTreeConfig.AttackBasePhysicalDamageValue;
                        float elementalBonus = SkillTreeConfig.AttackBaseElementalDamageValue;

                        // 물리 데미지 증가 (무기가 해당 타입 보유 시만)
                        if (hit.m_damage.m_blunt > 0) hit.m_damage.m_blunt += physicalBonus;
                        if (hit.m_damage.m_slash > 0) hit.m_damage.m_slash += physicalBonus;
                        if (hit.m_damage.m_pierce > 0) hit.m_damage.m_pierce += physicalBonus;

                        // 속성 데미지 증가 (무기가 해당 타입 보유 시만)
                        if (hit.m_damage.m_fire > 0) hit.m_damage.m_fire += elementalBonus;
                        if (hit.m_damage.m_frost > 0) hit.m_damage.m_frost += elementalBonus;
                        if (hit.m_damage.m_lightning > 0) hit.m_damage.m_lightning += elementalBonus;
                        if (hit.m_damage.m_poison > 0) hit.m_damage.m_poison += elementalBonus;
                        if (hit.m_damage.m_spirit > 0) hit.m_damage.m_spirit += elementalBonus;

                        isAttackTreeEffect = true;
                    }

                    // 2단계: 무기별 특화 ([CONFIG]% 확률로 +[CONFIG]% 추가 피해)
                    var currentWeapon = player.GetCurrentWeapon();
                    
                    // 무기 타입 변수들을 상위 스코프에 정의
                    bool isMelee = false;
                    bool isBow = false;
                    bool isCrossbow = false;
                    bool isStaff = false;
                    
                    if (currentWeapon != null)
                    {
                        isMelee = currentWeapon.m_shared.m_skillType == Skills.SkillType.Swords ||
                                  currentWeapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                                  currentWeapon.m_shared.m_skillType == Skills.SkillType.Knives ||
                                  currentWeapon.m_shared.m_skillType == Skills.SkillType.Spears ||
                                  currentWeapon.m_shared.m_skillType == Skills.SkillType.Polearms;
                        
                        isBow = currentWeapon.m_shared.m_skillType == Skills.SkillType.Bows;
                        isCrossbow = currentWeapon.m_shared.m_skillType == Skills.SkillType.Crossbows;
                        isStaff = currentWeapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic;

                        // 근접 특화
                        if (isMelee && manager.GetSkillLevel("atk_melee_bonus") > 0 && 
                            UnityEngine.Random.Range(0f, 100f) < SkillTreeConfig.AttackMeleeBonusChanceValue)
                        {
                            float bonus = SkillTreeConfig.AttackMeleeBonusDamageValue / 100f;
                            totalDamageMultiplier *= (1f + bonus);
                            showEffect = true;
                            isAttackTreeEffect = true;
                            SkillEffect.ShowSkillEffectText(player, "💥 " + L.Get("melee_specialization"),
                                new Color(1f, 0.3f, 0.3f), SkillEffect.SkillEffectTextType.Combat);
                        }

                        // 활 특화
                        if (isBow && manager.GetSkillLevel("atk_bow_bonus") > 0 && 
                            UnityEngine.Random.Range(0f, 100f) < SkillTreeConfig.AttackBowBonusChanceValue)
                        {
                            float bonus = SkillTreeConfig.AttackBowBonusDamageValue / 100f;
                            totalDamageMultiplier *= (1f + bonus);
                            showEffect = true;
                            isAttackTreeEffect = true;
                            SkillEffect.ShowSkillEffectText(player, "🏹 " + L.Get("bow_specialization"),
                                new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Combat);
                        }

                        // 활 집중 사격 - 치명타 확률 증가 (치명타 시스템 통합)
                        if (isBow && manager.GetSkillLevel("bow_Step2_focus") > 0)
                        {
                            float critChance = SkillTreeConfig.BowStep2FocusCritBonusValue;
                            // 치명타 시스템(Critical.cs)과 통합
                            float totalCritChance = Critical.CalculateCritChance(player, Skills.SkillType.Bows) + critChance;

                            if (UnityEngine.Random.Range(0f, 100f) < totalCritChance)
                            {
                                // 치명타 데미지(CriticalDamage.cs)에서 배율 가져오기
                                float critDamageMultiplier = CriticalDamage.CalculateCritDamageMultiplier(player, Skills.SkillType.Bows);
                                totalDamageMultiplier *= critDamageMultiplier;
                                showEffect = true;
                                isAttackTreeEffect = true;
                                SkillEffect.ShowSkillEffectText(player, "🎯 " + L.Get("focus_fire_crit", $"{critDamageMultiplier:F1}"),
                                    new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                                Plugin.Log.LogDebug($"[활 집중 사격] 치명타 발동 - 확률: {totalCritChance}%, 배율: {critDamageMultiplier:F1}x");
                            }
                        }

                        // 기본 활공격 - 공격력 증가 (고정값) - 패시브 스킬
                        if (isBow && manager.GetSkillLevel("bow_Step3_silentshot") > 0)
                        {
                            // 활의 주 데미지 타입인 pierce에 고정값 추가
                            hit.m_damage.m_pierce += SkillTreeConfig.BowStep3SilentShotDamageBonusValue;

                            // 패시브 스킬이므로 메시지 표시 안 함 (CLAUDE.md 규칙 준수)
                            isAttackTreeEffect = true;
                        }

                        // 석궁 특화 (공격력 증가)
                        if (isCrossbow && manager.GetSkillLevel("atk_crossbow_bonus") > 0 && 
                            UnityEngine.Random.Range(0f, 100f) < SkillTreeConfig.AttackCrossbowBonusChanceValue)
                        {
                            float bonus = SkillTreeConfig.AttackCrossbowBonusDamageValue / 100f;
                            totalDamageMultiplier *= (1f + bonus);
                            showEffect = true;
                            isAttackTreeEffect = true;
                            SkillEffect.ShowSkillEffectText(player, "⚡ " + L.Get("crossbow_specialization"),
                                new Color(1f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Combat);
                        }

                        // 석궁 "단 한 발" 스킬 효과 확인 및 적용
                        if (isCrossbow)
                        {
                            bool oneShotActivated = SkillEffect.CheckAndConsumeCrossbowOneShot(player, __instance);
                            if (oneShotActivated)
                            {
                                // 공격력 +120% 보너스 적용
                                float damageBonus = Crossbow_Config.CrossbowOneShotDamageBonusValue / 100f;
                                totalDamageMultiplier *= (1f + damageBonus);
                                showEffect = true;
                                isAttackTreeEffect = true;

                                SkillEffect.ShowSkillEffectText(player, "🎯 " + L.Get("one_shot", Crossbow_Config.CrossbowOneShotDamageBonusValue),
                                    new Color(1f, 0.8f, 0f), SkillEffect.SkillEffectTextType.Critical);
                            }
                        }

                        // 지팡이 특화 (공격력 증가 + 주변 적에게 속성 피해)
                        if (isStaff && manager.GetSkillLevel("atk_staff_bonus") > 0 && 
                            UnityEngine.Random.Range(0f, 100f) < SkillTreeConfig.AttackStaffBonusChanceValue)
                        {
                            float bonus = SkillTreeConfig.AttackStaffBonusDamageValue / 100f;
                            totalDamageMultiplier *= (1f + bonus);
                            ApplyStaffAreaDamage(player, hit);
                            showEffect = true;
                            isAttackTreeEffect = true;
                            SkillEffect.ShowSkillEffectText(player, "🔥 " + L.Get("staff_specialization"),
                                new Color(1f, 0.2f, 1f), SkillEffect.SkillEffectTextType.Combat);
                        }
                    }

                    // 3단계: 공격 증가 (물리 공격력 +[CONFIG]%, 속성 공격력 +[CONFIG]%)
                    if (manager.GetSkillLevel("atk_twohand_drain") > 0)
                    {
                        float physicalBonus = SkillTreeConfig.AttackTwoHandDrainPhysicalDamageValue / 100f;
                        float elementalBonus = SkillTreeConfig.AttackTwoHandDrainElementalDamageValue / 100f;

                        // 물리 데미지 증가
                        hit.m_damage.m_blunt *= (1f + physicalBonus);
                        hit.m_damage.m_slash *= (1f + physicalBonus);
                        hit.m_damage.m_pierce *= (1f + physicalBonus);

                        // 속성 데미지 증가
                        hit.m_damage.m_fire *= (1f + elementalBonus);
                        hit.m_damage.m_frost *= (1f + elementalBonus);
                        hit.m_damage.m_lightning *= (1f + elementalBonus);
                        hit.m_damage.m_poison *= (1f + elementalBonus);
                        hit.m_damage.m_spirit *= (1f + elementalBonus);

                        isAttackTreeEffect = true;
                    }

                    // 4단계: 정밀 공격 (치명타 확률 +5%) - Valheim 표준 치명타 시스템 활용
                    if (manager.GetSkillLevel("atk_crit_chance") > 0)
                    {
                        // Valheim 표준 치명타 시스템과 연동하여 추가 보너스만 제공
                        float critBonus = 0.05f; // 5% 치명타 확률 보너스
                        if (UnityEngine.Random.Range(0f, 1f) < critBonus)
                        {
                            totalDamageMultiplier *= 1.3f; // 추가 치명타 보너스
                            showEffect = true;
                            isAttackTreeEffect = true;
                            SkillEffect.ShowSkillEffectText(player, "💀 " + L.Get("precision_attack"),
                                new Color(1f, 0.1f, 0.1f), SkillEffect.SkillEffectTextType.Critical);
                        }
                    }

                    // 4단계: 근접 강화 (근접무기 2연속 공격 시 +10% 추가 피해)
                    if (manager.GetSkillLevel("atk_melee_crit") > 0 && currentWeapon != null)
                    {
                        CheckMeleeCombo(player, ref totalDamageMultiplier, ref showEffect, ref isAttackTreeEffect);
                    }

                    // 6단계: 약점 공격 (치명타 피해 +[CONFIG]%)
                    if (manager.GetSkillLevel("atk_crit_dmg") > 0 && hit.m_damage.GetTotalDamage() > 50f)
                    {
                        float bonus = SkillTreeConfig.AttackCritDamageBonusValue / 100f;
                        totalDamageMultiplier *= (1f + bonus);
                        showEffect = true;
                        isAttackTreeEffect = true;
                    }

                    // 6단계: 양손 분쇄 (양손 무기 공격력 +[CONFIG]%)
                    if (manager.GetSkillLevel("atk_twohand_crush") > 0 && currentWeapon != null)
                    {
                        if (IsTwoHandedWeapon(currentWeapon))
                        {
                            float bonus = SkillTreeConfig.AttackTwoHandedBonusValue / 100f;
                            totalDamageMultiplier *= (1f + bonus);
                            showEffect = true;
                            isAttackTreeEffect = true;
                        }
                    }

                    // 6단계: 속성 공격 (활, 지팡이 속성 공격 +[CONFIG]%)
                    if (manager.GetSkillLevel("atk_staff_mage") > 0 && currentWeapon != null)
                    {
                        bool isElementalWeapon = isBow || isStaff;
                        if (isElementalWeapon)
                        {
                            float bonus = SkillTreeConfig.AttackStaffElementalValue / 100f;
                            float multiplier = 1f + bonus;
                            hit.m_damage.m_fire *= multiplier;
                            hit.m_damage.m_frost *= multiplier;
                            hit.m_damage.m_lightning *= multiplier;
                            hit.m_damage.m_poison *= multiplier;
                            hit.m_damage.m_spirit *= multiplier;
                            
                            isAttackTreeEffect = true;
                            
                            // 시각적 효과 표시
                            if (UnityEngine.Random.Range(0f, 1f) < 0.1f)
                            {
                                showEffect = true;
                                SkillEffect.ShowSkillEffectText(player, "🔥 " + L.Get("elemental_attack"),
                                    new Color(0.8f, 0.2f, 0.8f), SkillEffect.SkillEffectTextType.Combat);
                            }
                        }
                    }

                    // 4단계: 원거리 강화 (원거리 무기 공격력 +5%)
                    if (manager.GetSkillLevel("atk_ranged_enhance") > 0 && currentWeapon != null)
                    {
                        bool isRanged = isBow || isCrossbow || isStaff;
                        if (isRanged)
                        {
                            float bonus = SkillTreeConfig.AttackRangedEnhancementValue / 100f;
                            totalDamageMultiplier *= (1f + bonus);
                            showEffect = true;
                            isAttackTreeEffect = true;
                            SkillEffect.ShowSkillEffectText(player, "🏹 " + L.Get("ranged_enhance"),
                                new Color(0.2f, 0.8f, 0.8f), SkillEffect.SkillEffectTextType.Combat);
                        }
                    }

                    // 총 데미지 배율 적용
                    if (totalDamageMultiplier > 1f)
                    {
                        hit.m_damage.m_damage *= totalDamageMultiplier;
                        hit.m_damage.m_blunt *= totalDamageMultiplier;
                        hit.m_damage.m_slash *= totalDamageMultiplier;
                        hit.m_damage.m_pierce *= totalDamageMultiplier;
                        hit.m_damage.m_chop *= totalDamageMultiplier;
                        hit.m_damage.m_pickaxe *= totalDamageMultiplier;

                        // attack_root 스킬 전용 효과 표시 (배운 경우에만)
                        if (showAttackRootEffect && UnityEngine.Random.Range(0f, 1f) < 0.1f)
                        {
                            SkillEffect.ShowSkillEffectText(player, "⚔️ " + L.Get("attack_expert"),
                                new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                        }
                        
                        // 디버깅 로그 (가끔씩만)
                        if (UnityEngine.Random.Range(0f, 1f) < 0.02f)
                        {
                            Plugin.Log.LogInfo($"[공격 트리] 총 데미지 배율: {totalDamageMultiplier:F2}x");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[공격 트리] 데미지 보너스 패치 오류: {ex.Message}");
            }
        }

        private static void CheckMeleeCombo(Player player, ref float totalDamageMultiplier, ref bool showEffect, ref bool isAttackTreeEffect)
        {
            float currentTime = Time.time;
            
            if (!AttackTreeTracker.meleeComboCount.ContainsKey(player))
                AttackTreeTracker.meleeComboCount[player] = 0;
            if (!AttackTreeTracker.meleeLastHitTime.ContainsKey(player))
                AttackTreeTracker.meleeLastHitTime[player] = 0;

            // 3초 내 연속 공격인지 확인
            if (currentTime - AttackTreeTracker.meleeLastHitTime[player] < 3f)
            {
                AttackTreeTracker.meleeComboCount[player]++;
            }
            else
            {
                AttackTreeTracker.meleeComboCount[player] = 1;
            }

            AttackTreeTracker.meleeLastHitTime[player] = currentTime;

            // 2연속 공격 시 보너스 (근접 강화)
            var manager = SkillTreeManager.Instance;
            if (manager != null && manager.GetSkillLevel("atk_melee_crit") > 0 && 
                AttackTreeTracker.meleeComboCount[player] >= 2)
            {
                float bonus = SkillTreeConfig.AttackMeleeEnhancementValue / 100f;
                totalDamageMultiplier *= (1f + bonus);
                showEffect = true;
                isAttackTreeEffect = true;
                if (AttackTreeTracker.meleeComboCount[player] == 2)
                {
                    SkillEffect.ShowSkillEffectText(player, "⚔️ " + L.Get("melee_enhance"),
                        new Color(0.3f, 0.8f, 0.8f), SkillEffect.SkillEffectTextType.Combat);
                }
            }

            // 3연속 공격 시 추가 보너스 (연속 근접의 대가)
            if (manager != null && manager.GetSkillLevel("atk_finisher_melee") > 0 && 
                AttackTreeTracker.meleeComboCount[player] >= 3)
            {
                float bonus = SkillTreeConfig.AttackFinisherMeleeBonusValue / 100f;
                totalDamageMultiplier *= (1f + bonus);
                if (AttackTreeTracker.meleeComboCount[player] == 3)
                {
                    SkillEffect.ShowSkillEffectText(player, "🔥 " + L.Get("consecutive_melee_master"),
                        new Color(1f, 0.5f, 0f), SkillEffect.SkillEffectTextType.Critical);
                }
            }
        }

        private static void ApplyStaffAreaDamage(Player player, HitData originalHit)
        {
            try
            {
                // Valheim 표준 방식: 특정 위치 기준으로 반경 내 적들 찾기
                var targetPosition = originalHit.m_point; // 타격 지점 기준
                var nearbyEnemies = Character.GetAllCharacters()
                    .Where(c => c != null && 
                           !c.IsPlayer() && // 플레이어 제외
                           !c.IsTamed() && // 길들여지지 않은 생물만
                           BaseAI.IsEnemy(c, player) && // 적대적인 관계 확인
                           Vector3.Distance(c.transform.position, targetPosition) < 8f)
                    .Take(3); // 최대 3마리

                foreach (var enemy in nearbyEnemies)
                {
                    // Valheim 표준 HitData 생성
                    var areaHit = new HitData();
                    areaHit.m_attacker = player.GetZDOID();
                    areaHit.m_point = enemy.transform.position;
                    areaHit.m_dir = (enemy.transform.position - targetPosition).normalized;
                    
                    // 속성 피해만 적용 (물리 피해는 제외)
                    areaHit.m_damage.m_fire = originalHit.m_damage.m_fire * 0.3f;
                    areaHit.m_damage.m_frost = originalHit.m_damage.m_frost * 0.3f;
                    areaHit.m_damage.m_lightning = originalHit.m_damage.m_lightning * 0.3f;
                    areaHit.m_damage.m_poison = originalHit.m_damage.m_poison * 0.3f;
                    areaHit.m_damage.m_spirit = originalHit.m_damage.m_spirit * 0.3f;
                    
                    enemy.Damage(areaHit);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 특화] 광역 피해 적용 실패: {ex.Message}");
            }
        }

        private static bool IsTwoHandedWeapon(ItemDrop.ItemData weapon)
        {
            if (weapon?.m_shared == null) return false;
            
            // Valheim 표준 아이템 타입 확인 (ItemData.ItemType.TwoHandedWeapon 사용)
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                return true;
                
            // 스킬 타입 기반으로도 확인 (양손 무기 스킬 타입들)
            return weapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Polearms ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Spears;
        }
    }

    // ===== 중복 패치 제거 완료 =====
    // 모든 공격 전문화 로직은 Character_Damage_AttackTree_Patch에서 통합 처리됨

    // 치명타 확률 및 피해 증가 패치
    [HarmonyPatch(typeof(Character), nameof(Character.GetRandomSkillFactor))]
    public static class SkillTree_Character_GetRandomSkillFactor_CritBonus_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, Skills.SkillType skill, ref float __result)
        {
            try
            {
                if (!__instance.IsPlayer()) return;

                // 정밀 공격: 치명타 확률 +5%
                if (SkillEffect.HasSkill("atk_crit_chance"))
                {
                    __result += 0.05f;
                }

                // 대마법사 스킬 제거됨

                // 약점 공격: 치명타 피해 +7%
                if (SkillEffect.HasSkill("atk_crit_dmg") && __result > 1.0f) // 치명타 발생 시
                {
                    __result += 0.07f;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→발하임] GetRandomSkillFactor 치명타 패치 오류: {ex.Message}");
            }
        }
    }

    // 양손 무기 공격력 증가 패치
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class SkillTree_ItemData_GetDamage_TwoHandedCrush_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            try
            {
                if (__instance?.m_shared == null) return;
                
                // 양손 분쇄: 양손 무기 공격력 +[CONFIG]%
                if (SkillEffect.HasSkill("atk_twohand_crush") && __instance.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                {
                    float multiplier = 1f + (SkillTreeConfig.AttackTwoHandedBonusValue / 100f);
                    __result.m_damage *= multiplier;
                    __result.m_blunt *= multiplier;
                    __result.m_slash *= multiplier;
                    __result.m_pierce *= multiplier;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→발하임] GetDamage 양손분쇄 패치 오류: {ex.Message}");
            }
        }
    }

    // 공격 전문가 스킬 툴팁 표시 패치
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
        new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
    public static class AttackTree_ItemData_GetTooltip_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(ItemDrop.ItemData item, int qualityLevel, bool crafting, float worldLevel, int stackOverride, ref string __result)
        {
            try
            {
                // 무기 아이템만 처리
                if (item?.m_shared == null) return;
                if (item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.OneHandedWeapon &&
                    item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.TwoHandedWeapon &&
                    item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.TwoHandedWeaponLeft &&
                    item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow &&
                    item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Torch) return;

                // 크래프팅 화면이 아닐 때만 표시
                if (crafting) return;

                // 플레이어 확인
                var player = Player.m_localPlayer;
                if (player == null) return;

                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                string bonusText = "";

                // 공격 전문가 루트: 모든 공격력 +[CONFIG]%
                if (manager.GetSkillLevel("attack_root") > 0)
                {
                    float bonus = SkillTreeConfig.AttackRootDamageBonusValue;
                    bonusText += $"\n<color=#ffd700>⚔️ 공격 전문가: 모든 공격력 +{bonus}%</color>";
                }

                // 기본 공격: 물리 공격력 +[CONFIG], 속성 공격력 +[CONFIG]
                if (manager.GetSkillLevel("atk_base") > 0)
                {
                    float physicalBonus = SkillTreeConfig.AttackBasePhysicalDamageValue;
                    float elementalBonus = SkillTreeConfig.AttackBaseElementalDamageValue;
                    bonusText += $"\n<color=#00ff00>💪 기본 공격: 물리 +{physicalBonus}, 속성 +{elementalBonus}</color>";
                }

                // 공격 증가: 물리 공격력 +[CONFIG]%, 속성 공격력 +[CONFIG]%
                if (manager.GetSkillLevel("atk_twohand_drain") > 0)
                {
                    float physicalBonus = SkillTreeConfig.AttackTwoHandDrainPhysicalDamageValue;
                    float elementalBonus = SkillTreeConfig.AttackTwoHandDrainElementalDamageValue;
                    bonusText += $"\n<color=#ff8c00>🔥 공격 증가: 물리 +{physicalBonus}%, 속성 +{elementalBonus}%</color>";
                }

                if (!string.IsNullOrEmpty(bonusText))
                {
                    __result += bonusText;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[공격 전문가 툴팁] GetTooltip 패치 오류: {ex.Message}");
            }
        }
    }

    // 근접 전문가 효과 패치는 SkillEffect.MeleeSkills.cs로 이동됨

    // MMO 방식에 맞게 공격 상태 추적 시스템 제거
    // 스킬 효과는 MMO getParameter 패치를 통해 구현
    // CLAUDE.md 규칙: 프레임별 패치 금지, 이벤트 기반 업데이트만 사용
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 검/창 스킬 효과 시스템
    /// SkillEffect.MeleeSkills.cs에서 분리된 검/창 관련 기능들
    /// 폴암 스킬은 SkillEffect.PolearmTree.cs에 정의됨
    /// </summary>
    public static partial class SkillEffect
    {
        // === 꿰뚫는 창 (번개 충격) 시스템 ===
        public static Dictionary<Player, float> spearPenetrateBuffEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, int> spearPenetrateComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> spearPenetrateLastHitTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> spearPenetrateCooldownEndTime = new Dictionary<Player, float>();

        // 재진입 방지 플래그 (번개 충격 데미지가 다시 콤보를 트리거하지 않도록)
        private static bool isProcessingSpearLightningDamage = false;

        /// <summary>
        /// 번개 충격 처리 중 여부 확인 (외부에서 재진입 방지용)
        /// </summary>
        public static bool IsProcessingSpearLightningDamage() => isProcessingSpearLightningDamage;

        // === 검 연계 시스템 ===

        /// <summary>
        /// 검 연계 공격 카운트 업데이트
        /// </summary>
        public static void UpdateSwordCombo(Player player)
        {
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            float now = Time.time;
            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 5f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;

            // 3연타 달성 시 다음 공격 부스트
            if (swordComboCount[player] >= 3)
            {
                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1.5f;
                nextAttackExpiry[player] = now + 5f;
                nextAttackShowMessage[player] = true;
                PlaySkillEffect(player, "sword_combo");
                DrawFloatingText(player, "⚔️ " + L.Get("sword_combo_3hit"));
            }
        }

        /// <summary>
        /// 검 전문가 2연속 공격 체크 (sword_expert)
        /// </summary>
        public static void CheckSwordExpertCombo(Player player)
        {
            if (!HasSkill("sword_expert")) return;

            float now = Time.time;
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 5f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;

            if (swordComboCount[player] >= 2)
            {
                DrawFloatingText(player, "⚔️ " + L.Get("sword_expert_combo", SkillTreeConfig.SwordStep1ExpertComboBonusValue));

                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1f + (SkillTreeConfig.SwordStep1ExpertComboBonusValue / 100f);
                nextAttackExpiry[player] = now + SkillTreeConfig.SwordStep1ExpertDurationValue;
                nextAttackShowMessage[player] = true;
            }
        }

        /// <summary>
        /// 연속베기 3연속 공격 체크 (sword_step2_combo)
        /// </summary>
        public static void CheckSwordComboSlash(Player player)
        {
            if (!HasSkill("sword_step2_combo")) return;

            float now = Time.time;
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 10f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;

            if (swordComboCount[player] >= 3)
            {
                DrawFloatingText(player, "⚔️ " + L.Get("sword_combo_slash", SkillTreeConfig.SwordStep2ComboSlashBonusValue));

                swordComboSlashBuffEndTime[player] = now + SkillTreeConfig.SwordStep2ComboSlashDurationValue;
                swordComboCount[player] = 0;
            }
        }

        /// <summary>
        /// 반격 자세 - 패링 성공 후 방어력 증가 효과
        /// </summary>
        public static void ApplySwordCounterDefense(Player player)
        {
            if (!HasSkill("sword_step1_counter")) return;

            float duration = SkillTreeConfig.SwordStep1CounterDurationValue;
            swordCounterDefenseEndTime[player] = Time.time + duration;

            DrawFloatingText(player, "🛡️ " + L.Get("sword_counter_stance", duration, SkillTreeConfig.SwordStep1CounterDefenseBonusValue));
            Plugin.Log.LogInfo($"[반격 자세] 패링 성공 - {duration}초간 방어력 +{SkillTreeConfig.SwordStep1CounterDefenseBonusValue}% 적용");
        }

        /// <summary>
        /// 칼날 되치기 - 패시브 베기 +5 고정 (MeleeSkillPatches에서 항상 적용됨, 패링 버프 불필요)
        /// </summary>
        public static void ApplySwordBladeCounter(Player player)
        {
            // 이전 설계의 패링 버프 메커니즘 제거됨
            // 실제 효과는 MeleeSkillPatches.ApplySwordPassiveBonus에서 고정값으로 적용 중
        }

        /// <summary>
        /// 공방일체 - 양손 무기 착용 시 공격력/방어력 보너스
        /// </summary>
        public static void ApplySwordOffenseDefense(Player player, HitData hit)
        {
            if (!HasSkill("sword_step3_allinone")) return;

            if (Sword_Skill.IsUsingSword(player))
            {
                DrawFloatingText(player, "⚔️🛡️ " + L.Get("sword_offense_defense", SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue, Sword_Config.SwordStep3AllInOneDefenseBonusValue));
                Plugin.Log.LogDebug($"[공방일체] 검 착용 - 공격력 +{SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue}%, 막기 방어력 +{Sword_Config.SwordStep3AllInOneDefenseBonusValue} 적용");
            }
        }

        // ApplySwordDefenseSwitch 제거됨 - 패링 돌격 액티브 스킬로 전환 (Sword_Skill.cs)

        /// <summary>
        /// Sword Slash 액티브 스킬 데미지 보너스 적용
        /// </summary>
        public static void ApplySwordSlashDamageBonus(Player player, Character target, HitData hit)
        {
            if (!HasSkill("sword_step5_finalcut") && !HasSkill("sword_slash")) return;
            if (!Sword_Skill.IsSwordSlashActive(player)) return;

            float damageRatio = Sword_Config.RushSlash1stDamageRatioValue / 100f;

            hit.m_damage.m_blunt *= damageRatio;
            hit.m_damage.m_slash *= damageRatio;
            hit.m_damage.m_pierce *= damageRatio;
            hit.m_damage.m_chop *= damageRatio;
            hit.m_damage.m_pickaxe *= damageRatio;
            hit.m_damage.m_fire *= damageRatio;
            hit.m_damage.m_frost *= damageRatio;
            hit.m_damage.m_lightning *= damageRatio;
            hit.m_damage.m_poison *= damageRatio;
            hit.m_damage.m_spirit *= damageRatio;

            PlaySkillEffect(player, "sword_slash", target.transform.position);
            DrawFloatingText(player, $"⚔️ Sword Slash! ({damageRatio * 100:F0}%)", Color.red);

            Plugin.Log.LogInfo($"[Sword Slash] 데미지 보너스 적용 - {damageRatio * 100:F0}%");
        }

        // === 창 스킬 시스템 ===

        // === 회피 찌르기 버프 시스템 ===
        public static Dictionary<Player, float> spearEvasionBuffEndTime = new Dictionary<Player, float>();

        /// <summary>
        /// 회피 찌르기 - 창 공격 시 5초간 회피율 버프 발동
        /// </summary>
        public static void ApplySpearEvasionBuff(Player player)
        {
            if (!HasSkill("spear_Step2_evasion")) return;
            float duration = 5f;
            spearEvasionBuffEndTime[player] = Time.time + duration;
            DrawFloatingText(player, "💨 " + L.Get("spear_evasion_buff", Spear_Config.SpearStep3EvasionBonusValue));
            UpdateDefenseDodgeRate(player);
            Plugin.Log.LogDebug($"[회피찌르기] 공격 시 회피율 +{Spear_Config.SpearStep3EvasionBonusValue}% 버프 ({duration}초)");
        }

        /// <summary>
        /// 회피 찌르기 버프 활성 여부 확인
        /// </summary>
        public static bool IsSpearEvasionBuffActive(Player player)
        {
            return spearEvasionBuffEndTime.TryGetValue(player, out float expiry) && Time.time < expiry;
        }

        /// <summary>
        /// 창 전문가 2연속 공격 체크 (MMO 연동 방식)
        /// </summary>
        public static void CheckSpearExpertCombo(Player player)
        {
            if (!HasSkill("spear_expert")) return;

            float now = Time.time;
            if (!spearExpertComboCount.ContainsKey(player))
                spearExpertComboCount[player] = 0;

            if (spearExpertLastHitTime.ContainsKey(player) && now - spearExpertLastHitTime[player] < 5f)
            {
                spearExpertComboCount[player]++;
            }
            else
            {
                spearExpertComboCount[player] = 1;
            }
            spearExpertLastHitTime[player] = now;
        }

        /// <summary>
        /// 이연창 2연속 공격 체크 - 버프 지속시간 방식
        /// </summary>
        public static void CheckSpearDualCombo(Player player)
        {
            if (!HasSkill("spear_Step4_triple")) return;

            // [Fix 1] 버프가 이미 활성 중이면 재발동 없음
            if (IsSpearDualBuffActive(player)) return;

            float now = Time.time;
            if (!spearComboCount.ContainsKey(player))
                spearComboCount[player] = 0;

            // [Fix 2] 0.05초 이내 중복 히트 무시 (폭발창 AoE 다중 히트 대응)
            if (spearLastHitTime.ContainsKey(player) && now - spearLastHitTime[player] < 0.05f)
                return;

            if (spearLastHitTime.ContainsKey(player) && now - spearLastHitTime[player] < 5f)
            {
                spearComboCount[player]++;
            }
            else
            {
                spearComboCount[player] = 1;
            }
            spearLastHitTime[player] = now;

            // 2연속 공격 시 버프 발동
            if (spearComboCount[player] >= 2)
            {
                float duration = Spear_Config.SpearDualDurationValue;
                spearDualBuffExpiry[player] = now + duration;
                spearComboCount[player] = 0;

                DrawFloatingText(player, "⚡ " + L.Get("spear_dual_strike", duration, Spear_Config.SpearDualDamageBonusValue));
                Plugin.Log.LogInfo($"[이연창] 2연속 공격 달성 - {duration}초간 공격력 보너스 적용");
            }
        }

        // === 창 전문가 proc 버프 시스템 ===
        public static Dictionary<Player, bool> spearExpertProcBuffActive = new Dictionary<Player, bool>();
        public static Dictionary<Player, int> spearExpertProcCharges = new Dictionary<Player, int>();

        /// <summary>
        /// 창 전문가 proc 발동 (28% 확률 → 다음 1회 공격 200% 속도)
        /// </summary>
        public static void TriggerSpearExpertProc(Player player)
        {
            if (!HasSkill("spear_expert")) return;

            float procChance = Spear_Config.SpearExpertProcChanceValue / 100f;
            if (UnityEngine.Random.value < procChance)
            {
                spearExpertProcBuffActive[player] = true;
                spearExpertProcCharges[player] = 1;
                DrawFloatingText(player, "⚡ " + L.Get("spear_expert_proc"));
                Plugin.Log.LogDebug($"[창 전문가] proc 발동 - 다음 1회 공격 {100f + Spear_Config.SpearExpertSpeedBoostPercentValue}% 속도");
            }
        }

        /// <summary>
        /// 창 전문가 proc 버프 소비 (공격 시 1회 차지 감소)
        /// </summary>
        public static void ConsumeSpearExpertProc(Player player)
        {
            if (!spearExpertProcBuffActive.TryGetValue(player, out bool active) || !active) return;
            spearExpertProcCharges[player]--;
            if (spearExpertProcCharges[player] <= 0)
                spearExpertProcBuffActive[player] = false;
        }

        /// <summary>
        /// 창 전문가 proc 버프 활성 여부
        /// </summary>
        public static bool IsSpearExpertProcActive(Player player)
        {
            return spearExpertProcBuffActive.TryGetValue(player, out bool active) && active;
        }

        // 이연창 버프 만료시간 추적
        public static Dictionary<Player, float> spearDualBuffExpiry = new Dictionary<Player, float>();

        /// <summary>
        /// 이연창 버프가 활성화되어 있는지 확인
        /// </summary>
        public static bool IsSpearDualBuffActive(Player player)
        {
            return spearDualBuffExpiry.TryGetValue(player, out float expiry) && Time.time < expiry;
        }

        // 레거시 호환용
        public static void CheckSpearTripleCombo(Player player) => CheckSpearDualCombo(player);

        /// <summary>
        /// 연격창 무기 공격력 +4 패시브 보너스 적용
        /// </summary>
        public static void CheckDoubleAttack(Player player, Character target, HitData originalHit)
        {
            if (!HasSkill("spear_Step3_pierce")) return;

            float bonusValue = SkillTreeConfig.SpearStep3PierceDamageBonusValue;
            originalHit.m_damage.m_pierce += bonusValue;

            Plugin.Log.LogDebug($"[연격창] 무기 공격력 보너스 적용 (pierce): +{SkillTreeConfig.SpearStep3PierceDamageBonusValue}");
        }

        /// <summary>
        /// 투창 전문가 공격력 보너스 적용
        /// </summary>
        public static void ApplySpearThrowExpertDamage(HitData hit)
        {
            if (!HasSkill("spear_Step1_throw")) return;

            float bonusPercent = 0.3f; // 30%
            hit.m_damage.m_pierce *= (1f + bonusPercent);

            Plugin.Log.LogDebug("[투창 전문가] 투창 공격력 +30% 적용 (pierce)");
        }

        /// <summary>
        /// 꿰뚫는 창 G키 액티브 스킬 (쿨타임 및 스태미나 체크)
        /// </summary>
        public static void ActivateSpearPenetrateLightning(Player player)
        {
            if (!HasSkill("spear_Step5_penetrate")) return;

            // 쿨타임 체크
            float cooldown = Spear_Config.SpearStep6PenetrateCooldownValue;
            if (spearPenetrateCooldownEndTime.TryGetValue(player, out float cooldownEnd) && Time.time < cooldownEnd)
            {
                float remaining = cooldownEnd - Time.time;
                DrawFloatingText(player, L.Get("spear_penetrate_cooldown", $"{remaining:F1}"), Color.red);
                return;
            }

            // 스태미나 체크 (고정값)
            float staminaCost = Spear_Config.SpearStep6PenetrateStaminaCostValue;
            if (player.GetStamina() < staminaCost)
            {
                DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                return;
            }

            // 스태미나 소모
            player.UseStamina(staminaCost);

            // 버프 활성화
            ActivateSpearPenetrateBuff(player);

            // 쿨타임 시작
            spearPenetrateCooldownEndTime[player] = Time.time + cooldown;
            ActiveSkillCooldownRegistry.SetCooldown("G", cooldown);

            // VFX 재생 (플레이어 위치)
            SimpleVFX.Play("vfx_offering_activate", player.transform.position, 1f);

            Plugin.Log.LogInfo($"[꿰뚫는 창] G키 액티브 스킬 발동 - 쿨타임: {cooldown}초, 스태미나: {staminaCost:F0}");
        }

        /// <summary>
        /// 꿰뚫는 창 버프 활성화 (내부 호출용)
        /// </summary>
        public static void ActivateSpearPenetrateBuff(Player player)
        {
            float duration = Spear_Config.SpearStep6PenetrateBuffDurationValue;
            spearPenetrateBuffEndTime[player] = Time.time + duration;
            spearPenetrateComboCount[player] = 0;

            DrawFloatingText(player, "⚡ " + L.Get("spear_penetrate_activated", $"{duration}"), Color.yellow);
            Plugin.Log.LogInfo($"[꿰뚫는 창] 버프 활성화 - {duration}초간 지속");
        }

        /// <summary>
        /// 꿰뚫는 창 버프 활성 여부 확인
        /// </summary>
        public static bool IsSpearPenetrateBuffActive(Player player)
        {
            if (!spearPenetrateBuffEndTime.ContainsKey(player)) return false;
            return Time.time < spearPenetrateBuffEndTime[player];
        }

        /// <summary>
        /// 꿰뚫는 창 콤보 체크 및 번개 충격 발동
        /// G키로 버프 활성화 후 3회 연속 적중 시 번개 충격 발동
        /// </summary>
        public static void CheckSpearPenetrateCombo(Player player, Character target, HitData hit)
        {
            if (!HasSkill("spear_Step5_penetrate")) return;
            if (target == null || !target.IsMonsterFaction(Time.time)) return;

            // 번개 충격 데미지 처리 중이면 스킵 (재진입 방지)
            if (isProcessingSpearLightningDamage) return;

            // 버프가 비활성화 상태면 스킵 (G키로 활성화 필요)
            if (!IsSpearPenetrateBuffActive(player)) return;

            float now = Time.time;
            int requiredCombo = Spear_Config.SpearStep6PenetrateComboCountValue;

            // 콤보 카운터 초기화 체크 (3초 이내 연속 적중만 인정)
            if (!spearPenetrateComboCount.ContainsKey(player))
                spearPenetrateComboCount[player] = 0;

            if (spearPenetrateLastHitTime.ContainsKey(player) && now - spearPenetrateLastHitTime[player] < 5f)
            {
                spearPenetrateComboCount[player]++;
            }
            else
            {
                spearPenetrateComboCount[player] = 1;
            }
            spearPenetrateLastHitTime[player] = now;

            Plugin.Log.LogDebug($"[꿰뚫는 창] 콤보 카운트: {spearPenetrateComboCount[player]}/{requiredCombo}");

            // 연속 적중 횟수 달성 시 번개 충격 발동
            if (spearPenetrateComboCount[player] >= requiredCombo)
            {
                TriggerSpearPenetrateLightning(player, target, hit);
                spearPenetrateComboCount[player] = 0;
            }
        }

        /// <summary>
        /// 꿰뚫는 창 번개 충격 발동
        /// 몬스터를 2m 띄우고 번개 데미지 적용
        /// </summary>
        private static void TriggerSpearPenetrateLightning(Player player, Character target, HitData hit)
        {
            if (target == null || target.IsDead()) return;
            if (player == null) return;

            // 이미 번개 충격 처리 중이면 스킵 (재진입 방지)
            if (isProcessingSpearLightningDamage) return;

            try
            {
                // 번개 충격 처리 시작 플래그 설정
                isProcessingSpearLightningDamage = true;

                float damageMultiplier = Spear_Config.SpearStep6PenetrateLightningDamageValue / 100f;

                // 1. 번개 데미지 계산
                float baseDamage = hit != null ? hit.GetTotalDamage() : 50f;
                float lightningDamage = baseDamage * damageMultiplier;

                // 2. VFX 재생
                SimpleVFX.Play("flash_blue_purple", target.transform.position, 1.0f);

                // 3. 번개 데미지 적용
                if (!target.IsDead())
                {
                    HitData lightningHit = new HitData();
                    lightningHit.m_damage.m_lightning = lightningDamage;
                    lightningHit.m_point = target.transform.position;
                    lightningHit.m_dir = (target.transform.position - player.transform.position).normalized;
                    lightningHit.SetAttacker(player);
                    target.Damage(lightningHit);
                }

                DrawFloatingText(player, "⚡ " + L.Get("spear_lightning_shock", $"{lightningDamage:F0}"), Color.cyan);
                Plugin.Log.LogInfo($"[꿰뚫는 창] 번개 충격 발동 - 데미지: {lightningDamage:F0}");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[꿰뚫는 창] 번개 충격 오류: {ex.Message}");
            }
            finally
            {
                // 번개 충격 처리 완료 플래그 해제
                isProcessingSpearLightningDamage = false;
            }
        }

    }
}

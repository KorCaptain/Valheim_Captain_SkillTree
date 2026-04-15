using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 버서커 직업 전용 스킬 시스템 (재작성 버전 - 무한 로딩 문제 해결)
    /// 액티브: 버서커 분노 (Y키) - 20초간 체력 비례 데미지 증가
    /// 패시브: 죽음의 무시 - 스태미나 리젠 +20%, 체력 10% 이하 시 8초 무적
    /// </summary>
    public static class BerserkerSkills
    {
        #region State Classes

        /// <summary>
        /// 버서커 분노 액티브 스킬 상태
        /// </summary>
        private class RageState
        {
            public float EndTime;
            public float CooldownEndTime;
            public int LastDamageTier = -1; // 20% 간격 표시용

            public bool IsActive => Time.time < EndTime;
            public bool OnCooldown => Time.time < CooldownEndTime;

            public void Clear()
            {
                EndTime = 0f;
                CooldownEndTime = 0f;
                LastDamageTier = -1;
            }
        }

        /// <summary>
        /// 버서커 패시브 스킬 상태
        /// </summary>
        private class PassiveState
        {
            public float EndTime;
            public float CooldownEndTime;
            public float LastNotificationTime;

            public bool IsActive => Time.time < EndTime;
            public bool OnCooldown => Time.time < CooldownEndTime;

            public void Clear()
            {
                EndTime = 0f;
                CooldownEndTime = 0f;
                LastNotificationTime = 0f;
            }
        }

        #endregion

        #region Fields

        private static Dictionary<Player, RageState> rageStates = new Dictionary<Player, RageState>();
        private static Dictionary<Player, PassiveState> passiveStates = new Dictionary<Player, PassiveState>();

        // 버서커 분노 버프 VFX 인스턴스 (머리 위 statusailment_01_aura)
        private static Dictionary<Player, GameObject> rageBuffVFXInstances = new Dictionary<Player, GameObject>();

        #endregion

        #region Active Skill - Berserker Rage

        /// <summary>
        /// 버서커 분노 스킬 시전
        /// </summary>
        public static bool CastBerserkerRage(Player player)
        {
            try
            {
                if (player == null)
                {
                    Plugin.Log.LogWarning("[버서커 분노] 플레이어가 null");
                    return false;
                }

                // 상태 가져오기 (없으면 생성)
                if (!rageStates.TryGetValue(player, out var state))
                {
                    state = new RageState();
                    rageStates[player] = state;
                }

                // 쿨다운 확인
                if (state.OnCooldown)
                {
                    float remaining = state.CooldownEndTime - Time.time;
                    SkillEffect.ShowSkillEffectText(player, L.Get("berserker_cooldown", $"{remaining:F0}"),
                        Color.red, SkillEffect.SkillEffectTextType.Critical);
                    return false;
                }

                // 스태미나 확인
                float staminaCost = Berserker_Config.BerserkerRageStaminaCostValue;
                if (player.GetStamina() < staminaCost)
                {
                    SkillEffect.ShowSkillEffectText(player, L.Get("stamina_insufficient_short"),
                        Color.red, SkillEffect.SkillEffectTextType.Critical);
                    return false;
                }

                // 이미 분노 상태인지 확인
                if (state.IsActive)
                {
                    SkillEffect.ShowSkillEffectText(player, L.Get("already_rage_state"),
                        Color.yellow, SkillEffect.SkillEffectTextType.Critical);
                    return false;
                }

                // 스태미나 소모
                player.UseStamina(staminaCost);

                // 분노 상태 적용 (레벨별 쿨타임/지속시간)
                int bsLevel = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 1;
                float duration = Berserker_Config.GetEffectiveRageDuration(bsLevel);
                float cooldown = Berserker_Config.GetEffectiveRageCooldown(bsLevel);

                state.EndTime = Time.time + duration;
                state.CooldownEndTime = Time.time + cooldown;
                state.LastDamageTier = -1;
                ActiveSkillCooldownRegistry.SetCooldown("Y", cooldown);

                // VFX 효과 생성 (자동 정리, 사운드 포함)
                CreateRageEffect(player, duration);

                // 화면 텍스트
                SkillEffect.ShowSkillEffectText(player, "🔥 " + L.Get("berserker_rage"),
                    Color.red, SkillEffect.SkillEffectTextType.XLarge);

                // 분노 시전 성공
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 시전 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 플레이어가 분노 상태인지 확인
        /// </summary>
        public static bool IsPlayerInRage(Player player)
        {
            if (player == null) return false;
            return rageStates.TryGetValue(player, out var bsRageIsActive) && bsRageIsActive.IsActive;
        }

        /// <summary>
        /// 분노 데미지 보너스 계산 (Config 연동)
        /// </summary>
        public static float GetRageDamageBonus(Player player)
        {
            if (!IsPlayerInRage(player)) return 0f;

            try
            {
                // 체력 손실 비율 계산
                float currentHealthPercent = player.GetHealthPercentage();
                float lostHealthPercent = (1f - currentHealthPercent) * 100f;

                // 레벨별 값 사용 (Lv1:1.5%, Lv2:1.6%, Lv3:1.7%, Lv4:1.8%, Lv5:2.0%)
                int bsLvDmg = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 1;
                float damagePerPercent = Berserker_Config.GetEffectiveDamagePerHealthPercent(bsLvDmg);
                float maxBonus = Berserker_Config.GetEffectiveMaxDamageBonus(bsLvDmg);

                float damageBonus = lostHealthPercent * damagePerPercent;
                damageBonus = Mathf.Min(damageBonus, maxBonus);

                // 20% 간격 표시
                UpdateDamageDisplay(player, damageBonus);

                return damageBonus;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 데미지 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 20% 간격 데미지 증가 표시
        /// </summary>
        private static void UpdateDamageDisplay(Player player, float currentDamageBonus)
        {
            try
            {
                if (!rageStates.TryGetValue(player, out var state)) return;
                int bsLvDisp = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 1;
                float bsMaxBonus = Berserker_Config.GetEffectiveMaxDamageBonus(bsLvDisp);
                int currentTier = Mathf.FloorToInt(currentDamageBonus / 20f);
                currentTier = Mathf.Min(currentTier, Mathf.FloorToInt(bsMaxBonus / 20f)); // Lv1~3:10, Lv4~5:12

                if (currentTier > state.LastDamageTier && currentTier > 0)
                {
                    int displayPercentage = currentTier * 20;
                    SkillEffect.ShowSkillEffectText(player,
                        L.Get("berserker_rage_damage_display", displayPercentage),
                        Color.red,
                        SkillEffect.SkillEffectTextType.XLarge);

                    state.LastDamageTier = currentTier;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 표시 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 분노 VFX 효과 생성 (WackyEpicMMO 방식 - 플레이어를 따라다니는 이펙트)
        /// </summary>
        private static void CreateRageEffect(Player player, float duration)
        {
            try
            {
                // SimpleVFX 방식: Valheim 내장 VFX로 플레이어 효과
                SimpleVFX.PlayOnPlayer(player, Berserker_Config.BerserkerRageDurationValue);

                // 분노 사운드
                CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("sfx_dragon_scream", "", player.transform.position);

                // 머리 위 상태 표시 VFX 생성 (연공창 방식)
                CreateRageBuffVFX(player, duration);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] VFX 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 버서커 분노 버프 VFX 생성 (머리 위 - 연공창 방식)
        /// </summary>
        private static void CreateRageBuffVFX(Player player, float duration)
        {
            try
            {
                // 기존 VFX가 있으면 제거
                RemoveRageBuffVFX(player);

                // 새 VFX 생성 - statusailment_01_aura (머리 높이, 수동 관리)
                var vfx = SimpleVFX.PlayOnPlayer(player, "statusailment_01_aura", duration, new Vector3(0f, 1.2f, 0f));
                if (vfx != null)
                {
                    rageBuffVFXInstances[player] = vfx;
                    Plugin.Log.LogInfo("[버서커 분노] 버프 VFX 생성됨");

                    // 버프 만료 시 VFX 자동 제거 코루틴 시작
                    if (Plugin.Instance != null)
                    {
                        Plugin.Instance.StartCoroutine(MonitorRageBuffExpiration(player));
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[버서커 분노] 버프 VFX 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 버프 만료 모니터링 코루틴 - 분노 종료 시 VFX 제거
        /// </summary>
        private static IEnumerator MonitorRageBuffExpiration(Player player)
        {
            while (player != null && !player.IsDead())
            {
                // 버프 상태 확인
                if (!IsPlayerInRage(player))
                {
                    // 버프 종료 - VFX 제거
                    RemoveRageBuffVFX(player);
                    Plugin.Log.LogInfo("[버서커 분노] 버프 만료 - VFX 제거됨");
                    yield break;
                }

                yield return new WaitForSeconds(0.5f);
            }

            // 플레이어 사망 또는 null - VFX 정리
            RemoveRageBuffVFX(player);
        }

        /// <summary>
        /// 버서커 분노 버프 VFX 제거
        /// </summary>
        private static void RemoveRageBuffVFX(Player player)
        {
            try
            {
                if (rageBuffVFXInstances.TryGetValue(player, out var vfx) && vfx != null)
                {
                    UnityEngine.Object.Destroy(vfx);
                    Plugin.Log.LogInfo("[버서커 분노] 버프 VFX 제거됨");
                }
                rageBuffVFXInstances.Remove(player);
            }
            catch { }
        }

        /// <summary>
        /// 몬스터 적중 시 VFX 효과 (분노 버프 중 flash_round_yellow)
        /// </summary>
        public static void CreateMonsterHitEffect(Character target)
        {
            try
            {
                if (target == null) return;

                SimpleVFX.Play("flash_round_ellow", target.transform.position + Vector3.up * 1f, 1.5f);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 분노] 적중 효과 실패: {ex.Message}");
            }
        }

        #endregion

        #region Passive Skill - Death Defiance

        /// <summary>
        /// 버서커 패시브 스킬 체크
        /// </summary>
        public static void CheckBerserkerPassiveSkill(Player player, bool skipThresholdCheck = false)
        {
            try
            {
                if (player == null) return;

                // 상태 가져오기 (없으면 생성)
                if (!passiveStates.TryGetValue(player, out var state))
                {
                    state = new PassiveState();
                    passiveStates[player] = state;
                }

                // 이미 패시브 무적 상태면 패스
                if (state.IsActive) return;

                // 쿨다운 중이면 패스
                if (state.OnCooldown) return;

                // 체력 임계값 확인 (skipThresholdCheck=true 이면 우회 - 즉사 감지 경로)
                float currentHealthPercent = player.GetHealthPercentage();
                float threshold = Berserker_Config.BerserkerPassiveHealthThresholdValue / 100f;

                if (!skipThresholdCheck && currentHealthPercent > threshold) return;

                // 패시브 무적 발동
                ApplyPassiveInvincibility(player, state);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] 체크 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 패시브 무적 상태인지 확인
        /// </summary>
        public static bool IsPassiveInvincibilityActive(Player player)
        {
            if (player == null) return false;
            return passiveStates.TryGetValue(player, out var bsPassiveActive) && bsPassiveActive.IsActive;
        }

        /// <summary>
        /// 패시브 무적 쿨다운 확인
        /// </summary>
        public static bool IsPassiveInvincibilityOnCooldown(Player player)
        {
            if (player == null) return false;
            return passiveStates.TryGetValue(player, out var bsPassiveCd) && bsPassiveCd.OnCooldown;
        }

        /// <summary>
        /// 스태미나 리젠 보너스 (+20%)
        /// </summary>
        public static float GetBerserkerStaminaRegenBonus(Player player)
        {
            if (player == null) return 0f;
            return 0.2f; // 20% 보너스
        }

        /// <summary>
        /// 패시브 무적 적용
        /// </summary>
        private static void ApplyPassiveInvincibility(Player player, PassiveState state)
        {
            try
            {
                int bLvPassive = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 1;
                float duration = Berserker_Config.GetEffectiveInvincibilityDuration(bLvPassive);
                float cooldown = Berserker_Config.GetEffectivePassiveCooldown(bLvPassive);
                // Config 오설정 보호: 최소 60초 (쿨다운이 무적 지속시간보다 짧으면 무한 반복 방지)
                cooldown = Mathf.Max(cooldown, 60f);

                state.EndTime = Time.time + duration;
                state.CooldownEndTime = Time.time + cooldown;
                state.LastNotificationTime = 0f;
                ActiveSkillCooldownRegistry.SetCooldown("passive_berserker", cooldown);

                // VFX 효과 (자동 정리)
                CreatePassiveEffect(player);

                SkillEffect.ShowSkillEffectText(player, "⚡ " + L.Get("death_ignore") + " ⚡",
                    Color.yellow, SkillEffect.SkillEffectTextType.XLarge);

                // 무적 발동 완료
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] 무적 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 패시브 VFX 효과 생성 (WackyEpicMMO 방식)
        /// </summary>
        private static void CreatePassiveEffect(Player player)
        {
            try
            {
                float duration = Berserker_Config.BerserkerPassiveInvincibilityDurationValue;

                // 무적 발동: fx_shield_start (캐릭터 따라다님) + sfx_dragon_scream
                CaptainSkillTree.VFX.VFXManager.PlayVFXAttachedToPlayer(
                    player, "fx_shield_start", "sfx_dragon_scream", duration);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] VFX 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 패시브 쿨다운 알림
        /// </summary>
        public static void CheckPassiveCooldownNotifications(Player player)
        {
            try
            {
                if (player == null) return;
                if (!passiveStates.TryGetValue(player, out var state)) return;
                if (!state.OnCooldown) return;

                float remainingTime = state.CooldownEndTime - Time.time;
                if (remainingTime <= 0) return;

                float currentTime = Time.time;
                float timeSinceLastNotification = currentTime - state.LastNotificationTime;

                bool shouldNotify = false;

                // 마지막 1분 이하: 20초 간격
                if (remainingTime <= 60f && timeSinceLastNotification >= 20f)
                {
                    shouldNotify = true;
                }
                // 1분 초과: 60초 간격
                else if (remainingTime > 60f && timeSinceLastNotification >= 60f)
                {
                    shouldNotify = true;
                }

                if (shouldNotify)
                {
                    int minutes = Mathf.FloorToInt(remainingTime / 60f);
                    int seconds = Mathf.FloorToInt(remainingTime % 60f);

                    string message = minutes > 0
                        ? L.Get("berserker_passive_cd_ms", minutes, seconds)
                        : L.Get("berserker_passive_cd_s", seconds);

                    SkillEffect.ShowSkillEffectText(player, message,
                        new Color(0.8f, 0.8f, 1f), SkillEffect.SkillEffectTextType.Standard);

                    state.LastNotificationTime = currentTime;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 패시브] 알림 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 버서커 스킬 보유 확인
        /// </summary>
        private static bool HasBerserkerSkill(Player player)
        {
            try
            {
                if (player == null) return false;
                return SkillEffect.HasSkill("Berserker");
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// 안전한 정리 (무한 로딩 방지)
        /// </summary>
        public static void SafeCleanup(Player player)
        {
            try
            {
                if (player == null)
                {
                    // 플레이어가 null이면 전체 초기화
                    try { rageStates?.Clear(); } catch { }
                    try { passiveStates?.Clear(); } catch { }
                    return;
                }

                // 상태 제거 (예외 무시)
                try
                {
                    if (rageStates != null && rageStates.TryGetValue(player, out var bsDeathRage))
                    {
                        try { bsDeathRage?.Clear(); } catch { }
                        try { rageStates.Remove(player); } catch { }
                    }
                }
                catch { }

                try
                {
                    if (passiveStates != null && passiveStates.TryGetValue(player, out var bsDeathPassive))
                    {
                        try { bsDeathPassive?.Clear(); } catch { }
                        try { passiveStates.Remove(player); } catch { }
                    }
                }
                catch { }

                // 분노 버프 VFX 정리
                try { RemoveRageBuffVFX(player); } catch { }
            }
            catch
            {
                // 모든 예외 완전히 무시 - 무한 로딩 절대 방지
            }
        }

        /// <summary>
        /// 모든 상태 초기화
        /// </summary>
        public static void ClearAllStates()
        {
            try
            {
                rageStates?.Clear();
                passiveStates?.Clear();
                // 상태 초기화 완료
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[버서커 정리] 초기화 실패: {ex.Message}");
            }
        }

        #endregion

        #region Harmony Patches

        /// <summary>
        /// 버서커 패시브: 최대 체력 +100% (모든 효과 합산 기준)
        /// defense_Step6_body(요툰의 생명력)와 동일한 비율→고정값 변환 방식
        /// Priority.Last: 음식, MMO, 다른 스킬트리 패치 이후 최종 적용
        /// </summary>
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Berserker_Player_GetTotalFoodValue_HealthBonus_Patch
        {
            [HarmonyPriority(Priority.Last)]
            public static void Postfix(Player __instance, ref float hp)
            {
                try
                {
                    if (!HasBerserkerSkill(__instance)) return;

                    int bLv = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 1;
                    float bonusHealth = Berserker_Config.GetEffectiveHealthBonus(bLv);
                    hp += bonusHealth;
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[버서커 패시브] 체력 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 데미지 패치 - 분노 보너스 및 패시브 무적
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Berserker_Character_Damage_Patch
        {
            public static void Prefix(Character __instance, ref HitData hit)
            {
                try
                {
                    // === 방어 측: 피격자가 버서커 플레이어 ===
                    if (__instance is Player player && HasBerserkerSkill(player))
                    {
                        // 1. 패시브 무적 활성 중 → 데미지 전량 차단
                        if (IsPassiveInvincibilityActive(player))
                        {
                            hit.m_damage = new HitData.DamageTypes();
                            return;
                        }

                        // 2. 쿨다운 아님 + 즉사 위험 감지 → 선제 무적 발동 (Postfix에서 발동하면 이미 사망 후라 무의미)
                        if (!IsPassiveInvincibilityOnCooldown(player))
                        {
                            float currentHp = player.GetHealth();
                            float maxHp = player.GetMaxHealth();
                            float threshold = Berserker_Config.BerserkerPassiveHealthThresholdValue / 100f;

                            // raw 데미지 합산 (방어 계산 전 수치)
                            var dmg = hit.m_damage;
                            float rawDmg = dmg.m_blunt + dmg.m_slash + dmg.m_pierce
                                         + dmg.m_fire + dmg.m_frost + dmg.m_lightning
                                         + dmg.m_poison + dmg.m_spirit;

                            // 방어력 보정 적용 (Valheim 근사식: 실제 = raw - armor/2, 최소 raw의 10%)
                            float bodyArmor = player.GetBodyArmor();
                            float estimatedDmg = Mathf.Max(rawDmg - bodyArmor * 0.5f, rawDmg * 0.1f);

                            bool hpBelowThreshold = maxHp > 0f && (currentHp / maxHp) <= threshold;
                            bool willDieFromHit   = currentHp <= estimatedDmg;

                            if (hpBelowThreshold || willDieFromHit)
                            {
                                // 즉사 감지(willDieFromHit)이고 HP 임계값 이상인 경우: threshold 체크 우회
                                bool skipCheck = willDieFromHit && !hpBelowThreshold;
                                CheckBerserkerPassiveSkill(player, skipThresholdCheck: skipCheck);
                                if (IsPassiveInvincibilityActive(player))
                                {
                                    hit.m_damage = new HitData.DamageTypes();
                                    return;
                                }
                            }
                        }

                        // 2. Lv3: 분노 중 받는 피해 감소
                        if (IsPlayerInRage(player))
                        {
                            int bLv = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 0;
                            if (bLv >= 3)
                            {
                                float reduction = 1f - Berserker_Config.BerserkerLv3RageDamageReductionValue / 100f;
                                hit.m_damage.Modify(reduction);
                            }
                        }
                    }

                    // === 공격 측: 공격자가 버서커 플레이어 (Lv4 저체력 공격력 보너스) ===
                    var localPlayer = Player.m_localPlayer;
                    if (localPlayer != null && !(__instance is Player) && HasBerserkerSkill(localPlayer))
                    {
                        var attacker = hit.GetAttacker();
                        if (attacker == localPlayer)
                        {
                            int bLvAtk = SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 0;
                            if (bLvAtk >= 4)
                            {
                                float hpPct = localPlayer.GetHealthPercentage() * 100f;
                                if (hpPct <= Berserker_Config.BerserkerLv4LowHpAttackThresholdValue)
                                {
                                    float bonus = 1f + Berserker_Config.BerserkerLv4LowHpAttackBonusValue / 100f;
                                    hit.m_damage.Modify(bonus);
                                    CreateMonsterHitEffect(__instance);
                                }
                            }

                            // 분노 데미지 보너스 (공격자가 분노 상태일 때)
                            if (IsPlayerInRage(localPlayer))
                            {
                                float damageBonus = GetRageDamageBonus(localPlayer);
                                if (damageBonus > 0f)
                                {
                                    hit.m_damage.Modify(1f + damageBonus / 100f);
                                    CreateMonsterHitEffect(__instance);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[버서커 패치] Damage 패치 실패: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 피격 후 실제 체력 기준으로 죽음의 무시 발동 체크
        /// 예측 방식 대신 실제 HP 기반으로 정확한 발동 (오발동 방지)
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Berserker_Character_DamageCheck_Patch
        {
            public static void Postfix(Character __instance)
            {
                try
                {
                    if (__instance is Player player && HasBerserkerSkill(player))
                    {
                        // 이미 사망했으면 체크 스킵 (다음 공격 대비 HP 임계값 준비 용도)
                        if (player.IsDead() || player.GetHealth() <= 0f) return;
                        CheckBerserkerPassiveSkill(player);
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[버서커 패시브] 피격 후 체크 실패: {ex.Message}");
                }
            }
        }

        // Player Update 패치 제거 - 무한 로딩 원인
        // 분노 종료는 Coroutine으로 처리하거나 시간 제한 없이 영구 적용

        #endregion

        #region Cleanup

        [HarmonyPatch(typeof(Player), "OnDestroy")]
        public static class Berserker_Player_OnDestroy_Patch
        {
            static void Postfix(Player __instance)
            {
                if (__instance == null) return;
                if (rageStates.ContainsKey(__instance))
                {
                    // VFX 인스턴스 먼저 파괴
                    if (rageBuffVFXInstances.TryGetValue(__instance, out var vfx) && vfx != null)
                        UnityEngine.Object.Destroy(vfx);
                    rageBuffVFXInstances.Remove(__instance);
                    rageStates.Remove(__instance);
                }
                passiveStates.Remove(__instance);
            }
        }

        #endregion
    }
}

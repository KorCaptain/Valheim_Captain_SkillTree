using EpicMMOSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기(Mace) 액티브 스킬 전용 클래스
    /// - 수호자의 진심 (Guardian Heart) G키 액티브 스킬
    /// </summary>
    public static partial class SkillEffect
    {
        // ==================== 수호자의 진심 (Guardian Heart) ====================

        // 수호자의 진심 상태 관리
        private static Dictionary<Player, float> guardianHeartCooldowns = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> guardianHeartActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> guardianHeartExpiry = new Dictionary<Player, float>();

        // 원본 데미지 저장 (막기 전 데미지)
        private static Dictionary<Player, HitData.DamageTypes> guardianHeartOriginalDamage = new Dictionary<Player, HitData.DamageTypes>();

        // 머리 위 상태 효과 GameObject 추적
        private static Dictionary<Player, GameObject> guardianHeartStatusEffects = new Dictionary<Player, GameObject>();
        private static GameObject cachedGuardianHeartStatusPrefab = null;

        /// <summary>
        /// 수호자의 진심 액티브 스킬 발동
        /// G키로 활성화, 방패 막기 시 반사 데미지 버프 적용
        /// </summary>
        public static void ActivateGuardianHeart(Player player)
        {
            try
            {
                if (player == null || player.IsDead())
                {
                    return;
                }

                // 1. 스킬 보유 확인
                bool hasSkill = HasSkill("mace_Step7_guardian_heart");
                if (!hasSkill)
                {
                    DrawFloatingText(player, "수호자의 진심 스킬이 필요합니다", Color.red);
                    return;
                }

                // 2. 방패 + 한손둔기 착용 확인
                bool isUsingShield = HasShield(player);
                bool isUsingOneHandedMace = IsUsingOneHandedMace(player);
                if (!isUsingShield || !isUsingOneHandedMace)
                {
                    DrawFloatingText(player, "둔기 + 방패를 착용해야 합니다!", Color.red);
                    return;
                }

                // 3. 쿨타임 확인
                float now = Time.time;
                if (guardianHeartCooldowns.ContainsKey(player) && now < guardianHeartCooldowns[player])
                {
                    float remaining = guardianHeartCooldowns[player] - now;
                    DrawFloatingText(player, $"쿨타임: {Mathf.CeilToInt(remaining)}초", Color.yellow);
                    return;
                }

                // 4. 스태미나 소모 확인
                float requiredStamina = Mace_Config.GuardianHeartStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, "스태미나 부족", Color.red);
                    return;
                }

                // 5. 스킬 발동
                ExecuteGuardianHeart(player);

                // 6. 쿨타임 및 스태미나 소모 적용
                guardianHeartCooldowns[player] = now + Mace_Config.GuardianHeartCooldownValue;
                player.UseStamina(requiredStamina);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 수호자의 진심 실제 실행 로직
        /// </summary>
        private static void ExecuteGuardianHeart(Player player)
        {
            try
            {
                // 1. 버프 활성화
                float duration = Mace_Config.GuardianHeartDurationValue;
                guardianHeartActive[player] = true;
                guardianHeartExpiry[player] = Time.time + duration;

                // 2. 버프 표시
                SkillBuffDisplay.Instance.ShowBuff(
                    "guardian_heart",
                    "수호자의 진심",
                    duration,
                    new Color(0.2f, 0.8f, 1f, 1f), // 파란색
                    "🛡️"
                );

                // 3. 플로팅 텍스트
                DrawFloatingText(player, "[수호자의 진심] 반사 보호 활성화!", new Color(0.2f, 0.8f, 1f, 1f));

                // 3-1. 머리 위 상태 효과 VFX 재생 (45초 지속)
                PlayGuardianHeartStatusEffect(player);

                // 4. 버프 유지 코루틴 시작
                player.StartCoroutine(GuardianHeartBuffCoroutine(player, duration));
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심 실행] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 수호자의 진심 시작 VFX 효과
        /// </summary>
        /// <summary>
        /// 수호자의 진심 머리 위 상태 효과 재생 (statusailment_01_aura - 45초 지속)
        /// </summary>
        private static void PlayGuardianHeartStatusEffect(Player player)
        {
            try
            {
                // 캐시된 프리팹이 없으면 한 번만 로드
                if (cachedGuardianHeartStatusPrefab == null)
                {
                    cachedGuardianHeartStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedGuardianHeartStatusPrefab != null)
                    {
                        Plugin.Log.LogInfo("[수호자의 진심] statusailment_01_aura 프리팹 캐시됨");
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[수호자의 진심] VFXManager에서 statusailment_01_aura를 찾을 수 없음");
                        return;
                    }
                }

                // 기존 상태 효과가 있으면 제거
                if (guardianHeartStatusEffects.ContainsKey(player) && guardianHeartStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(guardianHeartStatusEffects[player]);
                    guardianHeartStatusEffects.Remove(player);
                    Plugin.Log.LogInfo("[수호자의 진심] 기존 상태 효과 제거");
                }

                // statusailment_01_aura 효과 생성 (머리 위에서 45초 지속)
                if (cachedGuardianHeartStatusPrefab != null)
                {
                    // 머리 위 위치 계산 (1.4m 위)
                    var headPosition = player.transform.position + Vector3.up * 1.4f;
                    var statusInstance = UnityEngine.Object.Instantiate(cachedGuardianHeartStatusPrefab, headPosition, Quaternion.identity);

                    // 플레이어를 따라다니도록 부모 설정
                    statusInstance.transform.SetParent(player.transform, false);
                    statusInstance.transform.localPosition = Vector3.up * 1.4f; // 머리 위 고정

                    // 크기 조정 (1배)
                    statusInstance.transform.localScale = Vector3.one * 1f;

                    // 상태 효과 인스턴스 저장 (버프 종료 시 제거)
                    guardianHeartStatusEffects[player] = statusInstance;

                    Plugin.Log.LogInfo("[수호자의 진심] statusailment_01_aura 상태 효과 재생 완료 (머리 위 1.4m, 플레이어 추적)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심] 상태 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 수호자의 진심 버프 유지 코루틴
        /// </summary>
        private static IEnumerator GuardianHeartBuffCoroutine(Player player, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration && guardianHeartActive.ContainsKey(player) && guardianHeartActive[player])
            {
                // 플레이어 사망 체크
                if (player == null || player.IsDead())
                {
                    // 버프 상태 정리
                    if (guardianHeartActive.ContainsKey(player))
                    {
                        guardianHeartActive[player] = false;
                    }
                    if (guardianHeartExpiry.ContainsKey(player))
                    {
                        guardianHeartExpiry.Remove(player);
                    }

                    // 상태 효과 제거
                    if (guardianHeartStatusEffects.ContainsKey(player) && guardianHeartStatusEffects[player] != null)
                    {
                        UnityEngine.Object.Destroy(guardianHeartStatusEffects[player]);
                        guardianHeartStatusEffects.Remove(player);
                        Plugin.Log.LogInfo("[수호자의 진심] 사망 시 상태 효과 제거");
                    }

                    yield break;
                }

                yield return new WaitForSeconds(1f);
                elapsed += 1f;

                // 중간 알림 (15초마다)
                if (elapsed % 15f < 1f)
                {
                    float remaining = duration - elapsed;
                    if (remaining > 0)
                    {
                        try
                        {
                            DrawFloatingText(player, $"🛡️ 수호자의 진심: {remaining:F0}초", new Color(0.2f, 0.8f, 1f, 1f));
                        }
                        catch (System.Exception ex)
                        {
                            Plugin.Log.LogError($"[수호자의 진심] 플로팅 텍스트 오류: {ex.Message}");
                        }
                    }
                }
            }

            // 버프 종료
            try
            {
                if (guardianHeartActive.ContainsKey(player))
                {
                    guardianHeartActive[player] = false;
                }
                if (guardianHeartExpiry.ContainsKey(player))
                {
                    guardianHeartExpiry.Remove(player);
                }

                // 상태 효과 GameObject 제거
                if (guardianHeartStatusEffects.ContainsKey(player) && guardianHeartStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(guardianHeartStatusEffects[player]);
                    guardianHeartStatusEffects.Remove(player);
                    Plugin.Log.LogInfo("[수호자의 진심] 버프 종료 - 상태 효과 제거");
                }

                DrawFloatingText(player, "수호자의 진심 효과 종료", Color.gray);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심] 버프 종료 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 수호자의 진심 버프가 활성화되어 있는지 확인
        /// </summary>
        public static bool IsGuardianHeartActive(Player player)
        {
            if (player == null) return false;

            return guardianHeartActive.ContainsKey(player) &&
                   guardianHeartActive[player] &&
                   guardianHeartExpiry.ContainsKey(player) &&
                   Time.time < guardianHeartExpiry[player];
        }

        /// <summary>
        /// 원본 데미지 저장 (막기 처리 전에 호출)
        /// </summary>
        public static void SaveOriginalDamage(Player player, HitData hit)
        {
            if (player == null || hit == null) return;

            // 버프 활성화 중에만 저장
            if (IsGuardianHeartActive(player))
            {
                // HitData.DamageTypes는 구조체이므로 자동으로 복사됨
                guardianHeartOriginalDamage[player] = hit.m_damage;
            }
        }

        /// <summary>
        /// 저장된 원본 데미지 가져오기 및 제거
        /// </summary>
        public static bool TryGetOriginalDamage(Player player, out HitData.DamageTypes originalDamage)
        {
            if (player != null && guardianHeartOriginalDamage.ContainsKey(player))
            {
                originalDamage = guardianHeartOriginalDamage[player];
                guardianHeartOriginalDamage.Remove(player); // 사용 후 제거
                return true;
            }

            originalDamage = new HitData.DamageTypes();
            return false;
        }

        /// <summary>
        /// 방패 막기 시 반사 데미지 적용 (MMO getParameter 패치에서 호출)
        /// </summary>
        public static void ApplyGuardianHeartReflectDamage(Player player, Character attacker, HitData hit)
        {
            try
            {
                Plugin.Log.LogInfo($"[수호자의 진심] ApplyGuardianHeartReflectDamage 호출됨");

                if (!IsGuardianHeartActive(player))
                {
                    Plugin.Log.LogInfo($"[수호자의 진심] 버프 비활성화 상태로 반사 스킵");
                    return;
                }

                if (attacker == null || attacker == player)
                {
                    Plugin.Log.LogInfo($"[수호자의 진심] 공격자 없음 또는 자기 자신 - 반사 스킵");
                    return;
                }

                // 반사 데미지 계산 (막기 전 저장된 원본 데미지 사용)
                float reflectPercent = Mace_Config.GuardianHeartReflectPercentValue / 100f;

                // 저장된 원본 데미지 가져오기
                float originalDamage = 0f;

                if (TryGetOriginalDamage(player, out HitData.DamageTypes originalDamageTypes))
                {
                    // 저장된 원본 데미지 사용 (Rule 11 준수 - 모든 데미지 타입)
                    originalDamage = originalDamageTypes.m_blunt + originalDamageTypes.m_slash +
                                   originalDamageTypes.m_pierce + originalDamageTypes.m_chop +
                                   originalDamageTypes.m_pickaxe + originalDamageTypes.m_fire +
                                   originalDamageTypes.m_frost + originalDamageTypes.m_lightning +
                                   originalDamageTypes.m_poison + originalDamageTypes.m_spirit;
                }
                else
                {
                    // 저장된 데이터 없음 - 현재 HitData 사용 (폴백)
                    originalDamage = hit.GetTotalDamage();
                }

                float reflectDamage = originalDamage * reflectPercent;

                // 최소 반사 데미지 보장 (원본 데미지가 있을 때만)
                if (originalDamage > 0f && reflectDamage < 1f)
                {
                    reflectDamage = 1f;
                }

                // 반사 데미지 적용 (Tanker 어그로 코드 참고하여 완전한 HitData 구성)
                var reflectHit = new HitData();
                reflectHit.m_damage.m_blunt = reflectDamage; // 블런트(둔기) 데미지로 반사
                reflectHit.m_attacker = player.GetZDOID();
                reflectHit.m_point = attacker.GetCenterPoint(); // 정확한 충돌 지점
                reflectHit.m_dir = (attacker.transform.position - player.transform.position).normalized;
                reflectHit.m_skill = Skills.SkillType.Clubs; // 둔기 스킬
                reflectHit.m_pushForce = 0f; // 밀침 없음
                reflectHit.m_blockable = false; // 막을 수 없음
                reflectHit.m_dodgeable = false; // 회피 불가
                reflectHit.m_ranged = false; // 근접 공격
                reflectHit.m_staggerMultiplier = 0f; // 스태거 없음
                reflectHit.m_toolTier = 0; // 무기 티어 없음

                Plugin.Log.LogInfo($"[수호자의 진심] 반사 데미지 적용: {reflectDamage:F0}, 공격자 위치: {attacker.transform.position}");

                attacker.Damage(reflectHit);

                // 반사 효과 표시
                Plugin.Log.LogInfo($"[수호자의 진심] VFX 재생 직전 - 플레이어 방패 위치");
                PlayGuardianHeartReflectEffect(player);
                DrawFloatingText(player, $"⚡ 반사: {reflectDamage:F0}", new Color(1f, 0.5f, 0f, 1f));
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심] 반사 데미지 적용 오류: {ex.Message}\n스택: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// 반사 데미지 VFX 효과 (방패 앞 위치)
        /// </summary>
        private static void PlayGuardianHeartReflectEffect(Player player)
        {
            try
            {
                // 방패 위치 계산: 플레이어 앞 + 왼쪽 오프셋 + 높이
                Vector3 shieldPosition = player.transform.position +
                                        player.transform.forward * 0.5f +    // 앞쪽 0.5m
                                        -player.transform.right * 0.3f +     // 왼쪽 0.3m (방패 위치)
                                        Vector3.up * 1.2f;                   // 위쪽 1.2m (가슴 높이)

                Plugin.Log.LogInfo($"[수호자의 진심] 막기 반사 VFX 재생 시도 - 방패 위치: {shieldPosition}");

                // ✅ 막을 시 guard_01 VFX 재생 (방패 앞 위치)
                VFXManager.PlayVFXMultiplayer(
                    "guard_01",  // 파란색 원형 물파장 효과
                    "",  // 사운드 없음
                    shieldPosition,
                    Quaternion.identity,
                    1.5f,
                    1f  // 크기 1배
                );

                Plugin.Log.LogInfo($"[수호자의 진심] guard_01 VFX 재생 완료 (방패 앞)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심 반사] VFX 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 수호자의 진심 정리 메서드 (플레이어 사망 시 호출)
        /// Dictionary 정리 (Valheim 기본 VFX 사용으로 GameObject 정리 불필요)
        /// </summary>
        public static void CleanupGuardianHeartOnDeath(Player player)
        {
            try
            {
                // 상태 효과 GameObject 제거
                if (guardianHeartStatusEffects.ContainsKey(player) && guardianHeartStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(guardianHeartStatusEffects[player]);
                    guardianHeartStatusEffects.Remove(player);
                    Plugin.Log.LogInfo("[수호자의 진심] 사망 시 상태 효과 GameObject 제거");
                }

                // 상태 초기화 (5개 Dictionary)
                guardianHeartActive.Remove(player);
                guardianHeartCooldowns.Remove(player);
                guardianHeartExpiry.Remove(player);
                guardianHeartOriginalDamage.Remove(player);
                guardianHeartStatusEffects.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[수호자의 진심] 정리 실패: {ex.Message}");
            }
        }
    }
}

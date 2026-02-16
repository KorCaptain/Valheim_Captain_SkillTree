using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 아처 직업 전용 패시브 스킬 시스템
    /// 점프 높이 증가 및 낙사 데미지 감소
    /// </summary>
    public static class ArcherSkills
    {
        /// <summary>
        /// 아처 패시브 스킬을 SkillTreeManager에 등록
        /// </summary>
        public static void RegisterArcherPassiveSkills()
        {
            Plugin.Log.LogInfo("[아처 패시브] 패시브 스킬 등록 완료");
        }

        /// <summary>
        /// 아처인지 확인
        /// </summary>
        public static bool IsArcher(Player player)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                return manager != null && manager.GetSkillLevel("Archer") > 0;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 패시브] 아처 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 아처 점프 높이 보너스 가져오기
        /// </summary>
        public static float GetArcherJumpHeightBonus(Player player)
        {
            if (!IsArcher(player)) return 0f;
            
            return Archer_Config.ArcherJumpHeightBonusValue / 100f; // 퍼센트를 소수로 변환
        }

        /// <summary>
        /// 아처 낙사 데미지 감소 가져오기
        /// </summary>
        public static float GetArcherFallDamageReduction(Player player)
        {
            if (!IsArcher(player)) return 0f;
            
            return Archer_Config.ArcherFallDamageReductionValue / 100f; // 퍼센트를 소수로 변환
        }
    }

    /// <summary>
    /// 아처 점프 높이 증가 패치 - 물리 기반 접근법
    /// Character.Jump 메서드 Postfix에서 Rigidbody velocity 직접 조작
    /// </summary>
    [HarmonyPatch(typeof(Character), "Jump")]
    public static class ArcherJumpHeightPatch
    {
        static void Postfix(Character __instance, bool force)
        {
            try
            {
                // 플레이어가 아니면 무시
                if (!(__instance is Player player))
                    return;

                // 아처가 아니면 무시
                if (!ArcherSkills.IsArcher(player))
                    return;

                // 점프가 실제로 실행되었는지 확인 (지상에 있지 않고 위쪽 속도가 있는 경우)
                var rigidbody = __instance.GetComponent<Rigidbody>();
                if (rigidbody == null)
                    return;

                // 아처 점프 높이 보너스 적용
                float jumpBonus = ArcherSkills.GetArcherJumpHeightBonus(player);
                if (jumpBonus > 0f)
                {
                    // 현재 속도 벡터 가져오기
                    Vector3 currentVelocity = rigidbody.velocity;
                    
                    // Y축(수직) 속도가 양수이고 플레이어가 지상에 있지 않을 때만 적용
                    if (currentVelocity.y > 0.1f && !player.IsOnGround())
                    {
                        // Y축 속도에만 보너스 적용
                        currentVelocity.y *= (1f + jumpBonus);
                        rigidbody.velocity = currentVelocity;
                        
                        Plugin.Log.LogDebug($"[아처 패시브] {player.GetPlayerName()} 점프 높이 보너스 적용: +{jumpBonus * 100f}% (Y속도: {currentVelocity.y / (1f + jumpBonus):F2} → {currentVelocity.y:F2})");
                        
                        // 시각적 피드백 (선택적)
                        player.Message(MessageHud.MessageType.TopLeft, L.Get("archer_jump_bonus", (jumpBonus * 100f).ToString("F0")));
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 패시브] 점프 높이 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 아처 낙사 데미지 감소 패치
    /// Character.UpdateGroundContact에서 낙사 데미지 감소
    /// </summary>
    [HarmonyPatch(typeof(Character), "UpdateGroundContact")]
    public static class ArcherFallDamagePatch
    {
        static void Prefix(Character __instance)
        {
            try
            {
                // 플레이어가 아니면 무시
                if (!(__instance is Player player))
                    return;

                // 아처가 아니면 무시
                if (!ArcherSkills.IsArcher(player))
                    return;

                // 지상에 있으면 무시
                if (player.IsOnGround())
                    return;

                // 아처 낙사 데미지 감소 플래그 설정
                ArcherFallDamageReductionPatch.SetArcherFallDamageReduction(player.GetPlayerID().ToString(), true);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 패시브] 낙사 데미지 전처리 오류: {ex.Message}");
            }
        }

        static void Postfix(Character __instance)
        {
            try
            {
                // 플레이어가 아니면 무시
                if (!(__instance is Player player))
                    return;

                // 지상에 닿았으면 낙사 데미지 감소 플래그 해제
                if (player.IsOnGround())
                {
                    ArcherFallDamageReductionPatch.SetArcherFallDamageReduction(player.GetPlayerID().ToString(), false);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 패시브] 낙사 데미지 후처리 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 아처 낙사 데미지 감소 패치
    /// Character.Damage에서 낙사 데미지 감소 적용
    /// </summary>
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class ArcherFallDamageReductionPatch
    {
        /// <summary>
        /// 플레이어별 낙사 데미지 감소 상태 관리
        /// Key: PlayerID, Value: 낙사 데미지 감소 활성화 여부
        /// </summary>
        private static System.Collections.Generic.Dictionary<string, bool> fallDamageReductionStates = 
            new System.Collections.Generic.Dictionary<string, bool>();

        /// <summary>
        /// 플레이어의 낙사 데미지 감소 상태 설정
        /// </summary>
        public static void SetArcherFallDamageReduction(string playerKey, bool active)
        {
            try
            {
                if (active)
                {
                    fallDamageReductionStates[playerKey] = true;
                }
                else
                {
                    fallDamageReductionStates.Remove(playerKey);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 낙사 데미지] 상태 설정 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// Character.Damage 실행 전 호출 - 아처 낙사 데미지 감소 적용
        /// </summary>
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                // 플레이어가 아니면 무시
                if (!(__instance is Player player))
                    return;

                // 아처가 아니면 무시
                if (!ArcherSkills.IsArcher(player))
                    return;

                string playerKey = player.GetPlayerID().ToString();

                // 낙사 데미지 감소 상태가 활성화되어 있는지 확인
                if (fallDamageReductionStates.TryGetValue(playerKey, out bool isActive) && isActive)
                {
                    // 낙사 데미지인지 확인 (attacker가 없고 데미지가 있는 경우)
                    if (hit.m_attacker.IsNone() && hit.GetTotalDamage() > 0f)
                    {
                        // 원래 데미지 저장
                        var originalDamage = hit.GetTotalDamage();

                        if (originalDamage > 0f)
                        {
                            // 낙사 데미지 감소 적용
                            float reductionAmount = ArcherSkills.GetArcherFallDamageReduction(player);
                            float reductionMultiplier = 1f - reductionAmount; // 감소율 적용

                            // 모든 데미지 타입에 감소 적용
                            hit.m_damage.m_damage *= reductionMultiplier;
                            hit.m_damage.m_blunt *= reductionMultiplier;
                            hit.m_damage.m_slash *= reductionMultiplier;
                            hit.m_damage.m_pierce *= reductionMultiplier;
                            hit.m_damage.m_chop *= reductionMultiplier;
                            hit.m_damage.m_pickaxe *= reductionMultiplier;
                            hit.m_damage.m_fire *= reductionMultiplier;
                            hit.m_damage.m_frost *= reductionMultiplier;
                            hit.m_damage.m_lightning *= reductionMultiplier;
                            hit.m_damage.m_poison *= reductionMultiplier;
                            hit.m_damage.m_spirit *= reductionMultiplier;

                            var reducedDamage = hit.GetTotalDamage();
                            float damageReduced = originalDamage - reducedDamage;

                            Plugin.Log.LogInfo($"[아처 낙사 데미지] {player.GetPlayerName()} 낙사 데미지 감소 적용 - 원래: {originalDamage:F1} → 감소후: {reducedDamage:F1} (감소량: {damageReduced:F1})");

                            // 플레이어에게 낙사 데미지 감소 메시지 표시
                            player.Message(MessageHud.MessageType.TopLeft, L.Get("archer_fall_damage_reduced", damageReduced.ToString("F0")));
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 낙사 데미지] Damage 패치 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 모든 낙사 데미지 감소 상태 정리 (플러그인 종료시)
        /// </summary>
        public static void ClearAllStates()
        {
            fallDamageReductionStates.Clear();
            Plugin.Log.LogInfo("[아처 낙사 데미지] 모든 상태 정리 완료");
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 탱커 도발 반사데미지 시스템
    /// 전장의 함성(Y키) 발동 시 버프 지속시간 동안 피격 시 공격자에게 반사데미지 적용
    /// TankerSkills.cs 분리 (800줄 제한)
    /// </summary>
    public static class TankerReflect
    {
        // === 반사 상태 관리 (플래그 기반) ===
        private static HashSet<long> tankerReflectActive = new HashSet<long>();
        private static Dictionary<long, float> tankerReflectEndTime = new Dictionary<long, float>();
        private static Dictionary<Player, HitData.DamageTypes> tankerReflectOriginalDamage =
            new Dictionary<Player, HitData.DamageTypes>();

        /// <summary>
        /// 탱커 반사 버프 활성화 (도발 발동 시 호출)
        /// </summary>
        public static void ActivateTankerReflect(Player player, float duration)
        {
            if (player == null) return;

            long playerId = player.GetPlayerID();
            tankerReflectActive.Add(playerId);
            tankerReflectEndTime[playerId] = Time.time + duration;

            Plugin.Log.LogDebug($"[탱커 반사] {player.GetPlayerName()} 반사 활성화 - {duration}초간");
        }

        /// <summary>
        /// 탱커 반사 버프 활성화 여부 확인 (SkillEffect.cs에서 호출)
        /// </summary>
        public static bool IsTankerReflectActive(Player player)
        {
            if (player == null) return false;

            long playerId = player.GetPlayerID();
            if (!tankerReflectActive.Contains(playerId)) return false;

            if (tankerReflectEndTime.TryGetValue(playerId, out float endTime))
            {
                if (Time.time > endTime)
                {
                    // 만료 시 자동 정리
                    tankerReflectActive.Remove(playerId);
                    tankerReflectEndTime.Remove(playerId);
                    return false;
                }
                return true;
            }

            tankerReflectActive.Remove(playerId);
            return false;
        }

        /// <summary>
        /// 탱커 반사 상태 초기화 (사망 시 등 호출)
        /// </summary>
        public static void ResetTankerReflect(Player player)
        {
            if (player == null) return;

            long playerId = player.GetPlayerID();
            tankerReflectActive.Remove(playerId);
            tankerReflectEndTime.Remove(playerId);
            tankerReflectOriginalDamage.Remove(player);
        }

        /// <summary>
        /// 원본 데미지 저장 (Character.Damage Prefix에서 막기 처리 전 호출)
        /// Plugin.Systems.cs에서 호출
        /// </summary>
        public static void SaveTankerReflectOriginalDamage(Player player, HitData hit)
        {
            if (player == null || hit == null) return;

            if (IsTankerReflectActive(player))
            {
                // HitData.DamageTypes는 구조체 - 자동으로 값 복사됨
                tankerReflectOriginalDamage[player] = hit.m_damage;
            }
        }

        /// <summary>
        /// 저장된 원본 데미지 가져오기 및 제거
        /// </summary>
        private static bool TryGetTankerReflectOriginalDamage(Player player, out HitData.DamageTypes originalDamage)
        {
            if (player != null && tankerReflectOriginalDamage.ContainsKey(player))
            {
                originalDamage = tankerReflectOriginalDamage[player];
                tankerReflectOriginalDamage.Remove(player);
                return true;
            }

            originalDamage = new HitData.DamageTypes();
            return false;
        }

        /// <summary>
        /// 탱커 반사 데미지 적용 (SkillEffect.cs ApplyDamage Postfix에서 호출)
        /// 방패 막기 조건 없음 - 도발 버프 활성 중 피격 시 무조건 반사
        /// </summary>
        public static void ApplyTankerReflectDamage(Player player, Character attacker)
        {
            try
            {
                if (!IsTankerReflectActive(player)) return;
                if (attacker == null || attacker == player) return;

                // 반사 비율 가져오기
                float reflectPercent = Tanker_Config.TankerTauntReflectPercentValue / 100f;

                // 저장된 원본 데미지 복원 (모든 10개 타입 합산 - Rule 11)
                float originalDamage = 0f;
                if (TryGetTankerReflectOriginalDamage(player, out HitData.DamageTypes originalDamageTypes))
                {
                    originalDamage = originalDamageTypes.m_blunt + originalDamageTypes.m_slash +
                                     originalDamageTypes.m_pierce + originalDamageTypes.m_chop +
                                     originalDamageTypes.m_pickaxe + originalDamageTypes.m_fire +
                                     originalDamageTypes.m_frost + originalDamageTypes.m_lightning +
                                     originalDamageTypes.m_poison + originalDamageTypes.m_spirit;
                }

                float reflectDamage = originalDamage * reflectPercent;

                // 최소 반사 데미지 보장
                if (originalDamage > 0f && reflectDamage < 1f)
                    reflectDamage = 1f;

                if (reflectDamage <= 0f) return;

                // HitData 구성 (수호자의 진심과 동일 패턴)
                var reflectHit = new HitData();
                reflectHit.m_damage.m_blunt = reflectDamage;
                reflectHit.m_attacker = player.GetZDOID();
                reflectHit.m_point = attacker.GetCenterPoint();
                reflectHit.m_dir = (attacker.transform.position - player.transform.position).normalized;
                reflectHit.m_skill = Skills.SkillType.Clubs;
                reflectHit.m_pushForce = 0f;
                reflectHit.m_blockable = false;
                reflectHit.m_dodgeable = false;
                reflectHit.m_ranged = false;
                reflectHit.m_staggerMultiplier = 0f;
                reflectHit.m_toolTier = 0;

                // 반사 데미지 적용
                attacker.Damage(reflectHit);

                // VFX + 플로팅 텍스트
                SimpleVFX.Play("guard_01", player.GetCenterPoint(), 1.5f);
                SkillEffect.DrawFloatingText(player, L.Get("tanker_reflect_damage", $"{reflectDamage:F0}"), new Color(1f, 0.5f, 0f, 1f));

                Plugin.Log.LogDebug($"[탱커 반사] {attacker.name}에게 {reflectDamage:F0} 반사 데미지 적용");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[탱커 반사] 반사 데미지 오류: {ex.Message}");
            }
        }
    }
}

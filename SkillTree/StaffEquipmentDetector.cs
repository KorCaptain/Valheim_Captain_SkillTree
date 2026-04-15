using UnityEngine;
using CaptainSkillTree;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.Localization;
using static CaptainSkillTree.SkillTree.SkillEffect;
using System.Collections.Generic;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 지팡이/완드 착용 감지 최적화 유틸리티
    /// - 인벤토리 전체 스캔 대신 현재 착용 무기만 직접 확인
    /// - 성능 최적화: O(n) → O(1) 복잡도 개선 + 캐싱 시스템
    /// - 모든 지팡이 전문가 스킬에서 통합 사용
    /// - 이벤트 기반 캐싱으로 불필요한 반복 체크 방지
    /// </summary>
    public static class StaffEquipmentDetector
    {
        // 착용 상태 캐시 시스템 (플레이어별 캐싱)
        private static Dictionary<long, bool> playerStaffEquipCache = new Dictionary<long, bool>();
        private static Dictionary<long, string> playerLastWeaponName = new Dictionary<long, string>();
        private static Dictionary<long, float> playerLastCheckTime = new Dictionary<long, float>();
        private const float MIN_CHECK_INTERVAL = 0.2f; // 최소 0.2초 간격으로만 체크
        /// <summary>
        /// 현재 지팡이 또는 완드를 착용하고 있는지 확인 (캐싱 최적화 버전)
        /// - 캐시 우선 확인으로 불필요한 반복 체크 방지
        /// - 무기 변경 시에만 실제 검사 수행
        /// </summary>
        public static bool IsWieldingStaffOrWand(Player player)
        {
            if (player == null) return false;

            long playerId = player.GetPlayerID();
            float currentTime = Time.time;

            // 시간 기반 체크 제한: 최소 간격보다 짧으면 캐시된 결과 반환
            if (playerLastCheckTime.TryGetValue(playerId, out float lastCheckTime) &&
                currentTime - lastCheckTime < MIN_CHECK_INTERVAL &&
                playerStaffEquipCache.TryGetValue(playerId, out bool timedCachedResult))
            {
                return timedCachedResult;
            }

            var currentWeapon = player.GetCurrentWeapon();
            string currentWeaponName = currentWeapon?.m_shared?.m_name ?? "None";

            // 무기 변경 기반 캐시 확인: 무기가 변경되지 않았다면 캐시된 결과 반환
            if (playerLastWeaponName.TryGetValue(playerId, out string lastWeaponName) &&
                lastWeaponName == currentWeaponName &&
                playerStaffEquipCache.TryGetValue(playerId, out bool weaponCachedResult))
            {
                playerLastCheckTime[playerId] = currentTime; // 체크 시간 업데이트
                return weaponCachedResult;
            }

            // 실제 검사 수행 및 캐시 업데이트
            bool isStaffEquipped = IsWieldingStaffOrWandInternal(player, currentWeapon);
            playerStaffEquipCache[playerId] = isStaffEquipped;
            playerLastWeaponName[playerId] = currentWeaponName;
            playerLastCheckTime[playerId] = currentTime;

            return isStaffEquipped;
        }

        /// <summary>
        /// 실제 지팡이/완드 감지 로직 (내부 메서드)
        /// </summary>
        private static bool IsWieldingStaffOrWandInternal(Player player, ItemDrop.ItemData currentWeapon)
        {
            try
            {
                if (currentWeapon?.m_shared == null)
                {
                    return false;
                }

                // 1순위: Valheim 기본 ElementalMagic/BloodMagic 스킬 타입 (가장 정확한 방법)
                if (currentWeapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic ||
                    currentWeapon.m_shared.m_skillType == Skills.SkillType.BloodMagic)
                {
                    Plugin.Log.LogInfo($"[지팡이 착용 감지] Magic 타입 지팡이 착용: {currentWeapon.m_shared.m_name}");
                    return true;
                }

                // 2순위: 프리팹 이름 패턴 매칭
                string prefabName = currentWeapon.m_dropPrefab?.name?.ToLower() ?? "";
                if (prefabName.Contains("staff") || prefabName.Contains("wand") || prefabName.Contains("rod"))
                {
                    Plugin.Log.LogInfo($"[지팡이 착용 감지] 프리팹명으로 지팡이 감지: {prefabName} ({currentWeapon.m_shared.m_name})");
                    return true;
                }

                // 3순위: 무기 표시 이름 패턴 매칭 (다국어 및 커스텀 무기 지원)
                string weaponName = currentWeapon.m_shared.m_name?.ToLower() ?? "";
                var staffPatterns = new[]
                {
                    "staff", "wand", "rod", "scepter",           // 기본 영어 패턴
                    "지팡이", "완드", "막대",                      // 한글 패턴
                    "$item_staff", "$item_wand",                // Valheim 아이템 ID 패턴
                    "stafffire", "staffice", "stafflight",      // Valheim 기본 지팡이들
                    "보호의", "치료의", "마법의", "원소의",           // 한글 수식어
                    "protection", "healing", "magic", "elemental" // 영어 수식어
                };

                foreach (var pattern in staffPatterns)
                {
                    if (weaponName.Contains(pattern))
                    {
                        Plugin.Log.LogInfo($"[지팡이 착용 감지] 무기명 패턴으로 지팡이 감지: {currentWeapon.m_shared.m_name} (패턴: {pattern})");
                        return true;
                    }
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 착용 감지] 감지 중 오류 발생: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 지팡이/완드 미착용 시 플레이어에게 알림 표시
        /// </summary>
        public static void ShowStaffRequiredMessage(Player player)
        {
            if (player != null)
            {
                player.Message(MessageHud.MessageType.Center, L.Get("staff_wand_required"));
                SkillEffect.DrawFloatingText(player, L.Get("staff_wand_required"), Color.red);
            }
        }

        /// <summary>
        /// 현재 착용 중인 지팡이/완드의 기본 공격력 가져오기
        /// </summary>
        public static float GetCurrentStaffDamage(Player player)
        {
            try
            {
                var currentWeapon = player?.GetCurrentWeapon();
                if (currentWeapon?.m_shared == null) return 0f;

                // 지팡이인지 확인
                if (!IsWieldingStaffOrWand(player)) return 0f;

                // 기본 공격력 반환 (데미지 배열의 총합)
                float totalDamage = 0f;
                var damages = currentWeapon.GetDamage();

                totalDamage += damages.m_damage;          // 물리 데미지
                totalDamage += damages.m_blunt;           // 둔기 데미지
                totalDamage += damages.m_slash;           // 베기 데미지
                totalDamage += damages.m_pierce;          // 찌르기 데미지
                totalDamage += damages.m_fire;            // 화염 데미지
                totalDamage += damages.m_frost;           // 냉기 데미지
                totalDamage += damages.m_lightning;       // 번개 데미지
                totalDamage += damages.m_poison;          // 독 데미지
                totalDamage += damages.m_spirit;          // 영혼 데미지

                return totalDamage;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 데미지] 데미지 계산 중 오류: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 특정 플레이어의 캐시 클리어 (무기 변경 시 호출)
        /// </summary>
        public static void ClearPlayerCache(Player player)
        {
            if (player == null) return;

            long playerId = player.GetPlayerID();
            playerStaffEquipCache.Remove(playerId);
            playerLastWeaponName.Remove(playerId);
            playerLastCheckTime.Remove(playerId);
        }

        /// <summary>
        /// 모든 플레이어 캐시 클리어 (성능 최적화나 메모리 정리 시 호출)
        /// </summary>
        public static void ClearAllCache()
        {
            playerStaffEquipCache.Clear();
            playerLastWeaponName.Clear();
            playerLastCheckTime.Clear();
            Plugin.Log.LogInfo("[지팡이 착용 감지] 모든 플레이어 캐시 클리어 완료");
        }

        /// <summary>
        /// 캐시 상태 정보 로깅 (디버깅용)
        /// </summary>
        public static void LogCacheStatus()
        {
            Plugin.Log.LogInfo($"[지팡이 착용 감지] 캐시 상태 - 캐시된 플레이어 수: {playerStaffEquipCache.Count}, 체크 시간 기록: {playerLastCheckTime.Count}");

            foreach (var cache in playerStaffEquipCache)
            {
                long playerId = cache.Key;
                bool isEquipped = cache.Value;
                string weaponName = playerLastWeaponName.TryGetValue(playerId, out string weapon) ? weapon : "알 수 없음";
                float lastTime = playerLastCheckTime.TryGetValue(playerId, out float time) ? time : 0f;

                Plugin.Log.LogInfo($"  플레이어 {playerId}: 지팡이 착용={isEquipped}, 무기={weaponName}, 마지막체크={Time.time - lastTime:F1}초 전");
            }
        }

        /// <summary>
        /// 스킬 시전 시에만 지팡이 착용 상태를 확인하는 최적화 메서드
        /// - 일반 체크보다 더 엄격한 조건으로 성능 최적화
        /// </summary>
        public static bool IsWieldingStaffOrWandForSkill(Player player)
        {
            if (player == null) return false;

            // 스킬 시전 시에만 호출되므로 캐시 우선 확인
            long playerId = player.GetPlayerID();
            if (playerStaffEquipCache.TryGetValue(playerId, out bool cachedResult))
            {
                return cachedResult;
            }

            // 캐시가 없으면 일반 메서드 호출 (캐시 생성됨)
            return IsWieldingStaffOrWand(player);
        }
    }
}
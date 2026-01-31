using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// CaptainSkillTree 전용 Valheim 이동속도 시스템
    /// ModifyMovementSpeed.cs를 참고하여 Valheim API를 완전히 준수하는 구현
    /// </summary>
    public static class Speed
    {
        // 속도 효과 상태 캐시 (성능 최적화)
        private static readonly Dictionary<Player, float> _speedCache = new();
        private static float _lastUpdateTime = 0f;
        private static readonly float UPDATE_INTERVAL = 0.1f; // 100ms마다 업데이트
        
        /// <summary>
        /// 플레이어의 기본 스킬트리 이동속도 보너스 계산 (StatusEffect용 - 조건부 제외)
        /// </summary>
        public static float GetBaseSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            
            try
            {
                float totalBonus = 0f;
                
                // 속도 전문가 루트: 모든 이동속도 +5%
                if (SkillTreeManager.Instance?.GetSkillLevel("speed_root") > 0)
                {
                    totalBonus += SkillTreeConfig.SpeedRootMoveSpeedValue / 100f;
                }
                
                // 속도 트리 1단계: 민첩함의 기초 - 이동속도 +3%, 구르기 +10%
                if (SkillTreeManager.Instance?.GetSkillLevel("speed_base") > 0)
                {
                    totalBonus += SkillTreeConfig.SpeedBaseMoveSpeedValue / 100f;
                }
                
                Plugin.Log.LogDebug($"[Speed] 기본 속도 보너스: +{totalBonus * 100f:F1}%");
                return totalBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Speed] 기본 속도 계산 오류: {ex.Message}");
                return 0f;
            }
        }
        
        /// <summary>
        /// 플레이어의 총 스킬트리 이동속도 보너스 계산 (기본 + 조건부)
        /// </summary>
        public static float GetTotalSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            
            float baseBonus = GetBaseSpeedBonus(player);
            float conditionalBonus = GetConditionalSpeedBonus(player);
            
            return baseBonus + conditionalBonus;
        }
        
        /// <summary>
        /// 조건부 이동속도 보너스 계산 (중복 적용 방지) - MMO 시스템용 public 메서드
        /// </summary>
        public static float GetConditionalSpeedBonus(Player player)
        {
            float conditionalBonus = 0f;
            float currentTime = Time.time;

            try
            {
                // === 구르기 후 이동속도 (speed_base) - 다른 효과와 중첩 가능 ===
                float dodgeBonus = SkillEffect.GetDodgeSpeedBonus(player);
                if (dodgeBonus > 0f)
                {
                    conditionalBonus += dodgeBonus;
                    Plugin.Log.LogDebug($"[Speed] 민첩함의 기초 활성화: +{dodgeBonus * 100f:F1}%");
                }

                // === 무기별 조건부 보너스 (하나만 적용) ===
                bool hasWeaponBonus = false;

                // 우선순위 1: 석궁 숙련자 이동속도
                if (!hasWeaponBonus && SkillTreeManager.Instance?.GetSkillLevel("crossbow_reload2") > 0 &&
                    SkillEffect.crossbowExpertSpeedEndTime.TryGetValue(player, out float crossbowEndTime) &&
                    currentTime < crossbowEndTime)
                {
                    conditionalBonus += SkillTreeConfig.SpeedCrossbowExpertSpeedValue / 100f;
                    hasWeaponBonus = true;
                    Plugin.Log.LogDebug($"[Speed] 석궁 숙련자 활성화: +{SkillTreeConfig.SpeedCrossbowExpertSpeedValue}%");
                }

                // 우선순위 2: 이동 시전 (moving_cast) - 마법 시전 중 이동속도
                if (!hasWeaponBonus)
                {
                    float movingCastBonus = SkillEffect.GetMovingCastSpeedBonus(player);
                    if (movingCastBonus > 0f)
                    {
                        conditionalBonus += movingCastBonus;
                        hasWeaponBonus = true;
                        Plugin.Log.LogDebug($"[Speed] 이동 시전 활성화: +{movingCastBonus * 100f:F1}%");
                    }
                }

                // === 장전 중 이동속도 (중첩 가능) ===
                // 활 장전 중 이동속도 (bow_draw1)
                float bowDrawBonus = SkillEffect.GetBowDrawMoveSpeedBonus(player);
                if (bowDrawBonus > 0f)
                {
                    conditionalBonus += bowDrawBonus;
                    Plugin.Log.LogDebug($"[Speed] 활 가속 장전 중: +{bowDrawBonus * 100f:F1}%");
                }

                // 석궁 재장전 중 이동속도 (crossbow_draw1)
                float crossbowReloadBonus = SkillEffect.GetCrossbowReloadMoveSpeedBonus(player);
                if (crossbowReloadBonus > 0f)
                {
                    conditionalBonus += crossbowReloadBonus;
                    Plugin.Log.LogDebug($"[Speed] 석궁 가속 재장전 중: +{crossbowReloadBonus * 100f:F1}%");
                }

                // === 요툰의 방패: 방패 장착 시 이동속도 보너스 ===
                if (SkillTreeManager.Instance?.GetSkillLevel("defense_Step6_true") > 0)
                {
                    float shieldSpeedBonus = GetJotunnShieldSpeedBonus(player);
                    if (shieldSpeedBonus > 0f)
                    {
                        conditionalBonus += shieldSpeedBonus;
                        Plugin.Log.LogDebug($"[Speed] 요툰의 방패 활성화: +{shieldSpeedBonus * 100f:F1}%");
                    }
                }

                return conditionalBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Speed] 조건부 속도 계산 오류: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 요툰의 방패 이동속도 보너스 계산
        /// </summary>
        private static float GetJotunnShieldSpeedBonus(Player player)
        {
            try
            {
                var inventory = player.GetInventory();
                if (inventory == null) return 0f;

                // 방패 장착 확인
                var shieldItem = inventory.GetEquippedItems().FirstOrDefault(item =>
                    item.m_shared?.m_itemType == ItemDrop.ItemData.ItemType.Shield);

                if (shieldItem != null)
                {
                    // 방패 타입 확인: movementModifier로 구분
                    // 일반 방패: -0.05 (5% 이속 저하)
                    // 타워/대형 방패: -0.10 (10% 이속 저하)
                    float movementModifier = shieldItem.m_shared.m_movementModifier;

                    bool isTowerShield = movementModifier <= -0.08f; // -10% 이하면 대형 방패로 판단

                    if (isTowerShield)
                    {
                        // 대형 방패: +10% 이동속도
                        return Defense_Config.JotunnShieldTowerSpeedBonusValue / 100f;
                    }
                    else
                    {
                        // 일반 방패: +5% 이동속도
                        return Defense_Config.JotunnShieldNormalSpeedBonusValue / 100f;
                    }
                }

                return 0f;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Speed] 요툰의 방패 보너스 계산 오류: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 속도 효과 캐시 업데이트 (성능 최적화)
        /// </summary>
        private static void UpdateSpeedCache(Player player)
        {
            if (Time.time - _lastUpdateTime < UPDATE_INTERVAL) return;
            
            float newSpeed = GetTotalSpeedBonus(player);
            
            if (!_speedCache.TryGetValue(player, out float cachedSpeed) || 
                Mathf.Abs(newSpeed - cachedSpeed) > 0.001f)
            {
                _speedCache[player] = newSpeed;
                Plugin.Log.LogDebug($"[Speed] 캐시 업데이트: {player.GetPlayerName()} = +{newSpeed * 100f:F1}%");
            }
            
            _lastUpdateTime = Time.time;
        }
        
        /// <summary>
        /// 플레이어 로그아웃 시 캐시 정리
        /// </summary>
        public static void ClearPlayerCache(Player player)
        {
            if (player != null && _speedCache.ContainsKey(player))
            {
                _speedCache.Remove(player);
                Plugin.Log.LogDebug($"[Speed] 플레이어 {player.GetPlayerName()} 캐시 정리");
            }
        }
        
        /// <summary>
        /// 모든 속도 캐시 정리
        /// </summary>
        public static void ClearAllCache()
        {
            _speedCache.Clear();
            Plugin.Log.LogInfo("[Speed] 모든 속도 캐시 정리 완료");
        }
    }
}
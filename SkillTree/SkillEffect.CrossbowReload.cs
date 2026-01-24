using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 석궁 고속 장전 스킬 전용 패치 (crossbow_Step3_rapid)
    /// 속도 전문가 "석궁 가속"(AnimationSpeedManager)과 별도 메커니즘
    /// </summary>
    public static partial class SkillEffect
    {
        /// <summary>
        /// 플레이어별 자동 장전 버프 상태 추적
        /// 다음 1회 장전 시 200% 속도 (50% 시간) 적용
        /// </summary>
        private static Dictionary<Player, bool> autoReloadBuffActive = new Dictionary<Player, bool>();

        /// <summary>
        /// 플레이어별 최근 자동 장전 소모 시간 추적
        /// 연속 발사와 동시 발동 감지용
        /// </summary>
        private static Dictionary<Player, float> lastAutoReloadConsumedTime = new Dictionary<Player, float>();

        /// <summary>
        /// 자동 장전 버프 상태 설정
        /// </summary>
        public static void SetAutoReloadBuff(Player player, bool state)
        {
            if (player == null) return;
            autoReloadBuffActive[player] = state;
        }

        /// <summary>
        /// 자동 장전 버프 활성화 확인
        /// </summary>
        public static bool HasAutoReloadBuff(Player player)
        {
            if (player == null) return false;
            return autoReloadBuffActive.ContainsKey(player) && autoReloadBuffActive[player];
        }

        /// <summary>
        /// 최근 자동 장전이 소모된 시간 반환
        /// </summary>
        public static float GetLastAutoReloadConsumedTime(Player player)
        {
            if (player == null) return 0f;
            return lastAutoReloadConsumedTime.ContainsKey(player) ? lastAutoReloadConsumedTime[player] : 0f;
        }

        /// <summary>
        /// 자동 장전 소모 시간 설정
        /// </summary>
        public static void SetLastAutoReloadConsumedTime(Player player, float time)
        {
            if (player == null) return;
            lastAutoReloadConsumedTime[player] = time;
        }

        /// <summary>
        /// 석궁 장전 속도 보너스 계산 (고속 장전 스킬만)
        /// </summary>
        /// <param name="player">플레이어 인스턴스</param>
        /// <returns>장전 속도 보너스 (%)</returns>
        public static float GetCrossbowReloadSpeedBonus(Player player)
        {
            if (player == null) return 0f;

            float bonus = 0f;

            // 석궁 전문가 Tier 3: 고속 장전
            if (HasSkill("crossbow_Step3_rapid"))
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon?.m_shared.m_skillType == Skills.SkillType.Crossbows)
                {
                    bonus += Crossbow_Config.CrossbowRapidReloadSpeedValue;
                    Plugin.Log.LogDebug($"[고속 장전] +{Crossbow_Config.CrossbowRapidReloadSpeedValue}%");
                }
            }

            return bonus;
        }
    }

    /// <summary>
    /// 석궁 리로드 속도 패치 - ItemDrop.ItemData.GetWeaponLoadingTime Postfix
    /// 석궁의 실제 리로드 시간을 단축
    /// </summary>
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetWeaponLoadingTime))]
    public static class Crossbow_RapidReload_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
        {
            // 석궁 착용 확인
            if (__instance == null || __instance.m_shared.m_skillType != Skills.SkillType.Crossbows)
                return;

            // 리로드 무기인지 확인
            if (!__instance.m_shared.m_attack.m_requiresReload)
                return;

            var player = Player.m_localPlayer;
            if (player == null) return;

            float originalTime = __result;
            float totalSpeedBonus = 0f;
            bool autoReloadActive = false;

            // 자동 장전 버프 확인
            if (SkillEffect.HasAutoReloadBuff(player))
            {
                totalSpeedBonus += 200f; // 200% 속도 보너스
                autoReloadActive = true;
                SkillEffect.SetAutoReloadBuff(player, false); // 1회 사용 후 해제

                // 자동 장전 소모 시간 기록 (연속 발사와 동시 발동 감지용)
                SkillEffect.SetLastAutoReloadConsumedTime(player, Time.time);
            }

            // 고속 장전 스킬 확인 (자동 장전과 합산)
            if (SkillEffect.HasSkill("crossbow_Step3_rapid"))
            {
                totalSpeedBonus += Crossbow_Config.CrossbowRapidReloadSpeedValue;
            }

            // 총 속도 보너스 적용
            if (totalSpeedBonus > 0)
            {
                // 예: 210% 보너스 = 시간을 1/3.1 = 32.26%로 감소
                float multiplier = 1f / (1f + (totalSpeedBonus / 100f));
                __result *= multiplier;

                // 자동 장전 발동 시 텍스트 표시
                if (autoReloadActive)
                {
                    SkillEffect.ShowSkillEffectText(player,
                        $"⚡ 자동 장전! (+{totalSpeedBonus:F0}% 속도)",
                        new Color(1f, 0.8f, 0f),
                        SkillEffect.SkillEffectTextType.Combat);
                }
            }
        }
    }
}

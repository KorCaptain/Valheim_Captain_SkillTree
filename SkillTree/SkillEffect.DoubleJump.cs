using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 이중 점프 시스템 - 방어 전문가 최종 스킬에 적용
    /// </summary>
    public static partial class SkillEffect
    {
        // === 이중 점프 관련 변수 ===
        public static int multiJumpCombo = 0;
        public static Dictionary<Player, float> lastGroundTime = new Dictionary<Player, float>();
        
        /// <summary>
        /// 이중 점프 Harmony 패치
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.Jump))]
        public static class DoubleJump_Character_Jump_Patch
        {
            static bool Prepare()
            {
                Plugin.Log.LogDebug("[이중 점프] Character.Jump 패치 준비 완료");
                return true;
            }
            
            public static bool Prefix(Character __instance)
            {
                // 로컬 플레이어가 아니면 패스
                if (Player.m_localPlayer == null || __instance != Player.m_localPlayer)
                {
                    return true;
                }
                
                var player = __instance as Player;
                if (player == null) return true;
                
                // 이중 점프 스킬이 없으면 기본 점프
                if (!HasSkill("defense_step6_double_jump"))
                {
                    return true;
                }
                
                // 땅에 있으면 기본 점프, 콤보 리셋
                if (__instance.IsOnGround())
                {
                    multiJumpCombo = 0;
                    lastGroundTime[player] = Time.time;
                    return true;
                }
                else
                {
                    multiJumpCombo++;
                }
                
                // 이중 점프 가능 횟수 확인 (방어 전문가는 1회 추가 점프)
                int maxExtraJumps = 1;
                if (multiJumpCombo > maxExtraJumps)
                {
                    return false; // 더 이상 점프 불가
                }
                
                // 이중 점프 실행
                ExecuteDoubleJump(__instance, multiJumpCombo);
                return false; // 기본 점프 로직 무시
            }
        }
        
        /// <summary>
        /// 이중 점프 실행 로직
        /// </summary>
        public static void ExecuteDoubleJump(Character character, float jumpCount)
        {
            var player = character as Player;
            if (player == null) return;
            
            // 상태 확인
            if (player.IsEncumbered() || player.InDodge() || player.IsKnockedBack() || player.IsStaggering())
            {
                return;
            }
            
            // 스태미나 확인 - Player의 점프 스태미나 사용량
            float staminaUsage = 10f; // 기본 점프 스태미나
            if (!player.HaveStamina(staminaUsage))
            {
                Hud.instance.StaminaBarEmptyFlash();
                return;
            }
            
            // 점프 스킬 레벨 계산
            float skillFactor = 0f;
            Skills skills = player.GetSkills();
            if (skills != null)
            {
                skillFactor = skills.GetSkillFactor(Skills.SkillType.Jump);
                player.RaiseSkill(Skills.SkillType.Jump);
            }
            
            // 점프 벡터 계산 - 간단한 수직 점프
            float jumpForce = 12f; // 기본 점프 힘
            float skillOffset = 1f + skillFactor * 0.4f;
            Vector3 jumpVector = Vector3.up * (jumpForce * skillOffset);
            
            // 점프 실행
            player.ForceJump(jumpVector);
            
            // 스태미나 소모
            player.UseStamina(staminaUsage);
            
            // VFX/SFX 효과 적용
            PlaySkillEffect(player, "defense_step6_double_jump", player.transform.position);
            // 텍스트 표시 제거됨
            
            Plugin.Log.LogDebug($"[이중 점프] {jumpCount}번째 점프 실행");
        }
        
        /// <summary>
        /// 착지 시 콤보 리셋 (안전장치)
        /// </summary>
        [HarmonyPatch(typeof(Character), "UpdateGroundContact")]
        public static class DoubleJump_UpdateGroundContact_Patch
        {
            public static void Postfix(Character __instance)
            {
                if (__instance == Player.m_localPlayer && __instance.IsOnGround())
                {
                    multiJumpCombo = 0;
                    if (__instance is Player player)
                    {
                        lastGroundTime[player] = Time.time;
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 직업 스킬들의 공통 유틸리티 함수들
    /// </summary>
    public static class JobSkillsUtility
    {
        // 스킬 쿨다운 관리
        private static Dictionary<string, float> skillCooldowns = new Dictionary<string, float>();

        /// <summary>
        /// 스킬이 쿨다운 중인지 확인
        /// </summary>
        public static bool IsOnCooldown(Player player, string skillName)
        {
            if (player == null) return true;
            
            string key = $"{player.GetPlayerID()}_{skillName}";
            
            if (skillCooldowns.ContainsKey(key))
            {
                float remainingTime = skillCooldowns[key] - Time.time;
                if (remainingTime > 0)
                {
                    return true;
                }
                else
                {
                    skillCooldowns.Remove(key);
                    return false;
                }
            }
            
            return false;
        }

        /// <summary>
        /// 스킬 쿨다운 설정
        /// </summary>
        public static void SetCooldown(Player player, string skillName, float cooldownSeconds)
        {
            if (player == null) return;
            
            string key = $"{player.GetPlayerID()}_{skillName}";
            skillCooldowns[key] = Time.time + cooldownSeconds;
        }

        /// <summary>
        /// 스킬 쿨다운 남은 시간 확인
        /// </summary>
        public static float GetRemainingCooldown(Player player, string skillName)
        {
            if (player == null) return 0f;
            
            string key = $"{player.GetPlayerID()}_{skillName}";
            
            if (skillCooldowns.ContainsKey(key))
            {
                float remainingTime = skillCooldowns[key] - Time.time;
                return Mathf.Max(0f, remainingTime);
            }
            
            return 0f;
        }

        /// <summary>
        /// 쿨다운 메시지 표시
        /// </summary>
        public static void ShowCooldownMessage(Player player, string skillName)
        {
            if (player == null) return;
            
            float remainingTime = GetRemainingCooldown(player, skillName);
            if (remainingTime > 0)
            {
                string message = $"{skillName} 스킬이 쿨다운 중입니다. 남은 시간: {remainingTime:F1}초";
                player.Message(MessageHud.MessageType.Center, message);
                Plugin.Log.LogInfo($"[스킬 쿨다운] {player.GetPlayerName()} - {message}");
            }
        }

        /// <summary>
        /// 스킬 요구사항 메시지 표시
        /// </summary>
        public static void ShowRequirementMessage(Player player, string message)
        {
            if (player == null) return;
            
            player.Message(MessageHud.MessageType.Center, message);
            Plugin.Log.LogInfo($"[스킬 요구사항] {player.GetPlayerName()} - {message}");
        }

        /// <summary>
        /// 직업 스킬 보유 여부 확인
        /// </summary>
        public static bool HasJobSkill(string jobName)
        {
            return SkillTreeManager.Instance.GetSkillLevel(jobName) > 0;
        }

        /// <summary>
        /// 플레이어가 활을 사용 중인지 확인
        /// </summary>
        public static bool IsUsingBow(Player player)
        {
            if (player == null) return false;
            
            var weapon = player.GetCurrentWeapon();
            return weapon?.m_shared?.m_skillType == Skills.SkillType.Bows;
        }

        /// <summary>
        /// 플레이어가 근접 무기를 사용 중인지 확인
        /// </summary>
        public static bool IsUsingMeleeWeapon(Player player)
        {
            if (player == null) return false;
            
            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared == null) return false;
            
            var skillType = weapon.m_shared.m_skillType;
            return skillType == Skills.SkillType.Swords || 
                   skillType == Skills.SkillType.Axes || 
                   skillType == Skills.SkillType.Clubs || 
                   skillType == Skills.SkillType.Polearms || 
                   skillType == Skills.SkillType.Spears || 
                   skillType == Skills.SkillType.Knives;
        }

        /// <summary>
        /// 플레이어가 지팡이를 사용 중인지 확인 (개선된 감지 시스템)
        /// 3단계 우선순위 시스템으로 Valheim 기본 무기와 다른 모드 무기 모두 감지
        /// </summary>
        public static bool IsUsingStaff(Player player)
        {
            if (player == null) return false;

            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared == null) return false;

            try
            {
                // 1순위: Valheim 기본 스킬 타입 확인 (가장 확실한 방법)
                if (weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic ||
                    weapon.m_shared.m_skillType == Skills.SkillType.BloodMagic)
                {
                    return true;
                }

                // 2순위: 프리팹 이름으로 확인 (모드 무기 감지)
                string prefabName = "";
                if (weapon.m_dropPrefab?.name != null)
                {
                    prefabName = weapon.m_dropPrefab.name.ToLower();
                }

                if (prefabName.Contains("staff") || prefabName.Contains("wand") || prefabName.Contains("rod"))
                {
                    Plugin.Log.LogInfo($"[지팡이 감지] 프리팹 이름으로 지팡이 감지: {prefabName} ({weapon.m_shared.m_name})");
                    return true;
                }

                // 3순위: 무기 표시 이름으로 확인 (다국어 및 커스텀 무기 지원)
                string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
                if (weaponName.Contains("지팡이") || weaponName.Contains("staff") ||
                    weaponName.Contains("wand") || weaponName.Contains("rod"))
                {
                    Plugin.Log.LogInfo($"[지팡이 감지] 무기 이름으로 지팡이 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                    return true;
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 감지] 감지 중 오류 발생: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 몬스터 팩션인지 확인
        /// </summary>
        public static bool IsMonsterFaction(Character.Faction faction)
        {
            return faction != Character.Faction.Players && 
                   faction != Character.Faction.AnimalsVeg && 
                   faction != Character.Faction.Dverger;
        }

        /// <summary>
        /// 안전한 거리 계산
        /// </summary>
        public static float SafeDistance(Vector3 pos1, Vector3 pos2)
        {
            try
            {
                return Vector3.Distance(pos1, pos2);
            }
            catch
            {
                return float.MaxValue;
            }
        }

        /// <summary>
        /// 안전한 방향 계산
        /// </summary>
        public static Vector3 SafeDirection(Vector3 from, Vector3 to)
        {
            try
            {
                Vector3 direction = (to - from).normalized;
                return direction.magnitude > 0.1f ? direction : Vector3.forward;
            }
            catch
            {
                return Vector3.forward;
            }
        }
    }
}
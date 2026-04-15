using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 스킬트리 데이터 등록 오케스트레이터
    /// 각 전문가 트리는 별도 파일로 분리됨:
    /// - AttackSkillData.cs: 공격 전문가 트리
    /// - ProductionSkillData.cs: 생산 전문가 트리
    /// - SpeedSkillData.cs: 속도 전문가 트리
    /// - RangedSkillData.cs: 원거리 전문가 트리
    /// - MeleeSkillData.cs: 근접 전문가 트리
    /// - DefenseTreeData.cs: 방어 전문가 트리
    /// </summary>
    public static class SkillTreeData
    {
        public static void RegisterAll()
        {
            var manager = SkillTreeManager.Instance;

            // === 직업 스킬 등록 ===
            JobSkills.RegisterJobSkills();

            // === 원거리 스킬 등록 ===
            RangedSkillData.RegisterRangedSkills();
            RangedSkillData.SetupRangedSkillIcons();

            // === 근접 스킬 등록 ===
            MeleeSkillData.RegisterMeleeSkills();
            MeleeSkillData.SetupMeleeSkillIcons();

            // === 방어 전문가 스킬 등록 ===
            DefenseTreeData.RegisterDefenseSkills();

            // === 공격 전문가 스킬 등록 ===
            AttackSkillData.RegisterAttackSkills();

            // === 생산 전문가 스킬 등록 ===
            ProductionSkillData.RegisterProductionSkills();

            // === 속도 전문가 스킬 등록 ===
            SpeedSkillData.RegisterSpeedSkills();

            // === 후처리: 아이콘 및 위치 설정 ===
            SetupDefaultIcons(manager);
            SetupNodePositions(manager);
            RemoveInvalidNodes(manager);
            UpdateWeaponDescriptions(manager);
            UpdateMeleeWeaponTooltips();
        }

        /// <summary>
        /// 모든 노드의 기본 아이콘을 락 상태로 설정
        /// </summary>
        private static void SetupDefaultIcons(SkillTreeManager manager)
        {
            foreach (var node in manager.SkillNodes.Values)
            {
                if (!string.IsNullOrEmpty(node.IconNameLocked))
                    node.IconName = node.IconNameLocked;
                else if (!string.IsNullOrEmpty(node.IconNameUnlocked))
                    node.IconName = node.IconNameUnlocked;
            }

            // 특별한 아이콘이 없는 노드는 기본 아이콘 자동 지정
            foreach (var node in manager.SkillNodes.Values)
            {
                if (string.IsNullOrEmpty(node.IconNameLocked) || node.IconNameLocked == "all_skill_lock")
                {
                    node.IconNameLocked = "all_skill_lock";
                    node.IconNameUnlocked = "all_skill_unlock";
                }
            }
        }

        /// <summary>
        /// 근접/공격/원거리/생존/방어/속도 노드 위치 지정
        /// </summary>
        private static void SetupNodePositions(SkillTreeManager manager)
        {
            if (manager.SkillNodes.ContainsKey("attack_root"))
                manager.SkillNodes["attack_root"].Position = new Vector2(0, 95);
            if (manager.SkillNodes.ContainsKey("production_root"))
                manager.SkillNodes["production_root"].Position = new Vector2(0, -95);
            if (manager.SkillNodes.ContainsKey("speed_root"))
                manager.SkillNodes["speed_root"].Position = new Vector2(-90, -60);

            // 근접 무기 노드 좌표 설정
            if (manager.SkillNodes.ContainsKey("spear"))
                manager.SkillNodes["spear"].Position = new Vector2(185, 35);
            if (manager.SkillNodes.ContainsKey("spear"))
                manager.SkillNodes["spear"].Prerequisites = new List<string> { "melee_root" };
        }

        /// <summary>
        /// 중앙 0,0에 위치한 불필요한 노드 제거
        /// </summary>
        private static void RemoveInvalidNodes(SkillTreeManager manager)
        {
            var removeList = manager.SkillNodes.Values
                .Where(n => n.Position == Vector2.zero)
                .Select(n => n.Id)
                .ToList();
            foreach (var id in removeList)
                manager.SkillNodes.Remove(id);
        }

        /// <summary>
        /// 무기 타입별 조건 문구 자동 교체
        /// </summary>
        private static void UpdateWeaponDescriptions(SkillTreeManager manager)
        {
            foreach (var node in manager.SkillNodes.Values)
            {
                // 각 노드마다 weapon을 독립적으로 판단 (기존 버그: 한번 설정 후 리셋 안 됨)
                string weapon = null;
                if (node.Id.StartsWith("knife_expert")) weapon = "단검";
                else if (node.Id.StartsWith("sword_expert")) weapon = "검";
                else if (node.Id.StartsWith("spear_expert")) weapon = "창";
                else if (node.Id.StartsWith("polearm_expert")) weapon = "폴암";
                else if (node.Id.StartsWith("mace_Step1")) weapon = "둔기";
                else if (node.Id.StartsWith("spear")) weapon = "창";
                else if (node.Id.StartsWith("bow")) weapon = "활";
                else if (node.Id.StartsWith("crossbow")) weapon = "석궁";

                if (weapon == null) continue;

                try
                {
                    if (!string.IsNullOrEmpty(node.Description))
                    {
                        // 기존 조건 라인 패턴을 찾아서 교체
                        node.Description = Regex.Replace(node.Description,
                            @"\\n<color=#00BFFF><size=14>※ [^<\n]+착용 ?시 효과발동<\/size><\/color>", "");
                        node.Description = Regex.Replace(node.Description,
                            @"\\n<color=#00BFFF><size=14>※ [^<\n]+착용 ?시 효과 발동<\/size><\/color>", "");
                        node.Description = Regex.Replace(node.Description,
                            @"\\n?※ [^<\n]+착용 ?시 효과발동", "");
                        node.Description = Regex.Replace(node.Description,
                            @"\\n?※ [^<\n]+착용 ?시 효과 발동", "");
                        node.Description = node.Description.Trim();
                    }
                }
                catch (FormatException ex)
                {
                    Plugin.Log.LogWarning($"[SkillTreeData] 스킬 설명 업데이트 실패 ({node.Id}): {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 근접 무기 전문가 스킬 툴팁 강제 업데이트
        /// </summary>
        private static void UpdateMeleeWeaponTooltips()
        {
            try
            {
                Knife_Tooltip.UpdateKnifeTooltips();
                Sword_Tooltip.UpdateSwordTooltips();
                Spear_Tooltip.UpdateSpearTooltips();
                Polearm_Tooltip.UpdatePolearmTooltips();
                Mace_Tooltip.UpdateMaceTooltips();
                Plugin.Log.LogDebug("[SkillTreeData] 근접 무기 툴팁 강제 업데이트 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeData] 근접 무기 툴팁 업데이트 실패: {ex.Message}");
            }
        }
    }
}

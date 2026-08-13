using System.Collections.Generic;
using System.Linq;
using CaptainSkillTree.Localization;
using CaptainSkillTree.MMO_System;

namespace CaptainSkillTree.SkillTree.JobRestriction
{
    /// <summary>2차 트리 허용 방식</summary>
    public enum JobSecondaryMode
    {
        ChooseOne,   // 후보 중 1개만 선택 가능(리셋 전까지 잠김)
        AllRemaining // 기본 트리로 선택되지 않은 후보 전부 허용 (제작전문가 전용)
    }

    public class JobTreeRule
    {
        /// <summary>기본 트리 후보(패시브+액티브 전체 허용). 2개 이상이면 그 중 1개만 선택(투자 시작 시 확정, 리셋 전까지 잠김)</summary>
        public string[] BaseChoices;
        /// <summary>2차 트리 후보(패시브만 허용)</summary>
        public string[] SecondaryChoices;
        public JobSecondaryMode SecondaryMode;
        /// <summary>true면 2차 트리 접근 전 기본 트리를 "모두 배워야"(전체 만렙) 함 - 아처/메이지 전용</summary>
        public bool RequireBaseMastery;
        public bool AllowShield;
    }

    /// <summary>
    /// 직업별 스킬트리 학습 제한 규칙 테이블 및 게이팅 로직.
    /// 무기 트리는 SkillNode.Id의 접두사(bow_/crossbow_/staff_/sword_/knife_/spear_/polearm_/mace_)로 구분한다
    /// (SkillTreeManager.GetWeaponGroupPrefix()와 동일한 관례).
    /// </summary>
    public static class JobTreeRules
    {
        private static readonly string[] AllWeaponPrefixes =
            { "bow_", "crossbow_", "staff_", "sword_", "knife_", "spear_", "polearm_", "mace_" };

        // 액티브 스킬 ID (SkillTreeManager.CanUnlockActiveSkill의 rKeySkills/gKeyMeleeSkills/hKeySkills와 동일 목록)
        private static readonly HashSet<string> ActiveSkillIds = new HashSet<string>
        {
            "crossbow_Step6_expert", "bow_Step6_critboost", "staff_Step6_dual_cast",
            "sword_step5_finalcut", "sword_slash", "knife_step9_assassin_heart",
            "spear_Step5_penetrate", "polearm_step5_king", "defense_Step6_mind", "mace_Step7_guardian_heart",
            "sword_step5_defswitch", "knife_step10_stack_explosion", "spear_Step5_combo",
            "mace_Step7_fury_hammer", "staff_Step6_heal", "bow_Step6_arrow_rain", "crossbow_ice_breath",
            "mace_Step7_shockwave_slam"
        };

        public static bool IsActiveSkillId(string skillId) => ActiveSkillIds.Contains(skillId);

        public static readonly Dictionary<string, JobTreeRule> Rules = new Dictionary<string, JobTreeRule>
        {
            ["Archer"] = new JobTreeRule
            {
                BaseChoices = new[] { "bow_" },
                SecondaryChoices = new[] { "crossbow_", "knife_" },
                SecondaryMode = JobSecondaryMode.ChooseOne,
                RequireBaseMastery = true,
                AllowShield = false
            },
            ["Mage"] = new JobTreeRule
            {
                BaseChoices = new[] { "staff_" },
                SecondaryChoices = new[] { "crossbow_" },
                SecondaryMode = JobSecondaryMode.ChooseOne,
                RequireBaseMastery = true,
                AllowShield = false
            },
            ["Paladin"] = new JobTreeRule
            {
                BaseChoices = new[] { "mace_", "sword_" },
                SecondaryChoices = new[] { "crossbow_", "bow_" },
                SecondaryMode = JobSecondaryMode.ChooseOne,
                RequireBaseMastery = false,
                AllowShield = true
            },
            ["Berserker"] = new JobTreeRule
            {
                BaseChoices = new[] { "sword_", "mace_", "spear_", "polearm_" },
                SecondaryChoices = new[] { "crossbow_" },
                SecondaryMode = JobSecondaryMode.ChooseOne,
                RequireBaseMastery = false,
                AllowShield = true
            },
            ["Producer"] = new JobTreeRule
            {
                BaseChoices = new[] { "sword_", "mace_", "spear_", "polearm_", "bow_", "crossbow_" },
                SecondaryChoices = new[] { "sword_", "mace_", "spear_", "polearm_", "bow_", "crossbow_" },
                SecondaryMode = JobSecondaryMode.AllRemaining,
                RequireBaseMastery = false,
                AllowShield = true
            },
            ["Tanker"] = new JobTreeRule
            {
                BaseChoices = new[] { "sword_", "mace_", "spear_" },
                SecondaryChoices = new[] { "crossbow_" },
                SecondaryMode = JobSecondaryMode.ChooseOne,
                RequireBaseMastery = false,
                AllowShield = true
            },
            ["Rogue"] = new JobTreeRule
            {
                BaseChoices = new[] { "knife_" },
                SecondaryChoices = new[] { "bow_", "crossbow_" },
                SecondaryMode = JobSecondaryMode.ChooseOne,
                RequireBaseMastery = false,
                AllowShield = true
            },
        };

        /// <summary>현재 확정된 직업 ID(레벨 1 이상인 Y키 스킬). 없으면 null.</summary>
        public static string GetCurrentJob(Player player)
        {
            if (player == null) return null;
            foreach (var jobId in Rules.Keys)
            {
                if (SkillTreeManager.Instance.GetSkillLevel(jobId) > 0) return jobId;
            }
            return null;
        }

        public static bool TryGetRule(string jobId, out JobTreeRule rule) => Rules.TryGetValue(jobId, out rule);

        private static string GetPrefixOf(string skillId)
        {
            if (string.IsNullOrEmpty(skillId)) return null;
            foreach (var p in AllWeaponPrefixes)
                if (skillId.StartsWith(p, System.StringComparison.OrdinalIgnoreCase)) return p;
            return null;
        }

        /// <summary>해당 스킬이 무기 전문가 트리(bow_/crossbow_/staff_/sword_/knife_/spear_/polearm_/mace_) 소속인지 여부.
        /// 공격/방어/속도/생산 등 공통 트리는 false를 반환한다(직업과 무관하게 항상 허용되는 트리).</summary>
        public static bool IsWeaponTreeSkill(string skillId) => GetPrefixOf(skillId) != null;

        /// <summary>후보 접두사 중 투자 포인트(레벨 &gt; 0)가 있는 것을 찾는다. 없으면 null.</summary>
        private static string GetChosenPrefix(string[] candidates)
        {
            var mgr = SkillTreeManager.Instance;
            foreach (var prefix in candidates)
            {
                foreach (var node in mgr.SkillNodes.Values)
                {
                    if (node.Id.StartsWith(prefix, System.StringComparison.OrdinalIgnoreCase) && mgr.GetSkillLevel(node.Id) > 0)
                        return prefix;
                }
            }
            return null;
        }

        /// <summary>해당 접두사 트리의 모든 등록 노드가 만렙인지 확인 (아처/메이지 2차 트리 게이트용)</summary>
        private static bool IsPrefixTreeFullyLearned(string prefix)
        {
            var mgr = SkillTreeManager.Instance;
            bool foundAny = false;
            foreach (var node in mgr.SkillNodes.Values)
            {
                if (!node.Id.StartsWith(prefix, System.StringComparison.OrdinalIgnoreCase)) continue;
                foundAny = true;
                if (mgr.GetSkillLevel(node.Id) < node.MaxLevel) return false;
            }
            return foundAny;
        }

        /// <summary>
        /// 현재 직업 규칙상 해당 스킬(무기 트리 노드)에 투자 가능한지 판정한다.
        /// 직업 미선택 상태이거나 무기 트리 접두사가 없는 공통 계열(공격/방어/속도/생산 등)은 항상 허용한다.
        /// </summary>
        public static bool IsTreeAccessAllowed(Player player, string skillId, bool isActiveSkill, out string restrictionMessage)
        {
            restrictionMessage = null;
            if (player == null) return true;
            if (CaptainLevelConfig.IsAdminModeActive()) return true;

            var mgr = SkillTreeManager.Instance;
            if (mgr.IsJobSkill(skillId)) return true; // 직업 노드 자체는 CanUnlockActiveSkill의 Y키 로직에서 처리

            string jobId = GetCurrentJob(player);

            // 방패돌진(mace_Step7_guardian_heart): 방어 트리에 위치하지만 트리 위치와 무관하게 탱커 전용
            if (skillId == "mace_Step7_guardian_heart" && jobId != "Tanker")
            {
                restrictionMessage = L.Get("shield_charge_tanker_only");
                return false;
            }

            if (jobId == null || !Rules.TryGetValue(jobId, out var rule)) return true; // 직업 미선택: 자유

            string prefix = GetPrefixOf(skillId);
            if (prefix == null) return true; // 무기 트리 접두사가 없는 공통 계열은 제한 대상 아님

            bool isBaseCandidate = rule.BaseChoices.Contains(prefix);
            bool isSecondaryCandidate = rule.SecondaryChoices != null && rule.SecondaryChoices.Contains(prefix);

            if (!isBaseCandidate && !isSecondaryCandidate)
            {
                restrictionMessage = L.Get("job_tree_forbidden");
                return false;
            }

            string chosenBase = rule.BaseChoices.Length == 1 ? rule.BaseChoices[0] : GetChosenPrefix(rule.BaseChoices);

            // 기본 트리로 이미 확정되었거나(또는 아직 아무 기본 트리도 선택 전이라 첫 시도가 허용되는 경우) 전체 허용
            if (isBaseCandidate && (chosenBase == null || chosenBase == prefix))
                return true;

            // 여기부터는 "기본 트리로서는" 허용되지 않는 상태 (다른 기본 트리가 이미 확정됨)
            if (isActiveSkill)
            {
                restrictionMessage = isBaseCandidate
                    ? L.Get("job_tree_locked_base_choice")
                    : L.Get("job_tree_secondary_active_denied");
                return false;
            }

            if (!isSecondaryCandidate)
            {
                restrictionMessage = L.Get("job_tree_locked_base_choice");
                return false;
            }

            if (rule.RequireBaseMastery)
            {
                string baseFixed = rule.BaseChoices.Length == 1 ? rule.BaseChoices[0] : chosenBase;
                if (baseFixed == null || !IsPrefixTreeFullyLearned(baseFixed))
                {
                    restrictionMessage = L.Get("job_tree_base_not_mastered");
                    return false;
                }
            }

            if (rule.SecondaryMode == JobSecondaryMode.ChooseOne && rule.SecondaryChoices.Length > 1)
            {
                string chosenSecondary = GetChosenPrefix(rule.SecondaryChoices);
                if (chosenSecondary != null && chosenSecondary != prefix)
                {
                    restrictionMessage = L.Get("job_tree_locked_secondary_choice");
                    return false;
                }
            }

            return true;
        }
    }
}

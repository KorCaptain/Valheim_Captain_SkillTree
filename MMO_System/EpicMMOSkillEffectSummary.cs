using System;
using System.Collections.Generic;
using System.Linq;
using CaptainSkillTree.Localization;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMO 패널용 스킬트리 효과 텍스트 생성기
    /// 투자된 스킬들을 카테고리별로 요약하여 표시
    /// </summary>
    public static class EpicMMOSkillEffectSummary
    {
        // 카테고리 → 로컬라이제이션 키 매핑
        private static readonly Dictionary<string, string> CategoryKeyMap = new Dictionary<string, string>
        {
            ["원거리"]  = "mmo_cat_ranged",
            ["근접"]    = "mmo_cat_melee",
            ["공격"]    = "mmo_cat_attack",
            ["방어"]    = "mmo_cat_defense",
            ["속도"]    = "mmo_cat_speed",
            ["생산"]    = "mmo_cat_production",
            ["직업"]    = "mmo_cat_job",
            ["지팡이"]  = "mmo_cat_staff",
        };

        // 카테고리 표시 순서
        private static readonly string[] CategoryOrder =
        {
            "직업", "원거리", "지팡이", "근접", "공격", "방어", "속도", "생산"
        };

        // 액티브 스킬 R키 목록
        private static readonly string[] RKeySkills =
        {
            "crossbow_Step6_expert",
            "bow_Step6_critboost",
            "staff_Step6_dual_cast"
        };

        // 액티브 스킬 G키 목록
        private static readonly string[] GKeySkills =
        {
            "sword_step5_finalcut",
            "knife_step9_assassin_heart",
            "spear_Step5_penetrate",
            "polearm_step5_king",
            "mace_Step7_shockwave_slam"
        };

        // 액티브 스킬 H키 목록
        private static readonly string[] HKeySkills =
        {
            "sword_step5_defswitch",
            "spear_Step5_combo",
            "mace_Step7_fury_hammer",
            "staff_Step6_heal",
            "bow_Step6_arrow_rain"
        };

        // 직업 스킬 Y키 목록
        private static readonly string[] YKeySkills =
        {
            "Berserker", "Tanker", "Archer", "Rogue", "Mage", "Paladin", "Producer"
        };

        // ─────────────────────────────────────────────────────────────────
        // 1. "발동중인 효과" 패널 주입용 - 간략 요약
        // ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// "발동중인 효과" 패널에 주입할 텍스트 줄 목록 반환
        /// 투자된 스킬을 카테고리별로 묶어 한 줄씩 출력
        /// </summary>
        public static List<string> GetActiveEffectLines()
        {
            var lines = new List<string>();
            var manager = SkillTreeManager.Instance;
            if (manager == null) return lines;

            // 카테고리별로 투자된 스킬 수집
            var grouped = GetInvestedByCategory(manager);
            if (grouped.Count == 0) return lines;

            foreach (var cat in CategoryOrder)
            {
                if (!grouped.TryGetValue(cat, out var nodes) || nodes.Count == 0)
                    continue;

                string catLabel = GetCategoryLabel(cat);
                string skillNames = string.Join(", ", nodes.Select(n => n.Name));
                lines.Add($"{catLabel} : {skillNames}");
            }

            return lines;
        }

        // ─────────────────────────────────────────────────────────────────
        // 2. 직업 탭용 효과
        // ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// 투자된 직업 스킬 목록 반환 (직업명 + 레벨 + 설명 첫 줄)
        /// </summary>
        public static List<string> GetJobEffectLines()
        {
            var lines = new List<string>();
            var manager = SkillTreeManager.Instance;
            if (manager == null) return lines;

            foreach (var jobId in YKeySkills)
            {
                int level = manager.GetSkillLevel(jobId);
                if (level <= 0) continue;

                if (!manager.SkillNodes.TryGetValue(jobId, out var node)) continue;

                string lvLabel = L.Get("mmo_job_level");
                string desc = TruncateDescription(node.Description);
                lines.Add($"{node.Name} {lvLabel}{level}");
                if (!string.IsNullOrEmpty(desc))
                    lines.Add($"  {desc}");
            }

            return lines;
        }

        // ─────────────────────────────────────────────────────────────────
        // 3. 액티브 스킬 탭용 효과
        // ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// 투자된 액티브 스킬 목록 (키 라벨 포함)
        /// </summary>
        public static List<string> GetActiveSkillLines()
        {
            var lines = new List<string>();
            var manager = SkillTreeManager.Instance;
            if (manager == null) return lines;

            // R키 (원거리 액티브)
            foreach (var id in RKeySkills)
            {
                if (manager.GetSkillLevel(id) <= 0) continue;
                if (!manager.SkillNodes.TryGetValue(id, out var node)) continue;
                lines.Add($"{L.Get("mmo_key_r")} {node.Name}");
            }

            // G키 (근접 메인 액티브)
            foreach (var id in GKeySkills)
            {
                if (manager.GetSkillLevel(id) <= 0) continue;
                if (!manager.SkillNodes.TryGetValue(id, out var node)) continue;
                lines.Add($"{L.Get("mmo_key_g")} {node.Name}");
            }

            // H키 (보조 액티브)
            foreach (var id in HKeySkills)
            {
                if (manager.GetSkillLevel(id) <= 0) continue;
                if (!manager.SkillNodes.TryGetValue(id, out var node)) continue;
                lines.Add($"{L.Get("mmo_key_h")} {node.Name}");
            }

            // Y키 (직업)
            foreach (var id in YKeySkills)
            {
                if (manager.GetSkillLevel(id) <= 0) continue;
                if (!manager.SkillNodes.TryGetValue(id, out var node)) continue;
                int lv = manager.GetSkillLevel(id);
                lines.Add($"{L.Get("mmo_key_y")} {node.Name} {L.Get("mmo_job_level")}{lv}");
            }

            return lines;
        }

        // ─────────────────────────────────────────────────────────────────
        // 4. 패시브 탭용 카테고리별 효과
        // ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// 카테고리별 투자된 패시브 스킬 정보 반환
        /// Key: 카테고리 표시명, Value: 스킬명 목록
        /// </summary>
        public static Dictionary<string, List<string>> GetPassiveEffectsByCategory()
        {
            var result = new Dictionary<string, List<string>>();
            var manager = SkillTreeManager.Instance;
            if (manager == null) return result;

            // 액티브 스킬 ID 집합 (패시브 필터링용)
            var activeIds = new HashSet<string>(
                RKeySkills.Concat(GKeySkills).Concat(HKeySkills).Concat(YKeySkills));

            var grouped = GetInvestedByCategory(manager);

            foreach (var cat in CategoryOrder)
            {
                if (!grouped.TryGetValue(cat, out var nodes)) continue;

                // 직업 탭에서 이미 표시하므로 패시브 탭에서는 제외
                if (cat == "직업") continue;

                var passiveNodes = nodes
                    .Where(n => !activeIds.Contains(n.Id))
                    .ToList();

                if (passiveNodes.Count == 0) continue;

                string catLabel = GetCategoryLabel(cat);
                result[catLabel] = passiveNodes.Select(n => n.Name).ToList();
            }

            return result;
        }

        // ─────────────────────────────────────────────────────────────────
        // 내부 헬퍼
        // ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// 투자된 스킬을 Category 기준으로 그룹화
        /// </summary>
        private static Dictionary<string, List<SkillNode>> GetInvestedByCategory(SkillTreeManager manager)
        {
            var result = new Dictionary<string, List<SkillNode>>();

            foreach (var kv in manager.SkillNodes)
            {
                if (manager.GetSkillLevel(kv.Key) <= 0) continue;

                var node = kv.Value;
                string cat = string.IsNullOrEmpty(node.Category) ? "기타" : node.Category;

                if (!result.ContainsKey(cat))
                    result[cat] = new List<SkillNode>();

                result[cat].Add(node);
            }

            return result;
        }

        /// <summary>
        /// 카테고리 내부 키 → 표시 라벨 변환
        /// </summary>
        private static string GetCategoryLabel(string category)
        {
            if (CategoryKeyMap.TryGetValue(category, out var key))
                return L.Get(key);
            return category;
        }

        /// <summary>
        /// 설명 텍스트를 첫 줄 / 최대 50자로 잘라 반환
        /// </summary>
        private static string TruncateDescription(string desc)
        {
            if (string.IsNullOrEmpty(desc)) return string.Empty;

            // 첫 줄만
            int nl = desc.IndexOf('\n');
            string first = nl > 0 ? desc.Substring(0, nl) : desc;

            // 최대 50자
            if (first.Length > 50)
                first = first.Substring(0, 47) + "...";

            return first.Trim();
        }
    }
}

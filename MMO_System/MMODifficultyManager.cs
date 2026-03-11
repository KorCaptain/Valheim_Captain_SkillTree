using System;
using UnityEngine;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// MMO 몬스터 난이도 계산 로직
    /// SP 기반 m_level 보너스 + 별 확률 + 액티브/직업 스킬 확정 보너스
    /// </summary>
    public static class MMODifficultyManager
    {
        // ZDO 키: 몬스터당 1회만 적용되도록 중복 방지
        public const string ZDO_KEY_APPLIED = "CaptainMMO_DiffApplied";
        private static readonly int ZdoKeyHash = ZDO_KEY_APPLIED.GetStableHashCode();

        // ── R키: 원거리 액티브 스킬 ──
        private static readonly string[] RKeySkills =
        {
            "crossbow_Step6_expert",
            "bow_Step6_critboost",
            "staff_Step6_dual_cast"
        };

        // ── G키: 근접 메인 액티브 스킬 ──
        private static readonly string[] GKeySkills =
        {
            "sword_step5_finalcut",
            "knife_step9_assassin_heart",
            "spear_Step5_penetrate",
            "polearm_step5_king",
            "mace_Step7_guardian_heart"
        };

        // ── H키: 보조 액티브 스킬 ──
        private static readonly string[] HKeySkills =
        {
            "sword_step5_defswitch",
            "spear_Step5_combo",
            "mace_Step7_fury_hammer",
            "staff_Step6_heal"
        };

        // ── Y키: 직업 액티브 스킬 ──
        private static readonly string[] YKeySkills =
        {
            "Berserker", "Tanker", "Archer", "Rogue", "Mage", "Paladin"
        };

        /// <summary>현재 플레이어의 총 사용 SP 반환</summary>
        public static int GetTotalSP()
        {
            return SkillTreeManager.Instance?.GetTotalUsedPoints() ?? 0;
        }

        /// <summary>
        /// SP 구간별 m_level 보너스 값 반환 (MaxLevelBonus 상한 적용)
        /// </summary>
        public static int GetSpLevelBonus(int totalSP)
        {
            int bonus;
            if      (totalSP <= 10)  bonus = MMODifficultyConfig.SpBonus_0_10.Value;
            else if (totalSP <= 20)  bonus = MMODifficultyConfig.SpBonus_11_20.Value;
            else if (totalSP <= 30)  bonus = MMODifficultyConfig.SpBonus_21_30.Value;
            else if (totalSP <= 40)  bonus = MMODifficultyConfig.SpBonus_31_40.Value;
            else if (totalSP <= 70)  bonus = MMODifficultyConfig.SpBonus_41_70.Value;
            else if (totalSP <= 80)  bonus = MMODifficultyConfig.SpBonus_71_80.Value;
            else if (totalSP <= 100) bonus = MMODifficultyConfig.SpBonus_81_100.Value;
            else                     bonus = MMODifficultyConfig.SpBonus_101Plus.Value;

            return Math.Min(bonus, MMODifficultyConfig.MaxLevelBonus.Value);
        }

        /// <summary>
        /// 별 추가 확률 롤: chance = BaseStarChance + totalSP / StarChanceDivider (최대 MaxStarChance)
        /// </summary>
        public static bool RollStarChance(int totalSP)
        {
            float divider = MMODifficultyConfig.StarChanceDivider.Value;
            float spContribution = divider > 0f ? totalSP / divider : 0f;
            float chance = MMODifficultyConfig.BaseStarChance.Value + spContribution;
            chance = Math.Min(chance, MMODifficultyConfig.MaxStarChance.Value);
            return UnityEngine.Random.value * 100f < chance;
        }

        /// <summary>G/H/R키 액티브 스킬 중 1개 이상 보유 여부</summary>
        public static bool HasActiveSkill()
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return false;

            foreach (var id in RKeySkills)
                if (manager.GetSkillLevel(id) > 0) return true;
            foreach (var id in GKeySkills)
                if (manager.GetSkillLevel(id) > 0) return true;
            foreach (var id in HKeySkills)
                if (manager.GetSkillLevel(id) > 0) return true;
            return false;
        }

        /// <summary>Y키 직업 스킬 보유 여부</summary>
        public static bool HasJobSkill()
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null) return false;

            foreach (var id in YKeySkills)
                if (manager.GetSkillLevel(id) > 0) return true;
            return false;
        }

        /// <summary>
        /// 최종 m_level 보너스 계산
        ///  - SP 기반 보너스: RollStarChance 성공 시 GetSpLevelBonus 적용
        ///  - 액티브 스킬 보유: +1 확정 (EnableActiveSkillBonus가 true인 경우)
        ///  - 직업 스킬 보유: +1 확정 (EnableActiveSkillBonus가 true인 경우)
        /// </summary>
        public static int CalculateFinalLevelBonus(int totalSP)
        {
            int spBonus = RollStarChance(totalSP) ? GetSpLevelBonus(totalSP) : 0;

            if (!MMODifficultyConfig.EnableActiveSkillBonus.Value)
                return spBonus;

            int activeBonus = HasActiveSkill() ? 1 : 0;
            int jobBonus    = HasJobSkill()    ? 1 : 0;
            return spBonus + activeBonus + jobBonus;
        }

        /// <summary>이미 난이도가 적용된 몬스터인지 ZDO로 확인</summary>
        public static bool IsAlreadyApplied(Character monster)
        {
            try
            {
                var nview = monster.GetComponent<ZNetView>();
                if (nview == null || !nview.IsValid()) return false;
                return nview.GetZDO().GetBool(ZdoKeyHash);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[MMODifficultyManager] IsAlreadyApplied 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>ZDO에 적용 완료 플래그 저장</summary>
        public static void MarkAsApplied(Character monster)
        {
            try
            {
                var nview = monster.GetComponent<ZNetView>();
                if (nview == null || !nview.IsValid()) return;
                nview.GetZDO().Set(ZdoKeyHash, true);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[MMODifficultyManager] MarkAsApplied 실패: {ex.Message}");
            }
        }
    }
}

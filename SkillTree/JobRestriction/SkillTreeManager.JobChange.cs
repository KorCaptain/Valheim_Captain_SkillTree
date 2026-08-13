using CaptainSkillTree.SkillTree.JobRestriction;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 전직(직업 선택/변경) 플로우: 캐릭터당 평생 2회(최초 선택 + 재전직 1회)까지만 허용하며,
    /// 매 전직 시 스킬 포인트를 전체 강제 리셋한다. 최초 전직은 무료, 재전직 1회는 기존 코인 리셋 비용을 적용한다.
    /// 실제 새 직업 스킬 투자(아이템 소모 포함)는 이 함수 호출 후 기존 InvestPoint/ConfirmInvestments 경로에서 처리한다.
    /// </summary>
    public partial class SkillTreeManager
    {
        private const int MaxJobChanges = 2; // 최초 선택 + 재전직 1회
        private const string JobChangeCountKey = "CaptainSkillTree_JobChangeCount";

        public int GetJobChangeCount(Player player = null)
        {
            var target = player ?? Player.m_localPlayer;
            if (target == null) return 0;
            if (target.m_customData.TryGetValue(JobChangeCountKey, out var str) && int.TryParse(str, out int count))
                return System.Math.Max(0, count);
            return 0;
        }

        public int GetRemainingJobChanges(Player player = null)
        {
            return System.Math.Max(0, MaxJobChanges - GetJobChangeCount(player));
        }

        /// <summary>이번 전직 시도가 무료(전직 전용 무료 1회)인지 여부. 최초 직업 선택만 무료.</summary>
        public bool IsNextJobChangeFree(Player player = null)
        {
            return GetJobChangeCount(player) == 0;
        }

        /// <summary>
        /// 직업 노드의 RequiredPlayerLevel(기본 10) 충족 여부. 관리자 모드는 항상 true.
        /// 직업 아이콘 클릭은 기존 CanInvestWithMessage의 "0. 플레이어 레벨 조건 체크"를 거치지 않고
        /// TryHandleJobSelectionClick에서 곧바로 처리되므로, 레벨 조건은 여기서 별도로 검증해야 한다.
        /// </summary>
        public bool HasRequiredPlayerLevelForJob(string jobId, out int requiredLevel, out int currentLevel)
        {
            requiredLevel = 0;
            currentLevel = GetCurrentLevel();
            if (MMO_System.CaptainLevelConfig.IsAdminModeActive()) return true;
            if (SkillNodes.TryGetValue(jobId, out var node))
                requiredLevel = node.GetEffectiveRequiredPlayerLevel(GetSkillLevel(jobId) + 1);
            return requiredLevel <= 0 || currentLevel >= requiredLevel;
        }

        /// <summary>새 직업 Lv1 아이템 요구사항(에이크쉬르 트로피 등) 보유 여부. 관리자 모드는 항상 true.</summary>
        public bool HasJobLv1Items(string jobId)
        {
            if (MMO_System.CaptainLevelConfig.IsAdminModeActive()) return true;
            switch (jobId)
            {
                case "Archer": return HasArcherLevelItems(1);
                case "Rogue": return HasRogueLevelItems(1);
                case "Mage": return HasMageLevelItems(1);
                case "Berserker": return HasBerserkerLevelItems(1);
                case "Tanker": return HasTankerLevelItems(1);
                case "Paladin": return HasPaladinLevelItems(1);
                case "Producer": return HasProducerLevelItems(1);
                default: return false;
            }
        }

        public System.Collections.Generic.List<string> GetMissingJobLv1Items(string jobId)
        {
            switch (jobId)
            {
                case "Archer": return GetMissingArcherItems(1);
                case "Rogue": return GetMissingRogueItems(1);
                case "Mage": return GetMissingMageItems(1);
                case "Berserker": return GetMissingBerserkerItems(1);
                case "Tanker": return GetMissingTankerItems(1);
                case "Paladin": return GetMissingPaladinItems(1);
                case "Producer": return GetMissingProducerItems(1);
                default: return new System.Collections.Generic.List<string>();
            }
        }

        /// <summary>
        /// 무기 전문가 트리(8종: bow_/crossbow_/staff_/sword_/knife_/spear_/polearm_/mace_)만 초기화한다.
        /// 직업·생산·공격·방어·속도 스킬은 전혀 손대지 않는다.
        /// 직업 제한 시스템(v1.25.97) 도입 이전부터 여러 무기 트리를 자유롭게 섞어 투자해 둔 기존 유저를 위한
        /// 1회성 마이그레이션 전용 함수(JobTreeMigration.cs에서 호출).
        /// </summary>
        public void ResetWeaponTreeSkillLevels(Player player = null)
        {
            var target = player ?? Player.m_localPlayer;
            if (target == null) return;
            foreach (var node in SkillNodes.Values)
            {
                if (!JobTreeRules.IsWeaponTreeSkill(node.Id)) continue;
                target.m_customData[$"CaptainSkillTree_{node.Id}"] = "0";
            }
            pendingInvestments.Clear();
            SkillEffect.InvalidateAttackSpeedCache();
            SkillEffect.UpdateDefenseDodgeRate(target);
            if (target == Player.m_localPlayer)
                CaptainSkillTree.Gui.ActiveSkillHUD.Instance?.RefreshSlots();
        }

        /// <summary>
        /// 전직 가능 여부 판정. newJobId가 현재 직업과 동일하면(최초 확정 포함) 항상 true.
        /// 재전직 횟수 한도와 새 직업 Lv1 아이템 보유 여부를 함께 검사한다(리셋/비용 확정 전에 반드시 통과해야 함).
        /// </summary>
        public bool CanChangeJob(Player player, string newJobId, out string reasonKey)
        {
            reasonKey = null;
            var target = player ?? Player.m_localPlayer;
            if (target == null || target != Player.m_localPlayer)
            {
                // GetSkillLevel/ResetAllSkillLevels 등은 로컬 플레이어 전용이므로 그 외 대상은 지원하지 않는다
                reasonKey = "job_change_no_player";
                return false;
            }

            string currentJob = JobTreeRules.GetCurrentJob(target);
            if (currentJob == newJobId) return true;

            if (MMO_System.CaptainLevelConfig.IsAdminModeActive()) return true; // 관리자 모드: 재전직 횟수/레벨/아이템 제한 전부 면제

            if (GetJobChangeCount(target) >= MaxJobChanges)
            {
                reasonKey = "job_change_limit_reached";
                return false;
            }

            if (!HasRequiredPlayerLevelForJob(newJobId, out _, out _))
            {
                reasonKey = "player_level_required";
                return false;
            }

            if (!HasJobLv1Items(newJobId))
            {
                reasonKey = "job_change_missing_items";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 전직 확정: 전체 스킬 포인트 강제 리셋 + 무료/유료 판정(유료 시 코인 차감) + 카운터 증가.
        /// CanChangeJob()이 재전직 횟수·아이템 요구사항을 모두 통과한 뒤에만 상태를 변경하므로
        /// 리셋/카운터 소모 후 새 직업 투자가 실패하는 상황(진행도 손실)이 발생하지 않는다.
        /// 새 직업 스킬 자체의 투자(포인트 예약/확정)는 호출자가 기존 AddPendingInvestment/ConfirmInvestments로 이어서 처리해야 한다.
        /// </summary>
        public bool ConfirmJobChange(Player player, string newJobId, out string failReasonKey)
        {
            failReasonKey = null;
            var target = player ?? Player.m_localPlayer;
            if (target == null) { failReasonKey = "job_change_no_player"; return false; }

            if (!CanChangeJob(target, newJobId, out failReasonKey)) return false;

            bool isAdmin = MMO_System.CaptainLevelConfig.IsAdminModeActive();
            bool free = isAdmin || IsNextJobChangeFree(target);
            if (!free)
            {
                var inventory = target.GetInventory();
                int cost = SkillTreeConfig.JobResetCostValue;
                if (inventory == null || inventory.CountItems("$item_coins") < cost)
                {
                    failReasonKey = "job_change_not_enough_coins";
                    return false;
                }
                inventory.RemoveItem("$item_coins", cost);
            }

            string previousJob = JobTreeRules.GetCurrentJob(target);
            ResetAllSkillLevels();
            if (!isAdmin)
                target.m_customData[JobChangeCountKey] = (GetJobChangeCount(target) + 1).ToString();

            Plugin.Log.LogInfo($"[SkillTreeManager] 전직 확정: {(previousJob ?? "(없음)")} -> {newJobId} (무료={free}, 관리자={isAdmin})");
            return true;
        }
    }
}

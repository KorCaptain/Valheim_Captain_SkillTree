using UnityEngine;
using L10n = CaptainSkillTree.Localization.LocalizationManager;

namespace CaptainSkillTree.Gui
{
    /// <summary>
    /// 직업 아이콘(레벨 0) 클릭 시 "배울 수 있는 스킬트리 요약 + 확인/취소" 다이얼로그를 띄우고,
    /// 확인 시 SkillTreeManager.ConfirmJobChange()로 전직을 확정한다.
    /// 이미 보유 중인 직업(레벨 1 이상) 클릭은 기존 레벨업 플로우(ShowXUpgradeConfirmDialog)를 그대로 사용한다.
    /// </summary>
    public partial class SkillTreeUI
    {
        private static readonly string[] JobIconIds = { "Paladin", "Tanker", "Berserker", "Rogue", "Mage", "Archer", "Producer" };

        /// <summary>직업 선택/전직 클릭을 처리했으면 true(기존 투자 플로우를 건너뜀), 대상이 아니면 false.</summary>
        private bool TryHandleJobSelectionClick(CaptainSkillTree.SkillTree.SkillNode node)
        {
            if (System.Array.IndexOf(JobIconIds, node.Id) < 0) return false;

            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            if (manager.GetSkillLevel(node.Id) > 0) return false; // 이미 보유한 직업 -> 기존 레벨업 플로우

            var player = Player.m_localPlayer;
            string newJobId = node.Id;

            if (!manager.CanChangeJob(player, newJobId, out string blockReasonKey))
            {
                string warning;
                if (blockReasonKey == "player_level_required")
                {
                    manager.HasRequiredPlayerLevelForJob(newJobId, out int reqLv, out int curLv);
                    warning = L10n.Get("player_level_required", reqLv.ToString(), curLv.ToString());
                }
                else if (blockReasonKey == "job_change_missing_items")
                {
                    warning = L10n.Get(blockReasonKey);
                    var missing = manager.GetMissingJobLv1Items(newJobId);
                    if (missing.Count > 0) warning += "\n" + string.Join(", ", missing);
                }
                else
                {
                    warning = L10n.Get(blockReasonKey);
                }
                tooltipUI.ShowWarning(warning);
                return true;
            }

            bool free = manager.IsNextJobChangeFree(player);
            string jobKey = newJobId.ToLowerInvariant();
            string titleKey = free ? "job_select_confirm_title" : "job_change_confirm_title";
            string messageKey = free ? $"job_tree_summary_{jobKey}" : $"job_tree_summary_{jobKey}_rejob";

            ShowResetConfirmDialog(titleKey, messageKey, () =>
            {
                if (!manager.ConfirmJobChange(player, newJobId, out string failKey))
                {
                    tooltipUI.ShowWarning(L10n.Get(failKey));
                    return;
                }

                manager.AddPendingInvestment(newJobId);
                PlaySkillInvestmentEffects();
                manager.ConfirmInvestments();

                if (nodeUI != null)
                {
                    nodeUI.RefreshNodeStates();
                    nodeUI.UpdateConnectionLines();
                }
                RefreshUI();
            });
            return true;
        }
    }
}

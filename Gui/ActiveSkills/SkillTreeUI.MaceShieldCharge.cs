using UnityEngine;
using UnityEngine.UI;
using CaptainSkillTree.Localization;
using L10n = CaptainSkillTree.Localization.LocalizationManager;

namespace CaptainSkillTree.Gui
{
    // 방패돌진(mace_Step7_guardian_heart) UI — 업그레이드 다이얼로그, 핸들러, 투자 검증
    public partial class SkillTreeUI
    {
        // ─── CanInvestWithMessage 분기 ───
        private InvestResult CheckShieldChargeInvest(SkillTree.SkillNode node, int currentLevel)
        {
            int targetLevel = currentLevel + 1;
            if (targetLevel > 7)
                return new InvestResult(false, L10n.Get("shield_charge_max_level"));
            bool isAdmin = CaptainSkillTree.MMO_System.CaptainLevelConfig.IsAdminModeActive();
            if (!isAdmin && !SkillTree.SkillTreeManager.Instance.HasShieldChargeLevelItems(targetLevel))
            {
                var missing = SkillTree.SkillTreeManager.Instance.GetMissingShieldChargeItems(targetLevel);
                var msg = L10n.Get("shield_charge_level_item_required", targetLevel);
                if (missing.Count > 0)
                    msg += "\n" + L10n.Get("shield_charge_missing_items", string.Join(", ", missing));
                return new InvestResult(false, msg);
            }
            return new InvestResult(true);
        }

        // ─── 클릭 핸들러 ───
        private bool HandleShieldChargeClick(SkillTree.SkillNode node)
        {
            var manager = SkillTree.SkillTreeManager.Instance;
            int skillLevel = manager.GetSkillLevel("mace_Step7_guardian_heart");
            if (skillLevel < 0) return false;

            int targetLevel = skillLevel + 1;
            if (targetLevel > 7)
            {
                tooltipUI.ShowWarning(L10n.Get("shield_charge_max_level"));
                return true;
            }
            bool isAdmin = CaptainSkillTree.MMO_System.CaptainLevelConfig.IsAdminModeActive();
            if (!isAdmin && manager.GetAvailablePoints(false) < node.RequiredPoints)
            {
                tooltipUI.ShowWarning(L10n.Get("skill_insufficient_points_detail", node.RequiredPoints, manager.GetAvailablePoints(false)));
                return true;
            }
            if (isAdmin || manager.HasShieldChargeLevelItems(targetLevel))
            {
                RectTransform nodeRect = null;
                if (nodeUI != null && nodeUI.nodeObjects.ContainsKey(node.Id))
                {
                    var obj = nodeUI.nodeObjects[node.Id];
                    if (obj != null) nodeRect = obj.GetComponent<RectTransform>();
                }
                ShowShieldChargeUpgradeConfirmDialog(targetLevel, nodeRect);
            }
            else
            {
                var missing = manager.GetMissingShieldChargeItems(targetLevel);
                var msg = L10n.Get("shield_charge_level_item_required", targetLevel);
                if (missing.Count > 0)
                    msg += "\n" + L10n.Get("shield_charge_missing_items", string.Join(", ", missing));
                tooltipUI.ShowWarning(msg);
            }
            return true;
        }

        // ─── 업그레이드 확인 다이얼로그 ───
        private void ShowShieldChargeUpgradeConfirmDialog(int targetLevel, RectTransform nodeRect)
        {
            if (confirmDialog != null)
                DestroyImmediate(confirmDialog);

            confirmDialog = new GameObject("ShieldChargeUpgradeDialog");
            confirmDialog.transform.SetParent(panel.transform, false);

            var bgImage = confirmDialog.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.7f);
            var bgRect = confirmDialog.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            bgRect.anchoredPosition = Vector2.zero;

            var border = new GameObject("GoldBorder");
            border.transform.SetParent(confirmDialog.transform, false);
            var borderImage = border.AddComponent<Image>();
            borderImage.color = new Color(0.2f, 0.1f, 0.05f, 0.85f);
            var borderRect = border.GetComponent<RectTransform>();
            borderRect.sizeDelta = new Vector2(420, 260);
            borderRect.anchoredPosition = Vector2.zero;

            var darkBg = new GameObject("DarkBg");
            darkBg.transform.SetParent(border.transform, false);
            var darkBgImage = darkBg.AddComponent<Image>();
            darkBgImage.color = new Color(0.1f, 0.06f, 0.02f, 0.97f);
            var darkBgRect = darkBg.GetComponent<RectTransform>();
            darkBgRect.anchorMin = new Vector2(0.5f, 0.5f);
            darkBgRect.anchorMax = new Vector2(0.5f, 0.5f);
            darkBgRect.pivot = new Vector2(0.5f, 0.5f);
            darkBgRect.sizeDelta = new Vector2(414, 254);
            darkBgRect.anchoredPosition = Vector2.zero;

            var titleBar = new GameObject("TitleBar");
            titleBar.transform.SetParent(darkBg.transform, false);
            titleBar.AddComponent<Image>().color = new Color(0.3f, 0.15f, 0.05f, 1f);
            var titleBarRect = titleBar.GetComponent<RectTransform>();
            titleBarRect.anchorMin = titleBarRect.anchorMax = titleBarRect.pivot = new Vector2(0.5f, 0.5f);
            titleBarRect.sizeDelta = new Vector2(414, 44);
            titleBarRect.anchoredPosition = new Vector2(0, 108);

            var titleObj = new GameObject("Title");
            titleObj.transform.SetParent(darkBg.transform, false);
            var titleText = titleObj.AddComponent<UnityEngine.UI.Text>();
            titleText.text = L10n.Get("shield_charge_upgrade_title");
            titleText.fontSize = 20;
            titleText.fontStyle = FontStyle.Bold;
            titleText.color = new Color(1f, 0.8f, 0.2f, 1f);
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            titleObj.AddComponent<UnityEngine.UI.Shadow>().effectColor = new Color(0, 0, 0, 0.8f);
            var titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.anchorMin = titleRect.anchorMax = titleRect.pivot = new Vector2(0.5f, 0.5f);
            titleRect.sizeDelta = new Vector2(380, 34);
            titleRect.anchoredPosition = new Vector2(0, 98);

            var contentObj = new GameObject("Content");
            contentObj.transform.SetParent(darkBg.transform, false);
            var contentText = contentObj.AddComponent<UnityEngine.UI.Text>();
            contentText.text = L10n.Get("shield_charge_upgrade_confirm", targetLevel);
            contentText.fontSize = 16;
            contentText.color = Color.white;
            contentText.alignment = TextAnchor.MiddleCenter;
            contentText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            contentObj.AddComponent<UnityEngine.UI.Shadow>().effectColor = new Color(0, 0, 0, 0.8f);
            var contentRect = contentObj.GetComponent<RectTransform>();
            contentRect.anchorMin = contentRect.anchorMax = contentRect.pivot = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(390, 120);
            contentRect.anchoredPosition = new Vector2(0, 10);

            confirmButton = CreateLuxuryButton("ConfirmButton", L10n.Get("ui_confirm"),
                new Vector2(-68f, -102f), new Color(0.4f, 0.2f, 0.05f, 1f), darkBg.transform, () =>
                {
                    PlayConfirmSound();
                    HideResetConfirmDialog();
                    var mgr = SkillTree.SkillTreeManager.Instance;
                    mgr.AddPendingInvestment("mace_Step7_guardian_heart");
                    mgr.ConfirmInvestments();
                    nodeUI.RefreshNodeStates();
                    RefreshUI();
                });

            cancelButton = CreateLuxuryButton("CancelButton", L10n.Get("ui_cancel"),
                new Vector2(68f, -102f), new Color(0.22f, 0.22f, 0.30f, 1f), darkBg.transform, () =>
                {
                    PlayCancelSound();
                    HideResetConfirmDialog();
                });

            confirmDialog.transform.SetAsLastSibling();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.Gui
{
    /// <summary>스킬트리 UI 내부 좌측 상단 Quest 버튼. QuestPanelUI(독립 오버레이) 토글만 담당.</summary>
    public partial class SkillTreeUI
    {
        private Button? questButton;

        private void CreateQuestButton(GameObject parent)
        {
            try
            {
                if (!CaptainSkillTree.SkillTree.Quest_Config.IsEnabled) return;

                questButton = CreateFancyButton("QuestButton", L.Get("quest_button_label"),
                    new Vector2(-590, -40), new Color(0.55f, 0.35f, 0.10f, 1f), parent.transform,
                    () => QuestPanelUI.Toggle());
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeUI] Quest 버튼 생성 실패: {ex.Message}");
            }
        }
    }
}

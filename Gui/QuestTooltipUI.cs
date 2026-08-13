using UnityEngine;
using UnityEngine.UI;

namespace CaptainSkillTree.Gui
{
    /// <summary>
    /// 퀘스트 목록 행 마우스오버용 경량 텍스트 툴팁.
    /// SkillTreeTooltipUI(SkillNode 전용, 이미 800줄을 크게 초과한 파일)는 건드리지 않고,
    /// 퀘스트 쪽에서 필요한 "순수 텍스트 표시"만 담당하는 별도의 작은 컴포넌트로 분리한다.
    /// </summary>
    public class QuestTooltipUI : MonoBehaviour
    {
        private GameObject _tooltipObj;
        private static Canvas _tooltipCanvas;

        public void ShowTooltip(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            if (_tooltipObj != null) Destroy(_tooltipObj);

            if (_tooltipCanvas == null)
                CreateTooltipCanvas();

            _tooltipObj = new GameObject("QuestTooltip", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            _tooltipObj.transform.SetParent(_tooltipCanvas.transform, false);
            var rect = _tooltipObj.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0f, 1f);

            var borderObj = new GameObject("Border", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            borderObj.transform.SetParent(_tooltipObj.transform, false);
            var borderRect = borderObj.GetComponent<RectTransform>();
            borderRect.anchorMin = Vector2.zero;
            borderRect.anchorMax = Vector2.one;
            borderRect.offsetMin = new Vector2(-3, -3);
            borderRect.offsetMax = new Vector2(3, 3);
            var borderImg = borderObj.GetComponent<Image>();
            borderImg.color = new Color(0.85f, 0.7f, 0.1f, 0.9f); // 퀘스트 패널과 동일한 골드 테두리
            borderImg.raycastTarget = false;

            var bgObj = new GameObject("Background", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            bgObj.transform.SetParent(_tooltipObj.transform, false);
            var bgRect = bgObj.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var bgImg = bgObj.GetComponent<Image>();
            bgImg.color = new Color(0.02f, 0.02f, 0.05f, 0.96f);
            bgImg.raycastTarget = false;

            var rootImg = _tooltipObj.GetComponent<Image>();
            rootImg.color = new Color(0f, 0f, 0f, 0f);
            rootImg.raycastTarget = false;

            var txtObj = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            txtObj.transform.SetParent(_tooltipObj.transform, false);
            var txtRect = txtObj.GetComponent<RectTransform>();
            txtRect.anchorMin = Vector2.zero;
            txtRect.anchorMax = Vector2.one;
            txtRect.offsetMin = new Vector2(14, 14);
            txtRect.offsetMax = new Vector2(-14, -14);
            var txt = txtObj.GetComponent<Text>();
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = 15;
            txt.color = Color.white;
            txt.alignment = TextAnchor.UpperLeft;
            txt.horizontalOverflow = HorizontalWrapMode.Wrap;
            txt.verticalOverflow = VerticalWrapMode.Overflow;
            txt.lineSpacing = 1.15f;
            txt.supportRichText = true;
            txt.text = text;

            AutoResize(rect, txt);
            PositionNearMouse(rect);

            _tooltipObj.transform.SetAsLastSibling();
        }

        public void HideTooltip()
        {
            if (_tooltipObj != null)
            {
                Destroy(_tooltipObj);
                _tooltipObj = null;
            }
        }

        private static void CreateTooltipCanvas()
        {
            var allCanvases = FindObjectsOfType<Canvas>();
            int highestOrder = int.MinValue;
            foreach (var c in allCanvases)
                if (c.sortingOrder > highestOrder) highestOrder = c.sortingOrder;

            var canvasObj = new GameObject("QuestTooltipCanvas", typeof(Canvas), typeof(GraphicRaycaster));
            _tooltipCanvas = canvasObj.GetComponent<Canvas>();
            _tooltipCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _tooltipCanvas.sortingOrder = highestOrder + 100;

            var raycaster = canvasObj.GetComponent<GraphicRaycaster>();
            raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

            DontDestroyOnLoad(canvasObj);
        }

        /// <summary>SkillTreeTooltipUI.AutoResizeTooltip과 동일한 방식(텍스트 preferred 크기 기반 clamp)을 따른다.</summary>
        private static void AutoResize(RectTransform rect, Text txt)
        {
            Canvas.ForceUpdateCanvases();

            const float minWidth = 260f, maxWidth = 440f;
            const float minHeight = 60f, maxHeight = 320f;
            const float padding = 28f, verticalPadding = 28f;

            float width = Mathf.Clamp(txt.preferredWidth + padding, minWidth, maxWidth);
            float height = Mathf.Clamp(txt.preferredHeight + verticalPadding, minHeight, maxHeight);
            rect.sizeDelta = new Vector2(width, height);
        }

        private static void PositionNearMouse(RectTransform rect)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 size = rect.sizeDelta;

            Vector2 targetPos = mousePos + new Vector2(20, -20);
            if (targetPos.x + size.x > screenSize.x) targetPos.x = mousePos.x - size.x - 20;
            if (targetPos.y - size.y < 0) targetPos.y = mousePos.y + size.y + 20;

            rect.position = targetPos;
        }
    }
}

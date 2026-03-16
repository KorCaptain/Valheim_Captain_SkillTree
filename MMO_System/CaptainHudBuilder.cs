using UnityEngine;
using UnityEngine.UI;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// WackyEpicMMOSystem 스타일 가로 HUD 코드 빌드 전담 클래스
    /// HorizontalLayoutGroup 기반, 에이트르 유무에 따라 1050/1475px 동적 크기
    /// </summary>
    internal static class CaptainHudBuilder
    {
        // ──────────────────────────────────────────
        // 색상 상수
        // ──────────────────────────────────────────

        private static readonly Color COL_PANEL_BG = new Color(0.184f, 0.086f, 0.000f, 0.88f);
        private static readonly Color COL_BORDER   = new Color(0.55f,  0.35f,  0.05f,  0.75f);
        private static readonly Color COL_BAR_BG   = new Color(0.10f,  0.05f,  0.00f,  0.92f);

        // 레벨 색상
        internal static readonly Color COL_LVL_DEFAULT = new Color(1.00f, 0.85f, 0.30f, 1.00f);
        internal static readonly Color COL_LVL_SILVER  = new Color(0.95f, 0.80f, 0.55f, 1.00f);
        internal static readonly Color COL_LVL_GOLD    = new Color(1.00f, 0.60f, 0.05f, 1.00f);
        internal static readonly Color COL_LVL_PLAT    = new Color(0.90f, 0.75f, 1.00f, 1.00f);

        // 가로 패널 폭
        private const float WIDTH_NO_EITR  = 1050f;
        private const float WIDTH_WITH_EITR = 1475f;
        private const float PANEL_HEIGHT   = 38f;

        // ──────────────────────────────────────────
        // HUD 컴포넌트 구조체
        // ──────────────────────────────────────────

        internal struct HudComponents
        {
            public Canvas        Canvas;
            public RectTransform PanelRt;

            // 레벨/경험치
            public Text  LevelText;
            public Text  ExpPercentText;
            public Image ExpBarFill;
            public Image ExpBarGlow;

            // HP
            public Text        HpText;
            public Image       HpBarFill;
            public GameObject  HpPanel;

            // 스태미나
            public Text        StaminaText;
            public Image       StaminaBarFill;
            public GameObject  StaminaPanel;

            // 에이트르
            public Text        EitrText;
            public Image       EitrBarFill;
            public GameObject  EitrPanel;
        }

        // ──────────────────────────────────────────
        // 진입점
        // ──────────────────────────────────────────

        /// <summary>
        /// HUD 전체를 코드로 빌드하고 컴포넌트 참조를 반환
        /// </summary>
        internal static HudComponents Build()
        {
            var comp = new HudComponents();

            // Canvas
            var canvasGO = new GameObject("CaptainExpHUDCanvas");
            var canvas   = canvasGO.AddComponent<Canvas>();
            canvas.renderMode   = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1511;

            var scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.scaleFactor = CaptainLevelConfig.HudScale?.Value ?? 1.0f;

            canvasGO.AddComponent<GraphicRaycaster>();
            UnityEngine.Object.DontDestroyOnLoad(canvasGO);
            comp.Canvas = canvas;

            // ── 메인 패널 (좌하단 앵커, HorizontalLayoutGroup) ──
            var panel = new GameObject("CaptainHudPanel");
            panel.transform.SetParent(canvasGO.transform, false);

            var panelRt = panel.AddComponent<RectTransform>();
            panelRt.anchorMin        = new Vector2(0f, 0f);
            panelRt.anchorMax        = new Vector2(0f, 0f);
            panelRt.pivot            = new Vector2(0f, 0f);
            panelRt.anchoredPosition = new Vector2(10f, 10f);
            panelRt.sizeDelta        = new Vector2(WIDTH_NO_EITR, PANEL_HEIGHT);
            comp.PanelRt = panelRt;

            // 배경 + 테두리
            AddPanelBackground(panel);

            // HorizontalLayoutGroup
            var hLayout = panel.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing              = 12f;
            hLayout.childForceExpandWidth  = false;
            hLayout.childForceExpandHeight = true;
            hLayout.childAlignment        = TextAnchor.MiddleLeft;
            hLayout.padding               = new RectOffset(10, 10, 5, 5);

            // ── 서브패널: HP ──
            Color hpColor = ParseColor(CaptainLevelConfig.HudHpColor?.Value, new Color(0.529f, 0.000f, 0.000f));
            comp.HpPanel = BuildStatSubPanel(
                panel.transform, "HP", "♥", hpColor, 200f,
                out comp.HpText, out comp.HpBarFill);
            CaptainHudDrag.AttachTo(comp.HpPanel, "HP", canvas);

            // ── 서브패널: 스태미나 ──
            Color stamColor = ParseColor(CaptainLevelConfig.HudStaminaColor?.Value, new Color(0.596f, 0.380f, 0.000f));
            comp.StaminaPanel = BuildStatSubPanel(
                panel.transform, "Stamina", "⚡", stamColor, 200f,
                out comp.StaminaText, out comp.StaminaBarFill);
            CaptainHudDrag.AttachTo(comp.StaminaPanel, "Stamina", canvas);

            // ── 서브패널: 에이트르 (기본 숨김) ──
            Color eitrColor = ParseColor(CaptainLevelConfig.HudEitrColor?.Value, new Color(0.518f, 0.145f, 0.486f));
            comp.EitrPanel = BuildStatSubPanel(
                panel.transform, "Eitr", "✦", eitrColor, 200f,
                out comp.EitrText, out comp.EitrBarFill);
            comp.EitrPanel.SetActive(false);
            CaptainHudDrag.AttachTo(comp.EitrPanel, "Eitr", canvas);

            // ── 서브패널: 경험치 ──
            Color expColor = ParseColor(CaptainLevelConfig.HudExpColor?.Value, new Color(0.784f, 0.471f, 0.125f));
            BuildExpSubPanel(
                panel.transform, expColor,
                out comp.LevelText, out comp.ExpPercentText,
                out comp.ExpBarFill, out comp.ExpBarGlow);

            return comp;
        }

        // ──────────────────────────────────────────
        // 패널 크기 조정 (에이트르 유무)
        // ──────────────────────────────────────────

        internal static void ResizePanelForEitr(RectTransform panelRt, bool hasEitr)
        {
            if (panelRt == null) return;
            float targetWidth = hasEitr ? WIDTH_WITH_EITR : WIDTH_NO_EITR;
            panelRt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
        }

        // ──────────────────────────────────────────
        // 서브패널 빌더 (HP / 스태미나 / 에이트르)
        // ──────────────────────────────────────────

        private static GameObject BuildStatSubPanel(
            Transform parent, string name, string icon, Color barColor, float barWidth,
            out Text textOut, out Image fillOut)
        {
            var sub = new GameObject($"Sub_{name}");
            sub.transform.SetParent(parent, false);

            var subRt = sub.AddComponent<RectTransform>();
            var subElem = sub.AddComponent<LayoutElement>();
            subElem.preferredWidth  = barWidth + 100f; // 아이콘 + 텍스트 + 바
            subElem.minHeight       = PANEL_HEIGHT - 8f;
            subElem.preferredHeight = PANEL_HEIGHT - 8f;

            // 서브패널 배경 (약한 투명 배경)
            var subBg = sub.AddComponent<Image>();
            subBg.color = new Color(0.12f, 0.05f, 0.00f, 0.72f);
            subBg.raycastTarget = true;

            // 내부 HorizontalLayoutGroup
            var hLayout = sub.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing               = 4f;
            hLayout.childForceExpandWidth  = false;
            hLayout.childForceExpandHeight = false;
            hLayout.childAlignment        = TextAnchor.MiddleLeft;
            hLayout.padding               = new RectOffset(4, 4, 2, 2);

            // 아이콘
            CreateText(sub.transform, icon, 16, FontStyle.Bold, barColor, 20f, PANEL_HEIGHT - 8f);

            // 숫자 텍스트
            textOut = CreateText(sub.transform, "0/0", 12, FontStyle.Normal, Color.white, 72f, PANEL_HEIGHT - 8f);
            textOut.alignment = TextAnchor.MiddleRight;

            // 바 + 글로우 + 필
            BuildBar(sub.transform, barWidth, barColor, out fillOut);

            return sub;
        }

        // ──────────────────────────────────────────
        // 경험치 서브패널
        // ──────────────────────────────────────────

        private static void BuildExpSubPanel(
            Transform parent, Color expColor,
            out Text levelTextOut, out Text expPercentOut,
            out Image fillOut, out Image glowOut)
        {
            var sub = new GameObject("Sub_Exp");
            sub.transform.SetParent(parent, false);

            var subElem = sub.AddComponent<LayoutElement>();
            subElem.flexibleWidth  = 1f;
            subElem.minHeight      = PANEL_HEIGHT - 8f;
            subElem.preferredHeight = PANEL_HEIGHT - 8f;

            var subBg = sub.AddComponent<Image>();
            subBg.color = new Color(0.12f, 0.05f, 0.00f, 0.72f);
            subBg.raycastTarget = true;

            var vLayout = sub.AddComponent<VerticalLayoutGroup>();
            vLayout.spacing               = 2f;
            vLayout.childForceExpandWidth  = true;
            vLayout.childForceExpandHeight = false;
            vLayout.childAlignment        = TextAnchor.UpperLeft;
            vLayout.padding               = new RectOffset(4, 4, 2, 2);

            // 헤더 줄 (레벨 | %)
            var header = new GameObject("ExpHeader");
            header.transform.SetParent(sub.transform, false);

            var hLayout = header.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing               = 6f;
            hLayout.childForceExpandWidth  = false;
            hLayout.childForceExpandHeight = false;
            hLayout.childAlignment        = TextAnchor.MiddleLeft;

            var headerFitter = header.AddComponent<ContentSizeFitter>();
            headerFitter.verticalFit   = ContentSizeFitter.FitMode.PreferredSize;
            headerFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            // LV 텍스트
            levelTextOut = CreateText(header.transform, "LV.1", 14, FontStyle.Bold, COL_LVL_DEFAULT, 80f, 18f);

            // 구분선
            CreateText(header.transform, "│", 12, FontStyle.Normal, new Color(0.75f, 0.55f, 0.20f), 10f, 18f);

            // % 텍스트
            expPercentOut = CreateText(header.transform, "0.00 %", 12, FontStyle.Bold, new Color(0.95f, 0.80f, 0.50f), 70f, 18f);

            // 경험치 바
            var barWrap = new GameObject("ExpBarWrap");
            barWrap.transform.SetParent(sub.transform, false);

            var barElem = barWrap.AddComponent<LayoutElement>();
            barElem.minHeight       = 10f;
            barElem.preferredHeight = 10f;

            var barBg = barWrap.AddComponent<Image>();
            barBg.color          = COL_BAR_BG;
            barBg.raycastTarget  = false;

            // 글로우
            glowOut = AddFillImage(barWrap.transform, "ExpGlow",
                new Color(expColor.r, expColor.g, expColor.b, 0.60f), offsetY: 0f);
            glowOut.fillAmount = 0f;

            // 메인 Fill
            fillOut = AddFillImage(barWrap.transform, "ExpFill", expColor, offsetY: 2f);
            fillOut.fillAmount = 0f;

            CaptainHudDrag.AttachTo(sub, "Exp", null);
        }

        // ──────────────────────────────────────────
        // 공통 바 빌더
        // ──────────────────────────────────────────

        private static void BuildBar(Transform parent, float barWidth, Color fillColor, out Image fillOut)
        {
            var barWrap = new GameObject("BarWrap");
            barWrap.transform.SetParent(parent, false);

            var barElem = barWrap.AddComponent<LayoutElement>();
            barElem.preferredWidth  = barWidth;
            barElem.preferredHeight = 16f;
            barElem.minHeight       = 16f;

            var barBg = barWrap.AddComponent<Image>();
            barBg.color         = COL_BAR_BG;
            barBg.raycastTarget = false;

            // 글로우
            var glowColor = new Color(fillColor.r, fillColor.g, fillColor.b, 0.60f);
            AddFillImage(barWrap.transform, "Glow", glowColor, offsetY: 0f);

            // 메인 Fill
            fillOut = AddFillImage(barWrap.transform, "Fill", fillColor, offsetY: 2f);
            fillOut.fillAmount = 1f;
        }

        // ──────────────────────────────────────────
        // 패널 배경/테두리 유틸
        // ──────────────────────────────────────────

        private static void AddPanelBackground(GameObject panel)
        {
            var bg = panel.AddComponent<Image>();
            bg.color          = COL_PANEL_BG;
            bg.raycastTarget  = true;

            var border = new GameObject("Border");
            border.transform.SetParent(panel.transform, false);
            var borderRt = border.AddComponent<RectTransform>();
            borderRt.anchorMin = Vector2.zero;
            borderRt.anchorMax = Vector2.one;
            borderRt.offsetMin = new Vector2(-1f, -1f);
            borderRt.offsetMax = new Vector2(1f, 1f);
            var borderImg = border.AddComponent<Image>();
            borderImg.color         = COL_BORDER;
            borderImg.raycastTarget = false;
            border.transform.SetAsFirstSibling();
        }

        // ──────────────────────────────────────────
        // 공통 유틸
        // ──────────────────────────────────────────

        private static Image AddFillImage(Transform parent, string name, Color color, float offsetY)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = new Vector2(0f, offsetY);
            rt.offsetMax = new Vector2(0f, -offsetY);
            var img = go.AddComponent<Image>();
            img.color          = color;
            img.raycastTarget  = false;
            img.type           = Image.Type.Filled;
            img.fillMethod     = Image.FillMethod.Horizontal;
            img.fillOrigin     = (int)Image.OriginHorizontal.Left;
            img.fillAmount     = 1f;
            return img;
        }

        internal static Text CreateText(Transform parent, string content, int size, FontStyle style,
            Color color, float w, float h)
        {
            var go = new GameObject("Txt_" + content);
            go.transform.SetParent(parent, false);

            var t = go.AddComponent<Text>();
            t.font          = Resources.GetBuiltinResource<Font>("Arial.ttf");
            t.text          = content;
            t.fontSize      = size;
            t.fontStyle     = style;
            t.color         = color;
            t.alignment     = TextAnchor.MiddleLeft;
            t.raycastTarget = false;

            var elem = go.AddComponent<LayoutElement>();
            elem.preferredWidth  = w;
            elem.preferredHeight = h;
            elem.minHeight       = h;

            return t;
        }

        private static Color ParseColor(string hex, Color fallback)
        {
            if (hex != null && ColorUtility.TryParseHtmlString(hex, out Color c))
                return c;
            return fallback;
        }
    }
}

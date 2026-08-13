using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMO epicasset 번들에서 EpicHudPanelCanvas 프리팹을 직접 인스턴스화하여
    /// WackyEpicMMOSystem과 완전히 동일한 HUD를 구성하는 클래스
    /// </summary>
    internal static class CaptainHudBuilder
    {
        // ──────────────────────────────────────────
        // 레벨 색상 상수 (CaptainExpHud.cs에서 참조)
        // ──────────────────────────────────────────

        internal static readonly Color COL_LVL_DEFAULT = new Color(1.00f, 0.85f, 0.30f, 1.00f);
        internal static readonly Color COL_LVL_SILVER  = new Color(0.95f, 0.80f, 0.55f, 1.00f);
        internal static readonly Color COL_LVL_GOLD    = new Color(1.00f, 0.60f, 0.05f, 1.00f);
        internal static readonly Color COL_LVL_PLAT    = new Color(0.90f, 0.75f, 1.00f, 1.00f);

        // 패널 폭 (에이트르 유무에 따라 조정)
        private const float WIDTH_NO_EITR   = 1050f;
        private const float WIDTH_WITH_EITR = 1475f;

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
        // 진입점: epicasset 번들에서 프리팹 로딩
        // ──────────────────────────────────────────

        /// <summary>
        /// epicasset AssetBundle에서 EpicHudPanelCanvas 프리팹을 인스턴스화하고
        /// Transform.Find()로 UI 참조를 획득하여 반환
        /// </summary>
        internal static HudComponents Build()
        {
            var comp = new HudComponents();

            // 1. epicasset 번들 로드 (CaptainLevelUpVFX와 공유 캐시 사용, 중복 로드 방지)
            var bundle = EpicAssetBundleCache.Get();
            if (bundle == null)
            {
                Plugin.Log.LogError("[CaptainHudBuilder] epicasset AssetBundle을 가져올 수 없습니다.");
                return comp;
            }

            // 2. EpicHudPanelCanvas 프리팹 로드 & 인스턴스화
            var prefab = bundle.LoadAsset<GameObject>("EpicHudPanelCanvas");
            if (prefab == null)
            {
                Plugin.Log.LogError("[CaptainHudBuilder] EpicHudPanelCanvas 프리팹을 찾을 수 없습니다.");
                return comp;
            }

            // native Unity 경고 억제: Application.logMessageReceivedThreaded 백킹 필드를 reflection으로 일시 null화
            var threadedField = typeof(Application).GetField(
                "s_LogCallbackHandlerThreaded",
                BindingFlags.Static | BindingFlags.NonPublic);
            Application.LogCallback savedCallback = null;
            try
            {
                if (threadedField != null)
                {
                    savedCallback = (Application.LogCallback)threadedField.GetValue(null);
                    threadedField.SetValue(null, null);
                }
            }
            catch { /* reflection 실패 시 무시 - 경고 표시만 될 뿐 */ }

            Transform panelRoot;
            try { panelRoot = Object.Instantiate(prefab).transform; }
            finally
            {
                try { if (threadedField != null) threadedField.SetValue(null, savedCallback); }
                catch { }
            }
            UnityEngine.Object.DontDestroyOnLoad(panelRoot.gameObject);

            // 3. Canvas 설정
            var canvas = panelRoot.GetComponentInChildren<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode   = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 1511;
            }
            comp.Canvas = canvas;

            // CanvasScaler 스케일
            var scaler = panelRoot.GetComponentInChildren<CanvasScaler>();
            if (scaler != null)
                scaler.scaleFactor = CaptainLevelConfig.HudScale?.Value ?? 1.0f;

            // 4. Transform.Find()로 UI 컴포넌트 참조 (EpicMMO InitHudPanel 동일 경로)
            var expPanel = panelRoot.Find("EpicHudPanel");
            comp.PanelRt = expPanel?.GetComponent<RectTransform>();

            // Exp
            comp.LevelText      = expPanel?.Find("Container/Exp/Lvl")?.GetComponent<Text>();
            comp.ExpPercentText = expPanel?.Find("Container/Exp/Exp")?.GetComponent<Text>();
            comp.ExpBarFill     = expPanel?.Find("Container/Exp/Bar/Fill")?.GetComponent<Image>();
            comp.ExpBarGlow     = null; // EpicMMO 프리팹에 Glow 없음

            // HP
            comp.HpText    = expPanel?.Find("Container/Hp/Text")?.GetComponent<Text>();
            comp.HpBarFill = expPanel?.Find("Container/Hp/Bar/Fill")?.GetComponent<Image>();
            comp.HpPanel   = expPanel?.Find("Container/Hp")?.gameObject;

            // 스태미나
            comp.StaminaText    = expPanel?.Find("Container/Stamina/Text")?.GetComponent<Text>();
            comp.StaminaBarFill = expPanel?.Find("Container/Stamina/Bar/Fill")?.GetComponent<Image>();
            comp.StaminaPanel   = expPanel?.Find("Container/Stamina")?.gameObject;

            // 에이트르 (기본 숨김)
            comp.EitrText    = expPanel?.Find("Container/Eitr/Text")?.GetComponent<Text>();
            comp.EitrBarFill = expPanel?.Find("Container/Eitr/Bar/Fill")?.GetComponent<Image>();
            comp.EitrPanel   = expPanel?.Find("Container/Eitr")?.gameObject;
            comp.EitrPanel?.SetActive(false);

            // 5. 드래그 - Canvas 루트가 아닌 실제 패널(EpicHudPanel)에 부착
            if (expPanel != null)
                CaptainHudDrag.AttachTo(expPanel.gameObject, "MainHud", canvas);

            // 6. 번들은 EpicAssetBundleCache가 세션 동안 유지 관리 (여기서 언로드하지 않음)

            // 컴포넌트 참조 null 경고
            LogNullWarnings(comp);

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
        // 공통 유틸 (외부 참조 유지)
        // ──────────────────────────────────────────

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

        // ──────────────────────────────────────────
        // 헬퍼
        // ──────────────────────────────────────────

        private static void LogNullWarnings(HudComponents comp)
        {
            if (comp.LevelText      == null) Plugin.Log.LogWarning("[CaptainHudBuilder] LevelText null (경로: Container/Exp/Lvl)");
            if (comp.ExpPercentText == null) Plugin.Log.LogWarning("[CaptainHudBuilder] ExpPercentText null (경로: Container/Exp/Exp)");
            if (comp.ExpBarFill     == null) Plugin.Log.LogWarning("[CaptainHudBuilder] ExpBarFill null (경로: Container/Exp/Bar/Fill)");
            if (comp.HpBarFill      == null) Plugin.Log.LogWarning("[CaptainHudBuilder] HpBarFill null (경로: Container/Hp/Bar/Fill)");
            if (comp.StaminaBarFill == null) Plugin.Log.LogWarning("[CaptainHudBuilder] StaminaBarFill null (경로: Container/Stamina/Bar/Fill)");
        }
    }
}

using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain Exp HUD - EpicMMO 스타일 세련된 경험치/스탯 HUD
    /// HP / 스태미나 / 에이트르 / 경험치 바 포함
    /// EpicMMO가 없을 때만 활성화됨
    /// </summary>
    [HarmonyPatch]
    public static class CaptainExpHud
    {
        // ──────────────────────────────────────────
        // UI 컴포넌트 캐시
        // ──────────────────────────────────────────
        private static GameObject _hudCanvas;

        // 경험치/레벨
        private static Text  _levelText;
        private static Text  _expPercentText;
        private static Image _expBarFill;
        private static Image _expBarGlow;   // 빛나는 하이라이트

        // HP
        private static Text  _hpText;
        private static Image _hpBarFill;
        private static GameObject _hpRow;

        // 스태미나
        private static Text  _staminaText;
        private static Image _staminaBarFill;
        private static GameObject _staminaRow;

        // 에이트르 (마법 바)
        private static Text  _eitrText;
        private static Image _eitrBarFill;
        private static GameObject _eitrRow;

        // 바닐라 HP 패널 (숨기기용)
        private static GameObject _vanillaHealth;
        private static GameObject _vanillaHealthIcon;

        // 드래그
        private static RectTransform _panelRt;
        private static bool  _isDragging;
        private static Vector2 _dragOffset;

        // 초기화 플래그
        private static bool _initialized;

        // ──────────────────────────────────────────
        // 색상 상수 (EpicMMO 팔레트 참조)
        // ──────────────────────────────────────────
        private static readonly Color COL_PANEL_BG    = new Color(0.04f, 0.04f, 0.10f, 0.86f);
        private static readonly Color COL_BORDER      = new Color(0.25f, 0.30f, 0.45f, 0.70f);
        private static readonly Color COL_BAR_BG      = new Color(0.08f, 0.08f, 0.14f, 0.90f);
        private static readonly Color COL_HP          = new Color(0.90f, 0.15f, 0.15f, 1.00f);
        private static readonly Color COL_HP_GLOW     = new Color(1.00f, 0.40f, 0.40f, 0.45f);
        private static readonly Color COL_STAMINA     = new Color(0.94f, 0.72f, 0.08f, 1.00f);
        private static readonly Color COL_STAMINA_GLOW= new Color(1.00f, 0.90f, 0.30f, 0.45f);
        private static readonly Color COL_EITR        = new Color(0.22f, 0.50f, 1.00f, 1.00f);
        private static readonly Color COL_EITR_GLOW   = new Color(0.45f, 0.70f, 1.00f, 0.45f);
        private static readonly Color COL_EXP         = new Color(0.28f, 0.82f, 1.00f, 1.00f);
        private static readonly Color COL_EXP_GLOW    = new Color(0.55f, 0.95f, 1.00f, 0.40f);
        private static readonly Color COL_LVL_DEFAULT = new Color(1.00f, 0.85f, 0.20f, 1.00f);
        private static readonly Color COL_LVL_SILVER  = new Color(0.80f, 0.88f, 1.00f, 1.00f);
        private static readonly Color COL_LVL_GOLD    = new Color(1.00f, 0.65f, 0.05f, 1.00f);
        private static readonly Color COL_LVL_PLAT    = new Color(0.60f, 0.95f, 1.00f, 1.00f);

        // ──────────────────────────────────────────
        // Harmony Patches
        // ──────────────────────────────────────────

        [HarmonyPatch(typeof(Hud), "Awake")]
        [HarmonyPostfix]
        public static void Hud_Awake_Postfix(Hud __instance)
        {
            try
            {
                if (CaptainMMOBridge.UseEpicMMO) return;
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;
                if (!CaptainLevelConfig.ShowLevelHUD.Value) return;

                Build(__instance);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainExpHud] Hud_Awake 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(Game), "SpawnPlayer")]
        [HarmonyPostfix]
        public static void Game_SpawnPlayer_Postfix()
        {
            try
            {
                if (!_initialized) return;
                UpdateExpBar();
                _hudCanvas?.SetActive(true);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] SpawnPlayer 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(Hud), "SetVisible")]
        [HarmonyPostfix]
        public static void Hud_SetVisible_Postfix(bool visible)
        {
            try
            {
                if (!_initialized) return;
                _hudCanvas?.SetActive(visible && CaptainLevelConfig.ShowLevelHUD.Value);
            }
            catch { }
        }

        // Hud.Update 에서 HP / 스태미나 / 에이트르 / 경험치 모두 업데이트
        // (UpdateHealth/UpdateStamina/UpdateEitr는 publicized DLL에 미노출 - 폴링 방식으로 대체)
        [HarmonyPatch(typeof(Hud), "Update")]
        [HarmonyPostfix]
        public static void Hud_Update_Postfix()
        {
            if (!_initialized) return;

            HandleDrag();

            var player = Player.m_localPlayer;
            if (player != null)
            {
                // HP / 스태미나 - 매 5프레임
                if (Time.frameCount % 5 == 0)
                {
                    UpdateBar(_hpBarFill,      _hpText,      player.GetHealth(),   player.GetMaxHealth());
                    UpdateBar(_staminaBarFill, _staminaText, player.GetStamina(),  player.GetMaxStamina());
                }

                // 에이트르 - 매 10프레임 (show/hide + 값)
                if (Time.frameCount % 10 == 0)
                {
                    float eitrMax = player.GetMaxEitr();
                    bool hasEitr = eitrMax > 2f;
                    if (_eitrRow != null && _eitrRow.activeSelf != hasEitr)
                        _eitrRow.SetActive(hasEitr);
                    if (hasEitr)
                        UpdateBar(_eitrBarFill, _eitrText, player.GetEitr(), eitrMax);
                }
            }

            // 경험치 - 30프레임마다
            if (Time.frameCount % 30 == 0)
                UpdateExpBar();
        }

        // ──────────────────────────────────────────
        // HUD 빌드 (코드 생성)
        // ──────────────────────────────────────────

        private static void Build(Hud hud)
        {
            if (_initialized) return;

            // 바닐라 HP 패널 숨기기 (EpicMMO와 동일)
            _vanillaHealth     = hud.m_healthPanel?.Find("Health")?.gameObject;
            _vanillaHealthIcon = hud.m_healthPanel?.Find("healthicon")?.gameObject;
            _vanillaHealth?.SetActive(false);
            _vanillaHealthIcon?.SetActive(false);

            // 전용 Canvas (sortingOrder 1511 - EpicMMO와 동일)
            var canvasGO = new GameObject("CaptainExpHUDCanvas");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1511;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            UnityEngine.Object.DontDestroyOnLoad(canvasGO);
            _hudCanvas = canvasGO;

            // ── 메인 패널 (좌하단, 드래그 가능) ──
            var panel = new GameObject("EpicStylePanel");
            panel.transform.SetParent(canvasGO.transform, false);

            _panelRt = panel.AddComponent<RectTransform>();
            _panelRt.anchorMin = new Vector2(0f, 0f);
            _panelRt.anchorMax = new Vector2(0f, 0f);
            _panelRt.pivot     = new Vector2(0f, 0f);
            _panelRt.anchoredPosition = new Vector2(10f, 10f);
            _panelRt.sizeDelta = new Vector2(560f, 120f);

            // 배경
            var panelBg = panel.AddComponent<Image>();
            panelBg.color = COL_PANEL_BG;
            panelBg.raycastTarget = true; // 드래그용

            // 테두리 (살짝 큰 이미지 뒤에 배치)
            var border = CreateChild(panel.transform, "Border");
            var borderRt = border.GetComponent<RectTransform>();
            borderRt.anchorMin = Vector2.zero;
            borderRt.anchorMax = Vector2.one;
            borderRt.offsetMin = new Vector2(-1f, -1f);
            borderRt.offsetMax = new Vector2(1f, 1f);
            var borderImg = border.AddComponent<Image>();
            borderImg.color = COL_BORDER;
            borderImg.raycastTarget = false;
            border.transform.SetAsFirstSibling();

            // ── 컨테이너 (VerticalLayoutGroup) ──
            var container = new GameObject("Container");
            container.transform.SetParent(panel.transform, false);
            var containerRt = container.AddComponent<RectTransform>();
            containerRt.anchorMin = Vector2.zero;
            containerRt.anchorMax = Vector2.one;
            containerRt.offsetMin = new Vector2(10f, 8f);
            containerRt.offsetMax = new Vector2(-10f, -8f);

            var vLayout = container.AddComponent<VerticalLayoutGroup>();
            vLayout.spacing = 5f;
            vLayout.childForceExpandWidth = true;
            vLayout.childForceExpandHeight = false;
            vLayout.childAlignment = TextAnchor.UpperLeft;

            // ── 윗줄: HP + 스태미나 (수평) ──
            var topRow = new GameObject("TopRow");
            topRow.transform.SetParent(container.transform, false);
            var topLayout = topRow.AddComponent<HorizontalLayoutGroup>();
            topLayout.spacing = 14f;
            topLayout.childForceExpandWidth = false;
            topLayout.childForceExpandHeight = false;
            topLayout.childAlignment = TextAnchor.MiddleLeft;
            var topFitter = topRow.AddComponent<ContentSizeFitter>();
            topFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            topFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            _hpRow      = BuildStatBar(topRow.transform, "♥", COL_HP, COL_HP_GLOW, 220f, out _hpText, out _hpBarFill);
            _staminaRow = BuildStatBar(topRow.transform, "⚡", COL_STAMINA, COL_STAMINA_GLOW, 220f, out _staminaText, out _staminaBarFill);

            // ── 에이트르 줄 (숨겨진 상태) ──
            var eitrRowWrap = new GameObject("EitrRowWrap");
            eitrRowWrap.transform.SetParent(container.transform, false);
            _eitrRow = eitrRowWrap;
            _eitrRow.SetActive(false);
            var eitrLayout = eitrRowWrap.AddComponent<HorizontalLayoutGroup>();
            eitrLayout.childForceExpandWidth = false;
            eitrLayout.childForceExpandHeight = false;
            var eitrFitter = eitrRowWrap.AddComponent<ContentSizeFitter>();
            eitrFitter.verticalFit   = ContentSizeFitter.FitMode.PreferredSize;
            eitrFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            BuildStatBar(eitrRowWrap.transform, "✦", COL_EITR, COL_EITR_GLOW, 454f, out _eitrText, out _eitrBarFill);

            // ── 경험치 줄 ──
            BuildExpRow(container.transform);

            _initialized = true;
            _hudCanvas.SetActive(false);
            Plugin.Log.LogInfo("[CaptainExpHud] EpicMMO 스타일 HUD 빌드 완료");
        }

        // ── 스탯 바 한 줄 (아이콘 + 값 + 바) ──
        private static GameObject BuildStatBar(Transform parent, string icon, Color fillColor, Color glowColor,
            float barWidth, out Text textOut, out Image fillOut)
        {
            var row = new GameObject($"StatRow_{icon}");
            row.transform.SetParent(parent, false);

            var hLayout = row.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing = 5f;
            hLayout.childForceExpandWidth = false;
            hLayout.childForceExpandHeight = false;
            hLayout.childAlignment = TextAnchor.MiddleLeft;

            var fitter = row.AddComponent<ContentSizeFitter>();
            fitter.verticalFit   = ContentSizeFitter.FitMode.PreferredSize;
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            // 아이콘 레이블
            var iconLabel = CreateText(row.transform, icon, 13, FontStyle.Bold, fillColor, 16f, 18f);

            // 숫자 텍스트
            textOut = CreateText(row.transform, "0/0", 11, FontStyle.Normal, Color.white, 68f, 18f);
            textOut.alignment = TextAnchor.MiddleRight;

            // 바 컨테이너
            var barWrap = new GameObject("BarWrap");
            barWrap.transform.SetParent(row.transform, false);
            var barWrapElem = barWrap.AddComponent<LayoutElement>();
            barWrapElem.preferredWidth  = barWidth;
            barWrapElem.preferredHeight = 14f;
            barWrapElem.minHeight       = 14f;

            // 바 배경
            var barBg = barWrap.AddComponent<Image>();
            barBg.color = COL_BAR_BG;
            barBg.raycastTarget = false;

            // 글로우 레이어
            var glowGO = CreateChild(barWrap.transform, "Glow");
            var glowRt = glowGO.GetComponent<RectTransform>();
            glowRt.anchorMin = Vector2.zero; glowRt.anchorMax = Vector2.one;
            glowRt.offsetMin = Vector2.zero; glowRt.offsetMax = Vector2.zero;
            var glowImg = glowGO.AddComponent<Image>();
            glowImg.color = glowColor;
            glowImg.raycastTarget = false;
            glowImg.type = Image.Type.Filled;
            glowImg.fillMethod = Image.FillMethod.Horizontal;
            glowImg.fillOrigin = (int)Image.OriginHorizontal.Left;
            glowImg.fillAmount = 1f;

            // 메인 Fill
            var fillGO = CreateChild(barWrap.transform, "Fill");
            var fillRt = fillGO.GetComponent<RectTransform>();
            fillRt.anchorMin = Vector2.zero; fillRt.anchorMax = Vector2.one;
            fillRt.offsetMin = new Vector2(0f, 2f); fillRt.offsetMax = new Vector2(0f, -2f);
            fillOut = fillGO.AddComponent<Image>();
            fillOut.color = fillColor;
            fillOut.raycastTarget = false;
            fillOut.type = Image.Type.Filled;
            fillOut.fillMethod = Image.FillMethod.Horizontal;
            fillOut.fillOrigin = (int)Image.OriginHorizontal.Left;
            fillOut.fillAmount = 1f;

            // glowImg도 fill과 동기화
            var elem = row.AddComponent<LayoutElement>();
            elem.minHeight = 18f;

            return row;
        }

        // ── 경험치 줄 (레벨 뱃지 + % 텍스트 + 넓은 바) ──
        private static void BuildExpRow(Transform parent)
        {
            var row = new GameObject("ExpRow");
            row.transform.SetParent(parent, false);

            var vLayout = row.AddComponent<VerticalLayoutGroup>();
            vLayout.spacing = 3f;
            vLayout.childForceExpandWidth = true;
            vLayout.childForceExpandHeight = false;

            var fitter = row.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var elem = row.AddComponent<LayoutElement>();
            elem.minHeight = 32f;

            // 헤더줄 (레벨 + %)
            var header = new GameObject("ExpHeader");
            header.transform.SetParent(row.transform, false);

            var hLayout = header.AddComponent<HorizontalLayoutGroup>();
            hLayout.spacing = 6f;
            hLayout.childForceExpandWidth = false;
            hLayout.childForceExpandHeight = false;
            hLayout.childAlignment = TextAnchor.MiddleLeft;

            var headerFitter = header.AddComponent<ContentSizeFitter>();
            headerFitter.verticalFit   = ContentSizeFitter.FitMode.PreferredSize;
            headerFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            // 레벨 텍스트 (금색, 굵게)
            _levelText = CreateText(header.transform, "LV.1", 16, FontStyle.Bold, COL_LVL_DEFAULT, 90f, 22f);

            // 구분선
            CreateText(header.transform, "│", 14, FontStyle.Normal, new Color(0.4f, 0.4f, 0.5f), 12f, 22f);

            // 경험치 % 텍스트
            _expPercentText = CreateText(header.transform, "0.00 %", 13, FontStyle.Normal, new Color(0.82f, 0.82f, 0.82f), 80f, 22f);

            // 경험치 바
            var barWrap = new GameObject("ExpBarWrap");
            barWrap.transform.SetParent(row.transform, false);

            var barElem = barWrap.AddComponent<LayoutElement>();
            barElem.minHeight       = 10f;
            barElem.preferredHeight = 10f;

            var barBg = barWrap.AddComponent<Image>();
            barBg.color = COL_BAR_BG;
            barBg.raycastTarget = false;

            // 글로우 레이어
            var glowGO = CreateChild(barWrap.transform, "ExpGlow");
            var glowRt = glowGO.GetComponent<RectTransform>();
            glowRt.anchorMin = Vector2.zero; glowRt.anchorMax = Vector2.one;
            glowRt.offsetMin = Vector2.zero; glowRt.offsetMax = Vector2.zero;
            _expBarGlow = glowGO.AddComponent<Image>();
            _expBarGlow.color = COL_EXP_GLOW;
            _expBarGlow.raycastTarget = false;
            _expBarGlow.type = Image.Type.Filled;
            _expBarGlow.fillMethod = Image.FillMethod.Horizontal;
            _expBarGlow.fillOrigin = (int)Image.OriginHorizontal.Left;
            _expBarGlow.fillAmount = 0f;

            // 메인 Fill
            var fillGO = CreateChild(barWrap.transform, "ExpFill");
            var fillRt = fillGO.GetComponent<RectTransform>();
            fillRt.anchorMin = Vector2.zero; fillRt.anchorMax = Vector2.one;
            fillRt.offsetMin = new Vector2(0f, 2f); fillRt.offsetMax = new Vector2(0f, -2f);
            _expBarFill = fillGO.AddComponent<Image>();
            _expBarFill.color = COL_EXP;
            _expBarFill.raycastTarget = false;
            _expBarFill.type = Image.Type.Filled;
            _expBarFill.fillMethod = Image.FillMethod.Horizontal;
            _expBarFill.fillOrigin = (int)Image.OriginHorizontal.Left;
            _expBarFill.fillAmount = 0f;
        }

        // ──────────────────────────────────────────
        // 업데이트 로직
        // ──────────────────────────────────────────

        private static void UpdateBar(Image fill, Text text, float current, float max)
        {
            if (fill == null || text == null) return;
            float ratio = max > 0f ? Mathf.Clamp01(current / max) : 0f;
            fill.fillAmount = ratio;

            // 글로우도 같이 업데이트 (fill의 부모에서 찾기)
            var glowImg = fill.transform.parent?.Find("Glow")?.GetComponent<Image>();
            if (glowImg != null) glowImg.fillAmount = ratio;

            text.text = $"{Mathf.CeilToInt(current)}/{Mathf.CeilToInt(max)}";
        }

        public static void UpdateExpBar()
        {
            try
            {
                if (!_initialized || _levelText == null) return;

                var ls = CaptainLevelSystem.Instance;
                if (ls == null) return;

                int   level = ls.Level;
                long  cur   = ls.CurrentExp;
                long  need  = ls.GetExpToNextLevel();
                float ratio = need > 0 ? Mathf.Clamp01((float)cur / need) : 0f;

                // 레벨 텍스트 + 색상
                _levelText.text  = $"LV.{level}";
                _levelText.color = LevelColor(level);

                // 경험치 %
                _expPercentText.text = $"{ratio * 100f:F2} %";

                // 경험치 바 색상 (레벨 구간별)
                var expColor = ExpBarColor(level);
                _expBarFill.fillAmount = ratio;
                _expBarFill.color = expColor;
                if (_expBarGlow != null)
                {
                    _expBarGlow.fillAmount = ratio;
                    _expBarGlow.color = new Color(expColor.r, expColor.g, expColor.b, 0.40f);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] UpdateExpBar 오류: {ex.Message}");
            }
        }

        private static Color LevelColor(int level)
        {
            if (level >= 80) return COL_LVL_PLAT;
            if (level >= 50) return COL_LVL_GOLD;
            if (level >= 25) return COL_LVL_SILVER;
            return COL_LVL_DEFAULT;
        }

        private static Color ExpBarColor(int level)
        {
            if (level >= 80) return COL_LVL_PLAT;
            if (level >= 50) return new Color(1.00f, 0.65f, 0.05f, 1f); // 금
            if (level >= 25) return new Color(0.72f, 0.45f, 1.00f, 1f); // 보라
            return COL_EXP; // 기본 하늘색
        }

        // ──────────────────────────────────────────
        // 드래그
        // ──────────────────────────────────────────

        private static void HandleDrag()
        {
            if (_panelRt == null) return;

            if (Input.GetMouseButtonDown(0) && IsOverPanel())
            {
                _isDragging = true;
                _dragOffset = _panelRt.anchoredPosition - (Vector2)Input.mousePosition;
            }
            if (_isDragging)
            {
                _panelRt.anchoredPosition = (Vector2)Input.mousePosition + _dragOffset;
                if (Input.GetMouseButtonUp(0))
                    _isDragging = false;
            }
        }

        private static bool IsOverPanel()
        {
            if (_panelRt == null) return false;
            Vector2 m = Input.mousePosition;
            Vector2 p = _panelRt.anchoredPosition;
            Rect r    = new Rect(p.x, p.y, _panelRt.sizeDelta.x, _panelRt.sizeDelta.y);
            return r.Contains(m);
        }

        // ──────────────────────────────────────────
        // 공개 API
        // ──────────────────────────────────────────

        public static void ShowExpGainPopup(int expGained)
        {
            try
            {
                if (!CaptainLevelConfig.ShowExpPopup.Value) return;
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.TopLeft,
                    $"<color=#55CCFF>+{expGained:N0} EXP</color>");
            }
            catch { }
        }

        public static void ShowLevelUpNotification(int newLevel)
        {
            try
            {
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center,
                    $"<color=#FFD700>✦ LEVEL UP! ✦ LV.{newLevel}</color>");

                if (!CaptainLevelConfig.ShowLevelUpEffect.Value) return;
                var player = Player.m_localPlayer;
                if (player == null) return;

                var pos = player.transform.position + Vector3.up;
                TrySpawnEffect(ZNetScene.instance?.GetPrefab("fx_skillup"), pos);
                TrySpawnEffect(ZNetScene.instance?.GetPrefab("fx_GP_activated"), pos);
            }
            catch { }
        }

        public static void ToggleExpHud(bool show)
            => _hudCanvas?.SetActive(show && _initialized);

        // ──────────────────────────────────────────
        // 헬퍼
        // ──────────────────────────────────────────

        private static void TrySpawnEffect(GameObject prefab, Vector3 pos)
        {
            if (prefab != null)
                UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
        }

        private static GameObject CreateChild(Transform parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            go.AddComponent<RectTransform>();
            return go;
        }

        private static Text CreateText(Transform parent, string content, int size, FontStyle style,
            Color color, float w, float h)
        {
            var go = new GameObject("Txt_" + content);
            go.transform.SetParent(parent, false);

            var t = go.AddComponent<Text>();
            t.font      = Resources.GetBuiltinResource<Font>("Arial.ttf");
            t.text      = content;
            t.fontSize  = size;
            t.fontStyle = style;
            t.color     = color;
            t.alignment = TextAnchor.MiddleLeft;
            t.raycastTarget = false;

            var elem = go.AddComponent<LayoutElement>();
            elem.preferredWidth  = w;
            elem.preferredHeight = h;
            elem.minHeight       = h;

            return t;
        }
    }
}

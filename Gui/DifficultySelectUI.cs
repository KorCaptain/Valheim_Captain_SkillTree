using UnityEngine;
using UnityEngine.UI;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.Gui
{
    /// <summary>
    /// 난이도 선택 모달 창.
    /// 모드 첫 설치 또는 버전 업데이트 시 FejdStartup(메인 메뉴) 에서 표시.
    ///
    /// 옵션:
    ///   1. Normal      — Vanilla +a (약한 수치)
    ///   2. Very Hard   — CLLC Very Hard + Monster HP x2 (강한 수치)
    ///   3. User Setting — 이전 세션 유저 설정 (User 백업 파일이 있을 때만 표시)
    /// </summary>
    public class DifficultySelectUI : MonoBehaviour
    {
        private void Awake()
        {
            BuildUI();
        }

        private void BuildUI()
        {
            bool hasUser = DifficultyManager.HasUserPreset();

            // ── 전체화면 반투명 오버레이 ──
            var rect = gameObject.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            var overlay = gameObject.AddComponent<Image>();
            overlay.color         = new Color(0f, 0f, 0f, 0.78f);
            overlay.raycastTarget = true;

            // ── 중앙 박스 ──
            float boxH = hasUser ? 470f : 390f;
            var box = CreatePanel("DiffBox", gameObject.transform,
                new Vector2(760f, boxH), Vector2.zero,
                new Color(0.07f, 0.07f, 0.07f, 0.97f));

            float y = boxH * 0.5f - 32f;

            // 제목 (가운데)
            CreateText(box.transform, "Title",
                L.Get("difficulty_title"),
                new Vector2(0f, y), new Vector2(740f, 38f),
                17, Color.yellow, TextAnchor.MiddleCenter);
            y -= 42f;

            // 구분선
            CreateSep(box.transform, y); y -= 30f;

            // ── 1. Normal ──
            CreateOptionRow(box.transform, ref y,
                L.Get("difficulty_normal_title"),
                L.Get("difficulty_normal_desc"),
                Color.white);

            // ── 2. Very Hard ──
            CreateOptionRow(box.transform, ref y,
                L.Get("difficulty_veryhard_title"),
                L.Get("difficulty_veryhard_desc"),
                new Color(1f, 0.7f, 0.4f));

            // ── 3. User Setting (백업 있을 때만) ──
            if (hasUser)
            {
                CreateOptionRow(box.transform, ref y,
                    L.Get("difficulty_user_title"),
                    L.Get("difficulty_user_desc"),
                    new Color(0.5f, 0.85f, 1f));
            }

            y -= 6f;
            // 구분선
            CreateSep(box.transform, y); y -= 26f;

            // 안내
            CreateText(box.transform, "Hint",
                L.Get("difficulty_hint"),
                new Vector2(0f, y), new Vector2(740f, 24f),
                11, new Color(0.5f, 0.5f, 0.5f), TextAnchor.MiddleCenter);
            y -= 40f;

            // ── 버튼 ──
            if (hasUser)
            {
                CreateDiffButton(box.transform, L.Get("difficulty_btn_normal"),   new Vector2(-230f, y),
                    new Color(0.15f, 0.40f, 0.15f), OnNormal);
                CreateDiffButton(box.transform, L.Get("difficulty_btn_veryhard"), new Vector2(0f,    y),
                    new Color(0.45f, 0.15f, 0.10f), OnVeryHard);
                CreateDiffButton(box.transform, L.Get("difficulty_btn_user"),     new Vector2(230f,  y),
                    new Color(0.10f, 0.30f, 0.50f), OnUser);
            }
            else
            {
                CreateDiffButton(box.transform, L.Get("difficulty_btn_normal"),   new Vector2(-180f, y),
                    new Color(0.15f, 0.40f, 0.15f), OnNormal);
                CreateDiffButton(box.transform, L.Get("difficulty_btn_veryhard"), new Vector2(180f,  y),
                    new Color(0.45f, 0.15f, 0.10f), OnVeryHard);
            }
        }

        // ────── 선택 핸들러 ──────
        private bool _selected = false;

        private void OnNormal()   { _selected = true; DifficultyManager.ApplyNormal();   Destroy(gameObject); }
        private void OnVeryHard() { _selected = true; DifficultyManager.ApplyVeryHard(); Destroy(gameObject); }
        private void OnUser()     { _selected = true; DifficultyManager.ApplyUser();     Destroy(gameObject); }

        private void OnDestroy()
        {
            if (!_selected)
                DifficultyManager.ApplyVeryHard(); // 선택 없이 창이 닫힌 경우 기본 베리하드 적용
        }

        // ────── 레이아웃 헬퍼 ──────

        /// <summary>옵션 한 행 (굵은 제목 + 설명 두 줄 좌측 정렬)</summary>
        private static void CreateOptionRow(Transform parent, ref float y,
            string title, string desc, Color titleColor)
        {
            // 제목
            CreateText(parent, "OTitle_" + title,
                title,
                new Vector2(-10f, y), new Vector2(720f, 28f),
                14, titleColor, TextAnchor.MiddleLeft, FontStyle.Bold);
            y -= 28f;

            // 설명
            CreateText(parent, "ODesc_" + title,
                desc,
                new Vector2(-10f, y), new Vector2(720f, 24f),
                13, new Color(0.8f, 0.8f, 0.8f), TextAnchor.MiddleLeft);
            y -= 32f;
        }

        private static void CreateSep(Transform parent, float y)
        {
            var go = new GameObject("Sep");
            go.transform.SetParent(parent, false);
            var r = go.AddComponent<RectTransform>();
            r.anchoredPosition = new Vector2(0f, y);
            r.sizeDelta        = new Vector2(720f, 1f);
            var img = go.AddComponent<Image>();
            img.color = new Color(0.35f, 0.35f, 0.35f, 1f);
        }

        private static GameObject CreatePanel(string name, Transform parent,
            Vector2 size, Vector2 pos, Color color)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var r = go.AddComponent<RectTransform>();
            r.sizeDelta        = size;
            r.anchoredPosition = pos;
            var img = go.AddComponent<Image>();
            img.color = color;
            return go;
        }

        private static void CreateText(Transform parent, string name, string content,
            Vector2 pos, Vector2 size, int fontSize, Color color,
            TextAnchor anchor, FontStyle style = FontStyle.Normal)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var r = go.AddComponent<RectTransform>();
            r.anchoredPosition = pos;
            r.sizeDelta        = size;
            var t = go.AddComponent<Text>();
            t.text      = content;
            t.fontSize  = fontSize;
            t.fontStyle = style;
            t.alignment = anchor;
            t.color     = color;
            t.font      = Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        private static void CreateDiffButton(Transform parent, string label,
            Vector2 pos, Color bgColor, UnityEngine.Events.UnityAction onClick)
        {
            var go = new GameObject("Btn_" + label);
            go.transform.SetParent(parent, false);
            var r = go.AddComponent<RectTransform>();
            r.anchoredPosition = pos;
            r.sizeDelta        = new Vector2(200f, 48f);

            var img = go.AddComponent<Image>();
            img.color = bgColor;

            var btn = go.AddComponent<Button>();
            btn.targetGraphic = img;
            var cols = btn.colors;
            cols.highlightedColor = new Color(
                Mathf.Min(bgColor.r + 0.15f, 1f),
                Mathf.Min(bgColor.g + 0.15f, 1f),
                Mathf.Min(bgColor.b + 0.15f, 1f));
            cols.pressedColor = new Color(
                Mathf.Max(bgColor.r - 0.1f, 0f),
                Mathf.Max(bgColor.g - 0.1f, 0f),
                Mathf.Max(bgColor.b - 0.1f, 0f));
            btn.colors = cols;
            btn.onClick.AddListener(onClick);

            var lblGo = new GameObject("Label");
            lblGo.transform.SetParent(go.transform, false);
            var tr = lblGo.AddComponent<RectTransform>();
            tr.anchorMin = Vector2.zero;
            tr.anchorMax = Vector2.one;
            tr.offsetMin = Vector2.zero;
            tr.offsetMax = Vector2.zero;
            var t = lblGo.AddComponent<Text>();
            t.text      = label;
            t.fontSize  = 14;
            t.fontStyle = FontStyle.Bold;
            t.alignment = TextAnchor.MiddleCenter;
            t.color     = Color.white;
            t.font      = Resources.GetBuiltinResource<Font>("Arial.ttf");
        }
    }
}

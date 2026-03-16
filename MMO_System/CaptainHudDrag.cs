using UnityEngine;
using UnityEngine.EventSystems;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// IDragHandler 기반 패널 드래그 컴포넌트
    /// PlayerPrefs로 위치 저장/복원 - 게임 재시작 후에도 마지막 위치 유지
    /// WackyEpicMMOSystem DragMenu.cs 패턴 참조 + 위치 저장/화면 바운더리 확장
    /// </summary>
    public class CaptainHudDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private string _keyX;
        private string _keyY;
        private RectTransform _rt;
        private Canvas _canvas;

        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
        }

        // ──────────────────────────────────────────
        // 팩토리
        // ──────────────────────────────────────────

        /// <summary>
        /// GameObject에 CaptainHudDrag 부착 및 저장된 위치 복원
        /// </summary>
        /// <param name="go">드래그 대상 GameObject</param>
        /// <param name="panelKey">PlayerPrefs 저장 키 (예: "CaptainHud_HP")</param>
        /// <param name="canvas">부모 Canvas (화면 바운더리 계산용)</param>
        public static CaptainHudDrag AttachTo(GameObject go, string panelKey, Canvas canvas)
        {
            var drag = go.AddComponent<CaptainHudDrag>();
            drag._keyX    = $"CaptainHud_{panelKey}_X";
            drag._keyY    = $"CaptainHud_{panelKey}_Y";
            drag._canvas  = canvas;
            drag._rt      = go.GetComponent<RectTransform>();
            drag.RestorePosition();
            return drag;
        }

        // ──────────────────────────────────────────
        // IDragHandler 구현
        // ──────────────────────────────────────────

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 드래그 시작 시 별도 처리 불필요 (OnDrag에서 처리)
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_rt == null) return;

            _rt.position += (Vector3)eventData.delta;
            ClampToScreen();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SavePosition();
        }

        // ──────────────────────────────────────────
        // 위치 저장/복원
        // ──────────────────────────────────────────

        private void SavePosition()
        {
            if (_rt == null || _keyX == null) return;
            PlayerPrefs.SetFloat(_keyX, _rt.anchoredPosition.x);
            PlayerPrefs.SetFloat(_keyY, _rt.anchoredPosition.y);
            PlayerPrefs.Save();
        }

        private void RestorePosition()
        {
            if (_rt == null || _keyX == null) return;

            if (PlayerPrefs.HasKey(_keyX) && PlayerPrefs.HasKey(_keyY))
            {
                float x = PlayerPrefs.GetFloat(_keyX);
                float y = PlayerPrefs.GetFloat(_keyY);
                _rt.anchoredPosition = new Vector2(x, y);
                ClampToScreen();
            }
        }

        // ──────────────────────────────────────────
        // 화면 바운더리 클램프
        // ──────────────────────────────────────────

        private void ClampToScreen()
        {
            if (_rt == null) return;

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 size       = _rt.sizeDelta;

            // anchorMin/Max가 (0,0)이고 pivot이 (0,0)인 경우
            float x = Mathf.Clamp(_rt.anchoredPosition.x, 0f, screenSize.x - size.x);
            float y = Mathf.Clamp(_rt.anchoredPosition.y, 0f, screenSize.y - size.y);
            _rt.anchoredPosition = new Vector2(x, y);
        }
    }
}

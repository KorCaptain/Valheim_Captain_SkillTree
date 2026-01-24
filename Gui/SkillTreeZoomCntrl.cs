using UnityEngine;
using UnityEngine.EventSystems;

namespace CaptainSkillTree.Gui
{
    // SkillTree UI 전체에 마우스 휠로 확대/축소(줌인/줌아웃) 기능을 제공
    public class SkillTreeZoomCntrl : MonoBehaviour, IScrollHandler
    {
        [SerializeField] private float minimumScale = 0.5f;
        [SerializeField] private float maximumScale = 3.0f;
        [SerializeField] private float scaleStep = 0.0256f;

        private RectTransform _rectTransform;
        private float _scale = 1f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _scale = _rectTransform.localScale.x;
        }

        public void OnScroll(PointerEventData eventData)
        {
            float scrollDeltaY = eventData.scrollDelta.y * scaleStep;
            float newScale = Mathf.Clamp(_scale + scrollDeltaY, minimumScale, maximumScale);
            if (!Mathf.Approximately(newScale, _scale))
            {
                _scale = newScale;
                _rectTransform.localScale = new Vector3(_scale, _scale, _scale);
            }
        }
    }
} 
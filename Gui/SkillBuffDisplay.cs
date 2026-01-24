using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace CaptainSkillTree.Gui
{
    public class SkillBuffDisplay : MonoBehaviour
    {
        private static SkillBuffDisplay instance;
        public static SkillBuffDisplay Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("SkillBuffDisplay");
                    instance = go.AddComponent<SkillBuffDisplay>();
                    DontDestroyOnLoad(go);
                }
                return instance;
            }
        }

        private Dictionary<string, BuffUI> activeBuffs = new Dictionary<string, BuffUI>();
        private GameObject buffContainer;
        private Canvas worldCanvas;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeBuffDisplay();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void InitializeBuffDisplay()
        {
            // 월드 캔버스 생성
            GameObject canvasObj = new GameObject("SkillBuffCanvas");
            canvasObj.transform.SetParent(transform);
            worldCanvas = canvasObj.AddComponent<Canvas>();
            worldCanvas.renderMode = RenderMode.WorldSpace;
            worldCanvas.sortingOrder = 100;
            worldCanvas.worldCamera = Camera.main;
            
            var canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            canvasScaler.scaleFactor = 0.005f; // 월드 공간에서 더 작게 표시
            canvasScaler.referenceResolution = new Vector2(1920, 1080); // 해상도 기준 추가
            
            // 버프 컨테이너 생성
            buffContainer = new GameObject("BuffContainer");
            buffContainer.transform.SetParent(canvasObj.transform, false);
            var rectTransform = buffContainer.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 100);
            rectTransform.pivot = new Vector2(0.5f, 0f); // 하단 중앙을 기준점으로
        }

        public void ShowBuff(string buffId, string buffName, float duration, Color textColor, string icon = "")
        {
            if (Player.m_localPlayer == null) return;

            // 기존 버프가 있으면 업데이트
            if (activeBuffs.ContainsKey(buffId))
            {
                activeBuffs[buffId].UpdateDuration(duration);
                return;
            }

            // 새 버프 UI 생성
            var buffUI = CreateBuffUI(buffId, buffName, duration, textColor, icon);
            activeBuffs[buffId] = buffUI;
            
            // 버프 위치 재정렬
            ArrangeBuffs();
        }

        private BuffUI CreateBuffUI(string buffId, string buffName, float duration, Color textColor, string icon)
        {
            GameObject buffObj = new GameObject(string.Format("Buff_{0}", buffId));
            buffObj.transform.SetParent(buffContainer.transform, false);
            
            var buffUI = buffObj.AddComponent<BuffUI>();
            buffUI.Initialize(buffId, buffName, duration, textColor, icon);
            
            return buffUI;
        }

        private void ArrangeBuffs()
        {
            int index = 0;
            foreach (var buffUI in activeBuffs.Values)
            {
                var rect = buffUI.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, -index * 25);
                index++;
            }
        }

        public void RemoveBuff(string buffId)
        {
            if (activeBuffs.ContainsKey(buffId))
            {
                Destroy(activeBuffs[buffId].gameObject);
                activeBuffs.Remove(buffId);
                ArrangeBuffs();
            }
        }

        // === 성능 최적화된 위치 업데이트 ===
        private float lastPositionUpdate = 0f;
        private const float POSITION_UPDATE_INTERVAL = 0.05f; // 50ms (20 FPS)
        
        private void Update()
        {
            // ✅ IsDead() 체크 추가
            if (Player.m_localPlayer == null || Player.m_localPlayer.IsDead() || buffContainer == null || worldCanvas == null)
            {
                // 플레이어가 죽었으면 모든 버프 UI 제거
                if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead() && activeBuffs.Count > 0)
                {
                    // 플레이어 사망으로 모든 버프 UI 제거
                    foreach (var buffId in new List<string>(activeBuffs.Keys))
                    {
                        RemoveBuff(buffId);
                    }
                }
                return;
            }

            // 성능 최적화: 위치 업데이트 빈도 제한
            if (Time.time - lastPositionUpdate < POSITION_UPDATE_INTERVAL) return;
            lastPositionUpdate = Time.time;

            // 플레이어 머리 위에 버프 표시 위치 업데이트
            Vector3 playerHeadPos = Player.m_localPlayer.transform.position + Vector3.up * 2.5f;
            worldCanvas.transform.position = playerHeadPos;

            // 카메라를 향해 회전 (성능 최적화)
            if (Camera.main != null)
            {
                Vector3 directionToCamera = Camera.main.transform.position - worldCanvas.transform.position;
                worldCanvas.transform.rotation = Quaternion.LookRotation(-directionToCamera);
            }
        }

        public class BuffUI : MonoBehaviour
        {
            private string buffId;
            private string buffName;
            private float remainingTime;
            private Text nameText;
            private Text timeText;
            private Image iconImage;
            private Image backgroundImage;

            public void Initialize(string id, string name, float duration, Color textColor, string icon)
            {
                buffId = id;
                buffName = name;
                remainingTime = duration;

                CreateUI(textColor, icon);
                StartCoroutine(CountdownCoroutine());
            }

            private void CreateUI(Color textColor, string icon)
            {
                var rect = gameObject.AddComponent<RectTransform>();
                rect.sizeDelta = new Vector2(250, 20);

                // 배경 이미지
                backgroundImage = gameObject.AddComponent<Image>();
                backgroundImage.color = new Color(0f, 0f, 0f, 0.7f);
                backgroundImage.raycastTarget = false;

                // 아이콘
                if (!string.IsNullOrEmpty(icon))
                {
                    GameObject iconObj = new GameObject("Icon");
                    iconObj.transform.SetParent(transform, false);
                    var iconRect = iconObj.AddComponent<RectTransform>();
                    iconRect.sizeDelta = new Vector2(18, 18);
                    iconRect.anchorMin = new Vector2(0, 0.5f);
                    iconRect.anchorMax = new Vector2(0, 0.5f);
                    iconRect.pivot = new Vector2(0, 0.5f);
                    iconRect.anchoredPosition = new Vector2(5, 0);
                    
                    iconImage = iconObj.AddComponent<Image>();
                    iconImage.color = textColor;
                    iconImage.raycastTarget = false;
                }

                // 버프 이름 텍스트
                GameObject nameObj = new GameObject("BuffName");
                nameObj.transform.SetParent(transform, false);
                var nameRect = nameObj.AddComponent<RectTransform>();
                nameRect.sizeDelta = new Vector2(140, 20);
                nameRect.anchorMin = new Vector2(0, 0.5f);
                nameRect.anchorMax = new Vector2(0, 0.5f);
                nameRect.pivot = new Vector2(0, 0.5f);
                nameRect.anchoredPosition = new Vector2(string.IsNullOrEmpty(icon) ? 5 : 28, 0);
                
                nameText = nameObj.AddComponent<Text>();
                nameText.text = buffName;
                nameText.fontSize = 14;
                nameText.color = textColor;
                nameText.alignment = TextAnchor.MiddleLeft;
                nameText.raycastTarget = false;
                nameText.fontStyle = FontStyle.Bold;
                // 스킬트리 UI와 동일한 폰트 적용
                nameText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

                // 시간 텍스트
                GameObject timeObj = new GameObject("BuffTime");
                timeObj.transform.SetParent(transform, false);
                var timeRect = timeObj.AddComponent<RectTransform>();
                timeRect.sizeDelta = new Vector2(80, 20);
                timeRect.anchorMin = new Vector2(1, 0.5f);
                timeRect.anchorMax = new Vector2(1, 0.5f);
                timeRect.pivot = new Vector2(1, 0.5f);
                timeRect.anchoredPosition = new Vector2(-5, 0);
                
                timeText = timeObj.AddComponent<Text>();
                timeText.fontSize = 12;
                timeText.color = Color.white;
                timeText.alignment = TextAnchor.MiddleRight;
                timeText.raycastTarget = false;
                timeText.fontStyle = FontStyle.Bold;
                // 스킬트리 UI와 동일한 폰트 적용
                timeText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }

            private IEnumerator CountdownCoroutine()
            {
                while (remainingTime > 0)
                {
                    // ✅ 플레이어 사망 체크 추가
                    if (Player.m_localPlayer == null || Player.m_localPlayer.IsDead())
                    {
                        // 플레이어 사망으로 버프 코루틴 중단
                        SkillBuffDisplay.Instance.RemoveBuff(buffId);
                        yield break;
                    }

                    UpdateTimeDisplay();
                    yield return new WaitForSeconds(0.1f);
                    remainingTime -= 0.1f;
                }

                // 버프 만료
                SkillBuffDisplay.Instance.RemoveBuff(buffId);
            }

            private void UpdateTimeDisplay()
            {
                if (remainingTime > 60)
                {
                    int minutes = Mathf.FloorToInt(remainingTime / 60);
                    int seconds = Mathf.FloorToInt(remainingTime % 60);
                    timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
                }
                else if (remainingTime > 10)
                {
                    timeText.text = string.Format("{0:F0}s", remainingTime);
                }
                else
                {
                    timeText.text = string.Format("{0:F1}s", remainingTime);
                    // 10초 이하일 때 색상 변경
                    if (remainingTime <= 5)
                    {
                        timeText.color = Color.red;
                        nameText.color = Color.red;
                    }
                    else if (remainingTime <= 10)
                    {
                        timeText.color = Color.yellow;
                        nameText.color = Color.yellow;
                    }
                }
            }

            public void UpdateDuration(float newDuration)
            {
                remainingTime = newDuration;
            }
        }
    }
}
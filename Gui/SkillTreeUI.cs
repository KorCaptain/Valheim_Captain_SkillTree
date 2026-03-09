using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CaptainSkillTree.Gui;
using CaptainSkillTree.Audio;
using CaptainSkillTree.MMO_System;
using Jotunn.Managers;
using L10n = CaptainSkillTree.Localization.LocalizationManager;

namespace CaptainSkillTree.Gui
{
    /// <summary>
    /// 스킬 투자 조건 검사 결과
    /// </summary>
    public class InvestResult
    {
        public bool canInvest;
        public string message;
        
        public InvestResult(bool canInvest, string message = "")
        {
            this.canInvest = canInvest;
            this.message = message;
        }
    }

    public class SkillTreeUI : MonoBehaviour
    {
        public GameObject? panel;
        private UnityEngine.UI.Text? skillPointText;
        private Button? resetPointButton;
        private Button? resetJobButton;
        private Button? resetProductionButton;
        private Button? musicToggleButton;
        private Dictionary<string, GameObject> nodeObjects = new Dictionary<string, GameObject>();
        private Dictionary<(string from, string to), Image> connectionLines = new Dictionary<(string, string), Image>();
        private GameObject? dynamicTooltipObj = null;
        private GameObject? warningObj;
        private Text? warningText;
        private GameObject? confirmDialog; // 확인/취소 대화상자
        // UI에 추가될 새 요소들
        private Button? confirmButton;
        private Button? cancelButton;
        // 1. SkillTreeNodeUI, SkillTreeTooltipUI 컴포넌트 참조 추가
        private SkillTreeNodeUI nodeUI;
        private SkillTreeTooltipUI tooltipUI;

        // GUIManager.BlockInput 상태 추적 (Jotunn)
        private bool _lastPanelActiveState = false;

        /// <summary>
        /// MonoBehaviour OnEnable - 언어 변경 이벤트 구독
        /// </summary>
        private void OnEnable()
        {
            // 언어 변경 이벤트 구독
            L10n.OnLanguageChanged += RefreshAllText;
        }

        /// <summary>
        /// MonoBehaviour OnDisable - 언어 변경 이벤트 구독 해제
        /// </summary>
        private void OnDisable()
        {
            // 언어 변경 이벤트 구독 해제
            L10n.OnLanguageChanged -= RefreshAllText;
        }

        /// <summary>
        /// 모든 UI 텍스트 갱신 (언어 변경 시)
        /// </summary>
        private void RefreshAllText()
        {
            try
            {
                Plugin.Log.LogInfo("[SkillTreeUI] 언어 변경 감지 - UI 텍스트 갱신 중...");

                // 1. 스킬 포인트 텍스트
                UpdateSkillPointText();

                // 2. 버튼 텍스트 갱신
                if (resetPointButton != null)
                {
                    var btnText = resetPointButton.GetComponentInChildren<UnityEngine.UI.Text>();
                    if (btnText != null)
                    {
                        btnText.text = L10n.Get("ui_reset_points");
                    }
                }

                // 3. 직업 초기화 버튼 텍스트 갱신
                if (resetJobButton != null)
                {
                    var btnText = resetJobButton.GetComponentInChildren<UnityEngine.UI.Text>();
                    if (btnText != null)
                        btnText.text = L10n.Get("ui_reset_job");
                }

                // 4. 생산 초기화 버튼 텍스트 갱신
                if (resetProductionButton != null)
                {
                    var btnText = resetProductionButton.GetComponentInChildren<UnityEngine.UI.Text>();
                    if (btnText != null)
                        btnText.text = L10n.Get("ui_reset_production");
                }

                // 5. Music 버튼 텍스트 갱신
                if (musicToggleButton != null)
                {
                    var musicText = musicToggleButton.GetComponentInChildren<UnityEngine.UI.Text>();
                    if (musicText != null)
                    {
                        bool isBGMEnabled = SkillTreeBGMManager.Instance != null && SkillTreeBGMManager.Instance.IsBGMEnabled;
                        musicText.text = L10n.Get(isBGMEnabled ? "ui_music_on" : "ui_music_off");
                    }
                }

                // 5. 확인 다이얼로그가 열려있으면 텍스트 갱신
                if (confirmDialog != null && confirmDialog.activeSelf)
                {
                    var titleText = confirmDialog.transform.Find("TitleText")?.GetComponent<UnityEngine.UI.Text>();
                    if (titleText != null)
                    {
                        titleText.text = L10n.Get("ui_reset_confirm_title");
                    }

                    var contentText = confirmDialog.transform.Find("ContentText")?.GetComponent<UnityEngine.UI.Text>();
                    if (contentText != null)
                    {
                        contentText.text = L10n.Get("ui_reset_confirm_message");
                    }
                }

                // 6. 툴팁 갱신 (현재 열려있으면)
                if (tooltipUI != null)
                {
                    tooltipUI.RefreshTooltip();
                }

                Plugin.Log.LogInfo("[SkillTreeUI] ✓ UI 텍스트 갱신 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeUI] UI 텍스트 갱신 실패: {ex.Message}");
            }
        }

        private static readonly HashSet<string> JobIconNames = new HashSet<string> { "Berserker", "Tanker", "Rogue", "Archer", "Mage", "mage", "Paladin", "paladin", "Paladin" };
        private bool IsJobIcon(CaptainSkillTree.SkillTree.SkillNode node)
        {
            string iconName = node.IconName;
            
            // _unlock, _lock 접미사 제거
            if (iconName.EndsWith("_unlock") || iconName.EndsWith("_lock"))
            {
                iconName = iconName.Substring(0, iconName.LastIndexOf('_'));
            }
            
            return JobIconNames.Contains(iconName);
        }

        public void CreateUI(Canvas parentCanvas)
        {
            // EventSystem 확인 및 생성
            if (UnityEngine.EventSystems.EventSystem.current == null)
            {
                Plugin.Log.LogWarning("[UI] EventSystem이 없음 - 새로 생성");
                var eventSystemObj = new GameObject("EventSystem");
                eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }
            else
            {
                // EventSystem 확인됨
            }

            // GraphicRaycaster 확인
            var raycaster = parentCanvas.GetComponent<UnityEngine.UI.GraphicRaycaster>();
            if (raycaster == null)
            {
                Plugin.Log.LogWarning("[UI] GraphicRaycaster가 없음 - 추가");
                raycaster = parentCanvas.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }
            else
            {
                // GraphicRaycaster 확인됨
            }

            // Canvas 설정 확인 및 수정
            // Canvas renderMode 및 sortingOrder 확인
            
            // Canvas가 Screen Space - Overlay가 아니면 마우스 이벤트가 제대로 작동하지 않을 수 있음
            if (parentCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                Plugin.Log.LogWarning("[UI] Canvas가 ScreenSpaceOverlay가 아님 - 마우스 이벤트 문제 가능성");
            }
            
            // Canvas 렌더링 설정 (백업 폴더와 동일하게 설정하지 않음)
            
            // 1. SkillTreePanel 생성 (CanvasScaler는 최상위 Canvas에만)
            panel = new GameObject("SkillTreePanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            panel.transform.SetParent(parentCanvas.transform, false);
            panel.SetActive(false); // 항상 비활성화로 시작
            var panelRect = panel.GetComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(2048, 1152);

            // 스킬트리 UI는 항상 화면 중앙에 배치
            panelRect.anchoredPosition = Vector2.zero;
            Plugin.Log.LogInfo("[SkillTreeUI] UI 위치: 화면 중앙");
            
            // 패널 자체에 투명한 배경 설정하여 인벤토리 클릭 차단
            var panelImage = panel.GetComponent<Image>();
            panelImage.color = new Color(0, 0, 0, 0.01f); // 거의 투명하지만 클릭 가능
            panelImage.raycastTarget = true; // 인벤토리 클릭 차단용
            // 2. 하위에 배경 Image 오브젝트 생성 및 SkillTreeBG 적용
            GameObject bgObj = new GameObject("Image", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            bgObj.transform.SetParent(panel.transform, false);
            var bgRect = bgObj.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var bgImage = bgObj.GetComponent<Image>();
            var uiBundle = Plugin.GetUIAssetBundle();
            Sprite bgSprite = null;
            if (uiBundle != null)
                bgSprite = uiBundle.LoadAsset<Sprite>("SkillTreeBG");
            if (bgSprite != null)
            {
                bgImage.sprite = bgSprite;
                bgImage.color = Color.white;
                bgImage.type = Image.Type.Sliced;
            }
            else
            {
                bgImage.color = new Color(0,0,0,0.7f);
            }
            // 🚨 중요: 배경 이미지가 마우스 이벤트를 차단하지 않도록 설정
            bgImage.raycastTarget = false;

            // 2. 단검 노드(아이콘 방식, 프리팹 NO, 코드 직접 생성)
            // (기존 단검/근접/무기 노드 수동 생성 코드 전체 삭제)
            // ...
            // 중앙 상단 UI: [스킬트리 사용가능 포인트 00] 텍스트 복원
            GameObject txtObj = new GameObject("SkillPointText", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Text));
            txtObj.transform.SetParent(panel.transform, false);
            var skillPointUnityText = txtObj.GetComponent<UnityEngine.UI.Text>();
            if (skillPointUnityText != null) {
                skillPointText = skillPointUnityText; // TextMeshProUGUI 필드에 할당
                UpdateSkillPointText();
                skillPointUnityText.fontSize = 18;
                skillPointUnityText.color = new Color(1f, 0.2f, 0.7f, 1f); // 진한 분홍색, 완전 불투명
                skillPointUnityText.alignment = TextAnchor.MiddleCenter;
                skillPointUnityText.fontStyle = FontStyle.Bold;
                // Arial 폰트 할당
                skillPointUnityText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                var txtRect = txtObj.GetComponent<RectTransform>();
                txtRect.sizeDelta = new Vector2(300, 40);
                txtRect.anchorMin = new Vector2(0.5f, 1f);
                txtRect.anchorMax = new Vector2(0.5f, 1f);
                txtRect.pivot = new Vector2(0.5f, 1f);
                txtRect.anchoredPosition = new Vector2(0, -40); // 중앙 상단
            } else {
                Debug.LogError("[SkillTreeUI] UnityEngine.UI.Text 컴포넌트 생성 실패");
            }


            // 행 1: [포인트 초기화] [직업 초기화] [생산 초기화] (y=-40)
            // 행 2: [Music Off] (y=-76)

            // 행 1 버튼 공통 크기: 130x38, 폰트 13, 간격 12px
            // x 중심 위치: 120 / 262 / 404

            // [포인트 초기화] 버튼 생성 (행 1 왼쪽) - 스틸블루
            resetPointButton = CreateFancyButton("ResetPointButton", L10n.Get("ui_reset_points"),
                new Vector2(120, -40), new Color(0.22f, 0.45f, 0.70f, 1f), panel.transform,
                () => ResetSkillPoints());

            // [직업 초기화] 버튼 생성 (행 1 중간) - 보라
            resetJobButton = CreateFancyButton("ResetJobButton", L10n.Get("ui_reset_job"),
                new Vector2(262, -40), new Color(0.50f, 0.20f, 0.60f, 1f), panel.transform,
                () => ResetJobSkillPoints());

            // [생산 초기화] 버튼 생성 (행 1 오른쪽) - 에메랄드
            resetProductionButton = CreateFancyButton("ResetProductionButton", L10n.Get("ui_reset_production"),
                new Vector2(404, -40), new Color(0.15f, 0.55f, 0.25f, 1f), panel.transform,
                () => ResetProductionSkillPoints());

            // Music On/Off 토글 버튼 생성 (행 2)
            CreateMusicToggleButton(panel);

            // 하단 중앙 UI: 사용 가능 포인트, 확인/취소 버튼 (기존 텍스트 바로 아래로 이동)
            CreateConfirmationControls(panel);


            // [줌인/줌아웃] 기능: SkillTreeZoomCntrl 컴포넌트 동적 추가
            if (panel.GetComponent<SkillTreeZoomCntrl>() == null)
                panel.AddComponent<SkillTreeZoomCntrl>();
            // SkillTreeNodeUI, SkillTreeTooltipUI 컴포넌트 동적 추가
            nodeUI = panel.AddComponent<SkillTreeNodeUI>();
            tooltipUI = panel.AddComponent<SkillTreeTooltipUI>();
            // 노드 및 연결선 생성
            nodeUI.GenerateSkillTreeNodesAndLines(panel,
                (node, rect) => {
                    // 노드 클릭 처리

                    // CanInvestWithMessage 사용으로 통일하여 정확한 조건 체크와 메시지 제공
                    var investResult = CanInvestWithMessage(node);
                    if (investResult.canInvest) {
                        InvestPoint(node);
                        if (gameObject.activeInHierarchy) {
                            StartCoroutine(nodeUI.NodeInvestAnimation(rect));
                        }

                        // 직업 아이콘 클릭 후 배경 뒤로 사라지는 문제 방지
                        bool isJobIcon = nodeUI.IsJobIconName(node.IconName ?? node.Id);
                        bool isJobIconOrForced = isJobIcon || node.Id == "Mage" || node.Id == "Paladin";
                        if (isJobIconOrForced) {
                            // 클릭된 직업 아이콘을 다시 최상위로 설정
                            if (nodeUI.nodeObjects.TryGetValue(node.Id, out var nodeObj)) {
                                nodeObj.transform.SetAsLastSibling();
                                // 직업 아이콘 클릭 후 최상위로 재설정
                            }
                        }
                    } else {
                        // 조건을 만족하지 않을 때만 구체적인 메시지 표시 (빈 메시지는 표시하지 않음)
                        if (!string.IsNullOrEmpty(investResult.message))
                        {
                            tooltipUI.ShowWarning(investResult.message);
                        }
                    }
                },
                (node, pos) => { tooltipUI.ShowTooltip(node, pos); },
                () => { tooltipUI.HideTooltip(); }
            );
        }

        private void CreateConfirmationControls(GameObject parent)
        {
            GameObject container = new GameObject("ConfirmationContainer", typeof(RectTransform), typeof(CanvasGroup));
            container.transform.SetParent(parent.transform, false);
            var containerRect = container.GetComponent<RectTransform>();

            // skillPointText 바로 아래에 위치 (y -20)
            float baseX = 0f;
            float baseY = -60f; // 기본값
            if (skillPointText != null)
            {
                var skillPointRect = skillPointText.GetComponent<RectTransform>();
                baseX = skillPointRect.anchoredPosition.x;
                baseY = skillPointRect.anchoredPosition.y - 20f;
            }
            containerRect.anchorMin = new Vector2(0.5f, 1f);
            containerRect.anchorMax = new Vector2(0.5f, 1f);
            containerRect.pivot = new Vector2(0.5f, 1f);
            containerRect.anchoredPosition = new Vector2(0, -60); // -30에서 -60으로 변경 (추가 -30)
            containerRect.sizeDelta = new Vector2(300, 60);

            // 1. 사용 가능 포인트 텍스트 (제거 - 기존 텍스트를 사용)

            // 2. 확인 버튼 (50% 크기 = 30% + 20% 증가)
            confirmButton = CreateStyledButton("ConfirmButton", L10n.Get("ui_confirm"), new Vector2(-62.5f, 0), new Color(0.0f, 0.0f, 0.545f), container.transform, () => {
                // 투자 확정 시 이펙트와 효과음
                PlaySkillInvestmentEffects();
                
                SkillTree.SkillTreeManager.Instance.ConfirmInvestments();
                
                nodeUI.RefreshNodeStates();
                nodeUI.UpdateConnectionLines();
                RefreshUI();
            });

            // 3. 취소 버튼 (50% 크기 = 30% + 20% 증가)
            cancelButton = CreateStyledButton("CancelButton", L10n.Get("ui_cancel"), new Vector2(62.5f, 0), new Color(0.545f, 0.0f, 0.0f), container.transform, () => {
                // 취소 시 취소음
                PlayCancelSound();
                
                SkillTree.SkillTreeManager.Instance.CancelInvestments();
                
                nodeUI.RefreshNodeStates();
                nodeUI.UpdateConnectionLines();
                RefreshUI();
            });

            container.transform.SetAsLastSibling();
        }

        private Button CreateStyledButton(string name, string text, Vector2 position, Color color, Transform parent, UnityEngine.Events.UnityAction onClick, float scale = 1.0f, Vector2? buttonSize = null)
        {
            GameObject btnGo = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            btnGo.transform.SetParent(parent, false);
            var btnRect = btnGo.GetComponent<RectTransform>();
            btnRect.sizeDelta = new Vector2(75, 25); // 전체 버튼 크기 120x40 픽셀 고정
            btnRect.anchoredPosition = position;

            var btnImage = btnGo.GetComponent<Image>();
            btnImage.color = Color.black; // 배경은 검정
            btnImage.raycastTarget = true; // 클릭 가능하도록 설정

            // 버튼 내부 색상 영역 (컬러 영역이 검정보다 10% 작게)
            GameObject colorArea = new GameObject("ColorArea", typeof(RectTransform), typeof(Image));
            colorArea.transform.SetParent(btnGo.transform, false);
            var colorRect = colorArea.GetComponent<RectTransform>();
            float colorAreaScale = 0.9f; // 컬러 영역이 검정 배경보다 10% 작게
            colorRect.sizeDelta = new Vector2(
                75 * colorAreaScale,
                25 * colorAreaScale
            );
            colorRect.anchoredPosition = Vector2.zero;
            var colorImage = colorArea.GetComponent<Image>();
            colorImage.color = color;
            colorImage.raycastTarget = false; // 내부 색상 영역은 클릭 차단하지 않음

            var button = btnGo.GetComponent<Button>();
            
            // 클릭 효과 추가
            button.onClick.AddListener(() => {
                StartCoroutine(ButtonClickEffect(btnRect));
                onClick.Invoke();
            });

            GameObject textGo = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Text));
            textGo.transform.SetParent(colorArea.transform, false);
            var textComponent = textGo.GetComponent<UnityEngine.UI.Text>();
            textComponent.text = text;
            textComponent.fontSize = 15; // 글씨 크기 유지
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // 포인트 초기화 버튼과 동일한 폰트
            textComponent.raycastTarget = false; // 텍스트는 클릭 차단하지 않음
            var textRect = textGo.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            
            return button;
        }

        /// <summary>
        /// 4-레이어 입체감 버튼 생성 (Shadow + MainBody + Highlight + Text+Shadow)
        /// </summary>
        private Button CreateFancyButton(string name, string text, Vector2 position,
            Color mainColor, Transform parent, UnityEngine.Events.UnityAction onClick)
        {
            // 루트 컨테이너 (Button 컴포넌트, 투명)
            GameObject root = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            root.transform.SetParent(parent, false);
            var rootRect = root.GetComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(134, 42);
            rootRect.anchorMin = new Vector2(0.5f, 1f);
            rootRect.anchorMax = new Vector2(0.5f, 1f);
            rootRect.pivot = new Vector2(0.5f, 1f);
            rootRect.anchoredPosition = position;
            root.GetComponent<Image>().color = Color.clear;

            // 1. 드롭 섀도우 레이어 (먼저 생성 = 뒤에 렌더링)
            GameObject shadowGo = new GameObject("Shadow", typeof(RectTransform), typeof(Image));
            shadowGo.transform.SetParent(root.transform, false);
            var shadowRect = shadowGo.GetComponent<RectTransform>();
            shadowRect.anchorMin = new Vector2(0.5f, 0.5f);
            shadowRect.anchorMax = new Vector2(0.5f, 0.5f);
            shadowRect.pivot = new Vector2(0.5f, 0.5f);
            shadowRect.sizeDelta = new Vector2(130, 38);
            shadowRect.anchoredPosition = new Vector2(2f, -2f);
            var shadowImg = shadowGo.GetComponent<Image>();
            shadowImg.color = new Color(0, 0, 0, 0.6f);
            shadowImg.raycastTarget = false;

            // 2. 메인 바디 레이어
            GameObject mainGo = new GameObject("MainBody", typeof(RectTransform), typeof(Image));
            mainGo.transform.SetParent(root.transform, false);
            var mainRect = mainGo.GetComponent<RectTransform>();
            mainRect.anchorMin = new Vector2(0.5f, 0.5f);
            mainRect.anchorMax = new Vector2(0.5f, 0.5f);
            mainRect.pivot = new Vector2(0.5f, 0.5f);
            mainRect.sizeDelta = new Vector2(130, 38);
            mainRect.anchoredPosition = Vector2.zero;
            var mainImg = mainGo.GetComponent<Image>();
            mainImg.color = mainColor;
            mainImg.raycastTarget = false;

            // 3. 상단 하이라이트 레이어 (mainGo 자식)
            GameObject hlGo = new GameObject("Highlight", typeof(RectTransform), typeof(Image));
            hlGo.transform.SetParent(mainGo.transform, false);
            var hlRect = hlGo.GetComponent<RectTransform>();
            hlRect.anchorMin = new Vector2(0, 0.6f);
            hlRect.anchorMax = new Vector2(1, 1);
            hlRect.offsetMin = Vector2.zero;
            hlRect.offsetMax = Vector2.zero;
            var hlImg = hlGo.GetComponent<Image>();
            hlImg.color = new Color(
                Mathf.Min(mainColor.r + 0.25f, 1f),
                Mathf.Min(mainColor.g + 0.25f, 1f),
                Mathf.Min(mainColor.b + 0.25f, 1f),
                0.5f
            );
            hlImg.raycastTarget = false;

            // 4. 텍스트 + Shadow 컴포넌트 (mainGo 자식)
            GameObject textGo = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Text));
            textGo.transform.SetParent(mainGo.transform, false);
            var textComp = textGo.GetComponent<UnityEngine.UI.Text>();
            textComp.text = text;
            textComp.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComp.alignment = TextAnchor.MiddleCenter;
            textComp.color = Color.white;
            textComp.fontSize = 13;
            textComp.raycastTarget = false;
            var textRect = textGo.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            var textShadow = textGo.AddComponent<UnityEngine.UI.Shadow>();
            textShadow.effectColor = new Color(0, 0, 0, 0.8f);
            textShadow.effectDistance = new Vector2(1f, -1f);

            // 5. Button ColorBlock (Hover/Press 피드백)
            var button = root.GetComponent<Button>();
            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1.2f, 1.2f, 1.2f, 1f);
            colors.pressedColor = new Color(0.75f, 0.75f, 0.75f, 1f);
            colors.fadeDuration = 0.1f;
            button.colors = colors;

            button.onClick.AddListener(() => {
                StartCoroutine(ButtonClickEffect(rootRect));
                onClick.Invoke();
            });

            return button;
        }

        // 버튼 클릭 효과
        private System.Collections.IEnumerator ButtonClickEffect(RectTransform buttonRect)
        {
            Vector3 originalScale = buttonRect.localScale;
            // 축소
            buttonRect.localScale = originalScale * 0.9f;
            yield return new WaitForSeconds(0.1f);
            // 복원
            buttonRect.localScale = originalScale;
        }

        private void UpdateAvailablePointText()
        {
            // 기존 skillPointText를 사용하므로 이 함수는 더 이상 필요 없음
        }

        // 포인트 텍스트 갱신 함수 복원
        private void UpdateSkillPointText()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            int maxPoints = manager.GetTotalMaxPoints();
            int usedPoints = manager.GetTotalUsedPoints();
            int pendingPoints = 0;
            foreach (var pending in manager.pendingInvestments)
            {
                var node = manager.SkillNodes[pending.Key];
                pendingPoints += pending.Value * node.RequiredPoints;
            }
            int availablePoints = maxPoints - usedPoints - pendingPoints;

            if (skillPointText != null)
            {
                skillPointText.supportRichText = true;

                // 스킬포인트 기반 레벨 표시 모드 확인
                var levelInfo = CaptainMMOBridge.GetLevelInfo();

                if (levelInfo.isSkillPointBased)
                {
                    // 스킬포인트 기반 레벨 모드: Lv.34 스킬포인트 05 / 150
                    // (사용: 102, 2pt/Lv) - 줄바꿈 후 흰색 표시
                    skillPointText.text = $"<color=#00BFFF>Lv.{levelInfo.level}</color> {L10n.Get("skill_points")} <color=#FF0000>{availablePoints:00}</color><color=#FFFFFF> / </color><color=#000000>{maxPoints}</color>\n<color=#FFFFFF>({L10n.Get("ui_skill_used")}: {levelInfo.usedPoints}, {levelInfo.pointsPerLevel}{L10n.Get("ui_pt_per_level")})</color>";
                }
                else
                {
                    // 기존 모드: 스킬트리 사용가능 포인트 05 / 150
                    int currentLevel = CaptainMMOBridge.GetLevel();
                    skillPointText.text = $"<color=#00BFFF>Lv.{currentLevel}</color> {L10n.Get("skill_points")} <color=#FF0000>{availablePoints:00}</color><color=#FFFFFF> / </color><color=#000000>{maxPoints:00}</color>";
                }
            }
        }

        private Button CreateNode(Sprite icon, Vector2 anchoredPos, UnityEngine.Events.UnityAction onClick = null)
        {
            var go = new GameObject("Node", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            var rect = go.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(36, 36);
            rect.anchoredPosition = anchoredPos;
            var img = go.GetComponent<Image>();
            img.sprite = icon;
            img.color = Color.white;
            var btn = go.GetComponent<Button>();
            if (onClick != null) btn.onClick.AddListener(onClick);
            return btn;
        }

        private Image CreateLine(RectTransform parent, RectTransform from, RectTransform to)
        {
            var go = new GameObject("Line", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            go.transform.SetParent(parent, false);
            var img = go.GetComponent<Image>();
            img.color = new Color(0.7f, 0.7f, 0.7f, 0.8f); // 기본은 흐린 회색
            img.raycastTarget = false;
            var rect = go.GetComponent<RectTransform>();
            Vector2 start = from.anchoredPosition;
            Vector2 end = to.anchoredPosition;
            Vector2 dir = (end - start).normalized;
            float dist = Vector2.Distance(start, end);
            rect.sizeDelta = new Vector2(dist, 4);
            rect.anchoredPosition = (start + end) / 2f;
            float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;
            rect.localRotation = Quaternion.Euler(0,0,angle);
            
            
            return img;
        }

        private void ShowKnifeNodePopup()
        {
            // 간단한 설명 팝업(임시)
            Debug.Log("[스킬트리] 단검: 1포인트 투자 시 단검 공격력 +2");
            // 실제로는 UI 팝업/포인트 투자 로직 추가 예정
        }

        public void RefreshUI()
        {
            nodeUI.RefreshNodeStates();
            nodeUI.UpdateConnectionLines();
            UpdateSkillPointText();
            
            // RefreshUI 호출 시마다 직업 아이콘을 최상위로 유지 (클릭 후 배경 뒤로 사라지는 문제 방지)
            // nodeUI.RefreshNodeStates() 내부에서 EnsureJobIconsOnTop()이 호출되므로 추가 호출 불필요
        }
        

        // 연결선 색상 업데이트 (개선된 버전 - 생성 시 매핑된 연결선 사용)
        private void UpdateConnectionLines()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            
            // 모든 연결선을 기본 흐린 회색으로 설정
            foreach (var line in connectionLines.Values)
            {
                line.color = new Color(0.7f, 0.7f, 0.7f, 0.8f); // 기본 흐린 회색
            }
            
            // 투자된 노드들의 연결선만 색상 변경
            foreach (var kvp in connectionLines)
            {
                var (fromNodeId, toNodeId) = kvp.Key;
                var line = kvp.Value;
                
                // 시작 노드가 투자되었고, 도착 노드도 투자된 경우에만 연결선 색상 변경
                int fromLevel = manager.GetSkillLevel(fromNodeId);
                int fromPendingLevel = manager.pendingInvestments.ContainsKey(fromNodeId) ? manager.pendingInvestments[fromNodeId] : 0;
                bool fromInvested = fromLevel > 0 || fromPendingLevel > 0;
                
                int toLevel = manager.GetSkillLevel(toNodeId);
                int toPendingLevel = manager.pendingInvestments.ContainsKey(toNodeId) ? manager.pendingInvestments[toNodeId] : 0;
                bool toInvested = toLevel > 0 || toPendingLevel > 0;
                
                if (fromInvested && toInvested)
                {
                    line.color = Color.white; // 원래 회색 (투자 후)
                }
            }
        }

        /// <summary>
        /// 이벤트 기반 UI 입력 처리 - 키 입력 시에만 실행
        /// </summary>
        void Update()
        {
            bool currentPanelActive = panel != null && panel.activeInHierarchy;

            // ESC/Tab 키로 패널 닫기 (BlockInput 상태와 무관하게 항상 처리)
            if (currentPanelActive)
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
                {
                    // 툴팁이 열려있으면 툴팁만 닫기
                    bool tooltipActive = (dynamicTooltipObj != null && dynamicTooltipObj.activeInHierarchy) ||
                                         (tooltipUI != null && tooltipUI.IsTooltipVisible());

                    if (tooltipActive)
                    {
                        if (tooltipUI != null)
                            tooltipUI.HideTooltip();
                        else
                            HideTooltip();
                    }
                    else
                    {
                        // 패널 닫기
                        panel.SetActive(false);

                        // BGM 일시정지
                        if (SkillTreeBGMManager.Instance != null)
                        {
                            SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
                        }

                        // BlockInput 해제
                        try
                        {
                            if (GUIManager.Instance != null)
                            {
                                GUIManager.BlockInput(false);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Plugin.Log.LogWarning($"[SkillTreeUI] GUIManager.BlockInput 해제 실패: {ex.Message}");
                        }

                        _lastPanelActiveState = false;
                        return;
                    }
                }
            }

            // Jotunn GUIManager.BlockInput 상태 관리
            // 패널 활성화 상태가 변경되면 입력 차단/해제
            if (currentPanelActive != _lastPanelActiveState)
            {
                _lastPanelActiveState = currentPanelActive;
                try
                {
                    // GUIManager가 초기화되어 있는지 확인
                    if (GUIManager.Instance != null)
                    {
                        GUIManager.BlockInput(currentPanelActive);
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogWarning($"[SkillTreeUI] GUIManager.BlockInput 호출 실패: {ex.Message}");
                }
            }
        }

        // 임시: 포인트 초기화 함수
        private void ResetSkillPoints()
        {
            // 확인 다이얼로그 표시
            ShowResetConfirmDialog("ui_reset_confirm_title", "ui_reset_confirm_message", ExecuteResetSkillPoints);
        }
        
        /// <summary>
        /// 실제 스킬포인트 초기화 실행
        /// </summary>
        private void ExecuteResetSkillPoints()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            // 1. 생산 전문가 제외 모든 스킬트리 투자/레벨 0으로 초기화
            manager.ResetAllSkillLevelsExceptProduction();
            
            // 2. UI 갱신
            RefreshUI(); // RefreshUI가 내부적으로 UpdateSkillPointText를 호출하므로 중복 호출 필요 없음
            Debug.Log($"[SkillTreeUI] 스킬포인트가 초기화되었습니다.");
        }
        
        private void ResetJobSkillPoints()
        {
            ShowResetConfirmDialog(
                "ui_reset_job_confirm_title",
                "ui_reset_job_confirm_message",
                ExecuteResetJobSkillPoints);
        }

        private void ExecuteResetJobSkillPoints()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            manager.ResetJobSkillLevels();
            RefreshUI();
            Debug.Log("[SkillTreeUI] 직업 스킬이 초기화되었습니다.");
        }

        private void ResetProductionSkillPoints()
        {
            ShowResetConfirmDialog(
                "ui_reset_production_confirm_title",
                "ui_reset_production_confirm_message",
                ExecuteResetProductionSkillPoints);
        }

        private void ExecuteResetProductionSkillPoints()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            manager.ResetProductionSkillLevels();
            RefreshUI();
            Debug.Log("[SkillTreeUI] 생산 전문가 스킬이 초기화되었습니다.");
        }

        /// <summary>
        /// 스킬 초기화 확인 다이얼로그 표시
        /// </summary>
        private void ShowResetConfirmDialog(string titleKey, string messageKey, System.Action onConfirm)
        {
            // 기존 다이얼로그가 있으면 제거
            if (confirmDialog != null)
            {
                DestroyImmediate(confirmDialog);
            }
            
            // 다이얼로그 배경 패널 생성
            confirmDialog = new GameObject("ResetConfirmDialog");
            confirmDialog.transform.SetParent(panel.transform, false);
            
            // 전체 화면 배경 (반투명 검은색)
            var bgImage = confirmDialog.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.7f);
            
            var bgRect = confirmDialog.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            bgRect.anchoredPosition = Vector2.zero;
            
            // 다이얼로그 내용 패널
            var dialogPanel = new GameObject("DialogPanel");
            dialogPanel.transform.SetParent(confirmDialog.transform, false);
            
            var dialogImage = dialogPanel.AddComponent<Image>();
            dialogImage.color = new Color(0.2f, 0.2f, 0.3f, 0.95f);
            
            var dialogRect = dialogPanel.GetComponent<RectTransform>();
            dialogRect.sizeDelta = new Vector2(400, 200);
            dialogRect.anchoredPosition = Vector2.zero;
            
            // 제목 텍스트
            var titleObj = new GameObject("Title");
            titleObj.transform.SetParent(dialogPanel.transform, false);
            
            var titleText = titleObj.AddComponent<UnityEngine.UI.Text>();
            titleText.text = L10n.Get(titleKey);
            titleText.fontSize = 20;
            titleText.color = Color.white;
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            
            var titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.sizeDelta = new Vector2(360, 40);
            titleRect.anchoredPosition = new Vector2(0, 50);
            
            // 내용 텍스트
            var contentObj = new GameObject("Content");
            contentObj.transform.SetParent(dialogPanel.transform, false);
            
            var contentText = contentObj.AddComponent<UnityEngine.UI.Text>();
            contentText.text = L10n.Get(messageKey);
            contentText.fontSize = 14;
            contentText.color = Color.white;
            contentText.alignment = TextAnchor.MiddleCenter;
            contentText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            
            var contentRect = contentObj.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(360, 60);
            contentRect.anchoredPosition = new Vector2(0, 0);
            
            // 확인 버튼
            var confirmBtnObj = new GameObject("ConfirmButton");
            confirmBtnObj.transform.SetParent(dialogPanel.transform, false);
            
            var confirmBtnImage = confirmBtnObj.AddComponent<Image>();
            confirmBtnImage.color = new Color(0.8f, 0.2f, 0.2f, 1f); // 빨간색
            
            confirmButton = confirmBtnObj.AddComponent<Button>();
            confirmButton.onClick.AddListener(() => {
                PlayConfirmSound();
                HideResetConfirmDialog();
                onConfirm();
            });
            
            var confirmBtnRect = confirmBtnObj.GetComponent<RectTransform>();
            confirmBtnRect.sizeDelta = new Vector2(120, 40);
            confirmBtnRect.anchoredPosition = new Vector2(-70, -50);
            
            // 확인 버튼 텍스트
            var confirmTxtObj = new GameObject("ConfirmText");
            confirmTxtObj.transform.SetParent(confirmBtnObj.transform, false);
            
            var confirmTxt = confirmTxtObj.AddComponent<UnityEngine.UI.Text>();
            confirmTxt.text = L10n.Get("ui_confirm");
            confirmTxt.fontSize = 14;
            confirmTxt.color = Color.white;
            confirmTxt.alignment = TextAnchor.MiddleCenter;
            confirmTxt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            var confirmTxtRect = confirmTxtObj.GetComponent<RectTransform>();
            confirmTxtRect.sizeDelta = new Vector2(120, 40);
            confirmTxtRect.anchoredPosition = Vector2.zero;

            // 취소 버튼
            var cancelBtnObj = new GameObject("CancelButton");
            cancelBtnObj.transform.SetParent(dialogPanel.transform, false);

            var cancelBtnImage = cancelBtnObj.AddComponent<Image>();
            cancelBtnImage.color = new Color(0.4f, 0.4f, 0.4f, 1f); // 회색

            cancelButton = cancelBtnObj.AddComponent<Button>();
            cancelButton.onClick.AddListener(() => {
                PlayCancelSound();
                HideResetConfirmDialog();
            });

            var cancelBtnRect = cancelBtnObj.GetComponent<RectTransform>();
            cancelBtnRect.sizeDelta = new Vector2(120, 40);
            cancelBtnRect.anchoredPosition = new Vector2(70, -50);

            // 취소 버튼 텍스트
            var cancelTxtObj = new GameObject("CancelText");
            cancelTxtObj.transform.SetParent(cancelBtnObj.transform, false);

            var cancelTxt = cancelTxtObj.AddComponent<UnityEngine.UI.Text>();
            cancelTxt.text = L10n.Get("ui_cancel");
            cancelTxt.fontSize = 14;
            cancelTxt.color = Color.white;
            cancelTxt.alignment = TextAnchor.MiddleCenter;
            cancelTxt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            
            var cancelTxtRect = cancelTxtObj.GetComponent<RectTransform>();
            cancelTxtRect.sizeDelta = new Vector2(120, 40);
            cancelTxtRect.anchoredPosition = Vector2.zero;
            
            // 다이얼로그를 최상위로 설정
            confirmDialog.transform.SetAsLastSibling();

            // 스킬 초기화 확인 다이얼로그 표시
        }
        
        /// <summary>
        /// 확인 다이얼로그 숨기기
        /// </summary>
        private void HideResetConfirmDialog()
        {
            if (confirmDialog != null)
            {
                DestroyImmediate(confirmDialog);
                confirmDialog = null;
                // 스킬 초기화 확인 다이얼로그 숨김
            }
        }
        

        // SkillTree 노드와 연결선 자동 배치/생성 (방사형 구조)
        private void GenerateSkillTreeNodesAndLines(GameObject skillTreePanel)
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            nodeObjects.Clear();
            connectionLines.Clear(); // 연결선 매핑도 초기화
            var nodes = manager.SkillNodes;
            // 1. 노드 UI 오브젝트 생성 및 배치
            foreach (var node in nodes.Values)
            {
                var nodeObj = new GameObject(node.Id, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
                nodeObj.transform.SetParent(skillTreePanel.transform, false);
                var rect = nodeObj.GetComponent<RectTransform>();
                var img = nodeObj.GetComponent<Image>();
                var btn = nodeObj.GetComponent<Button>();

                rect.anchoredPosition = node.Position;
                bool isUnlocked = (manager.GetSkillLevel(node.Id) > 0);
                string iconToLoad = isUnlocked ? node.IconNameUnlocked : node.IconNameLocked;
                
                var sprite = TryLoadSprite(iconToLoad);
                if (sprite == null)
                {
                    // 기본 아이콘으로 대체
                    sprite = TryLoadSprite(isUnlocked ? "all_skill_unlock" : "all_skill_lock");
                    Debug.LogWarning($"[스킬트리] {iconToLoad} 로딩 실패, 기본 아이콘으로 대체");
                }
                img.sprite = sprite;
                // 락/언락 상태별 색상 처리
                if (isUnlocked)
                    img.color = Color.white;
                else
                    img.color = new Color(1,1,1,0.5f);
                // 직업 아이콘 크기 규칙 적용
                if (IsJobIcon(node) || node.Id == "Mage" || node.Id == "Paladin")
                {
                    // 모든 직업 아이콘을 버서커 크기로 통일 (메이지, Paladin 강제 포함)
                    rect.sizeDelta = isUnlocked ? new Vector2(105, 105) : new Vector2(85, 85);
                    
                    // 언락된 직업 아이콘은 노드선 위에 렌더링
                    if (isUnlocked)
                    {
                        nodeObj.transform.SetAsLastSibling();
                    }
                }
                
                // 버튼 이벤트 연결
                btn.onClick.AddListener(() =>
                {
                    var investResult = CanInvestWithMessage(node);
                    if (investResult.canInvest) {
                        InvestPoint(node);
                        if (gameObject.activeInHierarchy) {
                             StartCoroutine(NodeInvestAnimation(rect));
                        }
                    } else {
                        // 빈 메시지는 표시하지 않음 (이미 습득한 스킬에 대한 조용한 처리)
                        if (!string.IsNullOrEmpty(investResult.message))
                        {
                            ShowWarning(investResult.message);
                        }
                    }
                });
                
                // 이벤트 트리거 추가 (마우스 오버/아웃)
                var eventTrigger = nodeObj.AddComponent<UnityEngine.EventSystems.EventTrigger>();
                var pointerEnter = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter };
                pointerEnter.callback.AddListener((data) => { tooltipUI.ShowTooltip(node, rect.position); });
                eventTrigger.triggers.Add(pointerEnter);

                var pointerExit = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit };
                pointerExit.callback.AddListener((data) => { 
                    if (tooltipUI != null)
                        tooltipUI.HideTooltip();
                    else
                        HideTooltip();
                });
                eventTrigger.triggers.Add(pointerExit);

                nodeObjects[node.Id] = nodeObj;
            }

            // 2. 노드 간 연결선 생성 및 매핑 저장
            foreach (var node in nodes.Values)
            {
                if (node.Prerequisites != null && node.Prerequisites.Count > 0)
                {
                    if (IsJobIcon(node)) continue; // 현재 노드가 직업 아이콘이면 연결선 X
                    foreach (var preId in node.Prerequisites)
                    {
                        if (nodes.ContainsKey(preId) && nodeObjects.ContainsKey(node.Id) && nodeObjects.ContainsKey(preId))
                        {
                            if (IsJobIcon(nodes[preId])) continue; // 선행노드가 직업 아이콘이면 연결선 X
                            var line = CreateLine(skillTreePanel.GetComponent<RectTransform>(), nodeObjects[preId].GetComponent<RectTransform>(), nodeObjects[node.Id].GetComponent<RectTransform>());
                            
                            // 연결선 매핑 저장
                            connectionLines[(preId, node.Id)] = line;
                        }
                    }
                }
            }
            
            // 3. 노드 아이콘 렌더링 순서 설정
            foreach (var kvp in nodeObjects)
            {
                var nodeId = kvp.Key;
                var nodeObj = kvp.Value;
                var node = SkillTree.SkillTreeManager.Instance.SkillNodes[nodeId];
                
            }
            
            
            RefreshUI(); // 생성 후 최종 상태 업데이트
             
            // 경고창 생성 (가장 앞에)
            if (warningObj == null) CreateWarning(skillTreePanel.transform);
            if (warningObj != null) warningObj.transform.SetAsLastSibling();
        }
        
        private Sprite TryLoadSprite(string spriteName)
        {
            // 1. job_icon 번들에서 시도 (Job 아이콘들: Paladin, Tanker, Berserker, Archer, Mage, Rogue)
            var jobIconBundle = Plugin.GetJobIconBundle();
            if (jobIconBundle != null)
            {
                var sprite = jobIconBundle.LoadAsset<Sprite>(spriteName);
                if (sprite != null) 
                {
                    // Plugin.Log.LogInfo($"[아이콘] job_icon 번들에서 {spriteName} 로드 성공"); // 제거: 과도한 로그
                    return sprite;
                }
            }
            
            // 2. skill_node 번들에서 시도 (일반 스킬 아이콘들)
            var skillNodeBundle = Plugin.GetIconAssetBundle();
            if (skillNodeBundle != null)
            {
                var sprite = skillNodeBundle.LoadAsset<Sprite>(spriteName);
                if (sprite != null) 
                {
                    // Plugin.Log.LogInfo($"[아이콘] skill_node 번들에서 {spriteName} 로드 성공"); // 제거: 과도한 로그
                    return sprite;
                }
            }
            
            // 3. Resources에서 시도
            var resSprite = Resources.Load<Sprite>(spriteName);
            if (resSprite != null) 
            {
                // Plugin.Log.LogInfo($"[아이콘] Resources에서 {spriteName} 로드 성공"); // 제거: 과도한 로그
                return resSprite;
            }
            
            // 실패 시 null
            // 아이콘 로드 실패 - 기본 아이콘 사용
            return null;
        }

        /// <summary>
        /// 포인트 투자 조건 체크 (구체적인 메시지 포함)
        /// </summary>
        private InvestResult CanInvestWithMessage(CaptainSkillTree.SkillTree.SkillNode node)
        {
            var manager = SkillTree.SkillTreeManager.Instance;
            // 투자 조건 체크 시작
            
            // 0. 플레이어 레벨 조건 체크 (직업 아이콘 등)
            if (node.RequiredPlayerLevel > 0)
            {
                int currentPlayerLevel = CaptainMMOBridge.GetLevel();
                if (currentPlayerLevel < node.RequiredPlayerLevel)
                {
                    return new InvestResult(false, L10n.Get("player_level_required", node.RequiredPlayerLevel.ToString(), currentPlayerLevel.ToString()));
                }
            }
            
            // 1. 최대 레벨 체크 - 이미 언락된 스킬은 조용히 처리
            int currentLevel = manager.GetSkillLevel(node.Id);
            int pendingLevel = manager.pendingInvestments.ContainsKey(node.Id) ? manager.pendingInvestments[node.Id] : 0;
            if (currentLevel + pendingLevel >= node.MaxLevel) 
            {
                // 이미 습득 완료된 스킬에 대해서는 메시지 없이 조용히 실패 처리
                return new InvestResult(false, "");
            }

            // 2. 직업 스킬은 트로피 체크, 생산 스킬은 아이템 체크, 일반 스킬은 포인트 체크
            if (node.Id == "Paladin" || node.Id == "Tanker" || node.Id == "Berserker" ||
                node.Id == "Rogue" || node.Id == "Mage" || node.Id == "Archer")
            {
                // === 직업 중복 선택 방지 (우선 체크) ===
                string[] jobIds = { "Paladin", "Tanker", "Berserker", "Rogue", "Mage", "Archer" };
                foreach (var jobId in jobIds)
                {
                    if (jobId != node.Id)
                    {
                        int existingLevel = manager.GetSkillLevel(jobId);
                        int existingPending = manager.pendingInvestments.ContainsKey(jobId) ? manager.pendingInvestments[jobId] : 0;
                        if (existingLevel > 0 || existingPending > 0)
                        {
                            return new InvestResult(false, L10n.Get("job_class_one_only"));
                        }
                    }
                }

                // 직업 스킬: Eikthyr 트로피 체크
                var player = Player.m_localPlayer;
                if (player == null)
                {
                    return new InvestResult(false, L10n.Get("player_info_unavailable"));
                }

                var inventory = player.GetInventory();
                if (inventory == null)
                {
                    return new InvestResult(false, L10n.Get("inventory_unavailable"));
                }

                // 이미 전직한 경우는 트로피 체크 없이 통과
                if (currentLevel <= 0)
                {
                    // 처음 전직하는 경우 Eikthyr 트로피 확인
                    bool hasEikthyrTrophy = inventory.HaveItem("$item_trophy_eikthyr");
                    if (!hasEikthyrTrophy)
                    {
                        return new InvestResult(false, L10n.Get("trophy_eikthyr_required"));
                    }
                }
            }
            else if (SkillTree.SkillItemRequirements.IsProductionSkill(node.Id))
            {
                // 생산 스킬: 아이템 요구사항 체크
                if (!manager.CanLearnProductionSkill(node.Id))
                {
                    return new InvestResult(false, L10n.Get("items_insufficient"));
                }
            }
            else
            {
                // 일반 스킬: 포인트 체크
                int availablePoints = manager.GetAvailablePoints(true);
                if (availablePoints < node.RequiredPoints)
                {
                    return new InvestResult(false, L10n.Get("skill_insufficient_points_detail", node.RequiredPoints, availablePoints));
                }
            }

            // 3. 선행 스킬 체크
            if (node.Prerequisites != null && node.Prerequisites.Count > 0)
            {
                // 특별 케이스: 장인(grandmaster_artisan)은 AND 조건 (모든 전제조건 필요)
                if (node.Id == "grandmaster_artisan")
                {
                    var missingPrereqs = new List<string>();
                    foreach (var preId in node.Prerequisites)
                    {
                        int preLevel = manager.GetSkillLevel(preId);
                        int prePendingLevel = manager.pendingInvestments.ContainsKey(preId) ? manager.pendingInvestments[preId] : 0;
                        if (preLevel + prePendingLevel <= 0)
                        {
                            var preNode = manager.SkillNodes.ContainsKey(preId) ? manager.SkillNodes[preId] : null;
                            missingPrereqs.Add(preNode?.Name ?? preId);
                        }
                    }
                    if (missingPrereqs.Count > 0)
                    {
                        return new InvestResult(false, L10n.Get("skill_prerequisite_all_required", string.Join(", ", missingPrereqs)));
                    }
                }
                else
                {
                    // 일반 케이스: OR 조건 (하나 이상의 전제조건만 만족하면 됨)
                    bool hasAnyPrerequisite = false;
                    var availablePrereqs = new List<string>();
                    foreach (var preId in node.Prerequisites)
                    {
                        int preLevel = manager.GetSkillLevel(preId);
                        int prePendingLevel = manager.pendingInvestments.ContainsKey(preId) ? manager.pendingInvestments[preId] : 0;
                        if (preLevel + prePendingLevel > 0)
                        {
                            hasAnyPrerequisite = true;
                            break;
                        }
                        else
                        {
                            var preNode = manager.SkillNodes.ContainsKey(preId) ? manager.SkillNodes[preId] : null;
                            availablePrereqs.Add(preNode?.Name ?? preId);
                        }
                    }
                    if (!hasAnyPrerequisite)
                    {
                        string orSeparator = L10n.Get("or_separator");
                        return new InvestResult(false, L10n.Get("skill_prerequisite_any_required", string.Join($" {orSeparator} ", availablePrereqs)));
                    }
                }
            }

            return new InvestResult(true, L10n.Get("can_invest"));
        }

        // 포인트 투자 조건 체크(실제 구현)
        private bool CanInvest(CaptainSkillTree.SkillTree.SkillNode node)
        {
            var manager = SkillTree.SkillTreeManager.Instance;
            // 투자 조건 체크 시작

            // 0. 플레이어 레벨 조건 체크 (직업 아이콘 등)
            if (node.RequiredPlayerLevel > 0)
            {
                int currentPlayerLevel = CaptainMMOBridge.GetLevel();
                if (currentPlayerLevel < node.RequiredPlayerLevel)
                {
                    return false;
                }
            }

            // 1. 최대 레벨 체크
            int currentLevel = manager.GetSkillLevel(node.Id);
            int pendingLevel = manager.pendingInvestments.ContainsKey(node.Id) ? manager.pendingInvestments[node.Id] : 0;
            if (currentLevel + pendingLevel >= node.MaxLevel)
            {
                return false;
            }

            // 2. 직업 스킬은 트로피 체크, 생산 스킬은 아이템 체크, 일반 스킬은 포인트 체크
            if (node.Id == "Paladin" || node.Id == "Tanker" || node.Id == "Berserker" ||
                node.Id == "Rogue" || node.Id == "Mage" || node.Id == "Archer")
            {
                // 직업 스킬: Eikthyr 트로피 체크
                var player = Player.m_localPlayer;
                if (player == null)
                {
                    Plugin.Log.LogError($"[CanInvest 디버깅] {node.Id} 플레이어가 null입니다");
                    return false;
                }

                var inventory = player.GetInventory();
                if (inventory == null)
                {
                    Plugin.Log.LogError($"[CanInvest 디버깅] {node.Id} 인벤토리가 null입니다");
                    return false;
                }

                // 이미 전직한 경우는 트로피 체크 없이 통과
                if (currentLevel <= 0)
                {
                    // 처음 전직하는 경우 Eikthyr 트로피 확인
                    bool hasEikthyrTrophy = inventory.HaveItem("$item_trophy_eikthyr");
                    if (!hasEikthyrTrophy)
                    {
                        return false;
                    }
                }
            }
            else if (SkillTree.SkillItemRequirements.IsProductionSkill(node.Id))
            {
                // 생산 스킬: 아이템 요구사항 체크
                if (!manager.CanLearnProductionSkill(node.Id))
                {
                    return false;
                }
            }
            else
            {
                // 일반 스킬: 포인트 체크
                if (manager.GetAvailablePoints(true) < node.RequiredPoints)
                {
                    return false;
                }
            }

            // 3. 선행 스킬 체크
            if (node.Prerequisites != null && node.Prerequisites.Count > 0)
            {
                // 특별 케이스: 장인(grandmaster_artisan)은 AND 조건 (모든 전제조건 필요)
                if (node.Id == "grandmaster_artisan")
                {
                    foreach (var preId in node.Prerequisites)
                    {
                        int preLevel = manager.GetSkillLevel(preId);
                        int prePendingLevel = manager.pendingInvestments.ContainsKey(preId) ? manager.pendingInvestments[preId] : 0;
                        if (preLevel + prePendingLevel <= 0)
                        {
                            return false; // 하나라도 만족하지 않으면 실패
                        }
                    }
                }
                else
                {
                    // 일반 케이스: OR 조건 (하나 이상의 전제조건만 만족하면 됨)
                    bool hasAnyPrerequisite = false;
                    foreach (var preId in node.Prerequisites)
                    {
                        int preLevel = manager.GetSkillLevel(preId);
                        int prePendingLevel = manager.pendingInvestments.ContainsKey(preId) ? manager.pendingInvestments[preId] : 0;
                        if (preLevel + prePendingLevel > 0)
                        {
                            hasAnyPrerequisite = true;
                            break;
                        }
                    }
                    if (!hasAnyPrerequisite)
                    {
                        return false;
                    }
                }
            }


            // 4. 직업 스킬 아이템 요구사항 체크
            if (!CheckJobSkillRequirements(node))
            {
                return false; // 필요한 아이템이 없음
            }

            // 5. Expert-Based Active Skill Limitation 검증
            var validation = manager.ValidateActiveSkillLearning(node.Id);
            if (!validation.canLearn && validation.isBlocking)
            {
                return false; // 블로킹 에러는 투자 자체를 막음
            }

            return true;
        }
        // 포인트 투자 처리(실제 구현)
        private void InvestPoint(CaptainSkillTree.SkillTree.SkillNode node)
        {
            var manager = SkillTree.SkillTreeManager.Instance;
            
            // Expert-Based Active Skill 검증 및 메시지 표시
            var validation = manager.ValidateActiveSkillLearning(node.Id);
            if (!validation.canLearn && validation.isBlocking)
            {
                // 블로킹 에러 - 빨간색 메시지로 표시하고 투자 차단
                tooltipUI.ShowWarning(validation.message);
                return;
            }
            else if (!string.IsNullOrEmpty(validation.message))
            {
                // 경고 메시지 - 노란색으로 표시하되 투자는 허용
                if (validation.message.Contains("권장되지 않습니다"))
                {
                    ShowColoredWarning(validation.message, Color.yellow);
                }
                else
                {
                    ShowColoredWarning(validation.message, Color.green);
                }
            }
            
            // 생산 스킬은 아이템 소모, 일반 스킬은 포인트 소모
            if (SkillTree.SkillItemRequirements.IsProductionSkill(node.Id))
            {
                // 생산 스킬: 확인 다이얼로그 표시 후 언락
                ShowProductionSkillConfirmDialog(node, manager);
                return;
            }
            else
            {
                // 일반 스킬: 포인트 소모 (기존 시스템)
                int beforePending = manager.pendingInvestments.ContainsKey(node.Id) ? manager.pendingInvestments[node.Id] : 0;
                manager.AddPendingInvestment(node.Id);
                int afterPending = manager.pendingInvestments.ContainsKey(node.Id) ? manager.pendingInvestments[node.Id] : 0;
                
                // 투자가 차단된 경우 (직업 제한 등) 아무것도 하지 않고 리턴
                if (beforePending == afterPending)
                {
                    return;
                }
            }
            
            // 모든 아이콘에 클릭 효과음 재생 (직업 아이콘 포함)
            PlayNormalNodeSound();

            // 클릭 애니메이션 재생 (모든 노드, 직업 아이콘 포함)
            if (nodeUI != null && nodeUI.nodeObjects.ContainsKey(node.Id))
            {
                var nodeObj = nodeUI.nodeObjects[node.Id];
                if (nodeObj != null)
                {
                    var rect = nodeObj.GetComponent<RectTransform>();
                    if (rect != null)
                    {
                        StartCoroutine(NodeInvestAnimation(rect));
                    }
                }
            }

            nodeUI.RefreshNodeStates();
            RefreshUI();
        }
        // 투자 애니메이션(Scale 변화)
        private System.Collections.IEnumerator NodeInvestAnimation(RectTransform nodeRect)
        {
            Vector3 orig = nodeRect.localScale;
            nodeRect.localScale = orig * 1.2f;
            yield return new WaitForSeconds(0.1f);
            nodeRect.localScale = orig;
        }
        // 툴팁/경고 메시지 오브젝트 및 표시 함수
        private void CreateWarning(Transform parent)
        {
            warningObj = new GameObject("Warning", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            warningObj.transform.SetParent(parent, false);
            var rect = warningObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(400, 30); // 너비 증가, 높이 감소
            rect.anchoredPosition = new Vector2(0, 120); // 100에서 120으로 변경 (+20)
            warningText = warningObj.GetComponent<Text>();
            warningText.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // 포인트 초기화 버튼과 동일한 폰트
            warningText.fontSize = 20; // 16에서 20으로 변경
            warningText.color = Color.red;
            warningText.alignment = TextAnchor.MiddleCenter; // 중앙 정렬
            warningObj.SetActive(false);
        }
        private void ShowWarning(string msg)
        {
            if (warningObj == null || warningText == null)
            {
                return;
            }
            
            try
            {
                warningObj.SetActive(true);
                warningText.color = Color.red;
                warningText.text = msg;
                warningObj.transform.SetAsLastSibling();
                CancelInvoke(nameof(HideWarning));
                Invoke(nameof(HideWarning), 1.5f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeUI.ShowWarning] 경고 표시 중 오류: {ex.Message}");
            }
        }
        private void HideWarning()
        {
            if (warningObj != null) warningObj.SetActive(false);
        }
        
        /// <summary>
        /// 색상을 지정할 수 있는 경고 메시지 표시
        /// </summary>
        private void ShowColoredWarning(string msg, Color color)
        {
            if (warningObj == null)
            {
                CreateWarning(panel.transform);
            }
            
            warningObj.SetActive(true);
            warningText.color = color;
            warningText.text = msg;
            warningObj.transform.SetAsLastSibling();
            CancelInvoke(nameof(HideWarning));
            Invoke(nameof(HideWarning), 2.5f); // 경고 메시지는 조금 더 오래 표시
        }
        public void ShowTooltip(CaptainSkillTree.SkillTree.SkillNode node, Vector2 nodePos)
        {
            if (dynamicTooltipObj != null)
                Destroy(dynamicTooltipObj);
            var canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas == null) return;
            dynamicTooltipObj = new GameObject("Tooltip", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            dynamicTooltipObj.transform.SetParent(canvas.transform, false);
            var rect = dynamicTooltipObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(410, 180);
            rect.pivot = new Vector2(0, 1); // 좌상단 기준
            // 배경: 검정, 완전 불투명
            var img = dynamicTooltipObj.GetComponent<Image>();
            img.color = new Color(0f, 0f, 0f, 0.95f);
            img.raycastTarget = false;
            // 텍스트 오브젝트 생성
            var txtObj = new GameObject("TooltipText", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            txtObj.transform.SetParent(dynamicTooltipObj.transform, false);
            var txtRect = txtObj.GetComponent<RectTransform>();
            txtRect.anchorMin = new Vector2(0, 0);
            txtRect.anchorMax = new Vector2(1, 1);
            txtRect.offsetMin = new Vector2(10, 10);
            txtRect.offsetMax = new Vector2(-10, -10);
            var txt = txtObj.GetComponent<Text>();
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = 20;
            txt.color = Color.white;
            txt.alignment = TextAnchor.UpperLeft;
            txt.horizontalOverflow = HorizontalWrapMode.Wrap;
            txt.verticalOverflow = VerticalWrapMode.Overflow;
            string nodeName = node.Name ?? node.Id;
            string desc = node.Description ?? "";
            
            // Config 값을 반영한 설명 생성 (SkillTreeTooltip.cs의 ApplyConfigToDescription 사용)
            var tooltipUI = gameObject.GetComponent<SkillTreeTooltipUI>();
            if (tooltipUI != null)
            {
                string tempDescNoTag = Regex.Replace(desc, "<.*?>", "");
                desc = tooltipUI.ApplyConfigToDescription(node.Id, tempDescNoTag);
            }
            
            // 안내문구(※ ... 착용시 효과발동) 라인 분리 (RichText 태그 제거 후 추출)
            string descNoTag = Regex.Replace(desc, "<.*?>", "");
            string descMain = descNoTag;
            string condLine = null;
            var condMatch = Regex.Match(descNoTag, @"(※ ?[가-힣 ]+착용시 효과발동|※ ?[가-힣 ]+착용 시 효과 발동|※ ?[가-힣 ]+사용 시 효과 발동)");
            if (condMatch.Success) {
                condLine = condMatch.Value;
                descMain = descNoTag.Replace(condLine, "").TrimEnd('\n');
            }
            // Description/안내문구 내 RichText 태그 제거(크기/색상)
            descMain = descMain.Replace("\n", "").Trim();
            if (condLine != null) {
                condLine = condLine.Replace("\n", "").Trim();
            }
            string tooltipText = "";
            tooltipText += $"<color=#FFD700><size=20>[{nodeName}]</size></color>\n";
            tooltipText += $"<color=#FFFFFF><size=18>{descMain}</size></color>\n\n";
            if (!string.IsNullOrEmpty(condLine)) {
                if (condLine.StartsWith("※")) {
                    tooltipText += "<color=#DDA0DD><size=18>※</size></color>";
                    tooltipText += $"<color=#00BFFF><size=18>{condLine.Substring(1).Trim()}</size></color>\n";
                } else {
                    tooltipText += "<color=#DDA0DD><size=18>※</size></color>";
                    tooltipText += $"<color=#00BFFF><size=18>{condLine.Trim()}</size></color>\n";
                }
            }
            // 플레이어 레벨 조건 강조
            if (node.RequiredPlayerLevel > 0)
            {
                tooltipText += $"<color=#FFD700><b>{L10n.Get("player_level_required_short", node.RequiredPlayerLevel.ToString())}</b></color>\n";
            }

            // Prerequisites 조건 표시 (특히 장인 노드용)
            if (node.Prerequisites != null && node.Prerequisites.Count > 0)
            {
                var manager = SkillTree.SkillTreeManager.Instance;
                string prerequisiteText = "";
                
                if (node.Id == "grandmaster_artisan")
                {
                    // 장인 노드는 특별히 한국어 이름으로 표시 (AND 조건)
                    prerequisiteText = $"<size=18>{L10n.Get("prerequisite_label")} <color=#FFA500>{L10n.Get("req_grind_expert")} + {L10n.Get("req_craft_expert")}</color></size>\n";
                }
                else if (node.Prerequisites.Count > 1)
                {
                    // 여러 Prerequisites가 있는 경우 (OR 조건)
                    var prereqNames = new List<string>();
                    foreach (var preId in node.Prerequisites)
                    {
                        if (manager.SkillNodes.ContainsKey(preId))
                        {
                            prereqNames.Add(manager.SkillNodes[preId].Name);
                        }
                    }
                    if (prereqNames.Count > 0)
                    {
                        prerequisiteText = $"<size=18>{L10n.Get("prerequisite_label")} <color=#FFA500>{string.Join($" {L10n.Get("or_separator")} ", prereqNames)}</color></size>\n";
                    }
                }
                else
                {
                    // 단일 Prerequisite인 경우
                    var preId = node.Prerequisites[0];
                    if (manager.SkillNodes.ContainsKey(preId))
                    {
                        prerequisiteText = $"<size=18>{L10n.Get("prerequisite_label")} <color=#FFA500>{manager.SkillNodes[preId].Name}</color></size>\n";
                    }
                }
                
                if (!string.IsNullOrEmpty(prerequisiteText))
                {
                    tooltipText += prerequisiteText;
                }
            }

            tooltipText += $"<size=18>{L10n.Get("required_points_label")} <color=#FF0000>{node.RequiredPoints}</color></size>";
            txt.supportRichText = true;
            txt.text = tooltipText;
            // 마우스 위치를 Canvas 로컬 좌표로 변환
            Vector2 mousePos = Input.mousePosition;
            Vector2 localPoint;
            Vector2 offset = new Vector2(20, -20);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    mousePos,
                    canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                    out localPoint))
            {
                rect.anchoredPosition = localPoint + offset;
            }
            else
            {
                rect.anchoredPosition = mousePos + offset;
            }
            dynamicTooltipObj.transform.SetAsLastSibling();
        }
        public void HideTooltip()
        {
            if (dynamicTooltipObj != null)
            {
                Destroy(dynamicTooltipObj);
                dynamicTooltipObj = null;
            }
        }

        // 스킬 투자 확정 시 이펙트와 효과음
        private void PlaySkillInvestmentEffects()
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;

            // 투자된 스킬 개수 확인
            int totalInvestedSkills = manager.pendingInvestments.Count;

            // 플레이어 메시지로 투자 확정 피드백
            var player = Player.m_localPlayer;
            if (player != null && totalInvestedSkills > 0)
            {
                player.Message(MessageHud.MessageType.TopLeft, $"✅ {totalInvestedSkills}개 스킬 습득 완료!", 0, null);
            }

            // 투자 예정인 노드들에 이펙트 적용
            foreach (var investment in manager.pendingInvestments)
            {
                if (nodeUI != null && nodeUI.nodeObjects.ContainsKey(investment.Key))
                {
                    var nodeObj = nodeUI.nodeObjects[investment.Key];
                    var node = manager.SkillNodes[investment.Key];

                    // 스킬 이름 표시
                    if (player != null)
                    {
                        CaptainSkillTree.SkillTree.SkillEffect.ShowSkillEffectText(player, $"🌟 {node.Name} 습득!",
                            new Color(1f, 0.8f, 0.2f), CaptainSkillTree.SkillTree.SkillEffect.SkillEffectTextType.Standard);
                    }

                    // 직업 아이콘인지 확인
                    if (IsJobIcon(node))
                    {
                        // 직업 아이콘 특별 효과
                        StartCoroutine(PlayJobIconSpecialEffect(nodeObj.GetComponent<RectTransform>()));
                    }
                    else
                    {
                        // 일반 노드 효과 (효과음은 클릭 시점에 이미 재생됨)
                        StartCoroutine(PlayNodeUnlockAnimation(nodeObj.GetComponent<RectTransform>()));
                    }
                }
            }
            
            // 확정 효과음 재생
            PlayConfirmSound();
        }

        /// <summary>
        /// 생산 스킬 언락 확인 다이얼로그 표시
        /// </summary>
        private void ShowProductionSkillConfirmDialog(CaptainSkillTree.SkillTree.SkillNode node, CaptainSkillTree.SkillTree.SkillTreeManager manager)
        {
            // 재료 목록 가져오기
            var requirementText = CaptainSkillTree.SkillTree.ItemManager.GetSkillRequirementsText(node.Id);
            var resourceText = CaptainSkillTree.SkillTree.ResourceConsumption.GetConsumptionCostText(node.Id);
            
            string fullRequirementText = "";
            if (!string.IsNullOrEmpty(requirementText) && requirementText != "필요 재료 없음")
            {
                fullRequirementText += requirementText;
            }
            if (!string.IsNullOrEmpty(resourceText) && resourceText != "추가 비용 없음")
            {
                if (!string.IsNullOrEmpty(fullRequirementText))
                    fullRequirementText += "\n";
                fullRequirementText += resourceText;
            }
            
            // 확인 다이얼로그 생성
            string dialogMessage = $"<color=#FFD700><size=18>{L10n.Get("skill_acquire_title", node.Name)}</size></color>\n\n";
            dialogMessage += $"<color=#87CEEB><size=16>{L10n.Get("skill_acquire_materials")}</size></color>\n";
            dialogMessage += $"<color=#FFEB3B><size=14>{fullRequirementText}</size></color>\n\n";
            dialogMessage += $"<color=#FFA500><size=16>{L10n.Get("skill_acquire_confirm")}</size></color>";
            
            CreateConfirmDialog(dialogMessage, 
                () => { // 확인 버튼
                    TryUnlockProductionSkillConfirmed(node, manager);
                },
                () => { // 취소 버튼
                    // 생산 스킬 언락 취소됨
                });
        }
        
        /// <summary>
        /// 생산 스킬 언락 확인 후 실제 언락 처리
        /// </summary>
        private void TryUnlockProductionSkillConfirmed(CaptainSkillTree.SkillTree.SkillNode node, CaptainSkillTree.SkillTree.SkillTreeManager manager)
        {
            // 생산 스킬 언락 시도
            if (!manager.TryUnlockProductionSkill(node.Id))
            {
                // 생산 스킬 언락 실패
                var validationMessage = manager.GetProductionSkillValidationMessage(node.Id);
                if (validationMessage != "조건 충족")
                {
                    tooltipUI.ShowWarning($"재료 부족:\n{validationMessage}");
                }
                else
                {
                    tooltipUI.ShowWarning("스킬 언락에 실패했습니다.");
                }
                return;
            }
            // 생산 스킬 언락 성공
            
            // 대화상자 닫기 (스킬 언락 성공 시)
            if (confirmDialog != null)
            {
                UnityEngine.Object.Destroy(confirmDialog);
                confirmDialog = null;
            }
            
            // UI 갱신
            RefreshUI();
            UpdateSkillPointText();
            
            // 확정 효과음 재생
            PlayConfirmSound();
            
            // 언락 애니메이션 실행
            if (nodeObjects.ContainsKey(node.Id))
            {
                var nodeRect = nodeObjects[node.Id].GetComponent<RectTransform>();
                StartCoroutine(PlayNodeUnlockAnimation(nodeRect));
            }
        }
        
        /// <summary>
        /// 확인/취소 다이얼로그 생성
        /// </summary>
        private void CreateConfirmDialog(string message, System.Action onConfirm, System.Action onCancel)
        {
            // 기존 다이얼로그가 있으면 제거
            var existingDialog = GameObject.Find("ProductionSkillConfirmDialog");
            if (existingDialog != null)
            {
                Destroy(existingDialog);
            }
            
            // 다이얼로그 오브젝트 생성
            var dialogObj = new GameObject("ProductionSkillConfirmDialog", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            dialogObj.transform.SetParent(panel.transform, false);
            
            // confirmDialog 변수에 할당
            confirmDialog = dialogObj;
            
            var dialogRect = dialogObj.GetComponent<RectTransform>();
            dialogRect.sizeDelta = new Vector2(400, 250);
            dialogRect.anchoredPosition = Vector2.zero;
            
            var dialogImage = dialogObj.GetComponent<Image>();
            dialogImage.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            
            // 메시지 텍스트
            var textObj = new GameObject("DialogText", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Text));
            textObj.transform.SetParent(dialogObj.transform, false);
            
            var textRect = textObj.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(360, 150);
            textRect.anchoredPosition = new Vector2(0, 20);
            
            var text = textObj.GetComponent<UnityEngine.UI.Text>();
            text.text = message;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 14;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.supportRichText = true;
            
            // 확인 버튼
            var confirmBtnObj = new GameObject("ConfirmButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            confirmBtnObj.transform.SetParent(dialogObj.transform, false);
            
            var confirmRect = confirmBtnObj.GetComponent<RectTransform>();
            confirmRect.sizeDelta = new Vector2(80, 30);
            confirmRect.anchoredPosition = new Vector2(-50, -80);
            
            var confirmImage = confirmBtnObj.GetComponent<Image>();
            confirmImage.color = new Color(0.2f, 0.7f, 0.2f, 1f);
            
            var confirmBtn = confirmBtnObj.GetComponent<Button>();
            confirmBtn.onClick.AddListener(() => {
                onConfirm?.Invoke();
                Destroy(dialogObj);
            });
            
            // 확인 버튼 텍스트
            var confirmTextObj = new GameObject("ConfirmText", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Text));
            confirmTextObj.transform.SetParent(confirmBtnObj.transform, false);
            
            var confirmTextRect = confirmTextObj.GetComponent<RectTransform>();
            confirmTextRect.sizeDelta = new Vector2(80, 30);
            confirmTextRect.anchoredPosition = Vector2.zero;
            
            var confirmText = confirmTextObj.GetComponent<UnityEngine.UI.Text>();
            confirmText.text = L10n.Get("ui_confirm");
            confirmText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            confirmText.fontSize = 14;
            confirmText.color = Color.white;
            confirmText.alignment = TextAnchor.MiddleCenter;

            // 취소 버튼
            var cancelBtnObj = new GameObject("CancelButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            cancelBtnObj.transform.SetParent(dialogObj.transform, false);

            var cancelRect = cancelBtnObj.GetComponent<RectTransform>();
            cancelRect.sizeDelta = new Vector2(80, 30);
            cancelRect.anchoredPosition = new Vector2(50, -80);

            var cancelImage = cancelBtnObj.GetComponent<Image>();
            cancelImage.color = new Color(0.7f, 0.2f, 0.2f, 1f);

            var cancelBtn = cancelBtnObj.GetComponent<Button>();
            cancelBtn.onClick.AddListener(() => {
                onCancel?.Invoke();
                Destroy(dialogObj);
            });

            // 취소 버튼 텍스트
            var cancelTextObj = new GameObject("CancelText", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Text));
            cancelTextObj.transform.SetParent(cancelBtnObj.transform, false);

            var cancelTextRect = cancelTextObj.GetComponent<RectTransform>();
            cancelTextRect.sizeDelta = new Vector2(80, 30);
            cancelTextRect.anchoredPosition = Vector2.zero;

            var cancelText = cancelTextObj.GetComponent<UnityEngine.UI.Text>();
            cancelText.text = L10n.Get("ui_cancel");
            cancelText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            cancelText.fontSize = 14;
            cancelText.color = Color.white;
            cancelText.alignment = TextAnchor.MiddleCenter;
            
            // 다이얼로그를 최상위로 설정
            dialogObj.transform.SetAsLastSibling();
        }

        // 노드 언락 애니메이션 (DOTween 대신 코루틴 사용)
        private System.Collections.IEnumerator PlayNodeUnlockAnimation(RectTransform nodeRect)
        {
            if (nodeRect == null) yield break;
            
            Vector3 originalScale = nodeRect.localScale;
            Vector3 originalRotation = nodeRect.eulerAngles;
            
            float duration = 0.8f;
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / duration;
                
                // 회전하면서 커지는 애니메이션
                float rotation = Mathf.Sin(progress * Mathf.PI * 4) * 360f; // 4번 회전
                float scale = 1f + Mathf.Sin(progress * Mathf.PI) * 0.5f; // 최대 1.5배까지 커짐
                
                nodeRect.eulerAngles = originalRotation + Vector3.forward * rotation;
                nodeRect.localScale = originalScale * scale;
                
                yield return null;
            }
            
            // 원래 상태로 복원
            nodeRect.eulerAngles = originalRotation;
            nodeRect.localScale = originalScale;
        }

        // 확정 효과 재생 (LevelUpVFX 사용)
        private void PlayConfirmSound()
        {
            try
            {
                var znet = ZNetScene.instance;
                if (znet != null)
                {
                    var player = Player.m_localPlayer;
                    if (player == null) return;
                    
                    // MMO LevelUpVFX 효과 우선 사용
                    var levelUpVFX = znet.GetPrefab("LevelUpVFX");
                    if (levelUpVFX != null)
                    {
                        UnityEngine.Object.Instantiate(levelUpVFX, player.transform.position, Quaternion.identity);
                        Plugin.Log.LogDebug($"[스킬트리] LevelUpVFX 효과 재생 성공");
                        return;
                    }
                    
                    // 대체 LevelUpVFX2 시도
                    var levelUpVFX2 = znet.GetPrefab("LevelUpVFX2");
                    if (levelUpVFX2 != null)
                    {
                        UnityEngine.Object.Instantiate(levelUpVFX2, player.transform.position, Quaternion.identity);
                        Debug.Log($"[스킬트리] LevelUpVFX2 효과 재생 성공");
                        return;
                    }
                    
                    Debug.LogWarning($"[스킬트리] LevelUpVFX 효과를 찾을 수 없음");

                    // 마지막 대안: 플레이어 메시지로 피드백
                    player.Message(MessageHud.MessageType.TopLeft, L10n.Get("skill_invest_success"), 0, null);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[스킬트리] LevelUpVFX 효과 재생 실패: {ex.Message}");
            }
        }

        // 취소 효과음 재생 (비활성화됨 - sfx_guardstone_denied 효과음 사용 안 함)
        private void PlayCancelSound()
        {
            // 효과음 없이 빈 메서드로 유지
            // 필요 시 다른 효과음으로 대체 가능
        }

        // 직업 아이콘 특별 효과 (고급 중후한 효과)
        private System.Collections.IEnumerator PlayJobIconSpecialEffect(RectTransform nodeRect)
        {
            if (nodeRect == null) yield break;

            // 원본 상태 저장
            Vector2 originalPosition = nodeRect.anchoredPosition;
            Vector3 originalScale = nodeRect.localScale;
            Vector3 originalRotation = nodeRect.eulerAngles;
            var img = nodeRect.GetComponent<Image>();
            var originalColor = img.color;
            
            // 화면 중앙 좌표
            Vector2 centerPosition = Vector2.zero;
            
            // === 효과 시작 ===
            // 배경 어두워짐 효과 시작
            StartCoroutine(DarkenBackground(4.0f));
            
            // 시작 효과음 (웅장한 베이스)
            PlayJobIconSoundLayer("start");
            
            // 오라 효과 생성
            var auraEffect = CreateAuraEffect(nodeRect);
            
            // === 1단계: 준비 (0.5초) ===
            float prepDuration = 0.5f;
            float elapsed = 0f;
            
            // 화면 진동 시작
            StartCoroutine(CameraShake(0.1f, 4.0f));
            
            while (elapsed < prepDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / prepDuration;
                
                // 가벼운 펄스로 예고
                float pulseScale = 1f + Mathf.Sin(progress * Mathf.PI * 8) * 0.05f;
                nodeRect.localScale = originalScale * pulseScale;
                
                // 오라 강도 증가
                if (auraEffect != null)
                {
                    var auraColor = auraEffect.color;
                    auraColor.a = progress * 0.3f;
                    auraEffect.color = auraColor;
                }
                
                yield return null;
            }
            
            // === 2단계: 상승 및 회전 (2.0초) ===
            PlayJobIconSoundLayer("middle");
            
            float phase2Duration = 2.0f;
            elapsed = 0f;
            
            while (elapsed < phase2Duration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / phase2Duration;
                float easedProgress = EaseOutQuart(progress);
                
                // 위치 보간 (부드럽게 중앙으로)
                nodeRect.anchoredPosition = Vector2.Lerp(originalPosition, centerPosition, easedProgress);
                
                // 크기 보간 (2.5배로 확대)
                float scaleMultiplier = Mathf.Lerp(1f, 2.5f, easedProgress);
                nodeRect.localScale = originalScale * scaleMultiplier;
                
                // 회전 (처음 빠르게, 점점 느려짐) - 정확한 각도로 정착
                float targetRotation = 720f; // 2바퀴 회전
                float rotationProgress = targetRotation * EaseOutQuart(progress);
                nodeRect.eulerAngles = originalRotation + Vector3.forward * rotationProgress;
                
                // 오라 효과 강화
                if (auraEffect != null)
                {
                    var auraColor = auraEffect.color;
                    auraColor.a = 0.3f + progress * 0.4f;
                    auraEffect.color = auraColor;
                    
                    // 오라 크기도 증가
                    auraEffect.transform.localScale = Vector3.one * (1f + progress * 0.5f);
                }
                
                yield return null;
            }
            
            // === 3단계: 절정 - 파티클 폭발 & 강력한 빛 (1.0초) ===
            PlayJobIconSoundLayer("climax");
            CreateParticleExplosion(nodeRect.position);
            
            float climaxDuration = 1.0f;
            elapsed = 0f;
            
            while (elapsed < climaxDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / climaxDuration;
                
                // 강력한 빛 효과 (은빛 → 흰색 → 원래색)
                Color glowColor;
                Color silverColor = new Color(0.8f, 0.8f, 0.9f, 1f); // 은빛 색상
                if (progress < 0.3f)
                {
                    glowColor = Color.Lerp(originalColor, silverColor, progress / 0.3f);
                }
                else if (progress < 0.6f)
                {
                    glowColor = Color.Lerp(silverColor, Color.white, (progress - 0.3f) / 0.3f);
                }
                else
                {
                    glowColor = Color.Lerp(Color.white, originalColor, (progress - 0.6f) / 0.4f);
                }
                img.color = glowColor;
                
                // 오라 최대 강도
                if (auraEffect != null)
                {
                    var auraColor = auraEffect.color;
                    auraColor.a = 0.8f * (1f - progress * 0.5f);
                    auraEffect.color = auraColor;
                }
                
                yield return null;
            }
            
            // === 4단계: 여운과 복귀 (1.5초) ===
            PlayJobIconSoundLayer("finish");
            
            float returnDuration = 1.5f;
            elapsed = 0f;
            
            Vector2 currentPosition = nodeRect.anchoredPosition;
            Vector3 currentScale = nodeRect.localScale;
            Vector3 currentRotation = nodeRect.eulerAngles;
            
            while (elapsed < returnDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / returnDuration;
                float easedProgress = EaseOutBounce(progress); // 탄력감 있는 복귀
                
                // 부드러운 복귀
                nodeRect.anchoredPosition = Vector2.Lerp(currentPosition, originalPosition, easedProgress);
                nodeRect.localScale = Vector3.Lerp(currentScale, originalScale * 1.1f, easedProgress); // 약간 크게 유지
                
                // 회전 정착
                float rotationProgress = EaseInQuart(progress);
                nodeRect.eulerAngles = Vector3.Lerp(currentRotation, originalRotation, rotationProgress);
                
                // 오라 서서히 사라짐
                if (auraEffect != null)
                {
                    var auraColor = auraEffect.color;
                    auraColor.a = 0.3f * (1f - progress);
                    auraEffect.color = auraColor;
                }
                
                yield return null;
            }
            
            // === 최종 정리 ===
            yield return new WaitForSeconds(0.5f); // 여운
            
            // 월드 메시지 표시
            ShowJobChangeWorldMessage(nodeRect.name);
            
            // 오라 제거
            if (auraEffect != null)
                Destroy(auraEffect.gameObject);
            
            // 최종 상태 보장
            nodeRect.anchoredPosition = originalPosition;
            nodeRect.localScale = originalScale;
            nodeRect.eulerAngles = originalRotation;
            img.color = originalColor;
        }
        
        // Easing 함수들
        private float EaseOutQuart(float t)
        {
            return 1f - Mathf.Pow(1f - t, 4f);
        }
        
        private float EaseInQuart(float t)
        {
            return t * t * t * t;
        }
        
        private float EaseOutBounce(float t)
        {
            if (t < 1f / 2.75f)
            {
                return 7.5625f * t * t;
            }
            else if (t < 2f / 2.75f)
            {
                t -= 1.5f / 2.75f;
                return 7.5625f * t * t + 0.75f;
            }
            else if (t < 2.5f / 2.75f)
            {
                t -= 2.25f / 2.75f;
                return 7.5625f * t * t + 0.9375f;
            }
            else
            {
                t -= 2.625f / 2.75f;
                return 7.5625f * t * t + 0.984375f;
            }
        }
        
        // 빛나는 효과
        private System.Collections.IEnumerator BlinkEffect(Image img, Color originalColor)
        {
            if (img == null) yield break;
            
            float blinkDuration = 0.25f;
            
            // 은빛으로 변경
            Color silverColor = new Color(0.8f, 0.8f, 0.9f, 1f); // 은빛 색상
            float elapsed = 0f;
            while (elapsed < blinkDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / blinkDuration;
                img.color = Color.Lerp(originalColor, silverColor, progress);
                yield return null;
            }
            
            // 다시 원래 색상으로
            elapsed = 0f;
            while (elapsed < blinkDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / blinkDuration;
                img.color = Color.Lerp(silverColor, originalColor, progress);
                yield return null;
            }
            
            img.color = originalColor;
        }
        
        // 펄스 효과
        private System.Collections.IEnumerator PulseEffect(RectTransform nodeRect, Vector3 originalScale)
        {
            float pulseDuration = 0.15f;
            
            // 확대
            float elapsed = 0f;
            while (elapsed < pulseDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / pulseDuration;
                nodeRect.localScale = Vector3.Lerp(originalScale, originalScale * 1.1f, EaseOutQuart(progress));
                yield return null;
            }
            
            // 축소
            elapsed = 0f;
            while (elapsed < pulseDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / pulseDuration;
                nodeRect.localScale = Vector3.Lerp(originalScale * 1.1f, originalScale, EaseInQuart(progress));
                yield return null;
            }
            
            nodeRect.localScale = originalScale;
        }

        // 직업 아이콘 특별 효과음 재생 (climax 단계에서 MMO 레벨업 효과음 사용)
        private void PlayJobIconSoundLayer(string phase)
        {
            try
            {
                switch (phase)
                {
                    case "start":
                    case "middle":
                    case "finish":
                        // 다른 단계에서는 효과음 없음 (과부하 방지)
                        break;
                    case "climax":
                        // climax 단계(화면 중앙 도달 시)에서 MMO 레벨업 효과음 사용
                        TryPlayMMOLevelUpSound();
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[직업 아이콘] {phase} 효과음 재생 실패: {ex.Message}");
            }
        }
        
        // 배경 어두워짐 효과
        private System.Collections.IEnumerator DarkenBackground(float duration)
        {
            // 전체 화면을 덮는 어두운 오버레이 생성
            var canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas == null) yield break;
            
            var overlay = new GameObject("DarkOverlay", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            overlay.transform.SetParent(canvas.transform, false);
            
            var overlayRect = overlay.GetComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;
            
            var overlayImg = overlay.GetComponent<Image>();
            overlayImg.color = new Color(0, 0, 0, 0);
            overlayImg.raycastTarget = false;
            
            // 서서히 어두워짐 (최대 50% 어둠)
            float fadeInTime = duration * 0.1f;
            float holdTime = duration * 0.8f;
            float fadeOutTime = duration * 0.1f;
            
            // Fade In
            float elapsed = 0f;
            while (elapsed < fadeInTime)
            {
                elapsed += Time.deltaTime;
                float alpha = (elapsed / fadeInTime) * 0.5f;
                overlayImg.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            
            // Hold
            yield return new WaitForSeconds(holdTime);
            
            // Fade Out
            elapsed = 0f;
            while (elapsed < fadeOutTime)
            {
                elapsed += Time.deltaTime;
                float alpha = 0.5f * (1f - elapsed / fadeOutTime);
                overlayImg.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            
            Destroy(overlay);
        }
        
        // 카메라 셰이크 효과
        private System.Collections.IEnumerator CameraShake(float intensity, float duration)
        {
            var camera = Camera.main;
            if (camera == null) yield break;
            
            Vector3 originalPosition = camera.transform.position;
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                // 강도가 시간에 따라 감소
                float currentIntensity = intensity * (1f - elapsed / duration);
                
                // 무작위 셰이크
                float x = UnityEngine.Random.Range(-1f, 1f) * currentIntensity;
                float y = UnityEngine.Random.Range(-1f, 1f) * currentIntensity;
                
                camera.transform.position = originalPosition + new Vector3(x, y, 0);
                
                yield return null;
            }
            
            camera.transform.position = originalPosition;
        }
        
        // 오라 효과 생성
        private Image CreateAuraEffect(RectTransform target)
        {
            var auraGO = new GameObject("AuraEffect", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            auraGO.transform.SetParent(target.parent, false);
            
            var auraRect = auraGO.GetComponent<RectTransform>();
            auraRect.anchoredPosition = target.anchoredPosition;
            auraRect.sizeDelta = target.sizeDelta * 1.5f;
            
            var auraImg = auraGO.GetComponent<Image>();
            auraImg.color = new Color(1f, 0.8f, 0f, 0f); // 황금빛, 투명
            auraImg.raycastTarget = false;
            
            // 원형 모양으로 설정 (가능하다면)
            auraImg.sprite = null; // 기본 흰색 스프라이트
            
            return auraImg;
        }
        
        // 파티클 폭발 효과 (간단한 버전)
        private void CreateParticleExplosion(Vector3 position)
        {
            try
            {
                // 여러 개의 작은 이미지들을 사방으로 튀어나가게 함
                var canvas = GameObject.FindObjectOfType<Canvas>();
                if (canvas == null) return;
                
                for (int i = 0; i < 12; i++)
                {
                    var particle = new GameObject("Particle", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
                    particle.transform.SetParent(canvas.transform, false);
                    
                    var particleRect = particle.GetComponent<RectTransform>();
                    particleRect.sizeDelta = new Vector2(8, 8);
                    particleRect.position = position;
                    
                    var particleImg = particle.GetComponent<Image>();
                    particleImg.color = new Color(0.8f, 0.8f, 0.9f, 1f); // 은빛 색상
                    particleImg.raycastTarget = false;
                    
                    // 파티클 애니메이션 시작
                    StartCoroutine(AnimateParticle(particleRect, i));
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[파티클 효과] 생성 실패: {ex.Message}");
            }
        }
        
        // 개별 파티클 애니메이션
        private System.Collections.IEnumerator AnimateParticle(RectTransform particle, int index)
        {
            Vector3 startPos = particle.position;
            float angle = (index * 30f) * Mathf.Deg2Rad; // 30도씩 분산
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Vector3 endPos = startPos + direction * 100f;
            
            float duration = 0.8f;
            float elapsed = 0f;
            
            var img = particle.GetComponent<Image>();
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / duration;
                
                // 위치 이동
                particle.position = Vector3.Lerp(startPos, endPos, progress);
                
                // 페이드 아웃
                var color = img.color;
                color.a = 1f - progress;
                img.color = color;
                
                // 크기 감소
                float scale = 1f - progress * 0.5f;
                particle.localScale = Vector3.one * scale;
                
                yield return null;
            }
            
            Destroy(particle.gameObject);
        }
        
        // 직업 아이콘 특별 효과음 재생 (기존 호환성)
        private void PlayJobIconSound()
        {
            PlayJobIconSoundLayer("start");
        }
        
        // 직업 이름 매핑
        private string GetJobDisplayName(string nodeId)
        {
            var manager = CaptainSkillTree.SkillTree.SkillTreeManager.Instance;
            if (manager.SkillNodes.ContainsKey(nodeId))
            {
                var node = manager.SkillNodes[nodeId];
                string iconName = node.IconName;
                
                if (iconName.Contains("Archer")) return "궁수";
                if (iconName.Contains("Tanker")) return "탱커";
                if (iconName.Contains("Berserker")) return "버서커";
                if (iconName.Contains("Rogue")) return "로그";
                if (iconName.Contains("Mage")) return "메이지";
                if (iconName.Contains("Paladin")) return "Paladin";
            }
            return "전사"; // 기본값
        }
        
        // 월드 메시지 표시
        private void ShowJobChangeWorldMessage(string nodeId)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                
                string playerName = player.GetPlayerName();
                string jobName = GetJobDisplayName(nodeId);
                string message = $"{playerName}님이 {jobName}으로 전직하였습니다.";
                
                // Valheim의 MessageHud를 사용하여 월드 메시지 표시
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center, message);

                // 채팅으로도 표시
                Chat.instance?.AddString($"<color=yellow>[전직] {message}</color>");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[전직 메시지] 표시 실패: {ex.Message}");
            }
        }
        
        // 일반 노드 효과음 재생
        private void PlayNormalNodeSound()
        {
            try
            {
                var znet = ZNetScene.instance;
                if (znet != null)
                {
                    var soundPrefab = znet.GetPrefab("sfx_build_hammer_metal");
                    if (soundPrefab != null)
                    {
                        UnityEngine.Object.Instantiate(soundPrefab, Camera.main.transform.position, Quaternion.identity);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[일반 노드] 효과음 재생 실패: {ex.Message}");
            }
        }
        
        // 직업 아이콘 MMO 레벨업 효과음 재생 (LevelUpVFX 사용)
        private void TryPlayMMOLevelUpSound()
        {
            try
            {
                var znet = ZNetScene.instance;
                if (znet != null)
                {
                    // MMO 레벨업 VFX 먼저 시도
                    var mmoSoundPrefab = znet.GetPrefab("LevelUpVFX");
                    if (mmoSoundPrefab != null)
                    {
                        var player = Player.m_localPlayer;
                        if (player != null)
                        {
                            // MMO와 같은 방식으로 플레이어 위치에 생성
                            UnityEngine.Object.Instantiate(mmoSoundPrefab, player.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                        }
                        else
                        {
                            UnityEngine.Object.Instantiate(mmoSoundPrefab, Camera.main.transform.position, Quaternion.identity);
                        }
                        return;
                    }
                    
                    // 대체 MMO 레벨업 VFX 시도
                    var altMmoSoundPrefab = znet.GetPrefab("LevelUpVFX2");
                    if (altMmoSoundPrefab != null)
                    {
                        var player = Player.m_localPlayer;
                        if (player != null)
                        {
                            UnityEngine.Object.Instantiate(altMmoSoundPrefab, player.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                        }
                        else
                        {
                            UnityEngine.Object.Instantiate(altMmoSoundPrefab, Camera.main.transform.position, Quaternion.identity);
                        }
                        return;
                    }
                    
                    // MMO VFX가 없으면 기본 발헤임 효과음 사용
                    var soundPrefab = znet.GetPrefab("sfx_dragon_scream");
                    if (soundPrefab != null)
                    {
                        UnityEngine.Object.Instantiate(soundPrefab, Camera.main.transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    Debug.LogError($"[직업 아이콘] ZNetScene.instance가 null입니다");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[직업 아이콘] 효과음 재생 실패: {ex.Message}");
            }
        }
        
        // 프리팹 리스트 생성 (한 번만 실행)
        private static bool prefabsListCreated = false;
        private void CreatePrefabsList(ZNetScene znet)
        {
            if (prefabsListCreated) return;
            
            try
            {
                string filePath = @"C:\Users\ssuny\AppData\Roaming\r2modmanPlus-local\Valheim\profiles\Cusor_1\BepInEx\프리팹.txt";
                
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("=== ZNetScene 등록된 프리팹 리스트 ===");
                    writer.WriteLine($"생성 시간: {System.DateTime.Now}");
                    writer.WriteLine("");
                    
                    // ZNetScene의 모든 필드와 프로퍼티 검사
                    var type = typeof(ZNetScene);
                    writer.WriteLine("=== ZNetScene 필드 정보 ===");
                    
                    var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    foreach (var field in fields)
                    {
                        writer.WriteLine($"필드: {field.Name} - 타입: {field.FieldType.Name}");
                        
                        // 프리팹과 관련된 필드들 시도
                        if (field.Name.ToLower().Contains("prefab") || field.FieldType.Name.Contains("Dictionary"))
                        {
                            try
                            {
                                var value = field.GetValue(znet);
                                if (value != null)
                                {
                                    writer.WriteLine($"  → 값: {value.GetType().Name}");
                                    
                                    // Dictionary<string, GameObject> 타입인지 확인
                                    if (value is System.Collections.IDictionary dict)
                                    {
                                        writer.WriteLine($"  → Dictionary 발견! 항목 수: {dict.Count}");
                                        writer.WriteLine("");
                                        writer.WriteLine("=== 프리팹 목록 ===");
                                        
                                        var prefabNames = new System.Collections.Generic.List<string>();
                                        foreach (System.Collections.DictionaryEntry entry in dict)
                                        {
                                            if (entry.Value is GameObject go && go != null)
                                            {
                                                prefabNames.Add(go.name);
                                            }
                                            else if (entry.Key != null)
                                            {
                                                prefabNames.Add(entry.Key.ToString());
                                            }
                                        }
                                        
                                        prefabNames.Sort();
                                        foreach (var name in prefabNames)
                                        {
                                            writer.WriteLine(name);
                                        }
                                        
                                        prefabsListCreated = true;
                                        Debug.Log($"[프리팹 리스트] 파일 생성 완료: {filePath}");
                                        return;
                                    }
                                }
                            }
                            catch (System.Exception fieldEx)
                            {
                                writer.WriteLine($"  → 오류: {fieldEx.Message}");
                            }
                        }
                    }
                    
                    writer.WriteLine("");
                    writer.WriteLine("=== 대안: 직접 테스트 ===");
                    
                    // 일반적인 발헤임 효과음들 테스트
                    var commonSounds = new string[]
                    {
                        "sfx_creature_tamed", "sfx_build_hammer_metal", "sfx_build_hammer_wood",
                        "sfx_sword_hit", "sfx_sword_swing", "sfx_hammer_hit", "sfx_blocked", "sfx_guardstone_activate",
                        "sfx_burning", "sfx_creature_alerted", "sfx_sneak", "sfx_dodge",
                        "fx_hit_campdamage", "fx_backstab", "fx_crit", "fx_smoke", "fx_sword_hit", "fx_block"
                    };
                    
                    foreach (var soundName in commonSounds)
                    {
                        var prefab = znet.GetPrefab(soundName);
                        if (prefab != null)
                        {
                            writer.WriteLine($"✓ {soundName} - 사용 가능");
                        }
                        else
                        {
                            writer.WriteLine($"✗ {soundName} - 없음");
                        }
                    }
                }
                
                prefabsListCreated = true;
                Debug.Log($"[프리팹 리스트] 파일 생성 완료: {filePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[프리팹 리스트] 파일 생성 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 직업 스킬의 아이템 요구사항을 체크합니다
        /// </summary>
        private bool CheckJobSkillRequirements(CaptainSkillTree.SkillTree.SkillNode node)
        {
            var player = Player.m_localPlayer;
            if (player == null) return false;
            
            var inventory = player.GetInventory();
            if (inventory == null) return false;
            
            // 직업 스킬이 아닌 경우 통과
            if (node.Id != "Paladin" && node.Id != "Tanker" && node.Id != "Berserker" && 
                node.Id != "Rogue" && node.Id != "Mage" && node.Id != "Archer")
            {
                return true;
            }
            
            var manager = SkillTree.SkillTreeManager.Instance;
            int currentLevel = manager.GetSkillLevel(node.Id);
            
            // 이미 전직한 경우 (레벨 1 이상) 아이템 체크 없이 통과
            if (currentLevel > 0)
            {
                return true;
            }
            
            // 처음 전직하는 경우에만 아이템 체크
            // 에이크쉬르 트로피 체크 (모든 직업 공통)
            bool hasEikthyrTrophy = inventory.HaveItem("$item_trophy_eikthyr");
            
            // 모든 직업이 에이크쉬르 트로피만 필요
            return hasEikthyrTrophy;
        }
        
        /// <summary>
        /// 직업 스킬의 아이템 요구사항을 소모합니다
        /// </summary>
        private void ConsumeJobSkillRequirements(CaptainSkillTree.SkillTree.SkillNode node)
        {
            var player = Player.m_localPlayer;
            if (player == null) return;
            
            var inventory = player.GetInventory();
            if (inventory == null) return;
            
            // 직업 스킬이 아닌 경우 아무것도 소모하지 않음
            if (node.Id != "Paladin" && node.Id != "Tanker" && node.Id != "Berserker" && 
                node.Id != "Rogue" && node.Id != "Mage" && node.Id != "Archer")
            {
                return;
            }
            
            // 모든 직업: 에이크쉬르 트로피만 소모
            inventory.RemoveItem("$item_trophy_eikthyr", 1);
            ShowColoredWarning($"{node.Name} 전직: 에이크쉬르 트로피를 소모했습니다!", Color.green);
            
            // 인벤토리는 RemoveItem 호출 시 자동으로 업데이트됩니다
        }

        #region BGM Control
        /// <summary>
        /// Music On/Off 토글 버튼 생성 (포인트 초기화 버튼 오른쪽)
        /// </summary>
        private void CreateMusicToggleButton(GameObject parent)
        {
            try
            {
                bool isBGMEnabled = SkillTreeBGMManager.Instance?.IsBGMEnabled ?? true;
                Color initialColor = isBGMEnabled
                    ? new Color(0.10f, 0.50f, 0.80f, 1f)
                    : new Color(0.35f, 0.35f, 0.40f, 1f);
                string initialText = isBGMEnabled ? "Music On" : "Music Off";

                musicToggleButton = CreateFancyButton("MusicToggleButton", initialText,
                    new Vector2(262, -82), initialColor, parent.transform, () => ToggleBGM());
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[BGM UI] Music 버튼 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// BGM 토글 처리
        /// </summary>
        private void ToggleBGM()
        {
            try
            {
                if (SkillTreeBGMManager.Instance != null)
                {
                    SkillTreeBGMManager.Instance.ToggleBGM();
                    
                    // UI 업데이트
                    UpdateMusicButtonAppearance();

                    // 상태 메시지 표시 (선택적)
                    if ((System.Object)Player.m_localPlayer != null)
                    {
                        string message = SkillTreeBGMManager.Instance.IsBGMEnabled ? "🎵 BGM 활성화" : "🔇 BGM 비활성화";
                        Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, message, 0, null);
                    }
                }
                else
                {
                    Plugin.Log.LogWarning("[BGM UI] SkillTreeBGMManager.Instance가 null");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[BGM UI] BGM 토글 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// Music 버튼 외관 업데이트 (색상 + 텍스트)
        /// </summary>
        private void UpdateMusicButtonAppearance()
        {
            try
            {
                if (musicToggleButton != null)
                {
                    // 새 레이어 구조: MainBody Image 참조
                    var mainBodyTf = musicToggleButton.transform.Find("MainBody");
                    if (mainBodyTf != null)
                        UpdateMusicButtonColor(mainBodyTf.GetComponent<Image>());

                    var text = musicToggleButton.GetComponentInChildren<Text>();
                    if (text != null)
                        UpdateMusicButtonText(text);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[BGM UI] 버튼 외관 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// BGM 상태에 따라 버튼 색상 업데이트
        /// </summary>
        private void UpdateMusicButtonColor(Image mainLayerImage)
        {
            try
            {
                if (mainLayerImage == null) return;
                bool isBGMEnabled = SkillTreeBGMManager.Instance?.IsBGMEnabled ?? true;
                Color mainColor = isBGMEnabled
                    ? new Color(0.10f, 0.50f, 0.80f, 1f)
                    : new Color(0.35f, 0.35f, 0.40f, 1f);
                mainLayerImage.color = mainColor;

                // 하이라이트 레이어도 함께 업데이트
                var hlTf = mainLayerImage.transform.Find("Highlight");
                if (hlTf != null)
                {
                    var hlImg = hlTf.GetComponent<Image>();
                    if (hlImg != null)
                        hlImg.color = new Color(
                            Mathf.Min(mainColor.r + 0.25f, 1f),
                            Mathf.Min(mainColor.g + 0.25f, 1f),
                            Mathf.Min(mainColor.b + 0.25f, 1f),
                            0.5f
                        );
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[BGM UI] 버튼 색상 업데이트 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// BGM 상태에 따라 버튼 텍스트 업데이트
        /// </summary>
        private void UpdateMusicButtonText(Text buttonText)
        {
            try
            {
                bool isBGMEnabled = SkillTreeBGMManager.Instance?.IsBGMEnabled ?? true;
                buttonText.text = isBGMEnabled ? "Music On" : "Music Off";
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[BGM UI] 버튼 텍스트 업데이트 실패: {ex.Message}");
                // 기본 텍스트로 설정
                buttonText.text = "Music";
            }
        }
        #endregion
    }
} 
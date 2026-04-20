using UnityEngine;
using UnityEngine.UI;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.Gui
{
    /// <summary>
    /// 화면 좌하단 액티브 스킬 HUD (Y/R/G/H 슬롯)
    /// 쿨다운은 회색 오버레이가 위→아래로 빠지는 fill 애니메이션으로 표시 (물 차오르는 효과)
    /// </summary>
    public class ActiveSkillHUD : MonoBehaviour
    {
        private static ActiveSkillHUD _instance;
        public static ActiveSkillHUD Instance => _instance;

        // 슬롯 인덱스: 0=Y, 1=R, 2=G, 3=H, 4=M2(휠윈드)
        private SlotUI[] _slots;
        private Canvas _canvas;
        private RectTransform _containerRt;

        // 슬롯별 스킬 ID → 아이콘명 매핑 (Y슬롯: 직업, R/G: 무기 스킬)
        private static readonly string[] YJobIds  = { "Berserker", "Tanker", "Archer", "Rogue", "Mage", "Paladin", "Producer" };
        private static readonly string[] YIconNames = { "Berserker_unlock", "Tanker_unlock", "Archer_unlock", "Rogue_unlock", "Mage_unlock", "Paladin_unlock", "craft_unlock" };
        private static readonly string[] RSkillIds = { "crossbow_Step6_expert", "bow_Step6_critboost", "staff_Step6_dual_cast" };
        private static readonly string[] RIconNames = { "crossbow_unlock", "bow_unlock", "staff_unlock" };
        private static readonly string[] GSkillIds = {
            "sword_step5_finalcut", "knife_step9_assassin_heart",
            "spear_Step5_penetrate", "polearm_step5_king",
            "defense_Step6_mind", "mace_Step7_guardian_heart"
        };
        private static readonly string[] GIconNames = {
            "sword_unlock", "dagger_unlock",
            "spear_unlock", "polearm_unlock",
            "defense_unlock", "defense_unlock"
        };
        private static readonly string[] HSkillIds = {
            "knife_step10_stack_explosion",
            "crossbow_ice_breath",
            "sword_step5_defswitch", "spear_Step5_combo",
            "mace_Step7_fury_hammer", "staff_Step6_heal",
            "bow_Step6_arrow_rain"
        };
        private static readonly string[] HIconNames = {
            "attack_unlock",
            "ranged_unlock",
            "defense_unlock", "attack_unlock",
            "mace_unlock", "ranged_unlock",
            "ranged_unlock"
        };
        // M2 슬롯: 휠윈드 전용
        private static readonly string[] M2SkillIds  = { "polearm_step6_whirlwind" };
        private static readonly string[] M2IconNames = { "attack_unlock" };

        // HUD 슬롯 정보
        private static readonly string[] SlotKeys   = { "Y", "R", "G", "H", "M2" };

        // Y슬롯 디버그 로그 (1회만)
        private bool _ySlotDebugLogged = false;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            BuildHUD();
        }

        private void BuildHUD()
        {
            // Screen-space Overlay Canvas
            var canvasGO = new GameObject("ActiveSkillHUDCanvas");
            canvasGO.transform.SetParent(transform, false);
            _canvas = canvasGO.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.sortingOrder = 200;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            // 슬롯 컨테이너 - 좌하단 앵커
            var containerGO = new GameObject("SlotContainer");
            containerGO.transform.SetParent(canvasGO.transform, false);
            _containerRt = containerGO.AddComponent<RectTransform>();
            _containerRt.anchorMin = new Vector2(0f, 0f);
            _containerRt.anchorMax = new Vector2(0f, 0f);
            _containerRt.pivot = new Vector2(0f, 0f);
            _containerRt.anchoredPosition = new Vector2(
                SkillTreeConfig.HudPosX?.Value ?? 315,
                SkillTreeConfig.HudPosY?.Value ?? 110);

            var layout = containerGO.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 16f;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.childAlignment = TextAnchor.LowerLeft;

            var fitter = containerGO.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            _slots = new SlotUI[5];
            for (int i = 0; i < 5; i++)
            {
                _slots[i] = CreateSlot(containerGO.transform, SlotKeys[i]);
                _slots[i].Root?.SetActive(false); // 초기 숨김 (메인메뉴/캐릭선택 화면에서 빈 박스 방지)
            }
        }

        private SlotUI CreateSlot(Transform parent, string key)
        {
            var go = new GameObject("Slot_" + key);
            go.transform.SetParent(parent, false);

            var root = go.AddComponent<RectTransform>();
            root.sizeDelta = new Vector2(62f, 96f);

            var layoutElem = go.AddComponent<LayoutElement>();
            layoutElem.preferredWidth = 62f;
            layoutElem.preferredHeight = 96f;

            // 슬롯 배경
            var bg = new GameObject("Bg");
            bg.transform.SetParent(go.transform, false);
            var bgImg = bg.AddComponent<Image>();
            bgImg.color = new Color(0f, 0f, 0f, 0.55f);
            bgImg.raycastTarget = false;
            var bgRt = bg.GetComponent<RectTransform>();
            bgRt.anchorMin = new Vector2(0.5f, 0.5f);
            bgRt.anchorMax = new Vector2(0.5f, 0.5f);
            bgRt.pivot = new Vector2(0.5f, 0.5f);
            bgRt.anchoredPosition = new Vector2(0f, 10f);
            bgRt.sizeDelta = new Vector2(60f, 60f);

            // 황금색 테두리 (아이콘보다 4px 큰 66×66 → 테두리 2px만 보임)
            var borderGO = new GameObject("Border");
            borderGO.transform.SetParent(go.transform, false);
            var borderImg = borderGO.AddComponent<Image>();
            borderImg.color = new Color(1f, 0.8f, 0.1f, 1f);
            borderImg.raycastTarget = false;
            var borderRt = borderGO.GetComponent<RectTransform>();
            borderRt.anchorMin = new Vector2(0.5f, 0.5f);
            borderRt.anchorMax = new Vector2(0.5f, 0.5f);
            borderRt.pivot = new Vector2(0.5f, 0.5f);
            borderRt.anchoredPosition = new Vector2(0f, 10f);
            borderRt.sizeDelta = new Vector2(66f, 66f);

            // 스킬 아이콘
            var iconGO = new GameObject("Icon");
            iconGO.transform.SetParent(go.transform, false);
            var iconImg = iconGO.AddComponent<Image>();
            iconImg.raycastTarget = false;
            iconImg.color = Color.white;
            var iconRt = iconGO.GetComponent<RectTransform>();
            iconRt.anchorMin = new Vector2(0.5f, 0.5f);
            iconRt.anchorMax = new Vector2(0.5f, 0.5f);
            iconRt.pivot = new Vector2(0.5f, 0.5f);
            iconRt.anchoredPosition = new Vector2(0f, 10f);
            iconRt.sizeDelta = new Vector2(62f, 62f);

            // 쿨다운 오버레이 (검정 반투명 강화, fillOrigin=Top → 위→아래 순으로 빠짐)
            var overlayGO = new GameObject("CooldownOverlay");
            overlayGO.transform.SetParent(go.transform, false);
            var overlayImg = overlayGO.AddComponent<Image>();
            overlayImg.raycastTarget = false;
            overlayImg.color = new Color(0f, 0f, 0f, 0.88f);
            overlayImg.type = Image.Type.Filled;
            overlayImg.fillMethod = Image.FillMethod.Vertical;
            overlayImg.fillOrigin = (int)Image.OriginVertical.Top;
            overlayImg.fillAmount = 0f;
            var overlayRt = overlayGO.GetComponent<RectTransform>();
            overlayRt.anchorMin = new Vector2(0.5f, 0.5f);
            overlayRt.anchorMax = new Vector2(0.5f, 0.5f);
            overlayRt.pivot = new Vector2(0.5f, 0.5f);
            overlayRt.anchoredPosition = new Vector2(0f, 10f);
            overlayRt.sizeDelta = new Vector2(62f, 62f);

            // 5초 이하 카운트다운 텍스트 (아이콘 중앙, 애니메이션용)
            var countGO = new GameObject("CountdownText");
            countGO.transform.SetParent(go.transform, false);
            var countText = countGO.AddComponent<Text>();
            countText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            countText.fontSize = 24;
            countText.fontStyle = FontStyle.Bold;
            countText.color = Color.white;
            countText.alignment = TextAnchor.MiddleCenter;
            countText.raycastTarget = false;
            countText.text = "";
            var countRt = countGO.GetComponent<RectTransform>();
            countRt.anchorMin = new Vector2(0.5f, 0.5f);
            countRt.anchorMax = new Vector2(0.5f, 0.5f);
            countRt.pivot = new Vector2(0.5f, 0.5f);
            countRt.anchoredPosition = new Vector2(0f, 10f);
            countRt.sizeDelta = new Vector2(62f, 62f);
            countGO.SetActive(false);

            // 키 레이블 (아이콘 아래)
            var keyGO = new GameObject("KeyLabel");
            keyGO.transform.SetParent(go.transform, false);
            var keyText = keyGO.AddComponent<Text>();
            keyText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            keyText.fontSize = 28;
            keyText.fontStyle = FontStyle.Bold;
            keyText.color = new Color(1f, 0.9f, 0.2f);
            keyText.alignment = TextAnchor.MiddleCenter;
            keyText.raycastTarget = false;
            keyText.text = key;
            var keyRt = keyGO.GetComponent<RectTransform>();
            keyRt.anchorMin = new Vector2(0.5f, 0f);
            keyRt.anchorMax = new Vector2(0.5f, 0f);
            keyRt.pivot = new Vector2(0.5f, 0f);
            keyRt.anchoredPosition = new Vector2(0f, -12f);
            keyRt.sizeDelta = new Vector2(62f, 32f);
            keyGO.transform.SetAsLastSibling();

            var slot = new SlotUI
            {
                Root = go,
                IconRt = iconRt,
                IconImage = iconImg,
                BorderImage = borderImg,
                CooldownOverlay = overlayImg,
                CountdownText = countText,
                KeyLabelText = keyText
            };

            // Y슬롯: 버서커 패시브 쿨타임 서브 아이콘 생성
            if (key == "Y")
            {
                var subRoot = new GameObject("PassiveSubIcon");
                subRoot.transform.SetParent(go.transform, false);
                var subRt = subRoot.AddComponent<RectTransform>();
                subRt.anchorMin = new Vector2(0.5f, 0.5f);
                subRt.anchorMax = new Vector2(0.5f, 0.5f);
                subRt.pivot = new Vector2(0.5f, 0.5f);
                subRt.anchoredPosition = new Vector2(-20f, 30f);
                subRt.sizeDelta = new Vector2(22f, 22f);

                // 서브 아이콘 배경
                var subIconGO = new GameObject("SubIconBg");
                subIconGO.transform.SetParent(subRoot.transform, false);
                var subIconImg = subIconGO.AddComponent<Image>();
                subIconImg.color = new Color(0.2f, 0.2f, 0.2f, 0.85f);
                subIconImg.raycastTarget = false;
                var subIconRt = subIconGO.GetComponent<RectTransform>();
                subIconRt.anchorMin = Vector2.zero;
                subIconRt.anchorMax = Vector2.one;
                subIconRt.offsetMin = Vector2.zero;
                subIconRt.offsetMax = Vector2.zero;

                // 서브 쿨타임 오버레이 (위→아래)
                var subOverlayGO = new GameObject("PassiveOverlay");
                subOverlayGO.transform.SetParent(subRoot.transform, false);
                var subOverlay = subOverlayGO.AddComponent<Image>();
                subOverlay.raycastTarget = false;
                subOverlay.color = new Color(0f, 0f, 0f, 0.85f);
                subOverlay.type = Image.Type.Filled;
                subOverlay.fillMethod = Image.FillMethod.Vertical;
                subOverlay.fillOrigin = (int)Image.OriginVertical.Top;
                subOverlay.fillAmount = 0f;
                var subOverlayRt = subOverlayGO.GetComponent<RectTransform>();
                subOverlayRt.anchorMin = Vector2.zero;
                subOverlayRt.anchorMax = Vector2.one;
                subOverlayRt.offsetMin = Vector2.zero;
                subOverlayRt.offsetMax = Vector2.zero;

                // 서브 카운트다운 텍스트
                var subCountGO = new GameObject("PassiveCountdown");
                subCountGO.transform.SetParent(subRoot.transform, false);
                var subCount = subCountGO.AddComponent<Text>();
                subCount.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                subCount.fontSize = 12;
                subCount.fontStyle = FontStyle.Bold;
                subCount.color = Color.white;
                subCount.alignment = TextAnchor.MiddleCenter;
                subCount.raycastTarget = false;
                subCount.text = "";
                var subCountRt = subCountGO.GetComponent<RectTransform>();
                subCountRt.anchorMin = Vector2.zero;
                subCountRt.anchorMax = Vector2.one;
                subCountRt.offsetMin = Vector2.zero;
                subCountRt.offsetMax = Vector2.zero;

                subRoot.SetActive(false);

                slot.PassiveSubRoot = subRoot;
                slot.PassiveSubOverlay = subOverlay;
                slot.PassiveSubCountdown = subCount;

                // Y슬롯: 버프 수신자용 제작 전문가 아이콘 (직업 아이콘 왼쪽)
                var buffSubRoot = new GameObject("ProducerBuffSubIcon");
                buffSubRoot.transform.SetParent(go.transform, false);
                var buffSubRt = buffSubRoot.AddComponent<RectTransform>();
                buffSubRt.anchorMin = new Vector2(0.5f, 0.5f);
                buffSubRt.anchorMax = new Vector2(0.5f, 0.5f);
                buffSubRt.pivot = new Vector2(0.5f, 0.5f);
                buffSubRt.anchoredPosition = new Vector2(-55f, 10f);
                buffSubRt.sizeDelta = new Vector2(28f, 28f);

                // 배경
                var buffBgGO = new GameObject("Bg");
                buffBgGO.transform.SetParent(buffSubRoot.transform, false);
                var buffBgImg = buffBgGO.AddComponent<Image>();
                buffBgImg.color = new Color(0.2f, 0.2f, 0.2f, 0.85f);
                buffBgImg.raycastTarget = false;
                var buffBgRt = buffBgGO.GetComponent<RectTransform>();
                buffBgRt.anchorMin = Vector2.zero;
                buffBgRt.anchorMax = Vector2.one;
                buffBgRt.offsetMin = Vector2.zero;
                buffBgRt.offsetMax = Vector2.zero;

                // 아이콘
                var buffIconGO = new GameObject("Icon");
                buffIconGO.transform.SetParent(buffSubRoot.transform, false);
                var buffIconImg = buffIconGO.AddComponent<Image>();
                buffIconImg.raycastTarget = false;
                buffIconImg.color = Color.white;
                var buffIconRt = buffIconGO.GetComponent<RectTransform>();
                buffIconRt.anchorMin = Vector2.zero;
                buffIconRt.anchorMax = Vector2.one;
                buffIconRt.offsetMin = Vector2.zero;
                buffIconRt.offsetMax = Vector2.zero;

                // 오버레이 (fillOrigin=Top → 버프 지속시간 비율)
                var buffOverlayGO = new GameObject("Overlay");
                buffOverlayGO.transform.SetParent(buffSubRoot.transform, false);
                var buffOverlay = buffOverlayGO.AddComponent<Image>();
                buffOverlay.raycastTarget = false;
                buffOverlay.color = new Color(0f, 0f, 0f, 0.85f);
                buffOverlay.type = Image.Type.Filled;
                buffOverlay.fillMethod = Image.FillMethod.Vertical;
                buffOverlay.fillOrigin = (int)Image.OriginVertical.Top;
                buffOverlay.fillAmount = 0f;
                var buffOverlayRt = buffOverlayGO.GetComponent<RectTransform>();
                buffOverlayRt.anchorMin = Vector2.zero;
                buffOverlayRt.anchorMax = Vector2.one;
                buffOverlayRt.offsetMin = Vector2.zero;
                buffOverlayRt.offsetMax = Vector2.zero;

                // 카운트다운 텍스트
                var buffCountGO = new GameObject("Countdown");
                buffCountGO.transform.SetParent(buffSubRoot.transform, false);
                var buffCount = buffCountGO.AddComponent<Text>();
                buffCount.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                buffCount.fontSize = 10;
                buffCount.fontStyle = FontStyle.Bold;
                buffCount.color = Color.white;
                buffCount.alignment = TextAnchor.MiddleCenter;
                buffCount.raycastTarget = false;
                buffCount.text = "";
                var buffCountRt = buffCountGO.GetComponent<RectTransform>();
                buffCountRt.anchorMin = Vector2.zero;
                buffCountRt.anchorMax = Vector2.one;
                buffCountRt.offsetMin = Vector2.zero;
                buffCountRt.offsetMax = Vector2.zero;

                buffSubRoot.SetActive(false);

                slot.ProducerBuffSubRoot = buffSubRoot;
                slot.ProducerBuffSubOverlay = buffOverlay;
                slot.ProducerBuffSubCountdown = buffCount;
                slot.ProducerBuffSubIconImage = buffIconImg;

                // Y슬롯: 탱커 반사 버프 지속시간 서브 아이콘 (아이콘 우상단, +20, 30)
                var reflectSubRoot = new GameObject("TankerReflectSubIcon");
                reflectSubRoot.transform.SetParent(go.transform, false);
                var reflectSubRt = reflectSubRoot.AddComponent<RectTransform>();
                reflectSubRt.anchorMin = new Vector2(0.5f, 0.5f);
                reflectSubRt.anchorMax = new Vector2(0.5f, 0.5f);
                reflectSubRt.pivot = new Vector2(0.5f, 0.5f);
                reflectSubRt.anchoredPosition = new Vector2(20f, 30f);  // 우상단 (PassiveSubIcon은 -20f, 30f 좌상단)
                reflectSubRt.sizeDelta = new Vector2(22f, 22f);

                var reflectBgGO = new GameObject("SubIconBg");
                reflectBgGO.transform.SetParent(reflectSubRoot.transform, false);
                var reflectBgImg = reflectBgGO.AddComponent<Image>();
                reflectBgImg.color = new Color(0.2f, 0.1f, 0f, 0.85f);
                reflectBgImg.raycastTarget = false;
                var reflectBgRt = reflectBgGO.GetComponent<RectTransform>();
                reflectBgRt.anchorMin = Vector2.zero;
                reflectBgRt.anchorMax = Vector2.one;
                reflectBgRt.offsetMin = Vector2.zero;
                reflectBgRt.offsetMax = Vector2.zero;

                var reflectOverlayGO = new GameObject("ReflectOverlay");
                reflectOverlayGO.transform.SetParent(reflectSubRoot.transform, false);
                var reflectOverlay = reflectOverlayGO.AddComponent<Image>();
                reflectOverlay.raycastTarget = false;
                reflectOverlay.color = new Color(1f, 0.4f, 0f, 0.75f);  // 주황색: 반사 버프
                reflectOverlay.type = Image.Type.Filled;
                reflectOverlay.fillMethod = Image.FillMethod.Vertical;
                reflectOverlay.fillOrigin = (int)Image.OriginVertical.Top;
                reflectOverlay.fillAmount = 1f;
                var reflectOverlayRt = reflectOverlayGO.GetComponent<RectTransform>();
                reflectOverlayRt.anchorMin = Vector2.zero;
                reflectOverlayRt.anchorMax = Vector2.one;
                reflectOverlayRt.offsetMin = Vector2.zero;
                reflectOverlayRt.offsetMax = Vector2.zero;

                var reflectCountGO = new GameObject("ReflectCountdown");
                reflectCountGO.transform.SetParent(reflectSubRoot.transform, false);
                var reflectCount = reflectCountGO.AddComponent<Text>();
                reflectCount.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                reflectCount.fontSize = 12;
                reflectCount.fontStyle = FontStyle.Bold;
                reflectCount.color = Color.white;
                reflectCount.alignment = TextAnchor.MiddleCenter;
                reflectCount.raycastTarget = false;
                reflectCount.text = "";
                var reflectCountRt = reflectCountGO.GetComponent<RectTransform>();
                reflectCountRt.anchorMin = Vector2.zero;
                reflectCountRt.anchorMax = Vector2.one;
                reflectCountRt.offsetMin = Vector2.zero;
                reflectCountRt.offsetMax = Vector2.zero;

                reflectSubRoot.SetActive(false);

                slot.TankerReflectSubRoot = reflectSubRoot;
                slot.TankerReflectSubOverlay = reflectOverlay;
                slot.TankerReflectSubCountdown = reflectCount;
            }

            // M2슬롯: 휠윈드 지속시간 서브 아이콘 (아이콘 왼쪽 위)
            if (key == "M2")
            {
                var durRoot = new GameObject("DurationSubIcon");
                durRoot.transform.SetParent(go.transform, false);
                var durRt = durRoot.AddComponent<RectTransform>();
                durRt.anchorMin = new Vector2(0.5f, 0.5f);
                durRt.anchorMax = new Vector2(0.5f, 0.5f);
                durRt.pivot = new Vector2(0.5f, 0.5f);
                durRt.anchoredPosition = new Vector2(-20f, 30f);
                durRt.sizeDelta = new Vector2(22f, 22f);

                var durBgGO = new GameObject("DurationBg");
                durBgGO.transform.SetParent(durRoot.transform, false);
                var durBgImg = durBgGO.AddComponent<Image>();
                durBgImg.color = new Color(0.2f, 0.2f, 0.2f, 0.85f);
                durBgImg.raycastTarget = false;
                var durBgRt = durBgGO.GetComponent<RectTransform>();
                durBgRt.anchorMin = Vector2.zero;
                durBgRt.anchorMax = Vector2.one;
                durBgRt.offsetMin = Vector2.zero;
                durBgRt.offsetMax = Vector2.zero;

                var durOverlayGO = new GameObject("DurationOverlay");
                durOverlayGO.transform.SetParent(durRoot.transform, false);
                var durOverlay = durOverlayGO.AddComponent<Image>();
                durOverlay.raycastTarget = false;
                durOverlay.color = new Color(0f, 0.6f, 1f, 0.75f);
                durOverlay.type = Image.Type.Filled;
                durOverlay.fillMethod = Image.FillMethod.Vertical;
                durOverlay.fillOrigin = (int)Image.OriginVertical.Top;
                durOverlay.fillAmount = 1f;
                var durOverlayRt = durOverlayGO.GetComponent<RectTransform>();
                durOverlayRt.anchorMin = Vector2.zero;
                durOverlayRt.anchorMax = Vector2.one;
                durOverlayRt.offsetMin = Vector2.zero;
                durOverlayRt.offsetMax = Vector2.zero;

                var durCountGO = new GameObject("DurationCountdown");
                durCountGO.transform.SetParent(durRoot.transform, false);
                var durCount = durCountGO.AddComponent<Text>();
                durCount.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                durCount.fontSize = 12;
                durCount.fontStyle = FontStyle.Bold;
                durCount.color = Color.white;
                durCount.alignment = TextAnchor.MiddleCenter;
                durCount.raycastTarget = false;
                durCount.text = "";
                var durCountRt = durCountGO.GetComponent<RectTransform>();
                durCountRt.anchorMin = Vector2.zero;
                durCountRt.anchorMax = Vector2.one;
                durCountRt.offsetMin = Vector2.zero;
                durCountRt.offsetMax = Vector2.zero;

                durRoot.SetActive(false);

                slot.DurationSubRoot = durRoot;
                slot.DurationSubOverlay = durOverlay;
                slot.DurationSubCountdown = durCount;
            }

            return slot;
        }

        private float _updateTimer = 0f;
        private const float UPDATE_INTERVAL = 1.0f; // 초 단위 폴링 기본 간격
        // 2단계 적응형 폴링: 쿨타임 없으면 완전 중지, >60s이면 60s 간격, ≤60s이면 1s 간격
        private bool _cooldownActive = false;
        private float _currentInterval = 1f; // 현재 폴링 간격 (60f or 1f)
        private bool _playerWasNull = true;  // 플레이어 스폰 감지용

        // 드래그 이동
        private bool _isDragging = false;
        private Vector2 _dragOffset;

        /// <summary>스킬 변경 시 즉시 슬롯 갱신 (SkillTreeManager.SetSkillLevel에서 호출)</summary>
        public void RefreshSlots()
        {
            _cooldownActive = true;          // 강제 폴링 활성화 (쿨타임 없어도 슬롯 갱신)
            _updateTimer = _currentInterval; // 다음 Update()에서 즉시 갱신
        }

        /// <summary>스킬 사용 시 ActiveSkillCooldownRegistry.SetCooldown에서 호출</summary>
        public void OnCooldownStarted()
        {
            _cooldownActive = true;
            _updateTimer = _currentInterval; // 다음 프레임에서 즉시 1회 갱신
        }

        /// <summary>Config로 쿨타임 변경 시 RecalculateCooldown에서 호출 - interval 재계산</summary>
        public void OnCooldownChanged()
        {
            if (!_cooldownActive) return;
            float minRemaining = SkillTree.ActiveSkillCooldownRegistry.GetMinRemaining();
            if (minRemaining <= 0f)
            {
                _cooldownActive = false;
            }
            else
            {
                _currentInterval = 1f;
                _updateTimer = _currentInterval; // 즉시 갱신
            }
        }

        private void Update()
        {
            HandleDrag();

            // 플레이어 스폰 감지: null→non-null 전환 시 HUD 강제 갱신
            // Game.instance != null 조건으로 캐릭터선택/메인메뉴 프리뷰 캐릭터 제외
            bool playerExists = Player.m_localPlayer != null && Game.instance != null;
            if (playerExists && _playerWasNull)
            {
                _playerWasNull = false;
                RefreshSlots();
            }
            else if (!playerExists && !_playerWasNull)
            {
                _playerWasNull = true;
                _cooldownActive = false;  // 폴링 중지 (로그아웃 후 CPU 낭비 방지)
                foreach (var s in _slots)
                    s?.Root?.SetActive(false);
            }

            // 매 프레임: 스케일 + 카운트다운 애니메이션
            UpdateAnimations();

            // 쿨타임 없으면 즉시 종료 (폴링 0)
            if (!_cooldownActive) return;

            // 적응형 간격 폴링 (>60s: 60초, ≤60s: 1초)
            _updateTimer += Time.deltaTime;
            if (_updateTimer < _currentInterval) return;
            _updateTimer = 0f;

            // 안전가드: UpdateSlot NullRef 방지
            if (Player.m_localPlayer == null) return;

            // Config 위치 실시간 반영 (드래그 중 제외)
            if (!_isDragging && _containerRt != null)
            {
                _containerRt.anchoredPosition = new Vector2(
                    SkillTreeConfig.HudPosX?.Value ?? 315,
                    SkillTreeConfig.HudPosY?.Value ?? 110);
            }

            var mgr = SkillTreeManager.Instance;
            if (mgr == null) return;

            UpdateSlot(0, "Y", mgr);
            UpdateSlot(1, "R", mgr);
            UpdateSlot(2, "G", mgr);
            UpdateSlot(3, "H", mgr);
            UpdateSlot(4, "M2", mgr);

            // 갱신 후 다음 폴링 간격 재계산
            float minRemaining = SkillTree.ActiveSkillCooldownRegistry.GetMinRemaining();
            if (minRemaining <= 0f)
                _cooldownActive = false;          // 모든 쿨타임 종료 → Idle
            else
                _currentInterval = 1f;            // 쿨타임 있으면 항상 1초 간격
        }

        // =========================================================
        // 애니메이션 (매 프레임)
        // =========================================================

        private void UpdateAnimations()
        {
            foreach (var slot in _slots)
            {
                if (slot == null) continue;
                UpdateScaleAnim(slot);
            }

            // M2슬롯 휠윈드 지속시간 서브 아이콘 (매 프레임 갱신)
            UpdateWhirlwindDurationSub();

            // Y슬롯 탱커 반사 버프 지속시간 서브 아이콘 (매 프레임 갱신)
            UpdateTankerReflectSub();
        }

        private void UpdateTankerReflectSub()
        {
            var ySlot = _slots.Length > 0 ? _slots[0] : null;
            if (ySlot?.TankerReflectSubRoot == null) return;
            if (Player.m_localPlayer == null) return;

            float rem = TankerReflect.GetTankerReflectRemaining(Player.m_localPlayer);
            float total = TankerReflect.GetTankerReflectTotalDuration(Player.m_localPlayer);

            if (rem <= 0f || total <= 0f)
            {
                ySlot.TankerReflectSubRoot.SetActive(false);
                return;
            }

            ySlot.TankerReflectSubRoot.SetActive(true);
            ySlot.TankerReflectSubOverlay.fillAmount = rem / total;
            ySlot.TankerReflectSubCountdown.text = Mathf.CeilToInt(rem).ToString();
        }

        private void UpdateWhirlwindDurationSub()
        {
            var m2Slot = _slots.Length > 4 ? _slots[4] : null;
            if (m2Slot?.DurationSubRoot == null) return;

            float start = SkillEffect.whirlwindDurationStart;
            float max = SkillEffect.whirlwindDurationMax;

            if (start < 0f || max <= 0f)
            {
                m2Slot.DurationSubRoot.SetActive(false);
                return;
            }

            float remaining = max - (Time.time - start);
            if (remaining <= 0f)
            {
                m2Slot.DurationSubRoot.SetActive(false);
                return;
            }

            m2Slot.DurationSubRoot.SetActive(true);
            m2Slot.DurationSubOverlay.fillAmount = remaining / max;
            m2Slot.DurationSubCountdown.text = Mathf.CeilToInt(remaining).ToString();
        }

        private void UpdateScaleAnim(SlotUI slot)
        {
            if (!slot.ScaleAnimActive)
            {
                slot.IconRt.localScale = Vector3.one;
                slot.IconRt.anchoredPosition = new Vector2(0f, 10f);
                slot.IconImage.color = Color.white;
                slot.BorderImage.gameObject.SetActive(true);
                return;
            }
            float elapsed = Time.time - slot.ScaleAnimStart;
            const float phase1 = 0.4f, phase2 = 0.4f, total = 0.8f;
            const float halfH = 31f; // 62 / 2
            if (elapsed >= total)
            {
                slot.ScaleAnimActive = false;
                slot.IconRt.localScale = Vector3.one;
                slot.IconRt.anchoredPosition = new Vector2(0f, 10f);
                slot.BorderImage.gameObject.SetActive(true);
                return;
            }
            slot.BorderImage.gameObject.SetActive(false);
            float scale = elapsed < phase1
                ? Mathf.Lerp(1f, 1.5f, elapsed / phase1)
                : Mathf.Lerp(1.5f, 1f, (elapsed - phase1) / phase2);
            slot.IconRt.localScale = new Vector3(scale, scale, 1f);
            slot.IconRt.anchoredPosition = new Vector2(0f, 10f + (scale - 1f) * halfH);
        }

        // =========================================================
        // 슬롯 폴링 (0.05초마다)
        // =========================================================

        private void UpdateSlot(int idx, string slotKey, SkillTreeManager mgr)
        {
            var slot = _slots[idx];
            if (slot == null) return;

            string iconName = null;

            switch (slotKey)
            {
                case "Y":
                    for (int i = 0; i < YJobIds.Length; i++)
                    {
                        if (mgr.GetSkillLevel(YJobIds[i]) > 0)
                        {
                            iconName = YIconNames[i];
                            break;
                        }
                    }
                    if (iconName == null && !_ySlotDebugLogged)
                    {
                        _ySlotDebugLogged = true;
                        Plugin.Log.LogDebug(
                            $"[ActiveSkillHUD] Y slot: Berserker={mgr.GetSkillLevel("Berserker")}" +
                            $", Tanker={mgr.GetSkillLevel("Tanker")}" +
                            $", Archer={mgr.GetSkillLevel("Archer")}" +
                            $", Rogue={mgr.GetSkillLevel("Rogue")}" +
                            $", Mage={mgr.GetSkillLevel("Mage")}" +
                            $", Paladin={mgr.GetSkillLevel("Paladin")}");
                    }
                    break;
                case "R":
                    for (int i = 0; i < RSkillIds.Length; i++)
                    {
                        if (mgr.GetSkillLevel(RSkillIds[i]) > 0)
                        {
                            iconName = RIconNames[i];
                            break;
                        }
                    }
                    break;
                case "G":
                    for (int i = 0; i < GSkillIds.Length; i++)
                    {
                        if (mgr.GetSkillLevel(GSkillIds[i]) > 0)
                        {
                            iconName = GIconNames[i];
                            break;
                        }
                    }
                    break;
                case "H":
                    for (int i = 0; i < HSkillIds.Length; i++)
                    {
                        if (mgr.GetSkillLevel(HSkillIds[i]) > 0)
                        {
                            iconName = HIconNames[i];
                            break;
                        }
                    }
                    break;
                case "M2":
                    for (int i = 0; i < M2SkillIds.Length; i++)
                    {
                        if (mgr.GetSkillLevel(M2SkillIds[i]) > 0)
                        {
                            iconName = M2IconNames[i];
                            break;
                        }
                    }
                    break;
            }

            if (iconName == null)
            {
                slot.Root.SetActive(false);
                return;
            }

            slot.Root.SetActive(true);

            // 아이콘 갱신 (변경 시에만)
            if (slot.LastIconName != iconName)
            {
                slot.LastIconName = iconName;
                slot.IconImage.sprite = LoadIcon(iconName);
            }

            // 키 레이블 업데이트 (설정 키 반영)
            slot.KeyLabelText.text = GetConfiguredKeyLabel(slotKey);

            // 쿨다운 오버레이 업데이트
            float remaining = ActiveSkillCooldownRegistry.GetCooldownRemaining(slotKey);
            float ratio = ActiveSkillCooldownRegistry.GetCooldownRatio(slotKey);

            // 쿨타임 완료 감지 → 스케일 애니메이션 트리거
            if (slot.PrevRatio > 0f && ratio == 0f)
            {
                slot.ScaleAnimActive = true;
                slot.ScaleAnimStart = Time.time;
            }
            slot.PrevRatio = ratio;

            bool isCooldown = ratio > 0f;
            slot.CooldownOverlay.gameObject.SetActive(isCooldown);
            if (isCooldown) slot.CooldownOverlay.fillAmount = ratio;
            slot.IconImage.color = Color.white;

            // 쿨타임 전체 카운트다운 (아이콘 중앙 고정 표시)
            if (remaining > 0f)
            {
                slot.CountdownText.text = Mathf.CeilToInt(remaining).ToString();
                slot.CountdownText.gameObject.SetActive(true);
            }
            else
            {
                slot.CountdownText.text = "";
                slot.CountdownText.gameObject.SetActive(false);
            }

            // Y슬롯: 패시브/버프 서브 아이콘 업데이트
            if (slotKey == "Y")
            {
                bool isProducer = iconName == "craft_unlock";

                // PassiveSubRoot: 시전자 버프 시간 OR 버서커 패시브 쿨타임
                if (slot.PassiveSubRoot != null)
                {
                    if (isProducer)
                    {
                        float rem = ProducerSkills.GetBuffRemainingForPlayer(Player.m_localPlayer);
                        float total = ProducerSkills.GetBuffTotalDuration();
                        bool buffActive = rem > 0f && total > 0f;
                        slot.PassiveSubRoot.SetActive(buffActive);
                        if (buffActive)
                        {
                            slot.PassiveSubOverlay.fillAmount = rem / total;
                            slot.PassiveSubCountdown.text = Mathf.CeilToInt(rem).ToString();
                        }
                    }
                    else if (iconName == "Berserker_unlock")
                    {
                        float passiveRatio = ActiveSkillCooldownRegistry.GetCooldownRatio("passive_berserker");
                        float passiveRemaining = ActiveSkillCooldownRegistry.GetCooldownRemaining("passive_berserker");
                        bool passiveOnCooldown = passiveRatio > 0f || passiveRemaining > 0f;
                        slot.PassiveSubRoot.SetActive(passiveOnCooldown);
                        if (passiveOnCooldown)
                        {
                            slot.PassiveSubOverlay.fillAmount = passiveRatio;
                            if (passiveRemaining > 60f)
                                slot.PassiveSubCountdown.text = Mathf.CeilToInt(passiveRemaining / 60f) + "m";
                            else if (passiveRemaining > 0f)
                                slot.PassiveSubCountdown.text = Mathf.CeilToInt(passiveRemaining).ToString();
                            else
                                slot.PassiveSubCountdown.text = "";
                        }
                    }
                    else
                    {
                        slot.PassiveSubRoot.SetActive(false);
                    }
                }

                // ProducerBuffSubRoot: 비시전자가 버프를 받은 경우 왼쪽에 craft_unlock 아이콘 표시
                if (slot.ProducerBuffSubRoot != null)
                {
                    float rem = !isProducer ? ProducerSkills.GetBuffRemainingForPlayer(Player.m_localPlayer) : 0f;
                    float total = ProducerSkills.GetBuffTotalDuration();
                    bool show = rem > 0f && total > 0f;
                    slot.ProducerBuffSubRoot.SetActive(show);
                    if (show)
                    {
                        if (slot.ProducerBuffSubIconImage.sprite == null)
                            slot.ProducerBuffSubIconImage.sprite = LoadIcon("craft_unlock");
                        slot.ProducerBuffSubOverlay.fillAmount = rem / total;
                        slot.ProducerBuffSubCountdown.text = Mathf.CeilToInt(rem).ToString();
                    }
                }
            }
        }

        private string GetConfiguredKeyLabel(string defaultKey)
        {
            switch (defaultKey)
            {
                case "Y": return SkillTreeConfig.HotKeyY?.Value ?? "Y";
                case "R": return SkillTreeConfig.HotKeyR?.Value ?? "R";
                case "G": return SkillTreeConfig.HotKeyG?.Value ?? "G";
                case "H": return SkillTreeConfig.HotKeyH?.Value ?? "H";
                case "M2": return "M2";
                default:  return defaultKey;
            }
        }

        private Sprite LoadIcon(string iconName)
        {
            // job_icon 번들 우선
            var jobBundle = Plugin.GetJobIconBundle();
            if (jobBundle != null)
            {
                var sp = jobBundle.LoadAsset<Sprite>(iconName);
                if (sp != null) return sp;
            }

            // skill_node 번들
            var skillBundle = Plugin.GetIconAssetBundle();
            if (skillBundle != null)
            {
                var sp = skillBundle.LoadAsset<Sprite>(iconName);
                if (sp != null) return sp;

                // 폴백: all_skill_unlock
                var fallback = skillBundle.LoadAsset<Sprite>("all_skill_unlock");
                if (fallback != null) return fallback;
            }

            return null;
        }

        private void HandleDrag()
        {
            if (_containerRt == null) return;
            if (Input.GetMouseButtonDown(0) && IsMouseOverHUD())
            {
                _isDragging = true;
                _dragOffset = _containerRt.anchoredPosition - (Vector2)Input.mousePosition;
            }
            if (_isDragging)
            {
                _containerRt.anchoredPosition = (Vector2)Input.mousePosition + _dragOffset;
                if (Input.GetMouseButtonUp(0))
                {
                    _isDragging = false;
                    if (SkillTreeConfig.HudPosX != null)
                        SkillTreeConfig.HudPosX.Value = (int)_containerRt.anchoredPosition.x;
                    if (SkillTreeConfig.HudPosY != null)
                        SkillTreeConfig.HudPosY.Value = (int)_containerRt.anchoredPosition.y;
                }
            }
        }

        private bool IsMouseOverHUD()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 pos = _containerRt.anchoredPosition;
            float w = _containerRt.rect.width;
            float h = _containerRt.rect.height;
            return mousePos.x >= pos.x && mousePos.x <= pos.x + w &&
                   mousePos.y >= pos.y && mousePos.y <= pos.y + h;
        }

        private class SlotUI
        {
            public GameObject Root;
            public RectTransform IconRt;
            public Image IconImage;
            public Image BorderImage;
            public Image CooldownOverlay;
            public Text CountdownText;
            public Text KeyLabelText;
            public string LastIconName;

            // 스케일 애니메이션 (쿨타임 완료 시)
            public float PrevRatio;
            public bool ScaleAnimActive;
            public float ScaleAnimStart;

            // 버서커 패시브 서브 아이콘 (Y슬롯 전용)
            public GameObject PassiveSubRoot;
            public Image PassiveSubOverlay;
            public Text PassiveSubCountdown;

            // 휠윈드 지속시간 서브 아이콘 (M2슬롯 전용, 아이콘 왼쪽 위)
            public GameObject DurationSubRoot;
            public Image DurationSubOverlay;
            public Text DurationSubCountdown;

            // 제작 전문가 버프 수신자 서브 아이콘 (Y슬롯 전용)
            public GameObject ProducerBuffSubRoot;
            public Image ProducerBuffSubOverlay;
            public Text ProducerBuffSubCountdown;
            public Image ProducerBuffSubIconImage;

            // 탱커 반사 버프 지속시간 서브 아이콘 (Y슬롯 전용, 아이콘 우상단)
            public GameObject TankerReflectSubRoot;
            public Image TankerReflectSubOverlay;
            public Text TankerReflectSubCountdown;
        }
    }
}

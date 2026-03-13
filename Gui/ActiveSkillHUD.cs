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

        // 슬롯 인덱스: 0=Y, 1=R, 2=G, 3=H
        private SlotUI[] _slots;
        private Canvas _canvas;
        private RectTransform _containerRt;

        // 슬롯별 스킬 ID → 아이콘명 매핑 (Y슬롯: 직업, R/G: 무기 스킬)
        private static readonly string[] YJobIds  = { "Berserker", "Tanker", "Archer", "Rogue", "Mage", "Paladin" };
        private static readonly string[] YIconNames = { "Berserker_unlock", "Tanker_unlock", "Archer_unlock", "Rogue_unlock", "Mage_unlock", "Paladin_unlock" };
        private static readonly string[] RSkillIds = { "crossbow_Step6_expert", "bow_Step6_critboost", "staff_Step6_dual_cast" };
        private static readonly string[] RIconNames = { "crossbow_unlock", "bow_unlock", "staff_unlock" };
        private static readonly string[] GSkillIds = {
            "sword_step5_finalcut", "knife_step9_assassin_heart",
            "spear_Step5_penetrate", "polearm_step5_king", "mace_Step7_guardian_heart"
        };
        private static readonly string[] GIconNames = {
            "sword_unlock", "dagger_unlock",
            "spear_unlock", "polearm_unlock", "mace_unlock"
        };
        private static readonly string[] HSkillIds = {
            "sword_step5_defswitch", "spear_Step5_combo",
            "mace_Step7_fury_hammer", "staff_Step6_heal"
        };
        private static readonly string[] HIconNames = {
            "defense_unlock", "attack_unlock",
            "attack_unlock", "staff_unlock"
        };

        // HUD 슬롯 정보
        private static readonly string[] SlotKeys   = { "Y", "R", "G", "H" };

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

            _slots = new SlotUI[4];
            for (int i = 0; i < 4; i++)
            {
                _slots[i] = CreateSlot(containerGO.transform, SlotKeys[i]);
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
            }

            return slot;
        }

        private float _updateTimer = 0f;
        private const float UPDATE_INTERVAL = 0.05f;

        // 드래그 이동
        private bool _isDragging = false;
        private Vector2 _dragOffset;

        private void Update()
        {
            HandleDrag();

            // 매 프레임: 스케일 + 카운트다운 애니메이션
            UpdateAnimations();

            // 0.05초마다: 쿨타임/아이콘 폴링
            _updateTimer += Time.deltaTime;
            if (_updateTimer < UPDATE_INTERVAL) return;
            _updateTimer = 0f;

            // 플레이어가 로드되지 않은 경우 HUD 숨김
            if (Player.m_localPlayer == null)
            {
                foreach (var s in _slots)
                    s?.Root?.SetActive(false);
                return;
            }

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

            // Y슬롯: 버서커 패시브 서브 아이콘 업데이트
            if (slotKey == "Y" && slot.PassiveSubRoot != null)
            {
                bool isBerserker = iconName == "Berserker_unlock";
                if (isBerserker)
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
        }

        private string GetConfiguredKeyLabel(string defaultKey)
        {
            switch (defaultKey)
            {
                case "Y": return SkillTreeConfig.HotKeyY?.Value ?? "Y";
                case "R": return SkillTreeConfig.HotKeyR?.Value ?? "R";
                case "G": return SkillTreeConfig.HotKeyG?.Value ?? "G";
                case "H": return SkillTreeConfig.HotKeyH?.Value ?? "H";
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
        }
    }
}

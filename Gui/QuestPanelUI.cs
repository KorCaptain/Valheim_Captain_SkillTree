using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.Gui
{
    /// <summary>
    /// 퀘스트 목록/진행상황/보상 클레임 UI. 스킬트리 UI와 독립적으로
    /// (Tab 아이콘 / Ctrl+J(설정 가능) / 스킬트리 내부 버튼) 어디서든 열고 닫을 수 있는 오버레이 패널.
    /// 비주얼은 메인 스킬트리 패널과 톤을 맞춰 SkillTreeBG(9-slice) + 금테 프레임을 재사용한다.
    /// </summary>
    public static class QuestPanelUI
    {
        private static readonly Color GoldColor = new Color(0.85f, 0.7f, 0.1f, 0.9f);
        private static readonly Color DarkFillColor = new Color(0.04f, 0.04f, 0.12f, 0.88f);
        // SkillTreeBG는 밝은 양피지 톤이라 텍스트/오버레이는 어두운 계열로 대비를 확보한다.
        private static readonly Color LabelTextColor = new Color(0.18f, 0.10f, 0.04f, 1f);
        private static readonly Color ProgressTextColor = new Color(0.55f, 0.08f, 0.08f, 1f);
        private static readonly Color CompletedTextColor = new Color(0.55f, 0.38f, 0f, 1f);
        private static readonly Color ClaimedTextColor = new Color(0.45f, 0.40f, 0.35f, 1f);
        private static readonly Color RewardPreviewTextColor = new Color(0.02f, 0.10f, 0.02f, 1f);
        private const float BoxWidth = 960f; // 기존 800에서 약 20% 증가 (긴 [농사]/[요리]/[낚시] 라벨이 상태 수치와 겹치는 문제 대응)
        private const float BoxHeight = 760f;

        private static GameObject _panelRoot;
        private static RectTransform _content;
        private static AssetBundle _questBgBundle;
        private static QuestTooltipUI _tooltipUI;

        public static bool IsOpen => _panelRoot != null && _panelRoot.activeSelf;

        public static void Toggle()
        {
            if (IsOpen) Close();
            else Open();
        }

        public static void Open()
        {
            EnsureCreated();
            if (_panelRoot == null) return;
            // 스킬트리 UI 등 같은 Canvas의 다른 패널이 나중에 맨 위로 올라와 있을 수 있으므로,
            // 열 때마다 형제 순서를 맨 뒤로 올려서 항상 최상단에 렌더링되도록 보장한다.
            _panelRoot.transform.SetAsLastSibling();
            _panelRoot.SetActive(true);
            Refresh();
        }

        public static void Close()
        {
            // 툴팁은 _panelRoot의 자식이 아니라 별도의 DontDestroyOnLoad 캔버스(QuestTooltipCanvas)에 떠 있어서,
            // 마우스가 실제로 빠져나가지 않고 Ctrl+J/ESC/Tab으로 패널만 비활성화되면 PointerExit이 발생하지 않아
            // 툴팁이 화면에 그대로 남는다. 패널을 닫을 때 항상 함께 닫아준다.
            _tooltipUI?.HideTooltip();
            if (_panelRoot != null) _panelRoot.SetActive(false);
        }

        private static void EnsureCreated()
        {
            if (_panelRoot != null) return;

            var canvas = FindMainCanvas();
            if (canvas == null)
            {
                Plugin.Log.LogWarning("[QuestPanelUI] Canvas를 찾을 수 없어 패널을 생성하지 못했습니다.");
                return;
            }

            _panelRoot = new GameObject("QuestPanelRoot", typeof(RectTransform));
            _panelRoot.transform.SetParent(canvas.transform, false);
            var rootRect = _panelRoot.GetComponent<RectTransform>();
            rootRect.anchorMin = Vector2.zero;
            rootRect.anchorMax = Vector2.one;
            rootRect.offsetMin = Vector2.zero;
            rootRect.offsetMax = Vector2.zero;

            // 배경 블로커 (클릭 차단, 어둡게)
            var blocker = new GameObject("Blocker", typeof(RectTransform), typeof(Image));
            blocker.transform.SetParent(_panelRoot.transform, false);
            var blockerRect = blocker.GetComponent<RectTransform>();
            blockerRect.anchorMin = Vector2.zero;
            blockerRect.anchorMax = Vector2.one;
            blockerRect.offsetMin = Vector2.zero;
            blockerRect.offsetMax = Vector2.zero;
            blocker.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.45f);

            // 본체 패널 — 전용 quest_bg 에셋을 우선 사용하고, 없으면 SkillTreeBG(발헤임풍 9-slice)로 폴백
            var box = new GameObject("Box", typeof(RectTransform), typeof(Image));
            box.transform.SetParent(_panelRoot.transform, false);
            var boxRect = box.GetComponent<RectTransform>();
            boxRect.anchorMin = new Vector2(0.5f, 0.5f);
            boxRect.anchorMax = new Vector2(0.5f, 0.5f);
            boxRect.pivot = new Vector2(0.5f, 0.5f);
            boxRect.sizeDelta = new Vector2(BoxWidth, BoxHeight);
            boxRect.anchoredPosition = Vector2.zero;

            var boxImage = box.GetComponent<Image>();
            Sprite bgSprite = GetQuestBgSprite();
            if (bgSprite == null)
            {
                var uiBundle = Plugin.GetUIAssetBundle();
                if (uiBundle != null) bgSprite = uiBundle.LoadAsset<Sprite>("SkillTreeBG");
            }
            if (bgSprite != null)
            {
                boxImage.sprite = bgSprite;
                boxImage.type = Image.Type.Sliced;
                boxImage.color = Color.white;
            }
            else
            {
                boxImage.color = new Color(0.05f, 0.05f, 0.15f, 0.94f);
            }

            // 제목 — 스킬포인트 표시부와 동일한 금테+다크필 2겹 프레임
            var titleBorder = new GameObject("TitleBorder", typeof(RectTransform), typeof(Image));
            titleBorder.transform.SetParent(box.transform, false);
            var titleBorderRect = titleBorder.GetComponent<RectTransform>();
            titleBorderRect.anchorMin = new Vector2(0.5f, 1f);
            titleBorderRect.anchorMax = new Vector2(0.5f, 1f);
            titleBorderRect.pivot = new Vector2(0.5f, 1f);
            titleBorderRect.sizeDelta = new Vector2(260, 44);
            titleBorderRect.anchoredPosition = new Vector2(0, -14);
            titleBorder.GetComponent<Image>().color = GoldColor;

            var titleFill = new GameObject("TitleFill", typeof(RectTransform), typeof(Image));
            titleFill.transform.SetParent(titleBorder.transform, false);
            var titleFillRect = titleFill.GetComponent<RectTransform>();
            titleFillRect.anchorMin = Vector2.zero;
            titleFillRect.anchorMax = Vector2.one;
            titleFillRect.offsetMin = new Vector2(3, 3);
            titleFillRect.offsetMax = new Vector2(-3, -3);
            titleFill.GetComponent<Image>().color = DarkFillColor;

            var title = CreateText(titleFill.transform, "Title", L.Get("quest_panel_title"), 22, new Color(1f, 0.85f, 0f, 1f), TextAnchor.MiddleCenter);
            title.fontStyle = FontStyle.Bold;

            // 닫기(X) 버튼 — 우상단 고정 앵커
            var closeBtn = CreateSimpleButton(box.transform, "CloseButton", "X",
                new Color(0.55f, 0.12f, 0.12f, 1f), new Vector2(34, 30));
            var closeBtnRect = closeBtn.GetComponent<RectTransform>();
            closeBtnRect.anchorMin = new Vector2(1f, 1f);
            closeBtnRect.anchorMax = new Vector2(1f, 1f);
            closeBtnRect.pivot = new Vector2(1f, 1f);
            closeBtnRect.anchoredPosition = new Vector2(-14, -14);
            closeBtn.onClick.AddListener(() => Close());

            // 스크롤뷰
            var scrollGO = new GameObject("ScrollView", typeof(RectTransform), typeof(Image), typeof(ScrollRect));
            scrollGO.transform.SetParent(box.transform, false);
            var scrollRect = scrollGO.GetComponent<RectTransform>();
            scrollRect.anchorMin = new Vector2(0f, 0f);
            scrollRect.anchorMax = new Vector2(1f, 1f);
            scrollRect.offsetMin = new Vector2(20, 20);
            scrollRect.offsetMax = new Vector2(-20, -66);
            scrollGO.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.28f);

            var viewport = new GameObject("Viewport", typeof(RectTransform), typeof(Image), typeof(Mask));
            viewport.transform.SetParent(scrollGO.transform, false);
            var viewportRect = viewport.GetComponent<RectTransform>();
            viewportRect.anchorMin = Vector2.zero;
            viewportRect.anchorMax = Vector2.one;
            viewportRect.offsetMin = Vector2.zero;
            viewportRect.offsetMax = new Vector2(-16, 0); // 스크롤바 여백
            viewport.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.01f);
            viewport.GetComponent<Mask>().showMaskGraphic = false;

            var contentGO = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
            contentGO.transform.SetParent(viewport.transform, false);
            _content = contentGO.GetComponent<RectTransform>();
            _content.anchorMin = new Vector2(0f, 1f);
            _content.anchorMax = new Vector2(1f, 1f);
            _content.pivot = new Vector2(0.5f, 1f);
            _content.anchoredPosition = Vector2.zero;
            _content.sizeDelta = new Vector2(0f, _content.sizeDelta.y);

            var layout = contentGO.GetComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(6, 6, 6, 6);
            layout.spacing = 4f;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            layout.childControlHeight = true;
            layout.childControlWidth = true;

            var fitter = contentGO.GetComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var scroll = scrollGO.GetComponent<ScrollRect>();
            scroll.content = _content;
            scroll.viewport = viewportRect;
            scroll.horizontal = false;
            scroll.vertical = true;
            scroll.movementType = ScrollRect.MovementType.Clamped;

            var scrollbarGO = new GameObject("Scrollbar", typeof(RectTransform), typeof(Image), typeof(Scrollbar));
            scrollbarGO.transform.SetParent(scrollGO.transform, false);
            var sbRect = scrollbarGO.GetComponent<RectTransform>();
            sbRect.anchorMin = new Vector2(1f, 0f);
            sbRect.anchorMax = new Vector2(1f, 1f);
            sbRect.pivot = new Vector2(1f, 1f);
            sbRect.sizeDelta = new Vector2(12, 0);
            sbRect.anchoredPosition = Vector2.zero;
            scrollbarGO.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.15f);

            var handleGO = new GameObject("Handle", typeof(RectTransform), typeof(Image));
            handleGO.transform.SetParent(scrollbarGO.transform, false);
            var handleRect = handleGO.GetComponent<RectTransform>();
            handleRect.anchorMin = Vector2.zero;
            handleRect.anchorMax = Vector2.one;
            handleRect.offsetMin = Vector2.zero;
            handleRect.offsetMax = Vector2.zero;
            handleGO.GetComponent<Image>().color = GoldColor;

            var scrollbar = scrollbarGO.GetComponent<Scrollbar>();
            scrollbar.handleRect = handleRect;
            scrollbar.direction = Scrollbar.Direction.BottomToTop;
            scroll.verticalScrollbar = scrollbar;

            _tooltipUI = _panelRoot.AddComponent<QuestTooltipUI>();

            _panelRoot.SetActive(false);
        }

        /// <summary>퀘스트 목록을 바이옴별로 그룹핑해 다시 그린다.</summary>
        public static void Refresh()
        {
            if (_content == null) return;
            var player = Player.m_localPlayer;

            for (int i = _content.childCount - 1; i >= 0; i--)
                Object.Destroy(_content.GetChild(i).gameObject);

            var quests = QuestManager.GetActiveQuests();
            foreach (var biomeGroup in quests.GroupBy(q => q.Biome))
            {
                CreateBiomeHeader(biomeGroup.Key);
                foreach (var quest in biomeGroup.OrderBy(q => q.DisplayOrder))
                    CreateQuestRow(player, quest);
            }

            // 비활성 상태에서 쌓인 레이아웃 변경사항이 다음 프레임까지 밀리지 않도록 즉시 재계산
            LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        }

        private static void CreateBiomeHeader(string biome)
        {
            var headerRow = new GameObject("Header_" + biome, typeof(RectTransform), typeof(HorizontalLayoutGroup));
            headerRow.transform.SetParent(_content, false);
            var headerLayout = headerRow.GetComponent<HorizontalLayoutGroup>();
            headerLayout.padding = new RectOffset(4, 4, 4, 2);
            headerLayout.childForceExpandWidth = false;
            headerLayout.childForceExpandHeight = true;
            headerLayout.childControlWidth = true;
            headerLayout.childControlHeight = true;
            headerLayout.childAlignment = TextAnchor.MiddleLeft;
            var headerRowElem = headerRow.AddComponent<LayoutElement>();
            headerRowElem.preferredHeight = 30;
            headerRowElem.minHeight = 30;

            // Text는 자기 자신도 ILayoutElement라서 같은 오브젝트에 LayoutElement를 붙이면
            // 텍스트 내용에 따라 HorizontalLayoutGroup의 폭 계산과 충돌할 수 있다(바이옴 이름·퀘스트
            // 항목 텍스트가 반복적으로 왼쪽부터 잘리던 근본 원인으로 추정). 폭을 담당하는 래퍼와
            // 렌더링을 담당하는 Text를 분리해서 LayoutGroup이 항상 래퍼의 LayoutElement만 보게 한다.
            var headerWrap = new GameObject("LabelWrap", typeof(RectTransform));
            headerWrap.transform.SetParent(headerRow.transform, false);
            var headerElem = headerWrap.AddComponent<LayoutElement>();
            headerElem.preferredWidth = 220;
            headerElem.minWidth = 100;

            var header = CreateText(headerWrap.transform, "Label", L.Get("quest_biome_" + biome), 20,
                new Color(0.45f, 0.05f, 0.05f, 1f), TextAnchor.MiddleLeft);
            header.fontStyle = FontStyle.Bold;

            // 금색 구분선 (헤더와 형제 관계, _content의 직계 자식 - 기존에도 정상 렌더링되던 요소)
            var divider = new GameObject("Divider_" + biome, typeof(RectTransform), typeof(Image));
            divider.transform.SetParent(_content, false);
            divider.GetComponent<Image>().color = GoldColor;
            var dividerElem = divider.AddComponent<LayoutElement>();
            dividerElem.preferredHeight = 3;
            dividerElem.minHeight = 3;
        }

        private static void CreateQuestRow(Player player, QuestDefinition quest)
        {
            var row = new GameObject("Row_" + quest.Key, typeof(RectTransform), typeof(Image), typeof(HorizontalLayoutGroup));
            row.transform.SetParent(_content, false);
            var rowImage = row.GetComponent<Image>();
            var rowLayout = row.GetComponent<HorizontalLayoutGroup>();
            rowLayout.padding = new RectOffset(10, 10, 4, 4);
            rowLayout.childForceExpandWidth = false;
            rowLayout.childForceExpandHeight = true;
            rowLayout.childControlWidth = true;
            rowLayout.childControlHeight = true;
            rowLayout.childAlignment = TextAnchor.MiddleLeft;
            rowLayout.spacing = 12f; // 목록 텍스트와 상태 수치(0/20) 사이 여백을 넓히기 위해 8→12
            var rowElem = row.AddComponent<LayoutElement>();
            rowElem.preferredHeight = 36;
            rowElem.minHeight = 36;

            bool completed = player != null && QuestManager.IsCompleted(player, quest);
            bool claimed = player != null && QuestManager.IsClaimed(player, quest.Key);
            int progress = player != null ? QuestManager.GetProgress(player, quest) : 0;

            string label = BuildQuestLabel(quest);
            // [농사]/[요리]/[낚시] 문구가 기존 [처치]/[채집]보다 길어 상태 수치(0/20)와 겹쳐 보이던 문제 대응 - 폭 확장.
            var labelText = CreateSizedText(row.transform, "Label", label, 15, LabelTextColor, TextAnchor.MiddleLeft, 320, 220);
            labelText.fontStyle = FontStyle.Bold;

            if (claimed)
            {
                rowImage.color = new Color(0f, 0f, 0f, 0.04f);
                labelText.color = ClaimedTextColor;
                CreateSizedText(row.transform, "Status", L.Get("quest_status_claimed"), 15,
                    ClaimedTextColor, TextAnchor.MiddleLeft, 90, 70);
            }
            else if (completed)
            {
                rowImage.color = new Color(0.85f, 0.7f, 0.1f, 0.30f);
                var statusText = CreateSizedText(row.transform, "Status", L.Get("quest_status_completed"), 15,
                    CompletedTextColor, TextAnchor.MiddleLeft, 90, 70);
                statusText.fontStyle = FontStyle.Bold;

                // 보상 버튼은 "완료" 상태 텍스트 바로 오른쪽에 붙인다.
                var claimBtn = CreateSimpleButton(row.transform, "ClaimButton", L.Get("quest_claim_button"),
                    new Color(0.15f, 0.45f, 0.20f, 1f), new Vector2(70, 26));
                var claimBtnElem = claimBtn.gameObject.AddComponent<LayoutElement>();
                claimBtnElem.preferredWidth = 70;
                claimBtnElem.minWidth = 70;
                var claimQuest = quest;
                claimBtn.onClick.AddListener(() =>
                {
                    var p = Player.m_localPlayer;
                    if (p != null && QuestManager.ClaimReward(p, claimQuest))
                    {
                        Refresh();
                        CaptainSkillTree.MMO_System.QuestIconPatch.UpdateClaimableDot();
                    }
                });
            }
            else
            {
                rowImage.color = new Color(0f, 0f, 0f, 0.10f);
                bool levelLocked = quest.HasLevelRequirement && !QuestManager.IsLevelRequirementMet(quest);
                string statusLabel = levelLocked
                    ? L.Get("player_level_required_short", quest.RequiredLevel)
                    : $"{progress}/{quest.Amount}";
                var statusText = CreateSizedText(row.transform, "Status", statusLabel, 16,
                    levelLocked ? ClaimedTextColor : ProgressTextColor, TextAnchor.MiddleLeft, 90, 70);
                statusText.fontStyle = FontStyle.Bold;
            }

            // 경험치 태그가 추가되며 보상 목록 길이가 길어져, 행 오른쪽 끝에 딱 붙이면(플렉서블 스페이서)
            // 텍스트가 오른쪽으로 넘쳐 잘려 보였다. 고정 간격으로만 띄우고 목록은 왼쪽으로 당겨서 시작한다.
            var spacer = new GameObject("Spacer", typeof(RectTransform));
            spacer.transform.SetParent(row.transform, false);
            var spacerElem = spacer.AddComponent<LayoutElement>();
            spacerElem.preferredWidth = 20;
            spacerElem.minWidth = 20;

            // 완료 시 받을 보상 목록을 표시한다(오브/포션 등으로 길어질 수 있어 폭을 넉넉히 확보).
            var rewardText = CreateSizedText(row.transform, "RewardPreview", BuildRewardPreview(quest), 12,
                RewardPreviewTextColor, TextAnchor.MiddleLeft, 330, 160);
            rewardText.fontStyle = FontStyle.Bold;

            // 마우스오버 시 퀘스트 설명/진행상황/보상을 자세히 보여주는 툴팁을 붙인다.
            var hoverQuest = quest;
            var trigger = row.AddComponent<EventTrigger>();
            var enterEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            enterEntry.callback.AddListener(_ => _tooltipUI?.ShowTooltip(BuildQuestTooltipText(Player.m_localPlayer, hoverQuest)));
            trigger.triggers.Add(enterEntry);
            var exitEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            exitEntry.callback.AddListener(_ => _tooltipUI?.HideTooltip());
            trigger.triggers.Add(exitEntry);
        }

        /// <summary>퀘스트 행 툴팁 전체 텍스트(설명 + 진행상황 + 보상)를 조립한다.</summary>
        private static string BuildQuestTooltipText(Player player, QuestDefinition quest)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"<color=#FFD700><b>{BuildQuestLabel(quest)}</b></color>");

            string descKey = "quest_desc_" + quest.Key;
            if (L.Has(descKey))
            {
                sb.AppendLine();
                sb.AppendLine($"<color=#E0E0E0>{L.Get(descKey)}</color>");
            }

            bool completed = player != null && QuestManager.IsCompleted(player, quest);
            bool claimed = player != null && QuestManager.IsClaimed(player, quest.Key);
            int progress = player != null ? QuestManager.GetProgress(player, quest) : 0;
            bool levelLocked = quest.HasLevelRequirement && !QuestManager.IsLevelRequirementMet(quest);

            sb.AppendLine();
            string statusLine = claimed ? L.Get("quest_status_claimed")
                : completed ? L.Get("quest_status_completed")
                : levelLocked ? L.Get("player_level_required", quest.RequiredLevel, CaptainSkillTree.MMO_System.CaptainMMOBridge.GetLevel())
                : $"{progress}/{quest.Amount}";
            sb.AppendLine($"<color=#B0C4DE>{L.Get("quest_tooltip_progress")}: </color><color=#FFFFFF>{statusLine}</color>");

            sb.Append($"<color=#98FB98>{L.Get("quest_claim_button")}: </color><color=#FFFFFF>{BuildRewardPreview(quest)}</color>");

            return sb.ToString().TrimEnd();
        }

        /// <summary>퀘스트 완료 시 받을 보상(마법 오브/XP 포션/코인/아이템/특수보상)을 한 줄 요약 텍스트로 만든다.</summary>
        private static string BuildRewardPreview(QuestDefinition quest)
        {
            var parts = new List<string>();

            int itemRewardCount = QuestManager.GetQuestRewardItemCount(quest);
            string orbPrefab = QuestManager.GetQuestOrbPrefab(quest);
            if (!string.IsNullOrEmpty(orbPrefab)) parts.Add($"{GetItemDisplayName(orbPrefab)} x{itemRewardCount}");
            string potionPrefab = QuestManager.GetQuestPotionPrefab(quest);
            if (!string.IsNullOrEmpty(potionPrefab)) parts.Add($"{GetItemDisplayName(potionPrefab)} x{itemRewardCount}");

            if (quest.HasCoinReward)
            {
                string coinLabel = L.Get("quest_coin_label");
                parts.Add(quest.CoinMin == quest.CoinMax
                    ? $"{coinLabel} {quest.CoinMin}"
                    : $"{coinLabel} {quest.CoinMin}~{quest.CoinMax}");
            }
            if (quest.HasItemReward1) parts.Add($"{GetItemDisplayName(quest.ItemReward1)} x{quest.ItemReward1Amount}");
            if (quest.HasItemReward2) parts.Add($"{GetItemDisplayName(quest.ItemReward2)} x{quest.ItemReward2Amount}");

            string special = GetSpecialRewardLabel(quest.SpecialReward);
            if (!string.IsNullOrEmpty(special)) parts.Add(special);

            return parts.Count > 0 ? string.Join(", ", parts) : "-";
        }

        /// <summary>
        /// 보상 아이템의 표시 이름을 이 모드 자체 로컬라이제이션 사전에서 가져온다.
        /// (발헤임 자체 Localization을 리플렉션으로 우회하는 방식은 값이 항상 정상적으로
        /// 나오지 않아, BuildQuestLabel의 대상 이름 표시와 동일하게 L.Get() 방식으로 통일한다.)
        /// </summary>
        private static string GetItemDisplayName(string prefabName)
        {
            if (string.IsNullOrEmpty(prefabName)) return prefabName;
            string key = "item_" + prefabName.ToLowerInvariant();
            return L.Has(key) ? L.Get(key) : prefabName;
        }

        /// <summary>SpecialReward 문자열("TameLoxCalf" / "JumpProficiency:10")을 표시용 텍스트로 변환한다.</summary>
        private static string GetSpecialRewardLabel(string specialReward)
        {
            if (string.IsNullOrEmpty(specialReward)) return null;
            if (specialReward == "TameLoxCalf") return L.Get("quest_reward_tame_lox");
            if (specialReward == "TameWolfCub") return L.Get("quest_reward_tame_wolf");
            if (specialReward.StartsWith("JumpProficiency")) return L.Get("quest_reward_jump_proficiency");
            if (specialReward.StartsWith("StaminaBonus")) return L.Get("quest_reward_stamina_bonus");
            if (specialReward.StartsWith("ElementalDamage")) return L.Get("quest_reward_elemental_damage");
            if (specialReward.StartsWith("PhysicalDamage")) return L.Get("quest_reward_physical_damage");
            if (specialReward.StartsWith("FarmingSkill")) return L.Get("quest_reward_farming_skill", ParseSkillAmount(specialReward));
            if (specialReward.StartsWith("CookingSkill")) return L.Get("quest_reward_cooking_skill", ParseSkillAmount(specialReward));
            if (specialReward.StartsWith("FishingSkill")) return L.Get("quest_reward_fishing_skill", ParseSkillAmount(specialReward));
            if (specialReward.StartsWith("MeleeWeaponSkill")) return L.Get("quest_reward_melee_weapon_skill", ParseSkillAmount(specialReward));
            if (specialReward.StartsWith("RangedWeaponSkill")) return L.Get("quest_reward_ranged_weapon_skill", ParseSkillAmount(specialReward));
            if (specialReward.StartsWith("RandomItem:"))
            {
                var options = specialReward.Substring("RandomItem:".Length).Split(',');
                var names = new List<string>(options.Length);
                foreach (var option in options)
                    names.Add(GetItemDisplayName(option));
                return L.Get("quest_reward_random_item", string.Join(", ", names));
            }
            return specialReward;
        }

        /// <summary>"FarmingSkill:2" 같은 SpecialReward 문자열에서 ":" 뒤의 수치만 뽑아 미리보기에 표시한다.</summary>
        private static int ParseSkillAmount(string specialReward)
        {
            var parts = specialReward.Split(':');
            return parts.Length > 1 && int.TryParse(parts[1], out var v) ? v : 0;
        }

        private static string BuildQuestLabel(QuestDefinition quest)
        {
            if (quest.Type == QuestType.Plant || quest.Type == QuestType.Cook || quest.Type == QuestType.Fish)
            {
                string typeName = quest.Type == QuestType.Plant ? "farm" : quest.Type == QuestType.Cook ? "cook" : "fish";
                string tag = L.Get($"quest_type_{typeName}");
                string bodyKey = quest.RequireDistinctTargets ? $"quest_label_{typeName}_distinct" : $"quest_label_{typeName}_count";
                return $"[{tag}] {L.Get(bodyKey, quest.Amount)}";
            }

            // 모더 근접/원거리 전용 특수 퀘스트는 둘 다 같은 대상(Dragon) x1이라 일반 규칙대로
            // 라벨을 만들면 "[특수] 모더 x1"로 서로 구분이 안 된다 - 전용 문구로 대체.
            if (quest.Key == "Mountain_Quest5")
                return $"[{L.Get("quest_type_special")}] {L.Get("quest_label_mountain_melee_only")}";
            if (quest.Key == "Mountain_Quest6")
                return $"[{L.Get("quest_type_special")}] {L.Get("quest_label_mountain_ranged_only")}";

            string typeLabel = QuestManager.HiddenAchievementQuestKeys.Contains(quest.Key) ? L.Get("quest_type_special")
                              : quest.Type == QuestType.Gather ? L.Get("quest_type_gather")
                              : quest.Type == QuestType.KillBoss ? L.Get("quest_type_kill_boss")
                              : L.Get("quest_type_kill");
            string targetKey = "item_" + quest.TargetPrefab.ToLowerInvariant();
            string targetName = L.Has(targetKey) ? L.Get(targetKey) : quest.TargetPrefab;
            return $"[{typeLabel}] {targetName} x{quest.Amount}";
        }

        // ===== 공용 UI 헬퍼 =====

        /// <summary>부모를 풀스트레치로 채우는 Text를 만든다(LayoutGroup의 뒤늦은 오버라이드에 의존하지 않음).</summary>
        private static Text CreateText(Transform parent, string name, string text, int fontSize, Color color, TextAnchor anchor)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            go.transform.SetParent(parent, false);
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.pivot = new Vector2(0.5f, 0.5f);

            var t = go.GetComponent<Text>();
            t.text = text;
            t.fontSize = fontSize;
            t.color = color;
            t.alignment = anchor;
            t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            t.raycastTarget = false;
            t.horizontalOverflow = HorizontalWrapMode.Overflow;
            t.verticalOverflow = VerticalWrapMode.Overflow;
            return t;
        }

        /// <summary>
        /// HorizontalLayoutGroup 안에서 폭이 고정된 Text를 만든다. Text 자신도 ILayoutElement를
        /// 구현하기 때문에 LayoutElement를 Text와 같은 오브젝트에 직접 붙이면 텍스트 내용에 따라
        /// LayoutGroup의 폭 계산과 충돌할 수 있다. 폭을 담당하는 래퍼 오브젝트를 한 단계 두어
        /// LayoutGroup이 항상 래퍼의 LayoutElement만 보도록 분리한다.
        /// </summary>
        private static Text CreateSizedText(Transform parent, string name, string text, int fontSize, Color color,
            TextAnchor anchor, float preferredWidth, float minWidth)
        {
            var wrap = new GameObject(name + "Wrap", typeof(RectTransform));
            wrap.transform.SetParent(parent, false);
            var elem = wrap.AddComponent<LayoutElement>();
            elem.preferredWidth = preferredWidth;
            elem.minWidth = minWidth;
            return CreateText(wrap.transform, name, text, fontSize, color, anchor);
        }

        /// <summary>금테 + 다크필 2겹 레이어 버튼(메인 스킬트리 패널의 CreateStyledButton과 동일한 톤).</summary>
        private static Button CreateSimpleButton(Transform parent, string name, string text, Color fillColor, Vector2 size)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            go.transform.SetParent(parent, false);
            var rect = go.GetComponent<RectTransform>();
            rect.sizeDelta = size;
            go.GetComponent<Image>().color = GoldColor;

            var fillGO = new GameObject("Fill", typeof(RectTransform), typeof(Image));
            fillGO.transform.SetParent(go.transform, false);
            var fillRect = fillGO.GetComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.offsetMin = new Vector2(2, 2);
            fillRect.offsetMax = new Vector2(-2, -2);
            fillGO.GetComponent<Image>().color = fillColor;

            var textT = CreateText(fillGO.transform, "Text", text, 14, Color.white, TextAnchor.MiddleCenter);
            textT.fontStyle = FontStyle.Bold;

            return go.GetComponent<Button>();
        }

        /// <summary>
        /// 퀘스트 창 전용 배경 에셋(asset/Resources/quest_bg)에서 첫 번째 Sprite를 가져온다.
        /// Plugin.cs(수정 금지)의 다른 GetXxxBundle()과 달리 이 번들은 QuestPanelUI 전용이라 여기서 직접 로드한다.
        /// 내부 에셋 이름을 알 수 없으므로 이름으로 찾지 않고 번들에 들어있는 첫 Sprite를 그대로 사용한다.
        /// </summary>
        private static Sprite GetQuestBgSprite()
        {
            if (_questBgBundle == null)
            {
                var assembly = typeof(QuestPanelUI).Assembly;
                var stream = assembly.GetManifestResourceStream("CaptainSkillTree.asset.Resources.quest_bg");
                if (stream == null)
                {
                    Plugin.Log.LogWarning("[QuestPanelUI] quest_bg 리소스 스트림이 null");
                    return null;
                }

                _questBgBundle = AssetBundle.LoadFromStream(stream);
                if (_questBgBundle == null)
                {
                    Plugin.Log.LogError("[QuestPanelUI] quest_bg AssetBundle.LoadFromStream 실패");
                    return null;
                }

                _questBgBundle.hideFlags = HideFlags.HideAndDontSave;
                var allNames = _questBgBundle.GetAllAssetNames();
                foreach (var n in allNames) Plugin.Log.LogInfo($"[QuestPanelUI] quest_bg 에셋: {n}");
            }

            var sprites = _questBgBundle.LoadAllAssets<Sprite>();
            if (sprites == null || sprites.Length == 0)
            {
                Plugin.Log.LogWarning("[QuestPanelUI] quest_bg 번들에서 Sprite를 찾지 못함");
                return null;
            }
            return sprites[0];
        }

        private static Canvas FindMainCanvas()
        {
            var inventoryGui = InventoryGui.instance;
            if (inventoryGui != null)
            {
                var canvas = inventoryGui.GetComponentInParent<Canvas>();
                if (canvas != null) return canvas;
            }

            var canvases = Object.FindObjectsOfType<Canvas>();
            foreach (var c in canvases)
            {
                if (c.gameObject.name.Contains("GUI") || c.gameObject.name.Contains("Canvas"))
                    return c;
            }
            return canvases.Length > 0 ? canvases[0] : null;
        }
    }
}

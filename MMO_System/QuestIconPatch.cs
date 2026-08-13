using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.Gui;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// 탭 인벤토리 화면에 Quest 아이콘을 추가한다. Plugin.cs가 만드는 "ButtonSkillTree" 아이콘을
    /// 씬 그래프에서 찾아 복제하는 방식(md/CORE_PROTECTION_README.md에 문서화된 패턴과 동일)만 사용하고,
    /// Plugin.cs/SkillTreeInputListener.cs 자체는 절대 수정하지 않는다.
    /// EpicMMO가 설치되어 "ButtonSkillTree"가 존재하는 경우에만 동작한다(EpicMMO 없는 폴백 아이콘은 미지원).
    /// </summary>
    [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Show))]
    public static class QuestIconPatch
    {
        private static GameObject _questIconObj;
        private static GameObject _claimableDotObj;

        [HarmonyPostfix]
        [HarmonyPriority(Priority.Low)] // Plugin.cs의 ButtonSkillTree 생성 패치가 먼저 실행되도록 보장
        public static void Postfix()
        {
            try
            {
                if (!Quest_Config.IsEnabled) return;

                if (_questIconObj == null)
                {
                    if (!TryCreateQuestIcon()) return;
                }

                _questIconObj.SetActive(true);
                UpdateClaimableDot();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[QuestIconPatch] Quest 아이콘 표시 실패: {ex.Message}");
            }
        }

        private static bool TryCreateQuestIcon()
        {
            // "스킬트리ui 시작 아이콘은 보이는데"(=ButtonSkillTree는 실제로 씬에 존재) 라는 피드백에 따라,
            // EpicMMO(Clone)/Canvas/MyUI/navigationPanel/Buttons 경로를 가정해 찾던 방식(하드코딩된
            // 경로가 실제 계층 구조와 다르면 조용히 실패함) 대신, 씬 전체에서 "ButtonSkillTree"라는
            // 이름의 오브젝트를 직접 재귀 탐색한다 - Plugin.cs가 그 오브젝트를 어디에 붙였든 상관없이 찾는다.
            var skillTreeButton = FindGameObjectInAnyScene("ButtonSkillTree");
            if (skillTreeButton == null)
            {
                Plugin.Log.LogInfo("[QuestIconPatch] 씬 전체에서 'ButtonSkillTree'를 찾지 못함 - 아직 생성 전이거나 EpicMMO 미설치. 다음 InventoryGui.Show에서 재시도");
                return false;
            }

            var buttonsContainer = skillTreeButton.transform.parent;
            if (buttonsContainer == null)
            {
                Plugin.Log.LogWarning("[QuestIconPatch] 'ButtonSkillTree'에 부모가 없음 - 나란히 배치 불가");
                return false;
            }

            Plugin.Log.LogInfo($"[QuestIconPatch] 'ButtonSkillTree' 발견: 전체 경로 = {GetFullPath(skillTreeButton.transform)}");

            var existing = buttonsContainer.Find("ButtonQuest");
            if (existing != null) UnityEngine.Object.Destroy(existing.gameObject);

            var questButton = UnityEngine.Object.Instantiate(skillTreeButton, buttonsContainer);
            questButton.name = "ButtonQuest";
            Plugin.Log.LogInfo("[QuestIconPatch] ButtonSkillTree 복제 성공 - ButtonQuest 생성");

            var rect = questButton.GetComponent<RectTransform>();
            var origRect = skillTreeButton.GetComponent<RectTransform>();
            rect.anchoredPosition = origRect.anchoredPosition + new Vector2(80, 0);

            // "Icon"이라는 이름의 자식을 가정했으나 실제 계층 구조(LevelHud(Clone)/...)에서는
            // 이름이 다른 것으로 확인됨. 자식 Image들 중 "면적이 가장 작은 것"을 아이콘으로 간주해왔으나,
            // 실측 로그(발견된 자식 Image 목록: [Image:15x15])에서 자식이 15x15짜리 하나뿐임이 확인됨 -
            // 툴바 버튼 크기(수십 px)에 비해 지나치게 작아 실제로 보이는 아이콘이 아니라 장식 요소로 추정.
            // ButtonSkillTree는 자체 루트 Image가 곧 아이콘(별도 테두리 자식 없음)일 가능성이 높으므로,
            // 루트와 발견된 자식 모두에 스프라이트를 적용해 어느 쪽이 실제 아이콘이든 반영되게 한다.
            var rootImage = questButton.GetComponent<Image>();
            var rootSize = rootImage != null ? rootImage.rectTransform.rect.size : Vector2.zero;
            Plugin.Log.LogInfo($"[QuestIconPatch] 루트 Image 크기: {rootSize.x:F0}x{rootSize.y:F0}");

            // 유니티 소스 프로젝트에서 확인된 대로 생산전문가 아이콘은 skill_node 번들의 "production_unlock"이다.
            // job_icon 번들의 "craft_unlock", 그리고 최후 폴백으로 all_skill_unlock 순으로 시도한다.
            Sprite sprite = null;
            string loadedFrom = null;

            var skillNodeBundle = Plugin.GetIconAssetBundle();
            if (skillNodeBundle != null)
            {
                sprite = skillNodeBundle.LoadAsset<Sprite>("production_unlock");
                if (sprite != null) loadedFrom = "skill_node:production_unlock";
            }

            if (sprite == null)
            {
                var jobBundle = Plugin.GetJobIconBundle();
                if (jobBundle != null)
                {
                    sprite = jobBundle.LoadAsset<Sprite>("craft_unlock");
                    if (sprite != null) loadedFrom = "job_icon:craft_unlock";
                }
            }

            if (sprite == null && skillNodeBundle != null)
            {
                sprite = skillNodeBundle.LoadAsset<Sprite>("all_skill_unlock");
                if (sprite != null) loadedFrom = "skill_node:all_skill_unlock(fallback)";
            }

            if (sprite != null)
            {
                if (rootImage != null)
                {
                    rootImage.sprite = sprite;
                    rootImage.type = Image.Type.Simple;
                    rootImage.color = Color.white;
                }
                Plugin.Log.LogInfo($"[QuestIconPatch] 아이콘 스프라이트 적용 성공 ({loadedFrom})");
            }
            else
            {
                // 스프라이트 로드는 실패했지만 버튼 자체는 살아있음을 눈에 띄게 표시
                if (rootImage != null) rootImage.color = new Color(1f, 0.5f, 0f, 1f);
                Plugin.Log.LogWarning("[QuestIconPatch] 'production_unlock'/'craft_unlock'/'all_skill_unlock' 스프라이트 모두 로드 실패 - 주황색으로 표시");
            }

            var button = questButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => QuestPanelUI.Toggle());
            }

            // ButtonSkillTree(원본은 EpicMMO의 ButtonLevelSystem)에서 물려받은 GetChild(0)은
            // 스킬트리 아이콘 우상단에 뜨는 실제 동그란 알림 점(Plugin.cs의 UpdateSkillTreeIconDot()이
            // 활성/비활성을 토글하는 바로 그 오브젝트)이다. ButtonQuest는 ButtonSkillTree의 복제본이라
            // 이미 동일한 동그란 점을 갖고 있으므로, 새로 사각형 Image를 만드는 대신 이걸 그대로 재사용해
            // 스킬트리 아이콘과 모양이 완전히 같게 만든다.
            if (questButton.transform.childCount > 0)
                _claimableDotObj = questButton.transform.GetChild(0).gameObject;

            _questIconObj = questButton;
            return true;
        }

        /// <summary>활성 퀘스트 중 완료했지만 아직 수령 안 한 게 하나라도 있으면 알림 점을 켠다.</summary>
        public static void UpdateClaimableDot()
        {
            if (_claimableDotObj == null) return;

            var player = Player.m_localPlayer;
            bool hasClaimable = player != null && QuestManager.GetActiveQuests().Any(q =>
                QuestManager.IsCompleted(player, q) &&
                !QuestManager.IsClaimed(player, q.Key));

            _claimableDotObj.SetActive(hasClaimable);
        }

        /// <summary>현재 로드된 씬(에셋/프리팹 참조 제외)에서 이름이 일치하는 첫 GameObject를 재귀적으로 찾는다.</summary>
        private static GameObject FindGameObjectInAnyScene(string name)
        {
            var all = Resources.FindObjectsOfTypeAll<RectTransform>();
            foreach (var rt in all)
            {
                if (rt.name != name) continue;
                if (!rt.gameObject.scene.IsValid()) continue; // 씬에 없는(프리팹 에셋 등) 오브젝트는 제외
                if (rt.hideFlags != HideFlags.None) continue;
                return rt.gameObject;
            }
            return null;
        }

        private static string GetFullPath(Transform t)
        {
            string path = t.name;
            while (t.parent != null)
            {
                t = t.parent;
                path = t.name + "/" + path;
            }
            return path;
        }

        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Hide))]
        public static class HidePatch
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                if (_questIconObj != null) _questIconObj.SetActive(false);
            }
        }
    }
}

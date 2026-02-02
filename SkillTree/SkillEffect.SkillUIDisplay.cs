using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;
using TMPro;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 숙련도 레벨 표시 패치
    /// 기본 레벨과 스킬트리 보너스를 분리하여 표시
    /// 예: "5 +8" (5는 기본, +8은 빨간색 스킬트리 보너스)
    /// </summary>
    public static partial class SkillEffect
    {
        // 숙련도 UI 텍스트 색상 - 빨간색 (FF3333)
        public static readonly string BonusColorHex = "FF3333";

        // 스킬 타입 이름 매핑 (영어 이름 -> SkillType)
        private static Dictionary<string, Skills.SkillType> _skillNameMap = null;

        public static Skills.SkillType? GetSkillTypeFromName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            // 캐시 초기화
            if (_skillNameMap == null)
            {
                _skillNameMap = new Dictionary<string, Skills.SkillType>(StringComparer.OrdinalIgnoreCase);
                foreach (Skills.SkillType st in Enum.GetValues(typeof(Skills.SkillType)))
                {
                    if (st == Skills.SkillType.None || st == Skills.SkillType.All) continue;
                    _skillNameMap[st.ToString()] = st;
                }
                // 한글 이름 추가
                _skillNameMap["검"] = Skills.SkillType.Swords;
                _skillNameMap["칼"] = Skills.SkillType.Knives;
                _skillNameMap["둔기"] = Skills.SkillType.Clubs;
                _skillNameMap["창"] = Skills.SkillType.Spears;
                _skillNameMap["장창"] = Skills.SkillType.Polearms;
                _skillNameMap["폴암"] = Skills.SkillType.Polearms;
                _skillNameMap["도끼"] = Skills.SkillType.Axes;
                _skillNameMap["활"] = Skills.SkillType.Bows;
                _skillNameMap["석궁"] = Skills.SkillType.Crossbows;
                _skillNameMap["원소마법"] = Skills.SkillType.ElementalMagic;
                _skillNameMap["피마법"] = Skills.SkillType.BloodMagic;
                _skillNameMap["달리기"] = Skills.SkillType.Run;
                _skillNameMap["점프"] = Skills.SkillType.Jump;
                _skillNameMap["수영"] = Skills.SkillType.Swim;
                _skillNameMap["잠행"] = Skills.SkillType.Sneak;
                _skillNameMap["곡괭이"] = Skills.SkillType.Pickaxes;
                _skillNameMap["벌목"] = Skills.SkillType.WoodCutting;
            }

            Skills.SkillType result;
            if (_skillNameMap.TryGetValue(name, out result))
            {
                return result;
            }
            return null;
        }
    }

    /// <summary>
    /// SkillsDialog UI의 숙련도 레벨 텍스트 표시를 패치
    /// </summary>
    [HarmonyPatch(typeof(SkillsDialog), "Setup")]
    public static class SkillsDialog_Setup_Patch
    {
        private static FieldInfo _elementsField = null;
        private static int _lastSetupFrame = -1;

        [HarmonyPriority(Priority.Low)]
        public static void Postfix(SkillsDialog __instance, Player player)
        {
            try
            {
                if (player == null) return;

                // 같은 프레임에서 중복 호출 방지
                int currentFrame = Time.frameCount;
                if (_lastSetupFrame == currentFrame)
                {
                    return;
                }
                _lastSetupFrame = currentFrame;

                // m_elements 필드 접근
                if (_elementsField == null)
                {
                    _elementsField = typeof(SkillsDialog).GetField("m_elements",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                }

                if (_elementsField == null)
                {
                    Plugin.Log.LogWarning("[숙련도 UI] m_elements 필드를 찾을 수 없음");
                    return;
                }

                var elements = _elementsField.GetValue(__instance) as List<GameObject>;
                if (elements == null || elements.Count == 0)
                {
                    return;
                }

                var skills = player.GetSkills();
                if (skills == null) return;

                // 각 스킬 요소 처리
                foreach (var element in elements)
                {
                    if (element == null) continue;

                    try
                    {
                        ProcessSkillElement(element, skills);
                    }
                    catch (Exception ex)
                    {
                        Plugin.Log.LogDebug($"[숙련도 UI] 요소 처리 실패: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[숙련도 UI] Setup 패치 오류: {ex.Message}");
            }
        }

        private static void ProcessSkillElement(GameObject element, Skills skills)
        {
            // 모든 TMP_Text 컴포넌트 가져오기
            var tmpTexts = element.GetComponentsInChildren<TMP_Text>(true);
            if (tmpTexts == null || tmpTexts.Length < 2) return;

            TMP_Text nameText = null;
            TMP_Text levelText = null;
            int displayedLevel = 0;

            // 각 텍스트 컴포넌트 분석
            foreach (var tmp in tmpTexts)
            {
                if (tmp == null) continue;

                string text = tmp.text;
                if (string.IsNullOrEmpty(text)) continue;

                string trimmed = text.Trim();

                // 이미 우리 모드가 수정한 텍스트인지 확인 (빨간색 태그)
                if (trimmed.Contains("#FF3333") || trimmed.Contains("#ff3333"))
                {
                    continue; // 이미 처리됨, 건너뛰기
                }

                // 색상 태그 모두 제거
                string cleanText = System.Text.RegularExpressions.Regex.Replace(trimmed, "<.*?>", "");

                // 숫자만 포함된 텍스트 (순수 레벨)
                int parsedValue;
                if (int.TryParse(cleanText, out parsedValue))
                {
                    levelText = tmp;
                    displayedLevel = parsedValue;
                }
                // 다른 모드가 수정한 텍스트 (예: "10 +5" 등)
                else if (cleanText.Contains("+") && levelText == null)
                {
                    levelText = tmp;

                    // 첫 번째 숫자만 추출 (기본 레벨)
                    var parts = cleanText.Split(new char[] { ' ', '+' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0 && int.TryParse(parts[0], out parsedValue))
                    {
                        displayedLevel = parsedValue;
                    }
                }
                else if (!cleanText.Contains("+") && cleanText.Length > 0)
                {
                    // 스킬 이름 텍스트 (숫자가 아닌 것)
                    bool isNumber = true;
                    foreach (char c in cleanText)
                    {
                        if (!char.IsDigit(c) && c != ' ')
                        {
                            isNumber = false;
                            break;
                        }
                    }
                    if (!isNumber && nameText == null)
                    {
                        nameText = tmp;
                    }
                }
            }

            if (nameText == null || levelText == null) return;

            // 스킬 타입 찾기
            string skillName = nameText.text.Trim();
            var skillTypeNullable = SkillEffect.GetSkillTypeFromName(skillName);
            if (!skillTypeNullable.HasValue) return;

            Skills.SkillType skillType = skillTypeNullable.Value;

            // 우리 모드의 보너스 계산
            float bonusLevel = SkillEffect.GetSkillLevelBonus(skillType);
            if (bonusLevel <= 0f) return;

            int bonusLevelInt = Mathf.RoundToInt(bonusLevel);

            // 기본 레벨 계산 (표시된 값에서 우리 보너스 제외)
            int baseLevelInt = displayedLevel - bonusLevelInt;
            if (baseLevelInt < 0) baseLevelInt = 0;

            // 포맷 생성 - 기본 레벨 + 빨간색 보너스만 표시 (다른 모드 보너스 무시)
            string formattedText;
            if (baseLevelInt == 0)
            {
                // 기본 0 생략: "+보너스(빨간색)"만 표시
                formattedText = $"<color=#FF3333>+{bonusLevelInt}</color>";
            }
            else
            {
                // "기본 +보너스(빨간색)"
                formattedText = $"{baseLevelInt} <color=#FF3333>+{bonusLevelInt}</color>";
            }

            // 텍스트 적용
            levelText.richText = true;
            levelText.enableVertexGradient = false;
            levelText.overflowMode = TextOverflowModes.Overflow;
            levelText.text = formattedText;
            levelText.ForceMeshUpdate();

            Plugin.Log.LogDebug($"[숙련도 UI] {skillType}: {baseLevelInt} +{bonusLevelInt}");
        }
    }
}

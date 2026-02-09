using System;
using System.Collections;
using System.Reflection;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// 몬스터 레벨 및 별 표시 HUD (WackyEpicMMO 방식 참고)
    /// EnemyHud 패치로 몬스터 머리 위에 레벨 표시
    /// </summary>
    [HarmonyPatch]
    public static class CaptainMonsterHud
    {
        // 레벨 텍스트 위치 설정 (이름 옆)
        private static readonly Vector2 MobLevelPosition = new Vector2(37, -30);
        private static readonly Vector2 BossLevelPosition = new Vector2(50, -35);

        // Reflection 캐시
        private static FieldInfo _m_hudsField;
        private static FieldInfo _m_guiField;

        /// <summary>
        /// Reflection 필드 초기화
        /// </summary>
        private static void InitReflection()
        {
            if (_m_hudsField == null)
            {
                _m_hudsField = typeof(EnemyHud).GetField("m_huds",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        /// <summary>
        /// HudData에서 m_gui 가져오기 (Reflection)
        /// </summary>
        private static GameObject GetHudGui(object hudData)
        {
            if (hudData == null) return null;

            if (_m_guiField == null)
            {
                _m_guiField = hudData.GetType().GetField("m_gui",
                    BindingFlags.Public | BindingFlags.Instance);
            }

            return _m_guiField?.GetValue(hudData) as GameObject;
        }

        /// <summary>
        /// EnemyHud.ShowHud 패치 - 몬스터 체력바 표시 시 레벨도 표시
        /// </summary>
        [HarmonyPatch(typeof(EnemyHud), "ShowHud")]
        [HarmonyPriority(1)] // 거의 마지막에 실행
        [HarmonyPostfix]
        public static void ShowHud_Postfix(EnemyHud __instance, Character c)
        {
            try
            {
                // EpicMMO 사용 중이면 스킵 (EpicMMO가 처리)
                if (CaptainMMOBridge.UseEpicMMO) return;

                // 자체 시스템 비활성화 시 스킵
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;

                // 길들여진 동물은 처리하지 않음
                try { if (c.IsTamed()) return; } catch { }

                // 플레이어는 처리하지 않음
                if (c.IsPlayer()) return;

                // m_huds 필드 가져오기 (Reflection)
                InitReflection();
                if (_m_hudsField == null) return;

                var hudsDict = _m_hudsField.GetValue(__instance) as IDictionary;
                if (hudsDict == null || !hudsDict.Contains(c)) return;

                var hudData = hudsDict[c];
                GameObject hudGui = GetHudGui(hudData);
                if (hudGui == null) return;

                // 이미 레벨 텍스트가 있으면 스킵
                Transform existingLevel = hudGui.transform.Find("Name/CaptainLevel(Clone)");
                if (existingLevel != null) return;

                // 몬스터 이름으로 레벨 조회
                string monsterName = GetMonsterName(c);

                int monsterLevel = CaptainMonsterExp.GetLevel(monsterName);
                int starLevel = c.GetLevel() - 1; // Valheim 별 레벨

                // 별 레벨 보너스 적용 (설정에 따라)
                if (CaptainLevelConfig.MobLevelPerStar.Value)
                {
                    monsterLevel += starLevel;
                }

                // 플레이어 레벨 가져오기
                int playerLevel = CaptainLevelSystem.Instance?.Level ?? 1;
                int maxLevelExp = playerLevel + CaptainLevelConfig.MaxLevelExp.Value;
                int minLevelExp = playerLevel - CaptainLevelConfig.MinLevelExp.Value;

                // Name 오브젝트 찾기
                GameObject nameComponent = hudGui.transform.Find("Name")?.gameObject;
                if (nameComponent == null) return;

                // Name 복제하여 레벨 텍스트 생성 (WackyMMO 방식)
                GameObject levelTextObj = UnityEngine.Object.Instantiate(nameComponent, nameComponent.transform);
                levelTextObj.name = "CaptainLevel(Clone)";

                // 위치 설정
                var rect = levelTextObj.GetComponent<RectTransform>();
                rect.anchoredPosition = c.IsBoss() ? BossLevelPosition : MobLevelPosition;

                // WackyMMO 방식: 레벨 0이면 ??? 표시
                string levelString;
                Color levelColor;

                if (CaptainMonsterExp.GetLevel(monsterName) == 0)
                {
                    levelString = "[???]";
                    levelColor = Color.yellow;
                }
                else
                {
                    string starStr = GetStarString(starLevel);
                    levelString = $"[Lv.{monsterLevel}]{starStr}";

                    // 색상 결정 (플레이어 레벨 대비)
                    if (monsterLevel > maxLevelExp)
                        levelColor = Color.red; // 강한 몬스터
                    else if (monsterLevel < minLevelExp)
                        levelColor = Color.cyan; // 약한 몬스터
                    else
                        levelColor = Color.white; // 적정 레벨
                }

                // TextMeshProUGUI 설정
                var tmpText = levelTextObj.GetComponent<TextMeshProUGUI>();
                if (tmpText != null)
                {
                    tmpText.overflowMode = TextOverflowModes.Overflow;
                    tmpText.text = levelString;
                    tmpText.color = levelColor;
                }

                // ContentSizeFitter 추가 (텍스트 길이에 맞게)
                var fitter = levelTextObj.GetComponent<ContentSizeFitter>();
                if (fitter == null)
                {
                    fitter = levelTextObj.AddComponent<ContentSizeFitter>();
                }
                fitter.SetLayoutHorizontal();

                // 이름 색상도 동일하게 변경
                var nameText = nameComponent.GetComponent<TextMeshProUGUI>();
                if (nameText != null)
                {
                    nameText.color = levelColor;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainMonsterHud] ShowHud 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// EnemyHud.UpdateHuds 패치 - HUD 업데이트 시 레벨 텍스트 유지 및 색상 업데이트
        /// </summary>
        [HarmonyPatch(typeof(EnemyHud), "UpdateHuds")]
        [HarmonyPostfix]
        public static void UpdateHuds_Postfix(EnemyHud __instance)
        {
            try
            {
                // EpicMMO 사용 중이면 스킵
                if (CaptainMMOBridge.UseEpicMMO) return;

                // 자체 시스템 비활성화 시 스킵
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;

                // m_huds 필드 가져오기 (Reflection)
                InitReflection();
                if (_m_hudsField == null) return;

                var hudsDict = _m_hudsField.GetValue(__instance) as IDictionary;
                if (hudsDict == null) return;

                // 모든 HUD 업데이트
                foreach (DictionaryEntry entry in hudsDict)
                {
                    Character c = entry.Key as Character;
                    if (c == null) continue;
                    try { if (c.IsTamed()) continue; } catch { }
                    if (c.IsPlayer()) continue;

                    GameObject hudGui = GetHudGui(entry.Value);
                    if (hudGui == null) continue;

                    // 몬스터 레벨 정보
                    string monsterName = GetMonsterName(c);

                    int monsterLevel = CaptainMonsterExp.GetLevel(monsterName);
                    int starLevel = c.GetLevel() - 1;

                    if (CaptainLevelConfig.MobLevelPerStar.Value)
                    {
                        monsterLevel += starLevel;
                    }

                    // 플레이어 레벨 비교
                    int playerLevel = CaptainLevelSystem.Instance?.Level ?? 1;
                    int maxLevelExp = playerLevel + CaptainLevelConfig.MaxLevelExp.Value;
                    int minLevelExp = playerLevel - CaptainLevelConfig.MinLevelExp.Value;

                    // WackyMMO 방식: 레벨 0이면 ??? 표시
                    string levelString;
                    Color levelColor;

                    if (CaptainMonsterExp.GetLevel(monsterName) == 0)
                    {
                        levelString = "[???]";
                        levelColor = Color.yellow;
                    }
                    else
                    {
                        string starStr = GetStarString(starLevel);
                        levelString = $"[Lv.{monsterLevel}]{starStr}";

                        if (monsterLevel > maxLevelExp)
                            levelColor = Color.red;
                        else if (monsterLevel < minLevelExp)
                            levelColor = Color.cyan;
                        else
                            levelColor = Color.white;
                    }

                    // 기존 레벨 텍스트 찾기
                    Transform levelTransform = hudGui.transform.Find("Name/CaptainLevel(Clone)");
                    if (levelTransform != null)
                    {
                        levelTransform.gameObject.SetActive(true);

                        var tmpText = levelTransform.GetComponent<TextMeshProUGUI>();
                        if (tmpText != null)
                        {
                            tmpText.text = levelString;
                            tmpText.color = levelColor;
                        }
                    }
                    else
                    {
                        // 레벨 텍스트가 없으면 생성
                        GameObject nameComponent = hudGui.transform.Find("Name")?.gameObject;
                        if (nameComponent != null)
                        {
                            GameObject levelTextObj = UnityEngine.Object.Instantiate(nameComponent, nameComponent.transform);
                            levelTextObj.name = "CaptainLevel(Clone)";

                            var rect = levelTextObj.GetComponent<RectTransform>();
                            rect.anchoredPosition = c.IsBoss() ? BossLevelPosition : MobLevelPosition;

                            var tmpText = levelTextObj.GetComponent<TextMeshProUGUI>();
                            if (tmpText != null)
                            {
                                tmpText.overflowMode = TextOverflowModes.Overflow;
                                tmpText.text = levelString;
                                tmpText.color = levelColor;
                            }

                            var fitter = levelTextObj.GetComponent<ContentSizeFitter>();
                            if (fitter == null)
                            {
                                fitter = levelTextObj.AddComponent<ContentSizeFitter>();
                            }
                            fitter.SetLayoutHorizontal();
                        }
                    }

                    // 이름 색상도 업데이트
                    var nameText = hudGui.transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
                    if (nameText != null)
                    {
                        nameText.color = levelColor;
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainMonsterHud] UpdateHuds 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 별 문자열 생성
        /// </summary>
        private static string GetStarString(int starLevel)
        {
            if (starLevel <= 0) return "";
            if (starLevel == 1) return "<color=#FFD700>★</color>";
            if (starLevel == 2) return "<color=#FFD700>★★</color>";
            return $"<color=#FF4500>★x{starLevel}</color>";
        }

        /// <summary>
        /// 몬스터 이름 가져오기 (WackyMMO 방식: gameObject.name 직접 사용)
        /// </summary>
        private static string GetMonsterName(Character character)
        {
            // WackyMMO 방식: c.gameObject.name 직접 사용 (Clone 포함)
            return character.gameObject.name;
        }
    }
}

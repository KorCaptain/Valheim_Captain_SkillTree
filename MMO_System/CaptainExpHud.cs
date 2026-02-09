using System;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// 플레이어 레벨 및 경험치 HUD (WackyEpicMMO 방식 참고)
    /// 화면에 레벨/경험치 바 표시
    /// </summary>
    [HarmonyPatch]
    public static class CaptainExpHud
    {
        // UI 요소
        private static GameObject _expPanel;
        private static Text _levelText;
        private static Text _expText;
        private static Image _expBarFill;
        private static GameObject _expBarBackground;

        // 설정
        private static readonly Vector2 PanelPosition = new Vector2(-200, 40); // 화면 하단 중앙 위
        private static readonly Vector2 PanelSize = new Vector2(300, 50);
        private static readonly Color ExpBarColor = new Color(0.4f, 0.8f, 1f, 1f); // 하늘색
        private static readonly Color ExpBarBgColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        /// <summary>
        /// Hud.Awake 패치 - HUD 생성 시 경험치 패널도 생성
        /// </summary>
        [HarmonyPatch(typeof(Hud), "Awake")]
        [HarmonyPostfix]
        public static void Hud_Awake_Postfix(Hud __instance)
        {
            try
            {
                // EpicMMO 사용 중이면 스킵 (EpicMMO가 처리)
                if (CaptainMMOBridge.UseEpicMMO) return;

                // 자체 시스템 비활성화 시 스킵
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;

                // HUD 표시 비활성화 시 스킵
                if (!CaptainLevelConfig.ShowLevelHUD.Value) return;

                CreateExpPanel(__instance);
                Plugin.Log.LogDebug("[CaptainExpHud] 경험치 HUD 생성 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainExpHud] Hud_Awake 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// Game.SpawnPlayer 패치 - 플레이어 스폰 시 경험치 바 업데이트
        /// </summary>
        [HarmonyPatch(typeof(Game), "SpawnPlayer")]
        [HarmonyPostfix]
        public static void Game_SpawnPlayer_Postfix()
        {
            try
            {
                if (CaptainMMOBridge.UseEpicMMO) return;
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;
                if (!CaptainLevelConfig.ShowLevelHUD.Value) return;

                UpdateExpBar();

                if (_expPanel != null)
                    _expPanel.SetActive(true);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] SpawnPlayer 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// Hud.Update 패치 - 주기적으로 경험치 바 업데이트
        /// </summary>
        [HarmonyPatch(typeof(Hud), "Update")]
        [HarmonyPostfix]
        public static void Hud_Update_Postfix(Hud __instance)
        {
            try
            {
                if (CaptainMMOBridge.UseEpicMMO) return;
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;
                if (!CaptainLevelConfig.ShowLevelHUD.Value) return;

                // 30프레임마다 업데이트 (성능 최적화)
                if (Time.frameCount % 30 == 0)
                {
                    UpdateExpBar();
                }
            }
            catch { }
        }

        /// <summary>
        /// Hud.SetVisible 패치 - HUD 보이기/숨기기
        /// </summary>
        [HarmonyPatch(typeof(Hud), "SetVisible")]
        [HarmonyPostfix]
        public static void Hud_SetVisible_Postfix(bool visible)
        {
            try
            {
                if (CaptainMMOBridge.UseEpicMMO) return;
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;

                if (_expPanel != null)
                    _expPanel.SetActive(visible && CaptainLevelConfig.ShowLevelHUD.Value);
            }
            catch { }
        }

        /// <summary>
        /// 경험치 패널 생성
        /// </summary>
        private static void CreateExpPanel(Hud hud)
        {
            if (_expPanel != null) return;

            // 메인 패널 생성
            _expPanel = new GameObject("CaptainExpPanel");
            _expPanel.transform.SetParent(hud.m_rootObject.transform, false);

            // RectTransform 설정 (화면 하단 중앙)
            var panelRect = _expPanel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0);
            panelRect.anchorMax = new Vector2(0.5f, 0);
            panelRect.pivot = new Vector2(0.5f, 0);
            panelRect.anchoredPosition = PanelPosition;
            panelRect.sizeDelta = PanelSize;

            // 배경
            _expBarBackground = new GameObject("Background");
            _expBarBackground.transform.SetParent(_expPanel.transform, false);
            var bgRect = _expBarBackground.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var bgImage = _expBarBackground.AddComponent<Image>();
            bgImage.color = ExpBarBgColor;

            // 경험치 바 배경
            var barBg = new GameObject("BarBackground");
            barBg.transform.SetParent(_expPanel.transform, false);
            var barBgRect = barBg.AddComponent<RectTransform>();
            barBgRect.anchorMin = new Vector2(0, 0);
            barBgRect.anchorMax = new Vector2(1, 0.4f);
            barBgRect.offsetMin = new Vector2(10, 5);
            barBgRect.offsetMax = new Vector2(-10, 0);
            var barBgImage = barBg.AddComponent<Image>();
            barBgImage.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);

            // 경험치 바 Fill
            var barFill = new GameObject("BarFill");
            barFill.transform.SetParent(barBg.transform, false);
            var barFillRect = barFill.AddComponent<RectTransform>();
            barFillRect.anchorMin = Vector2.zero;
            barFillRect.anchorMax = Vector2.one;
            barFillRect.offsetMin = new Vector2(2, 2);
            barFillRect.offsetMax = new Vector2(-2, -2);
            _expBarFill = barFill.AddComponent<Image>();
            _expBarFill.color = ExpBarColor;
            _expBarFill.type = Image.Type.Filled;
            _expBarFill.fillMethod = Image.FillMethod.Horizontal;
            _expBarFill.fillAmount = 0f;

            // 레벨 텍스트 (왼쪽)
            var levelObj = new GameObject("LevelText");
            levelObj.transform.SetParent(_expPanel.transform, false);
            var levelRect = levelObj.AddComponent<RectTransform>();
            levelRect.anchorMin = new Vector2(0, 0.4f);
            levelRect.anchorMax = new Vector2(0.3f, 1);
            levelRect.offsetMin = new Vector2(10, 0);
            levelRect.offsetMax = new Vector2(0, -5);
            _levelText = levelObj.AddComponent<Text>();
            _levelText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            _levelText.fontSize = 16;
            _levelText.fontStyle = FontStyle.Bold;
            _levelText.alignment = TextAnchor.MiddleLeft;
            _levelText.color = Color.white;
            _levelText.text = "Lv. 1";

            // Outline 추가
            var levelOutline = levelObj.AddComponent<Outline>();
            levelOutline.effectColor = Color.black;
            levelOutline.effectDistance = new Vector2(1, -1);

            // 경험치 텍스트 (오른쪽)
            var expObj = new GameObject("ExpText");
            expObj.transform.SetParent(_expPanel.transform, false);
            var expRect = expObj.AddComponent<RectTransform>();
            expRect.anchorMin = new Vector2(0.3f, 0.4f);
            expRect.anchorMax = new Vector2(1, 1);
            expRect.offsetMin = new Vector2(0, 0);
            expRect.offsetMax = new Vector2(-10, -5);
            _expText = expObj.AddComponent<Text>();
            _expText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            _expText.fontSize = 14;
            _expText.alignment = TextAnchor.MiddleRight;
            _expText.color = Color.white;
            _expText.text = "0 / 300 (0.00%)";

            // Outline 추가
            var expOutline = expObj.AddComponent<Outline>();
            expOutline.effectColor = Color.black;
            expOutline.effectDistance = new Vector2(1, -1);

            _expPanel.SetActive(false);
        }

        /// <summary>
        /// 경험치 바 업데이트
        /// </summary>
        public static void UpdateExpBar()
        {
            try
            {
                if (_levelText == null || _expText == null || _expBarFill == null) return;

                var levelSystem = CaptainLevelSystem.Instance;
                if (levelSystem == null) return;

                // 스킬포인트 기반 레벨 정보 가져오기
                var levelInfo = CaptainMMOBridge.GetLevelInfo();
                int level = levelInfo.level;

                long currentExp = levelSystem.CurrentExp;
                long needExp = levelSystem.GetExpToNextLevel();

                // 레벨 텍스트 (스킬포인트 기반 모드일 때 추가 정보 표시)
                if (levelInfo.isSkillPointBased)
                {
                    _levelText.text = $"Lv. {level} (SP)";
                }
                else
                {
                    _levelText.text = $"Lv. {level}";
                }

                // 경험치 텍스트 (스킬포인트 기반 모드일 때는 사용 포인트 표시)
                if (levelInfo.isSkillPointBased)
                {
                    // 스킬포인트 기반 모드: 사용한 포인트 / 레벨당 필요 포인트 표시
                    int nextLevelPoints = (level + 1) * levelInfo.pointsPerLevel;
                    float spPercent = levelInfo.pointsPerLevel > 0
                        ? (float)(levelInfo.usedPoints % levelInfo.pointsPerLevel) / levelInfo.pointsPerLevel * 100f
                        : 0f;
                    _expText.text = $"{levelInfo.usedPoints} SP ({levelInfo.pointsPerLevel}pt/Lv)";

                    // 경험치 바: 다음 레벨까지의 진행률
                    int pointsInCurrentLevel = levelInfo.usedPoints % levelInfo.pointsPerLevel;
                    _expBarFill.fillAmount = levelInfo.pointsPerLevel > 0
                        ? (float)pointsInCurrentLevel / levelInfo.pointsPerLevel
                        : 0f;
                }
                else
                {
                    // 기존 경험치 모드
                    float percent = needExp > 0 ? (float)currentExp / needExp * 100f : 0f;
                    _expText.text = $"{currentExp:N0} / {needExp:N0} ({percent:F2}%)";
                    _expBarFill.fillAmount = needExp > 0 ? (float)currentExp / needExp : 0f;
                }

                // 레벨에 따른 색상 변화 (선택적)
                if (level >= 50)
                    _expBarFill.color = new Color(1f, 0.8f, 0.2f, 1f); // 금색
                else if (level >= 30)
                    _expBarFill.color = new Color(0.8f, 0.4f, 1f, 1f); // 보라색
                else if (level >= 15)
                    _expBarFill.color = new Color(0.4f, 1f, 0.4f, 1f); // 초록색
                else
                    _expBarFill.color = ExpBarColor; // 기본 하늘색
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] UpdateExpBar 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 경험치 획득 팝업 표시
        /// </summary>
        public static void ShowExpGainPopup(int expGained)
        {
            try
            {
                if (!CaptainLevelConfig.ShowExpPopup.Value) return;
                if (Player.m_localPlayer == null) return;

                // MessageHud를 통해 메시지 표시
                var msg = $"<color=#00FFFF>+{expGained} EXP</color>";
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.TopLeft, msg);
            }
            catch { }
        }

        /// <summary>
        /// 레벨업 알림 표시
        /// </summary>
        public static void ShowLevelUpNotification(int newLevel)
        {
            try
            {
                if (Player.m_localPlayer == null) return;

                // 레벨업 메시지
                var msg = $"<color=#FFD700>LEVEL UP! Lv.{newLevel}</color>";
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center, msg);

                // 레벨업 이펙트 (설정에 따라)
                if (CaptainLevelConfig.ShowLevelUpEffect.Value)
                {
                    PlayLevelUpEffect();
                }
            }
            catch { }
        }

        /// <summary>
        /// 레벨업 이펙트 재생
        /// </summary>
        private static void PlayLevelUpEffect()
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;

                // Valheim 기본 이펙트 사용
                var effectPos = player.transform.position + Vector3.up * 1f;

                // 스킬 레벨업 이펙트
                var skillEffect = ZNetScene.instance?.GetPrefab("fx_skillup");
                if (skillEffect != null)
                {
                    UnityEngine.Object.Instantiate(skillEffect, effectPos, Quaternion.identity);
                }

                // 추가: Guardian Power 획득 이펙트 (화려함)
                var gpEffect = ZNetScene.instance?.GetPrefab("fx_GP_activated");
                if (gpEffect != null)
                {
                    UnityEngine.Object.Instantiate(gpEffect, effectPos, Quaternion.identity);
                }
            }
            catch { }
        }

        /// <summary>
        /// HUD 표시/숨기기 토글
        /// </summary>
        public static void ToggleExpHud(bool show)
        {
            if (_expPanel != null)
                _expPanel.SetActive(show);
        }

        /// <summary>
        /// HUD 위치 조정
        /// </summary>
        public static void SetPosition(Vector2 position)
        {
            if (_expPanel != null)
            {
                var rect = _expPanel.GetComponent<RectTransform>();
                if (rect != null)
                    rect.anchoredPosition = position;
            }
        }
    }
}

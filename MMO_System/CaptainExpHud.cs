using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain Exp HUD - WackyEpicMMOSystem 스타일 가로 HUD
    /// Prefix 패치 방식 (UpdateHealth / UpdateStamina / UpdateEitr)
    /// 드래그: IDragHandler (CaptainHudDrag.cs)
    /// 레이아웃: HorizontalLayoutGroup 1050 / 1475px (에이트르 유무)
    /// EpicMMO가 없을 때만 활성화됨
    /// </summary>
    [HarmonyPatch]
    public static class CaptainExpHud
    {
        // ──────────────────────────────────────────
        // UI 컴포넌트 캐시
        // ──────────────────────────────────────────

        private static GameObject _hudCanvas;
        private static CaptainHudBuilder.HudComponents _comp;
        private static bool _initialized;

        // 바닐라 HP 패널 (숨기기용)
        private static GameObject _vanillaHealth;
        private static GameObject _vanillaHealthIcon;

        // ──────────────────────────────────────────
        // Harmony Patches - 빌드
        // ──────────────────────────────────────────

        [HarmonyPatch(typeof(Hud), "Awake")]
        [HarmonyPostfix]
        public static void Hud_Awake_Postfix(Hud __instance)
        {
            try
            {
                if (CaptainMMOBridge.UseEpicMMO) return;
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;
                if (!CaptainLevelConfig.ShowLevelHUD.Value) return;

                Build(__instance);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainExpHud] Hud_Awake 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(Game), "SpawnPlayer")]
        [HarmonyPostfix]
        public static void Game_SpawnPlayer_Postfix()
        {
            try
            {
                if (!_initialized) return;
                UpdateExpBar();
                _hudCanvas?.SetActive(true);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] SpawnPlayer 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(Hud), "SetVisible")]
        [HarmonyPostfix]
        public static void Hud_SetVisible_Postfix(bool visible)
        {
            try
            {
                if (!_initialized) return;
                _hudCanvas?.SetActive(visible && CaptainLevelConfig.ShowLevelHUD.Value);
            }
            catch { }
        }

        [HarmonyPatch(typeof(Game), "Logout")]
        [HarmonyPostfix]
        public static void Game_Logout_Postfix()
        {
            try
            {
                _hudCanvas?.SetActive(false);
                _initialized = false;
            }
            catch { }
        }

        // ──────────────────────────────────────────
        // Harmony Patches - Prefix (WackyEpicMMOSystem 패턴)
        // UpdateHealth / UpdateStamina / UpdateEitr 는 이벤트 기반으로만 실행
        // ──────────────────────────────────────────

        [HarmonyPatch(typeof(Hud), "UpdateHealth")]
        [HarmonyPrefix]
        public static bool Hud_UpdateHealth_Prefix(Player player)
        {
            if (!_initialized || player == null) return true;

            try
            {
                UpdateBar(_comp.HpBarFill, _comp.HpText,
                    player.GetHealth(), player.GetMaxHealth());
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] UpdateHealth 오류: {ex.Message}");
                return true;
            }
            return false;
        }

        [HarmonyPatch(typeof(Hud), "UpdateStamina")]
        [HarmonyPrefix]
        public static bool Hud_UpdateStamina_Prefix(Player player)
        {
            if (!_initialized || player == null) return true;

            try
            {
                UpdateBar(_comp.StaminaBarFill, _comp.StaminaText,
                    player.GetStamina(), player.GetMaxStamina());
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] UpdateStamina 오류: {ex.Message}");
                return true;
            }
            return false;
        }

        [HarmonyPatch(typeof(Hud), "UpdateEitr")]
        [HarmonyPrefix]
        public static bool Hud_UpdateEitr_Prefix(Player player)
        {
            if (!_initialized || player == null) return true;

            try
            {
                float eitrMax = player.GetMaxEitr();
                bool  hasEitr = eitrMax > 2f;

                // 에이트르 패널 표시/숨김 + 패널 크기 조정
                if (_comp.EitrPanel != null && _comp.EitrPanel.activeSelf != hasEitr)
                {
                    _comp.EitrPanel.SetActive(hasEitr);
                    CaptainHudBuilder.ResizePanelForEitr(_comp.PanelRt, hasEitr);
                }

                if (hasEitr)
                    UpdateBar(_comp.EitrBarFill, _comp.EitrText, player.GetEitr(), eitrMax);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] UpdateEitr 오류: {ex.Message}");
                return true;
            }
            return false;
        }

        // ──────────────────────────────────────────
        // HUD 빌드
        // ──────────────────────────────────────────

        private static void Build(Hud hud)
        {
            if (_initialized) return;

            // 바닐라 HP 패널 숨기기
            _vanillaHealth     = hud.m_healthPanel?.Find("Health")?.gameObject;
            _vanillaHealthIcon = hud.m_healthPanel?.Find("healthicon")?.gameObject;
            _vanillaHealth?.SetActive(false);
            _vanillaHealthIcon?.SetActive(false);

            // HudBuilder로 전체 UI 빌드
            _comp      = CaptainHudBuilder.Build();
            _hudCanvas = _comp.Canvas.gameObject;

            // CaptainLevelSystem 이벤트 구독 (경험치/레벨업 시 HUD 자동 갱신)
            var ls = CaptainLevelSystem.Instance;
            ls.OnExpGain += (_, gained) => { UpdateExpBar(); ShowExpGainPopup((int)gained); };
            ls.OnLevelUp += (level)     => { UpdateExpBar(); ShowLevelUpNotification(level); };

            _initialized = true;
            _hudCanvas.SetActive(false);
            Plugin.Log.LogInfo("[CaptainExpHud] WackyEpicMMOSystem 스타일 HUD 빌드 완료");
        }

        // ──────────────────────────────────────────
        // 업데이트 로직
        // ──────────────────────────────────────────

        private static void UpdateBar(Image fill, Text text, float current, float max)
        {
            if (fill == null || text == null) return;

            float ratio = max > 0f ? Mathf.Clamp01(current / max) : 0f;
            fill.fillAmount = ratio;

            // 글로우 (형제 오브젝트 "Glow")
            var glowImg = fill.transform.parent?.Find("Glow")?.GetComponent<Image>();
            if (glowImg != null) glowImg.fillAmount = ratio;

            text.text = $"{Mathf.CeilToInt(current)}/{Mathf.CeilToInt(max)}";
        }

        // ──────────────────────────────────────────
        // 공개 API (외부 참조 유지)
        // ──────────────────────────────────────────

        /// <summary>경험치 획득 팝업 표시 (CaptainPartyExp.cs에서 호출)</summary>
        public static void ShowExpGainPopup(int expGained)
        {
            try
            {
                if (!CaptainLevelConfig.ShowExpPopup.Value) return;
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.TopLeft,
                    $"<color=#C8A020>+{expGained:N0} EXP</color>");
            }
            catch { }
        }

        /// <summary>레벨업 알림 + 이펙트</summary>
        public static void ShowLevelUpNotification(int newLevel)
        {
            try
            {
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center,
                    $"<color=#FFD700>✦ LEVEL UP! ✦ LV.{newLevel}</color>");

                if (!CaptainLevelConfig.ShowLevelUpEffect.Value) return;

                // EpicMMO LevelUpVFX (플레이어 Spine2 본에 재생)
                CaptainLevelUpVFX.Play();
                CaptainSkillTree.Audio.HeroAwakensSoundManager.Instance.PlayOnce();
            }
            catch { }
        }

        /// <summary>HUD 표시/숨김 토글</summary>
        public static void ToggleExpHud(bool show)
            => _hudCanvas?.SetActive(show && _initialized);

        /// <summary>경험치 바 즉시 갱신</summary>
        public static void UpdateExpBar()
        {
            try
            {
                if (!_initialized || _comp.LevelText == null) return;

                var ls = CaptainLevelSystem.Instance;
                if (ls == null) return;

                int   level = CaptainMMOBridge.GetLevel();  // EpicMMO 또는 백업 레벨
                long  cur   = ls.CurrentExp;
                long  need  = ls.GetExpToNextLevel();
                float ratio = need > 0 ? Mathf.Clamp01((float)cur / need) : 0f;

                _comp.LevelText.text  = $"Lv.{level}";
                _comp.LevelText.color = LevelColor(level);

                _comp.ExpPercentText.text = $"{ratio * 100f:F2} %";

                var expColor = Color.white;
                _comp.ExpBarFill.fillAmount = ratio;
                _comp.ExpBarFill.color      = expColor;

                if (_comp.ExpBarGlow != null)
                {
                    _comp.ExpBarGlow.fillAmount = ratio;
                    _comp.ExpBarGlow.color = new Color(1f, 1f, 1f, 0.60f);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainExpHud] UpdateExpBar 오류: {ex.Message}");
            }
        }

        // ──────────────────────────────────────────
        // 레벨 색상 (레벨 구간별)
        // ──────────────────────────────────────────

        private static Color LevelColor(int level)
        {
            if (level >= 80) return CaptainHudBuilder.COL_LVL_PLAT;
            if (level >= 50) return CaptainHudBuilder.COL_LVL_GOLD;
            if (level >= 25) return CaptainHudBuilder.COL_LVL_SILVER;
            return CaptainHudBuilder.COL_LVL_DEFAULT;
        }

        private static Color ExpBarColor(int level)
        {
            if (level >= 80) return CaptainHudBuilder.COL_LVL_PLAT;
            if (level >= 50) return new Color(1.00f, 0.60f, 0.05f, 1f); // 금 (미세 조정)
            if (level >= 25) return new Color(0.60f, 0.30f, 0.80f, 1f); // 어두운 보라 (대비 개선)
            return new Color(0.784f, 0.471f, 0.125f, 1f);                // #C87820 따뜻한 앰버
        }

        // ──────────────────────────────────────────
        // 헬퍼
        // ──────────────────────────────────────────

    }
}

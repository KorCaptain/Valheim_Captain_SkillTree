using System.Collections;
using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// MMO 몬스터 난이도 시스템 Harmony 패치
    ///  - Character.Start: 몬스터 스폰 시 m_level 자동 조정
    ///  - Player.Start: 접속 시 난이도 알림 표시 (EpicMMO 메시지 이후 6초 대기)
    ///  - Terminal.InitTerminal: captaindifficulty 콘솔 명령어 등록
    /// </summary>
    [HarmonyPatch]
    public static class MMODifficultyPatches
    {
        // 세션당 1회만 접속 알림을 표시하기 위한 플래그
        private static bool _notificationShown = false;

        // ============================================================
        //  몬스터 스폰 시 m_level 조정
        // ============================================================

        [HarmonyPatch(typeof(Character), "Start")]
        [HarmonyPostfix]
        public static void Character_Start_Postfix(Character __instance)
        {
            try
            {
                // 1. 시스템 비활성화 시 스킵
                if (MMODifficultyConfig.EnableMMODifficulty == null ||
                    !MMODifficultyConfig.EnableMMODifficulty.Value)
                    return;
                if (CaptainLevelConfig.EnableCaptainLevel != null &&
                    !CaptainLevelConfig.EnableCaptainLevel.Value)
                    return;

                // 2. 플레이어 자신은 스킵
                if (__instance.IsPlayer()) return;

                // 3. 길들인 몬스터는 스킵
                if (__instance.IsTamed()) return;

                // 4. 로컬 플레이어 미존재 시 스킵
                if (Player.m_localPlayer == null) return;

                // 5. ZNetView 유효성 확인
                var nview = __instance.GetComponent<ZNetView>();
                if (nview == null || !nview.IsValid()) return;

                // 6. 이미 적용된 몬스터는 스킵
                if (MMODifficultyManager.IsAlreadyApplied(__instance)) return;

                // 7. 총 사용 SP 조회
                int totalSP = MMODifficultyManager.GetTotalSP();

                // 8. 최종 보너스 계산
                int bonus = MMODifficultyManager.CalculateFinalLevelBonus(totalSP);

                // 9. m_level 적용 (최소 1 보장)
                if (bonus > 0)
                {
                    int newLevel = Mathf.Max(1, __instance.GetLevel() + bonus);
                    __instance.SetLevel(newLevel);
                    Plugin.Log.LogInfo(
                        $"[MMODifficulty] 몬스터 레벨 상향: {__instance.name} +{bonus}레벨 " +
                        $"(SP:{totalSP}, 최종 레벨:{newLevel})");
                }

                // 10. 적용 완료 마킹
                MMODifficultyManager.MarkAsApplied(__instance);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogDebug($"[MMODifficulty] Character_Start 패치 오류: {ex.Message}");
            }
        }

        // ============================================================
        //  난이도 알림 외부 트리거 (LevelSyncManager에서 연동 메시지 후 호출)
        // ============================================================

        /// <summary>
        /// LevelSyncManager에서 연동 메시지 표시 완료 후 1초 뒤 호출
        /// </summary>
        public static void TriggerDifficultyNotification()
        {
            if (MMODifficultyConfig.EnableMMODifficulty == null ||
                !MMODifficultyConfig.EnableMMODifficulty.Value)
                return;
            if (CaptainLevelConfig.EnableCaptainLevel != null &&
                !CaptainLevelConfig.EnableCaptainLevel.Value)
                return;

            if (_notificationShown) return;
            _notificationShown = true;

            if (Plugin.Instance != null)
                Plugin.Instance.StartCoroutine(ShowDifficultyNotificationDelayed(1f));
        }

        private static IEnumerator ShowDifficultyNotificationDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);

            try
            {
                if (MessageHud.instance == null) yield break;

                int totalSP = MMODifficultyManager.GetTotalSP();
                float divider = MMODifficultyConfig.StarChanceDivider.Value > 0
                    ? MMODifficultyConfig.StarChanceDivider.Value : 3f;
                float chance = System.Math.Min(
                    MMODifficultyConfig.BaseStarChance.Value + totalSP / divider,
                    MMODifficultyConfig.MaxStarChance.Value);

                string title  = L.Get("mmo_diff_notification_title");
                string detail = string.Format(L.Get("mmo_diff_notification_detail"), totalSP, (int)chance);
                string msg    = $"<color=#FF8C00>{title}</color>\n{detail}";

                MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, msg);
                Plugin.Log.LogInfo($"[MMODifficulty] 난이도 알림 표시 (SP:{totalSP}, 별확률:{(int)chance}%)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogDebug($"[MMODifficulty] 알림 표시 실패: {ex.Message}");
            }
        }

        // ============================================================
        //  콘솔 명령어 등록
        // ============================================================

        [HarmonyPatch(typeof(Terminal), "InitTerminal")]
        [HarmonyPostfix]
        public static void Terminal_InitTerminal_Postfix()
        {
            new Terminal.ConsoleCommand(
                "captaindifficulty",
                "Show MMO difficulty status (SP, star chance, level bonus)",
                args =>
                {
                    int sp = MMODifficultyManager.GetTotalSP();
                    float divider = MMODifficultyConfig.StarChanceDivider?.Value ?? 3f;
                    float chance = System.Math.Min(
                        (MMODifficultyConfig.BaseStarChance?.Value ?? 25f) + sp / divider,
                        MMODifficultyConfig.MaxStarChance?.Value ?? 100f);
                    int spBonus = MMODifficultyManager.GetSpLevelBonus(sp);
                    bool hasActive = MMODifficultyManager.HasActiveSkill();
                    bool hasJob    = MMODifficultyManager.HasJobSkill();
                    bool enabled   = MMODifficultyConfig.EnableMMODifficulty?.Value ?? false;

                    args.Context.AddString("=== MMO 몬스터 난이도 시스템 ===");
                    args.Context.AddString($"활성화: {enabled}");
                    args.Context.AddString($"현재 SP: {sp}");
                    args.Context.AddString($"별 추가 확률: {(int)chance}%  (기본:{MMODifficultyConfig.BaseStarChance?.Value ?? 25f}% + SP/{divider})");
                    args.Context.AddString($"SP 기반 레벨 보너스: +{spBonus} (max:{MMODifficultyConfig.MaxLevelBonus?.Value ?? 10})");
                    args.Context.AddString($"액티브 스킬 보너스: {(hasActive ? "+1 확정" : "없음")}");
                    args.Context.AddString($"직업 스킬 보너스: {(hasJob ? "+1 확정" : "없음")}");
                    args.Context.AddString("================================");
                });
        }

        // ============================================================
        //  세션 플래그 초기화 (게임 재시작 대응)
        // ============================================================

        [HarmonyPatch(typeof(Game), "Logout")]
        [HarmonyPostfix]
        public static void Game_Logout_Postfix()
        {
            _notificationShown = false;
            LevelSyncManager.Instance.OnLogout();
            Plugin.Log.LogDebug("[MMODifficulty] 세션 플래그 초기화");
        }
    }
}

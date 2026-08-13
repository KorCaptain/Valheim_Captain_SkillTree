using System.IO;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.Gui;

namespace CaptainSkillTree
{
    /// <summary>
    /// 난이도 선택 시스템 — Plugin 부분 클래스.
    /// FejdStartup(메인 메뉴) Awake Postfix 에서
    /// 모드 설치·업데이트 시 DifficultySelectUI 를 표시.
    /// Plugin.cs 를 수정하지 않고 별도 partial 파일로 분리.
    /// </summary>
    public partial class Plugin
    {
        // ───────────────────────────────────────────────────────────
        // 1회성 마이그레이션 백업 — Plugin.Awake 이전에 실행
        // static 필드 초기화자는 인스턴스 생성 직전(Awake 전)에 실행됨.
        // Plugin.Awake가 버전 변경 시 cfg를 삭제하기 전에 User 백업 생성.
        // ───────────────────────────────────────────────────────────
        private static readonly bool _earlyUserBackup = CreateEarlyUserBackup();

        private static bool CreateEarlyUserBackup()
        {
            try
            {
                string configPath = Path.Combine(Paths.ConfigPath, "CaptainSkillTree.SkillTreeMod.cfg");
                string presetDir  = Path.Combine(Paths.ConfigPath, "CaptainSkillTree");
                string userBackup = Path.Combine(presetDir, "User_CaptainSkillTree.SkillTreeMod.cfg");

                if (File.Exists(configPath))
                {
                    Directory.CreateDirectory(presetDir);
                    File.Copy(configPath, userBackup, overwrite: true);
                    // 이 시점은 Plugin.Log 미초기화 — 로그 불가
                }
            }
            catch { /* 초기화 단계, 조용히 무시 */ }
            return true;
        }

        // ───────────────────────────────────────────────────────────
        // FejdStartup 패치 — 메인 메뉴 로드 직후 난이도 선택 창 표시
        // ───────────────────────────────────────────────────────────
        [HarmonyPatch(typeof(FejdStartup), "Awake")]
        static class FejdStartup_DifficultyPatch
        {
            [HarmonyPostfix]
            static void Postfix(FejdStartup __instance)
            {
                // 버전 비교 후 선택 필요 여부 결정 (1회 초기화)
                DifficultyManager.InitializeIfNeeded();

                if (!DifficultyManager.NeedsSelection) return;

                // Canvas 찾기 — FejdStartup 하위 또는 씬 전체에서 탐색
                var canvas = __instance.GetComponentInChildren<Canvas>(includeInactive: true)
                          ?? Object.FindObjectOfType<Canvas>();

                if (canvas == null)
                {
                    Log.LogWarning("[Difficulty] Canvas를 찾을 수 없어 난이도 선택 창을 열 수 없습니다. 이전 설정 유지 또는(최초 설치 시) VeryHard 적용.");
                    DifficultyManager.ApplyFallbackDefault();
                    return;
                }

                var go = new GameObject("DifficultySelectUI");
                go.transform.SetParent(canvas.transform, false);
                go.AddComponent<DifficultySelectUI>();

                // 최상위 레이어에 배치 (다른 UI 위에 표시)
                go.transform.SetAsLastSibling();


            }
        }
    }
}

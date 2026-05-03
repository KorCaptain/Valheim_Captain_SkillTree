using System;
using CaptainSkillTree.SkillTree;
using HarmonyLib;
using Jotunn.Managers;

namespace CaptainSkillTree
{
    // FejdStartup.Awake Postfix: 메인 메뉴 진입 시 커맨드 등록
    // Plugin.cs의 IEnumerator Start()와 충돌 없이 커맨드 추가
    [HarmonyPatch(typeof(FejdStartup), "Awake")]
    public static class SkillConfigCommandRegister
    {
        private static bool _registered = false;

        static void Postfix()
        {
            if (_registered) return;
            try
            {
                CommandManager.Instance.AddConsoleCommand(new SkillConfigConsoleCommand());
                _registered = true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Jotunn] skillconfig 커맨드 등록 실패: {ex.Message}");
            }
        }
    }
}

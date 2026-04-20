using BepInEx;
using HarmonyLib;
using CaptainSkillTree.Mods;

// Plugin.cs 수정 없이 BlacksmithingExpanded 소프트 의존성 선언
namespace CaptainSkillTree
{
    [BepInDependency(BlacksmithingBridge.GUID, BepInDependency.DependencyFlags.SoftDependency)]
    public partial class Plugin { }

    // FejdStartup.Awake 이후 실행 — 모든 BepInEx 플러그인 Awake 완료 후 BSE 런타임 패치 적용
    [HarmonyPatch(typeof(FejdStartup), "Awake")]
    internal static class BSE_LateInit_Patch
    {
        static void Postfix()
        {
            BlacksmithingBridge.Initialize();
        }
    }
}

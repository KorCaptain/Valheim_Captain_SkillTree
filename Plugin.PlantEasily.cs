using BepInEx;
using HarmonyLib;
using CaptainSkillTree.Mods;

// Plugin.cs 수정 없이 PlantEasily 소프트 의존성 선언
namespace CaptainSkillTree
{
    [BepInDependency("advize.PlantEasily", BepInDependency.DependencyFlags.SoftDependency)]
    public partial class Plugin { }

    // FejdStartup.Awake 이후 실행 — 모든 BepInEx 플러그인 Awake 완료 후 PlantEasily 런타임 패치 적용
    [HarmonyPatch(typeof(FejdStartup), "Awake")]
    internal static class PlantEasily_LateInit_Patch
    {
        static void Postfix()
        {
            PlantEasilyBridge.Initialize();
        }
    }
}

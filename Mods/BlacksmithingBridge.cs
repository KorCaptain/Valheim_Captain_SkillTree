using System;
using System.Reflection;
using BepInEx.Bootstrap;
using HarmonyLib;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.Mods
{
    internal static class BlacksmithingBridge
    {
        internal const string GUID = "org.bepinex.plugins.blacksmithingexpanded";

        public static bool IsInstalled { get; private set; }
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            IsInstalled = Chainloader.PluginInfos.ContainsKey(GUID);
            if (!IsInstalled) return;

            Plugin.Log.LogInfo("[BlacksmithingBridge] BlacksmithingExpanded 감지 - 제작 전문가 전용 모드");

            try
            {
                var bseAssembly = Chainloader.PluginInfos[GUID].Instance.GetType().Assembly;
                PatchApplyCraftingBonuses(bseAssembly);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[BlacksmithingBridge] 런타임 패치 실패 (무시): {ex.Message}");
            }
        }

        private static void PatchApplyCraftingBonuses(Assembly bseAssembly)
        {
            var harmony = new Harmony("CaptainSkillTree.BlacksmithingGate");

            foreach (var type in bseAssembly.GetTypes())
            {
                var method = type.GetMethod("ApplyCraftingBonuses",
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Static | BindingFlags.Instance);

                if (method == null) continue;

                var prefix = new HarmonyMethod(typeof(BlacksmithingBridge), nameof(BSE_ApplyCraftingBonuses_Prefix));
                harmony.Patch(method, prefix: prefix);
                Plugin.Log.LogInfo($"[BlacksmithingBridge] {type.Name}.ApplyCraftingBonuses 패치 완료");
                return;
            }

            Plugin.Log.LogWarning("[BlacksmithingBridge] ApplyCraftingBonuses 메서드 미발견 - 런타임 stat 패치만 적용");
        }

        // BSE ApplyCraftingBonuses Prefix — 비-제작 전문가면 실행 차단
        private static bool BSE_ApplyCraftingBonuses_Prefix()
        {
            return CanUse();
        }

        public static bool CanUse() =>
            !IsInstalled || ProducerSkills.IsProducer(Player.m_localPlayer);
    }
}

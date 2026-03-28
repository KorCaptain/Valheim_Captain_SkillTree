using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMO 패널 확장 — 현재 개발 중단 상태
    /// 재개 시 md/까마귀패널.md 참조
    /// </summary>
    public static class EpicMMOPanelExtension
    {
        public static void OnUIReload_Postfix(object __instance) { }

        public static void InvalidateCache() { }

        public static void OnInventoryGuiShow() { }
    }

    [HarmonyPatch(typeof(InventoryGui), "Show")]
    public static class InventoryGui_Show_EpicMMOPatch
    {
        public static void Postfix() { }
    }

    [HarmonyPatch]
    public static class MyUI_Show_EpicMMOPatch
    {
        static bool Prepare()
        {
            try
            {
                var t = Type.GetType("EpicMMOSystem.MyUI, EpicMMOSystem");
                return t?.GetMethod("Show",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null;
            }
            catch { return false; }
        }

        static System.Reflection.MethodBase TargetMethod()
        {
            var t = Type.GetType("EpicMMOSystem.MyUI, EpicMMOSystem");
            return t?.GetMethod("Show",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static void Postfix() { }
    }
}

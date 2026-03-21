using System;
using System.Reflection;
using HarmonyLib;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMO UIReload NullReferenceException 억제 패치
    /// PatchAll() 대신 수동 패치 사용 - EpicMMO 가용 확인 후 CaptainMMOBridge에서 호출
    /// </summary>
    public static class EpicMMOUIPatch
    {
        private static bool _applied = false;

        /// <summary>
        /// EpicMMO 가용 확인 후 수동으로 패치 적용
        /// CaptainMMOBridge.Initialize()에서 UseEpicMMO = true 확인 후 호출
        /// </summary>
        public static void TryApplyPatch()
        {
            if (_applied) return;
            if (!EpicMMOReflectionHelper.IsAvailable) return;

            try
            {
                var uiType = Type.GetType("EpicMMOSystem.EpicMMOSystemUI, EpicMMOSystem");
                var target = uiType?.GetMethod("UIReload",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (target == null)
                {
                    Plugin.Log?.LogDebug("[EpicMMO] UIReload 메서드 없음 - 패치 스킵");
                    return;
                }

                var finalizer = typeof(EpicMMOUIPatch).GetMethod(
                    nameof(Finalizer), BindingFlags.Public | BindingFlags.Static);

                var harmony = new Harmony("captainskilltree.epicmmo.uipatch");
                harmony.Patch(target, finalizer: new HarmonyMethod(finalizer));
                _applied = true;
                Plugin.Log?.LogInfo("[EpicMMO] UIReload NullRef 억제 패치 적용 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[EpicMMO] UIReload 패치 실패 (무시됨): {ex.Message}");
            }
        }

        public static Exception Finalizer(Exception __exception)
        {
            if (__exception is NullReferenceException)
            {
                Plugin.Log?.LogDebug("[EpicMMO] UIReload NullRef 억제 (UI 미초기화 상태)");
                return null; // 예외 억제
            }
            return __exception; // 다른 예외는 그대로 전달
        }
    }
}

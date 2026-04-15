using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.MMO_System;
using CaptainSkillTree.Audio;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// SkillTreeInputListener.Update를 Prefix로 완전 대체하여
    /// 설정 키(HotKeyY/R/G/H)를 사용하는 버전으로 동작시킵니다.
    /// SkillTreeInputListener.cs는 수정 금지이므로 패치로 처리합니다.
    /// </summary>
    [HarmonyPatch(typeof(SkillTreeInputListener), "Update")]
    public static class SkillTreeInputListener_KeyPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(SkillTreeInputListener __instance)
        {
            // === SkillTreeManager 업데이트 ===
            try { SkillTreeManager.Instance?.OnUpdate(); }
            catch (System.Exception e)
            {
                Plugin.Log.LogWarning($"[SkillTree] SkillTreeManager 업데이트 실패: {e.Message}");
            }

            // === LevelSyncManager 업데이트 ===
            try { LevelSyncManager.Instance?.Update(); }
            catch (System.Exception e)
            {
                Plugin.Log.LogWarning($"[SkillTree] LevelSyncManager 업데이트 실패: {e.Message}");
            }

            // === 채팅/콘솔 활성화 시 스킬 키 차단 ===
            if (IsChatOrConsoleOpen()) return false;

            var player = Player.m_localPlayer;
            if (player != null && !player.IsDead())
            {
                KeyCode keyY = SkillTreeConfig.GetHotKeyCode(SkillTreeConfig.HotKeyY, KeyCode.Y);
                KeyCode keyR = SkillTreeConfig.GetHotKeyCode(SkillTreeConfig.HotKeyR, KeyCode.R);
                KeyCode keyG = SkillTreeConfig.GetHotKeyCode(SkillTreeConfig.HotKeyG, KeyCode.G);
                KeyCode keyH = SkillTreeConfig.GetHotKeyCode(SkillTreeConfig.HotKeyH, KeyCode.H);

                if (Input.GetKeyDown(keyR))
                    SkillEffect.HandleRKeySkills(player);

                if (Input.GetKeyDown(keyG))
                    SkillEffect.HandleGKeySkills(player);

                if (Input.GetKeyUp(keyG))
                    SkillEffect.HandleGKeyUpSkills(player);

                if (Input.GetKeyDown(keyH))
                    SkillEffect.HandleHKeySkills(player);

                if (Input.GetKeyUp(keyH))
                    SkillEffect.HandleHKeyUpSkills(player);

                if (Input.GetKeyDown(keyY))
                    SkillTreeManager.Instance?.HandleActiveSkillKeyInput();

                // ] 키 - 디버그용 포인트 지급 (원본 동일)
                if (Input.GetKeyDown(KeyCode.RightBracket))
                {
                    try
                    {
                        long needExp = CaptainMMOBridge.GetExpToNextLevel();
                        CaptainMMOBridge.AddExp((int)needExp);
                        Plugin.SkillTreePoint += 3;
                    }
                    catch (System.Exception e)
                    {
                        Plugin.Log.LogWarning($"[SkillTree] 레벨업 실패: {e.Message}");
                    }
                }
            }

            // === UI가 열려 있을 때만 ESC/Tab 처리 ===
            if (__instance.skillTreeUI == null ||
                __instance.skillTreeUI.panel == null ||
                !__instance.skillTreeUI.panel.activeInHierarchy)
            {
                return false; // 원본 실행 건너뜀
            }

            bool escPressed = Input.GetKeyDown(KeyCode.Escape);
            bool tabPressed = Input.GetKeyDown(KeyCode.Tab);

            if (escPressed || tabPressed)
            {
                __instance.skillTreeUI.panel.SetActive(false);

                if (SkillTreeBGMManager.Instance != null)
                    SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
                else
                    Plugin.Log.LogError($"[KeyPatch] SkillTreeBGMManager.Instance가 null");
            }

            return false; // 원본 Update 실행 건너뜀
        }

        // === Reflection 메타 캐시 (타입/메서드 조회 1회) ===
        private static bool _reflCacheReady;
        private static System.Reflection.FieldInfo? _chatMInputField;   // Chat.m_input
        private static System.Reflection.PropertyInfo? _tmpIsFocusedProp; // TMP_InputField.isFocused
        private static System.Type? _tmpType;                            // TMP_InputField 타입
        private static System.Reflection.MethodInfo? _consoleIsVisible;

        /// <summary>
        /// Type/FieldInfo/MethodInfo를 1회만 조회하여 캐시.
        /// 이후 매 프레임은 GetValue + bool 읽기만 수행.
        /// </summary>
        internal static void EnsureReflCache()
        {
            if (_reflCacheReady) return;
            _reflCacheReady = true;

            // Chat.m_input 필드 (legacy InputField 또는 TMP_InputField)
            _chatMInputField = typeof(Chat).GetField("m_input",
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            // TMP_InputField 타입 조회 (없으면 null → TMP 경로 건너뜀)
            _tmpType = System.Type.GetType("TMPro.TMP_InputField, Unity.TextMeshPro")
                    ?? System.Type.GetType("TMPro.TMP_InputField, TMPro");
            _tmpIsFocusedProp = _tmpType?.GetProperty("isFocused");

            // Console.IsVisible 메서드
            var consoleType = System.Type.GetType("Console, assembly_valheim");
            _consoleIsVisible = consoleType?.GetMethod("IsVisible",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        }

        internal static bool IsChatOrConsoleOpen()
        {
            EnsureReflCache();

            if (Chat.instance != null)
            {
                // [1] Chat.m_input 직접 접근 (매 프레임 GetValue 1회 ≈ 수 나노초)
                //     컴포넌트 레퍼런스가 아닌 필드값을 매 프레임 읽어 stale 문제 방지
                var mInput = _chatMInputField?.GetValue(Chat.instance);

                if (mInput is UnityEngine.UI.InputField legacyInput && legacyInput.isFocused)
                    return true;

                // TMP_InputField인 경우 (TMPro 미사용 환경에선 _tmpIsFocusedProp == null → 건너뜀)
                if (mInput is UnityEngine.MonoBehaviour mb && _tmpIsFocusedProp != null
                    && _tmpType!.IsInstanceOfType(mb)
                    && (bool)(_tmpIsFocusedProp.GetValue(mb) ?? false))
                    return true;
            }

            // [2] EventSystem 폴백 - Chat.m_input이 null이거나 다른 UI가 포커스된 경우
            var selected = UnityEngine.EventSystems.EventSystem.current?.currentSelectedGameObject;
            if (selected != null)
            {
                if (selected.GetComponent<UnityEngine.UI.InputField>()?.isFocused == true)
                    return true;

                if (_tmpType != null && _tmpIsFocusedProp != null)
                {
                    var tmpComp = selected.GetComponent(_tmpType) as UnityEngine.MonoBehaviour;
                    if (tmpComp != null && (bool)(_tmpIsFocusedProp.GetValue(tmpComp) ?? false))
                        return true;
                }
            }

            // [3] Console.IsVisible
            if (_consoleIsVisible != null && (bool)_consoleIsVisible.Invoke(null, null))
                return true;

            return false;
        }
    }
}

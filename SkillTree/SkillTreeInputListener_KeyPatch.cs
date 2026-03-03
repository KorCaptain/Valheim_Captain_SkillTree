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

        private static bool IsChatOrConsoleOpen()
        {
            try
            {
                var eventSystem = UnityEngine.EventSystems.EventSystem.current;
                if (eventSystem?.currentSelectedGameObject != null)
                {
                    var input = eventSystem.currentSelectedGameObject.GetComponent<UnityEngine.UI.InputField>();
                    if (input != null && input.isFocused) return true;
                }

                if (Chat.instance != null && Chat.instance.gameObject.activeInHierarchy)
                {
                    try
                    {
                        var input = Chat.instance.GetComponentInChildren<UnityEngine.UI.InputField>(true);
                        if (input != null && input.isFocused) return true;
                    }
                    catch { }
                }

                var consoleType = System.Type.GetType("Console, assembly_valheim");
                if (consoleType != null)
                {
                    var method = consoleType.GetMethod("IsVisible",
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (method != null && (bool)method.Invoke(null, null)) return true;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[KeyPatch] 채팅/콘솔 감지 오류: {ex.Message}");
            }
            return false;
        }
    }
}

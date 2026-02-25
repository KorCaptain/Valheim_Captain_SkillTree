namespace CaptainSkillTree
{
    using CaptainSkillTree.Gui;
    using CaptainSkillTree.Audio;
    using CaptainSkillTree.MMO_System;
    using UnityEngine;

    public class SkillTreeInputListener : MonoBehaviour
    {
        public static SkillTreeInputListener? Instance { get; private set; }
        public SkillTreeUI? skillTreeUI;

        private float lastLogTime = 0f;

        void Awake()
        {
            lastLogTime = Time.time; // 초기화 시간 기록
            // Debug.Log($"[SkillTreeInputListener] Awake 호출됨, Instance={{Instance}}, this={{this.GetHashCode()}} (name={{gameObject.name}})"); // 제거: 과도한 Unity 로그
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                // Debug.Log($"[SkillTreeInputListener] Instance 등록, this={{this.GetHashCode()}} (name={{gameObject.name}})"); // 제거: 과도한 Unity 로그

                // LevelSyncManager 초기화
                LevelSyncManager.Instance.Initialize();
            }
            else
            {
                Debug.LogWarning($"[SkillTreeInputListener] 중복 생성 감지, 즉시 파괴, this={{this.GetHashCode()}} (name={{gameObject.name}})");
                Destroy(gameObject);
                return;
            }
        }

        void Update()
        {
            // === SkillTreeManager 업데이트 (멀티샷 타이머, 직업 스킬 상태 등) ===
            try
            {
                CaptainSkillTree.SkillTree.SkillTreeManager.Instance?.OnUpdate();
            }
            catch (System.Exception e)
            {
                Plugin.Log.LogWarning($"[SkillTree] SkillTreeManager 업데이트 실패: {e.Message}");
            }

            // === LevelSyncManager 업데이트 (EpicMMO 레벨 변화 감지) ===
            try
            {
                LevelSyncManager.Instance?.Update();
            }
            catch (System.Exception e)
            {
                Plugin.Log.LogWarning($"[SkillTree] LevelSyncManager 업데이트 실패: {e.Message}");
            }

            // === 채팅창/콘솔 활성화 시 키 입력 차단 ===
            if (IsChatOrConsoleOpen())
            {
                return; // 채팅창이나 콘솔이 열려있으면 모든 키 입력 무시
            }

            // === 이벤트 기반 액티브 스킬 처리 (입력 시에만 실행) ===
            var player = Player.m_localPlayer;
            if (player != null && !player.IsDead())
            {
                // R키 눌림 - 원거리 액티브 스킬
                if (Input.GetKeyDown(KeyCode.R))
                {
                    CaptainSkillTree.SkillTree.SkillEffect.HandleRKeySkills(player);
                }

                // G키 눌림 - 보조형 액티브 스킬 (차지 시작)
                if (Input.GetKeyDown(KeyCode.G))
                {
                    CaptainSkillTree.SkillTree.SkillEffect.HandleGKeySkills(player);
                }
                
                // G키 해제 - 분노의 망치 차지 해제
                if (Input.GetKeyUp(KeyCode.G))
                {
                    CaptainSkillTree.SkillTree.SkillEffect.HandleGKeyUpSkills(player);
                }
                
                // H키 눌림 - 보조형 액티브 스킬 (연공창, 분노의 망치 즉시 발동)
                if (Input.GetKeyDown(KeyCode.H))
                {
                    CaptainSkillTree.SkillTree.SkillEffect.HandleHKeySkills(player);
                }

                // H키 해제 - 사용하지 않음 (분노의 망치는 즉시 발동)
                if (Input.GetKeyUp(KeyCode.H))
                {
                    CaptainSkillTree.SkillTree.SkillEffect.HandleHKeyUpSkills(player);
                }
                
                // Y키 눌림 - 직업 액티브 스킬
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    CaptainSkillTree.SkillTree.SkillTreeManager.Instance.HandleActiveSkillKeyInput();
                }
                
                // 마우스 클릭 - 힐 파이어볼 시스템 제거됨 (H키 즉시 범위 힐로 변경)

                // 휠마우스 버튼 클릭은 더 이상 사용하지 않음 (연공창이 H키로 이동됨)

                // === ] 키 - 디버그용 레벨업/포인트 지급 ===
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
                        Plugin.Log.LogWarning($"[SkillTree] 레벨업 실패 (경험치 추가): {e.Message}");
                    }
                }
            }

            // UI가 열려 있을 때만 UI 관련 입력 체크
            if (skillTreeUI == null || skillTreeUI.panel == null || !skillTreeUI.panel.activeInHierarchy)
            {
                // 스킬트리가 닫혀있을 때는 입력 처리 안함
                return;
            }

            // 키 입력 체크
            bool escPressed = Input.GetKeyDown(KeyCode.Escape);
            bool tabPressed = Input.GetKeyDown(KeyCode.Tab);

            // ESC 키로 스킬트리 닫기 (강제 BGM 정지)
            if (escPressed)
            {
                skillTreeUI.panel.SetActive(false);

                // BGM 일시정지: 재생 위치 저장 후 일시정지
                if (SkillTreeBGMManager.Instance != null)
                {
                    SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
                }
                else
                {
                    Plugin.Log.LogError("[🔑 ESC] ❌ SkillTreeBGMManager.Instance가 null - ESC키 처리 실패");
                }
            }

            // Tab 키로 스킬트리 닫기 (BGM 일시정지)
            if (tabPressed)
            {
                skillTreeUI.panel.SetActive(false);

                // BGM 일시정지: 재생 위치 저장 후 일시정지
                if (SkillTreeBGMManager.Instance != null)
                {
                    SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
                }
                else
                {
                    Plugin.Log.LogError("[🔑 TAB] ❌ SkillTreeBGMManager.Instance가 null - Tab키 처리 실패");
                }
            }
        }

        /// <summary>
        /// 채팅창이나 콘솔이 열려있는지 확인
        /// </summary>
        /// <returns>채팅창 또는 콘솔이 열려있으면 true</returns>
        private bool IsChatOrConsoleOpen()
        {
            try
            {
                // 1. Chat 인스턴스 상태 확인 (다중 방법)
                if (IsChatInputActive())
                {
                    Plugin.Log.LogDebug("[키 입력 차단] 채팅 입력이 활성화됨 - 스킬 키 입력 차단");
                    return true;
                }

                // 2. Console (F5 콘솔) 상태 확인 - 더 강력한 감지 방법
                if (IsConsoleActive())
                {
                    Plugin.Log.LogDebug("[키 입력 차단] 콘솔이 활성화됨 - 스킬 키 입력 차단");
                    return true;
                }

                // 3. 기타 입력 필드 확인 (이미 위의 IsChatInputActive()에서 처리됨)

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[키 입력 차단] 채팅/콘솔 상태 확인 중 오류: {ex.Message}");
                return false; // 안전한 기본값 - 오류 시 키 입력 허용
            }
        }

        /// <summary>
        /// Valheim 채팅 입력이 활성화되어 있는지 확인 (경량화된 감지)
        /// </summary>
        /// <returns>채팅 입력이 활성화되어 있으면 true</returns>
        private bool IsChatInputActive()
        {
            try
            {
                // 방법 1: EventSystem 현재 선택 오브젝트 우선 확인 (가장 빠름)
                var eventSystem = UnityEngine.EventSystems.EventSystem.current;
                if (eventSystem?.currentSelectedGameObject != null)
                {
                    var inputField = eventSystem.currentSelectedGameObject.GetComponent<UnityEngine.UI.InputField>();
                    if (inputField != null && inputField.isFocused)
                    {
                        return true;
                    }
                }

                // 방법 2: Chat.instance 단순 확인 (백업) - 더 엄격한 null 체크
                if (Chat.instance != null)
                {
                    var chatGameObject = Chat.instance.gameObject;
                    if (chatGameObject != null && chatGameObject.activeInHierarchy)
                    {
                        try
                        {
                            var inputField = Chat.instance.GetComponentInChildren<UnityEngine.UI.InputField>(true); // includeInactive = true
                            if (inputField != null && inputField.isFocused)
                            {
                                return true;
                            }
                        }
                        catch (System.Exception innerEx)
                        {
                            // GetComponentInChildren 내부 오류만 무시하고 계속 진행
                            Plugin.Log.LogDebug($"[채팅 감지] GetComponentInChildren 실패: {innerEx.Message}");
                        }
                    }
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[채팅 감지] 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Valheim 콘솔(F5)이 활성화되어 있는지 확인 (경량화)
        /// </summary>
        /// <returns>콘솔이 열려있으면 true</returns>
        private bool IsConsoleActive()
        {
            try
            {
                // Console.IsVisible() 리플렉션 호출 (가장 확실한 방법)
                var consoleType = System.Type.GetType("Console, assembly_valheim");
                if (consoleType != null)
                {
                    var isVisibleMethod = consoleType.GetMethod("IsVisible", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (isVisibleMethod != null)
                    {
                        var isVisible = (bool)isVisibleMethod.Invoke(null, null);
                        if (isVisible)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[콘솔 감지] 오류: {ex.Message}");
                return false;
            }
        }

        void OnDestroy()
        {
            // OnDestroy - 모든 코루틴 정리
            StopAllCoroutines();

            // LevelSyncManager 정리
            LevelSyncManager.Instance?.Cleanup();

            if (Instance == this)
            {
                Instance = null;
                // Instance 해제됨
            }
        }
    }

    /// <summary>
    /// 플레이어 사망 시 SkillTreeInputListener의 모든 코루틴 강제 정리
    /// 무한 로딩 버그 방지를 위한 안전장치
    /// </summary>
    [HarmonyLib.HarmonyPatch(typeof(Player), "OnDeath")]
    public static class Player_OnDeath_StopCoroutines_Patch
    {
        [HarmonyLib.HarmonyPostfix]
        public static void Postfix(Player __instance)
        {
            if (__instance == Player.m_localPlayer && SkillTreeInputListener.Instance != null)
            {
                // 플레이어 사망: SkillTreeInputListener 모든 코루틴 강제 정리
                SkillTreeInputListener.Instance.StopAllCoroutines();
            }
        }
    }
} 
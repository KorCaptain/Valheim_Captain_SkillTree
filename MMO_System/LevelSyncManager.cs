using System;
using UnityEngine;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMO 레벨 동기화 시스템
    /// 레벨 변화(초기화, 커맨드 레벨업) 시 CaptainSkillTree의 스킬포인트를 자동으로 동기화
    /// </summary>
    public class LevelSyncManager
    {
        #region === Singleton ===

        private static LevelSyncManager _instance;
        public static LevelSyncManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelSyncManager();
                }
                return _instance;
            }
        }

        private LevelSyncManager() { }

        #endregion

        #region === Properties ===

        /// <summary>
        /// 이전 레벨 (변화 감지용)
        /// </summary>
        private int _lastKnownLevel = 0;

        /// <summary>
        /// 레벨 체크 간격 (초)
        /// </summary>
        private float _checkInterval = 1.0f;

        /// <summary>
        /// 마지막 체크 시간
        /// </summary>
        private float _lastCheckTime = 0f;

        /// <summary>
        /// 초기화 여부
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        #endregion

        #region === Events ===

        /// <summary>
        /// 레벨 변경 이벤트 (oldLevel, newLevel)
        /// </summary>
        public event Action<int, int> OnLevelChanged;

        /// <summary>
        /// 레벨 감소 이벤트 (newLevel)
        /// </summary>
        public event Action<int> OnLevelDecreased;

        /// <summary>
        /// 레벨 증가 이벤트 (newLevel)
        /// </summary>
        public event Action<int> OnLevelIncreased;

        #endregion

        #region === Initialize ===

        /// <summary>
        /// 레벨 동기화 매니저 초기화
        /// </summary>
        public void Initialize()
        {
            if (IsInitialized)
            {
                Plugin.Log.LogDebug("[LevelSyncManager] 이미 초기화됨");
                return;
            }

            try
            {
                // 현재 레벨로 초기화
                _lastKnownLevel = CaptainMMOBridge.GetLevel();
                _lastCheckTime = Time.time;

                // 이벤트 핸들러 등록
                OnLevelDecreased += HandleLevelDecreaseEvent;
                OnLevelIncreased += HandleLevelIncreaseEvent;

                IsInitialized = true;
                Plugin.Log.LogInfo($"[LevelSyncManager] 초기화 완료 - 현재 레벨: {_lastKnownLevel}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[LevelSyncManager] 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 리소스 정리
        /// </summary>
        public void Cleanup()
        {
            OnLevelDecreased -= HandleLevelDecreaseEvent;
            OnLevelIncreased -= HandleLevelIncreaseEvent;
            IsInitialized = false;
            Plugin.Log.LogDebug("[LevelSyncManager] 정리 완료");
        }

        #endregion

        #region === Update ===

        /// <summary>
        /// 매 프레임 호출 - 레벨 변화 감지
        /// SkillTreeInputListener.Update()에서 호출됨
        /// </summary>
        public void Update()
        {
            if (!IsInitialized) return;
            if (Player.m_localPlayer == null) return;

            // 체크 간격 확인
            if (Time.time - _lastCheckTime < _checkInterval) return;
            _lastCheckTime = Time.time;

            try
            {
                int currentLevel = CaptainMMOBridge.GetLevel();

                // 레벨 변화 감지 (이전 레벨이 0보다 큰 경우에만)
                if (currentLevel != _lastKnownLevel && _lastKnownLevel > 0)
                {
                    HandleLevelChange(_lastKnownLevel, currentLevel);
                }

                _lastKnownLevel = currentLevel;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[LevelSyncManager] Update 예외: {ex.Message}");
            }
        }

        #endregion

        #region === Level Change Handling ===

        /// <summary>
        /// 레벨 변화 처리
        /// </summary>
        private void HandleLevelChange(int oldLevel, int newLevel)
        {
            Plugin.Log.LogInfo($"[LevelSyncManager] 레벨 변화 감지: {oldLevel} -> {newLevel}");

            // 이벤트 발생
            OnLevelChanged?.Invoke(oldLevel, newLevel);

            if (newLevel < oldLevel)
            {
                // 레벨 감소: 스킬 초기화 필요 여부 확인
                HandleLevelDecrease(oldLevel, newLevel);
            }
            else
            {
                // 레벨 증가: UI 갱신
                HandleLevelIncrease(oldLevel, newLevel);
            }
        }

        /// <summary>
        /// 레벨 감소 처리 - 스킬포인트 부족 시 자동 초기화
        /// </summary>
        private void HandleLevelDecrease(int oldLevel, int newLevel)
        {
            var manager = SkillTreeManager.Instance;
            if (manager == null)
            {
                Plugin.Log.LogWarning("[LevelSyncManager] SkillTreeManager가 null입니다");
                return;
            }

            int pointsPerLevel = CaptainLevelConfig.SkillPointsPerLevel?.Value ?? 2;
            int bonusPoints = manager.GetBonusSkillPoints();
            int newMaxPoints = (newLevel * pointsPerLevel) + bonusPoints;
            int usedPoints = manager.GetTotalUsedPoints();

            Plugin.Log.LogInfo($"[LevelSyncManager] 레벨 감소 체크 - 사용포인트: {usedPoints}, 새 최대포인트: {newMaxPoints}");

            if (usedPoints > newMaxPoints)
            {
                // 스킬 자동 초기화
                Plugin.Log.LogWarning($"[LevelSyncManager] 스킬포인트 초과! ({usedPoints} > {newMaxPoints}) - 스킬 초기화 실행");
                manager.ResetAllSkillLevels();

                // MessageHud를 통한 알림
                ShowNotification($"<color=yellow>레벨 감소로 스킬이 초기화되었습니다</color>\n" +
                                 $"Lv.{oldLevel} → Lv.{newLevel}");

                Plugin.Log.LogInfo($"[LevelSyncManager] 레벨 감소로 스킬 초기화 완료: Lv.{oldLevel} → Lv.{newLevel}");
            }
            else
            {
                // 포인트가 충분하면 알림만
                ShowNotification($"<color=orange>레벨이 감소했습니다</color>\n" +
                                 $"Lv.{oldLevel} → Lv.{newLevel}");

                Plugin.Log.LogInfo($"[LevelSyncManager] 레벨 감소 (초기화 불필요): Lv.{oldLevel} → Lv.{newLevel}");
            }

            // 이벤트 발생
            OnLevelDecreased?.Invoke(newLevel);

            // UI 갱신
            RefreshUI();
        }

        /// <summary>
        /// 레벨 증가 처리 - UI 갱신 및 알림
        /// </summary>
        private void HandleLevelIncrease(int oldLevel, int newLevel)
        {
            int pointsPerLevel = CaptainLevelConfig.SkillPointsPerLevel?.Value ?? 2;
            int addedPoints = (newLevel - oldLevel) * pointsPerLevel;

            // MessageHud를 통한 알림
            // 게임 시작 시 (oldLevel이 0 또는 1이고 newLevel이 크게 점프한 경우) vs 실제 레벨업 구분
            bool isGameStart = (oldLevel <= 1 && (newLevel - oldLevel) > 1);

            if (isGameStart)
            {
                // 게임 시작 시 (첫 접속/로드) - EpicMMO 연동 메시지
                ShowNotification($"<color=green>EpicMMO 모드와 레벨 연동~!</color>\n" +
                                 $"+{addedPoints} 스킬포인트 (Lv.{newLevel})");
            }
            else
            {
                // 실제 레벨업 시
                ShowNotification($"<color=green>레벨 업!</color>\n" +
                                 $"+{addedPoints} 스킬포인트 (Lv.{newLevel})");
            }

            // 이벤트 발생
            OnLevelIncreased?.Invoke(newLevel);

            // UI 갱신
            RefreshUI();

            Plugin.Log.LogInfo($"[LevelSyncManager] 레벨 증가: Lv.{oldLevel} → Lv.{newLevel} (+{addedPoints} SP) [게임시작: {isGameStart}]");
        }

        #endregion

        #region === Event Handlers ===

        /// <summary>
        /// 레벨 감소 이벤트 핸들러
        /// </summary>
        private void HandleLevelDecreaseEvent(int newLevel)
        {
            Plugin.Log.LogDebug($"[LevelSyncManager] OnLevelDecreased 이벤트 처리: Lv.{newLevel}");
        }

        /// <summary>
        /// 레벨 증가 이벤트 핸들러
        /// </summary>
        private void HandleLevelIncreaseEvent(int newLevel)
        {
            Plugin.Log.LogDebug($"[LevelSyncManager] OnLevelIncreased 이벤트 처리: Lv.{newLevel}");
        }

        #endregion

        #region === Utility ===

        /// <summary>
        /// MessageHud를 통한 알림 표시
        /// </summary>
        private void ShowNotification(string message)
        {
            try
            {
                if (MessageHud.instance != null)
                {
                    MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, message);
                }
                else
                {
                    Plugin.Log.LogDebug($"[LevelSyncManager] MessageHud 없음, 로그만 출력: {message}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[LevelSyncManager] 알림 표시 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 스킬트리 UI 갱신
        /// </summary>
        private void RefreshUI()
        {
            try
            {
                // SkillTreeUI의 RefreshUI 호출
                var skillTreeUI = CaptainSkillTree.SkillTreeInputListener.Instance?.skillTreeUI;
                if (skillTreeUI != null && skillTreeUI.panel != null && skillTreeUI.panel.activeInHierarchy)
                {
                    // UI가 열려있으면 갱신
                    skillTreeUI.RefreshUI();
                    Plugin.Log.LogDebug("[LevelSyncManager] SkillTreeUI 갱신 완료");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[LevelSyncManager] UI 갱신 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 레벨 강제 동기화 (수동 호출용)
        /// </summary>
        public void ForceSync()
        {
            if (!IsInitialized) return;

            try
            {
                int currentLevel = CaptainMMOBridge.GetLevel();

                if (currentLevel != _lastKnownLevel)
                {
                    Plugin.Log.LogInfo($"[LevelSyncManager] 강제 동기화: {_lastKnownLevel} -> {currentLevel}");
                    HandleLevelChange(_lastKnownLevel, currentLevel);
                    _lastKnownLevel = currentLevel;
                }
                else
                {
                    Plugin.Log.LogInfo($"[LevelSyncManager] 레벨 변화 없음: Lv.{currentLevel}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[LevelSyncManager] 강제 동기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 상태 로그 출력 (디버깅용)
        /// </summary>
        public void LogStatus()
        {
            var manager = SkillTreeManager.Instance;

            Plugin.Log.LogInfo("=== LevelSyncManager 상태 ===");
            Plugin.Log.LogInfo($"초기화 여부: {IsInitialized}");
            Plugin.Log.LogInfo($"마지막 확인 레벨: {_lastKnownLevel}");
            Plugin.Log.LogInfo($"현재 레벨: {CaptainMMOBridge.GetLevel()}");
            Plugin.Log.LogInfo($"사용 포인트: {manager?.GetTotalUsedPoints() ?? 0}");
            Plugin.Log.LogInfo($"최대 포인트: {manager?.GetTotalMaxPoints() ?? 0}");
            Plugin.Log.LogInfo($"사용 가능 포인트: {manager?.GetAvailablePoints() ?? 0}");
            Plugin.Log.LogInfo("==============================");
        }

        #endregion
    }
}

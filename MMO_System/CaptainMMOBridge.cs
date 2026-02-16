using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain MMO Bridge
    /// EpicMMO와 자체 레벨 시스템 간 자동 스위치
    /// 듀얼 시스템 지원 + 양방향 데이터 마이그레이션
    /// </summary>
    public static class CaptainMMOBridge
    {
        #region === Properties ===

        public static bool UseEpicMMO { get; private set; } = false;
        public static bool IsInitialized { get; private set; } = false;
        public static string ActiveSystem => UseEpicMMO ? "EpicMMOSystem" : "CaptainLevelSystem";
        public static bool MigrationCompleted { get; private set; } = false;

        // 마이그레이션 키
        private const string KEY_MIGRATION_TO_EPIC = "CaptainSkillTree_MigrationToEpicMMO";
        private const string KEY_MIGRATION_TO_CAPTAIN = "CaptainSkillTree_MigrationToCaptain";

        // Captain Level 데이터 키
        private const string KEY_CAPTAIN_LEVEL = "CaptainSkillTree_Level";
        private const string KEY_CAPTAIN_CURRENT_EXP = "CaptainSkillTree_CurrentExp";
        private const string KEY_CAPTAIN_TOTAL_EXP = "CaptainSkillTree_TotalExp";

        // EpicMMO 백업 데이터 키 (EpicMMO 사용 중 주기적으로 백업)
        private const string KEY_EPICMMO_BACKUP_LEVEL = "CaptainSkillTree_EpicMMOBackup_Level";
        private const string KEY_EPICMMO_BACKUP_TOTAL_EXP = "CaptainSkillTree_EpicMMOBackup_TotalExp";

        // 레벨 마이그레이션 완료 플래그 (일회성)
        private const string KEY_LEVEL_MIGRATION_DONE = "CaptainSkillTree_LevelMigration_v1";

        #endregion

        #region === Initialize ===

        public static void Initialize()
        {
            if (IsInitialized)
            {
                Plugin.Log.LogDebug("[CaptainMMOBridge] 이미 초기화됨");
                return;
            }

            try
            {
                // 리플렉션 헬퍼 먼저 초기화
                EpicMMOReflectionHelper.Initialize();
                UseEpicMMO = EpicMMOReflectionHelper.IsAvailable;

                if (UseEpicMMO)
                {
                    CaptainLevelConfig.EnableCaptainLevel.Value = false;
                    Plugin.Log.LogInfo("[CaptainMMOBridge] EpicMMOSystem 감지됨 - 자체 레벨 시스템 OFF");
                }
                else
                {
                    CaptainLevelConfig.EnableCaptainLevel.Value = true;
                    CaptainLevelSystem.Instance.Initialize();
                    Plugin.Log.LogInfo("[CaptainMMOBridge] EpicMMOSystem 미감지 - 자체 레벨 시스템 ON");
                }

                IsInitialized = true;
                Plugin.Log.LogDebug($"[CaptainMMOBridge] 초기화 완료 - 활성 시스템: {ActiveSystem}");

                // JSON 파일 생성 옵션 체크
                TryGenerateJsonFiles();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOBridge] 초기화 실패: {ex.Message}");
                UseEpicMMO = false;
                CaptainLevelConfig.EnableCaptainLevel.Value = true;
                CaptainLevelSystem.Instance.Initialize();
                IsInitialized = true;
            }
        }

        private static bool IsEpicMMOAvailable()
        {
            try
            {
                var levelSystemType = Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
                if (levelSystemType == null)
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] EpicMMOSystem 타입 없음");
                    return false;
                }

                var instanceProperty = levelSystemType.GetProperty("Instance");
                if (instanceProperty == null)
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] EpicMMOSystem Instance 프로퍼티 없음");
                    return false;
                }

                Plugin.Log.LogDebug("[CaptainMMOBridge] EpicMMOSystem 어셈블리 감지됨");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainMMOBridge] EpicMMO 감지 실패: {ex.Message}");
                return false;
            }
        }

        public static bool CheckEpicMMORuntime()
        {
            if (!UseEpicMMO) return false;
            try
            {
                return EpicMMOReflectionHelper.HasInstance();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Config 옵션에 따라 JSON 파일 생성
        /// Config가 null이면 기본적으로 생성 (첫 실행 시)
        /// </summary>
        private static void TryGenerateJsonFiles()
        {
            try
            {
                // Config가 null이거나 true이면 생성 (기본 동작: 생성)
                bool shouldGenerate = CaptainLevelConfig.GenerateJsonFiles == null ||
                                      CaptainLevelConfig.GenerateJsonFiles.Value;

                if (!shouldGenerate)
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] JSON 파일 생성 비활성화됨");
                    return;
                }

                Plugin.Log.LogDebug($"[CaptainMMOBridge] JSON 파일 생성 시작 - 폴더: {CaptainExpJsonLoader.GetConfigFolderPath()}");

                // 파일이 없는 경우에만 생성
                if (!CaptainExpJsonLoader.HasMonsterExpJson())
                {
                    CaptainExpJsonLoader.CreateDefaultMonsterExpJson();
                    Plugin.Log.LogDebug("[CaptainMMOBridge] MonsterExp.json 기본 파일 생성됨");
                }
                else
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] MonsterExp.json 이미 존재");
                }

                if (!CaptainExpJsonLoader.HasLevelExpJson())
                {
                    CaptainExpJsonLoader.CreateDefaultLevelExpJson();
                    Plugin.Log.LogDebug("[CaptainMMOBridge] LevelExp.json 기본 파일 생성됨");
                }
                else
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] LevelExp.json 이미 존재");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOBridge] JSON 파일 생성 실패: {ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion

        #region === Unified API ===

        public static int GetLevel()
        {
            if (UseEpicMMO)
            {
                try
                {
                    // EpicMMO 있음: EpicMMO 레벨 사용 + 백업 저장
                    int epicLevel = EpicMMOReflectionHelper.GetLevel();
                    BackupLevel(epicLevel);  // 백업
                    return epicLevel;
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[CaptainMMOBridge] EpicMMO GetLevel 실패: {ex.Message}");
                    return 1;
                }
            }

            // EpicMMO 없음: 백업된 레벨 사용 (순환 참조 방지 - 계산하지 않음)
            return GetBackupLevel();
        }

        /// <summary>
        /// EpicMMO 레벨을 백업 저장 (EpicMMO 사용 중)
        /// </summary>
        private static void BackupLevel(int level)
        {
            if (Player.m_localPlayer == null) return;
            try
            {
                Player.m_localPlayer.m_customData[KEY_EPICMMO_BACKUP_LEVEL] = level.ToString();
                Plugin.Log.LogDebug($"[CaptainMMOBridge] 레벨 백업: {level}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainMMOBridge] 레벨 백업 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 백업된 레벨 가져오기 (EpicMMO 없을 때 사용)
        /// </summary>
        private static int GetBackupLevel()
        {
            if (Player.m_localPlayer == null) return 1;
            try
            {
                if (Player.m_localPlayer.m_customData.TryGetValue(KEY_EPICMMO_BACKUP_LEVEL, out string data))
                {
                    if (int.TryParse(data, out int level))
                    {
                        int result = Math.Max(1, level);
                        Plugin.Log.LogDebug($"[CaptainMMOBridge] 백업 레벨 사용: {result}");
                        return result;
                    }
                }
                // 백업 없으면 CaptainLevelSystem 레벨 사용
                int captainLevel = CaptainLevelSystem.Instance?.Level ?? 1;
                Plugin.Log.LogDebug($"[CaptainMMOBridge] 백업 없음, CaptainLevel 사용: {captainLevel}");
                return captainLevel;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[CaptainMMOBridge] GetBackupLevel 실패: {ex.Message}");
                return CaptainLevelSystem.Instance?.Level ?? 1;
            }
        }

        /// <summary>
        /// 일회성 레벨 마이그레이션 (레벨 11 같은 비정상 레벨 수정)
        /// 최대 스킬포인트 기준으로 올바른 레벨을 계산하여 백업에 저장
        /// </summary>
        public static void TryMigrateLevelIfNeeded()
        {
            if (Player.m_localPlayer == null) return;

            try
            {
                // 이미 마이그레이션 완료했으면 스킵
                if (Player.m_localPlayer.m_customData.ContainsKey(KEY_LEVEL_MIGRATION_DONE))
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] 레벨 마이그레이션 이미 완료됨");
                    return;
                }

                // EpicMMO 사용 중이면 마이그레이션 불필요
                if (UseEpicMMO)
                {
                    Player.m_localPlayer.m_customData[KEY_LEVEL_MIGRATION_DONE] = "1";
                    Plugin.Log.LogDebug("[CaptainMMOBridge] EpicMMO 사용 중 - 마이그레이션 스킵");
                    return;
                }

                // 현재 저장된 레벨
                int currentLevel = CaptainLevelSystem.Instance?.Level ?? 1;
                int pointsPerLevel = CaptainLevelConfig.SkillPointsPerLevel?.Value ?? 2;

                // 보너스 포인트 가져오기
                int bonusPoints = 0;
                if (Player.m_localPlayer != null)
                {
                    bonusPoints = SkillTree.SkillAddCommand.GetBonusSkillPoints(Player.m_localPlayer);
                }

                // 총 사용 포인트 가져오기
                int totalUsedPoints = 0;
                var manager = SkillTree.SkillTreeManager.Instance;
                if (manager != null)
                {
                    totalUsedPoints = manager.GetTotalUsedPoints();
                }

                // 사용 포인트 기준 예상 레벨 계산 (보너스 제외)
                // 공식: 예상 레벨 = 사용포인트 / 레벨당포인트
                int expectedLevel = totalUsedPoints / pointsPerLevel;
                expectedLevel = Math.Max(1, expectedLevel);

                Plugin.Log.LogInfo($"[CaptainMMOBridge] 레벨 마이그레이션 체크: 현재={currentLevel}, 사용포인트={totalUsedPoints}, 보너스={bonusPoints}, 예상레벨={expectedLevel}");

                // 저장된 레벨이 예상보다 50% 이상 낮고, 예상 레벨이 20 이상이면 마이그레이션
                if (currentLevel < expectedLevel * 0.5f && expectedLevel > 20)
                {
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] === 레벨 마이그레이션 시작 ===");
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] 현재 레벨({currentLevel})이 예상 레벨({expectedLevel})보다 현저히 낮음");

                    // 백업 레벨에 예상 레벨 저장
                    Player.m_localPlayer.m_customData[KEY_EPICMMO_BACKUP_LEVEL] = expectedLevel.ToString();

                    // CaptainLevelSystem 레벨도 업데이트
                    if (CaptainLevelSystem.Instance != null)
                    {
                        CaptainLevelSystem.Instance.SetLevel(expectedLevel);
                    }

                    Plugin.Log.LogInfo($"[CaptainMMOBridge] 레벨 마이그레이션 완료: {currentLevel} → {expectedLevel}");

                    // 사용자에게 메시지 표시
                    MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center,
                        $"<color=yellow>{L.Get("mmo_level_recovery_complete")}</color>\n{L.Get("mmo_level_recovery_detail", currentLevel, expectedLevel)}");
                }
                else
                {
                    Plugin.Log.LogDebug($"[CaptainMMOBridge] 레벨 마이그레이션 불필요 (현재={currentLevel}, 예상={expectedLevel})");
                }

                // 마이그레이션 완료 플래그 저장
                Player.m_localPlayer.m_customData[KEY_LEVEL_MIGRATION_DONE] = "1";
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOBridge] 레벨 마이그레이션 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 레벨 정보 반환 (UI 표시용)
        /// EpicMMO 없을 때: 저장된 레벨 사용 (투자/회수로 변하지 않음)
        /// </summary>
        public static (int level, int usedPoints, int pointsPerLevel, bool isSkillPointBased) GetLevelInfo()
        {
            // EpicMMO가 없으면 저장된 레벨 사용 (스킬포인트 투자/회수와 무관)
            // isSkillPointBased = false로 반환하여 경험치 바 형식 유지
            return (GetLevel(), 0, 0, false);
        }

        public static void AddExp(int exp)
        {
            if (exp <= 0) return;

            if (UseEpicMMO)
            {
                try
                {
                    EpicMMOReflectionHelper.AddExp(exp);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[CaptainMMOBridge] EpicMMO AddExp 실패: {ex.Message}");
                }
            }
            else
            {
                CaptainLevelSystem.Instance.AddExp(exp);
            }
        }

        public static long GetCurrentExp()
        {
            if (UseEpicMMO)
            {
                try
                {
                    return EpicMMOReflectionHelper.GetCurrentExp();
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[CaptainMMOBridge] EpicMMO GetCurrentExp 실패: {ex.Message}");
                    return 0;
                }
            }
            return CaptainLevelSystem.Instance.CurrentExp;
        }

        public static long GetExpToNextLevel()
        {
            if (UseEpicMMO)
            {
                try
                {
                    return EpicMMOReflectionHelper.GetNeedExp();
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[CaptainMMOBridge] EpicMMO GetExpToNextLevel 실패: {ex.Message}");
                    return 0;
                }
            }
            return CaptainLevelSystem.Instance.GetExpToNextLevel();
        }

        public static long GetTotalExp()
        {
            if (UseEpicMMO)
            {
                try
                {
                    return EpicMMOReflectionHelper.GetTotalExp();
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[CaptainMMOBridge] EpicMMO GetTotalExp 실패: {ex.Message}");
                    return 0;
                }
            }
            return CaptainLevelSystem.Instance.TotalExp;
        }

        public static float GetLevelProgress()
        {
            if (UseEpicMMO)
            {
                try
                {
                    long current = GetCurrentExp();
                    long need = GetExpToNextLevel();
                    if (need <= 0) return 1.0f;
                    return (float)current / need;
                }
                catch
                {
                    return 0f;
                }
            }
            return CaptainLevelSystem.Instance.GetLevelProgress();
        }

        public static int GetSkillPointsFromLevel()
        {
            int level = GetLevel();
            return level * CaptainLevelConfig.SkillPointsPerLevel.Value;
        }

        public static int GetMaxLevel()
        {
            return CaptainLevelConfig.MaxLevel.Value;
        }

        #endregion

        #region === Player Events ===

        public static void OnPlayerLoad()
        {
            if (!IsInitialized) Initialize();

            if (UseEpicMMO)
            {
                // EpicMMO 사용 - Captain -> EpicMMO 마이그레이션 체크
                TryMigrateToEpicMMO();

                // 스킬포인트 기반 레벨 -> EpicMMO 자동 동기화
                if (CaptainLevelConfig.AutoSyncToEpicMMO?.Value ?? true)
                {
                    TrySyncSkillPointLevelToEpicMMO();
                }
            }
            else
            {
                // Captain 사용 - EpicMMO 백업에서 복구 체크
                TryMigrateToCaptain();
                CaptainLevelSystem.Instance.Load();

                // 일회성 레벨 마이그레이션 (레벨 11 같은 비정상 레벨 수정)
                TryMigrateLevelIfNeeded();

                // 스킬포인트 기반 레벨 동기화
                if (CaptainLevelConfig.UseSkillPointBasedLevel?.Value ?? false)
                {
                    CaptainLevelSystem.Instance.SyncLevelFromSkillPoints();
                }
            }

            Plugin.Log.LogDebug($"[CaptainMMOBridge] 플레이어 로드 완료 - Lv.{GetLevel()}");
        }

        public static void OnPlayerSave()
        {
            if (UseEpicMMO)
            {
                // EpicMMO 사용 중 - 현재 데이터를 백업 저장 (나중에 EpicMMO 제거 시 복구용)
                BackupEpicMMOData();
            }
            else
            {
                CaptainLevelSystem.Instance.Save();
            }
        }

        #endregion

        #region === Migration: Captain -> EpicMMO ===

        public static void TryMigrateToEpicMMO()
        {
            var player = Player.m_localPlayer;
            if (player == null) return;

            try
            {
                if (IsMigrationToEpicCompleted(player))
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] Captain->EpicMMO 마이그레이션 이미 완료됨");
                    MigrationCompleted = true;
                    return;
                }

                if (!HasCaptainLevelData(player))
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] 마이그레이션할 Captain 데이터 없음");
                    MarkMigrationToEpicComplete(player);
                    return;
                }

                int captainLevel = GetCaptainLevelFromData(player);
                long captainTotalExp = GetCaptainTotalExpFromData(player);

                if (captainLevel <= 1 && captainTotalExp <= 0)
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] Captain 데이터가 기본값 - 스킵");
                    MarkMigrationToEpicComplete(player);
                    return;
                }

                int epicLevel = 1;
                long epicTotalExp = 0;
                try
                {
                    epicLevel = EpicMMOReflectionHelper.GetLevel();
                    epicTotalExp = EpicMMOReflectionHelper.GetTotalExp();
                }
                catch
                {
                    Plugin.Log.LogWarning("[CaptainMMOBridge] EpicMMO 레벨 조회 실패");
                }

                if (captainLevel > epicLevel || captainTotalExp > epicTotalExp)
                {
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] === Captain -> EpicMMO 마이그레이션 시작 ===");
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] Captain: Lv.{captainLevel}, TotalExp: {captainTotalExp:N0}");
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] EpicMMO: Lv.{epicLevel}, TotalExp: {epicTotalExp:N0}");

                    long expDiff = captainTotalExp - epicTotalExp;
                    if (expDiff > 0)
                    {
                        const int MAX_EXP_PER_CALL = 1000000;
                        while (expDiff > 0)
                        {
                            int addAmount = (int)Math.Min(expDiff, MAX_EXP_PER_CALL);
                            EpicMMOReflectionHelper.AddExp(addAmount);
                            expDiff -= addAmount;
                        }
                        Plugin.Log.LogInfo($"[CaptainMMOBridge] EpicMMO에 경험치 {captainTotalExp - epicTotalExp:N0} 추가");
                    }

                    int newEpicLevel = EpicMMOReflectionHelper.GetLevel();
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] 마이그레이션 후: Lv.{newEpicLevel}");

                    MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center,
                        $"<color=yellow>{L.Get("mmo_captain_to_epic_complete")}</color>\n{L.Get("mmo_captain_to_epic_detail", captainLevel, newEpicLevel)}");
                }
                else
                {
                    Plugin.Log.LogInfo("[CaptainMMOBridge] EpicMMO 레벨이 더 높음 - 마이그레이션 불필요");
                }

                MarkMigrationToEpicComplete(player);
                MigrationCompleted = true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOBridge] Captain->EpicMMO 마이그레이션 실패: {ex.Message}");
            }
        }

        #endregion

        #region === 스킬포인트 기반 레벨 -> EpicMMO 동기화 ===

        /// <summary>
        /// 스킬포인트 기반 레벨을 EpicMMO에 동기화
        /// EpicMMO는 SetLevel API가 없으므로 AddExp()로 목표 레벨까지 경험치 추가
        /// </summary>
        public static void TrySyncSkillPointLevelToEpicMMO()
        {
            if (!UseEpicMMO)
            {
                Plugin.Log.LogDebug("[CaptainMMOBridge] EpicMMO 미사용 - 동기화 스킵");
                return;
            }

            try
            {
                // 스킬포인트 기반 레벨 계산
                int skillPointLevel = CaptainLevelSystem.Instance.GetLevelFromSkillPoints();
                int epicLevel = EpicMMOReflectionHelper.GetLevel();

                if (skillPointLevel <= epicLevel)
                {
                    Plugin.Log.LogDebug($"[CaptainMMOBridge] EpicMMO 레벨({epicLevel})이 스킬포인트 레벨({skillPointLevel})보다 높거나 같음 - 동기화 불필요");
                    return;
                }

                Plugin.Log.LogInfo($"[CaptainMMOBridge] === 스킬포인트 -> EpicMMO 동기화 ===");
                Plugin.Log.LogInfo($"[CaptainMMOBridge] 스킬포인트 레벨: {skillPointLevel}, EpicMMO 레벨: {epicLevel}");

                // 목표 레벨까지 필요한 경험치 계산
                long currentTotalExp = EpicMMOReflectionHelper.GetTotalExp();
                long targetTotalExp = CaptainExpTable.GetTotalExpForLevel(skillPointLevel);

                if (targetTotalExp <= currentTotalExp)
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] 현재 경험치가 이미 충분함");
                    return;
                }

                long expDiff = targetTotalExp - currentTotalExp;
                Plugin.Log.LogInfo($"[CaptainMMOBridge] 경험치 차이: {expDiff:N0} (목표: {targetTotalExp:N0}, 현재: {currentTotalExp:N0})");

                // 경험치 추가 (분할하여 추가)
                const int MAX_EXP_PER_CALL = 1000000;
                while (expDiff > 0)
                {
                    int addAmount = (int)Math.Min(expDiff, MAX_EXP_PER_CALL);
                    EpicMMOReflectionHelper.AddExp(addAmount);
                    expDiff -= addAmount;
                }

                int newLevel = EpicMMOReflectionHelper.GetLevel();
                Plugin.Log.LogInfo($"[CaptainMMOBridge] 동기화 완료: Lv.{epicLevel} -> Lv.{newLevel}");

                // 메시지 표시
                MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center,
                    $"<color=yellow>{L.Get("mmo_skillpoint_sync_complete")}</color>\n{L.Get("mmo_skillpoint_sync_detail", epicLevel, newLevel)}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOBridge] 스킬포인트->EpicMMO 동기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 스킬포인트 기반 레벨을 EpicMMO에 강제 동기화 (콘솔 명령어용)
        /// </summary>
        public static bool ForceSyncSkillPointLevelToEpicMMO()
        {
            if (!UseEpicMMO)
            {
                Plugin.Log.LogWarning("[CaptainMMOBridge] EpicMMO 미사용 - 동기화 불가");
                return false;
            }

            TrySyncSkillPointLevelToEpicMMO();
            return true;
        }

        #endregion

        #region === Migration: EpicMMO Backup -> Captain ===

        /// <summary>
        /// EpicMMO 사용 중 데이터 백업 (나중에 EpicMMO 제거 시 복구용)
        /// </summary>
        private static void BackupEpicMMOData()
        {
            var player = Player.m_localPlayer;
            if (player == null) return;

            try
            {
                int level = EpicMMOReflectionHelper.GetLevel();
                long totalExp = EpicMMOReflectionHelper.GetTotalExp();

                player.m_customData[KEY_EPICMMO_BACKUP_LEVEL] = level.ToString();
                player.m_customData[KEY_EPICMMO_BACKUP_TOTAL_EXP] = totalExp.ToString();

                Plugin.Log.LogDebug($"[CaptainMMOBridge] EpicMMO 백업: Lv.{level}, Exp:{totalExp:N0}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainMMOBridge] EpicMMO 백업 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// EpicMMO 백업 데이터에서 Captain으로 복구
        /// </summary>
        public static void TryMigrateToCaptain()
        {
            var player = Player.m_localPlayer;
            if (player == null) return;

            try
            {
                if (IsMigrationToCaptainCompleted(player))
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] EpicMMO->Captain 마이그레이션 이미 완료됨");
                    return;
                }

                // EpicMMO 백업 데이터 확인
                if (!HasEpicMMOBackupData(player))
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] 복구할 EpicMMO 백업 데이터 없음");
                    MarkMigrationToCaptainComplete(player);
                    return;
                }

                int backupLevel = GetEpicMMOBackupLevel(player);
                long backupTotalExp = GetEpicMMOBackupTotalExp(player);

                if (backupLevel <= 1 && backupTotalExp <= 0)
                {
                    Plugin.Log.LogDebug("[CaptainMMOBridge] EpicMMO 백업 데이터가 기본값 - 스킵");
                    MarkMigrationToCaptainComplete(player);
                    return;
                }

                // 현재 Captain 데이터 확인
                int captainLevel = GetCaptainLevelFromData(player);
                long captainTotalExp = GetCaptainTotalExpFromData(player);

                // EpicMMO 백업이 더 높으면 복구
                if (backupLevel > captainLevel || backupTotalExp > captainTotalExp)
                {
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] === EpicMMO -> Captain 마이그레이션 시작 ===");
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] EpicMMO 백업: Lv.{backupLevel}, TotalExp: {backupTotalExp:N0}");
                    Plugin.Log.LogInfo($"[CaptainMMOBridge] Captain 현재: Lv.{captainLevel}, TotalExp: {captainTotalExp:N0}");

                    // Captain 데이터에 직접 저장
                    player.m_customData[KEY_CAPTAIN_LEVEL] = backupLevel.ToString();
                    player.m_customData[KEY_CAPTAIN_TOTAL_EXP] = backupTotalExp.ToString();

                    // CurrentExp 계산 (TotalExp에서 해당 레벨까지의 경험치 차감)
                    long levelExp = CaptainExpTable.GetTotalExpForLevel(backupLevel);
                    long currentExp = Math.Max(0, backupTotalExp - levelExp);
                    player.m_customData[KEY_CAPTAIN_CURRENT_EXP] = currentExp.ToString();

                    Plugin.Log.LogInfo($"[CaptainMMOBridge] Captain에 복구: Lv.{backupLevel}, CurrentExp:{currentExp:N0}");

                    MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center,
                        $"<color=yellow>{L.Get("mmo_epic_to_captain_complete")}</color>\n{L.Get("mmo_epic_to_captain_detail", backupLevel)}");
                }
                else
                {
                    Plugin.Log.LogInfo("[CaptainMMOBridge] Captain 레벨이 더 높음 - 복구 불필요");
                }

                MarkMigrationToCaptainComplete(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOBridge] EpicMMO->Captain 마이그레이션 실패: {ex.Message}");
            }
        }

        #endregion

        #region === Data Helpers ===

        private static bool HasCaptainLevelData(Player player)
        {
            return player.m_customData.ContainsKey(KEY_CAPTAIN_LEVEL) ||
                   player.m_customData.ContainsKey(KEY_CAPTAIN_TOTAL_EXP);
        }

        private static int GetCaptainLevelFromData(Player player)
        {
            if (player.m_customData.TryGetValue(KEY_CAPTAIN_LEVEL, out var lvlStr) &&
                int.TryParse(lvlStr, out int lvl))
                return lvl;
            return 1;
        }

        private static long GetCaptainTotalExpFromData(Player player)
        {
            if (player.m_customData.TryGetValue(KEY_CAPTAIN_TOTAL_EXP, out var expStr) &&
                long.TryParse(expStr, out long exp))
                return exp;
            return 0;
        }

        private static bool HasEpicMMOBackupData(Player player)
        {
            return player.m_customData.ContainsKey(KEY_EPICMMO_BACKUP_LEVEL) ||
                   player.m_customData.ContainsKey(KEY_EPICMMO_BACKUP_TOTAL_EXP);
        }

        private static int GetEpicMMOBackupLevel(Player player)
        {
            if (player.m_customData.TryGetValue(KEY_EPICMMO_BACKUP_LEVEL, out var lvlStr) &&
                int.TryParse(lvlStr, out int lvl))
                return lvl;
            return 1;
        }

        private static long GetEpicMMOBackupTotalExp(Player player)
        {
            if (player.m_customData.TryGetValue(KEY_EPICMMO_BACKUP_TOTAL_EXP, out var expStr) &&
                long.TryParse(expStr, out long exp))
                return exp;
            return 0;
        }

        private static bool IsMigrationToEpicCompleted(Player player)
        {
            return player.m_customData.ContainsKey(KEY_MIGRATION_TO_EPIC) &&
                   player.m_customData[KEY_MIGRATION_TO_EPIC] == "true";
        }

        private static void MarkMigrationToEpicComplete(Player player)
        {
            player.m_customData[KEY_MIGRATION_TO_EPIC] = "true";
            Plugin.Log.LogInfo("[CaptainMMOBridge] Captain->EpicMMO 마이그레이션 완료 플래그 설정");
        }

        private static bool IsMigrationToCaptainCompleted(Player player)
        {
            return player.m_customData.ContainsKey(KEY_MIGRATION_TO_CAPTAIN) &&
                   player.m_customData[KEY_MIGRATION_TO_CAPTAIN] == "true";
        }

        private static void MarkMigrationToCaptainComplete(Player player)
        {
            player.m_customData[KEY_MIGRATION_TO_CAPTAIN] = "true";
            Plugin.Log.LogInfo("[CaptainMMOBridge] EpicMMO->Captain 마이그레이션 완료 플래그 설정");
        }

        #endregion

        #region === Force Migration (Console Commands) ===

        /// <summary>
        /// Captain -> EpicMMO 강제 마이그레이션
        /// </summary>
        public static bool ForceMigrationToEpicMMO()
        {
            var player = Player.m_localPlayer;
            if (player == null)
            {
                Plugin.Log.LogWarning("[CaptainMMOBridge] 플레이어 없음");
                return false;
            }

            if (!UseEpicMMO)
            {
                Plugin.Log.LogWarning("[CaptainMMOBridge] EpicMMO 미사용 중");
                return false;
            }

            // 마이그레이션 플래그 리셋
            if (player.m_customData.ContainsKey(KEY_MIGRATION_TO_EPIC))
                player.m_customData.Remove(KEY_MIGRATION_TO_EPIC);
            MigrationCompleted = false;

            TryMigrateToEpicMMO();
            return true;
        }

        /// <summary>
        /// EpicMMO 백업 -> Captain 강제 마이그레이션
        /// </summary>
        public static bool ForceMigrationToCaptain()
        {
            var player = Player.m_localPlayer;
            if (player == null)
            {
                Plugin.Log.LogWarning("[CaptainMMOBridge] 플레이어 없음");
                return false;
            }

            if (UseEpicMMO)
            {
                Plugin.Log.LogWarning("[CaptainMMOBridge] EpicMMO 사용 중 - Captain 시스템으로 먼저 전환 필요");
                return false;
            }

            // 마이그레이션 플래그 리셋
            if (player.m_customData.ContainsKey(KEY_MIGRATION_TO_CAPTAIN))
                player.m_customData.Remove(KEY_MIGRATION_TO_CAPTAIN);

            TryMigrateToCaptain();

            // CaptainLevelSystem 재로드
            CaptainLevelSystem.Instance.Load();
            return true;
        }

        #endregion

        #region === Debug ===

        public static void LogStatus()
        {
            Plugin.Log.LogInfo("=== Captain MMO Bridge Status ===");
            Plugin.Log.LogInfo($"Active System: {ActiveSystem}");
            Plugin.Log.LogInfo($"UseEpicMMO: {UseEpicMMO}");
            Plugin.Log.LogInfo($"Level: {GetLevel()}");
            Plugin.Log.LogInfo($"Exp: {GetCurrentExp():N0}/{GetExpToNextLevel():N0}");
            Plugin.Log.LogInfo($"Progress: {GetLevelProgress() * 100:F1}%");
            Plugin.Log.LogInfo($"Skill Points: {GetSkillPointsFromLevel()}");

            // 스킬포인트 기반 레벨 정보
            var levelInfo = GetLevelInfo();
            Plugin.Log.LogInfo($"Skill Point Based Level: {levelInfo.isSkillPointBased}");
            if (levelInfo.isSkillPointBased)
            {
                Plugin.Log.LogInfo($"  Used Points: {levelInfo.usedPoints}");
                Plugin.Log.LogInfo($"  Points/Lv: {levelInfo.pointsPerLevel}");
                Plugin.Log.LogInfo($"  Calculated Level: {levelInfo.level}");
            }

            Plugin.Log.LogInfo("=================================");
        }

        public static void LogMigrationStatus()
        {
            var player = Player.m_localPlayer;
            Plugin.Log.LogInfo("=== Migration Status ===");
            Plugin.Log.LogInfo($"UseEpicMMO: {UseEpicMMO}");
            Plugin.Log.LogInfo($"MigrationCompleted: {MigrationCompleted}");

            if (player != null)
            {
                Plugin.Log.LogInfo($"Captain Data: Lv.{GetCaptainLevelFromData(player)}, Exp:{GetCaptainTotalExpFromData(player):N0}");
                Plugin.Log.LogInfo($"EpicMMO Backup: Lv.{GetEpicMMOBackupLevel(player)}, Exp:{GetEpicMMOBackupTotalExp(player):N0}");
                Plugin.Log.LogInfo($"ToEpic Flag: {IsMigrationToEpicCompleted(player)}");
                Plugin.Log.LogInfo($"ToCaptain Flag: {IsMigrationToCaptainCompleted(player)}");
            }
            Plugin.Log.LogInfo("========================");
        }

        public static void ForceSystem(bool useEpicMMO)
        {
            UseEpicMMO = useEpicMMO;
            CaptainLevelConfig.EnableCaptainLevel.Value = !useEpicMMO;

            if (!useEpicMMO && !CaptainLevelSystem.Instance.IsInitialized)
            {
                CaptainLevelSystem.Instance.Initialize();
                CaptainLevelSystem.Instance.Load();
            }

            Plugin.Log.LogInfo($"[CaptainMMOBridge] 시스템 강제 전환: {ActiveSystem}");
        }

        #endregion
    }
}

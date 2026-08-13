using System.IO;
using BepInEx;
using BepInEx.Configuration;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain Level System Config
    /// EpicMMO와 동일한 구조의 레벨/경험치 설정
    /// Config 최상단에 배치됨
    /// </summary>
    public static class CaptainLevelConfig
    {
        private const string SECTION = "- Captain Level System -";

        #region === 기본 설정 ===

        /// <summary>
        /// 자체 레벨 시스템 활성화 (EpicMMO 없을 때 자동 ON)
        /// </summary>
        public static ConfigEntry<bool> EnableCaptainLevel;

        /// <summary>
        /// 최대 레벨 (기본: 100)
        /// </summary>
        public static ConfigEntry<int> MaxLevel;

        #endregion

        #region === 경험치 설정 (EpicMMO 공식 그대로) ===

        /// <summary>
        /// 기본 경험치 (기본: 300)
        /// </summary>
        public static ConfigEntry<int> LevelExp;

        /// <summary>
        /// 레벨 배수 (기본: 1.05)
        /// </summary>
        public static ConfigEntry<float> MultiNextLevel;

        /// <summary>
        /// 경험치 배율 (기본: 1.0)
        /// </summary>
        public static ConfigEntry<float> RateExp;

        /// <summary>
        /// 누적 방식 (기본: true)
        /// true: current = current * multiply + levelExp
        /// false: current = current * multiply
        /// </summary>
        public static ConfigEntry<bool> LevelExpForEachLevel;

        #endregion

        #region === 레벨 범위 설정 ===

        /// <summary>
        /// 최대 레벨 차이 (기본: 10)
        /// 플레이어 레벨 + 이 값보다 높은 몬스터는 경험치 감소
        /// </summary>
        public static ConfigEntry<int> MaxLevelExp;

        /// <summary>
        /// 최소 레벨 차이 (기본: 10)
        /// 플레이어 레벨 - 이 값보다 낮은 몬스터는 경험치 감소
        /// </summary>
        public static ConfigEntry<int> MinLevelExp;

        /// <summary>
        /// 범위 밖 경험치 감소 (기본: false)
        /// true: 범위 밖 몬스터 경험치 점진적 감소
        /// </summary>
        public static ConfigEntry<bool> CurveExp;

        /// <summary>
        /// 범위 밖 경험치 0 (기본: false)
        /// true: 범위 밖 몬스터 경험치 0
        /// </summary>
        public static ConfigEntry<bool> NoExpPastLevel;

        #endregion

        #region === 몬스터 설정 ===

        /// <summary>
        /// 별 레벨 경험치 배수 (기본: 1.5)
        /// 몬스터 별 1개당 추가 경험치 배율
        /// </summary>
        public static ConfigEntry<float> ExpForLvlMonster;

        /// <summary>
        /// 별당 몬스터 레벨 증가 (기본: true)
        /// true: 1성 몬스터 = 기본 레벨 + 1
        /// </summary>
        public static ConfigEntry<bool> MobLevelPerStar;

        #endregion

        #region === UI 설정 ===

        /// <summary>
        /// 경험치 획득 팝업 표시
        /// </summary>
        public static ConfigEntry<bool> ShowExpPopup;

        /// <summary>
        /// 레벨업 이펙트 표시
        /// </summary>
        public static ConfigEntry<bool> ShowLevelUpEffect;

        /// <summary>
        /// 레벨/경험치 HUD 표시
        /// </summary>
        public static ConfigEntry<bool> ShowLevelHUD;

        // ── HUD 색상 / 스케일 ──

        /// <summary>HP 바 색상 (HTML 16진수, 기본: #E62626)</summary>
        public static ConfigEntry<string> HudHpColor;

        /// <summary>스태미나 바 색상 (기본: #F0B814)</summary>
        public static ConfigEntry<string> HudStaminaColor;

        /// <summary>에이트르 바 색상 (기본: #3880FF)</summary>
        public static ConfigEntry<string> HudEitrColor;

        /// <summary>경험치 바 색상 (기본: #47D1FF)</summary>
        public static ConfigEntry<string> HudExpColor;

        /// <summary>HUD 전체 스케일 (기본: 1.0)</summary>
        public static ConfigEntry<float> HudScale;

        #endregion

        #region === 스킬 포인트 설정 ===

        /// <summary>
        /// 레벨당 스킬 포인트 획득량 (기본: 2)
        /// </summary>
        public static ConfigEntry<int> SkillPointsPerLevel;

        #endregion

        #region === 스킬포인트 기반 레벨 설정 ===

        /// <summary>
        /// 스킬포인트 기반 레벨 계산 사용 여부
        /// EpicMMO 없을 때 사용한 스킬 포인트를 기준으로 레벨 표시
        /// </summary>
        public static ConfigEntry<bool> UseSkillPointBasedLevel;

        /// <summary>
        /// 레벨당 필요 스킬 포인트 (기본: 2)
        /// 예: 102 포인트 사용 / 2 = 51레벨
        /// </summary>
        public static ConfigEntry<int> PointsRequiredPerLevel;

        /// <summary>
        /// EpicMMO 설치 시 스킬포인트 기반 레벨을 EpicMMO에 자동 동기화
        /// </summary>
        public static ConfigEntry<bool> AutoSyncToEpicMMO;

        /// <summary>
        /// 탈출 버튼 표시 여부 (관리자 전용, 서버 싱크)
        /// </summary>
        public static ConfigEntry<bool> ExitButtonEnabled;

        #endregion

        #region === 레벨 차이 공격 데미지 감소 설정 (LV 차이 공격옵션) ===

        /// <summary>
        /// LV 차이 공격옵션 활성화 (관리자 전용, 서버 싱크)
        /// 몬스터 레벨이 플레이어보다 11 이상 높으면 플레이어가 가하는 피해가 감소합니다.
        /// </summary>
        public static ConfigEntry<bool> EnableLevelDiffDamageReduction;

        /// <summary>레벨 차이 11~15 구간 데미지 배율 % (기본: 40)</summary>
        public static ConfigEntry<float> LevelDiffTier1DamagePercent;

        /// <summary>레벨 차이 16~20 구간 데미지 배율 % (기본: 20)</summary>
        public static ConfigEntry<float> LevelDiffTier2DamagePercent;

        /// <summary>레벨 차이 21~30 구간 데미지 배율 % (기본: 10)</summary>
        public static ConfigEntry<float> LevelDiffTier3DamagePercent;

        /// <summary>레벨 차이 31 이상 구간 데미지 배율 % (기본: 0)</summary>
        public static ConfigEntry<float> LevelDiffTier4DamagePercent;

        #endregion

        #region === 레벨 차이 경험치 감소 설정 (구간별) ===

        /// <summary>레벨 차이(범위 초과분) 11~15 구간 경험치 배율 % (기본: 30)</summary>
        public static ConfigEntry<float> ExpDiffTier1Percent;

        /// <summary>레벨 차이(범위 초과분) 16~20 구간 경험치 배율 % (기본: 25)</summary>
        public static ConfigEntry<float> ExpDiffTier2Percent;

        /// <summary>레벨 차이(범위 초과분) 21~30 구간 경험치 배율 % (기본: 20)</summary>
        public static ConfigEntry<float> ExpDiffTier3Percent;

        /// <summary>레벨 차이(범위 초과분) 31 이상 구간 경험치 배율 % (기본: 10)</summary>
        public static ConfigEntry<float> ExpDiffTier4Percent;

        #endregion

        #region === 레벨 차이 아이템 드랍 억제 설정 ===

        /// <summary>
        /// 레벨 차이 아이템 드랍 억제 활성화 (관리자 전용, 서버 싱크)
        /// 몬스터 레벨이 플레이어보다 DropSuppressionLevelDiff 이상 높으면 아이템이 드랍되지 않습니다.
        /// </summary>
        public static ConfigEntry<bool> EnableLevelDiffDropSuppression;

        /// <summary>드랍 억제가 발동하는 레벨 차이 기준 (기본: 16)</summary>
        public static ConfigEntry<int> DropSuppressionLevelDiff;

        #endregion

        #region === JSON 설정 ===

        /// <summary>
        /// JSON 기본 파일 생성 여부
        /// </summary>
        public static ConfigEntry<bool> GenerateJsonFiles;

        #endregion

        #region === 관리자 설정 ===

        /// <summary>
        /// 관리자 모드 (스킬/직업 조건 무시)
        /// </summary>
        public static ConfigEntry<bool> AdminMode;

        #endregion

        #region === 서버싱크 Value 래퍼 ===

        public static int   MaxLevelValue             => (int)SkillTreeConfig.GetEffectiveValue("CaptainLevel_MaxLevel",             MaxLevel?.Value             ?? 100);
        public static int   LevelExpValue             => (int)SkillTreeConfig.GetEffectiveValue("CaptainLevel_LevelExp",             LevelExp?.Value             ?? 300);
        public static float MultiNextLevelValue       =>      SkillTreeConfig.GetEffectiveValue("CaptainLevel_MultiNextLevel",       MultiNextLevel?.Value       ?? 1.05f);
        public static float RateExpValue              =>      SkillTreeConfig.GetEffectiveValue("CaptainLevel_RateExp",              RateExp?.Value              ?? 1.0f);
        public static int   MaxLevelExpValue          => (int)SkillTreeConfig.GetEffectiveValue("CaptainLevel_MaxLevelExp",          MaxLevelExp?.Value          ?? 10);
        public static int   MinLevelExpValue          => (int)SkillTreeConfig.GetEffectiveValue("CaptainLevel_MinLevelExp",          MinLevelExp?.Value          ?? 10);
        public static float ExpForLvlMonsterValue     =>      SkillTreeConfig.GetEffectiveValue("CaptainLevel_ExpForLvlMonster",     ExpForLvlMonster?.Value     ?? 1.5f);
        public static int   SkillPointsPerLevelValue  => (int)SkillTreeConfig.GetEffectiveValue("CaptainLevel_SkillPointsPerLevel",  SkillPointsPerLevel?.Value  ?? 2);
        public static int   PointsRequiredPerLevelValue => (int)SkillTreeConfig.GetEffectiveValue("CaptainLevel_PointsRequired",    PointsRequiredPerLevel?.Value ?? 2);
        public static bool  ExitButtonEnabledValue     => SkillTreeConfig.GetEffectiveValue("exit_button_enabled", ExitButtonEnabled?.Value == true ? 1f : 0f) >= 0.5f;

        public static bool  EnableLevelDiffDamageReductionValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_LevelDiffEnabled", EnableLevelDiffDamageReduction?.Value == true ? 1f : 0f) >= 0.5f;
        public static float LevelDiffTier1DamagePercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_LevelDiffTier1Percent", LevelDiffTier1DamagePercent?.Value ?? 40f);
        public static float LevelDiffTier2DamagePercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_LevelDiffTier2Percent", LevelDiffTier2DamagePercent?.Value ?? 20f);
        public static float LevelDiffTier3DamagePercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_LevelDiffTier3Percent", LevelDiffTier3DamagePercent?.Value ?? 10f);
        public static float LevelDiffTier4DamagePercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_LevelDiffTier4Percent", LevelDiffTier4DamagePercent?.Value ?? 0f);

        public static float ExpDiffTier1PercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_ExpDiffTier1Percent", ExpDiffTier1Percent?.Value ?? 30f);
        public static float ExpDiffTier2PercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_ExpDiffTier2Percent", ExpDiffTier2Percent?.Value ?? 25f);
        public static float ExpDiffTier3PercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_ExpDiffTier3Percent", ExpDiffTier3Percent?.Value ?? 20f);
        public static float ExpDiffTier4PercentValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_ExpDiffTier4Percent", ExpDiffTier4Percent?.Value ?? 10f);

        public static bool EnableLevelDiffDropSuppressionValue =>
            SkillTreeConfig.GetEffectiveValue("CaptainLevel_DropSuppressionEnabled", EnableLevelDiffDropSuppression?.Value == true ? 1f : 0f) >= 0.5f;
        public static int DropSuppressionLevelDiffValue =>
            (int)SkillTreeConfig.GetEffectiveValue("CaptainLevel_DropSuppressionLevelDiff", DropSuppressionLevelDiff?.Value ?? 16);

        #endregion

        /// <summary>
        /// Config 바인딩 (Plugin.cs에서 최상단에 호출)
        /// BepInEx 기본 Config 파일 사용: BepInEx/config/CaptainSkillTree.SkillTreeMod.cfg
        /// </summary>
        public static void Bind(ConfigFile config)
        {
            // 기존 CaptainSkillTree 폴더 내 중복 config 파일 삭제
            CleanupOldConfigFolder();

            // === 기본 설정 ===
            EnableCaptainLevel = config.Bind(
                SECTION,
                "Enable Captain Level System",
                true,
                SkillTreeConfig.GetConfigDescription("Enable Captain Level System"));

            MaxLevel = config.Bind(
                SECTION,
                "Max Level",
                100,
                SkillTreeConfig.GetConfigDescription("Max Level"));

            // === 경험치 설정 ===
            LevelExp = config.Bind(
                SECTION,
                "Base Level Exp",
                300,
                SkillTreeConfig.GetConfigDescription("Base Level Exp"));

            MultiNextLevel = config.Bind(
                SECTION,
                "Multi Next Level",
                1.05f,
                SkillTreeConfig.GetConfigDescription("Multi Next Level"));

            RateExp = config.Bind(
                SECTION,
                "Exp Rate",
                1.0f,
                SkillTreeConfig.GetConfigDescription("Exp Rate"));

            LevelExpForEachLevel = config.Bind(
                SECTION,
                "Cumulative Exp Mode",
                true,
                SkillTreeConfig.GetConfigDescription("Cumulative Exp Mode"));

            // === 레벨 범위 설정 ===
            MaxLevelExp = config.Bind(
                SECTION,
                "Max Level Exp Range",
                10,
                SkillTreeConfig.GetConfigDescription("Max Level Exp Range"));

            MinLevelExp = config.Bind(
                SECTION,
                "Min Level Exp Range",
                10,
                SkillTreeConfig.GetConfigDescription("Min Level Exp Range"));

            CurveExp = config.Bind(
                SECTION,
                "Curve Exp Outside Range",
                false,
                SkillTreeConfig.GetConfigDescription("Curve Exp Outside Range"));

            NoExpPastLevel = config.Bind(
                SECTION,
                "No Exp Past Level Range",
                false,
                SkillTreeConfig.GetConfigDescription("No Exp Past Level Range"));

            // === 몬스터 설정 ===
            ExpForLvlMonster = config.Bind(
                SECTION,
                "Star Level Exp Multiplier",
                1.5f,
                SkillTreeConfig.GetConfigDescription("Star Level Exp Multiplier"));

            MobLevelPerStar = config.Bind(
                SECTION,
                "Mob Level Per Star",
                true,
                SkillTreeConfig.GetConfigDescription("Mob Level Per Star"));

            // === UI 설정 ===
            ShowExpPopup = config.Bind(
                SECTION,
                "Show Exp Popup",
                true,
                SkillTreeConfig.GetConfigDescription("Show Exp Popup"));

            ShowLevelUpEffect = config.Bind(
                SECTION,
                "Show Level Up Effect",
                true,
                SkillTreeConfig.GetConfigDescription("Show Level Up Effect"));

            ShowLevelHUD = config.Bind(
                SECTION,
                "Show Level HUD",
                true,
                SkillTreeConfig.GetConfigDescription("Show Level HUD"));

            // === HUD 색상/스케일 설정 ===
            HudHpColor = config.Bind(
                SECTION,
                "HUD HP Color",
                "#870000",
                SkillTreeConfig.GetConfigDescription("HUD HP Color"));

            HudStaminaColor = config.Bind(
                SECTION,
                "HUD Stamina Color",
                "#986100",
                SkillTreeConfig.GetConfigDescription("HUD Stamina Color"));

            HudEitrColor = config.Bind(
                SECTION,
                "HUD Eitr Color",
                "#84257C",
                SkillTreeConfig.GetConfigDescription("HUD Eitr Color"));

            HudExpColor = config.Bind(
                SECTION,
                "HUD Exp Color",
                "#C87820",
                SkillTreeConfig.GetConfigDescription("HUD Exp Color"));

            HudScale = config.Bind(
                SECTION,
                "HUD Scale",
                1.0f,
                SkillTreeConfig.GetConfigDescription("HUD Scale"));

            // === 스킬 포인트 설정 ===
            SkillPointsPerLevel = config.Bind(
                SECTION,
                "Skill Points Per Level",
                2,
                SkillTreeConfig.GetConfigDescription("Skill Points Per Level"));

            // === 스킬포인트 기반 레벨 설정 ===
            UseSkillPointBasedLevel = config.Bind(
                SECTION,
                "Use Skill Point Based Level",
                true,
                SkillTreeConfig.GetConfigDescription("Use Skill Point Based Level"));

            PointsRequiredPerLevel = config.Bind(
                SECTION,
                "Points Required Per Level",
                2,
                SkillTreeConfig.GetConfigDescription("Points Required Per Level"));

            AutoSyncToEpicMMO = config.Bind(
                SECTION,
                "Auto Sync To EpicMMO",
                true,
                SkillTreeConfig.GetConfigDescription("Auto Sync To EpicMMO"));

            ExitButtonEnabled = SkillTreeConfig.BindServerSync(
                config, SECTION, "Exit Button", false,
                SkillTreeConfig.GetConfigDescription("Exit Button"));

            // === 레벨 차이 공격 데미지 감소 설정 (LV 차이 공격옵션) ===
            EnableLevelDiffDamageReduction = SkillTreeConfig.BindServerSync(
                config, SECTION, "Enable Level Diff Damage Reduction", true,
                SkillTreeConfig.GetConfigDescription("Enable Level Diff Damage Reduction"));

            LevelDiffTier1DamagePercent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Level Diff Tier1 Damage Percent", 40f,
                SkillTreeConfig.GetConfigDescription("Level Diff Tier1 Damage Percent"));

            LevelDiffTier2DamagePercent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Level Diff Tier2 Damage Percent", 20f,
                SkillTreeConfig.GetConfigDescription("Level Diff Tier2 Damage Percent"));

            LevelDiffTier3DamagePercent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Level Diff Tier3 Damage Percent", 10f,
                SkillTreeConfig.GetConfigDescription("Level Diff Tier3 Damage Percent"));

            LevelDiffTier4DamagePercent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Level Diff Tier4 Damage Percent", 0f,
                SkillTreeConfig.GetConfigDescription("Level Diff Tier4 Damage Percent"));

            // === 레벨 차이 경험치 감소 설정 (구간별) ===
            ExpDiffTier1Percent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Exp Diff Tier1 Percent", 30f,
                SkillTreeConfig.GetConfigDescription("Exp Diff Tier1 Percent"));

            ExpDiffTier2Percent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Exp Diff Tier2 Percent", 25f,
                SkillTreeConfig.GetConfigDescription("Exp Diff Tier2 Percent"));

            ExpDiffTier3Percent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Exp Diff Tier3 Percent", 20f,
                SkillTreeConfig.GetConfigDescription("Exp Diff Tier3 Percent"));

            ExpDiffTier4Percent = SkillTreeConfig.BindServerSync(
                config, SECTION, "Exp Diff Tier4 Percent", 10f,
                SkillTreeConfig.GetConfigDescription("Exp Diff Tier4 Percent"));

            // === 레벨 차이 아이템 드랍 억제 설정 ===
            EnableLevelDiffDropSuppression = SkillTreeConfig.BindServerSync(
                config, SECTION, "Enable Level Diff Drop Suppression", true,
                SkillTreeConfig.GetConfigDescription("Enable Level Diff Drop Suppression"));

            DropSuppressionLevelDiff = SkillTreeConfig.BindServerSync(
                config, SECTION, "Drop Suppression Level Diff", 16,
                SkillTreeConfig.GetConfigDescription("Drop Suppression Level Diff"));

            // === JSON 설정 ===
            GenerateJsonFiles = config.Bind(
                SECTION,
                "Generate JSON Files",
                true,
                SkillTreeConfig.GetConfigDescription("Generate JSON Files"));

            // === 관리자 설정 ===
            AdminMode = config.Bind(
                SECTION,
                "Admin Mode",
                false,
                SkillTreeConfig.GetConfigDescription("Admin Mode"));

            Plugin.Log.LogDebug("[CaptainLevelConfig] Config 바인딩 완료 (기본 Config 사용)");
        }

        /// <summary>
        /// 기존 CaptainSkillTree 폴더 내 config 파일 정리
        /// </summary>
        private static void CleanupOldConfigFolder()
        {
            try
            {
                string oldConfigFolder = Path.Combine(Paths.ConfigPath, "CaptainSkillTree");
                string oldConfigFile = Path.Combine(oldConfigFolder, "CaptainSkillTree.SkillTreeMod.cfg");

                // 기존 config 파일 삭제
                if (File.Exists(oldConfigFile))
                {
                    File.Delete(oldConfigFile);
                    Plugin.Log.LogDebug($"[CaptainLevelConfig] 기존 config 파일 삭제: {oldConfigFile}");
                }

                // 폴더가 비어있으면 폴더도 삭제 (JSON 파일은 유지)
                if (Directory.Exists(oldConfigFolder))
                {
                    var files = Directory.GetFiles(oldConfigFolder, "*.cfg");
                    if (files.Length == 0)
                    {
                        // cfg 파일이 없으면 폴더 유지 (JSON 파일용)
                        Plugin.Log.LogDebug($"[CaptainLevelConfig] CaptainSkillTree 폴더 유지 (JSON 파일용)");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[CaptainLevelConfig] 기존 config 정리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 설정값 로그 출력 (디버깅용)
        /// </summary>
        public static void LogCurrentSettings()
        {
            Plugin.Log.LogDebug("=== Captain Level System Settings ===");
            Plugin.Log.LogDebug($"EnableCaptainLevel: {EnableCaptainLevel.Value}");
            Plugin.Log.LogDebug($"MaxLevel: {MaxLevel.Value}");
            Plugin.Log.LogDebug($"LevelExp: {LevelExp.Value}");
            Plugin.Log.LogDebug($"MultiNextLevel: {MultiNextLevel.Value}");
            Plugin.Log.LogDebug($"RateExp: {RateExp.Value}");
            Plugin.Log.LogDebug($"LevelExpForEachLevel: {LevelExpForEachLevel.Value}");
            Plugin.Log.LogDebug($"ExpForLvlMonster: {ExpForLvlMonster.Value}");
            Plugin.Log.LogDebug($"SkillPointsPerLevel: {SkillPointsPerLevel.Value}");
            Plugin.Log.LogDebug($"UseSkillPointBasedLevel: {UseSkillPointBasedLevel.Value}");
            Plugin.Log.LogDebug($"PointsRequiredPerLevel: {PointsRequiredPerLevel.Value}");
            Plugin.Log.LogDebug($"AutoSyncToEpicMMO: {AutoSyncToEpicMMO.Value}");
            Plugin.Log.LogDebug("=====================================");
        }

        /// <summary>
        /// 관리자 모드 활성화 여부 확인
        /// Config가 On이고, 현재 플레이어가 관리자 권한을 보유한 경우에만 true
        /// </summary>
        public static bool IsAdminModeActive()
        {
            if (!(AdminMode?.Value ?? false)) return false;

            var player = Player.m_localPlayer;
            if (player == null) return true;            // 콘솔 환경
            if (ZNet.instance == null) return true;     // 싱글플레이어

            if (ZNet.instance.IsServer()) return true;  // 서버 호스트

            var adminList = ZNet.instance.GetAdminList();
            if (adminList != null)
            {
                string playerId = player.GetPlayerID().ToString();
                string playerName = player.GetPlayerName();
                if (adminList.Contains(playerId) || adminList.Contains(playerName))
                    return true;
            }

            try
            {
                if (Jotunn.Managers.SynchronizationManager.Instance?.PlayerIsAdmin == true)
                    return true;
            }
            catch { }

            return false;
        }
    }
}

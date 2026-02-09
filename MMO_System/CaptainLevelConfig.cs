using System.IO;
using BepInEx;
using BepInEx.Configuration;

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

        #endregion

        #region === JSON 설정 ===

        /// <summary>
        /// JSON 기본 파일 생성 여부
        /// </summary>
        public static ConfigEntry<bool> GenerateJsonFiles;

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
                "자체 레벨 시스템 활성화 여부\n" +
                "EpicMMOSystem이 감지되면 자동으로 비활성화됩니다.\n" +
                "[기본: true]");

            MaxLevel = config.Bind(
                SECTION,
                "Max Level",
                100,
                "최대 레벨\n" +
                "[기본: 100, 범위: 1-999]");

            // === 경험치 설정 ===
            LevelExp = config.Bind(
                SECTION,
                "Base Level Exp",
                300,
                "기본 경험치 (레벨 1에서 2로 올리는데 필요한 경험치)\n" +
                "[기본: 300]");

            MultiNextLevel = config.Bind(
                SECTION,
                "Multi Next Level",
                1.05f,
                "다음 레벨 경험치 배수\n" +
                "레벨이 오를수록 필요 경험치가 이 배수만큼 증가합니다.\n" +
                "[기본: 1.05]");

            RateExp = config.Bind(
                SECTION,
                "Exp Rate",
                1.0f,
                "경험치 배율\n" +
                "획득하는 모든 경험치에 적용됩니다.\n" +
                "[기본: 1.0, 범위: 0.1-10.0]");

            LevelExpForEachLevel = config.Bind(
                SECTION,
                "Cumulative Exp Mode",
                true,
                "누적 경험치 모드\n" +
                "true: 필요 경험치 = (이전 필요 경험치 * 배수) + 기본 경험치\n" +
                "false: 필요 경험치 = 이전 필요 경험치 * 배수\n" +
                "[기본: true]");

            // === 레벨 범위 설정 ===
            MaxLevelExp = config.Bind(
                SECTION,
                "Max Level Exp Range",
                10,
                "최대 레벨 차이\n" +
                "플레이어 레벨 + 이 값보다 높은 몬스터는 경험치가 감소합니다.\n" +
                "[기본: 10]");

            MinLevelExp = config.Bind(
                SECTION,
                "Min Level Exp Range",
                10,
                "최소 레벨 차이\n" +
                "플레이어 레벨 - 이 값보다 낮은 몬스터는 경험치가 감소합니다.\n" +
                "[기본: 10]");

            CurveExp = config.Bind(
                SECTION,
                "Curve Exp Outside Range",
                false,
                "범위 밖 경험치 점진적 감소\n" +
                "범위 밖 몬스터의 경험치가 거리에 따라 점진적으로 감소합니다.\n" +
                "[기본: false]");

            NoExpPastLevel = config.Bind(
                SECTION,
                "No Exp Past Level Range",
                false,
                "범위 밖 경험치 0\n" +
                "범위 밖 몬스터의 경험치가 0이 됩니다.\n" +
                "[기본: false]");

            // === 몬스터 설정 ===
            ExpForLvlMonster = config.Bind(
                SECTION,
                "Star Level Exp Multiplier",
                1.5f,
                "별 레벨 경험치 배수\n" +
                "몬스터 별 1개당 추가 경험치 배율입니다.\n" +
                "예: 1성 몬스터 = 기본 경험치 + (최대 경험치 * 이 값 * 1)\n" +
                "[기본: 1.5]");

            MobLevelPerStar = config.Bind(
                SECTION,
                "Mob Level Per Star",
                true,
                "별당 몬스터 레벨 증가\n" +
                "true: 1성 몬스터는 기본 레벨 + 1로 표시됩니다.\n" +
                "예: Lv.10 몬스터가 1성이면 Lv.11로 표시\n" +
                "[기본: true]");

            // === UI 설정 ===
            ShowExpPopup = config.Bind(
                SECTION,
                "Show Exp Popup",
                true,
                "경험치 획득 시 팝업 표시\n" +
                "[기본: true]");

            ShowLevelUpEffect = config.Bind(
                SECTION,
                "Show Level Up Effect",
                true,
                "레벨업 시 이펙트 표시\n" +
                "[기본: true]");

            ShowLevelHUD = config.Bind(
                SECTION,
                "Show Level HUD",
                true,
                "레벨/경험치 바 HUD 표시\n" +
                "[기본: true]");

            // === 스킬 포인트 설정 ===
            SkillPointsPerLevel = config.Bind(
                SECTION,
                "Skill Points Per Level",
                2,
                "레벨당 스킬 포인트 획득량\n" +
                "[기본: 2, 범위: 1-10]");

            // === 스킬포인트 기반 레벨 설정 ===
            UseSkillPointBasedLevel = config.Bind(
                SECTION,
                "Use Skill Point Based Level",
                true,
                "스킬포인트 기반 레벨 계산 사용\n" +
                "EpicMMO가 없을 때 사용한 스킬 포인트를 기준으로 레벨을 계산합니다.\n" +
                "예: 102 포인트 사용 / 2 = 51레벨\n" +
                "[기본: true]");

            PointsRequiredPerLevel = config.Bind(
                SECTION,
                "Points Required Per Level",
                2,
                "레벨당 필요 스킬 포인트\n" +
                "스킬포인트 기반 레벨 계산 시 사용됩니다.\n" +
                "예: 102 포인트 / 2 = 51레벨\n" +
                "[기본: 2, 범위: 1-10]");

            AutoSyncToEpicMMO = config.Bind(
                SECTION,
                "Auto Sync To EpicMMO",
                true,
                "EpicMMO 자동 동기화\n" +
                "EpicMMO 설치 시 스킬포인트 기반 레벨을 EpicMMO에 동기화합니다.\n" +
                "[기본: true]");

            // === JSON 설정 ===
            GenerateJsonFiles = config.Bind(
                SECTION,
                "Generate JSON Files",
                true,
                "JSON 기본 파일 생성 여부\n" +
                "true로 설정 시 BepInEx/config/CaptainSkillTree/ 폴더에\n" +
                "MonsterExp.json, LevelExp.json 기본 파일이 생성됩니다.\n" +
                "(이미 파일이 있으면 덮어쓰지 않음)\n" +
                "[기본: true]");

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
    }
}

using BepInEx.Configuration;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// MMO 몬스터 난이도 시스템 Config
    /// CaptainLevelConfig와 동일 섹션 공유 → F1 Config Manager 최상단에 표시
    /// CaptainLevelConfig.Bind()에서 호출됨 (Plugin.cs 수정 없이 초기화)
    /// </summary>
    public static class MMODifficultyConfig
    {
        // CaptainLevelConfig와 동일 섹션 → 최상단 MMO 항목 안에 배치
        private const string SECTION = "- Captain Level System -";

        #region === 전체 ON/OFF ===

        /// <summary>
        /// MMO 몬스터 난이도 시스템 활성화 (기본: true)
        /// 스킬포인트(SP)에 비례해 몬스터 m_level이 자동으로 올라감
        /// </summary>
        public static ConfigEntry<bool> EnableMMODifficulty;

        #endregion

        #region === SP 구간별 m_level 보너스 ===

        /// <summary>SP 0~10 구간 몬스터 m_level 보너스 (기본: 2)</summary>
        public static ConfigEntry<int> SpBonus_0_10;

        /// <summary>SP 11~20 구간 몬스터 m_level 보너스 (기본: 3)</summary>
        public static ConfigEntry<int> SpBonus_11_20;

        /// <summary>SP 21~30 구간 몬스터 m_level 보너스 (기본: 4)</summary>
        public static ConfigEntry<int> SpBonus_21_30;

        /// <summary>SP 31~40 구간 몬스터 m_level 보너스 (기본: 5)</summary>
        public static ConfigEntry<int> SpBonus_31_40;

        /// <summary>SP 41~70 구간 몬스터 m_level 보너스 (기본: 5)</summary>
        public static ConfigEntry<int> SpBonus_41_70;

        /// <summary>SP 71~80 구간 몬스터 m_level 보너스 (기본: 6)</summary>
        public static ConfigEntry<int> SpBonus_71_80;

        /// <summary>SP 81~100 구간 몬스터 m_level 보너스 (기본: 7)</summary>
        public static ConfigEntry<int> SpBonus_81_100;

        /// <summary>SP 101+ 구간 몬스터 m_level 보너스 (기본: 8)</summary>
        public static ConfigEntry<int> SpBonus_101Plus;

        /// <summary>m_level 보너스 최대 상한 (기본: 10)</summary>
        public static ConfigEntry<int> MaxLevelBonus;

        #endregion

        #region === 별 확률 ===

        /// <summary>기본 별 추가 확률 % (기본: 25.0)</summary>
        public static ConfigEntry<float> BaseStarChance;

        /// <summary>별 확률 SP 나누기 값 (기본: 3.0) → 확률 = BaseStarChance + SP / StarChanceDivider</summary>
        public static ConfigEntry<float> StarChanceDivider;

        /// <summary>최대 별 추가 확률 % (기본: 100.0)</summary>
        public static ConfigEntry<float> MaxStarChance;

        #endregion

        #region === 액티브 스킬 보너스 ===

        /// <summary>액티브·직업 스킬 보유 시 +1 보너스 활성화 (기본: true)</summary>
        public static ConfigEntry<bool> EnableActiveSkillBonus;

        #endregion

        /// <summary>
        /// Config 바인딩
        /// CaptainLevelConfig.Bind() 마지막에 호출됨
        /// </summary>
        public static void Bind(ConfigFile config)
        {
            // 섹션 맨 위: 전체 ON/OFF
            EnableMMODifficulty = config.Bind(SECTION, "MMODiff_Enable", true,
                SkillTreeConfig.GetConfigDescription("MMODiff_Enable"));

            // SP 구간별 m_level 보너스
            SpBonus_0_10 = config.Bind(SECTION, "MMODiff_SpBonus_0_10", 2,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_0_10"));

            SpBonus_11_20 = config.Bind(SECTION, "MMODiff_SpBonus_11_20", 3,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_11_20"));

            SpBonus_21_30 = config.Bind(SECTION, "MMODiff_SpBonus_21_30", 4,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_21_30"));

            SpBonus_31_40 = config.Bind(SECTION, "MMODiff_SpBonus_31_40", 5,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_31_40"));

            SpBonus_41_70 = config.Bind(SECTION, "MMODiff_SpBonus_41_70", 5,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_41_70"));

            SpBonus_71_80 = config.Bind(SECTION, "MMODiff_SpBonus_71_80", 6,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_71_80"));

            SpBonus_81_100 = config.Bind(SECTION, "MMODiff_SpBonus_81_100", 7,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_81_100"));

            SpBonus_101Plus = config.Bind(SECTION, "MMODiff_SpBonus_101Plus", 8,
                SkillTreeConfig.GetConfigDescription("MMODiff_SpBonus_101Plus"));

            MaxLevelBonus = config.Bind(SECTION, "MMODiff_MaxLevelBonus", 10,
                SkillTreeConfig.GetConfigDescription("MMODiff_MaxLevelBonus"));

            // 별 확률
            BaseStarChance = config.Bind(SECTION, "MMODiff_BaseStarChance", 25.0f,
                SkillTreeConfig.GetConfigDescription("MMODiff_BaseStarChance"));

            StarChanceDivider = config.Bind(SECTION, "MMODiff_StarChanceDivider", 3.0f,
                SkillTreeConfig.GetConfigDescription("MMODiff_StarChanceDivider"));

            MaxStarChance = config.Bind(SECTION, "MMODiff_MaxStarChance", 100.0f,
                SkillTreeConfig.GetConfigDescription("MMODiff_MaxStarChance"));

            // 액티브 스킬 보너스
            EnableActiveSkillBonus = config.Bind(SECTION, "MMODiff_ActiveSkillBonus", true,
                SkillTreeConfig.GetConfigDescription("MMODiff_ActiveSkillBonus"));

            Plugin.Log.LogDebug("[MMODifficultyConfig] Config 바인딩 완료");
        }
    }
}

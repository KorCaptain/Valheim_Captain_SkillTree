using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// EpicMMOSystem Special/Strength 스탯 흡수 치명타 Config
    /// EpicMMOSystem 자체 치명타 시스템(Unpatch로 제거됨)을 대체하기 위해
    /// 동일한 기본값으로 복제한 독립 Config. 상세: md/CRITICAL_SYSTEM_RULES.md
    /// </summary>
    public static class MMO_CritIntegration_Config
    {
        public static ConfigEntry<float> MmoSpecialCritChancePerPoint;
        public static ConfigEntry<float> MmoSpecialCritChanceBase;
        public static ConfigEntry<float> MmoStrengthCritDamagePerPoint;
        public static ConfigEntry<float> MmoStrengthCritDamageBase;

        public static float MmoSpecialCritChancePerPointValue =>
            SkillTreeConfig.GetEffectiveValue("Mmo_Special_CritChancePerPoint", MmoSpecialCritChancePerPoint?.Value ?? 0.2f);
        public static float MmoSpecialCritChanceBaseValue =>
            SkillTreeConfig.GetEffectiveValue("Mmo_Special_CritChanceBase", MmoSpecialCritChanceBase?.Value ?? 1.0f);
        public static float MmoStrengthCritDamagePerPointValue =>
            SkillTreeConfig.GetEffectiveValue("Mmo_Strength_CritDamagePerPoint", MmoStrengthCritDamagePerPoint?.Value ?? 1.0f);
        public static float MmoStrengthCritDamageBaseValue =>
            SkillTreeConfig.GetEffectiveValue("Mmo_Strength_CritDamageBase", MmoStrengthCritDamageBase?.Value ?? 50.0f);

        public static void Initialize(ConfigFile config)
        {
            MmoSpecialCritChancePerPoint = SkillTreeConfig.BindServerSync(config,
                "MMO Integration", "Mmo_Special_CritChancePerPoint", 0.2f,
                SkillTreeConfig.GetConfigDescription("Mmo_Special_CritChancePerPoint"), order: 10);

            MmoSpecialCritChanceBase = SkillTreeConfig.BindServerSync(config,
                "MMO Integration", "Mmo_Special_CritChanceBase", 1.0f,
                SkillTreeConfig.GetConfigDescription("Mmo_Special_CritChanceBase"), order: 9);

            MmoStrengthCritDamagePerPoint = SkillTreeConfig.BindServerSync(config,
                "MMO Integration", "Mmo_Strength_CritDamagePerPoint", 1.0f,
                SkillTreeConfig.GetConfigDescription("Mmo_Strength_CritDamagePerPoint"), order: 8);

            MmoStrengthCritDamageBase = SkillTreeConfig.BindServerSync(config,
                "MMO Integration", "Mmo_Strength_CritDamageBase", 50.0f,
                SkillTreeConfig.GetConfigDescription("Mmo_Strength_CritDamageBase"), order: 7);
        }
    }
}

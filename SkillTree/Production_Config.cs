using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 생산 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Production_Config
    {
        // === 효과 설정 ===
        // Tier 0: 생산 전문가
        public static ConfigEntry<int> ProductionRootWoodBonusChance;
        // Tier 1: 초보 일꾼
        public static ConfigEntry<int> NoviceWorkerWoodBonusChance;
        // Tier 2: 전문 분야
        public static ConfigEntry<int> WoodcuttingLv2BonusChance;
        public static ConfigEntry<int> GatheringLv2BonusChance;
        public static ConfigEntry<int> MiningLv2BonusChance;
        public static ConfigEntry<int> CraftingLv2UpgradeChance;
        public static ConfigEntry<int> CraftingLv2DurabilityBonus;
        // Tier 3: 중급 스킬
        public static ConfigEntry<int> WoodcuttingLv3BonusChance;
        public static ConfigEntry<int> GatheringLv3BonusChance;
        public static ConfigEntry<int> MiningLv3BonusChance;
        public static ConfigEntry<int> CraftingLv3UpgradeChance;
        public static ConfigEntry<int> CraftingLv3DurabilityBonus;
        // Tier 4: 고급 스킬
        public static ConfigEntry<int> WoodcuttingLv4BonusChance;
        public static ConfigEntry<int> GatheringLv4BonusChance;
        public static ConfigEntry<int> MiningLv4BonusChance;
        public static ConfigEntry<int> CraftingLv4UpgradeChance;
        public static ConfigEntry<int> CraftingLv4DurabilityBonus;

        // === 효과 접근 프로퍼티 ===
        // Tier 0
        public static int ProductionRootWoodBonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("production_root_wood_chance", ProductionRootWoodBonusChance?.Value ?? 50);
        // Tier 1
        public static int NoviceWorkerWoodBonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("novice_worker_wood_chance", NoviceWorkerWoodBonusChance?.Value ?? 25);
        // Tier 2
        public static int WoodcuttingLv2BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("woodcutting_lv2_chance", WoodcuttingLv2BonusChance?.Value ?? 25);
        public static int GatheringLv2BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("gathering_lv2_chance", GatheringLv2BonusChance?.Value ?? 25);
        public static int MiningLv2BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("mining_lv2_chance", MiningLv2BonusChance?.Value ?? 25);
        public static int CraftingLv2UpgradeChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv2_upgrade_chance", CraftingLv2UpgradeChance?.Value ?? 25);
        public static int CraftingLv2DurabilityBonusValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv2_durability", CraftingLv2DurabilityBonus?.Value ?? 25);
        // Tier 3
        public static int WoodcuttingLv3BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("woodcutting_lv3_chance", WoodcuttingLv3BonusChance?.Value ?? 35);
        public static int GatheringLv3BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("gathering_lv3_chance", GatheringLv3BonusChance?.Value ?? 35);
        public static int MiningLv3BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("mining_lv3_chance", MiningLv3BonusChance?.Value ?? 25);
        public static int CraftingLv3UpgradeChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv3_upgrade_chance", CraftingLv3UpgradeChance?.Value ?? 25);
        public static int CraftingLv3DurabilityBonusValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv3_durability", CraftingLv3DurabilityBonus?.Value ?? 25);
        // Tier 4
        public static int WoodcuttingLv4BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("woodcutting_lv4_chance", WoodcuttingLv4BonusChance?.Value ?? 45);
        public static int GatheringLv4BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("gathering_lv4_chance", GatheringLv4BonusChance?.Value ?? 40);
        public static int MiningLv4BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("mining_lv4_chance", MiningLv4BonusChance?.Value ?? 25);
        public static int CraftingLv4UpgradeChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv4_upgrade_chance", CraftingLv4UpgradeChance?.Value ?? 25);
        public static int CraftingLv4DurabilityBonusValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv4_durability", CraftingLv4DurabilityBonus?.Value ?? 25);

        public static void Initialize(ConfigFile config)
        {
            // === Tier 0: 생산 전문가 ===
            ProductionRootWoodBonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier0_ProductionExpert_WoodBonusChance", 50,
                SkillTreeConfig.GetConfigDescription("Tier0_ProductionExpert_WoodBonusChance"), order: 60);

            // === Tier 1: 초보 일꾼 ===
            NoviceWorkerWoodBonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier1_NoviceWorker_WoodBonusChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier1_NoviceWorker_WoodBonusChance"), order: 50);

            // === Tier 2-1: 벌목 Lv2 ===
            WoodcuttingLv2BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_WoodcuttingLv2_BonusChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier2_WoodcuttingLv2_BonusChance"), order: 48);

            // === Tier 2-2: 채집 Lv2 ===
            GatheringLv2BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_GatheringLv2_BonusChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier2_GatheringLv2_BonusChance"), order: 46);

            // === Tier 2-3: 채광 Lv2 ===
            MiningLv2BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_MiningLv2_BonusChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier2_MiningLv2_BonusChance"), order: 44);

            // === Tier 2-4: 제작 Lv2 ===
            CraftingLv2UpgradeChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_CraftingLv2_UpgradeChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier2_CraftingLv2_UpgradeChance"), order: 42);

            CraftingLv2DurabilityBonus = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_CraftingLv2_DurabilityBonus", 25,
                SkillTreeConfig.GetConfigDescription("Tier2_CraftingLv2_DurabilityBonus"), order: 41);

            // === Tier 3-1: 벌목 Lv3 ===
            WoodcuttingLv3BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_WoodcuttingLv3_BonusChance", 35,
                SkillTreeConfig.GetConfigDescription("Tier3_WoodcuttingLv3_BonusChance"), order: 32);

            // === Tier 3-2: 채집 Lv3 ===
            GatheringLv3BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_GatheringLv3_BonusChance", 35,
                SkillTreeConfig.GetConfigDescription("Tier3_GatheringLv3_BonusChance"), order: 30);

            // === Tier 3-3: 채광 Lv3 ===
            MiningLv3BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_MiningLv3_BonusChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier3_MiningLv3_BonusChance"), order: 28);

            // === Tier 3-4: 제작 Lv3 ===
            CraftingLv3UpgradeChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_CraftingLv3_UpgradeChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier3_CraftingLv3_UpgradeChance"), order: 26);

            CraftingLv3DurabilityBonus = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_CraftingLv3_DurabilityBonus", 25,
                SkillTreeConfig.GetConfigDescription("Tier3_CraftingLv3_DurabilityBonus"), order: 25);

            // === Tier 4-1: 벌목 Lv4 ===
            WoodcuttingLv4BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_WoodcuttingLv4_BonusChance", 45,
                SkillTreeConfig.GetConfigDescription("Tier4_WoodcuttingLv4_BonusChance"), order: 18);

            // === Tier 4-2: 채집 Lv4 ===
            GatheringLv4BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_GatheringLv4_BonusChance", 40,
                SkillTreeConfig.GetConfigDescription("Tier4_GatheringLv4_BonusChance"), order: 16);

            // === Tier 4-3: 채광 Lv4 ===
            MiningLv4BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_MiningLv4_BonusChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier4_MiningLv4_BonusChance"), order: 14);

            // === Tier 4-4: 제작 Lv4 ===
            CraftingLv4UpgradeChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_CraftingLv4_UpgradeChance", 25,
                SkillTreeConfig.GetConfigDescription("Tier4_CraftingLv4_UpgradeChance"), order: 12);

            CraftingLv4DurabilityBonus = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_CraftingLv4_DurabilityBonus", 25,
                SkillTreeConfig.GetConfigDescription("Tier4_CraftingLv4_DurabilityBonus"), order: 11);

            Plugin.Log.LogDebug("[Production_Config] Production Expert tree config initialized");
        }
    }
}

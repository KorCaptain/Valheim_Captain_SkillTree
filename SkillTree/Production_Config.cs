using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 생산 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Production_Config
    {
        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> ProductionRootRequiredPoints;
        public static ConfigEntry<int> ProductionStep1RequiredPoints;
        public static ConfigEntry<int> ProductionStep2RequiredPoints;
        public static ConfigEntry<int> ProductionStep3RequiredPoints;
        public static ConfigEntry<int> ProductionStep4RequiredPoints;

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

        // === 필요 포인트 접근 프로퍼티 ===
        public static int ProductionRootRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("production_root_required_points", ProductionRootRequiredPoints?.Value ?? 2);
        public static int ProductionStep1RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("production_step1_required_points", ProductionStep1RequiredPoints?.Value ?? 2);
        public static int ProductionStep2RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("production_step2_required_points", ProductionStep2RequiredPoints?.Value ?? 2);
        public static int ProductionStep3RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("production_step3_required_points", ProductionStep3RequiredPoints?.Value ?? 2);
        public static int ProductionStep4RequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("production_step4_required_points", ProductionStep4RequiredPoints?.Value ?? 3);

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
            (int)SkillTreeConfig.GetEffectiveValue("woodcutting_lv3_chance", WoodcuttingLv3BonusChance?.Value ?? 25);
        public static int GatheringLv3BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("gathering_lv3_chance", GatheringLv3BonusChance?.Value ?? 25);
        public static int MiningLv3BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("mining_lv3_chance", MiningLv3BonusChance?.Value ?? 25);
        public static int CraftingLv3UpgradeChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv3_upgrade_chance", CraftingLv3UpgradeChance?.Value ?? 25);
        public static int CraftingLv3DurabilityBonusValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv3_durability", CraftingLv3DurabilityBonus?.Value ?? 25);
        // Tier 4
        public static int WoodcuttingLv4BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("woodcutting_lv4_chance", WoodcuttingLv4BonusChance?.Value ?? 25);
        public static int GatheringLv4BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("gathering_lv4_chance", GatheringLv4BonusChance?.Value ?? 25);
        public static int MiningLv4BonusChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("mining_lv4_chance", MiningLv4BonusChance?.Value ?? 25);
        public static int CraftingLv4UpgradeChanceValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv4_upgrade_chance", CraftingLv4UpgradeChance?.Value ?? 25);
        public static int CraftingLv4DurabilityBonusValue =>
            (int)SkillTreeConfig.GetEffectiveValue("crafting_lv4_durability", CraftingLv4DurabilityBonus?.Value ?? 25);

        public static void Initialize(ConfigFile config)
        {
            // === 필요 포인트 설정 ===
            ProductionRootRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier0_생산전문가_필요포인트", 2,
                "Tier 0: 생산 전문가(production_root) - 필요 포인트");

            ProductionStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier1_초보일꾼_필요포인트", 2,
                "Tier 1: 초보 일꾼(novice_worker) - 필요 포인트");

            ProductionStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_전문분야_필요포인트", 2,
                "Tier 2: 벌목/채집/채광/제작 Lv2 - 필요 포인트");

            ProductionStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_중급스킬_필요포인트", 2,
                "Tier 3: 벌목/채집/채광/제작 Lv3 - 필요 포인트");

            ProductionStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_고급스킬_필요포인트", 3,
                "Tier 4: 벌목/채집/채광/제작 Lv4 - 필요 포인트");

            // === Tier 0: 생산 전문가 ===
            ProductionRootWoodBonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier0_생산전문가_나무추가확률", 50,
                "Tier 0: 생산 전문가 - 나무 +1 확률 (%)");

            // === Tier 1: 초보 일꾼 ===
            NoviceWorkerWoodBonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier1_초보일꾼_나무추가확률", 25,
                "Tier 1: 초보 일꾼 - 나무 +1 확률 (%)");

            // === Tier 2: 전문 분야 ===
            WoodcuttingLv2BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_벌목Lv2_추가확률", 25,
                "Tier 2: 벌목 Lv2 - 나무 +1 확률 (%)");

            GatheringLv2BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_채집Lv2_추가확률", 25,
                "Tier 2: 채집 Lv2 - 채집물 +1 확률 (%)");

            MiningLv2BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_채광Lv2_추가확률", 25,
                "Tier 2: 채광 Lv2 - 광석 +1 확률 (%)");

            CraftingLv2UpgradeChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_제작Lv2_강화확률", 25,
                "Tier 2: 제작 Lv2 - 강화 +1 확률 (%)");

            CraftingLv2DurabilityBonus = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier2_제작Lv2_내구도보너스", 25,
                "Tier 2: 제작 Lv2 - 내구도 최대치 증가 (%)");

            // === Tier 3: 중급 스킬 ===
            WoodcuttingLv3BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_벌목Lv3_추가확률", 25,
                "Tier 3: 벌목 Lv3 - 나무 +1 확률 (%)");

            GatheringLv3BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_채집Lv3_추가확률", 25,
                "Tier 3: 채집 Lv3 - 채집물 +1 확률 (%)");

            MiningLv3BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_채광Lv3_추가확률", 25,
                "Tier 3: 채광 Lv3 - 광석 +1 확률 (%)");

            CraftingLv3UpgradeChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_제작Lv3_강화확률", 25,
                "Tier 3: 제작 Lv3 - 강화 +1 확률 (%)");

            CraftingLv3DurabilityBonus = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier3_제작Lv3_내구도보너스", 25,
                "Tier 3: 제작 Lv3 - 내구도 최대치 증가 (%)");

            // === Tier 4: 고급 스킬 ===
            WoodcuttingLv4BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_벌목Lv4_추가확률", 25,
                "Tier 4: 벌목 Lv4 - 나무 +1 확률 (%)");

            GatheringLv4BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_채집Lv4_추가확률", 25,
                "Tier 4: 채집 Lv4 - 채집물 +1 확률 (%)");

            MiningLv4BonusChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_채광Lv4_추가확률", 25,
                "Tier 4: 채광 Lv4 - 광석 +1 확률 (%)");

            CraftingLv4UpgradeChance = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_제작Lv4_강화확률", 25,
                "Tier 4: 제작 Lv4 - 강화 +1 확률 (%)");

            CraftingLv4DurabilityBonus = SkillTreeConfig.BindServerSync(config,
                "Production Tree", "Tier4_제작Lv4_내구도보너스", 25,
                "Tier 4: 제작 Lv4 - 내구도 최대치 증가 (%)");

            Plugin.Log.LogDebug("[Production_Config] 생산 전문가 트리 설정 초기화 완료");
        }
    }
}

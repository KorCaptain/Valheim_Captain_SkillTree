using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가(Producer) 직업 전용 컨피그 시스템
    /// </summary>
    public static class Producer_Config
    {
        // === Active Skill: Artisan's Blessing (장인의 축복) ===
        public static ConfigEntry<float> ProducerBuff_Cooldown;        // 쿨타임 (초)
        public static ConfigEntry<float> ProducerBuff_Duration;        // 지속시간 (초)
        public static ConfigEntry<float> ProducerBuff_Range;           // 범위 (m)
        public static ConfigEntry<float> ProducerBuff_AttackBonus;     // 공격력 보너스 (%)
        public static ConfigEntry<float> ProducerBuff_MaxHealthBonus;  // 최대 체력 보너스 (%)
        public static ConfigEntry<float> ProducerBuff_StaminaCost;     // 스태미나 소모

        // === Passive Lv1-2: Farm Grid ===
        public static ConfigEntry<int> ProducerFarmGrid_Lv1;
        public static ConfigEntry<int> ProducerFarmGrid_Lv2;
        public static ConfigEntry<int> ProducerFarmGrid_Lv3;
        public static ConfigEntry<int> ProducerFarmGrid_Lv4;
        public static ConfigEntry<int> ProducerFarmGrid_Lv5;

        // === Passive Lv1+: Crafting Durability Bonus (%) ===
        public static ConfigEntry<float> ProducerDurability_Lv1;
        public static ConfigEntry<float> ProducerDurability_Lv2;
        public static ConfigEntry<float> ProducerDurability_Lv3;
        public static ConfigEntry<float> ProducerDurability_Lv4;
        public static ConfigEntry<float> ProducerDurability_Lv5;

        // === Passive Lv1+: Crafting Success Rate (%) ===
        public static ConfigEntry<float> ProducerCraftingSuccessRate_Lv1;
        public static ConfigEntry<float> ProducerCraftingSuccessRate_Lv2;
        public static ConfigEntry<float> ProducerCraftingSuccessRate_Lv3;
        public static ConfigEntry<float> ProducerCraftingSuccessRate_Lv4;
        public static ConfigEntry<float> ProducerCraftingSuccessRate_Lv5;

        // === Passive Lv2+: Material Reduction (%) ===
        public static ConfigEntry<float> ProducerMaterialReduction_Lv2;
        public static ConfigEntry<float> ProducerMaterialReduction_Lv3;
        public static ConfigEntry<float> ProducerMaterialReduction_Lv4;
        public static ConfigEntry<float> ProducerMaterialReduction_Lv5;

        // === Enchant Chance (%) — 레벨별 마법부여 확률만 컨피그로 관리 ===
        // 수치(min/max)는 Producer_Enchant.json (EmbeddedResource)에서 관리
        public static ConfigEntry<float> ProducerEnchantChance_Lv1;
        public static ConfigEntry<float> ProducerEnchantChance_Lv2;
        public static ConfigEntry<float> ProducerEnchantChance_Lv3;
        public static ConfigEntry<float> ProducerEnchantChance_Lv4;
        public static ConfigEntry<float> ProducerEnchantChance_Lv5;

        // === 동적 값 접근자 ===
        public static float ProducerBuff_CooldownValue    => SkillTreeConfig.GetEffectiveValue("Producer_Buff_Cooldown", ProducerBuff_Cooldown.Value);
        public static float ProducerBuff_DurationValue    => SkillTreeConfig.GetEffectiveValue("Producer_Buff_Duration", ProducerBuff_Duration.Value);
        public static float ProducerBuff_RangeValue       => SkillTreeConfig.GetEffectiveValue("Producer_Buff_Range", ProducerBuff_Range.Value);
        public static float ProducerBuff_AttackBonusValue => SkillTreeConfig.GetEffectiveValue("Producer_Buff_AttackBonus", ProducerBuff_AttackBonus.Value);
        public static float ProducerBuff_MaxHealthBonusValue => SkillTreeConfig.GetEffectiveValue("Producer_Buff_MaxHealthBonus", ProducerBuff_MaxHealthBonus.Value);
        public static float ProducerBuff_StaminaCostValue => SkillTreeConfig.GetEffectiveValue("Producer_Buff_StaminaCost", ProducerBuff_StaminaCost.Value);

        public static int ProducerFarmGrid_Lv1Value => (int)SkillTreeConfig.GetEffectiveValue("Producer_FarmGrid_Lv1", ProducerFarmGrid_Lv1.Value);
        public static int ProducerFarmGrid_Lv2Value => (int)SkillTreeConfig.GetEffectiveValue("Producer_FarmGrid_Lv2", ProducerFarmGrid_Lv2.Value);
        public static int ProducerFarmGrid_Lv3Value => (int)SkillTreeConfig.GetEffectiveValue("Producer_FarmGrid_Lv3", ProducerFarmGrid_Lv3.Value);
        public static int ProducerFarmGrid_Lv4Value => (int)SkillTreeConfig.GetEffectiveValue("Producer_FarmGrid_Lv4", ProducerFarmGrid_Lv4.Value);
        public static int ProducerFarmGrid_Lv5Value => (int)SkillTreeConfig.GetEffectiveValue("Producer_FarmGrid_Lv5", ProducerFarmGrid_Lv5.Value);

        public static float ProducerDurability_Lv1Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv1", ProducerDurability_Lv1.Value);
        public static float ProducerDurability_Lv2Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv2", ProducerDurability_Lv2.Value);
        public static float ProducerDurability_Lv3Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv3", ProducerDurability_Lv3.Value);
        public static float ProducerDurability_Lv4Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv4", ProducerDurability_Lv4.Value);
        public static float ProducerDurability_Lv5Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv5", ProducerDurability_Lv5.Value);

        public static float ProducerCraftingSuccessRate_Lv1Value => SkillTreeConfig.GetEffectiveValue("Producer_CraftingSuccessRate_Lv1", ProducerCraftingSuccessRate_Lv1.Value);
        public static float ProducerCraftingSuccessRate_Lv2Value => SkillTreeConfig.GetEffectiveValue("Producer_CraftingSuccessRate_Lv2", ProducerCraftingSuccessRate_Lv2.Value);
        public static float ProducerCraftingSuccessRate_Lv3Value => SkillTreeConfig.GetEffectiveValue("Producer_CraftingSuccessRate_Lv3", ProducerCraftingSuccessRate_Lv3.Value);
        public static float ProducerCraftingSuccessRate_Lv4Value => SkillTreeConfig.GetEffectiveValue("Producer_CraftingSuccessRate_Lv4", ProducerCraftingSuccessRate_Lv4.Value);
        public static float ProducerCraftingSuccessRate_Lv5Value => SkillTreeConfig.GetEffectiveValue("Producer_CraftingSuccessRate_Lv5", ProducerCraftingSuccessRate_Lv5.Value);

        public static float ProducerMaterialReduction_Lv2Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv2", ProducerMaterialReduction_Lv2.Value);
        public static float ProducerMaterialReduction_Lv3Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv3", ProducerMaterialReduction_Lv3.Value);
        public static float ProducerMaterialReduction_Lv4Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv4", ProducerMaterialReduction_Lv4.Value);
        public static float ProducerMaterialReduction_Lv5Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv5", ProducerMaterialReduction_Lv5.Value);

        public static float ProducerEnchantChance_Lv1Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv1", ProducerEnchantChance_Lv1.Value);
        public static float ProducerEnchantChance_Lv2Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv2", ProducerEnchantChance_Lv2.Value);
        public static float ProducerEnchantChance_Lv3Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv3", ProducerEnchantChance_Lv3.Value);
        public static float ProducerEnchantChance_Lv4Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv4", ProducerEnchantChance_Lv4.Value);
        public static float ProducerEnchantChance_Lv5Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv5", ProducerEnchantChance_Lv5.Value);

        // === 레벨별 내구도 보너스 반환 헬퍼 ===
        public static float GetDurabilityBonus(int level)
        {
            return level switch {
                1 => ProducerDurability_Lv1Value,
                2 => ProducerDurability_Lv2Value,
                3 => ProducerDurability_Lv3Value,
                4 => ProducerDurability_Lv4Value,
                _ => level >= 5 ? ProducerDurability_Lv5Value : 0f
            };
        }

        // === 레벨별 제작 성공확률 반환 헬퍼 ===
        public static float GetCraftingSuccessRate(int level)
        {
            return level switch {
                1 => ProducerCraftingSuccessRate_Lv1Value,
                2 => ProducerCraftingSuccessRate_Lv2Value,
                3 => ProducerCraftingSuccessRate_Lv3Value,
                4 => ProducerCraftingSuccessRate_Lv4Value,
                _ => level >= 5 ? ProducerCraftingSuccessRate_Lv5Value : 0f
            };
        }

        // === 레벨별 재료 감소율 반환 헬퍼 ===
        public static float GetMaterialReduction(int level)
        {
            return level switch {
                2 => ProducerMaterialReduction_Lv2Value,
                3 => ProducerMaterialReduction_Lv3Value,
                4 => ProducerMaterialReduction_Lv4Value,
                _ => level >= 5 ? ProducerMaterialReduction_Lv5Value : 0f
            };
        }

        // === 레벨별 마법부여 확률 반환 헬퍼 ===
        public static float GetEnchantChance(int level)
        {
            return level switch {
                1 => ProducerEnchantChance_Lv1Value,
                2 => ProducerEnchantChance_Lv2Value,
                3 => ProducerEnchantChance_Lv3Value,
                4 => ProducerEnchantChance_Lv4Value,
                _ => level >= 5 ? ProducerEnchantChance_Lv5Value : 0f
            };
        }

        // === 레벨별 농사 그리드 (rows, cols) 반환 ===
        // lv1=1x2, lv2=2x2, lv3=2x3, lv4=3x3, lv5=3x4
        public static (int rows, int cols) GetFarmGridDimensions(int level)
        {
            return level switch {
                1 => (1, 2),
                2 => (2, 2),
                3 => (2, 3),
                4 => (3, 3),
                _ => level >= 5 ? (3, 4) : (0, 0)
            };
        }

        public static int GetFarmGridCount(int level)
        {
            var (rows, cols) = GetFarmGridDimensions(level);
            return rows * cols;
        }

        /// <summary>
        /// 제작 전문가 컨피그 초기화
        /// </summary>
        public static void InitializeProducerConfig(ConfigFile config)
        {
            try
            {
                // === Active Skill ===
                ProducerBuff_Cooldown = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Buff_Cooldown", 180f,
                    SkillTreeConfig.GetConfigDescription("Producer_Buff_Cooldown"));

                ProducerBuff_Duration = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Buff_Duration", 120f,
                    SkillTreeConfig.GetConfigDescription("Producer_Buff_Duration"));

                ProducerBuff_Range = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Buff_Range", 15f,
                    SkillTreeConfig.GetConfigDescription("Producer_Buff_Range"));

                ProducerBuff_AttackBonus = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Buff_AttackBonus", 15f,
                    SkillTreeConfig.GetConfigDescription("Producer_Buff_AttackBonus"));

                ProducerBuff_MaxHealthBonus = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Buff_MaxHealthBonus", 15f,
                    SkillTreeConfig.GetConfigDescription("Producer_Buff_MaxHealthBonus"));

                ProducerBuff_StaminaCost = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Buff_StaminaCost", 20f,
                    SkillTreeConfig.GetConfigDescription("Producer_Buff_StaminaCost"));

                // === Lv1 패시브 ===
                ProducerFarmGrid_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_FarmGrid_Lv1", 2,
                    SkillTreeConfig.GetConfigDescription("Producer_FarmGrid_Lv1"));

                ProducerDurability_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Durability_Lv1", 50f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv1"));

                ProducerCraftingSuccessRate_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_CraftingSuccessRate_Lv1", 25f,
                    SkillTreeConfig.GetConfigDescription("Producer_CraftingSuccessRate_Lv1"));

                ProducerEnchantChance_Lv1 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantChance_Lv1", 0f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantChance_Lv1"));

                // === Lv2 패시브 ===
                ProducerFarmGrid_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_FarmGrid_Lv2", 2,
                    SkillTreeConfig.GetConfigDescription("Producer_FarmGrid_Lv2"));

                ProducerDurability_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Durability_Lv2", 75f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv2"));

                ProducerCraftingSuccessRate_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_CraftingSuccessRate_Lv2", 45f,
                    SkillTreeConfig.GetConfigDescription("Producer_CraftingSuccessRate_Lv2"));

                ProducerMaterialReduction_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_MaterialReduction_Lv2", 10f,
                    SkillTreeConfig.GetConfigDescription("Producer_MaterialReduction_Lv2"));

                ProducerEnchantChance_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantChance_Lv2", 0f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantChance_Lv2"));

                // === Lv3 패시브 ===
                ProducerFarmGrid_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_FarmGrid_Lv3", 4,
                    SkillTreeConfig.GetConfigDescription("Producer_FarmGrid_Lv3"));

                ProducerDurability_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Durability_Lv3", 100f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv3"));

                ProducerCraftingSuccessRate_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_CraftingSuccessRate_Lv3", 65f,
                    SkillTreeConfig.GetConfigDescription("Producer_CraftingSuccessRate_Lv3"));

                ProducerMaterialReduction_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_MaterialReduction_Lv3", 15f,
                    SkillTreeConfig.GetConfigDescription("Producer_MaterialReduction_Lv3"));

                ProducerEnchantChance_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantChance_Lv3", 25f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantChance_Lv3"));

                // === Lv4 패시브 ===
                ProducerFarmGrid_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_FarmGrid_Lv4", 6,
                    SkillTreeConfig.GetConfigDescription("Producer_FarmGrid_Lv4"));

                ProducerDurability_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Durability_Lv4", 125f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv4"));

                ProducerCraftingSuccessRate_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_CraftingSuccessRate_Lv4", 75f,
                    SkillTreeConfig.GetConfigDescription("Producer_CraftingSuccessRate_Lv4"));

                ProducerMaterialReduction_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_MaterialReduction_Lv4", 20f,
                    SkillTreeConfig.GetConfigDescription("Producer_MaterialReduction_Lv4"));

                ProducerEnchantChance_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantChance_Lv4", 30f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantChance_Lv4"));

                // === Lv5 패시브 ===
                ProducerFarmGrid_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_FarmGrid_Lv5", 8,
                    SkillTreeConfig.GetConfigDescription("Producer_FarmGrid_Lv5"));

                ProducerDurability_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Durability_Lv5", 200f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv5"));

                ProducerCraftingSuccessRate_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_CraftingSuccessRate_Lv5", 100f,
                    SkillTreeConfig.GetConfigDescription("Producer_CraftingSuccessRate_Lv5"));

                ProducerMaterialReduction_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_MaterialReduction_Lv5", 30f,
                    SkillTreeConfig.GetConfigDescription("Producer_MaterialReduction_Lv5"));

                ProducerEnchantChance_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantChance_Lv5", 35f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantChance_Lv5"));

                Plugin.Log.LogDebug("[제작 전문가 컨피그] 초기화 완료");
                RegisterProducerEventHandlers();
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[제작 전문가 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        private static void RegisterProducerEventHandlers()
        {
            try
            {
                ProducerBuff_Cooldown.SettingChanged      += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerBuff_Duration.SettingChanged      += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerBuff_Range.SettingChanged         += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerBuff_AttackBonus.SettingChanged   += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerBuff_MaxHealthBonus.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerBuff_StaminaCost.SettingChanged   += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerFarmGrid_Lv1.SettingChanged       += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerFarmGrid_Lv2.SettingChanged       += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerFarmGrid_Lv3.SettingChanged       += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerFarmGrid_Lv4.SettingChanged       += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerFarmGrid_Lv5.SettingChanged       += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv1.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv2.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv3.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv4.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv5.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv2.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv3.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv4.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv5.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerCraftingSuccessRate_Lv1.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerCraftingSuccessRate_Lv2.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerCraftingSuccessRate_Lv3.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerCraftingSuccessRate_Lv4.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerCraftingSuccessRate_Lv5.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerEnchantChance_Lv1.SettingChanged  += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerEnchantChance_Lv2.SettingChanged  += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerEnchantChance_Lv3.SettingChanged  += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerEnchantChance_Lv4.SettingChanged  += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerEnchantChance_Lv5.SettingChanged  += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                Plugin.Log.LogDebug("[제작 전문가 컨피그] 이벤트 핸들러 등록 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[제작 전문가 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }
    }
}

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

        // === Passive Lv2+: Crafting Durability Bonus (%) ===
        public static ConfigEntry<float> ProducerDurability_Lv2;
        public static ConfigEntry<float> ProducerDurability_Lv3;
        public static ConfigEntry<float> ProducerDurability_Lv4;
        public static ConfigEntry<float> ProducerDurability_Lv5;

        // === Passive Lv2+: Material Reduction (%) ===
        public static ConfigEntry<float> ProducerMaterialReduction_Lv2;
        public static ConfigEntry<float> ProducerMaterialReduction_Lv3;
        public static ConfigEntry<float> ProducerMaterialReduction_Lv4;
        public static ConfigEntry<float> ProducerMaterialReduction_Lv5;

        // === Passive Lv3+: Enchant Chance (%) ===
        public static ConfigEntry<float> ProducerEnchantChance_Lv3;
        public static ConfigEntry<float> ProducerEnchantChance_Lv4;
        public static ConfigEntry<float> ProducerEnchantChance_Lv5;

        // === Enchant Value Ranges ===
        public static ConfigEntry<float> ProducerEnchantWeaponDmgMin_Lv3;
        public static ConfigEntry<float> ProducerEnchantWeaponDmgMax_Lv3;
        public static ConfigEntry<float> ProducerEnchantWeaponDmgMin_Lv4;
        public static ConfigEntry<float> ProducerEnchantWeaponDmgMax_Lv4;
        public static ConfigEntry<float> ProducerEnchantWeaponDmgMin_Lv5;
        public static ConfigEntry<float> ProducerEnchantWeaponDmgMax_Lv5;

        public static ConfigEntry<float> ProducerEnchantArmorMin_Lv3;
        public static ConfigEntry<float> ProducerEnchantArmorMax_Lv3;
        public static ConfigEntry<float> ProducerEnchantArmorMin_Lv4;
        public static ConfigEntry<float> ProducerEnchantArmorMax_Lv4;
        public static ConfigEntry<float> ProducerEnchantArmorMin_Lv5;
        public static ConfigEntry<float> ProducerEnchantArmorMax_Lv5;

        public static ConfigEntry<float> ProducerEnchantHpMin_Lv3;
        public static ConfigEntry<float> ProducerEnchantHpMax_Lv3;
        public static ConfigEntry<float> ProducerEnchantHpMin_Lv4;
        public static ConfigEntry<float> ProducerEnchantHpMax_Lv4;
        public static ConfigEntry<float> ProducerEnchantHpMin_Lv5;
        public static ConfigEntry<float> ProducerEnchantHpMax_Lv5;

        // === Enchant Value Ranges: Weapon Speed ===
        public static ConfigEntry<float> ProducerEnchantWeaponSpdMin_Lv3;
        public static ConfigEntry<float> ProducerEnchantWeaponSpdMax_Lv3;
        public static ConfigEntry<float> ProducerEnchantWeaponSpdMin_Lv4;
        public static ConfigEntry<float> ProducerEnchantWeaponSpdMax_Lv4;
        public static ConfigEntry<float> ProducerEnchantWeaponSpdMin_Lv5;
        public static ConfigEntry<float> ProducerEnchantWeaponSpdMax_Lv5;

        // === Enchant Value Ranges: Max Stamina ===
        public static ConfigEntry<float> ProducerEnchantStaminaMin_Lv3;
        public static ConfigEntry<float> ProducerEnchantStaminaMax_Lv3;
        public static ConfigEntry<float> ProducerEnchantStaminaMin_Lv4;
        public static ConfigEntry<float> ProducerEnchantStaminaMax_Lv4;
        public static ConfigEntry<float> ProducerEnchantStaminaMin_Lv5;
        public static ConfigEntry<float> ProducerEnchantStaminaMax_Lv5;

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

        public static float ProducerDurability_Lv2Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv2", ProducerDurability_Lv2.Value);
        public static float ProducerDurability_Lv3Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv3", ProducerDurability_Lv3.Value);
        public static float ProducerDurability_Lv4Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv4", ProducerDurability_Lv4.Value);
        public static float ProducerDurability_Lv5Value => SkillTreeConfig.GetEffectiveValue("Producer_Durability_Lv5", ProducerDurability_Lv5.Value);

        public static float ProducerMaterialReduction_Lv2Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv2", ProducerMaterialReduction_Lv2.Value);
        public static float ProducerMaterialReduction_Lv3Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv3", ProducerMaterialReduction_Lv3.Value);
        public static float ProducerMaterialReduction_Lv4Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv4", ProducerMaterialReduction_Lv4.Value);
        public static float ProducerMaterialReduction_Lv5Value => SkillTreeConfig.GetEffectiveValue("Producer_MaterialReduction_Lv5", ProducerMaterialReduction_Lv5.Value);

        public static float ProducerEnchantChance_Lv3Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv3", ProducerEnchantChance_Lv3.Value);
        public static float ProducerEnchantChance_Lv4Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv4", ProducerEnchantChance_Lv4.Value);
        public static float ProducerEnchantChance_Lv5Value => SkillTreeConfig.GetEffectiveValue("Producer_EnchantChance_Lv5", ProducerEnchantChance_Lv5.Value);

        // === 레벨별 내구도 보너스 반환 헬퍼 ===
        public static float GetDurabilityBonus(int level)
        {
            return level switch {
                2 => ProducerDurability_Lv2Value,
                3 => ProducerDurability_Lv3Value,
                4 => ProducerDurability_Lv4Value,
                _ => level >= 5 ? ProducerDurability_Lv5Value : 0f
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

                // === Lv2 패시브 ===
                ProducerFarmGrid_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_FarmGrid_Lv2", 2,
                    SkillTreeConfig.GetConfigDescription("Producer_FarmGrid_Lv2"));

                ProducerDurability_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Durability_Lv2", 10f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv2"));

                ProducerMaterialReduction_Lv2 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_MaterialReduction_Lv2", 10f,
                    SkillTreeConfig.GetConfigDescription("Producer_MaterialReduction_Lv2"));

                // === Lv3 패시브 ===
                ProducerFarmGrid_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_FarmGrid_Lv3", 4,
                    SkillTreeConfig.GetConfigDescription("Producer_FarmGrid_Lv3"));

                ProducerDurability_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_Durability_Lv3", 15f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv3"));

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
                    "Producer Job Skills", "Producer_Durability_Lv4", 20f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv4"));

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
                    "Producer Job Skills", "Producer_Durability_Lv5", 30f,
                    SkillTreeConfig.GetConfigDescription("Producer_Durability_Lv5"));

                ProducerMaterialReduction_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_MaterialReduction_Lv5", 30f,
                    SkillTreeConfig.GetConfigDescription("Producer_MaterialReduction_Lv5"));

                ProducerEnchantChance_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantChance_Lv5", 35f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantChance_Lv5"));

                // === Enchant Weapon Damage ===
                ProducerEnchantWeaponDmgMin_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponDmgMin_Lv3", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponDmgMin_Lv3"));
                ProducerEnchantWeaponDmgMax_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponDmgMax_Lv3", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponDmgMax_Lv3"));
                ProducerEnchantWeaponDmgMin_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponDmgMin_Lv4", 7f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponDmgMin_Lv4"));
                ProducerEnchantWeaponDmgMax_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponDmgMax_Lv4", 9f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponDmgMax_Lv4"));
                ProducerEnchantWeaponDmgMin_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponDmgMin_Lv5", 10f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponDmgMin_Lv5"));
                ProducerEnchantWeaponDmgMax_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponDmgMax_Lv5", 12f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponDmgMax_Lv5"));

                // === Enchant Armor ===
                ProducerEnchantArmorMin_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantArmorMin_Lv3", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantArmorMin_Lv3"));
                ProducerEnchantArmorMax_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantArmorMax_Lv3", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantArmorMax_Lv3"));
                ProducerEnchantArmorMin_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantArmorMin_Lv4", 7f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantArmorMin_Lv4"));
                ProducerEnchantArmorMax_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantArmorMax_Lv4", 9f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantArmorMax_Lv4"));
                ProducerEnchantArmorMin_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantArmorMin_Lv5", 10f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantArmorMin_Lv5"));
                ProducerEnchantArmorMax_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantArmorMax_Lv5", 12f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantArmorMax_Lv5"));

                // === Enchant HP ===
                ProducerEnchantHpMin_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantHpMin_Lv3", 2f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantHpMin_Lv3"));
                ProducerEnchantHpMax_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantHpMax_Lv3", 2f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantHpMax_Lv3"));
                ProducerEnchantHpMin_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantHpMin_Lv4", 4f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantHpMin_Lv4"));
                ProducerEnchantHpMax_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantHpMax_Lv4", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantHpMax_Lv4"));
                ProducerEnchantHpMin_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantHpMin_Lv5", 6f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantHpMin_Lv5"));
                ProducerEnchantHpMax_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantHpMax_Lv5", 8f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantHpMax_Lv5"));

                // === Enchant Weapon Speed ===
                ProducerEnchantWeaponSpdMin_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponSpdMin_Lv3", 3f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponSpdMin_Lv3"));
                ProducerEnchantWeaponSpdMax_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponSpdMax_Lv3", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponSpdMax_Lv3"));
                ProducerEnchantWeaponSpdMin_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponSpdMin_Lv4", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponSpdMin_Lv4"));
                ProducerEnchantWeaponSpdMax_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponSpdMax_Lv4", 8f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponSpdMax_Lv4"));
                ProducerEnchantWeaponSpdMin_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponSpdMin_Lv5", 8f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponSpdMin_Lv5"));
                ProducerEnchantWeaponSpdMax_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantWeaponSpdMax_Lv5", 12f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantWeaponSpdMax_Lv5"));

                // === Enchant Max Stamina ===
                ProducerEnchantStaminaMin_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantStaminaMin_Lv3", 5f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantStaminaMin_Lv3"));
                ProducerEnchantStaminaMax_Lv3 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantStaminaMax_Lv3", 8f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantStaminaMax_Lv3"));
                ProducerEnchantStaminaMin_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantStaminaMin_Lv4", 8f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantStaminaMin_Lv4"));
                ProducerEnchantStaminaMax_Lv4 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantStaminaMax_Lv4", 12f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantStaminaMax_Lv4"));
                ProducerEnchantStaminaMin_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantStaminaMin_Lv5", 12f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantStaminaMin_Lv5"));
                ProducerEnchantStaminaMax_Lv5 = SkillTreeConfig.BindServerSync(config,
                    "Producer Job Skills", "Producer_EnchantStaminaMax_Lv5", 15f,
                    SkillTreeConfig.GetConfigDescription("Producer_EnchantStaminaMax_Lv5"));

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
                ProducerDurability_Lv2.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv3.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv4.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerDurability_Lv5.SettingChanged     += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv2.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv3.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv4.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
                ProducerMaterialReduction_Lv5.SettingChanged += (s, a) => Producer_Tooltip.UpdateProducerTooltip();
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

using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetEnglishKeyNames()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Skill_Tree_Base - Key Bindings
                // ============================================
                ["HotKey_Y"] = "Job Skill Key",
                ["HotKey_R"] = "Ranged Skill Key",
                ["HotKey_G"] = "Melee Main Skill Key",
                ["HotKey_H"] = "Secondary Skill Key",
                ["HUD_PosX"] = "HUD X Position",
                ["HUD_PosY"] = "HUD Y Position",

                // ============================================
                // Attack Tree - 33 Keys
                // ============================================


                // === Tier 0: Attack Expert (2) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: [Attack Expert] All Damage Bonus (%)",
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: [Attack Expert] Required Points",

                // === Tier 1: Base Attack (3) ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] = "Tier 1: [Base Attack] Physical Damage Bonus (%)",
                ["Tier1_BaseAttack_ElementalDamageBonus"] = "Tier 1: [Base Attack] Elemental Damage Bonus (%)",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: [Base Attack] Required Points",

                // === Tier 2: Weapon Specialization (12) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2-1: [Melee Spec] Trigger Chance (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2-1: [Melee Spec] Bonus Damage",
                ["Tier2_MeleeSpec_RequiredPoints"] = "Tier 2-1: [Melee Spec] Required Points",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2-2: [Bow Spec] Trigger Chance (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2-2: [Bow Spec] Bonus Damage",
                ["Tier2_BowSpec_RequiredPoints"] = "Tier 2-2: [Bow Spec] Required Points",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2-3: [Crossbow Spec] Trigger Chance (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2-3: [Crossbow Spec] Bonus Damage",
                ["Tier2_CrossbowSpec_RequiredPoints"] = "Tier 2-3: [Crossbow Spec] Required Points",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2-4: [Staff Spec] Trigger Chance (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2-4: [Staff Spec] Bonus Damage",
                ["Tier2_StaffSpec_RequiredPoints"] = "Tier 2-4: [Staff Spec] Required Points",

                // === Tier 3: Attack Boost (3) ===
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: [Attack Boost] Physical Damage Bonus (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: [Attack Boost] Elemental Damage Bonus (%)",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: [Attack Boost] Required Points",

                // === Tier 4: Combat Enhancement (6) ===
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4-1: [Melee Enhance] 2-Hit Combo Bonus (%)",
                ["Tier4_MeleeEnhance_RequiredPoints"] = "Tier 4-1: [Melee Enhance] Required Points",
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4-2: [Precision Attack] Crit Chance (%)",
                ["Tier4_PrecisionAttack_RequiredPoints"] = "Tier 4-2: [Precision Attack] Required Points",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4-3: [Ranged Enhance] Damage Bonus (%)",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4-3: [Ranged Enhance] Required Points",

                // === Tier 5: Specialized Stats (2) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: [Specialized Stats] Weapon Specialization Bonus",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: [Specialized Stats] Required Points",

                // === Tier 6: Final Enhancement (8) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6-1: [Weak Point] Crit Damage Bonus (%)",
                ["Tier6_WeakPointAttack_RequiredPoints"] = "Tier 6-1: [Weak Point] Required Points",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6-2: [Combo Finisher] 3-Hit Combo Bonus (%)",
                ["Tier6_ComboFinisher_RequiredPoints"] = "Tier 6-2: [Combo Finisher] Required Points",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6-3: [Two-Hand Crush] Damage Bonus (%)",
                ["Tier6_TwoHandCrush_RequiredPoints"] = "Tier 6-3: [Two-Hand Crush] Required Points",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6-4: [Elemental Attack] Elemental Bonus (%)",
                ["Tier6_ElementalAttack_RequiredPoints"] = "Tier 6-4: [Elemental Attack] Required Points",

                // ============================================
                // Speed Tree - 49 Keys
                // ============================================


                // === Tier 0: Speed Expert (2) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: [Speed Expert] Move Speed Bonus (%)",
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: [Speed Expert] Required Points",

                // === Tier 1: Agility Foundation (5) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: [Agility Foundation] Post-Dodge Move Speed Bonus (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: [Agility Foundation] Buff Duration (sec)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: [Agility Foundation] Attack Speed Bonus (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: [Agility Foundation] Dodge Speed Bonus (%)",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: [Agility Foundation] Required Points",

                // === Tier 2-1: Continuous Flow (5) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2-1: [Continuous Flow] Attack Speed Bonus on 2-Hit (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2-1: [Continuous Flow] Stamina Reduction (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2-1: [Continuous Flow] Buff Duration (sec)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2-1: [Continuous Flow] Combo Speed Bonus (%)",
                ["Tier2_MeleeFlow_RequiredPoints"] = "Tier 2-1: [Continuous Flow] Required Points",

                // === Tier 2-2: Crossbow Expert (4) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2-2: [Crossbow Expert] Move Speed Bonus on Hit (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2-2: [Crossbow Expert] Buff Duration (sec)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2-2: [Crossbow Expert] Reload Speed Bonus During Buff (%)",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "Tier 2-2: [Crossbow Expert] Required Points",

                // === Tier 2-3: Bow Expert (4) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2-3: [Bow Expert] Stamina Reduction on 2-Hit Combo (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2-3: [Bow Expert] Next Arrow Draw Speed Bonus (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2-3: [Bow Expert] Buff Duration (sec)",
                ["Tier2_BowExpert_RequiredPoints"] = "Tier 2-3: [Bow Expert] Required Points",

                // === Tier 2-4: Mobile Cast (4) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2-4: [Mobile Cast] Move Speed Bonus While Casting (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2-4: [Mobile Cast] Eitr Cost Reduction (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2-4: [Mobile Cast] Move Speed While Staff Casting (%)",
                ["Tier2_MobileCast_RequiredPoints"] = "Tier 2-4: [Mobile Cast] Required Points",

                // === Tier 3-1: Practitioner 1 (3) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3-1: [Practitioner 1] Melee Proficiency Bonus",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3-1: [Practitioner 1] Crossbow Proficiency Bonus",
                ["Tier3_Practitioner1_RequiredPoints"] = "Tier 3-1: [Practitioner 1] Required Points",

                // === Tier 3-2: Practitioner 2 (3) ===
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3-2: [Practitioner 2] Staff Proficiency Bonus",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3-2: [Practitioner 2] Bow Proficiency Bonus",
                ["Tier3_Practitioner2_RequiredPoints"] = "Tier 3-2: [Practitioner 2] Required Points",

                // === Tier 4-1: Energizer (2) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4-1: [Energizer] Food Consumption Rate Reduction (%)",
                ["Tier4_Energizer_RequiredPoints"] = "Tier 4-1: [Energizer] Required Points",

                // === Tier 4-2: Captain (2) ===
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4-2: [Captain] Ship Speed Bonus (%)",
                ["Tier4_Captain_RequiredPoints"] = "Tier 4-2: [Captain] Required Points",

                // === Tier 5: Jump Master (3) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: [Jump Master] Jump Proficiency Bonus",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: [Jump Master] Jump Stamina Reduction (%)",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: [Jump Master] Required Points",

                // === Tier 6-1: Dexterity (3) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6-1: [Dexterity] Melee Attack Speed Bonus (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6-1: [Dexterity] Move Speed Bonus (%)",
                ["Tier6_Dexterity_RequiredPoints"] = "Tier 6-1: [Dexterity] Required Points",

                // === Tier 6-2: Endurance (2) ===
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6-2: [Endurance] Max Stamina Bonus",
                ["Tier6_Endurance_RequiredPoints"] = "Tier 6-2: [Endurance] Required Points",

                // === Tier 6-3: Intellect (2) ===
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6-3: [Intellect] Max Eitr Bonus",
                ["Tier6_Intellect_RequiredPoints"] = "Tier 6-3: [Intellect] Required Points",

                // === Tier 7: Master (3) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: [Master] Run Proficiency Bonus",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: [Master] Jump Proficiency Bonus",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: [Master] Required Points",

                // === Tier 8-1: Melee Acceleration (3) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8-1: [Melee Accel] Melee Attack Speed Bonus (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8-1: [Melee Accel] Next Attack Speed Bonus on 3-Hit Combo (%)",
                ["Tier8_MeleeAccel_RequiredPoints"] = "Tier 8-1: [Melee Accel] Required Points",

                // === Tier 8-2: Crossbow Acceleration (3) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8-2: [Crossbow Accel] Reload Speed Bonus (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8-2: [Crossbow Accel] Move Speed During Reload (%)",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "Tier 8-2: [Crossbow Accel] Required Points",

                // === Tier 8-3: Bow Acceleration (3) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8-3: [Bow Accel] Draw Speed Bonus (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8-3: [Bow Accel] Move Speed While Drawing (%)",
                ["Tier8_BowAccel_RequiredPoints"] = "Tier 8-3: [Bow Accel] Required Points",

                // === Tier 8-4: Cast Acceleration (3) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8-4: [Cast Accel] Magic Attack Speed Bonus (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8-4: [Cast Accel] Eitr Max Recovery Rate on 3-Hit Combo (%)",
                ["Tier8_CastAccel_RequiredPoints"] = "Tier 8-4: [Cast Accel] Required Points",

                // ============================================
                // Defense Tree - 59 Keys
                // ============================================

                // === Tier 0: Defense Expert (3) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: [Defense Expert] HP Bonus",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: [Defense Expert] Armor Bonus",
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: [Defense Expert] Required Points",

                // === Tier 1: Skin Hardening (3) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: [Skin Hardening] HP Bonus",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: [Skin Hardening] Armor Bonus",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: [Skin Hardening] Required Points",

                // === Tier 2-1: Mind-Body Training (3) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2-1: [Mind-Body Training] Max Stamina Bonus",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2-1: [Mind-Body Training] Max Eitr Bonus",
                ["Tier2_MindTraining_RequiredPoints"] = "Tier 2-1: [Mind-Body Training] Required Points",

                // === Tier 2-2: Health Training (3) ===
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2-2: [Health Training] HP Bonus",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2-2: [Health Training] Armor Bonus",
                ["Tier2_HealthTraining_RequiredPoints"] = "Tier 2-2: [Health Training] Required Points",

                // === Tier 3-1: Core Breathing (2) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3-1: [Core Breathing] Eitr Bonus",
                ["Tier3_CoreBreathing_RequiredPoints"] = "Tier 3-1: [Core Breathing] Required Points",

                // === Tier 3-2: Evasion Training (3) ===
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3-2: [Evasion Training] Dodge Bonus (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3-2: [Evasion Training] Roll Invincibility Increase (%)",
                ["Tier3_EvasionTraining_RequiredPoints"] = "Tier 3-2: [Evasion Training] Required Points",

                // === Tier 3-3: Health Boost (2) ===
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3-3: [Health Boost] HP Bonus",
                ["Tier3_HealthBoost_RequiredPoints"] = "Tier 3-3: [Health Boost] Required Points",

                // === Tier 3-4: Shield Training (2) ===
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3-4: [Shield Training] Shield Block Power Bonus",
                ["Tier3_ShieldTraining_RequiredPoints"] = "Tier 3-4: [Shield Training] Required Points",

                // === Tier 4-1: Shockwave (4) ===
                ["Tier4_Shockwave_Radius"] = "Tier 4-1: [Shockwave] Radius",
                ["Tier4_Shockwave_StunDuration"] = "Tier 4-1: [Shockwave] Stun Duration",
                ["Tier4_Shockwave_Cooldown"] = "Tier 4-1: [Shockwave] Cooldown",
                ["Tier4_Shockwave_RequiredPoints"] = "Tier 4-1: [Shockwave] Required Points",

                // === Tier 4-2: Ground Stomp (6) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4-2: [Ground Stomp] Effect Radius (m)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4-2: [Ground Stomp] Knockback Force",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4-2: [Ground Stomp] Cooldown (sec)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4-2: [Ground Stomp] Auto-Trigger HP Threshold",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4-2: [Ground Stomp] VFX Duration (sec)",
                ["Tier4_GroundStomp_RequiredPoints"] = "Tier 4-2: [Ground Stomp] Required Points",

                // === Tier 4-3: Rock Skin (2) ===
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4-3: [Rock Skin] Armor Amplification (%)",
                ["Tier4_RockSkin_RequiredPoints"] = "Tier 4-3: [Rock Skin] Required Points",

                // === Tier 5-1: Endurance (3) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5-1: [Endurance] Run Stamina Reduction (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5-1: [Endurance] Jump Stamina Reduction (%)",
                ["Tier5_Endurance_RequiredPoints"] = "Tier 5-1: [Endurance] Required Points",

                // === Tier 5-2: Agility (3) ===
                ["Tier5_Agility_DodgeBonus"] = "Tier 5-2: [Agility] Dodge Bonus (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5-2: [Agility] Roll Stamina Reduction (%)",
                ["Tier5_Agility_RequiredPoints"] = "Tier 5-2: [Agility] Required Points",

                // === Tier 5-3: Troll Regen (3) ===
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5-3: [Troll Regen] HP Regen Bonus (per sec)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5-3: [Troll Regen] Regen Interval (sec)",
                ["Tier5_TrollRegen_RequiredPoints"] = "Tier 5-3: [Troll Regen] Required Points",

                // === Tier 5-4: Block Master (3) ===
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5-4: [Block Master] Shield Block Power Bonus",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5-4: [Block Master] Parry Duration Bonus (sec)",
                ["Tier5_BlockMaster_RequiredPoints"] = "Tier 5-4: [Block Master] Required Points",

                // === Tier 6-1: Mind Shield (1) ===
                ["Tier6_MindShield_RequiredPoints"] = "Tier 6-1: [Mind Shield] Required Points",

                // === Tier 6-2: Nerve Enhancement (2) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6-2: [Nerve Enhancement] Permanent Dodge Bonus (%)",
                ["Tier6_NerveEnhancement_RequiredPoints"] = "Tier 6-2: [Nerve Enhancement] Required Points",

                // === Tier 6-3: Double Jump (1) ===
                ["Tier6_DoubleJump_RequiredPoints"] = "Tier 6-3: [Double Jump] Required Points",

                // === Tier 6-4: Jotunn's Vitality (3) ===
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6-4: [Jotunn's Vitality] HP Bonus (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6-4: [Jotunn's Vitality] Physical/Elemental Resistance (%)",
                ["Tier6_JotunnVitality_RequiredPoints"] = "Tier 6-4: [Jotunn's Vitality] Required Points",

                // === Tier 6-5: Jotunn's Shield (4) ===
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6-5: [Jotunn's Shield] Block Stamina Reduction (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6-5: [Jotunn's Shield] Normal Shield Move Speed Bonus (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6-5: [Jotunn's Shield] Tower Shield Move Speed Bonus (%)",
                ["Tier6_JotunnShield_RequiredPoints"] = "Tier 6-5: [Jotunn's Shield] Required Points",

                // ============================================
                // Production Tree - 22 Keys
                // ============================================


                // === Tier 0: Production Expert (1) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] = "Tier 0: Wood +1 Bonus Chance (%)",

                // === Tier 1: Novice Worker (1) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] = "Tier 1: Wood +1 Bonus Chance (%)",

                // === Tier 2: Specialization (5) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] = "Tier 2: Woodcutting Lv2 - Wood +1 Bonus Chance (%)",
                ["Tier2_GatheringLv2_BonusChance"] = "Tier 2: Gathering Lv2 - Item +1 Bonus Chance (%)",
                ["Tier2_MiningLv2_BonusChance"] = "Tier 2: Mining Lv2 - Ore +1 Bonus Chance (%)",
                ["Tier2_CraftingLv2_UpgradeChance"] = "Tier 2: Crafting Lv2 - Upgrade +1 Bonus Chance (%)",
                ["Tier2_CraftingLv2_DurabilityBonus"] = "Tier 2: Crafting Lv2 - Max Durability Increase (%)",

                // === Tier 3: Intermediate Skills (5) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] = "Tier 3: Woodcutting Lv3 - Wood +2 Bonus Chance (%)",
                ["Tier3_GatheringLv3_BonusChance"] = "Tier 3: Gathering Lv3 - Item +1 Bonus Chance (%)",
                ["Tier3_MiningLv3_BonusChance"] = "Tier 3: Mining Lv3 - Ore +1 Bonus Chance (%)",
                ["Tier3_CraftingLv3_UpgradeChance"] = "Tier 3: Crafting Lv3 - Upgrade +1 Bonus Chance (%)",
                ["Tier3_CraftingLv3_DurabilityBonus"] = "Tier 3: Crafting Lv3 - Max Durability Increase (%)",

                // === Tier 4: Advanced Skills (5) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] = "Tier 4: Woodcutting Lv4 - Wood +2 Bonus Chance (%)",
                ["Tier4_GatheringLv4_BonusChance"] = "Tier 4: Gathering Lv4 - Item +1 Bonus Chance (%)",
                ["Tier4_MiningLv4_BonusChance"] = "Tier 4: Mining Lv4 - Ore +1 Bonus Chance (%)",
                ["Tier4_CraftingLv4_UpgradeChance"] = "Tier 4: Crafting Lv4 - Upgrade +1 Bonus Chance (%)",
                ["Tier4_CraftingLv4_DurabilityBonus"] = "Tier 4: Crafting Lv4 - Max Durability Increase (%)",

                // === Production Tree: RequiredPoints (14) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: [Production Expert] Required Points",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: [Novice Worker] Required Points",
                ["Tier2_WoodcuttingLv2_RequiredPoints"] = "Tier 2: [Woodcutting Lv2] Required Points",
                ["Tier2_GatheringLv2_RequiredPoints"] = "Tier 2: [Gathering Lv2] Required Points",
                ["Tier2_MiningLv2_RequiredPoints"] = "Tier 2: [Mining Lv2] Required Points",
                ["Tier2_CraftingLv2_RequiredPoints"] = "Tier 2: [Crafting Lv2] Required Points",
                ["Tier3_WoodcuttingLv3_RequiredPoints"] = "Tier 3: [Woodcutting Lv3] Required Points",
                ["Tier3_GatheringLv3_RequiredPoints"] = "Tier 3: [Gathering Lv3] Required Points",
                ["Tier3_MiningLv3_RequiredPoints"] = "Tier 3: [Mining Lv3] Required Points",
                ["Tier3_CraftingLv3_RequiredPoints"] = "Tier 3: [Crafting Lv3] Required Points",
                ["Tier4_WoodcuttingLv4_RequiredPoints"] = "Tier 4: [Woodcutting Lv4] Required Points",
                ["Tier4_GatheringLv4_RequiredPoints"] = "Tier 4: [Gathering Lv4] Required Points",
                ["Tier4_MiningLv4_RequiredPoints"] = "Tier 4: [Mining Lv4] Required Points",
                ["Tier4_CraftingLv4_RequiredPoints"] = "Tier 4: [Crafting Lv4] Required Points",

                // ============================================
                // Bow Tree - 34 Keys
                // ============================================


                // === Tier 0: Bow Expert (2) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: [Bow Expert] Bow Damage Bonus (%)",
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: [Bow Expert] Required Points",

                // === Tier 1-1: [Focused Shot] (2) ===
                ["Tier1_FocusedShot_CritBonus"] = "Tier 1-1: [Focused Shot] Crit Chance Bonus (%)",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1-1: [Focused Shot] Required Points",

                // === Tier 1-2: [Multi-Shot Lv1] (5) ===
                ["Tier1_MultishotLv1_ActivationChance"] = "Tier 1-2: [Multi-Shot Lv1] Activation Chance (%)",
                ["Tier1_MultishotLv1_AdditionalArrows"] = "Tier 1-2: [Multi-Shot Lv1] Additional Arrows",
                ["Tier1_MultishotLv1_ArrowConsumption"] = "Tier 1-2: [Multi-Shot Lv1] Arrow Consumption",
                ["Tier1_MultishotLv1_DamagePerArrow"] = "Tier 1-2: [Multi-Shot Lv1] Damage Per Arrow (%)",
                ["Tier1_MultishotLv1_RequiredPoints"] = "Tier 1-2: [Multi-Shot Lv1] Required Points",

                // === Tier 2: [Bow Proficiency] (3) ===
                ["Tier2_BowMastery_SkillBonus"] = "Tier 2: [Bow Proficiency] Bow Skill Bonus",
                ["Tier2_BowMastery_SpecialArrowChance"] = "Tier 2: [Bow Proficiency] Special Arrow Chance (%)",
                ["Tier2_BowMastery_RequiredPoints"] = "Tier 2: [Bow Proficiency] Required Points",

                // === Tier 3-1: [Silent Shot] (2) ===
                ["Tier3_SilentStrike_DamageBonus"] = "Tier 3-1: [Silent Shot] Damage Increase",
                ["Tier3_SilentStrike_RequiredPoints"] = "Tier 3-1: [Silent Shot] Required Points",

                // === Tier 3-2: [Multi-Shot Lv2] (2) ===
                ["Tier3_MultishotLv2_ActivationChance"] = "Tier 3-2: [Multi-Shot Lv2] Activation Chance (%)",
                ["Tier3_MultishotLv2_RequiredPoints"] = "Tier 3-2: [Multi-Shot Lv2] Required Points",

                // === Tier 3-3: [Hunter's Instinct] (2) ===
                ["Tier3_HuntingInstinct_CritBonus"] = "Tier 3-3: [Hunter's Instinct] Crit Chance (%)",
                ["Tier3_HuntingInstinct_RequiredPoints"] = "Tier 3-3: [Hunter's Instinct] Required Points",

                // === Tier 4: [Precision Aim] (2) ===
                ["Tier4_PrecisionAim_CritDamage"] = "Tier 4: [Precision Aim] Crit Damage Bonus (%)",
                ["Tier4_PrecisionAim_RequiredPoints"] = "Tier 4: [Precision Aim] Required Points",

                // === Tier 5: [Explosive Arrow] (10) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] = "Tier 5: [Explosive Arrow] Damage Multiplier (%)",
                ["Tier5_ExplosiveArrow_Radius"] = "Tier 5: [Explosive Arrow] Explosion Radius (m)",
                ["Tier5_ExplosiveArrow_Cooldown"] = "Tier 5: [Explosive Arrow] Cooldown (sec)",
                ["Tier5_ExplosiveArrow_StaminaCost"] = "Tier 5: [Explosive Arrow] Stamina Cost (%)",
                ["Tier5_ExplosiveArrow_RequiredPoints"] = "Tier 5: [Explosive Arrow] Required Points",

                // ============================================
                // Sword Tree - 30 Keys
                // ============================================


                // === Sword Tree: Sword Expert (1개) ===
                ["Sword_Expert_DamageIncrease"] = "Tier 0: Sword Damage Increase (%)",

                // === Sword Tree: Fast Slash (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: Attack Speed Bonus (%)",

                // === Sword Tree: Combo Slash (2개) ===
                ["Sword_ComboSlash_Bonus"] = "Tier 2: Combo Attack Bonus (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: Buff Duration (sec)",

                // === Sword Tree: Blade Reflect (1개) ===
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: Damage Bonus",

                // === Sword Tree: True Duel (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: Attack Speed Bonus (%)",

                // === Sword Tree: Parry Charge (5개) ===
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: Buff Duration (sec)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: Charge Attack Bonus (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: Push Distance (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: Stamina Cost",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: Cooldown (sec)",

                // === Sword Tree: Rush Slash (8개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: 1st Hit Damage Ratio (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: 2nd Hit Damage Ratio (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: 3rd Hit Damage Ratio (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: Initial Dash Distance (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: Side Movement Distance (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: Stamina Cost",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: Cooldown (sec)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: Movement Speed (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: Attack Speed Bonus (%)",

                // ============================================
                // Spear Tree - 35 Keys
                // ============================================

                // === Spear Tree: Spear Expert (4 keys) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: [Spear Expert] Required Points",
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: [Spear Expert] 2-Hit Attack Speed Bonus (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: [Spear Expert] 2-Hit Damage Bonus (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: [Spear Expert] Effect Duration (sec)",

                // === Spear Tree: Vital Strike (2 keys) ===
                ["Tier1_QuickStrike_RequiredPoints"] = "Tier 1: [Vital Strike] Required Points",
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: [Vital Strike] Critical Damage Bonus (%)",

                // === Spear Tree: Throw Spear (3 keys + Legacy 1) ===
                ["Tier2_Throw_RequiredPoints"] = "Tier 2: [Throw Spear] Required Points",
                ["Tier2_Throw_Cooldown"] = "Tier 2: [Throw Spear] Cooldown (sec)",
                ["Tier2_Throw_DamageMultiplier"] = "Tier 2: [Throw Spear] Damage Multiplier (%)",
                ["Legacy_Throw_BuffDuration"] = "Legacy: Not Used",

                // === Spear Tree: Rapid Pierce (2 keys) ===
                ["Tier3_Pierce_RequiredPoints"] = "Tier 3-1: [Rapid Pierce] Required Points",
                ["Tier3_Rapid_DamageBonus"] = "Tier 3-1: [Rapid Pierce] Weapon Damage Bonus",

                // === Spear Tree: Explosive Spear (3 keys) ===
                ["Tier3_Explosion_Chance"] = "Tier 3-2: [Explosive Spear] Trigger Chance (%)",
                ["Tier3_Explosion_Radius"] = "Tier 3-2: [Explosive Spear] Explosion Radius (m)",
                ["Tier3_Explosion_DamageBonus"] = "Tier 3-2: [Explosive Spear] Damage Bonus (%)",

                // === Spear Tree: Evasion Strike (3 keys) ===
                ["Tier4_Evasion_RequiredPoints"] = "Tier 4-1: [Evasion Strike] Required Points",
                ["Tier4_Evasion_DamageBonus"] = "Tier 4-1: [Evasion Strike] Post-Dodge Damage Bonus (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4-1: [Evasion Strike] Stamina Cost Reduction (%)",

                // === Spear Tree: Dual Strike (2 keys) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4-2: [Dual Strike] 2-Hit Damage Bonus (%)",
                ["Tier4_Dual_Duration"] = "Tier 4-2: [Dual Strike] Buff Duration (sec)",

                // === Spear Tree: Penetrating Spear (6 keys + Legacy 1) ===
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5-1: [Penetrating Spear] Required Points",
                ["Legacy_Penetrate_CritChance"] = "Legacy: Not Used",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5-1: [Penetrating Spear] Buff Duration (sec)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5-1: [Penetrating Spear] Lightning Damage Multiplier (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5-1: [Penetrating Spear] Lightning Trigger Hit Count",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5-1: [Penetrating Spear] G-Key Cooldown (sec)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5-1: [Penetrating Spear] G-Key Stamina Cost",

                // === Spear Tree: Combo Spear (8 keys) ===
                ["Tier5_Combo_RequiredPoints"] = "Tier 5-2: [Combo Spear] Required Points",
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5-2: [Combo Spear] H-Key Cooldown (sec)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5-2: [Combo Spear] H-Key Damage Multiplier (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5-2: [Combo Spear] H-Key Stamina Cost",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5-2: [Combo Spear] H-Key Knockback Range (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5-2: [Combo Spear] Active Effect Range (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5-2: [Combo Spear] Buff Duration (sec)",
                ["Tier5_Combo_MaxUses"] = "Tier 5-2: [Combo Spear] Max Enhanced Throw Count",

                // ============================================
                // Staff Tree - 30 Keys
                // ============================================


                // === Tier 0: Staff Expert (2) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] = "Tier 0: [Staff Expert] Elemental Damage Bonus (%)",
                ["Tier0_StaffExpert_RequiredPoints"] = "Tier 0: [Staff Expert] Required Points",

                // === Tier 1-1: Mental Focus (2) ===
                ["Tier1_MindFocus_EitrReduction"] = "Tier 1-1: [Mental Focus] Eitr Cost Reduction (%)",
                ["Tier1_MindFocus_RequiredPoints"] = "Tier 1-1: [Mental Focus] Required Points",

                // === Tier 1-2: Magic Flow (2) ===
                ["Tier1_MagicFlow_EitrBonus"] = "Tier 1-2: [Magic Flow] Max Eitr Bonus",
                ["Tier1_MagicFlow_RequiredPoints"] = "Tier 1-2: [Magic Flow] Required Points",

                // === Tier 2: Magic Amplification (4) ===
                ["Tier2_MagicAmplify_Chance"] = "Tier 2: [Magic Amplification] Trigger Chance (%)",
                ["Tier2_MagicAmplify_DamageBonus"] = "Tier 2: [Magic Amplification] Elemental Damage Bonus (%)",
                ["Tier2_MagicAmplify_EitrCostIncrease"] = "Tier 2: [Magic Amplification] Eitr Cost Increase (%)",
                ["Tier2_MagicAmplify_RequiredPoints"] = "Tier 2: [Magic Amplification] Required Points",

                // === Tier 3-1: Frost Element (2) ===
                ["Tier3_FrostElement_DamageBonus"] = "Tier 3-1: [Frost Element] Damage Bonus",
                ["Tier3_FrostElement_RequiredPoints"] = "Tier 3-1: [Frost Element] Required Points",

                // === Tier 3-2: Fire Element (2) ===
                ["Tier3_FireElement_DamageBonus"] = "Tier 3-2: [Fire Element] Damage Bonus",
                ["Tier3_FireElement_RequiredPoints"] = "Tier 3-2: [Fire Element] Required Points",

                // === Tier 3-3: Lightning Element (2) ===
                ["Tier3_LightningElement_DamageBonus"] = "Tier 3-3: [Lightning Element] Damage Bonus",
                ["Tier3_LightningElement_RequiredPoints"] = "Tier 3-3: [Lightning Element] Required Points",

                // === Tier 4: Lucky Mana (2) ===
                ["Tier4_LuckyMana_Chance"] = "Tier 4: [Lucky Mana] Free Cast Trigger Chance (%)",
                ["Tier4_LuckyMana_RequiredPoints"] = "Tier 4: [Lucky Mana] Required Points",

                // === Tier 5-1: Dual Cast - R-Key Active (6) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] = "Tier 5-1: [Dual Cast] Additional Projectile Count",
                ["Tier5_DoubleCast_ProjectileDamagePercent"] = "Tier 5-1: [Dual Cast] Projectile Damage Percent (%)",
                ["Tier5_DoubleCast_AngleOffset"] = "Tier 5-1: [Dual Cast] Projectile Angle Offset (deg)",
                ["Tier5_DoubleCast_EitrCost"] = "Tier 5-1: [Dual Cast] Eitr Cost",
                ["Tier5_DoubleCast_Cooldown"] = "Tier 5-1: [Dual Cast] Cooldown (sec)",
                ["Tier5_DoubleCast_RequiredPoints"] = "Tier 5-1: [Dual Cast] Required Points",

                // === Tier 5-2: Instant Area Heal - H-Key Active (6) ===
                ["Tier5_InstantAreaHeal_Cooldown"] = "Tier 5-2: [Heal] Cooldown (sec)",
                ["Tier5_InstantAreaHeal_EitrCost"] = "Tier 5-2: [Heal] Eitr Cost",
                ["Tier5_InstantAreaHeal_HealPercent"] = "Tier 5-2: [Heal] Heal Amount (% of Max HP)",
                ["Tier5_InstantAreaHeal_Range"] = "Tier 5-2: [Heal] Heal Range (m)",
                ["Tier5_InstantAreaHeal_SelfHeal"] = "Tier 5-2: [Heal] Allow Self Heal",
                ["Tier5_InstantAreaHeal_RequiredPoints"] = "Tier 5-2: [Heal] Required Points",

                // ============================================
                // Crossbow Tree - 34 Keys
                // ============================================


                // === Tier 0: Crossbow Expert (2) ===
                ["Tier0_CrossbowExpert_DamageBonus"] = "Tier 0: [Crossbow Expert] Crossbow Damage Bonus (%)",
                ["Tier0_CrossbowExpert_RequiredPoints"] = "Tier 0: [Crossbow Expert] Required Points",

                // === Tier 1: Rapid Fire (6) ===
                ["Tier1_RapidFire_Chance"] = "Tier 1: [Rapid Fire] Trigger Chance (%)",
                ["Tier1_RapidFire_ShotCount"] = "Tier 1: [Rapid Fire] Shot Count",
                ["Tier1_RapidFire_DamagePercent"] = "Tier 1: [Rapid Fire] Damage Percent (%)",
                ["Tier1_RapidFire_Delay"] = "Tier 1: [Rapid Fire] Shot Delay (sec)",
                ["Tier1_RapidFire_BoltConsumption"] = "Tier 1: [Rapid Fire] Bolt Consumption",
                ["Tier1_RapidFire_RequiredPoints"] = "Tier 1: [Rapid Fire] Required Points",

                // === Tier 2-1: Balanced Aim / 2-2: Quick Reload / 2-3: True Shot (8) ===
                ["Tier2_BalancedAim_KnockbackChance"] = "Tier 2-1: [Balanced Aim] Knockback Trigger Chance (%)",
                ["Tier2_BalancedAim_KnockbackDistance"] = "Tier 2-1: [Balanced Aim] Knockback Distance (m)",
                ["Tier2_BalancedAim_RequiredPoints"] = "Tier 2-1: [Balanced Aim] Required Points",
                ["Tier2_RapidReload_SpeedIncrease"] = "Tier 2-2: [Quick Reload] Reload Speed Increase (%)",
                ["Tier2_RapidReload_RequiredPoints"] = "Tier 2-2: [Quick Reload] Required Points",
                ["Tier2_HonestShot_DamageBonus"] = "Tier 2-3: [True Shot] Damage Bonus (%)",
                ["Tier2_HonestShot_RequiredPoints"] = "Tier 2-3: [True Shot] Required Points",

                // === Tier 3: Auto Reload (2) ===
                ["Tier3_AutoReload_Chance"] = "Tier 3: [Auto Reload] Trigger Chance (%)",
                ["Tier3_AutoReload_RequiredPoints"] = "Tier 3: [Auto Reload] Required Points",

                // === Tier 4-1: Rapid Fire Lv2 / 4-2: Final Strike (9) ===
                ["Tier4_RapidFireLv2_Chance"] = "Tier 4-1: [Rapid Fire Lv2] Trigger Chance (%)",
                ["Tier4_RapidFireLv2_ShotCount"] = "Tier 4-1: [Rapid Fire Lv2] Shot Count",
                ["Tier4_RapidFireLv2_DamagePercent"] = "Tier 4-1: [Rapid Fire Lv2] Damage Percent (%)",
                ["Tier4_RapidFireLv2_Delay"] = "Tier 4-1: [Rapid Fire Lv2] Shot Delay (sec)",
                ["Tier4_RapidFireLv2_BoltConsumption"] = "Tier 4-1: [Rapid Fire Lv2] Bolt Consumption",
                ["Tier4_RapidFireLv2_RequiredPoints"] = "Tier 4-1: [Rapid Fire Lv2] Required Points",
                ["Tier4_FinalStrike_HpThreshold"] = "Tier 4-2: [Final Strike] Enemy HP Threshold (%)",
                ["Tier4_FinalStrike_DamageBonus"] = "Tier 4-2: [Final Strike] Bonus Damage (%)",
                ["Tier4_FinalStrike_RequiredPoints"] = "Tier 4-2: [Final Strike] Required Points",

                // === Tier 5: One Shot - R-Key Active (5) ===
                ["Tier5_OneShot_Duration"] = "Tier 5: [One Shot] Buff Duration (sec)",
                ["Tier5_OneShot_DamageBonus"] = "Tier 5: [One Shot] Damage Bonus (%)",
                ["Tier5_OneShot_KnockbackDistance"] = "Tier 5: [One Shot] Knockback Distance (m)",
                ["Tier5_OneShot_Cooldown"] = "Tier 5: [One Shot] Cooldown (sec)",
                ["Tier5_OneShot_RequiredPoints"] = "Tier 5: [One Shot] Required Points",

                // ============================================
                // Knife Tree - 32 Keys
                // ============================================

                // === Tier 0: Knife Expert (2) ===
                ["Tier0_KnifeExpert_BackstabBonus"] = "Tier 0: [Knife Expert] Backstab Bonus (%)",
                ["Tier0_KnifeExpert_RequiredPoints"] = "Tier 0: [Knife Expert] Required Points",

                // === Tier 1: Evasion Mastery (3) ===
                ["Tier1_Evasion_Chance"] = "Tier 1: [Evasion Mastery] Trigger Chance (%)",
                ["Tier1_Evasion_Duration"] = "Tier 1: [Evasion Mastery] Duration (sec)",
                ["Tier1_Evasion_RequiredPoints"] = "Tier 1: [Evasion Mastery] Required Points",

                // === Tier 2: Quick Movement (2) ===
                ["Tier2_FastMove_MoveSpeedBonus"] = "Tier 2: [Quick Movement] Move Speed Bonus (%)",
                ["Tier2_FastMove_RequiredPoints"] = "Tier 2: [Quick Movement] Required Points",

                // === Tier 3: Quick Attack (3) ===
                ["Tier3_CombatMastery_DamageBonus"] = "Tier 3: [Quick Attack] Damage Bonus",
                ["Tier3_CombatMastery_BuffDuration"] = "Tier 3: [Quick Attack] Buff Duration (sec)",
                ["Tier3_CombatMastery_RequiredPoints"] = "Tier 3: [Quick Attack] Required Points",

                // === Tier 4: Critical Mastery (4) ===
                ["Tier4_AttackEvasion_EvasionBonus"] = "Tier 4: [Critical Mastery] Evasion Bonus (%)",
                ["Tier4_AttackEvasion_BuffDuration"] = "Tier 4: [Critical Mastery] Buff Duration (sec)",
                ["Tier4_AttackEvasion_Cooldown"] = "Tier 4: [Critical Mastery] Cooldown (sec)",
                ["Tier4_AttackEvasion_RequiredPoints"] = "Tier 4: [Critical Mastery] Required Points",

                // === Tier 5: Lethal Damage (2) ===
                ["Tier5_CriticalDamage_DamageBonus"] = "Tier 5: [Lethal Damage] Damage Bonus (%)",
                ["Tier5_CriticalDamage_RequiredPoints"] = "Tier 5: [Lethal Damage] Required Points",

                // === Tier 6: Assassin (3) ===
                ["Tier6_Assassin_CritDamageBonus"] = "Tier 6: [Assassin] Crit Damage Bonus (%)",
                ["Tier6_Assassin_CritChanceBonus"] = "Tier 6: [Assassin] Crit Chance Bonus (%)",
                ["Tier6_Assassin_RequiredPoints"] = "Tier 6: [Assassin] Required Points",

                // === Tier 7: Assassination (3) ===
                ["Tier7_Assassination_StaggerChance"] = "Tier 7: [Assassination] Stagger Trigger Chance (%)",
                ["Tier7_Assassination_RequiredComboHits"] = "Tier 7: [Assassination] Required Combo Hits",
                ["Tier7_Assassination_RequiredPoints"] = "Tier 7: [Assassination] Required Points",

                // === Tier 8: Assassin's Heart - G-Key Active (10) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] = "Tier 8: [Assassin's Heart] Crit Damage Multiplier",
                ["Tier8_AssassinHeart_Duration"] = "Tier 8: [Assassin's Heart] Buff Duration (sec)",
                ["Tier8_AssassinHeart_StaminaCost"] = "Tier 8: [Assassin's Heart] Stamina Cost",
                ["Tier8_AssassinHeart_Cooldown"] = "Tier 8: [Assassin's Heart] Cooldown (sec)",
                ["Tier8_AssassinHeart_TeleportRange"] = "Tier 8: [Assassin's Heart] Teleport Range (m)",
                ["Tier8_AssassinHeart_TeleportBackDistance"] = "Tier 8: [Assassin's Heart] Behind Target Distance (m)",
                ["Tier8_AssassinHeart_StunDuration"] = "Tier 8: [Assassin's Heart] Stun Duration (sec)",
                ["Tier8_AssassinHeart_ComboAttackCount"] = "Tier 8: [Assassin's Heart] Combo Attack Count",
                ["Tier8_AssassinHeart_AttackInterval"] = "Tier 8: [Assassin's Heart] Attack Interval (sec)",
                ["Tier8_AssassinHeart_RequiredPoints"] = "Tier 8: [Assassin's Heart] Required Points",

                // ============================================
                // Sword Tree - 33 Keys (new key format)
                // ============================================

                // === Tier 0: Sword Expert (2) ===
                ["Tier0_SwordExpert_DamageBonus"] = "Tier 0: [Sword Expert] Sword Damage Bonus (%)",
                ["Tier0_SwordExpert_RequiredPoints"] = "Tier 0: [Sword Expert] Required Points",

                // === Tier 1-1: Fast Slash (2) ===
                ["Tier1_FastSlash_RequiredPoints"] = "Tier 1-1: [Fast Slash] Required Points",
                ["Tier1_FastSlash_AttackSpeedBonus"] = "Tier 1-1: [Fast Slash] Attack Speed Bonus (%)",

                // === Tier 1-2: Counter Stance (3) ===
                ["Tier1_CounterStance_RequiredPoints"] = "Tier 1-2: [Counter Stance] Required Points",
                ["Tier1_CounterStance_Duration"] = "Tier 1-2: [Counter Stance] Buff Duration (sec)",
                ["Tier1_CounterStance_DefenseBonus"] = "Tier 1-2: [Counter Stance] Defense Bonus (%)",

                // === Tier 2: Combo Slash (3) ===
                ["Tier2_ComboSlash_RequiredPoints"] = "Tier 2: [Combo Slash] Required Points",
                ["Tier2_ComboSlash_DamageBonus"] = "Tier 2: [Combo Slash] Damage Bonus (%)",
                ["Tier2_ComboSlash_BuffDuration"] = "Tier 2: [Combo Slash] Buff Duration (sec)",

                // === Tier 3: Riposte (2) ===
                ["Tier3_Riposte_RequiredPoints"] = "Tier 3: [Riposte] Required Points",
                ["Tier3_Riposte_DamageBonus"] = "Tier 3: [Riposte] Damage Bonus",

                // === Tier 4-1: All-In-One (3) ===
                ["Tier4_AllInOne_RequiredPoints"] = "Tier 4-1: [All-In-One] Required Points",
                ["Tier4_AllInOne_AttackBonus"] = "Tier 4-1: [All-In-One] Attack Bonus (%)",
                ["Tier4_AllInOne_DefenseBonus"] = "Tier 4-1: [All-In-One] Defense Bonus",

                // === Tier 4-2: True Duel (2) ===
                ["Tier4_TrueDuel_RequiredPoints"] = "Tier 4-2: [True Duel] Required Points",
                ["Tier4_TrueDuel_AttackSpeedBonus"] = "Tier 4-2: [True Duel] Attack Speed Bonus (%)",

                // === Tier 5: Parry Rush - G-Key Active (6) ===
                ["Tier5_ParryRush_RequiredPoints"] = "Tier 5: [Parry Rush] Required Points",
                ["Tier5_ParryRush_BuffDuration"] = "Tier 5: [Parry Rush] Buff Duration (sec)",
                ["Tier5_ParryRush_DamageBonus"] = "Tier 5: [Parry Rush] Damage Bonus (%)",
                ["Tier5_ParryRush_PushDistance"] = "Tier 5: [Parry Rush] Push Distance (m)",
                ["Tier5_ParryRush_StaminaCost"] = "Tier 5: [Parry Rush] Stamina Cost",
                ["Tier5_ParryRush_Cooldown"] = "Tier 5: [Parry Rush] Cooldown (sec)",

                // === Tier 6: Rush Slash - G-Key Active (10) ===
                ["Tier6_RushSlash_RequiredPoints"] = "Tier 6: [Rush Slash] Required Points",
                ["Tier6_RushSlash_Hit1DamageRatio"] = "Tier 6: [Rush Slash] 1st Hit Damage Ratio (%)",
                ["Tier6_RushSlash_Hit2DamageRatio"] = "Tier 6: [Rush Slash] 2nd Hit Damage Ratio (%)",
                ["Tier6_RushSlash_Hit3DamageRatio"] = "Tier 6: [Rush Slash] 3rd Hit Damage Ratio (%)",
                ["Tier6_RushSlash_InitialDistance"] = "Tier 6: [Rush Slash] Initial Dash Distance (m)",
                ["Tier6_RushSlash_SideDistance"] = "Tier 6: [Rush Slash] Side Move Distance (m)",
                ["Tier6_RushSlash_StaminaCost"] = "Tier 6: [Rush Slash] Stamina Cost",
                ["Tier6_RushSlash_Cooldown"] = "Tier 6: [Rush Slash] Cooldown (sec)",
                ["Tier6_RushSlash_MoveSpeed"] = "Tier 6: [Rush Slash] Move Speed (m/s)",
                ["Tier6_RushSlash_AttackSpeedBonus"] = "Tier 6: [Rush Slash] Attack Speed Bonus (%)",

                // ============================================
                // Mace Tree - 34 Keys
                // ============================================


                // === Tier 0: Mace Expert (4) ===
                ["Tier0_MaceExpert_DamageBonus"] = "Tier 0: [Mace Expert] Damage Bonus (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: [Mace Expert] Stun Chance (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: [Mace Expert] Stun Duration (sec)",
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: [Mace Expert] Required Points",

                // === Tier 1: Mace Damage Boost (2) ===
                ["Tier1_MaceExpert_DamageBonus"] = "Tier 1: [Mace Damage Boost] Damage Bonus (%)",
                ["Tier1_MaceExpert_RequiredPoints"] = "Tier 1: [Mace Damage Boost] Required Points",

                // === Tier 2: Stun Boost (3) ===
                ["Tier2_StunBoost_StunChanceBonus"] = "Tier 2: [Stun Boost] Stun Chance Bonus (%)",
                ["Tier2_StunBoost_StunDurationBonus"] = "Tier 2: [Stun Boost] Stun Duration Bonus (sec)",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: [Stun Boost] Required Points",

                // === Tier 3-1: Guard (2) ===
                ["Tier3_Guard_ArmorBonus"] = "Tier 3-1: [Guard] Armor Bonus",
                ["Tier3_Guard_RequiredPoints"] = "Tier 3-1: [Guard] Required Points",

                // === Tier 3-2: Heavy Strike (2) ===
                ["Tier3_HeavyStrike_DamageBonus"] = "Tier 3-2: [Heavy Strike] Damage Bonus",
                ["Tier3_HeavyStrike_RequiredPoints"] = "Tier 3-2: [Heavy Strike] Required Points",

                // === Tier 4: Push (2) ===
                ["Tier4_Push_KnockbackChance"] = "Tier 4: [Push] Knockback Chance (%)",
                ["Tier4_Push_RequiredPoints"] = "Tier 4: [Push] Required Points",

                // === Tier 5-1: Tank (3) ===
                ["Tier5_Tank_HealthBonus"] = "Tier 5-1: [Tank] Health Bonus (%)",
                ["Tier5_Tank_DamageReduction"] = "Tier 5-1: [Tank] Incoming Damage Reduction (%)",
                ["Tier5_Tank_RequiredPoints"] = "Tier 5-1: [Tank] Required Points",

                // === Tier 5-2: DPS Boost (3) ===
                ["Tier5_DPS_DamageBonus"] = "Tier 5-2: [DPS Boost] Damage Bonus (%)",
                ["Tier5_DPS_AttackSpeedBonus"] = "Tier 5-2: [DPS Boost] Attack Speed Bonus (%)",
                ["Tier5_DPS_RequiredPoints"] = "Tier 5-2: [DPS Boost] Required Points",

                // === Tier 6: Grandmaster (2) ===
                ["Tier6_Grandmaster_ArmorBonus"] = "Tier 6: [Grandmaster] Armor Bonus (%)",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: [Grandmaster] Required Points",

                // === Tier 7-1: Fury Hammer - H-Key Active (6) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] = "Tier 7-1: [Fury Hammer] Hits 1-4 Damage Multiplier (%)",
                ["Tier7_FuryHammer_FinalHitMultiplier"] = "Tier 7-1: [Fury Hammer] Hit 5 Final Damage Multiplier (%)",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7-1: [Fury Hammer] Stamina Cost",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7-1: [Fury Hammer] Cooldown (sec)",
                ["Tier7_FuryHammer_AoeRadius"] = "Tier 7-1: [Fury Hammer] AOE Radius (m)",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7-1: [Fury Hammer] Required Points",

                // === Tier 7-2: Guardian Heart - G-Key Active (5) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7-2: [Guardian Heart] Cooldown (sec)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7-2: [Guardian Heart] Stamina Cost",
                ["Tier7_GuardianHeart_Duration"] = "Tier 7-2: [Guardian Heart] Buff Duration (sec)",
                ["Tier7_GuardianHeart_ReflectPercent"] = "Tier 7-2: [Guardian Heart] Reflect Damage Percent (%)",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7-2: [Guardian Heart] Required Points",

                // ========================================
                // Polearm Tree (37 keys)
                // ========================================

                // === Tier 0 - Polearm Expert (2) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] = "Tier 0: [Polearm Expert] Attack Range Bonus (%)",
                ["Tier0_PolearmExpert_RequiredPoints"] = "Tier 0: [Polearm Expert] Required Points",

                // === Tier 1 - Spin Slash (2) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] = "Tier 1: [Spin Slash] Wheel Attack Damage Bonus (%)",
                ["Tier1_SpinWheel_RequiredPoints"] = "Tier 1: [Spin Slash] Required Points",

                // === Tier 2-1 - Polearm Enhancement (2) ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] = "Tier 2-1: [Polearm Enhancement] Weapon Damage Bonus (flat)",
                ["Tier2-1_PolearmBoost_RequiredPoints"] = "Tier 2-1: [Polearm Enhancement] Required Points",

                // === Tier 2-2 - Hero Strike (2) ===
                ["Tier2-2_HeroStrike_KnockbackChance"] = "Tier 2-2: [Hero Strike] Knockback Chance (%)",
                ["Tier2-2_HeroStrike_RequiredPoints"] = "Tier 2-2: [Hero Strike] Required Points",

                // === Tier 3 - Wide Slash (3) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] = "Tier 3: [Wide Slash] Double Hit Damage Bonus (%)",
                ["Tier3_AreaCombo_DoubleHitDuration"] = "Tier 3: [Wide Slash] Double Hit Buff Duration (sec)",
                ["Tier3_AreaCombo_RequiredPoints"] = "Tier 3: [Wide Slash] Required Points",

                // === Tier 4-1 - Ground Pound (2) ===
                ["Tier4-1_GroundWheel_WheelAttackDamageBonus"] = "Tier 4-1: [Ground Pound] Wheel Attack Damage Bonus (%)",
                ["Tier4-1_GroundWheel_RequiredPoints"] = "Tier 4-1: [Ground Pound] Required Points",

                // === Tier 4-2 - Crescent Slash (3) ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] = "Tier 4-2: [Crescent Slash] Attack Range Bonus (%)",
                ["Tier4-2_MoonSlash_StaminaReduction"] = "Tier 4-2: [Crescent Slash] Stamina Reduction (%)",
                ["Tier4-2_MoonSlash_RequiredPoints"] = "Tier 4-2: [Crescent Slash] Required Points",

                // === Tier 4-3 - Suppress Attack (2) ===
                ["Tier4-3_Suppress_DamageBonus"] = "Tier 4-3: [Suppress Attack] Damage Bonus (%)",
                ["Tier4-3_Suppress_RequiredPoints"] = "Tier 4-3: [Suppress Attack] Required Points",

                // === Tier 5 - Pierce Charge (9) ===
                ["Tier5_PierceCharge_DashDistance"] = "Tier 5: [Pierce Charge] Dash Distance (m)",
                ["Tier5_PierceCharge_FirstHitDamageBonus"] = "Tier 5: [Pierce Charge] First Hit Damage Bonus (%)",
                ["Tier5_PierceCharge_AoeDamageBonus"] = "Tier 5: [Pierce Charge] AOE Knockback Damage Bonus (%)",
                ["Tier5_PierceCharge_AoeAngle"] = "Tier 5: [Pierce Charge] AOE Angle (degrees)",
                ["Tier5_PierceCharge_AoeRadius"] = "Tier 5: [Pierce Charge] AOE Radius (m)",
                ["Tier5_PierceCharge_KnockbackDistance"] = "Tier 5: [Pierce Charge] Knockback Distance (m)",
                ["Tier5_PierceCharge_StaminaCost"] = "Tier 5: [Pierce Charge] Stamina Cost",
                ["Tier5_PierceCharge_Cooldown"] = "Tier 5: [Pierce Charge] Cooldown (sec)",
                ["Tier5_PierceCharge_RequiredPoints"] = "Tier 5: [Pierce Charge] Required Points",

                // ============================================
                // Archer Job Skills - 8 Keys
                // ============================================
                ["Archer_MultiShot_ArrowCount"] = "Multishot: Arrow Count",
                ["Archer_MultiShot_ArrowConsumption"] = "Multishot: Arrow Consumption",
                ["Archer_MultiShot_DamagePercent"] = "Multishot: Damage Per Arrow (%)",
                ["Archer_MultiShot_Cooldown"] = "Multishot: Cooldown (sec)",
                ["Archer_MultiShot_Charges"] = "Multishot: Shot Charges",
                ["Archer_MultiShot_StaminaCost"] = "Multishot: Stamina Cost",
                ["Archer_JumpHeightBonus"] = "Passive: Jump Height Bonus (%)",
                ["Archer_FallDamageReduction"] = "Passive: Fall Damage Reduction (%)",

                // ============================================
                // Mage Job Skills - 6 Keys
                // ============================================
                ["Mage_AOE_Range"] = "Active: Range (m)",
                ["Mage_Eitr_Cost"] = "Active: Eitr Cost",
                ["Mage_Damage_Multiplier"] = "Active: Damage Multiplier (%)",
                ["Mage_Cooldown"] = "Active: Cooldown (sec)",
                ["Mage_VFX_Name"] = "Active: VFX Effect Name",
                ["Mage_Elemental_Resistance"] = "Passive: Elemental Resistance (%)",

                // ============================================
                // Tanker Job Skills - 10 Keys
                // ============================================
                ["Tanker_Taunt_Cooldown"] = "War Cry: Cooldown (sec)",
                ["Tanker_Taunt_StaminaCost"] = "War Cry: Stamina Cost",
                ["Tanker_Taunt_Range"] = "War Cry: Taunt Range (m)",
                ["Tanker_Taunt_Duration"] = "War Cry: Taunt Duration (sec)",
                ["Tanker_Taunt_BossDuration"] = "War Cry: Boss Taunt Duration (sec)",
                ["Tanker_Taunt_DamageReduction"] = "War Cry: Damage Reduction (%)",
                ["Tanker_Taunt_BuffDuration"] = "War Cry: Buff Duration (sec)",
                ["Tanker_Taunt_EffectHeight"] = "War Cry: Effect Height (m)",
                ["Tanker_Taunt_EffectScale"] = "War Cry: Effect Scale",
                ["Tanker_Passive_DamageReduction"] = "Passive: Damage Reduction (%)",

                // ============================================
                // Rogue Job Skills - 10 Keys
                // ============================================
                ["Rogue_ShadowStrike_Cooldown"] = "Shadow Strike: Cooldown (sec)",
                ["Rogue_ShadowStrike_StaminaCost"] = "Shadow Strike: Stamina Cost",
                ["Rogue_ShadowStrike_AttackBonus"] = "Shadow Strike: Attack Bonus (%)",
                ["Rogue_ShadowStrike_BuffDuration"] = "Shadow Strike: Buff Duration (sec)",
                ["Rogue_ShadowStrike_SmokeScale"] = "Shadow Strike: Smoke Scale",
                ["Rogue_ShadowStrike_AggroRange"] = "Shadow Strike: Aggro Range (m)",
                ["Rogue_ShadowStrike_StealthDuration"] = "Shadow Strike: Stealth Duration (sec)",
                ["Rogue_AttackSpeed_Bonus"] = "Passive: Attack Speed Bonus (%)",
                ["Rogue_Stamina_Reduction"] = "Passive: Stamina Usage Reduction (%)",
                ["Rogue_ElementalResistance_Debuff"] = "Passive: Elemental Resistance Increase (%)",

                // ============================================
                // Paladin Job Skills - 9 Keys
                // ============================================
                ["Paladin_Active_Cooldown"] = "Holy Healing: Cooldown (sec)",
                ["Paladin_Active_Range"] = "Holy Healing: Range (m)",
                ["Paladin_Active_EitrCost"] = "Holy Healing: Eitr Cost",
                ["Paladin_Active_StaminaCost"] = "Holy Healing: Stamina Cost",
                ["Paladin_Active_SelfHealPercent"] = "Holy Healing: Self Heal Ratio (%)",
                ["Paladin_Active_AllyHealPercentOverTime"] = "Holy Healing: Ally HoT Ratio (%)",
                ["Paladin_Active_Duration"] = "Holy Healing: Duration (sec)",
                ["Paladin_Active_Interval"] = "Holy Healing: Interval (sec)",
                ["Paladin_Passive_ElementalResistanceReduction"] = "Passive: Resistance Bonus (%)",

                // ============================================
                // Berserker Job Skills - 10 Keys
                // ============================================
                ["Beserker_Active_Cooldown"] = "Berserker Rage: Cooldown (sec)",
                ["Beserker_Active_StaminaCost"] = "Berserker Rage: Stamina Cost",
                ["Beserker_Active_Duration"] = "Berserker Rage: Duration (sec)",
                ["Beserker_Active_DamagePerHealthPercent"] = "Berserker Rage: Damage Per HP% (%)",
                ["Beserker_Active_MaxDamageBonus"] = "Berserker Rage: Max Damage Bonus (%)",
                ["Beserker_Active_HealthThreshold"] = "Berserker Rage: HP Threshold (%)",
                ["Beserker_Passive_HealthThreshold"] = "Death Defiance: HP Threshold (%)",
                ["Beserker_Passive_InvincibilityDuration"] = "Death Defiance: Invincibility Duration (sec)",
                ["Beserker_Passive_Cooldown"] = "Death Defiance: Cooldown (sec)",
                ["Berserker_Passive_HealthBonus"] = "Passive: Max HP Bonus (%)",

                // ============================================
                // Sword Tree - Path Hit addition
                // ============================================
                ["Tier6_RushSlash_PathWidth"] = "Tier 6: [Rush Slash] Path Hit Width (m)",
            };
        }
    }
}

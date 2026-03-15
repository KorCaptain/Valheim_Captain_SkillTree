using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetGermanKeyNames()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetGermanKeyNames_Part1()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetGermanKeyNames_Part2()) dict[kv.Key] = kv.Value;
            return dict;
        }

        private static Dictionary<string, string> GetGermanKeyNames_Part1()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Skill_Tree_Base - Tastenbelegung
                // ============================================
                ["HotKey_Y"] = "Berufsfähigkeitstaste",
                ["HotKey_R"] = "Fernkampffähigkeitstaste",
                ["HotKey_G"] = "Nahkampf-Hauptfähigkeitstaste",
                ["HotKey_H"] = "Sekundäre Fähigkeitstaste",
                ["HUD_PosX"] = "HUD X-Position",
                ["HUD_PosY"] = "HUD Y-Position",
                ["PassiveMessageDisplay"] = "Passive Meldungsanzeige",

                // ============================================
                // Angriffs-Skilltree - 33 Schlüssel
                // ============================================

                // === Tier 0: Angriffs-Experte (2) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: [Angriffs-Experte] Gesamtschadensbonus (%)",
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: [Angriffs-Experte] Benötigte Punkte",

                // === Tier 1: Basisangriff (3) ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] = "Tier 1: [Basisangriff] Physischer Schadensbonus (%)",
                ["Tier1_BaseAttack_ElementalDamageBonus"] = "Tier 1: [Basisangriff] Elementarschadensbonus (%)",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: [Basisangriff] Benötigte Punkte",

                // === Tier 2: Waffenspezialisierung (12) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2-1: [Nahkampf-Spez.] Aktivierungschance (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2-1: [Nahkampf-Spez.] Schadensbonus",
                ["Tier2_MeleeSpec_RequiredPoints"] = "Tier 2-1: [Nahkampf-Spez.] Benötigte Punkte",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2-2: [Bogen-Spez.] Aktivierungschance (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2-2: [Bogen-Spez.] Schadensbonus",
                ["Tier2_BowSpec_RequiredPoints"] = "Tier 2-2: [Bogen-Spez.] Benötigte Punkte",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2-3: [Armbrust-Spez.] Aktivierungschance (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2-3: [Armbrust-Spez.] Schadensbonus",
                ["Tier2_CrossbowSpec_RequiredPoints"] = "Tier 2-3: [Armbrust-Spez.] Benötigte Punkte",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2-4: [Stab-Spez.] Aktivierungschance (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2-4: [Stab-Spez.] Schadensbonus",
                ["Tier2_StaffSpec_RequiredPoints"] = "Tier 2-4: [Stab-Spez.] Benötigte Punkte",

                // === Tier 3: Angriffsverstärkung (3) ===
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: [Angriffsverstärkung] Physischer Schadensbonus (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: [Angriffsverstärkung] Elementarschadensbonus (%)",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: [Angriffsverstärkung] Benötigte Punkte",

                // === Tier 4: Kampfverbesserung (6) ===
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4-1: [Nahkampf-Verb.] 2-Treffer-Kombo-Bonus (%)",
                ["Tier4_MeleeEnhance_RequiredPoints"] = "Tier 4-1: [Nahkampf-Verb.] Benötigte Punkte",
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4-2: [Präzisionsangriff] Kritische Trefferchance (%)",
                ["Tier4_PrecisionAttack_RequiredPoints"] = "Tier 4-2: [Präzisionsangriff] Benötigte Punkte",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4-3: [Fernkampf-Verb.] Schadensbonus (%)",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4-3: [Fernkampf-Verb.] Benötigte Punkte",

                // === Tier 5: Ladung (3) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: [Ladung] Ausdauerregeneration (%)",
                ["Tier5_Charge_TriggerChance"] = "Tier 5: [Ladung] Aktivierungschance (%)",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: [Ladung] Benötigte Punkte",

                // === Tier 6: Abschlussverbesserung (8) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6-1: [Schwachpunkt] Kritischer Schadensbonus (%)",
                ["Tier6_WeakPointAttack_RequiredPoints"] = "Tier 6-1: [Schwachpunkt] Benötigte Punkte",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6-2: [Kombo-Abschluss] 3-Treffer-Kombo-Bonus (%)",
                ["Tier6_ComboFinisher_RequiredPoints"] = "Tier 6-2: [Kombo-Abschluss] Benötigte Punkte",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6-3: [Zweihandschlag] Schadensbonus (%)",
                ["Tier6_TwoHandCrush_RequiredPoints"] = "Tier 6-3: [Zweihandschlag] Benötigte Punkte",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6-4: [Elementarangriff] Elementarbonus (%)",
                ["Tier6_ElementalAttack_RequiredPoints"] = "Tier 6-4: [Elementarangriff] Benötigte Punkte",

                // ============================================
                // Geschwindigkeits-Skilltree - 49 Schlüssel
                // ============================================

                // === Tier 0: Geschwindigkeits-Experte (2) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: [Geschwindigkeits-Experte] Bewegungsgeschwindigkeitsbonus (%)",
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: [Geschwindigkeits-Experte] Benötigte Punkte",

                // === Tier 1: Agilitätsbasis (5) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: [Agilitätsbasis] Geschwindigkeitsbonus nach Ausweichen (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: [Agilitätsbasis] Buff-Dauer (Sek.)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: [Agilitätsbasis] Angriffsgeschwindigkeitsbonus (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: [Agilitätsbasis] Ausweichgeschwindigkeitsbonus (%)",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: [Agilitätsbasis] Benötigte Punkte",

                // === Tier 2-1: Kontinuierlicher Fluss (5) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2-1: [Kont. Fluss] Angriffsgeschw.-Bonus beim 2. Treffer (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2-1: [Kont. Fluss] Ausdauerreduzierung (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2-1: [Kont. Fluss] Buff-Dauer (Sek.)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2-1: [Kont. Fluss] Kombogeschwindigkeitsbonus (%)",
                ["Tier2_MeleeFlow_RequiredPoints"] = "Tier 2-1: [Kont. Fluss] Benötigte Punkte",

                // === Tier 2-2: Armbrust-Spezialist (4) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2-2: [Armbrust-Spez.] Bewegungsgeschw.-Bonus bei Treffer (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2-2: [Armbrust-Spez.] Buff-Dauer (Sek.)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2-2: [Armbrust-Spez.] Nachladgeschw.-Bonus während Buff (%)",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "Tier 2-2: [Armbrust-Spez.] Benötigte Punkte",

                // === Tier 2-3: Bogen-Spezialist (4) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2-3: [Bogen-Spez.] Ausdauerreduzierung bei 2-Treffer-Kombo (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2-3: [Bogen-Spez.] Nächster-Pfeil-Geschwindigkeitsbonus (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2-3: [Bogen-Spez.] Buff-Dauer (Sek.)",
                ["Tier2_BowExpert_RequiredPoints"] = "Tier 2-3: [Bogen-Spez.] Benötigte Punkte",

                // === Tier 2-4: Mobiler Zauber (4) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2-4: [Mobiler Zauber] Bewegungsgeschw.-Bonus beim Zaubern (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2-4: [Mobiler Zauber] Eitr-Kostenreduzierung (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2-4: [Mobiler Zauber] Bewegungsgeschw. beim Stabzaubern (%)",
                ["Tier2_MobileCast_RequiredPoints"] = "Tier 2-4: [Mobiler Zauber] Benötigte Punkte",

                // === Tier 3-1: Praktikant 1 (3) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3-1: [Praktikant 1] Nahkampf-Fertigkeitsbonus",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3-1: [Praktikant 1] Armbrust-Fertigkeitsbonus",
                ["Tier3_Practitioner1_RequiredPoints"] = "Tier 3-1: [Praktikant 1] Benötigte Punkte",

                // === Tier 3-2: Praktikant 2 (3) ===
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3-2: [Praktikant 2] Stab-Fertigkeitsbonus",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3-2: [Praktikant 2] Bogen-Fertigkeitsbonus",
                ["Tier3_Practitioner2_RequiredPoints"] = "Tier 3-2: [Praktikant 2] Benötigte Punkte",

                // === Tier 4-1: Energizer (2) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4-1: [Energizer] Nahrungsverbrauchsreduzierung (%)",
                ["Tier4_Energizer_RequiredPoints"] = "Tier 4-1: [Energizer] Benötigte Punkte",

                // === Tier 4-2: Kapitän (2) ===
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4-2: [Kapitän] Schiffsgeschwindigkeitsbonus (%)",
                ["Tier4_Captain_RequiredPoints"] = "Tier 4-2: [Kapitän] Benötigte Punkte",

                // === Tier 5: Sprungmeister (3) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: [Sprungmeister] Sprung-Fertigkeitsbonus",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: [Sprungmeister] Sprung-Ausdauerreduzierung (%)",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: [Sprungmeister] Benötigte Punkte",

                // === Tier 6-1: Geschicklichkeit (3) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6-1: [Geschicklichkeit] Nahkampf-Angriffsgeschw.-Bonus (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6-1: [Geschicklichkeit] Bewegungsgeschwindigkeitsbonus (%)",
                ["Tier6_Dexterity_RequiredPoints"] = "Tier 6-1: [Geschicklichkeit] Benötigte Punkte",

                // === Tier 6-2: Ausdauer (2) ===
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6-2: [Ausdauer] Maximale Ausdauer-Bonus",
                ["Tier6_Endurance_RequiredPoints"] = "Tier 6-2: [Ausdauer] Benötigte Punkte",

                // === Tier 6-3: Intellekt (2) ===
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6-3: [Intellekt] Maximaler Eitr-Bonus",
                ["Tier6_Intellect_RequiredPoints"] = "Tier 6-3: [Intellekt] Benötigte Punkte",

                // === Tier 7: Meister (3) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: [Meister] Lauf-Fertigkeitsbonus",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: [Meister] Sprung-Fertigkeitsbonus",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: [Meister] Benötigte Punkte",

                // === Tier 8-1: Nahkampf-Beschleunigung (3) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8-1: [Nahkampf-Beschl.] Nahkampf-Angriffsgeschw.-Bonus (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8-1: [Nahkampf-Beschl.] Nächster-Angriff-Bonus bei 3-Treffer-Kombo (%)",
                ["Tier8_MeleeAccel_RequiredPoints"] = "Tier 8-1: [Nahkampf-Beschl.] Benötigte Punkte",

                // === Tier 8-2: Armbrust-Beschleunigung (3) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8-2: [Armbrust-Beschl.] Nachladgeschwindigkeitsbonus (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8-2: [Armbrust-Beschl.] Bewegungsgeschw. beim Nachladen (%)",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "Tier 8-2: [Armbrust-Beschl.] Benötigte Punkte",

                // === Tier 8-3: Bogen-Beschleunigung (3) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8-3: [Bogen-Beschl.] Pfeilspann-Geschwindigkeitsbonus (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8-3: [Bogen-Beschl.] Bewegungsgeschw. beim Spannen (%)",
                ["Tier8_BowAccel_RequiredPoints"] = "Tier 8-3: [Bogen-Beschl.] Benötigte Punkte",

                // === Tier 8-4: Zauber-Beschleunigung (3) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8-4: [Zauber-Beschl.] Magische Angriffsgeschw.-Bonus (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8-4: [Zauber-Beschl.] Max-Eitr-Regenerationsrate bei 3-Treffer-Kombo (%)",
                ["Tier8_CastAccel_RequiredPoints"] = "Tier 8-4: [Zauber-Beschl.] Benötigte Punkte",

                // ============================================
                // Verteidigungs-Skilltree - 59 Schlüssel
                // ============================================

                // === Tier 0: Verteidigungs-Experte (3) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: [Verteidigungs-Experte] LP-Bonus",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: [Verteidigungs-Experte] Rüstungsbonus",
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: [Verteidigungs-Experte] Benötigte Punkte",

                // === Tier 1: Hautverhärtung (3) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: [Hautverhärtung] LP-Bonus",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: [Hautverhärtung] Rüstungsbonus",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: [Hautverhärtung] Benötigte Punkte",

                // === Tier 2-1: Geist-Körper-Training (3) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2-1: [Geist-Körper-Training] Maximale Ausdauer-Bonus",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2-1: [Geist-Körper-Training] Maximaler Eitr-Bonus",
                ["Tier2_MindTraining_RequiredPoints"] = "Tier 2-1: [Geist-Körper-Training] Benötigte Punkte",

                // === Tier 2-2: Gesundheitstraining (3) ===
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2-2: [Gesundheitstraining] LP-Bonus",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2-2: [Gesundheitstraining] Rüstungsbonus",
                ["Tier2_HealthTraining_RequiredPoints"] = "Tier 2-2: [Gesundheitstraining] Benötigte Punkte",

                // === Tier 3-1: Kernatmung (2) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3-1: [Kernatmung] Eitr-Bonus",
                ["Tier3_CoreBreathing_RequiredPoints"] = "Tier 3-1: [Kernatmung] Benötigte Punkte",

                // === Tier 3-2: Ausweichtraining (3) ===
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3-2: [Ausweichtraining] Ausweichbonus (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3-2: [Ausweichtraining] Unverwundbarkeit beim Rollen (%)",
                ["Tier3_EvasionTraining_RequiredPoints"] = "Tier 3-2: [Ausweichtraining] Benötigte Punkte",

                // === Tier 3-3: Gesundheitssteigerung (2) ===
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3-3: [Gesundheitssteigerung] LP-Bonus",
                ["Tier3_HealthBoost_RequiredPoints"] = "Tier 3-3: [Gesundheitssteigerung] Benötigte Punkte",

                // === Tier 3-4: Schildtraining (2) ===
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3-4: [Schildtraining] Blockstärke-Bonus",
                ["Tier3_ShieldTraining_RequiredPoints"] = "Tier 3-4: [Schildtraining] Benötigte Punkte",

                // === Tier 4-1: Schockwelle (4) ===
                ["Tier4_Shockwave_Radius"] = "Tier 4-1: [Schockwelle] Radius",
                ["Tier4_Shockwave_StunDuration"] = "Tier 4-1: [Schockwelle] Betäubungsdauer",
                ["Tier4_Shockwave_Cooldown"] = "Tier 4-1: [Schockwelle] Abklingzeit",
                ["Tier4_Shockwave_RequiredPoints"] = "Tier 4-1: [Schockwelle] Benötigte Punkte",

                // === Tier 4-2: Bodenstomp (6) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4-2: [Bodenstomp] Effektradius (m)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4-2: [Bodenstomp] Rückstoßkraft",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4-2: [Bodenstomp] Abklingzeit (Sek.)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4-2: [Bodenstomp] LP-Schwellenwert für Auto-Aktivierung",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4-2: [Bodenstomp] VFX-Dauer (Sek.)",
                ["Tier4_GroundStomp_RequiredPoints"] = "Tier 4-2: [Bodenstomp] Benötigte Punkte",

                // === Tier 4-3: Steinhaut (2) ===
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4-3: [Steinhaut] Rüstungsverstärkung (%)",
                ["Tier4_RockSkin_RequiredPoints"] = "Tier 4-3: [Steinhaut] Benötigte Punkte",

                // === Tier 5-1: Ausdauer (3) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5-1: [Ausdauer] Lauf-Ausdauerreduzierung (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5-1: [Ausdauer] Sprung-Ausdauerreduzierung (%)",
                ["Tier5_Endurance_RequiredPoints"] = "Tier 5-1: [Ausdauer] Benötigte Punkte",

                // === Tier 5-2: Agilität (3) ===
                ["Tier5_Agility_DodgeBonus"] = "Tier 5-2: [Agilität] Ausweichbonus (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5-2: [Agilität] Roll-Ausdauerreduzierung (%)",
                ["Tier5_Agility_RequiredPoints"] = "Tier 5-2: [Agilität] Benötigte Punkte",

                // === Tier 5-3: Troll-Regeneration (3) ===
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5-3: [Troll-Regen.] LP-Regenerationsbonus (pro Sek.)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5-3: [Troll-Regen.] Regenerationsintervall (Sek.)",
                ["Tier5_TrollRegen_RequiredPoints"] = "Tier 5-3: [Troll-Regen.] Benötigte Punkte",

                // === Tier 5-4: Blockmeister (3) ===
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5-4: [Blockmeister] Blockstärke-Bonus",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5-4: [Blockmeister] Parierzeit-Bonus (Sek.)",
                ["Tier5_BlockMaster_RequiredPoints"] = "Tier 5-4: [Blockmeister] Benötigte Punkte",

                // === Tier 6-1: Geistesschild (1) ===
                ["Tier6_MindShield_RequiredPoints"] = "Tier 6-1: [Geistesschild] Benötigte Punkte",

                // === Tier 6-2: Nervenverbesserung (2) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6-2: [Nervenverb.] Bedingter Ausweichbonus (30 Sek., %)",
                ["Tier6_NerveEnhancement_RequiredPoints"] = "Tier 6-2: [Nervenverb.] Benötigte Punkte",

                // === Tier 6-3: Doppelsprung (1) ===
                ["Tier6_DoubleJump_RequiredPoints"] = "Tier 6-3: [Doppelsprung] Benötigte Punkte",

                // === Tier 6-4: Jotunn-Vitalität (3) ===
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6-4: [Jotunn-Vitalität] LP-Bonus (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6-4: [Jotunn-Vitalität] Physische/Elementare Resistenz (%)",
                ["Tier6_JotunnVitality_RequiredPoints"] = "Tier 6-4: [Jotunn-Vitalität] Benötigte Punkte",

                // === Tier 6-5: Jotunn-Schild (4) ===
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6-5: [Jotunn-Schild] Block-Ausdauerreduzierung (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6-5: [Jotunn-Schild] Geschw.-Bonus mit Normalschild (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6-5: [Jotunn-Schild] Geschw.-Bonus mit Turmschild (%)",
                ["Tier6_JotunnShield_RequiredPoints"] = "Tier 6-5: [Jotunn-Schild] Benötigte Punkte",

                // ============================================
                // Produktions-Skilltree - 22 Schlüssel
                // ============================================

                // === Tier 0: Produktions-Experte (1) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] = "Tier 0: Holz +1 Chance (%)",

                // === Tier 1: Anfänger-Arbeiter (1) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] = "Tier 1: Holz +1 Chance (%)",

                // === Tier 2: Spezialisierung (5) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] = "Tier 2: Holzfällen Lv2 - Holz +1 Chance (%)",
                ["Tier2_GatheringLv2_BonusChance"] = "Tier 2: Sammeln Lv2 - Gegenstand +1 Chance (%)",
                ["Tier2_MiningLv2_BonusChance"] = "Tier 2: Bergbau Lv2 - Erz +1 Chance (%)",
                ["Tier2_CraftingLv2_UpgradeChance"] = "Tier 2: Handwerk Lv2 - Aufwertung +1 Chance (%)",
                ["Tier2_CraftingLv2_DurabilityBonus"] = "Tier 2: Handwerk Lv2 - Max. Haltbarkeit (%)",

                // === Tier 3: Mittelstufen-Fähigkeiten (5) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] = "Tier 3: Holzfällen Lv3 - Holz +2 Chance (%)",
                ["Tier3_GatheringLv3_BonusChance"] = "Tier 3: Sammeln Lv3 - Gegenstand +1 Chance (%)",
                ["Tier3_MiningLv3_BonusChance"] = "Tier 3: Bergbau Lv3 - Erz +1 Chance (%)",
                ["Tier3_CraftingLv3_UpgradeChance"] = "Tier 3: Handwerk Lv3 - Aufwertung +1 Chance (%)",
                ["Tier3_CraftingLv3_DurabilityBonus"] = "Tier 3: Handwerk Lv3 - Max. Haltbarkeit (%)",

                // === Tier 4: Fortgeschrittene Fähigkeiten (5) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] = "Tier 4: Holzfällen Lv4 - Holz +2 Chance (%)",
                ["Tier4_GatheringLv4_BonusChance"] = "Tier 4: Sammeln Lv4 - Gegenstand +1 Chance (%)",
                ["Tier4_MiningLv4_BonusChance"] = "Tier 4: Bergbau Lv4 - Erz +1 Chance (%)",
                ["Tier4_CraftingLv4_UpgradeChance"] = "Tier 4: Handwerk Lv4 - Aufwertung +1 Chance (%)",
                ["Tier4_CraftingLv4_DurabilityBonus"] = "Tier 4: Handwerk Lv4 - Max. Haltbarkeit (%)",

                // === Produktionsbaum: Benötigte Punkte (14) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: [Produktions-Experte] Benötigte Punkte",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: [Anfänger-Arbeiter] Benötigte Punkte",
                ["Tier2_WoodcuttingLv2_RequiredPoints"] = "Tier 2: [Holzfällen Lv2] Benötigte Punkte",
                ["Tier2_GatheringLv2_RequiredPoints"] = "Tier 2: [Sammeln Lv2] Benötigte Punkte",
                ["Tier2_MiningLv2_RequiredPoints"] = "Tier 2: [Bergbau Lv2] Benötigte Punkte",
                ["Tier2_CraftingLv2_RequiredPoints"] = "Tier 2: [Handwerk Lv2] Benötigte Punkte",
                ["Tier3_WoodcuttingLv3_RequiredPoints"] = "Tier 3: [Holzfällen Lv3] Benötigte Punkte",
                ["Tier3_GatheringLv3_RequiredPoints"] = "Tier 3: [Sammeln Lv3] Benötigte Punkte",
                ["Tier3_MiningLv3_RequiredPoints"] = "Tier 3: [Bergbau Lv3] Benötigte Punkte",
                ["Tier3_CraftingLv3_RequiredPoints"] = "Tier 3: [Handwerk Lv3] Benötigte Punkte",
                ["Tier4_WoodcuttingLv4_RequiredPoints"] = "Tier 4: [Holzfällen Lv4] Benötigte Punkte",
                ["Tier4_GatheringLv4_RequiredPoints"] = "Tier 4: [Sammeln Lv4] Benötigte Punkte",
                ["Tier4_MiningLv4_RequiredPoints"] = "Tier 4: [Bergbau Lv4] Benötigte Punkte",
                ["Tier4_CraftingLv4_RequiredPoints"] = "Tier 4: [Handwerk Lv4] Benötigte Punkte",

                // ============================================
                // Bogen-Skilltree - 34 Schlüssel
                // ============================================

                // === Tier 0: Bogen-Experte (2) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: [Bogen-Experte] Bogenschadensbonus (%)",
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: [Bogen-Experte] Benötigte Punkte",

                // === Tier 1-1: Fokussierter Schuss (2) ===
                ["Tier1_FocusedShot_CritBonus"] = "Tier 1-1: [Fokussierter Schuss] Kritische Trefferchance (%)",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1-1: [Fokussierter Schuss] Benötigte Punkte",

                // === Tier 1-2: Mehrfachschuss Lv1 (5) ===
                ["Tier1_MultishotLv1_ActivationChance"] = "Tier 1-2: [Mehrfachschuss Lv1] Aktivierungschance (%)",
                ["Tier1_MultishotLv1_AdditionalArrows"] = "Tier 1-2: [Mehrfachschuss Lv1] Zusatzpfeile",
                ["Tier1_MultishotLv1_ArrowConsumption"] = "Tier 1-2: [Mehrfachschuss Lv1] Pfeilverbrauch",
                ["Tier1_MultishotLv1_DamagePerArrow"] = "Tier 1-2: [Mehrfachschuss Lv1] Schaden pro Pfeil (%)",
                ["Tier1_MultishotLv1_RequiredPoints"] = "Tier 1-2: [Mehrfachschuss Lv1] Benötigte Punkte",

                // === Tier 2: Bogenmeisterschaft (3) ===
                ["Tier2_BowMastery_SkillBonus"] = "Tier 2: [Bogenmeisterschaft] Bogenfähigkeitsbonus",
                ["Tier2_BowMastery_SpecialArrowChance"] = "Tier 2: [Bogenmeisterschaft] Spezialpfeil-Chance (%)",
                ["Tier2_BowMastery_RequiredPoints"] = "Tier 2: [Bogenmeisterschaft] Benötigte Punkte",

                // === Tier 3-1: Stiller Treffer (2) ===
                ["Tier3_SilentStrike_DamageBonus"] = "Tier 3-1: [Stiller Treffer] Schadenserhöhung",
                ["Tier3_SilentStrike_RequiredPoints"] = "Tier 3-1: [Stiller Treffer] Benötigte Punkte",

                // === Tier 3-2: Mehrfachschuss Lv2 (2) ===
                ["Tier3_MultishotLv2_ActivationChance"] = "Tier 3-2: [Mehrfachschuss Lv2] Aktivierungschance (%)",
                ["Tier3_MultishotLv2_RequiredPoints"] = "Tier 3-2: [Mehrfachschuss Lv2] Benötigte Punkte",

                // === Tier 3-3: Jägerinstinkt (2) ===
                ["Tier3_HuntingInstinct_CritBonus"] = "Tier 3-3: [Jägerinstinkt] Kritische Trefferchance (%)",
                ["Tier3_HuntingInstinct_RequiredPoints"] = "Tier 3-3: [Jägerinstinkt] Benötigte Punkte",

                // === Tier 4: Präzisionszielen (2) ===
                ["Tier4_PrecisionAim_CritDamage"] = "Tier 4: [Präzisionszielen] Kritischer Schadensbonus (%)",
                ["Tier4_PrecisionAim_RequiredPoints"] = "Tier 4: [Präzisionszielen] Benötigte Punkte",

                // === Tier 5: Explosivpfeil (5) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] = "Tier 5: [Explosivpfeil] Schadensmultiplikator (%)",
                ["Tier5_ExplosiveArrow_Radius"] = "Tier 5: [Explosivpfeil] Explosionsradius (m)",
                ["Tier5_ExplosiveArrow_Cooldown"] = "Tier 5: [Explosivpfeil] Abklingzeit (Sek.)",
                ["Tier5_ExplosiveArrow_StaminaCost"] = "Tier 5: [Explosivpfeil] Ausdauerkosten (%)",
                ["Tier5_ExplosiveArrow_RequiredPoints"] = "Tier 5: [Explosivpfeil] Benötigte Punkte",

                // ============================================
                // Schwert-Skilltree (Legacy) - 30 Schlüssel
                // ============================================

                ["Sword_Expert_DamageIncrease"] = "Tier 0: Schwertschadenserhöhung (%)",
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: Angriffsgeschwindigkeitsbonus (%)",
                ["Sword_ComboSlash_Bonus"] = "Tier 2: Kombo-Angriffsbonus (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: Buff-Dauer (Sek.)",
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: Schadensbonus",
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: Angriffsgeschwindigkeitsbonus (%)",
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: Buff-Dauer (Sek.)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: Ansturm-Angriffsbonus (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: Stoßdistanz (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: Ausdauerkosten",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: Abklingzeit (Sek.)",
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: 1. Treffer Schadensrate (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: 2. Treffer Schadensrate (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: 3. Treffer Schadensrate (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: Initiale Ansturmdistanz (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: Seitliche Bewegungsdistanz (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: Ausdauerkosten",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: Abklingzeit (Sek.)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: Bewegungsgeschwindigkeit (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: Angriffsgeschwindigkeitsbonus (%)",
            };
        }
    }
}

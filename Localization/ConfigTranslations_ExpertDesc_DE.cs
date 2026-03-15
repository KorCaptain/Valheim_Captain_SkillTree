using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetExpertDescriptions_DE()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Skill_Tree_Base
                // ========================================
                ["PassiveMessageDisplay"] =
                "【Passive Meldungsanzeige】\n" +
                "Steuert die Anzeige von Meldungen bei passiven Fähigkeiten.\n" +
                "  Center = Bildschirmmitte (Standard)\n" +
                "  TopLeft = Kleiner Text oben links\n" +
                "  Off = Deaktiviert\n" +
                "※ Lern- und Produktionsmeldungen erscheinen immer in der Mitte.",

                // ========================================
                // Angriffs-Skilltree (Attack Tree)
                // ========================================
                ["Tier0_AttackExpert_AllDamageBonus"] =
                "【Gesamtschadensbonus (%)】\n" +
                "Erhöht physischen und elementaren Schaden.\n" +
                "Grundlegende Angriffskraftsteigerung für alle Waffen.\n" +
                "Empfehlung: 8-12%",

                ["Tier2_MeleeSpec_BonusTriggerChance"] =
                "【Nahkampf-Immer-Schadensbonus (%)】\n" +
                "Fügt bei Nahkampfangriffen immer zusätzlichen Schaden hinzu.\n" +
                "Wird bei jedem Angriff fest angewendet.\n" +
                "Empfehlung: 15-25%",

                ["Tier2_MeleeSpec_MeleeDamage"] =
                "【Nahkampf-Zusatzschaden (Fest)】\n" +
                "Fester Zusatzschaden bei Aktivierung des Bonus.\n" +
                "Empfehlung: 8-15",

                ["Tier2_BowSpec_BonusTriggerChance"] =
                "【Bogen-Immer-Schadensbonus (%)】\n" +
                "Fügt bei Bogenangriffen immer zusätzlichen Schaden hinzu.\n" +
                "Wird bei jedem Angriff fest angewendet.\n" +
                "Empfehlung: 15-25%",

                ["Tier2_BowSpec_BowDamage"] =
                "【Bogen-Zusatzschaden (Fest)】\n" +
                "Fester Zusatzschaden bei Aktivierung des Bonus.\n" +
                "Empfehlung: 6-12",

                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] =
                "【Armbrust-Immer-Schadensbonus (%)】\n" +
                "Fügt bei Armbrustangriffen immer zusätzlichen Schaden hinzu.\n" +
                "Wird bei jedem Angriff fest angewendet.\n" +
                "Empfehlung: 12-20%",

                ["Tier2_CrossbowSpec_CrossbowDamage"] =
                "【Armbrust-Zusatzschaden (Fest)】\n" +
                "Fester Zusatzschaden bei Aktivierung des Bonus.\n" +
                "Empfehlung: 7-13",

                ["Tier2_StaffSpec_ElementalTriggerChance"] =
                "【Stab-Immer-Schadensbonus (%)】\n" +
                "Fügt bei Stabangriffen immer zusätzlichen Schaden hinzu.\n" +
                "Wird bei jedem Angriff fest angewendet.\n" +
                "Empfehlung: 15-25%",

                ["Tier2_StaffSpec_StaffDamage"] =
                "【Stab-Zusatzschaden (Fest)】\n" +
                "Fester Zusatzschaden bei Aktivierung des Bonus.\n" +
                "Empfehlung: 6-12",

                ["Tier1_BaseAttack_PhysicalDamageBonus"] =
                "【Physischer Schadensbonus (Fest)】\n" +
                "Erhöht den physischen Schaden aller Waffen um einen festen Wert.\n" +
                "Empfehlung: 1-3",

                ["Tier1_BaseAttack_ElementalDamageBonus"] =
                "【Elementarschadensbonus (Fest)】\n" +
                "Erhöht den Elementarschaden (Feuer, Eis, Blitz) um einen festen Wert.\n" +
                "Empfehlung: 1-3",

                ["Tier3_AttackBoost_PhysicalDamageBonus"] =
                "【Zweihand-Physischer Schadensbonus (%)】\n" +
                "Erhöht physischen Schaden bei Zweihandwaffen.\n" +
                "Empfehlung: 8-15%",

                ["Tier3_AttackBoost_ElementalDamageBonus"] =
                "【Zweihand-Elementarschadensbonus (%)】\n" +
                "Erhöht elementaren Schaden bei Zweihandwaffen.\n" +
                "Empfehlung: 8-15%",

                ["Tier4_PrecisionAttack_CritChance"] =
                "【Kritische Trefferchance-Bonus (%)】\n" +
                "Erhöht die kritische Trefferchance für alle Angriffe.\n" +
                "Empfehlung: 3-8%",

                ["Tier4_MeleeEnhance_2HitComboBonus"] =
                "【2-Treffer-Kombo-Bonus (%)】\n" +
                "Erhöht Schaden bei 2 aufeinanderfolgenden Nahkampfangriffen.\n" +
                "Empfehlung: 8-15%",

                ["Tier4_RangedEnhance_RangedDamageBonus"] =
                "【Fernkampfschadensbonus (Fest)】\n" +
                "Erhöht Fernkampfschaden (Bogen, Armbrust) um einen festen Wert.\n" +
                "Empfehlung: 3-8",

                ["Tier5_SpecialStat_SpecBonus"] =
                "【Ausdauerregeneration】\n" +
                "Prozentsatz der Ausdauerregeneration bei Treffern.\n" +
                "Empfehlung: 3-10",

                ["Tier5_Charge_TriggerChance"] =
                "【Aktivierungschance】\n" +
                "Chance auf Ausdauerregeneration bei Treffern.\n" +
                "Empfehlung: 20-50",

                ["Tier6_WeakPointAttack_CritDamageBonus"] =
                "【Kritischer Schadensbonus (%)】\n" +
                "Erhöht den Zusatzschaden bei kritischen Treffern.\n" +
                "Empfehlung: 10-20%",

                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] =
                "【Zweihand-Schadensbonus (%)】\n" +
                "Erhöht den Gesamtschaden bei Zweihandwaffen.\n" +
                "Empfehlung: 8-15%",

                ["Tier6_ElementalAttack_ElementalBonus"] =
                "【Stab-Elementarschadensbonus (%)】\n" +
                "Erhöht den Elementarschaden des Stabs (Feuer, Eis, Blitz).\n" +
                "Empfehlung: 8-15%",

                ["Tier6_ComboFinisher_3HitComboBonus"] =
                "【3-Treffer-Kombo-Abschluss-Bonus (%)】\n" +
                "Erhöht den Schaden des letzten Treffers in einer 3-Treffer-Kombo.\n" +
                "Empfehlung: 12-20%",

                // ========================================
                // Verteidigungs-Skilltree (Defense Tree)
                // ========================================
                ["Tier0_DefenseExpert_HPBonus"] =
                "【LP-Bonus (Fest)】\n" +
                "Erhöht die maximalen LP um einen festen Wert.\n" +
                "Empfehlung: 3-8",

                ["Tier0_DefenseExpert_ArmorBonus"] =
                "【Rüstungsbonus (Fest)】\n" +
                "Erhöht die Rüstung um einen festen Wert.\n" +
                "Empfehlung: 1-4",

                ["Tier1_SkinHardening_HPBonus"] =
                "【LP-Bonus (Fest)】\n" +
                "Erhöht zusätzlich die maximalen LP.\n" +
                "Empfehlung: 3-8",

                ["Tier1_SkinHardening_ArmorBonus"] =
                "【Rüstungsbonus (Fest)】\n" +
                "Erhöht zusätzlich die Rüstung.\n" +
                "Empfehlung: 3-8",

                ["Tier2_MindBodyTraining_StaminaBonus"] =
                "【Maximale Ausdauer-Bonus (Fest)】\n" +
                "Erhöht die maximale Ausdauer.\n" +
                "Empfehlung: 20-30",

                ["Tier2_MindBodyTraining_EitrBonus"] =
                "【Maximaler Eitr-Bonus (Fest)】\n" +
                "Erhöht den maximalen Eitr für magische Angriffe.\n" +
                "Empfehlung: 20-30",

                ["Tier2_HealthTraining_HPBonus"] =
                "【LP-Bonus (Fest)】\n" +
                "Erhöht die maximalen LP erheblich.\n" +
                "Empfehlung: 15-25",

                ["Tier2_HealthTraining_ArmorBonus"] =
                "【Rüstungsbonus (Fest)】\n" +
                "Erhöht zusätzlich die Rüstung.\n" +
                "Empfehlung: 3-8",

                ["Tier3_CoreBreathing_EitrBonus"] =
                "【Eitr-Bonus (Fest)】\n" +
                "Erhöht Eitr durch Meditation.\n" +
                "Empfehlung: 8-15",

                ["Tier3_EvasionTraining_DodgeBonus"] =
                "【Ausweichbonus (%)】\n" +
                "Erhöht die Chance, feindlichen Angriffen auszuweichen.\n" +
                "Empfehlung: 3-8%",

                ["Tier3_EvasionTraining_InvincibilityBonus"] =
                "【Unverwundbarkeitszeit beim Rollen (%)】\n" +
                "Verlängert die Unverwundbarkeitszeit beim Rollen.\n" +
                "Empfehlung: 15-25%",

                ["Tier3_HealthBoost_HPBonus"] =
                "【LP-Bonus (Fest)】\n" +
                "Erhöht zusätzlich die LP.\n" +
                "Empfehlung: 12-20",

                ["Tier3_ShieldTraining_BlockPowerBonus"] =
                "【Blockstärke-Bonus (Fest)】\n" +
                "Erhöht die Blockstärke des Schilds.\n" +
                "Empfehlung: 80-120",

                ["Tier4_GroundStomp_Radius"] =
                "【Effektradius (m)】\n" +
                "Radius der Erschütterungswelle.\n" +
                "Empfehlung: 2,5-4 m",

                ["Tier4_GroundStomp_KnockbackForce"] =
                "【Rückstoßkraft】\n" +
                "Kraft, mit der Feinde zurückgeworfen werden.\n" +
                "Empfehlung: 15-25",

                ["Tier4_GroundStomp_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 100-150 Sek.",

                ["Tier4_GroundStomp_HPThreshold"] =
                "【LP-Schwellenwert für automatische Aktivierung】\n" +
                "Aktiviert sich automatisch, wenn LP unter diesen Wert fallen.\n" +
                "0,35 = 35% der LP\n" +
                "Empfehlung: 0,30-0,40",

                ["Tier4_GroundStomp_VFXDuration"] =
                "【Visueller Effekt Dauer (Sek.)】\n" +
                "Anzeigedauer des visuellen Effekts.\n" +
                "Empfehlung: 0,8-1,5 Sek.",

                ["Tier4_RockSkin_ArmorBonus"] =
                "【Rüstungsverstärkung (%)】\n" +
                "Wendet einen prozentualen Bonus auf Helm, Brust, Beine und Schild an.\n" +
                "Empfehlung: 10-15%",

                ["Tier5_Endurance_RunStaminaReduction"] =
                "【Ausdauerverbrauch beim Laufen (%)】\n" +
                "Reduziert den Ausdauerverbrauch beim Laufen.\n" +
                "Empfehlung: 8-15%",

                ["Tier5_Endurance_JumpStaminaReduction"] =
                "【Ausdauerverbrauch beim Springen (%)】\n" +
                "Reduziert den Ausdauerverbrauch beim Springen.\n" +
                "Empfehlung: 8-15%",

                ["Tier5_Agility_DodgeBonus"] =
                "【Ausweichbonus (%)】\n" +
                "Erhöht zusätzlich die Ausweichance.\n" +
                "Empfehlung: 3-8%",

                ["Tier5_Agility_RollStaminaReduction"] =
                "【Ausdauerverbrauch beim Rollen (%)】\n" +
                "Reduziert den Ausdauerverbrauch beim Rollen.\n" +
                "Empfehlung: 10-18%",

                ["Tier5_TrollRegen_HPRegenBonus"] =
                "【LP-Regenerationsbonus (pro Sek.)】\n" +
                "Stellt automatisch LP wieder her wie ein Troll.\n" +
                "Empfehlung: 3-8",

                ["Tier5_TrollRegen_RegenInterval"] =
                "【Regenerationsintervall (Sek.)】\n" +
                "Zeitraum der LP-Wiederherstellung.\n" +
                "Empfehlung: 1,5-3 Sek.",

                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] =
                "【Blockstärke-Bonus (Fest)】\n" +
                "Erhöht die Blockstärke des Schilds erheblich.\n" +
                "Empfehlung: 80-120",

                ["Tier5_BlockMaster_ParryDurationBonus"] =
                "【Parierzeit-Bonus (Sek.)】\n" +
                "Verlängert die Wirkungsdauer nach erfolgreichem Parieren.\n" +
                "Empfehlung: 0,8-1,5 Sek.",

                ["Tier6_NerveEnhancement_DodgeBonus"] =
                "【Bedingter Ausweichbonus (30 Sek., %)】\n" +
                "Aktiviert sich, wenn 30 Sekunden lang nicht ausgewichen wurde.\n" +
                "Empfehlung: 30-50%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【LP-Bonus (%)】\n" +
                "Erhöht die maximalen LP prozentual.\n" +
                "Empfehlung: 25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【Physische/Elementare Resistenz (%)】\n" +
                "Reduziert den gesamten physischen und elementaren Schaden.\n" +
                "Empfehlung: 8-15%",

                ["Tier6_JotunnShield_BlockStaminaReduction"] =
                "【Ausdauerverbrauch beim Blocken (%)】\n" +
                "Reduziert den Ausdauerverbrauch beim Blocken mit dem Schild.\n" +
                "Empfehlung: 20-30%",

                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] =
                "【Bewegungsgeschwindigkeit mit Normalschild (%)】\n" +
                "Erhöht die Bewegungsgeschwindigkeit mit normalem Schild.\n" +
                "Empfehlung: 3-8%",

                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] =
                "【Bewegungsgeschwindigkeit mit Turmschild (%)】\n" +
                "Erhöht die Bewegungsgeschwindigkeit mit Turmschild.\n" +
                "Empfehlung: 8-15%",

                // ========================================
                // Produktions-Skilltree (Production Tree)
                // ========================================
                ["Tier0_ProductionExpert_WoodBonusChance"] =
                "【Holz +1 Bonuschance (%)】\n" +
                "Chance auf zusätzliches Holz beim Holzfällen.\n" +
                "Empfehlung: 40-60%",

                ["Tier0_ProductionExpert_RequiredPoints"] =
                "【Benötigte Punkte - Produktions-Experte】\n" +
                "Fertigkeitspunkte zum Freischalten des Produktions-Experten.\n" +
                "Empfehlung: 2",

                ["Tier1_NoviceWorker_WoodBonusChance"] =
                "【Holz +1 Bonuschance (%)】\n" +
                "Erhöht die Chance auf zusätzliches Holz beim Holzfällen.\n" +
                "Empfehlung: 20-30%",

                ["Tier1_NoviceWorker_RequiredPoints"] =
                "【Benötigte Punkte - Anfänger】\n" +
                "Fertigkeitspunkte zum Freischalten des Anfänger-Arbeiters.\n" +
                "Empfehlung: 2",

                ["Tier2_WoodcuttingLv2_BonusChance"] =
                "【Holz +1 Bonuschance (%)】\n" +
                "Holzfällen Lv.2 - Chance auf zusätzliches Holz.\n" +
                "Empfehlung: 20-30%",

                ["Tier2_WoodcuttingLv2_RequiredPoints"] =
                "【Benötigte Punkte - Holzfällen Lv.2】\n" +
                "Empfehlung: 2",

                ["Tier2_GatheringLv2_BonusChance"] =
                "【Gegenstand +1 Bonuschance (%)】\n" +
                "Sammeln Lv.2 - Chance auf zusätzlichen Gegenstand.\n" +
                "Empfehlung: 20-30%",

                ["Tier2_GatheringLv2_RequiredPoints"] =
                "【Benötigte Punkte - Sammeln Lv.2】\n" +
                "Empfehlung: 2",

                ["Tier2_MiningLv2_BonusChance"] =
                "【Erz +1 Bonuschance (%)】\n" +
                "Bergbau Lv.2 - Chance auf zusätzliches Erz.\n" +
                "Empfehlung: 20-30%",

                ["Tier2_MiningLv2_RequiredPoints"] =
                "【Benötigte Punkte - Bergbau Lv.2】\n" +
                "Empfehlung: 2",

                ["Tier2_CraftingLv2_UpgradeChance"] =
                "【Aufwertung +1 Chance (%)】\n" +
                "Handwerk Lv.2 - Chance auf eine zusätzliche Aufwertungsstufe.\n" +
                "Empfehlung: 20-30%",

                ["Tier2_CraftingLv2_RequiredPoints"] =
                "【Benötigte Punkte - Handwerk Lv.2】\n" +
                "Empfehlung: 2",

                ["Tier2_CraftingLv2_DurabilityBonus"] =
                "【Maximale Haltbarkeit (%)】\n" +
                "Handwerk Lv.2 - Erhöht die maximale Haltbarkeit hergestellter Gegenstände.\n" +
                "Empfehlung: 20-30%",

                ["Tier3_WoodcuttingLv3_BonusChance"] =
                "【Holz +2 Bonuschance (%)】\n" +
                "Holzfällen Lv.3 - Chance auf 2 zusätzliche Hölzer.\n" +
                "Empfehlung: 30-40%",

                ["Tier3_WoodcuttingLv3_RequiredPoints"] =
                "【Benötigte Punkte - Holzfällen Lv.3】\n" +
                "Empfehlung: 2",

                ["Tier3_GatheringLv3_BonusChance"] =
                "【Gegenstand +1 Bonuschance (%)】\n" +
                "Sammeln Lv.3 - Erhöhte Chance auf zusätzlichen Gegenstand.\n" +
                "Empfehlung: 20-30%",

                ["Tier3_GatheringLv3_RequiredPoints"] =
                "【Benötigte Punkte - Sammeln Lv.3】\n" +
                "Empfehlung: 2",

                ["Tier3_MiningLv3_BonusChance"] =
                "【Erz +1 Bonuschance (%)】\n" +
                "Bergbau Lv.3 - Erhöhte Chance auf zusätzliches Erz.\n" +
                "Empfehlung: 20-30%",

                ["Tier3_MiningLv3_RequiredPoints"] =
                "【Benötigte Punkte - Bergbau Lv.3】\n" +
                "Empfehlung: 2",

                ["Tier3_CraftingLv3_UpgradeChance"] =
                "【Aufwertung +1 Chance (%)】\n" +
                "Handwerk Lv.3 - Erhöhte Aufwertungschance.\n" +
                "Empfehlung: 20-30%",

                ["Tier3_CraftingLv3_RequiredPoints"] =
                "【Benötigte Punkte - Handwerk Lv.3】\n" +
                "Empfehlung: 2",

                ["Tier3_CraftingLv3_DurabilityBonus"] =
                "【Maximale Haltbarkeit (%)】\n" +
                "Handwerk Lv.3 - Zusätzliche Haltbarkeitssteigerung.\n" +
                "Empfehlung: 20-30%",

                ["Tier4_WoodcuttingLv4_BonusChance"] =
                "【Holz +2 Bonuschance (%)】\n" +
                "Holzfällen Lv.4 - Maximale Chance auf zusätzliches Holz.\n" +
                "Empfehlung: 40-50%",

                ["Tier4_WoodcuttingLv4_RequiredPoints"] =
                "【Benötigte Punkte - Holzfällen Lv.4】\n" +
                "Empfehlung: 2",

                ["Tier4_GatheringLv4_BonusChance"] =
                "【Gegenstand +1 Bonuschance (%)】\n" +
                "Sammeln Lv.4 - Maximale Chance auf zusätzlichen Gegenstand.\n" +
                "Empfehlung: 20-30%",

                ["Tier4_GatheringLv4_RequiredPoints"] =
                "【Benötigte Punkte - Sammeln Lv.4】\n" +
                "Empfehlung: 2",

                ["Tier4_MiningLv4_BonusChance"] =
                "【Erz +1 Bonuschance (%)】\n" +
                "Bergbau Lv.4 - Maximale Chance auf zusätzliches Erz.\n" +
                "Empfehlung: 20-30%",

                ["Tier4_MiningLv4_RequiredPoints"] =
                "【Benötigte Punkte - Bergbau Lv.4】\n" +
                "Empfehlung: 2",

                ["Tier4_CraftingLv4_UpgradeChance"] =
                "【Aufwertung +1 Chance (%)】\n" +
                "Handwerk Lv.4 - Maximale Aufwertungschance.\n" +
                "Empfehlung: 20-30%",

                ["Tier4_CraftingLv4_RequiredPoints"] =
                "【Benötigte Punkte - Handwerk Lv.4】\n" +
                "Empfehlung: 2",

                ["Tier4_CraftingLv4_DurabilityBonus"] =
                "【Maximale Haltbarkeit (%)】\n" +
                "Handwerk Lv.4 - Maximale Haltbarkeitssteigerung.\n" +
                "Empfehlung: 20-30%",

                // ========================================
                // Geschwindigkeits-Skilltree (Speed Tree)
                // ========================================
                ["Tier0_SpeedExpert_MoveSpeedBonus"] =
                "【Bewegungsgeschwindigkeitsbonus (%)】\n" +
                "Dauerhafte Erhöhung der Bewegungsgeschwindigkeit.\n" +
                "Empfehlung: 5-10%",

                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] =
                "【Geschwindigkeitsbonus nach Ausweichen (%)】\n" +
                "Erhöht kurzzeitig die Geschwindigkeit nach dem Rollen.\n" +
                "Empfehlung: 10-20%",

                ["Tier1_AgilityBase_BuffDuration"] =
                "【Effektdauer (Sek.)】\n" +
                "Dauer des Geschwindigkeitsbonus nach dem Rollen.\n" +
                "Empfehlung: 2-3 Sek.",

                ["Tier1_AgilityBase_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die allgemeine Angriffsgeschwindigkeit aller Waffen.\n" +
                "Empfehlung: 3-8%",

                ["Tier1_AgilityBase_DodgeSpeedBonus"] =
                "【Ausweichgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die Animationsgeschwindigkeit beim Rollen.\n" +
                "Empfehlung: 5-15%",
            };
        }
    }
}

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

                ["GameDifficulty"] =
                "【Spielschwierigkeit】\n" +
                "Wählt das Gesamt-Balance-Preset für das Skillbaum-Mod.\n" +
                "  Vanilla      = Milde Werte nahe am Vanilla-Valheim (Standard)\n" +
                "  VeryHard     = CLLC Sehr Schwer + Monster-HP ×2 (starke Werte)\n" +
                "  UserSettings = Stellt die zuvor gespeicherten Benutzereinstellungen wieder her\n" +
                "⚠️ Bei Änderung wird das gewählte Preset sofort angewendet (alle Skillwerte werden ersetzt).",

                ["ShowResetButtons"] =
                "【Reset-Schaltflächen anzeigen】\n" +
                "Steuert, ob die Punkte-/Job-/Produktions-Reset-Schaltflächen in der Skillbaum-UI angezeigt werden.\n" +
                "  true  = Reset-Schaltflächen anzeigen (Standard)\n" +
                "  false = Reset-Schaltflächen ausblenden (für Server, um Skill-Reset zu verhindern)",

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

                ["Tier6_WeakPointAttack_CritDamageBonus_Lv1"] = "【Schwachpunkt Lv1 Kritischer Schadensbonus (%)】\nKritischer Schadensbonus bei Stufe 1.\nEmpfehlung: 5%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv2"] = "【Schwachpunkt Lv2 Kritischer Schadensbonus (%)】\nKritischer Schadensbonus bei Stufe 2.\nEmpfehlung: 9%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv3"] = "【Schwachpunkt Lv3 Kritischer Schadensbonus (%)】\nKritischer Schadensbonus bei Stufe 3.\nEmpfehlung: 13%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv4"] = "【Schwachpunkt Lv4 Kritischer Schadensbonus (%)】\nKritischer Schadensbonus bei Stufe 4.\nEmpfehlung: 17%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv5"] = "【Schwachpunkt Lv5 Kritischer Schadensbonus (%)】\nKritischer Schadensbonus bei Stufe 5.\nEmpfehlung: 21%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv6"] = "【Schwachpunkt Lv6 Kritischer Schadensbonus (%)】\nKritischer Schadensbonus bei Stufe 6.\nEmpfehlung: 25%",
                ["Tier6_WeakPointAttack_CritDamageBonus_Lv7"] = "【Schwachpunkt Lv7 (Max.) Kritischer Schadensbonus (%)】\nKritischer Schadensbonus bei Stufe 7 (Max.).\nEmpfehlung: 29%",

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

                // ======================================== [Neues Angriffssystem: 4-Phasen]
                ["Tier1_Opener_DamageBonus"] =
                "【Erster-Schlag-Schadensbonus (%)】\n" +
                "Erhöht den Schaden in den ersten Sekunden nach Kampfbeginn.\n" +
                "Empfehlung: 15-25%",

                ["Tier1_Opener_StaminaReduction"] =
                "【Ausdauerverbrauchsreduktion (%)】\n" +
                "Reduziert den Ausdauerverbrauch während der Erster-Schlag-Phase.\n" +
                "Empfehlung: 20-30%",

                ["Tier1_Opener_Duration"] =
                "【Erster-Schlag-Dauer (Sek.)】\n" +
                "Dauer des Erster-Schlag-Effekts nach Kampfbeginn.\n" +
                "Empfehlung: 4-6 Sek.",

                ["Tier1_Opener_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur nächsten Aktivierung des Erster-Schlag-Effekts.\n" +
                "Empfehlung: 25-35 Sek.",

                ["Tier2_OpenerMelee_FinisherBonus"] =
                "【Nahkampf-Finisher-Bonus (%)】\n" +
                "Erhöht den Finisher-Multiplikator nach dem ersten Treffer im Kampf.\n" +
                "Empfehlung: 15-25%",

                ["Tier2_OpenerBow_CritChance"] =
                "【Bogen: Jägerauge - Kritischer Schaden (%)】\n" +
                "Zusätzlicher kritischer Schaden für den garantierten Krit beim ersten Pfeil im Erstschlag-Fenster.\n" +
                "Empfehlung: 6-10%",

                ["Tier2_OpenerCrossbow_FirstShotBonus"] =
                "【Armbrust-Erster-Schuss-Bonus (%)】\n" +
                "Erhöht den Schaden des ersten Armbrustbolzens nach Kampfbeginn.\n" +
                "Empfehlung: 40-60%",

                ["Tier2_OpenerMagic_StaggerProc"] =
                "【Magie-Taumel-Auslöser (0/1)】\n" +
                "Erster Magieangriff nach Kampfbeginn verursacht sicheres Taumeln.\n" +
                "0 = Deaktiviert, 1 = Aktiviert",

                ["Tier3_Pursuit_DamageBonus"] =
                "【Verfolgungsschadensbonus (%)】\n" +
                "Erhöht den Schaden gegen fliehende oder sich bewegende Feinde.\n" +
                "Empfehlung: 12-18%",

                ["Tier3_Pursuit_ChainDamageBonus"] =
                "【Verfolgungskettenbonus (%)】\n" +
                "Erhöhter Bonus, wenn Verfolgung nach Erster-Schlag-Kette aktiviert.\n" +
                "Empfehlung: 20-30%",

                ["Tier3_Pursuit_ChainWindow"] =
                "【Kettenfenster (Sek.)】\n" +
                "Zeitfenster nach Erster-Schlag für die Verfolgungskette.\n" +
                "Empfehlung: 4-6 Sek.",

                ["Tier4_PursuitSpeed_SpeedBonus"] =
                "【Bewegungsgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die Bewegungsgeschwindigkeit während des Kampfes.\n" +
                "Empfehlung: 10-15%",

                ["Tier4_FrenzyTrigger_CritChancePerLevel"] =
                "【Kritische-Trefferchance pro Stufe (%)】\n" +
                "Kritische Trefferchance, die pro Stufe hinzugefügt wird. (Stufe × Zuwachs)\n" +
                "Empfehlung: 1-3%",

                ["Tier5_Frenzy_StackBonusBase"] =
                "【Kampfgetümmel-Stack-Grundbonus (%)】\n" +
                "Schadensbonus pro Stack ohne Verfolgungskette.\n" +
                "Empfehlung: 4-6%",

                ["Tier5_Frenzy_StackBonusChain"] =
                "【Kampfgetümmel-Kettenstackbonus (%)】\n" +
                "Erhöhter Schadensbonus pro Stack mit aktiver Verfolgungskette.\n" +
                "Empfehlung: 7-10%",

                ["Tier5_Frenzy_MaxStacks"] =
                "【Maximale Stack-Anzahl】\n" +
                "Maximale Anzahl an Kampfgetümmel-Stacks.\n" +
                "Empfehlung: 4-6",

                ["Tier5_Frenzy_HitsPerStack"] =
                "【Treffer pro Stack】\n" +
                "Benötigte Trefferanzahl zum Aufbauen eines Stacks.\n" +
                "Empfehlung: 2-4",

                ["Tier5_Frenzy_Tier6Amplifier"] =
                "【Tier6-Verstärker bei Max-Stacks (×)】\n" +
                "Multiplikator für alle Tier6-Effekte bei maximaler Stack-Anzahl.\n" +
                "Empfehlung: 1,2-1,4",

                // === Neue RequiredPoints: 4-Phasen-System ===
                ["Tier1_Opener_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",
                ["Tier2_OpenerMelee_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",
                ["Tier2_OpenerBow_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",
                ["Tier2_OpenerCrossbow_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",
                ["Tier2_OpenerMagic_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",
                ["Tier3_Pursuit_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",
                ["Tier4_PursuitSpeed_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",
                ["Tier4_FrenzyTrigger_PointsPerLevel"] = "【Benötigte Punkte pro Stufe】\nFertigkeitspunkte, die pro Stufenaufstieg verbraucht werden.\nEmpfehlung: 2",
                ["Tier5_Frenzy_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

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

                ["Tier0_DefenseExpert_AtkPenalty"] =
                "【Angriffskraft-Malus (%)】\n" +
                "Das Erlernen von Verteidigungs-Experte verringert die Angriffskraft leicht.\n" +
                "Ein Kompromiss zwischen Verteidigung und Angriff.\n" +
                "Empfehlung: 1-3%",

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

                ["Tier3_BlockTraining_ParryBlockPowerRatio"] =
                "【Parry-Konter Blockstärke-Verhältnis (%)】\n" +
                "Bei Parry: Konter-Schaden = Blockstärke × Verhältnis / 100.\n" +
                "Empfehlung: 80-150%",

                ["Tier3_BlockTraining_PushDistance"] =
                "【Parry-Konter Rückstoß-Distanz (m)】\n" +
                "Distanz, um die Feinde beim Parry-Konter zurückgestoßen werden.\n" +
                "Empfehlung: 3-6m",

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
                "Empfehlung: 85%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【LP-Bonus (%)】\n" +
                "Erhöht die maximalen LP prozentual.\n" +
                "Empfehlung: 25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【Physische/Elementare Resistenz (%)】\n" +
                "Reduziert den gesamten physischen und elementaren Schaden.\n" +
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

                ["Tier3_BlockTraining_MaxChargeDistance"] =
                "【Konter-Maximalreichweite (m)】\n" +
                "Der Konter wird nur ausgelöst, wenn das taumelnde Monster sich innerhalb dieser Distanz befindet.\n" +
                "Empfehlung: 6-10m",

                ["Tier0_DefenseExpert_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier1_SkinHardening_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier2_MindTraining_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier2_HealthTraining_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier3_CoreBreathing_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier3_EvasionTraining_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier3_HealthBoost_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier3_ShieldTraining_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier4_Shockwave_Radius"] =
                "【Schockwellen-Radius】\n" +
                "Effektradius der Schockwelle-Fähigkeit in Metern.\n" +
                "Empfehlung: 3",

                ["Tier4_Shockwave_StunDuration"] =
                "【Schockwellen-Betäubungsdauer】\n" +
                "Dauer des Betäubungseffekts in Sekunden.\n" +
                "Empfehlung: 1",

                ["Tier4_Shockwave_Cooldown"] =
                "【Schockwellen-Abklingzeit】\n" +
                "Abklingzeit der Fähigkeit in Sekunden.\n" +
                "Empfehlung: 120",

                ["Tier4_Shockwave_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier4_Shockwave_KnockbackForce"] =
                "【Rückstoßkraft】\n" +
                "Kraft, die auf Feinde wirkt, wenn die Schockwelle ausgelöst wird.\n" +
                "Empfehlung: 15-25",

                ["Tier4_GroundStomp_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier4_RockSkin_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier5_Endurance_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier5_Agility_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier5_TrollRegen_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier5_BlockMaster_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier6_MindShield_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier6_MindShield_Cooldown"] =
                "【Mentalschild-Abklingzeit】\n" +
                "Abklingzeit der H-Taste-Fähigkeit Mentalschild (Sekunden).\n" +
                "Standard: 210 (3 Min. 30 Sek.)",

                ["Tier6_MindShield_EitrCost"] =
                "【Mentalschild-Eitr-Kosten】\n" +
                "Menge an Eitr, die beim Aktivieren des Mentalschilds verbraucht wird.\n" +
                "Standard: 30",

                ["Tier6_MindShield_Duration"] =
                "【Mentalschild-Dauer】\n" +
                "Dauer des Schilds (Sekunden). Absorbiert während dieser Zeit Schaden bis zum maximalen Eitr-LP-Wert.\n" +
                "Standard: 180 (3 Min.)",

                ["Tier6_NerveEnhancement_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier6_DoubleJump_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier6_JotunnVitality_RequiredPoints"] =
                "【Benötigte Punkte】\n" +
                "Fertigkeitspunkte zum Freischalten dieses Knotens.\n" +
                "Empfehlung: 2",

                ["Tier0_SpeedExpert_MoveSpeedPerLevel"] = "【Bewegungsgeschwindigkeitsbonus/Level (%)】\nErhöhung der Bewegungsgeschwindigkeit pro Level. (Wachstumssystem Lv1-7)\nEmpfehlung: 1-5%",

                ["Tier0_SpeedExpert_PointsPerLevel"] = "【Benötigte Punkte pro Level】\nFertigkeitspunkte, die pro Stufenaufstieg verbraucht werden.\nEmpfehlung: 1-2",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_2"] = "【Benötigtes Spielerlevel für Lv2】\nEpicMMO-Spielerlevel, das für den Aufstieg auf Lv2 erforderlich ist. (0=keins)\nStandard: 15",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_3"] = "【Benötigtes Spielerlevel für Lv3】\nEpicMMO-Spielerlevel, das für den Aufstieg auf Lv3 erforderlich ist.\nStandard: 30",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_4"] = "【Benötigtes Spielerlevel für Lv4】\nEpicMMO-Spielerlevel, das für den Aufstieg auf Lv4 erforderlich ist.\nStandard: 45",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_5"] = "【Benötigtes Spielerlevel für Lv5】\nEpicMMO-Spielerlevel, das für den Aufstieg auf Lv5 erforderlich ist.\nStandard: 50",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_6"] = "【Benötigtes Spielerlevel für Lv6】\nEpicMMO-Spielerlevel, das für den Aufstieg auf Lv6 erforderlich ist.\nStandard: 65",

                ["Tier0_SpeedExpert_RequiredPlayerLevel_7"] = "【Benötigtes Spielerlevel für Lv7】\nEpicMMO-Spielerlevel, das für den Aufstieg auf Lv7 erforderlich ist.\nStandard: 80",

                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "【Angriffsgeschwindigkeitsbonus bei 2 Treffern (%)】\nAngriffsgeschwindigkeit erhöht sich nach 2 aufeinanderfolgenden Nahkampftreffern.\nEmpfehlung: 8-15%",

                ["Tier2_MeleeFlow_StaminaReduction"] = "【Ausdauerreduktion (%)】\nAusdauerverbrauchsreduktion während des Flow-Buffs.\nEmpfehlung: 10-20%",

                ["Tier2_MeleeFlow_Duration"] = "【Pufferdauer (Sek.)】\nDauer des Nahkampf-Flow-Buffs.\nEmpfehlung: 3-5 Sek.",

                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "【Kombo-Geschwindigkeitsbonus (%)】\nZusätzlicher Angriffsgeschwindigkeitsbonus für Kombo-Ketten.\nEmpfehlung: 5-10%",

                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "【Bewegungsgeschwindigkeitsbonus bei Treffer (%)】\nBewegungsgeschwindigkeit erhöht sich, wenn ein Armbrustbolzen den Feind trifft.\nEmpfehlung: 10-15%",

                ["Tier2_CrossbowExpert_BuffDuration"] = "【Pufferdauer (Sek.)】\nDauer des Geschwindigkeitsbonus nach erfolgreichem Treffer.\nEmpfehlung: 3-5 Sek.",

                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "【Nachladegeschwindigkeitsbonus während Buff (%)】\nNachladegeschwindigkeit erhöht sich, während der Treffer-Buff aktiv ist.\nEmpfehlung: 10-15%",

                ["Tier2_BowExpert_StaminaReduction"] = "【Ausdauerreduktion bei 2-Treffer-Kombo (%)】\nAusdauerverbrauchsreduktion nach 2 aufeinanderfolgenden Bogenschüssen.\nEmpfehlung: 10-15%",

                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "【Zugsgeschwindigkeitsbonus für nächsten Pfeil (%)】\nZugsgeschwindigkeit für den nächsten Pfeil erhöht sich nach einer erfolgreichen Kombo.\nEmpfehlung: 10-20%",

                ["Tier2_BowExpert_BuffDuration"] = "【Pufferdauer (Sek.)】\nDauer des Kombo-Buffs.\nEmpfehlung: 4-6 Sek.",

                ["Tier2_MobileCast_MoveSpeedBonus"] = "【Bewegungsgeschwindigkeitsbonus beim Zaubern (%)】\nBewegungsgeschwindigkeitsbonus beim Wirken von Stabzaubern.\nEmpfehlung: 8-12%",

                ["Tier2_MobileCast_EitrReduction"] = "【Eitr-Kostenreduktion (%)】\nReduziert den Eitr-Verbrauch bei Stabzaubern.\nEmpfehlung: 8-15%",

                ["Tier2_MobileCast_CastMoveSpeed"] = "【Bewegungsgeschwindigkeit beim Stabzauber (%)】\nGrundbewegungsgeschwindigkeit während des Kanalisierens von Stabangriffen.\nEmpfehlung: 3-6%",

                ["Tier3_Practitioner1_MeleeSkillBonus"] = "【Nahkampfwaffen-Skillbonus】\nErhöht alle Nahkampfwaffen-Skilllevel.\nEmpfehlung: 5-10",

                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "【Armbrust-Skillbonus】\nErhöht das Armbrust-Skilllevel.\nEmpfehlung: 5-10",

                ["Tier3_Practitioner2_StaffSkillBonus"] = "【Stab-Skillbonus】\nErhöht das Stab-Skilllevel (Elementalmagi).\nEmpfehlung: 5-10",

                ["Tier3_Practitioner2_BowSkillBonus"] = "【Bogen-Skillbonus】\nErhöht das Bogen-Skilllevel.\nEmpfehlung: 5-10",

                ["Tier4_Energizer_FoodConsumptionReduction"] = "【Nahrungsverbrauchsrate-Reduktion (%)】\nVerlangsamt den Nahrungsverbrauch, sodass Buffs länger anhalten.\nEmpfehlung: 10-20%",

                ["Tier4_Captain_ShipSpeedBonus"] = "【Schiffsgeschwindigkeitsbonus (%)】\nErhöht die Segelgeschwindigkeit.\nEmpfehlung: 10-20%",

                ["Tier5_JumpMaster_JumpSkillBonus"] = "【Sprung-Skillbonus】\nErhöht das Sprung-Skilllevel.\nEmpfehlung: 5-15",

                ["Tier5_JumpMaster_JumpStaminaReduction"] = "【Sprung-Ausdauerreduktion (%)】\nReduziert den Ausdauerverbrauch beim Springen.\nEmpfehlung: 10-20%",

                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "【Nahkampf-Angriffsgeschwindigkeitsbonus (%)】\nErhöht die Nahkampf-Angriffsgeschwindigkeit.\nEmpfehlung: 5-8%",

                ["Tier6_Dexterity_MoveSpeedBonus"] = "【Bewegungsgeschwindigkeitsbonus (%)】\nErhöht die allgemeine Bewegungsgeschwindigkeit.\nEmpfehlung: 3-8%",

                ["Tier6_Endurance_StaminaMaxBonus"] = "【Maximaler Ausdauerbonus】\nErhöht den maximalen Ausdauerpool.\nEmpfehlung: 20-40",

                ["Tier6_Intellect_EitrMaxBonus"] = "【Maximaler Eitr-Bonus】\nErhöht den maximalen Eitr-Pool für Magie.\nEmpfehlung: 30-50",

                ["Tier7_Master_RunSkillBonus"] = "【Lauf-Skillbonus】\nErhöht das Lauf-Skilllevel.\nEmpfehlung: 5-15",

                ["Tier7_Master_JumpSkillBonus"] = "【Sprung-Skillbonus】\nErhöht das Sprung-Skilllevel.\nEmpfehlung: 5-15",

                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "【Nahkampf-Angriffsgeschwindigkeitsbonus (%)】\nFinale Steigerung der Nahkampf-Angriffsgeschwindigkeit.\nEmpfehlung: 5-10%",

                ["Tier8_MeleeAccel_TripleComboBonus"] = "【Angriffsgeschwindigkeitsbonus für nächsten Angriff bei 3-Treffer-Kombo (%)】\nMassiver Angriffsgeschwindigkeitsbonus für den nächsten Angriff nach einer 3-Treffer-Kombo.\nEmpfehlung: 20-30%",

                ["Tier8_CrossbowAccel_ReloadSpeed"] = "【Nachladegeschwindigkeitsbonus (%)】\nFinale Steigerung der Armbrust-Nachladegeschwindigkeit.\nEmpfehlung: 25-35%",

                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "【Bewegungsgeschwindigkeit beim Nachladen (%)】\nBewegungsgeschwindigkeit beim Nachladen der Armbrust.\nEmpfehlung: 20-30%",

                ["Tier8_BowAccel_DrawSpeed"] = "【Zugsgeschwindigkeitsbonus (%)】\nFinale Steigerung der Bogen-Zugsgeschwindigkeit.\nEmpfehlung: 15-20%",

                ["Tier8_BowAccel_DrawMoveSpeed"] = "【Bewegungsgeschwindigkeit beim Spannen (%)】\nBewegungsgeschwindigkeit beim Spannen der Bogensehne.\nEmpfehlung: 10-20%",

                ["Tier8_CastAccel_MagicAttackSpeed"] = "【Magie-Angriffsgeschwindigkeitsbonus (%)】\nFinale Steigerung der Magie-Angriffsgeschwindigkeit.\nEmpfehlung: 5-10%",

                ["Tier8_CastAccel_TripleEitrRecovery"] = "【Eitr-Max-Regenerationsrate bei 3-Treffer-Kombo (%)】\nErhöht die Eitr-Regenerationsrate nach einer 3-Zauber-Kombo.\nEmpfehlung: 10-15%",

                ["Tier1_AgilityBase_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_MeleeFlow_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_CrossbowExpert_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_BowExpert_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_MobileCast_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier3_Practitioner1_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier3_Practitioner2_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier4_Energizer_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier4_Captain_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier5_JumpMaster_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier6_Dexterity_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier6_Endurance_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier6_Intellect_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier7_Master_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier8_MeleeAccel_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier8_CrossbowAccel_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier8_BowAccel_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier8_CastAccel_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier0_AttackExpert_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier1_BaseAttack_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_MeleeSpec_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_BowSpec_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_CrossbowSpec_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier2_StaffSpec_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier3_AttackBoost_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier4_MeleeEnhance_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier4_PrecisionAttack_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier4_RangedEnhance_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier5_SpecialStat_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier6_WeakPointAttack_PointsPerLevel"] = "【Benötigte Punkte pro Stufe】\nFertigkeitspunkte, die pro Stufenaufstieg verbraucht werden.\nEmpfehlung: 2",

                ["Tier6_ComboFinisher_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier6_TwoHandCrush_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

                ["Tier6_ElementalAttack_RequiredPoints"] = "【Benötigte Punkte】\nFertigkeitspunkte zum Freischalten dieses Knotens.",

            };
        }
    }
}

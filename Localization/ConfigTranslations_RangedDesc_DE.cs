using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetRangedDescriptions_DE()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Stab-Skilltree (Staff Tree)
                // ========================================

                // === Tier 0: Stab-Experte ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【Elementarschadensbonus (%)】\n" +
                "Erhöht den Elementarschaden des Stabs (Feuer, Eis, Blitz).\n" +
                "Grundlage für magische Angriffe.\n" +
                "Empfehlung: 3-7%",

                ["Tier0_StaffExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Stab-Experten.",

                // === Tier 1: Geistesfokus & Magiefluss ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【Eitr-Kostenreduzierung (%)】\n" +
                "Geistesfokus reduziert die Eitr-Kosten von Zaubern.\n" +
                "Ermöglicht häufigere Magienutzung.\n" +
                "Empfehlung: 12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【Maximaler Eitr-Bonus (Fest)】\n" +
                "Magiefluss erhöht den maximalen Eitr.\n" +
                "Empfehlung: 25-35",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Geistesfokus.",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Magieflusses.",

                // === Tier 2: Magieverstärkung ===
                ["Tier2_MagicAmplify_Chance"] =
                "【Magieverstärkungschance (%)】\n" +
                "Chance auf Verstärkung des Elementarschadens.\n" +
                "Empfehlung: 30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【Elementarschadensbonus bei Verstärkung (%)】\n" +
                "Schadenserhöhung bei Aktivierung.\n" +
                "Empfehlung: 30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【Eitr-Kostenerhöhung (%)】\n" +
                "Erhöht die Eitr-Kosten beim Zaubern.\n" +
                "Der Preis mächtiger Magie.\n" +
                "Empfehlung: 15-25%",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Magieverstärkung.",

                // === Tier 3: Element ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【Eisschadensbonus (Fest)】\n" +
                "Erhöht den Schaden von Eismagie.\n" +
                "Empfehlung: 2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【Feuerschadensbonus (Fest)】\n" +
                "Erhöht den Schaden von Feuermagie.\n" +
                "Empfehlung: 2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【Blitzschadensbonus (Fest)】\n" +
                "Erhöht den Schaden von Blitzmagie.\n" +
                "Empfehlung: 2-5",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von Element Eis.",

                ["Tier3_FireElement_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von Element Feuer.",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von Element Blitz.",

                // === Tier 4: Managlück ===
                ["Tier4_LuckyMana_Chance"] =
                "【Chance auf kostenlosen Zauber (%)】\n" +
                "Chance, einen Zauber ohne Eitr-Verbrauch zu wirken.\n" +
                "Managlück öffnet den Weg zur unbegrenzten Magie.\n" +
                "Empfehlung: 30-40%",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von Managlück.",

                // === Tier 5-1: Doppelwurf (Aktiv T) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【Zusätzliche Projektile】\n" +
                "Anzahl der zusätzlichen magischen Projektile.\n" +
                "Empfehlung: 5-10",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【Projektilschaden (%)】\n" +
                "Schadensprozentsatz der zusätzlichen Projektile.\n" +
                "Empfehlung: 10-20%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【Streuwinkel (nicht verwendet)】\n" +
                "In der aktuellen Version nicht verwendet. Feste Richtung.",

                ["Tier5_DoubleCast_EitrCost"] =
                "【Eitr-Kosten】\n" +
                "Verbrauchter Eitr bei Fähigkeitsaktivierung.\n" +
                "Empfehlung: 15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 25-40 Sek.",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Doppelwurfs.",

                // === Tier 5-2: Sofortige Flächenheilung (Aktiv H) ===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 25-40 Sek.",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【Eitr-Kosten】\n" +
                "Verbrauchter Eitr bei Fähigkeitsnutzung.\n" +
                "Empfehlung: 25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【Heilungsmenge (% der max. LP)】\n" +
                "Prozentsatz der maximalen LP, der wiederhergestellt wird.\n" +
                "Empfehlung: 20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【Heilungsradius (Meter)】\n" +
                "Bereich, in dem die Heilung angewendet wird.\n" +
                "Empfehlung: 10-15 m",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Flächenheilung.",

                // ========================================
                // Armbrust-Skilltree (Crossbow Tree)
                // ========================================

                // === Tier 0: Armbrust-Experte ===
                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Armbrust-Experten.",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【Armbrustschadensbonus (%)】\n" +
                "Erhöht den Grundschaden von Armbrüsten und Bolzen.\n" +
                "Empfehlung: 8-12%",

                // === Tier 1: Schnellschuss ===
                ["Tier1_RapidFire_Chance"] =
                "【Schnellschuss-Chance (%)】\n" +
                "Chance auf Schnellschuss beim Schießen.\n" +
                "Schießt bei Aktivierung mehrere Bolzen schnell ab.\n" +
                "Empfehlung: 15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【Schnellschuss-Schussanzahl】\n" +
                "Anzahl der zusätzlichen Bolzen beim Schnellschuss.\n" +
                "Empfehlung: 2-4 Schüsse",

                ["Tier1_RapidFire_DamagePercent"] =
                "【Schnellschuss-Schaden (%)】\n" +
                "Schadensprozentsatz der Schnellschuss-Bolzen.\n" +
                "Im Verhältnis zum Grundangriff.\n" +
                "Empfehlung: 60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【Schnellschuss-Intervall (Sek.)】\n" +
                "Zeit zwischen den Schüssen beim Schnellschuss.\n" +
                "Weniger = schneller.\n" +
                "Empfehlung: 0,1-0,3 Sek.",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【Bolzenverbrauch beim Schnellschuss】\n" +
                "Anzahl der verbrauchten Bolzen pro Schnellschuss.\n" +
                "Empfehlung: 1-2",

                ["Tier1_RapidFire_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Schnellschusses.",

                // === Tier 2: Ausgewogene Zielerfassung ===
                ["Tier2_BalancedAim_KnockbackChance"] =
                "【Rückstoßchance (%)】\n" +
                "Chance auf Rückstoß bei Treffern mit der Armbrust.\n" +
                "Stabile Haltung garantiert einen zuverlässigen Treffer.\n" +
                "Empfehlung: 20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【Rückstoßdistanz (Meter)】\n" +
                "Wie viele Meter der Feind bei Aktivierung zurückgestoßen wird.\n" +
                "Empfehlung: 2-4 m",

                ["Tier2_BalancedAim_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der ausgewogenen Zielerfassung.",

                // === Tier 2: Schnelles Nachladen ===
                ["Tier2_RapidReload_SpeedIncrease"] =
                "【Nachladgeschwindigkeitserhöhung (%)】\n" +
                "Erhöht die Nachladgeschwindigkeit der Armbrust.\n" +
                "Lädt den nächsten Bolzen schneller.\n" +
                "Empfehlung: 10-20%",

                ["Tier2_RapidReload_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des schnellen Nachladens.",

                // === Tier 2: Ehrlicher Schuss ===
                ["Tier2_HonestShot_DamageBonus"] =
                "【Grundschadensbonus (%)】\n" +
                "Erhöht zusätzlich den Grundangriff der Armbrust.\n" +
                "Empfehlung: 10-18%",

                ["Tier2_HonestShot_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des ehrlichen Schusses.",

                // === Tier 3: Automatisches Nachladen ===
                ["Tier3_AutoReload_Chance"] =
                "【Automatisches Nachladen Chance (%)】\n" +
                "Chance, das nächste Nachladen bei Treffern mit 200% Geschwindigkeit durchzuführen.\n" +
                "Hilft, ein kontinuierliches Angriffstempo aufrechtzuerhalten.\n" +
                "Empfehlung: 20-35%",

                ["Tier3_AutoReload_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des automatischen Nachladens.",

                // === Tier 4: Schnellschuss Lv.2 ===
                ["Tier4_RapidFireLv2_Chance"] =
                "【Schnellschuss Lv.2 Chance (%)】\n" +
                "Chance auf verbesserten Schnellschuss.\n" +
                "Addiert sich zur Chance von Schnellschuss Lv.1.\n" +
                "Empfehlung: 20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【Schussanzahl Lv.2】\n" +
                "Anzahl der zusätzlichen Bolzen beim verbesserten Schnellschuss.\n" +
                "Mehr als bei Schnellschuss Lv.1.\n" +
                "Empfehlung: 4-6 Schüsse",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【Schnellschuss Lv.2 Schaden (%)】\n" +
                "Schadensprozentsatz des verbesserten Schnellschusses.\n" +
                "Mehr Schaden als Lv.1.\n" +
                "Empfehlung: 75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【Schnellschuss Lv.2 Intervall (Sek.)】\n" +
                "Zeit zwischen den Schüssen des verbesserten Schnellschusses.\n" +
                "Empfehlung: 0,1-0,3 Sek.",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【Bolzenverbrauch Lv.2】\n" +
                "Bolzenanzahl beim verbesserten Schnellschuss.\n" +
                "Empfehlung: 1-2",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von Schnellschuss Lv.2.",

                // === Tier 4: Finaler Schlag ===
                ["Tier4_FinalStrike_HpThreshold"] =
                "【Feind-LP-Schwellenwert (%)】\n" +
                "Verursacht Bonusschaden an Feinden mit LP über diesem Schwellenwert.\n" +
                "Wirksam gegen Ziele mit vielen LP.\n" +
                "Empfehlung: 40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【Bonusschaden (%)】\n" +
                "Zusätzlicher Schaden an Feinden über dem LP-Schwellenwert.\n" +
                "Empfehlung: 20-40%",

                ["Tier4_FinalStrike_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des finalen Schlags.",

                // === Tier 5: Ein Schuss (Aktiv T) ===
                ["Tier5_OneShot_Duration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wie lange der 'Ein Schuss'-Buff anhält.\n" +
                "In dieser Zeit kann ein mächtiger Schuss abgefeuert werden.\n" +
                "Empfehlung: 8-12 Sek.",

                ["Tier5_OneShot_DamageBonus"] =
                "【'Ein Schuss'-Schadensbonus (%)】\n" +
                "Zusätzlicher Schaden beim Abfeuern von 'Ein Schuss'.\n" +
                "Die Kraft eines verheerenden Schusses.\n" +
                "Empfehlung: 150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【Rückstoßdistanz (Meter)】\n" +
                "Wie viele Meter der Feind bei Treffern zurückgestoßen wird.\n" +
                "Der mächtige Aufprall schleudert Feinde zurück.\n" +
                "Empfehlung: 5-10 m",

                ["Tier5_OneShot_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 25-40 Sek.",

                ["Tier5_OneShot_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von 'Ein Schuss'.",

                // ========================================
                // Bogen-Skilltree (Bow Tree)
                // ========================================

                // === Tier 0: Bogen-Experte ===
                ["Tier0_BowExpert_DamageBonus"] =
                "【Bogenschadensbonus (%)】\n" +
                "Erhöht den Grundschaden von Bögen und Pfeilen.\n" +
                "Empfehlung: 8-12%",

                ["Tier0_BowExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Bogen-Experten.",

                // === Tier 1-1: Fokussierter Schuss ===
                ["Tier1_FocusedShot_CritBonus"] =
                "【Kritische Trefferchance-Bonus (%)】\n" +
                "Fokussierter Schuss erhöht die kritische Trefferchance.\n" +
                "Mehr Konzentration = höhere Kritchance.\n" +
                "Empfehlung: 5-10%",

                ["Tier1_FocusedShot_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des fokussierten Schusses.",

                // === Tier 1-2: Mehrfachschuss Lv.1 ===
                ["Tier1_MultishotLv1_ActivationChance"] =
                "【Mehrfachschuss Lv.1 Chance (%)】\n" +
                "Chance auf Mehrfachschuss beim Schießen mit dem Bogen.\n" +
                "Bei Aktivierung werden mehrere Pfeile gleichzeitig abgefeuert.\n" +
                "Empfehlung: 15-25%",

                ["Tier1_MultishotLv1_AdditionalArrows"] =
                "【Anzahl zusätzlicher Pfeile】\n" +
                "Anzahl der zusätzlichen Pfeile beim Mehrfachschuss.\n" +
                "Empfehlung: 2-4",

                ["Tier1_MultishotLv1_ArrowConsumption"] =
                "【Pfeilverbrauch】\n" +
                "Anzahl der verbrauchten Pfeile beim Mehrfachschuss.\n" +
                "Empfehlung: 1-2",

                ["Tier1_MultishotLv1_DamagePerArrow"] =
                "【Schaden pro Pfeil (%)】\n" +
                "Schadensprozentsatz jedes Pfeils beim Mehrfachschuss.\n" +
                "Empfehlung: 50-70%",

                ["Tier1_MultishotLv1_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von Mehrfachschuss Lv.1.",

                // === Tier 2: Bogenmeisterschaft ===
                ["Tier2_BowMastery_SkillBonus"] =
                "【Bogenfähigkeitsbonus (Fest)】\n" +
                "Erhöht den Bogenfähigkeitslevel.\n" +
                "Höhere Meisterschaft = stärkere Angriffe.\n" +
                "Empfehlung: 5-10",

                ["Tier2_BowMastery_SpecialArrowChance"] =
                "【Chance auf Spezialpfeil (%)】\n" +
                "Chance auf einen Pfeil mit Spezialeffekt.\n" +
                "Kann Gift, Feuer, Eis und andere Zustände anwenden.\n" +
                "Empfehlung: 25-35%",

                ["Tier2_BowMastery_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Bogenmeisterschaft.",

                // === Tier 3-1: Stiller Treffer ===
                ["Tier3_SilentStrike_DamageBonus"] =
                "【Stiller Treffer Schadensbonus (Fest)】\n" +
                "Erhöht den Bogenschaden um einen festen Wert.\n" +
                "Pfeile durchbohren Feinde für mehr Schaden.\n" +
                "Empfehlung: 3-8",

                ["Tier3_SilentStrike_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des stillen Treffers.",

                // === Tier 3-2: Mehrfachschuss Lv.2 ===
                ["Tier3_MultishotLv2_ActivationChance"] =
                "【Mehrfachschuss Lv.2 Chance (%)】\n" +
                "Verbesserte Mehrfachschuss-Chance.\n" +
                "Feuert mehr Pfeile als Lv.1.\n" +
                "Empfehlung: 20-30%",

                ["Tier3_MultishotLv2_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von Mehrfachschuss Lv.2.",

                // === Tier 3-3: Jägerinstinkt ===
                ["Tier3_HuntingInstinct_CritBonus"] =
                "【Jägerinstinkt Kritische Trefferchance-Bonus (%)】\n" +
                "Jägerinstinkt erhöht die kritische Trefferchance.\n" +
                "Empfehlung: 8-15%",

                ["Tier3_HuntingInstinct_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Jägerinstinkts.",

                // === Tier 4: Präzisionszielen ===
                ["Tier4_PrecisionAim_CritDamage"] =
                "【Kritischer Schadensbonus (%)】\n" +
                "Präzisionszielen erhöht den Schaden kritischer Treffer.\n" +
                "Schwachstellen treffen für enormen Schaden.\n" +
                "Empfehlung: 25-40%",

                ["Tier4_PrecisionAim_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Präzisionszielens.",

                // === Tier 5: Explosivpfeil (Aktiv T) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] =
                "【Explosivpfeil-Schaden (%)】\n" +
                "Aktiv T — Schadenmultiplikator des Explosivpfeils.\n" +
                "Mächtiger Pfeil mit Flächenschaden.\n" +
                "Empfehlung: 100-150%",

                ["Tier5_ExplosiveArrow_Radius"] =
                "【Explosionsradius (Meter)】\n" +
                "Schadensradius des Explosivpfeils.\n" +
                "Empfehlung: 3-6 m",

                ["Tier5_ExplosiveArrow_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 15-25 Sek.",

                ["Tier5_ExplosiveArrow_StaminaCost"] =
                "【Ausdauerkosten (%)】\n" +
                "Prozentsatz der Ausdauer bei Fähigkeitsnutzung.\n" +
                "Empfehlung: 10-20%",

                ["Tier5_ExplosiveArrow_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Explosivpfeils.",
            };
        }
    }
}

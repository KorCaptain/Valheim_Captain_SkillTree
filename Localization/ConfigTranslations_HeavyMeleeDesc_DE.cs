using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetHeavyMeleeDescriptions_DE()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Speer-Skilltree (Spear Tree)
                // ========================================

                // === Tier 0: Speer-Experte ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Speer-Experten.",

                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【Geschwindigkeitsbonus nach 2 Treffern (%)】\n" +
                "Die Angriffsgeschwindigkeit erhöht sich nach 2 aufeinanderfolgenden Speerstichen.\n" +
                "Überwältige Feinde mit schnellen Kombos.\n" +
                "Empfehlung: 10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【Schadensbonus nach 2 Treffern (%)】\n" +
                "Der Schaden erhöht sich nach 2 aufeinanderfolgenden Speerstichen.\n" +
                "Maximiert den Komboschadensangriff.\n" +
                "Empfehlung: 7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Effekts nach 2 Treffern.\n" +
                "Längere Dauer = stabiler Kampf.\n" +
                "Empfehlung: 4-8 Sek.",

                // === Tier 1: Präzisionsschlag ===
                ["Tier1_QuickStrike_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Präzisionsschlags.",

                ["Tier1_VitalStrike_DamageBonus"] =
                "【Kritischer Schadensbonus (%)】\n" +
                "Erhöht den Schaden kritischer Treffer mit dem Speer.\n" +
                "Spezialisierung auf präzise Treffer an Schwachstellen.\n" +
                "Empfehlung: 20-40%",

                // === Tier 2: Speerwurf ===
                ["Tier2_Throw_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Speerwurfs.",

                ["Tier2_Throw_Cooldown"] =
                "【Speerwurf-Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zum erneuten Speerwurf.\n" +
                "Weniger = häufigere Nutzung möglich.\n" +
                "Empfehlung: 20-40 Sek.",

                ["Tier2_Throw_DamageMultiplier"] =
                "【Speerwurf-Schadensmultiplikator (%)】\n" +
                "Schadensmultiplikator des geworfenen Speers.\n" +
                "Bestimmt die Stärke des Fernkampfangriffs.\n" +
                "Empfehlung: 100-150%",

                ["Legacy_Throw_BuffDuration"] =
                "【Nicht verwendet】\n" +
                "Dieser Parameter wird derzeit nicht verwendet.\n" +
                "Zu passiver Fähigkeit geändert.",

                // === Tier 3: Schnellspeer ===
                ["Tier3_Pierce_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Schnellspeers.",

                ["Tier3_Rapid_DamageBonus"] =
                "【Waffen-Angriffsbonus (Fest)】\n" +
                "Erhöht den Grundangriff des Speers um einen festen Wert.\n" +
                "Vorteilhaft für schnelle aufeinanderfolgende Angriffe.\n" +
                "Empfehlung: 3-6",

                ["Tier3_QuickSpear_AttackSpeed"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die Angriffsgeschwindigkeit bei Speer- oder Stangenwaffe.\n" +
                "Empfehlung: 15-25%",

                // === Tier 4: Ausweichschlag ===
                ["Tier4_Evasion_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Ausweichschlags.",

                ["Tier4_Evasion_EvasionBonus"] =
                "【Ausweichbonus beim Angriff (%)】\n" +
                "Beim Speerstich erhöht sich die Ausweichance für 5 Sekunden.\n" +
                "Verbessert das Überleben im aggressiven Kampfstil.\n" +
                "Empfehlung: 15-25%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【Ausdauerverbrauch beim Angriff (%)】\n" +
                "Der Ausdauerverbrauch der Ausweichschlag-Angriffe verringert sich.\n" +
                "Ermöglicht längeres Kämpfen.\n" +
                "Empfehlung: 5-15%",

                // === Tier 4: Doppelschlag ===
                ["Tier4_Dual_DamageBonus"] =
                "【Doppelschlag-Schadensbonus (%)】\n" +
                "Zusätzlicher Schaden bei zwei aufeinanderfolgenden Angriffen.\n" +
                "Spezialisierung auf Komboschadensangriffe.\n" +
                "Empfehlung: 18-30%",

                ["Tier4_Dual_Duration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Doppelschlag-Buffs.\n" +
                "Längere Dauer = stabiler Schaden.\n" +
                "Empfehlung: 8-15 Sek.",

                // === Tier 5: Durchbohrspeer (Aktiv G) ===
                ["Tier5_Penetrate_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Durchbohrspeers.",

                ["Legacy_Penetrate_CritChance"] =
                "【Nicht verwendet】\n" +
                "Dieser Parameter wird derzeit nicht verwendet.\n" +
                "Zu Blitztreffereffekt geändert.",

                ["Tier5_Penetrate_BuffDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Durchbohrspeer-Buffs.\n" +
                "Bestimmt die Effektdauer der Fähigkeit.\n" +
                "Empfehlung: 25-40 Sek.",

                ["Tier5_Penetrate_LightningDamage"] =
                "【Blitzschadensmultiplikator (%)】\n" +
                "Blitzschadensmultiplikator bei Komboangriffen.\n" +
                "Verursacht mächtigen Zusatzschaden.\n" +
                "Empfehlung: 200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【Benötigte Treffer für Blitz】\n" +
                "Anzahl aufeinanderfolgender Treffer zum Auslösen des Blitzes.\n" +
                "Weniger = häufigeres Auslösen.\n" +
                "Empfehlung: 3-5 Treffer",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【G-Taste Fähigkeitsabklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung der G-Fähigkeit.\n" +
                "Weniger = häufigere Nutzung möglich.\n" +
                "Empfehlung: 50-80 Sek.",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【G-Taste Fähigkeitsausdauerkosten】\n" +
                "Ausdauer bei Nutzung der G-Fähigkeit.\n" +
                "Ausdauermanagement ist wichtig.\n" +
                "Empfehlung: 20-35",

                // === Tier 5: Speerkombo (Aktiv H) ===
                ["Tier5_Combo_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Speerkombo.",

                ["Tier5_Combo_HKey_Cooldown"] =
                "【H-Taste Fähigkeitsabklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung der H-Fähigkeit.\n" +
                "Weniger = häufigere Nutzung möglich.\n" +
                "Empfehlung: 20-35 Sek.",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【H-Taste Fähigkeitsschadensmultiplikator (%)】\n" +
                "Schadensmultiplikator des Speerkombo-Angriffs (H).\n" +
                "Mächtige Einzeltrefferfähigkeit.\n" +
                "Empfehlung: 250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【H-Taste Fähigkeitsausdauerkosten】\n" +
                "Ausdauer bei Nutzung der H-Fähigkeit.\n" +
                "Ausdauermanagement ist notwendig.\n" +
                "Empfehlung: 15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【H-Taste Rückstoßdistanz (m)】\n" +
                "Distanz, um die der Feind beim H-Fähigkeitstreffer zurückgestoßen wird.\n" +
                "Nützlich zur Positionsanpassung im Kampf.\n" +
                "Empfehlung: 2-5 m",

                ["Tier5_Combo_ActiveRange"] =
                "【Aktiver Effektradius (m)】\n" +
                "Radius, in dem der Speerkombo-Buff aktiviert wird.\n" +
                "Mehr = Aktivierung in mehr Situationen.\n" +
                "Empfehlung: 2-5 m",

                ["Tier5_Combo_BuffDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Speerkombo-Buffs.\n" +
                "Längere Dauer = stabiler Wurfverstärkung.\n" +
                "Empfehlung: 25-40 Sek.",

                ["Tier5_Combo_MaxUses"] =
                "【Max. verstärkte Würfe】\n" +
                "Maximale Anzahl verstärkter Würfe während des Buffs.\n" +
                "Mehr = Verstärkung dauert länger.\n" +
                "Empfehlung: 2-5 Mal",

                // ========================================
                // Streitkolben-Skilltree (Mace Tree)
                // ========================================

                // === Tier 0: Streitkolben-Experte ===
                ["Tier0_MaceExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Streitkolben-Experten.",

                ["Tier0_MaceExpert_DamageBonus"] =
                "【Streitkolbenschadensbonus (%)】\n" +
                "Erhöht den Grundangriff von Schlaggeräten.\n" +
                "Gilt für alle Streitkolbentypen.\n" +
                "Empfehlung: 5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【Betäubungschance (%)】\n" +
                "Chance, den Feind beim Streitkolbenschlag zu betäuben.\n" +
                "Betäubte Feinde sind handlungsunfähig.\n" +
                "Empfehlung: 15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【Betäubungsdauer (Sek.)】\n" +
                "Wirkungsdauer des Betäubungseffekts.\n" +
                "Längere Dauer = mehr Zeit für Schaden.\n" +
                "Empfehlung: 0,3-1 Sek.",

                // === Tier 1: Streitkolbenverstärkung ===
                ["Tier1_MaceExpert_DamageBonus"] =
                "【Streitkolben-Angriffsbonus (%)】\n" +
                "Zusätzlicher Angriffsbonus für Schlaggeräte.\n" +
                "Empfehlung: 8-15%",

                ["Tier1_MaceExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Streitkolbenverstärkung.",

                // === Tier 2: Betäubungsverstärkung ===
                ["Tier2_StunBoost_StunChanceBonus"] =
                "【Betäubungschance-Bonus (%)】\n" +
                "Erhöht zusätzlich die Betäubungschance.\n" +
                "Addiert sich zum Streitkolben-Experten-Bonus.\n" +
                "Empfehlung: 10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【Betäubungsdauer-Bonus (Sek.)】\n" +
                "Erhöht zusätzlich die Betäubungszeit.\n" +
                "Mehr Zeit für Schadenszufügung.\n" +
                "Empfehlung: 0,3-0,8 Sek.",

                ["Tier2_StunBoost_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Betäubungsverstärkung.",

                // === Tier 3: Schild ===
                ["Tier3_Guard_ArmorBonus"] =
                "【Rüstungsbonus (Fest)】\n" +
                "Erhöht die Grundrüstung um einen festen Wert.\n" +
                "Nützlich für Tank-Builds.\n" +
                "Empfehlung: 2-5",

                ["Tier3_Guard_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Schilds.",

                // === Tier 3: Schwerer Schlag ===
                ["Tier3_HeavyStrike_DamageBonus"] =
                "【Aufprallschadensbonus (Fest)】\n" +
                "Erhöht den Aufprallschaden des Streitkolbens um einen festen Wert.\n" +
                "Addiert sich zum prozentualen Bonus.\n" +
                "Empfehlung: 2-5",

                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des schweren Schlags.",

                // === Tier 4: Rückstoß ===
                ["Tier4_Push_KnockbackChance"] =
                "【Rückstoßchance (%)】\n" +
                "Chance, den Feind beim Angriff zurückzustoßen.\n" +
                "Nützlich zum Abstandhalten und Schlachtkontrolle.\n" +
                "Empfehlung: 25-35%",

                ["Tier4_Push_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Rückstoßes.",

                // === Tier 5: Tanker ===
                ["Tier5_Tank_HealthBonus"] =
                "【LP-Bonus (%)】\n" +
                "Erhöht die maximalen LP.\n" +
                "Wesentlich zur Überlebensstärkung.\n" +
                "Empfehlung: 20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【Eingehende Schadensreduzierung (%)】\n" +
                "Reduziert den gesamten eingehenden Schaden.\n" +
                "In Kombination mit Rüstung — ideal für die Tankerrolle.\n" +
                "Empfehlung: 8-15%",

                ["Tier5_Tank_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Tankers.",

                // === Tier 5: Schaden (DPS) ===
                ["Tier5_DPS_DamageBonus"] =
                "【Angriffsbonus (%)】\n" +
                "Erhöht zusätzlich den Streitkolbenangriff.\n" +
                "Nützlich für DPS-Builds.\n" +
                "Empfehlung: 15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die Angriffsgeschwindigkeit mit dem Streitkolben.\n" +
                "Kompensiert die Langsamkeit schwerer Waffen.\n" +
                "Empfehlung: 8-15%",

                ["Tier5_DPS_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von DPS.",

                // === Tier 6: Großmeister ===
                ["Tier6_Grandmaster_ArmorBonus"] =
                "【Rüstungsbonus (%)】\n" +
                "Prozentualer Rüstungsbonus.\n" +
                "Gute Synergie mit hochrangiger Rüstung.\n" +
                "Empfehlung: 15-25%",

                ["Tier6_Grandmaster_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Großmeisters.",

                // === Tier 7: Wuthammer (Aktiv H) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【Schadensmultiplikator Treffer 1-4 (%)】\n" +
                "Schadensmultiplikator der Treffer 1-4 der 'Wuthammer' Fähigkeit (H).\n" +
                "Auf den aktuellen Angriff angewendet.\n" +
                "Empfehlung: 70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【Schadensmultiplikator des Abschlusstreffers (%)】\n" +
                "Schadensmultiplikator des Abschlusstreffers der 'Wuthammer' Fähigkeit (H).\n" +
                "Der mächtigste Treffer ist der letzte.\n" +
                "Empfehlung: 130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Nutzung der H-Fähigkeit.\n" +
                "Ausdauermanagement ist wichtig.\n" +
                "Empfehlung: 35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Weniger = häufigere Nutzung möglich.\n" +
                "Empfehlung: 25-35 Sek.",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【AOE-Radius (Meter)】\n" +
                "Flächenschadensradius der Fähigkeit.\n" +
                "Größer = mehr Feinde getroffen.\n" +
                "Empfehlung: 4-7 m",

                ["Tier7_FuryHammer_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Wuthammers.",

                // === Tier 7: Wächterherz (Aktiv G) ===
                ["Tier7_GuardianHeart_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung der G-Fähigkeit.\n" +
                "Weniger = häufigere Defensivhaltung möglich.\n" +
                "Empfehlung: 100-140 Sek.",

                ["Tier7_GuardianHeart_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Fähigkeitsnutzung.\n" +
                "Als Tanker ist Ausdauermanagement wichtig.\n" +
                "Empfehlung: 20-30",

                ["Tier7_GuardianHeart_Duration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Dauer der Defensivhaltung.\n" +
                "In dieser Zeit reflektierst du Schaden und hast hohe Verteidigung.\n" +
                "Empfehlung: 40-50 Sek.",

                ["Tier7_GuardianHeart_ReflectPercent"] =
                "【Schadensreflexionsprozentsatz (%)】\n" +
                "Prozentsatz des eingehenden Schadens, der zum Angreifer reflektiert wird.\n" +
                "Als Tanker den Schaden an den Feind zurückgeben.\n" +
                "Empfehlung: 5-8%",

                ["Tier7_GuardianHeart_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Wächterherzes.",

                // ========================================
                // Stangenwaffen-Skilltree (Polearm Tree)
                // ========================================

                // === Tier 0: Stangenwaffen-Experte ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【Angriffsreichweite-Bonus (%)】\n" +
                "Erhöht die Angriffsreichweite von Stangenwaffen.\n" +
                "Die große Reichweite ermöglicht Angriffe aus sicherer Distanz.\n" +
                "Empfehlung: 10-20%",

                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Stangenwaffen-Experten.",

                // === Tier 1: Drehrad ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【Drehschadensbonus (%)】\n" +
                "Zusätzlicher Schaden beim rotierenden Angriff.\n" +
                "Nützlich gegen mehrere Feinde.\n" +
                "Empfehlung: 50-80%",

                ["Tier1_SpinWheel_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Drehrades.",

                // === Tier 2-1: Stangenwaffenverstärkung ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] =
                "【Waffen-Angriffsbonus (Fest)】\n" +
                "Erhöht den Grundangriff der Stangenwaffe um einen festen Wert.\n" +
                "Gilt für alle Stangenwaffenangriffe.\n" +
                "Empfehlung: 4-7",

                ["Tier2-1_PolearmBoost_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Stangenwaffenverstärkung.",

                // === Tier 2-2: Heldenschlag ===
                ["Tier2-2_HeroStrike_KnockbackChance"] =
                "【Destabilisierungschance (%)】\n" +
                "Chance, den Feind beim Angriff zu destabilisieren.\n" +
                "Nützlich zur Schlachtkontrolle.\n" +
                "Empfehlung: 20-35%",

                ["Tier2-2_HeroStrike_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Heldenschlags.",

                // === Tier 3: Flächenkombo ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【Doppelschlag-Schadensbonus (%)】\n" +
                "Zusätzlicher Schaden bei zwei aufeinanderfolgenden Angriffen.\n" +
                "Spezialisierung auf Flächenkombos.\n" +
                "Empfehlung: 20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【Doppelschlag-Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Doppelschlag-Buffs.\n" +
                "Längere Dauer = stabile Kombo.\n" +
                "Empfehlung: 4-8 Sek.",

                ["Tier3_AreaCombo_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Flächenkombo.",

                // === Tier 4-1: Bodenschlag ===
                ["Tier4-1_GroundWheel_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Bodenschlags.",

                // === Tier 4-2: Halbmondschnitt ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] =
                "【Angriffsreichweite-Bonus (%)】\n" +
                "Erhöht die Reichweite des Halbmondschnitts.\n" +
                "Ermöglicht Angriffe in einem breiteren Radius.\n" +
                "Empfehlung: 12-20%",

                ["Tier4-2_MoonSlash_StaminaReduction"] =
                "【Ausdauerreduzierung (%)】\n" +
                "Reduziert den Ausdauerverbrauch beim Halbmondschnitt.\n" +
                "Ermöglicht längeres Kämpfen.\n" +
                "Empfehlung: 12-20%",

                ["Tier4-2_MoonSlash_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Halbmondschnitts.",

                // === Tier 4-3: Unterdrückung ===
                ["Tier4-3_Suppress_DamageBonus"] =
                "【Unterdrückungs-Schadensbonus (%)】\n" +
                "Zusätzlicher Schaden beim Unterdrückungsangriff.\n" +
                "Feinde unterdrücken und die Initiative im Kampf übernehmen.\n" +
                "Empfehlung: 25-40%",

                ["Tier4-3_Suppress_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Unterdrückung.",

                // === Tier 5: Durchbohr-Ansturm (Aktiv G) ===
                ["Tier5_PierceCharge_DashDistance"] =
                "【Ansturmdistanz (m)】\n" +
                "Ansturmdistanz bei der Durchbohr-Fähigkeit.\n" +
                "Stürme über große Distanz durch feindliche Reihen.\n" +
                "Empfehlung: 8-12 m",

                ["Tier5_PierceCharge_FirstHitDamageBonus"] =
                "【Erster Treffer Schadensbonus (%)】\n" +
                "Schadensmultiplikator des ersten Treffers während des Ansturms.\n" +
                "Ein mächtiger erster Treffer unterdrückt die Feinde.\n" +
                "Empfehlung: 180-250%",

                ["Tier5_PierceCharge_AoeDamageBonus"] =
                "【Rückstoß-AOE-Schadensbonus (%)】\n" +
                "Schadensmultiplikator des Flächenrückstoßes nach dem Ansturm.\n" +
                "Stößt Feinde in der Umgebung zurück und verursacht Schaden.\n" +
                "Empfehlung: 130-180%",

                ["Tier5_PierceCharge_AoeAngle"] =
                "【AOE-Winkel (Grad)】\n" +
                "Winkel des Flächenrückstoß-Effekts.\n" +
                "280° = hinterer/seitlicher Bereich, exklusive 80° frontal.\n" +
                "Empfehlung: 250-300°",

                ["Tier5_PierceCharge_AoeRadius"] =
                "【AOE-Radius (m)】\n" +
                "Radius des Flächenrückstoß-Effekts.\n" +
                "Größer = mehr Feinde werden zurückgestoßen.\n" +
                "Empfehlung: 4-7 m",

                ["Tier5_PierceCharge_KnockbackDistance"] =
                "【Rückstoßdistanz (m)】\n" +
                "Rückstoßdistanz der Feinde.\n" +
                "Nützlich zur Schlachtkontrolle.\n" +
                "Empfehlung: 6-10 m",

                ["Tier5_PierceCharge_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Nutzung der G-Fähigkeit.\n" +
                "Ausdauermanagement ist wichtig.\n" +
                "Empfehlung: 18-25",

                ["Tier5_PierceCharge_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung der G-Fähigkeit.\n" +
                "Weniger = häufigere Nutzung möglich.\n" +
                "Empfehlung: 25-40 Sek.",

                ["Tier5_PierceCharge_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Durchbohr-Ansturms.",
            };
        }
    }
}

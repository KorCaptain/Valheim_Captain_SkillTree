using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJobDescriptions_DE()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Bogenschützen-Fähigkeiten (Archer Job)
                // ========================================

                // === Bogenschütze: Aktiv «Mehrfachschuss» (6 Schlüssel) ===
                ["Archer_MultiShot_ArrowCount"] =
                "【Pfeilanzahl】\n" +
                "Anzahl der abgefeuerten Pfeile pro Mehrfachschuss.\n" +
                "Mehr Pfeile = mehr Flächenschaden.\n" +
                "Empfehlung: 4-7",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【Pfeilverbrauch】\n" +
                "Anzahl der verbrauchten Pfeile pro Mehrfachschuss.\n" +
                "Geringer Verbrauch für effiziente Angriffe.\n" +
                "Empfehlung: 1-2",

                ["Archer_MultiShot_DamagePercent"] =
                "【Schaden pro Pfeil (%)】\n" +
                "Schadensprozentsatz jedes einzelnen Pfeils.\n" +
                "Prozentsatz des Bogenbasisangriffs.\n" +
                "Empfehlung: 40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung des Mehrfachschusses.\n" +
                "Geringerer Wert = häufigere Nutzung möglich.\n" +
                "Empfehlung: 25-40 Sek.",

                ["Archer_MultiShot_Charges"] =
                "【Anzahl der Ladungen】\n" +
                "Anzahl aufeinanderfolgender Mehrfachschuss-Verwendungen.\n" +
                "Mehrere Schüsse für konzentrierten Schaden.\n" +
                "Empfehlung: 2-4",

                ["Archer_MultiShot_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Verbrauchte Ausdauer bei Mehrfachschuss-Nutzung.\n" +
                "Ausdauermanagement ist wichtig.\n" +
                "Empfehlung: 20-35",

                // === Bogenschütze: Passive Fähigkeiten (2 Schlüssel) ===
                ["Archer_JumpHeightBonus"] =
                "【Sprunghöhe-Bonus (%)】\n" +
                "Erhöht die Grundsprunghöhe.\n" +
                "Erleichtert das Erreichen erhöhter Positionen.\n" +
                "Empfehlung: 15-25%",

                ["Archer_FallDamageReduction"] =
                "【Fallschadensreduzierung (%)】\n" +
                "Reduziert den Schaden beim Fall aus großen Höhen.\n" +
                "Verbessert die Mobilität des Bogenschützen.\n" +
                "Empfehlung: 40-60%",

                // === Bogenschütze: Level-Boni (9 Schlüssel) ===
                ["Archer_Lv2_BonusArrows"] =
                "【Lv.2: Zusatzpfeile】\n" +
                "Zusätzliche Pfeile beim Aufstieg auf Lv.2.\n" +
                "Zu der Basispfeilanzahl addiert.\n" +
                "Empfehlung: 1",

                ["Archer_Lv2_DamagePercent"] =
                "【Lv.2: Schaden pro Pfeil (%)】\n" +
                "Schadensmultiplikator pro Pfeil auf Lv.2.\n" +
                "Angewendet als % des Gesamt-Bogen+Pfeil-Schadens.\n" +
                "Empfehlung: 50-60%",

                ["Archer_Lv3_BonusArrows"] =
                "【Lv.3: Zusatzpfeile】\n" +
                "Zusätzliche Pfeile beim Aufstieg auf Lv.3.\n" +
                "Zu der Basispfeilanzahl addiert.\n" +
                "Empfehlung: 2",

                ["Archer_Lv3_DamagePercent"] =
                "【Lv.3: Schaden pro Pfeil (%)】\n" +
                "Schadensmultiplikator pro Pfeil auf Lv.3.\n" +
                "Angewendet als % des Gesamt-Bogen+Pfeil-Schadens.\n" +
                "Empfehlung: 55-65%",

                ["Archer_Lv4_BonusArrows"] =
                "【Lv.4: Zusatzpfeile】\n" +
                "Zusätzliche Pfeile beim Aufstieg auf Lv.4.\n" +
                "Zu der Basispfeilanzahl addiert.\n" +
                "Empfehlung: 3",

                ["Archer_Lv4_DamagePercent"] =
                "【Lv.4: Schaden pro Pfeil (%)】\n" +
                "Schadensmultiplikator pro Pfeil auf Lv.4.\n" +
                "Angewendet als % des Gesamt-Bogen+Pfeil-Schadens.\n" +
                "Empfehlung: 60-70%",

                ["Archer_Lv5_BonusArrows"] =
                "【Lv.5: Zusatzpfeile】\n" +
                "Zusätzliche Pfeile beim Aufstieg auf Lv.5.\n" +
                "Zu der Basispfeilanzahl addiert.\n" +
                "Empfehlung: 3",

                ["Archer_Lv5_DamagePercent"] =
                "【Lv.5: Schaden pro Pfeil (%)】\n" +
                "Schadensmultiplikator pro Pfeil auf Lv.5.\n" +
                "Angewendet als % des Gesamt-Bogen+Pfeil-Schadens.\n" +
                "Empfehlung: 60-70%",

                ["Archer_Lv5_BonusCharges"] =
                "【Lv.5: Zusatzladungen】\n" +
                "Zusätzliche Mehrfachschuss-Ladungen auf Lv.5.\n" +
                "Zu der Basisladungsanzahl addiert.\n" +
                "Empfehlung: 1",

                // === Bogenschütze: Passive Level-Boni (8 Schlüssel) ===
                ["Archer_Lv2_JumpHeightBonus"] =
                "【Lv.2 Passiv: Sprunghöhe-Bonus (%)】\n" +
                "Zusätzlicher Sprunghöhebonus auf Lv.2.\n" +
                "Zum Basiswert von Lv.1 addiert.\n" +
                "Empfehlung: 10%",

                ["Archer_Lv3_JumpHeightBonus"] =
                "【Lv.3 Passiv: Sprunghöhe-Bonus (%)】\n" +
                "Zusätzlicher Sprunghöhebonus auf Lv.3.\n" +
                "Empfehlung: 20%",

                ["Archer_Lv4_JumpHeightBonus"] =
                "【Lv.4 Passiv: Sprunghöhe-Bonus (%)】\n" +
                "Zusätzlicher Sprunghöhebonus auf Lv.4.\n" +
                "Empfehlung: 20%",

                ["Archer_Lv5_JumpHeightBonus"] =
                "【Lv.5 Passiv: Sprunghöhe-Bonus (%)】\n" +
                "Zusätzlicher Sprunghöhebonus auf Lv.5.\n" +
                "Empfehlung: 20%",

                ["Archer_Lv3_FallDamageReduction"] =
                "【Lv.3 Passiv: Fallschadensreduzierung (%)】\n" +
                "Zusätzliche Fallschadensreduzierung auf Lv.3.\n" +
                "Zum Basiswert von Lv.1 addiert.\n" +
                "Empfehlung: 10%",

                ["Archer_Lv4_FallDamageReduction"] =
                "【Lv.4 Passiv: Fallschadensreduzierung (%)】\n" +
                "Zusätzliche Fallschadensreduzierung auf Lv.4.\n" +
                "Empfehlung: 20%",

                ["Archer_Lv5_FallDamageReduction"] =
                "【Lv.5 Passiv: Fallschadensreduzierung (%)】\n" +
                "Zusätzliche Fallschadensreduzierung auf Lv.5.\n" +
                "Empfehlung: 35%",

                ["Archer_ElementalResistPerLevel"] =
                "【Passiv: Elementarresistenz pro Level (%)】\n" +
                "Elementarresistenz pro Bogenschützenlevel.\n" +
                "Gift(Lv2+), Kälte(Lv3+), Feuer(Lv4+), Blitz(Lv5).\n" +
                "Empfehlung: 10%",

                // ========================================
                // Magier-Fähigkeiten (Mage Job)
                // ========================================

                // === Magier: Aktiv «AOE» (5 Schlüssel) ===
                ["Mage_AOE_Range"] =
                "【AOE-Reichweite (m)】\n" +
                "Radius des magischen Flächenangriffs.\n" +
                "Große Reichweite um mehrere Feinde zu treffen.\n" +
                "Empfehlung: 10-15 m",

                ["Mage_Eitr_Cost"] =
                "【Eitr-Kosten】\n" +
                "Verbrauchter Eitr bei Fähigkeitsnutzung.\n" +
                "Verwaltung der magischen Ressource ist wichtig.\n" +
                "Empfehlung: 30-45",

                ["Mage_Damage_Multiplier"] =
                "【Schadensmultiplikator (%)】\n" +
                "Schadensmultiplikator des magischen Flächenangriffs.\n" +
                "Mächtige Explosionsmagie zum Vernichten von Feinden.\n" +
                "Empfehlung: 250-350%",

                ["Mage_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Lange Abklingzeit aufgrund des mächtigen Effekts.\n" +
                "Empfehlung: 150-200 Sek.",

                // === Magier: Passiv (1 Schlüssel) ===
                ["Mage_Elemental_Resistance"] =
                "【Elementarresistenz (%)】\n" +
                "Erhöht Resistenz gegen Feuer, Eis, Blitz, Gift und Geist.\n" +
                "Physischer Schaden ausgeschlossen — nur magischer Schaden reduziert.\n" +
                "Empfehlung: 12-20%",

                // === Berserker: Passiver LP-Bonus ===
                ["berserker_passive_health_bonus"] =
                "【Maximaler LP-Bonus (%)】\n" +
                "Berserker Passiv: Erhöht die maximalen LP.\n" +
                "Angewendet als % der Gesamt-LP (Basis + MMO + alle Boni).\n" +
                "Heilung funktioniert korrekt (in m_baseHP enthalten).\n" +
                "Empfehlung: 100%",

                // ========================================
                // Tanker-Fähigkeiten (Tanker Job)
                // ========================================

                // === Tanker: Aktiv «Kriegsruf» (9 Schlüssel) ===
                ["Tanker_Taunt_Cooldown"] =
                "【Kriegsruf-Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung der Fähigkeit.\n" +
                "Empfehlung: 45-90 Sek.",

                ["Tanker_Taunt_StaminaCost"] =
                "【Kriegsruf-Ausdauerkosten】\n" +
                "Verbrauchte Ausdauer beim Aktivieren des Kriegsrufs.\n" +
                "Empfehlung: 20-30",

                ["Tanker_Taunt_Range"] =
                "【Kriegsruf-Reichweite (m)】\n" +
                "Radius, in dem Feinde provoziert werden.\n" +
                "Empfehlung: 10-15 m",

                ["Tanker_Taunt_Duration"] =
                "【Provokationsdauer bei normalen Monstern (Sek.)】\n" +
                "Wirkungsdauer der Provokation bei normalen Monstern.\n" +
                "Empfehlung: 4-8 Sek.",

                ["Tanker_Taunt_BossDuration"] =
                "【Provokationsdauer bei Bossen (Sek.)】\n" +
                "Wirkungsdauer der Provokation bei Bossen.\n" +
                "Bosse sind widerstandsfähiger — kürzerer Effekt.\n" +
                "Empfehlung: 1-3 Sek.",

                ["Tanker_Taunt_DamageReduction"] =
                "【Eingehende Schadensreduzierung (%)】\n" +
                "Schadensreduzierung während des aktiven Kriegsruf-Buffs.\n" +
                "Empfehlung: 15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【Schadensreduzierungs-Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Schadensreduzierungs-Buffs nach Aktivierung.\n" +
                "Empfehlung: 4-8 Sek.",

                ["Tanker_Taunt_EffectHeight"] =
                "【Provokationssymbol-Höhe (m)】\n" +
                "Höhe über dem Monster, wo das Provokationssymbol angezeigt wird.\n" +
                "Empfehlung: 1,5-2,5 m",

                ["Tanker_Taunt_EffectScale"] =
                "【Provokationssymbol-Skalierung】\n" +
                "Größenmultiplikator des visuellen Provokationseffekts.\n" +
                "Empfehlung: 0,2-0,5",

                // === Tanker: Passiv (1 Schlüssel) ===
                ["Tanker_Passive_DamageReduction"] =
                "【Passive Tanker-Schadensreduzierung (%)】\n" +
                "Tanker Passiv: Reduziert kontinuierlich den eingehenden Schaden.\n" +
                "Empfehlung: 10-20%",

                // ========================================
                // Schurken-Fähigkeiten (Rogue Job)
                // ========================================

                // === Schurke: Aktiv «Schattenschlag» (7 Schlüssel) ===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【Schattenschlag-Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung des Schattenschlags.\n" +
                "Empfehlung: 20-40 Sek.",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【Schattenschlag-Ausdauerkosten】\n" +
                "Verbrauchte Ausdauer beim Aktivieren des Schattenschlags.\n" +
                "Empfehlung: 20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【Schattenschlag-Angriffsbonus (%)】\n" +
                "Angriffserhöhung während der Buff-Dauer nach Aktivierung.\n" +
                "Empfehlung: 25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【Angriffs-Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Angriffserhöhungs-Buffs.\n" +
                "Empfehlung: 6-12 Sek.",

                ["Rogue_ShadowStrike_SmokeScale"] =
                "【Raucheffekt-Skalierung】\n" +
                "Größenmultiplikator des Rauch-VFX.\n" +
                "Empfehlung: 1,5-3,0",

                ["Rogue_ShadowStrike_AggroRange"] =
                "【Aggro-Entfernungsreichweite (m)】\n" +
                "Entfernt Aggro von allen Feinden in diesem Radius.\n" +
                "Empfehlung: 10-20 m",

                ["Rogue_ShadowStrike_StealthDuration"] =
                "【Tarnungsdauer (Sek.)】\n" +
                "Wirkungsdauer des Tarnungsmodus.\n" +
                "Empfehlung: 5-10 Sek.",

                // === Schurke: Passive Fähigkeiten (3 Schlüssel) ===
                ["Rogue_AttackSpeed_Bonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Schurke Passiv: Erhöht kontinuierlich die Angriffsgeschwindigkeit.\n" +
                "Empfehlung: 8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【Ausdauerverbrauchsreduzierung bei Angriffen (%)】\n" +
                "Schurke Passiv: Reduziert den Ausdauerverbrauch bei Angriffen.\n" +
                "Empfehlung: 10-20%",

                ["Rogue_ElementalResistance_Debuff"] =
                "【Elementarresistenz-Erhöhung (%)】\n" +
                "Schurke Passiv: Erhöht die Resistenz gegen elementaren Schaden.\n" +
                "Empfehlung: 8-15%",

                // ========================================
                // Paladin-Fähigkeiten (Paladin Job)
                // ========================================

                // === Paladin: Aktiv «Heiliges Licht» (8 Schlüssel) ===
                ["Paladin_Active_Cooldown"] =
                "【Heiliges Licht Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung der Fähigkeit.\n" +
                "Empfehlung: 20-45 Sek.",

                ["Paladin_Active_Range"] =
                "【Heiliges Licht Reichweite (m)】\n" +
                "Radius, in dem Verbündete geheilt werden.\n" +
                "Empfehlung: 4-8 m",

                ["Paladin_Active_EitrCost"] =
                "【Heiliges Licht Eitr-Kosten】\n" +
                "Verbrauchter Eitr beim Aktivieren von Heiliges Licht.\n" +
                "Empfehlung: 8-15",

                ["Paladin_Active_StaminaCost"] =
                "【Heiliges Licht Ausdauerkosten】\n" +
                "Verbrauchte Ausdauer beim Aktivieren von Heiliges Licht.\n" +
                "Empfehlung: 8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【Selbstheilungsprozentsatz (% der max. LP)】\n" +
                "Prozentsatz der eigenen LP, der bei Aktivierung wiederhergestellt wird.\n" +
                "Empfehlung: 10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【Verbündeten-Heilung über Zeit (% der max. LP pro Sek.)】\n" +
                "Prozentsatz der LP, die jedem Verbündeten pro Sekunde wiederhergestellt werden.\n" +
                "Empfehlung: 1-3%",

                ["Paladin_Active_Duration"] =
                "【Heilungs-Dauer (Sek.)】\n" +
                "Gesamtdauer der Verbündeten-Heilung über Zeit.\n" +
                "Empfehlung: 8-15 Sek.",

                ["Paladin_Active_Interval"] =
                "【Heilungsintervall (Sek.)】\n" +
                "Zeitraum der Heilungsanwendung.\n" +
                "Empfehlung: 1 Sek.",

                // === Paladin: Passiv (1 Schlüssel) ===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【Physische und elementare Resistenz-Bonus (%)】\n" +
                "Paladin Passiv: Erhöht die Resistenz gegen physischen und elementaren Schaden.\n" +
                "Empfehlung: 5-12%",

                // ========================================
                // Berserker-Fähigkeiten (Berserker Job)
                // ========================================

                // === Berserker: Aktiv «Berserkerwut» (6 Schlüssel, Typo Beserker beibehalten) ===
                ["Beserker_Active_Cooldown"] =
                "【Berserkerwut-Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung der Berserkerwut.\n" +
                "Empfehlung: 30-60 Sek.",

                ["Beserker_Active_StaminaCost"] =
                "【Berserkerwut-Ausdauerkosten】\n" +
                "Verbrauchte Ausdauer beim Aktivieren der Berserkerwut.\n" +
                "Empfehlung: 15-25",

                ["Beserker_Active_Duration"] =
                "【Berserkerwut-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Berserkerwut-Buffs.\n" +
                "Empfehlung: 15-25 Sek.",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【Schadensbonus pro 1% verlorener LP (%)】\n" +
                "Der Schaden erhöht sich mit sinkenden LP.\n" +
                "% verlorene LP × dieser Wert = Schadensbonus\n" +
                "Empfehlung: 1,5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【Maximaler Schadensbonus (%)】\n" +
                "Maximale Obergrenze des LP-gebundenen Schadensbonus.\n" +
                "Empfehlung: 150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【LP-Schwellenwert für Aktivierung (%)】\n" +
                "Der LP-gebundene Schadensbonus aktiviert sich unter diesem LP-%.\n" +
                "100% für konstante Aktivierung setzen.\n" +
                "Empfehlung: 50-100%",

                // === Berserker: Passiv «Todesherausforderung» (3 Schlüssel, Typo Beserker beibehalten) ===
                ["Beserker_Passive_HealthThreshold"] =
                "【LP-Schwellenwert für Passiv-Aktivierung (%)】\n" +
                "Unverwundbarkeit aktiviert sich, wenn LP unter diesen % fallen.\n" +
                "Empfehlung: 8-15%",

                ["Beserker_Passive_InvincibilityDuration"] =
                "【Unverwundbarkeitsdauer (Sek.)】\n" +
                "Wirkungsdauer der Unverwundbarkeit bei Passiv-Aktivierung.\n" +
                "Empfehlung: 5-10 Sek.",

                ["Beserker_Passive_Cooldown"] =
                "【Passiv-Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur nächsten Passiv-Unverwundbarkeitsaktivierung.\n" +
                "Standard: 180 Sek. (3 Minuten)\n" +
                "Empfehlung: 120-300 Sek.",

                // === Berserker: Passiver LP-Bonus ===
                ["Berserker_Passive_HealthBonus"] =
                "【Maximaler LP-Bonus (%)】\n" +
                "Berserker Passiv: Erhöht die maximalen LP.\n" +
                "Empfehlung: 100%",
            };
        }
    }
}

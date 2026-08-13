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

                ["Archer_MultiShot_FireInterval"] =
                "【Serienfeuer-Intervall (Sek.)】\n" +
                "Abstand zwischen den Salven-Pfeilen.\n" +
                "5 Pfeile werden in diesem Abstand nacheinander abgefeuert.\n" +
                "Empfehlung: 0,15-0,3 Sek.",

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

                ["Mage_Fire_Rain_Radius"] =
                "【Feuerregen-Fallradius (m)】\n" +
                "Radius um das Ziel, in dem 30 Feuerbälle niedergehen.\n" +
                "Empfehlung: 6-10 m",

                ["Mage_Fire_Rain_Impact_Radius"] =
                "【Feuerball-Einschlagradius (m)】\n" +
                "Schadensradius beim Einschlag jedes Feuerballs.\n" +
                "Empfehlung: 2-4 m",

                ["Mage_Fire_Rain_Projectile_Count"] =
                "【Feuerbälle pro Salve】\n" +
                "Anzahl der Feuerbälle, die pro Salve niedergehen.\n" +
                "Insgesamt 2 Salven (1. Salve -> 1 Sek. -> 2. Salve).\n" +
                "Empfehlung: 15-25",

                ["Mage_Dungeon_Buff_Damage_Bonus"] =
                "【Dungeon-Buff Schadensbonus (%)】\n" +
                "Schadenserhöhung des Selbstbuffs, der in Dungeons anstelle von Feuerregen ausgelöst wird.\n" +
                "Empfehlung: 20-30%",

                ["Mage_Dungeon_Buff_Duration"] =
                "【Dungeon-Buff Dauer (Sek.)】\n" +
                "Wie lange der Ersatz-Buff im Dungeon anhält.\n" +
                "Empfehlung: 8-12 Sek.",

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

                // === Berserker Lv2~5 Passive Config ===
                ["Berserker_Lv2_CooldownReduction"] =
                "【Berserker Lv2: Wut-KZ Reduzierung (Sek)】\n" +
                "Auf Lv2 reduziert sich die Wut-Abklingzeit um diesen Wert.\n" +
                "Empfehlung: 5 Sekunden",

                ["Berserker_Lv3_RageDamageReduction"] =
                "【Berserker Lv3: Schadensreduzierung in Wut (%)】\n" +
                "Auf Lv3 reduziert sich der erhaltene Schaden im Wutzustand.\n" +
                "Empfehlung: 15%",

                ["Berserker_Lv4_LowHpAttackBonus"] =
                "【Berserker Lv4: Angriffsbonus bei wenig LP (%)】\n" +
                "Auf Lv4 erhöht sich der Angriff wenn LP unter dem Schwellenwert.\n" +
                "Empfehlung: 15%",

                ["Berserker_Lv4_LowHpAttackThreshold"] =
                "【Berserker Lv4: Schwellenwert für wenig LP (%)】\n" +
                "Unter diesem LP-Prozentsatz aktiviert sich der Angriffsbonus Lv4.\n" +
                "Empfehlung: 50%",

                ["Berserker_Lv5_PassiveCooldownReduction"] =
                "【Berserker Lv5: Todestrotz KZ Reduzierung (Sek)】\n" +
                "Auf Lv5 reduziert sich die passive Abklingzeit.\n" +
                "Empfehlung: 120 Sekunden",

                ["Berserker_Lv5_InvincibilityBonus"] =
                "【Berserker Lv5: Zusätzliche Unverwundbarkeit (Sek)】\n" +
                "Auf Lv5 verlängert sich die Unverwundbarkeitszeit bei Todestrotz.\n" +
                "Empfehlung: 2 Sekunden",

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

                ["Tanker_Taunt_ReflectPercent"] =
                "【Spott-Reflektionsschaden (%)】\n" +
                "Reflektiert eingehenden Schaden an Angreifer während des Kriegsruf-Buffs.\n" +
                "Aktiv für die Buff-Dauer.\n" +
                "Empfehlung: 5-20%",

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

                ["Tanker_NormalShield_SpeedBonus"] =
                "【Tanker: Normales Schild Bewegungsgeschwindigkeit (%)】\n" +
                "Tanker Lv1+: Bewegungsgeschwindigkeitsbonus beim Tragen eines normalen Schildes.\n" +
                "Standard: 25%",

                ["Tanker_TowerShield_SpeedBonus"] =
                "【Tanker: Turmschild Bewegungsgeschwindigkeit (%)】\n" +
                "Tanker Lv1+: Bewegungsgeschwindigkeitsbonus beim Tragen eines Turmschildes.\n" +
                "Standard: 30%",

                // === Lv1 ===
                ["Tanker_ReflectDuration_Lv1"] =
                "【Tanker Reflektionsdauer Lv1 (Sek)】\n" +
                "Standard: 10 Sek",

                ["Tanker_Hp_Bonus_Lv1"] =
                "【Tanker Lv1 LP-Bonus (%)】\n" +
                "Maximale LP erhöht sich prozentual beim Erreichen von Tanker Lv1.\n" +
                "Standard: 25",

                // === Lv2 ===
                ["Tanker_Hp_Bonus_Lv2"] =
                "【Tanker Lv2 LP-Bonus (%)】\n" +
                "Maximale LP erhöht sich prozentual beim Erreichen von Tanker Lv2.\n" +
                "Standard: 30",

                ["Tanker_Lv2_BlockPower"] =
                "【Tanker Lv2 Blockstärke】\n" +
                "Passive Blockstärke des Tankers auf Lv2.\n" +
                "Standard: 5",

                ["Tanker_ReflectDuration_Lv2"] =
                "【Tanker Reflektionsdauer Lv2 (Sek)】\n" +
                "Standard: 12 Sek",

                // === Lv3 ===
                ["Tanker_Hp_Bonus_Lv3"] =
                "【Tanker Lv3 LP-Bonus (%)】\n" +
                "Maximale LP erhöht sich prozentual beim Erreichen von Tanker Lv3.\n" +
                "Standard: 35",

                ["Tanker_Lv3_BlockPower"] =
                "【Tanker Lv3 Blockstärke】\n" +
                "Passive Blockstärke des Tankers auf Lv3.\n" +
                "Standard: 10",

                ["Tanker_ReflectDuration_Lv3"] =
                "【Tanker Reflektionsdauer Lv3 (Sek)】\n" +
                "Standard: 14 Sek",

                // === Lv4 ===
                ["Tanker_Hp_Bonus_Lv4"] =
                "【Tanker Lv4 LP-Bonus (%)】\n" +
                "Maximale LP erhöht sich prozentual beim Erreichen von Tanker Lv4.\n" +
                "Standard: 40",

                ["Tanker_Lv4_BlockPower"] =
                "【Tanker Lv4 Blockstärke】\n" +
                "Passive Blockstärke des Tankers auf Lv4.\n" +
                "Standard: 15",

                ["Tanker_ReflectDuration_Lv4"] =
                "【Tanker Reflektionsdauer Lv4 (Sek)】\n" +
                "Standard: 16 Sek",

                // === Lv5 ===
                ["Tanker_Hp_Bonus_Lv5"] =
                "【Tanker Lv5 LP-Bonus (%)】\n" +
                "Maximale LP erhöht sich prozentual beim Erreichen von Tanker Lv5.\n" +
                "Standard: 50",

                ["Tanker_Lv5_BlockPower"] =
                "【Tanker Lv5 Blockstärke】\n" +
                "Passive Blockstärke des Tankers auf Lv5.\n" +
                "Standard: 20",

                ["Tanker_ReflectDuration_Lv5"] =
                "【Tanker Reflektionsdauer Lv5 (Sek)】\n" +
                "Standard: 20 Sek",

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

                ["Rogue_Lv1_DodgeChance"] =
                "【Lv1 Ausweichrate (%)】\n" +
                "Schurke Passiv: Erhöht die Ausweichrate. Wird mit Fähigkeitenbaum kumuliert.\n" +
                "Empfehlung: 3-6%",
                ["Rogue_Lv2_DodgeChance"] = "【Lv2 Ausweichrate (%)】\nEmpfehlung: 5-8%",
                ["Rogue_Lv3_DodgeChance"] = "【Lv3 Ausweichrate (%)】\nEmpfehlung: 7-10%",
                ["Rogue_Lv4_DodgeChance"] = "【Lv4 Ausweichrate (%)】\nEmpfehlung: 9-12%",
                ["Rogue_Lv5_DodgeChance"] = "【Lv5 Ausweichrate (%)】\nEmpfehlung: 11-15%",

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

                // === Paladin Lv2-5 ===
                ["Paladin_Lv2_SelfHealPercent"] = "【Lv2 Selbstheilung (%)】\nEmpfehlung: 15-20%",
                ["Paladin_Lv2_AllyHealPercent"] = "【Lv2 Verbündeten-Heilung (%/Tick)】\nEmpfehlung: 2-3%",
                ["Paladin_Lv3_SelfHealPercent"] = "【Lv3 Selbstheilung (%)】\nEmpfehlung: 17-22%",
                ["Paladin_Lv3_AllyHealPercent"] = "【Lv3 Verbündeten-Heilung (%/Tick)】\nEmpfehlung: 2,5-3,5%",
                ["Paladin_Lv3_HealRange"] = "【Lv3 Heilungsreichweite (m)】\nEmpfehlung: 5-7m",
                ["Paladin_Lv4_SelfHealPercent"] = "【Lv4 Selbstheilung (%)】\nEmpfehlung: 19-24%",
                ["Paladin_Lv4_AllyHealPercent"] = "【Lv4 Verbündeten-Heilung (%/Tick)】\nEmpfehlung: 3-4%",
                ["Paladin_Lv4_HealRange"] = "【Lv4 Heilungsreichweite (m)】\nEmpfehlung: 6-8m",
                ["Paladin_Lv5_SelfHealPercent"] = "【Lv5 Selbstheilung (%)】\nEmpfehlung: 22-28%",
                ["Paladin_Lv5_AllyHealPercent"] = "【Lv5 Verbündeten-Heilung (%/Tick)】\nEmpfehlung: 3,5-5%",
                ["Paladin_Lv5_HealRange"] = "【Lv5 Heilungsreichweite (m)】\nEmpfehlung: 7-10m",
                ["Paladin_Lv2_Cooldown"] = "【Lv2 Abklingzeit (Sek.)】\nEmpfehlung: 25-35 Sek.",
                ["Paladin_Lv3_Cooldown"] = "【Lv3 Abklingzeit (Sek.)】\nEmpfehlung: 24-34 Sek.",
                ["Paladin_Lv4_Cooldown"] = "【Lv4 Abklingzeit (Sek.)】\nEmpfehlung: 23-33 Sek.",
                ["Paladin_Lv5_Cooldown"] = "【Lv5 Abklingzeit (Sek.)】\nEmpfehlung: 20-30 Sek.",
                ["Paladin_Lv2_ResistanceReduction"] = "【Lv2 Resistenzreduzierung (%)】\nEmpfehlung: 6-10%",
                ["Paladin_Lv3_ResistanceReduction"] = "【Lv3 Resistenzreduzierung (%)】\nEmpfehlung: 8-12%",
                ["Paladin_Lv4_ResistanceReduction"] = "【Lv4 Resistenzreduzierung (%)】\nEmpfehlung: 10-14%",
                ["Paladin_Lv5_ResistanceReduction"] = "【Lv5 Resistenzreduzierung (%)】\nEmpfehlung: 12-18%",
                ["Paladin_Lv2_StaminaBonus"] = "【Lv2 Max. Ausdauer-Bonus】\nEmpfehlung: 8-15",
                ["Paladin_Lv3_StaminaBonus"] = "【Lv3 Max. Ausdauer-Bonus】\nEmpfehlung: 12-20",
                ["Paladin_Lv4_StaminaBonus"] = "【Lv4 Max. Ausdauer-Bonus】\nEmpfehlung: 15-25",
                ["Paladin_Lv5_StaminaBonus"] = "【Lv5 Max. Ausdauer-Bonus】\nEmpfehlung: 20-30",

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
                ["Berserker_Passive_HealthThreshold"] =
                "【LP-Schwellenwert für Passiv-Aktivierung (%)】\n" +
                "Unverwundbarkeit aktiviert sich, wenn LP unter diesen % fallen.\n" +
                "Empfehlung: 8-15%",

                ["Berserker_Passive_InvincibilityDuration"] =
                "【Unverwundbarkeitsdauer (Sek.)】\n" +
                "Wirkungsdauer der Unverwundbarkeit bei Passiv-Aktivierung.\n" +
                "Empfehlung: 5-10 Sek.",

                ["Berserker_Passive_Cooldown"] =
                "【Passiv-Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur nächsten Passiv-Unverwundbarkeitsaktivierung.\n" +
                "Standard: 540 Sek. (9 Minuten)\n" +
                "Empfehlung: 120-300 Sek.",

                // === Berserker: Passiver LP-Bonus ===
                ["Berserker_Passive_HealthBonus"] =
                "【Maximaler LP-Bonus (%)】\n" +
                "Berserker Passiv: Erhöht die maximalen LP.\n" +
                "Empfehlung: 100%",

                // === Berserker: Lv-basierte Wut-Abklingzeiten ===
                ["Berserker_Lv1_Active_Cooldown"] =
                "【Berserker Lv1: Wut-Abklingzeit (Sek.)】\n" +
                "Abklingzeit der Wut auf Lv1.\n" +
                "Empfehlung: 45 Sek.",

                ["Berserker_Lv2_Active_Cooldown"] =
                "【Berserker Lv2: Wut-Abklingzeit (Sek.)】\n" +
                "Abklingzeit der Wut auf Lv2.\n" +
                "Empfehlung: 40 Sek.",

                ["Berserker_Lv3_Active_Cooldown"] =
                "【Berserker Lv3: Wut-Abklingzeit (Sek.)】\n" +
                "Abklingzeit der Wut auf Lv3.\n" +
                "Empfehlung: 40 Sek.",

                ["Berserker_Lv4_Active_Cooldown"] =
                "【Berserker Lv4: Wut-Abklingzeit (Sek.)】\n" +
                "Abklingzeit der Wut auf Lv4.\n" +
                "Empfehlung: 40 Sek.",

                ["Berserker_Lv5_Active_Cooldown"] =
                "【Berserker Lv5: Wut-Abklingzeit (Sek.)】\n" +
                "Abklingzeit der Wut auf Lv5.\n" +
                "Empfehlung: 35 Sek.",

                // === Berserker: Lv-basierte Wut-Dauer ===
                ["Berserker_Lv1_Active_Duration"] =
                "【Berserker Lv1: Wut-Dauer (Sek.)】\n" +
                "Wirkungsdauer der Wut auf Lv1.\n" +
                "Empfehlung: 20 Sek.",

                ["Berserker_Lv2_Active_Duration"] =
                "【Berserker Lv2: Wut-Dauer (Sek.)】\n" +
                "Wirkungsdauer der Wut auf Lv2.\n" +
                "Empfehlung: 20 Sek.",

                ["Berserker_Lv3_Active_Duration"] =
                "【Berserker Lv3: Wut-Dauer (Sek.)】\n" +
                "Wirkungsdauer der Wut auf Lv3.\n" +
                "Empfehlung: 25 Sek.",

                ["Berserker_Lv4_Active_Duration"] =
                "【Berserker Lv4: Wut-Dauer (Sek.)】\n" +
                "Wirkungsdauer der Wut auf Lv4.\n" +
                "Empfehlung: 25 Sek.",

                ["Berserker_Lv5_Active_Duration"] =
                "【Berserker Lv5: Wut-Dauer (Sek.)】\n" +
                "Wirkungsdauer der Wut auf Lv5.\n" +
                "Empfehlung: 25 Sek.",

                // === Berserker: Lv-basierter passiver LP-Bonus ===
                ["Berserker_Lv1_Passive_HealthBonus"] =
                "【Berserker Lv1: Max-LP-Bonus】\n" +
                "Flacher Bonus auf max. LP auf Lv1.\n" +
                "Empfehlung: 40",

                ["Berserker_Lv2_Passive_HealthBonus"] =
                "【Berserker Lv2: Max-LP-Bonus】\n" +
                "Flacher Bonus auf max. LP auf Lv2.\n" +
                "Empfehlung: 60",

                ["Berserker_Lv3_Passive_HealthBonus"] =
                "【Berserker Lv3: Max-LP-Bonus】\n" +
                "Flacher Bonus auf max. LP auf Lv3.\n" +
                "Empfehlung: 80",

                ["Berserker_Lv4_Passive_HealthBonus"] =
                "【Berserker Lv4: Max-LP-Bonus】\n" +
                "Flacher Bonus auf max. LP auf Lv4.\n" +
                "Empfehlung: 100",

                ["Berserker_Lv5_Passive_HealthBonus"] =
                "【Berserker Lv5: Max-LP-Bonus】\n" +
                "Flacher Bonus auf max. LP auf Lv5.\n" +
                "Empfehlung: 120",

                // === Berserker: Lv-basierter Schaden pro 1% verlorener LP ===
                ["Berserker_Lv1_Active_DamagePerHP"] =
                "【Berserker Lv1: Schaden/1% verl. LP (%)】\n" +
                "Schadensbonus pro 1% verl. LP während Wut (Lv1).\n" +
                "Empfehlung: 1,5%",

                ["Berserker_Lv2_Active_DamagePerHP"] =
                "【Berserker Lv2: Schaden/1% verl. LP (%)】\n" +
                "Schadensbonus pro 1% verl. LP während Wut (Lv2).\n" +
                "Empfehlung: 1,6%",

                ["Berserker_Lv3_Active_DamagePerHP"] =
                "【Berserker Lv3: Schaden/1% verl. LP (%)】\n" +
                "Schadensbonus pro 1% verl. LP während Wut (Lv3).\n" +
                "Empfehlung: 1,7%",

                ["Berserker_Lv4_Active_DamagePerHP"] =
                "【Berserker Lv4: Schaden/1% verl. LP (%)】\n" +
                "Schadensbonus pro 1% verl. LP während Wut (Lv4).\n" +
                "Empfehlung: 1,8%",

                ["Berserker_Lv5_Active_DamagePerHP"] =
                "【Berserker Lv5: Schaden/1% verl. LP (%)】\n" +
                "Schadensbonus pro 1% verl. LP während Wut (Lv5).\n" +
                "Empfehlung: 2,0%",

                // === Handwerksmeister (Producer) Job Skills ===
                // ========================================
                ["Producer_Buff_Cooldown"] =
                "【Meistersegen: Abklingzeit (Sek.)】\n" +
                "Abklingzeit zwischen den Buff-Aktivierungen.\n" +
                "Standard: 180 Sek.",

                ["Producer_Buff_Duration"] =
                "【Meistersegen: Dauer (Sek.)】\n" +
                "Dauer des Angriffs-/LP-Buffs für Verbündete.\n" +
                "Standard: 120 Sek.",

                ["Producer_Buff_Range"] =
                "【Meistersegen: Reichweite (m)】\n" +
                "Reichweite, in der Verbündete den Buff erhalten.\n" +
                "Standard: 15 m",

                ["Producer_Buff_AttackBonus"] =
                "【Buff-Angriffsbonus (%)】\n" +
                "Angriffsbonus für gebuffte Verbündete.\n" +
                "Standard: 15%",

                ["Producer_Buff_MaxHealthBonus"] =
                "【Buff-max. LP-Bonus (%)】\n" +
                "Maximaler LP-Bonus für gebuffte Verbündete.\n" +
                "Standard: 15%",

                ["Producer_Buff_StaminaCost"] =
                "【Buff-Ausdauerkosten】\n" +
                "Verbrauchte Ausdauer beim Aktivieren des Buffs.\n" +
                "Standard: 20",

                // === Producer Lv1 ===
                ["Producer_EnchantChance_Lv1"] = "【Verzauberungschance Lv1 (%)】\nChance auf Verzauberung bei Herstellung (Lv1).\nStandard: 45%",
                ["Producer_ElementalProcChance_Lv1"] = "【Elementarschadenchance Lv1 (%)】\nChance, dass eine Lv1-Elementarverzauberung (Feuer/Geist/Gift/Blitz/Frost) bei jedem Treffer auslöst.\nStandard: 25%",

                // === Producer Lv2 ===
                ["Producer_Durability_Lv2"] = "【Haltbarkeitsbonus Lv2 (%)】\nBonushaltbarkeit hergestellter Gegenstände bei Lv2.\nStandard: 10%",
                ["Producer_MaterialReduction_Lv2"] = "【Materialeinsparung Lv2 (%)】\nEingesparte Materialien pro Herstellung bei Lv2.\nStandard: 10%",
                ["Producer_EnchantChance_Lv2"] = "【Verzauberungschance Lv2 (%)】\nChance auf Verzauberung bei Herstellung (Lv2).\nStandard: 55%",
                ["Producer_ElementalProcChance_Lv2"] = "【Elementarschadenchance Lv2 (%)】\nChance, dass eine Lv2-Elementarverzauberung bei jedem Treffer auslöst.\nStandard: 30%",

                // === Producer Lv3 ===
                ["Producer_Durability_Lv3"] = "【Haltbarkeitsbonus Lv3 (%)】\nBonushaltbarkeit hergestellter Gegenstände bei Lv3.\nStandard: 15%",
                ["Producer_MaterialReduction_Lv3"] = "【Materialeinsparung Lv3 (%)】\nEingesparte Materialien pro Herstellung bei Lv3.\nStandard: 15%",
                ["Producer_EnchantChance_Lv3"] = "【Verzauberungschance Lv3 (%)】\nChance auf Verzauberung bei Herstellung (Lv3).\nStandard: 25%",
                ["Producer_ElementalProcChance_Lv3"] = "【Elementarschadenchance Lv3 (%)】\nChance, dass eine Lv3-Elementarverzauberung bei jedem Treffer auslöst.\nStandard: 35%",

                // === Producer Lv4 ===
                ["Producer_Durability_Lv4"] = "【Haltbarkeitsbonus Lv4 (%)】\nBonushaltbarkeit hergestellter Gegenstände bei Lv4.\nStandard: 20%",
                ["Producer_MaterialReduction_Lv4"] = "【Materialeinsparung Lv4 (%)】\nEingesparte Materialien pro Herstellung bei Lv4.\nStandard: 20%",
                ["Producer_EnchantChance_Lv4"] = "【Verzauberungschance Lv4 (%)】\nChance auf Verzauberung bei Herstellung (Lv4).\nStandard: 80%",
                ["Producer_ElementalProcChance_Lv4"] = "【Elementarschadenchance Lv4 (%)】\nChance, dass eine Lv4-Elementarverzauberung bei jedem Treffer auslöst.\nStandard: 40%",

                // === Producer Lv5 ===
                ["Producer_Durability_Lv5"] = "【Haltbarkeitsbonus Lv5 (%)】\nBonushaltbarkeit hergestellter Gegenstände bei Lv5.\nStandard: 30%",
                ["Producer_MaterialReduction_Lv5"] = "【Materialeinsparung Lv5 (%)】\nEingesparte Materialien pro Herstellung bei Lv5.\nStandard: 30%",
                ["Producer_EnchantChance_Lv5"] = "【Verzauberungschance Lv5 (%)】\nChance auf Verzauberung bei Herstellung (Lv5).\nStandard: 35%",
                ["Producer_ElementalProcChance_Lv5"] = "【Elementarschadenchance Lv5 (%)】\nChance, dass eine Lv5-Elementarverzauberung bei jedem Treffer auslöst.\nStandard: 45%",

                ["Job_Lv1_Cost"] = "【Job Lv1 Münzkosten】\nMünzen beim Aufsteigen eines Berufs auf Lv1.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 1000",
                ["Job_Lv2_Cost"] = "【Job Lv2 Münzkosten】\nMünzen beim Aufsteigen eines Berufs auf Lv2.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 2000",
                ["Job_Lv3_Cost"] = "【Job Lv3 Münzkosten】\nMünzen beim Aufsteigen eines Berufs auf Lv3.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 3000",
                ["Job_Lv4_Cost"] = "【Job Lv4 Münzkosten】\nMünzen beim Aufsteigen eines Berufs auf Lv4.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 4000",
                ["Job_Lv5_Cost"] = "【Job Lv5 Münzkosten】\nMünzen beim Aufsteigen eines Berufs auf Lv5.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 5000",

                ["Job_Reset_Cost"]    = "【Job-Skill Reset-Kosten】\nMünzen beim Zurücksetzen der Job-Skillpunkte.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 1000",
                ["Active_Reset_Cost"] = "【Aktiv-Skill Reset-Kosten】\nMünzen beim Zurücksetzen der Aktiv-Skillpunkte.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 500",
                ["Passive_Reset_Cost"]= "【Passiv-Skill Reset-Kosten】\nMünzen beim Zurücksetzen der Passiv-Skillpunkte.\nNur Serveradmin, wird an Clients synchronisiert.\nStandard: 100",

                ["HotKey_Y"] =
                "【Job Skill Key】\n" +
                "Taste zum Auslösen der aktiven Berufsfähigkeit.\n" +
                "Standard: Y",

                ["HotKey_R"] =
                "【Fernkampf-Skill-Taste】\n" +
                "Taste zum Auslösen von Fernkampf-Aktivskills (Mehrfachschuss, Doppelzauber usw.).\n" +
                "Standard: R",

                ["HotKey_G"] =
                "【Nahkampf-Hauptskill-Taste】\n" +
                "Taste zum Auslösen von Nahkampf-Hauptaktivskills (Sturmangriff usw.).\n" +
                "Standard: G",

                ["HotKey_H"] =
                "【Sekundär-Skill-Taste】\n" +
                "Taste zum Auslösen sekundärer Aktivskills (Speerkombo, Beschützerherz usw.).\n" +
                "Standard: H",

                ["QuestToggleKey"] =
                "【Questfenster-Tastenkürzel】\n" +
                "Tastenkürzel zum Öffnen und Schließen des Questfensters.\n" +
                "Standard: Strg+J",

                ["HUD_IconSize"] =
                "【Skill-Symbolgröße】\n" +
                "Größe der Symbole im Aktivskill-HUD.\n" +
                "Standard: 62",

                ["HUD_PosX"] =
                "【Skill-Symbol HUD X-Position】\n" +
                "Horizontale Position des Aktivskill-HUD.\n" +
                "Standard: 306 (vom linken Bildschirmrand)",

                ["HUD_PosY"] =
                "【Skill-Symbol HUD Y-Position】\n" +
                "Vertikale Position des Aktivskill-HUD.\n" +
                "Standard: 139 (vom unteren Bildschirmrand)",

                ["Archer_Attack_StaminaReduction_Lv1"] =
                "【Lv1 Passiv: Angriffs-Ausdauerreduzierung (%)】\n" +
                "Reduziert den Ausdauerverbrauch bei Angriffen auf Bogenschütze Lv1.\n" +
                "Gilt für alle Bogen-/Armbrust-/Stab-Angriffe.\n" +
                "Empfehlung: 10-20%",

                ["Archer_Attack_StaminaReduction_Lv2"] =
                "【Lv2 Passiv: Angriffs-Ausdauerreduzierung (%)】\n" +
                "Reduziert den Ausdauerverbrauch bei Angriffen auf Bogenschütze Lv2.\n" +
                "Empfehlung: 20-30%",

                ["Archer_Attack_StaminaReduction_Lv3"] =
                "【Lv3 Passiv: Angriffs-Ausdauerreduzierung (%)】\n" +
                "Reduziert den Ausdauerverbrauch bei Angriffen auf Bogenschütze Lv3.\n" +
                "Empfehlung: 30-40%",

                ["Archer_Attack_StaminaReduction_Lv4"] =
                "【Lv4 Passiv: Angriffs-Ausdauerreduzierung (%)】\n" +
                "Reduziert den Ausdauerverbrauch bei Angriffen auf Bogenschütze Lv4.\n" +
                "Empfehlung: 40-50%",

                ["Archer_Attack_StaminaReduction_Lv5"] =
                "【Lv5 Passiv: Angriffs-Ausdauerreduzierung (%)】\n" +
                "Reduziert den Ausdauerverbrauch bei Angriffen auf Bogenschütze Lv5.\n" +
                "Empfehlung: 50-60%",

                ["Archer_AmmoSaveChance"] =
                "【Pfeil-/Bolzen-Erhaltungschance (%)】\n" +
                "Chance, beim Angriff keinen Pfeil oder Bolzen zu verbrauchen.\n" +
                "Bei 50 wird im Schnitt die Hälfte der Pfeile eingespart.\n" +
                "Empfehlung: 30-60%",

                ["Archer_TameHeal_PerLevel"] =
                "【Passiv: Zahmtier-Heilung (HP/s)】\n" +
                "Heilt nahe gezähmte Tiere jede Sekunde um Bogenschütze-Level × diesen Wert.\n" +
                "Bei Lv1 dieser Wert, bei Lv5 das 5-fache.\n" +
                "Empfehlung: 1",

                ["Archer_TameHeal_Range"] =
                "【Passiv: Zahmtier-Heilung Reichweite (m)】\n" +
                "Gezähmte Tiere in dieser Entfernung erhalten die Heilung.\n" +
                "Empfehlung: 8-15",

                ["Mage_Lv1_Cooldown"] =
                "【Abklingzeit Lv1 (Sek.)】\n" +
                "Magier Lv1: Wartezeit bis zur erneuten Verwendung der Fähigkeit.\n" +
                "Empfehlung: 120 Sek.",

                ["Mage_Lv2_Cooldown"] =
                "【Abklingzeit Lv2 (Sek.)】\n" +
                "Magier Lv2: Wartezeit bis zur erneuten Verwendung der Fähigkeit.\n" +
                "Empfehlung: 110 Sek.",

                ["Mage_Lv3_Cooldown"] =
                "【Abklingzeit Lv3 (Sek.)】\n" +
                "Magier Lv3: Wartezeit bis zur erneuten Verwendung der Fähigkeit.\n" +
                "Empfehlung: 100 Sek.",

                ["Mage_Lv4_Cooldown"] =
                "【Abklingzeit Lv4 (Sek.)】\n" +
                "Magier Lv4: Wartezeit bis zur erneuten Verwendung der Fähigkeit.\n" +
                "Empfehlung: 90 Sek.",

                ["Mage_Lv5_Cooldown"] =
                "【Abklingzeit Lv5 (Sek.)】\n" +
                "Magier Lv5: Wartezeit bis zur erneuten Verwendung der Fähigkeit.\n" +
                "Empfehlung: 80 Sek.",

                ["Mage_Lv1_AOE_Max_Targets"] =
                "【Maximale Zielanzahl Lv1】\n" +
                "Magier Lv1: Maximale Anzahl gleichzeitig getroffener Monster. Auswahl nach Nähe.\n" +
                "Empfehlung: 6",

                ["Mage_Lv2_AOE_Max_Targets"] =
                "【Maximale Zielanzahl Lv2】\n" +
                "Magier Lv2: Maximale Anzahl gleichzeitig getroffener Monster.\n" +
                "Empfehlung: 7",

                ["Mage_Lv3_AOE_Max_Targets"] =
                "【Maximale Zielanzahl Lv3】\n" +
                "Magier Lv3: Maximale Anzahl gleichzeitig getroffener Monster.\n" +
                "Empfehlung: 8",

                ["Mage_Lv4_AOE_Max_Targets"] =
                "【Maximale Zielanzahl Lv4】\n" +
                "Magier Lv4: Maximale Anzahl gleichzeitig getroffener Monster.\n" +
                "Empfehlung: 9",

                ["Mage_Lv5_AOE_Max_Targets"] =
                "【Maximale Zielanzahl Lv5】\n" +
                "Magier Lv5: Maximale Anzahl gleichzeitig getroffener Monster.\n" +
                "Empfehlung: 10",

                ["Mage_Lv1_Elemental_Resistance"] =
                "【Elementarresistenz Lv1 (%)】\n" +
                "Magier Lv1 Elementarresistenz. Reduziert Feuer/Frost/Blitz/Gift/Geist.\n" +
                "Empfehlung: 5%",

                ["Mage_Lv2_Elemental_Resistance"] =
                "【Elementarresistenz Lv2 (%)】\n" +
                "Magier Lv2 Elementarresistenz. Beinhaltet +1 zusätzlichen Zauber (innerhalb von 30 Sek.).\n" +
                "Empfehlung: 7%",

                ["Mage_Lv3_Elemental_Resistance"] =
                "【Elementarresistenz Lv3 (%)】\n" +
                "Magier Lv3 Elementarresistenz.\n" +
                "Empfehlung: 9%",

                ["Mage_Lv4_Elemental_Resistance"] =
                "【Elementarresistenz Lv4 (%)】\n" +
                "Magier Lv4 Elementarresistenz.\n" +
                "Empfehlung: 12%",

                ["Mage_Lv5_Elemental_Resistance"] =
                "【Elementarresistenz Lv5 (%)】\n" +
                "Magier Lv5 Elementarresistenz.\n" +
                "Empfehlung: 15%",

                ["Mage_Lv1_Damage_Multiplier"] =
                "【AOE-Schadensmultiplikator Lv1 (%)】\n" +
                "Magier Lv1 AOE-Schadensmultiplikator.\n" +
                "Empfehlung: 70%",

                ["Mage_Lv2_Damage_Multiplier"] =
                "【AOE-Schadensmultiplikator Lv2 (%)】\n" +
                "Magier Lv2 AOE-Schadensmultiplikator.\n" +
                "Empfehlung: 90%",

                ["Mage_Lv3_Damage_Multiplier"] =
                "【AOE-Schadensmultiplikator Lv3 (%)】\n" +
                "Magier Lv3 AOE-Schadensmultiplikator.\n" +
                "Empfehlung: 110%",

                ["Mage_Lv4_Damage_Multiplier"] =
                "【AOE-Schadensmultiplikator Lv4 (%)】\n" +
                "Magier Lv4 AOE-Schadensmultiplikator.\n" +
                "Empfehlung: 130%",

                ["Mage_Lv5_Damage_Multiplier"] =
                "【AOE-Schadensmultiplikator Lv5 (%)】\n" +
                "Magier Lv5 AOE-Schadensmultiplikator.\n" +
                "Empfehlung: 150%",

                ["Tanker_Explosion_Radius"] =
                "【Provokations-Explosionsradius (m)】\n" +
                "Radius des Explosionseffekts beim Aktivieren der Tanker-Provokation.\n" +
                "Empfehlung: 6-12 m",

                ["Tanker_BlockPower_Multiplier"] =
                "【Schild-Blockstärke-Multiplikator】\n" +
                "Multiplikator, der je nach Tanker-Berufslevel auf die Schild-Blockstärke angewendet wird.\n" +
                "Empfehlung: 1,0-2,0",

                ["Rogue_Poison_Range"] =
                "【Giftexplosionsreichweite (m)】\n" +
                "Radius jeder Giftexplosions-VFX.\n" +
                "Empfehlung: 8-15 m",

                ["Rogue_Poison_InstantDamage"] =
                "【Sofortiger Giftschaden】\n" +
                "Sofortiger Giftschaden pro VFX-Auslösung.\n" +
                "Empfehlung: 8-20",

                ["Rogue_Poison_DotDamage"] =
                "【Gift-DoT-Schaden pro Sekunde】\n" +
                "Schaden pro Sekunde durch den Gift-DoT-Effekt.\n" +
                "Empfehlung: 3-8",

                ["Rogue_Poison_DotDuration"] =
                "【Gift-DoT-Dauer (Sek.)】\n" +
                "Dauer des anhaltenden Giftschadeneffekts.\n" +
                "Empfehlung: 8-15 Sek.",

                ["Rogue_Poison_VFXCount"] =
                "【Giftexplosionsanzahl】\n" +
                "Anzahl der Wiederholungen der Giftexplosions-VFX.\n" +
                "Empfehlung: 6-10",

                ["Rogue_Poison_VFXInterval"] =
                "【Giftexplosionsintervall (Sek.)】\n" +
                "Zeit zwischen den einzelnen Giftexplosionen.\n" +
                "Empfehlung: 0,3-1,0 Sek.",

                ["Rogue_Lv2_Cooldown"] = "【Lv2 Schattenschlag-Abklingzeit (Sek.)】\nEmpfehlung: 25-30 Sek.",

                ["Rogue_Lv3_Cooldown"] = "【Lv3 Schattenschlag-Abklingzeit (Sek.)】\nEmpfehlung: 22-28 Sek.",

                ["Rogue_Lv4_Cooldown"] = "【Lv4 Schattenschlag-Abklingzeit (Sek.)】\nEmpfehlung: 20-26 Sek.",

                ["Rogue_Lv5_Cooldown"] = "【Lv5 Schattenschlag-Abklingzeit (Sek.)】\nEmpfehlung: 18-24 Sek.",

                ["Rogue_Lv2_AttackBonus"] = "【Lv2 Angriffs-Buff (%)】\nEmpfehlung: 35-50%",

                ["Rogue_Lv3_AttackBonus"] = "【Lv3 Angriffs-Buff (%)】\nEmpfehlung: 40-55%",

                ["Rogue_Lv4_AttackBonus"] = "【Lv4 Angriffs-Buff (%)】\nEmpfehlung: 45-60%",

                ["Rogue_Lv5_AttackBonus"] = "【Lv5 Angriffs-Buff (%)】\nEmpfehlung: 50-65%",

                ["Rogue_Lv2_BuffDuration"] = "【Lv2 Buff-Dauer (Sek.)】\nEmpfehlung: 8-12 Sek.",

                ["Rogue_Lv3_BuffDuration"] = "【Lv3 Buff-Dauer (Sek.)】\nEmpfehlung: 9-13 Sek.",

                ["Rogue_Lv4_BuffDuration"] = "【Lv4 Buff-Dauer (Sek.)】\nEmpfehlung: 10-14 Sek.",

                ["Rogue_Lv5_BuffDuration"] = "【Lv5 Buff-Dauer (Sek.)】\nEmpfehlung: 11-15 Sek.",

                ["Rogue_Lv2_PoisonBlasts"] = "【Lv2 Giftexplosionsanzahl】\nEmpfehlung: 8-12",

                ["Rogue_Lv3_PoisonBlasts"] = "【Lv3 Giftexplosionsanzahl】\nEmpfehlung: 9-13",

                ["Rogue_Lv4_PoisonBlasts"] = "【Lv4 Giftexplosionsanzahl】\nEmpfehlung: 10-14",

                ["Rogue_Lv5_PoisonBlasts"] = "【Lv5 Giftexplosionsanzahl】\nEmpfehlung: 11-15",

                ["Rogue_Lv2_PoisonInstant"] = "【Lv2 Sofortiger Giftschaden】\nEmpfehlung: 10-15",

                ["Rogue_Lv3_PoisonInstant"] = "【Lv3 Sofortiger Giftschaden】\nEmpfehlung: 12-18",

                ["Rogue_Lv4_PoisonInstant"] = "【Lv4 Sofortiger Giftschaden】\nEmpfehlung: 14-20",

                ["Rogue_Lv5_PoisonInstant"] = "【Lv5 Sofortiger Giftschaden】\nEmpfehlung: 16-25",

                ["Rogue_Lv2_PoisonDot"] = "【Lv2 Gift-DoT pro Sekunde】\nEmpfehlung: 5-8",

                ["Rogue_Lv3_PoisonDot"] = "【Lv3 Gift-DoT pro Sekunde】\nEmpfehlung: 6-9",

                ["Rogue_Lv4_PoisonDot"] = "【Lv4 Gift-DoT pro Sekunde】\nEmpfehlung: 7-10",

                ["Rogue_Lv5_PoisonDot"] = "【Lv5 Gift-DoT pro Sekunde】\nEmpfehlung: 8-12",

                ["Rogue_ShadowStrike_Charges"] = "【Schattenschlag-Basisladungen】\nGrundanzahl der verfügbaren Ladungen.\nEmpfehlung: 1",

                ["Rogue_Lv5_BonusCharges"] = "【Lv5 Zusatzladungen】\nZusätzliche Ladungen, die auf Lv5 freigeschaltet werden.\nEmpfehlung: 1",

                ["Rogue_Lv2_AttackSpeed"] = "【Lv2 Angriffsgeschwindigkeitsbonus (%)】\nEmpfehlung: 10-15%",

                ["Rogue_Lv3_AttackSpeed"] = "【Lv3 Angriffsgeschwindigkeitsbonus (%)】\nEmpfehlung: 12-18%",

                ["Rogue_Lv4_AttackSpeed"] = "【Lv4 Angriffsgeschwindigkeitsbonus (%)】\nEmpfehlung: 14-20%",

                ["Rogue_Lv5_AttackSpeed"] = "【Lv5 Angriffsgeschwindigkeitsbonus (%)】\nEmpfehlung: 16-22%",

                ["Rogue_Lv2_StaminaReduction"] = "【Lv2 Ausdauerreduzierung (%)】\nEmpfehlung: 15-20%",

                ["Rogue_Lv3_StaminaReduction"] = "【Lv3 Ausdauerreduzierung (%)】\nEmpfehlung: 17-22%",

                ["Rogue_Lv4_StaminaReduction"] = "【Lv4 Ausdauerreduzierung (%)】\nEmpfehlung: 19-25%",

                ["Rogue_Lv5_StaminaReduction"] = "【Lv5 Ausdauerreduzierung (%)】\nEmpfehlung: 22-30%",

                ["Rogue_Lv1_MoveSpeed"] = "【Lv1 Bewegungsgeschwindigkeitsbonus (%)】\nEmpfehlung: 3-7%",

                ["Rogue_Lv2_MoveSpeed"] = "【Lv2 Bewegungsgeschwindigkeitsbonus (%)】\nEmpfehlung: 5-10%",

                ["Rogue_Lv3_MoveSpeed"] = "【Lv3 Bewegungsgeschwindigkeitsbonus (%)】\nEmpfehlung: 7-12%",

                ["Rogue_Lv4_MoveSpeed"] = "【Lv4 Bewegungsgeschwindigkeitsbonus (%)】\nEmpfehlung: 10-15%",

                ["Rogue_Lv5_MoveSpeed"] = "【Lv5 Bewegungsgeschwindigkeitsbonus (%)】\nEmpfehlung: 12-18%",

                ["Producer_Durability_Lv1"] = "【Haltbarkeitsbonus für hergestellte Gegenstände Lv1 (%)】\nHaltbarkeitsbonus hergestellter Gegenstände bei Lv1.\nStandard: 50%",

            };
        }
    }
}

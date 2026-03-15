using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetSwordKnifeDescriptions_DE()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Schwert-Skilltree (Sword Tree)
                // ========================================

                // === Tier 0: Schwert-Experte ===
                ["Sword_Expert_DamageIncrease"] =
                "【Schwertvschadenserhöhung (%)】\n" +
                "Erhöht den Grundangriff von Schwertern.\n" +
                "Gilt für alle Schwerttypen.\n" +
                "Empfehlung: 10-20%",

                ["Tier0_SwordExpert_DamageBonus"] =
                "【Schwertschadenserhöhung (%)】\n" +
                "Erhöht den Grundangriff von Schwertern.\n" +
                "Gilt für alle Schwerttypen.\n" +
                "Empfehlung: 10-20%",

                ["Tier0_SwordExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Schwert-Experten.",

                // === Tier 1: Schnellschnitt ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die Angriffsgeschwindigkeit mit dem Schwert.\n" +
                "Ermöglicht schnelle aufeinanderfolgende Angriffe.\n" +
                "Empfehlung: 10-20%",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die Angriffsgeschwindigkeit mit dem Schwert.\n" +
                "Ermöglicht schnelle aufeinanderfolgende Angriffe.\n" +
                "Empfehlung: 10-20%",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Schnellschnitts.",

                // === Tier 1: Gegenschlag-Haltung ===
                ["Tier1_CounterStance_Duration"] =
                "【Dauer (Sek.)】\n" +
                "Wirkungsdauer der Gegenschlag-Haltung.\n" +
                "In dieser Zeit wird die Verteidigung erhöht.\n" +
                "Empfehlung: 3-6 Sek.",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【Verteidigungsbonus (%)】\n" +
                "Verteidigungserhöhung in der Gegenschlag-Haltung.\n" +
                "Halte stand und warte auf den Moment des Gegenschlags.\n" +
                "Empfehlung: 20-40%",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Gegenschlag-Haltung.",

                // === Tier 2: Komboschnitt ===
                ["Sword_ComboSlash_Bonus"] =
                "【Kombo-Angriffsbonus (%)】\n" +
                "Verursacht zusätzlichen Schaden bei Kombo-Angriffen.\n" +
                "Hohe Kombo aufrechterhalten garantiert hohen DPS.\n" +
                "Empfehlung: 15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Kombo-Angriffsbonus.\n" +
                "Angriffe in dieser Zeit verlängern den Buff.\n" +
                "Empfehlung: 3-5 Sek.",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【Kombo-Angriffsbonus (%)】\n" +
                "Verursacht zusätzlichen Schaden bei Kombo-Angriffen.\n" +
                "Hohe Kombo aufrechterhalten garantiert hohen DPS.\n" +
                "Empfehlung: 15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Kombo-Angriffsbonus.\n" +
                "Angriffe in dieser Zeit verlängern den Buff.\n" +
                "Empfehlung: 3-5 Sek.",

                ["Tier2_ComboSlash_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Komboschnnitts.",

                // === Tier 3: Klingenreflex / Konter ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【Angriffsbonus (Fest)】\n" +
                "Erhöht den Angriff des Klingenreflexes um einen festen Wert.\n" +
                "Nach dem Parieren ein mächtiger Gegenangriff.\n" +
                "Empfehlung: 20-40",

                ["Tier3_Riposte_DamageBonus"] =
                "【Angriffsbonus (Fest)】\n" +
                "Erhöht den Angriff des Klingenreflexes um einen festen Wert.\n" +
                "Nach dem Parieren ein mächtiger Gegenangriff.\n" +
                "Empfehlung: 5-15",

                ["Tier3_Riposte_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Konters.",

                // === Tier 4: Angriff und Verteidigung ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【Angriffsbonus (%)】\n" +
                "Stärkt Angriff und Verteidigung gleichzeitig.\n" +
                "Nützlich für einen ausgewogenen Kampfstil.\n" +
                "Empfehlung: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【Verteidigungsbonus (Fest)】\n" +
                "Verteidigungsbonus der 'Angriff und Verteidigung'-Haltung.\n" +
                "Solide Verteidigung auch beim Angreifen.\n" +
                "Empfehlung: 15-30",

                ["Tier4_AllInOne_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von 'Angriff und Verteidigung'.",

                // === Tier 4: Wahres Duell ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Angriffsgeschwindigkeitsbonus für den Einzelkampf.\n" +
                "Schnelle Schläge überwältigen den Feind.\n" +
                "Empfehlung: 15-30%",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Angriffsgeschwindigkeitsbonus für den Einzelkampf.\n" +
                "Schnelle Schläge überwältigen den Feind.\n" +
                "Empfehlung: 15-30%",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des wahren Duells.",

                // === Tier 5: Parier-Ansturm (Aktiv H) ===
                ["Sword_ParryCharge_BuffDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Buffs nach erfolgreichem Parieren.\n" +
                "In dieser Zeit ist der verbesserte Angriff verfügbar.\n" +
                "Empfehlung: 5-10 Sek.",

                ["Sword_ParryCharge_DamageBonus"] =
                "【Ansturm-Schadensbonus (%)】\n" +
                "Schadenserhöhung des Ansturms nach dem Parieren.\n" +
                "Perfektes Timing = mächtiger Gegenangriff.\n" +
                "Empfehlung: 50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【Stoßdistanz (m)】\n" +
                "Wie viele Meter der Feind beim Ansturm gestoßen wird.\n" +
                "Nützlich zum Anpassen der Distanz und Schlachtkontrolle.\n" +
                "Empfehlung: 3-7 m",

                ["Sword_ParryCharge_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Buff-Aktivierung (Taste H).\n" +
                "Ausdauermanagement ist wichtig.\n" +
                "Empfehlung: 20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Weniger = häufigere Nutzung möglich.\n" +
                "Empfehlung: 10-20 Sek.",

                ["Tier5_ParryRush_BuffDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Buffs nach erfolgreichem Parieren.\n" +
                "Empfehlung: 20-40 Sek.",

                ["Tier5_ParryRush_DamageBonus"] =
                "【Ansturm-Schadensbonus (%)】\n" +
                "Schadenserhöhung des Ansturms nach dem Parieren.\n" +
                "Empfehlung: 50-100%",

                ["Tier5_ParryRush_PushDistance"] =
                "【Stoßdistanz (m)】\n" +
                "Wie viele Meter der Feind beim Ansturm gestoßen wird.\n" +
                "Empfehlung: 3-7 m",

                ["Tier5_ParryRush_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Fähigkeitsaktivierung.\n" +
                "Empfehlung: 10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 30-60 Sek.",

                ["Tier5_ParryRush_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Parier-Ansturms.",

                // === Tier 6: Ansturm-Schnittserie (Aktiv G) ===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【1. Treffer Schaden (%)】\n" +
                "Schaden des ersten Treffers der Ansturm-Schnittserie.\n" +
                "Multiplikator im Verhältnis zum Grundangriff.\n" +
                "Empfehlung: 80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【2. Treffer Schaden (%)】\n" +
                "Schaden des zweiten Treffers der Ansturm-Schnittserie.\n" +
                "Die Kombo steigert sich und der Schaden wächst.\n" +
                "Empfehlung: 100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【3. Treffer Schaden (%)】\n" +
                "Schaden des Abschlusstreffers der Ansturm-Schnittserie.\n" +
                "Der mächtigste Abschlusstreffer.\n" +
                "Empfehlung: 150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【Ansturmdistanz (m)】\n" +
                "Distanz des Ansturms zu Beginn der Fähigkeit.\n" +
                "Nähert sich schnell dem Feind.\n" +
                "Empfehlung: 5-10 m",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【Seitliche Bewegungsdistanz (m)】\n" +
                "Bewegungsdistanz links/rechts während der Angriffe.\n" +
                "Gleichzeitig ausweichen und angreifen.\n" +
                "Empfehlung: 2-5 m",

                ["Sword_RushSlash_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Fähigkeitsnutzung (Taste G).\n" +
                "Mächtige Fähigkeit erfordert viel Ausdauer.\n" +
                "Empfehlung: 40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Weniger = häufigere Nutzung möglich.\n" +
                "Empfehlung: 15-30 Sek.",

                ["Sword_RushSlash_MovementSpeed"] =
                "【Bewegungsgeschwindigkeit (m/s)】\n" +
                "Bewegungsgeschwindigkeit während des Ansturms.\n" +
                "Schneller = dynamischerer Kampf.\n" +
                "Empfehlung: 8-15 m/s",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Angriffsgeschwindigkeitsbonus während der Fähigkeit.\n" +
                "Geschwindigkeiten anderer Bäume werden ignoriert, nur dieser Wert gilt.\n" +
                "Empfehlung: 20-40%",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【1. Treffer Schaden (%)】\n" +
                "Schaden des ersten Treffers der Ansturm-Schnittserie.\n" +
                "Empfehlung: 60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【2. Treffer Schaden (%)】\n" +
                "Schaden des zweiten Treffers der Ansturm-Schnittserie.\n" +
                "Empfehlung: 70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【3. Treffer Schaden (%)】\n" +
                "Schaden des Abschlusstreffers der Ansturm-Schnittserie.\n" +
                "Empfehlung: 80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【Initiale Ansturmdistanz (m)】\n" +
                "Distanz des Ansturms zu Beginn der Fähigkeit.\n" +
                "Empfehlung: 3-8 m",

                ["Tier6_RushSlash_SideDistance"] =
                "【Seitliche Bewegungsdistanz (m)】\n" +
                "Bewegungsdistanz links/rechts während der Angriffe.\n" +
                "Empfehlung: 2-5 m",

                ["Tier6_RushSlash_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Fähigkeitsnutzung (G).\n" +
                "Empfehlung: 20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 15-30 Sek.",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【Bewegungsgeschwindigkeit (m/s)】\n" +
                "Bewegungsgeschwindigkeit während des Ansturms.\n" +
                "Empfehlung: 10-25 m/s",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【Angriffsgeschwindigkeitsbonus (%)】\n" +
                "Angriffsgeschwindigkeitsbonus während der Fähigkeit.\n" +
                "Empfehlung: 150-250%",

                ["Tier6_RushSlash_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Ansturm-Schnittserie.",

                // ========================================
                // Messer-Skilltree (Knife Tree)
                // ========================================

                // === Tier 0: Messer-Experte ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【Rückstich-Schadensbonus (%)】\n" +
                "Zusätzlicher Schaden beim Angriff von hinten.\n" +
                "Grundfähigkeit des Assassinen.\n" +
                "Empfehlung: 30-50%",

                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Messer-Experten.",

                // === Tier 1: Ausweichmeisterschaft ===
                ["Tier1_Evasion_Chance"] =
                "【Ausweichance (%)】\n" +
                "Chance, feindlichen Angriffen auszuweichen.\n" +
                "Höher = weniger Treffer erhalten.\n" +
                "Empfehlung: 15-25%",

                ["Tier1_Evasion_Duration"] =
                "【Unverwundbarkeitsdauer (Sek.)】\n" +
                "Unverwundbarkeitszeit nach erfolgreichem Ausweichen.\n" +
                "Empfehlung: 2-4 Sek.",

                ["Tier1_Evasion_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Ausweichmeisterschaft.",

                // === Tier 2: Schnelle Bewegung ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【Bewegungsgeschwindigkeitsbonus (%)】\n" +
                "Erhöht die Grundbewegungsgeschwindigkeit.\n" +
                "Hohe Mobilität um Feinde zu verwirren.\n" +
                "Empfehlung: 10-20%",

                ["Tier2_FastMove_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der schnellen Bewegung.",

                // === Tier 3: Kampfmeisterschaft ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【Schadensbonus (Fest)】\n" +
                "Fügt jedem Angriff festen Schaden hinzu.\n" +
                "Empfehlung: 1-4",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Kampfmeisterschaft-Buffs.\n" +
                "Empfehlung: 8-12 Sek.",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten der Kampfmeisterschaft.",

                // === Tier 4: Angriff und Ausweichen ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【Ausweichance-Bonus (%)】\n" +
                "Erhöht die Ausweichance beim gleichzeitigen Angriff.\n" +
                "Aggressive Verteidigung.\n" +
                "Empfehlung: 20-30%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des erhöhten Ausweicheffekts.\n" +
                "Empfehlung: 8-12 Sek.",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur Buff-Reaktivierung.\n" +
                "Empfehlung: 25-35 Sek.",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten von 'Angriff und Ausweichen'.",

                // === Tier 5: Kritischer Schaden ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【Schadensbonus (%)】\n" +
                "Erhöht den Schaden kritischer Treffer.\n" +
                "Gute Synergie mit der hohen Kritchance des Messers.\n" +
                "Empfehlung: 20-35%",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des kritischen Schadens.",

                // === Tier 6: Assassine ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【Kritischer Schadensbonus (%)】\n" +
                "Erhöht den Schaden kritischer Treffer weiter.\n" +
                "Empfehlung: 20-30%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【Kritische Trefferchance-Bonus (%)】\n" +
                "Erhöht die Wahrscheinlichkeit kritischer Treffer.\n" +
                "Empfehlung: 10-18%",

                ["Tier6_Assassin_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Assassinen.",

                // === Tier 7: Meuchelmord ===
                ["Tier7_Assassination_StaggerChance"] =
                "【Betäubungschance (%)】\n" +
                "Chance, den Feind bei einer Kombo zu betäuben.\n" +
                "Unterbricht den feindlichen Angriff.\n" +
                "Empfehlung: 30-45%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【Benötigte Kombo-Treffer】\n" +
                "Anzahl aufeinanderfolgender Treffer zum Auslösen der Betäubung.\n" +
                "Empfehlung: 2-4 Treffer",

                ["Tier7_Assassination_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Meuchelmordes.",

                // === Tier 8: Assassinenherz (Aktiv G) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【Kritischer Schadensmultiplikator】\n" +
                "Aktiv G — Kritischer Schadensmultiplikator des Assassinenherzens.\n" +
                "Teleportiert hinter den Feind und führt eine tödliche Serie aus.\n" +
                "Empfehlung: 1,2-1,5x",

                ["Tier8_AssassinHeart_Duration"] =
                "【Buff-Dauer (Sek.)】\n" +
                "Wirkungsdauer des Assassinenherz-Buffs.\n" +
                "Empfehlung: 5-10 Sek.",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【Ausdauerkosten】\n" +
                "Ausdauer bei Fähigkeitsnutzung.\n" +
                "Empfehlung: 15-25",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【Abklingzeit (Sek.)】\n" +
                "Wartezeit bis zur erneuten Verwendung.\n" +
                "Empfehlung: 35-50 Sek.",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【Teleportreichweite (m)】\n" +
                "Maximale Suchreichweite des Feindes.\n" +
                "Teleportiert hinter den Feind innerhalb dieses Radius.\n" +
                "Empfehlung: 6-10 m",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【Positionierungsdistanz hinter dem Feind (m)】\n" +
                "Distanz der Positionierung hinter dem Feind.\n" +
                "Distanz für den Rückstichangriff.\n" +
                "Empfehlung: 0,8-1,5 m",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【Betäubungsdauer (Sek.)】\n" +
                "Zeit, in der der Feind betäubt ist.\n" +
                "Empfehlung: 0,5-2 Sek.",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【Anzahl der Kombo-Treffer】\n" +
                "Anzahl automatischer Treffer nach dem Teleport.\n" +
                "Empfehlung: 2-4 Treffer",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【Trefferintervall (Sek.)】\n" +
                "Zeit zwischen den Treffern der Serie.\n" +
                "Weniger = sofortige Treffer.\n" +
                "Empfehlung: 0,2-0,5 Sek.",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【Benötigte Punkte】\nPunkte zum Freischalten des Assassinenherzens.",
            };
        }
    }
}

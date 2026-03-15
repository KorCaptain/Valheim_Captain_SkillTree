using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetGermanKeyNames_Part2()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Speer-Skilltree - 35 Schlüssel
                // ============================================

                // === Tier 0: Speerspezialist (4) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: [Speerspezialist] Benötigte Punkte",
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: [Speerspezialist] Geschwindigkeitsbonus 2. Treffer (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: [Speerspezialist] Schadensbonus 2. Treffer (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: [Speerspezialist] Effektdauer (Sek)",

                // === Tier 1: Vitalstreich (2) ===
                ["Tier1_QuickStrike_RequiredPoints"] = "Tier 1: [Vitalstreich] Benötigte Punkte",
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: [Vitalstreich] Kritischer Schadensbonus (%)",

                // === Tier 2: Speerwurf (3) ===
                ["Tier2_Throw_RequiredPoints"] = "Tier 2: [Speerwurf] Benötigte Punkte",
                ["Tier2_Throw_Cooldown"] = "Tier 2: [Speerwurf] Abklingzeit (Sek)",
                ["Tier2_Throw_DamageMultiplier"] = "Tier 2: [Speerwurf] Schadensbonus (%)",

                // === Tier 3-1: Schnelle Durchdringung (2) ===
                ["Tier3_Pierce_RequiredPoints"] = "Tier 3-1: [Schnelle Durchdringung] Benötigte Punkte",
                ["Tier3_Rapid_DamageBonus"] = "Tier 3-1: [Schnelle Durchdringung] Durchdringungsschadensbonus",

                // === Tier 3-2: Schneller Speer (1) ===
                ["Tier3_QuickSpear_AttackSpeed"] = "Tier 3-2: [Schneller Speer] Angriffsgeschwindigkeit (%)",

                // === Tier 4-1: Ausweichstoß (3) ===
                ["Tier4_Evasion_RequiredPoints"] = "Tier 4-1: [Ausweichstoß] Benötigte Punkte",
                ["Tier4_Evasion_EvasionBonus"] = "Tier 4-1: [Ausweichstoß] Ausweichbonus beim Angriff (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4-1: [Ausweichstoß] Ausdauerkosten-Reduzierung (%)",

                // === Tier 4-2: Doppelstoß (2) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4-2: [Doppelstoß] Schadensbonus 2. Treffer (%)",
                ["Tier4_Dual_Duration"] = "Tier 4-2: [Doppelstoß] Buff-Dauer (Sek)",

                // === Tier 5-1: Durchdringender Speer (6) ===
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5-1: [Durchdringender Speer] Benötigte Punkte",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5-1: [Durchdringender Speer] Buff-Dauer (Sek)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5-1: [Durchdringender Speer] Blitzschadensbonus (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5-1: [Durchdringender Speer] Treffer bis Blitzauslösung",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5-1: [Durchdringender Speer] G-Taste Abklingzeit (Sek)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5-1: [Durchdringender Speer] G-Taste Ausdauerkosten",

                // === Tier 5-2: Combo-Speer (8) ===
                ["Tier5_Combo_RequiredPoints"] = "Tier 5-2: [Combo-Speer] Benötigte Punkte",
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5-2: [Combo-Speer] H-Taste Abklingzeit (Sek)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5-2: [Combo-Speer] H-Taste Schadensbonus (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5-2: [Combo-Speer] H-Taste Ausdauerkosten",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5-2: [Combo-Speer] H-Taste Rückstoßreichweite (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5-2: [Combo-Speer] Aktive Effektreichweite (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5-2: [Combo-Speer] Buff-Dauer (Sek)",
                ["Tier5_Combo_MaxUses"] = "Tier 5-2: [Combo-Speer] Max. verbesserte Würfe",

                // ============================================
                // Stab-Skilltree - 30 Schlüssel
                // ============================================

                // === Tier 0: Stabspezialist (2) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] = "Tier 0: [Stabspezialist] Elementarschadensbonus (%)",
                ["Tier0_StaffExpert_RequiredPoints"] = "Tier 0: [Stabspezialist] Benötigte Punkte",

                // === Tier 1-1: Geistiger Fokus (2) ===
                ["Tier1_MindFocus_EitrReduction"] = "Tier 1-1: [Geistiger Fokus] Eitr-Kosten-Reduzierung (%)",
                ["Tier1_MindFocus_RequiredPoints"] = "Tier 1-1: [Geistiger Fokus] Benötigte Punkte",

                // === Tier 1-2: Magiefluss (2) ===
                ["Tier1_MagicFlow_EitrBonus"] = "Tier 1-2: [Magiefluss] Max. Eitr-Bonus",
                ["Tier1_MagicFlow_RequiredPoints"] = "Tier 1-2: [Magiefluss] Benötigte Punkte",

                // === Tier 2: Magieverstärkung (4) ===
                ["Tier2_MagicAmplify_Chance"] = "Tier 2: [Magieverstärkung] Aktivierungschance (%)",
                ["Tier2_MagicAmplify_DamageBonus"] = "Tier 2: [Magieverstärkung] Elementarschadensbonus (%)",
                ["Tier2_MagicAmplify_EitrCostIncrease"] = "Tier 2: [Magieverstärkung] Eitr-Kostensteigerung (%)",
                ["Tier2_MagicAmplify_RequiredPoints"] = "Tier 2: [Magieverstärkung] Benötigte Punkte",

                // === Tier 3-1: Frostzauber (2) ===
                ["Tier3_FrostElement_DamageBonus"] = "Tier 3-1: [Frostzauber] Schadensbonus",
                ["Tier3_FrostElement_RequiredPoints"] = "Tier 3-1: [Frostzauber] Benötigte Punkte",

                // === Tier 3-2: Feuerzauber (2) ===
                ["Tier3_FireElement_DamageBonus"] = "Tier 3-2: [Feuerzauber] Schadensbonus",
                ["Tier3_FireElement_RequiredPoints"] = "Tier 3-2: [Feuerzauber] Benötigte Punkte",

                // === Tier 3-3: Blitzzauber (2) ===
                ["Tier3_LightningElement_DamageBonus"] = "Tier 3-3: [Blitzzauber] Schadensbonus",
                ["Tier3_LightningElement_RequiredPoints"] = "Tier 3-3: [Blitzzauber] Benötigte Punkte",

                // === Tier 4: Glücksmagie (2) ===
                ["Tier4_LuckyMana_Chance"] = "Tier 4: [Glücksmagie] Chance auf kostenlose Beschwörung (%)",
                ["Tier4_LuckyMana_RequiredPoints"] = "Tier 4: [Glücksmagie] Benötigte Punkte",

                // === Tier 5-1: Schnellsalve - Aktiv R-Taste (6) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] = "Tier 5-1: [Schnellsalve] Zusätzliche Projektile",
                ["Tier5_DoubleCast_ProjectileDamagePercent"] = "Tier 5-1: [Schnellsalve] Projektilschaden (%)",
                ["Tier5_DoubleCast_AngleOffset"] = "Tier 5-1: [Schnellsalve] Winkelversatz (Nicht verwendet)",
                ["Tier5_DoubleCast_EitrCost"] = "Tier 5-1: [Schnellsalve] Eitr-Kosten",
                ["Tier5_DoubleCast_Cooldown"] = "Tier 5-1: [Schnellsalve] Abklingzeit (Sek)",
                ["Tier5_DoubleCast_RequiredPoints"] = "Tier 5-1: [Schnellsalve] Benötigte Punkte",

                // === Tier 5-2: Sofortflächenheilung - Aktiv H-Taste (5) ===
                ["Tier5_InstantAreaHeal_Cooldown"] = "Tier 5-2: [Heilung] Abklingzeit (Sek)",
                ["Tier5_InstantAreaHeal_EitrCost"] = "Tier 5-2: [Heilung] Eitr-Kosten",
                ["Tier5_InstantAreaHeal_HealPercent"] = "Tier 5-2: [Heilung] Heilungsmenge (% Max-LP)",
                ["Tier5_InstantAreaHeal_Range"] = "Tier 5-2: [Heilung] Heilungsreichweite (m)",
                ["Tier5_InstantAreaHeal_RequiredPoints"] = "Tier 5-2: [Heilung] Benötigte Punkte",

                // ============================================
                // Armbrust-Skilltree - 34 Schlüssel
                // ============================================

                // === Tier 0: Armbrusstspezialist (2) ===
                ["Tier0_CrossbowExpert_DamageBonus"] = "Tier 0: [Armbrusstspezialist] Armbrust-Schadensbonus (%)",
                ["Tier0_CrossbowExpert_RequiredPoints"] = "Tier 0: [Armbrusstspezialist] Benötigte Punkte",

                // === Tier 1: Schnellfeuer (6) ===
                ["Tier1_RapidFire_Chance"] = "Tier 1: [Schnellfeuer] Aktivierungschance (%)",
                ["Tier1_RapidFire_ShotCount"] = "Tier 1: [Schnellfeuer] Schussanzahl",
                ["Tier1_RapidFire_DamagePercent"] = "Tier 1: [Schnellfeuer] Schaden pro Schuss (%)",
                ["Tier1_RapidFire_Delay"] = "Tier 1: [Schnellfeuer] Verzögerung zwischen Schüssen (Sek)",
                ["Tier1_RapidFire_BoltConsumption"] = "Tier 1: [Schnellfeuer] Bolzenverbrauch",
                ["Tier1_RapidFire_RequiredPoints"] = "Tier 1: [Schnellfeuer] Benötigte Punkte",

                // === Tier 2: Ausgewogenes Zielen / Schnellladen / Echter Schuss (8) ===
                ["Tier2_BalancedAim_KnockbackChance"] = "Tier 2-1: [Ausgewogenes Zielen] Rückstoßchance (%)",
                ["Tier2_BalancedAim_KnockbackDistance"] = "Tier 2-1: [Ausgewogenes Zielen] Rückstoßdistanz (m)",
                ["Tier2_BalancedAim_RequiredPoints"] = "Tier 2-1: [Ausgewogenes Zielen] Benötigte Punkte",
                ["Tier2_RapidReload_SpeedIncrease"] = "Tier 2-2: [Schnellladen] Ladegeschwindigkeitsbonus (%)",
                ["Tier2_RapidReload_RequiredPoints"] = "Tier 2-2: [Schnellladen] Benötigte Punkte",
                ["Tier2_HonestShot_DamageBonus"] = "Tier 2-3: [Echter Schuss] Schadensbonus (%)",
                ["Tier2_HonestShot_RequiredPoints"] = "Tier 2-3: [Echter Schuss] Benötigte Punkte",

                // === Tier 3: Automatisches Nachladen (2) ===
                ["Tier3_AutoReload_Chance"] = "Tier 3: [Automatisches Nachladen] Aktivierungschance (%)",
                ["Tier3_AutoReload_RequiredPoints"] = "Tier 3: [Automatisches Nachladen] Benötigte Punkte",

                // === Tier 4: Schnellfeuer Lv2 / Erster Angriff (9) ===
                ["Tier4_RapidFireLv2_Chance"] = "Tier 4-1: [Schnellfeuer Lv2] Aktivierungschance (%)",
                ["Tier4_RapidFireLv2_ShotCount"] = "Tier 4-1: [Schnellfeuer Lv2] Schussanzahl",
                ["Tier4_RapidFireLv2_DamagePercent"] = "Tier 4-1: [Schnellfeuer Lv2] Schaden pro Schuss (%)",
                ["Tier4_RapidFireLv2_Delay"] = "Tier 4-1: [Schnellfeuer Lv2] Verzögerung zwischen Schüssen (Sek)",
                ["Tier4_RapidFireLv2_BoltConsumption"] = "Tier 4-1: [Schnellfeuer Lv2] Bolzenverbrauch",
                ["Tier4_RapidFireLv2_RequiredPoints"] = "Tier 4-1: [Schnellfeuer Lv2] Benötigte Punkte",
                ["Tier4_FinalStrike_HpThreshold"] = "Tier 4-2: [Erster Angriff] Gegner-LP-Schwellenwert (%)",
                ["Tier4_FinalStrike_DamageBonus"] = "Tier 4-2: [Erster Angriff] Schadensbonus (%)",
                ["Tier4_FinalStrike_RequiredPoints"] = "Tier 4-2: [Erster Angriff] Benötigte Punkte",

                // === Tier 5: Einschuss - Aktiv R-Taste (5) ===
                ["Tier5_OneShot_Duration"] = "Tier 5: [Einschuss] Buff-Dauer (Sek)",
                ["Tier5_OneShot_DamageBonus"] = "Tier 5: [Einschuss] Schadensbonus (%)",
                ["Tier5_OneShot_KnockbackDistance"] = "Tier 5: [Einschuss] Rückstoßdistanz (m)",
                ["Tier5_OneShot_Cooldown"] = "Tier 5: [Einschuss] Abklingzeit (Sek)",
                ["Tier5_OneShot_RequiredPoints"] = "Tier 5: [Einschuss] Benötigte Punkte",

                // ============================================
                // Messer-Skilltree - 32 Schlüssel
                // ============================================

                // === Tier 0: Messerspezialist (2) ===
                ["Tier0_KnifeExpert_BackstabBonus"] = "Tier 0: [Messerspezialist] Rückenangriffs-Bonus (%)",
                ["Tier0_KnifeExpert_RequiredPoints"] = "Tier 0: [Messerspezialist] Benötigte Punkte",

                // === Tier 1: Ausweichmeisterschaft (3) ===
                ["Tier1_Evasion_Chance"] = "Tier 1: [Ausweichmeisterschaft] Aktivierungschance (%)",
                ["Tier1_Evasion_Duration"] = "Tier 1: [Ausweichmeisterschaft] Dauer (Sek)",
                ["Tier1_Evasion_RequiredPoints"] = "Tier 1: [Ausweichmeisterschaft] Benötigte Punkte",

                // === Tier 2: Schnelle Bewegung (2) ===
                ["Tier2_FastMove_MoveSpeedBonus"] = "Tier 2: [Schnelle Bewegung] Bewegungsgeschwindigkeitsbonus (%)",
                ["Tier2_FastMove_RequiredPoints"] = "Tier 2: [Schnelle Bewegung] Benötigte Punkte",

                // === Tier 3: Schnellangriff (3) ===
                ["Tier3_CombatMastery_DamageBonus"] = "Tier 3: [Schnellangriff] Hieb-/Durchdringungs-Schadensbonus",
                ["Tier3_CombatMastery_BuffDuration"] = "Tier 3: [Schnellangriff] Buff-Dauer (Sek)",
                ["Tier3_CombatMastery_RequiredPoints"] = "Tier 3: [Schnellangriff] Benötigte Punkte",

                // === Tier 4: Kritische Meisterschaft (4) ===
                ["Tier4_AttackEvasion_EvasionBonus"] = "Tier 4: [Kritische Meisterschaft] Ausweichbonus (%)",
                ["Tier4_AttackEvasion_BuffDuration"] = "Tier 4: [Kritische Meisterschaft] Buff-Dauer (Sek)",
                ["Tier4_AttackEvasion_Cooldown"] = "Tier 4: [Kritische Meisterschaft] Abklingzeit (Sek)",
                ["Tier4_AttackEvasion_RequiredPoints"] = "Tier 4: [Kritische Meisterschaft] Benötigte Punkte",

                // === Tier 5: Tödlicher Schaden (2) ===
                ["Tier5_CriticalDamage_DamageBonus"] = "Tier 5: [Tödlicher Schaden] Schadensbonus (%)",
                ["Tier5_CriticalDamage_RequiredPoints"] = "Tier 5: [Tödlicher Schaden] Benötigte Punkte",

                // === Tier 6: Assassine (3) ===
                ["Tier6_Assassin_CritDamageBonus"] = "Tier 6: [Assassine] Kritischer Schadensbonus (%)",
                ["Tier6_Assassin_CritChanceBonus"] = "Tier 6: [Assassine] Kritische Trefferchance-Bonus (%)",
                ["Tier6_Assassin_RequiredPoints"] = "Tier 6: [Assassine] Benötigte Punkte",

                // === Tier 7: Attentat (3) ===
                ["Tier7_Assassination_StaggerChance"] = "Tier 7: [Attentat] Stagger-Chance (%)",
                ["Tier7_Assassination_RequiredComboHits"] = "Tier 7: [Attentat] Benötigte Combo-Treffer",
                ["Tier7_Assassination_RequiredPoints"] = "Tier 7: [Attentat] Benötigte Punkte",

                // === Tier 8: Assassinenherz - Aktiv G-Taste (10) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] = "Tier 8: [Assassinenherz] Kritischer Schadensbonus",
                ["Tier8_AssassinHeart_Duration"] = "Tier 8: [Assassinenherz] Buff-Dauer (Sek)",
                ["Tier8_AssassinHeart_StaminaCost"] = "Tier 8: [Assassinenherz] Ausdauerkosten",
                ["Tier8_AssassinHeart_Cooldown"] = "Tier 8: [Assassinenherz] Abklingzeit (Sek)",
                ["Tier8_AssassinHeart_TeleportRange"] = "Tier 8: [Assassinenherz] Teleportreichweite (m)",
                ["Tier8_AssassinHeart_TeleportBackDistance"] = "Tier 8: [Assassinenherz] Distanz hinter Ziel (m)",
                ["Tier8_AssassinHeart_StunDuration"] = "Tier 8: [Assassinenherz] Betäubungsdauer (Sek)",
                ["Tier8_AssassinHeart_ComboAttackCount"] = "Tier 8: [Assassinenherz] Combo-Angriffsanzahl",
                ["Tier8_AssassinHeart_AttackInterval"] = "Tier 8: [Assassinenherz] Angriffsintervall (Sek)",
                ["Tier8_AssassinHeart_RequiredPoints"] = "Tier 8: [Assassinenherz] Benötigte Punkte",

                // ============================================
                // Schwert-Skilltree (neues Format) - 33 Schlüssel
                // ============================================

                // === Tier 0: Schwertsspezialist (2) ===
                ["Tier0_SwordExpert_DamageBonus"] = "Tier 0: [Schwertspezialist] Schwert-Schadensbonus (%)",
                ["Tier0_SwordExpert_RequiredPoints"] = "Tier 0: [Schwertspezialist] Benötigte Punkte",

                // === Tier 1-1: Schnellhieb (2) ===
                ["Tier1_FastSlash_RequiredPoints"] = "Tier 1-1: [Schnellhieb] Benötigte Punkte",
                ["Tier1_FastSlash_AttackSpeedBonus"] = "Tier 1-1: [Schnellhieb] Angriffsgeschwindigkeitsbonus (%)",

                // === Tier 1-2: Gegenschlaghaltung (3) ===
                ["Tier1_CounterStance_RequiredPoints"] = "Tier 1-2: [Gegenschlaghaltung] Benötigte Punkte",
                ["Tier1_CounterStance_Duration"] = "Tier 1-2: [Gegenschlaghaltung] Buff-Dauer (Sek)",
                ["Tier1_CounterStance_DefenseBonus"] = "Tier 1-2: [Gegenschlaghaltung] Verteidigungsbonus (%)",

                // === Tier 2: Combo-Hieb (3) ===
                ["Tier2_ComboSlash_RequiredPoints"] = "Tier 2: [Combo-Hieb] Benötigte Punkte",
                ["Tier2_ComboSlash_DamageBonus"] = "Tier 2: [Combo-Hieb] Schadensbonus (%)",
                ["Tier2_ComboSlash_BuffDuration"] = "Tier 2: [Combo-Hieb] Buff-Dauer (Sek)",

                // === Tier 3: Riposte (2) ===
                ["Tier3_Riposte_RequiredPoints"] = "Tier 3: [Riposte] Benötigte Punkte",
                ["Tier3_Riposte_DamageBonus"] = "Tier 3: [Riposte] Hiebschadensbonus",

                // === Tier 4-1: Alles auf eine Karte (3) ===
                ["Tier4_AllInOne_RequiredPoints"] = "Tier 4-1: [Alles auf eine Karte] Benötigte Punkte",
                ["Tier4_AllInOne_AttackBonus"] = "Tier 4-1: [Alles auf eine Karte] Angriffsbonus (%)",
                ["Tier4_AllInOne_DefenseBonus"] = "Tier 4-1: [Alles auf eine Karte] Verteidigungsbonus",

                // === Tier 4-2: Wahres Duell (2) ===
                ["Tier4_TrueDuel_RequiredPoints"] = "Tier 4-2: [Wahres Duell] Benötigte Punkte",
                ["Tier4_TrueDuel_AttackSpeedBonus"] = "Tier 4-2: [Wahres Duell] Angriffsgeschwindigkeitsbonus (%)",

                // === Tier 5: Parierstoß - Aktiv G-Taste (6) ===
                ["Tier5_ParryRush_RequiredPoints"] = "Tier 5: [Parierstoß] Benötigte Punkte",
                ["Tier5_ParryRush_BuffDuration"] = "Tier 5: [Parierstoß] Buff-Dauer (Sek)",
                ["Tier5_ParryRush_DamageBonus"] = "Tier 5: [Parierstoß] Schadensbonus (%)",
                ["Tier5_ParryRush_PushDistance"] = "Tier 5: [Parierstoß] Stoßdistanz (m)",
                ["Tier5_ParryRush_StaminaCost"] = "Tier 5: [Parierstoß] Ausdauerkosten",
                ["Tier5_ParryRush_Cooldown"] = "Tier 5: [Parierstoß] Abklingzeit (Sek)",

                // === Tier 6: Sturmhieb - Aktiv G-Taste (10) ===
                ["Tier6_RushSlash_RequiredPoints"] = "Tier 6: [Sturmhieb] Benötigte Punkte",
                ["Tier6_RushSlash_Hit1DamageRatio"] = "Tier 6: [Sturmhieb] Schadensanteil 1. Treffer (%)",
                ["Tier6_RushSlash_Hit2DamageRatio"] = "Tier 6: [Sturmhieb] Schadensanteil 2. Treffer (%)",
                ["Tier6_RushSlash_Hit3DamageRatio"] = "Tier 6: [Sturmhieb] Schadensanteil 3. Treffer (%)",
                ["Tier6_RushSlash_InitialDistance"] = "Tier 6: [Sturmhieb] Anfangssturmdistanz (m)",
                ["Tier6_RushSlash_SideDistance"] = "Tier 6: [Sturmhieb] Seitwärtsbewegungsdistanz (m)",
                ["Tier6_RushSlash_StaminaCost"] = "Tier 6: [Sturmhieb] Ausdauerkosten",
                ["Tier6_RushSlash_Cooldown"] = "Tier 6: [Sturmhieb] Abklingzeit (Sek)",
                ["Tier6_RushSlash_MoveSpeed"] = "Tier 6: [Sturmhieb] Bewegungsgeschwindigkeit (m/s)",
                ["Tier6_RushSlash_AttackSpeedBonus"] = "Tier 6: [Sturmhieb] Angriffsgeschwindigkeitsbonus (%)",

                // ============================================
                // Streitkolben-Skilltree - 34 Schlüssel
                // ============================================

                // === Tier 0: Streitkolbenspezialist (4) ===
                ["Tier0_MaceExpert_DamageBonus"] = "Tier 0: [Streitkolbenspezialist] Schadensbonus (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: [Streitkolbenspezialist] Betäubungschance (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: [Streitkolbenspezialist] Betäubungsdauer (Sek)",
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: [Streitkolbenspezialist] Benötigte Punkte",

                // === Tier 1: Streitkolben-Schadensschub (2) ===
                ["Tier1_MaceExpert_DamageBonus"] = "Tier 1: [Streitkolben-Schadensschub] Schadensbonus (%)",
                ["Tier1_MaceExpert_RequiredPoints"] = "Tier 1: [Streitkolben-Schadensschub] Benötigte Punkte",

                // === Tier 2: Betäubungsschub (3) ===
                ["Tier2_StunBoost_StunChanceBonus"] = "Tier 2: [Betäubungsschub] Betäubungschance-Bonus (%)",
                ["Tier2_StunBoost_StunDurationBonus"] = "Tier 2: [Betäubungsschub] Betäubungsdauer-Bonus (Sek)",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: [Betäubungsschub] Benötigte Punkte",

                // === Tier 3-1: Drehschlag (3) ===
                ["Tier3_SpinStrike_DamageBonus"] = "Tier 3-1: [Drehschlag] Schadensbonus (%)",
                ["Tier3_SpinStrike_Range"] = "Tier 3-1: [Drehschlag] AOE-Reichweite (m)",
                ["Tier3_Guard_RequiredPoints"] = "Tier 3-1: [Drehschlag] Benötigte Punkte",

                // === Tier 3-2: Schwerer Hieb (2) ===
                ["Tier3_HeavyStrike_DamageBonus"] = "Tier 3-2: [Schwerer Hieb] Stumpfer Schadensbonus",
                ["Tier3_HeavyStrike_RequiredPoints"] = "Tier 3-2: [Schwerer Hieb] Benötigte Punkte",

                // === Tier 4: Stoß (2) ===
                ["Tier4_Push_KnockbackChance"] = "Tier 4: [Stoß] Rückstoßchance (%)",
                ["Tier4_Push_RequiredPoints"] = "Tier 4: [Stoß] Benötigte Punkte",

                // === Tier 5-1: Panzer (3) ===
                ["Tier5_Tank_HealthBonus"] = "Tier 5-1: [Panzer] LP-Bonus (%)",
                ["Tier5_Tank_DamageReduction"] = "Tier 5-1: [Panzer] Schadensreduzierung (%)",
                ["Tier5_Tank_RequiredPoints"] = "Tier 5-1: [Panzer] Benötigte Punkte",

                // === Tier 5-2: DPS-Schub (2) ===
                ["Tier5_DPS_DamageBonus"] = "Tier 5-2: [DPS-Schub] Schadensbonus (%)",
                ["Tier5_DPS_RequiredPoints"] = "Tier 5-2: [DPS-Schub] Benötigte Punkte",

                // === Tier 6: Flinker Angriff (2) ===
                ["Tier6_Sokgong_AttackSpeedBonus"] = "Tier 6: [Flinker Angriff] Angriffsgeschwindigkeitsbonus (%)",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: [Flinker Angriff] Benötigte Punkte",

                // === Tier 7-1: Wuthammer - Aktiv H-Taste (6) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] = "Tier 7-1: [Wuthammer] Schadensbonus Treffer 1-4 (%)",
                ["Tier7_FuryHammer_FinalHitMultiplier"] = "Tier 7-1: [Wuthammer] Schadensbonus Finaler 5. Treffer (%)",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7-1: [Wuthammer] Ausdauerkosten",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7-1: [Wuthammer] Abklingzeit (Sek)",
                ["Tier7_FuryHammer_AoeRadius"] = "Tier 7-1: [Wuthammer] AOE-Radius (m)",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7-1: [Wuthammer] Benötigte Punkte",

                // === Tier 7-2: Wächterherz - Aktiv G-Taste (5) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7-2: [Wächterherz] Abklingzeit (Sek)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7-2: [Wächterherz] Ausdauerkosten",
                ["Tier7_GuardianHeart_Duration"] = "Tier 7-2: [Wächterherz] Buff-Dauer (Sek)",
                ["Tier7_GuardianHeart_ReflectPercent"] = "Tier 7-2: [Wächterherz] Schadensreflexion (%)",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7-2: [Wächterherz] Benötigte Punkte",

                // ============================================
                // Stangenwaffen-Skilltree - 37 Schlüssel
                // ============================================

                // === Tier 0: Stangenwaffenspezialist (2) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] = "Tier 0: [Stangenwaffenspezialist] Angriffsreichweite-Bonus (%)",
                ["Tier0_PolearmExpert_RequiredPoints"] = "Tier 0: [Stangenwaffenspezialist] Benötigte Punkte",

                // === Tier 1: Drehschneide (2) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] = "Tier 1: [Drehschneide] Drehangriffsschadensbonus (%)",
                ["Tier1_SpinWheel_RequiredPoints"] = "Tier 1: [Drehschneide] Benötigte Punkte",

                // === Tier 2-1: Stangenwaffen-Verstärkung (2) ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] = "Tier 2-1: [Stangenwaffen-Verstärkung] Durchdringungs-Schadensbonus (fest)",
                ["Tier2-1_PolearmBoost_RequiredPoints"] = "Tier 2-1: [Stangenwaffen-Verstärkung] Benötigte Punkte",

                // === Tier 2-2: Heldenhieb (2) ===
                ["Tier2-2_HeroStrike_KnockbackChance"] = "Tier 2-2: [Heldenhieb] Stagger-Chance (%)",
                ["Tier2-2_HeroStrike_RequiredPoints"] = "Tier 2-2: [Heldenhieb] Benötigte Punkte",

                // === Tier 3: Breithieb (3) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] = "Tier 3: [Breithieb] Doppelhieb-Schadensbonus (%)",
                ["Tier3_AreaCombo_DoubleHitDuration"] = "Tier 3: [Breithieb] Doppelhieb-Buff-Dauer (Sek)",
                ["Tier3_AreaCombo_RequiredPoints"] = "Tier 3: [Breithieb] Benötigte Punkte",

                // === Tier 4-1: Bodenhieb (2) ===
                ["Tier4-1_GroundWheel_RequiredPoints"] = "Tier 4-1: [Bodenhieb] Benötigte Punkte",

                // === Tier 4-2: Mondschnitt (3) ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] = "Tier 4-2: [Mondschnitt] Angriffsreichweite-Bonus (%)",
                ["Tier4-2_MoonSlash_StaminaReduction"] = "Tier 4-2: [Mondschnitt] Ausdauer-Reduzierung (%)",
                ["Tier4-2_MoonSlash_RequiredPoints"] = "Tier 4-2: [Mondschnitt] Benötigte Punkte",

                // === Tier 4-3: Unterdrückungsangriff (2) ===
                ["Tier4-3_Suppress_DamageBonus"] = "Tier 4-3: [Unterdrückungsangriff] Schadensbonus (%)",
                ["Tier4-3_Suppress_RequiredPoints"] = "Tier 4-3: [Unterdrückungsangriff] Benötigte Punkte",

                // === Tier 5: Durchbohrende Ladung (9) ===
                ["Tier5_PierceCharge_DashDistance"] = "Tier 5: [Durchbohrende Ladung] Sturmdistanz (m)",
                ["Tier5_PierceCharge_FirstHitDamageBonus"] = "Tier 5: [Durchbohrende Ladung] Schadensbonus 1. Treffer (%)",
                ["Tier5_PierceCharge_AoeDamageBonus"] = "Tier 5: [Durchbohrende Ladung] AOE-Rückstoßschadensbonus (%)",
                ["Tier5_PierceCharge_AoeAngle"] = "Tier 5: [Durchbohrende Ladung] AOE-Winkel (Grad)",
                ["Tier5_PierceCharge_AoeRadius"] = "Tier 5: [Durchbohrende Ladung] AOE-Radius (m)",
                ["Tier5_PierceCharge_KnockbackDistance"] = "Tier 5: [Durchbohrende Ladung] Rückstoßdistanz (m)",
                ["Tier5_PierceCharge_StaminaCost"] = "Tier 5: [Durchbohrende Ladung] Ausdauerkosten",
                ["Tier5_PierceCharge_Cooldown"] = "Tier 5: [Durchbohrende Ladung] Abklingzeit (Sek)",
                ["Tier5_PierceCharge_RequiredPoints"] = "Tier 5: [Durchbohrende Ladung] Benötigte Punkte",

                // ============================================
                // Klassenfähigkeiten - Bogenschütze
                // ============================================
                ["Archer_MultiShot_ArrowCount"] = "Mehrfachschuss: Pfeilanzahl",
                ["Archer_MultiShot_ArrowConsumption"] = "Mehrfachschuss: Pfeilverbrauch",
                ["Archer_MultiShot_DamagePercent"] = "Mehrfachschuss: Schaden pro Pfeil (%)",
                ["Archer_MultiShot_Cooldown"] = "Mehrfachschuss: Abklingzeit (Sek)",
                ["Archer_MultiShot_Charges"] = "Mehrfachschuss: Ladungen",
                ["Archer_MultiShot_StaminaCost"] = "Mehrfachschuss: Ausdauerkosten",
                ["Archer_JumpHeightBonus"] = "Passiv: Sprunghöhe-Bonus (%)",
                ["Archer_FallDamageReduction"] = "Passiv: Fallschadensreduzierung (%)",
                ["Archer_Lv2_BonusArrows"]   = "Lv2: Zusätzliche Pfeile",
                ["Archer_Lv2_DamagePercent"] = "Lv2: Schaden pro Pfeil (%)",
                ["Archer_Lv3_BonusArrows"]   = "Lv3: Zusätzliche Pfeile",
                ["Archer_Lv3_DamagePercent"] = "Lv3: Schaden pro Pfeil (%)",
                ["Archer_Lv4_BonusArrows"]   = "Lv4: Zusätzliche Pfeile",
                ["Archer_Lv4_DamagePercent"] = "Lv4: Schaden pro Pfeil (%)",
                ["Archer_Lv5_BonusArrows"]   = "Lv5: Zusätzliche Pfeile",
                ["Archer_Lv5_DamagePercent"] = "Lv5: Schaden pro Pfeil (%)",
                ["Archer_Lv5_BonusCharges"]  = "Lv5: Zusätzliche Ladungen",
                ["Archer_Lv2_JumpHeightBonus"]     = "Lv2 Passiv: Sprunghöhe-Bonus (%)",
                ["Archer_Lv3_JumpHeightBonus"]     = "Lv3 Passiv: Sprunghöhe-Bonus (%)",
                ["Archer_Lv4_JumpHeightBonus"]     = "Lv4 Passiv: Sprunghöhe-Bonus (%)",
                ["Archer_Lv5_JumpHeightBonus"]     = "Lv5 Passiv: Sprunghöhe-Bonus (%)",
                ["Archer_Lv3_FallDamageReduction"] = "Lv3 Passiv: Fallschadensreduzierung (%)",
                ["Archer_Lv4_FallDamageReduction"] = "Lv4 Passiv: Fallschadensreduzierung (%)",
                ["Archer_Lv5_FallDamageReduction"] = "Lv5 Passiv: Fallschadensreduzierung (%)",
                ["Archer_ElementalResistPerLevel"] = "Passiv: Elementarwiderstand pro Stufe (%)",

                // ============================================
                // Klassenfähigkeiten - Magier
                // ============================================
                ["Mage_AOE_Range"] = "Aktiv: Reichweite (m)",
                ["Mage_AOE_Max_Targets"] = "Aktiv: Max. Zielanzahl",
                ["Mage_Eitr_Cost"] = "Aktiv: Eitr-Kosten",
                ["Mage_Damage_Multiplier"] = "Aktiv: Schadensbonus (%)",
                ["Mage_Cooldown"] = "Aktiv: Abklingzeit (Sek)",
                ["Mage_Elemental_Resistance"] = "Passiv: Elementarwiderstand (%)",

                // ============================================
                // Klassenfähigkeiten - Verteidiger
                // ============================================
                ["Tanker_Taunt_Cooldown"] = "Kriegsschrei: Abklingzeit (Sek)",
                ["Tanker_Taunt_StaminaCost"] = "Kriegsschrei: Ausdauerkosten",
                ["Tanker_Taunt_Range"] = "Kriegsschrei: Spottreichweite (m)",
                ["Tanker_Taunt_Duration"] = "Kriegsschrei: Spottdauer (Sek)",
                ["Tanker_Taunt_BossDuration"] = "Kriegsschrei: Spottdauer gegen Bosse (Sek)",
                ["Tanker_Taunt_DamageReduction"] = "Kriegsschrei: Schadensreduzierung (%)",
                ["Tanker_Taunt_BuffDuration"] = "Kriegsschrei: Buff-Dauer (Sek)",
                ["Tanker_Taunt_EffectHeight"] = "Kriegsschrei: Effekthöhe (m)",
                ["Tanker_Taunt_EffectScale"] = "Kriegsschrei: Effektskalierung",
                ["Tanker_Passive_DamageReduction"] = "Passiv: Schadensreduzierung (%)",

                // ============================================
                // Klassenfähigkeiten - Schurke
                // ============================================
                ["Rogue_ShadowStrike_Cooldown"] = "Schattenschlag: Abklingzeit (Sek)",
                ["Rogue_ShadowStrike_StaminaCost"] = "Schattenschlag: Ausdauerkosten",
                ["Rogue_ShadowStrike_AttackBonus"] = "Schattenschlag: Angriffsbonus (%)",
                ["Rogue_ShadowStrike_BuffDuration"] = "Schattenschlag: Buff-Dauer (Sek)",
                ["Rogue_ShadowStrike_SmokeScale"] = "Schattenschlag: Rauchskalierung",
                ["Rogue_ShadowStrike_AggroRange"] = "Schattenschlag: Aggroreichtweite (m)",
                ["Rogue_ShadowStrike_StealthDuration"] = "Schattenschlag: Tarndauer (Sek)",
                ["Rogue_AttackSpeed_Bonus"] = "Passiv: Angriffsgeschwindigkeitsbonus (%)",
                ["Rogue_Stamina_Reduction"] = "Passiv: Ausdauerverbrauch-Reduzierung (%)",
                ["Rogue_ElementalResistance_Debuff"] = "Passiv: Elementarwiderstand-Bonus (%)",

                // ============================================
                // Klassenfähigkeiten - Paladin
                // ============================================
                ["Paladin_Active_Cooldown"] = "Heilige Heilung: Abklingzeit (Sek)",
                ["Paladin_Active_Range"] = "Heilige Heilung: Reichweite (m)",
                ["Paladin_Active_EitrCost"] = "Heilige Heilung: Eitr-Kosten",
                ["Paladin_Active_StaminaCost"] = "Heilige Heilung: Ausdauerkosten",
                ["Paladin_Active_SelfHealPercent"] = "Heilige Heilung: Selbstheilungsrate (%)",
                ["Paladin_Active_AllyHealPercentOverTime"] = "Heilige Heilung: Verbündeten-Heilungsrate über Zeit (%)",
                ["Paladin_Active_Duration"] = "Heilige Heilung: Dauer (Sek)",
                ["Paladin_Active_Interval"] = "Heilige Heilung: Intervall (Sek)",
                ["Paladin_Passive_ElementalResistanceReduction"] = "Passiv: Widerstandsbonus (%)",

                // ============================================
                // Klassenfähigkeiten - Berserker
                // ============================================
                ["Beserker_Active_Cooldown"] = "Berserkerwut: Abklingzeit (Sek)",
                ["Beserker_Active_StaminaCost"] = "Berserkerwut: Ausdauerkosten",
                ["Beserker_Active_Duration"] = "Berserkerwut: Dauer (Sek)",
                ["Beserker_Active_DamagePerHealthPercent"] = "Berserkerwut: Schaden pro LP-% (%)",
                ["Beserker_Active_MaxDamageBonus"] = "Berserkerwut: Max. Schadensbonus (%)",
                ["Beserker_Active_HealthThreshold"] = "Berserkerwut: LP-Schwellenwert (%)",
                ["Beserker_Passive_HealthThreshold"] = "Todesherausforderung: LP-Schwellenwert (%)",
                ["Beserker_Passive_InvincibilityDuration"] = "Todesherausforderung: Unverwundbarkeits-Dauer (Sek)",
                ["Beserker_Passive_Cooldown"] = "Todesherausforderung: Abklingzeit (Sek)",
                ["Berserker_Passive_HealthBonus"] = "Passiv: Max. LP-Bonus (%)",

                // ============================================
                // Schwert-Skilltree - Pfadangriff-Erweiterung
                // ============================================
                ["Tier6_RushSlash_PathWidth"] = "Tier 6: [Sturmhieb] Pfadangriffsbreite (m)",
            };
        }
    }
}

using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetPortugueseBrazilianKeyNames()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetPortugueseBrazilianKeyNames_Part1()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetPortugueseBrazilianKeyNames_Part2()) dict[kv.Key] = kv.Value;
            return dict;
        }

        private static Dictionary<string, string> GetPortugueseBrazilianKeyNames_Part1()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Skill_Tree_Base - Teclas de Atalho
                // ============================================
                ["HotKey_Y"] = "Tecla de Habilidade de Classe",
                ["HotKey_R"] = "Tecla de Habilidade à Distância",
                ["HotKey_G"] = "Tecla de Habilidade Corpo a Corpo Principal",
                ["HotKey_H"] = "Tecla de Habilidade Secundária",
                ["HUD_PosX"] = "Posição X do HUD",
                ["HUD_PosY"] = "Posição Y do HUD",
                ["PassiveMessageDisplay"] = "Exibição de Mensagem Passiva",

                // ============================================
                // Árvore de Ataque - 33 Chaves
                // ============================================

                // === Tier 0: Mestre de Ataque (2) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: [Mestre de Ataque] Bônus de Dano Total (%)",
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: [Mestre de Ataque] Pontos Necessários",

                // === Tier 1: Ataque Base (3) ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] = "Tier 1: [Ataque Base] Bônus de Dano Físico (%)",
                ["Tier1_BaseAttack_ElementalDamageBonus"] = "Tier 1: [Ataque Base] Bônus de Dano Elemental (%)",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: [Ataque Base] Pontos Necessários",

                // === Tier 2: Especialização em Armas (12) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2-1: [Espec. C.a.C.] Chance de Ativação (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2-1: [Espec. C.a.C.] Bônus de Dano",
                ["Tier2_MeleeSpec_RequiredPoints"] = "Tier 2-1: [Espec. C.a.C.] Pontos Necessários",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2-2: [Espec. Arco] Chance de Ativação (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2-2: [Espec. Arco] Bônus de Dano",
                ["Tier2_BowSpec_RequiredPoints"] = "Tier 2-2: [Espec. Arco] Pontos Necessários",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2-3: [Espec. Besta] Chance de Ativação (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2-3: [Espec. Besta] Bônus de Dano",
                ["Tier2_CrossbowSpec_RequiredPoints"] = "Tier 2-3: [Espec. Besta] Pontos Necessários",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2-4: [Espec. Cajado] Chance de Ativação (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2-4: [Espec. Cajado] Bônus de Dano",
                ["Tier2_StaffSpec_RequiredPoints"] = "Tier 2-4: [Espec. Cajado] Pontos Necessários",

                // === Tier 3: Impulso de Ataque (3) ===
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: [Impulso de Ataque] Bônus de Dano Físico (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: [Impulso de Ataque] Bônus de Dano Elemental (%)",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: [Impulso de Ataque] Pontos Necessários",

                // === Tier 4: Aprimoramento de Combate (6) ===
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4-1: [Aprim. C.a.C.] Bônus de Combo 2 Golpes (%)",
                ["Tier4_MeleeEnhance_RequiredPoints"] = "Tier 4-1: [Aprim. C.a.C.] Pontos Necessários",
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4-2: [Ataque de Precisão] Chance Crítica (%)",
                ["Tier4_PrecisionAttack_RequiredPoints"] = "Tier 4-2: [Ataque de Precisão] Pontos Necessários",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4-3: [Aprim. à Distância] Bônus de Dano (%)",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4-3: [Aprim. à Distância] Pontos Necessários",

                // === Tier 5: Carga (3) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: [Carga] Recuperação de Stamina (%)",
                ["Tier5_Charge_TriggerChance"] = "Tier 5: [Carga] Chance de Ativação (%)",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: [Carga] Pontos Necessários",

                // === Tier 6: Aprimoramento Final (8) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6-1: [Ponto Fraco] Bônus de Dano Crítico (%)",
                ["Tier6_WeakPointAttack_RequiredPoints"] = "Tier 6-1: [Ponto Fraco] Pontos Necessários",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6-2: [Finalizador de Combo] Bônus Combo 3 Golpes (%)",
                ["Tier6_ComboFinisher_RequiredPoints"] = "Tier 6-2: [Finalizador de Combo] Pontos Necessários",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6-3: [Golpe Duas Mãos] Bônus de Dano (%)",
                ["Tier6_TwoHandCrush_RequiredPoints"] = "Tier 6-3: [Golpe Duas Mãos] Pontos Necessários",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6-4: [Ataque Elemental] Bônus Elemental (%)",
                ["Tier6_ElementalAttack_RequiredPoints"] = "Tier 6-4: [Ataque Elemental] Pontos Necessários",

                // ============================================
                // Árvore de Velocidade - 49 Chaves
                // ============================================

                // === Tier 0: Mestre de Velocidade (2) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: [Mestre de Velocidade] Bônus de Vel. de Movimento (%)",
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: [Mestre de Velocidade] Pontos Necessários",

                // === Tier 1: Base de Agilidade (5) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: [Base de Agilidade] Bônus de Vel. Pós-Esquiva (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: [Base de Agilidade] Duração do Buff (seg)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: [Base de Agilidade] Bônus de Vel. de Ataque (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: [Base de Agilidade] Bônus de Vel. de Esquiva (%)",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: [Base de Agilidade] Pontos Necessários",

                // === Tier 2-1: Fluxo Contínuo (5) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2-1: [Fluxo Contínuo] Bônus de Vel. de Ataque no 2º Golpe (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2-1: [Fluxo Contínuo] Redução de Stamina (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2-1: [Fluxo Contínuo] Duração do Buff (seg)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2-1: [Fluxo Contínuo] Bônus de Vel. de Combo (%)",
                ["Tier2_MeleeFlow_RequiredPoints"] = "Tier 2-1: [Fluxo Contínuo] Pontos Necessários",

                // === Tier 2-2: Especialista em Besta (4) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2-2: [Espec. Besta] Bônus de Vel. de Movimento ao Acertar (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2-2: [Espec. Besta] Duração do Buff (seg)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2-2: [Espec. Besta] Bônus de Vel. de Recarga durante Buff (%)",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "Tier 2-2: [Espec. Besta] Pontos Necessários",

                // === Tier 2-3: Especialista em Arco (4) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2-3: [Espec. Arco] Redução de Stamina no Combo 2 Golpes (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2-3: [Espec. Arco] Bônus de Vel. de Próxima Seta (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2-3: [Espec. Arco] Duração do Buff (seg)",
                ["Tier2_BowExpert_RequiredPoints"] = "Tier 2-3: [Espec. Arco] Pontos Necessários",

                // === Tier 2-4: Conjuração Móvel (4) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2-4: [Conjuração Móvel] Bônus de Vel. ao Conjurar (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2-4: [Conjuração Móvel] Redução de Custo de Eitr (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2-4: [Conjuração Móvel] Vel. de Movimento ao Conjurar com Cajado (%)",
                ["Tier2_MobileCast_RequiredPoints"] = "Tier 2-4: [Conjuração Móvel] Pontos Necessários",

                // === Tier 3-1: Praticante 1 (3) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3-1: [Praticante 1] Bônus de Proficiência C.a.C.",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3-1: [Praticante 1] Bônus de Proficiência Besta",
                ["Tier3_Practitioner1_RequiredPoints"] = "Tier 3-1: [Praticante 1] Pontos Necessários",

                // === Tier 3-2: Praticante 2 (3) ===
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3-2: [Praticante 2] Bônus de Proficiência Cajado",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3-2: [Praticante 2] Bônus de Proficiência Arco",
                ["Tier3_Practitioner2_RequiredPoints"] = "Tier 3-2: [Praticante 2] Pontos Necessários",

                // === Tier 4-1: Energizador (2) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4-1: [Energizador] Redução de Consumo de Comida (%)",
                ["Tier4_Energizer_RequiredPoints"] = "Tier 4-1: [Energizador] Pontos Necessários",

                // === Tier 4-2: Capitão (2) ===
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4-2: [Capitão] Bônus de Vel. de Navio (%)",
                ["Tier4_Captain_RequiredPoints"] = "Tier 4-2: [Capitão] Pontos Necessários",

                // === Tier 5: Mestre do Salto (3) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: [Mestre do Salto] Bônus de Proficiência de Salto",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: [Mestre do Salto] Redução de Stamina no Salto (%)",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: [Mestre do Salto] Pontos Necessários",

                // === Tier 6-1: Destreza (3) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6-1: [Destreza] Bônus de Vel. de Ataque C.a.C. (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6-1: [Destreza] Bônus de Vel. de Movimento (%)",
                ["Tier6_Dexterity_RequiredPoints"] = "Tier 6-1: [Destreza] Pontos Necessários",

                // === Tier 6-2: Resistência (2) ===
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6-2: [Resistência] Bônus de Stamina Máxima",
                ["Tier6_Endurance_RequiredPoints"] = "Tier 6-2: [Resistência] Pontos Necessários",

                // === Tier 6-3: Inteligência (2) ===
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6-3: [Inteligência] Bônus de Eitr Máximo",
                ["Tier6_Intellect_RequiredPoints"] = "Tier 6-3: [Inteligência] Pontos Necessários",

                // === Tier 7: Mestre (3) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: [Mestre] Bônus de Proficiência de Corrida",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: [Mestre] Bônus de Proficiência de Salto",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: [Mestre] Pontos Necessários",

                // === Tier 8-1: Aceleração C.a.C. (3) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8-1: [Acel. C.a.C.] Bônus de Vel. de Ataque C.a.C. (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8-1: [Acel. C.a.C.] Bônus de Próximo Ataque no Combo 3 Golpes (%)",
                ["Tier8_MeleeAccel_RequiredPoints"] = "Tier 8-1: [Acel. C.a.C.] Pontos Necessários",

                // === Tier 8-2: Aceleração de Besta (3) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8-2: [Acel. Besta] Bônus de Vel. de Recarga (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8-2: [Acel. Besta] Vel. de Movimento durante Recarga (%)",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "Tier 8-2: [Acel. Besta] Pontos Necessários",

                // === Tier 8-3: Aceleração de Arco (3) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8-3: [Acel. Arco] Bônus de Vel. de Enfiamento (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8-3: [Acel. Arco] Vel. de Movimento ao Enfileirar (%)",
                ["Tier8_BowAccel_RequiredPoints"] = "Tier 8-3: [Acel. Arco] Pontos Necessários",

                // === Tier 8-4: Aceleração de Conjuração (3) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8-4: [Acel. Conjuração] Bônus de Vel. de Ataque Mágico (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8-4: [Acel. Conjuração] Taxa de Rec. de Eitr Máx. no Combo 3 Golpes (%)",
                ["Tier8_CastAccel_RequiredPoints"] = "Tier 8-4: [Acel. Conjuração] Pontos Necessários",

                // ============================================
                // Árvore de Defesa - 59 Chaves
                // ============================================

                // === Tier 0: Mestre de Defesa (3) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: [Mestre de Defesa] Bônus de HP",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: [Mestre de Defesa] Bônus de Armadura",
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: [Mestre de Defesa] Pontos Necessários",

                // === Tier 1: Endurecimento da Pele (3) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: [Endurecimento da Pele] Bônus de HP",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: [Endurecimento da Pele] Bônus de Armadura",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: [Endurecimento da Pele] Pontos Necessários",

                // === Tier 2-1: Treinamento Mente-Corpo (3) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2-1: [Trein. Mente-Corpo] Bônus de Stamina Máxima",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2-1: [Trein. Mente-Corpo] Bônus de Eitr Máximo",
                ["Tier2_MindTraining_RequiredPoints"] = "Tier 2-1: [Trein. Mente-Corpo] Pontos Necessários",

                // === Tier 2-2: Treinamento de Saúde (3) ===
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2-2: [Treinamento de Saúde] Bônus de HP",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2-2: [Treinamento de Saúde] Bônus de Armadura",
                ["Tier2_HealthTraining_RequiredPoints"] = "Tier 2-2: [Treinamento de Saúde] Pontos Necessários",

                // === Tier 3-1: Respiração Central (2) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3-1: [Respiração Central] Bônus de Eitr",
                ["Tier3_CoreBreathing_RequiredPoints"] = "Tier 3-1: [Respiração Central] Pontos Necessários",

                // === Tier 3-2: Treinamento de Evasão (3) ===
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3-2: [Trein. de Evasão] Bônus de Esquiva (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3-2: [Trein. de Evasão] Aumento de Invencibilidade na Rolagem (%)",
                ["Tier3_EvasionTraining_RequiredPoints"] = "Tier 3-2: [Trein. de Evasão] Pontos Necessários",

                // === Tier 3-3: Impulso de Saúde (2) ===
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3-3: [Impulso de Saúde] Bônus de HP",
                ["Tier3_HealthBoost_RequiredPoints"] = "Tier 3-3: [Impulso de Saúde] Pontos Necessários",

                // === Tier 3-4: Treinamento de Escudo (2) ===
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3-4: [Trein. de Escudo] Bônus de Poder de Bloqueio",
                ["Tier3_ShieldTraining_RequiredPoints"] = "Tier 3-4: [Trein. de Escudo] Pontos Necessários",

                // === Tier 4-1: Onda de Choque (4) ===
                ["Tier4_Shockwave_Radius"] = "Tier 4-1: [Onda de Choque] Raio",
                ["Tier4_Shockwave_StunDuration"] = "Tier 4-1: [Onda de Choque] Duração do Atordoamento",
                ["Tier4_Shockwave_Cooldown"] = "Tier 4-1: [Onda de Choque] Recarga",
                ["Tier4_Shockwave_RequiredPoints"] = "Tier 4-1: [Onda de Choque] Pontos Necessários",

                // === Tier 4-2: Pisão no Chão (6) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4-2: [Pisão no Chão] Raio do Efeito (m)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4-2: [Pisão no Chão] Força de Knockback",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4-2: [Pisão no Chão] Recarga (seg)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4-2: [Pisão no Chão] Limite de HP para Ativação Auto",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4-2: [Pisão no Chão] Duração do VFX (seg)",
                ["Tier4_GroundStomp_RequiredPoints"] = "Tier 4-2: [Pisão no Chão] Pontos Necessários",

                // === Tier 4-3: Pele de Pedra (2) ===
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4-3: [Pele de Pedra] Amplificação de Armadura (%)",
                ["Tier4_RockSkin_RequiredPoints"] = "Tier 4-3: [Pele de Pedra] Pontos Necessários",

                // === Tier 5-1: Resistência (3) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5-1: [Resistência] Redução de Stamina ao Correr (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5-1: [Resistência] Redução de Stamina ao Saltar (%)",
                ["Tier5_Endurance_RequiredPoints"] = "Tier 5-1: [Resistência] Pontos Necessários",

                // === Tier 5-2: Agilidade (3) ===
                ["Tier5_Agility_DodgeBonus"] = "Tier 5-2: [Agilidade] Bônus de Esquiva (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5-2: [Agilidade] Redução de Stamina na Rolagem (%)",
                ["Tier5_Agility_RequiredPoints"] = "Tier 5-2: [Agilidade] Pontos Necessários",

                // === Tier 5-3: Regeneração Troll (3) ===
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5-3: [Regen. Troll] Bônus de Regen. de HP (por seg)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5-3: [Regen. Troll] Intervalo de Regeneração (seg)",
                ["Tier5_TrollRegen_RequiredPoints"] = "Tier 5-3: [Regen. Troll] Pontos Necessários",

                // === Tier 5-4: Mestre de Bloqueio (3) ===
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5-4: [Mestre de Bloqueio] Bônus de Poder de Bloqueio",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5-4: [Mestre de Bloqueio] Bônus de Duração de Aparar (seg)",
                ["Tier5_BlockMaster_RequiredPoints"] = "Tier 5-4: [Mestre de Bloqueio] Pontos Necessários",

                // === Tier 6-1: Escudo Mental (1) ===
                ["Tier6_MindShield_RequiredPoints"] = "Tier 6-1: [Escudo Mental] Pontos Necessários",

                // === Tier 6-2: Aprimoramento Nervoso (2) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6-2: [Aprim. Nervoso] Bônus de Esquiva Condicional (30s, %)",
                ["Tier6_NerveEnhancement_RequiredPoints"] = "Tier 6-2: [Aprim. Nervoso] Pontos Necessários",

                // === Tier 6-3: Salto Duplo (1) ===
                ["Tier6_DoubleJump_RequiredPoints"] = "Tier 6-3: [Salto Duplo] Pontos Necessários",

                // === Tier 6-4: Vitalidade de Jotunn (3) ===
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6-4: [Vitalidade de Jotunn] Bônus de HP (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6-4: [Vitalidade de Jotunn] Resistência Física/Elemental (%)",
                ["Tier6_JotunnVitality_RequiredPoints"] = "Tier 6-4: [Vitalidade de Jotunn] Pontos Necessários",

                // === Tier 6-5: Escudo de Jotunn (4) ===
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6-5: [Escudo de Jotunn] Redução de Stamina no Bloqueio (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6-5: [Escudo de Jotunn] Bônus de Vel. com Escudo Normal (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6-5: [Escudo de Jotunn] Bônus de Vel. com Escudo Torre (%)",
                ["Tier6_JotunnShield_RequiredPoints"] = "Tier 6-5: [Escudo de Jotunn] Pontos Necessários",

                // ============================================
                // Árvore de Produção - 22 Chaves
                // ============================================

                // === Tier 0: Mestre de Produção (1) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] = "Tier 0: Chance de +1 Madeira (%)",

                // === Tier 1: Trabalhador Novato (1) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] = "Tier 1: Chance de +1 Madeira (%)",

                // === Tier 2: Especialização (5) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] = "Tier 2: Corte Nv2 - Chance de +1 Madeira (%)",
                ["Tier2_GatheringLv2_BonusChance"] = "Tier 2: Coleta Nv2 - Chance de +1 Item (%)",
                ["Tier2_MiningLv2_BonusChance"] = "Tier 2: Mineração Nv2 - Chance de +1 Minério (%)",
                ["Tier2_CraftingLv2_UpgradeChance"] = "Tier 2: Artesanato Nv2 - Chance de +1 Melhoria (%)",
                ["Tier2_CraftingLv2_DurabilityBonus"] = "Tier 2: Artesanato Nv2 - Aumento de Durabilidade Máx. (%)",

                // === Tier 3: Habilidades Intermediárias (5) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] = "Tier 3: Corte Nv3 - Chance de +2 Madeira (%)",
                ["Tier3_GatheringLv3_BonusChance"] = "Tier 3: Coleta Nv3 - Chance de +1 Item (%)",
                ["Tier3_MiningLv3_BonusChance"] = "Tier 3: Mineração Nv3 - Chance de +1 Minério (%)",
                ["Tier3_CraftingLv3_UpgradeChance"] = "Tier 3: Artesanato Nv3 - Chance de +1 Melhoria (%)",
                ["Tier3_CraftingLv3_DurabilityBonus"] = "Tier 3: Artesanato Nv3 - Aumento de Durabilidade Máx. (%)",

                // === Tier 4: Habilidades Avançadas (5) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] = "Tier 4: Corte Nv4 - Chance de +2 Madeira (%)",
                ["Tier4_GatheringLv4_BonusChance"] = "Tier 4: Coleta Nv4 - Chance de +1 Item (%)",
                ["Tier4_MiningLv4_BonusChance"] = "Tier 4: Mineração Nv4 - Chance de +1 Minério (%)",
                ["Tier4_CraftingLv4_UpgradeChance"] = "Tier 4: Artesanato Nv4 - Chance de +1 Melhoria (%)",
                ["Tier4_CraftingLv4_DurabilityBonus"] = "Tier 4: Artesanato Nv4 - Aumento de Durabilidade Máx. (%)",

                // === Árvore de Produção: Pontos Necessários (14) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: [Mestre de Produção] Pontos Necessários",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: [Trabalhador Novato] Pontos Necessários",
                ["Tier2_WoodcuttingLv2_RequiredPoints"] = "Tier 2: [Corte Nv2] Pontos Necessários",
                ["Tier2_GatheringLv2_RequiredPoints"] = "Tier 2: [Coleta Nv2] Pontos Necessários",
                ["Tier2_MiningLv2_RequiredPoints"] = "Tier 2: [Mineração Nv2] Pontos Necessários",
                ["Tier2_CraftingLv2_RequiredPoints"] = "Tier 2: [Artesanato Nv2] Pontos Necessários",
                ["Tier3_WoodcuttingLv3_RequiredPoints"] = "Tier 3: [Corte Nv3] Pontos Necessários",
                ["Tier3_GatheringLv3_RequiredPoints"] = "Tier 3: [Coleta Nv3] Pontos Necessários",
                ["Tier3_MiningLv3_RequiredPoints"] = "Tier 3: [Mineração Nv3] Pontos Necessários",
                ["Tier3_CraftingLv3_RequiredPoints"] = "Tier 3: [Artesanato Nv3] Pontos Necessários",
                ["Tier4_WoodcuttingLv4_RequiredPoints"] = "Tier 4: [Corte Nv4] Pontos Necessários",
                ["Tier4_GatheringLv4_RequiredPoints"] = "Tier 4: [Coleta Nv4] Pontos Necessários",
                ["Tier4_MiningLv4_RequiredPoints"] = "Tier 4: [Mineração Nv4] Pontos Necessários",
                ["Tier4_CraftingLv4_RequiredPoints"] = "Tier 4: [Artesanato Nv4] Pontos Necessários",

                // ============================================
                // Árvore de Arco - 34 Chaves
                // ============================================

                // === Tier 0: Especialista em Arco (2) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: [Espec. em Arco] Bônus de Dano com Arco (%)",
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: [Espec. em Arco] Pontos Necessários",

                // === Tier 1-1: Tiro Focado (2) ===
                ["Tier1_FocusedShot_CritBonus"] = "Tier 1-1: [Tiro Focado] Bônus de Chance Crítica (%)",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1-1: [Tiro Focado] Pontos Necessários",

                // === Tier 1-2: Disparo Múltiplo Nv1 (5) ===
                ["Tier1_MultishotLv1_ActivationChance"] = "Tier 1-2: [Disp. Múltiplo Nv1] Chance de Ativação (%)",
                ["Tier1_MultishotLv1_AdditionalArrows"] = "Tier 1-2: [Disp. Múltiplo Nv1] Flechas Adicionais",
                ["Tier1_MultishotLv1_ArrowConsumption"] = "Tier 1-2: [Disp. Múltiplo Nv1] Consumo de Flechas",
                ["Tier1_MultishotLv1_DamagePerArrow"] = "Tier 1-2: [Disp. Múltiplo Nv1] Dano por Flecha (%)",
                ["Tier1_MultishotLv1_RequiredPoints"] = "Tier 1-2: [Disp. Múltiplo Nv1] Pontos Necessários",

                // === Tier 2: Proficiência com Arco (3) ===
                ["Tier2_BowMastery_SkillBonus"] = "Tier 2: [Prof. com Arco] Bônus de Habilidade com Arco",
                ["Tier2_BowMastery_SpecialArrowChance"] = "Tier 2: [Prof. com Arco] Chance de Flecha Especial (%)",
                ["Tier2_BowMastery_RequiredPoints"] = "Tier 2: [Prof. com Arco] Pontos Necessários",

                // === Tier 3-1: Tiro Silencioso (2) ===
                ["Tier3_SilentStrike_DamageBonus"] = "Tier 3-1: [Tiro Silencioso] Aumento de Dano",
                ["Tier3_SilentStrike_RequiredPoints"] = "Tier 3-1: [Tiro Silencioso] Pontos Necessários",

                // === Tier 3-2: Disparo Múltiplo Nv2 (2) ===
                ["Tier3_MultishotLv2_ActivationChance"] = "Tier 3-2: [Disp. Múltiplo Nv2] Chance de Ativação (%)",
                ["Tier3_MultishotLv2_RequiredPoints"] = "Tier 3-2: [Disp. Múltiplo Nv2] Pontos Necessários",

                // === Tier 3-3: Instinto de Caçador (2) ===
                ["Tier3_HuntingInstinct_CritBonus"] = "Tier 3-3: [Instinto de Caçador] Chance Crítica (%)",
                ["Tier3_HuntingInstinct_RequiredPoints"] = "Tier 3-3: [Instinto de Caçador] Pontos Necessários",

                // === Tier 4: Mira de Precisão (2) ===
                ["Tier4_PrecisionAim_CritDamage"] = "Tier 4: [Mira de Precisão] Bônus de Dano Crítico (%)",
                ["Tier4_PrecisionAim_RequiredPoints"] = "Tier 4: [Mira de Precisão] Pontos Necessários",

                // === Tier 5: Flecha Explosiva (5) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] = "Tier 5: [Flecha Explosiva] Multiplicador de Dano (%)",
                ["Tier5_ExplosiveArrow_Radius"] = "Tier 5: [Flecha Explosiva] Raio da Explosão (m)",
                ["Tier5_ExplosiveArrow_Cooldown"] = "Tier 5: [Flecha Explosiva] Recarga (seg)",
                ["Tier5_ExplosiveArrow_StaminaCost"] = "Tier 5: [Flecha Explosiva] Custo de Stamina (%)",
                ["Tier5_ExplosiveArrow_RequiredPoints"] = "Tier 5: [Flecha Explosiva] Pontos Necessários",

                // ============================================
                // Árvore de Espada (Legado) - 30 Chaves
                // ============================================

                ["Sword_Expert_DamageIncrease"] = "Tier 0: Aumento de Dano da Espada (%)",
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: Bônus de Vel. de Ataque (%)",
                ["Sword_ComboSlash_Bonus"] = "Tier 2: Bônus de Ataque em Combo (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: Duração do Buff (seg)",
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: Bônus de Dano",
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: Bônus de Vel. de Ataque (%)",
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: Duração do Buff (seg)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: Bônus de Ataque de Carga (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: Distância de Empurrão (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: Custo de Stamina",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: Recarga (seg)",
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: Proporção de Dano 1º Golpe (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: Proporção de Dano 2º Golpe (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: Proporção de Dano 3º Golpe (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: Distância de Investida Inicial (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: Distância de Movimento Lateral (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: Custo de Stamina",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: Recarga (seg)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: Vel. de Movimento (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: Bônus de Vel. de Ataque (%)",
            };
        }
    }
}

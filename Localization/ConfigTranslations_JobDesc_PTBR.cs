using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJobDescriptions_PTBR()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Archer Job (arqueiro)
                // ========================================

                // === Archer Job: Habilidade ativa «Disparo Múltiplo» (6 chaves) ===
                ["Archer_MultiShot_ArrowCount"] =
                "【Quantidade de Flechas】\n" +
                "Número de flechas disparadas por Disparo Múltiplo.\n" +
                "Mais flechas = mais dano em área.\n" +
                "Valor recomendado: 4-7",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【Consumo de Flechas】\n" +
                "Quantidade de flechas consumidas por Disparo Múltiplo.\n" +
                "Consumo baixo para ataques eficientes.\n" +
                "Valor recomendado: 1-2",

                ["Archer_MultiShot_DamagePercent"] =
                "【Dano por Flecha (%)】\n" +
                "Porcentagem de dano de cada flecha individual.\n" +
                "Porcentagem do ataque base do arco.\n" +
                "Valor recomendado: 40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar Disparo Múltiplo.\n" +
                "Menor valor = pode usar com mais frequência.\n" +
                "Valor recomendado: 25-40 seg",

                ["Archer_MultiShot_Charges"] =
                "【Quantidade de Cargas】\n" +
                "Número de usos consecutivos de Disparo Múltiplo.\n" +
                "Múltiplos disparos para concentrar dano.\n" +
                "Valor recomendado: 2-4",

                ["Archer_MultiShot_StaminaCost"] =
                "【Custo de Stamina】\n" +
                "Stamina consumida ao usar Disparo Múltiplo.\n" +
                "Gerenciar stamina é importante.\n" +
                "Valor recomendado: 20-35",

                ["Archer_MultiShot_FireInterval"] =
                "【Intervalo de Disparo Sequencial (seg)】\n" +
                "Intervalo entre cada flecha da salva.\n" +
                "5 flechas disparam sequencialmente neste intervalo.\n" +
                "Valor recomendado: 0.15-0.3 seg",

                // === Archer Job: Habilidades passivas (2 chaves) ===
                ["Archer_JumpHeightBonus"] =
                "【Bônus de Altura de Salto (%)】\n" +
                "Aumenta a altura base do salto.\n" +
                "Facilita alcançar lugares elevados.\n" +
                "Valor recomendado: 15-25%",

                ["Archer_FallDamageReduction"] =
                "【Redução de Dano de Queda (%)】\n" +
                "Reduz o dano ao cair de alturas.\n" +
                "Melhora a mobilidade do arqueiro.\n" +
                "Valor recomendado: 40-60%",

                // === Archer Job: Bônus por nível (9 chaves) ===
                ["Archer_Lv2_BonusArrows"] =
                "【Nv.2: Flechas Adicionais】\n" +
                "Flechas extras ao avançar para Nv.2.\n" +
                "Somado à quantidade base de flechas.\n" +
                "Valor recomendado: 1",

                ["Archer_Lv2_DamagePercent"] =
                "【Nv.2: Dano por Flecha (%)】\n" +
                "Multiplicador de dano por flecha no Nv.2.\n" +
                "Aplicado como % do dano total de arco+flecha.\n" +
                "Valor recomendado: 50-60%",

                ["Archer_Lv3_BonusArrows"] =
                "【Nv.3: Flechas Adicionais】\n" +
                "Flechas extras ao avançar para Nv.3.\n" +
                "Somado à quantidade base de flechas.\n" +
                "Valor recomendado: 2",

                ["Archer_Lv3_DamagePercent"] =
                "【Nv.3: Dano por Flecha (%)】\n" +
                "Multiplicador de dano por flecha no Nv.3.\n" +
                "Aplicado como % do dano total de arco+flecha.\n" +
                "Valor recomendado: 55-65%",

                ["Archer_Lv4_BonusArrows"] =
                "【Nv.4: Flechas Adicionais】\n" +
                "Flechas extras ao avançar para Nv.4.\n" +
                "Somado à quantidade base de flechas.\n" +
                "Valor recomendado: 3",

                ["Archer_Lv4_DamagePercent"] =
                "【Nv.4: Dano por Flecha (%)】\n" +
                "Multiplicador de dano por flecha no Nv.4.\n" +
                "Aplicado como % do dano total de arco+flecha.\n" +
                "Valor recomendado: 60-70%",

                ["Archer_Lv5_BonusArrows"] =
                "【Nv.5: Flechas Adicionais】\n" +
                "Flechas extras ao avançar para Nv.5.\n" +
                "Somado à quantidade base de flechas.\n" +
                "Valor recomendado: 3",

                ["Archer_Lv5_DamagePercent"] =
                "【Nv.5: Dano por Flecha (%)】\n" +
                "Multiplicador de dano por flecha no Nv.5.\n" +
                "Aplicado como % do dano total de arco+flecha.\n" +
                "Valor recomendado: 60-70%",

                ["Archer_Lv5_BonusCharges"] =
                "【Nv.5: Cargas Adicionais】\n" +
                "Cargas extras de Disparo Múltiplo no Nv.5.\n" +
                "Somado à quantidade base de cargas.\n" +
                "Valor recomendado: 1",

                // === Arqueiro: Bônus passivos por nível (8 chaves) ===
                ["Archer_Lv2_JumpHeightBonus"] =
                "【Nv.2 Passivo: Bônus de Altura de Salto (%)】\n" +
                "Bônus extra de altura de salto no Nv.2.\n" +
                "Somado ao valor base do Nv.1.\n" +
                "Valor recomendado: 10%",

                ["Archer_Lv3_JumpHeightBonus"] =
                "【Nv.3 Passivo: Bônus de Altura de Salto (%)】\n" +
                "Bônus extra de altura de salto no Nv.3.\n" +
                "Valor recomendado: 20%",

                ["Archer_Lv4_JumpHeightBonus"] =
                "【Nv.4 Passivo: Bônus de Altura de Salto (%)】\n" +
                "Bônus extra de altura de salto no Nv.4.\n" +
                "Valor recomendado: 20%",

                ["Archer_Lv5_JumpHeightBonus"] =
                "【Nv.5 Passivo: Bônus de Altura de Salto (%)】\n" +
                "Bônus extra de altura de salto no Nv.5.\n" +
                "Valor recomendado: 20%",

                ["Archer_Lv3_FallDamageReduction"] =
                "【Nv.3 Passivo: Redução de Dano de Queda (%)】\n" +
                "Redução extra de dano de queda no Nv.3.\n" +
                "Somado ao valor base do Nv.1.\n" +
                "Valor recomendado: 10%",

                ["Archer_Lv4_FallDamageReduction"] =
                "【Nv.4 Passivo: Redução de Dano de Queda (%)】\n" +
                "Redução extra de dano de queda no Nv.4.\n" +
                "Valor recomendado: 20%",

                ["Archer_Lv5_FallDamageReduction"] =
                "【Nv.5 Passivo: Redução de Dano de Queda (%)】\n" +
                "Redução extra de dano de queda no Nv.5.\n" +
                "Valor recomendado: 35%",

                ["Archer_ElementalResistPerLevel"] =
                "【Passivo: Resistência Elemental por Nível (%)】\n" +
                "Resistência elemental base por nível do arqueiro.\n" +
                "Veneno(Nv2+), Frio(Nv3+), Fogo(Nv4+), Raio(Nv5).\n" +
                "Valor recomendado: 10%",

                // ========================================
                // Mage Job (mago)
                // ========================================

                // === Mage Job: Habilidade ativa «AOE» (5 chaves) ===
                ["Mage_AOE_Range"] =
                "【Alcance AOE (m)】\n" +
                "Raio do ataque mágico em área.\n" +
                "Alcance amplo para atingir múltiplos inimigos.\n" +
                "Valor recomendado: 10-15m",

                ["Mage_Eitr_Cost"] =
                "【Custo de Eitr】\n" +
                "Eitr consumido ao usar a habilidade.\n" +
                "Gerenciar o recurso mágico é importante.\n" +
                "Valor recomendado: 30-45",

                ["Mage_Damage_Multiplier"] =
                "【Multiplicador de Dano (%)】\n" +
                "Multiplicador de dano do ataque mágico em área.\n" +
                "Magia explosiva poderosa para eliminar inimigos.\n" +
                "Valor recomendado: 250-350%",

                ["Mage_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Recarga longa devido ao efeito poderoso.\n" +
                "Valor recomendado: 150-200 seg",

                // === Mage Job: Habilidade passiva (1 chave) ===
                ["Mage_Elemental_Resistance"] =
                "【Resistência Elemental (%)】\n" +
                "Aumenta resistência a fogo, gelo, raio, veneno e espírito.\n" +
                "Dano físico excluído — apenas dano mágico reduzido.\n" +
                "Valor recomendado: 12-20%",

                // === Berserker Job: Bônus passivo de HP ===
                ["berserker_passive_health_bonus"] =
                "【Bônus de HP Máximo (%)】\n" +
                "Berserker passivo: aumenta o HP máximo.\n" +
                "Aplicado como % do HP total (base + MMO + todos os bônus).\n" +
                "Cura funciona corretamente (incluído em m_baseHP).\n" +
                "Valor recomendado: 100%",

                // === Berserker Lv2~5 Passivo Config ===
                ["Berserker_Lv2_CooldownReduction"] =
                "【Berserker Lv2: Redução de CD da Fúria (seg)】\n" +
                "No Lv2, reduz o cooldown da Fúria por este valor.\n" +
                "Recomendado: 5 segundos",

                ["Berserker_Lv3_RageDamageReduction"] =
                "【Berserker Lv3: Redução de dano em fúria (%)】\n" +
                "No Lv3, reduz o dano recebido durante o estado de fúria.\n" +
                "Recomendado: 15%",

                ["Berserker_Lv4_LowHpAttackBonus"] =
                "【Berserker Lv4: Bônus de ataque com HP baixo (%)】\n" +
                "No Lv4, aumenta o ataque quando o HP fica abaixo do limiar.\n" +
                "Recomendado: 15%",

                ["Berserker_Lv4_LowHpAttackThreshold"] =
                "【Berserker Lv4: Limiar de HP baixo (%)】\n" +
                "Abaixo desta % de HP, o bônus de ataque do Lv4 se ativa.\n" +
                "Recomendado: 50%",

                ["Berserker_Lv5_PassiveCooldownReduction"] =
                "【Berserker Lv5: Redução CD Desafio à Morte (seg)】\n" +
                "No Lv5, reduz o cooldown do passivo por este valor.\n" +
                "Recomendado: 120 segundos",

                ["Berserker_Lv5_InvincibilityBonus"] =
                "【Berserker Lv5: Bônus de invencibilidade (seg)】\n" +
                "No Lv5, estende a duração da invencibilidade ao ativar Desafio à Morte.\n" +
                "Recomendado: 2 segundos",

                // ========================================
                // Tanker Job (tanque)
                // ========================================

                // === Tanker Job: Habilidade ativa «Provocação» (9 chaves) ===
                ["Tanker_Taunt_Cooldown"] =
                "【Recarga da Provocação (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 45-90 seg",

                ["Tanker_Taunt_StaminaCost"] =
                "【Custo de Stamina da Provocação】\n" +
                "Stamina consumida ao ativar a Provocação.\n" +
                "Valor recomendado: 20-30",

                ["Tanker_Taunt_Range"] =
                "【Alcance da Provocação (m)】\n" +
                "Raio em que os inimigos são provocados.\n" +
                "Valor recomendado: 10-15m",

                ["Tanker_Taunt_Duration"] =
                "【Duração da Provocação em Monstros Comuns (seg)】\n" +
                "Tempo de efeito da provocação em monstros comuns.\n" +
                "Valor recomendado: 4-8 seg",

                ["Tanker_Taunt_BossDuration"] =
                "【Duração da Provocação em Chefes (seg)】\n" +
                "Tempo de efeito da provocação em chefes.\n" +
                "Chefes são mais resistentes — efeito mais curto.\n" +
                "Valor recomendado: 1-3 seg",

                ["Tanker_Taunt_DamageReduction"] =
                "【Redução de Dano Recebido (%)】\n" +
                "Redução do dano recebido durante o buff ativo de Provocação.\n" +
                "Valor recomendado: 15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【Duração do Buff de Redução de Dano (seg)】\n" +
                "Tempo de ação do buff de redução de dano após ativação.\n" +
                "Valor recomendado: 4-8 seg",

                ["Tanker_Taunt_ReflectPercent"] =
                "【Dano de Reflexo de Provocação (%)】\n" +
                "Reflete parte do dano recebido de volta aos atacantes durante o buff de Grito de Guerra.\n" +
                "Ativo durante a duração do buff.\n" +
                "Valor recomendado: 5-20%",

                ["Tanker_Taunt_EffectHeight"] =
                "【Altura do Ícone de Provocação (m)】\n" +
                "Altura acima do monstro onde o ícone de provocação é exibido.\n" +
                "Valor recomendado: 1.5-2.5m",

                ["Tanker_Taunt_EffectScale"] =
                "【Escala do Ícone de Provocação】\n" +
                "Multiplicador do tamanho do efeito visual de provocação.\n" +
                "Valor recomendado: 0.2-0.5",

                // === Tanker Job: Habilidade passiva (1 chave) ===
                ["Tanker_Passive_DamageReduction"] =
                "【Redução Passiva de Dano do Tanque (%)】\n" +
                "Tanque passivo: reduz continuamente o dano recebido.\n" +
                "Valor recomendado: 10-20%",

                ["Tanker_NormalShield_SpeedBonus"] =
                "【Tanque: Velocidade com Escudo Normal (%)】\n" +
                "Tanque Lv1+: Bônus de velocidade ao equipar escudo normal.\n" +
                "Padrão: 25%",

                ["Tanker_TowerShield_SpeedBonus"] =
                "【Tanque: Velocidade com Torre-Escudo (%)】\n" +
                "Tanque Lv1+: Bônus de velocidade ao equipar torre-escudo.\n" +
                "Padrão: 30%",

                // === Lv1 ===
                ["Tanker_ReflectDuration_Lv1"] =
                "【Duração do Reflexo do Tanque Lv1 (seg)】\n" +
                "Padrão: 10 seg",

                ["Tanker_Hp_Bonus_Lv1"] =
                "【Bônus de HP do Tanque Lv1 (%)】\n" +
                "O HP máximo aumenta percentualmente ao atingir o Tanque Lv1.\n" +
                "Padrão: 25",

                // === Lv2 ===
                ["Tanker_Hp_Bonus_Lv2"] =
                "【Bônus de HP do Tanque Lv2 (%)】\n" +
                "O HP máximo aumenta percentualmente ao atingir o Tanque Lv2.\n" +
                "Padrão: 30",

                ["Tanker_Lv2_BlockPower"] =
                "【Poder de Bloqueio do Tanque Lv2】\n" +
                "Poder de bloqueio passivo do Tanque no Lv2.\n" +
                "Padrão: 5",

                ["Tanker_ReflectDuration_Lv2"] =
                "【Duração do Reflexo do Tanque Lv2 (seg)】\n" +
                "Padrão: 12 seg",

                // === Lv3 ===
                ["Tanker_Hp_Bonus_Lv3"] =
                "【Bônus de HP do Tanque Lv3 (%)】\n" +
                "O HP máximo aumenta percentualmente ao atingir o Tanque Lv3.\n" +
                "Padrão: 35",

                ["Tanker_Lv3_BlockPower"] =
                "【Poder de Bloqueio do Tanque Lv3】\n" +
                "Poder de bloqueio passivo do Tanque no Lv3.\n" +
                "Padrão: 10",

                ["Tanker_ReflectDuration_Lv3"] =
                "【Duração do Reflexo do Tanque Lv3 (seg)】\n" +
                "Padrão: 14 seg",

                // === Lv4 ===
                ["Tanker_Hp_Bonus_Lv4"] =
                "【Bônus de HP do Tanque Lv4 (%)】\n" +
                "O HP máximo aumenta percentualmente ao atingir o Tanque Lv4.\n" +
                "Padrão: 40",

                ["Tanker_Lv4_BlockPower"] =
                "【Poder de Bloqueio do Tanque Lv4】\n" +
                "Poder de bloqueio passivo do Tanque no Lv4.\n" +
                "Padrão: 15",

                ["Tanker_ReflectDuration_Lv4"] =
                "【Duração do Reflexo do Tanque Lv4 (seg)】\n" +
                "Padrão: 16 seg",

                // === Lv5 ===
                ["Tanker_Hp_Bonus_Lv5"] =
                "【Bônus de HP do Tanque Lv5 (%)】\n" +
                "O HP máximo aumenta percentualmente ao atingir o Tanque Lv5.\n" +
                "Padrão: 50",

                ["Tanker_Lv5_BlockPower"] =
                "【Poder de Bloqueio do Tanque Lv5】\n" +
                "Poder de bloqueio passivo do Tanque no Lv5.\n" +
                "Padrão: 20",

                ["Tanker_ReflectDuration_Lv5"] =
                "【Duração do Reflexo do Tanque Lv5 (seg)】\n" +
                "Padrão: 20 seg",

                // ========================================
                // Rogue Job (ladino)
                // ========================================

                // === Rogue Job: Habilidade ativa «Golpe das Sombras» (7 chaves) ===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【Recarga do Golpe das Sombras (seg)】\n" +
                "Tempo de espera para reutilizar Golpe das Sombras.\n" +
                "Valor recomendado: 20-40 seg",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【Custo de Stamina do Golpe das Sombras】\n" +
                "Stamina consumida ao ativar Golpe das Sombras.\n" +
                "Valor recomendado: 20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【Bônus de Ataque do Golpe das Sombras (%)】\n" +
                "Aumento de ataque durante a duração do buff após ativação.\n" +
                "Valor recomendado: 25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【Duração do Buff de Ataque (seg)】\n" +
                "Tempo de ação do buff de aumento de ataque.\n" +
                "Valor recomendado: 6-12 seg",

                ["Rogue_ShadowStrike_SmokeScale"] =
                "【Escala do Efeito de Fumaça】\n" +
                "Multiplicador do tamanho do VFX de fumaça.\n" +
                "Valor recomendado: 1.5-3.0",

                ["Rogue_ShadowStrike_AggroRange"] =
                "【Alcance de Remoção de Aggro (m)】\n" +
                "Remove o aggro de todos os inimigos neste raio.\n" +
                "Valor recomendado: 10-20m",

                ["Rogue_ShadowStrike_StealthDuration"] =
                "【Duração do Furtividade (seg)】\n" +
                "Tempo de ação do modo furtivo.\n" +
                "Valor recomendado: 5-10 seg",

                // === Rogue Job: Habilidades passivas (3 chaves) ===
                ["Rogue_AttackSpeed_Bonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Ladino passivo: aumenta continuamente a velocidade de ataque.\n" +
                "Valor recomendado: 8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【Redução de Custo de Stamina em Ataques (%)】\n" +
                "Ladino passivo: reduz o consumo de stamina em ataques.\n" +
                "Valor recomendado: 10-20%",

                ["Rogue_Lv1_DodgeChance"] =
                "【Lv1 Taxa de Esquiva (%)】\n" +
                "Ladino passivo: aumenta a taxa de esquiva. Acumula com total da árvore de habilidades.\n" +
                "Valor recomendado: 3-6%",
                ["Rogue_Lv2_DodgeChance"] = "【Lv2 Taxa de Esquiva (%)】\nValor recomendado: 5-8%",
                ["Rogue_Lv3_DodgeChance"] = "【Lv3 Taxa de Esquiva (%)】\nValor recomendado: 7-10%",
                ["Rogue_Lv4_DodgeChance"] = "【Lv4 Taxa de Esquiva (%)】\nValor recomendado: 9-12%",
                ["Rogue_Lv5_DodgeChance"] = "【Lv5 Taxa de Esquiva (%)】\nValor recomendado: 11-15%",

                // ========================================
                // Paladin Job (paladino)
                // ========================================

                // === Paladin Job: Habilidade ativa «Cura Sagrada» (8 chaves) ===
                ["Paladin_Active_Cooldown"] =
                "【Recarga da Cura Sagrada (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 20-45 seg",

                ["Paladin_Active_Range"] =
                "【Alcance da Cura Sagrada (m)】\n" +
                "Raio em que os aliados recebem cura.\n" +
                "Valor recomendado: 4-8m",

                ["Paladin_Active_EitrCost"] =
                "【Custo de Eitr da Cura Sagrada】\n" +
                "Eitr consumido ao ativar Cura Sagrada.\n" +
                "Valor recomendado: 8-15",

                ["Paladin_Active_StaminaCost"] =
                "【Custo de Stamina da Cura Sagrada】\n" +
                "Stamina consumida ao ativar Cura Sagrada.\n" +
                "Valor recomendado: 8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【Porcentagem de Auto-Cura (% do HP máx)】\n" +
                "Porcentagem do próprio HP restaurado ao ativar.\n" +
                "Valor recomendado: 10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【Cura Gradual de Aliados (% do HP máx por seg)】\n" +
                "Porcentagem de HP restaurado a cada aliado por segundo.\n" +
                "Valor recomendado: 1-3%",

                ["Paladin_Active_Duration"] =
                "【Duração da Cura Gradual (seg)】\n" +
                "Tempo total de ação do efeito de cura gradual dos aliados.\n" +
                "Valor recomendado: 8-15 seg",

                ["Paladin_Active_Interval"] =
                "【Intervalo de Cura (seg)】\n" +
                "Período de aplicação da cura gradual.\n" +
                "Valor recomendado: 1 seg",

                // === Paladin Job: Habilidade passiva (1 chave) ===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【Bônus de Resistência Física e Elemental (%)】\n" +
                "Paladino passivo: aumenta a resistência a dano físico e elemental.\n" +
                "Valor recomendado: 5-12%",

                // === Paladino Lv2-5 ===
                ["Paladin_Lv2_SelfHealPercent"] = "【Lv2 Auto-cura (%)】\nRecomendado: 15-20%",
                ["Paladin_Lv2_AllyHealPercent"] = "【Lv2 Cura de Aliados (%/tick)】\nRecomendado: 2-3%",
                ["Paladin_Lv3_SelfHealPercent"] = "【Lv3 Auto-cura (%)】\nRecomendado: 17-22%",
                ["Paladin_Lv3_AllyHealPercent"] = "【Lv3 Cura de Aliados (%/tick)】\nRecomendado: 2.5-3.5%",
                ["Paladin_Lv3_HealRange"] = "【Lv3 Alcance de Cura (m)】\nRecomendado: 5-7m",
                ["Paladin_Lv4_SelfHealPercent"] = "【Lv4 Auto-cura (%)】\nRecomendado: 19-24%",
                ["Paladin_Lv4_AllyHealPercent"] = "【Lv4 Cura de Aliados (%/tick)】\nRecomendado: 3-4%",
                ["Paladin_Lv4_HealRange"] = "【Lv4 Alcance de Cura (m)】\nRecomendado: 6-8m",
                ["Paladin_Lv5_SelfHealPercent"] = "【Lv5 Auto-cura (%)】\nRecomendado: 22-28%",
                ["Paladin_Lv5_AllyHealPercent"] = "【Lv5 Cura de Aliados (%/tick)】\nRecomendado: 3.5-5%",
                ["Paladin_Lv5_HealRange"] = "【Lv5 Alcance de Cura (m)】\nRecomendado: 7-10m",
                ["Paladin_Lv2_Cooldown"] = "【Lv2 Tempo de Recarga (seg)】\nRecomendado: 25-35 seg",
                ["Paladin_Lv3_Cooldown"] = "【Lv3 Tempo de Recarga (seg)】\nRecomendado: 24-34 seg",
                ["Paladin_Lv4_Cooldown"] = "【Lv4 Tempo de Recarga (seg)】\nRecomendado: 23-33 seg",
                ["Paladin_Lv5_Cooldown"] = "【Lv5 Tempo de Recarga (seg)】\nRecomendado: 20-30 seg",
                ["Paladin_Lv2_ResistanceReduction"] = "【Lv2 Redução de Resistência (%)】\nRecomendado: 6-10%",
                ["Paladin_Lv3_ResistanceReduction"] = "【Lv3 Redução de Resistência (%)】\nRecomendado: 8-12%",
                ["Paladin_Lv4_ResistanceReduction"] = "【Lv4 Redução de Resistência (%)】\nRecomendado: 10-14%",
                ["Paladin_Lv5_ResistanceReduction"] = "【Lv5 Redução de Resistência (%)】\nRecomendado: 12-18%",
                ["Paladin_Lv2_StaminaBonus"] = "【Lv2 Bônus Stamina Máx.】\nRecomendado: 8-15",
                ["Paladin_Lv3_StaminaBonus"] = "【Lv3 Bônus Stamina Máx.】\nRecomendado: 12-20",
                ["Paladin_Lv4_StaminaBonus"] = "【Lv4 Bônus Stamina Máx.】\nRecomendado: 15-25",
                ["Paladin_Lv5_StaminaBonus"] = "【Lv5 Bônus Stamina Máx.】\nRecomendado: 20-30",

                // ========================================
                // Berserker Job (berserker)
                // ========================================

                // === Berserker Job: Ativo «Fúria do Berserker» (6 chaves, typo Beserker mantido) ===
                ["Beserker_Active_Cooldown"] =
                "【Recarga da Fúria do Berserker (seg)】\n" +
                "Tempo de espera para reutilizar Fúria do Berserker.\n" +
                "Valor recomendado: 30-60 seg",

                ["Beserker_Active_StaminaCost"] =
                "【Custo de Stamina da Fúria do Berserker】\n" +
                "Stamina consumida ao ativar Fúria do Berserker.\n" +
                "Valor recomendado: 15-25",

                ["Beserker_Active_Duration"] =
                "【Duração da Fúria do Berserker (seg)】\n" +
                "Tempo de ação do buff de Fúria do Berserker.\n" +
                "Valor recomendado: 15-25 seg",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【Bônus de Dano por 1% de HP Perdido (%)】\n" +
                "O dano aumenta conforme o HP diminui.\n" +
                "% de HP perdido × este valor = bônus de dano\n" +
                "Valor recomendado: 1.5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【Bônus de Dano Máximo (%)】\n" +
                "Limite máximo do bônus de dano vinculado ao HP.\n" +
                "Valor recomendado: 150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【Limite de HP para Ativação (%)】\n" +
                "O bônus de dano vinculado ao HP ativa abaixo deste % de HP.\n" +
                "Defina 100% para ativação constante.\n" +
                "Valor recomendado: 50-100%",

                // === Berserker Job: Passivo «Desafio da Morte» (3 chaves, typo Beserker mantido) ===
                ["Berserker_Passive_HealthThreshold"] =
                "【Limite de HP para Ativação do Passivo (%)】\n" +
                "A invencibilidade ativa quando o HP cai abaixo deste %.\n" +
                "Valor recomendado: 8-15%",

                ["Berserker_Passive_InvincibilityDuration"] =
                "【Duração da Invencibilidade (seg)】\n" +
                "Tempo de ação da invencibilidade ao acionar o passivo.\n" +
                "Valor recomendado: 5-10 seg",

                ["Berserker_Passive_Cooldown"] =
                "【Recarga do Passivo (seg)】\n" +
                "Tempo de espera até o próximo acionamento da invencibilidade passiva.\n" +
                "Padrão: 540 seg (9 minutos)\n" +
                "Valor recomendado: 120-300 seg",

                // === Berserker Job: Bônus passivo de HP (chave com correção de capitalização) ===
                ["Berserker_Passive_HealthBonus"] =
                "【Bônus de HP Máximo (%)】\n" +
                "Berserker passivo: aumenta o HP máximo.\n" +
                "Valor recomendado: 100%",

                // === Berserker: Recarga da Fúria por nível ===
                ["Berserker_Lv1_Active_Cooldown"] =
                "【Berserker Lv1: Recarga da Fúria (seg)】\n" +
                "Recarga da habilidade Fúria no Lv1.\n" +
                "Recomendado: 45 seg",

                ["Berserker_Lv2_Active_Cooldown"] =
                "【Berserker Lv2: Recarga da Fúria (seg)】\n" +
                "Recarga da habilidade Fúria no Lv2.\n" +
                "Recomendado: 40 seg",

                ["Berserker_Lv3_Active_Cooldown"] =
                "【Berserker Lv3: Recarga da Fúria (seg)】\n" +
                "Recarga da habilidade Fúria no Lv3.\n" +
                "Recomendado: 40 seg",

                ["Berserker_Lv4_Active_Cooldown"] =
                "【Berserker Lv4: Recarga da Fúria (seg)】\n" +
                "Recarga da habilidade Fúria no Lv4.\n" +
                "Recomendado: 40 seg",

                ["Berserker_Lv5_Active_Cooldown"] =
                "【Berserker Lv5: Recarga da Fúria (seg)】\n" +
                "Recarga da habilidade Fúria no Lv5.\n" +
                "Recomendado: 35 seg",

                // === Berserker: Duração da Fúria por nível ===
                ["Berserker_Lv1_Active_Duration"] =
                "【Berserker Lv1: Duração da Fúria (seg)】\n" +
                "Duração do efeito Fúria no Lv1.\n" +
                "Recomendado: 20 seg",

                ["Berserker_Lv2_Active_Duration"] =
                "【Berserker Lv2: Duração da Fúria (seg)】\n" +
                "Duração do efeito Fúria no Lv2.\n" +
                "Recomendado: 20 seg",

                ["Berserker_Lv3_Active_Duration"] =
                "【Berserker Lv3: Duração da Fúria (seg)】\n" +
                "Duração do efeito Fúria no Lv3.\n" +
                "Recomendado: 25 seg",

                ["Berserker_Lv4_Active_Duration"] =
                "【Berserker Lv4: Duração da Fúria (seg)】\n" +
                "Duração do efeito Fúria no Lv4.\n" +
                "Recomendado: 25 seg",

                ["Berserker_Lv5_Active_Duration"] =
                "【Berserker Lv5: Duração da Fúria (seg)】\n" +
                "Duração do efeito Fúria no Lv5.\n" +
                "Recomendado: 25 seg",

                // === Berserker: Bônus passivo de HP máximo por nível ===
                ["Berserker_Lv1_Passive_HealthBonus"] =
                "【Berserker Lv1: Bônus de HP Máximo】\n" +
                "Bônus fixo de HP máximo no Lv1.\n" +
                "Recomendado: 40",

                ["Berserker_Lv2_Passive_HealthBonus"] =
                "【Berserker Lv2: Bônus de HP Máximo】\n" +
                "Bônus fixo de HP máximo no Lv2.\n" +
                "Recomendado: 60",

                ["Berserker_Lv3_Passive_HealthBonus"] =
                "【Berserker Lv3: Bônus de HP Máximo】\n" +
                "Bônus fixo de HP máximo no Lv3.\n" +
                "Recomendado: 80",

                ["Berserker_Lv4_Passive_HealthBonus"] =
                "【Berserker Lv4: Bônus de HP Máximo】\n" +
                "Bônus fixo de HP máximo no Lv4.\n" +
                "Recomendado: 100",

                ["Berserker_Lv5_Passive_HealthBonus"] =
                "【Berserker Lv5: Bônus de HP Máximo】\n" +
                "Bônus fixo de HP máximo no Lv5.\n" +
                "Recomendado: 120",

                // === Berserker: Bônus de dano por 1% de HP perdido por nível ===
                ["Berserker_Lv1_Active_DamagePerHP"] =
                "【Berserker Lv1: Dano/1% HP perdido (%)】\n" +
                "Bônus de dano por cada 1% de HP perdido durante Fúria (Lv1).\n" +
                "Recomendado: 1,5%",

                ["Berserker_Lv2_Active_DamagePerHP"] =
                "【Berserker Lv2: Dano/1% HP perdido (%)】\n" +
                "Bônus de dano por cada 1% de HP perdido durante Fúria (Lv2).\n" +
                "Recomendado: 1,6%",

                ["Berserker_Lv3_Active_DamagePerHP"] =
                "【Berserker Lv3: Dano/1% HP perdido (%)】\n" +
                "Bônus de dano por cada 1% de HP perdido durante Fúria (Lv3).\n" +
                "Recomendado: 1,7%",

                ["Berserker_Lv4_Active_DamagePerHP"] =
                "【Berserker Lv4: Dano/1% HP perdido (%)】\n" +
                "Bônus de dano por cada 1% de HP perdido durante Fúria (Lv4).\n" +
                "Recomendado: 1,8%",

                ["Berserker_Lv5_Active_DamagePerHP"] =
                "【Berserker Lv5: Dano/1% HP perdido (%)】\n" +
                "Bônus de dano por cada 1% de HP perdido durante Fúria (Lv5).\n" +
                "Recomendado: 2,0%",

                // === Mestre Artesão (Producer) Job Skills ===
                // ========================================
                ["Producer_Buff_Cooldown"] =
                "【Bênção do Artesão: Recarga (seg)】\n" +
                "Recarga entre ativações do buff do Mestre Artesão.\n" +
                "Padrão: 180 seg",

                ["Producer_Buff_Duration"] =
                "【Bênção do Artesão: Duração (seg)】\n" +
                "Duração do buff de ataque/HP para aliados.\n" +
                "Padrão: 120 seg",

                ["Producer_Buff_Range"] =
                "【Bênção do Artesão: Alcance (m)】\n" +
                "Alcance no qual os aliados recebem o buff.\n" +
                "Padrão: 15 m",

                ["Producer_Buff_AttackBonus"] =
                "【Bônus de Ataque do Buff (%)】\n" +
                "Bônus de ataque concedido aos aliados bufados.\n" +
                "Padrão: 15%",

                ["Producer_Buff_MaxHealthBonus"] =
                "【Bônus de HP Máximo do Buff (%)】\n" +
                "Bônus de HP máximo concedido aos aliados bufados.\n" +
                "Padrão: 15%",

                ["Producer_Buff_StaminaCost"] =
                "【Custo de Stamina do Buff】\n" +
                "Stamina consumida ao ativar o buff.\n" +
                "Padrão: 20",

                // === Producer Lv1 ===
                ["Producer_EnchantChance_Lv1"] = "【Chance de Encantamento Lv1 (%)】\nChance de encantar item fabricado em Lv1.\nPadrão: 0%",

                // === Producer Lv2 ===
                ["Producer_Durability_Lv2"] = "【Bônus de Durabilidade Lv2 (%)】\nBônus de durabilidade em itens fabricados em Lv2.\nPadrão: 10%",
                ["Producer_MaterialReduction_Lv2"] = "【Redução de Material Lv2 (%)】\nMateriais economizados por fabricação em Lv2.\nPadrão: 10%",
                ["Producer_EnchantChance_Lv2"] = "【Chance de Encantamento Lv2 (%)】\nChance de encantar item fabricado em Lv2.\nPadrão: 0%",

                // === Producer Lv3 ===
                ["Producer_Durability_Lv3"] = "【Bônus de Durabilidade Lv3 (%)】\nBônus de durabilidade em itens fabricados em Lv3.\nPadrão: 15%",
                ["Producer_MaterialReduction_Lv3"] = "【Redução de Material Lv3 (%)】\nMateriais economizados por fabricação em Lv3.\nPadrão: 15%",
                ["Producer_EnchantChance_Lv3"] = "【Chance de Encantamento Lv3 (%)】\nChance de encantar item fabricado em Lv3.\nPadrão: 25%",

                // === Producer Lv4 ===
                ["Producer_Durability_Lv4"] = "【Bônus de Durabilidade Lv4 (%)】\nBônus de durabilidade em itens fabricados em Lv4.\nPadrão: 20%",
                ["Producer_MaterialReduction_Lv4"] = "【Redução de Material Lv4 (%)】\nMateriais economizados por fabricação em Lv4.\nPadrão: 20%",
                ["Producer_EnchantChance_Lv4"] = "【Chance de Encantamento Lv4 (%)】\nChance de encantar item fabricado em Lv4.\nPadrão: 30%",

                // === Producer Lv5 ===
                ["Producer_Durability_Lv5"] = "【Bônus de Durabilidade Lv5 (%)】\nBônus de durabilidade em itens fabricados em Lv5.\nPadrão: 30%",
                ["Producer_MaterialReduction_Lv5"] = "【Redução de Material Lv5 (%)】\nMateriais economizados por fabricação em Lv5.\nPadrão: 30%",
                ["Producer_EnchantChance_Lv5"] = "【Chance de Encantamento Lv5 (%)】\nChance de encantar item fabricado em Lv5.\nPadrão: 35%",

                ["Job_Lv1_Cost"] = "【Custo de Moedas Lv1 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv1.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 1000",
                ["Job_Lv2_Cost"] = "【Custo de Moedas Lv2 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv2.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 2000",
                ["Job_Lv3_Cost"] = "【Custo de Moedas Lv3 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv3.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 3000",
                ["Job_Lv4_Cost"] = "【Custo de Moedas Lv4 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv4.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 4000",
                ["Job_Lv5_Cost"] = "【Custo de Moedas Lv5 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv5.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 5000",

                ["Job_Reset_Cost"]    = "【Custo de Reset de Profissão】\nMoedas consumidas ao resetar pontos de habilidade de profissão.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 1000",
                ["Active_Reset_Cost"] = "【Custo de Reset Ativo】\nMoedas consumidas ao resetar pontos de habilidade ativa.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 500",
                ["Passive_Reset_Cost"]= "【Custo de Reset Passivo】\nMoedas consumidas ao resetar pontos de habilidade passiva.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 100",
            };
        }
    }
}

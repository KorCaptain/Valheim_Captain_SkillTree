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

                ["Rogue_ElementalResistance_Debuff"] =
                "【Aumento de Resistência Elemental (%)】\n" +
                "Ladino passivo: aumenta a resistência a dano elemental.\n" +
                "Valor recomendado: 8-15%",

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
                ["Beserker_Passive_HealthThreshold"] =
                "【Limite de HP para Ativação do Passivo (%)】\n" +
                "A invencibilidade ativa quando o HP cai abaixo deste %.\n" +
                "Valor recomendado: 8-15%",

                ["Beserker_Passive_InvincibilityDuration"] =
                "【Duração da Invencibilidade (seg)】\n" +
                "Tempo de ação da invencibilidade ao acionar o passivo.\n" +
                "Valor recomendado: 5-10 seg",

                ["Beserker_Passive_Cooldown"] =
                "【Recarga do Passivo (seg)】\n" +
                "Tempo de espera até o próximo acionamento da invencibilidade passiva.\n" +
                "Padrão: 180 seg (3 minutos)\n" +
                "Valor recomendado: 120-300 seg",

                // === Berserker Job: Bônus passivo de HP (chave com correção de capitalização) ===
                ["Berserker_Passive_HealthBonus"] =
                "【Bônus de HP Máximo (%)】\n" +
                "Berserker passivo: aumenta o HP máximo.\n" +
                "Valor recomendado: 100%",
            };
        }
    }
}

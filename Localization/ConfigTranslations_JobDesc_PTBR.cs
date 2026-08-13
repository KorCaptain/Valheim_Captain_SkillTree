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

                ["Mage_Fire_Rain_Radius"] =
                "【Raio de Queda da Chuva de Fogo (m)】\n" +
                "Raio ao redor do alvo onde 30 bolas de fogo caem.\n" +
                "Valor recomendado: 6-10 m",

                ["Mage_Fire_Rain_Impact_Radius"] =
                "【Raio de Dano de Impacto da Bola de Fogo (m)】\n" +
                "Raio de dano ao atingir cada bola de fogo.\n" +
                "Valor recomendado: 2-4 m",

                ["Mage_Fire_Rain_Projectile_Count"] =
                "【Bolas de Fogo por Rajada】\n" +
                "Número de bolas de fogo que caem por rajada.\n" +
                "Total de 2 rajadas (1ª rajada -> 1 seg. -> 2ª rajada).\n" +
                "Valor recomendado: 15-25",

                ["Mage_Dungeon_Buff_Damage_Bonus"] =
                "【Bônus de Dano do Buff de Masmorra (%)】\n" +
                "Aumento de dano do autoaperfeiçoamento lançado dentro de masmorras em vez da Chuva de Fogo.\n" +
                "Valor recomendado: 20-30%",

                ["Mage_Dungeon_Buff_Duration"] =
                "【Duração do Buff de Masmorra (seg)】\n" +
                "Por quanto tempo o buff substituto dura dentro da masmorra.\n" +
                "Valor recomendado: 8-12 seg",

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
                ["Producer_EnchantChance_Lv1"] = "【Chance de Encantamento Lv1 (%)】\nChance de encantar item fabricado em Lv1.\nPadrão: 45%",
                ["Producer_ElementalProcChance_Lv1"] = "【Chance de Dano Elemental Lv1 (%)】\nChance de um encantamento elemental Lv1 (Fogo/Espírito/Veneno/Raio/Gelo) ativar a cada acerto.\nPadrão: 25%",

                // === Producer Lv2 ===
                ["Producer_Durability_Lv2"] = "【Bônus de Durabilidade Lv2 (%)】\nBônus de durabilidade em itens fabricados em Lv2.\nPadrão: 10%",
                ["Producer_MaterialReduction_Lv2"] = "【Redução de Material Lv2 (%)】\nMateriais economizados por fabricação em Lv2.\nPadrão: 10%",
                ["Producer_EnchantChance_Lv2"] = "【Chance de Encantamento Lv2 (%)】\nChance de encantar item fabricado em Lv2.\nPadrão: 55%",
                ["Producer_ElementalProcChance_Lv2"] = "【Chance de Dano Elemental Lv2 (%)】\nChance de um encantamento elemental Lv2 ativar a cada acerto.\nPadrão: 30%",

                // === Producer Lv3 ===
                ["Producer_Durability_Lv3"] = "【Bônus de Durabilidade Lv3 (%)】\nBônus de durabilidade em itens fabricados em Lv3.\nPadrão: 15%",
                ["Producer_MaterialReduction_Lv3"] = "【Redução de Material Lv3 (%)】\nMateriais economizados por fabricação em Lv3.\nPadrão: 15%",
                ["Producer_EnchantChance_Lv3"] = "【Chance de Encantamento Lv3 (%)】\nChance de encantar item fabricado em Lv3.\nPadrão: 25%",
                ["Producer_ElementalProcChance_Lv3"] = "【Chance de Dano Elemental Lv3 (%)】\nChance de um encantamento elemental Lv3 ativar a cada acerto.\nPadrão: 35%",

                // === Producer Lv4 ===
                ["Producer_Durability_Lv4"] = "【Bônus de Durabilidade Lv4 (%)】\nBônus de durabilidade em itens fabricados em Lv4.\nPadrão: 20%",
                ["Producer_MaterialReduction_Lv4"] = "【Redução de Material Lv4 (%)】\nMateriais economizados por fabricação em Lv4.\nPadrão: 20%",
                ["Producer_EnchantChance_Lv4"] = "【Chance de Encantamento Lv4 (%)】\nChance de encantar item fabricado em Lv4.\nPadrão: 80%",
                ["Producer_ElementalProcChance_Lv4"] = "【Chance de Dano Elemental Lv4 (%)】\nChance de um encantamento elemental Lv4 ativar a cada acerto.\nPadrão: 40%",

                // === Producer Lv5 ===
                ["Producer_Durability_Lv5"] = "【Bônus de Durabilidade Lv5 (%)】\nBônus de durabilidade em itens fabricados em Lv5.\nPadrão: 30%",
                ["Producer_MaterialReduction_Lv5"] = "【Redução de Material Lv5 (%)】\nMateriais economizados por fabricação em Lv5.\nPadrão: 30%",
                ["Producer_EnchantChance_Lv5"] = "【Chance de Encantamento Lv5 (%)】\nChance de encantar item fabricado em Lv5.\nPadrão: 35%",
                ["Producer_ElementalProcChance_Lv5"] = "【Chance de Dano Elemental Lv5 (%)】\nChance de um encantamento elemental Lv5 ativar a cada acerto.\nPadrão: 45%",

                ["Job_Lv1_Cost"] = "【Custo de Moedas Lv1 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv1.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 1000",
                ["Job_Lv2_Cost"] = "【Custo de Moedas Lv2 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv2.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 2000",
                ["Job_Lv3_Cost"] = "【Custo de Moedas Lv3 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv3.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 3000",
                ["Job_Lv4_Cost"] = "【Custo de Moedas Lv4 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv4.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 4000",
                ["Job_Lv5_Cost"] = "【Custo de Moedas Lv5 de Profissão】\nMoedas consumidas ao subir qualquer profissão para Lv5.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 5000",

                ["Job_Reset_Cost"]    = "【Custo de Reset de Profissão】\nMoedas consumidas ao resetar pontos de habilidade de profissão.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 1000",
                ["Active_Reset_Cost"] = "【Custo de Reset Ativo】\nMoedas consumidas ao resetar pontos de habilidade ativa.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 500",
                ["Passive_Reset_Cost"]= "【Custo de Reset Passivo】\nMoedas consumidas ao resetar pontos de habilidade passiva.\nSomente admin do servidor, sincronizado com clientes.\nPadrão: 100",

                ["HotKey_Y"] =
                "【Tecla de Habilidade de Profissão】\n" +
                "Tecla para ativar a habilidade ativa da sua profissão.\n" +
                "Padrão: Y",

                ["HotKey_R"] =
                "【Tecla de Habilidade à Distância】\n" +
                "Tecla para ativar habilidades ativas à distância (Disparo Múltiplo, Conjuração Dupla, etc.).\n" +
                "Padrão: R",

                ["HotKey_G"] =
                "【Tecla de Habilidade Principal Corpo a Corpo】\n" +
                "Tecla para ativar habilidades ativas principais corpo a corpo (Corte Avançado, etc.).\n" +
                "Padrão: G",

                ["HotKey_H"] =
                "【Tecla de Habilidade Secundária】\n" +
                "Tecla para ativar habilidades ativas secundárias (Lança Combo, Coração do Guardião, etc.).\n" +
                "Padrão: H",

                ["QuestToggleKey"] =
                "【Atalho do Painel de Missões】\n" +
                "Atalho para abrir e fechar o painel de missões.\n" +
                "Padrão: Ctrl+J",

                ["HUD_IconSize"] =
                "【Tamanho do Ícone de Habilidade】\n" +
                "Tamanho dos ícones exibidos no HUD de habilidades ativas.\n" +
                "Padrão: 62",

                ["HUD_PosX"] =
                "【Posição X do HUD de Ícone de Habilidade】\n" +
                "Posição horizontal do HUD de habilidades ativas.\n" +
                "Padrão: 306 (a partir da esquerda da tela)",

                ["HUD_PosY"] =
                "【Posição Y do HUD de Ícone de Habilidade】\n" +
                "Posição vertical do HUD de habilidades ativas.\n" +
                "Padrão: 139 (a partir da base da tela)",

                ["Archer_Attack_StaminaReduction_Lv1"] =
                "【Lv1 Passivo: Redução de Resistência em Ataque (%)】\n" +
                "Reduz o consumo de resistência ao atacar no Arqueiro Lv1.\n" +
                "Aplica-se a todos os ataques de arco/besta/cajado.\n" +
                "Valor recomendado: 10-20%",

                ["Archer_Attack_StaminaReduction_Lv2"] =
                "【Lv2 Passivo: Redução de Resistência em Ataque (%)】\n" +
                "Reduz o consumo de resistência ao atacar no Arqueiro Lv2.\n" +
                "Valor recomendado: 20-30%",

                ["Archer_Attack_StaminaReduction_Lv3"] =
                "【Lv3 Passivo: Redução de Resistência em Ataque (%)】\n" +
                "Reduz o consumo de resistência ao atacar no Arqueiro Lv3.\n" +
                "Valor recomendado: 30-40%",

                ["Archer_Attack_StaminaReduction_Lv4"] =
                "【Lv4 Passivo: Redução de Resistência em Ataque (%)】\n" +
                "Reduz o consumo de resistência ao atacar no Arqueiro Lv4.\n" +
                "Valor recomendado: 40-50%",

                ["Archer_Attack_StaminaReduction_Lv5"] =
                "【Lv5 Passivo: Redução de Resistência em Ataque (%)】\n" +
                "Reduz o consumo de resistência ao atacar no Arqueiro Lv5.\n" +
                "Valor recomendado: 50-60%",

                ["Archer_AmmoSaveChance"] =
                "【Chance de Economizar Flecha/Virote (%)】\n" +
                "Chance de não consumir uma flecha ou virote ao atacar.\n" +
                "Em 50, em média metade das flechas são economizadas.\n" +
                "Valor recomendado: 30-60%",

                ["Archer_TameHeal_PerLevel"] =
                "【Passiva: Cura de Animal Domesticado (HP/s)】\n" +
                "Cura animais domesticados próximos a cada segundo em Nível do Arqueiro × este valor.\n" +
                "No Lv1 cura este valor, no Lv5 cura 5x este valor.\n" +
                "Valor recomendado: 1",

                ["Archer_TameHeal_Range"] =
                "【Passiva: Alcance da Cura de Animal Domesticado (m)】\n" +
                "Animais domesticados dentro desta distância do Arqueiro recebem a cura.\n" +
                "Valor recomendado: 8-15",

                ["Mage_Lv1_Cooldown"] =
                "【Recarga Lv1 (seg)】\n" +
                "Tempo de espera para reativar a habilidade no Mago Lv1.\n" +
                "Valor recomendado: 120 seg",

                ["Mage_Lv2_Cooldown"] =
                "【Recarga Lv2 (seg)】\n" +
                "Tempo de espera para reativar a habilidade no Mago Lv2.\n" +
                "Valor recomendado: 110 seg",

                ["Mage_Lv3_Cooldown"] =
                "【Recarga Lv3 (seg)】\n" +
                "Tempo de espera para reativar a habilidade no Mago Lv3.\n" +
                "Valor recomendado: 100 seg",

                ["Mage_Lv4_Cooldown"] =
                "【Recarga Lv4 (seg)】\n" +
                "Tempo de espera para reativar a habilidade no Mago Lv4.\n" +
                "Valor recomendado: 90 seg",

                ["Mage_Lv5_Cooldown"] =
                "【Recarga Lv5 (seg)】\n" +
                "Tempo de espera para reativar a habilidade no Mago Lv5.\n" +
                "Valor recomendado: 80 seg",

                ["Mage_Lv1_AOE_Max_Targets"] =
                "【Número Máximo de Alvos Lv1】\n" +
                "Número máximo de monstros atingidos simultaneamente no Mago Lv1. Selecionados por proximidade.\n" +
                "Valor recomendado: 6",

                ["Mage_Lv2_AOE_Max_Targets"] =
                "【Número Máximo de Alvos Lv2】\n" +
                "Número máximo de monstros atingidos simultaneamente no Mago Lv2.\n" +
                "Valor recomendado: 7",

                ["Mage_Lv3_AOE_Max_Targets"] =
                "【Número Máximo de Alvos Lv3】\n" +
                "Número máximo de monstros atingidos simultaneamente no Mago Lv3.\n" +
                "Valor recomendado: 8",

                ["Mage_Lv4_AOE_Max_Targets"] =
                "【Número Máximo de Alvos Lv4】\n" +
                "Número máximo de monstros atingidos simultaneamente no Mago Lv4.\n" +
                "Valor recomendado: 9",

                ["Mage_Lv5_AOE_Max_Targets"] =
                "【Número Máximo de Alvos Lv5】\n" +
                "Número máximo de monstros atingidos simultaneamente no Mago Lv5.\n" +
                "Valor recomendado: 10",

                ["Mage_Lv1_Elemental_Resistance"] =
                "【Resistência Elemental Lv1 (%)】\n" +
                "Resistência elemental do Mago Lv1. Reduz Fogo/Gelo/Raio/Veneno/Espírito.\n" +
                "Valor recomendado: 5%",

                ["Mage_Lv2_Elemental_Resistance"] =
                "【Resistência Elemental Lv2 (%)】\n" +
                "Resistência elemental do Mago Lv2. Inclui +1 conjuração extra (em 30s).\n" +
                "Valor recomendado: 7%",

                ["Mage_Lv3_Elemental_Resistance"] =
                "【Resistência Elemental Lv3 (%)】\n" +
                "Resistência elemental do Mago Lv3.\n" +
                "Valor recomendado: 9%",

                ["Mage_Lv4_Elemental_Resistance"] =
                "【Resistência Elemental Lv4 (%)】\n" +
                "Resistência elemental do Mago Lv4.\n" +
                "Valor recomendado: 12%",

                ["Mage_Lv5_Elemental_Resistance"] =
                "【Resistência Elemental Lv5 (%)】\n" +
                "Resistência elemental do Mago Lv5.\n" +
                "Valor recomendado: 15%",

                ["Mage_Lv1_Damage_Multiplier"] =
                "【Multiplicador de Dano AOE Lv1 (%)】\n" +
                "Multiplicador de dano em área do Mago Lv1.\n" +
                "Valor recomendado: 70%",

                ["Mage_Lv2_Damage_Multiplier"] =
                "【Multiplicador de Dano AOE Lv2 (%)】\n" +
                "Multiplicador de dano em área do Mago Lv2.\n" +
                "Valor recomendado: 90%",

                ["Mage_Lv3_Damage_Multiplier"] =
                "【Multiplicador de Dano AOE Lv3 (%)】\n" +
                "Multiplicador de dano em área do Mago Lv3.\n" +
                "Valor recomendado: 110%",

                ["Mage_Lv4_Damage_Multiplier"] =
                "【Multiplicador de Dano AOE Lv4 (%)】\n" +
                "Multiplicador de dano em área do Mago Lv4.\n" +
                "Valor recomendado: 130%",

                ["Mage_Lv5_Damage_Multiplier"] =
                "【Multiplicador de Dano AOE Lv5 (%)】\n" +
                "Multiplicador de dano em área do Mago Lv5.\n" +
                "Valor recomendado: 150%",

                ["Tanker_Explosion_Radius"] =
                "【Raio de Explosão da Provocação (m)】\n" +
                "Raio do efeito de explosão quando a habilidade de provocação do Tanque é ativada.\n" +
                "Valor recomendado: 6-12m",

                ["Tanker_BlockPower_Multiplier"] =
                "【Multiplicador de Poder de Bloqueio do Escudo】\n" +
                "Multiplicador aplicado ao poder de bloqueio do escudo com base no nível de profissão do Tanque.\n" +
                "Valor recomendado: 1.0-2.0",

                ["Rogue_Poison_Range"] =
                "【Alcance da Explosão de Veneno (m)】\n" +
                "Raio de cada VFX de explosão de veneno.\n" +
                "Valor recomendado: 8-15m",

                ["Rogue_Poison_InstantDamage"] =
                "【Dano de Veneno Instantâneo】\n" +
                "Dano de veneno imediato causado por cada ativação do VFX.\n" +
                "Valor recomendado: 8-20",

                ["Rogue_Poison_DotDamage"] =
                "【Dano de Veneno por Segundo (DoT)】\n" +
                "Dano por segundo do efeito de veneno contínuo.\n" +
                "Valor recomendado: 3-8",

                ["Rogue_Poison_DotDuration"] =
                "【Duração do Veneno Contínuo (seg)】\n" +
                "Duração do efeito de dano de veneno ao longo do tempo.\n" +
                "Valor recomendado: 8-15 seg",

                ["Rogue_Poison_VFXCount"] =
                "【Quantidade de Explosões de Veneno】\n" +
                "Número de vezes que o VFX de explosão de veneno se repete.\n" +
                "Valor recomendado: 6-10",

                ["Rogue_Poison_VFXInterval"] =
                "【Intervalo das Explosões de Veneno (seg)】\n" +
                "Tempo entre cada explosão de veneno.\n" +
                "Valor recomendado: 0.3-1.0 seg",

                ["Rogue_Lv2_Cooldown"] = "【Recarga do Golpe das Sombras Lv2 (seg)】\nRecomendado: 25-30s",

                ["Rogue_Lv3_Cooldown"] = "【Recarga do Golpe das Sombras Lv3 (seg)】\nRecomendado: 22-28s",

                ["Rogue_Lv4_Cooldown"] = "【Recarga do Golpe das Sombras Lv4 (seg)】\nRecomendado: 20-26s",

                ["Rogue_Lv5_Cooldown"] = "【Recarga do Golpe das Sombras Lv5 (seg)】\nRecomendado: 18-24s",

                ["Rogue_Lv2_AttackBonus"] = "【Buff de Ataque Lv2 (%)】\nRecomendado: 35-50%",

                ["Rogue_Lv3_AttackBonus"] = "【Buff de Ataque Lv3 (%)】\nRecomendado: 40-55%",

                ["Rogue_Lv4_AttackBonus"] = "【Buff de Ataque Lv4 (%)】\nRecomendado: 45-60%",

                ["Rogue_Lv5_AttackBonus"] = "【Buff de Ataque Lv5 (%)】\nRecomendado: 50-65%",

                ["Rogue_Lv2_BuffDuration"] = "【Duração do Buff Lv2 (seg)】\nRecomendado: 8-12s",

                ["Rogue_Lv3_BuffDuration"] = "【Duração do Buff Lv3 (seg)】\nRecomendado: 9-13s",

                ["Rogue_Lv4_BuffDuration"] = "【Duração do Buff Lv4 (seg)】\nRecomendado: 10-14s",

                ["Rogue_Lv5_BuffDuration"] = "【Duração do Buff Lv5 (seg)】\nRecomendado: 11-15s",

                ["Rogue_Lv2_PoisonBlasts"] = "【Quantidade de Explosões de Veneno Lv2】\nRecomendado: 8-12",

                ["Rogue_Lv3_PoisonBlasts"] = "【Quantidade de Explosões de Veneno Lv3】\nRecomendado: 9-13",

                ["Rogue_Lv4_PoisonBlasts"] = "【Quantidade de Explosões de Veneno Lv4】\nRecomendado: 10-14",

                ["Rogue_Lv5_PoisonBlasts"] = "【Quantidade de Explosões de Veneno Lv5】\nRecomendado: 11-15",

                ["Rogue_Lv2_PoisonInstant"] = "【Dano de Veneno Instantâneo Lv2】\nRecomendado: 10-15",

                ["Rogue_Lv3_PoisonInstant"] = "【Dano de Veneno Instantâneo Lv3】\nRecomendado: 12-18",

                ["Rogue_Lv4_PoisonInstant"] = "【Dano de Veneno Instantâneo Lv4】\nRecomendado: 14-20",

                ["Rogue_Lv5_PoisonInstant"] = "【Dano de Veneno Instantâneo Lv5】\nRecomendado: 16-25",

                ["Rogue_Lv2_PoisonDot"] = "【Dano de Veneno por Segundo Lv2】\nRecomendado: 5-8",

                ["Rogue_Lv3_PoisonDot"] = "【Dano de Veneno por Segundo Lv3】\nRecomendado: 6-9",

                ["Rogue_Lv4_PoisonDot"] = "【Dano de Veneno por Segundo Lv4】\nRecomendado: 7-10",

                ["Rogue_Lv5_PoisonDot"] = "【Dano de Veneno por Segundo Lv5】\nRecomendado: 8-12",

                ["Rogue_ShadowStrike_Charges"] = "【Cargas Base do Golpe das Sombras】\nNúmero base de cargas disponíveis.\nRecomendado: 1",

                ["Rogue_Lv5_BonusCharges"] = "【Cargas Bônus Lv5】\nCargas extras desbloqueadas no Lv5.\nRecomendado: 1",

                ["Rogue_Lv2_AttackSpeed"] = "【Bônus de Velocidade de Ataque Lv2 (%)】\nRecomendado: 10-15%",

                ["Rogue_Lv3_AttackSpeed"] = "【Bônus de Velocidade de Ataque Lv3 (%)】\nRecomendado: 12-18%",

                ["Rogue_Lv4_AttackSpeed"] = "【Bônus de Velocidade de Ataque Lv4 (%)】\nRecomendado: 14-20%",

                ["Rogue_Lv5_AttackSpeed"] = "【Bônus de Velocidade de Ataque Lv5 (%)】\nRecomendado: 16-22%",

                ["Rogue_Lv2_StaminaReduction"] = "【Redução de Resistência Lv2 (%)】\nRecomendado: 15-20%",

                ["Rogue_Lv3_StaminaReduction"] = "【Redução de Resistência Lv3 (%)】\nRecomendado: 17-22%",

                ["Rogue_Lv4_StaminaReduction"] = "【Redução de Resistência Lv4 (%)】\nRecomendado: 19-25%",

                ["Rogue_Lv5_StaminaReduction"] = "【Redução de Resistência Lv5 (%)】\nRecomendado: 22-30%",

                ["Rogue_Lv1_MoveSpeed"] = "【Bônus de Velocidade de Movimento Lv1 (%)】\nRecomendado: 3-7%",

                ["Rogue_Lv2_MoveSpeed"] = "【Bônus de Velocidade de Movimento Lv2 (%)】\nRecomendado: 5-10%",

                ["Rogue_Lv3_MoveSpeed"] = "【Bônus de Velocidade de Movimento Lv3 (%)】\nRecomendado: 7-12%",

                ["Rogue_Lv4_MoveSpeed"] = "【Bônus de Velocidade de Movimento Lv4 (%)】\nRecomendado: 10-15%",

                ["Rogue_Lv5_MoveSpeed"] = "【Bônus de Velocidade de Movimento Lv5 (%)】\nRecomendado: 12-18%",

                ["Producer_Durability_Lv1"] = "【Bônus de Durabilidade de Item Fabricado Lv1 (%)】\nBônus de durabilidade de itens fabricados no Lv1.\nPadrão: 50%",

            };
        }
    }
}

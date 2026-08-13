using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetHeavyMeleeDescriptions_PTBR()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Spear Tree (Árvore de Lança)
                // ========================================

                // === Tier 0: Mestre da Lança ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre da Lança.",

                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【Bônus de Velocidade após 2 Golpes (%)】\n" +
                "A velocidade de ataque aumenta após 2 golpes consecutivos com lança.\n" +
                "Supere os inimigos com combos rápidos.\n" +
                "Valor recomendado: 10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【Bônus de Dano após 2 Golpes (%)】\n" +
                "O dano aumenta após 2 golpes consecutivos com lança.\n" +
                "Maximiza o dano do ataque em combo.\n" +
                "Valor recomendado: 7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do efeito após 2 golpes.\n" +
                "Maior duração = combate estável.\n" +
                "Valor recomendado: 4-8 seg",

                // === Tier 1: Movimento de Ataque Rápido ===
                ["Tier1_QuickStrike_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Movimento de Ataque Rápido.",

                ["Tier1_AttackMotion"] =
                "【Seleção de Movimento de Ataque】\n" +
                "Altera o movimento do ataque normal da lança (botão esquerdo).\n" +
                "단검(Faca): Movimento de 3 estocadas / 검(Espada): Ataque normal da espada\n" +
                "Valores aceitos: 단검, 검",

                // === Tier 2: Arremesso de Lança ===
                ["Tier2_Throw_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Arremesso de Lança.",

                ["Tier2_Throw_Cooldown"] =
                "【Recarga do Arremesso (seg)】\n" +
                "Tempo de espera para arremessar a lança novamente.\n" +
                "Menos = pode arremessar com mais frequência.\n" +
                "Valor recomendado: 20-40 seg",

                ["Tier2_Throw_DamageMultiplier"] =
                "【Multiplicador de Dano do Arremesso (%)】\n" +
                "Multiplicador de dano da lança arremessada.\n" +
                "Determina o poder do ataque à distância.\n" +
                "Valor recomendado: 100-150%",

                // === Tier 3: Lança Rápida ===
                ["Tier3_Pierce_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Lança Rápida.",

                ["Tier3_Rapid_DamageBonus"] =
                "【Bônus de Ataque da Arma (fixo)】\n" +
                "Aumenta o ataque base da lança em valor fixo.\n" +
                "Vantajoso para ataques rápidos consecutivos.\n" +
                "Valor recomendado: 3-6",

                ["Tier3_QuickSpear_AttackSpeed"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Aumenta a velocidade de ataque ao usar lança ou arma de haste.\n" +
                "Valor recomendado: 15-25%",

                // === Tier 4: Golpe Evasivo ===
                ["Tier4_Evasion_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Golpe Evasivo.",

                ["Tier4_Evasion_EvasionBonus"] =
                "【Bônus de Evasão ao Atacar (%)】\n" +
                "Ao atacar com lança, a evasão aumenta por 5 segundos.\n" +
                "Melhora a sobrevivência em estilo agressivo.\n" +
                "Valor recomendado: 15-25%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【Redução de Resistência ao Atacar (%)】\n" +
                "O consumo de resistência dos ataques do Golpe Evasivo diminui.\n" +
                "Permite lutar por mais tempo.\n" +
                "Valor recomendado: 5-15%",

                // === Tier 4: Golpe Duplo ===
                ["Tier4_Dual_DamageBonus"] =
                "【Bônus de Dano do Golpe Duplo (%)】\n" +
                "Dano adicional em dois ataques consecutivos.\n" +
                "Especialização em dano de combo.\n" +
                "Valor recomendado: 18-30%",

                ["Tier4_Dual_Duration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do buff do golpe duplo.\n" +
                "Maior duração garante dano estável.\n" +
                "Valor recomendado: 8-15 seg",

                // === Tier 5: Lança Perfurante (Ativa G) ===
                ["Tier5_Penetrate_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Lança Perfurante.",

                ["Tier5_Penetrate_BuffDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do buff da lança perfurante.\n" +
                "Determina a duração do efeito da habilidade.\n" +
                "Valor recomendado: 25-40 seg",

                ["Tier5_Penetrate_LightningDamage"] =
                "【Multiplicador de Dano de Raio (%)】\n" +
                "Multiplicador de dano de raio em ataques em série.\n" +
                "Causa dano adicional poderoso.\n" +
                "Valor recomendado: 200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【Golpes Necessários para Raio】\n" +
                "Quantidade de golpes consecutivos para ativar o raio.\n" +
                "Menos = aciona com mais frequência.\n" +
                "Valor recomendado: 3-5 golpes",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【Recarga da Habilidade G (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade G.\n" +
                "Menos = pode usar com mais frequência.\n" +
                "Valor recomendado: 50-80 seg",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【Custo de Resistência da Habilidade G】\n" +
                "Resistência ao usar a habilidade G.\n" +
                "Gerenciar a resistência é importante.\n" +
                "Valor recomendado: 20-35",

                // === Tier 5: Lança em Série (Ativa H) ===
                ["Tier5_Combo_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Lança em Série.",

                ["Tier5_Combo_HKey_Cooldown"] =
                "【Recarga da Habilidade H (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade H.\n" +
                "Menos = pode usar com mais frequência.\n" +
                "Valor recomendado: 20-35 seg",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【Multiplicador de Dano da Habilidade H (%)】\n" +
                "Multiplicador de dano do ataque da lança em série (H).\n" +
                "Habilidade poderosa de dano único.\n" +
                "Valor recomendado: 250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【Custo de Resistência da Habilidade H】\n" +
                "Resistência ao usar a habilidade H.\n" +
                "Necessário gerenciar a resistência.\n" +
                "Valor recomendado: 15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【Distância de Recuo da Habilidade H (m)】\n" +
                "Distância em que o inimigo é recuado ao acertar a habilidade H.\n" +
                "Útil para ajustar posição no combate.\n" +
                "Valor recomendado: 2-5m",

                ["Tier5_Combo_ActiveRange"] =
                "【Raio do Efeito Ativo (m)】\n" +
                "Raio em que o buff da lança em série é ativado.\n" +
                "Mais = aciona em mais situações.\n" +
                "Valor recomendado: 2-5m",

                ["Tier5_Combo_BuffDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do buff da lança em série.\n" +
                "Maior duração = reforço estável de arremessos.\n" +
                "Valor recomendado: 25-40 seg",

                ["Tier5_Combo_MaxUses"] =
                "【Máx. de Arremessos Reforçados】\n" +
                "Número máximo de arremessos reforçados durante o buff.\n" +
                "Mais = o reforço dura mais.\n" +
                "Valor recomendado: 2-5 vezes",

                // ========================================
                // Mace Tree (Árvore de Maça)
                // ========================================

                // === Tier 0: Mestre da Maça ===
                ["Tier0_MaceExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre da Maça.",

                ["Tier0_MaceExpert_DamageBonus"] =
                "【Bônus de Dano de Maça (%)】\n" +
                "Aumenta o ataque base de armas contundentes.\n" +
                "Aplicado a todos os tipos de maças.\n" +
                "Valor recomendado: 5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【Chance de Atordoamento (%)】\n" +
                "Chance de atordoar o inimigo ao golpear com maça.\n" +
                "O inimigo atordoado fica incapacitado.\n" +
                "Valor recomendado: 15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【Duração do Atordoamento (seg)】\n" +
                "Tempo de ação do efeito de atordoamento.\n" +
                "Maior duração = mais tempo para causar dano.\n" +
                "Valor recomendado: 0.3-1 seg",

                // === Tier 1: Reforço de Maça ===
                ["Tier1_MaceExpert_DamageBonus"] =
                "【Bônus de Ataque de Maça (%)】\n" +
                "Bônus adicional de ataque de arma contundente.\n" +
                "Valor recomendado: 8-15%",

                ["Tier1_MaceExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Reforço de Maça.",

                // === Tier 2: Reforço de Atordoamento ===
                ["Tier2_StunBoost_StunChanceBonus"] =
                "【Bônus de Chance de Atordoamento (%)】\n" +
                "Aumenta adicionalmente a chance de atordoamento.\n" +
                "Acumula com a habilidade Mestre da Maça.\n" +
                "Valor recomendado: 10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【Bônus de Duração de Atordoamento (seg)】\n" +
                "Aumenta adicionalmente o tempo de atordoamento.\n" +
                "Mais tempo para causar dano.\n" +
                "Valor recomendado: 0.3-0.8 seg",

                ["Tier2_StunBoost_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Reforço de Atordoamento.",

                // === Tier 3: Escudo ===
                ["Tier3_Guard_ArmorBonus"] =
                "【Bônus de Armadura (fixo)】\n" +
                "Aumenta a armadura base em valor fixo.\n" +
                "Útil para build de tanque.\n" +
                "Valor recomendado: 2-5",

                ["Tier3_Guard_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Escudo.",

                // === Tier 3: Golpe Pesado ===
                ["Tier3_HeavyStrike_DamageBonus"] =
                "【Bônus de Dano de Impacto (fixo)】\n" +
                "Aumenta o dano de impacto da maça em valor fixo.\n" +
                "Acumula com o bônus percentual.\n" +
                "Valor recomendado: 2-5",

                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Golpe Pesado.",

                // === Tier 4: Concussão ===
                ["Tier4_Push_KnockbackChance"] =
                "【Chance de Concussão (%)】\n" +
                "Chance ao acertar com uma maça de reduzir a velocidade de movimento e ataque do alvo em 30% por 1,5s.\n" +
                "Útil para controle de combate e vantagem de dano.\n" +
                "Valor recomendado: 30-40%",

                ["Tier4_Push_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Concussão.",

                // === Tier 5: Tanque ===
                ["Tier5_Tank_HealthBonus"] =
                "【Bônus de Vida (%)】\n" +
                "Aumenta a vida máxima.\n" +
                "Essencial para fortalecer a sobrevivência.\n" +
                "Valor recomendado: 20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【Redução de Dano Recebido (%)】\n" +
                "Reduz todo o dano recebido.\n" +
                "Combinado com armadura — ideal para o papel de tanque.\n" +
                "Valor recomendado: 8-15%",

                ["Tier5_Tank_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tanque.",

                // === Tier 5: Dano (DPS) ===
                ["Tier5_DPS_DamageBonus"] =
                "【Bônus de Ataque (%)】\n" +
                "Aumenta adicionalmente o ataque de arma contundente.\n" +
                "Útil para build de DPS.\n" +
                "Valor recomendado: 15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Aumenta a velocidade de ataque com maça.\n" +
                "Compensa a lentidão de armas pesadas.\n" +
                "Valor recomendado: 8-15%",

                ["Tier5_DPS_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear DPS.",

                // === Tier 6: Grão-Mestre ===
                ["Tier6_Grandmaster_ArmorBonus"] =
                "【Bônus de Armadura (%)】\n" +
                "Bônus percentual de armadura.\n" +
                "Boa sinergia com armaduras de alto nível.\n" +
                "Valor recomendado: 15-25%",

                ["Tier6_Grandmaster_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Grão-Mestre.",

                // === Tier 7: Martelo da Fúria (Ativa H) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【Multiplicador de Dano Golpes 1-4 (%)】\n" +
                "Multiplicador de dano dos golpes 1-4 da habilidade 'Martelo da Fúria' (H).\n" +
                "Aplicado ao ataque atual.\n" +
                "Valor recomendado: 70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【Multiplicador de Dano do Golpe Final (%)】\n" +
                "Multiplicador de dano do golpe final da habilidade 'Martelo da Fúria' (H).\n" +
                "O golpe mais poderoso é o final.\n" +
                "Valor recomendado: 130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao usar a habilidade H.\n" +
                "Gerenciar a resistência é importante.\n" +
                "Valor recomendado: 35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Menos = pode usar com mais frequência.\n" +
                "Valor recomendado: 25-35 seg",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【Raio AOE (metros)】\n" +
                "Raio de dano em área da habilidade.\n" +
                "Maior = atinge mais inimigos.\n" +
                "Valor recomendado: 4-7m",

                ["Tier7_FuryHammer_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Martelo da Fúria.",

                // === Tier 6-5: Investida com Escudo (movido da árvore de maça) ===
                ["Tier6_GuardianHeart_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a Investida com Escudo.\n" +
                "Valor recomendado: 30-40 seg",

                ["Tier6_GuardianHeart_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao usar Investida com Escudo.\n" +
                "Valor recomendado: 15-25",

                ["Tier6_ShieldCharge_DamagePercent"] =
                "【Dano do Bloqueio do Escudo (%)】\n" +
                "Dano causado na colisão como porcentagem do poder de bloqueio.\n" +
                "Valor recomendado: 60-80%",

                ["Tier6_ShieldCharge_MultiHitLevelBonus"] =
                "【Bônus de Nível de Múltiplos Golpes (%)】\n" +
                "Aumento do dano de múltiplos golpes por nível da habilidade.\n" +
                "Valor recomendado: 5-15%",

                ["Tier6_GuardianHeart_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Investida com Escudo.",

                ["Tier7_ShockwaveSlam_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar o Golpe de Onda de Choque.\n" +
                "Valor recomendado: 30-50 seg",

                ["Tier7_ShockwaveSlam_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao usar Golpe de Onda de Choque.\n" +
                "Valor recomendado: 15-25",

                ["Tier7_ShockwaveSlam_DamagePercent"] =
                "【Proporção de Dano da Arma (%)】\n" +
                "Dano causado ao acertar como porcentagem do dano da arma.\n" +
                "Valor recomendado: 200-260%",

                ["Tier7_ShockwaveSlam_LevelBonus"] =
                "【Bônus por Nível (%)】\n" +
                "Bônus de dano por nível do Golpe de Onda de Choque.\n" +
                "Valor recomendado: 10-30%",

                ["Tier7_ShockwaveSlam_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Golpe de Onda de Choque.",

                // ========================================
                // Polearm Tree (Árvore de Haste)
                // ========================================

                // === Tier 0: Mestre da Haste ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【Bônus de Alcance de Ataque (%)】\n" +
                "Aumenta o alcance de ataque de armas de haste.\n" +
                "O longo alcance permite atacar de distância segura.\n" +
                "Valor recomendado: 10-20%",

                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre da Haste.",

                // === Tier 1: Roda Giratória ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【Bônus de Dano de Rotação (%)】\n" +
                "Dano adicional no ataque giratório.\n" +
                "Útil contra vários inimigos.\n" +
                "Valor recomendado: 50-80%",

                ["Tier1_SpinWheel_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Roda Giratória.",

                // === Tier 2-1: Reforço de Haste ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] =
                "【Bônus de Ataque da Arma (fixo)】\n" +
                "Aumenta o ataque base da haste em valor fixo.\n" +
                "Aplicado a todos os ataques com haste.\n" +
                "Valor recomendado: 4-7",

                ["Tier2-1_PolearmBoost_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Reforço de Haste.",

                // === Tier 2-2: Golpe do Herói ===
                ["Tier2-2_HeroStrike_KnockbackChance"] =
                "【Chance de Desequilíbrio (%)】\n" +
                "Chance de desequilibrar o inimigo ao atacar.\n" +
                "Útil para controlar o campo de batalha.\n" +
                "Valor recomendado: 20-35%",

                ["Tier2-2_HeroStrike_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Golpe do Herói.",

                // === Tier 3: Golpe em Área ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【Bônus de Dano do Golpe Duplo (%)】\n" +
                "Dano adicional em dois ataques consecutivos.\n" +
                "Especialização em combos em área.\n" +
                "Valor recomendado: 20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【Duração do Buff de Golpe Duplo (seg)】\n" +
                "Tempo de ação do buff do golpe duplo.\n" +
                "Maior duração = combo estável.\n" +
                "Valor recomendado: 4-8 seg",

                ["Tier3_AreaCombo_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Golpe em Área.",

                // === Tier 4-1: Golpe no Chão ===
                ["Tier4-1_GroundWheel_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Golpe no Chão.",

                // === Tier 4-2: Meia-Lua ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] =
                "【Bônus de Alcance de Ataque (%)】\n" +
                "Aumenta o alcance do ataque meia-lua.\n" +
                "Permite atacar em raio mais amplo.\n" +
                "Valor recomendado: 12-20%",

                ["Tier4-2_MoonSlash_StaminaReduction"] =
                "【Redução de Resistência (%)】\n" +
                "Reduz o consumo de resistência ao usar meia-lua.\n" +
                "Permite lutar por mais tempo.\n" +
                "Valor recomendado: 12-20%",

                ["Tier4-2_MoonSlash_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Meia-Lua.",

                // === Tier 4-3: Supressão ===
                ["Tier4-3_Suppress_DamageBonus"] =
                "【Bônus de Dano de Supressão (%)】\n" +
                "Dano adicional no ataque supressivo.\n" +
                "Suprima os inimigos e assuma a iniciativa no combate.\n" +
                "Valor recomendado: 25-40%",

                ["Tier4-3_Suppress_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Supressão.",

                // === Tier 5: Investida Perfurante (Ativa G) ===
                ["Tier5_PierceCharge_DashDistance"] =
                "【Distância da Investida (m)】\n" +
                "Distância da investida na habilidade perfurante.\n" +
                "Avance por entre as fileiras inimigas por grande distância.\n" +
                "Valor recomendado: 8-12m",

                ["Tier5_PierceCharge_FirstHitDamageBonus"] =
                "【Bônus de Dano do Primeiro Golpe (%)】\n" +
                "Multiplicador de dano do primeiro golpe durante a investida.\n" +
                "Um primeiro golpe poderoso suprime os inimigos.\n" +
                "Valor recomendado: 180-250%",

                ["Tier5_PierceCharge_AoeDamageBonus"] =
                "【Bônus de Dano AOE de Recuo (%)】\n" +
                "Multiplicador de dano do recuo em área após a investida.\n" +
                "Repele e causa dano aos inimigos ao redor.\n" +
                "Valor recomendado: 130-180%",

                ["Tier5_PierceCharge_AoeAngle"] =
                "【Ângulo AOE (graus)】\n" +
                "Ângulo do efeito de recuo em área.\n" +
                "280° = área traseira/lateral, excluindo 80° frontais.\n" +
                "Valor recomendado: 250-300°",

                ["Tier5_PierceCharge_AoeRadius"] =
                "【Raio AOE (m)】\n" +
                "Raio do efeito de recuo em área.\n" +
                "Maior = mais inimigos são repelidos.\n" +
                "Valor recomendado: 4-7m",

                ["Tier5_PierceCharge_KnockbackDistance"] =
                "【Distância de Recuo (m)】\n" +
                "Distância de recuo dos inimigos.\n" +
                "Útil para controlar o campo de batalha.\n" +
                "Valor recomendado: 6-10m",

                ["Tier5_PierceCharge_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao usar a habilidade G.\n" +
                "Gerenciar a resistência é importante.\n" +
                "Valor recomendado: 18-25",

                ["Tier5_PierceCharge_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade G.\n" +
                "Menos = pode usar com mais frequência.\n" +
                "Valor recomendado: 25-40 seg",

                ["Tier5_PierceCharge_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Investida Perfurante.",

                // === Polearm Tree: Redemoinho (7 keys) ===
                ["Tier6_Whirlwind_DamagePercent"] =
                "【Taxa de Dano do Redemoinho (%)】\n" +
                "Multiplicador de dano por ciclo de salto-ataque.\n" +
                "Recomendado: 15-35%",

                ["Tier6_Whirlwind_StaminaPerSec"] =
                "【Custo de Stamina/Ciclo do Redemoinho】\n" +
                "Stamina consumida por ciclo de salto-ataque.\n" +
                "Recomendado: 3-6",

                ["Tier6_Whirlwind_MoveSpeed"] =
                "【Velocidade do Redemoinho (m/s)】\n" +
                "Base de distância de movimento por ciclo.\n" +
                "Recomendado: 3-6",

                ["Tier6_Whirlwind_AttackInterval"] =
                "【Intervalo de Ataque do Redemoinho (seg)】\n" +
                "Tempo de espera entre movimentos de ataque.\n" +
                "Recomendado: 0.2-0.5",

                ["Tier6_Whirlwind_VfxInterval"] =
                "【Intervalo VFX do Redemoinho (seg)】\n" +
                "Intervalo de reprodução do efeito visual.\n" +
                "Recomendado: 1-3",

                ["Tier6_Whirlwind_Cooldown"] =
                "【Recarga do Redemoinho (seg)】\n" +
                "Tempo de espera antes de reutilizar após o skill.\n" +
                "Recomendado: 10-30",

                ["Tier6_Whirlwind_RequiredPoints"] =
                "【Pontos Necessários】\n" +
                "Pontos de habilidade para desbloquear o Redemoinho.\n" +
                "Recomendado: 3",

                ["Tier0_SpearExpert_ProcChance"] =
                "【Chance de Ativação do Mestre da Lança (%)】\n" +
                "Chance de ativar o proc de Golpe de Raio ao atacar.\n" +
                "Ao ativar, o próximo ataque é executado em alta velocidade.\n" +
                "Valor recomendado: 20-35%",

                ["Tier0_SpearExpert_SpeedBoost"] =
                "【Aumento de Velocidade do Mestre da Lança (%)】\n" +
                "Bônus de velocidade de ataque adicionado quando o proc é ativado.\n" +
                "Base 100% + este valor = multiplicador total de velocidade de ataque.\n" +
                "Valor recomendado: 80-120%",

                ["Tier5_Penetrate_BaseDamage"] =
                "【Dano Único Base Lv1 (%)】\n" +
                "Dano único base causado a inimigos no caminho da investida.\n" +
                "Calculado como proporção do dano de perfuração da lança.\n" +
                "Valor recomendado: 80-120%",

                ["Tier5_Penetrate_LevelDamageBonus"] =
                "【Bônus de Dano Único por Nível (%)】\n" +
                "Dano único adicional por nível da habilidade.\n" +
                "Valor recomendado: 3-8%",

                ["Tier5_Penetrate_BaseAreaDamage"] =
                "【Dano em Área Base Lv1 (%)】\n" +
                "Dano base causado a inimigos dentro de um raio de 5m do caminho da investida.\n" +
                "Valor recomendado: 60-100%",

                ["Tier5_Penetrate_AreaLevelBonus"] =
                "【Bônus de Dano em Área por Nível (%)】\n" +
                "Dano em área adicional por nível da habilidade.\n" +
                "Valor recomendado: 3-8%",

                ["Tier5_Combo_LevelBonus"] =
                "【Bônus por Nível (%)】\n" +
                "Bônus de dano por nível da habilidade Lança em Série.\n" +
                "Valor recomendado: 5-15%",

                ["Tier3_SpinStrike_DamageBonus"] =
                "【Bônus de Dano do Ataque Secundário (%)】\n" +
                "Aumenta o dano no ataque secundário.\n" +
                "Baseado em porcentagem; maior dano base gera maior efeito.\n" +
                "Valor recomendado: 15-25%",

                ["Tier3_SpinStrike_Range"] =
                "【Raio AOE (metros)】\n" +
                "Raio no qual os inimigos próximos recebem dano no ataque secundário.\n" +
                "Valor recomendado: 5-10m",

                ["Tier3_SpinStrike_KnockbackForce"] =
                "【Distância de Recuo do Golpe Giratório (metros)】\n" +
                "Distância que os inimigos são repelidos no ataque secundário. Aplica-se a maças de uma e duas mãos.\n" +
                "Valor recomendado: 2-5m",

                ["Tier6_Sokgong_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Aumenta a velocidade de ataque da maça.\n" +
                "Compensa a lentidão do ataque de maça.\n" +
                "Valor recomendado: 8-15%",

                ["Tier7_FuryHammer_NormalHitLevelBonus"] =
                "【Bônus de Nível do Golpe Normal (%)】\n" +
                "Bônus de dano por nível para os golpes normais do Martelo da Fúria.\n" +
                "Valor recomendado: 5-15%",

                ["Tier7_FuryHammer_FinalHitLevelBonus"] =
                "【Bônus de Nível do Golpe Final (%)】\n" +
                "Bônus de dano por nível para o golpe final (explosão) do Martelo da Fúria.\n" +
                "Valor recomendado: 10-25%",

                ["Tier6_ShieldCharge_MultiHitDamagePercent"] =
                "【Proporção de Dano Multi-Golpe (%)】\n" +
                "Proporção base (nível 1) do poder de bloqueio do escudo, aplicada ao golpe em área a cada 0.08s\n" +
                "durante a investida e ao golpe final de múltiplos golpes (4 vezes, intervalo de 0.25s) nos inimigos reunidos.\n" +
                "Valor recomendado: 20-40%",

                ["Tier6_ShieldCharge_LevelBonus"] =
                "【Bônus por Nível (%)】\n" +
                "Bônus de dano por nível da habilidade Investida com Escudo.\n" +
                "Valor recomendado: 5-15%",

                ["Tier4-1_StormSlash_ExplosionBonus"] =
                "【Bônus de Dano de Raio】\n" +
                "Dano de raio adicionado ao usar o ataque do botão da roda do mouse dentro de 4s de um ataque primário.\n" +
                "Valor recomendado: 10-20",

                ["Tier5_PierceCharge_LevelBonus"] =
                "【Bônus de Dano por Nível (%)】\n" +
                "Bônus de dano adicional por nível da habilidade Investida Perfurante.\n" +
                "Valor recomendado: 20-40%",

                ["Tier6_Whirlwind_LevelBonus"] =
                "【Bônus por Nível (%)】\n" +
                "Bônus de dano por nível da habilidade Redemoinho.\n" +
                "Valor recomendado: 5-15%",

                ["Tier6_Whirlwind_DamageReductionPercent"] =
                "【Redução de Dano (%)】\n" +
                "Reduz o dano recebido enquanto o Redemoinho está ativo. (valor base Lv1)\n" +
                "Valor recomendado: 20-40%",

                ["Tier6_Whirlwind_DamageReductionLevelBonus"] =
                "【Bônus de Nível de Redução de Dano (%)】\n" +
                "Redução de dano adicional por nível da habilidade Redemoinho.\n" +
                "Valor recomendado: 5-10%",


            };
        }
    }
}

using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetSwordKnifeDescriptions_PTBR()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Sword Tree (Árvore de Espada)
                // ========================================

                // === Tier 0: Mestre da Espada ===
                ["Sword_Expert_DamageIncrease"] =
                "【Aumento de Dano de Espada (%)】\n" +
                "Aumenta o ataque base das espadas.\n" +
                "Aplicado a todos os tipos de espadas.\n" +
                "Valor recomendado: 10-20%",

                ["Tier0_SwordExpert_DamageBonus"] =
                "【Aumento de Dano de Espada (%)】\n" +
                "Aumenta o ataque base das espadas.\n" +
                "Aplicado a todos os tipos de espadas.\n" +
                "Valor recomendado: 10-20%",

                ["Tier0_SwordExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre da Espada.",

                // === Tier 1: Corte Rápido ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Aumenta a velocidade de ataque com espada.\n" +
                "Permite atacar rapidamente várias vezes seguidas.\n" +
                "Valor recomendado: 10-20%",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Aumenta a velocidade de ataque com espada.\n" +
                "Permite atacar rapidamente várias vezes seguidas.\n" +
                "Valor recomendado: 10-20%",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Corte Rápido.",

                // === Tier 1: Postura de Contra-ataque ===
                ["Tier1_CounterStance_Duration"] =
                "【Duração (seg)】\n" +
                "Tempo de ação da postura de contra-ataque.\n" +
                "Durante esse tempo a defesa é aumentada.\n" +
                "Valor recomendado: 3-6 seg",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【Bônus de Defesa (%)】\n" +
                "Aumento de defesa na postura de contra-ataque.\n" +
                "Aguente e espere o momento do contra-ataque.\n" +
                "Valor recomendado: 20-40%",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Postura de Contra-ataque.",

                // === Tier 2: Série de Golpes ===
                ["Sword_ComboSlash_Bonus"] =
                "【Bônus de Ataque em Série (%)】\n" +
                "Causa dano adicional em ataque em série.\n" +
                "Manter o combo garante alto DPS.\n" +
                "Valor recomendado: 15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do bônus de ataque em série.\n" +
                "Atacar durante esse tempo prolonga o buff.\n" +
                "Valor recomendado: 3-5 seg",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【Bônus de Ataque em Série (%)】\n" +
                "Causa dano adicional em ataque em série.\n" +
                "Manter o combo garante alto DPS.\n" +
                "Valor recomendado: 15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do bônus de ataque em série.\n" +
                "Atacar durante esse tempo prolonga o buff.\n" +
                "Valor recomendado: 3-5 seg",

                ["Tier2_ComboSlash_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Série de Golpes.",

                // === Tier 3: Ricochete da Lâmina / Contra-ataque ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【Bônus de Ataque (fixo)】\n" +
                "Aumenta o ataque do ricochete da lâmina em valor fixo.\n" +
                "Após aparar, um contra-ataque poderoso.\n" +
                "Valor recomendado: 20-40",

                ["Tier3_Riposte_DamageBonus"] =
                "【Bônus de Ataque (fixo)】\n" +
                "Aumenta o ataque do ricochete da lâmina em valor fixo.\n" +
                "Após aparar, um contra-ataque poderoso.\n" +
                "Valor recomendado: 5-15",

                ["Tier3_Riposte_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Contra-ataque.",

                // === Tier 4: Ataque e Defesa ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【Bônus de Ataque (%)】\n" +
                "Fortalece ataque e defesa simultaneamente.\n" +
                "Útil para estilo de combate equilibrado.\n" +
                "Valor recomendado: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【Bônus de Defesa (fixo)】\n" +
                "Bônus de defesa da postura 'Ataque e Defesa'.\n" +
                "Defesa sólida mesmo ao atacar.\n" +
                "Valor recomendado: 15-30",

                ["Tier4_AllInOne_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Ataque e Defesa.",

                // === Tier 4: Duelo Verdadeiro ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Bônus de velocidade de ataque para combate um a um.\n" +
                "Golpes rápidos superam o inimigo.\n" +
                "Valor recomendado: 15-30%",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Bônus de velocidade de ataque para combate um a um.\n" +
                "Golpes rápidos superam o inimigo.\n" +
                "Valor recomendado: 15-30%",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Duelo Verdadeiro.",

                // === Tier 5: Investida com Aparada (Ativa H) ===
                ["Sword_ParryCharge_BuffDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do buff após uma aparada bem-sucedida.\n" +
                "Durante esse tempo o ataque aprimorado está disponível.\n" +
                "Valor recomendado: 5-10 seg",

                ["Sword_ParryCharge_DamageBonus"] =
                "【Bônus de Dano da Investida (%)】\n" +
                "Aumento de dano da investida após aparar.\n" +
                "Timing perfeito = contra-ataque poderoso.\n" +
                "Valor recomendado: 50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【Distância de Empurrão (m)】\n" +
                "Quantos metros o inimigo é empurrado na investida.\n" +
                "Útil para ajustar distância e controlar o campo de batalha.\n" +
                "Valor recomendado: 3-7m",

                ["Sword_ParryCharge_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao ativar o buff (tecla H).\n" +
                "Gerenciar a resistência é importante.\n" +
                "Valor recomendado: 20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Menos = pode usar com mais frequência.\n" +
                "Valor recomendado: 10-20 seg",

                ["Tier5_ParryRush_BuffDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do buff após aparada bem-sucedida.\n" +
                "Valor recomendado: 20-40 seg",

                ["Tier5_ParryRush_DamageBonus"] =
                "【Bônus de Dano da Investida (%)】\n" +
                "Aumento de dano da investida após aparar.\n" +
                "Valor recomendado: 50-100%",

                ["Tier5_ParryRush_PushDistance"] =
                "【Distância de Empurrão (m)】\n" +
                "Quantos metros o inimigo é empurrado na investida.\n" +
                "Valor recomendado: 3-7m",

                ["Tier5_ParryRush_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao ativar a habilidade.\n" +
                "Valor recomendado: 10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 30-60 seg",

                ["Tier5_ParryRush_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Investida com Aparada.",

                // === Tier 6: Série de Investidas (Ativa G) ===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【Dano do 1º Golpe (%)】\n" +
                "Dano do primeiro golpe da série de investidas.\n" +
                "Multiplicador em relação ao ataque base.\n" +
                "Valor recomendado: 80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【Dano do 2º Golpe (%)】\n" +
                "Dano do segundo golpe da série de investidas.\n" +
                "O combo aumenta e o dano cresce.\n" +
                "Valor recomendado: 100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【Dano do 3º Golpe (%)】\n" +
                "Dano do golpe final da série de investidas.\n" +
                "O golpe final mais poderoso.\n" +
                "Valor recomendado: 150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【Distância da Investida (m)】\n" +
                "Distância da investida no início da habilidade.\n" +
                "Aproxima-se rapidamente do inimigo.\n" +
                "Valor recomendado: 5-10m",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【Distância de Movimento Lateral (m)】\n" +
                "Distância de movimento para esquerda/direita durante os ataques.\n" +
                "Esquive e ataque ao mesmo tempo.\n" +
                "Valor recomendado: 2-5m",

                ["Sword_RushSlash_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao usar a habilidade (tecla G).\n" +
                "Habilidade poderosa exige muita resistência.\n" +
                "Valor recomendado: 40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Menos = pode usar com mais frequência.\n" +
                "Valor recomendado: 15-30 seg",

                ["Sword_RushSlash_MovementSpeed"] =
                "【Velocidade de Movimento (m/s)】\n" +
                "Velocidade de movimento durante a investida.\n" +
                "Mais rápido = combate mais dinâmico.\n" +
                "Valor recomendado: 8-15 m/s",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Bônus de velocidade de ataque durante a habilidade.\n" +
                "Velocidades de outras árvores são ignoradas, apenas este valor é aplicado.\n" +
                "Valor recomendado: 20-40%",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【Dano do 1º Golpe (%)】\n" +
                "Dano do primeiro golpe da série de investidas.\n" +
                "Valor recomendado: 60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【Dano do 2º Golpe (%)】\n" +
                "Dano do segundo golpe da série de investidas.\n" +
                "Valor recomendado: 70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【Dano do 3º Golpe (%)】\n" +
                "Dano do golpe final da série de investidas.\n" +
                "Valor recomendado: 80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【Distância da Investida Inicial (m)】\n" +
                "Distância da investida no início da habilidade.\n" +
                "Valor recomendado: 3-8m",

                ["Tier6_RushSlash_SideDistance"] =
                "【Distância de Movimento Lateral (m)】\n" +
                "Distância de movimento para esquerda/direita durante os ataques.\n" +
                "Valor recomendado: 2-5m",

                ["Tier6_RushSlash_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao usar a habilidade (G).\n" +
                "Valor recomendado: 20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 15-30 seg",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【Velocidade de Movimento (m/s)】\n" +
                "Velocidade de movimento durante a investida.\n" +
                "Valor recomendado: 10-25 m/s",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【Bônus de Velocidade de Ataque (%)】\n" +
                "Bônus de velocidade de ataque durante a habilidade.\n" +
                "Valor recomendado: 150-250%",

                ["Tier6_RushSlash_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Série de Investidas.",

                // ========================================
                // Knife Tree (Árvore de Adaga)
                // ========================================

                // === Tier 0: Mestre da Adaga ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【Bônus de Dano pelas Costas (%)】\n" +
                "Dano adicional ao atacar pelas costas.\n" +
                "Habilidade base do assassino.\n" +
                "Valor recomendado: 30-50%",

                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre da Adaga.",

                // === Tier 1: Maestria de Evasão ===
                ["Tier1_Evasion_Chance"] =
                "【Chance de Evasão (%)】\n" +
                "Chance de esquivar do ataque inimigo.\n" +
                "Mais alto = menos acertos recebidos.\n" +
                "Valor recomendado: 15-25%",

                ["Tier1_Evasion_Duration"] =
                "【Duração de Invencibilidade (seg)】\n" +
                "Tempo de invencibilidade após evasão bem-sucedida.\n" +
                "Valor recomendado: 2-4 seg",

                ["Tier1_Evasion_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Maestria de Evasão.",

                // === Tier 2: Movimento Rápido ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【Bônus de Velocidade de Movimento (%)】\n" +
                "Aumenta a velocidade base de movimento.\n" +
                "Alta mobilidade para confundir os inimigos.\n" +
                "Valor recomendado: 10-20%",

                ["Tier2_FastMove_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Movimento Rápido.",

                // === Tier 3: Maestria de Combate ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【Bônus de Dano (fixo)】\n" +
                "Adiciona dano fixo a cada ataque.\n" +
                "Valor recomendado: 1-4",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do buff de maestria de combate.\n" +
                "Valor recomendado: 8-12 seg",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Maestria de Combate.",

                // === Tier 4: Ataque e Evasão ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【Bônus de Chance de Evasão (%)】\n" +
                "Aumenta a chance de evasão ao atacar simultaneamente.\n" +
                "Defesa agressiva.\n" +
                "Valor recomendado: 20-30%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do efeito de evasão aumentada.\n" +
                "Valor recomendado: 8-12 seg",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para o buff reativar.\n" +
                "Valor recomendado: 25-35 seg",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Ataque e Evasão.",

                // === Tier 5: Dano Crítico ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【Bônus de Dano (%)】\n" +
                "Aumenta o dano de acertos críticos.\n" +
                "Boa sinergia com alta chance crítica da adaga.\n" +
                "Valor recomendado: 20-35%",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Dano Crítico.",

                // === Tier 6: Assassino ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【Bônus de Dano Crítico (%)】\n" +
                "Aumenta ainda mais o dano de acertos críticos.\n" +
                "Valor recomendado: 20-30%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【Bônus de Chance Crítica (%)】\n" +
                "Aumenta a probabilidade de acerto crítico.\n" +
                "Valor recomendado: 10-18%",

                ["Tier6_Assassin_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Assassino.",

                // === Tier 7: Arte do Assassínio ===
                ["Tier7_Assassination_StaggerChance"] =
                "【Chance de Atordoamento (%)】\n" +
                "Chance de desestabilizar o inimigo em ataque em série.\n" +
                "Interrompe o ataque do inimigo.\n" +
                "Valor recomendado: 30-45%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【Golpes de Combo Necessários】\n" +
                "Número de golpes consecutivos para ativar o atordoamento.\n" +
                "Valor recomendado: 2-4 golpes",

                ["Tier7_Assassination_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Arte do Assassínio.",

                // === Tier 8: Coração do Assassino (Ativa G) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【Multiplicador de Dano Crítico】\n" +
                "Ativa G — multiplicador de dano crítico do Coração do Assassino.\n" +
                "Teleporte para as costas do inimigo e execute uma série letal.\n" +
                "Valor recomendado: 1.2-1.5x",

                ["Tier8_AssassinHeart_Duration"] =
                "【Duração do Buff (seg)】\n" +
                "Tempo de ação do buff do Coração do Assassino.\n" +
                "Valor recomendado: 5-10 seg",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【Custo de Resistência】\n" +
                "Resistência ao usar a habilidade.\n" +
                "Valor recomendado: 15-25",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 35-50 seg",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【Alcance do Teleporte (m)】\n" +
                "Alcance máximo de busca do inimigo.\n" +
                "Teleporte para as costas do inimigo dentro deste raio.\n" +
                "Valor recomendado: 6-10m",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【Distância de Posicionamento pelas Costas (m)】\n" +
                "Distância de posicionamento atrás do inimigo.\n" +
                "Distância para o golpe pelas costas.\n" +
                "Valor recomendado: 0.8-1.5m",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【Duração do Atordoamento (seg)】\n" +
                "Tempo em que o inimigo fica atordoado.\n" +
                "Valor recomendado: 0.5-2 seg",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【Número de Golpes em Série】\n" +
                "Quantidade de golpes automáticos após o teleporte.\n" +
                "Valor recomendado: 2-4 golpes",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【Intervalo entre Golpes (seg)】\n" +
                "Tempo entre os golpes da série.\n" +
                "Menos = golpes instantâneos.\n" +
                "Valor recomendado: 0.2-0.5 seg",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Coração do Assassino.",
            };
        }
    }
}

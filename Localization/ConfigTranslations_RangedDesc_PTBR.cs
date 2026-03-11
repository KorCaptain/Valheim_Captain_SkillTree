using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetRangedDescriptions_PTBR()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Staff Tree (Árvore de Cajado)
                // ========================================

                // === Tier 0: Mestre do Cajado ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【Bônus de Dano Elemental (%)】\n" +
                "Aumenta o dano elemental do cajado (fogo, gelo, raio).\n" +
                "Base dos ataques mágicos.\n" +
                "Valor recomendado: 3-7%",

                ["Tier0_StaffExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre do Cajado.",

                // === Tier 1: Concentração Mental & Fluxo Mágico ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【Redução de Custo de Eitr (%)】\n" +
                "Concentração Mental reduz o custo de Eitr dos feitiços.\n" +
                "Permite usar magia com mais frequência.\n" +
                "Valor recomendado: 12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【Bônus de Eitr Máximo (fixo)】\n" +
                "Fluxo Mágico aumenta o Eitr máximo.\n" +
                "Valor recomendado: 25-35",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Concentração Mental.",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Fluxo Mágico.",

                // === Tier 2: Amplificação Mágica ===
                ["Tier2_MagicAmplify_Chance"] =
                "【Chance de Amplificação Mágica (%)】\n" +
                "Chance de amplificar o dano elemental.\n" +
                "Valor recomendado: 30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【Bônus de Dano Elemental ao Amplificar (%)】\n" +
                "Aumento de dano elemental ao acionar.\n" +
                "Valor recomendado: 30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【Aumento de Custo de Eitr (%)】\n" +
                "Aumenta o custo de Eitr ao lançar feitiços.\n" +
                "O preço da magia poderosa.\n" +
                "Valor recomendado: 15-25%",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Amplificação Mágica.",

                // === Tier 3: Elemento ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【Bônus de Dano de Gelo (fixo)】\n" +
                "Aumenta o dano da magia de gelo.\n" +
                "Valor recomendado: 2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【Bônus de Dano de Fogo (fixo)】\n" +
                "Aumenta o dano da magia de fogo.\n" +
                "Valor recomendado: 2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【Bônus de Dano de Raio (fixo)】\n" +
                "Aumenta o dano da magia de raio.\n" +
                "Valor recomendado: 2-5",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Elemento Gelo.",

                ["Tier3_FireElement_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Elemento Fogo.",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Elemento Raio.",

                // === Tier 4: Sorte de Mana ===
                ["Tier4_LuckyMana_Chance"] =
                "【Chance de Feitiço Gratuito (%)】\n" +
                "Chance de usar um feitiço sem consumir Eitr.\n" +
                "Sorte de Mana abre o caminho para magia ilimitada.\n" +
                "Valor recomendado: 30-40%",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Sorte de Mana.",

                // === Tier 5-1: Duplo Lançamento (Ativa R) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【Projéteis Adicionais】\n" +
                "Quantidade de projéteis mágicos adicionais.\n" +
                "Recomendado: 5~10",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【Dano do Projétil (%)】\n" +
                "Porcentagem de dano dos projéteis adicionais.\n" +
                "Valor recomendado: 10-20%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【Ângulo de Dispersão (não usado)】\n" +
                "Não usado na versão atual. Fixo em uma direção.",

                ["Tier5_DoubleCast_EitrCost"] =
                "【Custo de Eitr】\n" +
                "Eitr consumido ao ativar a habilidade.\n" +
                "Valor recomendado: 15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 25-40 seg",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Duplo Lançamento.",

                // === Tier 5-2: Cura em Área Instantânea (Ativa H) ===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 25-40 seg",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【Custo de Eitr】\n" +
                "Eitr consumido ao usar a habilidade.\n" +
                "Valor recomendado: 25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【Volume de Cura (% da Vida Máx.)】\n" +
                "Porcentagem de vida restaurada em relação ao máximo.\n" +
                "Valor recomendado: 20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【Raio de Cura (metros)】\n" +
                "Área em que a cura é aplicada.\n" +
                "Valor recomendado: 10-15m",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Cura em Área.",

                // ========================================
                // Crossbow Tree (Árvore de Besta)
                // ========================================

                // === Tier 0: Mestre da Besta ===
                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre da Besta.",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【Bônus de Dano de Besta (%)】\n" +
                "Aumenta o dano base de bestas e virotes.\n" +
                "Valor recomendado: 8-12%",

                // === Tier 1: Tiro Rápido ===
                ["Tier1_RapidFire_Chance"] =
                "【Chance de Tiro Rápido (%)】\n" +
                "Chance de disparar tiro rápido ao atirar.\n" +
                "Dispara vários virotes rapidamente ao acionar.\n" +
                "Valor recomendado: 15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【Quantidade de Disparos do Tiro Rápido】\n" +
                "Número de virotes adicionais no tiro rápido.\n" +
                "Valor recomendado: 2-4 disparos",

                ["Tier1_RapidFire_DamagePercent"] =
                "【Dano do Tiro Rápido (%)】\n" +
                "Porcentagem de dano dos virotes do tiro rápido.\n" +
                "Em relação ao ataque base.\n" +
                "Valor recomendado: 60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【Intervalo do Tiro Rápido (seg)】\n" +
                "Tempo entre disparos no tiro rápido.\n" +
                "Menos = mais rápido.\n" +
                "Valor recomendado: 0.1-0.3 seg",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【Consumo de Virotes no Tiro Rápido】\n" +
                "Quantidade de virotes usados por tiro rápido.\n" +
                "Valor recomendado: 1-2",

                ["Tier1_RapidFire_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tiro Rápido.",

                // === Tier 2: Mira Equilibrada ===
                ["Tier2_BalancedAim_KnockbackChance"] =
                "【Chance de Recuo (%)】\n" +
                "Chance de repelir o inimigo ao acertar com besta.\n" +
                "Postura estável garante um golpe confiável.\n" +
                "Valor recomendado: 20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【Distância de Recuo (metros)】\n" +
                "Quantos metros o inimigo é repelido ao acionar.\n" +
                "Valor recomendado: 2-4m",

                ["Tier2_BalancedAim_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Mira Equilibrada.",

                // === Tier 2: Recarga Rápida ===
                ["Tier2_RapidReload_SpeedIncrease"] =
                "【Aumento de Velocidade de Recarga (%)】\n" +
                "Aumenta a velocidade de recarga da besta.\n" +
                "Carregue o próximo virote mais rápido.\n" +
                "Valor recomendado: 10-20%",

                ["Tier2_RapidReload_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Recarga Rápida.",

                // === Tier 2: Tiro Honesto ===
                ["Tier2_HonestShot_DamageBonus"] =
                "【Bônus de Dano Base (%)】\n" +
                "Aumenta adicionalmente o ataque base da besta.\n" +
                "Valor recomendado: 10-18%",

                ["Tier2_HonestShot_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tiro Honesto.",

                // === Tier 3: Auto-recarga ===
                ["Tier3_AutoReload_Chance"] =
                "【Chance de Auto-recarga (%)】\n" +
                "Chance de realizar a próxima recarga a 200% de velocidade ao acertar.\n" +
                "Ajuda a manter o ritmo de ataque contínuo.\n" +
                "Valor recomendado: 20-35%",

                ["Tier3_AutoReload_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Auto-recarga.",

                // === Tier 4: Tiro Rápido Nv.2 ===
                ["Tier4_RapidFireLv2_Chance"] =
                "【Chance de Tiro Rápido Nv.2 (%)】\n" +
                "Chance de disparar o tiro rápido aprimorado.\n" +
                "Acumula com a chance do Tiro Rápido Nv.1.\n" +
                "Valor recomendado: 20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【Quantidade de Disparos Nv.2】\n" +
                "Número de virotes adicionais no tiro rápido aprimorado.\n" +
                "Mais do que o Tiro Rápido Nv.1.\n" +
                "Valor recomendado: 4-6 disparos",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【Dano do Tiro Rápido Nv.2 (%)】\n" +
                "Porcentagem de dano do tiro rápido aprimorado.\n" +
                "Mais dano que o Nv.1.\n" +
                "Valor recomendado: 75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【Intervalo do Tiro Rápido Nv.2 (seg)】\n" +
                "Tempo entre disparos do tiro rápido aprimorado.\n" +
                "Valor recomendado: 0.1-0.3 seg",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【Consumo de Virotes Nv.2】\n" +
                "Quantidade de virotes no tiro rápido aprimorado.\n" +
                "Valor recomendado: 1-2",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tiro Rápido Nv.2.",

                // === Tier 4: Golpe Final ===
                ["Tier4_FinalStrike_HpThreshold"] =
                "【Limite de Vida do Inimigo (%)】\n" +
                "Causa dano bônus a inimigos com vida acima deste limite.\n" +
                "Eficaz contra alvos com muita vida.\n" +
                "Valor recomendado: 40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【Dano Bônus (%)】\n" +
                "Dano adicional a inimigos acima do limite de vida.\n" +
                "Valor recomendado: 20-40%",

                ["Tier4_FinalStrike_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Golpe Final.",

                // === Tier 5: Um Tiro (Ativa R) ===
                ["Tier5_OneShot_Duration"] =
                "【Duração do Buff (seg)】\n" +
                "Por quanto tempo o buff 'Um Tiro' dura.\n" +
                "Durante esse tempo é possível dar um tiro poderoso.\n" +
                "Valor recomendado: 8-12 seg",

                ["Tier5_OneShot_DamageBonus"] =
                "【Bônus de Dano 'Um Tiro' (%)】\n" +
                "Dano adicional ao disparar 'Um Tiro'.\n" +
                "O poder de um tiro devastador.\n" +
                "Valor recomendado: 150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【Distância de Recuo (metros)】\n" +
                "Quantos metros o inimigo é repelido ao acertar.\n" +
                "O impacto poderoso afasta os inimigos.\n" +
                "Valor recomendado: 5-10m",

                ["Tier5_OneShot_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 25-40 seg",

                ["Tier5_OneShot_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Um Tiro.",

                // ========================================
                // Bow Tree (Árvore de Arco)
                // ========================================

                // === Tier 0: Mestre do Arco ===
                ["Tier0_BowExpert_DamageBonus"] =
                "【Bônus de Dano de Arco (%)】\n" +
                "Aumenta o dano base de arcos e flechas.\n" +
                "Valor recomendado: 8-12%",

                ["Tier0_BowExpert_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear o Mestre do Arco.",

                // === Tier 1-1: Tiro Concentrado ===
                ["Tier1_FocusedShot_CritBonus"] =
                "【Bônus de Chance Crítica (%)】\n" +
                "Tiro Concentrado aumenta a chance de acerto crítico.\n" +
                "Maior concentração = mais chance de crítico.\n" +
                "Valor recomendado: 5-10%",

                ["Tier1_FocusedShot_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tiro Concentrado.",

                // === Tier 1-2: Tiro Múltiplo Nv.1 ===
                ["Tier1_MultishotLv1_ActivationChance"] =
                "【Chance de Tiro Múltiplo Nv.1 (%)】\n" +
                "Chance de ativar tiro múltiplo ao atirar com arco.\n" +
                "Ao acionar, dispara várias flechas simultaneamente.\n" +
                "Valor recomendado: 15-25%",

                ["Tier1_MultishotLv1_AdditionalArrows"] =
                "【Quantidade de Flechas Adicionais】\n" +
                "Número de flechas adicionais no tiro múltiplo.\n" +
                "Valor recomendado: 2-4",

                ["Tier1_MultishotLv1_ArrowConsumption"] =
                "【Consumo de Flechas】\n" +
                "Quantidade de flechas usadas no tiro múltiplo.\n" +
                "Valor recomendado: 1-2",

                ["Tier1_MultishotLv1_DamagePerArrow"] =
                "【Dano de Cada Flecha (%)】\n" +
                "Porcentagem de dano de cada flecha no tiro múltiplo.\n" +
                "Valor recomendado: 50-70%",

                ["Tier1_MultishotLv1_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tiro Múltiplo Nv.1.",

                // === Tier 2: Maestria do Arco ===
                ["Tier2_BowMastery_SkillBonus"] =
                "【Bônus de Habilidade de Arco (fixo)】\n" +
                "Aumenta o nível de habilidade de arco.\n" +
                "Maior maestria = ataques mais fortes.\n" +
                "Valor recomendado: 5-10",

                ["Tier2_BowMastery_SpecialArrowChance"] =
                "【Chance de Flecha Especial (%)】\n" +
                "Chance de disparar uma flecha com efeito especial.\n" +
                "Pode aplicar veneno, fogo, gelo e outros estados.\n" +
                "Valor recomendado: 25-35%",

                ["Tier2_BowMastery_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Maestria do Arco.",

                // === Tier 3-1: Tiro Silencioso ===
                ["Tier3_SilentStrike_DamageBonus"] =
                "【Bônus de Dano do Tiro Silencioso (fixo)】\n" +
                "Aumenta o dano do arco em valor fixo.\n" +
                "As flechas perfuram inimigos causando mais dano.\n" +
                "Valor recomendado: 3-8",

                ["Tier3_SilentStrike_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tiro Silencioso.",

                // === Tier 3-2: Tiro Múltiplo Nv.2 ===
                ["Tier3_MultishotLv2_ActivationChance"] =
                "【Chance de Tiro Múltiplo Nv.2 (%)】\n" +
                "Chance aprimorada de tiro múltiplo.\n" +
                "Dispara mais flechas que o Nv.1.\n" +
                "Valor recomendado: 20-30%",

                ["Tier3_MultishotLv2_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Tiro Múltiplo Nv.2.",

                // === Tier 3-3: Instinto de Caçador ===
                ["Tier3_HuntingInstinct_CritBonus"] =
                "【Bônus Crítico do Instinto de Caçador (%)】\n" +
                "Instinto de Caçador aumenta a chance de acerto crítico.\n" +
                "Valor recomendado: 8-15%",

                ["Tier3_HuntingInstinct_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Instinto de Caçador.",

                // === Tier 4: Mira Precisa ===
                ["Tier4_PrecisionAim_CritDamage"] =
                "【Bônus de Dano Crítico (%)】\n" +
                "Mira Precisa aumenta o dano de acertos críticos.\n" +
                "Atinja pontos fracos para dano enorme.\n" +
                "Valor recomendado: 25-40%",

                ["Tier4_PrecisionAim_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Mira Precisa.",

                // === Tier 5: Flecha Explosiva (Ativa R) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] =
                "【Dano da Flecha Explosiva (%)】\n" +
                "Ativa R — multiplicador de dano da flecha explosiva.\n" +
                "Flecha poderosa com dano em área.\n" +
                "Valor recomendado: 100-150%",

                ["Tier5_ExplosiveArrow_Radius"] =
                "【Raio da Explosão (metros)】\n" +
                "Raio de dano da flecha explosiva.\n" +
                "Valor recomendado: 3-6m",

                ["Tier5_ExplosiveArrow_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera para reutilizar a habilidade.\n" +
                "Valor recomendado: 15-25 seg",

                ["Tier5_ExplosiveArrow_StaminaCost"] =
                "【Custo de Resistência (%)】\n" +
                "Porcentagem de resistência ao usar a habilidade.\n" +
                "Valor recomendado: 10-20%",

                ["Tier5_ExplosiveArrow_RequiredPoints"] =
                "【Pontos Necessários】\nPontos para desbloquear Flecha Explosiva.",
            };
        }
    }
}

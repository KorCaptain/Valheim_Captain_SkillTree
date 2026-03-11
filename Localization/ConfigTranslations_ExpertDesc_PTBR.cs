using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetExpertDescriptions_PTBR()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_PTBR_Part1()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetExpertDescriptions_PTBR_Part2()) dict[kv.Key] = kv.Value;
            return dict;
        }

        private static Dictionary<string, string> GetExpertDescriptions_PTBR_Part1()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Skill_Tree_Base
                // ========================================
                ["PassiveMessageDisplay"] =
                "【Exibição de Mensagens Passivas】\n" +
                "Controla a exibição de mensagens de habilidades passivas.\n" +
                "  Center = Centro da tela (padrão)\n" +
                "  TopLeft = Texto pequeno no canto superior esquerdo\n" +
                "  Off = Desativado\n" +
                "※ Mensagens de aprendizado e produção sempre no centro.",

                // ========================================
                // Attack Tree (Árvore de Ataque)
                // ========================================
                ["Tier0_AttackExpert_AllDamageBonus"] =
                "【Bônus de Dano Total (%)】\n" +
                "Aumenta todo o dano físico e elemental.\n" +
                "Fortalecimento básico de ataque para todas as armas.\n" +
                "Valor recomendado: 8-12%",

                ["Tier2_MeleeSpec_BonusTriggerChance"] =
                "【Chance de Dano Extra Corpo a Corpo (%)】\n" +
                "Chance de causar dano adicional em ataques corpo a corpo.\n" +
                "Valor recomendado: 15-25%",

                ["Tier2_MeleeSpec_MeleeDamage"] =
                "【Dano Extra Corpo a Corpo (fixo)】\n" +
                "Dano adicional fixo ao acionar o efeito.\n" +
                "Valor recomendado: 8-15",

                ["Tier2_BowSpec_BonusTriggerChance"] =
                "【Chance de Dano Extra de Arco (%)】\n" +
                "Chance de causar dano adicional ao atacar com arco.\n" +
                "Valor recomendado: 15-25%",

                ["Tier2_BowSpec_BowDamage"] =
                "【Dano Extra de Arco (fixo)】\n" +
                "Dano adicional fixo ao acionar o efeito.\n" +
                "Valor recomendado: 6-12",

                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] =
                "【Chance de Dano Extra de Besta (%)】\n" +
                "Chance de causar dano adicional ao atirar com besta.\n" +
                "Valor recomendado: 12-20%",

                ["Tier2_CrossbowSpec_CrossbowDamage"] =
                "【Dano Extra de Besta (fixo)】\n" +
                "Dano adicional fixo ao acionar o efeito.\n" +
                "Valor recomendado: 7-13",

                ["Tier2_StaffSpec_ElementalTriggerChance"] =
                "【Chance de Dano Elemental de Cajado (%)】\n" +
                "Chance de causar dano elemental adicional ao atacar com cajado.\n" +
                "Valor recomendado: 15-25%",

                ["Tier2_StaffSpec_StaffDamage"] =
                "【Dano Extra de Cajado (fixo)】\n" +
                "Dano adicional fixo ao acionar o efeito.\n" +
                "Valor recomendado: 6-12",

                ["Tier1_BaseAttack_PhysicalDamageBonus"] =
                "【Bônus de Dano Físico (fixo)】\n" +
                "Aumenta o dano físico de todas as armas em valor fixo.\n" +
                "Valor recomendado: 1-3",

                ["Tier1_BaseAttack_ElementalDamageBonus"] =
                "【Bônus de Dano Elemental (fixo)】\n" +
                "Aumenta o dano elemental (fogo, gelo, raio) em valor fixo.\n" +
                "Valor recomendado: 1-3",

                ["Tier3_AttackBoost_PhysicalDamageBonus"] =
                "【Bônus Dano Físico - Duas Mãos (%)】\n" +
                "Aumenta o dano físico ao usar armas de duas mãos.\n" +
                "Valor recomendado: 8-15%",

                ["Tier3_AttackBoost_ElementalDamageBonus"] =
                "【Bônus Dano Elemental - Duas Mãos (%)】\n" +
                "Aumenta o dano elemental ao usar armas de duas mãos.\n" +
                "Valor recomendado: 8-15%",

                ["Tier4_PrecisionAttack_CritChance"] =
                "【Bônus de Chance Crítica (%)】\n" +
                "Aumenta a chance de acerto crítico para todos os ataques.\n" +
                "Valor recomendado: 3-8%",

                ["Tier4_MeleeEnhance_2HitComboBonus"] =
                "【Bônus de Combo 2 Golpes (%)】\n" +
                "Aumenta o dano ao realizar 2 ataques corpo a corpo consecutivos.\n" +
                "Valor recomendado: 8-15%",

                ["Tier4_RangedEnhance_RangedDamageBonus"] =
                "【Bônus de Dano à Distância (fixo)】\n" +
                "Aumenta o dano à distância (arco, besta) em valor fixo.\n" +
                "Valor recomendado: 3-8",

                ["Tier5_SpecialStat_SpecBonus"] =
                "【Recuperação de Resistência】\n" +
                "Porcentagem de recuperação de resistência ao acertar.\n" +
                "Valor recomendado: 3-10",

                ["Tier5_Charge_TriggerChance"] =
                "【Chance de Ativação】\n" +
                "Chance de recuperar resistência ao acertar.\n" +
                "Valor recomendado: 20-50",

                ["Tier6_WeakPointAttack_CritDamageBonus"] =
                "【Bônus de Dano Crítico (%)】\n" +
                "Aumenta o dano adicional ao acertar um golpe crítico.\n" +
                "Valor recomendado: 10-20%",

                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] =
                "【Bônus Dano - Duas Mãos (%)】\n" +
                "Aumenta o dano total ao usar armas de duas mãos.\n" +
                "Valor recomendado: 8-15%",

                ["Tier6_ElementalAttack_ElementalBonus"] =
                "【Bônus Dano Elemental de Cajado (%)】\n" +
                "Aumenta o dano elemental do cajado (fogo, gelo, raio).\n" +
                "Valor recomendado: 8-15%",

                ["Tier6_ComboFinisher_3HitComboBonus"] =
                "【Bônus Finalizador Combo 3 Golpes (%)】\n" +
                "Aumenta o dano do golpe final em combo de 3 ataques corpo a corpo.\n" +
                "Valor recomendado: 12-20%",

                // ========================================
                // Defense Tree (Árvore de Defesa)
                // ========================================
                ["Tier0_DefenseExpert_HPBonus"] =
                "【Bônus de Vida (fixo)】\n" +
                "Aumenta a vida máxima em valor fixo.\n" +
                "Valor recomendado: 3-8",

                ["Tier0_DefenseExpert_ArmorBonus"] =
                "【Bônus de Armadura (fixo)】\n" +
                "Aumenta a armadura em valor fixo.\n" +
                "Valor recomendado: 1-4",

                ["Tier1_SkinHardening_HPBonus"] =
                "【Bônus de Vida (fixo)】\n" +
                "Aumenta adicionalmente a vida máxima.\n" +
                "Valor recomendado: 3-8",

                ["Tier1_SkinHardening_ArmorBonus"] =
                "【Bônus de Armadura (fixo)】\n" +
                "Aumenta adicionalmente a armadura.\n" +
                "Valor recomendado: 3-8",

                ["Tier2_MindBodyTraining_StaminaBonus"] =
                "【Bônus de Resistência Máxima (fixo)】\n" +
                "Aumenta a resistência máxima.\n" +
                "Valor recomendado: 20-30",

                ["Tier2_MindBodyTraining_EitrBonus"] =
                "【Bônus de Eitr Máximo (fixo)】\n" +
                "Aumenta o Eitr máximo para ataques mágicos.\n" +
                "Valor recomendado: 20-30",

                ["Tier2_HealthTraining_HPBonus"] =
                "【Bônus de Vida (fixo)】\n" +
                "Aumenta significativamente a vida máxima.\n" +
                "Valor recomendado: 15-25",

                ["Tier2_HealthTraining_ArmorBonus"] =
                "【Bônus de Armadura (fixo)】\n" +
                "Aumenta adicionalmente a armadura.\n" +
                "Valor recomendado: 3-8",

                ["Tier3_CoreBreathing_EitrBonus"] =
                "【Bônus de Eitr (fixo)】\n" +
                "Aumenta o Eitr através de meditação.\n" +
                "Valor recomendado: 8-15",

                ["Tier3_EvasionTraining_DodgeBonus"] =
                "【Bônus de Evasão (%)】\n" +
                "Aumenta a chance de esquivar de ataques inimigos.\n" +
                "Valor recomendado: 3-8%",

                ["Tier3_EvasionTraining_InvincibilityBonus"] =
                "【Aumento de Invencibilidade ao Rolar (%)】\n" +
                "Estende o tempo de invencibilidade durante a rolagem.\n" +
                "Valor recomendado: 15-25%",

                ["Tier3_HealthBoost_HPBonus"] =
                "【Bônus de Vida (fixo)】\n" +
                "Aumenta adicionalmente a vida.\n" +
                "Valor recomendado: 12-20",

                ["Tier3_ShieldTraining_BlockPowerBonus"] =
                "【Bônus de Poder de Bloqueio (fixo)】\n" +
                "Aumenta o poder de bloqueio do escudo.\n" +
                "Valor recomendado: 80-120",

                ["Tier4_GroundStomp_Radius"] =
                "【Raio do Efeito (m)】\n" +
                "Raio da onda de choque.\n" +
                "Valor recomendado: 2.5-4 m",

                ["Tier4_GroundStomp_KnockbackForce"] =
                "【Força de Recuo】\n" +
                "Força com que inimigos são repelidos.\n" +
                "Valor recomendado: 15-25",

                ["Tier4_GroundStomp_Cooldown"] =
                "【Recarga (seg)】\n" +
                "Tempo de espera antes de usar novamente.\n" +
                "Valor recomendado: 100-150 seg",

                ["Tier4_GroundStomp_HPThreshold"] =
                "【Limite de Vida para Ativação Automática】\n" +
                "Ativa automaticamente quando a vida cai abaixo deste limite.\n" +
                "0.35 = 35% de vida\n" +
                "Valor recomendado: 0.30-0.40",

                ["Tier4_GroundStomp_VFXDuration"] =
                "【Duração do Efeito Visual (seg)】\n" +
                "Tempo de exibição do efeito visual.\n" +
                "Valor recomendado: 0.8-1.5 seg",

                ["Tier4_RockSkin_ArmorBonus"] =
                "【Aumento de Armadura (%)】\n" +
                "Aplica bônus percentual à armadura de capacete, peitoral, perneiras e escudo.\n" +
                "Valor recomendado: 10-15%",

                ["Tier5_Endurance_RunStaminaReduction"] =
                "【Redução de Resistência ao Correr (%)】\n" +
                "Reduz o consumo de resistência ao correr.\n" +
                "Valor recomendado: 8-15%",

                ["Tier5_Endurance_JumpStaminaReduction"] =
                "【Redução de Resistência ao Pular (%)】\n" +
                "Reduz o consumo de resistência ao pular.\n" +
                "Valor recomendado: 8-15%",

                ["Tier5_Agility_DodgeBonus"] =
                "【Bônus de Evasão (%)】\n" +
                "Aumenta adicionalmente a chance de evasão.\n" +
                "Valor recomendado: 3-8%",

                ["Tier5_Agility_RollStaminaReduction"] =
                "【Redução de Resistência ao Rolar (%)】\n" +
                "Reduz o consumo de resistência ao rolar.\n" +
                "Valor recomendado: 10-18%",

                ["Tier5_TrollRegen_HPRegenBonus"] =
                "【Bônus de Regeneração de Vida (por seg)】\n" +
                "Restaura vida automaticamente como um troll.\n" +
                "Valor recomendado: 3-8",

                ["Tier5_TrollRegen_RegenInterval"] =
                "【Intervalo de Regeneração (seg)】\n" +
                "Período de recuperação de vida.\n" +
                "Valor recomendado: 1.5-3 seg",

                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] =
                "【Bônus de Poder de Bloqueio (fixo)】\n" +
                "Aumenta significativamente o poder de bloqueio do escudo.\n" +
                "Valor recomendado: 80-120",

                ["Tier5_BlockMaster_ParryDurationBonus"] =
                "【Bônus de Duração de Aparada (seg)】\n" +
                "Estende o tempo do efeito após uma aparada bem-sucedida.\n" +
                "Valor recomendado: 0.8-1.5 seg",

                ["Tier6_NerveEnhancement_DodgeBonus"] =
                "【Bônus de Evasão Condicional (30s, %)】\n" +
                "Ativa quando não há evasão por 30 segundos.\n" +
                "Valor recomendado: 30-50%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【Bônus de Vida (%)】\n" +
                "Aumenta a vida máxima em porcentagem.\n" +
                "Valor recomendado: 25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【Resistência Física/Elemental (%)】\n" +
                "Reduz todo o dano físico e elemental.\n" +
                "Valor recomendado: 8-15%",

                ["Tier6_JotunnShield_BlockStaminaReduction"] =
                "【Redução de Resistência ao Bloquear (%)】\n" +
                "Reduz o consumo de resistência ao bloquear com escudo.\n" +
                "Valor recomendado: 20-30%",

                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] =
                "【Velocidade com Escudo Normal (%)】\n" +
                "Aumenta a velocidade de movimento com escudo normal.\n" +
                "Valor recomendado: 3-8%",

                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] =
                "【Velocidade com Escudo de Torre (%)】\n" +
                "Aumenta a velocidade de movimento com escudo de torre.\n" +
                "Valor recomendado: 8-15%",
            };
        }
    }
}

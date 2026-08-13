using System;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가 마법부여 신규 효과 패치 (타입 16~20) — 무기군별 확률적 속성 피해
    /// - 검/도끼: 화염(FireProc), 둔기: 영혼(SpiritProc), 단검: 독(PoisonProc)
    /// - 활: 번개(LightningProc), 석궁/창/폴암: 냉기(FrostProc), 폴암: 번개(LightningProc)도 겸함
    /// - 지팡이/완드: 4속성 중 제작 시 무작위 배정
    ///
    /// 발동 확률과 피해량은 서로 다른 소스에서 결정된다:
    ///   발동 확률 = 아이템이 부여된 제작 전문가 레벨(1~5)에 연동된 고정값 (Producer_Config.GetElementalProcChance)
    ///   피해량    = 아이템에 롤된 값(%, Producer_Enchant.json 표준 곱선) × 타격 기본 데미지 합계
    /// </summary>
    public static class ProducerCrafting_WeaponElement
    {
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Producer_Enchant_ElementalProc_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Prefix(Character __instance, HitData hit)
            {
                try
                {
                    var attacker = hit.GetAttacker();
                    if (attacker == null || !attacker.IsPlayer()) return;

                    var player = attacker as Player;
                    if (player == null) return;

                    var weapon = player.GetCurrentWeapon();
                    if (weapon == null) return;

                    var enchantType = ProducerCrafting.GetEnchantType(weapon);
                    string field = enchantType switch
                    {
                        ProducerCrafting.EnchantType.FireProc      => "fire",
                        ProducerCrafting.EnchantType.SpiritProc    => "spirit",
                        ProducerCrafting.EnchantType.PoisonProc    => "poison",
                        ProducerCrafting.EnchantType.LightningProc => "lightning",
                        ProducerCrafting.EnchantType.FrostProc     => "frost",
                        _ => null
                    };
                    if (field == null) return;

                    int enchantLevel = ProducerCrafting.GetEnchantLevel(weapon);
                    if (enchantLevel <= 0) return;

                    float chance = Producer_Config.GetElementalProcChance(enchantLevel);
                    if (chance <= 0f) return;
                    if (UnityEngine.Random.value >= (chance / 100f)) return;

                    float magnitudePct = ProducerCrafting.GetEnchantValue(weapon);
                    if (magnitudePct <= 0f) return;

                    float bonus = hit.m_damage.GetTotalDamage() * (magnitudePct / 100f);
                    if (bonus <= 0f) return;

                    switch (field)
                    {
                        case "fire":      hit.m_damage.m_fire      += bonus; break;
                        case "spirit":    hit.m_damage.m_spirit    += bonus; break;
                        case "poison":    hit.m_damage.m_poison    += bonus; break;
                        case "lightning": hit.m_damage.m_lightning += bonus; break;
                        case "frost":     hit.m_damage.m_frost     += bonus; break;
                    }

                    Plugin.Log.LogDebug($"[제작 전문가 속성 인챈트] {enchantType} 발동: +{bonus:F1} ({field})");
                }
                catch (Exception) { }
            }
        }
    }
}

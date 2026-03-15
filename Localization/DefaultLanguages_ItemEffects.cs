using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class DefaultLanguages
    {
        private static Dictionary<string, string> GetKorean_ItemEffects()
        {
            return new Dictionary<string, string>
            {
                // === Weapon Tooltip Effects ===
                ["weapon_effect_phys_atk"]            = "물리 공격력",
                ["weapon_effect_elem_atk"]            = "속성 공격력",
                ["weapon_effect_move_spd"]            = "이동속도",
                ["weapon_effect_atk_spd"]             = "공격속도",
                ["weapon_effect_crit_chance"]         = "치명타 확률",
                ["weapon_effect_crit_dmg"]            = "치명타 피해",
                ["weapon_effect_block_power"]         = "막기 방어력",
                ["weapon_effect_multishot_lv1"]       = "멀티샷 Lv1",
                ["weapon_effect_multishot_lv2"]       = "멀티샷 Lv2",
                ["weapon_effect_rapidfire_lv1"]       = "연속 발사 Lv1",
                ["weapon_effect_rapidfire_lv2"]       = "연속 발사 Lv2",
                ["weapon_effect_melee_expert"]        = "근접 전문가",
                ["weapon_effect_ranged_expert"]       = "원거리 전문가",
                ["weapon_effect_riposte"]             = "칼날 되치기",
                ["weapon_stat_slash_fixed"]           = "베기",
                ["weapon_effect_heavy_strike"]        = "무거운 일격",
                ["weapon_stat_blunt_fixed"]           = "타격",
                ["weapon_effect_melee_expert_suffix"] = "+3 물리",
                ["weapon_effect_ranged_expert_suffix"]= "+2 관통/속성",
                ["weapon_effect_prob"]                = "확률",
                ["weapon_effect_arrows"]              = "발",
                ["weapon_effect_fire_bonus"]          = "화염 속성 추가",
                ["weapon_effect_frost_bonus"]         = "냉기 속성 추가",
                ["weapon_effect_lightning_bonus"]     = "번개 속성 추가",
                ["weapon_effect_spin_attack"]         = "회전베기 (2차공격)",
                ["weapon_effect_polearm_boost"]       = "폴암강화",
                ["weapon_stat_pierce_fixed"]          = "관통",
                ["weapon_effect_suppress_attack"]     = "제압 공격",
                ["weapon_effect_hero_strike"]         = "영웅 타격",
                ["weapon_effect_stagger"]             = "스태거",

                // === Armor Tooltip Effects ===
                ["armor_effect_defense_expert"]  = "방어 전문가",
                ["armor_effect_evasion"]         = "회피",
                ["armor_effect_rockskin"]        = "바위피부",
                ["armor_effect_resistance"]      = "저항",
                ["armor_effect_phys_resist"]     = "물리저항",
                ["armor_effect_elem_resist"]     = "속성저항",
                ["armor_effect_skin_hardening"]  = "피부경화",
                ["armor_effect_health_boost"]    = "체력증강",
                ["armor_effect_berserker_hp"]    = "버서커 체력",
                ["armor_effect_body_training"]   = "체력단련",
                ["armor_effect_move_spd"]        = "이동속도",
                ["armor_effect_shield_training"] = "방패훈련",
                ["armor_effect_parry_master"]    = "막기달인",
                ["armor_effect_jotunn_shield"]   = "요툰의 방패",
                ["armor_stat_hp"]                = "체력",
                ["armor_stat_defense"]           = "방어력",
                ["armor_stat_parry"]             = "패링",
                ["armor_stat_block_stamina"]     = "블럭 스태미나",
                ["armor_stat_sec"]               = "초",
            };
        }

        private static Dictionary<string, string> GetEnglish_ItemEffects()
        {
            return new Dictionary<string, string>
            {
                // === Weapon Tooltip Effects ===
                ["weapon_effect_phys_atk"]            = "Physical Attack",
                ["weapon_effect_elem_atk"]            = "Elemental Attack",
                ["weapon_effect_move_spd"]            = "Movement Speed",
                ["weapon_effect_atk_spd"]             = "Attack Speed",
                ["weapon_effect_crit_chance"]         = "Critical Chance",
                ["weapon_effect_crit_dmg"]            = "Critical Damage",
                ["weapon_effect_block_power"]         = "Block Power",
                ["weapon_effect_multishot_lv1"]       = "Multishot Lv1",
                ["weapon_effect_multishot_lv2"]       = "Multishot Lv2",
                ["weapon_effect_rapidfire_lv1"]       = "Rapid Fire Lv1",
                ["weapon_effect_rapidfire_lv2"]       = "Rapid Fire Lv2",
                ["weapon_effect_melee_expert"]        = "Melee Expert",
                ["weapon_effect_ranged_expert"]       = "Ranged Expert",
                ["weapon_effect_riposte"]             = "Riposte",
                ["weapon_stat_slash_fixed"]           = "Slash",
                ["weapon_effect_heavy_strike"]        = "Heavy Strike",
                ["weapon_stat_blunt_fixed"]           = "Blunt",
                ["weapon_effect_melee_expert_suffix"] = "+3 Physical",
                ["weapon_effect_ranged_expert_suffix"]= "+2 Pierce/Elemental",
                ["weapon_effect_prob"]                = "chance",
                ["weapon_effect_arrows"]              = "arrows",
                ["weapon_effect_fire_bonus"]          = "Fire Bonus",
                ["weapon_effect_frost_bonus"]         = "Frost Bonus",
                ["weapon_effect_lightning_bonus"]     = "Lightning Bonus",
                ["weapon_effect_spin_attack"]         = "Spin Attack (Secondary)",
                ["weapon_effect_polearm_boost"]       = "Polearm Boost",
                ["weapon_stat_pierce_fixed"]          = "Pierce",
                ["weapon_effect_suppress_attack"]     = "Suppress Attack",
                ["weapon_effect_hero_strike"]         = "Hero Strike",
                ["weapon_effect_stagger"]             = "Stagger",

                // === Armor Tooltip Effects ===
                ["armor_effect_defense_expert"]  = "Defense Expert",
                ["armor_effect_evasion"]         = "Evasion",
                ["armor_effect_rockskin"]        = "Rock Skin",
                ["armor_effect_resistance"]      = "Resistance",
                ["armor_effect_phys_resist"]     = "Physical Resist",
                ["armor_effect_elem_resist"]     = "Elemental Resist",
                ["armor_effect_skin_hardening"]  = "Skin Hardening",
                ["armor_effect_health_boost"]    = "Health Boost",
                ["armor_effect_berserker_hp"]    = "Berserker HP",
                ["armor_effect_body_training"]   = "Body Training",
                ["armor_effect_move_spd"]        = "Movement Speed",
                ["armor_effect_shield_training"] = "Shield Training",
                ["armor_effect_parry_master"]    = "Parry Master",
                ["armor_effect_jotunn_shield"]   = "Jotunn's Shield",
                ["armor_stat_hp"]                = "HP",
                ["armor_stat_defense"]           = "Defense",
                ["armor_stat_parry"]             = "Parry",
                ["armor_stat_block_stamina"]     = "Block Stamina",
                ["armor_stat_sec"]               = "s",
            };
        }
    }
}

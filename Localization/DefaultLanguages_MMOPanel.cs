using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class DefaultLanguages
    {
        public static Dictionary<string, string> GetKorean_MMOPanel()
        {
            return new Dictionary<string, string>
            {
                // === EpicMMO 패널 메뉴 항목 ===
                ["mmo_panel_skilltree_effects"]  = "스킬트리 효과",
                ["mmo_panel_tab_job"]            = "직업 효과",
                ["mmo_panel_tab_active"]         = "액티브 스킬",
                ["mmo_panel_tab_passive"]        = "패시브 효과",

                // === 공통 메시지 ===
                ["mmo_panel_separator"]          = "── 스킬트리 투자 효과 ──",
                ["mmo_panel_no_skills"]          = "투자된 스킬 없음",
                ["mmo_panel_close"]              = "닫기",
                ["mmo_panel_title"]              = "스킬트리 효과",

                // === 카테고리 헤더 ===
                ["mmo_cat_ranged"]               = "원거리 전문가",
                ["mmo_cat_melee"]                = "근접 전문가",
                ["mmo_cat_attack"]               = "공격 전문가",
                ["mmo_cat_defense"]              = "방어 전문가",
                ["mmo_cat_speed"]                = "속도 전문가",
                ["mmo_cat_production"]           = "생산 전문가",
                ["mmo_cat_job"]                  = "직업 스킬",
                ["mmo_cat_staff"]                = "지팡이 전문가",

                // === 액티브 스킬 키 라벨 ===
                ["mmo_key_r"]                    = "[원거리]",
                ["mmo_key_g"]                    = "[근접G]",
                ["mmo_key_h"]                    = "[보조H]",
                ["mmo_key_y"]                    = "[직업Y]",

                // === 직업 탭 라벨 ===
                ["mmo_job_level"]                = "Lv.",
                ["mmo_job_none"]                 = "투자된 직업 없음",
                ["mmo_active_none"]              = "투자된 액티브 스킬 없음",
                ["mmo_passive_none"]             = "투자된 패시브 스킬 없음",
            };
        }

        public static Dictionary<string, string> GetEnglish_MMOPanel()
        {
            return new Dictionary<string, string>
            {
                // === EpicMMO panel menu items ===
                ["mmo_panel_skilltree_effects"]  = "Skill Tree Effects",
                ["mmo_panel_tab_job"]            = "Job Effects",
                ["mmo_panel_tab_active"]         = "Active Skills",
                ["mmo_panel_tab_passive"]        = "Passive Effects",

                // === Common messages ===
                ["mmo_panel_separator"]          = "── Skill Tree Effects ──",
                ["mmo_panel_no_skills"]          = "No skills invested",
                ["mmo_panel_close"]              = "Close",
                ["mmo_panel_title"]              = "Skill Tree Effects",

                // === Category headers ===
                ["mmo_cat_ranged"]               = "Ranged Expert",
                ["mmo_cat_melee"]                = "Melee Expert",
                ["mmo_cat_attack"]               = "Attack Expert",
                ["mmo_cat_defense"]              = "Defense Expert",
                ["mmo_cat_speed"]                = "Speed Expert",
                ["mmo_cat_production"]           = "Production Expert",
                ["mmo_cat_job"]                  = "Job Skills",
                ["mmo_cat_staff"]                = "Staff Expert",

                // === Active skill key labels ===
                ["mmo_key_r"]                    = "[Ranged]",
                ["mmo_key_g"]                    = "[Melee G]",
                ["mmo_key_h"]                    = "[Secondary H]",
                ["mmo_key_y"]                    = "[Job Y]",

                // === Job tab labels ===
                ["mmo_job_level"]                = "Lv.",
                ["mmo_job_none"]                 = "No job skills invested",
                ["mmo_active_none"]              = "No active skills invested",
                ["mmo_passive_none"]             = "No passive skills invested",
            };
        }
    }
}

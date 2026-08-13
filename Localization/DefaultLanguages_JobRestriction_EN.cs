using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class DefaultLanguages
    {
        /// <summary>Job-based skill tree restriction / job change system UI text (English)</summary>
        private static Dictionary<string, string> GetEnglish_JobRestriction()
        {
            return new Dictionary<string, string>
            {
                ["job_tree_migration_notice"] = "A patch added job-based weapon skill tree restrictions - only weapon expert trees (Bow/Crossbow/Staff/Sword/Mace/Dagger/Spear/Polearm) were reset once. Job/Production/Attack/Defense/Speed skills are untouched. Please reinvest under the new rules.",
                ["job_select_confirm_title"] = "Confirm Job Selection",
                ["job_change_confirm_title"] = "Confirm Job Change",

                ["job_tree_summary_archer"] =
                    "Learn the full Bow expert tree (passive+active).\nAfter mastering Bow, choose Crossbow OR Dagger passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.",
                ["job_tree_summary_archer_rejob"] =
                    "Learn the full Bow expert tree (passive+active).\nAfter mastering Bow, choose Crossbow OR Dagger passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.\nNote: You may change jobs only once per character.",

                ["job_tree_summary_mage"] =
                    "Learn the full Staff expert tree (passive+active).\nAfter mastering Staff, Crossbow passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.",
                ["job_tree_summary_mage_rejob"] =
                    "Learn the full Staff expert tree (passive+active).\nAfter mastering Staff, Crossbow passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.\nNote: You may change jobs only once per character.",

                ["job_tree_summary_paladin"] =
                    "Choose 1 of Mace or Sword(Axe) expert tree (full).\nOptionally learn Crossbow OR Bow passives only (no actives).\nCan equip a shield.\nProceeding will reset all of your current skill points.",
                ["job_tree_summary_paladin_rejob"] =
                    "Choose 1 of Mace or Sword(Axe) expert tree (full).\nOptionally learn Crossbow OR Bow passives only (no actives).\nCan equip a shield.\nProceeding will reset all of your current skill points.\nNote: You may change jobs only once per character.",

                ["job_tree_summary_berserker"] =
                    "Choose 1 melee expert tree (Sword/Mace/Spear/Polearm, full).\nAlso learn Crossbow passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.",
                ["job_tree_summary_berserker_rejob"] =
                    "Choose 1 melee expert tree (Sword/Mace/Spear/Polearm, full).\nAlso learn Crossbow passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.\nNote: You may change jobs only once per character.",

                ["job_tree_summary_producer"] =
                    "Choose 1 of Sword/Mace/Spear/Polearm/Bow/Crossbow expert tree (full).\nThe other 5 trees: passives only (no actives).\nCan equip a shield.\nProceeding will reset all of your current skill points.",
                ["job_tree_summary_producer_rejob"] =
                    "Choose 1 of Sword/Mace/Spear/Polearm/Bow/Crossbow expert tree (full).\nThe other 5 trees: passives only (no actives).\nCan equip a shield.\nProceeding will reset all of your current skill points.\nNote: You may change jobs only once per character.",

                ["job_tree_summary_tanker"] =
                    "Choose 1 of Sword(Axe)/Mace/Spear expert tree (full).\nAlso learn Crossbow passives only (no actives).\nCan equip a shield.\nProceeding will reset all of your current skill points.",
                ["job_tree_summary_tanker_rejob"] =
                    "Choose 1 of Sword(Axe)/Mace/Spear expert tree (full).\nAlso learn Crossbow passives only (no actives).\nCan equip a shield.\nProceeding will reset all of your current skill points.\nNote: You may change jobs only once per character.",

                ["job_tree_summary_rogue"] =
                    "Learn the full Dagger expert tree (passive+active).\nThen choose Bow OR Crossbow passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.",
                ["job_tree_summary_rogue_rejob"] =
                    "Learn the full Dagger expert tree (passive+active).\nThen choose Bow OR Crossbow passives only (no actives).\nCannot equip a shield.\nProceeding will reset all of your current skill points.\nNote: You may change jobs only once per character.",

                ["job_change_missing_items"] = "The following items are required to change to this job:",
                ["job_change_limit_reached"] = "You have used all job changes. (once per character, lifetime)",
                ["job_change_not_enough_coins"] = "Not enough coins for job change.",
                ["job_change_no_player"] = "Player information unavailable.",
                ["job_tree_forbidden"] = "This skill tree cannot be learned with your current job.",
                ["job_tree_locked_base_choice"] = "You already chose a different base weapon tree. Reset it first to switch.",
                ["job_tree_secondary_active_denied"] = "Only passive skills are allowed in secondary trees.",
                ["job_tree_locked_secondary_choice"] = "You already chose a different secondary tree. Reset it first to switch.",
                ["job_tree_base_not_mastered"] = "Fully master the base tree before learning the secondary tree.",
                ["job_shield_equip_denied"] = "Your current job cannot equip a shield.",
            };
        }
    }
}

using System.Collections.Generic;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    // 암살자의 심장(knife_step9_assassin_heart) 레벨 시스템
    // Lv1~7: 보스 트로피 + 바이옴 트로피 조합
    public partial class SkillTreeManager
    {
        public bool HasAssassinHeartLevelItems(int targetLevel)
        {
            var player = Player.m_localPlayer;
            if (player == null) return false;
            var inventory = player.GetInventory();
            if (inventory == null) return false;

            switch (targetLevel)
            {
                case 1: return inventory.HaveItem("$item_trophy_eikthyr") &&
                               inventory.HaveItem("$item_trophy_boar");
                case 2: return inventory.HaveItem("$item_trophy_elder") &&
                               inventory.HaveItem("$item_trophy_troll");
                case 3: return inventory.HaveItem("$item_trophy_bonemass") &&
                               inventory.HaveItem("$item_trophy_abomination");
                case 4: return inventory.HaveItem("$item_trophy_dragonqueen") &&
                               inventory.HaveItem("$item_trophy_sgolem");
                case 5: return inventory.HaveItem("$item_trophy_goblinking") &&
                               inventory.HaveItem("$item_trophy_goblinshaman");
                case 6: return inventory.HaveItem("$item_trophy_seekerqueen") &&
                               inventory.HaveItem("$item_trophy_gjall");
                case 7: return inventory.HaveItem("$item_trophy_fader") &&
                               inventory.HaveItem("$item_trophy_fallenvalkyrie");
                default: return false;
            }
        }

        public List<string> GetMissingAssassinHeartItems(int targetLevel)
        {
            var player = Player.m_localPlayer;
            var missing = new List<string>();
            if (player == null) return missing;
            var inventory = player.GetInventory();
            if (inventory == null) return missing;

            switch (targetLevel)
            {
                case 1:
                    if (!inventory.HaveItem("$item_trophy_eikthyr")) missing.Add(L.Get("item_trophy_eikthyr"));
                    if (!inventory.HaveItem("$item_trophy_boar")) missing.Add(L.Get("item_trophy_boar"));
                    break;
                case 2:
                    if (!inventory.HaveItem("$item_trophy_elder")) missing.Add(L.Get("item_trophy_elder"));
                    if (!inventory.HaveItem("$item_trophy_troll")) missing.Add(L.Get("item_trophy_troll"));
                    break;
                case 3:
                    if (!inventory.HaveItem("$item_trophy_bonemass")) missing.Add(L.Get("item_trophy_bonemass"));
                    if (!inventory.HaveItem("$item_trophy_abomination")) missing.Add(L.Get("item_trophy_abomination"));
                    break;
                case 4:
                    if (!inventory.HaveItem("$item_trophy_dragonqueen")) missing.Add(L.Get("item_trophy_dragonqueen"));
                    if (!inventory.HaveItem("$item_trophy_sgolem")) missing.Add(L.Get("item_trophy_sgolem"));
                    break;
                case 5:
                    if (!inventory.HaveItem("$item_trophy_goblinking")) missing.Add(L.Get("item_trophy_goblinking"));
                    if (!inventory.HaveItem("$item_trophy_goblinshaman")) missing.Add(L.Get("item_trophy_goblinshaman"));
                    break;
                case 6:
                    if (!inventory.HaveItem("$item_trophy_seekerqueen")) missing.Add(L.Get("item_trophy_seekerqueen"));
                    if (!inventory.HaveItem("$item_trophy_gjall")) missing.Add(L.Get("item_trophy_gjall"));
                    break;
                case 7:
                    if (!inventory.HaveItem("$item_trophy_fader")) missing.Add(L.Get("item_trophy_fader"));
                    if (!inventory.HaveItem("$item_trophy_fallenvalkyrie")) missing.Add(L.Get("item_trophy_fallenvalkyrie"));
                    break;
            }
            return missing;
        }

        private void ConsumeAssassinHeartLevelItems(int targetLevel)
        {
            var player = Player.m_localPlayer;
            if (player == null) return;
            var inventory = player.GetInventory();
            if (inventory == null) return;

            switch (targetLevel)
            {
                case 1:
                    inventory.RemoveItem("$item_trophy_eikthyr", 1);
                    inventory.RemoveItem("$item_trophy_boar", 1);
                    break;
                case 2:
                    inventory.RemoveItem("$item_trophy_elder", 1);
                    inventory.RemoveItem("$item_trophy_troll", 1);
                    break;
                case 3:
                    inventory.RemoveItem("$item_trophy_bonemass", 1);
                    inventory.RemoveItem("$item_trophy_abomination", 1);
                    break;
                case 4:
                    inventory.RemoveItem("$item_trophy_dragonqueen", 1);
                    inventory.RemoveItem("$item_trophy_sgolem", 1);
                    break;
                case 5:
                    inventory.RemoveItem("$item_trophy_goblinking", 1);
                    inventory.RemoveItem("$item_trophy_goblinshaman", 1);
                    break;
                case 6:
                    inventory.RemoveItem("$item_trophy_seekerqueen", 1);
                    inventory.RemoveItem("$item_trophy_gjall", 1);
                    break;
                case 7:
                    inventory.RemoveItem("$item_trophy_fader", 1);
                    inventory.RemoveItem("$item_trophy_fallenvalkyrie", 1);
                    break;
            }
        }
    }
}

using System.Collections.Generic;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    // 관통 돌격(polearm_step5_king) 레벨 시스템
    // Lv1: 에기크쉬르 + 멧돼지 트로피 / Lv2~7: 보스 트로피 + 바이옴 트로피
    public partial class SkillTreeManager
    {
        public bool HasPierceChargeItems(int targetLevel)
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
                               inventory.HaveItem("$item_trophy_neck");
                case 3: return inventory.HaveItem("$item_trophy_bonemass") &&
                               inventory.HaveItem("$item_trophy_leech");
                case 4: return inventory.HaveItem("$item_trophy_dragonqueen") &&
                               inventory.HaveItem("$item_trophy_wolf");
                case 5: return inventory.HaveItem("$item_trophy_goblinking") &&
                               inventory.HaveItem("$item_trophy_deathsquito");
                case 6: return inventory.HaveItem("$item_trophy_seekerqueen") &&
                               inventory.HaveItem("$item_trophy_seekerbrute");
                case 7: return inventory.HaveItem("$item_trophy_fader") &&
                               inventory.HaveItem("$item_trophy_volture");
                default: return false;
            }
        }

        public List<string> GetMissingPierceChargeItems(int targetLevel)
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
                    if (!inventory.HaveItem("$item_trophy_neck")) missing.Add(L.Get("item_trophy_neck"));
                    break;
                case 3:
                    if (!inventory.HaveItem("$item_trophy_bonemass")) missing.Add(L.Get("item_trophy_bonemass"));
                    if (!inventory.HaveItem("$item_trophy_leech")) missing.Add(L.Get("item_trophy_leech"));
                    break;
                case 4:
                    if (!inventory.HaveItem("$item_trophy_dragonqueen")) missing.Add(L.Get("item_trophy_dragonqueen"));
                    if (!inventory.HaveItem("$item_trophy_wolf")) missing.Add(L.Get("item_trophy_wolf"));
                    break;
                case 5:
                    if (!inventory.HaveItem("$item_trophy_goblinking")) missing.Add(L.Get("item_trophy_goblinking"));
                    if (!inventory.HaveItem("$item_trophy_deathsquito")) missing.Add(L.Get("item_trophy_deathsquito"));
                    break;
                case 6:
                    if (!inventory.HaveItem("$item_trophy_seekerqueen")) missing.Add(L.Get("item_trophy_seekerqueen"));
                    if (!inventory.HaveItem("$item_trophy_seekerbrute")) missing.Add(L.Get("item_trophy_seekerbrute"));
                    break;
                case 7:
                    if (!inventory.HaveItem("$item_trophy_fader")) missing.Add(L.Get("item_trophy_fader"));
                    if (!inventory.HaveItem("$item_trophy_volture")) missing.Add(L.Get("item_trophy_volture"));
                    break;
            }
            return missing;
        }

        private void ConsumePierceChargeItems(int targetLevel)
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
                    inventory.RemoveItem("$item_trophy_neck", 1);
                    break;
                case 3:
                    inventory.RemoveItem("$item_trophy_bonemass", 1);
                    inventory.RemoveItem("$item_trophy_leech", 1);
                    break;
                case 4:
                    inventory.RemoveItem("$item_trophy_dragonqueen", 1);
                    inventory.RemoveItem("$item_trophy_wolf", 1);
                    break;
                case 5:
                    inventory.RemoveItem("$item_trophy_goblinking", 1);
                    inventory.RemoveItem("$item_trophy_deathsquito", 1);
                    break;
                case 6:
                    inventory.RemoveItem("$item_trophy_seekerqueen", 1);
                    inventory.RemoveItem("$item_trophy_seekerbrute", 1);
                    break;
                case 7:
                    inventory.RemoveItem("$item_trophy_fader", 1);
                    inventory.RemoveItem("$item_trophy_volture", 1);
                    break;
            }
        }
    }
}

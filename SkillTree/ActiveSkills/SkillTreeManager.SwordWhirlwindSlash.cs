using System.Collections.Generic;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    // 회오리베기(sword_step5_defswitch) 레벨 시스템
    // Lv1: 에이크쉬르 + 멧돼지 트로피 / Lv2~7: 보스 트로피 + 바이옴 트로피
    public partial class SkillTreeManager
    {
        public bool HasWhirlwindSlashLevelItems(int targetLevel)
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
                               inventory.HaveItem("$item_trophy_draugrelite");
                case 4: return inventory.HaveItem("$item_trophy_dragonqueen") &&
                               inventory.HaveItem("$item_trophy_sgolem");
                case 5: return inventory.HaveItem("$item_trophy_goblinking") &&
                               inventory.HaveItem("$item_trophy_goblinshaman");
                case 6: return inventory.HaveItem("$item_trophy_seekerqueen") &&
                               inventory.HaveItem("$item_trophy_seekerbrute", false);
                case 7: return inventory.HaveItem("$item_trophy_fader") &&
                               inventory.HaveItem("$item_trophy_fallenvalkyrie");
                default: return false;
            }
        }

        public List<string> GetMissingWhirlwindSlashItems(int targetLevel)
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
                    if (!inventory.HaveItem("$item_trophy_draugrelite")) missing.Add(L.Get("item_trophy_draugrelite"));
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
                    if (!inventory.HaveItem("$item_trophy_seekerbrute", false)) missing.Add(L.Get("item_trophy_seekerbrute"));
                    break;
                case 7:
                    if (!inventory.HaveItem("$item_trophy_fader")) missing.Add(L.Get("item_trophy_fader"));
                    if (!inventory.HaveItem("$item_trophy_fallenvalkyrie")) missing.Add(L.Get("item_trophy_fallenvalkyrie"));
                    break;
            }
            return missing;
        }

        private void ConsumeWhirlwindSlashLevelItems(int targetLevel)
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
                    inventory.RemoveItem("$item_trophy_draugrelite", 1);
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
                    inventory.RemoveItem("$item_trophy_seekerbrute", 1, -1, false);
                    break;
                case 7:
                    inventory.RemoveItem("$item_trophy_fader", 1);
                    inventory.RemoveItem("$item_trophy_fallenvalkyrie", 1);
                    break;
            }
        }
    }
}

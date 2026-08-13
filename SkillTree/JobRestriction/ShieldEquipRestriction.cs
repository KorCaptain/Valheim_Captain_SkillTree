using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree.JobRestriction
{
    /// <summary>
    /// 직업 규칙상 방패 착용이 금지된 직업(아처/메이지/버서커/로그)은 방패를 장착할 수 없게 차단한다.
    /// EquipItem()은 Humanoid에 정의되어 있으므로 typeof(Humanoid)를 패치 대상으로 한다(HARMONY_PATCH_TARGET_RULES.md).
    /// </summary>
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))]
    public static class ShieldEquipRestriction_Humanoid_EquipItem_Patch
    {
        [HarmonyPriority(Priority.High)]
        public static bool Prefix(Humanoid __instance, ItemDrop.ItemData item)
        {
            if (!(__instance is Player player) || player != Player.m_localPlayer) return true;
            if (item?.m_shared == null || item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield) return true;
            if (MMO_System.CaptainLevelConfig.IsAdminModeActive()) return true;

            string jobId = JobTreeRules.GetCurrentJob(player);
            if (jobId == null || !JobTreeRules.TryGetRule(jobId, out var rule) || rule.AllowShield) return true;

            player.Message(MessageHud.MessageType.Center, L.Get("job_shield_equip_denied"));
            return false; // 원본 EquipItem 실행 차단 (장착 실패)
        }
    }
}

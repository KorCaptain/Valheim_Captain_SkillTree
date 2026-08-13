using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 길들인 동물을 플레이어 스킬 데미지로부터 보호하는 전역 패치
    /// </summary>
    public static partial class SkillEffect
    {
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class TamedAnimalDamageProtection_Patch
        {
            public static bool Prefix(Character __instance, HitData hit)
            {
                if (!__instance.IsTamed()) return true;
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return true;

                // 도살칼은 테임 동물에게 데미지 허용
                if (attacker is Player player)
                {
                    var weapon = player.GetCurrentWeapon();
                    if (weapon != null)
                    {
                        string prefabName = weapon.m_dropPrefab?.name?.ToLower() ?? "";
                        string itemName = weapon.m_shared?.m_name?.ToLower() ?? "";
                        if (prefabName.Contains("butcher") || itemName.Contains("butcher"))
                            return true;
                    }
                }

                return false;
            }
        }
    }
}

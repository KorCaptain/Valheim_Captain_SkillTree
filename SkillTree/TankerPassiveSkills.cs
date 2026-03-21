using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 탱커 직업 패시브 저항 패치 (Lv2+)
    /// TankerSkills.cs가 한계 초과이므로 분리
    /// 패시브이므로 VFX/SFX 없음 (텍스트/로그만)
    /// </summary>
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class TankerAllResistPatch
    {
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (!(__instance is Player player)) return;

                // 낙사 데미지(공격자 없음) 제외
                if (hit.m_attacker.IsNone()) return;

                int level = SkillTreeManager.Instance?.GetSkillLevel("Tanker") ?? 0;
                if (level < 2) return;

                float resistPct = TankerSkills.GetTankerResistForLevel(level);
                if (resistPct <= 0f) return;

                float mult = 1f - (resistPct / 100f);
                hit.m_damage.m_blunt     *= mult;
                hit.m_damage.m_slash     *= mult;
                hit.m_damage.m_pierce    *= mult;
                hit.m_damage.m_fire      *= mult;
                hit.m_damage.m_frost     *= mult;
                hit.m_damage.m_lightning *= mult;
                hit.m_damage.m_poison    *= mult;
                hit.m_damage.m_spirit    *= mult;
                hit.m_damage.m_damage    *= mult;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[탱커 저항 패치] 오류: {ex.Message}");
            }
        }
    }
}

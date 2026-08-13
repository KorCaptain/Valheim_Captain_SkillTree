using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// "원거리(활/석궁/마법) 무기로만 모더 처치" 특수 퀘스트(Mountain_Quest6) 조건 추적.
    /// 로컬 플레이어가 모더(Dragon)를 처음 타격해 전투가 시작된 시점부터 처치까지
    /// 근접 무기(검/도끼/둔기/창/폴암/단검)를 사용했는지 기록한다.
    /// QuestGdKingMeleeOnlyTracker와 동일한 구조이며 조건만 반대(근접 금지 → 원거리/마법 전용)다.
    /// </summary>
    [HarmonyPatch]
    public static class QuestModerRangedOnlyTracker
    {
        private static bool _encounterActive;
        private static bool _usedMelee;

        [HarmonyPatch(typeof(Character), "Damage")]
        [HarmonyPostfix]
        public static void OnDamage(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null || hit.GetTotalDamage() <= 0f) return;

                var player = Player.m_localPlayer;
                if (player == null) return;
                if (hit.m_attacker != player.GetZDOID()) return;
                if (Utils.GetPrefabName(__instance.gameObject) != "Dragon") return;

                if (!_encounterActive)
                {
                    _encounterActive = true;
                    _usedMelee = false;
                }

                if (WeaponHelper.IsUsingMeleeWeapon(player))
                    _usedMelee = true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[QuestModerRangedOnlyTracker] 오류: {ex.Message}");
            }
        }

        /// <summary>전투가 시작되었고, 그 동안 근접 무기를 한 번도 쓰지 않았으면 true.</summary>
        public static bool WasRangedOnly() => _encounterActive && !_usedMelee;

        /// <summary>모더 처치 시점에 호출해 다음 전투를 위해 상태를 초기화한다.</summary>
        public static void ResetEncounter()
        {
            _encounterActive = false;
            _usedMelee = false;
        }
    }
}

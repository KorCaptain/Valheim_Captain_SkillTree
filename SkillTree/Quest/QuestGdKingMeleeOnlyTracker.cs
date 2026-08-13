using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// "근접 무기로만 엘더 처치" 특수 퀘스트(BlackForest_Quest7) 조건 추적.
    /// 로컬 플레이어가 엘더(gd_king)를 처음 타격해 전투가 시작된 시점부터 처치까지
    /// 원거리 무기(활/석궁) 또는 마법(원소/피 마법)을 사용했는지 기록한다.
    /// QuestEikthyrFlawlessTracker와 동일하게 로컬 플레이어 기준으로 동작한다.
    /// </summary>
    [HarmonyPatch]
    public static class QuestGdKingMeleeOnlyTracker
    {
        private static bool _encounterActive;
        private static bool _usedRangedOrMagic;

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
                if (Utils.GetPrefabName(__instance.gameObject) != "gd_king") return;

                if (!_encounterActive)
                {
                    _encounterActive = true;
                    _usedRangedOrMagic = false;
                }

                if (WeaponHelper.IsUsingRangedWeapon(player) || WeaponHelper.IsUsingStaffOrWand(player))
                    _usedRangedOrMagic = true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[QuestGdKingMeleeOnlyTracker] 오류: {ex.Message}");
            }
        }

        /// <summary>전투가 시작되었고, 그 동안 원거리/마법을 한 번도 쓰지 않았으면 true.</summary>
        public static bool WasMeleeOnly() => _encounterActive && !_usedRangedOrMagic;

        /// <summary>엘더 처치 시점에 호출해 다음 전투를 위해 상태를 초기화한다.</summary>
        public static void ResetEncounter()
        {
            _encounterActive = false;
            _usedRangedOrMagic = false;
        }
    }
}

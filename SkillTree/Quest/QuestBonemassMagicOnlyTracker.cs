using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// "마법 공격으로만 본메스 처치" 특수 퀘스트(Swamp_Quest7) 조건 추적.
    /// 로컬 플레이어가 본메스(Bonemass)를 처음 타격해 전투가 시작된 시점부터 처치까지
    /// 마법(원소/피 마법)이 아닌 무기로 공격했는지 기록한다.
    /// QuestGdKingMeleeOnlyTracker와 동일한 구조이며 조건만 반대(근접/원거리 → 마법 전용)다.
    /// </summary>
    [HarmonyPatch]
    public static class QuestBonemassMagicOnlyTracker
    {
        private static bool _encounterActive;
        private static bool _usedNonMagic;

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
                if (Utils.GetPrefabName(__instance.gameObject) != "Bonemass") return;

                if (!_encounterActive)
                {
                    _encounterActive = true;
                    _usedNonMagic = false;
                }

                if (!WeaponHelper.IsUsingStaffOrWand(player))
                    _usedNonMagic = true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[QuestBonemassMagicOnlyTracker] 오류: {ex.Message}");
            }
        }

        /// <summary>전투가 시작되었고, 그 동안 마법이 아닌 무기를 한 번도 쓰지 않았으면 true.</summary>
        public static bool WasMagicOnly() => _encounterActive && !_usedNonMagic;

        /// <summary>본메스 처치 시점에 호출해 다음 전투를 위해 상태를 초기화한다.</summary>
        public static void ResetEncounter()
        {
            _encounterActive = false;
            _usedNonMagic = false;
        }
    }
}

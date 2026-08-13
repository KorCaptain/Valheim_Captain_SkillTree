using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// "무피해로 에이크시르 처치" 특수 퀘스트(Meadows_Quest6) 조건 추적.
    /// 로컬 플레이어가 에이크시르를 처음 타격해 전투가 시작된 시점부터 처치까지
    /// 피해를 입었는지 여부를 기록한다. QuestKillPatch.cs와 동일하게 로컬 플레이어 기준으로 동작한다.
    /// </summary>
    [HarmonyPatch]
    public static class QuestEikthyrFlawlessTracker
    {
        private static bool _encounterActive;
        private static bool _tookDamage;

        [HarmonyPatch(typeof(Character), "Damage")]
        [HarmonyPostfix]
        public static void OnDamage(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null || hit.GetTotalDamage() <= 0f) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

                if (__instance.IsPlayer())
                {
                    if (_encounterActive && __instance == (Character)player)
                        _tookDamage = true;
                    return;
                }

                if (_encounterActive) return; // 이미 전투 시작됨
                if (hit.m_attacker != player.GetZDOID()) return;
                if (Utils.GetPrefabName(__instance.gameObject) != "Eikthyr") return;

                _encounterActive = true;
                _tookDamage = false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[QuestEikthyrFlawlessTracker] 오류: {ex.Message}");
            }
        }

        /// <summary>전투가 시작되었고, 그 동안 로컬 플레이어가 피해를 한 번도 입지 않았으면 true.</summary>
        public static bool WasFlawless() => _encounterActive && !_tookDamage;

        /// <summary>에이크시르 처치 시점에 호출해 다음 전투를 위해 상태를 초기화한다.</summary>
        public static void ResetEncounter()
        {
            _encounterActive = false;
            _tookDamage = false;
        }
    }
}

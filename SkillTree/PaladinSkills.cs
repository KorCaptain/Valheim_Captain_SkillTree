using HarmonyLib;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 성기사(Paladin) 패시브 효과 - 최대 스태미나 보너스 패치
    /// GetTotalFoodValue Postfix로 성기사 Lv2+ 최대 스태미나 증가
    /// </summary>
    public static class PaladinSkills
    {
        private static float _cachedStaminaBonus = 0f;
        private static bool _cacheValid = false;

        /// <summary>
        /// 스태미나 캐시 갱신 (레벨업 또는 로그인 시 호출)
        /// </summary>
        public static void RefreshPaladinStatusCache()
        {
            _cacheValid = false;
            Plugin.Log.LogDebug("[PaladinSkills] 스태미나 캐시 무효화");
        }

        private static float GetStaminaBonus()
        {
            if (_cacheValid) return _cachedStaminaBonus;

            var manager = SkillTreeManager.Instance;
            int paladinLevel = manager?.GetSkillLevel("Paladin") ?? 0;
            _cachedStaminaBonus = paladinLevel >= 2 ? Paladin_Config.GetStaminaBonus(paladinLevel) : 0f;
            _cacheValid = true;
            return _cachedStaminaBonus;
        }

        /// <summary>
        /// Player.GetTotalFoodValue Postfix - 성기사 Lv2+ 최대 스태미나 보너스 추가
        /// </summary>
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Player_GetTotalFoodValue_Stamina_Paladin_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(ref float stamina)
            {
                float bonus = GetStaminaBonus();
                if (bonus > 0f) stamina += bonus;
            }
        }
    }
}

using UnityEngine;

namespace CaptainSkillTree.SkillTree.CriticalSystem
{
    /// <summary>
    /// 치명타 피해 계산 및 적용 시스템
    /// 모든 무기와 스킬의 치명타 피해를 중앙에서 관리
    /// </summary>
    public static class CriticalDamage
    {
        /// <summary>
        /// 무기 타입별 치명타 피해 배수 계산
        /// </summary>
        /// <param name="player">플레이어 인스턴스</param>
        /// <param name="weaponType">무기 타입</param>
        /// <returns>치명타 피해 보너스 (%)</returns>
        public static float CalculateCritDamageMultiplier(Player player, Skills.SkillType weaponType)
        {
            if (player == null) return 0f;

            switch (weaponType)
            {
                case Skills.SkillType.Knives:
                    return GetKnifeCritDamage(player);
                case Skills.SkillType.Bows:
                    return GetBowCritDamage(player);
                case Skills.SkillType.Crossbows:
                    return GetCrossbowCritDamage(player);
                case Skills.SkillType.Swords:
                    return GetSwordCritDamage(player);
                case Skills.SkillType.Clubs:
                    return GetMaceCritDamage(player);
                case Skills.SkillType.Spears:
                    return GetSpearCritDamage(player);
                case Skills.SkillType.Polearms:
                    return GetPolearmCritDamage(player);
                default:
                    return 0f;
            }
        }

        /// <summary>
        /// HitData에 치명타 피해 적용
        /// </summary>
        /// <param name="player">플레이어 인스턴스</param>
        /// <param name="hit">HitData 참조</param>
        /// <param name="critMultiplier">치명타 피해 보너스 (%)</param>
        /// <param name="weaponType">무기 타입</param>
        public static void ApplyCriticalDamage(Player player, ref HitData hit, float critMultiplier, Skills.SkillType weaponType)
        {
            if (critMultiplier <= 0f)
            {
                Plugin.Log.LogWarning("[치명타 피해] 피해 배수가 0 이하입니다. 적용 취소.");
                return;
            }

            float damageBonus = 1f + (critMultiplier / 100f);

            // 물리 데미지 타입들 (전투용 - 4종, Rule #11 준수)
            hit.m_damage.m_pierce *= damageBonus;
            hit.m_damage.m_blunt *= damageBonus;
            hit.m_damage.m_slash *= damageBonus;
            hit.m_damage.m_chop *= damageBonus;

            // 시각 효과
            ShowCriticalEffect(player, hit.m_point, weaponType, critMultiplier);

            Plugin.Log.LogInfo($"[치명타 피해] {GetWeaponName(weaponType)} 치명타 발생! +{critMultiplier}% 피해 (배수: {damageBonus:F2}x)");
        }

        #region 공통 치명타 피해 보너스 (모든 무기 적용)

        /// <summary>
        /// 공격 전문가 트리 치명타 피해 보너스 (모든 무기에 적용)
        /// </summary>
        public static float GetCommonCritDamageBonus(Player player)
        {
            float bonus = 0f;

            // Tier 6: 약점 공격 - 치명타 피해 +7%
            if (SkillEffect.HasSkill("atk_crit_dmg"))
            {
                float tierBonus = SkillTreeConfig.AttackCritDamageBonusValue;
                bonus += tierBonus;
                Plugin.Log.LogDebug($"[공통 치명타 피해] Tier 6 약점 공격: +{tierBonus}%");
            }

            return bonus;
        }

        #endregion

        #region 단검 치명타 피해

        /// <summary>
        /// 단검 치명타 피해 계산 (모든 보너스 합산)
        /// </summary>
        public static float GetKnifeCritDamage(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritDamageBonus(player);

            // 구 버전 스킬 (하위 호환)
            if (SkillEffect.HasSkill("knife_crit2"))
            {
                bonus += 25f;
                Plugin.Log.LogDebug("[치명타 피해] 구 버전 단검 스킬: +25%");
            }

            // Tier 7: 암살자 - 치명타 피해 +20%
            if (SkillEffect.HasSkill("knife_step7_execution"))
            {
                float tierBonus = Knife_Config.KnifeExecutionCritDamageValue;
                bonus += tierBonus;
                Plugin.Log.LogDebug($"[치명타 피해] Tier 7 암살자 (패시브): +{tierBonus}%");
            }

            // Tier 9: 암살자의 심장 - ApplyKnifeAssassinHeartCrit에서 직접 hit.m_damage 배율로 처리됨 (이중 적용 금지)

            if (bonus > 0f)
            {
                Plugin.Log.LogDebug($"[단검 치명타 피해] 총 보너스: +{bonus}%");
            }

            return bonus;
        }

        #endregion

        #region 다른 무기 치명타 피해 (향후 확장)

        /// <summary>
        /// 활 치명타 피해 계산 (모든 보너스 합산)
        /// </summary>
        public static float GetBowCritDamage(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritDamageBonus(player);

            // Tier 5: 정조준 - 크리티컬 데미지 +[CONFIG]%
            if (SkillEffect.HasSkill("bow_Step5_master"))
            {
                float tierBonus = SkillTreeConfig.BowStep5MasterCritDamageValue;
                bonus += tierBonus;
                Plugin.Log.LogDebug($"[치명타 피해] Tier 5 정조준 (패시브): +{tierBonus}%");
            }

            if (bonus > 0f)
            {
                Plugin.Log.LogDebug($"[활 치명타 피해] 총 보너스: +{bonus}%");
            }

            return bonus;
        }

        /// <summary>
        /// 석궁 치명타 피해 계산
        /// </summary>
        public static float GetCrossbowCritDamage(Player player)
        {
            // "정직한 한 발" 스킬이 있으면 치명타 피해도 0
            if (SkillEffect.HasSkill("crossbow_Step3_mark"))
            {
                return 0f; // 치명타 비활성화 시 피해 보너스도 무시
            }

            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritDamageBonus(player);

            // 석궁은 치명타 피해 증가 스킬이 없음 (향후 확장 가능)

            return bonus;
        }

        /// <summary>
        /// 검 치명타 피해 (향후 구현)
        /// </summary>
        public static float GetSwordCritDamage(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritDamageBonus(player);

            // TODO: 검 치명타 스킬 구현 시 추가

            return bonus;
        }

        /// <summary>
        /// 둔기 치명타 피해 (향후 구현)
        /// </summary>
        public static float GetMaceCritDamage(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritDamageBonus(player);

            // TODO: 둔기 치명타 스킬 구현 시 추가

            return bonus;
        }

        /// <summary>
        /// 창 치명타 피해 (향후 구현)
        /// </summary>
        public static float GetSpearCritDamage(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritDamageBonus(player);

            // TODO: 창 치명타 스킬 구현 시 추가

            return bonus;
        }

        /// <summary>
        /// 폴암 치명타 피해 (향후 구현)
        /// </summary>
        public static float GetPolearmCritDamage(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritDamageBonus(player);

            // TODO: 폴암 치명타 스킬 구현 시 추가

            return bonus;
        }

        #endregion

        #region 시각 효과

        /// <summary>
        /// 치명타 시각 효과
        /// </summary>
        private static void ShowCriticalEffect(Player player, Vector3 position, Skills.SkillType weaponType, float damage)
        {
            string weaponName = GetWeaponName(weaponType);
            SkillEffect.ShowSkillEffectText(
                player,
                $"💥 {weaponName} 치명타! (+{damage:F0}%)",
                Color.red,
                SkillEffect.SkillEffectTextType.Combat
            );
        }

        /// <summary>
        /// 무기 타입을 한글 이름으로 변환
        /// </summary>
        private static string GetWeaponName(Skills.SkillType weaponType)
        {
            switch (weaponType)
            {
                case Skills.SkillType.Knives: return "단검";
                case Skills.SkillType.Bows: return "활";
                case Skills.SkillType.Crossbows: return "석궁";
                case Skills.SkillType.Swords: return "검";
                case Skills.SkillType.Clubs: return "둔기";
                case Skills.SkillType.Spears: return "창";
                case Skills.SkillType.Polearms: return "폴암";
                case Skills.SkillType.Axes: return "도끼";
                case Skills.SkillType.Blocking: return "방패";
                default: return "무기";
            }
        }

        #endregion
    }
}

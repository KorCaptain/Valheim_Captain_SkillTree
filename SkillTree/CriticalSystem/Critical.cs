using UnityEngine;
using CaptainSkillTree.MMO_System;

namespace CaptainSkillTree.SkillTree.CriticalSystem
{
    /// <summary>
    /// 치명타 확률 계산 및 발생 판정 시스템
    /// 모든 무기와 스킬의 치명타 확률을 중앙에서 관리
    /// </summary>
    public static class Critical
    {
        /// <summary>
        /// 무기 타입별 치명타 확률 계산
        /// </summary>
        /// <param name="player">플레이어 인스턴스</param>
        /// <param name="weaponType">무기 타입</param>
        /// <returns>치명타 확률 (0-100 범위)</returns>
        public static float CalculateCritChance(Player player, Skills.SkillType weaponType)
        {
            if (player == null) return 0f;

            switch (weaponType)
            {
                case Skills.SkillType.Knives:
                    return GetKnifeCritChance(player);
                case Skills.SkillType.Bows:
                    return GetBowCritChance(player);
                case Skills.SkillType.Crossbows:
                    return GetCrossbowCritChance(player);
                case Skills.SkillType.Swords:
                    return GetSwordCritChance(player);
                case Skills.SkillType.Clubs:
                    return GetMaceCritChance(player);
                case Skills.SkillType.Spears:
                    return GetSpearCritChance(player);
                case Skills.SkillType.Polearms:
                    return GetPolearmCritChance(player);
                case Skills.SkillType.Unarmed:
                    return GetKnifeCritChance(player);
                case Skills.SkillType.ElementalMagic:
                case Skills.SkillType.BloodMagic:
                    return GetStaffCritChance(player);
                default:
                    return 0f;
            }
        }

        /// <summary>
        /// 치명타 발생 여부 판정 (0-100 범위)
        /// </summary>
        /// <param name="critChance">치명타 확률 (%)</param>
        /// <returns>치명타 발생 여부</returns>
        public static bool RollCritical(float critChance)
        {
            if (critChance <= 0f) return false;
            float roll = UnityEngine.Random.Range(0f, 100f);
            bool isCrit = roll < critChance;

            if (isCrit)
            {
                Plugin.Log.LogDebug($"[치명타 판정] 확률: {critChance:F1}%, 주사위: {roll:F1} → 성공!");
            }

            return isCrit;
        }

        #region 공통 치명타 보너스 (모든 무기 적용)

        /// <summary>
        /// 공격 전문가 트리 치명타 확률 보너스 (모든 무기에 적용)
        /// </summary>
        public static float GetCommonCritChanceBonus(Player player)
        {
            float bonus = 0f;

            // Tier 4-2: 치명적인 공격(atk_frenzy_trigger, 성장형 Lv1~7) - 치명타 확률 +레벨×증가량%
            int frenzyLv = SkillTreeManager.Instance.GetSkillLevel("atk_frenzy_trigger");
            if (frenzyLv > 0)
            {
                float tierBonus = frenzyLv * Attack_Config.AtkFrenzyTriggerCritChancePerLevelValue;
                bonus += tierBonus;
                Plugin.Log.LogDebug($"[공통 치명타] 치명적인 공격 Lv{frenzyLv}: +{tierBonus}%");
            }

            // EpicMMOSystem Special 스탯 흡수 (자체 치명타 시스템 Unpatch 후 통합 굴림으로 대체)
            bonus += EpicMMOCritIntegration.GetSpecialCritChanceBonus();

            return bonus;
        }

        #endregion

        #region 단검 치명타 확률

        /// <summary>
        /// 단검 치명타 확률 계산 (모든 보너스 합산)
        /// </summary>
        public static float GetKnifeCritChance(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            // 구 버전 스킬 (하위 호환)
            if (SkillEffect.HasSkill("knife_crit1") || SkillEffect.HasSkill("knife_crit2"))
            {
                bonus += 15f;
                Plugin.Log.LogDebug("[치명타] 구 버전 단검 스킬: +15%");
            }

            // Tier 6: 암살자 - 치명타 확률 +12%
            if (SkillEffect.HasSkill("knife_step7_execution"))
            {
                float tierBonus = Knife_Config.KnifeExecutionCritChanceValue;
                bonus += tierBonus;
                Plugin.Log.LogDebug($"[단검 치명타] 암살자 패시브: +{tierBonus}%");
            }

            if (bonus > 0f)
            {
                Plugin.Log.LogDebug($"[단검 치명타] 총 확률: {bonus}%");
            }

            return bonus;
        }

        #endregion

        #region 헤드샷 감지

        /// <summary>
        /// 화살이 몬스터의 머리를 맞췄는지 판별
        /// HitData.m_point(충돌 좌표)와 Character.GetTopPoint()를 비교
        /// </summary>
        /// <param name="target">피격 대상</param>
        /// <param name="hitPoint">HitData.m_point (화살 충돌 좌표)</param>
        /// <returns>헤드샷 여부</returns>
        public static bool IsHeadshot(Character target, Vector3 hitPoint)
        {
            if (target == null) return false;
            if (target.IsPlayer() && !target.IsPVPEnabled()) return false;

            Vector3 topPoint = target.GetTopPoint();
            float bottom = target.transform.position.y;
            float totalHeight = topPoint.y - bottom;

            if (totalHeight <= 0.1f) return false;

            // 상위 20% 영역을 머리로 정의 (Config에서 비율 조정 가능)
            float headThreshold = topPoint.y - (totalHeight * Bow_Config.BowHeadshotZoneRatioValue);
            bool isHead = hitPoint.y >= headThreshold;

            if (isHead)
            {
                Plugin.Log.LogDebug($"[헤드샷] 판정 성공! hitY={hitPoint.y:F2}, threshold={headThreshold:F2}, top={topPoint.y:F2}");
            }

            return isHead;
        }

        #endregion

        #region 다른 무기 치명타 확률 (향후 확장)

        /// <summary>
        /// 활 치명타 확률 계산 (모든 보너스 합산)
        /// </summary>
        public static float GetBowCritChance(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            // Tier 2: 헤드샷 - 머리 적중 시 100% 크리티컬 (Plugin.Patches.cs에서 처리, 여기선 패시브 보너스 없음)

            // Tier 5: 사냥 본능 - 치명타 확률 +[CONFIG]%
            if (SkillEffect.HasSkill("bow_Step5_instinct"))
            {
                float tierBonus = SkillTreeConfig.BowStep5InstinctCritBonusValue;
                bonus += tierBonus;
                Plugin.Log.LogDebug($"[치명타] Tier 5 사냥 본능 (패시브): +{tierBonus}%");
            }

            // 제작 전문가 BowCrit 마법부여 (활 장착 시)
            var bow = player?.GetCurrentWeapon();
            if (bow != null && ProducerCrafting.GetEnchantType(bow) == ProducerCrafting.EnchantType.BowCrit)
                bonus += ProducerCrafting.GetEnchantValue(bow);

            if (bonus > 0f)
            {
                Plugin.Log.LogDebug($"[활 치명타] 총 확률: {bonus}%");
            }

            return bonus;
        }

        /// <summary>
        /// 석궁 치명타 확률 계산
        /// </summary>
        public static float GetCrossbowCritChance(Player player)
        {
            // Tier 3: 정직한 한 발 - 치명타 확률 0% 고정 (치명타 비활성화, 대신 데미지 +35%)
            if (SkillEffect.HasSkill("crossbow_Step3_mark"))
            {
                Plugin.Log.LogDebug("[치명타] Tier 3 정직한 한 발: 치명타 비활성화 (0% 고정, 공통 보너스도 무시)");
                return 0f; // 치명타 완전 차단 (공통 보너스도 적용 안 됨)
            }

            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            // 석궁은 치명타 증가 스킬이 없음 (향후 확장 가능)

            return bonus;
        }

        /// <summary>
        /// 검 치명타 확률 (향후 구현)
        /// </summary>
        public static float GetSwordCritChance(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            // TODO: 검 치명타 스킬 구현 시 추가

            return bonus;
        }

        /// <summary>
        /// 둔기 치명타 확률 (향후 구현)
        /// </summary>
        public static float GetMaceCritChance(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            // TODO: 둔기 치명타 스킬 구현 시 추가

            return bonus;
        }

        /// <summary>
        /// 창 치명타 확률 계산
        /// </summary>
        public static float GetSpearCritChance(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            // Tier 6: 꿰뚫는 창 - 번개 충격으로 변경됨 (SkillEffect.SwordSpearSkillEffects.cs)
            // 3회 연속 적중 시 번개 충격 발동, 치명타 확률 보너스 제거됨

            if (bonus > 0f)
            {
                Plugin.Log.LogDebug($"[창 치명타] 총 확률: {bonus}%");
            }

            return bonus;
        }

        /// <summary>
        /// 폴암 치명타 확률 (향후 구현)
        /// </summary>
        public static float GetPolearmCritChance(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            // TODO: 폴암 치명타 스킬 구현 시 추가

            return bonus;
        }

        /// <summary>
        /// 지팡이/완드 치명타 확률 계산 (ElementalMagic, BloodMagic)
        /// </summary>
        public static float GetStaffCritChance(Player player)
        {
            float bonus = 0f;

            // === 공통 보너스 (공격 전문가 트리) ===
            bonus += GetCommonCritChanceBonus(player);

            return bonus;
        }

        #endregion
    }
}

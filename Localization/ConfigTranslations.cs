using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// BepInEx Configuration Manager 로컬라이제이션
    /// F1 메뉴에서 표시되는 카테고리와 설명을 언어별로 번역합니다.
    /// </summary>
    public static class ConfigTranslations
    {
        /// <summary>
        /// 카테고리 번역 가져오기
        /// </summary>
        public static Dictionary<string, string> GetCategoryTranslations(string lang)
        {
            return (lang == "en") ? GetEnglishCategories() : GetKoreanCategories();
        }

        /// <summary>
        /// 설명 번역 가져오기
        /// </summary>
        public static Dictionary<string, string> GetDescriptionTranslations(string lang)
        {
            return (lang == "en") ? GetEnglishDescriptions() : GetKoreanDescriptions();
        }

        // ============================================
        // 카테고리 번역 (한국어)
        // ============================================
        private static Dictionary<string, string> GetKoreanCategories()
        {
            return new Dictionary<string, string>
            {
                ["Bow Tree"] = "활 트리",
                ["Sword Tree"] = "검 트리",
                ["Spear Tree"] = "창 트리",
                ["Mace Tree"] = "둔기 트리",
                ["Knife Tree"] = "단검 트리",
            };
        }

        // ============================================
        // 설명 번역 (한국어)
        // ============================================
        private static Dictionary<string, string> GetKoreanDescriptions()
        {
            return new Dictionary<string, string>
            {
                // === Bow Tree: 필요 포인트 (7개) ===
                ["Tier1_BowExpert_RequiredPoints"] = "Tier 1: 활 전문가(bow_Step1_damage) - 필요 포인트",
                ["Tier2_FocusedShot_RequiredPoints"] = "Tier 2: 집중 사격(bow_Step2_focus) - 필요 포인트",
                ["Tier2_MultishotLv1_RequiredPoints"] = "Tier 2: 멀티샷 Lv1(bow_Step2_multishot) - 필요 포인트",
                ["Tier3_BowMastery_RequiredPoints"] = "Tier 3: 활 숙련(bow_Step3_speedshot) - 필요 포인트",
                ["Tier4_MultishotLv2_RequiredPoints"] = "Tier 4: 멀티샷 Lv2(bow_Step4_multishot2) - 필요 포인트",
                ["Tier5_PrecisionAim_RequiredPoints"] = "Tier 5: 정조준(bow_Step5_master) - 필요 포인트",
                ["Tier6_ExplosiveArrow_RequiredPoints"] = "Tier 6: 폭발 화살(R키 액티브) - 필요 포인트",

                // === Bow Tree: 멀티샷 패시브 (5개) ===
                ["Tier2_MultishotLv1_ActivationChance"] = "Tier 2: 멀티샷 Lv1(bow_multishot_lv1) - 발동 확률 (%)",
                ["Tier4_MultishotLv2_ActivationChance"] = "Tier 4: 멀티샷 Lv2(bow_multishot_lv2) - 발동 확률 (%)",
                ["Tier2_Multishot_AdditionalArrows"] = "Tier 2: 멀티샷 - 추가 발사 화살 수",
                ["Tier2_Multishot_ArrowConsumption"] = "Tier 2: 멀티샷 - 화살 소모량",
                ["Tier2_Multishot_DamagePerArrow"] = "Tier 2: 멀티샷 - 화살당 데미지 비율 (%)",

                // === Bow Tree: 공격 스킬 (16개) ===
                ["Tier1_BowExpert_DamageBonus"] = "Tier 1: 활 전문가(bow_Step1_damage) - 활 공격력 보너스 (%)",
                ["Tier2_FocusedShot_CritBonus"] = "Tier 2: 집중 사격(bow_step2_focus) - 치명타 확률 보너스 (%)",
                ["Tier3_BowMastery_SkillBonus"] = "Tier 3: 활 숙련(bow_step3_speedshot) - 활 기술(숙련도) 보너스",
                ["Tier3_SilentShot_DamageIncrease"] = "Tier 3: 관통(bow_step3_silentshot) - 활 공격력 증가 (고정값)",
                ["Tier3_SpecialArrow_Chance"] = "Tier 3: 특수 화살(bow_step3_special_arrow) - 특수 화살 발사 확률 (%)",
                ["Tier4_PowerShot_KnockbackChance"] = "Tier 4: 강력한 한 발(bow_step4_powershot) - 강한 넉백 확률 (%)",
                ["Tier4_PowerShot_KnockbackDistance"] = "Tier 4: 강력한 한 발(bow_step4_powershot) - 넉백 거리 (m)",
                ["Tier5_ArrowRain_Chance"] = "Tier 5: 화살비(bow_step5_arrow_rain) - 화살 3개 발사 확률 (%)",
                ["Tier5_ArrowRain_ArrowCount"] = "Tier 5: 화살비(bow_step5_arrow_rain) - 발사할 화살 개수",
                ["Tier5_BackstepShot_CritBonus"] = "Tier 5: 백스텝 샷(bow_step5_backstep_shot) - 구르기 후 치명타 확률 보너스 (%)",
                ["Tier5_BackstepShot_Duration"] = "Tier 5: 백스텝 샷(bow_step5_backstep_shot) - 구르기 후 효과 지속시간 (초)",
                ["Tier5_HuntingInstinct_CritBonus"] = "Tier 5: 사냥 본능(bow_step5_instinct) - 치명타 확률 보너스 (%)",
                ["Tier5_PrecisionAim_CritDamage"] = "Tier 5: 정조준(bow_step5_master) - 크리티컬 데미지 보너스 (%)",
                ["Tier6_CritBoost_DamageBonus"] = "Tier 6: 크리티컬 부스트(bow_step6_critboost) - R키 액티브 스킬 데미지 보너스 (%)",
                ["Tier6_CritBoost_CritChance"] = "Tier 6: 크리티컬 부스트(bow_step6_critboost) - R키 액티브 스킬 치명타 확률 (%)",
                ["Tier6_CritBoost_ArrowCount"] = "Tier 6: 크리티컬 부스트(bow_step6_critboost) - R키 액티브 스킬 화살 개수",
                ["Tier6_CritBoost_Cooldown"] = "Tier 6: 크리티컬 부스트(bow_step6_critboost) - R키 액티브 스킬 쿨타임 (초)",
                ["Tier6_CritBoost_StaminaCost"] = "Tier 6: 크리티컬 부스트(bow_step6_critboost) - R키 액티브 스킬 스태미나 소모 (%)",
                ["Tier6_ExplosiveArrow_DamageMultiplier"] = "Tier 6: 폭발 화살(bow_step6_explosive) - R키 액티브 스킬 데미지 배율 (%)",
                ["Tier6_ExplosiveArrow_Cooldown"] = "Tier 6: 폭발 화살(bow_step6_explosive) - R키 액티브 스킬 쿨타임 (초)",
                ["Tier6_ExplosiveArrow_StaminaCost"] = "Tier 6: 폭발 화살(bow_step6_explosive) - R키 액티브 스킬 스태미나 소모 (%)",
                ["Tier6_ExplosiveArrow_Radius"] = "Tier 6: 폭발 화살(bow_step6_explosive) - 폭발 범위 (m)",

                // === Sword Tree: 필요 포인트 (11개) ===
                ["Sword_Expert_RequiredPoints"] = "Tier 0: 검 전문가 - 필요 포인트",
                ["Sword_FastSlash_RequiredPoints"] = "Tier 1: 빠른 베기(sword_step1_fastslash) - 필요 포인트",
                ["Sword_CounterStance_RequiredPoints"] = "Tier 1: 반격 자세(sword_step1_counter) - 필요 포인트",
                ["Sword_ComboSlash_RequiredPoints"] = "Tier 2: 연속 베기(sword_step2_combo_slash) - 필요 포인트",
                ["Sword_BladeReflect_RequiredPoints"] = "Tier 3: 칼날 되치기(sword_step3_riposte) - 필요 포인트",
                ["Sword_OffenseDefense_RequiredPoints"] = "Tier 4: 공방일체(sword_step3_allinone) - 필요 포인트",
                ["Sword_TrueDuel_RequiredPoints"] = "Tier 5: 진검승부(sword_step4_duel) - 필요 포인트",
                ["Sword_ParryCharge_RequiredPoints"] = "Tier 5: 패링 돌격(sword_step5_defswitch) - 필요 포인트",
                ["Sword_RushSlash_RequiredPoints"] = "Tier 6: 돌진 연속 베기(sword_step5_finalcut) - 필요 포인트",

                // === Sword Tree: 검 전문가 (1개) ===
                ["Sword_Expert_DamageIncrease"] = "Tier 0: 검 전문가 - 피해 증가 (%)",

                // === Sword Tree: 빠른 베기 (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: 빠른 베기(sword_step1_fastslash) - 공격속도 보너스 (%)",

                // === Sword Tree: 연속 베기 (2개) ===
                ["Sword_ComboSlash_Bonus"] = "Tier 2: 연속 베기(sword_step2_combo_slash) - 연속 공격 보너스 (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: 연속 베기(sword_step2_combo_slash) - 버프 지속시간 (초)",

                // === Sword Tree: 칼날 되치기 (1개) ===
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: 칼날 되치기(sword_step3_riposte) - 공격력 보너스 (고정값)",

                // === Sword Tree: 진검승부 (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: 진검승부(sword_step4_duel) - 공격속도 보너스 (%)",

                // === Sword Tree: 패링 돌격 (5개) ===
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: 패링 돌격 - 버프 지속시간 (초)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: 패링 돌격 - 패링 성공 시 돌격 공격력 보너스 (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: 패링 돌격 - 밀어내기 거리 (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: 패링 돌격 - G키 버프 활성화 스태미나 소모",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: 패링 돌격 - 쿨타임 (초)",

                // === Sword Tree: 돌진 연속 베기 (8개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: 돌진 연속 베기 - 1차 공격력 비율 (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: 돌진 연속 베기 - 2차 공격력 비율 (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: 돌진 연속 베기 - 3차 공격력 비율 (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: 돌진 연속 베기 - 초기 돌진 거리 (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: 돌진 연속 베기 - 측면 이동 거리 (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: 돌진 연속 베기 - 스태미나 소모량",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: 돌진 연속 베기 - 쿨타임 (초)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: 돌진 연속 베기 - 이동 속도 (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: 돌진 연속 베기 - 공격 속도 보너스 (%, 기본 대비). 스킬 중 다른 트리 공격속도 무시",

                // === Spear Tree: 필요 포인트 (7개) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: 창 전문가(spear_expert) - 필요 포인트",
                ["Tier1_SpearStep1_RequiredPoints"] = "Tier 1: 창 스킬 - 필요 포인트",
                ["Tier2_SpearStep2_RequiredPoints"] = "Tier 2: 창 스킬 - 필요 포인트",
                ["Tier3_SpearStep3_RequiredPoints"] = "Tier 3: 창 스킬 - 필요 포인트",
                ["Tier4_SpearStep4_RequiredPoints"] = "Tier 4: 창 스킬 - 필요 포인트",
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5: 꿰뚫는 창(G키 액티브) - 필요 포인트",
                ["Tier5_Combo_RequiredPoints"] = "Tier 5: 연공창(H키 액티브) - 필요 포인트",

                // === Spear Tree: 창 전문가 (3개) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: 창 전문가(spear_expert) - 2연속 공격 속도 보너스 (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: 창 전문가(spear_expert) - 2연속 공격력 보너스 (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: 창 전문가(spear_expert) - 효과 지속시간 (초)",

                // === Spear Tree: 투창 전문가 (3개) ===
                ["Tier1_Throw_Cooldown"] = "Tier 1: 투창 전문가(spear_step1_throw) - 투창 쿨타임 (초)",
                ["Tier1_Throw_DamageMultiplier"] = "Tier 1: 투창 전문가(spear_step1_throw) - 투창 데미지 배율 (%)",
                ["Tier1_Throw_BuffDuration_NotUsed"] = "Tier 1: 투창 전문가(spear_step1_throw) - 사용 안 함 (패시브 스킬로 변경됨)",

                // === Spear Tree: 급소 찌르기 (1개) ===
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: 급소 찌르기(spear_step1_crit) - 창 공격력 보너스 (%)",

                // === Spear Tree: 연격창 (1개) ===
                ["Tier3_Rapid_DamageBonus"] = "Tier 3: 연격창(spear_Step3_pierce) - 무기 공격력 보너스 (고정 수치)",

                // === Spear Tree: 회피 찌르기 (2개) ===
                ["Tier4_Evasion_DamageBonus"] = "Tier 4: 회피 찌르기(spear_step2_evasion) - 구르기 직후 공격 피해 보너스 (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4: 회피 찌르기(spear_step2_evasion) - 공격 스태미나 감소 (%)",

                // === Spear Tree: 폭발창 (3개) ===
                ["Tier3_Explosion_Chance"] = "Tier 3: 폭발창(spear_step3_explosion) - 폭발 발동 확률 (%)",
                ["Tier3_Explosion_Radius"] = "Tier 3: 폭발창(spear_step3_explosion) - 폭발 범위 (m)",
                ["Tier3_Explosion_DamageBonus"] = "Tier 3: 폭발창(spear_step3_explosion) - 폭발 공격력 보너스 (%)",

                // === Spear Tree: 이연창 (2개) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4: 이연창(spear_step4_dual) - 2연속 공격 시 공격력 보너스 (%)",
                ["Tier4_Dual_Duration"] = "Tier 4: 이연창(spear_step4_dual) - 버프 지속시간 (초)",

                // === Spear Tree: 꿰뚫는 창 (6개) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] = "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 미사용 (번개 충격으로 변경됨)",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 버프 지속시간 (초)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 번개 충격 데미지 배율 (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 번개 발동 연속 적중 횟수",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - G키 액티브 쿨타임 (초)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - G키 액티브 스태미나 소모 (%)",

                // === Spear Tree: 연공창 (7개) ===
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5: 연공창(spear_step5_combo_active) - H키 액티브 쿨타임 (초)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5: 연공창(spear_step5_combo_active) - H키 액티브 데미지 배율 (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5: 연공창(spear_step5_combo_active) - H키 액티브 스태미나 소모 (%)",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5: 연공창(spear_step5_combo_active) - H키 액티브 넉백 범위 (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5: 연공창(spear_step5_combo_active) - 액티브 효과 범위 (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5: 연공창(spear_step5_combo) - H키 버프 지속시간 (초)",
                ["Tier5_Combo_MaxUses"] = "Tier 5: 연공창(spear_step5_combo) - 버프 중 최대 강화 투창 횟수",

                // === Mace Tree: 필요 포인트 (7개) ===
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: 둔기 전문가 - 필요 포인트",
                ["Tier1_MaceStep1_RequiredPoints"] = "Tier 1: 둔기 전문가(mace_Step1_damage) - 필요 포인트",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: 기절 강화(mace_Step2_stun_boost) - 필요 포인트",
                ["Tier3_DefenseBoost_RequiredPoints"] = "Tier 3: 방어 강화(mace_Step3_branch_guard) - 필요 포인트",
                ["Tier3_HeavyHit_RequiredPoints"] = "Tier 3: 무거운 타격(mace_Step3_branch_heavy) - 필요 포인트",
                ["Tier4_PushBack_RequiredPoints"] = "Tier 4: 밀어내기(mace_Step4_push) - 필요 포인트",
                ["Tier5_Tanker_RequiredPoints"] = "Tier 5: 탱커(mace_Step5_tank) - 필요 포인트",
                ["Tier5_PowerBoost_RequiredPoints"] = "Tier 5: 공격력 강화(mace_Step5_dps) - 필요 포인트",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: 그랜드마스터(mace_Step6_grandmaster) - 필요 포인트",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7: 분노의 망치(mace_step7_fury_hammer) - 필요 포인트",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7: 수호자의 진심(mace_step7_guardian_heart) - 필요 포인트",

                // === Mace Tree: 둔기 전문가 (3개) ===
                ["Tier0_MaceExpert_DamageIncrease"] = "Tier 0: 둔기 전문가 - 피해 증가 (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: 둔기 전문가 - 기절 확률 (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: 둔기 전문가 - 기절 지속시간 (초)",

                // === Mace Tree: 둔기 공격력 강화 (1개) ===
                ["Tier1_MaceStep1_DamageBonus"] = "Tier 1: 둔기 전문가(mace_Step1_damage) - 둔기 공격력 보너스 (%)",

                // === Mace Tree: 기절 강화 (2개) ===
                ["Tier2_StunBoost_ChanceBonus"] = "Tier 2: 기절 강화(mace_Step2_stun_boost) - 기절 확률 보너스 (%)",
                ["Tier2_StunBoost_DurationBonus"] = "Tier 2: 기절 강화(mace_Step2_stun_boost) - 기절 지속시간 보너스 (초)",

                // === Mace Tree: 방어 강화 (1개) ===
                ["Tier3_DefenseBoost_DefenseBonus"] = "Tier 3: 방어 강화(mace_Step3_branch_guard) - 방어력 보너스 (고정값)",

                // === Mace Tree: 무거운 타격 (1개) ===
                ["Tier3_HeavyHit_DamageBonus"] = "Tier 3: 무거운 타격(mace_Step3_branch_heavy) - 공격력 보너스 (고정값)",

                // === Mace Tree: 밀어내기 (1개) ===
                ["Tier4_PushBack_KnockbackChance"] = "Tier 4: 밀어내기(mace_Step4_push) - 넉백 확률 (%)",

                // === Mace Tree: 탱커 (2개) ===
                ["Tier5_Tanker_HealthBonus"] = "Tier 5: 탱커(mace_Step5_tank) - 체력 보너스 (%)",
                ["Tier5_Tanker_DamageReduction"] = "Tier 5: 탱커(mace_Step5_tank) - 받는 데미지 감소 (%)",

                // === Mace Tree: 공격력 강화 (2개) ===
                ["Tier5_PowerBoost_DamageBonus"] = "Tier 5: 공격력 강화(mace_Step5_dps) - 공격력 보너스 (%)",
                ["Tier5_PowerBoost_AttackSpeedBonus"] = "Tier 5: 공격력 강화(mace_Step5_dps) - 공격속도 보너스 (%)",

                // === Mace Tree: 그랜드마스터 (1개) ===
                ["Tier6_Grandmaster_DefenseBonus"] = "Tier 6: 그랜드마스터(mace_Step6_grandmaster) - 방어력 보너스 (%)",

                // === Mace Tree: 분노의 망치 (6개) ===
                ["Tier7_FuryHammer_DamageMultiplier"] = "Tier 7: 분노의 망치 - 데미지 배율 (%) - 현재 공격력 기준",
                ["Tier7_FuryHammer_AttackSpeedBonus"] = "Tier 7: 분노의 망치 - 공격속도 버프 (%)",
                ["Tier7_FuryHammer_PullForce"] = "Tier 7: 분노의 망치 - 중력 세기",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7: 분노의 망치(mace_step7_fury_hammer) - 스태미나 소모",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7: 분노의 망치(mace_step7_fury_hammer) - 쿨타임 (초)",
                ["Tier7_FuryHammer_AOERange"] = "Tier 7: 분노의 망치(mace_step7_fury_hammer) - AOE 범위 (미터)",

                // === Mace Tree: 수호자의 진심 (4개) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7: 수호자의 진심(mace_step7_guardian_heart) - 쿨타임 (초)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7: 수호자의 진심(mace_step7_guardian_heart) - 스태미나 소모",
                ["Tier7_GuardianHeart_BuffDuration"] = "Tier 7: 수호자의 진심(mace_step7_guardian_heart) - 버프 지속시간 (초)",
                ["Tier7_GuardianHeart_ReflectDamageRatio"] = "Tier 7: 수호자의 진심(mace_step7_guardian_heart) - 반사 데미지 비율 (%)",

                // === Knife Tree: 필요 포인트 (9개) ===
                ["Knife_Expert_RequiredPoints"] = "Tier 0: 단검 전문가(knife_expert_damage) - 필요 포인트",
                ["Knife_Evasion_RequiredPoints"] = "Tier 1: 회피 숙련(knife_step2_evasion) - 필요 포인트",
                ["Knife_FastMove_RequiredPoints"] = "Tier 2: 빠른 움직임(knife_step3_move_speed) - 필요 포인트",
                ["Knife_CombatMastery_RequiredPoints"] = "Tier 3: 전투 숙련(knife_step4_attack_damage) - 필요 포인트",
                ["Knife_AttackEvasion_RequiredPoints"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 필요 포인트",
                ["Knife_CriticalDamage_RequiredPoints"] = "Tier 5: 치명적 피해(knife_step6_combat_damage) - 필요 포인트",
                ["Knife_Assassin_RequiredPoints"] = "Tier 6: 암살자(knife_step7_execution) - 필요 포인트",
                ["Knife_AssassinSkill_RequiredPoints"] = "Tier 7: 암살술(knife_step8_assassination) - 필요 포인트",
                ["Knife_AssassinHeart_RequiredPoints"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 필요 포인트",

                // === Knife Tree: 단검 전문가 (1개) ===
                ["Knife_Expert_BackstabDamageBonus"] = "Tier 0: 단검 전문가(knife_expert_damage) - 백스탭 데미지 보너스 (%)",

                // === Knife Tree: 회피 숙련 (2개) ===
                ["Knife_Evasion_Chance"] = "Tier 1: 회피 숙련(knife_step2_evasion) - 회피 확률 (%)",
                ["Knife_Evasion_InvincibilityDuration"] = "Tier 1: 회피 숙련(knife_step2_evasion) - 회피 후 무적 시간 (초)",

                // === Knife Tree: 빠른 움직임 (1개) ===
                ["Knife_FastMove_MovementSpeedIncrease"] = "Tier 2: 빠른 움직임(knife_step3_move_speed) - 이동속도 증가 (%)",

                // === Knife Tree: 전투 숙련 (2개) ===
                ["Knife_CombatMastery_DamageIncrease"] = "Tier 3: 전투 숙련(knife_step4_attack_damage) - 적 처치 시 단검 데미지 증가 (고정값)",
                ["Knife_CombatMastery_EffectDuration"] = "Tier 3: 전투 숙련(knife_step4_attack_damage) - 효과 지속시간 (초)",

                // === Knife Tree: 공격과 회피 (3개) ===
                ["Knife_AttackEvasion_EvasionRateIncrease"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 2연속 공격 시 회피율 증가 (%)",
                ["Knife_AttackEvasion_EffectDuration"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 효과 지속시간 (초)",
                ["Knife_AttackEvasion_Cooldown"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 쿨타임 (초)",

                // === Knife Tree: 치명적 피해 (1개) ===
                ["Knife_CriticalDamage_DamageIncrease"] = "Tier 5: 치명적 피해(knife_step6_combat_damage) - 공격력 증가 (%)",

                // === Knife Tree: 암살자 (2개) ===
                ["Knife_Assassin_CritDamageIncrease"] = "Tier 6: 암살자(knife_step7_execution) - 치명타 피해 증가 (%)",
                ["Knife_Assassin_CritChanceIncrease"] = "Tier 6: 암살자(knife_step7_execution) - 치명타 확률 증가 (%)",

                // === Knife Tree: 암살술 (2개) ===
                ["Knife_AssassinSkill_StaggerChance"] = "Tier 7: 암살술(knife_step8_assassination) - 3연속 공격 시 스태거 확률 (%)",
                ["Knife_AssassinSkill_RequiredComboHits"] = "Tier 7: 암살술(knife_step8_assassination) - 스태거 발동에 필요한 연속 공격 횟수",

                // === Knife Tree: 암살자의 심장 (9개) ===
                ["Knife_AssassinHeart_CritDamageMultiplier"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 치명타 데미지 배수",
                ["Knife_AssassinHeart_EffectDuration"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 효과 지속시간 (초)",
                ["Knife_AssassinHeart_StaminaCost"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 스태미나 소모량",
                ["Knife_AssassinHeart_Cooldown"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 쿨타임 (초)",
                ["Knife_AssassinHeart_TeleportSearchRange"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 순간이동 대상 탐색 범위 (m)",
                ["Knife_AssassinHeart_TeleportBackDistance"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 대상 뒤쪽으로 이동할 거리 (m)",
                ["Knife_AssassinHeart_StunDuration"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 순간이동 후 대상 스턴 지속시간 (초)",
                ["Knife_AssassinHeart_ComboAttackCount"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 순간이동 후 연속 공격 횟수",
                ["Knife_AssassinHeart_AttackInterval"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 연속 공격 간격 (초)",
            };
        }

        // ============================================
        // 카테고리 번역 (영어)
        // ============================================
        private static Dictionary<string, string> GetEnglishCategories()
        {
            return new Dictionary<string, string>
            {
                ["Bow Tree"] = "Bow Tree",
                ["Sword Tree"] = "Sword Tree",
                ["Spear Tree"] = "Spear Tree",
                ["Mace Tree"] = "Mace Tree",
                ["Knife Tree"] = "Knife Tree",
            };
        }

        // ============================================
        // 설명 번역 (영어)
        // ============================================
        private static Dictionary<string, string> GetEnglishDescriptions()
        {
            return new Dictionary<string, string>
            {
                // === Bow Tree: Required Points (7개) ===
                ["Tier1_BowExpert_RequiredPoints"] = "Tier 1: Bow Expert - Required Points",
                ["Tier2_FocusedShot_RequiredPoints"] = "Tier 2: Focused Shot - Required Points",
                ["Tier2_MultishotLv1_RequiredPoints"] = "Tier 2: Multishot Lv1 - Required Points",
                ["Tier3_BowMastery_RequiredPoints"] = "Tier 3: Bow Mastery - Required Points",
                ["Tier4_MultishotLv2_RequiredPoints"] = "Tier 4: Multishot Lv2 - Required Points",
                ["Tier5_PrecisionAim_RequiredPoints"] = "Tier 5: Precision Aim - Required Points",
                ["Tier6_ExplosiveArrow_RequiredPoints"] = "Tier 6: Explosive Arrow (R-Key Active) - Required Points",

                // === Bow Tree: Multishot Passive (5개) ===
                ["Tier2_MultishotLv1_ActivationChance"] = "Tier 2: Multishot Lv1 - Activation Chance (%)",
                ["Tier4_MultishotLv2_ActivationChance"] = "Tier 4: Multishot Lv2 - Activation Chance (%)",
                ["Tier2_Multishot_AdditionalArrows"] = "Tier 2: Multishot - Additional Arrows",
                ["Tier2_Multishot_ArrowConsumption"] = "Tier 2: Multishot - Arrow Consumption",
                ["Tier2_Multishot_DamagePerArrow"] = "Tier 2: Multishot - Damage Per Arrow (%)",

                // === Bow Tree: Attack Skills (16개) ===
                ["Tier1_BowExpert_DamageBonus"] = "Tier 1: Bow Expert - Bow Damage Bonus (%)",
                ["Tier2_FocusedShot_CritBonus"] = "Tier 2: Focused Shot - Critical Chance Bonus (%)",
                ["Tier3_BowMastery_SkillBonus"] = "Tier 3: Bow Mastery - Bow Skill Proficiency Bonus",
                ["Tier3_SilentShot_DamageIncrease"] = "Tier 3: Penetration - Bow Damage Increase (Fixed Value)",
                ["Tier3_SpecialArrow_Chance"] = "Tier 3: Special Arrow - Activation Chance (%)",
                ["Tier4_PowerShot_KnockbackChance"] = "Tier 4: Power Shot - Strong Knockback Chance (%)",
                ["Tier4_PowerShot_KnockbackDistance"] = "Tier 4: Power Shot - Knockback Distance (m)",
                ["Tier5_ArrowRain_Chance"] = "Tier 5: Arrow Rain - 3 Arrows Fire Chance (%)",
                ["Tier5_ArrowRain_ArrowCount"] = "Tier 5: Arrow Rain - Arrows to Fire",
                ["Tier5_BackstepShot_CritBonus"] = "Tier 5: Backstep Shot - Critical Chance Bonus After Dodge (%)",
                ["Tier5_BackstepShot_Duration"] = "Tier 5: Backstep Shot - Effect Duration After Dodge (sec)",
                ["Tier5_HuntingInstinct_CritBonus"] = "Tier 5: Hunting Instinct - Critical Chance Bonus (%)",
                ["Tier5_PrecisionAim_CritDamage"] = "Tier 5: Precision Aim - Critical Damage Bonus (%)",
                ["Tier6_CritBoost_DamageBonus"] = "Tier 6: Critical Boost - R-Key Active Skill Damage Bonus (%)",
                ["Tier6_CritBoost_CritChance"] = "Tier 6: Critical Boost - R-Key Active Skill Critical Chance (%)",
                ["Tier6_CritBoost_ArrowCount"] = "Tier 6: Critical Boost - R-Key Active Skill Arrow Count",
                ["Tier6_CritBoost_Cooldown"] = "Tier 6: Critical Boost - R-Key Active Skill Cooldown (sec)",
                ["Tier6_CritBoost_StaminaCost"] = "Tier 6: Critical Boost - R-Key Active Skill Stamina Cost (%)",
                ["Tier6_ExplosiveArrow_DamageMultiplier"] = "Tier 6: Explosive Arrow - R-Key Active Skill Damage Multiplier (%)",
                ["Tier6_ExplosiveArrow_Cooldown"] = "Tier 6: Explosive Arrow - R-Key Active Skill Cooldown (sec)",
                ["Tier6_ExplosiveArrow_StaminaCost"] = "Tier 6: Explosive Arrow - R-Key Active Skill Stamina Cost (%)",
                ["Tier6_ExplosiveArrow_Radius"] = "Tier 6: Explosive Arrow - Explosion Radius (m)",

                // === Sword Tree: Required Points (11개) ===
                ["Sword_Expert_RequiredPoints"] = "Tier 0: Sword Expert - Required Points",
                ["Sword_FastSlash_RequiredPoints"] = "Tier 1: Fast Slash - Required Points",
                ["Sword_CounterStance_RequiredPoints"] = "Tier 1: Counter Stance - Required Points",
                ["Sword_ComboSlash_RequiredPoints"] = "Tier 2: Combo Slash - Required Points",
                ["Sword_BladeReflect_RequiredPoints"] = "Tier 3: Riposte - Required Points",
                ["Sword_OffenseDefense_RequiredPoints"] = "Tier 4: Attack-Defense Unity - Required Points",
                ["Sword_TrueDuel_RequiredPoints"] = "Tier 5: True Duel - Required Points",
                ["Sword_ParryCharge_RequiredPoints"] = "Tier 5: Parry Rush - Required Points",
                ["Sword_RushSlash_RequiredPoints"] = "Tier 6: Rush Slash - Required Points",

                // === Sword Tree: Sword Expert (1개) ===
                ["Sword_Expert_DamageIncrease"] = "Tier 0: Sword Expert - Damage Increase (%)",

                // === Sword Tree: Fast Slash (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: Fast Slash - Attack Speed Bonus (%)",

                // === Sword Tree: Combo Slash (2개) ===
                ["Sword_ComboSlash_Bonus"] = "Tier 2: Combo Slash - Combo Attack Bonus (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: Combo Slash - Buff Duration (sec)",

                // === Sword Tree: Riposte (1개) ===
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: Riposte - Damage Bonus (Fixed Value)",

                // === Sword Tree: True Duel (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: True Duel - Attack Speed Bonus (%)",

                // === Sword Tree: Parry Rush (5개) ===
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: Parry Rush - Buff Duration (sec)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: Parry Rush - Rush Damage Bonus on Successful Parry (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: Parry Rush - Push Distance (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: Parry Rush - G-Key Buff Activation Stamina Cost",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: Parry Rush - Cooldown (sec)",

                // === Sword Tree: Rush Slash (8개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: Rush Slash - 1st Hit Damage Ratio (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: Rush Slash - 2nd Hit Damage Ratio (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: Rush Slash - 3rd Hit Damage Ratio (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: Rush Slash - Initial Rush Distance (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: Rush Slash - Side Move Distance (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: Rush Slash - Stamina Cost",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: Rush Slash - Cooldown (sec)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: Rush Slash - Move Speed (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: Rush Slash - Attack Speed Bonus (% vs Base). Ignores other tree bonuses during skill",

                // === Spear Tree: Required Points (7개) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: Spear Expert - Required Points",
                ["Tier1_SpearStep1_RequiredPoints"] = "Tier 1: Spear Skill - Required Points",
                ["Tier2_SpearStep2_RequiredPoints"] = "Tier 2: Spear Skill - Required Points",
                ["Tier3_SpearStep3_RequiredPoints"] = "Tier 3: Spear Skill - Required Points",
                ["Tier4_SpearStep4_RequiredPoints"] = "Tier 4: Spear Skill - Required Points",
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5: Penetrating Spear (G-Key Active) - Required Points",
                ["Tier5_Combo_RequiredPoints"] = "Tier 5: Combo Spear (H-Key Active) - Required Points",

                // === Spear Tree: Spear Expert (3개) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: Spear Expert - 2-Hit Attack Speed Bonus (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: Spear Expert - 2-Hit Damage Bonus (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: Spear Expert - Effect Duration (sec)",

                // === Spear Tree: Throwing Spear Expert (3개) ===
                ["Tier1_Throw_Cooldown"] = "Tier 1: Throwing Spear Expert - Throwing Cooldown (sec)",
                ["Tier1_Throw_DamageMultiplier"] = "Tier 1: Throwing Spear Expert - Throwing Damage Multiplier (%)",
                ["Tier1_Throw_BuffDuration_NotUsed"] = "Tier 1: Throwing Spear Expert - Not Used (Changed to Passive)",

                // === Spear Tree: Vital Strike (1개) ===
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: Vital Strike - Spear Damage Bonus (%)",

                // === Spear Tree: Rapid Spear (1개) ===
                ["Tier3_Rapid_DamageBonus"] = "Tier 3: Rapid Spear - Weapon Damage Bonus (Fixed Value)",

                // === Spear Tree: Evasion Strike (2개) ===
                ["Tier4_Evasion_DamageBonus"] = "Tier 4: Evasion Strike - Damage Bonus After Dodge (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4: Evasion Strike - Attack Stamina Reduction (%)",

                // === Spear Tree: Explosive Spear (3개) ===
                ["Tier3_Explosion_Chance"] = "Tier 3: Explosive Spear - Explosion Chance (%)",
                ["Tier3_Explosion_Radius"] = "Tier 3: Explosive Spear - Explosion Radius (m)",
                ["Tier3_Explosion_DamageBonus"] = "Tier 3: Explosive Spear - Explosion Damage Bonus (%)",

                // === Spear Tree: Dual Spear (2개) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4: Dual Spear - 2-Hit Damage Bonus (%)",
                ["Tier4_Dual_Duration"] = "Tier 4: Dual Spear - Buff Duration (sec)",

                // === Spear Tree: Penetrating Spear (6개) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] = "Tier 5: Penetrating Spear - Not Used (Changed to Lightning Strike)",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5: Penetrating Spear - Buff Duration (sec)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5: Penetrating Spear - Lightning Strike Damage Multiplier (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5: Penetrating Spear - Lightning Trigger Combo Hits",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5: Penetrating Spear - G-Key Active Cooldown (sec)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5: Penetrating Spear - G-Key Active Stamina Cost (%)",

                // === Spear Tree: Combo Spear (7개) ===
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5: Combo Spear - H-Key Active Cooldown (sec)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5: Combo Spear - H-Key Active Damage Multiplier (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5: Combo Spear - H-Key Active Stamina Cost (%)",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5: Combo Spear - H-Key Active Knockback Radius (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5: Combo Spear - Active Effect Range (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5: Combo Spear - H-Key Buff Duration (sec)",
                ["Tier5_Combo_MaxUses"] = "Tier 5: Combo Spear - Max Enhanced Throws During Buff",

                // === Mace Tree: Required Points (11개) ===
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: Mace Expert - Required Points",
                ["Tier1_MaceStep1_RequiredPoints"] = "Tier 1: Mace Expert - Required Points",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: Stun Enhancement - Required Points",
                ["Tier3_DefenseBoost_RequiredPoints"] = "Tier 3: Guard Enhancement - Required Points",
                ["Tier3_HeavyHit_RequiredPoints"] = "Tier 3: Heavy Strike - Required Points",
                ["Tier4_PushBack_RequiredPoints"] = "Tier 4: Push Back - Required Points",
                ["Tier5_Tanker_RequiredPoints"] = "Tier 5: Tank - Required Points",
                ["Tier5_PowerBoost_RequiredPoints"] = "Tier 5: Damage Enhancement - Required Points",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: Grandmaster - Required Points",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7: Fury Hammer - Required Points",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7: Guardian Heart - Required Points",

                // === Mace Tree: Mace Expert (3개) ===
                ["Tier0_MaceExpert_DamageIncrease"] = "Tier 0: Mace Expert - Damage Increase (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: Mace Expert - Stun Chance (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: Mace Expert - Stun Duration (sec)",

                // === Mace Tree: Mace Damage Enhancement (1개) ===
                ["Tier1_MaceStep1_DamageBonus"] = "Tier 1: Mace Expert - Mace Damage Bonus (%)",

                // === Mace Tree: Stun Enhancement (2개) ===
                ["Tier2_StunBoost_ChanceBonus"] = "Tier 2: Stun Enhancement - Stun Chance Bonus (%)",
                ["Tier2_StunBoost_DurationBonus"] = "Tier 2: Stun Enhancement - Stun Duration Bonus (sec)",

                // === Mace Tree: Guard Enhancement (1개) ===
                ["Tier3_DefenseBoost_DefenseBonus"] = "Tier 3: Guard Enhancement - Armor Bonus (Fixed Value)",

                // === Mace Tree: Heavy Strike (1개) ===
                ["Tier3_HeavyHit_DamageBonus"] = "Tier 3: Heavy Strike - Damage Bonus (Fixed Value)",

                // === Mace Tree: Push Back (1개) ===
                ["Tier4_PushBack_KnockbackChance"] = "Tier 4: Push Back - Knockback Chance (%)",

                // === Mace Tree: Tank (2개) ===
                ["Tier5_Tanker_HealthBonus"] = "Tier 5: Tank - Health Bonus (%)",
                ["Tier5_Tanker_DamageReduction"] = "Tier 5: Tank - Damage Reduction (%)",

                // === Mace Tree: Damage Enhancement (2개) ===
                ["Tier5_PowerBoost_DamageBonus"] = "Tier 5: Damage Enhancement - Damage Bonus (%)",
                ["Tier5_PowerBoost_AttackSpeedBonus"] = "Tier 5: Damage Enhancement - Attack Speed Bonus (%)",

                // === Mace Tree: Grandmaster (1개) ===
                ["Tier6_Grandmaster_DefenseBonus"] = "Tier 6: Grandmaster - Armor Bonus (%)",

                // === Mace Tree: Fury Hammer (6개) ===
                ["Tier7_FuryHammer_DamageMultiplier"] = "Tier 7: Fury Hammer - Damage Multiplier (%)",
                ["Tier7_FuryHammer_AttackSpeedBonus"] = "Tier 7: Fury Hammer - Attack Speed Bonus (%)",
                ["Tier7_FuryHammer_PullForce"] = "Tier 7: Fury Hammer - Gravity Pull Force",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7: Fury Hammer - Stamina Cost",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7: Fury Hammer - Cooldown (sec)",
                ["Tier7_FuryHammer_AOERange"] = "Tier 7: Fury Hammer - AOE Radius (m)",

                // === Mace Tree: Guardian Heart (4개) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7: Guardian Heart - Cooldown (sec)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7: Guardian Heart - Stamina Cost",
                ["Tier7_GuardianHeart_BuffDuration"] = "Tier 7: Guardian Heart - Buff Duration (sec)",
                ["Tier7_GuardianHeart_ReflectDamageRatio"] = "Tier 7: Guardian Heart - Reflect Damage Percent (%)",

                // === Knife Tree: Required Points (9개) ===
                ["Knife_Expert_RequiredPoints"] = "Tier 0: Dagger Expert - Required Points",
                ["Knife_Evasion_RequiredPoints"] = "Tier 1: Evasion Mastery - Required Points",
                ["Knife_FastMove_RequiredPoints"] = "Tier 2: Swift Movement - Required Points",
                ["Knife_CombatMastery_RequiredPoints"] = "Tier 3: Combat Mastery - Required Points",
                ["Knife_AttackEvasion_RequiredPoints"] = "Tier 4: Attack & Evasion - Required Points",
                ["Knife_CriticalDamage_RequiredPoints"] = "Tier 5: Lethal Damage - Required Points",
                ["Knife_Assassin_RequiredPoints"] = "Tier 6: Assassin - Required Points",
                ["Knife_AssassinSkill_RequiredPoints"] = "Tier 7: Assassination Art - Required Points",
                ["Knife_AssassinHeart_RequiredPoints"] = "Tier 8: Assassin's Heart - Required Points",

                // === Knife Tree: Dagger Expert (1개) ===
                ["Knife_Expert_BackstabDamageBonus"] = "Tier 0: Dagger Expert - Backstab Damage Bonus (%)",

                // === Knife Tree: Evasion Mastery (2개) ===
                ["Knife_Evasion_Chance"] = "Tier 1: Evasion Mastery - Evasion Chance (%)",
                ["Knife_Evasion_InvincibilityDuration"] = "Tier 1: Evasion Mastery - Invincibility Duration After Dodge (sec)",

                // === Knife Tree: Swift Movement (1개) ===
                ["Knife_FastMove_MovementSpeedIncrease"] = "Tier 2: Swift Movement - Move Speed Increase (%)",

                // === Knife Tree: Combat Mastery (2개) ===
                ["Knife_CombatMastery_DamageIncrease"] = "Tier 3: Combat Mastery - Dagger Damage Increase On Kill (Fixed Value)",
                ["Knife_CombatMastery_EffectDuration"] = "Tier 3: Combat Mastery - Effect Duration (sec)",

                // === Knife Tree: Attack & Evasion (3개) ===
                ["Knife_AttackEvasion_EvasionRateIncrease"] = "Tier 4: Attack & Evasion - Evasion Rate Increase on 2-Hit Combo (%)",
                ["Knife_AttackEvasion_EffectDuration"] = "Tier 4: Attack & Evasion - Effect Duration (sec)",
                ["Knife_AttackEvasion_Cooldown"] = "Tier 4: Attack & Evasion - Cooldown (sec)",

                // === Knife Tree: Lethal Damage (1개) ===
                ["Knife_CriticalDamage_DamageIncrease"] = "Tier 5: Lethal Damage - Damage Increase (%)",

                // === Knife Tree: Assassin (2개) ===
                ["Knife_Assassin_CritDamageIncrease"] = "Tier 6: Assassin - Critical Damage Increase (%)",
                ["Knife_Assassin_CritChanceIncrease"] = "Tier 6: Assassin - Critical Chance Increase (%)",

                // === Knife Tree: Assassination Art (2개) ===
                ["Knife_AssassinSkill_StaggerChance"] = "Tier 7: Assassination Art - Stagger Chance on 3-Hit Combo (%)",
                ["Knife_AssassinSkill_RequiredComboHits"] = "Tier 7: Assassination Art - Required Consecutive Hits for Stagger",

                // === Knife Tree: Assassin's Heart (9개) ===
                ["Knife_AssassinHeart_CritDamageMultiplier"] = "Tier 8: Assassin's Heart - Critical Damage Multiplier",
                ["Knife_AssassinHeart_EffectDuration"] = "Tier 8: Assassin's Heart - Effect Duration (sec)",
                ["Knife_AssassinHeart_StaminaCost"] = "Tier 8: Assassin's Heart - Stamina Cost",
                ["Knife_AssassinHeart_Cooldown"] = "Tier 8: Assassin's Heart - Cooldown (sec)",
                ["Knife_AssassinHeart_TeleportSearchRange"] = "Tier 8: Assassin's Heart - Teleport Target Detection Range (m)",
                ["Knife_AssassinHeart_TeleportBackDistance"] = "Tier 8: Assassin's Heart - Distance Behind Target (m)",
                ["Knife_AssassinHeart_StunDuration"] = "Tier 8: Assassin's Heart - Target Stun Duration After Teleport (sec)",
                ["Knife_AssassinHeart_ComboAttackCount"] = "Tier 8: Assassin's Heart - Consecutive Attacks After Teleport",
                ["Knife_AssassinHeart_AttackInterval"] = "Tier 8: Assassin's Heart - Attack Interval (sec)",
            };
        }
    }
}

using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 창 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Spear_Config
    {
        // === 필요 포인트 설정 ===
        public static ConfigEntry<int> SpearExpertRequiredPoints;
        public static ConfigEntry<int> SpearStep1RequiredPoints;
        public static ConfigEntry<int> SpearStep2RequiredPoints;
        public static ConfigEntry<int> SpearStep3RequiredPoints;
        public static ConfigEntry<int> SpearStep4RequiredPoints;
        public static ConfigEntry<int> SpearPenetrateRequiredPoints;
        public static ConfigEntry<int> SpearComboRequiredPoints;

        // === 창 전문가 스킬 설정 ===
        public static ConfigEntry<float> SpearStep1AttackSpeed;
        public static ConfigEntry<float> SpearStep1DamageBonus;
        public static ConfigEntry<float> SpearStep1Duration;
        public static ConfigEntry<float> SpearStep1ThrowCooldown;
        public static ConfigEntry<float> SpearStep1ThrowDamage;
        public static ConfigEntry<float> SpearStep1ThrowBuffDuration;
        public static ConfigEntry<float> SpearStep1CritDamageBonus;
        public static ConfigEntry<float> SpearStep2EvasionDamageBonus;
        public static ConfigEntry<float> SpearStep2EvasionStaminaReduction;
        public static ConfigEntry<float> SpearStep3PierceDamageBonus;
        public static ConfigEntry<float> SpearStep3QuickDamageBonus;
        public static ConfigEntry<float> SpearStep4TripleDamageBonus;
        public static ConfigEntry<float> SpearStep5PenetrateCritChance;

        // === 꿰뚫는 창 (번개 충격) 설정 ===
        public static ConfigEntry<float> SpearStep5PenetrateBuffDuration;
        public static ConfigEntry<float> SpearStep5PenetrateLightningDamage;
        public static ConfigEntry<int> SpearStep5PenetrateComboCount;
        public static ConfigEntry<float> SpearStep5PenetrateCooldown;
        public static ConfigEntry<float> SpearStep5PenetrateStaminaCost;

        public static ConfigEntry<float> SpearStep5ComboCooldown;
        public static ConfigEntry<float> SpearStep5ComboDamage;
        public static ConfigEntry<float> SpearStep5ComboStaminaCost;
        public static ConfigEntry<float> SpearStep5ComboKnockbackRadius;
        public static ConfigEntry<float> SpearStep5ComboRange;
        public static ConfigEntry<float> SpearStep5ComboBuffDuration;
        public static ConfigEntry<int> SpearStep5ComboMaxUses;

        // === 필요 포인트 접근 프로퍼티 ===
        public static int SpearExpertRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_expert_required_points", SpearExpertRequiredPoints?.Value ?? 2);
        public static int SpearStep1RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step1_required_points", SpearStep1RequiredPoints?.Value ?? 2);
        public static int SpearStep2RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step2_required_points", SpearStep2RequiredPoints?.Value ?? 2);
        public static int SpearStep3RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step3_required_points", SpearStep3RequiredPoints?.Value ?? 2);
        public static int SpearStep4RequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_step4_required_points", SpearStep4RequiredPoints?.Value ?? 2);
        public static int SpearPenetrateRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_penetrate_required_points", SpearPenetrateRequiredPoints?.Value ?? 3);
        public static int SpearComboRequiredPointsValue => (int)SkillTreeConfig.GetEffectiveValue("spear_combo_required_points", SpearComboRequiredPoints?.Value ?? 3);

        // === 창 전문가 접근 프로퍼티들 ===
        public static float SpearStep1AttackSpeedValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_attack_speed", SpearStep1AttackSpeed.Value);
        public static float SpearStep1DamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_damage_bonus", SpearStep1DamageBonus.Value);
        public static float SpearStep1DurationValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_duration", SpearStep1Duration.Value);
        public static float SpearStep2ThrowCooldownValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_throw_cooldown", SpearStep1ThrowCooldown.Value);
        public static float SpearStep2ThrowDamageValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_throw_damage", SpearStep1ThrowDamage.Value);
        public static float SpearStep2ThrowBuffDurationValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_throw_buff_duration", SpearStep1ThrowBuffDuration.Value);
        public static float SpearStep2CritDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step1_crit_damage_bonus", SpearStep1CritDamageBonus.Value);
        public static float SpearStep3EvasionDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step2_evasion_damage_bonus", SpearStep2EvasionDamageBonus.Value);
        public static float SpearStep3EvasionStaminaReductionValue => SkillTreeConfig.GetEffectiveValue("spear_Step2_evasion_stamina_reduction", SpearStep2EvasionStaminaReduction.Value);
        public static float SpearStep3PierceDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step3_pierce_damage", SpearStep3PierceDamageBonus.Value);
        public static float SpearStep4QuickDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step3_quick_damage_bonus", SpearStep3QuickDamageBonus.Value);
        public static float SpearStep5TripleDamageBonusValue => SkillTreeConfig.GetEffectiveValue("spear_Step4_triple_damage_bonus", SpearStep4TripleDamageBonus.Value);
        public static float SpearStep6PenetrateCritChanceValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_crit_chance", SpearStep5PenetrateCritChance.Value);

        // === 꿰뚫는 창 (번개 충격) 접근 프로퍼티 ===
        public static float SpearStep6PenetrateBuffDurationValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_buff_duration", SpearStep5PenetrateBuffDuration.Value);
        public static float SpearStep6PenetrateLightningDamageValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_lightning_damage", SpearStep5PenetrateLightningDamage.Value);
        public static int SpearStep6PenetrateComboCountValue => (int)SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_combo_count", SpearStep5PenetrateComboCount.Value);
        public static float SpearStep6PenetrateCooldownValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_cooldown", SpearStep5PenetrateCooldown.Value);
        public static float SpearStep6PenetrateStaminaCostValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_penetrate_stamina_cost", SpearStep5PenetrateStaminaCost.Value);

        public static float SpearStep6ComboCooldownValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_cooldown", SpearStep5ComboCooldown.Value);
        public static float SpearStep6ComboDamageValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_damage", SpearStep5ComboDamage.Value);
        public static float SpearStep6ComboStaminaCostValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_stamina_cost", SpearStep5ComboStaminaCost.Value);
        public static float SpearStep6ComboKnockbackRadiusValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_knockback_radius", SpearStep5ComboKnockbackRadius.Value);
        public static float SpearStep2ThrowRangeValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_range", SpearStep5ComboRange.Value);
        public static float SpearStep2ThrowStaminaCostValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_stamina_cost", SpearStep5ComboStaminaCost.Value);
        public static float SpearStep6ComboBuffDurationValue => SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_buff_duration", SpearStep5ComboBuffDuration.Value);
        public static int SpearStep6ComboMaxUsesValue => (int)SkillTreeConfig.GetEffectiveValue("spear_Step5_combo_max_uses", SpearStep5ComboMaxUses.Value);

        public static void Initialize(ConfigFile config)
        {
            // === 필요 포인트 설정 ===
            SpearExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier0_창전문가_필요포인트", 2,
                "Tier 0: 창 전문가(spear_expert) - 필요 포인트");

            SpearStep1RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_창스킬_필요포인트", 2,
                "Tier 1: 창 스킬 - 필요 포인트");

            SpearStep2RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier2_창스킬_필요포인트", 2,
                "Tier 2: 창 스킬 - 필요 포인트");

            SpearStep3RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier3_창스킬_필요포인트", 2,
                "Tier 3: 창 스킬 - 필요 포인트");

            SpearStep4RequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier4_창스킬_필요포인트", 2,
                "Tier 4: 창 스킬 - 필요 포인트");

            SpearPenetrateRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_꿰뚫는창_필요포인트", 3,
                "Tier 5: 꿰뚫는 창(G키 액티브) - 필요 포인트");

            SpearComboRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_필요포인트", 3,
                "Tier 5: 연공창(H키 액티브) - 필요 포인트");

            SpearStep1AttackSpeed = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_창전문가_2연속공격속도보너스", 10f,
                "Tier 1: 창 전문가(spear_step1_expert) - 2연속 공격 속도 보너스 (%)");

            SpearStep1DamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_창전문가_2연속공격력보너스", 7f,
                "Tier 1: 창 전문가(spear_step1_expert) - 2연속 공격력 보너스 (%)");

            SpearStep1Duration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_창전문가_효과지속시간", 4f,
                "Tier 1: 창 전문가(spear_step1_expert) - 효과 지속시간 (초)");

            SpearStep1ThrowCooldown = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_투창전문가_투창쿨타임", 30f,
                "Tier 1: 투창 전문가(spear_step1_throw) - 투창 쿨타임 (초)");

            SpearStep1ThrowDamage = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_투창전문가_투창데미지배율", 120f,
                "Tier 1: 투창 전문가(spear_step1_throw) - 투창 데미지 배율 (%)");

            SpearStep1ThrowBuffDuration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_투창전문가_버프지속시간_사용안함", 15f,
                "Tier 1: 투창 전문가(spear_step1_throw) - 사용 안 함 (패시브 스킬로 변경됨)");

            SpearStep1CritDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier1_급소찌르기_창공격력보너스", 20f,
                "Tier 1: 급소 찌르기(spear_step1_crit) - 창 공격력 보너스 (%)");

            SpearStep3PierceDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier3_연격창_무기공격력보너스", 4f,
                "Tier 3: 연격창(spear_Step3_pierce) - 무기 공격력 보너스 (고정 수치)");

            SpearStep2EvasionDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier4_회피찌르기_구르기직후피해보너스", 25f,
                "Tier 4: 회피 찌르기(spear_step2_evasion) - 구르기 직후 공격 피해 보너스 (%)");

            SpearStep2EvasionStaminaReduction = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier4_회피찌르기_공격스태미나감소", 8f,
                "Tier 4: 회피 찌르기(spear_step2_evasion) - 공격 스태미나 감소 (%)");

            SpearStep3QuickDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier3_쾌속창_투창공격력보너스", 40f,
                "Tier 3: 쾌속 창(spear_step3_quick) - 투창 공격력 보너스 (%)");

            SpearStep4TripleDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier4_삼연창_3연속공격공격력보너스", 20f,
                "Tier 4: 삼연창(spear_step4_triple) - 3연속 공격 공격력 보너스 (%)");

            SpearStep5PenetrateCritChance = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_꿰뚫는창_치명타확률_미사용", 12f,
                "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 미사용 (번개 충격으로 변경됨)");

            SpearStep5PenetrateBuffDuration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_꿰뚫는창_버프지속시간", 30f,
                "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 버프 지속시간 (초)");

            SpearStep5PenetrateLightningDamage = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_꿰뚫는창_번개데미지배율", 260f,
                "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 번개 충격 데미지 배율 (%)");

            SpearStep5PenetrateComboCount = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_꿰뚫는창_연속적중횟수", 3,
                "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - 번개 발동 연속 적중 횟수");

            SpearStep5PenetrateCooldown = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_꿰뚫는창_G키쿨타임", 60f,
                "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - G키 액티브 쿨타임 (초)");

            SpearStep5PenetrateStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_꿰뚫는창_G키스태미나소모", 25f,
                "Tier 5: 꿰뚫는 창(spear_step5_penetrate) - G키 액티브 스태미나 소모 (%)");

            SpearStep5ComboCooldown = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_G키쿨타임", 25f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 쿨타임 (초)");

            SpearStep5ComboDamage = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_G키데미지배율", 280f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 데미지 배율 (%)");

            SpearStep5ComboStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_G키스태미나소모", 20f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 스태미나 소모 (%)");

            SpearStep5ComboKnockbackRadius = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_G키넉백범위", 3f,
                "Tier 5: 연공창(spear_step5_combo_active) - G키 액티브 넉백 범위 (m)");

            SpearStep5ComboRange = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_액티브효과범위", 3f,
                "Tier 5: 연공창(spear_step5_combo_active) - 액티브 효과 범위 (m)");

            SpearStep5ComboBuffDuration = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_버프지속시간", 30f,
                "Tier 5: 연공창(spear_step5_combo) - H키 버프 지속시간 (초)");

            SpearStep5ComboMaxUses = SkillTreeConfig.BindServerSync(config,
                "Spear Tree", "Tier5_연공창_최대사용횟수", 3,
                "Tier 5: 연공창(spear_step5_combo) - 버프 중 최대 강화 투창 횟수");

            Plugin.Log.LogDebug("[Spear_Config] 창 전문가 트리 설정 초기화 완료");
        }
    }
}

using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 활 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Bow_Config
    {
        // === 활 전문가 멀티샷 패시브 스킬 설정 ===
        public static ConfigEntry<float> BowMultishotLv1Chance;
        public static ConfigEntry<float> BowMultishotLv2Chance;
        public static ConfigEntry<int> BowMultishotArrowCount;
        public static ConfigEntry<int> BowMultishotArrowConsumption;
        public static ConfigEntry<float> BowMultishotDamagePercent;

        // === 활 전문가 스킬 설정 ===
        public static ConfigEntry<float> BowStep1ExpertDamageBonus;
        public static ConfigEntry<float> BowStep2FocusCritBonus;
        public static ConfigEntry<float> BowStep3SpeedShotSkillBonus;
        public static ConfigEntry<float> BowStep3SilentShotDamageBonus;
        public static ConfigEntry<float> BowStep3SpecialArrowChance;
        public static ConfigEntry<float> BowStep4PowerShotKnockbackChance;
        public static ConfigEntry<float> BowStep4PowerShotKnockbackPower;
        public static ConfigEntry<float> BowStep5InstinctCritBonus;
        public static ConfigEntry<float> BowStep5MasterCritDamage;
        public static ConfigEntry<float> BowStep5ArrowRainChance;
        public static ConfigEntry<int> BowStep5ArrowRainCount;
        public static ConfigEntry<float> BowStep5BackstepShotCritBonus;
        public static ConfigEntry<float> BowStep5BackstepShotWindow;
        public static ConfigEntry<float> BowStep6CritBoostDamageBonus;
        public static ConfigEntry<float> BowStep6CritBoostCritChance;
        public static ConfigEntry<int> BowStep6CritBoostArrowCount;
        public static ConfigEntry<float> BowStep6CritBoostCooldown;
        public static ConfigEntry<float> BowStep6CritBoostStaminaCost;
        public static ConfigEntry<float> BowExplosiveArrowDamage;
        public static ConfigEntry<float> BowExplosiveArrowCooldown;
        public static ConfigEntry<float> BowExplosiveArrowStaminaCost;
        public static ConfigEntry<float> BowExplosiveArrowRadius;

        // === 활 전문가 접근 프로퍼티들 ===
        public static float BowMultishotLv1ChanceValue => SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_Lv1_Chance", BowMultishotLv1Chance.Value);
        public static float BowMultishotLv2ChanceValue => SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_Lv2_Chance", BowMultishotLv2Chance.Value);
        public static int BowMultishotArrowCountValue => (int)SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_ArrowCount", BowMultishotArrowCount.Value);
        public static int BowMultishotArrowConsumptionValue => (int)SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_ArrowConsumption", BowMultishotArrowConsumption.Value);
        public static float BowMultishotDamagePercentValue => SkillTreeConfig.GetEffectiveValue("Bow_MultiShot_DamagePercent", BowMultishotDamagePercent.Value);
        public static float BowStep1ExpertDamageBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step1_expert_damage_bonus", BowStep1ExpertDamageBonus.Value);
        public static float BowStep2FocusCritBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step2_focus_crit_bonus", BowStep2FocusCritBonus.Value);
        public static float BowStep3SpeedShotSkillBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step3_speedshot_skill_bonus", BowStep3SpeedShotSkillBonus.Value);
        public static float BowStep3SilentShotDamageBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step3_silentshot_damage_bonus", BowStep3SilentShotDamageBonus.Value);
        public static float BowStep3SpecialArrowChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step3_special_arrow_chance", BowStep3SpecialArrowChance.Value);
        public static float BowStep4PowerShotKnockbackChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step4_powershot_knockback_chance", BowStep4PowerShotKnockbackChance.Value);
        public static float BowStep4PowerShotKnockbackPowerValue => SkillTreeConfig.GetEffectiveValue("bow_Step4_powershot_knockback_power", BowStep4PowerShotKnockbackPower.Value);
        public static float BowStep5ArrowRainChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_arrow_rain_chance", BowStep5ArrowRainChance.Value);
        public static int BowStep5ArrowRainCountValue => (int)SkillTreeConfig.GetEffectiveValue("bow_Step5_arrow_rain_count", (float)BowStep5ArrowRainCount.Value);
        public static float BowStep5BackstepShotCritBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_backstep_shot_crit_bonus", BowStep5BackstepShotCritBonus.Value);
        public static float BowStep5BackstepShotWindowValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_backstep_shot_window", BowStep5BackstepShotWindow.Value);
        public static float BowStep5InstinctCritBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_instinct_crit_bonus", BowStep5InstinctCritBonus.Value);
        public static float BowStep5MasterCritDamageValue => SkillTreeConfig.GetEffectiveValue("bow_Step5_master_crit_damage", BowStep5MasterCritDamage.Value);
        public static float BowStep6CritBoostDamageBonusValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_damage_bonus", BowStep6CritBoostDamageBonus.Value);
        public static float BowStep6CritBoostCritChanceValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_crit_chance", BowStep6CritBoostCritChance.Value);
        public static int BowStep6CritBoostArrowCountValue => (int)SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_arrow_count", (float)BowStep6CritBoostArrowCount.Value);
        public static float BowStep6CritBoostCooldownValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_cooldown", BowStep6CritBoostCooldown.Value);
        public static float BowStep6CritBoostStaminaCostValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_critboost_stamina_cost", BowStep6CritBoostStaminaCost.Value);
        public static float BowExplosiveArrowDamageValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_damage", BowExplosiveArrowDamage.Value);
        public static float BowExplosiveArrowCooldownValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_cooldown", BowExplosiveArrowCooldown.Value);
        public static float BowExplosiveArrowStaminaCostValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_stamina_cost", BowExplosiveArrowStaminaCost.Value);
        public static float BowExplosiveArrowRadiusValue => SkillTreeConfig.GetEffectiveValue("bow_Step6_explosive_radius", BowExplosiveArrowRadius.Value);

        public static void Initialize(ConfigFile config)
        {
            // === Bow Tree: 멀티샷 패시브 ===
            BowMultishotLv1Chance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier_멀티샷Lv1_발동확률", 15f,
                "활 전문가: 멀티샷 Lv1(bow_multishot_lv1) - 발동 확률 (%)");

            BowMultishotLv2Chance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier_멀티샷Lv2_발동확률", 36f,
                "활 전문가: 멀티샷 Lv2(bow_multishot_lv2) - 발동 확률 (%)");

            BowMultishotArrowCount = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier_멀티샷_추가화살수", 2,
                "활 전문가: 멀티샷 - 추가 발사 화살 수");

            BowMultishotArrowConsumption = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier_멀티샷_화살소모량", 0,
                "활 전문가: 멀티샷 - 화살 소모량");

            BowMultishotDamagePercent = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier_멀티샷_화살데미지비율", 70f,
                "활 전문가: 멀티샷 - 화살당 데미지 비율 (%)");

            // === Bow Tree: 공격 스킬 ===
            BowStep1ExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier1_활전문가_활공격력보너스", 5f,
                "Tier 1: 활 전문가(bow_Step1_damage) - 활 공격력 보너스 (%)");

            BowStep2FocusCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier2_집중사격_치명타확률보너스", 7f,
                "Tier 2: 집중 사격(bow_step2_focus) - 치명타 확률 보너스 (%)");

            BowStep3SpeedShotSkillBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_활숙련_활기술숙련도보너스", 10f,
                "Tier 3: 활 숙련(bow_step3_speedshot) - 활 기술(숙련도) 보너스");

            BowStep3SilentShotDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_기본활공격_공격력증가", 3f,
                "Tier 3: 기본 활공격(bow_step3_silentshot) - 활 공격력 증가 (고정값)");

            BowStep3SpecialArrowChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier3_특수화살_발사확률", 30f,
                "Tier 3: 특수 화살(bow_step3_special_arrow) - 특수 화살 발사 확률 (%)");

            BowStep4PowerShotKnockbackChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier4_강력한한발_강한넉백확률", 35f,
                "Tier 4: 강력한 한 발(bow_step4_powershot) - 강한 넉백 확률 (%)");

            BowStep4PowerShotKnockbackPower = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier4_강력한한발_넉백거리", 5f,
                "Tier 4: 강력한 한 발(bow_step4_powershot) - 넉백 거리 (m)");

            BowStep5ArrowRainChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_화살비_화살3개발사확률", 29f,
                "Tier 5: 화살비(bow_step5_arrow_rain) - 화살 3개 발사 확률 (%)");

            BowStep5ArrowRainCount = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_화살비_발사할화살개수", 3,
                "Tier 5: 화살비(bow_step5_arrow_rain) - 발사할 화살 개수");

            BowStep5BackstepShotCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_백스텝샷_구르기후치명타확률보너스", 25f,
                "Tier 5: 백스텝 샷(bow_step5_backstep_shot) - 구르기 후 치명타 확률 보너스 (%)");

            BowStep5BackstepShotWindow = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_백스텝샷_구르기후효과지속시간", 3f,
                "Tier 5: 백스텝 샷(bow_step5_backstep_shot) - 구르기 후 효과 지속시간 (초)");

            BowStep5InstinctCritBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_사냥본능_치명타확률보너스", 10f,
                "Tier 5: 사냥 본능(bow_step5_instinct) - 치명타 확률 보너스 (%)");

            BowStep5MasterCritDamage = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier5_정조준_크리티컬데미지보너스", 30f,
                "Tier 5: 정조준(bow_step5_master) - 크리티컬 데미지 보너스 (%)");

            BowStep6CritBoostDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_크리티컬부스트_T키액티브데미지보너스", 50f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 데미지 보너스 (%)");

            BowStep6CritBoostCritChance = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_크리티컬부스트_T키액티브치명타확률", 100f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 치명타 확률 (%)");

            BowStep6CritBoostArrowCount = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_크리티컬부스트_T키액티브화살개수", 5,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 화살 개수");

            BowStep6CritBoostCooldown = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_크리티컬부스트_T키액티브쿨타임", 45f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 쿨타임 (초)");

            BowStep6CritBoostStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_크리티컬부스트_T키액티브스태미나소모", 25f,
                "Tier 6: 크리티컬 부스트(bow_step6_critboost) - T키 액티브 스킬 스태미나 소모 (%)");

            BowExplosiveArrowDamage = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_폭발화살_T키액티브데미지배율", 120f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - T키 액티브 스킬 데미지 배율 (%)");

            BowExplosiveArrowCooldown = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_폭발화살_T키액티브쿨타임", 20f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - T키 액티브 스킬 쿨타임 (초)");

            BowExplosiveArrowStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_폭발화살_T키액티브스태미나소모", 15f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - T키 액티브 스킬 스태미나 소모 (%)");

            BowExplosiveArrowRadius = SkillTreeConfig.BindServerSync(config,
                "Bow Tree", "Tier6_폭발화살_폭발범위", 4f,
                "Tier 6: 폭발 화살(bow_step6_explosive) - 폭발 범위 (m)");

            Plugin.Log.LogDebug("[Bow_Config] 활 전문가 트리 설정 초기화 완료");
        }
    }
}

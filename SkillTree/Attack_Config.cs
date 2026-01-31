using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 공격 전문가 스킬트리 Config 설정
    /// </summary>
    public static class Attack_Config
    {
        // === 공격 전문가 노드 설정 ===
        // 루트 (공격 전문가)
        public static ConfigEntry<float> AttackRootDamageBonus;

        // 2단계 (4가지)
        public static ConfigEntry<float> AttackMeleeBonusChance;
        public static ConfigEntry<float> AttackMeleeBonusDamage;
        public static ConfigEntry<float> AttackBowBonusChance;
        public static ConfigEntry<float> AttackBowBonusDamage;
        public static ConfigEntry<float> AttackCrossbowBonusChance;
        public static ConfigEntry<float> AttackCrossbowBonusDamage;
        public static ConfigEntry<float> AttackStaffBonusChance;
        public static ConfigEntry<float> AttackStaffBonusDamage;

        // 3단계 (기본 공격)
        public static ConfigEntry<float> AttackBasePhysicalDamage;
        public static ConfigEntry<float> AttackBaseElementalDamage;
        public static ConfigEntry<float> AttackStatBonus;
        public static ConfigEntry<float> AttackTwoHandDrainPhysicalDamage;
        public static ConfigEntry<float> AttackTwoHandDrainElementalDamage;

        // 4단계 (3가지)
        public static ConfigEntry<float> AttackCritChance;
        public static ConfigEntry<float> AttackMeleeEnhancement;
        public static ConfigEntry<float> AttackRangedEnhancement;

        // 5단계 (특수화 스탯)
        public static ConfigEntry<float> AttackSpecialStat;

        // 6단계 (4가지)
        public static ConfigEntry<float> AttackCritDamageBonus;
        public static ConfigEntry<float> AttackTwoHandedBonus;
        public static ConfigEntry<float> AttackStaffElemental;
        public static ConfigEntry<float> AttackFinisherMeleeBonus;

        // === 공격 전문가 접근 프로퍼티들 ===
        public static float AttackRootDamageBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Expert_Damage", AttackRootDamageBonus.Value);
        public static float AttackMeleeBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_MeleeBonusChance", AttackMeleeBonusChance.Value);
        public static float AttackMeleeBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_MeleeBonusDamage", AttackMeleeBonusDamage.Value);
        public static float AttackBowBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_BowBonusChance", AttackBowBonusChance.Value);
        public static float AttackBowBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_BowBonusDamage", AttackBowBonusDamage.Value);
        public static float AttackCrossbowBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_CrossbowBonusChance", AttackCrossbowBonusChance.Value);
        public static float AttackCrossbowBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_CrossbowBonusDamage", AttackCrossbowBonusDamage.Value);
        public static float AttackStaffBonusChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_StaffBonusChance", AttackStaffBonusChance.Value);
        public static float AttackStaffBonusDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step2_StaffBonusDamage", AttackStaffBonusDamage.Value);
        public static float AttackBasePhysicalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_PhysicalDamage", AttackBasePhysicalDamage.Value);
        public static float AttackBaseElementalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_ElementalDamage", AttackBaseElementalDamage.Value);
        public static float AttackStatBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_StatBonus", AttackStatBonus.Value);
        public static float AttackTwoHandDrainPhysicalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_TwoHandPhysical", AttackTwoHandDrainPhysicalDamage.Value);
        public static float AttackTwoHandDrainElementalDamageValue => SkillTreeConfig.GetEffectiveValue("Attack_Step3_TwoHandElemental", AttackTwoHandDrainElementalDamage.Value);
        public static float AttackCritChanceValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_CritChance", AttackCritChance.Value);
        public static float AttackMeleeEnhancementValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_MeleeEnhancement", AttackMeleeEnhancement.Value);
        public static float AttackRangedEnhancementValue => SkillTreeConfig.GetEffectiveValue("Attack_Step4_RangedEnhancement", AttackRangedEnhancement.Value);
        public static float AttackSpecialStatValue => SkillTreeConfig.GetEffectiveValue("Attack_Step5_SpecialStat", AttackSpecialStat.Value);
        public static float AttackCritDamageBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_CritDamageBonus", AttackCritDamageBonus.Value);
        public static float AttackTwoHandedBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_TwoHandedBonus", AttackTwoHandedBonus.Value);
        public static float AttackStaffElementalValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_StaffElemental", AttackStaffElemental.Value);
        public static float AttackFinisherMeleeBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_Step6_FinisherMeleeBonus", AttackFinisherMeleeBonus.Value);

        // 레거시 호환 프로퍼티
        public static float AttackOneHandedBonusValue => SkillTreeConfig.GetEffectiveValue("Attack_OneHandedBonus", 5f);

        public static void Initialize(ConfigFile config)
        {
            // === Tier 0: 공격 전문가 ===
            AttackRootDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier0_공격전문가_모든데미지보너스", 10f,
                "[서버동기화] Tier 0: 공격 전문가(attack_root) - 모든 데미지 보너스 (%)");

            // === Tier 2: 무기 특화 ===
            AttackMeleeBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_근접특화_추가피해발동확률", 20f,
                "Tier 2: 근접 특화(attack_step2_melee) - 추가 피해 발동 확률 (%)");

            AttackMeleeBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_근접특화_근접공격력", 10f,
                "Tier 2: 근접 특화(attack_step2_melee) - 근접 공격력 (%)");

            AttackBowBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_활특화_추가피해발동확률", 20f,
                "Tier 2: 활 특화(attack_step2_bow) - 추가 피해 발동 확률 (%)");

            AttackBowBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_활특화_활공격력", 8f,
                "Tier 2: 활 특화(attack_step2_bow) - 활 공격력 (%)");

            AttackCrossbowBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_석궁특화_강화발동확률", 15f,
                "Tier 2: 석궁 특화(attack_step2_crossbow) - 강화 발동 확률 (%)");

            AttackCrossbowBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_석궁특화_석궁공격력", 9f,
                "Tier 2: 석궁 특화(attack_step2_crossbow) - 석궁 공격력 (%)");

            AttackStaffBonusChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_지팡이특화_속성피해발동확률", 20f,
                "Tier 2: 지팡이 특화(attack_step2_staff) - 속성 피해 발동 확률 (%)");

            AttackStaffBonusDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier2_지팡이특화_지팡이공격력", 8f,
                "Tier 2: 지팡이 특화(attack_step2_staff) - 지팡이 공격력 (%)");

            // === Tier 3: 기본 공격 ===
            AttackBasePhysicalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_기본공격_물리공격력보너스", 2f,
                "Tier 3: 기본 공격(attack_step3_base) - 물리 공격력 보너스");

            AttackBaseElementalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_기본공격_속성공격력보너스", 2f,
                "Tier 3: 기본 공격(attack_step3_base) - 속성 공격력 보너스");

            AttackStatBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_공격증가_힘지능보너스", 5f,
                "Tier 3: 공격 증가(atk_twohand_drain) - 힘과 지능 보너스");

            AttackTwoHandDrainPhysicalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_공격증가_물리공격력보너스", 10f,
                "Tier 3: 공격 증가(atk_twohand_drain) - 물리 공격력 보너스");

            AttackTwoHandDrainElementalDamage = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier3_공격증가_속성공격력보너스", 10f,
                "Tier 3: 공격 증가(atk_twohand_drain) - 속성 공격력 보너스");

            // === Tier 4: 전투 강화 ===
            AttackCritChance = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_정밀공격_치명타확률", 5f,
                "Tier 4: 정밀 공격(attack_step4_crit) - 치명타 확률 (%)");

            AttackMeleeEnhancement = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_근접강화_2연속공격추가피해", 10f,
                "Tier 4: 근접 강화(attack_step4_melee_enhance) - 2연속 공격 추가 피해 (%)");

            AttackRangedEnhancement = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier4_원거리강화_원거리무기공격력", 5f,
                "Tier 4: 원거리 강화(attack_step4_ranged_enhance) - 원거리 무기 공격력 (%)");

            // === Tier 5: 특수화 스탯 ===
            AttackSpecialStat = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier5_특수화스탯_특수화보너스", 5f,
                "Tier 5: 특수화 스탯(attack_step5_special) - 특수화 보너스");

            // === Tier 6: 최종 강화 ===
            AttackCritDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_약점공격_치명타피해보너스", 12f,
                "Tier 6: 약점 공격(attack_step6_crit_damage) - 치명타 피해 보너스 (%)");

            AttackTwoHandedBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_양손분쇄_양손무기공격력보너스", 10f,
                "Tier 6: 양손 분쇄(attack_step6_twohanded) - 양손 무기 공격력 보너스 (%)");

            AttackStaffElemental = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_속성공격_속성공격보너스", 10f,
                "Tier 6: 속성 공격(attack_step6_elemental) - 속성 공격 보너스 (활, 지팡이) (%)");

            AttackFinisherMeleeBonus = SkillTreeConfig.BindServerSync(config,
                "Attack Tree", "Tier6_연속근접의대가_3연속공격보너스", 15f,
                "Tier 6: 연속 근접의 대가(attack_step6_finisher) - 3연속 공격 보너스 (%)");

            Plugin.Log.LogDebug("[Attack_Config] 공격 전문가 트리 설정 초기화 완료");
        }
    }
}

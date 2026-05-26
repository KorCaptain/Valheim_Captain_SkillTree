using BepInEx.Configuration;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Rogue 직업 전용 컨피그 시스템 (Lv1~5)
    /// 그림자 일격: 연속 독 폭발 (레벨별 성장)
    /// 은신 기능 제거됨
    /// </summary>
    public static class Rogue_Config
    {
        // === 그림자 일격 Lv1 기본 설정 ===
        public static ConfigEntry<float> RogueShadowStrikeCooldown;
        public static ConfigEntry<float> RogueShadowStrikeStaminaCost;
        public static ConfigEntry<float> RogueShadowStrikeAttackBonus;
        public static ConfigEntry<float> RogueShadowStrikeBuffDuration;

        // === 그림자 일격 Lv2~5 쿨다운 ===
        public static ConfigEntry<float> RogueLv2Cooldown;
        public static ConfigEntry<float> RogueLv3Cooldown;
        public static ConfigEntry<float> RogueLv4Cooldown;
        public static ConfigEntry<float> RogueLv5Cooldown;

        // === 그림자 일격 Lv2~5 공격력 버프 ===
        public static ConfigEntry<float> RogueLv2AttackBonus;
        public static ConfigEntry<float> RogueLv3AttackBonus;
        public static ConfigEntry<float> RogueLv4AttackBonus;
        public static ConfigEntry<float> RogueLv5AttackBonus;

        // === 그림자 일격 Lv2~5 버프 지속시간 ===
        public static ConfigEntry<float> RogueLv2BuffDuration;
        public static ConfigEntry<float> RogueLv3BuffDuration;
        public static ConfigEntry<float> RogueLv4BuffDuration;
        public static ConfigEntry<float> RogueLv5BuffDuration;

        // === 독 폭발 Lv1 기본 설정 ===
        public static ConfigEntry<float> RoguePoisonRange;
        public static ConfigEntry<float> RoguePoisonInstantDamage;
        public static ConfigEntry<float> RoguePoisonDotDamage;
        public static ConfigEntry<float> RoguePoisonDotDuration;
        public static ConfigEntry<float> RoguePoisonVFXCount;
        public static ConfigEntry<float> RoguePoisonVFXInterval;

        // === 독 폭발 Lv2~5 폭발 횟수 ===
        public static ConfigEntry<float> RogueLv2PoisonBlasts;
        public static ConfigEntry<float> RogueLv3PoisonBlasts;
        public static ConfigEntry<float> RogueLv4PoisonBlasts;
        public static ConfigEntry<float> RogueLv5PoisonBlasts;

        // === 독 즉시 데미지 Lv2~5 ===
        public static ConfigEntry<float> RogueLv2PoisonInstant;
        public static ConfigEntry<float> RogueLv3PoisonInstant;
        public static ConfigEntry<float> RogueLv4PoisonInstant;
        public static ConfigEntry<float> RogueLv5PoisonInstant;

        // === 독 DoT 데미지 Lv2~5 ===
        public static ConfigEntry<float> RogueLv2PoisonDot;
        public static ConfigEntry<float> RogueLv3PoisonDot;
        public static ConfigEntry<float> RogueLv4PoisonDot;
        public static ConfigEntry<float> RogueLv5PoisonDot;

        // === 충전 시스템 ===
        public static ConfigEntry<float> RogueShadowStrikeCharges;
        public static ConfigEntry<float> RogueLv5BonusCharges;

        // === 로그 패시브 Lv1 기본 ===
        public static ConfigEntry<float> RogueAttackSpeedBonus;
        public static ConfigEntry<float> RogueStaminaReduction;

        // === 패시브 Lv2~5 공격속도 ===
        public static ConfigEntry<float> RogueLv2AttackSpeed;
        public static ConfigEntry<float> RogueLv3AttackSpeed;
        public static ConfigEntry<float> RogueLv4AttackSpeed;
        public static ConfigEntry<float> RogueLv5AttackSpeed;

        // === 패시브 Lv2~5 스태미나 감소 ===
        public static ConfigEntry<float> RogueLv2StaminaReduction;
        public static ConfigEntry<float> RogueLv3StaminaReduction;
        public static ConfigEntry<float> RogueLv4StaminaReduction;
        public static ConfigEntry<float> RogueLv5StaminaReduction;

        // === 패시브 회피율 Lv1~5 ===
        public static ConfigEntry<float> RogueLv1DodgeChance;
        public static ConfigEntry<float> RogueLv2DodgeChance;
        public static ConfigEntry<float> RogueLv3DodgeChance;
        public static ConfigEntry<float> RogueLv4DodgeChance;
        public static ConfigEntry<float> RogueLv5DodgeChance;

        // === 패시브 이동속도 Lv1~5 ===
        public static ConfigEntry<float> RogueLv1MoveSpeed;
        public static ConfigEntry<float> RogueLv2MoveSpeed;
        public static ConfigEntry<float> RogueLv3MoveSpeed;
        public static ConfigEntry<float> RogueLv4MoveSpeed;
        public static ConfigEntry<float> RogueLv5MoveSpeed;

        // ====================================================
        // 동적 값 접근자 (Lv1 기본)
        // ====================================================
        public static float RogueShadowStrikeCooldownValue        => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_Cooldown",      RogueShadowStrikeCooldown?.Value      ?? 35f);
        public static float RogueShadowStrikeStaminaCostValue     => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_StaminaCost",   RogueShadowStrikeStaminaCost?.Value   ?? 25f);
        public static float RogueShadowStrikeAttackBonusValue     => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_AttackBonus",   RogueShadowStrikeAttackBonus?.Value   ?? 30f);
        public static float RogueShadowStrikeBuffDurationValue    => SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_BuffDuration",  RogueShadowStrikeBuffDuration?.Value  ?? 8f);

        // Lv2~5 쿨다운
        public static float RogueLv2CooldownValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_Cooldown", RogueLv2Cooldown?.Value ?? 34f);
        public static float RogueLv3CooldownValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_Cooldown", RogueLv3Cooldown?.Value ?? 33f);
        public static float RogueLv4CooldownValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_Cooldown", RogueLv4Cooldown?.Value ?? 32f);
        public static float RogueLv5CooldownValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_Cooldown", RogueLv5Cooldown?.Value ?? 30f);

        // Lv2~5 공격력 버프
        public static float RogueLv2AttackBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_AttackBonus", RogueLv2AttackBonus?.Value ?? 40f);
        public static float RogueLv3AttackBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_AttackBonus", RogueLv3AttackBonus?.Value ?? 45f);
        public static float RogueLv4AttackBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_AttackBonus", RogueLv4AttackBonus?.Value ?? 50f);
        public static float RogueLv5AttackBonusValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_AttackBonus", RogueLv5AttackBonus?.Value ?? 55f);

        // Lv2~5 버프 지속시간
        public static float RogueLv2BuffDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_BuffDuration", RogueLv2BuffDuration?.Value ?? 9f);
        public static float RogueLv3BuffDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_BuffDuration", RogueLv3BuffDuration?.Value ?? 10f);
        public static float RogueLv4BuffDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_BuffDuration", RogueLv4BuffDuration?.Value ?? 11f);
        public static float RogueLv5BuffDurationValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_BuffDuration", RogueLv5BuffDuration?.Value ?? 12f);

        // 독 폭발 Lv1 기본
        public static float RoguePoisonRangeValue        => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_Range",        RoguePoisonRange?.Value        ?? 15f);
        public static float RoguePoisonInstantDamageValue => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_InstantDamage", RoguePoisonInstantDamage?.Value ?? 40f);
        public static float RoguePoisonDotDamageValue    => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_DotDamage",    RoguePoisonDotDamage?.Value    ?? 30f);
        public static float RoguePoisonDotDurationValue  => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_DotDuration",  RoguePoisonDotDuration?.Value  ?? 10f);
        public static int   RoguePoisonVFXCountValue     => (int)SkillTreeConfig.GetEffectiveValue("Rogue_Poison_VFXCount", RoguePoisonVFXCount?.Value      ?? 6f);
        public static float RoguePoisonVFXIntervalValue  => SkillTreeConfig.GetEffectiveValue("Rogue_Poison_VFXInterval",  RoguePoisonVFXInterval?.Value  ?? 0.5f);

        // 독 폭발 횟수 Lv2~5
        public static int RogueLv2PoisonBlastsValue => (int)SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_PoisonBlasts", RogueLv2PoisonBlasts?.Value ?? 7f);
        public static int RogueLv3PoisonBlastsValue => (int)SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_PoisonBlasts", RogueLv3PoisonBlasts?.Value ?? 8f);
        public static int RogueLv4PoisonBlastsValue => (int)SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_PoisonBlasts", RogueLv4PoisonBlasts?.Value ?? 9f);
        public static int RogueLv5PoisonBlastsValue => (int)SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_PoisonBlasts", RogueLv5PoisonBlasts?.Value ?? 10f);

        // 독 즉시 데미지 Lv2~5
        public static float RogueLv2PoisonInstantValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_PoisonInstant", RogueLv2PoisonInstant?.Value ?? 45f);
        public static float RogueLv3PoisonInstantValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_PoisonInstant", RogueLv3PoisonInstant?.Value ?? 50f);
        public static float RogueLv4PoisonInstantValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_PoisonInstant", RogueLv4PoisonInstant?.Value ?? 55f);
        public static float RogueLv5PoisonInstantValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_PoisonInstant", RogueLv5PoisonInstant?.Value ?? 60f);

        // 독 DoT 데미지 Lv2~5
        public static float RogueLv2PoisonDotValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_PoisonDot", RogueLv2PoisonDot?.Value ?? 35f);
        public static float RogueLv3PoisonDotValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_PoisonDot", RogueLv3PoisonDot?.Value ?? 40f);
        public static float RogueLv4PoisonDotValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_PoisonDot", RogueLv4PoisonDot?.Value ?? 45f);
        public static float RogueLv5PoisonDotValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_PoisonDot", RogueLv5PoisonDot?.Value ?? 50f);

        // 충전 시스템
        public static int RogueShadowStrikeChargesValue => (int)SkillTreeConfig.GetEffectiveValue("Rogue_ShadowStrike_Charges",  RogueShadowStrikeCharges?.Value ?? 1f);
        public static int RogueLv5BonusChargesValue     => (int)SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_BonusCharges",      RogueLv5BonusCharges?.Value    ?? 1f);

        // 패시브 Lv1 기본
        public static float RogueAttackSpeedBonusValue           => SkillTreeConfig.GetEffectiveValue("Rogue_AttackSpeed_Bonus", RogueAttackSpeedBonus?.Value ?? 7f);
        public static float RogueStaminaReductionValue            => SkillTreeConfig.GetEffectiveValue("Rogue_Stamina_Reduction", RogueStaminaReduction?.Value ?? 15f);

        // 패시브 공격속도 Lv2~5
        public static float RogueLv2AttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_AttackSpeed", RogueLv2AttackSpeed?.Value ?? 9f);
        public static float RogueLv3AttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_AttackSpeed", RogueLv3AttackSpeed?.Value ?? 11f);
        public static float RogueLv4AttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_AttackSpeed", RogueLv4AttackSpeed?.Value ?? 13f);
        public static float RogueLv5AttackSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_AttackSpeed", RogueLv5AttackSpeed?.Value ?? 15f);

        // 패시브 스태미나 감소 Lv2~5
        public static float RogueLv2StaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_StaminaReduction", RogueLv2StaminaReduction?.Value ?? 17f);
        public static float RogueLv3StaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_StaminaReduction", RogueLv3StaminaReduction?.Value ?? 19f);
        public static float RogueLv4StaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_StaminaReduction", RogueLv4StaminaReduction?.Value ?? 21f);
        public static float RogueLv5StaminaReductionValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_StaminaReduction", RogueLv5StaminaReduction?.Value ?? 25f);

        // 패시브 회피율 Lv1~5
        public static float RogueLv1DodgeChanceValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv1_DodgeChance", RogueLv1DodgeChance?.Value ?? 4f);
        public static float RogueLv2DodgeChanceValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_DodgeChance", RogueLv2DodgeChance?.Value ?? 6f);
        public static float RogueLv3DodgeChanceValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_DodgeChance", RogueLv3DodgeChance?.Value ?? 8f);
        public static float RogueLv4DodgeChanceValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_DodgeChance", RogueLv4DodgeChance?.Value ?? 10f);
        public static float RogueLv5DodgeChanceValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_DodgeChance", RogueLv5DodgeChance?.Value ?? 12f);

        // 패시브 이동속도 Lv1~5
        public static float RogueLv1MoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv1_MoveSpeed", RogueLv1MoveSpeed?.Value ?? 5f);
        public static float RogueLv2MoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv2_MoveSpeed", RogueLv2MoveSpeed?.Value ?? 7f);
        public static float RogueLv3MoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv3_MoveSpeed", RogueLv3MoveSpeed?.Value ?? 9f);
        public static float RogueLv4MoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv4_MoveSpeed", RogueLv4MoveSpeed?.Value ?? 12f);
        public static float RogueLv5MoveSpeedValue => SkillTreeConfig.GetEffectiveValue("Rogue_Lv5_MoveSpeed", RogueLv5MoveSpeed?.Value ?? 15f);

        public static void InitializeRogueConfig(ConfigFile config)
        {
            try
            {
                Plugin.Log.LogDebug("[로그 컨피그] 초기화 시작");

                // === 그림자 일격 Lv1 기본 ===
                RogueShadowStrikeCooldown = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_ShadowStrike_Cooldown", 35f, SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_Cooldown"));
                RogueShadowStrikeStaminaCost = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_ShadowStrike_StaminaCost", 25f, SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_StaminaCost"));
                RogueShadowStrikeAttackBonus = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_ShadowStrike_AttackBonus", 30f, SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_AttackBonus"));
                RogueShadowStrikeBuffDuration = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_ShadowStrike_BuffDuration", 8f, SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_BuffDuration"));

                // === 그림자 일격 Lv2~5 쿨다운 ===
                RogueLv2Cooldown = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_Cooldown", 34f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_Cooldown"));
                RogueLv3Cooldown = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_Cooldown", 33f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_Cooldown"));
                RogueLv4Cooldown = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_Cooldown", 32f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_Cooldown"));
                RogueLv5Cooldown = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_Cooldown", 30f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_Cooldown"));

                // === 그림자 일격 Lv2~5 공격력 버프 ===
                RogueLv2AttackBonus = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_AttackBonus", 40f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_AttackBonus"));
                RogueLv3AttackBonus = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_AttackBonus", 45f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_AttackBonus"));
                RogueLv4AttackBonus = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_AttackBonus", 50f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_AttackBonus"));
                RogueLv5AttackBonus = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_AttackBonus", 55f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_AttackBonus"));

                // === 그림자 일격 Lv2~5 버프 지속시간 ===
                RogueLv2BuffDuration = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_BuffDuration", 9f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_BuffDuration"));
                RogueLv3BuffDuration = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_BuffDuration", 10f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_BuffDuration"));
                RogueLv4BuffDuration = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_BuffDuration", 11f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_BuffDuration"));
                RogueLv5BuffDuration = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_BuffDuration", 12f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_BuffDuration"));

                // === 독 폭발 Lv1 기본 ===
                RoguePoisonRange = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Poison_Range", 15f, SkillTreeConfig.GetConfigDescription("Rogue_Poison_Range"));
                RoguePoisonInstantDamage = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Poison_InstantDamage", 40f, SkillTreeConfig.GetConfigDescription("Rogue_Poison_InstantDamage"));
                RoguePoisonDotDamage = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Poison_DotDamage", 30f, SkillTreeConfig.GetConfigDescription("Rogue_Poison_DotDamage"));
                RoguePoisonDotDuration = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Poison_DotDuration", 10f, SkillTreeConfig.GetConfigDescription("Rogue_Poison_DotDuration"));
                RoguePoisonVFXCount = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Poison_VFXCount", 6f, SkillTreeConfig.GetConfigDescription("Rogue_Poison_VFXCount"));
                RoguePoisonVFXInterval = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Poison_VFXInterval", 0.5f, SkillTreeConfig.GetConfigDescription("Rogue_Poison_VFXInterval"));

                // === 독 폭발 횟수 Lv2~5 ===
                RogueLv2PoisonBlasts = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_PoisonBlasts", 7f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_PoisonBlasts"));
                RogueLv3PoisonBlasts = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_PoisonBlasts", 8f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_PoisonBlasts"));
                RogueLv4PoisonBlasts = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_PoisonBlasts", 9f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_PoisonBlasts"));
                RogueLv5PoisonBlasts = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_PoisonBlasts", 10f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_PoisonBlasts"));

                // === 독 즉시 데미지 Lv2~5 ===
                RogueLv2PoisonInstant = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_PoisonInstant", 45f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_PoisonInstant"));
                RogueLv3PoisonInstant = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_PoisonInstant", 50f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_PoisonInstant"));
                RogueLv4PoisonInstant = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_PoisonInstant", 55f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_PoisonInstant"));
                RogueLv5PoisonInstant = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_PoisonInstant", 60f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_PoisonInstant"));

                // === 독 DoT 데미지 Lv2~5 ===
                RogueLv2PoisonDot = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_PoisonDot", 35f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_PoisonDot"));
                RogueLv3PoisonDot = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_PoisonDot", 40f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_PoisonDot"));
                RogueLv4PoisonDot = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_PoisonDot", 45f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_PoisonDot"));
                RogueLv5PoisonDot = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_PoisonDot", 50f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_PoisonDot"));

                // === 충전 시스템 ===
                RogueShadowStrikeCharges = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_ShadowStrike_Charges", 1f, SkillTreeConfig.GetConfigDescription("Rogue_ShadowStrike_Charges"));
                RogueLv5BonusCharges = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_BonusCharges", 1f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_BonusCharges"));

                // === 패시브 Lv1 기본 ===
                RogueAttackSpeedBonus = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_AttackSpeed_Bonus", 7f, SkillTreeConfig.GetConfigDescription("Rogue_AttackSpeed_Bonus"));
                RogueStaminaReduction = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Stamina_Reduction", 15f, SkillTreeConfig.GetConfigDescription("Rogue_Stamina_Reduction"));

                // === 패시브 공격속도 Lv2~5 ===
                RogueLv2AttackSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_AttackSpeed", 9f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_AttackSpeed"));
                RogueLv3AttackSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_AttackSpeed", 11f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_AttackSpeed"));
                RogueLv4AttackSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_AttackSpeed", 13f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_AttackSpeed"));
                RogueLv5AttackSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_AttackSpeed", 15f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_AttackSpeed"));

                // === 패시브 스태미나 감소 Lv2~5 ===
                RogueLv2StaminaReduction = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_StaminaReduction", 17f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_StaminaReduction"));
                RogueLv3StaminaReduction = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_StaminaReduction", 19f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_StaminaReduction"));
                RogueLv4StaminaReduction = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_StaminaReduction", 21f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_StaminaReduction"));
                RogueLv5StaminaReduction = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_StaminaReduction", 25f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_StaminaReduction"));

                // === 패시브 회피율 Lv1~5 ===
                RogueLv1DodgeChance = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv1_DodgeChance", 4f, SkillTreeConfig.GetConfigDescription("Rogue_Lv1_DodgeChance"));
                RogueLv2DodgeChance = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_DodgeChance", 6f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_DodgeChance"));
                RogueLv3DodgeChance = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_DodgeChance", 8f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_DodgeChance"));
                RogueLv4DodgeChance = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_DodgeChance", 10f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_DodgeChance"));
                RogueLv5DodgeChance = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_DodgeChance", 12f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_DodgeChance"));

                // === 패시브 이동속도 Lv1~5 ===
                RogueLv1MoveSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv1_MoveSpeed", 5f, SkillTreeConfig.GetConfigDescription("Rogue_Lv1_MoveSpeed"));
                RogueLv2MoveSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv2_MoveSpeed", 7f, SkillTreeConfig.GetConfigDescription("Rogue_Lv2_MoveSpeed"));
                RogueLv3MoveSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv3_MoveSpeed", 9f, SkillTreeConfig.GetConfigDescription("Rogue_Lv3_MoveSpeed"));
                RogueLv4MoveSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv4_MoveSpeed", 12f, SkillTreeConfig.GetConfigDescription("Rogue_Lv4_MoveSpeed"));
                RogueLv5MoveSpeed = SkillTreeConfig.BindServerSync(config, "Rogue Job Skills", "Rogue_Lv5_MoveSpeed", 15f, SkillTreeConfig.GetConfigDescription("Rogue_Lv5_MoveSpeed"));

                RegisterRogueEventHandlers();
                Plugin.Log.LogDebug("[로그 컨피그] 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 컨피그] 초기화 실패: {ex.Message}");
            }
        }

        private static void RegisterRogueEventHandlers()
        {
            try
            {
                RogueShadowStrikeCooldown.SettingChanged    += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeStaminaCost.SettingChanged += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeAttackBonus.SettingChanged += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueShadowStrikeBuffDuration.SettingChanged+= (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv2Cooldown.SettingChanged             += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3Cooldown.SettingChanged             += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4Cooldown.SettingChanged             += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5Cooldown.SettingChanged             += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv2AttackBonus.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3AttackBonus.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4AttackBonus.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5AttackBonus.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RoguePoisonRange.SettingChanged             += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RoguePoisonInstantDamage.SettingChanged     += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RoguePoisonDotDamage.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueAttackSpeedBonus.SettingChanged        += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueStaminaReduction.SettingChanged        += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv1DodgeChance.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv1MoveSpeed.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 버프 지속시간 Lv2~5 ===
                RogueLv2BuffDuration.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3BuffDuration.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4BuffDuration.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5BuffDuration.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 독 폭발 횟수 Lv2~5 ===
                RogueLv2PoisonBlasts.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3PoisonBlasts.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4PoisonBlasts.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5PoisonBlasts.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 독 즉시 데미지 Lv2~5 ===
                RogueLv2PoisonInstant.SettingChanged        += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3PoisonInstant.SettingChanged        += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4PoisonInstant.SettingChanged        += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5PoisonInstant.SettingChanged        += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 독 DoT 데미지 Lv2~5 ===
                RogueLv2PoisonDot.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3PoisonDot.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4PoisonDot.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5PoisonDot.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 패시브 공격속도 Lv2~5 ===
                RogueLv2AttackSpeed.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3AttackSpeed.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4AttackSpeed.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5AttackSpeed.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 패시브 스태미나 감소 Lv2~5 ===
                RogueLv2StaminaReduction.SettingChanged     += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3StaminaReduction.SettingChanged     += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4StaminaReduction.SettingChanged     += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5StaminaReduction.SettingChanged     += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 패시브 회피율 Lv2~5 ===
                RogueLv2DodgeChance.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3DodgeChance.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4DodgeChance.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5DodgeChance.SettingChanged          += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 패시브 이동속도 Lv2~5 ===
                RogueLv2MoveSpeed.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv3MoveSpeed.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv4MoveSpeed.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5MoveSpeed.SettingChanged            += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                // === 충전 시스템 ===
                RogueShadowStrikeCharges.SettingChanged     += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();
                RogueLv5BonusCharges.SettingChanged         += (s, a) => Rogue_Tooltip.UpdateRogueTooltip();

                Plugin.Log.LogDebug("[로그 컨피그] 이벤트 핸들러 등록 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[로그 컨피그] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }

        public static void LogRogueConfigValues()
        {
            Plugin.Log.LogInfo("[로그 컨피그] === 현재 설정값 ===");
            Plugin.Log.LogInfo($"  Lv1 쿨다운: {RogueShadowStrikeCooldownValue}초, 공격력버프: {RogueShadowStrikeAttackBonusValue}%");
            Plugin.Log.LogInfo($"  Lv2 쿨다운: {RogueLv2CooldownValue}초, 공격력버프: {RogueLv2AttackBonusValue}%");
            Plugin.Log.LogInfo($"  Lv5 쿨다운: {RogueLv5CooldownValue}초, 공격력버프: {RogueLv5AttackBonusValue}%");
        }
    }
}

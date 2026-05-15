using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 寃 ?ㅽ궗 ?몃━ Config ?ㅼ젙
    /// Mace_Config ?⑦꽩 湲곕컲 ?꾨㈃ ?ъ옉??
    /// </summary>
    public static class Sword_Config
    {
        // ===== Tier 0: 寃 ?꾨Ц媛 (Sword Expert) =====

        /// <summary>
        /// 寃 ?꾨Ц媛 - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordExpertRequiredPoints;

        /// <summary>
        /// 寃 ?꾨Ц媛 - ?쇳빐 利앷? (%)
        /// </summary>
        public static ConfigEntry<float> SwordExpertDamageBonus;

        // ===== Tier 1: 鍮좊Ⅸ 踰좉린 =====

        /// <summary>
        /// Tier 1 鍮좊Ⅸ 踰좉린 - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordStep1FastSlashRequiredPoints;

        /// <summary>
        /// Tier 1 諛섍꺽 ?먯꽭 - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordStep1CounterRequiredPoints;

        /// <summary>
        /// Tier 1 諛섍꺽 ?먯꽭 - 吏?띿떆媛?(珥?
        /// </summary>
        public static ConfigEntry<float> SwordStep1CounterDuration;

        /// <summary>
        /// Tier 1 諛섍꺽 ?먯꽭 - 諛⑹뼱??蹂대꼫??(%)
        /// </summary>
        public static ConfigEntry<float> SwordStep1CounterDefenseBonus;

        /// <summary>
        /// Tier 1 鍮좊Ⅸ 踰좉린 - 怨듦꺽?띾룄 蹂대꼫??(%)
        /// </summary>
        public static ConfigEntry<float> SwordStep1FastSlashSpeed;

        // ===== Tier 2: ?곗냽 踰좉린 =====

        /// <summary>
        /// Tier 2 ?곗냽 踰좉린 - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordStep2ComboSlashRequiredPoints;

        /// <summary>
        /// Tier 2 ?곗냽 踰좉린 - ?곗냽 怨듦꺽 蹂대꼫??(%)
        /// </summary>
        public static ConfigEntry<float> SwordStep2ComboSlashBonus;

        /// <summary>
        /// Tier 2 ?곗냽 踰좉린 - 踰꾪봽 吏?띿떆媛?(珥?
        /// </summary>
        public static ConfigEntry<float> SwordStep2ComboSlashDuration;

        // ===== Tier 3: 移쇰궇 ?섏튂湲?=====

        /// <summary>
        /// Tier 3 移쇰궇 ?섏튂湲?- ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordStep3RiposteRequiredPoints;

        /// <summary>
        /// Tier 3 移쇰궇 ?섏튂湲?- 怨듦꺽??蹂대꼫??(怨좎젙媛?
        /// </summary>
        public static ConfigEntry<float> SwordStep3RiposteDamageBonus;

        // ===== Tier 4: 吏꾧??밸? =====

        /// <summary>
        /// Tier 4 怨듬갑?쇱껜 - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordStep3AllInOneRequiredPoints;

        /// <summary>
        /// Tier 4 怨듬갑?쇱껜 - 怨듦꺽??蹂대꼫??(%)
        /// </summary>
        public static ConfigEntry<float> SwordStep3AllInOneAttackBonus;

        /// <summary>
        /// Tier 4 怨듬갑?쇱껜 - 留됯린 諛⑹뼱??蹂대꼫??(怨좎젙媛?
        /// </summary>
        public static ConfigEntry<float> SwordStep3AllInOneDefenseBonus;

        /// <summary>
        /// Tier 4 吏꾧??밸? - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordStep4TrueDuelRequiredPoints;

        /// <summary>
        /// Tier 4 吏꾧??밸? - 怨듦꺽?띾룄 蹂대꼫??(%)
        /// </summary>
        public static ConfigEntry<float> SwordStep4TrueDuelSpeed;

        // ===== Tier 5: ?⑤쭅 ?뚭꺽 (Parry Rush) - G???≫떚釉??ㅽ궗 =====

        /// <summary>
        /// Tier 5 ?⑤쭅 ?뚭꺽 - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> SwordStep5DefenseSwitchRequiredPoints;

        /// <summary>
        /// ?⑤쭅 ?뚭꺽 - 踰꾪봽 吏?띿떆媛?(珥?
        /// </summary>
        public static ConfigEntry<float> ParryRushDuration;

        /// <summary>
        /// ?⑤쭅 ?뚭꺽 - 留됯린 諛⑹뼱??鍮꾩쑉 (%)
        /// </summary>
        public static ConfigEntry<float> ParryRushBlockPowerRatio;

        /// <summary>
        /// ?⑤쭅 ?뚭꺽 - 諛?대궡湲?嫄곕━ (m)
        /// </summary>
        public static ConfigEntry<float> ParryRushPushDistance;

        /// <summary>
        /// ?⑤쭅 ?뚭꺽 - ?ㅽ깭誘몃굹 ?뚮え
        /// </summary>
        public static ConfigEntry<float> ParryRushStaminaCost;

        /// <summary>
        /// ?⑤쭅 ?뚭꺽 - 荑⑦???(珥?
        /// </summary>
        public static ConfigEntry<float> ParryRushCooldown;

        private static ConfigEntry<float> WhirlwindDamageLevelBonus;

        // ===== Tier 6: ?뚯쭊 ?곗냽 踰좉린 (Rush Slash) ?≫떚釉??ㅽ궗 =====

        /// <summary>
        /// Tier 6 Rush Slash - ?꾩슂 ?ъ씤??
        /// </summary>
        public static ConfigEntry<int> RushSlashRequiredPoints;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 1李?怨듦꺽??鍮꾩쑉 (%)
        /// </summary>
        public static ConfigEntry<float> RushSlash1stDamageRatio;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 2李?怨듦꺽??鍮꾩쑉 (%)
        /// </summary>
        public static ConfigEntry<float> RushSlash2ndDamageRatio;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 3李?怨듦꺽??鍮꾩쑉 (%)
        /// </summary>
        public static ConfigEntry<float> RushSlash3rdDamageRatio;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 珥덇린 ?뚯쭊 嫄곕━ (m)
        /// </summary>
        public static ConfigEntry<float> RushSlashInitialDistance;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 痢〓㈃ ?대룞 嫄곕━ (m)
        /// </summary>
        public static ConfigEntry<float> RushSlashSideDistance;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - ?ㅽ깭誘몃굹 ?뚮え??
        /// </summary>
        public static ConfigEntry<float> RushSlashStaminaCost;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 荑⑦???(珥?
        /// </summary>
        public static ConfigEntry<float> RushSlashCooldown;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - ?대룞 ?띾룄 (m/s)
        /// </summary>
        public static ConfigEntry<float> RushSlashMoveSpeed;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 怨듦꺽 ?띾룄 蹂대꼫??(%, 湲곕낯 怨듦꺽?띾룄 ?鍮?
        /// ?ㅽ궗 諛쒕룞 以??ㅻⅨ ?몃━ 怨듦꺽?띾룄 臾댁떆, ??媛믩쭔 ?곸슜
        /// </summary>
        public static ConfigEntry<float> RushSlashAttackSpeedBonus;

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 - 寃쎈줈 ?덊듃 ?덈퉬 (m)
        /// ?대룞 以???諛섍꼍 ??紐ъ뒪?곕? 紐⑤몢 ?곸쨷
        /// </summary>
        public static ConfigEntry<float> RushSlashPathWidth;
        public static ConfigEntry<float> RushSlashDamageLevelBonus;

        // ===== ?숈쟻 媛??꾨줈?쇳떚 (?쒕쾭 ?숆린??吏?? =====

        // === Tier 0: 寃 ?꾨Ц媛 ===
        public static int SwordExpertRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Expert_RequiredPoints", SwordExpertRequiredPoints?.Value ?? 2);
        public static float SwordExpertDamageValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Expert_DamageBonus", SwordExpertDamageBonus?.Value ?? 10f);

        // === Tier 1: 鍮좊Ⅸ 踰좉린 ===
        public static int SwordStep1FastSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step1_RequiredPoints", SwordStep1FastSlashRequiredPoints?.Value ?? 2);
        public static int SwordStep1CounterRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step1_Counter_RequiredPoints", SwordStep1CounterRequiredPoints?.Value ?? 3);
        public static float SwordStep1CounterDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step1_Counter_Duration", SwordStep1CounterDuration?.Value ?? 5f);
        public static float SwordStep1CounterDefenseBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step1_Counter_DefenseBonus", SwordStep1CounterDefenseBonus?.Value ?? 30f);
        public static float SwordStep1FastSlashSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step1_FastSlash_Speed", SwordStep1FastSlashSpeed?.Value ?? 10f);

        // === Tier 2: ?곗냽 踰좉린 ===
        public static int SwordStep2ComboSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step2_RequiredPoints", SwordStep2ComboSlashRequiredPoints?.Value ?? 1);
        public static float SwordStep2ComboSlashBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step2_ComboSlash_Bonus", SwordStep2ComboSlashBonus?.Value ?? 15f);
        public static float SwordStep2ComboSlashDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step2_ComboSlash_Duration", SwordStep2ComboSlashDuration?.Value ?? 5f);

        // === Tier 3: 移쇰궇 ?섏튂湲?===
        public static int SwordStep3RiposteRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step3_RequiredPoints", SwordStep3RiposteRequiredPoints?.Value ?? 1);
        public static float SwordRiposteDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step3_Riposte_DamageBonus", SwordStep3RiposteDamageBonus?.Value ?? 5f);

        // === Tier 4: 怨듬갑?쇱껜 ===
        public static int SwordStep3AllInOneRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step3_AllInOne_RequiredPoints", SwordStep3AllInOneRequiredPoints?.Value ?? 2);
        public static float SwordStep3AllInOneAttackBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step3_AllInOne_AttackBonus", SwordStep3AllInOneAttackBonus?.Value ?? 10f);
        public static float SwordStep3AllInOneDefenseBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step3_AllInOne_DefenseBonus", SwordStep3AllInOneDefenseBonus?.Value ?? 15f);

        // === Tier 5: 吏꾧??밸? ===
        public static int SwordStep4TrueDuelRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step4_RequiredPoints", SwordStep4TrueDuelRequiredPoints?.Value ?? 3);
        public static float SwordStep4TrueDuelSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step4_TrueDuel_Speed", SwordStep4TrueDuelSpeed?.Value ?? 15f);

        // === Tier 5: ?⑤쭅 ?뚭꺽 (Parry Rush) ===
        public static int SwordStep5DefenseSwitchRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step5_RequiredPoints", SwordStep5DefenseSwitchRequiredPoints?.Value ?? 3);
        public static float ParryRushDurationValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_Duration", ParryRushDuration?.Value ?? 30f);
        public static float ParryRushBlockPowerRatioValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_BlockPowerRatio", ParryRushBlockPowerRatio?.Value ?? 50f);
        public static float ParryRushPushDistanceValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_PushDistance", ParryRushPushDistance?.Value ?? 4f);
        public static float ParryRushStaminaCostValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_StaminaCost", ParryRushStaminaCost?.Value ?? 10f);
        public static float ParryRushCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_Cooldown", ParryRushCooldown?.Value ?? 35f);
        public static float WhirlwindDamageLevelBonusValue =>
            SkillTreeConfig.GetEffectiveValue("sword_tier6_whirlwind_damage_level_bonus", WhirlwindDamageLevelBonus?.Value ?? 10f);

        // === Tier 6: ?뚯쭊 ?곗냽 踰좉린 (Rush Slash) ===
        public static int RushSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step6_RequiredPoints", RushSlashRequiredPoints?.Value ?? 3);
        public static float RushSlash1stDamageRatioValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_1st_DamageRatio", RushSlash1stDamageRatio?.Value ?? 40f);
        public static float RushSlash2ndDamageRatioValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_2nd_DamageRatio", RushSlash2ndDamageRatio?.Value ?? 50f);
        public static float RushSlash3rdDamageRatioValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_3rd_DamageRatio", RushSlash3rdDamageRatio?.Value ?? 60f);
        public static float RushSlashDamageLevelBonusValue =>
            SkillTreeConfig.GetEffectiveValue("sword_tier6_rushslash_damage_level_bonus", RushSlashDamageLevelBonus?.Value ?? 10f);
        public static float RushSlashInitialDistanceValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_InitialDistance", RushSlashInitialDistance?.Value ?? 5f);
        public static float RushSlashSideDistanceValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_SideDistance", RushSlashSideDistance?.Value ?? 3f);
        public static float RushSlashStaminaCostValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_StaminaCost", RushSlashStaminaCost?.Value ?? 30f);
        public static float RushSlashCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_Cooldown", RushSlashCooldown?.Value ?? 25f);
        public static float RushSlashMoveSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_MoveSpeed", RushSlashMoveSpeed?.Value ?? 20f);
        public static float RushSlashAttackSpeedBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_AttackSpeedBonus", RushSlashAttackSpeedBonus?.Value ?? 220f);
        public static float RushSlashPathWidthValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_PathWidth", RushSlashPathWidth?.Value ?? 3.0f);

        // ===== ?명솚???섑띁 (湲곗〈 肄붾뱶 吏?? =====

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 ?ㅽ궗 ?곸꽭 ?뺣낫 援ъ“泥?
        /// </summary>
        public struct RushSlashSkillData
        {
            public float damage1stRatio;   // 1李?怨듦꺽??鍮꾩쑉 (70%)
            public float damage2ndRatio;   // 2李?怨듦꺽??鍮꾩쑉 (80%)
            public float damage3rdRatio;   // 3李?怨듦꺽??鍮꾩쑉 (90%)
            public float initialDistance;  // 珥덇린 ?뚯쭊 嫄곕━ (5m)
            public float sideDistance;     // 痢〓㈃ ?대룞 嫄곕━ (3m)
            public float moveSpeed;        // ?대룞 ?띾룄 (20m/s)
            public float staminaCost;      // ?ㅽ깭誘몃굹 ?뚮え??(30)
            public float cooldown;         // 荑⑦???(25珥?
            public string skillKey;        // ??諛붿씤??
            public string requirement;     // ?ъ슜 議곌굔
        }

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 ?ㅽ궗 ?곗씠??媛?몄삤湲?
        /// </summary>
        public static RushSlashSkillData GetRushSlashData()
        {
            return new RushSlashSkillData
            {
                damage1stRatio = RushSlash1stDamageRatioValue,
                damage2ndRatio = RushSlash2ndDamageRatioValue,
                damage3rdRatio = RushSlash3rdDamageRatioValue,
                initialDistance = RushSlashInitialDistanceValue,
                sideDistance = RushSlashSideDistanceValue,
                moveSpeed = RushSlashMoveSpeedValue,
                staminaCost = RushSlashStaminaCostValue,
                cooldown = RushSlashCooldownValue,
                skillKey = "G??",
                requirement = "寃 李⑹슜"
            };
        }

        // ===== 湲곗〈 ?명솚???꾨줈?쇳떚 (SkillTreeConfig 李몄“ -> ??Config 李몄“) =====

        // 湲곗〈 肄붾뱶?먯꽌 ?ъ슜?섎뜕 ?꾨줈?쇳떚??(?명솚???좎?)
        public static float SwordExpertCritValue => 15f; // 湲곕낯媛??좎?
        public static float SwordStep1DamageValue => 10f;
        public static float SwordStep2CritValue => 8f;
        public static float SwordStep3ArmorValue => 12f;
        public static float SwordStep4BlockValue => 20f;
        public static float SwordStep1ExpertDurationValue => SwordStep2ComboSlashDurationValue;
        public static float SwordStep2ComboSlashDurationValueLegacy => SwordStep2ComboSlashDurationValue;
        public static bool IsConfigLoaded => true;

        // ===== 珥덇린??硫붿꽌??=====

        /// <summary>
        /// Sword Config 珥덇린??
        /// </summary>
        /// <param name="config">BepInEx ConfigFile</param>
        public static void Initialize(ConfigFile config)
        {
            // Tier 0: 寃 ?꾨Ц媛 (Sword Expert)
            SwordExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier0_SwordExpert_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier0_SwordExpert_RequiredPoints")
            );

            SwordExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier0_SwordExpert_DamageBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier0_SwordExpert_DamageBonus")
            );

            // Tier 1: 鍮좊Ⅸ 踰좉린
            SwordStep1FastSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_FastSlash_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier1_FastSlash_RequiredPoints")
            );

            // Tier 1: 諛섍꺽 ?먯꽭
            SwordStep1CounterRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_CounterStance_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier1_CounterStance_RequiredPoints")
            );

            SwordStep1CounterDuration = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_CounterStance_Duration",
                5f,
                SkillTreeConfig.GetConfigDescription("Tier1_CounterStance_Duration")
            );

            SwordStep1CounterDefenseBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_CounterStance_DefenseBonus",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier1_CounterStance_DefenseBonus")
            );

            SwordStep1FastSlashSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_FastSlash_AttackSpeedBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier1_FastSlash_AttackSpeedBonus")
            );

            // Tier 2: ?곗냽 踰좉린
            SwordStep2ComboSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_ComboSlash_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier2_ComboSlash_RequiredPoints")
            );

            SwordStep2ComboSlashBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_ComboSlash_DamageBonus",
                15f,
                SkillTreeConfig.GetConfigDescription("Tier2_ComboSlash_DamageBonus")
            );

            SwordStep2ComboSlashDuration = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_ComboSlash_BuffDuration",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier2_ComboSlash_BuffDuration")
            );

            // Tier 3: 移쇰궇 ?섏튂湲?
            SwordStep3RiposteRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier3_Riposte_RequiredPoints",
                1,
                SkillTreeConfig.GetConfigDescription("Tier3_Riposte_RequiredPoints")
            );

            SwordStep3RiposteDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier3_Riposte_DamageBonus",
                5f,
                SkillTreeConfig.GetConfigDescription("Tier3_Riposte_DamageBonus")
            );

            // Tier 4: 怨듬갑?쇱껜
            SwordStep3AllInOneRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_AllInOne_RequiredPoints",
                2,
                SkillTreeConfig.GetConfigDescription("Tier4_AllInOne_RequiredPoints")
            );

            SwordStep3AllInOneAttackBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_AllInOne_AttackBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier4_AllInOne_AttackBonus")
            );

            SwordStep3AllInOneDefenseBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_AllInOne_DefenseBonus",
                15f,
                SkillTreeConfig.GetConfigDescription("Tier4_AllInOne_DefenseBonus")
            );

            // Tier 4: 吏꾧??밸?
            SwordStep4TrueDuelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_TrueDuel_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier4_TrueDuel_RequiredPoints")
            );

            SwordStep4TrueDuelSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_TrueDuel_AttackSpeedBonus",
                15f,
                SkillTreeConfig.GetConfigDescription("Tier4_TrueDuel_AttackSpeedBonus")
            );

            // Tier 5: ?⑤쭅 ?뚭꺽 (Parry Rush)
            SwordStep5DefenseSwitchRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_ParryRush_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier5_ParryRush_RequiredPoints"),
                order: -50
            );

            ParryRushDuration = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_ParryRush_BuffDuration",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier5_ParryRush_BuffDuration"),
                order: -51
            );

            ParryRushBlockPowerRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_ParryRush_BlockPowerRatio",
                50f,
                SkillTreeConfig.GetConfigDescription("Tier5_ParryRush_BlockPowerRatio"),
                order: -52
            );

            ParryRushPushDistance = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_ParryRush_PushDistance",
                4f,
                SkillTreeConfig.GetConfigDescription("Tier5_ParryRush_PushDistance"),
                order: -53
            );

            ParryRushStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_ParryRush_StaminaCost",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier5_ParryRush_StaminaCost"),
                order: -54
            );

            ParryRushCooldown = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_ParryRush_Cooldown",
                35f,
                SkillTreeConfig.GetConfigDescription("Tier5_ParryRush_Cooldown"),
                order: -55
            );

            WhirlwindDamageLevelBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_Whirlwind_DamageLevelBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier6_Whirlwind_DamageLevelBonus"),
                order: -56
            );

            // Tier 6: ?뚯쭊 ?곗냽 踰좉린 (Rush Slash) ?≫떚釉??ㅽ궗
            RushSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_RequiredPoints",
                3,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_RequiredPoints")
            );

            RushSlash1stDamageRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_Hit1DamageRatio",
                40f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_Hit1DamageRatio")
            );

            RushSlash2ndDamageRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_Hit2DamageRatio",
                50f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_Hit2DamageRatio")
            );

            RushSlash3rdDamageRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_Hit3DamageRatio",
                60f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_Hit3DamageRatio")
            );

            RushSlashDamageLevelBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_DamageLevelBonus",
                10f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_DamageLevelBonus")
            );

            RushSlashInitialDistance = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_InitialDistance",
                5f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_InitialDistance")
            );

            RushSlashSideDistance = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_SideDistance",
                3f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_SideDistance")
            );

            RushSlashStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_StaminaCost",
                30f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_StaminaCost")
            );

            RushSlashCooldown = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_Cooldown",
                25f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_Cooldown")
            );

            RushSlashMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_MoveSpeed",
                20f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_MoveSpeed")
            );

            RushSlashAttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_AttackSpeedBonus",
                220f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_AttackSpeedBonus")
            );

            RushSlashPathWidth = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_PathWidth", 3.0f,
                SkillTreeConfig.GetConfigDescription("Tier6_RushSlash_PathWidth")
            );

            Plugin.Log.LogDebug("[Sword Config] 寃 ?ㅽ궗 Config 珥덇린???꾨즺");
        }

        /// <summary>
        /// ?뚯쭊 ?곗냽 踰좉린 ?곕?吏 怨꾩궛 (怨듦꺽 李⑥닔蹂?
        /// </summary>
        /// <param name="baseDamage">湲곕낯 臾닿린 ?곕?吏</param>
        /// <param name="attackNumber">怨듦꺽 李⑥닔 (1, 2, 3)</param>
        public static float CalculateRushSlashDamage(float baseDamage, int attackNumber)
        {
            float ratio = attackNumber switch
            {
                1 => RushSlash1stDamageRatioValue,
                2 => RushSlash2ndDamageRatioValue,
                3 => RushSlash3rdDamageRatioValue,
                _ => RushSlash1stDamageRatioValue
            };
            return baseDamage * (ratio / 100f);
        }

        /// <summary>
        /// 珥??ㅽ궗 吏?띿떆媛?怨꾩궛 (?대룞 湲곕컲)
        /// </summary>
        public static float CalculateTotalSkillDuration()
        {
            // 珥덇린 ?뚯쭊 + 痢〓㈃ ?대룞 2??+ ?꾨갑 ?대룞
            float totalDistance = RushSlashInitialDistanceValue + (RushSlashSideDistanceValue * 3);
            return totalDistance / RushSlashMoveSpeedValue + 0.6f; // 怨듦꺽 紐⑥뀡 ?쒓컙 異붽?
        }

    }
}


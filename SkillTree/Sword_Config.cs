using BepInEx.Configuration;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 검 스킬 트리 Config 설정
    /// Mace_Config 패턴 기반 전면 재작성
    /// </summary>
    public static class Sword_Config
    {
        // ===== Tier 0: 검 전문가 (Sword Expert) =====

        /// <summary>
        /// 검 전문가 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordExpertRequiredPoints;

        /// <summary>
        /// 검 전문가 - 피해 증가 (%)
        /// </summary>
        public static ConfigEntry<float> SwordExpertDamageBonus;

        // ===== Tier 1: 빠른 베기 =====

        /// <summary>
        /// Tier 1 빠른 베기 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep1FastSlashRequiredPoints;

        /// <summary>
        /// Tier 1 반격 자세 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep1CounterRequiredPoints;

        /// <summary>
        /// Tier 1 빠른 베기 - 공격속도 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep1FastSlashSpeed;

        // ===== Tier 2: 연속 베기 =====

        /// <summary>
        /// Tier 2 연속 베기 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep2ComboSlashRequiredPoints;

        /// <summary>
        /// Tier 2 연속 베기 - 연속 공격 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep2ComboSlashBonus;

        /// <summary>
        /// Tier 2 연속 베기 - 버프 지속시간 (초)
        /// </summary>
        public static ConfigEntry<float> SwordStep2ComboSlashDuration;

        // ===== Tier 3: 칼날 되치기 =====

        /// <summary>
        /// Tier 3 칼날 되치기 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep3RiposteRequiredPoints;

        /// <summary>
        /// Tier 3 칼날 되치기 - 공격력 보너스 (고정값)
        /// </summary>
        public static ConfigEntry<float> SwordStep3RiposteDamageBonus;

        // ===== Tier 4: 진검승부 =====

        /// <summary>
        /// Tier 4 공방일체 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep3AllInOneRequiredPoints;

        /// <summary>
        /// Tier 4 진검승부 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep4TrueDuelRequiredPoints;

        /// <summary>
        /// Tier 4 진검승부 - 공격속도 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> SwordStep4TrueDuelSpeed;

        // ===== Tier 5: 패링 돌격 (Parry Rush) - G키 액티브 스킬 =====

        /// <summary>
        /// Tier 5 패링 돌격 - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> SwordStep5DefenseSwitchRequiredPoints;

        /// <summary>
        /// 패링 돌격 - 버프 지속시간 (초)
        /// </summary>
        public static ConfigEntry<float> ParryRushDuration;

        /// <summary>
        /// 패링 돌격 - 공격력 보너스 (%)
        /// </summary>
        public static ConfigEntry<float> ParryRushDamageBonus;

        /// <summary>
        /// 패링 돌격 - 밀어내기 거리 (m)
        /// </summary>
        public static ConfigEntry<float> ParryRushPushDistance;

        /// <summary>
        /// 패링 돌격 - 스태미나 소모
        /// </summary>
        public static ConfigEntry<float> ParryRushStaminaCost;

        /// <summary>
        /// 패링 돌격 - 쿨타임 (초)
        /// </summary>
        public static ConfigEntry<float> ParryRushCooldown;

        // ===== Tier 6: 돌진 연속 베기 (Rush Slash) 액티브 스킬 =====

        /// <summary>
        /// Tier 6 Rush Slash - 필요 포인트
        /// </summary>
        public static ConfigEntry<int> RushSlashRequiredPoints;

        /// <summary>
        /// 돌진 연속 베기 - 1차 공격력 비율 (%)
        /// </summary>
        public static ConfigEntry<float> RushSlash1stDamageRatio;

        /// <summary>
        /// 돌진 연속 베기 - 2차 공격력 비율 (%)
        /// </summary>
        public static ConfigEntry<float> RushSlash2ndDamageRatio;

        /// <summary>
        /// 돌진 연속 베기 - 3차 공격력 비율 (%)
        /// </summary>
        public static ConfigEntry<float> RushSlash3rdDamageRatio;

        /// <summary>
        /// 돌진 연속 베기 - 초기 돌진 거리 (m)
        /// </summary>
        public static ConfigEntry<float> RushSlashInitialDistance;

        /// <summary>
        /// 돌진 연속 베기 - 측면 이동 거리 (m)
        /// </summary>
        public static ConfigEntry<float> RushSlashSideDistance;

        /// <summary>
        /// 돌진 연속 베기 - 스태미나 소모량
        /// </summary>
        public static ConfigEntry<float> RushSlashStaminaCost;

        /// <summary>
        /// 돌진 연속 베기 - 쿨타임 (초)
        /// </summary>
        public static ConfigEntry<float> RushSlashCooldown;

        /// <summary>
        /// 돌진 연속 베기 - 이동 속도 (m/s)
        /// </summary>
        public static ConfigEntry<float> RushSlashMoveSpeed;

        /// <summary>
        /// 돌진 연속 베기 - 공격 속도 보너스 (%, 기본 공격속도 대비)
        /// 스킬 발동 중 다른 트리 공격속도 무시, 이 값만 적용
        /// </summary>
        public static ConfigEntry<float> RushSlashAttackSpeedBonus;

        // ===== 동적 값 프로퍼티 (서버 동기화 지원) =====

        // === Tier 0: 검 전문가 ===
        public static int SwordExpertRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Expert_RequiredPoints", SwordExpertRequiredPoints?.Value ?? 2);
        public static float SwordExpertDamageValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Expert_DamageBonus", SwordExpertDamageBonus?.Value ?? 10f);

        // === Tier 1: 빠른 베기 ===
        public static int SwordStep1FastSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step1_RequiredPoints", SwordStep1FastSlashRequiredPoints?.Value ?? 2);
        public static int SwordStep1CounterRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step1_Counter_RequiredPoints", SwordStep1CounterRequiredPoints?.Value ?? 3);
        public static float SwordStep1FastSlashSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step1_FastSlash_Speed", SwordStep1FastSlashSpeed?.Value ?? 10f);

        // === Tier 2: 연속 베기 ===
        public static int SwordStep2ComboSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step2_RequiredPoints", SwordStep2ComboSlashRequiredPoints?.Value ?? 1);
        public static float SwordStep2ComboSlashBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step2_ComboSlash_Bonus", SwordStep2ComboSlashBonus?.Value ?? 15f);
        public static float SwordStep2ComboSlashDurationValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step2_ComboSlash_Duration", SwordStep2ComboSlashDuration?.Value ?? 5f);

        // === Tier 3: 칼날 되치기 ===
        public static int SwordStep3RiposteRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step3_RequiredPoints", SwordStep3RiposteRequiredPoints?.Value ?? 1);
        public static float SwordRiposteDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step3_Riposte_DamageBonus", SwordStep3RiposteDamageBonus?.Value ?? 5f);

        // === Tier 4: 공방일체 ===
        public static int SwordStep3AllInOneRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step3_AllInOne_RequiredPoints", SwordStep3AllInOneRequiredPoints?.Value ?? 2);

        // === Tier 5: 진검승부 ===
        public static int SwordStep4TrueDuelRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step4_RequiredPoints", SwordStep4TrueDuelRequiredPoints?.Value ?? 3);
        public static float SwordStep4TrueDuelSpeedValue =>
            SkillTreeConfig.GetEffectiveValue("Sword_Step4_TrueDuel_Speed", SwordStep4TrueDuelSpeed?.Value ?? 15f);

        // === Tier 5: 패링 돌격 (Parry Rush) ===
        public static int SwordStep5DefenseSwitchRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step5_RequiredPoints", SwordStep5DefenseSwitchRequiredPoints?.Value ?? 3);
        public static float ParryRushDurationValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_Duration", ParryRushDuration?.Value ?? 30f);
        public static float ParryRushDamageBonusValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_DamageBonus", ParryRushDamageBonus?.Value ?? 100f);
        public static float ParryRushPushDistanceValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_PushDistance", ParryRushPushDistance?.Value ?? 4f);
        public static float ParryRushStaminaCostValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_StaminaCost", ParryRushStaminaCost?.Value ?? 10f);
        public static float ParryRushCooldownValue =>
            SkillTreeConfig.GetEffectiveValue("ParryRush_Cooldown", ParryRushCooldown?.Value ?? 60f);

        // === Tier 6: 돌진 연속 베기 (Rush Slash) ===
        public static int RushSlashRequiredPointsValue =>
            (int)SkillTreeConfig.GetEffectiveValue("Sword_Step6_RequiredPoints", RushSlashRequiredPoints?.Value ?? 3);
        public static float RushSlash1stDamageRatioValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_1st_DamageRatio", RushSlash1stDamageRatio?.Value ?? 70f);
        public static float RushSlash2ndDamageRatioValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_2nd_DamageRatio", RushSlash2ndDamageRatio?.Value ?? 80f);
        public static float RushSlash3rdDamageRatioValue =>
            SkillTreeConfig.GetEffectiveValue("Rush_Slash_3rd_DamageRatio", RushSlash3rdDamageRatio?.Value ?? 90f);
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

        // ===== 호환성 래퍼 (기존 코드 지원) =====

        /// <summary>
        /// 돌진 연속 베기 스킬 상세 정보 구조체
        /// </summary>
        public struct RushSlashSkillData
        {
            public float damage1stRatio;   // 1차 공격력 비율 (70%)
            public float damage2ndRatio;   // 2차 공격력 비율 (80%)
            public float damage3rdRatio;   // 3차 공격력 비율 (90%)
            public float initialDistance;  // 초기 돌진 거리 (5m)
            public float sideDistance;     // 측면 이동 거리 (3m)
            public float moveSpeed;        // 이동 속도 (20m/s)
            public float staminaCost;      // 스태미나 소모량 (30)
            public float cooldown;         // 쿨타임 (25초)
            public string skillKey;        // 키 바인딩
            public string requirement;     // 사용 조건
        }

        /// <summary>
        /// 돌진 연속 베기 스킬 데이터 가져오기
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
                skillKey = "G키",
                requirement = "검 착용"
            };
        }

        // ===== 기존 호환성 프로퍼티 (SkillTreeConfig 참조 -> 새 Config 참조) =====

        // 기존 코드에서 사용하던 프로퍼티들 (호환성 유지)
        public static float SwordExpertCritValue => 15f; // 기본값 유지
        public static float SwordStep1DamageValue => 10f;
        public static float SwordStep2CritValue => 8f;
        public static float SwordStep3ArmorValue => 12f;
        public static float SwordStep4BlockValue => 20f;
        public static float SwordStep1ExpertDurationValue => SwordStep2ComboSlashDurationValue;
        public static float SwordStep2ComboSlashDurationValueLegacy => SwordStep2ComboSlashDurationValue;
        public static bool IsConfigLoaded => true;

        // ===== 초기화 메서드 =====

        /// <summary>
        /// Sword Config 초기화
        /// </summary>
        /// <param name="config">BepInEx ConfigFile</param>
        public static void Initialize(ConfigFile config)
        {
            // Tier 0: 검 전문가 (Sword Expert)
            SwordExpertRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier0_검전문가_필요포인트",
                2,
                "Tier 0: 검 전문가 - 필요 포인트"
            );

            SwordExpertDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier0_검전문가_피해증가",
                10f,
                "Tier 0: 검 전문가 - 피해 증가 (%)"
            );

            // Tier 1: 빠른 베기
            SwordStep1FastSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_빠른베기_필요포인트",
                2,
                "Tier 1: 빠른 베기(sword_step1_fastslash) - 필요 포인트"
            );

            // Tier 1: 반격 자세
            SwordStep1CounterRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_반격자세_필요포인트",
                3,
                "Tier 1: 반격 자세(sword_step1_counter) - 필요 포인트"
            );

            SwordStep1FastSlashSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier1_빠른베기_공격속도보너스",
                10f,
                "Tier 1: 빠른 베기(sword_step1_fastslash) - 공격속도 보너스 (%)"
            );

            // Tier 2: 연속 베기
            SwordStep2ComboSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_필요포인트",
                1,
                "Tier 2: 연속 베기(sword_step2_combo_slash) - 필요 포인트"
            );

            SwordStep2ComboSlashBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_보너스",
                15f,
                "Tier 2: 연속 베기(sword_step2_combo_slash) - 연속 공격 보너스 (%)"
            );

            SwordStep2ComboSlashDuration = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier2_연속베기_지속시간",
                5f,
                "Tier 2: 연속 베기(sword_step2_combo_slash) - 버프 지속시간 (초)"
            );

            // Tier 3: 칼날 되치기
            SwordStep3RiposteRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier3_칼날되치기_필요포인트",
                1,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 필요 포인트"
            );

            SwordStep3RiposteDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier3_칼날되치기_공격력보너스",
                5f,
                "Tier 3: 칼날 되치기(sword_step3_riposte) - 공격력 보너스 (고정값)"
            );

            // Tier 4: 공방일체
            SwordStep3AllInOneRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier4_공방일체_필요포인트",
                2,
                "Tier 4: 공방일체(sword_step3_allinone) - 필요 포인트"
            );

            // Tier 5: 진검승부
            SwordStep4TrueDuelRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_진검승부_필요포인트",
                3,
                "Tier 5: 진검승부(sword_step4_duel) - 필요 포인트"
            );

            SwordStep4TrueDuelSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_진검승부_공격속도보너스",
                15f,
                "Tier 5: 진검승부(sword_step4_duel) - 공격속도 보너스 (%)"
            );

            // Tier 5: 패링 돌격 (Parry Rush)
            SwordStep5DefenseSwitchRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_패링돌격_필요포인트",
                3,
                "Tier 5: 패링 돌격(sword_step5_defswitch) - 필요 포인트"
            );

            ParryRushDuration = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_패링돌격_버프지속시간",
                30f,
                "Tier 5: 패링 돌격 - 버프 지속시간 (초)"
            );

            ParryRushDamageBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_패링돌격_공격력보너스",
                100f,
                "Tier 5: 패링 돌격 - 패링 성공 시 돌격 공격력 보너스 (%)"
            );

            ParryRushPushDistance = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_패링돌격_밀어내기거리",
                4f,
                "Tier 5: 패링 돌격 - 밀어내기 거리 (m)"
            );

            ParryRushStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_패링돌격_스태미나소모",
                10f,
                "Tier 5: 패링 돌격 - G키 버프 활성화 스태미나 소모"
            );

            ParryRushCooldown = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier5_패링돌격_쿨타임",
                60f,
                "Tier 5: 패링 돌격 - 쿨타임 (초)"
            );

            // Tier 6: 돌진 연속 베기 (Rush Slash) 액티브 스킬
            RushSlashRequiredPoints = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_필요포인트",
                3,
                "Tier 6: 돌진 연속 베기(sword_step5_finalcut) - 필요 포인트"
            );

            RushSlash1stDamageRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_1차공격력비율",
                70f,
                "Tier 6: 돌진 연속 베기 - 1차 공격력 비율 (%)"
            );

            RushSlash2ndDamageRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_2차공격력비율",
                80f,
                "Tier 6: 돌진 연속 베기 - 2차 공격력 비율 (%)"
            );

            RushSlash3rdDamageRatio = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_3차공격력비율",
                90f,
                "Tier 6: 돌진 연속 베기 - 3차 공격력 비율 (%)"
            );

            RushSlashInitialDistance = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_초기돌진거리",
                5f,
                "Tier 6: 돌진 연속 베기 - 초기 돌진 거리 (m)"
            );

            RushSlashSideDistance = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_측면이동거리",
                3f,
                "Tier 6: 돌진 연속 베기 - 측면 이동 거리 (m)"
            );

            RushSlashStaminaCost = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_스태미나소모",
                30f,
                "Tier 6: 돌진 연속 베기 - 스태미나 소모량"
            );

            RushSlashCooldown = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_쿨타임",
                25f,
                "Tier 6: 돌진 연속 베기 - 쿨타임 (초)"
            );

            RushSlashMoveSpeed = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_이동속도",
                20f,
                "Tier 6: 돌진 연속 베기 - 이동 속도 (m/s)"
            );

            RushSlashAttackSpeedBonus = SkillTreeConfig.BindServerSync(config,
                "Sword Tree",
                "Tier6_RushSlash_공격속도보너스",
                220f,
                "Tier 6: 돌진 연속 베기 - 공격 속도 보너스 (%, 기본 대비). 스킬 중 다른 트리 공격속도 무시"
            );

            Plugin.Log.LogDebug("[Sword Config] 검 스킬 Config 초기화 완료");
        }

        /// <summary>
        /// 돌진 연속 베기 데미지 계산 (공격 차수별)
        /// </summary>
        /// <param name="baseDamage">기본 무기 데미지</param>
        /// <param name="attackNumber">공격 차수 (1, 2, 3)</param>
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
        /// 총 스킬 지속시간 계산 (이동 기반)
        /// </summary>
        public static float CalculateTotalSkillDuration()
        {
            // 초기 돌진 + 측면 이동 2회 + 후방 이동
            float totalDistance = RushSlashInitialDistanceValue + (RushSlashSideDistanceValue * 3);
            return totalDistance / RushSlashMoveSpeedValue + 0.6f; // 공격 모션 시간 추가
        }

    }
}

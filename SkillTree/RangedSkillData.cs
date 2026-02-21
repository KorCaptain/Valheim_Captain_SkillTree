using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 원거리 스킬 데이터 (활, 석궁, 지팡이)
    /// SkillTreeData.cs에서 분리된 원거리 무기 전용 스킬 트리
    /// </summary>
    public static class RangedSkillData
    {
        /// <summary>
        /// 원거리 스킬 트리 등록 (활, 석궁, 지팡이)
        /// </summary>
        public static void RegisterRangedSkills()
        {
            var manager = SkillTreeManager.Instance;

            // ==================== 원거리 전문가 루트 노드 ====================
            manager.AddSkill(new SkillNode {
                Id = "ranged_root",
                NameKey = "ranged_skill_expert",
                DescriptionKey = "ranged_root_desc",
                RequiredPoints = 2,
                MaxLevel = 1,
                Position = new Vector2(-90, 60),
                Category = "원거리",
                IconNameLocked = "ranged_lock",
                IconNameUnlocked = "ranged_unlock",
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, L.Get("ranged_root_effect"));
                    }
                }
            });

            // ==================== 석궁 스킬 트리 ====================

            // Step 1: 석궁 전문가
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step1_damage",
                NameKey = "crossbow_skill_expert",
                DescriptionKey = "crossbow_expert_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowExpertDamageBonusValue },
                RequiredPoints = Crossbow_Config.CrossbowExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-160, 90),
                Category = "원거리",
                IconNameLocked = "crossbow_lock",
                IconNameUnlocked = "crossbow_unlock",
                Prerequisites = new List<string> { "ranged_root" },
                NextNodes = new List<string> { "crossbow_Step2_rapid_fire" },
                ApplyEffect = (lv) => { }
            });

            // Step 2: 연속 발사 Lv1
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step2_rapid_fire",
                NameKey = "crossbow_rapid_fire_lv1_name",
                DescriptionKey = "crossbow_rapid_fire_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowRapidFireChanceValue, Crossbow_Config.CrossbowRapidFireShotCountValue, Crossbow_Config.CrossbowRapidFireDamagePercentValue, Crossbow_Config.CrossbowRapidFireBoltConsumptionValue },
                RequiredPoints = Crossbow_Config.CrossbowStep2RequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-230, 130),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step1_damage" },
                NextNodes = new List<string> { "crossbow_Step2_balance", "crossbow_Step3_rapid", "crossbow_Step3_mark" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-1: 균형 조준
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step2_balance",
                NameKey = "crossbow_balance_name",
                DescriptionKey = "crossbow_balance_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowBalanceKnockbackChanceValue, Crossbow_Config.CrossbowBalanceKnockbackDistanceValue },
                RequiredPoints = Crossbow_Config.CrossbowStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-310, 140),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_rapid_fire" },
                NextNodes = new List<string> { "crossbow_Step4_re" },
                ApplyEffect = (lv) => { }
            });

            // Step 3-1: 고속 장전
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step3_rapid",
                NameKey = "crossbow_rapid_name",
                DescriptionKey = "crossbow_rapid_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowRapidReloadSpeedValue },
                RequiredPoints = Crossbow_Config.CrossbowStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-300, 190),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_rapid_fire" },
                NextNodes = new List<string> { "crossbow_Step4_re" },
                ApplyEffect = (lv) => { }
            });

            // Step 3-2: 정직한 한 발
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step3_mark",
                NameKey = "crossbow_mark_name",
                DescriptionKey = "crossbow_mark_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowMarkDamageBonusValue },
                RequiredPoints = Crossbow_Config.CrossbowStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-290, 240),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_rapid_fire" },
                NextNodes = new List<string> { "crossbow_Step4_re" },
                ApplyEffect = (lv) => { }
            });

            // Step 4: 자동 장전
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step4_re",
                NameKey = "crossbow_auto_reload_name",
                DescriptionKey = "crossbow_auto_reload_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowAutoReloadChanceValue },
                RequiredPoints = Crossbow_Config.CrossbowStep4RequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-370, 240),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step2_balance", "crossbow_Step3_rapid", "crossbow_Step3_mark" },
                NextNodes = new List<string> { "crossbow_Step4_rapid_fire_lv2", "crossbow_Step5_final" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-1: 연속 발사 Lv2
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step4_rapid_fire_lv2",
                NameKey = "crossbow_rapid_fire_lv2_name",
                DescriptionKey = "crossbow_rapid_fire_lv2_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowRapidFireLv2ChanceValue, Crossbow_Config.CrossbowRapidFireLv2ShotCountValue, Crossbow_Config.CrossbowRapidFireLv2DamagePercentValue, Crossbow_Config.CrossbowRapidFireLv2BoltConsumptionValue },
                RequiredPoints = Crossbow_Config.CrossbowStep5RequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-450, 250),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step4_re" },
                NextNodes = new List<string> { "crossbow_Step6_expert" },
                ApplyEffect = (lv) => { }
            });

            // Step 5: 결전의 일격
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step5_final",
                NameKey = "crossbow_final_strike_name",
                DescriptionKey = "crossbow_final_strike_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowFinalStrikeHpThresholdValue, Crossbow_Config.CrossbowFinalStrikeDamageBonusValue },
                RequiredPoints = Crossbow_Config.CrossbowStep5RequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-430, 330),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step4_re" },
                NextNodes = new List<string> { "crossbow_Step6_expert" },
                ApplyEffect = (lv) => { }
            });

            // Step 6: 단 한 발 (액티브 스킬) - 표준 툴팁 형식
            manager.AddSkill(new SkillNode {
                Id = "crossbow_Step6_expert",
                NameKey = "crossbow_oneshot_name",
                DescriptionKey = "crossbow_oneshot_desc",
                DescriptionArgs = new object[] { Crossbow_Config.CrossbowOneShotDamageBonusValue, Crossbow_Config.CrossbowOneShotDurationValue },
                RequiredPoints = Crossbow_Config.CrossbowOneShotRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-550, 350),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crossbow_Step4_rapid_fire_lv2", "crossbow_Step5_final" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // ==================== 활 스킬 트리 ====================

            // Step 1: 활 전문가
            manager.AddSkill(new SkillNode {
                Id = "bow_Step1_damage",
                NameKey = "bow_skill_expert",
                DescriptionKey = "bow_expert_desc",
                DescriptionArgs = new object[] { Bow_Config.BowStep1ExpertDamageBonusValue },
                RequiredPoints = Bow_Config.BowExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-135, 140),
                Category = "원거리",
                IconNameLocked = "bow_lock",
                IconNameUnlocked = "bow_unlock",
                Prerequisites = new List<string> { "ranged_root" },
                NextNodes = new List<string> { "bow_Step2_focus", "bow_Step2_multishot" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-1: 집중 사격
            manager.AddSkill(new SkillNode {
                Id = "bow_Step2_focus",
                NameKey = "bow_focus_name",
                DescriptionKey = "bow_focus_desc",
                DescriptionArgs = new object[] { Bow_Config.BowStep2FocusCritBonusValue },
                RequiredPoints = Bow_Config.BowStep2RequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-205, 170),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step1_damage" },
                NextNodes = new List<string> { "bow_Step3_speedshot" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-2: 멀티샷 Lv1
            manager.AddSkill(new SkillNode {
                Id = "bow_Step2_multishot",
                NameKey = "bow_multishot_lv1_name",
                DescriptionKey = "bow_multishot_lv1_desc",
                DescriptionArgs = new object[] { Bow_Config.BowMultishotLv1ChanceValue },
                RequiredPoints = Bow_Config.BowMultishotRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-135, 220),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step1_damage" },
                NextNodes = new List<string> { "bow_Step3_speedshot" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, L.Get("bow_multishot_lv1_effect"),
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // Step 3: 활 숙련
            manager.AddSkill(new SkillNode {
                Id = "bow_Step3_speedshot",
                NameKey = "bow_proficiency_name",
                DescriptionKey = "bow_proficiency_desc",
                DescriptionArgs = new object[] { Bow_Config.BowStep3SpeedShotSkillBonusValue },
                RequiredPoints = Bow_Config.BowStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-205, 240),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step2_focus", "bow_Step2_multishot" },
                NextNodes = new List<string> { "bow_Step3_silentshot", "bow_Step4_multishot2", "bow_Step5_instinct" },
                ApplyEffect = (lv) => {
                    // 숙련도 보너스는 Skills.GetSkillLevel 패치에서 자동 적용됨
                    // 사망해도 유지되며, 스킬트리 해제 시 자동 초기화
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        float skillLevelBonus = Bow_Config.BowStep3SpeedShotSkillBonusValue;
                        SkillEffect.ShowSkillEffectText(player,
                            L.Get("bow_proficiency_effect", skillLevelBonus),
                            new Color(0.2f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo($"[활 숙련] 활 숙련도 +{skillLevelBonus} 보너스 활성화");
                    }
                }
            });

            // Step 3-1: 관통
            manager.AddSkill(new SkillNode {
                Id = "bow_Step3_silentshot",
                NameKey = "bow_penetration_name",
                DescriptionKey = "bow_penetration_desc",
                DescriptionArgs = new object[] { Bow_Config.BowStep3SilentShotDamageBonusValue },
                RequiredPoints = Bow_Config.BowStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-305, 285),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_speedshot" },
                NextNodes = new List<string> { "bow_Step5_master" },
                ApplyEffect = (lv) => { }
            });

            // Step 4: 멀티샷 Lv2
            manager.AddSkill(new SkillNode {
                Id = "bow_Step4_multishot2",
                NameKey = "bow_multishot_lv2_name",
                DescriptionKey = "bow_multishot_lv2_desc",
                DescriptionArgs = new object[] { Bow_Config.BowMultishotLv2ChanceValue },
                RequiredPoints = Bow_Config.BowStep4RequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-255, 315),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_speedshot" },
                NextNodes = new List<string> { "bow_Step5_master" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, L.Get("bow_multishot_lv2_effect"),
                            new Color(0.3f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // Step 5-1: 사냥 본능
            manager.AddSkill(new SkillNode {
                Id = "bow_Step5_instinct",
                NameKey = "bow_instinct_name",
                DescriptionKey = "bow_instinct_desc",
                DescriptionArgs = new object[] { Bow_Config.BowStep5InstinctCritBonusValue },
                RequiredPoints = Bow_Config.BowStep4RequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-205, 335),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_speedshot" },
                NextNodes = new List<string> { "bow_Step5_master" },
                ApplyEffect = (lv) => { }
            });

            // Step 5: 정조준
            manager.AddSkill(new SkillNode {
                Id = "bow_Step5_master",
                NameKey = "bow_precision_name",
                DescriptionKey = "bow_precision_desc",
                DescriptionArgs = new object[] { Bow_Config.BowStep5MasterCritDamageValue },
                RequiredPoints = Bow_Config.BowStep5RequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-305, 395),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step3_silentshot", "bow_Step4_multishot2", "bow_Step5_instinct" },
                NextNodes = new List<string> { "bow_Step6_critboost" },
                ApplyEffect = (lv) => { }
            });

            // Step 6: 폭발 화살 (액티브 스킬) - 표준 툴팁 형식
            manager.AddSkill(new SkillNode {
                Id = "bow_Step6_critboost",
                NameKey = "bow_explosive_name",
                DescriptionKey = "bow_explosive_desc",
                DescriptionArgs = new object[] { Bow_Config.BowExplosiveArrowDamageValue, Bow_Config.BowExplosiveArrowStaminaCostValue, Bow_Config.BowExplosiveArrowCooldownValue },
                RequiredPoints = Bow_Config.BowExplosiveArrowRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-365, 475),
                Category = "원거리",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_Step5_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // ==================== 지팡이 스킬 트리 ====================
            
            // Step 1: 지팡이 전문가
            manager.AddSkill(new SkillNode {
                Id = "staff_Step1_damage",
                NameKey = "staff_skill_expert_name",
                DescriptionKey = "staff_expert_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffExpertDamageValue },
                RequiredPoints = Staff_Config.StaffExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-160, 30),
                Category = "지팡이",
                IconNameLocked = "staff_lock",
                IconNameUnlocked = "staff_unlock",
                Prerequisites = new List<string> { "ranged_root" },
                NextNodes = new List<string> { "staff_Step2_focus", "staff_Step2_stream" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-1: 정신 집중
            manager.AddSkill(new SkillNode {
                Id = "staff_Step2_focus",
                NameKey = "staff_focus_name",
                DescriptionKey = "staff_focus_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffFocusEitrReductionValue },
                RequiredPoints = Staff_Config.StaffFocusRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-230, 70),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step1_damage" },
                NextNodes = new List<string> { "staff_Step3_amp" },
                ApplyEffect = (lv) => { }
            });

            // Step 2-2: 마법 흐름
            manager.AddSkill(new SkillNode {
                Id = "staff_Step2_stream",
                NameKey = "staff_stream_name",
                DescriptionKey = "staff_stream_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffStreamEitrBonusValue },
                RequiredPoints = Staff_Config.StaffStreamRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-230, 0),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step1_damage" },
                NextNodes = new List<string> { "staff_Step3_amp" },
                ApplyEffect = (lv) => { }
            });

            // Step 3: 마법 증폭
            manager.AddSkill(new SkillNode {
                Id = "staff_Step3_amp",
                NameKey = "staff_amp_name",
                DescriptionKey = "staff_amp_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffAmpDamageValue },
                RequiredPoints = Staff_Config.StaffAmpRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-310, 35),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step2_focus", "staff_Step2_stream" },
                NextNodes = new List<string> { "staff_Step4_reduction", "staff_Step4_range", "staff_Step4_surge" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-1: 냉기 속성
            manager.AddSkill(new SkillNode {
                Id = "staff_Step4_reduction",
                NameKey = "staff_frost_name",
                DescriptionKey = "staff_frost_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffFrostDamageBonusValue },
                RequiredPoints = Staff_Config.StaffFrostRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-400, 115),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step3_amp" },
                NextNodes = new List<string> { "staff_Step5_archmage" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-2: 화염 속성
            manager.AddSkill(new SkillNode {
                Id = "staff_Step4_range",
                NameKey = "staff_fire_name",
                DescriptionKey = "staff_fire_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffFireDamageBonusValue },
                RequiredPoints = Staff_Config.StaffFireRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-400, 35),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step3_amp" },
                NextNodes = new List<string> { "staff_Step5_archmage" },
                ApplyEffect = (lv) => { }
            });

            // Step 4-3: 번개 속성
            manager.AddSkill(new SkillNode {
                Id = "staff_Step4_surge",
                NameKey = "staff_lightning_name",
                DescriptionKey = "staff_lightning_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffLightningDamageBonusValue },
                RequiredPoints = Staff_Config.StaffLightningRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-400, -45),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step3_amp" },
                NextNodes = new List<string> { "staff_Step5_archmage" },
                ApplyEffect = (lv) => { }
            });

            // Step 5: 행운 마력 (기존 대마법사를 행운 마력으로 변경)
            manager.AddSkill(new SkillNode {
                Id = "staff_Step5_archmage",
                NameKey = "staff_luck_mana_name",
                DescriptionKey = "staff_luck_mana_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffLuckManaChanceValue },
                RequiredPoints = Staff_Config.StaffLuckManaRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-490, 35),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step4_reduction", "staff_Step4_range", "staff_Step4_surge" },
                NextNodes = new List<string> { "staff_Step6_dual_cast", "staff_Step6_heal" },
                ApplyEffect = (lv) => { }
            });

            // Step 6-1: 이중 시전 (액티브 스킬)
            manager.AddSkill(new SkillNode {
                Id = "staff_Step6_dual_cast",
                NameKey = "staff_dual_cast_name",
                DescriptionKey = "staff_dual_cast_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffDoubleCastProjectileCountValue, Staff_Config.StaffDoubleCastAngleOffsetValue, Staff_Config.StaffDoubleCastDamagePercentValue, Staff_Config.StaffDoubleCastEitrCostValue, Staff_Config.StaffDoubleCastCooldownValue },
                RequiredPoints = Staff_Config.StaffDoubleCastRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-580, 115),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step5_archmage" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // Step 6-2: 힐 (액티브 스킬)
            manager.AddSkill(new SkillNode {
                Id = "staff_Step6_heal",
                NameKey = "staff_heal_name",
                DescriptionKey = "staff_heal_full_desc",
                DescriptionArgs = new object[] { Staff_Config.StaffHealRangeValue, Staff_Config.StaffHealPercentageValue, Staff_Config.StaffHealEitrCostValue, Staff_Config.StaffHealCooldownValue },
                RequiredPoints = Staff_Config.StaffHealRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-580, -45),
                Category = "지팡이",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "staff_Step5_archmage" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });
        }

        /// <summary>
        /// 원거리 스킬 아이콘 통일 설정
        /// </summary>
        public static void SetupRangedSkillIcons()
        {
            var manager = SkillTreeManager.Instance;
            if (manager?.SkillNodes == null) return;

            // 활 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "원거리" && node.Id.StartsWith("bow_"))
                {
                    if (node.Id != "bow_Step1_damage") // 마스터리는 제외
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 석궁 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "원거리" && node.Id.StartsWith("crossbow_"))
                {
                    if (node.Id != "crossbow_Step1_damage") // 마스터리는 제외
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 지팡이 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "지팡이" && node.Id.StartsWith("staff_"))
                {
                    if (node.Id != "staff_Step1_damage") // 마스터리는 제외
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 원거리 루트 노드 위치 설정
            if (manager.SkillNodes.ContainsKey("ranged_root"))
            {
                manager.SkillNodes["ranged_root"].Position = new Vector2(-90, 60);
            }
        }

        /// <summary>
        /// 폭발 화살 동적 툴팁 생성 (R키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형 → 쿨타임 → 필요조건 → 필요포인트
        /// </summary>
        public static string GetExplosiveArrowTooltip()
        {
            var tooltip = "";

            // 1. 스킬명
            tooltip += $"<color=#FFD700><size=22>{L.Get("bow_explosive_name")}</size></color>\n\n";

            // 2. 설명
            tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{L.Get("bow_explosive_tooltip_desc")}</size></color>\n";

            // 3. 데미지
            tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color><color=#FFB6C1><size=16>{L.Get("bow_explosive_damage_format", Bow_Config.BowExplosiveArrowDamageValue)}</size></color>\n";

            // 4. 범위
            tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{L.Get("bow_explosive_range_format", 5)}</size></color>\n";

            // 5. 소모
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stamina_percent_format", Bow_Config.BowExplosiveArrowStaminaCostValue)}</size></color>\n";

            // 6. 스킬유형 (R키 강조)
            tooltip += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#FFD700><size=16>{L.Get("skill_type_active_key", "R")}</size></color>\n";

            // 7. 쿨타임
            tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{L.Get("seconds_format", Bow_Config.BowExplosiveArrowCooldownValue)}</size></color>\n";

            // 8. 필요조건
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{L.Get("requirement_bow_equip")}</size></color>\n";

            // 9. 필요포인트
            tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>4</size></color>";

            return tooltip.TrimEnd('\n');
        }

        /// <summary>
        /// 단 한 발 동적 툴팁 생성 (R키 액티브 스킬)
        /// 표준 항목 순서: 스킬명 → 설명 → 데미지 → 범위 → 소모 → 스킬유형 → 쿨타임 → 필요조건 → 필요포인트
        /// </summary>
        public static string GetOneShotTooltip()
        {
            var tooltip = "";

            // 1. 스킬명
            tooltip += $"<color=#FFD700><size=22>{L.Get("crossbow_oneshot_name")}</size></color>\n\n";

            // 2. 설명
            tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{L.Get("crossbow_oneshot_tooltip_desc", Crossbow_Config.CrossbowOneShotDurationValue)}</size></color>\n";

            // 3. 데미지
            tooltip += $"<color=#FF6B6B><size=16>{L.Get("tooltip_damage")}: </size></color><color=#FFB6C1><size=16>{L.Get("attack_power_bonus_format", Crossbow_Config.CrossbowOneShotDamageBonusValue)}</size></color>\n";

            // 4. 범위 (넉백)
            tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{L.Get("knockback_format", Crossbow_Config.CrossbowOneShotKnockbackValue)}</size></color>\n";

            // 5. 소모
            tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("stamina_format", 20)}</size></color>\n";

            // 6. 스킬유형 (R키 강조)
            tooltip += $"<color=#9400D3><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#FFD700><size=16>{L.Get("skill_type_active_key", "R")}</size></color>\n";

            // 7. 쿨타임
            tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{L.Get("seconds_format", Crossbow_Config.CrossbowOneShotCooldownValue)}</size></color>\n";

            // 8. 필요조건
            tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirements")}: </size></color><color=#00FF00><size=16>{L.Get("requirement_crossbow_equip")}</size></color>\n";

            // 9. 필요포인트
            tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_points")}: </size></color><color=#FF6B6B><size=16>4</size></color>";

            return tooltip.TrimEnd('\n');
        }
    }
}
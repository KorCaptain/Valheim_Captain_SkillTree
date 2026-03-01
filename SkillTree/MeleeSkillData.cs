using System;
using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    public static class MeleeSkillData
    {
        public static void RegisterMeleeSkills()
        {
            var manager = SkillTreeManager.Instance;

            // ==================== 근접 전문가 루트 노드 ====================
            manager.AddSkill(new SkillNode {
                Id = "melee_root",
                NameKey = "melee_skill_expert",
                DescriptionKey = "melee_desc_expert",
                RequiredPoints = 2,
                MaxLevel = 1,
                Position = new Vector2(90, 60),
                Category = "근접",
                IconNameLocked = "melee_lock",
                IconNameUnlocked = "melee_unlock",
                NextNodes = new List<string> { "knife_expert_backstab", "sword_expert", "spear_expert", "polearm_expert", "mace_Step1_damage" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, L.Get("melee_root_effect"));
                    }
                }
            });

            // ==================== 단검 스킬 트리 ====================
            // 단검 트리 단계별/분기별/포인트/효과 구조 추가
            manager.AddSkill(new SkillNode
            {
                Id = "knife_expert_backstab",
                NameKey = "knife_skill_expert",
                DescriptionKey = "knife_desc_expert",
                DescriptionArgs = new object[] { Knife_Config.KnifeExpertBackstabBonusValue },
                RequiredPoints = Knife_Config.KnifeExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(135, 165),
                Category = "근접",
                IconName = "dagger_unlock",
                IconNameLocked = "dagger_lock",
                IconNameUnlocked = "dagger_unlock",
                Prerequisites = new List<string> { "melee_root" },
                NextNodes = new List<string> { "knife_step2_evasion", "knife_step3_move_speed" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step2_evasion",
                NameKey = "knife_skill_evasion",
                DescriptionKey = "knife_desc_evasion",
                DescriptionArgs = new object[] { Knife_Config.KnifeEvasionBonusValue },
                RequiredPoints = Knife_Config.KnifeEvasionRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(170, 280),
                Category = "근접",
                IconName = "knife_poison",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_expert_backstab" },
                NextNodes = new List<string> { "knife_step4_attack_damage" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step3_move_speed",
                NameKey = "knife_skill_move_speed",
                DescriptionKey = "knife_desc_move_speed",
                DescriptionArgs = new object[] { Knife_Config.KnifeMoveSpeedBonusValue },
                RequiredPoints = Knife_Config.KnifeMoveSpeedRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(195, 220),
                Category = "근접",
                IconName = "knife_crit",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_expert_backstab" },
                NextNodes = new List<string> { "knife_step4_attack_damage" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step4_attack_damage",
                NameKey = "knife_skill_attack_speed",
                DescriptionKey = "knife_desc_attack_speed",
                DescriptionArgs = new object[] { Knife_Config.KnifeAttackDamageBonusValue },
                RequiredPoints = Knife_Config.KnifeAttackSpeedRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(230, 308),
                Category = "근접",
                IconName = "knife_weakpoint",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_step2_evasion", "knife_step3_move_speed" },
                NextNodes = new List<string> { "knife_step5_crit_rate", "knife_step6_combat_damage", "knife_step7_execution" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step5_crit_rate",
                NameKey = "knife_skill_crit_rate",
                DescriptionKey = "knife_desc_attack_evasion",
                DescriptionArgs = new object[] { Knife_Config.KnifeAttackEvasionBonusValue, Knife_Config.KnifeAttackEvasionDurationValue },
                RequiredPoints = Knife_Config.KnifeCritRateRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(215, 410),
                Category = "근접",
                IconName = "knife_stealth",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_step4_attack_damage" },
                NextNodes = new List<string> { "knife_step8_assassination" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step6_combat_damage",
                NameKey = "knife_skill_combat_damage",
                DescriptionKey = "knife_desc_combat_damage",
                DescriptionArgs = new object[] { Knife_Config.KnifeCombatDamageBonusValue },
                RequiredPoints = Knife_Config.KnifeCombatDamageRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(255, 370),
                Category = "근접",
                IconName = "knife_poison_master",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_step4_attack_damage" },
                NextNodes = new List<string> { "knife_step8_assassination" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step7_execution",
                NameKey = "knife_skill_execution",
                DescriptionKey = "knife_desc_execution",
                DescriptionArgs = new object[] { Knife_Config.KnifeExecutionCritDamageValue, Knife_Config.KnifeExecutionCritChanceValue },
                RequiredPoints = Knife_Config.KnifeExecutionRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(295, 330),
                Category = "근접",
                IconName = "knife_crit_master",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_step4_attack_damage" },
                NextNodes = new List<string> { "knife_step8_assassination" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step8_assassination",
                NameKey = "knife_skill_assassination",
                DescriptionKey = "knife_desc_assassination",
                DescriptionArgs = new object[] { Knife_Config.KnifeAssassinationStaggerChanceValue, Knife_Config.KnifeAssassinationRequiredHitsValue },
                RequiredPoints = Knife_Config.KnifeAssassinationRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(305, 450),
                Category = "근접",
                IconName = "knife_speed",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_step5_crit_rate", "knife_step6_combat_damage", "knife_step7_execution" },
                NextNodes = new List<string> { "knife_step9_assassin_heart" },
                ApplyEffect = (lv) => { }
            });
            manager.AddSkill(new SkillNode
            {
                Id = "knife_step9_assassin_heart",
                NameKey = "knife_skill_assassin",
                DescriptionKey = "knife_desc_assassin_main",
                DescriptionArgs = new object[] { Knife_Config.KnifeAssassinHeartTeleportRangeValue, Knife_Config.KnifeAssassinHeartTeleportBehindValue, Knife_Config.KnifeAssassinHeartStunDurationValue, Knife_Config.KnifeAssassinHeartAttackCountValue },
                RequiredPoints = Knife_Config.KnifeAssassinHeartRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(395, 480),
                Category = "근접",
                IconName = "knife_assassin",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "knife_step8_assassination" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });



            // 검 스킬트리 기준점 및 거리/각도 테이블
            Vector2 swordBase = new Vector2(170, 120); // 연속베기(중심) X+20, Y-20
            float sword_r2 = 80f, sword_r3 = 160f, sword_r4 = 240f, sword_r5 = 320f; // sword_r6 삭제
            // 각도(도, 시계방향)
            float sword_a2_1 = 60f; // 2단계
            float sword_a3 = 45f;   // 3단계
            float sword_a4_2 = 45f, sword_a4_3 = 30f; // 4단계
            float sword_a5 = 45f;   // 5단계
            // float sword_a6 = 45f; // 삭제
            // 좌표 계산 함수
            Vector2 SwordPos(float deg, float r) => swordBase + new Vector2(Mathf.Cos(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad)) * r;

            // 1단계: 검 전문가
            manager.AddSkill(new SkillNode
            {
                Id = "sword_expert",
                NameKey = "sword_skill_expert",
                DescriptionKey = "sword_desc_expert",
                DescriptionArgs = new object[] { SkillTreeConfig.SwordExpertDamageValue, SkillTreeConfig.SwordStep1ExpertComboBonusValue, SkillTreeConfig.SwordStep1ExpertDurationValue },
                RequiredPoints = Sword_Config.SwordExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = swordBase,
                Category = "근접",
                IconName = "sword_unlock",
                IconNameLocked = "sword_lock",
                IconNameUnlocked = "sword_unlock",
                Prerequisites = new List<string> { "melee_root" },
                NextNodes = new List<string> { "sword_step1_fastslash", "sword_step1_counter" },
                ApplyEffect = (lv) => { }
            });
            // 2단계: 빠른 베기
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step1_fastslash",
                NameKey = "sword_skill_fast_slash",
                DescriptionKey = "sword_desc_fast_slash",
                DescriptionArgs = new object[] { SkillTreeConfig.SwordStep1FastSlashSpeedValue },
                RequiredPoints = Sword_Config.SwordStep1FastSlashRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = SwordPos(sword_a2_1, sword_r2),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_expert" },
                NextNodes = new List<string> { "sword_step3_riposte" },
                ApplyEffect = (lv) => { }
            });

            // 2단계: 반격 자세
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step1_counter",
                NameKey = "sword_skill_counter",
                DescriptionKey = "sword_desc_counter",
                DescriptionArgs = new object[] { SkillTreeConfig.SwordStep1CounterDurationValue, SkillTreeConfig.SwordStep1CounterDefenseBonusValue },
                RequiredPoints = Sword_Config.SwordStep1CounterRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(245, 145),
                Category = "근접",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_expert" },
                NextNodes = new List<string> { "sword_step3_riposte" },
                ApplyEffect = (lv) => { }
            });
            // 3단계: 칼날 되치기 (티어 순서 변경: 연속베기와 교체)
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step3_riposte",
                NameKey = "sword_skill_riposte",
                DescriptionKey = "sword_desc_riposte",
                DescriptionArgs = new object[] { SkillTreeConfig.SwordStep3BladeCounterBonusValue },
                RequiredPoints = Sword_Config.SwordStep3RiposteRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = SwordPos(sword_a3, sword_r3),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_step1_fastslash", "sword_step1_counter" },
                NextNodes = new List<string> { "sword_step2_combo", "sword_step3_allinone" },
                ApplyEffect = (lv) => { }
            });
            // 4단계: 연속베기 (티어 순서 변경: 칼날 되치기와 교체)
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step2_combo",
                NameKey = "sword_skill_combo",
                DescriptionKey = "sword_desc_combo",
                DescriptionArgs = new object[] { SkillTreeConfig.SwordStep2ComboSlashBonusValue, SkillTreeConfig.SwordStep2ComboSlashDurationValue },
                RequiredPoints = Sword_Config.SwordStep2ComboSlashRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = SwordPos(sword_a4_2, sword_r4),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_step3_riposte" },
                NextNodes = new List<string> { "sword_step4_duel" },
                ApplyEffect = (lv) => { }
            });

            // 4단계: 공방일체
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step3_allinone",
                NameKey = "sword_skill_all_in_one",
                DescriptionKey = "sword_desc_all_in_one",
                DescriptionArgs = new object[] { SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue, SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue },
                RequiredPoints = Sword_Config.SwordStep3AllInOneRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = SwordPos(sword_a4_3, sword_r4),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_step3_riposte" },
                NextNodes = new List<string> { "sword_step4_duel" },
                ApplyEffect = (lv) => { }
            });
            // 5단계: 진검승부 (4단계 중 하나 이상 선행)
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step4_duel",
                NameKey = "sword_skill_duel",
                DescriptionKey = "sword_desc_duel",
                DescriptionArgs = new object[] { SkillTreeConfig.SwordStep4TrueDuelSpeedValue },
                RequiredPoints = Sword_Config.SwordStep4TrueDuelRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = SwordPos(sword_a5, sword_r5),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_step2_combo", "sword_step3_allinone" },
                NextNodes = new List<string> { "sword_step5_finalcut", "sword_step5_defswitch" },
                ApplyEffect = (lv) => { }
            });
            // 6단계: Sword Slash (액티브 G키 스킬)
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step5_finalcut",
                NameKey = "sword_skill_rush_slash",
                DescriptionKey = "sword_desc_rush_slash",
                DescriptionArgs = new object[] { Sword_Config.RushSlashInitialDistanceValue },
                RequiredPoints = Sword_Config.RushSlashRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(435, 420),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_step4_duel" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // 6단계: 패링 돌격 (액티브 H키 스킬)
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step5_defswitch",
                NameKey = "sword_skill_parry_rush",
                DescriptionKey = "sword_desc_parry_rush",
                DescriptionArgs = new object[] { Sword_Config.ParryRushDurationValue },
                RequiredPoints = Sword_Config.SwordStep5DefenseSwitchRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(470, 360),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "sword_step4_duel" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            // ==================== 창 스킬 트리 ====================

            // 1단계: 창 전문가
            manager.AddSkill(new SkillNode {
                Id = "spear_expert",
                NameKey = "spear_skill_expert",
                DescriptionKey = "spear_desc_expert",
                DescriptionArgs = new object[] { SkillTreeConfig.SpearStep1AttackSpeedValue, SkillTreeConfig.SpearStep1DamageBonusValue, SkillTreeConfig.SpearStep1DurationValue },
                RequiredPoints = Spear_Config.SpearExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(205, 15),
                Category = "근접",
                Prerequisites = new List<string> { "melee_root" },
                NextNodes = new List<string> { "spear_Step1_throw", "spear_Step1_crit" },
                IconName = "spear_unlock",
                IconNameLocked = "spear_lock",
                IconNameUnlocked = "spear_unlock",
                ApplyEffect = (lv) => { }
            });

            // 2-1단계: 투창 전문가 (패시브)
            manager.AddSkill(new SkillNode {
                Id = "spear_Step1_throw",
                NameKey = "spear_skill_throw",
                DescriptionKey = "spear_desc_throw",
                DescriptionArgs = new object[] { SkillTreeConfig.SpearStep2ThrowDamageValue },
                RequiredPoints = Spear_Config.SpearStep1RequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(275, 65),
                Category = "근접",
                Prerequisites = new List<string> { "spear_expert" },
                NextNodes = new List<string> { "spear_Step3_pierce" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 2-2단계: 급소 찌르기
            manager.AddSkill(new SkillNode {
                Id = "spear_Step1_crit",
                NameKey = "spear_skill_crit",
                DescriptionKey = "spear_desc_crit",
                DescriptionArgs = new object[] { SkillTreeConfig.SpearStep2CritDamageBonusValue },
                RequiredPoints = Spear_Config.SpearStep1RequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(290, 15),
                Category = "근접",
                Prerequisites = new List<string> { "spear_expert" },
                NextNodes = new List<string> { "spear_Step3_pierce" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 3단계: 연격창 (티어 순서 변경: 회피찌르기보다 먼저)
            manager.AddSkill(new SkillNode {
                Id = "spear_Step3_pierce",
                NameKey = "spear_skill_pierce",
                DescriptionKey = "spear_desc_pierce",
                DescriptionArgs = new object[] { SkillTreeConfig.SpearStep3PierceDamageBonusValue },
                RequiredPoints = Spear_Config.SpearStep2RequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(360, 60),
                Category = "근접",
                Prerequisites = new List<string> { "spear_Step1_throw", "spear_Step1_crit" },
                NextNodes = new List<string> { "spear_Step2_evasion", "spear_Step3_quick" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 4-1단계: 회피 찌르기 (티어 순서 변경: 연격창 다음)
            manager.AddSkill(new SkillNode {
                Id = "spear_Step2_evasion",
                NameKey = "spear_skill_evasion",
                DescriptionKey = "spear_desc_evasion",
                DescriptionArgs = new object[] { SkillTreeConfig.SpearStep3EvasionDamageBonusValue, Spear_Config.SpearStep3EvasionStaminaReductionValue },
                RequiredPoints = Spear_Config.SpearStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(435, 140),
                Category = "근접",
                Prerequisites = new List<string> { "spear_Step3_pierce" },
                NextNodes = new List<string> { "spear_Step4_triple" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 4-2단계: 폭발창
            manager.AddSkill(new SkillNode {
                Id = "spear_Step3_quick",
                NameKey = "spear_skill_explosion",
                DescriptionKey = "spear_desc_explosion",
                DescriptionArgs = new object[] { Spear_Config.SpearExplosionChanceValue, Spear_Config.SpearExplosionRadiusValue, Spear_Config.SpearExplosionDamageBonusValue },
                RequiredPoints = Spear_Config.SpearStep3RequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(455, 60),
                Category = "근접",
                Prerequisites = new List<string> { "spear_Step3_pierce" },
                NextNodes = new List<string> { "spear_Step4_triple" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 5단계: 이연창
            manager.AddSkill(new SkillNode {
                Id = "spear_Step4_triple",
                NameKey = "spear_skill_dual",
                DescriptionKey = "spear_desc_dual",
                DescriptionArgs = new object[] { Spear_Config.SpearDualDurationValue, Spear_Config.SpearDualDamageBonusValue },
                RequiredPoints = Spear_Config.SpearStep4RequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(525, 120),
                Category = "근접",
                Prerequisites = new List<string> { "spear_Step2_evasion", "spear_Step3_quick" },
                NextNodes = new List<string> { "spear_Step5_penetrate", "spear_Step5_combo" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 6-1단계: 꿰뚫는 창
            manager.AddSkill(new SkillNode {
                Id = "spear_Step5_penetrate",
                NameKey = "spear_skill_penetrate",
                DescriptionKey = "spear_desc_penetrate",
                DescriptionArgs = new object[] { Spear_Config.SpearStep6PenetrateBuffDurationValue, Spear_Config.SpearStep6PenetrateComboCountValue },
                RequiredPoints = Spear_Config.SpearPenetrateRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(615, 135),
                Category = "근접",
                Prerequisites = new List<string> { "spear_Step4_triple" },
                NextNodes = new List<string>(),
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 6-2단계: 연공창
            manager.AddSkill(new SkillNode {
                Id = "spear_Step5_combo",
                NameKey = "spear_skill_combo",
                DescriptionKey = "spear_desc_combo",
                DescriptionArgs = new object[] { SkillTreeConfig.SpearStep6ComboDamageValue },
                RequiredPoints = Spear_Config.SpearComboRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(615, 90),
                Category = "근접",
                Prerequisites = new List<string> { "spear_Step4_triple" },
                NextNodes = new List<string>(),
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });
            
            // ==================== 폴암 스킬 트리 ====================

            // 1단계: 폴암 전문가
            manager.AddSkill(new SkillNode {
                Id = "polearm_expert",
                NameKey = "polearm_skill_expert",
                DescriptionKey = "polearm_desc_expert",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmExpertRangeBonusValue },
                RequiredPoints = Polearm_Config.PolearmExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(230, -30),
                Category = "근접",
                Prerequisites = new List<string> { "melee_root" },
                NextNodes = new List<string> { "polearm_step1_spin" },
                IconName = "polearm_unlock",
                IconNameLocked = "polearm_lock",
                IconNameUnlocked = "polearm_unlock",
                ApplyEffect = (lv) => { }
            });

            // 2단계: 회전베기
            manager.AddSkill(new SkillNode {
                Id = "polearm_step1_spin",
                NameKey = "polearm_skill_spin",
                DescriptionKey = "polearm_desc_spin",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmStep1SpinWheelDamageValue },
                RequiredPoints = Polearm_Config.PolearmSpinWheelRequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(310, -30),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_expert" },
                NextNodes = new List<string> { "polearm_step4_charge", "polearm_step2_hero" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 3-1단계: 폴암강화 (티어 순서 변경: 제압 공격과 교환)
            manager.AddSkill(new SkillNode {
                Id = "polearm_step4_charge",
                NameKey = "polearm_skill_charge",
                DescriptionKey = "polearm_desc_charge",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmStep4ChargeDamageBonusValue },
                RequiredPoints = Polearm_Config.PolearmPolearmBoostRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(375, 25),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_step1_spin" },
                NextNodes = new List<string> { "polearm_step3_area" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 3-2단계: 영웅 타격
            manager.AddSkill(new SkillNode {
                Id = "polearm_step2_hero",
                NameKey = "polearm_skill_hero",
                DescriptionKey = "polearm_desc_hero",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmStep2HeroKnockbackChanceValue },
                RequiredPoints = Polearm_Config.PolearmHeroStrikeRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(385, -55),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_step1_spin" },
                NextNodes = new List<string> { "polearm_step3_area" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 4단계: 광역 강타
            manager.AddSkill(new SkillNode {
                Id = "polearm_step3_area",
                NameKey = "polearm_skill_area",
                DescriptionKey = "polearm_desc_area",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmStep3AreaComboBonusValue, SkillTreeConfig.PolearmStep3AreaComboDurationValue },
                RequiredPoints = Polearm_Config.PolearmAreaComboRequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(465, -5),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_step4_charge", "polearm_step2_hero" },
                NextNodes = new List<string> { "polearm_step3_ground", "polearm_step4_moon", "polearm_step1_suppress" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 5-1단계: 지면 강타
            manager.AddSkill(new SkillNode {
                Id = "polearm_step3_ground",
                NameKey = "polearm_skill_ground",
                DescriptionKey = "polearm_desc_ground",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmStep3GroundWheelDamageValue },
                RequiredPoints = Polearm_Config.PolearmGroundWheelRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(545, 65),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_step3_area" },
                NextNodes = new List<string> { "polearm_step5_king" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 5-2단계: 반달 베기
            manager.AddSkill(new SkillNode {
                Id = "polearm_step4_moon",
                NameKey = "polearm_skill_moon",
                DescriptionKey = "polearm_desc_moon",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmStep4MoonRangeBonusValue, SkillTreeConfig.PolearmStep4MoonStaminaReductionValue },
                RequiredPoints = Polearm_Config.PolearmMoonSlashRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(555, 20),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_step3_area" },
                NextNodes = new List<string> { "polearm_step5_king" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 5-3단계: 제압 공격 (티어 순서 변경: 폴암강화와 교환)
            manager.AddSkill(new SkillNode {
                Id = "polearm_step1_suppress",
                NameKey = "polearm_skill_suppress",
                DescriptionKey = "polearm_desc_suppress",
                DescriptionArgs = new object[] { SkillTreeConfig.PolearmStep1SuppressDamageValue },
                RequiredPoints = Polearm_Config.PolearmSuppressRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(565, -25),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_step3_area" },
                NextNodes = new List<string> { "polearm_step5_king" },
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });

            // 6단계: 관통 돌격
            manager.AddSkill(new SkillNode {
                Id = "polearm_step5_king",
                NameKey = "polearm_skill_king",
                DescriptionKey = "polearm_desc_king",
                DescriptionArgs = new object[] { Polearm_Config.PolearmPierceChargeDashDistanceValue },
                RequiredPoints = Polearm_Config.PolearmKingRequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(625, 20),
                Category = "근접",
                Prerequisites = new List<string> { "polearm_step3_ground", "polearm_step4_moon", "polearm_step1_suppress" },
                NextNodes = new List<string>(),
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                ApplyEffect = (lv) => { }
            });
            
            // 4. 둔기 스킬 트리 (SkillTreeData.cs와 동일한 구조)

            // 1단계: 둔기 전문가
            manager.AddSkill(new SkillNode {
                Id = "mace_Step1_damage",
                NameKey = "mace_skill_expert",
                DescriptionKey = "mace_desc_expert",
                DescriptionArgs = new object[] { Mace_Config.MaceExpertDamageBonusValue, Mace_Config.MaceExpertStunChanceValue, Mace_Config.MaceExpertStunDurationValue },
                RequiredPoints = Mace_Config.MaceExpertRequiredPointsValue,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(185, 65),
                Category = "근접",
                Prerequisites = new List<string> { "melee_root" },
                NextNodes = new List<string> { "mace_Step2_stun_boost" },
                IconName = "mace_unlock",
                IconNameLocked = "mace_lock",
                IconNameUnlocked = "mace_unlock",
                ApplyEffect = (lv) => { }
            });

            // 2단계: 기절 강화
            manager.AddSkill(new SkillNode {
                Id = "mace_Step2_stun_boost",
                NameKey = "mace_skill_stun_boost",
                DescriptionKey = "mace_desc_stun_boost",
                DescriptionArgs = new object[] { Mace_Config.MaceStep2StunChanceBonusValue, Mace_Config.MaceStep2StunDurationBonusValue },
                RequiredPoints = Mace_Config.MaceStep2RequiredPointsValue,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(245, 105),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step1_damage" },
                NextNodes = new List<string> { "mace_Step3_branch_guard", "mace_Step3_branch_heavy" },
                ApplyEffect = (lv) => { }
            });

            // 3-1단계: 방패 진형
            manager.AddSkill(new SkillNode {
                Id = "mace_Step3_branch_guard",
                NameKey = "mace_skill_guard_boost",
                DescriptionKey = "mace_desc_guard_boost",
                DescriptionArgs = new object[] { Mace_Config.MaceStep3GuardArmorBonusValue },
                RequiredPoints = Mace_Config.MaceStep3GuardRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(290, 180),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step2_stun_boost" },
                NextNodes = new List<string> { "mace_Step4_push" },
                ApplyEffect = (lv) => { }
            });

            // 3-2단계: 무거운 일격
            manager.AddSkill(new SkillNode {
                Id = "mace_Step3_branch_heavy",
                NameKey = "mace_skill_heavy_strike",
                DescriptionKey = "mace_desc_heavy_strike",
                DescriptionArgs = new object[] { Mace_Config.MaceStep3HeavyDamageBonusValue },
                RequiredPoints = Mace_Config.MaceStep3HeavyRequiredPointsValue,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(330, 120),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step2_stun_boost" },
                NextNodes = new List<string> { "mace_Step4_push" },
                ApplyEffect = (lv) => { }
            });

            // 4단계: 3연격 넉백
            manager.AddSkill(new SkillNode {
                Id = "mace_Step4_push",
                NameKey = "mace_skill_knockback",
                DescriptionKey = "mace_desc_knockback",
                DescriptionArgs = new object[] { Mace_Config.MaceStep4KnockbackChanceValue },
                RequiredPoints = Mace_Config.MaceStep4RequiredPointsValue,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(395, 180),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step3_branch_guard", "mace_Step3_branch_heavy" },
                NextNodes = new List<string> { "mace_Step5_tank", "mace_Step5_dps" },
                ApplyEffect = (lv) => { }
            });

            // 5-1단계: 방패 제압 (Tank)
            manager.AddSkill(new SkillNode {
                Id = "mace_Step5_tank",
                NameKey = "mace_skill_tanker",
                DescriptionKey = "mace_desc_tanker",
                DescriptionArgs = new object[] { Mace_Config.MaceStep5TankHealthBonusValue, Mace_Config.MaceStep5TankDamageReductionValue },
                RequiredPoints = Mace_Config.MaceStep5TankRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(450, 240),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step4_push" },
                NextNodes = new List<string> { "mace_Step6_grandmaster" },
                ApplyEffect = (lv) => { }
            });

            // 5-2단계: 분쇄자 (DPS)
            manager.AddSkill(new SkillNode {
                Id = "mace_Step5_dps",
                NameKey = "mace_skill_dps_boost",
                DescriptionKey = "mace_desc_dps_boost",
                DescriptionArgs = new object[] { Mace_Config.MaceStep5DpsDamageBonusValue, Mace_Config.MaceStep5DpsAttackSpeedBonusValue },
                RequiredPoints = Mace_Config.MaceStep5DpsRequiredPointsValue,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(475, 190),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step4_push" },
                NextNodes = new List<string> { "mace_Step6_grandmaster" },
                ApplyEffect = (lv) => { }
            });

            // 6단계: 둔기 마스터
            manager.AddSkill(new SkillNode {
                Id = "mace_Step6_grandmaster",
                NameKey = "mace_skill_grandmaster",
                DescriptionKey = "mace_desc_grandmaster",
                DescriptionArgs = new object[] { Mace_Config.MaceStep6ArmorBonusValue },
                RequiredPoints = Mace_Config.MaceStep6RequiredPointsValue,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(540, 260),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step5_tank", "mace_Step5_dps" },
                NextNodes = new List<string> { "mace_Step7_fury_hammer" },
                ApplyEffect = (lv) => { }
            });

            // 7-1단계: 분노의 망치 (G키 액티브)
            manager.AddSkill(new SkillNode {
                Id = "mace_Step7_fury_hammer",
                NameKey = "mace_skill_fury",
                DescriptionKey = "mace_desc_fury_attack",
                DescriptionArgs = new object[] { 5 },
                RequiredPoints = Mace_Config.FuryHammerRequiredPointsValue,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(635, 255),
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step6_grandmaster" },
                NextNodes = new List<string> { "mace_Step7_guardian_heart" },
                ApplyEffect = (lv) => { }
            });

            // 7-2단계: 수호자의 진심 (G키 액티브) - 둔기 마스터 위쪽으로 이동 (x+20, y+10)
            manager.AddSkill(new SkillNode {
                Id = "mace_Step7_guardian_heart",
                NameKey = "mace_skill_guardian",
                DescriptionKey = "mace_desc_guardian_buff",
                DescriptionArgs = new object[] { Mace_Config.GuardianHeartDurationValue },
                RequiredPoints = Mace_Config.GuardianHeartRequiredPointsValue,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(600, 320), // 현 위치(580, 300)에서 x+20, y+20
                Category = "근접",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mace_Step6_grandmaster" }, // 둔기 마스터에서 직접 연결
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });
        }
        
        /// <summary>
        /// 근접 스킬 아이콘 설정
        /// </summary>
        public static void SetupMeleeSkillIcons()
        {
            var manager = SkillTreeManager.Instance;
            if (manager?.SkillNodes == null) return;

            // 단검 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "근접" && node.Id.StartsWith("knife_"))
                {
                    if (node.Id != "knife_expert_backstab") // 단검 전문가는 전용 아이콘
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 검 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "근접" && node.Id.StartsWith("sword_"))
                {
                    if (node.Id != "sword_expert") // 검 전문가는 전용 아이콘
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 둔기 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "근접" && node.Id.StartsWith("mace_"))
                {
                    if (node.Id != "mace_Step1_damage") // 둔기 전문가는 전용 아이콘
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 창 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "근접" && node.Id.StartsWith("spear_"))
                {
                    if (node.Id != "spear_expert") // 투창 전문가는 전용 아이콘
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 폴암 스킬 아이콘 설정
            foreach (var kvp in manager.SkillNodes)
            {
                var node = kvp.Value;
                if (node.Category == "근접" && node.Id.StartsWith("polearm_"))
                {
                    if (node.Id != "polearm_expert") // 폴암 전문가는 전용 아이콘
                    {
                        node.IconNameLocked = "all_skill_lock";
                        node.IconNameUnlocked = "all_skill_unlock";
                    }
                }
            }

            // 근접 루트 노드 위치 설정
            if (manager.SkillNodes.ContainsKey("melee_root"))
            {
                manager.SkillNodes["melee_root"].Position = new Vector2(90, 60);
            }

            // 모든 근접 무기 스킬 시스템 초기화 및 툴팁 업데이트
            try
            {
                Knife_Skill.InitializeKnifeSkills();
                
                // 모든 근접 무기 툴팁 강제 업데이트 (지연 후)
                Plugin.Instance.StartCoroutine(DelayedMeleeTooltipsUpdate());
                
                Plugin.Log.LogDebug("[근접 스킬] 모든 근접 무기 스킬 시스템 초기화 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 근접 무기 스킬 시스템 초기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 지연된 모든 근접 무기 툴팁 업데이트 (스킬 노드 등록 완료 대기)
        /// </summary>
        private static System.Collections.IEnumerator DelayedMeleeTooltipsUpdate()
        {
            yield return new UnityEngine.WaitForSeconds(1f);
            
            try
            {
                // 모든 근접 무기 툴팁 강제 업데이트
                Knife_Tooltip.UpdateKnifeTooltips();
                Sword_Tooltip.UpdateSwordTooltips();
                Spear_Tooltip.UpdateSpearTooltips();
                Polearm_Tooltip.UpdatePolearmTooltips();
                Mace_Tooltip.UpdateMaceTooltips();
                Plugin.Log.LogDebug("[지연된 툴팁] 모든 근접 무기 툴팁 업데이트 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[근접 툴팁] 지연된 툴팁 업데이트 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 지연된 단검 툴팁 업데이트 (스킬 노드 등록 완료 대기) - 호환성 유지
        /// </summary>
        private static System.Collections.IEnumerator DelayedKnifeTooltipUpdate()
        {
            yield return new UnityEngine.WaitForSeconds(1f);
            
            try
            {
                Knife_Tooltip.UpdateKnifeTooltips();
                Plugin.Log.LogDebug("[지연된] 지연된 툴팁 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 툴팁] 지연된 툴팁 업데이트 실패: {ex.Message}");
            }
        }

    }
}
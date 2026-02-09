using System;
using System.Collections.Generic;
using UnityEngine;

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
                Name = "근접 전문가",
                Description = "근접무기 공격력 +3",
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
                        SkillEffect.DrawFloatingText(player, "근접무기 공격력 +3 (고정값)");
                    }
                }
            });

            // ==================== 단검 스킬 트리 ====================
            // 단검 트리 단계별/분기별/포인트/효과 구조 추가
            manager.AddSkill(new SkillNode
            {
                Id = "knife_expert_backstab",
                Name = "<color=#FFD700><size=22>단검 전문가</size></color>",
                Description = "",
                RequiredPoints = 2,
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
                Name = "<color=#FFD700><size=22>회피 숙련</size></color>",
                Description = "",
                RequiredPoints = 3,
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
                Name = "<color=#FFD700><size=22>빠른 움직임</size></color>",
                Description = "",
                RequiredPoints = 2,
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
                Name = "<color=#FFD700><size=22>빠른 공격</size></color>",
                Description = "",
                RequiredPoints = 3,
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
                Name = "<color=#FFD700><size=22>치명타 숙련</size></color>",
                Description = "",
                RequiredPoints = 3,
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
                Name = "<color=#FFD700><size=22>치명적 피해</size></color>",
                Description = "",
                RequiredPoints = 2,
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
                Name = "<color=#FFD700><size=22>암살자</size></color>",
                Description = "",
                RequiredPoints = 3,
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
                Name = "<color=#FFD700><size=22>암살술</size></color>",
                Description = "",
                RequiredPoints = 2,
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
                Name = "<color=#FFD700><size=22>암살자의 심장</size></color>",
                Description = "",
                RequiredPoints = 3,
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
                Name = "검 전문가",
                Description = "",
                RequiredPoints = 2,
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
                Name = "빠른 베기",
                Description = "",
                RequiredPoints = 2,
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
                Name = "반격 자세",
                Description = "",
                RequiredPoints = 3,
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
                Name = "칼날 되치기",
                Description = "",
                RequiredPoints = 3,
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
                Name = "연속베기",
                Description = "",
                RequiredPoints = 2,
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
                Name = "공방일체",
                Description = "",
                RequiredPoints = 2,
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
                Name = "진검승부",
                Description = "",
                RequiredPoints = 3,
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
                Name = "Sword Slash",
                Description = "",
                RequiredPoints = 3,
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

            // 6단계: 패링 돌격 (액티브 G키 스킬)
            manager.AddSkill(new SkillNode
            {
                Id = "sword_step5_defswitch",
                Name = "패링 돌격",
                Description = "",
                RequiredPoints = 3,
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
                Name = "창 전문가",
                Description = $"2연속 공격 시 공격 속도 +{SkillTreeConfig.SpearStep1AttackSpeedValue}%, 공격력 +{SkillTreeConfig.SpearStep1DamageBonusValue}%({SkillTreeConfig.SpearStep1DurationValue}초 동안)\n필요조건: 창 착용",
                RequiredPoints = 2,
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
                Name = "투창 전문가",
                Description = $"창 던지기 공격력 +{SkillTreeConfig.SpearStep2ThrowDamageValue}% (쿨타임: {SkillTreeConfig.SpearStep2ThrowCooldownValue:F0}초)\n필요조건: 한손 창 착용",
                RequiredPoints = 2,
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
                Name = "급소 찌르기",
                Description = "창 공격력 +20%\n필요조건: 창 착용",
                RequiredPoints = 2,
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
                Name = "연격창",
                Description = "무기 공격력 +4\n필요조건: 창 착용",
                RequiredPoints = 2,
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
                Name = "회피 찌르기",
                Description = "구르기 직후 공격 시 피해 +25%, 공격 스태미나 -8%\n필요조건: 창 착용",
                RequiredPoints = 3,
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

            // 4-2단계: 쾌속 창
            manager.AddSkill(new SkillNode {
                Id = "spear_Step3_quick",
                Name = "쾌속 창",
                Description = "투창 공격력 +40%\n필요조건: 창 착용",
                RequiredPoints = 3,
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

            // 5단계: 삼연창
            manager.AddSkill(new SkillNode {
                Id = "spear_Step4_triple",
                Name = "삼연창",
                Description = "3연속 공격 시 공격력 +20%\n필요조건: 창 착용",
                RequiredPoints = 3,
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
                Name = "꿰뚫는 창",
                Description = $"G키: {Spear_Config.SpearStep6PenetrateBuffDurationValue}초간 번개 충격 모드, {Spear_Config.SpearStep6PenetrateComboCountValue}회 연속 적중 시 번개 충격 발동 (데미지 +{Spear_Config.SpearStep6PenetrateLightningDamageValue}%) | 소모: 스태미나 {Spear_Config.SpearStep6PenetrateStaminaCostValue}% | 쿨타임: {Spear_Config.SpearStep6PenetrateCooldownValue}초",
                RequiredPoints = 3,
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
                Name = "연공창",
                Description = $"투창을 강화하여 창을 던지고 적과 주변 몬스터를 넉백시킴\n데미지 +{SkillTreeConfig.SpearStep6ComboDamageValue}%\n액티브 스킬 - G키",
                RequiredPoints = 3,
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
                Name = "폴암 전문가",
                Description = $"공격 범위 +{SkillTreeConfig.PolearmExpertRangeBonusValue}%\n필요조건: 폴암 착용",
                RequiredPoints = 2,
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
                Name = "회전베기",
                Description = $"휠 마우스 공격력 +{SkillTreeConfig.PolearmStep1SpinWheelDamageValue}%",
                RequiredPoints = 2,
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
                Name = "폴암강화",
                Description = "무기 공격력 +5",
                RequiredPoints = 2,
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
                Name = "영웅 타격",
                Description = $"{SkillTreeConfig.PolearmStep2HeroKnockbackChanceValue}% 확률로 넉백",
                RequiredPoints = 2,
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
                Name = "광역 강타",
                Description = $"2연속 공격 시 공격력 +{SkillTreeConfig.PolearmStep3AreaComboBonusValue}%({SkillTreeConfig.PolearmStep3AreaComboDurationValue}초동안)",
                RequiredPoints = 3,
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
                Name = "지면 강타",
                Description = $"휠 마우스 공격력 +{SkillTreeConfig.PolearmStep3GroundWheelDamageValue}%",
                RequiredPoints = 3,
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
                Name = "반달 베기",
                Description = $"공격 범위 +{SkillTreeConfig.PolearmStep4MoonRangeBonusValue}%, 공격 스태미나 -{SkillTreeConfig.PolearmStep4MoonStaminaReductionValue}%",
                RequiredPoints = 3,
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
                Name = "제압 공격",
                Description = $"공격력 +{SkillTreeConfig.PolearmStep1SuppressDamageValue}%",
                RequiredPoints = 3,
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

            // 6단계: 장창의 제왕
            manager.AddSkill(new SkillNode {
                Id = "polearm_step5_king",
                Name = "장창의 제왕",
                Description = $"체력 {SkillTreeConfig.PolearmStep5KingHealthThresholdValue}%이상인 적에게 추가 피해 +{SkillTreeConfig.PolearmStep5KingDamageBonusValue}%\n소모: 스태미나 {SkillTreeConfig.PolearmStep5KingStaminaCostValue}%\n스킬유형: 액티브 G키\n무기타입: 폴암\n쿨타임: {SkillTreeConfig.PolearmStep5KingCooldownValue}초",
                RequiredPoints = 3,
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
                Name = "둔기 전문가",
                Description = "둔기 피해 +5%, 공격 시 20% 확률로 0.5초 기절\n필요조건: 둔기 착용",
                RequiredPoints = 2,
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
                Name = "기절 강화",
                Description = "기절된 적에게 피해 +20%, 20% 확률로 넉백\n필요조건: 둔기 착용",
                RequiredPoints = 3,
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
                Name = "방패 진형",
                Description = "한손둔기 착용 시 피해 -10%, 스태미나 소모 -10%\n필요조건: 둔기 착용",
                RequiredPoints = 3,
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
                Name = "무거운 일격",
                Description = "양손둔기 착용 시 피해 +15%, 기절 확률 +10%\n필요조건: 둔기 착용",
                RequiredPoints = 3,
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
                Name = "3연격 넉백",
                Description = "3연속 공격 시 적 100% 넉백 (1.5m)\n필요조건: 둔기 착용",
                RequiredPoints = 2,
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
                Name = "방패 제압",
                Description = "한손둔기 착용 시, 방어력 +10 또는 기절 적 피해 +40%\n필요조건: 둔기 착용",
                RequiredPoints = 3,
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
                Name = "분쇄자",
                Description = "양손둔기 착용 시, 공격력 +20% 또는 기절 적 피해 +30%\n필요조건: 둔기 착용",
                RequiredPoints = 3,
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
                Name = "둔기 마스터",
                Description = "둔기 공격력 +3\n필요조건: 둔기 착용",
                RequiredPoints = 2,
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
                Name = "분노의 망치",
                Description = "\"G키\" 홀드 1~5초 후 차지 공격: 초당 피해 +20%, +45%, +75%, +120%, +200% (100% 넉백, 포인트 3 소모)\n필요조건: 양손둔기 착용",
                RequiredPoints = 3,
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
                Name = "수호자의 진심",
                Description = $"G키로 {Mace_Config.GuardianHeartDurationValue:F0}초 동안 반사 방어막 생성 (방패로 차단 시 몬스터 공격력의 {Mace_Config.GuardianHeartReflectPercentValue:F0}% 반사)\n소모: 스태미나 {Mace_Config.GuardianHeartStaminaCostValue:F0}%\n스킬유형: 액티브 G키\n무기타입: 둔기\n쿨타임: {Mace_Config.GuardianHeartCooldownValue:F0}초\n필요조건: 둔기 + 방패 착용",
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
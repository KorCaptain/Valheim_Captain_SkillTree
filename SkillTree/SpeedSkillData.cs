using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 속도 전문가 스킬트리 노드 등록
    /// </summary>
    public static class SpeedSkillData
    {
        public static void RegisterSpeedSkills()
        {
            var manager = SkillTreeManager.Instance;

            // === 속도 전문가 루트 ===
            manager.AddSkill(new SkillNode {
                Id = "speed_root",
                Name = "속도 전문가",
                Description = $"이동속도 +{SkillTreeConfig.SpeedRootMoveSpeedValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 0,
                Position = new Vector2(-90, -60),
                Category = "속도",
                IconNameLocked = "speed_lock",
                IconNameUnlocked = "speed_unlock",
                NextNodes = new List<string> { "speed_base" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player,
                            $"🏃 속도 전문가 투자 완료! (+{SkillTreeConfig.SpeedRootMoveSpeedValue}% 이동속도)",
                            new Color(0.2f, 0.9f, 1f), SkillEffect.SkillEffectTextType.Combat);
                        Plugin.Log.LogInfo($"[속도 전문가] 스킬 투자 완료: +{SkillTreeConfig.SpeedRootMoveSpeedValue}% 이동속도");
                    }
                }
            });

            // 티어1: 민첩함의 기초
            manager.AddSkill(new SkillNode {
                Id = "speed_base",
                Name = "민첩함의 기초",
                Description = $"공격속도 +{SkillTreeConfig.SpeedBaseAttackSpeedValue}%\n구르기 후 {SkillTreeConfig.SpeedBaseDodgeDurationValue}초간 이동속도 +{SkillTreeConfig.SpeedBaseDodgeMoveSpeedValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(-180, -100),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_root" },
                NextNodes = new List<string> { "melee_combo", "crossbow_reload2", "bow_speed2", "moving_cast" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player,
                            $"🏃 민첩함의 기초 습득!\n공격속도 +{SkillTreeConfig.SpeedBaseAttackSpeedValue}%\n구르기 후 이동속도 +{SkillTreeConfig.SpeedBaseDodgeMoveSpeedValue}%",
                            new Color(0.2f, 0.9f, 1f), SkillEffect.SkillEffectTextType.Combat);
                    }
                }
            });

            // 티어2: 무기별 특화 분기 (4개)
            manager.AddSkill(new SkillNode {
                Id = "melee_combo",
                Name = "연속의 흐름",
                Description = $"근접 2연속 적중 시 {SkillTreeConfig.SpeedMeleeComboDurationValue}초간 공격속도 +{SkillTreeConfig.SpeedMeleeComboAttackSpeedValue}%, 스태미나 -{SkillTreeConfig.SpeedMeleeComboStaminaValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-310, -80),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex1" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "crossbow_reload2",
                Name = "석궁 숙련자",
                Description = $"석궁 적중 시 이동속도 +{SkillTreeConfig.SpeedCrossbowExpertSpeedValue}%({SkillTreeConfig.SpeedCrossbowExpertDurationValue}초), 버프 중 재장전 +{SkillTreeConfig.SpeedCrossbowExpertReloadValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-270, -130),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex1" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "bow_speed2",
                Name = "활 숙련자",
                Description = $"활 2연속 적중 시 스태미나 -{SkillTreeConfig.SpeedBowExpertStaminaValue}%, 다음 장전 +{SkillTreeConfig.SpeedBowExpertDrawSpeedValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-240, -160),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex2" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "moving_cast",
                Name = "이동 시전",
                Description = $"마법 시전 중 이동속도 +{SkillTreeConfig.SpeedStaffCastMoveSpeedValue}%, 에이트르 소모 -{SkillTreeConfig.SpeedStaffCastEitrReductionValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-200, -210),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_base" },
                NextNodes = new List<string> { "speed_ex2" },
                ApplyEffect = (lv) => { }
            });

            // 티어3: 수련자 (2개)
            manager.AddSkill(new SkillNode {
                Id = "speed_ex1",
                Name = "수련자1",
                Description = $"근접무기 숙련 +{SkillTreeConfig.SpeedEx1MeleeSkillValue}, 석궁 숙련 +{SkillTreeConfig.SpeedEx1CrossbowSkillValue}",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-330, -160),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "melee_combo", "crossbow_reload2" },
                NextNodes = new List<string> { "speed_master", "ship_master" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "speed_ex2",
                Name = "수련자2",
                Description = $"지팡이 숙련 +{SkillTreeConfig.SpeedEx2StaffSkillValue}, 활 숙련 +{SkillTreeConfig.SpeedEx2BowSkillValue}",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-300, -190),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "bow_speed2", "moving_cast" },
                NextNodes = new List<string> { "speed_master", "ship_master" },
                ApplyEffect = (lv) => { }
            });

            // 티어4: 마스터 (2개)
            manager.AddSkill(new SkillNode {
                Id = "speed_master",
                Name = "에너자이져",
                Description = $"음식 소모 속도 -{SkillTreeConfig.SpeedFoodEfficiencyValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-390, -190),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_ex1", "speed_ex2" },
                NextNodes = new List<string> { "agility_peak" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "ship_master",
                Name = "선 장",
                Description = $"배 운전시 속도 +{SkillTreeConfig.SpeedShipBonusValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-360, -220),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_ex1", "speed_ex2" },
                NextNodes = new List<string> { "agility_peak" },
                ApplyEffect = (lv) => { }
            });

            // 티어5: 점프 숙련자
            manager.AddSkill(new SkillNode {
                Id = "agility_peak",
                Name = "점프 숙련자",
                Description = $"점프 숙련 +{SkillTreeConfig.JumpSkillLevelBonusValue}, 점프 스태미나 -{SkillTreeConfig.JumpStaminaReductionValue}%",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(-425, -230),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_master", "ship_master" },
                NextNodes = new List<string> { "speed_1", "speed_2", "speed_3" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player == null) return;
                    var skills = player.GetSkills();
                    if (skills == null) return;
                    float skillLevelBonus = SkillTreeConfig.JumpSkillLevelBonusValue;
                    float previousLevel = skills.GetSkillLevel(Skills.SkillType.Jump);
                    try {
                        skills.CheatRaiseSkill("Jump", skillLevelBonus, true);
                        float newLevel = skills.GetSkillLevel(Skills.SkillType.Jump);
                        SkillEffect.ShowSkillEffectText(player, $"🦘 점프 숙련자 습득! 점프 +{skillLevelBonus} (레벨: {previousLevel:F0} → {newLevel:F0})",
                            new Color(0.3f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Critical);
                    } catch (System.Exception ex) {
                        Plugin.Log.LogError($"[점프 숙련자] 실패: {ex.Message}");
                    }
                }
            });

            // 티어6: 스텟 증가 (3개)
            manager.AddSkill(new SkillNode {
                Id = "speed_1",
                Name = "민첩",
                Description = $"근접 공격속도 +{SkillTreeConfig.SpeedDexterityAttackSpeedBonusValue}%, 이동속도 +{SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-510, -210),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "agility_peak" },
                NextNodes = new List<string> { "all_master" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "speed_2",
                Name = "지구력",
                Description = $"스태미나 최대치 +{SkillTreeConfig.SpeedEnduranceStaminaBonusValue}",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-475, -250),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "agility_peak" },
                NextNodes = new List<string> { "all_master" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "speed_3",
                Name = "지능",
                Description = $"에이트르 최대치 +{SkillTreeConfig.SpeedIntellectEitrBonusValue}",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-430, -290),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "agility_peak" },
                NextNodes = new List<string> { "all_master" },
                ApplyEffect = (lv) => { }
            });

            // 티어7: 숙련자
            manager.AddSkill(new SkillNode {
                Id = "all_master",
                Name = "숙련자",
                Description = $"이동 숙련 +{SkillTreeConfig.AllMasterRunSkillValue}, 점프 숙련 +{SkillTreeConfig.AllMasterJumpSkillValue}",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(-565, -280),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "speed_1", "speed_2", "speed_3" },
                NextNodes = new List<string> { "melee_speed1", "crossbow_draw1", "bow_draw1", "staff_speed1" },
                ApplyEffect = (lv) => { }
            });

            // 티어8: 최종 가속 스킬들 (4개)
            manager.AddSkill(new SkillNode {
                Id = "melee_speed1",
                Name = "근접 가속",
                Description = $"근접 공격속도 +{SkillTreeConfig.SpeedMeleeAttackSpeedValue}%, 3연속 적중 시 다음 공격속도 +{SkillTreeConfig.SpeedMeleeComboTripleBonusValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-645, -250),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "crossbow_draw1",
                Name = "석궁 가속",
                Description = $"석궁 재장전 +{SkillTreeConfig.SpeedCrossbowDrawSpeedValue}%, 재장전 중 이동속도 +{SkillTreeConfig.SpeedCrossbowReloadMoveSpeedValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-665, -300),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "bow_draw1",
                Name = "활 가속",
                Description = $"활 장전 +{SkillTreeConfig.SpeedBowDrawSpeedValue}%, 장전 중 이동속도 +{SkillTreeConfig.SpeedBowDrawMoveSpeedValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-635, -350),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "staff_speed1",
                Name = "시전 가속",
                Description = $"마법 공격속도 +{SkillTreeConfig.SpeedStaffCastSpeedFinalValue}%, 3연속 적중 시 에이트르 최대치의 {SkillTreeConfig.SpeedStaffTripleEitrRecoveryValue}% 회복",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-565, -350),
                Category = "속도",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "all_master" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });
        }
    }
}

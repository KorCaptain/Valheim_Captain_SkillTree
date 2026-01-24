using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace CaptainSkillTree.SkillTree
{
    public static class SkillTreeData
    {
        public static void RegisterAll()
        {
            var manager = SkillTreeManager.Instance;
            
            // 직업 스킬 등록 (JobSkills에서 가져오기)
            JobSkills.RegisterJobSkills();
            
            // 원거리 스킬 등록 (RangedSkillData에서 가져오기)
            RangedSkillData.RegisterRangedSkills();
            
            // 원거리 스킬 아이콘 설정 (RangedSkillData에서 처리)
            RangedSkillData.SetupRangedSkillIcons();
            
            // 근접 스킬 등록 (MeleeSkillData에서 가져오기)
            MeleeSkillData.RegisterMeleeSkills();
            
            // 근접 스킬 아이콘 설정 (MeleeSkillData에서 처리)
            MeleeSkillData.SetupMeleeSkillIcons();
            
            // 방어 전문가 스킬 등록 (DefenseTreeData에서 가져오기)
            DefenseTreeData.RegisterDefenseSkills();

            // === 근접 전문가 트리는 MeleeSkillData.cs로 이동됨 ===
            // melee_root, 창, 단검, 검, 둔기 스킬들은 MeleeSkillData.cs에서 등록됨

            // 시작노드 명확히 추가 및 위치/아이콘 지정
            manager.AddSkill(new SkillNode {
                Id = "attack_root",
                Name = "공격 전문가",
                Description = $"모든 데미지 +{SkillTreeConfig.AttackRootDamageBonusValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Position = new Vector2(0, 95),
                Category = "공격",
                IconNameLocked = "attack_lock",
                IconNameUnlocked = "attack_unlock",
                NextNodes = new List<string> { "atk_base" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "⚔️ 공격 전문가 습득!", 
                            new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);
                        Plugin.Log.LogInfo("[공격 전문가] 효과 적용 완료 - 모든 데미지 +5% (Harmony 패치를 통해 자동 적용)");
                    }
                }
            });

            // ===== 공격 전문화 스킬트리 =====
            
            // 1단계: 기본 공격
            manager.AddSkill(new SkillNode {
                Id = "atk_base",
                Name = "기본 공격",
                Description = $"물리 공격력 +{SkillTreeConfig.AttackBasePhysicalDamageValue}, 속성 공격력 +{SkillTreeConfig.AttackBaseElementalDamageValue}",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(0, 155),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "attack_root" },
                NextNodes = new List<string> { "atk_melee_bonus", "atk_bow_bonus", "atk_crossbow_bonus", "atk_staff_bonus" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "💪 기본 공격 습득!",
                            new Color(0.8f, 0.6f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // 2단계: 무기별 특화
            manager.AddSkill(new SkillNode {
                Id = "atk_melee_bonus",
                Name = "근접 특화",
                Description = $"근접 무기 사용 시 {SkillTreeConfig.AttackMeleeBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackMeleeBonusDamageValue}% 근접 공격력 증가",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-90, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "⚔️ 근접 특화 습득!");
                        Plugin.Log.LogInfo("[근접 특화] 효과 적용 완료");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_bow_bonus",
                Name = "활 특화",
                Description = $"활 사용 시 {SkillTreeConfig.AttackBowBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackBowBonusDamageValue}% 활 공격력 증가",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-45, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_crossbow_bonus",
                Name = "석궁 특화",
                Description = $"석궁 사용 시 {SkillTreeConfig.AttackCrossbowBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackCrossbowBonusDamageValue}% 석궁 공격력 증가",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(45, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_staff_bonus",
                Name = "지팡이 특화",
                Description = $"지팡이 사용 시 {SkillTreeConfig.AttackStaffBonusChanceValue}% 확률로 +{SkillTreeConfig.AttackStaffBonusDamageValue}% 지팡이 공격력 증가 + 주변 적에게 속성 피해",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(90, 205),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_base" },
                NextNodes = new List<string> { "atk_twohand_drain" },
                ApplyEffect = (lv) => { }
            });

            // 3단계: 공격 증가
            manager.AddSkill(new SkillNode {
                Id = "atk_twohand_drain",
                Name = "공격 증가",
                Description = $"물리 공격력 +{SkillTreeConfig.AttackTwoHandDrainPhysicalDamageValue}, 속성 공격력 +{SkillTreeConfig.AttackTwoHandDrainElementalDamageValue}",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(0, 275),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_melee_bonus", "atk_bow_bonus", "atk_crossbow_bonus", "atk_staff_bonus" },
                NextNodes = new List<string> { "atk_melee_crit", "atk_crit_chance", "atk_ranged_enhance" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, $"💪 공격 증가!", 
                            new Color(0.9f, 0.7f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[공격 증가] 효과 적용 완료 - 힘+{SkillTreeConfig.AttackStatBonusValue}, 지능+{SkillTreeConfig.AttackStatBonusValue}");
                    }
                }
            });

            // 4단계: 세부 강화
            manager.AddSkill(new SkillNode {
                Id = "atk_melee_crit",
                Name = "근접 강화",
                Description = $"근접무기 2연속 공격 시 +{SkillTreeConfig.AttackMeleeEnhancementValue}% 추가 피해",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-60, 325),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_twohand_drain" },
                NextNodes = new List<string> { "atk_special" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_crit_chance",
                Name = "정밀 공격",
                Description = $"치명타 확률 +{SkillTreeConfig.AttackCritChanceValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(0, 325),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_twohand_drain" },
                NextNodes = new List<string> { "atk_special" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_ranged_enhance",
                Name = "원거리 강화",
                Description = $"원거리 무기 공격력 +{SkillTreeConfig.AttackRangedEnhancementValue}% (석궁, 활, 지팡이)",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(60, 325),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_twohand_drain" },
                NextNodes = new List<string> { "atk_special" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, $"🏹 원거리 강화!",
                            new Color(0.2f, 0.8f, 0.8f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[원거리 강화] 효과 적용 완료 - 원거리 무기 공격력 +{SkillTreeConfig.AttackRangedEnhancementValue}%");
                    }
                }
            });

            // 5단계: 특수화 스탯
            manager.AddSkill(new SkillNode {
                Id = "atk_special",
                Name = "특수화 스탯",
                Description = $"치명타 확률 +{SkillTreeConfig.AttackSpecialStatValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 5,
                Position = new Vector2(0, 375),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_melee_crit", "atk_crit_chance", "atk_ranged_enhance" },
                NextNodes = new List<string> { "atk_crit_dmg", "atk_finisher_melee", "atk_twohand_crush", "atk_staff_mage" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, $"⭐ 특수화 스탯! 치명타 확률 +{SkillTreeConfig.AttackSpecialStatValue}%",
                            new Color(1f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[특수화 스탯] 효과 적용 완료 - 치명타 확률 +{SkillTreeConfig.AttackSpecialStatValue}%");
                    }
                }
            });

            // 6단계: 최종 특화
            manager.AddSkill(new SkillNode {
                Id = "atk_crit_dmg",
                Name = "약점 공격",
                Description = $"치명타 피해 +{SkillTreeConfig.AttackCritDamageBonusValue}%",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-90, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_finisher_melee",
                Name = "연속 근접의 대가",
                Description = $"근접 3연속 공격 시 +{SkillTreeConfig.AttackFinisherMeleeBonusValue}% 추가 피해",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(-45, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_twohand_crush",
                Name = "양손 분쇄",
                Description = $"양손 무기 공격력 +{SkillTreeConfig.AttackTwoHandedBonusValue}%",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(45, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "atk_staff_mage",
                Name = "속성 공격",
                Description = $"속성 공격 +{SkillTreeConfig.AttackStaffElementalValue}% (활, 지팡이)",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 6,
                Position = new Vector2(90, 415),
                Category = "공격",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "atk_special" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, $"🔥 속성 공격!", 
                            new Color(0.8f, 0.2f, 0.8f), SkillEffect.SkillEffectTextType.Standard);
                        Plugin.Log.LogInfo($"[속성 공격] 효과 적용 완료 - 속성 공격 +{SkillTreeConfig.AttackStaffElementalValue}% (활, 지팡이)");
                    }
                }
            });
            
            // === 원거리 전문가 트리는 RangedSkillData.cs로 이동됨 ===
            // ranged_root, 석궁, 활, 지팡이 스킬들은 RangedSkillData.cs에서 등록됨

            // === 생산 전문가 트리 ===
            manager.AddSkill(new SkillNode {
                Id = "production_root",
                Name = "생산 전문가",
                Description = "50% 확률로 나무+1",
                RequiredPoints = 2,
                MaxLevel = 1,
                Position = new Vector2(0, -95),
                Category = "생산",
                IconNameLocked = "production_lock",
                IconNameUnlocked = "production_unlock",
                NextNodes = new List<string> { "novice_worker" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "벌목 수량 +1");
                    }
                }
            });



            manager.AddSkill(new SkillNode {
                Id = "speed_root",
                Name = "속도 전문가",
                Description = $"이동속도 +{SkillTreeConfig.SpeedRootMoveSpeedValue}%",
                RequiredPoints = 2,
                MaxLevel = 1,
                Position = new Vector2(-90, -60),
                Category = "속도",
                IconNameLocked = "speed_lock",
                IconNameUnlocked = "speed_unlock",
                NextNodes = new List<string> { "speed_base" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        // 스킬 투자 완료 표시
                        SkillEffect.ShowSkillEffectText(player,
                            $"🏃 속도 전문가 투자 완료! (+{SkillTreeConfig.SpeedRootMoveSpeedValue}% 이동속도)",
                            new Color(0.2f, 0.9f, 1f), SkillEffect.SkillEffectTextType.Combat);
                        
                        Plugin.Log.LogInfo($"[속도 전문가] 스킬 투자 완료: +{SkillTreeConfig.SpeedRootMoveSpeedValue}% 이동속도 (StatusEffect 방식)");
                    }
                }
            });

            // 모든 노드의 기본 아이콘을 락 상태로 설정 (투자 상태에 따라 UI에서 동적 변경)
            foreach (var node in manager.SkillNodes.Values)
            {
                if (!string.IsNullOrEmpty(node.IconNameLocked))
                    node.IconName = node.IconNameLocked;
                else if (!string.IsNullOrEmpty(node.IconNameUnlocked))
                    node.IconName = node.IconNameUnlocked;
            }

            // 실제 존재하는 스프라이트 이름으로 자동 지정 (특별한 아이콘이 없는 노드만)
            foreach (var node in manager.SkillNodes.Values) {
                // ranged_root, defense_root 등 이미 특별한 아이콘이 설정된 노드는 건드리지 않음
                if (string.IsNullOrEmpty(node.IconNameLocked) || node.IconNameLocked == "all_skill_lock") {
                    node.IconNameLocked = "all_skill_lock";
                    node.IconNameUnlocked = "all_skill_unlock";
                }
            }

            // 근접/공격/원거리/생존/방어/속도 노드 위치 지정
            // melee_root 위치는 MeleeSkillData.cs에서 설정됨
            if (manager.SkillNodes.ContainsKey("attack_root"))
                manager.SkillNodes["attack_root"].Position = new Vector2(0, 95); // y+15
            // ranged_root 위치는 RangedSkillData.cs에서 설정됨
            if (manager.SkillNodes.ContainsKey("production_root"))
                manager.SkillNodes["production_root"].Position = new Vector2(0, -95); // y-15
            if (manager.SkillNodes.ContainsKey("speed_root"))
                manager.SkillNodes["speed_root"].Position = new Vector2(-90, -60);

            // 근접 무기 노드 좌표 설정 (일부는 MeleeSkillData.cs에서 처리)
            if (manager.SkillNodes.ContainsKey("spear"))
                manager.SkillNodes["spear"].Position = new Vector2(185, 35);
            // 폴암 스킬은 MeleeSkillData.cs로 이동됨
            // 근접 무기 노드끼리 연결선이 없도록 Prerequisites/NextNodes 점검
            // 각 무기 노드의 Prerequisites가 melee_root만 가리키도록 유지
            if (manager.SkillNodes.ContainsKey("spear"))
                manager.SkillNodes["spear"].Prerequisites = new List<string> { "melee_root" };
            // 폴암 Prerequisites는 MeleeSkillData.cs에서 설정됨

            // 중앙 0,0에 위치한 불필요한 노드가 있다면 UI에서 제외(예: node.Position == Vector2.zero)
            var removeList = manager.SkillNodes.Values.Where(n => n.Position == Vector2.zero).Select(n => n.Id).ToList();
            foreach (var id in removeList) manager.SkillNodes.Remove(id);

            // 무기 타입별 조건 문구 자동 교체
            string weapon = null;
            foreach (var node in manager.SkillNodes.Values)
            {
                if (node.Id.StartsWith("knife_expert")) weapon = "단검";
                else if (node.Id.StartsWith("sword_expert")) weapon = "검";
                else if (node.Id.StartsWith("spear_expert")) weapon = "창";
                else if (node.Id.StartsWith("polearm_expert")) weapon = "폴암";
                else if (node.Id.StartsWith("mace_Step1")) weapon = "둔기";
                else if (node.Id.StartsWith("spear")) weapon = "창";
                else if (node.Id.StartsWith("bow")) weapon = "활";
                else if (node.Id.StartsWith("crossbow")) weapon = "석궁";
                if (weapon != null && !string.IsNullOrEmpty(node.Description))
                {
                    // 기존 조건 라인 패턴을 찾아서 교체
                    node.Description = Regex.Replace(node.Description, @"\\n<color=#00BFFF><size=14>※ [^<\n]+착용 ?시 효과발동<\/size><\/color>", "");
                    node.Description = Regex.Replace(node.Description, @"\\n<color=#00BFFF><size=14>※ [^<\n]+착용 ?시 효과 발동<\/size><\/color>", "");
                    node.Description = Regex.Replace(node.Description, @"\\n?※ [^<\n]+착용 ?시 효과발동", "");
                    node.Description = Regex.Replace(node.Description, @"\\n?※ [^<\n]+착용 ?시 효과 발동", "");
                    node.Description = node.Description.Trim();
                }
            }
            
            // 근접 무기 전문가 스킬 툴팁 강제 업데이트
            try
            {
                Knife_Tooltip.UpdateKnifeTooltips();
                Sword_Tooltip.UpdateSwordTooltips();
                Spear_Tooltip.UpdateSpearTooltips();
                Polearm_Tooltip.UpdatePolearmTooltips();
                Mace_Tooltip.UpdateMaceTooltips();
                Plugin.Log.LogDebug("[SkillTreeData] 근접 무기 툴팁 강제 업데이트 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[SkillTreeData] 근접 무기 툴팁 업데이트 실패: {ex.Message}");
            }
            
            // 근접 무기 트리들은 MeleeSkillData.cs에서 고정 위치로 구현됨
            // ===== 둔기 스킬 트리는 MeleeSkillData.cs로 이동됨 =====

            // ===== 생산 전문가 스킬트리 =====
            
            // 1단계: 초보 일꾼
            manager.AddSkill(new SkillNode {
                Id = "novice_worker",
                Name = "초보 일꾼",
                Description = "25% 확률로 나무+1",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 1,
                Position = new Vector2(0, -155),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "production_root" },
                NextNodes = new List<string> { "woodcutting_lv2", "gathering_lv2", "mining_lv2", "crafting_lv2" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🔨 초보 일꾼 습득!", 
                            new Color(0.4f, 0.8f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // 2단계: 전문 분야
            manager.AddSkill(new SkillNode {
                Id = "woodcutting_lv2",
                Name = "벌목 Lv2",
                Description = "25% 확률로 나무+1",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-110, -215),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "novice_worker" },
                NextNodes = new List<string> { "woodcutting_lv3" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        // float totalBonus = 20f + 30f; // 초보일꾼 + 벌목Lv2 (미사용 변수 제거)
                        SkillEffect.ShowSkillEffectText(player, "🪓 벌목 Lv2 습득!", 
                            new Color(0.6f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "gathering_lv2",
                Name = "채집 Lv2",
                Description = "25% 확률로 채집(나무제외)+1",
                RequiredPoints = 2,
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(-45, -215),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "novice_worker" },
                NextNodes = new List<string> { "gathering_lv3" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🍄 채집 Lv2 습득!", 
                            new Color(0.2f, 0.8f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "mining_lv2",
                Name = "채광 Lv2",
                Description = "25% 확률로 광석+1",
                RequiredPoints = 0, // 자원 조건 기반
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(45, -215),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "novice_worker" },
                NextNodes = new List<string> { "mining_lv3" },
                ApplyEffect = (lv) => { }
            });

            manager.AddSkill(new SkillNode {
                Id = "crafting_lv2",
                Name = "제작 Lv2",
                Description = "25% 확률로 강화+1, 내구도 최대치 25% 증가",
                RequiredPoints = 0, // 장비 조건 기반
                MaxLevel = 1,
                Tier = 2,
                Position = new Vector2(110, -215),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "novice_worker" },
                NextNodes = new List<string> { "crafting_lv3" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🔨 제작 Lv2 습득!", 
                            new Color(0.8f, 0.6f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });


            // 3단계: 새로운 벌목/채집/채광/제작 스킬들
            manager.AddSkill(new SkillNode {
                Id = "woodcutting_lv3",
                Name = "벌목 Lv3",
                Description = "25% 확률로 나무+1",
                RequiredPoints = 0, // 자원 조건 기반
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-110, -255),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "woodcutting_lv2" },
                NextNodes = new List<string> { "woodcutting_lv4" }, // 벌목 Lv4로만 연결
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🪓 벌목 Lv3 습득!", 
                            new Color(0.6f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "gathering_lv3",
                Name = "채집 Lv3",
                Description = "25% 확률로 채집(나무제외)+1",
                RequiredPoints = 0, // 자원 조건 기반
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-45, -255),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "gathering_lv2" },
                NextNodes = new List<string> { "gathering_lv4" }, // 채집 Lv4로만 연결
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🍄 채집 Lv3 습득!", 
                            new Color(0.2f, 0.8f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // 3단계: 새로운 채광/제작 스킬들
            manager.AddSkill(new SkillNode {
                Id = "mining_lv3",
                Name = "채광 Lv3",
                Description = "25% 확률로 광석+1",
                RequiredPoints = 0, // 자원 조건 기반
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(45, -255),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mining_lv2" },
                NextNodes = new List<string> { "mining_lv4" }, // 채광 Lv4로만 연결
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "⛏️ 채광 Lv3 습득!");
                        SkillEffect.DrawFloatingText(player, "철 30개 소모 시 광석 추가 획득!");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "crafting_lv3",
                Name = "제작 Lv3",
                Description = "25% 확률로 강화+1, 내구도 최대치 25% 증가",
                RequiredPoints = 0, // 장비 조건 기반
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(110, -255),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crafting_lv2" },
                NextNodes = new List<string> { "crafting_lv4" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "🔨 제작 Lv3 습득!");
                        SkillEffect.DrawFloatingText(player, "철 검+헬멧 보유 시 제작 강화 효과!");
                    }
                }
            });

            // 4단계: 고급 벌목/채집/채광/제작 스킬들 (최종 단계 - 연결 없음)
            manager.AddSkill(new SkillNode {
                Id = "woodcutting_lv4",
                Name = "벌목 Lv4",
                Description = "25% 확률로 나무+1",
                RequiredPoints = 0, // 자원 조건 기반
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-110, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "woodcutting_lv3" },
                NextNodes = new List<string>(), // 최종 단계 - 연결 없음
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "🪓 벌목 Lv4 습득!");
                        SkillEffect.DrawFloatingText(player, "나무 400개 보유 시 추가 획득!");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "gathering_lv4",
                Name = "채집 Lv4",
                Description = "채집류 200개 보유 시 25% 확률로 채집+1",
                RequiredPoints = 0, // 자원 조건 기반
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-45, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "gathering_lv3" },
                NextNodes = new List<string>(), // 최종 단계 - 연결 없음
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "🍄 채집 Lv4 습득!");
                        SkillEffect.DrawFloatingText(player, "채집 200개 보유 시 추가 획득!");
                    }
                }
            });

            // 4단계: 고급 채광/제작 스킬들
            manager.AddSkill(new SkillNode {
                Id = "mining_lv4",
                Name = "채광 Lv4",
                Description = "25% 확률로 광석+1",
                RequiredPoints = 0, // 자원 조건 기반
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(45, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mining_lv3" },
                NextNodes = new List<string>(), // 최종 단계 - 연결 없음
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "⛏️ 채광 Lv4 습득!");
                        SkillEffect.DrawFloatingText(player, "은 25개 소모 시 광석 추가 획득!");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "crafting_lv4",
                Name = "제작 Lv4",
                Description = "25% 확률로 강화+1, 내구도 최대치 25% 증가",
                RequiredPoints = 0, // 장비 조건 기반
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(110, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crafting_lv3" },
                NextNodes = new List<string>(), // 최종 단계 - 연결 없음
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "🔨 제작 Lv4 습득!");
                        SkillEffect.DrawFloatingText(player, "은 검+헬멧 보유 시 제작 강화 효과!");
                    }
                }
            });




            // === 생산 스킬 시스템 초기화 ===
            // 제작 강화 시스템은 CraftingEnhancement.cs에서 자동으로 처리됨

            // ===== 속도 전문가 스킬트리 =====
            
            // 루트 노드: 속도 전문가
            // (중복 등록된 speed_root 노드 삭제)

            // 1단계: 공격속도
            manager.AddSkill(new SkillNode {
                Id = "speed_base",
                Name = "민첩함의 기초",
                Description = $"공격속도 +{SkillTreeConfig.SpeedBaseAttackSpeedValue}%, 이동속도 +{SkillTreeConfig.SpeedBaseMoveSpeedValue}%",
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
                            $"🏃 민첩함의 기초 습득! (+{SkillTreeConfig.SpeedBaseMoveSpeedValue}% 이동속도)",
                            new Color(0.2f, 0.9f, 1f), SkillEffect.SkillEffectTextType.Combat);
                        
                        Plugin.Log.LogInfo($"[민첩함의 기초] 스킬 투자 완료: +{SkillTreeConfig.SpeedBaseMoveSpeedValue}% 이동속도 (StatusEffect 방식)");
                    }
                }
            });

            // 2단계: 무기별 이동속도 분기 (4개)
            manager.AddSkill(new SkillNode {
                Id = "melee_combo",
                Name = "연속의 흐름",
                Description = $"근접 공격 2연속 공격 시 스태미나 사용 -{SkillTreeConfig.SpeedMeleeComboStaminaValue}%({SkillTreeConfig.SpeedMeleeComboDurationValue}초간)",
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
                Description = $"석궁 공격 시 이동속도 +{SkillTreeConfig.SpeedCrossbowExpertSpeedValue}%({SkillTreeConfig.SpeedCrossbowExpertDurationValue}초 동안)",
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
                Description = $"활 2연속 공격 시 스태미나 사용 -{SkillTreeConfig.SpeedBowExpertStaminaValue}%({SkillTreeConfig.SpeedBowExpertDurationValue}초 동안)",
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
                Description = $"마법 시전 중 이동속도 +{SkillTreeConfig.SpeedStaffCastSpeedValue}%",
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

            // 3단계: 수련자 (2개)
            manager.AddSkill(new SkillNode {
                Id = "speed_ex1",
                Name = "수련자1",
                Description = "근접무기 숙련 +3, 석궁 숙련 +3",
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
                Description = "지팡이 숙련 +3, 활 숙련 +3",
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

            // 4단계: 마스터 (2개)
            manager.AddSkill(new SkillNode {
                Id = "speed_master",
                Name = "에너자이져",
                Description = "음식 소모 속도 -15%",
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
                Description = "배 운전시 속도 +15%",
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

            // 5단계: 점프 숙련자
            manager.AddSkill(new SkillNode {
                Id = "agility_peak",
                Name = "점프 숙련자",
                Description = "점프 기술 레벨 +10, 점프 스태미나 -10%",
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
                    if (player == null) {
                        Plugin.Log.LogError("[점프 숙련자] Player.m_localPlayer가 null입니다!");
                        return;
                    }
                    
                    var skills = player.GetSkills();
                    if (skills == null) {
                        Plugin.Log.LogError("[점프 숙련자] player.GetSkills()가 null입니다!");
                        return;
                    }
                    
                    // 발하임 점프 스킬 레벨 직접 증가 (CheatRaiseSkill 방식 - 활 숙련과 동일)
                    float skillLevelBonus = SkillTreeConfig.JumpSkillLevelBonusValue; // 10
                    float previousLevel = skills.GetSkillLevel(Skills.SkillType.Jump);
                    
                    Plugin.Log.LogWarning($"[점프 숙련자] 현재 점프 기술 레벨: {previousLevel}, 상승할 레벨: {skillLevelBonus}");
                    
                    try {
                        // 발하임 공식 CheatRaiseSkill 메서드 사용 (활 숙련과 동일한 방식)
                        skills.CheatRaiseSkill("Jump", skillLevelBonus, true);
                        
                        float newLevel = skills.GetSkillLevel(Skills.SkillType.Jump);
                        
                        SkillEffect.ShowSkillEffectText(player, $"🦘 점프 숙련자 습득! 점프 기술 +{skillLevelBonus} 레벨 (레벨: {previousLevel:F1} → {newLevel:F1})", 
                            new Color(0.3f, 0.9f, 0.3f), SkillEffect.SkillEffectTextType.Critical);
                        
                        Plugin.Log.LogWarning($"[점프 숙련자] CheatRaiseSkill로 점프 기술 레벨 상승: {previousLevel:F1} → {newLevel:F1} (+{skillLevelBonus} 레벨)");
                    } catch (System.Exception ex) {
                        Plugin.Log.LogError($"[점프 숙련자] CheatRaiseSkill 실패: {ex.Message}");
                    }
                }
            });

            // 6단계: 스텟 증가 (3개)
            manager.AddSkill(new SkillNode {
                Id = "speed_1",
                Name = "민첩",
                Description = $"공격속도 +{SkillTreeConfig.SpeedDexterityAttackSpeedBonusValue}%, 이동속도 +{SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue}%",
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

            // 7단계: 숙련자
            manager.AddSkill(new SkillNode {
                Id = "all_master",
                Name = "숙련자",
                Description = "모든 스텟 +2",
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

            // 8단계: 최종 가속 스킬들 (4개)
            manager.AddSkill(new SkillNode {
                Id = "melee_speed1",
                Name = "근접 가속",
                Description = $"근접 공격속도 +{SkillTreeConfig.SpeedMeleeAttackSpeedValue}%",
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
                Description = $"석궁 공격속도 +{SkillTreeConfig.SpeedCrossbowDrawSpeedValue}%",
                RequiredPoints = 1,
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
                Description = $"활 공격속도 +{SkillTreeConfig.SpeedBowDrawSpeedValue}%",
                RequiredPoints = 3,
                MaxLevel = 1,
                Tier = 8,
                Position = new Vector2(-635, -350), // 기존 (-645, -360)
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
                Description = $"지팡이 공격속도 +{SkillTreeConfig.SpeedStaffCastSpeedFinalValue}%",
                RequiredPoints = 3,
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
           
            // === 지팡이 전문가 트리는 RangedSkillData.cs로 이동됨 ===
        }
        
    }
} 
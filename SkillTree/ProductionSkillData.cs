using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 생산 전문가 스킬트리 노드 등록
    /// </summary>
    public static class ProductionSkillData
    {
        public static void RegisterProductionSkills()
        {
            var manager = SkillTreeManager.Instance;

            // === 생산 전문가 루트 ===
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
                RequiredPoints = 0,
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
                RequiredPoints = 0,
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
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-110, -255),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "woodcutting_lv2" },
                NextNodes = new List<string> { "woodcutting_lv4" },
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
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(-45, -255),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "gathering_lv2" },
                NextNodes = new List<string> { "gathering_lv4" },
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.ShowSkillEffectText(player, "🍄 채집 Lv3 습득!",
                            new Color(0.2f, 0.8f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "mining_lv3",
                Name = "채광 Lv3",
                Description = "25% 확률로 광석+1",
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 3,
                Position = new Vector2(45, -255),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mining_lv2" },
                NextNodes = new List<string> { "mining_lv4" },
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
                RequiredPoints = 0,
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

            // 4단계: 고급 벌목/채집/채광/제작 스킬들 (최종 단계)
            manager.AddSkill(new SkillNode {
                Id = "woodcutting_lv4",
                Name = "벌목 Lv4",
                Description = "25% 확률로 나무+1",
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-110, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "woodcutting_lv3" },
                NextNodes = new List<string>(),
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
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(-45, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "gathering_lv3" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "🍄 채집 Lv4 습득!");
                        SkillEffect.DrawFloatingText(player, "채집 200개 보유 시 추가 획득!");
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "mining_lv4",
                Name = "채광 Lv4",
                Description = "25% 확률로 광석+1",
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(45, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "mining_lv3" },
                NextNodes = new List<string>(),
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
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 4,
                Position = new Vector2(110, -295),
                Category = "생산",
                IconName = "all_skill_unlock",
                IconNameLocked = "all_skill_lock",
                IconNameUnlocked = "all_skill_unlock",
                Prerequisites = new List<string> { "crafting_lv3" },
                NextNodes = new List<string>(),
                ApplyEffect = (lv) => {
                    var player = Player.m_localPlayer;
                    if (player != null) {
                        SkillEffect.DrawFloatingText(player, "🔨 제작 Lv4 습득!");
                        SkillEffect.DrawFloatingText(player, "은 검+헬멧 보유 시 제작 강화 효과!");
                    }
                }
            });
        }
    }
}

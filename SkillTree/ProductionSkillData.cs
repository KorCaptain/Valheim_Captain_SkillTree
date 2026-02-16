using System.Collections.Generic;
using UnityEngine;
using CaptainSkillTree.Localization;

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
                NameKey = "production_skill_expert",
                DescriptionKey = "production_desc_expert",
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
                        SkillEffect.DrawFloatingText(player, L.Get("production_root_effect"));
                    }
                }
            });

            // 1단계: 초보 일꾼
            manager.AddSkill(new SkillNode {
                Id = "novice_worker",
                NameKey = "novice_worker_name",
                DescriptionKey = "novice_worker_desc",
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
                        SkillEffect.ShowSkillEffectText(player, L.Get("novice_worker_effect"),
                            new Color(0.4f, 0.8f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // 2단계: 전문 분야
            manager.AddSkill(new SkillNode {
                Id = "woodcutting_lv2",
                NameKey = "woodcutting_lv2_name",
                DescriptionKey = "woodcutting_lv2_desc",
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
                        SkillEffect.ShowSkillEffectText(player, L.Get("woodcutting_lv2_effect"),
                            new Color(0.6f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "gathering_lv2",
                NameKey = "gathering_lv2_name",
                DescriptionKey = "gathering_lv2_desc",
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
                        SkillEffect.ShowSkillEffectText(player, L.Get("gathering_lv2_effect"),
                            new Color(0.2f, 0.8f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "mining_lv2",
                NameKey = "mining_lv2_name",
                DescriptionKey = "mining_lv2_desc",
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
                NameKey = "crafting_lv2_name",
                DescriptionKey = "crafting_lv2_desc",
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
                        SkillEffect.ShowSkillEffectText(player, L.Get("crafting_lv2_effect"),
                            new Color(0.8f, 0.6f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            // 3단계: 새로운 벌목/채집/채광/제작 스킬들
            manager.AddSkill(new SkillNode {
                Id = "woodcutting_lv3",
                NameKey = "woodcutting_lv3_name",
                DescriptionKey = "woodcutting_lv3_desc",
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
                        SkillEffect.ShowSkillEffectText(player, L.Get("woodcutting_lv3_effect"),
                            new Color(0.6f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "gathering_lv3",
                NameKey = "gathering_lv3_name",
                DescriptionKey = "gathering_lv3_desc",
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
                        SkillEffect.ShowSkillEffectText(player, L.Get("gathering_lv3_effect"),
                            new Color(0.2f, 0.8f, 0.4f), SkillEffect.SkillEffectTextType.Standard);
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "mining_lv3",
                NameKey = "mining_lv3_name",
                DescriptionKey = "mining_lv3_desc",
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
                        SkillEffect.DrawFloatingText(player, L.Get("mining_lv3_effect"));
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "crafting_lv3",
                NameKey = "crafting_lv3_name",
                DescriptionKey = "crafting_lv3_desc",
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
                        SkillEffect.DrawFloatingText(player, L.Get("crafting_lv3_effect"));
                    }
                }
            });

            // 4단계: 고급 벌목/채집/채광/제작 스킬들 (최종 단계)
            manager.AddSkill(new SkillNode {
                Id = "woodcutting_lv4",
                NameKey = "woodcutting_lv4_name",
                DescriptionKey = "woodcutting_lv4_desc",
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
                        SkillEffect.DrawFloatingText(player, L.Get("woodcutting_lv4_effect"));
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "gathering_lv4",
                NameKey = "gathering_lv4_name",
                DescriptionKey = "gathering_lv4_desc",
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
                        SkillEffect.DrawFloatingText(player, L.Get("gathering_lv4_effect"));
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "mining_lv4",
                NameKey = "mining_lv4_name",
                DescriptionKey = "mining_lv4_desc",
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
                        SkillEffect.DrawFloatingText(player, L.Get("mining_lv4_effect"));
                    }
                }
            });

            manager.AddSkill(new SkillNode {
                Id = "crafting_lv4",
                NameKey = "crafting_lv4_name",
                DescriptionKey = "crafting_lv4_desc",
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
                        SkillEffect.DrawFloatingText(player, L.Get("crafting_lv4_effect"));
                    }
                }
            });
        }
    }
}

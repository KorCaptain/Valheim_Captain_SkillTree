using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 퀘스트 진행도 추적, 완료 판정, 보상 클레임을 총괄하는 매니저.
    /// 진행 상태는 Player.m_customData(플레이어 세이브에 영속화)에 저장된다.
    /// </summary>
    public static class QuestManager
    {
        private const string DataPrefix = "CaptainSkillTree_Quest_";

        /// <summary>일반 처치 진행과 달리 별도 트래커 조건(무피해/근접전용/마법전용 등)을 만족해야
        /// 진행되는 히든 퀘스트 Key 목록. QuestKillPatch.cs의 조건부 진행 분기와 QuestPanelUI.cs의
        /// "[특수]" 라벨 표시가 이 목록을 공유해 서로 어긋나지 않게 한다.</summary>
        public static readonly HashSet<string> HiddenAchievementQuestKeys = new HashSet<string>
        {
            "Meadows_Quest6",
            "BlackForest_Quest7",
            "Swamp_Quest7",
            "Mountain_Quest5",
            "Mountain_Quest6",
        };

        private static List<QuestDefinition> _cachedQuests;

        /// <summary>Quest_Config에서 활성화된 퀘스트 목록을 읽어온다(비활성 퀘스트는 제외).</summary>
        public static List<QuestDefinition> GetActiveQuests()
        {
            if (_cachedQuests != null) return _cachedQuests;

            var result = new List<QuestDefinition>();
            foreach (var kvp in Quest_Config.Quests)
            {
                string prefix = kvp.Key;
                var cfg = kvp.Value;

                // 수치형 필드는 SkillTreeConfig.GetEffectiveValue(), 문자열 필드는
                // Quest_StringSync.GetEffectiveString()으로 서버 동기화 값을 우선 사용한다
                // (md/SERVER_SYNC_RULES.md).
                bool enabled = SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyEnabled(prefix),
                    cfg.Enabled?.Value == true ? 1f : 0f) > 0.5f;
                if (!enabled) continue;

                string typeStr = Quest_StringSync.GetEffectiveString(Quest_Config.SyncKeyType(prefix), cfg.Type.Value);
                if (!Enum.TryParse(typeStr, true, out QuestType type))
                {
                    Plugin.Log.LogWarning($"[QuestManager] 알 수 없는 퀘스트 Type: {typeStr} ({prefix})");
                    continue;
                }

                result.Add(new QuestDefinition
                {
                    Key = prefix,
                    Biome = prefix.Substring(0, prefix.IndexOf('_')),
                    Type = type,
                    TargetPrefab = Quest_StringSync.GetEffectiveString(Quest_Config.SyncKeyTargetPrefab(prefix), cfg.TargetPrefab.Value),
                    Amount = (int)SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyAmount(prefix), cfg.Amount.Value),
                    ItemReward1 = Quest_StringSync.GetEffectiveString(Quest_Config.SyncKeyItem1(prefix), cfg.ItemReward1.Value),
                    ItemReward1Amount = (int)SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyItem1Amount(prefix), cfg.ItemReward1Amount.Value),
                    ItemReward2 = Quest_StringSync.GetEffectiveString(Quest_Config.SyncKeyItem2(prefix), cfg.ItemReward2.Value),
                    ItemReward2Amount = (int)SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyItem2Amount(prefix), cfg.ItemReward2Amount.Value),
                    CoinMin = (int)SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyCoinMin(prefix), cfg.CoinMin.Value),
                    CoinMax = (int)SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyCoinMax(prefix), cfg.CoinMax.Value),
                    SpecialReward = Quest_StringSync.GetEffectiveString(Quest_Config.SyncKeySpecialReward(prefix), cfg.SpecialReward.Value),
                    OrbPrefabOverride = Quest_StringSync.GetEffectiveString(Quest_Config.SyncKeyOrbPrefab(prefix), cfg.OrbPrefabOverride.Value),
                    PotionPrefabOverride = Quest_StringSync.GetEffectiveString(Quest_Config.SyncKeyPotionPrefab(prefix), cfg.PotionPrefabOverride.Value),
                    RewardItemCountOverride = (int)SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyRewardItemCount(prefix), cfg.RewardItemCountOverride.Value),
                    RequiredLevel = (int)SkillTreeConfig.GetEffectiveValue(Quest_Config.SyncKeyRequiredLevel(prefix), cfg.RequiredLevel.Value),
                    DisplayOrder = cfg.DisplayOrder,
                    RequireDistinctTargets = cfg.RequireDistinctTargets,
                });
            }
            _cachedQuests = result;
            return result;
        }

        /// <summary>Config가 리로드될 때(F5 등) 캐시를 비운다.</summary>
        public static void InvalidateCache() => _cachedQuests = null;

        public static QuestDefinition FindByTarget(QuestType type, string targetPrefab)
        {
            foreach (var q in GetActiveQuests())
            {
                if (q.Type == type && string.Equals(q.TargetPrefab, targetPrefab, StringComparison.OrdinalIgnoreCase))
                    return q;
            }
            return null;
        }

        /// <summary>같은 Type+TargetPrefab을 공유하는 모든 퀘스트를 반환한다(둘 이상의 퀘스트가 같은 몬스터를 목표로 할 때 필요).</summary>
        public static List<QuestDefinition> FindAllByTarget(QuestType type, string targetPrefab)
        {
            var result = new List<QuestDefinition>();
            foreach (var q in GetActiveQuests())
            {
                if (q.Type == type && string.Equals(q.TargetPrefab, targetPrefab, StringComparison.OrdinalIgnoreCase))
                    result.Add(q);
            }
            return result;
        }

        // ===== 진행 상태 저장/조회 (Player.m_customData) =====

        /// <summary>퀘스트 진행도. 저장된 처치 카운터를 반환한다.</summary>
        public static int GetProgress(Player player, QuestDefinition quest)
        {
            if (player == null || quest == null) return 0;
            if (player.m_customData.TryGetValue(DataPrefix + quest.Key + "_Count", out var str) && int.TryParse(str, out var v))
                return v;
            return 0;
        }

        private static void SetProgress(Player player, string questKey, int value)
        {
            player.m_customData[DataPrefix + questKey + "_Count"] = value.ToString();
        }

        /// <summary>퀘스트 완료(처치 목표 달성) 여부. 저장된 완료 플래그로 판정한다.</summary>
        public static bool IsCompleted(Player player, QuestDefinition quest)
        {
            if (player == null || quest == null) return false;
            return player.m_customData.TryGetValue(DataPrefix + quest.Key + "_Completed", out var str) && str == "1";
        }

        private static void SetCompleted(Player player, string questKey, bool value)
        {
            player.m_customData[DataPrefix + questKey + "_Completed"] = value ? "1" : "0";
        }

        public static bool IsClaimed(Player player, string questKey)
        {
            if (player == null) return false;
            return player.m_customData.TryGetValue(DataPrefix + questKey + "_Claimed", out var str) && str == "1";
        }

        private static void SetClaimed(Player player, string questKey, bool value)
        {
            player.m_customData[DataPrefix + questKey + "_Claimed"] = value ? "1" : "0";
        }

        /// <summary>RequiredLevel이 0이면 항상 true. 0보다 크면 로컬 플레이어의 현재 레벨(EpicMMO 또는
        /// 자체 레벨 시스템, CaptainMMOBridge.GetLevel())이 그 이상이어야 true.</summary>
        public static bool IsLevelRequirementMet(QuestDefinition quest)
        {
            if (quest == null || quest.RequiredLevel <= 0) return true;
            return CaptainSkillTree.MMO_System.CaptainMMOBridge.GetLevel() >= quest.RequiredLevel;
        }

        // ===== 진행도 증가 =====

        /// <summary>퀘스트 진행 카운트를 delta만큼 증가시키고, 목표 달성 시 완료 처리한다.</summary>
        public static void AddProgress(Player player, QuestDefinition quest, int delta = 1)
        {
            if (player == null || quest == null || !Quest_Config.IsEnabled) return;
            if (IsCompleted(player, quest)) return; // 이미 완료된 퀘스트는 더 진행하지 않음
            if (!IsLevelRequirementMet(quest)) return; // 요구 레벨 미달 시 진행도 자체가 쌓이지 않음

            int current = GetProgress(player, quest);
            int next = Mathf.Min(current + delta, quest.Amount);
            SetProgress(player, quest.Key, next);

            if (next > current)
            {
                // 발헤임에서 자원 채집 시 나는 "채집됨" 효과음을 퀘스트 진행 카운트마다 재생
                VFXManager.PlaySound("sfx_pickable_pick", player.transform.position, 1.5f);
            }

            if (next >= quest.Amount)
            {
                SetCompleted(player, quest.Key, true);
                NotifyCompleted(player, quest);
                CaptainSkillTree.MMO_System.QuestIconPatch.UpdateClaimableDot();
            }

            // 퀘스트 창이 열려있는 동안에도 진행 수치가 실시간으로 보이도록 즉시 새로고침
            if (CaptainSkillTree.Gui.QuestPanelUI.IsOpen)
                CaptainSkillTree.Gui.QuestPanelUI.Refresh();
        }

        /// <summary>"서로 다른 종류 N가지" 퀘스트 전용 진행도 증가. 지금까지 확인한 프리팹명을 커스텀데이터에 누적하고, 중복은 무시한다.</summary>
        public static void AddDistinctProgress(Player player, QuestDefinition quest, string prefabName)
        {
            if (player == null || quest == null || string.IsNullOrEmpty(prefabName) || !Quest_Config.IsEnabled) return;
            if (IsCompleted(player, quest)) return;

            string seenKey = DataPrefix + quest.Key + "_Seen";
            player.m_customData.TryGetValue(seenKey, out var seenStr);
            var seen = string.IsNullOrEmpty(seenStr)
                ? new List<string>()
                : new List<string>(seenStr.Split(','));

            if (seen.Contains(prefabName, StringComparer.OrdinalIgnoreCase)) return; // 이미 확인한 종류

            seen.Add(prefabName);
            player.m_customData[seenKey] = string.Join(",", seen);

            int next = Mathf.Min(seen.Count, quest.Amount);
            SetProgress(player, quest.Key, next);

            VFXManager.PlaySound("sfx_pickable_pick", player.transform.position, 1.5f);

            if (next >= quest.Amount)
            {
                SetCompleted(player, quest.Key, true);
                NotifyCompleted(player, quest);
                CaptainSkillTree.MMO_System.QuestIconPatch.UpdateClaimableDot();
            }

            if (CaptainSkillTree.Gui.QuestPanelUI.IsOpen)
                CaptainSkillTree.Gui.QuestPanelUI.Refresh();
        }

        /// <summary>internal: QuestBossTrophyPatch가 트로피 실시간 추적 보스 퀘스트의 목표 최초 달성 시점에도 호출한다.</summary>
        internal static void NotifyCompleted(Player player, QuestDefinition quest)
        {
            string detailKey;
            object[] detailArgs;

            if (quest.Type == QuestType.Plant || quest.Type == QuestType.Cook || quest.Type == QuestType.Fish)
            {
                string typeName = quest.Type == QuestType.Plant ? "plant" : quest.Type == QuestType.Cook ? "cook" : "fish";
                detailKey = quest.RequireDistinctTargets ? $"quest_completed_detail_{typeName}_distinct" : $"quest_completed_detail_{typeName}";
                detailArgs = new object[] { quest.Amount };
            }
            else
            {
                string targetName = L.Has("item_" + quest.TargetPrefab.ToLowerInvariant())
                    ? L.Get("item_" + quest.TargetPrefab.ToLowerInvariant())
                    : quest.TargetPrefab;
                detailKey = quest.Type == QuestType.Gather ? "quest_completed_detail_gather" : "quest_completed_detail_kill";
                detailArgs = new object[] { targetName, quest.Amount };
            }

            string message = $"<color=yellow>{L.Get("quest_completed_title")}</color>\n{L.Get(detailKey, detailArgs)}";
            MessageHud.instance?.ShowMessage(MessageHud.MessageType.Center, message);

            VFXManager.PlayVFXMultiplayer("confetti_blast_multicolor", "", player.transform.position, Quaternion.identity, 2f);
            VFXManager.PlayVFXMultiplayer("vfx_shieldgenerator_refuel", "", player.transform.position, Quaternion.identity, 3f);
            if (Quest_Config.QuestCompleteMusicEnabled?.Value != false)
                CaptainSkillTree.Audio.QuestCompleteSoundManager.Instance.PlayOnce(Quest_Config.QuestCompleteMusicVolumeValue);
        }

        // ===== 경험치 아이템 보상 (WackyEpicMMOSystem의 마법 오브 + XP 포션 실물 지급) =====
        // 프리팹명은 md/WACKY_EPICMMO_ITEMS_ANALYSIS.md에 분석해 둔 실제 소스 기준.

        /// <summary>바이옴 → 마법 오브 프리팹(mmo_orb1~8, 난이도 순).</summary>
        private static readonly Dictionary<string, string> BiomeOrbPrefabs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Meadows"] = "mmo_orb1",
            ["BlackForest"] = "mmo_orb2",
            ["Swamp"] = "mmo_orb3",
            ["Mountain"] = "mmo_orb4",
            ["Plains"] = "mmo_orb5",
            ["Mistlands"] = "mmo_orb6",
            ["Ashlands"] = "mmo_orb7",
            ["DeepNorth"] = "mmo_orb8", // 미구현 바이옴 - 실제 퀘스트 추가 시 Quest_Config의 Biome 값과 일치시킬 것
        };

        /// <summary>바이옴 → XP 포션 프리팹(mmo_xp_drink1/2/3, 포션은 3단계뿐이라 난이도별로 묶음).</summary>
        private static readonly Dictionary<string, string> BiomePotionPrefabs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Meadows"] = "mmo_xp_drink1",
            ["BlackForest"] = "mmo_xp_drink1",
            ["Swamp"] = "mmo_xp_drink1",
            ["Mountain"] = "mmo_xp_drink2",
            ["Plains"] = "mmo_xp_drink2",
            ["Mistlands"] = "mmo_xp_drink3",
            ["Ashlands"] = "mmo_xp_drink3",
            ["DeepNorth"] = "mmo_xp_drink3",
        };

        /// <summary>Override 값으로 쓰이는 "None" 센티널 - 바이옴 기본 오브/포션 지급을 명시적으로 끈다.</summary>
        private const string NoRewardOverride = "None";

        public static string GetQuestOrbPrefab(QuestDefinition quest)
        {
            if (quest == null) return null;
            if (string.Equals(quest.OrbPrefabOverride, NoRewardOverride, StringComparison.OrdinalIgnoreCase)) return null;
            if (!string.IsNullOrEmpty(quest.OrbPrefabOverride)) return quest.OrbPrefabOverride;
            BiomeOrbPrefabs.TryGetValue(quest.Biome ?? "", out var prefab);
            return prefab;
        }

        public static string GetQuestPotionPrefab(QuestDefinition quest)
        {
            if (quest == null) return null;
            if (string.Equals(quest.PotionPrefabOverride, NoRewardOverride, StringComparison.OrdinalIgnoreCase)) return null;
            if (!string.IsNullOrEmpty(quest.PotionPrefabOverride)) return quest.PotionPrefabOverride;
            BiomePotionPrefabs.TryGetValue(quest.Biome ?? "", out var prefab);
            return prefab;
        }

        /// <summary>바이옴 내 표시 순서(DisplayOrder)를 기준으로 1~3개 선형 배분 (맨 위 퀘스트=1개, 보스=3개).
        /// RewardItemCountOverride가 0보다 크면 그 값을 그대로 사용한다.</summary>
        public static int GetQuestRewardItemCount(QuestDefinition quest)
        {
            if (quest == null) return 1;
            if (quest.RewardItemCountOverride > 0) return quest.RewardItemCountOverride;

            var siblings = GetActiveQuests().Where(q => string.Equals(q.Biome, quest.Biome, StringComparison.OrdinalIgnoreCase)).ToList();
            int n = siblings.Count;
            if (n <= 1) return 1;

            // 위에서부터 1,1,...,1,2,3(보스) 순 - 뒤쪽 두 자리만 늘리고 나머지는 최소치 유지.
            int i = quest.DisplayOrder;
            if (i == n - 1) return 3;
            if (i == n - 2) return 2;
            return 1;
        }

        // ===== 보상 클레임 =====

        /// <summary>완료(미수령) 상태 확인 후 퀘스트 보상을 캐릭터 앞에 지급한다.</summary>
        public static bool ClaimReward(Player player, QuestDefinition quest)
        {
            if (player == null || quest == null) return false;
            if (!IsCompleted(player, quest) || IsClaimed(player, quest.Key)) return false;
            if (!IsLevelRequirementMet(quest)) return false;

            Vector3 dropPos = player.transform.position + player.transform.forward * 1.5f + Vector3.up * 0.3f;

            if (quest.HasCoinReward)
            {
                int amount = UnityEngine.Random.Range(quest.CoinMin, quest.CoinMax + 1);
                QuestRewardSpawner.GrantCoins(player, amount, dropPos);
            }
            if (quest.HasItemReward1)
                QuestRewardSpawner.SpawnStackedItem(quest.ItemReward1, quest.ItemReward1Amount, dropPos);
            if (quest.HasItemReward2)
                QuestRewardSpawner.SpawnStackedItem(quest.ItemReward2, quest.ItemReward2Amount, dropPos);
            if (quest.HasSpecialReward)
                QuestRewardSpawner.GrantSpecialReward(player, quest.SpecialReward, dropPos);

            int itemRewardCount = GetQuestRewardItemCount(quest);
            string orbPrefab = GetQuestOrbPrefab(quest);
            if (!string.IsNullOrEmpty(orbPrefab))
                QuestRewardSpawner.SpawnStackedItem(orbPrefab, itemRewardCount, dropPos);
            string potionPrefab = GetQuestPotionPrefab(quest);
            if (!string.IsNullOrEmpty(potionPrefab))
                QuestRewardSpawner.SpawnStackedItem(potionPrefab, itemRewardCount, dropPos);

            SetClaimed(player, quest.Key, true);
            CaptainSkillTree.MMO_System.QuestIconPatch.UpdateClaimableDot();

            VFXManager.PlaySound("sfx_lootspawn", dropPos, 2f);

            return true;
        }
    }
}

namespace CaptainSkillTree.SkillTree
{
    /// <summary>Gather / Kill / KillBoss / Plant / Cook / Fish 퀘스트 종류</summary>
    public enum QuestType
    {
        Gather,
        Kill,
        KillBoss,
        Plant,
        Cook,
        Fish
    }

    /// <summary>Quest_Config에서 읽어온 퀘스트 1개의 런타임 스냅샷 (읽기 전용)</summary>
    public class QuestDefinition
    {
        /// <summary>"{Biome}_{Id}" 형태의 고유 키 (예: "Meadows_Quest1")</summary>
        public string Key;
        public string Biome;
        public QuestType Type;
        public string TargetPrefab;
        public int Amount;

        public string ItemReward1;
        public int ItemReward1Amount;
        public string ItemReward2;
        public int ItemReward2Amount;

        public int CoinMin;
        public int CoinMax;

        /// <summary>빈 문자열이면 없음. "TameLoxCalf" / "JumpProficiency:10" 형태</summary>
        public string SpecialReward;

        /// <summary>퀘스트 패널 표시 순서(바이옴 내 0부터, 보스는 항상 가장 큼).</summary>
        public int DisplayOrder;

        /// <summary>true면 TargetPrefab 종류가 아니라 "서로 다른 종류를 몇 가지 확보했는가"로 진행도를 센다(Plant/Cook/Fish 전용).</summary>
        public bool RequireDistinctTargets;

        /// <summary>비어 있으면 바이옴 기본 매핑(BiomeOrbPrefabs) 사용, 있으면 이 값 우선.</summary>
        public string OrbPrefabOverride;
        /// <summary>비어 있으면 바이옴 기본 매핑(BiomePotionPrefabs) 사용, 있으면 이 값 우선.</summary>
        public string PotionPrefabOverride;
        /// <summary>0이면 DisplayOrder 기반 자동 배분 공식 사용, 0보다 크면 이 값 그대로 지급.</summary>
        public int RewardItemCountOverride;

        /// <summary>0이면 제한 없음. 0보다 크면 플레이어 레벨이 이 값 이상이어야 진행/클레임 가능(보스/특수 퀘스트 전용).</summary>
        public int RequiredLevel;

        public bool HasItemReward1 => !string.IsNullOrEmpty(ItemReward1) && ItemReward1Amount > 0;
        public bool HasItemReward2 => !string.IsNullOrEmpty(ItemReward2) && ItemReward2Amount > 0;
        public bool HasCoinReward => CoinMax > 0;
        public bool HasSpecialReward => !string.IsNullOrEmpty(SpecialReward);
        public bool HasLevelRequirement => RequiredLevel > 0;
    }
}

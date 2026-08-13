using System.Collections.Generic;
using BepInEx.Configuration;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 퀘스트 하나의 Config 항목 묶음 (Enabled/Type/TargetPrefab/Amount/보상/코인/특수보상)
    /// </summary>
    public class QuestConfigEntry
    {
        public ConfigEntry<bool> Enabled;
        public ConfigEntry<string> Type; // "Gather" / "Kill" / "KillBoss"
        public ConfigEntry<string> TargetPrefab;
        public ConfigEntry<int> Amount;
        public ConfigEntry<string> ItemReward1;
        public ConfigEntry<int> ItemReward1Amount;
        public ConfigEntry<string> ItemReward2;
        public ConfigEntry<int> ItemReward2Amount;
        public ConfigEntry<int> CoinMin;
        public ConfigEntry<int> CoinMax;
        public ConfigEntry<string> SpecialReward; // 빈 값 / "TameLoxCalf" / "JumpProficiency:10"
        public ConfigEntry<string> OrbPrefabOverride;
        public ConfigEntry<string> PotionPrefabOverride;
        public ConfigEntry<int> RewardItemCountOverride;
        /// <summary>0이면 제한 없음. 0보다 크면 플레이어 레벨이 이 값 이상이어야 진행/클레임 가능(보스/특수 퀘스트 전용).</summary>
        public ConfigEntry<int> RequiredLevel;

        /// <summary>퀘스트 패널 표시 순서(바이옴 내 0부터). Config 동기화 대상 아님 - Seeds에서 그대로 복사.</summary>
        public int DisplayOrder;
        /// <summary>true면 "서로 다른 종류 N가지" 방식으로 진행도 추적(Plant/Cook/Fish 전용). Config 동기화 대상 아님.</summary>
        public bool RequireDistinctTargets;
    }

    /// <summary>
    /// 바이옴별 자원채집/몬스터처치 퀘스트 Config.
    /// 퀘스트 목록은 Seeds 배열의 기본값으로 시작하며, 실제 값은 전부 BindServerSync로
    /// F1 Config Manager에서 조정 가능. 신규 퀘스트는 Seeds에 항목을 추가하면 됨.
    /// </summary>
    public static class Quest_Config
    {
        public static ConfigEntry<bool> QuestSystemEnabled;

        /// <summary>퀘스트 완료 시 재생되는 완료음 on/off. 서버 동기화 없이 클라이언트 개인 취향 설정으로 둔다
        /// (ShowExpPopup 등 다른 클라이언트 로컬 표시/사운드 토글과 동일한 패턴).</summary>
        public static ConfigEntry<bool> QuestCompleteMusicEnabled;

        /// <summary>퀘스트 완료음 볼륨(%). QuestCompleteMusicEnabled와 동일하게 서버 동기화 없이 클라이언트 개인 설정.</summary>
        public static ConfigEntry<int> QuestCompleteMusicVolume;

        /// <summary>0~100(%) 값을 QuestCompleteSoundManager.PlayOnce()가 받는 0~1 볼륨 배율로 변환.</summary>
        public static float QuestCompleteMusicVolumeValue => Mathf.Clamp((QuestCompleteMusicVolume?.Value ?? 70) / 100f, 0f, 1f);

        /// <summary>퀘스트 패널 토글 단축키. QuestCompleteMusicEnabled와 동일하게 서버 동기화 없이 클라이언트 로컬 설정
        /// (SkillTreeConfig의 HotKeyY/R/G/H와 동일한 ConfigEntry&lt;KeyboardShortcut&gt; 패턴).</summary>
        public static ConfigEntry<KeyboardShortcut> QuestToggleKey;

        /// <summary>
        /// 서버 동기화가 반영된 실제 활성화 여부. md/SERVER_SYNC_RULES.md 규칙에 따라
        /// 어드민 Config를 그대로 쓰지 않고 SkillTreeConfig.GetEffectiveValue()를 거친다.
        /// </summary>
        public static bool IsEnabled =>
            SkillTreeConfig.GetEffectiveValue(SyncKeySystemEnabled, QuestSystemEnabled?.Value == true ? 1f : 0f) > 0.5f;

        /// <summary>키: "{Biome}_{Id}" (예: "Meadows_Quest1")</summary>
        public static readonly Dictionary<string, QuestConfigEntry> Quests = new Dictionary<string, QuestConfigEntry>();

        // ===== 서버 동기화 키 (Broadcast.cs / QuestManager.cs 둘 다 이 헬퍼로만 키를 만들어
        // md/SERVER_SYNC_RULES.md가 경고하는 "문자열 불일치" 위험을 없앤다) =====
        // 수치 필드는 기존 SkillTreeConfig의 float 동기화 채널을, 문자열 필드는
        // Quest_StringSync의 별도 채널을 사용한다(기존 250여개 float Config에 영향 없이 분리).
        public const string SyncKeySystemEnabled = "Quest_SystemEnabled";
        public static string SyncKeyEnabled(string prefix) => $"Quest_{prefix}_Enabled";
        public static string SyncKeyAmount(string prefix) => $"Quest_{prefix}_Amount";
        public static string SyncKeyType(string prefix) => $"Quest_{prefix}_Type";
        public static string SyncKeyTargetPrefab(string prefix) => $"Quest_{prefix}_TargetPrefab";
        public static string SyncKeyItem1(string prefix) => $"Quest_{prefix}_ItemReward1";
        public static string SyncKeyItem2(string prefix) => $"Quest_{prefix}_ItemReward2";
        public static string SyncKeySpecialReward(string prefix) => $"Quest_{prefix}_SpecialReward";
        public static string SyncKeyItem1Amount(string prefix) => $"Quest_{prefix}_ItemReward1_Amount";
        public static string SyncKeyItem2Amount(string prefix) => $"Quest_{prefix}_ItemReward2_Amount";
        public static string SyncKeyCoinMin(string prefix) => $"Quest_{prefix}_CoinMin";
        public static string SyncKeyCoinMax(string prefix) => $"Quest_{prefix}_CoinMax";
        public static string SyncKeyOrbPrefab(string prefix) => $"Quest_{prefix}_OrbPrefabOverride";
        public static string SyncKeyPotionPrefab(string prefix) => $"Quest_{prefix}_PotionPrefabOverride";
        public static string SyncKeyRewardItemCount(string prefix) => $"Quest_{prefix}_RewardItemCountOverride";
        public static string SyncKeyRequiredLevel(string prefix) => $"Quest_{prefix}_RequiredLevel";

        private struct QuestSeed
        {
            public string Biome, Id, Type, TargetPrefab;
            public int Amount;
            public string Item1; public int Item1Amt;
            public string Item2; public int Item2Amt;
            public int CoinMin, CoinMax;
            public string Special;
            /// <summary>퀘스트 패널 표시 순서(바이옴 내 0부터, 보스는 항상 마지막).</summary>
            public int DisplayOrder;
            public bool RequireDistinct;
            public string OrbOverride;
            public string PotionOverride;
            public int RewardCountOverride;
            /// <summary>0이면 제한 없음. 보스/특수(히든) 퀘스트에 바이옴별 최소 레벨 조건을 걸 때 사용.</summary>
            public int RequiredLevel;
        }

        private static readonly QuestSeed[] Seeds = new[]
        {
            // ===== 생활 Life ===== (전투 무관 - 농사/요리/낚시. Meadows보다 먼저 나열해 목초지 위에 표시되게 한다)
            new QuestSeed{ Biome="Life", Id="Quest1", Type="Plant", TargetPrefab="", Amount=100, CoinMin=100, CoinMax=100, Special="FarmingSkill:2", OrbOverride="mmo_orb1", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=0 },
            new QuestSeed{ Biome="Life", Id="Quest2", Type="Plant", TargetPrefab="", Amount=1000, CoinMin=250, CoinMax=250, Special="FarmingSkill:8", OrbOverride="mmo_orb3", PotionOverride="mmo_xp_drink2", RewardCountOverride=1, DisplayOrder=1 },
            new QuestSeed{ Biome="Life", Id="Quest3", Type="Plant", TargetPrefab="", Amount=10, RequireDistinct=true, CoinMin=300, CoinMax=300, Special="FarmingSkill:10", OrbOverride="mmo_orb4", PotionOverride="mmo_xp_drink3", RewardCountOverride=1, DisplayOrder=2 },
            new QuestSeed{ Biome="Life", Id="Quest4", Type="Cook", TargetPrefab="", Amount=50, CoinMin=50, CoinMax=50, Special="CookingSkill:2", OrbOverride="mmo_orb1", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=3 },
            new QuestSeed{ Biome="Life", Id="Quest5", Type="Cook", TargetPrefab="", Amount=500, CoinMin=200, CoinMax=200, Special="CookingSkill:8", OrbOverride="mmo_orb1", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=4 },
            new QuestSeed{ Biome="Life", Id="Quest6", Type="Cook", TargetPrefab="", Amount=50, RequireDistinct=true, CoinMin=500, CoinMax=500, Special="CookingSkill:10", OrbOverride="mmo_orb3", PotionOverride="mmo_xp_drink3", RewardCountOverride=1, DisplayOrder=5 },
            new QuestSeed{ Biome="Life", Id="Quest7", Type="Fish", TargetPrefab="", Amount=10, CoinMin=100, CoinMax=100, Special="FishingSkill:2", OrbOverride="mmo_orb1", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=6 },
            new QuestSeed{ Biome="Life", Id="Quest8", Type="Fish", TargetPrefab="", Amount=50, CoinMin=200, CoinMax=200, Special="FishingSkill:8", OrbOverride="mmo_orb2", PotionOverride="mmo_xp_drink2", RewardCountOverride=1, DisplayOrder=7 },
            new QuestSeed{ Biome="Life", Id="Quest9", Type="Fish", TargetPrefab="", Amount=12, RequireDistinct=true, CoinMin=1000, CoinMax=1000, Special="FishingSkill:20", OrbOverride="mmo_orb3", PotionOverride="mmo_xp_drink3", RewardCountOverride=1, DisplayOrder=8 },

            // ===== 목초지 Meadows ===== (표시순서: Raspberry(0) > Boar(1) > Neck(2) > Deer(3) > Eikthyr보스(4) > EikthyrFlawless(5))
            // 오브/포션 보상은 아래 명시된 퀘스트에만 부여(OrbOverride/PotionOverride="None"은 지급 안 함)
            new QuestSeed{ Biome="Meadows", Id="Quest1", Type="Kill", TargetPrefab="Deer", Amount=20, Item1="CookedDeerMeat", Item1Amt=5, Item2="DeerHide", Item2Amt=20, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=3 },
            new QuestSeed{ Biome="Meadows", Id="Quest2", Type="Kill", TargetPrefab="Neck", Amount=30, Item1="Flint", Item1Amt=20, CoinMin=30, CoinMax=30, OrbOverride="None", PotionOverride="None", DisplayOrder=2 },
            new QuestSeed{ Biome="Meadows", Id="Quest3", Type="Kill", TargetPrefab="Boar", Amount=15, Item1="LeatherScraps", Item1Amt=12, CoinMin=50, CoinMax=50, OrbOverride="None", PotionOverride="None", DisplayOrder=1 },
            new QuestSeed{ Biome="Meadows", Id="Quest4", Type="KillBoss", TargetPrefab="Eikthyr", Amount=5, Item1="SurtlingCore", Item1Amt=10, CoinMin=100, CoinMax=100, PotionOverride="None", RewardCountOverride=1, DisplayOrder=4, RequiredLevel=10 },
            new QuestSeed{ Biome="Meadows", Id="Quest5", Type="Gather", TargetPrefab="Raspberry", Amount=20, Item1="QueenBee", Item1Amt=1, OrbOverride="None", PotionOverride="None", DisplayOrder=0 },
            // 특수: 무피해 + 무음식 상태로 에이크시르 처치(QuestKillPatch.cs가 QuestEikthyrFlawlessTracker로 조건 검증 후에만 진행도 반영)
            new QuestSeed{ Biome="Meadows", Id="Quest6", Type="KillBoss", TargetPrefab="Eikthyr", Amount=1, Special="RandomItem:HelmetTrollLeather,ArmorTrollLeatherChest,ArmorTrollLeatherLegs", CoinMin=500, CoinMax=500, PotionOverride="None", RewardCountOverride=1, DisplayOrder=5, RequiredLevel=10 },

            // ===== 검은숲 BlackForest ===== (표시순서: Greydwarf20(0) > Greydwarf50(1) > GreydwarfElite(2) > Bjorn(3) > Troll(4) > gd_king보스(5) > gd_king근접전용(6))
            // 오브는 전 퀘스트 미지급, 포션은 전 퀘스트 mmo_xp_drink1(경험치 포션 소) 지급(수량은 RewardCountOverride로 명시)
            new QuestSeed{ Biome="BlackForest", Id="Quest1", Type="Kill", TargetPrefab="Greydwarf", Amount=20, CoinMin=50, CoinMax=50, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=0 },
            new QuestSeed{ Biome="BlackForest", Id="Quest2", Type="Kill", TargetPrefab="Greydwarf", Amount=50, Item1="FineWood", Item1Amt=10, CoinMin=80, CoinMax=80, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=1 },
            new QuestSeed{ Biome="BlackForest", Id="Quest3", Type="Kill", TargetPrefab="Troll", Amount=5, Item1="SurtlingCore", Item1Amt=10, CoinMin=110, CoinMax=110, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=4 },
            new QuestSeed{ Biome="BlackForest", Id="Quest4", Type="Kill", TargetPrefab="Bjorn", Amount=10, Item1="TinOre", Item1Amt=50, CoinMin=120, CoinMax=120, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=2, DisplayOrder=3 },
            new QuestSeed{ Biome="BlackForest", Id="Quest5", Type="KillBoss", TargetPrefab="gd_king", Amount=5, Item1="SurtlingCore", Item1Amt=6, Item2="IronScrap", Item2Amt=10, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=2, DisplayOrder=5, RequiredLevel=20 },
            // 신규: 난쟁이 엘리(그레이드워프브루트) 처치
            new QuestSeed{ Biome="BlackForest", Id="Quest6", Type="Kill", TargetPrefab="Greydwarf_Elite", Amount=10, Item1="TrollHide", Item1Amt=6, CoinMin=80, CoinMax=80, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=2 },
            // 신규/히든: 근접 무기로만 엘더 처치 (QuestKillPatch.cs가 QuestGdKingMeleeOnlyTracker로 조건 검증 후에만 진행도 반영)
            new QuestSeed{ Biome="BlackForest", Id="Quest7", Type="KillBoss", TargetPrefab="gd_king", Amount=1, Special="RandomItem:HelmetIron,ArmorIronChest,ArmorIronLegs,HelmetBronze,ArmorBronzeChest,ArmorBronzeLegs", CoinMin=800, CoinMax=800, OrbOverride="None", PotionOverride="None", DisplayOrder=6, RequiredLevel=20 },

            // ===== 늪지 Swamp ===== (표시순서: Draugr20(0) > Draugr300(1) > Blob(2) > Wraith(3) > Abomination(4) > Bonemass보스(5))
            // 오브는 Abomination만 Lv2 지급, 그 외는 미지급. 포션은 전 퀘스트 지급(등급/수량은 명시값)
            new QuestSeed{ Biome="Swamp", Id="Quest2", Type="Kill", TargetPrefab="Draugr", Amount=20, CoinMin=120, CoinMax=120, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=0 },
            // 신규: 드라우그 대량 처치(2단계)
            new QuestSeed{ Biome="Swamp", Id="Quest6", Type="Kill", TargetPrefab="Draugr", Amount=300, Item1="Guck", Item1Amt=50, CoinMin=200, CoinMax=200, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=1 },
            new QuestSeed{ Biome="Swamp", Id="Quest4", Type="Kill", TargetPrefab="Blob", Amount=5, Item1="SurtlingCore", Item1Amt=5, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=1, DisplayOrder=2 },
            new QuestSeed{ Biome="Swamp", Id="Quest3", Type="Kill", TargetPrefab="Wraith", Amount=10, Item1="SurtlingCore", Item1Amt=10, CoinMin=150, CoinMax=150, OrbOverride="None", PotionOverride="mmo_xp_drink1", RewardCountOverride=2, DisplayOrder=3 },
            new QuestSeed{ Biome="Swamp", Id="Quest1", Type="Kill", TargetPrefab="Abomination", Amount=10, Item1="PickaxeIron", Item1Amt=1, OrbOverride="mmo_orb2", PotionOverride="None", RewardCountOverride=1, DisplayOrder=4 },
            new QuestSeed{ Biome="Swamp", Id="Quest5", Type="KillBoss", TargetPrefab="Bonemass", Amount=5, Item1="SurtlingCore", Item1Amt=5, Item2="SilverOre", Item2Amt=20, OrbOverride="None", PotionOverride="mmo_xp_drink2", RewardCountOverride=1, DisplayOrder=5, RequiredLevel=30 },
            // 신규/히든: 마법 공격으로만 본메스 처치 (QuestKillPatch.cs가 QuestBonemassMagicOnlyTracker로 조건 검증 후에만 진행도 반영)
            new QuestSeed{ Biome="Swamp", Id="Quest7", Type="KillBoss", TargetPrefab="Bonemass", Amount=1, Special="RandomItem:HelmetIron,ArmorIronChest,ArmorIronLegs,HelmetDrake,ArmorWolfChest,ArmorWolfLegs", CoinMin=1200, CoinMax=1200, OrbOverride="None", PotionOverride="None", DisplayOrder=6, RequiredLevel=30 },

            // ===== 산 Mountain ===== (표시순서: Wolf(0) > Fenring(1) > StoneGolem(2) > Dragon보스(3) > ModerMeleeOnly(4) > ModerRangedOnly(5))
            // 마법 오브 보상은 4개 퀘스트 전부 미지급(OrbOverride="None"), 코인은 범위 대신 고정값 사용
            new QuestSeed{ Biome="Mountain", Id="Quest1", Type="Kill", TargetPrefab="StoneGolem", Amount=10, Item1="SilverOre", Item1Amt=100, CoinMin=380, CoinMax=380, OrbOverride="None", DisplayOrder=2 },
            new QuestSeed{ Biome="Mountain", Id="Quest2", Type="Kill", TargetPrefab="Wolf", Amount=100, CoinMin=200, CoinMax=200, Special="StaminaBonus:10", OrbOverride="None", DisplayOrder=0 },
            new QuestSeed{ Biome="Mountain", Id="Quest3", Type="Kill", TargetPrefab="Fenring", Amount=10, Item1="SurtlingCore", Item1Amt=5, CoinMin=350, CoinMax=350, OrbOverride="None", DisplayOrder=1 },
            new QuestSeed{ Biome="Mountain", Id="Quest4", Type="KillBoss", TargetPrefab="Dragon", Amount=5, Item1="SilverOre", Item1Amt=150, CoinMin=500, CoinMax=500, OrbOverride="None", DisplayOrder=3, RequiredLevel=40 },
            // 신규/히든: 근접 무기로만 모더 처치 (QuestKillPatch.cs가 QuestModerMeleeOnlyTracker로 조건 검증 후에만 진행도 반영)
            new QuestSeed{ Biome="Mountain", Id="Quest5", Type="KillBoss", TargetPrefab="Dragon", Amount=1, Special="MeleeWeaponSkill:5", OrbOverride="None", PotionOverride="None", DisplayOrder=4, RequiredLevel=40 },
            // 신규/히든: 원거리(활/석궁/마법) 무기로만 모더 처치 (QuestKillPatch.cs가 QuestModerRangedOnlyTracker로 조건 검증 후에만 진행도 반영)
            new QuestSeed{ Biome="Mountain", Id="Quest6", Type="KillBoss", TargetPrefab="Dragon", Amount=1, Special="RangedWeaponSkill:5", OrbOverride="None", PotionOverride="None", DisplayOrder=5, RequiredLevel=40 },

            // ===== 평원 Plains ===== (표시순서: Goblin(0) > GoblinBrute(1) > GoblinShaman(2) > Lox(3) > GoblinKing보스(4))
            new QuestSeed{ Biome="Plains", Id="Quest1", Type="Kill", TargetPrefab="GoblinShaman", Amount=20, CoinMin=250, CoinMax=350, Special="ElementalDamage:2", DisplayOrder=2 },
            new QuestSeed{ Biome="Plains", Id="Quest2", Type="Kill", TargetPrefab="Goblin", Amount=100, Item1="SurtlingCore", Item1Amt=10, Item2="GoblinTotem", Item2Amt=5, CoinMin=250, CoinMax=350, DisplayOrder=0 },
            new QuestSeed{ Biome="Plains", Id="Quest3", Type="Kill", TargetPrefab="GoblinBrute", Amount=10, CoinMin=300, CoinMax=400, Special="TameWolfCub", DisplayOrder=1 },
            new QuestSeed{ Biome="Plains", Id="Quest4", Type="Kill", TargetPrefab="Lox", Amount=100, Special="TameLoxCalf", DisplayOrder=3 },
            new QuestSeed{ Biome="Plains", Id="Quest5", Type="KillBoss", TargetPrefab="GoblinKing", Amount=5, Item1="SurtlingCore", Item1Amt=10, Special="JumpProficiency:10", DisplayOrder=4, RequiredLevel=50 },

            // ===== 안개숲 Mistlands ===== (표시순서: Seeker(0) > Gjall(1) > SeekerBrute(2) > SeekerQueen보스(3))
            new QuestSeed{ Biome="Mistlands", Id="Quest1", Type="Kill", TargetPrefab="SeekerBrute", Amount=10, Item1="Softtissue", Item1Amt=20, CoinMin=350, CoinMax=500, DisplayOrder=2 },
            new QuestSeed{ Biome="Mistlands", Id="Quest2", Type="Kill", TargetPrefab="Seeker", Amount=30, CoinMin=350, CoinMax=500, Special="PhysicalDamage:1", DisplayOrder=0 },
            new QuestSeed{ Biome="Mistlands", Id="Quest3", Type="Kill", TargetPrefab="Gjall", Amount=5, Item1="Softtissue", Item1Amt=20, CoinMin=450, CoinMax=600, DisplayOrder=1 },
            new QuestSeed{ Biome="Mistlands", Id="Quest4", Type="KillBoss", TargetPrefab="SeekerQueen", Amount=5, Item1="Softtissue", Item1Amt=200, DisplayOrder=3, RequiredLevel=60 },

            // ===== 잿빛땅 Ashlands ===== (표시순서: Morgen(0) > Asksvin(1) > FallenValkyrie(2) > Fader보스(3))
            new QuestSeed{ Biome="Ashlands", Id="Quest1", Type="Kill", TargetPrefab="FallenValkyrie", Amount=30, Item1="SurtlingCore", Item1Amt=20, CoinMin=500, CoinMax=700, DisplayOrder=2 },
            new QuestSeed{ Biome="Ashlands", Id="Quest2", Type="Kill", TargetPrefab="Asksvin", Amount=20, Item1="BellFragment", Item1Amt=2, CoinMin=500, CoinMax=700, DisplayOrder=1 },
            new QuestSeed{ Biome="Ashlands", Id="Quest3", Type="Kill", TargetPrefab="Morgen", Amount=8, Item1="BlackCore", Item1Amt=5, CoinMin=650, CoinMax=850, DisplayOrder=0 },
            new QuestSeed{ Biome="Ashlands", Id="Quest4", Type="KillBoss", TargetPrefab="Fader", Amount=5, Item1="SurtlingCore", Item1Amt=40, CoinMin=900, CoinMax=1200, DisplayOrder=3, RequiredLevel=70 },
        };

        public static void Initialize(ConfigFile config)
        {
            QuestSystemEnabled = SkillTreeConfig.BindServerSync(config, "Quest System", "QuestSystemEnabled", true,
                SkillTreeConfig.GetConfigDescription("QuestSystemEnabled"), order: 999);

            QuestCompleteMusicEnabled = config.Bind("Quest System", "QuestCompleteMusicEnabled", true,
                SkillTreeConfig.GetConfigDescription("QuestCompleteMusicEnabled"));

            QuestCompleteMusicVolume = config.Bind("Quest System", "QuestCompleteMusicVolume", 70,
                new ConfigDescription(
                    SkillTreeConfig.GetConfigDescription("QuestCompleteMusicVolume"),
                    new AcceptableValueRange<int>(0, 100)));

            QuestToggleKey = config.Bind(
                "Quest System",
                "QuestToggleKey",
                new KeyboardShortcut(KeyCode.J, KeyCode.LeftControl),
                new ConfigDescription(
                    SkillTreeConfig.GetConfigDescription("QuestToggleKey"),
                    null,
                    new ConfigurationManagerAttributes { IsAdminOnly = false, DispName = SkillTreeConfig.GetLocalizedKeyName("QuestToggleKey") }
                )
            );

            foreach (var seed in Seeds)
            {
                string section = $"Quest - {seed.Biome}";
                string prefix = $"{seed.Biome}_{seed.Id}";

                var entry = new QuestConfigEntry
                {
                    Enabled = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_Enabled", true,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_Enabled"), order: 10),
                    Type = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_Type", seed.Type,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_Type"), order: 9),
                    TargetPrefab = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_TargetPrefab", seed.TargetPrefab,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_TargetPrefab"), order: 8),
                    Amount = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_Amount", seed.Amount,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_Amount"), order: 7),
                    ItemReward1 = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_ItemReward1", seed.Item1 ?? "",
                        SkillTreeConfig.GetConfigDescription($"{prefix}_ItemReward1"), order: 6),
                    ItemReward1Amount = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_ItemReward1_Amount", seed.Item1Amt,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_ItemReward1_Amount"), order: 5),
                    ItemReward2 = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_ItemReward2", seed.Item2 ?? "",
                        SkillTreeConfig.GetConfigDescription($"{prefix}_ItemReward2"), order: 4),
                    ItemReward2Amount = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_ItemReward2_Amount", seed.Item2Amt,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_ItemReward2_Amount"), order: 3),
                    CoinMin = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_CoinMin", seed.CoinMin,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_CoinMin"), order: 2),
                    CoinMax = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_CoinMax", seed.CoinMax,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_CoinMax"), order: 1),
                    SpecialReward = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_SpecialReward", seed.Special ?? "",
                        SkillTreeConfig.GetConfigDescription($"{prefix}_SpecialReward"), order: 0),
                    OrbPrefabOverride = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_OrbPrefabOverride", seed.OrbOverride ?? "",
                        SkillTreeConfig.GetConfigDescription($"{prefix}_OrbPrefabOverride"), order: -2),
                    PotionPrefabOverride = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_PotionPrefabOverride", seed.PotionOverride ?? "",
                        SkillTreeConfig.GetConfigDescription($"{prefix}_PotionPrefabOverride"), order: -3),
                    RewardItemCountOverride = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_RewardItemCountOverride", seed.RewardCountOverride,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_RewardItemCountOverride"), order: -4),
                    RequiredLevel = SkillTreeConfig.BindServerSync(config, section, $"{prefix}_RequiredLevel", seed.RequiredLevel,
                        SkillTreeConfig.GetConfigDescription($"{prefix}_RequiredLevel"), order: 11),
                    DisplayOrder = seed.DisplayOrder,
                    RequireDistinctTargets = seed.RequireDistinct,
                };

                Quests[prefix] = entry;
            }

            // F1 Config Manager 등에서 값이 바뀌면 QuestManager의 캐시된 QuestDefinition 목록을 무효화한다.
            config.SettingChanged += (_, __) => QuestManager.InvalidateCache();

            Plugin.Log.LogDebug($"[Quest_Config] 퀘스트 {Quests.Count}개 초기화 완료");
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// 몬스터 경험치 데이터
    /// WackyEpicMMO의 DataMonsters.cs 구조 복제
    /// </summary>
    public class Monster
    {
        public string name;
        public int minExp;
        public int maxExp;
        public int level;

        public Monster(string name, int minExp, int maxExp, int level)
        {
            this.name = name;
            this.minExp = minExp;
            this.maxExp = maxExp;
            this.level = level;
        }
    }

    /// <summary>
    /// Captain Monster Exp
    /// 몬스터별 경험치 데이터 관리
    /// WackyEpicMMO Default.json 데이터 사용
    /// </summary>
    public static class CaptainMonsterExp
    {
        private static Dictionary<string, Monster> dictionary = new Dictionary<string, Monster>();
        private static bool isInitialized = false;

        public static void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;

            LoadDefaultData();

            if (CaptainExpJsonLoader.HasMonsterExpJson())
            {
                CaptainExpJsonLoader.LoadMonsterExp();
            }

            Plugin.Log.LogDebug($"[CaptainMonsterExp] 몬스터 경험치 데이터 로드 완료 ({dictionary.Count}종)");
        }

        /// <summary>
        /// 몬스터 추가 (WackyMMO 방식: Clone 붙여서 저장)
        /// </summary>
        private static void AddMonster(string name, int minExp, int maxExp, int level)
        {
            var monster = new Monster(name, minExp, maxExp, level);
            dictionary[$"{name}(Clone)"] = monster;
        }

        /// <summary>
        /// WackyEpicMMO Default.json 데이터 그대로 사용
        /// </summary>
        public static void LoadDefaultData()
        {
            dictionary.Clear();

            // ============================================
            // 초원 (Meadows) - WackyMMO 기준
            // ============================================
            AddMonster("Boar", 15, 20, 5);
            AddMonster("Deer", 10, 20, 3);
            AddMonster("Neck", 15, 20, 5);
            AddMonster("Greyling", 25, 40, 10);
            AddMonster("Hare", 10, 20, 6);
            AddMonster("Hen", 10, 20, 6);
            AddMonster("Chick", 5, 10, 3);

            // ============================================
            // 검은숲 (Black Forest) - WackyMMO 기준
            // ============================================
            AddMonster("Greydwarf", 50, 60, 12);
            AddMonster("Greydwarf_Elite", 100, 150, 20);
            AddMonster("Greydwarf_Shaman", 80, 120, 17);
            AddMonster("Skeleton", 60, 80, 15);
            AddMonster("Skeleton_Poison", 100, 150, 20);
            AddMonster("Skeleton_NoArcher", 60, 80, 18);
            AddMonster("Ghost", 80, 120, 20);
            AddMonster("Troll", 300, 500, 22);
            AddMonster("Bjorn", 300, 500, 22);
            AddMonster("TentaRoot", 5, 5, 15);

            // ============================================
            // 늪지대 (Swamp) - WackyMMO 기준
            // ============================================
            AddMonster("Blob", 120, 180, 28);
            AddMonster("Leech", 120, 180, 28);
            AddMonster("Wraith", 200, 250, 32);
            AddMonster("Draugr", 100, 150, 30);
            AddMonster("Draugr_Ranged", 180, 220, 31);
            AddMonster("Surtling", 100, 130, 30);
            AddMonster("Draugr_Elite", 300, 500, 34);
            AddMonster("BlobElite", 180, 250, 33);
            AddMonster("Abomination", 400, 600, 35);

            // ============================================
            // 산악 (Mountain) - WackyMMO 기준
            // ============================================
            AddMonster("Bat", 140, 180, 35);
            AddMonster("Wolf", 250, 350, 41);
            AddMonster("Fenring", 300, 400, 43);
            AddMonster("Fenring_Cultist", 300, 400, 44);
            AddMonster("Ulv", 300, 400, 42);
            AddMonster("Hatchling", 250, 300, 40);
            AddMonster("StoneGolem", 600, 700, 45);

            // ============================================
            // 바다 (Ocean) - WackyMMO 기준
            // ============================================
            AddMonster("Serpent", 400, 500, 30);

            // ============================================
            // 평원 (Plains) - WackyMMO 기준
            // ============================================
            AddMonster("BlobTar", 350, 450, 51);
            AddMonster("Goblin", 350, 450, 51);
            AddMonster("GoblinArcher", 380, 480, 52);
            AddMonster("Deathsquito", 200, 300, 50);
            AddMonster("Lox", 500, 700, 55);
            AddMonster("Unbjorn", 500, 700, 55);
            AddMonster("GoblinBrute", 450, 650, 54);
            AddMonster("GoblinShaman", 300, 400, 53);

            // ============================================
            // 안개땅 (Mistlands) - WackyMMO 기준
            // ============================================
            AddMonster("Seeker", 700, 900, 61);
            AddMonster("SeekerBrood", 400, 600, 60);
            AddMonster("SeekerBrute", 950, 1250, 62);
            AddMonster("Gjall", 1000, 1800, 63);
            AddMonster("Tick", 180, 240, 60);
            AddMonster("Dverger", 700, 900, 61);
            AddMonster("DvergerMage", 700, 900, 61);

            // ============================================
            // 재의 땅 (Ashlands) - WackyMMO 기준
            // ============================================
            AddMonster("Volture", 700, 900, 70);
            AddMonster("Charred_Archer", 1000, 1100, 71);
            AddMonster("Charred_Mage", 1000, 1100, 71);
            AddMonster("Charred_Melee", 1000, 1100, 71);
            AddMonster("Charred_Twitcher", 1000, 1100, 71);
            AddMonster("Charred_Archer_Fader", 500, 500, 71);
            AddMonster("Charred_Melee_Fader", 500, 500, 71);
            AddMonster("Charred_Twitcher_Summoned", 500, 500, 71);
            AddMonster("Charred_Melee_Dyrnwyn", 1500, 1700, 73);
            AddMonster("BlobLava", 1000, 1100, 71);
            AddMonster("Asksvin", 1200, 1300, 72);
            AddMonster("FallenValkyrie", 1500, 1800, 73);
            AddMonster("Morgen_NonSleeping", 1400, 1600, 73);
            AddMonster("Morgen", 1500, 1800, 73);
            AddMonster("BonemawSerpent", 1500, 1700, 73);

            // ============================================
            // Hildir 던전 몬스터 - WackyMMO 기준
            // ============================================
            AddMonster("Skeleton_Hildir", 600, 800, 23);
            AddMonster("Skeleton_Hildir_nochest", 600, 800, 23);
            AddMonster("Fenring_Cultist_Hildir", 1000, 1200, 43);
            AddMonster("Fenring_Cultist_Hildir_nochest", 1000, 1200, 43);
            AddMonster("GoblinBruteBros", 2000, 2500, 55);
            AddMonster("GoblinShaman_Hildir", 1300, 2000, 53);
            AddMonster("GoblinBruteBros_nochest", 2000, 2500, 55);
            AddMonster("GoblinShaman_Hildir_nochest", 1300, 2000, 53);

            // ============================================
            // 보스 - WackyMMO 기준
            // ============================================
            AddMonster("Eikthyr", 150, 200, 15);
            AddMonster("gd_king", 300, 500, 26);
            AddMonster("Bonemass", 700, 900, 36);
            AddMonster("Dragon", 1000, 1400, 46);
            AddMonster("GoblinKing", 1500, 3000, 56);
            AddMonster("SeekerQueen", 3200, 4500, 66);
            AddMonster("Fader", 4700, 6400, 76);

            Plugin.Log.LogDebug($"[CaptainMonsterExp] WackyMMO 기본 데이터 {dictionary.Count}종 로드됨");
        }

        /// <summary>
        /// 몬스터 경험치 획득 (랜덤 min~max)
        /// </summary>
        public static int GetExp(string name)
        {
            if (!isInitialized) Initialize();

            if (dictionary.TryGetValue(name, out var monster))
            {
                return UnityEngine.Random.Range(monster.minExp, monster.maxExp + 1);
            }
            return 10;
        }

        public static int GetMaxExp(string name)
        {
            if (!isInitialized) Initialize();
            if (dictionary.TryGetValue(name, out var monster))
                return monster.maxExp;
            return 10;
        }

        public static int GetMinExp(string name)
        {
            if (!isInitialized) Initialize();
            if (dictionary.TryGetValue(name, out var monster))
                return monster.minExp;
            return 5;
        }

        /// <summary>
        /// 몬스터 레벨 획득 (WackyMMO 방식)
        /// </summary>
        public static int GetLevel(string name)
        {
            if (!isInitialized) Initialize();
            if (dictionary.TryGetValue(name, out var monster))
                return monster.level;
            return 0; // 0이면 ???로 표시
        }

        /// <summary>
        /// 몬스터가 등록되어 있는지 확인
        /// </summary>
        public static bool HasMonster(string name)
        {
            if (!isInitialized) Initialize();
            return dictionary.ContainsKey(name);
        }

        /// <summary>
        /// 커스텀 몬스터 추가
        /// </summary>
        public static void AddCustomMonster(string name, int minExp, int maxExp, int level)
        {
            if (!isInitialized) Initialize();
            AddMonster(name, minExp, maxExp, level);
            Plugin.Log.LogDebug($"[CaptainMonsterExp] 커스텀 몬스터 추가: {name} (Exp: {minExp}-{maxExp}, Lv: {level})");
        }

        public static void Reload()
        {
            isInitialized = false;
            Initialize();
        }

        public static int GetMonsterCount()
        {
            if (!isInitialized) Initialize();
            return dictionary.Count;
        }
    }
}

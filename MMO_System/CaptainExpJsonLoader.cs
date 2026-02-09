using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// JSON 파일로 경험치 데이터 관리
    /// 파일 경로: BepInEx/config/CaptainSkillTree/
    /// </summary>
    public static class CaptainExpJsonLoader
    {
        private static string ConfigFolder => Path.Combine(BepInEx.Paths.ConfigPath, "CaptainSkillTree");
        private static string MonsterExpFile => Path.Combine(ConfigFolder, "MonsterExp.json");
        private static string LevelExpFile => Path.Combine(ConfigFolder, "LevelExp.json");

        #region === Monster Exp JSON ===

        /// <summary>
        /// 몬스터 경험치 JSON 데이터 구조
        /// </summary>
        [Serializable]
        public class MonsterExpData
        {
            public List<MonsterEntry> monsters = new List<MonsterEntry>();
        }

        [Serializable]
        public class MonsterEntry
        {
            public string name;
            public int minExp;
            public int maxExp;
            public int level;
        }

        /// <summary>
        /// 몬스터 경험치 JSON 로드
        /// Unity JsonUtility 대신 수동 파싱 (안정성 향상)
        /// </summary>
        /// <returns>성공 여부</returns>
        public static bool LoadMonsterExp()
        {
            try
            {
                if (!File.Exists(MonsterExpFile))
                {
                    Plugin.Log.LogDebug($"[ExpJsonLoader] MonsterExp.json 없음 - 기본값 사용");
                    return false;
                }

                string json = File.ReadAllText(MonsterExpFile);

                // 파일이 비어있거나 너무 짧으면 재생성
                if (string.IsNullOrWhiteSpace(json) || json.Length < 50)
                {
                    Plugin.Log.LogWarning("[ExpJsonLoader] MonsterExp.json이 비어있음 - 재생성");
                    File.Delete(MonsterExpFile);
                    CreateDefaultMonsterExpJson();
                    json = File.ReadAllText(MonsterExpFile);
                }

                // 수동 파싱
                int loadedCount = 0;
                var lines = json.Split('\n');

                string currentName = null;
                int currentMinExp = 0;
                int currentMaxExp = 0;
                int currentLevel = 1;

                foreach (var line in lines)
                {
                    var trimmed = line.Trim();

                    if (trimmed.Contains("\"name\""))
                    {
                        var start = trimmed.IndexOf(":") + 1;
                        var value = trimmed.Substring(start).Trim().Trim(',').Trim('"');
                        currentName = value;
                    }
                    else if (trimmed.Contains("\"minExp\""))
                    {
                        var start = trimmed.IndexOf(":") + 1;
                        var value = trimmed.Substring(start).Trim().Trim(',');
                        int.TryParse(value, out currentMinExp);
                    }
                    else if (trimmed.Contains("\"maxExp\""))
                    {
                        var start = trimmed.IndexOf(":") + 1;
                        var value = trimmed.Substring(start).Trim().Trim(',');
                        int.TryParse(value, out currentMaxExp);
                    }
                    else if (trimmed.Contains("\"level\""))
                    {
                        var start = trimmed.IndexOf(":") + 1;
                        var value = trimmed.Substring(start).Trim().Trim(',');
                        int.TryParse(value, out currentLevel);

                        // level이 마지막 필드이므로 여기서 몬스터 추가
                        if (!string.IsNullOrEmpty(currentName))
                        {
                            CaptainMonsterExp.AddCustomMonster(currentName, currentMinExp, currentMaxExp, currentLevel);
                            loadedCount++;
                            currentName = null;
                        }
                    }
                }

                if (loadedCount > 0)
                {
                    Plugin.Log.LogDebug($"[ExpJsonLoader] MonsterExp.json 로드 완료 ({loadedCount}개)");
                    return true;
                }
                else
                {
                    Plugin.Log.LogWarning("[ExpJsonLoader] MonsterExp.json 파싱 실패 - 기본값 사용");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ExpJsonLoader] MonsterExp.json 로드 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 기본 몬스터 경험치 JSON 파일 생성
        /// Unity JsonUtility 대신 수동 JSON 생성 (안정성 향상)
        /// </summary>
        public static void CreateDefaultMonsterExpJson()
        {
            try
            {
                EnsureConfigFolder();

                // 몬스터 데이터 정의
                var monsters = new List<(string name, int minExp, int maxExp, int level)>
                {
                    // === 초원 (Meadows) ===
                    ("$enemy_boar", 10, 15, 1),
                    ("$enemy_neck", 15, 20, 1),
                    ("$enemy_greyling", 20, 30, 2),

                    // === 검은숲 (Black Forest) ===
                    ("$enemy_greydwarf", 25, 35, 3),
                    ("$enemy_greydwarf_shaman", 40, 50, 4),
                    ("$enemy_greydwarf_elite", 50, 65, 5),
                    ("$enemy_troll", 100, 150, 8),
                    ("$enemy_skeleton", 30, 40, 4),
                    ("$enemy_ghost", 35, 45, 5),

                    // === 늪지대 (Swamp) ===
                    ("$enemy_draugr", 45, 60, 6),
                    ("$enemy_draugr_ranged", 50, 65, 6),
                    ("$enemy_draugr_elite", 70, 90, 8),
                    ("$enemy_blob", 40, 55, 5),
                    ("$enemy_oozer", 55, 70, 6),
                    ("$enemy_wraith", 60, 80, 7),
                    ("$enemy_leech", 35, 45, 5),
                    ("$enemy_surtling", 45, 60, 6),
                    ("$enemy_abomination", 120, 160, 10),

                    // === 산악 (Mountain) ===
                    ("$enemy_wolf", 50, 65, 6),
                    ("$enemy_fenring", 80, 100, 9),
                    ("$enemy_drake", 70, 90, 8),
                    ("$enemy_stonegolem", 120, 160, 10),
                    ("$enemy_bat", 40, 50, 6),
                    ("$enemy_ulv", 90, 110, 10),
                    ("$enemy_fenring_cultist", 100, 130, 11),

                    // === 평원 (Plains) ===
                    ("$enemy_lox", 100, 130, 10),
                    ("$enemy_fuling", 80, 100, 9),
                    ("$enemy_fuling_berserker", 150, 200, 12),
                    ("$enemy_fuling_shaman", 100, 130, 10),
                    ("$enemy_deathsquito", 40, 55, 7),
                    ("$enemy_growth", 90, 120, 9),
                    ("$enemy_goblinbrute", 140, 180, 12),

                    // === 안개 (Mistlands) ===
                    ("$enemy_seeker", 100, 140, 11),
                    ("$enemy_seeker_brood", 60, 80, 9),
                    ("$enemy_seeker_brute", 180, 250, 14),
                    ("$enemy_gjall", 200, 280, 15),
                    ("$enemy_tick", 60, 80, 8),
                    ("$enemy_dverger", 110, 150, 12),
                    ("$enemy_dverger_mage", 130, 170, 13),
                    ("$enemy_dverger_mage_fire", 140, 180, 14),
                    ("$enemy_dverger_mage_ice", 140, 180, 14),
                    ("$enemy_dverger_mage_support", 120, 160, 13),

                    // === 재의 땅 (Ashlands) ===
                    ("$enemy_charredmelee", 160, 220, 16),
                    ("$enemy_charredranged", 150, 200, 15),
                    ("$enemy_charredmage", 170, 230, 17),
                    ("$enemy_charredtwitcher", 140, 190, 15),
                    ("$enemy_morgen", 250, 350, 20),
                    ("$enemy_asksvin", 180, 250, 18),
                    ("$enemy_volture", 200, 280, 19),
                    ("$enemy_fallenvalkyrie", 300, 400, 22),

                    // === 바다 (Ocean) ===
                    ("$enemy_serpent", 150, 200, 12),
                    ("$enemy_leviathan", 300, 400, 20),

                    // === 보스 (Bosses) ===
                    ("$enemy_eikthyr", 500, 600, 15),
                    ("$enemy_gdking", 1000, 1200, 25),
                    ("$enemy_bonemass", 1500, 1800, 35),
                    ("$enemy_dragon", 2000, 2400, 45),
                    ("$enemy_goblinking", 2500, 3000, 55),
                    ("$enemy_dvergking", 3000, 3600, 65),
                    ("$enemy_dvergqueen", 4000, 4800, 75),
                    ("$enemy_fader", 5000, 6000, 85),
                };

                // 수동 JSON 생성
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("{");
                sb.AppendLine("  \"monsters\": [");

                for (int i = 0; i < monsters.Count; i++)
                {
                    var m = monsters[i];
                    sb.AppendLine("    {");
                    sb.AppendLine($"      \"name\": \"{m.name}\",");
                    sb.AppendLine($"      \"minExp\": {m.minExp},");
                    sb.AppendLine($"      \"maxExp\": {m.maxExp},");
                    sb.AppendLine($"      \"level\": {m.level}");
                    sb.Append("    }");
                    if (i < monsters.Count - 1)
                        sb.AppendLine(",");
                    else
                        sb.AppendLine();
                }

                sb.AppendLine("  ]");
                sb.AppendLine("}");

                File.WriteAllText(MonsterExpFile, sb.ToString());
                Plugin.Log.LogDebug($"[ExpJsonLoader] MonsterExp.json 기본 파일 생성: {MonsterExpFile} ({monsters.Count}개 몬스터)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ExpJsonLoader] MonsterExp.json 생성 실패: {ex.Message}");
            }
        }

        #endregion

        #region === Level Exp JSON ===

        /// <summary>
        /// 레벨 경험치 JSON 데이터 구조
        /// </summary>
        [Serializable]
        public class LevelExpData
        {
            public int baseExp = 300;
            public float multiplier = 1.05f;
            public int maxLevel = 100;
            public bool useAccumulative = true;
            public List<LevelExpEntry> customLevels = new List<LevelExpEntry>();
        }

        [Serializable]
        public class LevelExpEntry
        {
            public int level;
            public long expRequired;
        }

        /// <summary>
        /// 레벨 경험치 JSON 로드
        /// </summary>
        /// <returns>로드된 데이터 (null이면 기본값 사용)</returns>
        public static LevelExpData LoadLevelExp()
        {
            try
            {
                if (!File.Exists(LevelExpFile))
                {
                    Plugin.Log.LogDebug($"[ExpJsonLoader] LevelExp.json 없음 - Config 값 사용");
                    return null;
                }

                string json = File.ReadAllText(LevelExpFile);
                var data = JsonUtility.FromJson<LevelExpData>(json);

                if (data == null)
                {
                    Plugin.Log.LogWarning("[ExpJsonLoader] LevelExp.json 파싱 실패 - Config 값 사용");
                    return null;
                }

                Plugin.Log.LogDebug($"[ExpJsonLoader] LevelExp.json 로드 완료 (BaseExp: {data.baseExp}, Multi: {data.multiplier}, Max: {data.maxLevel})");
                return data;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ExpJsonLoader] LevelExp.json 로드 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 기본 레벨 경험치 JSON 파일 생성
        /// Unity JsonUtility 대신 수동 JSON 생성 (안정성 향상)
        /// </summary>
        public static void CreateDefaultLevelExpJson()
        {
            try
            {
                EnsureConfigFolder();

                // 수동 JSON 생성 (customLevels 예시 포함)
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("{");
                sb.AppendLine("  \"_comment\": \"customLevels에 값을 넣으면 공식 대신 직접 지정한 경험치 사용\",");
                sb.AppendLine("  \"baseExp\": 300,");
                sb.AppendLine("  \"multiplier\": 1.05,");
                sb.AppendLine("  \"maxLevel\": 100,");
                sb.AppendLine("  \"useAccumulative\": true,");
                sb.AppendLine("  \"customLevels\": []");
                sb.AppendLine("}");

                File.WriteAllText(LevelExpFile, sb.ToString());
                Plugin.Log.LogDebug($"[ExpJsonLoader] LevelExp.json 기본 파일 생성: {LevelExpFile}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ExpJsonLoader] LevelExp.json 생성 실패: {ex.Message}");
            }
        }

        #endregion

        #region === Utility ===

        /// <summary>
        /// Config 폴더 생성
        /// </summary>
        private static void EnsureConfigFolder()
        {
            try
            {
                Plugin.Log.LogDebug($"[ExpJsonLoader] Config 폴더 확인: {ConfigFolder}");

                if (!Directory.Exists(ConfigFolder))
                {
                    Directory.CreateDirectory(ConfigFolder);
                    Plugin.Log.LogDebug($"[ExpJsonLoader] Config 폴더 생성됨: {ConfigFolder}");
                }
                else
                {
                    Plugin.Log.LogDebug($"[ExpJsonLoader] Config 폴더 이미 존재: {ConfigFolder}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[ExpJsonLoader] Config 폴더 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// JSON 파일 존재 여부 확인
        /// </summary>
        public static bool HasMonsterExpJson() => File.Exists(MonsterExpFile);
        public static bool HasLevelExpJson() => File.Exists(LevelExpFile);

        /// <summary>
        /// 기본 JSON 파일 모두 생성
        /// </summary>
        public static void CreateAllDefaultJsonFiles()
        {
            CreateDefaultMonsterExpJson();
            CreateDefaultLevelExpJson();
        }

        /// <summary>
        /// JSON 파일 경로 반환
        /// </summary>
        public static string GetConfigFolderPath() => ConfigFolder;

        #endregion
    }
}

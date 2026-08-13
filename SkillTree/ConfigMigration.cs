using BepInEx.Configuration;
using System.IO;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 컨피그 스키마 버전 관리 및 선택적 마이그레이션 시스템.
    /// - 신규 항목: BepInEx Bind() 시 자동으로 기본값 적용 (별도 처리 불필요)
    /// - 기존 항목: 사용자가 변경한 값 유지
    /// - 밸런스 변경 항목 (_balanceChangedKeys): 기본값으로 강제 업데이트
    ///
    /// SCHEMA_VERSION을 올리면 _balanceChangedKeys에 등록된 항목만 기본값으로 초기화.
    /// 전체 초기화가 필요한 경우 ResetAllToDefaults()를 명시적으로 호출.
    /// </summary>
    public static class ConfigMigration
    {
        /// <summary>
        /// 형식: "YYYY.MM.patch" — 이 값을 올리면 ApplyMigration()이 실행됨.
        /// ⚠️ YYYY.MM은 반드시 이 값을 실제로 올리는 시점(오늘 날짜)을 반영해야 함.
        /// 과거 변경 사유를 적더라도 라벨은 항상 "지금 배포되는 날짜" 기준으로 갱신할 것.
        /// </summary>
        public const string SCHEMA_VERSION = "2026.08.2";

        // 절대 변경 금지 키 (언어 설정, 스키마 버전 보존)
        private static readonly System.Collections.Generic.HashSet<string> _preserveKeys =
            new System.Collections.Generic.HashSet<string>
            {
                "Language",
                "Config_Schema_Version"
            };

        /// <summary>
        /// 이번 스키마 버전에서 밸런스가 변경된 Config 키 목록.
        /// 여기 등록된 키만 사용자 값을 무시하고 기본값으로 덮어씁니다.
        ///
        /// 사용법:
        ///   - SCHEMA_VERSION을 올릴 때 변경된 키를 여기에 추가
        ///   - 다음 버전 올릴 때 이전 항목 제거 후 새 항목 추가
        ///
        /// 예시:
        ///   "SwordExpertDamage",
        ///   "BowStep3CritBonus",
        /// </summary>
        private static readonly System.Collections.Generic.HashSet<string> _balanceChangedKeys =
            new System.Collections.Generic.HashSet<string>
            {
                // v2026.08.2: 목초지/검은숲/늪 특수(히든) 보스 퀘스트에 코인 보상이 새로 추가됨(0 → 500/800/1200).
                // 이미 0으로 저장되어 있던 기존 사용자 .cfg 값을 새 기본값으로 강제 리셋.
                "Meadows_Quest6_CoinMin",
                "Meadows_Quest6_CoinMax",
                "BlackForest_Quest7_CoinMin",
                "BlackForest_Quest7_CoinMax",
                "Swamp_Quest7_CoinMin",
                "Swamp_Quest7_CoinMax",
            };

        /// <summary>
        /// INI 파일에서 저장된 스키마 버전을 직접 읽는다 (Bind() 호출 전).
        /// 파일 없음 또는 키 없음 → "" 반환 → 마이그레이션 대상
        /// </summary>
        public static string ReadStoredVersion(ConfigFile config)
        {
            try
            {
                string configPath = config.ConfigFilePath;
                if (!File.Exists(configPath))
                    return "";

                bool inBase = false;
                foreach (string raw in File.ReadAllLines(configPath))
                {
                    string line = raw.Trim();
                    if (line == "[Skill_Tree_Base]") { inBase = true; continue; }
                    if (inBase && line.StartsWith("[")) break;
                    if (inBase && line.StartsWith("Config_Schema_Version ="))
                        return line.Substring("Config_Schema_Version =".Length).Trim();
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[ConfigMigration] 버전 읽기 실패: {ex.Message}");
            }
            return "";
        }

        /// <summary>
        /// 선택적 마이그레이션:
        ///   - 신규 항목: Bind() 시 이미 기본값 적용됨 (처리 불필요)
        ///   - 기존 항목: 사용자 값 유지
        ///   - _balanceChangedKeys 항목: 기본값으로 강제 업데이트
        /// Initialize() 끝에서 호출해야 한다 (모든 Bind() 완료 후).
        /// </summary>
        public static void ApplyMigration(ConfigFile config)
        {
            foreach (var pair in config)
            {
                if (_preserveKeys.Contains(pair.Key.Key))
                    continue;
                if (_balanceChangedKeys.Contains(pair.Key.Key))
                {
                    pair.Value.BoxedValue = pair.Value.DefaultValue;
                }
            }

            config.Save();
        }

        /// <summary>
        /// 전체 강제 초기화 (구버전 호환 또는 긴급 리셋용).
        /// Language 및 Config_Schema_Version 키는 보존.
        /// 일반 업데이트에는 ApplyMigration() 사용 권장.
        /// </summary>
        public static void ResetAllToDefaults(ConfigFile config)
        {
            int resetCount = 0;
            foreach (var pair in config)
            {
                if (_preserveKeys.Contains(pair.Key.Key))
                    continue;
                pair.Value.BoxedValue = pair.Value.DefaultValue;
                resetCount++;
            }
            config.Save();
            Plugin.Log.LogWarning($"[ConfigMigration] [WARN] 전체 초기화 완료: {resetCount}개 항목 → 기본값으로 리셋.");
        }
    }
}

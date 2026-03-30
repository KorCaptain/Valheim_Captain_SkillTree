using BepInEx.Configuration;
using System.IO;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 컨피그 스키마 버전 관리 및 강제 초기화 시스템.
    /// SCHEMA_VERSION을 올리면 다음 번 DLL 로드 시 모든 사용자의
    /// 컨피그가 새 기본값으로 강제 초기화된다.
    /// </summary>
    public static class ConfigMigration
    {
        /// <summary>
        /// 이 값을 올리면 기존 사용자 컨피그를 기본값으로 강제 초기화.
        /// 형식: "YYYY.MM.patch" (예: 2026.03.1)
        /// </summary>
        public const string SCHEMA_VERSION = "2026.03.1";

        // Language, Schema_Version 키는 리셋 대상에서 제외 (사용자 언어 설정 보존)
        private static readonly System.Collections.Generic.HashSet<string> _preserveKeys =
            new System.Collections.Generic.HashSet<string>
            {
                "Language",
                "Config_Schema_Version"
            };

        /// <summary>
        /// INI 파일에서 저장된 스키마 버전을 직접 읽는다 (Bind() 호출 전).
        /// 파일 없음 또는 키 없음 → "" 반환 → 항상 초기화 대상
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
        /// 모든 컨피그 엔트리를 기본값으로 강제 초기화하고 파일에 저장한다.
        /// Language 및 Config_Schema_Version 키는 보존.
        /// Initialize() 끝에서 호출해야 한다 (모든 Bind() 완료 후).
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
            Plugin.Log.LogWarning($"[ConfigMigration] ✅ 컨피그 초기화 완료: {resetCount}개 항목 → 기본값으로 리셋.");
        }
    }
}

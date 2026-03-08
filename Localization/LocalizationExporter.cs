using System.IO;
using System.Text;
using BepInEx;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// 영어 번역 템플릿(en.json)을 BepInEx/config/CaptainSkillTree/ 폴더에 내보냅니다.
    /// 커뮤니티 번역자가 이 파일을 기반으로 자국어 번역을 제출할 수 있습니다.
    /// </summary>
    public static class LocalizationExporter
    {
        public static void ExportEnJson()
        {
            var en = DefaultLanguages.GetEnglish();

            var sb = new StringBuilder("{\n");
            bool first = true;
            foreach (var kv in en)
            {
                if (!first) sb.Append(",\n");
                string val = kv.Value
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n");
                sb.Append($"  \"{kv.Key}\": \"{val}\"");
                first = false;
            }
            sb.Append("\n}");

            string dir = Path.Combine(Paths.ConfigPath, "CaptainSkillTree", "Translation");
            Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, "en.json");
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            Plugin.Log.LogInfo($"[LocalizationExporter] en.json 내보내기 완료: {path} ({en.Count}개 키)");
        }
    }
}

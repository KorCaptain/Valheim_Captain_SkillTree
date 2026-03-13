using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// Default language data for Korean and English
    /// These are used when JSON files don't exist or fail to load
    /// </summary>
    public static partial class DefaultLanguages
    {
        /// <summary>
        /// Get Korean translations (default language)
        /// </summary>
        public static Dictionary<string, string> GetKorean()
        {
            var d = new Dictionary<string, string>();
            foreach (var kv in GetKorean_GameMessages())    d[kv.Key] = kv.Value;
            foreach (var kv in GetKorean_WeaponSkills())    d[kv.Key] = kv.Value;
            foreach (var kv in GetKorean_JobExpert())       d[kv.Key] = kv.Value;
            foreach (var kv in GetKorean_AttackProduction()) d[kv.Key] = kv.Value;
            foreach (var kv in GetKorean_ItemEffects())      d[kv.Key] = kv.Value;
            return d;
        }

        /// <summary>
        /// Get English translations
        /// </summary>
        public static Dictionary<string, string> GetEnglish()
        {
            var d = new Dictionary<string, string>();
            foreach (var kv in GetEnglish_GameMessages())    d[kv.Key] = kv.Value;
            foreach (var kv in GetEnglish_WeaponSkills())    d[kv.Key] = kv.Value;
            foreach (var kv in GetEnglish_JobExpert())       d[kv.Key] = kv.Value;
            foreach (var kv in GetEnglish_AttackProduction()) d[kv.Key] = kv.Value;
            foreach (var kv in GetEnglish_ItemEffects())     d[kv.Key] = kv.Value;
            return d;
        }
    }
}

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// Shorthand helper for localization
    /// Usage: L.Get("key") or L.Get("key", arg1, arg2)
    /// </summary>
    public static class L
    {
        /// <summary>
        /// Get localized string by key with optional format arguments
        /// </summary>
        public static string Get(string key, params object[] args)
            => LocalizationManager.Get(key, args);

        /// <summary>
        /// Check if key exists
        /// </summary>
        public static bool Has(string key)
            => LocalizationManager.HasKey(key);

        /// <summary>
        /// Get current language code (ko, en, etc.)
        /// </summary>
        public static string Lang => LocalizationManager.GetCurrentLanguage();
    }
}

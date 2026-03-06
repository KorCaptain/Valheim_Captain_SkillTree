using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Configuration;
using UnityEngine;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// CaptainSkillTree Localization Manager
    /// Manages multi-language support for in-game text
    /// Language files are auto-detected from config folder (ko.json, en.json, eu.json, etc.)
    /// </summary>
    public static class LocalizationManager
    {
        private static Dictionary<string, Dictionary<string, string>> _translations = new Dictionary<string, Dictionary<string, string>>();
        private static string _currentLanguage = "ko";
        private static bool _initialized = false;
        private static readonly HashSet<string> _warnedMissingKeys = new HashSet<string>();

        // Config entry for language selection (references SkillTreeConfig)
        public static ConfigEntry<string> LanguageConfig => SkillTree.SkillTreeConfig.Language;

        // ===== 언어 변경 이벤트 =====
        /// <summary>
        /// 언어가 변경될 때 발생하는 이벤트 (UI 갱신용)
        /// </summary>
        public static event Action OnLanguageChanged;

        // Dynamically detected supported languages (uppercase codes: KR, EN, EU, etc.)
        private static List<string> _supportedLanguages = new List<string> { "KR", "EN" };

        // Default language (uppercase)
        public const string DefaultLanguage = "KR";

        // Language code mapping (uppercase display -> lowercase file)
        // Not readonly - allows dynamic additions for new language files
        private static Dictionary<string, string> LanguageCodeMap = new Dictionary<string, string>
        {
            { "KR", "ko" },
            { "EN", "en" },
            { "JP", "ja" },
            { "CN", "zh" },
            { "EU", "eu" },
            { "DE", "de" },
            { "FR", "fr" },
            { "ES", "es" },
            { "RU", "ru" },
            { "PT", "pt" }
        };

        /// <summary>
        /// Get supported languages (for config dropdown)
        /// </summary>
        public static string[] SupportedLanguages => _supportedLanguages.ToArray();

        /// <summary>
        /// Initialize the localization system
        /// </summary>
        public static void Initialize(ConfigFile config)
        {
            if (_initialized) return;

            try
            {
                // First, detect available language files to build supported languages list
                DetectAvailableLanguages();

                // LanguageConfig는 이미 SkillTreeConfig에서 초기화됨
                // Config 변경 이벤트만 등록
                SkillTree.SkillTreeConfig.Language.SettingChanged += (sender, args) =>
                {
                    Plugin.Log.LogDebug($"[Localization] Config 변경 감지: '{SkillTree.SkillTreeConfig.Language.Value}'");
                    ReloadLanguage();
                    OnLanguageChanged?.Invoke();  // UI 갱신 트리거
                };

                // Load language files
                LoadLanguageFiles();

                // Detect or set language
                var configValue = LanguageConfig.Value.Trim();
                Plugin.Log.LogDebug($"[Localization] Config value: '{configValue}'");

                if (configValue.Equals("Auto", StringComparison.OrdinalIgnoreCase))
                {
                    Plugin.Log.LogDebug("[Localization] Auto mode enabled - detecting Valheim language...");
                    DetectLanguage();
                }
                else
                {
                    SetLanguage(configValue);
                }

                _initialized = true;

                // 현재 언어 번역 개수 확인
                if (!_translations.ContainsKey(_currentLanguage))
                {
                    Plugin.Log.LogError($"[Localization] ✗ CRITICAL: Current language '{_currentLanguage}' NOT LOADED!");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Localization] Initialization failed: {ex.Message}");
                // Fallback to Korean
                _currentLanguage = "ko";
                LoadDefaultTranslations();
                _initialized = true;
            }
        }

        /// <summary>
        /// Detect available language files in config folder
        /// Adds new language options dynamically (e.g., eu.json -> EU option)
        /// </summary>
        private static void DetectAvailableLanguages()
        {
            var configPath = Path.Combine(BepInEx.Paths.ConfigPath, "CaptainSkillTree");

            // Start with default languages (KR, EN always available)
            // CN, JP, RU, EU are also available by default for major international support
            _supportedLanguages = new List<string> { "KR", "EN", "CN", "JP", "RU", "EU" };

            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
                return;
            }

            // Scan for .json language files
            var jsonFiles = Directory.GetFiles(configPath, "*.json");
            foreach (var file in jsonFiles)
            {
                var fileName = Path.GetFileNameWithoutExtension(file).ToLower();

                // Find matching uppercase code (use ToList() to avoid collection modification during enumeration)
                string upperCode = null;
                foreach (var kvp in LanguageCodeMap.ToList())
                {
                    if (kvp.Value == fileName)
                    {
                        upperCode = kvp.Key;
                        break;
                    }
                }

                // LanguageCodeMap에 없는 파일(MonsterExp.json, LevelExp.json 등)은 무시
                if (upperCode == null) continue;

                // Add to supported languages if not already present
                if (!_supportedLanguages.Contains(upperCode))
                {
                    _supportedLanguages.Add(upperCode);
                    Plugin.Log.LogDebug($"[Localization] Detected language file: {fileName}.json -> {upperCode}");
                }
            }

            // Sort languages (KR first, EN second, major languages, then others alphabetically)
            _supportedLanguages = _supportedLanguages
                .OrderBy(l => l == "KR" ? 0 :
                             l == "EN" ? 1 :
                             l == "CN" ? 2 :
                             l == "JP" ? 3 :
                             l == "RU" ? 4 :
                             l == "EU" ? 5 : 6)
                .ThenBy(l => l)
                .ToList();
        }

        /// <summary>
        /// Get display language code (lowercase -> uppercase)
        /// </summary>
        private static string GetDisplayLanguage(string langCode)
        {
            foreach (var kvp in LanguageCodeMap.ToList())
            {
                if (kvp.Value == langCode)
                    return kvp.Key;
            }
            return langCode.ToUpper();
        }

        /// <summary>
        /// Get file language code (uppercase -> lowercase)
        /// </summary>
        private static string GetFileLanguage(string displayCode)
        {
            if (LanguageCodeMap.TryGetValue(displayCode.ToUpper(), out var fileCode))
                return fileCode;
            return displayCode.ToLower();
        }

        /// <summary>
        /// Load language files from config folder
        /// Creates default files if they don't exist
        /// Automatically merges missing keys from DefaultLanguages
        /// </summary>
        private static void LoadLanguageFiles()
        {
            var configPath = Path.Combine(BepInEx.Paths.ConfigPath, "CaptainSkillTree");

            // Create directory if not exists
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            // Load each supported language (convert uppercase display code to lowercase file code)
            // Use ToList() to avoid collection modification during enumeration (CreateLanguageTemplate may add to _supportedLanguages)
            foreach (var displayLang in _supportedLanguages.ToList())
            {
                var fileLang = GetFileLanguage(displayLang);
                var filePath = Path.Combine(configPath, $"{fileLang}.json");

                // Get default translations for this language
                // For ko/en use their specific defaults, for others use English as base
                Dictionary<string, string> defaultData = null;
                if (fileLang == "ko")
                    defaultData = DefaultLanguages.GetKorean();
                else
                    defaultData = DefaultLanguages.GetEnglish(); // All non-Korean languages use English as base

                // 파일이 없으면: 임베디드 리소스 시도 → DefaultLanguages 폴백
                if (!File.Exists(filePath))
                {
                    var embeddedData = LoadFromEmbeddedResource(fileLang);
                    if (embeddedData != null)
                    {
                        foreach (var kvp in (defaultData ?? new Dictionary<string, string>()))
                            if (!embeddedData.ContainsKey(kvp.Key))
                                embeddedData[kvp.Key] = kvp.Value;
                        _translations[fileLang] = embeddedData;
                        Plugin.Log.LogDebug($"[Localization] {fileLang}: 임베디드 리소스 로드 ({embeddedData.Count} entries)");
                    }
                    else
                    {
                        _translations[fileLang] = defaultData ?? new Dictionary<string, string>();
                        Plugin.Log.LogDebug($"[Localization] {fileLang}: 파일 없음 - DefaultLanguages 메모리 사용 ({_translations[fileLang].Count} entries)");
                    }
                    continue;
                }

                // Load the file
                try
                {
                    var json = File.ReadAllText(filePath);
                    var langData = ParseJsonToDict(json);

                    // Merge/update keys from DefaultLanguages (for ko and en only)
                    // DefaultLanguages is authoritative: adds missing keys AND updates changed format strings
                    if (defaultData != null)
                    {
                        int addedKeys = 0;
                        int updatedKeys = 0;
                        foreach (var kvp in defaultData)
                        {
                            if (!langData.ContainsKey(kvp.Key))
                            {
                                langData[kvp.Key] = kvp.Value;
                                addedKeys++;
                            }
                            else if (langData[kvp.Key] != kvp.Value)
                            {
                                // Update stale format strings from DefaultLanguages
                                // This prevents FormatException when format placeholders change between versions
                                langData[kvp.Key] = kvp.Value;
                                updatedKeys++;
                            }
                        }

                        // 파일에 쓰지 않음 - 메모리에서만 키 머지 사용
                        if (addedKeys > 0 || updatedKeys > 0)
                        {
                            Plugin.Log.LogDebug($"[Localization] {fileLang}.json 메모리 업데이트: {addedKeys} new, {updatedKeys} refreshed keys");
                        }
                    }

                    _translations[fileLang] = langData;
                    Plugin.Log.LogDebug($"[Localization] ✓ Loaded {fileLang}.json ({langData.Count} entries)");

                    // 첫 5개 키 샘플 출력 (디버깅용)
                    var sampleKeys = langData.Keys.Take(5).ToArray();
                    Plugin.Log.LogDebug($"[Localization]   Sample keys: {string.Join(", ", sampleKeys)}");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[Localization] Failed to load {fileLang}.json: {ex.Message}");
                    // Use default translations as fallback
                    _translations[fileLang] = defaultData ?? new Dictionary<string, string>();
                }
            }
        }

        /// <summary>
        /// Load default translations (fallback when files fail)
        /// </summary>
        private static void LoadDefaultTranslations()
        {
            _translations["ko"] = DefaultLanguages.GetKorean();
            _translations["en"] = DefaultLanguages.GetEnglish();
        }

        /// <summary>
        /// Create default language file
        /// </summary>
        private static void CreateDefaultLanguageFile(string lang, string filePath)
        {
            try
            {
                var defaultData = lang == "ko" ? DefaultLanguages.GetKorean() : DefaultLanguages.GetEnglish();
                var json = DictToJson(defaultData);
                File.WriteAllText(filePath, json);
                Plugin.Log.LogDebug($"[Localization] Created default {lang}.json");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Localization] Failed to create {lang}.json: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new language template file based on English
        /// Call this to generate eu.json, de.json, fr.json etc. with English as base
        /// </summary>
        /// <param name="langCode">Language code (eu, de, fr, es, etc.)</param>
        /// <returns>True if created successfully</returns>
        public static bool CreateLanguageTemplate(string langCode)
        {
            try
            {
                var configPath = Path.Combine(BepInEx.Paths.ConfigPath, "CaptainSkillTree");
                if (!Directory.Exists(configPath))
                    Directory.CreateDirectory(configPath);

                var filePath = Path.Combine(configPath, $"{langCode.ToLower()}.json");

                if (File.Exists(filePath))
                {
                    Plugin.Log.LogDebug($"[Localization] {langCode}.json already exists");
                    return false;
                }

                // Use English as base template
                var templateData = DefaultLanguages.GetEnglish();
                var json = DictToJson(templateData);
                File.WriteAllText(filePath, json);

                // Add to supported languages
                var upperCode = langCode.ToUpper();
                if (!_supportedLanguages.Contains(upperCode))
                {
                    _supportedLanguages.Add(upperCode);
                    if (!LanguageCodeMap.ContainsKey(upperCode))
                        LanguageCodeMap[upperCode] = langCode.ToLower();
                }

                Plugin.Log.LogDebug($"[Localization] Created {langCode}.json template (based on English)");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Localization] Failed to create {langCode}.json: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Get config folder path for language files
        /// </summary>
        public static string GetLanguageFilesPath()
        {
            return Path.Combine(BepInEx.Paths.ConfigPath, "CaptainSkillTree");
        }

        /// <summary>
        /// Detect language from Valheim settings
        /// </summary>
        private static void DetectLanguage()
        {
            try
            {
                Plugin.Log.LogDebug("========================================");
                Plugin.Log.LogDebug("[Localization] DetectLanguage() 시작");

                // 기본값을 Korean으로 변경 (사용자 요청)
                string valheimLang = null;
                bool detected = false;

                // 방법 1: PlayerPrefs에서 언어 설정 읽기 (가장 신뢰할 수 있음)
                try
                {
                    string playerPrefsLang = PlayerPrefs.GetString("language", "");
                    Plugin.Log.LogDebug($"[Localization]   PlayerPrefs 'language': '{playerPrefsLang}'");

                    if (!string.IsNullOrEmpty(playerPrefsLang))
                    {
                        valheimLang = playerPrefsLang;
                        detected = true;
                        Plugin.Log.LogDebug($"[Localization]   ✓ PlayerPrefs에서 언어 감지: '{valheimLang}'");
                    }
                }
                catch (Exception ppEx)
                {
                    Plugin.Log.LogDebug($"[Localization]   PlayerPrefs 읽기 실패: {ppEx.Message}");
                }

                // 방법 2: Valheim Localization.instance에서 언어 가져오기
                if (!detected)
                {
                    var localizationType = typeof(Player).Assembly.GetType("Localization");
                    Plugin.Log.LogDebug($"[Localization]   Localization 타입: {localizationType != null}");

                    if (localizationType != null)
                    {
                        // 2-1: Localization.instance.GetSelectedLanguage() 시도
                        var instanceProp = localizationType.GetProperty("instance",
                            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                        Plugin.Log.LogDebug($"[Localization]   instance 속성: {instanceProp != null}");

                        if (instanceProp != null)
                        {
                            var locInstance = instanceProp.GetValue(null);
                            Plugin.Log.LogDebug($"[Localization]   Localization.instance: {(locInstance != null ? "✓ 준비됨" : "✗ NULL (미준비)")}");

                            if (locInstance != null)
                            {
                                var getSelectedLanguageMethod = localizationType.GetMethod("GetSelectedLanguage");
                                if (getSelectedLanguageMethod != null)
                                {
                                    valheimLang = getSelectedLanguageMethod.Invoke(locInstance, null) as string;
                                    if (!string.IsNullOrEmpty(valheimLang))
                                    {
                                        detected = true;
                                        Plugin.Log.LogDebug($"[Localization]   ✓ GetSelectedLanguage() 반환: '{valheimLang}'");
                                    }
                                }

                                // 2-2: m_language 필드 직접 접근 시도
                                if (!detected)
                                {
                                    var langField = localizationType.GetField("m_language",
                                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                                    if (langField != null)
                                    {
                                        valheimLang = langField.GetValue(locInstance) as string;
                                        if (!string.IsNullOrEmpty(valheimLang))
                                        {
                                            detected = true;
                                            Plugin.Log.LogDebug($"[Localization]   ✓ m_language 필드: '{valheimLang}'");
                                        }
                                    }
                                }
                            }
                        }

                        // 2-3: 정적 프로퍼티 Localization.language 시도
                        if (!detected)
                        {
                            var langProp = localizationType.GetProperty("language",
                                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                            if (langProp != null)
                            {
                                valheimLang = langProp.GetValue(null) as string;
                                if (!string.IsNullOrEmpty(valheimLang))
                                {
                                    detected = true;
                                    Plugin.Log.LogDebug($"[Localization]   ✓ Localization.language: '{valheimLang}'");
                                }
                            }
                        }
                    }
                }

                // 감지 실패 시 기본값 Korean
                if (!detected || string.IsNullOrEmpty(valheimLang))
                {
                    valheimLang = "Korean";
                    Plugin.Log.LogWarning("[Localization]   ✗ 언어 감지 실패 - 기본값 Korean 사용");
                }

                Plugin.Log.LogDebug($"[Localization] Valheim 언어 감지됨: '{valheimLang}'");

                // Map Valheim language to our language codes (case-insensitive)
                var lowerLang = valheimLang.ToLower();

                if (lowerLang.Contains("korean") || lowerLang.Contains("한국어"))
                {
                    _currentLanguage = "ko";
                    Plugin.Log.LogDebug("[Localization] ✓ 자동 감지: 한국어 (KR)");
                }
                else if (lowerLang.Contains("japanese") || lowerLang.Contains("日本語") || lowerLang.Contains("japan"))
                {
                    _currentLanguage = _translations.ContainsKey("ja") ? "ja" : "en";
                    Plugin.Log.LogDebug($"[Localization] Auto-detect: Japanese -> {(_currentLanguage == "ja" ? "JP" : "EN (fallback)")}");
                }
                else if (lowerLang.Contains("chinese") || lowerLang.Contains("中文") || lowerLang.Contains("china"))
                {
                    _currentLanguage = _translations.ContainsKey("zh") ? "zh" : "en";
                    Plugin.Log.LogDebug($"[Localization] Auto-detect: Chinese -> {(_currentLanguage == "zh" ? "CN" : "EN (fallback)")}");
                }
                else if (lowerLang.Contains("german") || lowerLang.Contains("deutsch"))
                {
                    _currentLanguage = _translations.ContainsKey("de") ? "de" : "en";
                    Plugin.Log.LogDebug($"[Localization] Auto-detect: German -> {(_currentLanguage == "de" ? "DE" : "EN (fallback)")}");
                }
                else if (lowerLang.Contains("french") || lowerLang.Contains("français") || lowerLang.Contains("francais"))
                {
                    _currentLanguage = _translations.ContainsKey("fr") ? "fr" : "en";
                    Plugin.Log.LogDebug($"[Localization] Auto-detect: French -> {(_currentLanguage == "fr" ? "FR" : "EN (fallback)")}");
                }
                else if (lowerLang.Contains("spanish") || lowerLang.Contains("español") || lowerLang.Contains("espanol"))
                {
                    _currentLanguage = _translations.ContainsKey("es") ? "es" : "en";
                    Plugin.Log.LogDebug($"[Localization] Auto-detect: Spanish -> {(_currentLanguage == "es" ? "ES" : "EN (fallback)")}");
                }
                else if (lowerLang.Contains("russian") || lowerLang.Contains("русский"))
                {
                    _currentLanguage = _translations.ContainsKey("ru") ? "ru" : "en";
                    Plugin.Log.LogDebug($"[Localization] Auto-detect: Russian -> {(_currentLanguage == "ru" ? "RU" : "EN (fallback)")}");
                }
                else if (lowerLang.Contains("english"))
                {
                    _currentLanguage = "en";
                    Plugin.Log.LogDebug("[Localization] ✓ 자동 감지: 영어 (EN)");
                }
                else
                {
                    // 알 수 없는 언어는 한국어 기본 (사용자 요청)
                    _currentLanguage = "ko";
                    Plugin.Log.LogWarning($"[Localization] 알 수 없는 언어 '{valheimLang}', 한국어(KR)로 기본 설정");
                }

                Plugin.Log.LogDebug($"[Localization] 최종 언어: {GetDisplayLanguage(_currentLanguage)} ({_currentLanguage})");
                Plugin.Log.LogDebug("========================================");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Localization] DetectLanguage 실패: {ex.Message}");
                // 오류 시 한국어 기본
                _currentLanguage = "ko";
                Plugin.Log.LogDebug("[Localization] 오류 시 기본값: 한국어 (KR)");
            }
        }

        /// <summary>
        /// 언어 재로드 (Config 변경 시 호출)
        /// </summary>
        public static void ReloadLanguage()
        {
            Plugin.Log.LogDebug("[Localization] 언어 재로드 중...");

            var configValue = LanguageConfig.Value.Trim();
            if (configValue.Equals("Auto", StringComparison.OrdinalIgnoreCase))
            {
                Plugin.Log.LogDebug("[Localization] Auto 모드 - 발헤임 언어 감지");
                DetectLanguage();
            }
            else
            {
                Plugin.Log.LogDebug($"[Localization] 수동 모드 - '{configValue}'로 설정");
                SetLanguage(configValue);
            }

            Plugin.Log.LogDebug($"[Localization] ✓ 언어 재로드 완료: {GetDisplayLanguage(_currentLanguage)}");
        }

        /// <summary>
        /// Set current language
        /// Accepts both display codes (KR, EN) and file codes (ko, en)
        /// Case-insensitive: kr, KR, Kr 모두 인식
        /// </summary>
        public static void SetLanguage(string langCode)
        {
            if (string.IsNullOrWhiteSpace(langCode))
            {
                Plugin.Log.LogWarning("[Localization] Empty language code, using default (KR)");
                _currentLanguage = "ko";
                return;
            }

            var trimmedCode = langCode.Trim();
            Plugin.Log.LogDebug($"[Localization] SetLanguage called with: '{trimmedCode}'");

            // Convert to uppercase for comparison
            var upperCode = trimmedCode.ToUpper();

            // Check if it's a supported display code (KR, EN, JP, etc.)
            if (_supportedLanguages.Contains(upperCode))
            {
                _currentLanguage = GetFileLanguage(upperCode);
                Plugin.Log.LogDebug($"[Localization] ✓ Language set to: {upperCode} (file: {_currentLanguage})");
                return;
            }

            // Check if it's already a file code (ko, en, ja, etc.)
            var lowerCode = trimmedCode.ToLower();
            if (LanguageCodeMap.Values.Contains(lowerCode))
            {
                _currentLanguage = lowerCode;
                Plugin.Log.LogDebug($"[Localization] ✓ Language set to: {GetDisplayLanguage(lowerCode)} (file: {lowerCode})");
                return;
            }

            // Fallback to Korean (default)
            Plugin.Log.LogWarning($"[Localization] ✗ Unsupported language code: '{langCode}'");
            Plugin.Log.LogWarning($"[Localization] Supported codes: {string.Join(", ", _supportedLanguages)}");
            Plugin.Log.LogWarning($"[Localization] Using default: Korean (KR)");
            _currentLanguage = "ko";
        }

        /// <summary>
        /// Get current language code
        /// </summary>
        public static string GetCurrentLanguage() => _currentLanguage;

        /// <summary>
        /// Get localized string by key
        /// Fallback order: Current Language -> English -> Korean -> DefaultLanguages.Korean -> Key
        /// </summary>
        public static string Get(string key, params object[] args)
        {
            if (string.IsNullOrEmpty(key)) return key;

            // If not initialized yet, use default translations directly based on _currentLanguage
            if (!_initialized || _translations.Count == 0)
            {
                // Choose correct default language based on current setting
                var defaultDict = (_currentLanguage == "ko")
                    ? DefaultLanguages.GetKorean()
                    : DefaultLanguages.GetEnglish();

                if (defaultDict.TryGetValue(key, out var defaultText))
                {
                    return args.Length > 0 ? string.Format(defaultText, args) : defaultText;
                }

                // Fallback to the other language if key not found
                var fallbackDict = (_currentLanguage == "ko")
                    ? DefaultLanguages.GetEnglish()
                    : DefaultLanguages.GetKorean();

                if (fallbackDict.TryGetValue(key, out var earlyFallbackText))
                {
                    return args.Length > 0 ? string.Format(earlyFallbackText, args) : earlyFallbackText;
                }

                Plugin.Log.LogDebug($"[Localization] Key not found (not initialized): '{key}'");
                return key;
            }

            // Try current language
            if (_translations.TryGetValue(_currentLanguage, out var langDict))
            {
                if (langDict.TryGetValue(key, out var text))
                {
                    return args.Length > 0 ? string.Format(text, args) : text;
                }
            }

            // Fallback to English (universal fallback)
            if (_currentLanguage != "en" && _translations.TryGetValue("en", out var enDict))
            {
                if (enDict.TryGetValue(key, out var enText))
                {
                    Plugin.Log.LogDebug($"[Localization] Key '{key}' not found in {_currentLanguage}, using English fallback");
                    return args.Length > 0 ? string.Format(enText, args) : enText;
                }
            }

            // Fallback to Korean
            if (_currentLanguage != "ko" && _translations.TryGetValue("ko", out var koDict))
            {
                if (koDict.TryGetValue(key, out var koText))
                {
                    Plugin.Log.LogDebug($"[Localization] Key '{key}' not found in {_currentLanguage}, using Korean fallback");
                    return args.Length > 0 ? string.Format(koText, args) : koText;
                }
            }

            // Last resort: try DefaultLanguages directly
            var fallbackKo = DefaultLanguages.GetKorean();
            if (fallbackKo.TryGetValue(key, out var fallbackText))
            {
                Plugin.Log.LogDebug($"[Localization] Key '{key}' found in DefaultLanguages.Korean");
                return args.Length > 0 ? string.Format(fallbackText, args) : fallbackText;
            }

            // Return key as fallback
            if (_warnedMissingKeys.Add(key))
                Plugin.Log.LogWarning($"[Localization] ✗ Key not found in any language: '{key}'");
            return key;
        }

        /// <summary>
        /// Check if a key exists
        /// </summary>
        public static bool HasKey(string key)
        {
            if (_translations.TryGetValue(_currentLanguage, out var langDict))
            {
                return langDict.ContainsKey(key);
            }
            return false;
        }

        /// <summary>
        /// Reload language files (hot reload)
        /// </summary>
        public static void ReloadLanguageFiles()
        {
            _translations.Clear();
            LoadLanguageFiles();
            Plugin.Log.LogDebug("[Localization] Language files reloaded");
        }

        #region JSON Parsing (Simple implementation without external dependencies)

        /// <summary>
        /// Parse simple flat JSON to dictionary
        /// Format: { "key1": "value1", "key2": "value2" }
        /// </summary>
        private static Dictionary<string, string> ParseJsonToDict(string json)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(json)) return result;

            // Remove outer braces and whitespace
            json = json.Trim();
            if (json.StartsWith("{")) json = json.Substring(1);
            if (json.EndsWith("}")) json = json.Substring(0, json.Length - 1);

            // Split by lines and parse each key-value pair
            var lines = json.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed) || trimmed == "," || trimmed == "{" || trimmed == "}")
                    continue;

                // Remove trailing comma
                if (trimmed.EndsWith(","))
                    trimmed = trimmed.Substring(0, trimmed.Length - 1);

                // Find the colon separator (first colon after first quote)
                var colonIndex = trimmed.IndexOf("\":");
                if (colonIndex < 0) continue;

                colonIndex++; // Move to the colon position

                // Extract key
                var keyPart = trimmed.Substring(0, colonIndex).Trim();
                if (keyPart.StartsWith("\"")) keyPart = keyPart.Substring(1);
                if (keyPart.EndsWith("\"")) keyPart = keyPart.Substring(0, keyPart.Length - 1);

                // Extract value
                var valuePart = trimmed.Substring(colonIndex + 1).Trim();
                if (valuePart.StartsWith("\"")) valuePart = valuePart.Substring(1);
                if (valuePart.EndsWith("\"")) valuePart = valuePart.Substring(0, valuePart.Length - 1);

                // Unescape special characters
                valuePart = valuePart.Replace("\\n", "\n").Replace("\\\"", "\"").Replace("\\\\", "\\");

                if (!string.IsNullOrEmpty(keyPart))
                {
                    result[keyPart] = valuePart;
                }
            }

            return result;
        }

        /// <summary>
        /// Convert dictionary to JSON string
        /// </summary>
        private static string DictToJson(Dictionary<string, string> dict)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("{");

            var keys = new List<string>(dict.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var value = dict[key];

                // Escape special characters
                value = value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n");

                sb.Append($"  \"{key}\": \"{value}\"");

                if (i < keys.Count - 1)
                    sb.AppendLine(",");
                else
                    sb.AppendLine();
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>임베디드 리소스에서 언어 파일 로드 (ru.json 등)</summary>
        private static Dictionary<string, string> LoadFromEmbeddedResource(string lang)
        {
            try
            {
                var resourceName = $"CaptainSkillTree.Localization.{lang}.json";
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null) return null;
                    using (var reader = new System.IO.StreamReader(stream))
                        return ParseJsonToDict(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[Localization] {lang} 임베디드 리소스 로드 실패: {ex.Message}");
                return null;
            }
        }

        #endregion
    }
}

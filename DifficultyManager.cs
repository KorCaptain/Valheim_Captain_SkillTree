using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace CaptainSkillTree
{
    /// <summary>
    /// 난이도 프리셋 관리자.
    /// BepInEx\config\CaptainSkillTree\ 폴더의 프리셋 cfg 파일을
    /// 메인 cfg에 복사하고 Config.Reload()로 즉시 적용.
    ///
    /// 백업 메커니즘:
    ///   - 게임 종료 시(Application.quitting) 현재 메인 cfg → User_*.cfg 로 자동 저장
    ///   - 다음 업데이트 시 User_*.cfg 가 존재하면 선택창에 "3. User Setting" 표시
    /// </summary>
    public static class DifficultyManager
    {
        // ──────────────────────────── 상수 ────────────────────────────
        private const string PRESET_DIR_NAME = "CaptainSkillTree";
        private const string PRESET_NORMAL   = "Vanilra_Config_CaptainSkillTree.SkillTreeMod.cfg";
        private const string PRESET_VERYHARD = "Veryhard_CaptainSkillTree.SkillTreeMod.cfg";
        private const string PRESET_USER     = "User_CaptainSkillTree.SkillTreeMod.cfg";
        private const string DIFF_VER_EXT    = ".difficulty_ver";

        // ──────────────────────────── 상태 ────────────────────────────
        private static bool   _initialized    = false;
        private static string _configFilePath = "";

        /// <summary>true 이면 FejdStartup 패치에서 선택 창을 표시</summary>
        public static bool NeedsSelection { get; private set; }

        /// <summary>프리셋 적용 중일 때 true — GameDifficulty SettingChanged 재진입 방지용</summary>
        public static bool IsApplyingPreset { get; private set; }

        // ──────────────────────────── 초기화 ────────────────────────────
        /// <summary>
        /// FejdStartup.Awake Postfix에서 1회 호출.
        /// .version 파일과 .difficulty_ver 파일을 비교해 재선택 필요 여부를 판단.
        /// Application.quitting 이벤트를 등록해 종료 시 User 백업을 자동 저장.
        /// </summary>
        public static void InitializeIfNeeded()
        {
            if (_initialized) return;
            _initialized = true;

            // Plugin.Instance.Config.ConfigFilePath 를 우선 사용 (BepInEx 실제 경로)
            _configFilePath = Plugin.Instance?.Config?.ConfigFilePath
                              ?? Path.Combine(Paths.ConfigPath, "CaptainSkillTree.SkillTreeMod.cfg");

            string versionFile       = Path.ChangeExtension(_configFilePath, ".version");
            string difficultyVerFile = Path.ChangeExtension(_configFilePath, DIFF_VER_EXT);

            string currentVer  = File.Exists(versionFile)       ? File.ReadAllText(versionFile).Trim()       : "";
            string selectedVer = File.Exists(difficultyVerFile)  ? File.ReadAllText(difficultyVerFile).Trim() : "";

            EnsurePresetDirectory();
            Application.quitting += SaveUserBackupOnQuit;

            // ── 첫 설치: difficulty_ver 없음 → 선택 창 표시 ──
            bool isFirstInstall = string.IsNullOrEmpty(selectedVer);
            if (isFirstInstall)
            {
                NeedsSelection = true;
                return;
            }

            // ── 업데이트: 버전 불일치 → 유저값 유지 + 신규 키 VH 적용 후 창 표시 ──
            bool isUpdate = (currentVer != selectedVer);
            if (isUpdate)
            {
                ApplyVeryhardWithUserOverlay();  // 기존 유저값 보존 + 신규 키 VeryHard 적용
                SaveUserBackupNow();             // 병합 결과를 User 프리셋으로 저장 (창 3번 옵션)
                NeedsSelection = true;           // 선택 창 표시 (확인/재선택 가능)
                return;
            }

            // ── 동일 버전: 재선택 불필요 ──
            NeedsSelection = false;
        }

        // ──────────────────────────── 유저 백업 확인 ────────────────────────────
        /// <summary>User 프리셋 파일이 존재하면 true (3번 옵션 표시 여부)</summary>
        public static bool HasUserPreset() =>
            File.Exists(Path.Combine(GetPresetDirectory(), PRESET_USER));

        // ──────────────────────────── 업데이트 오버레이 ────────────────────────────
        /// <summary>
        /// 업데이트 시 자동 호출.
        /// 1) 현재 유저 값 스냅샷 (메모리에 로드된 기존 키/값)
        /// 2) VeryHard 전체 적용 (신규 키 포함 모든 항목이 VH 값으로 초기화)
        /// 3) 스냅샷에 있던 기존 키만 유저 값으로 복원
        ///    → 신규 추가 키는 VH 기본값 유지, 기존 커스텀 값은 보존
        /// </summary>
        private static void ApplyVeryhardWithUserOverlay()
        {
            var config = Plugin.Instance?.Config;
            if (config == null)
            {
                Plugin.Log?.LogError("[Difficulty] ApplyVeryhardWithUserOverlay: Config가 null입니다.");
                return;
            }

            // 1. 유저 기존 값 스냅샷 (VeryHard 적용 전)
            var userSnapshot = new Dictionary<ConfigDefinition, string>();
            foreach (var kvp in config)
                userSnapshot[kvp.Key] = kvp.Value.GetSerializedValue();

            // 2. VeryHard 전체 적용 (이 시점에서 메모리+디스크가 VH 값으로 덮어씌워짐)
            ApplyVeryHard();

            // 3. 유저 기존 값 복원 (스냅샷에 없는 신규 키는 VH 값 유지)
            // Language, 스키마 버전, 난이도 플래그는 복원 대상에서 제외
            var skipKeys = new HashSet<string>
            {
                "Language",
                "Config_Schema_Version",
                "GameDifficulty",
                "EnableLiveConfigSync"
            };

            bool prev = config.SaveOnConfigSet;
            config.SaveOnConfigSet = false;

            int restored = 0;
            int skipped  = 0;
            foreach (var kvp in userSnapshot)
            {
                if (skipKeys.Contains(kvp.Key.Key)) { skipped++; continue; }

                var entry = config[kvp.Key];
                if (entry == null) continue;

                try
                {
                    entry.SetSerializedValue(kvp.Value);
                    restored++;
                }
                catch { /* 타입 불일치(신규 타입 변경) → 무시 후 VH값 유지 */ }
            }

            // 결과가 유저 커스텀 값이므로 GameDifficulty를 "UserSettings"로 표시
            var gameDiffEntry = SkillTree.SkillTreeConfig.GameDifficulty;
            if (gameDiffEntry != null)
                gameDiffEntry.Value = "UserSettings";

            config.Save();
            config.SaveOnConfigSet = prev;

        }

        // ──────────────────────────── 적용 (편의 래퍼) ────────────────────────────
        /// <summary>Normal 프리셋 적용</summary>
        public static void ApplyNormal()   => ApplyPreset(PRESET_NORMAL);

        /// <summary>Very Hard 프리셋 적용</summary>
        public static void ApplyVeryHard() => ApplyPreset(PRESET_VERYHARD);

        /// <summary>User 프리셋 적용</summary>
        public static void ApplyUser()     => ApplyPreset(PRESET_USER);

        // ──────────────────────────── 내부 적용 로직 ────────────────────────────
        private static void ApplyPreset(string presetFileName)
        {
            IsApplyingPreset = true;
            string presetPath = Path.Combine(GetPresetDirectory(), presetFileName);

            if (File.Exists(presetPath))
            {
                // 1단계: 파일 복사 (디스크 영속성)
                try
                {
                    File.Copy(presetPath, _configFilePath, overwrite: true);
                }
                catch (Exception ex)
                {
                    Plugin.Log?.LogError($"[Difficulty] 파일 복사 실패: {ex.Message}");
                }

                // 2단계: SaveOnConfigSet 전체 차단 → Reload + 직접 설정 + 기본값 리셋 → 1회 Save
                // (중간 auto-save가 유저값을 덮어쓰는 문제 방지)
                var cfg = Plugin.Instance?.Config;
                if (cfg != null)
                {
                    bool prev = cfg.SaveOnConfigSet;
                    cfg.SaveOnConfigSet = false;          // ← 전체 작업 동안 autosave 차단

                    cfg.Reload();                         // 파일(프리셋)에서 메모리로 로드

                    int applied = ApplyPresetValuesToConfig(presetPath);  // 직접 설정 + 미포함 항목→기본값

                    // GameDifficulty config 동기화 (프리셋 파일에 없는 신규 키이므로 직접 설정)
                    string diffMode = (presetFileName == PRESET_NORMAL) ? "Vanilla"
                                    : (presetFileName == PRESET_VERYHARD) ? "VeryHard"
                                    : "UserSettings";
                    var diffEntry = SkillTree.SkillTreeConfig.GameDifficulty;
                    if (diffEntry != null)
                        diffEntry.Value = diffMode;

                    cfg.Save();                           // 1회 최종 저장
                    cfg.SaveOnConfigSet = prev;
                }
            }
            else
            {
                Plugin.Log?.LogWarning(
                    $"[Difficulty] ⚠️ 프리셋 파일 없음: {presetPath}\n" +
                    $"  → 현재 설정을 그대로 유지합니다.");
            }

            SaveDifficultyVersion();
            NeedsSelection = false;
            IsApplyingPreset = false;
        }

        // ──────────────────────────── 직접 값 적용 ────────────────────────────
        // ConfigFile 내부 entries 딕셔너리 — lazy init (타입 기반 검색)
        private static FieldInfo _entriesField;
        private static bool _entriesFieldSearched;

        private static FieldInfo GetEntriesField()
        {
            if (_entriesFieldSearched) return _entriesField;
            _entriesFieldSearched = true;

            var targetType = typeof(Dictionary<ConfigDefinition, ConfigEntryBase>);
            Type t = typeof(ConfigFile);
            while (t != null && t != typeof(object))
            {
                foreach (var f in t.GetFields(
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance  | BindingFlags.DeclaredOnly))
                {
                    if (f.FieldType == targetType)
                    {
                        _entriesField = f;
                        return f;
                    }
                }
                t = t.BaseType;
            }

            // 못 찾으면 전체 필드 목록 출력 (진단)
            t = typeof(ConfigFile);
            while (t != null && t != typeof(object))
            {
                foreach (var f in t.GetFields(
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance  | BindingFlags.DeclaredOnly))
                    Plugin.Log?.LogWarning($"[Difficulty] Field: {t.Name}.{f.Name} : {f.FieldType.FullName}");
                t = t.BaseType;
            }
            Plugin.Log?.LogWarning("[Difficulty] entries field not found");
            return null;
        }

        /// <summary>
        /// 프리셋 cfg 파일을 파싱해 ConfigEntry에 직접 값을 설정.
        /// Config.Reload() 타이밍 문제를 우회해 즉시 반영.
        /// </summary>
        private static int ApplyPresetValuesToConfig(string presetPath)
        {
            var config = Plugin.Instance?.Config;
            if (config == null) return 0;

            // lazy init으로 entries 딕셔너리 획득 (타입 기반 검색)
            var field = GetEntriesField();
            var entries = field?.GetValue(config)
                as Dictionary<ConfigDefinition, ConfigEntryBase>;
            if (entries == null)
            {
                Plugin.Log?.LogWarning("[Difficulty] entries 딕셔너리 접근 실패");
                return -1;
            }
            int count = 0;
            string currentSection = "";
            var appliedDefs = new HashSet<ConfigDefinition>();

            try
            {
                foreach (var raw in File.ReadAllLines(presetPath))
                {
                    var line = raw.Trim();

                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        currentSection = line.Substring(1, line.Length - 2);
                        continue;
                    }

                    if (line.StartsWith("#") || !line.Contains(" = ")) continue;

                    int eqIdx = line.IndexOf(" = ");
                    string key   = line.Substring(0, eqIdx).Trim();
                    string value = line.Substring(eqIdx + 3);

                    var def = new ConfigDefinition(currentSection, key);
                    if (entries.TryGetValue(def, out var entry))
                    {
                        try
                        {
                            entry.SetSerializedValue(value);
                            appliedDefs.Add(def);
                            count++;
                        }
                        catch { /* 파싱 실패는 무시 */ }
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[Difficulty] 값 직접 적용 실패: {ex.Message}");
            }

            // 프리셋에 없는 항목 → 기본값으로 리셋 (신규 추가 키 포함)
            int resetCount = 0;
            foreach (var kvp in entries)
            {
                if (!appliedDefs.Contains(kvp.Key))
                {
                    try
                    {
                        kvp.Value.BoxedValue = kvp.Value.DefaultValue;
                        resetCount++;
                    }
                    catch { /* 타입 불일치 무시 */ }
                }
            }
            return count + resetCount;
        }

        // ──────────────────────────── 헬퍼 ────────────────────────────
        /// <summary>프리셋 파일이 저장되는 경로</summary>
        public static string GetPresetDirectory() =>
            Path.Combine(Paths.ConfigPath, PRESET_DIR_NAME);

        /// <summary>프리셋 디렉토리 생성 + 내장 프리셋 cfg 파일 자동 추출</summary>
        public static void EnsurePresetDirectory()
        {
            try { Directory.CreateDirectory(GetPresetDirectory()); }
            catch (Exception ex) { Plugin.Log?.LogWarning($"[Difficulty] 폴더 생성 실패: {ex.Message}"); }

            ExtractEmbeddedPreset(PRESET_NORMAL);
            ExtractEmbeddedPreset(PRESET_VERYHARD);
        }

        /// <summary>
        /// DLL에 내장된 프리셋 cfg를 프리셋 디렉토리에 추출.
        /// Normal/VeryHard는 항상 DLL 내장 버전으로 덮어써서 올바른 값을 보장.
        /// </summary>
        private static void ExtractEmbeddedPreset(string fileName)
        {
            string destPath    = Path.Combine(GetPresetDirectory(), fileName);
            string resourceName = $"CaptainSkillTree.asset.{fileName}";
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        Plugin.Log?.LogWarning($"[Difficulty] 내장 리소스 없음: {resourceName}");
                        return;
                    }
                    using (var fs = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                        stream.CopyTo(fs);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[Difficulty] 프리셋 추출 실패 ({fileName}): {ex.Message}");
            }
        }

        /// <summary>
        /// 게임 종료 시 호출 — 현재 메인 cfg 를 User 백업으로 저장.
        /// 다음 업데이트 후 선택창에 "3. User Setting" 옵션으로 표시됨.
        /// </summary>
        private static void SaveUserBackupOnQuit()
        {
            if (string.IsNullOrEmpty(_configFilePath)) return;
            if (!File.Exists(_configFilePath)) return;

            string backupPath = Path.Combine(GetPresetDirectory(), PRESET_USER);
            try
            {
                File.Copy(_configFilePath, backupPath, overwrite: true);
                Plugin.Log?.LogInfo("[Difficulty] User 설정 백업 저장 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[Difficulty] User 백업 저장 실패: {ex.Message}");
            }
        }

        private static void SaveUserBackupNow()
        {
            if (string.IsNullOrEmpty(_configFilePath) || !File.Exists(_configFilePath)) return;
            string backupPath = Path.Combine(GetPresetDirectory(), PRESET_USER);
            try
            {
                File.Copy(_configFilePath, backupPath, overwrite: true);
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[Difficulty] User 백업 갱신 실패: {ex.Message}");
            }
        }

        private static void SaveDifficultyVersion()
        {
            try
            {
                string versionFile       = Path.ChangeExtension(_configFilePath, ".version");
                string difficultyVerFile = Path.ChangeExtension(_configFilePath, DIFF_VER_EXT);
                string currentVer        = File.Exists(versionFile) ? File.ReadAllText(versionFile).Trim() : "";
                File.WriteAllText(difficultyVerFile, currentVer);
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[Difficulty] 버전 기록 실패: {ex.Message}");
            }
        }
    }
}

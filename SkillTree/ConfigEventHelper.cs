using BepInEx.Configuration;
using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// Config 이벤트 핸들러 등록용 공통 헬퍼 클래스
    /// 여러 Config 파일에서 중복되는 SettingChanged 이벤트 등록을 공통화
    ///
    /// 참고: ConfigEntryBase에는 SettingChanged 이벤트가 없으므로,
    /// 제네릭 타입별 메서드(RegisterFloatEntries, RegisterIntEntries 등)를 사용해야 합니다.
    /// </summary>
    public static class ConfigEventHelper
    {
        /// <summary>
        /// 여러 ConfigEntry&lt;float&gt;에 동일한 콜백을 등록
        /// </summary>
        /// <param name="callback">설정 변경 시 호출할 콜백</param>
        /// <param name="entries">이벤트를 등록할 ConfigEntry 배열</param>
        public static void RegisterFloatEntries(Action callback, params ConfigEntry<float>[] entries)
        {
            if (callback == null) return;

            foreach (var entry in entries)
            {
                if (entry != null)
                {
                    entry.SettingChanged += (sender, args) => callback();
                }
            }
        }

        /// <summary>
        /// 여러 ConfigEntry&lt;int&gt;에 동일한 콜백을 등록
        /// </summary>
        public static void RegisterIntEntries(Action callback, params ConfigEntry<int>[] entries)
        {
            if (callback == null) return;

            foreach (var entry in entries)
            {
                if (entry != null)
                {
                    entry.SettingChanged += (sender, args) => callback();
                }
            }
        }

        /// <summary>
        /// 여러 ConfigEntry&lt;bool&gt;에 동일한 콜백을 등록
        /// </summary>
        public static void RegisterBoolEntries(Action callback, params ConfigEntry<bool>[] entries)
        {
            if (callback == null) return;

            foreach (var entry in entries)
            {
                if (entry != null)
                {
                    entry.SettingChanged += (sender, args) => callback();
                }
            }
        }

        /// <summary>
        /// 여러 ConfigEntry&lt;string&gt;에 동일한 콜백을 등록
        /// </summary>
        public static void RegisterStringEntries(Action callback, params ConfigEntry<string>[] entries)
        {
            if (callback == null) return;

            foreach (var entry in entries)
            {
                if (entry != null)
                {
                    entry.SettingChanged += (sender, args) => callback();
                }
            }
        }

        /// <summary>
        /// 특정 ConfigEntry에 값 변경 로깅 핸들러 추가
        /// </summary>
        public static void AddValueChangeLogger<T>(ConfigEntry<T> entry, string configName, string settingName)
        {
            if (entry == null) return;

            entry.SettingChanged += (sender, args) =>
            {
                Plugin.Log.LogInfo($"[{configName}] {settingName} 변경됨: {entry.Value}");
            };
        }

        /// <summary>
        /// float와 int 항목을 함께 등록하는 헬퍼 메서드
        /// </summary>
        /// <param name="callback">설정 변경 시 호출할 콜백</param>
        /// <param name="floatEntries">float 타입 ConfigEntry 배열</param>
        /// <param name="intEntries">int 타입 ConfigEntry 배열</param>
        public static void RegisterMixedEntries(
            Action callback,
            ConfigEntry<float>[] floatEntries,
            ConfigEntry<int>[] intEntries = null)
        {
            RegisterFloatEntries(callback, floatEntries);
            if (intEntries != null)
            {
                RegisterIntEntries(callback, intEntries);
            }
        }
    }
}

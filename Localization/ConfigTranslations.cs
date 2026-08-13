using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// BepInEx Configuration Manager 로컬라이제이션
    /// F1 메뉴에서 표시되는 카테고리와 설명을 언어별로 번역합니다.
    /// 이 파일은 코어 파일 (partial class)입니다.
    /// 각 트리별 번역은 ConfigTranslations_*Desc.cs 파일에 있습니다.
    /// 키 이름 번역은 ConfigTranslations_KeyNames_*.cs 파일에 있습니다.
    /// _RequiredPoints 항목은 SkillTreeConfig.GetLocalizedDescription/KeyName에서 런타임 처리됩니다.
    /// </summary>
    public static partial class ConfigTranslations
    {
        /// <summary>
        /// 설명 번역 가져오기
        /// </summary>
        public static Dictionary<string, string> GetDescriptionTranslations(string lang)
        {
            if (lang == "ko") return GetKoreanDescriptions();
            if (lang == "zh-cn") return GetChineseDescriptions();
            if (lang == "de") return GetGermanDescriptions();
            if (lang == "ru") return GetRussianDescriptions();
            if (lang == "pt_BR") return GetPortugueseBrazilianDescriptions();
            if (lang == "ja") return GetJapaneseDescriptions();
            return GetEnglishDescriptions();
        }

        /// <summary>
        /// Config 키 이름 번역 가져오기 (F1 메뉴 2차 항목 표시명)
        /// BindServerSync → GetLocalizedKeyName()에서 호출되어 ConfigurationManagerAttributes { DispName }에 실제 적용됨
        /// </summary>
        public static Dictionary<string, string> GetKeyNameTranslations(string lang)
        {
            if (lang == "ko") return GetKoreanKeyNames();
            if (lang == "zh-cn") return GetChineseKeyNames();
            if (lang == "de") return GetGermanKeyNames();
            if (lang == "ru") return GetRussianKeyNames();
            if (lang == "pt_BR") return GetPortugueseBrazilianKeyNames();
            if (lang == "ja") return GetJapaneseKeyNames();
            return GetEnglishKeyNames();
        }

        // ============================================
        // 설명 번역 집합 (한국어) - 각 파일의 부분 메서드를 합산
        // ============================================
        private static Dictionary<string, string> GetKoreanDescriptions()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_KO())    dict[kv.Key] = kv.Value;
            foreach (var kv in GetRangedDescriptions_KO())    dict[kv.Key] = kv.Value;
            foreach (var kv in GetSwordKnifeDescriptions_KO()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetHeavyMeleeDescriptions_KO()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetJobDescriptions_KO())        dict[kv.Key] = kv.Value;
            foreach (var kv in GetCaptainLevelDescriptions_KO()) dict[kv.Key] = kv.Value;
            return dict;
        }

        // ============================================
        // 설명 번역 집합 (영어) - 각 파일의 부분 메서드를 합산
        // ============================================
        private static Dictionary<string, string> GetEnglishDescriptions()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_EN())    dict[kv.Key] = kv.Value;
            foreach (var kv in GetRangedDescriptions_EN())    dict[kv.Key] = kv.Value;
            foreach (var kv in GetSwordKnifeDescriptions_EN()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetHeavyMeleeDescriptions_EN()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetJobDescriptions_EN())        dict[kv.Key] = kv.Value;
            foreach (var kv in GetCaptainLevelDescriptions_EN()) dict[kv.Key] = kv.Value;
            return dict;
        }

        // ============================================
        // 설명 번역 집합 (러시아어) - 각 파일의 부분 메서드를 합산
        // ============================================
        private static Dictionary<string, string> GetRussianDescriptions()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_RU())     dict[kv.Key] = kv.Value;
            foreach (var kv in GetRangedDescriptions_RU())     dict[kv.Key] = kv.Value;
            foreach (var kv in GetSwordKnifeDescriptions_RU()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetHeavyMeleeDescriptions_RU()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetJobDescriptions_RU())         dict[kv.Key] = kv.Value;
            return dict;
        }

        // ============================================
        // 설명 번역 집합 (독일어) - 각 파일의 부분 메서드를 합산
        // ============================================
        private static Dictionary<string, string> GetGermanDescriptions()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_DE())      dict[kv.Key] = kv.Value;
            foreach (var kv in GetRangedDescriptions_DE())      dict[kv.Key] = kv.Value;
            foreach (var kv in GetSwordKnifeDescriptions_DE())  dict[kv.Key] = kv.Value;
            foreach (var kv in GetHeavyMeleeDescriptions_DE())  dict[kv.Key] = kv.Value;
            foreach (var kv in GetJobDescriptions_DE())          dict[kv.Key] = kv.Value;
            return dict;
        }

        // ============================================
        // 설명 번역 집합 (포르투갈어 BR)
        // ============================================
        private static Dictionary<string, string> GetPortugueseBrazilianDescriptions()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_PTBR())     dict[kv.Key] = kv.Value;
            foreach (var kv in GetRangedDescriptions_PTBR())     dict[kv.Key] = kv.Value;
            foreach (var kv in GetSwordKnifeDescriptions_PTBR()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetHeavyMeleeDescriptions_PTBR()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetJobDescriptions_PTBR())        dict[kv.Key] = kv.Value;
            return dict;
        }

        // ============================================
        // 설명 번역 집합 (중국어 간체)
        // ============================================
        private static Dictionary<string, string> GetChineseDescriptions()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_CN())      dict[kv.Key] = kv.Value;
            foreach (var kv in GetRangedDescriptions_CN())      dict[kv.Key] = kv.Value;
            foreach (var kv in GetSwordKnifeDescriptions_CN())  dict[kv.Key] = kv.Value;
            foreach (var kv in GetHeavyMeleeDescriptions_CN())  dict[kv.Key] = kv.Value;
            foreach (var kv in GetJobDescriptions_CN())         dict[kv.Key] = kv.Value;
            return dict;
        }

        // GetKoreanKeyNames()  → ConfigTranslations_KeyNames_KO.cs
        // GetEnglishKeyNames() → ConfigTranslations_KeyNames_EN.cs
        // GetGermanKeyNames()  → ConfigTranslations_KeyNames_DE.cs + _DE_Part2.cs
        // GetRussianKeyNames() → ConfigTranslations_KeyNames_RU.cs
        // GetChineseKeyNames() → ConfigTranslations_KeyNames_CN.cs + _CN_Part2.cs
        // GetJapaneseKeyNames() → GetEnglishKeyNames() fallback (JP 전용 파일 없음)
        // GetExpertDescriptions_KO/EN/DE/RU/CN()     → ConfigTranslations_ExpertDesc.cs / *_DE.cs / *_RU.cs / *_CN.cs
        // GetRangedDescriptions_KO/EN/DE/RU/CN()     → ConfigTranslations_RangedDesc.cs / *_DE.cs / *_RU.cs / *_CN.cs
        // GetSwordKnifeDescriptions_KO/EN/DE/RU/CN() → ConfigTranslations_SwordKnifeDesc.cs / *_DE.cs / *_RU.cs / *_CN.cs
        // GetHeavyMeleeDescriptions_KO/EN/DE/RU/CN() → ConfigTranslations_HeavyMeleeDesc.cs / *_DE.cs / *_RU.cs / *_CN.cs
        // GetJobDescriptions_KO/EN/DE/RU/CN()        → ConfigTranslations_JobDesc.cs / *_DE.cs / *_RU.cs / *_CN.cs

        // ============================================
        // 설명 번역 집합 (일본어)
        // GetExpertDescriptions_JP()     → ConfigTranslations_ExpertDesc_JP.cs
        // GetRangedDescriptions_JP()     → ConfigTranslations_RangedDesc_JP.cs
        // GetSwordKnifeDescriptions_JP() → ConfigTranslations_SwordKnifeDesc_JP.cs
        // GetHeavyMeleeDescriptions_JP() → ConfigTranslations_HeavyMeleeDesc_JP.cs
        // GetJobDescriptions_JP()        → ConfigTranslations_JobDesc_JP.cs
        // ============================================
        private static Dictionary<string, string> GetJapaneseDescriptions()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetExpertDescriptions_JP()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetRangedDescriptions_JP()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetSwordKnifeDescriptions_JP()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetHeavyMeleeDescriptions_JP()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetJobDescriptions_JP()) dict[kv.Key] = kv.Value;
            return dict;
        }

        // GetJapaneseKeyNames() → ConfigTranslations_KeyNames_JP.cs + _JP_Part2.cs
        private static Dictionary<string, string> GetJapaneseKeyNames()
        {
            var dict = new Dictionary<string, string>();
            foreach (var kv in GetJapaneseKeyNames_Part1()) dict[kv.Key] = kv.Value;
            foreach (var kv in GetJapaneseKeyNames_Part2()) dict[kv.Key] = kv.Value;
            return dict;
        }
    }
}

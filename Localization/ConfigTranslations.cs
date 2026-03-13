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
        /// 카테고리 번역 가져오기
        /// </summary>
        public static Dictionary<string, string> GetCategoryTranslations(string lang)
        {
            return (lang == "ko") ? GetKoreanCategories()
                 : (lang == "pt_BR") ? GetPortugueseBrazilianCategories()
                 : GetEnglishCategories();
        }

        /// <summary>
        /// 설명 번역 가져오기
        /// </summary>
        public static Dictionary<string, string> GetDescriptionTranslations(string lang)
        {
            if (lang == "ko") return GetKoreanDescriptions();
            if (lang == "ru") return GetRussianDescriptions();
            if (lang == "pt_BR") return GetPortugueseBrazilianDescriptions();
            return GetEnglishDescriptions();
        }

        /// <summary>
        /// Config 키 이름 번역 가져오기 (F1 메뉴 2차 항목 표시명)
        /// BindServerSync → GetLocalizedKeyName()에서 호출되어 ConfigurationManagerAttributes { DispName }에 실제 적용됨
        /// </summary>
        public static Dictionary<string, string> GetKeyNameTranslations(string lang)
        {
            if (lang == "ko") return GetKoreanKeyNames();
            if (lang == "ru") return GetRussianKeyNames();
            if (lang == "pt_BR") return GetPortugueseBrazilianKeyNames();
            return GetEnglishKeyNames();
        }

        // ============================================
        // 카테고리 번역 (한국어)
        // ============================================
        private static Dictionary<string, string> GetKoreanCategories()
        {
            return new Dictionary<string, string>
            {
                ["Attack Tree"] = "Attack Tree",
                ["Defense Tree"] = "Defense Tree",
                ["Production Tree"] = "Production Tree",
                ["Staff Tree"] = "Staff Tree",
                ["Crossbow Tree"] = "Crossbow Tree",
                ["Bow Tree"] = "Bow Tree",
                ["Sword Tree"] = "Sword Tree",
                ["Spear Tree"] = "Spear Tree",
                ["Mace Tree"] = "Mace Tree",
                ["Polearm Tree"] = "Polearm Tree",
                ["Knife Tree"] = "Knife Tree",
                ["Speed Tree"] = "Speed Tree",
                ["Archer Job Skills"] = "Archer Job Skills",
                ["Mage Job Skills"] = "Mage Job Skills",
                ["Tanker Job Skills"] = "Tanker Job Skills",
                ["Rogue Job Skills"] = "Rogue Job Skills",
                ["Paladin Job Skills"] = "Paladin Job Skills",
                ["Berserker Job Skills"] = "Berserker Job Skills",
            };
        }

        // ============================================
        // 카테고리 번역 (영어)
        // ============================================
        private static Dictionary<string, string> GetEnglishCategories()
        {
            return new Dictionary<string, string>
            {
                ["Attack Tree"] = "Attack Tree",
                ["Defense Tree"] = "Defense Tree",
                ["Production Tree"] = "Production Tree",
                ["Staff Tree"] = "Staff Tree",
                ["Crossbow Tree"] = "Crossbow Tree",
                ["Bow Tree"] = "Bow Tree",
                ["Sword Tree"] = "Sword Tree",
                ["Spear Tree"] = "Spear Tree",
                ["Mace Tree"] = "Mace Tree",
                ["Polearm Tree"] = "Polearm Tree",
                ["Knife Tree"] = "Knife Tree",
                ["Speed Tree"] = "Speed Tree",
                ["Archer Job Skills"] = "Archer Job Skills",
                ["Mage Job Skills"] = "Mage Job Skills",
                ["Tanker Job Skills"] = "Tanker Job Skills",
                ["Rogue Job Skills"] = "Rogue Job Skills",
                ["Paladin Job Skills"] = "Paladin Job Skills",
                ["Berserker Job Skills"] = "Berserker Job Skills",
            };
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
        // 카테고리 번역 (포르투갈어 BR)
        // ============================================
        private static Dictionary<string, string> GetPortugueseBrazilianCategories()
        {
            return new Dictionary<string, string>
            {
                ["Attack Tree"] = "Árvore de Ataque",
                ["Defense Tree"] = "Árvore de Defesa",
                ["Production Tree"] = "Árvore de Produção",
                ["Staff Tree"] = "Árvore de Cajado",
                ["Crossbow Tree"] = "Árvore de Besta",
                ["Bow Tree"] = "Árvore de Arco",
                ["Sword Tree"] = "Árvore de Espada",
                ["Spear Tree"] = "Árvore de Lança",
                ["Mace Tree"] = "Árvore de Maça",
                ["Polearm Tree"] = "Árvore de Haste",
                ["Knife Tree"] = "Árvore de Adaga",
                ["Speed Tree"] = "Árvore de Velocidade",
                ["Archer Job Skills"] = "Habilidades de Arqueiro",
                ["Mage Job Skills"] = "Habilidades de Mago",
                ["Tanker Job Skills"] = "Habilidades de Tanker",
                ["Rogue Job Skills"] = "Habilidades de Ladino",
                ["Paladin Job Skills"] = "Habilidades de Paladino",
                ["Berserker Job Skills"] = "Habilidades de Berserker",
            };
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

        // GetKoreanKeyNames()  → ConfigTranslations_KeyNames_KO.cs
        // GetEnglishKeyNames() → ConfigTranslations_KeyNames_EN.cs
        // GetRussianKeyNames() → ConfigTranslations_KeyNames_RU.cs
        // GetExpertDescriptions_KO/EN/RU()    → ConfigTranslations_ExpertDesc.cs / *_RU.cs
        // GetRangedDescriptions_KO/EN/RU()    → ConfigTranslations_RangedDesc.cs / *_RU.cs
        // GetSwordKnifeDescriptions_KO/EN/RU() → ConfigTranslations_SwordKnifeDesc.cs / *_RU.cs
        // GetHeavyMeleeDescriptions_KO/EN/RU() → ConfigTranslations_HeavyMeleeDesc.cs / *_RU.cs
        // GetJobDescriptions_KO/EN/RU()        → ConfigTranslations_JobDesc.cs / *_RU.cs
    }
}

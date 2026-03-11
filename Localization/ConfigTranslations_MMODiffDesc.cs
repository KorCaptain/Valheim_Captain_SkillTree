using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// MMO 몬스터 난이도 시스템 Config 설명 번역
    /// ConfigTranslations_JobDesc.cs 패턴의 partial class
    /// ConfigTranslations.cs에서 GetKoreanDescriptions/GetEnglishDescriptions/GetRussianDescriptions에 통합됨
    /// </summary>
    public static partial class ConfigTranslations
    {
        // ============================================
        // MMO 난이도 설명 (한국어)
        // ============================================
        private static Dictionary<string, string> GetMMODiffDescriptions_KO()
        {
            return new Dictionary<string, string>
            {
                ["MMODiff_Enable"] =
                "【MMO 난이도 시스템 활성화】\n" +
                "스킬트리 스킬포인트(SP)에 비례해 몬스터의 m_level이\n" +
                "자동으로 올라가는 MMO 난이도 시스템입니다.\n" +
                "false로 설정 시 몬스터 레벨 변화 없음.\n" +
                "[기본: true]",

                ["MMODiff_SpBonus_0_10"] =
                "【SP 0~10 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 0~10 범위일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "별 확률 롤 성공 시에만 적용됩니다.\n" +
                "[기본: 2]",

                ["MMODiff_SpBonus_11_20"] =
                "【SP 11~20 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 11~20 범위일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "[기본: 3]",

                ["MMODiff_SpBonus_21_30"] =
                "【SP 21~30 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 21~30 범위일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "[기본: 4]",

                ["MMODiff_SpBonus_31_40"] =
                "【SP 31~40 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 31~40 범위일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "[기본: 5]",

                ["MMODiff_SpBonus_41_70"] =
                "【SP 41~70 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 41~70 범위일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "[기본: 5]",

                ["MMODiff_SpBonus_71_80"] =
                "【SP 71~80 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 71~80 범위일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "[기본: 6]",

                ["MMODiff_SpBonus_81_100"] =
                "【SP 81~100 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 81~100 범위일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "[기본: 7]",

                ["MMODiff_SpBonus_101Plus"] =
                "【SP 101+ 구간: 레벨 보너스】\n" +
                "사용된 스킬포인트가 101 이상일 때\n" +
                "몬스터 m_level에 추가되는 보너스 값입니다.\n" +
                "[기본: 8]",

                ["MMODiff_MaxLevelBonus"] =
                "【최대 레벨 보너스 상한】\n" +
                "SP 구간별 보너스가 이 값을 초과하지 않도록 제한합니다.\n" +
                "액티브/직업 스킬 보너스(+1씩)는 이 상한과 별개입니다.\n" +
                "[기본: 10]",

                ["MMODiff_BaseStarChance"] =
                "【기본 별 추가 확률 (%)】\n" +
                "SP와 무관하게 항상 적용되는 기본 별 추가 확률입니다.\n" +
                "최종 확률 = 기본 확률 + (SP / StarChanceDivider)\n" +
                "예: SP=30이면 25 + 30/3 = 35%\n" +
                "[기본: 25.0]",

                ["MMODiff_StarChanceDivider"] =
                "【별 확률 SP 나누기 값】\n" +
                "SP를 이 값으로 나눈 결과를 기본 확률에 더합니다.\n" +
                "값이 작을수록 SP에 따른 확률 상승이 빨라집니다.\n" +
                "예: 3.0이면 SP 225에서 확률 100% 도달\n" +
                "[기본: 3.0]",

                ["MMODiff_MaxStarChance"] =
                "【최대 별 추가 확률 (%)】\n" +
                "별 추가 확률의 최대 상한값입니다.\n" +
                "아무리 SP가 높아도 이 값을 초과하지 않습니다.\n" +
                "[기본: 100.0]",

                ["MMODiff_ActiveSkillBonus"] =
                "【액티브/직업 스킬 보너스 활성화】\n" +
                "G/H/R키 액티브 스킬 보유 시 +1 레벨 보너스 확정.\n" +
                "Y키 직업 스킬 보유 시 +1 레벨 보너스 추가 확정.\n" +
                "false로 설정 시 이 보너스들이 비활성화됩니다.\n" +
                "[기본: true]",
            };
        }

        // ============================================
        // MMO 난이도 설명 (영어)
        // ============================================
        private static Dictionary<string, string> GetMMODiffDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                ["MMODiff_Enable"] =
                "【MMO Difficulty System】\n" +
                "Automatically increases monster m_level proportional to\n" +
                "the player's used skill points (SP).\n" +
                "Set to false to disable monster level scaling.\n" +
                "[Default: true]",

                ["MMODiff_SpBonus_0_10"] =
                "【SP 0~10 Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 0~10.\n" +
                "Only applied when star chance roll succeeds.\n" +
                "[Default: 2]",

                ["MMODiff_SpBonus_11_20"] =
                "【SP 11~20 Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 11~20.\n" +
                "[Default: 3]",

                ["MMODiff_SpBonus_21_30"] =
                "【SP 21~30 Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 21~30.\n" +
                "[Default: 4]",

                ["MMODiff_SpBonus_31_40"] =
                "【SP 31~40 Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 31~40.\n" +
                "[Default: 5]",

                ["MMODiff_SpBonus_41_70"] =
                "【SP 41~70 Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 41~70.\n" +
                "[Default: 5]",

                ["MMODiff_SpBonus_71_80"] =
                "【SP 71~80 Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 71~80.\n" +
                "[Default: 6]",

                ["MMODiff_SpBonus_81_100"] =
                "【SP 81~100 Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 81~100.\n" +
                "[Default: 7]",

                ["MMODiff_SpBonus_101Plus"] =
                "【SP 101+ Range: Level Bonus】\n" +
                "Monster m_level bonus when used SP is 101 or more.\n" +
                "[Default: 8]",

                ["MMODiff_MaxLevelBonus"] =
                "【Max Level Bonus Cap】\n" +
                "The SP-based bonus will not exceed this value.\n" +
                "Active/job skill bonuses (+1 each) are separate from this cap.\n" +
                "[Default: 10]",

                ["MMODiff_BaseStarChance"] =
                "【Base Star Chance (%)】\n" +
                "The baseline star chance regardless of SP.\n" +
                "Final chance = Base + (SP / StarChanceDivider)\n" +
                "Example: SP=30 → 25 + 30/3 = 35%\n" +
                "[Default: 25.0]",

                ["MMODiff_StarChanceDivider"] =
                "【Star Chance SP Divider】\n" +
                "SP is divided by this value and added to base chance.\n" +
                "Lower value = faster chance increase per SP.\n" +
                "Example: 3.0 → reaches 100% at SP 225\n" +
                "[Default: 3.0]",

                ["MMODiff_MaxStarChance"] =
                "【Max Star Chance (%)】\n" +
                "Maximum cap for the star addition chance.\n" +
                "No matter how high SP is, chance will not exceed this.\n" +
                "[Default: 100.0]",

                ["MMODiff_ActiveSkillBonus"] =
                "【Active/Job Skill Bonus】\n" +
                "Having a G/H/R key active skill guarantees +1 level bonus.\n" +
                "Having a Y key job skill guarantees an additional +1 bonus.\n" +
                "Set to false to disable these bonuses.\n" +
                "[Default: true]",
            };
        }

        // ============================================
        // MMO 난이도 설명 (러시아어) - EN 원문으로 임시 등록
        // ============================================
        private static Dictionary<string, string> GetMMODiffDescriptions_RU()
        {
            // 전문 번역가 번역 전까지 EN 원문 사용
            return GetMMODiffDescriptions_EN();
        }
    }
}

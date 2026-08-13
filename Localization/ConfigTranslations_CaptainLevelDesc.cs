using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetCaptainLevelDescriptions_KO()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // - Captain Level System - 카테고리
                // ========================================
                ["Enable Captain Level System"] =
                "자체 레벨 시스템 활성화 여부\n" +
                "EpicMMOSystem이 감지되면 자동으로 비활성화됩니다.\n" +
                "[기본: true]",

                ["Max Level"] =
                "최대 레벨\n" +
                "[기본: 100, 범위: 1-999]",

                ["Base Level Exp"] =
                "기본 경험치 (레벨 1에서 2로 올리는데 필요한 경험치)\n" +
                "[기본: 300]",

                ["Multi Next Level"] =
                "다음 레벨 경험치 배수\n" +
                "레벨이 오를수록 필요 경험치가 이 배수만큼 증가합니다.\n" +
                "[기본: 1.05]",

                ["Exp Rate"] =
                "경험치 배율\n" +
                "획득하는 모든 경험치에 적용됩니다.\n" +
                "[기본: 1.0, 범위: 0.1-10.0]",

                ["Cumulative Exp Mode"] =
                "누적 경험치 모드\n" +
                "true: 필요 경험치 = (이전 필요 경험치 * 배수) + 기본 경험치\n" +
                "false: 필요 경험치 = 이전 필요 경험치 * 배수\n" +
                "[기본: true]",

                ["Max Level Exp Range"] =
                "최대 레벨 차이\n" +
                "플레이어 레벨 + 이 값보다 높은 몬스터는 경험치가 감소합니다.\n" +
                "[기본: 10]",

                ["Min Level Exp Range"] =
                "최소 레벨 차이\n" +
                "플레이어 레벨 - 이 값보다 낮은 몬스터는 경험치가 감소합니다.\n" +
                "[기본: 10]",

                ["Curve Exp Outside Range"] =
                "범위 밖 경험치 점진적 감소\n" +
                "범위 밖 몬스터의 경험치가 거리에 따라 점진적으로 감소합니다.\n" +
                "[기본: false]",

                ["No Exp Past Level Range"] =
                "범위 밖 경험치 0\n" +
                "범위 밖 몬스터의 경험치가 0이 됩니다.\n" +
                "[기본: false]",

                ["Star Level Exp Multiplier"] =
                "별 레벨 경험치 배수\n" +
                "몬스터 별 1개당 추가 경험치 배율입니다.\n" +
                "예: 1성 몬스터 = 기본 경험치 + (최대 경험치 * 이 값 * 1)\n" +
                "[기본: 1.5]",

                ["Mob Level Per Star"] =
                "별당 몬스터 레벨 증가\n" +
                "true: 1성 몬스터는 기본 레벨 + 1로 표시됩니다.\n" +
                "예: Lv.10 몬스터가 1성이면 Lv.11로 표시\n" +
                "[기본: true]",

                ["Show Exp Popup"] =
                "경험치 획득 시 팝업 표시\n" +
                "[기본: true]",

                ["Show Level Up Effect"] =
                "레벨업 시 이펙트 표시\n" +
                "[기본: true]",

                ["Show Level HUD"] =
                "레벨/경험치 바 HUD 표시\n" +
                "[기본: true]",

                ["HUD HP Color"] =
                "HP 바 색상 (HTML 16진수)\n" +
                "[기본: #870000]",

                ["HUD Stamina Color"] =
                "스태미나 바 색상 (HTML 16진수)\n" +
                "[기본: #986100]",

                ["HUD Eitr Color"] =
                "에이트르 바 색상 (HTML 16진수)\n" +
                "[기본: #84257C]",

                ["HUD Exp Color"] =
                "경험치 바 색상 (HTML 16진수)\n" +
                "[기본: #C87820]",

                ["HUD Scale"] =
                "HUD 전체 스케일\n" +
                "[기본: 1.0, 범위: 0.5-3.0]",

                ["Skill Points Per Level"] =
                "레벨당 스킬 포인트 획득량\n" +
                "[기본: 2, 범위: 1-10]",

                ["Use Skill Point Based Level"] =
                "스킬포인트 기반 레벨 계산 사용\n" +
                "EpicMMO가 없을 때 사용한 스킬 포인트를 기준으로 레벨을 계산합니다.\n" +
                "예: 102 포인트 사용 / 2 = 51레벨\n" +
                "[기본: true]",

                ["Points Required Per Level"] =
                "레벨당 필요 스킬 포인트\n" +
                "스킬포인트 기반 레벨 계산 시 사용됩니다.\n" +
                "예: 102 포인트 / 2 = 51레벨\n" +
                "[기본: 2, 범위: 1-10]",

                ["Auto Sync To EpicMMO"] =
                "EpicMMO 자동 동기화\n" +
                "EpicMMO 설치 시 스킬포인트 기반 레벨을 EpicMMO에 동기화합니다.\n" +
                "[기본: true]",

                ["Exit Button"] =
                "탈출 버튼 표시 (기본: false)\n" +
                "스킬트리 UI에 탈출 버튼을 표시합니다.\n" +
                "⚠️ 관리자만 변경 가능. 서버 설정이 모든 클라이언트에 동기화됩니다.\n" +
                "[기본: false]",

                ["Enable Level Diff Damage Reduction"] =
                "LV 차이 공격옵션 활성화 여부\n" +
                "몬스터 레벨이 플레이어보다 11 이상 높으면 플레이어가 가하는 피해가 감소합니다.\n" +
                "⚠️ 관리자만 변경 가능. 서버 설정이 모든 클라이언트에 동기화됩니다.\n" +
                "[기본: true]",

                ["Level Diff Tier1 Damage Percent"] =
                "레벨 차이 11~15 구간 데미지 배율 (%)\n" +
                "[기본: 40]",

                ["Level Diff Tier2 Damage Percent"] =
                "레벨 차이 16~20 구간 데미지 배율 (%)\n" +
                "[기본: 20]",

                ["Level Diff Tier3 Damage Percent"] =
                "레벨 차이 21~30 구간 데미지 배율 (%)\n" +
                "[기본: 10]",

                ["Level Diff Tier4 Damage Percent"] =
                "레벨 차이 31 이상 구간 데미지 배율 (%)\n" +
                "[기본: 0]",

                ["Exp Diff Tier1 Percent"] =
                "레벨 차이(범위 초과분) 11~15 구간 경험치 배율 (%)\n" +
                "Curve Exp 활성화 시, 범위(Max/Min Level Exp) 밖 몬스터의 경험치가 이 구간별 비율로 지급됩니다.\n" +
                "[기본: 30]",

                ["Exp Diff Tier2 Percent"] =
                "레벨 차이(범위 초과분) 16~20 구간 경험치 배율 (%)\n" +
                "[기본: 25]",

                ["Exp Diff Tier3 Percent"] =
                "레벨 차이(범위 초과분) 21~30 구간 경험치 배율 (%)\n" +
                "[기본: 20]",

                ["Exp Diff Tier4 Percent"] =
                "레벨 차이(범위 초과분) 31 이상 구간 경험치 배율 (%)\n" +
                "[기본: 10]",

                ["Enable Level Diff Drop Suppression"] =
                "레벨 차이 아이템 드랍 억제 활성화 여부\n" +
                "몬스터 레벨이 플레이어보다 Drop Suppression Level Diff 이상 높으면 아이템이 드랍되지 않습니다.\n" +
                "⚠️ 관리자만 변경 가능. 서버 설정이 모든 클라이언트에 동기화됩니다.\n" +
                "[기본: true]",

                ["Drop Suppression Level Diff"] =
                "아이템 드랍 억제가 발동하는 레벨 차이 기준\n" +
                "[기본: 16]",

                ["Generate JSON Files"] =
                "JSON 기본 파일 생성 여부\n" +
                "true로 설정 시 BepInEx/config/CaptainSkillTree/ 폴더에\n" +
                "MonsterExp.json, LevelExp.json 기본 파일이 생성됩니다.\n" +
                "(이미 파일이 있으면 덮어쓰지 않음)\n" +
                "[기본: true]",

                ["Admin Mode"] =
                "관리자 모드 (기본: false)\n" +
                "On: 스킬/직업 배우기 및 초기화 시 모든 조건(코인/트로피/포인트/전제조건) 무시\n" +
                "⚠️ 관리자 권한(서버 호스트 또는 AdminList)이 있는 플레이어만 적용됩니다.\n" +
                "[기본: false]",

                // ========================================
                // Skill_Tree_Base - 누락된 3개 키
                // ========================================
                ["EnableLiveConfigSync"] =
                "F1 메뉴 실시간 Config 싱크 활성화.\n" +
                "false (기본) = skillconfig sync 커맨드로만 적용 — 크래시 방지 권장\n" +
                "true = F1 메뉴 변경 즉시 서버 전송 (1.5초 debounce 적용)",

                ["My VFX 투명도"] =
                "메이지 스킬 VFX 전용 투명도 조절 (0=완전 투명, 100=원본 밝기)\n" +
                "기본값: 90 (90% 밝기)\n" +
                "변경 후 게임 재시작 필요",
            };
        }

        private static Dictionary<string, string> GetCaptainLevelDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // - Captain Level System - category
                // ========================================
                ["Enable Captain Level System"] =
                "Whether to enable the built-in leveling system.\n" +
                "Automatically disabled if EpicMMOSystem is detected.\n" +
                "[Default: true]",

                ["Max Level"] =
                "Maximum level.\n" +
                "[Default: 100, Range: 1-999]",

                ["Base Level Exp"] =
                "Base experience required to go from level 1 to 2.\n" +
                "[Default: 300]",

                ["Multi Next Level"] =
                "Next-level experience multiplier.\n" +
                "Required experience increases by this multiplier per level.\n" +
                "[Default: 1.05]",

                ["Exp Rate"] =
                "Experience rate.\n" +
                "Applied to all experience gained.\n" +
                "[Default: 1.0, Range: 0.1-10.0]",

                ["Cumulative Exp Mode"] =
                "Cumulative experience mode.\n" +
                "true: Required exp = (previous required exp * multiplier) + base exp\n" +
                "false: Required exp = previous required exp * multiplier\n" +
                "[Default: true]",

                ["Max Level Exp Range"] =
                "Maximum level difference.\n" +
                "Monsters higher than player level + this value give reduced experience.\n" +
                "[Default: 10]",

                ["Min Level Exp Range"] =
                "Minimum level difference.\n" +
                "Monsters lower than player level - this value give reduced experience.\n" +
                "[Default: 10]",

                ["Curve Exp Outside Range"] =
                "Gradually reduce experience outside range.\n" +
                "Experience from monsters outside range gradually decreases with distance.\n" +
                "[Default: false]",

                ["No Exp Past Level Range"] =
                "Zero experience outside range.\n" +
                "Monsters outside range give zero experience.\n" +
                "[Default: false]",

                ["Star Level Exp Multiplier"] =
                "Star level experience multiplier.\n" +
                "Extra experience multiplier per monster star.\n" +
                "Example: 1-star monster = base exp + (max exp * this value * 1)\n" +
                "[Default: 1.5]",

                ["Mob Level Per Star"] =
                "Increase monster level per star.\n" +
                "true: A 1-star monster is displayed as base level + 1.\n" +
                "Example: A Lv.10 monster with 1 star displays as Lv.11\n" +
                "[Default: true]",

                ["Show Exp Popup"] =
                "Show a popup when experience is gained.\n" +
                "[Default: true]",

                ["Show Level Up Effect"] =
                "Show an effect on level up.\n" +
                "[Default: true]",

                ["Show Level HUD"] =
                "Show the level/experience bar HUD.\n" +
                "[Default: true]",

                ["HUD HP Color"] =
                "HP bar color (HTML hex).\n" +
                "[Default: #870000]",

                ["HUD Stamina Color"] =
                "Stamina bar color (HTML hex).\n" +
                "[Default: #986100]",

                ["HUD Eitr Color"] =
                "Eitr bar color (HTML hex).\n" +
                "[Default: #84257C]",

                ["HUD Exp Color"] =
                "Experience bar color (HTML hex).\n" +
                "[Default: #C87820]",

                ["HUD Scale"] =
                "Overall HUD scale.\n" +
                "[Default: 1.0, Range: 0.5-3.0]",

                ["Skill Points Per Level"] =
                "Skill points gained per level.\n" +
                "[Default: 2, Range: 1-10]",

                ["Use Skill Point Based Level"] =
                "Use skill-point-based level calculation.\n" +
                "When EpicMMO is not installed, level is calculated from skill points spent.\n" +
                "Example: 102 points spent / 2 = level 51\n" +
                "[Default: true]",

                ["Points Required Per Level"] =
                "Skill points required per level.\n" +
                "Used for skill-point-based level calculation.\n" +
                "Example: 102 points / 2 = level 51\n" +
                "[Default: 2, Range: 1-10]",

                ["Auto Sync To EpicMMO"] =
                "Auto-sync to EpicMMO.\n" +
                "When EpicMMO is installed, syncs the skill-point-based level to EpicMMO.\n" +
                "[Default: true]",

                ["Exit Button"] =
                "Show exit button (Default: false).\n" +
                "Displays an exit button in the skill tree UI.\n" +
                "Admin only. Server setting is synced to all clients.\n" +
                "[Default: false]",

                ["Enable Level Diff Damage Reduction"] =
                "Enable level-difference attack damage reduction.\n" +
                "When a monster's level is 11 or more above the player's, damage dealt is reduced.\n" +
                "Admin only. Server setting is synced to all clients.\n" +
                "[Default: true]",

                ["Level Diff Tier1 Damage Percent"] =
                "Damage percent for level difference 11-15.\n" +
                "[Default: 40]",

                ["Level Diff Tier2 Damage Percent"] =
                "Damage percent for level difference 16-20.\n" +
                "[Default: 20]",

                ["Level Diff Tier3 Damage Percent"] =
                "Damage percent for level difference 21-30.\n" +
                "[Default: 10]",

                ["Level Diff Tier4 Damage Percent"] =
                "Damage percent for level difference 31+.\n" +
                "[Default: 0]",

                ["Exp Diff Tier1 Percent"] =
                "Exp percent for level difference (beyond Max/Min Level Exp range) 11-15.\n" +
                "When Curve Exp is enabled, exp from monsters outside the range is paid out at this tiered percent.\n" +
                "[Default: 30]",

                ["Exp Diff Tier2 Percent"] =
                "Exp percent for level difference (beyond range) 16-20.\n" +
                "[Default: 25]",

                ["Exp Diff Tier3 Percent"] =
                "Exp percent for level difference (beyond range) 21-30.\n" +
                "[Default: 20]",

                ["Exp Diff Tier4 Percent"] =
                "Exp percent for level difference (beyond range) 31+.\n" +
                "[Default: 10]",

                ["Enable Level Diff Drop Suppression"] =
                "Enable level-difference item drop suppression.\n" +
                "When a monster's level is Drop Suppression Level Diff or more above the player's, it drops no items.\n" +
                "Admin only. Server setting is synced to all clients.\n" +
                "[Default: true]",

                ["Drop Suppression Level Diff"] =
                "Level difference threshold that triggers drop suppression.\n" +
                "[Default: 16]",

                ["Generate JSON Files"] =
                "Whether to generate default JSON files.\n" +
                "When true, default MonsterExp.json and LevelExp.json files\n" +
                "are created in the BepInEx/config/CaptainSkillTree/ folder.\n" +
                "(existing files are not overwritten)\n" +
                "[Default: true]",

                ["Admin Mode"] =
                "Admin mode (Default: false).\n" +
                "On: Ignores all requirements (coins/trophies/points/prerequisites) when learning or resetting skills/jobs.\n" +
                "Only applies to players with admin permission (server host or AdminList).\n" +
                "[Default: false]",

                // ========================================
                // Skill_Tree_Base - 3 missing keys
                // ========================================
                ["EnableLiveConfigSync"] =
                "Enable real-time config sync from the F1 menu.\n" +
                "false (default) = only applied via the skillconfig sync command — recommended to prevent crashes\n" +
                "true = F1 menu changes are sent to the server immediately (1.5s debounce applied)",

                ["My VFX 투명도"] =
                "Opacity control for Mage skill VFX only (0 = fully transparent, 100 = original brightness).\n" +
                "Default: 90 (90% brightness)\n" +
                "Requires a game restart after changing",
            };
        }
    }
}

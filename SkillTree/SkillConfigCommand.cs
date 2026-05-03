using System;
using System.Collections.Generic;
using Jotunn.Entities;
using Jotunn.Managers;

namespace CaptainSkillTree.SkillTree
{
    public class SkillConfigConsoleCommand : ConsoleCommand
    {
        public override string Name => "skillconfig";

        public override string Help => "관리자 Config 서버 동기화 (관리자 전용): skillconfig sync";

        public override bool IsCheat => false;

        public override List<string> CommandOptionList()
        {
            return new List<string> { "sync" };
        }

        public override void Run(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0 || args[0] != "sync")
                {
                    Console.instance.Print("사용법: skillconfig sync");
                    Console.instance.Print("  - 관리자 로컬 Config를 서버에 적용하고 전체 클라이언트에 브로드캐스트합니다.");
                    return;
                }

                if (!SkillAddCommand.IsAdminUser(Player.m_localPlayer))
                {
                    Console.instance.Print("❌ 관리자 권한이 필요합니다.");
                    Plugin.Log.LogWarning("[SkillConfig] 권한 없는 sync 시도");
                    return;
                }

                int count = SkillTreeConfig.SendBatchSyncToServer();
                Console.instance.Print($"✅ skillconfig sync 요청 전송 완료 ({count}개 항목)");
                Plugin.Log.LogInfo($"[SkillConfig] 배치 싱크 전송: {count}개 항목");
            }
            catch (Exception ex)
            {
                Console.instance.Print($"❌ 오류: {ex.Message}");
                Plugin.Log.LogError($"[SkillConfig] 예외 발생: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}

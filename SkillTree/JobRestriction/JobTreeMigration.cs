using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree.JobRestriction
{
    /// <summary>
    /// 직업별 무기 스킬트리 제한 시스템(v1.25.97) 도입 이전부터 플레이하던 캐릭터를 위한 1회성 마이그레이션.
    /// 이미 직업을 가진 캐릭터가 새 규칙과 충돌하는 무기 트리 조합(예: 아처인데 검 액티브도 보유)을
    /// 가지고 있을 수 있으므로, 무기 전문가 트리 8종만 1회 초기화해 재분배할 수 있게 한다.
    /// 직업/생산/공격/방어/속도 스킬은 손대지 않는다.
    /// CaptainMMOPatches.cs의 Game_SpawnPlayer_Postfix와 동일한 훅(Game.SpawnPlayer)을 별도 클래스로 사용한다
    /// (Harmony는 같은 대상에 여러 Postfix가 붙어도 전부 정상 실행되므로 기존 파일 수정 불필요).
    /// </summary>
    [HarmonyPatch(typeof(Game), "SpawnPlayer")]
    public static class JobTreeMigration_Game_SpawnPlayer_Patch
    {
        private const string MigrationDoneKey = "CaptainSkillTree_JobTreeMigration_v1";

        [HarmonyPostfix]
        public static void Postfix()
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                if (player.m_customData.ContainsKey(MigrationDoneKey)) return; // 이미 처리됨

                player.m_customData[MigrationDoneKey] = "1";

                string currentJob = JobTreeRules.GetCurrentJob(player);
                if (currentJob != null)
                {
                    SkillTreeManager.Instance.ResetWeaponTreeSkillLevels(player);
                    player.Message(MessageHud.MessageType.Center, L.Get("job_tree_migration_notice"));
                    Plugin.Log.LogInfo($"[JobTreeMigration] 기존 직업({currentJob}) 보유 캐릭터의 무기 전문가 트리를 1회 초기화했습니다.");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[JobTreeMigration] 마이그레이션 실패: {ex.Message}");
            }
        }
    }
}

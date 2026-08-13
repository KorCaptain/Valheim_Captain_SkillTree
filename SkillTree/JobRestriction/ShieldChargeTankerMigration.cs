using HarmonyLib;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree.JobRestriction
{
    /// <summary>
    /// 방패돌진(mace_Step7_guardian_heart)이 탱커 전용으로 제한되기 전부터 다른 직업으로
    /// 이미 레벨을 투자해 둔 캐릭터를 위한 1회성 마이그레이션. 탱커가 아닌데 레벨이 남아있으면
    /// 해당 스킬만 0으로 초기화해 스킬 포인트를 반납한다(다른 스킬/트로피는 손대지 않음).
    /// JobTreeMigration.cs와 동일하게 Game.SpawnPlayer에 별도 Postfix로 붙는다
    /// (Harmony는 같은 대상에 여러 Postfix가 붙어도 전부 정상 실행되므로 기존 파일 수정 불필요).
    /// </summary>
    [HarmonyPatch(typeof(Game), "SpawnPlayer")]
    public static class ShieldChargeTankerMigration_Game_SpawnPlayer_Patch
    {
        private const string MigrationDoneKey = "CaptainSkillTree_ShieldChargeTankerRefund_v1";
        private const string SkillId = "mace_Step7_guardian_heart";

        [HarmonyPostfix]
        public static void Postfix()
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                if (player.m_customData.ContainsKey(MigrationDoneKey)) return; // 이미 처리됨

                player.m_customData[MigrationDoneKey] = "1";

                int level = SkillTreeManager.Instance?.GetSkillLevel(SkillId) ?? 0;
                if (level <= 0) return; // 배운 적 없으면 대상 아님

                string currentJob = JobTreeRules.GetCurrentJob(player);
                if (currentJob == "Tanker") return; // 탱커는 정상 보유 대상

                player.m_customData[$"CaptainSkillTree_{SkillId}"] = "0";
                player.Message(MessageHud.MessageType.Center, L.Get("shield_charge_tanker_refund_notice"));
                CaptainSkillTree.Gui.ActiveSkillHUD.Instance?.RefreshSlots();

                Plugin.Log.LogInfo($"[ShieldChargeTankerMigration] 탱커가 아닌 캐릭터({currentJob ?? "(직업 없음)"})의 방패돌진 Lv{level} 투자를 초기화하고 스킬 포인트를 반납했습니다.");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[ShieldChargeTankerMigration] 마이그레이션 실패: {ex.Message}");
            }
        }
    }
}

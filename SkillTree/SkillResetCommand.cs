using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Jotunn.Entities;
using Jotunn.Managers;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 스킬 초기화 콘솔 명령어 (Jotunn ConsoleCommand 사용)
    /// 관리자 전용: skillreset <플레이어이름>
    /// </summary>
    public class SkillResetConsoleCommand : ConsoleCommand
    {
        public override string Name => "skillreset";

        public override string Help => "스킬 초기화 (관리자 전용): skillreset <플레이어이름>";

        public override bool IsCheat => true; // 관리자 전용 (devcommands 필요)

        /// <summary>
        /// 자동완성 옵션 제공 - 현재 접속된 플레이어 목록
        /// </summary>
        public override List<string> CommandOptionList()
        {
            var options = new List<string>();

            // 현재 접속된 플레이어 이름 추가
            var allPlayers = Player.GetAllPlayers();
            if (allPlayers != null)
            {
                foreach (var player in allPlayers)
                {
                    if (player != null)
                    {
                        string playerName = player.GetPlayerName();
                        if (!string.IsNullOrEmpty(playerName))
                        {
                            options.Add(playerName);
                        }
                    }
                }
            }

            return options;
        }

        /// <summary>
        /// 명령어 실행
        /// </summary>
        public override void Run(string[] args)
        {
            try
            {
                if (args == null || args.Length < 1)
                {
                    Console.instance.Print("사용법: skillreset <플레이어이름>");
                    Console.instance.Print("예시: skillreset PlayerName");
                    return;
                }

                string targetPlayerName = args[0];
                Plugin.Log.LogInfo($"[스킬 초기화] 콘솔 명령어 실행: skillreset {targetPlayerName}");

                // 콘솔에서 실행하므로 commandExecutor는 null (관리자 권한으로 간주)
                bool success = SkillResetCommand.ResetPlayerSkills(targetPlayerName, null);

                if (success)
                {
                    Console.instance.Print($"✅ '{targetPlayerName}'의 스킬이 성공적으로 초기화되었습니다.");
                }
                else
                {
                    Console.instance.Print($"❌ '{targetPlayerName}'의 스킬 초기화에 실패했습니다.");
                    Console.instance.Print("- 플레이어 이름을 정확히 입력했는지 확인하세요.");
                    Console.instance.Print("- 해당 플레이어가 현재 접속 중인지 확인하세요.");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬 초기화] 콘솔 명령어 실행 중 오류: {ex.Message}");
                Console.instance.Print($"❌ 명령어 실행 중 오류가 발생했습니다: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 관리자 전용 스킬 초기화 로직
    /// </summary>
    public static class SkillResetCommand
    {
        /// <summary>
        /// 특정 플레이어의 스킬을 초기화하는 관리자 명령어
        /// </summary>
        /// <param name="targetPlayerName">초기화할 플레이어명</param>
        /// <param name="commandExecutor">명령어를 실행한 플레이어 (관리자 권한 확인용)</param>
        /// <returns>성공 시 true</returns>
        public static bool ResetPlayerSkills(string targetPlayerName, Player commandExecutor = null)
        {
            try
            {
                Plugin.Log.LogInfo($"[스킬 초기화] 명령어 실행 시작 - 대상: {targetPlayerName}");

                // 1. 관리자 권한 확인
                if (!IsAdminUser(commandExecutor))
                {
                    Plugin.Log.LogWarning($"[스킬 초기화] 관리자 권한 없음 - 실행자: {commandExecutor?.GetPlayerName() ?? "Console"}");
                    return false;
                }

                // 2. 대상 플레이어 찾기
                Player targetPlayer = FindPlayerByName(targetPlayerName);
                if (targetPlayer == null)
                {
                    Plugin.Log.LogWarning($"[스킬 초기화] 플레이어를 찾을 수 없음: {targetPlayerName}");
                    if (commandExecutor != null)
                    {
                        SkillEffect.DrawFloatingText(commandExecutor,
                            $"플레이어 '{targetPlayerName}'을(를) 찾을 수 없습니다", Color.red);
                    }
                    return false;
                }

                // 3. 스킬 데이터 초기화 실행
                bool resetSuccess = ResetSkillData(targetPlayer);

                // 4. 결과 메시지
                if (resetSuccess)
                {
                    Plugin.Log.LogDebug($"[스킬 초기화] 성공 - {targetPlayerName}의 스킬이 초기화되었습니다");

                    // 대상 플레이어에게 알림
                    SkillEffect.DrawFloatingText(targetPlayer,
                        "관리자에 의해 스킬이 초기화되었습니다", Color.yellow);

                    // 실행자에게 확인 메시지
                    if (commandExecutor != null)
                    {
                        SkillEffect.DrawFloatingText(commandExecutor,
                            $"{targetPlayerName}의 스킬이 초기화되었습니다", Color.green);
                    }

                    // 서버 전체에 알림 (선택적)
                    BroadcastResetNotification(targetPlayerName, commandExecutor?.GetPlayerName() ?? "관리자");

                    return true;
                }
                else
                {
                    Plugin.Log.LogError($"[스킬 초기화] 실패 - {targetPlayerName}의 스킬 초기화 중 오류 발생");
                    if (commandExecutor != null)
                    {
                        SkillEffect.DrawFloatingText(commandExecutor,
                            $"{targetPlayerName}의 스킬 초기화에 실패했습니다", Color.red);
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬 초기화] 명령어 실행 중 예외 발생: {ex.Message}");
                Plugin.Log.LogError($"[스킬 초기화] StackTrace: {ex.StackTrace}");

                if (commandExecutor != null)
                {
                    SkillEffect.DrawFloatingText(commandExecutor,
                        "스킬 초기화 중 오류가 발생했습니다", Color.red);
                }
                return false;
            }
        }

        /// <summary>
        /// 관리자 권한 확인
        /// </summary>
        private static bool IsAdminUser(Player player)
        {
            try
            {
                // 1. 콘솔에서 실행한 경우 (player가 null) - 항상 관리자로 간주
                if (player == null)
                {
                    Plugin.Log.LogInfo("[스킬 초기화] 콘솔에서 실행 - 관리자 권한 승인");
                    return true;
                }

                // 2. ZNet이 없는 경우 (싱글플레이어) - 항상 관리자로 간주
                if (ZNet.instance == null)
                {
                    Plugin.Log.LogInfo("[스킬 초기화] 싱글플레이어 모드 - 관리자 권한 승인");
                    return true;
                }

                // 3. 서버 호스트인 경우 - 관리자로 간주
                if (ZNet.instance.IsServer())
                {
                    Plugin.Log.LogInfo("[스킬 초기화] 서버 호스트 - 관리자 권한 승인");
                    return true;
                }

                // 4. Valheim 기본 관리자 시스템 확인
                var adminList = ZNet.instance.GetAdminList();
                if (adminList != null)
                {
                    string playerID = player.GetPlayerID().ToString();
                    string playerName = player.GetPlayerName();

                    Plugin.Log.LogInfo($"[스킬 초기화] 관리자 권한 확인 - 플레이어ID: {playerID}, 이름: {playerName}");

                    if (adminList.Contains(playerID) || adminList.Contains(playerName))
                    {
                        Plugin.Log.LogInfo("[스킬 초기화] 관리자 권한 확인됨");
                        return true;
                    }
                }

                // 5. SynchronizationManager의 Admin 상태 확인 (Jotunn)
                if (SynchronizationManager.Instance?.PlayerIsAdmin == true)
                {
                    Plugin.Log.LogInfo("[스킬 초기화] Jotunn Admin 권한 확인됨");
                    return true;
                }

                Plugin.Log.LogWarning($"[스킬 초기화] 관리자 권한 없음 - {player.GetPlayerName()}");
                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬 초기화] 관리자 권한 확인 중 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 이름으로 플레이어 찾기
        /// </summary>
        private static Player FindPlayerByName(string playerName)
        {
            try
            {
                if (string.IsNullOrEmpty(playerName))
                {
                    Plugin.Log.LogWarning("[스킬 초기화] 플레이어 이름이 비어있음");
                    return null;
                }

                // 1. 로컬 플레이어 확인 (정확히 일치)
                if (Player.m_localPlayer != null &&
                    Player.m_localPlayer.GetPlayerName().Equals(playerName, StringComparison.OrdinalIgnoreCase))
                {
                    Plugin.Log.LogInfo($"[스킬 초기화] 로컬 플레이어 매칭: {playerName}");
                    return Player.m_localPlayer;
                }

                // 2. 모든 플레이어 목록에서 검색
                var allPlayers = Player.GetAllPlayers();
                if (allPlayers != null && allPlayers.Count > 0)
                {
                    foreach (var player in allPlayers)
                    {
                        if (player != null)
                        {
                            string currentPlayerName = player.GetPlayerName();

                            // 대소문자 구분 없이 정확히 일치하는지 확인
                            if (currentPlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase))
                            {
                                Plugin.Log.LogInfo($"[스킬 초기화] 플레이어 발견: {playerName}");
                                return player;
                            }
                        }
                    }

                    // 3. 부분 매칭 시도 (정확한 매칭이 실패한 경우)
                    foreach (var player in allPlayers)
                    {
                        if (player != null)
                        {
                            string currentPlayerName = player.GetPlayerName();

                            if (currentPlayerName.IndexOf(playerName, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                Plugin.Log.LogInfo($"[스킬 초기화] 부분 매칭 플레이어 발견: {playerName} -> {currentPlayerName}");
                                return player;
                            }
                        }
                    }
                }

                Plugin.Log.LogWarning($"[스킬 초기화] 플레이어를 찾을 수 없음: {playerName}");
                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬 초기화] 플레이어 찾기 중 오류: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 실제 스킬 데이터 초기화 로직 - SkillTreeUI.ResetSkillPoints()와 동일한 방식 사용
        /// </summary>
        private static bool ResetSkillData(Player targetPlayer)
        {
            try
            {
                if (targetPlayer == null)
                {
                    Plugin.Log.LogError("[스킬 초기화] 대상 플레이어가 null");
                    return false;
                }

                Plugin.Log.LogDebug($"[스킬 초기화] {targetPlayer.GetPlayerName()}의 스킬 데이터 초기화 시작");

                // SkillTreeUI.ResetSkillPoints()와 동일한 방식으로 초기화
                var skillTreeManager = SkillTreeManager.Instance;
                if (skillTreeManager != null)
                {
                    // 백업 생성 (복구용)
                    CreateSkillBackup(targetPlayer);

                    // SkillTreeManager.ResetAllSkillLevels() 메서드 호출
                    skillTreeManager.ResetAllSkillLevels();

                    Plugin.Log.LogInfo($"[스킬 초기화] SkillTreeManager.ResetAllSkillLevels() 호출 완료");
                }
                else
                {
                    Plugin.Log.LogWarning("[스킬 초기화] SkillTreeManager.Instance가 null");
                    return false;
                }

                // 보너스 스킬 포인트도 초기화 (skilladd로 추가된 포인트)
                int previousBonusPoints = SkillAddCommand.GetBonusSkillPoints(targetPlayer);
                SkillAddCommand.SetBonusSkillPoints(targetPlayer, 0);
                Plugin.Log.LogInfo($"[스킬 초기화] 보너스 포인트 초기화: {previousBonusPoints} -> 0");

                // UI 새로고침 (해당 플레이어가 로컬 플레이어인 경우)
                if (targetPlayer == Player.m_localPlayer)
                {
                    RefreshSkillTreeUI();
                }

                Plugin.Log.LogDebug($"[스킬 초기화] {targetPlayer.GetPlayerName()} 스킬 초기화 완료 (보너스 포인트 포함)");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[스킬 초기화] 스킬 데이터 초기화 중 오류: {ex.Message}");
                Plugin.Log.LogError($"[스킬 초기화] StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// 스킬 백업 생성 (복구용)
        /// </summary>
        private static void CreateSkillBackup(Player player)
        {
            try
            {
                var backupData = new Dictionary<string, string>();
                foreach (var kvp in player.m_customData)
                {
                    if (kvp.Key.StartsWith("CaptainSkillTree_"))
                    {
                        backupData[kvp.Key] = kvp.Value;
                    }
                }

                // 백업 데이터를 로그로 기록 (복구 시 참조용)
                Plugin.Log.LogInfo($"[스킬 백업] {player.GetPlayerName()}의 스킬 백업 생성 - {backupData.Count}개 항목");
                foreach (var kvp in backupData)
                {
                    Plugin.Log.LogDebug($"[스킬 백업] {kvp.Key} = {kvp.Value}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[스킬 백업] 백업 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 스킬트리 UI 새로고침
        /// </summary>
        private static void RefreshSkillTreeUI()
        {
            try
            {
                var pluginType = typeof(Plugin);
                var skillTreeUIField = pluginType.GetField("skillTreeUI",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

                if (skillTreeUIField != null)
                {
                    var skillTreeUI = skillTreeUIField.GetValue(null) as CaptainSkillTree.Gui.SkillTreeUI;
                    if (skillTreeUI != null && skillTreeUI.panel != null && skillTreeUI.panel.activeSelf)
                    {
                        skillTreeUI.RefreshUI();
                        Plugin.Log.LogInfo("[스킬 초기화] 스킬트리 UI 새로고침 성공");
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[스킬 초기화] UI 새로고침 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 서버 전체에 스킬 초기화 알림 브로드캐스트
        /// </summary>
        private static void BroadcastResetNotification(string targetPlayerName, string adminName)
        {
            try
            {
                // 서버의 모든 플레이어에게 알림 (선택적 기능)
                if (ZNet.instance != null && ZNet.instance.IsServer())
                {
                    string message = $"[관리자 알림] {adminName}이(가) {targetPlayerName}의 스킬을 초기화했습니다";

                    // 모든 플레이어에게 채팅 메시지 전송
                    var allPlayers = Player.GetAllPlayers();
                    foreach (var player in allPlayers)
                    {
                        if (player != null)
                        {
                            player.Message(MessageHud.MessageType.Center, message);
                        }
                    }

                    Plugin.Log.LogInfo($"[스킬 초기화] 서버 알림 브로드캐스트: {message}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[스킬 초기화] 브로드캐스트 실패: {ex.Message}");
            }
        }
    }
}

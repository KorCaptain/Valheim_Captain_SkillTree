using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Jotunn.Entities;
using Jotunn.Managers;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 스킬 포인트 추가 명령어 시스템 (Jotunn ConsoleCommand 사용)
    /// 관리자가 게임상에서 /skilladd 숫자 캐릭터이름 명령어로 플레이어에게 스킬 포인트를 추가할 수 있습니다.
    /// 추가된 포인트는 스킬 리셋 후에도 유지됩니다.
    /// </summary>
    public class SkillAddConsoleCommand : ConsoleCommand
    {
        public override string Name => "skilladd";

        public override string Help => "스킬 포인트 추가 (관리자 전용): skilladd <숫자> <캐릭터이름>";

        public override bool IsCheat => true; // 관리자 전용

        /// <summary>
        /// 자동완성 옵션 제공
        /// </summary>
        public override List<string> CommandOptionList()
        {
            var options = new List<string> { "10", "50", "100" };

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
                Plugin.Log.LogInfo("[SkillAddCommand] skilladd 명령어 실행 시작");

                // 인수 검증
                if (args == null || args.Length < 2)
                {
                    Console.instance.Print("사용법: skilladd <숫자> <캐릭터이름>");
                    Console.instance.Print("예시: skilladd 10 PlayerName");
                    Plugin.Log.LogWarning("[SkillAddCommand] 명령어 인수 부족");
                    return;
                }

                // 숫자 파싱
                if (!int.TryParse(args[0], out int pointsToAdd) || pointsToAdd <= 0)
                {
                    Console.instance.Print("오류: 추가할 포인트는 1 이상의 숫자여야 합니다.");
                    Plugin.Log.LogWarning($"[SkillAddCommand] 잘못된 포인트 값: {args[0]}");
                    return;
                }

                string targetPlayerName = args[1];

                // 스킬 포인트 추가 실행
                bool success = SkillAddCommand.AddPlayerSkillPoints(targetPlayerName, pointsToAdd, Player.m_localPlayer);

                if (success)
                {
                    Console.instance.Print($"✅ 성공: {targetPlayerName}에게 스킬 포인트 {pointsToAdd}개를 추가했습니다.");
                    Plugin.Log.LogInfo($"[SkillAddCommand] 스킬 포인트 추가 성공: {targetPlayerName} +{pointsToAdd}");
                }
                else
                {
                    Console.instance.Print($"❌ 실패: {targetPlayerName}에게 스킬 포인트를 추가할 수 없습니다.");
                    Console.instance.Print("- 플레이어가 존재하지 않거나 권한이 없습니다.");
                    Plugin.Log.LogError($"[SkillAddCommand] 스킬 포인트 추가 실패: {targetPlayerName} +{pointsToAdd}");
                }
            }
            catch (Exception ex)
            {
                Console.instance.Print($"❌ 오류: 명령어 실행 중 예외가 발생했습니다: {ex.Message}");
                Plugin.Log.LogError($"[SkillAddCommand] 예외 발생: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }

    /// <summary>
    /// 스킬 포인트 추가 로직 (정적 메서드로 분리)
    /// </summary>
    public static class SkillAddCommand
    {
        /// <summary>
        /// 플레이어에게 스킬 포인트 추가
        /// </summary>
        /// <param name="targetPlayerName">대상 플레이어 이름</param>
        /// <param name="pointsToAdd">추가할 포인트 수</param>
        /// <param name="commandExecutor">명령어 실행자 (권한 확인용)</param>
        /// <returns>성공 시 true</returns>
        public static bool AddPlayerSkillPoints(string targetPlayerName, int pointsToAdd, Player commandExecutor = null)
        {
            try
            {
                Plugin.Log.LogInfo($"[SkillAddCommand] 스킬 포인트 추가 시작: {targetPlayerName} +{pointsToAdd}");

                // 관리자 권한 재확인
                if (commandExecutor != null && !IsAdminUser(commandExecutor))
                {
                    Plugin.Log.LogWarning($"[SkillAddCommand] 권한 없음: {commandExecutor.GetPlayerName()}");
                    return false;
                }

                // 대상 플레이어 찾기
                Player targetPlayer = FindPlayerByName(targetPlayerName);
                if (targetPlayer == null)
                {
                    Plugin.Log.LogWarning($"[SkillAddCommand] 플레이어를 찾을 수 없음: {targetPlayerName}");
                    return false;
                }

                // 현재 보너스 포인트 가져오기
                int currentBonusPoints = GetBonusSkillPoints(targetPlayer);
                int newBonusPoints = currentBonusPoints + pointsToAdd;

                // 보너스 포인트 저장
                SetBonusSkillPoints(targetPlayer, newBonusPoints);

                // 성공 메시지를 플레이어에게 표시
                if (targetPlayer != null)
                {
                    SkillEffect.DrawFloatingText(targetPlayer,
                        $"📈 스킬 포인트 +{pointsToAdd} 획득!",
                        Color.cyan);

                    // 현재 총 보너스 포인트도 표시
                    SkillEffect.DrawFloatingText(targetPlayer,
                        $"💎 보너스 포인트 총 {newBonusPoints}개",
                        Color.yellow);
                }

                // 스킬트리 UI 업데이트 (대상 플레이어가 로컬 플레이어인 경우)
                if (targetPlayer == Player.m_localPlayer)
                {
                    RefreshSkillTreeUI();
                }

                Plugin.Log.LogInfo($"[SkillAddCommand] 스킬 포인트 추가 완료: {targetPlayerName} (기존: {currentBonusPoints}, 추가: {pointsToAdd}, 총합: {newBonusPoints})");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillAddCommand] 스킬 포인트 추가 실패: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// 관리자 권한 확인
        /// </summary>
        public static bool IsAdminUser(Player player)
        {
            try
            {
                if (player == null)
                {
                    Plugin.Log.LogWarning("[SkillAddCommand] 플레이어가 null입니다.");
                    return false;
                }

                // 1. 콘솔에서 실행하는 경우 (로컬 플레이어가 없음)
                if (Player.m_localPlayer == null)
                {
                    Plugin.Log.LogInfo("[SkillAddCommand] 콘솔에서 실행됨 - 관리자 권한 부여");
                    return true;
                }

                // 2. 서버 호스트인 경우
                if (ZNet.instance != null && ZNet.instance.IsServer())
                {
                    Plugin.Log.LogInfo("[SkillAddCommand] 서버 호스트 - 관리자 권한 부여");
                    return true;
                }

                // 3. 관리자 목록에 등록된 경우
                if (ZNet.instance != null)
                {
                    var adminList = ZNet.instance.GetAdminList();
                    if (adminList != null)
                    {
                        string playerID = player.GetPlayerID().ToString();
                        bool isAdmin = adminList.Contains(playerID);
                        Plugin.Log.LogInfo($"[SkillAddCommand] 관리자 목록 확인: {player.GetPlayerName()} ({playerID}) - {(isAdmin ? "관리자" : "일반 사용자")}");
                        return isAdmin;
                    }
                }

                // 4. SynchronizationManager의 Admin 상태 확인 (Jotunn)
                if (SynchronizationManager.Instance?.PlayerIsAdmin == true)
                {
                    Plugin.Log.LogInfo("[SkillAddCommand] Jotunn Admin 권한 확인됨");
                    return true;
                }

                Plugin.Log.LogWarning($"[SkillAddCommand] 관리자 권한 없음: {player.GetPlayerName()}");
                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillAddCommand] 관리자 권한 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 이름으로 플레이어 찾기
        /// </summary>
        public static Player FindPlayerByName(string playerName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Plugin.Log.LogWarning("[SkillAddCommand] 플레이어 이름이 비어있습니다.");
                    return null;
                }

                // 1. 로컬 플레이어 확인
                if (Player.m_localPlayer != null &&
                    string.Equals(Player.m_localPlayer.GetPlayerName(), playerName, StringComparison.OrdinalIgnoreCase))
                {
                    Plugin.Log.LogInfo($"[SkillAddCommand] 로컬 플레이어 발견: {playerName}");
                    return Player.m_localPlayer;
                }

                // 2. 모든 플레이어 검색
                var allPlayers = Player.GetAllPlayers();
                if (allPlayers != null)
                {
                    foreach (var player in allPlayers)
                    {
                        if (player != null &&
                            string.Equals(player.GetPlayerName(), playerName, StringComparison.OrdinalIgnoreCase))
                        {
                            Plugin.Log.LogInfo($"[SkillAddCommand] 플레이어 발견: {playerName}");
                            return player;
                        }
                    }
                }

                Plugin.Log.LogWarning($"[SkillAddCommand] 플레이어를 찾을 수 없음: {playerName}");
                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillAddCommand] 플레이어 검색 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 플레이어의 보너스 스킬 포인트 가져오기
        /// </summary>
        public static int GetBonusSkillPoints(Player player)
        {
            try
            {
                if (player == null || player.m_customData == null)
                {
                    return 0;
                }

                string key = "CaptainSkillTree_BonusPoints";
                if (player.m_customData.TryGetValue(key, out var str) &&
                    int.TryParse(str, out int bonusPoints))
                {
                    return Math.Max(0, bonusPoints);
                }

                return 0;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillAddCommand] 보너스 포인트 가져오기 실패: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 플레이어의 보너스 스킬 포인트 설정
        /// </summary>
        public static void SetBonusSkillPoints(Player player, int bonusPoints)
        {
            try
            {
                if (player == null || player.m_customData == null)
                {
                    Plugin.Log.LogWarning("[SkillAddCommand] 플레이어 또는 customData가 null입니다.");
                    return;
                }

                bonusPoints = Math.Max(0, bonusPoints);
                string key = "CaptainSkillTree_BonusPoints";
                player.m_customData[key] = bonusPoints.ToString();

                Plugin.Log.LogInfo($"[SkillAddCommand] 보너스 포인트 설정: {player.GetPlayerName()} = {bonusPoints}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillAddCommand] 보너스 포인트 설정 실패: {ex.Message}");
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
                        Plugin.Log.LogInfo($"[SkillAddCommand] 스킬트리 UI 포인트 표시 업데이트 완료");
                    }
                }
            }
            catch (Exception uiEx)
            {
                Plugin.Log.LogWarning($"[SkillAddCommand] 스킬트리 UI 업데이트 실패: {uiEx.Message}");
            }
        }

        /// <summary>
        /// 모든 플레이어의 보너스 포인트 초기화 (디버그용)
        /// </summary>
        public static void ResetAllBonusPoints()
        {
            try
            {
                Plugin.Log.LogInfo("[SkillAddCommand] 모든 보너스 포인트 초기화 시작");

                if (Player.m_localPlayer != null)
                {
                    SetBonusSkillPoints(Player.m_localPlayer, 0);
                }

                var allPlayers = Player.GetAllPlayers();
                if (allPlayers != null)
                {
                    foreach (var player in allPlayers)
                    {
                        if (player != null)
                        {
                            SetBonusSkillPoints(player, 0);
                        }
                    }
                }

                Plugin.Log.LogInfo("[SkillAddCommand] 모든 보너스 포인트 초기화 완료");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[SkillAddCommand] 보너스 포인트 초기화 실패: {ex.Message}");
            }
        }
    }
}

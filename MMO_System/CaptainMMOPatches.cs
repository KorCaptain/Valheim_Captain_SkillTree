using System;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// Captain MMO Patches
    /// 자체 레벨 시스템을 위한 Harmony 패치
    /// 몬스터 처치 시 경험치 획득 등
    /// </summary>
    [HarmonyPatch]
    public static class CaptainMMOPatches
    {
        #region === Monster Death Patch ===

        /// <summary>
        /// 몬스터 사망 시 경험치 획득 패치
        /// 플레이어 또는 파티원이 죽인 몬스터만 경험치 획득
        /// </summary>
        [HarmonyPatch(typeof(Character), "OnDeath")]
        [HarmonyPostfix]
        public static void OnMonsterDeath(Character __instance)
        {
            try
            {
                // EpicMMO 사용 중이면 스킵 (EpicMMO가 처리)
                if (CaptainMMOBridge.UseEpicMMO) return;

                // 자체 시스템 비활성화 시 스킵
                if (!CaptainLevelConfig.EnableCaptainLevel.Value) return;

                // 플레이어 사망은 처리하지 않음
                if (__instance.IsPlayer()) return;

                // 로컬 플레이어 확인
                var player = Player.m_localPlayer;
                if (player == null) return;

                // 길들여진 동물/동료는 경험치 없음
                if (__instance.IsTamed()) return;

                // ★ 핵심: 플레이어 또는 파티원이 죽인 몬스터인지 확인
                if (!IsKilledByPlayerOrParty(__instance, player))
                {
                    return; // 플레이어/파티가 죽인 게 아니면 경험치 없음
                }

                // 몬스터 이름 (프리팹명 또는 m_name)
                string monsterName = GetMonsterName(__instance);
                if (string.IsNullOrEmpty(monsterName)) return;

                // 경험치 계산 및 지급
                int resultExp = CalculateMonsterExp(__instance, monsterName, player);
                if (resultExp > 0)
                {
                    CaptainLevelSystem.Instance.AddExp(resultExp);
                    Plugin.Log.LogDebug($"[CaptainMMOPatches] {monsterName} 처치 - 경험치 +{resultExp}");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] OnMonsterDeath 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어 또는 파티원이 죽인 몬스터인지 확인
        /// Reflection으로 m_lastHit 접근
        /// </summary>
        private static bool IsKilledByPlayerOrParty(Character monster, Player localPlayer)
        {
            try
            {
                // Reflection으로 m_lastHit 접근
                var lastHitField = typeof(Character).GetField("m_lastHit",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (lastHitField != null)
                {
                    var lastHit = lastHitField.GetValue(monster) as HitData;
                    if (lastHit != null)
                    {
                        var attacker = lastHit.GetAttacker();
                        if (attacker != null)
                        {
                            // 로컬 플레이어가 공격자인 경우
                            if (attacker == localPlayer)
                                return true;

                            // 플레이어의 길들인 동물이 공격자인 경우
                            if (attacker.IsTamed())
                            {
                                // 길들인 동물의 주인이 플레이어인지 확인
                                var tamedCharacter = attacker as Character;
                                if (tamedCharacter != null)
                                {
                                    float distanceToPlayer = Vector3.Distance(localPlayer.transform.position, tamedCharacter.transform.position);
                                    if (distanceToPlayer <= 50f) // 플레이어 근처 길들인 동물
                                        return true;
                                }
                            }

                            // 다른 플레이어가 공격자인 경우 (파티 시스템)
                            if (attacker.IsPlayer())
                            {
                                // 파티원 확인: 일정 거리 내 플레이어
                                float partyRange = 50f; // 파티 경험치 공유 범위
                                float distance = Vector3.Distance(localPlayer.transform.position, attacker.transform.position);
                                if (distance <= partyRange)
                                    return true;
                            }
                        }
                    }
                }

                // Fallback: 플레이어가 최근에 이 몬스터를 공격했는지 확인
                // 플레이어와 매우 가까운 거리 (전투 범위)
                float maxExpRange = 15f; // 경험치 획득 최대 거리 (좁은 범위)
                float distanceToMonster = Vector3.Distance(localPlayer.transform.position, monster.transform.position);

                if (distanceToMonster <= maxExpRange)
                {
                    // 플레이어가 무기를 들고 있거나 최근 공격했을 가능성
                    var currentWeapon = localPlayer.GetCurrentWeapon();
                    if (currentWeapon != null)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainMMOPatches] IsKilledByPlayerOrParty 오류: {ex.Message}");
            }

            return false;
        }

        private static string GetMonsterName(Character character)
        {
            string prefabName = Utils.GetPrefabName(character.gameObject);
            if (!string.IsNullOrEmpty(prefabName))
                return $"{prefabName}(Clone)";

            if (!string.IsNullOrEmpty(character.m_name))
                return character.m_name;

            return null;
        }

        private static int CalculateMonsterExp(Character monster, string monsterName, Player player)
        {
            int baseExp = CaptainMonsterExp.GetExp(monsterName);
            int maxExp = CaptainMonsterExp.GetMaxExp(monsterName);
            int monsterLevel = CaptainMonsterExp.GetLevel(monsterName);
            int starLevel = monster.GetLevel() - 1;

            float lvlBonus = CaptainLevelConfig.ExpForLvlMonster.Value;
            int resultExp = baseExp + (int)(maxExp * lvlBonus * starLevel);

            int playerLevel = CaptainLevelSystem.Instance.Level;
            int maxRange = playerLevel + CaptainLevelConfig.MaxLevelExp.Value;
            int minRange = playerLevel - CaptainLevelConfig.MinLevelExp.Value;

            if (monsterLevel > maxRange || monsterLevel < minRange)
            {
                if (CaptainLevelConfig.NoExpPastLevel.Value)
                {
                    resultExp = 0;
                }
                else if (CaptainLevelConfig.CurveExp.Value)
                {
                    int diff = monsterLevel > maxRange ? monsterLevel - maxRange : minRange - monsterLevel;
                    resultExp = resultExp / (diff + 1);
                }
            }

            return Mathf.Max(0, resultExp);
        }

        #endregion

        #region === Player Patches ===

        [HarmonyPatch(typeof(Player), "Awake")]
        [HarmonyPostfix]
        public static void Player_Awake_Postfix(Player __instance)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;
                if (!CaptainMMOBridge.IsInitialized)
                    CaptainMMOBridge.Initialize();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] Player_Awake 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(Player), "Load")]
        [HarmonyPostfix]
        public static void Player_Load_Postfix(Player __instance)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;
                CaptainMMOBridge.OnPlayerLoad();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] Player_Load 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(Player), "Save")]
        [HarmonyPostfix]
        public static void Player_Save_Postfix(Player __instance)
        {
            try
            {
                if (__instance != Player.m_localPlayer) return;
                CaptainMMOBridge.OnPlayerSave();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] Player_Save 오류: {ex.Message}");
            }
        }

        #endregion

        #region === Game Events ===

        [HarmonyPatch(typeof(Game), "SavePlayerProfile")]
        [HarmonyPostfix]
        public static void Game_SavePlayerProfile_Postfix()
        {
            try
            {
                CaptainMMOBridge.OnPlayerSave();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] Game_SavePlayerProfile 오류: {ex.Message}");
            }
        }

        [HarmonyPatch(typeof(Game), "SpawnPlayer")]
        [HarmonyPostfix]
        public static void Game_SpawnPlayer_Postfix()
        {
            try
            {
                CaptainMMOBridge.ShowStartupNotification();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] Game_SpawnPlayer 알림 오류: {ex.Message}");
            }
        }

        #endregion

        #region === Console Commands ===

        /// <summary>
        /// 콘솔 명령어 등록
        /// </summary>
        [HarmonyPatch(typeof(Terminal), "InitTerminal")]
        [HarmonyPostfix]
        public static void Terminal_InitTerminal_Postfix()
        {
            // captainlvl [level] - 레벨 설정/확인
            new Terminal.ConsoleCommand("captainlvl", "[level] - Set/view Captain level (cheat)",
                args =>
                {
                    if (args.Length >= 2 && int.TryParse(args[1], out int level))
                    {
                        if (!CaptainMMOBridge.UseEpicMMO)
                        {
                            CaptainLevelSystem.Instance.SetLevel(level);
                            args.Context.AddString($"Captain Level set to {level}");
                        }
                        else
                        {
                            args.Context.AddString("Cannot set level: EpicMMO is active");
                        }
                    }
                    else
                    {
                        args.Context.AddString($"Current Level: {CaptainMMOBridge.GetLevel()}");
                        args.Context.AddString("Usage: captainlvl [level]");
                    }
                }, isCheat: true);

            // captainexp [amount] - 경험치 추가/확인
            new Terminal.ConsoleCommand("captainexp", "[amount] - Add exp to Captain system (cheat)",
                args =>
                {
                    if (args.Length >= 2 && int.TryParse(args[1], out int exp))
                    {
                        if (!CaptainMMOBridge.UseEpicMMO)
                        {
                            CaptainLevelSystem.Instance.AddExp(exp);
                            args.Context.AddString($"Added {exp} EXP");
                        }
                        else
                        {
                            args.Context.AddString("Cannot add exp: EpicMMO is active");
                        }
                    }
                    else
                    {
                        args.Context.AddString($"Current Exp: {CaptainMMOBridge.GetCurrentExp():N0}");
                        args.Context.AddString($"Exp to Next: {CaptainMMOBridge.GetExpToNextLevel():N0}");
                        args.Context.AddString("Usage: captainexp [amount]");
                    }
                }, isCheat: true);

            // captainstatus - 상태 출력
            new Terminal.ConsoleCommand("captainstatus", "Show Captain level system status",
                args =>
                {
                    CaptainMMOBridge.LogStatus();
                    args.Context.AddString($"Active System: {CaptainMMOBridge.ActiveSystem}");
                    args.Context.AddString($"Level: {CaptainMMOBridge.GetLevel()}");
                    args.Context.AddString($"Exp: {CaptainMMOBridge.GetCurrentExp():N0}/{CaptainMMOBridge.GetExpToNextLevel():N0}");
                    args.Context.AddString($"Progress: {CaptainMMOBridge.GetLevelProgress() * 100:F1}%");
                    args.Context.AddString($"Total Exp: {CaptainMMOBridge.GetTotalExp():N0}");
                });

            // captainreset - 레벨/경험치 리셋
            new Terminal.ConsoleCommand("captainreset", "Reset Captain level to 1 (cheat)",
                args =>
                {
                    if (!CaptainMMOBridge.UseEpicMMO)
                    {
                        CaptainLevelSystem.Instance.ResetData();
                        args.Context.AddString("Captain Level System reset to Level 1");
                    }
                    else
                    {
                        args.Context.AddString("Cannot reset: EpicMMO is active");
                    }
                }, isCheat: true);

            // captainmigrate - 마이그레이션 강제 실행
            new Terminal.ConsoleCommand("captainmigrate", "Force migrate data (cheat)",
                args =>
                {
                    if (CaptainMMOBridge.UseEpicMMO)
                    {
                        args.Context.AddString("Starting migration to EpicMMO...");
                        bool success = CaptainMMOBridge.ForceMigrationToEpicMMO();
                        if (success)
                        {
                            args.Context.AddString("Migration to EpicMMO completed!");
                            args.Context.AddString($"Current EpicMMO Level: {CaptainMMOBridge.GetLevel()}");
                        }
                        else
                        {
                            args.Context.AddString("Migration failed. Check logs.");
                        }
                    }
                    else
                    {
                        args.Context.AddString("Starting migration to CaptainLevel...");
                        bool success = CaptainMMOBridge.ForceMigrationToCaptain();
                        if (success)
                        {
                            args.Context.AddString("Migration to CaptainLevel completed!");
                            args.Context.AddString($"Current Captain Level: {CaptainMMOBridge.GetLevel()}");
                        }
                        else
                        {
                            args.Context.AddString("Migration failed. Check logs.");
                        }
                    }
                }, isCheat: true);

            // captainmigratestatus - 마이그레이션 상태 확인
            new Terminal.ConsoleCommand("captainmigratestatus", "Show migration status",
                args =>
                {
                    CaptainMMOBridge.LogMigrationStatus();
                    args.Context.AddString($"Active System: {CaptainMMOBridge.ActiveSystem}");
                    args.Context.AddString($"Migration Completed: {CaptainMMOBridge.MigrationCompleted}");

                    var player = Player.m_localPlayer;
                    if (player != null)
                    {
                        if (player.m_customData.TryGetValue("CaptainSkillTree_Level", out var lvl))
                            args.Context.AddString($"Captain Level Data: Lv.{lvl}");
                        if (player.m_customData.TryGetValue("CaptainSkillTree_TotalExp", out var exp))
                            args.Context.AddString($"Captain TotalExp Data: {exp}");
                        if (player.m_customData.TryGetValue("CaptainSkillTree_EpicMMOBackup_Level", out var epicLvl))
                            args.Context.AddString($"EpicMMO Backup Level: Lv.{epicLvl}");
                        if (player.m_customData.TryGetValue("CaptainSkillTree_EpicMMOBackup_TotalExp", out var epicExp))
                            args.Context.AddString($"EpicMMO Backup TotalExp: {epicExp}");
                    }
                });

            // captainhelp - 명령어 도움말
            new Terminal.ConsoleCommand("captainhelp", "Show Captain level system commands",
                args =>
                {
                    args.Context.AddString("=== Captain Level System Commands ===");
                    args.Context.AddString("captainlvl [level] - Set/view level");
                    args.Context.AddString("captainexp [amount] - Add exp");
                    args.Context.AddString("captainstatus - Show current status");
                    args.Context.AddString("captainreset - Reset to level 1");
                    args.Context.AddString("captainmigrate - Force migration");
                    args.Context.AddString("captainmigratestatus - Show migration status");
                    args.Context.AddString("=====================================");
                });

            Plugin.Log.LogDebug("[CaptainMMOPatches] Captain Level 콘솔 명령어 등록 완료");
        }

        #endregion
    }
}

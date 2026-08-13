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
        internal static bool IsKilledByPlayerOrParty(Character monster, Player localPlayer)
        {
            try
            {
                // Reflection으로 m_lastHit 접근
                var lastHitField = typeof(Character).GetField("m_lastHit",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                Character resolvedAttacker = null;
                if (lastHitField != null)
                {
                    var lastHit = lastHitField.GetValue(monster) as HitData;
                    resolvedAttacker = lastHit?.GetAttacker();
                }

                if (resolvedAttacker != null && EvaluateAttacker(resolvedAttacker, localPlayer))
                    return true;

                // Fallback: 도트(독/화상 등) 사망 시 m_lastHit에는 공격자 정보가 없는 HitData만
                // 남으므로, 최근 유효 타격 시점에 기록해둔 공격자로 재판정한다.
                if (CaptainLastAttackerCache.TryGetRecent(monster, 15f, out var cachedAttacker)
                    && EvaluateAttacker(cachedAttacker, localPlayer))
                    return true;

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

        /// <summary>주어진 공격자가 로컬 플레이어 본인 / 그의 길들인 동물 / 파티원인지 판정한다.</summary>
        private static bool EvaluateAttacker(Character attacker, Player localPlayer)
        {
            // 로컬 플레이어가 공격자인 경우
            if (attacker == localPlayer)
                return true;

            // 플레이어의 길들인 동물이 공격자인 경우
            if (attacker.IsTamed())
            {
                float distanceToPlayer = Vector3.Distance(localPlayer.transform.position, attacker.transform.position);
                if (distanceToPlayer <= 50f) // 플레이어 근처 길들인 동물
                    return true;
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

            float lvlBonus = CaptainLevelConfig.ExpForLvlMonsterValue;
            int resultExp = baseExp + (int)(maxExp * lvlBonus * starLevel);

            int playerLevel = CaptainLevelSystem.Instance.Level;
            int maxRange = playerLevel + CaptainLevelConfig.MaxLevelExpValue;
            int minRange = playerLevel - CaptainLevelConfig.MinLevelExpValue;

            if (monsterLevel > maxRange || monsterLevel < minRange)
            {
                if (CaptainLevelConfig.NoExpPastLevel.Value)
                {
                    resultExp = 0;
                }
                else if (CaptainLevelConfig.CurveExp.Value)
                {
                    int diff = monsterLevel > maxRange ? monsterLevel - maxRange : minRange - monsterLevel;
                    resultExp = (int)(resultExp * GetExpDiffMultiplier(diff));
                }
            }

            return Mathf.Max(0, resultExp);
        }

        #endregion

        #region === Last Attacker Cache (도트 킬 판정용) ===

        /// <summary>
        /// 공격자 정보가 있는 모든 타격을 몬스터별로 캐시해둔다.
        /// 독/화상 등 도트 틱은 공격자 없는 HitData로 ApplyDamage를 호출하므로,
        /// 도트 사망 시 IsKilledByPlayerOrParty가 여기 캐시된 마지막 유효 공격자로 fallback 판정한다.
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        [HarmonyPostfix]
        public static void RecordAttacker_ApplyDamage_Postfix(Character __instance, HitData hit)
        {
            var attacker = hit?.GetAttacker();
            if (attacker != null)
                CaptainLastAttackerCache.Record(__instance, attacker);
        }

        #endregion

        #region === Level Diff Attack Damage Reduction (LV 차이 공격옵션) ===

        /// <summary>
        /// LV 차이 공격옵션: 몬스터 레벨이 플레이어보다 크게 높을 때 플레이어가 가하는 피해를 감소시킴
        /// 경험치 감소 로직과는 독립적인 PvE 밸런스 옵션 (서버싱크 Config)
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        [HarmonyPriority(Priority.Low)]
        [HarmonyPrefix]
        public static void LevelDiffAttack_ApplyDamage_Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (!CaptainLevelConfig.EnableLevelDiffDamageReductionValue) return;

                // 공격자가 플레이어인지 확인
                var attacker = hit.GetAttacker();
                if (attacker == null || !(attacker is Player)) return;

                // 피격자가 몬스터인지 확인 (플레이어 본인 / 길들인 동물 제외)
                if (__instance.IsPlayer()) return;
                if (__instance.IsTamed()) return;

                // 몬스터 레벨 조회 (미등록 몬스터 = 0 = "???" -> 스킵)
                string monsterName = GetMonsterName(__instance);
                if (string.IsNullOrEmpty(monsterName)) return;

                int monsterLevel = CaptainMonsterExp.GetLevel(monsterName);
                if (monsterLevel <= 0) return;

                int playerLevel = CaptainMMOBridge.GetLevel();
                int diff = monsterLevel - playerLevel;
                if (diff < 11) return; // 11 미만 차이는 정상 데미지 (100%)

                float multiplier = GetLevelDiffDamageMultiplier(diff);
                hit.m_damage.Modify(multiplier);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] LevelDiffAttack_ApplyDamage_Prefix 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 레벨 차이 구간별 데미지 배율 반환 (Config 값 기반, 0.0~1.0)
        /// 구간 경계값(11/16/21/31)은 고정 상수 - Config로 노출하지 않음
        /// </summary>
        private static float GetLevelDiffDamageMultiplier(int levelDiff)
        {
            if (levelDiff <= 15) return CaptainLevelConfig.LevelDiffTier1DamagePercentValue / 100f;   // 11~15
            if (levelDiff <= 20) return CaptainLevelConfig.LevelDiffTier2DamagePercentValue / 100f;   // 16~20
            if (levelDiff <= 30) return CaptainLevelConfig.LevelDiffTier3DamagePercentValue / 100f;   // 21~30
            return CaptainLevelConfig.LevelDiffTier4DamagePercentValue / 100f;                        // 31+
        }

        /// <summary>
        /// 레벨 차이(범위 초과분) 구간별 경험치 배율 반환 (Config 값 기반, 0.0~1.0)
        /// 구간 경계값(10/16/21/31)은 데미지 감소와 동일 - Config로 노출하지 않음
        /// CaptainMMOPatches.CalculateMonsterExp / CaptainPartyExp.ApplyLevelCurve 공용
        /// </summary>
        internal static float GetExpDiffMultiplier(int levelDiff)
        {
            if (levelDiff <= 15) return CaptainLevelConfig.ExpDiffTier1PercentValue / 100f;   // 11~15
            if (levelDiff <= 20) return CaptainLevelConfig.ExpDiffTier2PercentValue / 100f;   // 16~20
            if (levelDiff <= 30) return CaptainLevelConfig.ExpDiffTier3PercentValue / 100f;   // 21~30
            return CaptainLevelConfig.ExpDiffTier4PercentValue / 100f;                        // 31+
        }

        #endregion

        #region === Level Diff Drop Suppression (레벨 차이 아이템 드랍 억제) ===

        /// <summary>
        /// 레벨 차이 아이템 드랍 억제: 몬스터 레벨이 플레이어보다 DropSuppressionLevelDiff 이상 높으면
        /// 아이템 드랍 자체를 막음. 데미지/경험치 감소와는 독립적인 PvE 밸런스 옵션 (서버싱크 Config)
        /// </summary>
        [HarmonyPatch(typeof(CharacterDrop), "OnDeath")]
        [HarmonyPriority(Priority.Low)]
        [HarmonyPrefix]
        public static bool LevelDiffDrop_OnDeath_Prefix(CharacterDrop __instance)
        {
            try
            {
                if (!CaptainLevelConfig.EnableLevelDiffDropSuppressionValue) return true;

                var monster = Traverse.Create(__instance).Field("m_character").GetValue<Character>();
                if (monster == null || monster.IsPlayer() || monster.IsTamed()) return true;

                var localPlayer = Player.m_localPlayer;
                if (localPlayer == null) return true;
                if (!IsKilledByPlayerOrParty(monster, localPlayer)) return true;

                string monsterName = GetMonsterName(monster);
                if (string.IsNullOrEmpty(monsterName)) return true;

                int monsterLevel = CaptainMonsterExp.GetLevel(monsterName);
                if (monsterLevel <= 0) return true;

                int playerLevel = CaptainMMOBridge.GetLevel();
                int diff = monsterLevel - playerLevel;
                bool suppressed = diff >= CaptainLevelConfig.DropSuppressionLevelDiffValue;

                Plugin.Log?.LogInfo($"[CaptainMMOPatches] LevelDiffDrop 판정: monster={monsterName} monsterLv={monsterLevel} playerLv={playerLevel} diff={diff} threshold={CaptainLevelConfig.DropSuppressionLevelDiffValue} => {(suppressed ? "억제됨(드랍 없음)" : "드랍 허용")}");

                if (!suppressed) return true;

                return false; // 드랍 억제 - 원본 OnDeath(드랍 스폰) 실행 안 함
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] LevelDiffDrop_OnDeath_Prefix 오류: {ex.Message}");
                return true; // 오류 시 안전하게 원래 드랍 허용
            }
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

        [HarmonyPatch(typeof(Game), "SpawnPlayer")]
        [HarmonyPostfix]
        public static void Game_SpawnPlayer_Postfix()
        {
            try
            {
                LevelSyncManager.Instance.Reset();

                var player = Player.m_localPlayer;
                if (player != null)
                    CaptainJobNameHud.RestoreJobTitleFromCustomData(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[CaptainMMOPatches] Game_SpawnPlayer 오류: {ex.Message}");
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

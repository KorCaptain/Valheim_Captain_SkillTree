using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.Mods
{
    internal static class PlantEasilyBridge
    {
        internal const string GUID = "advize.PlantEasily";

        public static bool IsInstalled { get; private set; }
        private static bool _initialized;

        // PlantEasily의 modEnabled ConfigEntry<bool> — 발견되면 per-frame 토글 방식 사용
        private static ConfigEntry<bool> _enabledEntry;
        // 토글 중 여부 (Prefix에서 껐을 때만 Postfix에서 복원)
        private static bool _toggleActive;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            IsInstalled = Chainloader.PluginInfos.ContainsKey(GUID);
            if (!IsInstalled) return;

            try
            {
                FindEnabledEntry();
                ApplyProducerRestriction();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[PlantEasilyBridge] 패치 실패 (무시): {ex.Message}");
            }
        }

        private static void FindEnabledEntry()
        {
            var pluginInstance = Chainloader.PluginInfos[GUID].Instance;
            var config = pluginInstance.Config;

            foreach (var kvp in config)
            {
                if (kvp.Value is ConfigEntry<bool> boolEntry)
                {
                    var key = kvp.Key.Key.ToLower();
                    if (key == "isenabled" || key == "enabled" || key == "enable" ||
                        key == "modenabled" || key.Contains("enable") || key.Contains("active"))
                    {
                        _enabledEntry = boolEntry;
                        return;
                    }
                }
            }

            Plugin.Log.LogWarning("[PlantEasilyBridge] 활성화 Config 미발견 — 전체 패치 메서드 직접 탐색으로 전환");
        }

        private static void ApplyProducerRestriction()
        {
            var harmony = new Harmony("CaptainSkillTree.PlantEasilyGate");

            if (_enabledEntry != null)
            {
                // ── 방식 A: PlantEasily의 modEnabled를 비제작전문가 호출 동안만 false로 토글 ──
                // UpdatePlacementGhost : 그리드 미리보기 차단
                // PlacePiece          : 배치 실행 차단
                var pre  = new HarmonyMethod(typeof(PlantEasilyBridge), nameof(TogglePrefix));
                var post = new HarmonyMethod(typeof(PlantEasilyBridge), nameof(TogglePostfix));

                harmony.Patch(AccessTools.Method(typeof(Player), "UpdatePlacementGhost"),
                    prefix: pre, postfix: post);
                harmony.Patch(AccessTools.Method(typeof(Player), "PlacePiece"),
                    prefix: pre, postfix: post);

            }
            else
            {
                // ── 방식 B: PlantEasily가 패치한 모든 게임 메서드에 직접 Prefix 추가 ──
                // Transpiler 포함 — PlantEasily가 패치한 메서드 자체에 prefix 주입
                var prefix = new HarmonyMethod(typeof(PlantEasilyBridge), nameof(ProducerOnlyPrefix));
                int count  = 0;

                foreach (var method in Harmony.GetAllPatchedMethods())
                {
                    var info = Harmony.GetPatchInfo(method);
                    bool owned = false;
                    foreach (var p in info.Prefixes)   if (p.owner == GUID) { owned = true; break; }
                    if (!owned) foreach (var p in info.Postfixes)  if (p.owner == GUID) { owned = true; break; }
                    if (!owned) foreach (var p in info.Transpilers) if (p.owner == GUID) { owned = true; break; }

                    if (!owned) continue;

                    // UpdatePlacementGhost는 비제작전문가도 바닐라 고스트가 필요하므로 건너뜀
                    // PlacePiece(실제 심기)만 차단
                    if (method.Name == "UpdatePlacementGhost") continue;

                    harmony.Patch(method, prefix: prefix);
                    Plugin.Log.LogInfo($"[PlantEasilyBridge] 방식 B — {method.Name} 제한 적용");
                    count++;
                }

                Plugin.Log.LogInfo($"[PlantEasilyBridge] 방식 B 완료 ({count}개 메서드)");
            }
        }

        // ── 방식 A 전용: Prefix — 비제작전문가일 때 PlantEasily 일시 비활성화 ──
        static void TogglePrefix(Player __instance)
        {
            _toggleActive = false;
            if (!SkillTreeConfig.PlantEasilyProducerLimit) return;
            if (__instance != Player.m_localPlayer) return;
            if (_enabledEntry == null || !_enabledEntry.Value) return;
            if (ProducerSkills.IsProducer(__instance)) return;

            _enabledEntry.Value = false;
            _toggleActive = true;
        }

        // ── 방식 A 전용: Postfix — 원래 값으로 복원 ──
        static void TogglePostfix()
        {
            if (!_toggleActive) return;
            _enabledEntry.Value = true;
            _toggleActive = false;
        }

        // ── 방식 B 전용: 비제작전문가면 해당 메서드 실행 자체를 차단 ──
        private static bool ProducerOnlyPrefix()
        {
            if (!SkillTreeConfig.PlantEasilyProducerLimit) return true;
            return ProducerSkills.IsProducer(Player.m_localPlayer);
        }

        public static bool CanUse()
        {
            if (!IsInstalled) return true;
            if (!SkillTreeConfig.PlantEasilyProducerLimit) return true;
            return ProducerSkills.IsProducer(Player.m_localPlayer);
        }
    }
}

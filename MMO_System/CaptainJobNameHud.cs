using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CaptainSkillTree.Localization;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// 직업 전직 시 캐릭터 오버헤드 이름 앞에 직업명을 색상과 함께 표시
    /// EnemyHud 패치로 플레이어 이름을 "[직업명] 캐릭터명" 형태로 표시
    /// ZDO를 통해 멀티플레이어 동기화
    ///
    /// 성능 최적화:
    /// - 로컬 플레이어: 전직 확정 시 이벤트 기반으로 즉시 1회 갱신 (폴링 없음)
    /// - 원격 플레이어: 2초 간격 폴링 + dirty-flag 캐시 (변경 없으면 텍스트 업데이트 skip)
    /// - ZNetView, TextMeshProUGUI 캐시로 리플렉션/GetComponent 반복 호출 제거
    /// - 플레이어 퇴장 시 stale 캐시 자동 정리 (메모리 누수 방지)
    /// </summary>
    [HarmonyPatch]
    public static class CaptainJobNameHud
    {
        // ZDO 키 (네트워크 동기화용)
        private const string ZDO_JOB_KEY = "CaptainJobTitle";

        // 직업 ID → 색상 코드
        private static readonly Dictionary<string, string> JobColors = new Dictionary<string, string>
        {
            { "Berserker", "#FF4040" },
            { "Tanker",    "#00CCFF" },
            { "Rogue",     "#CC44FF" },
            { "Archer",    "#44DD44" },
            { "Mage",      "#4499FF" },
            { "Paladin",   "#FFD700" },
            { "Producer",  "#FF8800" },
        };

        // 직업 ID → 영문 표시명 (멀티플레이어: 상대방 언어 무관하게 통일)
        private static readonly Dictionary<string, string> JobDisplayNames = new Dictionary<string, string>
        {
            { "Berserker", "Berserker" },
            { "Tanker",    "Tanker" },
            { "Rogue",     "Rogue" },
            { "Archer",    "Archer" },
            { "Mage",      "Mage" },
            { "Paladin",   "Paladin" },
            { "Producer",  "Producer" },
        };

        // 원본 이름 캐시 (UpdateHuds에서 덮어쓰기 방지)
        private static readonly Dictionary<ZDOID, string> _originalNames = new Dictionary<ZDOID, string>();

        // 마지막으로 적용한 직업 ID 캐시 (dirty-flag: 변경 없으면 텍스트 갱신 skip)
        private static readonly Dictionary<ZDOID, string> _lastKnownJobId = new Dictionary<ZDOID, string>();

        // TextMeshProUGUI 캐시 (매 폴링마다 GetComponent 호출 방지)
        private static readonly Dictionary<ZDOID, TextMeshProUGUI> _cachedNameText = new Dictionary<ZDOID, TextMeshProUGUI>();

        // ZNetView 캐시 (매 폴링마다 Traverse 리플렉션 호출 방지)
        private static readonly Dictionary<ZDOID, ZNetView> _cachedNView = new Dictionary<ZDOID, ZNetView>();

        // 원격 플레이어 폴링 간격 (직업은 전직 시에만 바뀌므로 2초면 충분)
        private static float _lastRemoteUpdateTime = 0f;
        private const float RemoteUpdateInterval = 2f;

        // Reflection 캐시
        private static FieldInfo _m_hudsField;
        private static FieldInfo _m_guiField;
        private static FieldInfo _m_nviewField;

        private static void InitReflection()
        {
            if (_m_hudsField == null)
            {
                _m_hudsField = typeof(EnemyHud).GetField("m_huds",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            }
            if (_m_nviewField == null)
            {
                _m_nviewField = typeof(Character).GetField("m_nview",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        private static GameObject GetHudGui(object hudData)
        {
            if (hudData == null) return null;
            if (_m_guiField == null)
            {
                _m_guiField = hudData.GetType().GetField("m_gui",
                    BindingFlags.Public | BindingFlags.Instance);
            }
            return _m_guiField?.GetValue(hudData) as GameObject;
        }

        private static ZNetView GetCachedNView(Character c, ZDOID zdoid)
        {
            if (_cachedNView.TryGetValue(zdoid, out ZNetView cached) && cached != null)
                return cached;

            InitReflection();
            var nview = _m_nviewField?.GetValue(c) as ZNetView;
            if (nview != null && zdoid != ZDOID.None)
                _cachedNView[zdoid] = nview;
            return nview;
        }

        // ───────────────────────────────────────────────────
        //  Public API (다른 파일에서 호출)
        // ───────────────────────────────────────────────────

        /// <summary>
        /// 직업 전직 시 ZDO에 직업 ID 저장 후 로컬 플레이어 HUD 즉시 갱신
        /// SkillTreeUI.ShowJobChangeWorldMessage에서 호출
        /// 로컬 플레이어는 이 호출만으로 처리 완료 (UpdateHuds 폴링 불필요)
        /// </summary>
        public static void SyncJobTitleToZDO(Player player, string jobId)
        {
            try
            {
                if (player == null) return;
                if (!JobColors.ContainsKey(jobId)) return;

                var nview = _m_nviewField != null
                    ? _m_nviewField.GetValue(player) as ZNetView
                    : Traverse.Create(player).Field("m_nview").GetValue<ZNetView>();
                if (nview == null || !nview.IsOwner()) return;
                var zdo = nview.GetZDO();
                if (zdo == null) return;

                zdo.Set(ZDO_JOB_KEY, jobId);
                Plugin.Log.LogDebug($"[CaptainJobNameHud] ZDO 직업 동기화: {jobId}");

                // 로컬 플레이어는 전직 확정 시 즉시 1회 갱신 (폴링 불필요)
                ApplyJobPrefixImmediate(player, nview, jobId);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainJobNameHud] ZDO 동기화 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어 스폰/로드 시 m_customData에서 현재 직업을 읽어 ZDO 재동기화 및 HUD 즉시 갱신
        /// CaptainMMOPatches.Player_OnSpawned_Postfix에서 호출
        /// </summary>
        public static void RestoreJobTitleFromCustomData(Player player)
        {
            try
            {
                if (player == null) return;

                InitReflection();
                var nview = _m_nviewField?.GetValue(player) as ZNetView;
                if (nview == null || !nview.IsOwner()) return;
                var zdo = nview.GetZDO();
                if (zdo == null) return;

                foreach (var jobId in JobColors.Keys)
                {
                    string key = $"CaptainSkillTree_{jobId}";
                    if (player.m_customData.TryGetValue(key, out string val)
                        && int.TryParse(val, out int level)
                        && level >= 1)
                    {
                        zdo.Set(ZDO_JOB_KEY, jobId);
                        Plugin.Log.LogDebug($"[CaptainJobNameHud] 스폰 시 직업 복구: {jobId}");

                        // 스폰 시에도 즉시 갱신
                        ApplyJobPrefixImmediate(player, nview, jobId);
                        return;
                    }
                }

                // 직업 없으면 ZDO 키 초기화
                zdo.Set(ZDO_JOB_KEY, "");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainJobNameHud] 직업 복구 실패: {ex.Message}");
            }
        }

        // ───────────────────────────────────────────────────
        //  Harmony 패치
        // ───────────────────────────────────────────────────

        /// <summary>
        /// EnemyHud.ShowHud Postfix - 플레이어 HUD 최초 생성 시 캐시 등록 및 직업 prefix 적용
        /// </summary>
        [HarmonyPatch(typeof(EnemyHud), "ShowHud")]
        [HarmonyPostfix]
        public static void ShowHud_Postfix(EnemyHud __instance, Character c)
        {
            try
            {
                if (!c.IsPlayer()) return;

                InitReflection();
                if (_m_hudsField == null) return;

                var hudsDict = _m_hudsField.GetValue(__instance) as IDictionary;
                if (hudsDict == null || !hudsDict.Contains(c)) return;

                var hudData = hudsDict[c];
                GameObject hudGui = GetHudGui(hudData);
                if (hudGui == null) return;

                GameObject nameObj = hudGui.transform.Find("Name")?.gameObject;
                if (nameObj == null) return;

                var tmpText = nameObj.GetComponent<TextMeshProUGUI>();
                if (tmpText == null) return;

                var zdoid = GetZdoid(c);
                if (zdoid == ZDOID.None) return;

                // 캐시 등록
                if (!_originalNames.ContainsKey(zdoid))
                    _originalNames[zdoid] = tmpText.text;
                _cachedNameText[zdoid] = tmpText;

                // 로컬 플레이어는 이미 SyncJobTitleToZDO/RestoreJobTitleFromCustomData에서 처리됨
                // 원격 플레이어만 여기서 초기 적용
                if (c != Player.m_localPlayer)
                    ApplyJobPrefix(c, tmpText, zdoid);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainJobNameHud] ShowHud 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// EnemyHud.UpdateHuds Postfix - 원격 플레이어만 2초 간격으로 직업 prefix 갱신
        /// dirty-flag 캐시로 변경 없는 경우 텍스트 업데이트 완전 skip
        /// 퇴장한 플레이어의 stale 캐시 자동 정리
        /// </summary>
        [HarmonyPatch(typeof(EnemyHud), "UpdateHuds")]
        [HarmonyPostfix]
        public static void UpdateHuds_Postfix(EnemyHud __instance)
        {
            try
            {
                // 2초 간격 제어 (직업은 전직 시에만 바뀌므로 잦은 갱신 불필요)
                if (Time.time - _lastRemoteUpdateTime < RemoteUpdateInterval) return;
                _lastRemoteUpdateTime = Time.time;

                InitReflection();
                if (_m_hudsField == null) return;

                var hudsDict = _m_hudsField.GetValue(__instance) as IDictionary;
                if (hudsDict == null) return;

                // 현재 활성 ZDOID 수집 (stale 캐시 정리용)
                var activeZdoids = new HashSet<ZDOID>();

                foreach (DictionaryEntry entry in hudsDict)
                {
                    Character c = entry.Key as Character;
                    if (c == null || !c.IsPlayer()) continue;

                    // 로컬 플레이어는 이벤트 기반으로 처리하므로 skip
                    if (c == Player.m_localPlayer) continue;

                    var zdoid = GetZdoid(c);
                    if (zdoid == ZDOID.None) continue;

                    activeZdoids.Add(zdoid);

                    // TextMeshProUGUI 캐시 조회 (없으면 GetComponent)
                    if (!_cachedNameText.TryGetValue(zdoid, out TextMeshProUGUI tmpText) || tmpText == null)
                    {
                        GameObject hudGui = GetHudGui(entry.Value);
                        if (hudGui == null) continue;
                        GameObject nameObj = hudGui.transform.Find("Name")?.gameObject;
                        if (nameObj == null) continue;
                        tmpText = nameObj.GetComponent<TextMeshProUGUI>();
                        if (tmpText == null) continue;
                        _cachedNameText[zdoid] = tmpText;
                    }

                    if (!_originalNames.ContainsKey(zdoid))
                        _originalNames[zdoid] = StripJobPrefix(tmpText.text);

                    ApplyJobPrefix(c, tmpText, zdoid);
                }

                // stale 캐시 정리 (퇴장한 플레이어 메모리 해제)
                CleanupStaleEntries(activeZdoids);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainJobNameHud] UpdateHuds 오류: {ex.Message}");
            }
        }

        // ───────────────────────────────────────────────────
        //  내부 헬퍼
        // ───────────────────────────────────────────────────

        /// <summary>
        /// 로컬 플레이어 전직 확정 시 즉시 HUD 갱신 (EnemyHud.instance를 통해 직접 접근)
        /// </summary>
        private static void ApplyJobPrefixImmediate(Player player, ZNetView nview, string jobId)
        {
            try
            {
                var hudInstance = EnemyHud.instance;
                if (hudInstance == null) return;

                InitReflection();
                if (_m_hudsField == null) return;

                var hudsDict = _m_hudsField.GetValue(hudInstance) as IDictionary;
                if (hudsDict == null || !hudsDict.Contains(player)) return;

                var hudData = hudsDict[player];
                GameObject hudGui = GetHudGui(hudData);
                if (hudGui == null) return;

                GameObject nameObj = hudGui.transform.Find("Name")?.gameObject;
                if (nameObj == null) return;

                var tmpText = nameObj.GetComponent<TextMeshProUGUI>();
                if (tmpText == null) return;

                var zdoid = nview.GetZDO()?.m_uid ?? ZDOID.None;
                if (zdoid == ZDOID.None) return;

                // 캐시 갱신
                _cachedNameText[zdoid] = tmpText;
                _cachedNView[zdoid] = nview;

                if (!_originalNames.ContainsKey(zdoid))
                    _originalNames[zdoid] = StripJobPrefix(tmpText.text);

                string originalName = _originalNames[zdoid];

                if (!string.IsNullOrEmpty(jobId) && JobColors.TryGetValue(jobId, out string color))
                {
                    string displayName = JobDisplayNames.TryGetValue(jobId, out string dn) ? dn : jobId;
                    tmpText.text = $"<color=white>[</color><color={color}>{displayName}</color><color=white>]</color> {originalName}";
                    tmpText.richText = true;
                }
                else
                {
                    tmpText.text = originalName;
                }

                // dirty-flag 캐시 업데이트
                _lastKnownJobId[zdoid] = jobId ?? "";
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainJobNameHud] 즉시 갱신 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// ZDO에서 직업 읽어 이름 텍스트에 색상 prefix 적용
        /// dirty-flag 캐시로 변경 없으면 즉시 return (텍스트 업데이트 skip)
        /// </summary>
        private static void ApplyJobPrefix(Character c, TextMeshProUGUI tmpText, ZDOID zdoid)
        {
            var nview = GetCachedNView(c, zdoid);
            string jobId = nview?.GetZDO()?.GetString(ZDO_JOB_KEY, "") ?? "";

            // dirty-flag: 직업 변경 없으면 텍스트 업데이트 완전 skip
            if (_lastKnownJobId.TryGetValue(zdoid, out string lastJob) && lastJob == jobId)
                return;

            _lastKnownJobId[zdoid] = jobId;

            string originalName = _originalNames.TryGetValue(zdoid, out string cached)
                ? cached
                : tmpText.text;

            if (!string.IsNullOrEmpty(jobId) && JobColors.TryGetValue(jobId, out string color))
            {
                string displayName = JobDisplayNames.TryGetValue(jobId, out string dn) ? dn : jobId;
                tmpText.text = $"<color=white>[</color><color={color}>{displayName}</color><color=white>]</color> {originalName}";
                tmpText.richText = true;
            }
            else
            {
                tmpText.text = originalName;
            }
        }

        /// <summary>
        /// 퇴장한 플레이어의 stale 캐시 정리 (메모리 누수 방지)
        /// </summary>
        private static void CleanupStaleEntries(HashSet<ZDOID> activeZdoids)
        {
            var toRemove = new List<ZDOID>();
            foreach (var zdoid in _originalNames.Keys)
            {
                if (!activeZdoids.Contains(zdoid))
                    toRemove.Add(zdoid);
            }

            foreach (var zdoid in toRemove)
            {
                _originalNames.Remove(zdoid);
                _lastKnownJobId.Remove(zdoid);
                _cachedNameText.Remove(zdoid);
                _cachedNView.Remove(zdoid);
            }

            if (toRemove.Count > 0)
                Plugin.Log.LogDebug($"[CaptainJobNameHud] 캐시 정리: {toRemove.Count}명 퇴장");
        }

        /// <summary>
        /// Character의 ZDOID 반환 (캐시된 ZNetView 활용)
        /// </summary>
        private static ZDOID GetZdoid(Character c)
        {
            InitReflection();
            var nview = _m_nviewField?.GetValue(c) as ZNetView;
            return nview?.GetZDO()?.m_uid ?? ZDOID.None;
        }

        /// <summary>
        /// 이미 prefix가 붙은 텍스트에서 원본 이름 추출 (color 태그 제거)
        /// </summary>
        private static string StripJobPrefix(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            const string marker = "</color> ";
            int lastClose = text.LastIndexOf(marker, StringComparison.Ordinal);
            if (lastClose >= 0)
                return text.Substring(lastClose + marker.Length);
            return text;
        }
    }
}

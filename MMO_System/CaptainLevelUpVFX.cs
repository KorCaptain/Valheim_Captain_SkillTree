using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMO epicasset에서 LevelUpVFX 프리팹을 로드하여 캐싱,
    /// 레벨업 시 플레이어 본(Spine2)에 재생하는 클래스.
    /// EpicMMO PlayerFVX.levelUp() 동일 패턴.
    /// </summary>
    [HarmonyPatch]
    public static class CaptainLevelUpVFX
    {
        private const string SPINE_PATH = "Visual/Armature/Hips/Spine/Spine1/Spine2";
        private static readonly Vector3 VFX_SCALE = new Vector3(0.01352632f, 0.01352632f, 0.01352632f);

        private static GameObject _prefab;
        private static bool _loaded;

        // ──────────────────────────────────────────
        // ZNetScene.Awake: epicasset에서 LevelUpVFX 로드 & 캐싱
        // ──────────────────────────────────────────

        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        [HarmonyPostfix]
        public static void ZNetScene_Awake_Postfix()
        {
            if (_loaded) return;

            // 이미 로드된 epicasset 번들 재사용 (EpicMMO가 먼저 로드했을 수 있음)
            AssetBundle bundle = null;
            System.IO.Stream stream = null;
            bool ownedBundle = false;

            foreach (var b in AssetBundle.GetAllLoadedAssetBundles())
            {
                if (b.Contains("LevelUpVFX") || b.Contains("EpicHudPanelCanvas")) { bundle = b; break; }
            }

            if (bundle == null)
            {
                stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("CaptainSkillTree.asset.Resources.epicasset");
                if (stream == null)
                {
                    Plugin.Log.LogWarning("[CaptainLevelUpVFX] epicasset 스트림 없음");
                    return;
                }
                bundle = AssetBundle.LoadFromStream(stream);
                if (bundle == null)
                {
                    Plugin.Log.LogWarning("[CaptainLevelUpVFX] epicasset 번들 로드 실패");
                    stream.Dispose();
                    return;
                }
                ownedBundle = true;
            }

            _prefab = bundle.LoadAsset<GameObject>("LevelUpVFX");
            if (_prefab != null)
                Object.DontDestroyOnLoad(_prefab);

            if (ownedBundle) { bundle.Unload(false); stream.Dispose(); }

            _loaded = true;
            if (_prefab == null)
                Plugin.Log.LogWarning("[CaptainLevelUpVFX] LevelUpVFX 프리팹을 번들에서 찾을 수 없음");
        }

        // ──────────────────────────────────────────
        // 재생 API
        // ──────────────────────────────────────────

        /// <summary>
        /// 로컬 플레이어 Spine2 본에 LevelUpVFX 재생 (EpicMMO 동일 패턴)
        /// </summary>
        public static void Play()
        {
            if (_prefab == null) return;

            try
            {
                var player = Player.m_localPlayer;
                if (player == null) return;

                var spine = player.transform.Find(SPINE_PATH);
                if (spine == null)
                {
                    // fallback: 플레이어 위치 기반 재생
                    var vfx = Object.Instantiate(_prefab, player.transform.position + Vector3.up, Quaternion.identity);
                    vfx.transform.localScale = VFX_SCALE;
                    Object.Destroy(vfx, 5f);
                    return;
                }

                var go = Object.Instantiate(_prefab, spine);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale    = VFX_SCALE;
                // 파티클 완료 후 정리 (ZNetScene 프리팹이 아니므로 Destroy 안전)
                Object.Destroy(go, 5f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogDebug($"[CaptainLevelUpVFX] Play 오류: {ex.Message}");
            }
        }
    }
}

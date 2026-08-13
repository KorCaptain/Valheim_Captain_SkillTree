using System.Reflection;
using UnityEngine;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// epicasset AssetBundle을 세션 동안 한 번만 로드하여 CaptainLevelUpVFX와
    /// CaptainHudBuilder가 공유. 각자 독립적으로 LoadFromStream을 호출하면서
    /// 생기던 "AssetBundle already loaded" 충돌을 방지하기 위한 캐시.
    /// </summary>
    internal static class EpicAssetBundleCache
    {
        private static AssetBundle _bundle;
        private static bool _attempted;

        internal static AssetBundle Get()
        {
            if (_bundle != null) return _bundle;
            if (_attempted) return null;
            _attempted = true;

            foreach (var b in AssetBundle.GetAllLoadedAssetBundles())
            {
                if (b.Contains("LevelUpVFX") || b.Contains("EpicHudPanelCanvas"))
                {
                    _bundle = b;
                    return _bundle;
                }
            }

            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("CaptainSkillTree.asset.Resources.epicasset"))
            {
                if (stream == null)
                {
                    Plugin.Log.LogWarning("[EpicAssetBundleCache] epicasset 스트림 없음");
                    return null;
                }
                _bundle = AssetBundle.LoadFromStream(stream);
            }

            if (_bundle == null)
                Plugin.Log.LogWarning("[EpicAssetBundleCache] epicasset 번들 로드 실패");

            return _bundle;
        }
    }
}

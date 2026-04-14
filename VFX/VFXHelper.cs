using UnityEngine;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// VFX 헬퍼. VFXManager를 래핑.
    /// 커스텀 VFX (area_* 등): CaptainSkillTree 커스텀 번들.
    /// 발헤임 기본 VFX (fx_*, vfx_*, sfx_*): 항상 작동.
    /// </summary>
    public static class BossVFXHelper
    {
        /// <summary>VFX 또는 SFX 단독 재생</summary>
        public static void PlayAt(string prefabName, Vector3 position, float duration = 5f)
        {
            if (string.IsNullOrEmpty(prefabName)) return;
            VFXManager.PlayVFXMultiplayer(prefabName, "", position, Quaternion.identity, duration);
        }

        /// <summary>VFX + SFX 동시 재생</summary>
        public static void PlayAt(string vfxName, string sfxName,
            Vector3 position, float duration = 5f)
        {
            VFXManager.PlayVFXMultiplayer(vfxName ?? "", sfxName ?? "",
                position, Quaternion.identity, duration);
        }
    }
}

using System;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.VFX
{
    public static class VFXManager
    {
        public static bool VFX_ENABLED = true;

        public static GameObject GetCachedPrefab(string vfxName)
        {
            return SimpleVFX.GetPrefab(vfxName);
        }

        public static GameObject GetVFXPrefab(string prefabName)
        {
            return SimpleVFX.GetPrefab(prefabName);
        }

        public static GameObject PlayVFXFollowPlayer(Player player, string vfxName, float duration = 5f, Vector3 localScale = default)
        {
            if (!VFX_ENABLED || player == null) return null;
            return SimpleVFX.PlayOnPlayer(player, vfxName, duration);
        }

        public static GameObject PlayVFXAtPosition(string vfxName, Vector3 position, float duration = 5f, float scale = 1f)
        {
            if (!VFX_ENABLED) return null;
            return SimpleVFX.Play(vfxName, position, duration);
        }

        public static GameObject PlayVFXOnPlayer(string vfxName, Player player, float duration = 5f, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;
            if (offset == default) offset = Vector3.up * 1f;
            return SimpleVFX.Play(vfxName, player.transform.position + offset, duration);
        }

        public static void PlaySound(string soundName, Vector3 position, float duration = 5f)
        {
            if (string.IsNullOrEmpty(soundName)) return;
            SimpleVFX.Play(soundName, position, duration);
        }

        public static void PlayVFXMultiplayer(string vfxName, string soundName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f, float scale = 1f)
        {
            if (!VFX_ENABLED) return;

            if (SimpleVFX.IsCustomVFXName(vfxName))
            {
                if (rotation != default && rotation != Quaternion.identity)
                    SimpleVFX.PlayWithRotation(vfxName, soundName, position, rotation, destroyAfter);
                else
                    SimpleVFX.PlayWithSound(vfxName, soundName, position, destroyAfter);
                return;
            }

            SimpleVFX.PlayVanillaNetworked(vfxName, position, rotation);
            if (!string.IsNullOrEmpty(soundName))
                SimpleVFX.Play(soundName, position, destroyAfter);
        }

        public static GameObject PlayVFXAttachedToPlayer(Player player, string vfxName,
            string soundName, float duration, Vector3 offset = default)
        {
            if (!VFX_ENABLED || player == null) return null;

            Vector3? localOffset = offset == default ? null : (Vector3?)offset;
            var vfxObj = SimpleVFX.IsCustomVFXName(vfxName)
                ? SimpleVFX.PlayOnPlayer(player, vfxName, duration, localOffset)
                : SimpleVFX.PlayVanillaOnPlayerNetworked(player, vfxName, localOffset);

            if (!string.IsNullOrEmpty(soundName))
                SimpleVFX.Play(soundName, player.transform.position, duration);

            return vfxObj;
        }

        public static void PlayVFX(string prefabName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f)
        {
            if (!VFX_ENABLED) return;
            SimpleVFX.Play(prefabName, position, destroyAfter);
        }

        public static void PlayVFXNetworked(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED) return;

            if (SimpleVFX.IsCustomVFXName(prefabName))
                SimpleVFX.PlayWithSound(prefabName, soundName, position, destroyAfter);
            else
            {
                SimpleVFX.PlayVanillaNetworked(prefabName, position, rotation);
                if (!string.IsNullOrEmpty(soundName))
                    SimpleVFX.Play(soundName, position, destroyAfter);
            }
        }

        public static void PlayVFXNetworkedOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED || player == null) return;

            if (SimpleVFX.IsCustomVFXName(prefabName))
                SimpleVFX.PlayWithSound(prefabName, soundName, player.transform.position + Vector3.up * 1f, destroyAfter);
            else
            {
                SimpleVFX.PlayVanillaOnPlayerNetworked(player, prefabName, Vector3.up * 1f);
                if (!string.IsNullOrEmpty(soundName))
                    SimpleVFX.Play(soundName, player.transform.position + Vector3.up * 1f, destroyAfter);
            }
        }

        public static void PlayVFXOptimized(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f, bool isNetworked = false)
        {
            if (!VFX_ENABLED) return;
            PlayVFXNetworked(prefabName, position, rotation, destroyAfter, soundName, soundVolume);
        }

        public static void PlayVFXHybrid(string prefabName, Vector3 position, Quaternion rotation = default,
            float destroyAfter = 5f, string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED) return;
            PlayVFXNetworked(prefabName, position, rotation, destroyAfter, soundName, soundVolume);
        }

        public static void PlayVFXHybridOnPlayer(string prefabName, Player player, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f)
        {
            if (!VFX_ENABLED || player == null) return;
            PlayVFXNetworkedOnPlayer(prefabName, player, destroyAfter, soundName, soundVolume);
        }

        public static void Initialize() { }
        public static void CachePrefabs() { }
        public static void InitializeVFXRPC() { }
        public static void ClearCache() { }
        public static void ClearFailureList() { }
        public static void RetryFailedPrefabs() { }
        public static void LogStatus() { }
        public static void LogPrefabDetails(string prefabName) { }
        public static void DiagnoseCriticalPrefabs() { }
        public static void DiagnoseHybridSystem() { }
        public static void DeepDebugBuff01() { }
        public static VFXPlaybackMethod GetOptimalPlaybackMethod(string prefabName) => VFXPlaybackMethod.Registry;
    }

    public enum VFXPlaybackMethod
    {
        Registry,
        RPC,
        Fallback
    }

    [HarmonyPatch(typeof(ShieldDomeParticleColor), "Start")]
    internal static class Patch_ShieldDomeParticleColor_Start
    {
        static Exception Finalizer(Exception __exception) => null;
    }
}
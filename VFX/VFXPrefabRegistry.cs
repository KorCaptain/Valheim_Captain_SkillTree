using UnityEngine;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// VFX 프리팹 등록 시스템 - 비활성화됨 (무한 로딩 방지)
    /// </summary>
    public static class VFXPrefabRegistry
    {
        public static bool IsRegistrationComplete => false;

        public static bool IsVFXRegistered(string vfxName) => false;

        public static GameObject GetRegisteredVFX(string vfxName) => null;

        public static bool PlayRegisteredVFX(string vfxName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f) => false;

        public static void DiagnoseRegistrationStatus() { }

        internal static void RegisterVFXPrefabs(ZNetScene znetScene) { }

        internal static void ClearRegistration() { }
    }
}

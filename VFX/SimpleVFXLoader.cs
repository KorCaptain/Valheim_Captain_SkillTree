using UnityEngine;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// 단순 VFX 로더 - 비활성화됨 (무한 로딩 방지)
    /// </summary>
    public static class SimpleVFXLoader
    {
        public static bool IsRegistered => false;

        public static GameObject GetVFX(string name) => null;

        public static GameObject PlayVFX(string name, Vector3 position,
            Quaternion rotation, float destroyAfter = 5f) => null;

        internal static void RegisterToZNetScene(ZNetScene znetScene) { }
    }
}

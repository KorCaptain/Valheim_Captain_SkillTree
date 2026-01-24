using UnityEngine;

namespace CaptainSkillTree.VFX
{
    /// <summary>
    /// VFX 네트워크 동기화 - 비활성화됨 (무한 로딩 방지)
    /// </summary>
    public class VFXNetworkSync : MonoBehaviour
    {
        private static VFXNetworkSync instance;

        public static VFXNetworkSync Instance => instance;

        public static void PlayVFXNetworked(string vfxName, Vector3 position,
            Quaternion rotation = default, float destroyAfter = 5f,
            string soundName = "", float soundVolume = 0.8f, bool forceBatch = false)
        {
            // 비활성화
        }

        public static bool IsExternalVFX(string vfxName) => false;

        public static void Cleanup()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
                instance = null;
            }
        }

        public static void DiagnoseNetworkStatus() { }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}

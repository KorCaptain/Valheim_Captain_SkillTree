using UnityEngine;

namespace CaptainSkillTree.VFX
{
    public class VFXNetworkController : MonoBehaviour
    {
        private ZNetView m_nview;
        private float m_destroyTime;
        private bool m_isOwner;

        void Awake()
        {
            m_nview = GetComponent<ZNetView>();
        }

        void Start()
        {
            if (m_nview == null || !m_nview.IsValid()) return;

            string vfxName = m_nview.GetZDO().GetString("vfx_name");
            float duration = m_nview.GetZDO().GetFloat("vfx_duration", 5f);

            m_isOwner = m_nview.IsOwner();
            if (m_isOwner)
                m_destroyTime = Time.time + duration;

            if (string.IsNullOrEmpty(vfxName)) return;

            // 각 클라이언트에서 로컬로 VFX 재생 (ZNetScene host가 보장하는 위치에서)
            var prefab = SimpleVFX.GetPrefab(vfxName);
            if (prefab == null) return;

            var vfxObj = Object.Instantiate(prefab, transform.position, Quaternion.identity);
            if (vfxObj != null)
                Object.Destroy(vfxObj, duration + 0.5f);
        }

        // Owner가 Instantiate 직후 호출 — ZDO에 VFX 정보 기록
        public void InitAsOwner(string vfxName, float duration)
        {
            if (m_nview == null || !m_nview.IsOwner()) return;
            m_nview.GetZDO().Set("vfx_name", vfxName);
            m_nview.GetZDO().Set("vfx_duration", duration);
            m_destroyTime = Time.time + duration;
            m_isOwner = true;
        }

        void Update()
        {
            if (!m_isOwner || m_nview == null || !m_nview.IsValid()) return;
            if (Time.time > m_destroyTime)
                ZNetScene.instance.Destroy(gameObject);
        }
    }
}

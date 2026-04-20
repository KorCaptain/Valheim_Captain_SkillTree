using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.VFX
{
    // ZNetScene 초기화 시 "CaptainVFXHost" 네트워크 프리팹 등록
    // Orbs.cs 패턴: ZNetScene에 등록된 프리팹은 모든 클라이언트에 자동 동기화됨
    [HarmonyPatch(typeof(ZNetScene), "Awake")]
    internal static class Patch_ZNetScene_RegisterVFXHost
    {
        private static GameObject _hostPrefab;
        private static readonly FieldInfo _namedPrefabsField =
            typeof(ZNetScene).GetField("m_namedPrefabs", BindingFlags.NonPublic | BindingFlags.Instance);

        internal static GameObject HostPrefab => _hostPrefab;

        static void Postfix(ZNetScene __instance)
        {
            if (_hostPrefab != null) return;

            _hostPrefab = new GameObject("CaptainVFXHost");
            _hostPrefab.SetActive(false); // Awake/Start 차단 상태로 등록

            var nv = _hostPrefab.AddComponent<ZNetView>();
            nv.m_persistent = false;
            nv.m_type = ZDO.ObjectType.Default;

            _hostPrefab.AddComponent<VFXNetworkController>();

            Object.DontDestroyOnLoad(_hostPrefab);

            __instance.m_prefabs.Add(_hostPrefab);

            // m_namedPrefabs는 private 필드 — 리플렉션으로 접근 (PrefabRegistry 동일 패턴)
            var namedPrefabs = _namedPrefabsField?.GetValue(__instance) as Dictionary<int, GameObject>;
            if (namedPrefabs != null)
            {
                int hash = "CaptainVFXHost".GetStableHashCode();
                if (!namedPrefabs.ContainsKey(hash))
                    namedPrefabs[hash] = _hostPrefab;
            }

            Plugin.Log?.LogInfo("[VFXNetworkRegistrar] CaptainVFXHost 등록 완료");
        }
    }
}

using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace CaptainSkillTree.VFX
{
    /// <summary>TotalVFX VFX 재생 방식</summary>
    public enum TotalVFXPlaybackMethod { Registry, RPC, Fallback }

    /// <summary>
    /// 발헤임 기본 VFX 위치 추적 컴포넌트.
    /// 플레이어 자식 부착 대신 매 프레임 위치를 갱신하여 무한 로딩 방지.
    /// </summary>
    public class TotalVFXFollowBehaviour : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;

        void Update()
        {
            if (Target == null) { UnityEngine.Object.Destroy(gameObject); return; }
            transform.position = Target.position + Offset;
        }
    }

    /// <summary>
    /// VFX 밝기·투명도 감소 컴포넌트.
    /// LateUpdate에서 1회 적용 후 자동 제거.
    /// Material: _TintColor / _Color / _BaseColor + EmissionColor
    /// ParticleSystem: startColor + startSize 감소
    /// Light: intensity 감소
    /// </summary>
    public class TotalVFXDimmerBehaviour : MonoBehaviour
    {
        public float factor = 0.5f;

        private void LateUpdate()
        {
            try { Apply(); }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[TotalVFXDimmer] 적용 실패: {ex.Message}");
            }
            Destroy(this);
        }

        private void Apply()
        {
            var colorProps = new[] { "_TintColor", "_Color", "_BaseColor" };
            var matList = new List<Material>();

            // 1. Material
            foreach (var r in GetComponentsInChildren<Renderer>(true))
            {
                if (r == null) continue;
                r.GetMaterials(matList);
                foreach (var mat in matList)
                {
                    if (mat == null) continue;
                    foreach (var prop in colorProps)
                    {
                        if (!mat.HasProperty(prop)) continue;
                        Color c = mat.GetColor(prop);
                        c.r *= factor; c.g *= factor; c.b *= factor; c.a *= factor;
                        mat.SetColor(prop, c);
                        break;
                    }
                    if (mat.HasProperty("_EmissionColor"))
                        mat.SetColor("_EmissionColor", mat.GetColor("_EmissionColor") * factor);
                }
            }

            // 2. Light
            foreach (var lt in GetComponentsInChildren<Light>(true))
                if (lt != null) lt.intensity *= factor;

            // 3. ParticleSystem
            var allPS = GetComponentsInChildren<ParticleSystem>(true);
            if (allPS.Length == 0) return;

            foreach (var ps in allPS)
                if (ps != null) ps.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

            foreach (var ps in allPS)
            {
                if (ps == null) continue;
                var main = ps.main;

                Color sc = main.startColor.color;
                sc.r *= factor; sc.g *= factor; sc.b *= factor; sc.a *= factor;
                main.startColor = new ParticleSystem.MinMaxGradient(sc);
                main.startSizeMultiplier *= factor;

                // colorOverLifetime gradient 감소 (startColor를 덮어쓰는 모듈 대응)
                var col = ps.colorOverLifetime;
                if (col.enabled)
                {
                    var grad = col.color;
                    if (grad.mode == ParticleSystemGradientMode.Gradient ||
                        grad.mode == ParticleSystemGradientMode.TwoGradients)
                    {
                        var g = grad.gradient;
                        var keys = g.colorKeys;
                        for (int i = 0; i < keys.Length; i++)
                        {
                            var k = keys[i];
                            k.color = new Color(
                                k.color.r * factor, k.color.g * factor,
                                k.color.b * factor, k.color.a);
                            keys[i] = k;
                        }
                        g.colorKeys = keys;
                        col.color = new ParticleSystem.MinMaxGradient(g);
                    }
                }
                // emission rate 수정 제거: Curve 모드에서 constant=0 설정 시 파티클 전체 소멸 버그 발생
            }

            foreach (var ps in allPS)
                if (ps != null) ps.Play(false);
        }
    }

    /// <summary>ShieldDomeParticleColor.Start() NullRef 억제 (게임 내부 버그 우회)</summary>
    [HarmonyPatch(typeof(ShieldDomeParticleColor), "Start")]
    internal static class Patch_TotalVFX_ShieldDomeParticleColor
    {
        static Exception Finalizer(Exception __exception) => null;
    }
}

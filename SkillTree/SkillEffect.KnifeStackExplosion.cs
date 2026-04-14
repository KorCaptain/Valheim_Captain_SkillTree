using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;
using CaptainSkillTree;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 약점폭발(Stack Explosion) — 단검 H키 액티브 스킬
    ///
    /// 흐름:
    ///   H키 → 버프 활성화 (12s)
    ///   버프 중 적 적중 → 스택 누적 + fallenvalkyrie_spit_projectile 플레이어 머리 위 추종
    ///   M2키 → 수동 폭발 / 최대 스택(7) 달성 또는 버프 만료 → 자동 폭발
    ///   폭발: vfx_GodExplosion + sfx_imp_fireball_explode
    /// </summary>
    public static class KnifeStackExplosion
    {
        // === 상태 딕셔너리 ===
        private static readonly Dictionary<Player, bool>             _buffActive      = new Dictionary<Player, bool>();
        private static readonly Dictionary<Player, int>              _stackCount      = new Dictionary<Player, int>();
        private static readonly Dictionary<Player, List<GameObject>> _stackVFXObjects = new Dictionary<Player, List<GameObject>>();
        private static readonly Dictionary<Player, Character>        _stackTarget     = new Dictionary<Player, Character>();
        private static readonly Dictionary<Player, Coroutine>        _buffCoroutines  = new Dictionary<Player, Coroutine>();

        private const string SkillId       = "knife_step10_stack_explosion";
        private const string ProjectilePfb = "fallenvalkyrie_spit_projectile";

        // ────────────────────────────────────────────────────────
        //  공개 API
        // ────────────────────────────────────────────────────────

        /// <summary>버프 활성 여부 확인 (M2 인터셉트용)</summary>
        public static bool IsBuffActive(Player player) =>
            player != null && _buffActive.TryGetValue(player, out bool v) && v;

        // ────────────────────────────────────────────────────────
        //  H키 → 버프 시작
        // ────────────────────────────────────────────────────────

        public static void ActivateStackExplosion(Player player)
        {
            if (player == null || player.IsDead()) return;
            if (!SkillEffect.HasSkill(SkillId)) return;
            if (!Knife_Skill.IsUsingDagger(player))
            {
                SkillEffect.DrawFloatingText(player, L.Get("dagger_equip_required"), Color.red);
                return;
            }

            float cdRemaining = ActiveSkillCooldownRegistry.GetCooldownRemaining("H");
            if (cdRemaining > 0f)
            {
                SkillEffect.DrawFloatingText(player, L.Get("skill_on_cooldown", (int)cdRemaining), Color.yellow);
                return;
            }

            float staminaCost = Knife_Config.KnifeStackExplosionStaminaCostValue;
            if (player.GetStamina() < staminaCost)
            {
                SkillEffect.DrawFloatingText(player, L.Get("not_enough_stamina"), Color.red);
                return;
            }

            player.UseStamina(staminaCost);
            ActiveSkillCooldownRegistry.SetCooldown("H", Knife_Config.KnifeStackExplosionCooldownValue);

            if (_buffCoroutines.TryGetValue(player, out var prevCo) && prevCo != null)
                SkillTreeInputListener.Instance.StopCoroutine(prevCo);
            ClearStacks(player);

            _buffActive[player] = true;

            // 버프 시작 VFX — buff_01 발밑 기준 캐릭터 추종 1회 재생 후 자동 소멸
            VFXManager.PlayVFXAttachedToPlayer(player, "buff_01", "", 5f, Vector3.zero);

            VFXManager.PlayVFXAtPosition("sfx_fader_bite_pre", player.transform.position);
            SkillEffect.DrawFloatingText(player, L.Get("stack_explosion_buff_start"), new Color(1f, 0.4f, 0f));
            Plugin.Log.LogDebug("[약점폭발] 버프 활성화");

            var co = SkillTreeInputListener.Instance.StartCoroutine(BuffExpireCoroutine(player));
            _buffCoroutines[player] = co;
        }

        // ────────────────────────────────────────────────────────
        //  공격 적중 → 스택 누적
        // ────────────────────────────────────────────────────────

        public static void AddStack(Player player, Character monster)
        {
            if (player == null || monster == null || monster.IsDead()) return;
            if (!SkillEffect.HasSkill(SkillId)) return;
            if (!_buffActive.TryGetValue(player, out bool active) || !active) return;

            int maxStacks = Knife_Config.KnifeStackExplosionMaxStacksValue;

            if (!_stackCount.ContainsKey(player))     _stackCount[player]      = 0;
            if (!_stackVFXObjects.ContainsKey(player)) _stackVFXObjects[player] = new List<GameObject>();

            if (_stackCount[player] >= maxStacks) return;

            // 전역 스택: 타겟 전환해도 스택 유지, 폭발 대상만 최근 타겟으로 갱신
            _stackTarget[player] = monster;
            _stackCount[player]++;
            int currentStack = _stackCount[player];

            // 발사체 스폰 — 물리 콜백 외부에서 실행 (1프레임 지연)
            SkillTreeInputListener.Instance.StartCoroutine(
                SpawnStackProjectileDeferred(player, currentStack - 1, maxStacks));

            Plugin.Log.LogDebug($"[약점폭발] 스택 {currentStack}/{maxStacks}");

            if (currentStack >= maxStacks)
            {
                SkillEffect.DrawFloatingText(player, L.Get("stack_explosion_max_stack"), new Color(1f, 0.2f, 0f));
                ExplodeStacks(player);
            }
        }

        // ────────────────────────────────────────────────────────
        //  M2키 → 수동 폭발
        // ────────────────────────────────────────────────────────

        /// <summary>
        /// M2키 인터셉트에서 호출. 버프 활성 + 스택 있을 때 폭발.
        /// 반환값: true → 폭발 처리됨 (M2 기본 동작 차단), false → 폭발 불가 (기본 동작 통과)
        /// </summary>
        public static bool TriggerExplosionByM2(Player player)
        {
            if (!IsBuffActive(player)) return false;
            if (!_stackCount.TryGetValue(player, out int cnt) || cnt <= 0)
            {
                SkillEffect.DrawFloatingText(player, L.Get("stack_explosion_no_stack"), Color.yellow);
                return false;
            }
            ExplodeStacks(player);
            return true;
        }

        // ────────────────────────────────────────────────────────
        //  스택 폭발
        // ────────────────────────────────────────────────────────

        private static void ExplodeStacks(Player player)
        {
            if (!_stackCount.TryGetValue(player, out int stacks) || stacks <= 0)
            {
                ClearAll(player);
                return;
            }
            if (!_stackTarget.TryGetValue(player, out var target) || target == null || target.IsDead())
            {
                ClearAll(player);
                return;
            }

            float damagePercent = Knife_Config.KnifeStackExplosionDamagePercentValue / 100f;
            float weaponDamage  = GetWeaponBaseDamage(player);
            float totalDamage   = stacks * weaponDamage * damagePercent;

            HitData hit = new HitData();
            hit.m_damage.m_pierce = totalDamage;  // 단검 주 속성 — 화염 저항 우회
            hit.m_point           = target.GetCenterPoint();
            hit.m_dir             = (target.transform.position - player.transform.position).normalized;
            hit.m_pushForce       = 0f;
            hit.m_blockable       = false;
            hit.m_dodgeable       = false;
            hit.m_attacker        = player.GetZDOID();
            hit.SetAttacker(player);
            target.Damage(hit);

            // === 주변 7m AOE (스택 데미지의 config%) ===
            float aoeDamage = totalDamage * (Knife_Config.KnifeStackExplosionAoePercentValue / 100f);
            float aoeRadius = 7f;
            Vector3 center  = target.GetCenterPoint();

            foreach (var ch in Character.GetAllCharacters())
            {
                if (ch == null || ch == target) continue;
                if (ch.IsPlayer()) continue;
                if (!ch.IsMonsterFaction(0f) && ch.m_faction != Character.Faction.Boss) continue;
                if (Vector3.Distance(ch.transform.position, center) > aoeRadius) continue;

                var aoeHit = new HitData();
                aoeHit.m_damage.m_pierce = aoeDamage;
                aoeHit.m_point           = ch.GetCenterPoint();
                aoeHit.m_dir             = (ch.transform.position - player.transform.position).normalized;
                aoeHit.m_pushForce       = 0f;
                aoeHit.m_blockable       = false;
                aoeHit.m_dodgeable       = false;
                aoeHit.m_attacker        = player.GetZDOID();
                aoeHit.SetAttacker(player);
                ch.Damage(aoeHit);
            }

            Vector3 pos = target.GetCenterPoint();
            VFXManager.PlayVFXMultiplayer("fx_blobLava_explosion",    "", pos);
            VFXManager.PlayVFXMultiplayer("sfx_imp_fireball_explode", "", pos);

            // 실제 계산값 확인 (데미지 검증용)
            Plugin.Log.LogInfo($"[약점폭발] {stacks}스택 | 무기:{GetWeaponBaseDamage(player):F1} | %:{Knife_Config.KnifeStackExplosionDamagePercentValue} | 총:{totalDamage:F1}");
            SkillEffect.DrawFloatingText(player, $"폭발 {totalDamage:F0}", new Color(1f, 0.3f, 0f));
            ClearAll(player);
        }

        // ────────────────────────────────────────────────────────
        //  버프 만료 코루틴
        // ────────────────────────────────────────────────────────

        private static IEnumerator BuffExpireCoroutine(Player player)
        {
            yield return new UnityEngine.WaitForSeconds(Knife_Config.KnifeStackExplosionBuffDurationValue);

            if (!_buffActive.TryGetValue(player, out bool active) || !active) yield break;

            bool hasStacks = _stackCount.TryGetValue(player, out int cnt) && cnt > 0;
            if (hasStacks)
            {
                SkillEffect.DrawFloatingText(player, L.Get("stack_explosion_buff_expire"), Color.yellow);
                ExplodeStacks(player);
            }
            else
            {
                ClearAll(player);
            }
        }

        // ────────────────────────────────────────────────────────
        //  발사체 스폰 (1프레임 지연 — DestroyImmediate 물리 콜백 에러 방지)
        // ────────────────────────────────────────────────────────

        private static IEnumerator SpawnStackProjectileDeferred(Player player, int stackIndex, int maxStacks)
        {
            yield return null; // 물리 콜백 외부 프레임에서 실행

            if (player == null || player.IsDead()) yield break;
            if (!IsBuffActive(player)) yield break;

            var vfxObj = SpawnStackProjectile(player, stackIndex, maxStacks);
            if (vfxObj != null)
            {
                if (!_stackVFXObjects.ContainsKey(player))
                    _stackVFXObjects[player] = new List<GameObject>();
                _stackVFXObjects[player].Add(vfxObj);
            }
        }

        /// <summary>
        /// fallenvalkyrie_spit_projectile 프리팹을 플레이어 머리 위에 비주얼 전용으로 인스턴스화.
        /// ZNetView/ZSyncTransform/Projectile 제거 후 StackProjectileFollower로 플레이어 위치 추종.
        /// </summary>
        private static GameObject SpawnStackProjectile(Player player, int stackIndex, int maxStacks)
        {
            if (ZNetScene.instance == null) return null;

            var prefab = ZNetScene.instance.GetPrefab(ProjectilePfb);
            if (prefab == null)
            {
                Plugin.Log.LogWarning($"[약점폭발] 프리팹 없음: {ProjectilePfb}");
                return null;
            }

            try
            {
                // 왼쪽 어깨 → 오른쪽 방향 호(arc) 배치 (플레이어 로컬 공간)
                // 왼쪽(π) → 정수리(π/2) → 오른쪽(0) 반원 호
                float arcCenterY = 1.65f;  // 호 중심 높이 (어깨~머리 사이)
                float arcRadius  = 0.65f;  // 호 반경 → 정수리 2.3m, 양쪽 1.65m
                float t     = (maxStacks > 1) ? (float)stackIndex / (maxStacks - 1) : 0f;
                float angle = Mathf.PI * (1f - t); // π→0 (왼쪽→오른쪽)
                Vector3 localOffset = new Vector3(
                    Mathf.Cos(angle) * arcRadius,
                    arcCenterY + Mathf.Sin(angle) * arcRadius,
                    0.05f
                );

                // ① 프리팹 비활성화 → Awake() 차단
                bool wasActive = prefab.activeSelf;
                prefab.SetActive(false);

                // 월드 스폰 위치: 플레이어 로컬 방향 변환
                Vector3 spawnPos = player.transform.position
                    + player.transform.right   * localOffset.x
                    + player.transform.up      * localOffset.y
                    + player.transform.forward * localOffset.z;
                var obj = UnityEngine.Object.Instantiate(prefab, spawnPos, Quaternion.identity);

                prefab.SetActive(wasActive); // ② 프리팹 원상복구

                // ZNet 관련 컴포넌트 제거 (ZDO 등록 방지)
                var znv   = obj.GetComponent<ZNetView>();
                if (znv   != null) UnityEngine.Object.DestroyImmediate(znv);
                var zsync = obj.GetComponent<ZSyncTransform>();
                if (zsync != null) UnityEngine.Object.DestroyImmediate(zsync);
                var proj  = obj.GetComponent<Projectile>();
                if (proj  != null) UnityEngine.Object.DestroyImmediate(proj);

                // 콜라이더 비활성화
                foreach (var col in obj.GetComponentsInChildren<Collider>())
                    col.enabled = false;

                // 물리 고정
                var rb = obj.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                // 추종 컴포넌트 부착 — 플레이어 로컬 방향 기반
                var follower = obj.AddComponent<StackProjectileFollower>();
                follower.Initialize(player.transform, localOffset);

                // ③ 활성화
                obj.SetActive(true);
                SimpleVFX.ApplyVFXDim(obj, SkillTreeConfig.VFXOpacityValue);
                return obj;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[약점폭발] 발사체 스폰 실패: {ex.Message}");
                return null;
            }
        }

        // ────────────────────────────────────────────────────────
        //  정리 메서드
        // ────────────────────────────────────────────────────────

        private static void ClearStackVFX(Player player)
        {
            if (_stackVFXObjects.TryGetValue(player, out var list))
            {
                foreach (var go in list)
                    if (go != null) UnityEngine.Object.Destroy(go);
                list.Clear();
            }
        }

        public static void ClearStacks(Player player)
        {
            ClearStackVFX(player);
            _stackCount.Remove(player);
            _stackTarget.Remove(player);
            _stackVFXObjects.Remove(player);
        }

        private static void ClearAll(Player player)
        {
            if (_buffCoroutines.TryGetValue(player, out var co) && co != null)
                SkillTreeInputListener.Instance?.StopCoroutine(co);
            _buffCoroutines.Remove(player);
            _buffActive.Remove(player);
            ClearStacks(player);
        }

        // ────────────────────────────────────────────────────────
        //  유틸리티
        // ────────────────────────────────────────────────────────

        private static float GetWeaponBaseDamage(Player player)
        {
            try
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return 10f;
                float skillFactor = player.GetSkillFactor(Skills.SkillType.Knives);
                return weapon.GetDamage().GetTotalDamage() * (1f + skillFactor);
            }
            catch { return 10f; }
        }
    }

    /// <summary>
    /// 약점폭발 버프 활성 중 공격 스태미나를 4로 고정하는 패치.
    /// H키 시전 비용(15)은 InAttack() 외부 호출이므로 영향 없음.
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
    public static class KnifeStackExplosion_AttackStamina_Patch
    {
        static void Prefix(Player __instance, ref float v)
        {
            try
            {
                if (!KnifeStackExplosion.IsBuffActive(__instance)) return;
                if (!__instance.InAttack()) return;
                if (v > 4f) v = 4f;
            }
            catch (System.Exception) { }
        }
    }

    /// <summary>
    /// 스택 발사체 추종 컴포넌트 — LateUpdate에서 타겟(플레이어) 위치를 따라다닌다.
    /// </summary>
    public class StackProjectileFollower : MonoBehaviour
    {
        private Transform _targetTransform;
        private Vector3   _localOffset;

        public void Initialize(Transform targetTransform, Vector3 localOffset)
        {
            _targetTransform = targetTransform;
            _localOffset     = localOffset;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }

        private void LateUpdate()
        {
            if (_targetTransform == null)
            {
                Destroy(gameObject);
                return;
            }
            // 플레이어 로컬 방향 기반 — 회전 시 어깨 위치 유지
            transform.position = _targetTransform.position
                + _targetTransform.right   * _localOffset.x
                + _targetTransform.up      * _localOffset.y
                + _targetTransform.forward * _localOffset.z;
        }
    }
}

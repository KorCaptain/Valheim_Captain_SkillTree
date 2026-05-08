using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 활 멀티샷 패시브 스킬 시스템 (적중 후 자동 타겟 방식)
    /// bow_Step2_multishot: 15% 확률로 추가 화살 3발 자동 타겟팅 발사
    /// bow_Step4_multishot2: +15% 확률 추가
    /// 동작: 원래 화살 정상 발사 → 적 적중 시 확률 체크 → 머리 위에서 추가 화살 부채꼴 자동 타겟팅
    /// </summary>

    /// <summary>
    /// 추가 화살 마커 — OnHit 패치에서 재귀 방지용
    /// </summary>
    public class MultiShotArrowTag : MonoBehaviour { }

    public static partial class SkillEffect
    {
        private static bool _multishotVFXRegistered = false;

        private static void EnsureMultishotVFX()
        {
            if (_multishotVFXRegistered) return;
            SimpleVFX.RegisterValheimVFXAsCustom("staff_lightning_projectile");
            _multishotVFXRegistered = true;
        }

        // 이중시전과 동일한 수직 반원 호 — 높이 2.2f, 반지름 0.6f, 왼(-45°)→중(0°)→오(+45°)
        private static Vector3[] CalcMultishotLocalOffsets()
        {
            float height = 2.2f, radius = 0.6f;
            var center = new Vector3(0f, height, 0.3f);
            float[] degs = { -45f, 0f, 45f };
            var offsets = new Vector3[3];
            for (int i = 0; i < 3; i++)
            {
                float rad = degs[i] * Mathf.Deg2Rad;
                offsets[i] = center + new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0f);
            }
            return offsets;
        }

        internal static System.Collections.IEnumerator DelayedFireMultishot(Player player, Character hitChar)
        {
            // 사전 검증
            var ammo = player.GetAmmoItem();
            if (ammo == null) yield break;
            if (ammo.m_shared.m_attack?.m_attackProjectile == null) yield break;

            var angles = new float[] { -3f, 0f, 3f };
            var localOffsets = CalcMultishotLocalOffsets();

            // ── 소환 단계 — SimpleVFX custom 등록 후 PlayFollowing ──
            EnsureMultishotVFX();
            var previews = new GameObject[3];
            for (int i = 0; i < 3; i++)
                previews[i] = SimpleVFX.PlayFollowing("staff_lightning_projectile", player.transform, localOffsets[i], 3f);

            // ── 2초 대기 — 0.1초마다 사망 체크 ──
            float elapsed = 0f;
            while (elapsed < 2f)
            {
                yield return new WaitForSeconds(0.1f);
                elapsed += 0.1f;
                if (player == null || hitChar == null || hitChar.IsDead())
                {
                    DestroyPreviews(previews);
                    yield break;
                }
            }

            var weapon = player.GetCurrentWeapon();
            ammo = player.GetAmmoItem();
            if (weapon == null || ammo == null)
            {
                DestroyPreviews(previews);
                yield break;
            }

            var consumeCount = SkillTreeConfig.BowMultishotArrowConsumptionValue;
            if (consumeCount > 0) ConsumeMultishotArrows(player, consumeCount);

            bool hasLv1 = HasSkill("bow_Step2_multishot");
            bool hasLv2 = HasSkill("bow_Step4_multishot2");
            string skillLevel = (hasLv1 && hasLv2) ? "Lv1+Lv2" : (hasLv2 ? "Lv2" : "Lv1");
            var arrowCount = SkillTreeConfig.BowMultishotArrowCountValue;

            // ── 0.3초 간격 순차 발사 (try-finally로 잔존 오브젝트 보장) ──
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    if (player == null || hitChar == null || hitChar.IsDead()) break;
                    var firePos = previews[i] != null
                        ? previews[i].transform.position
                        : player.transform.TransformPoint(localOffsets[i]);

                    if (previews[i] != null) { previews[i].SetActive(false); UnityEngine.Object.Destroy(previews[i]); previews[i] = null; }

                    var baseDir = (SafeGetCenter(hitChar) - firePos).normalized;
                    VFXManager.PlaySound("sfx_bow_draw", firePos, 0.6f);
                    FireAutoTargetArrow(player, weapon, ammo, firePos, baseDir, angles[i]);

                    if (i < 2)
                    {
                        yield return new WaitForSeconds(0.3f);
                        if (player == null || hitChar == null || hitChar.IsDead()) break;
                    }
                }

                ShowSkillEffectText(player,
                    $"🏹 {L.Get("multishot_skill", skillLevel, arrowCount)}",
                    new Color(0.2f, 0.8f, 0.2f), SkillEffectTextType.Combat);
                PlaySkillEffect(player, "bow_multishot", player.transform.position);
            }
            finally
            {
                DestroyPreviews(previews);
            }
        }

        /// <summary>
        /// 자동 타겟 추가 화살 발사 — 부채꼴 각도 적용, MultiShotArrowTag 부착
        /// </summary>
        private static void DestroyPreviews(GameObject[] previews)
        {
            foreach (var p in previews)
                if (p != null) { p.SetActive(false); UnityEngine.Object.Destroy(p); }
        }

        private static Vector3 SafeGetCenter(Character chr)
        {
            try
            {
                if (chr == null) return Vector3.zero;
                return chr.GetCenterPoint();
            }
            catch { return Vector3.zero; }
        }

        private static void FireAutoTargetArrow(Player player, ItemDrop.ItemData weapon, ItemDrop.ItemData ammo,
            Vector3 spawnPoint, Vector3 baseDir, float angleOffset)
        {
            try
            {
                // 부채꼴 방향 계산
                var fireDir = angleOffset != 0f
                    ? Quaternion.AngleAxis(angleOffset, Vector3.up) * baseDir
                    : baseDir;

                var ammoAttack = ammo.m_shared.m_attack;
                if (ammoAttack?.m_attackProjectile == null)
                {
                    Plugin.Log.LogError($"[멀티샷] 프로젝타일 없음: {ammo.m_shared.m_name}");
                    return;
                }

                var bowAttack = weapon.m_shared.m_attack;
                if (bowAttack == null) return;

                var projectileObj = UnityEngine.Object.Instantiate(
                    ammoAttack.m_attackProjectile,
                    spawnPoint,
                    Quaternion.LookRotation(fireDir)
                );

                if (projectileObj == null) return;

                // 재귀 방지 마커 부착
                projectileObj.AddComponent<MultiShotArrowTag>();

                var projectile = projectileObj.GetComponent<Projectile>();
                if (projectile != null)
                {
                    var hitData = new HitData();
                    var fullDamage = weapon.GetDamage();
                    fullDamage.Add(ammo.GetDamage());
                    fullDamage.Modify(SkillTreeConfig.BowMultishotDamagePercentValue / 100f);

                    hitData.m_damage = fullDamage;
                    hitData.m_point = spawnPoint;
                    hitData.m_dir = fireDir;
                    hitData.m_attacker = player.GetZDOID();
                    hitData.m_skill = Skills.SkillType.Bows;
                    hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;
                    hitData.SetAttacker(player);

                    var velocity = fireDir * bowAttack.m_projectileVel;
                    projectile.Setup(player, velocity, bowAttack.m_projectileAccuracy, hitData, ammo, weapon);

                    var rb = projectileObj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = velocity;
                        rb.isKinematic = false;
                        rb.useGravity = true;
                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(projectileObj);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷 자동 타겟] 화살 발사 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 멀티샷용 화살 소모 처리
        /// </summary>
        private static void ConsumeMultishotArrows(Player player, int consumeCount)
        {
            try
            {
                var inventory = player.GetInventory();
                if (inventory == null) return;

                var ammo = GetEquippedArrow(player);
                if (ammo == null) return;

                if (ammo.m_stack >= consumeCount)
                {
                    ammo.m_stack -= consumeCount;
                    if (ammo.m_stack <= 0)
                        inventory.RemoveItem(ammo);
                }
                else
                {
                    ammo.m_stack = 0;
                    inventory.RemoveItem(ammo);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷] 화살 소모 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 장착 화살 조회
        /// </summary>
        private static ItemDrop.ItemData GetEquippedArrow(Player player)
        {
            var inventory = player.GetInventory();
            if (inventory == null) return null;

            try
            {
                var arrows = inventory.GetAmmoItem("bow", "");
                if (arrows != null && arrows.m_stack > 0 && ValidateArrowProjectile(arrows))
                    return arrows;

                foreach (var item in inventory.GetAllItems())
                {
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo &&
                        item.m_stack > 0 && ValidateArrowProjectile(item))
                        return item;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷] 화살 검색 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 추가 화살 착탄 시 2m 스플래시 + 3m 화염 폭발 적용
        /// </summary>
        internal static void ApplyMultiShotExplosion(Vector3 hitPoint, Player attacker)
        {
            try
            {
                VFXManager.PlayVFXMultiplayer("fx_batteringram_fire", "sfx_blobLava_explosion", hitPoint + Vector3.up * 0.2f);

                var weapon = attacker.GetCurrentWeapon();
                if (weapon == null) return;

                float baseDmg = weapon.GetDamage().GetTotalDamage();

                // 2m 스플래시 (물리 충격) — 직격 반경
                var splash = Physics.OverlapSphere(hitPoint, 2f);
                foreach (var col in splash)
                {
                    var chr = col.GetComponent<Character>();
                    if (chr == null || chr == attacker || chr.IsDead()) continue;
                    if (!BaseAI.IsEnemy(attacker, chr) && chr.GetComponent<MonsterAI>() == null) continue;

                    float dmg = baseDmg * 0.3f;
                    if (chr.m_boss) dmg *= 0.5f;

                    var hit = new HitData();
                    hit.m_damage.m_pierce = dmg;
                    hit.m_point = chr.GetCenterPoint();
                    hit.m_dir = (chr.transform.position - hitPoint).normalized;
                    hit.SetAttacker(attacker);
                    hit.m_skill = Skills.SkillType.Bows;
                    chr.Damage(hit);
                }

                // 3m 화염 폭발 (속성 피해) — 외곽 범위
                var fireZone = Physics.OverlapSphere(hitPoint, 3f);
                foreach (var col in fireZone)
                {
                    var chr = col.GetComponent<Character>();
                    if (chr == null || chr == attacker || chr.IsDead()) continue;
                    if (!BaseAI.IsEnemy(attacker, chr) && chr.GetComponent<MonsterAI>() == null) continue;

                    float dmg = baseDmg * 0.5f;
                    if (chr.m_boss) dmg *= 0.5f;

                    var hit = new HitData();
                    hit.m_damage.m_fire = dmg;
                    hit.m_point = chr.GetCenterPoint();
                    hit.m_dir = (chr.transform.position - hitPoint).normalized;
                    hit.SetAttacker(attacker);
                    hit.m_skill = Skills.SkillType.Bows;
                    chr.Damage(hit);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷 폭발] 오류: {ex.Message}");
            }
        }

        // ValidateArrowProjectile 은 SkillEffect.ArcherMultiShot.cs 에 정의됨
    }

    /// <summary>
    /// FireProjectileBurst 패치 — ArcherMultiShot(Y키) 차단만 유지.
    /// BowExpert 멀티샷은 원래 화살 발사 후 Projectile.OnHit 에서 처리.
    /// </summary>
    [HarmonyPatch(typeof(Attack), "FireProjectileBurst")]
    [HarmonyPriority(Priority.High)]
    public static class BowExpertMultiShot_FireProjectileBurst_Patch
    {
        [HarmonyPrefix]
        private static bool Prefix(Attack __instance)
        {
            try
            {
                var attacker = Traverse.Create(__instance).Field("m_character").GetValue<Character>();
                if (attacker == null || attacker != Player.m_localPlayer) return true;

                var player = Player.m_localPlayer;
                if (player == null) return true;

                var currentWeapon = player.GetCurrentWeapon();
                if (currentWeapon?.m_shared?.m_skillType != Skills.SkillType.Bows) return true;

                // ArcherMultiShot(Y키) 활성 → 원래 화살 유지 + 순차 볼리 발사
                if (SkillEffect.IsArcherMultiShotReady(player))
                {
                    Vector3 attackDir = player.GetLookDir();
                    SkillEffect.StartArcherVolleyCoroutine(player, currentWeapon, attackDir);
                    return true;
                }

                // BowExpert 멀티샷: 원래 화살 그대로 발사, OnHit에서 처리
                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷] FireProjectileBurst 패치 오류: {ex.Message}");
                return true;
            }
        }
    }

    /// <summary>
    /// Projectile.OnHit 패치 — 원래 화살 적중 시 멀티샷 확률 체크 후 자동 타겟 추가 화살 발사
    /// </summary>
    [HarmonyPatch(typeof(Projectile), "OnHit",
        new System.Type[] { typeof(Collider), typeof(Vector3), typeof(bool), typeof(Vector3) })]
    [HarmonyPriority(Priority.Low)]
    public static class BowExpertMultiShot_ProjectileHit_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(Projectile __instance,
            Collider collider, Vector3 hitPoint, bool water, Vector3 normal)
        {
            try
            {
                if (water) return;
                if (__instance.m_skill != Skills.SkillType.Bows) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

                // 아처 볼리 화살은 BowExpert 처리 스킵 (ArcherMultiShot.cs에서 처리)
                if (__instance.GetComponent<ArcherMultiShotProjectileTag>() != null) return;

                // 추가 화살 적중 → 폭발 처리 후 종료 (재귀 방지)
                if (__instance.GetComponent<MultiShotArrowTag>() != null)
                {
                    SkillEffect.ApplyMultiShotExplosion(hitPoint, player);
                    return;
                }

                // 적 캐릭터 적중 확인
                var hitChar = collider?.GetComponentInParent<Character>();
                if (hitChar == null || hitChar == player || hitChar.IsDead()) return;

                // 적대적 대상만
                if (!BaseAI.IsEnemy(player, hitChar) && hitChar.GetComponent<MonsterAI>() == null) return;

                // 스킬 보유 확인
                bool hasLv1 = SkillEffect.HasSkill("bow_Step2_multishot");
                bool hasLv2 = SkillEffect.HasSkill("bow_Step4_multishot2");
                if (!hasLv1 && !hasLv2) return;

                // 확률 계산
                var lv1Chance = SkillTreeConfig.BowMultishotLv1ChanceValue / 100f;
                var lv2Chance = SkillTreeConfig.BowMultishotLv2ChanceValue / 100f;

                float totalChance = 0f;
                if (hasLv1 && hasLv2) totalChance = lv1Chance + lv2Chance;
                else if (hasLv2)      totalChance = lv2Chance;
                else                  totalChance = lv1Chance;

                if (UnityEngine.Random.Range(0f, 1f) >= totalChance) return;

                SkillTreeInputListener.Instance.StartCoroutine(SkillEffect.DelayedFireMultishot(player, hitChar));
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷] OnHit 패치 오류: {ex.Message}");
            }
        }
    }


}

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
        /// <summary>
        /// 적 적중 시 멀티샷 확률 체크 후 자동 타겟 추가 화살 발사
        /// </summary>
        internal static void FireAutoTargetMultishot(Player player, Character hitChar)
        {
            try
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return;

                var ammo = player.GetAmmoItem();
                if (ammo == null)
                {
                    Plugin.Log.LogWarning("[멀티샷] 화살 없음 — 발사 중단");
                    return;
                }

                // 화살 소모
                var consumeCount = SkillTreeConfig.BowMultishotArrowConsumptionValue;
                if (consumeCount > 0)
                    ConsumeMultishotArrows(player, consumeCount);

                // 발사 기점: 머리 위 1.8f
                var spawnPoint = player.transform.position + player.transform.up * 1.8f;

                // 자동 타겟 방향: 피격 적의 중심으로
                var targetPos = hitChar.GetCenterPoint();
                var baseDir = (targetPos - spawnPoint).normalized;

                bool hasLv1 = HasSkill("bow_Step2_multishot");
                bool hasLv2 = HasSkill("bow_Step4_multishot2");
                string skillLevel = (hasLv1 && hasLv2) ? "Lv1+Lv2" : (hasLv2 ? "Lv2" : "Lv1");
                var arrowCount = SkillTreeConfig.BowMultishotArrowCountValue;

                // 추가 화살 소환 사운드 (시위 당기는 소리 — 3발 동시 소환 느낌)
                VFXManager.PlaySound("sfx_bow_draw", spawnPoint, 1f);

                // 부채꼴 각도 (-3°, 0°, +3°)
                var angles = new float[] { -3f, 0f, 3f };
                foreach (var angle in angles)
                    FireAutoTargetArrow(player, weapon, ammo, spawnPoint, baseDir, angle);

                ShowSkillEffectText(player,
                    $"🏹 {L.Get("multishot_skill", skillLevel, arrowCount)}",
                    new Color(0.2f, 0.8f, 0.2f), SkillEffectTextType.Combat);
                PlaySkillEffect(player, "bow_multishot", player.transform.position);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷 자동 타겟] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 자동 타겟 추가 화살 발사 — 부채꼴 각도 적용, MultiShotArrowTag 부착
        /// </summary>
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

                // ArcherMultiShot(Y키) 활성 → 기존 방식 유지 (원래 화살 차단)
                if (SkillEffect.IsArcherMultiShotReady(player))
                {
                    Vector3 attackDir = player.GetLookDir();
                    SkillEffect.PerformArcherMultiShotAttack(player, currentWeapon, attackDir);
                    return false;
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

                // 추가 화살(멀티샷이 발사한 것)이면 재귀 방지
                if (__instance.GetComponent<MultiShotArrowTag>() != null) return;

                // 활 발사체만 처리
                if (__instance.m_skill != Skills.SkillType.Bows) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

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

                Plugin.Log.LogDebug($"[멀티샷] 발동! 대상: {hitChar.name} (확률: {totalChance * 100f:F0}%)");
                SkillEffect.FireAutoTargetMultishot(player, hitChar);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷] OnHit 패치 오류: {ex.Message}");
            }
        }
    }
}

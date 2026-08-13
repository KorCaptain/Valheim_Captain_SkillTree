using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using CaptainSkillTree;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 화살비 시스템 - H키 액티브 스킬 (버프형)
    /// H키 → 버프 활성화 → 마우스 좌클릭 활 발사 → 적중 지점 위 200m에서 30발 낙하
    /// </summary>
    public static partial class SkillEffect
    {
        // === 화살비 관련 변수 ===
        private static Dictionary<Player, float> arrowRainCooldown = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> arrowRainReady = new Dictionary<Player, bool>();
        private static Dictionary<Player, GameObject> arrowRainStatusEffects = new Dictionary<Player, GameObject>();
        private static GameObject cachedArrowRainStatusPrefab = null;

        // === 던전 내 대체 버프(공격력 강화) 만료 시각 및 상태 VFX ===
        private static Dictionary<Player, float> arrowRainDungeonBuffExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, GameObject> arrowRainDungeonBuffStatusEffects = new Dictionary<Player, GameObject>();
        private static GameObject cachedArrowRainDungeonBuffStatusPrefab = null;

        /// <summary>
        /// H키 화살비 실행 진입점 - 버프 활성화
        /// </summary>
        public static void ExecuteArrowRain(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                // 1. 쿨타임 확인
                if (!arrowRainCooldown.ContainsKey(player))
                    arrowRainCooldown[player] = 0f;

                float cooldown = Bow_Config.ArrowRainCooldownValue;
                float elapsed  = Time.time - arrowRainCooldown[player];
                if (elapsed < cooldown)
                {
                    float remaining = cooldown - elapsed;
                    ShowSkillEffectText(player, L.Get("cooldown_format", $"{remaining:F1}"),
                        Color.yellow, SkillEffectTextType.Passive);
                    return;
                }

                // 2. 활 착용 확인
                var weapon = player.GetCurrentWeapon();
                if (weapon == null || weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
                {
                    ShowSkillEffectText(player, L.Get("bow_equip_required"), Color.red, SkillEffectTextType.Standard);
                    return;
                }

                // 3. 스태미나 확인
                float maxStamina  = player.GetMaxStamina();
                float reqStamina  = maxStamina * (Bow_Config.ArrowRainStaminaCostValue / 100f);
                if (player.GetStamina() < reqStamina)
                {
                    ShowSkillEffectText(player, L.Get("stamina_insufficient"), Color.red, SkillEffectTextType.Standard);
                    return;
                }

                // 4. 쿨타임 등록 + 스태미나 소모
                arrowRainCooldown[player] = Time.time;
                ActiveSkillCooldownRegistry.SetCooldownForSkill("H", "bow_Step6_arrow_rain", cooldown);
                player.UseStamina(reqStamina);

                // 던전 내부(천장으로 상공 낙하 연출이 막힘): 화살비 대신 공격력 강화 자버프로 대체
                if (player.transform.position.y > 4000f)
                {
                    ExecuteArrowRainDungeonBuff(player);
                    return;
                }

                // 5. 버프 활성화
                arrowRainReady[player] = true;

                // 6. VFX/SFX (폭발화살과 동일한 활성화 효과)
                PlayArrowRainActivationEffects(player);

                ShowSkillEffectText(player, "🏹 " + L.Get("bow_arrow_rain_ready"),
                    new Color(0.3f, 0.8f, 1f), SkillEffectTextType.Combat);
                Plugin.Log.LogInfo("[화살비] 버프 활성화 완료 - 다음 활 발사 대기");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] 오류: {ex.Message}");
            }
        }

        /// <summary>화살비 준비 상태 확인 (패치에서 호출)</summary>
        public static bool IsArrowRainReady(Player player)
        {
            return arrowRainReady.TryGetValue(player, out bool ready) && ready;
        }

        /// <summary>화살비 소모 + 코루틴 시작 (패치에서 호출)</summary>
        public static void ConsumeArrowRain(Player player, Vector3 hitPoint)
        {
            try
            {
                arrowRainReady[player] = false;
                CleanupArrowRainStatusEffect(player);
                Plugin.Instance.StartCoroutine(ArrowRainCoroutine(player, hitPoint));
                Plugin.Instance.StartCoroutine(ArrowRainSoundCoroutine(hitPoint));
                Plugin.Log.LogInfo($"[화살비] 발동 - 중심점: {hitPoint}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] ConsumeArrowRain 오류: {ex.Message}");
            }
        }

        /// <summary>화살비 시작 시 SFX 10회 (1.5초 동안 0.15초 간격)</summary>
        private static IEnumerator ArrowRainSoundCoroutine(Vector3 center)
        {
            for (int i = 0; i < 10; i++)
            {
                VFXManager.PlaySound("sfx_fader_spawn_meteor_impact", center, 20f);
                yield return new WaitForSeconds(0.15f);
            }
        }

        /// <summary>착지 반경 2m 내 몬스터에게 냉기 슬로우 2초 적용 (패치에서도 호출)</summary>
        public static void ApplyArrowRainFrostSlow(Vector3 point)
        {
            try
            {
                var cols = Physics.OverlapSphere(point, 2f);
                foreach (var col in cols)
                {
                    var ch = col.GetComponent<Character>()
                          ?? col.GetComponentInParent<Character>();
                    if (ch == null || ch.IsDead()) continue;
                    if (ch.IsPlayer() && !IsPvPCombat(ch, Player.m_localPlayer)) continue;

                    var hash = "ArrowRainFrost".GetStableHashCode();
                    ch.GetSEMan()?.RemoveStatusEffect(hash);

                    var se           = ScriptableObject.CreateInstance<SE_Stats>();
                    se.name          = "ArrowRainFrost";
                    se.m_name        = L.Get("se_arrow_rain_frost");
                    se.m_ttl         = 2f;
                    se.m_speedModifier = -0.5f;
                    ch.GetSEMan()?.AddStatusEffect(se);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] 냉기 슬로우 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 화살비 코루틴 - 30발 × 0.05초 간격 하늘 낙하
        /// </summary>
        private static IEnumerator ArrowRainCoroutine(Player player, Vector3 center)
        {
            int   count  = Bow_Config.ArrowRainArrowCountValue;
            float radius = Bow_Config.ArrowRainRadiusValue;

            for (int i = 0; i < count; i++)
            {
                if (player == null || player.IsDead()) yield break;

                // 반경 내 랜덤 착탄 지점 (XZ 평면)
                Vector2 rnd      = UnityEngine.Random.insideUnitCircle * radius;
                Vector3 groundPos = center + new Vector3(rnd.x, 0f, rnd.y);

                // 지면 높이 보정
                if (ZoneSystem.instance != null)
                {
                    ZoneSystem.instance.GetGroundHeight(groundPos, out float gh);
                    groundPos.y = gh;
                }

                // 200m 상공에서 스폰
                Vector3 spawnPos = groundPos + Vector3.up * 200f;
                SpawnArrowRainProjectile(player, spawnPos, Vector3.down);

                yield return new WaitForSeconds(0.05f);
            }

            ShowSkillEffectText(player, "🏹 " + L.Get("bow_arrow_rain_complete"),
                new Color(0.3f, 0.8f, 1f), SkillEffectTextType.Standard);
        }

        /// <summary>화살비 단발 발사체 생성 (하늘에서 낙하)</summary>
        private static void SpawnArrowRainProjectile(Player player, Vector3 spawnPos, Vector3 dir)
        {
            try
            {
                var ammo   = player.GetAmmoItem();
                var weapon = player.GetCurrentWeapon();

                // 프로젝타일 프리팹 결정 (화살 우선, 없으면 활)
                GameObject prefab = null;
                if (ammo?.m_shared.m_attack?.m_attackProjectile != null)
                    prefab = ammo.m_shared.m_attack.m_attackProjectile;
                else if (weapon?.m_shared.m_attack?.m_attackProjectile != null)
                    prefab = weapon.m_shared.m_attack.m_attackProjectile;

                if (prefab == null)
                {
                    Plugin.Log.LogWarning("[화살비] 프로젝타일 프리팹 없음");
                    return;
                }

                var go = UnityEngine.Object.Instantiate(prefab, spawnPos, Quaternion.LookRotation(dir));

                // 착지 VFX 트리거용 태그 추가
                var tag = go.AddComponent<ArrowRainProjectileTag>();
                tag.Processed = false;
                tag.Owner = player;

                // Projectile 컴포넌트 설정
                var proj = go.GetComponent<Projectile>();
                if (proj != null)
                {
                    var hitData = new HitData();
                    var dmg     = weapon != null ? weapon.GetDamage() : new HitData.DamageTypes();
                    if (ammo != null) dmg.Add(ammo.GetDamage());
                    int arrowRainLevel1 = SkillTreeManager.Instance?.GetSkillLevel("bow_Step6_arrow_rain") ?? 1;
                    float arrowRainBonus1 = (arrowRainLevel1 - 1) * Bow_Config.ArrowRainLevelBonusValue;
                    dmg.Modify((Bow_Config.ArrowRainDamagePercentValue + arrowRainBonus1) / 100f);

                    hitData.m_damage   = dmg;
                    hitData.m_dir      = dir;
                    hitData.m_skill    = Skills.SkillType.Bows;
                    hitData.m_attacker = player.GetZDOID();

                    float accuracy = weapon?.m_shared.m_attack?.m_projectileAccuracy ?? 0f;
                    Vector3 velocity = dir * 60f;
                    proj.Setup(player, velocity, accuracy, hitData, ammo, weapon);
                }

                // 물리: 중력 없이 직선 낙하
                var rb = go.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity    = dir * 60f;
                    rb.useGravity  = false;
                    rb.isKinematic = false;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] SpawnArrowRainProjectile 오류: {ex.Message}");
            }
        }

        /// <summary>화살비 활성화 VFX/SFX (별도 딕셔너리 - 폭발화살과 충돌 방지)</summary>
        private static void PlayArrowRainActivationEffects(Player player)
        {
            try
            {
                // 장전 시작 사운드
                VFXManager.PlaySound("sfx_reload_start", player.transform.position, 5f);

                // 발밑 버프 VFX (buff_01), 2초간
                var buff01Prefab = VFXManager.GetVFXPrefab("buff_01");
                if (buff01Prefab != null)
                {
                    var buff01 = UnityEngine.Object.Instantiate(
                        buff01Prefab,
                        player.transform.position + Vector3.up * 0.5f,
                        player.transform.rotation);
                    buff01.transform.SetParent(player.transform, false);
                    buff01.transform.localPosition = Vector3.up * 0.5f;
                    buff01.transform.localScale     = Vector3.one * 0.8f;
                    UnityEngine.Object.Destroy(buff01, 2f);
                }

                // 머리 위 오라 VFX (화살 발사까지 지속)
                PlayArrowRainStatusEffect(player);

                // 버프 완료 사운드
                VFXManager.PlaySound("sfx_reload_dverger_done", player.transform.position, 5f);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] 활성화 효과 오류: {ex.Message}");
            }
        }

        /// <summary>화살비 오라 VFX (머리 위 2m, 발사까지 지속)</summary>
        private static void PlayArrowRainStatusEffect(Player player)
        {
            try
            {
                if (cachedArrowRainStatusPrefab == null)
                {
                    cachedArrowRainStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedArrowRainStatusPrefab == null)
                    {
                        Plugin.Log.LogWarning("[화살비] statusailment_01_aura 프리팹 없음");
                        return;
                    }
                }

                // 기존 오라 제거 후 재생성
                if (arrowRainStatusEffects.ContainsKey(player) && arrowRainStatusEffects[player] != null)
                    UnityEngine.Object.Destroy(arrowRainStatusEffects[player]);
                arrowRainStatusEffects.Remove(player);

                var inst = UnityEngine.Object.Instantiate(
                    cachedArrowRainStatusPrefab,
                    player.transform.position + Vector3.up * 2f,
                    Quaternion.identity);
                inst.transform.SetParent(player.transform, false);
                inst.transform.localPosition = Vector3.up * 2f;
                inst.transform.localScale     = Vector3.one * 0.6f;
                arrowRainStatusEffects[player] = inst;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] 오라 VFX 오류: {ex.Message}");
            }
        }

        /// <summary>오라 VFX 제거</summary>
        private static void CleanupArrowRainStatusEffect(Player player)
        {
            if (arrowRainStatusEffects.TryGetValue(player, out var go) && go != null)
                UnityEngine.Object.Destroy(go);
            arrowRainStatusEffects.Remove(player);
        }

        /// <summary>
        /// 던전 내부 전용 대체 효과: 화살비 대신 자기 자신에게 공격력 강화 버프 부여
        /// </summary>
        private static void ExecuteArrowRainDungeonBuff(Player player)
        {
            try
            {
                float duration = Bow_Config.ArrowRainDungeonBuffDurationValue;
                float bonus = Bow_Config.ArrowRainDungeonBuffDamageBonusValue;

                arrowRainDungeonBuffExpiry[player] = Time.time + duration;

                PlayArrowRainDungeonBuffActivationEffects(player, duration);

                ShowSkillEffectText(player, "🏹 " + L.Get("bow_arrow_rain_dungeon_buff_cast", (int)bonus, (int)duration),
                    new Color(0.3f, 0.8f, 1f), SkillEffectTextType.Combat);

                Plugin.Log.LogInfo($"[화살비 던전버프] {player.GetPlayerName()} 공격력 +{bonus}% ({duration}초)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비 던전버프] 스킬 실행 실패: {ex.Message}");
            }
        }

        /// <summary>발밑 buff_01(활성화, 2초) + 머리 위 statusailment_01_aura(지속, duration초) 버프 VFX</summary>
        private static void PlayArrowRainDungeonBuffActivationEffects(Player player, float duration)
        {
            try
            {
                // 0. 버프 활성화 사운드
                VFXManager.PlaySound("sfx_reload_dverger_done", player.transform.position, 2f);

                // 1. 활성화 효과 (buff_01) - 발밑, 2초간
                var buff01Prefab = VFXManager.GetVFXPrefab("buff_01");
                if (buff01Prefab != null)
                {
                    var buff01Effect = UnityEngine.Object.Instantiate(
                        buff01Prefab,
                        player.transform.position + Vector3.up * 0.5f,
                        player.transform.rotation);
                    buff01Effect.transform.SetParent(player.transform, false);
                    buff01Effect.transform.localPosition = Vector3.up * 0.5f;
                    buff01Effect.transform.localScale = Vector3.one * 0.8f;
                    UnityEngine.Object.Destroy(buff01Effect, 2f);
                }

                // 2. 상태 표시 효과 (statusailment_01_aura) - 머리 위, duration초 지속
                if (cachedArrowRainDungeonBuffStatusPrefab == null)
                {
                    cachedArrowRainDungeonBuffStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                }

                if (cachedArrowRainDungeonBuffStatusPrefab != null)
                {
                    if (arrowRainDungeonBuffStatusEffects.TryGetValue(player, out var existing) && existing != null)
                    {
                        UnityEngine.Object.Destroy(existing);
                        arrowRainDungeonBuffStatusEffects.Remove(player);
                    }

                    var headPosition = player.transform.position + Vector3.up * 2.0f;
                    var statusInstance = UnityEngine.Object.Instantiate(cachedArrowRainDungeonBuffStatusPrefab, headPosition, Quaternion.identity);
                    statusInstance.transform.SetParent(player.transform, false);
                    statusInstance.transform.localPosition = Vector3.up * 2.0f;
                    statusInstance.transform.localScale = Vector3.one * 0.6f;

                    arrowRainDungeonBuffStatusEffects[player] = statusInstance;
                    UnityEngine.Object.Destroy(statusInstance, duration);
                }
                else
                {
                    Plugin.Log.LogWarning("[화살비 던전버프] statusailment_01_aura 프리팹을 찾을 수 없음");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비 던전버프] 활성화 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>던전 내 대체 버프가 현재 활성 상태인지 확인</summary>
        public static bool IsArrowRainDungeonBuffActive(Player player)
        {
            if (player == null) return false;
            return arrowRainDungeonBuffExpiry.TryGetValue(player, out float expiry) && Time.time < expiry;
        }

        /// <summary>던전 내 대체 버프의 공격력 보너스(%) 반환 (비활성 시 0)</summary>
        public static float GetArrowRainDungeonBuffDamageBonus(Player player)
        {
            return IsArrowRainDungeonBuffActive(player) ? Bow_Config.ArrowRainDungeonBuffDamageBonusValue : 0f;
        }

        /// <summary>화살비 착지 AOE 데미지 - 반경 3m 내 몬스터에게 적용</summary>
        public static void ApplyArrowRainAOEDamage(Player owner, Vector3 point)
        {
            try
            {
                if (owner == null) return;
                var weapon = owner.GetCurrentWeapon();
                var ammo   = owner.GetAmmoItem();

                var dmg = weapon != null ? weapon.GetDamage() : new HitData.DamageTypes();
                if (ammo != null) dmg.Add(ammo.GetDamage());
                int arrowRainLevel = SkillTreeManager.Instance?.GetSkillLevel("bow_Step6_arrow_rain") ?? 1;
                float arrowRainLevelBonus = (arrowRainLevel - 1) * Bow_Config.ArrowRainLevelBonusValue;
                dmg.Modify((Bow_Config.ArrowRainDamagePercentValue + arrowRainLevelBonus) / 100f);

                var cols = Physics.OverlapSphere(point, 3f);
                foreach (var col in cols)
                {
                    var ch = col.GetComponent<Character>()
                          ?? col.GetComponentInParent<Character>();
                    if (ch == null || ch.IsDead()) continue;
                    if (ch.IsPlayer() && !(ch.IsPVPEnabled() && owner.IsPVPEnabled())) continue;

                    var hit = new HitData();

                    // 보스 판별: 이름 패턴 기반 (TankerSkills 패턴과 동일)
                    var targetDmg = dmg;                      // struct 값 복사 (원본 보존)
                    string chName = ch.name?.ToLower() ?? "";
                    bool isBoss = chName.Contains("eikthyr") || chName.Contains("elder") ||
                                  chName.Contains("bonemass") || chName.Contains("moder") ||
                                  chName.Contains("yagluth") || chName.Contains("queen") ||
                                  chName.Contains("boss") || chName.Contains("dragon") ||
                                  chName.Contains("giantslime");
                    if (isBoss)
                        targetDmg.Modify(0.5f);
                    hit.m_damage   = targetDmg;

                    hit.m_point    = point;
                    hit.m_dir      = (ch.transform.position - point).normalized;
                    hit.m_skill    = Skills.SkillType.Bows;
                    hit.m_attacker = owner.GetZDOID();
                    ch.Damage(hit);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] AOE 데미지 오류: {ex.Message}");
            }
        }

        /// <summary>사망 시 정리 (외부 호출용)</summary>
        public static void CleanupArrowRainOnDeath(Player player)
        {
            try
            {
                arrowRainCooldown.Remove(player);
                arrowRainReady.Remove(player);
                CleanupArrowRainStatusEffect(player);

                arrowRainDungeonBuffExpiry.Remove(player);
                if (arrowRainDungeonBuffStatusEffects.TryGetValue(player, out var dungeonBuffFx) && dungeonBuffFx != null)
                    UnityEngine.Object.Destroy(dungeonBuffFx);
                arrowRainDungeonBuffStatusEffects.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[화살비] 정리 실패: {ex.Message}");
            }
        }
    }

    // ================================================================
    // 화살비 낙하 화살 태그 컴포넌트
    // ================================================================
    public class ArrowRainProjectileTag : MonoBehaviour
    {
        public bool Processed = false;
        public Player Owner;
    }

    // ================================================================
    // Projectile.OnHit 패치 - 화살비 트리거 + 착지 VFX/SFX
    // ================================================================
    /// <summary>
    /// Case A: ArrowRainProjectileTag 있음 → 착지 VFX/SFX
    /// Case B: 일반 활 발사체 + 화살비 준비 상태 → ConsumeArrowRain
    /// </summary>
    [HarmonyPatch(typeof(Projectile), "OnHit",
        new Type[] { typeof(Collider), typeof(Vector3), typeof(bool), typeof(Vector3) })]
    [HarmonyPriority(Priority.Low)]
    public static class ArrowRain_ProjectileHit_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Projectile __instance,
            Collider collider, Vector3 hitPoint, bool water, Vector3 normal)
        {
            try
            {
                // Case A: 화살비 낙하 화살 착지 (물이면 스킵)
                var tag = __instance.GetComponent<ArrowRainProjectileTag>();
                if (tag != null)
                {
                    if (water) return;
                    if (tag.Processed) return;
                    tag.Processed = true;

                    // 착탄 지점 3m 내 적 존재 여부 확인
                    bool hasEnemy = false;
                    var nearbyCols = Physics.OverlapSphere(hitPoint, 3f);
                    foreach (var col in nearbyCols)
                    {
                        var ch = col.GetComponent<Character>() ?? col.GetComponentInParent<Character>();
                        if (ch == null || ch.IsDead()) continue;
                        if (ch.IsPlayer() && !SkillEffect.IsPvPCombat(ch, tag.Owner)) continue;
                        hasEnemy = true;
                        break;
                    }

                    // 적 근방에만 VFX/SFX 재생
                    if (hasEnemy)
                    {
                        var _icePrefab = ZNetScene.instance?.GetPrefab("fx_DvergerMage_Ice_hit");
                        if (_icePrefab != null)
                            UnityEngine.Object.Instantiate(_icePrefab, hitPoint, Quaternion.identity);
                        VFXManager.PlaySound("sfx_arrow_hit", hitPoint, 5f);
                    }

                    SkillEffect.ApplyArrowRainFrostSlow(hitPoint);
                    SkillEffect.ApplyArrowRainAOEDamage(tag.Owner, hitPoint);
                    return;
                }

                // Case B: 일반 활 발사체 적중 → 화살비 트리거 (water 여부 무관)
                if (__instance.m_skill != Skills.SkillType.Bows) return;

                var player = Player.m_localPlayer;
                if (player == null) return;
                if (!SkillEffect.IsArrowRainReady(player)) return;

                SkillEffect.ConsumeArrowRain(player, hitPoint);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비] OnHit 패치 오류: {ex.Message}");
            }
        }
    }

    // ================================================================
    // 화살비 던전 내 대체 버프 - 공격력 증가 패치
    // ================================================================
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class ArrowRain_DungeonBuff_Damage_Patch
    {
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                var localPlayer = Player.m_localPlayer;
                if (localPlayer == null || __instance == localPlayer) return;

                var attacker = hit.GetAttacker();
                if (attacker != localPlayer) return;

                float bonus = SkillEffect.GetArrowRainDungeonBuffDamageBonus(localPlayer);
                if (bonus > 0f)
                {
                    hit.m_damage.Modify(1f + bonus / 100f);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[화살비 던전버프] Damage 패치 오류: {ex.Message}");
            }
        }
    }
}

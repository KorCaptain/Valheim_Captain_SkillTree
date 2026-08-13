using HarmonyLib;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 폭발 화살 시스템 - R키 액티브 스킬
    /// 활성화 시간 동안 자동 발사, 착탄 2m 내 적도 적중 처리
    /// </summary>
    public static partial class SkillEffect
    {
        // === 폭발 화살 관련 변수 ===
        private static Dictionary<Player, float> explosiveArrowCooldown = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> explosiveArrowReady = new Dictionary<Player, bool>();

        // Archer Lv2: 추가 사용 차지 관리
        private static Dictionary<Player, bool> _archerLv2ExplosiveArrowChargeReady = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> _archerLv2ExplosiveArrowChargeTime = new Dictionary<Player, float>();

        // 버프 상태 VFX 관리 (머리 위 statusailment_01_aura)
        private static Dictionary<Player, GameObject> explosiveArrowStatusEffects = new Dictionary<Player, GameObject>();
        private static GameObject cachedExplosiveArrowStatusPrefab = null;

        // 버프 만료 타이머 코루틴 추적
        private static readonly Dictionary<Player, Coroutine> _explosiveArrowExpiry =
            new Dictionary<Player, Coroutine>();

        /// <summary>
        /// 폭발 화살 R키 액티브 스킬 실행
        /// </summary>
        public static void ExecuteExplosiveArrow(Player player)
        {
            try
            {
                Plugin.Log.LogInfo("[폭발 화살] R키 실행 시작");

                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogInfo("[폭발 화살] 플레이어 없음 또는 사망");
                    return;
                }

                // 1. 스킬 보유 확인
                if (!HasSkill("bow_Step6_critboost"))
                {
                    ShowSkillEffectText(player, L.Get("explosive_arrow_skill_required"), Color.red, SkillEffectTextType.Standard);
                    Plugin.Log.LogInfo("[폭발 화살] 스킬 미보유");
                    return;
                }

                // 2. 쿨타임 확인
                bool hasArcherLv2 = SkillTreeManager.Instance != null
                    && SkillTreeManager.Instance.GetSkillLevel("Archer") >= 2;
                bool hasExtraCharge = hasArcherLv2
                    && _archerLv2ExplosiveArrowChargeReady.TryGetValue(player, out bool eaChargeReady) && eaChargeReady
                    && _archerLv2ExplosiveArrowChargeTime.TryGetValue(player, out float eaChargeTime)
                    && (Time.time - eaChargeTime) < 30f;

                if (!hasExtraCharge)
                {
                    if (!explosiveArrowCooldown.TryGetValue(player, out float eaCooldown))
                        eaCooldown = 0f;

                    if (Time.time - eaCooldown < SkillTreeConfig.BowExplosiveArrowCooldownValue)
                    {
                        float remainingCooldown = SkillTreeConfig.BowExplosiveArrowCooldownValue - (Time.time - eaCooldown);
                        ShowSkillEffectText(player, L.Get("cooldown_format", $"{remainingCooldown:F1}"), Color.yellow, SkillEffectTextType.Passive);
                        Plugin.Log.LogInfo($"[폭발 화살] 쿨타임 중 - 남은 시간: {remainingCooldown:F1}초");
                        return;
                    }
                }

                // 3. 활 착용 확인
                if (!IsUsingBowForExplosive(player))
                {
                    ShowSkillEffectText(player, L.Get("bow_equip_required"), Color.red, SkillEffectTextType.Standard);
                    Plugin.Log.LogInfo("[폭발 화살] 활 미착용");
                    return;
                }

                // 4. 스태미나 소모 확인 (플랫 수치)
                float requiredStamina = SkillTreeConfig.BowExplosiveArrowStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    ShowSkillEffectText(player, L.Get("stamina_insufficient"), Color.red, SkillEffectTextType.Standard);
                    Plugin.Log.LogInfo($"[폭발 화살] 스태미나 부족 - 필요: {requiredStamina:F1}, 현재: {player.GetStamina():F1}");
                    return;
                }

                // 5. 스킬 발동 - ready 상태 + 만료 타이머 시작
                explosiveArrowReady[player] = true;
                StartExplosiveArrowExpiry(player);

                if (hasExtraCharge)
                {
                    _archerLv2ExplosiveArrowChargeReady[player] = false;
                    explosiveArrowCooldown[player] = Time.time;
                    ActiveSkillCooldownRegistry.SetCooldownForSkill("R", "bow_Step6_critboost", SkillTreeConfig.BowExplosiveArrowCooldownValue);
                    Plugin.Log.LogDebug("[폭발화살] Archer Lv2 2번째 사용 → 쿨타임 시작");
                }
                else if (hasArcherLv2)
                {
                    _archerLv2ExplosiveArrowChargeReady[player] = true;
                    _archerLv2ExplosiveArrowChargeTime[player] = Time.time;
                    Plugin.Log.LogDebug("[폭발화살] Archer Lv2 1번째 사용 → 추가 사용 준비");
                }
                else
                {
                    explosiveArrowCooldown[player] = Time.time;
                    ActiveSkillCooldownRegistry.SetCooldownForSkill("R", "bow_Step6_critboost", SkillTreeConfig.BowExplosiveArrowCooldownValue);
                }

                player.UseStamina(requiredStamina);
                PlayExplosiveArrowActivationEffects(player);
                ShowSkillEffectText(player, "💥 " + L.Get("explosive_arrow_ready"), new Color(1f, 0.4f, 0f), SkillEffectTextType.Combat);
                Plugin.Log.LogInfo("[폭발 화살] [OK] R키 액티브 스킬 발동 완료 - 버프 활성화");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 폭발 화살 R키 활성화 시 VFX/SFX 효과
        /// </summary>
        private static void PlayExplosiveArrowActivationEffects(Player player)
        {
            try
            {
                VFXManager.PlaySoundLoud("sfx_reload_start", player.transform.position, 5f, 3f);

                var buff01Prefab = VFXManager.GetVFXPrefab("buff_01");
                if (buff01Prefab != null)
                {
                    var buff01Effect = UnityEngine.Object.Instantiate(
                        buff01Prefab,
                        player.transform.position + Vector3.up * 0.5f,
                        player.transform.rotation
                    );
                    buff01Effect.transform.SetParent(player.transform, false);
                    buff01Effect.transform.localPosition = Vector3.up * 0.5f;
                    buff01Effect.transform.localScale = Vector3.one * 0.8f;
                    UnityEngine.Object.Destroy(buff01Effect, 2f);
                }

                PlayExplosiveArrowStatusEffect(player);
                VFXManager.PlaySoundLoud("sfx_reload_dverger_done", player.transform.position, 5f, 3f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살] 활성화 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 폭발 화살 상태 효과 VFX (머리 위, 자동발사 종료까지 지속)
        /// </summary>
        private static void PlayExplosiveArrowStatusEffect(Player player)
        {
            try
            {
                if (cachedExplosiveArrowStatusPrefab == null)
                {
                    cachedExplosiveArrowStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedExplosiveArrowStatusPrefab == null)
                    {
                        Plugin.Log.LogWarning("[폭발 화살] statusailment_01_aura 프리팹을 찾을 수 없음");
                        return;
                    }
                }

                if (explosiveArrowStatusEffects.TryGetValue(player, out var eaPrevStatus) && eaPrevStatus != null)
                {
                    UnityEngine.Object.Destroy(eaPrevStatus);
                    explosiveArrowStatusEffects.Remove(player);
                }

                var headPosition = player.transform.position + Vector3.up * 2.0f;
                var statusInstance = UnityEngine.Object.Instantiate(
                    cachedExplosiveArrowStatusPrefab,
                    headPosition,
                    Quaternion.identity
                );
                statusInstance.transform.SetParent(player.transform, false);
                statusInstance.transform.localPosition = Vector3.up * 2.0f;
                statusInstance.transform.localScale = Vector3.one * 0.6f;
                explosiveArrowStatusEffects[player] = statusInstance;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살] 상태 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 폭발 화살 준비 상태 확인
        /// </summary>
        public static bool IsExplosiveArrowReady(Player player)
        {
            if (!explosiveArrowReady.TryGetValue(player, out bool isReady)) return false;
            if (isReady)
                Plugin.Log.LogInfo($"[폭발 화살] {player.GetPlayerName()} 폭발 화살 준비 상태 확인됨");
            return isReady;
        }

        /// <summary>
        /// 폭발 화살 소비 처리 (명중/만료 시 호출)
        /// </summary>
        public static void ConsumeExplosiveArrow(Player player)
        {
            if (explosiveArrowReady.TryGetValue(player, out bool eaConsume) && eaConsume)
            {
                explosiveArrowReady[player] = false;
                StopExplosiveArrowExpiry(player);

                if (explosiveArrowStatusEffects.TryGetValue(player, out var eaConsumeStatus) && eaConsumeStatus != null)
                {
                    UnityEngine.Object.Destroy(eaConsumeStatus);
                    explosiveArrowStatusEffects.Remove(player);
                }

                ShowSkillEffectText(player, "💥 " + L.Get("explosive_arrow_fire"), Color.red, SkillEffectTextType.Critical);
            }
        }

        /// <summary>
        /// 활 착용 여부 확인 (폭발 화살용)
        /// </summary>
        private static bool IsUsingBowForExplosive(Player player)
        {
            var currentWeapon = player.GetCurrentWeapon();
            if (currentWeapon == null) return false;
            return currentWeapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow;
        }

        /// <summary>
        /// 폭발 화살 정리 (플레이어 사망 시 호출)
        /// </summary>
        public static void CleanupExplosiveArrowOnDeath(Player player)
        {
            try
            {
                StopExplosiveArrowExpiry(player);
                explosiveArrowCooldown.Remove(player);
                explosiveArrowReady.Remove(player);
                _archerLv2ExplosiveArrowChargeReady.Remove(player);
                _archerLv2ExplosiveArrowChargeTime.Remove(player);

                if (explosiveArrowStatusEffects.TryGetValue(player, out var eaDestroyStatus))
                {
                    if (eaDestroyStatus != null)
                    {
                        try { UnityEngine.Object.Destroy(eaDestroyStatus); } catch { }
                    }
                    explosiveArrowStatusEffects.Remove(player);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[폭발 화살] 정리 실패: {ex.Message}");
            }
        }

        // ── 버프 만료 타이머 ───────────────────────────────────────────────────

        private static void StartExplosiveArrowExpiry(Player player)
        {
            StopExplosiveArrowExpiry(player);
            var c = Plugin.Instance.StartCoroutine(ExplosiveArrowExpiryCoroutine(player));
            _explosiveArrowExpiry[player] = c;
        }

        private static void StopExplosiveArrowExpiry(Player player)
        {
            if (_explosiveArrowExpiry.TryGetValue(player, out var c) && c != null)
            {
                try { Plugin.Instance.StopCoroutine(c); } catch { }
            }
            _explosiveArrowExpiry.Remove(player);
        }

        private static IEnumerator ExplosiveArrowExpiryCoroutine(Player player)
        {
            yield return new WaitForSeconds(Bow_Config.BowExplosiveArrowActivationDuration);

            if (IsExplosiveArrowReady(player))
            {
                ConsumeExplosiveArrow(player);
                Plugin.Log.LogInfo("[폭발 화살] 버프 만료");
            }
            _explosiveArrowExpiry.Remove(player);
        }
    }

    /// <summary>
    /// 폭발 화살 히트 감지 패치 - Character.Damage 방식으로 안전하게 구현
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    [HarmonyPriority(Priority.VeryLow)]
    public class ExplosiveArrow_Character_Damage_Patch
    {
        [HarmonyPrefix]
        private static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer()) return;

                var attacker = hit.GetAttacker() as Player;
                if (attacker == null) return;

                Plugin.Log.LogDebug($"[폭발 화살 히트] {attacker.GetPlayerName()}이 {__instance.name}에게 데미지");

                if (!SkillEffect.IsExplosiveArrowReady(attacker))
                {
                    Plugin.Log.LogDebug("[폭발 화살 히트] 폭발 화살이 준비되지 않음");
                    return;
                }

                if (!IsBowAttack(hit, attacker))
                {
                    Plugin.Log.LogDebug("[폭발 화살 히트] 활 공격이 아님");
                    return;
                }

                Plugin.Log.LogInfo($"[폭발 화살] [OK] 모든 조건 만족! {__instance.name}에게 폭발 적용");

                SkillEffect.ConsumeExplosiveArrow(attacker);
                ApplyExplosionEffect(__instance.GetCenterPoint(), __instance, attacker, hit);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살 히트] 오류: {ex.Message}");
            }
        }

        private static bool IsBowAttack(HitData hit, Player attacker)
        {
            try
            {
                if (hit.m_skill == Skills.SkillType.Bows)
                {
                    Plugin.Log.LogDebug("[폭발 화살] Bow 스킬 감지");
                    return true;
                }

                var weapon = attacker.GetCurrentWeapon();
                if (weapon != null && weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                {
                    Plugin.Log.LogDebug($"[폭발 화살] 활 무기 감지: {weapon.m_shared.m_name}");
                    return true;
                }

                Plugin.Log.LogDebug($"[폭발 화살] 활 공격 아님 - 스킬: {hit.m_skill}, 무기: {weapon?.m_shared.m_name}");
                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[활 공격 확인] 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 폭발 효과 적용. originalHit가 null이면 직접 명중 없이 근접 트리거된 경우.
        /// </summary>
        internal static void ApplyExplosionEffect(Vector3 hitPoint, Character hitTarget, Player attacker, HitData originalHit)
        {
            try
            {
                Plugin.Log.LogInfo($"[폭발 화살] 폭발 효과 시작 - 위치: {hitPoint}");

                // 직접 명중 시에만 원본 히트에 추가 데미지
                if (originalHit != null)
                    AddExplosiveDamageToHit(originalHit, attacker);

                PlayExplosionEffects(hitPoint);
                ApplyExplosionAreaDamage(hitPoint, attacker);

                Plugin.Log.LogInfo("[폭발 화살] [OK] 폭발 효과 적용 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 효과 적용] 오류: {ex.Message}");
            }
        }

        private static void AddExplosiveDamageToHit(HitData hit, Player attacker)
        {
            try
            {
                var weapon = attacker.GetCurrentWeapon();
                if (weapon == null) return;

                float bowSkillFactor = attacker.GetSkillFactor(Skills.SkillType.Bows);
                var baseDamage = weapon.GetDamage(0, bowSkillFactor);
                float totalBaseDamage = baseDamage.GetTotalDamage();
                int explosiveLevel = SkillTreeManager.Instance?.GetSkillLevel("bow_Step6_critboost") ?? 1;
                float levelBonus = (explosiveLevel - 1) * Bow_Config.BowExplosiveArrowLevelBonusValue;
                float explosiveDamage = totalBaseDamage * ((SkillTreeConfig.BowExplosiveArrowDamageValue + levelBonus) / 100f);

                Plugin.Log.LogInfo($"[폭발 데미지] 원본 히트에 추가 데미지 적용: +{explosiveDamage:F0} (기본: {totalBaseDamage:F0}, 레벨: {explosiveLevel}, 보너스: +{levelBonus}%)");
                hit.m_damage.m_fire += explosiveDamage;
                SkillEffect.DrawFloatingText(attacker, "💥 " + L.Get("explosion_damage", $"{explosiveDamage:F0}"), Color.red);
                Plugin.Log.LogInfo($"[폭발 화살] 원본 히트에 폭발 데미지 {explosiveDamage:F0} 추가 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[원본 히트 폭발 데미지] 오류: {ex.Message}");
            }
        }

        private static void PlayExplosionEffects(Vector3 position)
        {
            try
            {
                Plugin.Log.LogInfo($"[폭발 화살] 폭발 이팩트 생성 시작 - 위치: {position}");

                var _blobPrefab = ZNetScene.instance?.GetPrefab("fx_blobLava_explosion");
                if (_blobPrefab != null)
                    UnityEngine.Object.Instantiate(_blobPrefab, position, Quaternion.identity);

                var _siegePrefab = ZNetScene.instance?.GetPrefab("fx_siegebomb_explosion");
                if (_siegePrefab != null)
                    UnityEngine.Object.Instantiate(_siegePrefab, position + Vector3.up * 0.5f, Quaternion.identity);

                VFXManager.PlaySound("sfx_blobLava_explosion", position, 5f);
                VFXManager.PlaySound("sfx_unstablerock_explosion", position, 5f);
                CreateLightEffect(position);

                Plugin.Log.LogInfo("[폭발 화살] [OK] VFXManager로 폭발 이팩트 생성 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 이팩트] 오류: {ex.Message}");
            }
        }

        private static void CreateLightEffect(Vector3 position)
        {
            try
            {
                var go = new GameObject("ExplosiveArrowLight");
                go.transform.position = position;
                var light = go.AddComponent<Light>();
                light.color = new Color(1f, 0.4f, 0f, 1f);
                light.intensity = 10f;
                light.range = 12f;
                light.type = LightType.Point;
                UnityEngine.Object.Destroy(go, 2f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[빛 효과] 오류: {ex.Message}");
            }
        }

        private static void ApplyExplosionAreaDamage(Vector3 center, Player attacker)
        {
            try
            {
                float radius = SkillTreeConfig.BowExplosiveArrowRadiusValue;
                Plugin.Log.LogInfo($"[폭발 화살] 범위 데미지 시작 - 반경: {radius}m");

                var colliders = Physics.OverlapSphere(center, radius);
                int hitCount = 0;

                foreach (var collider in colliders)
                {
                    var character = collider.GetComponent<Character>();
                    if (character != null && character != attacker && !character.IsDead())
                    {
                        if (BaseAI.IsEnemy(attacker, character) || character.GetComponent<MonsterAI>() != null)
                        {
                            ApplyAreaExplosiveDamage(character, attacker);
                            hitCount++;
                        }
                    }
                }

                Plugin.Log.LogInfo($"[폭발 화살] 범위 데미지 완료 - {hitCount}개 대상 적중");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[범위 폭발 데미지] 오류: {ex.Message}");
            }
        }

        private static void ApplyAreaExplosiveDamage(Character target, Player attacker)
        {
            try
            {
                var weapon = attacker.GetCurrentWeapon();
                if (weapon == null) return;

                var baseDamage = weapon.GetDamage();
                float totalBaseDamage = baseDamage.GetTotalDamage();
                int explosiveLevelArea = SkillTreeManager.Instance?.GetSkillLevel("bow_Step6_critboost") ?? 1;
                float areaPercent = explosiveLevelArea switch {
                    2 => 70f, 3 => 85f, 4 => 100f,
                    5 => 115f, 6 => 130f, 7 => 150f,
                    _ => 55f
                };
                float areaDamage = totalBaseDamage * (areaPercent / 100f);

                Plugin.Log.LogInfo($"[범위 폭발] {target.name}에게 범위 데미지: {areaDamage:F0} (레벨: {explosiveLevelArea})");

                var hitData = new HitData();
                hitData.m_damage.m_fire = areaDamage;
                hitData.m_point = target.GetCenterPoint();
                hitData.m_dir = (target.transform.position - attacker.transform.position).normalized;
                hitData.m_hitCollider = target.GetComponent<Collider>();
                hitData.SetAttacker(attacker);
                hitData.m_skill = Skills.SkillType.Bows;

                target.Damage(hitData);
                SkillEffect.DrawFloatingText(target as Player ?? attacker, $"💥 {areaDamage:F0}", new Color(1f, 0.7f, 0f, 1f));
                Plugin.Log.LogInfo($"[폭발 화살] {target.name}에게 범위 데미지 {areaDamage:F0} 적용 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[범위 데미지] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 폭발 화살 환경 히트 패치 - 착탄점 2m 내 적도 폭발 트리거
    /// </summary>
    [HarmonyPatch(typeof(Projectile), "OnHit",
        new Type[] { typeof(Collider), typeof(Vector3), typeof(bool), typeof(Vector3) })]
    [HarmonyPriority(Priority.Low)]
    public class ExplosiveArrow_ProjectileHit_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Projectile __instance,
            Collider collider, Vector3 hitPoint, bool water, Vector3 normal)
        {
            try
            {
                if (water) return;
                if (__instance.GetComponent<ArrowRainProjectileTag>() != null) return;
                if (__instance.m_skill != Skills.SkillType.Bows) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

                if (!SkillEffect.IsExplosiveArrowReady(player)) return;

                // 착탄점 2m 이내 적 검사
                var nearbyColliders = Physics.OverlapSphere(hitPoint, 2f);
                Character proximityTarget = null;
                float minD = float.MaxValue;
                foreach (var col in nearbyColliders)
                {
                    var chr = col.GetComponent<Character>();
                    if (chr == null || chr == (Character)player || chr.IsDead()) continue;
                    if (!BaseAI.IsEnemy(player, chr) && chr.GetComponent<MonsterAI>() == null) continue;
                    float d = Vector3.Distance(hitPoint, chr.transform.position);
                    if (d < minD) { minD = d; proximityTarget = chr; }
                }

                if (proximityTarget != null)
                {
                    Plugin.Log.LogInfo($"[폭발화살] 2m 근접 적중! {proximityTarget.name} (거리: {minD:F2}m)");
                    SkillEffect.ConsumeExplosiveArrow(player);
                    ExplosiveArrow_Character_Damage_Patch.ApplyExplosionEffect(hitPoint, proximityTarget, player, null);
                }
                // 근처 적 없음 → 소비하지 않고 계속 발사
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발화살] OnHit 패치 오류: {ex.Message}");
            }
        }
    }
}

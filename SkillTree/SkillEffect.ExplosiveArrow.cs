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
    /// 석궁 단 한발과 똑같은 방식으로 구현 (즉시 발동, 다음 한 발만)
    /// Character.Damage 패치를 사용하여 안전하게 구현
    /// </summary>
    public static partial class SkillEffect
    {
        // === 폭발 화살 관련 변수 ===
        private static Dictionary<Player, float> explosiveArrowCooldown = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> explosiveArrowReady = new Dictionary<Player, bool>();

        // Archer Lv2: 추가 사용 차지 관리
        private static Dictionary<Player, bool> _archerLv2ExplosiveArrowChargeReady = new Dictionary<Player, bool>();

        // 버프 상태 VFX 관리 (머리 위 statusailment_01_aura)
        private static Dictionary<Player, GameObject> explosiveArrowStatusEffects = new Dictionary<Player, GameObject>();
        private static GameObject cachedExplosiveArrowStatusPrefab = null;
        
        /// <summary>
        /// 폭발 화살 R키 액티브 스킬 실행 (석궁 단 한발과 동일)
        /// </summary>
        public static void ExecuteExplosiveArrow(Player player)
        {
            try
            {
                Plugin.Log.LogInfo("[폭발 화살] R키 실행 시작");
                
                // 기본 조건 검사
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

                // 2. 쿨타임 확인 (석궁 단 한발과 동일)
                bool hasArcherLv2 = SkillTreeManager.Instance != null
                    && SkillTreeManager.Instance.GetSkillLevel("Archer") >= 2;
                bool hasExtraCharge = hasArcherLv2
                    && _archerLv2ExplosiveArrowChargeReady.TryGetValue(player, out bool eaChargeReady) && eaChargeReady;

                // Archer Lv2 추가 차지 있으면 쿨타임 체크 건너뜀
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

                // 4. 스태미나 소모 확인
                float maxStamina = player.GetMaxStamina();
                float requiredStamina = maxStamina * (SkillTreeConfig.BowExplosiveArrowStaminaCostValue / 100f);
                if (player.GetStamina() < requiredStamina)
                {
                    ShowSkillEffectText(player, L.Get("stamina_insufficient"), Color.red, SkillEffectTextType.Standard);
                    Plugin.Log.LogInfo($"[폭발 화살] 스태미나 부족 - 필요: {requiredStamina:F1}, 현재: {player.GetStamina():F1}");
                    return;
                }

                // 5. 스킬 발동 (석궁 단 한발과 동일 - 즉시 준비 상태)
                explosiveArrowReady[player] = true;

                if (hasExtraCharge)
                {
                    // 2번째 사용: 차지 소비 → 쿨타임 시작
                    _archerLv2ExplosiveArrowChargeReady[player] = false;
                    explosiveArrowCooldown[player] = Time.time;
                    ActiveSkillCooldownRegistry.SetCooldownForSkill("R", "bow_Step6_critboost", SkillTreeConfig.BowExplosiveArrowCooldownValue);
                    Plugin.Log.LogDebug("[폭발화살] Archer Lv2 2번째 사용 → 쿨타임 시작");
                }
                else if (hasArcherLv2)
                {
                    // 1번째 사용: 차지 부여, 쿨타임 없음
                    _archerLv2ExplosiveArrowChargeReady[player] = true;
                    Plugin.Log.LogDebug("[폭발화살] Archer Lv2 1번째 사용 → 추가 사용 준비");
                }
                else
                {
                    explosiveArrowCooldown[player] = Time.time;
                    ActiveSkillCooldownRegistry.SetCooldownForSkill("R", "bow_Step6_critboost", SkillTreeConfig.BowExplosiveArrowCooldownValue);
                }

                // 스태미나 소모
                player.UseStamina(requiredStamina);

                // VFX/SFX 효과 (석궁 단 한발과 동일한 방식)
                PlayExplosiveArrowActivationEffects(player);

                // 성공 메시지 (석궁과 동일한 스타일)
                ShowSkillEffectText(player, "💥 " + L.Get("explosive_arrow_ready"), new Color(1f, 0.4f, 0f), SkillEffectTextType.Combat);
                Plugin.Log.LogInfo("[폭발 화살] ✅ R키 액티브 스킬 발동 완료 - 다음 한 발 준비됨");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살] 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 폭발 화살 R키 활성화 시 VFX/SFX 효과 (아처 멀티샷과 동일한 방식)
        /// </summary>
        private static void PlayExplosiveArrowActivationEffects(Player player)
        {
            try
            {
                // 0. 장전 시작 사운드 (sfx_reload_start) - 철컹하는 장전 소리
                VFXManager.PlaySound("sfx_reload_start", player.transform.position, 5f);

                // 1. 버프 활성화 VFX (buff_01) - 발밑 효과, 2초간
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

                    // 2초 후 제거
                    UnityEngine.Object.Destroy(buff01Effect, 2f);
                }

                // 2. 버프 상태 VFX (statusailment_01_aura) - 머리 위 효과, 화살 발사까지 지속
                PlayExplosiveArrowStatusEffect(player);

                // 3. 버프 활성화 사운드 (sfx_reload_dverger_done)
                VFXManager.PlaySound("sfx_reload_dverger_done", player.transform.position, 5f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살] 활성화 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 폭발 화살 상태 효과 VFX (statusailment_01_aura - 머리 위, 화살 발사까지 지속)
        /// </summary>
        private static void PlayExplosiveArrowStatusEffect(Player player)
        {
            try
            {
                // 캐시된 프리팹이 없으면 한 번만 로드
                if (cachedExplosiveArrowStatusPrefab == null)
                {
                    cachedExplosiveArrowStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedExplosiveArrowStatusPrefab == null)
                    {
                        Plugin.Log.LogWarning("[폭발 화살] statusailment_01_aura 프리팹을 찾을 수 없음");
                        return;
                    }
                }

                // 기존 상태 효과가 있으면 제거
                if (explosiveArrowStatusEffects.TryGetValue(player, out var eaPrevStatus) && eaPrevStatus != null)
                {
                    UnityEngine.Object.Destroy(eaPrevStatus);
                    explosiveArrowStatusEffects.Remove(player);
                }

                // statusailment_01_aura 효과 생성 (머리 위 2미터)
                var headPosition = player.transform.position + Vector3.up * 2.0f;
                var statusInstance = UnityEngine.Object.Instantiate(
                    cachedExplosiveArrowStatusPrefab,
                    headPosition,
                    Quaternion.identity
                );

                // 캐릭터를 따라다니도록 부모 설정
                statusInstance.transform.SetParent(player.transform, false);
                statusInstance.transform.localPosition = Vector3.up * 2.0f;
                statusInstance.transform.localScale = Vector3.one * 0.6f;

                // 상태 효과 인스턴스 저장 (화살 발사 시 제거하기 위해)
                explosiveArrowStatusEffects[player] = statusInstance;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살] 상태 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 폭발 화살 준비 상태 확인 (석궁 단 한발과 동일)
        /// </summary>
        public static bool IsExplosiveArrowReady(Player player)
        {
            if (!explosiveArrowReady.TryGetValue(player, out bool isReady)) return false;

            if (isReady)
            {
                Plugin.Log.LogInfo($"[폭발 화살] {player.GetPlayerName()} 폭발 화살 준비 상태 확인됨");
            }
            
            return isReady;
        }
        
        /// <summary>
        /// 폭발 화살 사용 처리 (발사 후 호출)
        /// </summary>
        public static void ConsumeExplosiveArrow(Player player)
        {
            if (explosiveArrowReady.TryGetValue(player, out bool eaConsume) && eaConsume)
            {
                explosiveArrowReady[player] = false;

                // 상태 효과 제거 (statusailment_01_aura)
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

            // 활 타입 확인
            return currentWeapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow;
        }

        /// <summary>
        /// 폭발 화살 정리 메서드 (플레이어 사망 시 호출)
        /// </summary>
        public static void CleanupExplosiveArrowOnDeath(Player player)
        {
            try
            {
                explosiveArrowCooldown.Remove(player);
                explosiveArrowReady.Remove(player);
                _archerLv2ExplosiveArrowChargeReady.Remove(player);

                // 상태 효과 GameObject 제거 (statusailment_01_aura)
                if (explosiveArrowStatusEffects.TryGetValue(player, out var eaDestroyStatus))
                {
                    if (eaDestroyStatus != null)
                    {
                        try
                        {
                            UnityEngine.Object.Destroy(eaDestroyStatus);
                        }
                        catch { }
                    }
                    explosiveArrowStatusEffects.Remove(player);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[폭발 화살] 정리 실패: {ex.Message}");
            }
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
                // 공격자가 플레이어인지 확인
                if (hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer()) return;
                
                var attacker = hit.GetAttacker() as Player;
                if (attacker == null) return;
                
                Plugin.Log.LogDebug($"[폭발 화살 히트] {attacker.GetPlayerName()}이 {__instance.name}에게 데미지");
                
                // 폭발 화살 준비 상태 확인
                if (!SkillEffect.IsExplosiveArrowReady(attacker))
                {
                    Plugin.Log.LogDebug("[폭발 화살 히트] 폭발 화살이 준비되지 않음");
                    return;
                }
                
                // 활 공격인지 확인
                if (!IsBowAttack(hit, attacker))
                {
                    Plugin.Log.LogDebug("[폭발 화살 히트] 활 공격이 아님");
                    return;
                }
                
                Plugin.Log.LogInfo($"[폭발 화살] ✅ 모든 조건 만족! {__instance.name}에게 폭발 적용");
                
                // 폭발 화살 소모
                SkillEffect.ConsumeExplosiveArrow(attacker);
                
                // 폭발 효과 적용
                ApplyExplosionEffect(__instance.GetCenterPoint(), __instance, attacker, hit);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 화살 히트] 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 활 공격인지 확인
        /// </summary>
        private static bool IsBowAttack(HitData hit, Player attacker)
        {
            try
            {
                // 1. 스킬 타입 확인
                if (hit.m_skill == Skills.SkillType.Bows)
                {
                    Plugin.Log.LogDebug("[폭발 화살] Bow 스킬 감지");
                    return true;
                }
                
                // 2. 현재 착용 무기 확인
                var weapon = attacker.GetCurrentWeapon();
                if (weapon != null && weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                {
                    Plugin.Log.LogDebug($"[폭발 화살] 활 무기 감지: {weapon.m_shared.m_name}");
                    return true;
                }
                
                // 3. 히트 데이터 타입 확인 (스킬 우선 체크)
                if (hit.m_skill != Skills.SkillType.None)
                {
                    Plugin.Log.LogDebug($"[폭발 화살] 스킬 타입 확인: {hit.m_skill}");
                    // 활 스킬이 아니어도 무기가 활이면 활 공격으로 처리
                }
                
                Plugin.Log.LogDebug($"[폭발 화살] 활 공격 아님 - 스킬: {hit.m_skill}, 무기: {weapon?.m_shared.m_name}, 히트타입: {hit.m_hitType}");
                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[활 공격 확인] 오류: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 폭발 효과 적용
        /// </summary>
        private static void ApplyExplosionEffect(Vector3 hitPoint, Character hitTarget, Player attacker, HitData originalHit)
        {
            try
            {
                Plugin.Log.LogInfo($"[폭발 화살] 폭발 효과 시작 - 위치: {hitPoint}");
                
                // 1. 원본 데미지에 폭발 데미지 추가
                AddExplosiveDamageToHit(originalHit, attacker);
                
                // 2. VFX 효과 (간단한 빛 효과)
                PlayExplosionEffects(hitPoint);
                
                // 3. 범위 데미지 적용
                ApplyExplosionAreaDamage(hitPoint, attacker);
                
                Plugin.Log.LogInfo("[폭발 화살] ✅ 폭발 효과 적용 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 효과 적용] 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 원본 히트에 폭발 데미지 추가 (즉시 적용)
        /// </summary>
        private static void AddExplosiveDamageToHit(HitData hit, Player attacker)
        {
            try
            {
                var weapon = attacker.GetCurrentWeapon();
                if (weapon == null) return;
                
                float bowSkillFactor = attacker.GetSkillFactor(Skills.SkillType.Bows);
                var baseDamage = weapon.GetDamage(0, bowSkillFactor);
                float totalBaseDamage = baseDamage.GetTotalDamage();
                float explosiveDamage = totalBaseDamage * (SkillTreeConfig.BowExplosiveArrowDamageValue / 100f);
                
                Plugin.Log.LogInfo($"[폭발 데미지] 원본 히트에 추가 데미지 적용: +{explosiveDamage:F0} (기본: {totalBaseDamage:F0})");
                
                // 기존 데미지에 폭발 데미지 추가 (화염 데미지로)
                hit.m_damage.m_fire += explosiveDamage;
                
                // 플로팅 텍스트
                SkillEffect.DrawFloatingText(attacker, "💥 " + L.Get("explosion_damage", $"{explosiveDamage:F0}"), Color.red);
                
                Plugin.Log.LogInfo($"[폭발 화살] 원본 히트에 폭발 데미지 {explosiveDamage:F0} 추가 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[원본 히트 폭발 데미지] 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 폭발 VFX/SFX 효과 - VFXManager 사용 (발헤임 기본 VFX)
        /// </summary>
        private static void PlayExplosionEffects(Vector3 position)
        {
            try
            {
                Plugin.Log.LogInfo($"[폭발 화살] 폭발 이팩트 생성 시작 - 위치: {position}");

                // 1. 주 폭발 효과 (라바 블롭 폭발)
                {
                    var _blobPrefab = ZNetScene.instance?.GetPrefab("fx_blobLava_explosion");
                    if (_blobPrefab != null)
                    {
                        var _blobGo = UnityEngine.Object.Instantiate(_blobPrefab, position, Quaternion.identity);
                    }
                }

                // 2. 추가 폭발 효과 (공성 폭탄 폭발)
                {
                    var _siegePrefab = ZNetScene.instance?.GetPrefab("fx_siegebomb_explosion");
                    if (_siegePrefab != null)
                    {
                        var _siegeGo = UnityEngine.Object.Instantiate(_siegePrefab, position + Vector3.up * 0.5f, Quaternion.identity);
                    }
                }

                // 3. 폭발 사운드 효과 (VFXManager 사운드 메서드 사용)
                VFXManager.PlaySound("sfx_blobLava_explosion", position, 5f);
                VFXManager.PlaySound("sfx_unstablerock_explosion", position, 5f);

                // 4. 간단한 빛 효과 추가 (보조)
                CreateLightEffect(position);

                Plugin.Log.LogInfo("[폭발 화살] ✅ VFXManager로 폭발 이팩트 생성 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발 이팩트] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 보조 빛 효과 생성
        /// </summary>
        private static void CreateLightEffect(Vector3 position)
        {
            try
            {
                var go = new GameObject("ExplosiveArrowLight");
                go.transform.position = position;
                
                var light = go.AddComponent<Light>();
                light.color = new Color(1f, 0.4f, 0f, 1f); // 주황색
                light.intensity = 10f;
                light.range = 12f;
                light.type = LightType.Point;
                
                // 2초 후 삭제
                UnityEngine.Object.Destroy(go, 2f);
                
                Plugin.Log.LogInfo("[폭발 화살] 보조 빛 효과 생성 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[빛 효과] 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 폭발 범위 데미지 적용
        /// </summary>
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
                        // 적대적인 대상만
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
        
        /// <summary>
        /// 범위 내 대상에게 폭발 데미지 적용
        /// </summary>
        private static void ApplyAreaExplosiveDamage(Character target, Player attacker)
        {
            try
            {
                var weapon = attacker.GetCurrentWeapon();
                if (weapon == null) return;
                
                var baseDamage = weapon.GetDamage();
                float totalBaseDamage = baseDamage.GetTotalDamage();
                // 범위 데미지는 70% 적용
                float areaDamage = totalBaseDamage * (SkillTreeConfig.BowExplosiveArrowDamageValue / 100f) * 0.7f;
                
                Plugin.Log.LogInfo($"[범위 폭발] {target.name}에게 범위 데미지: {areaDamage:F0}");
                
                // HitData 생성
                var hitData = new HitData();
                hitData.m_damage.m_fire = areaDamage; // 화염 데미지로 적용
                hitData.m_point = target.GetCenterPoint();
                hitData.m_dir = (target.transform.position - attacker.transform.position).normalized;
                hitData.m_hitCollider = target.GetComponent<Collider>();
                hitData.SetAttacker(attacker);
                hitData.m_skill = Skills.SkillType.Bows;
                
                // 데미지 적용
                target.Damage(hitData);
                
                // 플로팅 텍스트
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
    /// 폭발 화살 환경 히트 패치 - 지면/벽에 맞을 때도 VFX 제거
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
                // 화살비 낙하 화살은 스킵 (ArrowRain 패치에서 처리)
                if (__instance.GetComponent<ArrowRainProjectileTag>() != null) return;
                if (__instance.m_skill != Skills.SkillType.Bows) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

                // ConsumeExplosiveArrow 내부에서 ready=false면 무시 → 중복 호출 안전
                SkillEffect.ConsumeExplosiveArrow(player);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[폭발화살] OnHit 패치 오류: {ex.Message}");
            }
        }
    }
}
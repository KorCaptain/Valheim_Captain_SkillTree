using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 원거리 무기 (활, 석궁) 패시브 스킬 시스템
    /// SkillEffect.cs에서 분리된 원거리 관련 기능들
    /// </summary>
    public static partial class SkillEffect
    {
        /// <summary>
        /// 석궁 연속 발사 코루틴
        /// 시간차로 여러 발의 볼트를 순차 발사
        /// </summary>
        public static IEnumerator FireCrossbowRapidFireSequence(
            Player player,
            ItemDrop.ItemData weapon,
            Vector3 fireDirection,
            int shotCount,
            float delay,
            float damagePercent,
            int boltConsumption)
        {
            // 최근 자동 장전 소모 확인 (2초 이내)
            float lastAutoReloadTime = GetLastAutoReloadConsumedTime(player);
            bool recentAutoReload = (Time.time - lastAutoReloadTime) < 2f;

            // 텍스트 효과 (자동 장전 + 연속 발사 통합 메시지)
            if (recentAutoReload)
            {
                ShowSkillEffectText(player,
                    $"⚡ {L.Get("crossbow_auto_reload_and_rapid_fire", shotCount)}",
                    new Color(1f, 0.8f, 0f),
                    SkillEffectTextType.Combat);
            }
            else
            {
                ShowSkillEffectText(player,
                    $"🎯 {L.Get("crossbow_rapid_fire_activated", shotCount)}",
                    new Color(1f, 0.5f, 0f),
                    SkillEffectTextType.Combat);
            }

            // 볼트 소모 처리 (시작 시 1회만)
            if (boltConsumption > 0)
            {
                var inventory = player.GetInventory();
                var ammo = player.GetAmmoItem();
                if (inventory != null && ammo != null && ammo.m_stack >= boltConsumption)
                {
                    ammo.m_stack -= boltConsumption;

                    if (ammo.m_stack <= 0)
                    {
                        inventory.RemoveItem(ammo);
                    }
                }
                else
                {
                    yield break;
                }
            }

            // 짧은 대기: return false가 원본 발사를 완전히 차단할 시간 확보
            yield return new WaitForSeconds(0.05f);

            for (int i = 0; i < shotCount; i++)
            {
                // 중단 조건 체크
                if (player == null || player.IsDead() || player.GetCurrentWeapon() != weapon)
                {
                    break;
                }

                // 발사 간격 (첫 발은 바로, 이후는 간격 두고)
                if (i > 0)
                {
                    yield return new WaitForSeconds(delay);
                }

                // 발사체 생성
                FireSingleBolt(player, weapon, fireDirection, damagePercent);
            }
        }

        /// <summary>
        /// 단일 볼트 발사 (데미지 비율 적용, 볼트 소모 없음)
        /// </summary>
        private static void FireSingleBolt(
            Player player,
            ItemDrop.ItemData weapon,
            Vector3 fireDirection,
            float damagePercent)
        {
            try
            {
                // 볼트 정보 가져오기 (소모하지 않음)
                var ammo = player.GetAmmoItem();
                if (ammo == null) return;

                var ammoAttack = ammo.m_shared.m_attack;
                if (ammoAttack?.m_attackProjectile == null) return;

                var bowAttack = weapon.m_shared.m_attack;
                if (bowAttack == null) return;

                // 발사 위치
                var spawnPoint = player.transform.position +
                    player.transform.up * 1.5f +
                    fireDirection * 0.5f;

                // 시각 효과 재생
                if (bowAttack.m_startEffect != null)
                {
                    bowAttack.m_startEffect.Create(spawnPoint, Quaternion.identity, null, 1f);
                }

                // 석궁 발사 사운드 재생
                CaptainSkillTree.VFX.VFXManager.PlaySound("sfx_arbalest_fire", spawnPoint, 2f);

                // 프로젝타일 생성
                var projectileObj = UnityEngine.Object.Instantiate(
                    ammoAttack.m_attackProjectile,
                    spawnPoint,
                    Quaternion.LookRotation(fireDirection));

                var projectile = projectileObj.GetComponent<Projectile>();
                if (projectile != null)
                {
                    // 데미지 계산
                    var hitData = new HitData();
                    var fullDamage = weapon.GetDamage();
                    fullDamage.Add(ammo.GetDamage());

                    // 석궁 스킬 보너스 제거 - 컨피그 설정 데미지만 적용
                    // 패시브 스킬은 컨피그 값이 최종 데미지
                    // 데미지 비율 적용 (파라미터로 전달받음)
                    fullDamage.Modify(damagePercent / 100f);

                    hitData.m_damage = fullDamage;
                    hitData.m_point = spawnPoint;
                    hitData.m_dir = fireDirection;
                    hitData.m_attacker = player.GetZDOID();
                    hitData.m_skill = Skills.SkillType.Crossbows;
                    hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;

                    var velocity = fireDirection * bowAttack.m_projectileVel;
                    projectile.Setup(player, velocity, bowAttack.m_projectileAccuracy, hitData, ammo, weapon);

                    // 적중 VFX 설정 (vfx_arrowhit)
                    var hitEffectPrefab = ZNetScene.instance?.GetPrefab("vfx_arrowhit");
                    if (hitEffectPrefab != null && projectile.m_hitEffects != null)
                    {
                        projectile.m_hitEffects.m_effectPrefabs = new EffectList.EffectData[]
                        {
                            new EffectList.EffectData { m_prefab = hitEffectPrefab, m_enabled = true }
                        };
                    }

                    var rigidbody = projectileObj.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        rigidbody.velocity = velocity;
                        rigidbody.isKinematic = false;
                        rigidbody.useGravity = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 볼트 발사 실패: {ex.Message}");
            }
        }

        // === 원거리 스킬 이펙트 데이터 ===
        private static readonly Dictionary<string, SkillEffectData> rangedSkillEffects = new Dictionary<string, SkillEffectData>
        {
            // 활 스킬들
            ["bow_Step6_critboost"] = new SkillEffectData("Buff", "sfx_bow_fire", Color.yellow),
            ["bow_multishot"] = new SkillEffectData("vfx_ashlandsbow_blood_fire", "sfx_bow_fire", new Color(0.2f, 0.8f, 0.2f)),
            
            // 석궁 스킬들
            ["crossbow_Step6_expert"] = new SkillEffectData("vfx_crossbow_lightning_fire", "sfx_bow_draw", Color.cyan),
            ["crossbow_Step2_pierce_all"] = new SkillEffectData("fx_QueenPierceGround", "sfx_HiveQueen_pierce", Color.red),
         ["crossbow_Step4_re"] = new SkillEffectData("fx_turret_reload", "sfx_reload_done", Color.blue),
            ["crossbow_expert_final"] = new SkillEffectData("Star aura", "sfx_build_hammer_metal", new Color(1f, 0.5f, 0f)),
        };

        // === 원거리 스킬 이름 매핑 ===
        private static readonly Dictionary<string, string> rangedSkillNames = new Dictionary<string, string>
        {
            // 활 스킬 이름들
            ["bow_Step6_critboost"] = "폭발 화살",
            
            // 석궁 스킬 이름들
            ["crossbow_Step6_expert"] = "석궁 전문가",
            ["crossbow_draw"] = "석궁 장전 마스터",
            ["crossbow_expert_final"] = "단 한 발",
        };

        // === 원거리 무기 확인 헬퍼 함수들 ===
        // 무기 확인 함수들은 ActiveSkills.cs에 정의되어 있음
        // === 원거리 스킬 확률 시스템 ===
        
        /// <summary>
        /// 28% 확률로 모든 몬스터 관통 (석궁 강한 관통)
        /// </summary>
        public static bool TryPierceAllMonsters()
        {
            return UnityEngine.Random.Range(0f, 1f) < 0.28f;
        }

        /// <summary>
        /// Config 확률로 넉백 발생 (석궁 균형 조준)
        /// </summary>
        public static bool TryKnockbackOnHit(Character target, Vector3 attackerPosition)
        {
            float chance = Crossbow_Config.CrossbowBalanceKnockbackChanceValue / 100f;
            if (UnityEngine.Random.Range(0f, 1f) < chance)
            {
                try
                {
                    // Config 넉백 거리 적용
                    Vector3 knockbackDirection = (target.transform.position - attackerPosition).normalized;
                    float knockbackDistance = Crossbow_Config.CrossbowBalanceKnockbackDistanceValue;

                    // Valheim의 넉백 방식 - Stagger 사용
                    target.Stagger(knockbackDirection);

                    // 추가 넉백 힘 적용
                    var rigidbody = target.GetComponent<Rigidbody>();
                    if (rigidbody != null && !rigidbody.isKinematic)
                    {
                        rigidbody.AddForce(knockbackDirection * knockbackDistance * 2f, ForceMode.Impulse);
                    }

                    return true;
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogWarning($"[석궁 넉백] 넉백 적용 실패: {ex.Message}");
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 30% 확률로 자동 장전 (석궁 자동 장전)
        /// 다음 1회 장전 시 200% 속도 적용
        /// </summary>
        public static bool TryAutoReload(Player player)
        {
            float chance = Crossbow_Config.CrossbowAutoReloadChanceValue / 100f;
            if (UnityEngine.Random.Range(0f, 1f) < chance)
            {
                try
                {
                    // 자동 장전 버프 활성화
                    SetAutoReloadBuff(player, true);

                    // 시각 효과
                    PlaySkillEffect(player, "crossbow_re", player.transform.position);

                    return true;
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogWarning($"[석궁 자동 장전] 자동 장전 실패: {ex.Message}");
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 적의 체력이 50% 이상인지 확인 (석궁 결전의 일격)
        /// </summary>
        public static bool IsHighHealthTarget(Character target)
        {
            if (target == null) return false;
            return target.GetHealthPercentage() >= 0.5f;
        }

        // === 원거리 스킬 데이터 접근자 ===
        
        /// <summary>
        /// 원거리 스킬 이펙트 데이터 가져오기
        /// </summary>
        public static SkillEffectData? GetRangedSkillEffect(string skillId)
        {
            return rangedSkillEffects.TryGetValue(skillId, out var effect) ? effect : (SkillEffectData?)null;
        }

        /// <summary>
        /// 원거리 스킬 이름 가져오기
        /// </summary>
        public static string GetRangedSkillName(string skillId)
        {
            return rangedSkillNames.TryGetValue(skillId, out var name) ? name : skillId;
        }

        /// <summary>
        /// 모든 원거리 스킬 이펙트를 메인 딕셔너리에 추가
        /// </summary>
        public static void RegisterRangedSkillEffects(Dictionary<string, SkillEffectData> mainEffects)
        {
            foreach (var kvp in rangedSkillEffects)
            {
                mainEffects[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// 모든 원거리 스킬 이름을 메인 딕셔너리에 추가
        /// </summary>
        public static void RegisterRangedSkillNames(Dictionary<string, string> mainNames)
        {
            foreach (var kvp in rangedSkillNames)
            {
                mainNames[kvp.Key] = kvp.Value;
            }
        }
        
        /// <summary>
        /// 무기가 원거리 무기인지 확인 (스킬타입 + 프리팹 이름 키워드)
        /// </summary>
        public static bool IsRangedWeapon(ItemDrop.ItemData weapon)
        {
            if (weapon?.m_shared == null) return false;
            
            // 스킬 타입으로 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Bows ||
                weapon.m_shared.m_skillType == Skills.SkillType.Crossbows ||
                weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic)
            {
                return true;
            }
            
            // 프리팹 이름에 키워드가 포함되어 있는지 확인
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            string prefabName = weapon.m_dropPrefab?.name?.ToLower() ?? "";
            
            return ContainsRangedKeyword(weaponName) || ContainsRangedKeyword(prefabName);
        }
        
        /// <summary>
        /// 프리팹 이름에 원거리 무기 관련 키워드가 포함되어 있는지 확인
        /// </summary>
        private static bool ContainsRangedKeyword(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            
            string[] rangedKeywords = { "crossbow", "bow", "staff", "wand" };
            string lowerName = name.ToLower();
            
            foreach (string keyword in rangedKeywords)
            {
                if (lowerName.Contains(keyword))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        /// <summary>
        /// 무기가 지팡이인지 확인 (기존 ItemData 기반 호환성 유지)
        /// 새로운 StaffEquipmentDetector 통합 사용을 권장
        /// </summary>
        public static bool IsStaffWeapon(ItemDrop.ItemData weapon)
        {
            if (weapon?.m_shared == null) return false;

            try
            {
                // 1순위: Valheim 기본 스킬 타입 확인 (가장 확실한 방법)
                if (weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic)
                {
                    return true;
                }

                // 2순위: 프리팹 이름으로 확인 (모드 무기 감지)
                string prefabName = "";
                if (weapon.m_dropPrefab?.name != null)
                {
                    prefabName = weapon.m_dropPrefab.name.ToLower();
                }

                if (prefabName.Contains("staff") || prefabName.Contains("wand") || prefabName.Contains("rod"))
                {
                    Plugin.Log.LogInfo($"[지팡이 감지] 프리팹 이름으로 지팡이 감지: {prefabName} ({weapon.m_shared.m_name})");
                    return true;
                }

                // 3순위: 무기 표시 이름으로 확인 (다국어 및 커스텀 무기 지원)
                string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
                if (weaponName.Contains("지팡이") || weaponName.Contains("staff") ||
                    weaponName.Contains("wand") || weaponName.Contains("rod"))
                {
                    Plugin.Log.LogInfo($"[지팡이 감지] 무기 이름으로 지팡이 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                    return true;
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 감지] 감지 중 오류 발생: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 플레이어가 현재 지팡이/완드를 착용하고 있는지 확인 (최적화된 버전)
        /// </summary>
        public static bool IsPlayerWieldingStaff(Player player)
        {
            return StaffEquipmentDetector.IsWieldingStaffOrWand(player);
        }
    }

    // === 원거리 스킬 Harmony 패치들 ===
    // 모든 원거리 스킬 로직은 Character.Damage 패치에서 처리
    /// 활 및 석궁 공격 보너스 시스템
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class RangedSkills_Character_Damage_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null) return;

                var weapon = player.GetCurrentWeapon();
                
                // 활/석궁 특화는 AttackTree.cs에서 통합 처리됨
                // 원거리 무기별 패시브 스킬들만 여기서 처리
                
                // 활 숙련 스킬: 스킬 습득 시점에 레벨 상승 (공격 시마다 추가 경험치는 제거)
                // 주석 처리: 스킬 습득 시점에만 레벨 상승하도록 변경
                /*
                if (weapon?.m_shared?.m_skillType == Skills.SkillType.Bows && 
                    SkillEffect.HasSkill("bow_Step3_speedshot"))
                {
                    // 이제 ApplyEffect에서 처리하므로 주석 처리
                }
                */
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[원거리 스킬] 피해 패치 오류: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// 원거리 전문가 루트 노드 효과 패치
    /// </summary>
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class RangedSkills_ItemData_GetDamage_RangedExpert_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            try
            {
                if (__instance?.m_shared == null) return;
                
                // Player 변수 선언 (활, 지팡이 섹션 공통 사용)
                var player = Player.m_localPlayer;

                // 원거리 전문가: 활/석궁은 관통 +2
                if (SkillEffect.HasSkill("ranged_root") && SkillEffect.IsRangedWeapon(__instance))
                {
                    if (__instance.m_shared.m_skillType == Skills.SkillType.Bows ||
                        __instance.m_shared.m_skillType == Skills.SkillType.Crossbows)
                    {
                        // 활/석궁: 관통 데미지만 증가
                        if (__result.m_pierce > 0) __result.m_pierce += 2f;
                    }
                }

                // 원거리 전문가: 지팡이/완드 화염 공격 +2 (화염 속성 스킬과 동일한 패턴)
                if (player != null && SkillEffect.HasSkill("ranged_root") &&
                    StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    __result.m_fire += 2f;
                }

                // === 활 패시브 스킬 보너스 ===
                if (__instance.m_shared.m_skillType == Skills.SkillType.Bows)
                {
                    float totalBowBonus = 0f;

                    // Tier 1: 활 전문가 - 활 공격력 보너스
                    if (SkillEffect.HasSkill("bow_Step1_damage"))
                    {
                        totalBowBonus += SkillTreeConfig.BowStep1ExpertDamageBonusValue;
                        Plugin.Log.LogDebug($"[활 전문가] +{SkillTreeConfig.BowStep1ExpertDamageBonusValue}%");
                    }

                    // Tier 3: 관통 - 활 공격력 고정값 증가 (패시브)
                    if (SkillEffect.HasSkill("bow_Step3_silentshot"))
                    {
                        // 고정값 +3을 pierce 데미지에 직접 추가
                        __result.m_pierce += SkillTreeConfig.BowStep3SilentShotDamageBonusValue;
                        Plugin.Log.LogDebug($"[관통] +{SkillTreeConfig.BowStep3SilentShotDamageBonusValue} (고정값)");
                    }

                    // GetDamageHelper를 사용하여 물리 데미지 보너스 적용
                    if (totalBowBonus > 0)
                    {
                        GetDamageHelper.ApplyPhysicalDamageBonus(ref __result, totalBowBonus);
                        Plugin.Log.LogDebug($"[활 스킬] 총 데미지 +{totalBowBonus}%");
                    }
                }

                // === 석궁 패시브 스킬 보너스 ===
                if (__instance.m_shared.m_skillType == Skills.SkillType.Crossbows)
                {
                    float totalCrossbowBonus = 0f;

                    // Tier 1: 석궁 전문가 - 석궁 공격력 +X%
                    if (SkillEffect.HasSkill("crossbow_Step1_damage"))
                    {
                        totalCrossbowBonus += Crossbow_Config.CrossbowExpertDamageBonusValue;
                    }

                    // Tier 3: 정직한 한 발 - 석궁 공격력 +X% (치명타 비활성화 대신)
                    if (SkillEffect.HasSkill("crossbow_Step3_mark"))
                    {
                        totalCrossbowBonus += Crossbow_Config.CrossbowMarkDamageBonusValue;
                    }

                    // GetDamageHelper를 사용하여 물리 데미지 보너스 적용
                    if (totalCrossbowBonus > 0)
                    {
                        GetDamageHelper.ApplyPhysicalDamageBonus(ref __result, totalCrossbowBonus);
                    }
                }

                // 지팡이 전문가: 지팡이 속성 공격력 증가 (착용 기반 최적화)
                if (player != null && SkillEffect.HasSkill("staff_Step1_damage") &&
                    StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    // 설정값 기반 속성 데미지 증가
                    float damageMultiplier = 1.0f + (Staff_Config.StaffExpertDamageValue / 100f);
                    if (__result.m_fire > 0) __result.m_fire *= damageMultiplier;
                    if (__result.m_frost > 0) __result.m_frost *= damageMultiplier;
                    if (__result.m_lightning > 0) __result.m_lightning *= damageMultiplier;
                    if (__result.m_poison > 0) __result.m_poison *= damageMultiplier;
                    if (__result.m_spirit > 0) __result.m_spirit *= damageMultiplier;
                }

                // staff_Step4_range: 화염 속성 (화염 공격 +[CONFIG])
                if (player != null && SkillEffect.HasSkill("staff_Step4_range") &&
                    StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    float fireBonus = Staff_Config.StaffFireDamageBonusValue;
                    __result.m_fire += fireBonus;
                }

                // staff_Step4_reduction: 냉기 속성 (냉기 공격 +[CONFIG])
                if (player != null && SkillEffect.HasSkill("staff_Step4_reduction") &&
                    StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    float frostBonus = Staff_Config.StaffFrostDamageBonusValue;
                    __result.m_frost += frostBonus;
                }

                // staff_Step4_surge: 번개 속성 (번개 공격 +[CONFIG])
                if (player != null && SkillEffect.HasSkill("staff_Step4_surge") &&
                    StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    float lightningBonus = Staff_Config.StaffLightningDamageBonusValue;
                    __result.m_lightning += lightningBonus;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[원거리 스킬→발하임] GetDamage 원거리전문가 패치 오류: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// 지팡이 에이트르 소모량 감소 패치 (정신 집중 스킬)
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.UseEitr))]
    public static class StaffSkills_Player_UseEitr_Patch
    {
        [HarmonyPrefix]
        public static void Prefix(Player __instance, ref float v)
        {
            try
            {
                if (__instance == null) return;
                
                // 정신 집중: 에이트르 소모량 감소 (착용 기반 최적화)
                if (SkillEffect.HasSkill("staff_Step2_focus") && StaffEquipmentDetector.IsWieldingStaffOrWand(__instance))
                {
                    float reductionPercent = Staff_Config.StaffFocusEitrReductionValue / 100f;
                    v *= (1.0f - reductionPercent);
                }

                // staff_Step3_amp: 마법 증폭 - 에이트르 소모 증가 (디버프)
                if (SkillEffect.HasSkill("staff_Step3_amp") && StaffEquipmentDetector.IsWieldingStaffOrWand(__instance))
                {
                    float increasePercent = Staff_Config.StaffAmpEitrCostIncreaseValue / 100f;
                    v *= (1.0f + increasePercent);
                }

                // staff_Step5_archmage: 행운 마력 - 확률로 에이트르 무소모 (착용 기반 최적화)
                if (SkillEffect.HasSkill("staff_Step5_archmage") && StaffEquipmentDetector.IsWieldingStaffOrWand(__instance))
                {
                    float randomValue = UnityEngine.Random.Range(0f, 100f);
                    if (randomValue < Staff_Config.StaffLuckManaChanceValue)
                    {
                        v = 0f; // 에이트르 소모 없음

                        // 화면 중앙에 알림 표시
                        __instance.Message(MessageHud.MessageType.Center, L.Get("luck_magic_activated"));
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 스킬] UseEitr 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 마법 증폭 실제 공격 시 발동 패치 (Character.Damage 기반 - UI 오발 방지)
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class StaffSkills_MagicAmplify_Damage_Patch
    {
        [HarmonyPrefix]
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (!(hit.GetAttacker() is Player player)) return;
                if (!SkillEffect.HasSkill("staff_Step3_amp")) return;
                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player)) return;
                if (player.GetEitr() < 1f) return;

                float randomValue = UnityEngine.Random.Range(0f, 100f);
                if (randomValue < Staff_Config.StaffAmpChanceValue)
                {
                    float damageMultiplier = 1.0f + (Staff_Config.StaffAmpDamageValue / 100f);
                    if (hit.m_damage.m_fire > 0) hit.m_damage.m_fire *= damageMultiplier;
                    if (hit.m_damage.m_frost > 0) hit.m_damage.m_frost *= damageMultiplier;
                    if (hit.m_damage.m_lightning > 0) hit.m_damage.m_lightning *= damageMultiplier;
                    if (hit.m_damage.m_poison > 0) hit.m_damage.m_poison *= damageMultiplier;
                    if (hit.m_damage.m_spirit > 0) hit.m_damage.m_spirit *= damageMultiplier;

                    player.Message(MessageHud.MessageType.Center, L.Get("staff_amp_activated"));
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[마법 증폭] Character.Damage 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 지팡이 공격 속도 증가 패치 (마법 흐름 스킬)
    /// 간단한 스태미나 기반 구현
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
    public static class StaffSkills_Player_UseStamina_Patch
    {
        [HarmonyPrefix]
        public static void Prefix(Player __instance, ref float v)
        {
            try
            {
                if (__instance == null) return;
                
                var weapon = __instance.GetCurrentWeapon();
                
                // 마법 흐름: 에이트르 +30 (MMO Intellect 스탯 연동으로 구현됨)
                // 시전 속도 효과 제거 - 에이트르 증가 효과로 변경
                // SkillEffect.cs의 MMO getParameter 패치에서 Intellect 스탯으로 적용
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 스킬] UseStamina 패치 오류: {ex.Message}");
            }
        }
    }

    // 기존 마법 집중 치명타 패치 제거됨 (staff_Step4_reduction은 이제 냉기 속성)

    /// <summary>
    /// 석궁 연속 발사 FireProjectileBurst 패치
    /// Lv1: 15% 확률, Lv2: 30% 확률 (둘 다 있으면 확률 합산)
    /// </summary>
    [HarmonyPatch(typeof(Attack), "FireProjectileBurst")]
    [HarmonyPriority(Priority.High)]
    public static class CrossbowRapidFire_FireProjectileBurst_Patch
    {
        [HarmonyPrefix]
        private static bool Prefix(Attack __instance)
        {
            try
            {
                // ✅ CRITICAL: 플레이어 공격만 처리 (몬스터/NPC 차단)
                // Attack.m_character로 실제 공격자가 로컬 플레이어인지 검증
                var attacker = Traverse.Create(__instance).Field("m_character").GetValue<Character>();
                if (attacker == null || attacker != Player.m_localPlayer) return true;

                var player = Player.m_localPlayer;
                if (player == null) return true;

                var weapon = player.GetCurrentWeapon();

                if (weapon?.m_shared?.m_skillType != Skills.SkillType.Crossbows)
                    return true;

                bool hasLv1 = SkillEffect.HasSkill("crossbow_Step2_rapid_fire");
                bool hasLv2 = SkillEffect.HasSkill("crossbow_Step4_rapid_fire_lv2");

                // 스킬이 없으면 패스
                if (!hasLv1 && !hasLv2)
                    return true;

                // 확률 합산
                float totalChance = 0f;
                if (hasLv1) totalChance += Crossbow_Config.CrossbowRapidFireChanceValue;
                if (hasLv2) totalChance += Crossbow_Config.CrossbowRapidFireLv2ChanceValue;

                // 확률 체크
                float roll = UnityEngine.Random.Range(0f, 100f);
                if (roll >= totalChance)
                    return true;

                // 연속 발사 발동 (Lv2 우선, 없으면 Lv1 사용)
                int shotCount = hasLv2 ? Crossbow_Config.CrossbowRapidFireLv2ShotCountValue : Crossbow_Config.CrossbowRapidFireShotCountValue;
                float delay = hasLv2 ? Crossbow_Config.CrossbowRapidFireLv2DelayValue : Crossbow_Config.CrossbowRapidFireDelayValue;
                float damagePercent = hasLv2 ? Crossbow_Config.CrossbowRapidFireLv2DamagePercentValue : Crossbow_Config.CrossbowRapidFireDamagePercentValue;
                int boltConsumption = hasLv2 ? Crossbow_Config.CrossbowRapidFireLv2BoltConsumptionValue : Crossbow_Config.CrossbowRapidFireBoltConsumptionValue;

                var fireDir = player.GetLookDir();
                SkillTreeInputListener.Instance.StartCoroutine(
                    SkillEffect.FireCrossbowRapidFireSequence(player, weapon, fireDir, shotCount, delay, damagePercent, boltConsumption)
                );

                return false; // 원본 발사 차단, 코루틴에서 모든 발사 수동 처리
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[연속 발사] 패치 오류: {ex.Message}");
                return true;
            }
        }
    }

    /// <summary>
    /// 석궁 스킬 발동 패치 - Projectile.OnHit
    /// 균형 조준(넉백), 자동 장전 스킬 발동
    /// </summary>
    [HarmonyPatch(typeof(Projectile), nameof(Projectile.OnHit))]
    [HarmonyPriority(Priority.Normal)]
    public static class Crossbow_ProjectileHit_Patch
    {
        [HarmonyPrefix]
        private static void Prefix(Projectile __instance, Collider collider, bool water)
        {
            try
            {
                // 물에 떨어진 경우 무시
                if (water) return;

                // 발사체의 스킬 타입 확인
                if (__instance?.m_skill != Skills.SkillType.Crossbows)
                    return;

                // 발사한 플레이어 확인
                var player = Player.m_localPlayer;
                if (player == null) return;

                // 적중한 대상 확인
                Character target = null;
                if (collider != null)
                {
                    target = collider.GetComponent<Character>();
                    if (target == null)
                    {
                        target = collider.GetComponentInParent<Character>();
                    }
                }

                // 적 캐릭터 적중 시에만 효과 발동
                if (target != null && target != player && !target.IsDead())
                {
                    var hitPosition = __instance.transform.position;

                    // 균형 조준: 넉백 효과
                    if (SkillEffect.HasSkill("crossbow_Step2_balance"))
                    {
                        if (SkillEffect.TryKnockbackOnHit(target, player.transform.position))
                        {
                            SkillEffect.ShowSkillEffectText(player,
                                $"💥 {L.Get("knockback_effect")}",
                                new Color(1f, 0.3f, 0f),
                                SkillEffect.SkillEffectTextType.Combat);
                        }
                    }

                    // 자동 장전: 장전 속도 버프
                    if (SkillEffect.HasSkill("crossbow_Step4_re"))
                    {
                        SkillEffect.TryAutoReload(player);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[석궁 스킬] Projectile.OnHit 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 활 아이템 툴팁에 스킬 보너스 표시
    /// </summary>
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
        new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
    public static class BowSkills_ItemData_GetTooltip_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(ItemDrop.ItemData item, int qualityLevel, bool crafting, float worldLevel, int stackOverride, ref string __result)
        {
            try
            {
                // 활 아이템만 처리
                if (item?.m_shared?.m_skillType != Skills.SkillType.Bows) return;

                // 크래프팅 화면이 아닐 때만 표시
                if (crafting) return;

                // 플레이어 확인
                var player = Player.m_localPlayer;
                if (player == null) return;

                // 관통 스킬 보유 확인
                if (!SkillEffect.HasSkill("bow_Step3_silentshot")) return;

                // 툴팁에 추가 정보 표시
                float damageBonus = Bow_Config.BowStep3SilentShotDamageBonusValue;
                string bonusText = $"\n<color=#00ff00>관통: 공격력 +{damageBonus}</color>";

                __result += bonusText;
                Plugin.Log.LogDebug($"[활 툴팁] 관통 보너스 표시: +{damageBonus}");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[활 툴팁] GetTooltip 패치 오류: {ex.Message}");
            }
        }
    }
}
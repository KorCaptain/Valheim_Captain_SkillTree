using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 활 멀티샷 패시브 스킬 시스템
    /// bow_Step2_multishot: 15% 확률로 화살 2개 발사
    /// bow_Step4_multishot2: 15% 확률로 추가 화살 1발 발사 (+3도 각도)
    /// </summary>
    public static partial class SkillEffect
    {
        // === 멀티샷 상태 추적 ===
        internal static Dictionary<Player, bool> isMultishotProcessing = new Dictionary<Player, bool>();
        internal static Dictionary<Player, bool> bowExpertMultishotTriggered = new Dictionary<Player, bool>();
        
        /// <summary>
        /// 멀티샷 확률 체크 및 추가 화살 발사 (누적 확률 시스템)
        /// </summary>
        public static void CheckAndFireMultishot(Player player, ItemDrop.ItemData weapon, Vector3 attackDir)
        {
            try
            {
                // 재귀 호출 방지
                if (isMultishotProcessing.ContainsKey(player) && isMultishotProcessing[player])
                {
                    Plugin.Log.LogDebug("[멀티샷] 재귀 호출 방지됨");
                    return;
                }
                
                // 활 착용 확인
                if (weapon?.m_shared?.m_skillType != Skills.SkillType.Bows)
                {
                    Plugin.Log.LogDebug("[멀티샷] 활 미착용");
                    return;
                }
                
                isMultishotProcessing[player] = true;
                
                // 스킬 보유 여부 확인
                bool hasLv1 = HasSkill("bow_Step2_multishot");
                bool hasLv2 = HasSkill("bow_Step4_multishot2");

                if (!hasLv1 && !hasLv2)
                {
                    Plugin.Log.LogDebug("[멀티샷] 멀티샷 스킬 미보유");
                    return;
                }

                // 스킬 레벨 결정 (확률 체크는 FireProjectileBurst 패치에서 이미 완료됨)
                string skillLevel = (hasLv1 && hasLv2) ? "Lv1+Lv2" : (hasLv2 ? "Lv2" : "Lv1");

                // 컨피그에서 화살 수 및 소모량 가져오기
                var arrowCount = SkillTreeConfig.BowMultishotArrowCountValue;
                var consumeCount = SkillTreeConfig.BowMultishotArrowConsumptionValue;

                // 화살 미리 조회 (1회만) - player.GetAmmoItem()이 현재 무기에 맞는 탄약 반환
                var ammo = player.GetAmmoItem();
                if (ammo == null)
                {
                    Plugin.Log.LogWarning("[멀티샷] 화살이 없음 - 발사 중단");
                    return;
                }

                // 화살 소모 처리
                if (consumeCount > 0)
                {
                    ConsumeMultishotArrows(player, consumeCount);
                }

                // 멀티샷 트리거 상태 설정 (기본 화살 차단용)
                bowExpertMultishotTriggered[player] = true;

                // 기본 3발 (-3°, 0°, 3°) 발사
                var angles = new float[] { -3f, 0f, 3f };
                for (int i = 0; i < angles.Length; i++)
                {
                    FireAdditionalArrow(player, weapon, ammo, attackDir, angles[i]);
                }
                
                // 스킬 효과 텍스트 및 VFX
                ShowSkillEffectText(player, $"🏹 {L.Get("multishot_skill", skillLevel, arrowCount)}",
                    new Color(0.2f, 0.8f, 0.2f), SkillEffectTextType.Combat);
                PlaySkillEffect(player, "bow_multishot", player.transform.position);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷] 오류: {ex.Message}");
            }
            finally
            {
                isMultishotProcessing[player] = false;
                bowExpertMultishotTriggered[player] = false;
            }
        }
        
        /// <summary>
        /// 추가 화살 발사 (Valheim 네이티브 방식으로 개선)
        /// </summary>
        private static void FireAdditionalArrow(Player player, ItemDrop.ItemData weapon, ItemDrop.ItemData ammo, Vector3 baseDirection, float angleOffset)
        {
            try
            {
                // 발사 방향 계산 (각도 편차 적용)
                var fireDirection = baseDirection;
                if (angleOffset != 0f)
                {
                    var rotation = Quaternion.AngleAxis(angleOffset, Vector3.up);
                    fireDirection = rotation * baseDirection;
                }
                
                // 화살에서 프로젝타일 정보 가져오기 (아처 멀티샷과 동일한 방식)
                var ammoAttack = ammo.m_shared.m_attack;
                if (ammoAttack?.m_attackProjectile == null)
                {
                    Plugin.Log.LogError($"[활 전문가 멀티샷] 화살 프로젝타일 없음: {ammo.m_shared.m_name}");
                    return;
                }
                
                // 활에서 Attack 설정 가져오기 (속도, 정확도 등)
                var bowAttack = weapon.m_shared.m_attack;
                if (bowAttack == null)
                {
                    Plugin.Log.LogError($"[활 전문가 멀티샷] 활 Attack 정보 없음: {weapon.m_shared.m_name}");
                    return;
                }
                
                // Valheim의 표준 프로젝타일 발사 방식 사용
                var spawnPoint = player.transform.position +
                    player.transform.up * 1.5f +
                    fireDirection * 0.5f;

                // 프로젝타일 직접 생성 및 설정 (화살의 프로젝타일 사용)
                var projectileObj = UnityEngine.Object.Instantiate(
                    ammoAttack.m_attackProjectile, 
                    spawnPoint, 
                    Quaternion.LookRotation(fireDirection)
                );
                
                if (projectileObj == null)
                {
                    Plugin.Log.LogError($"[활 전문가 멀티샷] 프로젝타일 생성 실패");
                    return;
                }
                
                // Projectile 컴포넌트 설정
                var projectile = projectileObj.GetComponent<Projectile>();
                if (projectile != null)
                {
                    // HitData 구성 (컨피그 설정 데미지)
                    var hitData = new HitData();
                    var fullDamage = weapon.GetDamage();
                    fullDamage.Add(ammo.GetDamage());

                    // 활 스킬 보너스 제거 - 컨피그 설정 데미지만 적용
                    // 패시브 스킬은 컨피그 값(70%)이 최종 데미지
                    var damagePercent = SkillTreeConfig.BowMultishotDamagePercentValue / 100f;
                    fullDamage.Modify(damagePercent);
                    
                    hitData.m_damage = fullDamage;
                    hitData.m_point = spawnPoint;
                    hitData.m_dir = fireDirection;
                    hitData.m_attacker = player.GetZDOID();
                    hitData.m_skill = Skills.SkillType.Bows;
                    hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;
                    
                    // 발사 속도 계산 (활의 속도 설정 사용)
                    var velocity = fireDirection * bowAttack.m_projectileVel;
                    
                    // Projectile Setup 호출 (Valheim 표준)
                    projectile.Setup(player, velocity, bowAttack.m_projectileAccuracy, hitData, ammo, weapon);

                    // 적중 감지 및 VFX 제거 (성능 문제로 삭제)
                    // 프로젝타일 태그 추가 코드 제거됨

                    // 물리 엔진 활성화 강제
                    var rigidbody = projectileObj.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        rigidbody.velocity = velocity;
                        rigidbody.isKinematic = false;
                        rigidbody.useGravity = true;
                    }
                }
                else
                {
                    Plugin.Log.LogError($"[활 전문가 멀티샷] Projectile 컴포넌트 없음 - 삭제");
                    UnityEngine.Object.Destroy(projectileObj);
                }
                
                // 패시브 스킬이므로 효과음은 생략 (VFX/사운드 규칙 준수)
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[활 전문가 멀티샷] 추가 화살 발사 실패: {ex.Message}");
            }
        }
        
        
        /// <summary>
        /// 장착된 화살 가져오기 (아처 멀티샷과 동일한 방식)
        /// </summary>
        private static ItemDrop.ItemData GetEquippedArrow(Player player)
        {
            var inventory = player.GetInventory();
            if (inventory == null) return null;
            
            try
            {
                // 방법 1: Inventory.GetAmmoItem() 사용
                var arrows = inventory.GetAmmoItem("bow", "");
                if (arrows != null && arrows.m_stack > 0 && ValidateArrowProjectile(arrows))
                {
                    Plugin.Log.LogDebug($"[활 전문가 멀티샷] 화살 발견: {arrows.m_shared.m_name} (스택: {arrows.m_stack})");
                    return arrows;
                }
                
                // 방법 2: 모든 아이템 검색 (아처 멀티샷과 동일)
                var allItems = inventory.GetAllItems();
                foreach (var item in allItems)
                {
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo && 
                        item.m_stack > 0 && ValidateArrowProjectile(item))
                    {
                        Plugin.Log.LogDebug($"[활 전문가 멀티샷] 대체 화살 발견: {item.m_shared.m_name}");
                        return item;
                    }
                }
                
                Plugin.Log.LogWarning("[활 전문가 멀티샷] 사용 가능한 화살이 없음");
                return null;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[활 전문가 멀티샷] 화살 검색 실패: {ex.Message}");
                return null;
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
                if (inventory == null)
                {
                    Plugin.Log.LogWarning("[멀티샷] 인벤토리가 null");
                    return;
                }
                
                var ammo = GetEquippedArrow(player);
                if (ammo == null)
                {
                    Plugin.Log.LogWarning("[멀티샷] 화살이 없음");
                    return;
                }

                if (ammo.m_stack >= consumeCount)
                {
                    ammo.m_stack -= consumeCount;

                    if (ammo.m_stack <= 0)
                    {
                        inventory.RemoveItem(ammo);
                    }
                }
                else
                {
                    Plugin.Log.LogWarning($"[멀티샷] 화살 부족 - 필요: {consumeCount}, 보유: {ammo.m_stack}");
                    ammo.m_stack = 0;
                    inventory.RemoveItem(ammo);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[멀티샷] 화살 소모 처리 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 활 전문가 멀티샷이 트리거되었는지 확인 (기본 화살 차단용)
        /// </summary>
        public static bool IsBowExpertMultishotTriggered(Player player)
        {
            return bowExpertMultishotTriggered.ContainsKey(player) && bowExpertMultishotTriggered[player];
        }
        
        // ValidateArrowProjectile 메서드는 SkillEffect.ArcherMultiShot.cs에서 이미 정의됨

        // 적중 감지 및 VFX 재생 코드 제거 (성능 문제로 삭제)
        // - PlayBowExpertMultiShotHitEffect 메서드 제거
        // - BowExpertMultiShotProjectileTag 클래스 제거
        // - BowExpertMultiShot_ProjectileHit_Patch 패치 제거
        // 패시브 스킬이므로 VFX/사운드 효과 불필요 (CLAUDE.md 규칙 준수)
    }

    // ===== OnAttackTrigger 패치 제거 =====
    // FireProjectileBurst 패치만으로 충분함
    // OnAttackTrigger는 화살 발사 외에도 모든 Attack 이벤트에서 호출되어 버그 발생
    // (화살 미발사 시, 몬스터에게 맞을 때도 로그 출력되는 문제)
    
    /// <summary>
    /// 활 전문가 멀티샷용 FireProjectileBurst 패치 - 기본 화살 차단 및 멀티샷 발사
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
                // ✅ CRITICAL: 플레이어 공격만 처리 (몬스터/NPC 차단)
                // Attack.m_character로 실제 공격자가 로컬 플레이어인지 검증
                var attacker = Traverse.Create(__instance).Field("m_character").GetValue<Character>();
                if (attacker == null || attacker != Player.m_localPlayer) return true;

                var player = Player.m_localPlayer;
                if (player == null) return true;

                var currentWeapon = player.GetCurrentWeapon();

                // 활 공격인지 확인
                if (currentWeapon?.m_shared?.m_skillType != Skills.SkillType.Bows) return true;

                // ✅ 재귀 호출 방지 - 멀티샷 처리 중이면 무시
                if (SkillEffect.isMultishotProcessing.ContainsKey(player) && SkillEffect.isMultishotProcessing[player])
                {
                    return true; // 멀티샷이 추가로 발사한 화살은 다시 멀티샷 발동 안 함
                }
                
                // 1. 아커 멀티샷 버프 활성화 상태 확인 (최우선순위)
                if (SkillEffect.IsArcherMultiShotReady(player))
                {
                    Plugin.Log.LogInfo($"[아처 멀티샷] 버프 활성화 상태 - 기본 화살 차단하고 아처 멀티샷 실행");
                    
                    // 공격 방향 계산
                    Vector3 attackDir = player.GetLookDir();
                    
                    // 아처 멀티샷 실행 (원래 화살 대신)
                    SkillEffect.PerformArcherMultiShotAttack(player, currentWeapon, attackDir);
                    
                    return false; // 기본 화살 차단
                }
                
                // 2. 활 전문가 멀티샷 체크 (아처 멀티샷이 활성화되지 않은 경우만)
                var bowAttackDir = player.GetLookDir();
                
                // 스킬 보유 여부 확인
                bool hasLv1 = SkillEffect.HasSkill("bow_Step2_multishot");
                bool hasLv2 = SkillEffect.HasSkill("bow_Step4_multishot2");
                
                if (hasLv1 || hasLv2)
                {
                    // 확률 계산
                    var lv1Chance = SkillTreeConfig.BowMultishotLv1ChanceValue / 100f;
                    var lv2Chance = SkillTreeConfig.BowMultishotLv2ChanceValue / 100f;
                    
                    float totalChance = 0f;
                    if (hasLv1 && hasLv2)
                    {
                        totalChance = lv1Chance + lv2Chance;
                    }
                    else if (hasLv2)
                    {
                        totalChance = lv2Chance;
                    }
                    else if (hasLv1)
                    {
                        totalChance = lv1Chance;
                    }
                    
                    // 확률 체크
                    if (UnityEngine.Random.Range(0f, 1f) < totalChance)
                    {
                        // 멀티샷 발사 처리
                        SkillEffect.CheckAndFireMultishot(player, currentWeapon, bowAttackDir);
                        
                        return false; // 기본 화살 차단
                    }
                }
                
                return true; // 기본 화살 발사 허용
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[활 전문가 멀티샷] FireProjectileBurst 패치 오류: {ex.Message}");
                return true; // 오류 시 기본 동작 유지
            }
        }
    }
}
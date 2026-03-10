using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 아처 직업 멀티샷 시스템 - Y키 액티브 스킬 (EpicLoot 방식 적용)
    /// </summary>
    public static partial class SkillEffect
    {
        // 아처 멀티샷 쿨다운 관리
        private static float lastArcherMultiShotTime = 0f;
        
        // 아처 멀티샷 버프 상태 관리 (2회 사용 가능)
        private static Dictionary<Player, int> archerMultiShotCharges = new Dictionary<Player, int>();
        
        // 아처 멀티샷 버프 효과 인스턴스 관리 (플레이어별)
        private static Dictionary<Player, GameObject> archerMultiShotBuffEffects = new Dictionary<Player, GameObject>();
        
        // 아처 멀티샷 상태 표시 효과 인스턴스 관리 (플레이어별 - 머리 위)
        private static Dictionary<Player, GameObject> archerMultiShotStatusEffects = new Dictionary<Player, GameObject>();
        
        // 아처 멀티샷 버프 효과 프리팹 캐시 (한 번만 로드)
        private static GameObject cachedArcherBuffEffectPrefab = null;
        
        // 아처 멀티샷 상태 효과 프리팹 캐시 (한 번만 로드)
        private static GameObject cachedArcherStatusEffectPrefab = null;
        
        // 아처 현재 레벨 조회
        private static int GetArcherLevel() =>
            SkillTreeManager.Instance?.GetSkillLevel("Archer") ?? 0;

        // 화살 수는 컨피그 기본값 + 레벨 보너스
        public static int ARCHER_MULTISHOT_ARROWS
        {
            get
            {
                var baseCount = Archer_Config.ArcherMultiShotArrowCountValue;
                var lv = GetArcherLevel();
                return lv switch {
                    2 => baseCount + Archer_Config.ArcherLv2BonusArrowsValue,
                    3 => baseCount + Archer_Config.ArcherLv3BonusArrowsValue,
                    4 => baseCount + Archer_Config.ArcherLv4BonusArrowsValue,
                    5 => baseCount + Archer_Config.ArcherLv5BonusArrowsValue,
                    _ => baseCount
                };
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 버프 활성화 (Y키 - 2회 사용 가능한 버프 활성화)
        /// </summary>
        public static bool ExecuteArcherMultiShot(Player player)
        {
            try
            {
                // 전제 조건 검사
                if (!CanUseArcherMultiShot(player))
                    return false;
                
                // 현재 장착된 활 확인
                var weapon = player.GetCurrentWeapon();
                if (weapon?.m_shared?.m_skillType != Skills.SkillType.Bows)
                {
                    ShowSkillEffectText(player, "❌ " + L.Get("bow_equip_required"),
                        Color.red, SkillEffectTextType.Combat);
                    return false;
                }
                
                // 화살 확인
                var ammo = GetArcherArrow(player);
                if (ammo == null || ammo.m_stack <= 0)
                {
                    ShowSkillEffectText(player, "❌ " + L.Get("no_arrows"),
                        Color.red, SkillEffectTextType.Combat);
                    return false;
                }
                
                // 스태미나 확인
                var staminaCost = Archer_Config.ArcherMultiShotStaminaCostValue;
                if (player.GetStamina() < staminaCost)
                {
                    ShowSkillEffectText(player, "❌ " + L.Get("stamina_insufficient"),
                        Color.red, SkillEffectTextType.Combat);
                    return false;
                }
                
                // Y키로 멀티샷 버프 활성화 (컨피그 설정 충전 횟수)
                lastArcherMultiShotTime = Time.time;
                ActiveSkillCooldownRegistry.SetCooldown("Y", Archer_Config.ArcherMultiShotCooldownValue);
                player.UseStamina(staminaCost);
                
                // 멀티샷 충전 횟수 설정 (Lv5에서 +1 보너스)
                var baseCharges = Archer_Config.ArcherMultiShotChargesValue;
                var charges = GetArcherLevel() >= 5 ? baseCharges + Archer_Config.ArcherLv5BonusChargesValue : baseCharges;
                archerMultiShotCharges[player] = charges;
                
                ShowSkillEffectText(player, "🏹 " + L.Get("multishot_ready", charges.ToString()),
                    new Color(0.2f, 0.8f, 0.2f), SkillEffectTextType.Combat);

                // 버프 활성화 VFX/SFX 효과
                PlayArcherMultiShotBuffActivationEffects(player);

                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 버프 활성화 오류: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 사용 가능 여부 확인
        /// </summary>
        private static bool CanUseArcherMultiShot(Player player)
        {
            // 아처 직업 스킬 습득 확인
            if (!HasSkill("Archer"))
            {
                ShowSkillEffectText(player, "❌ " + L.Get("archer_job_required"),
                    Color.red, SkillEffectTextType.Combat);
                return false;
            }
            
            // 쿨다운 확인
            var cooldown = Archer_Config.ArcherMultiShotCooldownValue;
            var remainingCooldown = cooldown - (Time.time - lastArcherMultiShotTime);
            if (remainingCooldown > 0f)
            {
                ShowSkillEffectText(player, "⏳ " + L.Get("cooldown_format", $"{remainingCooldown:F1}"),
                    Color.yellow, SkillEffectTextType.Combat);
                return false;
            }
            
            return true;
        }
        
        // 기존 복잡한 발사 로직 제거 - EpicLoot 방식 Attack.FireProjectileBurst 패치 사용
        
        /// <summary>
        /// 아처용 화살 가져오기 (Valheim API 방식)
        /// </summary>
        public static ItemDrop.ItemData GetArcherArrow(Player player)
        {
            var inventory = player.GetInventory();
            if (inventory == null) return null;
            
            try
            {
                // Inventory.GetAmmoItem() 사용
                var arrows = inventory.GetAmmoItem("bow", "");
                if (arrows != null && arrows.m_stack > 0 && ValidateArrowProjectile(arrows))
                {
                    return arrows;
                }
                
                // 대체 방법: 모든 아이템 검색
                var allItems = inventory.GetAllItems();
                foreach (var item in allItems)
                {
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo &&
                        item.m_stack > 0 && ValidateArrowProjectile(item))
                    {
                        return item;
                    }
                }
                
                return null;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 화살 검색 실패: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 버프 상태 확인
        /// </summary>
        public static bool IsArcherMultiShotReady(Player player)
        {
            return archerMultiShotCharges.ContainsKey(player) && archerMultiShotCharges[player] > 0;
        }
        
        /// <summary>
        /// 아처 멀티샷 남은 충전 횟수 확인
        /// </summary>
        public static int GetArcherMultiShotCharges(Player player)
        {
            return archerMultiShotCharges.ContainsKey(player) ? archerMultiShotCharges[player] : 0;
        }
        
        /// <summary>
        /// 아처 멀티샷 실제 실행 (활 공격 시 자동 발동 - 원래 화살 1발 대신 5발 발사)
        /// </summary>
        public static void PerformArcherMultiShotAttack(Player player, ItemDrop.ItemData weapon, Vector3 baseDirection)
        {
            try
            {
                var ammo = GetArcherArrow(player);
                if (ammo == null)
                {
                    Plugin.Log.LogWarning("[아처 멀티샷] 화살이 없음");
                    return;
                }

                var arrowCount = ARCHER_MULTISHOT_ARROWS; // 5발 고정
                var currentCharges = GetArcherMultiShotCharges(player);

                // 부채꼴 각도 계산 (5발 고정)
                var angles = CalculateMultiShotAngles(arrowCount);

                // 각 화살 발사 (원래 화살 1발 대신 5발 발사)
                for (int i = 0; i < arrowCount; i++)
                {
                    FireArcherArrow(player, weapon, ammo, baseDirection, angles[i], i);
                }
                
                // 충전 횟수 차감
                var newCharges = currentCharges - 1;
                archerMultiShotCharges[player] = newCharges;

                if (newCharges <= 0)
                {
                    // 모든 충전 사용 완료 - 버프 해제 및 효과 제거
                    archerMultiShotCharges.Remove(player);
                    
                    // 버프 효과 제거 (buff_02a)
                    if (archerMultiShotBuffEffects.ContainsKey(player) && archerMultiShotBuffEffects[player] != null)
                    {
                        UnityEngine.Object.Destroy(archerMultiShotBuffEffects[player]);
                        archerMultiShotBuffEffects.Remove(player);
                    }

                    // 상태 효과 제거 (statusailment_01_aura)
                    if (archerMultiShotStatusEffects.ContainsKey(player) && archerMultiShotStatusEffects[player] != null)
                    {
                        UnityEngine.Object.Destroy(archerMultiShotStatusEffects[player]);
                        archerMultiShotStatusEffects.Remove(player);
                    }

                    ShowSkillEffectText(player, "🏹 " + L.Get("multishot_completed"),
                        new Color(0.8f, 0.8f, 0.2f), SkillEffectTextType.Combat);
                }
                else
                {
                    ShowSkillEffectText(player, "🏹 " + L.Get("multishot_remaining_charges", newCharges.ToString()),
                        new Color(0.2f, 0.8f, 0.2f), SkillEffectTextType.Combat);
                }
                
                // 멀티샷 발동 VFX/SFX 효과
                PlayArcherMultiShotLaunchEffect(player);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 실행 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 개별 화살 발사 (BowMultishot FireAdditionalArrow 방식)
        /// </summary>
        private static void FireArcherArrow(Player player, ItemDrop.ItemData weapon, ItemDrop.ItemData ammo,
            Vector3 baseDirection, float angleOffset, int arrowIndex)
        {
            try
            {
                // 발사 방향 계산
                var fireDirection = baseDirection;
                if (angleOffset != 0f)
                {
                    var rotation = Quaternion.AngleAxis(angleOffset, Vector3.up);
                    fireDirection = rotation * baseDirection;
                }
                
                // 화살에서 프로젝타일 정보 가져오기 (BowMultishot 방식)
                var ammoAttack = ammo.m_shared.m_attack;
                if (ammoAttack?.m_attackProjectile == null)
                {
                    Plugin.Log.LogError($"[아처 멀티샷] 화살 프로젝타일 없음: {ammo.m_shared.m_name}");
                    return;
                }
                
                // 활에서 Attack 설정 가져오기 (속도, 정확도 등)
                var bowAttack = weapon.m_shared.m_attack;
                if (bowAttack == null)
                {
                    Plugin.Log.LogError($"[아처 멀티샷] 활 Attack 정보 없음: {weapon.m_shared.m_name}");
                    return;
                }
                
                // 발사 위치 계산
                var spawnPoint = player.transform.position + 
                    player.transform.up * 1.5f + 
                    fireDirection * 0.5f;
                
                // 프로젝타일 생성 (화살의 프로젝타일 사용)
                var projectileObj = UnityEngine.Object.Instantiate(
                    ammoAttack.m_attackProjectile, 
                    spawnPoint, 
                    Quaternion.LookRotation(fireDirection)
                );
                
                if (projectileObj == null)
                {
                    Plugin.Log.LogError($"[아처 멀티샷] 프로젝타일 생성 실패");
                    return;
                }
                
                // Projectile 컴포넌트 설정
                var projectile = projectileObj.GetComponent<Projectile>();
                if (projectile != null)
                {
                    // HitData 구성 (활+화살 공격력의 컨피그 설정 비율 데미지)
                    var hitData = new HitData();
                    var fullDamage = weapon.GetDamage();
                    fullDamage.Add(ammo.GetDamage());
                    var baseDmg = Archer_Config.ArcherMultiShotDamagePercentValue;
                    var lv = GetArcherLevel();
                    float lvDmg = lv switch {
                        2 => Archer_Config.ArcherLv2DamagePercentValue,
                        3 => Archer_Config.ArcherLv3DamagePercentValue,
                        4 => Archer_Config.ArcherLv4DamagePercentValue,
                        5 => Archer_Config.ArcherLv5DamagePercentValue,
                        _ => baseDmg
                    };
                    var damagePercent = lvDmg / 100f;
                    fullDamage.Modify(damagePercent); // 아처 멀티샷 레벨 보정 데미지
                    
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

                    // 물리 엔진 활성화
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
                    Plugin.Log.LogError($"[아처 멀티샷] Projectile 컴포넌트 없음");
                    UnityEngine.Object.Destroy(projectileObj);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 화살 {arrowIndex + 1} 발사 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 화살 수에 따른 부채꼴 각도 계산
        /// </summary>
        private static float[] CalculateMultiShotAngles(int arrowCount)
        {
            if (arrowCount <= 0) return new float[0];
            if (arrowCount == 1) return new float[] { 0f };
            
            // 최대 펼침 각도 (총 20도)
            var maxSpread = 20f;
            var angles = new float[arrowCount];
            
            if (arrowCount % 2 == 1)
            {
                // 홀수개: 가운데 0도, 좌우 대칭
                angles[arrowCount / 2] = 0f; // 가운데
                var step = maxSpread / (arrowCount - 1);
                
                for (int i = 0; i < arrowCount / 2; i++)
                {
                    var angle = (i + 1) * step;
                    angles[arrowCount / 2 - (i + 1)] = -angle; // 왼쪽
                    angles[arrowCount / 2 + (i + 1)] = angle;  // 오른쪽
                }
            }
            else
            {
                // 짝수개: 가운데 없이 좌우 대칭
                var step = maxSpread / (arrowCount - 1);
                
                for (int i = 0; i < arrowCount; i++)
                {
                    angles[i] = -maxSpread / 2 + i * step;
                }
            }

            return angles;
        }
        
        /// <summary>
        /// 화살 소모 처리 (컨피그 설정 수량)
        /// </summary>
        private static void ConsumeArrows(Player player, ItemDrop.ItemData ammo)
        {
            try
            {
                var consumeCount = Archer_Config.ArcherMultiShotArrowConsumptionValue;

                if (consumeCount <= 0)
                {
                    return;
                }
                
                var inventory = player.GetInventory();
                if (inventory == null)
                {
                    Plugin.Log.LogWarning("[아처 멀티샷] 인벤토리가 null");
                    return;
                }
                
                var originalStack = ammo.m_stack;
                
                // 현재 화살 스택에서 소모
                if (ammo.m_stack >= consumeCount)
                {
                    ammo.m_stack -= consumeCount;

                    // 스택이 0이 되면 인벤토리에서 제거
                    if (ammo.m_stack <= 0)
                    {
                        inventory.RemoveItem(ammo);
                    }
                }
                else
                {
                    Plugin.Log.LogWarning($"[아처 멀티샷] 화살 부족 - 필요: {consumeCount}, 보유: {ammo.m_stack}");
                    // 부족하더라도 가진 만큼만 소모
                    ammo.m_stack = 0;
                    inventory.RemoveItem(ammo);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 화살 소모 처리 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 화살 프로젝타일 유효성 검증
        /// </summary>
        private static bool ValidateArrowProjectile(ItemDrop.ItemData arrow)
        {
            try
            {
                if (arrow?.m_shared?.m_attack?.m_attackProjectile == null)
                {
                    Plugin.Log.LogWarning($"[아처 멀티샷] 화살 프로젝타일 없음: {arrow?.m_shared?.m_name ?? "Unknown"}");
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 화살 검증 오류: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 아처 멀티샷 데미지 조정값 (50% 데미지)
        /// </summary>
        private static readonly HitData.DamageTypes ArcherMultiShotDamageModifier = new HitData.DamageTypes
        {
            m_damage = 0.5f,    // 아처 멀티샷은 50% 데미지
            m_blunt = 0.5f,
            m_slash = 0.5f,
            m_pierce = 0.5f,
            m_chop = 0.5f,
            m_pickaxe = 0.5f,
            m_fire = 0.5f,
            m_frost = 0.5f,
            m_lightning = 0.5f,
            m_poison = 0.5f,
            m_spirit = 0.5f
        };
        
        // ===== 중복 패치 제거 =====
        // FireProjectileBurst 패치는 BowMultishot.cs에서 통합 처리됨
        // 아처 멀티샷 관련 로직도 BowMultishot.cs의 통합 패치에서 우선순위 처리
        
        /// <summary>
        /// 아처 멀티샷 버프 활성화 효과 (buff_02a + statusailment_01_aura + sfx_StaffLightning_charge)
        /// </summary>
        public static void PlayArcherMultiShotBuffActivationEffects(Player player)
        {
            try
            {
                // 1. 캐릭터 발밑 buff_02a 효과 (1회 적용, 따라다니는 효과)
                PlayArcherMultiShotBuffEffect(player);
                
                // 2. 캐릭터 머리위 statusailment_01_aura 효과 (멀티샷 지속 상태 표시)
                PlayArcherMultiShotStatusEffect(player);
                
                // 3. sfx_StaffLightning_charge 사운드 효과
                PlayArcherMultiShotActivationSound(player);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 버프 활성화 효과 재생 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 버프 활성화 시 캐릭터 발밑 효과
        /// </summary>
        private static void PlayArcherMultiShotBuffEffect(Player player)
        {
            try
            {
                // VFX 등록 완료 체크 제거 - VFXManager가 안전하게 처리
                
                // 캐시된 프리팹이 없으면 한 번만 로드
                if (cachedArcherBuffEffectPrefab == null)
                {
                    // VFXManager를 통해 buff_02a 프리팹 로드
                    cachedArcherBuffEffectPrefab = VFXManager.GetVFXPrefab("buff_02a");
                    if (cachedArcherBuffEffectPrefab == null)
                    {
                        Plugin.Log.LogWarning("[아처 멀티샷] VFXManager에서 buff_02a를 찾을 수 없음");
                        return;
                    }
                }
                
                // 기존 버프 효과가 있으면 제거
                if (archerMultiShotBuffEffects.ContainsKey(player) && archerMultiShotBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(archerMultiShotBuffEffects[player]);
                    archerMultiShotBuffEffects.Remove(player);
                }

                // buff_02a 효과 실행 (캐릭터를 따라다니며 2회 사용 후 사라짐)
                if (cachedArcherBuffEffectPrefab != null)
                {
                    // 캐릭터 발밑 위치 계산 (약간 아래쪽으로)
                    var footPosition = player.transform.position + Vector3.down * 0.1f;
                    var effectInstance = UnityEngine.Object.Instantiate(cachedArcherBuffEffectPrefab, footPosition, Quaternion.identity);
                    
                    // 캐릭터를 따라다니도록 부모 설정
                    effectInstance.transform.SetParent(player.transform, false);
                    effectInstance.transform.localPosition = Vector3.down * 0.1f; // 발밑에서 살짝 아래
                    
                    // 🎯 VFX 크기 조정 - 40% 크기 유지
                    effectInstance.transform.localScale = Vector3.one * 0.4f;
                    
                    // 🎯 VFX 투명도 조정 - 15% 불투명도 (85% 투명화)
                    SetArcherBuffTransparency(effectInstance, 0.15f);
                    
                    // 버프 효과 인스턴스 저장 (2회 사용 후 제거하기 위해)
                    archerMultiShotBuffEffects[player] = effectInstance;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 버프 활성화 효과 재생 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 상태 표시 효과 (statusailment_01_aura - 캐릭터 머리 위, 멀티샷 모두 사용할 때까지 지속)
        /// </summary>
        private static void PlayArcherMultiShotStatusEffect(Player player)
        {
            try
            {
                // 캐시된 상태 효과 프리팹이 없으면 한 번만 로드
                if (cachedArcherStatusEffectPrefab == null)
                {
                    // VFXManager를 통해 statusailment_01_aura 프리팹 로드
                    cachedArcherStatusEffectPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedArcherStatusEffectPrefab == null)
                    {
                        Plugin.Log.LogWarning("[아처 멀티샷] VFXManager에서 statusailment_01_aura를 찾을 수 없음");
                        return;
                    }
                }
                
                // 기존 상태 효과가 있으면 제거
                if (archerMultiShotStatusEffects.ContainsKey(player) && archerMultiShotStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(archerMultiShotStatusEffects[player]);
                    archerMultiShotStatusEffects.Remove(player);
                }

                // statusailment_01_aura 효과 실행 (캐릭터 머리 위에서 멀티샷 완료까지 지속)
                if (cachedArcherStatusEffectPrefab != null)
                {
                    // 캐릭터 머리 위 위치 계산 (약 2미터 위)
                    var headPosition = player.transform.position + Vector3.up * 2.0f;
                    var statusInstance = UnityEngine.Object.Instantiate(cachedArcherStatusEffectPrefab, headPosition, Quaternion.identity);
                    
                    // 캐릭터를 따라다니도록 부모 설정
                    statusInstance.transform.SetParent(player.transform, false);
                    statusInstance.transform.localPosition = Vector3.up * 2.0f; // 머리 위 고정 위치
                    
                    // 🎯 상태 효과 크기 조정 - 60% 크기
                    statusInstance.transform.localScale = Vector3.one * 0.6f;
                    
                    // 상태 효과 인스턴스 저장 (멀티샷 모두 사용 후 제거하기 위해)
                    archerMultiShotStatusEffects[player] = statusInstance;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 상태 효과 재생 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 활성화 시 사운드 효과 (sfx_StaffLightning_charge)
        /// </summary>
        private static void PlayArcherMultiShotActivationSound(Player player)
        {
            try
            {
                var znet = ZNetScene.instance;
                if (znet != null)
                {
                    // sfx_StaffLightning_charge 사운드 효과
                    var soundEffect = znet.GetPrefab("sfx_StaffLightning_charge");
                    if (soundEffect != null)
                    {
                        UnityEngine.Object.Instantiate(soundEffect, player.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[아처 멀티샷] sfx_StaffLightning_charge 사운드를 찾을 수 없음");
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 활성화 사운드 재생 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 발사 시 효과 (발사 효과 없음 - buff_02a만 사용)
        /// </summary>
        private static void PlayArcherMultiShotLaunchEffect(Player player)
        {
            // 아처 멀티샷은 별도 발사 효과 없음 - buff_02a가 2회 사용 후 사라지는 것으로 충분
        }
        
        /// <summary>
        /// 아처 멀티샷 화살 적중 시 hit_01 효과 (VFXManager 사용)
        /// </summary>
        public static void PlayArcherMultiShotHitEffect(Vector3 hitPosition, Character target)
        {
            try
            {
                if (target == null)
                {
                    Plugin.Log.LogWarning("[아처 멀티샷] 타겟이 null - hit_01 효과 생성 불가");
                    return;
                }
                
                // 몬스터 머리 위 위치 계산 (몬스터 크기에 따라 조정)
                var headOffset = Vector3.up * 2.0f; // 기본 2미터 위
                if (target.name.Contains("Troll")) headOffset = Vector3.up * 4.0f; // 트롤은 4미터
                else if (target.name.Contains("Dragon")) headOffset = Vector3.up * 8.0f; // 드래곤은 8미터
                else if (target.name.Contains("Deer") || target.name.Contains("Boar")) headOffset = Vector3.up * 1.0f; // 작은 동물은 1미터
                
                var headPosition = target.transform.position + headOffset;
                
                // SimpleVFX로 hit_01 효과 재생
                SimpleVFX.PlayWithSound("hit_01", "arrow_hit", headPosition, 1.5f);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] hit_01 적중 효과 재생 오류: {ex.Message}");
            }
        }
        
        
        /// <summary>
        /// 아처 멀티샷 쿨다운 남은 시간
        /// </summary>
        public static float GetArcherMultiShotCooldownRemaining()
        {
            var cooldown = Archer_Config.ArcherMultiShotCooldownValue;
            var remaining = cooldown - (Time.time - lastArcherMultiShotTime);
            return Mathf.Max(0f, remaining);
        }
        
        /// <summary>
        /// 아처 멀티샷 버프 효과의 투명도 설정 (성기사 시스템과 동일한 방식)
        /// </summary>
        private static void SetArcherBuffTransparency(GameObject buffEffect, float alpha)
        {
            try
            {
                if (buffEffect == null)
                {
                    Plugin.Log.LogWarning("[아처 멀티샷] 버프 효과가 null - 투명도 설정 불가");
                    return;
                }

                // 전체 GameObject의 투명도 설정
                var renderers = buffEffect.GetComponentsInChildren<Renderer>(true);
                var particleSystems = buffEffect.GetComponentsInChildren<ParticleSystem>(true);
                
                // Renderer 투명도 설정
                foreach (var renderer in renderers)
                {
                    if (renderer != null && renderer.material != null)
                    {
                        var materials = renderer.materials;
                        for (int i = 0; i < materials.Length; i++)
                        {
                            var mat = materials[i];
                            if (mat != null && mat.HasProperty("_Color"))
                            {
                                Color color = mat.color;
                                color.a = alpha; // 투명도 설정
                                mat.color = color;
                                
                                // 투명 렌더링 설정
                                if (mat.HasProperty("_Mode"))
                                {
                                    mat.SetFloat("_Mode", 2); // Fade mode
                                    mat.renderQueue = 3000;
                                }
                            }
                        }
                    }
                }
                
                // ParticleSystem 투명도 설정
                foreach (var ps in particleSystems)
                {
                    if (ps != null)
                    {
                        var main = ps.main;
                        Color startColor = main.startColor.color;
                        startColor.a = alpha;
                        main.startColor = startColor;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 버프 효과 투명도 설정 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 아처 멀티샷 사용 가능 여부
        /// </summary>
        public static bool IsArcherMultiShotReady()
        {
            return GetArcherMultiShotCooldownRemaining() <= 0f && HasSkill("Archer");
        }

        /// <summary>
        /// 아처 멀티샷 정리 메서드 (플레이어 사망 시 호출)
        /// 멀티샷 충전 및 버프 효과 모두 정리
        /// </summary>
        public static void CleanupArcherMultiShotOnDeath(Player player)
        {
            try
            {
                // 멀티샷 충전 횟수 제거
                archerMultiShotCharges.Remove(player);

                // 버프 효과 GameObject 제거 (buff_02a)
                if (archerMultiShotBuffEffects.ContainsKey(player))
                {
                    var buffEffect = archerMultiShotBuffEffects[player];
                    if (buffEffect != null)
                    {
                        try
                        {
                            UnityEngine.Object.Destroy(buffEffect);
                        }
                        catch { }
                    }
                    archerMultiShotBuffEffects.Remove(player);
                }

                // 상태 효과 GameObject 제거 (statusailment_01_aura)
                if (archerMultiShotStatusEffects.ContainsKey(player))
                {
                    var statusEffect = archerMultiShotStatusEffects[player];
                    if (statusEffect != null)
                    {
                        try
                        {
                            UnityEngine.Object.Destroy(statusEffect);
                        }
                        catch { }
                    }
                    archerMultiShotStatusEffects.Remove(player);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Archer MultiShot] 정리 실패: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 아처 멀티샷 프로젝타일 태그 컴포넌트 (적중 효과 감지용)
    /// </summary>
    public class ArcherMultiShotProjectileTag : MonoBehaviour
    {
        public long shooterPlayerId;
    }

    /// <summary>
    /// 아처 멀티샷 적중 효과를 위한 Projectile 패치 (성능 최적화를 위해 비활성화)
    /// </summary>
    [HarmonyPatch(typeof(Projectile), nameof(Projectile.OnHit))]
    [HarmonyPriority(Priority.Low)]
    public static class ArcherMultiShot_ProjectileHit_Patch
    {
        [HarmonyPrefix]
        private static void Prefix(Projectile __instance, Collider collider, bool water)
        {
            // 성능 최적화: 적중 감지 비활성화 (랙 방지)
            return;
        }
    }
}
/*
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 아처 직업 스킬 - 멀티샷 기능 구현 (DEPRECATED)
    /// 이 파일은 더 이상 사용되지 않습니다.
    /// 새로운 구현은 SkillEffect.ArcherMultiShot.cs에서 확인하세요.
    /// Valheim API 방식으로 개선된 버전을 사용합니다.
    /// </summary>
    public class ArcherMultiShot_DEPRECATED
    {
        private static readonly float SPREAD_ANGLE = 10f; // 부채꼴 각도 (좌우 각각 10도)
        
        private static float lastMultiShotTime = 0f;
        
        /// <summary>
        /// 아처 직업 멀티샷 실행
        /// </summary>
        public static bool TryExecuteArcherMultiShot(Player player)
        {
            // 전제 조건 검사
            if (!CanExecuteArcherMultiShot(player))
                return false;
                
            // 현재 장착된 활과 화살 확인
            var weapon = player.GetCurrentWeapon();
            if (weapon?.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
            {
                ShowMessage(player, "활을 착용해야 합니다!");
                return false;
            }
                
            var ammo = GetEquippedArrow(player);
            if (ammo == null)
            {
                ShowMessage(player, "화살이 없습니다!");
                return false;
            }
            
            // 스태미나 확인 (고정 25)
            float requiredStamina = 25f;
            if (player.GetStamina() < requiredStamina)
            {
                ShowMessage(player, "스태미나가 부족합니다!");
                return false;
            }
            
            // 멀티샷 실행
            Plugin.Log.LogInfo($"[아처 멀티샷] 멀티샷 실행 시작 - 활: {weapon.m_shared.m_name}, 화살: {ammo.m_shared.m_name}");
            ExecuteArcherMultiShot(player, weapon, ammo);
            return true;
        }
        
        /// <summary>
        /// 아처 멀티샷 실행 가능 여부 확인
        /// </summary>
        private static bool CanExecuteArcherMultiShot(Player player)
        {
            // 아처 직업 스킬 습득 확인
            if (SkillTreeManager.Instance.GetSkillLevel("Archer") <= 0)
            {
                ShowMessage(player, "아처 직업이 필요합니다!");
                return false;
            }
                
            // 쿨다운 확인 (고정 25초)
            float cooldown = 25f;
            if (Time.time - lastMultiShotTime < cooldown)
            {
                float remainingTime = cooldown - (Time.time - lastMultiShotTime);
                ShowMessage(player, $"쿨다운: {remainingTime:F1}초");
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// 장착된 화살 가져오기
        /// </summary>
        private static ItemDrop.ItemData GetEquippedArrow(Player player)
        {
            var inventory = player.GetInventory();
            if (inventory == null) return null;
            
            // 현재 선택된 화살 찾기 (모든 화살 타입 검색)
            var arrows = inventory.GetAmmoItem("", "");
            if (arrows == null)
            {
                // 대체 방법: 인벤토리에서 화살 아이템 직접 검색
                foreach (var item in inventory.GetAllItems())
                {
                    if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo)
                    {
                        return item;
                    }
                }
            }
            return arrows;
        }
        
        /// <summary>
        /// 아처 멀티샷 실행 - 설정된 수의 화살을 부채꼴로 발사
        /// </summary>
        private static void ExecuteArcherMultiShot(Player player, ItemDrop.ItemData weapon, ItemDrop.ItemData ammo)
        {
            // 스태미나 소모 (고정 25)
            float requiredStamina = 25f;
            player.UseStamina(requiredStamina);
            
            // 쿨다운 설정
            lastMultiShotTime = Time.time;
            
            // 화살 개수 고정 (5발)
            int arrowCount = 5;
            Plugin.Log.LogInfo($"[아처 멀티샷] 화살 개수: {arrowCount}발 (고정)");
            var angles = CalculateSpreadAngles(arrowCount);
            
            // 직접 발사체 생성 방식 (확실한 방법)
            Plugin.Log.LogInfo("[아처 멀티샷] 직접 발사체 생성 방식으로 멀티샷 실행");
            for (int i = 0; i < arrowCount; i++)
            {
                Plugin.Log.LogInfo($"[아처 멀티샷] 화살 {i+1}/{arrowCount} 발사 각도: {angles[i]}°");
                FireArrow(player, weapon, ammo, angles[i]);
            }
            
            // 화살 소모 (1개만 - 아처 특전)
            ConsumeArrows(player, ammo, 1);
            
            // 효과음 및 이펙트
            PlayArcherMultiShotEffects(player);
            
            // 메시지 표시
            ShowMessage(player, $"🏹 멀티샷! ({arrowCount}발 발사)");
        }
        
        
        /// <summary>
        /// 화살 개수에 따른 부채꼴 각도 계산
        /// </summary>
        private static float[] CalculateSpreadAngles(int arrowCount)
        {
            var angles = new float[arrowCount];
            
            if (arrowCount == 1)
            {
                angles[0] = 0f;
            }
            else if (arrowCount == 2)
            {
                angles[0] = -SPREAD_ANGLE / 2;
                angles[1] = SPREAD_ANGLE / 2;
            }
            else
            {
                float step = (SPREAD_ANGLE * 2) / (arrowCount - 1);
                for (int i = 0; i < arrowCount; i++)
                {
                    angles[i] = -SPREAD_ANGLE + (step * i);
                }
            }
            
            return angles;
        }
        
        /// <summary>
        /// 지정된 각도로 화살 발사 (Player의 네이티브 메서드 사용)
        /// </summary>
        private static void FireArrow(Player player, ItemDrop.ItemData weapon, ItemDrop.ItemData ammo, float angleOffset)
        {
            try
            {
                Plugin.Log.LogInfo($"[아처 멀티샷] FireArrow 시작 - 각도: {angleOffset}°");
                
                // 발사 방향 계산
                var lookDirection = player.GetLookDir();
                var rotation = Quaternion.AngleAxis(angleOffset, Vector3.up);
                var fireDirection = rotation * lookDirection;
                
                // Attack 정보 가져오기
                var attack = weapon.m_shared.m_attack;
                if (attack?.m_attackProjectile == null)
                {
                    Plugin.Log.LogError($"[아처 멀티샷] 활 프로젝타일 없음: {weapon.m_shared.m_name}");
                    return;
                }
                
                Plugin.Log.LogInfo($"[아처 멀티샷] 프로젝타일: {attack.m_attackProjectile.name}");
                
                // 방법 1: Player의 FireProjectile 메서드 시도
                try
                {
                    // Player 클래스에서 발사 메서드 찾기 (리플렉션 사용)
                    var playerType = typeof(Player);
                    var fireMethod = playerType.GetMethod("FireProjectile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    
                    if (fireMethod != null)
                    {
                        Plugin.Log.LogInfo("[아처 멀티샷] Player.FireProjectile 메서드 발견, 사용 시도");
                        
                        // 발사 위치
                        var firePosition = player.transform.position + Vector3.up * 1.5f + lookDirection * 0.3f;
                        
                        // FireProjectile 호출
                        fireMethod.Invoke(player, new object[] 
                        { 
                            attack.m_attackProjectile,  // projectile prefab
                            firePosition,               // spawn position
                            fireDirection,              // direction
                            attack.m_projectileVel,     // velocity
                            -1f,                        // accuracy (정확도, -1은 기본값)
                            weapon,                     // weapon
                            ammo                        // ammo
                        });
                        
                        Plugin.Log.LogInfo($"[아처 멀티샷] Player.FireProjectile 호출 성공 - 각도: {angleOffset}°");
                        return;
                    }
                }
                catch (System.Exception reflectionEx)
                {
                    Plugin.Log.LogWarning($"[아처 멀티샷] FireProjectile 리플렉션 실패: {reflectionEx.Message}");
                }
                
                // 방법 2: Player의 DoAttack 메서드 시도
                try
                {
                    var playerType = typeof(Player);
                    var doAttackMethod = playerType.GetMethod("DoAttack", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    
                    if (doAttackMethod != null)
                    {
                        Plugin.Log.LogInfo("[아처 멀티샷] Player.DoAttack 메서드 발견, 사용 시도");
                        
                        // 임시로 플레이어 방향 조정
                        var originalRotation = player.transform.rotation;
                        player.transform.rotation = Quaternion.LookRotation(fireDirection);
                        
                        // DoAttack 호출
                        doAttackMethod.Invoke(player, new object[] { false, false }); // primary attack, not charged
                        
                        // 원래 방향 복원
                        player.transform.rotation = originalRotation;
                        
                        Plugin.Log.LogInfo($"[아처 멀티샷] Player.DoAttack 호출 성공 - 각도: {angleOffset}°");
                        return;
                    }
                }
                catch (System.Exception attackEx)
                {
                    Plugin.Log.LogWarning($"[아처 멀티샷] DoAttack 호출 실패: {attackEx.Message}");
                }
                
                // 방법 3: 가장 간단한 방법 - 직접 GameObject 생성
                Plugin.Log.LogInfo("[아처 멀티샷] 네이티브 메서드 실패, 직접 생성 방식 사용");
                
                var firePosition2 = player.transform.position + Vector3.up * 1.5f + lookDirection * 0.3f;
                var projectileGO = Object.Instantiate(attack.m_attackProjectile, firePosition2, Quaternion.LookRotation(fireDirection));
                
                if (projectileGO != null)
                {
                    // 즉시 속도 적용
                    var rb = projectileGO.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        var velocity = fireDirection * attack.m_projectileVel;
                        rb.velocity = velocity;
                        rb.useGravity = true;
                        rb.isKinematic = false;
                        
                        Plugin.Log.LogInfo($"[아처 멀티샷] 직접 생성 성공 - 속도: {velocity.magnitude:F1}");
                    }
                    else
                    {
                        Plugin.Log.LogError("[아처 멀티샷] Rigidbody 없음 - 화살이 움직이지 않을 수 있음");
                    }
                    
                    // Projectile 컴포넌트 설정
                    var projectile = projectileGO.GetComponent<Projectile>();
                    if (projectile != null)
                    {
                        var hitData = new HitData();
                        // (활 + 화살) 합계의 70% 데미지 계산
                        var weaponDamage = weapon.GetDamage();
                        var ammoDamage = ammo.GetDamage();
                        var totalDamage = weaponDamage;
                        totalDamage.Add(ammoDamage);
                        totalDamage.Modify(0.7f); // 70% 데미지 (활+화살 합계의 70%)
                        hitData.m_damage = totalDamage;
                        
                        hitData.m_point = firePosition2;
                        hitData.m_dir = fireDirection;
                        hitData.m_skill = Skills.SkillType.Bows;
                        hitData.m_attacker = player.GetZDOID();
                        
                        projectile.Setup(player, rb?.velocity ?? fireDirection * attack.m_projectileVel, -1f, hitData, ammo, weapon);
                        Plugin.Log.LogInfo("[아처 멀티샷] Projectile 설정 완료");
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[아처 멀티샷] Projectile 컴포넌트 없음");
                    }
                }
                else
                {
                    Plugin.Log.LogError("[아처 멀티샷] 프로젝타일 생성 실패!");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] FireArrow 전체 오류: {ex.Message}\n스택: {ex.StackTrace}");
            }
        }
        
        
        /// <summary>
        /// 화살 소모 처리
        /// </summary>
        private static void ConsumeArrows(Player player, ItemDrop.ItemData ammo, int count)
        {
            var inventory = player.GetInventory();
            if (inventory == null) return;
            
            // 지정된 수량만큼 화살 소모
            for (int i = 0; i < count && ammo.m_stack > 0; i++)
            {
                inventory.RemoveOneItem(ammo);
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 효과음 및 이펙트 재생
        /// </summary>
        private static void PlayArcherMultiShotEffects(Player player)
        {
            try
            {
                // 효과음 재생 - sfx_arbalest_fire 사운드
                try
                {
                    // sfx_arbalest_fire 효과음 재생
                    var soundPrefab = ZNetScene.instance?.GetPrefab("sfx_arbalest_fire");
                    if (soundPrefab != null)
                    {
                        var audioSource = soundPrefab.GetComponent<AudioSource>();
                        if (audioSource != null)
                        {
                            var tempGO = Object.Instantiate(soundPrefab, player.transform.position, Quaternion.identity);
                            if (tempGO != null)
                            {
                                // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                                var znetView = tempGO.GetComponent<ZNetView>();
                                if (znetView != null) Object.DestroyImmediate(znetView);
                                Object.Destroy(tempGO, 3f); // 3초 후 삭제
                                Plugin.Log.LogDebug("[아처 멀티샷] sfx_arbalest_fire 효과음 재생");
                            }
                        }
                    }
                    else
                    {
                        // 대체 효과음 (기본 활 발사음)
                        var audioSource = player.GetComponent<AudioSource>();
                        if (audioSource != null && audioSource.clip != null)
                        {
                            audioSource.PlayOneShot(audioSource.clip);
                            Plugin.Log.LogInfo("[아처 멀티샷] 대체 효과음 재생");
                        }
                    }
                }
                catch (System.Exception audioEx)
                {
                    Plugin.Log.LogError($"[아처 멀티샷] 효과음 재생 오류: {audioEx.Message}");
                }
                
                // skill_node 번들에서 Buff_02a 프리팹 사용 (다리 높이에서 시작)
                try
                {
                    var iconBundle = Plugin.GetIconAssetBundle();
                    var effectPrefab = iconBundle?.LoadAsset<GameObject>("Buff_02a");
                    if (effectPrefab != null)
                    {
                        // 다리 높이에서 이펙트 시작 (Y축 0.3f로 낮춤)
                        var effectPos = player.transform.position + Vector3.up * 0.3f;
                        var effectInstance = Object.Instantiate(effectPrefab, effectPos, Quaternion.identity);
                        
                        // 이펙트 크기 조절
                        if (effectInstance != null)
                        {
                            effectInstance.transform.localScale = Vector3.one * 1.2f;
                            Plugin.Log.LogInfo("[아처 멀티샷] Buff_02a 이펙트 생성 성공 (다리 높이)");
                        }
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[아처 멀티샷] Buff_02a 프리팹을 찾을 수 없음, 대체 이펙트 시도");
                        
                        // 대체 이펙트 시도 (다리 높이)
                        var vfxNames = new string[] { 
                            "fx_bow_shoot", "fx_arrow_hit", "fx_creature_tamed"
                        };
                        
                        foreach (var vfxName in vfxNames)
                        {
                            var vfxPrefab = ZNetScene.instance.GetPrefab(vfxName);
                            if (vfxPrefab != null)
                            {
                                var effectPos = player.transform.position + Vector3.up * 0.3f;
                                var effectInstance = Object.Instantiate(vfxPrefab, effectPos, Quaternion.identity);

                                if (effectInstance != null)
                                {
                                    // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                                    var znetView = effectInstance.GetComponent<ZNetView>();
                                    if (znetView != null) Object.DestroyImmediate(znetView);
                                    effectInstance.transform.localScale = Vector3.one * 0.8f;
                                    Object.Destroy(effectInstance, 3f);
                                }

                                Plugin.Log.LogDebug($"[아처 멀티샷] 대체 VFX 이펙트 생성: {vfxName} (다리 높이)");
                                break;
                            }
                        }
                    }
                }
                catch (System.Exception effectEx)
                {
                    Plugin.Log.LogError($"[아처 멀티샷] 이펙트 생성 오류: {effectEx.Message}");
                }
                
                // 화면 흔들림 제거 (사용자 요청)
                Plugin.Log.LogInfo("[아처 멀티샷] 화면 흔들림 효과 제거됨 (사용자 요청)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[아처 멀티샷] 이펙트 재생 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 아처 멀티샷 쿨다운 확인
        /// </summary>
        public static float GetArcherMultiShotCooldownRemaining()
        {
            var remaining = 25f - (Time.time - lastMultiShotTime); // 고정 25초 쿨타임
            return Mathf.Max(0f, remaining);
        }
        
        /// <summary>
        /// 아처 멀티샷 사용 가능 여부
        /// </summary>
        public static bool IsArcherMultiShotReady()
        {
            return GetArcherMultiShotCooldownRemaining() <= 0f;
        }
        
        /// <summary>
        /// 메시지 표시 헬퍼 함수
        /// </summary>
        private static void ShowMessage(Player player, string message)
        {
            try
            {
                if (MessageHud.instance != null)
                {
                    MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, message);
                }
            }
            catch
            {
                Plugin.Log.LogInfo($"[아처 멀티샷] {message}");
            }
        }
    }
}
*/
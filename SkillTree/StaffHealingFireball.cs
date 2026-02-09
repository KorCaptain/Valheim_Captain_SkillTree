using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// ⚠️ 제거된 시스템 - 참조용으로만 보관 ⚠️
    ///
    /// 지팡이 힐 스킬은 현재 "캐릭터 중심 즉시 광역 힐" 방식으로 변경되었습니다.
    /// (SkillEffect.ActiveSkills.cs의 ActivateStaffAreaHeal 메서드 참조)
    ///
    /// 이 파일은 과거 사용했던 "힐 파이어볼" 시스템의 코드입니다:
    /// - H키로 힐 모드 토글
    /// - 마우스 클릭으로 힐 파이어볼 발사
    /// - PVP 무시 힐링
    /// - healing VFX 2초 유지
    ///
    /// 현재는 사용하지 않으며, 향후 참고용으로만 보관합니다.
    /// </summary>
    public static class StaffHealingFireball
    {
        // === 힐 모드 토글 상태 관리 ===
        private static Dictionary<Player, bool> isHealModeActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, GameObject> healModeUIIndicator = new Dictionary<Player, GameObject>();
        
        // 쿨타임 관리
        private static Dictionary<Player, float> lastHealFireballTime = new Dictionary<Player, float>();
        
        // 활성 힐링 효과 추적 (자동 제거용)
        private static Dictionary<Player, GameObject> activeHealingEffects = new Dictionary<Player, GameObject>();
        
        // 무기 변경 감지용 (이전 무기 추적)
        private static Dictionary<Player, ItemDrop.ItemData> previousWeapons = new Dictionary<Player, ItemDrop.ItemData>();
        
        // 중복 힐링 방지 시스템 (시전 시간 기준으로 그룹화)
        internal static Dictionary<long, HashSet<long>> recentlyHealedPlayers = new Dictionary<long, HashSet<long>>();
        internal static Dictionary<long, float> healCastTimes = new Dictionary<long, float>();
        private const float DUPLICATE_HEAL_WINDOW = 1f; // 1초 내 중복 힐링 방지
        
        // 설정값들 (추후 컨피그로 이동)
        private const float HEAL_FIREBALL_COOLDOWN = 3f;        // 3초 쿨타임 (마우스 클릭용)
        private const float HEAL_FIREBALL_EITR_COST = 15f;      // Eitr 15 소모
        private const float HEAL_PERCENTAGE = 0.25f;            // 최대체력 25% 힐링
        private const float HEAL_RANGE = 10f;                   // 10m 범위 (5m에서 10m로 증가)
        private const float HEALING_VFX_DURATION = 2f;          // healing VFX 2초 지속
        
        /// <summary>
        /// H키로 힐 모드 토글 (활성화/비활성화)
        /// </summary>
        public static bool ToggleHealMode(Player player)
        {
            try
            {
                // 1. 지팡이 착용 확인 (최적화된 감지 시스템 사용)
                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    StaffEquipmentDetector.ShowStaffRequiredMessage(player);
                    return false;
                }
                
                // 2. 현재 상태 확인 및 토글
                bool currentlyActive = isHealModeActive.ContainsKey(player) && isHealModeActive[player];
                
                if (currentlyActive)
                {
                    // 힐 모드 비활성화
                    DeactivateHealMode(player);
                    player.Message(MessageHud.MessageType.Center, "🩹 힐 모드 비활성화");
                    Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 힐 모드 비활성화");
                    return true;
                }
                else
                {
                    // 힐 모드 활성화
                    ActivateHealMode(player);
                    player.Message(MessageHud.MessageType.Center, "💚 힐 모드 활성화 - 마우스 클릭으로 힐 파이어볼 발사!");
                    Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 힐 모드 활성화");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 모드] 토글 오류: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 힐 모드 활성화
        /// </summary>
        private static void ActivateHealMode(Player player)
        {
            if (player == null) return;
            
            // 기존 이팩트가 있다면 먼저 제거
            if (healModeUIIndicator.ContainsKey(player))
            {
                var existingIndicator = healModeUIIndicator[player];
                if (existingIndicator != null)
                {
                    Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 기존 힐링 이팩트 제거");
                    UnityEngine.Object.Destroy(existingIndicator);
                }
                healModeUIIndicator.Remove(player);
            }

            // 상태 설정
            isHealModeActive[player] = true;

            // Valheim 내장 VFX를 캐릭터 발밑에 부착해서 따라다니게 함
            // (buff_03a 대신 vfx_Potion_health_medium 사용 - ZNetView 충돌 방지)
            try
            {
                var healVfxPrefab = VFXManager.GetVFXPrefab("vfx_Potion_health_medium");
                if (healVfxPrefab != null)
                {
                    // 캐릭터 발밑 위치로 변경 (Y 좌표를 0.1로 낮춤)
                    Vector3 buffPosition = player.transform.position + Vector3.up * 0.1f;
                    GameObject buffEffect = UnityEngine.Object.Instantiate(healVfxPrefab, buffPosition, player.transform.rotation);

                    // 캐릭터를 따라다니도록 부모 설정 (발밑에 위치)
                    buffEffect.transform.SetParent(player.transform, false);
                    buffEffect.transform.localPosition = Vector3.up * 0.1f; // 발밑 위치로 로컬 설정

                    // 힐 모드 이팩트임을 명시적으로 표시 (이름 설정)
                    buffEffect.name = "HealMode_VFX_Effect";

                    healModeUIIndicator[player] = buffEffect;
                    Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 힐링 이펙트 발밑에 부착 완료 - {buffEffect.name}");
                }
                else
                {
                    Plugin.Log.LogWarning("[힐 모드] vfx_Potion_health_medium 프리팹 찾을 수 없음 - 기본 효과로 대체");
                    // 실패시 기본 효과도 발밑에 위치
                    Vector3 indicatorPos = player.transform.position + Vector3.up * 0.2f;
                    SimpleVFX.Play("vfx_HealthUpgrade", indicatorPos, 999f);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[힐 모드] 힐링 이펙트 부착 실패: {ex.Message} - 기본 효과로 대체");
                // 실패시 기본 효과도 발밑에 위치
                Vector3 indicatorPos = player.transform.position + Vector3.up * 0.2f;
                SimpleVFX.Play("vfx_HealthUpgrade", indicatorPos, 999f);
            }

            Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 힐 모드 활성화 완료 (발밑 이펙트)");
        }
        
        /// <summary>
        /// 힐 모드 비활성화
        /// </summary>
        private static void DeactivateHealMode(Player player)
        {
            if (player == null) return;
            
            // 상태 해제
            isHealModeActive[player] = false;

            // 힐링 이팩트 제거 (확실하게 제거)
            if (healModeUIIndicator.ContainsKey(player))
            {
                var indicator = healModeUIIndicator[player];
                if (indicator != null)
                {
                    Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 힐링 이팩트 제거");
                    UnityEngine.Object.Destroy(indicator);
                }
                healModeUIIndicator.Remove(player);
            }

            // 혹시 남아있을 수 있는 다른 이팩트들도 정리
            try
            {
                // 플레이어 하위의 모든 힐 모드 이팩트 찾아서 제거
                var childEffects = player.transform.GetComponentsInChildren<Transform>();
                foreach (var child in childEffects)
                {
                    if (child != null && (child.name.Contains("HealMode_") || child.name.Contains("buff_03a")))
                    {
                        Plugin.Log.LogInfo($"[힐 모드] 추가 힐링 이팩트 발견 및 제거: {child.name}");
                        UnityEngine.Object.Destroy(child.gameObject);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[힐 모드] 추가 이팩트 정리 중 오류: {ex.Message}");
            }

            Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 힐 모드 비활성화 완료 - 힐링 이팩트 완전 제거");
        }
        
        /// <summary>
        /// 힐 모드 활성화 상태 확인
        /// </summary>
        public static bool IsHealModeActive(Player player)
        {
            return player != null && isHealModeActive.ContainsKey(player) && isHealModeActive[player];
        }
        
        /// <summary>
        /// 마우스 클릭으로 힐 파이어볼 발사 (힐 모드 활성화 시에만)
        /// </summary>
        public static bool LaunchHealFireballOnClick(Player player)
        {
            try
            {
                // 1. 힐 모드 활성화 상태 확인
                if (!IsHealModeActive(player))
                {
                    return false; // 힐 모드가 아니면 조용히 반환
                }
                
                // 2. 지팡이 착용 확인 (최적화된 감지 시스템 사용)
                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    StaffEquipmentDetector.ShowStaffRequiredMessage(player);
                    return false;
                }
                
                // 3. 쿨타임 확인
                if (IsOnCooldown(player))
                {
                    float remaining = GetCooldownRemaining(player);
                    player.Message(MessageHud.MessageType.Center, $"힐 파이어볼 쿨타임: {remaining:F1}초 남음");
                    return false;
                }
                
                // 4. Eitr 확인
                if (player.GetEitr() < HEAL_FIREBALL_EITR_COST)
                {
                    player.Message(MessageHud.MessageType.Center, $"Eitr 부족! (필요: {HEAL_FIREBALL_EITR_COST})");
                    return false;
                }
                
                // 5. 힐 파이어볼 발사
                long casterId = player.GetPlayerID();
                float castTime = Time.time;
                
                // 시전 시간 기록 및 힐링 받은 플레이어 목록 초기화
                healCastTimes[casterId] = castTime;
                recentlyHealedPlayers[casterId] = new HashSet<long>();
                
                bool success = LaunchHealingFireball(player);
                if (success)
                {
                    // 6. 쿨타임 설정 및 Eitr 소모
                    lastHealFireballTime[player] = castTime;
                    player.UseEitr(HEAL_FIREBALL_EITR_COST);
                    
                    player.Message(MessageHud.MessageType.Center, "💚 힐 파이어볼 발사!");
                    Plugin.Log.LogInfo($"[힐 파이어볼] {player.GetPlayerName()} 힐 파이어볼 발사 완료");
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] 마우스 클릭 발사 오류: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 지팡이/완드 착용 확인 (다양한 지팡이 타입 지원 - 보호의 지팡이 등 포함)
        /// </summary>
        private static bool IsWieldingHealingStaff(Player player)
        {
            try
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon?.m_shared == null) return false;
                
                // 1순위: Valheim 기본 ElementalMagic 스킬 타입
                if (weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic)
                {
                    return true;
                }
                
                // 2순위: 무기 이름 패턴 매칭 (더 포괄적)
                string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
                string weaponInternalName = weapon.m_shared.m_name?.ToLower() ?? "";
                
                // 일반적인 지팡이/완드 패턴
                var staffPatterns = new[]
                {
                    "staff", "wand", "rod", "scepter",           // 기본 영어 패턴
                    "지팡이", "완드", "막대",                      // 한글 패턴
                    "$item_staff", "$item_wand",                // Valheim 아이템 ID 패턴
                    "stafffire", "staffice", "stafflight",      // Valheim 기본 지팡이들
                    "보호의", "치료의", "마법의", "원소의",           // 한글 수식어
                    "protection", "healing", "magic", "elemental" // 영어 수식어
                };
                
                foreach (var pattern in staffPatterns)
                {
                    if (weaponName.Contains(pattern) || weaponInternalName.Contains(pattern))
                    {
                        Plugin.Log.LogInfo($"[힐 파이어볼] 패턴으로 지팡이 감지: {weapon.m_shared.m_name} (패턴: {pattern})");
                        return true;
                    }
                }
                
                // 3순위: 무기 애니메이션 타입 확인 (일부 지팡이들은 특별한 애니메이션을 사용)
                var animationState = weapon.m_shared.m_animationState;
                if (animationState == ItemDrop.ItemData.AnimationState.OneHanded && 
                    weapon.m_shared.m_attack?.m_attackProjectile != null)
                {
                    Plugin.Log.LogInfo($"[힐 파이어볼] 프로젝타일 기반 지팡이 감지: {weapon.m_shared.m_name}");
                    return true;
                }
                
                // 4순위: 프로젝타일이 있는 한손 무기 (대부분의 지팡이 특성)
                if (weapon.m_shared.m_attack?.m_attackProjectile != null &&
                    weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon)
                {
                    // 추가로 데미지 타입 확인 (마법 계열 데미지가 있는 경우)
                    var damageTypes = weapon.GetDamage();
                    if (damageTypes.m_fire > 0 || damageTypes.m_frost > 0 || 
                        damageTypes.m_lightning > 0 || damageTypes.m_spirit > 0)
                    {
                        Plugin.Log.LogInfo($"[힐 파이어볼] 마법 데미지 기반 지팡이 감지: {weapon.m_shared.m_name}");
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] 지팡이 확인 실패: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 힐 파이어볼 발사 (고정적으로 파이어볼 발사체 사용 + fx_Fader_Roar 시전 효과)
        /// </summary>
        private static bool LaunchHealingFireball(Player player)
        {
            try
            {
                // 지팡이를 장착했는지 확인 (최적화된 감지 시스템 사용)
                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    Plugin.Log.LogError("[힐 파이어볼] 지팡이가 아닌 무기 장착");
                    return false;
                }
                
                // 고정적으로 파이어볼 프로젝타일 사용 (PrefabManager에서 검색)
                GameObject fireballPrefab = GetFireballProjectile();
                if (fireballPrefab == null)
                {
                    Plugin.Log.LogError("[힐 파이어볼] 파이어볼 프로젝타일을 찾을 수 없음");
                    return false;
                }
                
                // 시전시 fx_Fader_Roar 효과 재생 (SimpleVFX 방식)
                Vector3 playerPos = player.transform.position;
                Plugin.Log.LogInfo($"[힐 파이어볼] {player.GetPlayerName()} 시전 효과 재생 시도: fx_Fader_Roar at {playerPos}");
                SimpleVFX.PlayWithSound("fx_Fader_Roar", "magic_cast", playerPos, 3f);
                Plugin.Log.LogInfo($"[힐 파이어볼] 시전 효과 재생 완료");
                
                // 발사 방향 계산 (2발 발사용)
                Vector3 baseDirection = player.GetLookDir();
                Vector3 spawnPoint = player.transform.position + 
                    player.transform.up * 1.5f + 
                    baseDirection * 0.5f;
                
                // 2발 발사 (왼쪽 -2도, 오른쪽 +2도)
                Vector3[] fireDirections = new Vector3[2];
                fireDirections[0] = Quaternion.Euler(0, -2f, 0) * baseDirection; // 왼쪽 -2도
                fireDirections[1] = Quaternion.Euler(0, +2f, 0) * baseDirection; // 오른쪽 +2도
                
                bool anySuccess = false;
                
                for (int i = 0; i < 2; i++)
                {
                    // 고정 파이어볼 프로젝타일 생성
                    var projectileObj = UnityEngine.Object.Instantiate(
                        fireballPrefab,
                        spawnPoint,
                        Quaternion.LookRotation(fireDirections[i])
                    );
                    
                    if (projectileObj == null)
                    {
                        Plugin.Log.LogError($"[힐 파이어볼] 프로젝타일 {i+1}번 생성 실패");
                        continue;
                    }
                    
                    // Projectile 컴포넌트 설정
                    var projectile = projectileObj.GetComponent<Projectile>();
                    if (projectile != null)
                    {
                        // HitData 구성 (데미지 0으로 설정)
                        var hitData = new HitData();
                        hitData.m_damage = new HitData.DamageTypes(); // 모든 데미지 0
                        hitData.m_point = spawnPoint;
                        hitData.m_dir = fireDirections[i];
                        hitData.m_attacker = player.GetZDOID();
                        hitData.m_skill = Skills.SkillType.ElementalMagic;
                        hitData.m_ignorePVP = true; // PVP 무시 설정
                        
                        // 파이어볼 기본 속도 사용 (고정값)
                        var velocity = fireDirections[i] * 25f; // 파이어볼 기본 속도
                        
                        // Projectile Setup 호출 (null weapon 전달)
                        projectile.Setup(player, velocity, -1f, hitData, null, null);
                        
                        // 힐 파이어볼 태그 추가
                        var healTag = projectileObj.AddComponent<StaffHealingFireballTag>();
                        healTag.casterPlayerId = player.GetPlayerID();
                        healTag.healPercentage = HEAL_PERCENTAGE;
                        healTag.healRange = HEAL_RANGE;
                        healTag.ignorePVP = true;
                        healTag.projectileIndex = i; // 발사체 인덱스 추가 (중복 방지용)
                        
                        anySuccess = true;
                        Plugin.Log.LogInfo($"[힐 파이어볼] 파이어볼 프로젝타일 {i+1}번 생성 완료");
                    }
                    else
                    {
                        Plugin.Log.LogError($"[힐 파이어볼] 프로젝타일 {i+1}번 Projectile 컴포넌트 없음");
                        UnityEngine.Object.Destroy(projectileObj);
                    }
                }
                
                return anySuccess;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] 발사 실패: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// PVP 무시 힐링 처리 (강화된 버전)
        /// </summary>
        public static void ProcessHealingIgnorePVP(Player target, StaffHealingFireballTag healTag)
        {
            try
            {
                if (target == null || healTag == null) return;
                
                float healAmount = target.GetMaxHealth() * healTag.healPercentage;
                
                // 다중 힐링 방식으로 PVP 설정 우회
                // 방법 1: 직접 힐링 (PVP 무시)
                target.Heal(healAmount, true);
                
                // 방법 2: 추가 힐링 (PVP 설정 우회)
                target.Heal(healAmount * 0.1f, true); // 추가 힐링 (보험용)
                
                // 방법 3: 체력 직접 설정 (최후 수단)
                float currentHP = target.GetHealth();
                float maxHP = target.GetMaxHealth();
                float targetHP = Mathf.Min(maxHP, currentHP + healAmount);
                if (targetHP > currentHP)
                {
                    var seMan = target.GetSEMan();
                    if (seMan != null)
                    {
                        // StatusEffect를 통한 힐링 (PVP 우회)
                        float regenAmount = healAmount / HEALING_VFX_DURATION;
                        target.GetSEMan()?.ModifyHealthRegen(ref regenAmount);
                    }
                }
                
                // 힐링 플로팅 텍스트
                SkillEffect.DrawFloatingText(target, $"💚 +{healAmount:F0} HP", Color.green);
                
                Plugin.Log.LogInfo($"[힐 파이어볼] PVP 무시 힐링: {target.GetPlayerName()} +{healAmount:F1} HP (강화된 방식)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] PVP 무시 힐링 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// healing VFX 2초 유지 후 자동 제거 (하이브리드 방식으로 모든 유저가 볼 수 있도록 개선)
        /// </summary>
        public static void PlayHealingVFXWithAutoRemoval(Player target)
        {
            try
            {
                if (target == null)
                {
                    Plugin.Log.LogWarning("[힐 VFX] target이 null - VFX 재생 불가");
                    return;
                }

                // 기존 힐링 효과가 있으면 제거
                if (activeHealingEffects.ContainsKey(target) && activeHealingEffects[target] != null)
                {
                    UnityEngine.Object.Destroy(activeHealingEffects[target]);
                    activeHealingEffects.Remove(target);
                }

                // 플레이어 위치 및 회전 계산
                Vector3 targetPos = target.transform.position;
                Vector3 elevatedPos = targetPos + Vector3.up * 1.5f;
                Quaternion targetRot = target.transform.rotation;

                Plugin.Log.LogInfo($"[힐 VFX] {target.GetPlayerName()} 위치: {targetPos}, 회전: {targetRot}");

                // === SimpleVFX 방식으로 VFX 재생 ===

                // 주요 힐링 VFX (발헤임 기본 프리팹)
                Plugin.Log.LogInfo($"[힐 VFX] vfx_HealthUpgrade 재생 시도: {targetPos}");
                SimpleVFX.PlayWithSound("vfx_HealthUpgrade", "sfx_health_up", targetPos, HEALING_VFX_DURATION);

                // 힐링 시각 효과 (매핑된 이름 사용)
                Vector3 healPos = elevatedPos + Vector3.up * 0.5f;
                Plugin.Log.LogInfo($"[힐 VFX] healing 재생 시도: {healPos}");
                SimpleVFX.PlayWithSound("healing", "heal", healPos, HEALING_VFX_DURATION);

                // 지속적인 힐링 표시를 위한 추가 효과
                Vector3 backPos = targetPos - target.transform.forward * 0.3f + Vector3.up * 0.6f;
                Plugin.Log.LogInfo($"[힐 VFX] healing 지속 효과 재생 시도: {backPos}");
                SimpleVFX.PlayWithSound("healing", "heal_continuous", backPos, HEALING_VFX_DURATION * 1.2f);

                Plugin.Log.LogInfo($"[힐 VFX] 모든 VFX 재생 완료 - {target.GetPlayerName()}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 VFX] VFX 재생 실패: {ex.Message}\n{ex.StackTrace}");
            }
        }
        
        /// <summary>
        /// 지연 후 GameObject 제거 코루틴 (필요한 경우에만 사용)
        /// </summary>
        private static IEnumerator DestroyAfterDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);

            // ✅ 플레이어 사망 체크 추가 (대기 후)
            if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
            {
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
                Plugin.Log.LogInfo("[힐링 파이어볼] 플레이어 사망으로 객체 조기 제거");
                yield break;
            }

            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }
        
        /// <summary>
        /// 힐링 가능 여부 확인 (체력이 만땅이 아닌 경우)
        /// </summary>
        public static bool CanBeHealed(Player target)
        {
            try
            {
                if (target == null || target.IsDead()) return false;
                
                float currentHealth = target.GetHealth();
                float maxHealth = target.GetMaxHealth();
                
                // 체력이 98% 미만인 경우에만 힐링 가능 (약간의 여유를 둠)
                float healthPercentage = currentHealth / maxHealth;
                bool canHeal = healthPercentage < 0.98f;
                
                Plugin.Log.LogDebug($"[힐 파이어볼] {target.GetPlayerName()} 힐링 가능 여부: {canHeal} (체력: {currentHealth:F1}/{maxHealth:F1}, {healthPercentage * 100:F1}%)");
                return canHeal;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] 힐링 가능 여부 확인 오류: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 5m 범위 내 플레이어 검색
        /// </summary>
        public static List<Player> GetPlayersInRange(Vector3 center, float range)
        {
            var playersInRange = new List<Player>();
            try
            {
                foreach (var player in Player.GetAllPlayers())
                {
                    if (player != null && Vector3.Distance(center, player.transform.position) <= range)
                    {
                        playersInRange.Add(player);
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] 범위 내 플레이어 검색 오류: {ex.Message}");
            }
            return playersInRange;
        }
        
        /// <summary>
        /// 쿨타임 확인
        /// </summary>
        private static bool IsOnCooldown(Player player)
        {
            if (!lastHealFireballTime.ContainsKey(player)) return false;
            return (Time.time - lastHealFireballTime[player]) < HEAL_FIREBALL_COOLDOWN;
        }
        
        /// <summary>
        /// 남은 쿨타임 반환
        /// </summary>
        private static float GetCooldownRemaining(Player player)
        {
            if (!lastHealFireballTime.ContainsKey(player)) return 0f;
            float remaining = HEAL_FIREBALL_COOLDOWN - (Time.time - lastHealFireballTime[player]);
            return Mathf.Max(0f, remaining);
        }
        
        /// <summary>
        /// 무기 변경 감지 및 힐 모드 자동 종료
        /// </summary>
        public static void CheckWeaponChangeAndDeactivateHeal(Player player)
        {
            try
            {
                if (player == null || !IsHealModeActive(player)) return;
                
                var currentWeapon = player.GetCurrentWeapon();
                
                // 이전 무기 정보 얻기
                ItemDrop.ItemData previousWeapon = null;
                if (previousWeapons.ContainsKey(player))
                {
                    previousWeapon = previousWeapons[player];
                }
                
                // 무기가 변경된 경우
                bool weaponChanged = false;
                
                if (previousWeapon == null && currentWeapon != null)
                {
                    weaponChanged = true;
                }
                else if (previousWeapon != null && currentWeapon == null)
                {
                    weaponChanged = true;
                }
                else if (previousWeapon != null && currentWeapon != null)
                {
                    // 무기 이름이 다른 경우
                    string prevName = previousWeapon.m_shared?.m_name ?? "";
                    string currName = currentWeapon.m_shared?.m_name ?? "";
                    
                    if (prevName != currName)
                    {
                        weaponChanged = true;
                    }
                }
                
                // 무기가 변경되고, 새 무기가 지팡이가 아닌 경우 힐 모드 종료
                if (weaponChanged)
                {
                    if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                    {
                        DeactivateHealMode(player);
                        player.Message(MessageHud.MessageType.Center, "🚫 지팡이가 아닌 무기로 변경 - 힐 모드 자동 비활성화");
                        Plugin.Log.LogInfo($"[힐 모드] {player.GetPlayerName()} 무기 변경으로 힐 모드 자동 종료");
                    }
                }
                
                // 현재 무기 저장
                previousWeapons[player] = currentWeapon;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 모드] 무기 변경 감지 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 모든 상태 정리 (플러그인 종료시)
        /// </summary>
        public static void ClearAllStates()
        {
            lastHealFireballTime.Clear();
            
            // 힐 모드 상태 정리
            isHealModeActive.Clear();

            // 힐링 UI 표시 효과 완전 정리
            foreach (var kvp in healModeUIIndicator)
            {
                if (kvp.Value != null)
                {
                    Plugin.Log.LogInfo($"[힐 모드 정리] 힐링 이팩트 제거: {kvp.Key.GetPlayerName()}");
                    UnityEngine.Object.Destroy(kvp.Value);
                }
            }
            healModeUIIndicator.Clear();

            // 모든 플레이어의 남은 힐 모드 이팩트 정리
            try
            {
                foreach (var player in Player.GetAllPlayers())
                {
                    if (player != null)
                    {
                        var childEffects = player.transform.GetComponentsInChildren<Transform>();
                        foreach (var child in childEffects)
                        {
                            if (child != null && (child.name.Contains("HealMode_") || child.name.Contains("buff_03a")))
                            {
                                Plugin.Log.LogInfo($"[힐 모드 정리] 남은 힐링 이팩트 발견 및 제거: {child.name}");
                                UnityEngine.Object.Destroy(child.gameObject);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[힐 모드 정리] 남은 이팩트 정리 중 오류: {ex.Message}");
            }
            
            // 활성 힐링 효과 정리
            foreach (var effect in activeHealingEffects.Values)
            {
                if (effect != null)
                {
                    UnityEngine.Object.Destroy(effect);
                }
            }
            activeHealingEffects.Clear();
            
            // 무기 변경 감지 데이터 정리
            previousWeapons.Clear();
            
            // 중복 힐링 방지 데이터 정리
            recentlyHealedPlayers.Clear();
            healCastTimes.Clear();
            
            Plugin.Log.LogInfo("[힐 파이어볼] 모든 상태 정리 완료 - 힐링 이팩트 완전 제거");
        }
        
        /// <summary>
        /// 고정 파이어볼 프로젝타일 프리팹 가져오기
        /// </summary>
        private static GameObject GetFireballProjectile()
        {
            try
            {
                // Valheim 기본 파이어볼 프로젝타일 이름들 시도
                string[] fireballNames = {
                    "Projectile_Fireball",      // 기본 파이어볼
                    "projectile_fireball",      // 소문자 버전
                    "fireball",                 // 단순 이름
                    "StaffFireProjectile",      // 지팡이 파이어볼
                    "staff_fire_projectile",    // 지팡이 파이어볼 (언더스코어)
                    "FireProjectile",           // Fire 프로젝타일
                    "fire_projectile"           // fire 프로젝타일 (언더스코어)
                };
                
                // PrefabManager에서 프리팹 검색
                foreach (string name in fireballNames)
                {
                    var prefab = ZNetScene.instance?.GetPrefab(name);
                    if (prefab != null && prefab.GetComponent<Projectile>() != null)
                    {
                        Plugin.Log.LogInfo($"[힐 파이어볼] 파이어볼 프로젝타일 발견: {name}");
                        return prefab;
                    }
                }
                
                // ObjectDB에서 찾기 시도
                foreach (string name in fireballNames)
                {
                    var item = ObjectDB.instance?.GetItemPrefab(name);
                    if (item != null && item.GetComponent<Projectile>() != null)
                    {
                        Plugin.Log.LogInfo($"[힐 파이어볼] ObjectDB에서 파이어볼 발견: {name}");
                        return item;
                    }
                }
                
                // 모든 프리팹에서 "fire"가 포함된 Projectile 검색
                if (ZNetScene.instance != null)
                {
                    var allPrefabs = ZNetScene.instance.m_prefabs;
                    foreach (var prefab in allPrefabs)
                    {
                        if (prefab != null && 
                            prefab.name.ToLower().Contains("fire") && 
                            prefab.GetComponent<Projectile>() != null)
                        {
                            Plugin.Log.LogInfo($"[힐 파이어볼] fire 패턴 매칭으로 발견: {prefab.name}");
                            return prefab;
                        }
                    }
                }
                
                Plugin.Log.LogWarning("[힐 파이어볼] 파이어볼 프로젝타일을 찾을 수 없음 - 기본 투사체 생성 시도");
                
                // 마지막 수단: 기본 투사체가 있는지 확인
                var basicProjectile = ZNetScene.instance?.GetPrefab("Projectile_Arrow"); // 화살이라도 사용
                if (basicProjectile != null && basicProjectile.GetComponent<Projectile>() != null)
                {
                    Plugin.Log.LogInfo("[힐 파이어볼] 대체 투사체 사용: Projectile_Arrow");
                    return basicProjectile;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] 파이어볼 프리팹 검색 실패: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 지팡이 힐 모드 사망 시 정리 시스템
        /// </summary>
        public static void CleanupStaffHealOnDeath(Player player)
        {
            try
            {
                // 지팡이 힐 모드 Dictionary 정리 (7개)
                isHealModeActive.Remove(player);

                if (healModeUIIndicator.ContainsKey(player))
                {
                    var uiObj = healModeUIIndicator[player];
                    if (uiObj != null)
                    {
                        try
                        {
                            UnityEngine.Object.Destroy(uiObj);
                        }
                        catch { }
                    }
                    healModeUIIndicator.Remove(player);
                }

                lastHealFireballTime.Remove(player);

                if (activeHealingEffects.ContainsKey(player))
                {
                    var effect = activeHealingEffects[player];
                    if (effect != null)
                    {
                        try
                        {
                            UnityEngine.Object.Destroy(effect);
                        }
                        catch { }
                    }
                    activeHealingEffects.Remove(player);
                }

                previousWeapons.Remove(player);

                if (player.GetZDOID().UserID != 0)
                {
                    recentlyHealedPlayers.Remove(player.GetZDOID().UserID);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Staff Heal] 정리 실패: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 힐 파이어볼 태그 컴포넌트 (투사체 식별용)
    /// </summary>
    public class StaffHealingFireballTag : MonoBehaviour
    {
        public long casterPlayerId;        // 시전자 ID
        public float healPercentage;       // 힐링 비율 (25%)
        public float healRange;            // 힐링 범위 (5m)
        public bool ignorePVP = true;      // PVP 설정 무시
        public bool hasTriggered = false;  // 중복 발동 방지
        public int projectileIndex;        // 발사체 인덱스 (0: 왼쪽, 1: 오른쪽)
    }
    
    /// <summary>
    /// 힐 파이어볼 충돌 처리를 위한 Projectile.OnHit 패치
    /// </summary>
    [HarmonyPatch(typeof(Projectile), nameof(Projectile.OnHit))]
    [HarmonyPriority(Priority.Low)]
    public static class StaffHealingFireball_ProjectileHit_Patch
    {
        [HarmonyPrefix]
        private static void Prefix(Projectile __instance, Collider collider, bool water)
        {
            try
            {
                // 물에 떨어진 경우는 무시
                if (water)
                {
                    Plugin.Log.LogDebug($"[힐 파이어볼] 물에 떨어짐 - 무시: {__instance?.name}");
                    return;
                }
                
                // 힐 파이어볼 프로젝타일인지 확인
                var healTag = __instance?.GetComponent<StaffHealingFireballTag>();
                if (healTag == null)
                {
                    return; // 힐 파이어볼이 아니므로 조용히 반환
                }
                
                // 이미 발동했으면 무시
                if (healTag.hasTriggered)
                {
                    Plugin.Log.LogDebug("[힐 파이어볼] 이미 발동한 프로젝타일 - 무시");
                    return;
                }
                
                Plugin.Log.LogInfo($"[힐 파이어볼] 힐 파이어볼 충돌 감지! PlayerID: {healTag.casterPlayerId}");
                
                // 충돌 위치 계산
                Vector3 impactPoint = __instance.transform.position;
                
                // 원본 데미지를 0으로 설정 (힐링 전용)
                __instance.m_damage.Modify(0f); // 모든 데미지 0으로
                
                // 5m 범위 내 플레이어 검색
                var playersInRange = StaffHealingFireball.GetPlayersInRange(impactPoint, healTag.healRange);
                
                int healedCount = 0;
                
                // 중복 힐링 방지 체크
                long casterId = healTag.casterPlayerId;
                if (!StaffHealingFireball.recentlyHealedPlayers.ContainsKey(casterId))
                {
                    StaffHealingFireball.recentlyHealedPlayers[casterId] = new HashSet<long>();
                }
                
                foreach (var target in playersInRange)
                {
                    // 시전자 제외
                    if (target.GetPlayerID() == healTag.casterPlayerId)
                    {
                        Plugin.Log.LogDebug($"[힐 파이어볼] 시전자 제외: {target.GetPlayerName()}");
                        continue;
                    }
                    
                    // 생존 확인
                    if (target.IsDead())
                    {
                        Plugin.Log.LogDebug($"[힐 파이어볼] 사망한 플레이어 제외: {target.GetPlayerName()}");
                        continue;
                    }
                    
                    // 중복 힐링 방지 - 이미 이 시전으로 힐을 받았는지 확인
                    long targetId = target.GetPlayerID();
                    if (StaffHealingFireball.recentlyHealedPlayers[casterId].Contains(targetId))
                    {
                        Plugin.Log.LogDebug($"[힐 파이어볼] 이미 힐링을 받은 플레이어 제외: {target.GetPlayerName()}");
                        continue;
                    }
                    
                    // 힐링이 필요한지 확인 (체력이 만땅이 아닌 경우에만 힐링)
                    bool canBeHealed = StaffHealingFireball.CanBeHealed(target);
                    if (!canBeHealed)
                    {
                        Plugin.Log.LogDebug($"[힐 파이어볼] 힐링이 필요하지 않은 플레이어 제외: {target.GetPlayerName()} (체력: {target.GetHealth():F1}/{target.GetMaxHealth():F1})");
                        continue;
                    }
                    
                    // PVP 무시 힐링 실행
                    StaffHealingFireball.ProcessHealingIgnorePVP(target, healTag);
                    
                    // 중복 힐링 방지 목록에 추가
                    StaffHealingFireball.recentlyHealedPlayers[casterId].Add(targetId);
                    
                    // 힐링을 받은 캐릭터에게만 healing VFX 2초 유지 후 제거
                    StaffHealingFireball.PlayHealingVFXWithAutoRemoval(target);
                    
                    healedCount++;
                    Plugin.Log.LogInfo($"[힐 파이어볼] 힐링 완료: {target.GetPlayerName()}");
                }
                
                // 적중 지역에 healing 이팩트 표시 (프로젝타일 충돌 위치)
                Plugin.Log.LogInfo($"[힐 파이어볼] 충돌 지역 이팩트 재생 시도: healing at {impactPoint}");
                SimpleVFX.PlayWithSound("healing", "magic_heal", impactPoint, 3f);
                Plugin.Log.LogInfo($"[힐 파이어볼] 충돌 지역 이팩트 재생 완료");
                
                // 시전자에게 결과 메시지
                var caster = Player.GetAllPlayers().FirstOrDefault(p => p.GetPlayerID() == healTag.casterPlayerId);
                if (caster != null)
                {
                    caster.Message(MessageHud.MessageType.Center, $"💚 {healedCount}명 힐링 완료!");
                }
                
                // 발동 완료 처리
                healTag.hasTriggered = true;
                
                Plugin.Log.LogInfo($"[힐 파이어볼] 충돌 처리 완료 - 힐링된 플레이어 수: {healedCount}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 파이어볼] 충돌 패치 오류: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// 무기 바뀔 때마다 힐 모드 자동 종료 체크를 위한 Humanoid.EquipItem 패치
    /// </summary>
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))]
    [HarmonyPriority(Priority.Low)]
    public static class StaffHealingFireball_EquipItem_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(Humanoid __instance, ItemDrop.ItemData item, bool triggerEquipEffects)
        {
            try
            {
                // 플레이어인지 확인
                if (__instance is Player player)
                {
                    // 힐 모드가 활성화되어 있는지 확인 후 무기 변경 체크
                    StaffHealingFireball.CheckWeaponChangeAndDeactivateHeal(player);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[힐 모드] 무기 장착 패치 오류: {ex.Message}");
            }
        }
    }
}
using EpicMMOSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    public static partial class SkillEffect
    {
        // === 액티브 스킬 관련 상태 변수 ===
        // 분노의 망치 관련 필드는 MaceSkills.FuryHammer.cs로 완전 이동
        public static float tauntCooldown = 15f;
        public static float lastTauntTime = -999f;
        public static Dictionary<Player, Coroutine> tauntCooldownCoroutine = new Dictionary<Player, Coroutine>();

        // === 검 Sword Slash 스킬은 Sword_Skill.cs에서 관리 ===
        
        // === 반사 효과 관련 변수들 ===
        
        // === 이중 폭발 시스템 제거됨: 이제 즉시 발사체 생성 방식 사용 ===
        
        // === 석궁 "단 한 발" 액티브 스킬 변수 ===
        private static Dictionary<Player, float> crossbowOneShotCooldown = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> crossbowOneShotReady = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> crossbowOneShotExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> crossbowOneShotCoroutine = new Dictionary<Player, Coroutine>();
        private static readonly float crossbowOneShotCooldownTime = 60f; // 60초 쿨타임
        private static readonly float crossbowOneShotDuration = 30f; // 30초 지속
        
        // === 캐릭터를 따라다니는 버프 이펙트 변수 ===
        private static Dictionary<Player, GameObject> followingBuffEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, Coroutine> followingBuffCoroutines = new Dictionary<Player, Coroutine>();
        
        // === 새로운 액티브 스킬 상태 변수 ===
        // 이중 시전 쿨타임 관리 (발사체 방식)
        private static Dictionary<Player, float> staffDualExplosionCooldowns = new Dictionary<Player, float>();
        // 힐 스킬 쿨타임 관리 (즉시 범위 힐)
        private static Dictionary<Player, float> staffHealCooldowns = new Dictionary<Player, float>();

        // === 이중 시전 버프 상태 관리 (아처 멀티샷 패턴) ===
        private static Dictionary<Player, bool> staffDualCastReady = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> staffDualCastExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> staffDualCastBuffCoroutines = new Dictionary<Player, Coroutine>();

        // 이중 시전 버프 효과 인스턴스 관리 (플레이어별)
        private static Dictionary<Player, GameObject> staffDualCastBuffEffects = new Dictionary<Player, GameObject>();
        private static Dictionary<Player, GameObject> staffDualCastStatusEffects = new Dictionary<Player, GameObject>();

        // 이중 시전 버프 효과 프리팹 캐시 (한 번만 로드)
        private static GameObject cachedStaffDualCastBuffPrefab = null;
        private static GameObject cachedStaffDualCastStatusPrefab = null;

        // === 전문가 스킬 제한 시스템 ===
        

        // === 액티브 스킬 키 입력 처리 시스템 ===
        
        public static void HandleActiveSkillInputs(Player player)
        {
            if (player == null || player.IsDead()) return;
            // 키 입력은 SkillTreeInputListener에서 처리되므로 여기서는 제거
            // T키: 원거리 액티브 스킬 처리만
            HandleTKeySkills(player);
        }

        public static void HandleTKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;
                // 1. 석궁 단 한 발 (crossbow_Step6_expert)
                bool hasCrossbowExpert = HasSkill("crossbow_Step6_expert");
                bool isUsingCrossbow = IsUsingCrossbow(player);
                if (hasCrossbowExpert && isUsingCrossbow)
                {
                    Plugin.Log.LogDebug("[석궁 단 한 발] T키 단 한 발 버프 발동!");
                    ActivateCrossbowOneShot(player);
                    return;
                }
                
                // 2. 폭발 화살 (bow_Step6_critboost)
                bool hasExplosiveArrow = HasSkill("bow_Step6_critboost");
                bool isUsingBow = IsUsingBow(player);
                if (hasExplosiveArrow && isUsingBow)
                {
                    Plugin.Log.LogInfo("[폭발 화살] T키 폭발 화살 발동!");
                    ExecuteExplosiveArrow(player);
                    return;
                }
                
                // 3. 지팡이 이중시전 (staff_Step6_dual_cast)
                bool hasStaffDualCast = HasSkill("staff_Step6_dual_cast");
                if (hasStaffDualCast)
                {
                    bool isUsingStaff = IsUsingStaff(player);
                    if (isUsingStaff)
                    {
                        Plugin.Log.LogInfo("[지팡이 이중시전] T키 이중시전 발동!");
                        ActivateStaffDualCast(player);
                        return;
                    }
                    else
                    {
                        DrawFloatingText(player, "지팡이를 장착해야 합니다!", Color.red);
                        return;
                    }
                }
                
                // 조건 부족 시 메시지
                DrawFloatingText(player, "T키 액티브 스킬 조건 부족");
                return;
            }

        public static void HandleGKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;

            // G키: 보조형 액티브 스킬 - 착용한 무기에 따라 우선순위 결정
            float nowG = Time.time;

            // 현재 착용한 무기 타입 확인
            bool isUsingStaff = IsUsingStaff(player);
            bool isUsingDagger = SkillEffect.IsUsingDagger(player);
            bool isUsingSword = Sword_Skill.IsUsingSword(player);
            bool isUsingSpear = IsUsingSpear(player);
            bool isUsingMace = IsUsingTwoHandedMace(player);

            // 1. 지팡이 착용 시: 즉시 범위 힐 (staff_Step6_heal)
            if (isUsingStaff)
            {
                bool hasStaffHeal = HasSkill("staff_Step6_heal");
                if (hasStaffHeal)
                {
                    Plugin.Log.LogInfo("[지팡이 범위 힐] G키 즉시 범위 치료 발동!");
                    ActivateStaffAreaHeal(player);
                    return;
                }
                else
                {
                    DrawFloatingText(player, "지팡이 힐 스킬이 필요합니다!", Color.red);
                    return;
                }
            }

            // 2. 단검 착용 시: 암살자의 심장 (knife_step9_assassin_heart)
            if (isUsingDagger)
            {
                bool hasAssassinHeart = SkillEffect.HasSkill("knife_step9_assassin_heart");
                if (hasAssassinHeart)
                {
                    Plugin.Log.LogInfo("[암살자의 심장] G키 액티브 스킬 발동!");
                    SkillEffect.ActivateKnifeAssassinHeart(player);
                    return;
                }
                else
                {
                    DrawFloatingText(player, "암살자의 심장 스킬이 필요합니다!", Color.red);
                    return;
                }
            }

            // 3. 검 착용 시: Sword Slash (sword_step5_finalcut / sword_slash)
            if (isUsingSword)
            {
                bool hasSwordSlash = SkillEffect.HasSkill("sword_step5_finalcut") || SkillEffect.HasSkill("sword_slash");
                if (hasSwordSlash)
                {
                    Plugin.Log.LogInfo("[Sword Slash] G키 액티브 스킬 발동!");
                    Sword_Skill.ActivateSwordSlash(player);
                    return;
                }
                else
                {
                    DrawFloatingText(player, "검 액티브 스킬이 필요합니다!", Color.red);
                    return;
                }
            }

            // 4. 창 착용 시: 연공창 (spear_Step5_combo)
            if (isUsingSpear)
            {
                bool hasSpearCombo = SkillEffect.HasSkill("spear_Step5_combo");
                if (hasSpearCombo)
                {
                    Plugin.Log.LogInfo("[연공창] G키 액티브 스킬 발동!");
                    HandleSpearActiveSkill(player);
                    return;
                }

                // 스킬 없으면
                DrawFloatingText(player, "창 액티브 스킬이 필요합니다!", Color.red);
                return;
            }

            // 5. 폴암 착용 시: 장창의 제왕 (polearm_step5_king)
            bool isUsingPolearm = IsUsingPolearm(player);
            if (isUsingPolearm)
            {
                bool hasPolearmKing = HasSkill("polearm_step5_king");
                if (hasPolearmKing)
                {
                    Plugin.Log.LogInfo("[장창의 제왕] G키 액티브 스킬 발동!");
                    UsePolearmKingSkill(player);
                    return;
                }
                else
                {
                    DrawFloatingText(player, "장창의 제왕 스킬이 필요합니다!", Color.red);
                    return;
                }
            }

            // 6. 둔기 착용 시: 분노의 망치 → MaceSkills.FuryHammer.cs
            if (isUsingMace)
            {
                FuryHammerSkill.HandleGKeyPress(player);
                return;
            }

            // 방패 + 한손둔기 - 수호자의 진심 (G키)
            bool isUsingShield = HasShield(player);
            bool isUsingOneHandedMace = IsUsingOneHandedMace(player);
            if (isUsingShield && isUsingOneHandedMace)
            {
                bool hasGuardianHeart = HasSkill("mace_Step7_guardian_heart");
                if (hasGuardianHeart)
                {
                    Plugin.Log.LogDebug("[수호자의 진심] G키 액티브 스킬 발동!");
                    ActivateGuardianHeart(player);
                    return;
                }
                else
                {
                    DrawFloatingText(player, "수호자의 진심 스킬이 필요합니다!", Color.red);
                    return;
                }
            }

            // 무기 미착용 또는 G키 지원 안하는 무기
            DrawFloatingText(player, "G키를 지원하는 무기를 착용하세요!", Color.red);
        }

        public static void HandleGKeyUpSkills(Player player)
        {
            if (player == null || player.IsDead()) return;

            // G키 해제: 분노의 망치 → MaceSkills.FuryHammer.cs
            FuryHammerSkill.HandleGKeyRelease(player);
        }
        // === 헬퍼 함수들 ===
        /// <summary>
        /// 플레이어가 활을 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 Bows 스킬 타입
        /// 2순위: 프리팹 이름에 "Bow", "bow", "Longbow", "longbow" 포함
        /// 3순위: 무기 이름에 "활", "bow", "longbow" 포함
        /// </summary>
        public static bool IsUsingBow(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 Bows 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Bows)
            {
                Plugin.Log.LogDebug($"[활 감지] Valheim 기본 Bows 스킬 타입: {weapon.m_shared.m_name}");
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Bow") || prefabName.Contains("bow") || 
                prefabName.Contains("Longbow") || prefabName.Contains("longbow"))
            {
                Plugin.Log.LogInfo($"[활 감지] 프리팹 이름으로 활 감지: {prefabName} ({weapon.m_shared.m_name})");
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("활") || weaponName.Contains("bow") || weaponName.Contains("longbow"))
            {
                Plugin.Log.LogInfo($"[활 감지] 무기 이름으로 활 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                return true;
            }
            
            Plugin.Log.LogDebug($"[활 감지] 활이 아님: {weapon.m_shared.m_name} (스킬타입: {weapon.m_shared.m_skillType}, 프리팹: {prefabName})");
            return false;
        }
        
        /// <summary>
        /// 플레이어가 석궁을 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: 프리팹 이름에 "Crossbow", "crossbow" 포함
        /// 2순위: 무기 이름에 "석궁", "crossbow" 포함
        /// 3순위: Valheim 기본 스킬 타입 체크 (석궁은 보통 Bows로 분류됨)
        /// </summary>
        public static bool IsUsingCrossbow(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Crossbow") || prefabName.Contains("crossbow"))
            {
                Plugin.Log.LogDebug($"[석궁 감지] 프리팹 이름으로 석궁 감지: {prefabName} ({weapon.m_shared.m_name})");
                return true;
            }

            // 2순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("석궁") || weaponName.Contains("crossbow"))
            {
                Plugin.Log.LogDebug($"[석궁 감지] 무기 이름으로 석궁 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                return true;
            }
            
            // 3순위: Valheim 기본 스킬 타입 확인 (석궁은 보통 Bows로 분류)
            if (weapon.m_shared.m_skillType == Skills.SkillType.Bows && 
                (prefabName.ToLower().Contains("crossbow") || weaponName.Contains("crossbow")))
            {
                Plugin.Log.LogDebug($"[석궁 감지] Bows 스킬 타입의 석궁: {weapon.m_shared.m_name}");
                return true;
            }
            
            Plugin.Log.LogDebug($"[석궁 감지] 석궁이 아님: {weapon.m_shared.m_name} (스킬타입: {weapon.m_shared.m_skillType}, 프리팹: {prefabName})");
            return false;
        }
        
        /// <summary>
        /// 플레이어가 지팡이를 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 ElementalMagic 스킬 타입
        /// 2순위: 프리팹 이름에 "Staff", "staff", "Wand", "wand", "Rod", "rod" 포함
        /// 3순위: 무기 이름에 "지팡이", "staff", "wand", "rod" 포함
        /// </summary>
        public static bool IsUsingStaff(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 ElementalMagic 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic)
            {
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Staff") || prefabName.Contains("staff") || 
                prefabName.Contains("Wand") || prefabName.Contains("wand") ||
                prefabName.Contains("Rod") || prefabName.Contains("rod"))
            {
                Plugin.Log.LogInfo($"[지팡이 감지] 프리팹 이름으로 지팡이 감지: {prefabName} ({weapon.m_shared.m_name})");
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("지팡이") || weaponName.Contains("staff") || 
                weaponName.Contains("wand") || weaponName.Contains("rod"))
            {
                Plugin.Log.LogInfo($"[지팡이 감지] 무기 이름으로 지팡이 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                return true;
            }

            return false;
        }
        
        // === 새로운 액티브 스킬 함수들 ===
        // 폭발 화살 함수는 SkillEffect.ExplosiveArrow.cs에서 구현됨
        
        /// <summary>
        /// 이중 시전 버프 활성화 (T키 - 아처 멀티샷 패턴)
        /// 30초간 버프 상태 활성화, 다음 마법 공격 시 추가 발사체 2개 발사
        /// </summary>
        public static void ActivateStaffDualCast(Player player)
        {
            try
            {
                // 이미 호출 전에 지팡이 확인 완료

                // 쿨타임 확인
                if (staffDualExplosionCooldowns.ContainsKey(player) && Time.time < staffDualExplosionCooldowns[player])
                {
                    float remaining = staffDualExplosionCooldowns[player] - Time.time;
                    DrawFloatingText(player, $"이중시전 쿨타임: {Mathf.CeilToInt(remaining)}초", Color.red);
                    return;
                }

                // 에이트르 확인 (컨피그 값 사용)
                float eitrCost = Staff_Config.StaffDoubleCastEitrCostValue;
                if (player.GetEitr() < eitrCost)
                {
                    DrawFloatingText(player, $"에이트르가 부족합니다 ({eitrCost} 필요)", Color.red);
                    return;
                }

                // 에이트르 소모
                player.UseEitr(eitrCost);

                // 쿨타임 적용
                staffDualExplosionCooldowns[player] = Time.time + Staff_Config.StaffDoubleCastCooldownValue;

                // T키로 이중 시전 버프 활성화 (30초간 지속)
                float buffDuration = 30f;
                staffDualCastReady[player] = true;
                staffDualCastExpiry[player] = Time.time + buffDuration;

                // 버프 활성화 VFX/SFX 효과
                PlayStaffDualCastBuffActivationEffects(player);

                // 기존 코루틴 중단 (중복 실행 방지)
                if (staffDualCastBuffCoroutines.ContainsKey(player))
                {
                    SkillTreeInputListener.Instance.StopCoroutine(staffDualCastBuffCoroutines[player]);
                    staffDualCastBuffCoroutines.Remove(player);
                    Plugin.Log.LogInfo("[이중 시전] 기존 코루틴 중단");
                }

                // 30초 타이머 코루틴 시작 및 등록
                var coroutine = SkillTreeInputListener.Instance.StartCoroutine(StaffDualCastBuffTimer(player, buffDuration));
                staffDualCastBuffCoroutines[player] = coroutine;

                DrawFloatingText(player, $"✨ 이중시전 준비! (30초간)", new Color(0.8f, 0.3f, 1f, 1f));
                Plugin.Log.LogInfo($"[이중 시전] T키로 버프 활성화 - 지속시간: {buffDuration}초, 에이트르 소모: {eitrCost}");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 버프 활성화 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 추가 마법 발사체 생성 (-5도, +5도 각도로 2발)
        /// </summary>
        private static void CreateAdditionalMagicProjectiles(Player player)
        {
            try
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return;

                Vector3 playerPos = player.transform.position + Vector3.up * 1.5f;
                Vector3 forward = player.transform.forward;
                Quaternion playerRotation = player.transform.rotation;

                // 컨피그 값들
                int projectileCount = Staff_Config.StaffDoubleCastProjectileCountValue;
                float angleOffset = Staff_Config.StaffDoubleCastAngleOffsetValue;
                float damagePercent = Staff_Config.StaffDoubleCastDamagePercentValue / 100f;

                // 2발의 발사체를 -5도, +5도 각도로 생성
                for (int i = 0; i < projectileCount; i++)
                {
                    float angle = (i == 0) ? -angleOffset : angleOffset; // 첫번째: -5도, 두번째: +5도
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * forward;

                    CreateValheimProjectile(player, weapon, direction, damagePercent, i + 1);
                }

                Plugin.Log.LogInfo($"[이중 시전] 추가 발사체 {projectileCount}발 생성 완료 - 각도: ±{angleOffset}도, 데미지: {damagePercent * 100}%");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 발사체 생성 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 개별 마법 발사체 생성
        /// </summary>
        /// <summary>
        /// Valheim 발사체 시스템을 직접 사용하여 추가 발사체 생성
        /// </summary>
        private static void CreateValheimProjectile(Player player, ItemDrop.ItemData weapon, Vector3 direction, float damagePercent, int projectileIndex)
        {
            try
            {
                Plugin.Log.LogInfo($"[이중 시전] Valheim 발사체 {projectileIndex}번 생성 시작");

                // 무기의 공격 정보 가져오기
                var attack = weapon.m_shared.m_attack;
                if (attack.m_attackProjectile == null)
                {
                    Plugin.Log.LogWarning($"[이중 시전] 무기에 발사체가 설정되지 않음: {weapon.m_shared.m_name}");
                    return;
                }

                // 발사 위치 계산 (플레이어 위치 + 약간 위쪽)
                Vector3 startPos = player.transform.position + Vector3.up * 1.5f + player.transform.forward * 1.0f;

                Plugin.Log.LogInfo($"[이중 시전] 발사체 생성 - 위치: {startPos}, 방향: {direction}, 원본프리팹: {attack.m_attackProjectile.name}");

                // Valheim의 실제 발사체 생성
                GameObject projectile = UnityEngine.Object.Instantiate(attack.m_attackProjectile, startPos, Quaternion.LookRotation(direction));

                if (projectile != null)
                {
                    // 발사체 컴포넌트 설정
                    var projectileComp = projectile.GetComponent<Projectile>();
                    if (projectileComp != null)
                    {
                        // 데미지 조정 - 무기의 데미지 정보 사용
                        projectileComp.m_damage = weapon.GetDamage();
                        projectileComp.m_damage.Modify(damagePercent);

                        // 발사체 속도 설정
                        var rb = projectile.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            float projectileSpeed = attack.m_projectileVel;
                            rb.velocity = direction.normalized * projectileSpeed;
                            Plugin.Log.LogInfo($"[이중 시전] 발사체 속도 설정: {projectileSpeed}");
                        }

                        // 소유자 설정
                        projectileComp.Setup(player, direction * attack.m_projectileVel, -1f, null, weapon, null);

                        Plugin.Log.LogInfo($"[이중 시전] Valheim 발사체 {projectileIndex}번 성공적으로 생성");
                    }
                    else
                    {
                        Plugin.Log.LogWarning($"[이중 시전] 생성된 오브젝트에 Projectile 컴포넌트가 없음");
                    }
                }
                else
                {
                    Plugin.Log.LogError($"[이중 시전] 발사체 생성 실패 - Instantiate 반환값이 null");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] Valheim 발사체 생성 오류: {ex.Message}\n스택 트레이스: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// 이중 시전 버프 활성화 효과 (buff_02a + statusailment_01_aura + sfx_StaffLightning_charge)
        /// 아처 멀티샷과 동일한 패턴 적용
        /// </summary>
        public static void PlayStaffDualCastBuffActivationEffects(Player player)
        {
            try
            {
                // 1. 캐릭터 발밑 buff_02a 효과 (1회 사용 후 사라지는 따라다니는 효과)
                PlayStaffDualCastBuffEffect(player);

                // 2. 캐릭터 머리위 statusailment_01_aura 효과 (버프 지속 상태 표시)
                PlayStaffDualCastStatusEffect(player);

                // 3. sfx_StaffLightning_charge 사운드 효과
                PlayStaffDualCastActivationSound(player);

                Plugin.Log.LogInfo("[이중 시전] buff_02a(발밑) + statusailment_01_aura(머리위) + sfx_StaffLightning_charge 활성화 효과 재생 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 버프 활성화 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중 시전 버프 활성화 시 캐릭터 발밑 효과 (아처 패턴 동일)
        /// </summary>
        private static void PlayStaffDualCastBuffEffect(Player player)
        {
            try
            {
                // 캐시된 프리팹이 없으면 한 번만 로드
                if (cachedStaffDualCastBuffPrefab == null)
                {
                    // VFXManager를 통해 buff_02a 프리팹 로드
                    cachedStaffDualCastBuffPrefab = VFXManager.GetVFXPrefab("buff_02a");
                    if (cachedStaffDualCastBuffPrefab != null)
                    {
                        Plugin.Log.LogInfo("[이중 시전] VFXManager를 통해 buff_02a 프리팹 캐시됨");
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[이중 시전] VFXManager에서 buff_02a를 찾을 수 없음");
                        return;
                    }
                }

                // 기존 버프 효과가 있으면 제거
                if (staffDualCastBuffEffects.ContainsKey(player) && staffDualCastBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastBuffEffects[player]);
                    staffDualCastBuffEffects.Remove(player);
                    Plugin.Log.LogInfo("[이중 시전] 기존 버프 효과 제거");
                }

                // buff_02a 효과 실행 (캐릭터를 따라다니며 1회 사용 후 사라짐)
                if (cachedStaffDualCastBuffPrefab != null)
                {
                    // 캐릭터 발밑 위치 계산 (약간 아래쪽으로)
                    var footPosition = player.transform.position + Vector3.down * 0.1f;
                    var effectInstance = UnityEngine.Object.Instantiate(cachedStaffDualCastBuffPrefab, footPosition, Quaternion.identity);

                    // 캐릭터를 따라다니도록 부모 설정
                    effectInstance.transform.SetParent(player.transform, false);
                    effectInstance.transform.localPosition = Vector3.down * 0.1f; // 발밑에서 살짝 아래

                    // 🎯 VFX 크기 조정 - 40% 크기 유지 (아처와 동일)
                    effectInstance.transform.localScale = Vector3.one * 0.4f;

                    // 🎯 VFX 투명도 조정 - 20% 불투명도 (마법적 느낌을 위해 아처보다 조금 더 진하게)
                    SetStaffDualCastBuffTransparency(effectInstance, 0.2f);

                    // 버프 효과 인스턴스 저장 (1회 사용 후 제거하기 위해)
                    staffDualCastBuffEffects[player] = effectInstance;

                    Plugin.Log.LogInfo("[이중 시전] buff_02a 버프 효과 재생 완료 (캐릭터 추적, 40% 크기, 20% 투명도, 발밑 위치, 1회 사용 후 제거)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 버프 활성화 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중 시전 상태 표시 효과 (statusailment_01_aura - 캐릭터 머리 위, 버프 사용할 때까지 지속)
        /// </summary>
        private static void PlayStaffDualCastStatusEffect(Player player)
        {
            try
            {
                // 캐시된 상태 효과 프리팹이 없으면 한 번만 로드
                if (cachedStaffDualCastStatusPrefab == null)
                {
                    // VFXManager를 통해 statusailment_01_aura 프리팹 로드
                    cachedStaffDualCastStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedStaffDualCastStatusPrefab != null)
                    {
                        Plugin.Log.LogInfo("[이중 시전] VFXManager를 통해 statusailment_01_aura 프리팹 캐시됨");
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[이중 시전] VFXManager에서 statusailment_01_aura를 찾을 수 없음");
                        return;
                    }
                }

                // 기존 상태 효과가 있으면 제거
                if (staffDualCastStatusEffects.ContainsKey(player) && staffDualCastStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastStatusEffects[player]);
                    staffDualCastStatusEffects.Remove(player);
                    Plugin.Log.LogInfo("[이중 시전] 기존 상태 효과 제거");
                }

                // statusailment_01_aura 효과 실행 (캐릭터 머리 위에서 버프 완료까지 지속)
                if (cachedStaffDualCastStatusPrefab != null)
                {
                    // 캐릭터 머리 위 위치 계산 (약 2미터 위)
                    var headPosition = player.transform.position + Vector3.up * 2.0f;
                    var statusInstance = UnityEngine.Object.Instantiate(cachedStaffDualCastStatusPrefab, headPosition, Quaternion.identity);

                    // 캐릭터를 따라다니도록 부모 설정
                    statusInstance.transform.SetParent(player.transform, false);
                    statusInstance.transform.localPosition = Vector3.up * 2.0f; // 머리 위 고정 위치

                    // 🎯 상태 효과 크기 조정 - 70% 크기 (마법적 느낌을 위해 아처보다 조금 크게)
                    statusInstance.transform.localScale = Vector3.one * 0.7f;

                    // 상태 효과 인스턴스 저장 (버프 사용 후 제거하기 위해)
                    staffDualCastStatusEffects[player] = statusInstance;

                    Plugin.Log.LogInfo("[이중 시전] statusailment_01_aura 상태 효과 재생 완료 (캐릭터 추적, 70% 크기, 머리 위 위치, 버프 완료까지 지속)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 상태 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중 시전 활성화 시 사운드 효과 (sfx_StaffLightning_charge)
        /// </summary>
        private static void PlayStaffDualCastActivationSound(Player player)
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
                        Plugin.Log.LogWarning("[이중 시전] sfx_StaffLightning_charge 사운드를 찾을 수 없음");
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 활성화 사운드 재생 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중 시전 버프 효과의 투명도 설정 (아처 시스템과 동일한 방식)
        /// </summary>
        private static void SetStaffDualCastBuffTransparency(GameObject buffEffect, float alpha)
        {
            try
            {
                if (buffEffect == null)
                {
                    Plugin.Log.LogWarning("[이중 시전] 버프 효과가 null - 투명도 설정 불가");
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

                Plugin.Log.LogInfo($"[이중 시전] 버프 효과 투명도 {alpha * 100}% 설정 완료 (Renderer: {renderers.Length}, ParticleSystem: {particleSystems.Length})");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 버프 효과 투명도 설정 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중 시전 버프 30초 타이머 코루틴 (아처 멀티샷 패턴)
        /// </summary>
        private static IEnumerator StaffDualCastBuffTimer(Player player, float buffDuration)
        {
            Plugin.Log.LogInfo($"[이중 시전] 30초 타이머 시작 - 플레이어: {player.GetPlayerName()}");

            float startTime = Time.time;
            float nextRemindTime = startTime + 10f; // 10초마다 상기시킬 시간

            // 30초 동안 또는 버프가 사용될 때까지 대기
            while (staffDualCastReady.ContainsKey(player) && staffDualCastReady[player] &&
                   Time.time < staffDualCastExpiry[player])
            {
                // 플레이어 사망 또는 로그아웃 체크
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogInfo($"[이중 시전] 플레이어 사망/로그아웃으로 버프 조기 정리");
                    ClearStaffDualCastBuff(player);
                    yield break;
                }

                // 10초마다 남은 시간 표시 (아처와 달리 더 자주 표시)
                if (Time.time >= nextRemindTime)
                {
                    float remainingTime = staffDualCastExpiry[player] - Time.time;
                    if (remainingTime > 0)
                    {
                        DrawFloatingText(player, $"✨ 이중 시전 준비됨 ({remainingTime:F0}초)",
                            new Color(0.8f, 0.3f, 1f, 1f));
                        nextRemindTime = Time.time + 10f; // 다음 알림 시간 설정
                    }
                }

                yield return new WaitForSeconds(1f); // 1초마다 체크
            }

            // 타이머 만료 또는 버프 사용 완료 처리
            if (staffDualCastReady.ContainsKey(player) && staffDualCastReady[player])
            {
                // 30초 시간 만료
                DrawFloatingText(player, "이중 시전 버프 만료", Color.yellow);
                ClearStaffDualCastBuff(player);
                Plugin.Log.LogInfo($"[이중 시전] 30초 타이머 만료 - 버프 자동 해제");
            }
            else
            {
                // 버프가 사용되어 이미 해제됨
                Plugin.Log.LogInfo($"[이중 시전] 버프 사용으로 타이머 정상 종료");
            }
        }

        /// <summary>
        /// 이중 시전 버프 상태 확인
        /// </summary>
        public static bool IsStaffDualCastReady(Player player)
        {
            return staffDualCastReady.ContainsKey(player) &&
                   staffDualCastReady[player] &&
                   Time.time < staffDualCastExpiry[player];
        }

        /// <summary>
        /// 이중 시전 버프 정리 (30초 만료 또는 사용 완료 시)
        /// </summary>
        public static void ClearStaffDualCastBuff(Player player)
        {
            try
            {
                if (player == null) return;

                // 버프 상태 제거
                if (staffDualCastReady.ContainsKey(player))
                {
                    staffDualCastReady.Remove(player);
                    Plugin.Log.LogInfo($"[이중 시전] 버프 상태 제거: {player.GetPlayerName()}");
                }

                if (staffDualCastExpiry.ContainsKey(player))
                {
                    staffDualCastExpiry.Remove(player);
                    Plugin.Log.LogInfo($"[이중 시전] 만료 시간 제거: {player.GetPlayerName()}");
                }

                // 버프 효과 제거 (buff_02a - 발밑 효과)
                if (staffDualCastBuffEffects.ContainsKey(player) && staffDualCastBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastBuffEffects[player]);
                    staffDualCastBuffEffects.Remove(player);
                    Plugin.Log.LogInfo("[이중 시전] 버프 효과 제거 (buff_02a)");
                }

                // 상태 효과 제거 (statusailment_01_aura - 머리위 효과)
                if (staffDualCastStatusEffects.ContainsKey(player) && staffDualCastStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(staffDualCastStatusEffects[player]);
                    staffDualCastStatusEffects.Remove(player);
                    Plugin.Log.LogInfo("[이중 시전] 상태 효과 제거 (statusailment_01_aura)");
                }

                // 코루틴 정상 종료 시 Dictionary에서 제거
                if (staffDualCastBuffCoroutines.ContainsKey(player))
                {
                    staffDualCastBuffCoroutines.Remove(player);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 버프 정리 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중 시전 실제 실행 (마법 공격 시 추가 발사체 2개 발사 + 버프 해제)
        /// 아처 멀티샷의 PerformArcherMultiShotAttack 패턴과 동일
        /// </summary>
        public static void PerformStaffDualCastAttack(Player player, ItemDrop.ItemData weapon, Vector3 baseDirection)
        {
            try
            {
                if (!IsStaffDualCastReady(player))
                {
                    Plugin.Log.LogWarning("[이중 시전] 버프 상태가 아님 - 실행 취소");
                    return;
                }

                Plugin.Log.LogInfo($"[이중 시전] 실제 실행 시작 - 추가 발사체 2개 생성, 무기: {weapon?.m_shared?.m_name}");

                // 컨피그 값들
                int projectileCount = Staff_Config.StaffDoubleCastProjectileCountValue;
                float angleOffset = Staff_Config.StaffDoubleCastAngleOffsetValue;
                float damagePercent = Staff_Config.StaffDoubleCastDamagePercentValue / 100f;

                Plugin.Log.LogInfo($"[이중 시전] 설정값 - 발사체수: {projectileCount}, 각도: {angleOffset}도, 데미지: {damagePercent * 100}%");

                // Valheim의 실제 Attack 시스템 사용
                var currentAttack = weapon.m_shared.m_attack.Clone();

                // 2발의 추가 발사체를 -5도, +5도 각도로 생성
                for (int i = 0; i < projectileCount; i++)
                {
                    float angle = (i == 0) ? -angleOffset : angleOffset; // 첫번째: -5도, 두번째: +5도
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * baseDirection;

                    Plugin.Log.LogInfo($"[이중 시전] 발사체 {i + 1}번 생성 - 각도: {angle}도, 방향: {direction}");

                    // Valheim 발사체 시스템 직접 사용
                    CreateValheimProjectile(player, weapon, direction, damagePercent, i + 1);
                }

                // 버프 사용 완료 - 즉시 해제
                ClearStaffDualCastBuff(player);

                DrawFloatingText(player, $"✨ 이중 시전 발동! 추가 발사체 {projectileCount}개",
                    new Color(0.8f, 0.3f, 1f, 1f));

                Plugin.Log.LogInfo($"[이중 시전] 실제 실행 완료 - 추가 발사체 {projectileCount}개 생성, 버프 해제됨");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] 실제 실행 오류: {ex.Message}\n스택 트레이스: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// 지팡이 즉시 범위 힐 스킬
        /// </summary>
        public static void ActivateStaffAreaHeal(Player player)
        {
            try
            {
                // 쿨타임 확인
                if (staffHealCooldowns.ContainsKey(player) && Time.time < staffHealCooldowns[player])
                {
                    float remaining = staffHealCooldowns[player] - Time.time;
                    DrawFloatingText(player, $"힐 쿨타임: {Mathf.CeilToInt(remaining)}초", Color.red);
                    return;
                }

                // 에이트르 확인
                float eitrCost = HealerMode_Config.HealerModeEitrCostValue;
                if (player.GetEitr() < eitrCost)
                {
                    DrawFloatingText(player, $"에이트르가 부족합니다 ({eitrCost} 필요)", Color.red);
                    return;
                }

                // 에이트르 소모
                player.UseEitr(eitrCost);

                // 쿨타임 적용
                staffHealCooldowns[player] = Time.time + HealerMode_Config.HealerModeCooldownValue;

                // 범위 힐 실행 (성기사 패턴 적용)
                ExecuteAreaHeal(player);

                Plugin.Log.LogInfo($"[지팡이 범위 힐] 발동 - 범위: {HealerMode_Config.HealRangeValue}m, 치료량: {HealerMode_Config.HealPercentageValue}%");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 범위 힐] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 시전자를 중심으로 한 즉시 범위 치료 실행 (성기사 패턴 기반)
        /// </summary>
        private static void ExecuteAreaHeal(Player caster)
        {
            try
            {
                Vector3 casterPos = caster.transform.position;
                float healRange = HealerMode_Config.HealRangeValue;
                float healPercent = HealerMode_Config.HealPercentageValue / 100f;

                // 1. 스킬 발동 시 이펙트 및 사운드 (ZRoutedRpc 멀티플레이어 동기화)
                try
                {
                    Plugin.Log.LogInfo($"[지팡이 힐] VFX 재생 시도 - 위치: {casterPos}, 회전: {caster.transform.rotation}");

                    // 시전자 위치에 shaman_heal_aoe 이펙트 + 사운드
                    SimpleVFX.PlayWithSound("shaman_heal_aoe", "sfx_dverger_heal_finish", casterPos, 3f);

                    Plugin.Log.LogInfo("[지팡이 힐] shaman_heal_aoe VFX/사운드 재생 완료");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[지팡이 힐] VFX/사운드 재생 실패: {ex.Message}\n{ex.StackTrace}");
                }

                // 2. 범위 내 모든 플레이어에게 즉시 힐링 적용 (시전자 제외)
                var allPlayers = Player.GetAllPlayers();
                Plugin.Log.LogInfo($"[지팡이 힐] 전체 플레이어 수: {allPlayers.Count}, 시전자: {caster.GetPlayerName()} (ID: {caster.GetPlayerID()})");

                var nearbyPlayers = allPlayers
                    .Where(p => {
                        bool isCaster = p == caster;
                        float distance = Vector3.Distance(p.transform.position, casterPos);
                        bool inRange = distance <= healRange;
                        bool alive = !p.IsDead();
                        bool shouldHeal = !isCaster && inRange && alive;

                        Plugin.Log.LogInfo($"[지팡이 힐] 플레이어 {p.GetPlayerName()} - 시전자여부: {isCaster}, 거리: {distance:F1}m, 범위내: {inRange}, 생존: {alive}, 힐대상: {shouldHeal}");

                        return shouldHeal;
                    })
                    .ToList();

                Plugin.Log.LogInfo($"[지팡이 힐] 시전자 제외 후 최종 힐 대상: {nearbyPlayers.Count}명");

                int healedCount = 0;

                foreach (var targetPlayer in nearbyPlayers)
                {
                    try
                    {
                        // 체력 회복량 계산
                        float maxHealth = targetPlayer.GetMaxHealth();
                        float healAmount = maxHealth * healPercent;

                        // 즉시 체력 회복 (성기사와 동일한 Heal 메서드 사용)
                        targetPlayer.Heal(healAmount, true);

                        // 각 플레이어 위치에 개별 힐 이펙트
                        try
                        {
                            Vector3 playerPos = targetPlayer.transform.position;
                            Plugin.Log.LogInfo($"[지팡이 힐] 개별 VFX 재생 시도 - {targetPlayer.GetPlayerName()} at {playerPos}");

                            SimpleVFX.Play("vfx_spawn_small", playerPos, 1f);

                            Plugin.Log.LogInfo($"[지팡이 힐] 개별 VFX 재생 완료 - {targetPlayer.GetPlayerName()}");
                        }
                        catch (Exception vfxEx)
                        {
                            Plugin.Log.LogError($"[지팡이 힐] 개별 VFX 재생 실패: {vfxEx.Message}\n{vfxEx.StackTrace}");
                        }

                        // 플로팅 텍스트 (해당 플레이어에게)
                        DrawFloatingText(targetPlayer, $"✨ +{healAmount:F0} HP", Color.green);

                        healedCount++;
                        Plugin.Log.LogInfo($"[지팡이 힐] {targetPlayer.GetPlayerName()}을 치료 - {healAmount:F0} HP 회복");
                    }
                    catch (Exception healEx)
                    {
                        Plugin.Log.LogError($"[지팡이 힐] {targetPlayer?.GetPlayerName() ?? "Unknown"} 힐링 실패: {healEx.Message}");
                    }
                }

                // 3. 시전자에게 결과 알림
                if (healedCount > 0)
                {
                    DrawFloatingText(caster, $"💚 지팡이 신성한 치유! ({healedCount}명 치료)", Color.green);
                }
                else
                {
                    DrawFloatingText(caster, "💚 치료할 대상이 없습니다", Color.yellow);
                }

                Plugin.Log.LogInfo($"[지팡이 힐] 총 {healedCount}명 치료 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[지팡이 힐] 실행 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 힐 이펙트 재생
        /// </summary>
        private static void PlayHealEffect(Vector3 position)
        {
            try
            {
                // 힐 이펙트
                var healEffect = ZNetScene.instance?.GetPrefab("vfx_HealthUpgrade");
                if (healEffect != null)
                {
                    var effectObj = UnityEngine.Object.Instantiate(healEffect, position + Vector3.up * 1f, Quaternion.identity);
                    // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                    var znetView = effectObj?.GetComponent<ZNetView>();
                    if (znetView != null) UnityEngine.Object.DestroyImmediate(znetView);
                    if (effectObj != null) UnityEngine.Object.Destroy(effectObj, 3f);
                }

                // 힐 사운드
                var healSound = ZNetScene.instance?.GetPrefab("sfx_dverger_heal_start");
                if (healSound != null)
                {
                    var soundObj = UnityEngine.Object.Instantiate(healSound, position, Quaternion.identity);
                    // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                    var znetView2 = soundObj?.GetComponent<ZNetView>();
                    if (znetView2 != null) UnityEngine.Object.DestroyImmediate(znetView2);
                    if (soundObj != null) UnityEngine.Object.Destroy(soundObj, 3f);
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[힐 이펙트] 재생 오류: {ex.Message}");
            }
        }

        // === 기존 이중 폭발 시스템 제거됨: 새로운 즉시 발사체 시스템 사용 ===
        
        // 기존 ActivateStaffHeal 메서드 제거됨 - StaffHealingFireball 시스템으로 대체
        
        // 기존 도트 힐 및 범위 플레이어 검색 메서드 제거됨 - StaffHealingFireball 시스템으로 대체
        
        public static void ActivateCrossbowOneShot(Player player)
        {
            if (!crossbowOneShotCooldown.ContainsKey(player))
                crossbowOneShotCooldown[player] = 0f;
            if (Time.time - crossbowOneShotCooldown[player] < crossbowOneShotCooldownTime)
            {
                float remainingCooldown = crossbowOneShotCooldownTime - (Time.time - crossbowOneShotCooldown[player]);
                DrawFloatingText(player, $"단 한 발 쿨타임: {remainingCooldown:F1}초");
                if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
                {
                    player.StopCoroutine(crossbowOneShotCoroutine[player]);
                }
                crossbowOneShotCoroutine[player] = player.StartCoroutine(ShowCooldownDisplay(player, remainingCooldown, "단 한 발"));
                return;
            }
            if (!IsUsingCrossbow(player))
            {
                DrawFloatingText(player, "석궁을 착용해야 합니다!");
                return;
            }
            crossbowOneShotCooldown[player] = Time.time;
            crossbowOneShotReady[player] = true;
            crossbowOneShotExpiry[player] = Time.time + crossbowOneShotDuration;
            if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
            {
                player.StopCoroutine(crossbowOneShotCoroutine[player]);
            }
            crossbowOneShotCoroutine[player] = player.StartCoroutine(CrossbowOneShotBuffDisplay(player));
            try
            {
                // 캐릭터를 따라다니는 지속 효과만 시작 (즉시 효과 제거)
                StartFollowingBuffEffect(player);
                
                // 효과음 재생
                var buffSoundNames = new string[] { "sfx_creature_tamed", "sfx_build_hammer_metal" };
                foreach (var soundName in buffSoundNames)
                {
                    var sound = ZNetScene.instance?.GetPrefab(soundName)?.GetComponent<AudioSource>()?.clip;
                    if (sound != null)
                    {
                        AudioSource.PlayClipAtPoint(sound, player.transform.position);
                        Plugin.Log.LogDebug($"[석궁 단 한 발] 버프 사운드 재생: {soundName}");
                        break;
                    }
                }
            }
            catch (System.Exception buffEffectEx)
            {
                Plugin.Log.LogError($"[단 한 발 버프] VFX 재생 실패: {buffEffectEx.Message}");
            }
            DrawFloatingText(player, "🎯 단 한 발 준비 완료! (30초)");
        }
        
        /// <summary>
        /// 캐릭터를 따라다니는 버프 이펙트 시작
        /// </summary>
        private static void StartFollowingBuffEffect(Player player)
        {
            try
            {
                // 기존 이펙트가 있다면 정리
                StopFollowingBuffEffect(player);
                
                // 새로운 이펙트 생성 및 시작
                if (followingBuffCoroutines.ContainsKey(player) && followingBuffCoroutines[player] != null)
                {
                    player.StopCoroutine(followingBuffCoroutines[player]);
                }
                
                followingBuffCoroutines[player] = player.StartCoroutine(FollowingBuffEffectCoroutine(player));
                Plugin.Log.LogDebug($"[석궁 단 한 발] {player.GetPlayerName()}에게 캐릭터 따라다니는 버프 이펙트 시작");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogDebug($"[석궁 단 한 발] 캐릭터 따라다니는 이펙트 시작 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 캐릭터를 따라다니는 버프 이펙트 정리
        /// </summary>
        private static void StopFollowingBuffEffect(Player player)
        {
            try
            {
                // 코루틴 중지
                if (followingBuffCoroutines.ContainsKey(player) && followingBuffCoroutines[player] != null)
                {
                    player.StopCoroutine(followingBuffCoroutines[player]);
                    followingBuffCoroutines[player] = null;
                }
                
                // 이펙트 객체 정리
                if (followingBuffEffects.ContainsKey(player) && followingBuffEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(followingBuffEffects[player]);
                    followingBuffEffects[player] = null;
                }

                Plugin.Log.LogDebug($"[석궁 단 한 발] {player.GetPlayerName()}의 캐릭터 따라다니는 버프 이펙트 정리 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogDebug($"[석궁 단 한 발] 캐릭터 따라다니는 이펙트 정리 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 캐릭터를 따라다니는 Buff_01 + statusailment_01_aura 이펙트 코루틴
        /// buff_01: 캐릭터를 따라다니면서 2초간 지속 후 사라짐 (GameObject + SetParent)
        /// statusailment_01_aura: 캐릭터 머리위에서 따라다니면서 전체 버프 시간 동안 유지
        /// </summary>
        private static IEnumerator FollowingBuffEffectCoroutine(Player player)
        {
            Plugin.Log.LogDebug($"[석궁 단 한 발] {player.GetPlayerName()} 캐릭터 따라다니는 이펙트 시작");

            GameObject buff01Effect = null;
            GameObject starAuraEffect = null;

            try
            {
                // === buff_01 이펙트: GameObject로 생성해서 캐릭터를 2초간 따라다니다 사라짐 ===
                var buff01Prefab = VFXManager.GetVFXPrefab("buff_01");
                if (buff01Prefab != null)
                {
                    Vector3 buff01Position = player.transform.position + Vector3.up * 0.5f;
                    buff01Effect = UnityEngine.Object.Instantiate(buff01Prefab, buff01Position, player.transform.rotation);

                    // 캐릭터를 따라다니도록 부모 설정
                    buff01Effect.transform.SetParent(player.transform, false);
                    buff01Effect.transform.localPosition = Vector3.up * 0.5f; // 캐릭터 중간 높이
                    buff01Effect.transform.localScale = Vector3.one * 0.8f;

                    Plugin.Log.LogDebug($"[석궁 단 한 발] buff_01 이펙트 생성 - 캐릭터 따라다니며 2초간 지속");

                    // 2초 후 buff_01 이펙트 제거
                    player.StartCoroutine(DestroyEffectAfterDelay(buff01Effect, 2f, "buff_01"));
                }

                // === 제자리 buff_01 효과 제거됨 - 캐릭터를 따라다니는 효과만 유지 ===
                Plugin.Log.LogDebug($"[석궁 단 한 발] 제자리 buff_01 효과는 제거됨 - 캐릭터 따라다니는 효과만 사용");

                // === statusailment_01_aura 이펙트: 캐릭터 머리위에서 따라다니면서 버프 시간만큼 유지 ===
                var starAuraPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                if (starAuraPrefab != null)
                {
                    Vector3 starPosition = player.transform.position + Vector3.up * 2.2f; // 머리 위
                    starAuraEffect = UnityEngine.Object.Instantiate(starAuraPrefab, starPosition, player.transform.rotation);

                    // 캐릭터를 따라다니도록 부모 설정
                    starAuraEffect.transform.SetParent(player.transform, false);
                    starAuraEffect.transform.localPosition = Vector3.up * 2.2f; // 머리 위
                    starAuraEffect.transform.localScale = Vector3.one * 0.6f;

                    float buffDuration = crossbowOneShotExpiry[player] - Time.time;
                    Plugin.Log.LogDebug($"[석궁 단 한 발] statusailment_01_aura 이펙트 생성 - 머리 위에서 따라다님 ({buffDuration:F1}초간 유지)");

                    // 버프 시간만큼 유지 후 제거
                    player.StartCoroutine(DestroyEffectAfterDelay(starAuraEffect, buffDuration, "statusailment_01_aura"));
                }

            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogDebug($"[석궁 단 한 발] 이펙트 생성 오류: {ex.Message}");
            }

            // 버프가 활성 상태인 동안 대기
            while (crossbowOneShotReady.ContainsKey(player) && crossbowOneShotReady[player] && Time.time < crossbowOneShotExpiry[player])
            {
                if (player == null || player.IsDead())
                {
                    // 플레이어 사망 시 모든 이펙트 정리
                    if (buff01Effect != null) UnityEngine.Object.Destroy(buff01Effect);
                    if (starAuraEffect != null) UnityEngine.Object.Destroy(starAuraEffect);
                    Plugin.Log.LogDebug($"[석궁 단 한 발] 플레이어 사망으로 이펙트 조기 정리");
                    break;
                }

                yield return new WaitForSeconds(1f);
            }

            Plugin.Log.LogDebug($"[석궁 단 한 발] 따라다니는 이펙트 코루틴 종료");
        }
        
        /// <summary>
        /// 지정된 시간 후 이펙트 제거하는 헬퍼 코루틴
        /// </summary>
        private static IEnumerator DestroyEffectAfterDelay(GameObject effect, float delay, string effectName)
        {
            if (effect == null) yield break;

            yield return new WaitForSeconds(delay);

            // ✅ 플레이어 사망 체크 추가 (대기 후)
            if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
            {
                if (effect != null)
                {
                    UnityEngine.Object.Destroy(effect);
                }
                Plugin.Log.LogDebug($"[석궁 단 한 발] 플레이어 사망으로 {effectName} 이펙트 조기 제거");
                yield break;
            }

            if (effect != null)
            {
                UnityEngine.Object.Destroy(effect);
                Plugin.Log.LogDebug($"[석궁 단 한 발] {effectName} 이펙트 제거됨 ({delay:F1}초 후)");
            }
        }

        private static IEnumerator CrossbowOneShotBuffDisplay(Player player)
        {
            while (crossbowOneShotReady[player] && Time.time < crossbowOneShotExpiry[player])
            {
                // ✅ 플레이어 사망 체크 추가 (루프 시작 시)
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogDebug("[석궁 단 한 발] 플레이어 사망으로 버프 표시 중단");
                    break;
                }

                float remainingTime = crossbowOneShotExpiry[player] - Time.time;
                if (remainingTime > 0)
                {
                    yield return new WaitForSeconds(5f);

                    // ✅ 플레이어 사망 체크 추가 (5초 대기 후)
                    if (player == null || player.IsDead())
                    {
                        Plugin.Log.LogDebug("[석궁 단 한 발] 플레이어 사망으로 버프 표시 중단 (대기 후)");
                        break;
                    }

                    if (crossbowOneShotReady[player])
                    {
                        DrawFloatingText(player, $"🎯 단 한 발 준비됨 ({remainingTime:F0}초)");
                    }
                }
                else
                {
                    break;
                }
            }
            
            // 버프가 만료될 때 캐릭터 따라다니는 이펙트도 정리
            StopFollowingBuffEffect(player);
            
            if (crossbowOneShotReady[player])
            {
                crossbowOneShotReady[player] = false;
                DrawFloatingText(player, "단 한 발 버프 만료");
            }
        }
        private static IEnumerator ShowCooldownDisplay(Player player, float cooldownTime, string skillName)
        {
            float elapsed = 0f;
            while (elapsed < cooldownTime)
            {
                // ✅ 플레이어 사망 체크 추가 (루프 시작 시)
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogInfo($"[{skillName}] 플레이어 사망으로 쿨타임 표시 중단");
                    yield break;
                }

                yield return new WaitForSeconds(2f);

                // ✅ 플레이어 사망 체크 추가 (대기 후)
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogInfo($"[{skillName}] 플레이어 사망으로 쿨타임 표시 중단 (대기 후)");
                    yield break;
                }

                elapsed += 2f;
                float remaining = cooldownTime - elapsed;
                if (remaining > 0)
                {
                    DrawFloatingText(player, $"{skillName} 쿨타임: {remaining:F1}초");
                }
            }
        }
        // === 분노의 망치는 MaceSkills.FuryHammer.cs로 완전 이동 ===

        /// <summary>
        /// 플레이어 사망 시 이중 시전 정리
        /// </summary>
        public static void CleanupStaffDualCastOnDeath(Player player)
        {
            try
            {
                // 코루틴 중단
                if (staffDualCastBuffCoroutines.ContainsKey(player))
                {
                    SkillTreeInputListener.Instance.StopCoroutine(staffDualCastBuffCoroutines[player]);
                    staffDualCastBuffCoroutines.Remove(player);
                }

                // 버프 상태 정리
                ClearStaffDualCastBuff(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[이중 시전] 정리 실패: {ex.Message}");
            }
        }

        private static IEnumerator ReleaseTauntAfterDelay(BaseAI ai, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (ai != null)
            {
                try
                {
                    var setAggravatedMethod = ai.GetType().GetMethod("SetAggravated");
                    if (setAggravatedMethod != null)
                    {
                        setAggravatedMethod.Invoke(ai, new object[] { false, BaseAI.AggravatedReason.Damage });
                    }
                    var setTargetMethod = ai.GetType().GetMethod("SetTarget");
                    if (setTargetMethod != null)
                    {
                        setTargetMethod.Invoke(ai, new object[] { null });
                    }
                    Plugin.Log.LogInfo($"[도발] 보스 도발 해제됨");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogWarning($"[도발] 보스 도발 해제 실패: {ex.Message}");
                }
            }
        }
        public static IEnumerator TauntCooldownDisplay(Player player)
        {
            float startTime = Time.time;

            // 쿨다운 시작 시 한 번만 버프 표시
            SkillBuffDisplay.Instance.ShowBuff(
                "taunt_cooldown",
                "도발 쿨타임",
                tauntCooldown,
                new Color(0.8f, 0.8f, 0.8f, 1f), // 회색
                "😤"
            );

            // 쿨타임 대기
            yield return new WaitForSeconds(tauntCooldown);

            // ✅ 플레이어 사망 체크 추가 (대기 후)
            if (player == null || player.IsDead())
            {
                Plugin.Log.LogInfo("[도발] 플레이어 사망으로 쿨타임 표시 중단");
                // 코루틴 딕셔너리에서 제거
                if (tauntCooldownCoroutine.ContainsKey(player))
                {
                    tauntCooldownCoroutine.Remove(player);
                }
                yield break;
            }

            // 쿨타임 완료 메시지 (한 번만)
            DrawFloatingText(player, "도발 준비 완료!", Color.green);

            // 코루틴 딕셔너리에서 제거
            if (tauntCooldownCoroutine.ContainsKey(player))
            {
                tauntCooldownCoroutine.Remove(player);
            }
        }
        
        public static void ApplyWarriorShoutEffect(Player player, int level)
        {
            // TODO: 실제 효과 구현 (파티원 전체 공격력+10%, 방어력+10% (10초간), 이펙트/사운드/플로팅 텍스트 등)
        }
        public static bool IsBossMonster(Character mob)
        {
            // TODO: 실제 보스 판정 로직 (mob.m_name, m_characterID 등 활용)
            return mob != null && mob.m_name != null && mob.m_name.Contains("보스"); // 실제 게임 로직에 맞게 수정 필요
        }
        
        private static void ShowTauntEffectOnMonster(Character monster)
        {
            try
            {
                // 간단한 이펙트 표시 (기본 파티클)
                Vector3 effectPosition = monster.transform.position + Vector3.up * 2f;
                
                // ZNetScene에서 기본 이펙트 사용
                var znet = ZNetScene.instance;
                if (znet != null)
                {
                    var effectNames = new string[] { "fx_creature_tamed", "fx_levelup", "fx_hit_campdamage" };
                    foreach (var effectName in effectNames)
                    {
                        var effectPrefab = znet.GetPrefab(effectName);
                        if (effectPrefab != null)
                        {
                            UnityEngine.Object.Instantiate(effectPrefab, effectPosition, Quaternion.identity);
                            Plugin.Log.LogInfo($"[도발 이펙트] {monster.name}에 {effectName} 이펙트 생성");
                            break;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[도발 이펙트] 이펙트 생성 중 오류: {ex.Message}");
            }
        }

        // === 창 투창 전문가 패시브 스킬 변수 및 구현 ===
        private static Dictionary<Player, float> spearThrowPassiveCooldown = new Dictionary<Player, float>();

        /// <summary>
        /// 투창 전문가 패시브 쿨타임 확인
        /// </summary>
        public static bool CanUseSpearThrowPassive(Player player)
        {
            if (player == null) return false;

            float now = Time.time;
            if (spearThrowPassiveCooldown.ContainsKey(player) && now < spearThrowPassiveCooldown[player])
            {
                // 쿨타임 중
                return false;
            }

            return true;
        }

        /// <summary>
        /// 투창 전문가 패시브 쿨타임 설정
        /// </summary>
        public static void SetSpearThrowPassiveCooldown(Player player)
        {
            if (player == null) return;

            float cooldown = SkillTreeConfig.SpearStep2ThrowCooldownValue;
            spearThrowPassiveCooldown[player] = Time.time + cooldown;

            Plugin.Log.LogDebug($"[투창 전문가] 쿨타임 {cooldown}초 설정");
        }

        // === 창 연공창 액티브 스킬 변수 및 구현 ===
        private static Dictionary<Player, float> spearEnhancedThrowCooldowns = new Dictionary<Player, float>();
        public static Dictionary<Player, float> spearEnhancedThrowBuffEndTime = new Dictionary<Player, float>();

        /// <summary>
        /// 창 연공창 액티브 스킬 (휠마우스 버튼)
        /// - 투창을 강화하여 창을 던지고 적과 주변 몬스터를 넉백시키고 피해 증가
        /// - 소모: 스태미나, 쿿타임 적용 (컨피그로 설정 가능)
        /// </summary>
        public static void HandleSpearActiveSkill(Player player)
        {
            try
            {
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogInfo("[연공창] 플레이어 없음 또는 사망");
                    return;
                }

                // 1. 스킬 보유 확인
                bool hasSkill = HasSkill("spear_Step5_combo");
                if (!hasSkill)
                {
                    DrawFloatingText(player, "연공창 스킬이 필요합니다", Color.red);
                    Plugin.Log.LogInfo("[연공창] 스킬 미보유");
                    return;
                }

                // 2. 창 착용 확인
                bool isUsingSpear = IsUsingSpear(player);
                if (!isUsingSpear)
                {
                    DrawFloatingText(player, "창을 착용해야 합니다", Color.red);
                    Plugin.Log.LogInfo("[연공창] 창 미착용");
                    return;
                }

                // 3. 쿨타임 확인
                float now = Time.time;
                if (spearEnhancedThrowCooldowns.ContainsKey(player) && now < spearEnhancedThrowCooldowns[player])
                {
                    float remaining = spearEnhancedThrowCooldowns[player] - now;
                    DrawFloatingText(player, $"쿨타임: {Mathf.CeilToInt(remaining)}초", Color.yellow);
                    Plugin.Log.LogInfo($"[연공창] 쿨타임 중 - 남은 시간: {remaining:F1}초");
                    return;
                }

                // 4. 스태미나 소모 확인
                float maxStamina = player.GetMaxStamina();
                float requiredStamina = maxStamina * (SkillTreeConfig.SpearStep2ThrowStaminaCostValue / 100f);
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, "스태미나 부족", Color.red);
                    Plugin.Log.LogInfo($"[연공창] 스태미나 부족 - 필요: {requiredStamina:F1}, 현재: {player.GetStamina():F1}");
                    return;
                }

                // 5. 스킬 발동
                ExecuteSpearEnhancedThrow(player);

                // 6. 쿨타임 및 스태미나 소모 적용
                spearEnhancedThrowCooldowns[player] = now + SkillTreeConfig.SpearStep2ThrowCooldownValue;
                player.UseStamina(requiredStamina);

                Plugin.Log.LogInfo("[연공창] 액티브 스킬 발동 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[연공창] 오류: {ex.Message}");
            }
        }


        /// <summary>
        /// 연공창 실제 실행 로직
        /// </summary>
        private static void ExecuteSpearEnhancedThrow(Player player)
        {
            try
            {
                Vector3 playerPosition = player.transform.position;

                // 1. 시작 VFX 효과
                PlayEnhancedThrowStartEffects(player, playerPosition);

                // 2. 연공창 버프 활성화 (Config 기반 지속시간 및 데미지)
                float buffDuration = SkillTreeConfig.SpearStep2ThrowBuffDurationValue;
                float damageBonus = SkillTreeConfig.SpearStep6ComboDamageValue;
                spearEnhancedThrowBuffEndTime[player] = Time.time + buffDuration;

                // 3. 실제 창 던지기 실행 (세컨드 어택)
                player.StartAttack(null, true);

                // 4. 버프 표시
                SkillBuffDisplay.Instance.ShowBuff(
                    "spear_enhanced_throw",
                    "연공창",
                    buffDuration,
                    new Color(1f, 0.8f, 0.2f, 1f), // 골드색
                    "🏹"
                );

                // 5. 플로팅 텍스트
                DrawFloatingText(player, $"[연공창] 강화된 투창! +{damageBonus}%", new Color(1f, 0.8f, 0.2f, 1f));

                Plugin.Log.LogInfo($"[연공창] 강화된 투창 발사 완료 - 데미지 +{damageBonus}%, 지속시간 {buffDuration}초");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[연공창 실행] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 연공창 시작 VFX 효과 (사용자 VFX 제거)
        /// </summary>
        private static void PlayEnhancedThrowStartEffects(Player player, Vector3 position)
        {
            // 사용자 VFX/사운드 제거 - 몬스터 적중 시에만 효과 표시
            Plugin.Log.LogInfo("[연공창 VFX] 시작 효과 생략 (사용자 VFX 제거)");
        }

        /// <summary>
        /// 강화된 창 투사체 생성
        /// </summary>
        private static void CreateEnhancedSpearProjectile(Player player, Vector3 startPosition, Vector3 direction)
        {
            try
            {
                // 투사체 생성 (기본 창 기반)
                GameObject projectile = new GameObject("EnhancedSpearProjectile");
                projectile.transform.position = startPosition + Vector3.up * 1.5f;
                projectile.transform.rotation = Quaternion.LookRotation(direction);

                // Rigidbody 추가
                var rb = projectile.AddComponent<Rigidbody>();
                rb.mass = 0.1f;
                rb.drag = 0.1f;
                rb.velocity = direction * 25f; // 빠른 속도

                // 기본 창 던지기 모션 사용 (궤적 효과 제거)

                // 조명 효과
                var light = projectile.AddComponent<Light>();
                light.color = new Color(1f, 0.8f, 0.3f, 1f);
                light.intensity = 2f;
                light.range = 8f;

                // 충돌 처리 컴포넌트 추가
                var collisionHandler = projectile.AddComponent<EnhancedSpearProjectileHandler>();
                collisionHandler.Initialize(player);

                // 5초 후 자동 파괴
                UnityEngine.Object.Destroy(projectile, 5f);

                Plugin.Log.LogInfo("[연공창] 강화된 투사체 생성 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[연공창] 투사체 생성 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 이중시전 마법 발사체 충돌 처리 컴포넌트
        /// </summary>
        public class DoubleCastProjectileHandler : MonoBehaviour
        {
            private Player caster;
            private ItemDrop.ItemData weapon;
            private float damagePercent;
            private int projectileIndex;
            private bool hasHit = false;

            public void Initialize(Player player, ItemDrop.ItemData weaponData, float dmgPercent, int index)
            {
                caster = player;
                weapon = weaponData;
                damagePercent = dmgPercent;
                projectileIndex = index;
            }

            private void OnTriggerEnter(Collider other)
            {
                if (hasHit || caster == null) return;

                var character = other.GetComponent<Character>();
                if (character != null && character != caster && !character.IsPlayer())
                {
                    hasHit = true;
                    HandleProjectileImpact(character);
                }
            }

            private void HandleProjectileImpact(Character target)
            {
                try
                {
                    Vector3 impactPoint = transform.position;

                    // 충돌 효과
                    PlayMagicImpactEffects(impactPoint);

                    // 데미지 적용
                    ApplyMagicDamage(target, damagePercent);

                    // 발사체 파괴
                    Destroy(gameObject);

                    Plugin.Log.LogInfo($"[이중 시전] 발사체 {projectileIndex}번이 {target.name}에 충돌");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[이중 시전] 발사체 충돌 처리 오류: {ex.Message}");
                }
            }

            private void PlayMagicImpactEffects(Vector3 impactPoint)
            {
                try
                {
                    // 마법 충돌 효과
                    var magicEffect = ZNetScene.instance?.GetPrefab("fx_icestaffprojectile_hit");
                    if (magicEffect != null)
                    {
                        var effectObj = UnityEngine.Object.Instantiate(magicEffect, impactPoint, Quaternion.identity);
                        // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                        var znetView = effectObj?.GetComponent<ZNetView>();
                        if (znetView != null) UnityEngine.Object.DestroyImmediate(znetView);
                        if (effectObj != null) UnityEngine.Object.Destroy(effectObj, 3f);
                    }

                    // 마법 사운드
                    var magicSound = ZNetScene.instance?.GetPrefab("sfx_staff_fireball_hit");
                    if (magicSound != null)
                    {
                        var soundObj = UnityEngine.Object.Instantiate(magicSound, impactPoint, Quaternion.identity);
                        // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                        var znetView2 = soundObj?.GetComponent<ZNetView>();
                        if (znetView2 != null) UnityEngine.Object.DestroyImmediate(znetView2);
                        if (soundObj != null) UnityEngine.Object.Destroy(soundObj, 3f);
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[이중 시전] 충돌 효과 재생 오류: {ex.Message}");
                }
            }

            private void ApplyMagicDamage(Character target, float dmgPercent)
            {
                try
                {
                    if (weapon == null) return;

                    // 지팡이/완드 기본 데미지 계산
                    var baseDamage = weapon.GetDamage();

                    // 70% 데미지 적용
                    var projectileDamage = new HitData.DamageTypes();
                    projectileDamage.m_fire = baseDamage.m_fire * dmgPercent;
                    projectileDamage.m_frost = baseDamage.m_frost * dmgPercent;
                    projectileDamage.m_lightning = baseDamage.m_lightning * dmgPercent;
                    projectileDamage.m_poison = baseDamage.m_poison * dmgPercent;
                    projectileDamage.m_spirit = baseDamage.m_spirit * dmgPercent;

                    // HitData 생성
                    var hitData = new HitData();
                    hitData.m_damage = projectileDamage;
                    hitData.m_point = target.transform.position;
                    hitData.m_dir = (target.transform.position - caster.transform.position).normalized;
                    hitData.m_attacker = caster.GetZDOID();
                    hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;

                    // 데미지 적용
                    target.Damage(hitData);

                    Plugin.Log.LogInfo($"[이중 시전] {target.name}에게 마법 데미지 적용 완료 ({dmgPercent * 100}%)");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[이중 시전] 데미지 적용 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 강화된 창 투사체 충돌 처리 컴포넌트
        /// </summary>
        public class EnhancedSpearProjectileHandler : MonoBehaviour
        {
            private Player thrower;
            private bool hasHit = false;

            public void Initialize(Player player)
            {
                thrower = player;
            }

            private void OnTriggerEnter(Collider other)
            {
                if (hasHit) return;

                var character = other.GetComponent<Character>();
                if (character != null && character != thrower && !character.IsPlayer())
                {
                    hasHit = true;
                    HandleSpearImpact(character);
                }
            }

            private void HandleSpearImpact(Character target)
            {
                try
                {
                    Vector3 impactPoint = transform.position;

                    // 1. 충돌 VFX 효과 (몬스터 위치에)
                    PlaySpearImpactEffects(target);

                    // 2. 주 타겟에게 강화된 피해
                    ApplyEnhancedSpearDamage(target, SkillTreeConfig.SpearStep2ThrowDamageValue / 100f);

                    // 3. 주변 범위 넉백 및 데미지
                    ApplyAreaKnockbackAndDamage(impactPoint, target);

                    // 4. 투사체 파괴
                    Destroy(gameObject);

                    Plugin.Log.LogInfo($"[연공창] {target.name}에 충돌 처리 완료");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[연공창] 충돌 처리 오류: {ex.Message}");
                }
            }

            private void PlaySpearImpactEffects(Character target)
            {
                try
                {
                    // 몬스터 위치에 타격 불꽃 효과 (vfx_HitSparks)
                    Vector3 targetPosition = target.transform.position + Vector3.up * 1f;

                    SimpleVFX.Play("vfx_HitSparks", targetPosition, 1.5f);

                    Plugin.Log.LogInfo($"[연공창] {target.name}에게 타격 불꽃 효과 재생 (vfx_HitSparks)");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[연공창] 충돌 VFX 오류: {ex.Message}");
                }
            }

            private void ApplyEnhancedSpearDamage(Character target, float damageMultiplier)
            {
                try
                {
                    if (thrower?.GetCurrentWeapon() == null) return;

                    // 기본 창 데미지 계산
                    var weapon = thrower.GetCurrentWeapon();
                    var baseDamage = weapon.GetDamage();

                    // Config 기반 피해 적용 (damageMultiplier는 이미 Config에서 전달됨)
                    var enhancedDamage = new HitData.DamageTypes();
                    enhancedDamage.m_pierce = baseDamage.m_pierce * damageMultiplier;
                    enhancedDamage.m_slash = baseDamage.m_slash * damageMultiplier;
                    enhancedDamage.m_blunt = baseDamage.m_blunt * damageMultiplier;

                    // 피해 적용
                    var hitData = new HitData();
                    hitData.m_damage = enhancedDamage;
                    hitData.m_point = target.transform.position;
                    hitData.m_dir = (target.transform.position - thrower.transform.position).normalized;
                    hitData.m_attacker = thrower.GetZDOID();
                    hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;

                    target.Damage(hitData);

                    Plugin.Log.LogInfo($"[연공창] {target.name}에게 강화된 피해 적용 완료 (배율: {damageMultiplier})");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[연공창] 피해 적용 오류: {ex.Message}");
                }
            }

            private void ApplyAreaKnockbackAndDamage(Vector3 center, Character mainTarget)
            {
                try
                {
                    float radius = SkillTreeConfig.SpearStep2ThrowRangeValue; // 설정된 범위
                    var colliders = Physics.OverlapSphere(center, radius);

                    foreach (var collider in colliders)
                    {
                        var character = collider.GetComponent<Character>();
                        if (character != null && character != thrower && !character.IsPlayer() && character != mainTarget)
                        {
                            // 넉백 적용 (대체 방법 사용)
                            Vector3 knockbackDir = (character.transform.position - center).normalized;
                            character.SetLookDir(knockbackDir * -1f, 0.2f);
                            
                            // 넉백 효과를 위한 HitData 생성
                            var knockbackHit = new HitData();
                            knockbackHit.m_damage.m_blunt = 1f; // 최소 피해
                            knockbackHit.m_point = character.transform.position;
                            knockbackHit.m_dir = knockbackDir;
                            knockbackHit.m_pushForce = 100f; // 넉백 힘
                            knockbackHit.m_attacker = thrower.GetZDOID();
                            
                            character.Damage(knockbackHit);

                            // 50% 피해 적용 (주변 몬스터)
                            ApplyEnhancedSpearDamage(character, 0.5f);
                        }
                    }

                    Plugin.Log.LogInfo($"[연공창] 범위 넉백 완료 - 반경: {radius}m");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[연공창] 범위 넉백 오류: {ex.Message}");
                }
            }
        }
        
        // === 검 Sword Slash 스킬은 Sword_Skill.cs에서 관리됨 ===
        
        /// <summary>
        /// 석궁 "단 한 발" 스킬 사용 확인 및 효과 제거
        /// 몬스터 적중 시 호출되어 30초 카운트를 제거하고 스킬을 소모함
        /// </summary>
        public static bool CheckAndConsumeCrossbowOneShot(Player player, Character target)
        {
            try
            {
                // "단 한 발" 버프가 활성화되어 있는지 확인
                if (!crossbowOneShotReady.ContainsKey(player) || !crossbowOneShotReady[player])
                {
                    return false; // 버프가 없으면 false 반환
                }

                // 플레이어 자신을 공격한 경우는 무시
                if (target == player)
                {
                    return false;
                }

                // 몬스터나 적 캐릭터인지 확인
                bool isValidTarget = target.IsMonsterFaction(Time.time) ||
                                   (target.IsPlayer() && target != player) ||
                                   target.name.Contains("Deer") || target.name.Contains("Boar") ||
                                   target.name.Contains("Neck") || target.name.Contains("Greyling");

                if (isValidTarget)
                {
                    // === 넉백 효과 적용 (균형 조준과 동일한 구현, 100% 확률) ===
                    try
                    {
                        Vector3 knockbackDirection = (target.transform.position - player.transform.position).normalized;
                        float knockbackDistance = Crossbow_Config.CrossbowOneShotKnockbackValue;

                        // 스태거 효과
                        target.Stagger(knockbackDirection);

                        // 물리 기반 넉백
                        var rigidbody = target.GetComponent<Rigidbody>();
                        if (rigidbody != null && !rigidbody.isKinematic)
                        {
                            rigidbody.AddForce(knockbackDirection * knockbackDistance * 2f, ForceMode.Impulse);
                        }

                        Plugin.Log.LogDebug($"[석궁 단 한 발] 넉백 효과 적용 - 거리: {knockbackDistance}m");
                    }
                    catch (Exception knockbackEx)
                    {
                        Plugin.Log.LogWarning($"[석궁 단 한 발] 넉백 효과 적용 실패: {knockbackEx.Message}");
                    }

                    // "단 한 발" 효과 제거
                    crossbowOneShotReady[player] = false;

                    // 실행 중인 코루틴 정지
                    if (crossbowOneShotCoroutine.ContainsKey(player) && crossbowOneShotCoroutine[player] != null)
                    {
                        player.StopCoroutine(crossbowOneShotCoroutine[player]);
                        crossbowOneShotCoroutine[player] = null;
                    }

                    // 캐릭터를 따라다니는 이펙트도 정리
                    StopFollowingBuffEffect(player);

                    // 몬스터 적중 시 explosion 이펙트 재생 (네트워크 동기화)
                    try
                    {
                        SimpleVFX.Play("fx_siegebomb_explosion", target.transform.position, 3f);
                    }
                    catch (Exception vfxEx)
                    {
                        Plugin.Log.LogWarning($"[석궁 단 한 발] VFX 재생 실패: {vfxEx.Message}");
                    }

                    // 스킬 사용 완료 메시지 표시
                    DrawFloatingText(player, "🎯 단 한 발 발동!");

                    Plugin.Log.LogDebug($"[석궁 단 한 발] 스킬 사용 완료 - 타겟: {target.name}");

                    return true; // 버프 사용됨
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[석궁 단 한 발] 스킬 소모 처리 오류: {ex.Message}");
                return false;
            }
        }

    }

    /// <summary>
    /// 지팡이 공격 시 이중 시전 버프 자동 발동을 위한 Harmony 패치
    /// 아처 멀티샷과 동일한 Attack.FireProjectileBurst 패치 패턴 사용
    /// </summary>
    [HarmonyPatch(typeof(Attack), "FireProjectileBurst")]
    [HarmonyPriority(Priority.High)] // 아처 패치보다 먼저 실행
    public static class StaffDualCast_Attack_FireProjectileBurst_Patch
    {
        [HarmonyPrefix]
        private static bool Prefix(Attack __instance)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null)
                {
                    return true;
                }

                // 현재 지팡이/완드 착용 확인 (최적화된 감지 시스템 사용)
                if (!StaffEquipmentDetector.IsWieldingStaffOrWand(player))
                {
                    return true; // 지팡이가 아니면 원래 메서드 실행
                }

                var currentWeapon = player.GetCurrentWeapon();

                // 이중 시전 버프가 활성화된 상태인지 확인
                bool isReady = SkillEffect.IsStaffDualCastReady(player);

                if (!isReady)
                {
                    return true; // 버프 상태가 아니면 원래 메서드 실행
                }

                // 공격 방향 계산
                Vector3 attackDir = player.GetLookDir();

                // 기존 지팡이 공격은 그대로 실행하고, 추가로 이중 시전 발동
                // 원래 메서드가 실행된 후 추가 발사체를 생성하기 위해 코루틴 사용
                SkillTreeInputListener.Instance.StartCoroutine(DelayedDualCastExecution(player, currentWeapon, attackDir));

                return true; // 원래 메서드 실행 (기존 지팡이 공격)
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이중 시전] Attack.FireProjectileBurst 패치 오류: {ex.Message}\n스택 트레이스: {ex.StackTrace}");
                return true;
            }
        }

        /// <summary>
        /// 원래 지팡이 공격 후 약간의 지연을 두고 추가 발사체 생성
        /// </summary>
        private static IEnumerator DelayedDualCastExecution(Player player, ItemDrop.ItemData weapon, Vector3 attackDir)
        {
            Plugin.Log.LogInfo($"[이중 시전] 0.5초 지연 시작 - 플레이어: {player?.GetPlayerName()}, 무기: {weapon?.m_shared?.m_name}");
            yield return new WaitForSeconds(0.5f); // 0.5초 지연으로 변경

            // 플레이어 사망 체크
            if (player == null || player.IsDead())
            {
                Plugin.Log.LogInfo("[이중 시전] 플레이어 사망으로 코루틴 중단");
                yield break;
            }

            if (player != null && SkillEffect.IsStaffDualCastReady(player))
            {
                Plugin.Log.LogInfo($"[이중 시전] 지연 완료, 추가 발사체 생성 시작");
                SkillEffect.PerformStaffDualCastAttack(player, weapon, attackDir);
                Plugin.Log.LogInfo($"[이중 시전] 지연된 추가 발사체 생성 완료");
            }
            else
            {
                Plugin.Log.LogWarning($"[이중 시전] 지연 후 실행 실패 - 플레이어: {player != null}, 버프상태: {SkillEffect.IsStaffDualCastReady(player)}");
            }
        }
    }

    // === 수호자의 진심 스킬 구현은 Mace_Active.cs로 이동 ===
    // 모든 GuardianHeart 관련 코드는 Mace_Active.cs에 있습니다.

    public static partial class SkillEffect
    {
        // ===== 발구르기 스킬 시스템 =====

        /// <summary>
        /// 발구르기 스킬 실행 (자동 발동 패시브)
        /// </summary>
        /// <param name="player">플레이어</param>
        public static void ExecuteStompSkill(Player player)
        {
            try
            {
                if (player == null || player.IsDead())
                {
                    return;
                }

                var manager = SkillTreeManager.Instance;
                if (manager == null)
                {
                    return;
                }

                // 스킬 보유 여부 확인
                if (!HasSkill("defense_Step4_instant"))
                {
                    return;
                }

                // Config 값 로드
                float radius = Defense_Config.StompRadiusValue;
                float knockback = Defense_Config.StompKnockbackValue;
                float vfxDuration = Defense_Config.StompVFXDurationValue;

                Vector3 playerPos = player.transform.position;

                // 범위 내 적 검색 및 넉백 적용
                Collider[] hitColliders = Physics.OverlapSphere(playerPos, radius);
                List<Character> hitEnemies = new List<Character>();

                foreach (var collider in hitColliders)
                {
                    if (collider == null) continue;

                    Character enemy = collider.GetComponentInParent<Character>();
                    if (enemy != null && enemy != player && !enemy.IsDead() &&
                        BaseAI.IsEnemy(player, enemy) && !hitEnemies.Contains(enemy))
                    {
                        // 넉백 방향 계산
                        Vector3 direction = (enemy.transform.position - playerPos).normalized;
                        direction.y = 0.5f; // 위로 띄우기

                        // 강력한 넉백 적용 (HitData 방식)
                        HitData pushHit = new HitData();
                        pushHit.m_point = enemy.transform.position;
                        pushHit.m_dir = direction;
                        pushHit.m_pushForce = knockback;
                        pushHit.m_damage.m_damage = 0.01f; // 극소 데미지 (넉백 트리거)
                        pushHit.m_attacker = player.GetZDOID();
                        pushHit.SetAttacker(player);

                        enemy.Damage(pushHit);
                        enemy.Stagger(direction);

                        hitEnemies.Add(enemy);

                        // 넉백된 적 머리 위에 statusailment_01_aura VFX
                        try
                        {
                            Vector3 enemyHeadPos = enemy.GetHeadPoint();
                            SimpleVFX.Play("statusailment_01_aura", enemyHeadPos, 2f);
                        }
                        catch
                        {
                            // VFX 재생 실패 시 무시 (적이 사라진 경우)
                        }
                    }
                }

                int enemyCount = hitEnemies.Count;

                // VFX 효과 재생 (1초 후 자동 삭제)
                PlayStompVFX(playerPos, vfxDuration);

                // SFX 효과 재생 (2회, 0.1초 간격)
                player.StartCoroutine(PlayStompSFX(playerPos));

                // 플레이어 메시지
                if (enemyCount > 0)
                {
                    player.Message(MessageHud.MessageType.Center, $"발구르기! ({enemyCount}명 밀어냄)");
                }

                Plugin.Log.LogInfo($"[발구르기] 실행 완료 - 범위: {radius}m, 넉백: {knockback}, 적: {enemyCount}명");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[발구르기] 실행 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 발구르기 VFX 효과 재생 (1초 후 자동 삭제)
        /// </summary>
        private static void PlayStompVFX(Vector3 position, float duration)
        {
            try
            {
                // shine_blue VFX 재생 (사운드 없음)
                SimpleVFX.Play("shine_blue", position, duration);
                Plugin.Log.LogDebug($"[발구르기 VFX] shine_blue 재생 완료 ({duration}초 지속)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[발구르기 VFX] 재생 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 발구르기 SFX 효과 재생 (2회, 0.1초 간격)
        /// </summary>
        private static IEnumerator PlayStompSFX(Vector3 position)
        {
            // 첫 번째 재생
            var sfxPrefab = ZNetScene.instance?.GetPrefab("sfx_metal_blocked");
            if (sfxPrefab != null)
            {
                var sfxObj1 = UnityEngine.Object.Instantiate(sfxPrefab, position, Quaternion.identity);
                // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                var znetView1 = sfxObj1?.GetComponent<ZNetView>();
                if (znetView1 != null) UnityEngine.Object.DestroyImmediate(znetView1);
                if (sfxObj1 != null) UnityEngine.Object.Destroy(sfxObj1, 3f);
                Plugin.Log.LogDebug("[발구르기 SFX] 1차 sfx_metal_blocked 재생");
            }

            // 0.1초 대기
            yield return new WaitForSeconds(0.1f);

            // 두 번째 재생
            if (sfxPrefab != null)
            {
                var sfxObj2 = UnityEngine.Object.Instantiate(sfxPrefab, position, Quaternion.identity);
                // ✅ ZNetView 즉시 제거 (무한 로딩 방지)
                var znetView2 = sfxObj2?.GetComponent<ZNetView>();
                if (znetView2 != null) UnityEngine.Object.DestroyImmediate(znetView2);
                if (sfxObj2 != null) UnityEngine.Object.Destroy(sfxObj2, 3f);
                Plugin.Log.LogDebug("[발구르기 SFX] 2차 sfx_metal_blocked 재생");
            }
        }

        // ===== 액티브 스킬 사망 시 정리 메서드들 =====
        // 수호자의 진심 정리 메서드는 Mace_Active.cs에 있습니다.

        /// <summary>
        /// 석궁 단 한 발 정리 메서드 (플레이어 사망 시 호출)
        /// Coroutine, Dictionary 정리
        /// </summary>
        public static void CleanupCrossbowOneShotOnDeath(Player player)
        {
            try
            {
                // Coroutine 중지
                if (crossbowOneShotCoroutine.ContainsKey(player))
                {
                    if (crossbowOneShotCoroutine[player] != null)
                    {
                        try
                        {
                            player.StopCoroutine(crossbowOneShotCoroutine[player]);
                        }
                        catch { }
                    }
                    crossbowOneShotCoroutine.Remove(player);
                }

                // 상태 초기화
                crossbowOneShotReady.Remove(player);
                crossbowOneShotCooldown.Remove(player);
                crossbowOneShotExpiry.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[석궁 단 한 발] 정리 실패: {ex.Message}");
            }
        }

        // === PlayLocalVFX/PlayLocalSound 제거 (VFX 규칙 위반) ===
        // 대체: VFXManager.PlayVFXMultiplayer 사용

        /// <summary>
        /// 도발 정리 메서드 (플레이어 사망 시 호출)
        /// Coroutine, Dictionary 정리
        /// </summary>
        public static void CleanupTauntOnDeath(Player player)
        {
            try
            {
                // Coroutine 중지
                if (tauntCooldownCoroutine.ContainsKey(player))
                {
                    if (tauntCooldownCoroutine[player] != null)
                    {
                        try
                        {
                            player.StopCoroutine(tauntCooldownCoroutine[player]);
                        }
                        catch { }
                    }
                    tauntCooldownCoroutine.Remove(player);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[도발] 정리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 창 강화 투척 정리 메서드 (플레이어 사망 시 호출)
        /// Dictionary 정리
        /// </summary>
        public static void CleanupSpearEnhancedThrowOnDeath(Player player)
        {
            try
            {
                spearEnhancedThrowCooldowns.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[창 강화 투척] 정리 실패: {ex.Message}");
            }
        }
    }
}
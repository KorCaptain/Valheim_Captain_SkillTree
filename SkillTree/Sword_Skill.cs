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
    /// <summary>
    /// 검 스킬 전용 로직 시스템
    /// Sword Slash 액티브 스킬 및 검 관련 모든 스킬 구현
    /// </summary>
    public static class Sword_Skill
    {
        // === Sword Slash 액티브 스킬 관련 변수 ===
        private static Dictionary<Player, float> swordSlashCooldowns = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> swordSlashActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> swordSlashEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> swordSlashCoroutines = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, int> swordSlashAttackCount = new Dictionary<Player, int>();
        
        // === AnimationSpeedManager 연동을 위한 추가 변수 ===
        private static Dictionary<Player, List<float>> swordSlashAttackStartTime = new Dictionary<Player, List<float>>();
        
        // === 공격 속도 배율 설정 - 모든 공격 20배로 통일 ===
        private static readonly float[] attackSpeedMultipliers = { 20.0f, 20.0f, 20.0f }; // 1차, 2차, 3차 모든 공격 20배 속도
        
        /// <summary>
        /// 무기 데이터 백업을 위한 구조체
        /// </summary>
        private class WeaponBackupData
        {
            public ItemDrop.ItemData weapon;
            public float originalAttackSpeed;
            public float originalAnimationSpeed;
        }
        
        // === 민첩성 조작 중복 실행 방지 ===
        private static Dictionary<Player, bool> agilityBeingModified = new Dictionary<Player, bool>();
        private static Dictionary<Player, float> originalAgilityLevels = new Dictionary<Player, float>();
        
        // === 초고속 공격을 위한 민첩성 설정 ===
        private static readonly float highSpeedAgilityBoost = 15000f; // 15000 민첩성 증가 (확실한 초고속)

        // === 패링 스택 시스템 (신규) ===
        private static Dictionary<Player, int> parryStacks = new Dictionary<Player, int>();
        private static Dictionary<Player, float> parryStackExpiry = new Dictionary<Player, float>();

        /// <summary>
        /// 패링 성공 시 호출 (방어 전환 스킬 연계)
        /// Plugin.cs의 BlockAttack 패치에서 호출됨
        /// 방패 착용 + 방어 전환 스킬 보유 시에만 스택 축적
        /// </summary>
        public static void OnParrySuccess(Player player)
        {
            if (player == null || !IsUsingSword(player)) return;

            // 방어 전환 스킬 보유 확인
            if (!SkillEffect.HasSkill("sword_step5_defswitch"))
            {
                return;
            }

            // 방패 착용 확인
            if (!HasShield(player))
            {
                return;
            }

            // 현재 스택 가져오기 (만료 확인 포함)
            int currentStacks = GetParryStacks(player);
            int maxStacks = Sword_Config.MaxParryStacksValue; // 3

            // 스택 증가 (최대값 제한)
            int newStacks = Math.Min(currentStacks + 1, maxStacks);
            parryStacks[player] = newStacks;

            // 버프 타이머 갱신 (항상 15초로 리셋)
            float buffDuration = Sword_Config.ParryStackBuffDurationValue;
            parryStackExpiry[player] = Time.time + buffDuration;

            // 스택 보너스 계산
            float damageBonus = Sword_Config.CalculateParryDamageBonus(newStacks);

            // 화면 중앙 메시지 표시
            string stackText = newStacks >= maxStacks
                ? $"⚔️ 패링 MAX! ({newStacks}스택) +{damageBonus}% [{buffDuration}초]"
                : $"⚔️ 패링 성공! ({newStacks}/{maxStacks}스택) +{damageBonus}%";

            if (MessageHud.instance != null)
            {
                MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, stackText);
            }

            // 3스택 최초 달성 시 특수 효과
            if (newStacks >= maxStacks && currentStacks < maxStacks)
            {
                Vector3 playerPos = player.transform.position + Vector3.up * 1f;
                SimpleVFX.Play("fx_eikthyr_stomp", playerPos, 0.8f);
            }
        }

        /// <summary>
        /// 플레이어가 방패를 착용 중인지 확인
        /// </summary>
        public static bool HasShield(Player player)
        {
            if (player == null) return false;

            try
            {
                var leftItemField = typeof(Humanoid).GetField("m_leftItem",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var leftItem = leftItemField?.GetValue(player) as ItemDrop.ItemData;

                return leftItem != null && leftItem.IsWeapon() &&
                       leftItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 현재 패링 스택 수 반환 (만료 확인 포함)
        /// </summary>
        public static int GetParryStacks(Player player)
        {
            if (player == null) return 0;

            // 스택 존재 확인
            if (!parryStacks.ContainsKey(player))
            {
                return 0;
            }

            // 만료 확인
            if (parryStackExpiry.TryGetValue(player, out float expiry) && Time.time > expiry)
            {
                // 스택 만료 - 초기화
                parryStacks[player] = 0;
                return 0;
            }

            return parryStacks[player];
        }

        /// <summary>
        /// 패링 스택에 따른 공격력 보너스 반환 (%)
        /// 1스택: 55%, 2스택: 120%, 3스택: 200%
        /// </summary>
        public static float GetParryDamageBonus(Player player)
        {
            // 방어 전환 스킬 + 방패 착용 확인
            if (!SkillEffect.HasSkill("sword_step5_defswitch")) return 0f;
            if (!HasShield(player)) return 0f;

            int stacks = GetParryStacks(player);
            return Sword_Config.CalculateParryDamageBonus(stacks);
        }

        /// <summary>
        /// 패링 스택 버프가 활성화 상태인지 확인
        /// </summary>
        public static bool IsParryBuffActive(Player player)
        {
            if (player == null) return false;
            return GetParryStacks(player) > 0;
        }

        /// <summary>
        /// 플레이어가 검을 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 Swords 스킬 타입
        /// 2순위: 프리팹 이름에 "Sword", "sword", "Blade", "blade" 포함
        /// 3순위: 무기 이름에 "검", "sword", "blade" 포함
        /// </summary>
        public static bool IsUsingSword(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 Swords 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
            {
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Sword") || prefabName.Contains("sword") || 
                prefabName.Contains("Blade") || prefabName.Contains("blade"))
            {
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("검") || weaponName.Contains("sword") || weaponName.Contains("blade"))
            {
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Sword Slash G키 액티브 스킬 활성화
        /// - 0.2초 간격으로 3회 연속 공격 (1회 공격력 80%)
        /// - 지속시간: 1초 | 소모: 스태미나 25 | 쿨타임: 35초
        /// </summary>
        public static void ActivateSwordSlash(Player player)
        {
            try
            {
                if (player == null || player.IsDead())
                {
                    return;
                }

                // 1. 스킬 보유 확인
                bool hasSkill = SkillEffect.HasSkill("sword_step5_finalcut") || SkillEffect.HasSkill("sword_slash");
                if (!hasSkill)
                {
                    SkillEffect.DrawFloatingText(player, "Sword Slash 스킬이 필요합니다", Color.red);
                    return;
                }

                // 2. 검 착용 확인
                if (!IsUsingSword(player))
                {
                    SkillEffect.DrawFloatingText(player, "검을 착용해야 합니다", Color.red);
                    return;
                }

                // 3. 쿨타임 확인
                float now = Time.time;
                if (swordSlashCooldowns.ContainsKey(player) && now < swordSlashCooldowns[player])
                {
                    float remaining = swordSlashCooldowns[player] - now;
                    SkillEffect.DrawFloatingText(player, $"쿨타임: {Mathf.CeilToInt(remaining)}초", Color.yellow);
                    return;
                }

                // 4. 스태미나 소모 확인
                float requiredStamina = Sword_Config.SwordSlashStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    SkillEffect.DrawFloatingText(player, "스태미나 부족", Color.red);
                    return;
                }

                // 5. 이미 다른 Sword Slash가 실행 중인지 확인
                if (swordSlashActive.ContainsKey(player) && swordSlashActive[player])
                {
                    SkillEffect.DrawFloatingText(player, "Sword Slash 실행 중", Color.yellow);
                    return;
                }

                // 6. 스킬 활성화
                float duration = Sword_Config.SwordSlashDurationValue;
                float cooldown = Sword_Config.SwordSlashCooldownValue;
                
                swordSlashActive[player] = true;
                swordSlashEndTime[player] = now + duration;
                swordSlashCooldowns[player] = now + cooldown;
                swordSlashAttackCount[player] = 0;

                // 7. 스태미나 소모
                player.UseStamina(requiredStamina);

                // 8. 액티브 스킬: 풍부한 VFX/SFX 적용
                SkillEffect.PlaySkillEffect(player, "sword_slash");
                SkillEffect.DrawFloatingText(player, $"⚔️ Sword Slash 발동! ({Sword_Config.SwordSlashAttackCountValue}회 연속공격)", Color.red);

                // 9. 연속 공격 코루틴 시작
                if (swordSlashCoroutines.ContainsKey(player) && swordSlashCoroutines[player] != null)
                {
                    player.StopCoroutine(swordSlashCoroutines[player]);
                }
                
                // 코루틴 시작을 try-catch 밖에서 실행
                var coroutine = ExecuteSwordSlashCombo(player);
                swordSlashCoroutines[player] = player.StartCoroutine(coroutine);

            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 스킬 활성화 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sword Slash 연속 공격 실행 코루틴 - FuryHammer 패턴 기반 재설계
        /// 민첩성 조작 제거, 직접 데미지 적용, 패링 스택 피니셔 시스템
        /// </summary>
        private static IEnumerator ExecuteSwordSlashCombo(Player player)
        {
            // 플레이어 사망 체크
            if (player == null || player.IsDead())
            {
                yield break;
            }

            var skillData = Sword_Config.GetSwordSlashData();
            int attackCount = skillData.attackCount;
            float attackInterval = skillData.attackInterval;
            float damageRatio = skillData.damageRatio / 100f; // 80% = 0.8

            // 무기 확인
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                if (swordSlashActive.ContainsKey(player))
                    swordSlashActive[player] = false;
                yield break;
            }

            // 무기 기본 데미지
            var weaponDamage = weapon.GetDamage();
            float baseSlash = weaponDamage.m_slash;
            float baseBlunt = weaponDamage.m_blunt;
            float basePierce = weaponDamage.m_pierce;

            // VFX 고정 위치
            Vector3 fixedVfxOffset = player.transform.forward * 2f;
            Vector3 fixedVfxPosition = player.transform.position + fixedVfxOffset + Vector3.up * 1f;

            int totalHits = 0;

            // 공격 모션 시작
            player.StartAttack(null, false);
            yield return new WaitForSeconds(0.3f);

            // 사망 체크
            if (player == null || player.IsDead())
            {
                if (swordSlashActive.ContainsKey(player))
                    swordSlashActive[player] = false;
                yield break;
            }

            // 연속 공격 루프
            for (int i = 0; i < attackCount; i++)
            {
                // 매 공격마다 사망 체크
                if (player == null || player.IsDead() || !swordSlashActive.ContainsKey(player) || !swordSlashActive[player])
                {
                    break;
                }

                // 검 착용 상태 확인
                if (!IsUsingSword(player))
                {
                    SkillEffect.DrawFloatingText(player, "검이 해제되어 Sword Slash 중단", Color.red);
                    break;
                }

                // 데미지 배율 계산
                float totalMultiplier = damageRatio;

                // VFX 선택
                string vfxName;
                string sfxName = "sfx_sword_hit";
                float vfxDuration;

                switch (i)
                {
                    case 0:
                        vfxName = "vfx_slash_hit";
                        vfxDuration = 1.5f;
                        break;
                    case 1:
                        vfxName = "flash_blue_purple";
                        vfxDuration = 1.5f;
                        break;
                    case 2:
                        vfxName = "debuff_03";
                        vfxDuration = 1.5f;
                        break;
                    default:
                        vfxName = "vfx_slash_hit";
                        vfxDuration = 1.5f;
                        break;
                }

                // VFX 재생
                Vector3 hitPosition = player.transform.position + player.transform.forward * 2f + Vector3.up * 1f;
                SimpleVFX.PlayWithSound(vfxName, sfxName, hitPosition, vfxDuration);

                // 직접 데미지 적용 (범위 3m 내 몬스터)
                var monsters = Character.GetAllCharacters()
                    .Where(c => c.IsMonsterFaction(Time.time) &&
                               Vector3.Distance(c.transform.position, player.transform.position) < 3f)
                    .Take(5);

                int hitCount = 0;
                foreach (var monster in monsters)
                {
                    var hit = new HitData();

                    // 검 주요 데미지 타입: slash
                    hit.m_damage.m_slash = baseSlash * totalMultiplier;
                    hit.m_damage.m_blunt = baseBlunt * totalMultiplier;
                    hit.m_damage.m_pierce = basePierce * totalMultiplier;

                    hit.m_point = monster.GetCenterPoint();
                    hit.m_dir = (monster.transform.position - player.transform.position).normalized;
                    hit.m_attacker = player.GetZDOID();
                    hit.SetAttacker(player);
                    hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                    monster.Damage(hit);
                    hitCount++;
                }

                totalHits += hitCount;
                swordSlashAttackCount[player] = i + 1;

                // 플로팅 텍스트
                string attackText = $"⚔️ Slash {i + 1}! ({(int)(totalMultiplier * 100)}%)";
                SkillEffect.DrawFloatingText(player, attackText, Color.red);

                // 다음 공격 대기
                if (i < attackCount - 1)
                {
                    yield return new WaitForSeconds(attackInterval);

                    // 대기 후 사망 체크
                    if (player == null || player.IsDead())
                    {
                        break;
                    }
                }
            }

            // 상태 정리
            if (swordSlashActive.ContainsKey(player))
            {
                swordSlashActive[player] = false;
            }

            SkillEffect.DrawFloatingText(player, $"⚔️ Sword Slash 완료! (총 {totalHits}명 타격)", Color.green);

            yield return null;
        }
        
        /// <summary>
        /// 단일 Slash 공격 실행
        /// </summary>
        private static void ExecuteSingleSlashAttack(Player player, int attackNumber, float damageRatio)
        {
            try
            {
                
                // 공격 VFX 효과
                PlaySlashAttackEffects(player, attackNumber);
                
                // 검 공격 애니메이션 강제 실행
                ForceAttackAnimation(player, attackNumber);
                
                // 플로팅 텍스트
                SkillEffect.DrawFloatingText(player, $"⚔️ Slash {attackNumber}! ({damageRatio}%)", Color.red);
                
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] {attackNumber}번째 공격 실행 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Slash 공격 VFX 효과 재생 - debuff_03 (시전) + flash_blue_purple (적중)
        /// </summary>
        private static void PlaySlashAttackEffects(Player player, int attackNumber)
        {
            try
            {
                Vector3 playerPos = player.transform.position + Vector3.up * 1f;
                Vector3 forwardPos = playerPos + player.transform.forward * 2f;
                
                // 스킬 시전 효과: debuff_03 (플레이어 주변)
                try
                {
                    SimpleVFX.Play("debuff_03", playerPos, 1.2f);
                }
                catch (System.Exception vfxEx)
                {
                    Plugin.Log.LogWarning($"[Sword Slash] 시전 VFX 재생 실패: {vfxEx.Message}");
                }

                // 적중 효과: flash_blue_purple (전방 위치)
                try
                {
                    SimpleVFX.Play("flash_blue_purple", forwardPos, 1.0f);
                }
                catch (System.Exception hitVfxEx)
                {
                    Plugin.Log.LogWarning($"[Sword Slash] 적중 VFX 재생 실패: {hitVfxEx.Message}");
                }
                
                // 공격 사운드
                var attackSounds = new string[]
                {
                    "sfx_sword_hit",
                    "sfx_metal_hit", 
                    "sfx_wood_hit"
                };
                
                foreach (var soundName in attackSounds)
                {
                    var sound = ZNetScene.instance?.GetPrefab(soundName)?.GetComponent<AudioSource>()?.clip;
                    if (sound != null)
                    {
                        AudioSource.PlayClipAtPoint(sound, playerPos);
                        break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] VFX 효과 재생 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 무기 데이터 백업 및 공격속도 극단 가속 (0.05초로 설정)
        /// </summary>
        private static WeaponBackupData BackupAndModifyWeaponSpeed(Player player)
        {
            try
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon == null || weapon.m_shared == null)
                {
                    Plugin.Log.LogWarning("[Sword Slash] 무기 정보를 가져올 수 없음");
                    return null;
                }
                
                // 원래 공격속도 백업 (animationSpeed는 Valheim에 없으므로 제거)
                var backup = new WeaponBackupData
                {
                    weapon = weapon,
                    originalAttackSpeed = weapon.m_shared.m_aiAttackInterval,
                    originalAnimationSpeed = 1.0f // 기본값으로 설정
                };
                
                
                // 공격속도를 극단으로 가속 (0.05초로 설정)
                weapon.m_shared.m_aiAttackInterval = 0.05f;
                
                
                return backup;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 무기 데이터 백업 및 수정 실패: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 무기 데이터 원상복구
        /// </summary>
        private static void RestoreWeaponSpeed(Player player, WeaponBackupData backup)
        {
            try
            {
                if (backup?.weapon?.m_shared == null)
                {
                    Plugin.Log.LogWarning("[Sword Slash] 백업 데이터가 없어 무기 데이터 복구 불가");
                    return;
                }
                
                // 원래 공격속도로 복구
                backup.weapon.m_shared.m_aiAttackInterval = backup.originalAttackSpeed;
                
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 무기 데이터 복구 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Valheim의 실제 공격 시스템 호출
        /// </summary>
        /// <summary>
        /// 실제 마우스 클릭과 동일한 공격 실행 - 직접 데미지 처리 방식
        /// </summary>
        private static void ExecuteMouseClickAttack(Player player, int attackNumber, float damageRatio)
        {
            try
            {
                
                // 1. 무기 정보 확인
                var weapon = player.GetCurrentWeapon();
                if (weapon == null)
                {
                    Plugin.Log.LogWarning($"[Sword Slash] {attackNumber}번째 공격 - 무기가 없음");
                    return;
                }
                
                // 2. 애니메이션 트리거 (검 공격 모션)
                var animator = player.GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    // 검 공격 애니메이션 트리거
                    animator.SetTrigger("swing_longsword");
                    animator.SetBool("attacking", true);
                }
                
                // 3. StartAttack 호출로 실제 공격 시작
                player.StartAttack(null, false);
                
                // 4. 직접 데미지 처리 (주변 몬스터에게)
                ApplyDirectSwordDamage(player, attackNumber, damageRatio);
                
                // 5. 플로팅 텍스트 표시
                SkillEffect.DrawFloatingText(player, $"⚔️ Slash {attackNumber}! ({damageRatio}%)", Color.red);
                
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] {attackNumber}번째 마우스 클릭 공격 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 기존 메서드들 정리 완료 - Valheim Attack 시스템 직접 제어로 대체됨
        /// </summary>
        
        /// <summary>
        /// 직접 검 데미지 처리 - 마우스 연속공격 시뮤레이션
        /// </summary>
        private static void ApplyDirectSwordDamage(Player player, int attackNumber, float damageRatio)
        {
            try
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return;
                
                // 주변 몰스터에게 직접 데미지 적용
                var monsters = Character.GetAllCharacters()
                    .Where(c => c.IsMonsterFaction(Time.time) && 
                               Vector3.Distance(c.transform.position, player.transform.position) < 3f)
                    .Take(5); // 최대 5체
                
                foreach (var monster in monsters)
                {
                    // 검 데미지 계산
                    var baseDamage = weapon.GetDamage();
                    var hitData = new HitData();
                    
                    hitData.m_damage.m_slash = baseDamage.m_slash * (damageRatio / 100f);
                    hitData.m_damage.m_blunt = baseDamage.m_blunt * (damageRatio / 100f);
                    hitData.m_damage.m_pierce = baseDamage.m_pierce * (damageRatio / 100f);
                    
                    hitData.m_point = monster.transform.position;
                    hitData.m_dir = (monster.transform.position - player.transform.position).normalized;
                    hitData.m_attacker = player.GetZDOID();
                    hitData.m_toolTier = (short)weapon.m_shared.m_toolTier;
                    
                    // 데미지 적용
                    monster.Damage(hitData);
                    
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 직접 데믴지 처리 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 공격 애니메이션 강제 실행 - 여러 방법 동시 사용
        /// </summary>
        private static void ForceAttackAnimation(Player player, int attackNumber)
        {
            try
            {
                // 방법 1: StartAttack 호출
                player.StartAttack(null, false);
                
                // 방법 2: 애니메이터 직접 제어
                var animator = player.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("swing_longsword");
                    animator.SetTrigger("attack");
                }
                
                // 방법 3: 공격 상태 강제 설정
                if (player.GetCurrentWeapon() != null)
                {
                    // 공격 차수 기록
                    swordSlashAttackCount[player] = attackNumber;
                    
                    // 공격 시작 시간 기록
                    var currentTime = Time.time;
                    if (!swordSlashAttackStartTime.ContainsKey(player))
                    {
                        swordSlashAttackStartTime[player] = new List<float>();
                    }
                    swordSlashAttackStartTime[player].Add(currentTime);
                }
                
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 공격 애니메이션 강제 실행 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 전체 콤보 기간 동안 초고속 민첩성 적용
        /// </summary>
        private static void ApplyHighSpeedAgility(Player player)
        {
            try
            {
                var skills = player.GetSkills();
                float currentAgility = skills.GetSkillLevel(Skills.SkillType.Run);
                
                // 원래 민첩성 레벨 저장
                originalAgilityLevels[player] = currentAgility;
                
                // 민첩성을 0으로 리셋 후 초고속 설정 (더 확실한 방법)
                skills.CheatRaiseSkill("Run", -currentAgility, true); // 현재 민첩성을 0으로
                skills.CheatRaiseSkill("Run", highSpeedAgilityBoost, true); // 15000으로 설정
                
                float newAgility = skills.GetSkillLevel(Skills.SkillType.Run);
                
                // 민첩성 조작 상태 플래그 설정
                agilityBeingModified[player] = true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 초고속 민첩성 적용 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 각 공격마다 민첩성 재적용 (확실한 속도 보장)
        /// </summary>
        private static void RefreshHighSpeedAgility(Player player, int attackNumber)
        {
            try
            {
                var skills = player.GetSkills();
                float currentAgility = skills.GetSkillLevel(Skills.SkillType.Run);
                
                // 매번 민첩성을 초고속으로 강제 설정 (확실하게)
                skills.CheatRaiseSkill("Run", -currentAgility, true); // 0으로 리셋
                skills.CheatRaiseSkill("Run", highSpeedAgilityBoost, true); // 15000으로 설정
                
                float newAgility = skills.GetSkillLevel(Skills.SkillType.Run);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 민첩성 재적용 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 애니메이션 속도 복구 - 민첩성 원상복구
        /// </summary>
        private static void RestoreAnimationSpeed(Player player)
        {
            try
            {
                // 슬래쉬 상태 정리
                if (swordSlashActive.ContainsKey(player))
                {
                    swordSlashActive[player] = false;
                }
                
                // 공격 시간 기록 정리
                if (swordSlashAttackStartTime.ContainsKey(player))
                {
                    swordSlashAttackStartTime[player].Clear();
                }
                
                // 민첩성 원상복구
                try
                {
                    var skills = player.GetSkills();
                    float currentAgility = skills.GetSkillLevel(Skills.SkillType.Run);
                    
                    // 저장된 원래 민첩성 레벨이 있으면 정확히 복구
                    if (originalAgilityLevels.ContainsKey(player))
                    {
                        float originalLevel = originalAgilityLevels[player];
                        float difference = currentAgility - originalLevel;
                        
                        if (Math.Abs(difference) > 1f) // 1 이상 차이가 나면 복구
                        {
                            skills.CheatRaiseSkill("Run", -difference, true);
                        }
                        
                        originalAgilityLevels.Remove(player);
                    }
                    else
                    {
                        // 저장된 원래 값이 없다면 기본값으로 복구
                        if (currentAgility > 1000f)
                        {
                            skills.CheatRaiseSkill("Run", -currentAgility, true);
                            skills.CheatRaiseSkill("Run", 100f, true);
                        }
                    }
                }
                catch (System.Exception agilityEx)
                {
                    Plugin.Log.LogError($"[Sword Slash] 민첩성 복구 실패: {agilityEx.Message}");
                }
                finally
                {
                    // 민첩성 조작 상태 플래그 해제
                    if (agilityBeingModified.ContainsKey(player))
                    {
                        agilityBeingModified[player] = false;
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[Sword Slash] 애니메이션 속도 복구 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sword Slash 액티브 상태 확인
        /// </summary>
        public static bool IsSwordSlashActive(Player player)
        {
            return swordSlashActive.TryGetValue(player, out bool active) && active &&
                   swordSlashEndTime.TryGetValue(player, out float endTime) && Time.time < endTime;
        }
        
        /// <summary>
        /// 현재 Sword Slash 공격 횟수 확인
        /// </summary>
        public static int GetSwordSlashAttackCount(Player player)
        {
            return swordSlashAttackCount.TryGetValue(player, out int count) ? count : 0;
        }
        
        /// <summary>
        /// Sword Slash 스킬 강제 중단
        /// </summary>
        public static void StopSwordSlash(Player player)
        {
            try
            {
                if (swordSlashActive.ContainsKey(player))
                {
                    swordSlashActive[player] = false;
                }
                
                if (swordSlashCoroutines.ContainsKey(player) && swordSlashCoroutines[player] != null)
                {
                    player.StopCoroutine(swordSlashCoroutines[player]);
                    swordSlashCoroutines[player] = null;
                }
                
                // 애니메이션 속도 복구
                RestoreAnimationSpeed(player);
                
                SkillEffect.DrawFloatingText(player, "Sword Slash 중단됨", Color.yellow);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Slash] 스킬 중단 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 검 스킬 쿨타임 정보 조회
        /// </summary>
        public static float GetSwordSlashCooldownRemaining(Player player)
        {
            if (swordSlashCooldowns.TryGetValue(player, out float cooldownEnd))
            {
                return Mathf.Max(0f, cooldownEnd - Time.time);
            }
            return 0f;
        }
        
        /// <summary>
        /// 모든 검 스킬 상태 초기화 (플레이어 로그아웃 시 등)
        /// </summary>
        public static void ClearSwordSkillStates(Player player)
        {
            try
            {
                StopSwordSlash(player);
                
                swordSlashCooldowns.Remove(player);
                swordSlashActive.Remove(player);
                swordSlashEndTime.Remove(player);
                swordSlashCoroutines.Remove(player);
                swordSlashAttackCount.Remove(player);
                swordSlashAttackStartTime.Remove(player); // AnimationSpeedManager 연동용 추가
                agilityBeingModified.Remove(player); // 중복 실행 방지 플래그 정리
                originalAgilityLevels.Remove(player); // 원래 민첩성 레벨 정리
                parryStacks.Remove(player); // 패링 스택 정리
                parryStackExpiry.Remove(player); // 패링 만료시간 정리

                // 민첩성 완전 정리 - 로그아웃 시 확실한 복구
                try
                {
                    var skills = player.GetSkills();
                    float currentAgility = skills.GetSkillLevel(Skills.SkillType.Run);
                    
                    // 민첩성이 비정상적으로 높다면 강제 복구
                    if (currentAgility > 1000f)
                    {
                        // 0으로 리셋 후 기본값 100으로 설정
                        skills.CheatRaiseSkill("Run", -currentAgility, true);
                        skills.CheatRaiseSkill("Run", 100f, true);
                        
                        Plugin.Log.LogInfo($"[Sword Skill 정리] {player.GetPlayerName()} 민첩성 강제 복구: {currentAgility:F1} → {skills.GetSkillLevel(Skills.SkillType.Run):F1}");
                    }
                }
                catch (System.Exception agilityEx)
                {
                    Plugin.Log.LogError($"[Sword Skill 정리] 민첩성 복구 실패: {agilityEx.Message}");
                }
                
                Plugin.Log.LogDebug($"[Sword Skill] {player.GetPlayerName()} 모든 검 스킬 상태 초기화 완료 (민첩성 복구 포함)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Sword Skill] 상태 초기화 오류: {ex.Message}");
            }
        }
        
        // === AnimationSpeedManager 연동 메서드들 ===
        
        /// <summary>
        /// 슬래쉬 스킬이 활성화 상태인지 확인
        /// AnimationSpeedManager에서 호출됨
        /// </summary>
        public static bool IsSlashActive(Player player)
        {
            if (player == null) return false;
            
            bool isActive = swordSlashActive.TryGetValue(player, out bool active) && active;
            
            if (isActive)
            {
                Plugin.Log.LogInfo($"[Slash Speed] {player.GetPlayerName()} 슬래쉬 활성 상태 확인됨 - 20배 속도 준비");
            }
            
            return isActive;
        }
        
        /// <summary>
        /// 현재 공격 차수에 따른 속도 배율 반환
        /// 모든 공격: 20.0x (통일된 고속 공격)
        /// </summary>
        public static float GetCurrentSlashSpeedMultiplier(Player player)
        {
            if (player == null) return 1.0f;
            
            int currentAttack = swordSlashAttackCount.TryGetValue(player, out int count) ? count : 0;
            
            // 공격 차수에 따른 속도 배율 (1부터 시작하므로 index는 -1)
            if (currentAttack >= 1 && currentAttack <= attackSpeedMultipliers.Length)
            {
                float multiplier = attackSpeedMultipliers[currentAttack - 1];
                Plugin.Log.LogInfo($"[Slash Speed] {player.GetPlayerName()} {currentAttack}차 공격 속도 배율: {multiplier}x");
                return multiplier;
            }
            
            // 기본값 (슬래쉬 활성이지만 공격 차수가 범위를 벗어난 경우)
            Plugin.Log.LogDebug($"[Slash Speed] {player.GetPlayerName()} 공격 차수 범위 외: {currentAttack}, 기본 속도 유지");
            return 1.0f;
        }
        
        /// <summary>
        /// AnimationSpeedManager에서 호출되는 슬래쉬 공격 속도 수정 메서드
        /// 새로운 방식: 전체 콤보 기간 동안 초고속 민첩성 유지
        /// </summary>
        public static double ModifySlashAttackSpeed(Player player, double speed)
        {
            try
            {
                // 슬래쉬 스킬이 활성화되어 있는지 확인
                if (!IsSlashActive(player))
                {
                    return speed; // 슬래쉬가 활성화되지 않았으면 기본 속도 반환
                }
                
                // 검을 사용 중인지 확인
                if (!IsUsingSword(player))
                {
                    return speed; // 검이 아니면 기본 속도 반환
                }
                
                // 민첩성 조작이 이미 적용되어 있는지 확인
                if (agilityBeingModified.TryGetValue(player, out bool isModifying) && isModifying)
                {
                    // 이미 초고속 민첩성이 적용된 상태이므로 그대로 유지
                    Plugin.Log.LogDebug($"[Slash Speed] {player.GetPlayerName()} 초고속 민첩성 상태 유지 중");
                    return speed; // 민첩성 부스트가 속도에 자동 반영됨
                }
                
                return speed; // 기본 속도 반환
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Slash Speed] ModifySlashAttackSpeed 오류: {ex.Message}");
                return speed; // 오류 발생 시 기본 속도 반환
            }
        }

        /// <summary>
        /// 검 베기 액티브 스킬 사망 시 정리 시스템
        /// </summary>
        public static void CleanupSwordSkillOnDeath(Player player)
        {
            try
            {
                // 검 베기 스킬 Dictionary 정리 (10개)
                swordSlashCooldowns.Remove(player);
                swordSlashActive.Remove(player);
                swordSlashEndTime.Remove(player);

                if (swordSlashCoroutines.ContainsKey(player) && swordSlashCoroutines[player] != null)
                {
                    try
                    {
                        Plugin.Instance?.StopCoroutine(swordSlashCoroutines[player]);
                    }
                    catch { }
                    swordSlashCoroutines.Remove(player);
                }

                swordSlashAttackCount.Remove(player);
                swordSlashAttackStartTime.Remove(player);
                agilityBeingModified.Remove(player);
                originalAgilityLevels.Remove(player);
                parryStacks.Remove(player); // 패링 스택 정리
                parryStackExpiry.Remove(player); // 패링 만료시간 정리
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Sword Skill] 정리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 검 전문가 - 공격력 보너스 (비율)
        /// 실제 효과는 ItemData.GetDamage 패치에서 적용됨
        /// </summary>
        public static float GetSwordExpertDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_expert") || !Sword_Skill.IsUsingSword(player))
                return 0f;

            try
            {
                float damageBonus = Sword_Config.SwordExpertDamageValue;
                Plugin.Log.LogDebug($"[검 전문가] 공격력 +{damageBonus}%");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 전문가] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 칼날 되치기 - 공격력 고정 보너스
        /// 실제 효과는 ItemData.GetDamage 패치에서 적용됨
        /// </summary>
        public static float GetSwordRiposteDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_step3_riposte") || !Sword_Skill.IsUsingSword(player)) return 0f;

            try
            {
                float damageBonus = Sword_Config.SwordRiposteDamageBonusValue;
                Plugin.Log.LogDebug($"[칼날 되치기] 공격력 +{damageBonus}");
                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[칼날 되치기] 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

        /// <summary>
        /// 방어 전환 - 패링 스택 기반 공격력 보너스
        /// 방패 착용 + 패링 성공 시 스택에 따라 공격력 증가
        /// 1스택: 55%, 2스택: 120%, 3스택: 200%
        /// </summary>
        public static float GetSwordDefenseSwitchDamageBonus(Player player)
        {
            if (!SkillEffect.HasSkill("sword_step5_defswitch") || !Sword_Skill.IsUsingSword(player)) return 0f;

            try
            {
                // 방패 착용 확인
                if (!HasShield(player))
                {
                    return 0f; // 방패 미착용 시 보너스 없음
                }

                // 패링 스택 기반 공격력 보너스 반환
                float damageBonus = GetParryDamageBonus(player);
                if (damageBonus > 0f)
                {
                    int stacks = GetParryStacks(player);
                    Plugin.Log.LogDebug($"[방어 전환] 패링 스택 {stacks} - 공격력 +{damageBonus}%");
                }

                return damageBonus;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[방어 전환] 공격력 보너스 계산 실패: {ex.Message}");
                return 0f;
            }
        }

    }

    /// <summary>
    /// 검 공격력 증가 패치 (전문가, 단계별 스킬)
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class SwordDamageBonus_Patch
    {
        /// <summary>
        /// Sword Slash 액티브 스킬 데미지 배율만 처리
        /// 일반 공격력 보너스는 GetDamage 패치로 이동 (Rule 13)
        /// </summary>
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (hit.GetAttacker() is Player player && Sword_Skill.IsUsingSword(player))
                {
                    // Sword Slash 액티브 80% 배율 적용
                    if (Sword_Skill.IsSwordSlashActive(player))
                    {
                        float damageRatio = Sword_Config.SwordSlashDamageRatioValue / 100f;
                        hit.m_damage.m_slash *= damageRatio;

                        Plugin.Log.LogDebug($"[Sword Slash] 액티브 배율 {damageRatio:F2}x (slash)");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 데미지 패치] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 방어 전환 - 방패 착용 시 받는 피해 감소
        /// 실제 효과는 Character.Damage 패치에서 적용됨
        /// </summary>
        public static float GetSwordDefenseSwitchDamageReduction(Player player)
        {
            if (!SkillEffect.HasSkill("sword_step5_defswitch") || !Sword_Skill.IsUsingSword(player)) return 0f;

            try
            {
                // 방패 착용 확인 (리플렉션 사용)
                var leftItemField = typeof(Humanoid).GetField("m_leftItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var leftItem = leftItemField?.GetValue(player) as ItemDrop.ItemData;
                bool hasShield = leftItem != null && leftItem.IsWeapon() && leftItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield;

                if (hasShield)
                {
                    float damageReduction = Sword_Config.SwordDefenseSwitchDamageReductionValue;
                    Plugin.Log.LogDebug($"[방어 전환] 방패 착용 - 받는 피해 -{damageReduction}%");
                    return damageReduction;
                }

                return 0f;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[방어 전환] 피해 감소 계산 실패: {ex.Message}");
                return 0f;
            }
        }
    }
}
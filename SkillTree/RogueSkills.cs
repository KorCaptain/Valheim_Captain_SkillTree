using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 로그 직업 전용 스킬 시스템
    /// Y키로 발동되는 로그 그림자 일격 스킬 관리
    /// </summary>
    public static class RogueSkills
    {
        // === 🔒 수정 5: Dictionary 동시 접근 방지 lock ===
        private static readonly object rogueDictionaryLock = new object();

        // === 그림자 일격 상태 관리 ===
        private static Dictionary<Player, float> rogueAttackBuffExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> rogueAttackBuffCoroutine = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, GameObject> rogueSparkleEffects = new Dictionary<Player, GameObject>();
        
        // === 스텔스 시스템 ===
        private static Dictionary<Player, float> stealthEndTime = new Dictionary<Player, float>();
        private static Dictionary<Player, bool> stealthActive = new Dictionary<Player, bool>();
        private static Dictionary<Player, Coroutine> stealthDurationCoroutine = new Dictionary<Player, Coroutine>();

        // === 버프 VFX 시스템 ===
        private static Dictionary<Player, GameObject> rogueBuffVFXInstances = new Dictionary<Player, GameObject>();

        
        /// <summary>
        /// 로그 스킬을 SkillTreeManager에 등록
        /// </summary>
        public static void RegisterRogueSkill()
        {
            var manager = SkillTreeManager.Instance;
            
            // 로그 직업 스킬 등록 (동적 툴팁 연동)
            manager.AddSkill(new SkillNode {
                Id = "Rogue",
                Name = "로그",
                Description = Rogue_Tooltip.GetRogueTooltip(), // 컨피그 연동 동적 툴팁
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(350, 395),
                Category = "직업",
                IconName = "Rogue_unlock",
                IconNameLocked = "Rogue_lock",
                IconNameUnlocked = "Rogue_unlock",
                NextNodes = new List<string>(),
                RequiredPlayerLevel = 10,
                ApplyEffect = (lv) => { }
            });
        }

        /// <summary>
        /// 로그인지 확인
        /// </summary>
        public static bool IsRogue(Player player)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                return manager != null && manager.GetSkillLevel("Rogue") > 0;
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 스킬] 로그 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Y키 로그 그림자 일격 스킬 실행
        /// </summary>
        public static void ExecuteRogueShadowStrike(Player player)
        {
            if (player == null)
            {
                // Plugin.Log.LogWarning("[로그 그림자 일격] 플레이어가 null입니다");
                return;
            }

            // 쿨다운 체크
            if (JobSkillsUtility.IsOnCooldown(player, "Rogue"))
            {
                // 쿨타임 남은 시간과 함께 메시지 표시
                float remainingTime = JobSkillsUtility.GetRemainingCooldown(player, "Rogue");
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_shadow_strike_cooldown", remainingTime.ToString("F1")));
                return;
            }

            // 단검 착용 체크
            if (!IsUsingDagger(player))
            {
                JobSkillsUtility.ShowRequirementMessage(player, L.Get("rogue_dagger_required"));
                return;
            }

            // 스태미나 체크
            float requiredStamina = Rogue_Config.RogueShadowStrikeStaminaCostValue;
            if (player.GetStamina() < requiredStamina)
            {
                JobSkillsUtility.ShowRequirementMessage(player, L.Get("stamina_insufficient"));
                return;
            }

            // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 그림자 일격 스킬 시작");

            try
            {
                // 스태미나 소모
                player.UseStamina(requiredStamina);

                // 연막 효과 생성 (2배 크기)
                CreateSmokeEffect(player);

                // 주변 몬스터 어그로 제거
                int aggroRemoved = RemoveNearbyMonsterAggro(player);

                // 어그로 해제 결과 메시지 (화면 중앙)
                if (aggroRemoved > 0)
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("rogue_shadow_strike_success", aggroRemoved.ToString()));
                }
                else
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("rogue_shadow_strike_no_enemy"));
                }

                // 공격력 증가 버프 적용
                ApplyRogueAttackBuff(player);

                // 스텔스 적용 (8초간 몬스터가 타겟팅하지 못함)
                ApplyStealthState(player);

                // 쿨다운 설정
                JobSkillsUtility.SetCooldown(player, "Rogue", Rogue_Config.RogueShadowStrikeCooldownValue);

                // 스킬 발동 효과
                PlayRogueEffects(player);
                
                // 스킬 시전 효과음 재생 (sfx_oozebomb_explode)
                PlayRogueCastSound(player);

                // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 그림자 일격 완료 - {aggroRemoved}마리 어그로 제거");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] 스킬 실행 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 단검 또는 클로(Claw) 착용 여부 확인
        /// </summary>
        private static bool IsUsingDagger(Player player)
        {
            try
            {
                var weapon = player?.GetCurrentWeapon();
                if (weapon?.m_shared != null)
                {
                    // 단검(나이프) 스킬 타입 확인
                    bool isDagger = weapon.m_shared.m_skillType == Skills.SkillType.Knives;
                    // 클로(Claw) - Unarmed 스킬 타입
                    bool isClaw = weapon.m_shared.m_skillType == Skills.SkillType.Unarmed;

                    // 프리팹 이름에 단검 관련 키워드가 포함되어 있는지 확인
                    string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
                    string prefabName = weapon.m_dropPrefab?.name?.ToLower() ?? "";
                    bool isDaggerByName = ContainsDaggerKeyword(weaponName) || ContainsDaggerKeyword(prefabName);

                    bool isValidWeapon = isDagger || isClaw || isDaggerByName;
                    // Plugin.Log.LogInfo($"[로그 스킬] {player.GetPlayerName()} 무기 확인: {weapon.m_shared.m_name} - 단검타입: {isDagger}, 클로: {isClaw}, 단검이름: {isDaggerByName}");
                    return isValidWeapon;
                }

                // 무기가 없는 경우 (맨손) - 클로 미착용으로 처리
                // Plugin.Log.LogInfo($"[로그 스킬] {player.GetPlayerName()} 무기 없음 - 클로 미착용");
                return false;
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 스킬] 무기 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 프리팹 이름에 단검 관련 키워드가 포함되어 있는지 확인
        /// </summary>
        private static bool ContainsDaggerKeyword(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            
            string[] daggerKeywords = { "knives", "knife", "dagger", "claw", "fist" };
            string lowerName = name.ToLower();

            foreach (string keyword in daggerKeywords)
            {
                if (lowerName.Contains(keyword))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 연막 효과 생성 (발하임 기본 smokebomb_explosion 사용)
        /// </summary>
        private static void CreateSmokeEffect(Player player)
        {
            try
            {
                Vector3 playerPos = player.transform.position;

                // 발헤임 기본 smokebomb_explosion 프리팹 (데미지 컴포넌트 비활성화)
                var znetScene = ZNetScene.instance;
                if (znetScene != null)
                {
                    var smokePrefab = znetScene.GetPrefab("smokebomb_explosion");
                    if (smokePrefab != null)
                    {
                        GameObject smokeInstance = UnityEngine.Object.Instantiate(smokePrefab, playerPos, Quaternion.identity);

                        // AOE 데미지 컴포넌트 비활성화 (질식 데미지 제거)
                        Aoe aoeComponent = smokeInstance.GetComponentInChildren<Aoe>();
                        if (aoeComponent != null)
                        {
                            aoeComponent.enabled = false;
                            // Plugin.Log.LogDebug("[로그 그림자 일격] smokebomb_explosion AOE 데미지 비활성화");
                        }

                        // Plugin.Log.LogDebug($"[로그 그림자 일격] smokebomb_explosion 연막 효과 생성 (데미지 없음)");
                        return;
                    }
                }

                // 프리팹 로드 실패 시 메시지만
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_smoke"));
                // Plugin.Log.LogWarning("[로그 그림자 일격] smokebomb_explosion 프리팹 없음 - 메시지만 표시");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] 연막 효과 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 대체 연막 효과 생성 (네트워크 동기화) - 사운드 제거
        /// </summary>
        private static void CreateAlternativeSmokeEffect(Player player)
        {
            try
            {
                // smoke ground 제거 (중복 VFX 방지) - sparkle_ellow 주석처리
                // CreateSparkleEffect(player);

                // 메시지 표시
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_smoke"));
                // Plugin.Log.LogInfo("[로그 그림자 일격] 대체 연막 효과: 메시지만 표시 (sparkle_ellow 비활성화)");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] 대체 연막 효과 실패: {ex.Message}");
                try
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("rogue_smoke"));
                    // Plugin.Log.LogInfo("[로그 그림자 일격] 메시지 효과로 대체");
                }
                catch (System.Exception)
                {
                    // Plugin.Log.LogError($"[로그 그림자 일격] 메시지 효과도 실패: {msgEx.Message}");
                }
            }
        }

        /// <summary>
        /// sparkle_ellow 이팩트 생성 - 버프 지속시간 동안 플레이어를 따라다님
        /// (주석처리 - 무한 로딩 원인 가능성)
        /// </summary>
        /*
        private static void CreateSparkleEffect(Player player)
        {
            try
            {
                // 기존 sparkle 이팩트가 있으면 제거
                RemoveSparkleEffect(player);

                // VFX 매니저를 통해 sparkle_ellow 프리팹 가져오기
                var sparklePrefab = VFXManager.GetVFXPrefab("sparkle_ellow");
                if (sparklePrefab != null)
                {
                    // 프리팹을 Instantiate하여 지속적인 이팩트 생성
                    var sparkleEffect = UnityEngine.Object.Instantiate(sparklePrefab, player.transform.position, Quaternion.identity);
                    if (sparkleEffect != null)
                    {
                        // 플레이어를 부모로 설정하여 따라다니게 함
                        sparkleEffect.transform.SetParent(player.transform);
                        sparkleEffect.transform.localPosition = Vector3.up * 1.5f; // 플레이어 머리 위 약간

                        // Dictionary에 저장
                        rogueSparkleEffects[player] = sparkleEffect;

                        // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} sparkle_ellow 이팩트 생성 완료");
                    }
                    else
                    {
                        // Plugin.Log.LogWarning("[로그 그림자 일격] sparkle_ellow Instantiate 실패");
                    }
                }
                else
                {
                    // Plugin.Log.LogWarning("[로그 그림자 일격] sparkle_ellow 프리팹을 찾을 수 없음 - VFX 매니저에서 로드 실패");
                }
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] sparkle_ellow 이팩트 생성 실패: {ex.Message}");
            }
        }
        */

        /// <summary>
        /// sparkle_ellow 이팩트 제거
        /// (주석처리 - 무한 로딩 원인 가능성)
        /// </summary>
        /*
        private static void RemoveSparkleEffect(Player player)
        {
            try
            {
                if (rogueSparkleEffects.ContainsKey(player))
                {
                    var sparkleEffect = rogueSparkleEffects[player];
                    if (sparkleEffect != null)
                    {
                        UnityEngine.Object.Destroy(sparkleEffect);
                        // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} sparkle_ellow 이팩트 제거 완료");
                    }

                    rogueSparkleEffects.Remove(player);
                }
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] sparkle_ellow 이팩트 제거 실패: {ex.Message}");
            }
        }
        */

        /// <summary>
        /// 주변 몬스터 어그로 제거 - 개선된 버전
        /// </summary>
        private static int RemoveNearbyMonsterAggro(Player player)
        {
            int aggroRemovedCount = 0;
            
            try
            {
                float aggroRange = Rogue_Config.RogueShadowStrikeAggroRangeValue;
                Vector3 playerPos = player.transform.position;
                
                // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 어그로 제거 시작 (범위: {aggroRange}m)");
                
                // Character.GetAllCharacters()를 사용하여 모든 캐릭터 확인 (탱커 스킬과 동일한 방식)
                var nearbyEnemies = Character.GetAllCharacters()
                    .Where(c => c != null && !c.IsDead() && c != player && !c.IsPlayer())
                    .Where(c => Vector3.Distance(playerPos, c.transform.position) <= aggroRange)
                    .ToList();

                // Plugin.Log.LogInfo($"[로그 그림자 일격] 범위 {aggroRange}m 내 {nearbyEnemies.Count}마리 몬스터 발견");

                foreach (var enemy in nearbyEnemies)
                {
                    if (enemy == null) continue;

                    try
                    {
                        string enemyName = enemy.GetHoverName() ?? enemy.name ?? "Unknown";
                        // Plugin.Log.LogInfo($"[로그 그림자 일격] {enemyName} 어그로 제거 시도");

                        // AI 컴포넌트 가져오기
                        var baseAI = enemy.GetBaseAI();
                        if (baseAI != null)
                        {
                            // 안전한 어그로 제거 시스템 (여러 방법 시도)
                            bool aggroRemoved = SafeRemoveAggro(player, enemy, baseAI, enemyName);
                            
                            if (aggroRemoved)
                            {
                                aggroRemovedCount++;
                                // Plugin.Log.LogInfo($"[로그 그림자 일격] {enemyName} 어그로 제거 성공!");
                            }
                            else
                            {
                                // Plugin.Log.LogWarning($"[로그 그림자 일격] {enemyName} 어그로 제거 실패");
                            }
                        }
                        else
                        {
                            // Plugin.Log.LogWarning($"[로그 그림자 일격] {enemyName}에 BaseAI 컴포넌트가 없음");
                        }
                    }
                    catch (System.Exception)
                    {
                        // Plugin.Log.LogError($"[로그 그림자 일격] {enemy.name ?? "Unknown"} 어그로 제거 중 예외 발생: {ex.Message}");
                    }
                }
                
                // Plugin.Log.LogInfo($"[로그 그림자 일격] 어그로 제거 완료 - 총 {aggroRemovedCount}마리");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] 어그로 제거 실패: {ex.Message}");
            }
            
            return aggroRemovedCount;
        }

        /// <summary>
        /// 안전한 어그로 제거 시스템 - 강화된 다단계 어그로 해제
        /// </summary>
        private static bool SafeRemoveAggro(Player player, Character enemy, BaseAI ai, string enemyName)
        {
            bool success = false;
            
            try
            {
                // 1단계: 현재 타겟 확인
                var currentTarget = ai.GetTargetCreature();
                bool isTargetingPlayer = (currentTarget != null && currentTarget.gameObject == player.gameObject);
                
                Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} 현재 타겟: {(isTargetingPlayer ? "플레이어" : "다른 대상 또는 없음")}");
                
                if (!isTargetingPlayer)
                {
                    Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName}는 이미 플레이어를 타겟하지 않음");
                    return true; // 이미 어그로가 없음
                }
                
                // 2단계: MonsterAI 특별 처리 - 강화된 버전
                var monsterAI = ai as MonsterAI;
                if (monsterAI != null)
                {
                    try
                    {
                        // MonsterAI의 SetHuntPlayer 해제
                        var setHuntMethod = typeof(MonsterAI).GetMethod("SetHuntPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (setHuntMethod != null)
                        {
                            setHuntMethod.Invoke(monsterAI, new object[] { null });
                            Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} SetHuntPlayer 해제 성공");
                            success = true;
                        }
                        
                        // 추가: MonsterAI의 m_hunt 필드 직접 해제
                        var huntField = typeof(MonsterAI).GetField("m_hunt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (huntField != null)
                        {
                            huntField.SetValue(monsterAI, null);
                            Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} m_hunt 필드 해제 성공");
                            success = true;
                        }
                    }
                    catch (System.Exception huntEx)
                    {
                        Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} MonsterAI 처리 실패: {huntEx.Message}");
                    }
                }
                
                // 3단계: BaseAI 리플렉션으로 강력한 타겟 해제
                try
                {
                    // SetTarget 메서드 호출
                    var setTargetMethod = ai.GetType().GetMethod("SetTarget", new[] { typeof(Character) });
                    if (setTargetMethod != null)
                    {
                        setTargetMethod.Invoke(ai, new object[] { null });
                        Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} SetTarget(null) 호출 성공");
                        success = true;
                    }
                    
                    // m_target 필드 직접 해제
                    var targetField = ai.GetType().GetField("m_target", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (targetField != null)
                    {
                        targetField.SetValue(ai, null);
                        Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} m_target 필드 해제 성공");
                        success = true;
                    }
                    
                    // m_targetCreature 필드 직접 해제
                    var targetCreatureField = ai.GetType().GetField("m_targetCreature", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (targetCreatureField != null)
                    {
                        targetCreatureField.SetValue(ai, null);
                        Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} m_targetCreature 필드 해제 성공");
                        success = true;
                    }
                }
                catch (System.Exception targetEx)
                {
                    Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} 타겟 해제 실패: {targetEx.Message}");
                }
                
                // 4단계: 강제 상태 초기화 - 완전한 리셋
                try
                {
                    // alerted 상태 해제
                    var alertedField = ai.GetType().GetField("m_alerted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (alertedField != null)
                    {
                        alertedField.SetValue(ai, false);
                        Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} alerted 상태 해제");
                        success = true;
                    }
                    
                    // 추가: timeSinceHurt 리셋
                    var timeSinceHurtField = ai.GetType().GetField("m_timeSinceHurt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (timeSinceHurtField != null)
                    {
                        timeSinceHurtField.SetValue(ai, 999f); // 오래된 시간으로 설정
                        Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} timeSinceHurt 리셋");
                        success = true;
                    }
                    
                    // 추가: lastDamageTime 리셋
                    var lastDamageTimeField = ai.GetType().GetField("m_lastDamageTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (lastDamageTimeField != null)
                    {
                        lastDamageTimeField.SetValue(ai, 0f);
                        Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} lastDamageTime 리셋");
                        success = true;
                    }
                }
                catch (System.Exception stateEx)
                {
                    Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} 상태 리셋 실패: {stateEx.Message}");
                }
                
                // 5단계: 물리적 방해 - 약간의 스턴 효과로 AI 리셋
                try
                {
                    var stunField = enemy.GetType().GetField("m_staggerTimer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (stunField != null)
                    {
                        stunField.SetValue(enemy, 0.1f); // 매우 짧은 스턴
                        Plugin.Log.LogInfo($"[안전한 어그로 제거] {enemyName} 짧은 스턴으로 AI 리셋");
                        success = true;
                    }
                }
                catch (System.Exception stunEx)
                {
                    Plugin.Log.LogDebug($"[안전한 어그로 제거] {enemyName} 스턴 리셋 실패: {stunEx.Message}");
                }
                
                // 6단계: 최종 확인 (Thread.Sleep 제거)
                try
                {
                    var finalTarget = ai.GetTargetCreature();
                    if (finalTarget == null || finalTarget.gameObject != player.gameObject)
                    {
                        Plugin.Log.LogInfo($"[✅ 안전한 어그로 제거] {enemyName} 어그로 해제 확인됨");
                        return true;
                    }
                    else
                    {
                        Plugin.Log.LogWarning($"[❌ 안전한 어그로 제거] {enemyName} 어그로 해제 실패 - 여전히 플레이어를 타겟 중");
                        return success;
                    }
                }
                catch (System.Exception checkEx)
                {
                    Plugin.Log.LogWarning($"[안전한 어그로 제거] {enemyName} 최종 확인 실패: {checkEx.Message}");
                    return success; // 부분적으로라도 성공했으면 true 반환
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[❌ 안전한 어그로 제거] {enemyName} 처리 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 로그 공격력 증가 버프 적용
        /// </summary>
        private static void ApplyRogueAttackBuff(Player player)
        {
            try
            {
                float buffDuration = Rogue_Config.RogueShadowStrikeBuffDurationValue;
                float attackBonus = Rogue_Config.RogueShadowStrikeAttackBonusValue;
                
                // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 공격력 버프 적용 (+{attackBonus}%, {buffDuration}초)");
                
                // 기존 버프 제거
                if (rogueAttackBuffCoroutine.ContainsKey(player))
                {
                    if (rogueAttackBuffCoroutine[player] != null)
                    {
                        Plugin.Instance?.StopCoroutine(rogueAttackBuffCoroutine[player]);
                    }
                    rogueAttackBuffCoroutine.Remove(player);
                }
                
                // 기존 sparkle 이팩트 제거 (주석처리 - 무한 로딩 원인 가능성)
                // RemoveSparkleEffect(player);
                
                // 버프 만료 시간 설정
                rogueAttackBuffExpiry[player] = Time.time + buffDuration;
                
                // 버프 지속 코루틴 시작
                if (Plugin.Instance != null)
                {
                    var coroutine = Plugin.Instance.StartCoroutine(RogueAttackBuffCoroutine(player, buffDuration));
                    rogueAttackBuffCoroutine[player] = coroutine;
                }

                // 버프 VFX 생성 (머리 위 1.2m)
                CreateRogueBuffVFX(player);

                // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 공격력 버프 시작");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] 공격력 버프 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 로그 공격력 버프 코루틴
        /// </summary>
        private static IEnumerator RogueAttackBuffCoroutine(Player player, float duration)
        {
            // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 공격력 버프 코루틴 시작 ({duration}초)");

            yield return new WaitForSeconds(duration);

            // 🔒 수정 2: 안전한 player 검증
            if (player == null)
            {
                // Plugin.Log.LogInfo("[로그 그림자 일격] 플레이어 null로 코루틴 중단");
                yield break;  // player가 null이면 Dictionary 접근 안 함
            }

            if (player.IsDead())
            {
                // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 사망으로 코루틴 중단");

                // 🔒 수정 7: 코루틴 내부 Dictionary 접근도 lock 보호
                lock (rogueDictionaryLock)
                {
                    try
                    {
                        if (rogueAttackBuffExpiry.ContainsKey(player))
                            rogueAttackBuffExpiry.Remove(player);
                        if (rogueAttackBuffCoroutine.ContainsKey(player))
                            rogueAttackBuffCoroutine.Remove(player);
                    }
                    catch (Exception)
                    {
                        // Plugin.Log.LogWarning($"[로그 스킬] Dictionary 정리 실패: {ex.Message}");
                    }
                }

                // 버프 VFX 제거
                RemoveRogueBuffVFX(player);

                yield break;
            }

            // 🔒 버프 정상 종료 시 Dictionary 정리 (lock 보호)
            lock (rogueDictionaryLock)
            {
                try
                {
                    if (rogueAttackBuffExpiry.ContainsKey(player))
                        rogueAttackBuffExpiry.Remove(player);

                    if (rogueAttackBuffCoroutine.ContainsKey(player))
                        rogueAttackBuffCoroutine.Remove(player);
                }
                catch (Exception)
                {
                    // Plugin.Log.LogWarning($"[로그 스킬] 버프 종료 시 Dictionary 정리 실패: {ex.Message}");
                }
            }

            // sparkle_ellow 이팩트 제거 (주석처리 - 무한 로딩 원인 가능성)
            // RemoveSparkleEffect(player);

            // 버프 VFX 제거
            RemoveRogueBuffVFX(player);

            // 🔒 수정 3: 안전한 버프 종료 알림
            if (player != null && !player.IsDead())
            {
                try
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("rogue_buff_end"));
                }
                catch (Exception)
                {
                    // 버프 종료 알림 실패 시 무시
                }
            }
            else
            {
                // Plugin.Log.LogInfo("[로그 스킬] 버프 종료 알림 건너뜀 (플레이어 유효하지 않음)");
            }

            // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 공격력 버프 종료");
        }

        /// <summary>
        /// 로그 공격력 버프 활성 상태 확인
        /// </summary>
        public static bool IsRogueAttackBuffActive(Player player)
        {
            if (player == null) return false;
            
            if (rogueAttackBuffExpiry.ContainsKey(player))
            {
                if (Time.time < rogueAttackBuffExpiry[player])
                {
                    return true;
                }
                else
                {
                    // 만료된 버프 제거
                    rogueAttackBuffExpiry.Remove(player);
                    if (rogueAttackBuffCoroutine.ContainsKey(player))
                    {
                        rogueAttackBuffCoroutine.Remove(player);
                    }
                    
                    // 만료된 sparkle 이팩트도 제거 (주석처리 - 무한 로딩 원인 가능성)
                    // RemoveSparkleEffect(player);
                }
            }
            
            return false;
        }

        /// <summary>
        /// 로그 공격력 버프 배율 가져오기
        /// </summary>
        public static float GetRogueAttackBuffMultiplier(Player player)
        {
            if (IsRogueAttackBuffActive(player))
            {
                float attackBonus = Rogue_Config.RogueShadowStrikeAttackBonusValue;
                return 1f + (attackBonus / 100f); // 35% -> 1.35
            }
            
            return 1f;
        }

        /// <summary>
        /// 로그 스킬 효과 재생 (네트워크 동기화) - 시각 효과만, 사운드 제거
        /// </summary>
        private static void PlayRogueEffects(Player player)
        {
            try
            {
                Vector3 playerPos = player.transform.position;
                Quaternion playerRot = player.transform.rotation;

                // SimpleVFX로 VFX 재생
                SimpleVFX.Play("flash_blue_purple", playerPos, 2f);

                // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 시각 효과 재생 완료 (flash_blue_purple)");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 그림자 일격] 시각 효과 재생 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 로그 스킬 시전 효과음 재생 (sfx_oozebomb_explode)
        /// </summary>
        private static void PlayRogueCastSound(Player player)
        {
            try
            {
                // SimpleVFX로 사운드 재생
                Vector3 playerPos = player.transform.position;

                SimpleVFX.Play("sfx_oozebomb_explode", playerPos, 0.5f);
                // Plugin.Log.LogDebug($"[로그 그림자 일격] {player.GetPlayerName()} 사운드 효과 재생 (sfx_oozebomb_explode)");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogWarning($"[로그 그림자 일격] 사운드 재생 실패: {ex.Message} - 플레이어에게 메시지로 알림");
                
                // 실패시 플레이어에게 메시지로 대체
                try
                {
                    player.Message(MessageHud.MessageType.TopLeft, L.Get("rogue_shadow_strike_activate"));
                    // Plugin.Log.LogInfo($"[로그 그림자 일격] {player.GetPlayerName()} 사운드 재생 실패 - 메시지로 대체");
                }
                catch (System.Exception)
                {
                    // Plugin.Log.LogError($"[로그 그림자 일격] 메시지 대체도 실패: {msgEx.Message}");
                }
            }
        }

        /// <summary>
        /// 지연 후 효과 제거 코루틴
        /// </summary>
        private static IEnumerator DestroyEffectAfterDelay(GameObject effect, float delay)
        {
            yield return new WaitForSeconds(delay);

            // ✅ 플레이어 사망 체크 추가 (대기 후)
            if (Player.m_localPlayer != null && Player.m_localPlayer.IsDead())
            {
                if (effect != null)
                {
                    UnityEngine.Object.Destroy(effect);
                }
                // Plugin.Log.LogInfo("[로그] 플레이어 사망으로 이펙트 조기 제거");
                yield break;
            }

            if (effect != null)
            {
                UnityEngine.Object.Destroy(effect);
            }
        }

        // === 스텔스 시스템 메서드들 ===

        /// <summary>
        /// 스텔스 상태 적용 (8초간 몬스터가 플레이어를 타겟팅하지 못함)
        /// </summary>
        private static void ApplyStealthState(Player player)
        {
            try
            {
                float stealthDuration = Rogue_Config.RogueShadowStrikeStealthDurationValue;
                
                // Plugin.Log.LogInfo($"[로그 스텔스] {player.GetPlayerName()} 스텔스 시작 ({stealthDuration}초)");
                
                // 스텔스 상태 설정
                stealthEndTime[player] = Time.time + stealthDuration;
                stealthActive[player] = true;
                
                // 플레이어에게 스텔스 시작 알림
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_stealth_start", stealthDuration.ToString()));
                
                // 첫 번째 스텔스 사용 시 스텔스 클리너 시작
                EnsureStealthCleanerRunning();
                
                // 🔧 수정: 스텔스 종료 코루틴 시작 및 Dictionary에 저장 (좀비 코루틴 방지)
                if (Plugin.Instance != null)
                {
                    var stealthCoroutine = Plugin.Instance.StartCoroutine(StealthDurationCoroutine(player, stealthDuration));
                    stealthDurationCoroutine[player] = stealthCoroutine;
                }
                
                // Plugin.Log.LogInfo($"[로그 스텔스] {player.GetPlayerName()} 스텔스 활성화 완료");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 스텔스] 스텔스 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 스텔스 클리너가 실행 중인지 확인하고 필요 시 시작
        /// </summary>
        private static bool stealthCleanerStarted = false;
        private static void EnsureStealthCleanerRunning()
        {
            if (!stealthCleanerStarted)
            {
                stealthCleanerStarted = true;
                BaseAI_Stealth_Patches.StartStealthCleaner();
                // Plugin.Log.LogInfo("[로그 스텔스] 스텔스 클리너 시작됨");
            }
        }

        /// <summary>
        /// 스텔스 지속시간 관리 코루틴
        /// </summary>
        private static IEnumerator StealthDurationCoroutine(Player player, float duration)
        {
            // Plugin.Log.LogInfo($"[로그 스텔스] {player.GetPlayerName()} 스텔스 코루틴 시작 ({duration}초)");

            yield return new WaitForSeconds(duration);

            // ✅ 플레이어 사망 체크 추가 (대기 후)
            if (player == null || player.IsDead())
            {
                // Plugin.Log.LogInfo("[로그 스텔스] 플레이어 사망으로 스텔스 코루틴 중단");
                yield break;
            }

            // 스텔스 자동 종료
            if (stealthActive.ContainsKey(player) && stealthActive[player])
            {
                RemoveStealthState(player, "시간 만료");
            }
        }

        /// <summary>
        /// 스텔스 상태 제거
        /// </summary>
        public static void RemoveStealthState(Player player, string reason = "알 수 없음")
        {
            try
            {
                if (!stealthActive.ContainsKey(player) || !stealthActive[player])
                {
                    // Plugin.Log.LogDebug($"[로그 스텔스] {player.GetPlayerName()} 스텔스 상태가 아님 - 제거 불필요");
                    return;
                }
                
                // Plugin.Log.LogInfo($"[로그 스텔스] {player.GetPlayerName()} 스텔스 해제 시작 (이유: {reason})");
                
                // 스텔스 상태 제거
                stealthActive[player] = false;
                if (stealthEndTime.ContainsKey(player))
                {
                    stealthEndTime.Remove(player);
                }
                
                // 플레이어에게 스텔스 해제 알림
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_stealth_end", reason));
                
                // Plugin.Log.LogInfo($"[로그 스텔스] {player.GetPlayerName()} 스텔스 해제 완료");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 스텔스] 스텔스 해제 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어가 현재 스텔스 상태인지 확인
        /// </summary>
        public static bool IsPlayerInStealth(Player player)
        {
            if (player == null) return false;
            
            // 활성 상태 확인
            if (!stealthActive.ContainsKey(player) || !stealthActive[player])
            {
                return false;
            }
            
            // 시간 만료 확인
            if (stealthEndTime.ContainsKey(player))
            {
                if (Time.time >= stealthEndTime[player])
                {
                    // 만료된 스텔스 자동 제거
                    RemoveStealthState(player, "시간 만료");
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// 스텔스 시스템 상태 정리 (플레이어 로그아웃 시)
        /// </summary>
        public static void CleanupStealthState(Player player)
        {
            try
            {
                if (stealthActive.ContainsKey(player))
                {
                    stealthActive.Remove(player);
                }
                if (stealthEndTime.ContainsKey(player))
                {
                    stealthEndTime.Remove(player);
                }
                
                // Plugin.Log.LogInfo($"[로그 스텔스] {player.GetPlayerName()} 스텔스 상태 정리 완료");
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 스텔스] 스텔스 상태 정리 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 로그 스킬 정리 메서드 (플레이어 사망 시 호출)
        /// 그림자 일격 버프 및 스텔스 상태 모두 정리
        /// </summary>
        public static void CleanupRogueSkillsOnDeath(Player player)
        {
            if (player == null) return;

            // 🔒 수정 6: 전체 정리 작업을 lock으로 보호 (Race Condition 방지)
            lock (rogueDictionaryLock)
            {
                try
                {
                    // 그림자 일격 공격력 버프 정리
                    rogueAttackBuffExpiry.Remove(player);

                // 🔧 수정 1: 코루틴 중단 보장 시스템
                if (rogueAttackBuffCoroutine.TryGetValue(player, out var coroutineToStop) && coroutineToStop != null)
                {
                    try
                    {
                        // Plugin.Instance가 null이어도 코루틴 중단 시도
                        if (Plugin.Instance != null)
                        {
                            Plugin.Instance.StopCoroutine(coroutineToStop);
                        }
                        else if (player != null)
                        {
                            // Plugin.Instance가 null인 경우 player의 MonoBehaviour 사용
                            player.StopCoroutine(coroutineToStop);
                        }
                        // Plugin.Log.LogInfo($"[로그 스킬] {player?.GetPlayerName()} 코루틴 중단 성공");
                    }
                    catch (Exception)
                    {
                        // Plugin.Log.LogWarning($"[로그 스킬] 코루틴 중단 실패 (무시): {ex.Message}");
                    }
                    finally
                    {
                        rogueAttackBuffCoroutine.Remove(player);  // 🔒 무조건 Dictionary 정리
                    }
                }
                else
                {
                    rogueAttackBuffCoroutine.Remove(player);
                }

                // 🧹 수정 4: VFX GameObject 정리 활성화 (메모리 누수 방지)
                if (rogueSparkleEffects.ContainsKey(player))
                {
                    try
                    {
                        var sparkleEffect = rogueSparkleEffects[player];
                        if (sparkleEffect != null)
                        {
                            UnityEngine.Object.Destroy(sparkleEffect);
                            // Plugin.Log.LogInfo($"[로그 스킬] {player?.GetPlayerName()} Sparkle Effect GameObject 제거");
                        }
                    }
                    catch (Exception)
                    {
                        // Plugin.Log.LogWarning($"[로그 스킬] Sparkle Effect 제거 실패: {ex.Message}");
                    }
                    finally
                    {
                        rogueSparkleEffects.Remove(player);  // 🔒 무조건 Dictionary 정리
                    }
                }

                    // 🔧 수정: 스텔스 코루틴 중단 (좀비 코루틴 방지)
                    if (stealthDurationCoroutine.TryGetValue(player, out var stealthCo) && stealthCo != null)
                    {
                        try
                        {
                            if (Plugin.Instance != null)
                                Plugin.Instance.StopCoroutine(stealthCo);
                            else if (player != null)
                                player.StopCoroutine(stealthCo);
                            // Plugin.Log.LogInfo($"[로그 스킬] {player?.GetPlayerName()} 스텔스 코루틴 중단 성공");
                        }
                        catch (Exception)
                        {
                            // Plugin.Log.LogWarning($"[로그 스킬] 스텔스 코루틴 중단 실패: {ex.Message}");
                        }
                        finally
                        {
                            stealthDurationCoroutine.Remove(player);
                        }
                    }
                    else
                    {
                        stealthDurationCoroutine.Remove(player);
                    }

                    // 스텔스 상태 정리
                    stealthEndTime.Remove(player);
                    stealthActive.Remove(player);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[Rogue Skills] 정리 실패: {ex.Message}");
                }
            } // lock 종료
        }

        #region === 버프 VFX 시스템 ===

        /// <summary>
        /// 로그 버프 VFX 생성 (머리 위 1.2m)
        /// </summary>
        private static void CreateRogueBuffVFX(Player player)
        {
            try
            {
                // 기존 VFX 제거
                RemoveRogueBuffVFX(player);

                // 새 VFX 생성 (머리 위 1.2m, 무한 지속)
                var vfx = SimpleVFX.PlayOnPlayer(player, "statusailment_01_aura", 9999f, new Vector3(0f, 1.2f, 0f));
                if (vfx != null)
                {
                    rogueBuffVFXInstances[player] = vfx;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[로그 버프 VFX] 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 로그 버프 VFX 제거
        /// </summary>
        private static void RemoveRogueBuffVFX(Player player)
        {
            try
            {
                if (rogueBuffVFXInstances.TryGetValue(player, out var vfx) && vfx != null)
                {
                    UnityEngine.Object.Destroy(vfx);
                }
                rogueBuffVFXInstances.Remove(player);
            }
            catch (Exception)
            {
                // 조용히 무시
            }
        }

        #endregion

    }

    /// <summary>
    /// 로그 공격력 버프 적용을 위한 Character.Damage 패치
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_RogueAttackBuff_Patch
    {
        public static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                // 공격자가 플레이어인지 확인
                if (hit.GetAttacker() is Player player)
                {
                    // 스텔스 상태 확인 및 공격 시 스텔스 해제
                    if (RogueSkills.IsPlayerInStealth(player))
                    {
                        RogueSkills.RemoveStealthState(player, "공격");
                        // Plugin.Log.LogInfo($"[로그 스텔스] {player.GetPlayerName()} 공격으로 인한 스텔스 해제");
                    }
                    
                    // 로그 공격력 버프 활성 확인
                    if (RogueSkills.IsRogueAttackBuffActive(player))
                    {
                        float buffMultiplier = RogueSkills.GetRogueAttackBuffMultiplier(player);
                        
                        // 모든 데미지 타입에 버프 적용
                        hit.m_damage.m_blunt *= buffMultiplier;
                        hit.m_damage.m_slash *= buffMultiplier;
                        hit.m_damage.m_pierce *= buffMultiplier;
                        hit.m_damage.m_chop *= buffMultiplier;
                        hit.m_damage.m_pickaxe *= buffMultiplier;
                        hit.m_damage.m_fire *= buffMultiplier;
                        hit.m_damage.m_frost *= buffMultiplier;
                        hit.m_damage.m_lightning *= buffMultiplier;
                        hit.m_damage.m_poison *= buffMultiplier;
                        hit.m_damage.m_spirit *= buffMultiplier;
                        
                        float attackBonus = Rogue_Config.RogueShadowStrikeAttackBonusValue;
                        // Plugin.Log.LogDebug($"[로그 공격력 버프] {player.GetPlayerName()} 공격력 +{attackBonus}% 적용됨");
                    }
                }
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 공격력 버프] 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 로그 패시브 - 속성 저항 증가 패치
    /// 로그가 받는 속성 피해(불/냉기/번개/독/정령)를 감소시킴 (장점)
    /// </summary>
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class RoguePassivePatch
    {
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                // 피격 대상이 플레이어이고 로그인 경우만 처리
                if (!(__instance is Player player)) return;
                if (!RogueSkills.IsRogue(player)) return;

                float resist = Rogue_Config.RogueElementalResistanceDebuffValue / 100f;

                // 속성 피해 감소 (저항 증가 = 덜 받음)
                hit.m_damage.m_fire      *= (1f - resist);
                hit.m_damage.m_frost     *= (1f - resist);
                hit.m_damage.m_lightning *= (1f - resist);
                hit.m_damage.m_poison    *= (1f - resist);
                hit.m_damage.m_spirit    *= (1f - resist);
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 패시브] 속성 저항 패치 오류: {ex.Message}");
            }
        }
    }

    // [로그 패시브] 공격 속도 보너스: Player.GetAttackSpeedFactor는 현재 Valheim에 없음.
    // → 공격속도 보너스는 GetTotalAttackSpeedBonus() (SkillEffect.SpeedTree.cs)에서 처리됨.

    /// <summary>
    /// 로그 패시브 - 스태미나 사용량 감소 패치
    /// Player.UseStamina에서 공격 스태미나 -15% 적용
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
    public static class RogueStaminaReductionPatch
    {
        static void Prefix(Player __instance, ref float v)
        {
            try
            {
                if (!RogueSkills.IsRogue(__instance)) return;
                if (!__instance.InAttack()) return; // 공격 시에만 스태미나 감소 적용
                float reduction = Rogue_Config.RogueStaminaReductionValue / 100f;
                v *= (1f - reduction);
            }
            catch (System.Exception)
            {
                // Plugin.Log.LogError($"[로그 패시브] 스태미나 감소 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 통합 이동속도 보너스 패치 - 배율 곱셈 방식 (v0.1.226+)
    /// 공식: 최종 속도 = (발헤임 기본 + 다른 모드) × (1 + 스킬트리 보너스%)
    /// </summary>
    [HarmonyPatch(typeof(Player), "GetJogSpeedFactor")]
    public static class ImprovedMoveSpeedPatch
    {
        // 경고 표시 여부 추적 (플레이어당 한 번만 표시)
        private static Dictionary<Player, bool> _moveSpeedWarningShown = new Dictionary<Player, bool>();

        [HarmonyPostfix]
        [HarmonyPriority(Priority.Last)] // 다른 모드 이후 실행
        public static void Postfix(Player __instance, ref float __result)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                // 1. 현재 속도 = (발헤임 기본 + 다른 모드들)
                float baseSpeed = __result;

                // 2. 스킬트리 보너스 계산 (백분율 누적)
                float totalBonus = 0f;

                // speed_root: 속도 전문가 +5%
                if (manager.GetSkillLevel("speed_root") > 0)
                {
                    totalBonus += SkillTreeConfig.SpeedRootMoveSpeedValue;
                    Plugin.Log.LogDebug($"[이동속도] speed_root: +{SkillTreeConfig.SpeedRootMoveSpeedValue}%");
                }

                // speed_1: 민첩 +이동속도%
                if (manager.GetSkillLevel("speed_1") > 0)
                {
                    totalBonus += SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue;
                    Plugin.Log.LogDebug($"[이동속도] speed_1 민첩: +{SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue}%");
                }

                // knife_step3_move_speed: 단검 빠른 움직임 +5% (단검 착용 시만)
                if (manager.GetSkillLevel("knife_step3_move_speed") > 0 && WeaponHelper.IsUsingDagger(__instance))
                {
                    totalBonus += Knife_Config.KnifeMoveSpeedBonusValue;
                    Plugin.Log.LogDebug($"[이동속도] 단검 빠른 움직임: +{Knife_Config.KnifeMoveSpeedBonusValue}%");
                }

                // 조건부 보너스 (구르기, 석궁 숙련자 등, 요툰의 방패 포함) - Speed.cs에서 처리
                float conditionalBonus = Speed.GetConditionalSpeedBonus(__instance);
                if (conditionalBonus > 0f)
                {
                    totalBonus += conditionalBonus * 100f; // 백분율로 변환
                    Plugin.Log.LogDebug($"[이동속도] 조건부 보너스: +{conditionalBonus * 100f:F1}%");
                }

                // 3. 최대치 제한 적용
                float maxBonus = SkillTreeConfig.MoveSpeedMaxBonusValue;
                if (totalBonus > maxBonus)
                {
                    // 한 번만 경고 (플레이어당)
                    if (!_moveSpeedWarningShown.ContainsKey(__instance) || !_moveSpeedWarningShown[__instance])
                    {
                        Plugin.Log.LogWarning($"[이동속도] {__instance.GetPlayerName()} 보너스 제한: {totalBonus:F1}% → {maxBonus}%");

                        // 화면 메시지 표시
                        __instance.Message(MessageHud.MessageType.Center,
                            $"이동속도 제한을 {maxBonus}%를 넘길 수 없습니다.");

                        _moveSpeedWarningShown[__instance] = true;
                    }

                    totalBonus = maxBonus;
                }

                // 4. 배율 적용 (핵심!)
                if (totalBonus > 0f)
                {
                    __result = baseSpeed * (1f + totalBonus / 100f);
                    Plugin.Log.LogDebug(
                        $"[이동속도] {__instance.GetPlayerName()}: " +
                        $"기본={baseSpeed:F3} × (1+{totalBonus:F1}%) = {__result:F3}"
                    );
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이동속도 패치] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어 로그아웃/사망 시 경고 상태 정리
        /// </summary>
        public static void ClearWarningState(Player player)
        {
            if (_moveSpeedWarningShown.ContainsKey(player))
                _moveSpeedWarningShown.Remove(player);
        }

    }

    /// <summary>
    /// 달리기(Shift키) 이동속도 보너스 패치 - GetJogSpeedFactor와 동일한 보너스 적용
    /// </summary>
    [HarmonyPatch(typeof(Player), "GetRunSpeedFactor")]
    public static class ImprovedRunSpeedPatch
    {
        [HarmonyPostfix]
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(Player __instance, ref float __result)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                float baseSpeed = __result;
                float totalBonus = 0f;

                // speed_root: 속도 전문가 +5%
                if (manager.GetSkillLevel("speed_root") > 0)
                    totalBonus += SkillTreeConfig.SpeedRootMoveSpeedValue;

                // speed_1: 민첩 +이동속도%
                if (manager.GetSkillLevel("speed_1") > 0)
                    totalBonus += SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue;

                // knife_step3_move_speed: 단검 착용 시만
                if (manager.GetSkillLevel("knife_step3_move_speed") > 0 && WeaponHelper.IsUsingDagger(__instance))
                    totalBonus += Knife_Config.KnifeMoveSpeedBonusValue;

                // 조건부 보너스 (구르기, 석궁 숙련자, 요툰의 방패 등) - Speed.cs에서 처리
                float conditionalBonus = Speed.GetConditionalSpeedBonus(__instance);
                if (conditionalBonus > 0f)
                    totalBonus += conditionalBonus * 100f;

                // 최대치 제한
                float maxBonus = SkillTreeConfig.MoveSpeedMaxBonusValue;
                if (totalBonus > maxBonus)
                    totalBonus = maxBonus;

                if (totalBonus > 0f)
                    __result = baseSpeed * (1f + totalBonus / 100f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[달리기속도 패치] 오류: {ex.Message}");
            }
        }
    }
}
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
    /// 탱커 직업 전용 스킬 시스템
    /// Y키로 발동되는 탱커 도발 스킬 관리
    /// </summary>
    public static class TankerSkills
    {
        // ✅ 플래그 기반 피해 감소 (코루틴 대체)
        private static HashSet<long> tankerDamageReductionActive = new HashSet<long>();
        private static Dictionary<long, float> tankerDamageReductionEndTime = new Dictionary<long, float>();

        /// <summary>
        /// 탱커 스킬을 SkillTreeManager에 등록
        /// </summary>
        public static void RegisterTankerSkill()
        {
            var manager = SkillTreeManager.Instance;
            
            // 탱커 직업 스킬 등록
            manager.AddSkill(new SkillNode {
                Id = "Tanker",
                Name = "탱커",
                Description = Tanker_Tooltip.GetTankerTooltip(), // 컨피그 연동 동적 툴팁
                RequiredPoints = 0,
                MaxLevel = 1,
                Tier = 7,
                Position = new Vector2(550, 180),
                Category = "직업",
                IconName = "Tanker_unlock",
                IconNameLocked = "Tanker_lock",
                IconNameUnlocked = "Tanker_unlock",
                NextNodes = new List<string>(),
                RequiredPlayerLevel = 10,
                ApplyEffect = (lv) => { }
            });
        }

        /// <summary>
        /// Y키 탱커 도발 스킬 실행
        /// </summary>
        public static void ExecuteTankerTaunt(Player player)
        {
            if (player == null)
            {
                Plugin.Log.LogWarning("[탱커 도발] 플레이어가 null입니다");
                return;
            }

            // 쿨다운 체크
            if (IsOnCooldown(player, "Tanker"))
            {
                ShowCooldownMessage(player, "Tanker");
                return;
            }

            // 방패 착용 체크
            if (!IsWearingShield(player))
            {
                ShowRequirementMessage(player, L.Get("tanker_shield_required"));
                return;
            }

            // 스태미나 체크
            float requiredStamina = Tanker_Config.TankerTauntStaminaCostValue;
            if (player.GetStamina() < requiredStamina)
            {
                ShowRequirementMessage(player, L.Get("stamina_insufficient"));
                return;
            }

            Plugin.Log.LogDebug($"[탱커 도발] {player.GetPlayerName()} 도발 스킬 시작");

            try
            {
                // 스태미나 소모
                player.UseStamina(requiredStamina);

                // 주변 몬스터 도발
                int taunted = TauntNearbyEnemies(player);

                // 쿨다운 설정
                SetCooldown(player, "Tanker", Tanker_Config.TankerTauntCooldownValue);

                // 스킬 발동 효과
                PlayTankerEffects(player);
                
                // 탱커 방어 버프 시작 (피해 감소 + 지속 이펙트)
                StartTankerDefenseBuff(player);

                Plugin.Log.LogDebug($"[탱커 도발] {player.GetPlayerName()} 도발 완료 - {taunted}마리 도발");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[탱커 도발] 스킬 실행 중 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 주변 몬스터들에게 도발 효과 적용
        /// </summary>
        private static int TauntNearbyEnemies(Player player)
        {
            int taunted = 0;
            float tauntRange = Tanker_Config.TankerTauntRangeValue;

            // 주변 적들 찾기
            var nearbyEnemies = Character.GetAllCharacters()
                .Where(c => c != null && !c.IsDead() && c != player && JobSkillsUtility.IsMonsterFaction(c.GetFaction()))
                .Where(c => Vector3.Distance(player.transform.position, c.transform.position) <= tauntRange)
                .ToList();

            Plugin.Log.LogDebug($"[탱커 도발] 범위 {tauntRange}m 내 {nearbyEnemies.Count}마리 몬스터 발견");

            foreach (var enemy in nearbyEnemies)
            {
                if (enemy == null) continue;

                var ai = enemy.GetComponent<BaseAI>();
                if (ai != null)
                {
                    try
                    {
                        bool isBoss = IsBossMonster(enemy);
                        float duration = isBoss ? Tanker_Config.TankerTauntBossDurationValue : Tanker_Config.TankerTauntDurationValue;

                        string enemyName = enemy.name ?? "Unknown";
                        Plugin.Log.LogDebug($"[탱커 도발] {enemyName} 도발 시도 (보스: {isBoss}, 지속시간: {duration}초)");

                        // 안전한 무시각 도발 시스템 적용
                        if (SafeInvisibleTaunt(player, enemy, ai))
                        {
                            // ✅ Harmony AI 패치에 몬스터 등록 (매 프레임 강제 타겟)
                            TankerTauntAIPatch.AddTauntedMonster(enemy, player, duration);

                            // ❌ 안전한 어그로 유지 시스템 비활성화 (AI 패치가 이미 매 프레임 강제하므로 불필요)
                            // if (Plugin.Instance != null)
                            // {
                            //     Plugin.Instance.StartCoroutine(SafeAggroMaintenance(player, enemy, ai, duration, isBoss));
                            // }

                            // 시각적 도발 효과 표시
                            ShowTauntEffectOnMonster(enemy, duration);

                            taunted++;
                            Plugin.Log.LogDebug($"[탱커 도발] {enemyName} 도발 성공! (총 {taunted}마리, 지속: {duration}초)");
                        }
                        else
                        {
                            // ✅ AI 패치가 매 프레임 강제하므로 경고하지 않음
                            Plugin.Log.LogDebug($"[탱커 도발] {enemyName} 초기 타겟 설정 실패 - AI 패치가 강제 유지");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Plugin.Log.LogError($"[탱커 도발] {enemy.name ?? "Unknown"} 도발 중 예외 발생: {ex.Message}");
                    }
                }
                else
                {
                    Plugin.Log.LogWarning($"[탱커 도발] {enemy.name ?? "Unknown"}에 BaseAI 컴포넌트가 없음");
                }
            }

            return taunted;
        }

        /// <summary>
        /// 강화된 안전한 도발 시스템 - 다른 유저 어그로도 강제로 탈취하는 강력한 도발
        /// </summary>
        private static bool SafeInvisibleTaunt(Player tanker, Character enemy, BaseAI ai)
        {
            if (tanker == null || enemy == null || ai == null)
            {
                Plugin.Log.LogWarning("[안전한 도발] null 객체 감지 - 도발 중단");
                return false;
            }
            
            string enemyName = enemy.name ?? "Unknown";
            Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 강화된 도발 시작 - 탱커: {tanker.GetPlayerName()}");
            
            try
            {
                // 0단계: 현재 어그로 상태 확인 및 강제 탈취 준비
                Character currentTarget = GetCurrentTarget(ai);
                string currentTargetInfo = GetCharacterDisplayName(currentTarget);
                Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 현재 타겟: {currentTargetInfo}");
                
                if (currentTarget != null && currentTarget != tanker)
                {
                    Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 다른 유저({currentTargetInfo})로부터 어그로 강제 탈취 시도");
                    
                    // 기존 어그로 완전 초기화 (다른 유저 어그로 해제)
                    ForceRemoveExistingAggro(enemy, ai, currentTarget, enemyName);
                }
                
                // 1단계: 직접적인 적대 관계 설정 (가장 확실한 방법)
                Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 적대 관계 직접 설정");
                
                // MonsterAI나 BaseAI의 SetHuntPlayer 직접 호출
                var monsterAI = ai as MonsterAI;
                if (monsterAI != null)
                {
                    try
                    {
                        // MonsterAI.SetHuntPlayer 호출 - 가장 효과적인 도발
                        var setHuntMethod = typeof(MonsterAI).GetMethod("SetHuntPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (setHuntMethod != null)
                        {
                            setHuntMethod.Invoke(monsterAI, new object[] { tanker });
                            Plugin.Log.LogDebug($"[안전한 도발] {enemyName} SetHuntPlayer 호출 성공");
                        }
                    }
                    catch (System.Exception huntEx)
                    {
                        Plugin.Log.LogWarning($"[안전한 도발] {enemyName} SetHuntPlayer 실패: {huntEx.Message}");
                    }
                }
                
                // 2단계: 리플렉션으로 SetTarget 호출 (백업)
                try
                {
                    var setTargetMethod = ai.GetType().GetMethod("SetTarget", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (setTargetMethod != null)
                    {
                        setTargetMethod.Invoke(ai, new object[] { tanker });
                        Plugin.Log.LogDebug($"[안전한 도발] {enemyName} SetTarget 호출 성공");
                    }
                    else
                    {
                        Plugin.Log.LogWarning($"[안전한 도발] {enemyName} SetTarget 메서드를 찾을 수 없음");
                    }
                }
                catch (System.Exception targetEx)
                {
                    Plugin.Log.LogWarning($"[안전한 도발] {enemyName} SetTarget 실패: {targetEx.Message}");
                }
                
                // 3단계: 물리적 어그로 생성 제거 (AI 패치가 매 프레임 강제하므로 불필요)
                // ❌ Character.Damage() 호출을 완전히 제거하여 히트 반응 방지
                // TankerTauntAIPatch가 매 프레임 강제로 타겟을 설정하므로 물리적 데미지 불필요
                Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 물리적 데미지 없이 도발 적용 (AI 패치 전용)");

                // 기존 물리적 어그로 생성 코드 제거 (히트 반응 유발 방지)
                // var hitData = new HitData();
                // hitData.m_attacker = tanker.GetZDOID();
                // hitData.m_damage.m_blunt = 0.01f;
                // ...
                // enemy.Damage(hitData);
                
                // 4단계: 추가적인 강제 어그로 확보 (다른 유저 어그로 완전 덮어쓰기)
                try
                {
                    ForceOverrideAggro(enemy, ai, tanker, enemyName);
                    Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 강제 어그로 덮어쓰기 완료");
                }
                catch (System.Exception overrideEx)
                {
                    Plugin.Log.LogWarning($"[안전한 도발] {enemyName} 강제 어그로 덮어쓰기 실패: {overrideEx.Message}");
                }
                
                // 5단계: 즉시 시야 설정으로 반응 보장
                try
                {
                    Vector3 directionToTanker = (tanker.transform.position - enemy.transform.position).normalized;
                    enemy.SetLookDir(directionToTanker, 0f);
                    Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 시야 방향 설정 완료");
                }
                catch (System.Exception lookEx)
                {
                    Plugin.Log.LogWarning($"[안전한 도발] {enemyName} 시야 설정 실패: {lookEx.Message}");
                }
                
                // 6단계: 최종 도발 확인 (성공으로 처리, AI 패치가 매 프레임 강제함)
                Character finalTarget = GetCurrentTarget(ai);
                string finalTargetInfo = GetCharacterDisplayName(finalTarget);
                Plugin.Log.LogDebug($"[안전한 도발] {enemyName} 최종 타겟 확인: {finalTargetInfo}");

                // ✅ AI 패치가 매 프레임 강제로 타겟을 설정하므로 즉시 성공 여부와 관계없이 true 반환
                Plugin.Log.LogDebug($"[✅ 강화된 도발] {enemyName} 도발 등록 완료 - AI 패치가 매 프레임 강제 타겟 유지");
                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[❌ 강화된 도발] {enemyName} 처리 실패: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 현재 몬스터의 타겟 가져오기 (리플렉션 사용)
        /// </summary>
        private static Character GetCurrentTarget(BaseAI ai)
        {
            try
            {
                var getTargetMethod = ai.GetType().GetMethod("GetTarget", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (getTargetMethod != null)
                {
                    return getTargetMethod.Invoke(ai, null) as Character;
                }
                
                // 백업: 리플렉션으로 m_target 필드 직접 접근
                var targetField = ai.GetType().GetField("m_target", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (targetField != null)
                {
                    return targetField.GetValue(ai) as Character;
                }
                
                return null;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[현재 타겟] 가져오기 실패: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 기존 어그로 완전 초기화 (다른 유저로부터 어그로 해제)
        /// </summary>
        private static void ForceRemoveExistingAggro(Character enemy, BaseAI ai, Character previousTarget, string enemyName)
        {
            try
            {
                Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} 기존 어그로 해제 시작 - 이전 타겟: {GetCharacterDisplayName(previousTarget)}");
                
                // 1. 타겟 완전 해제
                try
                {
                    var setTargetMethod = ai.GetType().GetMethod("SetTarget", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (setTargetMethod != null)
                    {
                        setTargetMethod.Invoke(ai, new object[] { null });
                        Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} SetTarget(null) 호출 성공");
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogWarning($"[어그로 초기화] {enemyName} SetTarget(null) 실패: {ex.Message}");
                }
                
                // 2. MonsterAI 특수 처리 (Hunt 상태 해제)
                var monsterAI = ai as MonsterAI;
                if (monsterAI != null)
                {
                    try
                    {
                        // Hunt 상태를 null로 설정
                        var setHuntMethod = typeof(MonsterAI).GetMethod("SetHuntPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (setHuntMethod != null)
                        {
                            setHuntMethod.Invoke(monsterAI, new object[] { null });
                            Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} SetHuntPlayer(null) 호출 성공");
                        }
                        
                        // m_hunt 필드를 직접 null로 설정
                        var huntField = typeof(MonsterAI).GetField("m_hunt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (huntField != null)
                        {
                            huntField.SetValue(monsterAI, null);
                            Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} m_hunt 필드 null 설정 성공");
                        }
                        
                        // AI 상태도 리셋 (Idle로 강제 변경)
                        var stateField = typeof(BaseAI).GetField("m_currentState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (stateField != null)
                        {
                            // Idle 상태로 변경하여 완전히 리셋
                            stateField.SetValue(ai, null); // null로 설정하면 기본 상태로 돌아감
                            Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} AI 상태 리셋 완료");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Plugin.Log.LogWarning($"[어그로 초기화] {enemyName} MonsterAI 처리 실패: {ex.Message}");
                    }
                }
                
                // 3. 기본 상태 리셋
                try
                {
                    // 알림 상태 해제
                    var alertedField = ai.GetType().GetField("m_alerted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (alertedField != null)
                    {
                        alertedField.SetValue(ai, false);
                        Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} alerted 상태 해제");
                    }
                    
                    // 마지막 데미지 시간 리셋
                    var lastDamageField = ai.GetType().GetField("m_lastDamageTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (lastDamageField != null)
                    {
                        lastDamageField.SetValue(ai, 0f);
                        Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} lastDamageTime 리셋");
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogWarning($"[어그로 초기화] {enemyName} 상태 리셋 실패: {ex.Message}");
                }
                
                Plugin.Log.LogDebug($"[어그로 초기화] {enemyName} 기존 어그로 완전 해제 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[어그로 초기화] {enemyName} 처리 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 강제 어그로 덮어쓰기 (탱커로 강제 설정)
        /// </summary>
        private static void ForceOverrideAggro(Character enemy, BaseAI ai, Player tanker, string enemyName)
        {
            try
            {
                Plugin.Log.LogDebug($"[어그로 덮어쓰기] {enemyName} 탱커로 강제 어그로 덮어쓰기 시작");
                
                // 1. 직접 타겟 설정 (여러 방법 동시 사용)
                var setTargetMethod = ai.GetType().GetMethod("SetTarget", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (setTargetMethod != null)
                {
                    setTargetMethod.Invoke(ai, new object[] { tanker });
                }
                
                // 2. m_target 필드 직접 설정
                var targetField = ai.GetType().GetField("m_target", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (targetField != null)
                {
                    targetField.SetValue(ai, tanker);
                }
                
                // 3. m_targetCreature 필드도 설정
                var targetCreatureField = ai.GetType().GetField("m_targetCreature", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (targetCreatureField != null)
                {
                    targetCreatureField.SetValue(ai, tanker);
                }
                
                // 4. MonsterAI 특수 처리
                var monsterAI = ai as MonsterAI;
                if (monsterAI != null)
                {
                    // SetHuntPlayer로 강제 설정
                    var setHuntMethod = typeof(MonsterAI).GetMethod("SetHuntPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (setHuntMethod != null)
                    {
                        setHuntMethod.Invoke(monsterAI, new object[] { tanker });
                    }
                    
                    // m_hunt 필드 직접 설정
                    var huntField = typeof(MonsterAI).GetField("m_hunt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (huntField != null)
                    {
                        huntField.SetValue(monsterAI, tanker);
                    }
                    
                    // Alert 메서드도 호출하여 즉각적인 반응 유도
                    try
                    {
                        var alertMethod = typeof(BaseAI).GetMethod("Alert", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (alertMethod != null)
                        {
                            alertMethod.Invoke(ai, null);
                            Plugin.Log.LogDebug($"[어그로 덮어쓰기] {enemyName} Alert 호출 성공");
                        }
                    }
                    catch (System.Exception alertEx)
                    {
                        Plugin.Log.LogWarning($"[어그로 덮어쓰기] {enemyName} Alert 호출 실패: {alertEx.Message}");
                    }
                }
                
                // 5. 알림 상태 활성화
                var alertedField = ai.GetType().GetField("m_alerted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (alertedField != null)
                {
                    alertedField.SetValue(ai, true);
                }
                
                // 6. 연속 물리적 어그로 생성 제거 (AI 패치가 매 프레임 강제하므로 불필요)
                // ❌ 5회 연속 데미지도 제거하여 히트 반응 완전 방지
                // TankerTauntAIPatch가 매 프레임 강제로 타겟을 설정하므로 연속 데미지 불필요
                Plugin.Log.LogDebug($"[어그로 덮어쓰기] {enemyName} 연속 데미지 없이 타겟 설정만 수행 (AI 패치 전용)");

                // 기존 연속 물리적 어그로 생성 코드 제거 (히트 반응 유발 방지)
                // for (int i = 0; i < 5; i++)
                // {
                //     var consecutiveHitData = new HitData();
                //     ...
                //     enemy.Damage(consecutiveHitData);
                // }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[어그로 덮어쓰기] {enemyName} 처리 실패: {ex.Message}");
            }
        }

        // ❌ SafeAggroMaintenance 코루틴 제거됨 - AI 패치가 매 프레임 강제 타겟 유지
        // ❌ SafeMaintainAggro 제거됨 - 코루틴 종속 함수

        /// <summary>
        /// 보스 몬스터 여부 확인
        /// </summary>
        private static bool IsBossMonster(Character enemy)
        {
            try
            {
                string enemyName = enemy.name?.ToLower() ?? "";
                
                // 보스 몬스터 이름 패턴
                string[] bossPatterns = {
                    "eikthyr", "elder", "bonemass", "moder", "yagluth", "queen",
                    "boss", "dragon", "giantslime"
                };
                
                return bossPatterns.Any(pattern => enemyName.Contains(pattern));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 방패 착용 여부 확인 - Valheim 내부 시스템 활용한 정확한 감지
        /// </summary>
        private static bool IsWearingShield(Player player)
        {
            try
            {
                Plugin.Log.LogDebug("[Tanker 방패 체크] 시작");
                
                // 1. 현재 활성 무기가 방패인지 확인 (주무기 또는 보조무기)
                var currentWeapon = player.GetCurrentWeapon();
                if (currentWeapon != null && currentWeapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                {
                    Plugin.Log.LogDebug($"[Tanker 방패 체크] ✅ 현재 활성 방패: {currentWeapon.m_shared.m_name}");
                    return true;
                }
                
                // 2. 인벤토리 전체에서 장착된 방패 검색
                var inventory = player.GetInventory();
                if (inventory != null)
                {
                    var equippedShields = inventory.GetAllItems()
                        .Where(item => item != null && 
                                     item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield &&
                                     item.m_equipped)
                        .ToList();
                    
                    if (equippedShields.Any())
                    {
                        var shield = equippedShields.First();
                        Plugin.Log.LogDebug($"[Tanker 방패 체크] ✅ 인벤토리 장착 방패: {shield.m_shared.m_name}");
                        return true;
                    }
                }
                
                Plugin.Log.LogWarning("[Tanker 방패 체크] ❌ 방패를 찾을 수 없음");
                Plugin.Log.LogDebug($"[Tanker 방패 체크] 현재 무기: {currentWeapon?.m_shared.m_name ?? "없음"}");
                
                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Tanker 방패 체크] 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 몬스터 도발 효과 표시 - 순수 Instantiate만 사용
        /// ✅ 발헤임 기본 VFX는 SetParent/Destroy/코루틴 금지!
        /// </summary>
        private static void ShowTauntEffectOnMonster(Character monster, float duration)
        {
            if (monster == null) return;

            try
            {
                string monsterName = monster.name ?? "Unknown";
                float dynamicHeight = CalculateMonsterHeight(monster);
                Vector3 headPosition = monster.transform.position + Vector3.up * dynamicHeight;

                Plugin.Log.LogDebug($"[Tanker 도발] {monsterName} 머리 위 도발 효과 생성 - 높이: {dynamicHeight}m");

                // taunt 프리팹 - 순수 Instantiate만 (발헤임이 알아서 정리)
                var znetScene = ZNetScene.instance;
                if (znetScene != null)
                {
                    var tauntVFX = znetScene.GetPrefab("taunt");
                    if (tauntVFX != null)
                    {
                        // ✅ 순수 Instantiate만 - SetParent/Destroy 금지!
                        UnityEngine.Object.Instantiate(tauntVFX, headPosition, Quaternion.identity);
                        Plugin.Log.LogDebug($"[taunt 이펙트] {monsterName} 순수 표현 완료");
                        return;
                    }
                }

                // 프리팹 없으면 메시지만
                Plugin.Log.LogDebug($"[taunt 이펙트] {monsterName} taunt 프리팹 없음");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[도발 이펙트] 생성 실패: {ex.Message}");
            }
        }

        // ❌ FollowMonsterEffect 코루틴 제거됨 - SetParent/Destroy 사용으로 무한 로딩 유발
        // ❌ ShowFallbackTauntEffect 제거됨 - 발헤임 기본 VFX 순수 표현으로 대체

        /// <summary>
        /// <summary>
        /// 몬스터 크기에 따른 동적 높이 계산
        /// </summary>
        private static float CalculateMonsterHeight(Character monster)
        {
            try
            {
                string monsterName = monster.name?.ToLower() ?? "";
                float baseHeight = Tanker_Config.TankerTauntEffectHeightValue; // 기본 2.0m
                
                // 1단계: 몬스터 머리 위 더 높은 위치에 나타나도록 높이 조정 (이전보다 +1.0m 추가)
                if (monsterName.Contains("troll"))
                {
                    return baseHeight + 2.5f; // 트롤 = 4.5m (머리 위 더 높게)
                }
                else if (monsterName.Contains("giant") || monsterName.Contains("stone_golem"))
                {
                    return baseHeight + 2.2f; // 거대한 몬스터 = 4.2m (머리 위 더 높게)
                }
                else if (monsterName.Contains("drake") || monsterName.Contains("dragon"))
                {
                    return baseHeight + 2.0f; // 비행 몬스터 = 4.0m (머리 위 더 높게)
                }
                else if (monsterName.Contains("wolf") || monsterName.Contains("boar") || monsterName.Contains("deer"))
                {
                    return baseHeight + 1.2f; // 중형 동물 = 3.2m (머리 위 더 높게)
                }
                else if (monsterName.Contains("neck") || monsterName.Contains("greyling") || monsterName.Contains("skeleton"))
                {
                    return baseHeight + 1.0f; // 소형 몬스터 = 3.0m (머리 위 더 높게)
                }
                
                // 2단계: 몬스터의 실제 바운딩 박스 기반 계산 (백업) - 더 높게 조정
                try
                {
                    var collider = monster.GetComponent<Collider>();
                    if (collider != null && collider.bounds.size.y > 0.1f)
                    {
                        float colliderHeight = collider.bounds.size.y;
                        float calculatedHeight = colliderHeight * 1.5f + 1.0f; // 몬스터 높이의 1.5배 + 1.0m 추가 (더 높게)
                        
                        // 더 높은 최소/최대 높이 제한
                        calculatedHeight = Mathf.Clamp(calculatedHeight, baseHeight + 1.0f, baseHeight + 5.0f);
                        
                        Plugin.Log.LogDebug($"[높이 계산] {monsterName} 바운딩 박스 기반 높이: {calculatedHeight}m (콜라이더: {colliderHeight}m)");
                        return calculatedHeight;
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogDebug($"[높이 계산] {monsterName} 바운딩 박스 계산 실패: {ex.Message}");
                }
                
                // 3단계: 기본 높이 반환 (더 높게 조정)
                float adjustedBaseHeight = baseHeight + 1.0f; // 기본 높이에 1.0m 추가
                Plugin.Log.LogDebug($"[높이 계산] {monsterName} 조정된 기본 높이 사용: {adjustedBaseHeight}m (원래: {baseHeight}m)");
                return adjustedBaseHeight;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[높이 계산] 오류 발생: {ex.Message}");
                return Tanker_Config.TankerTauntEffectHeightValue + 1.0f; // 기본값 + 1.0m 반환
            }
        }
        
        // ❌ CalculateEffectScale 제거됨 - 코루틴 종속 함수
        // ❌ ShowFallbackTauntEffect 제거됨 - 사용되지 않음

        /// <summary>
        /// 탱커 방어 버프 시작 - 피해 감소만 적용 (VFX는 PlayTankerEffects에서 fx_Fader_Spin만 사용)
        /// </summary>
        private static void StartTankerDefenseBuff(Player player)
        {
            try
            {
                float buffDuration = Tanker_Config.TankerTauntBuffDurationValue;
                Plugin.Log.LogDebug($"[탱커 방어 버프] {player.GetPlayerName()} 방어 버프 시작 - {buffDuration}초간 피해 {Tanker_Config.TankerTauntDamageReductionValue}% 감소");

                // 피해 감소 버프 적용
                ApplyTankerDamageReduction(player, buffDuration);

                // 텍스트 알림
                player.Message(MessageHud.MessageType.Center, L.Get("tanker_defense_buff_activated"));

                Plugin.Log.LogDebug($"[탱커 방어 버프] {player.GetPlayerName()} 방어 버프 적용 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[탱커 방어 버프] 버프 적용 실패: {ex.Message}");
            }
        }

        // ❌ TankerDefenseBuffEffect 코루틴 제거됨 - fx_guardstone_permitted_add + fx_Fader_Spin 동시 사용 시 무한 로딩
        // ❌ PlayFallbackTankerVFX 제거됨 - 더 이상 사용되지 않음

        /// <summary>
        /// 탱커 피해 감소 적용 (플래그 기반 시스템 - 코루틴 없음)
        /// </summary>
        private static void ApplyTankerDamageReduction(Player player, float duration)
        {
            if (player == null) return;

            long playerId = player.GetPlayerID();

            // ✅ 플래그 기반: 코루틴 없이 시간 기반 관리
            tankerDamageReductionActive.Add(playerId);
            tankerDamageReductionEndTime[playerId] = Time.time + duration;

            Plugin.Log.LogDebug($"[탱커 피해 감소] {player.GetPlayerName()} 피해 감소 활성화 - {duration}초간");
        }

        /// <summary>
        /// 탱커 피해 감소 활성 여부 확인 (외부에서 사용)
        /// </summary>
        public static bool IsTankerDamageReductionActive(Player player)
        {
            if (player == null) return false;

            long playerId = player.GetPlayerID();

            // 활성화 상태가 아니면 false
            if (!tankerDamageReductionActive.Contains(playerId)) return false;

            // 시간 만료 체크
            if (tankerDamageReductionEndTime.TryGetValue(playerId, out float endTime))
            {
                if (Time.time > endTime)
                {
                    // 만료됨 - 정리
                    ResetTankerDamageReduction(player);
                    return false;
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// 탱커 피해 감소 초기화
        /// </summary>
        public static void ResetTankerDamageReduction(Player player)
        {
            if (player == null) return;

            long playerId = player.GetPlayerID();
            tankerDamageReductionActive.Remove(playerId);
            tankerDamageReductionEndTime.Remove(playerId);
        }

        /// <summary>
        /// 탱커 스킬 발동 효과 - fx_Fader_Spin VFX
        /// ✅ 발헤임 기본 VFX는 순수 Instantiate만 사용 (SetParent/Destroy 금지)
        /// </summary>
        private static void PlayTankerEffects(Player player)
        {
            if (player == null) return;

            try
            {
                // fx_Fader_Spin 이펙트 - 순수 Instantiate (발헤임이 알아서 정리)
                var znetScene = ZNetScene.instance;
                if (znetScene != null)
                {
                    var faderSpinVFX = znetScene.GetPrefab("fx_Fader_Spin");
                    if (faderSpinVFX != null)
                    {
                        Vector3 playerPos = player.transform.position;
                        // ✅ 순수 Instantiate만 - SetParent/Destroy 금지!
                        UnityEngine.Object.Instantiate(faderSpinVFX, playerPos, Quaternion.identity);
                        Plugin.Log.LogDebug("[탱커 VFX] fx_Fader_Spin 순수 표현 완료");
                    }
                    else
                    {
                        Plugin.Log.LogDebug("[탱커 VFX] fx_Fader_Spin 프리팹을 찾을 수 없음");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[탱커 VFX] fx_Fader_Spin 재생 실패: {ex.Message}");
            }
        }


        // 공통 유틸리티 함수들 (JobSkills.cs에서 가져옴)
        private static bool IsOnCooldown(Player player, string skillName) => JobSkillsUtility.IsOnCooldown(player, skillName);
        private static void SetCooldown(Player player, string skillName, float cooldown) => JobSkillsUtility.SetCooldown(player, skillName, cooldown);
        private static void ShowCooldownMessage(Player player, string skillName) => JobSkillsUtility.ShowCooldownMessage(player, skillName);
        private static void ShowRequirementMessage(Player player, string message) => JobSkillsUtility.ShowRequirementMessage(player, message);
        
        /// <summary>
        /// Character 타입에 안전한 표시 이름 가져오기 (Character vs Player 구분)
        /// </summary>
        private static string GetCharacterDisplayName(Character character)
        {
            if (character == null) return "없음";

            // Player 타입인 경우 GetPlayerName() 사용
            if (character is Player player)
                return player.GetPlayerName();

            // 일반 Character인 경우 GetHoverName() 또는 name 사용
            return character.GetHoverName() ?? character.name ?? "Unknown";
        }

        /// <summary>
        /// 탱커 플레이어 사망 시 정리 (Plugin.cs에서 호출)
        /// ✅ 단순화된 정리 (VFX 추적 시스템 제거됨)
        /// </summary>
        public static void CleanupTankerOnDeath(Player player)
        {
            if (player == null) return;

            string playerName = player.GetPlayerName() ?? "Unknown";

            // ✅ 1. 피해 감소 플래그 초기화
            ResetTankerDamageReduction(player);

            // ✅ 2. Harmony AI 패치에서 해당 플레이어의 모든 도발 해제
            TankerTauntAIPatch.ClearTauntsByPlayer(player);

            Plugin.Log.LogDebug($"[탱커 정리] {playerName} 사망 시 플래그 초기화 완료");
        }
    }

    /// <summary>
    /// 탱커 도발용 무시각 데미지 텍스트 패치
    /// 0 데미지 시 텍스트 표시를 차단하여 완전 무시각 어그로 구현
    /// </summary>
    [HarmonyPatch(typeof(DamageText), "ShowText", new Type[] { typeof(DamageText.TextType), typeof(Vector3), typeof(float), typeof(bool) })]
    public static class TankerInvisibleDamagePatch
    {
        /// <summary>
        /// 탱커 도발용 무시각 데미지 플래그
        /// SafeInvisibleTaunt 실행 중일 때 true로 설정
        /// </summary>
        public static bool IsTankerInvisibleDamageActive = false;

        /// <summary>
        /// DamageText.ShowText 실행 전 호출
        /// 탱커 무시각 도발 중이고 0 데미지일 때 텍스트 표시 차단
        /// </summary>
        static bool Prefix(DamageText.TextType type, Vector3 pos, float dmg, bool player)
        {
            try
            {
                // 탱커 무시각 데미지 모드가 활성화되어 있고 데미지가 0 이하일 때
                if (IsTankerInvisibleDamageActive && dmg <= 0f)
                {
                    Plugin.Log.LogDebug($"[탱커 무시각] 0 데미지 텍스트 차단 완료 - 데미지: {dmg}, 타입: {type}");
                    return false; // ShowText 실행 차단
                }

                // 일반적인 경우는 정상 진행
                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[탱커 무시각] DamageText 패치 오류: {ex.Message}");
                return true; // 오류 시 정상 진행
            }
        }
    }
}
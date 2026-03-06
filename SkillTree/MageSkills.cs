using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 메이지 직업 전용 액티브 스킬 및 패시브 스킬 시스템
    /// 메이지 마법 폭발 (AOE 즉시 공격) 및 속성 저항 증가
    /// </summary>
    public static class MageSkills
    {
        // 메이지 액티브 스킬 상태 관리
        private static readonly Dictionary<string, float> lastActivationTime = new Dictionary<string, float>();
        private static readonly HashSet<string> pendingExplosions = new HashSet<string>();

        /// <summary>
        /// 메이지 패시브 및 액티브 스킬을 SkillTreeManager에 등록
        /// </summary>
        public static void RegisterMageSkills()
        {
            Plugin.Log.LogInfo("[메이지 스킬] 패시브 및 액티브 스킬 등록 완료");
        }

        // 메이지 상태 캐시 (직업 변경 후 갱신 필요)
        private static readonly Dictionary<string, bool> mageStatusCache = new Dictionary<string, bool>();
        private static float lastCacheUpdate = 0f;
        private static readonly float CACHE_UPDATE_INTERVAL = 1f; // 1초마다 캐시 갱신
        
        /// <summary>
        /// 메이지인지 확인 (캐시 기반으로 성능 개선 및 직업 변경 후 갱신 보장)
        /// </summary>
        public static bool IsMage(Player player)
        {
            try
            {
                if (player == null) return false;
                
                string playerId = player.GetPlayerID().ToString();
                float currentTime = Time.time;
                
                // 캐시 갱신 조건: 캐시가 없거나, 일정 시간 경과 시
                if (!mageStatusCache.ContainsKey(playerId) || 
                    (currentTime - lastCacheUpdate) > CACHE_UPDATE_INTERVAL)
                {
                    var manager = SkillTreeManager.Instance;
                    bool isMageResult = manager != null && manager.GetSkillLevel("Mage") > 0;
                    
                    mageStatusCache[playerId] = isMageResult;
                    lastCacheUpdate = currentTime;
                    
                    Plugin.Log.LogDebug($"[메이지 스킬] 메이지 상태 캐시 갱신: {player.GetPlayerName()} = {isMageResult}");
                }
                
                return mageStatusCache[playerId];
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 스킬] 메이지 확인 실패: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// 메이지 상태 캐시 강제 갱신 (직업 변경 후 호출)
        /// </summary>
        public static void RefreshMageStatusCache(Player player = null)
        {
            try
            {
                if (player != null)
                {
                    string playerId = player.GetPlayerID().ToString();
                    mageStatusCache.Remove(playerId); // 특정 플레이어 캐시 제거
                }
                else
                {
                    mageStatusCache.Clear(); // 전체 캐시 초기화
                }
                
                lastCacheUpdate = 0f; // 강제 갱신을 위해 시간 리셋
                Plugin.Log.LogInfo($"[메이지 스킬] 메이지 상태 캐시 강제 갱신 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 스킬] 캐시 갱신 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 메이지 속성 저항 보너스 가져오기
        /// </summary>
        public static float GetMageElementalResistance(Player player)
        {
            if (!IsMage(player)) return 0f;
            
            return Mage_Config.MageElementalResistanceValue / 100f; // 퍼센트를 소수로 변환
        }

        /// <summary>
        /// 메이지 액티브 스킬 실행 (Y키)
        /// </summary>
        public static bool TryExecuteMageActiveSkill(Player player)
        {
            if (!IsMage(player))
            {
                return false;
            }

            try
            {
                // 쿨타임 체크
                string playerKey = player.GetPlayerID().ToString();
                float currentTime = Time.time;
                
                if (lastActivationTime.TryGetValue(playerKey, out float lastTime))
                {
                    float cooldownRemaining = Mage_Config.MageCooldownValue - (currentTime - lastTime);
                    if (cooldownRemaining > 0)
                    {
                        player.Message(MessageHud.MessageType.Center, L.Get("mage_cooldown", cooldownRemaining.ToString("F1")));
                        return false;
                    }
                }

                // Eitr 체크
                int eitrCost = Mage_Config.MageEitrCostValue;
                if (player.GetEitr() < eitrCost)
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("eitr_insufficient", eitrCost.ToString()));
                    return false;
                }

                // 지팡이 착용 체크
                if (!IsWieldingStaff(player))
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("staff_required"));
                    return false;
                }

                // 스킬 실행
                ExecuteMageAOESkill(player);
                
                // 쿨타임 설정
                lastActivationTime[playerKey] = currentTime;
                
                // Eitr 소모
                player.AddEitr(-eitrCost);
                
                Plugin.Log.LogInfo($"[메이지 액티브] {player.GetPlayerName()} 메이지 스킬 사용");
                return true;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 액티브] 스킬 실행 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 지팡이 착용 여부 확인
        /// </summary>
        private static bool IsWieldingStaff(Player player)
        {
            try
            {
                var rightItem = player.GetCurrentWeapon();
                if (rightItem?.m_shared != null)
                {
                    // 지팡이 아이템 이름으로 확인 (Staff 타입이 없으므로)
                    string itemName = rightItem.m_shared.m_name?.ToLower() ?? "";
                    if (itemName.Contains("staff") || itemName.Contains("wand") || 
                        itemName.Contains("$item_staff"))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 스킬] 지팡이 확인 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 메이지 AOE 스킬 실행 - 시전시 vfx_GodExplosion + 몬스터에 vfx_HealthUpgrade 2초 후 150% 데미지
        /// </summary>
        private static void ExecuteMageAOESkill(Player player)
        {
            try
            {
                float range = Mage_Config.MageAOERangeValue;

                Vector3 playerPos = player.transform.position;

                // SimpleVFX로 VFX 재생
                SimpleVFX.Play("vfx_GodExplosion", playerPos, 3f);

                // 범위 내 모든 몬스터 찾기 (개선된 검색 로직)
                List<Character> targets = new List<Character>();
                Collider[] colliders = Physics.OverlapSphere(playerPos, range);
                
                foreach (var collider in colliders)
                {
                    var character = collider.GetComponent<Character>();
                    if (character != null && character != player && !character.IsDead())
                    {
                        // 플레이어가 아닌 캐릭터인지 확인
                        if (!character.IsPlayer())
                        {
                            // 다양한 방법으로 적대적 몬스터 확인
                            bool isHostileMonster = false;
                            
                            // 방법 1: 팩션 비교 (기본)
                            if (character.GetFaction() != player.GetFaction())
                            {
                                isHostileMonster = true;
                            }
                            
                            // 방법 2: 몬스터 AI 확인
                            var monsterAI = character.GetComponent<MonsterAI>();
                            if (monsterAI != null)
                            {
                                isHostileMonster = true;
                            }
                            
                            // 방법 3: Humanoid가 아닌 캐릭터 (일반적으로 몬스터)
                            var humanoid = character.GetComponent<Humanoid>();
                            if (humanoid == null || !humanoid.IsPlayer())
                            {
                                // 이름으로 확인 (일반적인 몬스터 패턴)
                                string charName = character.GetHoverName()?.ToLower() ?? "";
                                if (charName.Contains("greydwarf") || charName.Contains("skeleton") || 
                                    charName.Contains("draugr") || charName.Contains("troll") || 
                                    charName.Contains("boar") || charName.Contains("deer") ||
                                    !charName.Contains("player"))
                                {
                                    isHostileMonster = true;
                                }
                            }
                            
                            // 방법 4: 마지막 수단 - 플레이어가 아니고 죽지 않았으면 몬스터로 간주
                            if (!isHostileMonster && !character.IsPlayer() && !character.IsDead())
                            {
                                isHostileMonster = true;
                            }
                            
                            if (isHostileMonster)
                            {
                                targets.Add(character);
                                Plugin.Log.LogDebug($"[메이지 AOE] 타겟 추가: {character.GetHoverName()} (팩션: {character.GetFaction()})");
                            }
                        }
                    }
                }

                if (targets.Count == 0)
                {
                    player.Message(MessageHud.MessageType.Center, L.Get("no_targets_in_range"));
                    return;
                }

                // 가까운 순서로 정렬 후 최대 N마리로 제한 (Config 연동)
                int maxTargets = Mage_Config.MageAOEMaxTargetsValue;
                targets = targets
                    .OrderBy(c => Vector3.Distance(playerPos, c.transform.position))
                    .Take(maxTargets)
                    .ToList();

                // 각 몬스터에게 개별적으로 vfx_HealthUpgrade 효과를 적용하고 2초 후 데미지 적용
                int validTargets = 0;
                
                foreach (var target in targets)
                {
                    if (target != null && !target.IsDead())
                    {
                        // 각 몬스터별로 지연 데미지 적용 시작
                        Plugin.Instance?.StartCoroutine(AttachVFXToMonster(player, target, 2f));
                        validTargets++;
                        
                        Plugin.Log.LogDebug($"[메이지 지연 폭발] {target.GetHoverName()}에게 2초 후 폭발 예정");
                    }
                }
                
                // 플레이어에게 피드백
                player.Message(MessageHud.MessageType.Center, L.Get("mage_spell_cast", validTargets.ToString()));
                
                Plugin.Log.LogInfo($"[메이지 AOE] {player.GetPlayerName()} - {validTargets}마리에게 지연 폭발 주문 시전");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 AOE] 스킬 실행 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 몬스터에 VFX 재생 후 지연 데미지 적용
        /// ✅ EpicMMOSystem 방식으로 단순화 - while 루프 제거
        /// </summary>
        private static IEnumerator AttachVFXToMonster(Player caster, Character target, float duration)
        {
            if (caster == null || target == null) yield break;

            // ✅ VFX 재생 (SimpleVFX 방식)
            try
            {
                Vector3 targetPos = target.transform.position + Vector3.up * 1.5f;
                SimpleVFX.Play("vfx_HealthUpgrade", targetPos, duration);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 VFX] VFX 생성 실패: {ex.Message}");
                yield break;
            }

            // ✅ 단순 대기 (while 루프 제거)
            yield return new WaitForSeconds(duration);

            // 지연 데미지 적용
            if (target != null && !target.IsDead() && caster != null && !caster.IsDead())
            {
                ApplyDelayedDamage(caster, target);
            }
        }
        
        /// <summary>
        /// 지연된 데미지 적용 - 2초 후 150% 공격 효과
        /// </summary>
        private static void ApplyDelayedDamage(Player caster, Character target)
        {
            try
            {
                // 타겟이 여전히 유효한지 확인
                if (target == null || target.IsDead() || caster == null)
                {
                    Plugin.Log.LogDebug("[메이지 지연 폭발] 타겟 또는 시전자가 유효하지 않아 폭발 취소");
                    return;
                }
                
                // 기본 공격력 계산 + 150% 데미지 배수 적용
                float baseDamage = CalculatePlayerBaseDamage(caster);
                float damageMultiplier = Mage_Config.MageDamageMultiplierValue / 100f;
                float finalDamage = baseDamage * damageMultiplier;
                
                // 폭발 효과 - vfx_HealthUpgrade 종료와 함께 데미지 적용
                Vector3 targetPos = target.transform.position;

                // SimpleVFX로 VFX + 사운드 재생
                SimpleVFX.PlayWithSound("fx_siegebomb_explosion", "sfx_GiantDemolisher_rock_destroyed", targetPos, 2f);

                // 데미지 적용
                HitData hitData = new HitData();
                hitData.m_damage.m_damage = finalDamage;
                hitData.m_point = target.GetCenterPoint();
                hitData.m_dir = (targetPos - caster.transform.position).normalized;
                hitData.m_attacker = caster.GetZDOID();
                hitData.m_skill = Skills.SkillType.ElementalMagic;
                
                // 실제 데미지 적용
                target.Damage(hitData);
                
                // 시전자에게 피드백
                if (caster is Player player)
                {
                    player.Message(MessageHud.MessageType.TopLeft, L.Get("mage_explosion_damage", target.GetHoverName(), finalDamage.ToString("F0")));
                }
                
                Plugin.Log.LogInfo($"[메이지 지연 폭발] {target.GetHoverName()}에게 {finalDamage:F1} 데미지 적용 완료 (2초 지연)");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 지연 폭발] 데미지 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어 기본 공격력 계산
        /// </summary>
        private static float CalculatePlayerBaseDamage(Player player)
        {
            try
            {
                // 기본 마법 공격력
                float baseDamage = 50f;
                
                // 지팡이 공격력 추가
                var weapon = player.GetCurrentWeapon();
                if (weapon != null)
                {
                    baseDamage += weapon.GetDamage().GetTotalDamage();
                }
                
                // 마법 스킬 레벨 보너스
                var elementalMagicLevel = player.GetSkillLevel(Skills.SkillType.ElementalMagic);
                baseDamage += elementalMagicLevel * 2f; // 스킬 레벨당 +2 데미지
                
                return baseDamage;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 스킬] 기본 데미지 계산 실패: {ex.Message}");
                return 50f; // 기본값 반환
            }
        }

        /// <summary>
        /// 모든 메이지 스킬 상태 정리 (플러그인 종료시)
        /// </summary>
        public static void ClearAllStates()
        {
            lastActivationTime.Clear();
            pendingExplosions.Clear();
            mageStatusCache.Clear(); // 캐시도 함께 정리
            Plugin.Log.LogInfo("[메이지 스킬] 모든 상태 정리 완료 (캐시 포함)");
        }
    }

    /// <summary>
    /// 메이지 속성 저항 패시브 스킬 패치
    /// Character.Damage에서 속성 데미지 감소 적용
    /// </summary>
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class MageElementalResistancePatch
    {
        /// <summary>
        /// Character.Damage 실행 전 호출 - 메이지 속성 저항 적용
        /// </summary>
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                // 플레이어가 아니면 무시
                if (!(__instance is Player player))
                    return;

                // 메이지가 아니면 무시
                if (!MageSkills.IsMage(player))
                    return;

                // 속성 저항 보너스 가져오기
                float resistance = MageSkills.GetMageElementalResistance(player);
                if (resistance <= 0f)
                    return;

                // 원래 속성 데미지 저장
                var originalElementalDamage = GetElementalDamage(hit);
                
                if (originalElementalDamage > 0f)
                {
                    // 속성 저항 적용 (감소 배수 계산)
                    float reductionMultiplier = 1f - resistance;
                    
                    // 모든 속성 데미지에 저항 적용
                    hit.m_damage.m_fire *= reductionMultiplier;
                    hit.m_damage.m_frost *= reductionMultiplier;
                    hit.m_damage.m_lightning *= reductionMultiplier;
                    hit.m_damage.m_poison *= reductionMultiplier;
                    hit.m_damage.m_spirit *= reductionMultiplier;

                    var reducedElementalDamage = GetElementalDamage(hit);
                    float damageReduced = originalElementalDamage - reducedElementalDamage;

                    if (damageReduced > 0.1f) // 의미있는 감소량일 때만 로그/메시지
                    {
                        Plugin.Log.LogInfo($"[메이지 속성 저항] {player.GetPlayerName()} 속성 데미지 감소 - 원래: {originalElementalDamage:F1} → 감소후: {reducedElementalDamage:F1} (감소량: {damageReduced:F1})");
                        
                        // 플레이어에게 속성 저항 적용 메시지 표시
                        player.Message(MessageHud.MessageType.TopLeft, L.Get("mage_elemental_resist", damageReduced.ToString("F0")));
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[메이지 속성 저항] Damage 패치 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 총 속성 데미지 계산
        /// </summary>
        private static float GetElementalDamage(HitData hit)
        {
            return hit.m_damage.m_fire + hit.m_damage.m_frost + hit.m_damage.m_lightning +
                   hit.m_damage.m_poison + hit.m_damage.m_spirit;
        }
  }
}
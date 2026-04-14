using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 분노의 망치 스킬 전용 클래스 (둔기 H키 액티브 스킬)
    /// VFX 규칙 준수: VFXManager.PlayVFXMultiplayer 사용
    /// </summary>
    public static class FuryHammerSkill
    {
        // === 정적 필드 ===
        // public static bool isChargingFuryHammer = false; // [제거됨] 차지 시스템 제거로 불필요
        private static Dictionary<Player, Coroutine> furyHammerCoroutine = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, bool> furyHammer1stHitBuff = new Dictionary<Player, bool>(); // 1타 공격속도 버프
        private static float lastMaceSkillTime = 0f;

        // === Paladin Lv2 추가 사용 창 ===
        private static Dictionary<Player, float> _furyHammerPendingWindow = new Dictionary<Player, float>();
        private const float FuryHammerExtraWindow = 30f;

        // === 경직 면역 (스킬 활성 중) ===
        private static HashSet<Player> _furyHammerImmune = new HashSet<Player>();

        // === 철슬랫지 공격 모션 캐시 ===
        private static Attack s_sledgeIronAttackCache = null;
        private static Attack s_sledgeIronSecondaryAttackCache = null; // 2차 공격 모션 캐시

        // === 하드코딩 상수 (수정 불가) ===
        private const int ATTACK_COUNT = 5;           // 연속공격 횟수 고정
        private const float ATTACK_INTERVAL = 0.5f;   // 공격간 딜레이 고정 (초)

        /// <summary>
        /// H키 누름 처리: 즉시 스킬 발동
        /// </summary>
        public static void HandleHKeyPress(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 현재 시간
            float nowG = Time.time;

            // 양손둔기 착용 체크
            if (!SkillEffect.IsUsingTwoHandedMace(player))
            {
                SkillEffect.DrawFloatingText(player, L.Get("two_hand_mace_required"), Color.red);
                return;
            }

            // 스킬 보유 및 쿨타임 체크
            bool canFuryHammer = SkillEffect.HasSkill("mace_Step7_fury_hammer");
            float cooldown = Mace_Config.FuryHammerCooldownValue;

            // Paladin Lv2 / Berserker Lv2 / Tanker Lv2 추가 사용 창 확인
            bool hasPaladinLv2 = (SkillTreeManager.Instance?.GetSkillLevel("Paladin") ?? 0) >= 2;
            bool hasBerserkerLv2 = (SkillTreeManager.Instance?.GetSkillLevel("Berserker") ?? 0) >= 2;
            bool hasTankerLv2 = (SkillTreeManager.Instance?.GetSkillLevel("Tanker") ?? 0) >= 2;
            bool hasExtraUse = hasPaladinLv2 || hasBerserkerLv2 || hasTankerLv2;
            bool inPendingWindow = hasExtraUse
                && _furyHammerPendingWindow.TryGetValue(player, out float windowExpiry)
                && nowG <= windowExpiry;

            if (canFuryHammer && (nowG - lastMaceSkillTime > cooldown || inPendingWindow))
            {
                // 스태미나 체크
                float requiredStamina = Mace_Config.FuryHammerStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    SkillEffect.DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // 기존 코루틴 중단
                if (furyHammerCoroutine.TryGetValue(player, out var prevFHCoroutine))
                {
                    SkillTreeInputListener.Instance?.StopCoroutine(prevFHCoroutine);
                    furyHammerCoroutine.Remove(player);
                }

                // Paladin Lv2 / Berserker Lv2 분기: 창 내 2번째 사용이면 쿨타임 시작, 1번째 사용이면 창 오픈
                if (hasExtraUse && !inPendingWindow && nowG - lastMaceSkillTime > cooldown)
                {
                    // 1번째 사용: 쿨타임 보류 + 30초 창 오픈
                    _furyHammerPendingWindow[player] = nowG + FuryHammerExtraWindow;
                    SkillTreeInputListener.Instance?.StartCoroutine(ExpireFuryHammerWindow(player));
                }
                else
                {
                    // 2번째 사용(창 내) or 비팔라딘: 쿨타임 즉시 시작
                    _furyHammerPendingWindow.Remove(player);
                    lastMaceSkillTime = nowG;
                    ActiveSkillCooldownRegistry.SetCooldown("H", cooldown);
                }

                // 새 코루틴 시작
                var coroutine = SkillTreeInputListener.Instance.StartCoroutine(ApplyFuryHammer(player, 0f));
                furyHammerCoroutine[player] = coroutine;

                // 스태미나 소모 (발동 확정 후)
                player.UseStamina(requiredStamina);
            }
            else if (canFuryHammer)
            {
                float remainingCooldown = cooldown - (nowG - lastMaceSkillTime);
                SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_cooldown", $"{remainingCooldown:F1}"), Color.yellow);
            }
            else
            {
                SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_skill_required"), Color.red);
            }
        }

        /// <summary>
        /// H키 해제 처리: 사용하지 않음 (즉시 발동 방식)
        /// </summary>
        public static void HandleHKeyRelease(Player player)
        {
            // H키 누름 시 즉시 발동하므로 해제 처리 불필요
            return;
        }

        /// <summary>
        /// 분노의 망치 1타 공격속도 버프 활성 상태 확인
        /// AnimationSpeedManager와 통합 사용
        /// </summary>
        public static bool IsFuryHammer1stHitBuffActive(Player player)
        {
            if (player == null) return false;
            return furyHammer1stHitBuff.TryGetValue(player, out bool fhBuff) && fhBuff;
        }

        /// <summary>
        /// 분노의 망치 1타 공격속도 버프 보너스 (%)
        /// AnimationSpeedManager가 GetTotalAttackSpeedBonus()를 통해 자동 적용
        /// </summary>
        public static float GetFuryHammer1stHitSpeedBonus(Player player)
        {
            return IsFuryHammer1stHitBuffActive(player) ? 200f : 0f;
        }

        /// <summary>
        /// 분노의 망치 스킬 효과 적용 (5연타)
        /// VFX 규칙 준수: VFXManager.PlayVFXMultiplayer 사용
        /// </summary>
        private static IEnumerator ApplyFuryHammer(Player player, float charge)
        {
            // 플레이어 사망 체크
            if (player == null || player.IsDead())
            {
                yield break;
            }

            // === 도약 돌진: transform.position 포물선 아크 ===
            _furyHammerImmune.Add(player); // 경직 면역 시작

            // 무기 사전 조회 (대시 중 공격 모션에 필요)
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                _furyHammerImmune.Remove(player);
                yield break;
            }
            float baseWeaponDamage = weapon.GetDamage().GetTotalDamage();

            Vector3 dashDir = player.GetLookDir();
            dashDir.y = 0f;
            dashDir.Normalize();

            float dashTime   = 0.5f;   // 전체 도약 시간 (초)
            float dashDist   = 10f;    // 수평 이동 거리 (m)
            float peakHeight = 3.5f;   // 최고 점프 높이 (m, 5m의 70%)

            Vector3 startPos = player.transform.position;
            Vector3 endPos   = startPos + dashDir * dashDist;

            // 착지 위치 지형/바위 높이 보정 (언덕, 바위 위에 착지)
            if (Physics.Raycast(endPos + Vector3.up * 15f, Vector3.down, out RaycastHit landHit, 25f,
                LayerMask.GetMask("terrain", "Default", "static_solid")))
            {
                endPos.y = landHit.point.y;
            }

            float elapsed    = 0f;
            float originalPushForce = 0f;

            while (elapsed < dashTime)
            {
                if (player == null || player.IsDead()) break;
                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(elapsed / dashTime);
                Vector3 pos = Vector3.Lerp(startPos, endPos, t);
                pos.y = startPos.y + peakHeight * 4f * t * (1f - t);

                // 전방 구조물 충돌 체크 (수평 이동 방향만)
                // 시작점을 1.5m 높여 올라타 있는 바위/오브젝트 오탐 방지
                // normal.y < 0.7: 수평면(바닥·바위 위)은 차단 제외, 수직면(벽)만 차단
                // distance > 0.2: 이미 올라타 있는 표면 무시
                LayerMask blockMask = LayerMask.GetMask("piece", "static_solid");
                Vector3 flatDir = new Vector3(dashDir.x, 0f, dashDir.z);
                if (flatDir.magnitude > 0.05f && Physics.SphereCast(
                    player.transform.position + Vector3.up * 1.5f, 0.4f, flatDir.normalized,
                    out RaycastHit blockHit, 1.5f, blockMask)
                    && blockHit.normal.y < 0.7f
                    && blockHit.distance > 0.2f)
                {
                    Plugin.Log.LogDebug($"[분노의 망치] 구조물 충돌 - 도약 중단");
                    endPos = player.transform.position;
                    break;
                }

                player.transform.position = pos;
                yield return null;
            }

            // 착지: 목적지 확정 + 낙하 데미지 방지
            if (player != null && !player.IsDead())
            {
                player.transform.position = endPos;
                var altField = typeof(Character).GetField("m_maxAirAltitude",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                altField?.SetValue(player, endPos.y);

                // 착지 후 슬레지해머 2차 공격 모션 트리거 (착지 상태에서 StartAttack 호출)
                if (weapon.m_shared != null)
                {
                    originalPushForce = weapon.m_shared.m_attackForce;
                    weapon.m_shared.m_attackForce = 0f;
                }
                furyHammer1stHitBuff[player] = true;
                var sledgeAttack = GetCachedSledgeIronSecondaryAttack();
                Attack originalAttack = null;
                if (sledgeAttack != null && weapon.m_shared != null)
                {
                    originalAttack = weapon.m_shared.m_attack;
                    weapon.m_shared.m_attack = sledgeAttack;
                }
                player.StartAttack(null, false);
                if (originalAttack != null && weapon.m_shared != null)
                    weapon.m_shared.m_attack = originalAttack;

                yield return null; // 한 프레임 대기 (애니메이터 상태 처리)

                if (weapon.m_shared != null)
                    weapon.m_shared.m_attackForce = originalPushForce;
                furyHammer1stHitBuff[player] = false;
                CaptainSkillTree.AttackSpeedHandler_Game_Awake_Patch.ClearAttackSpeedWarningState(player);
            }

            // 하드코딩 상수 사용 (수정 불가)
            int attackCount = ATTACK_COUNT;               // 5타 고정

            // Config에서 값 가져오기 (데미지 배율, AOE 범위만)
            float normalHitMultiplier = Mace_Config.FuryHammerNormalHitMultiplierValue / 100f;
            float finalHitMultiplier = Mace_Config.FuryHammerFinalHitMultiplierValue / 100f;
            float aoeRadius = Mace_Config.FuryHammerAoeRadiusValue;

            int totalHits = 0;

            // 데미지 5회 자동 적용 (1타만 플레이어 공격 모션, 나머지는 VFX만)

            for (int i = 0; i < attackCount; i++)
            {
                // 플레이어 사망 체크 (매 데미지마다)
                if (player == null || player.IsDead())
                {
                    yield break;
                }

                // 타격별 데미지 배율 적용 (1~4타: 80%, 5타: 150%)
                bool isLastAttack = (i == attackCount - 1);
                float damageMultiplier = isLastAttack ? finalHitMultiplier : normalHitMultiplier;
                float totalDamage = baseWeaponDamage * damageMultiplier;

                // === 1타: 모션은 도약 중 0.35초에 이미 실행됨 → 착지 즉시 데미지 적용 ===
                if (i == 0)
                {
                    // ✅ 매 타격마다 현재 플레이어 위치 기준으로 동적 계산
                    Vector3 hitPosition = player.transform.position + player.GetLookDir() * 2f;
                    var mobs = Character.GetAllCharacters().Where(c =>
                        (c.IsMonsterFaction(0f) || c.m_faction == Character.Faction.Boss) &&
                        Vector3.Distance(c.transform.position, hitPosition) < aoeRadius
                    );

                    int hitCount = 0;
                    foreach (var mob in mobs)
                    {
                        var hit = new HitData();
                        hit.m_damage.m_blunt = totalDamage;

                        // ✅ 1타는 넉백 제거 (중력효과와 충돌 방지)
                        hit.m_pushForce = 0f;
                        hit.m_dir = (mob.transform.position - player.transform.position).normalized;
                        hit.m_point = mob.GetCenterPoint();
                        hit.m_attacker = player.GetZDOID();
                        hit.SetAttacker(player);

                        mob.Damage(hit);
                        VFXManager.PlayVFXMultiplayer("fx_crit", "", mob.GetCenterPoint(), Quaternion.identity, 1f);
                        // ✅ 1타는 Stagger도 제거 (중력효과 유지)
                        // mob.Stagger(hit.m_dir);
                        hitCount++;
                    }

                    totalHits += hitCount;

                    // ✅ 적중 확인 후 VFX 재생 (중력효과 제거됨)
                    if (hitCount > 0)
                    {
                        // VFX + SFX 재생
                        SimpleVFX.PlayWithSound("flash_round_ellow", "sfx_sledge_iron_hit", hitPosition, 2f);
                    }
                }
                // === 2~5타: VFX 먼저 → 데미지 ===
                else
                {
                    // VFX + SFX 재생
                    string vfxName;
                    string sfxName;
                    float duration;

                    switch (i)
                    {
                        case 1: // 2타: 노란 플래시 + 철제 둔기 타격음
                            vfxName = "flash_round_ellow";
                            sfxName = "sfx_sledge_iron_hit";
                            duration = 2f;
                            break;
                        case 2: // 3타: 물 폭발 + 철제 둔기 타격음
                            vfxName = "water_blast_blue";
                            sfxName = "sfx_sledge_iron_hit";
                            duration = 2f;
                            break;
                        case 3: // 4타: 별 폭발 + 철제 둔기 타격음
                            vfxName = "flash_star_ellow_purple";
                            sfxName = "sfx_sledge_iron_hit";
                            duration = 2f;
                            break;
                        case 4: // 5타: 최종 폭발 + 철제 둔기 타격음 (VFX는 별도 처리)
                            vfxName = "";
                            sfxName = "sfx_sledge_iron_hit";
                            duration = 3f;
                            break;
                        default:
                            vfxName = "";
                            sfxName = "";
                            duration = 1.5f;
                            break;
                    }

                    // ✅ 매 타격마다 현재 플레이어 위치 기준으로 동적 계산
                    Vector3 hitPosition = player.transform.position + player.GetLookDir() * 2f;

                    SimpleVFX.PlayWithSound(vfxName, sfxName, hitPosition, duration);

                    // 5타(i==4): fx_siegebomb_explosion dim 적용
                    if (i == 4)
                    {
                        var _siegePrefab = ZNetScene.instance?.GetPrefab("fx_siegebomb_explosion");
                        if (_siegePrefab != null)
                        {
                            var _siegeGo = UnityEngine.Object.Instantiate(_siegePrefab, hitPosition, Quaternion.identity);
                            SimpleVFX.ApplyVFXDim(_siegeGo, SkillTreeConfig.VFXOpacityValue);
                            // ⚠️ Destroy 생략 — 발헤임 기본 VFX 자동 정리
                        }
                    }

                    // 마지막 공격만 0.5초 대기 후 데미지
                    if (isLastAttack)
                    {
                        SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_final_hit_ready"), new Color(1f, 0.3f, 0f));

                        yield return new WaitForSeconds(0.5f);

                        if (player == null || player.IsDead())
                        {
                            yield break;
                        }
                        // 대기 후 위치 재계산
                        hitPosition = player.transform.position + player.GetLookDir() * 2f;
                    }

                    // 데미지 적용
                    var mobs = Character.GetAllCharacters().Where(c =>
                        (c.IsMonsterFaction(0f) || c.m_faction == Character.Faction.Boss) &&
                        Vector3.Distance(c.transform.position, hitPosition) < aoeRadius
                    );

                    int hitCount = 0;
                    foreach (var mob in mobs)
                    {
                        var hit = new HitData();
                        hit.m_damage.m_blunt = totalDamage;

                        // ✅ 넉백 제거 (중력효과 유지)
                        hit.m_pushForce = 0f;
                        hit.m_dir = (mob.transform.position - player.transform.position).normalized;
                        hit.m_point = mob.GetCenterPoint();
                        hit.m_attacker = player.GetZDOID();
                        hit.SetAttacker(player);

                        mob.Damage(hit);
                        VFXManager.PlayVFXMultiplayer("fx_crit", "", mob.GetCenterPoint(), Quaternion.identity, 1f);
                        // ✅ 스태거 제거 (중력효과 유지)
                        // mob.Stagger(hit.m_dir);
                        hitCount++;
                    }

                    totalHits += hitCount;

                    // ✅ 개별 메시지 제거 - totalHits에만 누적
                }

                // 타격 간 딜레이 (1→2, 2→3, 3→4: 0.8초 / 4→5: 1.2초)
                if (i < attackCount - 1)
                {
                    float delayTime;
                    if (i == 3) // 4타 → 5타
                    {
                        delayTime = 1.2f;
                    }
                    else // 1→2, 2→3, 3→4
                    {
                        delayTime = 0.8f;
                    }

                    yield return new WaitForSeconds(delayTime);

                    // 대기 직후 사망 체크
                    if (player == null || player.IsDead())
                    {
                        yield break;
                    }
                }
            }

            // 경직 면역 해제
            _furyHammerImmune.Remove(player);

            // 최종 완료 메시지
            SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_combo_complete", totalHits.ToString()), new Color(1f, 0.5f, 0f));

            // 코루틴 정상 종료 시 Dictionary에서 제거
            if (furyHammerCoroutine.ContainsKey(player))
            {
                furyHammerCoroutine.Remove(player);
            }

            yield return null;
        }

        // [제거됨] ApplyGravityEffectSmoothly - 중력 효과 제거

        /// <summary>
        /// Paladin Lv2 추가 사용 창 만료 처리
        /// </summary>
        private static System.Collections.IEnumerator ExpireFuryHammerWindow(Player player)
        {
            yield return new WaitForSeconds(FuryHammerExtraWindow);
            if (_furyHammerPendingWindow.ContainsKey(player))
            {
                _furyHammerPendingWindow.Remove(player);
                lastMaceSkillTime = Time.time;
                ActiveSkillCooldownRegistry.SetCooldown("H", Mace_Config.FuryHammerCooldownValue);
            }
        }

        /// <summary>
        /// 경직(Stagger) 면역 패치 - 스킬 활성 중에는 경직 무시
        /// </summary>
        [HarmonyPatch(typeof(Character), "Stagger")]
        public static class FuryHammer_StaggerImmune_Patch
        {
            static bool Prefix(Character __instance)
            {
                if (__instance is Player player && _furyHammerImmune.Contains(player))
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 플레이어 사망 시 분노의 망치 정리 (무한 로딩 방지)
        /// </summary>
        public static void CleanupFuryHammerOnDeath(Player player)
        {
            try
            {
                // 1. 코루틴 중단
                if (furyHammerCoroutine.TryGetValue(player, out var fhCleanupCoroutine))
                {
                    if (fhCleanupCoroutine != null)
                    {
                        try
                        {
                            SkillTreeInputListener.Instance?.StopCoroutine(fhCleanupCoroutine);
                        }
                        catch (Exception ex)
                        {
                            Plugin.Log.LogWarning($"[분노의 망치] 코루틴 중단 실패 (무시): {ex.Message}");
                        }
                    }
                    furyHammerCoroutine.Remove(player);
                }

                // 2. 상태 초기화 (차지 시스템 제거로 불필요)

                // 3. 1타 공격속도 버프 정리
                if (furyHammer1stHitBuff.ContainsKey(player))
                {
                    furyHammer1stHitBuff.Remove(player);
                }

                // 4. Paladin Lv2 추가 사용 창 정리
                _furyHammerPendingWindow.Remove(player);

                // 5. 경직 면역 해제
                _furyHammerImmune.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[분노의 망치] 정리 실패: {ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// 철슬랫지 1차 공격 모션 캐시 반환 (MaceSkills.GetCachedPolearmSecondary 패턴 동일)
        /// </summary>
        internal static Attack GetCachedSledgeIronAttack()
        {
            if (s_sledgeIronAttackCache != null) return s_sledgeIronAttackCache;
            if (ObjectDB.instance == null) return null;

            string[] candidates = { "SledgeIron", "SledgeStagbreaker", "SledgeDemolisher" };
            foreach (var prefabName in candidates)
            {
                var prefab = ObjectDB.instance.GetItemPrefab(prefabName);
                if (prefab == null) continue;
                var item = prefab.GetComponent<ItemDrop>();
                var attack = item?.m_itemData?.m_shared?.m_attack;
                if (attack == null) continue;
                s_sledgeIronAttackCache = attack;
                Plugin.Log.LogInfo($"[분노의 망치] 철슬랫지 1차 공격 모션 캐시 완료: {prefabName}");
                return s_sledgeIronAttackCache;
            }

            Plugin.Log.LogWarning("[분노의 망치] SledgeIron 프리팹을 찾지 못했습니다");
            return null;
        }

        /// <summary>
        /// 철슬랫지 2차 공격 모션 캐시 반환
        /// </summary>
        internal static Attack GetCachedSledgeIronSecondaryAttack()
        {
            if (s_sledgeIronSecondaryAttackCache != null) return s_sledgeIronSecondaryAttackCache;
            if (ObjectDB.instance == null) return null;

            string[] candidates = { "SledgeIron", "SledgeStagbreaker", "SledgeDemolisher" };
            foreach (var prefabName in candidates)
            {
                var prefab = ObjectDB.instance.GetItemPrefab(prefabName);
                if (prefab == null) continue;
                var item = prefab.GetComponent<ItemDrop>();
                var attack = item?.m_itemData?.m_shared?.m_secondaryAttack;
                if (attack == null) continue;
                s_sledgeIronSecondaryAttackCache = attack;
                Plugin.Log.LogInfo($"[분노의 망치] 철슬랫지 2차 공격 모션 캐시 완료: {prefabName}");
                return s_sledgeIronSecondaryAttackCache;
            }

            Plugin.Log.LogWarning("[분노의 망치] SledgeIron 2차 공격 모션을 찾지 못했습니다");
            return null;
        }

        [HarmonyPatch(typeof(Player), "OnDestroy")]
        public static class FuryHammer_Player_OnDestroy_Patch
        {
            static void Postfix(Player __instance)
            {
                if (__instance == null) return;
                if (furyHammerCoroutine.TryGetValue(__instance, out var co) && co != null)
                    SkillTreeInputListener.Instance?.StopCoroutine(co);
                furyHammerCoroutine.Remove(__instance);
                furyHammer1stHitBuff.Remove(__instance);
                _furyHammerPendingWindow.Remove(__instance);
                _furyHammerImmune.Remove(__instance);
            }
        }
    }
}

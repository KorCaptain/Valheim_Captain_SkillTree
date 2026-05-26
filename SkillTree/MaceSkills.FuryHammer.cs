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

        // === 공격속도 캡 우회 (5연타 전 구간) ===
        private static HashSet<Player> _furyHammerCapBypass = new HashSet<Player>();

        // === 철슬랫지 공격 모션 캐시 ===
        private static Attack s_sledgeIronAttackCache = null;
        private static Attack s_sledgeIronSecondaryAttackCache = null; // 2차 공격 모션 캐시

        // === 도약 모션 캐시 (단검 세컨드 고정) ===
        private static Attack s_knifeSecondaryAttackCache = null;

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

            // 둔기 착용 체크 (한손/양손 모두 허용)
            if (!SkillEffect.IsUsingMace(player))
            {
                SkillEffect.DrawFloatingText(player, L.Get("mace_required"), Color.red);
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
                    ActiveSkillCooldownRegistry.SetCooldownForSkill("H", "mace_Step7_fury_hammer", cooldown);
                }

                // 새 코루틴 시작
                var coroutine = SkillTreeInputListener.Instance.StartCoroutine(ApplyFuryHammer(player, 0f));
                furyHammerCoroutine[player] = coroutine;

                // 스태미나 소모 (발동 확정 후)
                player.UseStamina(requiredStamina);
                SkillEffect.TankerPrereqLastUsedTime = Time.time;
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
        /// 분노의 망치 공격속도 캡 우회 활성 상태 확인 (5연타 전 구간)
        /// Plugin.Patches.cs AnimationSpeedManager에서 사용
        /// </summary>
        public static bool IsFuryHammerCapBypassActive(Player player)
        {
            if (player == null) return false;
            return _furyHammerCapBypass.Contains(player);
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
            return IsFuryHammer1stHitBuffActive(player) ? 100f : 0f;
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
            _furyHammerImmune.Add(player);       // 경직 면역 시작
            _furyHammerCapBypass.Add(player);    // 공격속도 캡 우회 시작 (5연타 종료까지)
            furyHammer1stHitBuff[player] = true; // 1타 +100% 속도 버프 (착지 후 0.5초)

            // 무기 사전 조회 (대시 중 공격 모션에 필요)
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                _furyHammerImmune.Remove(player);
                yield break;
            }
            float skillFactor = player.GetSkillFactor(Skills.SkillType.Clubs);
            float baseWeaponDamage = weapon.GetDamage(0, skillFactor).GetTotalDamage();

            Vector3 dashDir = player.GetLookDir();
            dashDir.y = 0f;
            dashDir.Normalize();

            // === 시전 즉시: 카메라 방향 전환 ===
            Quaternion lookRot = Quaternion.LookRotation(dashDir, Vector3.up);
            player.transform.rotation = lookRot;

            var sledgeAttack = GetCachedSledgeIronAttack(); // 착지 시 슬랫지 primary 모션 (규칙: 착지 시에만)
            var zanim = player.GetComponentInChildren<ZSyncAnimation>();
            var animator = player.GetComponentInChildren<Animator>();

            // Root Motion 비활성화 (대시 중 위치 제어 충돌 방지)
            bool origRootMotion = animator != null && animator.applyRootMotion;
            if (animator != null) animator.applyRootMotion = false;

            // 시전 즉시 슬랫지 primary 모션 트리거 (StartAttack 금지 — 현재 무기 모션 오염)
            string landingAnimTrigger = null;
            if (sledgeAttack != null && zanim != null && !string.IsNullOrEmpty(sledgeAttack.m_attackAnimation))
            {
                landingAnimTrigger = (sledgeAttack.m_attackChainLevels > 1 || sledgeAttack.m_attackRandomAnimations >= 2)
                    ? sledgeAttack.m_attackAnimation + "0"
                    : sledgeAttack.m_attackAnimation;
                zanim.SetTrigger(landingAnimTrigger);
                Plugin.Log.LogInfo($"[분노의 망치] 시전 모션(슬랫지 primary): '{landingAnimTrigger}'");
            }

            float dashTime   = 0.3f;   // 전체 도약 시간 (초)
            float dashDist   = 15f;    // 수평 이동 거리 (m) — 1.5배
            float peakHeight = 1.0f;   // 최고 점프 높이 (m)

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

            // Rigidbody 사전 조회 (루트 모션 스냅백 방지용 — PolearmWhirlwind 패턴 동일)
            var rb = player.GetComponent<Rigidbody>();

            while (elapsed < dashTime)
            {
                if (player == null || player.IsDead()) break;
                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(elapsed / dashTime);
                Vector3 pos = Vector3.Lerp(startPos, endPos, t);
                pos.y = startPos.y + peakHeight * 4f * t * (1f - t);

                // 전방 장애물 충돌 체크
                // Default 포함: 바위·나무 등 자연 오브젝트는 Default 레이어에 있음
                // normal.y < 0.7: 수평면(바닥·바위 위) 제외, 수직면(벽·바위 측면)만 차단
                // distance > 0.2: 이미 올라타 있는 표면 무시
                LayerMask blockMask = LayerMask.GetMask("piece", "static_solid", "Default", "terrain");
                Vector3 flatDir = new Vector3(dashDir.x, 0f, dashDir.z);
                bool blocked = false;
                if (flatDir.magnitude > 0.05f)
                {
                    // 상단 체크 (0.8m): 중간 키 이상 장애물 — 기존 1.5m에서 낮춰 낮은 바위도 감지
                    if (Physics.SphereCast(
                            player.transform.position + Vector3.up * 0.8f, 0.4f, flatDir.normalized,
                            out RaycastHit blockHitHigh, 2.5f, blockMask)
                        && blockHitHigh.normal.y < 0.7f
                        && blockHitHigh.distance > 0.2f)
                    {
                        blocked = true;
                        Plugin.Log.LogDebug($"[분노의 망치] 상단 장애물 충돌 ({blockHitHigh.collider.name})");
                    }
                    // 하단 체크 (0.3m): 낮은 바위·울타리 — 거의 수직 면만 차단
                    else if (Physics.SphereCast(
                            player.transform.position + Vector3.up * 0.3f, 0.3f, flatDir.normalized,
                            out RaycastHit blockHitLow, 1.5f, blockMask)
                        && blockHitLow.normal.y < 0.3f
                        && blockHitLow.distance > 0.15f)
                    {
                        blocked = true;
                        Plugin.Log.LogDebug($"[분노의 망치] 하단 장애물 충돌 ({blockHitLow.collider.name})");
                    }
                }
                if (blocked)
                {
                    endPos = player.transform.position;
                    break;
                }

                player.transform.position = pos;
                if (rb != null) rb.position = pos;   // Valheim UpdateMotion() 루트 모션 덮어쓰기 방지
                player.transform.rotation = lookRot; // 매 프레임 방향 유지

                // 대시 전 구간: 적 접촉 시 즉시 착지 (상승/하강 모두)
                if (Character.GetAllCharacters().Any(c =>
                    c != (Character)player &&
                    (c.IsMonsterFaction(0f) || c.m_faction == Character.Faction.Boss) &&
                    Vector3.Distance(c.transform.position, pos) < 2.5f))
                {
                    endPos = pos;
                    Plugin.Log.LogDebug("[분노의 망치] 대시 중 적 접촉 - 즉시 착지");
                    break;
                }

                // 하강 구간(t > 0.5): 지면 접촉 → 조기 착지
                if (t > 0.5f)
                {
                    LayerMask groundMask = LayerMask.GetMask("terrain", "Default", "static_solid");
                    if (Physics.Raycast(pos + Vector3.up * 0.5f, Vector3.down, 1.0f, groundMask))
                    {
                        endPos = pos;
                        Plugin.Log.LogDebug("[분노의 망치] 지면 접촉 - 조기 착지");
                        break;
                    }
                }

                yield return null;
            }

            // 착지: 목적지 확정 + 낙하 데미지 방지
            if (player != null && !player.IsDead())
            {
                player.transform.position = endPos;
                // Rigidbody 위치·속도 초기화 (루트 모션 스냅백 방지 — PolearmWhirlwind 패턴)
                if (rb != null)
                {
                    rb.position = endPos;            // physics 위치 동기화
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                // Valheim 내부 속도 초기화 (m_currentVel 스냅백 핵심 원인)
                var velField = typeof(Character).GetField("m_currentVel",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                velField?.SetValue(player, Vector3.zero);
                // ZDO 위치 동기화 (Valheim 네트워크 보정 스냅백 방지)
                var nview = HarmonyLib.Traverse.Create(player).Field("m_nview").GetValue<ZNetView>();
                if (nview != null && nview.IsOwner())
                {
                    var zdo = nview.GetZDO();
                    if (zdo != null) zdo.SetPosition(endPos);
                }
                var altField = typeof(Character).GetField("m_maxAirAltitude",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                altField?.SetValue(player, endPos.y);
                if (weapon.m_shared != null)
                    weapon.m_shared.m_attackForce = originalPushForce;
                CaptainSkillTree.AttackSpeedHandler_Game_Awake_Patch.ClearAttackSpeedWarningState(player);

                VFXManager.PlayVFXMultiplayer("vfx_sledge_hit", "sfx_sledge_hit", endPos, Quaternion.identity, 1f);
                SkillTreeInputListener.Instance?.StartCoroutine(ResetFuryHammerSpeed(player, animator, 0.5f));
            }

            // 하드코딩 상수 사용 (수정 불가)
            int attackCount = ATTACK_COUNT;               // 5타 고정

            // Config에서 값 가져오기 (데미지 배율, AOE 범위만)
            int furyLevel = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_fury_hammer") ?? 1;
            float normalBonus = (furyLevel - 1) * Mace_Config.FuryHammerNormalHitLevelBonusValue;
            float finalBonus = (furyLevel - 1) * Mace_Config.FuryHammerFinalHitLevelBonusValue;
            float normalHitMultiplier = (Mace_Config.FuryHammerNormalHitMultiplierValue + normalBonus) / 100f;
            float finalHitMultiplier = (Mace_Config.FuryHammerFinalHitMultiplierValue + finalBonus) / 100f;
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

                // === 1타: 착지 후 애니메이션 임팩트 프레임 대기 → 데미지 적용 ===
                if (i == 0)
                {
                    // 1.5x 속도 기준 스윙→임팩트 구간 대기
                    yield return new WaitForSeconds(0.2f);
                    if (player == null || player.IsDead()) yield break;

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
                        hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

                        mob.Damage(hit);
                        VFXManager.PlayVFXMultiplayer("fx_crit", "", mob.GetCenterPoint(), Quaternion.identity, 1f);
                        VFXManager.PlayVFXMultiplayer("vfx_sledge_hit", "", mob.GetCenterPoint(), Quaternion.identity, 1f); // 몬스터 적중 VFX
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
                        case 4: // 5타: 최종 폭발 + 철제 둔기 타격음
                            vfxName = "fx_siegebomb_explosion";
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
                        hit.m_toolTier = (short)weapon.m_shared.m_toolTier;

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

            // Root Motion 복원 (5연타 완료 후 — 착지 직후 복원 시 애니메이션이 원위치로 당기는 문제 방지)
            if (animator != null) animator.applyRootMotion = origRootMotion;

            // 경직 면역 + 공격속도 캡 우회 해제
            _furyHammerImmune.Remove(player);
            _furyHammerCapBypass.Remove(player);

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
        /// 공격속도 복구 코루틴 (데미지 루프와 병렬 실행)
        /// </summary>
        private static IEnumerator ResetFuryHammerSpeed(Player player, Animator animator, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (player != null) furyHammer1stHitBuff[player] = false;
            if (animator != null) animator.speed = 1f;
        }

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
                ActiveSkillCooldownRegistry.SetCooldownForSkill("H", "mace_Step7_fury_hammer", Mace_Config.FuryHammerCooldownValue);
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

                // 5. 경직 면역 + 공격속도 캡 우회 해제
                _furyHammerImmune.Remove(player);
                _furyHammerCapBypass.Remove(player);
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
        /// 단검 2차 공격 모션 캐시 반환 (도약 모션 전용 — 고정값, 수정 금지)
        /// 분노의 망치 도약 구간: 반드시 이 메서드 사용. StartAttack() 호출 금지.
        /// </summary>
        internal static Attack GetCachedKnifeSecondaryAttack()
        {
            if (s_knifeSecondaryAttackCache != null) return s_knifeSecondaryAttackCache;
            if (ObjectDB.instance == null) return null;

            string[] candidates = { "KnifeBlackMetal", "KnifeSilver", "KnifeChitin", "KnifeCopper", "KnifeFlint" };
            foreach (var prefabName in candidates)
            {
                var prefab = ObjectDB.instance.GetItemPrefab(prefabName);
                if (prefab == null) continue;
                var item = prefab.GetComponent<ItemDrop>();
                var attack = item?.m_itemData?.m_shared?.m_secondaryAttack;
                if (attack == null || string.IsNullOrEmpty(attack.m_attackAnimation)) continue;
                s_knifeSecondaryAttackCache = attack;
                Plugin.Log.LogInfo($"[분노의 망치] 단검 도약 모션 캐시: {prefabName} ({attack.m_attackAnimation})");
                return s_knifeSecondaryAttackCache;
            }

            Plugin.Log.LogWarning("[분노의 망치] 단검 세컨드 공격 모션을 찾지 못했습니다");
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
                _furyHammerCapBypass.Remove(__instance);
            }
        }
    }
}

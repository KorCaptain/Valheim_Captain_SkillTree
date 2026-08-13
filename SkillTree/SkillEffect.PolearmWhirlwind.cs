using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 휠윈드 (Whirlwind) 액티브 스킬
    /// Mouse2 홀드: 바라보는 방향(커서/카메라)으로 도약+공격 → 제자리 귀환 → 1.5초 대기 → 반복
    /// - 데미지: 무기 공격력 35%
    /// - 스태미나: 초당 0.5 소모
    /// - 넉백/Stagger: 완전 제거
    /// - VFX: 공격 성공 시 가슴 위치 1회
    /// - 몬스터 물리 충돌 무시
    /// - 활성 중: 일반 공격·점프 차단
    /// </summary>
    public static partial class SkillEffect
    {
        // === 휠윈드 상태 추적 ===
        public static Dictionary<Player, bool> whirlwindActive = new Dictionary<Player, bool>();
        public static Dictionary<Player, Coroutine> whirlwindCoroutines = new Dictionary<Player, Coroutine>();
        public static Dictionary<Player, float> whirlwindLastUseTime = new Dictionary<Player, float>();

        // 휠윈드 내부 공격 트리거 플래그
        public static Dictionary<Player, bool> whirlwindInternalAttack = new Dictionary<Player, bool>();

        // 휠윈드 공격 데미지 활성 플래그 (패치용)
        public static Dictionary<Player, bool> whirlwindDealingDamage = new Dictionary<Player, bool>();

        // 휠윈드 공격속도 버프 활성 플래그 (공격 모션 중 +80%)
        public static Dictionary<Player, bool> whirlwindAttackSpeedActive = new Dictionary<Player, bool>();

        // HUD 지속시간 표시용 (M2 슬롯 서브 아이콘)
        public static float whirlwindDurationStart = -1f;
        public static float whirlwindDurationMax = 0f;

        /// <summary>
        /// 휠윈드 공격 모션 중 공격속도 보너스 반환 (SpeedTree에서 호출)
        /// </summary>
        public static float GetWhirlwindAttackSpeedBonus(Player player)
        {
            if (player == null) return 0f;
            return whirlwindAttackSpeedActive.TryGetValue(player, out bool active) && active ? 80f : 0f;
        }

        // 가슴 높이 오프셋
        private const float ChestHeightOffset = 1.4f;

        // 도약 파라미터
        private const float WW_DashTime   = 0.50f;  // 전진 도약 시간 (0.65 → 0.50, 30% 빠르게)
        private const float WW_DashDist   = 5f;     // 전진 거리 (m)
        private const float WW_PeakHeight = 1.2f;   // 점프 높이
        private const float WW_CycleDelay = 0.38f;  // 착지 후 다음 사이클까지 대기 (0.5 → 0.38, 30% 빠르게)

        /// <summary>
        /// 휠윈드 스킬 사용 (Mouse2 눌림)
        /// </summary>
        public static bool UseWhirlwindSkill(Player player)
        {
            if (player == null) return false;

            // 단검 약점폭발 버프 활성 시 M2 → 스택 폭발 (휠윈드보다 우선)
            if (KnifeStackExplosion.IsBuffActive(player) && Knife_Skill.IsUsingDagger(player))
                return KnifeStackExplosion.TriggerExplosionByM2(player);

            if (!HasSkill("polearm_step6_whirlwind")) return false;
            if (!IsUsingPolearm(player))
            {
                DrawFloatingText(player, "❌ " + L.Get("polearm_required"));
                return false;
            }
            if (whirlwindActive.TryGetValue(player, out bool wwActive) && wwActive) return false;

            float now = Time.time;
            if (whirlwindLastUseTime.TryGetValue(player, out float wwLastUse))
            {
                float cooldown = Polearm_Config.PolearmWhirlwindCooldownValue;
                float elapsed = now - wwLastUse;
                if (elapsed < cooldown)
                {
                    float remaining = cooldown - elapsed;
                    DrawFloatingText(player, "⏳ " + L.Get("polearm_cooldown_remaining", $"{remaining:F1}"));
                    return false;
                }
            }

            if (player.GetStamina() < 0.5f)
            {
                DrawFloatingText(player, "❌ " + L.Get("polearm_stamina_insufficient"));
                return false;
            }

            whirlwindActive[player] = true;
            whirlwindDealingDamage[player] = true;
            // 쿨타임은 스킬 종료 시점에 등록 (finally 블록에서 처리)

            whirlwindCoroutines[player] = player.StartCoroutine(ExecuteWhirlwindLoop(player));

            DrawFloatingText(player, "🌀 " + L.Get("polearm_skill_whirlwind"));
            Plugin.Log.LogDebug("[휠윈드] 스킬 시작");
            return true;
        }

        /// <summary>
        /// 휠윈드 메인 루프:
        /// 바라보는 방향으로 도약+공격 → 제자리 귀환 → 1.5초 대기 → 반복
        /// </summary>
        private static IEnumerator ExecuteWhirlwindLoop(Player player)
        {
            if (player == null) yield break;

            int groundMask    = LayerMask.GetMask("terrain", "Default");
            int playerLayer   = player.gameObject.layer;
            int characterLayer = LayerMask.NameToLayer("character");

            // 시전 즉시 VFX
            {
                var _whirlVfxPos = player.transform.position + Vector3.up * ChestHeightOffset;
                var _whirlPrefab = ZNetScene.instance?.GetPrefab("fx_fallenfalkyrie_spin");
                if (_whirlPrefab != null)
                {
                    var _whirlGo = UnityEngine.Object.Instantiate(_whirlPrefab, _whirlVfxPos, Quaternion.identity);
                    // ⚠️ Destroy 생략 — 발헤임 기본 VFX 자동 정리 (호출 시 무한 로딩 위험)
                }
            }

            // 광역 지속 데미지 코루틴 병행 시작
            player.StartCoroutine(ExecuteWhirlwindAoE(player));

            // 몬스터 레이어 충돌 무시
            Physics.IgnoreLayerCollision(playerLayer, characterLayer, true);

            // 낙하 데미지 방지용 리플렉션 필드
            var altField = typeof(Character).GetField("m_maxAirAltitude",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            bool aborted = false;
            float skillStartTime = Time.time;
            float maxDuration = Polearm_Config.PolearmWhirlwindMaxDurationValue;

            // HUD 지속시간 타이머 시작
            whirlwindDurationStart = skillStartTime;
            whirlwindDurationMax = maxDuration;

            try
            {
                while (!aborted && Input.GetKey(KeyCode.Mouse2) && player != null && !player.IsDead()
                       && (Time.time - skillStartTime) < maxDuration)
                {
                    // ── 스태미나 소모: 회당 4 고정 ──
                    player.UseStamina(4f);
                    if (player.GetStamina() <= 0f) break;

                    // ── 점프 방향: Valheim 표준 커서 방향 (폴백: 캐릭터 전방) ──
                    Vector3 jumpDir = player.GetLookDir();
                    jumpDir.y = 0f;
                    if (jumpDir.sqrMagnitude < 0.01f)
                        jumpDir = player.transform.forward;
                    else
                        jumpDir.Normalize();

                    // ── 전진/귀환 좌표 계산 ──
                    Vector3 startPos = player.transform.position;
                    Vector3 endPos   = startPos + jumpDir * WW_DashDist;

                    // 오브젝트 충돌 체크 (바위, 나무 등 통과 방지)
                    endPos = ClampWhirlwindEndToObstacle(startPos, endPos);

                    // 던전 출구 트리거(TeleportWorld) 통과 방지
                    endPos = ClampAwayFromTeleportTrigger(startPos, endPos);

                    // 착지 지형 높이 보정 (던전·실내: 천장 감지 시 스킵 → 던전 이탈 방지)
                    bool hasCeiling = Physics.Raycast(
                        startPos + Vector3.up * 0.5f, Vector3.up, 10f,
                        LayerMask.GetMask("piece", "Default", "static_solid", "terrain"));
                    if (!hasCeiling && Physics.Raycast(endPos + Vector3.up * 15f, Vector3.down, out RaycastHit landHit, 25f,
                        LayerMask.GetMask("terrain", "Default", "static_solid")))
                    {
                        // 착지 높이가 출발점보다 1.5m 이상 높으면 바위/나무 위 → 보정 거부
                        if (landHit.point.y <= startPos.y + 1.5f)
                            endPos.y = landHit.point.y;
                    }

                    // 캐릭터 회전
                    player.transform.rotation = Quaternion.LookRotation(jumpDir);

                    // ── 점프와 동시에 공격 트리거 ──
                    TriggerWhirlwindAttack(player);

                    // ──────────────────────────────
                    // Phase 1: 전진 도약 (포물선)
                    // ──────────────────────────────
                    float elapsed = 0f;

                    // 물리 간섭 차단 (isKinematic 대신 constraints 사용 — Valheim Character 코드가 velocity 설정 가능하도록)
                    var rb = player.GetComponent<Rigidbody>();
                    RigidbodyConstraints savedConstraints = RigidbodyConstraints.None;
                    if (rb != null)
                    {
                        savedConstraints = rb.constraints;
                        rb.constraints = RigidbodyConstraints.FreezeAll;
                    }

                    while (elapsed < WW_DashTime)
                    {
                        if (player == null || player.IsDead()) { aborted = true; break; }
                        elapsed += Time.deltaTime;

                        float t = Mathf.Clamp01(elapsed / WW_DashTime);
                        Vector3 pos = Vector3.Lerp(startPos, endPos, t);
                        // startPos.y → endPos.y 선형 보간 + 포물선 피크
                        pos.y = Mathf.Lerp(startPos.y, endPos.y, t) + WW_PeakHeight * 4f * t * (1f - t);
                        player.transform.position = pos;
                        if (rb != null) rb.position = pos;  // physics position 동기화 (스냅 방지)
                        yield return null;
                    }
                    if (aborted) break;

                    // 착지 확정 + 낙하 데미지 방지
                    if (player != null && !player.IsDead())
                    {
                        player.transform.position = endPos;
                        if (rb != null) rb.position = endPos;  // physics position 확정
                        altField?.SetValue(player, endPos.y);
                        whirlwindAttackSpeedActive[player] = false;  // 공격 모션 종료 → 속도 복원

                        // ZNetView 동기화 (위치 스냅 방지)
                        var nview = HarmonyLib.Traverse.Create(player).Field("m_nview").GetValue<ZNetView>();
                        if (nview != null && nview.IsOwner())
                        {
                            var zdo = nview.GetZDO();
                            if (zdo != null) zdo.SetPosition(endPos);
                        }
                    }

                    // constraints 복원
                    if (rb != null) rb.constraints = savedConstraints;

                    // ──────────────────────────────
                    // Phase 3: 1.5초 대기 (자유 이동)
                    // Mouse2 해제 시 즉시 종료
                    // ──────────────────────────────
                    float waitEnd = Time.time + WW_CycleDelay;
                    while (Time.time < waitEnd)
                    {
                        if (!Input.GetKey(KeyCode.Mouse2) || player == null || player.IsDead())
                        {
                            aborted = true;
                            break;
                        }
                        yield return null;
                    }
                }
            }
            finally
            {
                // 충돌 복원
                Physics.IgnoreLayerCollision(playerLayer, characterLayer, false);

                // 쿨타임: 스킬 종료 시점에 등록
                if (player != null)
                {
                    whirlwindLastUseTime[player] = Time.time;
                    ActiveSkillCooldownRegistry.SetCooldown("M2", Polearm_Config.PolearmWhirlwindCooldownValue);
                }

                // 종료 처리
                if (player != null)
                {
                    whirlwindActive[player] = false;
                    whirlwindDealingDamage[player] = false;
                    whirlwindAttackSpeedActive[player] = false;
                }
                // HUD 지속시간 타이머 리셋
                whirlwindDurationStart = -1f;
                Plugin.Log.LogDebug("[휠윈드] 스킬 종료");
            }
        }

        /// <summary>
        /// 세컨드 공격 트리거 + VFX (공격 성공 시 1회)
        /// </summary>
        private static void TriggerWhirlwindAttack(Player player)
        {
            if (player == null) return;
            try
            {
                var weapon = player.GetCurrentWeapon();
                if (weapon == null) return;

                // 세컨드 공격 스태미나 임시 0 (휠윈드 회당 4만 소모)
                var secAtk = weapon.m_shared?.m_secondaryAttack;
                float origSecStamina = secAtk?.m_attackStamina ?? 0f;
                if (secAtk != null) secAtk.m_attackStamina = 0f;

                whirlwindInternalAttack[player] = true;
                whirlwindDealingDamage[player] = true;
                whirlwindAttackSpeedActive[player] = true;   // 공격 모션 중 +100% 속도

                // VFX + 공격 모션 동시 시전 (시간차 없음)
                Vector3 chestPos = player.transform.position + Vector3.up * ChestHeightOffset;
                {
                    var _whirlPrefab2 = ZNetScene.instance?.GetPrefab("fx_fallenfalkyrie_spin");
                    if (_whirlPrefab2 != null)
                    {
                        var _whirlGo2 = UnityEngine.Object.Instantiate(_whirlPrefab2, chestPos, Quaternion.identity);
                        // ⚠️ Destroy 생략
                    }
                }

                player.StartAttack(null, true);

                whirlwindInternalAttack[player] = false;

                // 스태미나 복원
                if (secAtk != null) secAtk.m_attackStamina = origSecStamina;
            }
            catch (System.Exception ex)
            {
                whirlwindDealingDamage[player] = false;
                whirlwindInternalAttack[player] = false;
                Plugin.Log.LogWarning($"[휠윈드] 공격 트리거 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 휠윈드 광역 지속 데미지: 플레이어 주변 6m 이내 초당 3회, 무기 공격력 15%
        /// </summary>
        private static IEnumerator ExecuteWhirlwindAoE(Player player)
        {
            const float aoeRadius   = 6f;
            const float interval    = 1f / 3f; // 초당 3회
            int wwAoeLevel = SkillTreeManager.Instance?.GetSkillLevel("polearm_step6_whirlwind") ?? 1;
            float aoePercent = wwAoeLevel switch {
                2 => 20f, 3 => 25f, 4 => 30f,
                5 => 35f, 6 => 40f, 7 => 50f,
                _ => 15f
            };
            float damageMult = aoePercent / 100f;

            while (IsWhirlwindActive(player) && player != null && !player.IsDead())
            {
                yield return new WaitForSeconds(interval);

                if (!IsWhirlwindActive(player) || player == null || player.IsDead()) break;

                var weapon = player.GetCurrentWeapon();
                if (weapon == null) continue;

                var dmg   = weapon.GetDamage();
                Vector3 center = player.transform.position;

                foreach (var ch in Character.GetAllCharacters())
                {
                    if (ch == null) continue;
                    if (ch.IsPlayer() && !(ch.IsPVPEnabled() && player.IsPVPEnabled())) continue;
                    if (!ch.IsPlayer() && !ch.IsMonsterFaction(0f) && ch.m_faction != Character.Faction.Boss) continue;
                    if (Vector3.Distance(ch.transform.position, center) > aoeRadius) continue;

                    var hit = new HitData();
                    hit.m_damage.m_slash   = dmg.m_slash   * damageMult;
                    hit.m_damage.m_pierce  = dmg.m_pierce  * damageMult;
                    hit.m_damage.m_blunt   = dmg.m_blunt   * damageMult;
                    hit.m_damage.m_chop    = dmg.m_chop    * damageMult;
                    hit.m_damage.m_pickaxe = dmg.m_pickaxe * damageMult;
                    hit.m_pushForce = 0f;
                    hit.m_dir   = (ch.transform.position - center).normalized;
                    hit.m_point = ch.GetCenterPoint();
                    hit.m_attacker = player.GetZDOID();
                    hit.SetAttacker(player);

                    ch.Damage(hit);
                }
            }
        }

        /// <summary>
        /// 도약 경로에 오브젝트(바위, 나무 등)가 있으면 충돌 직전으로 endPos를 클램프
        /// </summary>
        private static Vector3 ClampWhirlwindEndToObstacle(Vector3 startPos, Vector3 endPos)
        {
            Vector3 dir = endPos - startPos;
            float dist = dir.magnitude;
            if (dist < 0.5f) return endPos;
            dir.Normalize();

            // CapsuleCast: 플레이어 전체 높이 커버 (발 ~ 머리) → 얇은 나무/바위 통과 방지
            Vector3 capsuleBot = startPos + Vector3.up * 0.4f;
            Vector3 capsuleTop = startPos + Vector3.up * 1.8f;
            int blockMask = LayerMask.GetMask("piece", "Default", "static_solid");

            if (Physics.CapsuleCast(capsuleBot, capsuleTop, 0.35f, dir, out RaycastHit hit, dist, blockMask))
            {
                if (hit.collider.GetComponentInParent<Character>() == null)
                {
                    Plugin.Log.LogDebug($"[휠윈드] 장애물 감지: {hit.collider.name}");
                    float safeDistance = Mathf.Max(0f, hit.distance - 0.5f);
                    return startPos + dir * safeDistance;
                }
            }
            return endPos;
        }

        /// <summary>
        /// 도착 지점 근처에 TeleportWorld 트리거가 있으면 이동 차단 (던전 출구 방지)
        /// </summary>
        private static Vector3 ClampAwayFromTeleportTrigger(Vector3 startPos, Vector3 endPos)
        {
            var nearby = Physics.OverlapSphere(endPos, 2f);
            foreach (var col in nearby)
            {
                if (col == null) continue;
                if (!col.isTrigger) continue;
                if (col.GetComponentInParent<TeleportWorld>() != null)
                {
                    Plugin.Log.LogDebug("[휠윈드] 던전 출구 트리거 감지 → 이동 차단");
                    return startPos;
                }
            }
            return endPos;
        }

        /// <summary>
        /// 마우스 커서 방향 계산 (지면 레이캐스트)
        /// </summary>
        private static Vector3 GetCursorDirection(Player player, int groundMask)
        {
            if (Camera.main == null) return Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPos;

            if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundMask))
                targetPos = hit.point;
            else
                targetPos = ray.origin + ray.direction * 10f;

            Vector3 dir = targetPos - player.transform.position;
            dir.y = 0f;
            return dir.sqrMagnitude > 0.01f ? dir.normalized : Vector3.zero;
        }

        /// <summary>
        /// 휠윈드 활성 상태 확인
        /// </summary>
        public static bool IsWhirlwindActive(Player player)
        {
            return whirlwindActive.TryGetValue(player, out bool active) && active;
        }

        /// <summary>
        /// 사망/로그아웃 시 정리
        /// </summary>
        public static void CleanupWhirlwindOnDeath(Player player)
        {
            try
            {
                int playerLayer   = player?.gameObject.layer ?? 9;
                int characterLayer = LayerMask.NameToLayer("character");
                Physics.IgnoreLayerCollision(playerLayer, characterLayer, false);

                whirlwindActive.Remove(player);
                whirlwindLastUseTime.Remove(player);
                whirlwindDealingDamage.Remove(player);
                whirlwindInternalAttack.Remove(player);
                whirlwindAttackSpeedActive.Remove(player);

                if (whirlwindCoroutines.TryGetValue(player, out var coroutine) && coroutine != null)
                {
                    try { player.StopCoroutine(coroutine); } catch { }
                    whirlwindCoroutines.Remove(player);
                }

                Plugin.Log.LogDebug("[휠윈드] 플레이어 상태 정리 완료");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogWarning($"[휠윈드] 정리 실패: {ex.Message}");
            }
        }
    }

    // ============================================================
    // 휠윈드 데미지 패치: 35% 적용 + 넉백 제거
    // ============================================================

    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_Whirlwind_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.First)]
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (__instance == null || hit == null) return;
                if (__instance.IsPlayer() && !SkillEffect.IsPvPCombat(__instance, hit.GetAttacker())) return;

                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null) return;

                if (!SkillEffect.whirlwindDealingDamage.TryGetValue(player, out bool dealing) || !dealing) return;
                if (!SkillEffect.IsUsingPolearm(player)) return;

                int wwLevel = SkillTreeManager.Instance?.GetSkillLevel("polearm_step6_whirlwind") ?? 1;
                float levelBonus = (wwLevel - 1) * Polearm_Config.PolearmWhirlwindLevelBonusValue;
                float mult = (Polearm_Config.PolearmWhirlwindDamagePercentValue + levelBonus) / 100f;
                hit.m_damage.m_slash   *= mult;
                hit.m_damage.m_pierce  *= mult;
                hit.m_damage.m_blunt   *= mult;
                hit.m_damage.m_chop    *= mult;
                hit.m_damage.m_pickaxe *= mult;
                hit.m_pushForce = 0f;

                Plugin.Log.LogDebug($"[휠윈드] 데미지 {mult * 100f:F0}% 적용, 넉백 제거");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Damage_Whirlwind_Patch] 오류: {ex.Message}");
            }
        }
    }

    // ============================================================
    // 휠윈드 방어 패시브: 사용 중 받는 피해 감소 (Lv1 30% ~ Lv7 78%)
    // ============================================================

    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class Character_Damage_WhirlwindReduction_Patch
    {
        static void Prefix(Character __instance, HitData hit)
        {
            try
            {
                if (!(__instance is Player player)) return;
                if (hit.m_attacker.IsNone()) return; // 낙사 등 공격자 없는 데미지 제외
                if (!SkillEffect.IsWhirlwindActive(player)) return;
                if (!SkillEffect.IsUsingPolearm(player)) return; // 무기 교체 악용 방지

                int wwLevel = SkillTreeManager.Instance?.GetSkillLevel("polearm_step6_whirlwind") ?? 1;
                float levelBonus = (wwLevel - 1) * Polearm_Config.PolearmWhirlwindDamageReductionLevelBonusValue;
                float reduction = Mathf.Clamp(Polearm_Config.PolearmWhirlwindDamageReductionPercentValue + levelBonus, 0f, 100f);
                float mult = 1f - reduction / 100f;

                hit.m_damage.m_blunt     *= mult;
                hit.m_damage.m_slash     *= mult;
                hit.m_damage.m_pierce    *= mult;
                hit.m_damage.m_chop      *= mult;
                hit.m_damage.m_pickaxe   *= mult;
                hit.m_damage.m_fire      *= mult;
                hit.m_damage.m_frost     *= mult;
                hit.m_damage.m_lightning *= mult;
                hit.m_damage.m_poison    *= mult;
                hit.m_damage.m_spirit    *= mult;
                hit.m_damage.m_damage    *= mult;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[휠윈드 방어 패시브] 오류: {ex.Message}");
            }
        }
    }

    [HarmonyPatch(typeof(Character), nameof(Character.Stagger))]
    public static class Character_Stagger_Whirlwind_Patch
    {
        static bool Prefix(Character __instance)
        {
            try
            {
                if (__instance == null || __instance.IsPlayer()) return true;
                var player = Player.m_localPlayer;
                if (player == null) return true;
                if (SkillEffect.whirlwindDealingDamage.TryGetValue(player, out bool dealing) && dealing)
                    return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Stagger_Whirlwind_Patch] 오류: {ex.Message}");
            }
            return true;
        }
    }

    // ============================================================
    // 입력 차단 패치: 활성 중 일반 공격·점프 불가
    // ============================================================

    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
    public static class Humanoid_StartAttack_WhirlwindBlock_Patch
    {
        [HarmonyPriority(HarmonyLib.Priority.High)]
        static bool Prefix(Humanoid __instance, bool secondaryAttack)
        {
            try
            {
                var player = __instance as Player;
                if (player == null || player != Player.m_localPlayer) return true;
                if (!SkillEffect.IsWhirlwindActive(player)) return true;
                if (SkillEffect.whirlwindInternalAttack.TryGetValue(player, out bool isInternal) && isInternal)
                    return true;
                return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Humanoid_StartAttack_WhirlwindBlock_Patch] 오류: {ex.Message}");
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Character), nameof(Character.Jump))]
    public static class Character_Jump_WhirlwindBlock_Patch
    {
        static bool Prefix(Character __instance)
        {
            try
            {
                var player = __instance as Player;
                if (player != null && player == Player.m_localPlayer && SkillEffect.IsWhirlwindActive(player))
                    return false;
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Character_Jump_WhirlwindBlock_Patch] 오류: {ex.Message}");
            }
            return true;
        }
    }
}

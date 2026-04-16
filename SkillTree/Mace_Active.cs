using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree;
using CaptainSkillTree.Gui;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 둔기(Mace) 액티브 스킬 전용 클래스
    /// - 방패돌진 (Shield Charge) G키 액티브 스킬
    /// </summary>
    public static partial class SkillEffect
    {
        // ==================== 방패돌진 (Shield Charge) ====================

        // 쿨타임 관리 (버프 없음 - 쿨타임만 추적)
        private static Dictionary<Player, float> shieldChargeCooldowns = new Dictionary<Player, float>();

        // 돌진 중 중복 발동 방지
        private static HashSet<Player> shieldChargeActive = new HashSet<Player>();

        /// <summary>
        /// 방패돌진 액티브 스킬 발동
        /// G키로 활성화, 카메라 방향 8m 돌진 후 방패 막기력의 70% 데미지
        /// </summary>
        public static void ActivateShieldCharge(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                // 1. 스킬 보유 확인
                if (!HasSkill("mace_Step7_guardian_heart"))
                {
                    DrawFloatingText(player, L.Get("guardian_heart_skill_required"), Color.red);
                    return;
                }

                // 2. 방패 착용 확인 (한손둔기 불필요)
                if (!HasShield(player))
                {
                    DrawFloatingText(player, L.Get("shield_equip_required"), Color.red);
                    return;
                }

                // 3. 돌진 중 중복 발동 방지
                if (shieldChargeActive.Contains(player))
                {
                    DrawFloatingText(player, L.Get("cooldown_active"), Color.yellow);
                    return;
                }

                // 4. 쿨타임 확인
                float now = Time.time;
                if (shieldChargeCooldowns.ContainsKey(player) && now < shieldChargeCooldowns[player])
                {
                    float remaining = shieldChargeCooldowns[player] - now;
                    DrawFloatingText(player, L.Get("cooldown_seconds", Mathf.CeilToInt(remaining).ToString()), Color.yellow);
                    return;
                }

                // 5. 스태미나 소모 확인
                float requiredStamina = Mace_Config.GuardianHeartStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // 6. 스킬 발동
                player.StartCoroutine(ShieldChargeCoroutine(player));

                // 7. 쿨타임 및 스태미나 소모 적용
                shieldChargeCooldowns[player] = now + Mace_Config.GuardianHeartCooldownValue;
                ActiveSkillCooldownRegistry.SetCooldown("G", Mace_Config.GuardianHeartCooldownValue);
                player.UseStamina(requiredStamina);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[방패돌진] 발동 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 방패돌진 핵심 코루틴
        /// 카메라 방향 8m 돌진 → 첫 적 충돌 시 데미지+스태거+AoE+도발
        /// </summary>
        private static IEnumerator ShieldChargeCoroutine(Player player)
        {
            shieldChargeActive.Add(player);
            ZSyncAnimation zanim = null;

            try
            {
                // 1. 돌진 방향 (카메라 방향, 수평)
                Vector3 dashDir = player.GetLookDir();
                dashDir.y = 0f;
                dashDir.Normalize();

                float dashDist = 10f;
                Vector3 startPos = player.transform.position;
                Vector3 endPos = startPos + dashDir * dashDist;

                // 2. blocking 모션 시작
                zanim = player.GetComponentInChildren<ZSyncAnimation>();
                if (zanim != null)
                    zanim.SetBool("blocking", true);

                // 3. 시전 VFX + 사운드
                {
                    var _shieldPrefab = ZNetScene.instance?.GetPrefab("fx_shieldgenerator_domehit");
                    if (_shieldPrefab != null)
                    {
                        var _shieldGo = UnityEngine.Object.Instantiate(_shieldPrefab, player.GetCenterPoint(), Quaternion.identity);
                    }
                }
                VFXManager.PlayVFXMultiplayer("sfx_fader_taunt", "", player.GetCenterPoint(), Quaternion.identity, 2f);

                // 5. Rigidbody 가져오기 (physics-safe 이동)
                var body = HarmonyLib.Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();

                Plugin.Log.LogInfo("[방패돌진] 돌진 시작 (10m, 0.5초)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[방패돌진] 초기화 오류: {ex.Message}");
                shieldChargeActive.Remove(player);
                yield break;
            }

            // 5. 돌진 루프
            float elapsed = 0f;
            float vfxTimer = 0f;
            bool hitEnemy = false;
            Vector3 dashDir2 = player.GetLookDir();
            dashDir2.y = 0f;
            dashDir2.Normalize();
            Vector3 startPos2 = player.transform.position;
            Vector3 endPos2 = startPos2 + dashDir2 * 10f;
            var body2 = HarmonyLib.Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();

            while (elapsed < 0.5f && !hitEnemy)
            {
                if (player == null || player.IsDead()) break;

                elapsed += Time.deltaTime;
                vfxTimer += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / 0.5f);
                Vector3 newPos = Vector3.Lerp(startPos2, endPos2, t);

                // 캐릭터를 돌진 방향으로 매 프레임 강제 회전 (캐릭터 컨트롤러 덮어쓰기 방지)
                if (dashDir2 != Vector3.zero)
                {
                    player.transform.rotation = Quaternion.LookRotation(dashDir2);
                    HarmonyLib.Traverse.Create(player).Field("m_lookDir").SetValue(dashDir2);
                }

                // 돌진 중 VFX 0.05초마다 반복 재생 (연속 돔 이펙트)
                if (vfxTimer >= 0.05f)
                {
                    var _shieldPrefab2 = ZNetScene.instance?.GetPrefab("fx_shieldgenerator_domehit");
                    if (_shieldPrefab2 != null)
                    {
                        var _shieldGo2 = UnityEngine.Object.Instantiate(_shieldPrefab2, player.GetCenterPoint(), Quaternion.identity);
                    }
                    vfxTimer = 0f;
                }

                // 지면 높이 보정 (블록 체크 전 먼저 실행 - 경사면 이동방향을 정확히 계산하기 위해)
                if (Physics.Raycast(newPos + Vector3.up * 5f, Vector3.down, out RaycastHit groundHit, 10f,
                    LayerMask.GetMask("terrain", "static_solid")))
                {
                    newPos.y = groundHit.point.y + 0.3f;
                }

                // 전방 장애물 충돌 체크 (벽/바위/나무/건축물) - 지형 보정 후 실제 이동방향으로 체크
                // Default 레이어 추가: Valheim 나무/덤불이 Default 레이어에 존재
                LayerMask blockMask = LayerMask.GetMask("piece", "static_solid", "Default");
                Vector3 moveDir = newPos - player.transform.position;
                float moveDist = moveDir.magnitude;
                if (moveDist > 0.05f && Physics.SphereCast(
                    player.GetCenterPoint(), 0.5f, moveDir.normalized,
                    out RaycastHit blockHit, moveDist, blockMask))
                {
                    // Character 컴포넌트가 있으면 몬스터/플레이어 → 적중 감지에서 처리, 블록 안 함
                    if (blockHit.collider.GetComponentInParent<Character>() == null)
                    {
                        Plugin.Log.LogDebug($"[방패돌진] 장애물 충돌({blockHit.collider.name}) - 돌진 중단");
                        break;
                    }
                }

                // Rigidbody 이동 (physics-safe)
                if (body2 != null)
                {
                    body2.velocity = Vector3.zero;
                    body2.MovePosition(newPos);
                }
                else
                {
                    player.transform.position = newPos;
                }

                // 적 감지: OverlapSphere(겹침) + SphereCast(전방) 병행
                // SphereCast는 origin에 겹친 콜라이더를 감지 못하므로 OverlapSphere 선행
                LayerMask charMask = LayerMask.GetMask("character", "hitbox");
                Vector3 castOrigin = player.GetCenterPoint();

                // 1단계: OverlapSphere - 플레이어와 겹친 적 감지 (0.8m → 1.2m로 근접 보완)
                if (!hitEnemy)
                {
                    var overlaps = Physics.OverlapSphere(castOrigin, 1.2f, charMask);
                    foreach (var col in overlaps)
                    {
                        var enemy = col.GetComponentInParent<Character>();
                        if (enemy != null && enemy != player && !enemy.IsDead() &&
                            enemy.GetFaction() != Character.Faction.Players)
                        {
                            hitEnemy = true;
                            try { ApplyShieldChargeHit(player, enemy, dashDir2, enemy.GetCenterPoint()); }
                            catch (Exception ex) { Plugin.Log.LogError($"[방패돌진] 충돌 처리 오류: {ex.Message}"); }
                            break;
                        }
                    }
                }

                // 2단계: SphereCast - 전방 2m 적 감지
                if (!hitEnemy && Physics.SphereCast(castOrigin, 0.8f, dashDir2, out RaycastHit sphereHit, 2.0f, charMask))
                {
                    var enemy = sphereHit.collider.GetComponentInParent<Character>();
                    if (enemy != null && enemy != player && !enemy.IsDead() &&
                        enemy.GetFaction() != Character.Faction.Players)
                    {
                        hitEnemy = true;
                        try { ApplyShieldChargeHit(player, enemy, dashDir2, enemy.GetCenterPoint()); }
                        catch (Exception ex) { Plugin.Log.LogError($"[방패돌진] 충돌 처리 오류: {ex.Message}"); }
                    }
                }

                // 3단계: 보정 적중 - 3m 이내 가장 가까운 적 감지 (경로 옆 미스 보완)
                if (!hitEnemy)
                {
                    var nearOverlaps = Physics.OverlapSphere(castOrigin, 3f, charMask);
                    Character nearest = null;
                    float nearestDist = float.MaxValue;
                    foreach (var col in nearOverlaps)
                    {
                        var nearEnemy = col.GetComponentInParent<Character>();
                        if (nearEnemy == null || nearEnemy == player || nearEnemy.IsDead()) continue;
                        if (nearEnemy.GetFaction() == Character.Faction.Players) continue;
                        float dist = Vector3.Distance(castOrigin, nearEnemy.GetCenterPoint());
                        if (dist < nearestDist) { nearestDist = dist; nearest = nearEnemy; }
                    }
                    if (nearest != null)
                    {
                        hitEnemy = true;
                        try { ApplyShieldChargeHit(player, nearest, dashDir2, nearest.GetCenterPoint()); }
                        catch (Exception ex) { Plugin.Log.LogError($"[방패돌진] 보정 적중 오류: {ex.Message}"); }
                    }
                }

                yield return null;
            }

            // 6. blocking 해제
            try
            {
                if (zanim != null && player != null && !player.IsDead())
                    zanim.SetBool("blocking", false);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[방패돌진] blocking 해제 오류: {ex.Message}");
            }

            shieldChargeActive.Remove(player);
            Plugin.Log.LogInfo($"[방패돌진] 돌진 완료 (적중: {hitEnemy})");
        }

        /// <summary>
        /// 방패돌진 충돌 처리: 데미지, 스태거, AoE, 도발
        /// </summary>
        private static void ApplyShieldChargeHit(Player player, Character enemy, Vector3 dashDir, Vector3 hitPoint)
        {
            // 방패 막기력 계산
            float skillFactor = player.GetSkillFactor(Skills.SkillType.Blocking);
            var shieldItem = HarmonyLib.Traverse.Create(player).Field("m_leftItem").GetValue<ItemDrop.ItemData>();

            float blockPower = 0f;
            if (shieldItem != null && shieldItem.m_shared?.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                blockPower = shieldItem.GetBlockPower(skillFactor);

            float damageRatio = Mace_Config.ShieldChargeDamagePercentValue / 100f;
            float damage = blockPower * damageRatio;
            if (damage < 1f) damage = 1f;

            // 주 타겟 데미지
            var chargeHit = new HitData();
            chargeHit.m_damage.m_blunt = damage;
            chargeHit.m_attacker = player.GetZDOID();
            chargeHit.SetAttacker(player);
            chargeHit.m_point = enemy.GetCenterPoint();
            chargeHit.m_dir = dashDir;
            chargeHit.m_skill = Skills.SkillType.Clubs;
            chargeHit.m_pushForce = 5f;
            chargeHit.m_blockable = false;
            chargeHit.m_dodgeable = true;
            chargeHit.m_staggerMultiplier = 2f;

            enemy.Damage(chargeHit);
            enemy.Stagger(dashDir);  // 1.5초 기절

            // 충돌 VFX (파링 효과음)
            VFXManager.PlayVFXMultiplayer("vfx_blocked", "", hitPoint, Quaternion.identity, 2f);

            DrawFloatingText(player, "🛡️ " + L.Get("guardian_heart_activated") + $" {Mathf.RoundToInt(damage)}", new Color(0.2f, 0.8f, 1f, 1f));
            Plugin.Log.LogInfo($"[방패돌진] 주 타겟 적중: {enemy.name}, 막기력 {blockPower:F0} x {Mace_Config.ShieldChargeDamagePercentValue}% = {damage:F0}");

            // AoE 6m 범위 데미지 (주 타겟 제외, ToList 제거 - 직접 순회)
            float aoeDamage = damage * 0.5f;
            int aoeCount = 0;
            foreach (var aoeTarget in Character.GetAllCharacters())
            {
                if (aoeTarget == null || aoeTarget.IsDead() || aoeTarget == player || aoeTarget == enemy) continue;
                if (!JobSkillsUtility.IsMonsterFaction(aoeTarget.GetFaction())) continue;
                if (Vector3.Distance(aoeTarget.transform.position, hitPoint) >= 6f) continue;
                aoeCount++;
                try
                {
                    var aoeHit = new HitData();
                    aoeHit.m_damage.m_blunt = aoeDamage;
                    aoeHit.m_attacker = player.GetZDOID();
                    aoeHit.SetAttacker(player);
                    aoeHit.m_point = aoeTarget.GetCenterPoint();
                    aoeHit.m_dir = (aoeTarget.transform.position - hitPoint).normalized;
                    aoeHit.m_skill = Skills.SkillType.Clubs;
                    aoeHit.m_pushForce = 2f;
                    aoeHit.m_blockable = false;
                    aoeHit.m_dodgeable = true;
                    aoeTarget.Damage(aoeHit);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[방패돌진 AoE] {aoeTarget?.name} 데미지 오류: {ex.Message}");
                }
            }

            // AoE VFX/SFX
            SimpleVFX.Play("flash_star_ellow_purple", hitPoint, 3f);
            VFXManager.PlayVFXMultiplayer("sfx_metal_shield_blocked", "", hitPoint, Quaternion.identity, 2f);

            if (aoeCount > 0)
                Plugin.Log.LogInfo($"[방패돌진 AoE] 6m 범위 {aoeCount}마리 적중, AoE 데미지: {aoeDamage:F0}");

            // 도발 - 6m 내 모든 몬스터 (주 타겟 포함, ToList 제거 - 직접 순회)
            int tauntCount = 0;
            foreach (var tauntTarget in Character.GetAllCharacters())
            {
                if (tauntTarget == null || tauntTarget.IsDead() || tauntTarget == player) continue;
                if (!JobSkillsUtility.IsMonsterFaction(tauntTarget.GetFaction())) continue;
                if (Vector3.Distance(tauntTarget.transform.position, hitPoint) >= 6f) continue;
                tauntCount++;
                try
                {
                    TankerTauntAIPatch.AddTauntedMonster(tauntTarget, player, 5f);

                    // 머리 위 도발 VFX (탱커 패턴 동일)
                    float headHeight = 2.0f;
                    SimpleVFX.PlayFollowing("taunt", tauntTarget.transform, Vector3.up * headHeight, 5f);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[방패돌진 도발] {tauntTarget?.name} 도발 오류: {ex.Message}");
                }
            }

            // 도발 펄스 코루틴 시작 (1초 간격 5회, 플레이어에게 fx_Fader_Spin)
            if (Plugin.Instance != null)
                Plugin.Instance.StartCoroutine(ShieldChargeTauntPulseCoroutine(player, hitPoint));

            Plugin.Log.LogInfo($"[방패돌진 도발] 6m 범위 {tauntCount}마리 도발 시작");
        }

        /// <summary>
        /// 방패돌진 도발 펄스 코루틴
        /// 1초 간격 5회: 플레이어에게 fx_Fader_Spin VFX + 6m 내 몬스터 어그로 유지
        /// 탱커 TauntPulseRoutine 패턴 동일
        /// </summary>
        private static IEnumerator ShieldChargeTauntPulseCoroutine(Player player, Vector3 hitPoint)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player == null || player.IsDead()) yield break;

                // 플레이어에게 fx_Fader_Spin VFX + sfx_metal_shield_blocked_overlay (1초마다)
                try
                {
                    VFXManager.PlayVFXMultiplayer("fx_Fader_Spin", "", player.GetCenterPoint(), Quaternion.identity, 1.5f);
                    VFXManager.PlayVFXMultiplayer("sfx_metal_shield_blocked_overlay", "", player.GetCenterPoint(), Quaternion.identity, 1.5f);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[방패돌진 펄스] VFX 오류: {ex.Message}");
                }

                // 6m 내 몬스터 재수집 → 어그로 유지
                try
                {
                    foreach (var enemy in Character.GetAllCharacters())
                    {
                        if (enemy == null || enemy.IsDead() || enemy == player) continue;
                        if (!JobSkillsUtility.IsMonsterFaction(enemy.GetFaction())) continue;
                        if (Vector3.Distance(enemy.transform.position, hitPoint) >= 6f) continue;
                        TankerTauntAIPatch.AddTauntedMonster(enemy, player, 1f);
                    }

                    Plugin.Log.LogDebug($"[방패돌진 펄스] {i + 1}/5회 어그로 갱신");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[방패돌진 펄스] 어그로 갱신 오류: {ex.Message}");
                }

                if (i < 4)
                    yield return new WaitForSeconds(1f);
            }
        }

        /// <summary>
        /// 방패돌진 정리 메서드 (플레이어 사망 시 호출)
        /// </summary>
        public static void CleanupShieldChargeOnDeath(Player player)
        {
            try
            {
                shieldChargeCooldowns.Remove(player);
                shieldChargeActive.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[방패돌진] 정리 실패: {ex.Message}");
            }
        }
    }
}

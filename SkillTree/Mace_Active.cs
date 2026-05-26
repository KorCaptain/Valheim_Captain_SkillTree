using System;
using System.Collections;
using System.Collections.Generic;
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
    /// - 돌진방패 (Shield Charge) G키 액티브 스킬
    ///   전방 12m 돌진 이동기. 장애물(바위·나무 등) 충돌 시 정지. 경로 상 모든 몬스터 타격 및 도발.
    /// </summary>
    public static partial class SkillEffect
    {
        // ==================== 돌진방패 (Shield Charge) ====================

        private static Dictionary<Player, float> shieldChargeCooldowns = new Dictionary<Player, float>();
        private static HashSet<Player> shieldChargeActive = new HashSet<Player>();
        private static Dictionary<Player, float> _shieldChargePendingWindow = new Dictionary<Player, float>();
        private const float ShieldChargeExtraWindow = 30f;

        /// <summary>
        /// 돌진방패 액티브 스킬 발동
        /// 타겟 불필요 — 전방 8m 방향으로 즉시 돌진
        /// </summary>
        public static void ActivateShieldCharge(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                if (!HasSkill("mace_Step7_guardian_heart"))
                {
                    DrawFloatingText(player, L.Get("guardian_heart_skill_required"), Color.red);
                    return;
                }

                if (!HasShield(player))
                {
                    DrawFloatingText(player, L.Get("shield_equip_required"), Color.red);
                    return;
                }

                if (shieldChargeActive.Contains(player))
                {
                    DrawFloatingText(player, L.Get("shield_charge_active"), Color.yellow);
                    return;
                }

                float now = Time.time;
                bool hasTankerLv2_shield = (SkillTreeManager.Instance?.GetSkillLevel("Tanker") ?? 0) >= 2;
                bool inShieldChargeWindow = hasTankerLv2_shield
                    && _shieldChargePendingWindow.TryGetValue(player, out float shieldWinExpiry)
                    && now <= shieldWinExpiry;

                if (!inShieldChargeWindow && shieldChargeCooldowns.ContainsKey(player) && now < shieldChargeCooldowns[player])
                {
                    float remaining = shieldChargeCooldowns[player] - now;
                    DrawFloatingText(player, L.Get("cooldown_seconds", Mathf.CeilToInt(remaining).ToString()), Color.yellow);
                    return;
                }

                float requiredStamina = Mace_Config.GuardianHeartStaminaCostValue;
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // Tanker Lv2 추가 사용 창 분기
                if (hasTankerLv2_shield && !inShieldChargeWindow)
                {
                    // 1번째 사용: 쿨타임 보류 + 30초 창 오픈
                    _shieldChargePendingWindow[player] = now + ShieldChargeExtraWindow;
                    player.StartCoroutine(ExpireShieldChargeWindow(player));
                    ActiveSkillCooldownRegistry.SetCooldownForSkill("G", "mace_Step7_guardian_heart", ShieldChargeExtraWindow);
                }
                else
                {
                    // 2번째 사용(창 내) or 비탱커: 쿨타임 즉시 시작
                    _shieldChargePendingWindow.Remove(player);
                    shieldChargeCooldowns[player] = now + Mace_Config.GuardianHeartCooldownValue;
                    ActiveSkillCooldownRegistry.SetCooldownForSkill("G", "mace_Step7_guardian_heart", Mace_Config.GuardianHeartCooldownValue);
                }
                player.UseStamina(requiredStamina);
                TankerPrereqLastUsedTime = Time.time;

                player.StartCoroutine(ShieldChargeCoroutine(player));
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[돌진방패] 발동 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 돌진방패 핵심 코루틴
        /// 전방 12m 직선 이동 — 장애물(바위·나무 등) 충돌 시 정지, 경로 상 모든 몬스터 타격
        /// </summary>
        private static IEnumerator ShieldChargeCoroutine(Player player)
        {
            shieldChargeActive.Add(player);
            ZSyncAnimation zanim = null;

            // 전방 수평 방향 계산
            Vector3 lookDir = player.GetLookDir();
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude < 0.01f) lookDir = player.transform.forward;
            lookDir.Normalize();

            Vector3 startPos = player.transform.position;
            float chargeDistance = 12f;
            var rb = player.GetComponent<Rigidbody>();

            // 장애물(바위·나무 등) 충돌 체크 — static_solid/Default/piece 레이어 기준
            int obstacleMask = LayerMask.GetMask("static_solid", "Default", "piece");
            if (Physics.SphereCast(startPos + Vector3.up * 0.8f, 0.4f, lookDir, out RaycastHit obstacleHit, chargeDistance, obstacleMask))
            {
                chargeDistance = Mathf.Max(0f, obstacleHit.distance - 0.5f);
                Plugin.Log.LogInfo($"[돌진방패] 장애물 감지 — 이동거리 {chargeDistance:F1}m로 단축");
            }

            Vector3 endPos = startPos + lookDir * chargeDistance;

            // 착지 지점 지면 보정 — 발 아래 방향 단거리 (던전 내부 천장 관통 방지)
            if (Physics.Raycast(endPos + Vector3.up * 0.1f, Vector3.down, out RaycastHit landHit, 5f,
                LayerMask.GetMask("terrain", "static_solid", "piece")))
            {
                endPos.y = landHit.point.y;
            }

            // 돌진 시작 시 3m 이내 적 수집 (끌어모으기 대상)
            var gatheredEnemies = new List<Character>();
            var gatheredIds = new HashSet<int>();
            foreach (var c in Character.GetAllCharacters())
            {
                if (c == null || c.IsDead() || c == player) continue;
                if (!JobSkillsUtility.IsMonsterFaction(c.GetFaction())) continue;
                if (Vector3.Distance(c.transform.position, startPos) <= 3f)
                {
                    gatheredEnemies.Add(c);
                    gatheredIds.Add(c.GetInstanceID());
                }
            }

            try
            {
                zanim = player.GetComponentInChildren<ZSyncAnimation>();
                if (zanim != null) zanim.SetBool("blocking", true);

                var shieldPrefab = ZNetScene.instance?.GetPrefab("fx_shieldgenerator_domehit");
                if (shieldPrefab != null)
                    UnityEngine.Object.Instantiate(shieldPrefab, player.GetCenterPoint(), Quaternion.identity);

                VFXManager.PlayVFXMultiplayer("sfx_fader_taunt", "", player.GetCenterPoint(), Quaternion.identity, 2f);

                // 수집된 적 스태거 (끌어모으기 시작 연출)
                foreach (var ge in gatheredEnemies)
                    if (ge != null && !ge.IsDead()) ge.Stagger(lookDir);

                Plugin.Log.LogInfo($"[돌진방패] 발동 — 전방 12m 돌진 시작, 수집 적: {gatheredEnemies.Count}명");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[돌진방패] 초기화 오류: {ex.Message}");
                shieldChargeActive.Remove(player);
                yield break;
            }

            float elapsed = 0f;
            float vfxTimer = 0f;
            var alreadyHit = new HashSet<int>();

            while (elapsed < 0.5f)
            {
                if (player == null || player.IsDead()) break;

                elapsed += Time.deltaTime;
                vfxTimer += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / 0.5f);

                Vector3 newPos = Vector3.Lerp(startPos, endPos, t);

                // 지면 아래로 꺼지지 않도록 클램프 — 발 아래 방향 단거리 (던전 천장 관통 방지)
                if (Physics.Raycast(newPos + Vector3.up * 0.1f, Vector3.down, out RaycastHit groundHit, 5f,
                    LayerMask.GetMask("terrain", "static_solid", "piece")))
                {
                    float terrainY = groundHit.point.y + 0.3f;
                    if (newPos.y < terrainY) newPos.y = terrainY;
                }

                player.transform.position = newPos;
                if (rb != null) rb.position = newPos;

                // 수집된 적을 플레이어 전방 포메이션으로 유지
                Vector3 rightDir = Vector3.Cross(lookDir, Vector3.up).normalized;
                for (int gi = 0; gi < gatheredEnemies.Count; gi++)
                {
                    var ge = gatheredEnemies[gi];
                    if (ge == null || ge.IsDead()) continue;
                    float sideOff = (gatheredEnemies.Count > 1)
                        ? (gi - (gatheredEnemies.Count - 1) * 0.5f) * 0.8f : 0f;
                    Vector3 pullPos = player.transform.position + lookDir * 1.5f + rightDir * sideOff;
                    if (Physics.Raycast(pullPos + Vector3.up * 0.1f, Vector3.down, out RaycastHit pgh, 5f,
                        LayerMask.GetMask("terrain", "static_solid", "piece")))
                        pullPos.y = pgh.point.y;
                    ge.transform.position = pullPos;
                    var geRb = HarmonyLib.Traverse.Create(ge).Field("m_body").GetValue<Rigidbody>();
                    if (geRb == null) geRb = ge.GetComponent<Rigidbody>();
                    if (geRb != null)
                    {
                        geRb.position = pullPos;
                        geRb.velocity = Vector3.zero;
                        geRb.angularVelocity = Vector3.zero;
                    }
                }

                // 경로 히트: 3m 이내 몬스터 (수집된 적 제외, 중복 방지)
                foreach (var c in Character.GetAllCharacters())
                {
                    if (c == null || c.IsDead() || c == player) continue;
                    if (!JobSkillsUtility.IsMonsterFaction(c.GetFaction())) continue;
                    int id = c.GetInstanceID();
                    if (alreadyHit.Contains(id)) continue;
                    if (gatheredIds.Contains(id)) continue;
                    if (Vector3.Distance(c.transform.position, player.transform.position) > 3f) continue;

                    alreadyHit.Add(id);
                    gatheredEnemies.Add(c);
                    gatheredIds.Add(id);
                    c.Stagger(lookDir);
                }

                // 돌진 잔상 VFX
                if (vfxTimer >= 0.08f)
                {
                    var shieldPrefab2 = ZNetScene.instance?.GetPrefab("fx_shieldgenerator_domehit");
                    if (shieldPrefab2 != null)
                        UnityEngine.Object.Instantiate(shieldPrefab2, player.GetCenterPoint(), Quaternion.identity);
                    vfxTimer = 0f;
                }

                yield return null;
            }

            // 최종 위치 확정 + 스냅백 완전 방지
            if (player != null && !player.IsDead())
            {
                player.transform.position = endPos;
                if (rb != null)
                {
                    rb.position = endPos;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                // Valheim 내부 속도 초기화 (m_currentVel 스냅백 핵심 원인)
                var velField = typeof(Character).GetField("m_currentVel",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                velField?.SetValue(player, Vector3.zero);
                // ZDO 위치 동기화 (네트워크 스냅백 방지 — FuryHammer 패턴)
                var nview = HarmonyLib.Traverse.Create(player).Field("m_nview").GetValue<ZNetView>();
                if (nview != null && nview.IsOwner())
                {
                    var zdo = nview.GetZDO();
                    if (zdo != null) zdo.SetPosition(endPos);
                }
            }

            // 낙하 데미지 방지
            try
            {
                var altField = typeof(Character).GetField("m_maxAirAltitude",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                altField?.SetValue(player, endPos.y);
            }
            catch { }

            try
            {
                if (zanim != null && player != null && !player.IsDead())
                    zanim.SetBool("blocking", false);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[돌진방패] blocking 해제 오류: {ex.Message}");
            }

            // 수집된 적 최종 타격
            if (gatheredEnemies.Count > 0)
            {
                try { ApplyShieldChargeGatheredFinishDamage(player, gatheredEnemies, lookDir); }
                catch (Exception ex) { Plugin.Log.LogError($"[돌진방패] 최종타 오류: {ex.Message}"); }
            }

            shieldChargeActive.Remove(player);
            Plugin.Log.LogInfo($"[돌진방패] 돌진 완료 — 경로타격: {alreadyHit.Count}, 수집타격: {gatheredEnemies.Count}");

            // 도발 펄스 (타격한 적 대상)
            if ((alreadyHit.Count > 0 || gatheredEnemies.Count > 0) && Plugin.Instance != null)
                Plugin.Instance.StartCoroutine(ShieldChargeTauntPulseCoroutine(player, player.transform.position));
        }

        /// <summary>
        /// 경로 히트 단일 처리: 데미지 + 스태거 + 도발 + VFX
        /// </summary>
        private static void ApplyShieldChargeHit(Player player, Character enemy, Vector3 dashDir)
        {
            float skillFactor = player.GetSkillFactor(Skills.SkillType.Blocking);
            var shieldItem = HarmonyLib.Traverse.Create(player).Field("m_leftItem").GetValue<ItemDrop.ItemData>();

            float blockPower = 0f;
            if (shieldItem != null && shieldItem.m_shared?.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                blockPower = shieldItem.GetBlockPower(skillFactor);

            int scLevel = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_guardian_heart") ?? 1;
            float levelBonus = (scLevel - 1) * Mace_Config.ShieldChargeLevelBonusValue;
            float damageRatio = (Mace_Config.ShieldChargeDamagePercentValue + levelBonus) / 100f;
            float damage = blockPower * damageRatio;
            if (damage < 1f) damage = 1f;

            var chargeHit = new HitData();
            chargeHit.m_damage.m_blunt = damage;
            chargeHit.m_attacker = player.GetZDOID();
            chargeHit.SetAttacker(player);
            chargeHit.m_point = enemy.GetCenterPoint();
            chargeHit.m_dir = dashDir;
            chargeHit.m_skill = Skills.SkillType.Clubs;
            chargeHit.m_pushForce = 3f;
            chargeHit.m_blockable = false;
            chargeHit.m_dodgeable = true;
            chargeHit.m_staggerMultiplier = 2f;

            enemy.Damage(chargeHit);
            enemy.Stagger(dashDir);

            // 도발
            TankerTauntAIPatch.AddTauntedMonster(enemy, player, 5f);

            // 머리 위 도발 아이콘
            try
            {
                var tauntCol = enemy.GetComponent<Collider>();
                float headHeight = (tauntCol != null && tauntCol.bounds.size.y > 0.1f)
                    ? tauntCol.bounds.max.y - enemy.transform.position.y + 0.5f
                    : 2.5f;
                SimpleVFX.PlayFollowing("taunt", enemy.transform, Vector3.up * headHeight, 5f);
            }
            catch { }

            VFXManager.PlayVFXMultiplayer("vfx_blocked", "", enemy.GetCenterPoint(), Quaternion.identity, 2f);
            VFXManager.PlayVFXMultiplayer("sfx_rock_hit", "", enemy.GetCenterPoint(), Quaternion.identity, 2f);
            DrawFloatingText(player, "🛡️ " + L.Get("guardian_heart_activated") + $" {Mathf.RoundToInt(damage)}", new Color(0.2f, 0.8f, 1f, 1f));
            Plugin.Log.LogInfo($"[돌진방패] 경로 타격: {enemy.name}, 데미지={damage:F0}");
        }

        /// <summary>
        /// VFX 발동마다 호출: 3m 반경 모든 적에게 방패 방어력 광역 데미지 + fx_crit
        /// alreadyHit 무관 — 다단히트 허용. excludeIds 지정 시 해당 적 제외.
        /// </summary>
        private static void ApplyShieldChargeAreaMultiHit(Player player, Vector3 dashDir,
            HashSet<int> excludeIds = null)
        {
            float skillFactor = player.GetSkillFactor(Skills.SkillType.Blocking);
            var shieldItem = HarmonyLib.Traverse.Create(player)
                .Field("m_leftItem").GetValue<ItemDrop.ItemData>();

            float blockPower = 0f;
            if (shieldItem != null &&
                shieldItem.m_shared?.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                blockPower = shieldItem.GetBlockPower(skillFactor);

            int scLevelArea = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_guardian_heart") ?? 1;
            float multiHitPercent = 150f + (scLevelArea - 1) * 20f;
            float damage = Mathf.Max(1f, blockPower * multiHitPercent / 100f);

            foreach (var c in Character.GetAllCharacters())
            {
                if (c == null || c.IsDead() || c == player) continue;
                if (!JobSkillsUtility.IsMonsterFaction(c.GetFaction())) continue;
                if (excludeIds != null && excludeIds.Contains(c.GetInstanceID())) continue;
                if (Vector3.Distance(c.transform.position, player.transform.position) > 3f) continue;

                var hit = new HitData();
                hit.m_damage.m_blunt = damage;
                hit.m_attacker = player.GetZDOID();
                hit.SetAttacker(player);
                hit.m_point = c.GetCenterPoint();
                hit.m_dir = dashDir;
                hit.m_skill = Skills.SkillType.Clubs;
                hit.m_blockable = false;
                hit.m_dodgeable = false;

                c.Damage(hit);

                VFXManager.PlayVFXMultiplayer("fx_crit", "", c.GetCenterPoint(), Quaternion.identity, 1.5f);
            }
        }

        /// <summary>
        /// 돌진 완료 후 끌어모은 적 최종 타격:
        /// 첫 번째 적 = 단일 공격력, 나머지 적 = 끌어모은 수만큼 다단히트
        /// </summary>
        private static void ApplyShieldChargeGatheredFinishDamage(
            Player player, List<Character> gathered, Vector3 dashDir)
        {
            int total = gathered.Count;

            // 첫 번째 적: 단일 공격력
            var first = gathered[0];
            if (first != null && !first.IsDead())
                ApplyShieldChargeHit(player, first, dashDir);

            // 나머지 적: 끌어모은 수(total)만큼 다단히트
            if (total < 2) return;

            float skillFactor = player.GetSkillFactor(Skills.SkillType.Blocking);
            var shieldItem = HarmonyLib.Traverse.Create(player)
                .Field("m_leftItem").GetValue<ItemDrop.ItemData>();
            float blockPower = 0f;
            if (shieldItem?.m_shared?.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                blockPower = shieldItem.GetBlockPower(skillFactor);
            int scLv = SkillTreeManager.Instance?.GetSkillLevel("mace_Step7_guardian_heart") ?? 1;
            float pct = 150f + (scLv - 1) * 20f;
            float multiDmg = Mathf.Max(1f, blockPower * pct / 100f);

            for (int i = 1; i < gathered.Count; i++)
            {
                var enemy = gathered[i];
                if (enemy == null || enemy.IsDead()) continue;
                for (int h = 0; h < total; h++)
                {
                    var hit = new HitData();
                    hit.m_damage.m_blunt = multiDmg;
                    hit.m_attacker = player.GetZDOID();
                    hit.SetAttacker(player);
                    hit.m_point = enemy.GetCenterPoint();
                    hit.m_dir = dashDir;
                    hit.m_skill = Skills.SkillType.Clubs;
                    hit.m_blockable = false;
                    hit.m_dodgeable = false;
                    enemy.Damage(hit);
                    VFXManager.PlayVFXMultiplayer("fx_crit", "", enemy.GetCenterPoint(),
                        Quaternion.identity, 1.5f);
                }
            }
            Plugin.Log.LogInfo($"[돌진방패] 최종타 완료 — 수집:{total}명, 첫번째=단일, 나머지={total}회 다단히트");
        }

        /// <summary>
        /// 도발 펄스 코루틴 — 도달 위치 기준 6m 이내 적 어그로 갱신 (1초 × 5회)
        /// </summary>
        private static IEnumerator ShieldChargeTauntPulseCoroutine(Player player, Vector3 center)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player == null || player.IsDead()) yield break;

                try
                {
                    VFXManager.PlayVFXMultiplayer("fx_Fader_Spin", "", player.GetCenterPoint(), Quaternion.identity, 1.5f);
                    VFXManager.PlayVFXMultiplayer("sfx_metal_shield_blocked_overlay", "", player.GetCenterPoint(), Quaternion.identity, 1.5f);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[돌진방패 펄스] VFX 오류: {ex.Message}");
                }

                try
                {
                    foreach (var enemy in Character.GetAllCharacters())
                    {
                        if (enemy == null || enemy.IsDead() || enemy == player) continue;
                        if (!JobSkillsUtility.IsMonsterFaction(enemy.GetFaction())) continue;
                        if (Vector3.Distance(enemy.transform.position, center) >= 6f) continue;
                        TankerTauntAIPatch.AddTauntedMonster(enemy, player, 1f);
                    }

                    Plugin.Log.LogDebug($"[돌진방패 펄스] {i + 1}/5회 어그로 갱신");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[돌진방패 펄스] 어그로 갱신 오류: {ex.Message}");
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
                _shieldChargePendingWindow.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[돌진방패] 정리 실패: {ex.Message}");
            }
        }

        private static IEnumerator ExpireShieldChargeWindow(Player player)
        {
            yield return new WaitForSeconds(ShieldChargeExtraWindow);
            if (_shieldChargePendingWindow.ContainsKey(player))
            {
                _shieldChargePendingWindow.Remove(player);
                shieldChargeCooldowns[player] = Time.time + Mace_Config.GuardianHeartCooldownValue;
                ActiveSkillCooldownRegistry.SetCooldownForSkill("G", "mace_Step7_guardian_heart", Mace_Config.GuardianHeartCooldownValue);
            }
        }
    }
}

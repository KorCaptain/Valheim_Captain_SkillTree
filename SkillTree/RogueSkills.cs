using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 로그 직업 전용 스킬 시스템 (Lv1~5)
    /// Y키: 그림자 일격 → 연속 독 폭발 (범위, 즉시 독데미지, DoT 레벨별 성장)
    /// 헬퍼 메서드: RogueSkillHelpers.cs (partial class)
    /// </summary>
    public static partial class RogueSkills
    {
        // === 🔒 Dictionary 동시 접근 방지 lock ===
        private static readonly object rogueDictionaryLock = new object();

        // === 그림자 일격 버프 상태 관리 ===
        private static Dictionary<Player, float> rogueAttackBuffExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> rogueAttackBuffCoroutine = new Dictionary<Player, Coroutine>();

        // === 충전 시스템 ===
        private static Dictionary<Player, int> rogueShadowStrikeChargesLeft = new Dictionary<Player, int>();

        // === 버프 VFX 시스템 ===
        private static Dictionary<Player, GameObject> rogueBuffVFXInstances = new Dictionary<Player, GameObject>();

        // === 독 DoT 코루틴 관리 (적별) ===
        private static Dictionary<Character, Coroutine> poisonDotCoroutines = new Dictionary<Character, Coroutine>();

        /// <summary>
        /// 로그 스킬을 SkillTreeManager에 등록
        /// </summary>
        public static void RegisterRogueSkill()
        {
            var manager = SkillTreeManager.Instance;
            manager.AddSkill(new SkillNode {
                Id = "Rogue",
                Name = "로그",
                Description = Rogue_Tooltip.GetRogueTooltip(),
                RequiredPoints = 0,
                MaxLevel = 5,
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
                return false;
            }
        }

        /// <summary>
        /// Y키 로그 그림자 일격 - 8회 연속 독 폭발
        /// 범위 10m, 즉시 독데미지 +10, 10초간 초당 5 독데미지
        /// </summary>
        public static void ExecuteRogueShadowStrike(Player player)
        {
            if (player == null) return;

            int lv = GetRogueLevel();

            // 충전 시스템: 남은 충전이 없으면 쿨다운 체크
            bool hasCharge = rogueShadowStrikeChargesLeft.TryGetValue(player, out int charges) && charges > 0;
            if (!hasCharge && JobSkillsUtility.IsOnCooldown(player, "Rogue"))
            {
                float remainingTime = JobSkillsUtility.GetRemainingCooldown(player, "Rogue");
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_shadow_strike_cooldown", remainingTime.ToString("F1")));
                return;
            }

            if (!IsUsingDagger(player))
            {
                JobSkillsUtility.ShowRequirementMessage(player, L.Get("rogue_dagger_required"));
                return;
            }

            float requiredStamina = Rogue_Config.RogueShadowStrikeStaminaCostValue;
            if (player.GetStamina() < requiredStamina)
            {
                JobSkillsUtility.ShowRequirementMessage(player, L.Get("stamina_insufficient"));
                return;
            }

            player.UseStamina(requiredStamina);

            // 충전 소모 또는 쿨다운 설정
            if (hasCharge)
            {
                rogueShadowStrikeChargesLeft[player] = charges - 1;
            }
            else
            {
                float cooldown = GetCooldownForLevel(lv);
                JobSkillsUtility.SetCooldown(player, "Rogue", cooldown);
                ActiveSkillCooldownRegistry.SetCooldown("Y", cooldown);

                // 충전 시스템: 최대 충전 수 복원 예약 (Lv5 이상: 2회)
                int maxCharges = GetChargesForLevel(lv) - 1; // 현재 사용한 1회 빼고
                if (maxCharges > 0)
                    rogueShadowStrikeChargesLeft[player] = maxCharges;
            }

            try
            {
                player.Message(MessageHud.MessageType.Center, L.Get("rogue_shadow_strike_activate"));
                ApplyRogueAttackBuff(player);
                PlayRogueCastSound(player);
                Plugin.Instance?.StartCoroutine(RoguePoisonBlastCoroutine(player, lv));
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// 연속 독 폭발 코루틴 (레벨별 횟수)
        /// </summary>
        private static IEnumerator RoguePoisonBlastCoroutine(Player player, int lv)
        {
            int count = GetPoisonBlastsForLevel(lv);
            float interval = Rogue_Config.RoguePoisonVFXIntervalValue;

            for (int i = 0; i < count; i++)
            {
                if (player == null || player.IsDead()) yield break;

                try
                {
                    VFX.VFXManager.PlayVFXMultiplayer("fx_greenroots_projectile_hit", "", player.transform.position, Quaternion.identity, 1f);
                    SimpleVFX.Play("statusailment_01", player.transform.position, 1.5f);
                    DealPoisonToNearbyEnemies(player, lv);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[로그 독 폭발] tick {i + 1} 오류: {ex.Message}");
                }

                yield return new WaitForSeconds(interval);
            }
        }

        /// <summary>
        /// 주변 범위 적에게 즉시 독데미지 + DoT 적용 (레벨별)
        /// </summary>
        private static float GetWeaponAttackPower(Player player)
        {
            try
            {
                var weapon = player?.GetCurrentWeapon();
                if (weapon?.m_shared == null) return 10f;
                var dmg = weapon.GetDamage();
                float total = dmg.m_slash + dmg.m_pierce + dmg.m_blunt;
                return total > 0f ? total : 10f;
            }
            catch { return 10f; }
        }

        private static void DealPoisonToNearbyEnemies(Player player, int lv)
        {
            float range = Rogue_Config.RoguePoisonRangeValue;
            float weaponDmg = GetWeaponAttackPower(player);
            float instantDmg = weaponDmg * (GetPoisonInstantForLevel(lv) / 100f);
            Vector3 pos = player.transform.position;

            foreach (var enemy in Character.GetAllCharacters())
            {
                if (enemy == null || enemy.IsDead() || enemy == player || enemy.IsPlayer()) continue;
                if (Vector3.Distance(pos, enemy.transform.position) > range) continue;
                try
                {
                    // 즉시 독 데미지 (m_poison은 SE_Poison 상태효과라 즉시 HP감소 없음 → m_slash로 즉시 처리)
                    var hit = new HitData();
                    hit.m_damage.m_slash = instantDmg;
                    hit.m_attacker = player.GetZDOID();
                    hit.m_point = enemy.transform.position;
                    hit.m_dir = (enemy.transform.position - pos).normalized;
                    enemy.Damage(hit);

                    // DoT 갱신 (기존 코루틴 중단 후 재시작)
                    if (poisonDotCoroutines.TryGetValue(enemy, out var existing) && existing != null)
                        Plugin.Instance?.StopCoroutine(existing);

                    var dotCo = Plugin.Instance?.StartCoroutine(PoisonDotCoroutine(player, enemy, lv, weaponDmg));
                    if (dotCo != null)
                        poisonDotCoroutines[enemy] = dotCo;
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// 독 DoT 코루틴: 레벨별 초당 데미지
        /// </summary>
        private static IEnumerator PoisonDotCoroutine(Player player, Character enemy, int lv, float weaponDmg)
        {
            float dotDmg = weaponDmg * (GetPoisonDotForLevel(lv) / 100f);
            float duration = Rogue_Config.RoguePoisonDotDurationValue;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                yield return new WaitForSeconds(1f);
                elapsed += 1f;

                if (enemy == null || enemy.IsDead()) break;

                try
                {
                    var hit = new HitData();
                    hit.m_damage.m_slash = dotDmg;  // m_poison은 SE_Poison 갱신이라 틱 데미지 1/5로 줄어듦 → m_slash로 즉시 처리
                    hit.m_attacker = player?.GetZDOID() ?? ZDOID.None;
                    hit.m_point = enemy.transform.position;
                    enemy.Damage(hit);
                    VFXManager.PlayVFXMultiplayer("fx_crit", "", enemy.GetCenterPoint(), Quaternion.identity, 1.5f);
                }
                catch (Exception) { }
            }

            poisonDotCoroutines.Remove(enemy);
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
                    bool isDagger = weapon.m_shared.m_skillType == Skills.SkillType.Knives;
                    bool isClaw = weapon.m_shared.m_skillType == Skills.SkillType.Unarmed;
                    string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
                    string prefabName = weapon.m_dropPrefab?.name?.ToLower() ?? "";
                    bool isDaggerByName = ContainsDaggerKeyword(weaponName) || ContainsDaggerKeyword(prefabName);
                    return isDagger || isClaw || isDaggerByName;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private static bool ContainsDaggerKeyword(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            string[] daggerKeywords = { "knives", "knife", "dagger", "claw", "fist" };
            string lowerName = name.ToLower();
            foreach (string keyword in daggerKeywords)
                if (lowerName.Contains(keyword)) return true;
            return false;
        }

        /// <summary>
        /// 로그 공격력 증가 버프 적용
        /// </summary>
        private static void ApplyRogueAttackBuff(Player player)
        {
            try
            {
                int lv = GetRogueLevel();
                float buffDuration = GetBuffDurationForLevel(lv);

                if (rogueAttackBuffCoroutine.TryGetValue(player, out var existing) && existing != null)
                    Plugin.Instance?.StopCoroutine(existing);
                rogueAttackBuffCoroutine.Remove(player);

                rogueAttackBuffExpiry[player] = Time.time + buffDuration;

                if (Plugin.Instance != null)
                {
                    var coroutine = Plugin.Instance.StartCoroutine(RogueAttackBuffCoroutine(player, buffDuration));
                    rogueAttackBuffCoroutine[player] = coroutine;
                }

                CreateRogueBuffVFX(player, buffDuration);
            }
            catch (System.Exception) { }
        }

        private static IEnumerator RogueAttackBuffCoroutine(Player player, float duration)
        {
            yield return new WaitForSeconds(duration);

            if (player == null)
            {
                yield break;
            }

            if (player.IsDead())
            {
                lock (rogueDictionaryLock)
                {
                    try
                    {
                        rogueAttackBuffExpiry.Remove(player);
                        rogueAttackBuffCoroutine.Remove(player);
                    }
                    catch (Exception) { }
                }
                RemoveRogueBuffVFX(player);
                yield break;
            }

            lock (rogueDictionaryLock)
            {
                try
                {
                    rogueAttackBuffExpiry.Remove(player);
                    rogueAttackBuffCoroutine.Remove(player);
                }
                catch (Exception) { }
            }

            RemoveRogueBuffVFX(player);

            if (player != null && !player.IsDead())
            {
                try { player.Message(MessageHud.MessageType.Center, L.Get("rogue_buff_end")); }
                catch (Exception) { }
            }
        }

        public static bool IsRogueAttackBuffActive(Player player)
        {
            if (player == null) return false;

            if (rogueAttackBuffExpiry.ContainsKey(player))
            {
                if (Time.time < rogueAttackBuffExpiry[player])
                    return true;

                rogueAttackBuffExpiry.Remove(player);
                rogueAttackBuffCoroutine.Remove(player);
            }
            return false;
        }

        public static float GetRogueAttackBuffMultiplier(Player player)
        {
            if (!IsRogueAttackBuffActive(player)) return 1f;
            int lv = GetRogueLevel();
            float attackBonus = GetAttackBonusForLevel(lv);
            return 1f + (attackBonus / 100f);
        }

        private static void PlayRogueEffects(Player player)
        {
            try
            {
                SimpleVFX.Play("flash_blue_purple", player.transform.position, 2f);
            }
            catch (System.Exception) { }
        }

        private static void PlayRogueCastSound(Player player)
        {
            try
            {
                CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("sfx_oozebomb_explode", "", player.transform.position, Quaternion.identity, 0.5f);
            }
            catch (System.Exception)
            {
                try { player.Message(MessageHud.MessageType.TopLeft, L.Get("rogue_shadow_strike_activate")); }
                catch (System.Exception) { }
            }
        }

        /// <summary>
        /// 로그 스킬 정리 메서드 (플레이어 사망 시 호출)
        /// </summary>
        public static void CleanupRogueSkillsOnDeath(Player player)
        {
            if (player == null) return;

            lock (rogueDictionaryLock)
            {
                try
                {
                    rogueAttackBuffExpiry.Remove(player);
                    StopAndRemoveCoroutine(rogueAttackBuffCoroutine, player);
                    RemoveRogueBuffVFX(player);
                    rogueShadowStrikeChargesLeft.Remove(player);

                }
                catch (Exception ex)
                {
                    Plugin.Log.LogError($"[Rogue Skills] 정리 실패: {ex.Message}");
                }
            }
        }

        private static void StopAndRemoveCoroutine(Dictionary<Player, Coroutine> dict, Player player)
        {
            if (dict.TryGetValue(player, out var co) && co != null)
            {
                try
                {
                    if (Plugin.Instance != null) Plugin.Instance.StopCoroutine(co);
                    else if (player != null) player.StopCoroutine(co);
                }
                catch (Exception) { }
            }
            dict.Remove(player);
        }

        #region === 버프 VFX 시스템 ===

        private static void CreateRogueBuffVFX(Player player, float duration)
        {
            try
            {
                RemoveRogueBuffVFX(player);
                var vfx = SimpleVFX.PlayOnPlayer(player, "statusailment_01_aura", duration, new Vector3(0f, 1.2f, 0f));
                if (vfx != null)
                    rogueBuffVFXInstances[player] = vfx;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[로그 버프 VFX] 생성 실패: {ex.Message}");
            }
        }

        private static void RemoveRogueBuffVFX(Player player)
        {
            try
            {
                if (rogueBuffVFXInstances.TryGetValue(player, out var vfx) && vfx != null)
                    UnityEngine.Object.Destroy(vfx);
                rogueBuffVFXInstances.Remove(player);
            }
            catch (Exception) { }
        }

        #endregion

        /// <summary>
        /// 이동속도 스킬트리 보너스 합산 (Jog + Run 패치 공유)
        /// </summary>
        internal static float CalculateTotalSpeedBonusPercent(Player player, SkillTreeManager manager)
        {
            float totalBonus = 0f;

            if (manager.GetSkillLevel("speed_root") > 0)
                totalBonus += SkillTreeConfig.SpeedRootMoveSpeedValue;

            if (manager.GetSkillLevel("speed_1") > 0)
                totalBonus += SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue;

            if (manager.GetSkillLevel("knife_step3_move_speed") > 0 && WeaponHelper.IsUsingDagger(player))
                totalBonus += Knife_Config.KnifeMoveSpeedBonusValue;

            // 로그 패시브: Lv2~5 이동속도 보너스
            int rogueLv = manager.GetSkillLevel("Rogue");
            if (rogueLv >= 2)
                totalBonus += GetMoveSpeedForLevel(rogueLv);

            float conditionalBonus = Speed.GetConditionalSpeedBonus(player);
            if (conditionalBonus > 0f)
                totalBonus += conditionalBonus * 100f;

            return totalBonus;
        }
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
                if (hit.GetAttacker() is Player player)
                {
                    if (RogueSkills.IsRogueAttackBuffActive(player))
                    {
                        float buffMultiplier = RogueSkills.GetRogueAttackBuffMultiplier(player);
                        hit.m_damage.m_blunt     *= buffMultiplier;
                        hit.m_damage.m_slash     *= buffMultiplier;
                        hit.m_damage.m_pierce    *= buffMultiplier;
                        hit.m_damage.m_chop      *= buffMultiplier;
                        hit.m_damage.m_pickaxe   *= buffMultiplier;
                        hit.m_damage.m_fire      *= buffMultiplier;
                        hit.m_damage.m_frost     *= buffMultiplier;
                        hit.m_damage.m_lightning *= buffMultiplier;
                        hit.m_damage.m_poison    *= buffMultiplier;
                        hit.m_damage.m_spirit    *= buffMultiplier;

                        // 버프 중 적 적중 시 flash_blue_purple VFX
                        if (!(__instance is Player))
                            SimpleVFX.Play("flash_blue_purple", __instance.GetCenterPoint(), 1.5f);
                    }
                }
            }
            catch (System.Exception) { }
        }
    }


    /// <summary>
    /// 로그 패시브 - 스태미나 사용량 감소 패치
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
    public static class RogueStaminaReductionPatch
    {
        static void Prefix(Player __instance, ref float v)
        {
            try
            {
                int lv = RogueSkills.GetRogueLevel();
                if (lv <= 0) return;
                if (!__instance.InAttack()) return;
                float reduction = RogueSkills.GetStaminaReductionForLevel(lv) / 100f;
                v *= (1f - reduction);
            }
            catch (System.Exception) { }
        }
    }

    /// <summary>
    /// 통합 이동속도 보너스 패치 - 조깅(Jog) 속도
    /// </summary>
    [HarmonyPatch(typeof(Player), "GetJogSpeedFactor")]
    public static class ImprovedMoveSpeedPatch
    {
        private static Dictionary<Player, bool> _moveSpeedWarningShown = new Dictionary<Player, bool>();

        [HarmonyPostfix]
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(Player __instance, ref float __result)
        {
            try
            {
                var manager = SkillTreeManager.Instance;
                if (manager == null) return;

                float totalBonus = RogueSkills.CalculateTotalSpeedBonusPercent(__instance, manager);
                float maxBonus = SkillTreeConfig.MoveSpeedMaxBonusValue;

                if (totalBonus > maxBonus)
                {
                    if (!_moveSpeedWarningShown.TryGetValue(__instance, out bool shown) || !shown)
                    {
                        Plugin.Log.LogWarning($"[이동속도] {__instance.GetPlayerName()} 보너스 제한: {totalBonus:F1}% → {maxBonus}%");
                        __instance.Message(MessageHud.MessageType.Center, L.Get("move_speed_cap_warning", $"{maxBonus:F0}"));
                        _moveSpeedWarningShown[__instance] = true;
                    }
                    totalBonus = maxBonus;
                }

                if (totalBonus > 0f)
                    __result = __result * (1f + totalBonus / 100f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[이동속도 패치] 오류: {ex.Message}");
            }
        }

        public static void ClearWarningState(Player player)
        {
            _moveSpeedWarningShown.Remove(player);
        }
    }

    /// <summary>
    /// 달리기(Shift키) 이동속도 보너스 패치
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

                float totalBonus = RogueSkills.CalculateTotalSpeedBonusPercent(__instance, manager);
                float maxBonus = SkillTreeConfig.MoveSpeedMaxBonusValue;
                if (totalBonus > maxBonus) totalBonus = maxBonus;

                if (totalBonus > 0f)
                    __result = __result * (1f + totalBonus / 100f);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[달리기속도 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 로그/이동속도 Dictionary 메모리 정리 (OnDestroy)
    /// </summary>
    [HarmonyPatch(typeof(Player), "OnDestroy")]
    public static class RogueSkills_Player_OnDestroy_Patch
    {
        static void Postfix(Player __instance)
        {
            if (__instance == null) return;
            RogueSkills.CleanupRogueSkillsOnDeath(__instance);
            ImprovedMoveSpeedPatch.ClearWarningState(__instance);
        }
    }
}

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
    /// 로그 직업 전용 스킬 시스템 - 핵심 로직, VFX, 사운드, 버프
    /// 스텔스/어그로 시스템은 RogueStealthSystem.cs (partial class) 참조
    /// </summary>
    public static partial class RogueSkills
    {
        // === 🔒 Dictionary 동시 접근 방지 lock ===
        private static readonly object rogueDictionaryLock = new object();

        // === 그림자 일격 상태 관리 ===
        private static Dictionary<Player, float> rogueAttackBuffExpiry = new Dictionary<Player, float>();
        private static Dictionary<Player, Coroutine> rogueAttackBuffCoroutine = new Dictionary<Player, Coroutine>();

        // === 버프 VFX 시스템 ===
        private static Dictionary<Player, GameObject> rogueBuffVFXInstances = new Dictionary<Player, GameObject>();

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
                return false;
            }
        }

        /// <summary>
        /// Y키 로그 그림자 일격 스킬 실행
        /// </summary>
        public static void ExecuteRogueShadowStrike(Player player)
        {
            if (player == null) return;

            if (JobSkillsUtility.IsOnCooldown(player, "Rogue"))
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

            try
            {
                player.UseStamina(requiredStamina);
                CreateSmokeEffect(player);

                int aggroRemoved = RemoveNearbyMonsterAggro(player);
                if (aggroRemoved > 0)
                    player.Message(MessageHud.MessageType.Center, L.Get("rogue_shadow_strike_success", aggroRemoved.ToString()));
                else
                    player.Message(MessageHud.MessageType.Center, L.Get("rogue_shadow_strike_no_enemy"));

                ApplyRogueAttackBuff(player);
                ApplyStealthState(player);

                float aggroProtectionDuration = Rogue_Config.RogueShadowStrikeStealthDurationValue;
                aggroRemovalEndTime[player] = Time.time + aggroProtectionDuration;
                aggroRemovalActive[player] = true;
                if (Plugin.Instance != null)
                {
                    if (aggroRemovalLoopCoroutine.TryGetValue(player, out var existingLoop) && existingLoop != null)
                        Plugin.Instance.StopCoroutine(existingLoop);
                    var loopCo = Plugin.Instance.StartCoroutine(AggroRemovalLoopCoroutine(player, aggroProtectionDuration));
                    aggroRemovalLoopCoroutine[player] = loopCo;
                }

                JobSkillsUtility.SetCooldown(player, "Rogue", Rogue_Config.RogueShadowStrikeCooldownValue);
                ActiveSkillCooldownRegistry.SetCooldown("Y", Rogue_Config.RogueShadowStrikeCooldownValue);
                PlayRogueEffects(player);
                PlayRogueCastSound(player);
            }
            catch (System.Exception)
            {
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

        /// <summary>
        /// 프리팹 이름에 단검 관련 키워드가 포함되어 있는지 확인
        /// </summary>
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
        /// 연막 효과 생성 (발헤임 기본 fx_greenroots_projectile_hit)
        /// </summary>
        private static void CreateSmokeEffect(Player player)
        {
            try
            {
                VFX.VFXManager.PlayVFXMultiplayer("fx_greenroots_projectile_hit", "", player.transform.position, Quaternion.identity, 1f);
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// 로그 공격력 증가 버프 적용
        /// </summary>
        private static void ApplyRogueAttackBuff(Player player)
        {
            try
            {
                float buffDuration = Rogue_Config.RogueShadowStrikeBuffDurationValue;

                if (rogueAttackBuffCoroutine.TryGetValue(player, out var existing) && existing != null)
                    Plugin.Instance?.StopCoroutine(existing);
                rogueAttackBuffCoroutine.Remove(player);

                rogueAttackBuffExpiry[player] = Time.time + buffDuration;

                if (Plugin.Instance != null)
                {
                    var coroutine = Plugin.Instance.StartCoroutine(RogueAttackBuffCoroutine(player, buffDuration));
                    rogueAttackBuffCoroutine[player] = coroutine;
                }

                CreateRogueBuffVFX(player);
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// 로그 공격력 버프 코루틴
        /// </summary>
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

        /// <summary>
        /// 로그 공격력 버프 활성 상태 확인
        /// </summary>
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

        /// <summary>
        /// 로그 공격력 버프 배율 가져오기
        /// </summary>
        public static float GetRogueAttackBuffMultiplier(Player player)
        {
            if (!IsRogueAttackBuffActive(player)) return 1f;
            float attackBonus = Rogue_Config.RogueShadowStrikeAttackBonusValue;
            return 1f + (attackBonus / 100f);
        }

        /// <summary>
        /// 로그 스킬 효과 재생 (flash_blue_purple VFX)
        /// </summary>
        private static void PlayRogueEffects(Player player)
        {
            try
            {
                SimpleVFX.Play("flash_blue_purple", player.transform.position, 2f);
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// 로그 스킬 시전 효과음 재생 (sfx_oozebomb_explode)
        /// </summary>
        private static void PlayRogueCastSound(Player player)
        {
            try
            {
                SimpleVFX.Play("sfx_oozebomb_explode", player.transform.position, 0.5f);
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
                    // 그림자 일격 버프 정리
                    rogueAttackBuffExpiry.Remove(player);
                    StopAndRemoveCoroutine(rogueAttackBuffCoroutine, player);
                    RemoveRogueBuffVFX(player);

                    // 스텔스 + 어그로 제거 상태 정리 (RogueStealthSystem.cs)
                    CleanupStealthAndAggroState(player);
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

        private static void CreateRogueBuffVFX(Player player)
        {
            try
            {
                RemoveRogueBuffVFX(player);
                var vfx = SimpleVFX.PlayOnPlayer(player, "statusailment_01_aura", 9999f, new Vector3(0f, 1.2f, 0f));
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
                    if (RogueSkills.IsPlayerInStealth(player))
                        RogueSkills.RemoveStealthState(player, "공격");

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
                    }
                }
            }
            catch (System.Exception) { }
        }
    }

    /// <summary>
    /// 로그 패시브 - 속성 저항 증가 패치
    /// </summary>
    [HarmonyPatch(typeof(Character), "Damage")]
    public static class RoguePassivePatch
    {
        static void Prefix(Character __instance, ref HitData hit)
        {
            try
            {
                if (!(__instance is Player player)) return;
                if (!RogueSkills.IsRogue(player)) return;

                float resist = Rogue_Config.RogueElementalResistanceDebuffValue / 100f;
                hit.m_damage.m_fire      *= (1f - resist);
                hit.m_damage.m_frost     *= (1f - resist);
                hit.m_damage.m_lightning *= (1f - resist);
                hit.m_damage.m_poison    *= (1f - resist);
                hit.m_damage.m_spirit    *= (1f - resist);
            }
            catch (System.Exception) { }
        }
    }

    // [로그 패시브] 공격 속도 보너스: GetTotalAttackSpeedBonus() (SkillEffect.SpeedTree.cs)에서 처리됨.

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
                if (!RogueSkills.IsRogue(__instance)) return;
                if (!__instance.InAttack()) return;
                float reduction = Rogue_Config.RogueStaminaReductionValue / 100f;
                v *= (1f - reduction);
            }
            catch (System.Exception) { }
        }
    }

    /// <summary>
    /// 통합 이동속도 보너스 패치 - 조깅(Jog) 속도
    /// 공식: 최종 속도 = (발헤임 기본 + 다른 모드) × (1 + 스킬트리 보너스%)
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
                        __instance.Message(MessageHud.MessageType.Center, $"이동속도 제한을 {maxBonus}%를 넘길 수 없습니다.");
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
}

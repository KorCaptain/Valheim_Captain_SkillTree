using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 스탯 변환 전용 효과 시스템 - MMO 독립적인 직접 효과 구현
    ///
    /// 구현 노드:
    /// - atk_twohand_drain: 힘+5, 지능+5 → 물리/속성 공격력 직접 증가
    /// - defense_root ~ defense_Step3: 방어력/체력/스태미나/에이트르 직접 증가
    /// - speed_1: 공격속도/이동속도 +2%
    /// - speed_2: 지구력+3 → 스태미나 최대치 증가
    /// - speed_3: 지능+3 → 에이트르 최대치 증가
    ///
    /// 핵심 원칙: MMO 독립성, 직접 효과, 조건부 적용, Config 기반 동적 관리
    /// </summary>
    public static partial class SkillEffect
    {
        // === 스탯 트리 상태 추적 변수들 ===

        // StatusEffect 캐시 (성능 최적화)
        private static readonly Dictionary<Player, bool> statTreeStatusEffectApplied = new Dictionary<Player, bool>();

        // 신경강화: 마지막 피격 회피 발동 시간 (Time.time)
        internal static Dictionary<Player, float> nerveLastEvasionTime = new Dictionary<Player, float>();

        /// <summary>
        /// 1. atk_twohand_drain: 물리/속성 공격력 직접 증가
        /// Character.Damage 패치를 통한 직접 데미지 증가 방식
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Character_Damage_StatTree_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Prefix(Character __instance, HitData hit)
            {
                try
                {
                    // 공격자 검증
                    var attacker = hit.GetAttacker();
                    if (attacker == null || !attacker.IsPlayer()) return;

                    var player = attacker as Player;
                    if (player == null) return;

                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    // atk_twohand_drain 활성화 확인
                    if (manager.GetSkillLevel("atk_twohand_drain") <= 0) return;

                    bool isStatTreeEffect = false;

                    // === atk_twohand_drain: 물리/속성 공격력 증가 ===
                    float physicalBonus = SkillTreeConfig.AttackTwoHandDrainPhysicalDamageValue;
                    float elementalBonus = SkillTreeConfig.AttackTwoHandDrainElementalDamageValue;

                    // 물리 데미지 증가 (무기가 해당 타입 보유 시만)
                    if (hit.m_damage.m_blunt > 0) hit.m_damage.m_blunt += physicalBonus;
                    if (hit.m_damage.m_slash > 0) hit.m_damage.m_slash += physicalBonus;
                    if (hit.m_damage.m_pierce > 0) hit.m_damage.m_pierce += physicalBonus;

                    // 속성 데미지 증가 (무기가 해당 타입 보유 시만)
                    if (hit.m_damage.m_fire > 0) hit.m_damage.m_fire += elementalBonus;
                    if (hit.m_damage.m_frost > 0) hit.m_damage.m_frost += elementalBonus;
                    if (hit.m_damage.m_lightning > 0) hit.m_damage.m_lightning += elementalBonus;
                    if (hit.m_damage.m_poison > 0) hit.m_damage.m_poison += elementalBonus;
                    if (hit.m_damage.m_spirit > 0) hit.m_damage.m_spirit += elementalBonus;

                    isStatTreeEffect = true;

                    // 효과 적용 시각 피드백 (10% 확률)
                    if (isStatTreeEffect && UnityEngine.Random.Range(0f, 1f) < 0.1f)
                    {
                        ShowSkillEffectText(player, $"⚔️ {L.Get("attack_increased")}",
                            new Color(1f, 0.6f, 0.2f), SkillEffectTextType.Combat);
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[스탯 트리] atk_twohand_drain 데미지 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 2. defense_root ~ defense_Step3: 방어력 직접 증가
        /// Character.GetBodyArmor 패치를 통한 방어력 보너스
        /// defense_root: 투구 방어력 +2
        /// defense_Step1_survival: 흉갑 방어력 +5
        /// defense_Step2_health: 레깅스 방어력 +5
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
        public static class Character_GetBodyArmor_StatTree_Patch
        {
            // FieldInfo 캐시 - Traverse.Create 3회 반복 제거 (GetBodyArmor는 매우 자주 호출됨)
            private static System.Reflection.FieldInfo _fiHelmet;
            private static System.Reflection.FieldInfo _fiChest;
            private static System.Reflection.FieldInfo _fiLegs;
            private static bool _fieldsCached;

            private static void EnsureFieldCache()
            {
                if (_fieldsCached) return;
                _fieldsCached = true;
                var bf = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public;
                _fiHelmet = typeof(Humanoid).GetField("m_helmetItem", bf);
                _fiChest  = typeof(Humanoid).GetField("m_chestItem",  bf);
                _fiLegs   = typeof(Humanoid).GetField("m_legItem",    bf);
            }

            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Character __instance, ref float __result)
            {
                try
                {
                    if (__instance == null || !__instance.IsPlayer()) return;

                    var player = __instance as Player;
                    if (player == null) return;

                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    float armorBonus = 0f;

                    // Humanoid로 캐스팅하여 아이템 슬롯 접근
                    var humanoid = player as Humanoid;
                    if (humanoid == null) return;

                    // 캐시된 FieldInfo 사용 (Traverse.Create 3회 반복 제거)
                    EnsureFieldCache();
                    var helmetItem = _fiHelmet?.GetValue(humanoid) as ItemDrop.ItemData;
                    var chestItem  = _fiChest?.GetValue(humanoid)  as ItemDrop.ItemData;
                    var legItem    = _fiLegs?.GetValue(humanoid)   as ItemDrop.ItemData;

                    // defense_root: 투구 방어력 +2
                    if (manager.GetSkillLevel("defense_root") > 0 && helmetItem != null)
                    {
                        armorBonus += Defense_Config.DefenseRootArmorBonusValue;
                    }

                    // defense_Step1_survival: 흉갑 방어력 +5
                    if (manager.GetSkillLevel("defense_Step1_survival") > 0 && chestItem != null)
                    {
                        armorBonus += Defense_Config.SurvivalArmorBonusValue;
                    }

                    // defense_Step2_health: 레깅스 방어력 +5
                    if (manager.GetSkillLevel("defense_Step2_health") > 0 && legItem != null)
                    {
                        armorBonus += Defense_Config.HealthArmorBonusValue;
                    }

                    if (armorBonus > 0)
                    {
                        __result += armorBonus;
                    }

                    // 바위피부 % + 제작 마법부여 방어력 % 합산 적용 (덧셈)
                    float totalArmorPct = 0f;
                    if (manager.GetSkillLevel("defense_Step4_tanker") > 0)
                        totalArmorPct += Defense_Config.TankerArmorBonusValue;

                    float enchantArmorPct = ProducerCrafting.GetEquippedArmorEnchantTotal(player, ProducerCrafting.EnchantType.Armor);
                    totalArmorPct += enchantArmorPct;

                    if (totalArmorPct > 0f)
                    {
                        __result += __result * (totalArmorPct / 100f);
                    }

                    // 소수점 1자리 반올림 (표시 정리: 98.56001 → 98.6)
                    __result = Mathf.Round(__result * 10f) / 10f;
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[스탯 트리] 방어력 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 3. defense 트리: 체력 최대치 증가
        /// Player.GetTotalFoodValue 패치 (hp 파라미터)
        /// MMO 방식 동일하게 적용 - 음식 시스템과 통합
        /// </summary>
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Player_GetTotalFoodValue_HP_StatTree_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(ref float hp)
            {
                try
                {
                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    float healthBonus = 0f;

                    // defense_root: 방어 전문가 체력 보너스
                    if (manager.GetSkillLevel("defense_root") > 0)
                        healthBonus += Defense_Config.DefenseRootHealthBonusValue;

                    // defense_Step1_survival: 피부경화 체력 보너스
                    if (manager.GetSkillLevel("defense_Step1_survival") > 0)
                        healthBonus += Defense_Config.SurvivalHealthBonusValue;

                    // defense_Step2_health: 체력단련 체력 보너스
                    if (manager.GetSkillLevel("defense_Step2_health") > 0)
                        healthBonus += Defense_Config.HealthBonusValue;

                    // defense_Step3_boost: 체력증강 체력 보너스
                    if (manager.GetSkillLevel("defense_Step3_boost") > 0)
                        healthBonus += Defense_Config.BoostHealthBonusValue;

                    // defense_Step6_body: 요툰의 생명력 - 체력 +30% (비율→고정값 변환)
                    // ✅ 힐링 깜빡임 방지: 비율 보너스를 고정 수치로 변환하여 m_baseHP에 포함
                    if (manager.GetSkillLevel("defense_Step6_body") > 0)
                    {
                        float baseHealthBeforeBonus = hp + healthBonus;
                        healthBonus += baseHealthBeforeBonus * (Defense_Config.BodyHealthBonusValue / 100f);
                    }

                    if (healthBonus > 0)
                        hp += healthBonus;
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[스탯 트리] 체력 GetTotalFoodValue 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 4. defense + speed 트리: 스태미나 최대치 증가
        /// Player.GetTotalFoodValue 패치 (stamina 파라미터)
        /// MMO 방식 동일하게 적용 - 음식 시스템과 통합
        /// </summary>
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Player_GetTotalFoodValue_Stamina_StatTree_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance, ref float stamina)
            {
                try
                {
                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    float staminaBonus = 0f;

                    // defense_Step2_dodge: 심신단련 스태미나 보너스
                    if (manager.GetSkillLevel("defense_Step2_dodge") > 0)
                        staminaBonus += Defense_Config.DodgeStaminaBonusValue;

                    // speed_2: 스태미나 +15
                    if (manager.GetSkillLevel("speed_2") > 0)
                        staminaBonus += SkillTreeConfig.SpeedEnduranceStaminaBonusValue;

                    if (staminaBonus > 0)
                        stamina += staminaBonus;

                    // 인챈트 MaxStamina: 퍼센트 증가 (GetMaxStamina 패치 대신 여기서 처리)
                    if (__instance != null)
                    {
                        float enchantPct = ProducerCrafting.GetEquippedArmorEnchantTotal(
                            __instance, ProducerCrafting.EnchantType.MaxStamina);
                        if (enchantPct > 0f)
                            stamina += stamina * (enchantPct / 100f);
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[스탯 트리] 스태미나 GetTotalFoodValue 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 5. defense + speed 트리: 에이트르 최대치 증가
        /// Player.GetTotalFoodValue 패치 (eitr 파라미터)
        /// MMO 방식 동일하게 적용 - 음식 시스템과 통합
        /// </summary>
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Player_GetTotalFoodValue_Eitr_StatTree_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(ref float eitr)
            {
                try
                {
                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    float eitrBonus = 0f;

                    // defense_Step2_dodge: 심신단련 에이트르 보너스
                    if (manager.GetSkillLevel("defense_Step2_dodge") > 0)
                    {
                        float bonus = Defense_Config.DodgeEitrBonusValue;
                        eitrBonus += bonus;
                        Plugin.Log.LogDebug($"[방어→음식] 심신단련 에이트르: +{bonus}");
                    }

                    // defense_Step3_breath: 단전호흡 에이트르 보너스
                    if (manager.GetSkillLevel("defense_Step3_breath") > 0)
                    {
                        float bonus = Defense_Config.BreathEitrBonusValue;
                        eitrBonus += bonus;
                        Plugin.Log.LogDebug($"[방어→음식] 단전호흡 에이트르: +{bonus}");
                    }

                    // speed_3: 에이트르 +10
                    if (manager.GetSkillLevel("speed_3") > 0)
                        eitrBonus += SkillTreeConfig.SpeedIntellectEitrBonusValue;

                    // staff_Step2_stream: 마법 흐름 에이트르 +30
                    if (manager.GetSkillLevel("staff_Step2_stream") > 0)
                    {
                        float bonus = Staff_Config.StaffStreamEitrBonusValue;
                        eitrBonus += bonus;
                    }

                    if (eitrBonus > 0)
                        eitr += eitrBonus;
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[스탯 트리] 에이트르 GetTotalFoodValue 패치 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 6. speed_1: 공격속도 +2%
        /// CharacterAnimEvent.CustomFixedUpdate 패치를 통한 Animator.speed 조작
        /// </summary>
        // 임시 비활성화: animator.speed 직접 제어는 AnimationSpeedManager와 충돌
        // speed_1 스킬의 공격속도 보너스는 AnimationSpeedManager에서 처리
        /*
        [HarmonyPatch(typeof(CharacterAnimEvent), nameof(CharacterAnimEvent.CustomFixedUpdate))]
        public static class CharacterAnimEvent_StatTree_AttackSpeed_Patch
        {
            private static readonly Dictionary<Player, float> lastSpeedApplied = new Dictionary<Player, float>();

            [HarmonyPriority(Priority.Low)]
            public static void Postfix(CharacterAnimEvent __instance)
            {
                try
                {
                    var player = Player.m_localPlayer;
                    if (player == null) return;

                    var manager = SkillTreeManager.Instance;
                    if (manager == null || manager.GetSkillLevel("speed_1") <= 0)
                    {
                        // speed_1 미보유 시 리셋
                        ResetAnimatorSpeed(player);
                        return;
                    }

                    // 0.2초마다만 업데이트 (성능 최적화)
                    if (lastSpeedApplied.TryGetValue(player, out float lastUpdate) &&
                        Time.time - lastUpdate < 0.2f)
                        return;

                    var animator = player.GetComponentInChildren<Animator>();
                    if (animator == null) return;

                    float speedBonus = SkillTreeConfig.SpeedDexterityAttackSpeedBonusValue / 100f;
                    float targetSpeed = 1f + speedBonus;

                    if (Mathf.Abs(animator.speed - targetSpeed) > 0.01f)
                    {
                        animator.speed = targetSpeed;
                        lastSpeedApplied[player] = Time.time;
                        Plugin.Log.LogDebug($"[스탯 트리] 공격속도 보너스 적용: {targetSpeed:F2}x");
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[스탯 트리] 공격속도 패치 오류: {ex.Message}");
                }
            }

            private static void ResetAnimatorSpeed(Player player)
            {
                try
                {
                    var animator = player?.GetComponentInChildren<Animator>();
                    if (animator != null && Mathf.Abs(animator.speed - 1f) > 0.01f)
                    {
                        animator.speed = 1f;
                        Plugin.Log.LogDebug($"[스탯 트리] 공격속도 리셋: 1.0x");
                    }
                }
                catch { }
            }
        }
        */

        /// <summary>
        /// 무기 장착 시 이동속도 SE 재적용 (단검 재장착 복구용)
        /// </summary>
        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))]
        public static class StatTree_Humanoid_EquipItem_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Humanoid __instance, bool __result)
            {
                if (!__result) return;
                if (__instance is Player player && player == Player.m_localPlayer)
                {
                    SkillEffect.NotifyKnifeMoveSpeedActive(player);
                }
            }
        }

        /// <summary>
        /// 방어 트리 회피율 재계산 (스킬 변경 시 호출)
        /// </summary>
        public static void UpdateDefenseDodgeRate(Player player)
        {
            if (player == null) return;

            var manager = SkillTreeManager.Instance;
            if (manager == null)
            {
                player.SetCustomDodgeChance(0f);
                return;
            }

            float totalDodge = 0f;

            int defenseRootLevel = manager.GetSkillLevel("defense_root");
            bool hasDefenseRoot = defenseRootLevel > 0;

            if (hasDefenseRoot)
            {
                // defense_Step3_agile: 회피단련
                int agileLevel = manager.GetSkillLevel("defense_Step3_agile");
                if (agileLevel > 0)
                {
                    totalDodge += Defense_Config.AgileDodgeBonusValue / 100f;
                }

                // defense_Step5_stamina: 기민함
                int staminaLevel = manager.GetSkillLevel("defense_Step5_stamina");
                if (staminaLevel > 0)
                {
                    totalDodge += Defense_Config.StaminaDodgeBonusValue / 100f;
                }

                // defense_Step6_attack: 신경강화 (45초 미발동 조건)
                int attackLevel = manager.GetSkillLevel("defense_Step6_attack");
                if (attackLevel > 0)
                {
                    bool isInCooldown = nerveLastEvasionTime.ContainsKey(player) &&
                                        Time.time - nerveLastEvasionTime[player] < 45f;
                    if (!isInCooldown)
                        totalDodge += Defense_Config.AttackDodgeBonusValue / 100f;
                }
            }

            // === 단검 전문가 회피율 ===
            // knife_step2_evasion: 회피 숙련 (방어 트리와 독립적으로 적용)
            int knifeEvasionLevel = manager.GetSkillLevel("knife_step2_evasion");
            if (knifeEvasionLevel > 0)
            {
                float knifeEvasionBonus = Knife_Config.KnifeEvasionBonusValue / 100f;
                totalDodge += knifeEvasionBonus;
                Plugin.Log.LogDebug($"[회피 숙련] 단검 회피율 +{Knife_Config.KnifeEvasionBonusValue}%");
            }

            // knife_step5_crit_rate: 공격과 회피 (2연속 공격 시 일시적 회피율 증가)
            float attackEvasionBonus = GetKnifeAttackEvasionBonus(player);
            if (attackEvasionBonus > 0f)
            {
                totalDodge += attackEvasionBonus / 100f;
                Plugin.Log.LogDebug($"[공격과 회피] 단검 임시 회피율 +{attackEvasionBonus}%");
            }

            // spear_Step2_evasion: 회피 찌르기 (창 공격 후 5초간 회피율 버프 - 버프 활성 시 항상 적용)
            if (IsSpearEvasionBuffActive(player))
            {
                totalDodge += Spear_Config.SpearStep3EvasionBonusValue / 100f;
                Plugin.Log.LogDebug($"[회피 찌르기] 창 회피율 +{Spear_Config.SpearStep3EvasionBonusValue}%");
            }

            // === 로그 직업 회피율 ===
            int rogueLevel = manager.GetSkillLevel("Rogue");
            if (rogueLevel > 0)
            {
                float rogueDodge = RogueSkills.GetRogueDodgeChance(rogueLevel) / 100f;
                totalDodge += rogueDodge;
                Plugin.Log.LogDebug($"[로그 패시브] 회피율 +{RogueSkills.GetRogueDodgeChance(rogueLevel)}%");
            }

            player.SetCustomDodgeChance(totalDodge);
        }

        /// <summary>
        /// 플레이어 스폰 시 초기 회피율 설정
        /// </summary>
        [HarmonyPatch(typeof(Player), "Awake")]
        public static class Player_Awake_DefenseDodge_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance)
            {
                // 로컬 플레이어만 처리
                if (__instance != Player.m_localPlayer) return;

                // 초기 회피율 설정
                UpdateDefenseDodgeRate(__instance);
            }
        }

        [HarmonyPatch(typeof(Player), "OnDestroy")]
        public static class StatTree_Player_OnDestroy_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Prefix(Player __instance)
            {
                if (statTreeStatusEffectApplied.ContainsKey(__instance))
                {
                    statTreeStatusEffectApplied.Remove(__instance);
                }
                if (nerveLastEvasionTime.ContainsKey(__instance))
                {
                    nerveLastEvasionTime.Remove(__instance);
                }
            }
        }
    }

    /// <summary>
    /// 신경강화 스킬의 45초 쿨다운 타이머 (피격 회피 발동 시 플레이어 GameObject에 부착)
    /// </summary>
    internal class NerveEnhancementTimer : MonoBehaviour
    {
        private Coroutine _restoreCoroutine;

        public void ResetTimer(Player player)
        {
            if (_restoreCoroutine != null) StopCoroutine(_restoreCoroutine);
            _restoreCoroutine = StartCoroutine(RestoreAfterDelay(player));
        }

        private IEnumerator RestoreAfterDelay(Player player)
        {
            yield return new WaitForSeconds(45f);
            SkillEffect.nerveLastEvasionTime.Remove(player);
            SkillEffect.UpdateDefenseDodgeRate(player);
            _restoreCoroutine = null;
        }
    }
}

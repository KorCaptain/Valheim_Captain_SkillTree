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
        /// </summary>
        [HarmonyPatch(typeof(Character), nameof(Character.GetBodyArmor))]
        public static class Character_GetBodyArmor_StatTree_Patch
        {
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

                    // defense_root: 방어력 +2
                    if (manager.GetSkillLevel("defense_root") > 0)
                    {
                        armorBonus += Defense_Config.DefenseRootArmorBonusValue;
                    }

                    // defense_Step1_survival: 방어력 +3
                    if (manager.GetSkillLevel("defense_Step1_survival") > 0)
                    {
                        armorBonus += Defense_Config.SurvivalArmorBonusValue;
                    }

                    // defense_Step2_health: 방어력 +4
                    if (manager.GetSkillLevel("defense_Step2_health") > 0)
                    {
                        armorBonus += Defense_Config.HealthArmorBonusValue;
                    }

                    if (armorBonus > 0)
                    {
                        __result += armorBonus;
                        Plugin.Log.LogDebug($"[스탯 트리] 방어력 보너스 적용: +{armorBonus} (총 방어력: {__result})");
                    }
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
                    {
                        float bonus = Defense_Config.DefenseRootHealthBonusValue;
                        healthBonus += bonus;
                        Plugin.Log.LogDebug($"[방어→음식] defense_root 체력: +{bonus}");
                    }

                    // defense_Step1_survival: 피부경화 체력 보너스
                    if (manager.GetSkillLevel("defense_Step1_survival") > 0)
                    {
                        float bonus = Defense_Config.SurvivalHealthBonusValue;
                        healthBonus += bonus;
                        Plugin.Log.LogDebug($"[방어→음식] 피부경화 체력: +{bonus}");
                    }

                    // defense_Step2_health: 체력단련 체력 보너스
                    if (manager.GetSkillLevel("defense_Step2_health") > 0)
                    {
                        float bonus = Defense_Config.HealthBonusValue;
                        healthBonus += bonus;
                        Plugin.Log.LogDebug($"[방어→음식] 체력단련 체력: +{bonus}");
                    }

                    // defense_Step3_boost: 체력증강 체력 보너스
                    if (manager.GetSkillLevel("defense_Step3_boost") > 0)
                    {
                        float bonus = Defense_Config.BoostHealthBonusValue;
                        healthBonus += bonus;
                        Plugin.Log.LogDebug($"[방어→음식] 체력증강 체력: +{bonus}");
                    }

                    // defense_Step6_body: 요툰의 생명력 - 체력 +30% (비율→고정값 변환)
                    // ✅ 힐링 깜빡임 방지: 비율 보너스를 고정 수치로 변환하여 m_baseHP에 포함
                    if (manager.GetSkillLevel("defense_Step6_body") > 0)
                    {
                        float baseHealthBeforeBonus = hp + healthBonus;  // 현재까지의 총 기본 체력
                        float bonusPercent = Defense_Config.BodyHealthBonusValue / 100f;  // 30% = 0.3
                        float bonusHealth = baseHealthBeforeBonus * bonusPercent;  // 고정 체력 계산
                        healthBonus += bonusHealth;  // m_baseHP에 포함
                        Plugin.Log.LogDebug($"[요툰의 생명력→음식] 기본 체력 +{Defense_Config.BodyHealthBonusValue}%: {baseHealthBeforeBonus:F0} * {bonusPercent:F2} = +{bonusHealth:F0}");
                    }

                    if (healthBonus > 0)
                    {
                        hp += healthBonus;
                        Plugin.Log.LogDebug($"[스탯 트리] 체력 보너스 적용: +{healthBonus} (최종 최대 체력: {hp})");
                    }
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
            public static void Postfix(ref float stamina)
            {
                try
                {
                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    float staminaBonus = 0f;

                    // defense_Step2_dodge: 심신단련 스태미나 보너스
                    if (manager.GetSkillLevel("defense_Step2_dodge") > 0)
                    {
                        float bonus = Defense_Config.DodgeStaminaBonusValue;
                        staminaBonus += bonus;
                        Plugin.Log.LogDebug($"[방어→음식] 심신단련 스태미나: +{bonus}");
                    }

                    // speed_2: 스태미나 +15
                    if (manager.GetSkillLevel("speed_2") > 0)
                        staminaBonus += SkillTreeConfig.SpeedEnduranceStaminaBonusValue;

                    if (staminaBonus > 0)
                    {
                        stamina += staminaBonus;
                        Plugin.Log.LogDebug($"[스탯 트리] 스태미나 보너스 적용: +{staminaBonus} (최종 최대 스태미나: {stamina})");
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
                    {
                        eitr += eitrBonus;
                        Plugin.Log.LogDebug($"[스탯 트리] 에이트르 보너스 적용: +{eitrBonus} (최종 최대 에이트르: {eitr})");
                    }
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
        /// 7. speed_1: 이동속도 +2%
        /// StatusEffect 방식으로 이동속도 적용 (Speed.cs 패턴 참조)
        /// </summary>
        public class SE_StatTreeSpeed : SE_Stats
        {
            public SE_StatTreeSpeed()
            {
                m_name = "스탯 트리 속도 강화";
                m_tooltip = "스탯 트리로 인한 이동속도 증가";
                m_ttl = 0f; // 무한 지속
                m_icon = null;
            }

            public override void Setup(Character character)
            {
                base.Setup(character);

                if (character is Player player)
                {
                    var manager = SkillTreeManager.Instance;
                    if (manager != null)
                    {
                        float totalSpeedBonus = 0f;

                        // speed_1: 속도 전문가 트리
                        if (manager.GetSkillLevel("speed_1") > 0)
                        {
                            totalSpeedBonus += SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue / 100f;
                        }

                        // knife_step3_move_speed: 단검 빠른 움직임 (조건부 - 단검 착용 시만)
                        if (manager.GetSkillLevel("knife_step3_move_speed") > 0 &&
                            SkillEffect.IsUsingDagger(player))
                        {
                            totalSpeedBonus += Knife_Config.KnifeMoveSpeedBonusValue / 100f;
                        }

                        if (totalSpeedBonus > 0f)
                        {
                            m_speedModifier = 1f + totalSpeedBonus;
                            m_tooltip = $"이동속도 +{totalSpeedBonus * 100f:F1}%";

                            Plugin.Log.LogInfo($"[이동속도] StatusEffect 적용: {player.GetPlayerName()} " +
                                $"+{totalSpeedBonus * 100f:F1}% (배율: {m_speedModifier:F3})");
                        }
                    }
                }
            }

            public override void UpdateStatusEffect(float dt)
            {
                base.UpdateStatusEffect(dt);

                // 스킬 레벨 변화 감지
                if (m_character is Player player)
                {
                    var manager = SkillTreeManager.Instance;
                    if (manager == null)
                    {
                        m_character.GetSEMan()?.RemoveStatusEffect(this);
                        return;
                    }

                    float totalSpeedBonus = 0f;

                    // speed_1: 속도 전문가 트리
                    if (manager.GetSkillLevel("speed_1") > 0)
                    {
                        totalSpeedBonus += SkillTreeConfig.SpeedDexterityMoveSpeedBonusValue / 100f;
                    }

                    // knife_step3_move_speed: 단검 빠른 움직임 (조건부 - 단검 착용 시만)
                    if (manager.GetSkillLevel("knife_step3_move_speed") > 0 &&
                        SkillEffect.IsUsingDagger(player))
                    {
                        totalSpeedBonus += Knife_Config.KnifeMoveSpeedBonusValue / 100f;
                    }

                    // 모든 이동속도 스킬 해제 시 StatusEffect 제거
                    if (totalSpeedBonus <= 0f)
                    {
                        m_character.GetSEMan()?.RemoveStatusEffect(this);
                        return;
                    }

                    float newModifier = 1f + totalSpeedBonus;

                    if (Mathf.Abs(m_speedModifier - newModifier) > 0.001f)
                    {
                        m_speedModifier = newModifier;
                        m_tooltip = $"이동속도 +{totalSpeedBonus * 100f:F1}%";
                        Plugin.Log.LogDebug($"[이동속도] StatusEffect 업데이트: +{totalSpeedBonus * 100f:F1}%");
                    }
                }
            }
        }

        /// <summary>
        /// StatusEffect 관리 시스템
        /// </summary>
        public static class StatTreeStatusEffectManager
        {
            public static void ApplyStatTreeSpeedEffect(Player player)
            {
                if (player == null || player != Player.m_localPlayer) return;

                var seMan = player.GetSEMan();
                if (seMan == null) return;

                var manager = SkillTreeManager.Instance;
                bool hasSpeed1 = manager != null && manager.GetSkillLevel("speed_1") > 0;
                var hash = typeof(SE_StatTreeSpeed).Name.GetStableHashCode();
                bool hasEffect = seMan.HaveStatusEffect(hash);

                try
                {
                    if (hasSpeed1 && !hasEffect)
                    {
                        // StatusEffect 추가
                        var se = ScriptableObject.CreateInstance<SE_StatTreeSpeed>();
                        seMan.AddStatusEffect(se);
                        Plugin.Log.LogInfo($"[스탯 트리] StatusEffect 활성화: {player.GetPlayerName()}");
                    }
                    else if (!hasSpeed1 && hasEffect)
                    {
                        // StatusEffect 제거
                        seMan.RemoveStatusEffect(hash);
                        Plugin.Log.LogInfo($"[스탯 트리] StatusEffect 비활성화: {player.GetPlayerName()}");
                    }
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[스탯 트리] StatusEffect 관리 오류: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Player 생명주기 관리 - StatusEffect 적용
        /// </summary>
        [HarmonyPatch(typeof(Player), "Awake")]
        public static class StatTree_Player_Awake_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance)
            {
                if (__instance == Player.m_localPlayer)
                {
                    StatTreeStatusEffectManager.ApplyStatTreeSpeedEffect(__instance);
                    Plugin.Log.LogInfo($"[스탯 트리] 플레이어 {__instance.GetPlayerName()} 스탯 트리 시스템 초기화 (Awake)");
                }
            }
        }

        [HarmonyPatch(typeof(Player), "Start")]
        public static class StatTree_Player_Start_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix(Player __instance)
            {
                if (__instance == Player.m_localPlayer)
                {
                    __instance.StartCoroutine(DelayedStatTreeEffectApplication(__instance));
                }
            }

            private static IEnumerator DelayedStatTreeEffectApplication(Player player)
            {
                // 2초 대기 (스킬트리 데이터 완전 로딩 대기)
                yield return new WaitForSeconds(2f);

                if (player == null || player.IsDead())
                {
                    yield break;
                }

                if (player == Player.m_localPlayer)
                {
                    StatTreeStatusEffectManager.ApplyStatTreeSpeedEffect(player);
                }
            }
        }

        [HarmonyPatch(typeof(Game), "Start")]
        public static class StatTree_Game_Start_Patch
        {
            [HarmonyPriority(Priority.Low)]
            public static void Postfix()
            {
                Plugin.Instance.StartCoroutine(DelayedGameStatTreeEffectApplication());
            }

            private static IEnumerator DelayedGameStatTreeEffectApplication()
            {
                // 5초 대기 (게임 완전 초기화 대기)
                yield return new WaitForSeconds(5f);

                var player = Player.m_localPlayer;

                if (player == null || player.IsDead())
                {
                    yield break;
                }

                if (player != null)
                {
                    StatTreeStatusEffectManager.ApplyStatTreeSpeedEffect(player);
                    Plugin.Log.LogInfo($"[스탯 트리] 게임 초기화 완료 후 스탯 트리 시스템 적용: {player.GetPlayerName()}");
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

                // defense_Step6_attack: 신경강화
                int attackLevel = manager.GetSkillLevel("defense_Step6_attack");
                if (attackLevel > 0)
                {
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
            }
        }
    }
}

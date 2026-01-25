using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.SkillTree;
using CaptainSkillTree.SkillTree.CriticalSystem;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Audio;

namespace CaptainSkillTree
{
    /// <summary>
    /// Plugin 클래스의 Harmony 패치 부분
    /// </summary>
    public partial class Plugin
    {
        // 치명타 시스템 패치 (모든 무기 지원)
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        [HarmonyPriority(Priority.Normal)]
        public static class WeaponCriticalSystemPatch
        {
            public static void Prefix(Character __instance, ref bool showDamageText, ref HitData hit)
            {
                if (Player.m_localPlayer == null) return;
                var weapon = Player.m_localPlayer.GetCurrentWeapon();
                if (weapon == null) return;

                var player = Player.m_localPlayer;
                var weaponType = weapon.m_shared.m_skillType;

                // === 창 연공창 액티브 스킬 (G키) - 280% 데미지 ===
                if (weaponType == Skills.SkillType.Spears)
                {
                    if (SkillEffect.spearEnhancedThrowBuffEndTime.TryGetValue(player, out float endTime)
                        && Time.time < endTime)
                    {
                        // 연공창 버프 활성화 중 - 280% 데미지 적용
                        float damageBonus = SkillTreeConfig.SpearStep6ComboDamageValue;
                        float multiplier = 1f + (damageBonus / 100f);

                        // 물리 데미지 4종 (Rule 11 준수)
                        hit.m_damage.m_pierce *= multiplier;
                        hit.m_damage.m_blunt *= multiplier;
                        hit.m_damage.m_slash *= multiplier;
                        hit.m_damage.m_chop *= multiplier;

                        Log.LogInfo($"[연공창] ✅ 강화된 투창 데미지 +{damageBonus}% 적용!");

                        // 몬스터 맞았을 때 confetti 효과
                        if (__instance != null && !__instance.IsPlayer())
                        {
                            Vector3 monsterPos = __instance.transform.position + Vector3.up * 1f;
                            SimpleVFX.Play("confetti_directional_multicolor", monsterPos, 2f);
                            Log.LogDebug("[연공창] confetti 효과 재생");
                        }

                        // 버프 종료
                        SkillEffect.spearEnhancedThrowBuffEndTime.Remove(player);
                        return; // 투창 전문가 패시브와 중복 방지
                    }
                }

                // === 창 투창 전문가 패시브 적용 ===
                if (weaponType == Skills.SkillType.Spears && SkillEffect.HasSkill("spear_Step1_throw"))
                {
                    // 쿨타임 확인
                    if (SkillEffect.CanUseSpearThrowPassive(player))
                    {
                        // 투창 공격력 보너스 적용
                        float damageBonus = SkillTreeConfig.SpearStep2ThrowDamageValue;
                        float multiplier = 1f + (damageBonus / 100f);

                        // 물리 데미지 4종 (Rule 11 준수)
                        hit.m_damage.m_pierce *= multiplier;
                        hit.m_damage.m_blunt *= multiplier;
                        hit.m_damage.m_slash *= multiplier;
                        hit.m_damage.m_chop *= multiplier;

                        Log.LogInfo($"[투창 전문가] ✅ 투창 공격력 +{damageBonus}% 적용!");
                        SkillEffect.ShowSkillEffectText(player, $"💥 투창 +{damageBonus}%!", new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Combat);

                        // 쿨타임 설정
                        SkillEffect.SetSpearThrowPassiveCooldown(player);
                    }
                }

                // === 치명타 시스템 (모듈화) ===
                float critChance = Critical.CalculateCritChance(player, weaponType);

                if (Critical.RollCritical(critChance))
                {
                    float critMultiplier = CriticalDamage.CalculateCritDamageMultiplier(player, weaponType);
                    CriticalDamage.ApplyCriticalDamage(player, ref hit, critMultiplier, weaponType);
                    showDamageText = false;
                }

                // === knife_stagger 제거됨 - 실제 스킬 트리에 존재하지 않음 ===
                // 암살술(knife_step8_assassination)이 비틀거림 효과를 처리함
            }
        }

        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyAttackStaminaUsage))]
        public static class KnifeSkillTreeStaminaPatch
        {
            public static void Postfix(SEMan __instance, ref float staminaUse)
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                var weapon = player.GetCurrentWeapon();
                if (weapon == null || weapon.m_shared.m_skillType != Skills.SkillType.Knives) return;
                float reduction = SkillEffect.GetKnifeStaminaReduction(0f);
                staminaUse *= (1f - reduction / 100f);
            }
        }

        // 단검 공격 속도 패치 - 적 처치 후 일정 시간 동안만 공격속도 증가 효과 적용 (무기 착용 기반)
        [HarmonyPatch(typeof(CharacterAnimEvent), nameof(CharacterAnimEvent.CustomFixedUpdate))]
        public static class KnifeAttackSpeedAnimatorPatch
        {
            private static readonly Dictionary<Player, float> lastSpeedApplied = new Dictionary<Player, float>();

            public static void Postfix(CharacterAnimEvent __instance)
            {
                try
                {
                    // 로컬 플레이어만 처리
                    var player = Player.m_localPlayer;
                    if (player == null) return;

                    // CharacterAnimEvent에서 플레이어의 애니메이터인지 확인
                    var playerAnimator = player.GetComponentInChildren<Animator>();
                    if (playerAnimator == null || __instance.GetComponentInParent<Player>() != player) return;

                    // 단검을 착용하고 있는지 확인
                    var currentWeapon = player.GetCurrentWeapon();
                    if (currentWeapon?.m_shared?.m_skillType != Skills.SkillType.Knives)
                    {
                        // 단검이 아니면 속도를 기본값으로 복구
                        ResetAnimatorSpeed(player);
                        return;
                    }

                    // 스킬 보유 확인
                    if (!SkillEffect.HasSkill("knife_step4_attack_damage"))
                    {
                        ResetAnimatorSpeed(player);
                        return;
                    }

                    // 단검 데미지 버프 활성화 여부 확인 (AnimationSpeedManager 사용하지 않음)
                    // 단검 데미지 버프는 데미지 계산 시 직접 적용됨
                    if (SkillEffect.knifeDamageBonusEndTime.TryGetValue(player, out float endTime) && Time.time < endTime)
                    {
                        // 데미지 버프 활성 상태 - 별도 처리 불필요
                    }
                    else
                    {
                        // 버프 시간 종료
                    }
                }
                catch (Exception ex)
                {
                    Log.LogError($"[단검 데미지 버프] 상태 확인 오류: {ex.Message}");
                }
            }

            private static void ResetAnimatorSpeed(Player player)
            {
                var animator = player.GetComponentInChildren<Animator>();
                if (animator == null) return;

                // 이미 기본 속도면 건드리지 않음
                if (Mathf.Abs(animator.speed - 1f) < 0.01f) return;

                animator.speed = 1f;
                lastSpeedApplied.Remove(player);
                Log.LogDebug($"[단검 공격속도] {player.GetPlayerName()} 애니메이터 속도 복구: 1.0 (버프 해제)");
            }
        }

        // 스태미나 재생 누적
        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyStaminaRegen))]
        public static class KnifeSkillTreeStaminaRegenPatch
        {
            public static void Postfix(SEMan __instance, ref float staminaMultiplier)
            {
                var player = Player.m_localPlayer;
                if (player == null) return;
                staminaMultiplier += SkillEffect.GetStaminaRegen(0f) / 100f;
            }
        }

        // 달리기 스태미나 감소 누적
        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyRunStaminaDrain))]
        public static class KnifeSkillTreeRunStaminaPatch
        {
            public static void Postfix(ref float drain)
            {
                float reduction = SkillEffect.GetStaminaReduction(0f);
                drain *= (1f - reduction / 100f);
            }
        }

        // 점프 스태미나 감소 누적
        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyJumpStaminaUsage))]
        public static class KnifeSkillTreeJumpStaminaPatch
        {
            public static void Postfix(ref float staminaUse)
            {
                // 기존 칼 스킬 점프 스태미나 감소
                float knifeReduction = SkillEffect.GetStaminaReduction(0f);
                staminaUse *= (1f - knifeReduction / 100f);

                // 점프 숙련자 스킬 점프 스태미나 감소
                float jumpExpertReduction = SkillEffect.GetJumpStaminaReduction();
                staminaUse *= (1f - jumpExpertReduction / 100f);
            }
        }

        // 물리/마법 방어 누적 (ApplyDamage에서 동시 적용)
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        [HarmonyPriority(Priority.High)]
        public static class KnifeSkillTreeArmorPatch
        {
            public static void Prefix(Character __instance, HitData hit)
            {
                if (!__instance.IsPlayer()) return;
                // 물리 방어
                float addPhys = SkillEffect.GetPhysicArmor(0f);
                var valuePhys = 1 - addPhys / 100f;
                hit.m_damage.m_blunt *= valuePhys;
                hit.m_damage.m_slash *= valuePhys;
                hit.m_damage.m_pierce *= valuePhys;
                hit.m_damage.m_chop *= valuePhys;
                hit.m_damage.m_pickaxe *= valuePhys;
                // 마법 방어
                float addMagic = SkillEffect.GetMagicArmor(0f);
                var valueMagic = 1 - addMagic / 100f;
                hit.m_damage.m_fire *= valueMagic;
                hit.m_damage.m_frost *= valueMagic;
                hit.m_damage.m_lightning *= valueMagic;
                hit.m_damage.m_poison *= valueMagic;
                hit.m_damage.m_spirit *= valueMagic;
            }
        }

        [HarmonyPatch(typeof(Character), "BlockAttack")]
        public static class SwordSkillTreeParryPatch
        {
            public static void Postfix(Character __instance, bool __result)
            {
                if (__result && __instance is Player player && player.IsPlayer())
                {
                    var currentWeapon = player.GetCurrentWeapon();
                    if (currentWeapon == null || currentWeapon.m_shared.m_skillType != Skills.SkillType.Swords) return;

                    // 패링 스택 시스템 호출 (검 전문가 스킬 보유 시)
                    Sword_Skill.OnParrySuccess(player);

                    var manager = SkillTreeManager.Instance;
                    var seman = player.GetSEMan();

                    if (manager.GetSkillLevel("sword_counter") > 0)
                    {
                        var effect = ScriptableObject.CreateInstance<SE_SwordCounter>();
                        effect.m_ttl = 5f;
                        seman.AddStatusEffect(effect, true);
                    }

                    if (manager.GetSkillLevel("sword_riposte") > 0)
                    {
                        var effect = ScriptableObject.CreateInstance<SE_SwordRiposte>();
                        effect.m_ttl = 5f;
                        seman.AddStatusEffect(effect, true);
                    }
                }
            }
        }

        public class SE_SwordCounter : StatusEffect
        {
            public SE_SwordCounter()
            {
                m_name = "칼날 되치기";
                m_tooltip = "다음 공격의 피해량이 20% 증가합니다.";
                m_icon = null;
                m_ttl = 5f;
            }

            public override void ModifyAttack(Skills.SkillType skill, ref HitData hitData)
            {
                if (skill == Skills.SkillType.Swords)
                {
                    hitData.m_damage.Modify(1.2f);
                    m_character.GetSEMan().RemoveStatusEffect(this, true);
                }
            }
        }

        public class SE_SwordRiposte : StatusEffect
        {
            public SE_SwordRiposte()
            {
                m_name = "칼날 되치기";
                m_tooltip = "다음 공격의 피해량이 20% 증가합니다.";
                m_icon = null;
            }
        }

        // SE_SwordRiposte의 실제 효과를 적용하기 위한 별도의 패치
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        public static class SwordRiposteDamagePatch
        {
            private static readonly int SwordCounterHash = Animator.StringToHash("칼날 되치기");
            private static readonly int SwordRiposteHash = Animator.StringToHash("반격 자세");

            private static void Prefix(Character __instance, HitData hit)
            {
                if (hit.GetAttacker() is Player player)
                {
                    var seman = player.GetSEMan();
                    if (seman.HaveStatusEffect(SwordRiposteHash))
                    {
                        hit.m_damage.m_blunt *= 1.2f;
                        hit.m_damage.m_slash *= 1.2f;
                        hit.m_damage.m_pierce *= 1.2f;
                        seman.RemoveStatusEffect(SwordRiposteHash);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Hide))]
        public static class InventoryHidePatch
        {
            public static void Postfix()
            {
                try
                {
                    if (skillTreeUI != null && skillTreeUI.panel != null && skillTreeUI.panel.activeSelf)
                    {
                        skillTreeUI.panel.SetActive(false);

                        // 인벤토리 닫을 때도 BGM 일시정지 및 발헤임 음악 복원
                        if (SkillTreeBGMManager.Instance != null)
                        {
                            SkillTreeBGMManager.Instance.PauseSkillTreeBGM();
                        }
                    }

                    // 아이콘이 비활성화되지 않도록 보장
                    if (skillTreeIconObj != null && !skillTreeIconObj.activeSelf)
                    {
                        Log.LogWarning("[스킬트리] 아이콘이 비활성화됨, 다시 활성화 시도");
                        // 인벤토리가 닫혔을 때는 아이콘을 숨김 (정상 동작)
                        skillTreeIconObj.SetActive(false);
                    }
                }
                catch (Exception ex)
                {
                    Log.LogError($"[스킬트리] InventoryHidePatch 오류: {ex.Message}");
                }
            }
        }

        // ZNet 초기화 완료 시 서버 싱크 시스템 초기화 (타이밍 이슈 해결)
        [HarmonyPatch(typeof(ZNet), "Awake")]
        public static class ZNet_Awake_Patch
        {
            static void Postfix()
            {
                InitializeServerSync();
            }
        }

        // 클라이언트에서 서버 설정 수신
        [HarmonyPatch(typeof(ZNet), "OnNewConnection")]
        public static class ZNet_OnNewConnection_Patch
        {
            static void Postfix(ZNet __instance)
            {
                // 새 클라이언트가 접속하면 서버 설정 전송
                if (__instance.IsServer())
                {
                    // 약간의 지연 후 설정 전송 (클라이언트 초기화 대기)
                    __instance.StartCoroutine(DelayedConfigBroadcast());
                }
            }

            private static IEnumerator DelayedConfigBroadcast()
            {
                yield return new WaitForSeconds(2f);
                SkillTreeConfig.BroadcastConfigToClients();
            }
        }

        // 콘솔 명령어 등록
        [HarmonyPatch(typeof(Terminal), "InitTerminal")]
        public static class Terminal_InitTerminal_Patch
        {
            static void Postfix()
            {
                // 공격 전문가 설정 명령어들
                new Terminal.ConsoleCommand("skilltree_attack_root", "공격 전문가 루트 데미지 보너스 설정 (예: skilltree_attack_root 7)",
                    args => SetAttackConfig("AttackRootDamageBonus", args));

                new Terminal.ConsoleCommand("skilltree_melee_chance", "근접 특화 발동 확률 설정 (예: skilltree_melee_chance 25)",
                    args => SetAttackConfig("AttackMeleeBonusChance", args));

                new Terminal.ConsoleCommand("skilltree_melee_damage", "근접 특화 피해량 설정 (예: skilltree_melee_damage 15)",
                    args => SetAttackConfig("AttackMeleeBonusDamage", args));

                new Terminal.ConsoleCommand("skilltree_bow_chance", "활 특화 발동 확률 설정 (예: skilltree_bow_chance 30)",
                    args => SetAttackConfig("AttackBowBonusChance", args));

                new Terminal.ConsoleCommand("skilltree_bow_damage", "활 특화 피해량 설정 (예: skilltree_bow_damage 18)",
                    args => SetAttackConfig("AttackBowBonusDamage", args));

                // 속도 전문가 설정 명령어들
                new Terminal.ConsoleCommand("skilltree_speed_root", "속도 전문가 루트 이동속도 설정 (예: skilltree_speed_root 5)",
                    args => SetSpeedConfig("Speed_Expert_MoveSpeed", args));

                new Terminal.ConsoleCommand("skilltree_speed_dodge", "구르기 속도 보너스 설정 (예: skilltree_speed_dodge 15)",
                    args => SetSpeedConfig("Speed_Step1_DodgeSpeed", args));

                new Terminal.ConsoleCommand("skilltree_speed_melee_combo", "근접 콤보 이동속도 설정 (예: skilltree_speed_melee_combo 6)",
                    args => SetSpeedConfig("Speed_Step2_MeleeComboBonus", args));

                new Terminal.ConsoleCommand("skilltree_speed_bow_hit", "활 적중 이동속도 설정 (예: skilltree_speed_bow_hit 8)",
                    args => SetSpeedConfig("Speed_Step2_BowHitBonus", args));

                new Terminal.ConsoleCommand("skilltree_speed_attack", "공격속도 증가 설정 (예: skilltree_speed_attack 10)",
                    args => SetSpeedConfig("Speed_Step8_MeleeAttackSpeed", args));

                new Terminal.ConsoleCommand("skilltree_config_reload", "설정 리로드 및 재전송",
                    args => SkillTreeConfig.ReloadAndBroadcast());

                new Terminal.ConsoleCommand("skilltree_config_show", "현재 설정 표시",
                    args => ShowCurrentConfig());

                // 아처 멀티샷 설정 명령어들
                new Terminal.ConsoleCommand("skilltree_archer_arrows", "아처 멀티샷 화살 수 설정 (예: skilltree_archer_arrows 7)",
                    args => SetArcherConfig("Archer_MultiShot_ArrowCount", args));

                new Terminal.ConsoleCommand("skilltree_archer_consume", "아처 멀티샷 화살 소모량 설정 (예: skilltree_archer_consume 2)",
                    args => SetArcherConfig("Archer_MultiShot_ArrowConsumption", args));

                new Terminal.ConsoleCommand("skilltree_archer_damage", "아처 멀티샷 데미지 비율 설정 (예: skilltree_archer_damage 80)",
                    args => SetArcherConfig("Archer_MultiShot_DamagePercent", args));

                // 활 전문가 멀티샷 설정 명령어들
                new Terminal.ConsoleCommand("skilltree_bow_lv1_chance", "활 전문가 멀티샷 Lv1 확률 설정 (예: skilltree_bow_lv1_chance 15)",
                    args => SetBowConfig("Bow_MultiShot_Lv1_Chance", args));

                new Terminal.ConsoleCommand("skilltree_bow_lv2_chance", "활 전문가 멀티샷 Lv2 확률 설정 (예: skilltree_bow_lv2_chance 36)",
                    args => SetBowConfig("Bow_MultiShot_Lv2_Chance", args));

                new Terminal.ConsoleCommand("skilltree_bow_arrows", "활 전문가 멀티샷 화살 수 설정 (예: skilltree_bow_arrows 2)",
                    args => SetBowConfig("Bow_MultiShot_ArrowCount", args));

                new Terminal.ConsoleCommand("skilltree_bow_consume", "활 전문가 멀티샷 화살 소모량 설정 (예: skilltree_bow_consume 0)",
                    args => SetBowConfig("Bow_MultiShot_ArrowConsumption", args));

                new Terminal.ConsoleCommand("skilltree_bow_damage", "활 전문가 멀티샷 데미지 비율 설정 (예: skilltree_bow_damage 70)",
                    args => SetBowConfig("Bow_MultiShot_DamagePercent", args));

                // skilladd, skillreset 명령어는 Jotunn CommandManager로 이동됨 (자동완성 지원)
                // RegisterJotunnCommands() 메서드에서 등록
            }
        }
    }

    /// <summary>
    /// 플레이어 사망 시 스킬/VFX 정리 후 코루틴 강제 정리
    /// 무한 로딩 버그 방지를 위한 안전장치 (정리 순서 중요!)
    /// </summary>
    [HarmonyPatch(typeof(Player), "OnDeath")]
    public static class Player_OnDeath_StopPluginCoroutines_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Player __instance)
        {
            if (__instance == Player.m_localPlayer && Plugin.Instance != null)
            {
                // ✅ 1. 먼저 탱커 VFX/코루틴 즉시 정리 (코루틴 중지 전에 실행!)
                try
                {
                    TankerSkills.CleanupTankerOnDeath(__instance);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[탱커 정리] 실패 (무시): {ex.Message}");
                }

                // ✅ 2. 직업 스킬 정리
                try
                {
                    JobSkills.CleanupAllJobSkillsOnDeath(__instance);
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[스킬 정리] 실패 (무시): {ex.Message}");
                }

                // ✅ 3. 마지막으로 코루틴 중지 (모든 정리 완료 후)
                Plugin.Log.LogInfo("[플레이어 사망] 모든 정리 완료 후 코루틴 중지");
                Plugin.Instance.StopAllCoroutines();
            }
        }
    }

    /// <summary>
    /// Game.Awake 패치 - AnimationSpeedManager에 공격속도 핸들러 등록
    /// EpicLoot의 ModifyAttackSpeed 패턴 준수
    /// </summary>
    [HarmonyPatch(typeof(Game), "Awake")]
    public static class CaptainSkillTree_AnimationSpeedManager_Patch
    {
        private static bool _attackSpeedHandlerRegistered = false;

        [HarmonyPostfix]
        public static void Postfix(Game __instance)
        {
            if (_attackSpeedHandlerRegistered) return;  // 중복 방지

            try
            {
                // ✅ 직접 호출 방식 (DLL 참조)
                AnimationSpeedManager.Add((character, speed) =>
                {
                    if (character is Player player && player.InAttack())
                    {
                        float attackSpeedBonus =
                            SkillEffect.GetTotalAttackSpeedBonus(player);

                        if (attackSpeedBonus > 0f)
                        {
                            double bonusMultiplier = 1.0 + (attackSpeedBonus / 100.0);
                            double modifiedSpeed = speed * bonusMultiplier;

                            return modifiedSpeed;
                        }
                    }
                    return speed;
                });

                _attackSpeedHandlerRegistered = true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[공격 속도] AnimationSpeedManager 등록 실패: {ex.Message}");
            }
        }
    }
}

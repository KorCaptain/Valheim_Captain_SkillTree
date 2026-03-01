using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree
{
    /// <summary>
    /// 신경강화 시스템 - 회피/이동속도 버프 관리
    /// </summary>
    public static class NerveEnhancementSystem
    {
        // 플레이어별 마지막 버프 발동 시간 (쿨타임용)
        private static readonly Dictionary<long, float> lastTriggeredTime = new Dictionary<long, float>();
        // 플레이어별 커스텀 회피율 (기본 시스템에 없으므로 자체 관리)
        private static readonly Dictionary<long, float> dodgeChanceMap = new Dictionary<long, float>();
        // 버프 지속시간 및 쿨타임
        private const float BuffDuration = 4f;
        private const float BuffCooldown = 8f;

        // 플레이어의 커스텀 회피율을 가져오는 확장 메소드
        public static float GetCustomDodgeChance(this Player player)
        {
            return dodgeChanceMap.TryGetValue(player.GetPlayerID(), out var value) ? value : 0f;
        }

        // 플레이어의 커스텀 회피율을 설정하는 확장 메소드
        public static void SetCustomDodgeChance(this Player player, float value)
        {
            dodgeChanceMap[player.GetPlayerID()] = value;
        }

        // 버프를 시도하는 메인 함수
        // ✅ 변경: 회피율은 이제 SkillEffect.StatTree에서 영구 적용하므로
        //    여기서는 이동속도 버프만 적용 (선택적)
        public static void TryApplyDodgeAndSpeedBuff(Player player)
        {
            // 신경강화 스킬의 회피율은 SkillEffect.StatTree에서 영구 적용
            if (SkillTreeManager.Instance.GetSkillLevel("defense_Step6_attack") <= 0) return;
        }

        // 이동속도 버프를 위한 간단한 상태 효과 (SE_Stats 상속)
        public class SE_NerveEnhancement : SE_Stats
        {
            public SE_NerveEnhancement()
            {
                m_name = "신경 강화";
                m_tooltip = "이동속도 +12%";
                m_ttl = BuffDuration;
                m_speedModifier = 1.12f;
            }
        }

        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Damage_Patch
        {
            // Prefix는 데미지 계산 전에 실행
            private static bool Prefix(Character __instance, HitData hit)
            {
                // 로직 1: 내가 공격을 받았을 때 (회피 및 탱커 패시브 피해 감소 적용)
                if (__instance.IsPlayer())
                {
                    var player = __instance as Player;

                    // 수호자의 진심: 원본 데미지 저장 (막기 처리 전)
                    SkillEffect.SaveOriginalDamage(player, hit);

                    // 회피 적용
                    float dodgeChance = player.GetCustomDodgeChance();

                    if (dodgeChance > 0 && !player.IsBlocking())
                    {
                        float roll = UnityEngine.Random.Range(0f, 1f);

                        if (roll < dodgeChance)
                        {
                            hit.m_damage.m_blunt = 0;
                            hit.m_damage.m_slash = 0;
                            hit.m_damage.m_pierce = 0;
                            hit.m_damage.m_chop = 0;
                            hit.m_damage.m_pickaxe = 0;
                            hit.m_damage.m_fire = 0;
                            hit.m_damage.m_frost = 0;
                            hit.m_damage.m_lightning = 0;
                            hit.m_damage.m_poison = 0;
                            hit.m_damage.m_spirit = 0;
                            player.m_dodgeEffects.Create(player.transform.position, Quaternion.identity);

                            // 회피 성공 메시지 - 스킬명 표시
                            var manager = SkillTreeManager.Instance;
                            bool hasKnifeEvasion = manager?.GetSkillLevel("knife_step2_evasion") > 0;
                            bool hasDefenseEvasion = manager?.GetSkillLevel("defense_Step3_agile") > 0 ||
                                                     manager?.GetSkillLevel("defense_Step5_stamina") > 0 ||
                                                     manager?.GetSkillLevel("defense_Step6_attack") > 0;

                            string evasionMessage = "회피 성공!";
                            if (hasKnifeEvasion && hasDefenseEvasion)
                            {
                                evasionMessage = "🗡️ 회피 숙련 + 방어 트리 회피!";
                            }
                            else if (hasKnifeEvasion)
                            {
                                evasionMessage = "🗡️ 회피 숙련!";
                            }
                            else if (hasDefenseEvasion)
                            {
                                evasionMessage = "🛡️ 방어 트리 회피!";
                            }

                            player.Message(MessageHud.MessageType.Center, evasionMessage);

                            return false; // 데미지 적용 자체를 막음 (회피 성공)
                        }
                    }

                    // 탱커 패시브 피해 감소 적용
                    if (SkillTreeManager.Instance?.GetSkillLevel("Tanker") > 0)
                    {
                        var originalDamage = hit.GetTotalDamage();
                        if (originalDamage > 0f)
                        {
                            // 15% 피해 감소 적용
                            float reductionMultiplier = 1f - (Tanker_Config.TankerPassiveDamageReductionValue / 100f);

                            hit.m_damage.m_damage *= reductionMultiplier;
                            hit.m_damage.m_blunt *= reductionMultiplier;
                            hit.m_damage.m_slash *= reductionMultiplier;
                            hit.m_damage.m_pierce *= reductionMultiplier;
                            hit.m_damage.m_chop *= reductionMultiplier;
                            hit.m_damage.m_pickaxe *= reductionMultiplier;
                            hit.m_damage.m_fire *= reductionMultiplier;
                            hit.m_damage.m_frost *= reductionMultiplier;
                            hit.m_damage.m_lightning *= reductionMultiplier;
                            hit.m_damage.m_poison *= reductionMultiplier;
                            hit.m_damage.m_spirit *= reductionMultiplier;

                            var reducedDamage = hit.GetTotalDamage();
                            float damageReduced = originalDamage - reducedDamage;

                            if (damageReduced > 0f)
                            {
                                Plugin.Log.LogDebug($"[탱커 패시브] {player.GetPlayerName()} 피해 감소 적용 - 원래: {originalDamage:F1} → 감소후: {reducedDamage:F1} (감소량: {damageReduced:F1})");
                                player.Message(MessageHud.MessageType.TopLeft, $"탱커 방어: -{damageReduced:F0} 피해 차단!");
                            }
                        }
                    }
                }

                // 로직 2: 내가 공격을 성공시켰을 때 - 암살술 적용 (경직 상태 적 공격)
                var attacker = hit.GetAttacker();
                if (attacker != null && attacker.IsPlayer())
                {
                    var attackerPlayer = attacker as Player;

                    // 암살술: 경직 상태 적 공격 시 데미지 보너스
                    if (SkillTreeManager.Instance?.GetSkillLevel("knife_step8_assassination") > 0)
                    {
                        Knife_Skill.ApplyKnifeAssassinationBonus(attackerPlayer, __instance);
                    }
                }

                return true; // 정상적으로 데미지 적용
            }

            // Postfix는 데미지 계산 후에 실행
            private static void Postfix(Character __instance, HitData hit)
            {
                // 로직 2: 내가 공격을 성공시켰을 때 (버프 발동)
                var attacker = hit.GetAttacker();
                if (attacker != null && attacker.IsPlayer())
                {
                    TryApplyDodgeAndSpeedBuff(attacker as Player);
                }

                // 로직 3: 내가 공격을 받았을 때 - 발구르기 자동 발동 체크
                if (__instance.IsPlayer())
                {
                    var player = __instance as Player;
                    CheckStompAutoTrigger(player);
                }
            }

            /// <summary>
            /// 발구르기 자동 발동 체크 (체력 35% 이하, 120초 쿨타임)
            /// </summary>
            private static Dictionary<Player, float> stompCooldowns = new Dictionary<Player, float>();

            private static void CheckStompAutoTrigger(Player player)
            {
                if (player == null || player.IsDead())
                {
                    return;
                }

                // 스킬 보유 여부 확인
                if (SkillTreeManager.Instance?.GetSkillLevel("defense_Step4_instant") <= 0)
                {
                    return;
                }

                // 체력 비율 확인
                float healthPercent = player.GetHealthPercentage();
                float threshold = Defense_Config.StompHealthThresholdValue;

                if (healthPercent > threshold)
                {
                    return; // 체력이 임계값보다 높으면 발동 안 함
                }

                // 쿨타임 확인
                float cooldown = Defense_Config.StompCooldownValue;
                if (stompCooldowns.ContainsKey(player))
                {
                    float elapsed = Time.time - stompCooldowns[player];
                    if (elapsed < cooldown)
                    {
                        // 쿨타임 중 - 남은 시간 표시
                        float remaining = cooldown - elapsed;
                        player.Message(MessageHud.MessageType.Center, $"발구르기 쿨타임 ({remaining:F0}초 남음)");
                        return;
                    }
                }

                // 발구르기 실행
                SkillEffect.ExecuteStompSkill(player);

                // 쿨타임 기록
                stompCooldowns[player] = Time.time;
            }
        }
    }

    /// <summary>
    /// Plugin 클래스의 시스템 관련 부분 (서버 싱크, 터미널 설정, 공격 속도)
    /// </summary>
    public partial class Plugin
    {
        // === 서버 싱크 시스템 ===
        internal static void InitializeServerSync()
        {
            if (ZRoutedRpc.instance != null)
            {
                try
                {
                    // RPC 안전 등록 (이미 등록된 경우 예외 처리)
                    ZRoutedRpc.instance.Register<string>("CaptainSkillTree.SkillTreeMod_ConfigSync", RPC_ReceiveConfigSync);

                    // ❌ VFX RPC 초기화 비활성화 (EpicMMOSystem 방식 - 무한 로딩 방지)
                    // VFX.VFXManager.InitializeVFXRPC();
                }
                catch (ArgumentException ex)
                {
                    Log.LogWarning($"[서버 싱크] RPC가 이미 등록되어 있습니다: {ex.Message}");
                    // 계속 진행 - 다른 모드나 이전 인스턴스에서 등록했을 수 있음
                }
                catch (Exception ex)
                {
                    Log.LogError($"[서버 싱크] RPC 등록 중 예외 발생: {ex.Message}");
                }
            }
            else
            {
                Log.LogWarning("[서버 싱크] ZRoutedRpc.instance가 null입니다. 월드 로드 후 재시도 필요.");
            }
        }

        // 서버 설정 수신 RPC 핸들러
        private static void RPC_ReceiveConfigSync(long sender, string jsonData)
        {
            try
            {
                SkillTreeConfig.ReceiveServerConfig(jsonData);
            }
            catch (Exception ex)
            {
                Log.LogError($"[서버 싱크] 설정 수신 실패: {ex.Message}");
            }
        }

        #region Terminal Config Helpers

        internal static void SetAttackConfig(string key, Terminal.ConsoleEventArgs args)
        {
            if (args.Length < 2)
            {
                args.Context.AddString($"사용법: {args[0]} <값>");
                return;
            }

            if (float.TryParse(args[1], out float value))
            {
                if (SkillTreeConfig.SetConfigValue(key, value))
                {
                    args.Context.AddString($"[SkillTree] {key} = {value} 설정 완료");
                }
                else
                {
                    args.Context.AddString($"[SkillTree] 설정 실패 (서버만 가능)");
                }
            }
            else
            {
                args.Context.AddString($"[SkillTree] 잘못된 값: {args[1]}");
            }
        }

        internal static void SetSpeedConfig(string key, Terminal.ConsoleEventArgs args)
        {
            if (args.Length < 2)
            {
                args.Context.AddString($"사용법: {args[0]} <값>");
                return;
            }

            if (float.TryParse(args[1], out float value))
            {
                if (SkillTreeConfig.SetConfigValue(key, value))
                {
                    args.Context.AddString($"[SkillTree] {key} = {value} 설정 완료");
                }
                else
                {
                    args.Context.AddString($"[SkillTree] 설정 실패 (서버만 가능)");
                }
            }
            else
            {
                args.Context.AddString($"[SkillTree] 잘못된 값: {args[1]}");
            }
        }

        internal static void SetArcherConfig(string key, Terminal.ConsoleEventArgs args)
        {
            if (args.Length < 2)
            {
                args.Context.AddString($"사용법: {args[0]} <값>");
                return;
            }

            if (float.TryParse(args[1], out float value))
            {
                if (SkillTreeConfig.SetConfigValue(key, value))
                {
                    args.Context.AddString($"[SkillTree] {key} = {value} 설정 완료");
                }
                else
                {
                    args.Context.AddString($"[SkillTree] 설정 실패 (서버만 가능)");
                }
            }
            else
            {
                args.Context.AddString($"[SkillTree] 잘못된 값: {args[1]}");
            }
        }

        internal static void SetBowConfig(string key, Terminal.ConsoleEventArgs args)
        {
            if (args.Length < 2)
            {
                args.Context.AddString($"사용법: {args[0]} <값>");
                return;
            }

            if (float.TryParse(args[1], out float value))
            {
                if (SkillTreeConfig.SetConfigValue(key, value))
                {
                    args.Context.AddString($"[SkillTree] {key} = {value} 설정 완료");
                }
                else
                {
                    args.Context.AddString($"[SkillTree] 설정 실패 (서버만 가능)");
                }
            }
            else
            {
                args.Context.AddString($"[SkillTree] 잘못된 값: {args[1]}");
            }
        }

        internal static void ShowCurrentConfig()
        {
            Log.LogInfo("=== 현재 공격 전문가 설정 ===");
            Log.LogInfo($"공격 루트 데미지: {SkillTreeConfig.AttackRootDamageBonusValue}%");
            Log.LogInfo($"근접 특화 확률: {SkillTreeConfig.AttackMeleeBonusChanceValue}%");
            Log.LogInfo($"근접 특화 피해: {SkillTreeConfig.AttackMeleeBonusDamageValue}%");
            Log.LogInfo($"활 특화 확률: {SkillTreeConfig.AttackBowBonusChanceValue}%");
            Log.LogInfo($"활 특화 피해: {SkillTreeConfig.AttackBowBonusDamageValue}%");
            Log.LogInfo($"석궁 특화 확률: {SkillTreeConfig.AttackCrossbowBonusChanceValue}%");
            Log.LogInfo($"석궁 특화 피해: {SkillTreeConfig.AttackCrossbowBonusDamageValue}%");
            Log.LogInfo($"지팡이 특화 확률: {SkillTreeConfig.AttackStaffBonusChanceValue}%");
            Log.LogInfo($"지팡이 특화 피해: {SkillTreeConfig.AttackStaffBonusDamageValue}%");
            Log.LogInfo($"치명타 피해 보너스: {SkillTreeConfig.AttackCritDamageBonusValue}%");
            Log.LogInfo($"양손 무기 보너스: {SkillTreeConfig.AttackTwoHandedBonusValue}%");
            Log.LogInfo($"한손 무기 보너스: {SkillTreeConfig.AttackOneHandedBonusValue}%");
            Log.LogInfo($"연속 근접 보너스: {SkillTreeConfig.AttackFinisherMeleeBonusValue}%");

            Log.LogInfo("=== 현재 속도 전문가 설정 ===");
            Log.LogInfo($"속도 루트 이동속도: {SkillTreeConfig.SpeedRootMoveSpeedValue}%");
            Log.LogInfo($"구르기 속도: {SkillTreeConfig.SpeedBaseDodgeSpeedValue}%");
            Log.LogInfo($"근접 콤보 보너스: {SkillTreeConfig.SpeedMeleeComboBonusValue}%");
            Log.LogInfo($"근접 콤보 지속시간: {SkillTreeConfig.SpeedMeleeComboDurationValue}초");
            Log.LogInfo($"석궁 장전 속도: {SkillTreeConfig.SpeedCrossbowReloadSpeedValue}%");
            Log.LogInfo($"활 적중 보너스: {SkillTreeConfig.SpeedBowHitBonusValue}%");
            Log.LogInfo($"활 적중 지속시간: {SkillTreeConfig.SpeedBowHitDurationValue}초");
            Log.LogInfo($"지팡이 시전 속도: {SkillTreeConfig.SpeedStaffCastSpeedValue}%");
            Log.LogInfo($"음식 효율: {SkillTreeConfig.SpeedFoodEfficiencyValue}%");
            Log.LogInfo($"배 속도: {SkillTreeConfig.SpeedShipBonusValue}%");
            Log.LogInfo($"근접 공격속도: {SkillTreeConfig.SpeedMeleeAttackSpeedValue}%");
            Log.LogInfo($"석궁 장전속도: {SkillTreeConfig.SpeedCrossbowDrawSpeedValue}%");
            Log.LogInfo($"활 장전속도: {SkillTreeConfig.SpeedBowDrawSpeedValue}%");
            Log.LogInfo($"지팡이 시전속도: {SkillTreeConfig.SpeedStaffCastSpeedFinalValue}%");

            Log.LogInfo("=== 현재 아처 멀티샷 설정 ===");
            Log.LogInfo($"발사할 화살 수: {Archer_Config.ArcherMultiShotArrowCountValue}발");
            Log.LogInfo($"화살 소모량: {Archer_Config.ArcherMultiShotArrowConsumptionValue}개");
            Log.LogInfo($"화살당 데미지: {Archer_Config.ArcherMultiShotDamagePercentValue}%");

            Log.LogInfo("=== 현재 활 전문가 멀티샷 설정 ===");
            Log.LogInfo($"Lv1 발동 확률: {SkillTreeConfig.BowMultishotLv1ChanceValue}%");
            Log.LogInfo($"Lv2 발동 확률: {SkillTreeConfig.BowMultishotLv2ChanceValue}%");
            Log.LogInfo($"추가 화살 수: {SkillTreeConfig.BowMultishotArrowCountValue}발");
            Log.LogInfo($"화살 소모량: {SkillTreeConfig.BowMultishotArrowConsumptionValue}개");
            Log.LogInfo($"화살당 데미지: {SkillTreeConfig.BowMultishotDamagePercentValue}%");
        }

        #endregion
    }
}

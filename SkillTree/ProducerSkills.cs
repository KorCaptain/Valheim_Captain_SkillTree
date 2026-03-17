using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Localization;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 제작 전문가(Producer) 직업 스킬 시스템
    /// 액티브: 장인의 축복 (Y키) - 파티원 공격력/체력 버프
    /// 패시브: 농사 그리드, 내구도, 재료 감소, 제작 마법부여
    /// </summary>
    public static class ProducerSkills
    {
        #region State

        private class BuffState
        {
            public float EndTime;
            public float CooldownEndTime;
            public bool IsActive  => Time.time < EndTime;
            public bool OnCooldown => Time.time < CooldownEndTime;
        }

        private static Dictionary<long, BuffState>   buffStates           = new Dictionary<long, BuffState>();
        private static Dictionary<long, GameObject>  producerStatusVFXs   = new Dictionary<long, GameObject>();
        private static GameObject                    cachedStatusVFXPrefab = null;

        #endregion

        #region Registration

        public static void RegisterProducerSkill()
        {
            var manager = SkillTreeManager.Instance;
            manager.AddSkill(new SkillNode {
                Id              = "Producer",
                Name            = L.Get("job_producer"),
                Description     = Producer_Tooltip.GetProducerTooltip(),
                RequiredPoints  = 0,
                MaxLevel        = 5,
                Tier            = 7,
                Position        = new Vector2(0, -355),
                Category        = "직업",
                IconName        = "craft_unlock",
                IconNameLocked  = "craft_lock",
                IconNameUnlocked = "craft_unlock",
                NextNodes       = new List<string>(),
                RequiredPlayerLevel = 10,
                ApplyEffect     = (lv) => { }
            });
        }

        #endregion

        #region Active Skill - Artisan's Blessing

        /// <summary>
        /// Y키 장인의 축복 스킬 실행
        /// </summary>
        public static void ExecuteProducerBuff(Player player)
        {
            if (player == null) return;

            long id = player.GetPlayerID();
            if (!buffStates.ContainsKey(id))
                buffStates[id] = new BuffState();

            var state = buffStates[id];

            if (state.OnCooldown)
            {
                float remaining = state.CooldownEndTime - Time.time;
                player.Message(MessageHud.MessageType.TopLeft,
                    L.Get("cooldown_remaining", $"{remaining:F0}"));
                return;
            }

            float staminaCost = Producer_Config.ProducerBuff_StaminaCostValue;
            if (player.GetStamina() < staminaCost)
            {
                player.Message(MessageHud.MessageType.Center, L.Get("stamina_insufficient_short"));
                return;
            }

            player.UseStamina(staminaCost);

            float duration = Producer_Config.ProducerBuff_DurationValue;
            float cooldown = Producer_Config.ProducerBuff_CooldownValue;
            state.EndTime        = Time.time + duration;
            state.CooldownEndTime = Time.time + cooldown;

            ActiveSkillCooldownRegistry.SetCooldown("Y", cooldown);

            // 파티원 포함 자신에게 VFX + 메시지 적용
            ApplyBuffToNearbyPlayers(player, duration);

            // 시전 VFX (시전자 위치)
            VFXManager.PlayVFXAtPosition("fx_Fader_Spin", player.transform.position);

            player.Message(MessageHud.MessageType.Center, L.Get("producer_buff_applied"));
            Plugin.Log.LogDebug($"[제작 전문가] {player.GetPlayerName()} 장인의 축복 발동 ({duration}초)");
        }

        private static void ApplyBuffToNearbyPlayers(Player caster, float duration)
        {
            float range = Producer_Config.ProducerBuff_RangeValue;
            float buffAtk   = Producer_Config.ProducerBuff_AttackBonusValue;
            float buffHp    = Producer_Config.ProducerBuff_MaxHealthBonusValue;

            // 로컬 플레이어 버프 등록 (자신 포함)
            SetPlayerBuff(caster, duration);
            SimpleVFX.Play("buff_01", caster.transform.position + Vector3.up * 1.5f);
            StartProducerStatusVFX(caster, duration);

            // 근처 파티원 버프 (Player 목록에서 범위 내 검색)
            foreach (var p in Player.GetAllPlayers())
            {
                if (p == caster) continue;
                if (Vector3.Distance(p.transform.position, caster.transform.position) <= range)
                {
                    SetPlayerBuff(p, duration);
                    SimpleVFX.Play("buff_01", p.transform.position + Vector3.up * 1.5f);
                    StartProducerStatusVFX(p, duration);
                    p.Message(MessageHud.MessageType.Center, L.Get("producer_buff_applied"));
                }
            }
        }

        private static void SetPlayerBuff(Player player, float duration)
        {
            long id = player.GetPlayerID();
            if (!buffStates.ContainsKey(id))
                buffStates[id] = new BuffState();
            buffStates[id].EndTime = Time.time + duration;
        }

        /// <summary>
        /// buff_01 이후 버프 종료까지 캐릭터를 따라다니는 상태 VFX (statusailment_01_aura)
        /// </summary>
        private static void StartProducerStatusVFX(Player player, float duration)
        {
            try
            {
                long id = player.GetPlayerID();

                // 기존 효과 제거 (중복 방지)
                if (producerStatusVFXs.TryGetValue(id, out var existing) && existing != null)
                    UnityEngine.Object.Destroy(existing);
                producerStatusVFXs.Remove(id);

                if (cachedStatusVFXPrefab == null)
                    cachedStatusVFXPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                if (cachedStatusVFXPrefab == null) return;

                var inst = UnityEngine.Object.Instantiate(
                    cachedStatusVFXPrefab,
                    player.transform.position + Vector3.up * 2.0f,
                    Quaternion.identity
                );
                inst.transform.SetParent(player.transform, false);
                inst.transform.localPosition = Vector3.up * 2.0f;
                inst.transform.localScale    = Vector3.one * 0.6f;

                producerStatusVFXs[id] = inst;
                UnityEngine.Object.Destroy(inst, duration); // 버프 종료 시 자동 제거
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[제작 전문가] 상태 VFX 시작 실패: {ex.Message}");
            }
        }

        private static void StopProducerStatusVFX(long id)
        {
            if (producerStatusVFXs.TryGetValue(id, out var vfx) && vfx != null)
                UnityEngine.Object.Destroy(vfx);
            producerStatusVFXs.Remove(id);
        }

        /// <summary>
        /// 플레이어가 제작 전문가 버프 중인지 확인
        /// </summary>
        public static bool IsProducerBuffActive(Player player)
        {
            if (player == null) return false;
            long id = player.GetPlayerID();
            if (!buffStates.ContainsKey(id)) return false;
            if (!buffStates[id].IsActive)
            {
                // 만료 시 정리
                StopProducerStatusVFX(id);
                if (!buffStates[id].OnCooldown)
                    buffStates.Remove(id);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 플레이어가 Producer 직업인지 확인
        /// </summary>
        public static bool IsProducer(Player player)
        {
            if (player == null) return false;
            var manager = SkillTreeManager.Instance;
            return manager != null && manager.GetSkillLevel("Producer") > 0;
        }

        public static int GetProducerLevel(Player player)
        {
            if (player == null) return 0;
            var manager = SkillTreeManager.Instance;
            return manager?.GetSkillLevel("Producer") ?? 0;
        }

        /// <summary>플레이어의 제작 전문가 버프 남은 시간 반환 (초, 비활성이면 0)</summary>
        public static float GetBuffRemainingForPlayer(Player player)
        {
            if (player == null) return 0f;
            long id = player.GetPlayerID();
            if (!buffStates.TryGetValue(id, out var state)) return 0f;
            if (!state.IsActive) return 0f;
            return state.EndTime - Time.time;
        }

        /// <summary>버프 총 지속 시간 (Config 기준)</summary>
        public static float GetBuffTotalDuration() => Producer_Config.ProducerBuff_DurationValue;

        #endregion

        #region Harmony Patches

        // === 버프 중 최대 체력 +15% ===
        [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
        public static class Producer_Player_GetTotalFoodValue_HealthBonus_Patch
        {
            [HarmonyPriority(Priority.Last)]
            public static void Postfix(Player __instance, ref float hp)
            {
                try
                {
                    if (!IsProducerBuffActive(__instance)) return;
                    float bonus = Producer_Config.ProducerBuff_MaxHealthBonusValue / 100f;
                    hp += hp * bonus;
                }
                catch (Exception) { }
            }
        }

        // === 버프 중 공격력 +15% (아웃고잉 데미지) ===
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class Producer_Character_Damage_AttackBonus_Patch
        {
            public static void Prefix(Character __instance, ref HitData hit)
            {
                try
                {
                    if (hit.GetAttacker() is Player attacker && IsProducerBuffActive(attacker))
                    {
                        float bonus = 1f + Producer_Config.ProducerBuff_AttackBonusValue / 100f;
                        hit.m_damage.m_blunt     *= bonus;
                        hit.m_damage.m_slash     *= bonus;
                        hit.m_damage.m_pierce    *= bonus;
                        hit.m_damage.m_fire      *= bonus;
                        hit.m_damage.m_frost     *= bonus;
                        hit.m_damage.m_lightning *= bonus;
                        hit.m_damage.m_poison    *= bonus;
                        hit.m_damage.m_spirit    *= bonus;
                    }
                }
                catch (Exception) { }
            }
        }

        #endregion
    }
}

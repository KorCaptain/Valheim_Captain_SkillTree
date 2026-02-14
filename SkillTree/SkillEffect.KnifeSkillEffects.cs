using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 단검 스킬 효과 시스템
    /// SkillEffect.MeleeSkills.cs에서 분리된 단검 관련 기능들
    /// </summary>
    public static partial class SkillEffect
    {
        // === 단검 스킬 특수 효과 시스템 ===

        /// <summary>
        /// 뒤에서 공격 여부 확인 (백스탭)
        /// </summary>
        public static bool IsBackstab(Character attacker, Character target)
        {
            if (attacker == null || target == null) return false;
            Vector3 dirToTarget = (target.transform.position - attacker.transform.position).normalized;
            Vector3 targetForward = target.transform.forward;
            float dot = Vector3.Dot(dirToTarget, targetForward);
            return dot > 0.5f; // 뒤에서 공격
        }

        /// <summary>
        /// 단검 전문가 백스탭 데미지 보너스 체크
        /// </summary>
        public static void CheckKnifeExpertBackstab(Player player, Character target, HitData hit)
        {
            if (!HasSkill("knife_expert_backstab") || !IsUsingDagger(player)) return;

            if (IsBackstab(player, target))
            {
                float backstabBonus = Knife_Config.KnifeExpertBackstabBonusValue / 100f;
                hit.m_damage.m_slash *= (1f + backstabBonus);
                hit.m_damage.m_pierce *= (1f + backstabBonus);
                DrawFloatingText(player, $"🗡️ 단검 전문가 백스탭! (+{Knife_Config.KnifeExpertBackstabBonusValue}%)");
                Plugin.Log.LogDebug($"[단검 전문가] 백스탭 데미지 +{Knife_Config.KnifeExpertBackstabBonusValue}% 적용");
            }
        }

        /// <summary>
        /// 회피 숙련 - 구르기 후 회피율 증가
        /// </summary>
        public static void CheckKnifeEvasion(Player player)
        {
            if (!HasSkill("knife_step2_evasion") || !IsUsingDagger(player)) return;

            float duration = Knife_Config.KnifeEvasionDurationValue;
            knifeEvasionEndTime[player] = Time.time + duration;

            // 패시브 스킬: 텍스트만 표시
            DrawFloatingText(player, $"🛡️ 회피 숙련 ({duration}초간 +{Knife_Config.KnifeEvasionBonusValue}% 회피율)");
            Plugin.Log.LogDebug($"[회피 숙련] {duration}초간 회피율 +{Knife_Config.KnifeEvasionBonusValue}% 버프 활성화");
        }

        /// <summary>
        /// 빠른 움직임 - 이동속도 증가
        /// </summary>
        public static void ActivateKnifeMoveSpeed(Player player)
        {
            if (!HasSkill("knife_step3_move_speed") || !IsUsingDagger(player)) return;

            float duration = Knife_Config.KnifeMoveSpeedDurationValue;
            knifeMoveSpeedEndTime[player] = Time.time + duration;

            // 패시브 스킬: MMO 방식 DamageText로 표시
            DrawFloatingText(player,
                $"💨 빠른 움직임 ({duration}초간 +{Knife_Config.KnifeMoveSpeedBonusValue}% 이동속도)",
                new Color(0.8f, 1f, 0.8f, 1f)); // 연한 초록색 (빠른 움직임)
            Plugin.Log.LogDebug($"[빠른 움직임] {duration}초간 이동속도 +{Knife_Config.KnifeMoveSpeedBonusValue}% 버프 활성화");
        }

        /// <summary>
        /// 빠른 공격 - 패시브 스킬로 변경됨
        /// ⚠️ ItemData.GetDamage 패치에서 자동 처리됩니다.
        /// </summary>
        [Obsolete("패시브 스킬로 변경되어 자동 적용됩니다.")]
        public static void ActivateKnifeAttackDamage(Player player)
        {
            // 더 이상 사용되지 않음
            return;
        }

        /// <summary>
        /// 치명타 숙련 - 패시브 스킬로 변경됨
        /// ⚠️ Critical 시스템에서 자동 처리됩니다.
        /// </summary>
        [Obsolete("패시브 스킬로 변경되어 Critical 시스템에서 자동 적용됩니다.")]
        public static void ActivateKnifeCritRate(Player player)
        {
            // 더 이상 사용되지 않음
            return;
        }

        /// <summary>
        /// 전투 숙련 - 전투 중 공격력 증가
        /// </summary>
        public static void CheckKnifeCombatDamage(Player player, HitData hit)
        {
            if (!HasSkill("knife_step6_combat_damage") || !IsUsingDagger(player)) return;

            float damageBonus = Knife_Config.KnifeCombatDamageBonusValue / 100f;
            hit.m_damage.m_slash *= (1f + damageBonus);
            hit.m_damage.m_pierce *= (1f + damageBonus);

            // 패시브 스킬: 텍스트만 표시
            DrawFloatingText(player, $"⚔️ 전투 숙련 (+{Knife_Config.KnifeCombatDamageBonusValue}% 공격력)");
            Plugin.Log.LogDebug($"[전투 숙련] 공격력 +{Knife_Config.KnifeCombatDamageBonusValue}% 적용");
        }

        /// <summary>
        /// 암살자 - 패시브 치명타 확률 및 치명타 피해 증가
        /// </summary>
        public static void CheckKnifeExecutionPassive(Player player, HitData hit, bool isCritical)
        {
            if (!HasSkill("knife_step7_execution") || !Knife_Skill.IsUsingDagger(player)) return;

            Knife_Skill.ApplyKnifeExecutionPassive(player, ref hit, isCritical);
        }

        /// <summary>
        /// 화면 중앙(카메라 방향) 기준 몬스터 탐색 (시야각 45도 이내)
        /// </summary>
        private static Character FindFrontMonster(Player player, float range, float maxAngle = 45f)
        {
            if (player == null) return null;

            Vector3 playerPos = player.transform.position;

            // 카메라 방향 사용 (화면 중앙 기준)
            Vector3 cameraForward = Vector3.forward;
            if (GameCamera.instance != null)
            {
                cameraForward = GameCamera.instance.transform.forward;
                cameraForward.y = 0; // 수평 방향만 고려
                cameraForward.Normalize();
            }

            // 카메라 방향 시야각 내의 몬스터만 필터링, 거리순 정렬
            var frontMonster = Character.GetAllCharacters()
                .Where(c => c != null && !c.IsDead() && c != player && !c.IsPlayer())
                .Where(c => c.GetFaction() != Character.Faction.Players)
                .Where(c => Vector3.Distance(playerPos, c.transform.position) <= range)
                .Where(c =>
                {
                    // 플레이어 → 몬스터 방향 벡터
                    Vector3 dirToMonster = (c.transform.position - playerPos).normalized;
                    dirToMonster.y = 0;
                    dirToMonster.Normalize();
                    // 카메라 방향과의 각도 계산
                    float angle = Vector3.Angle(cameraForward, dirToMonster);
                    return angle <= maxAngle;
                })
                .OrderBy(c => Vector3.Distance(playerPos, c.transform.position))
                .FirstOrDefault();

            return frontMonster;
        }

        /// <summary>
        /// 대상 몬스터의 뒤쪽으로 빠른 돌격 (순간이동처럼 보임)
        /// </summary>
        private static bool TeleportBehindTarget(Player player, Character target, float behindDistance)
        {
            if (player == null || target == null) return false;

            try
            {
                // 안전성 검사
                if (player.IsEncumbered() || player.InDodge() || player.IsKnockedBack() || player.IsStaggering())
                {
                    Plugin.Log.LogDebug("[암살자의 심장] 돌격 불가 상태");
                    return false;
                }

                // 돌격 전 위치 저장
                Vector3 originalPosition = player.transform.position;

                // 대상 몬스터의 뒤쪽 위치 계산
                Vector3 targetPos = target.transform.position;
                Vector3 targetForward = target.transform.forward;
                Vector3 behindPosition = targetPos - (targetForward * behindDistance);

                // 지형 높이 조정
                if (ZoneSystem.instance != null)
                {
                    float groundHeight = ZoneSystem.instance.GetGroundHeight(behindPosition);
                    behindPosition.y = groundHeight;
                }

                // 시작점 VFX
                SimpleVFX.Play("vfx_spawn_small", originalPosition, 1.5f);

                // Rigidbody로 빠른 돌격 실행
                var body = HarmonyLib.Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();
                if (body != null)
                {
                    body.velocity = Vector3.zero;
                    body.angularVelocity = Vector3.zero;
                    body.position = behindPosition;
                }

                // Transform 위치/회전 설정
                player.transform.position = behindPosition;

                // 몬스터를 바라보도록 회전
                Vector3 lookDirection = (targetPos - behindPosition).normalized;
                lookDirection.y = 0;
                if (lookDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    player.transform.rotation = targetRotation;
                    if (body != null)
                    {
                        body.rotation = targetRotation;
                    }
                }

                // ZNetView 네트워크 동기화
                var nview = HarmonyLib.Traverse.Create(player).Field("m_nview").GetValue<ZNetView>();
                if (nview != null && nview.IsOwner())
                {
                    var zdo = nview.GetZDO();
                    if (zdo != null)
                    {
                        zdo.SetPosition(behindPosition);
                        zdo.SetRotation(player.transform.rotation);
                    }
                }

                // 도착점 VFX
                SimpleVFX.Play("fx_backstab", behindPosition, 2f);

                Plugin.Log.LogDebug($"[암살자의 심장] 돌격 성공 - {target.GetHoverName() ?? target.name} 뒤로 이동");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[암살자의 심장] 돌격 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 암살자의 심장 - G키 액티브 스킬 활성화 (순간이동 포함)
        /// </summary>
        public static bool ActivateKnifeAssassinHeart(Player player)
        {
            if (!HasSkill("knife_step9_assassin_heart") || !IsUsingDagger(player)) return false;

            float currentTime = Time.time;

            // 쿨타임 체크
            if (knifeAssassinHeartCooldownEndTime.TryGetValue(player, out float cooldownEnd) &&
                currentTime < cooldownEnd)
            {
                float remainingCooldown = cooldownEnd - currentTime;
                DrawFloatingText(player, $"암살자의 심장 쿨타임: {remainingCooldown:F1}초", Color.gray);
                return false;
            }

            // 정면 몬스터 탐색
            float teleportRange = Knife_Config.KnifeAssassinHeartTeleportRangeValue;
            Character targetMonster = FindFrontMonster(player, teleportRange);

            if (targetMonster == null)
            {
                DrawFloatingText(player, $"{teleportRange}미터 이내 적이 없습니다", Color.yellow);
                Plugin.Log.LogDebug($"[암살자의 심장] 취소 - 정면 {teleportRange}m 내 몬스터 없음");
                return false;
            }

            // 스태미나 체크
            float staminaCost = player.GetMaxStamina() * (Knife_Config.KnifeAssassinHeartStaminaCostValue / 100f);
            if (player.GetStamina() < staminaCost)
            {
                DrawFloatingText(player, "스태미나 부족!", Color.red);
                return false;
            }

            // 원래 위치 저장 (공격 후 복귀용)
            Vector3 originalPosition = player.transform.position;

            // 순간이동 실행
            float behindDistance = Knife_Config.KnifeAssassinHeartTeleportBehindValue;
            bool teleportSuccess = TeleportBehindTarget(player, targetMonster, behindDistance);

            if (!teleportSuccess)
            {
                DrawFloatingText(player, "순간이동 실패!", Color.red);
                return false;
            }

            // 스태미나 소모
            player.UseStamina(staminaCost);

            // 대상에게 스턴 적용
            ApplyStunToTargetMonster(player, targetMonster);

            // 연속 공격 코루틴 실행 (원래 위치 전달)
            player.StartCoroutine(ExecuteAssassinHeartAttacksCoroutine(player, targetMonster, originalPosition));

            // 버프 활성화
            float duration = Knife_Config.KnifeAssassinHeartDurationValue;
            knifeAssassinHeartEndTime[player] = currentTime + duration;

            // 쿨타임 설정
            float cooldown = Knife_Config.KnifeAssassinHeartCooldownValue;
            knifeAssassinHeartCooldownEndTime[player] = currentTime + cooldown;

            // 액티브 스킬: VFX/SFX 사용
            PlaySkillEffect(player, "knife_step9_assassin_heart", player.transform.position);

            string targetName = targetMonster.GetHoverName() ?? targetMonster.name ?? "적";
            DrawFloatingText(player, $"💀 암살자의 심장! {targetName} 뒤로 이동!", Color.red);

            Plugin.Log.LogInfo($"[암살자의 심장] 활성화 - {targetName} 뒤로 순간이동, {duration}초간 치명타 확률 +{Knife_Config.KnifeAssassinHeartCritChanceValue}%");
            return true;
        }

        /// <summary>
        /// 암살자의 심장 버프가 활성화되어 있는지 확인
        /// </summary>
        public static bool IsKnifeAssassinHeartActive(Player player)
        {
            return knifeAssassinHeartEndTime.TryGetValue(player, out float endTime) &&
                   Time.time < endTime;
        }

        /// <summary>
        /// 암살자의 심장 치명타 효과 적용
        /// </summary>
        public static void ApplyKnifeAssassinHeartCrit(Player player, HitData hit)
        {
            if (!HasSkill("knife_step9_assassin_heart") || !IsUsingDagger(player)) return;

            if (IsKnifeAssassinHeartActive(player))
            {
                // 공격력 보너스 적용
                float damageBonus = Knife_Config.KnifeAssassinHeartDamageBonusValue / 100f;
                hit.m_damage.m_slash *= (1f + damageBonus);
                hit.m_damage.m_pierce *= (1f + damageBonus);

                // 치명타 확률 체크
                float critChance = Knife_Config.KnifeAssassinHeartCritChanceValue / 100f;
                if (UnityEngine.Random.Range(0f, 1f) <= critChance)
                {
                    float critDamageBonus = Knife_Config.KnifeAssassinHeartCritDamageValue / 100f;
                    hit.m_damage.m_slash *= (1f + critDamageBonus);
                    hit.m_damage.m_pierce *= (1f + critDamageBonus);

                    PlaySkillEffect(player, "knife_step9_assassin_heart", hit.m_point);
                    DrawFloatingText(player, $"💥 암살자의 치명타! (+{Knife_Config.KnifeAssassinHeartCritDamageValue}%)", Color.red);

                    Plugin.Log.LogDebug($"[암살자의 심장] 치명타 발동! +{Knife_Config.KnifeAssassinHeartCritDamageValue}% 피해");
                }
            }
        }

        /// <summary>
        /// 연속 공격 카운트 업데이트 (단검용)
        /// </summary>
        public static void UpdateConsecutiveHits(Player player)
        {
            if (!consecutiveHits.ContainsKey(player))
                consecutiveHits[player] = 0;

            float now = Time.time;
            if (lastHitTime.ContainsKey(player) && now - lastHitTime[player] < 2f)
            {
                consecutiveHits[player]++;
            }
            else
            {
                consecutiveHits[player] = 1;
            }
            lastHitTime[player] = now;
        }

        /// <summary>
        /// 은신 이동 보너스 적용
        /// </summary>
        public static void ApplyStealthMovementBonus(Player player, bool enable)
        {
            if (enable && HasSkill("knife_step8_assassination"))
            {
                stealthMovementBonus[player] = true;
                Plugin.Log.LogInfo("[단검 은신] 이동 속도 보너스 활성화");
            }
            else
            {
                stealthMovementBonus[player] = false;
            }
        }

        /// <summary>
        /// 암살자의 심장 - 대상 몬스터에게 스턴 적용
        /// </summary>
        private static void ApplyStunToTargetMonster(Player player, Character target)
        {
            if (player == null || target == null || target.IsDead()) return;

            try
            {
                float stunDuration = Knife_Config.KnifeAssassinHeartStunDurationValue;

                // HitData로 강제 스태거
                HitData staggerHit = new HitData();
                staggerHit.m_damage.m_blunt = 0.1f;
                staggerHit.m_staggerMultiplier = 100f;
                staggerHit.m_pushForce = 0f;
                staggerHit.m_point = target.transform.position;
                staggerHit.m_dir = -target.transform.forward;
                staggerHit.SetAttacker(player);
                target.Damage(staggerHit);

                // Stagger 호출
                target.Stagger(-target.transform.forward);

                // VFX
                SimpleVFX.Play("debuff", target.transform.position + Vector3.up, 2f);

                string targetName = target.GetHoverName() ?? target.name ?? "적";
                Plugin.Log.LogDebug($"[암살자의 심장] {targetName}에게 스턴 적용 ({stunDuration}초)");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[암살자의 심장] 스턴 적용 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 암살자의 심장 - 연속 공격 코루틴 (3회 직접 적중 + 원래 위치 복귀)
        /// </summary>
        private static IEnumerator ExecuteAssassinHeartAttacksCoroutine(Player player, Character target, Vector3 originalPosition)
        {
            // 순간이동 후 약간의 대기
            yield return new WaitForSeconds(0.15f);

            var weapon = player?.GetCurrentWeapon();
            if (weapon == null)
            {
                Plugin.Log.LogWarning("[암살자의 심장] 무기 없음 - 연속 공격 취소");
                TeleportToPosition(player, originalPosition); // 복귀
                yield break;
            }

            int requiredHits = Knife_Config.KnifeAssassinHeartAttackCountValue; // 3회 적중 필요
            float attackSpeedBonus = 500f; // 공격속도 500% 증가
            float attackInterval = 0.15f; // 공격 간격

            // 공격 모드 활성화 + 공격속도 버프 설정
            assassinHeartAttackMode[player] = true;
            assassinHeartTarget[player] = target;
            assassinHeartHitCount[player] = 0;
            assassinHeartAttackSpeedBonus[player] = attackSpeedBonus;

            Plugin.Log.LogInfo($"[암살자의 심장] 연속 공격 시작 - 목표: {requiredHits}회 적중");

            // 데미지 계산 준비
            var weaponDamage = weapon.GetDamage();
            float damageMultiplier = 1.0f + (Knife_Config.KnifeAssassinHeartDamageBonusValue / 100f);

            for (int i = 0; i < requiredHits; i++)
            {
                // 플레이어 상태 체크
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogDebug("[암살자의 심장] 플레이어 사망 - 연속 공격 중단");
                    break;
                }

                // 대상 상태 체크
                if (target == null || target.IsDead())
                {
                    Plugin.Log.LogDebug($"[암살자의 심장] 대상 사망 - 연속 공격 완료 ({i}회 적중)");
                    DrawFloatingText(player, $"💀 암살 완료! ({i}회 적중)", Color.red);
                    break;
                }

                // 직접 HitData로 데미지 적용 (StartAttack 없이 - 이중 공격 방지)
                try
                {
                    HitData hit = new HitData();
                    hit.m_damage.m_slash = weaponDamage.m_slash * damageMultiplier;
                    hit.m_damage.m_pierce = weaponDamage.m_pierce * damageMultiplier;
                    hit.m_point = target.GetCenterPoint();
                    hit.m_dir = (target.transform.position - player.transform.position).normalized;
                    hit.m_attacker = player.GetZDOID();
                    hit.SetAttacker(player);
                    hit.m_skill = Skills.SkillType.Knives;

                    target.Damage(hit);

                    // VFX
                    SimpleVFX.Play("hit_01", target.GetCenterPoint(), 1f);
                    SimpleVFX.Play("confetti_directional_multicolor", target.GetCenterPoint(), 1.5f);

                    // 적중 카운트 증가
                    assassinHeartHitCount[player] = i + 1;
                    DrawFloatingText(player, $"💀 ({i + 1}/{requiredHits})", Color.red);

                    Plugin.Log.LogDebug($"[암살자의 심장] 적중 {i + 1}/{requiredHits}");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[암살자의 심장] 데미지 적용 실패: {ex.Message}");
                }

                yield return new WaitForSeconds(attackInterval);
            }

            // 최종 VFX
            if (target != null && !target.IsDead())
            {
                SimpleVFX.Play("fx_backstab", target.GetCenterPoint(), 2f);
            }

            // 공격 모드 종료 + 버프 해제
            assassinHeartAttackMode[player] = false;
            assassinHeartTarget.Remove(player);
            assassinHeartAttackSpeedBonus.Remove(player);

            int finalHits = assassinHeartHitCount.TryGetValue(player, out int fh) ? fh : 0;
            assassinHeartHitCount.Remove(player);

            // 원래 위치로 복귀
            yield return new WaitForSeconds(0.1f);
            if (player != null && !player.IsDead())
            {
                TeleportToPosition(player, originalPosition);
                SimpleVFX.Play("vfx_spawn_small", originalPosition, 1.5f);
                DrawFloatingText(player, $"💀 암살 완료! 복귀!", Color.red);
                Plugin.Log.LogInfo($"[암살자의 심장] 원래 위치로 복귀 - 총 {finalHits}회 적중");
            }

            Plugin.Log.LogInfo($"[암살자의 심장] 연속 공격 종료 - 총 {finalHits}회 적중");
        }

        /// <summary>
        /// 플레이어가 대상을 바라보도록 강제 회전 (Rigidbody + Transform + 내부 필드)
        /// </summary>
        private static void LookAtTarget(Player player, Character target)
        {
            if (player == null || target == null) return;

            try
            {
                Vector3 direction = (target.transform.position - player.transform.position).normalized;
                direction.y = 0;
                if (direction == Vector3.zero) return;

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Transform 회전
                player.transform.rotation = targetRotation;

                // Rigidbody 회전 (네트워크 동기화용)
                var body = HarmonyLib.Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();
                if (body != null)
                {
                    body.rotation = targetRotation;
                }

                // 내부 lookDir 설정 (공격 방향 결정에 사용됨)
                var lookDir = HarmonyLib.Traverse.Create(player).Field("m_lookDir");
                if (lookDir.FieldExists())
                {
                    lookDir.SetValue(direction);
                }

                // lookYaw 설정
                float yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                var lookYaw = HarmonyLib.Traverse.Create(player).Field("m_lookYaw");
                if (lookYaw.FieldExists())
                {
                    lookYaw.SetValue(yaw);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogDebug($"[암살자의 심장] LookAtTarget 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 플레이어를 지정된 위치로 순간이동
        /// </summary>
        private static void TeleportToPosition(Player player, Vector3 position)
        {
            if (player == null) return;

            try
            {
                // 지형 높이 조정
                if (ZoneSystem.instance != null)
                {
                    float groundHeight = ZoneSystem.instance.GetGroundHeight(position);
                    position.y = groundHeight;
                }

                // VFX (출발점)
                SimpleVFX.Play("vfx_spawn_small", player.transform.position, 1.5f);

                // Rigidbody 이동
                var body = HarmonyLib.Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();
                if (body != null)
                {
                    body.velocity = Vector3.zero;
                    body.angularVelocity = Vector3.zero;
                    body.position = position;
                }

                // Transform 이동
                player.transform.position = position;

                // ZNetView 네트워크 동기화
                var nview = HarmonyLib.Traverse.Create(player).Field("m_nview").GetValue<ZNetView>();
                if (nview != null && nview.IsOwner())
                {
                    var zdo = nview.GetZDO();
                    if (zdo != null)
                    {
                        zdo.SetPosition(position);
                    }
                }

                Plugin.Log.LogDebug($"[암살자의 심장] 위치 이동 완료: {position}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[암살자의 심장] 위치 이동 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 암살자의 심장 공격 모드인지 확인
        /// </summary>
        public static bool IsAssassinHeartAttackMode(Player player)
        {
            return assassinHeartAttackMode.TryGetValue(player, out bool active) && active;
        }

        /// <summary>
        /// 암살자의 심장 공격속도 보너스 가져오기
        /// </summary>
        public static float GetAssassinHeartAttackSpeedBonus(Player player)
        {
            if (assassinHeartAttackSpeedBonus.TryGetValue(player, out float bonus))
                return bonus;
            return 0f;
        }

        /// <summary>
        /// 암살자의 심장 적중 카운트 증가 (패치에서 호출)
        /// </summary>
        public static void IncrementAssassinHeartHitCount(Player player)
        {
            if (!assassinHeartAttackMode.TryGetValue(player, out bool active) || !active)
                return;

            if (!assassinHeartHitCount.ContainsKey(player))
                assassinHeartHitCount[player] = 0;

            assassinHeartHitCount[player]++;
            int hits = assassinHeartHitCount[player];
            int required = Knife_Config.KnifeAssassinHeartAttackCountValue;

            Plugin.Log.LogDebug($"[암살자의 심장] 적중! ({hits}/{required})");

            if (hits < required)
            {
                DrawFloatingText(player, $"💀 ({hits}/{required})", Color.red);
            }
        }

        /// <summary>
        /// 암살자의 심장 대상 몬스터 가져오기
        /// </summary>
        public static Character GetAssassinHeartTarget(Player player)
        {
            if (assassinHeartTarget.TryGetValue(player, out Character target))
                return target;
            return null;
        }
    }
}

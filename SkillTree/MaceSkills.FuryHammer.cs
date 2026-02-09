using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 분노의 망치 스킬 전용 클래스 (둔기 H키 액티브 스킬)
    /// VFX 규칙 준수: VFXManager.PlayVFXMultiplayer 사용
    /// </summary>
    public static class FuryHammerSkill
    {
        // === 정적 필드 ===
        public static bool isChargingFuryHammer = false;
        private static Dictionary<Player, Coroutine> furyHammerCoroutine = new Dictionary<Player, Coroutine>();
        private static float lastMaceSkillTime = 0f;

        // === 하드코딩 상수 (수정 불가) ===
        private const int ATTACK_COUNT = 5;           // 연속공격 횟수 고정
        private const float ATTACK_INTERVAL = 0.5f;   // 공격간 딜레이 고정 (초)

        /// <summary>
        /// H키 누름 처리: 차지 시작
        /// </summary>
        public static void HandleHKeyPress(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 현재 시간
            float nowG = Time.time;

            // 양손둔기 착용 체크
            if (!SkillEffect.IsUsingTwoHandedMace(player))
            {
                SkillEffect.DrawFloatingText(player, "양손둔기를 착용해야 합니다!", Color.red);
                return;
            }

            // 스킬 보유 및 쿨타임 체크
            bool canFuryHammer = SkillEffect.HasSkill("mace_Step7_fury_hammer");
            float cooldown = Mace_Config.FuryHammerCooldownValue; // Config에서 쿨타임 가져오기

            if (canFuryHammer && nowG - lastMaceSkillTime > cooldown)
            {
                Plugin.Log.LogInfo("[분노의 망치] H키 차지 시작");
                isChargingFuryHammer = true;
            }
            else if (canFuryHammer)
            {
                float remainingCooldown = cooldown - (nowG - lastMaceSkillTime);
                SkillEffect.DrawFloatingText(player, $"분노의 망치 쿨타임: {remainingCooldown:F1}초", Color.yellow);
            }
            else
            {
                SkillEffect.DrawFloatingText(player, "분노의 망치 스킬이 필요합니다!", Color.red);
            }
        }

        /// <summary>
        /// H키 해제 처리: 스킬 발동
        /// </summary>
        public static void HandleHKeyRelease(Player player)
        {
            if (!isChargingFuryHammer) return;
            if (player == null || player.IsDead()) return;

            bool canFuryHammer = SkillEffect.HasSkill("mace_Step7_fury_hammer") && SkillEffect.IsUsingTwoHandedMace(player);
            if (canFuryHammer)
            {
                float chargeDuration = Time.time - Time.time; // 차지 시간 계산 (현재는 0)
                isChargingFuryHammer = false;

                // 기존 코루틴 중단
                if (furyHammerCoroutine.ContainsKey(player))
                {
                    SkillTreeInputListener.Instance?.StopCoroutine(furyHammerCoroutine[player]);
                    furyHammerCoroutine.Remove(player);
                    Plugin.Log.LogInfo("[분노의 망치] 기존 코루틴 중단");
                }

                Plugin.Log.LogInfo($"[분노의 망치] H키 해제 - ApplyFuryHammer 호출, 차지: {chargeDuration}");

                // 새 코루틴 시작
                lastMaceSkillTime = Time.time;
                var coroutine = SkillTreeInputListener.Instance.StartCoroutine(ApplyFuryHammer(player, chargeDuration));
                furyHammerCoroutine[player] = coroutine;
            }
        }

        /// <summary>
        /// 분노의 망치 스킬 효과 적용 (5연타)
        /// VFX 규칙 준수: VFXManager.PlayVFXMultiplayer 사용
        /// </summary>
        private static IEnumerator ApplyFuryHammer(Player player, float charge)
        {
            Plugin.Log.LogInfo("[분노의 망치 디버그] ========== 코루틴 시작 ==========");

            // 플레이어 사망 체크
            if (player == null || player.IsDead())
            {
                Plugin.Log.LogInfo("[분노의 망치 디버그] 코루틴 시작 시점 플레이어 사망 감지 - 중단");
                yield break;
            }

            Plugin.Log.LogInfo($"[분노의 망치 디버그] 플레이어 상태 체크 완료 - IsAlive: {!player.IsDead()}, Health: {player.GetHealth():F1}/{player.GetMaxHealth():F1}");

            // 하드코딩 상수 사용 (수정 불가)
            int attackCount = ATTACK_COUNT;               // 5타 고정
            float attackInterval = ATTACK_INTERVAL;       // 0.5초 고정

            // Config에서 값 가져오기 (데미지 배율, AOE 범위만)
            float normalHitMultiplier = Mace_Config.FuryHammerNormalHitMultiplierValue / 100f; // 80% = 0.8
            float finalHitMultiplier = Mace_Config.FuryHammerFinalHitMultiplierValue / 100f;   // 150% = 1.5
            float aoeRadius = Mace_Config.FuryHammerAoeRadiusValue;

            Plugin.Log.LogInfo($"[분노의 망치 디버그] Config 로드 완료 - 타격 횟수: {attackCount} (고정), 간격: {attackInterval}초 (고정), AOE: {aoeRadius}m");

            // 플레이어 현재 공격력 계산 (무기 + 스킬트리 보너스)
            var weapon = player.GetCurrentWeapon();
            if (weapon == null)
            {
                Plugin.Log.LogWarning("[분노의 망치 디버그] 무기가 없어서 스킬 중단");
                yield break;
            }

            // 무기의 기본 데미지 가져오기
            var weaponDamage = weapon.GetDamage();
            float baseWeaponDamage = weaponDamage.GetTotalDamage();

            Plugin.Log.LogInfo($"[분노의 망치 디버그] 무기 기본 데미지: {baseWeaponDamage:F1}, 1~4타 배율: {normalHitMultiplier * 100}%, 5타 배율: {finalHitMultiplier * 100}%");

            int totalHits = 0;

            // 1. 세컨더리 공격 모션 1회 재생 (두손 둔기 내려찍기)
            player.StartAttack(null, true);
            Plugin.Log.LogInfo("[분노의 망치 디버그] 세컨더리 공격 모션 시작");

            // ✅ 세컨더리 공격 모션 완료 대기 (내려찍기 모션 완료 시점에 1타 시작)
            Plugin.Log.LogInfo("[분노의 망치] 세컨더리 공격 모션 완료 대기 (0.8초)...");
            yield return new WaitForSeconds(0.8f);

            // 모션 완료 후 사망 체크
            if (player == null || player.IsDead())
            {
                Plugin.Log.LogInfo("[분노의 망치] 세컨더리 공격 모션 대기 중 사망 감지 - 중단");
                yield break;
            }

            Plugin.Log.LogInfo("[분노의 망치] 세컨더리 공격 모션 완료 - 5연타 시작!");

            // 2. 데미지 5회 자동 적용 (각 타격마다 VFX)
            Plugin.Log.LogInfo("[분노의 망치 디버그] ========== 5연타 루프 시작 ==========");

            // ✅ 스킬 시작 위치 고정 (VFX가 이 위치에서 순차적으로 표시)
            Vector3 fixedVfxOffset = player.transform.forward * 2f;
            Vector3 fixedVfxPosition = player.transform.position + fixedVfxOffset;
            Plugin.Log.LogInfo($"[분노의 망치] 스킬 시작 위치 고정: {fixedVfxPosition}");

            for (int i = 0; i < attackCount; i++)
            {
                Plugin.Log.LogInfo($"[분노의 망치 디버그] ----- {i + 1}타 시작 -----");

                // 플레이어 사망 체크 (매 데미지마다)
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 시작 시점 플레이어 사망 감지 - 중단");
                    yield break;
                }

                Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 플레이어 생존 확인 - Health: {player.GetHealth():F1}/{player.GetMaxHealth():F1}");

                // 타격별 데미지 배율 적용 (1~4타: 80%, 5타: 150%)
                bool isLastAttack = (i == attackCount - 1);
                float damageMultiplier = isLastAttack ? finalHitMultiplier : normalHitMultiplier;
                float totalDamage = baseWeaponDamage * damageMultiplier;

                Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 데미지 계산: {baseWeaponDamage:F1} × {damageMultiplier * 100}% = {totalDamage:F1}");

                // 마지막 공격만 0.5초 대기 후 데미지
                if (isLastAttack)
                {
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] 5타 - 0.5초 대기 시작 (플레이어 Health: {player.GetHealth():F1})");
                    SkillEffect.DrawFloatingText(player, $"⚡ 최종타 준비!", new Color(1f, 0.3f, 0f));

                    yield return new WaitForSeconds(0.5f);

                    Plugin.Log.LogInfo($"[분노의 망치 디버그] 5타 - 0.5초 대기 완료");

                    // 0.5초 대기 직후 사망 체크
                    if (player == null || player.IsDead())
                    {
                        Plugin.Log.LogInfo("[분노의 망치 디버그] 0.5초 대기 후 플레이어 사망 감지 - 코루틴 중단");
                        yield break;
                    }

                    Plugin.Log.LogInfo($"[분노의 망치 디버그] 0.5초 대기 후 플레이어 생존 확인 - Health: {player.GetHealth():F1}");
                }

                // ✅ VFX + SFX 재생 (중복 방지: 각 타격마다 고유한 효과 - 무한 로딩 방지)
                // 사망 체크 (VFX/SFX 호출 직전 - 필수!)
                if (player == null || player.IsDead())
                {
                    Plugin.Log.LogInfo($"[분노의 망치] {i + 1}타 VFX/SFX 호출 직전 사망 감지 - 중단");
                    yield break;
                }

                // 각 타격마다 고유한 VFX + SFX (중복 호출 절대 금지!)
                string vfxName;
                string sfxName;
                float duration;

                switch (i)
                {
                    case 0: // 1타: VFX/SFX 없음 (세컨더리 어택 VFX만 활용)
                        vfxName = "";
                        sfxName = "";
                        duration = 1.5f;
                        break;
                    case 1: // 2타: 노란 플래시 + 철제 둔기 타격음
                        vfxName = "flash_round_ellow";
                        sfxName = "sfx_sledge_iron_hit";
                        duration = 2f;
                        break;
                    case 2: // 3타: 물 폭발 + 철제 둔기 타격음
                        vfxName = "water_blast_blue";
                        sfxName = "sfx_sledge_iron_hit";
                        duration = 2f;
                        break;
                    case 3: // 4타: 별 폭발 + 철제 둔기 타격음
                        vfxName = "flash_star_ellow_purple";
                        sfxName = "sfx_sledge_iron_hit";
                        duration = 2f;
                        break;
                    case 4: // 5타: 최종 폭발 + 철제 둔기 타격음
                        vfxName = "fx_siegebomb_explosion";
                        sfxName = "sfx_sledge_iron_hit";
                        duration = 3f;
                        break;
                    default:
                        vfxName = "";
                        sfxName = "";
                        duration = 1.5f;
                        break;
                }

                SimpleVFX.PlayWithSound(vfxName, sfxName, fixedVfxPosition, duration);
                Plugin.Log.LogInfo($"[분노의 망치] {i + 1}타 효과 재생: VFX={vfxName}, SFX={sfxName} (고정 위치)");

                // 데미지 적용 (고정된 VFX 위치와 동일)
                Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 데미지 적용 시작");

                Vector3 hitPosition = fixedVfxPosition;
                var mobs = Character.GetAllCharacters().Where(c =>
                    c.IsMonsterFaction(0f) &&
                    Vector3.Distance(c.transform.position, hitPosition) < aoeRadius
                );

                int hitCount = 0;
                foreach (var mob in mobs)
                {
                    var hit = new HitData();
                    hit.m_damage.m_blunt = totalDamage;

                    // 넉백 설정
                    hit.m_pushForce = 100f;
                    hit.m_dir = (mob.transform.position - player.transform.position).normalized;
                    hit.m_point = mob.GetCenterPoint();
                    hit.m_attacker = player.GetZDOID();
                    hit.SetAttacker(player);

                    mob.Damage(hit);
                    mob.Stagger(hit.m_dir);
                    hitCount++;
                }

                totalHits += hitCount;

                Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 데미지 적용 완료 - 적중: {hitCount}명, 총 적중: {totalHits}명");

                // 플로팅 텍스트 (데미지 단계별)
                string comboText = $"🔥 {i + 1}타! (데미지 {damageMultiplier:P0}, 적중: {hitCount})";
                SkillEffect.DrawFloatingText(player, comboText, Color.red);

                Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 완료 - 데미지: {totalDamage:F0}, 배율: {damageMultiplier:F2}x, 적중: {hitCount}명");

                // 데미지 간 딜레이 (1~4타: 0.8초 간격, 마지막 데미지 후에는 딜레이 없음)
                if (i < attackCount - 1)
                {
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 → {i + 2}타 딜레이 0.8초 시작 (플레이어 Health: {player.GetHealth():F1})");
                    yield return new WaitForSeconds(0.8f); // 1~4타 간격: 0.8초
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 → {i + 2}타 딜레이 0.8초 완료");

                    // ✅ 대기 직후 사망 체크 (메이지 패턴)
                    if (player == null || player.IsDead())
                    {
                        Plugin.Log.LogInfo($"[분노의 망치] {i + 1}타 대기 후 사망 감지 - 코루틴 중단");
                        yield break;
                    }
                }
            }

            Plugin.Log.LogInfo("[분노의 망치 디버그] ========== 5연타 루프 완료 ==========");

            // 최종 완료 메시지
            SkillEffect.DrawFloatingText(player, $"💥 5연타 완료! (총 {totalHits}명 타격)", new Color(1f, 0.5f, 0f));
            Plugin.Log.LogInfo($"[분노의 망치 디버그] 5연타 완료 - 총 적중: {totalHits}명");

            // 코루틴 정상 종료 시 Dictionary에서 제거
            if (furyHammerCoroutine.ContainsKey(player))
            {
                furyHammerCoroutine.Remove(player);
                Plugin.Log.LogInfo("[분노의 망치 디버그] 코루틴 Dictionary에서 제거 완료");
            }

            Plugin.Log.LogInfo("[분노의 망치 디버그] ========== 코루틴 정상 종료 ==========");
            yield return null;
        }

        /// <summary>
        /// 플레이어 사망 시 분노의 망치 정리 (무한 로딩 방지)
        /// </summary>
        public static void CleanupFuryHammerOnDeath(Player player)
        {
            try
            {
                // 1. 코루틴 중단
                if (furyHammerCoroutine.ContainsKey(player))
                {
                    if (furyHammerCoroutine[player] != null)
                    {
                        try
                        {
                            SkillTreeInputListener.Instance?.StopCoroutine(furyHammerCoroutine[player]);
                            Plugin.Log.LogInfo("[분노의 망치] 코루틴 중단 성공");
                        }
                        catch (Exception ex)
                        {
                            Plugin.Log.LogWarning($"[분노의 망치] 코루틴 중단 실패 (무시): {ex.Message}");
                        }
                    }
                    furyHammerCoroutine.Remove(player);
                }
                else
                {
                }

                // 2. 상태 초기화
                if (player == Player.m_localPlayer)
                {
                    isChargingFuryHammer = false;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[분노의 망치] 정리 실패: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}

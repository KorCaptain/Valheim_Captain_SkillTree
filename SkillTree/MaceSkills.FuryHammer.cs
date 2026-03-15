using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CaptainSkillTree.VFX;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 분노의 망치 스킬 전용 클래스 (둔기 H키 액티브 스킬)
    /// VFX 규칙 준수: VFXManager.PlayVFXMultiplayer 사용
    /// </summary>
    public static class FuryHammerSkill
    {
        // === 정적 필드 ===
        // public static bool isChargingFuryHammer = false; // [제거됨] 차지 시스템 제거로 불필요
        private static Dictionary<Player, Coroutine> furyHammerCoroutine = new Dictionary<Player, Coroutine>();
        private static Dictionary<Player, bool> furyHammer1stHitBuff = new Dictionary<Player, bool>(); // 1타 공격속도 버프
        private static float lastMaceSkillTime = 0f;

        // === 하드코딩 상수 (수정 불가) ===
        private const int ATTACK_COUNT = 5;           // 연속공격 횟수 고정
        private const float ATTACK_INTERVAL = 0.5f;   // 공격간 딜레이 고정 (초)

        /// <summary>
        /// H키 누름 처리: 즉시 스킬 발동
        /// </summary>
        public static void HandleHKeyPress(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 현재 시간
            float nowG = Time.time;

            // 양손둔기 착용 체크
            if (!SkillEffect.IsUsingTwoHandedMace(player))
            {
                SkillEffect.DrawFloatingText(player, L.Get("two_hand_mace_required"), Color.red);
                return;
            }

            // 스킬 보유 및 쿨타임 체크
            bool canFuryHammer = SkillEffect.HasSkill("mace_Step7_fury_hammer");
            float cooldown = Mace_Config.FuryHammerCooldownValue;

            if (canFuryHammer && nowG - lastMaceSkillTime > cooldown)
            {
                Plugin.Log.LogInfo("[분노의 망치] H키 누름 - 즉시 스킬 발동");

                // 기존 코루틴 중단
                if (furyHammerCoroutine.ContainsKey(player))
                {
                    SkillTreeInputListener.Instance?.StopCoroutine(furyHammerCoroutine[player]);
                    furyHammerCoroutine.Remove(player);
                }

                // 새 코루틴 시작
                lastMaceSkillTime = Time.time;
                ActiveSkillCooldownRegistry.SetCooldown("H", cooldown);
                var coroutine = SkillTreeInputListener.Instance.StartCoroutine(ApplyFuryHammer(player, 0f));
                furyHammerCoroutine[player] = coroutine;
            }
            else if (canFuryHammer)
            {
                float remainingCooldown = cooldown - (nowG - lastMaceSkillTime);
                SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_cooldown", $"{remainingCooldown:F1}"), Color.yellow);
            }
            else
            {
                SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_skill_required"), Color.red);
            }
        }

        /// <summary>
        /// H키 해제 처리: 사용하지 않음 (즉시 발동 방식)
        /// </summary>
        public static void HandleHKeyRelease(Player player)
        {
            // H키 누름 시 즉시 발동하므로 해제 처리 불필요
            return;
        }

        /// <summary>
        /// 분노의 망치 1타 공격속도 버프 활성 상태 확인
        /// AnimationSpeedManager와 통합 사용
        /// </summary>
        public static bool IsFuryHammer1stHitBuffActive(Player player)
        {
            if (player == null) return false;
            return furyHammer1stHitBuff.ContainsKey(player) && furyHammer1stHitBuff[player];
        }

        /// <summary>
        /// 분노의 망치 1타 공격속도 버프 보너스 (%)
        /// AnimationSpeedManager가 GetTotalAttackSpeedBonus()를 통해 자동 적용
        /// </summary>
        public static float GetFuryHammer1stHitSpeedBonus(Player player)
        {
            return IsFuryHammer1stHitBuffActive(player) ? 200f : 0f;
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

            // 데미지 5회 자동 적용 (1타만 플레이어 공격 모션, 나머지는 VFX만)
            Plugin.Log.LogInfo("[분노의 망치 디버그] ========== 5연타 루프 시작 ==========");

            // ✅ 스킬 시작 위치: 화면 중앙 카메라 방향
            Vector3 fixedVfxOffset = player.GetLookDir() * 2f;
            Vector3 fixedVfxPosition = player.transform.position + fixedVfxOffset;
            Plugin.Log.LogInfo($"[분노의 망치] 스킬 시작 위치 (카메라 방향): {fixedVfxPosition}");

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

                // === 1타: 플레이어 공격 모션 → 데미지 → 적중 확인 → 중력+VFX ===
                if (i == 0)
                {
                    // 1타 공격속도 버프 활성화
                    furyHammer1stHitBuff[player] = true;
                    Plugin.Log.LogInfo("[분노의 망치] 1타 공격속도 버프 활성화 (+200%)");

                    // ✅ 공격 모션 중 넉백 차단 (무기 넉백 힘 임시 저장)
                    float originalPushForce = 0f;
                    if (weapon != null && weapon.m_shared != null)
                    {
                        originalPushForce = weapon.m_shared.m_attackForce;
                        weapon.m_shared.m_attackForce = 0f;
                        Plugin.Log.LogInfo($"[분노의 망치] 무기 넉백 임시 차단 (원본: {originalPushForce})");
                    }

                    // 일반 공격 모션 실행 (넉백 없음)
                    player.StartAttack(null, false);
                    Plugin.Log.LogInfo("[분노의 망치] 1타: 일반 공격 모션 시작 (+200% 공격속도 적용, 넉백 차단)");

                    // 공격 모션 완료 대기 (3배 빠르므로 약 0.27초)
                    yield return new WaitForSeconds(0.27f);

                    // ✅ 무기 넉백 힘 복원
                    if (weapon != null && weapon.m_shared != null)
                    {
                        weapon.m_shared.m_attackForce = originalPushForce;
                        Plugin.Log.LogInfo($"[분노의 망치] 무기 넉백 복원 (복원값: {originalPushForce})");
                    }

                    // 모션 완료 후 사망 체크
                    if (player == null || player.IsDead())
                    {
                        Plugin.Log.LogInfo("[분노의 망치] 1타 공격 모션 대기 중 사망 감지 - 중단");
                        yield break;
                    }

                    // 1타 공격속도 버프 비활성화
                    furyHammer1stHitBuff[player] = false;
                    // 경고 상태 초기화: 다음 일반 공격 시 속도가 캡 하에서 정상 동작하도록
                    CaptainSkillTree.AttackSpeedHandler_Game_Awake_Patch.ClearAttackSpeedWarningState(player);
                    Plugin.Log.LogInfo("[분노의 망치] 1타 공격속도 버프 비활성화 (일반 공격 완료)");

                    // ✅ 데미지 먼저 적용 (넉백 0)
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] 1타 데미지 적용 시작");

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

                        // ✅ 1타는 넉백 제거 (중력효과와 충돌 방지)
                        hit.m_pushForce = 0f;
                        hit.m_dir = (mob.transform.position - player.transform.position).normalized;
                        hit.m_point = mob.GetCenterPoint();
                        hit.m_attacker = player.GetZDOID();
                        hit.SetAttacker(player);

                        mob.Damage(hit);
                        // ✅ 1타는 Stagger도 제거 (중력효과 유지)
                        // mob.Stagger(hit.m_dir);
                        hitCount++;
                    }

                    totalHits += hitCount;
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] 1타 데미지 적용 완료 - 적중: {hitCount}명 (넉백 없음)");

                    // ✅ 적중 확인 후 VFX 재생 (중력효과 제거됨)
                    if (hitCount > 0)
                    {
                        // VFX + SFX 재생
                        SimpleVFX.PlayWithSound("flash_round_ellow", "sfx_sledge_iron_hit", fixedVfxPosition, 2f);
                        Plugin.Log.LogInfo($"[분노의 망치] 1타 적중 확인 - VFX 재생 ({hitCount}명 적중)");
                    }
                    else
                    {
                        Plugin.Log.LogInfo("[분노의 망치] 1타 빗나감 - VFX 스킵");
                    }

                    Plugin.Log.LogInfo($"[분노의 망치 디버그] 1타 완료 - 데미지: {totalDamage:F0}, 배율: {damageMultiplier:F2}x, 적중: {hitCount}명");
                }
                // === 2~5타: VFX 먼저 → 데미지 ===
                else
                {
                    // VFX + SFX 재생
                    string vfxName;
                    string sfxName;
                    float duration;

                    switch (i)
                    {
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
                    Plugin.Log.LogInfo($"[분노의 망치] {i + 1}타 효과 재생: VFX={vfxName}, SFX={sfxName}");

                    // 마지막 공격만 0.5초 대기 후 데미지
                    if (isLastAttack)
                    {
                        Plugin.Log.LogInfo($"[분노의 망치 디버그] 5타 - 0.5초 대기 시작");
                        SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_final_hit_ready"), new Color(1f, 0.3f, 0f));

                        yield return new WaitForSeconds(0.5f);

                        if (player == null || player.IsDead())
                        {
                            Plugin.Log.LogInfo("[분노의 망치 디버그] 0.5초 대기 후 플레이어 사망 감지 - 중단");
                            yield break;
                        }

                        Plugin.Log.LogInfo($"[분노의 망치 디버그] 5타 - 0.5초 대기 완료");
                    }

                    // 데미지 적용
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

                        // ✅ 넉백 제거 (중력효과 유지)
                        hit.m_pushForce = 0f;
                        hit.m_dir = (mob.transform.position - player.transform.position).normalized;
                        hit.m_point = mob.GetCenterPoint();
                        hit.m_attacker = player.GetZDOID();
                        hit.SetAttacker(player);

                        mob.Damage(hit);
                        // ✅ 스태거 제거 (중력효과 유지)
                        // mob.Stagger(hit.m_dir);
                        hitCount++;
                    }

                    totalHits += hitCount;
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 데미지 적용 완료 - 적중: {hitCount}명");

                    // ✅ 개별 메시지 제거 - totalHits에만 누적

                    Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 완료 - 데미지: {totalDamage:F0}, 배율: {damageMultiplier:F2}x, 적중: {hitCount}명");
                }

                // 타격 간 딜레이 (1→2, 2→3, 3→4: 0.8초 / 4→5: 1.2초)
                if (i < attackCount - 1)
                {
                    float delayTime;
                    if (i == 3) // 4타 → 5타
                    {
                        delayTime = 1.2f;
                    }
                    else // 1→2, 2→3, 3→4
                    {
                        delayTime = 0.8f;
                    }

                    Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 → {i + 2}타 딜레이 {delayTime}초 시작");
                    yield return new WaitForSeconds(delayTime);
                    Plugin.Log.LogInfo($"[분노의 망치 디버그] {i + 1}타 → {i + 2}타 딜레이 {delayTime}초 완료");

                    // 대기 직후 사망 체크
                    if (player == null || player.IsDead())
                    {
                        Plugin.Log.LogInfo($"[분노의 망치] {i + 1}타 대기 후 사망 감지 - 코루틴 중단");
                        yield break;
                    }
                }
            }

            Plugin.Log.LogInfo("[분노의 망치 디버그] ========== 5연타 루프 완료 ==========");

            // 최종 완료 메시지
            SkillEffect.DrawFloatingText(player, L.Get("fury_hammer_combo_complete", totalHits.ToString()), new Color(1f, 0.5f, 0f));
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

        // [제거됨] ApplyGravityEffectSmoothly - 중력 효과 제거

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

                // 2. 상태 초기화 (차지 시스템 제거로 불필요)

                // 3. 1타 공격속도 버프 정리
                if (furyHammer1stHitBuff.ContainsKey(player))
                {
                    furyHammer1stHitBuff.Remove(player);
                    Plugin.Log.LogInfo("[분노의 망치] 1타 공격속도 버프 정리 완료");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[분노의 망치] 정리 실패: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}

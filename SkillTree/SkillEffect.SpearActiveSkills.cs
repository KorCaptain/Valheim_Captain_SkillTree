using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Gui;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 창 투창 전문가 패시브 및 연공창 액티브 스킬 시스템
    /// H키: 30초 버프 활성화 (즉시 공격 X)
    /// 버프 중 세컨더리 어택(휠마우스 창 던지기) 시 특수 효과 발동
    /// </summary>
    public static partial class SkillEffect
    {
        // === 투창 전문가 패시브 쿨타임 ===
        private static Dictionary<Player, float> spearThrowPassiveCooldown = new Dictionary<Player, float>();

        // === 연공창 액티브 스킬 상태 ===
        private static Dictionary<Player, float> spearEnhancedThrowCooldowns = new Dictionary<Player, float>();
        public static Dictionary<Player, float> spearEnhancedThrowBuffEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, int> spearComboThrowUsesRemaining = new Dictionary<Player, int>();

        // 마지막으로 사용한 창 아이템 저장 (회수용)
        public static Dictionary<Player, ItemDrop.ItemData> lastThrownSpear = new Dictionary<Player, ItemDrop.ItemData>();

        // 버프 VFX 인스턴스 저장 (상태 관리용)
        private static Dictionary<Player, GameObject> spearBuffVFXInstances = new Dictionary<Player, GameObject>();

        /// <summary>
        /// 투창 전문가 패시브 쿨타임 확인
        /// </summary>
        public static bool CanUseSpearThrowPassive(Player player)
        {
            if (player == null) return false;
            float now = Time.time;
            if (spearThrowPassiveCooldown.ContainsKey(player) && now < spearThrowPassiveCooldown[player])
                return false;
            return true;
        }

        /// <summary>
        /// 투창 전문가 패시브 쿨타임 설정
        /// </summary>
        public static void SetSpearThrowPassiveCooldown(Player player)
        {
            if (player == null) return;
            float cooldown = SkillTreeConfig.SpearStep2ThrowCooldownValue;
            spearThrowPassiveCooldown[player] = Time.time + cooldown;
        }

        /// <summary>
        /// 창 연공창 액티브 스킬 (H키)
        /// 30초 버프 활성화 - 세컨더리 어택 시 특수 효과 발동
        /// </summary>
        public static void HandleSpearActiveSkill(Player player)
        {
            try
            {
                if (player == null || player.IsDead()) return;

                // 스킬 보유 확인
                if (!HasSkill("spear_Step5_combo"))
                {
                    DrawFloatingText(player, "연공창 스킬이 필요합니다", Color.red);
                    return;
                }

                // 창 착용 확인
                if (!IsUsingSpear(player))
                {
                    DrawFloatingText(player, "창을 착용해야 합니다", Color.red);
                    return;
                }

                // 쿨타임 확인
                float now = Time.time;
                if (spearEnhancedThrowCooldowns.ContainsKey(player) && now < spearEnhancedThrowCooldowns[player])
                {
                    float remaining = spearEnhancedThrowCooldowns[player] - now;
                    DrawFloatingText(player, $"쿨타임: {Mathf.CeilToInt(remaining)}초", Color.yellow);
                    return;
                }

                // 스태미나 확인
                float maxStamina = player.GetMaxStamina();
                float requiredStamina = maxStamina * (SkillTreeConfig.SpearStep2ThrowStaminaCostValue / 100f);
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, "스태미나 부족", Color.red);
                    return;
                }

                // 버프 활성화 (즉시 공격 X)
                ActivateSpearComboThrowBuff(player);

                // 쿨타임 및 스태미나 소모 적용
                spearEnhancedThrowCooldowns[player] = now + Spear_Config.SpearStep6ComboCooldownValue;
                player.UseStamina(requiredStamina);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 연공창 버프 활성화 (H키)
        /// </summary>
        private static void ActivateSpearComboThrowBuff(Player player)
        {
            try
            {
                float buffDuration = Spear_Config.SpearStep6ComboBuffDurationValue;
                int maxUses = Spear_Config.SpearStep6ComboMaxUsesValue;
                float damageBonus = Spear_Config.SpearStep6ComboDamageValue;

                // 버프 상태 설정
                spearEnhancedThrowBuffEndTime[player] = Time.time + buffDuration;
                spearComboThrowUsesRemaining[player] = maxUses;

                // 버프 VFX 생성 - statusailment_01_aura (머리 위, 버프 동안 유지)
                CreateSpearBuffVFX(player);

                // 버프 UI 표시
                SkillBuffDisplay.Instance?.ShowBuff(
                    "spear_combo_throw",
                    $"연공창 ({maxUses}회)",
                    buffDuration,
                    new Color(1f, 0.8f, 0.2f, 1f),
                    "🏹"
                );

                DrawFloatingText(player, $"[연공창] 버프 활성화! ({maxUses}회, +{damageBonus}%)", new Color(1f, 0.8f, 0.2f, 1f));
                Plugin.Log.LogInfo($"[연공창] 버프 활성화 - 지속시간: {buffDuration}초, 최대 사용 횟수: {maxUses}회");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창 버프 활성화] 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 연공창 버프 VFX 생성 (머리 위)
        /// </summary>
        private static void CreateSpearBuffVFX(Player player)
        {
            try
            {
                // 기존 VFX가 있으면 제거
                RemoveSpearBuffVFX(player);

                // 새 VFX 생성 - statusailment_01_aura (머리 높이, 수동 관리)
                var vfx = SimpleVFX.PlayOnPlayer(player, "statusailment_01_aura", 9999f, new Vector3(0f, 1.2f, 0f));
                if (vfx != null)
                {
                    spearBuffVFXInstances[player] = vfx;
                    Plugin.Log.LogInfo("[연공창] 버프 VFX 생성됨");

                    // 버프 만료 시 VFX 자동 제거 코루틴 시작
                    player.StartCoroutine(MonitorSpearBuffExpiration(player));
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[연공창] 버프 VFX 생성 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// 버프 만료 모니터링 코루틴 - 3회 사용 또는 30초 만료 시 VFX 제거
        /// </summary>
        private static IEnumerator MonitorSpearBuffExpiration(Player player)
        {
            while (player != null && !player.IsDead())
            {
                // 버프 상태 확인
                if (!IsSpearComboThrowBuffActive(player))
                {
                    // 버프 종료 - VFX 제거
                    RemoveSpearBuffVFX(player);
                    SkillBuffDisplay.Instance?.RemoveBuff("spear_combo_throw");
                    Plugin.Log.LogInfo("[연공창] 버프 만료 - VFX 제거됨");
                    yield break;
                }

                yield return new WaitForSeconds(0.5f);
            }

            // 플레이어 사망 또는 null - VFX 정리
            RemoveSpearBuffVFX(player);
        }

        /// <summary>
        /// 연공창 버프 VFX 제거
        /// </summary>
        private static void RemoveSpearBuffVFX(Player player)
        {
            try
            {
                if (spearBuffVFXInstances.TryGetValue(player, out var vfx) && vfx != null)
                {
                    UnityEngine.Object.Destroy(vfx);
                    Plugin.Log.LogInfo("[연공창] 버프 VFX 제거됨");
                }
                spearBuffVFXInstances.Remove(player);
            }
            catch { }
        }

        /// <summary>
        /// 연공창 버프 활성 상태 확인
        /// </summary>
        public static bool IsSpearComboThrowBuffActive(Player player)
        {
            if (player == null) return false;
            if (!spearEnhancedThrowBuffEndTime.ContainsKey(player)) return false;
            if (Time.time >= spearEnhancedThrowBuffEndTime[player]) return false;
            if (!spearComboThrowUsesRemaining.ContainsKey(player)) return false;
            if (spearComboThrowUsesRemaining[player] <= 0) return false;
            return true;
        }

        /// <summary>
        /// 연공창 사용 횟수 감소 및 버프 상태 업데이트 (창 던질 때 호출)
        /// </summary>
        public static void ConsumeSpearComboThrowUse(Player player)
        {
            if (player == null) return;
            if (!spearComboThrowUsesRemaining.ContainsKey(player)) return;

            // 창 던질 때 VFX 제거
            RemoveSpearBuffVFX(player);

            spearComboThrowUsesRemaining[player]--;
            int remaining = spearComboThrowUsesRemaining[player];

            Plugin.Log.LogInfo($"[연공창] 사용 횟수 감소 - 남은 횟수: {remaining}");

            if (remaining <= 0)
            {
                // 버프 종료 - VFX도 제거됨
                spearEnhancedThrowBuffEndTime[player] = 0f;
                SkillBuffDisplay.Instance?.RemoveBuff("spear_combo_throw");
                DrawFloatingText(player, "[연공창] 버프 종료", Color.gray);
            }
            else
            {
                // 남은 횟수 텍스트로 표시
                DrawFloatingText(player, $"[연공창] 남은 횟수: {remaining}회", new Color(1f, 0.8f, 0.2f, 1f));
            }
        }

        /// <summary>
        /// 창 자동 회수 - 현재 장착 중인 창과 동일한 창을 인벤토리에 추가 후 자동 장착
        /// </summary>
        public static void AutoReturnSpearToInventory(Player player)
        {
            try
            {
                if (player == null) return;

                var inventory = player.GetInventory();
                if (inventory == null) return;

                // 마지막으로 던진 창 정보 확인
                if (!lastThrownSpear.ContainsKey(player) || lastThrownSpear[player] == null)
                {
                    Plugin.Log.LogWarning("[연공창] 회수할 창 정보 없음");
                    DrawFloatingText(player, "[연공창] 창 회수 실패", Color.red);
                    return;
                }

                var thrownSpear = lastThrownSpear[player];
                string spearName = thrownSpear.m_shared.m_name;

                // 아이템 데이터 복제하여 추가 (m_equipped 초기화 필수!)
                var newSpear = thrownSpear.Clone();
                newSpear.m_stack = 1;
                newSpear.m_equipped = false;  // 중요: 장착 상태 초기화
                if (inventory.AddItem(newSpear))
                {
                    // 버프가 아직 남아있으면 VFX 다시 생성
                    if (IsSpearComboThrowBuffActive(player))
                    {
                        CreateSpearBuffVFX(player);
                    }

                    // 자동 장착 (지연 실행으로 인벤토리 동기화 대기)
                    player.StartCoroutine(DelayedEquipSpear(player, spearName));

                    DrawFloatingText(player, "[연공창] 창 회수 & 장착!", new Color(0.5f, 1f, 0.5f, 1f));
                    Plugin.Log.LogInfo($"[연공창] 창 회수 및 장착 성공: {spearName}");
                    return;
                }

                Plugin.Log.LogWarning($"[연공창] 창 회수 실패: {spearName}");
                DrawFloatingText(player, "[연공창] 인벤토리 가득!", Color.red);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창] 창 회수 오류: {ex.Message}");
            }
        }

        // 리플렉션 캐시 (m_rightItem 접근용)
        private static System.Reflection.FieldInfo _rightItemField = null;
        private static System.Reflection.FieldInfo RightItemField
        {
            get
            {
                if (_rightItemField == null)
                    _rightItemField = AccessTools.Field(typeof(Humanoid), "m_rightItem");
                return _rightItemField;
            }
        }

        /// <summary>
        /// 창 자동 장착 코루틴 (지연 실행으로 인벤토리 동기화 대기)
        /// </summary>
        private static IEnumerator DelayedEquipSpear(Player player, string spearName)
        {
            // 프레임 대기 (인벤토리 동기화)
            yield return null;

            if (player == null || player.IsDead()) yield break;

            // ★ 핵심: 플레이어가 공격/애니메이션 중이면 끝날 때까지 대기
            float waitTimeout = 2f;
            float waitStart = Time.time;
            while (player != null && !player.IsDead() && Time.time - waitStart < waitTimeout)
            {
                // InAttack, InDodge, IsBlocking 등 행동 중이면 대기
                bool isBusy = player.InAttack() || player.InDodge() || player.IsBlocking() ||
                              player.IsStaggering() || player.InPlaceMode();

                if (!isBusy)
                {
                    Plugin.Log.LogInfo($"[연공창] 플레이어 대기 상태 확인 완료 ({Time.time - waitStart:F2}초 대기)");
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.15f);

            if (player == null || player.IsDead()) yield break;

            var inventory = player.GetInventory();
            if (inventory == null) yield break;

            // 먼저 현재 무기 확인 - 이미 해당 창을 들고 있으면 성공
            var currentCheck = player.GetCurrentWeapon();
            if (currentCheck != null && currentCheck.m_shared.m_name == spearName)
            {
                Plugin.Log.LogInfo($"[연공창] 이미 해당 창 장착됨: {spearName}");
                yield break;
            }

            // 인벤토리에서 창 찾기 (m_equipped 플래그 무시하고 찾기)
            ItemDrop.ItemData spearToEquip = null;
            foreach (var item in inventory.GetAllItems())
            {
                if (item != null && item.m_shared.m_name == spearName &&
                    item.m_shared.m_skillType == Skills.SkillType.Spears)
                {
                    // m_equipped 상태 강제 초기화 후 선택
                    item.m_equipped = false;
                    spearToEquip = item;
                    break;
                }
            }

            if (spearToEquip == null)
            {
                Plugin.Log.LogWarning($"[연공창] 인벤토리에서 창을 찾을 수 없음: {spearName}");
                yield break;
            }

            Plugin.Log.LogInfo($"[연공창] 장착 시도 시작: {spearName}");

            // ★ 최대 3회 재시도 루프
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                if (player == null || player.IsDead()) yield break;

                // 장착 전 다시 한번 상태 체크
                if (player.InAttack() || player.InDodge())
                {
                    Plugin.Log.LogInfo($"[연공창] 시도 {attempt}: 플레이어 행동 중, 대기...");
                    yield return new WaitForSeconds(0.3f);
                    continue;
                }

                // 현재 오른손 무기 확인 및 해제
                var currentWeapon = player.GetCurrentWeapon();
                if (currentWeapon != null && currentWeapon.m_equipped && currentWeapon != spearToEquip)
                {
                    Plugin.Log.LogInfo($"[연공창] 시도 {attempt}: 현재 무기 해제: {currentWeapon.m_shared.m_name}");
                    player.UnequipItem(currentWeapon, false);
                    yield return new WaitForSeconds(0.1f);
                }

                // 손 숨기기
                player.HideHandItems();
                yield return new WaitForSeconds(0.05f);

                // m_equipped 상태 강제 초기화
                spearToEquip.m_equipped = false;

                // EquipItem 시도
                bool success = false;
                try
                {
                    success = player.EquipItem(spearToEquip, true);
                    Plugin.Log.LogInfo($"[연공창] 시도 {attempt}: EquipItem 결과: {success}");
                }
                catch (Exception ex)
                {
                    Plugin.Log.LogWarning($"[연공창] 시도 {attempt}: EquipItem 예외: {ex.Message}");
                }

                yield return new WaitForSeconds(0.2f);

                // 장착 확인
                currentWeapon = player.GetCurrentWeapon();
                if (currentWeapon != null && currentWeapon.m_shared.m_name == spearName)
                {
                    Plugin.Log.LogInfo($"[연공창] 창 자동 장착 성공! (시도 {attempt})");
                    yield break;
                }

                // 실패 시 UseItem으로 재시도
                if (attempt < 3)
                {
                    try
                    {
                        spearToEquip.m_equipped = false;
                        player.UseItem(inventory, spearToEquip, false);
                        Plugin.Log.LogInfo($"[연공창] 시도 {attempt}: UseItem 재시도");
                    }
                    catch { }

                    yield return new WaitForSeconds(0.25f);

                    // UseItem 후 장착 확인
                    currentWeapon = player.GetCurrentWeapon();
                    if (currentWeapon != null && currentWeapon.m_shared.m_name == spearName)
                    {
                        Plugin.Log.LogInfo($"[연공창] 창 자동 장착 성공! (UseItem, 시도 {attempt})");
                        yield break;
                    }
                }
            }

            // 3회 시도 후 최종 확인
            var finalWeapon = player.GetCurrentWeapon();
            if (finalWeapon != null && finalWeapon.m_shared.m_name == spearName)
            {
                Plugin.Log.LogInfo($"[연공창] 창 자동 장착 최종 성공: {spearName}");
            }
            else
            {
                string currentName = finalWeapon != null ? finalWeapon.m_shared.m_name : "없음";
                Plugin.Log.LogWarning($"[연공창] 창 자동 장착 실패 (3회 시도). 현재 무기: {currentName}");
            }
        }

        /// <summary>
        /// 창 강화 투척 정리 (플레이어 사망 시)
        /// </summary>
        public static void CleanupSpearEnhancedThrowOnDeath(Player player)
        {
            try
            {
                // VFX 제거
                RemoveSpearBuffVFX(player);

                spearEnhancedThrowCooldowns.Remove(player);
                spearEnhancedThrowBuffEndTime.Remove(player);
                spearComboThrowUsesRemaining.Remove(player);
                lastThrownSpear.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[창 강화 투척] 정리 실패: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 연공창 강화 투사체 태그 컴포넌트
    /// </summary>
    public class ComboSpearProjectileTag : MonoBehaviour
    {
        public Player thrower;
        public bool processed = false;
    }

    /// <summary>
    /// 창 투사체 생성 감지 패치 (Projectile.Setup Postfix) - 시그니처 수정됨
    /// Valheim API: Setup(Character owner, Vector3 velocity, float hitNoise, HitData hitData, ItemDrop.ItemData item, ItemDrop.ItemData ammo)
    /// </summary>
    [HarmonyPatch(typeof(Projectile), "Setup",
        new Type[] { typeof(Character), typeof(Vector3), typeof(float),
                     typeof(HitData), typeof(ItemDrop.ItemData), typeof(ItemDrop.ItemData) })]
    [HarmonyPriority(Priority.Low)]
    public static class SpearComboThrow_ProjectileSetup_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Projectile __instance, Character owner,
            Vector3 velocity, float hitNoise, HitData hitData,
            ItemDrop.ItemData item, ItemDrop.ItemData ammo)
        {
            try
            {
                var player = owner as Player;
                if (player == null) return;

                // 연공창 버프 활성 확인
                if (!SkillEffect.IsSpearComboThrowBuffActive(player)) return;

                // 창 투사체인지 확인
                if (__instance.m_skill != Skills.SkillType.Spears) return;

                // 이미 태그가 있으면 무시
                if (__instance.GetComponent<ComboSpearProjectileTag>() != null) return;

                // 던진 창 정보 저장 (item 파라미터 사용)
                if (item != null && item.m_shared.m_skillType == Skills.SkillType.Spears)
                {
                    var clonedSpear = item.Clone();
                    clonedSpear.m_equipped = false;  // 중요: 장착 상태 초기화
                    SkillEffect.lastThrownSpear[player] = clonedSpear;
                    Plugin.Log.LogInfo($"[연공창] 던진 창 저장: {item.m_shared.m_name}");
                }

                // 태그 추가
                var tag = __instance.gameObject.AddComponent<ComboSpearProjectileTag>();
                tag.thrower = player;
                tag.processed = false;

                // 데미지 강화 (280%)
                float damageMultiplier = Spear_Config.SpearStep6ComboDamageValue / 100f;
                var damage = __instance.m_damage;
                damage.m_pierce *= damageMultiplier;
                damage.m_slash *= damageMultiplier;
                damage.m_blunt *= damageMultiplier;
                __instance.m_damage = damage;

                // 발사 VFX
                try
                {
                    Vector3 vfxPos = player.transform.position + player.transform.forward * 1.5f + Vector3.up * 1.2f;
                    SimpleVFX.Play("hit_03", vfxPos, 1.5f);
                }
                catch { }

                // 사용 횟수 감소
                SkillEffect.ConsumeSpearComboThrowUse(player);

                Plugin.Log.LogInfo($"[연공창] 강화 투창 발사! 데미지 배율: {damageMultiplier}x");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창 투사체 설정] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 창 투사체 적중 감지 패치 (Projectile.OnHit Postfix) - 시그니처 수정됨
    /// Valheim API: OnHit(Collider collider, Vector3 hitPoint, bool water, Vector3 normal)
    /// 땅/몬스터/환경 상관없이 무조건 회수
    /// </summary>
    [HarmonyPatch(typeof(Projectile), "OnHit",
        new Type[] { typeof(Collider), typeof(Vector3), typeof(bool), typeof(Vector3) })]
    [HarmonyPriority(Priority.Low)]
    public static class SpearComboThrow_ProjectileHit_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Projectile __instance, Collider collider,
            Vector3 hitPoint, bool water, Vector3 normal)
        {
            try
            {
                var tag = __instance.GetComponent<ComboSpearProjectileTag>();
                if (tag == null) return;
                if (tag.processed) return;
                tag.processed = true;

                // hitPoint 파라미터 직접 사용 (정확한 위치)

                // 적중 VFX (물 제외)
                if (!water)
                {
                    try
                    {
                        // 몬스터/땅 상관없이 VFX
                        SimpleVFX.Play("hit_01", hitPoint, 1.5f);

                        // 몬스터 적중 시 추가 VFX
                        Character hitTarget = null;
                        if (collider != null)
                        {
                            hitTarget = collider.GetComponent<Character>();
                            if (hitTarget == null)
                                hitTarget = collider.GetComponentInParent<Character>();
                        }

                        if (hitTarget != null && !hitTarget.IsPlayer())
                        {
                            // 몬스터에게 커스텀 VFX 표시
                            SimpleVFX.Play("flash_star_ellow_purple", hitPoint, 2f);

                            // 몬스터 적중 사운드 (fx_crit)
                            CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("", "fx_crit", hitPoint, Quaternion.identity, 1f);

                            Plugin.Log.LogInfo($"[연공창] 몬스터 적중! 위치: {hitPoint}");
                        }
                        else
                        {
                            Plugin.Log.LogInfo($"[연공창] 지면/환경 적중! 위치: {hitPoint}");
                        }
                    }
                    catch (Exception vfxEx)
                    {
                        Plugin.Log.LogWarning($"[연공창] 적중 VFX 오류: {vfxEx.Message}");
                    }
                }

                // 창 자동 회수 (땅/몬스터/환경 상관없이 무조건)
                if (tag.thrower != null)
                {
                    SkillEffect.AutoReturnSpearToInventory(tag.thrower);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창 적중 패치] 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 창 드롭 방지 패치 - 연공창 투사체는 땅에 아이템 생성 안 함
    /// Valheim API: SpawnOnHit(GameObject go, Collider collider, Vector3 normal)
    /// </summary>
    [HarmonyPatch(typeof(Projectile), "SpawnOnHit",
        new Type[] { typeof(GameObject), typeof(Collider), typeof(Vector3) })]
    [HarmonyPriority(Priority.High)]
    public static class SpearComboThrow_PreventDrop_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(Projectile __instance, GameObject go,
            Collider collider, Vector3 normal)
        {
            try
            {
                var tag = __instance.GetComponent<ComboSpearProjectileTag>();
                if (tag != null)
                {
                    // 연공창 투사체는 드롭 아이템 생성 방지
                    Plugin.Log.LogInfo("[연공창] 드롭 아이템 생성 방지");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창 드롭 방지] 오류: {ex.Message}");
            }
            return true;
        }
    }
}

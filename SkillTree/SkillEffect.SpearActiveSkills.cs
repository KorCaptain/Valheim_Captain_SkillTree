using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CaptainSkillTree.Gui;
using CaptainSkillTree.Localization;

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
                    DrawFloatingText(player, L.Get("combo_spear_skill_required"), Color.red);
                    return;
                }

                // 창 착용 확인
                if (!IsUsingSpear(player))
                {
                    DrawFloatingText(player, L.Get("spear_equip_required"), Color.red);
                    return;
                }

                // 쿨타임 확인
                float now = Time.time;
                if (spearEnhancedThrowCooldowns.ContainsKey(player) && now < spearEnhancedThrowCooldowns[player])
                {
                    float remaining = spearEnhancedThrowCooldowns[player] - now;
                    DrawFloatingText(player, L.Get("cooldown_seconds", Mathf.CeilToInt(remaining).ToString()), Color.yellow);
                    return;
                }

                // 스태미나 확인
                float maxStamina = player.GetMaxStamina();
                float requiredStamina = maxStamina * (SkillTreeConfig.SpearStep2ThrowStaminaCostValue / 100f);
                if (player.GetStamina() < requiredStamina)
                {
                    DrawFloatingText(player, L.Get("stamina_insufficient"), Color.red);
                    return;
                }

                // 버프 활성화 (즉시 공격 X)
                ActivateSpearComboThrowBuff(player);

                // 쿨타임 및 스태미나 소모 적용
                spearEnhancedThrowCooldowns[player] = now + Spear_Config.SpearStep6ComboCooldownValue;
                ActiveSkillCooldownRegistry.SetCooldown("H", Spear_Config.SpearStep6ComboCooldownValue);
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
                    L.Get("combo_spear_buff_name", maxUses.ToString()),
                    buffDuration,
                    new Color(1f, 0.8f, 0.2f, 1f),
                    "🏹"
                );

                DrawFloatingText(player, L.Get("combo_spear_buff_activated", maxUses.ToString(), damageBonus.ToString()), new Color(1f, 0.8f, 0.2f, 1f));
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
                    RemoveSpearBuffVFX(player);
                    SkillBuffDisplay.Instance?.RemoveBuff("spear_combo_throw");
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

            if (remaining <= 0)
            {
                // 버프 종료 - VFX도 제거됨
                spearEnhancedThrowBuffEndTime[player] = 0f;
                SkillBuffDisplay.Instance?.RemoveBuff("spear_combo_throw");
                DrawFloatingText(player, L.Get("combo_spear_buff_ended"), Color.gray);
            }
            else
            {
                // 남은 횟수 텍스트로 표시
                DrawFloatingText(player, L.Get("combo_spear_uses_remaining", remaining.ToString()), new Color(1f, 0.8f, 0.2f, 1f));
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
                    DrawFloatingText(player, L.Get("combo_spear_retrieve_failed"), Color.red);
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
                    if (IsSpearComboThrowBuffActive(player))
                    {
                        CreateSpearBuffVFX(player);
                    }
                    player.StartCoroutine(DelayedEquipSpear(player, spearName));
                    DrawFloatingText(player, L.Get("combo_spear_retrieved_equipped"), new Color(0.5f, 1f, 0.5f, 1f));
                    return;
                }
                DrawFloatingText(player, L.Get("combo_spear_inventory_full"), Color.red);
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
                bool isBusy = player.InAttack() || player.InDodge() || player.IsBlocking() ||
                              player.IsStaggering() || player.InPlaceMode();
                if (!isBusy) break;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.15f);

            if (player == null || player.IsDead()) yield break;

            var inventory = player.GetInventory();
            if (inventory == null) yield break;

            var currentCheck = player.GetCurrentWeapon();
            if (currentCheck != null && currentCheck.m_shared.m_name == spearName)
                yield break;

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
                yield break;

            // ★ 최대 3회 재시도 루프
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                if (player == null || player.IsDead()) yield break;

                if (player.InAttack() || player.InDodge())
                {
                    yield return new WaitForSeconds(0.3f);
                    continue;
                }

                var currentWeapon = player.GetCurrentWeapon();
                if (currentWeapon != null && currentWeapon.m_equipped && currentWeapon != spearToEquip)
                {
                    player.UnequipItem(currentWeapon, false);
                    yield return new WaitForSeconds(0.1f);
                }

                // 손 숨기기
                player.HideHandItems();
                yield return new WaitForSeconds(0.05f);

                // m_equipped 상태 강제 초기화
                spearToEquip.m_equipped = false;

                bool success = false;
                try
                {
                    success = player.EquipItem(spearToEquip, true);
                }
                catch { }

                yield return new WaitForSeconds(0.2f);

                currentWeapon = player.GetCurrentWeapon();
                if (currentWeapon != null && currentWeapon.m_shared.m_name == spearName)
                    yield break;

                if (attempt < 3)
                {
                    try
                    {
                        spearToEquip.m_equipped = false;
                        player.UseItem(inventory, spearToEquip, false);
                    }
                    catch { }

                    yield return new WaitForSeconds(0.25f);

                    currentWeapon = player.GetCurrentWeapon();
                    if (currentWeapon != null && currentWeapon.m_shared.m_name == spearName)
                        yield break;
                }
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
    /// 이동 중 2m 반경 내 몬스터 자동 적중 및 넉백 처리
    /// </summary>
    public class ComboSpearProjectileTag : MonoBehaviour
    {
        public Player thrower;
        public bool processed = false;

        // 적중 처리된 몬스터 추적 (중복 데미지 방지)
        private HashSet<Character> hitTargets = new HashSet<Character>();

        // 적중 반경 (2m)
        private const float HIT_RADIUS = 2f;
        // 넉백 힘
        private const float KNOCKBACK_FORCE = 15f;
        // 체크 간격
        private float checkInterval = 0.05f;
        private float lastCheckTime = 0f;

        private Projectile projectile;

        void Start()
        {
            projectile = GetComponent<Projectile>();
        }

        void FixedUpdate()
        {
            if (processed || thrower == null) return;
            if (Time.time - lastCheckTime < checkInterval) return;
            lastCheckTime = Time.time;

            // 창 현재 위치 기준 2m 반경 내 몬스터 탐색
            CheckNearbyMonsters(transform.position);
        }

        /// <summary>
        /// 주변 몬스터 탐색 및 적중 처리
        /// </summary>
        private void CheckNearbyMonsters(Vector3 position)
        {
            try
            {
                var colliders = Physics.OverlapSphere(position, HIT_RADIUS);
                foreach (var col in colliders)
                {
                    if (col == null) continue;

                    Character target = col.GetComponent<Character>();
                    if (target == null)
                        target = col.GetComponentInParent<Character>();

                    if (target == null) continue;
                    if (target.IsPlayer()) continue;  // 플레이어 제외
                    if (hitTargets.Contains(target)) continue;  // 이미 적중한 대상 제외

                    // 적중 처리
                    ApplyHitToMonster(target, position);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[연공창] 주변 몬스터 탐색 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 몬스터에게 데미지 및 넉백 적용
        /// </summary>
        private void ApplyHitToMonster(Character target, Vector3 hitPosition)
        {
            try
            {
                hitTargets.Add(target);

                // 데미지 계산 (프로젝타일 데미지 사용)
                if (projectile != null && thrower != null)
                {
                    HitData hitData = new HitData();
                    hitData.m_damage = projectile.m_damage;
                    hitData.m_point = target.transform.position;
                    hitData.m_dir = (target.transform.position - thrower.transform.position).normalized;
                    hitData.m_attacker = thrower.GetZDOID();
                    hitData.m_skill = Skills.SkillType.Spears;

                    // 넉백 설정
                    hitData.m_pushForce = KNOCKBACK_FORCE;

                    // 데미지 적용
                    target.Damage(hitData);

                    SimpleVFX.Play("hit_01", target.transform.position + Vector3.up, 1.5f);
                    SimpleVFX.Play("flash_star_ellow_purple", target.transform.position + Vector3.up * 0.5f, 2f);
                    CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("", "fx_crit", target.transform.position, Quaternion.identity, 1f);
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[연공창] 몬스터 적중 처리 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 최종 적중 시 주변 몬스터 전체에 넉백 적용 (땅/벽/몬스터 적중 시)
        /// </summary>
        public void ApplyAreaKnockback(Vector3 hitPosition)
        {
            try
            {
                if (thrower == null) return;

                var colliders = Physics.OverlapSphere(hitPosition, HIT_RADIUS);
                int knockbackCount = 0;

                foreach (var col in colliders)
                {
                    if (col == null) continue;

                    Character target = col.GetComponent<Character>();
                    if (target == null)
                        target = col.GetComponentInParent<Character>();

                    if (target == null) continue;
                    if (target.IsPlayer()) continue;

                    // 이미 적중한 대상도 넉백은 적용 (추가 데미지 없이)
                    if (!hitTargets.Contains(target))
                    {
                        // 아직 데미지를 받지 않은 대상에게 데미지 + 넉백
                        ApplyHitToMonster(target, hitPosition);
                    }
                    else
                    {
                        // 이미 데미지를 받은 대상에게는 넉백만 추가
                        Vector3 knockbackDir = (target.transform.position - hitPosition).normalized;
                        if (knockbackDir.sqrMagnitude < 0.01f)
                            knockbackDir = Vector3.up;

                        // Stagger 적용 (넉백 효과)
                        target.Stagger(knockbackDir);
                    }
                    knockbackCount++;
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[연공창] 범위 넉백 오류: {ex.Message}");
            }
        }
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

                if (item != null && item.m_shared.m_skillType == Skills.SkillType.Spears)
                {
                    var clonedSpear = item.Clone();
                    clonedSpear.m_equipped = false;
                    SkillEffect.lastThrownSpear[player] = clonedSpear;
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

                SkillEffect.ConsumeSpearComboThrowUse(player);
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
    /// 땅/몬스터/환경 상관없이 무조건 회수 + 2m 반경 넉백
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

                // 적중 VFX (물 제외)
                if (!water)
                {
                    try
                    {
                        // 적중 지점 VFX
                        SimpleVFX.Play("hit_01", hitPoint, 1.5f);

                        // 직접 충돌한 대상 확인
                        Character directHitTarget = null;
                        if (collider != null)
                        {
                            directHitTarget = collider.GetComponent<Character>();
                            if (directHitTarget == null)
                                directHitTarget = collider.GetComponentInParent<Character>();
                        }

                        if (directHitTarget != null && !directHitTarget.IsPlayer())
                        {
                            SimpleVFX.Play("flash_star_ellow_purple", hitPoint, 2f);
                            CaptainSkillTree.VFX.VFXManager.PlayVFXMultiplayer("", "fx_crit", hitPoint, Quaternion.identity, 1f);
                        }

                        // ★ 핵심: 적중 지점 2m 반경 내 모든 몬스터에게 넉백 및 데미지 적용
                        tag.ApplyAreaKnockback(hitPoint);
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
                    return false;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[연공창 드롭 방지] 오류: {ex.Message}");
            }
            return true;
        }
    }
}

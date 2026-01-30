using EpicMMOSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using CaptainSkillTree;
using CaptainSkillTree.VFX;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 근접 무기 (단검, 검, 둔기, 창, 폴암) 패시브 스킬 시스템
    /// SkillEffect.cs에서 분리된 근접 무기 관련 기능들
    /// </summary>
    public static partial class SkillEffect
    {
        // === 근접 무기 상태 추적 변수들 ===
        
        // 단검 관련 상태 추적
        public static Dictionary<Player, int> consecutiveHits = new Dictionary<Player, int>();
        public static Dictionary<Player, float> lastHitTime = new Dictionary<Player, float>();
        public static Dictionary<Player, Coroutine> evasionBuffCoroutine = new Dictionary<Player, Coroutine>();
        public static Dictionary<Player, float> evasionBonus = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> stealthMovementBonus = new Dictionary<Player, bool>();
        
        // 단검 신규 스킬 상태 추적
        public static Dictionary<Player, float> knifeEvasionEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeMoveSpeedEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeDamageBonusEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeCritRateEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeLastRollTime = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> knifeAfterRoll = new Dictionary<Player, bool>();
        
        // 암살자의 심장 액티브 스킬 상태 추적
        public static Dictionary<Player, float> knifeAssassinHeartEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeAssassinHeartCooldownEndTime = new Dictionary<Player, float>();

        // 암살자의 심장 VFX 추적 - 사용 안함 (주석 처리)
        // public static Dictionary<Player, GameObject> knifeAssassinHeartStatusEffects = new Dictionary<Player, GameObject>();
        // private static GameObject cachedAssassinHeartStatusPrefab = null;

        // 검 관련 상태 추적
        public static Dictionary<Player, int> swordComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> swordLastHitTime = new Dictionary<Player, float>();
        public static Dictionary<Player, Coroutine> defenseBuffCoroutine = new Dictionary<Player, Coroutine>();
        public static Dictionary<Player, float> defenseBonus = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> nextAttackBoosted = new Dictionary<Player, bool>();
        public static Dictionary<Player, float> nextAttackMultiplier = new Dictionary<Player, float>();
        public static Dictionary<Player, float> nextAttackExpiry = new Dictionary<Player, float>();
        
        // 검 신규 스킬 상태 추적
        public static Dictionary<Player, float> swordCounterDefenseEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> swordBladeCounterEndTime = new Dictionary<Player, float>();

        // 창 관련 상태 변수 (새로운 스킬 적용)
        public static Dictionary<Player, int> spearComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> spearLastHitTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> spearThrowCooldown = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> spearAfterRoll = new Dictionary<Player, bool>();
        public static Dictionary<Player, float> spearRollTime = new Dictionary<Player, float>();
        public static Dictionary<Player, Coroutine> spearRollBuffCoroutine = new Dictionary<Player, Coroutine>();
        public static Dictionary<Player, bool> spearTripleComboActive = new Dictionary<Player, bool>();
        public static Dictionary<Player, bool> spearComboSequenceActive = new Dictionary<Player, bool>();

        // 창 전문가 콤보 상태 추적 (데미지 보너스용)
        public static Dictionary<Player, int> spearExpertComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> spearExpertLastHitTime = new Dictionary<Player, float>();


        // 반사 효과 관련 변수 (둔기 관련)
        public static float guardianSoulReflectionEndTime = 0f;
        // === 근접 스킬 이펙트 데이터 ===
        private static readonly Dictionary<string, SkillEffectData> meleeSkillEffects = new Dictionary<string, SkillEffectData>
        {
            // 단검 스킬들 (패시브는 VFX/SFX 제거)
            ["knife_expert_backstab"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["knife_step2_evasion"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["knife_step3_move_speed"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["knife_step4_attack_damage"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["knife_step5_crit_rate"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["knife_step6_combat_damage"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["knife_step7_execution"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거 (치명타 보너스)
            ["knife_step8_assassination"] = new SkillEffectData("fx_crit_large", "sfx_sword_hit", Color.magenta), // 액티브
            ["knife_step9_assassin_heart"] = new SkillEffectData("fx_guardstone_activate", "", new Color(1f, 0.2f, 0.2f)), // G키 액티브 - 사운드 제거
            
            // 검 스킬들 (Step 기반 ID 적용, 패시브는 VFX/SFX 제거)
            ["sword_expert"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["sword_Step1_fast_slash"] = new SkillEffectData("fx_sword_hit", "sfx_sword_swing", Color.cyan),
            ["sword_Step1_counter_stance"] = new SkillEffectData("fx_block", "sfx_blocked", Color.white),
            ["sword_Step2_combo_slash"] = new SkillEffectData("fx_crit_large", "sfx_sword_hit", Color.yellow),
            ["sword_Step3_blade_counter"] = new SkillEffectData("fx_guardstone_activate", "sfx_sword_hit", Color.red),
            ["sword_Step3_offense_defense"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", Color.green),
            ["sword_Step4_true_duel"] = new SkillEffectData("fx_dodge_attack", "sfx_sword_swing", Color.magenta),
            ["sword_Step5_defense_switch"] = new SkillEffectData("fx_block", "sfx_blocked", new Color(1f, 0.5f, 0f)),
            ["sword_Step6_ultimate_slash"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", Color.yellow),
            
            // G키 액티브 스킬: Sword Slash (빠른 연속공격)
            ["sword_slash"] = new SkillEffectData("fx_sword_hit", "sfx_sword_swing", Color.red),
            
            // 둔기 스킬들 (액티브 제외)
            ["mace_expert"] = new SkillEffectData("fx_mace_hit", "sfx_mace_swing", Color.gray),
            ["mace_crush"] = new SkillEffectData("fx_mace_crush", "sfx_mace_crush", Color.red),
            
            // 창 스킬들 (Step 기반 ID 적용, 패시브는 VFX/SFX 제거)
            ["spear_expert"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["spear_Step1_vital_strike"] = new SkillEffectData("fx_pierce", "sfx_spear_crit", Color.red),
            ["spear_Step2_dodge_strike"] = new SkillEffectData("fx_dodge_attack", "sfx_dodge", Color.green),
            ["spear_Step3_combo_strike"] = new SkillEffectData("fx_double_hit", "sfx_spear_double", new Color(1f, 0.5f, 0f)),
            
            // 폴암 스킬들 (MeleeSkillData.cs ID 기준, 패시브는 VFX/SFX 제거)
            ["polearm_expert"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["polearm_step1_spin"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["polearm_step1_suppress"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["polearm_step2_hero"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", Color.green), // 액티브 - 27% 넉백
            ["polearm_step3_area"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거 (2연속 공격)
            ["polearm_step3_ground"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["polearm_step4_moon"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["polearm_step4_charge"] = new SkillEffectData("", "", Color.clear), // 패시브 - VFX/SFX 제거
            ["polearm_step5_king"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", new Color(1f, 0.8f, 0f)), // G키 액티브
        };

        // === 근접 스킬 이름 매핑 ===
        private static readonly Dictionary<string, string> meleeSkillNames = new Dictionary<string, string>
        {
            // 단검 스킬 이름들
            ["knife_expert_backstab"] = "단검 전문가",
            ["knife_step2_evasion"] = "회피 숙련",
            ["knife_step3_move_speed"] = "빠른 움직임",
            ["knife_step4_attack_damage"] = "빠른 공격",
            ["knife_step5_crit_rate"] = "치명타 숙련",
            ["knife_step6_combat_damage"] = "피해로",
            ["knife_step7_execution"] = "암살자",
            ["knife_step8_assassination"] = "암살술",
            ["knife_step9_assassin_heart"] = "암살자의 심장",
            
            // 검 스킬 이름들 (Step 기반 ID 적용)
            ["sword_expert"] = "검 전문가",
            ["sword_Step1_fast_slash"] = "빠른 베기",
            ["sword_Step1_counter_stance"] = "반격 자세",
            ["sword_Step2_combo_slash"] = "연속베기",
            ["sword_Step3_blade_counter"] = "칼날 되치기",
            ["sword_Step3_offense_defense"] = "공방일체",
            ["sword_Step4_true_duel"] = "진검승부",
            ["sword_Step5_defense_switch"] = "방어 전환",
            ["sword_Step6_ultimate_slash"] = "궁극 베기",
            
            // G키 액티브 스킬: Sword Slash
            ["sword_slash"] = "검의 일섬",
            
            // 둔기 스킬 이름들
            ["mace_expert"] = "둔기 전문가",
            ["mace_crush"] = "분쇄 공격",
            
            // 창 스킬 이름들 (새로운 이름 적용)
            ["spear_expert"] = "창 전문가",
            ["spear_Step1_vital_strike"] = "급소 찌르기",
            ["spear_Step2_dodge_strike"] = "회피 찌르기",
            ["spear_Step3_combo_strike"] = "연격창",
            
            // 폴암 스킬 이름들 (MeleeSkillData.cs ID 기준)
            ["polearm_expert"] = "폴암 전문가",
            ["polearm_step1_spin"] = "회전베기",
            ["polearm_step1_suppress"] = "제압 공격",
            ["polearm_step2_hero"] = "영웅 타격",
            ["polearm_step3_area"] = "광역 강타",
            ["polearm_step3_ground"] = "지면 강타",
            ["polearm_step4_moon"] = "반달 베기",
            ["polearm_step4_charge"] = "폴암강화",
            ["polearm_step5_king"] = "장창의 제왕",
        };

        // === 근접 무기 확인 헬퍼 함수들 ===
        
        /// <summary>
        /// 플레이어가 단검을 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 Knives 스킬 타입
        /// 2순위: 프리팹 이름에 "Dagger", "dagger", "Knife", "knife" 포함
        /// 3순위: 무기 이름에 "단검", "dagger", "knife" 포함
        /// </summary>
        public static bool IsUsingDagger(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 Knives 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Knives)
            {
                Plugin.Log.LogDebug($"[단검 감지] Valheim 기본 Knives 스킬 타입: {weapon.m_shared.m_name}");
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Dagger") || prefabName.Contains("dagger") || 
                prefabName.Contains("Knife") || prefabName.Contains("knife"))
            {
                Plugin.Log.LogInfo($"[단검 감지] 프리팹 이름으로 단검 감지: {prefabName} ({weapon.m_shared.m_name})");
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("단검") || weaponName.Contains("dagger") || weaponName.Contains("knife"))
            {
                Plugin.Log.LogInfo($"[단검 감지] 무기 이름으로 단검 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                return true;
            }
            
            Plugin.Log.LogDebug($"[단검 감지] 단검이 아님: {weapon.m_shared.m_name} (스킬타입: {weapon.m_shared.m_skillType}, 프리팹: {prefabName})");
            return false;
        }

        /// <summary>
        /// 플레이어가 검을 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 Swords 스킬 타입
        /// 2순위: 프리팹 이름에 "Sword", "sword", "Blade", "blade" 포함
        /// 3순위: 무기 이름에 "검", "sword", "blade" 포함
        /// </summary>
        public static bool IsUsingSword(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 Swords 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
            {
                Plugin.Log.LogDebug($"[검 감지] Valheim 기본 Swords 스킬 타입: {weapon.m_shared.m_name}");
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Sword") || prefabName.Contains("sword") || 
                prefabName.Contains("Blade") || prefabName.Contains("blade"))
            {
                Plugin.Log.LogInfo($"[검 감지] 프리팹 이름으로 검 감지: {prefabName} ({weapon.m_shared.m_name})");
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("검") || weaponName.Contains("sword") || weaponName.Contains("blade"))
            {
                Plugin.Log.LogInfo($"[검 감지] 무기 이름으로 검 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                return true;
            }
            
            Plugin.Log.LogDebug($"[검 감지] 검이 아님: {weapon.m_shared.m_name} (스킬타입: {weapon.m_shared.m_skillType}, 프리팹: {prefabName})");
            return false;
        }

        /// <summary>
        /// 플레이어가 양손검을 사용 중인지 확인
        /// </summary>
        public static bool IsUsingTwoHandedSword(Player player)
        {
            if (!IsUsingSword(player)) return false;
            var weapon = player.GetCurrentWeapon();
            return weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon;
        }

        /// <summary>
        /// 플레이어가 둔기를 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 Clubs 스킬 타입
        /// 2순위: 프리팹 이름에 "Mace", "mace", "Club", "club", "Hammer", "hammer" 포함
        /// 3순위: 무기 이름에 "둔기", "mace", "club", "hammer" 포함
        /// </summary>
        public static bool IsUsingMace(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 Clubs 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Clubs)
            {
                Plugin.Log.LogDebug($"[둔기 감지] Valheim 기본 Clubs 스킬 타입: {weapon.m_shared.m_name}");
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Mace") || prefabName.Contains("mace") || 
                prefabName.Contains("Club") || prefabName.Contains("club") ||
                prefabName.Contains("Hammer") || prefabName.Contains("hammer"))
            {
                Plugin.Log.LogInfo($"[둔기 감지] 프리팹 이름으로 둔기 감지: {prefabName} ({weapon.m_shared.m_name})");
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("둔기") || weaponName.Contains("mace") || 
                weaponName.Contains("club") || weaponName.Contains("hammer"))
            {
                Plugin.Log.LogInfo($"[둔기 감지] 무기 이름으로 둔기 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                return true;
            }
            
            Plugin.Log.LogDebug($"[둔기 감지] 둔기가 아님: {weapon.m_shared.m_name} (스킬타입: {weapon.m_shared.m_skillType}, 프리팹: {prefabName})");
            return false;
        }

        /// <summary>
        /// 플레이어가 한손둔기를 사용 중인지 확인
        /// </summary>
        public static bool IsUsingOneHandedMace(Player player)
        {
            if (!IsUsingMace(player)) return false;
            var weapon = player.GetCurrentWeapon();
            return weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon;
        }

        /// <summary>
        /// 플레이어가 양손둔기를 사용 중인지 확인
        /// </summary>
        public static bool IsUsingTwoHandedMace(Player player)
        {
            if (!IsUsingMace(player)) return false;
            var weapon = player.GetCurrentWeapon();
            return weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon;
        }

        /// <summary>
        /// 플레이어가 창을 사용 중인지 확인 (확장성 고려 - 다른 모드 지원)
        /// 1순위: Valheim 기본 Spears 스킬 타입
        /// 2순위: 프리팹 이름에 "Spear", "spear", "Lance", "lance", "Pike", "pike" 포함
        /// 3순위: 무기 이름에 "창", "spear", "lance", "pike" 포함
        /// </summary>
        public static bool IsUsingSpear(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 1순위: Valheim 기본 Spears 스킬 타입 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Spears)
            {
                Plugin.Log.LogDebug($"[창 감지] Valheim 기본 Spears 스킬 타입: {weapon.m_shared.m_name}");
                return true;
            }
            
            // 2순위: 프리팹 이름 확인 (다른 모드 지원)
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Spear") || prefabName.Contains("spear") || 
                prefabName.Contains("Lance") || prefabName.Contains("lance") ||
                prefabName.Contains("Pike") || prefabName.Contains("pike"))
            {
                Plugin.Log.LogInfo($"[창 감지] 프리팹 이름으로 창 감지: {prefabName} ({weapon.m_shared.m_name})");
                return true;
            }
            
            // 3순위: 무기 이름 확인 (지역화 및 커스텀 이름 지원)
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("창") || weaponName.Contains("spear") || 
                weaponName.Contains("lance") || weaponName.Contains("pike"))
            {
                Plugin.Log.LogInfo($"[창 감지] 무기 이름으로 창 감지: {weapon.m_shared.m_name} (프리팹: {prefabName})");
                return true;
            }
            
            Plugin.Log.LogDebug($"[창 감지] 창이 아님: {weapon.m_shared.m_name} (스킬타입: {weapon.m_shared.m_skillType}, 프리팹: {prefabName})");
            return false;
        }

        /// <summary>
        /// 플레이어가 폴암을 사용 중인지 확인
        /// </summary>
        public static bool IsUsingPolearm(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();
            
            // 폴암은 주로 Polearms 스킬 타입이거나 이름으로 확인
            if (weapon.m_shared.m_skillType == Skills.SkillType.Polearms)
            {
                return true;
            }
            
            // 이름으로 폴암 확인
            string weaponName = weapon.m_shared?.m_name?.ToLower() ?? "";
            string prefabName = weapon.m_dropPrefab?.name?.ToLower() ?? "";
            
            return weaponName.Contains("atgeir") || weaponName.Contains("halberd") || 
                   weaponName.Contains("polearm") || weaponName.Contains("glaive") ||
                   prefabName.Contains("atgeir") || prefabName.Contains("halberd") || 
                   prefabName.Contains("polearm") || prefabName.Contains("glaive");
        }

        /// <summary>
        /// 플레이어가 방패를 착용 중인지 확인
        /// </summary>
        public static bool HasShield(Player player)
        {
            if (player == null) return false;
            
            // 인벤토리에서 방패 확인 (더 안전한 방법)
            var inventory = player.GetInventory();
            if (inventory == null) return false;
            
            // 장착된 아이템들 중에서 방패 찾기
            var equippedItems = inventory.GetEquippedItems();
            foreach (var item in equippedItems)
            {
                if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                {
                    return true;
                }
            }
            
            return false;
        }

        // === 근접 스킬 특수 효과 시스템 ===
        
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
                new Color(0.8f, 1f, 0.8f, 1f)); // 연한 초록새 (빠른 움직임)
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
        /// 치명타 확률 보너스는 MMO getParameter 패치에서 처리
        /// </summary>
        public static void CheckKnifeExecutionPassive(Player player, HitData hit, bool isCritical)
        {
            if (!HasSkill("knife_step7_execution") || !Knife_Skill.IsUsingDagger(player)) return;

            Knife_Skill.ApplyKnifeExecutionPassive(player, ref hit, isCritical);
        }

        /* ===== 은신 치명타 효과 제거됨 =====
        /// <summary>
        /// 암살술 - 은신 상태에서 치명타 효과 (사용 안함 - 비틀거림만 유지)
        /// </summary>
        public static void CheckKnifeAssassination(Player player, HitData hit)
        {
            if (!HasSkill("knife_step8_assassination") || !IsUsingDagger(player)) return;

            // 은신 상태 확인 (스니크 상태)
            if (player.IsCrouching() && !player.IsAttached())
            {
                float critMultiplier = Knife_Config.KnifeAssassinationCritMultiplierValue;
                hit.m_damage.m_slash *= critMultiplier;
                hit.m_damage.m_pierce *= critMultiplier;

                // 액티브 스킬: VFX/SFX 사용
                PlaySkillEffect(player, "knife_step8_assassination", hit.m_point);
                DrawFloatingText(player, $"🗡️ 암살술! 치명타 x{critMultiplier}!", Color.magenta);
                Plugin.Log.LogInfo($"[암살술] 은신 상태에서 치명타 x{critMultiplier} 발동!");
            }
        }
        ===== 은신 치명타 효과 제거 끝 ===== */

        /* ===== VFX 사용 안함 - 주석 처리됨 =====
        /// <summary>
        /// 암살자의 심장 상태 효과(statusailment_01_aura) 재생 - 사용 안함
        /// </summary>
        private static void PlayAssassinHeartStatusEffect(Player player, float duration)
        {
            try
            {
                // 캐시된 프리팹이 없으면 한 번만 로드
                if (cachedAssassinHeartStatusPrefab == null)
                {
                    cachedAssassinHeartStatusPrefab = VFXManager.GetVFXPrefab("statusailment_01_aura");
                    if (cachedAssassinHeartStatusPrefab != null)
                    {
                        Plugin.Log.LogInfo("[암살자의 심장] statusailment_01_aura 프리팹 캐시됨");
                    }
                    else
                    {
                        Plugin.Log.LogWarning("[암살자의 심장] VFXManager에서 statusailment_01_aura를 찾을 수 없음");
                        return;
                    }
                }

                // 기존 상태 효과가 있으면 제거
                if (knifeAssassinHeartStatusEffects.ContainsKey(player) && knifeAssassinHeartStatusEffects[player] != null)
                {
                    UnityEngine.Object.Destroy(knifeAssassinHeartStatusEffects[player]);
                    knifeAssassinHeartStatusEffects.Remove(player);
                    Plugin.Log.LogInfo("[암살자의 심장] 기존 상태 효과 제거");
                }

                // statusailment_01_aura 효과 생성 (머리 위에서 duration 지속)
                if (cachedAssassinHeartStatusPrefab != null)
                {
                    // 머리 위 위치 계산 (1.4m 위)
                    var headPosition = player.transform.position + Vector3.up * 1.4f;
                    var statusInstance = UnityEngine.Object.Instantiate(cachedAssassinHeartStatusPrefab, headPosition, Quaternion.identity);

                    // 플레이어를 따라다니도록 부모 설정
                    statusInstance.transform.SetParent(player.transform, false);
                    statusInstance.transform.localPosition = Vector3.up * 1.4f; // 머리 위 고정

                    // 크기 조정 (1배)
                    statusInstance.transform.localScale = Vector3.one * 1f;

                    // 상태 효과 인스턴스 저장
                    knifeAssassinHeartStatusEffects[player] = statusInstance;

                    // duration 후 자동 제거
                    UnityEngine.Object.Destroy(statusInstance, duration);

                    Plugin.Log.LogInfo($"[암살자의 심장] statusailment_01_aura 상태 효과 재생 완료 (머리 위 1.4m, {duration}초 지속)");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[암살자의 심장] 상태 효과 재생 실패: {ex.Message}");
            }
        }
        ===== VFX 사용 안함 - 주석 처리 끝 ===== */

        /// <summary>
        /// 플레이어 정면의 몬스터 탐색 (시야각 45도 이내)
        /// </summary>
        private static Character FindFrontMonster(Player player, float range, float maxAngle = 45f)
        {
            if (player == null) return null;

            Vector3 playerPos = player.transform.position;
            Vector3 playerForward = player.transform.forward;

            // 정면 시야각 내의 몬스터만 필터링, 거리순 정렬
            var frontMonster = Character.GetAllCharacters()
                .Where(c => c != null && !c.IsDead() && c != player && !c.IsPlayer())
                .Where(c => c.GetFaction() != Character.Faction.Players)
                .Where(c => Vector3.Distance(playerPos, c.transform.position) <= range)
                .Where(c =>
                {
                    // 플레이어 → 몬스터 방향 벡터
                    Vector3 dirToMonster = (c.transform.position - playerPos).normalized;
                    // 플레이어 정면과의 각도 계산
                    float angle = Vector3.Angle(playerForward, dirToMonster);
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
                // 안전성 검사 (DoubleJump.cs 패턴)
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

                // 시작점 VFX (먼저 재생)
                SimpleVFX.Play("vfx_spawn_small", originalPosition, 1.5f);

                // Rigidbody로 빠른 돌격 실행
                var body = HarmonyLib.Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();
                if (body != null)
                {
                    // 물리 속도 초기화 후 위치 직접 설정
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
            catch (System.Exception ex)
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

            // === 정면 몬스터 탐색 (스태미나/쿨타임 적용 전) ===
            float teleportRange = Knife_Config.KnifeAssassinHeartTeleportRangeValue;
            Character targetMonster = FindFrontMonster(player, teleportRange);

            // 정면에 몬스터가 없으면 스킬 취소 (스태미나/쿨타임 미적용)
            if (targetMonster == null)
            {
                DrawFloatingText(player, "정면에 적 없음!", Color.yellow);
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

            // === 순간이동 실행 ===
            float behindDistance = Knife_Config.KnifeAssassinHeartTeleportBehindValue;
            bool teleportSuccess = TeleportBehindTarget(player, targetMonster, behindDistance);

            if (!teleportSuccess)
            {
                DrawFloatingText(player, "순간이동 실패!", Color.red);
                Plugin.Log.LogWarning("[암살자의 심장] 순간이동 실패 - 스킬 취소");
                return false;
            }

            // 스태미나 소모 (순간이동 성공 후)
            player.UseStamina(staminaCost);

            // 버프 활성화
            float duration = Knife_Config.KnifeAssassinHeartDurationValue;
            knifeAssassinHeartEndTime[player] = currentTime + duration;

            // 쿨타임 설정
            float cooldown = Knife_Config.KnifeAssassinHeartCooldownValue;
            knifeAssassinHeartCooldownEndTime[player] = currentTime + cooldown;

            // 액티브 스킬: VFX/SFX 사용 (순간이동 후 위치에서)
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
                // 공격력 보너스 적용 (40% 기본값)
                float damageBonus = Knife_Config.KnifeAssassinHeartDamageBonusValue / 100f;
                hit.m_damage.m_slash *= (1f + damageBonus);
                hit.m_damage.m_pierce *= (1f + damageBonus);
                
                // 치명타 확률 체크 (12% 기본값)
                float critChance = Knife_Config.KnifeAssassinHeartCritChanceValue / 100f;
                if (UnityEngine.Random.Range(0f, 1f) <= critChance)
                {
                    // 치명타 피해 적용 (30% 추가 피해)
                    float critDamageBonus = Knife_Config.KnifeAssassinHeartCritDamageValue / 100f;
                    hit.m_damage.m_slash *= (1f + critDamageBonus);
                    hit.m_damage.m_pierce *= (1f + critDamageBonus);
                    
                    // 치명타 시각 효과
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
        /// 검 연계 공격 카운트 업데이트
        /// </summary>
        public static void UpdateSwordCombo(Player player)
        {
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            float now = Time.time;
            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 3f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;
            
            // 3연타 달성 시 다음 공격 부스트
            if (swordComboCount[player] >= 3)
            {
                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1.5f;
                nextAttackExpiry[player] = now + 5f;
                PlaySkillEffect(player, "sword_combo");
                DrawFloatingText(player, "⚔️ 검술 연계 3단!");
            }
        }

        /// <summary>
        /// 창 롤 후 공격 보너스 체크
        /// </summary>
        public static void CheckSpearRollAttack(Player player)
        {
            if (!spearAfterRoll.ContainsKey(player)) return;
            
            float now = Time.time;
            if (spearAfterRoll[player] && now - spearRollTime[player] < 2f)
            {
                // 롤 후 2초 내 공격 시 보너스 데미지
                spearAfterRoll[player] = false;
                PlaySkillEffect(player, "spear_evasion");
                DrawFloatingText(player, "🎯 회피 후 반격!");
            }
        }

        // === 근접 스킬 데이터 접근자 ===
        
        /// <summary>
        /// 근접 스킬 이펙트 데이터 가져오기
        /// </summary>
        public static SkillEffectData? GetMeleeSkillEffect(string skillId)
        {
            return meleeSkillEffects.TryGetValue(skillId, out var effect) ? effect : (SkillEffectData?)null;
        }

        /// <summary>
        /// 근접 스킬 이름 가져오기
        /// </summary>
        public static string GetMeleeSkillName(string skillId)
        {
            return meleeSkillNames.TryGetValue(skillId, out var name) ? name : skillId;
        }

        /// <summary>
        /// 모든 근접 스킬 이펙트를 메인 딕셔너리에 추가
        /// </summary>
        public static void RegisterMeleeSkillEffects(Dictionary<string, SkillEffectData> mainEffects)
        {
            foreach (var kvp in meleeSkillEffects)
            {
                mainEffects[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// 모든 근접 스킬 이름을 메인 딕셔너리에 추가
        /// </summary>
        public static void RegisterMeleeSkillNames(Dictionary<string, string> mainNames)
        {
            foreach (var kvp in meleeSkillNames)
            {
                mainNames[kvp.Key] = kvp.Value;
            }
        }
        
        // === 창 전문가 MMO 연동 시스템 ===
        
        /// <summary>
        /// 창 전문가 2연속 공격 체크 (MMO 연동 방식)
        /// </summary>
        public static void CheckSpearExpertCombo(Player player)
        {
            if (!HasSkill("spear_expert")) return;
            
            float now = Time.time;
            if (!spearExpertComboCount.ContainsKey(player))
                spearExpertComboCount[player] = 0;

            // 3초 내 연속 공격 체크
            if (spearExpertLastHitTime.ContainsKey(player) && now - spearExpertLastHitTime[player] < 3f)
            {
                spearExpertComboCount[player]++;
            }
            else
            {
                spearExpertComboCount[player] = 1;
            }
            spearExpertLastHitTime[player] = now;
        }

        // === 검 전문가 스킬 메서드들 (누락된 부분 구현) ===
        
        /// <summary>
        /// 검 전문가 2연속 공격 체크 (sword_expert)
        /// </summary>
        public static void CheckSwordExpertCombo(Player player)
        {
            if (!HasSkill("sword_expert")) return;
            
            float now = Time.time;
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            // 3초 내 연속 공격 체크
            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 3f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;
            
            // 2연속 공격 달성 시 +7% 데미지 보너스 적용
            if (swordComboCount[player] >= 2)
            {
                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                DrawFloatingText(player, $"⚔️ 검 전문가 2연속! (+{SkillTreeConfig.SwordStep1ExpertComboBonusValue}%)");
                
                Plugin.Log.LogInfo($"[검 전문가] 2연속 공격 달성 - 공격력 +{SkillTreeConfig.SwordStep1ExpertComboBonusValue}% 보너스 적용");
                
                // 다음 공격에 보너스 적용 설정 (설정값 사용)
                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1f + (SkillTreeConfig.SwordStep1ExpertComboBonusValue / 100f);
                nextAttackExpiry[player] = now + SkillTreeConfig.SwordStep1ExpertDurationValue;
            }
        }
        
        /// <summary>
        /// 연속베기 3연속 공격 체크 (sword_step2_combo)
        /// </summary>
        public static void CheckSwordComboSlash(Player player)
        {
            if (!HasSkill("sword_step2_combo")) return;
            
            float now = Time.time;
            if (!swordComboCount.ContainsKey(player))
                swordComboCount[player] = 0;

            // 3초 내 연속 공격 체크
            if (swordLastHitTime.ContainsKey(player) && now - swordLastHitTime[player] < 3f)
            {
                swordComboCount[player]++;
            }
            else
            {
                swordComboCount[player] = 1;
            }
            swordLastHitTime[player] = now;
            
            // 3연속 공격 달성 시 +13% 데미지 보너스 적용
            if (swordComboCount[player] >= 3)
            {
                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                DrawFloatingText(player, $"⚔️ 연속베기! 3연속 공격력 +{SkillTreeConfig.SwordStep2ComboSlashBonusValue}%");
                
                Plugin.Log.LogInfo("[연속베기] 3연속 공격 달성 - 공격력 +13% 보너스 적용");
                
                // 다음 공격에 보너스 적용 설정 (설정값 사용)
                nextAttackBoosted[player] = true;
                nextAttackMultiplier[player] = 1f + (SkillTreeConfig.SwordStep2ComboSlashBonusValue / 100f);
                nextAttackExpiry[player] = now + SkillTreeConfig.SwordStep2ComboSlashDurationValue;
                
                // 콤보 카운트 리셋
                swordComboCount[player] = 0;
            }
        }
        
        /// <summary>
        /// 삼연창 3연속 공격 체크
        /// </summary>
        public static void CheckSpearTripleCombo(Player player)
        {
            if (!HasSkill("spear_Step4_triple")) return;
            
            float now = Time.time;
            if (!spearComboCount.ContainsKey(player))
                spearComboCount[player] = 0;

            // 3초 내 연속 공격 체크
            if (spearLastHitTime.ContainsKey(player) && now - spearLastHitTime[player] < 3f)
            {
                spearComboCount[player]++;
            }
            else
            {
                spearComboCount[player] = 1;
            }
            spearLastHitTime[player] = now;
            
            // 3연속 공격 달성 시 효과 활성화
            if (spearComboCount[player] >= 3)
            {
                spearTripleComboActive[player] = true;
                spearComboCount[player] = 0; // 리셋
                
                Plugin.Log.LogInfo("[삼연창] 3연속 공격 달성 - 다음 공격에 보너스 적용");
            }
        }
        
        /// <summary>
        /// 연격창 무기 공격력 +4 패시브 보너스 적용
        /// </summary>
        public static void CheckDoubleAttack(Player player, Character target, HitData originalHit)
        {
            if (!HasSkill("spear_Step3_pierce")) return;

            // 무기 공격력 +4 적용 (고정 수치 덧셈, pierce만)
            float bonusValue = SkillTreeConfig.SpearStep3PierceDamageBonusValue;

            originalHit.m_damage.m_pierce += bonusValue;

            Plugin.Log.LogDebug($"[연격창] 무기 공격력 보너스 적용 (pierce): +{SkillTreeConfig.SpearStep3PierceDamageBonusValue}");
        }
        
        // ExecuteDelayedDoubleAttack 메서드 제거 (즉시 공격력 증가로 변경됨)
        
        /// <summary>
        /// 투창 전문가 공격력 보너스 적용
        /// </summary>
        public static void ApplySpearThrowExpertDamage(HitData hit)
        {
            if (!HasSkill("spear_Step1_throw")) return;
            
            // 투창 전문가 투창 공격력 +30% 적용 (pierce만)
            float bonusPercent = 0.3f; // 30% (고정값, MMO 스탯 연동을 통해 처리)
            hit.m_damage.m_pierce *= (1f + bonusPercent);

            Plugin.Log.LogDebug("[투창 전문가] 투창 공격력 +30% 적용 (pierce)");
        }
        
        /// <summary>
        /// 꿰뚫는 창 치명타 확률 적용
        /// </summary>
        public static void ApplySpearPenetrateCritical(Player player, HitData hit)
        {
            if (!HasSkill("spear_Step5_penetrate")) return;
            
            // 치명타 확률 12% 체크
            float critChance = 0.12f; // 12%
            if (UnityEngine.Random.Range(0f, 1f) <= critChance)
            {
                // 치명타 발생시 데미지 2배 (pierce만)
                hit.m_damage.m_pierce *= 2f;

                // 패시브 스킬: 텍스트만 표시
                DrawFloatingText(player, "💥 꿰뚫는 창 치명타!", Color.red);
                
                Plugin.Log.LogDebug("[꿰뚫는 창] 치명타 발생! (12% 확률)");
            }
        }

        // === 검 전문가 신규 스킬들 ===

        /// <summary>
        /// 반격 자세 - 패링 성공 후 방어력 증가 효과 (sword_step1_counter)
        /// </summary>
        public static void ApplySwordCounterDefense(Player player)
        {
            if (!HasSkill("sword_step1_counter")) return;
            
            float duration = SkillTreeConfig.SwordStep1CounterDurationValue;
            float now = Time.time;
            
            // 방어력 증가 상태 설정
            swordCounterDefenseEndTime[player] = now + duration;
            
            // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
            DrawFloatingText(player, $"🛡️ 반격 자세! ({duration}초간 방어력 +{SkillTreeConfig.SwordStep1CounterDefenseBonusValue}%)");
            
            Plugin.Log.LogInfo($"[반격 자세] 패링 성공 - {duration}초간 방어력 +{SkillTreeConfig.SwordStep1CounterDefenseBonusValue}% 적용");
        }
        
        /// <summary>
        /// 칼날 되치기 - 패링 성공 시 공격력 증가 효과 (sword_step3_riposte)
        /// </summary>
        public static void ApplySwordBladeCounter(Player player)
        {
            if (!HasSkill("sword_step3_riposte")) return;
            
            float duration = SkillTreeConfig.SwordStep3BladeCounterDurationValue;
            float now = Time.time;
            
            // 공격력 증가 상태 설정
            swordBladeCounterEndTime[player] = now + duration;
            
            // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
            DrawFloatingText(player, $"⚔️ 칼날 되치기! ({duration}초간 공격력 +{SkillTreeConfig.SwordStep3BladeCounterBonusValue}%)");
            
            Plugin.Log.LogInfo($"[칼날 되치기] 패링 성공 - {duration}초간 공격력 +{SkillTreeConfig.SwordStep3BladeCounterBonusValue}% 적용");
        }
        
        /// <summary>
        /// 공방일체 - 양손 무기 착용 시 공격력/방어력 보너스 (sword_step3_allinone)
        /// </summary>
        public static void ApplySwordOffenseDefense(Player player, HitData hit)
        {
            if (!HasSkill("sword_step3_allinone")) return;
            
            // 양손 무기 착용 여부 확인
            var currentWeapon = player.GetCurrentWeapon();
            if (currentWeapon != null && currentWeapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
            {
                // MMO 시스템을 통한 스탯 보너스는 getParameter 패치에서 처리됨
                // 패시브 스킬: 텍스트 표시만 (VFX/SFX 금지)
                DrawFloatingText(player, $"⚔️🛡️ 공방일체! (공격력 +{SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue}%, 방어력 +{SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue})");
                
                Plugin.Log.LogDebug($"[공방일체] 양손 무기 착용 - 공격력 +{SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue}%, 방어력 +{SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue} 적용");
            }
        }

        /// <summary>
        /// 방어 전환 - 방패 착용/미착용에 따른 스탯 조정 (sword_step5_defswitch)
        /// </summary>
        public static void ApplySwordDefenseSwitch(Player player, HitData hit)
        {
            if (!HasSkill("sword_step5_defswitch")) return;
            
            // 방패 착용 여부 확인 - Player 인벤토리에서 직접 확인
            var inventory = player.GetInventory();
            bool hasShield = false;
            if (inventory != null)
            {
                var items = inventory.GetEquippedItems();
                hasShield = items.Any(item => item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield);
            }
            
            if (hasShield)
            {
                // 방패 착용 시: 방어력 +15%, 공격력 -5%
                DrawFloatingText(player, "🛡️ 방어 전환 (방패): 방어력 +15%, 공격력 -5%");
                Plugin.Log.LogDebug("[방어 전환] 방패 착용 모드 - 방어력 +15%, 공격력 -5%");
            }
            else
            {
                // 방패 미착용 시: 공격력 +20%, 방어력 -10%
                DrawFloatingText(player, "⚔️ 방어 전환 (공격): 공격력 +20%, 방어력 -10%");
                Plugin.Log.LogDebug("[방어 전환] 공격 모드 - 공격력 +20%, 방어력 -10%");
            }
        }
        
        /// <summary>
        /// Sword Slash 액티브 스킬 데미지 보너스 적용 (sword_step5_finalcut)
        /// </summary>
        public static void ApplySwordSlashDamageBonus(Player player, Character target, HitData hit)
        {
            if (!HasSkill("sword_step5_finalcut") && !HasSkill("sword_slash")) return;
            if (!Sword_Skill.IsSwordSlashActive(player)) return;
            
            // Sword Slash 활성화 중 데미지 보너스 적용
            float damageRatio = Sword_Config.SwordSlashDamageRatioValue / 100f;
            
            // 모든 데미지 타입에 80% 적용
            hit.m_damage.m_blunt *= damageRatio;
            hit.m_damage.m_slash *= damageRatio;
            hit.m_damage.m_pierce *= damageRatio;
            hit.m_damage.m_chop *= damageRatio;
            hit.m_damage.m_pickaxe *= damageRatio;
            hit.m_damage.m_fire *= damageRatio;
            hit.m_damage.m_frost *= damageRatio;
            hit.m_damage.m_lightning *= damageRatio;
            hit.m_damage.m_poison *= damageRatio;
            hit.m_damage.m_spirit *= damageRatio;
            
            // 액티브 스킬: 풍부한 VFX/SFX 적용
            PlaySkillEffect(player, "sword_slash", target.transform.position);
            DrawFloatingText(player, $"⚔️ Sword Slash! ({damageRatio*100:F0}%)", Color.red);
            
            Plugin.Log.LogInfo($"[Sword Slash] 데미지 보너스 적용 - {damageRatio*100:F0}%");
        }
    }

    // === 근접 스킬 Harmony 패치들 ===
    
    /// <summary>
    /// 단검 공격 시 연속 공격 및 백스탭 효과
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MeleeSkills_Dagger_Attack_Patch
    {
        static bool Prepare() 
        {
            Plugin.Log.LogDebug("[안전 장치] 단검 공격 패치 준비 완료");
            return true;
        }
        
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingDagger(player)) return;

                // 연속 공격 카운트 업데이트
                SkillEffect.UpdateConsecutiveHits(player);

                // 백스탭 확인
                bool isBackstab = SkillEffect.IsBackstab(player, __instance);
                
                // 단검 전문가 - 백스탭 공격 시 데미지 보너스
                SkillEffect.CheckKnifeExpertBackstab(player, __instance, hit);
                
                // 회피 숙련 - 구르기 후 회피율 증가 (패시브 효과)
                // 빠른 움직임 - 이동속도 증가 (패시브 효과)
                // 빠른 공격 - 공격속도 증가 (패시브 효과)
                // 치명타 숙련 - 치명타 확률 증가 (패시브 효과)
                
                // 전투 숙련 - 전투 중 공격력 증가
                SkillEffect.CheckKnifeCombatDamage(player, hit);

                // 암살자 - 패시브 치명타 확률 및 치명타 피해 증가 (MMO getParameter 패치에서 처리)
                // CheckKnifeExecutionPassive는 치명타 발생 시 호출됨

                // 암살술 - 은신 치명타 효과 제거됨 (비틀거림만 유지)
                // SkillEffect.CheckKnifeAssassination(player, hit);
                
                // 암살자의 심장 - G키 액티브 스킬 치명타 효과
                SkillEffect.ApplyKnifeAssassinHeartCrit(player, hit);

                // 백스탭 마스터 (뒤에서 공격 시 +50% 데미지)
                if (SkillEffect.HasSkill("knife_expert_backstab") && isBackstab)
                {
                    hit.m_damage.m_slash *= 1.5f;
                    hit.m_damage.m_pierce *= 1.5f;
                    SkillEffect.PlaySkillEffect(player, "knife_master", hit.m_point);
                    SkillEffect.DrawFloatingText(player, "🗡️ 백스탭!");
                }

                // 연속 공격 치명타 (3회 연속 공격 시)
                var consecutiveHits = SkillEffect.consecutiveHits.TryGetValue(player, out var hits) ? hits : 0;
                if (consecutiveHits >= 3)
                {
                    if (SkillEffect.HasSkill("knife_step5_crit_rate"))
                    {
                        hit.m_damage.m_slash *= 1.3f;
                        hit.m_damage.m_pierce *= 1.3f;
                        SkillEffect.PlaySkillEffect(player, "knife_crit1", hit.m_point);
                        SkillEffect.DrawFloatingText(player, "⚡ 연속 공격!");
                    }
                }

                // 치명적 일격 (5회 연속 공격 시)
                if (consecutiveHits >= 5 && SkillEffect.HasSkill("knife_step8_assassination"))
                {
                    hit.m_damage.m_slash *= 2.0f;
                    hit.m_damage.m_pierce *= 2.0f;
                    SkillEffect.PlaySkillEffect(player, "knife_crit2", hit.m_point);
                    SkillEffect.DrawFloatingText(player, "💀 치명적 일격!");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 단검 공격 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 검 공격 시 연계 및 방어 효과
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MeleeSkills_Sword_Attack_Patch
    {
        static bool Prepare() 
        {
            Plugin.Log.LogDebug("[안전 장치] 검 공격 패치 준비 완료");
            return true;
        }
        
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingSword(player)) return;

                // 검술 연계 업데이트
                SkillEffect.UpdateSwordCombo(player);
                
                // 검 전문가 2연속 공격 체크
                SkillEffect.CheckSwordExpertCombo(player);
                
                // 연속베기 3연속 공격 체크  
                SkillEffect.CheckSwordComboSlash(player);

                // 다음 공격 부스트 적용
                if (SkillEffect.nextAttackBoosted.TryGetValue(player, out var boosted) && boosted)
                {
                    if (Time.time < SkillEffect.nextAttackExpiry[player])
                    {
                        float multiplier = SkillEffect.nextAttackMultiplier[player];
                        hit.m_damage.m_slash *= multiplier;
                        hit.m_damage.m_pierce *= multiplier;
                        SkillEffect.nextAttackBoosted[player] = false;
                        SkillEffect.PlaySkillEffect(player, "sword_power", hit.m_point);
                        SkillEffect.DrawFloatingText(player, $"⚔️ 강화된 일격! (+{(multiplier - 1) * 100:F0}%)");
                    }
                    else
                    {
                        SkillEffect.nextAttackBoosted[player] = false;
                    }
                }

                // 양손검 추가 데미지
                if (SkillEffect.IsUsingTwoHandedSword(player) && SkillEffect.HasSkill("sword_power"))
                {
                    hit.m_damage.m_slash *= 1.2f;
                    hit.m_damage.m_pierce *= 1.2f;
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 검 공격 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 창 공격 시 특수 효과들
    /// </summary>
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class MeleeSkills_Spear_Attack_Patch
    {
        static bool Prepare() 
        {
            Plugin.Log.LogDebug("[안전 장치] 창 공격 패치 준비 완료");
            return true;
        }
        
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Character __instance, HitData hit)
        {
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingSpear(player)) return;

                // 창 전문가 2연속 공격 체크 (MMO 연동 방식)
                SkillEffect.CheckSpearExpertCombo(player);
                
                // 창 전문가 공격력 보너스 적용 (2연속 공격 달성 시, pierce만)
                if (SkillEffect.spearExpertComboCount.TryGetValue(player, out int comboCount) && comboCount >= 2)
                {
                    float bonusPercent = SkillTreeConfig.SpearStep1DamageBonusValue / 100f;
                    hit.m_damage.m_pierce *= (1f + bonusPercent);
                    SkillEffect.DrawFloatingText(player, $"🔥 창 전문가 공격력! (+{SkillTreeConfig.SpearStep1DamageBonusValue}%)");
                }
                
                // 회피 찌르기 체크 (구르기 직후 공격)
                bool isAfterRoll = SkillEffect.spearAfterRoll.TryGetValue(player, out bool afterRoll) && afterRoll &&
                                  Time.time - SkillEffect.spearRollTime[player] < 2f;

                // 급소 찌르기 (창 공격력 +20%, pierce만)
                if (SkillEffect.HasSkill("spear_Step1_crit"))
                {
                    float bonusPercent = SkillTreeConfig.SpearStep2CritDamageBonusValue / 100f;
                    hit.m_damage.m_pierce *= (1f + bonusPercent);
                    SkillEffect.PlaySkillEffect(player, "spear_Step1_crit", hit.m_point);
                }

                // 회피 찌르기 (구르기 직후 공격 시 +25%, pierce만)
                if (isAfterRoll && SkillEffect.HasSkill("spear_Step2_evasion"))
                {
                    float bonusPercent = SkillTreeConfig.SpearStep3EvasionDamageBonusValue / 100f;
                    hit.m_damage.m_pierce *= (1f + bonusPercent);
                    SkillEffect.spearAfterRoll[player] = false;
                    SkillEffect.PlaySkillEffect(player, "spear_Step2_evasion", hit.m_point);
                    SkillEffect.DrawFloatingText(player, $"🎯 회피 찌르기! (+{SkillTreeConfig.SpearStep3EvasionDamageBonusValue}%)");
                }

                // 삼연창 효과 (3연속 공격 시 +20%, pierce만)
                SkillEffect.CheckSpearTripleCombo(player);
                if (SkillEffect.spearTripleComboActive.TryGetValue(player, out bool tripleActive) && tripleActive)
                {
                    float bonusPercent = SkillTreeConfig.SpearStep5TripleDamageBonusValue / 100f;
                    hit.m_damage.m_pierce *= (1f + bonusPercent);
                    SkillEffect.spearTripleComboActive[player] = false;
                    SkillEffect.PlaySkillEffect(player, "spear_Step4_triple", hit.m_point);
                    SkillEffect.DrawFloatingText(player, $"🔥 삼연창! (+{SkillTreeConfig.SpearStep5TripleDamageBonusValue}%)");
                }
                
                // 연격창 (37% 확률로 추가 공격)
                SkillEffect.CheckDoubleAttack(player, __instance, hit);
                
                // 투창 전문가 (투창 공격력 +30%)
                SkillEffect.ApplySpearThrowExpertDamage(hit);
                
                // 꿰뚫는 창 - 치명타 확률 효과 적용
                SkillEffect.ApplySpearPenetrateCritical(player, hit);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 창 공격 패치 오류: {ex.Message}");
            }

            // === 폴암 스킬 효과 적용 ===
            try
            {
                var attacker = hit.GetAttacker();
                if (attacker == null || !attacker.IsPlayer() || __instance.IsPlayer()) return;

                var player = attacker as Player;
                if (player == null || !SkillEffect.IsUsingPolearm(player)) return;

                // 광역 강타 2연속 공격 체크 (polearm_step3_area)
                SkillEffect.CheckPolearmAreaCombo(player);

                // 다음 공격 부스트 적용 (광역 강타)
                if (SkillEffect.nextAttackBoosted.TryGetValue(player, out var boosted) && boosted)
                {
                    if (Time.time < SkillEffect.nextAttackExpiry[player])
                    {
                        float multiplier = SkillEffect.nextAttackMultiplier[player];
                        hit.m_damage.m_pierce *= multiplier;
                        hit.m_damage.m_slash *= multiplier;
                        hit.m_damage.m_blunt *= multiplier;
                        SkillEffect.nextAttackBoosted[player] = false;
                        Plugin.Log.LogDebug($"[광역 강타] 강화된 일격 적용 - {(multiplier - 1) * 100:F0}% 보너스");
                    }
                    else
                    {
                        SkillEffect.nextAttackBoosted[player] = false;
                    }
                }

                // 폴암강화 (무기 공격력 +5 패시브, pierce만)
                if (SkillEffect.HasSkill("polearm_step4_charge"))
                {
                    float bonusValue = SkillTreeConfig.PolearmStep4ChargeDamageBonusValue;
                    hit.m_damage.m_pierce += bonusValue;
                    Plugin.Log.LogDebug($"[폴암강화] 무기 공격력 보너스 적용 (pierce): +{bonusValue}");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 폴암 공격 패치 오류: {ex.Message}");
            }
        }
    }


    /// <summary>
    /// 구르기 시 창/단검 스킬 상태 추적 (Dodge 메서드를 통한 구르기 감지)
    /// </summary>
    [HarmonyPatch(typeof(Player), "Dodge")]
    public static class MeleeSkill_Dodge_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(Player __instance, Vector3 dodgeDir)
        {
            try
            {
                // 창 스킬: 회피 찌르기
                if (SkillEffect.IsUsingSpear(__instance))
                {
                    // 회피 찌르기를 위한 구르기 시간 기록
                    if (SkillEffect.HasSkill("spear_Step2_evasion"))
                    {
                        SkillEffect.spearAfterRoll[__instance] = true;
                        SkillEffect.spearRollTime[__instance] = Time.time;
                        
                        // 2초 후 버프 자동 해제 코루틴 시작
                        if (SkillEffect.spearRollBuffCoroutine.ContainsKey(__instance))
                        {
                            __instance.StopCoroutine(SkillEffect.spearRollBuffCoroutine[__instance]);
                        }
                        SkillEffect.spearRollBuffCoroutine[__instance] = __instance.StartCoroutine(ClearSpearRollBuff(__instance));
                        
                        Plugin.Log.LogInfo("[회피 찌르기] 구르기 감지 - 2초간 보너스 활성화");
                        SkillEffect.DrawFloatingText(__instance, "🏃 회피 찌르기 준비!", Color.green);
                    }
                }
                
                // 단검 스킬: 구르기 관련 버프들
                if (SkillEffect.IsUsingDagger(__instance))
                {
                    SkillEffect.knifeLastRollTime[__instance] = Time.time;
                    SkillEffect.knifeAfterRoll[__instance] = true;
                    
                    // 회피 숙련 - 구르기 후 회피율 증가
                    SkillEffect.CheckKnifeEvasion(__instance);
                    
                    // 빠른 움직임 - 이동속도 증가
                    SkillEffect.ActivateKnifeMoveSpeed(__instance);

                    // 빠른 공격 - 단검 데미지 증가
                    SkillEffect.ActivateKnifeAttackDamage(__instance);

                    // 치명타 숙련 - 치명타 확률 증가
                    SkillEffect.ActivateKnifeCritRate(__instance);
                    
                    Plugin.Log.LogInfo("[단검 구르기] 모든 단검 버프 스킬 활성화");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[근접 스킬] 구르기 패치 오류: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 창 회피 찌르기 버프 자동 해제 코루틴
        /// </summary>
        private static System.Collections.IEnumerator ClearSpearRollBuff(Player player)
        {
            yield return new WaitForSeconds(2f); // 2초 대기
            
            if (SkillEffect.spearAfterRoll.ContainsKey(player))
            {
                SkillEffect.spearAfterRoll[player] = false;
                Plugin.Log.LogInfo("[회피 찌르기] 버프 자동 해제");
                SkillEffect.DrawFloatingText(player, "회피 찌르기 종료", Color.gray);
            }
        }
    }

    // 근접 전문가 효과 패치
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new[] { typeof(int), typeof(float) })]
    public static class SkillTree_ItemData_GetDamage_MeleeExpert_Patch
    {
        [HarmonyPriority(Priority.Low)]
        public static void Postfix(ItemDrop.ItemData __instance, ref HitData.DamageTypes __result)
        {
            try
            {
                if (__instance?.m_shared == null) return;

                var player = Player.m_localPlayer;
                if (player == null) return;

                // 근접 전문가: 각 근접무기 타입별 데미지 +2 (고정값)
                if (SkillEffect.HasSkill("melee_root") && IsMeleeWeapon(__instance))
                {
                    // 주요 근접 데미지 타입에만 적용
                    if (__result.m_slash > 0) __result.m_slash += 2f;
                    if (__result.m_pierce > 0) __result.m_pierce += 2f;
                    if (__result.m_blunt > 0) __result.m_blunt += 2f;
                }

                // === 단검 패시브 스킬 보너스 ===
                if (__instance.m_shared.m_skillType == Skills.SkillType.Knives)
                {
                    // Tier 3: 빠른 공격 - 베기 데미지만 +2 (고정값)
                    float knifeDamageBonus = Knife_Skill.GetKnifeAttackDamageBonus(player);
                    if (knifeDamageBonus > 0)
                    {
                        if (__result.m_slash > 0) __result.m_slash += knifeDamageBonus;
                        Plugin.Log.LogDebug($"[빠른 공격] 단검 베기 데미지 +{knifeDamageBonus}");
                    }

                    // Tier 4: 치명적 피해 - 공격력 +25% (비율)
                    float combatDamageBonus = Knife_Skill.GetKnifeCombatDamageBonus(player);
                    if (combatDamageBonus > 0)
                    {
                        float multiplier = 1f + (combatDamageBonus / 100f);
                        __result.m_damage *= multiplier;
                        __result.m_slash *= multiplier;
                        __result.m_pierce *= multiplier;
                        __result.m_blunt *= multiplier;
                        Plugin.Log.LogDebug($"[치명적 피해] 단검 공격력 +{combatDamageBonus}% (배수: {multiplier:F2}x)");
                    }

                    // Tier 9: 암살자의 심장 (G키 액티브) - 피해 +50%
                    if (SkillEffect.IsKnifeAssassinHeartActive(player))
                    {
                        float heartDamageBonus = Knife_Config.KnifeAssassinHeartDamageBonusValue / 100f;
                        __result.m_slash *= (1f + heartDamageBonus);
                        __result.m_pierce *= (1f + heartDamageBonus);
                        Plugin.Log.LogDebug($"[암살자의 심장] 단검 데미지 +{Knife_Config.KnifeAssassinHeartDamageBonusValue}% (지속시간 중)");
                    }

                    // 로그 직업: 그림자 일격 버프 - 피해 증가
                    if (RogueSkills.IsRogueAttackBuffActive(player))
                    {
                        float rogueAttackBonus = Rogue_Config.RogueShadowStrikeAttackBonusValue / 100f;
                        __result.m_slash *= (1f + rogueAttackBonus);
                        __result.m_pierce *= (1f + rogueAttackBonus);
                        Plugin.Log.LogDebug($"[로그 그림자 일격] 단검 데미지 +{Rogue_Config.RogueShadowStrikeAttackBonusValue}% (버프 지속 중)");
                    }
                }

                // === 검 패시브 스킬 보너스 ===
                if (__instance.m_shared.m_skillType == Skills.SkillType.Swords)
                {
                    float totalSwordBonusPercent = 0f;
                    float totalSwordBonusFixed = 0f;

                    // Tier 0: 검 전문가 - 공격력 +10%
                    float expertBonus = Sword_Skill.GetSwordExpertDamageBonus(player);
                    if (expertBonus > 0)
                    {
                        totalSwordBonusPercent += expertBonus;
                        Plugin.Log.LogDebug($"[검 전문가] +{expertBonus}%");
                    }

                    // Tier 3: 칼날 되치기 - 고정값
                    float riposteBonus = Sword_Skill.GetSwordRiposteDamageBonus(player);
                    if (riposteBonus > 0)
                    {
                        totalSwordBonusFixed += riposteBonus;
                        Plugin.Log.LogDebug($"[칼날 되치기] +{riposteBonus} 고정");
                    }

                    // Tier 5: 방어 전환 - 방패 미착용 시
                    float defenseSwitchBonus = Sword_Skill.GetSwordDefenseSwitchDamageBonus(player);
                    if (defenseSwitchBonus > 0)
                    {
                        totalSwordBonusPercent += defenseSwitchBonus;
                    }

                    // 비율 보너스 적용 (slash만)
                    if (totalSwordBonusPercent > 0 && __result.m_slash > 0)
                    {
                        float multiplier = 1f + (totalSwordBonusPercent / 100f);
                        __result.m_slash *= multiplier;
                    }

                    // 고정값 보너스 적용 (slash만)
                    if (totalSwordBonusFixed > 0 && __result.m_slash > 0)
                    {
                        __result.m_slash += totalSwordBonusFixed;
                    }
                }

                // === 둔기 패시브 스킬 보너스 ===
                if (__instance.m_shared.m_skillType == Skills.SkillType.Clubs)
                {
                    float totalMaceBonusPercent = 0f;
                    float totalMaceBonusFixed = 0f;

                    // Tier 1: 둔기 전문가 - 공격력 +10%
                    if (SkillEffect.HasSkill("mace_Step1_damage"))
                    {
                        totalMaceBonusPercent += Mace_Config.MaceStep1DamageBonusValue;
                        Plugin.Log.LogDebug($"[둔기 전문가] +{Mace_Config.MaceStep1DamageBonusValue}%");
                    }

                    // Tier 3: 무거운 타격 - 공격력 +3 (고정값)
                    if (SkillEffect.HasSkill("mace_Step3_branch_heavy"))
                    {
                        totalMaceBonusFixed += Mace_Config.MaceStep3HeavyDamageBonusValue;
                        // 매 프레임 호출되므로 로그 제거
                    }

                    // Tier 5: 공격력 강화 - 공격력 +20%
                    if (SkillEffect.HasSkill("mace_Step5_dps"))
                    {
                        totalMaceBonusPercent += Mace_Config.MaceStep5DpsDamageBonusValue;
                        Plugin.Log.LogDebug($"[공격력 강화] +{Mace_Config.MaceStep5DpsDamageBonusValue}%");
                    }

                    // 비율 보너스 적용 (blunt만)
                    if (totalMaceBonusPercent > 0)
                    {
                        GetDamageHelper.ApplyPhysicalDamageBonus(ref __result, totalMaceBonusPercent);
                        // 매 프레임 호출되므로 LogDebug로 변경 (무한 로그 방지)
                    }

                    // 고정값 보너스 적용 (blunt만)
                    if (totalMaceBonusFixed > 0 && __result.m_blunt > 0)
                    {
                        __result.m_blunt += totalMaceBonusFixed;
                        // 매 프레임 호출되므로 LogDebug로 변경 (무한 로그 방지)
                    }
                }

                // === 창 패시브 스킬 보너스 ===
                if (__instance.m_shared.m_skillType == Skills.SkillType.Spears)
                {
                    float totalSpearBonus = 0f;

                    // Tier 1: 창 전문가 - 2연속 공격 시 공격력 보너스
                    if (SkillEffect.HasSkill("spear_expert"))
                    {
                        totalSpearBonus += SkillTreeConfig.SpearStep1DamageBonusValue;
                        Plugin.Log.LogDebug($"[창 전문가] +{SkillTreeConfig.SpearStep1DamageBonusValue}%");
                    }

                    // Tier 2: 회피 찌르기 - 구르기 직후 공격 시 피해 보너스
                    if (SkillEffect.HasSkill("spear_Step2_evasion"))
                    {
                        totalSpearBonus += SkillTreeConfig.SpearStep3EvasionDamageBonusValue;
                        Plugin.Log.LogDebug($"[회피 찌르기] +{SkillTreeConfig.SpearStep3EvasionDamageBonusValue}%");
                    }

                    // Tier 3: 연격창 - 무기 공격력 보너스
                    if (SkillEffect.HasSkill("spear_Step3_pierce"))
                    {
                        totalSpearBonus += SkillTreeConfig.SpearStep3PierceDamageBonusValue;
                        Plugin.Log.LogDebug($"[연격창] +{SkillTreeConfig.SpearStep3PierceDamageBonusValue}%");
                    }

                    // Tier 4: 삼연창 - 3연속 공격 시 공격력 보너스
                    if (SkillEffect.HasSkill("spear_Step4_triple"))
                    {
                        totalSpearBonus += SkillTreeConfig.SpearStep5TripleDamageBonusValue;
                        Plugin.Log.LogDebug($"[삼연창] +{SkillTreeConfig.SpearStep5TripleDamageBonusValue}%");
                    }

                    // GetDamageHelper를 사용하여 물리 데미지 보너스 적용
                    if (totalSpearBonus > 0)
                    {
                        GetDamageHelper.ApplyPhysicalDamageBonus(ref __result, totalSpearBonus);
                        Plugin.Log.LogInfo($"[창 스킬] 총 데미지 +{totalSpearBonus}%");
                    }
                }

                // === 폴암 패시브 스킬 보너스 ===
                if (__instance.m_shared.m_skillType == Skills.SkillType.Polearms)
                {
                    float totalPolearmBonusPercent = 0f;
                    float totalPolearmBonusFixed = 0f;

                    // Tier 1: 제압 공격 - 공격력 +30%
                    if (SkillEffect.HasSkill("polearm_step1_suppress"))
                    {
                        totalPolearmBonusPercent += SkillTreeConfig.PolearmStep1SuppressDamageValue;
                        Plugin.Log.LogDebug($"[제압 공격] +{SkillTreeConfig.PolearmStep1SuppressDamageValue}%");
                    }

                    // Tier 4: 폴암강화 - 무기 공격력 +5 (고정값)
                    if (SkillEffect.HasSkill("polearm_step4_charge"))
                    {
                        totalPolearmBonusFixed += SkillTreeConfig.PolearmStep4ChargeDamageBonusValue;
                        Plugin.Log.LogDebug($"[폴암강화] +{SkillTreeConfig.PolearmStep4ChargeDamageBonusValue} 고정 데미지");
                    }

                    // 비율 보너스 적용
                    if (totalPolearmBonusPercent > 0)
                    {
                        GetDamageHelper.ApplyPhysicalDamageBonus(ref __result, totalPolearmBonusPercent);
                        Plugin.Log.LogInfo($"[폴암 스킬] 비율 보너스 +{totalPolearmBonusPercent}%");
                    }

                    // 고정값 보너스 적용 (pierce에만)
                    if (totalPolearmBonusFixed > 0)
                    {
                        GetDamageHelper.AddFixedDamage(ref __result, totalPolearmBonusFixed, "pierce");
                        Plugin.Log.LogInfo($"[폴암 스킬] 고정 데미지 +{totalPolearmBonusFixed} (pierce)");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[스킬트리→발하임] GetDamage 근접전문가 패치 오류: {ex.Message}");
            }
        }
        
        private static bool IsMeleeWeapon(ItemDrop.ItemData weapon)
        {
            if (weapon?.m_shared == null) return false;
            
            return weapon.m_shared.m_skillType == Skills.SkillType.Swords ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Axes ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Clubs ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Knives ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Spears ||
                   weapon.m_shared.m_skillType == Skills.SkillType.Polearms;
        }
    }
    
    /// <summary>
    /// 창 패시브 스킬들의 MMO 연동 - Strength 스탯 증가
    /// </summary>
    // [HarmonyPatch] // 임시 비활성화 - TargetMethod 오류 방지
    public static class SpearPassiveSkills_MMO_Strength_Patch
    {
        // MMO 시스템 사용 가능 여부 확인
        static bool Prepare() 
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) 
            {
                Plugin.Log.LogWarning("[MMO 패치] EpicMMOSystem을 찾을 수 없어 창 패시브 MMO 연동 패치를 건너뜁니다");
                return false;
            }
            return true;
        }
        
        // EpicMMOSystem.LevelSystem.getParameter 메서드 패치
        static System.Reflection.MethodBase TargetMethod()
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) return null;
            
            return levelSystemType.GetMethod("getParameter", new[] { typeof(object) });
        }
        
        [HarmonyPriority(Priority.High)]
        public static void Postfix(object parameter, ref int __result)
        {
            try
            {
                if (parameter?.ToString() != "Strength") return;
                
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingSpear(player)) return;
                
                int strengthBonus = 0;
                
                // 창 전문가 - 투창 공격력 보너스 (MMO 스탯 연동)
                if (SkillEffect.HasSkill("spear_expert"))
                {
                    // 투창 공격력 30% 기본값, 설정에서 조정 가능
                    float damageBonus = 30f; // 기본 30%
                    strengthBonus += (int)(damageBonus * 0.5f); // Strength 변환 계수
                }
                
                // 급소 찌르기 (Step2) - Strength +10 (창 공격력 +20% 효과)
                if (SkillEffect.HasSkill("spear_Step1_crit"))
                {
                    strengthBonus += 10;
                }
                
                // 회피 찌르기 (Step3) - 조건부 보너스는 직접 패치에서 처리
                
                // 쾌속 창 (Step4) - 투창 공격력 +40%
                if (SkillEffect.HasSkill("spear_Step3_quick"))
                {
                    strengthBonus += 20; // Strength +20 = +40% 투창 공격력
                }
                
                // 삼연창 (Step5) - Strength +10 (3연속 공격력 +20% 효과)
                if (SkillEffect.HasSkill("spear_Step4_triple"))
                {
                    strengthBonus += 10;
                }
                
                // 꿰뚫는 창 (Step6) - Strength +12 (관통 효과)
                if (SkillEffect.HasSkill("spear_Step5_penetrate"))
                {
                    strengthBonus += 12;
                }
                
                if (strengthBonus > 0)
                {
                    __result += strengthBonus;
                    Plugin.Log.LogDebug($"[창 패시브] MMO Strength 증가: +{strengthBonus}");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[창 패시브] MMO Strength 패치 오류: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// 창 쾌속 창 스킬 - MMO Agility 스탯 연동
    /// </summary>
    // [HarmonyPatch] // 임시 비활성화 - TargetMethod 오류 방지
    public static class SpearQuickSpeed_MMO_Agility_Patch
    {
        // MMO 시스템 사용 가능 여부 확인
        static bool Prepare() 
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) 
            {
                Plugin.Log.LogWarning("[MMO 패치] EpicMMOSystem을 찾을 수 없어 쾌속 창 MMO 연동 패치를 건너뜁니다");
                return false;
            }
            return true;
        }
        
        // EpicMMOSystem.LevelSystem.getParameter 메서드 패치
        static System.Reflection.MethodBase TargetMethod()
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) return null;
            
            return levelSystemType.GetMethod("getParameter", new[] { typeof(object) });
        }
        
        [HarmonyPriority(Priority.High)]
        public static void Postfix(object parameter, ref int __result)
        {
            try
            {
                if (parameter?.ToString() != "Agility") return;
                
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingSpear(player)) return;
                
                int agilityBonus = 0;
                
                // 쾌속 창 직접 처리 (투창 공격력 보너스는 Strength로 이동)
                
                if (agilityBonus > 0)
                {
                    __result += agilityBonus;
                    // 쾌속 창 Agility 보너스 제거됨
                    // Plugin.Log.LogDebug($"[쾌속 창] MMO Agility 증가: +{agilityBonus}");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[쾌속 창] MMO Agility 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 단검 스킬들의 MMO 연동 - Agility 및 Strength 스탯 증가
    /// </summary>
    // [HarmonyPatch] // 임시 비활성화 - TargetMethod 오류 방지
    public static class DaggerSkills_MMO_Stats_Patch
    {
        // MMO 시스템 사용 가능 여부 확인
        static bool Prepare() 
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) 
            {
                Plugin.Log.LogWarning("[MMO 패치] EpicMMOSystem을 찾을 수 없어 단검 스킬 MMO 연동 패치를 건너뜁니다");
                return false;
            }
            return true;
        }
        
        // EpicMMOSystem.LevelSystem.getParameter 메서드 패치
        static System.Reflection.MethodBase TargetMethod()
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) return null;
            
            return levelSystemType.GetMethod("getParameter", new[] { typeof(object) });
        }
        
        [HarmonyPriority(Priority.High)]
        public static void Postfix(object parameter, ref int __result)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingDagger(player)) return;
                
                string paramType = parameter?.ToString();
                int bonus = 0;
                float currentTime = Time.time;
                
                if (paramType == "Agility") // 민첩성 - 이동속도, 회피율
                {
                    // 빠른 공격 → 단검 데미지로 변경됨 (Agility와 무관)
                    // 단검 데미지 버프는 데미지 계산 시 직접 적용
                    
                    // 빠른 움직임 - 이동속도 증가 (버프 활성화 시)
                    if (SkillEffect.knifeMoveSpeedEndTime.TryGetValue(player, out float moveSpeedEndTime) && 
                        currentTime < moveSpeedEndTime)
                    {
                        bonus += (int)(Knife_Config.KnifeMoveSpeedBonusValue * 3); // 12% = +36 Agility
                    }
                    
                    // 회피 숙련 - 회피율 증가 (버프 활성화 시)
                    if (SkillEffect.knifeEvasionEndTime.TryGetValue(player, out float evasionEndTime) && 
                        currentTime < evasionEndTime)
                    {
                        bonus += (int)(Knife_Config.KnifeEvasionBonusValue * 2);
                    }
                }
                else if (paramType == "Strength") // 힘 - 공격력
                {
                    // 단검 전문가 - 백스탭 공격력 (패시브 보너스)
                    if (SkillEffect.HasSkill("knife_expert_backstab"))
                    {
                        bonus += (int)(Knife_Config.KnifeExpertBackstabBonusValue * 0.6f); // 25% = +15 Strength
                    }
                    
                    // 전투 숙련 - 전투 중 공격력 증가 (패시브)
                    if (SkillEffect.HasSkill("knife_step6_combat_damage"))
                    {
                        bonus += (int)(Knife_Config.KnifeCombatDamageBonusValue * 0.8f); // 18% = +14.4 Strength
                    }
                    
                    // 처형술 - 특수 효과에 대한 고정 보너스
                    if (SkillEffect.HasSkill("knife_step7_execution"))
                    {
                        bonus += 8; // 처형 능력에 대한 고정 Strength 보너스
                    }
                    
                    // 암살술 - 은신 상태 치명타에 대한 보너스
                    if (SkillEffect.HasSkill("knife_step8_assassination"))
                    {
                        bonus += 10; // 암살 능력에 대한 고정 Strength 보너스
                    }
                }
                else if (paramType == "Body") // 체력 - 생존력
                {
                    // 회피 숙련 - 회피율 증가 (방어적 효과)
                    if (SkillEffect.HasSkill("knife_step2_evasion"))
                    {
                        bonus += 5; // 회피 능력에 대한 Body 보너스
                    }
                }
                
                if (bonus > 0)
                {
                    __result += bonus;
                    Plugin.Log.LogDebug($"[단검 스킬] MMO {paramType} 증가: +{bonus}");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[단검 스킬] MMO 스탯 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 검 스킬들의 MMO 연동 - Strength 및 Agility 스탯 증가
    /// </summary>
    // [HarmonyPatch] // 임시 비활성화 - TargetMethod 오류 방지
    public static class SwordSkills_MMO_Stats_Patch
    {
        // MMO 시스템 사용 가능 여부 확인
        static bool Prepare() 
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) 
            {
                Plugin.Log.LogWarning("[MMO 패치] EpicMMOSystem을 찾을 수 없어 검 스킬 MMO 연동 패치를 건너뜁니다");
                return false;
            }
            return true;
        }
        
        // EpicMMOSystem.LevelSystem.getParameter 메서드 패치
        static System.Reflection.MethodBase TargetMethod()
        {
            var levelSystemType = System.Type.GetType("EpicMMOSystem.LevelSystem, EpicMMOSystem");
            if (levelSystemType == null) return null;
            
            return levelSystemType.GetMethod("getParameter", new[] { typeof(object) });
        }
        
        [HarmonyPriority(Priority.High)]
        public static void Postfix(object parameter, ref int __result)
        {
            try
            {
                var player = Player.m_localPlayer;
                if (player == null || !SkillEffect.IsUsingSword(player)) return;
                
                string paramType = parameter?.ToString();
                int bonus = 0;
                
                if (paramType == "Strength")
                {
                    // 검 전문가 - 2연속 공격력 보너스 (MMO 스템 연동)
                    if (SkillEffect.HasSkill("sword_expert"))
                    {
                        bonus += (int)(SkillTreeConfig.SwordStep1ExpertComboBonusValue * 1.5f); // 7% * 1.5 = +10.5 Strength
                    }
                    
                    // 연속베기 (Step3) - 콤보 공격력 보너스
                    if (SkillEffect.HasSkill("sword_Step2_combo_slash"))
                    {
                        bonus += (int)(SkillTreeConfig.SwordStep2ComboSlashBonusValue * 1.2f); // 13% * 1.2 = +15.6 Strength
                    }
                    
                    // 칼날 되치기 (Step4) - 패링 후 공격력 보너스 (조건부이므로 기본 5만)
                    if (SkillEffect.HasSkill("sword_Step3_blade_counter"))
                    {
                        bonus += 5;
                    }
                    
                    // 공방일체 (Step4) - 양손 무기 공격력 보너스 (양손검 착용 시만)
                    if (SkillEffect.HasSkill("sword_Step3_offense_defense") && SkillEffect.IsUsingTwoHandedSword(player))
                    {
                        bonus += (int)(SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue * 1.0f); // 20%
                    }
                    
                    // 진검승부 (Step4) - 검 전문가 5단계 패시브 공격력 보너스
                    if (SkillEffect.HasSkill("sword_Step4_true_duel"))
                    {
                        bonus += 8; // 진검승부 패시브 공격력 보너스
                    }
                    
                    // 방어 전환 (Step6) - 방패 없을 시 공격력 보너스
                    if (SkillEffect.HasSkill("sword_Step5_defense_switch") && !SkillEffect.HasShield(player))
                    {
                        bonus += (int)(SkillTreeConfig.SwordStep5DefenseSwitchNoShieldBonusValue * 0.8f); // 30% * 0.8 = +24
                    }
                    
                    // 궁극 베기 (Step7) - 모든 검 스킬 강화
                    if (SkillEffect.HasSkill("sword_Step6_ultimate_slash"))
                    {
                        bonus = (int)(bonus * SkillTreeConfig.SwordStep6UltimateSlashMultiplierValue); // 1.5배 강화
                    }
                }
                else if (paramType == "Body") // 방어력 관련
                {
                    // 반격 자세 (Step2) - 방어력 보너스 (조건부이므로 기본값만)
                    if (SkillEffect.HasSkill("sword_Step1_counter_stance"))
                    {
                        bonus += 3; // 기본 방어력 향상
                    }
                    
                    // 공방일체 (Step4) - 양손 무기 방어력 보너스
                    if (SkillEffect.HasSkill("sword_Step3_offense_defense") && SkillEffect.IsUsingTwoHandedSword(player))
                    {
                        bonus += (int)(SkillTreeConfig.SwordStep3OffenseDefenseDefenseBonusValue * 0.5f); // 10% * 0.5 = +5
                    }
                    
                    // 방어 전환 (Step6) - 방패 착용 시 방어력 보너스
                    if (SkillEffect.HasSkill("sword_Step5_defense_switch") && SkillEffect.HasShield(player))
                    {
                        bonus += (int)(SkillTreeConfig.SwordStep5DefenseSwitchShieldReductionValue * 0.6f); // 8% * 0.6 = +4.8
                    }
                }
                
                if (bonus > 0)
                {
                    __result += bonus;
                    Plugin.Log.LogDebug($"[검 스킬] MMO {paramType} 증가: +{bonus}");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[검 스킬] MMO 스탯 패치 오류: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 근접 무기 스킬 사망 시 정리 시스템
    /// </summary>
    public static partial class SkillEffect
    {
        public static void CleanupMeleeSkillsOnDeath(Player player)
        {
            try
            {
                // 단검 관련 Dictionary 정리 (10개)
                consecutiveHits.Remove(player);
                lastHitTime.Remove(player);
                if (evasionBuffCoroutine.ContainsKey(player))
                {
                    evasionBuffCoroutine.Remove(player);
                }
                evasionBonus.Remove(player);
                stealthMovementBonus.Remove(player);
                knifeEvasionEndTime.Remove(player);
                knifeMoveSpeedEndTime.Remove(player);
                knifeDamageBonusEndTime.Remove(player);
                knifeCritRateEndTime.Remove(player);
                knifeLastRollTime.Remove(player);
                knifeAfterRoll.Remove(player);
                knifeAssassinHeartEndTime.Remove(player);
                knifeAssassinHeartCooldownEndTime.Remove(player);

                // 검 관련 Dictionary 정리 (8개)
                swordComboCount.Remove(player);
                swordLastHitTime.Remove(player);
                if (defenseBuffCoroutine.ContainsKey(player))
                {
                    defenseBuffCoroutine.Remove(player);
                }
                defenseBonus.Remove(player);
                nextAttackBoosted.Remove(player);
                nextAttackMultiplier.Remove(player);
                nextAttackExpiry.Remove(player);
                swordCounterDefenseEndTime.Remove(player);
                swordBladeCounterEndTime.Remove(player);

                // 창 관련 Dictionary 정리 (10개)
                spearComboCount.Remove(player);
                spearLastHitTime.Remove(player);
                spearThrowCooldown.Remove(player);
                spearAfterRoll.Remove(player);
                spearRollTime.Remove(player);
                if (spearRollBuffCoroutine.ContainsKey(player))
                {
                    spearRollBuffCoroutine.Remove(player);
                }
                spearTripleComboActive.Remove(player);
                spearComboSequenceActive.Remove(player);
                spearExpertComboCount.Remove(player);
                spearExpertLastHitTime.Remove(player);
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[Melee Skills] 정리 실패: {ex.Message}");
            }
        }
    }
}
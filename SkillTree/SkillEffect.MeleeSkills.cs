using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 근접 무기 (단검, 검, 둔기, 창, 폴암) 패시브 스킬 시스템
    ///
    /// 분리된 파일들:
    /// - SkillEffect.KnifeSkillEffects.cs - 단검 스킬 효과
    /// - SkillEffect.SwordSpearSkillEffects.cs - 검/창/폴암 스킬 효과
    /// - SkillEffect.MeleeSkillPatches.cs - Harmony 패치들
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
        public static Dictionary<Player, float> knifeAttackEvasionEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeAttackEvasionCooldownEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeLastRollTime = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> knifeAfterRoll = new Dictionary<Player, bool>();

        // 암살자의 심장 액티브 스킬 상태 추적
        public static Dictionary<Player, float> knifeAssassinHeartEndTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> knifeAssassinHeartCooldownEndTime = new Dictionary<Player, float>();

        // 암살자의 심장 연속 공격 시스템
        public static Dictionary<Player, int> assassinHeartHitCount = new Dictionary<Player, int>();
        public static Dictionary<Player, bool> assassinHeartAttackMode = new Dictionary<Player, bool>();
        public static Dictionary<Player, Character> assassinHeartTarget = new Dictionary<Player, Character>();
        public static Dictionary<Player, float> assassinHeartAttackSpeedBonus = new Dictionary<Player, float>();

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

        // 창 관련 상태 변수
        public static Dictionary<Player, int> spearComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> spearLastHitTime = new Dictionary<Player, float>();
        public static Dictionary<Player, float> spearThrowCooldown = new Dictionary<Player, float>();
        public static Dictionary<Player, bool> spearAfterRoll = new Dictionary<Player, bool>();
        public static Dictionary<Player, float> spearRollTime = new Dictionary<Player, float>();
        public static Dictionary<Player, Coroutine> spearRollBuffCoroutine = new Dictionary<Player, Coroutine>();
        public static Dictionary<Player, bool> spearTripleComboActive = new Dictionary<Player, bool>();
        public static Dictionary<Player, bool> spearComboSequenceActive = new Dictionary<Player, bool>();

        // 창 전문가 콤보 상태 추적
        public static Dictionary<Player, int> spearExpertComboCount = new Dictionary<Player, int>();
        public static Dictionary<Player, float> spearExpertLastHitTime = new Dictionary<Player, float>();

        // 반사 효과 관련 변수 (둔기 관련)
        public static float guardianSoulReflectionEndTime = 0f;

        // === 근접 스킬 이펙트 데이터 ===
        private static readonly Dictionary<string, SkillEffectData> meleeSkillEffects = new Dictionary<string, SkillEffectData>
        {
            // 단검 스킬들 (패시브는 VFX/SFX 제거)
            ["knife_expert_backstab"] = new SkillEffectData("", "", Color.clear),
            ["knife_step2_evasion"] = new SkillEffectData("", "", Color.clear),
            ["knife_step3_move_speed"] = new SkillEffectData("", "", Color.clear),
            ["knife_step4_attack_damage"] = new SkillEffectData("", "", Color.clear),
            ["knife_step5_crit_rate"] = new SkillEffectData("", "", Color.clear),
            ["knife_step6_combat_damage"] = new SkillEffectData("", "", Color.clear),
            ["knife_step7_execution"] = new SkillEffectData("", "", Color.clear),
            ["knife_step8_assassination"] = new SkillEffectData("fx_crit_large", "sfx_sword_hit", Color.magenta),
            ["knife_step9_assassin_heart"] = new SkillEffectData("fx_guardstone_activate", "", new Color(1f, 0.2f, 0.2f)),

            // 검 스킬들
            ["sword_expert"] = new SkillEffectData("", "", Color.clear),
            ["sword_Step1_fast_slash"] = new SkillEffectData("fx_sword_hit", "sfx_sword_swing", Color.cyan),
            ["sword_Step1_counter_stance"] = new SkillEffectData("fx_block", "sfx_blocked", Color.white),
            ["sword_Step2_combo_slash"] = new SkillEffectData("fx_crit_large", "sfx_sword_hit", Color.yellow),
            ["sword_Step3_blade_counter"] = new SkillEffectData("fx_guardstone_activate", "sfx_sword_hit", Color.red),
            ["sword_Step3_offense_defense"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", Color.green),
            ["sword_Step4_true_duel"] = new SkillEffectData("fx_dodge_attack", "sfx_sword_swing", Color.magenta),
            ["sword_Step5_defense_switch"] = new SkillEffectData("fx_block", "sfx_blocked", new Color(1f, 0.5f, 0f)),
            ["sword_Step6_ultimate_slash"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", Color.yellow),
            ["sword_slash"] = new SkillEffectData("fx_sword_hit", "sfx_sword_swing", Color.red),

            // 둔기 스킬들
            ["mace_expert"] = new SkillEffectData("fx_mace_hit", "sfx_mace_swing", Color.gray),
            ["mace_crush"] = new SkillEffectData("fx_mace_crush", "sfx_mace_crush", Color.red),

            // 창 스킬들
            ["spear_expert"] = new SkillEffectData("", "", Color.clear),
            ["spear_Step1_vital_strike"] = new SkillEffectData("fx_pierce", "sfx_spear_crit", Color.red),
            ["spear_Step2_dodge_strike"] = new SkillEffectData("fx_dodge_attack", "sfx_dodge", Color.green),
            ["spear_Step3_combo_strike"] = new SkillEffectData("fx_double_hit", "sfx_spear_double", new Color(1f, 0.5f, 0f)),

            // 폴암 스킬들
            ["polearm_expert"] = new SkillEffectData("", "", Color.clear),
            ["polearm_step1_spin"] = new SkillEffectData("", "", Color.clear),
            ["polearm_step1_suppress"] = new SkillEffectData("", "", Color.clear),
            ["polearm_step2_hero"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", Color.green),
            ["polearm_step3_area"] = new SkillEffectData("", "", Color.clear),
            ["polearm_step3_ground"] = new SkillEffectData("", "", Color.clear),
            ["polearm_step4_moon"] = new SkillEffectData("", "", Color.clear),
            ["polearm_step4_charge"] = new SkillEffectData("", "", Color.clear),
            ["polearm_step5_king"] = new SkillEffectData("fx_guardstone_activate", "sfx_creature_tamed", new Color(1f, 0.8f, 0f)),
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

            // 검 스킬 이름들
            ["sword_expert"] = "검 전문가",
            ["sword_Step1_fast_slash"] = "빠른 베기",
            ["sword_Step1_counter_stance"] = "반격 자세",
            ["sword_Step2_combo_slash"] = "연속베기",
            ["sword_Step3_blade_counter"] = "칼날 되치기",
            ["sword_Step3_offense_defense"] = "공방일체",
            ["sword_Step4_true_duel"] = "진검승부",
            ["sword_Step5_defense_switch"] = "방어 전환",
            ["sword_Step6_ultimate_slash"] = "궁극 베기",
            ["sword_slash"] = "검의 일섬",

            // 둔기 스킬 이름들
            ["mace_expert"] = "둔기 전문가",
            ["mace_crush"] = "분쇄 공격",

            // 창 스킬 이름들
            ["spear_expert"] = "창 전문가",
            ["spear_Step1_vital_strike"] = "급소 찌르기",
            ["spear_Step2_dodge_strike"] = "회피 찌르기",
            ["spear_Step3_combo_strike"] = "연격창",

            // 폴암 스킬 이름들
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
        /// 플레이어가 단검을 사용 중인지 확인 (Claw/claw, Fist/fist 포함)
        /// </summary>
        public static bool IsUsingDagger(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            if (weapon.m_shared.m_skillType == Skills.SkillType.Knives)
                return true;

            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Dagger") || prefabName.Contains("dagger") ||
                prefabName.Contains("Knife") || prefabName.Contains("knife") ||
                prefabName.Contains("Claw") || prefabName.Contains("claw") ||
                prefabName.Contains("Fist") || prefabName.Contains("fist"))
                return true;

            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("단검") || weaponName.Contains("dagger") ||
                weaponName.Contains("knife") || weaponName.Contains("claw") ||
                weaponName.Contains("fist"))
                return true;

            return false;
        }

        /// <summary>
        /// 플레이어가 검을 사용 중인지 확인
        /// </summary>
        public static bool IsUsingSword(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            if (weapon.m_shared.m_skillType == Skills.SkillType.Swords)
                return true;

            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Sword") || prefabName.Contains("sword") ||
                prefabName.Contains("Blade") || prefabName.Contains("blade"))
                return true;

            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("검") || weaponName.Contains("sword") || weaponName.Contains("blade"))
                return true;

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
        /// 플레이어가 둔기를 사용 중인지 확인
        /// </summary>
        public static bool IsUsingMace(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            if (weapon.m_shared.m_skillType == Skills.SkillType.Clubs)
                return true;

            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Mace") || prefabName.Contains("mace") ||
                prefabName.Contains("Club") || prefabName.Contains("club") ||
                prefabName.Contains("Hammer") || prefabName.Contains("hammer"))
                return true;

            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("둔기") || weaponName.Contains("mace") ||
                weaponName.Contains("club") || weaponName.Contains("hammer"))
                return true;

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
        /// 플레이어가 창을 사용 중인지 확인
        /// </summary>
        public static bool IsUsingSpear(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            if (weapon.m_shared.m_skillType == Skills.SkillType.Spears)
                return true;

            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Spear") || prefabName.Contains("spear") ||
                prefabName.Contains("Lance") || prefabName.Contains("lance") ||
                prefabName.Contains("Pike") || prefabName.Contains("pike"))
                return true;

            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("창") || weaponName.Contains("spear") ||
                weaponName.Contains("lance") || weaponName.Contains("pike"))
                return true;

            return false;
        }

        /// <summary>
        /// 플레이어가 폴암을 사용 중인지 확인
        /// </summary>
        public static bool IsUsingPolearm(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            if (weapon.m_shared.m_skillType == Skills.SkillType.Polearms)
                return true;

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

            var inventory = player.GetInventory();
            if (inventory == null) return false;

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

        // === 스킬 데이터 등록 메서드 ===

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
    }
}

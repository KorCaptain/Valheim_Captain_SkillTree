using System;
using UnityEngine;
using CaptainSkillTree.Gui;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 액티브 스킬 핵심 시스템 - 키 입력 처리 및 무기 감지 헬퍼
    ///
    /// 분리된 파일들:
    /// - SkillEffect.StaffDualCast.cs - 지팡이 이중시전
    /// - SkillEffect.StaffAreaHeal.cs - 지팡이 범위 힐
    /// - SkillEffect.CrossbowOneShot.cs - 석궁 단 한 발
    /// - SkillEffect.SpearActiveSkills.cs - 창 연공창/투창
    /// - SkillEffect.MiscActiveSkills.cs - 발구르기, 도발, 기타
    ///
    /// 디스패치 방식: 착용 무기 우선 → 해당 무기의 스킬 발동
    /// 여러 키스킬을 배워도 착용 무기에 따라 자동 전환됨
    /// </summary>
    public static partial class SkillEffect
    {
        // === 탱커 Lv2 업그레이드 선행 스킬 사용 추적 ===
        /// <summary>선행 스킬(7종) 마지막 사용 시각 — 탱커 Lv2 업그레이드 시 30초 이내인지 체크</summary>
        public static float TankerPrereqLastUsedTime = -999f;

        // === 액티브 스킬 키 입력 처리 시스템 ===

        public static void HandleActiveSkillInputs(Player player)
        {
            if (player == null || player.IsDead()) return;
            JobSkills.ExecuteJobSkill(player);
        }

        /// <summary>
        /// Z/R키: 원거리 액티브 스킬 (석궁, 활, 지팡이)
        /// 착용 무기가 학습한 스킬과 일치하면 발동, 아니면 "스킬에 맞는 무기를 착용하세요"
        /// </summary>
        public static void HandleRKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;
            if (SkillTreeInputListener_KeyPatch.IsChatOrConsoleOpen()) return;

            bool hasCrossbowSkill = HasSkill("crossbow_Step6_expert");
            bool hasBowSkill = HasSkill("bow_Step6_critboost");
            bool hasStaffSkill = HasSkill("staff_Step6_dual_cast");

            // Phase 1: 착용 무기 + 스킬 동시 일치 시 발동
            if (hasCrossbowSkill && WeaponHelper.IsUsingCrossbow(player))
            {
                ActivateCrossbowOneShot(player);
                return;
            }
            if (hasBowSkill && WeaponHelper.IsUsingBow(player))
            {
                ExecuteExplosiveArrow(player);
                return;
            }
            if (hasStaffSkill && WeaponHelper.IsUsingStaffOrWand(player))
            {
                ActivateStaffDualCast(player);
                return;
            }

            // Phase 2: 학습한 Z키 스킬이 있지만 착용 무기가 맞지 않음
            bool hasAnyRSkill = hasCrossbowSkill || hasBowSkill || hasStaffSkill;
            if (hasAnyRSkill)
            {
                DrawFloatingText(player, L.Get("skill_weapon_mismatch"), Color.red);
                return;
            }

            // Phase 3: 아무 원거리 스킬도 미학습
            DrawFloatingText(player, L.Get("r_key_skill_condition_not_met"), Color.red);
        }

        /// <summary>
        /// G키: 무기-우선 디스패치 (착용 무기 → 해당 무기의 G키 스킬 발동)
        /// </summary>
        public static void HandleGKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;
            if (SkillTreeInputListener_KeyPatch.IsChatOrConsoleOpen()) return;

            bool hasAnyGSkill = HasSkill("sword_step5_finalcut") || HasSkill("sword_slash") ||
                                HasSkill("knife_step9_assassin_heart") ||
                                HasSkill("spear_Step5_penetrate") ||
                                HasSkill("polearm_step5_king") ||
                                HasSkill("mace_Step7_guardian_heart") ||
                                HasSkill("defense_Step6_mind");

            // 1. 지팡이/완드: 마인드쉴드 (방어전문가)
            if (WeaponHelper.IsUsingStaffOrWand(player))
            {
                if (HasSkill("defense_Step6_mind"))
                {
                    MindShieldSkill.Activate(player);
                    return;
                }
                DrawFloatingText(player, hasAnyGSkill ? L.Get("skill_weapon_mismatch") : L.Get("g_key_skill_required"), Color.red);
                return;
            }

            // 2. 검/도끼: 돌진 연속 베기
            if (WeaponHelper.IsUsingSwordOrAxe(player))
            {
                if (HasSkill("sword_step5_finalcut") || HasSkill("sword_slash"))
                {
                    Sword_Skill.ActivateSwordSlash(player);
                    return;
                }
                // 검 G스킬 없음 → 방패돌진 폴백 (검+방패 조합)
                if (HasSkill("mace_Step7_guardian_heart"))
                {
                    if (WeaponHelper.HasShield(player)) { ActivateShieldCharge(player); return; }
                    DrawFloatingText(player, L.Get("shield_equip_required"), Color.red);
                    return;
                }
                DrawFloatingText(player, hasAnyGSkill ? L.Get("skill_weapon_mismatch") : L.Get("g_key_skill_required"), Color.red);
                return;
            }

            // 3. 단검: 암살자의 심장
            if (WeaponHelper.IsUsingDagger(player))
            {
                if (HasSkill("knife_step9_assassin_heart"))
                {
                    ActivateKnifeAssassinHeart(player);
                    return;
                }
                DrawFloatingText(player, hasAnyGSkill ? L.Get("skill_weapon_mismatch") : L.Get("g_key_skill_required"), Color.red);
                return;
            }

            // 4. 창: 꿰뚫는 창
            if (WeaponHelper.IsUsingSpear(player))
            {
                if (HasSkill("spear_Step5_penetrate"))
                {
                    ActivateSpearPenetrateLightning(player);
                    return;
                }
                DrawFloatingText(player, hasAnyGSkill ? L.Get("skill_weapon_mismatch") : L.Get("g_key_skill_required"), Color.red);
                return;
            }

            // 5. 폴암: 관통 돌격
            if (WeaponHelper.IsUsingPolearm(player))
            {
                if (HasSkill("polearm_step5_king"))
                {
                    UsePolearmPierceChargeSkill(player);
                    return;
                }
                DrawFloatingText(player, hasAnyGSkill ? L.Get("skill_weapon_mismatch") : L.Get("g_key_skill_required"), Color.red);
                return;
            }

            // 6. 방패돌진: 무기 종류 무관, 방패 착용 여부만 확인
            if (HasSkill("mace_Step7_guardian_heart"))
            {
                if (WeaponHelper.HasShield(player))
                {
                    ActivateShieldCharge(player);
                    return;
                }
                DrawFloatingText(player, L.Get("shield_equip_required"), Color.red);
                return;
            }

            // 7. 어떤 무기도 G키 스킬과 매칭 안 됨
            DrawFloatingText(player, hasAnyGSkill ? L.Get("skill_weapon_mismatch") : L.Get("g_key_skill_required"), Color.red);
        }

        /// <summary>
        /// G키 해제 처리
        /// </summary>
        public static void HandleGKeyUpSkills(Player player)
        {
            if (player == null || player.IsDead()) return;
        }

        /// <summary>
        /// H키: 무기-우선 디스패치 (착용 무기 → 해당 무기의 H키 스킬 발동)
        /// </summary>
        public static void HandleHKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;
            if (SkillTreeInputListener_KeyPatch.IsChatOrConsoleOpen()) return;

            bool hasAnyHSkill = HasSkill("crossbow_ice_breath") ||
                                HasSkill("bow_Step6_arrow_rain") ||
                                HasSkill("sword_step5_defswitch") ||
                                HasSkill("spear_Step5_combo") ||
                                HasSkill("mace_Step7_fury_hammer") ||
                                HasSkill("staff_Step6_heal") ||
                                HasSkill("knife_step10_stack_explosion");

            // 1. 석궁: 발칸 아이스
            if (WeaponHelper.IsUsingCrossbow(player))
            {
                if (HasSkill("crossbow_ice_breath"))
                {
                    ActivateCrossbowIceBreath(player);
                    return;
                }
                DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
                return;
            }

            // 2. 활: 화살비
            if (WeaponHelper.IsUsingBow(player))
            {
                if (HasSkill("bow_Step6_arrow_rain"))
                {
                    ExecuteArrowRain(player);
                    return;
                }
                DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
                return;
            }

            // 3. 검/도끼: 회오리베기
            if (WeaponHelper.IsUsingSwordOrAxe(player))
            {
                if (HasSkill("sword_step5_defswitch"))
                {
                    Sword_Skill.ActivateWhirlwindSlash(player);
                    return;
                }
                DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
                return;
            }

            // 4. 창: 연공창
            if (WeaponHelper.IsUsingSpear(player))
            {
                if (HasSkill("spear_Step5_combo"))
                {
                    HandleSpearActiveSkill(player);
                    return;
                }
                DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
                return;
            }

            // 5. 둔기: 분노의 망치
            if (WeaponHelper.IsUsingMace(player))
            {
                if (HasSkill("mace_Step7_fury_hammer"))
                {
                    FuryHammerSkill.HandleHKeyPress(player);
                    return;
                }
                DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
                return;
            }

            // 6. 지팡이: 범위 힐
            if (WeaponHelper.IsUsingStaffOrWand(player))
            {
                if (HasSkill("staff_Step6_heal"))
                {
                    ActivateStaffAreaHeal(player);
                    return;
                }
                DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
                return;
            }

            // 7. 단검: 약점폭발
            if (WeaponHelper.IsUsingDagger(player))
            {
                if (HasSkill("knife_step10_stack_explosion"))
                {
                    KnifeStackExplosion.ActivateStackExplosion(player);
                    return;
                }
                DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
                return;
            }

            // 8. 어떤 무기도 H키 스킬과 매칭 안 됨
            DrawFloatingText(player, hasAnyHSkill ? L.Get("skill_weapon_mismatch") : L.Get("h_key_skill_required"), Color.red);
        }

        /// <summary>
        /// H키 해제 처리 (분노의 망치는 즉시 발동 방식)
        /// </summary>
        public static void HandleHKeyUpSkills(Player player)
        {
            if (player == null || player.IsDead()) return;
            FuryHammerSkill.HandleHKeyRelease(player);
        }

        // === 무기 감지 헬퍼 함수들 - WeaponHelper.cs로 통합됨 ===
        // 모든 무기 감지는 WeaponHelper 클래스 사용:
        // - WeaponHelper.IsUsingBow()
        // - WeaponHelper.IsUsingCrossbow()
        // - WeaponHelper.IsUsingStaffOrWand()
        // - WeaponHelper.IsUsingSword()
        // - WeaponHelper.IsUsingSpear()
        // - WeaponHelper.IsUsingPolearm()
        // - WeaponHelper.IsUsingDagger() / IsUsingKnife()
        // - WeaponHelper.IsUsingMace()
        // - WeaponHelper.IsUsingOneHandedMace()
        // - WeaponHelper.IsUsingTwoHandedMace()
        // - WeaponHelper.HasShield()
    }
}

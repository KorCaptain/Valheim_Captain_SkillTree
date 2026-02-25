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
    /// </summary>
    public static partial class SkillEffect
    {
        // === 액티브 스킬 키 입력 처리 시스템 ===

        public static void HandleActiveSkillInputs(Player player)
        {
            if (player == null || player.IsDead()) return;
            HandleRKeySkills(player);
        }

        /// <summary>
        /// R키: 원거리 액티브 스킬 (석궁, 활, 지팡이)
        /// </summary>
        public static void HandleRKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 1. 석궁 단 한 발
            if (HasSkill("crossbow_Step6_expert") && WeaponHelper.IsUsingCrossbow(player))
            {
                ActivateCrossbowOneShot(player);
                return;
            }

            // 2. 폭발 화살
            if (HasSkill("bow_Step6_critboost") && WeaponHelper.IsUsingBow(player))
            {
                ExecuteExplosiveArrow(player);
                return;
            }

            // 3. 지팡이 이중시전
            if (HasSkill("staff_Step6_dual_cast"))
            {
                if (WeaponHelper.IsUsingStaffOrWand(player))
                {
                    ActivateStaffDualCast(player);
                    return;
                }
                DrawFloatingText(player, L.Get("staff_equip_required"), Color.red);
                return;
            }

            DrawFloatingText(player, L.Get("r_key_skill_condition_not_met"));
        }

        /// <summary>
        /// G키: 보조형 액티브 스킬 (보유 스킬 우선 → 무기 확인)
        /// </summary>
        public static void HandleGKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;

            // === 보유 스킬 우선 라우팅 (스킬을 배웠으면 무기 확인 후 실행) ===

            // 1. 검: 돌진 연속 베기 (sword_step5_finalcut)
            if (HasSkill("sword_step5_finalcut") || HasSkill("sword_slash"))
            {
                if (!Sword_Skill.IsUsingSword(player))
                {
                    DrawFloatingText(player, L.Get("sword_equip_required"), Color.red);
                    return;
                }
                Sword_Skill.ActivateSwordSlash(player);
                return;
            }

            // 3. 단검: 암살자의 심장
            if (HasSkill("knife_step9_assassin_heart"))
            {
                if (!WeaponHelper.IsUsingDagger(player))
                {
                    DrawFloatingText(player, L.Get("dagger_equip_required"), Color.red);
                    return;
                }
                ActivateKnifeAssassinHeart(player);
                return;
            }

            // 4. 지팡이: 범위 힐 - H키로 이동됨

            // 5-1. 창: 꿰뚫는 창 (번개 충격)
            if (HasSkill("spear_Step5_penetrate"))
            {
                if (!WeaponHelper.IsUsingSpear(player))
                {
                    DrawFloatingText(player, L.Get("spear_equip_required"), Color.red);
                    return;
                }
                ActivateSpearPenetrateLightning(player);
                return;
            }

            // 5-2. 창: 연공창 - H키로 이동됨

            // 6. 폴암: 관통 돌격
            if (HasSkill("polearm_step5_king"))
            {
                if (!WeaponHelper.IsUsingPolearm(player))
                {
                    DrawFloatingText(player, L.Get("polearm_equip_required"), Color.red);
                    return;
                }
                UsePolearmPierceChargeSkill(player);
                return;
            }

            // 7. 양손 둔기: 분노의 망치 - H키로 이동됨

            // 8. 방패 + 한손 둔기: 수호자의 진심
            if (HasSkill("mace_Step7_guardian_heart"))
            {
                if (!WeaponHelper.HasShield(player) || !WeaponHelper.IsUsingOneHandedMace(player))
                {
                    DrawFloatingText(player, L.Get("one_hand_mace_shield_required"), Color.red);
                    return;
                }
                ActivateGuardianHeart(player);
                return;
            }

            DrawFloatingText(player, L.Get("g_key_skill_required"), Color.red);
        }

        /// <summary>
        /// G키 해제 처리 (수호자의 진심 등)
        /// </summary>
        public static void HandleGKeyUpSkills(Player player)
        {
            if (player == null || player.IsDead()) return;
            // 분노의 망치가 H키로 이동되어 G키 해제 처리 없음
        }

        /// <summary>
        /// H키: 연공창, 분노의 망치 (보조형 액티브 스킬)
        /// </summary>
        public static void HandleHKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 1. 검: 패링 돌격 (sword_step5_defswitch)
            if (HasSkill("sword_step5_defswitch"))
            {
                if (!Sword_Skill.IsUsingSword(player))
                {
                    DrawFloatingText(player, L.Get("sword_equip_required"), Color.red);
                    return;
                }
                Sword_Skill.ActivateParryRush(player);
                return;
            }

            // 2. 창: 연공창
            if (HasSkill("spear_Step5_combo"))
            {
                if (!WeaponHelper.IsUsingSpear(player))
                {
                    DrawFloatingText(player, L.Get("spear_equip_required"), Color.red);
                    return;
                }
                HandleSpearActiveSkill(player);
                return;
            }

            // 3. 양손 둔기: 분노의 망치
            if (HasSkill("mace_Step7_fury_hammer"))
            {
                if (!WeaponHelper.IsUsingTwoHandedMace(player))
                {
                    DrawFloatingText(player, L.Get("two_hand_mace_required"), Color.red);
                    return;
                }
                FuryHammerSkill.HandleHKeyPress(player);
                return;
            }

            // 4. 지팡이: 범위 힐
            if (HasSkill("staff_Step6_heal"))
            {
                if (!WeaponHelper.IsUsingStaffOrWand(player))
                {
                    DrawFloatingText(player, L.Get("staff_equip_required"), Color.red);
                    return;
                }
                ActivateStaffAreaHeal(player);
                return;
            }

            DrawFloatingText(player, L.Get("h_key_skill_required"), Color.red);
        }

        /// <summary>
        /// H키 해제 처리 (사용하지 않음 - 분노의 망치는 즉시 발동 방식)
        /// </summary>
        public static void HandleHKeyUpSkills(Player player)
        {
            if (player == null || player.IsDead()) return;
            FuryHammerSkill.HandleHKeyRelease(player); // 빈 메서드 (호환성 유지)
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

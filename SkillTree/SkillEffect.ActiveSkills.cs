using System;
using UnityEngine;
using CaptainSkillTree.Gui;

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
            HandleTKeySkills(player);
        }

        /// <summary>
        /// T키: 원거리 액티브 스킬 (석궁, 활, 지팡이)
        /// </summary>
        public static void HandleTKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 1. 석궁 단 한 발
            if (HasSkill("crossbow_Step6_expert") && IsUsingCrossbow(player))
            {
                ActivateCrossbowOneShot(player);
                return;
            }

            // 2. 폭발 화살
            if (HasSkill("bow_Step6_critboost") && IsUsingBow(player))
            {
                ExecuteExplosiveArrow(player);
                return;
            }

            // 3. 지팡이 이중시전
            if (HasSkill("staff_Step6_dual_cast"))
            {
                if (IsUsingStaff(player))
                {
                    ActivateStaffDualCast(player);
                    return;
                }
                DrawFloatingText(player, "지팡이를 장착해야 합니다!", Color.red);
                return;
            }

            DrawFloatingText(player, "T키 액티브 스킬 조건 부족");
        }

        /// <summary>
        /// G키: 보조형 액티브 스킬 (무기별 처리)
        /// </summary>
        public static void HandleGKeySkills(Player player)
        {
            if (player == null || player.IsDead()) return;

            // 1. 지팡이: 범위 힐
            if (IsUsingStaff(player))
            {
                if (HasSkill("staff_Step6_heal"))
                {
                    ActivateStaffAreaHeal(player);
                    return;
                }
                DrawFloatingText(player, "지팡이 힐 스킬이 필요합니다!", Color.red);
                return;
            }

            // 2. 단검: 암살자의 심장
            if (IsUsingDagger(player))
            {
                if (HasSkill("knife_step9_assassin_heart"))
                {
                    ActivateKnifeAssassinHeart(player);
                    return;
                }
                DrawFloatingText(player, "암살자의 심장 스킬이 필요합니다!", Color.red);
                return;
            }

            // 3. 검: Sword Slash
            if (Sword_Skill.IsUsingSword(player))
            {
                if (HasSkill("sword_step5_finalcut") || HasSkill("sword_slash"))
                {
                    Sword_Skill.ActivateSwordSlash(player);
                    return;
                }
                DrawFloatingText(player, "검 액티브 스킬이 필요합니다!", Color.red);
                return;
            }

            // 4. 창: 연공창
            if (IsUsingSpear(player))
            {
                if (HasSkill("spear_Step5_combo"))
                {
                    HandleSpearActiveSkill(player);
                    return;
                }
                DrawFloatingText(player, "창 액티브 스킬이 필요합니다!", Color.red);
                return;
            }

            // 5. 폴암: 장창의 제왕
            if (IsUsingPolearm(player))
            {
                if (HasSkill("polearm_step5_king"))
                {
                    UsePolearmKingSkill(player);
                    return;
                }
                DrawFloatingText(player, "장창의 제왕 스킬이 필요합니다!", Color.red);
                return;
            }

            // 6. 양손 둔기: 분노의 망치
            if (IsUsingTwoHandedMace(player))
            {
                FuryHammerSkill.HandleGKeyPress(player);
                return;
            }

            // 7. 방패 + 한손 둔기: 수호자의 진심
            if (HasShield(player) && IsUsingOneHandedMace(player))
            {
                if (HasSkill("mace_Step7_guardian_heart"))
                {
                    ActivateGuardianHeart(player);
                    return;
                }
                DrawFloatingText(player, "수호자의 진심 스킬이 필요합니다!", Color.red);
                return;
            }

            DrawFloatingText(player, "G키를 지원하는 무기를 착용하세요!", Color.red);
        }

        /// <summary>
        /// G키 해제 처리
        /// </summary>
        public static void HandleGKeyUpSkills(Player player)
        {
            if (player == null || player.IsDead()) return;
            FuryHammerSkill.HandleGKeyRelease(player);
        }

        // === 무기 감지 헬퍼 함수들 ===

        /// <summary>
        /// 활 착용 확인
        /// </summary>
        public static bool IsUsingBow(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            // Valheim 기본 Bows 스킬 타입
            if (weapon.m_shared.m_skillType == Skills.SkillType.Bows)
                return true;

            // 프리팹 이름 확인
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Bow") || prefabName.Contains("bow") ||
                prefabName.Contains("Longbow") || prefabName.Contains("longbow"))
                return true;

            // 무기 이름 확인
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("활") || weaponName.Contains("bow") || weaponName.Contains("longbow"))
                return true;

            return false;
        }

        /// <summary>
        /// 석궁 착용 확인
        /// </summary>
        public static bool IsUsingCrossbow(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            // 프리팹 이름 확인
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Crossbow") || prefabName.Contains("crossbow"))
                return true;

            // 무기 이름 확인
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("석궁") || weaponName.Contains("crossbow"))
                return true;

            // Bows 스킬 타입 + crossbow 이름
            if (weapon.m_shared.m_skillType == Skills.SkillType.Bows &&
                (prefabName.ToLower().Contains("crossbow") || weaponName.Contains("crossbow")))
                return true;

            return false;
        }

        /// <summary>
        /// 지팡이 착용 확인
        /// </summary>
        public static bool IsUsingStaff(Player player)
        {
            if (player == null || player.GetCurrentWeapon() == null) return false;
            var weapon = player.GetCurrentWeapon();

            // Valheim 기본 ElementalMagic 스킬 타입
            if (weapon.m_shared.m_skillType == Skills.SkillType.ElementalMagic)
                return true;

            // 프리팹 이름 확인
            string prefabName = weapon.m_dropPrefab?.name ?? "";
            if (prefabName.Contains("Staff") || prefabName.Contains("staff") ||
                prefabName.Contains("Wand") || prefabName.Contains("wand") ||
                prefabName.Contains("Rod") || prefabName.Contains("rod"))
                return true;

            // 무기 이름 확인
            string weaponName = weapon.m_shared.m_name?.ToLower() ?? "";
            if (weaponName.Contains("지팡이") || weaponName.Contains("staff") ||
                weaponName.Contains("wand") || weaponName.Contains("rod"))
                return true;

            return false;
        }
    }
}

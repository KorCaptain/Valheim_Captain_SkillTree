using HarmonyLib;
using CaptainSkillTree.Localization;
using CaptainSkillTree.SkillTree.CriticalSystem;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 무기 아이템 툴팁 스킬 효과 표시
    ///
    /// 방어구 툴팁(SkillEffect.ArmorTooltip.cs)과 동일한 색상/포맷 규칙 적용.
    /// 기존 데미지 라인을 교체하여 (기본값 + 스킬보너스) × %배율 형태로 표현.
    ///
    /// 색상 규칙:
    ///   orange    → 총합 값
    ///   white     → 기본 weapon 값
    ///   #4FC3F7   → flat 스킬 보너스 (파랑)
    ///   #00BFFF   → % 스킬 보너스 (하늘)
    ///   #808080   → 괄호/연산자 (회색)
    /// </summary>
    public enum WeaponGroup
    {
        None, Knife, Sword, Mace, Spear, Polearm, Bow, Crossbow, Staff
    }

    public struct WeaponBonuses
    {
        public float FlatPierce;
        public float FlatSlash;
        public float FlatBlunt;
        public float FlatAllPhysical;   // 모든 물리 타입 고정값 (atk_base)
        public float FlatAllElemental;  // 모든 속성 타입 고정값 (atk_base elem)
        public float MoveSpeed;         // 이동속도 % (knife_step3)
        public float PctPhysical;   // 물리 데미지 % 배율
        public float PctElemental;  // 속성 데미지 % 배율
        public float AttackSpeed;
        public float CritChance;
        public float CritDamage;
        public float RapidFireChance;     // 연속 발사 Lv1 확률
        public float RapidFireLv2Chance;  // 연속 발사 Lv2 확률
        public float MultishotLv1Chance;  // 멀티샷 Lv1 확률
        public float MultishotLv1Arrows;  // 멀티샷 Lv1 화살 수
        public float MultishotLv2Chance;  // 멀티샷 Lv2 확률
        public float MultishotLv2Arrows;  // 멀티샷 Lv2 화살 수
        public bool HasMeleeExpert;       // 근접 전문가 레이블 표시
        public bool HasRangedExpert;      // 원거리 전문가 레이블 표시
        public bool HasRiposte;           // 칼날 되치기 레이블 표시용
        public float BlockPower;          // 막기 방어력 % 보너스

        public bool HasAny()
        {
            return FlatPierce        > 0.01f || FlatSlash         > 0.01f || FlatBlunt          > 0.01f ||
                   FlatAllPhysical   > 0.01f || FlatAllElemental  > 0.01f || MoveSpeed          > 0.01f ||
                   PctPhysical       > 0.01f || PctElemental      > 0.01f || AttackSpeed        > 0.01f ||
                   CritChance        > 0.01f || CritDamage        > 0.01f ||
                   RapidFireChance   > 0.01f || RapidFireLv2Chance > 0.01f ||
                   MultishotLv1Chance > 0.01f || MultishotLv2Chance > 0.01f ||
                   BlockPower        > 0.01f;
        }
    }

    public static class WeaponTooltipHelper
    {
        private const string COL_TOTAL = "orange";
        private const string COL_BASE  = "white";
        private const string COL_BONUS = "#4FC3F7";
        private const string COL_PCT   = "#00BFFF";
        private const string COL_GRAY  = "#808080";

        // 효과별 색상
        private const string COL_ATK_PHY  = "#FF6B35";  // 물리 공격력 - 주황빨강
        private const string COL_ATK_ELEM = "#C77DFF";  // 속성 공격력 - 보라
        private const string COL_MOVE_SPD = "#80FF80";  // 이동속도    - 연두
        private const string COL_ATK_SPD  = "#FFD700";  // 공격속도    - 금색
        private const string COL_CRIT_CHC = "#FF4D4D";  // 치명타 확률 - 붉은색
        private const string COL_CRIT_DMG = "#FF9900";  // 치명타 피해 - 주황

        // ─────────────────────────────────────────────────────────────────
        // 무기 그룹 판별
        // ─────────────────────────────────────────────────────────────────
        public static WeaponGroup GetWeaponGroup(ItemDrop.ItemData item)
        {
            if (item?.m_shared == null) return WeaponGroup.None;
            switch (item.m_shared.m_skillType)
            {
                case Skills.SkillType.Knives:       return WeaponGroup.Knife;
                case Skills.SkillType.Swords:       return WeaponGroup.Sword;
                case Skills.SkillType.Clubs:        return WeaponGroup.Mace;
                case Skills.SkillType.Spears:       return WeaponGroup.Spear;
                case Skills.SkillType.Polearms:     return WeaponGroup.Polearm;
                case Skills.SkillType.Bows:         return WeaponGroup.Bow;
                case Skills.SkillType.Crossbows:    return WeaponGroup.Crossbow;
                case Skills.SkillType.ElementalMagic:
                case Skills.SkillType.BloodMagic:   return WeaponGroup.Staff;
                case Skills.SkillType.Unarmed:      return WeaponGroup.Knife;
                default:                            return WeaponGroup.None;
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // 무기별 스킬 보너스 수집
        // ─────────────────────────────────────────────────────────────────
        public static WeaponBonuses CollectBonuses(ItemDrop.ItemData item, Player player, WeaponGroup group)
        {
            var b = new WeaponBonuses();

            // 공통: 공격 전문가 트리 % 보너스
            float physPct = 0f;
            float elemPct = 0f;

            if (SkillEffect.HasSkill("attack_root"))
            {
                float pct = Attack_Config.AttackRootDamageBonusValue;
                physPct += pct;
                elemPct += pct;
            }
            if (SkillEffect.HasSkill("atk_twohand_drain"))
            {
                physPct += Attack_Config.AttackTwoHandDrainPhysicalDamageValue;
                elemPct += Attack_Config.AttackTwoHandDrainElementalDamageValue;
            }
            if (SkillEffect.HasSkill("atk_base"))
            {
                b.FlatAllPhysical  += Attack_Config.AttackBasePhysicalDamageValue;
                b.FlatAllElemental += Attack_Config.AttackBaseElementalDamageValue;
            }

            // 공통: 공격속도 상시 패시브
            if (SkillEffect.HasSkill("speed_base")) b.AttackSpeed += Speed_Config.SpeedBaseAttackSpeedValue;
            if (SkillEffect.HasSkill("speed_1"))    b.AttackSpeed += Speed_Config.SpeedDexterityAttackSpeedBonusValue;

            // 공통: 치명타 (Critical 시스템 통합값)
            var skillType = item.m_shared.m_skillType;
            b.CritChance = Critical.CalculateCritChance(player, skillType);
            b.CritDamage = CriticalDamage.CalculateCritDamageMultiplier(player, skillType);

            // 근접 전문가 공통 flat 보너스 (melee_root) — 근접 무기 타입에만 적용
            if (SkillEffect.HasSkill("melee_root") &&
                (group == WeaponGroup.Knife  || group == WeaponGroup.Sword ||
                 group == WeaponGroup.Mace   || group == WeaponGroup.Spear ||
                 group == WeaponGroup.Polearm))
            {
                b.FlatAllPhysical += 3f;
                b.HasMeleeExpert = true;
            }

            // 무기별 보너스
            switch (group)
            {
                case WeaponGroup.Knife:
                    if (SkillEffect.HasSkill("knife_step4_attack_damage"))
                    {
                        b.FlatSlash  += Knife_Config.KnifeAttackDamageBonusValue;
                        b.FlatPierce += Knife_Config.KnifeAttackDamageBonusValue;
                    }
                    if (SkillEffect.HasSkill("knife_step3_move_speed"))
                        b.MoveSpeed += Knife_Config.KnifeMoveSpeedBonusValue;
                    if (SkillEffect.HasSkill("knife_step6_combat_damage"))
                        physPct += Knife_Config.KnifeCombatDamageBonusValue;
                    if (SkillEffect.HasSkill("atk_melee_bonus"))
                        physPct += Attack_Config.AttackMeleeBonusDamageValue;
                    if (SkillEffect.HasSkill("melee_speed1"))
                        b.AttackSpeed += Speed_Config.SpeedMeleeAttackSpeedValue;
                    if (RogueSkills.IsRogue(player))
                        b.AttackSpeed += Rogue_Config.RogueAttackSpeedBonusValue;
                    break;

                case WeaponGroup.Sword:
                    if (SkillEffect.HasSkill("sword_expert"))
                        physPct += Sword_Config.SwordExpertDamageValue;
                    if (SkillEffect.HasSkill("sword_step3_riposte"))
                    {
                        b.FlatSlash += Sword_Config.SwordRiposteDamageBonusValue;
                        b.HasRiposte = true;
                    }
                    if (SkillEffect.HasSkill("sword_step3_allinone") && WeaponHelper.IsUsingTwoHandedSword(player))
                    {
                        physPct += SkillTreeConfig.SwordStep3OffenseDefenseAttackBonusValue;
                        b.BlockPower += Sword_Config.SwordStep3AllInOneDefenseBonusValue;
                    }
                    if (SkillEffect.HasSkill("atk_melee_bonus"))
                        physPct += Attack_Config.AttackMeleeBonusDamageValue;
                    if (SkillEffect.HasSkill("melee_speed1"))
                        b.AttackSpeed += Speed_Config.SpeedMeleeAttackSpeedValue;
                    if (SkillEffect.HasSkill("sword_step1_fastslash"))
                        b.AttackSpeed += Sword_Config.SwordStep1FastSlashSpeedValue;
                    if (SkillEffect.HasSkill("sword_step4_duel"))
                        b.AttackSpeed += Sword_Config.SwordStep4TrueDuelSpeedValue;
                    break;

                case WeaponGroup.Mace:
                    if (SkillEffect.HasSkill("mace_Step1_damage"))
                        physPct += Mace_Config.MaceExpertDamageBonusValue;
                    if (SkillEffect.HasSkill("atk_melee_bonus"))
                        physPct += Attack_Config.AttackMeleeBonusDamageValue;
                    if (SkillEffect.HasSkill("melee_speed1"))
                        b.AttackSpeed += Speed_Config.SpeedMeleeAttackSpeedValue;
                    if (SkillEffect.HasSkill("mace_Step3_branch_heavy"))
                        b.FlatBlunt += Mace_Config.MaceStep3HeavyDamageBonusValue;
                    if (SkillEffect.HasSkill("mace_Step5_dps"))
                    {
                        physPct += Mace_Config.MaceStep5DpsDamageBonusValue;
                    }
                    if (SkillEffect.HasSkill("mace_Step6_grandmaster"))
                    {
                        b.AttackSpeed += Mace_Config.MaceStep6AttackSpeedBonusValue;
                    }
                    break;

                case WeaponGroup.Spear:
                    if (SkillEffect.HasSkill("spear_Step3_pierce"))
                        b.FlatPierce += Spear_Config.SpearStep3PierceDamageBonusValue;
                    if (SkillEffect.HasSkill("spear_expert"))
                        physPct += Spear_Config.SpearStep1DamageBonusValue;
                    if (SkillEffect.HasSkill("atk_melee_bonus"))
                        physPct += Attack_Config.AttackMeleeBonusDamageValue;
                    if (SkillEffect.HasSkill("melee_speed1"))
                        b.AttackSpeed += Speed_Config.SpeedMeleeAttackSpeedValue;
                    break;

                case WeaponGroup.Polearm:
                    if (SkillEffect.HasSkill("atk_melee_bonus"))
                        physPct += Attack_Config.AttackMeleeBonusDamageValue;
                    if (SkillEffect.HasSkill("melee_speed1"))
                        b.AttackSpeed += Speed_Config.SpeedMeleeAttackSpeedValue;
                    break;

                case WeaponGroup.Bow:
                    if (SkillEffect.HasSkill("ranged_root"))
                    {
                        b.FlatPierce += 2f;
                        b.HasRangedExpert = true;
                    }
                    if (SkillEffect.HasSkill("bow_Step3_silentshot"))
                        b.FlatPierce += Bow_Config.BowStep3SilentShotDamageBonusValue;
                    if (SkillEffect.HasSkill("bow_Step1_damage"))
                        physPct += Bow_Config.BowStep1ExpertDamageBonusValue;
                    if (SkillEffect.HasSkill("atk_bow_bonus"))
                        physPct += Attack_Config.AttackBowBonusDamageValue;
                    if (SkillEffect.HasSkill("atk_ranged_enhance"))
                        physPct += Attack_Config.AttackRangedEnhancementValue;
                    if (SkillEffect.HasSkill("bow_draw1"))
                        b.AttackSpeed += Speed_Config.SpeedBowDrawSpeedValue;
                    if (SkillEffect.HasSkill("bow_Step2_multishot"))
                    {
                        b.MultishotLv1Chance = Bow_Config.BowMultishotLv1ChanceValue;
                        b.MultishotLv1Arrows = Bow_Config.BowMultishotArrowCountValue;
                    }
                    if (SkillEffect.HasSkill("bow_Step4_multishot2"))
                    {
                        b.MultishotLv2Chance = Bow_Config.BowMultishotLv2ChanceValue;
                        b.MultishotLv2Arrows = Bow_Config.BowMultishotArrowCountValue;
                    }
                    // 활/석궁은 공통 속도 패시브(speed_base, speed_1) 미적용 (공격속도 표시 제거)
                    b.AttackSpeed -= Speed_Config.SpeedBaseAttackSpeedValue;
                    b.AttackSpeed -= Speed_Config.SpeedDexterityAttackSpeedBonusValue;
                    if (b.AttackSpeed < 0f) b.AttackSpeed = 0f;
                    break;

                case WeaponGroup.Crossbow:
                    if (SkillEffect.HasSkill("ranged_root"))
                    {
                        b.FlatPierce += 2f;
                        b.HasRangedExpert = true;
                    }
                    if (SkillEffect.HasSkill("crossbow_Step1_damage"))
                        physPct += Crossbow_Config.CrossbowExpertDamageBonusValue;
                    if (SkillEffect.HasSkill("atk_crossbow_bonus"))
                        physPct += Attack_Config.AttackCrossbowBonusDamageValue;
                    if (SkillEffect.HasSkill("atk_ranged_enhance"))
                        physPct += Attack_Config.AttackRangedEnhancementValue;
                    if (SkillEffect.HasSkill("crossbow_draw1"))
                        b.AttackSpeed += Speed_Config.SpeedCrossbowDrawSpeedValue;
                    if (SkillEffect.HasSkill("crossbow_Step2_rapid_fire"))
                        b.RapidFireChance = Crossbow_Config.CrossbowRapidFireChanceValue;
                    if (SkillEffect.HasSkill("crossbow_Step4_rapid_fire_lv2"))
                        b.RapidFireLv2Chance = Crossbow_Config.CrossbowRapidFireLv2ChanceValue;
                    // 활/석궁은 공통 속도 패시브(speed_base, speed_1) 미적용 (공격속도 표시 제거)
                    b.AttackSpeed -= Speed_Config.SpeedBaseAttackSpeedValue;
                    b.AttackSpeed -= Speed_Config.SpeedDexterityAttackSpeedBonusValue;
                    if (b.AttackSpeed < 0f) b.AttackSpeed = 0f;
                    break;

                case WeaponGroup.Staff:
                    if (SkillEffect.HasSkill("ranged_root"))
                    {
                        b.FlatAllElemental += 2f;
                        b.HasRangedExpert = true;
                    }
                    if (SkillEffect.HasSkill("staff_Step1_damage"))
                        elemPct += Staff_Config.StaffExpertDamageValue;
                    if (SkillEffect.HasSkill("atk_staff_bonus"))
                        elemPct += Attack_Config.AttackStaffBonusDamageValue;
                    if (SkillEffect.HasSkill("atk_staff_mage"))
                        elemPct += Attack_Config.AttackStaffElementalValue;
                    if (SkillEffect.HasSkill("atk_ranged_enhance"))
                        elemPct += Attack_Config.AttackRangedEnhancementValue;
                    if (SkillEffect.HasSkill("staff_speed1"))
                        b.AttackSpeed += Speed_Config.SpeedStaffCastSpeedFinalValue;
                    break;
            }

            b.PctPhysical  = physPct;
            b.PctElemental = elemPct;
            return b;
        }

        // ─────────────────────────────────────────────────────────────────
        // 패치 전 순수 기본 데미지 계산 (GetDamage() 호출 금지 — 이미 패치됨)
        // ─────────────────────────────────────────────────────────────────
        public static HitData.DamageTypes GetRawBaseDamage(ItemDrop.ItemData item, int qualityLevel)
        {
            var raw = item.m_shared.m_damages;
            if (qualityLevel > 1)
            {
                var perLv = item.m_shared.m_damagesPerLevel;
                int n = qualityLevel - 1;
                raw.m_blunt     += perLv.m_blunt     * n;
                raw.m_slash     += perLv.m_slash     * n;
                raw.m_pierce    += perLv.m_pierce    * n;
                raw.m_chop      += perLv.m_chop      * n;
                raw.m_pickaxe   += perLv.m_pickaxe   * n;
                raw.m_fire      += perLv.m_fire      * n;
                raw.m_frost     += perLv.m_frost     * n;
                raw.m_lightning += perLv.m_lightning * n;
                raw.m_poison    += perLv.m_poison    * n;
                raw.m_spirit    += perLv.m_spirit    * n;
            }
            return raw;
        }

        // ─────────────────────────────────────────────────────────────────
        // 데미지 라인 교체
        // ─────────────────────────────────────────────────────────────────
        public static void ModifyDamageLines(ref string result, WeaponBonuses b, HitData.DamageTypes raw)
        {
            var lines = result.Split('\n');
            bool changed = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string nl = TryReplaceDamageLine(lines[i], raw, b);
                if (nl != null) { lines[i] = nl; changed = true; }
            }

            if (changed) result = string.Join("\n", lines);
        }

        private static string TryReplaceDamageLine(string line, HitData.DamageTypes raw, WeaponBonuses b)
        {
            if (raw.m_pierce    > 0f && line.Contains("$inventory_pierce"))
                return BuildDamageLine(line, raw.m_pierce,    b.FlatPierce + b.FlatAllPhysical, b.PctPhysical);
            if (raw.m_slash     > 0f && line.Contains("$inventory_slash"))
                return BuildDamageLine(line, raw.m_slash,     b.FlatSlash  + b.FlatAllPhysical, b.PctPhysical);
            if (raw.m_blunt     > 0f && line.Contains("$inventory_blunt"))
                return BuildDamageLine(line, raw.m_blunt,     b.FlatBlunt  + b.FlatAllPhysical, b.PctPhysical);
            if (raw.m_fire      > 0f && line.Contains("$inventory_fire"))
                return BuildDamageLine(line, raw.m_fire,      b.FlatAllElemental, b.PctElemental);
            if (raw.m_frost     > 0f && line.Contains("$inventory_frost"))
                return BuildDamageLine(line, raw.m_frost,     b.FlatAllElemental, b.PctElemental);
            if (raw.m_lightning > 0f && line.Contains("$inventory_lightning"))
                return BuildDamageLine(line, raw.m_lightning, b.FlatAllElemental, b.PctElemental);
            if (raw.m_poison    > 0f && line.Contains("$inventory_poison"))
                return BuildDamageLine(line, raw.m_poison,    b.FlatAllElemental, b.PctElemental);
            if (raw.m_spirit    > 0f && line.Contains("$inventory_spirit"))
                return BuildDamageLine(line, raw.m_spirit,    b.FlatAllElemental, b.PctElemental);
            return null;
        }

        /// <summary>
        /// 데미지 라인 포맷 생성
        /// flatOnly:  label <orange>total</orange> <gray>(<white>base</white> + <blue>flat</blue>)</gray>
        /// pctOnly:   label <orange>total</orange> <gray>(<white>base</white> × <sky>+pct%</sky>)</gray>
        /// both:      label <orange>total</orange> <gray>((<white>base</white> + <blue>flat</blue>) × <sky>+pct%</sky>)</gray>
        /// </summary>
        private static string BuildDamageLine(string line, float rawBase, float flatBonus, float pctBonus)
        {
            int colonIdx = line.IndexOf(':');
            if (colonIdx < 0) return line;
            string label = line.Substring(0, colonIdx + 1);

            bool hasFlat = flatBonus > 0.01f;
            bool hasPct  = pctBonus  > 0.01f;
            if (!hasFlat && !hasPct) return line;

            float total = (rawBase + flatBonus) * (1f + pctBonus / 100f);

            if (hasFlat && !hasPct)
            {
                return $"{label} <color={COL_TOTAL}>{total:F0}</color> " +
                       $"<color={COL_GRAY}>(</color>" +
                       $"<color={COL_BASE}>{rawBase:F0}</color>" +
                       $"<color={COL_GRAY}> + </color>" +
                       $"<color={COL_BONUS}>{flatBonus:F0}</color>" +
                       $"<color={COL_GRAY}>)</color>";
            }
            else if (!hasFlat)
            {
                return $"{label} <color={COL_TOTAL}>{total:F0}</color> " +
                       $"<color={COL_GRAY}>(</color>" +
                       $"<color={COL_BASE}>{rawBase:F0}</color>" +
                       $"<color={COL_GRAY}> \u00d7 </color>" +
                       $"<color={COL_PCT}>+{pctBonus:F0}%</color>" +
                       $"<color={COL_GRAY}>)</color>";
            }
            else
            {
                return $"{label} <color={COL_TOTAL}>{total:F0}</color> " +
                       $"<color={COL_GRAY}>(</color>" +
                       $"<color={COL_GRAY}>(</color>" +
                       $"<color={COL_BASE}>{rawBase:F0}</color>" +
                       $"<color={COL_GRAY}> + </color>" +
                       $"<color={COL_BONUS}>{flatBonus:F0}</color>" +
                       $"<color={COL_GRAY}>)</color>" +
                       $"<color={COL_GRAY}> \u00d7 </color>" +
                       $"<color={COL_PCT}>+{pctBonus:F0}%</color>" +
                       $"<color={COL_GRAY}>)</color>";
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // 추가 스탯 라인 (툴팁 맨 뒤에 추가)
        // ─────────────────────────────────────────────────────────────────
        public static void AppendExtraStats(ref string result, WeaponBonuses b)
        {
            if (b.PctPhysical > 0.01f)
                result += $"\n<color={COL_ATK_PHY}>⚔️ {L.Get("weapon_effect_phys_atk")}: +{b.PctPhysical:F0}%</color>";
            if (b.PctElemental > 0.01f)
                result += $"\n<color={COL_ATK_ELEM}>🔥 {L.Get("weapon_effect_elem_atk")}: +{b.PctElemental:F0}%</color>";
            if (b.MoveSpeed > 0.01f)
                result += $"\n<color={COL_MOVE_SPD}>💨 {L.Get("weapon_effect_move_spd")}: +{b.MoveSpeed:F0}%</color>";
            if (b.AttackSpeed > 0.01f)
                result += $"\n<color={COL_ATK_SPD}>⚡ {L.Get("weapon_effect_atk_spd")}: +{b.AttackSpeed:F0}%</color>";
            if (b.MultishotLv1Chance > 0.01f)
                result += $"\n<color={COL_ATK_PHY}>🎯 {L.Get("weapon_effect_multishot_lv1")}:</color> <color=orange>{b.MultishotLv1Chance:F0}%</color> <color=white>{L.Get("weapon_effect_prob")} +{b.MultishotLv1Arrows:F0}{L.Get("weapon_effect_arrows")}</color>";
            if (b.MultishotLv2Chance > 0.01f)
                result += $"\n<color={COL_ATK_PHY}>🎯 {L.Get("weapon_effect_multishot_lv2")}:</color> <color=orange>{b.MultishotLv2Chance:F0}%</color> <color=white>{L.Get("weapon_effect_prob")} +{b.MultishotLv2Arrows:F0}{L.Get("weapon_effect_arrows")}</color>";
            if (b.RapidFireChance > 0.01f)
                result += $"\n<color={COL_ATK_PHY}>🔁 {L.Get("weapon_effect_rapidfire_lv1")}:</color> <color=orange>{b.RapidFireChance:F0}%</color> <color=white>{L.Get("weapon_effect_prob")}</color>";
            if (b.RapidFireLv2Chance > 0.01f)
                result += $"\n<color={COL_ATK_PHY}>🔁 {L.Get("weapon_effect_rapidfire_lv2")}:</color> <color=orange>{b.RapidFireLv2Chance:F0}%</color> <color=white>{L.Get("weapon_effect_prob")}</color>";
            if (b.CritChance > 0.01f)
                result += $"\n<color={COL_CRIT_CHC}>🎯 {L.Get("weapon_effect_crit_chance")}: +{b.CritChance:F0}%</color>";
            if (b.CritDamage > 0.01f)
                result += $"\n<color={COL_CRIT_DMG}>💥 {L.Get("weapon_effect_crit_dmg")}: +{b.CritDamage:F0}%</color>";
            if (b.BlockPower > 0.01f)
                result += $"\n<color=#87CEEB>🛡️ {L.Get("weapon_effect_block_power")}: +{b.BlockPower:F0}%</color>";
            if (b.HasRiposte)
                result += $"\n<color={COL_ATK_PHY}>⚔️ {L.Get("weapon_effect_riposte")}: +{b.FlatSlash:F0} {L.Get("weapon_stat_slash_fixed")}</color>";
            if (b.HasMeleeExpert)
                result += $"\n<color={COL_ATK_PHY}>⚔️ {L.Get("weapon_effect_melee_expert")}: {L.Get("weapon_effect_melee_expert_suffix")}</color>";
            if (b.HasRangedExpert)
                result += $"\n<color={COL_ATK_PHY}>🏹 {L.Get("weapon_effect_ranged_expert")}: {L.Get("weapon_effect_ranged_expert_suffix")}</color>";
        }
    }

    // ─────────────────────────────────────────────────────────────────────
    // Harmony 패치
    // ─────────────────────────────────────────────────────────────────────
    public static partial class SkillEffect
    {
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
            new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
        public static class WeaponTooltip_Patch
        {
            [HarmonyPostfix]
            [HarmonyPriority(Priority.Low)]
            private static void Postfix(ItemDrop.ItemData item, int qualityLevel,
                bool crafting, float worldLevel, int stackOverride, ref string __result)
            {
                try
                {
                    if (crafting) return;
                    if (item?.m_shared == null) return;

                    var player = Player.m_localPlayer;
                    if (player == null) return;

                    // 무기 아이템만 허용 (화이트리스트 방식 - 음식/물약/열쇠 등 비무기 완전 차단)
                    var itemType = item.m_shared.m_itemType;
                    bool isUnarmed = item.m_shared.m_skillType == Skills.SkillType.Unarmed;
                    if (itemType != ItemDrop.ItemData.ItemType.OneHandedWeapon &&
                        itemType != ItemDrop.ItemData.ItemType.TwoHandedWeapon &&
                        itemType != ItemDrop.ItemData.ItemType.Bow             &&
                        itemType != ItemDrop.ItemData.ItemType.Torch           &&
                        !isUnarmed)  // 클로(Unarmed) 타입 예외 허용
                        return;

                    var group = WeaponTooltipHelper.GetWeaponGroup(item);
                    if (group == WeaponGroup.None) return;

                    var bonuses = WeaponTooltipHelper.CollectBonuses(item, player, group);
                    if (!bonuses.HasAny()) return;

                    var rawBase = WeaponTooltipHelper.GetRawBaseDamage(item, qualityLevel);
                    WeaponTooltipHelper.ModifyDamageLines(ref __result, bonuses, rawBase);
                    WeaponTooltipHelper.AppendExtraStats(ref __result, bonuses);
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[무기 툴팁] 패치 오류: {ex.Message}");
                }
            }
        }
    }
}

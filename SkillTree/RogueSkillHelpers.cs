using System;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 로그 스킬 레벨별 헬퍼 메서드 (RogueSkills partial class)
    /// 모든 Lv1~5 스탯 디스패치 담당
    /// </summary>
    public static partial class RogueSkills
    {
        // ====================================================
        // 현재 레벨 조회
        // ====================================================
        public static int GetRogueLevel()
        {
            try { return SkillTreeManager.Instance?.GetSkillLevel("Rogue") ?? 0; }
            catch (Exception) { return 0; }
        }

        // ====================================================
        // 액티브 - 레벨별 스탯 헬퍼
        // ====================================================

        /// <summary>공격력 버프 (%) - 레벨별</summary>
        public static float GetAttackBonusForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2AttackBonusValue,
            3 => Rogue_Config.RogueLv3AttackBonusValue,
            4 => Rogue_Config.RogueLv4AttackBonusValue,
            5 => Rogue_Config.RogueLv5AttackBonusValue,
            _ => Rogue_Config.RogueShadowStrikeAttackBonusValue
        };

        /// <summary>버프 지속시간 (초) - 레벨별</summary>
        public static float GetBuffDurationForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2BuffDurationValue,
            3 => Rogue_Config.RogueLv3BuffDurationValue,
            4 => Rogue_Config.RogueLv4BuffDurationValue,
            5 => Rogue_Config.RogueLv5BuffDurationValue,
            _ => Rogue_Config.RogueShadowStrikeBuffDurationValue
        };

        /// <summary>독 폭발 횟수 - 레벨별</summary>
        public static int GetPoisonBlastsForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2PoisonBlastsValue,
            3 => Rogue_Config.RogueLv3PoisonBlastsValue,
            4 => Rogue_Config.RogueLv4PoisonBlastsValue,
            5 => Rogue_Config.RogueLv5PoisonBlastsValue,
            _ => Rogue_Config.RoguePoisonVFXCountValue
        };

        /// <summary>독 즉시 데미지 - 레벨별</summary>
        public static float GetPoisonInstantForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2PoisonInstantValue,
            3 => Rogue_Config.RogueLv3PoisonInstantValue,
            4 => Rogue_Config.RogueLv4PoisonInstantValue,
            5 => Rogue_Config.RogueLv5PoisonInstantValue,
            _ => Rogue_Config.RoguePoisonInstantDamageValue
        };

        /// <summary>독 DoT 데미지 (초당) - 레벨별</summary>
        public static float GetPoisonDotForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2PoisonDotValue,
            3 => Rogue_Config.RogueLv3PoisonDotValue,
            4 => Rogue_Config.RogueLv4PoisonDotValue,
            5 => Rogue_Config.RogueLv5PoisonDotValue,
            _ => Rogue_Config.RoguePoisonDotDamageValue
        };

        /// <summary>쿨다운 (초) - 레벨별</summary>
        public static float GetCooldownForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2CooldownValue,
            3 => Rogue_Config.RogueLv3CooldownValue,
            4 => Rogue_Config.RogueLv4CooldownValue,
            5 => Rogue_Config.RogueLv5CooldownValue,
            _ => Rogue_Config.RogueShadowStrikeCooldownValue
        };

        /// <summary>충전 횟수 - 레벨별 (Lv5: 기본+보너스)</summary>
        public static int GetChargesForLevel(int lv) => lv >= 5
            ? Rogue_Config.RogueShadowStrikeChargesValue + Rogue_Config.RogueLv5BonusChargesValue
            : Rogue_Config.RogueShadowStrikeChargesValue;

        // ====================================================
        // 패시브 - 레벨별 스탯 헬퍼
        // ====================================================

        /// <summary>공격속도 보너스 (%) - 레벨별</summary>
        public static float GetAttackSpeedForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2AttackSpeedValue,
            3 => Rogue_Config.RogueLv3AttackSpeedValue,
            4 => Rogue_Config.RogueLv4AttackSpeedValue,
            5 => Rogue_Config.RogueLv5AttackSpeedValue,
            _ => Rogue_Config.RogueAttackSpeedBonusValue
        };

        /// <summary>스태미나 소모 감소 (%) - 레벨별</summary>
        public static float GetStaminaReductionForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2StaminaReductionValue,
            3 => Rogue_Config.RogueLv3StaminaReductionValue,
            4 => Rogue_Config.RogueLv4StaminaReductionValue,
            5 => Rogue_Config.RogueLv5StaminaReductionValue,
            _ => Rogue_Config.RogueStaminaReductionValue
        };

        /// <summary>속성 저항 보너스 (%) - 레벨별</summary>
        public static float GetElementalResistForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2ElementalResistValue,
            3 => Rogue_Config.RogueLv3ElementalResistValue,
            4 => Rogue_Config.RogueLv4ElementalResistValue,
            5 => Rogue_Config.RogueLv5ElementalResistValue,
            _ => Rogue_Config.RogueElementalResistanceDebuffValue
        };

        /// <summary>이동속도 보너스 (%) - 레벨별 (Lv1: 0, Lv2~5: 성장)</summary>
        public static float GetMoveSpeedForLevel(int lv) => lv switch
        {
            2 => Rogue_Config.RogueLv2MoveSpeedValue,
            3 => Rogue_Config.RogueLv3MoveSpeedValue,
            4 => Rogue_Config.RogueLv4MoveSpeedValue,
            5 => Rogue_Config.RogueLv5MoveSpeedValue,
            _ => 0f
        };

        // ====================================================
        // 패시브 - 속성별 저항 (레벨 누적)
        // ====================================================

        /// <summary>독 저항 (Lv2부터: +10%, +20%, +30%, +40%)</summary>
        public static float GetRoguePoisonResist(int lv) => lv >= 2 ? (lv - 1) * 10f : 0f;

        /// <summary>냉기 저항 (Lv3부터: +10%, +20%, +30%)</summary>
        public static float GetRogueColdResist(int lv) => lv >= 3 ? (lv - 2) * 10f : 0f;

        /// <summary>화염 저항 (Lv4부터: +10%, +20%)</summary>
        public static float GetRogueFireResist(int lv) => lv >= 4 ? (lv - 3) * 10f : 0f;

        /// <summary>번개 저항 (Lv5: +10%)</summary>
        public static float GetRogueLightningResist(int lv) => lv >= 5 ? 10f : 0f;

        // ====================================================
        // 레벨업 필요 아이템 텍스트
        // ====================================================

        /// <summary>레벨별 필요 아이템 텍스트 (아처와 동일한 트로피 키 사용)</summary>
        public static string GetRogueLevelCostText(int lv)
        {
            try
            {
                return lv switch
                {
                    1 => $"{CaptainSkillTree.Localization.L.Get("item_trophy_greydwarf_brute")} x1 + {CaptainSkillTree.Localization.L.Get("item_eikthyr_trophy")} x1",
                    2 => $"{CaptainSkillTree.Localization.L.Get("item_eikthyr_trophy")} x1 + {CaptainSkillTree.Localization.L.Get("item_elder_trophy")} x1",
                    3 => $"{CaptainSkillTree.Localization.L.Get("item_hatchling_trophy")} x1 + {CaptainSkillTree.Localization.L.Get("item_elder_trophy")} x1 + {CaptainSkillTree.Localization.L.Get("item_bonemass_trophy")} x1",
                    4 => $"{CaptainSkillTree.Localization.L.Get("item_abomination_trophy")} x1 + {CaptainSkillTree.Localization.L.Get("item_bonemass_trophy")} x1 + {CaptainSkillTree.Localization.L.Get("item_dragon_queen_trophy")} x1",
                    5 => $"{CaptainSkillTree.Localization.L.Get("item_dragon_queen_trophy")} x1 + {CaptainSkillTree.Localization.L.Get("item_goblin_king_trophy")} x1 + {CaptainSkillTree.Localization.L.Get("item_seeker_queen_trophy")} x1",
                    _ => ""
                };
            }
            catch (Exception) { return ""; }
        }
    }
}

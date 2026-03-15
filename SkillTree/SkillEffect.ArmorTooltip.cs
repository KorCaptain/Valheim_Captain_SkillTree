using HarmonyLib;
using UnityEngine;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 방어구 아이템 툴팁 패치
    ///
    /// 핵심: GetTooltip 반환값은 로컬라이제이션 전 원시 텍스트
    ///   방패 키: $item_blockarmor  (≠ "가드 방어력")
    ///   방어구 키: $item_armor
    ///   값 형식: <color=orange>114</color> <color=yellow>(118)</color>
    ///
    /// 표시 형식 (로컬라이제이션 후):
    ///   스킬만:     방어력: [주황]22 [회색]([흰색]20 [회색]+ [파랑]2[회색])
    ///   바위피부:   방어력: [주황]24 [회색]([흰색]20[회색] * [하늘]12%[회색])
    ///   둘 다:      방어력: [주황]27 [회색](([흰색]20[회색]+[파랑]2[회색]) * [하늘]12%[회색])
    /// </summary>
    public static partial class SkillEffect
    {
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip),
            new[] { typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int) })]
        public static class ItemData_GetTooltip_ArmorBonus_Patch
        {
            // 색상 상수 (Valheim 원본 스타일 맞춤)
            private const string COL_TOTAL = "orange";    // 총합: 발헤임 기본 값 색상
            private const string COL_BASE  = "white";     // 기본 방어력: 흰색
            private const string COL_BONUS = "#4FC3F7";   // 스킬 보너스: 파란색
            private const string COL_ROCK  = "#00BFFF";   // 바위피부 %: 하늘색
            private const string COL_GRAY  = "#808080";   // 괄호/연산자: 회색

            [HarmonyPostfix]
            [HarmonyPriority(Priority.Low)]
            private static void Postfix(ItemDrop.ItemData item, int qualityLevel, bool crafting,
                float worldLevel, int stackOverride, ref string __result)
            {
                try
                {
                    if (item == null) return;
                    var player = Player.m_localPlayer;
                    if (player == null) return;
                    var manager = SkillTreeManager.Instance;
                    if (manager == null) return;

                    var itemType = item.m_shared.m_itemType;

                    bool rockSkinActive = manager.GetSkillLevel("defense_Step4_tanker") > 0;
                    float rockSkinPct   = Defense_Config.TankerArmorBonusValue;
                    float rockSkinMult  = 1f + rockSkinPct / 100f;

                    float flatBonus      = 0f;
                    float flatBlockBonus = 0f;
                    bool  isShield       = false;

                    switch (itemType)
                    {
                        case ItemDrop.ItemData.ItemType.Helmet:
                            if (manager.GetSkillLevel("defense_root") > 0)
                                flatBonus = Defense_Config.DefenseRootArmorBonusValue;
                            break;
                        case ItemDrop.ItemData.ItemType.Chest:
                            if (manager.GetSkillLevel("defense_Step1_survival") > 0)
                                flatBonus = Defense_Config.SurvivalArmorBonusValue;
                            break;
                        case ItemDrop.ItemData.ItemType.Legs:
                            if (manager.GetSkillLevel("defense_Step2_health") > 0)
                                flatBonus = Defense_Config.HealthArmorBonusValue;
                            break;
                        case ItemDrop.ItemData.ItemType.Shield:
                            isShield = true;
                            if (manager.GetSkillLevel("defense_Step3_shield") > 0)
                                flatBlockBonus += Defense_Config.ShieldTrainingBlockPowerBonusValue;
                            if (manager.GetSkillLevel("defense_Step5_parry") > 0)
                                flatBlockBonus += Defense_Config.ParryMasterBlockPowerBonusValue;
                            break;
                        case ItemDrop.ItemData.ItemType.TwoHandedWeapon:
                        case ItemDrop.ItemData.ItemType.OneHandedWeapon:
                            if (item.m_shared.m_skillType != Skills.SkillType.Swords) return;
                            if (manager.GetSkillLevel("sword_step3_allinone") == 0) return;
                            if (!WeaponHelper.IsUsingTwoHandedSword(player)) return;
                            {
                                float pct = Sword_Config.SwordStep3AllInOneDefenseBonusValue;
                                float mult = 1f + pct / 100f;
                                string[] swordLines = __result.Split('\n');
                                for (int i = 0; i < swordLines.Length; i++)
                                {
                                    if (!swordLines[i].Contains("$item_blockarmor")) continue;
                                    int ci = swordLines[i].IndexOf(':');
                                    if (ci < 0) break;
                                    string lbl = swordLines[i].Substring(0, ci + 1);
                                    float rawBase = item.m_shared.m_blockPower +
                                                    item.m_shared.m_blockPowerPerLevel * (qualityLevel - 1);
                                    swordLines[i] = BuildLine(lbl, rawBase, 0f, true, pct, mult);
                                    break;
                                }
                                __result = string.Join("\n", swordLines);
                            }
                            return;
                        default:
                            return;
                    }

                    // 추가 스킬 체크
                    bool bodyActive         = manager.GetSkillLevel("defense_Step6_body") > 0;
                    bool jotunnShieldActive = manager.GetSkillLevel("defense_Step6_true") > 0;
                    bool boostActive        = manager.GetSkillLevel("defense_Step3_boost") > 0;
                    bool berserkerActive    = manager.GetSkillLevel("Berserker") > 0;

                    float dodgeTotal = 0f;
                    if (manager.GetSkillLevel("defense_Step3_agile") > 0)
                        dodgeTotal += Defense_Config.AgileDodgeBonusValue;
                    if (manager.GetSkillLevel("defense_Step5_stamina") > 0)
                        dodgeTotal += Defense_Config.StaminaDodgeBonusValue;
                    if (manager.GetSkillLevel("defense_Step6_attack") > 0)
                    {
                        bool isInCooldown = SkillEffect.nerveLastEvasionTime.ContainsKey(Player.m_localPlayer) &&
                                            Time.time - SkillEffect.nerveLastEvasionTime[Player.m_localPlayer] < 30f;
                        if (!isInCooldown)
                            dodgeTotal += Defense_Config.AttackDodgeBonusValue;
                    }
                    if (manager.GetSkillLevel("knife_step2_evasion") > 0 && WeaponHelper.IsUsingDagger(player))
                        dodgeTotal += Knife_Config.KnifeEvasionBonusValue;

                    float moveSpeedTotal = 0f;
                    if (manager.GetSkillLevel("speed_root") > 0)
                        moveSpeedTotal += Speed_Config.SpeedRootMoveSpeedValue;
                    if (manager.GetSkillLevel("speed_1") > 0)
                        moveSpeedTotal += Speed_Config.SpeedDexterityMoveSpeedBonusValue;
                    if (manager.GetSkillLevel("knife_step3_move_speed") > 0 && WeaponHelper.IsUsingDagger(player))
                        moveSpeedTotal += Knife_Config.KnifeMoveSpeedBonusValue;

                    // 방패: 항상 처리 (바닐라 yellow "(118)" 제거)
                    // 방어구: 표시할 보너스가 없으면 스킵
                    float physResist = bodyActive ? Defense_Config.BodyArmorBonusValue : 0f;
                    float elemResist = bodyActive ? Defense_Config.BodyArmorBonusValue : 0f;
                    bool mageActive = manager.GetSkillLevel("Mage") > 0;
                    if (mageActive)
                        elemResist += Mage_Config.MageElementalResistanceValue;
                    bool rogueActive = manager.GetSkillLevel("Rogue") > 0;
                    if (rogueActive)
                        elemResist += Rogue_Config.RogueElementalResistanceDebuffValue;
                    bool resistActive = physResist > 0f || elemResist > 0f;

                    if (!isShield)
                    {
                        bool hasBonus = flatBonus != 0f || rockSkinActive || resistActive
                                        || boostActive || berserkerActive || dodgeTotal > 0f
                                        || moveSpeedTotal > 0f;
                        if (!hasBonus) return;
                    }

                    string[] lines = __result.Split('\n');

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];

                        if (isShield)
                        {
                            // $item_blockarmor 키로 감지
                            // 예: "$item_blockarmor: <color=orange>114</color> <color=yellow>(118)</color>"
                            if (!line.Contains("$item_blockarmor")) continue;

                            int colonIdx = line.IndexOf(':');
                            if (colonIdx < 0) break;
                            string label = line.Substring(0, colonIdx + 1);

                            // m_shared 필드에서 언패치된 기본 블록파워 계산
                            float rawBase = item.m_shared.m_blockPower +
                                            item.m_shared.m_blockPowerPerLevel * (qualityLevel - 1);

                            lines[i] = BuildLine(label, rawBase, flatBlockBonus,
                                                  rockSkinActive, rockSkinPct, rockSkinMult);
                            break;
                        }
                        else
                        {
                            // $item_armor 키로 감지
                            // 예: "$item_armor: <color=orange>22</color>"
                            if (!line.Contains("$item_armor")) continue;

                            int colonIdx = line.IndexOf(':');
                            if (colonIdx < 0) break;
                            string label = line.Substring(0, colonIdx + 1);

                            SkillEffect_DefenseTree.ItemData_GetArmor_DefenseTree_Patch.SuppressPatch = true;
                            float baseArmor = item.GetArmor(qualityLevel, worldLevel);
                            SkillEffect_DefenseTree.ItemData_GetArmor_DefenseTree_Patch.SuppressPatch = false;

                            lines[i] = BuildLine(label, baseArmor, flatBonus,
                                                  rockSkinActive, rockSkinPct, rockSkinMult);
                            break;
                        }
                    }

                    // 각반 이동속도 스킬 보너스 → 바닐라 Total 수정
                    if (!isShield && (itemType == ItemDrop.ItemData.ItemType.Legs || itemType == ItemDrop.ItemData.ItemType.Chest) && moveSpeedTotal > 0f)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (!lines[i].Contains("$item_movement_modifier")) continue;
                            const string totalKey = "$item_total:<color=yellow>";
                            int totalIdx = lines[i].IndexOf(totalKey);
                            if (totalIdx < 0) break;
                            int pctStart = totalIdx + totalKey.Length;
                            int pctEnd = lines[i].IndexOf("%", pctStart);
                            if (pctEnd < 0) break;
                            string pctStr = lines[i].Substring(pctStart, pctEnd - pctStart);
                            if (float.TryParse(pctStr, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out float currentTotal))
                            {
                                float newTotal = currentTotal + moveSpeedTotal;
                                lines[i] = lines[i].Substring(0, pctStart) + $"{newTotal:F0}" + lines[i].Substring(pctEnd);
                            }
                            break;
                        }
                    }

                    __result = string.Join("\n", lines);

                    // bonusText 수집
                    string bonusText = "";

                    switch (itemType)
                    {
                        case ItemDrop.ItemData.ItemType.Helmet:
                            if (flatBonus > 0f)
                            {
                                float hp = Defense_Config.DefenseRootHealthBonusValue;
                                bonusText += $"\n<color=#FFD700>🛡️</color><color=white>{L.Get("armor_effect_defense_expert")}</color> : {L.Get("armor_stat_hp")} <color=#4FC3F7>+{hp:F0}</color>, {L.Get("armor_stat_defense")} <color=#4FC3F7>+{flatBonus:F0}</color>";
                            }
                            if (dodgeTotal > 0f)
                                bonusText += $"\n<color=#40E0D0>💨</color><color=white>{L.Get("armor_effect_evasion")}</color> : <color=#00BFFF>+{dodgeTotal:F0}%</color>";
                            if (rockSkinActive)
                                bonusText += $"\n<color=#FF8C00>🪨</color><color=white>{L.Get("armor_effect_rockskin")}</color> : {L.Get("armor_stat_defense")} <color=orange>+{rockSkinPct:F0}%</color>";
                            if (resistActive)
                            {
                                string resistLine = $"\n<color=#E040FB>🔰</color><color=white>{L.Get("armor_effect_resistance")}</color> :";
                                if (physResist > 0f)
                                    resistLine += $" {L.Get("armor_effect_phys_resist")} <color=#4FC3F7>+{physResist:F0}</color>";
                                if (elemResist > 0f)
                                    resistLine += $"{(physResist > 0f ? "," : "")} {L.Get("armor_effect_elem_resist")} <color=#4FC3F7>+{elemResist:F0}</color>";
                                bonusText += resistLine;
                            }
                            break;
                        case ItemDrop.ItemData.ItemType.Chest:
                            if (flatBonus > 0f)
                            {
                                float hp = Defense_Config.SurvivalHealthBonusValue;
                                bonusText += $"\n<color=#00FF00>🪨</color><color=white>{L.Get("armor_effect_skin_hardening")}</color> : {L.Get("armor_stat_hp")} <color=#4FC3F7>+{hp:F0}</color>, {L.Get("armor_stat_defense")} <color=#4FC3F7>+{flatBonus:F0}</color>";
                            }
                            if (moveSpeedTotal > 0f)
                                bonusText += $"\n<color=#ADFF2F>🏃</color><color=white>{L.Get("armor_effect_move_spd")}</color> : <color=#00BFFF>+{moveSpeedTotal:F0}%</color>";
                            if (boostActive)
                                bonusText += $"\n<color=#7FFF00>❤️</color><color=white>{L.Get("armor_effect_health_boost")}</color> : <color=#4FC3F7>+{Defense_Config.BoostHealthBonusValue:F0}</color>";
                            if (berserkerActive)
                                bonusText += $"\n<color=#FF4500>💢</color><color=white>{L.Get("armor_effect_berserker_hp")}</color> : <color=orange>+{Berserker_Config.BerserkerPassiveHealthBonusValue:F0}%</color>";
                            if (dodgeTotal > 0f)
                                bonusText += $"\n<color=#40E0D0>💨</color><color=white>{L.Get("armor_effect_evasion")}</color> : <color=#00BFFF>+{dodgeTotal:F0}%</color>";
                            if (rockSkinActive)
                                bonusText += $"\n<color=#FF8C00>🪨</color><color=white>{L.Get("armor_effect_rockskin")}</color> : {L.Get("armor_stat_defense")} <color=orange>+{rockSkinPct:F0}%</color>";
                            if (resistActive)
                            {
                                string resistLine = $"\n<color=#E040FB>🔰</color><color=white>{L.Get("armor_effect_resistance")}</color> :";
                                if (physResist > 0f)
                                    resistLine += $" {L.Get("armor_effect_phys_resist")} <color=#4FC3F7>+{physResist:F0}</color>";
                                if (elemResist > 0f)
                                    resistLine += $"{(physResist > 0f ? "," : "")} {L.Get("armor_effect_elem_resist")} <color=#4FC3F7>+{elemResist:F0}</color>";
                                bonusText += resistLine;
                            }
                            break;
                        case ItemDrop.ItemData.ItemType.Legs:
                            if (flatBonus > 0f)
                            {
                                float hp = Defense_Config.HealthBonusValue;
                                bonusText += $"\n<color=#00BFFF>💪</color><color=white>{L.Get("armor_effect_body_training")}</color> : {L.Get("armor_stat_hp")} <color=#4FC3F7>+{hp:F0}</color>, {L.Get("armor_stat_defense")} <color=#4FC3F7>+{flatBonus:F0}</color>";
                            }
                            if (moveSpeedTotal > 0f)
                                bonusText += $"\n<color=#ADFF2F>🏃</color><color=white>{L.Get("armor_effect_move_spd")}</color> : <color=#00BFFF>+{moveSpeedTotal:F0}%</color>";
                            if (dodgeTotal > 0f)
                                bonusText += $"\n<color=#40E0D0>💨</color><color=white>{L.Get("armor_effect_evasion")}</color> : <color=#00BFFF>+{dodgeTotal:F0}%</color>";
                            if (rockSkinActive)
                                bonusText += $"\n<color=#FF8C00>🪨</color><color=white>{L.Get("armor_effect_rockskin")}</color> : {L.Get("armor_stat_defense")} <color=orange>+{rockSkinPct:F0}%</color>";
                            if (resistActive)
                            {
                                string resistLine = $"\n<color=#E040FB>🔰</color><color=white>{L.Get("armor_effect_resistance")}</color> :";
                                if (physResist > 0f)
                                    resistLine += $" {L.Get("armor_effect_phys_resist")} <color=#4FC3F7>+{physResist:F0}</color>";
                                if (elemResist > 0f)
                                    resistLine += $"{(physResist > 0f ? "," : "")} {L.Get("armor_effect_elem_resist")} <color=#4FC3F7>+{elemResist:F0}</color>";
                                bonusText += resistLine;
                            }
                            break;
                        case ItemDrop.ItemData.ItemType.Shield:
                            if (manager.GetSkillLevel("defense_Step3_shield") > 0)
                                bonusText += $"\n<color=#00FF00>🛡️</color><color=white>{L.Get("armor_effect_shield_training")}</color> : <color=#4FC3F7>+{Defense_Config.ShieldTrainingBlockPowerBonusValue:F0}</color>";
                            if (manager.GetSkillLevel("defense_Step5_parry") > 0)
                                bonusText += $"\n<color=#00BFFF>⚔️</color><color=white>{L.Get("armor_effect_parry_master")}</color> : {L.Get("armor_stat_parry")} <color=#4FC3F7>+{Defense_Config.ParryMasterParryDurationBonusValue:F0}{L.Get("armor_stat_sec")}</color>, {L.Get("armor_stat_defense")} <color=#4FC3F7>+{Defense_Config.ParryMasterBlockPowerBonusValue:F0}</color>";
                            if (rockSkinActive)
                                bonusText += $"\n<color=#FF8C00>🪨</color><color=white>{L.Get("armor_effect_rockskin")}</color> : <color=orange>+{rockSkinPct:F0}%</color>";
                            if (jotunnShieldActive)
                            {
                                bool isTower = item.m_shared.m_name?.ToLower().Contains("tower") ?? false;
                                float speed  = isTower
                                    ? Defense_Config.JotunnShieldTowerSpeedBonusValue
                                    : Defense_Config.JotunnShieldNormalSpeedBonusValue;
                                bonusText += $"\n<color=#9400D3>✨</color><color=white>{L.Get("armor_effect_jotunn_shield")}</color> : {L.Get("armor_stat_block_stamina")} <color=orange>-{Defense_Config.JotunnShieldBlockStaminaReductionValue:F0}%</color>, {L.Get("armor_effect_move_spd")} <color=#00BFFF>+{speed:F0}%</color>";
                            }
                            break;
                    }

                    if (!string.IsNullOrEmpty(bonusText))
                        __result += bonusText;
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"[방어구 툴팁] 패치 오류: {ex.Message}");
                }
            }

            /// <summary>
            /// 툴팁 라인 생성 (방패/방어구 공통)
            /// 스킬 없음:  label <orange>base</orange>
            /// 스킬:       label <orange>total</orange> <gray>(<white>base</white> <gray>+</gray> <blue>bonus</blue><gray>)</gray>
            /// 바위피부:   label <orange>total</orange> <gray>(<white>base</white> * <sky>pct%</sky><gray>)</gray>
            /// 둘 다:      label <orange>total</orange> <gray>((<white>base</white>+<blue>bonus</blue>) * <sky>pct%</sky><gray>)</gray>
            /// </summary>
            private static string BuildLine(
                string label, float baseVal, float flatBonus,
                bool rockSkin, float rockPct, float rockMult)
            {
                if (flatBonus == 0f && !rockSkin)
                {
                    // 스킬 없음: yellow "(max)" 제거하고 orange 값만
                    return $"{label} <color={COL_TOTAL}>{baseVal:F0}</color>";
                }

                if (rockSkin)
                {
                    float total = (baseVal + flatBonus) * rockMult;
                    string inner;
                    if (flatBonus > 0f)
                    {
                        inner = $"<color={COL_GRAY}>(</color>" +
                                $"<color={COL_BASE}>{baseVal}</color>" +
                                $"<color={COL_GRAY}> + </color>" +
                                $"<color={COL_BONUS}>{flatBonus}</color>" +
                                $"<color={COL_GRAY}>)</color>";
                    }
                    else
                    {
                        inner = $"<color={COL_BASE}>{baseVal}</color>";
                    }
                    return $"{label} <color={COL_TOTAL}>{total:F0}</color> " +
                           $"<color={COL_GRAY}>({inner} * </color>" +
                           $"<color={COL_ROCK}>{rockPct}%</color>" +
                           $"<color={COL_GRAY}>)</color>";
                }
                else
                {
                    float total = baseVal + flatBonus;
                    return $"{label} <color={COL_TOTAL}>{total:F0}</color> " +
                           $"<color={COL_GRAY}>(</color>" +
                           $"<color={COL_BASE}>{baseVal}</color>" +
                           $"<color={COL_GRAY}> + </color>" +
                           $"<color={COL_BONUS}>{flatBonus}</color>" +
                           $"<color={COL_GRAY}>)</color>";
                }
            }
        }
    }
}

using HarmonyLib;
using UnityEngine;

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
                        default:
                            return;
                    }

                    // 방패는 항상 처리 (바닐라 yellow "(118)" 표시 제거)
                    if (!isShield && flatBonus == 0f && !rockSkinActive)
                        return;

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

                    __result = string.Join("\n", lines);
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

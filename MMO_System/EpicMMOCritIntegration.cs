using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using CaptainSkillTree.SkillTree;

namespace CaptainSkillTree.MMO_System
{
    /// <summary>
    /// EpicMMOSystem 자체 치명타(Special/Strength 스탯 기반) 시스템을 CaptainSkillTree의
    /// 통합 치명타 시스템(SkillTree/CriticalSystem)으로 흡수한다.
    ///
    /// EpicMMOSystem의 AddCritDmg 패치(Character.ApplyDamage Prefix, void 반환)는
    /// 협조적으로 비활성화할 수 없어 Harmony.Unpatch로 제거하고,
    /// Special/Strength 원시 포인트만 공개 API(EpicMMOSystem_API.GetAttribute)로 읽어와
    /// CaptainSkillTree 자체 굴림에 합산한다 — 투자한 포인트의 효과는 그대로 유지되고
    /// 굴림/적용 지점만 하나로 통합된다.
    ///
    /// 리플렉션 대상: https://github.com/Wacky-Mole/WackyEpicMMOSystem
    /// (LevelSystem_Strength.cs: AddCritDmg, getAddCriticalChance/Dmg /
    ///  EpicMMOSystem_API.cs: GetAttribute, Attribut enum)
    /// WackyEpicMMOSystem 의존성 버전업 시 위 파일들의 시그니처 변경 여부를 확인할 것.
    /// 상세: md/CRITICAL_SYSTEM_RULES.md
    /// </summary>
    public static class EpicMMOCritIntegration
    {
        /// <summary>
        /// EpicMMOSystem 자체 치명타 Prefix를 Unpatch로 제거하는 데 성공했는지 여부.
        /// false인 경우 EpicMMOSystem이 없거나(정상) 버전 변경으로 탐지 실패(로그 확인 필요).
        /// </summary>
        public static bool IsEpicMmoCritSuppressed { get; private set; } = false;

        /// <summary>
        /// Plugin.Awake()에서 harmony.PatchAll() 이후 1회 호출.
        /// 실패해도 예외를 삼키고 경고 로그만 남긴다 (EpicMMO 자체 치명타는 그대로 유지되어 이중 발동은 발생할 수 있으나 크래시는 없음).
        /// </summary>
        public static void UnpatchEpicMmoCrit(Harmony harmony)
        {
            try
            {
                if (!EpicMMOReflectionHelper.IsInitialized)
                    EpicMMOReflectionHelper.Initialize();

                if (!EpicMMOReflectionHelper.IsAvailable)
                {
                    Plugin.Log?.LogDebug("[EpicMMOCritIntegration] EpicMMOSystem 미탐지 - Unpatch 생략");
                    return;
                }

                var originalMethod = AccessTools.Method(typeof(Character), "ApplyDamage");
                if (originalMethod == null)
                {
                    Plugin.Log?.LogWarning("[EpicMMOCritIntegration] Character.ApplyDamage 메서드를 찾을 수 없음");
                    return;
                }

                var patches = Harmony.GetPatchInfo(originalMethod);
                if (patches == null || patches.Prefixes == null)
                {
                    Plugin.Log?.LogDebug("[EpicMMOCritIntegration] ApplyDamage에 등록된 Prefix 없음");
                    return;
                }

                var epicMmoCritPatch = patches.Prefixes
                    .FirstOrDefault(p => p.PatchMethod?.DeclaringType?.FullName?.Contains("AddCritDmg") == true);

                if (epicMmoCritPatch == null)
                {
                    Plugin.Log?.LogWarning("[EpicMMOCritIntegration] EpicMMOSystem AddCritDmg 패치를 찾지 못함 " +
                        "(EpicMMOSystem 버전이 바뀌었을 수 있음 - github.com/Wacky-Mole/WackyEpicMMOSystem 확인 필요). " +
                        "EpicMMO 자체 치명타가 CaptainSkillTree와 별도로 계속 동작합니다.");
                    return;
                }

                harmony.Unpatch(originalMethod, epicMmoCritPatch.PatchMethod);
                IsEpicMmoCritSuppressed = true;
                Plugin.Log?.LogInfo("[EpicMMOCritIntegration] EpicMMOSystem 자체 치명타 Prefix 제거 완료 - " +
                    "Special/Strength 스탯은 CaptainSkillTree 통합 치명타 시스템으로 흡수됩니다.");
            }
            catch (Exception ex)
            {
                Plugin.Log?.LogWarning($"[EpicMMOCritIntegration] Unpatch 실패 (안전하게 건너뜀): {ex.Message}");
            }
        }

        /// <summary>
        /// Special 스탯 기반 치명타 확률 보너스 (%). EpicMMO 원래 공식과 동일 구조:
        /// Special 포인트 × 레벨당 증가량 + 기본값
        /// </summary>
        public static float GetSpecialCritChanceBonus()
        {
            if (!EpicMMOReflectionHelper.IsAvailable) return 0f;

            int specialPoints = EpicMMOReflectionHelper.GetSpecialPoints();

            float bonus = specialPoints * MMO_CritIntegration_Config.MmoSpecialCritChancePerPointValue
                + MMO_CritIntegration_Config.MmoSpecialCritChanceBaseValue;
            Plugin.Log?.LogDebug($"[공통 치명타] EpicMMO Special({specialPoints}pt) 흡수: +{bonus}%");
            return bonus;
        }

        /// <summary>
        /// Strength 스탯 기반 치명타 피해 보너스 (%). EpicMMO 원래 공식과 동일 구조:
        /// Strength 포인트 × 레벨당 증가량 + 기본값
        /// </summary>
        public static float GetStrengthCritDamageBonus()
        {
            if (!EpicMMOReflectionHelper.IsAvailable) return 0f;

            int strengthPoints = EpicMMOReflectionHelper.GetStrengthPoints();

            float bonus = strengthPoints * MMO_CritIntegration_Config.MmoStrengthCritDamagePerPointValue
                + MMO_CritIntegration_Config.MmoStrengthCritDamageBaseValue;
            Plugin.Log?.LogDebug($"[공통 치명타 피해] EpicMMO Strength({strengthPoints}pt) 흡수: +{bonus}%");
            return bonus;
        }
    }
}

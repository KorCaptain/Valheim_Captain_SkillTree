using BepInEx.Configuration;
using System;
using CaptainSkillTree.Localization;

namespace CaptainSkillTree.SkillTree
{
    /// <summary>
    /// 성기사(Paladin) 직업 스킬 설정
    /// 신성한 치유 - 지속 힐링 스킬의 모든 파라미터 관리
    /// </summary>
    public static class Paladin_Config
    {
        // === 기본 스킬 설정 ===
        public static ConfigEntry<float> PaladinHealCooldown { get; set; }
        public static ConfigEntry<float> PaladinHealRange { get; set; }
        public static ConfigEntry<float> PaladinHealEitrCost { get; set; }
        public static ConfigEntry<float> PaladinHealStaminaCost { get; set; }
        
        // === 힐링 효과 설정 ===
        public static ConfigEntry<float> PaladinHealSelfPercent { get; set; }
        public static ConfigEntry<float> PaladinHealAllyPercentOverTime { get; set; }
        public static ConfigEntry<float> PaladinHealDuration { get; set; }
        public static ConfigEntry<float> PaladinHealInterval { get; set; }
        
        // === 패시브 효과 설정 ===
        public static ConfigEntry<float> PaladinElementalResistanceReduction { get; set; }
        
        /// <summary>
        /// 성기사 컨피그 초기화
        /// </summary>
        public static void InitializePaladinConfig()
        {
            // === 액티브 스킬 설정 ===
            PaladinHealCooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_Cooldown",
                30f,
                "성기사 신성한 치유 쿨타임 (초) / Paladin Holy Healing - Cooldown (sec)"
            );

            PaladinHealRange = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_Range",
                5f,
                "성기사 신성한 치유 범위 (미터) / Paladin Holy Healing - Range (m)"
            );

            PaladinHealEitrCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_EitrCost",
                10f,
                "성기사 신성한 치유 에이트르 소모량 / Paladin Holy Healing - Eitr Cost"
            );

            PaladinHealStaminaCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_StaminaCost",
                10f,
                "성기사 신성한 치유 스태미나 소모량 / Paladin Holy Healing - Stamina Cost"
            );

            // === 액티브 힐링 효과 설정 ===
            PaladinHealSelfPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_SelfHealPercent",
                15f,
                "성기사 자가 치유 비율 (최대 체력의 %) / Paladin Self Heal Ratio (% of Max HP)"
            );

            PaladinHealAllyPercentOverTime = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_AllyHealPercentOverTime",
                2f,
                "성기사 아군 지속 치유 비율 (최대 체력의 %, 매초) / Paladin Ally HoT Ratio (% of Max HP, per sec)"
            );

            PaladinHealDuration = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_Duration",
                10f,
                "성기사 지속 치유 지속 시간 (초) / Paladin HoT Duration (sec)"
            );

            PaladinHealInterval = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Active_Interval",
                1f,
                "성기사 지속 치유 간격 (초) / Paladin HoT Interval (sec)"
            );

            // === 패시브 효과 설정 ===
            PaladinElementalResistanceReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "Paladin_Passive_ElementalResistanceReduction",
                8f,
                "성기사 물리 및 속성 저항 감소 비율 (%) / Paladin Physical & Elemental Resistance Reduction (%)"
            );
            
            // === 이벤트 핸들러 등록 (툴팁 자동 업데이트) ===
            RegisterPaladinEventHandlers();
            
            Plugin.Log.LogDebug("[Paladin Config] 성기사 컨피그 초기화 완료");
        }

        /// <summary>
        /// 성기사 컨피그 변경 시 툴팁 자동 업데이트 이벤트 등록
        /// </summary>
        private static void RegisterPaladinEventHandlers()
        {
            try
            {
                PaladinHealCooldown.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinHealRange.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinHealEitrCost.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinHealStaminaCost.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinHealSelfPercent.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinHealAllyPercentOverTime.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinHealDuration.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinHealInterval.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinElementalResistanceReduction.SettingChanged += (sender, args) => UpdatePaladinTooltip();

                Plugin.Log.LogDebug("[Paladin Config] 이벤트 핸들러 등록 완룄 - 툴팁 자동 업데이트 활성화");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Paladin Config] 이벤트 핸들러 등록 실패: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 성기사 툴팁 업데이트 (컨피그 변경 시 호출)
        /// </summary>
        private static void UpdatePaladinTooltip()
        {
            try
            {
                Plugin.Log.LogInfo("[Paladin Config] UpdatePaladinTooltip 호출됨");
                
                // JobSkills의 UpdatePaladinTooltip 호출
                JobSkills.UpdatePaladinTooltip();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Paladin Config] UpdatePaladinTooltip 실패: {ex.Message}");
            }
        }
        
        // === 편의성 프로퍼티 ===
        
        /// <summary>
        /// 쿨타임 값 (초)
        /// </summary>
        public static float CooldownValue => PaladinHealCooldown?.Value ?? 30f;
        
        /// <summary>
        /// 범위 값 (미터)
        /// </summary>
        public static float RangeValue => PaladinHealRange?.Value ?? 5f;
        
        /// <summary>
        /// 에이트르 소모량
        /// </summary>
        public static float EitrCostValue => PaladinHealEitrCost?.Value ?? 10f;
        
        /// <summary>
        /// 스태미나 소모량
        /// </summary>
        public static float StaminaCostValue => PaladinHealStaminaCost?.Value ?? 10f;
        
        /// <summary>
        /// 자가 치유 비율 (0-1 범위)
        /// </summary>
        public static float SelfHealPercentValue => (PaladinHealSelfPercent?.Value ?? 15f) / 100f;
        
        /// <summary>
        /// 아군 지속 치유 비율 (0-1 범위)
        /// </summary>
        public static float AllyHealPercentValue => (PaladinHealAllyPercentOverTime?.Value ?? 2f) / 100f;
        
        /// <summary>
        /// 지속 치유 지속 시간 (초)
        /// </summary>
        public static float HealDurationValue => PaladinHealDuration?.Value ?? 10f;
        
        /// <summary>
        /// 지속 치유 간격 (초)
        /// </summary>
        public static float HealIntervalValue => PaladinHealInterval?.Value ?? 1f;

        /// <summary>
        /// 물리 및 속성 저항 감소 비율 (%)
        /// </summary>
        public static float ElementalResistanceReductionValue => PaladinElementalResistanceReduction?.Value ?? 8f;
        
        /// <summary>
        /// 총 힐 틱 수 계산
        /// </summary>
        public static int TotalHealTicks => (int)(HealDurationValue / HealIntervalValue);

        /// <summary>
        /// 아군이 받는 총 힐량 비율 계산 (0-1 범위)
        /// </summary>
        public static float TotalAllyHealPercent => AllyHealPercentValue * TotalHealTicks;

        /// <summary>
        /// 힐링 숫자 표시 여부
        /// </summary>
        public static bool ShowHealNumbersValue => true;

        /// <summary>
        /// 힐링 진행 상황 표시 여부
        /// </summary>
        public static bool ShowHealProgressValue => true;
        
        /// <summary>
        /// 성기사 툴팁용 설명 생성 - 동적 연동 (액티브 + 패시브 효과)
        /// </summary>
        public static string GetPaladinTooltip()
        {
            try
            {
                // 컨피그에서 실제 값 가져오기
                var selfHealPercent = PaladinHealSelfPercent?.Value ?? 15f;
                var allyHealPercent = PaladinHealAllyPercentOverTime?.Value ?? 2f;
                var duration = HealDurationValue;
                var interval = HealIntervalValue;
                var resistanceReduction = ElementalResistanceReductionValue;
                var range = RangeValue;
                var cooldown = CooldownValue;
                var staminaCost = StaminaCostValue;
                var eitrCost = EitrCostValue;

                // 메이지 스타일 툴팁 생성
                var tooltip = "";

                // 스킬 이름 (황금색, 크기 22)
                tooltip += $"<color=#FFD700><size=22>{L.Get("paladin_name")}</size></color>\n\n";

                // 설명 섹션
                tooltip += $"<color=#FFD700><size=16>{L.Get("tooltip_description")}: </size></color><color=#E0E0E0><size=16>{L.Get("paladin_tooltip_desc", allyHealPercent, interval, duration, selfHealPercent)}</size></color>\n";

                // 범위 섹션
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_range")}: </size></color><color=#B0E0E6><size=16>{range}m</size></color>\n";

                // 소모 섹션
                tooltip += $"<color=#FFB347><size=16>{L.Get("tooltip_cost")}: </size></color><color=#FFDAB9><size=16>{L.Get("tooltip_stamina")} {staminaCost}, {L.Get("tooltip_eitr")} {eitrCost}</size></color>\n";

                // 스킬유형 섹션
                tooltip += $"<color=#DDA0DD><size=16>{L.Get("tooltip_skill_type")}: </size></color><color=#E6E6FA><size=16>{L.Get("tooltip_job_active_y")}</size></color>\n";

                // 패시브 섹션
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_passive")}: </size></color><color=#ADFF2F><size=16>{L.Get("paladin_passive_resistance", resistanceReduction)}</size></color>\n";

                // 쿨타임 섹션
                tooltip += $"<color=#FFA500><size=16>{L.Get("tooltip_cooldown")}: </size></color><color=#FFDB58><size=16>{cooldown}{L.Get("tooltip_seconds")}</size></color>\n";

                // 필요조건 섹션
                tooltip += $"<color=#98FB98><size=16>{L.Get("tooltip_requirement")}: </size></color><color=#00FF00><size=16>{L.Get("paladin_requirement")}</size></color>\n";

                // 확인사항 섹션
                tooltip += $"<color=#F0E68C><size=16>{L.Get("tooltip_confirmation")}: </size></color><color=#FFE4B5><size=16>{L.Get("tooltip_job_limit")}</size></color>\n";

                // 필요 아이템 섹션
                tooltip += $"<color=#87CEEB><size=16>{L.Get("tooltip_required_item")}: </size></color><color=#FF6B6B><size=16>{L.Get("paladin_required_item")}</size></color>";

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Paladin Config] GetPaladinTooltip 실패: {ex.Message}");
                return L.Get("paladin_tooltip_error");
            }
        }
    }
}
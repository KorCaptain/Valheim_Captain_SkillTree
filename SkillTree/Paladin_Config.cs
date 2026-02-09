using BepInEx.Configuration;
using System;

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
        
        // === VFX 설정 ===
        public static ConfigEntry<float> PaladinHealCircleEffectInterval { get; set; }
        public static ConfigEntry<bool> PaladinShowHealNumbers { get; set; }
        public static ConfigEntry<bool> PaladinShowHealProgress { get; set; }
        
        // === 패시브 효과 설정 ===
        public static ConfigEntry<float> PaladinElementalResistanceReduction { get; set; }
        
        /// <summary>
        /// 성기사 컨피그 초기화
        /// </summary>
        public static void InitializePaladinConfig()
        {
            // === 기본 스킬 설정 ===
            PaladinHealCooldown = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealCooldown",
                30f,
                "성기사 신성한 치유 쿨타임 (초)"
            );
            
            PaladinHealRange = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealRange",
                5f,
                "성기사 신성한 치유 범위 (미터)"
            );
            
            PaladinHealEitrCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealEitrCost",
                10f,
                "성기사 신성한 치유 에이트르 소모량"
            );
            
            PaladinHealStaminaCost = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealStaminaCost",
                10f,
                "성기사 신성한 치유 스태미나 소모량"
            );
            
            // === 힐링 효과 설정 ===
            PaladinHealSelfPercent = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealSelfPercent",
                15f,
                "성기사 자가 치유 비율 (최대 체력의 %)"
            );
            
            PaladinHealAllyPercentOverTime = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealAllyPercentOverTime",
                2f,
                "성기사 아군 지속 치유 비율 (최대 체력의 %, 매초)"
            );
            
            PaladinHealDuration = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealDuration",
                10f,
                "성기사 지속 치유 지속 시간 (초)"
            );
            
            PaladinHealInterval = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealInterval",
                1f,
                "성기사 지속 치유 간격 (초)"
            );
            
            // === VFX 설정 ===
            PaladinHealCircleEffectInterval = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinHealCircleEffectInterval",
                2f,
                "성기사 healing circle 효과 갱신 간격 (초)"
            );
            
            PaladinShowHealNumbers = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinShowHealNumbers",
                true,
                "성기사 힐링 수치 표시 여부"
            );
            
            PaladinShowHealProgress = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinShowHealProgress",
                true,
                "성기사 지속 힐링 진행률 표시 여부 (3/10 형태)"
            );
            
            // === 패시브 효과 설정 ===
            PaladinElementalResistanceReduction = SkillTreeConfig.BindServerSync(Plugin.Instance.Config,
                "Paladin Job Skills",
                "PaladinElementalResistanceReduction",
                8f,
                "성기사 물리 및 속성 저항 감소 비율 (%)"
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
                PaladinHealCircleEffectInterval.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinShowHealNumbers.SettingChanged += (sender, args) => UpdatePaladinTooltip();
                PaladinShowHealProgress.SettingChanged += (sender, args) => UpdatePaladinTooltip();
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
        /// Healing Circle 효과 갱신 간격 (초)
        /// </summary>
        public static float CircleEffectIntervalValue => PaladinHealCircleEffectInterval?.Value ?? 2f;
        
        /// <summary>
        /// 힐링 수치 표시 여부
        /// </summary>
        public static bool ShowHealNumbersValue => PaladinShowHealNumbers?.Value ?? true;
        
        /// <summary>
        /// 지속 힐링 진행률 표시 여부
        /// </summary>
        public static bool ShowHealProgressValue => PaladinShowHealProgress?.Value ?? true;
        
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
                tooltip += $"<color=#FFD700><size=22>성기사</size></color>\n\n";

                // 설명 섹션
                tooltip += $"<color=#FFD700><size=16>설명: </size></color><color=#E0E0E0><size=16>범위 아군 체력 {allyHealPercent}%/{interval}초 ({duration}초), 자가 {selfHealPercent}% 즉시</size></color>\n";

                // 범위 섹션
                tooltip += $"<color=#87CEEB><size=16>범위: </size></color><color=#B0E0E6><size=16>{range}m</size></color>\n";

                // 소모 섹션
                tooltip += $"<color=#FFB347><size=16>소모: </size></color><color=#FFDAB9><size=16>스태미나 {staminaCost}, 에이트르 {eitrCost}</size></color>\n";

                // 스킬유형 섹션
                tooltip += $"<color=#DDA0DD><size=16>스킬유형: </size></color><color=#E6E6FA><size=16>직업 액티브 스킬 - Y키</size></color>\n";

                // 패시브 섹션
                tooltip += $"<color=#98FB98><size=16>패시브: </size></color><color=#ADFF2F><size=16>물리 및 속성 저항 -{resistanceReduction}%</size></color>\n";

                // 쿨타임 섹션
                tooltip += $"<color=#FFA500><size=16>쿨타임: </size></color><color=#FFDB58><size=16>{cooldown}초</size></color>\n";

                // 필요조건 섹션
                tooltip += $"<color=#98FB98><size=16>필요조건: </size></color><color=#00FF00><size=16>한손 근접무기 착용, 성기사 직업</size></color>\n";

                // 확인사항 섹션
                string confirmation = "직업은 1개만 선택가능, 레벨 10 이상";
                tooltip += $"<color=#F0E68C><size=16>확인사항: </size></color><color=#FFE4B5><size=16>{confirmation}</size></color>\n";

                // 필요 아이템 섹션
                string requiredItem = "에이크쉬르 트로피";
                tooltip += $"<color=#87CEEB><size=16>필요 아이템: </size></color><color=#FF6B6B><size=16>{requiredItem}</size></color>";

                return tooltip.TrimEnd('\n');
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[Paladin Config] GetPaladinTooltip 실패: {ex.Message}");
                return "성기사 스킬 정보를 불러올 수 없음";
            }
        }
    }
}
using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetJobDescriptions_KO()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Archer Job (Y-Key Active, 8 keys)
                // ========================================

                // === Archer Job: 멀티샷 액티브 스킬 (6개) ===
                ["Archer_MultiShot_ArrowCount"] =
                "【발사 화살 수】\n" +
                "멀티샷 시 한 번에 발사되는 화살의 개수입니다.\n" +
                "더 많은 화살로 광역 피해를 가합니다.\n" +
                "권장값: 4-7개",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【화살 소모량】\n" +
                "멀티샷 사용 시 소모되는 화살의 개수입니다.\n" +
                "적은 소모로 효율적인 공격이 가능합니다.\n" +
                "권장값: 1-2개",

                ["Archer_MultiShot_DamagePercent"] =
                "【화살당 피해 비율 (%)】\n" +
                "각 화살이 가하는 피해의 비율입니다.\n" +
                "기본 활 공격력 대비 비율입니다.\n" +
                "권장값: 40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "멀티샷 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 25-40초",

                ["Archer_MultiShot_Charges"] =
                "【발사 회수】\n" +
                "멀티샷을 연속으로 사용할 수 있는 횟수입니다.\n" +
                "여러 번 발사하여 화력을 집중할 수 있습니다.\n" +
                "권장값: 2-4회",

                ["Archer_MultiShot_StaminaCost"] =
                "【스태미나 소모】\n" +
                "멀티샷 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 20-35",

                // === Archer Job: 패시브 스킬 (2개) ===
                ["Archer_JumpHeightBonus"] =
                "【점프 높이 보너스 (%)】\n" +
                "기본 점프 높이를 증가시킵니다.\n" +
                "높은 곳에 쉽게 올라갈 수 있습니다.\n" +
                "권장값: 15-25%",

                ["Archer_FallDamageReduction"] =
                "【낙하 피해 감소 (%)】\n" +
                "높은 곳에서 떨어질 때 받는 피해를 감소시킵니다.\n" +
                "아처의 기동성을 강화합니다.\n" +
                "권장값: 40-60%",

                // ========================================
                // Mage Job (Y-Key Active, 6 keys)
                // ========================================

                // === Mage Job: AOE 액티브 스킬 (5개) ===
                ["Mage_AOE_Range"] =
                "【AOE 범위 (m)】\n" +
                "광역 마법 공격의 범위입니다.\n" +
                "넓은 범위로 다수의 적을 공격합니다.\n" +
                "권장값: 10-15m",

                ["Mage_Eitr_Cost"] =
                "【Eitr 소모량】\n" +
                "스킬 사용 시 소모되는 Eitr입니다.\n" +
                "마법 자원 관리가 중요합니다.\n" +
                "권장값: 30-45",

                ["Mage_Damage_Multiplier"] =
                "【공격 피해 배율 (%)】\n" +
                "광역 마법 공격의 피해 배율입니다.\n" +
                "강력한 폭발 마법으로 적을 섬멸합니다.\n" +
                "권장값: 250-350%",

                ["Mage_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "강력한 스킬이므로 긴 쿨타임을 가집니다.\n" +
                "권장값: 150-200초",

                ["Mage_VFX_Name"] =
                "【VFX 효과명】\n" +
                "스킬 사용 시 표시되는 시각 효과 이름입니다.\n" +
                "비워두면 기본 효과를 사용합니다.\n" +
                "권장값: 기본값 사용",

                // === Mage Job: 패시브 스킬 (1개) ===
                ["Mage_Elemental_Resistance"] =
                "【마법 속성 저항 (%)】\n" +
                "화염, 냉기, 번개, 독, 영혼 속성 저항을 증가시킵니다.\n" +
                "물리 피해는 제외되며 마법 피해만 감소합니다.\n" +
                "권장값: 12-20%",

                // === Berserker Job: 패시브 스킬 체력 보너스 ===
                ["berserker_passive_health_bonus"] =
                "【최대 체력 보너스 (%)】\n" +
                "버서커 패시브: 최대 체력을 증가시킵니다.\n" +
                "발헤임 기본 체력 + MMO 스탯 효과 + 모든 체력 증감의 총합 기준으로 비율 적용.\n" +
                "힐링 정상 작동 (m_baseHP에 포함).\n" +
                "권장값: 100%",

                // ========================================
                // Tanker Job Skills (탱커 직업 스킬)
                // ========================================

                // === Tanker Job: 전장의 함성 액티브 (9개) ===
                ["Tanker_Taunt_Cooldown"] =
                "【전장의 함성 쿨타임 (초)】\n" +
                "전장의 함성 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 45-90초",

                ["Tanker_Taunt_StaminaCost"] =
                "【전장의 함성 스태미나 소모】\n" +
                "전장의 함성 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 20-30",

                ["Tanker_Taunt_Range"] =
                "【전장의 함성 도발 범위 (m)】\n" +
                "도발 효과가 적용되는 주변 범위입니다.\n" +
                "권장값: 10-15m",

                ["Tanker_Taunt_Duration"] =
                "【일반 몬스터 도발 지속시간 (초)】\n" +
                "일반 몬스터에게 도발 효과가 지속되는 시간입니다.\n" +
                "권장값: 4-8초",

                ["Tanker_Taunt_BossDuration"] =
                "【보스 도발 지속시간 (초)】\n" +
                "보스 몬스터에게 도발 효과가 지속되는 시간입니다.\n" +
                "보스는 일반보다 짧게 적용됩니다.\n" +
                "권장값: 1-3초",

                ["Tanker_Taunt_DamageReduction"] =
                "【자신 피해 감소 (%)】\n" +
                "전장의 함성 발동 시 자신이 받는 피해 감소량입니다.\n" +
                "권장값: 15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【피해 감소 버프 지속시간 (초)】\n" +
                "피해 감소 버프가 지속되는 시간입니다.\n" +
                "권장값: 4-8초",

                ["Tanker_Taunt_EffectHeight"] =
                "【도발 효과 표시 높이 (m)】\n" +
                "몬스터 머리 위에 표시되는 도발 아이콘의 높이입니다.\n" +
                "권장값: 1.5-2.5m",

                ["Tanker_Taunt_EffectScale"] =
                "【도발 효과 크기 배율】\n" +
                "도발 아이콘의 크기 배율입니다.\n" +
                "권장값: 0.2-0.5",

                // === Tanker Job: 패시브 (1개) ===
                ["Tanker_Passive_DamageReduction"] =
                "【탱커 패시브 피해 감소 (%)】\n" +
                "탱커 직업 패시브: 항상 받는 피해를 감소시킵니다.\n" +
                "권장값: 10-20%",

                // ========================================
                // Rogue Job Skills (로그 직업 스킬)
                // ========================================

                // === Rogue Job: 그림자 일격 액티브 (7개) ===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【그림자 일격 쿨타임 (초)】\n" +
                "그림자 일격 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 20-40초",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【그림자 일격 스태미나 소모】\n" +
                "그림자 일격 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【그림자 일격 공격력 증가 (%)】\n" +
                "그림자 일격 발동 후 버프 지속시간 동안 증가하는 공격력입니다.\n" +
                "권장값: 25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【공격력 버프 지속시간 (초)】\n" +
                "공격력 증가 버프가 지속되는 시간입니다.\n" +
                "권장값: 6-12초",

                ["Rogue_ShadowStrike_SmokeScale"] =
                "【연막 효과 크기 배율】\n" +
                "연막 VFX 효과의 크기 배율입니다.\n" +
                "권장값: 1.5-3.0",

                ["Rogue_ShadowStrike_AggroRange"] =
                "【어그로 제거 범위 (m)】\n" +
                "이 범위 내의 적 어그로를 초기화합니다.\n" +
                "권장값: 10-20m",

                ["Rogue_ShadowStrike_StealthDuration"] =
                "【스텔스 지속시간 (초)】\n" +
                "은신 상태가 지속되는 시간입니다.\n" +
                "권장값: 5-10초",

                // === Rogue Job: 패시브 (3개) ===
                ["Rogue_AttackSpeed_Bonus"] =
                "【공격 속도 보너스 (%)】\n" +
                "로그 직업 패시브: 공격 속도를 증가시킵니다.\n" +
                "권장값: 8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【공격 스태미나 사용 감소 (%)】\n" +
                "로그 직업 패시브: 공격 시 스태미나 소모를 감소시킵니다.\n" +
                "권장값: 10-20%",

                ["Rogue_ElementalResistance_Debuff"] =
                "【속성 저항 증가 (%)】\n" +
                "로그 직업 패시브: 속성 피해에 대한 저항을 증가시킵니다.\n" +
                "권장값: 8-15%",

                // ========================================
                // Paladin Job Skills (성기사 직업 스킬)
                // ========================================

                // === Paladin Job: 신성한 치유 액티브 (8개) ===
                ["Paladin_Active_Cooldown"] =
                "【신성한 치유 쿨타임 (초)】\n" +
                "신성한 치유 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 20-45초",

                ["Paladin_Active_Range"] =
                "【신성한 치유 범위 (m)】\n" +
                "아군 힐링이 적용되는 주변 범위입니다.\n" +
                "권장값: 4-8m",

                ["Paladin_Active_EitrCost"] =
                "【신성한 치유 에이트르 소모량】\n" +
                "신성한 치유 사용 시 소모되는 에이트르입니다.\n" +
                "권장값: 8-15",

                ["Paladin_Active_StaminaCost"] =
                "【신성한 치유 스태미나 소모량】\n" +
                "신성한 치유 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【자가 치유 비율 (최대 체력의 %)】\n" +
                "스킬 발동 시 자신이 회복하는 체력 비율입니다.\n" +
                "권장값: 10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【아군 지속 치유 비율 (최대 체력의 %, 매초)】\n" +
                "범위 내 아군에게 매초 적용되는 지속 힐 비율입니다.\n" +
                "권장값: 1-3%",

                ["Paladin_Active_Duration"] =
                "【지속 치유 시간 (초)】\n" +
                "아군 지속 힐링이 적용되는 총 시간입니다.\n" +
                "권장값: 8-15초",

                ["Paladin_Active_Interval"] =
                "【지속 치유 간격 (초)】\n" +
                "지속 힐링이 적용되는 주기입니다.\n" +
                "권장값: 1초",

                // === Paladin Job: 패시브 (1개) ===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【물리 및 속성 저항 감소 (%)】\n" +
                "성기사 직업 패시브: 물리 및 속성 피해에 대한 저항을 증가시킵니다.\n" +
                "권장값: 5-12%",

                // ========================================
                // Berserker Job Skills (버서커 직업 스킬)
                // ========================================

                // === Berserker Job: 버서커의 분노 액티브 (6개, Beserker 오타 유지) ===
                ["Beserker_Active_Cooldown"] =
                "【버서커의 분노 쿨타임 (초)】\n" +
                "버서커의 분노 스킬의 재사용 대기시간입니다.\n" +
                "권장값: 30-60초",

                ["Beserker_Active_StaminaCost"] =
                "【버서커의 분노 스태미나 소모】\n" +
                "버서커의 분노 발동 시 소모되는 스태미나입니다.\n" +
                "권장값: 15-25",

                ["Beserker_Active_Duration"] =
                "【버서커의 분노 지속시간 (초)】\n" +
                "버서커의 분노 버프가 지속되는 시간입니다.\n" +
                "권장값: 15-25초",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【HP 1%당 피해 증가 (%)】\n" +
                "현재 HP가 낮을수록 더 높은 피해 보너스를 얻습니다.\n" +
                "잃은 HP % × 이 값 = 피해 보너스\n" +
                "권장값: 1.5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【최대 피해 보너스 상한선 (%)】\n" +
                "HP 연동 피해 보너스의 최대 한도입니다.\n" +
                "권장값: 150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【효과 발동 HP 임계값 (%)】\n" +
                "이 HP% 이하일 때 HP 연동 피해 보너스가 활성화됩니다.\n" +
                "100%로 설정하면 항상 활성화됩니다.\n" +
                "권장값: 50-100%",

                // === Berserker Job: 죽음의 도전 패시브 (3개, Beserker 오타 유지) ===
                ["Beserker_Passive_HealthThreshold"] =
                "【패시브 발동 HP 임계값 (%)】\n" +
                "이 HP% 이하로 떨어지면 무적 효과가 발동됩니다.\n" +
                "권장값: 8-15%",

                ["Beserker_Passive_InvincibilityDuration"] =
                "【무적 지속시간 (초)】\n" +
                "패시브 발동 시 무적 상태가 지속되는 시간입니다.\n" +
                "권장값: 5-10초",

                ["Beserker_Passive_Cooldown"] =
                "【패시브 스킬 쿨타임 (초)】\n" +
                "패시브 무적 효과의 재발동 대기시간입니다.\n" +
                "기본값: 180초 (3분)\n" +
                "권장값: 120-300초",
            };
        }

        private static Dictionary<string, string> GetJobDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Archer Job (Y-Key Active, 8 keys)
                // ========================================

                // === Archer Job: Multishot Active Skill (6 keys) ===
                ["Archer_MultiShot_ArrowCount"] =
                "【Arrows to Fire】\n" +
                "Number of arrows fired in a single multishot.\n" +
                "More arrows for area damage.\n" +
                "Recommended: 4-7",

                ["Archer_MultiShot_ArrowConsumption"] =
                "【Arrow Consumption】\n" +
                "Number of arrows consumed per multishot.\n" +
                "Low consumption for efficient attacks.\n" +
                "Recommended: 1-2",

                ["Archer_MultiShot_DamagePercent"] =
                "【Damage Per Arrow (%)】\n" +
                "Damage ratio for each arrow.\n" +
                "Percentage of base bow damage.\n" +
                "Recommended: 40-60%",

                ["Archer_MultiShot_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Multishot reactivation wait time.\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 25-40 sec",

                ["Archer_MultiShot_Charges"] =
                "【Shot Charges】\n" +
                "Number of consecutive multishot uses.\n" +
                "Multiple shots to concentrate firepower.\n" +
                "Recommended: 2-4",

                ["Archer_MultiShot_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using multishot.\n" +
                "Stamina management is important.\n" +
                "Recommended: 20-35",

                // === Archer Job: Passive Skills (2 keys) ===
                ["Archer_JumpHeightBonus"] =
                "【Jump Height Bonus (%)】\n" +
                "Increases base jump height.\n" +
                "Easily reach higher positions.\n" +
                "Recommended: 15-25%",

                ["Archer_FallDamageReduction"] =
                "【Fall Damage Reduction (%)】\n" +
                "Reduces damage from falling.\n" +
                "Enhances archer mobility.\n" +
                "Recommended: 40-60%",

                // ========================================
                // Mage Job (Y-Key Active, 6 keys)
                // ========================================

                // === Mage Job: AOE Active Skill (5 keys) ===
                ["Mage_AOE_Range"] =
                "【AOE Range (m)】\n" +
                "Range of area-of-effect magic attack.\n" +
                "Wide range to hit multiple enemies.\n" +
                "Recommended: 10-15 m",

                ["Mage_Eitr_Cost"] =
                "【Eitr Cost】\n" +
                "Eitr consumed when using skill.\n" +
                "Magic resource management is important.\n" +
                "Recommended: 30-45",

                ["Mage_Damage_Multiplier"] =
                "【Damage Multiplier (%)】\n" +
                "Damage multiplier for AOE magic attack.\n" +
                "Powerful explosive magic to annihilate enemies.\n" +
                "Recommended: 250-350%",

                ["Mage_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reactivation wait time.\n" +
                "Long cooldown due to powerful effect.\n" +
                "Recommended: 150-200 sec",

                ["Mage_VFX_Name"] =
                "【VFX Effect Name】\n" +
                "Visual effect name displayed on skill use.\n" +
                "Leave empty to use default effect.\n" +
                "Recommended: Use default",

                // === Mage Job: Passive Skill (1 key) ===
                ["Mage_Elemental_Resistance"] =
                "【Elemental Resistance (%)】\n" +
                "Increases resistance to Fire, Frost, Lightning, Poison, and Spirit.\n" +
                "Physical damage excluded, only reduces magic damage.\n" +
                "Recommended: 12-20%",

                // === Berserker Job: Passive HP Bonus ===
                ["berserker_passive_health_bonus"] =
                "【Max HP Bonus (%)】\n" +
                "Berserker Passive: Increases maximum health.\n" +
                "Applied as a percentage of total HP (base + MMO stats + all bonuses).\n" +
                "Healing works correctly (included in m_baseHP).\n" +
                "Recommended: 100%",

                // ========================================
                // Tanker Job Skills
                // ========================================

                // === Tanker Job: War Cry Active (9 keys) ===
                ["Tanker_Taunt_Cooldown"] =
                "【War Cry Cooldown (sec)】\n" +
                "Cooldown time before War Cry can be used again.\n" +
                "Recommended: 45-90 sec",

                ["Tanker_Taunt_StaminaCost"] =
                "【War Cry Stamina Cost】\n" +
                "Stamina consumed when activating War Cry.\n" +
                "Recommended: 20-30",

                ["Tanker_Taunt_Range"] =
                "【War Cry Taunt Range (m)】\n" +
                "Radius in which enemies are taunted.\n" +
                "Recommended: 10-15m",

                ["Tanker_Taunt_Duration"] =
                "【Normal Monster Taunt Duration (sec)】\n" +
                "Duration of taunt effect on regular monsters.\n" +
                "Recommended: 4-8 sec",

                ["Tanker_Taunt_BossDuration"] =
                "【Boss Taunt Duration (sec)】\n" +
                "Duration of taunt effect on boss monsters.\n" +
                "Shorter than normal monsters due to boss resistance.\n" +
                "Recommended: 1-3 sec",

                ["Tanker_Taunt_DamageReduction"] =
                "【Self Damage Reduction (%)】\n" +
                "Incoming damage reduction while War Cry buff is active.\n" +
                "Recommended: 15-25%",

                ["Tanker_Taunt_BuffDuration"] =
                "【Damage Reduction Buff Duration (sec)】\n" +
                "Duration of the damage reduction buff after activation.\n" +
                "Recommended: 4-8 sec",

                ["Tanker_Taunt_EffectHeight"] =
                "【Taunt Effect Height (m)】\n" +
                "Height above monster where the taunt icon appears.\n" +
                "Recommended: 1.5-2.5m",

                ["Tanker_Taunt_EffectScale"] =
                "【Taunt Effect Scale】\n" +
                "Size multiplier for the taunt icon visual effect.\n" +
                "Recommended: 0.2-0.5",

                // === Tanker Job: Passive (1 key) ===
                ["Tanker_Passive_DamageReduction"] =
                "【Tanker Passive Damage Reduction (%)】\n" +
                "Tanker passive: Permanently reduces incoming damage.\n" +
                "Recommended: 10-20%",

                // ========================================
                // Rogue Job Skills
                // ========================================

                // === Rogue Job: Shadow Strike Active (7 keys) ===
                ["Rogue_ShadowStrike_Cooldown"] =
                "【Shadow Strike Cooldown (sec)】\n" +
                "Cooldown time before Shadow Strike can be used again.\n" +
                "Recommended: 20-40 sec",

                ["Rogue_ShadowStrike_StaminaCost"] =
                "【Shadow Strike Stamina Cost】\n" +
                "Stamina consumed when activating Shadow Strike.\n" +
                "Recommended: 20-30",

                ["Rogue_ShadowStrike_AttackBonus"] =
                "【Shadow Strike Attack Bonus (%)】\n" +
                "Attack power increase during the buff duration after activation.\n" +
                "Recommended: 25-50%",

                ["Rogue_ShadowStrike_BuffDuration"] =
                "【Attack Buff Duration (sec)】\n" +
                "Duration of the attack power increase buff.\n" +
                "Recommended: 6-12 sec",

                ["Rogue_ShadowStrike_SmokeScale"] =
                "【Smoke Effect Scale】\n" +
                "Size multiplier for the smoke VFX effect.\n" +
                "Recommended: 1.5-3.0",

                ["Rogue_ShadowStrike_AggroRange"] =
                "【Aggro Clear Range (m)】\n" +
                "Resets aggro of all enemies within this range.\n" +
                "Recommended: 10-20m",

                ["Rogue_ShadowStrike_StealthDuration"] =
                "【Stealth Duration (sec)】\n" +
                "Duration of the stealth (hidden) state.\n" +
                "Recommended: 5-10 sec",

                // === Rogue Job: Passive (3 keys) ===
                ["Rogue_AttackSpeed_Bonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Rogue passive: Permanently increases attack speed.\n" +
                "Recommended: 8-15%",

                ["Rogue_Stamina_Reduction"] =
                "【Attack Stamina Usage Reduction (%)】\n" +
                "Rogue passive: Reduces stamina cost on attacks.\n" +
                "Recommended: 10-20%",

                ["Rogue_ElementalResistance_Debuff"] =
                "【Elemental Resistance Increase (%)】\n" +
                "Rogue passive: Increases resistance to elemental damage.\n" +
                "Recommended: 8-15%",

                // ========================================
                // Paladin Job Skills
                // ========================================

                // === Paladin Job: Holy Healing Active (8 keys) ===
                ["Paladin_Active_Cooldown"] =
                "【Holy Healing Cooldown (sec)】\n" +
                "Cooldown time before Holy Healing can be used again.\n" +
                "Recommended: 20-45 sec",

                ["Paladin_Active_Range"] =
                "【Holy Healing Range (m)】\n" +
                "Radius in which allies receive healing.\n" +
                "Recommended: 4-8m",

                ["Paladin_Active_EitrCost"] =
                "【Holy Healing Eitr Cost】\n" +
                "Eitr consumed when activating Holy Healing.\n" +
                "Recommended: 8-15",

                ["Paladin_Active_StaminaCost"] =
                "【Holy Healing Stamina Cost】\n" +
                "Stamina consumed when activating Holy Healing.\n" +
                "Recommended: 8-15",

                ["Paladin_Active_SelfHealPercent"] =
                "【Self Heal Ratio (% of Max HP)】\n" +
                "Percentage of max HP restored to self on activation.\n" +
                "Recommended: 10-20%",

                ["Paladin_Active_AllyHealPercentOverTime"] =
                "【Ally HoT Ratio (% of Max HP, per sec)】\n" +
                "Percentage of max HP restored to each ally per second.\n" +
                "Recommended: 1-3%",

                ["Paladin_Active_Duration"] =
                "【HoT Duration (sec)】\n" +
                "Total duration of the ally heal-over-time effect.\n" +
                "Recommended: 8-15 sec",

                ["Paladin_Active_Interval"] =
                "【HoT Interval (sec)】\n" +
                "Interval between each heal tick.\n" +
                "Recommended: 1 sec",

                // === Paladin Job: Passive (1 key) ===
                ["Paladin_Passive_ElementalResistanceReduction"] =
                "【Physical & Elemental Resistance Bonus (%)】\n" +
                "Paladin passive: Increases resistance to physical and elemental damage.\n" +
                "Recommended: 5-12%",

                // ========================================
                // Berserker Job Skills
                // ========================================

                // === Berserker Job: Berserker Rage Active (6 keys, Beserker typo preserved) ===
                ["Beserker_Active_Cooldown"] =
                "【Berserker Rage Cooldown (sec)】\n" +
                "Cooldown time before Berserker Rage can be used again.\n" +
                "Recommended: 30-60 sec",

                ["Beserker_Active_StaminaCost"] =
                "【Berserker Rage Stamina Cost】\n" +
                "Stamina consumed when activating Berserker Rage.\n" +
                "Recommended: 15-25",

                ["Beserker_Active_Duration"] =
                "【Berserker Rage Duration (sec)】\n" +
                "Duration of the Berserker Rage buff.\n" +
                "Recommended: 15-25 sec",

                ["Beserker_Active_DamagePerHealthPercent"] =
                "【Damage Bonus per 1% HP Lost (%)】\n" +
                "Damage increases as health decreases.\n" +
                "Lost HP % × this value = damage bonus\n" +
                "Recommended: 1.5-3%",

                ["Beserker_Active_MaxDamageBonus"] =
                "【Max Damage Bonus Cap (%)】\n" +
                "Maximum limit for the HP-linked damage bonus.\n" +
                "Recommended: 150-250%",

                ["Beserker_Active_HealthThreshold"] =
                "【HP Threshold for Activation (%)】\n" +
                "HP-linked damage bonus activates below this HP%.\n" +
                "Set to 100% to always activate.\n" +
                "Recommended: 50-100%",

                // === Berserker Job: Death Defiance Passive (3 keys, Beserker typo preserved) ===
                ["Beserker_Passive_HealthThreshold"] =
                "【Passive Trigger HP Threshold (%)】\n" +
                "Invincibility triggers when HP falls below this percentage.\n" +
                "Recommended: 8-15%",

                ["Beserker_Passive_InvincibilityDuration"] =
                "【Invincibility Duration (sec)】\n" +
                "Duration of the invincibility effect when passive triggers.\n" +
                "Recommended: 5-10 sec",

                ["Beserker_Passive_Cooldown"] =
                "【Passive Cooldown (sec)】\n" +
                "Cooldown before the passive invincibility can trigger again.\n" +
                "Default: 180 sec (3 minutes)\n" +
                "Recommended: 120-300 sec",
            };
        }
    }
}

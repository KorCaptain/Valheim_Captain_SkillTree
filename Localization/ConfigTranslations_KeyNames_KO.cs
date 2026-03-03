using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetKoreanKeyNames()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Skill_Tree_Base - 키 바인딩
                // ============================================
                ["HotKey_Y"] = "직업 스킬 키",
                ["HotKey_R"] = "원거리 스킬 키",
                ["HotKey_G"] = "근접 메인 스킬 키",
                ["HotKey_H"] = "보조 스킬 키",
                ["HUD_PosX"] = "HUD X 위치",
                ["HUD_PosY"] = "HUD Y 위치",

                // ============================================
                // Attack Tree (공격 트리) - 33개
                // ============================================


                // === Tier 0: 공격 전문가 (2개) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: [공격 전문가] 모든 공격력 보너스 (%)",
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: [공격 전문가] 필요 포인트",

                // === Tier 1: 기본 공격 강화 (3개) ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] = "Tier 1: [기본 공격] 물리 데미지 보너스 (%)",
                ["Tier1_BaseAttack_ElementalDamageBonus"] = "Tier 1: [기본 공격] 속성 데미지 보너스 (%)",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: [기본 공격] 필요 포인트",

                // === Tier 2: 무기 전문화 (12개) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2-1: [근접 특화] 발동 확률 (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2-1: [근접 특화] 추가 데미지",
                ["Tier2_MeleeSpec_RequiredPoints"] = "Tier 2-1: [근접 특화] 필요 포인트",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2-2: [활 특화] 발동 확률 (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2-2: [활 특화] 추가 데미지",
                ["Tier2_BowSpec_RequiredPoints"] = "Tier 2-2: [활 특화] 필요 포인트",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2-3: [석궁 특화] 발동 확률 (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2-3: [석궁 특화] 추가 데미지",
                ["Tier2_CrossbowSpec_RequiredPoints"] = "Tier 2-3: [석궁 특화] 필요 포인트",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2-4: [지팡이 특화] 발동 확률 (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2-4: [지팡이 특화] 추가 데미지",
                ["Tier2_StaffSpec_RequiredPoints"] = "Tier 2-4: [지팡이 특화] 필요 포인트",

                // === Tier 3: 공격 증가 (3개) ===
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: [공격 증가] 물리 데미지 보너스 (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: [공격 증가] 속성 데미지 보너스 (%)",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: [공격 증가] 필요 포인트",

                // === Tier 4: 전투 강화 (6개) ===
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4-1: [근접 강화] 2연타 콤보 보너스 (%)",
                ["Tier4_MeleeEnhance_RequiredPoints"] = "Tier 4-1: [근접 강화] 필요 포인트",
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4-2: [정밀 공격] 치명타 확률 (%)",
                ["Tier4_PrecisionAttack_RequiredPoints"] = "Tier 4-2: [정밀 공격] 필요 포인트",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4-3: [원거리 강화] 데미지 보너스 (%)",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4-3: [원거리 강화] 필요 포인트",

                // === Tier 5: 특수화 스탯 (2개) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: [특수화 스탯] 무기 특화 보너스",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: [특수화 스탯] 필요 포인트",

                // === Tier 6: 최종 강화 (8개) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6-1: [약점 공격] 치명타 데미지 보너스 (%)",
                ["Tier6_WeakPointAttack_RequiredPoints"] = "Tier 6-1: [약점 공격] 필요 포인트",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6-2: [연속 근접] 3연타 피니셔 보너스 (%)",
                ["Tier6_ComboFinisher_RequiredPoints"] = "Tier 6-2: [연속 근접] 필요 포인트",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6-3: [양손 분쇄] 양손 데미지 보너스 (%)",
                ["Tier6_TwoHandCrush_RequiredPoints"] = "Tier 6-3: [양손 분쇄] 필요 포인트",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6-4: [속성 공격] 속성 데미지 보너스 (%)",
                ["Tier6_ElementalAttack_RequiredPoints"] = "Tier 6-4: [속성 공격] 필요 포인트",

                // ============================================
                // Speed Tree (속도 트리) - 49개
                // ============================================


                // === Tier 0: 속도 전문가 (2개) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: [속도 전문가] 이동속도 보너스 (%)",
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: [속도 전문가] 필요 포인트",

                // === Tier 1: 민첩함의 기초 (5개) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: [민첩함의 기초] 구르기 후 이동속도 보너스 (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: [민첩함의 기초] 버프 지속시간 (초)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: [민첩함의 기초] 공격속도 보너스 (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: [민첩함의 기초] 구르기 속도 보너스 (%)",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: [민첩함의 기초] 필요 포인트",

                // === Tier 2-1: 연속의 흐름 (5개) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2-1: [연속의 흐름] 2연타 시 공격속도 보너스 (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2-1: [연속의 흐름] 스태미나 소모 감소 (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2-1: [연속의 흐름] 버프 지속시간 (초)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2-1: [연속의 흐름] 콤보 속도 보너스 (%)",
                ["Tier2_MeleeFlow_RequiredPoints"] = "Tier 2-1: [연속의 흐름] 필요 포인트",

                // === Tier 2-2: 석궁 숙련자 (4개) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2-2: [석궁 숙련자] 명중 시 이동속도 보너스 (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2-2: [석궁 숙련자] 버프 지속시간 (초)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2-2: [석궁 숙련자] 버프 중 재장전 속도 보너스 (%)",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "Tier 2-2: [석궁 숙련자] 필요 포인트",

                // === Tier 2-3: 활 숙련자 (4개) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2-3: [활 숙련자] 2연타 콤보 시 스태미나 감소 (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2-3: [활 숙련자] 다음 화살 시위 당김 속도 보너스 (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2-3: [활 숙련자] 버프 지속시간 (초)",
                ["Tier2_BowExpert_RequiredPoints"] = "Tier 2-3: [활 숙련자] 필요 포인트",

                // === Tier 2-4: 이동 시전 (4개) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2-4: [이동 시전] 시전 중 이동속도 보너스 (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2-4: [이동 시전] Eitr 소모 감소 (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2-4: [이동 시전] 지팡이 시전 중 이동속도 (%)",
                ["Tier2_MobileCast_RequiredPoints"] = "Tier 2-4: [이동 시전] 필요 포인트",

                // === Tier 3-1: 수련자1 (3개) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3-1: [수련자1] 근접 무기 숙련 보너스",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3-1: [수련자1] 석궁 숙련 보너스",
                ["Tier3_Practitioner1_RequiredPoints"] = "Tier 3-1: [수련자1] 필요 포인트",

                // === Tier 3-2: 수련자2 (3개) ===
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3-2: [수련자2] 지팡이 숙련 보너스",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3-2: [수련자2] 활 숙련 보너스",
                ["Tier3_Practitioner2_RequiredPoints"] = "Tier 3-2: [수련자2] 필요 포인트",

                // === Tier 4-1: 에너자이저 (2개) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4-1: [에너자이저] 음식 소모율 감소 (%)",
                ["Tier4_Energizer_RequiredPoints"] = "Tier 4-1: [에너자이저] 필요 포인트",

                // === Tier 4-2: 선장 (2개) ===
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4-2: [선장] 배 속도 보너스 (%)",
                ["Tier4_Captain_RequiredPoints"] = "Tier 4-2: [선장] 필요 포인트",

                // === Tier 5: 점프 숙련자 (3개) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: [점프 숙련자] 점프 숙련 보너스",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: [점프 숙련자] 점프 스태미나 감소 (%)",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: [점프 숙련자] 필요 포인트",

                // === Tier 6-1: 민첩 (3개) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6-1: [민첩] 근접 공격속도 보너스 (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6-1: [민첩] 이동속도 보너스 (%)",
                ["Tier6_Dexterity_RequiredPoints"] = "Tier 6-1: [민첩] 필요 포인트",

                // === Tier 6-2: 지구력 (2개) ===
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6-2: [지구력] 최대 스태미나 보너스",
                ["Tier6_Endurance_RequiredPoints"] = "Tier 6-2: [지구력] 필요 포인트",

                // === Tier 6-3: 지능 (2개) ===
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6-3: [지능] 최대 Eitr 보너스",
                ["Tier6_Intellect_RequiredPoints"] = "Tier 6-3: [지능] 필요 포인트",

                // === Tier 7: 숙련자 (3개) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: [숙련자] 달리기 숙련 보너스",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: [숙련자] 점프 숙련 보너스",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: [숙련자] 필요 포인트",

                // === Tier 8-1: 근접 가속 (3개) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8-1: [근접 가속] 근접 공격속도 보너스 (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8-1: [근접 가속] 3연타 시 다음 공격 속도 보너스 (%)",
                ["Tier8_MeleeAccel_RequiredPoints"] = "Tier 8-1: [근접 가속] 필요 포인트",

                // === Tier 8-2: 석궁 가속 (3개) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8-2: [석궁 가속] 재장전 속도 보너스 (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8-2: [석궁 가속] 재장전 중 이동속도 (%)",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "Tier 8-2: [석궁 가속] 필요 포인트",

                // === Tier 8-3: 활 가속 (3개) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8-3: [활 가속] 시위 당김 속도 보너스 (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8-3: [활 가속] 시위 당기는 중 이동속도 (%)",
                ["Tier8_BowAccel_RequiredPoints"] = "Tier 8-3: [활 가속] 필요 포인트",

                // === Tier 8-4: 시전 가속 (3개) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8-4: [시전 가속] 마법 공격속도 보너스 (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8-4: [시전 가속] 3연타 시 Eitr 최대 회복률 (%)",
                ["Tier8_CastAccel_RequiredPoints"] = "Tier 8-4: [시전 가속] 필요 포인트",

                // ============================================
                // Defense Tree (방어 트리) - 59개
                // ============================================

                // === Tier 0: 방어 전문가 (3개) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: [방어 전문가] 체력 보너스",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: [방어 전문가] 방어력 보너스",
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: [방어 전문가] 필요 포인트",

                // === Tier 1: 피부 경화 (3개) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: [피부 경화] 체력 보너스",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: [피부 경화] 방어력 보너스",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: [피부 경화] 필요 포인트",

                // === Tier 2-1: 심신단련 (3개) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2-1: [심신단련] 최대 스태미나 보너스",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2-1: [심신단련] 최대 Eitr 보너스",
                ["Tier2_MindTraining_RequiredPoints"] = "Tier 2-1: [심신단련] 필요 포인트",

                // === Tier 2-2: 체력단련 (3개) ===
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2-2: [체력단련] 체력 보너스",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2-2: [체력단련] 방어력 보너스",
                ["Tier2_HealthTraining_RequiredPoints"] = "Tier 2-2: [체력단련] 필요 포인트",

                // === Tier 3-1: 단전호흡 (2개) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3-1: [단전호흡] Eitr 보너스",
                ["Tier3_CoreBreathing_RequiredPoints"] = "Tier 3-1: [단전호흡] 필요 포인트",

                // === Tier 3-2: 회피단련 (3개) ===
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3-2: [회피단련] 회피율 보너스 (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3-2: [회피단련] 구르기 무적시간 증가 (%)",
                ["Tier3_EvasionTraining_RequiredPoints"] = "Tier 3-2: [회피단련] 필요 포인트",

                // === Tier 3-3: 체력증강 (2개) ===
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3-3: [체력증강] 체력 보너스",
                ["Tier3_HealthBoost_RequiredPoints"] = "Tier 3-3: [체력증강] 필요 포인트",

                // === Tier 3-4: 방패훈련 (2개) ===
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3-4: [방패훈련] 방패 방어력 보너스",
                ["Tier3_ShieldTraining_RequiredPoints"] = "Tier 3-4: [방패훈련] 필요 포인트",

                // === Tier 4-1: 충격파방출 (4개) ===
                ["Tier4_Shockwave_Radius"] = "Tier 4-1: [충격파방출] 효과 범위",
                ["Tier4_Shockwave_StunDuration"] = "Tier 4-1: [충격파방출] 기절 지속시간",
                ["Tier4_Shockwave_Cooldown"] = "Tier 4-1: [충격파방출] 쿨타임",
                ["Tier4_Shockwave_RequiredPoints"] = "Tier 4-1: [충격파방출] 필요 포인트",

                // === Tier 4-2: 발구르기 (6개) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4-2: [발구르기] 효과 반경 (미터)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4-2: [발구르기] 넉백 강도",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4-2: [발구르기] 쿨타임 (초)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4-2: [발구르기] 자동 발동 체력 임계값",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4-2: [발구르기] VFX 지속시간 (초)",
                ["Tier4_GroundStomp_RequiredPoints"] = "Tier 4-2: [발구르기] 필요 포인트",

                // === Tier 4-3: 바위피부 (2개) ===
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4-3: [바위피부] 방어력 증폭 (%)",
                ["Tier4_RockSkin_RequiredPoints"] = "Tier 4-3: [바위피부] 필요 포인트",

                // === Tier 5-1: 지구력 (3개) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5-1: [지구력] 달리기 스태미나 감소 (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5-1: [지구력] 점프 스태미나 감소 (%)",
                ["Tier5_Endurance_RequiredPoints"] = "Tier 5-1: [지구력] 필요 포인트",

                // === Tier 5-2: 기민함 (3개) ===
                ["Tier5_Agility_DodgeBonus"] = "Tier 5-2: [기민함] 회피율 보너스 (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5-2: [기민함] 구르기 스태미나 감소 (%)",
                ["Tier5_Agility_RequiredPoints"] = "Tier 5-2: [기민함] 필요 포인트",

                // === Tier 5-3: 트롤의 재생력 (3개) ===
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5-3: [트롤의 재생력] 체력 재생 보너스 (초당)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5-3: [트롤의 재생력] 재생 간격 (초)",
                ["Tier5_TrollRegen_RequiredPoints"] = "Tier 5-3: [트롤의 재생력] 필요 포인트",

                // === Tier 5-4: 막기달인 (3개) ===
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5-4: [막기달인] 방패 방어력 보너스",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5-4: [막기달인] 패링 지속시간 보너스 (초)",
                ["Tier5_BlockMaster_RequiredPoints"] = "Tier 5-4: [막기달인] 필요 포인트",

                // === Tier 6-1: 마인드쉴드 (1개) ===
                ["Tier6_MindShield_RequiredPoints"] = "Tier 6-1: [마인드쉴드] 필요 포인트",

                // === Tier 6-2: 신경강화 (2개) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6-2: [신경강화] 회피율 영구 보너스 (%)",
                ["Tier6_NerveEnhancement_RequiredPoints"] = "Tier 6-2: [신경강화] 필요 포인트",

                // === Tier 6-3: 이단점프 (1개) ===
                ["Tier6_DoubleJump_RequiredPoints"] = "Tier 6-3: [이단점프] 필요 포인트",

                // === Tier 6-4: 요툰의 생명력 (3개) ===
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6-4: [요툰의 생명력] 체력 보너스 (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6-4: [요툰의 생명력] 물리/속성 저항 (%)",
                ["Tier6_JotunnVitality_RequiredPoints"] = "Tier 6-4: [요툰의 생명력] 필요 포인트",

                // === Tier 6-5: 요툰의 방패 (4개) ===
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6-5: [요툰의 방패] 방어 스태미나 감소 (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6-5: [요툰의 방패] 일반 방패 이동속도 보너스 (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6-5: [요툰의 방패] 타워 실드 이동속도 보너스 (%)",
                ["Tier6_JotunnShield_RequiredPoints"] = "Tier 6-5: [요툰의 방패] 필요 포인트",

                // ============================================
                // Production Tree (생산 트리) - 22개
                // ============================================


                // === Tier 0: 생산 전문가 (1개) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] = "Tier 0: 나무 +1 보너스 확률 (%)",

                // === Tier 1: 초보 일꾼 (1개) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] = "Tier 1: 나무 +1 보너스 확률 (%)",

                // === Tier 2: 전문 분야 (5개) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] = "Tier 2: 벌목 Lv2 - 나무 +1 보너스 확률 (%)",
                ["Tier2_GatheringLv2_BonusChance"] = "Tier 2: 채집 Lv2 - 아이템 +1 보너스 확률 (%)",
                ["Tier2_MiningLv2_BonusChance"] = "Tier 2: 채광 Lv2 - 광석 +1 보너스 확률 (%)",
                ["Tier2_CraftingLv2_UpgradeChance"] = "Tier 2: 제작 Lv2 - 업그레이드 +1 보너스 확률 (%)",
                ["Tier2_CraftingLv2_DurabilityBonus"] = "Tier 2: 제작 Lv2 - 내구도 최대치 증가 (%)",

                // === Tier 3: 중급 스킬 (5개) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] = "Tier 3: 벌목 Lv3 - 나무 +2 보너스 확률 (%)",
                ["Tier3_GatheringLv3_BonusChance"] = "Tier 3: 채집 Lv3 - 아이템 +1 보너스 확률 (%)",
                ["Tier3_MiningLv3_BonusChance"] = "Tier 3: 채광 Lv3 - 광석 +1 보너스 확률 (%)",
                ["Tier3_CraftingLv3_UpgradeChance"] = "Tier 3: 제작 Lv3 - 업그레이드 +1 보너스 확률 (%)",
                ["Tier3_CraftingLv3_DurabilityBonus"] = "Tier 3: 제작 Lv3 - 내구도 최대치 증가 (%)",

                // === Tier 4: 고급 스킬 (5개) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] = "Tier 4: 벌목 Lv4 - 나무 +2 보너스 확률 (%)",
                ["Tier4_GatheringLv4_BonusChance"] = "Tier 4: 채집 Lv4 - 아이템 +1 보너스 확률 (%)",
                ["Tier4_MiningLv4_BonusChance"] = "Tier 4: 채광 Lv4 - 광석 +1 보너스 확률 (%)",
                ["Tier4_CraftingLv4_UpgradeChance"] = "Tier 4: 제작 Lv4 - 업그레이드 +1 보너스 확률 (%)",
                ["Tier4_CraftingLv4_DurabilityBonus"] = "Tier 4: 제작 Lv4 - 내구도 최대치 증가 (%)",

                // === Production Tree: RequiredPoints (14개) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: [생산 전문가] 필요 포인트",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: [초보 일꾼] 필요 포인트",
                ["Tier2_WoodcuttingLv2_RequiredPoints"] = "Tier 2: [벌목 Lv2] 필요 포인트",
                ["Tier2_GatheringLv2_RequiredPoints"] = "Tier 2: [채집 Lv2] 필요 포인트",
                ["Tier2_MiningLv2_RequiredPoints"] = "Tier 2: [채광 Lv2] 필요 포인트",
                ["Tier2_CraftingLv2_RequiredPoints"] = "Tier 2: [제작 Lv2] 필요 포인트",
                ["Tier3_WoodcuttingLv3_RequiredPoints"] = "Tier 3: [벌목 Lv3] 필요 포인트",
                ["Tier3_GatheringLv3_RequiredPoints"] = "Tier 3: [채집 Lv3] 필요 포인트",
                ["Tier3_MiningLv3_RequiredPoints"] = "Tier 3: [채광 Lv3] 필요 포인트",
                ["Tier3_CraftingLv3_RequiredPoints"] = "Tier 3: [제작 Lv3] 필요 포인트",
                ["Tier4_WoodcuttingLv4_RequiredPoints"] = "Tier 4: [벌목 Lv4] 필요 포인트",
                ["Tier4_GatheringLv4_RequiredPoints"] = "Tier 4: [채집 Lv4] 필요 포인트",
                ["Tier4_MiningLv4_RequiredPoints"] = "Tier 4: [채광 Lv4] 필요 포인트",
                ["Tier4_CraftingLv4_RequiredPoints"] = "Tier 4: [제작 Lv4] 필요 포인트",

                // ============================================
                // Bow Tree (활 트리) - 34개
                // ============================================


                // === Tier 0: 활 전문가 (2개) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: [활 전문가] 활 공격력 보너스 (%)",
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: [활 전문가] 필요 포인트",

                // === Tier 1-1: [집중 사격] (2개) ===
                ["Tier1_FocusedShot_CritBonus"] = "Tier 1-1: [집중 사격] 치명타 확률 보너스 (%)",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1-1: [집중 사격] 필요 포인트",

                // === Tier 1-2: [멀티샷 Lv1] (5개) ===
                ["Tier1_MultishotLv1_ActivationChance"] = "Tier 1-2: [멀티샷 Lv1] 발동 확률 (%)",
                ["Tier1_MultishotLv1_AdditionalArrows"] = "Tier 1-2: [멀티샷 Lv1] 추가 화살 수",
                ["Tier1_MultishotLv1_ArrowConsumption"] = "Tier 1-2: [멀티샷 Lv1] 화살 소모량",
                ["Tier1_MultishotLv1_DamagePerArrow"] = "Tier 1-2: [멀티샷 Lv1] 화살당 데미지 (%)",
                ["Tier1_MultishotLv1_RequiredPoints"] = "Tier 1-2: [멀티샷 Lv1] 필요 포인트",

                // === Tier 2: [활 숙련] (3개) ===
                ["Tier2_BowMastery_SkillBonus"] = "Tier 2: [활 숙련] 활 기술 보너스",
                ["Tier2_BowMastery_SpecialArrowChance"] = "Tier 2: [활 숙련] 특수 화살 발동 확률 (%)",
                ["Tier2_BowMastery_RequiredPoints"] = "Tier 2: [활 숙련] 필요 포인트",

                // === Tier 3-1: [침묵의 일격] (2개) ===
                ["Tier3_SilentStrike_DamageBonus"] = "Tier 3-1: [침묵의 일격] 공격력 증가",
                ["Tier3_SilentStrike_RequiredPoints"] = "Tier 3-1: [침묵의 일격] 필요 포인트",

                // === Tier 3-2: [멀티샷 Lv2] (2개) ===
                ["Tier3_MultishotLv2_ActivationChance"] = "Tier 3-2: [멀티샷 Lv2] 발동 확률 (%)",
                ["Tier3_MultishotLv2_RequiredPoints"] = "Tier 3-2: [멀티샷 Lv2] 필요 포인트",

                // === Tier 3-3: [사냥 본능] (2개) ===
                ["Tier3_HuntingInstinct_CritBonus"] = "Tier 3-3: [사냥 본능] 치명타 확률 (%)",
                ["Tier3_HuntingInstinct_RequiredPoints"] = "Tier 3-3: [사냥 본능] 필요 포인트",

                // === Tier 4: [정조준] (2개) ===
                ["Tier4_PrecisionAim_CritDamage"] = "Tier 4: [정조준] 치명타 데미지 보너스 (%)",
                ["Tier4_PrecisionAim_RequiredPoints"] = "Tier 4: [정조준] 필요 포인트",

                // === Tier 5: [폭발 화살] (10개) ===
                ["Tier5_ExplosiveArrow_DamageMultiplier"] = "Tier 5: [폭발 화살] 데미지 배율 (%)",
                ["Tier5_ExplosiveArrow_Radius"] = "Tier 5: [폭발 화살] 폭발 범위 (m)",
                ["Tier5_ExplosiveArrow_Cooldown"] = "Tier 5: [폭발 화살] 쿨타임 (초)",
                ["Tier5_ExplosiveArrow_StaminaCost"] = "Tier 5: [폭발 화살] 스태미나 소모 (%)",
                ["Tier5_ExplosiveArrow_RequiredPoints"] = "Tier 5: [폭발 화살] 필요 포인트",

                // ============================================
                // Sword Tree (검 트리) - 30개
                // ============================================


                // === Sword Tree: 검 전문가 (1개) ===
                ["Sword_Expert_DamageIncrease"] = "Tier 0: 검 공격력 증가 (%)",

                // === Sword Tree: 빠른 베기 (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: 공격속도 보너스 (%)",

                // === Sword Tree: 연속 베기 (2개) ===
                ["Sword_ComboSlash_Bonus"] = "Tier 2: 연속 공격 보너스 (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: 버프 지속시간 (초)",

                // === Sword Tree: 칼날 되치기 (1개) ===
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: 공격력 보너스",

                // === Sword Tree: 진검승부 (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: 공격속도 보너스 (%)",

                // === Sword Tree: 패링 돌격 (5개) ===
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: 버프 지속시간 (초)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: 돌격 공격력 보너스 (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: 밀어내기 거리 (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: 스태미나 소모",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: 쿨타임 (초)",

                // === Sword Tree: 돌진 연속 베기 (8개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: 1차 공격력 비율 (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: 2차 공격력 비율 (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: 3차 공격력 비율 (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: 초기 돌진 거리 (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: 측면 이동 거리 (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: 스태미나 소모량",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: 쿨타임 (초)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: 이동 속도 (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: 공격 속도 보너스 (%)",

                // ============================================
                // Spear Tree (창 트리) - 35개
                // ============================================

                // === Spear Tree: 창 전문가 (4개) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: [창 전문가] 필요 포인트",
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: [창 전문가] 2연속 공격속도 보너스 (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: [창 전문가] 2연속 공격력 보너스 (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: [창 전문가] 효과 지속시간 (초)",

                // === Spear Tree: 급소 찌르기 (2개) ===
                ["Tier1_QuickStrike_RequiredPoints"] = "Tier 1: [급소 찌르기] 필요 포인트",
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: [급소 찌르기] 치명타 피해 보너스 (%)",

                // === Spear Tree: 투창 (3개 + Legacy 1개) ===
                ["Tier2_Throw_RequiredPoints"] = "Tier 2: [투창] 필요 포인트",
                ["Tier2_Throw_Cooldown"] = "Tier 2: [투창] 쿨타임 (초)",
                ["Tier2_Throw_DamageMultiplier"] = "Tier 2: [투창] 데미지 배율 (%)",
                ["Legacy_Throw_BuffDuration"] = "레거시: 미사용",

                // === Spear Tree: 연격창 (2개) ===
                ["Tier3_Pierce_RequiredPoints"] = "Tier 3-1: [연격창] 필요 포인트",
                ["Tier3_Rapid_DamageBonus"] = "Tier 3-1: [연격창] 무기 공격력 보너스",

                // === Spear Tree: 폭발창 (3개) ===
                ["Tier3_Explosion_Chance"] = "Tier 3-2: [폭발창] 발동 확률 (%)",
                ["Tier3_Explosion_Radius"] = "Tier 3-2: [폭발창] 폭발 범위 (m)",
                ["Tier3_Explosion_DamageBonus"] = "Tier 3-2: [폭발창] 공격력 보너스 (%)",

                // === Spear Tree: 회피 찌르기 (3개) ===
                ["Tier4_Evasion_RequiredPoints"] = "Tier 4-1: [회피 찌르기] 필요 포인트",
                ["Tier4_Evasion_DamageBonus"] = "Tier 4-1: [회피 찌르기] 구르기 후 공격력 보너스 (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4-1: [회피 찌르기] 스태미나 소모 감소 (%)",

                // === Spear Tree: 이연창 (2개) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4-2: [이연창] 2연속 공격력 보너스 (%)",
                ["Tier4_Dual_Duration"] = "Tier 4-2: [이연창] 버프 지속시간 (초)",

                // === Spear Tree: 꿰뚫는 창 (6개 + Legacy 1개) ===
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5-1: [꿰뚫는 창] 필요 포인트",
                ["Legacy_Penetrate_CritChance"] = "레거시: 미사용",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5-1: [꿰뚫는 창] 버프 지속시간 (초)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5-1: [꿰뚫는 창] 번개 충격 데미지 배율 (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5-1: [꿰뚫는 창] 번개 발동 연속 적중 횟수",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5-1: [꿰뚫는 창] G키 쿨타임 (초)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5-1: [꿰뚫는 창] G키 스태미나 소모",

                // === Spear Tree: 연공창 (8개) ===
                ["Tier5_Combo_RequiredPoints"] = "Tier 5-2: [연공창] 필요 포인트",
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5-2: [연공창] H키 쿨타임 (초)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5-2: [연공창] H키 데미지 배율 (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5-2: [연공창] H키 스태미나 소모",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5-2: [연공창] H키 넉백 범위 (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5-2: [연공창] 액티브 효과 범위 (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5-2: [연공창] 버프 지속시간 (초)",
                ["Tier5_Combo_MaxUses"] = "Tier 5-2: [연공창] 최대 강화 투창 횟수",

                // ============================================
                // Staff Tree (지팡이 트리) - 30개
                // ============================================


                // === Tier 0: 지팡이 전문가 (2개) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] = "Tier 0: [지팡이 전문가] 속성 데미지 보너스 (%)",
                ["Tier0_StaffExpert_RequiredPoints"] = "Tier 0: [지팡이 전문가] 필요 포인트",

                // === Tier 1-1: 정신 집중 (2개) ===
                ["Tier1_MindFocus_EitrReduction"] = "Tier 1-1: [정신 집중] Eitr 소모 감소 (%)",
                ["Tier1_MindFocus_RequiredPoints"] = "Tier 1-1: [정신 집중] 필요 포인트",

                // === Tier 1-2: 마법 흐름 (2개) ===
                ["Tier1_MagicFlow_EitrBonus"] = "Tier 1-2: [마법 흐름] 최대 Eitr 보너스",
                ["Tier1_MagicFlow_RequiredPoints"] = "Tier 1-2: [마법 흐름] 필요 포인트",

                // === Tier 2: 마법 증폭 (4개) ===
                ["Tier2_MagicAmplify_Chance"] = "Tier 2: [마법 증폭] 발동 확률 (%)",
                ["Tier2_MagicAmplify_DamageBonus"] = "Tier 2: [마법 증폭] 속성 공격 보너스 (%)",
                ["Tier2_MagicAmplify_EitrCostIncrease"] = "Tier 2: [마법 증폭] Eitr 소모 증가 (%)",
                ["Tier2_MagicAmplify_RequiredPoints"] = "Tier 2: [마법 증폭] 필요 포인트",

                // === Tier 3-1: 냉기 속성 (2개) ===
                ["Tier3_FrostElement_DamageBonus"] = "Tier 3-1: [냉기 속성] 공격력 보너스",
                ["Tier3_FrostElement_RequiredPoints"] = "Tier 3-1: [냉기 속성] 필요 포인트",

                // === Tier 3-2: 화염 속성 (2개) ===
                ["Tier3_FireElement_DamageBonus"] = "Tier 3-2: [화염 속성] 공격력 보너스",
                ["Tier3_FireElement_RequiredPoints"] = "Tier 3-2: [화염 속성] 필요 포인트",

                // === Tier 3-3: 번개 속성 (2개) ===
                ["Tier3_LightningElement_DamageBonus"] = "Tier 3-3: [번개 속성] 공격력 보너스",
                ["Tier3_LightningElement_RequiredPoints"] = "Tier 3-3: [번개 속성] 필요 포인트",

                // === Tier 4: 행운 마력 (2개) ===
                ["Tier4_LuckyMana_Chance"] = "Tier 4: [행운 마력] Eitr 무소모 발동 확률 (%)",
                ["Tier4_LuckyMana_RequiredPoints"] = "Tier 4: [행운 마력] 필요 포인트",

                // === Tier 5-1: 이중시전 - R키 액티브 (6개) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] = "Tier 5-1: [이중시전] 추가 투사체 개수",
                ["Tier5_DoubleCast_ProjectileDamagePercent"] = "Tier 5-1: [이중시전] 투사체 데미지 비율 (%)",
                ["Tier5_DoubleCast_AngleOffset"] = "Tier 5-1: [이중시전] 투사체 각도 오프셋 (도)",
                ["Tier5_DoubleCast_EitrCost"] = "Tier 5-1: [이중시전] Eitr 소모량",
                ["Tier5_DoubleCast_Cooldown"] = "Tier 5-1: [이중시전] 쿨타임 (초)",
                ["Tier5_DoubleCast_RequiredPoints"] = "Tier 5-1: [이중시전] 필요 포인트",

                // === Tier 5-2: 즉시 범위 힐 - H키 액티브 (6개) ===
                ["Tier5_InstantAreaHeal_Cooldown"] = "Tier 5-2: [힐] 쿨타임 (초)",
                ["Tier5_InstantAreaHeal_EitrCost"] = "Tier 5-2: [힐] Eitr 소모량",
                ["Tier5_InstantAreaHeal_HealPercent"] = "Tier 5-2: [힐] 회복량 (최대 HP 대비 %)",
                ["Tier5_InstantAreaHeal_Range"] = "Tier 5-2: [힐] 치유 범위 (미터)",
                ["Tier5_InstantAreaHeal_SelfHeal"] = "Tier 5-2: [힐] 자가 치유 허용",
                ["Tier5_InstantAreaHeal_RequiredPoints"] = "Tier 5-2: [힐] 필요 포인트",

                // ============================================
                // Crossbow Tree (석궁 트리) - 34개
                // ============================================


                // === Tier 0: 석궁 전문가 (2개) ===
                ["Tier0_CrossbowExpert_DamageBonus"] = "Tier 0: [석궁 전문가] 석궁 공격력 보너스 (%)",
                ["Tier0_CrossbowExpert_RequiredPoints"] = "Tier 0: [석궁 전문가] 필요 포인트",

                // === Tier 1: 연발 (6개) ===
                ["Tier1_RapidFire_Chance"] = "Tier 1: [연발] 발동 확률 (%)",
                ["Tier1_RapidFire_ShotCount"] = "Tier 1: [연발] 발사 횟수",
                ["Tier1_RapidFire_DamagePercent"] = "Tier 1: [연발] 데미지 비율 (%)",
                ["Tier1_RapidFire_Delay"] = "Tier 1: [연발] 발사 간격 (초)",
                ["Tier1_RapidFire_BoltConsumption"] = "Tier 1: [연발] 볼트 소모량",
                ["Tier1_RapidFire_RequiredPoints"] = "Tier 1: [연발] 필요 포인트",

                // === Tier 2-1: 균형 조준 / 2-2: 고속 장전 / 2-3: 정직한 한 발 (8개) ===
                ["Tier2_BalancedAim_KnockbackChance"] = "Tier 2-1: [균형 조준] 넉백 발동 확률 (%)",
                ["Tier2_BalancedAim_KnockbackDistance"] = "Tier 2-1: [균형 조준] 넉백 거리 (미터)",
                ["Tier2_BalancedAim_RequiredPoints"] = "Tier 2-1: [균형 조준] 필요 포인트",
                ["Tier2_RapidReload_SpeedIncrease"] = "Tier 2-2: [고속 장전] 장전 속도 증가 (%)",
                ["Tier2_RapidReload_RequiredPoints"] = "Tier 2-2: [고속 장전] 필요 포인트",
                ["Tier2_HonestShot_DamageBonus"] = "Tier 2-3: [정직한 한 발] 데미지 보너스 (%)",
                ["Tier2_HonestShot_RequiredPoints"] = "Tier 2-3: [정직한 한 발] 필요 포인트",

                // === Tier 3: 자동 장전 (2개) ===
                ["Tier3_AutoReload_Chance"] = "Tier 3: [자동 장전] 발동 확률 (%)",
                ["Tier3_AutoReload_RequiredPoints"] = "Tier 3: [자동 장전] 필요 포인트",

                // === Tier 4-1: 연속 발사 Lv2 / 4-2: 결전의 일격 (9개) ===
                ["Tier4_RapidFireLv2_Chance"] = "Tier 4-1: [연속 발사 Lv2] 발동 확률 (%)",
                ["Tier4_RapidFireLv2_ShotCount"] = "Tier 4-1: [연속 발사 Lv2] 발사 횟수",
                ["Tier4_RapidFireLv2_DamagePercent"] = "Tier 4-1: [연속 발사 Lv2] 데미지 비율 (%)",
                ["Tier4_RapidFireLv2_Delay"] = "Tier 4-1: [연속 발사 Lv2] 발사 간격 (초)",
                ["Tier4_RapidFireLv2_BoltConsumption"] = "Tier 4-1: [연속 발사 Lv2] 볼트 소모량",
                ["Tier4_RapidFireLv2_RequiredPoints"] = "Tier 4-1: [연속 발사 Lv2] 필요 포인트",
                ["Tier4_FinalStrike_HpThreshold"] = "Tier 4-2: [결전의 일격] 적 체력 임계값 (%)",
                ["Tier4_FinalStrike_DamageBonus"] = "Tier 4-2: [결전의 일격] 추가 데미지 보너스 (%)",
                ["Tier4_FinalStrike_RequiredPoints"] = "Tier 4-2: [결전의 일격] 필요 포인트",

                // === Tier 5: 단 한 발 - R키 액티브 (5개) ===
                ["Tier5_OneShot_Duration"] = "Tier 5: [단 한 발] 버프 지속시간 (초)",
                ["Tier5_OneShot_DamageBonus"] = "Tier 5: [단 한 발] 데미지 보너스 (%)",
                ["Tier5_OneShot_KnockbackDistance"] = "Tier 5: [단 한 발] 넉백 거리 (미터)",
                ["Tier5_OneShot_Cooldown"] = "Tier 5: [단 한 발] 쿨타임 (초)",
                ["Tier5_OneShot_RequiredPoints"] = "Tier 5: [단 한 발] 필요 포인트",

                // ============================================
                // Knife Tree (단검 트리) - 32개
                // ============================================

                // === Tier 0: 단검 전문가 ===
                ["Tier0_KnifeExpert_BackstabBonus"] = "Tier 0: [단검 전문가] 백스탭 보너스 (%)",
                ["Tier0_KnifeExpert_RequiredPoints"] = "Tier 0: [단검 전문가] 필요 포인트",

                // === Tier 1: 회피 숙련 ===
                ["Tier1_Evasion_Chance"] = "Tier 1: [회피 숙련] 발동 확률 (%)",
                ["Tier1_Evasion_Duration"] = "Tier 1: [회피 숙련] 지속시간 (초)",
                ["Tier1_Evasion_RequiredPoints"] = "Tier 1: [회피 숙련] 필요 포인트",

                // === Tier 2: 빠른 움직임 ===
                ["Tier2_FastMove_MoveSpeedBonus"] = "Tier 2: [빠른 움직임] 이동속도 보너스 (%)",
                ["Tier2_FastMove_RequiredPoints"] = "Tier 2: [빠른 움직임] 필요 포인트",

                // === Tier 3: 빠른 공격 ===
                ["Tier3_CombatMastery_DamageBonus"] = "Tier 3: [빠른 공격] 공격력 보너스",
                ["Tier3_CombatMastery_BuffDuration"] = "Tier 3: [빠른 공격] 버프 지속시간 (초)",
                ["Tier3_CombatMastery_RequiredPoints"] = "Tier 3: [빠른 공격] 필요 포인트",

                // === Tier 4: 치명타 숙련 ===
                ["Tier4_AttackEvasion_EvasionBonus"] = "Tier 4: [치명타 숙련] 회피율 보너스 (%)",
                ["Tier4_AttackEvasion_BuffDuration"] = "Tier 4: [치명타 숙련] 버프 지속시간 (초)",
                ["Tier4_AttackEvasion_Cooldown"] = "Tier 4: [치명타 숙련] 쿨타임 (초)",
                ["Tier4_AttackEvasion_RequiredPoints"] = "Tier 4: [치명타 숙련] 필요 포인트",

                // === Tier 5: 치명적 피해 ===
                ["Tier5_CriticalDamage_DamageBonus"] = "Tier 5: [치명적 피해] 데미지 보너스 (%)",
                ["Tier5_CriticalDamage_RequiredPoints"] = "Tier 5: [치명적 피해] 필요 포인트",

                // === Tier 6: 암살자 ===
                ["Tier6_Assassin_CritDamageBonus"] = "Tier 6: [암살자] 치명타 데미지 보너스 (%)",
                ["Tier6_Assassin_CritChanceBonus"] = "Tier 6: [암살자] 치명타 확률 보너스 (%)",
                ["Tier6_Assassin_RequiredPoints"] = "Tier 6: [암살자] 필요 포인트",

                // === Tier 7: 암살술 ===
                ["Tier7_Assassination_StaggerChance"] = "Tier 7: [암살술] 스태거 발동 확률 (%)",
                ["Tier7_Assassination_RequiredComboHits"] = "Tier 7: [암살술] 필요 연속 적중 횟수",
                ["Tier7_Assassination_RequiredPoints"] = "Tier 7: [암살술] 필요 포인트",

                // === Tier 8: 암살자의 심장 - G키 액티브 ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] = "Tier 8: [암살자의 심장] 치명타 데미지 배율",
                ["Tier8_AssassinHeart_Duration"] = "Tier 8: [암살자의 심장] 버프 지속시간 (초)",
                ["Tier8_AssassinHeart_StaminaCost"] = "Tier 8: [암살자의 심장] 스태미나 소모",
                ["Tier8_AssassinHeart_Cooldown"] = "Tier 8: [암살자의 심장] 쿨타임 (초)",
                ["Tier8_AssassinHeart_TeleportRange"] = "Tier 8: [암살자의 심장] 순간이동 범위 (m)",
                ["Tier8_AssassinHeart_TeleportBackDistance"] = "Tier 8: [암살자의 심장] 등 뒤 이동 거리 (m)",
                ["Tier8_AssassinHeart_StunDuration"] = "Tier 8: [암살자의 심장] 기절 지속시간 (초)",
                ["Tier8_AssassinHeart_ComboAttackCount"] = "Tier 8: [암살자의 심장] 연속 공격 횟수",
                ["Tier8_AssassinHeart_AttackInterval"] = "Tier 8: [암살자의 심장] 공격 간격 (초)",
                ["Tier8_AssassinHeart_RequiredPoints"] = "Tier 8: [암살자의 심장] 필요 포인트",

                // ============================================
                // Sword Tree (검 트리) - 33개 (신규 키 형식)
                // ============================================

                // === Tier 0: 검 전문가 (2개) ===
                ["Tier0_SwordExpert_DamageBonus"] = "Tier 0: [검 전문가] 검 공격력 보너스 (%)",
                ["Tier0_SwordExpert_RequiredPoints"] = "Tier 0: [검 전문가] 필요 포인트",

                // === Tier 1-1: 빠른 베기 (2개) ===
                ["Tier1_FastSlash_RequiredPoints"] = "Tier 1-1: [빠른 베기] 필요 포인트",
                ["Tier1_FastSlash_AttackSpeedBonus"] = "Tier 1-1: [빠른 베기] 공격속도 보너스 (%)",

                // === Tier 1-2: 반격 자세 (3개) ===
                ["Tier1_CounterStance_RequiredPoints"] = "Tier 1-2: [반격 자세] 필요 포인트",
                ["Tier1_CounterStance_Duration"] = "Tier 1-2: [반격 자세] 지속시간 (초)",
                ["Tier1_CounterStance_DefenseBonus"] = "Tier 1-2: [반격 자세] 방어력 보너스 (%)",

                // === Tier 2: 연속 베기 (3개) ===
                ["Tier2_ComboSlash_RequiredPoints"] = "Tier 2: [연속 베기] 필요 포인트",
                ["Tier2_ComboSlash_DamageBonus"] = "Tier 2: [연속 베기] 공격력 보너스 (%)",
                ["Tier2_ComboSlash_BuffDuration"] = "Tier 2: [연속 베기] 버프 지속시간 (초)",

                // === Tier 3: 칼날 되치기 (2개) ===
                ["Tier3_Riposte_RequiredPoints"] = "Tier 3: [칼날 되치기] 필요 포인트",
                ["Tier3_Riposte_DamageBonus"] = "Tier 3: [칼날 되치기] 공격력 보너스",

                // === Tier 4-1: 공방일체 (3개) ===
                ["Tier4_AllInOne_RequiredPoints"] = "Tier 4-1: [공방일체] 필요 포인트",
                ["Tier4_AllInOne_AttackBonus"] = "Tier 4-1: [공방일체] 공격력 보너스 (%)",
                ["Tier4_AllInOne_DefenseBonus"] = "Tier 4-1: [공방일체] 방어력 보너스",

                // === Tier 4-2: 진검승부 (2개) ===
                ["Tier4_TrueDuel_RequiredPoints"] = "Tier 4-2: [진검승부] 필요 포인트",
                ["Tier4_TrueDuel_AttackSpeedBonus"] = "Tier 4-2: [진검승부] 공격속도 보너스 (%)",

                // === Tier 5: 패링 돌격 - G키 액티브 (6개) ===
                ["Tier5_ParryRush_RequiredPoints"] = "Tier 5: [패링 돌격] 필요 포인트",
                ["Tier5_ParryRush_BuffDuration"] = "Tier 5: [패링 돌격] 버프 지속시간 (초)",
                ["Tier5_ParryRush_DamageBonus"] = "Tier 5: [패링 돌격] 공격력 보너스 (%)",
                ["Tier5_ParryRush_PushDistance"] = "Tier 5: [패링 돌격] 밀어내기 거리 (m)",
                ["Tier5_ParryRush_StaminaCost"] = "Tier 5: [패링 돌격] 스태미나 소모",
                ["Tier5_ParryRush_Cooldown"] = "Tier 5: [패링 돌격] 쿨타임 (초)",

                // === Tier 6: 돌진 연속 베기 - G키 액티브 (10개) ===
                ["Tier6_RushSlash_RequiredPoints"] = "Tier 6: [돌진 연속 베기] 필요 포인트",
                ["Tier6_RushSlash_Hit1DamageRatio"] = "Tier 6: [돌진 연속 베기] 1차 공격력 비율 (%)",
                ["Tier6_RushSlash_Hit2DamageRatio"] = "Tier 6: [돌진 연속 베기] 2차 공격력 비율 (%)",
                ["Tier6_RushSlash_Hit3DamageRatio"] = "Tier 6: [돌진 연속 베기] 3차 공격력 비율 (%)",
                ["Tier6_RushSlash_InitialDistance"] = "Tier 6: [돌진 연속 베기] 초기 돌진 거리 (m)",
                ["Tier6_RushSlash_SideDistance"] = "Tier 6: [돌진 연속 베기] 측면 이동 거리 (m)",
                ["Tier6_RushSlash_StaminaCost"] = "Tier 6: [돌진 연속 베기] 스태미나 소모량",
                ["Tier6_RushSlash_Cooldown"] = "Tier 6: [돌진 연속 베기] 쿨타임 (초)",
                ["Tier6_RushSlash_MoveSpeed"] = "Tier 6: [돌진 연속 베기] 이동 속도 (m/s)",
                ["Tier6_RushSlash_AttackSpeedBonus"] = "Tier 6: [돌진 연속 베기] 공격속도 보너스 (%)",

                // ============================================
                // Mace Tree (둔기 트리) - 34개
                // ============================================


                // === Tier 0: 둔기 전문가 (4개) ===
                ["Tier0_MaceExpert_DamageBonus"] = "Tier 0: [둔기 전문가] 공격력 보너스 (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: [둔기 전문가] 기절 확률 (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: [둔기 전문가] 기절 지속시간 (초)",
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: [둔기 전문가] 필요 포인트",

                // === Tier 1: 둔기 강화 (2개) ===
                ["Tier1_MaceExpert_DamageBonus"] = "Tier 1: [둔기 강화] 공격력 보너스 (%)",
                ["Tier1_MaceExpert_RequiredPoints"] = "Tier 1: [둔기 강화] 필요 포인트",

                // === Tier 2: 기절 강화 (3개) ===
                ["Tier2_StunBoost_StunChanceBonus"] = "Tier 2: [기절 강화] 기절 확률 보너스 (%)",
                ["Tier2_StunBoost_StunDurationBonus"] = "Tier 2: [기절 강화] 기절 지속시간 보너스 (초)",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: [기절 강화] 필요 포인트",

                // === Tier 3-1: 방어 강화 (2개) ===
                ["Tier3_Guard_ArmorBonus"] = "Tier 3-1: [방어 강화] 방어력 보너스",
                ["Tier3_Guard_RequiredPoints"] = "Tier 3-1: [방어 강화] 필요 포인트",

                // === Tier 3-2: 무거운 일격 (2개) ===
                ["Tier3_HeavyStrike_DamageBonus"] = "Tier 3-2: [무거운 일격] 공격력 보너스",
                ["Tier3_HeavyStrike_RequiredPoints"] = "Tier 3-2: [무거운 일격] 필요 포인트",

                // === Tier 4: 밀어내기 (2개) ===
                ["Tier4_Push_KnockbackChance"] = "Tier 4: [밀어내기] 넉백 확률 (%)",
                ["Tier4_Push_RequiredPoints"] = "Tier 4: [밀어내기] 필요 포인트",

                // === Tier 5-1: 탱커 (3개) ===
                ["Tier5_Tank_HealthBonus"] = "Tier 5-1: [탱커] 체력 보너스 (%)",
                ["Tier5_Tank_DamageReduction"] = "Tier 5-1: [탱커] 받는 데미지 감소 (%)",
                ["Tier5_Tank_RequiredPoints"] = "Tier 5-1: [탱커] 필요 포인트",

                // === Tier 5-2: 데미지 강화 (3개) ===
                ["Tier5_DPS_DamageBonus"] = "Tier 5-2: [데미지 강화] 공격력 보너스 (%)",
                ["Tier5_DPS_AttackSpeedBonus"] = "Tier 5-2: [데미지 강화] 공격속도 보너스 (%)",
                ["Tier5_DPS_RequiredPoints"] = "Tier 5-2: [데미지 강화] 필요 포인트",

                // === Tier 6: 그랜드마스터 (2개) ===
                ["Tier6_Grandmaster_ArmorBonus"] = "Tier 6: [그랜드마스터] 방어력 보너스 (%)",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: [그랜드마스터] 필요 포인트",

                // === Tier 7-1: 분노의 망치 - H키 액티브 (6개) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] = "Tier 7-1: [분노의 망치] 1-4타 데미지 배율 (%)",
                ["Tier7_FuryHammer_FinalHitMultiplier"] = "Tier 7-1: [분노의 망치] 5타 최종타 데미지 배율 (%)",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7-1: [분노의 망치] 스태미나 소모",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7-1: [분노의 망치] 쿨타임 (초)",
                ["Tier7_FuryHammer_AoeRadius"] = "Tier 7-1: [분노의 망치] AOE 범위 (m)",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7-1: [분노의 망치] 필요 포인트",

                // === Tier 7-2: 수호자의 진심 - G키 액티브 (5개) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7-2: [수호자의 진심] 쿨타임 (초)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7-2: [수호자의 진심] 스태미나 소모",
                ["Tier7_GuardianHeart_Duration"] = "Tier 7-2: [수호자의 진심] 버프 지속시간 (초)",
                ["Tier7_GuardianHeart_ReflectPercent"] = "Tier 7-2: [수호자의 진심] 반사 데미지 비율 (%)",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7-2: [수호자의 진심] 필요 포인트",

                // ========================================
                // Polearm Tree (37개 키)
                // ========================================

                // === Tier 0 - 폴암 전문가 (2개) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] = "Tier 0: [폴암 전문가] 공격 범위 보너스 (%)",
                ["Tier0_PolearmExpert_RequiredPoints"] = "Tier 0: [폴암 전문가] 필요 포인트",

                // === Tier 1 - 회전베기 (2개) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] = "Tier 1: [회전베기] 회전 공격 피해 보너스 (%)",
                ["Tier1_SpinWheel_RequiredPoints"] = "Tier 1: [회전베기] 필요 포인트",

                // === Tier 2-1 - 폴암강화 (2개) ===
                ["Tier2-1_PolearmBoost_WeaponDamageBonus"] = "Tier 2-1: [폴암강화] 무기 공격력 보너스 (고정값)",
                ["Tier2-1_PolearmBoost_RequiredPoints"] = "Tier 2-1: [폴암강화] 필요 포인트",

                // === Tier 2-2 - 영웅 타격 (2개) ===
                ["Tier2-2_HeroStrike_KnockbackChance"] = "Tier 2-2: [영웅 타격] 넉백 확률 (%)",
                ["Tier2-2_HeroStrike_RequiredPoints"] = "Tier 2-2: [영웅 타격] 필요 포인트",

                // === Tier 3 - 광역 강타 (3개) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] = "Tier 3: [광역 강타] 2연타 피해 보너스 (%)",
                ["Tier3_AreaCombo_DoubleHitDuration"] = "Tier 3: [광역 강타] 2연타 버프 지속시간 (초)",
                ["Tier3_AreaCombo_RequiredPoints"] = "Tier 3: [광역 강타] 필요 포인트",

                // === Tier 4-1 - 지면 강타 (2개) ===
                ["Tier4-1_GroundWheel_WheelAttackDamageBonus"] = "Tier 4-1: [지면 강타] 회전 공격 피해 보너스 (%)",
                ["Tier4-1_GroundWheel_RequiredPoints"] = "Tier 4-1: [지면 강타] 필요 포인트",

                // === Tier 4-2 - 반달 베기 (3개) ===
                ["Tier4-2_MoonSlash_AttackRangeBonus"] = "Tier 4-2: [반달 베기] 공격 범위 보너스 (%)",
                ["Tier4-2_MoonSlash_StaminaReduction"] = "Tier 4-2: [반달 베기] 스태미나 소모 감소 (%)",
                ["Tier4-2_MoonSlash_RequiredPoints"] = "Tier 4-2: [반달 베기] 필요 포인트",

                // === Tier 4-3 - 제압 공격 (2개) ===
                ["Tier4-3_Suppress_DamageBonus"] = "Tier 4-3: [제압 공격] 피해 보너스 (%)",
                ["Tier4-3_Suppress_RequiredPoints"] = "Tier 4-3: [제압 공격] 필요 포인트",

                // === Tier 5 - 관통 돌격 (9개) ===
                ["Tier5_PierceCharge_DashDistance"] = "Tier 5: [관통 돌격] 돌진 거리 (m)",
                ["Tier5_PierceCharge_FirstHitDamageBonus"] = "Tier 5: [관통 돌격] 첫 타격 피해 보너스 (%)",
                ["Tier5_PierceCharge_AoeDamageBonus"] = "Tier 5: [관통 돌격] 광역 넉백 피해 보너스 (%)",
                ["Tier5_PierceCharge_AoeAngle"] = "Tier 5: [관통 돌격] 광역 각도 (도)",
                ["Tier5_PierceCharge_AoeRadius"] = "Tier 5: [관통 돌격] 광역 반경 (m)",
                ["Tier5_PierceCharge_KnockbackDistance"] = "Tier 5: [관통 돌격] 넉백 거리 (m)",
                ["Tier5_PierceCharge_StaminaCost"] = "Tier 5: [관통 돌격] 스태미나 소모",
                ["Tier5_PierceCharge_Cooldown"] = "Tier 5: [관통 돌격] 쿨타임 (초)",
                ["Tier5_PierceCharge_RequiredPoints"] = "Tier 5: [관통 돌격] 필요 포인트",

                // ============================================
                // Archer Job Skills (아처 직업 스킬) - 8개
                // ============================================
                ["Archer_MultiShot_ArrowCount"] = "멀티샷: 화살 수",
                ["Archer_MultiShot_ArrowConsumption"] = "멀티샷: 화살 소모",
                ["Archer_MultiShot_DamagePercent"] = "멀티샷: 화살당 데미지 (%)",
                ["Archer_MultiShot_Cooldown"] = "멀티샷: 쿨타임 (초)",
                ["Archer_MultiShot_Charges"] = "멀티샷: 발사 회수",
                ["Archer_MultiShot_StaminaCost"] = "멀티샷: 스태미나 소모",
                ["Archer_JumpHeightBonus"] = "패시브: 점프 높이 보너스 (%)",
                ["Archer_FallDamageReduction"] = "패시브: 낙사 데미지 감소 (%)",

                // ============================================
                // Mage Job Skills (메이지 직업 스킬) - 6개
                // ============================================
                ["Mage_AOE_Range"] = "액티브: 범위 (m)",
                ["Mage_Eitr_Cost"] = "액티브: 에이트르 소모",
                ["Mage_Damage_Multiplier"] = "액티브: 데미지 배수 (%)",
                ["Mage_Cooldown"] = "액티브: 쿨타임 (초)",
                ["Mage_VFX_Name"] = "액티브: VFX 효과명",
                ["Mage_Elemental_Resistance"] = "패시브: 속성 저항 (%)",

                // ============================================
                // Tanker Job Skills (탱커 직업 스킬) - 10개
                // ============================================
                ["Tanker_Taunt_Cooldown"] = "전장의 함성: 쿨타임 (초)",
                ["Tanker_Taunt_StaminaCost"] = "전장의 함성: 스태미나 소모",
                ["Tanker_Taunt_Range"] = "전장의 함성: 도발 범위 (m)",
                ["Tanker_Taunt_Duration"] = "전장의 함성: 도발 지속시간 (초)",
                ["Tanker_Taunt_BossDuration"] = "전장의 함성: 보스 도발 지속시간 (초)",
                ["Tanker_Taunt_DamageReduction"] = "전장의 함성: 피해 감소 (%)",
                ["Tanker_Taunt_BuffDuration"] = "전장의 함성: 버프 지속시간 (초)",
                ["Tanker_Taunt_EffectHeight"] = "전장의 함성: 효과 높이 (m)",
                ["Tanker_Taunt_EffectScale"] = "전장의 함성: 효과 크기 배율",
                ["Tanker_Passive_DamageReduction"] = "패시브: 피해 감소 (%)",

                // ============================================
                // Rogue Job Skills (로그 직업 스킬) - 10개
                // ============================================
                ["Rogue_ShadowStrike_Cooldown"] = "그림자 일격: 쿨타임 (초)",
                ["Rogue_ShadowStrike_StaminaCost"] = "그림자 일격: 스태미나 소모",
                ["Rogue_ShadowStrike_AttackBonus"] = "그림자 일격: 공격력 증가 (%)",
                ["Rogue_ShadowStrike_BuffDuration"] = "그림자 일격: 버프 지속시간 (초)",
                ["Rogue_ShadowStrike_SmokeScale"] = "그림자 일격: 연막 크기 배율",
                ["Rogue_ShadowStrike_AggroRange"] = "그림자 일격: 어그로 제거 범위 (m)",
                ["Rogue_ShadowStrike_StealthDuration"] = "그림자 일격: 스텔스 지속시간 (초)",
                ["Rogue_AttackSpeed_Bonus"] = "패시브: 공격 속도 보너스 (%)",
                ["Rogue_Stamina_Reduction"] = "패시브: 스태미나 사용 감소 (%)",
                ["Rogue_ElementalResistance_Debuff"] = "패시브: 속성 저항 증가 (%)",

                // ============================================
                // Paladin Job Skills (성기사 직업 스킬) - 9개
                // ============================================
                ["Paladin_Active_Cooldown"] = "신성한 치유: 쿨타임 (초)",
                ["Paladin_Active_Range"] = "신성한 치유: 범위 (m)",
                ["Paladin_Active_EitrCost"] = "신성한 치유: 에이트르 소모",
                ["Paladin_Active_StaminaCost"] = "신성한 치유: 스태미나 소모",
                ["Paladin_Active_SelfHealPercent"] = "신성한 치유: 자가 치유 비율 (%)",
                ["Paladin_Active_AllyHealPercentOverTime"] = "신성한 치유: 아군 지속 치유 비율 (%)",
                ["Paladin_Active_Duration"] = "신성한 치유: 지속시간 (초)",
                ["Paladin_Active_Interval"] = "신성한 치유: 치유 간격 (초)",
                ["Paladin_Passive_ElementalResistanceReduction"] = "패시브: 저항 증가 (%)",

                // ============================================
                // Berserker Job Skills (버서커 직업 스킬) - 10개
                // ============================================
                ["Beserker_Active_Cooldown"] = "버서커의 분노: 쿨타임 (초)",
                ["Beserker_Active_StaminaCost"] = "버서커의 분노: 스태미나 소모",
                ["Beserker_Active_Duration"] = "버서커의 분노: 지속시간 (초)",
                ["Beserker_Active_DamagePerHealthPercent"] = "버서커의 분노: HP당 피해 증가 (%)",
                ["Beserker_Active_MaxDamageBonus"] = "버서커의 분노: 최대 피해 보너스 (%)",
                ["Beserker_Active_HealthThreshold"] = "버서커의 분노: 발동 HP 임계값 (%)",
                ["Beserker_Passive_HealthThreshold"] = "죽음의 도전: 발동 HP 임계값 (%)",
                ["Beserker_Passive_InvincibilityDuration"] = "죽음의 도전: 무적 지속시간 (초)",
                ["Beserker_Passive_Cooldown"] = "죽음의 도전: 쿨타임 (초)",
                ["Berserker_Passive_HealthBonus"] = "패시브: 최대 체력 보너스 (%)",

                // ============================================
                // Sword Tree (검 트리) - 경로 히트 추가분
                // ============================================
                ["Tier6_RushSlash_PathWidth"] = "Tier 6: [돌진 연속 베기] 경로 베기 너비 (m)",

            };
        }
    }
}

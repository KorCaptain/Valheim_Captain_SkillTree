using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// BepInEx Configuration Manager 로컬라이제이션
    /// F1 메뉴에서 표시되는 카테고리와 설명을 언어별로 번역합니다.
    /// </summary>
    public static class ConfigTranslations
    {
        /// <summary>
        /// 카테고리 번역 가져오기
        /// </summary>
        public static Dictionary<string, string> GetCategoryTranslations(string lang)
        {
            return (lang == "en") ? GetEnglishCategories() : GetKoreanCategories();
        }

        /// <summary>
        /// 설명 번역 가져오기
        /// </summary>
        public static Dictionary<string, string> GetDescriptionTranslations(string lang)
        {
            return (lang == "en") ? GetEnglishDescriptions() : GetKoreanDescriptions();
        }

        // ============================================
        // 카테고리 번역 (한국어)
        // ============================================
        private static Dictionary<string, string> GetKoreanCategories()
        {
            return new Dictionary<string, string>
            {
                ["Attack Tree"] = "Attack Tree",
                ["Defense Tree"] = "Defense Tree",
                ["Production Tree"] = "Production Tree",
                ["Staff Tree"] = "Staff Tree",
                ["Crossbow Tree"] = "Crossbow Tree",
                ["Bow Tree"] = "Bow Tree",
                ["Sword Tree"] = "Sword Tree",
                ["Spear Tree"] = "Spear Tree",
                ["Mace Tree"] = "Mace Tree",
                ["Polearm Tree"] = "Polearm Tree",
                ["Knife Tree"] = "Knife Tree",
                ["Speed Tree"] = "Speed Tree",
                ["Archer Job Skills"] = "Archer Job Skills",
                ["Mage Job Skills"] = "Mage Job Skills",
            };
        }

        // ============================================
        // 설명 번역 (한국어)
        // ============================================
        private static Dictionary<string, string> GetKoreanDescriptions()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Attack Tree (공격 트리)
                // ========================================

                // === Attack Tree: 필요 포인트 (7개) ===
                ["Tier0_AttackExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_BaseAttack_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_WeaponSpec_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_AttackBoost_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_DetailEnhance_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_RangedEnhance_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_SpecialStat_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier6_FinalSpec_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 0: 공격 전문가 (Attack Expert) ===
                ["Tier0_AttackExpert_AllDamageBonus"] =
                "【모든 공격력 보너스 (%)】\n" +
                "물리 및 속성 데미지를 모두 증가시킵니다.\n" +
                "모든 무기에 적용되는 기본 공격력 강화입니다.\n" +
                "권장값: 8-12%",

                // === Tier 2: 무기 전문화 (Weapon Specialization) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] =
                "【근접 무기 보너스 발동 확률 (%)】\n" +
                "근접 무기 공격 시 추가 데미지가 발동할 확률입니다.\n" +
                "확률적으로 폭발적인 데미지를 줄 수 있습니다.\n" +
                "권장값: 15-25%",

                ["Tier2_MeleeSpec_MeleeDamage"] =
                "【근접 무기 추가 데미지 (고정값)】\n" +
                "보너스가 발동했을 때 추가되는 고정 데미지입니다.\n" +
                "권장값: 8-15",

                ["Tier2_BowSpec_BonusTriggerChance"] =
                "【활 보너스 발동 확률 (%)】\n" +
                "활 공격 시 추가 데미지가 발동할 확률입니다.\n" +
                "치명적인 화살을 발사할 기회를 제공합니다.\n" +
                "권장값: 15-25%",

                ["Tier2_BowSpec_BowDamage"] =
                "【활 추가 데미지 (고정값)】\n" +
                "보너스가 발동했을 때 추가되는 고정 데미지입니다.\n" +
                "권장값: 6-12",

                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] =
                "【석궁 보너스 발동 확률 (%)】\n" +
                "석궁 공격 시 추가 데미지가 발동할 확률입니다.\n" +
                "강력한 볼트로 적을 관통합니다.\n" +
                "권장값: 12-20%",

                ["Tier2_CrossbowSpec_CrossbowDamage"] =
                "【석궁 추가 데미지 (고정값)】\n" +
                "보너스가 발동했을 때 추가되는 고정 데미지입니다.\n" +
                "권장값: 7-13",

                ["Tier2_StaffSpec_ElementalTriggerChance"] =
                "【지팡이 속성 보너스 발동 확률 (%)】\n" +
                "지팡이 공격 시 추가 속성 데미지가 발동할 확률입니다.\n" +
                "마법 공격의 위력을 극대화합니다.\n" +
                "권장값: 15-25%",

                ["Tier2_StaffSpec_StaffDamage"] =
                "【지팡이 추가 데미지 (고정값)】\n" +
                "보너스가 발동했을 때 추가되는 고정 데미지입니다.\n" +
                "권장값: 6-12",

                // === Tier 3: 기본 공격 강화 (Base Attack) ===
                ["Tier3_BaseAttack_PhysicalDamageBonus"] =
                "【물리 데미지 보너스 (고정값)】\n" +
                "모든 무기의 물리 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 1-3",

                ["Tier3_BaseAttack_ElementalDamageBonus"] =
                "【속성 데미지 보너스 (고정값)】\n" +
                "모든 무기의 속성 데미지(불, 얼음, 번개 등)를 고정값으로 증가시킵니다.\n" +
                "권장값: 1-3",

                ["Tier3_AttackBoost_StrIntBonus"] =
                "【힘/지능 스탯 보너스】\n" +
                "근접 무기는 힘, 마법 무기는 지능을 증가시킵니다.\n" +
                "스탯 증가는 데미지에 직접적인 영향을 줍니다.\n" +
                "권장값: 3-8",

                ["Tier3_AttackBoost_PhysicalDamageBonus"] =
                "【양손 무기 물리 데미지 보너스 (%)】\n" +
                "양손 무기 사용 시 물리 데미지가 증가합니다.\n" +
                "양손 무기의 강력함을 극대화합니다.\n" +
                "권장값: 8-15%",

                ["Tier3_AttackBoost_ElementalDamageBonus"] =
                "【양손 무기 속성 데미지 보너스 (%)】\n" +
                "양손 무기 사용 시 속성 데미지가 증가합니다.\n" +
                "권장값: 8-15%",

                // === Tier 4: 전투 강화 (Combat Enhancement) ===
                ["Tier4_PrecisionAttack_CritChance"] =
                "【치명타 확률 보너스 (%)】\n" +
                "모든 공격의 크리티컬 히트 확률을 증가시킵니다.\n" +
                "치명타는 일반 공격보다 높은 데미지를 줍니다.\n" +
                "권장값: 3-8%",

                ["Tier4_MeleeEnhance_2HitComboBonus"] =
                "【2연타 콤보 데미지 보너스 (%)】\n" +
                "근접 무기로 2회 연속 타격 시 데미지가 증가합니다.\n" +
                "콤보 공격으로 더 강력한 타격을 가할 수 있습니다.\n" +
                "권장값: 8-15%",

                ["Tier4_RangedEnhance_RangedDamageBonus"] =
                "【원거리 무기 데미지 보너스 (고정값)】\n" +
                "활, 석궁 등 원거리 무기의 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 3-8",

                // === Tier 5: 특수화 스탯 (Specialized Stats) ===
                ["Tier5_SpecialStat_SpecBonus"] =
                "【무기 특화 보너스】\n" +
                "현재 사용 중인 무기 타입의 특화 스탯을 증가시킵니다.\n" +
                "근접 무기는 힘, 원거리 무기는 민첩, 마법은 지능이 증가합니다.\n" +
                "권장값: 3-8",

                // === Tier 6: 최종 강화 (Final Enhancement) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] =
                "【크리티컬 데미지 보너스 (%)】\n" +
                "치명타 발생 시 추가 데미지가 증가합니다.\n" +
                "크리티컬 히트의 위력을 극대화합니다.\n" +
                "권장값: 10-20%",

                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] =
                "【양손 무기 데미지 보너스 (%)】\n" +
                "양손 무기 사용 시 전체 데미지가 증가합니다.\n" +
                "양손 무기의 압도적인 공격력을 강화합니다.\n" +
                "권장값: 8-15%",

                ["Tier6_ElementalAttack_ElementalBonus"] =
                "【지팡이 속성 데미지 보너스 (%)】\n" +
                "지팡이의 속성 데미지(불, 얼음, 번개)가 증가합니다.\n" +
                "마법 공격의 파괴력을 높입니다.\n" +
                "권장값: 8-15%",

                ["Tier6_ComboFinisher_3HitComboBonus"] =
                "【3연타 콤보 피니셔 데미지 보너스 (%)】\n" +
                "근접 무기로 3회 연속 타격 시 최종 타격 데미지가 증가합니다.\n" +
                "콤보 피니셔로 적을 제압합니다.\n" +
                "권장값: 12-20%",

                // ========================================
                // Defense Tree (방어 트리)
                // ========================================

                // === Defense Tree: 필요 포인트 (7개) ===
                ["Tier0_DefenseExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_SkinHardening_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_MindBodyHealth_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_CoreDodgeBoostShield_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_ShockwaveStompRock_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_EndureAgileRegenParry_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier6_FinalSkills_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 0: 방어 전문가 (Defense Expert) ===
                ["Tier0_DefenseExpert_HPBonus"] =
                "【체력 보너스 (고정값)】\n" +
                "최대 체력을 고정값으로 증가시킵니다.\n" +
                "생존력의 기본이 되는 핵심 스탯입니다.\n" +
                "권장값: 3-8",

                ["Tier0_DefenseExpert_ArmorBonus"] =
                "【방어력 보너스 (고정값)】\n" +
                "방어력을 고정값으로 증가시킵니다.\n" +
                "받는 데미지를 감소시킵니다.\n" +
                "권장값: 1-4",

                // === Tier 1: 피부경화 (Skin Hardening) ===
                ["Tier1_SkinHardening_HPBonus"] =
                "【체력 보너스 (고정값)】\n" +
                "최대 체력을 추가로 증가시킵니다.\n" +
                "권장값: 3-8",

                ["Tier1_SkinHardening_ArmorBonus"] =
                "【방어력 보너스 (고정값)】\n" +
                "방어력을 추가로 증가시킵니다.\n" +
                "권장값: 3-8",

                // === Tier 2: 심신단련 & 체력단련 ===
                ["Tier2_MindBodyTraining_StaminaBonus"] =
                "【최대 스태미나 보너스 (고정값)】\n" +
                "최대 스태미나를 증가시킵니다.\n" +
                "더 많은 행동을 취할 수 있습니다.\n" +
                "권장값: 20-30",

                ["Tier2_MindBodyTraining_EitrBonus"] =
                "【최대 Eitr 보너스 (고정값)】\n" +
                "최대 Eitr를 증가시킵니다.\n" +
                "더 많은 마법을 사용할 수 있습니다.\n" +
                "권장값: 20-30",

                ["Tier2_HealthTraining_HPBonus"] =
                "【체력 보너스 (고정값)】\n" +
                "최대 체력을 대폭 증가시킵니다.\n" +
                "권장값: 15-25",

                ["Tier2_HealthTraining_ArmorBonus"] =
                "【방어력 보너스 (고정값)】\n" +
                "방어력을 추가로 증가시킵니다.\n" +
                "권장값: 3-8",

                // === Tier 3: 다양한 방어 기술 ===
                ["Tier3_CoreBreathing_EitrBonus"] =
                "【Eitr 보너스 (고정값)】\n" +
                "단전호흡으로 Eitr를 증가시킵니다.\n" +
                "권장값: 8-15",

                ["Tier3_EvasionTraining_DodgeBonus"] =
                "【회피율 보너스 (%)】\n" +
                "적의 공격을 회피할 확률을 증가시킵니다.\n" +
                "권장값: 3-8%",

                ["Tier3_EvasionTraining_InvincibilityBonus"] =
                "【구르기 무적시간 증가 (%)】\n" +
                "구르기 중 무적 시간을 연장합니다.\n" +
                "더 안전하게 회피할 수 있습니다.\n" +
                "권장값: 15-25%",

                ["Tier3_HealthBoost_HPBonus"] =
                "【체력 보너스 (고정값)】\n" +
                "체력을 추가로 증가시킵니다.\n" +
                "권장값: 12-20",

                ["Tier3_ShieldTraining_BlockPowerBonus"] =
                "【방패 방어력 보너스 (고정값)】\n" +
                "방패의 방어력을 증가시킵니다.\n" +
                "더 강한 공격도 막아낼 수 있습니다.\n" +
                "권장값: 80-120",

                // === Tier 4: 충격파 발산 (Ground Stomp) ===
                ["Tier4_GroundStomp_Radius"] =
                "【효과 반경 (미터)】\n" +
                "충격파가 미치는 범위입니다.\n" +
                "권장값: 2.5-4m",

                ["Tier4_GroundStomp_KnockbackForce"] =
                "【넉백 강도】\n" +
                "적을 밀어내는 힘입니다.\n" +
                "권장값: 15-25",

                ["Tier4_GroundStomp_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 100-150초",

                ["Tier4_GroundStomp_HPThreshold"] =
                "【자동 발동 체력 임계값 (비율)】\n" +
                "이 체력 이하가 되면 자동으로 발동됩니다.\n" +
                "0.35 = 35% 체력 이하\n" +
                "권장값: 0.30-0.40",

                ["Tier4_GroundStomp_VFXDuration"] =
                "【VFX 지속시간 (초)】\n" +
                "시각 효과가 표시되는 시간입니다.\n" +
                "권장값: 0.8-1.5초",

                // === Tier 4: 바위피부 (Rock Skin) ===
                ["Tier4_RockSkin_ArmorBonus"] =
                "【방어력 증폭 (%)】\n" +
                "투구, 흉갑, 각반, 방패 방어력에 각각 퍼센트 보너스를 적용합니다.\n" +
                "아이템 마우스 오버 시 ((기본방어력 + 스킬 추가방어력) * X%) 형식으로 표시됩니다.\n" +
                "권장값: 10-15%",

                // === Tier 5: 인내 & 민첩 & 회복 & 방어 숙련 ===
                ["Tier5_Endurance_RunStaminaReduction"] =
                "【달리기 스태미나 감소 (%)】\n" +
                "달리기 시 스태미나 소모를 줄입니다.\n" +
                "권장값: 8-15%",

                ["Tier5_Endurance_JumpStaminaReduction"] =
                "【점프 스태미나 감소 (%)】\n" +
                "점프 시 스태미나 소모를 줄입니다.\n" +
                "권장값: 8-15%",

                ["Tier5_Agility_DodgeBonus"] =
                "【회피율 보너스 (%)】\n" +
                "적의 공격을 회피할 확률을 추가로 증가시킵니다.\n" +
                "권장값: 3-8%",

                ["Tier5_Agility_RollStaminaReduction"] =
                "【구르기 스태미나 감소 (%)】\n" +
                "구르기 시 스태미나 소모를 줄입니다.\n" +
                "권장값: 10-18%",

                ["Tier5_TrollRegen_HPRegenBonus"] =
                "【체력 재생 보너스 (초당)】\n" +
                "트롤처럼 체력이 자동으로 회복됩니다.\n" +
                "권장값: 3-8",

                ["Tier5_TrollRegen_RegenInterval"] =
                "【재생 간격 (초)】\n" +
                "체력이 회복되는 주기입니다.\n" +
                "권장값: 1.5-3초",

                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] =
                "【방패 방어력 보너스 (고정값)】\n" +
                "방패의 방어력을 대폭 증가시킵니다.\n" +
                "권장값: 80-120",

                ["Tier5_BlockMaster_ParryDurationBonus"] =
                "【패링 지속시간 보너스 (초)】\n" +
                "패링 성공 후 효과 지속 시간을 연장합니다.\n" +
                "권장값: 0.8-1.5초",

                // === Tier 6: 최종 방어 기술 ===
                ["Tier6_NerveEnhancement_DodgeBonus"] =
                "【회피율 영구 보너스 (%)】\n" +
                "신경 강화로 회피율이 영구적으로 증가합니다.\n" +
                "권장값: 3-8%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【체력 보너스 (%)】\n" +
                "요툰의 생명력으로 최대 체력이 비율로 증가합니다.\n" +
                "권장값: 25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【방어력 보너스 (%)】\n" +
                "요툰의 갑옷으로 방어력이 비율로 증가합니다.\n" +
                "권장값: 8-15%",

                ["Tier6_JotunnShield_BlockStaminaReduction"] =
                "【방어 스태미나 감소 (%)】\n" +
                "방패로 막을 때 스태미나 소모를 줄입니다.\n" +
                "권장값: 20-30%",

                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] =
                "【일반 방패 이동속도 보너스 (%)】\n" +
                "일반 방패 들고 있을 때 이동 속도가 증가합니다.\n" +
                "권장값: 3-8%",

                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] =
                "【타워 실드 이동속도 보너스 (%)】\n" +
                "타워 실드 들고 있을 때 이동 속도가 증가합니다.\n" +
                "권장값: 8-15%",

                // ========================================
                // Production Tree (생산 트리)
                // ========================================

                // === Production Tree: 필요 포인트 (5개) ===
                ["Tier0_ProductionExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_NoviceWorker_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_Specialization_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_IntermediateSkill_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_AdvancedSkill_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 0: 생산 전문가 (Production Expert) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] =
                "【나무 +1 보너스 확률 (%)】\n" +
                "나무를 벨 때 추가로 1개를 더 얻을 확률입니다.\n" +
                "생산의 기본이 되는 핵심 스킬입니다.\n" +
                "권장값: 40-60%",

                // === Tier 1: 초보 일꾼 (Novice Worker) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] =
                "【나무 +1 보너스 확률 (%)】\n" +
                "나무를 벨 때 추가로 1개를 더 얻을 확률이 증가합니다.\n" +
                "권장값: 20-30%",

                // === Tier 2: 전문 분야 (Specialization) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] =
                "【나무 +1 보너스 확률 (%)】\n" +
                "벌목 Lv2 - 나무를 벨 때 추가 보너스 확률입니다.\n" +
                "권장값: 20-30%",

                ["Tier2_GatheringLv2_BonusChance"] =
                "【아이템 +1 보너스 확률 (%)】\n" +
                "채집 Lv2 - 베리, 버섯 등 채집 시 추가 보너스 확률입니다.\n" +
                "권장값: 20-30%",

                ["Tier2_MiningLv2_BonusChance"] =
                "【광석 +1 보너스 확률 (%)】\n" +
                "채광 Lv2 - 광석 채집 시 추가 보너스 확률입니다.\n" +
                "권장값: 20-30%",

                ["Tier2_CraftingLv2_UpgradeChance"] =
                "【업그레이드 +1 보너스 확률 (%)】\n" +
                "제작 Lv2 - 아이템 제작/업그레이드 시 추가 업그레이드 레벨을 얻을 확률입니다.\n" +
                "권장값: 20-30%",

                ["Tier2_CraftingLv2_DurabilityBonus"] =
                "【내구도 최대치 증가 (%)】\n" +
                "제작 Lv2 - 제작한 아이템의 최대 내구도가 증가합니다.\n" +
                "권장값: 20-30%",

                // === Tier 3: 중급 스킬 (Intermediate Skills) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] =
                "【나무 +2 보너스 확률 (%)】\n" +
                "벌목 Lv3 - 나무를 벨 때 2개를 더 얻을 확률입니다.\n" +
                "권장값: 30-40%",

                ["Tier3_GatheringLv3_BonusChance"] =
                "【아이템 +1 보너스 확률 (%)】\n" +
                "채집 Lv3 - 채집 시 추가 보너스 확률이 증가합니다.\n" +
                "권장값: 20-30%",

                ["Tier3_MiningLv3_BonusChance"] =
                "【광석 +1 보너스 확률 (%)】\n" +
                "채광 Lv3 - 광석 채집 시 추가 보너스 확률이 증가합니다.\n" +
                "권장값: 20-30%",

                ["Tier3_CraftingLv3_UpgradeChance"] =
                "【업그레이드 +1 보너스 확률 (%)】\n" +
                "제작 Lv3 - 아이템 제작/업그레이드 시 추가 업그레이드 확률이 증가합니다.\n" +
                "권장값: 20-30%",

                ["Tier3_CraftingLv3_DurabilityBonus"] =
                "【내구도 최대치 증가 (%)】\n" +
                "제작 Lv3 - 제작한 아이템의 최대 내구도가 추가로 증가합니다.\n" +
                "권장값: 20-30%",

                // === Tier 4: 고급 스킬 (Advanced Skills) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] =
                "【나무 +2 보너스 확률 (%)】\n" +
                "벌목 Lv4 - 나무를 벨 때 2개를 더 얻을 확률이 대폭 증가합니다.\n" +
                "권장값: 40-50%",

                ["Tier4_GatheringLv4_BonusChance"] =
                "【아이템 +1 보너스 확률 (%)】\n" +
                "채집 Lv4 - 채집 시 추가 보너스 확률이 최대로 증가합니다.\n" +
                "권장값: 20-30%",

                ["Tier4_MiningLv4_BonusChance"] =
                "【광석 +1 보너스 확률 (%)】\n" +
                "채광 Lv4 - 광석 채집 시 추가 보너스 확률이 최대로 증가합니다.\n" +
                "권장값: 20-30%",

                ["Tier4_CraftingLv4_UpgradeChance"] =
                "【업그레이드 +1 보너스 확률 (%)】\n" +
                "제작 Lv4 - 아이템 제작/업그레이드 시 추가 업그레이드 확률이 최대로 증가합니다.\n" +
                "권장값: 20-30%",

                ["Tier4_CraftingLv4_DurabilityBonus"] =
                "【내구도 최대치 증가 (%)】\n" +
                "제작 Lv4 - 제작한 아이템의 최대 내구도가 최대로 증가합니다.\n" +
                "권장값: 20-30%",

                // ========================================
                // Staff Tree (지팡이 트리)
                // ========================================

                // === Staff Tree: 필요 포인트 (8개) ===
                ["Tier0_StaffExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_FireElement_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 0: 지팡이 전문가 (Staff Expert) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【속성 데미지 보너스 (%)】\n" +
                "지팡이의 속성 데미지(불, 얼음, 번개)를 증가시킵니다.\n" +
                "마법 공격의 기본이 되는 핵심 스킬입니다.\n" +
                "권장값: 10-15%",

                // === Tier 1: 정신 집중 & 마력 흐름 ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【Eitr 소모 감소 (%)】\n" +
                "정신 집중으로 지팡이 주문의 Eitr 소모를 줄입니다.\n" +
                "더 많은 마법을 사용할 수 있습니다.\n" +
                "권장값: 12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【최대 Eitr 보너스 (고정값)】\n" +
                "마력 흐름으로 최대 Eitr를 증가시킵니다.\n" +
                "권장값: 25-35",

                // === Tier 2: 마법 증폭 (Magic Amplify) ===
                ["Tier2_MagicAmplify_Chance"] =
                "【마법 증폭 발동 확률 (%)】\n" +
                "속성 공격이 증폭될 확률입니다.\n" +
                "권장값: 30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【마법 증폭 속성 공격 보너스 (%)】\n" +
                "발동 시 속성 데미지 증가량입니다.\n" +
                "권장값: 30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【마법 증폭 에이트르 소모 증가 (%)】\n" +
                "마법 시전 시 에이트르 소모량이 증가합니다.\n" +
                "강력한 마법의 대가로 작용합니다.\n" +
                "권장값: 15-25%",

                // === Tier 3: 속성 강화 (Elemental Enhancement) ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【얼음 공격력 보너스 (고정값)】\n" +
                "얼음 속성 마법의 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【불 공격력 보너스 (고정값)】\n" +
                "불 속성 마법의 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【번개 공격력 보너스 (고정값)】\n" +
                "번개 속성 마법의 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 2-5",

                // === Tier 4: 행운의 마력 (Lucky Mana) ===
                ["Tier4_LuckyMana_Chance"] =
                "【Eitr 무소모 발동 확률 (%)】\n" +
                "주문 사용 시 Eitr를 소모하지 않을 확률입니다.\n" +
                "행운의 마력으로 무한 시전이 가능합니다.\n" +
                "권장값: 30-40%",

                // === Tier 5-1: 이중 시전 (Double Cast - R키 액티브) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【추가 투사체 개수】\n" +
                "이중 시전 시 추가로 발사되는 투사체 개수입니다.\n" +
                "권장값: 1-3개",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【투사체 데미지 비율 (%)】\n" +
                "추가 투사체의 데미지 비율입니다.\n" +
                "권장값: 60-80%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【투사체 각도 오프셋 (도)】\n" +
                "투사체가 퍼지는 각도입니다.\n" +
                "권장값: 3-8도",

                ["Tier5_DoubleCast_EitrCost"] =
                "【Eitr 소모량】\n" +
                "스킬 활성화 시 소모되는 Eitr입니다.\n" +
                "권장값: 15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 25-40초",

                // === Tier 5-2: 즉시 범위 힐 (Instant Area Heal - H키 액티브) ===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 25-40초",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【Eitr 소모량】\n" +
                "스킬 사용 시 소모되는 Eitr입니다.\n" +
                "권장값: 25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【회복량 (최대 HP 대비 %)】\n" +
                "최대 체력 대비 회복되는 비율입니다.\n" +
                "권장값: 20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【치유 범위 (미터)】\n" +
                "힐이 적용되는 범위입니다.\n" +
                "권장값: 10-15m",

                ["Tier5_InstantAreaHeal_SelfHeal"] =
                "【자가 치유 허용】\n" +
                "true로 설정하면 자신도 치유됩니다.\n" +
                "권장값: false (아군만 치유)",

                // === Crossbow Tree ===
                // === Tier 0: 석궁 전문가 (Crossbow Expert) ===
                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【석궁 공격력 보너스 (%)】\n" +
                "석궁 및 볼트 무기의 기본 공격력을 증가시킵니다.\n" +
                "권장값: 8-12%",

                // === Tier 1: 연발 (Rapid Fire) ===
                ["Tier1_RapidFire_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_RapidFire_Chance"] =
                "【연발 발동 확률 (%)】\n" +
                "석궁 발사 시 연발이 발동될 확률입니다.\n" +
                "연발 발동 시 여러 발을 빠르게 발사합니다.\n" +
                "권장값: 15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【연발 발사 횟수】\n" +
                "연발 발동 시 추가로 발사되는 볼트 개수입니다.\n" +
                "권장값: 2-4발",

                ["Tier1_RapidFire_DamagePercent"] =
                "【연발 데미지 비율 (%)】\n" +
                "연발로 발사되는 볼트의 데미지 비율입니다.\n" +
                "원래 공격력 대비 비율입니다.\n" +
                "권장값: 60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【연발 간격 (초)】\n" +
                "연발 시 각 볼트 발사 사이의 시간 간격입니다.\n" +
                "짧을수록 빠르게 발사됩니다.\n" +
                "권장값: 0.1-0.3초",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【연발 볼트 소모량】\n" +
                "연발 시 소모되는 볼트 개수입니다.\n" +
                "권장값: 1-2개",

                ["Tier2_CrossbowSkills_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 2: 균형잡힌 조준 (Balanced Aim) ===
                ["Tier2_BalancedAim_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_BalancedAim_KnockbackChance"] =
                "【넉백 발동 확률 (%)】\n" +
                "석궁 명중 시 적을 밀어낼 확률입니다.\n" +
                "균형잡힌 자세에서 안정적인 충격을 줍니다.\n" +
                "권장값: 20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【넉백 거리 (미터)】\n" +
                "넉백 발동 시 적을 밀어내는 거리입니다.\n" +
                "권장값: 2-4m",

                // === Tier 2: 신속 장전 (Rapid Reload) ===
                ["Tier2_RapidReload_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_RapidReload_SpeedIncrease"] =
                "【장전 속도 증가 (%)】\n" +
                "석궁 재장전 속도를 증가시킵니다.\n" +
                "더 빠르게 다음 볼트를 장전할 수 있습니다.\n" +
                "권장값: 10-20%",

                // === Tier 2: 정직한 사격 (Honest Shot) ===
                ["Tier2_HonestShot_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_HonestShot_DamageBonus"] =
                "【기본 데미지 보너스 (%)】\n" +
                "석궁의 기본 공격력을 추가로 증가시킵니다.\n" +
                "권장값: 10-18%",

                // === Tier 3: 자동 재장전 (Auto Reload) ===
                ["Tier3_AutoReload_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_AutoReload_Chance"] =
                "【자동 장전 발동 확률 (%)】\n" +
                "석궁 명중 시 다음 재장전 속도가 200%로 증가할 확률입니다.\n" +
                "연속 공격 흐름을 유지하는 데 도움을 줍니다.\n" +
                "권장값: 20-35%",

                // === Tier 4: 연발 Lv2 (Rapid Fire Lv2) ===
                ["Tier4_CrossbowSkills_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_RapidFireLv2_Chance"] =
                "【연발 Lv2 발동 확률 (%)】\n" +
                "강화된 연발이 발동될 확률입니다.\n" +
                "Tier 1 연발과 확률이 합산됩니다.\n" +
                "권장값: 20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【연발 Lv2 발사 횟수】\n" +
                "강화된 연발의 추가 볼트 개수입니다.\n" +
                "Tier 1 연발보다 더 많이 발사됩니다.\n" +
                "권장값: 4-6발",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【연발 Lv2 데미지 비율 (%)】\n" +
                "강화된 연발 볼트의 데미지 비율입니다.\n" +
                "Tier 1보다 데미지가 증가합니다.\n" +
                "권장값: 75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【연발 Lv2 발사 간격 (초)】\n" +
                "강화된 연발 시 각 볼트 발사 사이의 시간 간격입니다.\n" +
                "권장값: 0.1-0.3초",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【연발 Lv2 볼트 소모량】\n" +
                "강화된 연발 시 소모되는 볼트 개수입니다.\n" +
                "권장값: 1-2개",

                // === Tier 4: 최후의 일격 (Final Strike) ===
                ["Tier4_FinalStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_FinalStrike_HpThreshold"] =
                "【적 체력 임계값 (%)】\n" +
                "이 체력 이상인 적에게 추가 데미지를 줍니다.\n" +
                "높은 체력의 적을 상대할 때 효과적입니다.\n" +
                "권장값: 40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【추가 데미지 보너스 (%)】\n" +
                "임계값 이상 체력의 적에게 주는 추가 데미지입니다.\n" +
                "권장값: 20-40%",

                // === Tier 5: 원샷 (One Shot - R키 액티브) ===
                ["Tier5_OneShot_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_OneShot_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "원샷 버프가 유지되는 시간입니다.\n" +
                "이 시간 동안 강화된 한 발을 발사할 수 있습니다.\n" +
                "권장값: 8-12초",

                ["Tier5_OneShot_DamageBonus"] =
                "【원샷 데미지 보너스 (%)】\n" +
                "원샷 발사 시 추가되는 데미지 보너스입니다.\n" +
                "치명적인 한 발의 위력입니다.\n" +
                "권장값: 150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【넉백 거리 (미터)】\n" +
                "원샷 명중 시 적을 밀어내는 거리입니다.\n" +
                "강력한 충격으로 적을 밀어냅니다.\n" +
                "권장값: 5-10m",

                ["Tier5_OneShot_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 25-40초",

                // === Bow Tree ===
                // === Tier 0: 활 전문가 (Bow Expert) ===
                ["Tier0_BowExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier0_BowExpert_DamageBonus"] =
                "【활 공격력 보너스 (%)】\n" +
                "활과 화살 무기의 기본 공격력을 증가시킵니다.\n" +
                "권장값: 8-12%",

                // === Tier 1: 집중 사격 (Focused Shot) ===
                ["Tier1_FocusedShot_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_FocusedShot_CritBonus"] =
                "【치명타 확률 보너스 (%)】\n" +
                "집중 사격으로 크리티컬 확률을 증가시킵니다.\n" +
                "조준에 집중할수록 치명타 기회가 높아집니다.\n" +
                "권장값: 5-10%",

                // === Tier 2: 멀티샷 Lv1 ===
                ["Tier2_MultishotLv1_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_MultishotLv1_ActivationChance"] =
                "【멀티샷 Lv1 발동 확률 (%)】\n" +
                "활 공격 시 멀티샷이 발동될 확률입니다.\n" +
                "발동 시 여러 화살을 동시에 발사합니다.\n" +
                "권장값: 15-25%",

                ["Tier2_Multishot_AdditionalArrows"] =
                "【추가 화살 개수】\n" +
                "멀티샷 발동 시 추가로 발사되는 화살 개수입니다.\n" +
                "권장값: 2-4개",

                ["Tier2_Multishot_ArrowConsumption"] =
                "【화살 소모량】\n" +
                "멀티샷 발동 시 소모되는 화살 개수입니다.\n" +
                "권장값: 1-2개",

                ["Tier2_Multishot_DamagePerArrow"] =
                "【화살당 데미지 비율 (%)】\n" +
                "멀티샷으로 발사되는 각 화살의 데미지 비율입니다.\n" +
                "권장값: 50-70%",

                // === Tier 3: 활 숙련 (Bow Mastery) ===
                ["Tier3_BowMastery_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_SpeedShot_SkillBonus"] =
                "【활 숙련도 보너스 (고정값)】\n" +
                "활 기술 레벨을 고정값으로 증가시킵니다.\n" +
                "숙련도가 높아질수록 더 강력해집니다.\n" +
                "권장값: 5-10",

                ["Tier3_SilentShot_DamageBonus"] =
                "【관통 데미지 보너스 (고정값)】\n" +
                "활 공격력을 고정 수치로 증가시킵니다.\n" +
                "화살이 적을 관통하여 더 큰 피해를 입힙니다.\n" +
                "권장값: 3-8",

                ["Tier3_SpecialArrow_Chance"] =
                "【특수 화살 발동 확률 (%)】\n" +
                "특수 효과를 가진 화살이 발사될 확률입니다.\n" +
                "독, 불, 얼음 등의 상태이상 화살이 발사됩니다.\n" +
                "권장값: 25-35%",

                // === Tier 4: 멀티샷 Lv2 ===
                ["Tier4_MultishotLv2_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_MultishotLv2_ActivationChance"] =
                "【멀티샷 Lv2 발동 확률 (%)】\n" +
                "강화된 멀티샷의 발동 확률입니다.\n" +
                "Lv1보다 많은 화살을 발사합니다.\n" +
                "권장값: 20-30%",

                ["Tier4_PowerShot_KnockbackChance"] =
                "【강력한 넉백 확률 (%)】\n" +
                "활 공격 시 적을 강하게 밀어낼 확률입니다.\n" +
                "거리 유지와 적 제어에 유용합니다.\n" +
                "권장값: 30-40%",

                ["Tier4_PowerShot_KnockbackDistance"] =
                "【넉백 거리 (미터)】\n" +
                "적이 밀려나는 거리입니다.\n" +
                "권장값: 4-8m",

                // === Tier 5: 정조준 및 고급 스킬 ===
                ["Tier5_PrecisionAim_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_PrecisionAim_CritDamage"] =
                "【크리티컬 데미지 보너스 (%)】\n" +
                "정조준으로 치명타 피해를 증가시킵니다.\n" +
                "약점을 노려 더 큰 피해를 입힙니다.\n" +
                "권장값: 25-40%",

                ["Tier5_ArrowRain_Chance"] =
                "【화살비 발동 확률 (%)】\n" +
                "여러 화살을 빠르게 연속 발사할 확률입니다.\n" +
                "화살의 비처럼 쏟아집니다.\n" +
                "권장값: 25-35%",

                ["Tier5_ArrowRain_ArrowCount"] =
                "【화살비 발사 개수】\n" +
                "화살비 발동 시 발사되는 화살 개수입니다.\n" +
                "권장값: 3-5개",

                ["Tier5_BackstepShot_CritBonus"] =
                "【회피 후 치명타 보너스 (%)】\n" +
                "구르기(회피) 직후 치명타 확률이 증가합니다.\n" +
                "백스텝 후 빠른 역습이 가능합니다.\n" +
                "권장값: 20-35%",

                ["Tier5_BackstepShot_Duration"] =
                "【회피 후 버프 지속시간 (초)】\n" +
                "구르기 후 효과가 유지되는 시간입니다.\n" +
                "권장값: 2-4초",

                ["Tier5_HuntingInstinct_CritBonus"] =
                "【사냥 본능 치명타 보너스 (%)】\n" +
                "사냥꾼의 본능으로 크리티컬 확률이 증가합니다.\n" +
                "권장값: 8-15%",

                // === Tier 6: 폭발 화살 (R키 액티브) ===
                ["Tier6_ExplosiveArrow_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier6_ExplosiveArrow_DamageMultiplier"] =
                "【폭발 화살 데미지 배율 (%)】\n" +
                "R키 액티브 스킬 - 폭발 화살의 데미지 배율입니다.\n" +
                "범위 피해를 입히는 강력한 화살입니다.\n" +
                "권장값: 100-150%",

                ["Tier6_ExplosiveArrow_Radius"] =
                "【폭발 범위 (미터)】\n" +
                "폭발 화살의 피해 범위입니다.\n" +
                "권장값: 3-6m",

                ["Tier6_ExplosiveArrow_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 15-25초",

                ["Tier6_ExplosiveArrow_StaminaCost"] =
                "【스태미나 소모 (%)】\n" +
                "스킬 사용 시 소모되는 스태미나 비율입니다.\n" +
                "권장값: 10-20%",

                // === Tier 6: 크리티컬 부스트 ===
                ["Tier6_CritBoost_DamageBonus"] =
                "【크리티컬 부스트 데미지 (%)】\n" +
                "크리티컬 히트 시 추가 데미지 보너스입니다.\n" +
                "권장값: 40-60%",

                ["Tier6_CritBoost_CritChance"] =
                "【크리티컬 부스트 확률 (%)】\n" +
                "치명타 확률을 대폭 증가시킵니다.\n" +
                "권장값: 80-100%",

                ["Tier6_CritBoost_ArrowCount"] =
                "【크리티컬 부스트 화살 개수】\n" +
                "크리티컬 부스트 발동 시 발사되는 화살 개수입니다.\n" +
                "권장값: 3-7개",

                ["Tier6_CritBoost_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 35-50초",

                ["Tier6_CritBoost_StaminaCost"] =
                "【스태미나 소모 (%)】\n" +
                "스킬 사용 시 소모되는 스태미나 비율입니다.\n" +
                "권장값: 20-30%",

                // === Sword Tree: 필요 포인트 (9개) ===
                ["Sword_Expert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_FastSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_CounterStance_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_ComboSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_BladeReflect_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_OffenseDefense_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_TrueDuel_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_ParryCharge_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Sword_RushSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Sword Tree: 검 전문가 (1개) ===
                ["Sword_Expert_DamageIncrease"] =
                "【검 피해 증가 (%)】\n" +
                "검 무기의 기본 공격력을 증가시킵니다.\n" +
                "모든 검류에 적용됩니다.\n" +
                "권장값: 10-20%",

                // === Sword Tree: 빠른 베기 (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "검 공격속도를 증가시킵니다.\n" +
                "빠른 연속 공격이 가능합니다.\n" +
                "권장값: 10-20%",

                // === Sword Tree: 반격 자세 (2개) ===
                ["Tier1_CounterStance_Duration"] =
                "【지속시간 (초)】\n" +
                "반격 자세를 유지하는 시간입니다.\n" +
                "이 시간 동안 방어력이 증가합니다.\n" +
                "권장값: 3-6초",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【방어력 보너스 (%)】\n" +
                "반격 자세 중 방어력 증가량입니다.\n" +
                "적의 공격을 버티며 반격 기회를 노립니다.\n" +
                "권장값: 20-40%",

                // === Sword Tree: 연속 베기 (2개) ===
                ["Sword_ComboSlash_Bonus"] =
                "【연속 공격 보너스 (%)】\n" +
                "연속 공격 시 추가 피해를 입힙니다.\n" +
                "콤보를 유지하면 높은 DPS를 낼 수 있습니다.\n" +
                "권장값: 15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "연속 공격 보너스가 유지되는 시간입니다.\n" +
                "이 시간 내에 다시 공격하면 버프가 연장됩니다.\n" +
                "권장값: 3-5초",

                // === Sword Tree: 칼날 되치기 (1개) ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【공격력 보너스 (고정값)】\n" +
                "칼날 되치기 공격력을 고정 수치로 증가시킵니다.\n" +
                "패링 후 강력한 반격이 가능합니다.\n" +
                "권장값: 20-40",

                // === Sword Tree: 공방일체 (2개) ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【공격력 보너스 (%)】\n" +
                "공격과 방어를 동시에 강화합니다.\n" +
                "균형잡힌 전투 스타일에 유용합니다.\n" +
                "권장값: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【방어력 보너스 (고정값)】\n" +
                "공방일체 자세의 방어력 보너스입니다.\n" +
                "공격하면서도 튼튼한 방어가 가능합니다.\n" +
                "권장값: 15-30",

                // === Sword Tree: 진검승부 (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "1:1 전투에 특화된 공격속도 보너스입니다.\n" +
                "빠른 연타로 적을 압도합니다.\n" +
                "권장값: 15-30%",

                // === Sword Tree: 패링 돌격 (H키 액티브 - 5개) ===
                ["Sword_ParryCharge_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "패링 성공 후 버프가 유지되는 시간입니다.\n" +
                "이 시간 동안 강화된 공격이 가능합니다.\n" +
                "권장값: 5-10초",

                ["Sword_ParryCharge_DamageBonus"] =
                "【돌격 공격력 보너스 (%)】\n" +
                "패링 성공 시 돌격 공격의 피해 증가량입니다.\n" +
                "완벽한 타이밍에 강력한 반격을 날립니다.\n" +
                "권장값: 50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【밀어내기 거리 (m)】\n" +
                "돌격 시 적을 밀어내는 거리입니다.\n" +
                "거리 조절과 전장 제어에 유용합니다.\n" +
                "권장값: 3-7m",

                ["Sword_ParryCharge_StaminaCost"] =
                "【스태미나 소모】\n" +
                "H키 버프 활성화 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 10-20초",

                // === Sword Tree: 돌진 연속 베기 (G키 액티브 - 9개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【1차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 첫 번째 공격 피해 비율입니다.\n" +
                "기본 공격력 대비 배율입니다.\n" +
                "권장값: 80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【2차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 두 번째 공격 피해 비율입니다.\n" +
                "콤보가 이어지며 피해가 증가합니다.\n" +
                "권장값: 100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【3차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 마지막 공격 피해 비율입니다.\n" +
                "가장 강력한 피니시 공격입니다.\n" +
                "권장값: 150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【초기 돌진 거리 (m)】\n" +
                "스킬 시작 시 돌진하는 거리입니다.\n" +
                "빠르게 적에게 접근합니다.\n" +
                "권장값: 5-10m",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【측면 이동 거리 (m)】\n" +
                "연속 공격 중 좌우로 이동하는 거리입니다.\n" +
                "적의 공격을 회피하며 공격합니다.\n" +
                "권장값: 2-5m",

                ["Sword_RushSlash_StaminaCost"] =
                "【스태미나 소모량】\n" +
                "G키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "강력한 만큼 많은 스태미나가 필요합니다.\n" +
                "권장값: 40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 15-30초",

                ["Sword_RushSlash_MovementSpeed"] =
                "【이동 속도 (m/s)】\n" +
                "돌진 중 이동 속도입니다.\n" +
                "빠를수록 역동적인 전투가 가능합니다.\n" +
                "권장값: 8-15 m/s",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【공격 속도 보너스 (%)】\n" +
                "스킬 중 공격속도 보너스 (기본 대비)입니다.\n" +
                "다른 트리의 공격속도는 무시되고 이 값만 적용됩니다.\n" +
                "권장값: 20-40%",

                // === Sword Tree: 새 Tier* 형식 키 (현재 Sword_Config.cs와 일치) ===
                ["Tier0_SwordExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier0_SwordExpert_DamageBonus"] =
                "【검 피해 증가 (%)】\n" +
                "검 무기의 기본 공격력을 증가시킵니다.\n" +
                "모든 검류에 적용됩니다.\n" +
                "권장값: 10-20%",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "검 공격속도를 증가시킵니다.\n" +
                "빠른 연속 공격이 가능합니다.\n" +
                "권장값: 10-20%",

                ["Tier2_ComboSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【연속 공격 보너스 (%)】\n" +
                "연속 공격 시 추가 피해를 입힙니다.\n" +
                "콤보를 유지하면 높은 DPS를 낼 수 있습니다.\n" +
                "권장값: 15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "연속 공격 보너스가 유지되는 시간입니다.\n" +
                "이 시간 내에 다시 공격하면 버프가 연장됩니다.\n" +
                "권장값: 3-5초",

                ["Tier3_Riposte_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_Riposte_DamageBonus"] =
                "【공격력 보너스 (고정값)】\n" +
                "칼날 되치기 공격력을 고정 수치로 증가시킵니다.\n" +
                "패링 후 강력한 반격이 가능합니다.\n" +
                "권장값: 5-15",

                ["Tier4_AllInOne_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "1:1 전투에 특화된 공격속도 보너스입니다.\n" +
                "빠른 연타로 적을 압도합니다.\n" +
                "권장값: 15-30%",

                ["Tier5_ParryRush_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_ParryRush_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "패링 성공 후 버프가 유지되는 시간입니다.\n" +
                "이 시간 동안 강화된 공격이 가능합니다.\n" +
                "권장값: 20-40초",

                ["Tier5_ParryRush_DamageBonus"] =
                "【돌격 공격력 보너스 (%)】\n" +
                "패링 성공 시 돌격 공격의 피해 증가량입니다.\n" +
                "완벽한 타이밍에 강력한 반격을 날립니다.\n" +
                "권장값: 50-100%",

                ["Tier5_ParryRush_PushDistance"] =
                "【밀어내기 거리 (m)】\n" +
                "돌격 시 적을 밀어내는 거리입니다.\n" +
                "거리 조절과 전장 제어에 유용합니다.\n" +
                "권장값: 3-7m",

                ["Tier5_ParryRush_StaminaCost"] =
                "【스태미나 소모】\n" +
                "스킬 활성화 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 30-60초",

                ["Tier6_RushSlash_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【1차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 첫 번째 공격 피해 비율입니다.\n" +
                "기본 공격력 대비 배율입니다.\n" +
                "권장값: 60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【2차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 두 번째 공격 피해 비율입니다.\n" +
                "콤보가 이어지며 피해가 증가합니다.\n" +
                "권장값: 70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【3차 공격력 비율 (%)】\n" +
                "돌진 연속 베기의 마지막 공격 피해 비율입니다.\n" +
                "가장 강력한 피니시 공격입니다.\n" +
                "권장값: 80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【초기 돌진 거리 (m)】\n" +
                "스킬 시작 시 돌진하는 거리입니다.\n" +
                "빠르게 적에게 접근합니다.\n" +
                "권장값: 3-8m",

                ["Tier6_RushSlash_SideDistance"] =
                "【측면 이동 거리 (m)】\n" +
                "연속 공격 중 좌우로 이동하는 거리입니다.\n" +
                "적의 공격을 회피하며 공격합니다.\n" +
                "권장값: 2-5m",

                ["Tier6_RushSlash_StaminaCost"] =
                "【스태미나 소모량】\n" +
                "G키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "강력한 만큼 많은 스태미나가 필요합니다.\n" +
                "권장값: 20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 15-30초",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【이동 속도 (m/s)】\n" +
                "돌진 중 이동 속도입니다.\n" +
                "빠를수록 역동적인 전투가 가능합니다.\n" +
                "권장값: 10-25 m/s",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【공격 속도 보너스 (%)】\n" +
                "스킬 중 공격속도 보너스 (기본 대비)입니다.\n" +
                "다른 트리의 공격속도는 무시되고 이 값만 적용됩니다.\n" +
                "권장값: 150-250%",

                // === Knife Tree ===
                // === Tier 0: 단검 전문가 (Knife Expert) ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【백스탭 데미지 보너스 (%)】\n" +
                "적의 뒤에서 공격 시 추가 데미지를 입힙니다.\n" +
                "암살자의 기본 기술입니다.\n" +
                "권장값: 30-50%",

                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 1: 회피 숙련 (Evasion Mastery) ===
                ["Tier1_Evasion_Chance"] =
                "【회피 확률 (%)】\n" +
                "적의 공격을 회피할 확률입니다.\n" +
                "높을수록 피격을 피할 수 있습니다.\n" +
                "권장값: 15-25%",

                ["Tier1_Evasion_Duration"] =
                "【무적 지속시간 (초)】\n" +
                "회피 성공 시 무적 상태가 유지되는 시간입니다.\n" +
                "권장값: 2-4초",

                ["Tier1_Evasion_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 2: 빠른 움직임 (Fast Movement) ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【이동 속도 보너스 (%)】\n" +
                "기본 이동 속도를 증가시킵니다.\n" +
                "빠른 기동력으로 적을 농락합니다.\n" +
                "권장값: 10-20%",

                ["Tier2_FastMove_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 3: 전투 숙련 (Combat Mastery) ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【데미지 보너스 (고정값)】\n" +
                "공격 시 고정 데미지를 추가합니다.\n" +
                "권장값: 1-4",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "전투 숙련 버프가 유지되는 시간입니다.\n" +
                "권장값: 8-12초",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 4: 공격과 회피 (Attack & Evasion) ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【회피율 보너스 (%)】\n" +
                "공격과 동시에 회피율을 증가시킵니다.\n" +
                "공격적인 방어가 가능합니다.\n" +
                "권장값: 20-30%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "회피율 증가 효과가 유지되는 시간입니다.\n" +
                "권장값: 8-12초",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "버프 효과의 재사용 대기 시간입니다.\n" +
                "권장값: 25-35초",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 5: 치명적 피해 (Critical Damage) ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【데미지 보너스 (%)】\n" +
                "치명타 데미지를 증가시킵니다.\n" +
                "단검의 높은 크리티컬과 시너지가 좋습니다.\n" +
                "권장값: 20-35%",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 6: 암살자 (Assassin) ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【크리티컬 데미지 보너스 (%)】\n" +
                "치명타 공격의 피해를 더욱 증가시킵니다.\n" +
                "권장값: 20-30%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【크리티컬 확률 보너스 (%)】\n" +
                "치명타 발동 확률을 증가시킵니다.\n" +
                "권장값: 10-18%",

                ["Tier6_Assassin_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 7: 암살술 (Assassination) ===
                ["Tier7_Assassination_StaggerChance"] =
                "【스태거 확률 (%)】\n" +
                "연속 공격 시 적을 비틀거리게 할 확률입니다.\n" +
                "적의 공격을 중단시킵니다.\n" +
                "권장값: 30-45%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【필요 콤보 횟수】\n" +
                "스태거 효과 발동에 필요한 연속 공격 횟수입니다.\n" +
                "권장값: 2-4회",

                ["Tier7_Assassination_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 8: 암살자의 심장 (G키 액티브) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【크리티컬 데미지 배율】\n" +
                "G키 액티브 - 암살자의 심장 발동 시 크리티컬 데미지 배율입니다.\n" +
                "적의 뒤로 순간이동하여 치명적인 연속 공격을 합니다.\n" +
                "권장값: 1.2-1.5배",

                ["Tier8_AssassinHeart_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "암살자의 심장 버프가 유지되는 시간입니다.\n" +
                "권장값: 5-10초",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【스태미나 소모】\n" +
                "스킬 사용 시 소모되는 스태미나입니다.\n" +
                "권장값: 15-25",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "권장값: 35-50초",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【순간이동 탐색 범위 (미터)】\n" +
                "적을 탐색하는 최대 거리입니다.\n" +
                "이 범위 내의 적 뒤로 순간이동합니다.\n" +
                "권장값: 6-10m",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【적 뒤 이동 거리 (미터)】\n" +
                "적의 뒤로 이동하는 거리입니다.\n" +
                "백스탭 각도를 잡기 위한 거리입니다.\n" +
                "권장값: 0.8-1.5m",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【스턴 지속시간 (초)】\n" +
                "적을 기절시키는 시간입니다.\n" +
                "권장값: 0.5-2초",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【연속 공격 횟수】\n" +
                "순간이동 후 자동으로 공격하는 횟수입니다.\n" +
                "권장값: 2-4회",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【공격 간격 (초)】\n" +
                "연속 공격 사이의 시간 간격입니다.\n" +
                "빠를수록 순식간에 공격합니다.\n" +
                "권장값: 0.2-0.5초",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // ========================================
                // Spear Tree (Tier 0~5, 35 keys)
                // ========================================

                // === Spear Tree: 필요 포인트 (7개) ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_QuickStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_Throw_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_Pierce_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_Evasion_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_Penetrate_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_Combo_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Spear Tree: Tier 0 - 창 전문가 (3개) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【2연타 후 공격속도 보너스 (%)】\n" +
                "창으로 2회 연속 공격 후 공격속도가 증가합니다.\n" +
                "빠른 연속 공격으로 적을 압도합니다.\n" +
                "권장값: 10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【2연타 후 공격력 보너스 (%)】\n" +
                "창으로 2회 연속 공격 후 공격력이 증가합니다.\n" +
                "콤보 공격의 피해를 극대화합니다.\n" +
                "권장값: 7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【버프 지속시간 (초)】\n" +
                "2연타 효과가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 전투가 가능합니다.\n" +
                "권장값: 4-8초",

                // === Spear Tree: Tier 1 - 급소 찌르기 (1개) ===
                ["Tier1_VitalStrike_DamageBonus"] =
                "【치명타 피해 보너스 (%)】\n" +
                "창 공격의 치명타 피해를 증가시킵니다.\n" +
                "급소를 노리는 정밀 공격에 특화됩니다.\n" +
                "권장값: 20-40%",

                // === Spear Tree: Tier 2 - 투창 (3개) ===
                ["Tier2_Throw_Cooldown"] =
                "【투창 쿨타임 (초)】\n" +
                "투창 스킬 재사용 대기시간입니다.\n" +
                "짧을수록 자주 투창 가능합니다.\n" +
                "권장값: 20-40초",

                ["Tier2_Throw_DamageMultiplier"] =
                "【투창 데미지 배율 (%)】\n" +
                "투창 시 가하는 피해의 배율입니다.\n" +
                "원거리 공격 위력을 결정합니다.\n" +
                "권장값: 100-150%",

                ["Tier2_Throw_BuffDuration_NotUsed"] =
                "【사용 안 함】\n" +
                "이 설정은 현재 사용되지 않습니다.\n" +
                "패시브 스킬로 변경되었습니다.",

                // === Spear Tree: Tier 3 - 연격창 (1개) ===
                ["Tier3_Rapid_DamageBonus"] =
                "【무기 공격력 보너스 (고정값)】\n" +
                "창의 기본 공격력을 고정값으로 증가시킵니다.\n" +
                "빠른 연속 공격에 유리합니다.\n" +
                "권장값: 3-6",

                // === Spear Tree: Tier 3 - 폭발창 (3개) ===
                ["Tier3_Explosion_Chance"] =
                "【폭발 발동 확률 (%)】\n" +
                "창 공격 시 폭발 효과가 발동할 확률입니다.\n" +
                "광역 피해를 가할 수 있습니다.\n" +
                "권장값: 20-40%",

                ["Tier3_Explosion_Radius"] =
                "【폭발 범위 (m)】\n" +
                "폭발 피해가 미치는 반경입니다.\n" +
                "넓을수록 다수의 적에게 피해를 줍니다.\n" +
                "권장값: 4-7m",

                ["Tier3_Explosion_DamageBonus"] =
                "【폭발 피해 보너스 (%)】\n" +
                "폭발 시 추가되는 피해량입니다.\n" +
                "광역 딜링의 핵심입니다.\n" +
                "권장값: 25-40%",

                // === Spear Tree: Tier 4 - 회피 찌르기 (2개) ===
                ["Tier4_Evasion_DamageBonus"] =
                "【회피 후 공격력 보너스 (%)】\n" +
                "구르기(회피) 직후 창 공격의 피해가 증가합니다.\n" +
                "기동성과 공격력을 동시에 강화합니다.\n" +
                "권장값: 20-35%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【스태미나 소모 감소 (%)】\n" +
                "회피 후 공격 시 스태미나 소모가 감소합니다.\n" +
                "지속적인 전투가 가능합니다.\n" +
                "권장값: 5-12%",

                // === Spear Tree: Tier 4 - 이연창 (2개) ===
                ["Tier4_Dual_DamageBonus"] =
                "【2연타 공격력 보너스 (%)】\n" +
                "2회 연속 공격 시 추가 피해를 가합니다.\n" +
                "콤보 딜링에 특화됩니다.\n" +
                "권장값: 18-30%",

                ["Tier4_Dual_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "이연창 버프가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 딜링이 가능합니다.\n" +
                "권장값: 8-15초",

                // === Spear Tree: Tier 5 - 꿰뚫는 창 (G키 액티브, 6개) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] =
                "【사용 안 함】\n" +
                "이 설정은 현재 사용되지 않습니다.\n" +
                "번개 충격 효과로 변경되었습니다.",

                ["Tier5_Penetrate_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "꿰뚫는 창 버프가 유지되는 시간입니다.\n" +
                "스킬 효과 지속 시간을 결정합니다.\n" +
                "권장값: 25-40초",

                ["Tier5_Penetrate_LightningDamage"] =
                "【번개 충격 피해 배율 (%)】\n" +
                "연속 공격 시 발동되는 번개 피해의 배율입니다.\n" +
                "강력한 추가 피해를 가합니다.\n" +
                "권장값: 200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【번개 발동 필요 연타 횟수】\n" +
                "번개 충격이 발동되기 위해 필요한 연속 공격 횟수입니다.\n" +
                "적을수록 자주 발동됩니다.\n" +
                "권장값: 3-5회",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【G키 스킬 쿨타임 (초)】\n" +
                "꿰뚫는 창(G키) 재사용 대기시간입니다.\n" +
                "짧을수록 자주 사용 가능합니다.\n" +
                "권장값: 50-80초",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【G키 스킬 스태미나 소모】\n" +
                "꿰뚫는 창(G키) 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 20-35",

                // === Spear Tree: Tier 5 - 연공창 (H키 액티브, 7개) ===
                ["Tier5_Combo_HKey_Cooldown"] =
                "【H키 스킬 쿨타임 (초)】\n" +
                "연공창(H키) 재사용 대기시간입니다.\n" +
                "짧을수록 자주 사용 가능합니다.\n" +
                "권장값: 20-35초",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【H키 스킬 피해 배율 (%)】\n" +
                "연공창(H키) 공격의 피해 배율입니다.\n" +
                "강력한 단발 딜링 스킬입니다.\n" +
                "권장값: 250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【H키 스킬 스태미나 소모】\n" +
                "연공창(H키) 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 필요합니다.\n" +
                "권장값: 15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【H키 스킬 넉백 범위 (m)】\n" +
                "연공창(H키) 적중 시 적을 밀쳐내는 거리입니다.\n" +
                "전투 위치 조절에 유용합니다.\n" +
                "권장값: 2-5m",

                ["Tier5_Combo_ActiveRange"] =
                "【액티브 효과 범위 (m)】\n" +
                "연공창 버프가 활성화되는 범위입니다.\n" +
                "넓을수록 더 많은 상황에서 효과 발동됩니다.\n" +
                "권장값: 2-5m",

                ["Tier5_Combo_BuffDuration"] =
                "【버프 지속시간 (초)】\n" +
                "연공창 버프가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 강화 투창이 가능합니다.\n" +
                "권장값: 25-40초",

                ["Tier5_Combo_MaxUses"] =
                "【최대 강화 투창 횟수】\n" +
                "버프 중 강화된 투창을 사용할 수 있는 최대 횟수입니다.\n" +
                "많을수록 더 오래 강화 효과를 누릴 수 있습니다.\n" +
                "권장값: 2-5회",

                // ========================================
                // Mace Tree (티어별 정렬)
                // ========================================

                // === Tier 0: 둔기 전문가 (Mace Expert) ===
                ["Tier0_MaceExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier0_MaceExpert_DamageBonus"] =
                "【둔기 피해 보너스 (%)】\n" +
                "둔기 무기의 기본 공격력을 증가시킵니다.\n" +
                "모든 둔기류(클럽, 메이스 등)에 적용됩니다.\n" +
                "권장값: 5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【기절 확률 (%)】\n" +
                "둔기 공격 시 적을 기절시킬 확률입니다.\n" +
                "기절 상태의 적은 행동 불능이 됩니다.\n" +
                "권장값: 15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【기절 지속시간 (초)】\n" +
                "기절 효과가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안전한 딜 타이밍을 확보할 수 있습니다.\n" +
                "권장값: 0.3-1초",

                // === Tier 1: 둔기 공격력 강화 ===
                ["Tier1_MaceExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_MaceExpert_DamageBonus"] =
                "【둔기 공격력 보너스 (%)】\n" +
                "둔기 무기의 추가 공격력 보너스입니다.\n" +
                "권장값: 8-15%",

                // === Tier 2: 기절 강화 (Stun Boost) ===
                ["Tier2_StunBoost_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_StunBoost_StunChanceBonus"] =
                "【기절 확률 보너스 (%)】\n" +
                "기절 확률을 추가로 증가시킵니다.\n" +
                "둔기 전문가 스킬과 누적 적용됩니다.\n" +
                "권장값: 10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【기절 지속시간 보너스 (초)】\n" +
                "기절 효과의 지속시간을 추가로 증가시킵니다.\n" +
                "더 긴 딜 타이밍 확보가 가능합니다.\n" +
                "권장값: 0.3-0.8초",

                // === Tier 3: 방어 강화 (Guard) ===
                ["Tier3_Guard_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_Guard_ArmorBonus"] =
                "【방어력 보너스 (고정값)】\n" +
                "기본 방어력을 고정 수치로 증가시킵니다.\n" +
                "탱커 빌드에 유용합니다.\n" +
                "권장값: 2-5",

                // === Tier 3: 무거운 타격 (Heavy Strike) ===
                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_HeavyStrike_DamageBonus"] =
                "【공격력 보너스 (고정값)】\n" +
                "둔기 공격력을 고정 수치로 증가시킵니다.\n" +
                "퍼센트 보너스와 함께 적용됩니다.\n" +
                "권장값: 2-5",

                // === Tier 4: 밀어내기 (Push) ===
                ["Tier4_Push_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_Push_KnockbackChance"] =
                "【넉백 확률 (%)】\n" +
                "공격 시 적을 밀어내는 확률입니다.\n" +
                "거리 유지와 전장 제어에 유용합니다.\n" +
                "권장값: 25-35%",

                // === Tier 5: 탱커 (Tank) ===
                ["Tier5_Tank_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_Tank_HealthBonus"] =
                "【체력 보너스 (%)】\n" +
                "최대 체력을 증가시킵니다.\n" +
                "생존력 강화에 필수적입니다.\n" +
                "권장값: 20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【받는 데미지 감소 (%)】\n" +
                "모든 피해를 감소시킵니다.\n" +
                "방어력과 함께 적용되어 탱커 역할에 최적입니다.\n" +
                "권장값: 8-15%",

                // === Tier 5: 공격력 강화 (DPS) ===
                ["Tier5_DPS_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_DPS_DamageBonus"] =
                "【공격력 보너스 (%)】\n" +
                "둔기 무기의 공격력을 추가로 증가시킵니다.\n" +
                "DPS 빌드에 유용합니다.\n" +
                "권장값: 15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【공격속도 보너스 (%)】\n" +
                "둔기 공격속도를 증가시킵니다.\n" +
                "느린 둔기의 약점을 보완합니다.\n" +
                "권장값: 8-15%",

                // === Tier 6: 그랜드마스터 (Grandmaster) ===
                ["Tier6_Grandmaster_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier6_Grandmaster_ArmorBonus"] =
                "【방어력 보너스 (%)】\n" +
                "퍼센트 기반 방어력 보너스입니다.\n" +
                "고급 방어구와 시너지가 좋습니다.\n" +
                "권장값: 15-25%",

                // === Tier 7: 분노의 망치 (Fury Hammer - H키 액티브) ===
                ["Tier7_FuryHammer_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【1~4타 데미지 배율 (%)】\n" +
                "H키 스킬 '분노의 망치' 1~4타 공격의 데미지 배율입니다.\n" +
                "현재 공격력 기준으로 적용됩니다.\n" +
                "권장값: 70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【5타(최종타) 데미지 배율 (%)】\n" +
                "H키 스킬 '분노의 망치' 최종 공격의 데미지 배율입니다.\n" +
                "현재 공격력 기준으로 적용됩니다.\n" +
                "가장 강력한 피니시 공격입니다.\n" +
                "권장값: 130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【스태미나 소모】\n" +
                "H키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 25-35초",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【AOE 범위 (미터)】\n" +
                "스킬의 광역 피해 범위입니다.\n" +
                "넓을수록 더 많은 적을 공격할 수 있습니다.\n" +
                "권장값: 4-7m",

                // === Tier 7: 수호자의 진심 (Guardian Heart - G키 액티브) ===
                ["Tier7_GuardianHeart_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "G키 스킬 '수호자의 진심' 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 방어 자세를 취할 수 있습니다.\n" +
                "권장값: 100-140초",

                ["Tier7_GuardianHeart_StaminaCost"] =
                "【스태미나 소모】\n" +
                "스킬 사용 시 소모되는 스태미나입니다.\n" +
                "탱커 역할 시 스태미나 관리가 중요합니다.\n" +
                "권장값: 20-30",

                ["Tier7_GuardianHeart_Duration"] =
                "【버프 지속시간 (초)】\n" +
                "방어 자세의 지속 시간입니다.\n" +
                "이 시간 동안 피해를 반사하며 높은 방어력을 유지합니다.\n" +
                "권장값: 40-50초",

                ["Tier7_GuardianHeart_ReflectPercent"] =
                "【반사 데미지 비율 (%)】\n" +
                "받은 피해를 공격자에게 반사하는 비율입니다.\n" +
                "탱커로서 적에게 피해를 돌려줄 수 있습니다.\n" +
                "권장값: 5-8%",

                ["Tier7_GuardianHeart_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // ========================================
                // Polearm Tree (Tier 0~6, 25 keys)
                // ========================================

                // === Polearm Tree: 필요 포인트 (7개) ===
                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier1_PolearmSkill_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier2_PolearmSkill_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier3_PolearmSkill_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier4_PolearmSkill_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier5_Suppress_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                ["Tier6_PierceCharge_RequiredPoints"] =
                "【필요 포인트】\n" +
                "이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Polearm Tree: Tier 0 - 폴암 전문가 (1개) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【공격 범위 보너스 (%)】\n" +
                "폴암(장창, 창날도끼 등)의 공격 범위를 증가시킵니다.\n" +
                "긴 리치로 안전 거리를 유지하며 공격할 수 있습니다.\n" +
                "권장값: 10-20%",

                // === Polearm Tree: Tier 1 - 회전베기 (1개) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【회전 공격 피해 보너스 (%)】\n" +
                "회전 공격 시 추가 피해를 가합니다.\n" +
                "다수의 적을 상대할 때 유용합니다.\n" +
                "권장값: 50-80%",

                // === Polearm Tree: Tier 2 - 영웅 타격 (1개) ===
                ["Tier2_HeroStrike_KnockbackChance"] =
                "【넉백 확률 (%)】\n" +
                "공격 시 적을 밀쳐낼 확률입니다.\n" +
                "전장 제어에 유용합니다.\n" +
                "권장값: 20-35%",

                // === Polearm Tree: Tier 3 - 광역 강타 (2개) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【2연타 피해 보너스 (%)】\n" +
                "2회 연속 공격 시 추가 피해를 가합니다.\n" +
                "광역 콤보 딜링에 특화됩니다.\n" +
                "권장값: 20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【2연타 버프 지속시간 (초)】\n" +
                "2연타 버프가 유지되는 시간입니다.\n" +
                "긴 지속시간으로 안정적인 콤보가 가능합니다.\n" +
                "권장값: 4-8초",

                // === Polearm Tree: Tier 3 - 지면 강타 (1개) ===
                ["Tier3_GroundWheel_WheelAttackDamageBonus"] =
                "【회전 공격 피해 보너스 (%)】\n" +
                "지면을 강타하는 회전 공격의 피해를 증가시킵니다.\n" +
                "광역 딜링 핵심 스킬입니다.\n" +
                "권장값: 70-100%",

                // === Polearm Tree: Tier 3 - 폴암 강화 (1개) ===
                ["Tier3_PolearmBoost_WeaponDamageBonus"] =
                "【무기 공격력 보너스 (고정값)】\n" +
                "폴암의 기본 공격력을 고정값으로 증가시킵니다.\n" +
                "모든 폴암 공격에 적용됩니다.\n" +
                "권장값: 4-7",

                // === Polearm Tree: Tier 4 - 반달 베기 (2개) ===
                ["Tier4_MoonSlash_AttackRangeBonus"] =
                "【공격 범위 보너스 (%)】\n" +
                "반달 베기의 공격 범위를 증가시킵니다.\n" +
                "더 넓은 범위의 적을 공격할 수 있습니다.\n" +
                "권장값: 12-20%",

                ["Tier4_MoonSlash_StaminaReduction"] =
                "【스태미나 소모 감소 (%)】\n" +
                "반달 베기 사용 시 스태미나 소모가 감소합니다.\n" +
                "지속적인 전투가 가능합니다.\n" +
                "권장값: 12-20%",

                // === Polearm Tree: Tier 5 - 제압 공격 (1개) ===
                ["Tier5_Suppress_DamageBonus"] =
                "【제압 공격 피해 보너스 (%)】\n" +
                "제압 공격 시 추가 피해를 가합니다.\n" +
                "적을 억압하며 전투 주도권을 장악합니다.\n" +
                "권장값: 25-40%",

                // === Polearm Tree: Tier 6 - 관통 돌격 (G키 액티브, 8개) ===
                ["Tier6_PierceCharge_DashDistance"] =
                "【돌진 거리 (m)】\n" +
                "관통 돌격 시 돌진하는 거리입니다.\n" +
                "긴 거리로 적진을 뚫고 들어갑니다.\n" +
                "권장값: 8-12m",

                ["Tier6_PierceCharge_FirstHitDamageBonus"] =
                "【첫 타격 피해 보너스 (%)】\n" +
                "돌격 중 첫 타격의 피해 배율입니다.\n" +
                "강력한 첫 타격으로 적을 제압합니다.\n" +
                "권장값: 180-250%",

                ["Tier6_PierceCharge_AoeDamageBonus"] =
                "【광역 넉백 피해 보너스 (%)】\n" +
                "돌격 종료 후 광역 넉백 공격의 피해 배율입니다.\n" +
                "주변 적들을 밀쳐내며 피해를 가합니다.\n" +
                "권장값: 130-180%",

                ["Tier6_PierceCharge_AoeAngle"] =
                "【광역 각도 (도)】\n" +
                "광역 넉백 효과의 각도입니다.\n" +
                "280도 = 전방 80도 제외한 후방/측면 범위입니다.\n" +
                "권장값: 250-300도",

                ["Tier6_PierceCharge_AoeRadius"] =
                "【광역 반경 (m)】\n" +
                "광역 넉백 효과의 반경입니다.\n" +
                "넓을수록 더 많은 적을 밀쳐냅니다.\n" +
                "권장값: 4-7m",

                ["Tier6_PierceCharge_KnockbackDistance"] =
                "【넉백 거리 (m)】\n" +
                "적을 밀쳐내는 거리입니다.\n" +
                "전장 제어에 유용합니다.\n" +
                "권장값: 6-10m",

                ["Tier6_PierceCharge_StaminaCost"] =
                "【스태미나 소모】\n" +
                "G키 스킬 사용 시 소모되는 스태미나입니다.\n" +
                "스태미나 관리가 중요합니다.\n" +
                "권장값: 18-25",

                ["Tier6_PierceCharge_Cooldown"] =
                "【쿨타임 (초)】\n" +
                "G키 스킬 재사용 대기 시간입니다.\n" +
                "짧을수록 자주 사용할 수 있습니다.\n" +
                "권장값: 25-40초",

                // === Knife Tree: 필요 포인트 (9개) ===
                ["Knife_Expert_RequiredPoints"] = "Tier 0: 단검 전문가(knife_expert_damage) - 필요 포인트",
                ["Knife_Evasion_RequiredPoints"] = "Tier 1: 회피 숙련(knife_step2_evasion) - 필요 포인트",
                ["Knife_FastMove_RequiredPoints"] = "Tier 2: 빠른 움직임(knife_step3_move_speed) - 필요 포인트",
                ["Knife_CombatMastery_RequiredPoints"] = "Tier 3: 전투 숙련(knife_step4_attack_damage) - 필요 포인트",
                ["Knife_AttackEvasion_RequiredPoints"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 필요 포인트",
                ["Knife_CriticalDamage_RequiredPoints"] = "Tier 5: 치명적 피해(knife_step6_combat_damage) - 필요 포인트",
                ["Knife_Assassin_RequiredPoints"] = "Tier 6: 암살자(knife_step7_execution) - 필요 포인트",
                ["Knife_AssassinSkill_RequiredPoints"] = "Tier 7: 암살술(knife_step8_assassination) - 필요 포인트",
                ["Knife_AssassinHeart_RequiredPoints"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 필요 포인트",

                // === Knife Tree: 단검 전문가 (1개) ===
                ["Knife_Expert_BackstabDamageBonus"] = "Tier 0: 단검 전문가(knife_expert_damage) - 백스탭 데미지 보너스 (%)",

                // === Knife Tree: 회피 숙련 (2개) ===
                ["Knife_Evasion_Chance"] = "Tier 1: 회피 숙련(knife_step2_evasion) - 회피 확률 (%)",
                ["Knife_Evasion_InvincibilityDuration"] = "Tier 1: 회피 숙련(knife_step2_evasion) - 회피 후 무적 시간 (초)",

                // === Knife Tree: 빠른 움직임 (1개) ===
                ["Knife_FastMove_MovementSpeedIncrease"] = "Tier 2: 빠른 움직임(knife_step3_move_speed) - 이동속도 증가 (%)",

                // === Knife Tree: 전투 숙련 (2개) ===
                ["Knife_CombatMastery_DamageIncrease"] = "Tier 3: 전투 숙련(knife_step4_attack_damage) - 적 처치 시 단검 데미지 증가 (고정값)",
                ["Knife_CombatMastery_EffectDuration"] = "Tier 3: 전투 숙련(knife_step4_attack_damage) - 효과 지속시간 (초)",

                // === Knife Tree: 공격과 회피 (3개) ===
                ["Knife_AttackEvasion_EvasionRateIncrease"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 2연속 공격 시 회피율 증가 (%)",
                ["Knife_AttackEvasion_EffectDuration"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 효과 지속시간 (초)",
                ["Knife_AttackEvasion_Cooldown"] = "Tier 4: 공격과 회피(knife_step5_crit_rate) - 쿨타임 (초)",

                // === Knife Tree: 치명적 피해 (1개) ===
                ["Knife_CriticalDamage_DamageIncrease"] = "Tier 5: 치명적 피해(knife_step6_combat_damage) - 공격력 증가 (%)",

                // === Knife Tree: 암살자 (2개) ===
                ["Knife_Assassin_CritDamageIncrease"] = "Tier 6: 암살자(knife_step7_execution) - 치명타 피해 증가 (%)",
                ["Knife_Assassin_CritChanceIncrease"] = "Tier 6: 암살자(knife_step7_execution) - 치명타 확률 증가 (%)",

                // === Knife Tree: 암살술 (2개) ===
                ["Knife_AssassinSkill_StaggerChance"] = "Tier 7: 암살술(knife_step8_assassination) - 3연속 공격 시 스태거 확률 (%)",
                ["Knife_AssassinSkill_RequiredComboHits"] = "Tier 7: 암살술(knife_step8_assassination) - 스태거 발동에 필요한 연속 공격 횟수",

                // === Knife Tree: 암살자의 심장 (9개) ===
                ["Knife_AssassinHeart_CritDamageMultiplier"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 치명타 데미지 배수",
                ["Knife_AssassinHeart_EffectDuration"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 효과 지속시간 (초)",
                ["Knife_AssassinHeart_StaminaCost"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 스태미나 소모량",
                ["Knife_AssassinHeart_Cooldown"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 쿨타임 (초)",
                ["Knife_AssassinHeart_TeleportSearchRange"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 순간이동 대상 탐색 범위 (m)",
                ["Knife_AssassinHeart_TeleportBackDistance"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 대상 뒤쪽으로 이동할 거리 (m)",
                ["Knife_AssassinHeart_StunDuration"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 순간이동 후 대상 스턴 지속시간 (초)",
                ["Knife_AssassinHeart_ComboAttackCount"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 순간이동 후 연속 공격 횟수",
                ["Knife_AssassinHeart_AttackInterval"] = "Tier 8: 암살자의 심장(knife_step9_assassin_heart) - 연속 공격 간격 (초)",

                // === Speed Tree: 필요 포인트 (9개) ===
                ["Tier0_SpeedExpert_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier1_AgilityBase_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier2_WeaponSpec_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier3_Practitioner_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier4_Master_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier5_JumpMaster_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier6_Stats_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier7_Master_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",
                ["Tier8_FinalAcceleration_RequiredPoints"] = "【필요 포인트】\n이 노드를 해금하기 위해 필요한 스킬 포인트 개수입니다.",

                // === Tier 0: 속도 전문가 (1개) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "【이동속도 보너스 (%)】\n영구적인 이동 속도 증가입니다.\n권장값: 5-10%",

                // === Tier 1: 민첩함의 기초 (4개) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "【구르기 후 이동속도 보너스 (%)】\n구르기(회피) 직후 짧은 시간 동안 이동 속도가 증가합니다.\n재빠른 회피 후 재배치에 유용합니다.\n권장값: 10-20%",
                ["Tier1_AgilityBase_BuffDuration"] = "【버프 지속시간 (초)】\n구르기 후 속도 버프의 지속 시간입니다.\n권장값: 2-3초",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "【공격속도 보너스 (%)】\n모든 무기의 전반적인 공격 속도가 증가합니다.\n권장값: 3-8%",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "【구르기 속도 보너스 (%)】\n구르기 애니메이션의 속도가 증가합니다.\n권장값: 5-15%",

                // === Tier 2: 근접 무기 (4개) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "【2연타 시 공격속도 보너스 (%)】\n근접 무기로 2회 연속 타격 후 공격 속도가 증가합니다.\n권장값: 8-15%",
                ["Tier2_MeleeFlow_StaminaReduction"] = "【스태미나 소모 감소 (%)】\n흐름 버프 중 스태미나 소모가 감소합니다.\n권장값: 10-20%",
                ["Tier2_MeleeFlow_Duration"] = "【버프 지속시간 (초)】\n근접 흐름 버프의 지속 시간입니다.\n권장값: 3-5초",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "【콤보 속도 보너스 (%)】\n콤보 연계 시 추가 공격 속도 보너스입니다.\n권장값: 5-10%",

                // === Tier 2: 석궁 숙련자 (3개) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "【적 명중 시 이동속도 보너스 (%)】\n석궁 화살이 적에게 명중할 때 이동 속도가 증가합니다.\n권장값: 10-15%",
                ["Tier2_CrossbowExpert_BuffDuration"] = "【버프 지속시간 (초)】\n명중 성공 후 속도 버프의 지속 시간입니다.\n권장값: 3-5초",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "【버프 중 재장전 속도 보너스 (%)】\n명중 버프가 활성화된 동안 재장전 속도가 증가합니다.\n권장값: 10-15%",

                // === Tier 2: 활 숙련자 (3개) ===
                ["Tier2_BowExpert_StaminaReduction"] = "【2연타 콤보 시 스태미나 감소 (%)】\n활로 2회 연속 명중 후 스태미나 소모가 감소합니다.\n권장값: 10-15%",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "【다음 화살 시위 당김 속도 보너스 (%)】\n콤보 성공 후 다음 화살의 시위 당김 속도가 증가합니다.\n권장값: 10-20%",
                ["Tier2_BowExpert_BuffDuration"] = "【버프 지속시간 (초)】\n콤보 버프의 지속 시간입니다.\n권장값: 4-6초",

                // === Tier 2: 이동 시전 (3개) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "【시전 중 이동속도 보너스 (%)】\n지팡이 주문 시전 중 이동 속도 보너스입니다.\n권장값: 8-12%",
                ["Tier2_MobileCast_EitrReduction"] = "【Eitr 소모 감소 (%)】\n지팡이 주문의 Eitr 소모가 감소합니다.\n권장값: 8-15%",
                ["Tier2_MobileCast_CastMoveSpeed"] = "【지팡이 시전 중 이동속도 (%)】\n지팡이 공격을 채널링하는 동안의 기본 이동 속도입니다.\n권장값: 3-6%",

                // === Tier 3: 수련자 (4개) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "【근접 무기 스킬 보너스】\n모든 근접 무기 스킬 레벨을 증가시킵니다.\n권장값: 5-10",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "【석궁 스킬 보너스】\n석궁 스킬 레벨을 증가시킵니다.\n권장값: 5-10",
                ["Tier3_Practitioner2_StaffSkillBonus"] = "【지팡이 스킬 보너스】\n지팡이 스킬 레벨(Elementalmagi)을 증가시킵니다.\n권장값: 5-10",
                ["Tier3_Practitioner2_BowSkillBonus"] = "【활 스킬 보너스】\n활 스킬 레벨을 증가시킵니다.\n권장값: 5-10",

                // === Tier 4: 마스터 (2개) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "【음식 소모율 감소 (%)】\n음식 소모 속도를 늦춰 버프가 더 오래 지속됩니다.\n권장값: 10-20%",
                ["Tier4_Captain_ShipSpeedBonus"] = "【배 속도 보너스 (%)】\n항해 속도를 증가시킵니다.\n권장값: 10-20%",

                // === Tier 5: 점프 숙련자 (2개) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "【점프 스킬 보너스】\n점프 스킬 레벨을 증가시킵니다.\n권장값: 5-15",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "【점프 스태미나 감소 (%)】\n점프 시 스태미나 소모를 감소시킵니다.\n권장값: 10-20%",

                // === Tier 6: 스탯 (4개) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "【근접 공격속도 보너스 (%)】\n근접 무기의 공격 속도를 증가시킵니다.\n권장값: 5-8%",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "【이동속도 보너스 (%)】\n전반적인 이동 속도를 증가시킵니다.\n권장값: 3-8%",
                ["Tier6_Endurance_StaminaMaxBonus"] = "【최대 스태미나 보너스】\n최대 스태미나 풀을 증가시킵니다.\n권장값: 20-40",
                ["Tier6_Intellect_EitrMaxBonus"] = "【최대 Eitr 보너스】\n마법을 위한 최대 Eitr 풀을 증가시킵니다.\n권장값: 30-50",

                // === Tier 7: 마스터 (2개) ===
                ["Tier7_Master_RunSkillBonus"] = "【달리기 스킬 보너스】\n달리기 스킬 레벨을 증가시킵니다.\n권장값: 5-15",
                ["Tier7_Master_JumpSkillBonus"] = "【점프 스킬 보너스】\n점프 스킬 레벨을 증가시킵니다.\n권장값: 5-15",

                // === Tier 8: 근접 가속 (2개) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "【근접 공격속도 보너스 (%)】\n근접 공격 속도의 최종 부스트입니다.\n권장값: 5-10%",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "【3연타 시 다음 공격 속도 보너스 (%)】\n3연타 콤보 후 다음 공격의 속도가 대폭 증가합니다.\n권장값: 20-30%",

                // === Tier 8: 석궁 가속 (2개) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "【재장전 속도 보너스 (%)】\n석궁 재장전 속도의 최종 부스트입니다.\n권장값: 25-35%",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "【재장전 중 이동속도 (%)】\n석궁 재장전 중 이동 속도입니다.\n권장값: 20-30%",

                // === Tier 8: 활 가속 (2개) ===
                ["Tier8_BowAccel_DrawSpeed"] = "【시위 당김 속도 보너스 (%)】\n활 시위 당김 속도의 최종 부스트입니다.\n권장값: 15-20%",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "【시위 당기는 중 이동속도 (%)】\n활 시위를 당기는 동안의 이동 속도입니다.\n권장값: 10-20%",

                // === Tier 8: 시전 가속 (2개) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "【마법 공격속도 보너스 (%)】\n마법 공격 속도의 최종 부스트입니다.\n권장값: 5-10%",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "【3연타 시 Eitr 최대 회복률 (%)】\n3회 주문 콤보 후 Eitr 재생 속도가 증가합니다.\n권장값: 10-15%",

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

        // ============================================
        // 카테고리 번역 (영어)
        // ============================================
        private static Dictionary<string, string> GetEnglishCategories()
        {
            return new Dictionary<string, string>
            {
                ["Attack Tree"] = "Attack Tree",
                ["Defense Tree"] = "Defense Tree",
                ["Production Tree"] = "Production Tree",
                ["Staff Tree"] = "Staff Tree",
                ["Crossbow Tree"] = "Crossbow Tree",
                ["Bow Tree"] = "Bow Tree",
                ["Sword Tree"] = "Sword Tree",
                ["Spear Tree"] = "Spear Tree",
                ["Mace Tree"] = "Mace Tree",
                ["Polearm Tree"] = "Polearm Tree",
                ["Knife Tree"] = "Knife Tree",
                ["Speed Tree"] = "Speed Tree",
                ["Archer Job Skills"] = "Archer Job Skills",
                ["Mage Job Skills"] = "Mage Job Skills",
            };
        }

        // ============================================
        // 설명 번역 (영어)
        // ============================================
        private static Dictionary<string, string> GetEnglishDescriptions()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Attack Tree
                // ========================================

                // === Attack Tree: Required Points (7) ===
                ["Tier0_AttackExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier1_BaseAttack_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier2_WeaponSpec_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier3_AttackBoost_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier4_DetailEnhance_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier4_RangedEnhance_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier5_SpecialStat_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier6_FinalSpec_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                // === Tier 0: Attack Expert ===
                ["Tier0_AttackExpert_AllDamageBonus"] =
                "【All Damage Bonus (%)】\n" +
                "Increases both physical and elemental damage.\n" +
                "Fundamental attack power enhancement for all weapons.\n" +
                "Recommended: 8-12%",

                // === Tier 2: Weapon Specialization ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] =
                "【Melee Bonus Trigger Chance (%)】\n" +
                "Chance for additional damage to trigger on melee attacks.\n" +
                "Provides explosive burst damage opportunities.\n" +
                "Recommended: 15-25%",

                ["Tier2_MeleeSpec_MeleeDamage"] =
                "【Melee Additional Damage (Flat)】\n" +
                "Flat damage added when bonus triggers.\n" +
                "Recommended: 8-15",

                ["Tier2_BowSpec_BonusTriggerChance"] =
                "【Bow Bonus Trigger Chance (%)】\n" +
                "Chance for additional damage to trigger on bow attacks.\n" +
                "Fires devastating arrows with this proc.\n" +
                "Recommended: 15-25%",

                ["Tier2_BowSpec_BowDamage"] =
                "【Bow Additional Damage (Flat)】\n" +
                "Flat damage added when bonus triggers.\n" +
                "Recommended: 6-12",

                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] =
                "【Crossbow Bonus Trigger Chance (%)】\n" +
                "Chance for additional damage to trigger on crossbow attacks.\n" +
                "Penetrates enemies with powerful bolts.\n" +
                "Recommended: 12-20%",

                ["Tier2_CrossbowSpec_CrossbowDamage"] =
                "【Crossbow Additional Damage (Flat)】\n" +
                "Flat damage added when bonus triggers.\n" +
                "Recommended: 7-13",

                ["Tier2_StaffSpec_ElementalTriggerChance"] =
                "【Staff Elemental Bonus Trigger Chance (%)】\n" +
                "Chance for additional elemental damage to trigger on staff attacks.\n" +
                "Maximizes magical attack power.\n" +
                "Recommended: 15-25%",

                ["Tier2_StaffSpec_StaffDamage"] =
                "【Staff Additional Damage (Flat)】\n" +
                "Flat damage added when bonus triggers.\n" +
                "Recommended: 6-12",

                // === Tier 3: Base Attack Enhancement ===
                ["Tier3_BaseAttack_PhysicalDamageBonus"] =
                "【Physical Damage Bonus (Flat)】\n" +
                "Increases physical damage of all weapons by a flat amount.\n" +
                "Recommended: 1-3",

                ["Tier3_BaseAttack_ElementalDamageBonus"] =
                "【Elemental Damage Bonus (Flat)】\n" +
                "Increases elemental damage (fire, frost, lightning) of all weapons by a flat amount.\n" +
                "Recommended: 1-3",

                ["Tier3_AttackBoost_StrIntBonus"] =
                "【Strength/Intelligence Stat Bonus】\n" +
                "Increases Strength for melee weapons, Intelligence for magic weapons.\n" +
                "Stat increases directly impact damage output.\n" +
                "Recommended: 3-8",

                ["Tier3_AttackBoost_PhysicalDamageBonus"] =
                "【Two-Hand Physical Damage Bonus (%)】\n" +
                "Increases physical damage when using two-handed weapons.\n" +
                "Maximizes the power of two-handed weapons.\n" +
                "Recommended: 8-15%",

                ["Tier3_AttackBoost_ElementalDamageBonus"] =
                "【Two-Hand Elemental Damage Bonus (%)】\n" +
                "Increases elemental damage when using two-handed weapons.\n" +
                "Recommended: 8-15%",

                // === Tier 4: Combat Enhancement ===
                ["Tier4_PrecisionAttack_CritChance"] =
                "【Critical Hit Chance Bonus (%)】\n" +
                "Increases critical hit chance for all attacks.\n" +
                "Critical hits deal higher damage than normal attacks.\n" +
                "Recommended: 3-8%",

                ["Tier4_MeleeEnhance_2HitComboBonus"] =
                "【2-Hit Combo Damage Bonus (%)】\n" +
                "Increases damage when landing 2 consecutive melee hits.\n" +
                "Deal stronger blows with combo attacks.\n" +
                "Recommended: 8-15%",

                ["Tier4_RangedEnhance_RangedDamageBonus"] =
                "【Ranged Damage Bonus (Flat)】\n" +
                "Increases damage of ranged weapons (bow, crossbow) by a flat amount.\n" +
                "Recommended: 3-8",

                // === Tier 5: Specialized Stats ===
                ["Tier5_SpecialStat_SpecBonus"] =
                "【Weapon Specialization Bonus】\n" +
                "Increases specialized stat for the currently equipped weapon type.\n" +
                "Melee increases Strength, ranged increases Dexterity, magic increases Intelligence.\n" +
                "Recommended: 3-8",

                // === Tier 6: Final Enhancement ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] =
                "【Critical Damage Bonus (%)】\n" +
                "Increases additional damage on critical hits.\n" +
                "Maximizes the power of critical strikes.\n" +
                "Recommended: 10-20%",

                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] =
                "【Two-Hand Damage Bonus (%)】\n" +
                "Increases total damage when using two-handed weapons.\n" +
                "Enhances the overwhelming attack power of two-handed weapons.\n" +
                "Recommended: 8-15%",

                ["Tier6_ElementalAttack_ElementalBonus"] =
                "【Staff Elemental Damage Bonus (%)】\n" +
                "Increases elemental damage (fire, frost, lightning) of staves.\n" +
                "Enhances the destructive power of magical attacks.\n" +
                "Recommended: 8-15%",

                ["Tier6_ComboFinisher_3HitComboBonus"] =
                "【3-Hit Combo Finisher Damage Bonus (%)】\n" +
                "Increases final hit damage when landing 3 consecutive melee hits.\n" +
                "Dominate enemies with combo finishers.\n" +
                "Recommended: 12-20%",

                // ========================================
                // Defense Tree
                // ========================================

                // === Defense Tree: Required Points (7) ===
                ["Tier0_DefenseExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier1_SkinHardening_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier2_MindBodyHealth_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier3_CoreDodgeBoostShield_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier4_ShockwaveStompRock_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier5_EndureAgileRegenParry_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier6_FinalSkills_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                // === Tier 0: Defense Expert ===
                ["Tier0_DefenseExpert_HPBonus"] =
                "【Health Bonus (Flat)】\n" +
                "Increases maximum health by a flat amount.\n" +
                "Foundation of survivability.\n" +
                "Recommended: 3-8",

                ["Tier0_DefenseExpert_ArmorBonus"] =
                "【Armor Bonus (Flat)】\n" +
                "Increases armor by a flat amount.\n" +
                "Reduces incoming damage.\n" +
                "Recommended: 1-4",

                // === Tier 1: Skin Hardening ===
                ["Tier1_SkinHardening_HPBonus"] =
                "【Health Bonus (Flat)】\n" +
                "Further increases maximum health.\n" +
                "Recommended: 3-8",

                ["Tier1_SkinHardening_ArmorBonus"] =
                "【Armor Bonus (Flat)】\n" +
                "Further increases armor.\n" +
                "Recommended: 3-8",

                // === Tier 2: Mind-Body Training & Health Training ===
                ["Tier2_MindBodyTraining_StaminaBonus"] =
                "【Max Stamina Bonus (Flat)】\n" +
                "Increases maximum stamina.\n" +
                "Allows more actions.\n" +
                "Recommended: 20-30",

                ["Tier2_MindBodyTraining_EitrBonus"] =
                "【Max Eitr Bonus (Flat)】\n" +
                "Increases maximum Eitr.\n" +
                "Allows more magic usage.\n" +
                "Recommended: 20-30",

                ["Tier2_HealthTraining_HPBonus"] =
                "【Health Bonus (Flat)】\n" +
                "Significantly increases maximum health.\n" +
                "Recommended: 15-25",

                ["Tier2_HealthTraining_ArmorBonus"] =
                "【Armor Bonus (Flat)】\n" +
                "Further increases armor.\n" +
                "Recommended: 3-8",

                // === Tier 3: Various Defense Skills ===
                ["Tier3_CoreBreathing_EitrBonus"] =
                "【Eitr Bonus (Flat)】\n" +
                "Core breathing increases Eitr.\n" +
                "Recommended: 8-15",

                ["Tier3_EvasionTraining_DodgeBonus"] =
                "【Dodge Rate Bonus (%)】\n" +
                "Increases chance to dodge enemy attacks.\n" +
                "Recommended: 3-8%",

                ["Tier3_EvasionTraining_InvincibilityBonus"] =
                "【Roll Invincibility Increase (%)】\n" +
                "Extends invincibility duration during rolls.\n" +
                "Safer evasion.\n" +
                "Recommended: 15-25%",

                ["Tier3_HealthBoost_HPBonus"] =
                "【Health Bonus (Flat)】\n" +
                "Further increases health.\n" +
                "Recommended: 12-20",

                ["Tier3_ShieldTraining_BlockPowerBonus"] =
                "【Shield Block Power Bonus (Flat)】\n" +
                "Increases shield's blocking power.\n" +
                "Can block stronger attacks.\n" +
                "Recommended: 80-120",

                // === Tier 4: Ground Stomp ===
                ["Tier4_GroundStomp_Radius"] =
                "【Effect Radius (meters)】\n" +
                "Range of the shockwave.\n" +
                "Recommended: 2.5-4m",

                ["Tier4_GroundStomp_KnockbackForce"] =
                "【Knockback Force】\n" +
                "Force that pushes enemies away.\n" +
                "Recommended: 15-25",

                ["Tier4_GroundStomp_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 100-150s",

                ["Tier4_GroundStomp_HPThreshold"] =
                "【Auto-trigger HP Threshold (ratio)】\n" +
                "Automatically triggers below this health.\n" +
                "0.35 = below 35% health\n" +
                "Recommended: 0.30-0.40",

                ["Tier4_GroundStomp_VFXDuration"] =
                "【VFX Duration (seconds)】\n" +
                "Duration of visual effects.\n" +
                "Recommended: 0.8-1.5s",

                // === Tier 4: Rock Skin ===
                ["Tier4_RockSkin_ArmorBonus"] =
                "【Armor Amplification (%)】\n" +
                "Applies a percentage bonus to helmet, chest, legs, and shield armor individually.\n" +
                "Item tooltip shows: ((base armor + skill bonus) * X%) format.\n" +
                "Recommended: 10-15%",

                // === Tier 5: Endurance & Agility & Regen & Block Master ===
                ["Tier5_Endurance_RunStaminaReduction"] =
                "【Run Stamina Reduction (%)】\n" +
                "Reduces stamina cost when running.\n" +
                "Recommended: 8-15%",

                ["Tier5_Endurance_JumpStaminaReduction"] =
                "【Jump Stamina Reduction (%)】\n" +
                "Reduces stamina cost when jumping.\n" +
                "Recommended: 8-15%",

                ["Tier5_Agility_DodgeBonus"] =
                "【Dodge Rate Bonus (%)】\n" +
                "Further increases dodge chance.\n" +
                "Recommended: 3-8%",

                ["Tier5_Agility_RollStaminaReduction"] =
                "【Roll Stamina Reduction (%)】\n" +
                "Reduces stamina cost when rolling.\n" +
                "Recommended: 10-18%",

                ["Tier5_TrollRegen_HPRegenBonus"] =
                "【HP Regen Bonus (per second)】\n" +
                "Health regenerates like a troll.\n" +
                "Recommended: 3-8",

                ["Tier5_TrollRegen_RegenInterval"] =
                "【Regen Interval (seconds)】\n" +
                "Frequency of health recovery.\n" +
                "Recommended: 1.5-3s",

                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] =
                "【Shield Block Power Bonus (Flat)】\n" +
                "Significantly increases shield's blocking power.\n" +
                "Recommended: 80-120",

                ["Tier5_BlockMaster_ParryDurationBonus"] =
                "【Parry Duration Bonus (seconds)】\n" +
                "Extends duration after successful parry.\n" +
                "Recommended: 0.8-1.5s",

                // === Tier 6: Final Defense Skills ===
                ["Tier6_NerveEnhancement_DodgeBonus"] =
                "【Permanent Dodge Rate Bonus (%)】\n" +
                "Nerve enhancement permanently increases dodge rate.\n" +
                "Recommended: 3-8%",

                ["Tier6_JotunnVitality_HPBonus"] =
                "【Health Bonus (%)】\n" +
                "Jotunn's vitality increases max health percentage.\n" +
                "Recommended: 25-40%",

                ["Tier6_JotunnVitality_ArmorBonus"] =
                "【Armor Bonus (%)】\n" +
                "Jotunn's armor increases armor percentage.\n" +
                "Recommended: 8-15%",

                ["Tier6_JotunnShield_BlockStaminaReduction"] =
                "【Block Stamina Reduction (%)】\n" +
                "Reduces stamina cost when blocking with shield.\n" +
                "Recommended: 20-30%",

                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] =
                "【Normal Shield Move Speed Bonus (%)】\n" +
                "Increases movement speed while holding normal shield.\n" +
                "Recommended: 3-8%",

                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] =
                "【Tower Shield Move Speed Bonus (%)】\n" +
                "Increases movement speed while holding tower shield.\n" +
                "Recommended: 8-15%",

                // ========================================
                // Production Tree
                // ========================================

                // === Production Tree: Required Points (5) ===
                ["Tier0_ProductionExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier1_NoviceWorker_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier2_Specialization_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier3_IntermediateSkill_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier4_AdvancedSkill_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                // === Tier 0: Production Expert ===
                ["Tier0_ProductionExpert_WoodBonusChance"] =
                "【Wood +1 Bonus Chance (%)】\n" +
                "Chance to gain one extra wood when chopping trees.\n" +
                "Foundation of production skills.\n" +
                "Recommended: 40-60%",

                // === Tier 1: Novice Worker ===
                ["Tier1_NoviceWorker_WoodBonusChance"] =
                "【Wood +1 Bonus Chance (%)】\n" +
                "Increases chance to gain extra wood when chopping.\n" +
                "Recommended: 20-30%",

                // === Tier 2: Specialization ===
                ["Tier2_WoodcuttingLv2_BonusChance"] =
                "【Wood +1 Bonus Chance (%)】\n" +
                "Woodcutting Lv2 - Extra wood bonus chance.\n" +
                "Recommended: 20-30%",

                ["Tier2_GatheringLv2_BonusChance"] =
                "【Item +1 Bonus Chance (%)】\n" +
                "Gathering Lv2 - Extra item bonus when gathering berries, mushrooms, etc.\n" +
                "Recommended: 20-30%",

                ["Tier2_MiningLv2_BonusChance"] =
                "【Ore +1 Bonus Chance (%)】\n" +
                "Mining Lv2 - Extra ore bonus when mining.\n" +
                "Recommended: 20-30%",

                ["Tier2_CraftingLv2_UpgradeChance"] =
                "【Upgrade +1 Bonus Chance (%)】\n" +
                "Crafting Lv2 - Chance to gain extra upgrade level when crafting/upgrading.\n" +
                "Recommended: 20-30%",

                ["Tier2_CraftingLv2_DurabilityBonus"] =
                "【Max Durability Increase (%)】\n" +
                "Crafting Lv2 - Increases maximum durability of crafted items.\n" +
                "Recommended: 20-30%",

                // === Tier 3: Intermediate Skills ===
                ["Tier3_WoodcuttingLv3_BonusChance"] =
                "【Wood +2 Bonus Chance (%)】\n" +
                "Woodcutting Lv3 - Chance to gain 2 extra wood.\n" +
                "Recommended: 30-40%",

                ["Tier3_GatheringLv3_BonusChance"] =
                "【Item +1 Bonus Chance (%)】\n" +
                "Gathering Lv3 - Increased extra item bonus chance.\n" +
                "Recommended: 20-30%",

                ["Tier3_MiningLv3_BonusChance"] =
                "【Ore +1 Bonus Chance (%)】\n" +
                "Mining Lv3 - Increased extra ore bonus chance.\n" +
                "Recommended: 20-30%",

                ["Tier3_CraftingLv3_UpgradeChance"] =
                "【Upgrade +1 Bonus Chance (%)】\n" +
                "Crafting Lv3 - Increased extra upgrade chance.\n" +
                "Recommended: 20-30%",

                ["Tier3_CraftingLv3_DurabilityBonus"] =
                "【Max Durability Increase (%)】\n" +
                "Crafting Lv3 - Further increases maximum durability.\n" +
                "Recommended: 20-30%",

                // === Tier 4: Advanced Skills ===
                ["Tier4_WoodcuttingLv4_BonusChance"] =
                "【Wood +2 Bonus Chance (%)】\n" +
                "Woodcutting Lv4 - Significantly increased chance for 2 extra wood.\n" +
                "Recommended: 40-50%",

                ["Tier4_GatheringLv4_BonusChance"] =
                "【Item +1 Bonus Chance (%)】\n" +
                "Gathering Lv4 - Maximized extra item bonus chance.\n" +
                "Recommended: 20-30%",

                ["Tier4_MiningLv4_BonusChance"] =
                "【Ore +1 Bonus Chance (%)】\n" +
                "Mining Lv4 - Maximized extra ore bonus chance.\n" +
                "Recommended: 20-30%",

                ["Tier4_CraftingLv4_UpgradeChance"] =
                "【Upgrade +1 Bonus Chance (%)】\n" +
                "Crafting Lv4 - Maximized extra upgrade chance.\n" +
                "Recommended: 20-30%",

                ["Tier4_CraftingLv4_DurabilityBonus"] =
                "【Max Durability Increase (%)】\n" +
                "Crafting Lv4 - Maximized durability increase.\n" +
                "Recommended: 20-30%",

                // ========================================
                // Staff Tree
                // ========================================

                // === Staff Tree: Required Points (8) ===
                ["Tier0_StaffExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier1_MindFocus_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier1_MagicFlow_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier2_MagicAmplify_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier3_FrostElement_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier3_FireElement_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier3_LightningElement_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier4_LuckyMana_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier5_DoubleCast_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                ["Tier5_InstantAreaHeal_RequiredPoints"] =
                "【Required Points】\n" +
                "Points required to unlock this node.",

                // === Tier 0: Staff Expert ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] =
                "【Elemental Damage Bonus (%)】\n" +
                "Increases elemental damage (fire, frost, lightning) of staves.\n" +
                "Foundation of magical attacks.\n" +
                "Recommended: 10-15%",

                // === Tier 1: Mind Focus & Magic Flow ===
                ["Tier1_MindFocus_EitrReduction"] =
                "【Eitr Cost Reduction (%)】\n" +
                "Mind focus reduces Eitr cost of staff spells.\n" +
                "Allows more magic usage.\n" +
                "Recommended: 12-20%",

                ["Tier1_MagicFlow_EitrBonus"] =
                "【Max Eitr Bonus (Flat)】\n" +
                "Magic flow increases maximum Eitr.\n" +
                "Recommended: 25-35",

                // === Tier 2: Magic Amplify ===
                ["Tier2_MagicAmplify_Chance"] =
                "【Magic Amplification Activation Chance (%)】\n" +
                "Chance to amplify elemental damage.\n" +
                "Recommended: 30-50%",

                ["Tier2_MagicAmplify_DamageBonus"] =
                "【Magic Amplification Elemental Damage Bonus (%)】\n" +
                "Elemental damage increase on activation.\n" +
                "Recommended: 30-40%",

                ["Tier2_MagicAmplify_EitrCostIncrease"] =
                "【Magic Amplification Eitr Cost Increase (%)】\n" +
                "Increases Eitr consumption when casting spells.\n" +
                "Price of powerful magic.\n" +
                "Recommended: 15-25%",

                // === Tier 3: Elemental Enhancement ===
                ["Tier3_FrostElement_DamageBonus"] =
                "【Frost Attack Bonus (Flat)】\n" +
                "Increases frost elemental magic damage by a flat amount.\n" +
                "Recommended: 2-5",

                ["Tier3_FireElement_DamageBonus"] =
                "【Fire Attack Bonus (Flat)】\n" +
                "Increases fire elemental magic damage by a flat amount.\n" +
                "Recommended: 2-5",

                ["Tier3_LightningElement_DamageBonus"] =
                "【Lightning Attack Bonus (Flat)】\n" +
                "Increases lightning elemental magic damage by a flat amount.\n" +
                "Recommended: 2-5",

                // === Tier 4: Lucky Mana ===
                ["Tier4_LuckyMana_Chance"] =
                "【Zero Eitr Cost Chance (%)】\n" +
                "Chance to cast spells without consuming Eitr.\n" +
                "Lucky mana enables infinite casting.\n" +
                "Recommended: 30-40%",

                // === Tier 5-1: Double Cast (R-key Active) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] =
                "【Additional Projectile Count】\n" +
                "Number of extra projectiles fired during double cast.\n" +
                "Recommended: 1-3",

                ["Tier5_DoubleCast_ProjectileDamagePercent"] =
                "【Projectile Damage Percent (%)】\n" +
                "Damage percentage of additional projectiles.\n" +
                "Recommended: 60-80%",

                ["Tier5_DoubleCast_AngleOffset"] =
                "【Projectile Angle Offset (degrees)】\n" +
                "Spread angle of projectiles.\n" +
                "Recommended: 3-8°",

                ["Tier5_DoubleCast_EitrCost"] =
                "【Eitr Cost】\n" +
                "Eitr consumed when activating the skill.\n" +
                "Recommended: 15-25",

                ["Tier5_DoubleCast_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 25-40s",

                // === Tier 5-2: Instant Area Heal (H-key Active) ===
                ["Tier5_InstantAreaHeal_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 25-40s",

                ["Tier5_InstantAreaHeal_EitrCost"] =
                "【Eitr Cost】\n" +
                "Eitr consumed when using the skill.\n" +
                "Recommended: 25-35",

                ["Tier5_InstantAreaHeal_HealPercent"] =
                "【Heal Amount (% of Max HP)】\n" +
                "Healing percentage relative to maximum health.\n" +
                "Recommended: 20-30%",

                ["Tier5_InstantAreaHeal_Range"] =
                "【Heal Range (meters)】\n" +
                "Range where healing is applied.\n" +
                "Recommended: 10-15m",

                ["Tier5_InstantAreaHeal_SelfHeal"] =
                "【Allow Self Heal】\n" +
                "If true, also heals the caster.\n" +
                "Recommended: false (allies only)",

                // === Crossbow Tree ===
                // === Tier 0: Crossbow Expert ===
                ["Tier0_CrossbowExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier0_CrossbowExpert_DamageBonus"] =
                "【Crossbow Damage Bonus (%)】\n" +
                "Increases base damage of crossbows and bolts.\n" +
                "Recommended: 8-12%",

                // === Tier 1: Rapid Fire ===
                ["Tier1_RapidFire_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_RapidFire_Chance"] =
                "【Rapid Fire Trigger Chance (%)】\n" +
                "Chance to trigger rapid fire when shooting crossbow.\n" +
                "Fires multiple bolts rapidly when triggered.\n" +
                "Recommended: 15-25%",

                ["Tier1_RapidFire_ShotCount"] =
                "【Rapid Fire Shot Count】\n" +
                "Number of additional bolts fired during rapid fire.\n" +
                "Recommended: 2-4 shots",

                ["Tier1_RapidFire_DamagePercent"] =
                "【Rapid Fire Damage Percent (%)】\n" +
                "Damage percentage of rapid fire bolts.\n" +
                "Relative to original attack power.\n" +
                "Recommended: 60-80%",

                ["Tier1_RapidFire_Delay"] =
                "【Rapid Fire Interval (seconds)】\n" +
                "Time interval between each bolt during rapid fire.\n" +
                "Shorter means faster firing.\n" +
                "Recommended: 0.1-0.3s",

                ["Tier1_RapidFire_BoltConsumption"] =
                "【Rapid Fire Bolt Consumption】\n" +
                "Number of bolts consumed during rapid fire.\n" +
                "Recommended: 1-2",

                ["Tier2_CrossbowSkills_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                // === Tier 2: Balanced Aim ===
                ["Tier2_BalancedAim_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_BalancedAim_KnockbackChance"] =
                "【Knockback Trigger Chance (%)】\n" +
                "Chance to knock back enemies on crossbow hit.\n" +
                "Stable stance delivers reliable impact.\n" +
                "Recommended: 20-35%",

                ["Tier2_BalancedAim_KnockbackDistance"] =
                "【Knockback Distance (meters)】\n" +
                "Distance enemies are pushed back when knockback triggers.\n" +
                "Recommended: 2-4m",

                // === Tier 2: Rapid Reload ===
                ["Tier2_RapidReload_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_RapidReload_SpeedIncrease"] =
                "【Reload Speed Increase (%)】\n" +
                "Increases crossbow reload speed.\n" +
                "Load the next bolt faster.\n" +
                "Recommended: 10-20%",

                // === Tier 2: Honest Shot ===
                ["Tier2_HonestShot_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_HonestShot_DamageBonus"] =
                "【Base Damage Bonus (%)】\n" +
                "Further increases crossbow base attack power.\n" +
                "Recommended: 10-18%",

                // === Tier 3: Auto Reload ===
                ["Tier3_AutoReload_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_AutoReload_Chance"] =
                "【Auto Reload Trigger Chance (%)】\n" +
                "Chance for next reload to be performed at 200% speed on hit.\n" +
                "Helps maintain continuous attack momentum.\n" +
                "Recommended: 20-35%",

                // === Tier 4: Rapid Fire Lv2 ===
                ["Tier4_CrossbowSkills_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_RapidFireLv2_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_RapidFireLv2_Chance"] =
                "【Rapid Fire Lv2 Trigger Chance (%)】\n" +
                "Chance to trigger enhanced rapid fire.\n" +
                "Stacks with Tier 1 rapid fire chance.\n" +
                "Recommended: 20-35%",

                ["Tier4_RapidFireLv2_ShotCount"] =
                "【Rapid Fire Lv2 Shot Count】\n" +
                "Number of additional bolts in enhanced rapid fire.\n" +
                "Fires more than Tier 1 rapid fire.\n" +
                "Recommended: 4-6 shots",

                ["Tier4_RapidFireLv2_DamagePercent"] =
                "【Rapid Fire Lv2 Damage Percent (%)】\n" +
                "Damage percentage of enhanced rapid fire bolts.\n" +
                "Higher damage than Tier 1.\n" +
                "Recommended: 75-90%",

                ["Tier4_RapidFireLv2_Delay"] =
                "【Rapid Fire Lv2 Interval (seconds)】\n" +
                "Time between each bolt in enhanced rapid fire.\n" +
                "Recommended: 0.1-0.3s",

                ["Tier4_RapidFireLv2_BoltConsumption"] =
                "【Rapid Fire Lv2 Bolt Consumption】\n" +
                "Number of bolts consumed during enhanced rapid fire.\n" +
                "Recommended: 1-2",

                // === Tier 4: Final Strike ===
                ["Tier4_FinalStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_FinalStrike_HpThreshold"] =
                "【Enemy HP Threshold (%)】\n" +
                "Deals bonus damage to enemies at or above this HP percentage.\n" +
                "Effective against high-HP targets.\n" +
                "Recommended: 40-60%",

                ["Tier4_FinalStrike_DamageBonus"] =
                "【Bonus Damage (%)】\n" +
                "Extra damage dealt to enemies above the HP threshold.\n" +
                "Recommended: 20-40%",

                // === Tier 5: One Shot (R-key Active) ===
                ["Tier5_OneShot_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_OneShot_Duration"] =
                "【Buff Duration (seconds)】\n" +
                "Duration the One Shot buff lasts.\n" +
                "Can fire one enhanced shot during this time.\n" +
                "Recommended: 8-12s",

                ["Tier5_OneShot_DamageBonus"] =
                "【One Shot Damage Bonus (%)】\n" +
                "Additional damage bonus when firing One Shot.\n" +
                "Power of the devastating shot.\n" +
                "Recommended: 150-250%",

                ["Tier5_OneShot_KnockbackDistance"] =
                "【Knockback Distance (meters)】\n" +
                "Distance enemies are knocked back on hit.\n" +
                "Powerful impact pushes enemies away.\n" +
                "Recommended: 5-10m",

                ["Tier5_OneShot_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 25-40s",

                // === Bow Tree ===
                // === Tier 0: Bow Expert ===
                ["Tier0_BowExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier0_BowExpert_DamageBonus"] =
                "【Bow Damage Bonus (%)】\n" +
                "Increases base damage of bows and arrows.\n" +
                "Recommended: 8-12%",

                // === Tier 1: Focused Shot ===
                ["Tier1_FocusedShot_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_FocusedShot_CritBonus"] =
                "【Critical Chance Bonus (%)】\n" +
                "Focused shot increases critical hit chance.\n" +
                "Higher focus leads to more critical opportunities.\n" +
                "Recommended: 5-10%",

                // === Tier 2: Multishot Lv1 ===
                ["Tier2_MultishotLv1_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_MultishotLv1_ActivationChance"] =
                "【Multishot Lv1 Trigger Chance (%)】\n" +
                "Chance to trigger multishot when attacking with bow.\n" +
                "Fires multiple arrows simultaneously when triggered.\n" +
                "Recommended: 15-25%",

                ["Tier2_Multishot_AdditionalArrows"] =
                "【Additional Arrow Count】\n" +
                "Number of extra arrows fired during multishot.\n" +
                "Recommended: 2-4",

                ["Tier2_Multishot_ArrowConsumption"] =
                "【Arrow Consumption】\n" +
                "Number of arrows consumed during multishot.\n" +
                "Recommended: 1-2",

                ["Tier2_Multishot_DamagePerArrow"] =
                "【Damage Per Arrow (%)】\n" +
                "Damage percentage for each multishot arrow.\n" +
                "Recommended: 50-70%",

                // === Tier 3: Bow Mastery ===
                ["Tier3_BowMastery_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_SpeedShot_SkillBonus"] =
                "【Bow Proficiency Bonus (Flat)】\n" +
                "Increases bow skill level by a flat amount.\n" +
                "Higher proficiency means stronger attacks.\n" +
                "Recommended: 5-10",

                ["Tier3_SilentShot_DamageBonus"] =
                "【Penetration Damage Bonus (Flat)】\n" +
                "Increases bow damage by a flat amount.\n" +
                "Arrows penetrate enemies dealing more damage.\n" +
                "Recommended: 3-8",

                ["Tier3_SpecialArrow_Chance"] =
                "【Special Arrow Trigger Chance (%)】\n" +
                "Chance to fire special effect arrows.\n" +
                "Can apply poison, fire, frost, or other status effects.\n" +
                "Recommended: 25-35%",

                // === Tier 4: Multishot Lv2 ===
                ["Tier4_MultishotLv2_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_MultishotLv2_ActivationChance"] =
                "【Multishot Lv2 Trigger Chance (%)】\n" +
                "Enhanced multishot trigger chance.\n" +
                "Fires more arrows than Lv1.\n" +
                "Recommended: 20-30%",

                ["Tier4_PowerShot_KnockbackChance"] =
                "【Strong Knockback Chance (%)】\n" +
                "Chance to knock back enemies strongly with bow attacks.\n" +
                "Useful for maintaining distance and crowd control.\n" +
                "Recommended: 30-40%",

                ["Tier4_PowerShot_KnockbackDistance"] =
                "【Knockback Distance (meters)】\n" +
                "Distance enemies are pushed back.\n" +
                "Recommended: 4-8m",

                // === Tier 5: Precision Aim & Advanced Skills ===
                ["Tier5_PrecisionAim_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_PrecisionAim_CritDamage"] =
                "【Critical Damage Bonus (%)】\n" +
                "Precision aim increases critical hit damage.\n" +
                "Target weak points for massive damage.\n" +
                "Recommended: 25-40%",

                ["Tier5_ArrowRain_Chance"] =
                "【Arrow Rain Trigger Chance (%)】\n" +
                "Chance to rapidly fire multiple arrows in succession.\n" +
                "Rains arrows like a storm.\n" +
                "Recommended: 25-35%",

                ["Tier5_ArrowRain_ArrowCount"] =
                "【Arrow Rain Count】\n" +
                "Number of arrows fired during arrow rain.\n" +
                "Recommended: 3-5",

                ["Tier5_BackstepShot_CritBonus"] =
                "【Post-Dodge Crit Bonus (%)】\n" +
                "Critical chance increases after dodging.\n" +
                "Quick counterattack after backstep.\n" +
                "Recommended: 20-35%",

                ["Tier5_BackstepShot_Duration"] =
                "【Post-Dodge Buff Duration (seconds)】\n" +
                "Duration the effect lasts after dodging.\n" +
                "Recommended: 2-4s",

                ["Tier5_HuntingInstinct_CritBonus"] =
                "【Hunting Instinct Crit Bonus (%)】\n" +
                "Hunter's instinct increases critical chance.\n" +
                "Recommended: 8-15%",

                // === Tier 6: Explosive Arrow (R-key Active) ===
                ["Tier6_ExplosiveArrow_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier6_ExplosiveArrow_DamageMultiplier"] =
                "【Explosive Arrow Damage (%)】\n" +
                "R-key active skill - Explosive arrow damage multiplier.\n" +
                "Powerful arrow that deals area damage.\n" +
                "Recommended: 100-150%",

                ["Tier6_ExplosiveArrow_Radius"] =
                "【Explosion Radius (meters)】\n" +
                "Damage radius of explosive arrow.\n" +
                "Recommended: 3-6m",

                ["Tier6_ExplosiveArrow_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 15-25s",

                ["Tier6_ExplosiveArrow_StaminaCost"] =
                "【Stamina Cost (%)】\n" +
                "Stamina percentage consumed when using skill.\n" +
                "Recommended: 10-20%",

                // === Tier 6: Critical Boost ===
                ["Tier6_CritBoost_DamageBonus"] =
                "【Critical Boost Damage (%)】\n" +
                "Additional damage bonus on critical hits.\n" +
                "Recommended: 40-60%",

                ["Tier6_CritBoost_CritChance"] =
                "【Critical Boost Chance (%)】\n" +
                "Greatly increases critical hit chance.\n" +
                "Recommended: 80-100%",

                ["Tier6_CritBoost_ArrowCount"] =
                "【Critical Boost Arrow Count】\n" +
                "Number of arrows fired during critical boost.\n" +
                "Recommended: 3-7",

                ["Tier6_CritBoost_Cooldown"] =
                "【Cooldown (seconds)】\n" +
                "Skill reuse wait time.\n" +
                "Recommended: 35-50s",

                ["Tier6_CritBoost_StaminaCost"] =
                "【Stamina Cost (%)】\n" +
                "Stamina percentage consumed when using skill.\n" +
                "Recommended: 20-30%",

                // === Sword Tree: Required Points (9개) ===
                ["Sword_Expert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_FastSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_CounterStance_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_ComboSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_BladeReflect_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_OffenseDefense_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_TrueDuel_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_ParryCharge_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Sword_RushSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                // === Sword Tree: Sword Expert (1개) ===
                ["Sword_Expert_DamageIncrease"] =
                "【Sword Damage Increase (%)】\n" +
                "Increases base damage of sword weapons.\n" +
                "Applies to all sword types.\n" +
                "Recommended: 10-20%",

                // === Sword Tree: Fast Slash (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases sword attack speed.\n" +
                "Enables rapid consecutive attacks.\n" +
                "Recommended: 10-20%",

                // === Sword Tree: Counter Stance (2개) ===
                ["Tier1_CounterStance_Duration"] =
                "【Duration (sec)】\n" +
                "Duration of the counter stance.\n" +
                "Defense increases during this time.\n" +
                "Recommended: 3-6 sec",

                ["Tier1_CounterStance_DefenseBonus"] =
                "【Defense Bonus (%)】\n" +
                "Defense increase during counter stance.\n" +
                "Withstand enemy attacks and wait for counter opportunity.\n" +
                "Recommended: 20-40%",

                // === Sword Tree: Combo Slash (2개) ===
                ["Sword_ComboSlash_Bonus"] =
                "【Combo Attack Bonus (%)】\n" +
                "Additional damage during consecutive attacks.\n" +
                "Maintaining combos results in high DPS.\n" +
                "Recommended: 15-30%",

                ["Sword_ComboSlash_Duration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of the combo attack bonus.\n" +
                "Attacking again within this time extends the buff.\n" +
                "Recommended: 3-5 sec",

                // === Sword Tree: Riposte (1개) ===
                ["Sword_BladeReflect_DamageBonus"] =
                "【Damage Bonus (Fixed Value)】\n" +
                "Increases riposte damage by a fixed amount.\n" +
                "Enables powerful counter after successful parry.\n" +
                "Recommended: 20-40",

                // === Sword Tree: Attack-Defense Unity (2개) ===
                ["Tier4_AllInOne_AttackBonus"] =
                "【Attack Bonus (%)】\n" +
                "Enhances both attack and defense simultaneously.\n" +
                "Useful for balanced combat style.\n" +
                "Recommended: 10-20%",

                ["Tier4_AllInOne_DefenseBonus"] =
                "【Defense Bonus (Fixed Value)】\n" +
                "Defense bonus of the unity stance.\n" +
                "Enables strong defense while attacking.\n" +
                "Recommended: 15-30",

                // === Sword Tree: True Duel (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus specialized for 1v1 combat.\n" +
                "Overwhelm enemies with rapid strikes.\n" +
                "Recommended: 15-30%",

                // === Sword Tree: Parry Rush (H-Key Active - 5개) ===
                ["Sword_ParryCharge_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of the buff after successful parry.\n" +
                "Enhanced attacks are possible during this time.\n" +
                "Recommended: 5-10 sec",

                ["Sword_ParryCharge_DamageBonus"] =
                "【Rush Damage Bonus (%)】\n" +
                "Rush attack damage increase on successful parry.\n" +
                "Deliver powerful counter with perfect timing.\n" +
                "Recommended: 50-100%",

                ["Sword_ParryCharge_PushDistance"] =
                "【Push Distance (m)】\n" +
                "Distance enemies are pushed back during rush.\n" +
                "Useful for distance control and battlefield management.\n" +
                "Recommended: 3-7m",

                ["Sword_ParryCharge_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on H-key buff activation.\n" +
                "Stamina management is important.\n" +
                "Recommended: 20-40",

                ["Sword_ParryCharge_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse delay.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 10-20 sec",

                // === Sword Tree: Rush Slash (G-Key Active - 9개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] =
                "【1st Hit Damage Ratio (%)】\n" +
                "Damage ratio of the first attack in rush slash.\n" +
                "Multiplier based on base attack power.\n" +
                "Recommended: 80-120%",

                ["Sword_RushSlash_Hit2DamageRatio"] =
                "【2nd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the second attack in rush slash.\n" +
                "Damage increases as the combo continues.\n" +
                "Recommended: 100-150%",

                ["Sword_RushSlash_Hit3DamageRatio"] =
                "【3rd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the final attack in rush slash.\n" +
                "The most powerful finishing strike.\n" +
                "Recommended: 150-200%",

                ["Sword_RushSlash_InitialDashDistance"] =
                "【Initial Rush Distance (m)】\n" +
                "Rush distance at skill activation.\n" +
                "Quickly close in on enemies.\n" +
                "Recommended: 5-10m",

                ["Sword_RushSlash_SideMovementDistance"] =
                "【Side Move Distance (m)】\n" +
                "Lateral movement distance during combo.\n" +
                "Evade enemy attacks while attacking.\n" +
                "Recommended: 2-5m",

                ["Sword_RushSlash_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on G-key skill use.\n" +
                "Powerful skill requires significant stamina.\n" +
                "Recommended: 40-60",

                ["Sword_RushSlash_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse delay.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 15-30 sec",

                ["Sword_RushSlash_MovementSpeed"] =
                "【Movement Speed (m/s)】\n" +
                "Movement speed during rush.\n" +
                "Faster speed enables more dynamic combat.\n" +
                "Recommended: 8-15 m/s",

                ["Sword_RushSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus during skill (vs base).\n" +
                "Other tree bonuses are ignored, only this value applies.\n" +
                "Recommended: 20-40%",

                // === Sword Tree: New Tier* format keys (matches current Sword_Config.cs) ===
                ["Tier0_SwordExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier0_SwordExpert_DamageBonus"] =
                "【Sword Damage Increase (%)】\n" +
                "Increases base attack power of sword weapons.\n" +
                "Applies to all sword types.\n" +
                "Recommended: 10-20%",

                ["Tier1_FastSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_CounterStance_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_FastSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases sword attack speed.\n" +
                "Enables rapid consecutive attacks.\n" +
                "Recommended: 10-20%",

                ["Tier2_ComboSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_ComboSlash_DamageBonus"] =
                "【Combo Attack Bonus (%)】\n" +
                "Deals additional damage on consecutive attacks.\n" +
                "Maintaining combos achieves high DPS.\n" +
                "Recommended: 15-30%",

                ["Tier2_ComboSlash_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration the combo attack bonus remains active.\n" +
                "Attacking again within this time extends the buff.\n" +
                "Recommended: 3-5 sec",

                ["Tier3_Riposte_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_Riposte_DamageBonus"] =
                "【Attack Power Bonus (flat)】\n" +
                "Increases riposte attack power by a flat amount.\n" +
                "Enables powerful counterattacks after parrying.\n" +
                "Recommended: 5-15",

                ["Tier4_AllInOne_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_TrueDuel_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_TrueDuel_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus specialized for 1v1 combat.\n" +
                "Overwhelm opponents with rapid strikes.\n" +
                "Recommended: 15-30%",

                ["Tier5_ParryRush_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_ParryRush_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration the buff remains after successful parry.\n" +
                "Enhanced attacks are possible during this time.\n" +
                "Recommended: 20-40 sec",

                ["Tier5_ParryRush_DamageBonus"] =
                "【Rush Attack Damage Bonus (%)】\n" +
                "Damage increase for rush attack on successful parry.\n" +
                "Deliver a powerful counterattack at the perfect timing.\n" +
                "Recommended: 50-100%",

                ["Tier5_ParryRush_PushDistance"] =
                "【Knockback Distance (m)】\n" +
                "Distance enemies are pushed during rush.\n" +
                "Useful for range control and battlefield positioning.\n" +
                "Recommended: 3-7m",

                ["Tier5_ParryRush_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when activating the skill.\n" +
                "Stamina management is important.\n" +
                "Recommended: 10-20",

                ["Tier5_ParryRush_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse wait time.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 30-60 sec",

                ["Tier6_RushSlash_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier6_RushSlash_Hit1DamageRatio"] =
                "【1st Hit Damage Ratio (%)】\n" +
                "Damage ratio of the first hit in Rush Slash.\n" +
                "Multiplier relative to base attack power.\n" +
                "Recommended: 60-90%",

                ["Tier6_RushSlash_Hit2DamageRatio"] =
                "【2nd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the second hit in Rush Slash.\n" +
                "Damage increases as the combo continues.\n" +
                "Recommended: 70-100%",

                ["Tier6_RushSlash_Hit3DamageRatio"] =
                "【3rd Hit Damage Ratio (%)】\n" +
                "Damage ratio of the final hit in Rush Slash.\n" +
                "The most powerful finishing strike.\n" +
                "Recommended: 80-120%",

                ["Tier6_RushSlash_InitialDistance"] =
                "【Initial Dash Distance (m)】\n" +
                "Distance dashed when skill starts.\n" +
                "Rapidly closes in on the target.\n" +
                "Recommended: 3-8m",

                ["Tier6_RushSlash_SideDistance"] =
                "【Side Movement Distance (m)】\n" +
                "Left/right movement distance during consecutive attacks.\n" +
                "Evade enemy attacks while striking.\n" +
                "Recommended: 2-5m",

                ["Tier6_RushSlash_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using G key skill.\n" +
                "Powerful skill requires significant stamina.\n" +
                "Recommended: 20-40",

                ["Tier6_RushSlash_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse wait time.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 15-30 sec",

                ["Tier6_RushSlash_MoveSpeed"] =
                "【Movement Speed (m/s)】\n" +
                "Movement speed during the rush.\n" +
                "Faster speed enables more dynamic combat.\n" +
                "Recommended: 10-25 m/s",

                ["Tier6_RushSlash_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Attack speed bonus during skill (vs base).\n" +
                "Other tree bonuses are ignored, only this value applies.\n" +
                "Recommended: 150-250%",

                // ========================================
                // Knife Tree (Tier 0~8, 32 keys)
                // ========================================

                // === Knife Tree: Required Points (9 keys) ===
                ["Tier0_KnifeExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_Evasion_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_FastMove_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_CombatMastery_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_AttackEvasion_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_CriticalDamage_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier6_Assassin_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier7_Assassination_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier8_AssassinHeart_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                // === Knife Tree: Tier 0 - Knife Expert (1 key) ===
                ["Tier0_KnifeExpert_BackstabBonus"] =
                "【Backstab Damage Bonus (%)】\n" +
                "Additional damage when attacking from behind.\n" +
                "Fundamental technique for assassins.\n" +
                "Recommended: 30-50%",

                // === Knife Tree: Tier 1 - Evasion (2 keys) ===
                ["Tier1_Evasion_Chance"] =
                "【Evasion Chance (%)】\n" +
                "Probability of evading incoming attacks.\n" +
                "Activates automatically when attacked.\n" +
                "Recommended: 5-15%",

                ["Tier1_Evasion_Duration"] =
                "【Evasion Buff Duration (sec)】\n" +
                "Duration of evasion buff effect.\n" +
                "Provides temporary invulnerability window.\n" +
                "Recommended: 0.5-1.5 sec",

                // === Knife Tree: Tier 2 - Fast Move (1 key) ===
                ["Tier2_FastMove_MoveSpeedBonus"] =
                "【Movement Speed Bonus (%)】\n" +
                "Increases base movement speed.\n" +
                "Critical for assassin mobility.\n" +
                "Recommended: 10-20%",

                // === Knife Tree: Tier 3 - Combat Mastery (2 keys) ===
                ["Tier3_CombatMastery_DamageBonus"] =
                "【Knife Damage Bonus (%)】\n" +
                "Increases knife weapon damage.\n" +
                "Core damage scaling stat.\n" +
                "Recommended: 15-30%",

                ["Tier3_CombatMastery_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of combat mastery buff.\n" +
                "Determines sustained damage window.\n" +
                "Recommended: 5-10 sec",

                // === Knife Tree: Tier 4 - Attack Evasion (3 keys) ===
                ["Tier4_AttackEvasion_EvasionBonus"] =
                "【Evasion Bonus (%)】\n" +
                "Additional evasion chance during attacks.\n" +
                "Stacks with base evasion.\n" +
                "Recommended: 5-10%",

                ["Tier4_AttackEvasion_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of attack evasion buff.\n" +
                "Active during aggressive combat.\n" +
                "Recommended: 3-8 sec",

                ["Tier4_AttackEvasion_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reactivation wait time.\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 10-20 sec",

                // === Knife Tree: Tier 5 - Critical Damage (1 key) ===
                ["Tier5_CriticalDamage_DamageBonus"] =
                "【Critical Damage Bonus (%)】\n" +
                "Increases critical hit damage multiplier.\n" +
                "Synergizes with critical chance.\n" +
                "Recommended: 20-40%",

                // === Knife Tree: Tier 6 - Assassin (2 keys) ===
                ["Tier6_Assassin_CritDamageBonus"] =
                "【Critical Damage Bonus (%)】\n" +
                "Additional critical damage multiplier.\n" +
                "Further enhances critical strikes.\n" +
                "Recommended: 30-50%",

                ["Tier6_Assassin_CritChanceBonus"] =
                "【Critical Chance Bonus (%)】\n" +
                "Increases critical hit probability.\n" +
                "Pairs with critical damage bonuses.\n" +
                "Recommended: 10-20%",

                // === Knife Tree: Tier 7 - Assassination (2 keys) ===
                ["Tier7_Assassination_StaggerChance"] =
                "【Stagger Chance (%)】\n" +
                "Probability of staggering enemies.\n" +
                "Interrupts enemy actions on successful combo.\n" +
                "Recommended: 30-60%",

                ["Tier7_Assassination_RequiredComboHits"] =
                "【Required Combo Hits】\n" +
                "Number of hits needed to trigger stagger.\n" +
                "Lower values mean easier activation.\n" +
                "Recommended: 3-5 hits",

                // === Knife Tree: Tier 8 - Assassin Heart (G-Key Active Skill, 10 keys) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] =
                "【Critical Damage Multiplier (x)】\n" +
                "Critical damage multiplier during skill.\n" +
                "Massively boosts assassination damage.\n" +
                "Recommended: 2.0-4.0x",

                ["Tier8_AssassinHeart_Duration"] =
                "【Skill Duration (sec)】\n" +
                "Active duration of Assassin Heart.\n" +
                "Determines time window for enhanced attacks.\n" +
                "Recommended: 5-10 sec",

                ["Tier8_AssassinHeart_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on activation.\n" +
                "Manage stamina carefully.\n" +
                "Recommended: 20-40",

                ["Tier8_AssassinHeart_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reactivation wait time.\n" +
                "Balance power with frequency.\n" +
                "Recommended: 30-60 sec",

                ["Tier8_AssassinHeart_TeleportRange"] =
                "【Teleport Range (m)】\n" +
                "Maximum teleport distance to enemy.\n" +
                "Gap-closer for assassination.\n" +
                "Recommended: 10-20 m",

                ["Tier8_AssassinHeart_TeleportBackDistance"] =
                "【Teleport Back Distance (m)】\n" +
                "Retreat distance after combo.\n" +
                "Safety disengagement range.\n" +
                "Recommended: 5-10 m",

                ["Tier8_AssassinHeart_StunDuration"] =
                "【Stun Duration (sec)】\n" +
                "Enemy stun duration on teleport.\n" +
                "Allows safe combo execution.\n" +
                "Recommended: 1-3 sec",

                ["Tier8_AssassinHeart_ComboAttackCount"] =
                "【Combo Attack Count】\n" +
                "Number of rapid attacks in combo.\n" +
                "More hits = more damage.\n" +
                "Recommended: 5-10 hits",

                ["Tier8_AssassinHeart_AttackInterval"] =
                "【Attack Interval (sec)】\n" +
                "Time between combo attacks.\n" +
                "Shorter = faster DPS.\n" +
                "Recommended: 0.1-0.3 sec",

                ["Tier8_AssassinHeart_RequiredKills"] =
                "【Required Kills】\n" +
                "Kills needed to unlock this skill.\n" +
                "Proves mastery of knife combat.\n" +
                "Recommended: 50-100",

                // ========================================
                // Spear Tree (Tier 0~5, 35 keys)
                // ========================================

                // === Spear Tree: Required Points (7 keys) ===
                ["Tier0_SpearExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_QuickStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_Throw_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_Pierce_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_Evasion_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_Penetrate_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_Combo_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                // === Spear Tree: Tier 0 - Spear Expert (3 keys) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] =
                "【Attack Speed Bonus After 2-Hit (%)】\n" +
                "Attack speed increases after 2 consecutive spear hits.\n" +
                "Overwhelm enemies with rapid combos.\n" +
                "Recommended: 10-20%",

                ["Tier0_SpearExpert_2HitDamageBonus"] =
                "【Damage Bonus After 2-Hit (%)】\n" +
                "Damage increases after 2 consecutive spear hits.\n" +
                "Maximizes combo attack damage.\n" +
                "Recommended: 7-15%",

                ["Tier0_SpearExpert_EffectDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of 2-hit combo effect.\n" +
                "Longer duration enables stable combat.\n" +
                "Recommended: 4-8 sec",

                // === Spear Tree: Tier 1 - Vital Strike (1 key) ===
                ["Tier1_VitalStrike_DamageBonus"] =
                "【Critical Damage Bonus (%)】\n" +
                "Increases spear critical hit damage.\n" +
                "Specialized for precision vital strikes.\n" +
                "Recommended: 20-40%",

                // === Spear Tree: Tier 2 - Throw (3 keys) ===
                ["Tier2_Throw_Cooldown"] =
                "【Throw Cooldown (sec)】\n" +
                "Skill reactivation wait time for spear throw.\n" +
                "Shorter allows more frequent throws.\n" +
                "Recommended: 20-40 sec",

                ["Tier2_Throw_DamageMultiplier"] =
                "【Throw Damage Multiplier (%)】\n" +
                "Damage multiplier for thrown spears.\n" +
                "Determines ranged attack power.\n" +
                "Recommended: 100-150%",

                ["Tier2_Throw_BuffDuration_NotUsed"] =
                "【Not Used】\n" +
                "This setting is currently unused.\n" +
                "Changed to passive skill.",

                // === Spear Tree: Tier 3 - Rapid Pierce (1 key) ===
                ["Tier3_Rapid_DamageBonus"] =
                "【Weapon Damage Bonus (Flat)】\n" +
                "Flat damage increase to base spear damage.\n" +
                "Favors rapid consecutive attacks.\n" +
                "Recommended: 3-6",

                // === Spear Tree: Tier 3 - Explosive Spear (3 keys) ===
                ["Tier3_Explosion_Chance"] =
                "【Explosion Trigger Chance (%)】\n" +
                "Probability of explosion effect on spear hit.\n" +
                "Deals area damage.\n" +
                "Recommended: 20-40%",

                ["Tier3_Explosion_Radius"] =
                "【Explosion Radius (m)】\n" +
                "Radius of explosion damage.\n" +
                "Larger radius damages multiple enemies.\n" +
                "Recommended: 4-7 m",

                ["Tier3_Explosion_DamageBonus"] =
                "【Explosion Damage Bonus (%)】\n" +
                "Additional damage from explosion.\n" +
                "Core of area-of-effect damage.\n" +
                "Recommended: 25-40%",

                // === Spear Tree: Tier 4 - Evasion Strike (2 keys) ===
                ["Tier4_Evasion_DamageBonus"] =
                "【Damage Bonus After Dodge (%)】\n" +
                "Spear damage increases immediately after dodging.\n" +
                "Enhances both mobility and damage.\n" +
                "Recommended: 20-35%",

                ["Tier4_Evasion_StaminaReduction"] =
                "【Stamina Cost Reduction (%)】\n" +
                "Reduces stamina cost for attacks after dodge.\n" +
                "Enables sustained combat.\n" +
                "Recommended: 5-12%",

                // === Spear Tree: Tier 4 - Dual Spear (2 keys) ===
                ["Tier4_Dual_DamageBonus"] =
                "【2-Hit Damage Bonus (%)】\n" +
                "Additional damage on 2 consecutive hits.\n" +
                "Specialized for combo damage.\n" +
                "Recommended: 18-30%",

                ["Tier4_Dual_Duration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of dual spear buff.\n" +
                "Longer duration for stable damage output.\n" +
                "Recommended: 8-15 sec",

                // === Spear Tree: Tier 5 - Penetrating Spear (G-Key Active, 6 keys) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] =
                "【Not Used】\n" +
                "This setting is currently unused.\n" +
                "Changed to lightning strike effect.",

                ["Tier5_Penetrate_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of penetrating spear buff.\n" +
                "Determines skill effect duration.\n" +
                "Recommended: 25-40 sec",

                ["Tier5_Penetrate_LightningDamage"] =
                "【Lightning Strike Damage Multiplier (%)】\n" +
                "Lightning damage multiplier on combo trigger.\n" +
                "Deals powerful additional damage.\n" +
                "Recommended: 200-300%",

                ["Tier5_Penetrate_HitCount"] =
                "【Lightning Trigger Combo Hits】\n" +
                "Consecutive hits required to trigger lightning.\n" +
                "Lower values trigger more frequently.\n" +
                "Recommended: 3-5 hits",

                ["Tier5_Penetrate_GKey_Cooldown"] =
                "【G-Key Skill Cooldown (sec)】\n" +
                "Reactivation time for Penetrating Spear (G-key).\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 50-80 sec",

                ["Tier5_Penetrate_GKey_StaminaCost"] =
                "【G-Key Skill Stamina Cost】\n" +
                "Stamina consumed when using Penetrating Spear.\n" +
                "Stamina management is critical.\n" +
                "Recommended: 20-35",

                // === Spear Tree: Tier 5 - Combo Spear (H-Key Active, 7 keys) ===
                ["Tier5_Combo_HKey_Cooldown"] =
                "【H-Key Skill Cooldown (sec)】\n" +
                "Reactivation time for Combo Spear (H-key).\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 20-35 sec",

                ["Tier5_Combo_HKey_DamageMultiplier"] =
                "【H-Key Skill Damage Multiplier (%)】\n" +
                "Damage multiplier for Combo Spear (H-key).\n" +
                "Powerful burst damage skill.\n" +
                "Recommended: 250-350%",

                ["Tier5_Combo_HKey_StaminaCost"] =
                "【H-Key Skill Stamina Cost】\n" +
                "Stamina consumed when using Combo Spear.\n" +
                "Stamina management required.\n" +
                "Recommended: 15-30",

                ["Tier5_Combo_HKey_KnockbackRange"] =
                "【H-Key Skill Knockback Range (m)】\n" +
                "Distance enemies are pushed back on hit.\n" +
                "Useful for combat positioning.\n" +
                "Recommended: 2-5 m",

                ["Tier5_Combo_ActiveRange"] =
                "【Active Effect Range (m)】\n" +
                "Range where combo spear buff activates.\n" +
                "Larger range triggers in more situations.\n" +
                "Recommended: 2-5 m",

                ["Tier5_Combo_BuffDuration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of combo spear buff.\n" +
                "Longer duration for stable enhanced throws.\n" +
                "Recommended: 25-40 sec",

                ["Tier5_Combo_MaxUses"] =
                "【Max Enhanced Throws】\n" +
                "Maximum enhanced throws available during buff.\n" +
                "More uses extend enhanced effect duration.\n" +
                "Recommended: 2-5 uses",

                // Mace Tree (Tier-based Sorting)
                // ========================================

                // === Tier 0: Mace Expert ===
                ["Tier0_MaceExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier0_MaceExpert_DamageBonus"] =
                "【Mace Damage Bonus (%)】\n" +
                "Increases base damage of mace weapons.\n" +
                "Applies to all blunt weapons (clubs, maces, etc.).\n" +
                "Recommended: 5-10%",

                ["Tier0_MaceExpert_StunChance"] =
                "【Stun Chance (%)】\n" +
                "Chance to stun enemies on mace attacks.\n" +
                "Stunned enemies cannot act.\n" +
                "Recommended: 15-25%",

                ["Tier0_MaceExpert_StunDuration"] =
                "【Stun Duration (sec)】\n" +
                "Duration of the stun effect.\n" +
                "Longer duration provides safe damage windows.\n" +
                "Recommended: 0.3-1 sec",

                // === Tier 1: Mace Damage Enhancement ===
                ["Tier1_MaceExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_MaceExpert_DamageBonus"] =
                "【Mace Damage Bonus (%)】\n" +
                "Additional damage bonus for mace weapons.\n" +
                "Recommended: 8-15%",

                // === Tier 2: Stun Boost ===
                ["Tier2_StunBoost_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_StunBoost_StunChanceBonus"] =
                "【Stun Chance Bonus (%)】\n" +
                "Additional increase to stun chance.\n" +
                "Stacks with Mace Expert skill.\n" +
                "Recommended: 10-20%",

                ["Tier2_StunBoost_StunDurationBonus"] =
                "【Stun Duration Bonus (sec)】\n" +
                "Additional increase to stun duration.\n" +
                "Provides even longer damage windows.\n" +
                "Recommended: 0.3-0.8 sec",

                // === Tier 3: Guard ===
                ["Tier3_Guard_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_Guard_ArmorBonus"] =
                "【Armor Bonus (Fixed Value)】\n" +
                "Increases base armor by a fixed amount.\n" +
                "Useful for tank builds.\n" +
                "Recommended: 2-5",

                // === Tier 3: Heavy Strike ===
                ["Tier3_HeavyStrike_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_HeavyStrike_DamageBonus"] =
                "【Damage Bonus (Fixed Value)】\n" +
                "Increases mace damage by a fixed amount.\n" +
                "Applies alongside percentage bonuses.\n" +
                "Recommended: 2-5",

                // === Tier 4: Push ===
                ["Tier4_Push_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_Push_KnockbackChance"] =
                "【Knockback Chance (%)】\n" +
                "Chance to knock back enemies on attack.\n" +
                "Useful for distance control and battlefield management.\n" +
                "Recommended: 25-35%",

                // === Tier 5: Tank ===
                ["Tier5_Tank_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_Tank_HealthBonus"] =
                "【Health Bonus (%)】\n" +
                "Increases maximum health.\n" +
                "Essential for survivability.\n" +
                "Recommended: 20-30%",

                ["Tier5_Tank_DamageReduction"] =
                "【Damage Reduction (%)】\n" +
                "Reduces all incoming damage.\n" +
                "Works with armor for optimal tanking.\n" +
                "Recommended: 8-15%",

                // === Tier 5: DPS ===
                ["Tier5_DPS_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_DPS_DamageBonus"] =
                "【Damage Bonus (%)】\n" +
                "Additional damage increase for mace weapons.\n" +
                "Useful for DPS builds.\n" +
                "Recommended: 15-25%",

                ["Tier5_DPS_AttackSpeedBonus"] =
                "【Attack Speed Bonus (%)】\n" +
                "Increases mace attack speed.\n" +
                "Compensates for slow mace attacks.\n" +
                "Recommended: 8-15%",

                // === Tier 6: Grandmaster ===
                ["Tier6_Grandmaster_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier6_Grandmaster_ArmorBonus"] =
                "【Armor Bonus (%)】\n" +
                "Percentage-based armor bonus.\n" +
                "Great synergy with high-tier armor.\n" +
                "Recommended: 15-25%",

                // === Tier 7: Fury Hammer (H-Key Active) ===
                ["Tier7_FuryHammer_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier7_FuryHammer_NormalHitMultiplier"] =
                "【Hits 1-4 Damage Multiplier (%)】\n" +
                "Damage multiplier for hits 1-4 of H-key skill 'Fury Hammer'.\n" +
                "Based on current attack power.\n" +
                "Recommended: 70-90%",

                ["Tier7_FuryHammer_FinalHitMultiplier"] =
                "【Hit 5 (Final) Damage Multiplier (%)】\n" +
                "Damage multiplier for the final hit of H-key skill 'Fury Hammer'.\n" +
                "Based on current attack power.\n" +
                "The most powerful finishing blow.\n" +
                "Recommended: 130-180%",

                ["Tier7_FuryHammer_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on H-key skill use.\n" +
                "Stamina management is important.\n" +
                "Recommended: 35-45",

                ["Tier7_FuryHammer_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Skill reuse delay.\n" +
                "Shorter cooldown allows more frequent use.\n" +
                "Recommended: 25-35 sec",

                ["Tier7_FuryHammer_AoeRadius"] =
                "【AOE Radius (meters)】\n" +
                "Area damage radius of the skill.\n" +
                "Larger radius hits more enemies.\n" +
                "Recommended: 4-7m",

                // === Tier 7: Guardian Heart (G-Key Active) ===
                ["Tier7_GuardianHeart_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "Reuse delay for G-key skill 'Guardian Heart'.\n" +
                "Shorter cooldown allows more frequent defensive stance.\n" +
                "Recommended: 100-140 sec",

                ["Tier7_GuardianHeart_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed on skill use.\n" +
                "Stamina management is crucial for tanking.\n" +
                "Recommended: 20-30",

                ["Tier7_GuardianHeart_Duration"] =
                "【Buff Duration (sec)】\n" +
                "Duration of the defensive stance.\n" +
                "Reflects damage and maintains high defense during this time.\n" +
                "Recommended: 40-50 sec",

                ["Tier7_GuardianHeart_ReflectPercent"] =
                "【Reflect Damage Ratio (%)】\n" +
                "Percentage of received damage reflected to attacker.\n" +
                "Deals damage back to enemies as a tank.\n" +
                "Recommended: 5-8%",

                ["Tier7_GuardianHeart_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                // ========================================
                // Polearm Tree (Tier 0~6, 25 keys)
                // ========================================

                // === Polearm Tree: Required Points (7 keys) ===
                ["Tier0_PolearmExpert_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier1_PolearmSkill_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier2_PolearmSkill_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier3_PolearmSkill_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier4_PolearmSkill_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier5_Suppress_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                ["Tier6_PierceCharge_RequiredPoints"] =
                "【Required Points】\n" +
                "Skill points needed to unlock this node.",

                // === Polearm Tree: Tier 0 - Polearm Expert (1 key) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] =
                "【Attack Range Bonus (%)】\n" +
                "Increases attack range of polearms (halberds, glaives, etc.).\n" +
                "Long reach allows safe distance attacks.\n" +
                "Recommended: 10-20%",

                // === Polearm Tree: Tier 1 - Spin Wheel (1 key) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] =
                "【Spinning Attack Damage Bonus (%)】\n" +
                "Additional damage on spinning attacks.\n" +
                "Useful against multiple enemies.\n" +
                "Recommended: 50-80%",

                // === Polearm Tree: Tier 2 - Hero Strike (1 key) ===
                ["Tier2_HeroStrike_KnockbackChance"] =
                "【Knockback Chance (%)】\n" +
                "Probability of pushing enemies back on hit.\n" +
                "Useful for battlefield control.\n" +
                "Recommended: 20-35%",

                // === Polearm Tree: Tier 3 - Area Combo (2 keys) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] =
                "【2-Hit Damage Bonus (%)】\n" +
                "Additional damage on 2 consecutive hits.\n" +
                "Specialized for area combo damage.\n" +
                "Recommended: 20-35%",

                ["Tier3_AreaCombo_DoubleHitDuration"] =
                "【2-Hit Buff Duration (sec)】\n" +
                "Duration of 2-hit combo buff.\n" +
                "Longer duration for stable combos.\n" +
                "Recommended: 4-8 sec",

                // === Polearm Tree: Tier 3 - Ground Wheel (1 key) ===
                ["Tier3_GroundWheel_WheelAttackDamageBonus"] =
                "【Spinning Attack Damage Bonus (%)】\n" +
                "Increases ground-smashing spinning attack damage.\n" +
                "Core area damage skill.\n" +
                "Recommended: 70-100%",

                // === Polearm Tree: Tier 3 - Polearm Boost (1 key) ===
                ["Tier3_PolearmBoost_WeaponDamageBonus"] =
                "【Weapon Damage Bonus (Flat)】\n" +
                "Flat damage increase to base polearm damage.\n" +
                "Applies to all polearm attacks.\n" +
                "Recommended: 4-7",

                // === Polearm Tree: Tier 4 - Moon Slash (2 keys) ===
                ["Tier4_MoonSlash_AttackRangeBonus"] =
                "【Attack Range Bonus (%)】\n" +
                "Increases Moon Slash attack range.\n" +
                "Hit more enemies in wider arc.\n" +
                "Recommended: 12-20%",

                ["Tier4_MoonSlash_StaminaReduction"] =
                "【Stamina Cost Reduction (%)】\n" +
                "Reduces stamina cost when using Moon Slash.\n" +
                "Enables sustained combat.\n" +
                "Recommended: 12-20%",

                // === Polearm Tree: Tier 5 - Suppress Attack (1 key) ===
                ["Tier5_Suppress_DamageBonus"] =
                "【Suppress Attack Damage Bonus (%)】\n" +
                "Additional damage on suppress attacks.\n" +
                "Dominate enemies and seize combat initiative.\n" +
                "Recommended: 25-40%",

                // === Polearm Tree: Tier 6 - Pierce Charge (G-Key Active, 8 keys) ===
                ["Tier6_PierceCharge_DashDistance"] =
                "【Dash Distance (m)】\n" +
                "Charge distance during Pierce Charge.\n" +
                "Long dash to penetrate enemy lines.\n" +
                "Recommended: 8-12 m",

                ["Tier6_PierceCharge_FirstHitDamageBonus"] =
                "【First Hit Damage Bonus (%)】\n" +
                "Damage multiplier for initial charge hit.\n" +
                "Powerful opening strike to suppress enemies.\n" +
                "Recommended: 180-250%",

                ["Tier6_PierceCharge_AoeDamageBonus"] =
                "【AOE Knockback Damage Bonus (%)】\n" +
                "Damage multiplier for AOE knockback after charge.\n" +
                "Push back and damage surrounding enemies.\n" +
                "Recommended: 130-180%",

                ["Tier6_PierceCharge_AoeAngle"] =
                "【AOE Angle (degrees)】\n" +
                "Angle of AOE knockback effect.\n" +
                "280 degrees = rear/side area excluding front 80 degrees.\n" +
                "Recommended: 250-300 degrees",

                ["Tier6_PierceCharge_AoeRadius"] =
                "【AOE Radius (m)】\n" +
                "Radius of AOE knockback effect.\n" +
                "Larger radius pushes more enemies.\n" +
                "Recommended: 4-7 m",

                ["Tier6_PierceCharge_KnockbackDistance"] =
                "【Knockback Distance (m)】\n" +
                "Distance enemies are pushed back.\n" +
                "Useful for battlefield control.\n" +
                "Recommended: 6-10 m",

                ["Tier6_PierceCharge_StaminaCost"] =
                "【Stamina Cost】\n" +
                "Stamina consumed when using G-key skill.\n" +
                "Stamina management is important.\n" +
                "Recommended: 18-25",

                ["Tier6_PierceCharge_Cooldown"] =
                "【Cooldown (sec)】\n" +
                "G-key skill reactivation wait time.\n" +
                "Shorter allows more frequent use.\n" +
                "Recommended: 25-40 sec",

                // === Knife Tree: Required Points (9개) ===
                ["Knife_Expert_RequiredPoints"] = "Tier 0: Dagger Expert - Required Points",
                ["Knife_Evasion_RequiredPoints"] = "Tier 1: Evasion Mastery - Required Points",
                ["Knife_FastMove_RequiredPoints"] = "Tier 2: Swift Movement - Required Points",
                ["Knife_CombatMastery_RequiredPoints"] = "Tier 3: Combat Mastery - Required Points",
                ["Knife_AttackEvasion_RequiredPoints"] = "Tier 4: Attack & Evasion - Required Points",
                ["Knife_CriticalDamage_RequiredPoints"] = "Tier 5: Lethal Damage - Required Points",
                ["Knife_Assassin_RequiredPoints"] = "Tier 6: Assassin - Required Points",
                ["Knife_AssassinSkill_RequiredPoints"] = "Tier 7: Assassination Art - Required Points",
                ["Knife_AssassinHeart_RequiredPoints"] = "Tier 8: Assassin's Heart - Required Points",

                // === Knife Tree: Dagger Expert (1개) ===
                ["Knife_Expert_BackstabDamageBonus"] = "Tier 0: Dagger Expert - Backstab Damage Bonus (%)",

                // === Knife Tree: Evasion Mastery (2개) ===
                ["Knife_Evasion_Chance"] = "Tier 1: Evasion Mastery - Evasion Chance (%)",
                ["Knife_Evasion_InvincibilityDuration"] = "Tier 1: Evasion Mastery - Invincibility Duration After Dodge (sec)",

                // === Knife Tree: Swift Movement (1개) ===
                ["Knife_FastMove_MovementSpeedIncrease"] = "Tier 2: Swift Movement - Move Speed Increase (%)",

                // === Knife Tree: Combat Mastery (2개) ===
                ["Knife_CombatMastery_DamageIncrease"] = "Tier 3: Combat Mastery - Dagger Damage Increase On Kill (Fixed Value)",
                ["Knife_CombatMastery_EffectDuration"] = "Tier 3: Combat Mastery - Effect Duration (sec)",

                // === Knife Tree: Attack & Evasion (3개) ===
                ["Knife_AttackEvasion_EvasionRateIncrease"] = "Tier 4: Attack & Evasion - Evasion Rate Increase on 2-Hit Combo (%)",
                ["Knife_AttackEvasion_EffectDuration"] = "Tier 4: Attack & Evasion - Effect Duration (sec)",
                ["Knife_AttackEvasion_Cooldown"] = "Tier 4: Attack & Evasion - Cooldown (sec)",

                // === Knife Tree: Lethal Damage (1개) ===
                ["Knife_CriticalDamage_DamageIncrease"] = "Tier 5: Lethal Damage - Damage Increase (%)",

                // === Knife Tree: Assassin (2개) ===
                ["Knife_Assassin_CritDamageIncrease"] = "Tier 6: Assassin - Critical Damage Increase (%)",
                ["Knife_Assassin_CritChanceIncrease"] = "Tier 6: Assassin - Critical Chance Increase (%)",

                // === Knife Tree: Assassination Art (2개) ===
                ["Knife_AssassinSkill_StaggerChance"] = "Tier 7: Assassination Art - Stagger Chance on 3-Hit Combo (%)",
                ["Knife_AssassinSkill_RequiredComboHits"] = "Tier 7: Assassination Art - Required Consecutive Hits for Stagger",

                // === Knife Tree: Assassin's Heart (9개) ===
                ["Knife_AssassinHeart_CritDamageMultiplier"] = "Tier 8: Assassin's Heart - Critical Damage Multiplier",
                ["Knife_AssassinHeart_EffectDuration"] = "Tier 8: Assassin's Heart - Effect Duration (sec)",
                ["Knife_AssassinHeart_StaminaCost"] = "Tier 8: Assassin's Heart - Stamina Cost",
                ["Knife_AssassinHeart_Cooldown"] = "Tier 8: Assassin's Heart - Cooldown (sec)",
                ["Knife_AssassinHeart_TeleportSearchRange"] = "Tier 8: Assassin's Heart - Teleport Target Detection Range (m)",
                ["Knife_AssassinHeart_TeleportBackDistance"] = "Tier 8: Assassin's Heart - Distance Behind Target (m)",
                ["Knife_AssassinHeart_StunDuration"] = "Tier 8: Assassin's Heart - Target Stun Duration After Teleport (sec)",
                ["Knife_AssassinHeart_ComboAttackCount"] = "Tier 8: Assassin's Heart - Consecutive Attacks After Teleport",
                ["Knife_AssassinHeart_AttackInterval"] = "Tier 8: Assassin's Heart - Attack Interval (sec)",

                // === Speed Tree: Required Points (9개) ===
                ["Tier0_SpeedExpert_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier1_AgilityBase_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier2_WeaponSpec_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier3_Practitioner_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier4_Master_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier5_JumpMaster_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier6_Stats_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier7_Master_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",
                ["Tier8_FinalAcceleration_RequiredPoints"] = "【Required Points】\nPoints required to unlock this node.",

                // === Speed Tree: Tier 0 Speed Expert (1개) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "【Move Speed Bonus (%)】\nPermanent movement speed increase.\nRecommended: 5-10%",

                // === Speed Tree: Tier 1 Agility Base (4개) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "【Post-Dodge Move Speed Bonus (%)】\nMovement speed increases briefly after dodging.\nUseful for quick repositioning after evasion.\nRecommended: 10-20%",
                ["Tier1_AgilityBase_BuffDuration"] = "【Buff Duration (sec)】\nDuration of the post-dodge speed buff.\nRecommended: 2-3 sec",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "【Attack Speed Bonus (%)】\nOverall attack speed increase for all weapons.\nRecommended: 3-8%",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "【Dodge Speed Bonus (%)】\nIncreases the speed of dodge roll animation.\nRecommended: 5-15%",

                // === Speed Tree: Tier 2 Melee Flow (4개) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "【Attack Speed Bonus on 2-Hit (%)】\nAttack speed increases after landing 2 consecutive melee hits.\nRecommended: 8-15%",
                ["Tier2_MeleeFlow_StaminaReduction"] = "【Stamina Reduction (%)】\nStamina cost reduction during the flow buff.\nRecommended: 10-20%",
                ["Tier2_MeleeFlow_Duration"] = "【Buff Duration (sec)】\nDuration of the melee flow buff.\nRecommended: 3-5 sec",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "【Combo Speed Bonus (%)】\nAdditional attack speed bonus for combo chains.\nRecommended: 5-10%",

                // === Speed Tree: Tier 2 Crossbow Expert (3개) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "【Move Speed Bonus on Hit (%)】\nMovement speed increases when crossbow bolt hits enemy.\nRecommended: 10-15%",
                ["Tier2_CrossbowExpert_BuffDuration"] = "【Buff Duration (sec)】\nDuration of the speed buff after successful hit.\nRecommended: 3-5 sec",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "【Reload Speed Bonus During Buff (%)】\nReload speed increases while the hit buff is active.\nRecommended: 10-15%",

                // === Speed Tree: Tier 2 Bow Expert (3개) ===
                ["Tier2_BowExpert_StaminaReduction"] = "【Stamina Reduction on 2-Hit Combo (%)】\nStamina cost reduction after landing 2 consecutive bow shots.\nRecommended: 10-15%",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "【Next Arrow Draw Speed Bonus (%)】\nDraw speed increases for the next arrow after a successful combo.\nRecommended: 10-20%",
                ["Tier2_BowExpert_BuffDuration"] = "【Buff Duration (sec)】\nDuration of the combo buff.\nRecommended: 4-6 sec",

                // === Speed Tree: Tier 2 Mobile Cast (3개) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "【Move Speed Bonus While Casting (%)】\nMovement speed bonus while casting staff spells.\nRecommended: 8-12%",
                ["Tier2_MobileCast_EitrReduction"] = "【Eitr Cost Reduction (%)】\nEitr consumption reduction for staff spells.\nRecommended: 8-15%",
                ["Tier2_MobileCast_CastMoveSpeed"] = "【Move Speed While Staff Casting (%)】\nBase movement speed while channeling staff attacks.\nRecommended: 3-6%",

                // === Speed Tree: Tier 3 Practitioner (4개) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "【Melee Weapon Skill Bonus】\nIncreases all melee weapon skill levels.\nRecommended: 5-10",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "【Crossbow Skill Bonus】\nIncreases crossbow skill level.\nRecommended: 5-10",
                ["Tier3_Practitioner2_StaffSkillBonus"] = "【Staff Skill Bonus】\nIncreases staff skill level (Elementalmagi).\nRecommended: 5-10",
                ["Tier3_Practitioner2_BowSkillBonus"] = "【Bow Skill Bonus】\nIncreases bow skill level.\nRecommended: 5-10",

                // === Speed Tree: Tier 4 Master (2개) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "【Food Consumption Rate Reduction (%)】\nSlows down food consumption rate, making buffs last longer.\nRecommended: 10-20%",
                ["Tier4_Captain_ShipSpeedBonus"] = "【Ship Speed Bonus (%)】\nIncreases sailing speed.\nRecommended: 10-20%",

                // === Speed Tree: Tier 5 Jump Master (2개) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "【Jump Skill Bonus】\nIncreases jump skill level.\nRecommended: 5-15",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "【Jump Stamina Reduction (%)】\nReduces stamina cost for jumping.\nRecommended: 10-20%",

                // === Speed Tree: Tier 6 Stats (4개) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "【Melee Attack Speed Bonus (%)】\nIncreases melee attack speed.\nRecommended: 5-8%",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "【Move Speed Bonus (%)】\nIncreases overall movement speed.\nRecommended: 3-8%",
                ["Tier6_Endurance_StaminaMaxBonus"] = "【Max Stamina Bonus】\nIncreases maximum stamina pool.\nRecommended: 20-40",
                ["Tier6_Intellect_EitrMaxBonus"] = "【Max Eitr Bonus】\nIncreases maximum Eitr pool for magic.\nRecommended: 30-50",

                // === Speed Tree: Tier 7 Master (2개) ===
                ["Tier7_Master_RunSkillBonus"] = "【Run Skill Bonus】\nIncreases running skill level.\nRecommended: 5-15",
                ["Tier7_Master_JumpSkillBonus"] = "【Jump Skill Bonus】\nIncreases jump skill level.\nRecommended: 5-15",

                // === Speed Tree: Tier 8 Melee Acceleration (2개) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "【Melee Attack Speed Bonus (%)】\nFinal boost to melee attack speed.\nRecommended: 5-10%",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "【Next Attack Speed Bonus on 3-Hit Combo (%)】\nMassive attack speed boost for the next attack after a 3-hit combo.\nRecommended: 20-30%",

                // === Speed Tree: Tier 8 Crossbow Acceleration (2개) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "【Reload Speed Bonus (%)】\nFinal boost to crossbow reload speed.\nRecommended: 25-35%",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "【Move Speed During Reload (%)】\nMovement speed while reloading crossbow.\nRecommended: 20-30%",

                // === Speed Tree: Tier 8 Bow Acceleration (2개) ===
                ["Tier8_BowAccel_DrawSpeed"] = "【Draw Speed Bonus (%)】\nFinal boost to bow draw speed.\nRecommended: 15-20%",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "【Move Speed While Drawing (%)】\nMovement speed while drawing bow string.\nRecommended: 10-20%",

                // === Speed Tree: Tier 8 Cast Acceleration (2개) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "【Magic Attack Speed Bonus (%)】\nFinal boost to magic attack speed.\nRecommended: 5-10%",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "【Eitr Max Recovery Rate on 3-Hit Combo (%)】\nBoosts Eitr regeneration rate after a 3-spell combo.\nRecommended: 10-15%",

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

        /// <summary>
        /// Config 키 이름 번역 가져오기 (F1 메뉴 2차 항목 표시명)
        /// BepInEx 제약으로 실제 적용 불가, 참조용으로만 유지
        /// </summary>
        public static Dictionary<string, string> GetKeyNameTranslations(string lang)
        {
            return (lang == "en") ? GetEnglishKeyNames() : GetKoreanKeyNames();
        }

        /// <summary>
        /// 한국어 Config 키 이름 (F1 메뉴 2차 항목) - 참조용
        /// </summary>
        private static Dictionary<string, string> GetKoreanKeyNames()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Attack Tree (공격 트리) - 31개
                // ============================================

                // === Attack Tree: 필요 포인트 (8개) ===
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_WeaponSpec_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_DetailEnhance_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier6_FinalSpec_RequiredPoints"] = "Tier 6: 필요 포인트",

                // === Tier 0: 공격 전문가 (1개) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: 모든 공격력 보너스 (%)",

                // === Tier 2: 무기 전문화 (8개) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2: 근접 무기 보너스 발동 확률 (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2: 근접 무기 추가 데미지",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2: 활 보너스 발동 확률 (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2: 활 추가 데미지",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2: 석궁 보너스 발동 확률 (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2: 석궁 추가 데미지",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2: 지팡이 속성 보너스 발동 확률 (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2: 지팡이 추가 데미지",

                // === Tier 3: 기본 공격 강화 (5개) ===
                ["Tier3_BaseAttack_PhysicalDamageBonus"] = "Tier 3: 물리 데미지 보너스",
                ["Tier3_BaseAttack_ElementalDamageBonus"] = "Tier 3: 속성 데미지 보너스",
                ["Tier3_AttackBoost_StrIntBonus"] = "Tier 3: 힘/지능 스탯 보너스",
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: 양손 무기 물리 데미지 보너스 (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: 양손 무기 속성 데미지 보너스 (%)",

                // === Tier 4: 전투 강화 (3개) ===
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4: 치명타 확률 보너스 (%)",
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4: 2연타 콤보 데미지 보너스 (%)",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4: 원거리 무기 데미지 보너스",

                // === Tier 5: 특수화 스탯 (1개) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: 무기 특화 보너스",

                // === Tier 6: 최종 강화 (4개) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6: 크리티컬 데미지 보너스 (%)",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6: 양손 무기 데미지 보너스 (%)",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6: 지팡이 속성 데미지 보너스 (%)",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6: 3연타 콤보 피니셔 데미지 보너스 (%)",

                // ============================================
                // Speed Tree (속도 트리) - 49개
                // ============================================

                // === Speed Tree: 필요 포인트 (9개) ===
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_WeaponSpec_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_Practitioner_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_Master_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier6_Stats_RequiredPoints"] = "Tier 6: 필요 포인트",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: 필요 포인트",
                ["Tier8_FinalAcceleration_RequiredPoints"] = "Tier 8: 필요 포인트",

                // === Tier 0: 속도 전문가 (1개) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: 이동속도 보너스 (%)",

                // === Tier 1: 민첩함의 기초 (4개) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: 구르기 후 이동속도 보너스 (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: 버프 지속시간 (초)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: 공격속도 보너스 (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: 구르기 속도 보너스 (%)",

                // === Tier 2: 근접 (4개) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2: 2연타 시 공격속도 보너스 (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2: 스태미나 소모 감소 (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2: 버프 지속시간 (초)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2: 콤보 속도 보너스 (%)",

                // === Tier 2: 석궁 (3개) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2: 적 명중 시 이동속도 보너스 (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2: 버프 지속시간 (초)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2: 버프 중 재장전 속도 보너스 (%)",

                // === Tier 2: 활 (3개) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2: 2연타 콤보 시 스태미나 감소 (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2: 다음 화살 시위 당김 속도 보너스 (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2: 버프 지속시간 (초)",

                // === Tier 2: 지팡이 (3개) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2: 시전 중 이동속도 보너스 (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2: Eitr 소모 감소 (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2: 지팡이 시전 중 이동속도 (%)",

                // === Tier 3: 수련자 (4개) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3: 근접 무기 스킬 보너스",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3: 석궁 스킬 보너스",
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3: 지팡이 스킬 보너스",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3: 활 스킬 보너스",

                // === Tier 4: 마스터 (2개) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4: 음식 소모율 감소 (%)",
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4: 배 속도 보너스 (%)",

                // === Tier 5: 점프 숙련자 (2개) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: 점프 스킬 보너스",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: 점프 스태미나 감소 (%)",

                // === Tier 6: 스탯 (4개) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6: 근접 공격속도 보너스 (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6: 이동속도 보너스 (%)",
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6: 최대 스태미나 보너스",
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6: 최대 Eitr 보너스",

                // === Tier 7: 마스터 (2개) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: 달리기 스킬 보너스",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: 점프 스킬 보너스",

                // === Tier 8: 근접 가속 (2개) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8: 근접 공격속도 보너스 (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8: 3연타 시 다음 공격 속도 보너스 (%)",

                // === Tier 8: 석궁 가속 (2개) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8: 재장전 속도 보너스 (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8: 재장전 중 이동속도 (%)",

                // === Tier 8: 활 가속 (2개) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8: 시위 당김 속도 보너스 (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8: 시위 당기는 중 이동속도 (%)",

                // === Tier 8: 시전 가속 (2개) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8: 마법 공격속도 보너스 (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8: 3연타 시 Eitr 최대 회복률 (%)",

                // ============================================
                // Defense Tree (방어 트리) - 39개
                // ============================================

                // === Defense Tree: 필요 포인트 (7개) ===
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_MindBodyHealth_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_CoreDodgeBoostShield_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_ShockwaveStompRock_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_EndureAgileRegenParry_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier6_FinalSkills_RequiredPoints"] = "Tier 6: 필요 포인트",

                // === Tier 0: 방어 전문가 (2개) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: 체력 보너스",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: 방어력 보너스",

                // === Tier 1: 피부경화 (2개) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: 체력 보너스",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: 방어력 보너스",

                // === Tier 2: 심신단련 & 체력단련 (4개) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2: 최대 스태미나 보너스",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2: 최대 Eitr 보너스",
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2: 체력 보너스",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2: 방어력 보너스",

                // === Tier 3: 다양한 방어 기술 (5개) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3: Eitr 보너스",
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3: 회피율 보너스 (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3: 구르기 무적시간 증가 (%)",
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3: 체력 보너스",
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3: 방패 방어력 보너스",

                // === Tier 4: 충격파 발산 (5개) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4: 효과 반경 (미터)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4: 넉백 강도",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4: 쿨타임 (초)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4: 자동 발동 체력 임계값",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4: VFX 지속시간 (초)",
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4: 방어력 증폭 (%)",

                // === Tier 5: 인내 & 민첩 & 회복 & 방어 숙련 (8개) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5: 달리기 스태미나 감소 (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5: 점프 스태미나 감소 (%)",
                ["Tier5_Agility_DodgeBonus"] = "Tier 5: 회피율 보너스 (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5: 구르기 스태미나 감소 (%)",
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5: 체력 재생 보너스 (초당)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5: 재생 간격 (초)",
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5: 방패 방어력 보너스",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5: 패링 지속시간 보너스 (초)",

                // === Tier 6: 최종 방어 기술 (6개) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6: 회피율 영구 보너스 (%)",
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6: 체력 보너스 (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6: 방어력 보너스 (%)",
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6: 방어 스태미나 감소 (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6: 일반 방패 이동속도 보너스 (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6: 타워 실드 이동속도 보너스 (%)",

                // ============================================
                // Production Tree (생산 트리) - 22개
                // ============================================

                // === Production Tree: 필요 포인트 (5개) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_Specialization_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_IntermediateSkill_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_AdvancedSkill_RequiredPoints"] = "Tier 4: 필요 포인트",

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

                // ============================================
                // Bow Tree (활 트리) - 31개
                // ============================================

                // === Bow Tree: 필요 포인트 (7개) ===
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_MultishotLv1_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_BowMastery_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_MultishotLv2_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_PrecisionAim_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier6_ExplosiveArrow_RequiredPoints"] = "Tier 6: 필요 포인트",

                // === Tier 0: 활 전문가 (1개) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: 활 공격력 보너스 (%)",

                // === Tier 1: 집중 사격 (1개) ===
                ["Tier1_FocusedShot_CritBonus"] = "Tier 1: 치명타 확률 보너스 (%)",

                // === Tier 2: 멀티샷 Lv1 (5개) ===
                ["Tier2_MultishotLv1_ActivationChance"] = "Tier 2: 멀티샷 Lv1 발동 확률 (%)",
                ["Tier2_Multishot_AdditionalArrows"] = "Tier 2: 멀티샷 추가 화살 수",
                ["Tier2_Multishot_ArrowConsumption"] = "Tier 2: 멀티샷 화살 소모량",
                ["Tier2_Multishot_DamagePerArrow"] = "Tier 2: 멀티샷 화살당 데미지 (%)",

                // === Tier 3: 활 숙련 (3개) ===
                ["Tier3_SpeedShot_SkillBonus"] = "Tier 3: 활 기술 보너스",
                ["Tier3_SilentShot_DamageBonus"] = "Tier 3: 관통 공격력 증가",
                ["Tier3_SpecialArrow_Chance"] = "Tier 3: 특수 화살 발사 확률 (%)",

                // === Tier 4: 멀티샷 Lv2 & 파워샷 (3개) ===
                ["Tier4_MultishotLv2_ActivationChance"] = "Tier 4: 멀티샷 Lv2 발동 확률 (%)",
                ["Tier4_PowerShot_KnockbackChance"] = "Tier 4: 강한 넉백 확률 (%)",
                ["Tier4_PowerShot_KnockbackDistance"] = "Tier 4: 넉백 거리 (m)",

                // === Tier 5: 정조준 & 고급 스킬 (6개) ===
                ["Tier5_PrecisionAim_CritDamage"] = "Tier 5: 크리티컬 데미지 보너스 (%)",
                ["Tier5_ArrowRain_Chance"] = "Tier 5: 화살비 발사 확률 (%)",
                ["Tier5_ArrowRain_ArrowCount"] = "Tier 5: 화살비 화살 개수",
                ["Tier5_BackstepShot_CritBonus"] = "Tier 5: 구르기 후 치명타 확률 (%)",
                ["Tier5_BackstepShot_Duration"] = "Tier 5: 구르기 후 효과 지속시간 (초)",
                ["Tier5_HuntingInstinct_CritBonus"] = "Tier 5: 사냥 본능 치명타 확률 (%)",

                // === Tier 6: 폭발 화살 & 크리티컬 부스트 (9개) ===
                ["Tier6_ExplosiveArrow_DamageMultiplier"] = "Tier 6: 폭발 화살 데미지 배율 (%)",
                ["Tier6_ExplosiveArrow_Radius"] = "Tier 6: 폭발 화살 범위 (m)",
                ["Tier6_ExplosiveArrow_Cooldown"] = "Tier 6: 폭발 화살 쿨타임 (초)",
                ["Tier6_ExplosiveArrow_StaminaCost"] = "Tier 6: 폭발 화살 스태미나 소모 (%)",
                ["Tier6_CritBoost_DamageBonus"] = "Tier 6: 크리티컬 부스트 데미지 (%)",
                ["Tier6_CritBoost_CritChance"] = "Tier 6: 크리티컬 부스트 확률 (%)",
                ["Tier6_CritBoost_ArrowCount"] = "Tier 6: 크리티컬 부스트 화살 개수",
                ["Tier6_CritBoost_Cooldown"] = "Tier 6: 크리티컬 부스트 쿨타임 (초)",
                ["Tier6_CritBoost_StaminaCost"] = "Tier 6: 크리티컬 부스트 스태미나 소모 (%)",

                // ============================================
                // Sword Tree (검 트리) - 30개
                // ============================================

                // === Sword Tree: 필요 포인트 (11개) ===
                ["Sword_Expert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Sword_FastSlash_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Sword_CounterStance_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Sword_ComboSlash_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Sword_BladeReflect_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Sword_OffenseDefense_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Sword_TrueDuel_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Sword_ParryCharge_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Sword_RushSlash_RequiredPoints"] = "Tier 6: 필요 포인트",

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

                // === Spear Tree: 필요 포인트 (7개) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_SpearStep1_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_SpearStep2_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_SpearStep3_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_SpearStep4_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier5_Combo_RequiredPoints"] = "Tier 5: 필요 포인트",

                // === Spear Tree: 창 전문가 (3개) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: 2연속 공격 속도 보너스 (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: 2연속 공격력 보너스 (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: 효과 지속시간 (초)",

                // === Spear Tree: 투창 전문가 (3개) ===
                ["Tier1_Throw_Cooldown"] = "Tier 1: 투창 쿨타임 (초)",
                ["Tier1_Throw_DamageMultiplier"] = "Tier 1: 투창 데미지 배율 (%)",
                ["Tier1_Throw_BuffDuration_NotUsed"] = "Tier 1: 사용 안 함",

                // === Spear Tree: 급소 찌르기 (1개) ===
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: 창 공격력 보너스 (%)",

                // === Spear Tree: 연격창 (1개) ===
                ["Tier3_Rapid_DamageBonus"] = "Tier 3: 무기 공격력 보너스",

                // === Spear Tree: 회피 찌르기 (2개) ===
                ["Tier4_Evasion_DamageBonus"] = "Tier 4: 구르기 후 공격 피해 보너스 (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4: 공격 스태미나 감소 (%)",

                // === Spear Tree: 폭발창 (3개) ===
                ["Tier3_Explosion_Chance"] = "Tier 3: 폭발 발동 확률 (%)",
                ["Tier3_Explosion_Radius"] = "Tier 3: 폭발 범위 (m)",
                ["Tier3_Explosion_DamageBonus"] = "Tier 3: 폭발 공격력 보너스 (%)",

                // === Spear Tree: 이연창 (2개) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4: 2연속 공격 공격력 보너스 (%)",
                ["Tier4_Dual_Duration"] = "Tier 4: 버프 지속시간 (초)",

                // === Spear Tree: 꿰뚫는 창 (6개) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] = "Tier 5: 미사용",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5: 버프 지속시간 (초)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5: 번개 충격 데미지 배율 (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5: 번개 발동 연속 적중 횟수",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5: G키 액티브 쿨타임 (초)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5: G키 액티브 스태미나 소모 (%)",

                // === Spear Tree: 연공창 (7개) ===
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5: H키 액티브 쿨타임 (초)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5: H키 액티브 데미지 배율 (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5: H키 액티브 스태미나 소모 (%)",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5: H키 액티브 넉백 범위 (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5: 액티브 효과 범위 (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5: H키 버프 지속시간 (초)",
                ["Tier5_Combo_MaxUses"] = "Tier 5: 버프 중 최대 강화 투창 횟수",

                // ============================================
                // Mace Tree (둔기 트리) - 27개
                // ============================================

                // ============================================
                // Staff Tree (지팡이 트리) - 30개
                // ============================================

                // === Staff Tree: 필요 포인트 (10개) ===
                ["Tier0_StaffExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_MindFocus_RequiredPoints"] = "Tier 1: 정신 집중 - 필요 포인트",
                ["Tier1_MagicFlow_RequiredPoints"] = "Tier 1: 마력 흐름 - 필요 포인트",
                ["Tier2_MagicAmplify_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_FrostElement_RequiredPoints"] = "Tier 3: 냉기 강화 - 필요 포인트",
                ["Tier3_FireElement_RequiredPoints"] = "Tier 3: 화염 강화 - 필요 포인트",
                ["Tier3_LightningElement_RequiredPoints"] = "Tier 3: 번개 강화 - 필요 포인트",
                ["Tier4_LuckyMana_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_DoubleCast_RequiredPoints"] = "Tier 5: 이중시전 - 필요 포인트",
                ["Tier5_InstantAreaHeal_RequiredPoints"] = "Tier 5: 범위 힐 - 필요 포인트",

                // === Tier 0: 지팡이 전문가 (1개) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] = "Tier 0: 속성 데미지 보너스 (%)",

                // === Tier 1: 정신 집중 & 마력 흐름 (2개) ===
                ["Tier1_MindFocus_EitrReduction"] = "Tier 1: Eitr 소모 감소 (%)",
                ["Tier1_MagicFlow_EitrBonus"] = "Tier 1: 최대 Eitr 보너스",

                // === Tier 2: 마법 증폭 (3개) ===
                ["Tier2_MagicAmplify_Chance"] = "Tier 2: 마법 증폭 발동 확률 (%)",
                ["Tier2_MagicAmplify_DamageBonus"] = "Tier 2: 마법 증폭 속성 공격 보너스 (%)",
                ["Tier2_MagicAmplify_EitrCostIncrease"] = "Tier 2: Eitr 소모 증가 (%)",

                // === Tier 3: 속성 강화 (3개) ===
                ["Tier3_FrostElement_DamageBonus"] = "Tier 3: 얼음 공격력 보너스",
                ["Tier3_FireElement_DamageBonus"] = "Tier 3: 불 공격력 보너스",
                ["Tier3_LightningElement_DamageBonus"] = "Tier 3: 번개 공격력 보너스",

                // === Tier 4: 행운의 마력 (1개) ===
                ["Tier4_LuckyMana_Chance"] = "Tier 4: Eitr 무소모 발동 확률 (%)",

                // === Tier 5-1: 이중시전 - R키 액티브 (5개) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] = "Tier 5: 추가 투사체 개수",
                ["Tier5_DoubleCast_ProjectileDamagePercent"] = "Tier 5: 투사체 데미지 비율 (%)",
                ["Tier5_DoubleCast_AngleOffset"] = "Tier 5: 투사체 각도 오프셋 (도)",
                ["Tier5_DoubleCast_EitrCost"] = "Tier 5: Eitr 소모량",
                ["Tier5_DoubleCast_Cooldown"] = "Tier 5: 쿨타임 (초)",

                // === Tier 5-2: 즉시 범위 힐 - H키 액티브 (5개) ===
                ["Tier5_InstantAreaHeal_Cooldown"] = "Tier 5: 쿨타임 (초)",
                ["Tier5_InstantAreaHeal_EitrCost"] = "Tier 5: Eitr 소모량",
                ["Tier5_InstantAreaHeal_HealPercent"] = "Tier 5: 회복량 (최대 HP 대비 %)",
                ["Tier5_InstantAreaHeal_Range"] = "Tier 5: 치유 범위 (미터)",
                ["Tier5_InstantAreaHeal_SelfHeal"] = "Tier 5: 자가 치유 허용",

                // ============================================
                // Crossbow Tree (석궁 트리) - 28개
                // ============================================

                // === Crossbow Tree: 필요 포인트 (6개) ===
                ["Tier0_CrossbowExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_RapidFire_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_CrossbowSkills_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_AutoReload_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_CrossbowSkills_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_OneShot_RequiredPoints"] = "Tier 5: 필요 포인트",

                // === Tier 0: 석궁 전문가 (1개) ===
                ["Tier0_CrossbowExpert_DamageBonus"] = "Tier 0: 석궁 공격력 보너스 (%)",

                // === Tier 1: 연발 (5개) ===
                ["Tier1_RapidFire_Chance"] = "Tier 1: 연발 발동 확률 (%)",
                ["Tier1_RapidFire_ShotCount"] = "Tier 1: 연발 발사 횟수",
                ["Tier1_RapidFire_DamagePercent"] = "Tier 1: 연발 데미지 비율 (%)",
                ["Tier1_RapidFire_Delay"] = "Tier 1: 연발 간격 (초)",
                ["Tier1_RapidFire_BoltConsumption"] = "Tier 1: 연발 볼트 소모량",

                // === Tier 2: 균형 조준 (2개) ===
                ["Tier2_BalancedAim_KnockbackChance"] = "Tier 2: 넉백 발동 확률 (%)",
                ["Tier2_BalancedAim_KnockbackDistance"] = "Tier 2: 넉백 거리 (미터)",

                // === Tier 2: 고속 장전 (1개) ===
                ["Tier2_RapidReload_SpeedIncrease"] = "Tier 2: 장전 속도 증가 (%)",

                // === Tier 2: 정직한 한 발 (1개) ===
                ["Tier2_HonestShot_DamageBonus"] = "Tier 2: 기본 데미지 보너스 (%)",

                // === Tier 3: 자동 장전 (1개) ===
                ["Tier3_AutoReload_Chance"] = "Tier 3: 자동 장전 발동 확률 (%)",

                // === Tier 4: 연발 Lv2 (5개) ===
                ["Tier4_RapidFireLv2_Chance"] = "Tier 4: 연발 Lv2 발동 확률 (%)",
                ["Tier4_RapidFireLv2_ShotCount"] = "Tier 4: 연발 Lv2 발사 횟수",
                ["Tier4_RapidFireLv2_DamagePercent"] = "Tier 4: 연발 Lv2 데미지 비율 (%)",
                ["Tier4_RapidFireLv2_Delay"] = "Tier 4: 연발 Lv2 간격 (초)",
                ["Tier4_RapidFireLv2_BoltConsumption"] = "Tier 4: 연발 Lv2 볼트 소모량",

                // === Tier 4: 결전의 일격 (2개) ===
                ["Tier4_FinalStrike_HpThreshold"] = "Tier 4: 적 체력 임계값 (%)",
                ["Tier4_FinalStrike_DamageBonus"] = "Tier 4: 추가 데미지 보너스 (%)",

                // === Tier 5: 단 한 발 - R키 액티브 (4개) ===
                ["Tier5_OneShot_Duration"] = "Tier 5: 버프 지속시간 (초)",
                ["Tier5_OneShot_DamageBonus"] = "Tier 5: 원샷 데미지 보너스 (%)",
                ["Tier5_OneShot_KnockbackDistance"] = "Tier 5: 넉백 거리 (미터)",
                ["Tier5_OneShot_Cooldown"] = "Tier 5: 쿨타임 (초)",

                // ============================================
                // Knife Tree (단검 트리) - 32개
                // ============================================

                // === Tier 0: 단검 전문가 ===
                ["Tier0_KnifeExpert_BackstabBonus"] = "Tier 0: 백스탭 보너스 (%)",
                ["Tier0_KnifeExpert_RequiredPoints"] = "Tier 0: 필요 포인트",

                // === Tier 1: 회피 숙련 ===
                ["Tier1_Evasion_Chance"] = "Tier 1: 회피 숙련 발동 확률 (%)",
                ["Tier1_Evasion_Duration"] = "Tier 1: 회피 지속시간 (초)",
                ["Tier1_Evasion_RequiredPoints"] = "Tier 1: 필요 포인트",

                // === Tier 2: 빠른 움직임 ===
                ["Tier2_FastMove_MoveSpeedBonus"] = "Tier 2: 이동속도 보너스 (%)",
                ["Tier2_FastMove_RequiredPoints"] = "Tier 2: 필요 포인트",

                // === Tier 3: 전투 숙련 ===
                ["Tier3_CombatMastery_DamageBonus"] = "Tier 3: 공격력 보너스",
                ["Tier3_CombatMastery_BuffDuration"] = "Tier 3: 버프 지속시간 (초)",
                ["Tier3_CombatMastery_RequiredPoints"] = "Tier 3: 필요 포인트",

                // === Tier 4: 공격과 회피 ===
                ["Tier4_AttackEvasion_EvasionBonus"] = "Tier 4: 회피율 보너스 (%)",
                ["Tier4_AttackEvasion_BuffDuration"] = "Tier 4: 버프 지속시간 (초)",
                ["Tier4_AttackEvasion_Cooldown"] = "Tier 4: 쿨타임 (초)",
                ["Tier4_AttackEvasion_RequiredPoints"] = "Tier 4: 필요 포인트",

                // === Tier 5: 치명적 피해 ===
                ["Tier5_CriticalDamage_DamageBonus"] = "Tier 5: 치명타 데미지 보너스 (%)",
                ["Tier5_CriticalDamage_RequiredPoints"] = "Tier 5: 필요 포인트",

                // === Tier 6: 암살자 ===
                ["Tier6_Assassin_CritDamageBonus"] = "Tier 6: 치명타 데미지 보너스 (%)",
                ["Tier6_Assassin_CritChanceBonus"] = "Tier 6: 치명타 확률 보너스 (%)",
                ["Tier6_Assassin_RequiredPoints"] = "Tier 6: 필요 포인트",

                // === Tier 7: 암살술 ===
                ["Tier7_Assassination_StaggerChance"] = "Tier 7: 스태거 발동 확률 (%)",
                ["Tier7_Assassination_RequiredComboHits"] = "Tier 7: 필요 연속 적중 횟수",
                ["Tier7_Assassination_RequiredPoints"] = "Tier 7: 필요 포인트",

                // === Tier 8: 암살자의 심장 - G키 액티브 ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] = "Tier 8: 치명타 데미지 배율",
                ["Tier8_AssassinHeart_Duration"] = "Tier 8: 버프 지속시간 (초)",
                ["Tier8_AssassinHeart_StaminaCost"] = "Tier 8: 스태미나 소모",
                ["Tier8_AssassinHeart_Cooldown"] = "Tier 8: 쿨타임 (초)",
                ["Tier8_AssassinHeart_TeleportRange"] = "Tier 8: 순간이동 범위 (m)",
                ["Tier8_AssassinHeart_TeleportBackDistance"] = "Tier 8: 등 뒤 이동 거리 (m)",
                ["Tier8_AssassinHeart_StunDuration"] = "Tier 8: 기절 지속시간 (초)",
                ["Tier8_AssassinHeart_ComboAttackCount"] = "Tier 8: 연속 공격 횟수",
                ["Tier8_AssassinHeart_AttackInterval"] = "Tier 8: 공격 간격 (초)",
                ["Tier8_AssassinHeart_RequiredPoints"] = "Tier 8: 필요 포인트",

                // ============================================
                // Sword Tree (검 트리) - 33개 (신규 키 형식)
                // ============================================

                // === Tier 0: 검 전문가 (2개) ===
                ["Tier0_SwordExpert_DamageBonus"] = "Tier 0: 검 공격력 보너스 (%)",
                ["Tier0_SwordExpert_RequiredPoints"] = "Tier 0: 필요 포인트",

                // === Tier 1: 빠른 베기 & 반격 자세 (5개) ===
                ["Tier1_FastSlash_AttackSpeedBonus"] = "Tier 1: 빠른 베기 - 공격속도 보너스 (%)",
                ["Tier1_FastSlash_RequiredPoints"] = "Tier 1: 빠른 베기 - 필요 포인트",
                ["Tier1_CounterStance_Duration"] = "Tier 1: 반격 자세 - 버프 지속시간 (초)",
                ["Tier1_CounterStance_DefenseBonus"] = "Tier 1: 반격 자세 - 방어력 보너스 (%)",
                ["Tier1_CounterStance_RequiredPoints"] = "Tier 1: 반격 자세 - 필요 포인트",

                // === Tier 2: 연속 베기 (3개) ===
                ["Tier2_ComboSlash_DamageBonus"] = "Tier 2: 연속 공격 보너스 (%)",
                ["Tier2_ComboSlash_BuffDuration"] = "Tier 2: 버프 지속시간 (초)",
                ["Tier2_ComboSlash_RequiredPoints"] = "Tier 2: 필요 포인트",

                // === Tier 3: 칼날 되치기 (2개) ===
                ["Tier3_Riposte_DamageBonus"] = "Tier 3: 공격력 보너스",
                ["Tier3_Riposte_RequiredPoints"] = "Tier 3: 필요 포인트",

                // === Tier 4: 공방일체 & 진검승부 (5개) ===
                ["Tier4_AllInOne_AttackBonus"] = "Tier 4: 공방일체 - 공격력 보너스 (%)",
                ["Tier4_AllInOne_DefenseBonus"] = "Tier 4: 공방일체 - 방어력 보너스",
                ["Tier4_AllInOne_RequiredPoints"] = "Tier 4: 공방일체 - 필요 포인트",
                ["Tier4_TrueDuel_AttackSpeedBonus"] = "Tier 4: 진검승부 - 공격속도 보너스 (%)",
                ["Tier4_TrueDuel_RequiredPoints"] = "Tier 4: 진검승부 - 필요 포인트",

                // === Tier 5: 패링 돌격 - G키 액티브 (6개) ===
                ["Tier5_ParryRush_BuffDuration"] = "Tier 5: 버프 지속시간 (초)",
                ["Tier5_ParryRush_DamageBonus"] = "Tier 5: 공격력 보너스 (%)",
                ["Tier5_ParryRush_PushDistance"] = "Tier 5: 밀어내기 거리 (m)",
                ["Tier5_ParryRush_StaminaCost"] = "Tier 5: 스태미나 소모",
                ["Tier5_ParryRush_Cooldown"] = "Tier 5: 쿨타임 (초)",
                ["Tier5_ParryRush_RequiredPoints"] = "Tier 5: 필요 포인트",

                // === Tier 6: 돌진 연속 베기 - G키 액티브 (10개) ===
                ["Tier6_RushSlash_Hit1DamageRatio"] = "Tier 6: 1차 공격력 비율 (%)",
                ["Tier6_RushSlash_Hit2DamageRatio"] = "Tier 6: 2차 공격력 비율 (%)",
                ["Tier6_RushSlash_Hit3DamageRatio"] = "Tier 6: 3차 공격력 비율 (%)",
                ["Tier6_RushSlash_InitialDistance"] = "Tier 6: 초기 돌진 거리 (m)",
                ["Tier6_RushSlash_SideDistance"] = "Tier 6: 측면 이동 거리 (m)",
                ["Tier6_RushSlash_StaminaCost"] = "Tier 6: 스태미나 소모량",
                ["Tier6_RushSlash_Cooldown"] = "Tier 6: 쿨타임 (초)",
                ["Tier6_RushSlash_MoveSpeed"] = "Tier 6: 이동 속도 (m/s)",
                ["Tier6_RushSlash_AttackSpeedBonus"] = "Tier 6: 공격속도 보너스 (%)",
                ["Tier6_RushSlash_RequiredPoints"] = "Tier 6: 필요 포인트",

                // ============================================
                // Mace Tree (둔기 트리) - 34개
                // ============================================

                // === Mace Tree: 필요 포인트 (11개) ===
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_MaceExpert_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_Guard_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier3_HeavyStrike_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_Push_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_Tank_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier5_DPS_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: 필요 포인트",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7: 필요 포인트",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7: 필요 포인트",

                // === Tier 0: 둔기 전문가 (3개) ===
                ["Tier0_MaceExpert_DamageBonus"] = "Tier 0: 둔기 공격력 보너스 (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: 기절 확률 (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: 기절 지속시간 (초)",

                // === Tier 1: 둔기 공격력 강화 (1개) ===
                ["Tier1_MaceExpert_DamageBonus"] = "Tier 1: 둔기 공격력 보너스 (%)",

                // === Tier 2: 기절 강화 (2개) ===
                ["Tier2_StunBoost_StunChanceBonus"] = "Tier 2: 기절 확률 보너스 (%)",
                ["Tier2_StunBoost_StunDurationBonus"] = "Tier 2: 기절 지속시간 보너스 (초)",

                // === Tier 3: 방어 강화 (1개) ===
                ["Tier3_Guard_ArmorBonus"] = "Tier 3: 방어력 보너스",

                // === Tier 3: 무거운 타격 (1개) ===
                ["Tier3_HeavyStrike_DamageBonus"] = "Tier 3: 공격력 보너스",

                // === Tier 4: 밀어내기 (1개) ===
                ["Tier4_Push_KnockbackChance"] = "Tier 4: 넉백 확률 (%)",

                // === Tier 5: 탱커 (2개) ===
                ["Tier5_Tank_HealthBonus"] = "Tier 5: 체력 보너스 (%)",
                ["Tier5_Tank_DamageReduction"] = "Tier 5: 받는 데미지 감소 (%)",

                // === Tier 5: 공격력 강화 DPS (2개) ===
                ["Tier5_DPS_DamageBonus"] = "Tier 5: 공격력 보너스 (%)",
                ["Tier5_DPS_AttackSpeedBonus"] = "Tier 5: 공격속도 보너스 (%)",

                // === Tier 6: 그랜드마스터 (1개) ===
                ["Tier6_Grandmaster_ArmorBonus"] = "Tier 6: 방어력 보너스 (%)",

                // === Tier 7: 분노의 망치 - G키 액티브 (5개) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] = "Tier 7: 1-4타 데미지 배율 (%)",
                ["Tier7_FuryHammer_FinalHitMultiplier"] = "Tier 7: 5타 최종타 데미지 배율 (%)",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7: 스태미나 소모",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7: 쿨타임 (초)",
                ["Tier7_FuryHammer_AoeRadius"] = "Tier 7: AOE 범위 (m)",

                // === Tier 7: 수호자의 진심 - H키 액티브 (4개) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7: 쿨타임 (초)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7: 스태미나 소모",
                ["Tier7_GuardianHeart_Duration"] = "Tier 7: 버프 지속시간 (초)",
                ["Tier7_GuardianHeart_ReflectPercent"] = "Tier 7: 반사 데미지 비율 (%)",

                // ========================================
                // Polearm Tree (25개 키)
                // ========================================

                // === 필요 포인트 (7개) ===
                ["Tier0_PolearmExpert_RequiredPoints"] = "Tier 0: 필요 포인트",
                ["Tier1_PolearmSkill_RequiredPoints"] = "Tier 1: 필요 포인트",
                ["Tier2_PolearmSkill_RequiredPoints"] = "Tier 2: 필요 포인트",
                ["Tier3_PolearmSkill_RequiredPoints"] = "Tier 3: 필요 포인트",
                ["Tier4_PolearmSkill_RequiredPoints"] = "Tier 4: 필요 포인트",
                ["Tier5_Suppress_RequiredPoints"] = "Tier 5: 필요 포인트",
                ["Tier6_PierceCharge_RequiredPoints"] = "Tier 6: 필요 포인트",

                // === Tier 0 - 폴암 전문가 (1개) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] = "Tier 0: 공격 범위 보너스 (%)",

                // === Tier 1 - 회전베기 (1개) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] = "Tier 1: 회전 공격 피해 보너스 (%)",

                // === Tier 2 - 영웅 타격 (1개) ===
                ["Tier2_HeroStrike_KnockbackChance"] = "Tier 2: 넉백 확률 (%)",

                // === Tier 3 (4개) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] = "Tier 3: 2연타 피해 보너스 (%)",
                ["Tier3_AreaCombo_DoubleHitDuration"] = "Tier 3: 2연타 버프 지속시간 (초)",
                ["Tier3_GroundWheel_WheelAttackDamageBonus"] = "Tier 3: 회전 공격 피해 보너스 (%)",
                ["Tier3_PolearmBoost_WeaponDamageBonus"] = "Tier 3: 무기 공격력 보너스 (고정값)",

                // === Tier 4 - 반달 베기 (2개) ===
                ["Tier4_MoonSlash_AttackRangeBonus"] = "Tier 4: 공격 범위 보너스 (%)",
                ["Tier4_MoonSlash_StaminaReduction"] = "Tier 4: 스태미나 소모 감소 (%)",

                // === Tier 5 - 제압 공격 (1개) ===
                ["Tier5_Suppress_DamageBonus"] = "Tier 5: 제압 공격 피해 보너스 (%)",

                // === Tier 6 - 관통 돌격 (8개) ===
                ["Tier6_PierceCharge_DashDistance"] = "Tier 6: 돌진 거리 (m)",
                ["Tier6_PierceCharge_FirstHitDamageBonus"] = "Tier 6: 첫 타격 피해 보너스 (%)",
                ["Tier6_PierceCharge_AoeDamageBonus"] = "Tier 6: 광역 넉백 피해 보너스 (%)",
                ["Tier6_PierceCharge_AoeAngle"] = "Tier 6: 광역 각도 (도)",
                ["Tier6_PierceCharge_AoeRadius"] = "Tier 6: 광역 반경 (m)",
                ["Tier6_PierceCharge_KnockbackDistance"] = "Tier 6: 넉백 거리 (m)",
                ["Tier6_PierceCharge_StaminaCost"] = "Tier 6: 스태미나 소모",
                ["Tier6_PierceCharge_Cooldown"] = "Tier 6: 쿨타임 (초)",

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

            };
        }

        /// <summary>
        /// 영어 Config 키 이름 (F1 메뉴 2차 항목) - 참조용
        /// </summary>
        private static Dictionary<string, string> GetEnglishKeyNames()
        {
            return new Dictionary<string, string>
            {
                // ============================================
                // Attack Tree - 31 Keys
                // ============================================

                // === Attack Tree: Required Points (8) ===
                ["Tier0_AttackExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_BaseAttack_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_WeaponSpec_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_AttackBoost_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_DetailEnhance_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier4_RangedEnhance_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_SpecialStat_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier6_FinalSpec_RequiredPoints"] = "Tier 6: Required Points",

                // === Tier 0: Attack Expert (1) ===
                ["Tier0_AttackExpert_AllDamageBonus"] = "Tier 0: All Damage Bonus (%)",

                // === Tier 2: Weapon Specialization (8) ===
                ["Tier2_MeleeSpec_BonusTriggerChance"] = "Tier 2: Melee Bonus Trigger Chance (%)",
                ["Tier2_MeleeSpec_MeleeDamage"] = "Tier 2: Melee Additional Damage",
                ["Tier2_BowSpec_BonusTriggerChance"] = "Tier 2: Bow Bonus Trigger Chance (%)",
                ["Tier2_BowSpec_BowDamage"] = "Tier 2: Bow Additional Damage",
                ["Tier2_CrossbowSpec_EnhanceTriggerChance"] = "Tier 2: Crossbow Bonus Trigger Chance (%)",
                ["Tier2_CrossbowSpec_CrossbowDamage"] = "Tier 2: Crossbow Additional Damage",
                ["Tier2_StaffSpec_ElementalTriggerChance"] = "Tier 2: Staff Elemental Bonus Trigger Chance (%)",
                ["Tier2_StaffSpec_StaffDamage"] = "Tier 2: Staff Additional Damage",

                // === Tier 3: Attack Boost (5) ===
                ["Tier3_BaseAttack_PhysicalDamageBonus"] = "Tier 3: Physical Damage Bonus",
                ["Tier3_BaseAttack_ElementalDamageBonus"] = "Tier 3: Elemental Damage Bonus",
                ["Tier3_AttackBoost_StrIntBonus"] = "Tier 3: Strength/Intelligence Bonus",
                ["Tier3_AttackBoost_PhysicalDamageBonus"] = "Tier 3: Two-Hand Physical Damage Bonus (%)",
                ["Tier3_AttackBoost_ElementalDamageBonus"] = "Tier 3: Two-Hand Elemental Damage Bonus (%)",

                // === Tier 4: Combat Enhancement (3) ===
                ["Tier4_PrecisionAttack_CritChance"] = "Tier 4: Critical Chance Bonus (%)",
                ["Tier4_MeleeEnhance_2HitComboBonus"] = "Tier 4: 2-Hit Combo Damage Bonus (%)",
                ["Tier4_RangedEnhance_RangedDamageBonus"] = "Tier 4: Ranged Damage Bonus",

                // === Tier 5: Specialized Stats (1) ===
                ["Tier5_SpecialStat_SpecBonus"] = "Tier 5: Weapon Specialization Bonus",

                // === Tier 6: Final Enhancement (4) ===
                ["Tier6_WeakPointAttack_CritDamageBonus"] = "Tier 6: Critical Damage Bonus (%)",
                ["Tier6_TwoHandCrush_TwoHandDamageBonus"] = "Tier 6: Two-Hand Damage Bonus (%)",
                ["Tier6_ElementalAttack_ElementalBonus"] = "Tier 6: Staff Elemental Damage Bonus (%)",
                ["Tier6_ComboFinisher_3HitComboBonus"] = "Tier 6: 3-Hit Combo Finisher Bonus (%)",

                // ============================================
                // Speed Tree - 49 Keys
                // ============================================

                // === Speed Tree: Required Points (9개) ===
                ["Tier0_SpeedExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_AgilityBase_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_WeaponSpec_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_Practitioner_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_Master_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_JumpMaster_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier6_Stats_RequiredPoints"] = "Tier 6: Required Points",
                ["Tier7_Master_RequiredPoints"] = "Tier 7: Required Points",
                ["Tier8_FinalAcceleration_RequiredPoints"] = "Tier 8: Required Points",

                // === Tier 0: Speed Expert (1개) ===
                ["Tier0_SpeedExpert_MoveSpeedBonus"] = "Tier 0: Move Speed Bonus (%)",

                // === Tier 1: Agility Base (4개) ===
                ["Tier1_AgilityBase_DodgeMoveSpeedBonus"] = "Tier 1: Post-Dodge Move Speed Bonus (%)",
                ["Tier1_AgilityBase_BuffDuration"] = "Tier 1: Buff Duration (sec)",
                ["Tier1_AgilityBase_AttackSpeedBonus"] = "Tier 1: Attack Speed Bonus (%)",
                ["Tier1_AgilityBase_DodgeSpeedBonus"] = "Tier 1: Dodge Speed Bonus (%)",

                // === Tier 2: Melee Flow (4개) ===
                ["Tier2_MeleeFlow_AttackSpeedBonus"] = "Tier 2: Attack Speed Bonus on 2-Hit (%)",
                ["Tier2_MeleeFlow_StaminaReduction"] = "Tier 2: Stamina Reduction (%)",
                ["Tier2_MeleeFlow_Duration"] = "Tier 2: Buff Duration (sec)",
                ["Tier2_MeleeFlow_ComboSpeedBonus"] = "Tier 2: Combo Speed Bonus (%)",

                // === Tier 2: Crossbow Expert (3개) ===
                ["Tier2_CrossbowExpert_MoveSpeedBonus"] = "Tier 2: Move Speed Bonus on Hit (%)",
                ["Tier2_CrossbowExpert_BuffDuration"] = "Tier 2: Buff Duration (sec)",
                ["Tier2_CrossbowExpert_ReloadSpeedBonus"] = "Tier 2: Reload Speed Bonus During Buff (%)",

                // === Tier 2: Bow Expert (3개) ===
                ["Tier2_BowExpert_StaminaReduction"] = "Tier 2: Stamina Reduction on 2-Hit Combo (%)",
                ["Tier2_BowExpert_NextDrawSpeedBonus"] = "Tier 2: Next Arrow Draw Speed Bonus (%)",
                ["Tier2_BowExpert_BuffDuration"] = "Tier 2: Buff Duration (sec)",

                // === Tier 2: Mobile Cast (3개) ===
                ["Tier2_MobileCast_MoveSpeedBonus"] = "Tier 2: Move Speed Bonus While Casting (%)",
                ["Tier2_MobileCast_EitrReduction"] = "Tier 2: Eitr Cost Reduction (%)",
                ["Tier2_MobileCast_CastMoveSpeed"] = "Tier 2: Move Speed While Staff Casting (%)",

                // === Tier 3: Practitioner (4개) ===
                ["Tier3_Practitioner1_MeleeSkillBonus"] = "Tier 3: Melee Weapon Skill Bonus",
                ["Tier3_Practitioner1_CrossbowSkillBonus"] = "Tier 3: Crossbow Skill Bonus",
                ["Tier3_Practitioner2_StaffSkillBonus"] = "Tier 3: Staff Skill Bonus",
                ["Tier3_Practitioner2_BowSkillBonus"] = "Tier 3: Bow Skill Bonus",

                // === Tier 4: Master (2개) ===
                ["Tier4_Energizer_FoodConsumptionReduction"] = "Tier 4: Food Consumption Rate Reduction (%)",
                ["Tier4_Captain_ShipSpeedBonus"] = "Tier 4: Ship Speed Bonus (%)",

                // === Tier 5: Jump Master (2개) ===
                ["Tier5_JumpMaster_JumpSkillBonus"] = "Tier 5: Jump Skill Bonus",
                ["Tier5_JumpMaster_JumpStaminaReduction"] = "Tier 5: Jump Stamina Reduction (%)",

                // === Tier 6: Stats (4개) ===
                ["Tier6_Dexterity_MeleeAttackSpeedBonus"] = "Tier 6: Melee Attack Speed Bonus (%)",
                ["Tier6_Dexterity_MoveSpeedBonus"] = "Tier 6: Move Speed Bonus (%)",
                ["Tier6_Endurance_StaminaMaxBonus"] = "Tier 6: Max Stamina Bonus",
                ["Tier6_Intellect_EitrMaxBonus"] = "Tier 6: Max Eitr Bonus",

                // === Tier 7: Master (2개) ===
                ["Tier7_Master_RunSkillBonus"] = "Tier 7: Run Skill Bonus",
                ["Tier7_Master_JumpSkillBonus"] = "Tier 7: Jump Skill Bonus",

                // === Tier 8: Melee Acceleration (2개) ===
                ["Tier8_MeleeAccel_AttackSpeedBonus"] = "Tier 8: Melee Attack Speed Bonus (%)",
                ["Tier8_MeleeAccel_TripleComboBonus"] = "Tier 8: Next Attack Speed Bonus on 3-Hit Combo (%)",

                // === Tier 8: Crossbow Acceleration (2개) ===
                ["Tier8_CrossbowAccel_ReloadSpeed"] = "Tier 8: Reload Speed Bonus (%)",
                ["Tier8_CrossbowAccel_ReloadMoveSpeed"] = "Tier 8: Move Speed During Reload (%)",

                // === Tier 8: Bow Acceleration (2개) ===
                ["Tier8_BowAccel_DrawSpeed"] = "Tier 8: Draw Speed Bonus (%)",
                ["Tier8_BowAccel_DrawMoveSpeed"] = "Tier 8: Move Speed While Drawing (%)",

                // === Tier 8: Cast Acceleration (2개) ===
                ["Tier8_CastAccel_MagicAttackSpeed"] = "Tier 8: Magic Attack Speed Bonus (%)",
                ["Tier8_CastAccel_TripleEitrRecovery"] = "Tier 8: Eitr Max Recovery Rate on 3-Hit Combo (%)",

                // ============================================
                // Defense Tree - 39 Keys
                // ============================================

                // === Defense Tree: Required Points (7) ===
                ["Tier0_DefenseExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_SkinHardening_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_MindBodyHealth_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_CoreDodgeBoostShield_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_ShockwaveStompRock_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_EndureAgileRegenParry_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier6_FinalSkills_RequiredPoints"] = "Tier 6: Required Points",

                // === Tier 0: Defense Expert (2) ===
                ["Tier0_DefenseExpert_HPBonus"] = "Tier 0: HP Bonus",
                ["Tier0_DefenseExpert_ArmorBonus"] = "Tier 0: Armor Bonus",

                // === Tier 1: Skin Hardening (2) ===
                ["Tier1_SkinHardening_HPBonus"] = "Tier 1: HP Bonus",
                ["Tier1_SkinHardening_ArmorBonus"] = "Tier 1: Armor Bonus",

                // === Tier 2: Mind & Body Training (4) ===
                ["Tier2_MindBodyTraining_StaminaBonus"] = "Tier 2: Max Stamina Bonus",
                ["Tier2_MindBodyTraining_EitrBonus"] = "Tier 2: Max Eitr Bonus",
                ["Tier2_HealthTraining_HPBonus"] = "Tier 2: HP Bonus",
                ["Tier2_HealthTraining_ArmorBonus"] = "Tier 2: Armor Bonus",

                // === Tier 3: Defense Techniques (5) ===
                ["Tier3_CoreBreathing_EitrBonus"] = "Tier 3: Eitr Bonus",
                ["Tier3_EvasionTraining_DodgeBonus"] = "Tier 3: Dodge Bonus (%)",
                ["Tier3_EvasionTraining_InvincibilityBonus"] = "Tier 3: Roll Invincibility Increase (%)",
                ["Tier3_HealthBoost_HPBonus"] = "Tier 3: HP Bonus",
                ["Tier3_ShieldTraining_BlockPowerBonus"] = "Tier 3: Shield Block Power Bonus",

                // === Tier 4: Ground Stomp (5) ===
                ["Tier4_GroundStomp_Radius"] = "Tier 4: Effect Radius (m)",
                ["Tier4_GroundStomp_KnockbackForce"] = "Tier 4: Knockback Force",
                ["Tier4_GroundStomp_Cooldown"] = "Tier 4: Cooldown (sec)",
                ["Tier4_GroundStomp_HPThreshold"] = "Tier 4: Auto-Trigger HP Threshold",
                ["Tier4_GroundStomp_VFXDuration"] = "Tier 4: VFX Duration (sec)",
                ["Tier4_RockSkin_ArmorBonus"] = "Tier 4: Armor Amplification (%)",

                // === Tier 5: Endurance & Agility & Regen & Block Master (8) ===
                ["Tier5_Endurance_RunStaminaReduction"] = "Tier 5: Run Stamina Reduction (%)",
                ["Tier5_Endurance_JumpStaminaReduction"] = "Tier 5: Jump Stamina Reduction (%)",
                ["Tier5_Agility_DodgeBonus"] = "Tier 5: Dodge Bonus (%)",
                ["Tier5_Agility_RollStaminaReduction"] = "Tier 5: Roll Stamina Reduction (%)",
                ["Tier5_TrollRegen_HPRegenBonus"] = "Tier 5: HP Regen Bonus (per sec)",
                ["Tier5_TrollRegen_RegenInterval"] = "Tier 5: Regen Interval (sec)",
                ["Tier5_BlockMaster_ShieldBlockPowerBonus"] = "Tier 5: Shield Block Power Bonus",
                ["Tier5_BlockMaster_ParryDurationBonus"] = "Tier 5: Parry Duration Bonus (sec)",

                // === Tier 6: Final Defense Skills (6) ===
                ["Tier6_NerveEnhancement_DodgeBonus"] = "Tier 6: Permanent Dodge Bonus (%)",
                ["Tier6_JotunnVitality_HPBonus"] = "Tier 6: HP Bonus (%)",
                ["Tier6_JotunnVitality_ArmorBonus"] = "Tier 6: Armor Bonus (%)",
                ["Tier6_JotunnShield_BlockStaminaReduction"] = "Tier 6: Block Stamina Reduction (%)",
                ["Tier6_JotunnShield_NormalShieldMoveSpeedBonus"] = "Tier 6: Normal Shield Move Speed Bonus (%)",
                ["Tier6_JotunnShield_TowerShieldMoveSpeedBonus"] = "Tier 6: Tower Shield Move Speed Bonus (%)",

                // ============================================
                // Production Tree - 22 Keys
                // ============================================

                // === Production Tree: Required Points (5) ===
                ["Tier0_ProductionExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_NoviceWorker_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_Specialization_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_IntermediateSkill_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_AdvancedSkill_RequiredPoints"] = "Tier 4: Required Points",

                // === Tier 0: Production Expert (1) ===
                ["Tier0_ProductionExpert_WoodBonusChance"] = "Tier 0: Wood +1 Bonus Chance (%)",

                // === Tier 1: Novice Worker (1) ===
                ["Tier1_NoviceWorker_WoodBonusChance"] = "Tier 1: Wood +1 Bonus Chance (%)",

                // === Tier 2: Specialization (5) ===
                ["Tier2_WoodcuttingLv2_BonusChance"] = "Tier 2: Woodcutting Lv2 - Wood +1 Bonus Chance (%)",
                ["Tier2_GatheringLv2_BonusChance"] = "Tier 2: Gathering Lv2 - Item +1 Bonus Chance (%)",
                ["Tier2_MiningLv2_BonusChance"] = "Tier 2: Mining Lv2 - Ore +1 Bonus Chance (%)",
                ["Tier2_CraftingLv2_UpgradeChance"] = "Tier 2: Crafting Lv2 - Upgrade +1 Bonus Chance (%)",
                ["Tier2_CraftingLv2_DurabilityBonus"] = "Tier 2: Crafting Lv2 - Max Durability Increase (%)",

                // === Tier 3: Intermediate Skills (5) ===
                ["Tier3_WoodcuttingLv3_BonusChance"] = "Tier 3: Woodcutting Lv3 - Wood +2 Bonus Chance (%)",
                ["Tier3_GatheringLv3_BonusChance"] = "Tier 3: Gathering Lv3 - Item +1 Bonus Chance (%)",
                ["Tier3_MiningLv3_BonusChance"] = "Tier 3: Mining Lv3 - Ore +1 Bonus Chance (%)",
                ["Tier3_CraftingLv3_UpgradeChance"] = "Tier 3: Crafting Lv3 - Upgrade +1 Bonus Chance (%)",
                ["Tier3_CraftingLv3_DurabilityBonus"] = "Tier 3: Crafting Lv3 - Max Durability Increase (%)",

                // === Tier 4: Advanced Skills (5) ===
                ["Tier4_WoodcuttingLv4_BonusChance"] = "Tier 4: Woodcutting Lv4 - Wood +2 Bonus Chance (%)",
                ["Tier4_GatheringLv4_BonusChance"] = "Tier 4: Gathering Lv4 - Item +1 Bonus Chance (%)",
                ["Tier4_MiningLv4_BonusChance"] = "Tier 4: Mining Lv4 - Ore +1 Bonus Chance (%)",
                ["Tier4_CraftingLv4_UpgradeChance"] = "Tier 4: Crafting Lv4 - Upgrade +1 Bonus Chance (%)",
                ["Tier4_CraftingLv4_DurabilityBonus"] = "Tier 4: Crafting Lv4 - Max Durability Increase (%)",

                // ============================================
                // Bow Tree - 31 Keys
                // ============================================

                // === Bow Tree: Required Points (7) ===
                ["Tier0_BowExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_FocusedShot_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_MultishotLv1_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_BowMastery_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_MultishotLv2_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_PrecisionAim_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier6_ExplosiveArrow_RequiredPoints"] = "Tier 6: Required Points",

                // === Tier 0: Bow Expert (1) ===
                ["Tier0_BowExpert_DamageBonus"] = "Tier 0: Bow Damage Bonus (%)",

                // === Tier 1: Focused Shot (1) ===
                ["Tier1_FocusedShot_CritBonus"] = "Tier 1: Critical Chance Bonus (%)",

                // === Tier 2: Multishot Lv1 (5) ===
                ["Tier2_MultishotLv1_ActivationChance"] = "Tier 2: Multishot Lv1 Activation Chance (%)",
                ["Tier2_Multishot_AdditionalArrows"] = "Tier 2: Additional Arrows",
                ["Tier2_Multishot_ArrowConsumption"] = "Tier 2: Arrow Consumption",
                ["Tier2_Multishot_DamagePerArrow"] = "Tier 2: Damage Per Arrow (%)",

                // === Tier 3: Bow Mastery (3) ===
                ["Tier3_SpeedShot_SkillBonus"] = "Tier 3: Bow Skill Bonus",
                ["Tier3_SilentShot_DamageBonus"] = "Tier 3: Penetration Damage Increase",
                ["Tier3_SpecialArrow_Chance"] = "Tier 3: Special Arrow Chance (%)",

                // === Tier 4: Multishot Lv2 & Power Shot (3) ===
                ["Tier4_MultishotLv2_ActivationChance"] = "Tier 4: Multishot Lv2 Activation Chance (%)",
                ["Tier4_PowerShot_KnockbackChance"] = "Tier 4: Strong Knockback Chance (%)",
                ["Tier4_PowerShot_KnockbackDistance"] = "Tier 4: Knockback Distance (m)",

                // === Tier 5: Precision Aim & Advanced Skills (6) ===
                ["Tier5_PrecisionAim_CritDamage"] = "Tier 5: Critical Damage Bonus (%)",
                ["Tier5_ArrowRain_Chance"] = "Tier 5: Arrow Rain Chance (%)",
                ["Tier5_ArrowRain_ArrowCount"] = "Tier 5: Arrow Rain Count",
                ["Tier5_BackstepShot_CritBonus"] = "Tier 5: Post-Dodge Crit Chance (%)",
                ["Tier5_BackstepShot_Duration"] = "Tier 5: Post-Dodge Duration (sec)",
                ["Tier5_HuntingInstinct_CritBonus"] = "Tier 5: Hunting Instinct Crit Chance (%)",

                // === Tier 6: Explosive Arrow & Crit Boost (9) ===
                ["Tier6_ExplosiveArrow_DamageMultiplier"] = "Tier 6: Explosive Arrow Damage (%)",
                ["Tier6_ExplosiveArrow_Radius"] = "Tier 6: Explosive Arrow Radius (m)",
                ["Tier6_ExplosiveArrow_Cooldown"] = "Tier 6: Explosive Arrow Cooldown (sec)",
                ["Tier6_ExplosiveArrow_StaminaCost"] = "Tier 6: Explosive Arrow Stamina Cost (%)",
                ["Tier6_CritBoost_DamageBonus"] = "Tier 6: Crit Boost Damage (%)",
                ["Tier6_CritBoost_CritChance"] = "Tier 6: Crit Boost Chance (%)",
                ["Tier6_CritBoost_ArrowCount"] = "Tier 6: Crit Boost Arrow Count",
                ["Tier6_CritBoost_Cooldown"] = "Tier 6: Crit Boost Cooldown (sec)",
                ["Tier6_CritBoost_StaminaCost"] = "Tier 6: Crit Boost Stamina Cost (%)",

                // ============================================
                // Sword Tree - 30 Keys
                // ============================================

                // === Sword Tree: Required Points (11개) ===
                ["Sword_Expert_RequiredPoints"] = "Tier 0: Required Points",
                ["Sword_FastSlash_RequiredPoints"] = "Tier 1: Required Points",
                ["Sword_CounterStance_RequiredPoints"] = "Tier 1: Required Points",
                ["Sword_ComboSlash_RequiredPoints"] = "Tier 2: Required Points",
                ["Sword_BladeReflect_RequiredPoints"] = "Tier 3: Required Points",
                ["Sword_OffenseDefense_RequiredPoints"] = "Tier 4: Required Points",
                ["Sword_TrueDuel_RequiredPoints"] = "Tier 5: Required Points",
                ["Sword_ParryCharge_RequiredPoints"] = "Tier 5: Required Points",
                ["Sword_RushSlash_RequiredPoints"] = "Tier 6: Required Points",

                // === Sword Tree: Sword Expert (1개) ===
                ["Sword_Expert_DamageIncrease"] = "Tier 0: Sword Damage Increase (%)",

                // === Sword Tree: Fast Slash (1개) ===
                ["Sword_FastSlash_AttackSpeedBonus"] = "Tier 1: Attack Speed Bonus (%)",

                // === Sword Tree: Combo Slash (2개) ===
                ["Sword_ComboSlash_Bonus"] = "Tier 2: Combo Attack Bonus (%)",
                ["Sword_ComboSlash_Duration"] = "Tier 2: Buff Duration (sec)",

                // === Sword Tree: Blade Reflect (1개) ===
                ["Sword_BladeReflect_DamageBonus"] = "Tier 3: Damage Bonus",

                // === Sword Tree: True Duel (1개) ===
                ["Sword_TrueDuel_AttackSpeedBonus"] = "Tier 5: Attack Speed Bonus (%)",

                // === Sword Tree: Parry Charge (5개) ===
                ["Sword_ParryCharge_BuffDuration"] = "Tier 5: Buff Duration (sec)",
                ["Sword_ParryCharge_DamageBonus"] = "Tier 5: Charge Attack Bonus (%)",
                ["Sword_ParryCharge_PushDistance"] = "Tier 5: Push Distance (m)",
                ["Sword_ParryCharge_StaminaCost"] = "Tier 5: Stamina Cost",
                ["Sword_ParryCharge_Cooldown"] = "Tier 5: Cooldown (sec)",

                // === Sword Tree: Rush Slash (8개) ===
                ["Sword_RushSlash_Hit1DamageRatio"] = "Tier 6: 1st Hit Damage Ratio (%)",
                ["Sword_RushSlash_Hit2DamageRatio"] = "Tier 6: 2nd Hit Damage Ratio (%)",
                ["Sword_RushSlash_Hit3DamageRatio"] = "Tier 6: 3rd Hit Damage Ratio (%)",
                ["Sword_RushSlash_InitialDashDistance"] = "Tier 6: Initial Dash Distance (m)",
                ["Sword_RushSlash_SideMovementDistance"] = "Tier 6: Side Movement Distance (m)",
                ["Sword_RushSlash_StaminaCost"] = "Tier 6: Stamina Cost",
                ["Sword_RushSlash_Cooldown"] = "Tier 6: Cooldown (sec)",
                ["Sword_RushSlash_MovementSpeed"] = "Tier 6: Movement Speed (m/s)",
                ["Sword_RushSlash_AttackSpeedBonus"] = "Tier 6: Attack Speed Bonus (%)",

                // ============================================
                // Spear Tree - 35 Keys
                // ============================================

                // === Spear Tree: Required Points (7개) ===
                ["Tier0_SpearExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_SpearStep1_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_SpearStep2_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_SpearStep3_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_SpearStep4_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_Penetrate_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier5_Combo_RequiredPoints"] = "Tier 5: Required Points",

                // === Spear Tree: Spear Expert (3개) ===
                ["Tier0_SpearExpert_2HitAttackSpeed"] = "Tier 0: 2-Hit Attack Speed Bonus (%)",
                ["Tier0_SpearExpert_2HitDamageBonus"] = "Tier 0: 2-Hit Damage Bonus (%)",
                ["Tier0_SpearExpert_EffectDuration"] = "Tier 0: Effect Duration (sec)",

                // === Spear Tree: Throw Expert (3개) ===
                ["Tier1_Throw_Cooldown"] = "Tier 1: Throw Cooldown (sec)",
                ["Tier1_Throw_DamageMultiplier"] = "Tier 1: Throw Damage Multiplier (%)",
                ["Tier1_Throw_BuffDuration_NotUsed"] = "Tier 1: Not Used",

                // === Spear Tree: Vital Strike (1개) ===
                ["Tier1_VitalStrike_DamageBonus"] = "Tier 1: Spear Damage Bonus (%)",

                // === Spear Tree: Rapid Spear (1개) ===
                ["Tier3_Rapid_DamageBonus"] = "Tier 3: Weapon Damage Bonus",

                // === Spear Tree: Evasion Strike (2개) ===
                ["Tier4_Evasion_DamageBonus"] = "Tier 4: Post-Dodge Attack Damage (%)",
                ["Tier4_Evasion_StaminaReduction"] = "Tier 4: Attack Stamina Reduction (%)",

                // === Spear Tree: Explosive Spear (3개) ===
                ["Tier3_Explosion_Chance"] = "Tier 3: Explosion Chance (%)",
                ["Tier3_Explosion_Radius"] = "Tier 3: Explosion Radius (m)",
                ["Tier3_Explosion_DamageBonus"] = "Tier 3: Explosion Damage Bonus (%)",

                // === Spear Tree: Dual Spear (2개) ===
                ["Tier4_Dual_DamageBonus"] = "Tier 4: 2-Hit Attack Damage Bonus (%)",
                ["Tier4_Dual_Duration"] = "Tier 4: Buff Duration (sec)",

                // === Spear Tree: Penetrating Spear (6개) ===
                ["Tier5_Penetrate_CritChance_NotUsed"] = "Tier 5: Not Used",
                ["Tier5_Penetrate_BuffDuration"] = "Tier 5: Buff Duration (sec)",
                ["Tier5_Penetrate_LightningDamage"] = "Tier 5: Lightning Shock Damage (%)",
                ["Tier5_Penetrate_HitCount"] = "Tier 5: Lightning Trigger Hit Count",
                ["Tier5_Penetrate_GKey_Cooldown"] = "Tier 5: G-Key Active Cooldown (sec)",
                ["Tier5_Penetrate_GKey_StaminaCost"] = "Tier 5: G-Key Active Stamina Cost (%)",

                // === Spear Tree: Combo Spear (7개) ===
                ["Tier5_Combo_HKey_Cooldown"] = "Tier 5: H-Key Active Cooldown (sec)",
                ["Tier5_Combo_HKey_DamageMultiplier"] = "Tier 5: H-Key Active Damage (%)",
                ["Tier5_Combo_HKey_StaminaCost"] = "Tier 5: H-Key Active Stamina Cost (%)",
                ["Tier5_Combo_HKey_KnockbackRange"] = "Tier 5: H-Key Active Knockback Range (m)",
                ["Tier5_Combo_ActiveRange"] = "Tier 5: Active Effect Range (m)",
                ["Tier5_Combo_BuffDuration"] = "Tier 5: H-Key Buff Duration (sec)",
                ["Tier5_Combo_MaxUses"] = "Tier 5: Max Enhanced Throw Count",

                // ============================================
                // Staff Tree - 30 Keys
                // ============================================

                // === Staff Tree: Required Points (10) ===
                ["Tier0_StaffExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_MindFocus_RequiredPoints"] = "Tier 1: Mind Focus - Required Points",
                ["Tier1_MagicFlow_RequiredPoints"] = "Tier 1: Magic Flow - Required Points",
                ["Tier2_MagicAmplify_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_FrostElement_RequiredPoints"] = "Tier 3: Frost Mastery - Required Points",
                ["Tier3_FireElement_RequiredPoints"] = "Tier 3: Fire Mastery - Required Points",
                ["Tier3_LightningElement_RequiredPoints"] = "Tier 3: Lightning Mastery - Required Points",
                ["Tier4_LuckyMana_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_DoubleCast_RequiredPoints"] = "Tier 5: Double Cast - Required Points",
                ["Tier5_InstantAreaHeal_RequiredPoints"] = "Tier 5: Instant Area Heal - Required Points",

                // === Tier 0: Staff Expert (1) ===
                ["Tier0_StaffExpert_ElementalDamageBonus"] = "Tier 0: Elemental Damage Bonus (%)",

                // === Tier 1: Mind Focus & Magic Flow (2) ===
                ["Tier1_MindFocus_EitrReduction"] = "Tier 1: Eitr Cost Reduction (%)",
                ["Tier1_MagicFlow_EitrBonus"] = "Tier 1: Max Eitr Bonus",

                // === Tier 2: Magic Amplify (3) ===
                ["Tier2_MagicAmplify_Chance"] = "Tier 2: Magic Amplify Trigger Chance (%)",
                ["Tier2_MagicAmplify_DamageBonus"] = "Tier 2: Amplified Elemental Damage Bonus (%)",
                ["Tier2_MagicAmplify_EitrCostIncrease"] = "Tier 2: Eitr Cost Increase (%)",

                // === Tier 3: Elemental Mastery (3) ===
                ["Tier3_FrostElement_DamageBonus"] = "Tier 3: Frost Damage Bonus",
                ["Tier3_FireElement_DamageBonus"] = "Tier 3: Fire Damage Bonus",
                ["Tier3_LightningElement_DamageBonus"] = "Tier 3: Lightning Damage Bonus",

                // === Tier 4: Lucky Mana (1) ===
                ["Tier4_LuckyMana_Chance"] = "Tier 4: Free Cast Trigger Chance (%)",

                // === Tier 5-1: Double Cast - R-Key Active (5) ===
                ["Tier5_DoubleCast_AdditionalProjectileCount"] = "Tier 5: Additional Projectile Count",
                ["Tier5_DoubleCast_ProjectileDamagePercent"] = "Tier 5: Projectile Damage Percent (%)",
                ["Tier5_DoubleCast_AngleOffset"] = "Tier 5: Projectile Angle Offset (deg)",
                ["Tier5_DoubleCast_EitrCost"] = "Tier 5: Eitr Cost",
                ["Tier5_DoubleCast_Cooldown"] = "Tier 5: Cooldown (sec)",

                // === Tier 5-2: Instant Area Heal - H-Key Active (5) ===
                ["Tier5_InstantAreaHeal_Cooldown"] = "Tier 5: Cooldown (sec)",
                ["Tier5_InstantAreaHeal_EitrCost"] = "Tier 5: Eitr Cost",
                ["Tier5_InstantAreaHeal_HealPercent"] = "Tier 5: Heal Amount (% of Max HP)",
                ["Tier5_InstantAreaHeal_Range"] = "Tier 5: Heal Range (m)",
                ["Tier5_InstantAreaHeal_SelfHeal"] = "Tier 5: Allow Self Heal",

                // ============================================
                // Crossbow Tree - 28 Keys
                // ============================================

                // === Crossbow Tree: Required Points (6) ===
                ["Tier0_CrossbowExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_RapidFire_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_CrossbowSkills_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_AutoReload_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_CrossbowSkills_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_OneShot_RequiredPoints"] = "Tier 5: Required Points",

                // === Tier 0: Crossbow Expert (1) ===
                ["Tier0_CrossbowExpert_DamageBonus"] = "Tier 0: Crossbow Damage Bonus (%)",

                // === Tier 1: Rapid Fire (5) ===
                ["Tier1_RapidFire_Chance"] = "Tier 1: Rapid Fire Trigger Chance (%)",
                ["Tier1_RapidFire_ShotCount"] = "Tier 1: Rapid Fire Shot Count",
                ["Tier1_RapidFire_DamagePercent"] = "Tier 1: Rapid Fire Damage Percent (%)",
                ["Tier1_RapidFire_Delay"] = "Tier 1: Rapid Fire Shot Delay (sec)",
                ["Tier1_RapidFire_BoltConsumption"] = "Tier 1: Rapid Fire Bolt Consumption",

                // === Tier 2: Balanced Aim (2) ===
                ["Tier2_BalancedAim_KnockbackChance"] = "Tier 2: Knockback Trigger Chance (%)",
                ["Tier2_BalancedAim_KnockbackDistance"] = "Tier 2: Knockback Distance (m)",

                // === Tier 2: Rapid Reload (1) ===
                ["Tier2_RapidReload_SpeedIncrease"] = "Tier 2: Reload Speed Increase (%)",

                // === Tier 2: Honest Shot (1) ===
                ["Tier2_HonestShot_DamageBonus"] = "Tier 2: Base Damage Bonus (%)",

                // === Tier 3: Auto Reload (1) ===
                ["Tier3_AutoReload_Chance"] = "Tier 3: Auto Reload Trigger Chance (%)",

                // === Tier 4: Rapid Fire Lv2 (5) ===
                ["Tier4_RapidFireLv2_Chance"] = "Tier 4: Rapid Fire Lv2 Trigger Chance (%)",
                ["Tier4_RapidFireLv2_ShotCount"] = "Tier 4: Rapid Fire Lv2 Shot Count",
                ["Tier4_RapidFireLv2_DamagePercent"] = "Tier 4: Rapid Fire Lv2 Damage Percent (%)",
                ["Tier4_RapidFireLv2_Delay"] = "Tier 4: Rapid Fire Lv2 Shot Delay (sec)",
                ["Tier4_RapidFireLv2_BoltConsumption"] = "Tier 4: Rapid Fire Lv2 Bolt Consumption",

                // === Tier 4: Final Strike (2) ===
                ["Tier4_FinalStrike_HpThreshold"] = "Tier 4: Enemy HP Threshold (%)",
                ["Tier4_FinalStrike_DamageBonus"] = "Tier 4: Bonus Damage (%)",

                // === Tier 5: One Shot - R-Key Active (4) ===
                ["Tier5_OneShot_Duration"] = "Tier 5: Buff Duration (sec)",
                ["Tier5_OneShot_DamageBonus"] = "Tier 5: One Shot Damage Bonus (%)",
                ["Tier5_OneShot_KnockbackDistance"] = "Tier 5: Knockback Distance (m)",
                ["Tier5_OneShot_Cooldown"] = "Tier 5: Cooldown (sec)",

                // ============================================
                // Knife Tree - 32 Keys
                // ============================================

                // === Tier 0: Knife Expert (2) ===
                ["Tier0_KnifeExpert_BackstabBonus"] = "Tier 0: Backstab Bonus (%)",
                ["Tier0_KnifeExpert_RequiredPoints"] = "Tier 0: Required Points",

                // === Tier 1: Evasion Mastery (3) ===
                ["Tier1_Evasion_Chance"] = "Tier 1: Evasion Trigger Chance (%)",
                ["Tier1_Evasion_Duration"] = "Tier 1: Evasion Duration (sec)",
                ["Tier1_Evasion_RequiredPoints"] = "Tier 1: Required Points",

                // === Tier 2: Fast Movement (2) ===
                ["Tier2_FastMove_MoveSpeedBonus"] = "Tier 2: Move Speed Bonus (%)",
                ["Tier2_FastMove_RequiredPoints"] = "Tier 2: Required Points",

                // === Tier 3: Combat Mastery (3) ===
                ["Tier3_CombatMastery_DamageBonus"] = "Tier 3: Damage Bonus",
                ["Tier3_CombatMastery_BuffDuration"] = "Tier 3: Buff Duration (sec)",
                ["Tier3_CombatMastery_RequiredPoints"] = "Tier 3: Required Points",

                // === Tier 4: Attack & Evasion (4) ===
                ["Tier4_AttackEvasion_EvasionBonus"] = "Tier 4: Evasion Bonus (%)",
                ["Tier4_AttackEvasion_BuffDuration"] = "Tier 4: Buff Duration (sec)",
                ["Tier4_AttackEvasion_Cooldown"] = "Tier 4: Cooldown (sec)",
                ["Tier4_AttackEvasion_RequiredPoints"] = "Tier 4: Required Points",

                // === Tier 5: Critical Damage (2) ===
                ["Tier5_CriticalDamage_DamageBonus"] = "Tier 5: Critical Damage Bonus (%)",
                ["Tier5_CriticalDamage_RequiredPoints"] = "Tier 5: Required Points",

                // === Tier 6: Assassin (3) ===
                ["Tier6_Assassin_CritDamageBonus"] = "Tier 6: Crit Damage Bonus (%)",
                ["Tier6_Assassin_CritChanceBonus"] = "Tier 6: Crit Chance Bonus (%)",
                ["Tier6_Assassin_RequiredPoints"] = "Tier 6: Required Points",

                // === Tier 7: Assassination (3) ===
                ["Tier7_Assassination_StaggerChance"] = "Tier 7: Stagger Trigger Chance (%)",
                ["Tier7_Assassination_RequiredComboHits"] = "Tier 7: Required Combo Hits",
                ["Tier7_Assassination_RequiredPoints"] = "Tier 7: Required Points",

                // === Tier 8: Assassin's Heart - G-Key Active (10) ===
                ["Tier8_AssassinHeart_CritDamageMultiplier"] = "Tier 8: Crit Damage Multiplier",
                ["Tier8_AssassinHeart_Duration"] = "Tier 8: Buff Duration (sec)",
                ["Tier8_AssassinHeart_StaminaCost"] = "Tier 8: Stamina Cost",
                ["Tier8_AssassinHeart_Cooldown"] = "Tier 8: Cooldown (sec)",
                ["Tier8_AssassinHeart_TeleportRange"] = "Tier 8: Teleport Range (m)",
                ["Tier8_AssassinHeart_TeleportBackDistance"] = "Tier 8: Teleport Behind Distance (m)",
                ["Tier8_AssassinHeart_StunDuration"] = "Tier 8: Stun Duration (sec)",
                ["Tier8_AssassinHeart_ComboAttackCount"] = "Tier 8: Combo Attack Count",
                ["Tier8_AssassinHeart_AttackInterval"] = "Tier 8: Attack Interval (sec)",
                ["Tier8_AssassinHeart_RequiredPoints"] = "Tier 8: Required Points",

                // ============================================
                // Sword Tree - 33 Keys (new key format)
                // ============================================

                // === Tier 0: Sword Expert (2) ===
                ["Tier0_SwordExpert_DamageBonus"] = "Tier 0: Sword Damage Bonus (%)",
                ["Tier0_SwordExpert_RequiredPoints"] = "Tier 0: Required Points",

                // === Tier 1: Fast Slash & Counter Stance (5) ===
                ["Tier1_FastSlash_AttackSpeedBonus"] = "Tier 1: Fast Slash - Attack Speed Bonus (%)",
                ["Tier1_FastSlash_RequiredPoints"] = "Tier 1: Fast Slash - Required Points",
                ["Tier1_CounterStance_Duration"] = "Tier 1: Counter Stance - Buff Duration (sec)",
                ["Tier1_CounterStance_DefenseBonus"] = "Tier 1: Counter Stance - Defense Bonus (%)",
                ["Tier1_CounterStance_RequiredPoints"] = "Tier 1: Counter Stance - Required Points",

                // === Tier 2: Combo Slash (3) ===
                ["Tier2_ComboSlash_DamageBonus"] = "Tier 2: Combo Attack Bonus (%)",
                ["Tier2_ComboSlash_BuffDuration"] = "Tier 2: Buff Duration (sec)",
                ["Tier2_ComboSlash_RequiredPoints"] = "Tier 2: Required Points",

                // === Tier 3: Riposte (2) ===
                ["Tier3_Riposte_DamageBonus"] = "Tier 3: Damage Bonus",
                ["Tier3_Riposte_RequiredPoints"] = "Tier 3: Required Points",

                // === Tier 4: All-In-One & True Duel (5) ===
                ["Tier4_AllInOne_AttackBonus"] = "Tier 4: All-In-One - Attack Bonus (%)",
                ["Tier4_AllInOne_DefenseBonus"] = "Tier 4: All-In-One - Defense Bonus",
                ["Tier4_AllInOne_RequiredPoints"] = "Tier 4: All-In-One - Required Points",
                ["Tier4_TrueDuel_AttackSpeedBonus"] = "Tier 4: True Duel - Attack Speed Bonus (%)",
                ["Tier4_TrueDuel_RequiredPoints"] = "Tier 4: True Duel - Required Points",

                // === Tier 5: Parry Rush - G-Key Active (6) ===
                ["Tier5_ParryRush_BuffDuration"] = "Tier 5: Buff Duration (sec)",
                ["Tier5_ParryRush_DamageBonus"] = "Tier 5: Damage Bonus (%)",
                ["Tier5_ParryRush_PushDistance"] = "Tier 5: Push Distance (m)",
                ["Tier5_ParryRush_StaminaCost"] = "Tier 5: Stamina Cost",
                ["Tier5_ParryRush_Cooldown"] = "Tier 5: Cooldown (sec)",
                ["Tier5_ParryRush_RequiredPoints"] = "Tier 5: Required Points",

                // === Tier 6: Rush Slash - G-Key Active (10) ===
                ["Tier6_RushSlash_Hit1DamageRatio"] = "Tier 6: Hit 1 Damage Ratio (%)",
                ["Tier6_RushSlash_Hit2DamageRatio"] = "Tier 6: Hit 2 Damage Ratio (%)",
                ["Tier6_RushSlash_Hit3DamageRatio"] = "Tier 6: Hit 3 Damage Ratio (%)",
                ["Tier6_RushSlash_InitialDistance"] = "Tier 6: Initial Dash Distance (m)",
                ["Tier6_RushSlash_SideDistance"] = "Tier 6: Side Movement Distance (m)",
                ["Tier6_RushSlash_StaminaCost"] = "Tier 6: Stamina Cost",
                ["Tier6_RushSlash_Cooldown"] = "Tier 6: Cooldown (sec)",
                ["Tier6_RushSlash_MoveSpeed"] = "Tier 6: Move Speed (m/s)",
                ["Tier6_RushSlash_AttackSpeedBonus"] = "Tier 6: Attack Speed Bonus (%)",
                ["Tier6_RushSlash_RequiredPoints"] = "Tier 6: Required Points",

                // ============================================
                // Mace Tree - 34 Keys
                // ============================================

                // === Mace Tree: Required Points (11) ===
                ["Tier0_MaceExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_MaceExpert_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_StunBoost_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_Guard_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier3_HeavyStrike_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_Push_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_Tank_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier5_DPS_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier6_Grandmaster_RequiredPoints"] = "Tier 6: Required Points",
                ["Tier7_FuryHammer_RequiredPoints"] = "Tier 7: Required Points",
                ["Tier7_GuardianHeart_RequiredPoints"] = "Tier 7: Required Points",

                // === Tier 0: Mace Expert (3) ===
                ["Tier0_MaceExpert_DamageBonus"] = "Tier 0: Mace Damage Bonus (%)",
                ["Tier0_MaceExpert_StunChance"] = "Tier 0: Stun Chance (%)",
                ["Tier0_MaceExpert_StunDuration"] = "Tier 0: Stun Duration (sec)",

                // === Tier 1: Mace Damage Boost (1) ===
                ["Tier1_MaceExpert_DamageBonus"] = "Tier 1: Mace Damage Bonus (%)",

                // === Tier 2: Stun Boost (2) ===
                ["Tier2_StunBoost_StunChanceBonus"] = "Tier 2: Stun Chance Bonus (%)",
                ["Tier2_StunBoost_StunDurationBonus"] = "Tier 2: Stun Duration Bonus (sec)",

                // === Tier 3: Guard (1) ===
                ["Tier3_Guard_ArmorBonus"] = "Tier 3: Armor Bonus",

                // === Tier 3: Heavy Strike (1) ===
                ["Tier3_HeavyStrike_DamageBonus"] = "Tier 3: Damage Bonus",

                // === Tier 4: Push (1) ===
                ["Tier4_Push_KnockbackChance"] = "Tier 4: Knockback Chance (%)",

                // === Tier 5: Tank (2) ===
                ["Tier5_Tank_HealthBonus"] = "Tier 5: Health Bonus (%)",
                ["Tier5_Tank_DamageReduction"] = "Tier 5: Incoming Damage Reduction (%)",

                // === Tier 5: DPS (2) ===
                ["Tier5_DPS_DamageBonus"] = "Tier 5: Damage Bonus (%)",
                ["Tier5_DPS_AttackSpeedBonus"] = "Tier 5: Attack Speed Bonus (%)",

                // === Tier 6: Grandmaster (1) ===
                ["Tier6_Grandmaster_ArmorBonus"] = "Tier 6: Armor Bonus (%)",

                // === Tier 7: Fury Hammer - G-Key Active (5) ===
                ["Tier7_FuryHammer_NormalHitMultiplier"] = "Tier 7: Hits 1-4 Damage Multiplier (%)",
                ["Tier7_FuryHammer_FinalHitMultiplier"] = "Tier 7: Hit 5 Final Damage Multiplier (%)",
                ["Tier7_FuryHammer_StaminaCost"] = "Tier 7: Stamina Cost",
                ["Tier7_FuryHammer_Cooldown"] = "Tier 7: Cooldown (sec)",
                ["Tier7_FuryHammer_AoeRadius"] = "Tier 7: AOE Radius (m)",

                // === Tier 7: Guardian Heart - H-Key Active (4) ===
                ["Tier7_GuardianHeart_Cooldown"] = "Tier 7: Cooldown (sec)",
                ["Tier7_GuardianHeart_StaminaCost"] = "Tier 7: Stamina Cost",
                ["Tier7_GuardianHeart_Duration"] = "Tier 7: Buff Duration (sec)",
                ["Tier7_GuardianHeart_ReflectPercent"] = "Tier 7: Reflect Damage Percent (%)",

                // ========================================
                // Polearm Tree (25 keys)
                // ========================================

                // === Required Points (7) ===
                ["Tier0_PolearmExpert_RequiredPoints"] = "Tier 0: Required Points",
                ["Tier1_PolearmSkill_RequiredPoints"] = "Tier 1: Required Points",
                ["Tier2_PolearmSkill_RequiredPoints"] = "Tier 2: Required Points",
                ["Tier3_PolearmSkill_RequiredPoints"] = "Tier 3: Required Points",
                ["Tier4_PolearmSkill_RequiredPoints"] = "Tier 4: Required Points",
                ["Tier5_Suppress_RequiredPoints"] = "Tier 5: Required Points",
                ["Tier6_PierceCharge_RequiredPoints"] = "Tier 6: Required Points",

                // === Tier 0 - Polearm Expert (1) ===
                ["Tier0_PolearmExpert_AttackRangeBonus"] = "Tier 0: Attack Range Bonus (%)",

                // === Tier 1 - Spin Wheel (1) ===
                ["Tier1_SpinWheel_WheelAttackDamageBonus"] = "Tier 1: Wheel Attack Damage Bonus (%)",

                // === Tier 2 - Hero Strike (1) ===
                ["Tier2_HeroStrike_KnockbackChance"] = "Tier 2: Knockback Chance (%)",

                // === Tier 3 (4) ===
                ["Tier3_AreaCombo_DoubleHitBonus"] = "Tier 3: Double Hit Damage Bonus (%)",
                ["Tier3_AreaCombo_DoubleHitDuration"] = "Tier 3: Double Hit Buff Duration (sec)",
                ["Tier3_GroundWheel_WheelAttackDamageBonus"] = "Tier 3: Ground Wheel Attack Damage Bonus (%)",
                ["Tier3_PolearmBoost_WeaponDamageBonus"] = "Tier 3: Weapon Damage Bonus (flat)",

                // === Tier 4 - Moon Slash (2) ===
                ["Tier4_MoonSlash_AttackRangeBonus"] = "Tier 4: Attack Range Bonus (%)",
                ["Tier4_MoonSlash_StaminaReduction"] = "Tier 4: Stamina Reduction (%)",

                // === Tier 5 - Suppress Attack (1) ===
                ["Tier5_Suppress_DamageBonus"] = "Tier 5: Suppress Attack Damage Bonus (%)",

                // === Tier 6 - Pierce Charge (8) ===
                ["Tier6_PierceCharge_DashDistance"] = "Tier 6: Dash Distance (m)",
                ["Tier6_PierceCharge_FirstHitDamageBonus"] = "Tier 6: First Hit Damage Bonus (%)",
                ["Tier6_PierceCharge_AoeDamageBonus"] = "Tier 6: AOE Knockback Damage Bonus (%)",
                ["Tier6_PierceCharge_AoeAngle"] = "Tier 6: AOE Angle (degrees)",
                ["Tier6_PierceCharge_AoeRadius"] = "Tier 6: AOE Radius (m)",
                ["Tier6_PierceCharge_KnockbackDistance"] = "Tier 6: Knockback Distance (m)",
                ["Tier6_PierceCharge_StaminaCost"] = "Tier 6: Stamina Cost",
                ["Tier6_PierceCharge_Cooldown"] = "Tier 6: Cooldown (sec)",

                // ============================================
                // Archer Job Skills - 8 Keys
                // ============================================
                ["Archer_MultiShot_ArrowCount"] = "Multishot: Arrow Count",
                ["Archer_MultiShot_ArrowConsumption"] = "Multishot: Arrow Consumption",
                ["Archer_MultiShot_DamagePercent"] = "Multishot: Damage Per Arrow (%)",
                ["Archer_MultiShot_Cooldown"] = "Multishot: Cooldown (sec)",
                ["Archer_MultiShot_Charges"] = "Multishot: Shot Charges",
                ["Archer_MultiShot_StaminaCost"] = "Multishot: Stamina Cost",
                ["Archer_JumpHeightBonus"] = "Passive: Jump Height Bonus (%)",
                ["Archer_FallDamageReduction"] = "Passive: Fall Damage Reduction (%)",

                // ============================================
                // Mage Job Skills - 6 Keys
                // ============================================
                ["Mage_AOE_Range"] = "Active: Range (m)",
                ["Mage_Eitr_Cost"] = "Active: Eitr Cost",
                ["Mage_Damage_Multiplier"] = "Active: Damage Multiplier (%)",
                ["Mage_Cooldown"] = "Active: Cooldown (sec)",
                ["Mage_VFX_Name"] = "Active: VFX Effect Name",
                ["Mage_Elemental_Resistance"] = "Passive: Elemental Resistance (%)",

                // ============================================
                // Tanker Job Skills - 10 Keys
                // ============================================
                ["Tanker_Taunt_Cooldown"] = "War Cry: Cooldown (sec)",
                ["Tanker_Taunt_StaminaCost"] = "War Cry: Stamina Cost",
                ["Tanker_Taunt_Range"] = "War Cry: Taunt Range (m)",
                ["Tanker_Taunt_Duration"] = "War Cry: Taunt Duration (sec)",
                ["Tanker_Taunt_BossDuration"] = "War Cry: Boss Taunt Duration (sec)",
                ["Tanker_Taunt_DamageReduction"] = "War Cry: Damage Reduction (%)",
                ["Tanker_Taunt_BuffDuration"] = "War Cry: Buff Duration (sec)",
                ["Tanker_Taunt_EffectHeight"] = "War Cry: Effect Height (m)",
                ["Tanker_Taunt_EffectScale"] = "War Cry: Effect Scale",
                ["Tanker_Passive_DamageReduction"] = "Passive: Damage Reduction (%)",

                // ============================================
                // Rogue Job Skills - 10 Keys
                // ============================================
                ["Rogue_ShadowStrike_Cooldown"] = "Shadow Strike: Cooldown (sec)",
                ["Rogue_ShadowStrike_StaminaCost"] = "Shadow Strike: Stamina Cost",
                ["Rogue_ShadowStrike_AttackBonus"] = "Shadow Strike: Attack Bonus (%)",
                ["Rogue_ShadowStrike_BuffDuration"] = "Shadow Strike: Buff Duration (sec)",
                ["Rogue_ShadowStrike_SmokeScale"] = "Shadow Strike: Smoke Scale",
                ["Rogue_ShadowStrike_AggroRange"] = "Shadow Strike: Aggro Range (m)",
                ["Rogue_ShadowStrike_StealthDuration"] = "Shadow Strike: Stealth Duration (sec)",
                ["Rogue_AttackSpeed_Bonus"] = "Passive: Attack Speed Bonus (%)",
                ["Rogue_Stamina_Reduction"] = "Passive: Stamina Usage Reduction (%)",
                ["Rogue_ElementalResistance_Debuff"] = "Passive: Elemental Resistance Increase (%)",

                // ============================================
                // Paladin Job Skills - 9 Keys
                // ============================================
                ["Paladin_Active_Cooldown"] = "Holy Healing: Cooldown (sec)",
                ["Paladin_Active_Range"] = "Holy Healing: Range (m)",
                ["Paladin_Active_EitrCost"] = "Holy Healing: Eitr Cost",
                ["Paladin_Active_StaminaCost"] = "Holy Healing: Stamina Cost",
                ["Paladin_Active_SelfHealPercent"] = "Holy Healing: Self Heal Ratio (%)",
                ["Paladin_Active_AllyHealPercentOverTime"] = "Holy Healing: Ally HoT Ratio (%)",
                ["Paladin_Active_Duration"] = "Holy Healing: Duration (sec)",
                ["Paladin_Active_Interval"] = "Holy Healing: Interval (sec)",
                ["Paladin_Passive_ElementalResistanceReduction"] = "Passive: Resistance Bonus (%)",

                // ============================================
                // Berserker Job Skills - 10 Keys
                // ============================================
                ["Beserker_Active_Cooldown"] = "Berserker Rage: Cooldown (sec)",
                ["Beserker_Active_StaminaCost"] = "Berserker Rage: Stamina Cost",
                ["Beserker_Active_Duration"] = "Berserker Rage: Duration (sec)",
                ["Beserker_Active_DamagePerHealthPercent"] = "Berserker Rage: Damage Per HP% (%)",
                ["Beserker_Active_MaxDamageBonus"] = "Berserker Rage: Max Damage Bonus (%)",
                ["Beserker_Active_HealthThreshold"] = "Berserker Rage: HP Threshold (%)",
                ["Beserker_Passive_HealthThreshold"] = "Death Defiance: HP Threshold (%)",
                ["Beserker_Passive_InvincibilityDuration"] = "Death Defiance: Invincibility Duration (sec)",
                ["Beserker_Passive_Cooldown"] = "Death Defiance: Cooldown (sec)",
                ["Berserker_Passive_HealthBonus"] = "Passive: Max HP Bonus (%)",
            };
        }
    }
}

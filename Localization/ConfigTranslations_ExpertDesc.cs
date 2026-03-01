using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class ConfigTranslations
    {
        private static Dictionary<string, string> GetExpertDescriptions_KO()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Attack Tree (공격 트리)
                // ========================================









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

                // === Tier 1: 기본 공격 강화 (Base Attack) ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] =
                "【물리 데미지 보너스 (고정값)】\n" +
                "모든 무기의 물리 데미지를 고정값으로 증가시킵니다.\n" +
                "권장값: 1-3",

                ["Tier1_BaseAttack_ElementalDamageBonus"] =
                "【속성 데미지 보너스 (고정값)】\n" +
                "모든 무기의 속성 데미지(불, 얼음, 번개 등)를 고정값으로 증가시킵니다.\n" +
                "권장값: 1-3",

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
                "【물리/속성 저항 (%)】\n" +
                "요툰의 생명력으로 모든 물리/속성 데미지가 감소합니다.\n" +
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

                // === Speed Tree: 필요 포인트 설명 ===
                ["Tier0_SpeedExpert_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier1_AgilityBase_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier2_MeleeFlow_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier2_BowExpert_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier2_MobileCast_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier3_Practitioner1_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier3_Practitioner2_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier4_Energizer_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier4_Captain_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier5_JumpMaster_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier6_Dexterity_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier6_Endurance_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier6_Intellect_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier7_Master_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier8_MeleeAccel_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier8_BowAccel_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",
                ["Tier8_CastAccel_RequiredPoints"] = "【필요 포인트】\n이 노드 해금에 필요한 스킬 포인트 수입니다.",

            };
        }

        private static Dictionary<string, string> GetExpertDescriptions_EN()
        {
            return new Dictionary<string, string>
            {
                // ========================================
                // Attack Tree
                // ========================================









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

                // === Tier 1: Base Attack Enhancement ===
                ["Tier1_BaseAttack_PhysicalDamageBonus"] =
                "【Physical Damage Bonus (Flat)】\n" +
                "Increases physical damage of all weapons by a flat amount.\n" +
                "Recommended: 1-3",

                ["Tier1_BaseAttack_ElementalDamageBonus"] =
                "【Elemental Damage Bonus (Flat)】\n" +
                "Increases elemental damage (fire, frost, lightning) of all weapons by a flat amount.\n" +
                "Recommended: 1-3",

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
                "【Physical/Elemental Resistance (%)】\n" +
                "Reduces all physical and elemental damage received.\n" +
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

                // === Speed Tree: Required Points ===
                ["Tier0_SpeedExpert_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier1_AgilityBase_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier2_MeleeFlow_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier2_CrossbowExpert_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier2_BowExpert_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier2_MobileCast_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier3_Practitioner1_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier3_Practitioner2_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier4_Energizer_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier4_Captain_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier5_JumpMaster_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier6_Dexterity_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier6_Endurance_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier6_Intellect_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier7_Master_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier8_MeleeAccel_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier8_CrossbowAccel_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier8_BowAccel_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",
                ["Tier8_CastAccel_RequiredPoints"] = "【Required Points】\nSkill points required to unlock this node.",

            };
        }
    }
}

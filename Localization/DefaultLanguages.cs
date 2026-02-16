using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    /// <summary>
    /// Default language data for Korean and English
    /// These are used when JSON files don't exist or fail to load
    /// </summary>
    public static class DefaultLanguages
    {
        /// <summary>
        /// Get Korean translations (default language)
        /// </summary>
        public static Dictionary<string, string> GetKorean()
        {
            return new Dictionary<string, string>
            {
                // === Common Messages ===
                ["cooldown_remaining"] = "{0} 쿨다운 중! 남은 시간: {1}초",
                ["skill_cooldown"] = "{0} 스킬 쿨타임: {1}초 남음",
                ["stamina_insufficient"] = "스태미나가 부족합니다",
                ["eitr_insufficient"] = "Eitr 부족! (필요: {0})",
                ["no_enemies_in_range"] = "범위 내에 적이 없습니다!",
                ["no_targets_in_range"] = "범위 내에 적이 없습니다!",

                // === Archer Passive ===
                ["archer_jump_bonus"] = "아처 패시브: 점프 높이 +{0}%",
                ["archer_fall_damage_reduced"] = "아처 패시브: -{0} 낙사 데미지 감소!",

                // === Mage Skills ===
                ["mage_cooldown"] = "메이지 스킬 쿨타임: {0}초 남음",
                ["staff_required"] = "지팡이를 착용해야 합니다!",
                ["mage_spell_cast"] = "메이지 주문 시전! {0}마리 표적 지정!",
                ["mage_explosion_damage"] = "{0} 폭발! {1} 데미지!",
                ["mage_elemental_resist"] = "메이지 패시브: -{0} 속성 데미지 감소!",

                // === Rogue Skills ===
                ["rogue_shadow_strike_cooldown"] = "그림자 일격 쿨다운 중! 남은 시간: {0}초",
                ["rogue_dagger_required"] = "단검 또는 클로 착용이 필요합니다",
                ["rogue_shadow_strike_success"] = "그림자 일격! {0}마리 어그로 해제!",
                ["rogue_shadow_strike_no_enemy"] = "그림자 일격! (주변에 적이 없음)",
                ["rogue_stealth_start"] = "스텔스 시작! ({0}초)",
                ["rogue_stealth_end"] = "스텔스 해제! (이유: {0})",
                ["rogue_buff_end"] = "그림자 일격 버프 종료!",
                ["rogue_smoke"] = "연막!",
                ["rogue_passive_fall_damage"] = "로그 패시브: -{0} 낙사 데미지 감소!",

                // === Tanker Skills ===
                ["tanker_taunt_cooldown"] = "도발 쿨다운 중! 남은 시간: {0}초",
                ["tanker_shield_required"] = "방패가 필요합니다",
                ["tanker_taunt_success"] = "도발! {0}마리가 당신을 타겟팅합니다!",
                ["tanker_taunt_no_enemy"] = "주변에 도발할 적이 없습니다",
                ["tanker_passive_damage_reduction"] = "탱커 패시브: -{0} 데미지 감소!",

                // === Berserker Skills ===
                ["berserker_rage_cooldown"] = "분노 쿨다운 중! 남은 시간: {0}초",
                ["berserker_rage_active"] = "분노 발동! 공격력 +{0}%!",
                ["berserker_rage_end"] = "분노 버프 종료!",
                ["berserker_passive_lifesteal"] = "버서커 패시브: 생명력 흡수 +{0}!",

                // === Paladin Skills ===
                ["paladin_cooldown"] = "성기사 스킬 쿨다운 중! 남은 시간: {0}초",
                ["paladin_weapon_required"] = "한손 근접무기를 착용해야 합니다",
                ["paladin_heal_cast"] = "성기사 축복! {0}명 치유!",
                ["paladin_heal_amount"] = "성기사 축복: +{0} 체력 회복!",

                // === Sword Skills ===
                ["sword_dash_cooldown"] = "돌진베기 쿨다운 중! 남은 시간: {0}초",
                ["sword_required"] = "검을 착용해야 합니다",
                ["sword_dash_success"] = "돌진베기! {0} 데미지!",
                ["sword_parry_cooldown"] = "패링돌격 쿨다운 중! 남은 시간: {0}초",
                ["sword_parry_success"] = "패링돌격! 방어력 +{0}%!",

                // === Spear Skills ===
                ["spear_penetrate_cooldown"] = "꿰뚫기 쿨다운 중! 남은 시간: {0}초",
                ["spear_required"] = "창을 착용해야 합니다",
                ["spear_penetrate_success"] = "꿰뚫기! {0} 데미지!",
                ["spear_combo_cooldown"] = "연공창 쿨다운 중! 남은 시간: {0}초",
                ["spear_combo_success"] = "연공창! {0}회 연속 공격!",

                // === Mace Skills ===
                ["mace_guardian_cooldown"] = "수호자의 진심 쿨다운 중! 남은 시간: {0}초",
                ["mace_required"] = "둔기를 착용해야 합니다",
                ["mace_guardian_success"] = "수호자의 진심! 방어력 +{0}%!",
                ["mace_fury_cooldown"] = "분노의 망치 쿨다운 중! 남은 시간: {0}초",
                ["mace_fury_success"] = "분노의 망치! {0} 데미지!",

                // === Polearm Skills ===
                ["polearm_king_cooldown"] = "장창제왕 쿨다운 중! 남은 시간: {0}초",
                ["polearm_required"] = "폴암을 착용해야 합니다",
                ["polearm_king_success"] = "장창제왕! {0} 데미지!",

                // === Knife Skills ===
                ["knife_assassin_cooldown"] = "암살자의 심장 쿨다운 중! 남은 시간: {0}초",
                ["knife_required"] = "단검을 착용해야 합니다",
                ["knife_assassin_success"] = "암살자의 심장! 치명타 데미지 +{0}%!",

                // === Bow Skills ===
                ["bow_explosive_cooldown"] = "폭발 화살 쿨다운 중! 남은 시간: {0}초",
                ["bow_required"] = "활을 착용해야 합니다",
                ["bow_explosive_success"] = "폭발 화살! {0} 데미지!",

                // === Crossbow Skills ===
                ["crossbow_oneshot_cooldown"] = "단 한 발 쿨다운 중! 남은 시간: {0}초",
                ["crossbow_required"] = "석궁을 착용해야 합니다",
                ["crossbow_oneshot_success"] = "단 한 발! {0} 데미지!",

                // === Staff Skills ===
                ["staff_dual_cast_cooldown"] = "이중 시전 쿨다운 중! 남은 시간: {0}초",
                ["staff_required"] = "지팡이를 착용해야 합니다",
                ["staff_dual_cast_success"] = "이중 시전! 마법 2배 발사!",
                ["staff_heal_cooldown"] = "힐 쿨다운 중! 남은 시간: {0}초",
                ["staff_heal_success"] = "힐! +{0} 체력 회복!",

                // === Job Active Skills (Y Key) ===
                ["job_skill_activated"] = "{0} 스킬 발동!",
                ["job_skill_no_job"] = "직업을 먼저 선택해야 합니다",

                // === Stealth System ===
                ["stealth_reason_attack"] = "공격",
                ["stealth_reason_damage"] = "피격",
                ["stealth_reason_expired"] = "시간 만료",

                // === General UI ===
                ["skill_points"] = "스킬 포인트",
                ["reset_button"] = "초기화",
                ["skill_learned"] = "{0} 스킬 습득!",
                ["skill_already_learned"] = "이미 습득한 스킬입니다",
                ["skill_prerequisites_not_met"] = "선행 스킬을 먼저 습득해야 합니다",
                ["skill_insufficient_points"] = "스킬 포인트가 부족합니다",
                ["skill_level_required"] = "레벨 {0} 이상이 필요합니다",

                // === Skill Types ===
                ["skill_type_passive"] = "패시브스킬",
                ["skill_type_active_y"] = "액티브스킬 (Y키)",
                ["skill_type_active_r"] = "액티브스킬 (R키)",
                ["skill_type_active_g"] = "액티브스킬 (G키)",
                ["skill_type_active_h"] = "액티브스킬 (H키)",
                ["skill_type_job_active"] = "직업 액티브스킬 (Y키)",

                // === Tooltip Labels ===
                ["tooltip_description"] = "설명",
                ["tooltip_range"] = "범위",
                ["tooltip_cost"] = "소모",
                ["tooltip_skill_type"] = "스킬유형",
                ["tooltip_cooldown"] = "쿨타임",
                ["tooltip_requirements"] = "필요조건",
                ["tooltip_notice"] = "확인사항",
                ["tooltip_required_points"] = "필요 포인트",
                ["tooltip_learned"] = "습득완료",
                ["tooltip_damage"] = "데미지",
                ["tooltip_passive"] = "패시브",
                ["tooltip_required_item"] = "필요 아이템",
                ["tooltip_error"] = "툴팁 생성 오류",
                ["tooltip_unknown_skill"] = "알 수 없는 {0} 스킬",

                // === Skill Types Extended ===
                ["skill_type_active"] = "액티브 스킬",
                ["skill_type_active_key"] = "액티브 스킬 - {0}키",

                // === Weapon Requirements ===
                ["requirement_sword_equip"] = "검 착용",
                ["requirement_spear_equip"] = "창 착용",
                ["requirement_mace_equip"] = "둔기 착용",
                ["requirement_polearm_equip"] = "폴암 착용",
                ["requirement_knife_equip"] = "단검 착용",
                ["requirement_bow_equip"] = "활 착용",
                ["requirement_crossbow_equip"] = "석궁 착용",
                ["requirement_staff_equip"] = "지팡이 착용",
                ["requirement_weapon_equip"] = "무기 착용",
                ["requirement_shield_equip"] = "방패 착용",
                ["requirement_one_hand_melee"] = "한손 근접무기 착용",

                // === Units ===
                ["unit_seconds"] = "초",
                ["unit_meter"] = "m",
                ["unit_arrow"] = "화살",
                ["unit_pieces"] = "개",

                // === Item Names ===
                ["item_wood"] = "나무",
                ["item_raspberry"] = "라즈베리",
                ["item_mushroom"] = "버섯",
                ["item_blueberries"] = "블루베리",
                ["item_copper_ore"] = "구리광석",
                ["item_iron_ore"] = "철 광석",
                ["item_silver_ore"] = "은 광석",
                ["item_bronze_sword"] = "청동 검",
                ["item_bronze_helmet"] = "청동 헬멧",
                ["item_iron_sword"] = "철 검",
                ["item_iron_helmet"] = "철 헬멧",
                ["item_silver_sword"] = "은 검",
                ["item_drake_helmet"] = "드레이크 헬멧",
                ["item_copper_knife"] = "구리 단검",
                ["item_crude_bow"] = "조잡한 활",
                ["item_spear"] = "창",

                // === Item Requirement Formats ===
                ["item_consumed"] = "(소모)",
                ["item_quantity_required"] = "{0} {1}개 보유",
                ["item_equip_required"] = "{0} 착용",
                ["item_equip_consume"] = "{0} 착용(소모)",

                // === Tooltip Format Strings ===
                ["seconds_format"] = "{0}초",
                ["stamina_format"] = "스태미나 {0}",
                ["stamina_percent_format"] = "스태미나 {0}%",
                ["attack_power_bonus_format"] = "공격력 +{0}%",
                ["knockback_format"] = "넉백 {0}m",
                ["bow_explosive_tooltip_desc"] = "폭발 화살 발사, 적중 시 주변 적에게 폭발 피해",
                ["bow_explosive_damage_format"] = "공격력의 {0}% 폭발 피해",
                ["bow_explosive_range_format"] = "폭발 범위 {0}m",
                ["crossbow_oneshot_tooltip_desc"] = "{0}초간 버프 활성화, 다음 석궁 발사 시 강력한 일격",

                // === Common Tooltip Text ===
                ["tooltip_job_limit"] = "직업은 1개만 선택가능",
                ["tooltip_level_required"] = "레벨 {0} 이상",
                ["tooltip_same_weapon_only"] = "같은 무기 전문가 내에서만 다중 습득 가능",
                ["tooltip_not_invincible"] = "스킬 사용 중 무적 아님",
                ["tooltip_eikthyr_trophy"] = "에이크쉬르 트로피",

                // === Sword Skill Names ===
                ["sword_skill_expert"] = "검 전문가",
                ["sword_skill_rush_slash"] = "돌진 연속 베기",
                ["sword_skill_fast_slash"] = "빠른 베기",
                ["sword_skill_counter"] = "반격 자세",
                ["sword_skill_combo"] = "연속베기",
                ["sword_skill_riposte"] = "칼날 되치기",
                ["sword_skill_all_in_one"] = "공방일체",
                ["sword_skill_duel"] = "진검승부",
                ["sword_skill_parry_rush"] = "패링 돌격",
                ["sword_skill_ultimate"] = "궁극 베기",

                // === Sword Skill Descriptions ===
                ["sword_desc_expert"] = "검 공격력 +{0}%\n2연속 공격력 +{1}% ({2}초)",
                ["sword_desc_rush_slash"] = "전방 {0}m 돌진 후 몬스터 주변을 빠르게 이동하며 3회 연속 베기",
                ["sword_desc_rush_slash_1st"] = "1차 베기: 공격력 {0}%",
                ["sword_desc_rush_slash_2nd"] = "2차 베기: 공격력 {0}%",
                ["sword_desc_rush_slash_3rd"] = "3차 베기 (피니셔): 공격력 {0}%",
                ["sword_desc_fast_slash"] = "공격속도 +{0}%",
                ["sword_desc_counter"] = "패링 성공 후 {0}초동안 방어력 +{1}%",
                ["sword_desc_combo"] = "3연속 공격력 +{0}% ({1}초)",
                ["sword_desc_riposte"] = "공격력 +{0}",
                ["sword_desc_all_in_one"] = "양손 무기 착용 시 공격력 +{0}%, 방어력 +{1}%",
                ["sword_desc_duel"] = "공격 속도 +{0}%",
                ["sword_desc_parry_rush"] = "{0}초 동안 패링 성공 시 몬스터에게 방패돌격",
                ["sword_desc_parry_rush_damage"] = "공격력 +{0}%",
                ["sword_desc_parry_rush_push"] = "{0}m 밀어내기",
                ["sword_desc_ultimate"] = "모든 검 스킬 효과 +{0}%",

                // === Sword Additional Tooltips ===
                ["tooltip_sword_effect_note"] = "※ 검 사용 시 효과 발동",
                ["tooltip_sword_damage_boost"] = "검 공격력 향상",
                ["tooltip_skill_not_found"] = "{0} 스킬 정보를 찾을 수 없음",
                ["tooltip_generation_error"] = "툴팁 생성 오류",

                // === Archer Skill ===
                ["archer_skill_multishot"] = "멀티샷",
                ["archer_desc_multishot"] = "{0}발씩 {1}회 발사, 화살 1발은 활+화살 공격력의 {2}%",
                ["archer_desc_passive"] = "점프 높이 +{0}%, 낙사 데미지 -{1}%",
                ["archer_range_arrows"] = "화살 {0}개 발사",

                // === Mace Skill Names ===
                ["mace_skill_expert"] = "둔기 전문가",
                ["mace_skill_guardian"] = "수호자의 진심",
                ["mace_skill_fury"] = "분노의 망치",
                ["mace_skill_damage_boost"] = "둔기 강화",
                ["mace_skill_stun_boost"] = "기절 강화",
                ["mace_skill_guard_boost"] = "방어 강화",
                ["mace_skill_heavy_strike"] = "무거운 일격",
                ["mace_skill_knockback"] = "밀어내기",
                ["mace_skill_tanker"] = "탱커",
                ["mace_skill_dps_boost"] = "데미지 강화",
                ["mace_skill_grandmaster"] = "그랜드마스터",

                // === Mace Skill Descriptions ===
                ["mace_desc_expert"] = "공격력 +{0}%, 기절 확률 +{1}%, 기절 지속시간 +{2}초",
                ["mace_desc_damage_boost"] = "둔기 공격력 +{0}%",
                ["mace_desc_stun_boost"] = "기절 확률 +{0}%, 기절 지속시간 +{1}초",
                ["mace_desc_guard_boost"] = "방어력 +{0}",
                ["mace_desc_heavy_strike"] = "공격력 +{0}%",
                ["mace_desc_knockback"] = "밀어내기 확률 {0}%",
                ["mace_desc_tanker"] = "체력 +{0}, 피해 감소 +{1}%",
                ["mace_desc_dps_boost"] = "공격력 +{0}%, 공격속도 +{1}%",
                ["mace_desc_grandmaster"] = "방어력 +{0}",
                ["mace_desc_fury_attack"] = "{0}회 연속 타격",
                ["mace_desc_fury_interval"] = "공격 간격 {0}초, 약 {1}초간 공격",
                ["mace_desc_fury_damage"] = "1~4타 {0}%, 5타(마무리) {1}%",
                ["mace_desc_guardian_buff"] = "{0}초간 방어 버프 활성화",
                ["mace_desc_guardian_reflect"] = "받는 데미지의 {0}%를 공격자에게 반사",
                ["mace_desc_guardian_note"] = "방어 태세로 아군을 보호",
                ["mace_effect_buff"] = "버프 지속시간",
                ["mace_effect_reflect"] = "데미지 반사",
                ["requirement_two_hand_mace"] = "양손 둔기 착용",
                ["requirement_mace_shield"] = "둔기 + 방패 착용",
                ["tooltip_effect"] = "효과",
                ["tooltip_special_note"] = "특별안내",

                // === Spear Skill Names ===
                ["spear_skill_expert"] = "창 전문가",
                ["spear_skill_penetrate"] = "꿰뚫는 창",
                ["spear_skill_combo"] = "연공창",
                ["spear_skill_throw"] = "투창 전문가",
                ["spear_skill_crit"] = "급소 찌르기",
                ["spear_skill_pierce"] = "연격창",
                ["spear_skill_evasion"] = "회피 찌르기",
                ["spear_skill_explosion"] = "폭발창",
                ["spear_skill_dual"] = "이연창",

                // === Spear Skill Descriptions ===
                ["spear_desc_expert"] = "2연속 공격 시 공격 속도 +{0}%, 공격력 +{1}%({2}초 동안)",
                ["spear_desc_throw"] = "창 던지기 공격력 +{0}%",
                ["spear_desc_crit"] = "창 공격력 +{0}%",
                ["spear_desc_pierce"] = "무기 공격력 +{0}",
                ["spear_desc_evasion"] = "구르기 직후 공격 시 피해 +{0}%, 공격 스태미나 -{1}%",
                ["spear_desc_explosion"] = "{0}% 확률로 폭발\n범위 {1}m, 공격력 +{2}%",
                ["spear_desc_dual"] = "2연속 공격 시 {0}초 동안 공격력 +{1}%",
                ["spear_desc_penetrate"] = "{0}초간 번개 충격 모드 활성화, {1}회 연속 적중 시 번개 충격 발동",
                ["spear_desc_penetrate_damage"] = "무기 공격력 +{0}%",
                ["spear_desc_combo"] = "투창을 강화하여 창을 던지고 적과 주변 몬스터를 넉백시킴",
                ["spear_desc_combo_damage"] = "+{0}%",
                ["spear_desc_combo_range"] = "주변 {0}m",
                ["requirement_one_hand_spear"] = "한손 창 착용",

                // === Polearm Skill Names ===
                ["polearm_skill_expert"] = "폴암 전문가",
                ["polearm_skill_king"] = "관통 돌격",
                ["polearm_skill_spin"] = "회전베기",
                ["polearm_skill_suppress"] = "제압 공격",
                ["polearm_skill_hero"] = "영웅 타격",
                ["polearm_skill_area"] = "광역 강타",
                ["polearm_skill_ground"] = "지면 강타",
                ["polearm_skill_moon"] = "반달 베기",
                ["polearm_skill_charge"] = "폴암강화",

                // === Polearm Skill Descriptions ===
                ["polearm_desc_expert"] = "공격 범위 +{0}%",
                ["polearm_desc_spin"] = "특수 공격 공격력 +{0}%",
                ["polearm_desc_suppress"] = "공격력 +{0}%",
                ["polearm_desc_hero"] = "{0}% 확률로 넉백",
                ["polearm_desc_area"] = "2연속 공격 시 공격력 +{0}%({1}초동안)",
                ["polearm_desc_ground"] = "특수 공격 공격력 +{0}%",
                ["polearm_desc_moon"] = "공격 범위 +{0}%, 공격 스태미나 -{1}%",
                ["polearm_desc_charge"] = "무기 공격력 +{0}",
                ["polearm_desc_king"] = "전방 {0}m 돌진, 적 충돌 시 관통 공격",
                ["polearm_desc_king_first"] = "공격력 +{0}%",
                ["polearm_desc_king_aoe"] = "공격력 +{0}% (뒤쪽 {1}°, {2}m)",
                ["polearm_desc_king_knockback"] = "{0}m",
                ["tooltip_first_hit"] = "첫 타격",
                ["tooltip_aoe_knockback"] = "AOE 넉백",
                ["tooltip_knockback_distance"] = "넉백 거리",

                // === Knife Skill Names ===
                ["knife_skill_expert"] = "단검 전문가",
                ["knife_skill_assassin"] = "암살자의 심장",
                ["knife_skill_evasion"] = "회피 숙련",
                ["knife_skill_move_speed"] = "빠른 움직임",
                ["knife_skill_attack_speed"] = "빠른 공격",
                ["knife_skill_crit_rate"] = "치명타 숙련",
                ["knife_skill_combat_damage"] = "치명적 피해",
                ["knife_skill_execution"] = "암살자",
                ["knife_skill_assassination"] = "암살술",

                // === Knife Skill Descriptions ===
                ["knife_desc_expert"] = "적의 뒤에서 공격 시 피해 +{0}%",
                ["knife_desc_evasion"] = "회피 확률 +{0}%",
                ["knife_desc_move_speed"] = "이동속도 +{0}%",
                ["knife_desc_attack_speed"] = "공격력 +{0}",
                ["knife_desc_crit_rate"] = "치명타 확률 +{0}%",
                ["knife_desc_combat_damage"] = "공격력 +{0}%",
                ["knife_desc_execution"] = "치명타 피해 +{0}%, 비틀거림 공격력 +{1}%",
                ["knife_desc_assassination"] = "백스탭 공격력 +{0}%",
                ["knife_desc_assassin_main"] = "{0}m 이내 적의 뒤({1}m)로 순간이동\n대상 {2}초 스턴 + {3}회 연속 공격\n공격 완료 후 원래 위치로 복귀\n{4}초간 피해 +{5}%, 치명타 확률 +{6}%",
                ["knife_desc_assassin_note"] = "암살자의 모든 능력을 극한까지 끌어올리는 궁극기",
                ["knife_desc_assassin_note2"] = "{0}m 이내 적이 없으면 스킬 취소",
                ["requirement_knife_claw"] = "단검 또는 클로 착용",

                // === Staff Skill Names ===
                ["staff_skill_expert"] = "지팡이 전문가",
                ["staff_skill_dual_cast"] = "이중 시전",
                ["staff_skill_heal"] = "힐",

                // === Staff Skill Descriptions ===
                ["staff_desc_dual_cast"] = "{0}발 추가 마법 발사체 발사",
                ["staff_desc_dual_cast_angle"] = "좌 -{0}°, 우 +{0}° 각도로 분산 발사",
                ["staff_desc_dual_cast_damage"] = "지팡이/완드 공격력의 {0}%",
                ["staff_desc_dual_cast_angle_unit"] = "±{0}°",
                ["staff_desc_dual_cast_note"] = "30초간 버프 유지, 다음 마법 공격 시 자동 발동",
                ["requirement_staff_wand"] = "지팡이 또는 완드 착용",
                ["tooltip_dispersion_angle"] = "분산 각도",

                // === Bow Skill Names ===
                ["bow_skill_explosive"] = "폭발 화살",

                // === Crossbow Skill Names ===
                ["crossbow_skill_oneshot"] = "단 한 발",

                // === Job Skill Names ===
                ["job_skill_archer"] = "아처",
                ["job_skill_tanker"] = "탱커",
                ["job_skill_berserker"] = "버서커",
                ["job_skill_rogue"] = "로그",
                ["job_skill_mage"] = "마법사",
                ["job_skill_paladin"] = "성기사",

                // === Job Names ===
                ["job_archer"] = "아처",
                ["job_mage"] = "마법사",
                ["job_tanker"] = "탱커",
                ["job_rogue"] = "로그",
                ["job_paladin"] = "성기사",
                ["job_berserker"] = "광전사",

                // === Weapon Types ===
                ["weapon_sword"] = "검",
                ["weapon_spear"] = "창",
                ["weapon_mace"] = "둔기",
                ["weapon_polearm"] = "폴암",
                ["weapon_knife"] = "단검",
                ["weapon_bow"] = "활",
                ["weapon_crossbow"] = "석궁",
                ["weapon_staff"] = "지팡이",

                // === Stat Names ===
                ["stat_damage"] = "공격력",
                ["stat_health"] = "체력",
                ["stat_stamina"] = "스태미나",
                ["stat_eitr"] = "에이트르",
                ["stat_armor"] = "방어력",
                ["stat_crit_chance"] = "치명타 확률",
                ["stat_crit_damage"] = "치명타 피해",
                ["stat_attack_speed"] = "공격 속도",
                ["stat_move_speed"] = "이동 속도",
                ["stat_dodge"] = "회피",

                // === UI Buttons ===
                ["ui_reset_points"] = "포인트 초기화",
                ["ui_confirm"] = "확인",
                ["ui_cancel"] = "취소",
                ["ui_reset_confirm_title"] = "스킬 초기화 확인",
                ["ui_reset_confirm_message"] = "정말로 모든 스킬을 초기화하시겠습니까?\n이 작업은 되돌릴 수 없습니다.",
                ["ui_music_on"] = "Music On",
                ["ui_music_off"] = "Music Off",

                // === Skill Investment Messages ===
                ["skill_insufficient_points_detail"] = "스킬 포인트가 부족합니다. (필요: {0}, 보유: {1})",
                ["skill_invest_success"] = "✅ 스킬 포인트 투자 확정!",

                // === Additional Tooltip Keys ===
                ["tooltip_none"] = "없음",
                ["tooltip_self"] = "자신",
                ["tooltip_affect_range"] = "영향 범위",
                ["tooltip_passive_effect"] = "패시브 효과",
                ["tooltip_additional"] = "추가",
                ["tooltip_no_player"] = "플레이어 정보 없음",
                ["tooltip_calculation_error"] = "계산 오류",
                ["tooltip_simulation_error"] = "시뮬레이션 오류",
                ["tooltip_load_error"] = "스킬 정보를 불러올 수 없음",
                ["stat_arrow"] = "화살",
                ["item_eikthyr_trophy"] = "에이크쉬르 트로피",
                ["confirmation_job_only"] = "직업은 1개만 선택가능, 레벨 10 이상",
                ["confirmation_job_select_only"] = "직업은 1개만 선택가능",
                ["skill_type_job_active"] = "직업 액티브 스킬 - {0}키",

                // === Archer Job ===
                ["archer_desc_multishot_fallback"] = "5발씩 2회 발사합니다.",
                ["archer_desc_arrow_damage_fallback"] = "화살 1발은 활+화살 공격력의 50%",
                ["archer_passive_skills"] = "점프 높이 +{0}%, 낙사 데미지 -{1}%",
                ["requirement_archer"] = "활 착용, 아처 직업",

                // === Tanker Job ===
                ["tanker_skill_warcry"] = "전장의 함성",
                ["tanker_desc_warcry"] = "{0}m 범위 적을 도발해 {1}초 동안 나를 공격하게 만듭니다.(보스 {2}초), 시전자는 {3}초 동안 피해감소 {4}%",
                ["tanker_desc_warcry_fallback"] = "적을 도발해 5초 동안 나를 공격하게 만듭니다.(보스 1초), 시전자는 피해감소 20%가 5초 지속",
                ["tanker_skill_type_taunt"] = "액티브 도발 스킬 - Y키",
                ["tanker_passive_damage_reduction"] = "받는 피해량 -{0}%",

                // === Berserker Job ===
                ["berserker_skill_rage"] = "버서커 분노",
                ["berserker_desc_rage"] = "{0}초간 분노 상태 발동하여 강력한 공격력 증가",
                ["berserker_desc_rage_detail"] = "체력 손실 1%당 데미지 +{0}%, 최대 +{1}% 한계",
                ["berserker_rage_effect"] = "분노 효과",
                ["berserker_rage_damage_per_health"] = "체력 손실 1%당 데미지 +{0}%",
                ["berserker_passive_desc"] = "스태미나 재생 +20%, 체력 {0}% 이하 시 {1}초간 무적 (쿨타임 {2}분)",
                ["berserker_not_in_rage"] = "분노 상태가 아님",
                ["berserker_current_status"] = "현재 체력: {0}%\n현재 데미지 보너스: +{1}%\n분노 상태: 활성",
                ["berserker_simulation_title"] = "체력별 데미지 보너스 예상:",
                ["berserker_simulation_line"] = "체력 {0}%: +{1}% 데미지",
                ["berserker_simulation_max"] = "최대 한계: +{0}%",
                ["berserker_default_tooltip"] = "설명:\n[액티브 스킬 - Y키]\n20초 동안 체력의 -1% 비례 데미지 +2%, 체력이 적을수록 더 강한 데미지\n\n[패시브 스킬]\n• 스태미나 리젠 +20% (항상 적용)\n• 체력 10% 이하 시 7초간 무적 (쿨타임 300초)\n\n추가: 최대 +200% 데미지 한계, 빨간/황금 오라 효과\n범위: 자신\n소모: 스태미나 20 (액티브만)\n쿨타임: 45초 (액티브), 300초 (패시브)\n필요조건: 직업 버서커\n확인사항: 직업은 1개만 선택가능, Lv 10 이상\n필요 아이템: 고대 바크 스피어",
                ["requirement_job_berserker"] = "직업 버서커",

                // === Rogue Job ===
                ["rogue_desc_shadow_strike"] = "{0}초 은신, 어그로 제거 범위 {1}m",
                ["rogue_desc_attack_bonus"] = "{0}초 공격력 +{1}%",
                ["rogue_passive_desc"] = "은신 스킬 +{0}, 은신 속도 +{1}%, 낙하 데미지 -{2}%",
                ["requirement_rogue"] = "단검 착용, 로그 직업",

                // === Mage Job ===
                ["mage_desc_aoe"] = "범위 공격력 {0}%",
                ["mage_passive_resistance"] = "마법 속성 저항 +{0}%",
                ["requirement_mage"] = "지팡이 착용, 메이지 직업",

                // === Healer/Staff ===
                ["healer_self_heal_included"] = "시전자 포함 치료",
                ["healer_self_heal_excluded"] = "시전자는 치료되지 않음 (다른 플레이어만 치료)",
                ["healer_desc_instant"] = "시전자 중심 {0}m 범위 즉시 힐링",
                ["healer_desc_heal_percent"] = "아군 최대체력의 {0}% 즉시 회복",
                ["healer_healing_effect"] = "힐링 효과",
                ["healer_instant_heal"] = "아군 최대체력의 {0} 즉시 회복",
                ["staff_desc_dual_cast_fallback"] = "추가 마법 발사체 2발 발사 (좌 -5°, 우 +5°)",
                ["staff_desc_dual_cast_damage_fallback"] = "지팡이/완드 공격력의 70%",

                // === Expert Tree Skill Descriptions ===
                // Attack Expert Tree
                ["attack_root_desc"] = "모든 데미지 +{0}%",
                ["atk_melee_bonus_desc"] = "근접 무기 사용 시 {0}% 확률로 +{1}% 추가 피해",
                ["atk_bow_bonus_desc"] = "활 사용 시 {0}% 확률로 +{1}% 추가 피해",
                ["atk_crossbow_bonus_desc"] = "석궁 사용 시 {0}% 확률로 넉백 발생",
                ["atk_staff_bonus_desc"] = "지팡이 사용 시 {0}% 확률로 주변 2마리 적에게 속성 {1}% 추가 피해",
                ["atk_crit_chance_desc"] = "치명타 확률 +{0}%",
                ["atk_melee_crit_desc"] = "한손 근접무기 2연속 공격 시 +{0}% 추가 피해",
                ["atk_crit_dmg_desc"] = "치명타 피해 +{0}%",
                ["atk_twohand_crush_desc"] = "양손 무기 공격력 +{0}%",
                ["atk_staff_mage_desc"] = "지팡이 공격 시 속성 공격 +{0}%",
                ["atk_finisher_melee_desc"] = "근접 3연속 공격 시 +{0}% 추가 피해",

                // Defense Expert Tree - Names
                ["defense_root_name"] = "방어 전문가",
                ["defense_survival_name"] = "피부경화",
                ["defense_dodge_name"] = "심신단련",
                ["defense_health_name"] = "체력단련",
                ["defense_breath_name"] = "단전호흡",
                ["defense_agile_name"] = "회피단련",
                ["defense_boost_name"] = "체력증강",
                ["defense_shield_name"] = "방패훈련",
                ["defense_mental_name"] = "충격파방출",
                ["defense_mental_desc"] = "생명력 45% 이하 시 {0}m 이내 적 {1}초간 기절 (쿨타임 {2}초)",
                ["defense_instant_name"] = "발구르기",
                ["defense_instant_desc"] = "생명력 35% 이하 시 주변 적 {0}m 밀어냄 (쿨타임 {1}초)",
                ["defense_tanker_name"] = "바위피부",
                ["defense_focus_name"] = "지구력",
                ["defense_stamina_name"] = "기민함",
                ["defense_heal_name"] = "트롤의 재생력",
                ["defense_parry_name"] = "막기달인",
                ["defense_mind_name"] = "마인드쉴드",
                ["defense_mind_desc"] = "방어막 유지 +{0}초",
                ["defense_attack_name"] = "신경강화",
                ["defense_double_jump_name"] = "이단점프",
                ["defense_double_jump_desc"] = "공중에서 추가로 {0}회 점프",
                ["defense_body_name"] = "요툰의 생명력",
                ["defense_true_name"] = "요툰의 방패",

                // Defense Expert Tree - Descriptions
                ["defense_root_desc"] = "체력 +{0}, 방어 +{1}",
                ["defense_survival_desc"] = "체력 +{0}, 방어 +{1}",
                ["defense_health_desc"] = "체력 +{0}, 방어 +{1}",
                ["defense_dodge_desc"] = "스태미나 최대치 +{0}, 에이트르 최대치 +{1}",
                ["defense_breath_desc"] = "에이트르 최대치 +{0}",
                ["defense_agile_desc"] = "회피 +{0}%, 구르기 무적시간 +{1}%",
                ["defense_boost_desc"] = "체력 +{0}",
                ["defense_shield_desc"] = "방패 방어력 +{0}",
                ["defense_tanker_desc"] = "방어력 +{0}%",
                ["defense_focus_desc"] = "달리기 스태미나 -{0}%, 점프 스태미나 -{1}%",
                ["defense_stamina_desc"] = "회피 +{0}%, 구르기 스태미나 -{1}%",
                ["defense_heal_desc"] = "{0}초마다 체력 +{1}",
                ["defense_parry_desc"] = "패링 +{0}초, 방패 방어력 +{1}",
                ["defense_attack_desc"] = "회피 +{0}%",
                ["defense_body_desc"] = "체력 최대치 +{0}%, 물리/마법 방어력 +{1}%",
                ["defense_true_desc"] = "블럭 스태미나 -{0}%, 일반 방패 이동속도 +{1}%, 대형 방패 이동속도 +{2}%",

                // Defense Expert Tree - Effect Texts
                ["defense_root_effect"] = "🛡️ 방어 전문가! 체력 +{0}, 방어 +{1}",
                ["defense_shield_effect"] = "🛡️ 방패훈련! 방패 방어력 +{0}",
                ["defense_parry_effect"] = "🛡️ 막기달인! 패링 +{0}초, 방패 방어력 +{1}",
                ["defense_body_effect"] = "🛡️ 요툰의 생명력! 체력 +{0}%, 방어력 +{1}%",

                // Staff Expert Tree
                ["staff_expert_desc"] = "속성 공격 +{0}%",
                ["staff_focus_desc"] = "에이트르 소모 -{0}%",
                ["staff_stream_desc"] = "최대 에이트르 +{0}",
                ["staff_amp_desc"] = "속성 공격 +{0}%",
                ["staff_frost_desc"] = "냉기 공격 +{0}",
                ["staff_fire_desc"] = "화염 공격 +{0}",
                ["staff_lightning_desc"] = "번개 공격 +{0}",
                ["staff_luck_mana_desc"] = "{0}% 확률로 에이트르 소모 없음",

                // Mace Expert Tree (additional)
                ["mace_expert_desc2"] = "둔기 피해 +{0}%, 공격 시 {1}% 확률로 {2}초 기절",
                ["mace_stun_boost_desc2"] = "기절 확률 +{0}%, 지속시간 +{1}초",
                ["mace_guard_boost_desc2"] = "방어 +{0}%",
                ["mace_heavy_strike_desc2"] = "무거운 공격 데미지 +{0}%",
                ["mace_knockback_desc2"] = "공격 시 {0}% 확률로 노크백",
                ["mace_tank_desc2"] = "체력 +{0}%, 받는 데미지 -{1}%",
                ["mace_dps_desc2"] = "공격력 +{0}%, 공격속도 +{1}%",
                ["mace_grandmaster_desc2"] = "방어 +{0}%",

                // === Prerequisite Text ===
                ["prerequisite_label"] = "🔗 필요",
                ["prerequisite_connector_or"] = " 또는 ",
                ["prerequisite_labor_craft"] = "노가다 전문가 + 제작 전문가",

                // === Weapon Requirement Conditions ===
                ["requirement_bow_effect"] = "활 착용",
                ["requirement_crossbow_effect"] = "석궁 착용",
                ["requirement_staff_effect"] = "지팡이 착용",
                ["requirement_sword_effect"] = "검 착용",
                ["requirement_axe_effect"] = "도끼 착용",
                ["requirement_knife_effect"] = "단검 착용",
                ["requirement_spear_effect"] = "창 착용",
                ["requirement_mace_effect"] = "둔기 착용",
                ["requirement_one_hand_melee_effect"] = "한손 근접무기 착용",

                // === MMO Bridge Messages ===
                ["mmo_level_recovery_complete"] = "레벨 데이터 복구 완료!",
                ["mmo_level_recovery_detail"] = "Lv.{0} → Lv.{1}",
                ["mmo_captain_to_epic_complete"] = "Captain -> EpicMMO 마이그레이션 완료!",
                ["mmo_captain_to_epic_detail"] = "Lv.{0} -> Lv.{1}",
                ["mmo_skillpoint_sync_complete"] = "스킬포인트 -> EpicMMO 동기화 완료!",
                ["mmo_skillpoint_sync_detail"] = "Lv.{0} -> Lv.{1}",
                ["mmo_epic_to_captain_complete"] = "EpicMMO -> Captain 데이터 복구 완료!",
                ["mmo_epic_to_captain_detail"] = "Lv.{0}",

                // === Job Skill Messages ===
                ["job_no_job_selected"] = "직업을 선택하지 않았습니다.",
                ["job_one_hand_melee_required"] = "한손 근접무기를 착용해야 합니다!",
                ["job_holy_heal_cooldown"] = "신성한 치유 쿨타임: {0}초",
                ["job_stamina_required"] = "스태미나가 부족합니다 ({0} 필요)",
                ["job_eitr_required"] = "에이트르가 부족합니다 ({0} 필요)",
                ["job_self_heal"] = "✨ 자가 치유: +{0} HP",
                ["job_paladin_heal_success"] = "⭐ 성기사 신성한 치유! (자가치유 + {0}명 지속힐)",
                ["job_continuous_heal_start"] = "🌟 지속 치유 시작!",
                ["job_continuous_heal_tick"] = "💚 지속 치유: +{0} HP",
                ["job_continuous_heal_progress"] = "💚 지속 치유: +{0} HP ({1}/{2})",
                ["job_continuous_heal_complete"] = "✨ 지속 치유 완료!",
                ["job_skill_error"] = "스킬 시전 중 오류가 발생했습니다.",

                // === Rogue Skill Messages ===
                ["rogue_shadow_strike_cooldown"] = "그림자 일격 쿨타임: {0}초",
                ["rogue_stamina_insufficient"] = "스태미나가 부족합니다.",
                ["rogue_shadow_strike_success"] = "🗡️ 그림자 일격! (어그로 제거 + {0}초간 데미지 +{1}%)",
                ["rogue_shadow_strike_end"] = "그림자 일격 종료",

                // === Mage Skill Messages ===
                ["mage_explosion_cooldown"] = "마법 폭발 쿨타임: {0}초",
                ["mage_eitr_insufficient"] = "에이트르가 부족합니다.",
                ["mage_explosion_success"] = "🔮 마법 폭발! ({0}초간 마법 데미지 +{1}%, 범위 +{2}m)",
                ["mage_explosion_end"] = "마법 폭발 종료",

                // === Archer Skill Messages ===
                ["archer_multishot_cooldown"] = "멀티샷 쿨타임: {0}초",
                ["archer_stamina_insufficient"] = "스태미나 부족",
                ["archer_multishot_success"] = "🏹 멀티샷! ({0}발 발사)",
                ["archer_bow_required"] = "활을 착용해야 합니다!",
                ["archer_no_arrow"] = "화살이 없습니다!",
                ["archer_job_required"] = "아처 직업이 필요합니다!",
                ["archer_cooldown"] = "쿨다운: {0}초",

                // === Berserker Skill Messages ===
                ["berserker_rage_end"] = "버서커의 분노 종료",

                // === Level Up Message ===
                ["level_up"] = "LEVEL UP!",

                // === Speed Expert Tree - Skill Names ===
                ["speed_root_name"] = "속도 전문가",
                ["speed_base_name"] = "민첩함의 기초",
                ["melee_combo_name"] = "연속의 흐름",
                ["crossbow_reload2_name"] = "석궁 숙련자",
                ["bow_speed2_name"] = "활 숙련자",
                ["moving_cast_name"] = "이동 시전",
                ["speed_ex1_name"] = "수련자1",
                ["speed_ex2_name"] = "수련자2",
                ["speed_master_name"] = "에너자이져",
                ["ship_master_name"] = "선 장",
                ["agility_peak_name"] = "점프 숙련자",
                ["speed_1_name"] = "민첩",
                ["speed_2_name"] = "지구력",
                ["speed_3_name"] = "지능",
                ["all_master_name"] = "숙련자",
                ["melee_speed1_name"] = "근접 가속",
                ["crossbow_draw1_name"] = "석궁 가속",
                ["bow_draw1_name"] = "활 가속",
                ["staff_speed1_name"] = "시전 가속",

                // === Speed Expert Tree - Skill Descriptions ===
                ["speed_root_desc"] = "이동속도 +{0}%",
                ["speed_base_desc"] = "공격속도 +{0}%, 구르기 후 {1}초간 이동속도 +{2}%",
                ["melee_combo_desc"] = "근접 2연속 적중 시 {0}초간 공격속도 +{1}%, 스태미나 -{2}%",
                ["crossbow_reload2_desc"] = "석궁 적중 시 이동속도 +{0}%({1}초), 버프 중 재장전 +{2}%",
                ["bow_speed2_desc"] = "활 2연속 적중 시 스태미나 -{0}%, 다음 장전 +{1}%",
                ["moving_cast_desc"] = "마법 시전 중 이동속도 +{0}%, 에이트르 소모 -{1}%",
                ["speed_ex1_desc"] = "근접무기 숙련 +{0}, 석궁 숙련 +{1}",
                ["speed_ex2_desc"] = "마법 숙련 +{0}, 활 숙련 +{1}",
                ["speed_master_desc"] = "음식 소모 속도 -{0}%",
                ["ship_master_desc"] = "배 운전시 속도 +{0}%",
                ["agility_peak_desc"] = "점프 숙련 +{0}, 점프 스태미나 -{1}%",
                ["speed_1_desc"] = "근접 공격속도 +{0}%, 이동속도 +{1}%",
                ["speed_2_desc"] = "스태미나 최대치 +{0}",
                ["speed_3_desc"] = "에이트르 최대치 +{0}",
                ["all_master_desc"] = "달리기 숙련 +{0}, 점프 숙련 +{1}",
                ["melee_speed1_desc"] = "근접 공격속도 +{0}%, 3연속 적중 시 다음 공격속도 +{1}%",
                ["crossbow_draw1_desc"] = "석궁 재장전 +{0}%, 재장전 중 이동속도 +{1}%",
                ["bow_draw1_desc"] = "활 장전 +{0}%, 장전 중 이동속도 +{1}%",
                ["staff_speed1_desc"] = "마법 공격속도 +{0}%, 3연속 적중 시 에이트르 최대치의 {1}% 회복",

                // === Speed Expert Tree - Effect Texts ===
                ["speed_root_effect"] = "🏃 속도 전문가 투자 완료! (+{0}% 이동속도)",
                ["speed_base_effect"] = "🏃 민첩함의 기초 습득!\n공격속도 +{0}%\n구르기 후 이동속도 +{1}%",
                ["agility_peak_effect"] = "🦘 점프 숙련자 습득! 점프 +{0} (레벨: {1:F0} → {2:F0})",

                // === Attack Expert Tree - Skill Names ===
                ["attack_root_name"] = "공격 전문가",
                ["atk_base_name"] = "기본 공격",
                ["atk_melee_bonus_name"] = "근접 특화",
                ["atk_bow_bonus_name"] = "활 특화",
                ["atk_crossbow_bonus_name"] = "석궁 특화",
                ["atk_staff_bonus_name"] = "지팡이 특화",
                ["atk_twohand_drain_name"] = "공격 증가",
                ["atk_melee_crit_name"] = "근접 강화",
                ["atk_crit_chance_name"] = "정밀 공격",
                ["atk_ranged_enhance_name"] = "원거리 강화",
                ["atk_special_name"] = "특수화 스탯",
                ["atk_crit_dmg_name"] = "약점 공격",
                ["atk_finisher_melee_name"] = "연속 근접의 대가",
                ["atk_twohand_crush_name"] = "양손 분쇄",
                ["atk_staff_mage_name"] = "속성 공격",

                // === Attack Expert Tree - Skill Descriptions ===
                ["atk_base_desc"] = "물리 공격력 +{0}, 속성 공격력 +{1}",
                ["atk_twohand_drain_desc"] = "물리 공격력 +{0}, 속성 공격력 +{1}",
                ["atk_ranged_enhance_desc"] = "원거리 무기 공격력 +{0}% (석궁, 활, 지팡이)",
                ["atk_special_desc"] = "치명타 확률 +{0}%",

                // === Attack Expert Tree - Effect Texts ===
                ["atk_base_effect"] = "💪 기본 공격 습득!",
                ["atk_melee_bonus_effect"] = "⚔️ 근접 특화 습득!",
                ["atk_twohand_drain_effect"] = "💪 공격 증가!",
                ["atk_ranged_enhance_effect"] = "🏹 원거리 강화!",
                ["atk_special_effect"] = "⭐ 특수화 스탯! 치명타 확률 +{0}%",
                ["atk_staff_mage_effect"] = "🔥 속성 공격!",

                // === Paladin Skill ===
                ["paladin_name"] = "성기사",
                ["paladin_desc"] = "신성한 치유: 자신 및 {0}m 범위 아군 치유",
                ["paladin_passive_desc"] = "한손 근접무기 데미지 +{0}%, 자신 및 주변 아군 힐링 (Y키)",
                ["paladin_tooltip_desc"] = "범위 아군 체력 {0}%/{1}초 ({2}초), 자가 {3}% 즉시",
                ["paladin_passive_resistance"] = "물리 및 속성 저항 -{0}%",
                ["paladin_requirement"] = "한손 근접무기 착용, 성기사 직업",
                ["paladin_required_item"] = "에이크쉬르 트로피",
                ["paladin_tooltip_error"] = "성기사 스킬 정보를 불러올 수 없음",

                // === Common Tooltip Labels ===
                ["tooltip_description"] = "설명",
                ["tooltip_range"] = "범위",
                ["tooltip_cost"] = "소모",
                ["tooltip_stamina"] = "스태미나",
                ["tooltip_eitr"] = "에이트르",
                ["tooltip_skill_type"] = "스킬유형",
                ["tooltip_job_active_y"] = "직업 액티브 스킬 - Y키",
                ["tooltip_passive"] = "패시브",
                ["tooltip_cooldown"] = "쿨타임",
                ["tooltip_seconds"] = "초",
                ["tooltip_requirement"] = "필요조건",
                ["tooltip_confirmation"] = "확인사항",
                ["tooltip_job_limit"] = "직업은 1개만 선택가능, 레벨 10 이상",
                ["tooltip_required_item"] = "필요 아이템",

                // === Melee Root Skill ===
                ["melee_skill_expert"] = "근접 전문가",
                ["melee_desc_expert"] = "근접무기 공격력 +3 (고정값)",
                ["melee_root_effect"] = "⚔️ 근접 전문가 습득! 근접무기 공격력 +3",

                // === Ranged Root Skill ===
                ["ranged_skill_expert"] = "원거리 전문가",
                ["ranged_desc_expert"] = "활/석궁 관통 +2, 지팡이/완드 화염 공격 +2",

                // === Production Skills ===
                ["production_skill_expert"] = "생산 전문가",
                ["production_desc_expert"] = "50% 확률로 나무+1",
                ["novice_worker_name"] = "초보 일꾼",
                ["novice_worker_desc"] = "25% 확률로 나무+1",
                ["woodcutting_lv2_name"] = "벌목 Lv2",
                ["woodcutting_lv2_desc"] = "25% 확률로 나무+1",
                ["woodcutting_lv3_name"] = "벌목 Lv3",
                ["woodcutting_lv3_desc"] = "25% 확률로 나무+1",
                ["woodcutting_lv4_name"] = "벌목 Lv4",
                ["woodcutting_lv4_desc"] = "25% 확률로 나무+1",
                ["gathering_lv2_name"] = "채집 Lv2",
                ["gathering_lv2_desc"] = "25% 확률로 채집(나무제외)+1",
                ["gathering_lv3_name"] = "채집 Lv3",
                ["gathering_lv3_desc"] = "25% 확률로 채집(나무제외)+1",
                ["gathering_lv4_name"] = "채집 Lv4",
                ["gathering_lv4_desc"] = "채집류 200개 보유 시 25% 확률로 채집+1",
                ["mining_lv2_name"] = "채광 Lv2",
                ["mining_lv2_desc"] = "25% 확률로 광석+1",
                ["mining_lv3_name"] = "채광 Lv3",
                ["mining_lv3_desc"] = "25% 확률로 광석+1",
                ["mining_lv4_name"] = "채광 Lv4",
                ["mining_lv4_desc"] = "25% 확률로 광석+1",
                ["crafting_lv2_name"] = "제작 Lv2",
                ["crafting_lv2_desc"] = "25% 확률로 강화+1, 내구도 최대치 25% 증가",
                ["crafting_lv3_name"] = "제작 Lv3",
                ["crafting_lv3_desc"] = "25% 확률로 강화+1, 내구도 최대치 25% 증가",
                ["crafting_lv4_name"] = "제작 Lv4",
                ["crafting_lv4_desc"] = "25% 확률로 강화+1, 내구도 최대치 25% 증가",

                // === Production Skills - Effect Texts ===
                ["production_root_effect"] = "🌲 생산 전문가 습득!",
                ["novice_worker_effect"] = "🔧 초보 일꾼 습득!",
                ["woodcutting_lv2_effect"] = "🪓 벌목 Lv2 습득!",
                ["woodcutting_lv3_effect"] = "🪓 벌목 Lv3 습득!",
                ["woodcutting_lv4_effect"] = "🪓 벌목 Lv4 습득! 나무 400개 보유 시 추가 획득!",
                ["gathering_lv2_effect"] = "🍄 채집 Lv2 습득!",
                ["gathering_lv3_effect"] = "🍄 채집 Lv3 습득!",
                ["gathering_lv4_effect"] = "🍄 채집 Lv4 습득! 채집 200개 보유 시 추가 획득!",
                ["mining_lv2_effect"] = "⛏️ 채광 Lv2 습득!",
                ["mining_lv3_effect"] = "⛏️ 채광 Lv3 습득!",
                ["mining_lv4_effect"] = "⛏️ 채광 Lv4 습득! 은 25개 소모 시 광석 추가 획득!",
                ["crafting_lv2_effect"] = "🔨 제작 Lv2 습득!",
                ["crafting_lv3_effect"] = "🔨 제작 Lv3 습득!",
                ["crafting_lv4_effect"] = "🔨 제작 Lv4 습득! 은 검+헬멧 보유 시 제작 강화 효과!",

                // === Crossbow Skills ===
                ["crossbow_skill_expert"] = "석궁 전문가",
                ["crossbow_rapid_fire_lv1_name"] = "연속 발사 Lv1",
                ["crossbow_balance_name"] = "균형 조준",
                ["crossbow_rapid_name"] = "고속 장전",
                ["crossbow_mark_name"] = "정직한 한 발",
                ["crossbow_auto_reload_name"] = "자동 장전",
                ["crossbow_rapid_fire_lv2_name"] = "연속 발사 Lv2",
                ["crossbow_final_strike_name"] = "결전의 일격",
                ["crossbow_oneshot_name"] = "단 한 발",

                // === Bow Skills ===
                ["bow_skill_expert"] = "활 전문가",
                ["bow_focus_name"] = "집중 사격",
                ["bow_multishot_lv1_name"] = "멀티샷 Lv1",
                ["bow_proficiency_name"] = "활 숙련",
                ["bow_basic_attack_name"] = "기본 활공격",
                ["bow_multishot_lv2_name"] = "멀티샷 Lv2",
                ["bow_instinct_name"] = "사냥 본능",
                ["bow_precision_name"] = "정조준",
                ["bow_explosive_name"] = "폭발 화살",

                // === Staff Skills ===
                ["staff_skill_expert_name"] = "지팡이 전문가",
                ["staff_focus_name"] = "정신 집중",
                ["staff_stream_name"] = "마법 흐름",
                ["staff_amp_name"] = "마법 증폭",
                ["staff_frost_name"] = "냉기 속성",
                ["staff_fire_name"] = "화염 속성",
                ["staff_lightning_name"] = "번개 속성",
                ["staff_luck_mana_name"] = "행운 마력",
                ["staff_dual_cast_name"] = "이중시전",
                ["staff_heal_name"] = "힐",

                // === Ranged/Crossbow/Bow Descriptions ===
                ["ranged_root_desc"] = "활/석궁 관통 +2, 지팡이/완드 화염 공격 +2",
                ["weapon_effect_note"] = "※ {0} 착용시 효과발동",
                ["crossbow_expert_desc"] = "석궁 공격력 +{0}%",
                ["crossbow_rapid_fire_desc"] = "{0}% 확률로 {1}발 연속 발사 (각 {2}% 데미지, 볼트 {3}발 소모)",
                ["crossbow_balance_desc"] = "명중 시 {0}% 확률로 넉백 ({1}m)",
                ["crossbow_rapid_desc"] = "장전속도 +{0}%",
                ["crossbow_mark_desc"] = "치명타 확률 0% 고정, 대신 석궁 공격력 +{0}%",
                ["crossbow_auto_reload_desc"] = "명중 시 {0}% 확률로 다음 1회 장전 속도 200%",
                ["crossbow_final_desc"] = "크리티컬 데미지 +{0}%",
                ["crossbow_oneshot_desc"] = "석궁 데미지 +{0}%, 다음 사격까지 {1}초 장전 지연",
                ["bow_expert_desc"] = "활 공격력 +{0}%",
                ["bow_focus_desc"] = "치명타 확률 +{0}%",
                ["bow_multishot_lv1_desc"] = "{0}% 확률로 추가 화살 1발 발사 (+3도 각도)",
                ["bow_proficiency_desc"] = "활 기술(숙련도) +{0}",
                ["bow_proficiency_note"] = "※ 사망해도 보너스 유지",
                ["bow_basic_attack_desc"] = "활 공격력 +{0}",
                ["bow_multishot_lv2_desc"] = "{0}% 확률로 추가 화살 2발 발사 (+3도 각도)",
                ["bow_instinct_desc"] = "치명타 확률 +{0}%",
                ["bow_precision_desc"] = "크리티컬 데미지 +{0}%",

                // === Crossbow Additional Descriptions ===
                ["crossbow_rapid_fire_lv2_desc"] = "{0}% 확률로 {1}발 연속 발사 (각 {2}% 데미지, 볼트 {3}발 소모)",
                ["crossbow_rapid_fire_lv2_note"] = "※ Lv1과 확률 합산",
                ["crossbow_final_strike_desc"] = "체력 {0}% 이상 적에게 추가 {1}% 피해",

                // === Staff Full Descriptions ===
                ["staff_expert_full_desc"] = "지팡이 속성 공격력 +{0}% 증가\n지팡이와 완드 사용법을 익혀 마법 공격력을 높입니다\n필요조건: 지팡이 또는 완드 착용",
                ["staff_focus_full_desc"] = "지팡이 사용 시 에이트르 소모량 -{0}% 감소\n정신력 집중으로 마나 효율성을 높입니다\n필요조건: 지팡이 또는 완드 착용",
                ["staff_stream_full_desc"] = "최대 에이트르 +{0} 증가\n마법의 흐름을 익혀 더 많은 마나를 보유할 수 있습니다\n필요조건: 지팡이 또는 완드 착용",
                ["staff_amp_full_desc"] = "지팡이 마법 공격력 +{0}% 증가\n마법력을 증폭하여 더 강력한 주문을 구사합니다\n필요조건: 지팡이 또는 완드 착용",
                ["staff_frost_full_desc"] = "냉기 공격 +{0}\n필요조건: 지팡이 또는 완드 착용",
                ["staff_fire_full_desc"] = "화염 공격 +{0}\n필요조건: 지팡이 또는 완드 착용",
                ["staff_lightning_full_desc"] = "번개 공격 +{0}\n필요조건: 지팡이 또는 완드 착용",
                ["staff_luck_mana_full_desc"] = "{0}% 확률로 에이트르 소모 없음\n행운의 마력이 깃들어 공짜로 마법을 시전합니다\n필요조건: 지팡이 또는 완드 착용",
                ["staff_dual_cast_full_desc"] = "R키로 추가 마법 발사체 {0}발 발사 (좌 -{1:F0}°, 우 +{1:F0}°)\n발사체 데미지: 지팡이/완드 공격력의 {2:F0}%\n소모: Eitr {3:F0}\n스킬유형: 액티브 R키\n무기타입: 지팡이\n쿨타임: {4:F0}초\n필요조건: 지팡이 또는 완드 착용",
                ["staff_heal_full_desc"] = "H키로 시전자 중심 {0:F0}m 범위 즉시 힐링\n아군 최대체력의 {1}% 즉시 회복\n소모: Eitr {2:F0}\n스킬유형: 액티브 H키\n무기타입: 지팡이\n쿨타임: {3:F0}초\n필요조건: 지팡이 또는 완드 착용",

                // === Ranged Skills - Effect Texts ===
                ["ranged_root_effect"] = "🏹 원거리 전문가 습득!",
                ["bow_multishot_lv1_effect"] = "🏹 멀티샷 Lv1 습득!",
                ["bow_multishot_lv2_effect"] = "🏹🏹 멀티샷 Lv2 습득!",
                ["bow_proficiency_effect"] = "🏹 활 숙련 습득! 활 숙련도 +{0}",
                ["bow_explosive_desc"] = "폭발 화살 발사\n데미지: 활 공격력의 {0}%\n스태미나 {1}% 소모\n쿨타임: {2}초"
            };
        }

        /// <summary>
        /// Get English translations
        /// </summary>
        public static Dictionary<string, string> GetEnglish()
        {
            return new Dictionary<string, string>
            {
                // === Common Messages ===
                ["cooldown_remaining"] = "{0} on cooldown! Remaining: {1}s",
                ["skill_cooldown"] = "{0} skill cooldown: {1}s remaining",
                ["stamina_insufficient"] = "Not enough stamina",
                ["eitr_insufficient"] = "Not enough Eitr! (Required: {0})",
                ["no_enemies_in_range"] = "No enemies in range!",
                ["no_targets_in_range"] = "No enemies in range!",

                // === Archer Passive ===
                ["archer_jump_bonus"] = "Archer Passive: Jump height +{0}%",
                ["archer_fall_damage_reduced"] = "Archer Passive: -{0} fall damage!",

                // === Mage Skills ===
                ["mage_cooldown"] = "Mage skill cooldown: {0}s remaining",
                ["staff_required"] = "You must equip a staff!",
                ["mage_spell_cast"] = "Mage spell cast! {0} targets marked!",
                ["mage_explosion_damage"] = "{0} exploded! {1} damage!",
                ["mage_elemental_resist"] = "Mage Passive: -{0} elemental damage!",

                // === Rogue Skills ===
                ["rogue_shadow_strike_cooldown"] = "Shadow Strike on cooldown! Remaining: {0}s",
                ["rogue_dagger_required"] = "Dagger or Claw required",
                ["rogue_shadow_strike_success"] = "Shadow Strike! {0} enemies lost aggro!",
                ["rogue_shadow_strike_no_enemy"] = "Shadow Strike! (No enemies nearby)",
                ["rogue_stealth_start"] = "Stealth started! ({0}s)",
                ["rogue_stealth_end"] = "Stealth ended! (Reason: {0})",
                ["rogue_buff_end"] = "Shadow Strike buff ended!",
                ["rogue_smoke"] = "Smoke!",
                ["rogue_passive_fall_damage"] = "Rogue Passive: -{0} fall damage!",

                // === Tanker Skills ===
                ["tanker_taunt_cooldown"] = "Taunt on cooldown! Remaining: {0}s",
                ["tanker_shield_required"] = "Shield required",
                ["tanker_taunt_success"] = "Taunt! {0} enemies targeting you!",
                ["tanker_taunt_no_enemy"] = "No enemies to taunt nearby",
                ["tanker_passive_damage_reduction"] = "Tanker Passive: -{0} damage!",

                // === Berserker Skills ===
                ["berserker_rage_cooldown"] = "Rage on cooldown! Remaining: {0}s",
                ["berserker_rage_active"] = "Rage activated! Attack +{0}%!",
                ["berserker_rage_end"] = "Rage buff ended!",
                ["berserker_passive_lifesteal"] = "Berserker Passive: Lifesteal +{0}!",

                // === Paladin Skills ===
                ["paladin_cooldown"] = "Paladin skill on cooldown! Remaining: {0}s",
                ["paladin_weapon_required"] = "One-handed melee weapon required",
                ["paladin_heal_cast"] = "Paladin's Blessing! {0} healed!",
                ["paladin_heal_amount"] = "Paladin's Blessing: +{0} HP!",

                // === Sword Skills ===
                ["sword_dash_cooldown"] = "Dash Slash on cooldown! Remaining: {0}s",
                ["sword_required"] = "Sword required",
                ["sword_dash_success"] = "Dash Slash! {0} damage!",
                ["sword_parry_cooldown"] = "Parry Rush on cooldown! Remaining: {0}s",
                ["sword_parry_success"] = "Parry Rush! Defense +{0}%!",

                // === Spear Skills ===
                ["spear_penetrate_cooldown"] = "Penetrate on cooldown! Remaining: {0}s",
                ["spear_required"] = "Spear required",
                ["spear_penetrate_success"] = "Penetrate! {0} damage!",
                ["spear_combo_cooldown"] = "Combo Spear on cooldown! Remaining: {0}s",
                ["spear_combo_success"] = "Combo Spear! {0} consecutive hits!",

                // === Mace Skills ===
                ["mace_guardian_cooldown"] = "Guardian's Heart on cooldown! Remaining: {0}s",
                ["mace_required"] = "Mace required",
                ["mace_guardian_success"] = "Guardian's Heart! Defense +{0}%!",
                ["mace_fury_cooldown"] = "Fury Hammer on cooldown! Remaining: {0}s",
                ["mace_fury_success"] = "Fury Hammer! {0} damage!",

                // === Polearm Skills ===
                ["polearm_king_cooldown"] = "Spear King on cooldown! Remaining: {0}s",
                ["polearm_required"] = "Polearm required",
                ["polearm_king_success"] = "Spear King! {0} damage!",

                // === Knife Skills ===
                ["knife_assassin_cooldown"] = "Assassin's Heart on cooldown! Remaining: {0}s",
                ["knife_required"] = "Knife required",
                ["knife_assassin_success"] = "Assassin's Heart! Crit damage +{0}%!",

                // === Bow Skills ===
                ["bow_explosive_cooldown"] = "Explosive Arrow on cooldown! Remaining: {0}s",
                ["bow_required"] = "Bow required",
                ["bow_explosive_success"] = "Explosive Arrow! {0} damage!",

                // === Crossbow Skills ===
                ["crossbow_oneshot_cooldown"] = "One Shot on cooldown! Remaining: {0}s",
                ["crossbow_required"] = "Crossbow required",
                ["crossbow_oneshot_success"] = "One Shot! {0} damage!",

                // === Staff Skills ===
                ["staff_dual_cast_cooldown"] = "Dual Cast on cooldown! Remaining: {0}s",
                ["staff_required"] = "Staff required",
                ["staff_dual_cast_success"] = "Dual Cast! Double magic projectile!",
                ["staff_heal_cooldown"] = "Heal on cooldown! Remaining: {0}s",
                ["staff_heal_success"] = "Heal! +{0} HP!",

                // === Job Active Skills (Y Key) ===
                ["job_skill_activated"] = "{0} skill activated!",
                ["job_skill_no_job"] = "You must select a job first",

                // === Stealth System ===
                ["stealth_reason_attack"] = "Attack",
                ["stealth_reason_damage"] = "Damage taken",
                ["stealth_reason_expired"] = "Time expired",

                // === General UI ===
                ["skill_points"] = "Skill Points",
                ["reset_button"] = "Reset",
                ["skill_learned"] = "{0} skill learned!",
                ["skill_already_learned"] = "Skill already learned",
                ["skill_prerequisites_not_met"] = "Prerequisites not met",
                ["skill_insufficient_points"] = "Not enough skill points",
                ["skill_level_required"] = "Level {0} required",

                // === Skill Types ===
                ["skill_type_passive"] = "Passive Skill",
                ["skill_type_active_y"] = "Active Skill (Y Key)",
                ["skill_type_active_r"] = "Active Skill (R Key)",
                ["skill_type_active_g"] = "Active Skill (G Key)",
                ["skill_type_active_h"] = "Active Skill (H Key)",
                ["skill_type_job_active"] = "Job Active Skill (Y Key)",

                // === Tooltip Labels ===
                ["tooltip_description"] = "Description",
                ["tooltip_range"] = "Range",
                ["tooltip_cost"] = "Cost",
                ["tooltip_skill_type"] = "Skill Type",
                ["tooltip_cooldown"] = "Cooldown",
                ["tooltip_requirements"] = "Requirements",
                ["tooltip_notice"] = "Notice",
                ["tooltip_required_points"] = "Required Points",
                ["tooltip_learned"] = "Learned",
                ["tooltip_damage"] = "Damage",
                ["tooltip_passive"] = "Passive",
                ["tooltip_required_item"] = "Required Item",
                ["tooltip_error"] = "Tooltip generation error",
                ["tooltip_unknown_skill"] = "Unknown {0} skill",

                // === Skill Types Extended ===
                ["skill_type_active"] = "Active Skill",
                ["skill_type_active_key"] = "Active Skill - {0} Key",

                // === Weapon Requirements ===
                ["requirement_sword_equip"] = "Sword equipped",
                ["requirement_spear_equip"] = "Spear equipped",
                ["requirement_mace_equip"] = "Mace equipped",
                ["requirement_polearm_equip"] = "Polearm equipped",
                ["requirement_knife_equip"] = "Knife equipped",
                ["requirement_bow_equip"] = "Bow equipped",
                ["requirement_crossbow_equip"] = "Crossbow equipped",
                ["requirement_staff_equip"] = "Staff equipped",
                ["requirement_weapon_equip"] = "Weapon equipped",
                ["requirement_shield_equip"] = "Shield equipped",
                ["requirement_one_hand_melee"] = "One-handed melee weapon equipped",

                // === Units ===
                ["unit_seconds"] = "s",
                ["unit_meter"] = "m",
                ["unit_arrow"] = "Arrow",
                ["unit_pieces"] = "pcs",

                // === Item Names ===
                ["item_wood"] = "Wood",
                ["item_raspberry"] = "Raspberry",
                ["item_mushroom"] = "Mushroom",
                ["item_blueberries"] = "Blueberries",
                ["item_copper_ore"] = "Copper Ore",
                ["item_iron_ore"] = "Iron Ore",
                ["item_silver_ore"] = "Silver Ore",
                ["item_bronze_sword"] = "Bronze Sword",
                ["item_bronze_helmet"] = "Bronze Helmet",
                ["item_iron_sword"] = "Iron Sword",
                ["item_iron_helmet"] = "Iron Helmet",
                ["item_silver_sword"] = "Silver Sword",
                ["item_drake_helmet"] = "Drake Helmet",
                ["item_copper_knife"] = "Copper Knife",
                ["item_crude_bow"] = "Crude Bow",
                ["item_spear"] = "Spear",

                // === Item Requirement Formats ===
                ["item_consumed"] = "(consumed)",
                ["item_quantity_required"] = "{0} x{1} in inventory",
                ["item_equip_required"] = "{0} equipped",
                ["item_equip_consume"] = "{0} equipped(consumed)",

                // === Tooltip Format Strings ===
                ["seconds_format"] = "{0}s",
                ["stamina_format"] = "Stamina {0}",
                ["stamina_percent_format"] = "Stamina {0}%",
                ["attack_power_bonus_format"] = "Attack power +{0}%",
                ["knockback_format"] = "Knockback {0}m",
                ["bow_explosive_tooltip_desc"] = "Fire explosive arrow, deals explosion damage to nearby enemies on hit",
                ["bow_explosive_damage_format"] = "Explosion damage {0}% of attack power",
                ["bow_explosive_range_format"] = "Explosion radius {0}m",
                ["crossbow_oneshot_tooltip_desc"] = "Buff active for {0}s, next crossbow shot deals powerful strike",

                // === Common Tooltip Text ===
                ["tooltip_job_limit"] = "Only one job can be selected",
                ["tooltip_level_required"] = "Level {0} or higher",
                ["tooltip_same_weapon_only"] = "Multiple skills only within same weapon tree",
                ["tooltip_not_invincible"] = "Not invincible during skill use",
                ["tooltip_eikthyr_trophy"] = "Eikthyr Trophy",

                // === Sword Skill Names ===
                ["sword_skill_expert"] = "Sword Expert",
                ["sword_skill_rush_slash"] = "Rush Slash",
                ["sword_skill_fast_slash"] = "Fast Slash",
                ["sword_skill_counter"] = "Counter Stance",
                ["sword_skill_combo"] = "Combo Slash",
                ["sword_skill_riposte"] = "Riposte",
                ["sword_skill_all_in_one"] = "Attack & Defense",
                ["sword_skill_duel"] = "True Duel",
                ["sword_skill_parry_rush"] = "Parry Rush",
                ["sword_skill_ultimate"] = "Ultimate Slash",

                // === Sword Skill Descriptions ===
                ["sword_desc_expert"] = "Sword damage +{0}%\n2-hit combo damage +{1}% ({2}s)",
                ["sword_desc_rush_slash"] = "Dash {0}m forward, then slash 3 times while circling the monster",
                ["sword_desc_rush_slash_1st"] = "1st slash: {0}% damage",
                ["sword_desc_rush_slash_2nd"] = "2nd slash: {0}% damage",
                ["sword_desc_rush_slash_3rd"] = "3rd slash (Finisher): {0}% damage",
                ["sword_desc_fast_slash"] = "Attack speed +{0}%",
                ["sword_desc_counter"] = "Defense +{1}% for {0}s after successful parry",
                ["sword_desc_combo"] = "3-hit combo damage +{0}% ({1}s)",
                ["sword_desc_riposte"] = "Attack power +{0}",
                ["sword_desc_all_in_one"] = "Two-handed: Attack +{0}%, Defense +{1}%",
                ["sword_desc_duel"] = "Attack speed +{0}%",
                ["sword_desc_parry_rush"] = "Shield charge on successful parry for {0}s",
                ["sword_desc_parry_rush_damage"] = "Damage +{0}%",
                ["sword_desc_parry_rush_push"] = "Knockback {0}m",
                ["sword_desc_ultimate"] = "All sword skill effects +{0}%",

                // === Sword Additional Tooltips ===
                ["tooltip_sword_effect_note"] = "※ Effect activates when using sword",
                ["tooltip_sword_damage_boost"] = "Sword damage boost",
                ["tooltip_skill_not_found"] = "{0} skill info not found",
                ["tooltip_generation_error"] = "Tooltip generation error",

                // === Archer Skill ===
                ["archer_skill_multishot"] = "Multi-Shot",
                ["archer_desc_multishot"] = "Fire {0} arrows x{1} times, each arrow deals {2}% of bow+arrow damage",
                ["archer_desc_passive"] = "Jump height +{0}%, Fall damage -{1}%",
                ["archer_range_arrows"] = "Fires {0} arrows",

                // === Mace Skill Names ===
                ["mace_skill_expert"] = "Mace Expert",
                ["mace_skill_guardian"] = "Guardian's Heart",
                ["mace_skill_fury"] = "Fury Hammer",
                ["mace_skill_damage_boost"] = "Mace Enhancement",
                ["mace_skill_stun_boost"] = "Stun Enhancement",
                ["mace_skill_guard_boost"] = "Guard Enhancement",
                ["mace_skill_heavy_strike"] = "Heavy Strike",
                ["mace_skill_knockback"] = "Knockback",
                ["mace_skill_tanker"] = "Tanker",
                ["mace_skill_dps_boost"] = "DPS Enhancement",
                ["mace_skill_grandmaster"] = "Grandmaster",

                // === Mace Skill Descriptions ===
                ["mace_desc_expert"] = "Damage +{0}%, Stun chance +{1}%, Stun duration +{2}s",
                ["mace_desc_damage_boost"] = "Mace damage +{0}%",
                ["mace_desc_stun_boost"] = "Stun chance +{0}%, Stun duration +{1}s",
                ["mace_desc_guard_boost"] = "Defense +{0}",
                ["mace_desc_heavy_strike"] = "Damage +{0}%",
                ["mace_desc_knockback"] = "Knockback chance {0}%",
                ["mace_desc_tanker"] = "Health +{0}, Damage reduction +{1}%",
                ["mace_desc_dps_boost"] = "Damage +{0}%, Attack speed +{1}%",
                ["mace_desc_grandmaster"] = "Defense +{0}",
                ["mace_desc_fury_attack"] = "{0} consecutive strikes",
                ["mace_desc_fury_interval"] = "Attack interval {0}s, about {1}s total",
                ["mace_desc_fury_damage"] = "Hit 1-4: {0}%, Hit 5 (Finisher): {1}%",
                ["mace_desc_guardian_buff"] = "Activate defense buff for {0}s",
                ["mace_desc_guardian_reflect"] = "Reflect {0}% of damage taken to attacker",
                ["mace_desc_guardian_note"] = "Protect allies with defensive stance",
                ["mace_effect_buff"] = "Buff duration",
                ["mace_effect_reflect"] = "Damage reflect",
                ["requirement_two_hand_mace"] = "Two-handed mace equipped",
                ["requirement_mace_shield"] = "Mace + Shield equipped",
                ["tooltip_effect"] = "Effect",
                ["tooltip_special_note"] = "Special Note",

                // === Spear Skill Names ===
                ["spear_skill_expert"] = "Spear Expert",
                ["spear_skill_penetrate"] = "Piercing Spear",
                ["spear_skill_combo"] = "Combo Spear",
                ["spear_skill_throw"] = "Throw Expert",
                ["spear_skill_crit"] = "Vital Strike",
                ["spear_skill_pierce"] = "Chain Stab",
                ["spear_skill_evasion"] = "Evasion Strike",
                ["spear_skill_explosion"] = "Explosive Spear",
                ["spear_skill_dual"] = "Dual Thrust",

                // === Spear Skill Descriptions ===
                ["spear_desc_expert"] = "2-hit combo: Attack speed +{0}%, Damage +{1}% ({2}s)",
                ["spear_desc_throw"] = "Spear throw damage +{0}%",
                ["spear_desc_crit"] = "Spear damage +{0}%",
                ["spear_desc_pierce"] = "Weapon damage +{0}",
                ["spear_desc_evasion"] = "After dodge: Damage +{0}%, Stamina cost -{1}%",
                ["spear_desc_explosion"] = "{0}% chance to explode\nRange {1}m, Damage +{2}%",
                ["spear_desc_dual"] = "2-hit combo: Damage +{1}% for {0}s",
                ["spear_desc_penetrate"] = "Lightning shock mode for {0}s, triggers on {1} consecutive hits",
                ["spear_desc_penetrate_damage"] = "Weapon damage +{0}%",
                ["spear_desc_combo"] = "Enhanced throw: knockback target and nearby monsters",
                ["spear_desc_combo_damage"] = "+{0}%",
                ["spear_desc_combo_range"] = "Radius {0}m",
                ["requirement_one_hand_spear"] = "One-handed spear equipped",

                // === Polearm Skill Names ===
                ["polearm_skill_expert"] = "Polearm Expert",
                ["polearm_skill_king"] = "Pierce Charge",
                ["polearm_skill_spin"] = "Spin Slash",
                ["polearm_skill_suppress"] = "Suppress Attack",
                ["polearm_skill_hero"] = "Hero Strike",
                ["polearm_skill_area"] = "Wide Slash",
                ["polearm_skill_ground"] = "Ground Pound",
                ["polearm_skill_moon"] = "Crescent Slash",
                ["polearm_skill_charge"] = "Polearm Enhancement",

                // === Polearm Skill Descriptions ===
                ["polearm_desc_expert"] = "Attack range +{0}%",
                ["polearm_desc_spin"] = "Special attack damage +{0}%",
                ["polearm_desc_suppress"] = "Damage +{0}%",
                ["polearm_desc_hero"] = "{0}% chance to knockback",
                ["polearm_desc_area"] = "2-hit combo: Damage +{0}% ({1}s)",
                ["polearm_desc_ground"] = "Special attack damage +{0}%",
                ["polearm_desc_moon"] = "Attack range +{0}%, Stamina cost -{1}%",
                ["polearm_desc_charge"] = "Weapon damage +{0}",
                ["polearm_desc_king"] = "Dash {0}m forward, pierce attack on enemy contact",
                ["polearm_desc_king_first"] = "Damage +{0}%",
                ["polearm_desc_king_aoe"] = "Damage +{0}% (Rear {1}°, {2}m)",
                ["polearm_desc_king_knockback"] = "{0}m",
                ["tooltip_first_hit"] = "First Hit",
                ["tooltip_aoe_knockback"] = "AOE Knockback",
                ["tooltip_knockback_distance"] = "Knockback Distance",

                // === Knife Skill Names ===
                ["knife_skill_expert"] = "Knife Expert",
                ["knife_skill_assassin"] = "Assassin's Heart",
                ["knife_skill_evasion"] = "Evasion Mastery",
                ["knife_skill_move_speed"] = "Quick Movement",
                ["knife_skill_attack_speed"] = "Quick Attack",
                ["knife_skill_crit_rate"] = "Critical Mastery",
                ["knife_skill_combat_damage"] = "Lethal Damage",
                ["knife_skill_execution"] = "Assassin",
                ["knife_skill_assassination"] = "Assassination",

                // === Knife Skill Descriptions ===
                ["knife_desc_expert"] = "Backstab damage +{0}%",
                ["knife_desc_evasion"] = "Evasion chance +{0}%",
                ["knife_desc_move_speed"] = "Move speed +{0}%",
                ["knife_desc_attack_speed"] = "Attack power +{0}",
                ["knife_desc_crit_rate"] = "Crit chance +{0}%",
                ["knife_desc_combat_damage"] = "Attack power +{0}%",
                ["knife_desc_execution"] = "Crit damage +{0}%, Stagger damage +{1}%",
                ["knife_desc_assassination"] = "Backstab damage +{0}%",
                ["knife_desc_assassin_main"] = "Teleport behind enemy within {0}m (offset {1}m)\nStun target for {2}s + {3} consecutive attacks\nReturn to original position after attacks\n+{5}% damage, +{6}% crit chance for {4}s",
                ["knife_desc_assassin_note"] = "Ultimate skill that pushes all assassin abilities to the limit",
                ["knife_desc_assassin_note2"] = "Skill cancelled if no enemy within {0}m",
                ["requirement_knife_claw"] = "Knife or Claw equipped",

                // === Staff Skill Names ===
                ["staff_skill_expert"] = "Staff Expert",
                ["staff_skill_dual_cast"] = "Dual Cast",
                ["staff_skill_heal"] = "Heal",

                // === Staff Skill Descriptions ===
                ["staff_desc_dual_cast"] = "Fire {0} additional magic projectiles",
                ["staff_desc_dual_cast_angle"] = "Left -{0}°, Right +{0}° spread",
                ["staff_desc_dual_cast_damage"] = "{0}% of Staff/Wand damage",
                ["staff_desc_dual_cast_angle_unit"] = "±{0}°",
                ["staff_desc_dual_cast_note"] = "Buff lasts 30s, triggers on next magic attack",
                ["requirement_staff_wand"] = "Staff or Wand equipped",
                ["tooltip_dispersion_angle"] = "Spread Angle",

                // === Bow Skill Names ===
                ["bow_skill_explosive"] = "Explosive Arrow",

                // === Crossbow Skill Names ===
                ["crossbow_skill_oneshot"] = "One Shot",

                // === Job Skill Names ===
                ["job_skill_archer"] = "Archer",
                ["job_skill_tanker"] = "Tanker",
                ["job_skill_berserker"] = "Berserker",
                ["job_skill_rogue"] = "Rogue",
                ["job_skill_mage"] = "Mage",
                ["job_skill_paladin"] = "Paladin",

                // === Job Names ===
                ["job_archer"] = "Archer",
                ["job_mage"] = "Mage",
                ["job_tanker"] = "Tanker",
                ["job_rogue"] = "Rogue",
                ["job_paladin"] = "Paladin",
                ["job_berserker"] = "Berserker",

                // === Weapon Types ===
                ["weapon_sword"] = "Sword",
                ["weapon_spear"] = "Spear",
                ["weapon_mace"] = "Mace",
                ["weapon_polearm"] = "Polearm",
                ["weapon_knife"] = "Knife",
                ["weapon_bow"] = "Bow",
                ["weapon_crossbow"] = "Crossbow",
                ["weapon_staff"] = "Staff",

                // === Stat Names ===
                ["stat_damage"] = "Damage",
                ["stat_health"] = "Health",
                ["stat_stamina"] = "Stamina",
                ["stat_eitr"] = "Eitr",
                ["stat_armor"] = "Armor",
                ["stat_crit_chance"] = "Crit Chance",
                ["stat_crit_damage"] = "Crit Damage",
                ["stat_attack_speed"] = "Attack Speed",
                ["stat_move_speed"] = "Move Speed",
                ["stat_dodge"] = "Dodge",

                // === UI Buttons ===
                ["ui_reset_points"] = "Reset Points",
                ["ui_confirm"] = "Confirm",
                ["ui_cancel"] = "Cancel",
                ["ui_reset_confirm_title"] = "Reset Skills Confirm",
                ["ui_reset_confirm_message"] = "Are you sure you want to reset all skills?\nThis action cannot be undone.",
                ["ui_music_on"] = "Music On",
                ["ui_music_off"] = "Music Off",

                // === Skill Investment Messages ===
                ["skill_insufficient_points_detail"] = "Not enough skill points. (Required: {0}, Available: {1})",
                ["skill_invest_success"] = "✅ Skill points invested!",

                // === Additional Tooltip Keys ===
                ["tooltip_none"] = "None",
                ["tooltip_self"] = "Self",
                ["tooltip_affect_range"] = "Affect Range",
                ["tooltip_passive_effect"] = "Passive Effect",
                ["tooltip_additional"] = "Additional",
                ["tooltip_no_player"] = "No player info",
                ["tooltip_calculation_error"] = "Calculation error",
                ["tooltip_simulation_error"] = "Simulation error",
                ["tooltip_load_error"] = "Cannot load skill info",
                ["stat_arrow"] = "Arrow",
                ["item_eikthyr_trophy"] = "Eikthyr Trophy",
                ["confirmation_job_only"] = "Only one job can be selected, Level 10+",
                ["confirmation_job_select_only"] = "Only one job can be selected",
                ["skill_type_job_active"] = "Job Active Skill - {0} Key",

                // === Archer Job ===
                ["archer_desc_multishot_fallback"] = "Fires 5 arrows x2 times.",
                ["archer_desc_arrow_damage_fallback"] = "Each arrow deals 50% of bow+arrow damage",
                ["archer_passive_skills"] = "Jump height +{0}%, Fall damage -{1}%",
                ["requirement_archer"] = "Bow equipped, Archer job",

                // === Tanker Job ===
                ["tanker_skill_warcry"] = "War Cry",
                ["tanker_desc_warcry"] = "Taunt enemies in {0}m range for {1}s (boss {2}s), caster gets {4}% damage reduction for {3}s",
                ["tanker_desc_warcry_fallback"] = "Taunt enemies for 5s (boss 1s), caster gets 20% damage reduction for 5s",
                ["tanker_skill_type_taunt"] = "Active Taunt Skill - Y Key",
                ["tanker_passive_damage_reduction"] = "Damage taken -{0}%",

                // === Berserker Job ===
                ["berserker_skill_rage"] = "Berserker Rage",
                ["berserker_desc_rage"] = "Activate rage state for {0}s with powerful attack boost",
                ["berserker_desc_rage_detail"] = "+{0}% damage per 1% health lost, max +{1}%",
                ["berserker_rage_effect"] = "Rage Effect",
                ["berserker_rage_damage_per_health"] = "+{0}% damage per 1% health lost",
                ["berserker_passive_desc"] = "Stamina regen +20%, Invincible for {1}s when health below {0}% (cooldown {2}min)",
                ["berserker_not_in_rage"] = "Not in rage state",
                ["berserker_current_status"] = "Current health: {0}%\nCurrent damage bonus: +{1}%\nRage state: Active",
                ["berserker_simulation_title"] = "Damage bonus by health:",
                ["berserker_simulation_line"] = "Health {0}%: +{1}% damage",
                ["berserker_simulation_max"] = "Max limit: +{0}%",
                ["berserker_default_tooltip"] = "Description:\n[Active Skill - Y Key]\n+2% damage per -1% health for 20s, stronger at lower health\n\n[Passive Skill]\n• Stamina regen +20% (always active)\n• 7s invincibility when health below 10% (300s cooldown)\n\nAdditional: Max +200% damage limit, red/gold aura effect\nRange: Self\nCost: Stamina 20 (active only)\nCooldown: 45s (active), 300s (passive)\nRequirement: Berserker job\nNotice: Only one job can be selected, Lv 10+\nRequired Item: Ancient Bark Spear",
                ["requirement_job_berserker"] = "Berserker job",

                // === Rogue Job ===
                ["rogue_desc_shadow_strike"] = "{0}s stealth, aggro removal range {1}m",
                ["rogue_desc_attack_bonus"] = "+{1}% attack for {0}s",
                ["rogue_passive_desc"] = "Sneak skill +{0}, Sneak speed +{1}%, Fall damage -{2}%",
                ["requirement_rogue"] = "Knife equipped, Rogue job",

                // === Mage Job ===
                ["mage_desc_aoe"] = "Area damage {0}%",
                ["mage_passive_resistance"] = "Magic resistance +{0}%",
                ["requirement_mage"] = "Staff equipped, Mage job",

                // === Healer/Staff ===
                ["healer_self_heal_included"] = "Includes caster",
                ["healer_self_heal_excluded"] = "Caster not healed (other players only)",
                ["healer_desc_instant"] = "Instant heal in {0}m radius from caster",
                ["healer_desc_heal_percent"] = "Instantly restore {0}% of ally max health",
                ["healer_healing_effect"] = "Healing Effect",
                ["healer_instant_heal"] = "Instantly restore {0} of ally max health",
                ["staff_desc_dual_cast_fallback"] = "Fire 2 additional projectiles (left -5°, right +5°)",
                ["staff_desc_dual_cast_damage_fallback"] = "70% of staff/wand damage",

                // === Expert Tree Skill Descriptions ===
                // Attack Expert Tree
                ["attack_root_desc"] = "All damage +{0}%",
                ["atk_melee_bonus_desc"] = "{0}% chance for +{1}% extra damage with melee weapons",
                ["atk_bow_bonus_desc"] = "{0}% chance for +{1}% extra damage with bow",
                ["atk_crossbow_bonus_desc"] = "{0}% chance for knockback with crossbow",
                ["atk_staff_bonus_desc"] = "{0}% chance for {1}% extra elemental damage to 2 nearby enemies with staff",
                ["atk_crit_chance_desc"] = "Crit chance +{0}%",
                ["atk_melee_crit_desc"] = "+{0}% extra damage on 2-hit combo with one-handed melee",
                ["atk_crit_dmg_desc"] = "Crit damage +{0}%",
                ["atk_twohand_crush_desc"] = "Two-handed weapon damage +{0}%",
                ["atk_staff_mage_desc"] = "Elemental damage +{0}% with staff attack",
                ["atk_finisher_melee_desc"] = "+{0}% extra damage on 3-hit melee combo",

                // Defense Expert Tree - Names
                ["defense_root_name"] = "Defense Expert",
                ["defense_survival_name"] = "Skin Hardening",
                ["defense_dodge_name"] = "Mind-Body Training",
                ["defense_health_name"] = "Health Training",
                ["defense_breath_name"] = "Core Breathing",
                ["defense_agile_name"] = "Evasion Training",
                ["defense_boost_name"] = "Health Boost",
                ["defense_shield_name"] = "Shield Training",
                ["defense_mental_name"] = "Shockwave Release",
                ["defense_mental_desc"] = "When HP below 45%, stun enemies within {0}m for {1}s (Cooldown: {2}s)",
                ["defense_instant_name"] = "Ground Stomp",
                ["defense_instant_desc"] = "When HP below 35%, push enemies {0}m away (Cooldown: {1}s)",
                ["defense_tanker_name"] = "Rock Skin",
                ["defense_focus_name"] = "Endurance",
                ["defense_stamina_name"] = "Agility",
                ["defense_heal_name"] = "Troll's Regeneration",
                ["defense_parry_name"] = "Block Master",
                ["defense_mind_name"] = "Mind Shield",
                ["defense_mind_desc"] = "Shield duration +{0}s",
                ["defense_attack_name"] = "Nerve Enhancement",
                ["defense_double_jump_name"] = "Double Jump",
                ["defense_double_jump_desc"] = "Jump {0} additional time(s) in air",
                ["defense_body_name"] = "Jotunn's Vitality",
                ["defense_true_name"] = "Jotunn's Shield",

                // Defense Expert Tree - Descriptions
                ["defense_root_desc"] = "Health +{0}, Defense +{1}",
                ["defense_survival_desc"] = "Health +{0}, Defense +{1}",
                ["defense_health_desc"] = "Health +{0}, Defense +{1}",
                ["defense_dodge_desc"] = "Max stamina +{0}, Max Eitr +{1}",
                ["defense_breath_desc"] = "Max Eitr +{0}",
                ["defense_agile_desc"] = "Dodge +{0}%, Roll invincibility +{1}%",
                ["defense_boost_desc"] = "Health +{0}",
                ["defense_shield_desc"] = "Shield block power +{0}",
                ["defense_tanker_desc"] = "Defense +{0}%",
                ["defense_focus_desc"] = "Run stamina -{0}%, Jump stamina -{1}%",
                ["defense_stamina_desc"] = "Dodge +{0}%, Roll stamina -{1}%",
                ["defense_heal_desc"] = "Health +{1} every {0}s",
                ["defense_parry_desc"] = "Parry +{0}s, Shield block power +{1}",
                ["defense_attack_desc"] = "Dodge +{0}%",
                ["defense_body_desc"] = "Max health +{0}%, Physical/Magic defense +{1}%",
                ["defense_true_desc"] = "Block stamina -{0}%, Normal shield speed +{1}%, Tower shield speed +{2}%",

                // Defense Expert Tree - Effect Texts (English)
                ["defense_root_effect"] = "🛡️ Defense Expert! Health +{0}, Defense +{1}",
                ["defense_shield_effect"] = "🛡️ Shield Training! Shield block power +{0}",
                ["defense_parry_effect"] = "🛡️ Parry Master! Parry +{0}s, Shield block power +{1}",
                ["defense_body_effect"] = "🛡️ Jotunn's Vitality! Health +{0}%, Defense +{1}%",

                // Staff Expert Tree
                ["staff_expert_desc"] = "Elemental damage +{0}%",
                ["staff_focus_desc"] = "Eitr cost -{0}%",
                ["staff_stream_desc"] = "Max Eitr +{0}",
                ["staff_amp_desc"] = "Elemental damage +{0}%",
                ["staff_frost_desc"] = "Frost damage +{0}",
                ["staff_fire_desc"] = "Fire damage +{0}",
                ["staff_lightning_desc"] = "Lightning damage +{0}",
                ["staff_luck_mana_desc"] = "{0}% chance for no Eitr cost",

                // Mace Expert Tree (additional)
                ["mace_expert_desc2"] = "Mace damage +{0}%, {1}% chance to stun for {2}s on hit",
                ["mace_stun_boost_desc2"] = "Stun chance +{0}%, Duration +{1}s",
                ["mace_guard_boost_desc2"] = "Defense +{0}%",
                ["mace_heavy_strike_desc2"] = "Heavy attack damage +{0}%",
                ["mace_knockback_desc2"] = "{0}% chance for knockback on attack",
                ["mace_tank_desc2"] = "Health +{0}%, Damage taken -{1}%",
                ["mace_dps_desc2"] = "Damage +{0}%, Attack speed +{1}%",
                ["mace_grandmaster_desc2"] = "Defense +{0}%",

                // === Prerequisite Text ===
                ["prerequisite_label"] = "🔗 Required",
                ["prerequisite_connector_or"] = " or ",
                ["prerequisite_labor_craft"] = "Labor Expert + Crafting Expert",

                // === Weapon Requirement Conditions ===
                ["requirement_bow_effect"] = "Bow equipped",
                ["requirement_crossbow_effect"] = "Crossbow equipped",
                ["requirement_staff_effect"] = "Staff equipped",
                ["requirement_sword_effect"] = "Sword equipped",
                ["requirement_axe_effect"] = "Axe equipped",
                ["requirement_knife_effect"] = "Knife equipped",
                ["requirement_spear_effect"] = "Spear equipped",
                ["requirement_mace_effect"] = "Mace equipped",
                ["requirement_one_hand_melee_effect"] = "One-handed melee weapon equipped",

                // === MMO Bridge Messages ===
                ["mmo_level_recovery_complete"] = "Level data recovery complete!",
                ["mmo_level_recovery_detail"] = "Lv.{0} → Lv.{1}",
                ["mmo_captain_to_epic_complete"] = "Captain -> EpicMMO migration complete!",
                ["mmo_captain_to_epic_detail"] = "Lv.{0} -> Lv.{1}",
                ["mmo_skillpoint_sync_complete"] = "Skill Points -> EpicMMO sync complete!",
                ["mmo_skillpoint_sync_detail"] = "Lv.{0} -> Lv.{1}",
                ["mmo_epic_to_captain_complete"] = "EpicMMO -> Captain data recovery complete!",
                ["mmo_epic_to_captain_detail"] = "Lv.{0}",

                // === Job Skill Messages ===
                ["job_no_job_selected"] = "No job selected.",
                ["job_one_hand_melee_required"] = "One-handed melee weapon required!",
                ["job_holy_heal_cooldown"] = "Holy Heal cooldown: {0}s",
                ["job_stamina_required"] = "Not enough stamina ({0} required)",
                ["job_eitr_required"] = "Not enough Eitr ({0} required)",
                ["job_self_heal"] = "✨ Self Heal: +{0} HP",
                ["job_paladin_heal_success"] = "⭐ Paladin Holy Heal! (Self heal + {0} allies DoT heal)",
                ["job_continuous_heal_start"] = "🌟 Continuous healing started!",
                ["job_continuous_heal_tick"] = "💚 Continuous heal: +{0} HP",
                ["job_continuous_heal_progress"] = "💚 Continuous heal: +{0} HP ({1}/{2})",
                ["job_continuous_heal_complete"] = "✨ Continuous healing complete!",
                ["job_skill_error"] = "Error during skill cast.",

                // === Rogue Skill Messages ===
                ["rogue_shadow_strike_cooldown"] = "Shadow Strike cooldown: {0}s",
                ["rogue_stamina_insufficient"] = "Not enough stamina.",
                ["rogue_shadow_strike_success"] = "🗡️ Shadow Strike! (Aggro removed + {1}% damage for {0}s)",
                ["rogue_shadow_strike_end"] = "Shadow Strike ended",

                // === Mage Skill Messages ===
                ["mage_explosion_cooldown"] = "Magic Explosion cooldown: {0}s",
                ["mage_eitr_insufficient"] = "Not enough Eitr.",
                ["mage_explosion_success"] = "🔮 Magic Explosion! (+{1}% magic damage, +{2}m range for {0}s)",
                ["mage_explosion_end"] = "Magic Explosion ended",

                // === Archer Skill Messages ===
                ["archer_multishot_cooldown"] = "Multi-Shot cooldown: {0}s",
                ["archer_stamina_insufficient"] = "Not enough stamina",
                ["archer_multishot_success"] = "🏹 Multi-Shot! ({0} arrows fired)",
                ["archer_bow_required"] = "Bow required!",
                ["archer_no_arrow"] = "No arrows!",
                ["archer_job_required"] = "Archer job required!",
                ["archer_cooldown"] = "Cooldown: {0}s",

                // === Berserker Skill Messages ===
                ["berserker_rage_end"] = "Berserker Rage ended",

                // === Level Up Message ===
                ["level_up"] = "LEVEL UP!",

                // === Speed Expert Tree - Skill Names ===
                ["speed_root_name"] = "Speed Expert",
                ["speed_base_name"] = "Agility Foundation",
                ["melee_combo_name"] = "Flow of Combo",
                ["crossbow_reload2_name"] = "Crossbow Mastery",
                ["bow_speed2_name"] = "Bow Mastery",
                ["moving_cast_name"] = "Moving Cast",
                ["speed_ex1_name"] = "Apprentice I",
                ["speed_ex2_name"] = "Apprentice II",
                ["speed_master_name"] = "Energizer",
                ["ship_master_name"] = "Captain",
                ["agility_peak_name"] = "Jump Master",
                ["speed_1_name"] = "Agility",
                ["speed_2_name"] = "Endurance",
                ["speed_3_name"] = "Intelligence",
                ["all_master_name"] = "Master",
                ["melee_speed1_name"] = "Melee Acceleration",
                ["crossbow_draw1_name"] = "Crossbow Acceleration",
                ["bow_draw1_name"] = "Bow Acceleration",
                ["staff_speed1_name"] = "Cast Acceleration",

                // === Speed Expert Tree - Skill Descriptions ===
                ["speed_root_desc"] = "Move speed +{0}%",
                ["speed_base_desc"] = "Attack speed +{0}%, Move speed +{2}% for {1}s after dodge",
                ["melee_combo_desc"] = "On melee 2-hit combo: Attack speed +{1}%, Stamina -{2}% for {0}s",
                ["crossbow_reload2_desc"] = "On crossbow hit: Move speed +{0}% ({1}s), Reload +{2}% during buff",
                ["bow_speed2_desc"] = "On bow 2-hit combo: Stamina -{0}%, Next draw +{1}%",
                ["moving_cast_desc"] = "Move speed +{0}% during cast, Eitr cost -{1}%",
                ["speed_ex1_desc"] = "Melee skill +{0}, Crossbow skill +{1}",
                ["speed_ex2_desc"] = "Magic skill +{0}, Bow skill +{1}",
                ["speed_master_desc"] = "Food consumption rate -{0}%",
                ["ship_master_desc"] = "Ship speed +{0}%",
                ["agility_peak_desc"] = "Jump skill +{0}, Jump stamina -{1}%",
                ["speed_1_desc"] = "Melee attack speed +{0}%, Move speed +{1}%",
                ["speed_2_desc"] = "Max stamina +{0}",
                ["speed_3_desc"] = "Max Eitr +{0}",
                ["all_master_desc"] = "Run skill +{0}, Jump skill +{1}",
                ["melee_speed1_desc"] = "Melee attack speed +{0}%, Next attack speed +{1}% on 3-hit combo",
                ["crossbow_draw1_desc"] = "Crossbow reload +{0}%, Move speed +{1}% during reload",
                ["bow_draw1_desc"] = "Bow draw +{0}%, Move speed +{1}% during draw",
                ["staff_speed1_desc"] = "Magic attack speed +{0}%, Recover {1}% max Eitr on 3-hit combo",

                // === Speed Expert Tree - Effect Texts (English) ===
                ["speed_root_effect"] = "🏃 Speed Expert Invested! (+{0}% Move Speed)",
                ["speed_base_effect"] = "🏃 Agility Foundation Learned!\nAttack Speed +{0}%\nMove Speed +{1}% after dodge",
                ["agility_peak_effect"] = "🦘 Jump Master Learned! Jump +{0} (Level: {1:F0} → {2:F0})",

                // === Attack Expert Tree - Skill Names ===
                ["attack_root_name"] = "Attack Expert",
                ["atk_base_name"] = "Basic Attack",
                ["atk_melee_bonus_name"] = "Melee Specialization",
                ["atk_bow_bonus_name"] = "Bow Specialization",
                ["atk_crossbow_bonus_name"] = "Crossbow Specialization",
                ["atk_staff_bonus_name"] = "Staff Specialization",
                ["atk_twohand_drain_name"] = "Attack Boost",
                ["atk_melee_crit_name"] = "Melee Enhancement",
                ["atk_crit_chance_name"] = "Precision Strike",
                ["atk_ranged_enhance_name"] = "Ranged Enhancement",
                ["atk_special_name"] = "Specialized Stats",
                ["atk_crit_dmg_name"] = "Weak Point Strike",
                ["atk_finisher_melee_name"] = "Combo Finisher",
                ["atk_twohand_crush_name"] = "Two-Hand Crush",
                ["atk_staff_mage_name"] = "Elemental Strike",

                // === Attack Expert Tree - Skill Descriptions (English) ===
                ["atk_base_desc"] = "Physical attack +{0}, Elemental attack +{1}",
                ["atk_twohand_drain_desc"] = "Physical attack +{0}, Elemental attack +{1}",
                ["atk_ranged_enhance_desc"] = "Ranged weapon damage +{0}% (Crossbow, Bow, Staff)",
                ["atk_special_desc"] = "Crit chance +{0}%",

                // === Attack Expert Tree - Effect Texts (English) ===
                ["atk_base_effect"] = "💪 Basic Attack Learned!",
                ["atk_melee_bonus_effect"] = "⚔️ Melee Specialization Learned!",
                ["atk_twohand_drain_effect"] = "💪 Attack Boost!",
                ["atk_ranged_enhance_effect"] = "🏹 Ranged Enhancement!",
                ["atk_special_effect"] = "⭐ Specialized Stats! Crit chance +{0}%",
                ["atk_staff_mage_effect"] = "🔥 Elemental Strike!",

                // === Paladin Skill ===
                ["paladin_name"] = "Paladin",
                ["paladin_desc"] = "Holy Heal: Heal self and allies within {0}m",
                ["paladin_passive_desc"] = "One-handed melee damage +{0}%, Heal self and nearby allies (Y Key)",
                ["paladin_tooltip_desc"] = "Area ally HP {0}%/{1}s ({2}s), Self {3}% instant",
                ["paladin_passive_resistance"] = "Physical & Elemental resistance -{0}%",
                ["paladin_requirement"] = "One-handed melee equipped, Paladin class",
                ["paladin_required_item"] = "Eikthyr Trophy",
                ["paladin_tooltip_error"] = "Failed to load Paladin skill info",

                // === Common Tooltip Labels ===
                ["tooltip_description"] = "Description",
                ["tooltip_range"] = "Range",
                ["tooltip_cost"] = "Cost",
                ["tooltip_stamina"] = "Stamina",
                ["tooltip_eitr"] = "Eitr",
                ["tooltip_skill_type"] = "Skill Type",
                ["tooltip_job_active_y"] = "Job Active Skill - Y Key",
                ["tooltip_passive"] = "Passive",
                ["tooltip_cooldown"] = "Cooldown",
                ["tooltip_seconds"] = "s",
                ["tooltip_requirement"] = "Requirement",
                ["tooltip_confirmation"] = "Note",
                ["tooltip_job_limit"] = "Only 1 job selectable, Level 10+",
                ["tooltip_required_item"] = "Required Item",

                // === Melee Root Skill ===
                ["melee_skill_expert"] = "Melee Expert",
                ["melee_desc_expert"] = "Melee weapon damage +3 (fixed)",
                ["melee_root_effect"] = "Melee Expert acquired! Melee weapon damage +3",

                // === Ranged Root Skill ===
                ["ranged_skill_expert"] = "Ranged Expert",
                ["ranged_desc_expert"] = "Bow/Crossbow pierce +2, Staff/Wand fire attack +2",

                // === Production Skills ===
                ["production_skill_expert"] = "Production Expert",
                ["production_desc_expert"] = "50% chance +1 wood",
                ["novice_worker_name"] = "Novice Worker",
                ["novice_worker_desc"] = "25% chance +1 wood",
                ["woodcutting_lv2_name"] = "Woodcutting Lv2",
                ["woodcutting_lv2_desc"] = "25% chance +1 wood",
                ["woodcutting_lv3_name"] = "Woodcutting Lv3",
                ["woodcutting_lv3_desc"] = "25% chance +1 wood",
                ["woodcutting_lv4_name"] = "Woodcutting Lv4",
                ["woodcutting_lv4_desc"] = "25% chance +1 wood",
                ["gathering_lv2_name"] = "Gathering Lv2",
                ["gathering_lv2_desc"] = "25% chance +1 gathering (except wood)",
                ["gathering_lv3_name"] = "Gathering Lv3",
                ["gathering_lv3_desc"] = "25% chance +1 gathering (except wood)",
                ["gathering_lv4_name"] = "Gathering Lv4",
                ["gathering_lv4_desc"] = "25% chance +1 gathering when holding 200+ items",
                ["mining_lv2_name"] = "Mining Lv2",
                ["mining_lv2_desc"] = "25% chance +1 ore",
                ["mining_lv3_name"] = "Mining Lv3",
                ["mining_lv3_desc"] = "25% chance +1 ore",
                ["mining_lv4_name"] = "Mining Lv4",
                ["mining_lv4_desc"] = "25% chance +1 ore",
                ["crafting_lv2_name"] = "Crafting Lv2",
                ["crafting_lv2_desc"] = "25% chance +1 upgrade, Max durability +25%",
                ["crafting_lv3_name"] = "Crafting Lv3",
                ["crafting_lv3_desc"] = "25% chance +1 upgrade, Max durability +25%",
                ["crafting_lv4_name"] = "Crafting Lv4",
                ["crafting_lv4_desc"] = "25% chance +1 upgrade, Max durability +25%",

                // === Production Skills - Effect Texts ===
                ["production_root_effect"] = "Production Expert acquired!",
                ["novice_worker_effect"] = "Novice Worker acquired!",
                ["woodcutting_lv2_effect"] = "Woodcutting Lv2 acquired!",
                ["woodcutting_lv3_effect"] = "Woodcutting Lv3 acquired!",
                ["woodcutting_lv4_effect"] = "Woodcutting Lv4 acquired! Bonus wood when holding 400+!",
                ["gathering_lv2_effect"] = "Gathering Lv2 acquired!",
                ["gathering_lv3_effect"] = "Gathering Lv3 acquired!",
                ["gathering_lv4_effect"] = "Gathering Lv4 acquired! Bonus when holding 200+ items!",
                ["mining_lv2_effect"] = "Mining Lv2 acquired!",
                ["mining_lv3_effect"] = "Mining Lv3 acquired!",
                ["mining_lv4_effect"] = "Mining Lv4 acquired! Bonus ore when using 25 silver!",
                ["crafting_lv2_effect"] = "Crafting Lv2 acquired!",
                ["crafting_lv3_effect"] = "Crafting Lv3 acquired!",
                ["crafting_lv4_effect"] = "Crafting Lv4 acquired! Crafting boost with silver sword+helmet!",

                // === Crossbow Skills ===
                ["crossbow_skill_expert"] = "Crossbow Expert",
                ["crossbow_rapid_fire_lv1_name"] = "Rapid Fire Lv1",
                ["crossbow_balance_name"] = "Balanced Aim",
                ["crossbow_rapid_name"] = "Quick Reload",
                ["crossbow_mark_name"] = "True Shot",
                ["crossbow_auto_reload_name"] = "Auto Reload",
                ["crossbow_rapid_fire_lv2_name"] = "Rapid Fire Lv2",
                ["crossbow_final_strike_name"] = "Final Strike",
                ["crossbow_oneshot_name"] = "One Shot",

                // === Bow Skills ===
                ["bow_skill_expert"] = "Bow Expert",
                ["bow_focus_name"] = "Focused Shot",
                ["bow_multishot_lv1_name"] = "Multi-Shot Lv1",
                ["bow_proficiency_name"] = "Bow Proficiency",
                ["bow_basic_attack_name"] = "Basic Bow Attack",
                ["bow_multishot_lv2_name"] = "Multi-Shot Lv2",
                ["bow_instinct_name"] = "Hunter's Instinct",
                ["bow_precision_name"] = "Precision Aim",
                ["bow_explosive_name"] = "Explosive Arrow",

                // === Staff Skills ===
                ["staff_skill_expert_name"] = "Staff Expert",
                ["staff_focus_name"] = "Mental Focus",
                ["staff_stream_name"] = "Magic Flow",
                ["staff_amp_name"] = "Magic Amplification",
                ["staff_frost_name"] = "Frost Element",
                ["staff_fire_name"] = "Fire Element",
                ["staff_lightning_name"] = "Lightning Element",
                ["staff_luck_mana_name"] = "Lucky Mana",
                ["staff_dual_cast_name"] = "Dual Cast",
                ["staff_heal_name"] = "Heal",

                // === Ranged/Crossbow/Bow Descriptions ===
                ["ranged_root_desc"] = "Bow/Crossbow piercing +2, Staff/Wand fire attack +2",
                ["weapon_effect_note"] = "※ Active when {0} equipped",
                ["crossbow_expert_desc"] = "Crossbow damage +{0}%",
                ["crossbow_rapid_fire_desc"] = "{0}% chance to fire {1} shots (each {2}% damage, {3} bolts consumed)",
                ["crossbow_balance_desc"] = "On hit, {0}% chance to knockback ({1}m)",
                ["crossbow_rapid_desc"] = "Reload speed +{0}%",
                ["crossbow_mark_desc"] = "Critical chance fixed at 0%, but crossbow damage +{0}%",
                ["crossbow_auto_reload_desc"] = "On hit, {0}% chance for next reload 200% faster",
                ["crossbow_final_desc"] = "Critical damage +{0}%",
                ["crossbow_oneshot_desc"] = "Crossbow damage +{0}%, {1}s reload delay until next shot",
                ["bow_expert_desc"] = "Bow damage +{0}%",
                ["bow_focus_desc"] = "Critical chance +{0}%",
                ["bow_multishot_lv1_desc"] = "{0}% chance to fire 1 extra arrow (+3° angle)",
                ["bow_proficiency_desc"] = "Bow skill (proficiency) +{0}",
                ["bow_proficiency_note"] = "※ Bonus persists through death",
                ["bow_basic_attack_desc"] = "Bow damage +{0}",
                ["bow_multishot_lv2_desc"] = "{0}% chance to fire 2 extra arrows (+3° angle)",
                ["bow_instinct_desc"] = "Critical chance +{0}%",
                ["bow_precision_desc"] = "Critical damage +{0}%",

                // === Crossbow Additional Descriptions ===
                ["crossbow_rapid_fire_lv2_desc"] = "{0}% chance to fire {1} consecutive shots (each {2}% damage, {3} bolts consumed)",
                ["crossbow_rapid_fire_lv2_note"] = "※ Stacks with Lv1 chance",
                ["crossbow_final_strike_desc"] = "Extra {1}% damage to enemies with {0}%+ HP",

                // === Staff Full Descriptions ===
                ["staff_expert_full_desc"] = "Staff elemental damage +{0}%\nMaster staff and wand usage for higher magic damage\nRequirement: Staff or Wand equipped",
                ["staff_focus_full_desc"] = "Staff Eitr consumption -{0}%\nMental focus increases mana efficiency\nRequirement: Staff or Wand equipped",
                ["staff_stream_full_desc"] = "Max Eitr +{0}\nMaster magic flow to hold more mana\nRequirement: Staff or Wand equipped",
                ["staff_amp_full_desc"] = "Staff magic damage +{0}%\nAmplify magic power for stronger spells\nRequirement: Staff or Wand equipped",
                ["staff_frost_full_desc"] = "Frost damage +{0}\nRequirement: Staff or Wand equipped",
                ["staff_fire_full_desc"] = "Fire damage +{0}\nRequirement: Staff or Wand equipped",
                ["staff_lightning_full_desc"] = "Lightning damage +{0}\nRequirement: Staff or Wand equipped",
                ["staff_luck_mana_full_desc"] = "{0}% chance for no Eitr cost\nLucky magic grants free spell casting\nRequirement: Staff or Wand equipped",
                ["staff_dual_cast_full_desc"] = "Press R to fire {0} additional magic projectiles (left -{1:F0}°, right +{1:F0}°)\nProjectile damage: {2:F0}% of staff/wand damage\nCost: Eitr {3:F0}\nSkill Type: Active R Key\nWeapon: Staff\nCooldown: {4:F0}s\nRequirement: Staff or Wand equipped",
                ["staff_heal_full_desc"] = "Press H for instant healing in {0:F0}m radius from caster\nRestore {1}% of ally max HP instantly\nCost: Eitr {2:F0}\nSkill Type: Active H Key\nWeapon: Staff\nCooldown: {3:F0}s\nRequirement: Staff or Wand equipped",

                // === Ranged Skills - Effect Texts ===
                ["ranged_root_effect"] = "Ranged Expert acquired!",
                ["bow_multishot_lv1_effect"] = "Multi-Shot Lv1 acquired!",
                ["bow_multishot_lv2_effect"] = "Multi-Shot Lv2 acquired!",
                ["bow_proficiency_effect"] = "Bow Proficiency acquired! Bow skill +{0}",
                ["bow_explosive_desc"] = "Fire explosive arrow\nDamage: {0}% of bow damage\nStamina cost: {1}%\nCooldown: {2}s"
            };
        }
    }
}

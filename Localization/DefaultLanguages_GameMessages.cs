using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class DefaultLanguages
    {
        private static Dictionary<string, string> GetKorean_GameMessages()
        {
            return new Dictionary<string, string>
            {
                // === Active Skill HUD Slots ===
                ["hud_slot_y"] = "직업",
                ["hud_slot_r"] = "원거리",
                ["hud_slot_g"] = "근접",
                ["hud_slot_h"] = "보조",

                // === Common Messages ===
                ["attack_speed_cap_warning"] = "공격속도 보너스가 {0}%를 초과할 수 없습니다.",
                ["move_speed_cap_warning"] = "이동속도 보너스가 {0}%를 초과할 수 없습니다.",
                ["cooldown_remaining"] = "{0} 쿨다운 중! 남은 시간: {1}초",
                ["skill_cooldown"] = "{0} 스킬 쿨타임: {1}초 남음",
                ["stamina_insufficient"] = "스태미나가 부족합니다",
                ["eitr_insufficient"] = "Eitr 부족! (필요: {0})",
                ["no_enemies_in_range"] = "범위 내에 적이 없습니다!",
                ["no_targets_in_range"] = "범위 내에 적이 없습니다!",
                ["no_enemy_in_range"] = "{0}m 내에 적이 없습니다!",
                ["can_invest"] = "포인트를 투자할 수 있습니다",

                // === Archer Passive ===
                ["archer_jump_bonus"] = "아처 패시브: 점프 높이 +{0}%",
                ["archer_fall_damage_reduced"] = "아처 패시브: -{0} 낙하 데미지 감소!",

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
                ["rogue_aggro_protection_end"] = "어그로 보호 해제됨",

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

                // === Weapon Equip Required Messages ===
                ["sword_equip_required"] = "검을 착용해야 합니다!",
                ["dagger_equip_required"] = "단검을 착용해야 합니다!",
                ["spear_equip_required"] = "창을 착용해야 합니다!",
                ["polearm_equip_required"] = "폴암을 착용해야 합니다!",
                ["one_hand_mace_shield_required"] = "한손 둔기와 방패를 착용해야 합니다!",
                ["two_hand_mace_required"] = "양손 둔기를 착용해야 합니다!",
                ["staff_equip_required"] = "지팡이를 착용해야 합니다!",
                ["r_key_skill_condition_not_met"] = "Z키 스킬 조건이 충족되지 않았습니다",
                ["g_key_skill_required"] = "G키 스킬이 필요합니다",
                ["h_key_skill_required"] = "H키 스킬이 필요합니다",

                // === Mace Skills ===
                ["mace_guardian_cooldown"] = "수호자의 진심 쿨다운 중! 남은 시간: {0}초",
                ["mace_required"] = "둔기를 착용해야 합니다",
                ["mace_guardian_success"] = "수호자의 진심! 방어력 +{0}%!",
                ["mace_fury_cooldown"] = "분노의 망치 쿨다운 중! 남은 시간: {0}초",
                ["mace_fury_success"] = "분노의 망치! {0} 데미지!",
                ["fury_hammer_cooldown"] = "분노의 망치 쿨다운: {0}초",
                ["fury_hammer_skill_required"] = "분노의 망치 스킬이 필요합니다",
                ["fury_hammer_combo_hit"] = "분노의 망치 {0}타! 데미지 {1}, {2}마리 적중",
                ["fury_hammer_final_hit_ready"] = "🔨 최종 일격 준비!",
                ["fury_hammer_combo_complete"] = "🔥 분노의 망치 완료! {0}명 적중",
                ["guardian_heart_activated"] = "🛡️ 수호자의 진심 발동!",
                ["guardian_heart_buff_name"] = "수호자의 진심",
                ["guardian_heart_ended"] = "수호자의 진심 종료",
                ["guardian_heart_reflect"] = "🛡️ 반사 피해: {0}",
                ["guardian_heart_remaining"] = "수호자의 진심 남은 시간: {0}초",
                ["guardian_heart_skill_required"] = "수호자의 진심 스킬이 필요합니다",
                ["mace_shield_required"] = "방패를 착용해야 합니다",

                // === Spear Combo Skills ===
                ["combo_spear_skill_required"] = "연공창 스킬이 필요합니다",
                ["cooldown_seconds"] = "쿨다운: {0}초",
                ["combo_spear_buff_name"] = "연공창",
                ["combo_spear_buff_activated"] = "⚡ 연공창 발동! {0}회 사용 가능 (+{1}% 데미지)",
                ["combo_spear_buff_ended"] = "연공창 효과 종료",
                ["combo_spear_uses_remaining"] = "🎯 연공창 남은 횟수: {0}",
                ["combo_spear_retrieve_failed"] = "창 회수 실패",
                ["combo_spear_retrieved_equipped"] = "✅ 창 회수 및 장착 완료!",
                ["combo_spear_inventory_full"] = "인벤토리가 가득 차서 창이 앞에 떨어졌습니다",

                // === Polearm Skills ===
                ["polearm_king_cooldown"] = "장창제왕 쿨다운 중! 남은 시간: {0}초",
                ["polearm_required"] = "폴암을 착용해야 합니다",
                ["polearm_king_success"] = "장창제왕! {0} 데미지!",
                ["storm_slash_first"] = "폭풍베기! 1차 베기 +{0}%",
                ["storm_slash_explosion"] = "폭풍베기 폭발! +{0}%",
                ["polearm_boost_active"] = "⚔️ 폴암강화 +{0} 관통!",
                ["suppress_active"] = "⚔️ 제압 공격 +{0}%!",

                // === Knife Skills ===
                ["knife_assassin_cooldown"] = "암살자의 심장 쿨다운 중! 남은 시간: {0}초",
                ["knife_required"] = "단검을 착용해야 합니다",
                ["knife_assassin_success"] = "암살자의 심장! 치명타 데미지 +{0}%!",
                ["knife_expert_backstab"] = "🗡️ 백스탭! +{0}% 데미지!",
                ["knife_evasion_buff"] = "🌀 회피 숙련! {0}초간 회피율 +{1}%",
                ["knife_attack_evasion_buff"] = "⚔️ 공격 회피! 회피율 +{0}%, {1}초 지속",
                ["knife_combat_mastery"] = "⚔️ 전투 숙련! 공격력 +{0}%",
                ["knife_text_assassination_stagger"] = "💀 암살 스태거!",
                ["assassin_complete_hits"] = "암살 {0}회 완료!",
                ["assassin_complete_return"] = "암살 완료! 돌아갑니다",
                ["assassin_heart_cooldown"] = "암살자의 심장 쿨다운: {0}초",
                ["assassin_heart_crit"] = "🎯 암살 치명타! +{0}% 데미지!",
                ["assassin_heart_teleport"] = "🌀 적 뒤로 순간이동!",
                ["assassin_hit_count"] = "암살 연속 타격: {0}/{1}",
                ["teleport_failed"] = "순간이동 실패!",
                ["enemy"] = "적",

                // === Bow Skills ===
                ["bow_explosive_cooldown"] = "폭발 화살 쿨다운 중! 남은 시간: {0}초",
                ["bow_required"] = "활을 착용해야 합니다",
                ["bow_explosive_success"] = "폭발 화살! {0} 데미지!",

                // === Crossbow Skills ===
                ["crossbow_oneshot_cooldown"] = "단 한 발 쿨다운 중! 남은 시간: {0}초",
                ["crossbow_required"] = "석궁을 착용해야 합니다",
                ["crossbow_equip_required"] = "석궁을 착용해야 합니다!",
                ["crossbow_oneshot_success"] = "단 한 발! {0} 데미지!",
                ["crossbow_oneshot_ready"] = "🎯 단 한 발 준비 완료!",
                ["crossbow_oneshot_remaining"] = "단 한 발 남은 시간: {0}초",
                ["crossbow_oneshot_expired"] = "단 한 발 효과 만료",
                ["crossbow_oneshot_activated"] = "💥 단 한 발 발동!",
                ["skill_cooldown_format"] = "{0} 쿨다운: {1}초",

                // === Staff Skills ===
                ["staff_dual_cast_cooldown"] = "연속 발사 쿨다운 중! 남은 시간: {0}초",
                ["staff_dual_cast_success"] = "연속 발사! 추가 마법 발사체!",
                ["staff_heal_cooldown"] = "힐 쿨다운 중! 남은 시간: {0}초",
                ["staff_heal_success"] = "힐! +{0} 체력 회복!",

                // === Job Active Skills (Y Key) ===
                ["job_skill_activated"] = "{0} 스킬 발동!",
                ["job_skill_no_job"] = "직업을 먼저 선택해야 합니다",

                // === Buff Display Messages ===
                ["counter_stance_buff"] = "반격 자세 +{0}%",
                ["blade_counter_buff"] = "칼날 되치기 +{0}%",
                ["woodcutting_efficiency_bonus"] = "벌목 효율: +{0}%",
                ["mining_efficiency_bonus"] = "채광 효율: +{0}%",
                ["building_durability_bonus"] = "건축 내구도 +{0}%",
                ["taunt_cooldown_display"] = "도발 쿨타임",

                // === Stealth System ===
                ["stealth_reason_attack"] = "공격",
                ["stealth_reason_damage"] = "피격",
                ["stealth_reason_expired"] = "시간 만료",

                // === General UI ===
                ["key"] = "키",
                ["skill_points"] = "스킬 포인트",
                ["reset_button"] = "초기화",
                ["skill_learned"] = "{0} 스킬 습득!",
                ["skill_already_learned"] = "이미 습득한 스킬입니다",
                ["skill_prerequisites_not_met"] = "선행 스킬을 먼저 습득해야 합니다",
                ["skill_insufficient_points"] = "스킬 포인트가 부족합니다",
                ["skill_level_required"] = "레벨 {0} 이상이 필요합니다",
                ["skill_acquire_title"] = "{0} 습득",
                ["skill_acquire_materials"] = "소모될 재료:",
                ["skill_acquire_confirm"] = "정말로 습득하시겠습니까?",

                // === Skill Types ===
                ["skill_type_passive"] = "패시브스킬",
                ["skill_type_active_y"] = "액티브스킬 (Y키)",
                ["skill_type_active_r"] = "액티브스킬 (Z키)",
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
                ["item_finewood"] = "질 좋은 나무",
                ["item_elderbark"] = "고대의 나무껍질",
                ["item_raspberry"] = "라즈베리",
                ["item_mushroom"] = "버섯",
                ["item_blueberries"] = "블루베리",
                ["item_copper_ore"] = "구리광석",
                ["item_iron_ore"] = "철 광석",
                ["item_iron_scrap"] = "고철",
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
                ["trophy_eikthyr_required"] = "에이크쉬르 트로피가 필요합니다",

                // === EpicMMO Connection Messages ===
                ["epicmmo_connected_title"] = "EpicMMO 연동됨",
                ["epicmmo_connected_detail"] = "EpicMMO 레벨 시스템과 연동되었습니다.\n자체 레벨 시스템이 비활성화됩니다.",

                // === MMO Difficulty Notification ===
                ["mmo_diff_notification_title"] = "몬스터 난이도 상향됨",
                ["mmo_diff_notification_detail"] = "사용 SP {0} → 별 출현 확률 +{1}%",

                // === Respawn Message ===
                ["respawn_message"] = "성장해서 다시 도전하세요~!",
            };
        }

        private static Dictionary<string, string> GetEnglish_GameMessages()
        {
            return new Dictionary<string, string>
            {
                // === Active Skill HUD Slots ===
                ["hud_slot_y"] = "Job",
                ["hud_slot_r"] = "Ranged",
                ["hud_slot_g"] = "Melee",
                ["hud_slot_h"] = "Secondary",

                // === Common Messages ===
                ["attack_speed_cap_warning"] = "Attack speed bonus cannot exceed {0}%.",
                ["move_speed_cap_warning"] = "Move speed bonus cannot exceed {0}%.",
                ["cooldown_remaining"] = "{0} on cooldown! Remaining: {1}s",
                ["skill_cooldown"] = "{0} skill cooldown: {1}s remaining",
                ["stamina_insufficient"] = "Not enough stamina",
                ["eitr_insufficient"] = "Not enough Eitr! (Required: {0})",
                ["no_enemies_in_range"] = "No enemies in range!",
                ["no_targets_in_range"] = "No enemies in range!",
                ["no_enemy_in_range"] = "No enemies within {0}m!",
                ["can_invest"] = "You can invest points",

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
                ["rogue_shadow_strike_activate"] = "💥 Shadow Strike!",
                ["rogue_aggro_protection_end"] = "Aggro protection ended",

                // === Tanker Skills ===
                ["tanker_taunt_cooldown"] = "Taunt on cooldown! Remaining: {0}s",
                ["tanker_shield_required"] = "Shield required",
                ["tanker_taunt_success"] = "Taunt! {0} enemies targeting you!",
                ["tanker_taunt_no_enemy"] = "No enemies to taunt nearby",
                ["tanker_passive_damage_reduction"] = "Tanker Passive: -{0} damage!",
                ["tanker_defense_buff_activated"] = "<color=#FFD700>⚔ Defense Buff Activated! ⚔</color>",

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

                // === Weapon Equip Required Messages ===
                ["sword_equip_required"] = "You must equip a sword!",
                ["dagger_equip_required"] = "You must equip a dagger!",
                ["spear_equip_required"] = "You must equip a spear!",
                ["polearm_equip_required"] = "You must equip a polearm!",
                ["one_hand_mace_shield_required"] = "You must equip a one-hand mace and shield!",
                ["two_hand_mace_required"] = "You must equip a two-hand mace!",
                ["staff_equip_required"] = "You must equip a staff!",
                ["r_key_skill_condition_not_met"] = "R key skill condition not met",
                ["g_key_skill_required"] = "G key skill required",
                ["h_key_skill_required"] = "H key skill required",

                // === Mace Skills ===
                ["mace_guardian_cooldown"] = "Guardian's Heart on cooldown! Remaining: {0}s",
                ["mace_required"] = "Mace required",
                ["mace_guardian_success"] = "Guardian's Heart! Defense +{0}%!",
                ["mace_fury_cooldown"] = "Fury Hammer on cooldown! Remaining: {0}s",
                ["mace_fury_success"] = "Fury Hammer! {0} damage!",
                ["fury_hammer_cooldown"] = "Fury Hammer cooldown: {0}s",
                ["fury_hammer_skill_required"] = "Fury Hammer skill required",
                ["fury_hammer_combo_hit"] = "Fury Hammer {0}! Damage {1}, {2} hit",
                ["fury_hammer_final_hit_ready"] = "🔨 Final hit ready!",
                ["fury_hammer_combo_complete"] = "🔥 Fury Hammer complete! {0} hit",
                ["guardian_heart_activated"] = "🛡️ Guardian's Heart activated!",
                ["guardian_heart_buff_name"] = "Guardian's Heart",
                ["guardian_heart_ended"] = "Guardian's Heart ended",
                ["guardian_heart_reflect"] = "🛡️ Reflect damage: {0}",
                ["guardian_heart_remaining"] = "Guardian's Heart remaining: {0}s",
                ["guardian_heart_skill_required"] = "Guardian's Heart skill required",
                ["mace_shield_required"] = "Shield required",

                // === Spear Combo Skills ===
                ["combo_spear_skill_required"] = "Combo Spear skill required",
                ["cooldown_seconds"] = "Cooldown: {0}s",
                ["combo_spear_buff_name"] = "Combo Spear",
                ["combo_spear_buff_activated"] = "⚡ Combo Spear activated! {0} uses (+{1}% damage)",
                ["combo_spear_buff_ended"] = "Combo Spear effect ended",
                ["combo_spear_uses_remaining"] = "🎯 Combo Spear remaining: {0}",
                ["combo_spear_retrieve_failed"] = "Failed to retrieve spear",
                ["combo_spear_retrieved_equipped"] = "✅ Spear retrieved and equipped!",
                ["combo_spear_inventory_full"] = "Inventory full - spear dropped in front of you",

                // === Polearm Skills ===
                ["polearm_king_cooldown"] = "Spear King on cooldown! Remaining: {0}s",
                ["polearm_required"] = "Polearm required",
                ["polearm_king_success"] = "Spear King! {0} damage!",

                // === Knife Skills ===
                ["knife_assassin_cooldown"] = "Assassin's Heart on cooldown! Remaining: {0}s",
                ["knife_required"] = "Knife required",
                ["knife_assassin_success"] = "Assassin's Heart! Crit damage +{0}%!",
                ["knife_expert_backstab"] = "🗡️ Backstab! +{0}% damage!",
                ["knife_evasion_buff"] = "🌀 Evasion Mastery! Evasion +{1}% for {0}s",
                ["knife_attack_evasion_buff"] = "⚔️ Attack Evasion! Evasion +{0}%, lasts {1}s",
                ["knife_combat_mastery"] = "⚔️ Combat Mastery! Attack +{0}%",
                ["knife_text_assassination_stagger"] = "💀 Assassination Stagger!",
                ["assassin_complete_hits"] = "Assassination {0} hits completed!",
                ["assassin_complete_return"] = "Assassination complete! Returning",
                ["assassin_heart_cooldown"] = "Assassin's Heart cooldown: {0}s",
                ["assassin_heart_crit"] = "🎯 Assassination Crit! +{0}% damage!",
                ["assassin_heart_teleport"] = "🌀 Teleport behind enemy!",
                ["assassin_hit_count"] = "Assassination combo: {0}/{1}",
                ["teleport_failed"] = "Teleport failed!",
                ["enemy"] = "enemy",

                // === Bow Skills ===
                ["bow_explosive_cooldown"] = "Explosive Arrow on cooldown! Remaining: {0}s",
                ["bow_required"] = "Bow required",
                ["bow_explosive_success"] = "Explosive Arrow! {0} damage!",

                // === Crossbow Skills ===
                ["crossbow_oneshot_cooldown"] = "One Shot on cooldown! Remaining: {0}s",
                ["crossbow_required"] = "Crossbow required",
                ["crossbow_equip_required"] = "You must equip a crossbow!",
                ["crossbow_oneshot_success"] = "One Shot! {0} damage!",
                ["crossbow_oneshot_ready"] = "🎯 One Shot Ready!",
                ["crossbow_oneshot_remaining"] = "One Shot remaining: {0}s",
                ["crossbow_oneshot_expired"] = "One Shot expired",
                ["crossbow_oneshot_activated"] = "💥 One Shot Activated!",
                ["skill_cooldown_format"] = "{0} cooldown: {1}s",

                // === Staff Skills ===
                ["staff_dual_cast_cooldown"] = "Rapid Barrage on cooldown! Remaining: {0}s",
                ["staff_dual_cast_success"] = "Rapid Barrage! Extra magic projectile!",
                ["staff_heal_cooldown"] = "Heal on cooldown! Remaining: {0}s",
                ["staff_heal_success"] = "Heal! +{0} HP!",

                // === Job Active Skills (Y Key) ===
                ["job_skill_activated"] = "{0} skill activated!",
                ["job_skill_no_job"] = "You must select a job first",

                // === Buff Display Messages ===
                ["counter_stance_buff"] = "Counter Stance +{0}%",
                ["blade_counter_buff"] = "Blade Counter +{0}%",
                ["woodcutting_efficiency_bonus"] = "Woodcutting Efficiency: +{0}%",
                ["mining_efficiency_bonus"] = "Mining Efficiency: +{0}%",
                ["building_durability_bonus"] = "Building Durability +{0}%",
                ["taunt_cooldown_display"] = "Taunt Cooldown",

                // === Skill Effect Messages ===
                ["skill_activated"] = "{0} Activated!",
                ["ground_stomp_effect"] = "Ground Stomp! ({0} enemies pushed)",
                ["stomp_30sec_remaining"] = "🦶 Ground Stomp ready in 30s!",
                ["stomp_ready"] = "🦶 Ground Stomp Ready!",
                ["shockwave_30sec_remaining"] = "⚡ Shockwave ready in 30s!",
                ["shockwave_ready"] = "⚡ Shockwave Ready!",
                ["luck_magic_activated"] = "✨ Luck Magic Activated!",
                ["staff_wand_required"] = "❌ You must equip a staff or wand!",

                // === Combat Effect Messages ===
                ["consecutive_attack"] = "Consecutive Attack!",
                ["critical_strike"] = "Critical Strike!",
                ["enhanced_strike"] = "Enhanced Strike! (+{0}%)",
                ["reflect_damage"] = "Reflect Damage {0}!",
                ["explosion_damage"] = "Explosion! +{0}",

                // === Spear Effect Messages ===
                ["spear_expert_damage"] = "Spear Expert Damage! (+{0}%)",
                ["evasion_thrust"] = "Evasion Thrust! (+{0}%)",
                ["evasion_thrust_ready"] = "Evasion Thrust Ready!",
                ["evasion_thrust_end"] = "Evasion Thrust Ended",
                ["explosion_spear"] = "Fast Spear! Attack Speed +{0}%",

                // === Polearm Effect Messages ===
                ["polearm_area_combo"] = "Area Combo! 2-hit damage +{0}%",
                ["polearm_cooldown_remaining"] = "Cooldown: {0}s remaining",
                ["polearm_stamina_insufficient"] = "Stamina Insufficient!",
                ["pierce_charge_in_progress"] = "Pierce Charge in progress",
                ["pierce_charge"] = "Pierce Charge!",
                ["pierce_charge_damage"] = "Pierce Charge! (+{0}%)",
                ["charge_complete"] = "Charge Complete",
                ["hero_strike_stagger"] = "Hero Strike! (Stagger)",
                ["wheel_attack"] = "Wheel Attack +{0}%!",
                ["storm_slash_first"] = "Storm Slash! 1st Slash +{0}%",
                ["storm_slash_explosion"] = "Storm Slash Explosion! +{0}%",
                ["polearm_boost_active"] = "⚔️ Polearm Boost +{0} Pierce!",
                ["suppress_active"] = "⚔️ Suppress Attack +{0}%!",

                // === Sword Effect Messages ===
                ["rush_slash_skill_required"] = "Rush Slash skill required",
                ["rush_slash_in_progress"] = "Rush Slash in progress",
                ["rush_slash_activate"] = "Rush Slash!",
                ["rush_slash_return"] = "Returning to original position!",
                ["rush_slash_complete"] = "Rush Slash Complete! ({0} hits)",
                ["rush_slash_canceled"] = "Rush Slash Canceled",
                ["parry_rush_skill_required"] = "Parry Rush skill required",
                ["shield_required"] = "You must equip a shield",
                ["parry_rush_already_active"] = "Parry Rush already active",
                ["parry_rush_activate"] = "Parry Rush! ({0}s)",
                ["parry_rush_damage"] = "Parry Rush! (+{0}%)",

                // === Berserker Effect Messages ===
                ["berserker_cooldown"] = "On cooldown ({0}s)",
                ["stamina_insufficient_short"] = "Stamina Insufficient!",
                ["already_rage_state"] = "Already in Rage state!",
                ["berserker_rage"] = "Berserker Rage!",
                ["death_ignore"] = "Death Ignore",

                // === Staff Heal Effect Messages ===
                ["heal_cooldown"] = "Heal Cooldown: {0}s",
                ["sacred_heal"] = "Staff Sacred Heal! ({0} healed)",
                ["no_heal_target"] = "No target to heal",

                // === Resource Consumption Messages ===
                ["equipment_consumed"] = "Equipment Consumed: {0}",
                ["material_consumed"] = "Material Consumed: {0}",

                // === Skill Tree Manager Messages ===
                ["cannot_learn_with"] = "Cannot learn with {0}",
                ["active_skill_ranged_only_one"] = "Only 1 ranged active skill allowed",
                ["active_skill_weapon_conflict"] = "Active skill from another weapon already selected",
                ["active_skill_job_only_one"] = "Only 1 job class allowed",
                ["job_class_one_only"] = "You can only select 1 job class",
                ["multishot_end"] = "Multishot Ended",

                // === Taunt Messages ===
                ["taunt_ready"] = "Taunt Ready!",

                // === Attack Expert Tree Effect Messages ===
                ["rage_bonus"] = "Extra Damage +{0}%",
                ["melee_specialization"] = "Melee Specialization!",
                ["bow_specialization"] = "Bow Specialization!",
                ["focus_fire_crit"] = "Focus Fire Crit x{0}!",
                ["basic_bow_damage"] = "Basic Bow Damage +{0}!",
                ["crossbow_specialization"] = "Crossbow Specialization!",
                ["one_shot"] = "One Shot! +{0}%",
                ["staff_specialization"] = "Staff Specialization!",
                ["precision_attack"] = "Precision Attack!",
                ["elemental_attack"] = "Elemental Attack!",
                ["ranged_enhance"] = "Ranged Enhance!",
                ["attack_expert"] = "Attack Expert!",
                ["melee_enhance"] = "Melee Enhance!",
                ["consecutive_melee_master"] = "Consecutive Melee Master!",
                ["energizer"] = "Energizer!",
                ["attack_increased"] = "Attack Increased!",
                ["attack_expert_acquired"] = "Attack Expert Acquired!",

                // === Speed Tree Effect Messages ===
                ["consecutive_flow"] = "Consecutive Flow!",
                ["bow_expert_mastery"] = "Bow Expert!",
                ["crossbow_expert_mastery"] = "Crossbow Expert!",
                ["agility_base"] = "Agility Basics! (+{0}% move speed)",
                ["melee_triple_combo"] = "Melee Triple Combo! (+{0}% attack speed)",
                ["cast_triple_combo"] = "Cast Triple Combo! (+{0} Eitr)",
                ["knockback_effect"] = "Knockback!",

                // === Bow/Crossbow Effect Messages ===
                ["multishot_skill"] = "Multishot {0}! (+{1} arrows)",
                ["explosive_arrow_skill_required"] = "Explosive Arrow skill required",
                ["explosive_arrow_ready"] = "Explosive Arrow Ready!",
                ["explosive_arrow_fire"] = "Explosive Arrow Fired!",

                // === JobSkills Messages ===
                ["archer_multishot_cooldown"] = "Multishot Cooldown: {0}s",
                ["archer_multishot_fired"] = "Multishot! ({0} arrows fired)",

                // === EpicMMO Connection Messages ===
                ["epicmmo_connected_title"] = "EpicMMO Connected",
                ["epicmmo_connected_detail"] = "Connected to EpicMMO level system.\nCaptain level system disabled.",

                // === MMO Difficulty Notification ===
                ["mmo_diff_notification_title"] = "Monster Difficulty Increased",
                ["mmo_diff_notification_detail"] = "Used SP {0} → Star chance +{1}%",

                // === Respawn Message ===
                ["respawn_message"] = "Grow stronger and challenge again~!",
            };
        }
    }
}

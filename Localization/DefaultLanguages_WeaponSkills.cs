using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class DefaultLanguages
    {
        private static Dictionary<string, string> GetKorean_WeaponSkills()
        {
            return new Dictionary<string, string>
            {
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
                ["sword_desc_rush_slash_path"] = "이동 경로의 모든 몬스터 적중",
                ["rush_slash_path_hit"] = "경로 베기!",
                ["sword_desc_fast_slash"] = "공격속도 +{0}%",
                ["sword_desc_counter"] = "패링 성공 후 {0}초동안 방어력 +{1}%",
                ["sword_desc_combo"] = "3연속 공격력 +{0}% ({1}초)",
                ["sword_desc_riposte"] = "베기 공격력 +{0}",
                ["sword_desc_all_in_one"] = "양손검 착용 시 공격력 +{0}%, 막기 방어력 +{1}%",
                ["sword_desc_duel"] = "공격 속도 +{0}%",
                ["sword_desc_parry_rush"] = "{0}초 동안 패링 성공 시 몬스터에게 방패돌격",
                ["sword_desc_parry_rush_damage"] = "막기력 x{0}% 타격 데미지",
                ["requirement_two_handed_sword"] = "양손검 착용",
                ["requirement_sword_or_shield_equip"] = "검 또는 방패 착용",
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
                ["archer_desc_passive"] = "점프 높이 +{0}%, 낙하 데미지 -{1}%",
                ["archer_range_arrows"] = "화살 {0}개 발사",

                // === Mace Skill Names ===
                ["mace_skill_expert"] = "둔기 전문가",
                ["mace_skill_guardian"] = "수호자의 진심",
                ["mace_skill_fury"] = "분노의 망치",
                ["mace_skill_damage_boost"] = "둔기 강화",
                ["mace_skill_stun_boost"] = "기절 강화",
                ["mace_skill_guard_boost"] = "회전 타격",
                ["mace_skill_heavy_strike"] = "무거운 일격",
                ["mace_skill_knockback"] = "밀어내기",
                ["mace_skill_tanker"] = "탱커",
                ["mace_skill_dps_boost"] = "데미지 강화",
                ["mace_skill_grandmaster"] = "속공",

                // === Mace Skill Descriptions ===
                ["mace_desc_expert"] = "공격력 +{0}%, 기절 확률 +{1}%, 기절 지속시간 +{2}초",
                ["mace_desc_damage_boost"] = "둔기 공격력 +{0}%",
                ["mace_desc_stun_boost"] = "기절 확률 +{0}%, 기절 지속시간 +{1}초",
                ["mace_desc_guard_boost"] = "세컨드 공격 시 공격력 +{0}%, 범위 {1}m",
                ["mace_desc_heavy_strike"] = "타격 +{0}",
                ["mace_desc_knockback"] = "막기 미사용 상태에서 피격 시 {0}% 확률로 공격자를 밀어냄",
                ["mace_desc_tanker"] = "체력 +{0}, 피해 감소 +{1}%",
                ["mace_desc_dps_boost"] = "공격력 +{0}%",
                ["mace_desc_grandmaster"] = "공격속도 +{0}%",
                ["spin_attack_hit"] = "🌀 회전 타격 +{0}%",
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
                ["spear_skill_explosion"] = "빠른창",
                ["spear_skill_dual"] = "이연창",

                // === Spear Skill Descriptions ===
                ["spear_desc_expert"] = "{0}% 확률로 공격 시 {1}% 공격속도로 1회 추가 공격 발동",
                ["spear_expert_proc"] = "번개 일격!",
                ["spear_desc_throw"] = "창 던지기 공격력 +{0}%",
                ["spear_desc_crit"] = "창 공격력 +{0}%",
                ["spear_desc_pierce"] = "관통 공격력 +{0}",
                ["spear_desc_evasion"] = "공격 시 5초 동안 회피 +{0}%, 공격 스태미나 -{1}%",
                ["spear_evasion_buff"] = "회피 찌르기: 회피 +{0}% (5초)",
                ["spear_desc_explosion"] = "공격 속도 +{0}%",
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
                ["polearm_skill_ground"] = "폭풍베기",
                ["polearm_skill_moon"] = "반달 베기",
                ["polearm_skill_charge"] = "폴암강화",

                // === Polearm Skill Descriptions ===
                ["polearm_desc_expert"] = "공격 범위 +{0}%",
                ["polearm_desc_spin"] = "특수(휠 마우스) 공격력 +{0}%",
                ["polearm_desc_suppress"] = "공격력 +{0}%",
                ["polearm_desc_hero"] = "{0}% 확률로 스태거",
                ["polearm_desc_area"] = "2연속 공격 시 공격력 +{0}%({1}초동안)",
                ["polearm_desc_ground"] = "1차 공격 후 4초 이내 특수(휠 마우스) 공격 시 번개속성 +{0} 추가",
                ["polearm_desc_moon"] = "공격 범위 +{0}%, 공격 스태미나 -{1}%",
                ["polearm_desc_charge"] = "관통 공격력 +{0}",
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
                ["knife_move_speed_buff"] = "빠른 움직임! 이동속도 +{0}%",
                ["knife_desc_attack_speed"] = "베기 공격력 +{0}, 관통 공격력 +{0}",
                ["knife_desc_crit_rate"] = "치명타 확률 +{0}%",
                ["knife_desc_attack_evasion"] = "공격 시 {1}초간 회피 +{0}%",
                ["knife_desc_combat_damage"] = "공격력 +{0}%",
                ["knife_desc_execution"] = "치명타 피해 +{0}%, 치명타 확률 +{1}%",
                ["knife_desc_assassination"] = "{1}연속 공격 시 {0}% 확률로 스태거",
                ["knife_desc_assassin_main"] = "{0}m 이내 적의 뒤로 순간이동\n대상 {2}초 스턴 + {3}회 연속 공격\n공격 완료 후 원래 위치로 복귀",
                ["knife_desc_assassin_note"] = "암살자의 모든 능력을 극한까지 끌어올리는 궁극기",
                ["knife_desc_assassin_note2"] = "{0}m 이내 적이 없으면 스킬 취소",
                ["requirement_knife_claw"] = "단검 또는 클로 착용",

                // === Staff Skill Names ===
                ["staff_skill_expert"] = "지팡이 전문가",
                ["staff_skill_dual_cast"] = "연속 발사",
                ["staff_skill_heal"] = "힐",

                // === Staff Skill Descriptions ===
                ["staff_desc_dual_cast"] = "{0}발 추가 마법 발사체를 0.25초 간격으로 연속 발사",
                ["staff_desc_dual_cast_angle"] = "발사 각도: 0도 (동일 방향)",
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
                ["archer_upgrade_title"] = "⚔️ 아처 직업 승급",
                ["ui_reset_confirm_title"] = "스킬 초기화 확인",
                ["ui_reset_confirm_message"] = "정말로 모든 스킬을 초기화하시겠습니까?\n이 작업은 되돌릴 수 없습니다.",
                ["ui_reset_production"] = "생산 초기화",
                ["ui_reset_production_confirm_title"] = "생산 전문가 초기화 확인",
                ["ui_reset_production_confirm_message"] = "생산 전문가 스킬을 초기화하시겠습니까?\n이 작업은 되돌릴 수 없습니다.",
                ["ui_reset_job"] = "직업 초기화",
                ["ui_reset_job_confirm_title"] = "직업 초기화 확인",
                ["ui_reset_job_confirm_message"] = "직업 스킬을 초기화하시겠습니까?\n이 작업은 되돌릴 수 없습니다.",
                ["ui_music_on"] = "Music On",
                ["ui_music_off"] = "Music Off",

                // === Skill Investment Messages ===
                ["skill_insufficient_points_detail"] = "스킬 포인트가 부족합니다. (필요: {0}, 보유: {1})",
                ["skill_invest_success"] = "✅ 스킬 포인트 투자 확정!",
                ["items_insufficient"] = "필요한 아이템이 부족합니다",

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
                ["archer_passive_skills"] = "점프 높이 +{0}%, 낙하 데미지 -{1}%",
                ["archer_passive_lv1"] = "점프 +{0}%, 낙하 데미지 -{1}%",
                ["archer_passive_lv2"] = "점프 +{0}%, 독 저항 +{1}%",
                ["archer_passive_lv3"] = "점프 +{0}%, 낙하 데미지 -{1}%, 독/냉기 저항 +{2}%",
                ["archer_passive_lv4"] = "점프 +{0}%, 낙하 데미지 -{1}%, 독/냉기/화염 저항 +{2}%",
                ["archer_passive_lv5"] = "점프 +{0}%, 낙하 데미지 -{1}%, 독/냉기/화염/번개 저항 +{2}%",
                ["requirement_archer"] = "활 착용, 아처 직업",
                ["archer_current_level"] = "현재 레벨",
                ["archer_max_level"] = "★ 최대 레벨 달성",
                ["archer_next_level_cost"] = "Lv{0} 업그레이드 재료",
                ["archer_effect_arrows"] = "{0}발씩 {1}회 발사, 1발당 {2}% 데미지",
                ["archer_level_item_required"] = "아처 Lv{0} 업그레이드 재료가 부족합니다",
                ["archer_upgrade_confirm"] = "아처 Lv{0}로 업그레이드 하겠습니까?",
                ["archer_missing_items"] = "부족: {0}",
                ["item_trophy_skeleton"] = "해골 트로피",
                ["item_trophy_greydwarf"] = "그레이드워프 트로피",
                ["item_trophy_greydwarfbrute"] = "그레이드워프 브루트 트로피",
                ["item_trophy_greydwarfshaman"] = "그레이드워프 샤먼 트로피",
                ["item_trophy_troll"] = "트롤 트로피",
                ["item_trophy_theelder"] = "엘더 트로피",
                ["item_trophy_abomination"] = "어보미네이션 트로피",
                ["item_trophy_bonemass"] = "본메쉬 트로피",
                ["item_trophy_hatchling"] = "드레이크 트로피",
                ["item_trophy_dragonqueen"] = "모더 트로피",
                ["item_trophy_goblinking"] = "야글루스 트로피",
                ["item_trophy_thequeen"] = "여왕 트로피",
                ["item_trophy_frosttroll"] = "서리 트롤 트로피",
                ["item_trophy_seekerqueen"] = "시커 여왕 트로피",

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
                ["berserker_passive_desc"] = "스태미나 재생 +20%, 최대 체력 +{3}%, 체력 {0}% 이하 시 {1}초간 무적 (쿨타임 {2}분)",
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
                ["rogue_passive_desc"] = "공격 속도 +{0}%, 스태미나 사용 -{1}%, 속성 저항 +{2}%",
                ["requirement_rogue"] = "단검, 클로 착용, 로그 직업",

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
                ["staff_desc_dual_cast_fallback"] = "7발 추가 마법 발사체를 0.25초 간격으로 연속 발사",
                ["staff_desc_dual_cast_damage_fallback"] = "지팡이/완드 공격력의 15%",
            };
        }

        private static Dictionary<string, string> GetEnglish_WeaponSkills()
        {
            return new Dictionary<string, string>
            {
                // === Stealth System ===
                ["stealth_reason_attack"] = "Attack",
                ["stealth_reason_damage"] = "Damage taken",
                ["stealth_reason_expired"] = "Time expired",

                // === General UI ===
                ["key"] = "key",
                ["skill_points"] = "Skill Points",
                ["reset_button"] = "Reset",
                ["skill_learned"] = "{0} skill learned!",
                ["skill_already_learned"] = "Skill already learned",
                ["skill_prerequisites_not_met"] = "Prerequisites not met",
                ["skill_insufficient_points"] = "Not enough skill points",
                ["skill_level_required"] = "Level {0} required",
                ["skill_acquire_title"] = "{0} Acquire",
                ["skill_acquire_materials"] = "Required Materials:",
                ["skill_acquire_confirm"] = "Do you really want to learn this skill?",

                // === Skill Types ===
                ["skill_type_passive"] = "Passive Skill",
                ["skill_type_active_y"] = "Active Skill (Y Key)",
                ["skill_type_active_r"] = "Active Skill (Z Key)",
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
                ["item_finewood"] = "Fine Wood",
                ["item_elderbark"] = "Ancient Bark",
                ["item_raspberry"] = "Raspberry",
                ["item_mushroom"] = "Mushroom",
                ["item_blueberries"] = "Blueberries",
                ["item_copper_ore"] = "Copper Ore",
                ["item_iron_ore"] = "Iron Ore",
                ["item_iron_scrap"] = "Iron Scrap",
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
                ["trophy_eikthyr_required"] = "Eikthyr Trophy required",

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
                ["sword_desc_rush_slash_path"] = "Hits all monsters along movement path",
                ["rush_slash_path_hit"] = "Path Slash!",
                ["sword_desc_fast_slash"] = "Attack speed +{0}%",
                ["sword_desc_counter"] = "Defense +{1}% for {0}s after successful parry",
                ["sword_desc_combo"] = "3-hit combo damage +{0}% ({1}s)",
                ["sword_desc_riposte"] = "Slash damage +{0}",
                ["sword_desc_all_in_one"] = "Two-Handed Sword: Attack +{0}%, Block Power +{1}%",
                ["sword_desc_duel"] = "Attack speed +{0}%",
                ["sword_desc_parry_rush"] = "Shield charge on successful parry for {0}s",
                ["sword_desc_parry_rush_damage"] = "Block Power x{0}% Blunt Damage",
                ["requirement_two_handed_sword"] = "Two-Handed Sword equipped",
                ["requirement_sword_or_shield_equip"] = "Sword or Shield equipped",
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
                ["mace_skill_guard_boost"] = "Spin Strike",
                ["mace_skill_heavy_strike"] = "Heavy Strike",
                ["mace_skill_knockback"] = "Knockback",
                ["mace_skill_tanker"] = "Tanker",
                ["mace_skill_dps_boost"] = "DPS Enhancement",
                ["mace_skill_grandmaster"] = "Swift Attack",

                // === Mace Skill Descriptions ===
                ["mace_desc_expert"] = "Damage +{0}%, Stun chance +{1}%, Stun duration +{2}s",
                ["mace_desc_damage_boost"] = "Mace damage +{0}%",
                ["mace_desc_stun_boost"] = "Stun chance +{0}%, Stun duration +{1}s",
                ["mace_desc_guard_boost"] = "Secondary attack damage +{0}%, AoE range {1}m",
                ["mace_desc_heavy_strike"] = "Blunt +{0}",
                ["mace_desc_knockback"] = "When not blocking, {0}% chance to push attacker on hit",
                ["mace_desc_tanker"] = "Health +{0}, Damage reduction +{1}%",
                ["mace_desc_dps_boost"] = "Damage +{0}%",
                ["mace_desc_grandmaster"] = "Attack speed +{0}%",
                ["spin_attack_hit"] = "🌀 Spin Strike +{0}%",
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
                ["spear_skill_explosion"] = "Fast Spear",
                ["spear_skill_dual"] = "Dual Thrust",

                // === Spear Skill Descriptions ===
                ["spear_desc_expert"] = "{0}% chance on hit to trigger 1 extra attack at {1}% speed",
                ["spear_expert_proc"] = "Lightning Strike!",
                ["spear_desc_throw"] = "Spear throw damage +{0}%",
                ["spear_desc_crit"] = "Spear damage +{0}%",
                ["spear_desc_pierce"] = "Pierce damage +{0}",
                ["spear_desc_evasion"] = "On attack: Evasion +{0}% for 5s, Attack stamina -{1}%",
                ["spear_evasion_buff"] = "Evasion Strike: Evasion +{0}% (5s)",
                ["spear_desc_explosion"] = "Attack Speed +{0}%",
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
                ["polearm_skill_ground"] = "Storm Slash",
                ["polearm_skill_moon"] = "Crescent Slash",
                ["polearm_skill_charge"] = "Polearm Enhancement",

                // === Polearm Skill Descriptions ===
                ["polearm_desc_expert"] = "Attack range +{0}%",
                ["polearm_desc_spin"] = "Special (wheel mouse) attack damage +{0}%",
                ["polearm_desc_suppress"] = "Damage +{0}%",
                ["polearm_desc_hero"] = "{0}% chance to stagger",
                ["polearm_desc_area"] = "2-hit combo: Damage +{0}% ({1}s)",
                ["polearm_desc_ground"] = "After 1st attack, within 4s: wheel mouse attack adds +{0} lightning",
                ["polearm_desc_moon"] = "Attack range +{0}%, Stamina cost -{1}%",
                ["polearm_desc_charge"] = "Pierce damage +{0}",
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
                ["knife_move_speed_buff"] = "Quick Movement! Move speed +{0}%",
                ["knife_desc_attack_speed"] = "Slash +{0}, Pierce +{0}",
                ["knife_desc_crit_rate"] = "Crit chance +{0}%",
                ["knife_desc_attack_evasion"] = "On attack: +{0}% evasion for {1}s",
                ["knife_desc_combat_damage"] = "Attack power +{0}%",
                ["knife_desc_execution"] = "Crit damage +{0}%, Crit chance +{1}%",
                ["knife_desc_assassination"] = "{0}% chance to stagger on {1} consecutive hits",
                ["knife_desc_assassin_main"] = "Teleport behind enemy within {0}m\nStun target for {2}s + {3} consecutive attacks\nReturn to original position after attacks",
                ["knife_desc_assassin_note"] = "Ultimate skill that pushes all assassin abilities to the limit",
                ["knife_desc_assassin_note2"] = "Skill cancelled if no enemy within {0}m",
                ["requirement_knife_claw"] = "Knife or Claw equipped",

                // === Staff Skill Names ===
                ["staff_skill_expert"] = "Staff Expert",
                ["staff_skill_dual_cast"] = "Rapid Barrage",
                ["staff_skill_heal"] = "Heal",

                // === Staff Skill Descriptions ===
                ["staff_desc_dual_cast"] = "Fire {0} additional magic projectiles in rapid succession (0.25s interval)",
                ["staff_desc_dual_cast_angle"] = "Fire angle: 0° (same direction)",
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
                ["archer_upgrade_title"] = "⚔️ Archer Class Ascension",
                ["ui_reset_confirm_title"] = "Reset Skills Confirm",
                ["ui_reset_confirm_message"] = "Are you sure you want to reset all skills?\nThis action cannot be undone.",
                ["ui_reset_production"] = "Reset Pd",
                ["ui_reset_production_confirm_title"] = "Reset Production Expert Confirm",
                ["ui_reset_production_confirm_message"] = "Are you sure you want to reset Production Expert skills?\nThis action cannot be undone.",
                ["ui_reset_job"] = "Reset Job",
                ["ui_reset_job_confirm_title"] = "Reset Job Confirm",
                ["ui_reset_job_confirm_message"] = "Are you sure you want to reset job skills?\nThis action cannot be undone.",
                ["ui_music_on"] = "Music On",
                ["ui_music_off"] = "Music Off",

                // === Skill Investment Messages ===
                ["skill_insufficient_points_detail"] = "Not enough skill points. (Required: {0}, Available: {1})",
                ["skill_invest_success"] = "✅ Skill points invested!",
                ["items_insufficient"] = "Insufficient items required",

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
            };
        }
    }
}

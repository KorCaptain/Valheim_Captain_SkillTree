using System.Collections.Generic;

namespace CaptainSkillTree.Localization
{
    public static partial class DefaultLanguages
    {
        private static Dictionary<string, string> GetKorean_JobExpert()
        {
            return new Dictionary<string, string>
            {
                // === Expert Tree Skill Descriptions ===
                // Attack Expert Tree
                ["attack_root_desc"] = "모든 공격력 +{0}% (물리, 속성)",
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
                ["defense_root_desc"] = "체력 +{0}, 투구 방어력 +{1}",
                ["defense_survival_desc"] = "체력 +{0}, 흉갑 방어력 +{1}",
                ["defense_health_desc"] = "체력 +{0}, 각반 방어력 +{1}",
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
                ["defense_attack_desc"] = "30초 동안 피격 회피 미발동 시 회피 +{0}%",
                ["defense_body_desc"] = "체력 최대치 +{0}%, 물리/속성 저항 +{1}%",
                ["defense_true_desc"] = "블럭 스태미나 -{0}%, 일반 방패 이동속도 +{1}%, 대형 방패 이동속도 +{2}%",

                // Defense Expert Tree - Effect Texts
                ["defense_root_effect"] = "🛡️ 방어 전문가! 체력 +{0}, 방어 +{1}",
                ["defense_shield_effect"] = "🛡️ 방패훈련! 방패 방어력 +{0}",
                ["defense_parry_effect"] = "🛡️ 막기달인! 패링 +{0}초, 방패 방어력 +{1}",
                ["defense_body_effect"] = "🛡️ 요툰의 생명력! 체력 +{0}%, 저항 +{1}%",

                // Staff Expert Tree
                ["staff_expert_desc"] = "속성 공격 +{0}%",
                ["staff_focus_desc"] = "에이트르 소모 -{0}%",
                ["staff_stream_desc"] = "최대 에이트르 +{0}",
                ["staff_frost_desc"] = "냉기 공격 +{0}",
                ["staff_fire_desc"] = "화염 공격 +{0}",
                ["staff_lightning_desc"] = "번개 공격 +{0}",
                ["staff_luck_mana_desc"] = "{0}% 확률로 에이트르 소모 없음",

                // Mace Expert Tree (additional)
                ["mace_expert_desc2"] = "둔기 피해 +{0}%, 공격 시 {1}% 확률로 {2}초 기절",
                ["mace_stun_boost_desc2"] = "기절 확률 +{0}%, 지속시간 +{1}초",
                ["mace_guard_boost_desc2"] = "세컨드 공격 시 공격력 +{0}%, 범위 {1}m",
                ["mace_heavy_strike_desc2"] = "무거운 공격 타격 +{0}",
                ["mace_knockback_desc2"] = "막기 미사용 피격 시 {0}% 확률로 공격자 밀어냄",
                ["mace_tank_desc2"] = "체력 +{0}%, 받는 데미지 -{1}%",
                ["mace_dps_desc2"] = "공격력 +{0}%",
                ["mace_grandmaster_desc2"] = "공격속도 +{0}%",

                // === Prerequisite Text ===
                ["prerequisite_label"] = "🔗 필요",
                ["prerequisite_connector_or"] = " 또는 ",
                ["prerequisite_labor_craft"] = "노가다 전문가 + 제작 전문가",
                ["or_separator"] = "또는",
                ["skill_prerequisite_any_required"] = "선행 스킬 중 하나 이상 필요: {0}",

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
                ["level_up_message"] = "<color=green>레벨 업!</color>\n+{0} 스킬포인트 (Lv.{1})",
                ["mmo_level_sync_message"] = "<color=#00BFFF>Lv.{0} / 스킬포인트. {1}</color>",

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

                // === Tanker Skill Messages ===
                ["tanker_defense_buff_activated"] = "<color=#FFD700>⚔ 방어 버프 활성화! ⚔</color>",
                ["tanker_shield_required"] = "방패 착용이 필요합니다",
                ["stamina_insufficient"] = "스태미나가 부족합니다",

                // === Rogue Active Skill Messages ===
                ["rogue_shadow_strike_activate"] = "💥 그림자 일격!",

                // === Skill Effect Messages ===
                ["skill_activated"] = "{0} 발동!",
                ["ground_stomp_effect"] = "발구르기! ({0}명 밀어냄)",
                ["stomp_30sec_remaining"] = "🦶 발구르기 30초 후 준비!",
                ["stomp_ready"] = "🦶 발구르기 준비완료!",
                ["shockwave_30sec_remaining"] = "⚡ 충격파방출 30초 후 준비!",
                ["shockwave_ready"] = "⚡ 충격파방출 준비완료!",
                ["luck_magic_activated"] = "✨ 행운 마력 발동!",
                ["staff_wand_required"] = "❌ 지팡이나 완드를 착용해야 합니다!",

                // === Combat Effect Messages ===
                ["consecutive_attack"] = "연속 공격!",
                ["critical_strike"] = "치명적 일격!",
                ["enhanced_strike"] = "강화된 일격! (+{0}%)",
                ["reflect_damage"] = "반사 데미지 {0}!",
                ["explosion_damage"] = "폭발! +{0}",

                // === Spear Effect Messages ===
                ["spear_expert_damage"] = "창 전문가 공격력! (+{0}%)",
                ["evasion_thrust"] = "회피 찌르기! (+{0}%)",
                ["evasion_thrust_ready"] = "회피 찌르기 준비!",
                ["evasion_thrust_end"] = "회피 찌르기 종료",
                ["explosion_spear"] = "빠른창! 공격 속도 +{0}%",

                // === Polearm Effect Messages ===
                ["polearm_area_combo"] = "광역 강타! 2연속 공격력 +{0}%",
                ["polearm_cooldown_remaining"] = "쿨타임: {0}초 남음",
                ["polearm_stamina_insufficient"] = "스태미나 부족!",
                ["polearm_required"] = "폴암을 착용해야 합니다!",
                ["pierce_charge_in_progress"] = "관통 돌격 실행 중",
                ["pierce_charge"] = "관통 돌격!",
                ["pierce_charge_damage"] = "관통 돌격! (+{0}%)",
                ["charge_complete"] = "돌진 완료",
                ["hero_strike_stagger"] = "영웅 타격! (스태거)",
                ["wheel_attack"] = "휠 공격 +{0}%!",

                // === Sword Effect Messages ===
                ["rush_slash_skill_required"] = "돌진 연속 베기 스킬이 필요합니다",
                ["sword_required"] = "검을 착용해야 합니다",
                ["cooldown_remaining"] = "쿨타임: {0}초",
                ["rush_slash_in_progress"] = "돌진 연속 베기 실행 중",
                ["rush_slash_activate"] = "돌진 연속 베기!",
                ["rush_slash_return"] = "원래 위치로 복귀!",
                ["rush_slash_complete"] = "돌진 연속 베기 완료! ({0}타격)",
                ["rush_slash_canceled"] = "돌진 연속 베기 중단됨",
                ["parry_rush_skill_required"] = "패링 돌격 스킬이 필요합니다",
                ["shield_required"] = "방패를 착용해야 합니다",
                ["sword_or_shield_required"] = "검 또는 방패를 착용해야 합니다",
                ["parry_rush_already_active"] = "패링 돌격 이미 활성 중",
                ["parry_rush_activate"] = "패링 돌격! ({0}초)",
                ["parry_rush_damage"] = "패링 돌격! (+{0}%)",

                // === Berserker Effect Messages ===
                ["berserker_cooldown"] = "쿨다운 중 ({0}초)",
                ["stamina_insufficient_short"] = "스태미나 부족!",
                ["already_rage_state"] = "이미 분노 상태!",
                ["berserker_rage"] = "버서커 분노!",
                ["death_ignore"] = "죽음의 무시",
                ["berserker_ranged_30pct"] = "(원거리 공격력은 30%만 적용)",

                // === Staff Heal Effect Messages ===
                ["heal_cooldown"] = "힐 쿨타임: {0}초",
                ["sacred_heal"] = "지팡이 신성한 치유! ({0}명 치료)",
                ["no_heal_target"] = "치료할 대상이 없습니다",

                // === Resource Consumption Messages ===
                ["equipment_consumed"] = "장비 소모: {0}",
                ["material_consumed"] = "재료 소모: {0}",

                // === Skill Tree Manager Messages ===
                ["cannot_learn_with"] = "{0}과(와) 동시에 배울 수 없습니다",
                ["active_skill_ranged_only_one"] = "원거리 액티브 스킬은 1개만 선택 가능합니다",
                ["active_skill_weapon_conflict"] = "다른 무기의 액티브 스킬이 이미 선택되어 있습니다",
                ["active_skill_job_only_one"] = "직업 액티브 스킬은 1개만 선택 가능합니다",
                ["job_class_one_only"] = "직업은 1개만 선택 가능합니다",
                ["multishot_end"] = "멀티샷 종료",

                // === Taunt Messages ===
                ["taunt_ready"] = "도발 준비 완료!",

                // === Attack Expert Tree Effect Messages ===
                ["rage_bonus"] = "추가 데미지 +{0}%",
                ["melee_specialization"] = "근접 특화!",
                ["bow_specialization"] = "활 특화!",
                ["focus_fire_crit"] = "집중 사격 치명타 x{0}!",
                ["basic_bow_damage"] = "기본 활공격 +{0}!",
                ["crossbow_specialization"] = "석궁 특화!",
                ["one_shot"] = "단 한 발! +{0}%",
                ["staff_specialization"] = "지팡이 특화!",
                ["precision_attack"] = "정밀 공격!",
                ["elemental_attack"] = "속성 공격!",
                ["ranged_enhance"] = "원거리 강화!",
                ["attack_expert"] = "공격 전문가!",
                ["melee_enhance"] = "근접 강화!",
                ["consecutive_melee_master"] = "연속 근접의 대가!",
                ["energizer"] = "에너자이져!",
                ["attack_increased"] = "공격 증가!",
                ["attack_expert_acquired"] = "공격 전문가 습득!",

                // === Speed Tree Effect Messages ===
                ["consecutive_flow"] = "연속의 흐름!",
                ["bow_expert_mastery"] = "활 숙련자!",
                ["crossbow_expert_mastery"] = "석궁 숙련자!",
                ["agility_base"] = "민첩함의 기초! (+{0}% 이동속도)",
                ["melee_triple_combo"] = "근접 가속 3연속! (+{0}% 공격속도)",
                ["cast_triple_combo"] = "시전 가속 3연속! (+{0} 에이트르)",
                ["knockback_effect"] = "넉백!",

                // === Bow/Crossbow Effect Messages ===
                ["multishot_skill"] = "멀티샷 {0}! (+{1}발)",
                ["explosive_arrow_skill_required"] = "폭발 화살 스킬이 필요합니다",
                ["explosive_arrow_ready"] = "폭발 화살 준비 완료!",
                ["explosive_arrow_fire"] = "폭발 화살 발사!",

                // === JobSkills Messages ===
                ["archer_multishot_cooldown"] = "멀티샷 쿨타임: {0}초",
                ["archer_multishot_fired"] = "멀틴샷! ({0}발 발사)",

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
                ["speed_ex1_effect"] = "⚔️ 수련자1 습득! 근접/석궁 숙련도 +{0}",
                ["speed_ex2_effect"] = "🏹 수련자2 습득! 지팡이/활 숙련도 +{0}",
                ["all_master_effect"] = "🏃 숙련자 습득! 달리기/점프 숙련도 +{0}",
                ["agility_peak_effect"] = "🦘 점프 숙련자 습득! 점프 숙련도 +{0}",

                // === Archer Tooltip Keys ===
                ["archer_level_req_items"] = "Lv{0} 필요아이템",
                ["archer_preview_arrows_damage"] = "추가 발사체 +{0}, 1발당 공격력 {1}%",
                ["archer_preview_charges"] = "추가 발사 회수 +{0}",
                ["archer_job_requirement"] = "활 착용, 아처 직업",
            };
        }

        private static Dictionary<string, string> GetEnglish_JobExpert()
        {
            return new Dictionary<string, string>
            {
                // === Archer Job ===
                ["archer_desc_multishot_fallback"] = "Fires 5 arrows x2 times.",
                ["archer_desc_arrow_damage_fallback"] = "Each arrow deals 50% of bow+arrow damage",
                ["archer_passive_skills"] = "Jump height +{0}%, Fall damage -{1}%",
                ["archer_passive_lv1"] = "Jump +{0}%, Fall Damage -{1}%",
                ["archer_passive_lv2"] = "Jump +{0}%, Poison Resist +{1}%",
                ["archer_passive_lv3"] = "Jump +{0}%, Fall Damage -{1}%, Poison/Cold Resist +{2}%",
                ["archer_passive_lv4"] = "Jump +{0}%, Fall Damage -{1}%, Poison/Cold/Fire Resist +{2}%",
                ["archer_passive_lv5"] = "Jump +{0}%, Fall Damage -{1}%, All Elemental Resist +{2}%",
                ["requirement_archer"] = "Bow equipped, Archer job",
                ["archer_current_level"] = "Current Level",
                ["archer_max_level"] = "★ Max Level Reached",
                ["archer_next_level_cost"] = "Lv{0} Upgrade Materials",
                ["archer_effect_arrows"] = "Fire {0} arrows x{1}, each deals {2}% damage",
                ["archer_level_item_required"] = "Not enough materials for Archer Lv{0} upgrade",
                ["archer_upgrade_confirm"] = "Upgrade Archer to Lv{0}?",
                ["archer_missing_items"] = "Missing: {0}",
                ["item_trophy_skeleton"] = "Skeleton Trophy",
                ["item_trophy_greydwarf"] = "Greydwarf Trophy",
                ["item_trophy_greydwarfbrute"] = "Greydwarf Brute Trophy",
                ["item_trophy_greydwarfshaman"] = "Greydwarf Shaman Trophy",
                ["item_trophy_troll"] = "Troll Trophy",
                ["item_trophy_theelder"] = "The Elder Trophy",
                ["item_trophy_abomination"] = "Abomination Trophy",
                ["item_trophy_bonemass"] = "Bonemass Trophy",
                ["item_trophy_hatchling"] = "Drake Trophy",
                ["item_trophy_dragonqueen"] = "Moder Trophy",
                ["item_trophy_goblinking"] = "Yagluth Trophy",
                ["item_trophy_thequeen"] = "The Queen Trophy",
                ["item_trophy_frosttroll"] = "Frost Troll Trophy",
                ["item_trophy_seekerqueen"] = "Seeker Queen Trophy",

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
                ["berserker_ranged_30pct"] = "(30% effectiveness for ranged attacks)",
                ["berserker_rage_effect"] = "Rage Effect",
                ["berserker_rage_damage_per_health"] = "+{0}% damage per 1% health lost",
                ["berserker_passive_desc"] = "Stamina regen +20%, Max HP +{3}%, Invincible for {1}s when health below {0}% (cooldown {2}min)",
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
                ["rogue_passive_desc"] = "Attack speed +{0}%, Stamina use -{1}%, Elemental resist +{2}%",
                ["requirement_rogue"] = "Knife or Claw equipped, Rogue job",

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
                ["staff_desc_dual_cast_fallback"] = "Fire 7 additional magic projectiles in rapid succession (0.25s interval)",
                ["staff_desc_dual_cast_damage_fallback"] = "15% of staff/wand damage",

                // === Expert Tree Skill Descriptions ===
                // Attack Expert Tree
                ["attack_root_desc"] = "All attack power +{0}% (physical, elemental)",
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
                ["defense_root_desc"] = "Health +{0}, Helmet Armor +{1}",
                ["defense_survival_desc"] = "Health +{0}, Chest Armor +{1}",
                ["defense_health_desc"] = "Health +{0}, Legs Armor +{1}",
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
                ["defense_attack_desc"] = "Dodge +{0}% if evasion not triggered for 30s",
                ["defense_body_desc"] = "Max health +{0}%, Physical/Elemental resistance +{1}%",
                ["defense_true_desc"] = "Block stamina -{0}%, Normal shield speed +{1}%, Tower shield speed +{2}%",

                // Defense Expert Tree - Effect Texts (English)
                ["defense_root_effect"] = "🛡️ Defense Expert! Health +{0}, Defense +{1}",
                ["defense_shield_effect"] = "🛡️ Shield Training! Shield block power +{0}",
                ["defense_parry_effect"] = "🛡️ Parry Master! Parry +{0}s, Shield block power +{1}",
                ["defense_body_effect"] = "🛡️ Jotunn's Vitality! Health +{0}%, Resistance +{1}%",

                // Staff Expert Tree
                ["staff_expert_desc"] = "Elemental damage +{0}%",
                ["staff_focus_desc"] = "Eitr cost -{0}%",
                ["staff_stream_desc"] = "Max Eitr +{0}",
                ["staff_frost_desc"] = "Frost damage +{0}",
                ["staff_fire_desc"] = "Fire damage +{0}",
                ["staff_lightning_desc"] = "Lightning damage +{0}",
                ["staff_luck_mana_desc"] = "{0}% chance for no Eitr cost",

                // Mace Expert Tree (additional)
                ["mace_expert_desc2"] = "Mace damage +{0}%, {1}% chance to stun for {2}s on hit",
                ["mace_stun_boost_desc2"] = "Stun chance +{0}%, Duration +{1}s",
                ["mace_guard_boost_desc2"] = "Secondary attack damage +{0}%, AoE range {1}m",
                ["mace_heavy_strike_desc2"] = "Heavy attack blunt +{0}",
                ["mace_knockback_desc2"] = "When not blocking, {0}% chance to push attacker on hit",
                ["mace_tank_desc2"] = "Health +{0}%, Damage taken -{1}%",
                ["mace_dps_desc2"] = "Damage +{0}%",
                ["mace_grandmaster_desc2"] = "Attack speed +{0}%",

                // === Prerequisite Text ===
                ["prerequisite_label"] = "🔗 Required",
                ["prerequisite_connector_or"] = " or ",
                ["prerequisite_labor_craft"] = "Labor Expert + Crafting Expert",
                ["or_separator"] = "or",
                ["skill_prerequisite_any_required"] = "Requires at least one: {0}",

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
                ["level_up_message"] = "<color=green>LEVEL UP!</color>\n+{0} Skill Points (Lv.{1})",
                ["mmo_level_sync_message"] = "<color=#00BFFF>Lv.{0} / Skill Point. {1}</color>",

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

                // === Sword Active Skill Messages ===
                ["sword_or_shield_required"] = "You must equip a sword or shield",

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
                ["speed_ex1_effect"] = "⚔️ Trainee 1 Learned! Melee/Crossbow Proficiency +{0}",
                ["speed_ex2_effect"] = "🏹 Trainee 2 Learned! Staff/Bow Proficiency +{0}",
                ["all_master_effect"] = "🏃 Master Learned! Run/Jump Proficiency +{0}",
                ["agility_peak_effect"] = "🦘 Jump Master Learned! Jump Proficiency +{0}",

                // === Archer Tooltip Keys ===
                ["archer_level_req_items"] = "Lv{0} Required Items",
                ["archer_preview_arrows_damage"] = "+{0} projectiles, {1}% damage per arrow",
                ["archer_preview_charges"] = "+{0} additional charges",
                ["archer_job_requirement"] = "Bow equipped, Archer job",
            };
        }
    }
}

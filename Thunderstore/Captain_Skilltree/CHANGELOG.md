# Changelog / 변경 로그
## Update
# [1.2.11] - 2026-03-31
- ✅fix1 : Berserker - Rage skill now correctly scales with level: duration, cooldown, damage-per-HP%, and max damage cap all use per-level values (was using fixed values regardless of skill level)
- ✅fix2 : Tanker - Config Manager: Missing Lv1 passive HP bonus translation added to all 7 languages
- ✅fix3 : Tanker - Lv2 passive tooltip text corrected: "HP +" → "Bonus HP +"

-
- ✅fix1 : 버서커 - 분노(Rage) 실제 효과가 레벨에 맞게 적용되도록 수정: 지속시간·쿨타임·체력당 데미지%·최대 보너스 상한이 모두 레벨별 수치 사용 (기존: 레벨 무관하게 고정값 적용)
- ✅fix2 : 탱커 - 컨피그 매니저: Lv1 패시브 추가 체력 번역 누락 항목 7개 언어 전체 추가
- ✅fix3 : 탱커 - Lv2 패시브 툴팁 텍스트 수정: "생명력 +" → "추가 체력 +"

# [1.2.11] - 2026-03-30
- ✅fix1 : Mace - Shield Charge: Dash distance increased from 8m to 10m
- ✅fix2 : Mace - Shield Charge: Added 3m proximity hit compensation — enemies within 3m during dash are now reliably hit
- ✅improve1 : Berserker - Tooltip now shows per-level HP damage rate (Lv1:1.5% / Lv2:1.6% / Lv3:1.7% / Lv4:1.8% / Lv5:2.0% per 1% HP lost), applied to all 7 languages

-
- ✅fix1 : 둔기 - 방패돌진: 돌진 거리 8m → 10m로 확장
- ✅fix2 : 둔기 - 방패돌진: 3m 이내 보정 적중 추가 — 돌진 경로 옆 적에게도 안정적으로 적중
- ✅improve1 : 버서커 - 툴팁에 레벨별 체력 1%당 공격력 증가 비율 표시 추가 (Lv1:1.5% / Lv2:1.6% / Lv3:1.7% / Lv4:1.8% / Lv5:2.0%), 7개 언어 전체 적용

# [1.2.11] - 2026-03-29
- ✅fix1 : Mace - Shield Charge: Fixed enemies not being hit when dashing through them (Added OverlapSphere overlap detection + hitbox layer to SphereCast)
- ✅fix2 : Mace - Shield Charge: Dash now follows terrain slopes properly; trees and rocks now correctly block the dash
- ✅fix3 : Modification of Tropic and Specific Skle Learning Conditions in relation to job-related lv2 level-up
- ✅improve1 : Producer - Config Manager: Enchant config simplified to Lv1~Lv5 chance only; min/max value entries removed (now managed by Producer_Enchant.json)
- ✅improve2 : Berserker - HP bonus changed to flat value per level (Lv1:+40, Lv2:+60, Lv3:+80, Lv4:+100, Lv5:+120)

-
- ✅fix1 : 둔기 - 방패돌진: 몬스터를 지나쳐도 적중이 안 되던 버그 수정 (OverlapSphere 겹침 감지 추가 + hitbox 레이어 포함)
- ✅fix2 : 둔기 - 방패돌진: 오르막/내리막에서 지형을 따라 정상 이동하도록 수정; 나무/바위가 돌진을 올바르게 차단
- ✅fix3 : 직업관련 lv2 레벨업 관련하여 트로피피 및 특정 스클학습 조건 수정
- ✅improve1 : 제작 전문가 - 컨피그 매니저 정리: 마법부여 Lv1~5 확률 항목만 표시되도록 단순화; min/max 수치 항목 제거 (Producer_Enchant.json으로 관리)
- ✅improve2 : 버서커 - 체력 보너스가 레벨별 고정값으로 변경 (Lv1:+40, Lv2:+60, Lv3:+80, Lv4:+100, Lv5:+120)

# [1.2.08] - 2026-03-28
- ✅fix1 : Performance - Fixed inventory freeze/stutter
- ✅fix2 : Performance - Correction of stopping when opening the box (storage)
- ✅fix3 : Correct Paladin Hill Malfunction and Change Effect
- ✅fix4 : Mage (Y-key) Fire Rain - No longer consumes cooldown or Eitr when no target is in range; Lv2 extra charge is also preserved on miss.
- ✅fix5 : Sword - Counter Stance: Defense bonus after parry was not being applied; fixed missing function calls and damage patch
- ✅fix6 : Spear - Throwing Spear Expert: Damage bonus was applying to all spear attacks; now correctly applies to secondary (throw) attack only
- ✅fix7 : Polearm - Spinning Slash: Damage bonus was intermittently applying; secondary attack detection changed from button-state check to StartAttack parameter for reliability
- ✅fix8 : Polearm - Polearm Boost: Floating text was spamming on every hit; removed (permanent passive needs no per-hit message)
- ✅improve1 : Revise the entire attack specialist
- ✅improve2 : System - Config auto-reset on mod version change; all users receive new default values on update
-
- ✅fix1 : 성능 - 인벤토리 멈춤 현상 수정
- ✅fix2 : 상자(보관함) 열때 멈춤 현상 수정
- ✅fix3 : 성기사 힐 오작동 수정 및 효과 변경
- ✅fix4 : 메이지(Y키) 불의 비 - 사거리 내 타겟 없을 때 쿨타임·Eitr가 소모되던 버그 수정; Lv2 연속 충전도 실패 시 유지됨.
- ✅fix5 : 검 - 반격 자세: 패링 성공 후 방어력 증가가 실제로 적용되지 않던 버그 수정 (함수 호출 누락 2곳 + 데미지 패치 미반영)
- ✅fix6 : 창 - 투창 전문가: 데미지 보너스가 모든 창 공격에 적용되던 버그 수정; 이제 2차 공격(투창)에만 적용
- ✅fix7 : 폴암 - 회전베기: 데미지 보너스가 간헐적으로 적용되던 버그 수정; 2차 공격 감지를 버튼 상태 체크 → StartAttack 파라미터로 변경
- ✅fix8 : 폴암 - 폴암강화: 타격마다 플로팅 텍스트가 출력되던 문제 수정 (영구 패시브는 타격 시 메시지 불필요)
- ✅improve1 : 공격 전문가 전체 새롭게 수정
- ✅improve2 : 시스템 - 모드 버전 변경 시 Config 자동 초기화; 업데이트 시 모든 사용자에게 새 기본값 적용

# [1.2.03] - 2026-03-26
- ✅new1 : Mage (Y-key) - Complete skill redesign: "Fire Rain" replaces old AOE explosion. Targets nearest enemy in camera direction (12m range), spawns 30 fireballs from upper-left sky falling in 8m radius, each with 3m AOE impact. Damage: weapon dmg × 22/24/26/28/30% per level. Cooldown 45s (all levels). Player rotates to camera direction and plays cheer emote on cast; debuff_03 VFX spawns at feet.
-
- ✅new1 : 메이지 (Y키) - 스킬 전면 교체: "불의 비(Fire Rain)"로 개편. 카메라 방향 12m 내 최근접 적 타겟팅, 좌측 상공에서 파이어볼 30개 낙하(반경 8m), 각 파이어볼 3m AOE 적중. 무기 공격력 × 레벨별 22/24/26/28/30%. 쿨타임 45초(전 레벨). 시전 시 캐릭터 카메라 방향 전환 + 환호 모션, 발 밑에 debuff_03 이팩트 재생.

# [1.2.02] - 2026-03-25
- ✅improve1 : Rogue - Passive changed: Elemental Resistance replaced with Evasion (Lv1:4% / Lv2:6% / Lv3:8% / Lv4:10% / Lv5:12%)
- ✅improve2 : Tanker - Reworked passive system: HP bonus changed from flat +100 (all levels) to level-based (Lv1: none, Lv2: +35, Lv3: +55, Lv4: +75, Lv5: +100). AllResist (%) removed and replaced with Shield Block Power per level (Lv2: +5, Lv3: +10, Lv4: +15, Lv5: +20)
- ✅improve3 : Attack Expert - Combo Finisher: Always-on +5% one-handed weapon damage; improved one-hand detection (excludes two-handed swords, axes, and mauls)
-
- ✅improve1 : 로그 - 패시브 변경: 속성 저항 → 회피율로 교체 (Lv1:4% / Lv2:6% / Lv3:8% / Lv4:10% / Lv5:12%),
- ✅improve2 : 탱커 - 패시브 시스템 개편: 체력 보너스가 고정 +100(전 레벨)에서 레벨별로 변경(Lv1: 없음, Lv2: +35, Lv3: +55, Lv4: +75, Lv5: +100). 모든 저항(%) 제거, 방패 막기 방어력으로 교체(Lv2: +5, Lv3: +10, Lv4: +15, Lv5: +20)
- ✅improve3 : 공격 전문가 - 연속 근접의 대가: 한손 무기 상시 공격력 +5%로 변경; 한손 무기 감지 개선 (양손검·양손 도끼·양손 둔기 제외)

# [1.1.86] - 2026-03-24
- ✅improve1 : Performance - Replaced fixed 1s polling in ActiveSkillHUD with adaptive 2-stage polling: completely stops when no cooldown is active (0 calls/frame), switches to 60s interval when remaining > 60s, and 1s interval when ≤ 60s — reduces UpdateSlot calls by ~87% (e.g. 540 → 68 for a 9-minute cooldown)
- ✅improve2 : Performance - Added ActiveSkillCooldownRegistry.GetMinRemaining() for interval recalculation; SetCooldown() / RecalculateCooldown() now notify HUD via OnCooldownStarted() / OnCooldownChanged() for immediate response without wasted polling
-
- ✅improve1 : 성능 - ActiveSkillHUD 쿨타임 폴링을 고정 1초에서 2단계 적응형으로 교체: 쿨타임 없을 시 완전 중지(호출 0), 잔여 >60s 이면 60초 간격, ≤60s 이면 1초 간격으로 전환 — 9분 쿨타임 기준 UpdateSlot 호출 540회 → 68회 (약 87% 감소)
- ✅improve2 : 성능 - ActiveSkillCooldownRegistry.GetMinRemaining() 추가로 폴링 간격 동적 재계산; SetCooldown() / RecalculateCooldown() 에서 HUD의 OnCooldownStarted() / OnCooldownChanged() 를 직접 호출하여 불필요한 폴링 없이 즉각 반응

# [1.1.85] - 2026-03-24
- ✅fix1 : Performance - Fixed inventory open lag caused by Type.GetType("EpicMMOSystem.MyUI") being called twice on every inventory open (now cached, called once only)
- ✅fix2 : Performance - Fixed item click lag (manual drag/drop) caused by 15+ Harmony patches (GetDamage/GetTooltip/GetBlockPower etc.) all running in one frame when recipe panel updates; added per-frame result cache to SkillBonusCalculator
- ✅fix3 : All Jobs - Fixed trophy name mismatch causing upgrade failure at Lv2+: "$item_trophy_elder" (non-existent) corrected to "$item_trophy_theelder" across all job functions (Berserker/Tanker/Mage/Paladin/Archer/Producer — HaveItem/GetMissing/Consume)
- ✅fix4 : Performance - Removed 5 LogDebug string allocations from hot paths (GetStaminaReduction every frame while running / getParameter Postfix per stat query / MonsterSpawn per dungeon spawn) eliminating GC pressure during combat and movement
- ✅fix5 : Performance - Reduced ActiveSkillHUD polling from 50ms to 1000ms (560→28 GetSkillLevel calls/sec); added RefreshSlots() for immediate update on skill invest
- ✅fix6 : Performance - Cached Camera.main in SkillBuffDisplay._camera field to avoid FindObjectOfType call every 50ms
- ✅fix7 : Performance - Replaced LINQ .Where().Sum() with foreach in SkillBonusCalculator, eliminating 2 enumerator allocations per damage calculation
- ✅fix8 : Performance - Replaced per-frame new List<string>(activeBuffs.Keys) on player death with a reusable _tempBuffKeys list
-
- ✅fix1 : 성능 - 인벤토리 열기 시 순간 멈춤 수정 — Type.GetType("EpicMMOSystem.MyUI")이 인벤 열 때마다 2회 호출되던 문제를 캐시로 해결 (최초 1회만 조회)
- ✅fix2 : 성능 - 아이템 수동 클릭 이동 시 랙 수정 — 제작 패널 업데이트 시 GetDamage/GetTooltip 등 15+ Harmony 패치가 한 프레임에 연속 실행되던 문제를 SkillBonusCalculator 프레임 단위 캐시로 해결 (Ctrl+클릭은 정상, 수동 클릭만 랙 발생하던 현상)
- ✅fix3 : 전 직업 - Lv2 이상 업그레이드 시 엘더 트로피를 인벤에 보유하고 있어도 "부족함" 메시지 뜨는 버그 수정 — "$item_trophy_elder"(존재하지 않는 ID) → "$item_trophy_theelder"로 전체 직업(버서커/탱커/메이지/성기사/아처/생산자) Has/GetMissing/Consume 함수 전부 교정
- ✅fix4 : 성능 - 달리기/전투/던전 스폰 중 매 프레임 string GC를 유발하던 핫패스 LogDebug 5개 제거 (GetStaminaReduction·getParameter Postfix·몬스터 스폰)
- ✅fix5 : 성능 - ActiveSkillHUD 스킬 폴링 간격 50ms→1000ms (초당 560→28회 호출), 스킬 투자 시 RefreshSlots() 즉시 갱신 연동
- ✅fix6 : 성능 - SkillBuffDisplay에서 매 50ms Camera.main(내부 FindObjectOfType) 호출을 _camera 캐시 필드로 교체
- ✅fix7 : 성능 - SkillBonusCalculator IEnumerable 오버로드의 LINQ .Where().Sum() → foreach 교체, 데미지 호출마다 열거자 2개 할당 제거
- ✅fix8 : 성능 - 플레이어 사망 시 매 프레임 new List<string>(activeBuffs.Keys) 생성을 재사용 리스트(_tempBuffKeys)로 교체

# [1.1.80] - 2026-03-23
- ✅fix1 : Tanker - Fixed 11 missing multilingual KeyNames (ReflectPercent, Hp_Bonus, Lv2~5_AllResist, ReflectDuration_Lv1~5) not displaying translated names in F1 Config Manager
- ✅improve1 : Tanker - Passive HP bonus now scales by level (Lv1:+35 / Lv2:+50 / Lv3:+65 / Lv4:+80 / Lv5:+100), configurable per level
-
- ✅fix1 : 탱커 - F1 Config Manager에서 번역 없이 표시되던 KeyNames 11개 누락 수정 (ReflectPercent, Hp_Bonus, Lv2~5_AllResist, ReflectDuration_Lv1~5)
- ✅improve1 : 탱커 - 패시브 체력 보너스 레벨별 차등 적용 (Lv1:+35 / Lv2:+50 / Lv3:+65 / Lv4:+80 / Lv5:+100), Config에서 조정 가능

# [1.1.78] - 2026-03-22
- ✅new1 : Arrow Rain: Added AOE damage (3m radius) when falling arrows land on monsters/terrain
- ✅new2 : Arrow Rain: Added structured tooltip matching Explosive Arrow format (desc/damage/range/cooldown/skill type)
- ✅new3 : Tanker - Reflect duration now scales by level (Lv1:10s / Lv2:12s / Lv3:14s / Lv4:16s / Lv5:20s), configurable per level
- ✅improve1 : Arrow Rain: Rebalanced default values — arrows: 100, damage: 8%, fall radius: 8m
- ✅improve2 : Tanker - Passive tooltip reformatted: removed "-" sign, added per-level reflect duration (e.g. "HP +100, Dmg taken 15%, Reflect for 10s on hit")
- ✅improve3 : Mace Fury Hammer - Redesigned as leaping charge (10m forward, 3.5m peak height, 0.5s)
- ✅improve4 : Mace Fury Hammer - Attack motion triggers mid-air (0.25s); landing applies 1st strike; stagger immunity + fall damage prevention during skill
- ✅fix1 : Tanker - Passive 15% damage reduction was never applied (now correctly activates at Lv1+)
-
- ✅new1 : 화살비: 낙하 화살 착지 시 반경 3m AOE 데미지 추가
- ✅new2 : 화살비: 폭발화살과 동일한 형식의 동적 툴팁 적용 (설명/데미지/범위/쿨타임/스킬유형 항목 분리)
- ✅new3 : 탱커 - 반사 지속시간 레벨별 차등 적용 (Lv1:10초 / Lv2:12초 / Lv3:14초 / Lv4:16초 / Lv5:20초), Config에서 조정 가능
- ✅improve1 : 화살비: 기본값 조정 — 화살수 100발, 화살당 데미지 8%, 낙하 반경 8m
- ✅improve2 : 탱커 - 패시브 툴팁 형식 개선: "-" 기호 제거, 레벨별 반사 시간 표시 (예: "생명력 +100, 피해감소 15%, 피격시 10초간 반사")
- ✅improve3 : 둔기 분노의 망치 - 도약 돌격으로 재설계 (10m 앞으로, 최고 높이 3.5m, 0.5초)
- ✅improve4 : 둔기 분노의 망치 - 0.25초에 공중 공격 모션 시작; 착지 시 1타 발동; 스킬 중 경직 면역 + 낙하 데미지 방지
- ✅fix1 : 탱커 - 패시브 피해 감소 15%가 실제로 적용되지 않던 버그 수정 (Lv1 이상 시 정상 작동)

# [1.1.46] - 2026-03-22
- ✅new1 : All jobs - Added coin cost requirement for level-up (Lv1:1000 / Lv2:2000 / Lv3:3000 / Lv4:4000 / Lv5:5000)
- ✅new2 : Config - Added Job_Lv1~5_Cost to Skill_Tree_Base section (server admin only, auto-synced to all clients)

- ✅fix1 : Paladin job - Fixed one-handed spear active skill not triggering
- ✅fix2 : Improvement of multilingual support for production experts (tree +1 related to collection)
- ✅fix3 : Production Professionals - Improving production blessing durability and effectiveness
- ✅fix4 : Improvement of Lv1 to 5 problems by occupation
- ✅fix5 : Paladin - Fixed missing level-up condition check in AddPendingInvestment (could bypass coin/trophy requirement and level up directly)
- ✅fix6 : Tanker - Fixed Lv0→Lv1 condition check bypass and missing warning messages for Lv2~5
- ✅fix7 : All jobs - Unified condition warning message font size to 20 (MessageHud floating text + Skill Tree UI warning text)
- ✅improve1 : Mace (Two-Handed) Fury Hammer - Now dashes 7m forward on activation
- ✅improve2 : Mace (Two-Handed) Fury Hammer - Gains stagger immunity during the skill (attacks continue even when hit by monsters)
-
- ✅new1 : 전 직업 - 레벨업 시 코인 소모 조건 추가 (Lv1:1000 / Lv2:2000 / Lv3:3000 / Lv4:4000 / Lv5:5000)
- ✅new2 : 컨피그 - Skill_Tree_Base 섹션에 Job_Lv1~5_Cost 추가 (서버 관리자 전용, 전 클라이언트 자동 동기화)

- ✅fix1 : 팔라딘 직업 - 한손창 액티브 스킬 미발동 버그 수정
- ✅fix2 : 생산전문가의 채집 관련 나무+1 등 다국어 지원 개선
- ✅fix3 : 제작전문가(Producer) - 제작의 축복 내구도 및 효과 표시 개선
- ✅fix4 : 직업별 Lv1~5 문제 개선
- ✅fix5 : 성기사 - AddPendingInvestment 조건 블록 누락 수정 (코인/트로피 조건 없이 바로 레벨업 되던 버그)
- ✅fix6 : 탱커 - Lv0→Lv1 조건 체크 우회 및 Lv2~5 조건 불충족 시 메시지 미표시 수정
- ✅fix7 : 전 직업 - 조건 불충족 경고 메시지 글씨 크기 20으로 통일 (MessageHud 플로팅 텍스트 + 스킬트리 UI 경고 텍스트)
- ✅improve1 : 둔기(양손) 분노의 망치 - 스킬 시전 시 바라보는 방향으로 7m 돌진 추가
- ✅improve2 : 둔기(양손) 분노의 망치 - 스킬 진행 중 경직 면역 부여 (몬스터에게 맞아도 5연타 계속 진행)


# [1.1.14] - 2025-03-21
- ✅fix1 : New Job - Production Expert (Producer)
- ✅fix2 : Reorganize to the Rogue level-up method
- ✅fix3 : Reorganization to Mage level up method
- ✅fix4 : Reorganization to Berserker level up method
- ✅fix5 : reorganization of Paladin level-up method
- ✅fix6 : Reorganization to Tanker level up method
- ✅fix7 : Multilingual - Japanese language support
- ✅fix8 : Production expert (Producer) - Production blessing (item reinforcement) modification
- ✅fix9 : Tanker Lv issues and improved rushing paring
- ✅fix10 : Multilingual - Additional translation
-
- ✅fix1 : 신규 직업 - 제작 전문가 (Producer)
- ✅fix2 : Rogue 레벨업 방식으로 개편
- ✅fix3 : Mage 레벨업 방식으로 개편
- ✅fix4 : Berserker 레벨업 방식으로 개편
- ✅fix5 : Paladin 레벨업 방식으로 개편
- ✅fix6 : Tanker 레벨업 방식으로 개편
- ✅fix7 : 다국어 - 일본어 지원
- ✅fix8 : 제작 전문가 (Producer) - 제작축복(아이템강화) 수정
- ✅fix9 : Tanker Lv 문제 및 돌진패링 개선
- ✅fix10 : 다국어 - 추가 번역

# [1.0.227] - 2025-03-17
- ✅fix1 : Multilingual - Chinese support, Russian untranslated addition
- ✅fix2 : Fury Hammer Passive Invincible Passive Cooltime Display Modified
- ✅fix3 : EPICMMO's HUD Application - Experience, Physical Fitness, Stemina, Aitrba
- ✅fix4 : Correct the invincible passivation of the busker
-
- ✅fix1 : 다국어 - 중국어 지원, 러시아어 미번역 추가
- ✅fix2 : 둔기 액티브 스킬 - 분노의 망치 패시브 무적 패시브 쿨타임 표시 수정
- ✅fix3 : EPICMMO의 HUD 적용- 경험치, 체력, 스테미나, 에이트르 바
- ✅fix4 : 버서커 무적패시브 수정


# [1.0.1] - 2025-03-16
- ✅fix1 : Archer Lv2~Lv5 Trophy modification
- ✅fix2 : Modify the Initialization button
- ✅fix3 : Multilingualism - German language support
- ✅fix4 : Apply marking to pole arm expert effect and item
- ✅fix5 : Apply Spear expert effects and marks to items
- ✅fix6 : Application of display to wand expert effect and item
- ✅fix7 : Correction related to avoidance
- ✅fix8 : Application of rotating cutlery by blunt instrument experts
- ✅fix9 : Modifying Spear Expert Skills
- ✅fix10 : Adjustment of skill level
- ✅fix11 : Revise skill points of blunt instrument experts
-
- ✅fix1 : 아처 Lv2~Lv5 트로피 수정
- ✅fix2 : 초기화 버튼 수정
- ✅fix3 : 다국어 관련 - 독일어 지원
- ✅fix4 : 폴암 전문가 효과 및 아이템에 표시 적용
- ✅fix5 : 창 전문가 효과 및 아이템에 표시 적용
- ✅fix6 : 지팡이 전문가 효과 및 아이템에 표시 적용
- ✅fix7 : 회피 관련 수정
- ✅fix8 : 둔기 전문가의 회전베기 적용
- ✅fix9 : 창 전문가 스킬의 수정
- ✅fix10 : 스킬들의 벨런스 조절
- ✅fix11 : 둔기 전문가의 스킬포인트 재 수정

# [0.1.943] - 2025-03-14 
- ✅fix1 : Change the icon of the paring charge skill
- ✅fix2 : Change the rush cuttings VFX
- ✅fix3 : Improve the display of attack and defense effects
- ✅fix4 : Archer Job Growth - Level Up System
- ✅fix5 : Portuguese - Brazil -> Config and detailed translation patches
- ✅fix6 : Improving the skill tree UI button
- ✅fix7 : Hammer of anger, change skill icon of senior Spear
- ✅fix8 : Adjust to Berserker invincible passive 10 minutes, display invincible cool time icon, and increase attack power only 30% from a distance
- ✅fix9 : peer active skill, blunt active skill attack downgrade patch
-
- ✅fix1 : 패링 돌격 스킬의 아이콘 변경
- ✅fix2 : 돌진베기 VFX  변경
- ✅fix3 : 공격 및 방어효과 표시개선(다국어 -영어,러시아,포르투갈-브라질)
- ✅fix4 : 아처 직업 성장- Level Up 시스템 
- ✅fix5 : 포루투갈어 - 브라질 -> 컨피그 및 세부 번역 패치
- ✅fix6 : 스킬트리 UI 버튼 개선 
- ✅fix7 : 분노의 망치, 연공창 스킬 아이콘 변경
- ✅fix8 : 버서커의 무적 패시브 10분으로 조정, 무적 쿨타임 아이콘표시, 버서커 공격력 증가는 원거리 30%만 적용
- ✅fix9 : 연공창, 분노의 망치 스킬 공격력 하향 패치 


## [0.1.914] - 2025-03-12
- ✅fix1 : Defensive Expert Defensive Downward Patch
- ✅fix2 : Crossbow's continuous projectile attack downgrade patch
- ✅fix3 : Improvement of the display of attack and defense effects - under development
- ✅fix4 : Archer Job Growth - Level Up System - Under Development
- ✅fix5 : Portuguese - Brazil
- ✅fix6 : Increasing the difficulty of MMO-related monsters
-
- ✅fix1 : 방어 전문가 방어력 하향 패치
- ✅fix2 : 석궁의 연속발사체 공격력 하향 패치
- ✅fix3 : 공격 및 방어효과 표시개선 - 개발중
- ✅fix4 : 아처 직업 성장- Level Up 시스템 - 개발중
- ✅fix5 : 포루투갈어 - 브라질
- ✅fix6 : MMO관련 몬스터 난이도 상향조정


## [0.1.904] - 2025-03-11
- ✅fix1: Adjust the reset button position
- ✅fix2: Improving the display of attack and defense effects - under development
- ✅fix3 : The heart of the assassin - out of the dungeon used in dungeon
- ✅fix4 : Fast movement skill of dagger expert - Accumulation problem of movement speed
- ✅Fix5: Sword experts, attack experts, close-range weapons experts, etc. continue to occur without time limits in the event of 2 consecutive and 3 consecutive attacks
- ✅fix6 : Problems displaying effects on food and other weapons and non-defensive items
- ✅fix7 : Changes and additions of effects and sound effects - Hill, Bercircar invincible, guardian's sincerity
- ✅fix8 : Archer Job Growth - Level Up System - Under Development
-
- ✅fix1 : 초기화 버튼 위치 조정
- ✅fix2 : 공격 및 방어효과 표시개선 - 개발중
- ✅fix3 : 암살자의 심장 - 던전안에서 사용시 던전 밖으로 나와짐
- ✅fix4 : 단검 전문가의 빠른 이동 스킬  - 이동속도 누적 문제
- ✅fix5 : 검 전문가, 공격 전문가, 근접 무기 전문가 등에서 2연속, 3연속 공격 시 효과 시간 제한 없이 계속 발생
- ✅fix6 : 음식 및 기타 무기, 방어구 아닌 아이템에 효과 표시 문제
- ✅fix7 : 효과 및 효과음 변경 및 추가 - 힐, 버서커 무적, 수호자의 진심
- ✅fix8 : 아처 직업 성장- Level Up 시스템 - 개발중


## [0.1.885] - 2025-03-10
- ✅fix1 : Recall senior speaker If the inventory is full, summon the speaker in front of the character
- ✅fix2 : Display improvement of attack and defense effects - in progress
- ✅fix3 : Add and change visual effects and sound effects
- ✅fix4 : Job initialization separate button
-
- ✅fix1 : 연공창 창 회수시 인벤 가득 찬 경우 캐릭터 앞에 창 소환
- ✅fix2 : 공격 및 방어효과 표시개선 - 진행중
- ✅fix3 : 시각 효과 및 효과음 추가와 변경
- ✅fix4 : 직업 초기화 별도 버튼


## [0.1.874] - 2025-03-09
- ✅fix1: Correction of the criticality of bow experts and attack trees
- ✅fix2: Critical Correction of Attack Expert Tree
- ✅fix3: Standardize the effectiveness of the tool and defense expert tree
- ✅fix4 : Message display controls (passive and combat, production skills, critical and large messages)
- ✅fix5 : Adding and changing visual effects and sound effects
- ✅fix6 : Add tree-specific effects to the attack weapon and mouse over the item
- ✅fix7: Defense expert's downgrade of shield defense
- ✅fix8: Improving the effect of the tanker's provocation and adding sound effects
- ✅fix9: Add Russian translation and provide en.json and ru.json to multilingual standard BepInEx\config\CaptainSkillTree\Translation
- 
- ✅fix1 : 활 전문가 및 공격 트리의 크리티컬 관련 수정
- ✅fix2 : 공격 전문가 트리의 크리티컬 관련 수정
- ✅fix3 : 방구 및 방어 전문가 트리의 효과 표시 표준화
- ✅fix4 : 메시지 표시 컨트롤 (패시브 및 전투, 생산스킬, 크리티컬 및 큰 메시지)
- ✅fix5 : 시각 효과 및 효과음 추가와 변경
- ✅fix6 : 공격 무기에 트리별 효과를 아이템에 마우스 오버시 표시 추가
- ✅fix7 : 방어 전문가의 방패 방어력 하향 조치
- ✅fix8 : 탱커의 도발 효과 개선 및 효과음 추가 
- ✅fix9 : 러시아어 번역 추가 및 다국어 표준  BepInEx\config\CaptainSkillTree\Translation 위치에 en.json 과 ru.json 제공



## [0.1.824] - 2025-03-06
- ✅fix1 : Change key of R skill to Z key
- ✅fix2 : Correcting the stopping phenomenon of skill icons, etc
- ✅fix3 : Correcting the problem of triggering skill effects in case of monster attack
- ✅fix4 : Wand expert's active skill double trial -> change to Rapid Barrage
- ✅fix5 : Bow Multi-shot Attack Downpatch
- ✅fix6 : Chang Expert Tree Explosion Spear -> Change to Quick Spear
- ✅fix7 : Heel skill effect and VFX 
-
- ✅fix1 : R 키 스킬를 Z키로 변경
- ✅fix2 : 스킬아이콘의 멈춤현상 등 수정
- ✅fix3 : 몬스터 공격시 스킬효과 발동문제 수정
- ✅fix4 : 지팡이 전문가의 액티브 스킬 이중 시전 -> 연속 발사 로 변경
- ✅fix5 : 활 멀티샷 공격력 하향패치
- ✅fix6 : 창 전문가 트리의 폭발창 -> 빠른창 으로 변경 
- ✅fix7 : 지팡이 전문가의 힐 스킬 효과 및 VFX 변경
- ✅fix8 : 근접 전문가 트리의 각 무기별 공격력 표시 수정 





## [0.1.783] - 2025-03-04
- ✅fix1 : Config Russian Language Translation
- ✅fix2 : In the configuration regarding skill key binding  Enables R,Y,G,H keys to be changed
- ✅fix3 : Place in the bottom left of the skill icon
- ✅fix4 : Change VFX on top of monster in case of tanker skill provocation
- ✅fix5 : Change the movement path of the rushing slash skill to all monsters
-
- ✅fix1 : Config 러시아 언어 번역
- ✅fix2 : 스킬 키바인딩 관련 하여 컨피그에서 R,Y,G,H 키 를 변경 가능하게 함
- ✅fix3 : 스킬 아이콘 좌측 하단에 배치 
- ✅fix4 : 탱커 스킬 도발시 몬스터 머리위에 VFX 변경
- ✅fix5 : 돌진베기 스킬의 이동경로 몬스터 모두 타격으로 변경



## [0.1.748] - 2025-03-01
- ✅fix1 : Additional tree acquisition by production experts
- ✅fix2 : Koreanization of the configuration
- ✅fix3 : Change to be parable separately from avoidance and prevention
- ✅fix4 : Standardize the display method on skill effect items
- ✅fix5 : attack expert, dagger expert bellance down patch
- ✅fix6 : Rusher - applies only to skill tree (config, message not applied)
- ✅fix7 : Display skill tree effects on weapon items
-
- ✅fix1 : 생산 전문가의 나무 추가 획득
- ✅fix2 : 컨피그 한글화
- ✅fix3 : 회피와 막기 분리하여 패링 가능하게 변경
- ✅fix4 : 스킬의 효과 아이템에 표시 방식 표준화
- ✅fix5 : 공격 전문가, 단검 전문가 벨런스 하향 패치 
- ✅fix6 : 러시어 - 스킬트리에만 적용 (컨피그 , 메시지 미적용)
- ✅fix7 : 무기 아이템에 스킬트리 효과 반영 표시

## [0.1.726] - 2025-02-28
- ✅fix1 : Defense experts apply full defense
- ✅fix2 : Multi-shot Problem
- ✅fix3 : Bow skill and attack problem
- ✅fix4 : a harvest-related problem
-
- ✅fix1 : 방어 전문가 전체 방어력 적용
- ✅fix2 : 멀티샷 문제
- ✅fix3 : 활 숙련자 및 비정상적 공격력
- ✅fix4 : 채집 관련 문제

## [0.1.696] - 2025-02-26
- Urgent update due to missing last patch file
- 지난 패치 파일 누락으로 긴급 업데이트

## [0.1.689] - 2025-02-25
- ✅fix1 : Issue with automatic firing of bow multi-shot and crossbow additional projectiles.
- ✅fix2 : Fatal strike error

###🌍Skill modification
**a hammer of anger
 - Add attack motion

**Production expert
- Woodcutting Lv3 Requirements Change to Fine Wood
- Woodcutting Lv4 Requirements Change to Ancient bark

**Defense Expert : Helmet Armor +2   (Change the numerical value applied when mouse over to item by part)
- Skin Hardening : chest Armor +5
- Health Training : Legs Armor +5
- Shield Traing

**Rush Slash
 - Return to the original position after using the skil

###🌍Job Skill
**BerserKer Skill
- Maximum stamina +100 additional effects


- ✅fix1 : 활 멀티샷 및 석궁 추가 발사체의 자동 발사 문제.
- ✅fix2 : 치명타 오류

###🌍스킬 수정
**분노의 망치
 - 공격 모션 추가

**생산 전문가
 - 벌목 Lv3 필요조건 질좋은 나무로 변경
 - 벌목 Lv4 필요조건 고대의 나무껍질로 변경

**방어 전문가: 헬멧 갑옷 +2 (부위별 아이템에서 적용되는 숫자 값을 확인 가능)
- 피부 경화: 흉갑 +5
- 건강 훈련: 각반 +5
- 방패 훈련: 가드 방어력 +100

**돌진 연속베기
 - 스킬 사용 후 원래 자리로 복귀

###🌍버서커(광전사)
- 최대 체력 +100 추가효과

###🌍Speed Limiting System
**We introduce a system that improves the method of calculating the speed of movement and attack and limits the maximum speed.
**(Valheim base speed + other mode speed) + Skilltree speed = up to 70% limit
**This can be modified from Skill_Tree_Base in config.

###🌍속도 제한 시스템
**이동 및 공격 속도 계산방식을 개선하고 최대 속도를 제한하는 시스템을 도입했다.
**(발헤임 기본 속도+다른 모드 속도)+스킬트리 속도 = 최대 70% 제한
**이는 config 의 Skill_Tree_Base 에서 수정가능하다.

###🌍Language
**I updated the Korean and English translations related to the configuration items and skills in detail

###🌍다국어 관련
**컨피그 설정 항목과 스킬 관련 한국어와 영어 번역을 세부적으로 업데이트 하였다


## [0.1.605)] - 2025-02-22

- ✅fix1 : Flow of Combo / Crossbow Mastery / One Shot - Stutter occurred
- ✅fix2 : BGM volume increase problem.
- ✅fix3 : Stutter occurred at VFX instantiation timing.
- ✅fix4 : Knife Expert Tree 
- ✅fix5 : Bow Expert Tree
- ✅fix6 : config - 100% multilingual (English) translation

### 🌍Change Rogue Job Passive :
*Hide skill, hide speed +20% removal
*Change the attack speed to +10% and to -15% effectiveness in the event of an attack


## [0.1.527)] - 2025-02-18

### 🌍 Multilingual Support / 다국어 지원 추가

**[EN] Major Update: Full Korean & English Localization

**With automatic language interworking according to game settings
**It is supported in Korean or English.

게임 설정에 따른 언어 자동 연동으로 한국어 또는 영어로 지원됩니다.

---
## [0.1.508] - 2025-02-17

### 🌍 Multilingual Support / 다국어 지원 추가

**[EN] Major Update: Full Korean & English Localization**

This release introduces complete bilingual support for all in-game text. 

Players can now enjoy the mod in their preferred language with seamless translations.

#### Added / 추가됨
- ✅ **Full Korean localization** for all skill names, descriptions, and UI text
- ✅ **Full English localization** for all skill names, descriptions, and UI text
- ✅ **Language detection system** - automatically detects game language
- ✅ **Fallback mechanism** - defaults to English if language not supported
- ✅ **Localized tooltips** - all skill tooltips display in selected language
- ✅ **Localized active skill messages** - combat notifications in user's language
- ✅ **Localized proficiency bonuses** - stat increase messages translated
- ✅ **Comprehensive translation coverage**:
  - All expert tree skills (Attack, Speed, Defense)
  - All weapon tree skills (Bow, Crossbow, Staff, Sword, Knife, Mace, Spear, Polearm)
  - All job class skills (Archer, Mage, Tanker, Rogue, Paladin, Berserker)
  - All active skill abilities (R, G, H, Y hotkeys)
  - All UI elements (tooltips, buttons, panels)
  - All system messages (level up, skill unlock, errors)

#### Technical Details / 기술 상세
- Implemented `LocalizationManager` for centralized translation management
- Created `SkillTranslationHelper` for skill-specific localization
- Added `LocalizationHelper` for UI text translations
- Optimized translation loading for performance
- Added fallback system for missing translations

---

**[KO] 주요 업데이트: 완전한 한국어 & 영어 현지화**

이번 릴리스에서 모든 게임 내 텍스트에 대한 완전한 이중 언어 지원이 도입되었습니다. 이제 플레이어는 원활한 번역으로 선호하는 언어로 모드를 즐길 수 있습니다.

#### 추가됨 / Added
- ✅ **완전한 한국어 현지화** - 모든 스킬 이름, 설명, UI 텍스트
- ✅ **완전한 영어 현지화** - 모든 스킬 이름, 설명, UI 텍스트
- ✅ **언어 감지 시스템** - 게임 언어 자동 감지
- ✅ **대체 메커니즘** - 지원하지 않는 언어일 경우 영어로 기본 설정
- ✅ **현지화된 툴팁** - 선택한 언어로 모든 스킬 툴팁 표시
- ✅ **현지화된 액티브 스킬 메시지** - 사용자 언어로 전투 알림
- ✅ **현지화된 숙련도 보너스** - 스탯 증가 메시지 번역
- ✅ **포괄적인 번역 범위**:
  - 모든 전문가 트리 스킬 (공격, 속도, 방어)
  - 모든 무기 트리 스킬 (활, 석궁, 지팡이, 검, 단검, 둔기, 창, 폴암)
  - 모든 직업 클래스 스킬 (궁수, 마법사, 탱커, 도적, 성기사, 광전사)
  - 모든 액티브 스킬 능력 (R, G, H, Y 단축키)
  - 모든 UI 요소 (툴팁, 버튼, 패널)
  - 모든 시스템 메시지 (레벨업, 스킬 해금, 오류)

#### 기술 상세 / Technical Details
- 중앙 집중식 번역 관리를 위한 `LocalizationManager` 구현
- 스킬별 현지화를 위한 `SkillTranslationHelper` 생성
- UI 텍스트 번역을 위한 `LocalizationHelper` 추가
- 성능을 위한 번역 로딩 최적화
- 누락된 번역을 위한 대체 시스템 추가

---

### 🎯 Translation Quality / 번역 품질

**[EN]** All translations have been carefully crafted to:
- Maintain game immersion and fantasy theme
- Use consistent terminology across all skills
- Preserve original skill mechanics and meanings
- Provide clear, concise descriptions

**[KO]** 모든 번역은 다음을 위해 신중하게 제작되었습니다:
- 게임 몰입감과 판타지 테마 유지
- 모든 스킬에서 일관된 용어 사용
- 원래 스킬 메커니즘과 의미 보존
- 명확하고 간결한 설명 제공

---

### 📊 Coverage Statistics / 범위 통계

- **Total Skills Translated / 총 번역된 스킬**: 200+
- **Languages Supported / 지원 언어**: 2 (English, Korean)
- **UI Elements Localized / 현지화된 UI 요소**: 100%
- **System Messages Translated / 번역된 시스템 메시지**: 100%


**

### 🔄 Migration Notes / 마이그레이션 참고사항

**[EN]** Existing save files are fully compatible. No action required from users.

**[KO]** 기존 세이브 파일과 완전히 호환됩니다. 사용자 조치가 필요하지 않습니다.


### 🙏 Acknowledgments / 감사의 말

**[EN]** Thank you especially for your feedback and suggestion to Balheim Server Goose in Korea.

**[KO]** 피드백과 제안을 주신 한국 발헤임 서버구스에 특별히 감사드립니다.
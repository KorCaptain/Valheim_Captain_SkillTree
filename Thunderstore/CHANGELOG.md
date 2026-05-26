# [1.24.59] - 2026-05-25
- ✅improve1 : Paladin tooltip — heal range moved below cost line; per-level cooldown (29s–26s) added to each Lv2–5 preview entry
- ✅fix1 : Axe — G-key (Rush Slash) and H-key (Whirlwind Slash) now activate when axe is equipped (dispatcher was using sword-only weapon check)
- ✅fix2 : Whirlwind Slash — now deals damage with axe (GetCurrentWeapon replaces GetEquippedSword; m_skill uses weapon's actual skill type)
- ✅fix3 : Whirlwind Slash — 2nd/3rd hit animations now trigger correctly with axe (primary attack replaces 2s secondary attack that blocked 0.9s intervals)
- ✅fix4 : Axe — inventory tooltip now shows Sword/Axe Expert tree bonuses (Expert damage, Fast Slash/True Duel speed, Riposte flat slash, Attack&Defense for 2H axe)
- ✅improve2 : Shield Charge tooltip — description simplified; damage line updated to show block power % explicitly with per-level scaling (all 7 languages)
- ✅fix5 : Whirlwind Slash — sword and axe now both use Atgeir secondary spin animation via ZSyncAnimation.SetTrigger (bypasses StartAttack blocking; fixes sword 3rd hit missing and axe spin animation)
- ✅improve3 : Fury Hammer — Lv1 tooltip description replaced with skill action text ("Leap forward and deliver consecutive downward strikes") across all 7 languages
- ✅fix6 : Axe — Artisan (Producer) WeaponSpd enchant tooltip now displays correctly (ProducerEnchantSpd added to HasAny check)
- ✅fix7 : Axe — melee_speed1 attack speed bonus now applies to axes (Axes added to isMelee condition)
- ✅fix8 : Rogue — Config fallback values corrected to match BindServerSync defaults (AttackBonus 35→30%, StaminaCost 20→25, PoisonInstantDamage 20→40%)
- ✅fix9 : Rogue — fallback tooltip corrected (Lv1 attack bonus 35→30%, blast count 8→6)
- ✅fix10 : Rogue — rogue_poison_info localization completed for DE/JA/ZH-CN/PT_BR (was showing English fallback)
- ✅fix11 : Mage — Lv3/4/5 passive tooltip now correctly shows double-cast text (was falling to lv1 default, omitting "Double Cast +1")
- ✅fix12 : Tanker — Lv3/4/5 passive tooltip now uses lv3 key (was showing lv2 text with incorrect "pre-skill +1 use within 30s" line)
- ✅improve4 : Staff Dual Cast — per-level damage now Config-driven (base 60% + 5%/lv); Lv1~7: 60/65/70/75/80/85/90%; tooltip reflects live Config values across all 7 languages
-
- ✅improve1 : 성기사 툴팁 — 치유 범위를 소모 아래로 이동; Lv2~5 미리보기 각 줄에 레벨별 쿨타임(29초~26초) 추가
- ✅fix1 : 도끼 — G키(돌진 연속 베기)·H키(회오리베기) 도끼 착용 시 정상 발동 (검 전용 무기 감지 로직 수정)
- ✅fix2 : 회오리베기 — 도끼 장착 시 피해 발생 (GetCurrentWeapon으로 교체, m_skill 도끼 숙련도 적용)
- ✅fix3 : 회오리베기 — 도끼 착용 시 2·3차 공격 모션 정상 작동 (2초 세컨더리 대신 프라이머리 공격으로 교체)
- ✅fix4 : 도끼 — 인벤토리 마우스오버 시 검/도끼 전문가 트리 보너스 표시 (전문가 공격력, 빠른 베기/진검승부 공격속도, 칼날 되치기, 2H 도끼 공방일체)
- ✅improve2 : 방패돌진 툴팁 — 설명 단순화, 데미지 줄에 막기력 % 명시 및 레벨별 수치 표시 (7개 언어 업데이트)
- ✅fix5 : 회오리베기 — 검·도끼 모두 폴암(Atgeir) 세컨드 모션으로 통일, ZSyncAnimation.SetTrigger 직접 호출 (StartAttack 블로킹 우회; 검 3차 모션 미발동 수정, 도끼 회전 모션 추가)
- ✅improve3 : 분노의 망치 — Lv1 툴팁 설명을 스킬 동작 문구("전방 점프 돌진하여 연속 공격으로 내려침")로 교체 (7개 언어)
- ✅fix6 : 도끼 — 제작전문가 WeaponSpd 마법부여 툴팁 정상 표시 (HasAny 조건에 ProducerEnchantSpd 추가)
- ✅fix7 : 도끼 — melee_speed1 공격속도 보너스 도끼에도 적용 (isMelee 조건에 Axes 추가)
- ✅fix8 : 로그 — Config fallback 값 BindServerSync 기본값으로 수정 (공격력버프 35→30%, 스태미나 20→25, 독즉시데미지 20→40%)
- ✅fix9 : 로그 — fallback 툴팁 수치 수정 (Lv1 공격력 35→30%, 폭발횟수 8→6회)
- ✅fix10 : 로그 — rogue_poison_info 독일어·일본어·중국어·포르투갈어 번역 누락 수정 (영어 fallback 표시 문제 해결)
- ✅fix11 : 마법사 — Lv3/4/5 패시브 툴팁에 "이중시전 +1회" 텍스트 정상 표시 (default 분기로 떨어져 lv1 텍스트 출력되던 버그 수정)
- ✅fix12 : 탱커 — Lv3/4/5 패시브 툴팁이 lv3 키 사용 (lv2 텍스트의 "선행 스킬 30초 내 +1회 사용" 오표시 수정)
- ✅improve4 : 이중시전 — 레벨별 데미지를 Config 연동으로 변경 (기본값 Lv1=60%, 레벨당 +5%) Lv1~7: 60/65/70/75/80/85/90%; 7개 언어 툴팁 실시간 반영

# [1.24.31] - 2026-05-24
- ✅improve1 : Axe — inventory tooltip now shows applicable skill bonuses (melee attack speed, Finisher Melee for 1H axes, Two-Handed Crush for Battleaxe, Melee Expert flat bonus)
- ✅improve2 : Sword Expert tree renamed to "Sword & Axe Expert" — all passives and actives now activate with sword or axe equipped; Attack & Defense (공방일체) supports two-handed axe; slash damage correctly applied to axe weapons per damage rules
- ✅improve3 : Archer / Rogue / Mage job Lv1 upgrade requirement changed — Greydwarf trophy replaced with Bear Trophy (Eikthyr Trophy + Bear Trophy)
- ✅improve4 : Mage job tooltip — prerequisite updated to "Staff or Wand equipped, Mage job"; cooldown text removed from passive description (all levels)
- ✅fix5 : Archer Lv2 unlock condition tooltip — "ExplosiveFire" corrected to "Explosive Arrow"
- ✅new1 : Archer Lv2 — OneShot / Explosive Arrow: extra use expires after 30s (was permanent); tooltip now shows this effect
- ✅fix6 : Arrow Rain Lv2 upgrade requirement corrected — Greydwarf Shaman Trophy replaced with Troll Trophy (tooltip + actual item check/consume)
- ✅improve5 : Paladin heal cooldown now scales per level — Lv2: 29s, Lv3: 28s, Lv4: 27s, Lv5+: 26s (previously Lv1–4: 30s, Lv5: 25s)
-
- ✅improve1 : 도끼 — 인벤토리 툴팁에 적용 스킬 보너스 표시 (근접 공격속도, 한손 도끼의 연속 근접의 대가, 배틀액스의 양손 분쇄, 근접 전문가 고정 보너스)
- ✅improve2 : 검 전문가 트리를 "검, 도끼 전문가"로 확장 — 패시브/액티브 전체 필요조건 검 또는 도끼 착용으로 변경; 공방일체 양손도끼 지원; 도끼 공격 시 slash 데미지 보너스 정상 적용
- ✅improve3 : 아처 / 로그 / 마법사 직업 Lv1 업그레이드 조건 변경 — 그레이드워프 트로피 → 곰 트로피 (에이크쉬르 트로피 + 곰 트로피)
- ✅improve4 : 마법사 직업 툴팁 — 필요조건 "지팡이 또는 완드 착용, 메이지 직업"으로 수정; 패시브 설명에서 쿨타임 텍스트 제거 (전 레벨)
- ✅fix5 : 아처 Lv2 해금 조건 툴팁 "폭발사격" → "폭발화살" 오탈자 수정
- ✅new1 : 아처 Lv2 — 단 한 발/폭발화살 추가 차지 30초 이내 미사용 시 만료 (기존 영구 유지); 툴팁에 해당 효과 표기 추가
- ✅fix6 : 화살비 Lv2 업그레이드 조건 수정 — 그레이드워프 주술사 트로피 → 트롤 트로피 (툴팁 및 실제 아이템 체크/소비 동시 수정)
- ✅improve5 : 성기사 힐 쿨타임 레벨별 차등 적용 — Lv2: 29초, Lv3: 28초, Lv4: 27초, Lv5+: 26초 (기존: Lv1~4 공통 30초, Lv5 25초)

# [1.24.23] - 2026-05-23
- ✅improve1 : Rush Slash tooltip — redesigned to match Assassin's Heart format (Lv stat preview, per-level damage preview Lv1~7, direct upgrade item list)
- ✅fix1 : Stack Explosion tooltip — Lv2~7 boss trophy names corrected (localization key item_XXX_trophy → item_trophy_XXX)
- ✅fix2 : Penetrating Spear (spear_Step5_penetrate) MaxLevel fixed 1→7 — Lv2~7 upgrade was silently blocked
- ✅improve2 : Shield Charge — enemies within 3m are pulled to player's front and pushed 12m with shield; first enemy: single hit at 250%+20%/lv of block power, others: multi-hit equal to gathered count at 150%+20%/lv per hit; tooltip & 7-language localization updated
- ✅fix3 : Shield Charge / Fury Hammer prerequisite message corrected — "Mace Master required" → "Swift Attack required" (actual name of mace_Step6_grandmaster); 7-language localization updated
- ✅improve3 : Stack Explosion tooltip — level display changed from Lv1/7 to Lv1
- ✅fix4 : Shield Charge — removed mutual exclusivity with Mind Shield (unrelated skills were incorrectly blocking each other)
- ✅fix5 : Shield Charge — damage rebalanced: base damage 250% → 100% of block power; area multi-hit during movement completely removed; gathered enemies receive N × (150%+20%/lv) hits at dash end
-
- ✅improve1 : 돌진 연속 베기 툴팁 — 암살자의 심장과 동일한 형식으로 변경 (현재 Lv 피해 수치 미리보기, Lv1~7 단계별 피해 프리뷰, 다음 레벨 요구 아이템 직접 표시)
- ✅fix1 : 약점폭발 툴팁 — Lv2~7 보스 트로피 이름 키 오류 수정 (잘못된 키 item_XXX_trophy → 올바른 키 item_trophy_XXX)
- ✅fix2 : 꿰뚫는 창(spear_Step5_penetrate) MaxLevel 1→7 수정 — Lv1 이후 Lv2~7 업그레이드가 조용히 막히던 버그 수정
- ✅improve2 : 방패돌진 — 돌진 시작 시 3m 이내 적을 전방에 끌어모아 12m 밀고 이동; 첫 번째 적: 막기력의 250%+레벨당 20% 단일 타격, 나머지: 끌어모은 수만큼 다단히트 (막기력 150%+레벨당 20%); 툴팁 및 7개 언어 현지화 업데이트
- ✅fix3 : 방패돌진 / 분노의 망치 선행 스킬 메시지 오류 수정 — "둔기 마스터 필요" → "속공 필요" (mace_Step6_grandmaster의 실제 이름); 7개 언어 현지화 업데이트
- ✅improve3 : 약점폭발 툴팁 — 레벨 표시 형식 Lv1/7 → Lv1으로 변경
- ✅fix4 : 방패돌진 — 마인드 쉴드와의 상호배타 설정 제거 (무관한 스킬 간 배타 조건이 잘못 적용되던 버그 수정)
- ✅fix5 : 방패돌진 — 데미지 재조정: 기본 단일 공격력 250% → 막기력의 100%; 이동 중 광역 다단히트 완전 제거; 끌어모은 적은 돌진 종료 시 N × (150%+레벨당 20%) 적용

# [1.24.07] - 2026-05-22
- ✅fix1 : Arrow Rain / Rush Slash / Whirlwind Slash Lv6 — TrophySeekerBrute now recognized correctly (HaveItem & RemoveItem with matchWorldLevel=false)
- ✅improve1 : Korean tooltip — "시커 브루트 트로피" renamed to "추적자 병사 트로피"
- ✅improve2 : Korean tooltip — "드로그르 엘리트 트로피" renamed to "정예 드라우그 트로피"
-
- ✅fix1 : 화살비 / 돌진 연속 베기 / 회오리 베기 Lv6 — 시커 브루트 트로피 인식 불가 버그 수정 (matchWorldLevel=false 적용)
- ✅improve1 : 한국어 툴팁 — "시커 브루트 트로피" → "추적자 병사 트로피" 변경
- ✅improve2 : 한국어 툴팁 — "드로그르 엘리트 트로피" → "정예 드라우그 트로피" 변경

# [1.24.06] - 2026-05-23
- ✅fix1 : Dual Cast — first-learn dialog now appears correctly (removed erroneous `skillLevel < 1` guard that blocked Lv1 upgrade dialog)
- ✅fix2 : Stack Explosion — Lv1 upgrade requirement corrected: now requires Eikthyr Trophy ×1 + Troll Trophy ×1 (was Troll Trophy ×1 only)
-
- ✅fix1 : 이중시전 — 처음 배울 때 업그레이드 다이얼로그가 표시되지 않던 버그 수정 (레벨 0 상태에서 다이얼로그를 차단하던 조건 제거)
- ✅fix2 : 약점폭발 — Lv1 강화 조건 수정: 에이크쉬르 트로피 ×1 + 트롤 트로피 ×1 필요 (기존: 트롤 트로피 ×1만 필요)

# [1.24.04] - 2026-05-22
- ✅fix1 : Dual Cast tooltip — Lv1~7 description updated from "Damage X%" to "Fires 7 fireballs — each dealing X% weapon damage"
-
- ✅fix1 : 이중시전 툴팁 — 전 레벨 설명을 "공격력 X%"에서 "화염구 7발 발사 - 1발당 무기 공격력의 X%"로 수정

# [1.24.01] - 2026-05-21
- ✅improve1 : Assassin's Heart tooltip — required points section added (cyan label / red value, reads from Config); matches CrossbowOneShot format
- ✅improve2 : Dual Cast tooltip — required points section added (cyan label / red value, reads from Config); matches CrossbowOneShot format
- ✅fix1 : Rush Slash tooltip — required points was hardcoded "3"; now reads from `Sword_Config.RushSlashRequiredPointsValue` (Config-adjustable)
- ✅new1 : Fury Hammer (H-key) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); hits 1-4 damage scales 40%+5%/lv, hit 5 scales 60%+10%/lv; MaxLevel corrected 1→7; requires Grandmaster prerequisite; 7-language upgrade dialog (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve3 : Fury Hammer tooltip redesigned to level-aware format — shows current level damage (hits 1-4 %, hit 5 %), next-level trophy requirement, Lv2~7 preview section
- ✅new2 : Pierce Charge (G-key, polearm) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); primary damage scales 200%+30%/lv → Lv7 380%; AOE independent table 150/175/200/225/250/275/300%; MaxLevel corrected 1→7; 7-language upgrade dialog (KO/EN/DE/RU/ZH-CN/JA/PT-BR); prerequisite check (any of Suppress/Moon Slash/Ground Wheel)
- ✅improve4 : Pierce Charge tooltip redesigned to level-aware format — shows current level damage (primary%·AOE%), next-level trophy requirement, Lv2~7 preview section
- ✅new3 : Polearm Whirlwind (Mouse2, polearm) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); hit damage scales 35%+10%/lv → Lv7 95%; AoE independent table 15/20/25/30/35/40/50%; MaxLevel corrected 1→7; 7-language upgrade dialog; prerequisite check (Pierce Charge required)
- ✅improve5 : Polearm Whirlwind tooltip redesigned to level-aware format — shows current Lv{N}: hit%·AoE%, next-level trophy requirement, Lv2~7 preview section
- ✅improve6 : Pierce Charge tooltip — skill name header now shows [Lv{N}/7] suffix when learned; required-points line moved before upgrade-trophy line (matches CrossbowOneShot section order)
- ✅new4 : Spear Combo (H-key) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); throws scale Lv1=3 → Lv7=9 (+1/lv); per-throw damage 80%+10%/lv → Lv7 140%; MaxLevel corrected 1→7; 7-language upgrade dialog (KO/EN/DE/RU/ZH-CN/JA/PT-BR); prerequisite Triple Spear (spear_Step4_triple)
- ✅fix2 : Spear Combo ApplyHitToMonster() — damageBonus was read but never applied to HitData.m_damage; projectile hits now correctly scale by level-computed bonus (stored in spearComboDamageBonus dictionary)
- ✅improve7 : Spear Combo tooltip redesigned to level-aware format — shows current Lv/throws/damage%, next-level trophy requirement, Lv2~7 preview section
- ✅new5 : Piercing Spear (G-key, spear) completely rewritten — replaced buff+lightning system with 12m forward dash charge mechanic; charges Lv1=1, Lv3=2, Lv5=3, Lv7=4; single pierce damage 100%+5%/lv, 5m area 80%+5%/lv; Lv1~7 upgrade system (boss+biome trophy pairs); knife-animation passive removed; 7-language localization
- ✅improve8 : Piercing Spear tooltip redesigned to CrossbowOneShot format — shows current Lv: single%·area%·N charges, next-level trophy requirement, Lv2~7 preview section
- ✅fix3 : Rush Slash — removed mutual exclusivity with Mind Shield; both skills can now be learned independently
-
- ✅improve1 : 암살자의 심장 툴팁 — 필요 포인트 섹션 추가 (청록/빨강, Config 값 사용); 단 한 발 형식과 통일
- ✅improve2 : 이중시전 툴팁 — 필요 포인트 섹션 추가 (청록/빨강, Config 값 사용); 단 한 발 형식과 통일
- ✅fix1 : 돌진베기 툴팁 — 필요 포인트 하드코딩 "3" → `Sword_Config.RushSlashRequiredPointsValue` Config 값으로 교체
- ✅new1 : 분노의 망치(H키) Lv1~7 업그레이드 시스템 — 바이옴 보스+몬스터 트로피 쌍 (에이크쉬르+멧돼지 → 페이더+폴른발키리); 1~4타 40%+5%/레벨, 5타 60%+10%/레벨; MaxLevel 1→7 수정; 둔기 마스터 선행 스킬 필요; 7개 언어 업그레이드 다이얼로그 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve3 : 분노의 망치 툴팁 레벨 연동 형식으로 개편 — 현재 레벨 데미지(1~4타 %, 5타 %), 다음 레벨 트로피 요구사항, Lv2~7 미리보기 섹션 표시
- ✅new2 : 관통 돌격(G키, 폴암) Lv1~7 업그레이드 시스템 추가 — 바이옴 보스+몬스터 트로피 쌍 (에이크쉬르+멧돼지 → 페이더+타락한발키리); 직격 200%+30%/레벨 → Lv7 380%; AOE 독립 수치표 150/175/200/225/250/275/300%; MaxLevel 1→7 수정; 7개 언어 강화 다이얼로그; 선행 스킬(제압 공격/반달 베기/지면 강타 중 하나) 체크
- ✅improve4 : 관통 돌격 툴팁 레벨 연동 형식으로 재설계 — 현재 레벨 데미지(직격%·AOE%), 다음 레벨 트로피 요건, Lv2~7 미리보기 섹션 표시
- ✅new3 : 폴암 휠윈드(Mouse2, 폴암) Lv1~7 업그레이드 시스템 추가 — 바이옴 보스+몬스터 트로피 쌍 (에이크쉬르+멧돼지 → 페이더+타락한발키리); 직격 35%+10%/레벨 → Lv7 95%; 광역 독립 수치표 15/20/25/30/35/40/50%; MaxLevel 1→7 수정; 7개 언어 강화 다이얼로그; 선행 스킬(관통 돌격) 체크
- ✅improve5 : 폴암 휠윈드 툴팁 레벨 연동 형식으로 재설계 — 현재 Lv{N}: 직격%·광역%, 다음 레벨 트로피 요건, Lv2~7 미리보기 섹션 표시
- ✅improve6 : 관통 돌격 툴팁 — 스킬명 뒤에 [Lv{N}/7] 표시 추가; 필요 포인트 줄을 강화 트로피 줄 앞으로 이동 (단 한 발 형식과 통일)
- ✅new4 : 연공창(H키) Lv1~7 업그레이드 시스템 추가 — 바이옴 보스+몬스터 트로피 쌍 (에이크쉬르+멧돼지 → 페이더+타락한발키리); 투창 횟수 Lv1=3 → Lv7=9 (+1/레벨); 투창당 데미지 80%+10%/레벨 → Lv7 140%; MaxLevel 1→7 수정; 7개 언어 강화 다이얼로그 (KO/EN/DE/RU/ZH-CN/JA/PT-BR); 선행 스킬: 이연창 필요
- ✅fix2 : 연공창 ApplyHitToMonster() — damageBonus가 읽히기만 하고 HitData.m_damage에 적용되지 않던 버그 수정; 이제 레벨 기반 보너스(spearComboDamageBonus 딕셔너리 저장값)가 투창 피격 시 실제 반영
- ✅improve7 : 연공창 툴팁 레벨 연동 형식으로 재설계 — 현재 Lv/투창 횟수/데미지%, 다음 레벨 트로피 요건, Lv2~7 미리보기 섹션 표시
- ✅new5 : 꿰뚫는 창(G키, 창) 완전 재설계 — 버프+번개 시스템을 12m 전방 돌진 회수 방식으로 교체; 회수 Lv1=1, Lv3=2, Lv5=3, Lv7=4; 단일 관통 피해 100%+5%/레벨, 5m 범위 80%+5%/레벨; Lv1~7 업그레이드 시스템(바이옴 보스+몬스터 트로피 쌍); 단검 모션 패시브 제거; 7개 언어 로컬라이제이션
- ✅improve8 : 꿰뚫는 창 툴팁 단 한 발 형식으로 재설계 — 현재 Lv: 단일%·범위%·N회 돌진, 다음 레벨 트로피 요건, Lv2~7 미리보기 섹션 표시
- ✅fix3 : 돌진 연속 베기 — 마인드 쉴드와의 상호 배타 관계 제거; 이제 두 스킬을 동시에 습득 가능

# [1.23.89] - 2026-05-20
- ✅new1 : Stack Explosion (H-key) Lv1~7 upgrade system — boss+biome trophy pairs (Elder+Skeleton → Fader+CharredArcher); damage per stack scales 30%→60% (+5%/lv); MaxLevel corrected 1→7; 7-language upgrade dialog (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve1 : Stack Explosion tooltip redesigned to BowExplosive format — shows Lv{X}/7 current stats, next-level trophy requirement, Lv2~7 damage preview section; all 7 languages
- ✅new2 : Rush Slash (G-key) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); damage bonus scales +10%/lv above base (config); MaxLevel corrected 1→7; 7-language upgrade dialog (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve2 : Rush Slash tooltip redesigned to Archer format — shows Lv{X}/7 with level-adjusted damage values (eff1/eff2/eff3), next-level trophy requirements; all 7 languages
- ✅fix2 : Bug fix — HandleRushSlashClick was missing node.Id guard; caused all skill clicks to show Rush Slash trophy requirement error and block normal skill investment
- ✅fix1 : Dual Cast — clicking other skill nodes was blocked when Dual Cast was Lv1+; missing node.Id check in HandleDualCastClick caused it to intercept all node clicks (fix: add `if (node.Id != "staff_Step6_dual_cast") return false`)
- ✅fix3 : Rush Slash Lv2 — corrected trophy item to prefab name `TrophyFrostTroll`; tooltip display changed from "Frost Troll Trophy" to "Troll Trophy"
- ✅fix4 : Lv2 upgrade trophy changed from `TrophyFrostTroll` (Frost Troll) to `TrophyForestTroll` (Forest Troll / "트롤 트로피") across all 5 Lv1~7 skills — Dual Cast, Staff Heal, Assassin's Heart, Explosive Arrow, Rush Slash
- ✅fix5 : Lv2 trophy display key corrected from `item_trophy_frosttroll` to `item_trophy_troll` in GetMissing and tooltip files — Dual Cast, Staff Heal, Assassin's Heart, Explosive Arrow, Rush Slash tooltips now use the correct Forest Troll key consistently
- ✅improve3 : Rapid Fire — first bolt fixed at 100% damage; 2nd and 3rd bolts scale with Config damage% (default 35%)
- ✅fix6 : Lv2 Troll Trophy inventory check fixed — replaced `TrophyForestTroll` (prefab name) with `$item_trophy_troll` (Valheim localization key); all 5 Lv1~7 skills now correctly detect the trophy in inventory (Dual Cast, Staff Heal, Assassin's Heart, Explosive Arrow, Rush Slash)
- ✅fix7 : `item_trophy_troll` key added to DE/JA/RU/ZH-CN JSON files; key was missing, causing Troll Trophy name to display as empty string in non-KO/EN locales for all Lv2 upgrade skills
- ✅fix8 : Fixed missing prerequisite skill check in 7 Lv1~7 upgrade skills — Lv1 unlock was possible without learning the required skill first; Arrow Rain/Explosive Arrow need Precision Shot, Dual Cast/Heal need Lucky Mana, Assassin's Heart/Stack Explosion need Assassination, Rush Slash needs True Duel
- ✅fix9 : Stack Explosion upgrade tooltip — 6 trophy name localization keys corrected (item_xxx_trophy → item_trophy_xxx); trophy names displayed as raw localization keys instead of translated names
- ✅fix10 : One Shot (crossbow) — fixed missing prerequisite check; Lv1 unlock was possible without First Strike (crossbow_Step5_final)
- ✅fix11 : Ice Breath (crossbow) — fixed missing prerequisite check; Lv1 unlock was possible without One Shot (crossbow_Step6_expert)
- ✅improve4 : One Shot tooltip redesigned to Archer format — removed [Lv/7] suffix, Lv{n}: damage%/AOE/duration single line, passive line for reload penalty, next-level trophy requirement, Lv2~7 preview; extracted to CrossbowOneShot_Tooltip.cs
- ✅new3 : Whirlwind Slash (H-key) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); base damage scales +20/lv (config); MaxLevel corrected 1→7; 7-language upgrade dialog (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve5 : Whirlwind Slash tooltip upgraded to level-aware format — shows Lv{X}/7, level-adjusted 3-phase damage (phase 1/2/3), next-level trophy requirement section
- ✅fix12 : One Shot (crossbow) — prerequisite relaxed; now requires either First Strike OR Rapid Fire Lv2 (previously First Strike only); 7 languages updated
- ✅improve6 : Whirlwind Slash tooltip redesigned to CrossbowOneShot format — 10-section layout: skill name with [Lv/7], 3-phase damage summary (d1/d2/d3 + AOE radii), knockback passive, stamina, H-key type, cooldown, requirements, required points, next-level trophy, Lv2~7 damage preview
- ✅new4 : Shield Charge (G-key, mace) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); single-hit scales 70%+15%/lv → Lv7 160%; multi-hit independent table 60/70/80/90/100/110/120%; MaxLevel corrected 1→7; 7-language upgrade dialog (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve7 : Shield Charge tooltip redesigned to level-aware format — shows Lv{X}/7 current stats (single%·multi%), next-level trophy requirement, Lv2~7 damage preview section; all 7 languages
-
- ✅new1 : 약점폭발 (H키) Lv1~7 강화 시스템 추가 — 보스+바이옴 트로피 쌍 필요 (장로+해골 → 페이더+차르드 아처); 스택당 데미지 30%→60% (+5%/레벨); MaxLevel 1→7 수정; 7개 언어 강화 다이얼로그 지원
- ✅improve1 : 약점폭발 툴팁 BowExplosive 형식으로 재설계 — 현재 Lv{X}/7 스탯, 다음 레벨 트로피 요건, Lv2~7 데미지 미리보기 구분선 포함; 7개 언어 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅new2 : 돌진 연속 베기 (G키) Lv1~7 강화 시스템 추가 — 보스+바이옴 트로피 쌍 필요 (에기크쉬르+멧돼지 → 페이더+타락한발키리); 레벨당 데미지 +10% (config 조정 가능); MaxLevel 1→7 수정; 7개 언어 강화 다이얼로그 지원
- ✅improve2 : 돌진 연속 베기 툴팁 아처 형식으로 재설계 — 현재 Lv{X}/7 레벨 보정 데미지 (eff1/eff2/eff3) 표시, 다음 레벨 트로피 조건 섹션 포함; 7개 언어 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅fix1 : 이중시전 — 이중시전 Lv1 이상 상태에서 다른 스킬 노드 클릭 시 학습 불가 버그 수정; HandleDualCastClick에 node.Id 체크 누락으로 모든 노드 클릭을 가로채던 문제 해결
- ✅fix2 : 돌진베기 클릭 핸들러 node.Id 가드 누락 버그 수정 — 다른 모든 스킬 클릭 시 돌진베기 트로피 조건 오류 메시지가 표시되고 정상 투자가 차단되던 문제 해결
- ✅fix3 : 돌진베기 Lv2 트로피 아이템명 수정 — `TrophyFrostTroll` 프리팹명으로 교정; 툴팁 표시 "서리 트롤 트로피" → "트롤 트로피" 변경
- ✅fix4 : Lv2 강화 트로피를 `TrophyFrostTroll`(서리 트롤)에서 `TrophyForestTroll`(트롤 트로피)로 변경 — 이중시전, 힐, 암살자의 심장, 폭발화살, 돌진베기 5개 스킬 일괄 적용
- ✅fix5 : GetMissing 및 툴팁 파일에서 Lv2 트로피 표시 키를 `item_trophy_frosttroll`에서 `item_trophy_troll`로 교정 — 이중시전·힐·암살자의심장·폭발화살·돌진베기 5개 스킬 툴팁에 올바른 트롤 트로피 키 적용
- ✅improve3 : 연속 발사 — 첫 번째 볼트 100% 고정 데미지; 2~3번째 볼트만 Config 데미지% (기본 35%) 적용
- ✅fix6 : Lv2 트롤 트로피 인벤토리 체크 버그 수정 — `TrophyForestTroll` (프리팹명) → `$item_trophy_troll` (Valheim 로컬라이제이션 키) 교체; 인벤에 트로피가 있어도 조건 미충족 오류 발생하던 버그 해결 (이중시전·힐·암살자의심장·폭발화살·돌진베기)
- ✅fix7 : DE/JA/RU/ZH-CN JSON에 `item_trophy_troll` 키 추가; 누락으로 인해 외국어 환경에서 Lv2 강화 시 트롤 트로피명이 공백으로 표시되던 버그 수정
- ✅fix8 : 7개 Lv1~7 강화 스킬의 선행 스킬 체크 누락 수정 — 선행 스킬 없이도 Lv1 첫 학습 가능하던 버그; 화살비/폭발화살→정조준, 이중시전/힐→행운마력, 암살자의심장/약점폭발→암살술, 돌진베기→진검승부
- ✅fix9 : 약점폭발 강화 툴팁 트로피 이름 키 6개 오타 수정 (item_xxx_trophy → item_trophy_xxx); 트로피명이 번역 대신 로컬라이제이션 키 문자열로 표시되던 버그 수정
- ✅fix10 : 단 한 발 (석궁) — 선행 스킬 체크 누락 수정; 결전의 일격 없이 Lv1 학습 가능하던 버그 수정
- ✅fix11 : 빙결폭발탄 (석궁) — 선행 스킬 체크 누락 수정; 단 한 발 없이 Lv1 학습 가능하던 버그 수정
- ✅improve4 : 단 한 발 툴팁 아처 형식으로 재설계 — [Lv/7] 접미사 제거, Lv{n}: 데미지%/AOE/지속시간 한 줄, 재장전 패널티 패시브 라인, 다음 레벨 트로피 조건, Lv2~7 미리보기 구분선; CrossbowOneShot_Tooltip.cs로 분리
- ✅new3 : 회오리베기 (H키) Lv1~7 강화 시스템 추가 — 보스+바이옴 트로피 쌍 필요 (에이크쉬르+멧돼지 → 페이더+타락한발키리); 레벨당 기저데미지 +20 (config 조정 가능); MaxLevel 1→7 수정; 7개 언어 강화 다이얼로그 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve5 : 회오리베기 툴팁 레벨 연동 형식으로 업그레이드 — Lv{X}/7 표시, 레벨 보정 3단계 데미지 (1/2/3단계 각각), 다음 레벨 트로피 조건 섹션 추가
- ✅fix12 : 단 한 발 (석궁) — 선행 스킬 조건 완화; 결전의 일격 또는 연속 발사 Lv2 중 하나면 학습 가능 (기존: 결전의 일격만); 7개 언어 업데이트
- ✅improve6 : 회오리베기 툴팁 CrossbowOneShot 형식으로 재설계 — 10-Section 구조: 스킬명[Lv/7], 3단계피해요약(d1/d2/d3+AOE반경), 밀어내기패시브, 스태미나, H키유형, 쿨타임, 조건, 필요포인트, 다음레벨트로피조건, Lv2~7데미지미리보기
- ✅new4 : 방패돌진(G키, 둔기) Lv1~7 업그레이드 시스템 — 보스+바이옴 트로피 조합(에이크쉬르+멧돼지 → 페이더+타락한발키리); 단일타격 70%+15%/레벨 → Lv7 160%; 다단히트 독립 수치 60/70/80/90/100/110/120%; MaxLevel 1→7 수정; 7개 언어 업그레이드 다이얼로그
- ✅improve7 : 방패돌진 툴팁 레벨 연동 형식으로 개선 — Lv{X}/7 현재 스탯(단일%·다단%), 다음 레벨 트로피 요구사항, Lv2~7 데미지 미리보기 섹션; 7개 언어 적용

# [1.23.66] - 2026-05-19
- ✅new1 : Crossbow One Shot (R-key) Lv1~7 upgrade system added — requires boss+biome trophy pairs (Eikthyr+Boar → Fader+Morgen); AOE damage scales 200%→440% (+40%/lv); base damage changed 400%→200%; MaxLevel corrected 1→7
- ✅improve1 : One Shot tooltip upgraded to level-aware format — shows current-Lv AOE damage%, trophy requirements (displayed from Lv0 unlearned), and Lv2~7 progression preview with divider; all 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅fix1 : One Shot — item_trophy_goblin (Lv5) and item_trophy_seeker (Lv6) keys added to all 7 languages; previously showed empty text for those trophy names
- ✅improve2 : Heal tooltip redesigned to match ArrowRain format — `Lv{X}: Heal X% · CD Xs` as main stat line, standalone orange/red trophy-requirement line, and divider+Lv2~7 preview section; struct+generator pattern removed; all 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅new2 : Assassin's Heart (G-key) Lv1~7 upgrade system — boss+biome trophy pairs (Eikthyr+Boar → Fader+FallenValkyrie); attack count 3→6 by every 2 levels; crit multiplier base+0.2×/lv (config); stamina changed to flat cost; MaxLevel corrected 1→7; all 7 languages
- ✅improve3 : Assassin's Heart tooltip redesigned to BowExplosive format — current-Lv stats (attacks + crit ×), next-level trophy requirement, Lv2~7 progression preview; all 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
-
- ✅new1 : 석궁 단 한 발 (R키) Lv1~7 강화 시스템 추가 — 보스+바이옴 트로피 쌍 필요 (에기크쉬르+멧돼지 → 페이더+모르겐); AOE 데미지 200%→440% (+40%/lv); 기본 데미지 400%→200% 변경; MaxLevel 1→7 수정
- ✅improve1 : 단 한 발 툴팁 레벨 표시 형식으로 업그레이드 — 현재 Lv AOE 데미지%, 트로피 요건 (Lv0 미학습 시부터 표시), Lv2~7 미리보기 구분선 포함; 7개 언어 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅fix1 : 단 한 발 — item_trophy_goblin(Lv5)과 item_trophy_seeker(Lv6) 키를 7개 언어에 추가; 기존에는 해당 트로피명이 빈 텍스트로 표시되던 버그 수정
- ✅improve2 : 힐 툴팁 ArrowRain 형식으로 재설계 — `Lv{X}: 힐 X% · 쿨 Xs` 메인 스탯 라인, 주황/빨강 트로피 조건 독립 라인, 구분선+Lv2~7 프리뷰 섹션으로 구조 통일; struct+generator 패턴 제거; 7개 언어 지원
- ✅new2 : 암살자의 심장 (G키) Lv1~7 강화 시스템 추가 — 보스+바이옴 트로피 쌍 필요 (에이크쉬르+멧돼지 → 페이더+타락한발키리); 공격 횟수 2단계마다 3→6회 증가; 크리티컬 배율 기본+0.2배/레벨 (config 조정 가능); 스태미나 % → 플랫 변경; MaxLevel 1→7; 7개 언어
- ✅improve3 : 암살자의 심장 툴팁 BowExplosive 형식으로 재설계 — 현재 Lv 스탯 (공격 횟수, 크리티컬 ×), 다음 레벨 트로피 요건, Lv2~7 미리보기 구분선 포함; 7개 언어 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)

# [1.23.62] - 2026-05-18
- ✅improve1 : Vanilla preset config — added 154 missing keys across 27 sections (all languages); attack/multi-hit damage values set to 1/5 of VeryHard, MultiShot fire interval ×5 (slower); cooldowns, durations, HP bonuses, success rates, and speed bonuses match VeryHard
- ✅new1 : Explosive Arrow (R-key) Lv1~7 system added — Lv1 requires Eikthyr + Boar trophy; Lv2~7 each require a pair of boss/biome trophies (Elder+FrostTroll → Fader+FallenValkyrie); damage scales 80%→200% (+20%/lv), area damage is 70% of direct hit; explosion radius 7m, flat stamina cost 15
- ✅improve2 : Explosive Arrow tooltip upgraded to level-aware format — shows current Lv damage (80%→200%) and area damage (56%→140%), next-level trophy requirements, and Lv2~7 progression preview; MaxLevel corrected 1→7; all 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅fix1 : Explosive Arrow — Lv0→Lv1 first-learn now correctly requires Eikthyr + Boar trophies; removed `currentLevel >= 1` bypass at 3 check points (SkillTreeUI, HandleExplosiveArrowClick, SkillTreeManager)
- ✅improve3 : Explosive Arrow area damage redesigned from flat 70% of direct hit to level-specific absolute values (Lv1:55%, Lv2:70%, Lv3:85%, Lv4:100%, Lv5:115%, Lv6:130%, Lv7:150%); tooltip updated accordingly
- ✅fix2 : PassiveMessageDisplay config (Center/TopLeft/Off) now applies to all skill messages — job skill activations (Y-key), condition messages (cooldown/stamina/weapon checks), passive procs, and crafting enchants; previously ~25 calls across 12 files used direct MessageHud bypassing config; Critical/XLarge/AlwaysCenter types now also respect Off setting
- ✅new2 : Arrow Rain (H-key) Lv1~7 system added — Lv1 requires Eikthyr + Boar trophy; Lv2~7 each require boss/biome trophy pairs (Elder+GreydwarfShaman → Fader+Vulture); per-arrow damage scales 8%→20% (+2%/lv); upgrade confirmation dialog with gold border UI
- ✅improve4 : Arrow Rain level system fully localized across 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR); trophy item names, upgrade prompts, and max-level notices added
- ✅new3 : Dual Cast (R-key) Lv1~7 system added — Lv1 requires Eikthyr + Boar trophy; Lv2~7 each require boss/biome trophy pairs (Elder+FrostTroll → Fader+FallenValkyrie); damage scales 60%→120% (+10%/lv); purple-themed upgrade confirmation dialog; MaxLevel corrected 1→7
- ✅improve5 : Dual Cast tooltip upgraded to level-aware format — shows current Lv damage (60%→120%), next-level trophy requirements, and Lv2~7 damage progression preview; all 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve6 : Dual Cast tooltip redesigned to match Explosive Arrow / Arrow Rain style — shows current-Lv damage%, next-level trophy requirements, and Lv2~7 damage progression preview separated by a divider line; all 7 languages
- ✅improve7 : Arrow Rain tooltip upgraded to level-aware format — shows current-Lv per-arrow damage (8%→20%), next-level trophy requirements, and Lv2~7 damage progression preview; separated BowExplosive_Tooltip.cs pattern into ArrowRain_Tooltip.cs
- ✅fix3 : Arrow Rain — clicking other skill nodes was blocked by Arrow Rain's upgrade condition; missing node.Id check in HandleArrowRainClick caused it to intercept all node clicks (fix: add `if (node.Id != "bow_Step6_arrow_rain") return false`)
- ✅improve8 : Arrow Rain tooltip — changed "damage" label to "ATK" (KO: 공격력) in all 7 languages to clarify the value represents bow+arrow attack power percentage, not flat damage
- ✅improve9 : Dual Cast per-level damage rebalanced to non-uniform values — Lv1:60%, Lv2:63%, Lv3:66%, Lv4:70%, Lv5:74%, Lv6:78%, Lv7:85% (previously scaled linearly at +10%/lv); tooltip preview updated accordingly
- ✅improve10 : Explosive Arrow tooltip — renamed Korean "데미지" label to "공격력" (attack power); percentage format (공격력 {0}% · 범위 {1}%) preserved across all 7 languages
- ✅improve11 : Dual Cast tooltip — renamed Korean "데미지" label to "공격력" (attack power) to clarify the value represents staff/wand attack power percentage
- ✅new4 : Staff Heal (H-key) Lv1~7 upgrade system added — each level requires boss+biome trophy pair (Eikthyr+Boar → Fader+FallenValkyrie); heal scales 18%→35%, cooldown reduces 30s→18s (-2s/lv); green-themed upgrade confirmation dialog; MaxLevel corrected 1→7
- ✅improve12 : Heal tooltip upgraded to level-aware format — shows current Lv heal% and cooldown, next-level trophy requirements, and Lv2~7 progression preview; all 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅new5 : Crossbow Ice Breath (H-key) Lv1~7 system added — Lv1 requires Eikthyr + Boar trophy; Lv2~7 each require boss/biome trophy pairs (Elder+GreydwarfBrute → Fader+CharredWarrior); first hit scales 150%→240% (+15%/lv), DoT scales 35%→65% (+5%/lv)×5; MaxLevel corrected 1→7
- ✅improve13 : Ice Breath tooltip upgraded to level-aware format — shows current Lv first hit% and DoT%×5, next-level trophy requirements, and Lv2~7 progression preview; all 7 languages (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅fix4 : Heal tooltip level preview — replaced hardcoded Korean text in Lv2~7 loop with `staff_heal_upgrade_requires` localization key; non-Korean players now see localized text in the progression table
- ✅fix5 : Ice Breath DoT base damage corrected — config default changed from 60% to 35%; per-level values now match spec (Lv1:35%×5 → Lv7:65%×5)
-
- ✅improve1 : Vanilla 프리셋 Config — 27개 섹션(전 언어) 누락 154개 키 추가; 공격력·다중타격 데미지 수치는 베리하드의 1/5, 멀티샷 발사간격은 ×5(느리게 조정); 쿨타임·지속시간·HP보너스·성공률·이동속도보너스는 베리하드와 동일
- ✅new1 : 폭발화살(R키) Lv1~7 시스템 추가 — Lv1: 에이크쉬르+멧돼지 트로피 소모; Lv2~7: 보스/바이옴 트로피 2종 소모 (엘더+서리트롤 → 페이더+타락한발키리); 데미지 80%→200%(+20%/레벨), 범위 피해는 직격의 70%; 폭발 반경 7m, 스태미나 플랫 15 소모
- ✅improve2 : 폭발화살 툴팁 레벨 연동 개선 — 현재 레벨 데미지(80%→200%) 및 범위 피해(56%→140%), 다음 레벨 트로피 강화 조건, Lv2~7 프리뷰 표시; MaxLevel 1→7 수정; 7개 언어 지원 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅fix1 : 폭발화살 — Lv0→Lv1 첫 학습 시 에이크쉬르+멧돼지 트로피 없이 학습 가능하던 버그 수정; 3곳의 `currentLevel >= 1` 조건 제거 (SkillTreeUI, HandleExplosiveArrowClick, SkillTreeManager)
- ✅improve3 : 폭발화살 범위 피해를 직격의 고정 70%에서 레벨별 독립 수치로 변경 (Lv1:55%, Lv2:70%, Lv3:85%, Lv4:100%, Lv5:115%, Lv6:130%, Lv7:150%); 툴팁 수치 동기화
- ✅fix2 : PassiveMessageDisplay 컨피그(Center/TopLeft/Off)가 모든 스킬 메시지에 적용됨 — 직업 스킬(Y키) 발동, 조건 메시지(쿨타임/스태미나/무기 미착용), 패시브 발동, 제작 인챈트 포함; 기존 12개 파일 25곳에서 MessageHud 직접 호출로 컨피그 우회; Critical/XLarge/AlwaysCenter 타입도 Off 설정 시 숨겨짐
- ✅new2 : 화살비(H키) Lv1~7 시스템 추가 — Lv1: 에이크쉬르+멧돼지 트로피 소모; Lv2~7: 보스/바이옴 트로피 2종 소모 (엘더+그레이드워프샤먼 → 페이더+볼처); 화살당 데미지 8%→20%(+2%/레벨); 골드 테두리 업그레이드 확인 다이얼로그 추가
- ✅improve4 : 화살비 레벨 시스템 7개 언어 완전 지원 (KO/EN/DE/RU/ZH-CN/JA/PT-BR); 트로피 아이템명, 업그레이드 안내, 최대 레벨 메시지 추가
- ✅new3 : 이중시전(R키) Lv1~7 시스템 추가 — Lv1: 에이크쉬르+멧돼지 트로피 소모; Lv2~7: 보스/바이옴 트로피 2종 소모 (엘더+서리트롤 → 페이더+타락한발키리); 데미지 60%→120%(+10%/레벨); 보라색 테마 업그레이드 확인 다이얼로그; MaxLevel 1→7 수정
- ✅improve5 : 이중시전 툴팁 레벨 연동 개선 — 현재 레벨 데미지(60%→120%), 다음 레벨 트로피 강화 조건, Lv2~7 데미지 프리뷰 표시; 7개 언어 지원 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅improve6 : 이중시전 툴팁 폭발화살·화살비 스타일로 통일 — 현재 레벨 데미지%, 다음 레벨 강화 트로피, Lv2~7 데미지 프리뷰를 구분선과 함께 표시; 7개 언어 지원
- ✅improve7 : 화살비 툴팁 레벨 연동 개선 — 현재 레벨 화살당 데미지(8%→20%), 다음 레벨 강화 트로피 조건, Lv2~7 데미지 프리뷰 표시; ArrowRain_Tooltip.cs 독립 파일로 분리 (폭발화살 패턴 동일)
- ✅fix3 : 화살비 — 다른 스킬 노드 클릭 시 화살비 업그레이드 조건으로 인해 학습 불가 버그 수정; HandleArrowRainClick에 node.Id 체크 누락으로 모든 노드 클릭을 가로채던 문제 해결
- ✅improve8 : 화살비 툴팁 — "데미지" 표기를 "공격력"으로 변경 (7개 언어 동기화); 화살당 수치가 활+화살 공격력 기반임을 명확히 표현
- ✅improve9 : 이중시전 레벨별 데미지 비균등 조정 — Lv1:60%, Lv2:63%, Lv3:66%, Lv4:70%, Lv5:74%, Lv6:78%, Lv7:85% (기존 +10%/레벨 균등 증가에서 변경); 툴팁 프리뷰 동기화
- ✅improve10 : 폭발화살 툴팁 — 한국어 "데미지" 표기를 "공격력"으로 변경; 7개 언어 퍼센트 형식 유지 (공격력 {0}% · 범위 {1}%)
- ✅improve11 : 이중시전 툴팁 — 한국어 "데미지" 표기를 "공격력"으로 변경; 지팡이/완드 공격력 기반 수치임을 명확히 표현
- ✅new4 : 힐(H키) Lv1~7 시스템 추가 — 레벨별 보스+바이옴 트로피 2종 소모 (에이크쉬르+멧돼지 → 페이더+타락한발키리); 힐링량 18%→35%, 쿨타임 30초→18초 (-2초/레벨); 초록 테마 업그레이드 확인 다이얼로그; MaxLevel 1→7 수정
- ✅improve12 : 힐 툴팁 레벨 연동 개선 — 현재 레벨 힐% 및 쿨타임, 다음 레벨 강화 트로피 조건, Lv2~7 프리뷰 표시; 7개 언어 지원 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅new5 : 빙결폭발탄(H키) Lv1~7 시스템 추가 — Lv1: 에이크쉬르+멧돼지 트로피 소모; Lv2~7: 보스/바이옴 트로피 2종 소모 (엘더+그레이드워프브루트 → 페이더+타락전사); 첫타격 150%→240%(+15%/레벨), DoT 35%→65%(+5%/레벨)×5; MaxLevel 1→7 수정
- ✅improve13 : 빙결폭발탄 툴팁 레벨 연동 개선 — 현재 레벨 첫타격% 및 DoT%×5, 다음 레벨 강화 트로피 조건, Lv2~7 프리뷰 표시; 7개 언어 지원 (KO/EN/DE/RU/ZH-CN/JA/PT-BR)
- ✅fix4 : 힐 툴팁 레벨 프리뷰 테이블 — 반복문 내 하드코딩된 한국어를 `staff_heal_upgrade_requires` 로컬라이제이션 키로 교체; 비한국어 플레이어도 Lv2~7 프리뷰 표에서 현지화된 텍스트 표시
- ✅fix5 : 빙결폭발탄 DoT 기본 데미지 오류 수정 — config 기본값 60%→35%로 수정; 레벨별 수치 스펙 일치 (Lv1:35%×5 → Lv7:65%×5)

# [1.23.42] - 2026-05-17
- ✅fix1 : User Setting — new skill config keys now default to VeryHard values instead of Vanilla when selected after update; root cause was IsApplyingPreset nesting bug (ApplyVeryHard() reset the flag to false mid-execution, triggering SettingChanged → ApplyUser() prematurely); fixed with a depth counter replacing the bool flag, and ApplyUser() now delegates to ApplyVeryhardWithUserOverlay()
- ✅new1 : Berserker per-level Config — rage cooldown (Lv1:45s→Lv5:35s), duration (Lv1:20s→Lv5:25s), passive max HP bonus (Lv1:40→Lv5:120 flat), and damage per 1% HP lost (Lv1:1.5%→Lv5:2.0%) are now each independently configurable per level via F1 config manager
- ✅improve1 : Berserker — removed Lv3 rage damage reduction, Lv4 low-HP attack bonus, and Lv5 passive enhancements (cooldown reduction + invincibility bonus); passive tooltip and all 7 locale strings updated to reflect simplified per-level progression
- ✅improve2 : F1 Config Manager — added DE/RU/CN/JA/PT-BR translations for 20 new Berserker per-level Config keys (Lv1~5 cooldown, duration, HP bonus, damage-per-HP); previously showed raw key names for non-KO/EN players
- ✅improve3 : Sword Expert — Whirlwind Slash (H-key) HUD slot icon changed from generic attack icon to sword expert icon (sword_unlock)
-
- ✅fix1 : 유저 설정 — 업데이트 후 "3. User Setting" 선택 시 신규 스킬 config 키가 Vanilla가 아닌 VeryHard 기본값으로 초기화됨; 원인은 IsApplyingPreset 중첩 미지원(ApplyVeryHard() 내부에서 플래그가 false로 리셋되어 SettingChanged → ApplyUser() 조기 호출); bool → 깊이 카운터로 교체 및 ApplyUser()를 ApplyVeryhardWithUserOverlay() 위임 방식으로 수정
- ✅new1 : 버서커 레벨별 Config 개별화 — 분노 쿨타임(Lv1:45초→Lv5:35초), 지속시간(Lv1:20초→Lv5:25초), 패시브 최대 HP 보너스(Lv1:40→Lv5:120 플랫), 잃은 HP 1%당 공격력 증가(Lv1:1.5%→Lv5:2.0%)를 각 레벨별 독립 Config 항목으로 분리; F1 컨피그에서 레벨별 수치 개별 조정 가능
- ✅improve1 : 버서커 — Lv3 분노 피해 감소, Lv4 저체력 공격 보너스, Lv5 패시브 강화(쿨감소+무적보너스) 제거; 패시브 툴팁 및 7개 언어 문자열 정리 (레벨별 분노 쿨타임/지속시간 개선으로 대체)
- ✅improve2 : F1 컨피그 매니저 — 새 버서커 레벨별 Config 20개 키(Lv1~5 쿨타임/지속시간/HP보너스/HP당공격력)에 DE·RU·CN·JA·PT-BR 번역 추가; 기존엔 비KO/EN 플레이어에게 raw 키 이름 노출됨
- ✅improve3 : 검 전문가 — 회오리베기(H키) HUD 슬롯 아이콘을 일반 공격 아이콘에서 검 전문가 아이콘(sword_unlock)으로 변경

# [1.23.36] - 2026-05-16
- ✅fix1 : Double jump now correctly matches total jump height including item enchant bonuses — lowered Harmony priority (VeryLow) so Producer enchant applies to m_jumpForce before double jump reads it; also added Jump skill factor multiplier (1 + factor * 0.4f)
- ✅fix2 : Berserker passive (Death Defiance) — added ActiveSkillCooldownRegistry as secondary cooldown guard in CheckBerserkerPassiveSkill and IsPassiveInvincibilityOnCooldown; preserved CooldownEndTime in SafeCleanup — prevents re-activation when passiveStates is cleared while HUD still shows remaining cooldown
- ✅new1 : Piercing Spear — added passive: primary attack (left-click) animation replaced with knife stab motion when skill is learned; uses Attack.Start Prefix/Postfix to temporarily swap m_attackAnimation before zanim.SetTrigger fires
- ✅fix3 : Piercing Spear passive — all 3 dagger chain animations (knife_stab0→1→2→0) now cycle correctly on consecutive left-clicks; previously only the first motion played due to zeroed m_attackChainLevels; fixed with manual per-player chain counter
-
- ✅fix1 : 이중점프가 아이템 인챈트 보너스를 포함한 실제 점프력과 동일하게 수정 — Harmony 우선순위를 VeryLow로 낮춰 Producer 인챈트가 먼저 m_jumpForce에 적용된 후 이중점프가 읽도록 수정; 점프 스킬 레벨 배율(1 + factor * 0.4f)도 함께 반영
- ✅fix2 : 버서커 패시브(죽음의 무시) — CheckBerserkerPassiveSkill 및 IsPassiveInvincibilityOnCooldown에 ActiveSkillCooldownRegistry 이중 쿨타임 방어 추가; SafeCleanup에서 CooldownEndTime 보존 — passiveStates가 클리어되어 HUD에는 쿨타임이 남아있는데 패시브가 재발동되던 버그 수정
- ✅new1 : 꿰뚫는 창 — 패시브 추가: 스킬 습득 시 좌클릭 공격 모션이 단검 1차 공격 모션으로 교체됨; Attack.Start Prefix에서 m_attackAnimation을 임시 교체 후 zanim.SetTrigger 호출
- ✅fix3 : 꿰뚫는 창 패시브 — 좌클릭 연속 공격 시 단검 3가지 모션(knife_stab0→1→2→0)이 순환 재생되도록 수정; m_attackChainLevels zeroing으로 인해 첫 번째 모션만 반복되던 버그를 수동 체인 카운터로 해결

# [1.23.29] - 2026-05-15
- ✅fix1 : Late-joining clients now automatically receive server Config sync 2 seconds after connecting (ZNet.RPC_PeerInfo patch → BroadcastConfigToClients per peer)
- ✅fix2 : Client-side Config changes are now blocked when server Config is active — a warning log is emitted and GetEffectiveValue() enforces server values
- ✅fix3 : Double jump height now matches normal jump — replaced hardcoded 12f force and skill-level multiplier with character.m_jumpForce
-
- ✅fix1 : 나중에 접속한 클라이언트도 접속 2초 후 서버 Config를 자동으로 수신하도록 수정 (ZNet.RPC_PeerInfo 패치 → 개별 peer에 BroadcastConfigToClients 전송)
- ✅fix2 : 서버 Config 수신 후 클라이언트 로컬 Config 변경 시 경고 로그 출력 및 차단 — GetEffectiveValue()가 서버 값을 강제 적용
- ✅fix3 : 이중점프 높이를 일반 점프와 동일하게 조정 — 하드코딩 12f 및 점프 스킬 레벨 배율 제거, character.m_jumpForce 사용

# [1.23.16] - 2026-05-14
- ✅fix1 : Block Training (defense_Step3_shield) — added max range check (8m, configurable) to prevent counter-attack from triggering when distant monsters stagger
- ✅fix2 : Block Training — removed real-time target position tracking in charge coroutine; player now dashes to fixed position instead of being teleported to monster's current location
- ✅fix3 : Block Training — skip counter-attack when Polearm Whirlwind is active; Harmony Postfix runs even when a Prefix returns false (stagger suppressed), which was causing teleport during whirlwind + block
-
- ✅fix1 : 막기훈련 (defense_Step3_shield) — 원거리 몬스터 스태거 시 반격 오발동 방지를 위한 최대 거리 검사 추가 (기본값 8m, Config 조정 가능)
- ✅fix2 : 막기훈련 — 반격 코루틴 내 실시간 타겟 위치 추적 제거; 반격 시작 시점 위치로 고정 이동 (블록/패링 시 몬스터 위치로 텔레포트되던 버그 수정)
- ✅fix3 : 막기훈련 — 휠윈드 활성 중 반격 발동 차단; Harmony Prefix가 return false로 스태거를 억제해도 Postfix는 실행되는 특성으로 인해 휠윈드+막기 조합 시 텔레포트되던 버그 수정

# [1.23.03] - 2026-05-12
- ✅fix1 : Fixed Harmony.PatchAll() crash from v1.23.01 — ZRoutedRpc is not a MonoBehaviour and has no Awake() method. Removed ZRoutedRpc_Awake_Patch and moved InitializeServerSync() back to ZNet.Awake postfix (ZRoutedRpc instance is guaranteed there since it is created inside ZNet.Awake).
- ✅fix2 : Fixed nerve enhancement passive dodge chance not applying on game load — replaced Player.Awake patch (fires before m_localPlayer is set) with Player.Load patch (fires after data is loaded)
- ✅improve1 : Replaced evasion slide motion (m_pushForce knockback) with upper-body bone lean — Chest rotates 25° sideways away from attacker, Spine 12.5°, feet stay planted, restores over 0.6 seconds
-
- ✅fix1 : v1.23.01에서 추가한 ZRoutedRpc.Awake 패치로 인한 Harmony.PatchAll() 크래시 수정. ZRoutedRpc는 MonoBehaviour가 아니라 Awake() 메서드가 없음. ZRoutedRpc_Awake_Patch 제거 후 InitializeServerSync()를 ZNet.Awake postfix로 복귀 (ZNet.Awake 내부에서 ZRoutedRpc가 생성되므로 해당 시점에 instance 보장).
- ✅fix2 : 신경강화 패시브 회피율 게임 로드 시 미적용 버그 수정 — Player.Awake 패치(m_localPlayer가 null인 시점)를 Player.Load 패치로 변경, 데이터 로드 완료 후 회피율 정상 초기화됨
- ✅improve1 : 회피 슬라이드 모션을 상체 본 기울기 애니메이션으로 교체 — m_pushForce(넉백 방식) 제거, 회피 성공 시 Chest 본 25°·Spine 12.5° 공격자 반대 방향으로 기울었다가 0.6초 후 복원 (발 고정)

# [1.23.01] - 2026-05-11
- ✅fix1 : Job class title prefix (e.g. [Archer], [Mage]) now displays correctly on overhead name after job change and on respawn
- ✅fix2 : Fixed skillconfig sync not applying on dedicated servers (RPC handler now registered in ZRoutedRpc.Awake instead of ZNet.Awake)
- ✅new1 : Added evasion slide motion — character dashes away from attacker on successful dodge
- ✅fix3 : Fixed Nerve Enhancement cooldown triggering even when another skill caused the dodge
-
- ✅fix1 : 전직 후 캐릭터 이름 앞 직업명(예: [Archer], [Mage]) 색상 표시가 전직 직후 및 재접속 후에도 정상 표시되도록 수정
- ✅fix2 : skillconfig sync 전용 서버 미작동 수정 (ZNet.Awake → ZRoutedRpc.Awake로 RPC 핸들러 등록 시점 변경)
- ✅new1 : 공격 회피 성공 시 슬라이드 모션 추가 — 공격자 반대 방향으로 빠르게 이동
- ✅fix3 : 신경강화 쿨타임 버그 수정 — 다른 회피 스킬 발동 시에도 신경강화 쿨타임이 걸리던 문제 수정

# [1.22.08] - 2026-05-09
- ✅new1 : Replaced Sword Expert H-key skill: Parry Rush → Whirlwind Slash (3-hit auto AoE chain on H-key press, radius 5/8/12m, damage 140/180/220% per hit, cooldown 40s, each hit staggers targets)
-
- ✅new1 : 검 전문가 H키 스킬 교체: 패링 돌격 → 회오리베기 (H키 1회 누르면 1→2→3차 자동 연속 AoE, 반경 5/8/12m, 공격력 140/180/220%, 쿨타임 40초, 각 공격마다 스태거)


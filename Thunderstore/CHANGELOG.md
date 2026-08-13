# Changelog / 변경 로그

## 2026-08-11 (v2.1.90)

### English
- [QuestManager.cs, QuestBossTrophyPatch.cs (removed), QuestKillPatch.cs, QuestPanelUI.cs, QuestIconPatch.cs] Boss quest rewards no longer require holding the boss's trophies — kill count alone is enough to claim, and trophies are no longer consumed.
- [Quest_Config.cs] Added coin rewards to hidden special boss quests — Meadows 500, Black Forest 800, Swamp 1200.
- [Quest_Config.cs, QuestDefinition.cs, QuestManager.cs, QuestPanelUI.cs] Added minimum player level requirements to biome boss quests — Meadows 10 through Ashlands 70.
- [ConfigMigration.cs] Fixed special-quest coin rewards not showing up in-game due to a stale saved config value.

### 한국어
- [QuestManager.cs, QuestBossTrophyPatch.cs(삭제), QuestKillPatch.cs, QuestPanelUI.cs, QuestIconPatch.cs] 보스 퀘스트 보상 수령 시 트로피 보유 요구조건 제거 — 처치 횟수만으로 수령 가능, 트로피 소모 안 함.
- [Quest_Config.cs] 특수(히든) 보스 퀘스트에 코인 보상 추가 — 목초지 500, 검은숲 800, 늪 1200.
- [Quest_Config.cs, QuestDefinition.cs, QuestManager.cs, QuestPanelUI.cs] 바이옴별 보스 퀘스트에 최소 플레이어 레벨 조건 추가 — 목초지 10 ~ 잿빛땅 70.
- [ConfigMigration.cs] 특수 퀘스트 코인 보상이 저장된 옛 Config 값 때문에 인게임에 표시되지 않던 버그 수정.

# \[2.1.61] - 2026-08-09
## Files Modified / 수정 파일
`Mace_Active.cs` · `SkillEffect.PolearmTree.cs`

* ✅fix1 : [Mace_Active.cs] Fixed the Mace expert's Shield Charge ("Guardian's Heart") skill's "multi-hit" (periodic 3m-radius area damage tick every 0.08s during the charge) never firing at all — the tooltip and config both documented and controlled this multi-hit, but the actual call to trigger it had been accidentally deleted during a previous refactor that added the enemy-gathering + finish-hit mechanic. Restored the call so the multi-hit now fires correctly on every VFX tick during the charge.
* ✅fix2 : [SkillEffect.PolearmTree.cs] Fixed the Polearm expert's "Pierce Charge" skill's monster knockback — the struck monster (and AOE-affected monsters) were teleported instantly with zero ground/obstacle check, causing them to sink into terrain on uneven ground or pass straight through rocks/walls. Knockback destinations are now clamped against nearby obstacles (stopping just short of a wall/rock) and snapped to the actual ground height.
*
* ✅fix1 : [Mace_Active.cs] 둔기 전문가 방패돌진(수호자의 진심) 스킬의 "다단히트"(돌진 중 0.08초 간격으로 3m 반경 광역 추가 타격)가 전혀 발동하지 않던 버그 수정 — 툴팁·config에는 다단히트가 명시되어 있었지만, 이전 리팩터링(끌어모으기+최종타 기능 추가) 과정에서 실제 호출 코드가 삭제된 채 남아있었습니다. 돌진 중 VFX 틱마다 다단히트가 정상적으로 발동하도록 복원했습니다.
* ✅fix2 : [SkillEffect.PolearmTree.cs] 폴암 전문가의 "관통 돌격" 스킬의 몬스터 넉백 버그 수정 — 적중당한 몬스터(및 AOE로 밀려나는 몬스터들)가 지면/장애물 검사 없이 즉시 순간이동되어, 지형이 고르지 않은 곳에서 땅속에 파묻히거나 바위·벽을 그대로 통과하는 문제가 있었습니다. 이제 넉백 목적지가 주변 장애물에 막히면 그 앞에서 멈추고, 최종 위치는 실제 지면 높이로 보정됩니다.

# \[2.1.59] - 2026-08-07
## Files Modified / 수정 파일
`SkillEffect.PolearmTree.cs` · `Sword_Skill.cs` · `Berserker_Skill.cs`

* ✅fix1 : [SkillEffect.PolearmTree.cs] Fixed the Polearm expert's "Pierce Charge" skill sometimes getting permanently stuck showing "Pierce Charge in progress" and refusing to activate again — the execution coroutine had no guaranteed cleanup, so if it was interrupted mid-dash (e.g. by an exception in the hit/knockback logic) the in-progress flag was never reset. The coroutine now uses try/finally so the flag (and the temporary 8x attack-speed boost) is always restored no matter how the coroutine ends.
* ✅fix2 : [Sword_Skill.cs] Applied the same fix to the Sword's "Rush Slash" combo skill, which had the identical structural risk (in-progress flag reset only on specific early-exit branches, not guaranteed on unexpected interruption).
* ✅fix3 : [Berserker_Skill.cs] Fixed the Berserker's "Death Defiance" passive's post-invincibility skill-loss protection window being anchored to when the invincibility *started* instead of when it *ended* — with the default 8-second invincibility duration, this left only ~2 seconds of real protection after the shield wore off, making the intended "skills won't drop if you die shortly after Death Defiance ends" grace period barely noticeable in practice. The window is now correctly measured from when invincibility ends. Also fixed a related bug where the death-cleanup routine was wiping the protection timer immediately after any death, so a second death within the same grace window (e.g. during a wipe) was never protected.
*
* ✅fix1 : [SkillEffect.PolearmTree.cs] 폴암 전문가의 "관통 돌격" 스킬이 가끔 "관통 돌격 실행 중"이라는 메시지가 뜬 채로 영구히 멈춰 재사용이 안 되던 문제 수정 — 실행 코루틴에 확실한 정리(cleanup) 보장이 없어서, 돌진 도중 예외 등으로 중단되면 '실행 중' 플래그가 리셋되지 않았습니다. 이제 코루틴이 try/finally를 사용해 어떤 방식으로 종료되든 플래그(및 임시 공격속도 8배 부스트)가 항상 원복됩니다.
* ✅fix2 : [Sword_Skill.cs] 검의 "돌진연속베기" 콤보 스킬에도 동일한 구조적 위험(특정 조기 종료 분기에서만 플래그 리셋, 예기치 못한 중단 시 보장 안 됨)이 있어 동일하게 수정했습니다.
* ✅fix3 : [Berserker_Skill.cs] 버서커 "죽음의 무시" 패시브의 무적 종료 후 스킬 하락 방지 유예시간이 무적이 "시작된" 시점 기준으로 계산되던 문제 수정 — 기본 무적 지속시간(8초) 기준으로는 무적이 끝난 뒤 실제 보호시간이 약 2초밖에 안 되어, "무시 종료 직후에 죽어도 스킬이 안 깎이는" 의도된 유예시간이 체감상 거의 없다시피 했습니다. 이제 무적이 "종료된" 시점 기준으로 정확히 계산됩니다. 또한 사망 직후 정리 로직이 보호 타이머를 즉시 지워버려, 같은 유예시간 안에 두 번째로 죽는 경우(예: 파티 전멸) 보호가 적용 안 되던 연관 버그도 함께 수정했습니다.

# \[2.1.57] - 2026-08-06
## Files Modified / 수정 파일
`JobSkills.cs` · `SkillEffect.StaffAreaHeal.cs` · `ConfigMigration.cs` · `SkillTreeConfig.cs` · `SkillTreeTooltip.cs` · `ActiveSkillHUD.cs` · `CrossbowOneShot_Tooltip.cs` · `CrossbowIceBreath_Tooltip.cs` · `BowExplosive_Tooltip.cs` · `KnifeAssassinHeart_Tooltip.cs` · `MindShield_Tooltip.cs` · `Staff_Tooltip.cs` · `Sword_Tooltip.cs` · `EpicMMOCritIntegration.cs` · `Plugin.Patches.cs` · `Archer_Config.cs` · `ArcherSkills.cs` · `SkillEffect.cs` · `Archer_Tooltip.cs` · `DefaultLanguages_WeaponSkills.cs` · `DefaultLanguages_JobExpert_EN.cs` · `de.json` · `ja.json` · `pt_BR.json` · `zh-cn.json` · `ru.json` · `ConfigTranslations_JobDesc.cs` · `ConfigTranslations_JobDesc_DE.cs` · `ConfigTranslations_JobDesc_CN.cs` · `ConfigTranslations_JobDesc_JP.cs` · `ConfigTranslations_JobDesc_PTBR.cs` · `ConfigTranslations_JobDesc_RU.cs` · `ConfigTranslations_KeyNames_KO.cs` · `ConfigTranslations_KeyNames_EN.cs` · `ConfigTranslations_KeyNames_CN_Part2.cs` · `ConfigTranslations_KeyNames_DE_Part2.cs` · `ConfigTranslations_KeyNames_JP_Part2.cs` · `ConfigTranslations_KeyNames_PTBR_2.cs` · `ConfigTranslations_KeyNames_RU.cs` · `Veryhard_CaptainSkillTree.SkillTreeMod.cfg` · `Vanilra_Config_CaptainSkillTree.SkillTreeMod.cfg`

* ✅fix1 : [JobSkills.cs] Fixed the Paladin's Y-key heal skill leaving lingering/stacking buff_03a_aura glow on characters after the skill visually ended — the ally heal coroutine was spawning the aura VFX twice (6 instances instead of the intended 3), and firing the same VFX 3 times in a row triggered a duplicate-prevention key collision in the networked VFX system, causing extra delayed-position copies to appear and disappear late. Removed the duplicate spawn call and switched the 3-point aura (caster and allies) to a single networked VFX call plus two local visual clones, eliminating both the double-spawn and the delayed duplicates.
* ✅fix2 : [SkillEffect.StaffAreaHeal.cs] Applied the same fix to the Staff's instant area-heal skill, which had the identical triple-call pattern causing the same lingering-glow duplication on healed allies.
* ✅fix3 : [ConfigMigration.cs, SkillTreeConfig.cs] Removed the config schema-migration "version change detected" / "migration complete" warning-level log messages from the game log — the migration itself still runs internally exactly as before (still silently resets the small set of balance-affected keys when the schema version changes), only the now-unnecessary log output and the item-counting code that existed solely to build that log message were removed.
* ✅fix4 : [EpicMMOCritIntegration.cs] Fixed the EpicMMO Strength/Special crit-damage and crit-chance "base value" configs (Mmo_Strength_CritDamageBase, Mmo_Special_CritChanceBase) never being applied while the player had 0 points invested in the matching EpicMMO stat — the integration function returned 0 before ever adding the base value, so a configured base bonus (e.g. 50% crit damage) silently did nothing until at least 1 point was invested in that stat. The base value is now always added regardless of invested points.
* ✅fix5 : [Plugin.Patches.cs] Fixed successful critical hits on any weapon other than Daggers/Unarmed showing zero visible feedback whenever the computed crit-damage bonus happened to be 0% (e.g. before investing in a weapon-specific crit-damage skill) — the crit VFX only played for Daggers/Unarmed, so a successful crit roll on Swords/Clubs/Spears/Polearms/Staves/Bows/Crossbows looked identical to a normal hit, making the Attack tree's "Deadly Strike" crit-chance skill feel like it wasn't working. The crit VFX now plays for every weapon type on a successful crit roll.
* ✅improve1 : [SkillTreeConfig.cs + 9 files] Changed the active skill hotkey bindings (Y/R/G/H) in the F1 config menu from a text/dropdown key-name selector to BepInEx's native "press a key to bind" KeyboardShortcut control — players can now click the hotkey field and simply press the desired key instead of picking a name from a list. Existing saved key bindings carry over automatically (no reset needed). Updated all tooltip/HUD code that displays the currently bound key to read from the new binding type.
* ✅new1 : [Archer_Config.cs, ArcherSkills.cs, SkillEffect.cs, Archer_Tooltip.cs, DefaultLanguages_WeaponSkills.cs, DefaultLanguages_JobExpert_EN.cs, de.json, ja.json, pt_BR.json, zh-cn.json, ru.json, ConfigTranslations_JobDesc.cs + 5 lang files, ConfigTranslations_KeyNames_KO/EN.cs + 5 lang files, Veryhard/Vanilla cfg] Added a new always-on Archer job passive: while holding Archer job Level 1+, nearby tamed creatures (wolves, boars, Lox, etc. within a configurable radius, default 10m) are healed for (Archer Level x a configurable per-level amount, default 1) HP every second — e.g. 1 HP/s at Lv1 scaling up to 5 HP/s at Lv5. Shown in the existing Archer passive tooltip line and fully localized across all 7 supported languages.
*
* ✅fix1 : [JobSkills.cs] 성기사 Y키 힐 스킬 사용 후 캐릭터에 buff_03a_aura 글로우가 남거나 쌓이는 것처럼 보이던 문제 수정 — 아군 지속힐 코루틴이 오라 VFX를 의도한 3개 대신 6개 중복 생성했고, 동일 VFX를 3번 연속 호출할 때 네트워크 VFX 시스템의 중복방지 키가 충돌해 지연된 위치에 추가 복제본이 늦게 생성되고 늦게 사라지던 것이 원인. 중복 호출을 제거하고, 3점 오라(시전자·아군)를 네트워크 VFX 1회 + 로컬 복제 2회 방식으로 바꿔서 이중 생성과 지연 복제 문제를 모두 해결.
* ✅fix2 : [SkillEffect.StaffAreaHeal.cs] 지팡이 즉시 범위 힐 스킬에도 동일한 3연속 호출 패턴으로 인해 같은 잔상 글로우 문제가 있어 동일하게 수정.
* ✅fix3 : [ConfigMigration.cs, SkillTreeConfig.cs] Config 스키마 마이그레이션의 "버전 변경 감지"/"마이그레이션 완료" 경고 로그 메시지를 게임 로그에서 제거 — 마이그레이션 자체는 기존과 동일하게 내부적으로만 동작하며(스키마 버전이 바뀌면 밸런스 관련 키만 조용히 기본값으로 리셋), 해당 로그를 위해서만 존재하던 항목 카운팅 코드도 함께 정리.
* ✅fix4 : [EpicMMOCritIntegration.cs] EpicMMO Strength/Special 치명타 피해·확률 "기본값" Config(Mmo_Strength_CritDamageBase, Mmo_Special_CritChanceBase)가 해당 EpicMMO 스탯에 포인트를 0 투자한 상태에서는 전혀 적용되지 않던 문제 수정 — 통합 함수가 기본값을 더하기도 전에 0을 반환해, 설정한 기본 보너스(예: 치명타 피해 50%)가 해당 스탯에 최소 1포인트 이상 투자하기 전까지는 조용히 무시됐습니다. 이제 스탯 포인트 투자 여부와 무관하게 기본값이 항상 적용됩니다.
* ✅fix5 : [Plugin.Patches.cs] 단검/맨주먹을 제외한 무기(검/둔기/창/폴암/지팡이/활/석궁)에서 치명타 피해 배수가 0%로 계산될 때(예: 무기 전용 치명타 피해 스킬을 아직 안 배운 경우) 확률 판정이 내부적으로 성공해도 VFX가 전혀 뜨지 않아 일반 타격과 구분이 안 되던 문제 수정 — 공격 트리의 "치명적인 공격"(치명타 확률) 스킬을 배워도 크리티컬이 발생하지 않는 것처럼 체감되던 원인이었습니다. 이제 크리티컬 판정 성공 시 모든 무기에서 크리티컬 VFX가 재생됩니다.
* ✅improve1 : [SkillTreeConfig.cs 외 9개 파일] F1 설정창에서 액티브 스킬 핫키(Y/R/G/H) 바인딩 방식을 텍스트/드롭다운 선택에서 BepInEx 네이티브 "키를 누르면 바로 등록"되는 KeyboardShortcut 방식으로 변경 — 목록에서 고르는 대신 필드를 클릭하고 원하는 키를 누르면 됩니다. 기존에 저장된 키 설정은 그대로 유지되며(초기화 불필요), 현재 바인딩된 키를 표시하는 모든 툴팁/HUD 코드도 새 타입에 맞게 함께 업데이트.
* ✅new1 : [Archer_Config.cs, ArcherSkills.cs, SkillEffect.cs, Archer_Tooltip.cs, DefaultLanguages_WeaponSkills.cs, DefaultLanguages_JobExpert_EN.cs, de.json, ja.json, pt_BR.json, zh-cn.json, ru.json, ConfigTranslations_JobDesc.cs 외 5개 언어 파일, ConfigTranslations_KeyNames_KO/EN.cs 외 5개 언어 파일, Veryhard/Vanilla cfg] 아처 직업 신규 상시 패시브 추가 — 아처 직업 레벨 1 이상을 보유하면 주변(기본 반경 10m, 설정 가능)의 길들인 생물(늑대, 멧돼지, 로스트바이킹 등)을 매초 (아처 레벨 × 레벨당 회복량, 기본 1) hp만큼 자동으로 회복시킵니다 — 예: Lv1 초당 1hp, Lv5 초당 5hp. 기존 아처 패시브 툴팁 줄에 함께 표시되며 지원 언어 7개 전부 번역 완료.

# \[2.1.50] - 2026-08-05
## Files Modified / 수정 파일
`ProducerEnchantData.cs` · `ProducerCrafting.cs` · `QuestPanelUI.cs` · `Quest_Config.cs` · `QuestManager.cs` · `QuestModerMeleeOnlyTracker.cs` · `QuestModerRangedOnlyTracker.cs` · `QuestKillPatch.cs` · `QuestRewardSpawner.cs` · `SkillEffect.SpeedTree2.cs`

* ✅fix1 : [ProducerEnchantData.cs] Fixed the Producer job's crafting enchant ("Crafting Blessing") failing to apply to virtually all melee weapons (sword/axe/mace/spear/polearm/dagger/staff) — a per-weapon-type enchant pool added in a previous update had no matching entry in already-generated on-disk config files, so newly crafted melee weapons silently received no enchant at all (no icon, no tooltip line). The mod now auto-detects and merges any enchant types/slot pools missing from an existing on-disk Producer_Enchant.json against the bundled defaults on load, without touching any values the player has customized.
* ✅fix2 : [ProducerCrafting.cs] Fixed a related silent-failure bug where a failed enchant application (e.g. from the missing-pool issue above) was still marked as "success" internally, skipping even the fallback durability VFX/feedback and leaving no trace in the logs to diagnose the issue.
* ✅fix3 : [QuestPanelUI.cs] Fixed boss trophy quest status text (e.g. "Kills 0/5 · Trophies 0/5") overflowing past its box into the reward list column in the Quest window, making the two overlap and become unreadable — the status text box for boss quests is now wide enough to fit the full counter string without overlapping the reward preview.
* ✅fix4 : [QuestPanelUI.cs] Fixed an already-claimed boss trophy quest (Eikthyr, Elder, Bonemass, Moder, Yagluth, Seeker Queen, Fader) reverting back to an active progress counter if the player later carried a matching trophy in their inventory again — a claimed quest now always stays shown as claimed regardless of the player's current trophy count.
* ✅new1 : [Quest_Config.cs, QuestModerMeleeOnlyTracker.cs, QuestModerRangedOnlyTracker.cs, QuestKillPatch.cs, QuestRewardSpawner.cs, QuestPanelUI.cs, SkillEffect.SpeedTree2.cs] Added two hidden achievement quests to the Mountain biome: defeat Moder using melee weapons only, or using ranged weapons/magic only. Claiming either grants a permanent +5 bonus to the matching weapon skills (melee: Swords/Clubs/Knives/Spears/Polearms/Axes, ranged: Bows/Crossbows), applied regardless of which weapon is currently equipped.
* ✅improve1 : [Quest_Config.cs] Rebalanced the Mountain biome's four existing quests (Stone Golem, Wolf, Fenring, Moder) — removed the magic orb reward from all four, switched coin rewards from a random range to a fixed amount, and increased Moder's silver ore and coin rewards.
*
* ✅fix1 : [ProducerEnchantData.cs] 제작 전문가의 제작 축복(마법부여)이 검/도끼/둔기/창/폴암/단검/지팡이 등 대부분의 근접무기에 전혀 적용되지 않던 문제 수정 — 이전 업데이트에서 무기군별로 세분화된 인챈트 풀이 추가됐지만, 이미 생성되어 있던 온디스크 설정 파일에는 해당 항목이 없어 신규 제작한 근접무기가 아무 마법부여도 받지 못했습니다(아이콘도, 효과 라인도 전혀 표시되지 않음). 이제 게임 시작 시 온디스크 Producer_Enchant.json에 내장 기본값 대비 누락된 인챈트 타입/슬롯 풀이 있으면 자동으로 감지해 병합하며, 플레이어가 직접 수정한 수치는 건드리지 않습니다.
* ✅fix2 : [ProducerCrafting.cs] 위 문제로 마법부여 적용이 실패해도 내부적으로는 "성공"으로 처리되어, 내구도 전용 VFX 폴백조차 실행되지 않고 로그에도 아무 흔적이 남지 않던 관련 버그 수정.
* ✅fix3 : [QuestPanelUI.cs] 퀘스트 창에서 보스 트로피 퀘스트의 상태 텍스트(예: "처치 0/5 · 트로피 0/5")가 박스 폭을 넘어 오른쪽 보상 목록 칸까지 겹쳐 보이던 문제 수정 — 보스 퀘스트의 상태 텍스트 칸 폭을 넓혀 전체 문구가 보상 목록과 겹치지 않고 표시됩니다.
* ✅fix4 : [QuestPanelUI.cs] 이미 수령완료한 보스 트로피 퀘스트(에이크쉬르, 엘더, 본메스, 모더, 야글루스, 시커퀸, 페이더)가 이후 해당 트로피를 인벤토리에 다시 넣으면 진행 중 카운터로 되돌아가던 문제 수정 — 한 번 수령완료된 퀘스트는 이후 트로피 보유 개수와 무관하게 항상 수령완료 상태로 표시됩니다.
* ✅new1 : [Quest_Config.cs, QuestModerMeleeOnlyTracker.cs, QuestModerRangedOnlyTracker.cs, QuestKillPatch.cs, QuestRewardSpawner.cs, QuestPanelUI.cs, SkillEffect.SpeedTree2.cs] 산 지역에 히든 업적 퀘스트 2종 추가 — 근접 무기만으로 모더 처치, 원거리(활/석궁/마법) 무기만으로 모더 처치. 수령 시 해당 무기 계열 스킬(근접: 검/둔기/단검/창/폴암/도끼, 원거리: 활/석궁)에 영구 +5 보너스가 적용되며, 현재 장착한 무기와 무관하게 항상 적용됩니다.
* ✅improve1 : [Quest_Config.cs] 산 지역 기존 퀘스트 4종(스톤골렘/늑대/펜링/모더) 보상 밸런스 조정 — 마법 오브 보상 전부 제거, 코인 보상을 범위 대신 고정값으로 변경, 모더의 은 원석·코인 보상 증가.

# \[2.1.46] - 2026-08-04
## Files Modified / 수정 파일
`SkillEffect.KnifeSkillEffects.cs` · `SkillTreeConfig.cs`

* ✅fix1 : [SkillEffect.KnifeSkillEffects.cs] Fixed the Dagger expert's "Assassin's Heart" skill sometimes dropping the player through the dungeon floor and out of the dungeon — when the dash's landing point behind the target had no floor beneath it (a nearby pit, stairwell, or room edge), the skill teleported there anyway and let gravity take over, causing a long fall that ended outside the dungeon. The dash is now cancelled (with a "teleport failed" message, no stamina/cooldown spent) if no solid ground is detected at the landing spot.
* ✅fix2 : [SkillTreeConfig.cs] Added "Mouse2" (mouse wheel/middle click) to the list of allowed keys for skill hotkey rebinding (F1 Config Manager) — it was missing even though the side mouse buttons (Mouse3/Mouse4) were already supported.
*
* ✅fix1 : [SkillEffect.KnifeSkillEffects.cs] 단검 전문가 "암살자의 심장" 스킬 사용 시 착지 지점에 바닥이 없으면(구덩이, 계단통, 방 경계 근처) 그대로 이동해 중력으로 낙하하다 던전 밖으로 튕겨나가던 문제 수정 — 이제 착지 지점에 바닥이 감지되지 않으면 돌격을 취소하고 "이동 실패" 메시지를 띄우며, 스태미나/쿨타임도 소모되지 않습니다.
* ✅fix2 : [SkillTreeConfig.cs] 스킬 단축키 변경(F1 컨피그 매니저) 허용 키 목록에 마우스 휠 버튼(Mouse2) 추가 — 사이드 마우스 버튼(Mouse3/Mouse4)는 이미 지원되고 있었지만 휠 버튼만 빠져있었습니다.

# \[2.1.44] - 2026-08-03

* ✅fix1 : Fixed the Mace expert's Tier 4 "Concussion" skill trigger — it now procs off your own mace attack landing on an enemy (not off being hit), and slows the target's attack speed in addition to movement speed
* ✅fix2 : Fixed the Polearm "Pierce Charge" skill letting you dash straight through dungeon walls/objects and out of the dungeon — it now stops short of obstacles and cancels near dungeon exit triggers, matching how every other dash skill (Shield Charge, Spear Rush, Mace Charge, Whirlwind) already behaves
* ✅fix3 : Fixed the Paladin job tooltip displaying the wrong active-skill keybind (G) — it now correctly shows Y
* ✅fix4 : Fixed boss trophy quests (Eikthyr, Elder, Bonemass, Moder, Yagluth, Seeker Queen, Fader) not counting kills at all, even solo — trophy counting silently stopped once the game's World Level advanced past what was stamped on trophies you already held. Kill count and trophy count are now tracked and shown separately (e.g. "Kills 3/5 · Trophies 2/5"), and both are required to turn the quest in
* ✅fix5 : Fixed the Mage expert's "Double Cast" on-screen prompt telling you to press the wrong key (R) to launch the summoned fireballs — it now correctly says Z, matching the default keybind
* ✅fix6 : Fixed the Dagger expert's "Assassin's Heart" skill being able to target and dash to a monster through a dungeon wall, pulling you outside the dungeon — targeting now skips monsters with no clear line of sight, and the dash itself now stops short of obstacles/dungeon exit triggers instead of teleporting through them
* ✅fix7 : Fixed the Mage dungeon-buff's Damage Bonus/Duration settings being missing from the Very Hard difficulty preset — they now sync with the preset like every other Mage setting
* ✅fix8 : Fixed the dungeon-buff cast for the Mage's "Fire Rain" and the Bow's "Arrow Rain" playing no sound at all — both now play an activation click when the buff triggers
* ✅improve1 : Reduced the Spear expert's "Piercing Spear" (G-key) skill cooldown from 60s to 35s
* ✅improve2 : Renamed the Mage expert's "Double Cast" skill to "Multi Cast" (name only, no gameplay change) — updated everywhere: skill name, tooltip, on-screen messages, and config descriptions, across all 7 languages
* ✅improve3 : Tripled the volume of the Bow expert's "Explosive Arrow" cast sound and the Mage's "Multi Cast" activation sound — both were too quiet to notice
* ✅improve4 : Lowered the quest-completion jingle's default volume to 70% (it was playing at 180%) and added a "QuestCompleteMusicVolume" slider (0-100%) to the F1 Config Manager's Quest System category
* ✅new1 : The Mage job's Y-key skill ("Fire Rain") now works properly inside dungeons — since fireballs falling from the sky don't make sense indoors, using it inside a dungeon instead casts a self-buff for +25% attack damage for 10 seconds (both values configurable). The tooltip has a new "Special" line explaining this
* ✅new2 : The Bow expert's H-key skill ("Arrow Rain") now also works properly inside dungeons, for the same reason as Fire Rain — since arrows can't rain from 200m overhead indoors, using it in a dungeon instead casts a self-buff for +25% attack damage for 10 seconds (both values configurable). The tooltip has a new "Special" line explaining this
* ✅fix9 : Fixed monster kills finished by a damage-over-time tick (poison/burn) not counting toward "Kill" quest progress or the built-in EXP system — the killing tick carries no attacker info, so quest counters and EXP could silently fail to increase even though the player dealt the fatal damage
* ✅fix10 : Fixed the Attack Expert's "Bow: Hunter's Eye" node — its tooltip promised +crit chance on top of its guaranteed first-arrow crit, but that bonus was only ever added in the tooltip's preview math, never in actual combat rolls, so it did nothing in real fights. Replaced it with a working +8% critical damage bonus that actually applies (stacks with the existing guaranteed-crit effect)
* ✅fix11 : Fixed the Attack Expert's "Opener" node (Tier 1) not actually reducing stamina cost — its tooltip promised a stamina discount during the opener window, but nothing in the code ever applied it. It now genuinely reduces stamina cost on attacks while the opener window is active
* ✅fix12 : Fixed the Attack Expert's "Swift Chase" node (Tier 4) doing nothing at all — its tooltip promised +move speed while in combat, but the bonus was never wired into the movement speed system anywhere, so the node was a pure pass-through on the skill tree. It now actually grants the move speed bonus while you're in combat
* ✅fix13 : Fixed the Attack Expert's "Magic: Chaos Initiation" node ignoring its own F1 Config Manager on/off toggle — the guaranteed stagger on your first magic attack in the opener window fired regardless of what the admin setting said. It now actually respects the toggle
* ✅improve5 : Renamed the Attack Expert tree's "Opener" mechanic's Korean display name from slang to a more formal term everywhere (skill name, tooltips, on-screen messages, config descriptions) — Korean text only, no gameplay change and no other language affected
* ✅improve6 : Cleaned up two pieces of dead code left over from a past Attack Expert tree redesign — a commented-out reference to a field that's no longer used, and a check for a "Precision Attack" skill node that no longer exists in the tree (its bonus was already hardcoded to 0, so this changes nothing in practice)
* ✅new3 : The Captain Level System's level-difference EXP reduction is now tiered like the existing damage-reduction option, instead of a flat inverse curve — monsters far above or below your level now pay out EXP at fixed tier rates (11-15 level gap: 30%, 16-20: 25%, 21-30: 20%, 31+: 10%), all configurable in the F1 Config Manager
* ✅new4 : Added a new "Level Diff Drop Suppression" option to the Captain Level System (F1 Config Manager, admin-only, server-synced) — when a monster's level is 16 or more above the killing player's (threshold configurable), it drops no items at all
* ✅fix14 : Aligned the "Level Diff Damage Reduction" option's starting threshold with the monster nameplate's white/red color cutoff and the new tiered EXP reduction — it now kicks in at an 11+ level gap instead of 10+, so a monster exactly 10 levels above you (still shown in white) no longer takes reduced damage from you a level early
* 
* ✅fix1 : 둔기 전문가 Tier4 "뇌진탕" 스킬의 발동 조건 수정 — 이제 피격 시가 아니라 둔기로 적을 공격했을 때 발동하며, 이동속도뿐 아니라 공격속도도 함께 감소시킵니다
* ✅fix2 : 폴암 "관통돌격" 스킬이 던전 벽/오브젝트를 뚫고 던전 밖으로 나가던 문제 수정 — 이제 장애물 앞에서 멈추고 던전 출구 트리거 근처에서는 돌진이 취소됩니다 (방패돌진/창 관통돌진/둔기 돌진/휠윈드와 동일한 방식 적용)
* ✅fix3 : 성기사 직업 툴팁에 액티브 스킬 키가 G로 잘못 표시되던 문제 수정 — Y키로 정정
* ✅fix4 : 보스 트로피 퀘스트(에이크시르/엘더/본메스/모더/야글루스/시커퀸/페이더)가 혼자 잡아도 처치 카운트가 전혀 되지 않던 문제 수정 — 월드 레벨이 오르면 이미 보유 중이던 트로피가 카운트에서 빠지던 버그였습니다. 이제 처치 횟수와 트로피 보유량을 각각 따로 추적/표시하며(예: "처치 3/5 · 트로피 2/5"), 완료 처리에는 둘 다 충족해야 합니다
* ✅fix5 : 지팡이 전문가 "이중시전" 사용 시 화면 중앙에 뜨는 "R키로 발사" 안내 문구가 실제 기본 키(Z)와 다르게 표시되던 문제 수정 — Z키로 정정
* ✅fix6 : 단검 전문가 "암살자의 심장" 스킬이 던전 벽 너머의 몬스터를 타게팅해 돌진하면서 던전 밖으로 끌려나가던 문제 수정 — 이제 시야가 막힌 몬스터는 타게팅 대상에서 제외되며, 돌진 자체도 벽/오브젝트나 던전 출구 트리거를 통과하지 않고 그 앞에서 멈춥니다
* ✅fix7 : 메이지 던전버프의 공격력 보너스/지속시간 설정값이 Veryhard 난이도 프리셋에 누락되어 있던 문제 수정 — 이제 다른 메이지 설정들과 마찬가지로 프리셋에 정상 반영됩니다
* ✅fix8 : 메이지 "불의 비"와 궁수 "화살비"의 던전 버프 발동 시 아무 소리도 나지 않던 문제 수정 — 이제 버프 발동 시 활성화 클릭 사운드가 함께 재생됩니다
* ✅improve1 : 창 전문가 "꿰뚫는 창"(G키) 스킬 쿨타임을 60초 → 35초로 단축
* ✅improve2 : 지팡이 전문가 "이중시전" 스킬명을 "다중시전"으로 변경 (이름만 변경, 효과 변화 없음) — 스킬명/툴팁/화면 메시지/컨피그 설명까지 7개 언어 전부 동기화
* ✅improve3 : 궁수 전문가 "폭발 화살" 시전 사운드와 메이지 "다중시전" 시전 사운드 볼륨을 3배로 키웠습니다 — 기존에는 너무 작아 잘 들리지 않았습니다
* ✅improve4 : 퀘스트 완료 시 재생되는 완료음 기본 볼륨을 70%로 낮췄습니다(기존 180%) — F1 Config Manager의 Quest System 카테고리에 "QuestCompleteMusicVolume"(0\~100%) 슬라이더도 추가했습니다
* ✅new1 : 마법사 직업 Y키 스킬("불의 비")이 던전 안에서도 제대로 작동하도록 개선 — 하늘에서 파이어볼이 떨어지는 연출이 실내에서는 어색하므로, 던전 안에서 사용 시 공격력 +25%를 10초간 부여하는 자버프로 대체 발동됩니다(수치는 Config에서 조절 가능). 툴팁에 이를 설명하는 "특수" 항목이 추가되었습니다
* ✅new2 : 궁수 전문가 H키 스킬("화살비")도 불의 비와 같은 이유로 던전 안에서 제대로 작동하도록 개선 — 200m 상공에서 화살이 떨어지는 연출이 실내에서는 불가능하므로, 던전 안에서 사용 시 공격력 +25%를 10초간 부여하는 자버프로 대체 발동됩니다(수치는 Config에서 조절 가능). 툴팁에 이를 설명하는 "특수" 항목이 추가되었습니다
* ✅fix9 : 독/화상 등 지속 피해(도트)로 몬스터를 처치했을 때 "처치" 퀘스트 진행도와 자체 경험치 시스템이 카운트되지 않던 문제 수정 — 도트 틱으로 마무리된 킬은 공격자 정보가 남지 않아, 실제로 플레이어가 죽인 몬스터인데도 퀘스트 진행도/경험치가 조용히 누락될 수 있었습니다
* ✅fix10 : 공격전문가 "활: 사냥의 눈" 노드 수정 — 툴팁에는 확정 크리티컬 외에 크리확률 보너스도 있다고 표시됐지만, 실제로는 툴팁 미리보기 계산에만 더해질 뿐 실전 크리 판정에는 전혀 적용되지 않던 죽은 스탯이었습니다. 이를 실제로 적용되는 크리티컬 피해 +8% 보너스로 교체했습니다(기존 확정 크리티컬 효과와 정상적으로 중첩됨)
* ✅fix11 : 공격전문가 "선공격"(Tier 1) 노드의 스태미나 소비 감소 효과가 실제로는 전혀 적용되지 않던 문제 수정 — 툴팁에는 선공격 윈도우 중 스태미나 소비 감소가 있다고 표시됐지만 코드에 적용 로직 자체가 없었습니다. 이제 선공격 윈도우 활성 중 공격 스태미나 소비가 실제로 감소합니다
* ✅fix12 : 공격전문가 "질풍 추격"(Tier 4) 노드가 사실상 아무 효과도 없던 문제 수정 — 툴팁에는 전투 중 이동속도 증가가 있다고 표시됐지만 이동속도 계산 어디에도 반영되지 않아, 트리 경로를 잇는 역할만 하고 있었습니다. 이제 전투 중 실제로 이동속도가 증가합니다
* ✅fix13 : 공격전문가 "마법: 혼돈의 시작" 노드가 F1 Config Manager의 온/오프 설정을 무시하던 문제 수정 — 선공격 윈도우 내 첫 마법 공격 확정 스태거가 관리자 설정값과 무관하게 항상 발동했습니다. 이제 설정을 실제로 반영합니다
* ✅improve5 : 공격전문가 트리 "선빵" 기믹의 한글 표시명을 비속어에서 격식체("선공격")로 변경 — 스킬명/툴팁/화면 메시지/컨피그 설명 전부 반영 (한국어 텍스트만 변경, 효과 변화 없음, 다른 언어는 영향 없음)
* ✅improve6 : 과거 공격전문가 트리 개편 과정에서 남은 죽은 코드 2건 정리 — 더 이상 쓰이지 않는 필드를 가리키던 주석 처리된 코드, 그리고 현재 트리에 존재하지 않는 "정밀 공격" 스킬을 체크하던 코드(수치가 이미 0으로 고정되어 있어 실질적인 동작 변화는 없음)
* ✅new3 : Captain Level System의 레벨 차이 경험치 감소가 기존 단순 반비례 곡선 대신, 데미지 감소 옵션과 동일하게 구간별 고정 비율로 바뀌었습니다 — 플레이어보다 몬스터 레벨이 크게 높거나 낮으면 레벨 차이 구간별로(11~15: 30%, 16~20: 25%, 21~30: 20%, 31+: 10%) 경험치가 지급되며, 전부 F1 Config Manager에서 조절 가능합니다
* ✅new4 : Captain Level System에 "레벨 차이 아이템 드랍 억제" 옵션을 추가했습니다(F1 Config Manager, 관리자 전용, 서버 동기화) — 몬스터 레벨이 처치한 플레이어보다 16 이상 높으면(기준값 조절 가능) 아이템이 전혀 드랍되지 않습니다
* ✅fix14 : "LV 차이 공격옵션"의 감소 시작 기준을 몬스터 이름표 흰색/빨간색 경계 및 새 경험치 구간제 기준과 맞췄습니다 — 이제 레벨차 10에서가 아니라 11부터 데미지 감소가 시작됩니다. 정확히 10랩 차이 몬스터(이름은 아직 흰색으로 표시됨)에게 한 랩 일찍 감소된 데미지가 들어가던 문제가 없어집니다

# \[2.1.19] - 2026-08-02

* ✅new1 : Added a new server-synced "Level Diff Damage Reduction" option (Captain Level System category in F1 Config Manager). When a monster's level is 10 or more above the attacking player's, outgoing damage is now capped by tier: 40% at a 10-15 level gap, 20% at 16-20, 10% at 21-30, and 0% at 31+. An admin-only toggle and all 4 tier percentages are configurable and synced to every client
* ✅fix1 : Fixed boss/monster kill quests (e.g. the Eikthyr quest) only counting progress for one party member instead of the whole party — kill credit is now broadcast to every nearby (within 50m) party member individually
* ✅improve1 : Reworked all 7 main boss-kill quests (Eikthyr, Elder, Bonemass, Moder, Yagluth, Seeker Queen, Fader) — progress now tracks how many boss trophies you're currently carrying (0-5) instead of a permanent kill counter, so it goes back down if you use, drop, or sell a trophy before turning the quest in, and turning it in consumes the 5 trophies as before
* ✅improve2 : Reworked the Mace expert's Tier 4 passive skill "Push" into "Concussion" — instead of knocking the attacker back, it now has a 35% chance to slow the attacker's movement speed by 30% for 1.5 seconds when hit while not blocking
* 
* ✅new1 : F1 Config Manager의 "Captain Level System" 카테고리에 서버 동기화되는 "LV 차이 공격옵션"을 추가했습니다. 몬스터 레벨이 공격하는 플레이어보다 10 이상 높으면 가하는 피해가 구간별로 제한됩니다: 레벨차 10\~15는 40%, 16\~20은 20%, 21\~30은 10%, 31 이상은 0%. 관리자 전용 온/오프 토글과 4개 구간 비율 모두 설정 가능하며 모든 클라이언트에 동기화됩니다
* ✅fix1 : 보스/몬스터 처치 퀘스트(예: 에이크시르 퀘스트)가 파티원 중 한 명에게만 카운트되고 나머지 파티원에게는 반영되지 않던 문제 수정 — 이제 처치 시 근처(50m 이내) 파티원 전원에게 개별적으로 진행도가 전파됩니다
* ✅improve1 : 7개 주요 보스 처치 퀘스트(에이크시르, 엘더, 본메스, 모더, 야글루스, 시커퀸, 페이더) 진행 방식 개편 — 이제 영구 처치 횟수 대신 현재 보유 중인 트로피 개수(0\~5)로 진행도를 표시합니다. 완료 처리 전에 트로피를 사용/버림/판매하면 진행도가 다시 줄어들며, 완료보고 시 기존과 동일하게 트로피 5개가 소모됩니다
* ✅improve2 : 둔기 전문가 Tier4 패시브 스킬 "밀어내기"를 "뇌진탕"으로 개편 — 공격자를 밀어내는 대신, 막기 미사용 상태에서 피격 시 35% 확률로 공격자의 이동속도를 1.5초간 30% 감소시킵니다

# \[2.1.13] - 2026-08-02

* ✅fix1 : Fixed the sword/axe 3-hit combo damage bonus and its "Enhanced Strike" floating text triggering even with zero skill points invested — it now requires the Sword Expert skill to be unlocked
* 
* ✅fix1 : 검/도끼 3연타 시 스킬 포인트를 전혀 투자하지 않아도 데미지 보너스와 "강화된 일격" 메시지가 뜨던 문제 수정 — 이제 검 전문가(Sword Expert) 스킬을 습득해야 발동됩니다

# \[2.1.12] - 2026-08-01

* ✅new1 : Added a new "Life" quest category (farming, cooking, fishing — 9 quests) that shows above Meadows in the Quest panel. Plant crops, cook meals, or catch fish, including three that require a set number of *different* types (10 crop types, 50 meal types, or all 12 fish species) instead of a raw count. Rewards a Magic Orb, an XP Potion, coins, and points in Valheim's native Farming/Cooking/Fishing skills
* ✅fix1 : Fixed the new Life-category quest labels (e.g. "Catch 12 Different Fish Species") visually overlapping the progress number next to them — widened the Quest window \~20% and gave the label column more room
* ✅fix2 : Cook quest progress now also counts meals made via the Cauldron/crafting menu (e.g. Carrot Soup) — previously only food cooked and collected from a CookingStation (campfire-style) counted
* ✅new2 : Reworked Meadows quest rewards — Raspberry now grants only a Queen Bee (no coins/orb/potion), Neck grants a fixed 30 coins + 20 Flint (no orb/potion), Boar grants a fixed 50 coins + 12 Leather Scraps (no orb/potion), Deer's coin reward was removed and it now grants a Minor XP Potion instead, and the Eikthyr boss quest now grants a fixed 100 coins plus exactly 1 Magic Orb (Lvl1) instead of 3 (no potion)
* ✅new3 : Added a new hidden Meadows quest — killing Eikthyr without ever taking damage and with no active food buffs now unlocks a bonus reward of 1 Magic Orb (Lvl1) plus a random piece of the Troll Leather armor set (Helmet, Chest, or Legs)
* ✅fix3 : Fixed the Quest window's hover tooltip staying stuck on screen after closing the window with Shift+Q, Esc, or Tab
* ✅new4 : Added a config toggle (QuestCompleteMusicEnabled) to turn the quest-complete jingle on/off, and made the jingle louder when it plays
* ✅new5 : Reworked Black Forest quest order and rewards — Greydwarf is now displayed as "Dwarf" in quests, a new Greydwarf Brute ("Elite Dwarf") kill quest was added, no quest grants a Magic Orb anymore, every quest grants a Minor XP Potion instead, and coin rewards were changed to fixed amounts (Greydwarf x20=50, Greydwarf x50=80, Greydwarf Brute x10=80, Bear x10=120, Troll x5=110, Elder boss=none)
* ✅new6 : Added a new hidden Black Forest quest — defeating the Elder using only melee weapons (no bow, crossbow, or magic) now unlocks a bonus reward of a random piece of Bronze armor (Helmet, Chest, or Legs)
* ✅new7 : Reworked Swamp quest order and rewards — Wraith is now displayed as "Ghost" in quests, the single Draugr-500 quest was split into two tiers (Draugr x20 and a new Draugr x300), no quest grants a Magic Orb except Abomination (now Lvl2 instead of the biome default), every quest grants an XP Potion, and coin rewards were changed to fixed amounts or removed (Draugr x20=120, Draugr x300=200, Blob=none, Wraith=150, Abomination=none, Bonemass boss=none)
* ✅new8 : Added a new hidden Swamp quest — defeating Bonemass using only magic (Elemental or Blood Magic staff, no melee or ranged weapons) now unlocks a bonus reward of a random piece of Iron or Drake/Wolf armor (Iron Helmet, Iron Chest, Iron Legs, Drake Helmet, Wolf Chest, or Wolf Legs)
* ✅fix4 : Fixed the 3 hidden achievement quests (flawless Eikthyr, melee-only Elder, magic-only Bonemass) showing a "Boss" tag instead of "Special", and fixed their random-armor-piece reward showing as a raw unlocalized string (e.g. "RandomItem:HelmetBronze,...") instead of translated item names
* ✅fix5 : Added missing hover-tooltip flavor descriptions for the 3 hidden achievement quests, and expanded the melee-only Elder quest's reward pool to also include a random piece of Iron armor alongside Bronze
* 
* ✅new1 : 목초지 위에 표시되는 새 "생활" 퀘스트 카테고리 추가(농사·요리·낚시 9종). 작물을 심거나 요리를 만들거나 물고기를 잡는 퀘스트이며, 그중 3개는 단순 누적 개수 대신 "서로 다른 종류"를 요구함(작물 10종, 요리 50종, 물고기 전체 12종). 보상으로 마법 오브, XP 포션, 코인과 함께 발헤임 자체의 농사/요리/낚시 숙련도를 지급
* ✅fix1 : 신규 생활 퀘스트 문구(예: "서로 다른 종류 물고기 12종 잡기")가 길어서 옆 진행 수치와 겹쳐 보이던 문제 수정 — 퀘스트 창 가로폭을 약 20% 넓히고 목록 텍스트 칸도 더 넓게 조정
* ✅fix2 : 가마솥(제작 메뉴)으로 만든 요리(예: 당근 스프)도 요리 퀘스트 진행도에 반영되도록 수정 — 기존에는 요리대에서 직접 구워 수거한 요리만 카운트됨
* ✅new2 : 목초지 퀘스트 보상 개편 — 라즈베리는 여왕벌 1개만 지급(코인/오브/포션 삭제), 넥은 코인 30 고정 + 부싯돌 20개(오브/포션 삭제), 멧돼지는 코인 50 고정 + 가죽 조각 12개(오브/포션 삭제), 사슴은 코인 보상을 없애고 대신 경험치 포션(소)을 지급, 에이크시르 보스 퀘스트는 코인 100 고정 + 마법 오브 Lv.1 1개만 지급(기존 3개→1개, 포션 삭제)
* ✅new3 : 목초지에 숨겨진 신규 퀘스트 추가 — 음식을 먹지 않은 상태에서 피해를 한 번도 입지 않고 에이크시르를 처치하면 마법 오브 Lv.1 1개와 트롤 가죽 세트(투구/가슴/하의 중 랜덤 1개)를 보너스로 지급
* ✅fix3 : Shift+Q/ESC/Tab으로 퀘스트 창을 닫아도 마우스오버 툴팁이 화면에 그대로 남아있던 문제 수정
* ✅new4 : 퀘스트 완료음을 켜고 끌 수 있는 Config 옵션(QuestCompleteMusicEnabled) 추가, 재생 시 음량도 더 크게 조정
* ✅new5 : 검은숲 퀘스트 순서·보상 개편 — 그레이드워프는 퀘스트에서 "난쟁이"로 표시되며, 새 처치 대상 "난쟁이 엘리(그레이드워프브루트)" 퀘스트 추가, 모든 퀘스트에서 마법 오브 지급을 없애고 대신 경험치 포션(소)을 지급, 코인 보상을 고정값으로 변경(난쟁이 x20=50, 난쟁이 x50=80, 난쟁이 엘리 x10=80, 곰 x10=120, 트롤 x5=110, 엘더 보스=없음)
* ✅new6 : 검은숲에 숨겨진 신규 퀘스트 추가 — 원거리 무기(활/석궁)나 마법 없이 근접 무기로만 엘더를 처치하면 청동 갑옷 세트(투구/가슴/하의 중 랜덤 1개)를 보너스로 지급
* ✅new7 : 늪지 퀘스트 순서·보상 개편 — 레이스는 퀘스트에서 "유령"으로 표시되며, 기존 드라우그 500마리 퀘스트를 두 단계(드라우그 x20, 신규 드라우그 x300)로 분리, 어보미네이션만 마법 오브 Lv.2 지급(바이옴 기본값 대신)하고 나머지는 오브 미지급, 모든 퀘스트에 경험치 포션 지급, 코인 보상을 고정값 또는 삭제로 변경(드라우그 x20=120, 드라우그 x300=200, 블롭=없음, 유령=150, 어보미네이션=없음, 본메스 보스=없음)
* ✅new8 : 늪지에 숨겨진 신규 퀘스트 추가 — 마법(원소/피 마법 지팡이)만으로 본메스를 처치하면(근접/원거리 무기 사용 없이) 철 갑옷 또는 드레이크/늑대 갑옷 세트(철투구/철상의/철하의/드레이크투구/늑대상의/늑대하의 중 랜덤 1개)를 보너스로 지급
* ✅fix4 : 숨겨진 특수 퀘스트 3종(무피해 에이크시르, 근접전용 엘더, 마법전용 본메스)이 "\[보스]"로 표시되던 문제를 "\[특수]"로 수정, 랜덤 방어구 보상이 "RandomItem:HelmetBronze,..." 같은 원본 문자열로 그대로 노출되던 문제를 언어별로 번역된 아이템명으로 표시되도록 수정
* ✅fix5 : 숨겨진 특수 퀘스트 3종에 빠져있던 마우스오버 툴팁 설명 추가, 근접전용 엘더 퀘스트의 보상 목록에 청동 갑옷과 함께 철 갑옷 랜덤 지급도 추가

# \[2.0.97] - 2026-07-31

* ✅fix1 : Fixed the Quest reward preview text still being hard to read — recolored it to a much darker green and made it bold
* ✅improve1 : Reworked several Meadows/Black Forest/Swamp quest targets and rewards — Deer/Greydwarf trophy-gathering quests now require killing the live monster instead (20 each, Deer Hide reward raised to 20); Black Forest's 50-Greydwarf-kill reward changed to Troll Hide; the Elder boss reward changed to Surtling Core + Iron Scrap; the Swamp Abomination reward upgraded to an Iron Pickaxe; corrected several Korean item names
* ✅fix2 : Fixed a bug where two Kill quests sharing the same monster target meant only the first one ever received kill credit — all matching quests now progress together
* ✅improve2 : Reworked several Mountain/Plains/Mistlands/Swamp/Ashlands quest rewards — Stone Golem now grants Silver Ore x100; Blob's Surtling Core reward raised to 10; the Mountain Wolf quest now requires 100 kills and grants a permanent +10 Max Stamina instead of Wolf Fangs; the Goblin Shaman (Fuling Shaman) quest now grants a permanent +2% Elemental Damage instead of Lox Pelt; the Goblin Brute quest now grants a tamed Wolf Cub instead of Linen Thread; the Seeker quest now grants a permanent +1% Physical Damage instead of Yggdrasil Wood; the Seeker Soldier's Soft Tissue reward (relabeled "Squishy Tissue") raised to 20; the Fallen Valkyrie reward changed from Charcoal Resin to Surtling Core; the Asksvin reward changed to 2 Bell Fragments; the Fader boss quest now grants 900-1200 coins plus 40 Surtling Cores
* ✅improve3 : The shield-refuel VFX and completion sound that used to play only when claiming the reward now play immediately when the quest goal is reached, alongside the confetti VFX; claiming the reward itself is now silent
* ✅improve4 : The quest-complete center message now says exactly what you finished (e.g. "Defeated Troll x5 — quest complete!" or "Gathered Raspberry x20 — quest complete!") instead of a generic "goal reached" line
* ✅improve5 : Claiming a quest reward now plays a short loot-drop chime again (the big VFX/music combo stays on quest completion; reward claim just gets a small confirmation sound)
* ✅new1 : Boss-kill quests now also require holding the boss's trophy in your inventory to claim the reward — if you don't have enough trophies, the row shows "Trophy x/y" instead of "Complete" and the reward button stays hidden; claiming consumes the required trophy count from your inventory
* ✅new2 : Quests now also grant WackyEpicMMOSystem's own XP items on claim — a Magic Orb (tier matches the quest's biome, Meadows=Lvl1 up through Ashlands=Lvl7) and an XP Potion (Minor for Meadows/Black Forest/Swamp, Medium for Mountain/Plains, Greater for Mistlands/Ashlands), both shown in the reward preview; every quest in a biome gives 1 until the second-to-last gives 2 and the boss gives 3
* ✅new3 : The tab-inventory Quest icon now shows a notification dot (matching the Skill Tree icon's existing one) whenever any quest is complete and ready to claim
* ✅fix5 : Fixed the reward preview list running off the right edge of the Quest window once the EXP tag made it longer — it's no longer pinned flush to the row's right edge and now starts further left with more room to grow
* ✅improve6 : Reordered each biome's quest list — boss quests always sit at the bottom, Meadows' Raspberry quest moved to the top, and a few quests were reshuffled (Swamp's Abomination, Mountain's Stone Golem, Mistlands' Seeker Soldier now sit right before their biome's boss; Plains' Fuling Shaman moved before Lox; Ashlands' Morgen and Fallen Valkyrie swapped)
* ✅fix4 : Fixed the Skill Tree icon's notification dot staying lit even with zero available skill points — it now recalculates from your actual available points both when you open the inventory and live while you invest/confirm/reset inside the Skill Tree panel
* ✅fix6 : Fixed the Quest icon's claimable-reward dot being a plain square instead of matching the Skill Tree icon's round dot — it now reuses that same inherited dot object instead of drawing a new square one, so the shape matches exactly; it also updates instantly when you claim a reward without needing to close and reopen the inventory
* ✅new4 : Hovering over a quest in the Quest window now shows a tooltip with a unique flavor description for that quest, its current progress, and its full reward list — fully localized in all 7 supported languages (KO/EN/DE/RU/PT-BR/JA/ZH-CN)
* 
* ✅fix1 : 퀘스트 보상 미리보기 텍스트가 여전히 잘 안 보이던 문제 수정 — 훨씬 진한 녹색으로 재조정하고 볼드 처리
* ✅improve1 : 목초지/검은숲/늪지 일부 퀘스트 대상과 보상 개편 — 사슴/회색난쟁이 트로피 채집 퀘스트를 실제 몬스터 처치로 변경(각 20마리, 사슴가죽 보상 20개로 증가), 검은숲 회색난쟁이 50마리 처치 보상을 트롤 가죽으로 변경, 엘더 보스 보상을 슈트링+고철로 변경, 늪지 어보미네이션 보상을 철 곡괭이로 업그레이드, 아이템 한글 표기 일부 수정
* ✅fix2 : 같은 몬스터를 목표로 하는 처치 퀘스트가 두 개 이상일 때 첫 번째 퀘스트만 진행되던 버그 수정 — 이제 조건에 맞는 모든 퀘스트가 함께 진행됨
* ✅improve2 : 산/평원/안개숲/늪지/잿빛땅 퀘스트 보상 개편 — 스톤골렘 보상을 은 원석 x100으로 변경, 블롭 보상 슈트링 3→10개, 늑대 처치는 100마리로 증가하고 늑대 이빨 대신 스테미나 최대치 영구 +10, 풀링 샤먼(고블린 샤먼) 처치는 록스 가죽 대신 속성 공격력 +2% 영구, 고블린 브루트 처치는 린넨 실 대신 길들여진 새끼 늑대 지급, 시커 처치는 위그드라실 나무 대신 물리 공격력 +1% 영구, 시커솔저 연조직(말랑한 조직) 보상 20개로 증가, 폴른 발키리 보상은 숯 수지 대신 슈트링, 아스크빈 보상은 종 조각 x2로 변경, 페이더 보스 보상을 코인 900\~1200 + 슈트링 x40으로 신규 설정
* ✅improve3 : 보상을 눌러야만 재생되던 방어막 충전 VFX와 완료음이 이제 목표 달성 즉시 컨페티 VFX와 함께 바로 재생됨 — 보상 수령 시에는 별도 이펙트 없이 조용히 지급
* ✅improve4 : 퀘스트 완료 중앙 메시지가 "목표를 달성했습니다" 같은 일반 문구 대신 "트롤 5마리 처치를 완료했습니다!", "라즈베리 20개 채집을 완료했습니다!"처럼 실제 완료한 내용을 정확히 표시
* ✅improve5 : 보상 수령 시 짧은 전리품 획득음이 다시 재생됨 (화려한 VFX·완료음은 퀘스트 완료 시점에 그대로 유지, 보상 수령은 작은 확인음만)
* ✅new1 : 보스 처치 퀘스트는 이제 처치 횟수뿐 아니라 해당 보스 트로피를 인벤토리에 보유해야 보상을 받을 수 있음 — 트로피가 부족하면 "완료" 대신 "트로피 x/y"로 표시되고 보상 버튼도 숨겨짐; 보상 수령 시 필요한 트로피 개수만큼 인벤토리에서 소모됨
* ✅new2 : 퀘스트 보상 수령 시 WackyEpicMMOSystem의 경험치 아이템도 함께 지급됨 — 퀘스트의 바이옴에 맞는 마법 오브(목초지=Lv.1 \~ 애쉬랜드=Lv.7)와 XP 포션(목초지/검은숲/늪지=소, 산/평원=중, 안개숲/애쉬랜드=대)을 보상 미리보기에 표시하고 실제 지급; 바이옴 내 마지막 바로 앞 퀘스트까지는 전부 1개, 그 다음은 2개, 보스는 3개 지급
* ✅new3 : 탭 인벤토리의 퀘스트 아이콘에 스킬트리 아이콘과 동일한 방식의 알림 점 추가 — 수령 가능한 완료 퀘스트가 있으면 뜸
* ✅improve6 : 바이옴별 퀘스트 목록 순서 재배치 — 보스 퀘스트는 항상 맨 아래로, 목초지 라즈베리 퀘스트는 맨 위로 이동, 늪지 어보미네이션·산 스톤골렘·안개숲 시커솔저는 각 바이옴 보스 직전으로, 평원 풀링 샤먼은 록스 앞으로, 애쉬랜드 모건과 폴른 발키리는 순서 교환
* ✅fix4 : 스킬트리 아이콘의 알림 점이 사용 가능한 스킬포인트가 0개여도 계속 켜져 있던 문제 수정 — 이제 인벤토리를 열 때뿐 아니라 스킬트리 창에서 투자/확정/초기화할 때도 실시간으로 실제 보유 포인트 기준으로 다시 계산됨
* ✅fix5 : 경험치 태그가 추가되며 길어진 보상 미리보기 목록이 퀘스트 창 오른쪽 밖으로 잘려 보이던 문제 수정 — 행 오른쪽 끝에 딱 붙이던 방식을 버리고 왼쪽으로 당겨 시작해 더 넓게 표시
* ✅fix6 : 퀘스트 아이콘의 보상수령 알림 점이 스킬트리 아이콘의 동그란 점과 달리 사각형으로 표시되던 문제 수정 — 새로 사각형을 그리는 대신 스킬트리 아이콘이 물려준 바로 그 동그란 점 오브젝트를 재사용해 모양이 완전히 일치함; 이 점도 인벤토리를 다시 여닫지 않아도 보상 수령 즉시 갱신됨
* ✅new4 : 퀘스트 창에서 퀘스트 위에 마우스를 올리면 해당 퀘스트의 고유 설명, 현재 진행상황, 전체 보상 목록을 보여주는 툴팁이 뜸 — 지원하는 7개 언어(한/영/독/러/포르투갈-브라질/일/중) 모두 완전 번역됨

# \[2.0.71] - 2026-07-30

* ✅new1 : Added a Quest system — biome-specific gather, monster-kill, and boss-kill quests that reward coins and items
* ✅new2 : Open the Quest panel from a new button inside the Skill Tree UI, a new icon next to the Tab-inventory skill tree icon, or the Shift+Q shortcut; close it with ESC or Tab
* ✅new3 : Reaching a quest goal plays a confetti VFX, and claiming the reward from the Quest panel plays a shield-refuel VFX with a bell sound
* ✅new4 : All quest lists, target amounts, and rewards are adjustable in the F1 Config Manager under "Quest System", including a master on/off switch (on by default)
* ✅new5 : Quest coin rewards are now granted straight to inventory instead of dropping on the ground (avoids spawning extra networked objects); coins only drop in front of the character if the inventory is full
* ✅improve1 : Quest System settings now fully follow the server config sync system (every field, including which resource/monster each quest targets and its rewards) — an admin's changes apply to every connected client instead of only their own machine
* ✅new6 : Added a new Meadows quest — gather 20 Raspberries for a Queen Bee reward
* ✅fix1 : Fixed the Quest window so biome names no longer get cut off and each row's progress/reward button actually shows up on the right side; also narrowed the overly wide rows
* ✅fix2 : Fixed the Quest icon next to the Tab-inventory Skill Tree icon (added diagnostics and a sprite fallback so it renders instead of staying blank)
* ✅fix3 : Gather quests now only progress when you personally harvest the resource — picking up items another player dropped or gathered no longer counts
* ✅improve2 : Quest list now shows monster/resource names translated into all 7 supported languages instead of raw English names
* ✅improve3 : Quest window now uses the same Valheim-styled background and gold-framed accents as the main Skill Tree panel instead of plain flat colors
* ✅fix4 : Fixed Evasion passive (dodge chance) incorrectly triggering against non-attack damage (fall, drowning, poison DoT, cold/weather) — it now only applies to actual enemy attacks
* ✅fix5 : Fixed the Rogue dagger skill "Assassin's Heart" (teleport strike) missing its attack animation — it now plays 3 consecutive melee swing motions on teleport-hit (motion only, no extra damage); its 500% attack speed buff now bypasses the global attack speed cap (like Fury Hammer/Spear Expert/Whirlwind) so it applies at full strength, still reverting to normal once the skill ends
* ✅fix6 : Fixed the Quest window's biome names still being cut off after the previous attempt — rebuilt the header layout to match the same pattern already used successfully by the quest rows
* ✅fix7 : Fixed the Quest window's progress numbers and status text being hard to read against the new parchment-style background — recolored them with higher-contrast dark tones
* ✅fix8 : Widened the window during which walking over resources from your own harvest (e.g. wood scattered after felling a tree) still counts toward gather quests, since auto-pickup can happen a few seconds after the harvest itself
* ✅fix9 : Fixed ore mining (Copper/Tin/Iron/Silver/Flametal etc.) never counting toward Gather quest progress even when personally mined — the personal-harvest check for mining/destructible nodes was comparing against the wrong health field and always failed
* ✅fix10 : Fixed the Quest window's biome names still getting cut off after two prior attempts — rebuilt the header using the exact same layout structure as the quest rows below it instead of a slightly different one
* ✅fix11 : Fixed the Wood gather quest still not progressing from felling trees — wood is now credited directly at the moment a tree/log is chopped down instead of waiting for it to be auto-picked-up afterward, since that pickup timing was too unpredictable to track reliably
* ✅fix12 : Fixed the Quest icon never appearing next to the Tab-inventory Skill Tree icon even when the Skill Tree icon itself was visible — now searches the whole scene for the Skill Tree icon directly instead of assuming a fixed menu path that didn't match this setup
* ✅fix13 : Fixed the Wood gather quest crediting almost nothing per tree — it was only reading the first entry of the tree's drop table, which isn't always Wood; it now scans every entry so the right item is always credited
* ✅fix14 : Fixed the cloned Quest icon showing the Skill Tree's own icon instead of the Production Expert icon (the "Icon" child name it expected didn't match this menu's actual structure)
* ✅fix15 : Fixed the Quest button inside the Skill Tree UI not responding to clicks — the Quest window could open behind the Skill Tree panel; it's now always brought to the front when opened
* ✅improve4 : The Quest window now updates progress numbers live while it's open instead of only refreshing when reopened
* ✅new7 : Each time a Quest's progress count ticks up, it now plays the same collection chime Valheim plays when gathering a resource
* ✅fix16 : Fixed the Quest window's biome names still getting cut off after several attempts — the header was requesting a text width almost as large as the whole window, which broke the layout math; lowered it to a sane size
* ✅fix17 : Fixed the cloned Quest icon still showing the Skill Tree's sword icon instead of the Production Expert icon — it was grabbing the border/frame image instead of the actual icon image; now picks the smallest image inside the button, which is reliably the icon
* ✅fix18 : Fixed the Quest window's item row and biome header text still getting clipped on the left — the Text component's own layout sizing was conflicting with the manually set width on the same object; separated them into a wrapper + child Text structure so the width calculation no longer depends on text content
* ✅fix19 : Fixed the Quest toolbar icon still showing the default sword icon even though the correct sprite loaded successfully — it was only recoloring a tiny 15x15 decorative child image; now the button's own root image is updated too
* ✅improve5 : Quest window background now uses a dedicated custom image instead of reusing the Skill Tree panel's background
* ✅improve6 : The reward-claim button now sits right next to "Complete", and the space it used to occupy always shows a preview of the quest's rewards (coins/items) instead of staying empty until completion
* ✅improve7 : Reworked several quest targets to fit their biome better — Meadows/Black Forest gathering quests now ask for Deer/Greydwarf trophies (credited immediately on kill, same as Wood already was); Swamp/Mountain/Plains/Mistlands/Ashlands gathering quests changed to killing Abomination/Stone Golem/Fuling Shaman/Seeker Soldier/Fallen Valkyrie instead; the Plains Lox-taming quest now needs 100 kills instead of 30
* ✅fix20 : Corrected the Korean quest names for Bjorn/The Elder/Moder
* ✅fix21 : Fixed the quest reward preview showing raw English item codes (e.g. "CookedDeerMeat") instead of proper names — it was trying to pull names from Valheim's own localization system via reflection, which didn't resolve reliably; switched to this mod's own translation dictionary (same method already used for quest target names)
* ✅new8 : The reward-claim sound now plays a custom "Quest Complete" jingle instead of the vanilla bell sound effect
* ✅new9 : Added a fanfare sound ("Hero Awakens") that plays whenever your character levels up, whether via EpicMMOSystem or the standalone leveling system — follows the existing "Show Level Up Effect" config toggle
* 
* ✅new1 : 바이옴별 자원 채집, 몬스터 처치, 보스 처치 퀘스트와 코인·아이템 보상을 받는 퀘스트 시스템 추가
* ✅new2 : 퀘스트 창은 스킬트리 UI 내 신규 버튼, 탭 인벤토리 화면의 신규 아이콘, Shift+Q 단축키로 열 수 있고 ESC 또는 Tab으로 닫힘
* ✅new3 : 퀘스트 목표 달성 시 폭죽 VFX가, 퀘스트 창에서 보상을 수령하면 실드 충전 VFX와 종소리 SFX가 재생됨
* ✅new4 : 모든 퀘스트 목록·목표 수량·보상은 F1 Config Manager의 "Quest System" 항목에서 조정 가능하며, 전체 기능을 켜고 끄는 스위치 포함(기본값 On)
* ✅new5 : 퀘스트 코인 보상은 바닥에 드롭하지 않고 인벤토리에 바로 지급되도록 변경(불필요한 네트워크 오브젝트 생성 방지) — 인벤토리가 가득 찼을 때만 캐릭터 앞에 드롭
* ✅improve1 : 퀘스트 시스템 설정 전체(어떤 자원/몬스터를 대상으로 하는지, 보상 종류까지 포함)가 서버 Config 동기화 시스템을 완전히 따르도록 변경 — 어드민이 값을 바꾸면 접속한 모든 클라이언트에 동일하게 적용됨
* ✅new6 : 목초지 신규 퀘스트 추가 — 라즈베리 20개 수집 시 보상으로 여왕벌 1개 지급
* ✅fix1 : 퀘스트 창에서 바이옴 이름이 잘리던 문제와 각 행 오른쪽 진행률/보상 버튼이 안 보이던 문제 수정, 지나치게 넓던 행 너비도 축소
* ✅fix2 : 탭 인벤토리 화면의 Quest 아이콘이 안 뜨던 문제 수정(진단 로그 및 스프라이트 폴백 추가로 빈 슬롯 대신 정상 표시)
* ✅fix3 : 채집 퀘스트가 본인이 직접 채집했을 때만 진행되도록 수정 — 다른 플레이어가 떨어뜨리거나 채집한 아이템을 주워도 더 이상 진행되지 않음
* ✅improve2 : 퀘스트 목록의 몬스터/자원 이름이 영문 그대로 표시되던 것을 지원하는 7개 언어 전부로 번역 표시하도록 변경
* ✅improve3 : 퀘스트 창이 밋밋한 단색 대신 메인 스킬트리 패널과 동일한 발헤임풍 배경과 금테 장식을 사용하도록 변경
* ✅fix6 : 이전 수정에도 바이옴 이름이 계속 잘리던 문제 재수정 — 이미 정상 동작하던 퀘스트 행과 동일한 구조로 헤더 레이아웃을 재구성
* ✅fix7 : 양피지풍 배경으로 바뀐 뒤 진행률 수치와 완료 상태 텍스트가 잘 안 보이던 문제 수정 — 대비가 뚜렷한 어두운 색상으로 재조정
* ✅fix10 : 두 번의 수정 후에도 바이옴 이름이 계속 잘리던 문제 재수정 — 헤더를 아래 퀘스트 행과 완전히 동일한 레이아웃 구조로 재구성
* ✅fix11 : 나무를 벌목해도 채집 퀘스트가 여전히 진행 안 되던 문제 수정 — 벌목 후 자동으로 주울 때까지 기다리지 않고, 나무/통나무를 베는 그 순간 바로 진행도를 지급하도록 변경(자동 습득 타이밍이 일정치 않아 신뢰할 수 없었음)
* ✅fix13 : 나무 채집 퀘스트가 거의 진행 안 되던 문제 재수정 — 드롭테이블의 첫 항목만 읽었는데 그게 항상 나무는 아니어서, 전체 항목을 훑어 정확한 아이템을 찾아 지급하도록 변경
* ✅fix14 : 복제된 Quest 아이콘이 생산전문가 아이콘이 아니라 스킬트리 아이콘 그대로 나오던 문제 수정 — 기대했던 'Icon' 자식 이름이 실제 메뉴 구조와 달랐던 게 원인
* ✅fix15 : 스킬트리 UI 안의 퀘스트 버튼을 눌러도 반응 없던 문제 수정 — 퀘스트 창이 스킬트리 패널 뒤에 가려져 열리고 있었음, 열 때마다 항상 맨 앞으로 오도록 변경
* ✅improve4 : 퀘스트 창이 열려있는 동안 진행 수치가 재접속 없이 실시간으로 갱신되도록 개선
* ✅new7 : 퀘스트 진행 카운트가 1씩 오를 때마다 발헤임의 자원 채집 시 나는 효과음이 재생되도록 추가
* ✅fix16 : 여러 번 수정해도 바이옴 이름이 계속 잘리던 문제 재수정 — 헤더가 창 전체 너비에 거의 맞먹는 텍스트 폭을 요청해서 레이아웃 계산이 깨졌던 것, 상식적인 크기로 낮춤
* ✅fix17 : 복제된 Quest 아이콘이 계속 스킬트리 검 아이콘 그대로 보이던 문제 수정 — 실제 아이콘이 아니라 테두리/배경 이미지를 잘못 집고 있었음, 버튼 안에서 가장 작은 이미지를 아이콘으로 간주하도록 변경
* ✅fix12 : 스킬트리 아이콘은 보이는데 그 옆 Quest 아이콘이 계속 안 뜨던 문제 수정 — 고정된 메뉴 경로를 가정하는 대신 씬 전체에서 스킬트리 아이콘을 직접 탐색하도록 변경
* ✅fix8 : 나무를 벌목한 뒤 흩어진 나무를 걸어가서 자동으로 줍는 경우처럼, 직접 채집한 직후 시간이 좀 걸려도 퀘스트가 인정되도록 유효 시간 확대
* ✅fix4 : 회피(회피 확률) 패시브가 공격이 아닌 피해(추락, 익사, 독 지속피해, 추위/날씨)에도 잘못 발동하던 문제 수정 — 이제 적의 실제 공격에만 적용됨
* ✅fix5 : 로그 단검 스킬 "암살자의 심장"(순간이동 연속공격)에서 공격 모션이 사라졌던 문제 수정 — 순간이동 적중 시 3연속 평타 모션이 재생됨(모션 전용, 추가 데미지 없음), 공격속도 버프 500%가 전역 공격속도 상한(캡)에 걸려 제대로 적용 안 되던 문제도 수정(분노의 망치/창 전문가/회오리베기처럼 캡 우회 적용, 스킬 종료 시 정상 속도로 복귀)
* ✅fix9 : 광석 채광(구리/주석/철/은/화염철 등)이 직접 캐도 채집 퀘스트 진행도로 전혀 인정되지 않던 문제 수정 — 채광/파괴형 자원의 "직접 채집" 판정이 잘못된 체력 필드를 참조해 항상 실패하고 있었음
* ✅fix18 : 퀘스트 창의 항목 텍스트와 바이옴 헤더가 여전히 왼쪽부터 잘리던 문제 수정 — Text 컴포넌트 자신의 레이아웃 크기 계산이 같은 오브젝트에 수동으로 지정한 폭과 충돌하고 있었음, 폭을 담당하는 래퍼와 텍스트를 분리해 텍스트 내용에 따라 폭 계산이 흔들리지 않도록 구조 변경
* ✅fix21 : 퀘스트 보상 미리보기가 제대로 된 이름 대신 영문 원본 코드(예: "CookedDeerMeat")를 그대로 보여주던 문제 수정 — 발헤임 자체 로컬라이제이션을 리플렉션으로 우회해서 가져오려하던 방식이 안정적이지 않았음, 퀘스트 대상 이름에 이미 쓰고 있던 이 모드 자체 번역 사전 방식으로 통일
* ✅new8 : 보상 수령 시 기본 종소리 대신 전용 "퀘스트 완료" 효과음이 재생되도록 변경
* ✅fix19 : 스프라이트 로드는 성공했는데도 퀘스트 툴바 아이콘이 계속 기본 검 아이콘으로 보이던 문제 수정 — 15x15짜리 작은 장식용 자식 이미지만 바꾸고 있었음, 이제 버튼 자체의 루트 이미지도 함께 적용
* ✅improve5 : 퀘스트 창 배경을 스킬트리 패널 배경 재사용 대신 전용 커스텀 이미지로 변경
* ✅improve6 : 보상 수령 버튼을 "완료" 텍스트 바로 옆으로 이동, 기존 버튼 자리에는 완료 여부와 상관없이 항상 보상(코인/아이템) 미리보기를 표시
* ✅improve7 : 여러 퀘스트 대상을 바이옴에 더 어울리게 변경 — 목초지/검은숲 채집 퀘스트를 사슴/회색난쟁이 트로피로 변경(나무처럼 처치 즉시 지급), 늪지/산/평원/안개숲/잿빛땅 채집 퀘스트를 어보미네이션/스톤골렘/풀링샤먼/시커솔저/폴른발키리 처치로 변경, 평원 록스 테이밍 퀘스트는 30마리에서 100마리로 증가
* ✅fix20 : 비외른/장로/모드르의 한글 퀘스트 표기를 곰/엘더/모더로 수정
* ✅new9 : 레벨업 시 팡파레 사운드("Hero Awakens")가 재생되도록 추가했습니다. EpicMMOSystem 연동 여부와 관계없이 모든 레벨업 경로에 적용되며, 기존 "레벨업 이펙트 표시" 설정으로 켜고 끌 수 있습니다.

# \[2.0.27] - 2026-07-29

* ✅fix1 : Fixed the Rogue job being unable to equip a shield — Rogue now allows shields, matching Tanker/Paladin/Producer
* ✅fix2 : Fixed the Berserker job being unable to equip a shield — Berserker now allows shields as well
* 
* ✅fix1 : 로그 직업이 방패를 착용할 수 없던 문제 수정 — 탱커/성기사/제작전문가와 동일하게 로그도 방패 착용 가능
* ✅fix2 : 버서커 직업이 방패를 착용할 수 없던 문제 수정 — 버서커도 방패 착용 가능


# CaptainSkillTree - Valheim Skill Tree Mod

**[The English description is below](#-English)**

---

# 한국어 (Korean)

## 개요
CaptainSkillTree는 발헤임의 캐릭터 성장을 위한 종합 스킬트리 모드입니다. 
전문가 트리, 무기 트리, 직업 시스템을 통해 바이킹의 능력을 강화하세요!

## 필수 모드 (Dependencies)

| 모드 | 버전 | 필수 여부 |
|------|------|----------|
| BepInExPack_Valheim | 5.4.2200+ | 필수 |
| Jotunn | 2.20.0+ | 필수 |
| EpicMMOSystem | 1.8.0+ | 권장 (없어도 동작) |
| ConfigurationManager | 최신 | 권장 (GUI 설정용) |

> **참고**: CaptainSkillTree는 자체 레벨 시스템을 내장하고 있습니다. EpicMMOSystem이 설치되지 않은 경우 자동으로 내장 레벨 시스템을 사용합니다.(보조적인 진행을 위함입니다. 가능한 EpicMMOSystem을 설치하고 플레이 바랍니다.)

## 주요 특징

### 종합 스킬트리 시스템
- **4개 전문가 트리**: 공격, 속도, 방어, 생산
- **7개 무기 트리**: 활, 지팡이, 석궁, 단검, 검, 둔기, 창, 폴암
- **6개 직업**: 궁수, 마법사, 탱커, 로그, 광전사, 성기사

### 패시브 & 액티브 스킬
- **패시브 스킬**: 자동 스탯 보너스 (VFX/SFX 없음)
- **액티브 스킬**: 키 바인딩으로 발동하는 강력한 스킬 + 화려한 VFX

### 서버 싱크
- 모든 Config가 서버에서 클라이언트로 동기화
- 관리자가 Config 파일로 밸런스 조절 가능
- 서버 재시작 없이 실시간 업데이트

### 독립 실행 모드
- EpicMMOSystem 없이도 독립적으로 작동
- 자동 감지 및 내장 레벨 시스템으로 전환
- 발헤임 업데이트로 인한 필수 모드만으로도 끊김 없이 플레이 가능

### 전용 BGM
- 스킬트리 전용 배경음악 탑재
- 스킬트리 UI가 열리면 자동 재생
- 발헤임 음악을 일시정지하고, 닫으면 자동 재개

## 스킬트리 여는 방법

1. **TAB** 키로 인벤토리 열기
2. 레벨 시스템 버튼 옆의 **스킬트리 아이콘** (검 아이콘) 클릭
3. 전용 BGM과 함께 스킬트리 UI 열림

## 키 바인딩

| 키 | 용도 | 설명 |
|----|------|------|
| **R** | 원거리 액티브 | 석궁/활/지팡이 중 택1 |
| **G** | 근접 메인 액티브 | 같은 무기 트리만 |
| **H** | 보조 액티브 | G키와 같은 무기 트리 |
| **Y** | 직업 액티브 | 6개 직업 중 택1 |

## 스킬 트리

### 전문가 트리 (Expert Trees)
| 트리 | 설명 | 주요 효과 |
|------|------|----------|
| Attack | 공격 전문가 | 데미지+, 크리티컬 확률/데미지 |
| Speed | 속도 전문가 | 이동속도, 공격속도, 쿨타임 감소 |
| Defense | 방어 전문가 | 체력, 방어력, 회피율, 재생력 |
| Production | 생산 전문가 | 채집 효율, 제작 보너스 |

### 무기 트리 (Weapon Trees)

**원거리**
| 트리 | 설명 | 액티브 스킬 |
|------|------|-----------|
| Bow | 활 전문가 | 폭발화살 (R키) |
| Staff | 지팡이 전문가 | 이중시전, 힐 (R/H키) |
| Crossbow | 석궁 전문가 | 단한발 (R키) |

**근접**
| 트리   |     설명           |         액티브 스킬                    |
|------- |-------------------|-------------------------------------|
| Knife  | 단검 전문가     | 암살자 (G키) |
| Sword |  검 전문가      | 돌진베기, 패링돌격 (G/H키) |
| Mace  | 둔기 전문가    | 수호자진심, 분노의망치 (G/H키) |
| Spear  | 창 전문가       | 꿰뚫기, 연공창 (G/H키) |
| Polearm | 폴암 전문가 | 장창제왕 (G키) |


### 직업 트리 (Job Classes)
| 직업 | 설명 | 액티브 (Y키) | 패시브 |
|------|------|------------|--------|
| Archer | 궁수 | 멀티샷 | 점프 높이+, 낙사 감소 |
| Mage | 마법사 | 마나폭발 | Eitr 보너스 |
| Tanker | 탱커 | 충격파방출 | 피해감소, 체력+ |
| Rogue | 로그 | 암살 | 크리티컬+, 은신 |
| Berserker | 광전사 | 광전사의 분노 | 체력 낮을수록 데미지+ |
| Paladin | 성기사 | 신성한 빛 | 힐, 버프 |

## 관리자 명령어

| 명령어 | 설명 |
|--------|------|
| `skillreset <플레이어이름>` | 플레이어 스킬 초기화 |
| `skilladd <숫자> <캐릭터이름>` | 스킬 포인트 추가 |

## 설정 관리 (Configuration Management)

### ConfigurationManager 사용하기 (권장)

**ConfigurationManager**를 사용하면 게임 내에서 GUI로 모든 모드 설정을 편리하게 조정할 수 있습니다.

1. Thunderstore에서 **BepInEx.ConfigurationManager** 설치
2. 게임 내에서 **F1** 키를 눌러 Configuration Manager 열기
3. 모드 목록에서 **CaptainSkillTree** 찾기
4. 슬라이더, 체크박스, 입력 필드로 원하는 설정 조정
5. 변경사항이 **즉시 적용** - 재시작 불필요!

### GUI 기능
- **실시간 미리보기**: 변경사항을 즉시 확인
- **검색 기능**: 특정 설정을 빠르게 찾기
- **카테고리 정리**: 트리/스킬 타입별로 그룹화
- **기본값 복원**: 원클릭으로 원래 값으로 복원
- **서버 싱크 표시**: 서버에서 동기화되는 설정 표시

### Config 파일 위치
```
BepInEx/config/CaptainSkillTree.cfg
```

### Config 카테고리

| 카테고리 | 설명 | 예시 설정 |
|----------|------|----------|
| **Attack Tree** | 공격 전문가 설정 | 데미지 보너스 %, 크리티컬 확률 |
| **Speed Tree** | 속도 전문가 설정 | 이동속도 %, 공격속도 % |
| **Defense Tree** | 방어 전문가 설정 | 체력 보너스, 방어력 보너스, 회피율 |
| **Production Tree** | 생산 전문가 설정 | 채집 효율 % |
----------------------------------------------------------------------------------------
| **Bow Tree** | 활 스킬 설정 | 멀티샷 확률, 화살 수 |
| **Staff Tree** | 지팡이 스킬 설정 | 이중시전 확률, 힐량 |
| **Crossbow Tree** | 석궁 스킬 설정 | 단한발 데미지 |
| **Sword Tree** | 검 스킬 설정 | 돌진베기 데미지, 쿨타임 |
| **Knife Tree** | 단검 스킬 설정 | 암살자 데미지 배율 |
| **Spear Tree** | 창 스킬 설정 | 꿰뚫기 데미지, 콤보 횟수 |
| **Polearm Tree** | 폴암 스킬 설정 | 장창제왕 범위, 넉백 |
| **Mace Tree** | 둔기 스킬 설정 | 수호자 반경, 분노 데미지 |
----------------------------------------------------------------------------------------
| **Archer Job Skills** | 궁수 직업 설정 | 멀티샷 화살 수, 쿨타임 |
| **Mage Job Skills** | 마법사 직업 설정 | 마나폭발 데미지, Eitr 소모 |
| **Tanker Job Skills** | 탱커 직업 설정 | 충격파 반경, 체력 보너스 |
| **Rogue Job Skills** | 로그 직업 설정 | 암살 배율 |
| **Berserker Job Skills** | 광전사 직업 설정 | 분노 지속시간, HP당 데미지 |
| **Paladin Job Skills** | 성기사 직업 설정 | 힐량, 버프 지속시간 |

### 서버 관리자 참고사항
- 모든 설정은 `IsAdminOnly = true`로 자동 서버 싱크
- 서버 설정이 클라이언트 설정을 자동으로 덮어씀
- Config 파일 변경이 감지되어 모든 클라이언트에 자동 전송
- 서버 재시작 불필요 - 변경사항이 실시간 적용

## 설치 방법
1. **권장**: r2modman/Thunderstore에서 자동 설치
2. **수동**: `BepInEx/plugins/CaptainSkillTree/` 폴더에 DLL 복사

---
# English

## Overview
CaptainSkillTree is a comprehensive skill tree mod for Valheim that adds Expert Trees, Weapon Trees, and a Job System. Enhance your Viking's abilities with passive bonuses and powerful active skills!

## Required Mods (Dependencies)

| Mod | Version | Required |
|-----|---------|----------|
| BepInExPack_Valheim | 5.4.2200+ | Required |
| Jotunn | 2.20.0+ | Required |
| EpicMMOSystem | 1.8.0+ | Recommended (works without it) |
| ConfigurationManager | Latest | Recommended (for GUI settings) |

> **Note**: CaptainSkillTree has its own built-in level system. If EpicMMOSystem is not installed, the mod will automatically use its internal leveling system.

## Features

### Comprehensive Skill Tree System
- **4 Expert Trees**: Attack, Speed, Defense, Production
- **7 Weapon Trees**: Bow, Staff, Crossbow, Knife, Sword, Mace, Spear, Polearm
- **6 Job Classes**: Archer, Mage, Tanker, Rogue, Berserker, Paladin

### Passive & Active Skills
- **Passive Skills**: Automatic stat bonuses (no VFX/SFX)
- **Active Skills**: Powerful abilities with keybindings and stunning VFX

### Server Sync
- All configs sync from server to clients
- Admins can adjust balance through config files
- Real-time updates without server restart

### Standalone Mode
- Works independently without EpicMMOSystem
- Automatic detection and fallback to built-in level system
- Seamless experience either way

### Custom BGM
- Exclusive skill tree background music
- Plays when skill tree UI is open
- Automatically pauses Valheim's music and resumes when closed

## How to Open Skill Tree

1. Press **TAB** to open inventory
2. Click the **Skill Tree Icon** (sword icon) near the level system button
3. Skill Tree UI opens with custom BGM

## Key Bindings

| Key | Function | Description |
|-----|----------|-------------|
| **R** | Ranged Active | Crossbow/Bow/Staff skill (choose 1) |
| **G** | Melee Main Active | Same weapon tree only |
| **H** | Sub Active | Same weapon tree as G key |
| **Y** | Job Active | 1 of 6 jobs |

## Skill Trees

### Expert Trees
| Tree | Description | Main Effects |
|------|-------------|--------------|
| Attack | Attack Expert | Damage+, Critical Chance/Damage |
| Speed | Speed Expert | Move Speed, Attack Speed, Cooldown Reduction |
| Defense | Defense Expert | HP, Armor, Dodge Rate, Regeneration |
| Production | Production Expert | Gathering Efficiency, Crafting Bonuses |

### Weapon Trees

**Ranged**
| Tree | Description | Active Skill |
|------|-------------|--------------|
| Bow | Bow Expert | Explosive Arrow (R) |
| Staff | Staff Expert | Double Cast, Heal (R/H) |
| Crossbow | Crossbow Expert | Single Shot (R) |

**Melee**
| Tree | Description | Active Skill |
|------|-------------|--------------|
| Sword | Sword Expert | Rush Slash, Parry Rush (G/H) |
| Knife | Knife Expert | Assassin (G) |
| Spear | Spear Expert | Penetrate, Combo Spear (G/H) |
| Polearm | Polearm Expert | Polearm King (G) |
| Mace | Mace Expert | Guardian Heart, Fury Hammer (G/H) |

### Job Classes
| Job | Description | Active (Y) | Passive |
|-----|-------------|------------|---------|
| Archer | Bowman | Multi-Shot | Jump Height+, Fall Damage- |
| Mage | Wizard | Mana Burst | Eitr Bonus |
| Tanker | Tank | Shockwave | Damage Reduction, HP+ |
| Rogue | Assassin | Assassination | Critical+, Stealth |
| Berserker | Berserker | Berserker Rage | Lower HP = Higher Damage |
| Paladin | Holy Knight | Holy Light | Heal, Buffs |

## Admin Commands

| Command | Description |
|---------|-------------|
| `skillreset <player>` | Reset player's skills |
| `skilladd <amount> <player>` | Add skill points to player |

## Configuration Management

### Using ConfigurationManager (Recommended)

**ConfigurationManager** provides a convenient in-game GUI for adjusting all mod settings.

1. Install **BepInEx.ConfigurationManager** from Thunderstore
2. Press **F1** in-game to open the Configuration Manager
3. Find **CaptainSkillTree** in the mod list
4. Adjust any setting with sliders, checkboxes, and input fields
5. Changes apply **immediately** - no restart required!

### GUI Features
- **Real-time Preview**: See changes instantly
- **Search Function**: Quickly find specific settings
- **Category Organization**: Settings grouped by tree/skill type
- **Reset to Default**: One-click restore original values
- **Server Sync Indicator**: Shows which settings sync from server

### Config File Location
```
BepInEx/config/CaptainSkillTree.cfg
```

### Config Categories

| Category | Description | Example Settings |
|----------|-------------|------------------|
| **Attack Tree** | Attack Expert settings | Damage bonus %, Critical chance |
| **Speed Tree** | Speed Expert settings | Move speed %, Attack speed % |
| **Defense Tree** | Defense Expert settings | HP bonus, Armor bonus, Dodge rate |
| **Production Tree** | Production Expert settings | Gathering efficiency % |
| **Bow Tree** | Bow skill settings | Multi-shot chance, Arrow count |
| **Staff Tree** | Staff skill settings | Double cast chance, Heal amount |
| **Crossbow Tree** | Crossbow skill settings | Single shot damage |
| **Sword Tree** | Sword skill settings | Rush slash damage, Cooldown |
| **Knife Tree** | Knife skill settings | Assassin damage multiplier |
| **Spear Tree** | Spear skill settings | Penetrate damage, Combo count |
| **Polearm Tree** | Polearm skill settings | King skill range, Knockback |
| **Mace Tree** | Mace skill settings | Guardian radius, Fury damage |
| **Archer Job Skills** | Archer job settings | Multi-shot arrows, Cooldown |
| **Mage Job Skills** | Mage job settings | Mana burst damage, Eitr cost |
| **Tanker Job Skills** | Tanker job settings | Shockwave radius, HP bonus |
| **Rogue Job Skills** | Rogue job settings | Assassination multiplier |
| **Berserker Job Skills** | Berserker job settings | Rage duration, Damage per HP% |
| **Paladin Job Skills** | Paladin job settings | Heal amount, Buff duration |

### Server Admin Notes
- All settings have `IsAdminOnly = true` for automatic server sync
- Server settings override client settings automatically
- Config file changes are detected and broadcast to all clients
- No server restart needed - changes apply in real-time

## Installation
1. **Recommended**: Install via r2modman/Thunderstore
2. **Manual**: Extract to `BepInEx/plugins/CaptainSkillTree/`

---

## Support / 지원

- Discord: KorCaptainSkillTree_MOD_Server - https://discord.gg/PyEvu6c8
- E-mail : ssunyme@naver.com
- Issues: Report bugs and suggestions on Discord

---

## Credits

- **Developer**: KorCaptain
- **Framework**: BepInEx, Jotunn
- **Compatible with**: EpicMMOSystem (WackyMole), ConfigurationManager

---

*Thank you for using CaptainSkillTree! / CaptainSkillTree를 사용해 주셔서 감사합니다!*

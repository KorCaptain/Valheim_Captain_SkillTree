# CaptainSkillTree - Valheim Skill Tree Mod

[![Ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/korcaptain)

**[한국어 설명 보기](#-한국어-korean)** | **[English Description](#-english)**

---

## 📸 ScreenShot~!

### 🌳 Skill Tree Overview
![Tree](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Skill_Tree.gif)

### ⚔️ Skill Icons
![Skill](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Skill_Icon_2.gif)

## 🎥 Play~

### 🗡️ Assassin - Stealth Strike (Knife / G)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Assasin.gif)

### 🏃 Rush Slash (Sword / G)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Rush.gif)

### 🛡️ Parry Rush (Sword / H)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/parry.gif)

### 🏹 Explosive Arrow (Bow / Z)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/ExArrow.gif)

### 💚 Heal (Paladin / Y)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Heal.gif)

### ⚡ Fire Rain (Mage / Y)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Mage_Firerain.gif)

### 🌧️ Arrow Rain (Bow / H)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Arrow_Rain2.gif)

### 🔨 Fury Hammer (Mace / G)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/hammer.gif)

### ⚡ Rush Shield (Mace / H)
![Demo](https://raw.githubusercontent.com/KorCaptain/Valheim_Captain_SkillTree/main/images/Rush_SH.gif)

#Language support: 한국어, English, Русский, Português-Brasil, das Deutsche, 中国话, 日本語

---

# 🇰🇷 한국어 (Korean)

## 개요
CaptainSkillTree는 발헤임의 캐릭터 성장을 위한 종합 스킬트리 모드입니다.
전문가 트리, 무기 트리, 직업 시스템을 통해 바이킹의 능력을 강화하세요!

---

<details>
<summary>📦 필수 모드 (Dependencies)</summary>

| 모드 | 버전 | 필수 여부 |
|------|------|----------|
| BepInExPack_Valheim | 5.4.2200+ | 필수 |
| Jotunn | 2.20.0+ | 필수 |
| WackyEpicMMOSystem | 최신 | 권장 |
| ConfigurationManager | 최신 | 권장 (GUI 설정용) |

> **참고**: CaptainSkillTree는 자체 레벨 시스템을 내장하고 있습니다. WackyEpicMMOSystem이 설치되지 않은 경우 자동으로 내장 레벨 시스템을 사용합니다.(보조적인 진행을 위함입니다. 가능한 WackyEpicMMOSystem을 설치하고 플레이 바랍니다.)

</details>

---

<details>
<summary>✨ 주요 특징</summary>

### 종합 스킬트리 시스템
- **4개 전문가 트리**: 공격, 속도, 방어, 생산
- **8개 무기 트리**: 활, 지팡이, 석궁, 단검, 검, 둔기, 창, 폴암
- **7개 직업**: 궁수, 마법사, 탱커, 로그, 광전사, 성기사, 제작전문가

### 패시브 & 액티브 스킬
- **패시브 스킬**: 자동 효과 보너스 (VFX/SFX 없음)
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

</details>

---

<details>
<summary>🗺️ 스킬트리 여는 방법</summary>

1. **TAB** 키로 인벤토리 열기
2. 레벨 시스템 버튼 옆의 **스킬트리 아이콘** (검 아이콘) 클릭
3. EpicMMO 모드 없을때는 캐릭터 머리위 **스킬트리 아이콘** (검 아이콘) 클릭
4. 전용 BGM과 함께 스킬트리 UI 열림

</details>

---

<details>
<summary>⌨️ 키 바인딩</summary>

| 키 | 용도 | 설명 |
|----|------|------|
| **Z** | 원거리 액티브 | 석궁/활/지팡이 중 택1 |
| **G** | 근접 메인 액티브 | 같은 무기 트리만 |
| **H** | 보조 액티브 | 화살비(활), 패링돌격(검), 방패돌진(둔기) 등 |
| **Y** | 직업 액티브 | 7개 직업 중 택1 |

</details>

---

<details>
<summary>🌳 스킬 트리</summary>

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
| Bow | 활 전문가 | 폭발화살 (Z키), 화살비 (H키) |
| Staff | 지팡이 전문가 | 연속시전, 힐 (Z/H키) |
| Crossbow | 석궁 전문가 | 단한발 (Z키) |

**근접**
| 트리 | 설명 | 액티브 스킬 |
|------|------|-----------|
| Knife | 단검 전문가 | 암살자 (G키) |
| Sword | 검 전문가 | 돌진베기 (G키), 패링돌격 (H키) |
| Mace | 둔기 전문가 | 분노의망치 (G키), 방패돌진 (H키) |
| Spear | 창 전문가 | 꿰뚫기, 연공창 (G/H키) |
| Polearm | 폴암 전문가 | 장창제왕 (G키) |

### 직업 트리 (Job Classes)
| 직업 | 설명 | 액티브 (Y키) | 패시브 |
|------|------|------------|--------|
| Archer | 궁수 | 멀티샷 | 점프 높이+, 낙사 감소 |
| Mage | 마법사 | 마나폭발 | Eitr 보너스 |
| Tanker | 탱커 | 전장의 함성 (범위 도발 + 반사 + 피해감소) | 피해감소, 체력+ |
| Rogue | 로그 | 암살 | 크리티컬+, 은신 |
| Berserker | 광전사 | 광전사의 분노 | 체력 낮을수록 데미지+ |
| Paladin | 성기사 | 신성한 빛 | 힐, 버프 |
| Producer | 제작전문가 | 장인의 축복 | 팜그리드, 내구도+, 재료감소, 마법부여 |

<details>
<summary>🌾 제작전문가 (Producer) 상세</summary>

#### 팜그리드 (Farm Grid)
- 경작 도구(Cultivator/Hoe) 장착 시 **녹색 격자** 시각화 자동 표시
- 씨앗 심을 때 격자 위치에 자동 추가 식재 (보유 씨앗 소모)

**휠마우스 조작:**

| 조작 | 효과 | 범위 |
|------|------|------|
| 휠마우스 | 격자 **회전** (5도 단위) | -180° ~ +180° |
| Alt + 휠마우스 | 격자 **간격** 조절 (0.1x 단위) | 0.5x ~ 3.0x |

**레벨별 그리드 크기:**

| 레벨 | 크기 | 한번에 심는 총 칸 수 |
|------|------|-------------------|
| Lv1 | 1×2 | 2칸 |
| Lv2 | 2×2 | 4칸 |
| Lv3 | 2×3 | 6칸 |
| Lv4 | 3×3 | 9칸 |
| Lv5 | 3×4 | 12칸 |

> 경작 도구 해제 시 격자·회전·간격이 초기화됩니다.

#### 마법부여 (Enchant) - Lv3+ 활성화
- 아이템 제작 완료 시 **확률적**으로 마법부여 적용
- 마법부여된 아이템은 인벤토리/핫키바에 **붉은 테두리** 표시
- 아이템 툴팁에 **금색 ✨** 아이콘과 마법부여 수치 표시

**마법부여 확률:**

| 레벨 | 확률 |
|------|------|
| Lv3 | 25% |
| Lv4 | 30% |
| Lv5 | 35% |

**마법부여 종류 - 일반 무기** (공격력/공격속도 1:1):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 공격력 (WeaponDmg) | 3~5% | 6~8% | 9~12% |
| 공격속도 (WeaponSpd) | 3~5% | 6~8% | 9~12% |

**마법부여 종류 - 활** (공격력/치명타 1:1):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 공격력 (WeaponDmg) | 3~5% | 6~8% | 9~12% |
| 치명타 확률 (BowCrit) | +3~5% | +6~8% | +9~12% |

**마법부여 종류 - 석궁** (공격력/재장전 1:1):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 공격력 (WeaponDmg) | 3~5% | 6~8% | 9~12% |
| 재장전 단축 (CrossbowReload) | 20~40ms | 45~70ms | 75~100ms |

**마법부여 종류 - 투구** (최대체력/쿨타임감소 1:1):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 최대체력 (MaxHP) | +3~5% | +6~8% | +9~12% |
| 쿨타임 감소 (CooldownReduce) | 3~5% | 6~8% | 9~12% |

**마법부여 종류 - 상의** (최대체력/방어력 1:1):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 최대체력 (MaxHP) | +3~5% | +6~8% | +9~12% |
| 방어력 (Armor) | +3~5% | +6~8% | +9~12% |

**마법부여 종류 - 하의** (회피스태미나/이동속도 1:1):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 회피 스태미나 감소 (DodgeRoll) | 3~5% | 6~8% | 9~12% |
| 이동속도 (MoveSpeed) | +3~5% | +6~8% | +9~12% |

**마법부여 종류 - 망토** (스태미나/에이트르 1:1):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 최대스태미나 (MaxStamina) | +5~8% | +9~12% | +13~15% |
| 에이트르 (Eitr) | +5~8 | +9~12 | +13~15 |

**마법부여 종류 - 악세사리** (인벤무게/에이트르회복/점프력 중 랜덤):

| 종류 | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| 인벤 최대 무게 (InvWeight) | +80~100 | +100~125 | +130~150 |
| 에이트르 회복속도 (EitrRegen) | +5~8% | +9~12% | +13~15% |
| 점프력 (JumpForce) | +5~8% | +9~12% | +13~15% |

> 적용 가능 아이템: 한손/양손무기, 활, 석궁, 투구, 상의, 하의, 망토, 악세사리

<details>
<summary>🔧 마법부여 수치 커스텀 (Producer_Enchant.json)</summary>

마법부여 수치와 슬롯 풀은 모드 내부의 `Producer_Enchant.json`으로 관리됩니다.
소스코드에서 이 파일을 수정 후 빌드하면 모든 수치를 자유롭게 조정할 수 있습니다.

#### JSON 구조

```json
{
  "enchant_types": [
    {
      "id": 6,
      "name": "BowCrit",
      "display_key": "producer_enchant_bow_crit",
      "unit": "%",
      "lv3": { "min": 3.0, "max": 5.0 },
      "lv4": { "min": 6.0, "max": 8.0 },
      "lv5": { "min": 9.0, "max": 12.0 }
    }
  ],
  "slot_pools": {
    "Bow": [{"id": 1, "weight": 1}, {"id": 6, "weight": 1}]
  }
}
```

| 필드 | 설명 |
|------|------|
| `id` | 마법부여 고유 번호 (1~14, 변경 금지) |
| `unit` | 표시 단위 (`%`, `ms`, 또는 빈 문자열) |
| `lv3/lv4/lv5` | 제작 전문가 Lv3/4/5 적용 시 랜덤 수치 범위 |
| `slot_pools` | 슬롯별 마법부여 후보 목록 |
| `weight` | 가중치 — 높을수록 더 자주 등장 (기본: 모두 1) |

#### 슬롯 키 목록

| 슬롯 키 | 대응 아이템 |
|---------|------------|
| `Weapon` | 한손/양손 무기 |
| `Bow` | 활 |
| `Crossbow` | 석궁 |
| `Helmet` | 투구 |
| `Chest` | 상의 |
| `Legs` | 하의 |
| `Shoulder` | 망토 |
| `Accessory` | 악세사리 |

#### 커스텀 예시

활의 치명타 확률 상한을 높이고, 공격력보다 2배 자주 등장시키려면:
```json
"slot_pools": {
  "Bow": [{"id": 1, "weight": 1}, {"id": 6, "weight": 2}]
},
"enchant_types": [
  { "id": 6, "name": "BowCrit", "unit": "%",
    "lv3": {"min": 5.0, "max": 8.0},
    "lv4": {"min": 8.0, "max": 12.0},
    "lv5": {"min": 12.0, "max": 18.0} }
]
```

> 마법부여 **확률**(Lv별 등장 %)은 F1 ConfigManager → Producer Job Skills에서 조정 가능합니다.

</details>

</details>

</details>

---

<details>
<summary>🛠️ 관리자 명령어</summary>

| 명령어 | 설명 |
|--------|------|
| `skillreset <플레이어이름>` | 플레이어 스킬 초기화 |
| `skilladd <숫자> <캐릭터이름>` | 스킬 포인트 추가 |

</details>

---

<details>
<summary>⚙️ 설정 관리 (Configuration Management)</summary>

**ConfigurationManager**를 사용하면 게임 내에서 GUI로 모든 모드 설정을 편리하게 조정할 수 있습니다.
게임 내 간편한 설정을 위해 **ConfigurationManager 사용을 강력히 권장**합니다:

1. Thunderstore에서 **BepInEx.ConfigurationManager** 설치
2. 게임 내에서 **F1** 키를 눌러 Configuration Manager 열기
3. 모드 목록에서 **CaptainSkillTree** 찾기
4. 슬라이더, 체크박스, 입력 필드로 원하는 설정 조정
5. 변경사항이 **즉시 적용** - 재시작 불필요!

**GUI 기능**
- **실시간 미리보기**: 변경사항을 즉시 확인
- **검색 기능**: 특정 설정을 빠르게 찾기
- **카테고리 정리**: 트리/스킬 타입별로 그룹화
- **기본값 복원**: 원클릭으로 원래 값으로 복원
- **서버 싱크 표시**: 서버에서 동기화되는 설정 표시

**Config 파일 위치**
```
BepInEx/config/CaptainSkillTree.cfg
```

| 카테고리 | 설명 | 예시 설정 |
|----------|------|----------|
| **Attack Tree** | 공격 전문가 설정 | 데미지 보너스 %, 크리티컬 확률 |
| **Speed Tree** | 속도 전문가 설정 | 이동속도 %, 공격속도 % |
| **Defense Tree** | 방어 전문가 설정 | 체력 보너스, 방어력 보너스, 회피율 |
| **Production Tree** | 생산 전문가 설정 | 채집 효율 % |
| **Bow Tree** | 활 스킬 설정 | 멀티샷 확률, 화살 수, 화살비 데미지/범위 |
| **Staff Tree** | 지팡이 스킬 설정 | 이중시전 확률, 힐량 |
| **Crossbow Tree** | 석궁 스킬 설정 | 단한발 데미지 |
| **Sword Tree** | 검 스킬 설정 | 돌진베기 데미지, 패링돌격 쿨타임 |
| **Knife Tree** | 단검 스킬 설정 | 암살자 데미지 배율 |
| **Spear Tree** | 창 스킬 설정 | 꿰뚫기 데미지, 콤보 횟수 |
| **Polearm Tree** | 폴암 스킬 설정 | 장창제왕 범위, 넉백 |
| **Mace Tree** | 둔기 스킬 설정 | 방패돌진 반경, 분노의망치 데미지 |
| **Archer Job Skills** | 궁수 직업 설정 | 멀티샷 화살 수, 쿨타임 |
| **Mage Job Skills** | 마법사 직업 설정 | 마나폭발 데미지, Eitr 소모 |
| **Tanker Job Skills** | 탱커 직업 설정 | 전장의 함성 범위/지속시간, 피해 반사 |
| **Rogue Job Skills** | 로그 직업 설정 | 암살 배율 |
| **Berserker Job Skills** | 광전사 직업 설정 | 분노 지속시간, HP당 데미지 |
| **Paladin Job Skills** | 성기사 직업 설정 | 힐량, 버프 지속시간 |
| **Producer Job Skills** | 제작전문가 직업 설정 | 팜그리드 크기, 내구도 보너스%, 재료감소%, 마법부여 확률/수치 |

**서버 관리자 참고사항**
- 모든 설정은 `IsAdminOnly = true`로 자동 서버 싱크
- 서버 설정이 클라이언트 설정을 자동으로 덮어씀
- Config 파일 변경이 감지되어 모든 클라이언트에 자동 전송
- 서버 재시작 불필요 - 변경사항이 실시간 적용

</details>

---

<details>
<summary>📥 설치 방법</summary>

1. **권장**: r2modman/Thunderstore에서 자동 설치
2. **수동**: `BepInEx/plugins/CaptainSkillTree/` 폴더에 DLL 복사

</details>

---

# 🏴󠁧󠁢󠁥󠁮󠁧󠁿 English

## Overview
CaptainSkillTree is a comprehensive skill tree mod for Valheim that adds Expert Trees, Weapon Trees, and a Job System. Enhance your Viking's abilities with passive bonuses and powerful active skills!

---

<details>
<summary>📦 Required Mods (Dependencies)</summary>

| Mod | Version | Required |
|-----|---------|----------|
| BepInExPack_Valheim | 5.4.2200+ | Required |
| Jotunn | 2.20.0+ | Required |
| EpicMMOSystem | 1.8.0+ | Recommended (works without it) |
| ConfigurationManager | Latest | Recommended (for GUI settings) |

> **Note**: CaptainSkillTree has its own built-in level system. If EpicMMOSystem is not installed, the mod will automatically use its internal leveling system.

</details>

---

<details>
<summary>✨ Features</summary>

### Comprehensive Skill Tree System
- **4 Expert Trees**: Attack, Speed, Defense, Production
- **8 Weapon Trees**: Bow, Staff, Crossbow, Knife, Sword, Mace, Spear, Polearm
- **7 Job Classes**: Archer, Mage, Tanker, Rogue, Berserker, Paladin, Producer

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

</details>

---

<details>
<summary>🗺️ How to Open Skill Tree</summary>

1. Press **TAB** to open inventory
2. Click the **Skill Tree Icon** (sword icon) near the level system button
3. Without EpicMMO: click the **Skill Tree Icon** above your character's head
4. Skill Tree UI opens with custom BGM

</details>

---

<details>
<summary>⌨️ Key Bindings</summary>

| Key | Function | Description |
|-----|----------|-------------|
| **Z** | Ranged Active | Crossbow/Bow/Staff skill (choose 1) |
| **G** | Melee Main Active | Same weapon tree only |
| **H** | Sub Active | Arrow Rain (Bow), Parry Rush (Sword), Rush Shield (Mace), etc. |
| **Y** | Job Active | 1 of 7 jobs |

</details>

---

<details>
<summary>🌳 Skill Trees</summary>

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
| Bow | Bow Expert | Explosive Arrow (Z), Arrow Rain (H) |
| Staff | Staff Expert | Double Cast, Heal (Z/H) |
| Crossbow | Crossbow Expert | Single Shot (Z) |

**Melee**
| Tree | Description | Active Skill |
|------|-------------|--------------|
| Sword | Sword Expert | Rush Slash (G), Parry Rush (H) |
| Knife | Knife Expert | Assassin (G) |
| Spear | Spear Expert | Penetrate, Combo Spear (G/H) |
| Polearm | Polearm Expert | Polearm King (G) |
| Mace | Mace Expert | Fury Hammer (G), Rush Shield (H) |

### Job Classes
| Job | Description | Active (Y) | Passive |
|-----|-------------|------------|---------|
| Archer | Bowman | Multi-Shot | Jump Height+, Fall Damage- |
| Mage | Wizard | Mana Burst | Eitr Bonus |
| Tanker | Tank | War Cry (AOE Taunt + Reflect + Damage Reduction) | Damage Reduction, HP+ |
| Rogue | Assassin | Assassination | Critical+, Stealth |
| Berserker | Berserker | Berserker Rage | Lower HP = Higher Damage |
| Paladin | Holy Knight | Holy Light | Heal, Buffs |
| Producer | Artisan | Artisan's Blessing | Farm Grid, Durability+, Material Reduction, Enchanting |

<details>
<summary>🌾 Producer (Artisan) Details</summary>

#### Farm Grid
- Equipping a **Cultivator or Hoe** automatically shows a **green grid overlay** at planting positions
- When planting seeds, additional seeds are auto-planted at all grid positions (seeds consumed from inventory)

**Mouse Wheel Controls:**

| Input | Effect | Range |
|-------|--------|-------|
| Mouse Wheel | **Rotate** grid (5° steps) | -180° ~ +180° |
| Alt + Mouse Wheel | **Adjust spacing** (0.1x steps) | 0.5x ~ 3.0x |

**Grid Size by Level:**

| Level | Grid Size | Total Cells |
|-------|-----------|-------------|
| Lv1 | 1×2 | 2 |
| Lv2 | 2×2 | 4 |
| Lv3 | 2×3 | 6 |
| Lv4 | 3×3 | 9 |
| Lv5 | 3×4 | 12 |

> Unequipping the cultivator resets the grid, rotation, and spacing.

#### Enchanting - Unlocks at Lv3
- Crafting equipment has a chance to apply a random enchantment
- Enchanted items show a **red glow border** in inventory and hotkey bar
- Item tooltip displays a **gold ✨** icon and enchant value

**Enchant Chance:**

| Level | Chance |
|-------|--------|
| Lv3 | 25% |
| Lv4 | 30% |
| Lv5 | 35% |

**Weapon Enchants** (50/50 chance each):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Weapon Damage | +3~5% | +6~8% | +9~12% |
| Attack Speed | +3~5% | +6~8% | +9~12% |

**Bow Enchants** (50/50 chance each):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Weapon Damage | +3~5% | +6~8% | +9~12% |
| Bow Crit Chance | +3~5% | +6~8% | +9~12% |

**Crossbow Enchants** (50/50 chance each):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Weapon Damage | +3~5% | +6~8% | +9~12% |
| Reload Speed | -20~40ms | -45~70ms | -75~100ms |

**Helmet Enchants** (50/50 chance each):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Max HP | +3~5% | +6~8% | +9~12% |
| Cooldown Reduce | 3~5% | 6~8% | 9~12% |

**Chest Enchants** (50/50 chance each):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Max HP | +3~5% | +6~8% | +9~12% |
| Armor | +3~5% | +6~8% | +9~12% |

**Legs Enchants** (50/50 chance each):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Dodge Stamina Cost | -3~5% | -6~8% | -9~12% |
| Move Speed | +3~5% | +6~8% | +9~12% |

**Cape Enchants** (50/50 chance each):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Max Stamina | +5~8% | +9~12% | +13~15% |
| Eitr | +5~8 | +9~12 | +13~15 |

**Accessory Enchants** (random from 3 types):

| Type | Lv3 | Lv4 | Lv5 |
|------|-----|-----|-----|
| Carry Weight | +80~100 | +100~125 | +130~150 |
| Eitr Regen | +5~8% | +9~12% | +13~15% |
| Jump Force | +5~8% | +9~12% | +13~15% |

> Applicable items: One/Two-handed weapons, Bows, Crossbows, Helmets, Chest, Legs, Capes, Accessories

<details>
<summary>🔧 Customizing Enchant Values (Producer_Enchant.json)</summary>

All enchant value ranges and slot pools are managed by `Producer_Enchant.json` embedded in the mod.
Modify this file in the source and rebuild to freely adjust any values.

#### JSON Structure

```json
{
  "enchant_types": [
    {
      "id": 6,
      "name": "BowCrit",
      "display_key": "producer_enchant_bow_crit",
      "unit": "%",
      "lv3": { "min": 3.0, "max": 5.0 },
      "lv4": { "min": 6.0, "max": 8.0 },
      "lv5": { "min": 9.0, "max": 12.0 }
    }
  ],
  "slot_pools": {
    "Bow": [{"id": 1, "weight": 1}, {"id": 6, "weight": 1}]
  }
}
```

| Field | Description |
|-------|-------------|
| `id` | Unique enchant ID (1~14, do not change) |
| `unit` | Display unit (`%`, `ms`, or empty string for flat values) |
| `lv3/lv4/lv5` | Random value range when Producer is Lv3/4/5 |
| `slot_pools` | Candidate enchant list per item slot |
| `weight` | Weight for random selection — higher = more frequent (default: all 1) |

#### Slot Key Reference

| Slot Key | Item Type |
|----------|-----------|
| `Weapon` | One/Two-handed weapons |
| `Bow` | Bows |
| `Crossbow` | Crossbows |
| `Helmet` | Helmets |
| `Chest` | Chest armor |
| `Legs` | Leg armor |
| `Shoulder` | Capes |
| `Accessory` | Accessories |

#### Customization Example

To increase Bow Crit max values and make it appear twice as often as Weapon Damage:
```json
"slot_pools": {
  "Bow": [{"id": 1, "weight": 1}, {"id": 6, "weight": 2}]
},
"enchant_types": [
  { "id": 6, "name": "BowCrit", "unit": "%",
    "lv3": {"min": 5.0, "max": 8.0},
    "lv4": {"min": 8.0, "max": 12.0},
    "lv5": {"min": 12.0, "max": 18.0} }
]
```

> Enchant **chance** (% per level) can be adjusted in-game via F1 ConfigManager → Producer Job Skills.

</details>

</details>

</details>

---

<details>
<summary>🛠️ Admin Commands</summary>

| Command | Description |
|---------|-------------|
| `skillreset <player>` | Reset player's skills |
| `skilladd <amount> <player>` | Add skill points to player |

</details>

---

<details>
<summary>⚙️ Configuration Management</summary>

**ConfigurationManager** provides a convenient in-game GUI for adjusting all mod settings.

1. Install **BepInEx.ConfigurationManager** from Thunderstore
2. Press **F1** in-game to open the Configuration Manager
3. Find **CaptainSkillTree** in the mod list
4. Adjust any setting with sliders, checkboxes, and input fields
5. Changes apply **immediately** - no restart required!

**GUI Features**
- **Real-time Preview**: See changes instantly
- **Search Function**: Quickly find specific settings
- **Category Organization**: Settings grouped by tree/skill type
- **Reset to Default**: One-click restore original values
- **Server Sync Indicator**: Shows which settings sync from server

**Config File Location**
```
BepInEx/config/CaptainSkillTree.cfg
```

| Category | Description | Example Settings |
|----------|-------------|------------------|
| **Attack Tree** | Attack Expert settings | Damage bonus %, Critical chance |
| **Speed Tree** | Speed Expert settings | Move speed %, Attack speed % |
| **Defense Tree** | Defense Expert settings | HP bonus, Armor bonus, Dodge rate |
| **Production Tree** | Production Expert settings | Gathering efficiency % |
| **Bow Tree** | Bow skill settings | Multi-shot chance, Arrow count, Arrow Rain damage/radius |
| **Staff Tree** | Staff skill settings | Double cast chance, Heal amount |
| **Crossbow Tree** | Crossbow skill settings | Single shot damage |
| **Sword Tree** | Sword skill settings | Rush slash damage, Parry Rush cooldown |
| **Knife Tree** | Knife skill settings | Assassin damage multiplier |
| **Spear Tree** | Spear skill settings | Penetrate damage, Combo count |
| **Polearm Tree** | Polearm skill settings | King skill range, Knockback |
| **Mace Tree** | Mace skill settings | Rush Shield radius, Fury Hammer damage |
| **Archer Job Skills** | Archer job settings | Multi-shot arrows, Cooldown |
| **Mage Job Skills** | Mage job settings | Mana burst damage, Eitr cost |
| **Tanker Job Skills** | Tanker job settings | War Cry range/duration, Damage reflect |
| **Rogue Job Skills** | Rogue job settings | Assassination multiplier |
| **Berserker Job Skills** | Berserker job settings | Rage duration, Damage per HP% |
| **Paladin Job Skills** | Paladin job settings | Heal amount, Buff duration |
| **Producer Job Skills** | Producer job settings | Farm grid size, Durability bonus%, Material reduction%, Enchant chance/values |

**Server Admin Notes**
- All settings have `IsAdminOnly = true` for automatic server sync
- Server settings override client settings automatically
- Config file changes are detected and broadcast to all clients
- No server restart needed - changes apply in real-time

</details>

---

<details>
<summary>📥 Installation</summary>

1. **Recommended**: Install via r2modman/Thunderstore
2. **Manual**: Extract to `BepInEx/plugins/CaptainSkillTree/`

</details>

---

## 🎮 My Other Mods / 함께 즐기면 더 좋은 모드

### 🎵 [CaptainAudio](https://thunderstore.io/c/valheim/p/korCaptain/CaptainAudio/)

> 🎶 **발헤임의 전투를 음악으로 더욱 생동감 있게!**
> CaptainSkillTree와 함께 설치하면 스킬 발동 시 웅장한 BGM과 효과음이 추가되어 **재미가 100% 상승**합니다!
>
> 🎶 **Bring your Valheim battles to life with music!**
> Install alongside CaptainSkillTree for epic BGM and sound effects on skill activation — **100% more fun guaranteed!**

---

## 📝 Credits / 크레딧

- **Developer / 개발자**: KorCaptain
- **Framework / 프레임워크**: BepInEx, Jotunn
- **Compatible with / 호환 모드**: WackyMoleEpicMMOSystem, ConfigurationManager

---

## 💬 Support / 지원

- **Discord**: KorCaptainSkillTree_MOD_Server - https://discord.gg/W26PTxYhug
- **Ko-fi**: https://ko-fi.com/korcaptain
- **E-mail**: ssunyme@naver.com
- **Issues**: Report bugs and suggestions on Discord
- **문제 보고**: Discord에서 버그 및 제안사항 보고

---

## 📜 License / 라이선스

**Developer** - KorCaptainSkillTree

---

**Enjoy your enhanced Valheim adventure! / 향상된 발헤임 모험을 즐기세요!**

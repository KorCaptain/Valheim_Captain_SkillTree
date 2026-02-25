1. Think Before Coding
Don't assume. Don't hide confusion. Surface tradeoffs.

Before implementing:

State your assumptions explicitly. If uncertain, ask.
If multiple interpretations exist, present them - don't pick silently.
If a simpler approach exists, say so. Push back when warranted.
If something is unclear, stop. Name what's confusing. Ask.
2. Simplicity First
Minimum code that solves the problem. Nothing speculative.

No features beyond what was asked.
No abstractions for single-use code.
No "flexibility" or "configurability" that wasn't requested.
No error handling for impossible scenarios.
If you write 200 lines and it could be 50, rewrite it.
Ask yourself: "Would a senior engineer say this is overcomplicated?" If yes, simplify.

3. Surgical Changes
Touch only what you must. Clean up only your own mess.

When editing existing code:

Don't "improve" adjacent code, comments, or formatting.
Don't refactor things that aren't broken.
Match existing style, even if you'd do it differently.
If you notice unrelated dead code, mention it - don't delete it.
When your changes create orphans:

Remove imports/variables/functions that YOUR changes made unused.
Don't remove pre-existing dead code unless asked.
The test: Every changed line should trace directly to the user's request.

4. Goal-Driven Execution
Define success criteria. Loop until verified.

Transform tasks into verifiable goals:

"Add validation" → "Write tests for invalid inputs, then make them pass"
"Fix the bug" → "Write a test that reproduces it, then make it pass"
"Refactor X" → "Ensure tests pass before and after"
For multi-step tasks, state a brief plan:

1. [Step] → verify: [check]
2. [Step] → verify: [check]
3. [Step] → verify: [check]
Strong success criteria let you loop independently. Weak criteria ("make it work") require constant clarification.


# CaptainSkillTree - Valheim Skill Tree Mod

## Skills

커스텀 검증 및 유지보수 스킬은 `.claude/skills/`에 정의되어 있습니다.

| Skill | Purpose |
|-------|---------|
| `verify-implementation` | 프로젝트의 모든 verify 스킬을 순차 실행하여 통합 검증 보고서를 생성합니다 |
| `manage-skills` | 세션 변경사항을 분석하고, 검증 스킬을 생성/업데이트하며, CLAUDE.md를 관리합니다 |
| `verify-localization` | 로컬라이제이션 규칙 준수 여부 검증. 하드코딩된 한글 텍스트, L.Get() 미사용, DisplayNameKey 패턴 위반 감지. |

## 글로벌 규칙
1. 한국어로 대화할 것
2. 메모리 최적화
3. 코드는 800줄 이상시 사용자에 경고안내 하고 800~1000줄 이내로 하고 필요 시 분할연동으로 만들 것

## Project Overview
CaptainSkillTree는 Valheim용 스킬트리 모드. BepInEx 플러그인 + Harmony 패치로 구현. EpicMMOSystem을 확장하여 스킬 기반 캐릭터 성장 시스템 추가.

- GitHub: https://github.com/KorCaptain/Valheim_Captain_SkillTree
- 프로젝트 폴더: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree`

## 개발 환경
- Visual Studio 2022, .NET Framework 4.7.2 / BepInEx + Harmony
- VALHEIM_INSTALL 환경 변수 설정 필수
- **빌드 출력**: `C:\Users\ssuny\Desktop\Cusor_data\bin\CaptainSkillTree.dll`

```bash
# 권장 빌드
cd C:/home/ssunyme/.npm-global/bin/CaptainSkillTree
dotnet build Captain_SkillTree.csproj -c Debug
dotnet build Captain_SkillTree.csproj -c Release
```

## 프로젝트 구조
```
CaptainSkillTree/
├── WackyEpicMMOSystem/     # 참고용 MMO 모드 (수정 금지)
├── Jotunn-dev/             # 발헤임 모드 개발 참고 (수정 금지)
└── CaptainSkillTree/       # 실제 개발 폴더
    ├── Plugin.cs           # 메인 엔트리 (수정 금지)
    ├── SkillTreeInputListener.cs  # 입력 리스너 (수정 금지)
    ├── Gui/                # UI 5개 파일
    ├── SkillTree/          # 핵심 스킬 로직 (~70개 파일)
    │   ├── *_Config.cs     # 직업별 설정
    │   ├── *Skills.cs      # 무기/직업별 스킬 구현
    │   └── Localization/   # DefaultLanguages.cs, ConfigTranslations.cs
    ├── VFX/                # VFXManager
    └── asset/              # EmbeddedResource 에셋
```

### 주요 네임스페이스
| 네임스페이스 | 용도 |
|---|---|
| `CaptainSkillTree` | Plugin, InputListener, SimpleVFX |
| `CaptainSkillTree.Gui` | UI, 노드UI, 툴팁, 줌, 버프표시 |
| `CaptainSkillTree.SkillTree` | 스킬 데이터, 설정, 효과 (메인) |
| `CaptainSkillTree.SkillTree.CriticalSystem` | 크리티컬 시스템 |
| `CaptainSkillTree.VFX` | VFX 매니저 |

---

## 필수 준수 사항 (CRITICAL RULES)

### 1. MMO 시스템 연동 우선순위
- **Tier 1 (최우선)**: MMO getParameter 패치를 통한 스탯 연동
- **Tier 2 (예외)**: 직접 패치는 MMO가 지원하지 않는 특수 효과만
- 모든 기본 스탯 효과는 MMO 시스템을 통해 구현

### 2. 패시브/액티브 VFX·사운드 규칙
- **패시브 스킬**: VFX/SFX 사용 금지, 텍스트 표시만
- **액티브 스킬**: 풍부한 VFX/SFX 구현
- **커스텀 VFX** (hit_01 등): `SimpleVFX` 사용
- **발헤임 기본 VFX** (vfx_blocked 등): `VFXManager.PlayVFXMultiplayer()` 사용
- Valheim 기본 VFX 목록: `VFX/Valheim_prefab.txt` / 커스텀 VFX: `asset/VFX/`

### 3. 스킬 변경 시 필수 동시 수정 5개 영역 (CRITICAL)

| 영역 | 파일 | 내용 |
|------|------|------|
| **Config** | `SkillTree/*_Config.cs` | GetConfigDescription() 사용 필수 |
| **효과** | `SkillTree/*Skills.cs` | HarmonyPatch 로직 |
| **툴팁** | `SkillTree/*_Tooltip.cs` | GenerateTooltip 파라미터 |
| **UI 다국어** | `Localization/DefaultLanguages.cs` | 모든 언어 키 동시 수정 |
| **Config 다국어** | `Localization/ConfigTranslations.cs` | 【】형식 번역 추가 |

> 상세 규칙 → `md/CONFIG_GUIDE.md`, `md/LOCALIZATION_GUIDE.md`

### 4. EmbeddedResource 시스템
- 모든 asset 파일은 EmbeddedResource로 DLL에 포함
- 리소스 명명: `CaptainSkillTree.asset.Resources.{bundle_name}`
- 필수 번들: `skill_start`, `skill_node`, `job_icon`, `captainskilltreeui`

### 5. 스킬 ID 명명 규칙
| 유형 | 형식 | 예시 |
|------|------|------|
| 전문가 스킬 | `{type}_expert_{attr}` | `sword_expert_damage` |
| 일반 스킬 | `{weapon}_Step{tier}_{name}` | `bow_step6_critboost` |
| 루트 노드 | `{category}_root` | `melee_root` |

### 6. 액티브 스킬 키 바인딩 및 제한
| 키 | 용도 | 제한 |
|----|------|------|
| R키 | 원거리 액티브 | 1개만 선택 가능 |
| G키 | 근접 메인 액티브 | 같은 무기 트리만 |
| H키 | 보조 액티브 | G키와 연동, 같은 무기 트리만 |
| Y키 | 직업 액티브 | 1개만 선택 가능 |

> G키/H키 연동 상세표 → `md/ACTIVE_SKILL_SYSTEM.md`

### 7. UI 렌더링 순서 (SetSiblingIndex)
```
0: 스킬트리 배경 (bgObj)
1: 노드 연결선 (line)
2: 일반 노드 아이콘
3: 직업 아이콘 (Berserker, Tanker, Rogue, Archer, Mage, Paladin)
최상위: 툴팁 (SetAsLastSibling)
```
적용 파일: `SkillTreeUI.cs`, `SkillTreeNodeUI.cs`, `SkillTreeTooltip.cs`

### 8. Config 초기화 순서 (SkillTreeConfig.cs)
```
전문가: (구분선) Attack → Speed → Defense → Production 
원거리:  (구분선) Bow → Staff → Crossbow                 
근접:   (구분선) Knife → Sword → Mace → Spear → Polearm  
직업:   (구분선) Archer → Mage → Tanker → Rogue → Paladin → Berserker
```
> 상세 규칙 (구분선 형식, 파일 구조 등) → `md/CONFIG_GUIDE.md`

### 9. 스킬 효과 누적 규칙
- 동일 효과는 서로 다른 트리에서도 반드시 **누적 합산** 적용
- 덮어쓰거나 하나만 반환하는 방식 금지

```csharp
// ✅ 올바름 - 모든 스킬 보너스 합산
return SkillBonusCalculator.CalculateTotal(
    ("speed_base", () => Config.SpeedBaseAttackSpeed),
    ("sword_step1_fastslash", () => Config.SwordFastSlash)
);
// ❌ 금지 - 하나만 반환
if (HasSkill("speed_base")) return Config.SpeedBaseAttackSpeed;
```

### 10. 공통 유틸리티 클래스 (반드시 사용)
| 클래스 | 경로 | 용도 |
|--------|------|------|
| `WeaponHelper` | `SkillTree/WeaponHelper.cs` | 무기 타입 체크 (IsUsingXXX 중복 작성 금지) |
| `SkillBonusCalculator` | `SkillTree/SkillBonusCalculator.cs` | 스킬 보너스 합산 |
| `SkillNodeBuilder` | `SkillTree/SkillNodeBuilder.cs` | 스킬 노드 생성 빌더 패턴 |

### 11. 로컬라이제이션 키 누락 방지
- 코드 작성 전 DefaultLanguages.cs에 KO + EN 키 먼저 등록
- 빌드 전 검증 스크립트 필수 실행:
  ```bash
  cd CaptainSkillTree/scripts
  powershell -ExecutionPolicy Bypass -File validate_loc_keys.ps1
  ```
> 상세 워크플로우 → `md/LOCALIZATION_GUIDE.md`

### 12. Config 다국어 번역
- `BindServerSync` Description에 **하드코딩된 영어 문자열 절대 금지**
- 반드시 `SkillTreeConfig.GetConfigDescription("키이름")` 사용
- 번역은 `Localization/ConfigTranslations.cs`에 【】형식으로 추가
> 상세 규칙 및 체크리스트 → `md/CONFIG_GUIDE.md`

### 13. 새 Config 키 추가 시 3종 세트 필수 (Config 번역 완성 규칙)
새 Config 키(`BindServerSync`)를 추가할 때 **반드시 아래 3가지를 동시에 등록**:

| 항목 | 파일 | 내용 |
|------|------|------|
| **① 2차 항목 표시명 (DispName)** | `ConfigTranslations.cs` → `GetKoreanKeyNames()` + `GetEnglishKeyNames()` | F1 Config Manager에서 키 이름 표시 |
| **② 마우스오버 세부설명 (Description)** | `ConfigTranslations.cs` → `GetDescriptionTranslations()` (KO + EN) | 마우스오버 시 나타나는 상세 설명 |
| **③ GetConfigDescription() 호출** | `*_Config.cs` → `BindServerSync()` description 파라미터 | 하드코딩 문자열 대신 반드시 사용 |

> ❌ ①②③ 중 하나라도 빠지면 F1 Config Manager에서 번역이 깨짐
> ❌ 특히 ②를 누락하면 마우스오버 설명이 영어 키 이름 그대로 표시됨

---

## 핵심 개발 원칙
1. **한국어로 응답**
2. **MMO 스탯 연동 방식 우선** - 안정성과 호환성 확보
3. **800 Line Limit** - 파일은 800라인 이하, 초과 시 분할
4. **실제 존재하는 Valheim 효과만 사용**
5. **EmbeddedResource 방식** - 모든 에셋을 DLL에 포함
6. **스킬 변경 5종 세트 원칙** - Config·효과·툴팁·UI다국어·Config다국어 동시 수정

## 금지 사항
- `Plugin.cs`, `SkillTreeInputListener.cs` 수정 금지
- 패시브 스킬에 VFX/SFX 적용 금지
- 프레임 기반 패치 금지 (이벤트 기반만)
- MMO 시스템 우회하는 직접 패치 남용 금지
- 로컬라이제이션 키 누락 금지
- Config Description 하드코딩 금지

---

## 참조 문서 (md/)

| 문서 | 내용 |
|------|------|
| `CONFIG_GUIDE.md` | Config 키 규칙, 초기화 순서, 다국어 번역, 툴팁, 멀티플레이어 동기화 |
| `LOCALIZATION_GUIDE.md` | 로컬라이제이션 키 관리, 검증 스크립트 |
| `ACTIVE_SKILL_SYSTEM.md` | 액티브 스킬 상세 규칙 |
| `MMO_INTEGRATION_GUIDE.md` | MMO getParameter 패치 |
| `DAMAGE_SYSTEM_RULES.md` | 데미지 시스템 |
| `QUICK_REFERENCE.md` | 빠른 참조 |

### 자동 트리거 Skill (Claude Code) - `.claude/skills/` 키워드 기반 자동 활성화

#### 핵심 시스템 (키워드 입력 시 자동 트리거)
| Skill | 트리거 키워드 |
|-------|-------------|
| `cst-damage` | damage, HitData, 데미지, 공격력 |
| `cst-health` | health, HP, heal, 체력 |
| `cst-critical` | critical, crit, 크리티컬 |
| `cst-attack-speed` | attack speed, AnimationSpeedManager, 공격속도 |
| `cst-evasion` | dodge, evasion, 회피 |
| `cst-armor-block` | armor, block power, 방어력, 블록 |
| `cst-config-guide` | config, BindServerSync, GetConfigDescription, 설정 |
| `cst-ui-system` | UI, panel, tooltip, SetSiblingIndex |
| `cst-eitr-stagger` | eitr, stagger, 에이트르, 스태거 |
| `cst-vfx-rules` | VFX, ZNetScene, PlayVFXMultiplayer |

#### 구현 가이드
| Skill | 트리거 키워드 |
|-------|-------------|
| `cst-mmo` | MMO, EpicMMO, getParameter |
| `cst-active-skills` | active skill, R키, G키, H키, Y키 |
| `cst-workflow` | workflow, 개발 순서, new skill |
| `cst-vfx-fix` | VFX loading, infinite loop, 무한 로딩 |
| `cst-weapon-detect` | WeaponHelper, IsUsing, 무기 감지 |
| `cst-core-protect` | Plugin.cs, InputListener, 수정 금지 |

#### 참고/디버그
| Skill | 트리거 키워드 |
|-------|-------------|
| `cst-build` | build error, 빌드 오류, CS0 |
| `cst-naming` | skill ID, naming, 명명 규칙 |
| `cst-prod-text` | production damage text, farming, 생산 데미지 |
| `cst-effect-text` | effect text, tooltip format, 효과 텍스트 |
| `cst-speed-expert` | speed tree, speed expert, 속도 전문가 |
| `cst-stagger-guide` | stagger verification, 스태거 검증 |
| `cst-quick` | quick reference, 빠른 참조 |
| `cst-harmony` | Harmony, HarmonyPatch, typeof |
| `cst-localization` | localization, L.Get(), 로컬라이제이션 |
| `cst-valheim-api` | Valheim API, ZDO, Humanoid |

#### 기타
| Skill | 트리거 키워드 |
|-------|-------------|
| `cst-proficiency` | proficiency, 숙련도 |
| `cst-tooltip` | tooltip color, 툴팁 색상 |
| `cst-parry` | parry, 패링, block detection |
| `cst-atk-spd-bug` | secondary attack speed bug |
| `cst-crossbow` | crossbow, 석궁 |
| `cst-atk-spd-debug` | attack speed debug, 공격속도 디버그 |
| `cst-patterns` | pattern, 개발 패턴 |
| `cst-mcp-setup` | MCP setup, MCP 설정 |
| `cst-buff-vfx` | 버프 VFX, buff visual |
| `cst-speed-tree-edit` | speed tree edit |


<claude-mem-context>
# Recent Activity

<!-- This section is auto-generated by claude-mem. Edit content outside the tags. -->

*No recent activity*
</claude-mem-context>

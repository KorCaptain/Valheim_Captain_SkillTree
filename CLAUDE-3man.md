# 프로젝트 규칙
파일을 읽기 전에 항상 qmd로 먼저 검색


model 사용에 대한 규칙을 명시하고 다음 규칙을 따른다.

# 멀티마켓 디버깅
1. model은 haiku 로 진행 디렉토리, 화일, 코드 검색
2. model은 haiku 로 에러 및 로그 수정
3. model은 haiku 로 Crypto 에러 로그 수정
4. sonnet은 종합 분석

# 테스트 + 품질 동시 점검
1. model은 haiku 로 pytest 실패 목록
2. model은 haiku 로 bare except 패턴
3. sonnet은 SOLID 원칙 리뷰
4. Opus는 새 차원 제안

#대규모 리팩터링
1-8. sonnet은 8개 scorer 파일 병렬 수정
9. Opus는 전체 일관성 검토

#모델별 사용 기준
##Haiku
1. 파일 탐색
2. 로그 grep
3. 단순 추출
4. 테스트 실행

##sonnet
1. 코드 수정
2. 코드 리뷰
3. 중간 복잡도 분석
4. 리팩터링

##Opus
1. 설계/ 아키택처
2. 창의적 제안
3. 최종 검토
4. 복잡한 추론

## 토큰 절약 5가지 규칙
1. 이미 읽은 파일은 다시 확인하지 않기
2. 불필요한 도구 호출은 차단하기
3. 가능한 호출은 동시에 실행하기
4. 20줄 이상 출력은 서브에이전트로 보내기
5. 사용자가 이미 설명한 내용을 다시 말하지 않기

* 밑의 내용은 아키텍트 에이전트에게 넘길것

## 코드 진행 후 검증 시스템
1. C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md 
   api의 경우 valheim_all_api.md 맞는지 확인하고 수정보완한다.
2. 액티브 및 패시브 스킬을 만들거나 수정했을 경우 C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md 
 의 알맞는 md 파일을 확인해서 문제 없는 지 확인하고 수정 보완한다.
3. 최종 빌드 해보고 에러가 생기면 에러가 안생기게 수정하고 빌드한다. 
4. 필요 검색이 필요한 경우 C:\home\ssunyme\.npm-global\bin\valheim_dll_api  폴더를 분석한다. (발헤임 ilspy 한 자료)
 
## 빠른 참조 맵 & 시스템 상세

### A. 수정 목적별 파일 빠른 참조 맵

| 작업 | 수정 파일 |
|------|---------|
| 새 스킬 추가 (예: 검 트리) | `SkillTree/MeleeSkillData.cs` + `SkillTree/Sword_Skill.cs` + `SkillTree/Sword_Config.cs` + `SkillTree/Sword_Tooltip.cs` + `Localization/DefaultLanguages_WeaponSkills.cs` + `Localization/ConfigTranslations_SwordKnifeDesc.cs` |
| 스킬 효과 패치 수정 | `SkillTree/SkillEffect.cs` 또는 `SkillTree/SkillEffect.{무기}Skills.cs` |
| UI 레이아웃 변경 | `Gui/SkillTreeUI.cs` (3223줄 – 수정 전 라인 확인 필수) |
| 툴팁 내용 변경 | `Gui/SkillTreeTooltip.cs` + 해당 `SkillTree/*_Tooltip.cs` |
| 다국어 키 추가 | `Localization/DefaultLanguages_*.cs` + `Localization/ru.json` |
| Config 키 추가 | `SkillTree/*_Config.cs` + `Localization/ConfigTranslations_*Desc.cs` |
| VFX 추가/수정 | `VFX/VFXManager.cs` + `SimpleVFX.cs` |
| BGM 수정 | `Audio/SkillTreeBGMManager.cs` |
| MMO 레벨 연동 | `MMO_System/CaptainMMOBridge.cs` + `MMO_System/CaptainLevelSystem.cs` |
| 크리티컬 시스템 | `SkillTree/CriticalSystem/Critical.cs` + `CriticalDamage.cs` |
| HUD 수정 | `Gui/ActiveSkillHUD.cs` + `Gui/SkillBuffDisplay.cs` |
| 시스템 초기화 순서 | `Plugin.Systems.cs` (497줄) |
| 입력 처리 | `Gui/SkillTreeInputListener.cs` (**수정 금지!**) |
| **인벤토리 관련 패치 수정** | **⚠️ `md/INVENTORY_PATCH_CHECKLIST.md` 반드시 먼저 확인** |
| **신규/수정 스킬 구현** | **⚠️ 완성 후 §15 성능 체크리스트 자가 점검 필수** |

> 시스템 상세 가이드:
> - UI 시스템 → `md/UI_SYSTEM_RULES.md`
> - 다국어 시스템 → `md/MULTILANGUAGE_GUIDE.md`
> - Config 시스템 → `md/CONFIG_GUIDE.md`
> - 스킬 트리 구조 → `md/SKILL_DEVELOPMENT_WORKFLOW.md`
> - VFX 시스템 → `md/ZNETSCENE_VFX_RULES.md`
> - MMO 시스템 → `md/MMO_INTEGRATION_GUIDE.md`
> - **인벤토리 패치 규칙 → `md/INVENTORY_PATCH_CHECKLIST.md` ← 멈춤 재발 방지**

---

### H. 핵심 유틸리티 클래스 (반드시 재사용)

| 클래스 | 파일 | 주요 메서드 |
|--------|------|-----------|
| `WeaponHelper` | `SkillTree/WeaponHelper.cs` | `IsUsingSword()`, `IsUsingBow()`, `IsUsingStaff()` 등 무기 감지 |
| `SkillBonusCalculator` | `SkillTree/SkillBonusCalculator.cs` | `CalculateTotal()`, `GetIfActive()`, `IsSkillActive()`, `CalculateMultiplier()` |
| `SkillNodeBuilder` | `SkillTree/SkillNodeBuilder.cs` | `Create()`, `Melee(tier)`, `Ranged(tier)`, `Defense(tier)` 팩토리 |
| `L` (헬퍼) | `Localization/L.cs` | `L.Get("key")` – 모든 텍스트에 사용 |

---

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
3. 코드는 800줄 이상시 사용자에 경고안내 하고 800~1000줄 이내로 하고 분할 하여 만든다

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

> 상세 규칙 → `md/CONFIG_GUIDE.md`, `md/MULTILANGUAGE_GUIDE.md`

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
| Z키 | 원거리 액티브 | 1개만 선택 가능 |
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
직업:   (구분선) Archer → Mage → Tanker → Rogue → Paladin → Berserker -> Producer
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
- 코드 작성 전 DefaultLanguages.cs에 KO + EN 키 **먼저** 등록
- `ru.json` 동기화 필수 (번역 없으면 EN 원문 사용)
- 빌드 전 검증 스크립트 필수 실행:
  ```bash
  cd CaptainSkillTree/scripts
  powershell -ExecutionPolicy Bypass -File validate_loc_keys.ps1
  ```
> 상세 규칙 및 체크리스트 → `md/MULTILANGUAGE_GUIDE.md`

### 12. Config 다국어 번역
- `BindServerSync` Description에 **하드코딩된 영어 문자열 절대 금지**
- 반드시 `SkillTreeConfig.GetConfigDescription("키이름")` 사용
- 번역은 `Localization/ConfigTranslations.cs`에 【】형식으로 추가
> 상세 규칙 및 체크리스트 → `md/CONFIG_GUIDE.md`, `md/MULTILANGUAGE_GUIDE.md`

### 13. 새 Config 키 추가 시 3종 세트 필수
반드시 ①DispName ②Description ③GetConfigDescription() 동시 등록.
> ❌ 하나라도 빠지면 F1 Config Manager에서 번역이 깨짐
> 상세 형식 및 예시 → `md/MULTILANGUAGE_GUIDE.md`

### 14. 다국어 7개 언어 모두 자동 동기화 (필수)
## C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\Localizationd 의 *.json 화일 모두 밑의 동기화 예실르 진행한다.
## 동기화 예시
- **DefaultLanguages*.cs에 키를 추가/수정/삭제할 때마다 `Localization/ru.json`도 반드시 동시 수정**
- 추가: 동일 키를 ru.json, de.json, ja.json, zh-cn.json, pt_BR.json, *ja.json 에 추가 (번역문 없으면 각 언어로 모두 번역사용)
- 삭제: ru.json, *.json에서도 해당 키 제거
> `md/MULTILANGUAGE_GUIDE.md` 참조

### 15. ⚠️ 성능 안전 규칙 — 메모리·서버 과부하·멈춤 방지 (신규/수정 스킬 모두 적용)

> 스킬을 **신규 생성하거나 수정할 때마다** 아래 항목을 반드시 확인할 것.
> 인벤토리 관련 패치는 추가로 `md/INVENTORY_PATCH_CHECKLIST.md` 확인.

#### 🔴 패치 발동 빈도 — 프레임 단위 패치 금지

| 금지 패턴 | 이유 | 대안 |
|---------|------|------|
| `Player.Update()` / `Character.Update()` Postfix 내 무거운 연산 | 초당 60회 실행 → CPU 과부하 | 이벤트 기반 패치로 대체 |
| `InventoryGrid.UpdateGui()` Postfix 내 `GetAllItems()` 무제한 호출 | 드래그 중 매 프레임 실행 | throttle (0.25s 이상) 필수 |
| `Hud.Update()` Postfix 내 스킬 계산 | 초당 60회 → 스태터 발생 | `SkillBonusCalculator` 프레임 캐시 사용 |
| `GetAllItems()` 반복 호출 (throttle 없음) | 인벤토리 크기에 비례해 느려짐 | static Dictionary 캐시 + 0.25s throttle |

#### 🔴 코루틴 무한 루프 — 반드시 탈출 조건 확인

```csharp
// ✅ 안전: 탈출 조건 명확
while (elapsed < duration && player != null && !player.IsDead())
{
    yield return new WaitForSeconds(0.1f); // 반드시 yield 포함
    elapsed += 0.1f;
}

// ❌ 위험: 탈출 조건 없음 → 무한 루프 → 서버 과부하
while (isActive)
{
    DoHeavyWork();
    yield return null; // yield 없으면 게임 완전 멈춤
}
```

- 모든 코루틴에 **최대 지속시간 또는 명확한 종료 플래그** 필수
- 코루틴 실행 중 플레이어 사망/로그아웃 시 자동 종료 조건 포함
- 동일 코루틴 중복 실행 방지: `if (coroutine != null) StopCoroutine()` 패턴 사용

#### 🔴 Harmony 패치 — 수리/상자/드래그 경로 오발동 방지

| 패치 메서드 | 실제 발동 상황 | 필수 early return |
|------------|--------------|-----------------|
| `InventoryGui.DoCrafting` | 제작 + **수리** 모두 호출 | `IsRepairAction()` 체크 후 수리 시 return |
| `InventoryGui.Show()` | 인벤토리 열기 + **상자 열기** 모두 호출 | 컨테이너 여부 확인 필요 시 체크 |
| `InventoryGrid.UpdateGui()` | **드래그 중 매 프레임** + 아이템 이동 | 0.25s throttle 필수 |
| `Player.ConsumeResources()` | 제작뿐 아니라 **일부 스킬 발동 시**도 호출 | craftButtonClicked 플래그 확인 |

#### 🟡 메모리 누수 방지

- `Dictionary<Player, Coroutine>` 등 플레이어 키 Dictionary: 플레이어 로그아웃 시 반드시 `Remove(player)` 처리
- `HashSet<string>` 강화 기록 (`enhancedItems`): 인벤토리 닫기 또는 일정 주기로 `Clear()` 필수
- static Texture2D / Sprite: `HideFlags.HideAndDontSave` 설정 후 씬 전환 시 재생성 여부 점검
- 이벤트 리스너 (`onClick.AddListener`): 중복 등록 방지 — `RemoveAllListeners()` 후 재등록

#### 🟡 Reflection 성능

```csharp
// ✅ 안전: 1회 캐싱
private static FieldInfo _myField;
private static FieldInfo GetMyField() =>
    _myField ??= typeof(TargetClass).GetField("m_field", BindingFlags.NonPublic | BindingFlags.Instance);

// ❌ 위험: 패치 메서드 호출마다 실행 → GC + 느림
void Postfix(...) {
    var field = typeof(TargetClass).GetField("m_field", ...); // 매번 실행!
}
```

#### 🟡 ZNet RPC / 멀티플레이어 부하

- `ZNetScene.instance.SpawnObject()` 또는 RPC 호출: 루프 내 반복 금지, 단발성만 허용
- `VFXManager.PlayVFXMultiplayer()`: 패시브 스킬에서 호출 금지 (피격마다 실행 → 서버 패킷 폭증)
- `ZDO.Set()` — **이벤트 발생 시 1회 호출이 원칙**:
  - ✅ 올바름: 스킬 발동, 버프 시작/종료, 상태 변경 **시점**에만 호출
  - ❌ 잘못됨: 코루틴 내 주기적 반복 호출 (`while` 루프 안에서 매 틱마다 `ZDO.Set()`)
  - ⚠️ 불가피한 경우만 예외: 버프 잔여시간처럼 값이 지속 변하는 경우에 한해 주기적 호출 허용 — 이때도 최소 0.5s 간격 유지. 단, 이 구조 자체가 설계 냄새이므로 이벤트 기반으로 리팩터링 검토 필요

#### 신규/수정 스킬 완성 후 자가 점검 체크리스트

```
[ ] 이 스킬의 Harmony 패치가 초당 몇 번 발동되는가?
    → 이벤트성(제작/피격/사용)이면 OK, Update급이면 캐시/throttle 추가
[ ] 코루틴에 최대 지속시간 또는 종료 플래그가 있는가?
[ ] DoCrafting 패치라면 IsRepairAction() early return이 있는가?
[ ] GetAllItems() 호출에 throttle이 있는가?
[ ] Reflection이 static 캐시를 사용하는가?
[ ] 플레이어 키 Dictionary가 있다면 퇴장 시 정리되는가?
[ ] ZNet RPC / VFX가 루프 내에서 반복 호출되지 않는가?
```

> 인벤토리 관련 추가 체크 → `md/INVENTORY_PATCH_CHECKLIST.md`

---

## 핵심 개발 원칙
1. **한국어로 응답**
2. **MMO 스탯 연동 방식 우선** - 안정성과 호환성 확보
3. **800 Line Limit** - 파일은 800라인 이하, 초과 시 분할
4. **실제 존재하는 Valheim 효과만 사용**
5. **EmbeddedResource 방식** - 모든 에셋을 DLL에 포함
6. **스킬 변경 7종 세트 원칙** - Config·효과·툴팁·UI다국어·Config다국어 동시 수정·스킬관련 메시지 수정(한국어, English, Русский, Português-Brasil, das Deutsche, 中国话, 日本語)
7. **성능 3종 체크 원칙** - 신규/수정 스킬마다 ①패치 발동빈도 ②코루틴 탈출조건 ③메모리 정리 반드시 확인 (규칙 §15 참조)

## 금지 사항
- `SkillTreeInputListener.cs` 수정 금지
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
| `MULTILANGUAGE_GUIDE.md` | 다국어 키 관리, Config 번역, 검증 스크립트, 체크리스트 |
| `ACTIVE_SKILL_SYSTEM.md` | 액티브 스킬 상세 규칙 |
| `MMO_INTEGRATION_GUIDE.md` | MMO getParameter 패치 |
| `DAMAGE_SYSTEM_RULES.md` | 데미지 시스템 |
| `QUICK_REFERENCE.md` | 빠른 참조 |
| `UI_SYSTEM_RULES.md` | UI 시스템 상세 |
| `ZNETSCENE_VFX_RULES.md` | VFX 시스템 상세 |
| `SKILL_DEVELOPMENT_WORKFLOW.md` | 스킬 트리 구조 상세 |

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
| `cst-producer-display` | producer enchant, 제작 축복, 제작 전문가 툴팁, crafting blessing, producer tooltip, weapon enchant display, armor enchant display |
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
| `cst-changelog` | changelog, 변경로그, CHANGELOG, 버전업, version up, 배포 준비 |


<claude-mem-context>
# Recent Activity

<!-- This section is auto-generated by claude-mem. Edit content outside the tags. -->

*No recent activity*
</claude-mem-context>

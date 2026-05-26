# LOCALIZATION_RULES.md — 다국어/로컬라이제이션 규칙

> ⚠️ 로컬라이제이션 관련 코드 수정 전 이 파일을 반드시 읽을 것
> 전체 가이드 → `md/MULTILANGUAGE_GUIDE.md`

---

## 지원 언어 (7개)

| 코드 | 언어 | 비고 |
|------|------|------|
| `ko` | 한국어 | 기준 언어 |
| `en` | 영어 | fallback 언어 |
| `de` | 독일어 | |
| `ru` | 러시아어 | |
| `pt_BR` | 포르투갈어 (브라질) | |
| `zh-cn` | 중국어 간체 | |
| `ja` | 일본어 | |

---

## 핵심 규칙

### R1. 스킬 변경 시 로컬라이제이션 7종 세트
스킬 추가/수정 시 아래를 **동시에** 수정:
1. `DefaultLanguages_*.cs` — KO 블록 키 추가/수정
2. `DefaultLanguages_*.cs` — EN 블록 키 추가/수정
3. `ru.json` — 동기화 (번역 없으면 EN 원문 임시값 사용)
4. `de.json` — 동기화 (번역 없으면 EN 원문 임시값 사용)
5. `pt_BR.json` — 동기화 (번역 없으면 EN 원문 임시값 사용)
6. `zh-cn.json` — 동기화 (번역 없으면 EN 원문 임시값 사용)
7. `ja.json` — 동기화 (번역 없으면 EN 원문 임시값 사용)

### R2. 키 추가 올바른 순서
```
1. DefaultLanguages_*.cs 에 KO + EN 키 먼저 추가
2. 코드에서 L.Get("key") 사용
3. validate_loc_keys.ps1 실행
4. 빌드 (경고 0개 확인)
```
❌ 코드에 L.Get() 먼저 쓰고 키 나중에 추가 → Warning 발생

> ⚠️ **DrawFloatingText / MessageHud 연동 시 특히 중요**  
> 누락 키 → L.Get()이 `[FAIL] Key not found` 경고 출력 → Unity 폰트 아틀라스 재빌드 → **인게임 렉 발생**  
> DrawFloatingText에 새 L.Get() 키를 추가할 때 반드시 R2 순서를 지킬 것.

### R3. L.Get() 필수 / 하드코딩 금지
```csharp
// ❌ 금지
Player.m_localPlayer.Message(MessageHud.MessageType.Center, "레벨이 감소했습니다.");
ui.text = "제작 축복";

// ✅ 필수
Player.m_localPlayer.Message(MessageHud.MessageType.Center, L.Get("level_decrease_msg"));
ui.text = L.Get("producer_blessing_text");
```

### R4. Config 번역 — GetConfigDescription() 필수
```csharp
// ❌ 금지 (하드코딩)
description: "공격 속도 증가 보너스"

// ✅ 필수
description: GetConfigDescription("Tier0_AttackSpeed")
```
ConfigTranslations.cs에 KO + EN 3종 세트(DisplayName, Description, KeyName)를 먼저 추가할 것.

### R5. {N} 플레이스홀더 — 언어 간 불일치 금지
```json
// EN (기준)
"spear_desc_expert": "Spear damage +{0}%, speed +{1}%"

// ❌ 잘못된 DE → FormatException 게임 크래시
"spear_desc_expert": "Schaden +{0}%, Geschwindigkeit +{1}%, Kraft +{2}%"

// ✅ 올바른 DE
"spear_desc_expert": "Schaden +{0}%, Geschwindigkeit +{1}%"
```
**{N} 개수·번호는 EN과 모든 언어에서 동일해야 함.**

### R6. 스킬 ID 언어 중립성
```
// ❌ 금지
"성기사", "광전사"

// ✅ 필수 (영문 snake_case)
"paladin", "berserker"
```

### R9. 로그 메시지에 유니코드 특수문자 금지 (폰트 렉 방지)

```csharp
// ❌ 금지 — ✗/✓ 는 Valheim-AveriaSansLibre 폰트에 없음
// → Unity 폰트 아틀라스 재빌드 → 렉 발생
Plugin.Log.LogWarning($"[Localization] ✗ Key not found: '{key}'");

// ✅ 필수 — ASCII 대체 사용
Plugin.Log.LogWarning($"[Localization] [FAIL] Key not found: '{key}'");
Plugin.Log.LogDebug($"[Localization] [OK] Loaded successfully");
```

**BepInEx → Unity Debug.Log 파이프라인 때문에 로그 문자열도 폰트 아틀라스에 영향을 줌.**  
모든 Plugin.Log 메시지에서 `✗ ✓ ✘ ✔` 등 유니코드 특수문자 사용 금지. `[OK]` / `[FAIL]` / `[WARN]` 사용.

### R7. 번역 파일 분리 원칙
| 파일 | 용도 |
|------|------|
| `DefaultLanguages*.cs` | 스킬트리 UI (노드명, 툴팁, 버프 표시) |
| `ConfigTranslations.cs` | F1 Config Manager (카테고리, 설정 설명) |

❌ 혼용 금지: DefaultLanguages.cs에 Config 키 추가하거나 반대로 하면 안 됨.

### R8. 새 JSON 파일 추가 시 csproj 등록 필수
```xml
<!-- Captain_SkillTree.csproj -->
<EmbeddedResource Include="Localization\xx.json" />
```
미등록 시 `LoadFromEmbeddedResource("xx")` 항상 null 반환.

---

## 검증 스크립트

```bash
cd C:/home/ssunyme/.npm-global/bin/CaptainSkillTree/scripts
powershell -ExecutionPolicy Bypass -File validate_loc_keys.ps1
```

**실행 시점**: 키 추가/수정 후, 빌드 전, 커밋 전

---

## 체크리스트

### 스킬/효과 추가·수정 시
- [ ] DefaultLanguages_*.cs — KO 블록 키 추가
- [ ] DefaultLanguages_*.cs — EN 블록 키 추가
- [ ] {0}, {1} 플레이스홀더 개수·번호 모든 언어 동일 확인
- [ ] ru/de/pt_BR/zh-cn/ja.json 동기화
- [ ] validate_loc_keys.ps1 실행 (누락 키 0개)

### 새 Config 추가 시
- [ ] ConfigTranslations.cs — KO/EN DisplayName 추가
- [ ] ConfigTranslations.cs — KO/EN Description 추가
- [ ] Config 파일 — GetConfigDescription() 사용

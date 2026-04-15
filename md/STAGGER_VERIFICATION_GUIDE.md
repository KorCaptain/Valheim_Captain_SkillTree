# 비틀거림(Stagger) 시스템 검증 가이드

**Rule 19 검증 절차** - Character.IsStaggering() API 작동 여부 확인

---

## 📋 검증 목표

1. ✅ `Character.IsStaggering()` API가 정상적으로 호출되는가?
2. ✅ 비틀거림 상태를 정확하게 감지하는가?
3. ✅ 단검 데미지가 30% 증가하는가?
4. ✅ 다양한 적 유형에서 작동하는가?
5. ✅ 멀티플레이어 환경에서 작동하는가?

---

## 🎮 Phase 1: API 존재 확인 (필수)

### 1단계: 게임 실행 및 스킬 활성화

```
1. Valheim 실행
2. 캐릭터 로드
3. 스킬 트리 UI 열기 (기본: U키)
4. knife_stagger 스킬 활성화
   - 위치: 단검 트리 → Tier 3
   - 이름: "기절 공격" 또는 "Stagger Attack"
5. 단검을 장착 (예: 사냥용 칼, 청동 단검 등)
```

### 2단계: 비틀거림 유발 및 공격

```
적합한 테스트 대상:
- 그레이드워프 (Greydwarf) - 추천 ⭐
- 스켈레톤 (Skeleton)
- 드라우그르 (Draugr)

비틀거림 유발 방법:
1. 둔기(클럽, 메이스)로 적을 3-5회 공격
2. 적이 비틀거림 상태가 되면 (휘청거리는 애니메이션)
3. 즉시 단검으로 공격

⚠️ 중요: 단검 전문가(knife_expert)도 활성화되어야 함!
```

### 3단계: 로그 확인

**로그 위치:**
- `C:\Users\[사용자명]\AppData\Roaming\r2modmanPlus-local\Valheim\profiles\Cusor_1\BepInEx\LogOutput.log`

**예상 로그 출력:**

#### ✅ 성공 시나리오 (API 정상 작동)
```log
[Debug   :CaptainSkillTree] [Stagger 검증] knife_stagger 스킬 활성화됨 (레벨: 1)
[Debug   :CaptainSkillTree] [Stagger 검증] IsStaggering() 호출 성공 → 결과: False
[Debug   :CaptainSkillTree] [Stagger 검증] knife_stagger 스킬 활성화됨 (레벨: 1)
[Debug   :CaptainSkillTree] [Stagger 검증] IsStaggering() 호출 성공 → 결과: True
[Warning :CaptainSkillTree] [Stagger 검증 성공] 비틀거림 추가 피해 적용!
[Warning :CaptainSkillTree]   → 원본 피해: 25.0
[Warning :CaptainSkillTree]   → 보너스: +30% (x1.30)
[Warning :CaptainSkillTree]   → 최종 피해: 32.5
```

#### ❌ 실패 시나리오 (API 오류)
```log
[Debug   :CaptainSkillTree] [Stagger 검증] knife_stagger 스킬 활성화됨 (레벨: 1)
[Error   :CaptainSkillTree] [Stagger 검증 실패] IsStaggering() API 오류: Method not found
[Error   :CaptainSkillTree]   → 스택: [스택 트레이스]
```

---

## 🧪 Phase 2: 추가 피해 검증 (성공 시 진행)

Phase 1에서 API가 정상 작동하면 추가 테스트 진행:

### 테스트 1: 데미지 증가 확인

```
1. 단검으로 일반 적 공격 (비틀거림 없음)
   → 피해량 기록 (예: 25.0)

2. 둔기로 비틀거림 유발 후 단검 공격
   → 피해량 기록 (예: 32.5)

3. 계산 확인:
   32.5 = 25.0 × 1.30 ✅
   (30% 증가 확인)
```

### 테스트 2: 다양한 적 유형

```
- [ ] 그레이드워프 (Greydwarf)
- [ ] 그레이드워프 무리 (Greydwarf Brute)
- [ ] 스켈레톤 (Skeleton)
- [ ] 드라우그르 (Draugr)
- [ ] 트롤 (Troll) - 대형 적
- [ ] 보스 (예: 고대 장로) - 선택적

각 적 유형에서 Warning 로그 출현 확인
```

### 테스트 3: 비틀거림 타이밍

```
1. 비틀거림 시작 직후 → 작동 여부
2. 비틀거림 중간 → 작동 여부
3. 비틀거림 끝나기 직전 → 작동 여부
4. 비틀거림 종료 후 → False 확인

예상 결과: 비틀거림 지속 시간 동안만 True
```

---

## 🌐 Phase 3: 멀티플레이어 테스트 (선택적)

### 호스트 테스트
```
1. 멀티플레이어 서버 생성 (호스트)
2. knife_stagger 스킬 활성화
3. Phase 1-2 반복
4. 로그 확인
```

### 클라이언트 테스트
```
1. 다른 플레이어의 서버 접속
2. knife_stagger 스킬 활성화
3. Phase 1-2 반복
4. 로그 확인

⚠️ 클라이언트에서도 동일하게 작동해야 함
```

---

## ⚡ Phase 4: 성능 영향 측정

### FPS 측정
```
1. F2 키로 FPS 표시 활성화
2. 일반 전투 시 FPS 기록
3. Stagger 보너스 발동 시 FPS 기록
4. FPS 하락 확인 (5% 이내 허용)
```

### 로그 스팸 확인
```
BepInEx 로그에서 과도한 메시지 확인:
- 1초당 10개 이상 메시지 → 문제
- 비틀거림 중에만 로그 → 정상
```

---

## ✅ 검증 체크리스트

### Phase 1: API 존재 확인
- [ ] knife_stagger 스킬 활성화 로그 출력
- [ ] IsStaggering() 호출 성공 로그 출력
- [ ] 에러 메시지 없음

### Phase 2: 기능 검증
- [ ] 비틀거림 상태 정확 감지 (True/False)
- [ ] 30% 데미지 증가 확인
- [ ] 다양한 적 유형 테스트 (최소 3종류)
- [ ] 비틀거림 타이밍 테스트

### Phase 3: 멀티플레이어 (선택적)
- [ ] 호스트 환경 테스트
- [ ] 클라이언트 환경 테스트

### Phase 4: 성능
- [ ] FPS 영향 5% 이내
- [ ] 로그 스팸 없음

---

## 📊 검증 결과 보고 양식

```markdown
## Stagger System 검증 결과

**검증 일자:** YYYY-MM-DD
**게임 버전:** Valheim x.x.x
**모드 버전:** CaptainSkillTree v0.1.64

### Phase 1: API 존재 확인
- [ ] ✅ 성공 / [ ] ❌ 실패
- 로그 출력: [복사 붙여넣기]
- 오류 메시지: [없음 / 오류 내용]

### Phase 2: 기능 검증
- [ ] ✅ 성공 / [ ] ❌ 실패
- 테스트한 적: [그레이드워프, 스켈레톤, ...]
- 데미지 증가 확인: [예: 25.0 → 32.5]

### Phase 3: 멀티플레이어
- [ ] ✅ 성공 / [ ] ❌ 실패 / [ ] ⏭️ 건너뜀
- 호스트: [성공/실패]
- 클라이언트: [성공/실패]

### Phase 4: 성능
- FPS 영향: [X%]
- 로그 스팸: [없음 / 있음]

### 최종 결론
- [ ] ✅ Rule 19 검증 완료 - "⚠️" 마크 제거 가능
- [ ] ❌ Rule 19 검증 실패 - 대안 API 조사 필요

### 추가 의견
[자유 작성]
```

---

## 🔄 다음 단계

### ✅ 검증 성공 시
1. **EITR_STAGGER_SYSTEM_RULES.md 업데이트**
   - "⚠️ 검증 필요" → "✅ 검증 완료" 변경

2. **Plugin.cs 로그 정리**
   - LogDebug 메시지 제거 (정상 작동 확인 시)
   - LogWarning을 LogInfo로 변경
   - 또는 주석 처리

3. **SKILL_DEVELOPMENT_WORKFLOW.md 업데이트**
   - Category 9 (Stagger)를 "✅ 검증 완료"로 표시

4. **다른 스킬에 Stagger 시스템 적용**
   - 예: 둔기 비틀거림 보너스, 도끼 기절 효과 등

### ❌ 검증 실패 시
1. **대안 API 조사**
   ```csharp
   // 대안 1: m_staggerTimer 필드 확인
   float staggerTimer = Traverse.Create(__instance).Field("m_staggerTimer").GetValue<float>();
   if (staggerTimer > 0) { /* 비틀거림 중 */ }

   // 대안 2: Character.Stagger() 메서드 후킹
   [HarmonyPatch(typeof(Character), "Stagger")]
   // 비틀거림 발생 시점 감지

   // 대안 3: HitData.m_staggerMultiplier 확인
   if (hit.m_staggerMultiplier > 0) { /* 비틀거림 피해 */ }
   ```

2. **EITR_STAGGER_SYSTEM_RULES.md 업데이트**
   - 검증 실패 사유 기록
   - 대안 구현 방식 문서화

3. **Plugin.cs 수정**
   - IsStaggering() 대신 대안 API 사용
   - 재검증 진행

---

## 🚨 주의사항

1. **단검 전문가 필수**: knife_expert 스킬 활성화 필수
2. **비틀거림 유발**: 둔기 사용 권장 (비틀거림 확률 높음)
3. **로그 레벨**: BepInEx 로그 레벨을 Debug로 설정 필요
   - `C:\Users\[사용자명]\AppData\Roaming\r2modmanPlus-local\Valheim\profiles\Cusor_1\BepInEx\config\BepInEx.cfg`
   - `LogLevel = Debug` 확인

4. **타이밍 중요**: 비틀거림 애니메이션이 보이는 순간 즉시 공격

---

**검증 완료 후 이 가이드에 따라 결과를 보고해주세요!**

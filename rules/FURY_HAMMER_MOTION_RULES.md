# 분노의 망치 공격 모션 규칙

> ⚠️ 이 파일의 규칙을 어기면 공격 모션이 의도치 않게 바뀝니다. 수정 전 반드시 읽을 것.

## 모션 구조 (변경 금지)

| 단계 | 모션 | 구현 방법 |
|------|------|-----------|
| 도약(상승) | 없음 | 물리 포물선만 — 모션 없음 |
| 하강 진입(`t > 0.5`) | **단검 세컨드** (`m_secondaryAttack`) | 루프 내 `knifeAnimTriggered` 플래그로 1회만 트리거 |
| 착지 | **슬랫지 primary** (`m_attack`, 좌클릭 내려치기) | `GetCachedSledgeIronAttack()` + `zanim.SetTrigger()` |

> **핵심 규칙**:  
> - 단검 세컨드 = 바디 애니메이션 (공중 자세 → 하강 동작)  
> - 슬랫지 primary = 무기 타격 모션 (착지 순간 내려치기)  
> - 상승 시 트리거하면 모션의 "점프" 구간이 물리 포물선과 겹쳐 두 배로 높아 보임

## 절대 금지 사항

### R1: 도약 구간에서 `StartAttack()` 호출 금지
```csharp
// ❌ 금지 — 현재 장착 무기(일반 둔기 등)의 모션으로 오염됨
player.StartAttack(null, false);

// ✅ 올바른 방법
zanim.SetTrigger(knifeSecondaryAttack.m_attackAnimation);
```

**이유**: `StartAttack(null, false)`는 현재 장착 무기의 `m_attack`을 사용한다.  
슬랫지가 아닌 둔기(철 메이스, 철퇴 등)를 들고 있으면 전혀 다른 모션이 나온다.

### R2: 착지 공격에 `m_secondaryAttack` 사용 금지
```csharp
// ❌ 금지 — 슬랫지 세컨드는 회전 모션
GetCachedSledgeIronSecondaryAttack()

// ✅ 올바른 방법 — 슬랫지 왼쪽 클릭 내려치기
GetCachedSledgeIronAttack()   // m_attack (primary)
```

### R3: 캐시 메서드 무시하고 ObjectDB 직접 탐색 금지
- 단검 도약 모션: 반드시 `GetCachedKnifeSecondaryAttack()` 사용
- 슬랫지 착지 모션: 반드시 `GetCachedSledgeIronAttack()` 사용

## `furyHammer1stHitBuff` 설정 위치

```csharp
// ❌ 도약 시점에서 설정 금지
furyHammer1stHitBuff[player] = true;  // 도약 직후 X

// ✅ 착지 시점에서만 설정
// ApplyFuryHammer() 내 착지 블록에서만 true로 설정
furyHammer1stHitBuff[player] = true;  // 착지 공격 모션 트리거 직전
```

## 관련 파일

- 구현: `SkillTree/MaceSkills.FuryHammer.cs`
- 캐시 메서드: `GetCachedKnifeSecondaryAttack()`, `GetCachedSledgeIronAttack()`
- 레퍼런스 문서: `md/fury.md`

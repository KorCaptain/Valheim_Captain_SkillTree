# 분노의 망치 (FuryHammer) 구현 정리

## 파일
`SkillTree/MaceSkills.FuryHammer.cs`

## 스킬 개요
- **키**: H키 (둔기 전문가 전용)
- **조건**: 양손둔기 착용 필수
- **동작**: 즉시 시전 → 0.5초 포물선 대시 → 착지 → 5연타 데미지

---

## 핵심 구현 패턴

### 1. 공격 모션 (t=0 즉시)
```csharp
// applyRootMotion=false → 대시 중 위치 제어 충돌 방지
animator.applyRootMotion = false;

// furyHammer1stHitBuff=true → AnimationSpeedManager가 +100% 적용
furyHammer1stHitBuff[player] = true;

// StartAttack → InAttack()=true → AnimationSpeedManager 활성화
bool attackStarted = player.StartAttack(null, false);
if (!attackStarted)
    zanim.SetTrigger(animTrigger); // 폴백
```

### 2. 포물선 대시 (while loop, 0.5초)
```csharp
Vector3 pos = Vector3.Lerp(startPos, endPos, t);
pos.y = startPos.y + peakHeight * 4f * t * (1f - t); // 포물선 공식
player.transform.position = pos;
player.transform.rotation = lookRot; // 매 프레임 방향 고정
```

**조기 착지 조건** (t > 0.5 하강 구간):
- 지면 raycast 감지
- 적 3m 이내 근접

### 3. 착지
```csharp
animator.applyRootMotion = origRootMotion; // Root Motion 복원
VFXManager.PlayVFXMultiplayer("vfx_sledge_hit", "", endPos, ...); // 착지 VFX
ResetFuryHammerSpeed(player, animator, 0.5f); // 0.5s 후 속도 복원
```

### 4. 공격속도 시스템
| 항목 | 내용 |
|------|------|
| 방식 | `AnimationSpeedManager` (직접 animator.speed 조작 금지) |
| 핸들러 위치 | `Plugin.Patches.cs:654` |
| 활성 조건 | `player.InAttack()=true` + `furyHammer1stHitBuff=true` |
| 보너스 | `GetFuryHammer1stHitSpeedBonus()` → `100f` (+100%) |
| 캡 우회 | `IsFuryHammer1stHitBuffActive()=true` 시 cap 미적용 |

### 5. VFX
| 시점 | VFX |
|------|-----|
| 착지 (지면) | `vfx_sledge_hit` at endPos |
| 몬스터 적중 (1타) | `vfx_sledge_hit` at mob.GetCenterPoint() |
| 1타 | `fx_crit` at mob.GetCenterPoint() |
| 2타 | `flash_round_ellow` |
| 3타 | `water_blast_blue` |
| 4타 | `flash_star_ellow_purple` |
| 5타 (최종) | `fx_siegebomb_explosion` |

---

## 주요 트러블슈팅

### animator.speed 직접 조작이 안 먹히는 이유
- `AnimationSpeedManager`가 매 프레임 `animator.speed`를 덮어씀
- **반드시** `furyHammer1stHitBuff[player] = true` + `StartAttack()`으로 InAttack=true 만들어야 함

### 제자리 복귀 문제
- 원인: 공격 애니메이션 Root Motion이 플레이어를 당김
- 해결: `animator.applyRootMotion = false` (트리거 전) → 착지 시 복원

### 공중에서 VFX 발동 문제
- 원인: `zanim.SetTrigger()` 단독 사용 시, 이전 일반 공격의 Attack 객체가 살아있으면 애니메이션 이벤트가 공중에서 VFX 발동
- 해결: `StartAttack()` 방식으로 전환 + 착지 시 수동 `vfx_sledge_hit` 재생

### StartAttack 공중 실패 문제
- 원인: 대시 중 `CanMove()=false` (m_maxAirAltitude 높음)
- 해결: t=0 (지상)에서 호출. 착지 후 `altField.SetValue(player, endPos.y)`로 고도 리셋

---

## 타이밍 흐름
```
t=0    : applyRootMotion=false, buff=true, StartAttack() → 모션+속도 시작
t=0~0.5: 포물선 대시, 매 프레임 위치/방향 오버라이드
t=0.5  : 착지 확정, applyRootMotion 복원, vfx_sledge_hit, ResetFuryHammerSpeed 시작
t=0.5+0.2: 1타 데미지 (0.2s 임팩트 프레임 대기)
t=1.0  : buff=false, animator.speed=1f 복원
t=0.5~4.7: 2~5타 데미지 (0.8s/0.8s/0.8s/1.2s+0.5s 간격)
```

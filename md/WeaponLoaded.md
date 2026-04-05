# Valheim 석궁 장전 상태 API

## 핵심 API

```csharp
player.IsWeaponLoaded()  // → bool
```

- **위치**: `Player` 클래스 (`assembly_valheim_publicized.dll`)
- **반환값**: 석궁에 볼트가 완전히 장전된 경우 `true`
- **관련 필드**: `m_weaponLoaded` (private bool)
- **관련 메서드**: `SetWeaponLoaded(bool loaded)`

## 사용 예시

```csharp
// 석궁 장전 완료 체크
if (!player.IsWeaponLoaded())
{
    DrawFloatingText(player, L.Get("crossbow_not_loaded"), Color.yellow);
    return;
}
```

## 조사 경위

### 문제
`Animator.GetBool("Loaded")` 방식이 볼트가 장전되어 있음에도 항상 `false` 반환.

```csharp
// ❌ 잘못된 방법 - 사용 금지
var animator = player.GetComponentInChildren<Animator>();
if (animator == null || !animator.GetBool("Loaded"))
{
    DrawFloatingText(player, L.Get("crossbow_not_loaded"), Color.yellow);
    return;
}
```

**원인**: `GetComponentInChildren<Animator>()`가 엉뚱한 Animator를 반환하거나,
발헤임 내부 상태와 Animator 파라미터가 동기화되지 않는 시점 문제.

### 해결 과정

1. `assembly_valheim_publicized.dll` 바이너리에서 관련 심볼 검색 (PowerShell):
   ```powershell
   $bytes = [System.IO.File]::ReadAllBytes("path\to\assembly_valheim_publicized.dll")
   $text = [System.Text.Encoding]::ASCII.GetString($bytes)
   # "Loaded", "WeaponLoad", "IsWeapon" 등 키워드 검색
   ```

2. 발견된 심볼:
   - `IsWeaponLoaded` ← 공식 API
   - `SetWeaponLoaded`
   - `m_weaponLoaded`

3. `Player.IsWeaponLoaded()` 로 교체 후 빌드 성공 (오류 0개) 및 정상 작동 확인.

## 적용 위치

| 파일 | 메서드 | 역할 |
|------|--------|------|
| `SkillTree/SkillEffect.CrossbowIceBreath.cs` | `ActivateCrossbowIceBreath()` | 빙결 폭발탄(H키) 장전 체크 |

## 관련 로컬라이제이션 키

| 키 | KO | EN |
|----|----|----|
| `crossbow_not_loaded` | 볼트가 장전되지 않았습니다! | Crossbow is not loaded! |

## 주의사항

- `IsWeaponLoaded()`는 석궁 전용. 활·지팡이 등 다른 원거리 무기에는 해당 없음.
- 반드시 `WeaponHelper.IsUsingCrossbow(player)` 체크 **이후**에 호출할 것.
  (석궁이 아닌 경우 `IsWeaponLoaded()`의 반환값 의미 없음)
- 게임 로직상 석궁은 발사 후 자동 장전 애니메이션이 재생되며,
  애니메이션 완료 전까지 `IsWeaponLoaded()`는 `false` 반환.

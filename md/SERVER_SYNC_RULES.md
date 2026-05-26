# 서버 Config 동기화 시스템 규칙

> 최종 업데이트: 2026-05-15
> 관련 작업: 지연 접속자 자동 동기화 + 클라이언트 로컬 변경 차단

---

## 1. 시스템 개요

**목적**: 서버 Config 값을 모든 클라이언트에게 강제 적용  
**핵심 원칙**: 클라이언트는 서버 Config만 사용하며, F1 메뉴에서 로컬 변경해도 게임 수치에 무효

| 플레이어 유형 | Config 적용 기준 |
|-------------|----------------|
| 서버 (또는 솔로) | 로컬 Config 값 직접 사용 |
| 클라이언트 (서버 Config 수신 후) | `_serverConfigValues` 우선 사용 |
| 클라이언트 (수신 전) | 로컬 Config 값 임시 사용 (접속 2초 후 자동 교체) |

---

## 2. 관련 파일

| 파일 | 역할 |
|------|------|
| `SkillTree/SkillTreeConfig.Broadcast.cs` | Config 직렬화 및 RPC 전송 (`BroadcastConfigToClients`) |
| `SkillTree/SkillTreeConfig.AdminSync.cs` | 변경 감지·차단·RPC 수신 핸들러 |
| `SkillTree/SkillTreeConfig.cs` | `_serverConfigValues`, `_hasReceivedServerConfig`, `GetEffectiveValue()` |
| `Plugin.Patches.cs` | `ZNet.RPC_PeerInfo` 패치 (접속 감지 → 자동 전송) |
| `Plugin.Systems.cs` | RPC 핸들러 등록 (`InitializeServerSync`) |

---

## 3. RPC 동작 흐름

```
[서버 Config 변경 (F1 메뉴 / skillconfig sync 명령)]
  → OnServerSettingChanged() — debounce 1.5초 적용
  → BroadcastConfigToClients()
  → ZRoutedRpc.InvokeRoutedRPC(Everybody, "SkillTreeMod_ConfigSync", configString)

[신규 클라이언트 접속]
  → ZNet.RPC_PeerInfo Postfix (서버 측 실행)
  → 2초 딜레이 코루틴 (클라이언트 RPC 핸들러 등록 대기)
  → BroadcastConfigToClients(peer.m_uid) — 해당 클라이언트만 타겟
  → ZRoutedRpc.InvokeRoutedRPC(peerId, "SkillTreeMod_ConfigSync", configString)

[클라이언트 수신]
  → RPC_ReceiveConfigSync(sender, jsonData)
  → ReceiveServerConfig(configString)
  → _serverConfigValues 딕셔너리 갱신
  → _hasReceivedServerConfig = true
  → RefreshAllSkillEffects() 호출

[이후 GetEffectiveValue() 호출 시]
  → !_isServer && _hasReceivedServerConfig → _serverConfigValues[key] 반환
  → 클라이언트 로컬 ConfigEntry 값은 무시됨

[클라이언트가 F1에서 Config 변경 시도]
  → OnClientSettingChanged() — 경고 로그 출력
  → GetEffectiveValue()가 서버 값 반환 → 실제 게임 수치 변화 없음
```

---

## 4. 핵심 메서드

### `BroadcastConfigToClients(long targetPeerId = 0)`
- `Plugin.Patches.cs` → `SkillTreeConfig.Broadcast.cs`
- `targetPeerId = 0`: 전체 클라이언트 (`ZRoutedRpc.Everybody`)
- `targetPeerId != 0`: 해당 peer에만 전송
- **서버에서만 실행** (`if (!_isServer) return;` 가드)

### `GetEffectiveValue(string key, float localValue)`
- `SkillTreeConfig.cs`
- 클라이언트에서 `_hasReceivedServerConfig = true`면 항상 서버 값 반환
- 모든 수치형 Config 항목은 이 메서드를 통해 읽어야 함

### `ReceiveServerConfig(string configString)`
- `SkillTreeConfig.cs`
- 서버 RPC 수신 시 호출됨
- `DeserializeConfigData()` → `_serverConfigValues` 갱신 → `_hasReceivedServerConfig = true`

### `DetectServerClientMode()`
- `ZNet.Awake` Postfix에서 호출
- `_isServer = ZNet.instance == null || ZNet.instance.IsServer()`

---

## 5. 새 Config 값 추가 시 필수 작업

새 Config 항목을 서버 싱크 대상에 포함하려면:

1. **`SkillTreeConfig.Broadcast.cs`** — `BroadcastConfigToClients()` 내부 딕셔너리에 추가
   ```csharp
   ["my_new_config_key"] = MyConfig.NewValue?.Value ?? 0f,
   ```

2. **Config 읽는 곳** — `localValue` 직접 사용 대신 `GetEffectiveValue()` 사용
   ```csharp
   // 변경 전
   float val = SkillTreeConfig.NewValue.Value;
   
   // 변경 후
   float val = SkillTreeConfig.GetEffectiveValue("my_new_config_key", SkillTreeConfig.NewValue.Value);
   ```

> ⚠️ 딕셔너리 키 문자열과 `GetEffectiveValue()` 호출 키가 **정확히 일치**해야 함 (대소문자 구분)

---

## 6. 디버그 로그 키워드

| 로그 접두사 | 발생 위치 | 의미 |
|-----------|---------|------|
| `[ConfigSync]` | `Plugin.Patches.cs` | 신규 접속자 자동 동기화 완료 |
| `[SkillTreeConfig]` | `Broadcast.cs` | Config 브로드캐스트 전송 |
| `[AdminSync]` | `AdminSync.cs` | 어드민 Config 전송 / 클라이언트 변경 차단 |
| `[ServerSync]` | `Systems.cs` | RPC 핸들러 등록 경고 |

---

## 7. 주의사항

- `_isProcessingRpcUpdate = true` 플래그 활성 중에는 `OnServerSettingChanged`, `OnClientSettingChanged` 모두 스킵됨 (RPC 수신 루프 방지)
- `ZNet.RPC_PeerInfo` 패치는 **서버 인스턴스**에서만 실행 (`__instance.IsServer()` 확인)
- 2초 딜레이 코루틴 내에서 peer 유효성 재확인 (`GetPeer(peerId) != null`)
- 솔로 플레이 시 `_isServer = true` → 브로드캐스트 경로 타지 않음

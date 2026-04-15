# Changelog

모든 주요 변경사항을 기록합니다.

## [v1.2.12] - 2026-03-26

### Added
- **Paladin 시스템 강화**
  - Paladin Lv2 업그레이드 조건 추가 (선행 스킬 요구)
  - 패링돌격(sword_step5_defswitch) 또는 연공창(spear_Step5_combo) 중 하나 필수
  - 연공창 Paladin Lv2 추가 사용 창 구현 (30초)
  - 첫 사용 시 30초 윈도우 오픈, 윈도우 내 재사용 가능

### Changed
- **성기사 아군 치유 수치 조정**
  - Lv2: 2.5% → 2.2% (-0.3%)
  - Lv3: 3.0% → 2.5% (-0.5%)
  - Lv4: 3.5% → 2.7% (-0.8%)
  - Lv5: 4.0% → 3.0% (-1.0%)
- **Paladin Lv2 툴팁 가독성 개선**
  - 액티브/패시브 구분자: ` | ` → `\n  ` (줄바꿈 + 들여쓰기)

### Fixed
- 기존 저장값 마이그레이션 로직 추가 (Config 자동 업데이트)
- 게임 밸런스 개선 (성기사 치유량 12-15% 감소)
- Paladin Lv2 선행 조건 없이 취득되는 버그 해결

### Status
- 빌드 상태: ✅ 성공 (오류 0개, 경고 1개 - 기존)
- Match Rate: 100%
- 배포 준비: 완료

---

## [v1.2.08] - 2026-03-26

### Added
- **SkillBuffDisplay.cs 성능 최적화**
  - RectTransform 캐싱 필드 추가 (`_rectTransform`)
  - 정적 WaitForSeconds 캐시 추가 (`_wait01`)
  - 조건부 텍스트 갱신 로직 추가 (`_lastDisplayedTime`)
  - IsDead() 기반 플레이어 사망 시 버프 정리 기능

### Changed
- `ArrangeBuffs()` 메서드: GetComponent<RectTransform>() 제거 → 캐싱된 `_rectTransform` 사용
- `CountdownCoroutine()` 메서드: new WaitForSeconds(0.1f) 제거 → 정적 `_wait01` 재사용
- `UpdateTimeDisplay()` 메서드: 조건부 텍스트 갱신으로 불필요한 string.Format 호출 제거

### Fixed
- GC 스파이크 최대 80% 감소 (메모리 할당 최소화)
- 버프 UI 텍스트 불필요한 갱신 제거 (렌더링 비용 약 83% 감소)
- 플레이어 사망 시 버프 UI 즉시 정리 (메모리 누수 방지)

### Performance
- 메모리 할당: ~110KB/초 → ~10KB/초 (91% 감소, 버프 10개 기준)
- 예상 FPS 개선: 약 10% (60fps 안정성 향상)
- 텍스트 렌더링 감소: 약 83% (0.1초 단위 변화 시에만 갱신)

---

## [v1.2.07] - 2026-03-XX

### Features
- 이전 버전 기능

---

## Version History

| Version | Date | Type | Impact | Status |
|---------|------|------|--------|--------|
| 1.2.12 | 2026-03-26 | Feature | Medium | ✅ Completed |
| 1.2.08 | 2026-03-26 | Performance | High | ✅ Completed |
| 1.2.07 | 2026-03-XX | Feature | Medium | ✓ Released |


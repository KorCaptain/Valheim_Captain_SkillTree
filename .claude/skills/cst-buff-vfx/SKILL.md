---
name: cst-buff-vfx
description: Use when implementing persistent buff-type VFX that follows the character. Triggers: 버프 VFX, buff visual, 지속 VFX, follow VFX, 버프 이펙트, buff effect
---

## 핵심 규칙 요약

- **버프형 VFX**: 캐릭터를 따라다니며 지속되는 시각 효과 (버프 활성화 상태 표시)
- VFX를 캐릭터 Transform의 자식으로 부착하여 따라다니게 구현
- 버프 해제 시 VFX 정리 필수 (`OnDestroy` 또는 버프 종료 시 Destroy)
- 패시브 스킬: VFX 금지, 액티브 스킬 버프 효과에만 사용
- `VFXManager.PlayVFXMultiplayer()` 방식 사용 (무한 로딩 방지)

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\버프형_VFX.md`

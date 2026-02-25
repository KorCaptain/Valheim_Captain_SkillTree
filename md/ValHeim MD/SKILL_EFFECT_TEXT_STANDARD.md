# 스킬 효과 텍스트 표준화 가이드

## 🎯 개요
CaptainSkillTree 모드에서 스킬 효과 발동 시 DamageText를 활용한 로컬 지역 표시 방식의 표준화 시스템입니다.

## 🔧 표준화된 API

### 기본 메서드
```csharp
SkillEffect.ShowSkillEffectText(Player player, string text, Color color, SkillEffectTextType type)
```

### 텍스트 타입 분류
```csharp
public enum SkillEffectTextType
{
    Standard,    // 일반 스킬 효과 (화면 중앙)
    Combat,      // 전투 관련 효과 (더 눈에 띄는 위치)
    Passive,     // 패시브 효과 (조용한 표시)
    Critical     // 중요한 효과 (강조 표시)
}
```

## 🎨 색상 가이드라인

### 공격 전문가 트리 색상 체계
- **루트 노드**: 골드색 `new Color(1f, 0.8f, 0.2f)` - 중요한 전문가 습득
- **근접 특화**: 빨간색 `new Color(1f, 0.3f, 0.3f)` - 근접 전투
- **활 특화**: 초록색 `new Color(0.2f, 0.8f, 0.2f)` - 자연적인 활
- **석궁 특화**: 노란색 `new Color(1f, 0.9f, 0.3f)` - 기계적인 석궁
- **지팡이 특화**: 마젠타 `new Color(1f, 0.2f, 1f)` - 마법적인 지팡이
- **치명타**: 진한 빨간색 `new Color(1f, 0.1f, 0.1f)` - 강렬한 효과
- **속성 공격**: 보라색 `new Color(0.8f, 0.2f, 0.8f)` - 속성 마법
- **강화 효과**: 청록색 `new Color(0.2f, 0.8f, 0.8f)` - 버프 효과

### 텍스트 타입별 표시 위치
- **Standard**: 화면 중앙 (MessageHud.MessageType.Center)
- **Combat**: 상단 좌측 (MessageHud.MessageType.TopLeft) - 전투 중 더 눈에 띄게
- **Passive**: 상단 좌측 (MessageHud.MessageType.TopLeft) - 조용하게
- **Critical**: 화면 중앙 크게 (`<size=20>`) - 강조

## 📋 사용 예시

### 공격 전문가 적용 예시
```csharp
// 루트 노드 습득 시
SkillEffect.ShowSkillEffectText(player, "⚔️ 공격 전문가 습득!", 
    new Color(1f, 0.8f, 0.2f), SkillEffect.SkillEffectTextType.Critical);

// 전투 중 특화 효과 발동
SkillEffect.ShowSkillEffectText(player, "💥 근접 특화!", 
    new Color(1f, 0.3f, 0.3f), SkillEffect.SkillEffectTextType.Combat);

// 치명타 발동
SkillEffect.ShowSkillEffectText(player, "💀 치명타!", 
    new Color(1f, 0.1f, 0.1f), SkillEffect.SkillEffectTextType.Critical);
```

## 🔄 기존 DrawFloatingText와의 호환성
```csharp
// 기존 코드 (하위 호환성 유지)
SkillEffect.DrawFloatingText(player, "텍스트", Color.red);

// 새로운 표준화된 방식 (권장)
SkillEffect.ShowSkillEffectText(player, "텍스트", Color.red, SkillEffectTextType.Combat);
```

## 🎯 다른 트리 적용 시 가이드라인

### 1. 색상 체계 설계
각 트리별로 고유한 색상 테마 설정:

#### 속도 전문가 트리 색상 체계
- **기본 스킬**: 청록색 `new Color(0.3f, 0.7f, 1f)` - 시원한 속도감
- **콤보 효과**: 밝은 파란색 `new Color(0.4f, 0.8f, 1f)` - 연속성
- **특화 효과**: 하늘색 `new Color(0.2f, 0.9f, 1f)` - 전문성
- **패시브 효과**: 연한 파란색 `new Color(0.5f, 0.8f, 1f)` - 조용한 버프

#### 생산 전문가 트리 색상 체계  
- **일반 생산**: 초록색 `new Color(0.4f, 0.8f, 0.4f)` - 자연적인 생산
- **벌목**: 황록색 `new Color(0.6f, 0.8f, 0.2f)` - 나무의 자연색
- **채집**: 연두색 `new Color(0.2f, 0.8f, 0.4f)` - 식물/베리류
- **제작**: 주황색 `new Color(0.8f, 0.6f, 0.2f)` - 금속/제작 도구

#### 기타 트리 계획
- **방어 트리**: 회색/은색 계열 (견고함, 보호)
- **직업 트리**: 각 직업별 특색 있는 색상

### 2. 텍스트 타입 선택
- **스킬 습득**: `Critical` (중요한 순간)
- **전투 효과**: `Combat` (전투 중 표시)
- **패시브 효과**: `Passive` (조용한 알림)
- **일반 효과**: `Standard` (기본)

### 3. 이모지 및 텍스트 가이드
- 간결하고 직관적인 텍스트 사용
- 각 스킬의 특성을 나타내는 이모지 활용
- 일관된 명명 규칙 적용

## ✅ 표준화의 장점

1. **일관성**: 모든 스킬 트리에서 동일한 방식의 피드백
2. **가독성**: 텍스트 타입별로 적절한 위치와 크기로 표시
3. **확장성**: 새로운 트리 추가 시 쉽게 적용 가능
4. **유지보수**: 중앙화된 텍스트 표시 시스템으로 관리 용이
5. **사용자 경험**: 로컬 플레이어만 보이는 개인화된 피드백

## 🔧 구현 상태

### ✅ 완료된 트리
- **공격 전문가 트리**: 모든 스킬 효과에 표준화 시스템 적용 완료
- **속도 전문가 트리**: 모든 스킬 효과에 표준화 시스템 적용 완료 (파란색 계열)
- **생산 전문가 트리**: 모든 스킬 효과에 표준화 시스템 적용 완료 (초록색 계열)

### 🔄 예정된 트리
- 방어 전문가 트리
- 직업 스킬 트리
- 근접/원거리 전문가 트리

이 표준화 시스템을 통해 CaptainSkillTree 모드의 모든 스킬 효과가 일관되고 직관적인 피드백을 제공할 수 있습니다.
# Captain_SkillTree (스킬트리 모드)

## 프로젝트 구조

```
Captain_SkillTree/
├── mmo/                # 참고용 mmo 모드(수정 금지)
├── CaptainSkillTree/   # 실제 스킬트리 모드 개발 폴더
│   ├── Plugin.cs       # 메인 플러그인 엔트리
│   ├── SkillTree/      # 스킬트리 핵심 로직 및 데이터
│   │   └── .gitkeep
│   ├── Gui/            # 스킬트리 UI 및 패널
│   │   └── .gitkeep
│   ├── Data/           # 스킬트리 데이터(저장/로드)
│   │   └── .gitkeep
│   ├── Thunderstore/   # 배포/패키징 관련 파일
│   │   └── .gitkeep
│   ├── Properties/     # .NET/Unity 프로젝트 설정
│   │   └── .gitkeep
│   └── README.md       # (옵션) 서브 프로젝트 설명
└── README.md           # 프로젝트 설명
```

## 개발 원칙
- mmo(WackyEpicMMOSystem) 모드의 구조, 데이터, 서버 싱크, UI, 효과 적용 방식을 100% 준수
- mmo 모드가 반드시 설치되어 있어야 정상 동작
- mmo 모드의 파일은 참고만 하며, 직접 수정하지 않음
- 스킬트리 모드의 모든 데이터/저장/포인트/효과는 mmo와 동일한 방식으로 구현
- mmo가 패치되면 스킬트리 모드도 즉시 구조/코드를 맞춰 패치

## 주요 연동 방식
- mmo의 레벨업, 포인트, UI, 서버 싱크 등 핵심 기능을 활용
- 탭키(인벤토리) UI에 스킬트리 버튼 추가, 클릭 시 스킬트리 UI 오픈
- 레벨업 시 스킬트리 포인트 3 지급, 투자/효과는 mmo/발헤임의 기존 기능 위주로 구현

---

## 📦 배포 방법

### 빌드 결과물
```bash
# Release 빌드 실행
cd CaptainSkillTree && dotnet build Captain_SkillTree.csproj -c Release

# 생성되는 파일들 (C:\Users\ssuny\Desktop\Cusor_data\bin\)
- CaptainSkillTree.dll (22MB) - 메인 모드 파일
- AnimationSpeedManager.dll (8KB) - 공격속도 시스템 라이브러리
```

### 다른 사람에게 배포 시
**두 파일 모두 포함**해서 배포하세요:

```
📁 CaptainSkillTree_v0.1.199.zip
├── CaptainSkillTree.dll
└── AnimationSpeedManager.dll
```

**설치 방법 (사용자용)**:
1. r2modman/Thunderstore 사용 시: 자동 설치
2. 수동 설치 시:
   ```
   압축 해제 → BepInEx/plugins/CaptainSkillTree/ 폴더에 복사
   ```

### 의존성 (Dependencies)
**필수 모드**:
- BepInExPack_Valheim (5.4.2200+)
- Jotunn (2.20.0+)
- **WackyEpicMMOSystem (1.8.0+)** - MMO 시스템 연동

**참고**: AnimationSpeedManager.dll은 WackyEpicMMOSystem에도 포함되어 있지만, CaptainSkillTree에서 직접 제공하여 독립적으로 작동할 수 있습니다.

---

> ⚠️ mmo 폴더는 참고용이며, 실제 스킬트리 모드는 CaptainSkillTree 폴더에서 개발합니다. 
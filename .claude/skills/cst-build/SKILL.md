---
name: cst-build
description: Use when encountering build errors or compilation failures. Triggers: build error, 빌드 오류, CS0, error, compile, 컴파일 에러, dotnet build
---

## 핵심 규칙 요약

- **빌드 명령**: `dotnet build Captain_SkillTree.csproj -c Debug/Release`
- **아이콘 미표시**: MMO 패치의 `TargetMethod()` 오류 → `Harmony.PatchAll()` 실패가 주원인
- **CS0** 오류: 네임스페이스/using 문 확인, 참조 DLL 경로 확인
- AnimationSpeedManager 오류: `Lib\AnimationSpeedManager.dll` 존재 여부 + `<Private>True</Private>` 확인
- **빌드 출력**: `C:\Users\ssuny\Desktop\Cusor_data\bin\CaptainSkillTree.dll`
- VALHEIM_INSTALL 환경 변수 미설정 시 일부 참조 실패 가능

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\BUILD_ERRORS_GUIDE.md`
